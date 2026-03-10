using System.Data;
using _3.Qbei_Uriage_DL;
using Qbei_Uriage_Common;


namespace Qbei_Uriage_BL
{
    public class OutsourcingMaster_BL
    {
        OutsourcingMaster_DL omdl;


        public OutsourcingMaster_BL()
        {
            omdl = new OutsourcingMaster_DL();
        }

        public DataTable Outsourcing_SelectAll()
        {
            return omdl.Outsourcing_SelectAll();
        }

        public DataTable OutsourcingMaster_Select(OutSourcingMaster_Entity ome)
        {
            return omdl.OutsourcingMaster_Select(ome);
        }

        public bool OutsourcingMaster_Insert(OutSourcingMaster_Entity ome)
        {
            return omdl.OutsourcingMaster_Insert(ome);
        }
        public bool OutsourcingMaster_Update(OutSourcingMaster_Entity ome)
        {
            return omdl.OutsourcingMaster_Update(ome);
        }
        public bool OutsourcingMaster_Delete(OutSourcingMaster_Entity ome)
        {
            return omdl.OutsourcingMaster_Delete(ome);
        }
    }
}
