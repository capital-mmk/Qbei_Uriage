using System.Data;
using _3.Qbei_Uriage_DL;
using Qbei_Uriage_Common;

namespace Qbei_Uriage_BL
{
    public class InsuranceMaster_BL
    {
        InsuranceMaster_DL imdl;

        public InsuranceMaster_BL()
        {
            imdl = new InsuranceMaster_DL();
        }

        public DataTable InsuranceMaster_Select(InsuranceMaster_Entity ie)
        {
            return imdl.InsuranceMaster_Select(ie);
        }

        public bool InsuranceMaster_Insert(InsuranceMaster_Entity ie)
        {
            return imdl.InsuranceMaster_Insert(ie);
        }

        public bool InsuranceMaster_Update(InsuranceMaster_Entity ie)
        {
            return imdl.InsuranceMaster_Update(ie);
        }

        public bool InsuranceMaster_Delete(InsuranceMaster_Entity ie)
        {
            return imdl.InsuranceMaster_Delete(ie);
        }

        public DataTable Insurance_SelectAll()
        {
            return imdl.Insurance_SelectAll();
        }
    }
}
