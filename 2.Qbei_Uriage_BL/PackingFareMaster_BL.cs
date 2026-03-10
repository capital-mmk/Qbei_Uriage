using System.Data;
using _3.Qbei_Uriage_DL;
using Qbei_Uriage_Common;


namespace Qbei_Uriage_BL
{
  public  class PackingFareMaster_BL
    {
      PackingFareMaster_DL pmdl;

        public PackingFareMaster_BL()
        {
            pmdl = new PackingFareMaster_DL();
        }

      
      public DataTable PackingFareMaster_Select(PackingFareMaster_Entity pme)
      {
          return pmdl.PackingFareMaster_Select(pme);
      }
      //public DataTable PackingFareMaster_SelectAll(PackingFareMaster_Entity pme)
      //{
      //    return pmdl.PackingFareMaster_SelectAll(pme);
      //}

      public bool PackingFareMaster_Insert(PackingFareMaster_Entity pme)
      {
          return pmdl.PackingFareMaster_Insert(pme);
      }

      public bool PackingFareMaster_Update(PackingFareMaster_Entity pme)
      {
          return pmdl.PackingFareMaster_Update(pme);
      }
      public bool PackingFareMaster_Delete(PackingFareMaster_Entity pme)
      {
          return pmdl.PackingFareMaster_Delete(pme);
      }
      public DataTable PackingFare_SelectAll()
      {
          return pmdl.PackingFare_SelectAll();
      }
    }
}
