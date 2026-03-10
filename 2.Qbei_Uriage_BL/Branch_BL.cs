using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3.Qbei_Uriage_DL;
using System.Data;
using _4.Qbei_Uriage_Common;

namespace _2.Qbei_Uriage_BL
{
   public class Branch_BL
    {
       Branch_DL bdl = new Branch_DL();
        public DataTable Branch_SelectAll()
        {
            return bdl.Branch_SelectAll();
        }
        public DataTable Branch_Search(Branch_Entity be)
        {
            return bdl.Branch_Search(be);
        }
        public bool IsDuplicateBranch(string branchCode)
        {
            return bdl.IsDuplicateBranch(branchCode);
        }
        public bool Branch_Save(Branch_Entity be)
        {
          
            return bdl.Branch_Save(be);
        }
        public DataTable Branch_Edit(Branch_Entity be)
        {
           
            return bdl.Branch_Edit(be);
        }
        public bool Branch_Update(Branch_Entity be)
        {
           
            return bdl.Branch_Update(be);
        }
        public bool BranchDelete(Branch_Entity be)
        {

            return bdl.BranchDelete(be);
        }

    }
}
