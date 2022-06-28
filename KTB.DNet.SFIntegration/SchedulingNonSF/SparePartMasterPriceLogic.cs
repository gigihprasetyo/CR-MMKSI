using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.BusinessFacade.SparePart;
using KTB.DNet.Domain;
using KTB.DNET.BusinessFacade;
using KTB.DNet.Domain.Search;
using System.Security.Principal;
using System.Data;
using System.Data.SqlClient;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class SparePartMasterPriceLogic
    {
        static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);

        public static List<SparePartMasterPrice> GetPriceToUpdate()
        {
            List<SparePartMasterPrice> SPPriceList = new List<SparePartMasterPrice>();
            SPPriceList = new SparePartMasterPriceFacade(User).GetSparePartPrice(DateTime.Now);
            
            return SPPriceList;
        }

        public static async Task UpdateSparePartMasterRetalPrice()
        {
            List<SparePartMasterPrice> SPPriceList = new List<SparePartMasterPrice>();
            SPPriceList = GetPriceToUpdate();
            if (SPPriceList.Count > 0)
            {
                foreach (SparePartMasterPrice oSPPrice in SPPriceList)
                {
                    Boolean Result = new SparePartMasterPriceFacade(User).UpdateSparePartMaster(oSPPrice.SparePartMaster.ID, oSPPrice.RetailPrice, User.Identity.Name, oSPPrice.ValidFrom);
                }
                
            }
        }
    }
}
