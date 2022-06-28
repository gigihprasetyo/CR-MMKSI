#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator 
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2019 
// ---------------------
// $History      : $
// Created on 21/01/2020 14:18:44]
//
// ===========================================================================	
#endregion

using Swashbuckle.Examples;

namespace KTB.DNet.Interface.WebApi.Models.Examples
{
    public class APIUpdateVWI_CRM_SLS_SalesUnitExample : IExamplesProvider
    {
        public object GetExamples()
        {
            var obj = new
            {
                UpdatedBy = "DealerUser"
				,company = "company Value"
				,businessunitcode = "businessunitcode Value"
				,xts_newvehicledeliveryordernumber = "xts_newvehicledeliveryordernumber Value"
				,ktb_vehiclecolorname = "ktb_vehiclecolorname Value"
				,ktb_tanggalpkt = "ktb_tanggalpkt Value"
				,sitename = "sitename Value"
				,xts_productdescription = "xts_productdescription Value"
				,personinchargename = "personinchargename Value"
				,xts_driver = "xts_driver Value"
				,xts_deliverydate = "xts_deliverydate Value"
				,customername = "customername Value"
				,xts_productexteriorcolor = "xts_productexteriorcolor Value"
				,xts_warehouse = "xts_warehouse Value"
				,productcategorycode = "productcategorycode Value"
				,productcategorydescription = "productcategorydescription Value"
				,msdyn_companycode = "msdyn_companycode Value"
            };

            return obj;
        }
    }
}