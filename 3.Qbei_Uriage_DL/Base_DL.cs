using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace _3.Qbei_Uriage_DL
{
    public class Base_DL
    {
        public String DataTableToXml(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            //string result = writer.ToString();
            return result;
        }

        
        public SqlConnection GetConnection()
        {
            string constr = ConfigurationManager.ConnectionStrings["Qbei_Uriage"].ToString();
            return new SqlConnection(constr);
        }
        
        #region Variables
        protected SqlConnection connection;
        protected SqlCommand command;
        protected SqlTransaction transaction;
        protected SqlDataAdapter adapter;
        #endregion

        #region Properties
        public SqlTransaction Transaction
        {
            get;
            private set;
        }

        public bool UseTransaction
        {
            get;
            private set;
        }
        #endregion

        #region Constructors
        public Base_DL()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Qbei_Uriage"].ConnectionString);
        }
        #endregion

        #region Transaction Methods
        public void StartTransaction()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            transaction = connection.BeginTransaction();
            UseTransaction = true;
        }

        public void StartTransaction(SqlTransaction transaction)
        {
            this.transaction = transaction;
            this.connection = transaction.Connection;
            UseTransaction = true;
        }

        public void CommitTransaction()
        {
            if (transaction != null)
                transaction.Commit();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            UseTransaction = false;
        }

        public void RollBackTransaction()
        {
            if (transaction != null)
                transaction.Rollback();

            if (connection.State == ConnectionState.Open)
                connection.Close();

            UseTransaction = false;
        }
        #endregion

        protected void AddParam(SqlCommand cmd, string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                cmd.Parameters.AddWithValue(key, DBNull.Value);
            else
                cmd.Parameters.AddWithValue(key, value);
        }

        public DataTable SelectData(Dictionary<string, string> dic, string sp)
        {
            DataTable dt = new DataTable();
            command = new SqlCommand(sp, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (KeyValuePair<string, string> pair in dic)
            {
                AddParam(command, pair.Key, (pair.Value == null) ? pair.Value : pair.Value.Trim());
            }
                adapter = new SqlDataAdapter(command);
                  adapter.Fill(dt);
            
            return dt;
        }

        public bool InsertUpdateDeleteData(Dictionary<string, string> dic, string sp)
        {
            try
            {
                if (UseTransaction)
                    command = new SqlCommand(sp, connection, transaction);
                else
                    command = new SqlCommand(sp, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (KeyValuePair<string, string> pair in dic)
                {
                    AddParam(command, pair.Key, (pair.Value == null) ? pair.Value : pair.Value.Trim());
                }

                if (!UseTransaction)
                    command.Connection.Open();
                command.ExecuteNonQuery();
                if (!UseTransaction)
                    command.Connection.Close();
                return true;
            }
            catch (Exception)
            {
                RollBackTransaction();
                return false;
            }
        }

        public int InsertData(Dictionary<string, string> dic, string sp)
        {
            try
            {
                if(UseTransaction)
                    command = new SqlCommand(sp, connection,transaction);
                else
                    command = new SqlCommand(sp, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (KeyValuePair<string, string> pair in dic)
                {
                    AddParam(command, pair.Key, (pair.Value==null)?pair.Value:pair.Value.Trim());
                }

                if(!UseTransaction)
                    command.Connection.Open();
                int id = (int)command.ExecuteScalar();
                if(!UseTransaction)
                    command.Connection.Close();
                return id;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
