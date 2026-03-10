using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Data;
using _4.Qbei_Uriage_Common;
using _3.Qbei_Uriage_DL;


namespace _2.Qbei_Uriage_BL
{
   public class QbeiUser_BL
    {
       QbeiUser_DL qudl = new QbeiUser_DL();

        //Delete_User
        public bool Delete_User(QbeiUser_Entity que)
        {
            QbeiUser_DL qudl = new QbeiUser_DL();
            return qudl.Delete_User(que);
        }

        public DataTable checkLogin(QbeiUser_Entity que)
       {
          
           DataTable dt = qudl.Qbei_User_Select(que);

           return dt;
           //if(dt.Rows.Count >0)
           //return true;
           //return false;
       }
        public DataTable checkExist(QbeiUser_Entity que)
        {

            DataTable dt = qudl.CheckExist(que);

            return dt;
            //if(dt.Rows.Count >0)
            //return true;
            //return false;
        }
        

       public DataTable UserList_SelectAll()
       {
           return qudl.UserList_SelectAll();
       }
       public DataTable UserList_Search(QbeiUser_Entity qe)
       {
           return qudl.UserList_Search(qe);
       }
       public bool User_Save(QbeiUser_Entity qe)
       {
           QbeiUser_DL qudl = new QbeiUser_DL();
           return qudl.User_Save(qe);
       }
       public DataTable Qbei_UserEdit(QbeiUser_Entity qe)
       {
           QbeiUser_DL qudl = new QbeiUser_DL();
           return qudl.Qbei_User_Edit(qe);
       }
       public bool User_Update(QbeiUser_Entity que)
       {
           QbeiUser_DL qudl = new QbeiUser_DL();
           return qudl.User_Update(que);
       }

}
}
