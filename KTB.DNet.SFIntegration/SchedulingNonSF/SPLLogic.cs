using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.Domain;
using System.Security.Principal;
using KTB.DNet.BusinessFacade.FinishUnit;
using KTB.DNet.Utility;
using KTB.DNET.BusinessFacade;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class SPLLogic
    {
        public static void ProcessSPL(){
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            SPLDetailFacade func = new SPLDetailFacade(User);

            List<SPLDetail> ListSPL = new List<SPLDetail>();
            List<SPLDetail> ListSPLDetailtoInsert = new List<SPLDetail>();
            //List<SPLDetailtoSPL> ListDetailSPLtoInsert = new List<SPLDetailtoSPL>();

            ListSPL = func.GetSPLDataToExtend();

            if(ListSPL.Count > 0){
                foreach (SPLDetail sd in ListSPL)
                {
                    int sisa = sd.Quantity - SPLFunction.GetResponseQtyPKDetail(sd);
                    if (sisa > 0)
                    {
                        SPLDetail NewSPLDetail = new SPLDetail();
                        NewSPLDetail.VechileType = sd.VechileType;
                        NewSPLDetail.SPL = sd.SPL;
                        NewSPLDetail.PriceRefDate = sd.PriceRefDate;
                        NewSPLDetail.Discount = sd.Discount;
                        NewSPLDetail.Surcharge = sd.Surcharge;
                        NewSPLDetail.MaxTopDate = sd.MaxTopDate;
                        NewSPLDetail.MaxTopDay = sd.MaxTopDay;
                        NewSPLDetail.MaxTopIndicator = sd.MaxTopIndicator;
                        NewSPLDetail.FreeIntIndicator = sd.FreeIntIndicator;
                        NewSPLDetail.CreditCeiling = sd.CreditCeiling;
                        NewSPLDetail.DeliveryDate = sd.DeliveryDate;
                        NewSPLDetail.PeriodMonth = DateTime.Now.Month;
                        NewSPLDetail.PeriodYear = DateTime.Now.Year;
                        NewSPLDetail.Quantity = sisa;
                        
                        SPLDetailFacade SPLDetailFac = new SPLDetailFacade(User);
                        int Result = SPLDetailFac.Insert(NewSPLDetail);
                        if (Result > 0)
                        {
                            SPLDetailtoSPLFacade SDSFac = new SPLDetailtoSPLFacade(User);
                            SPLFacade SPLFac = new SPLFacade(User);
                            List<SPLDetailtoSPL> ListSDS = SDSFac.RetrieveBySPLDetail(sd.ID);
                            foreach (SPLDetailtoSPL SPDs in ListSDS)
                            {
                                SPLDetailtoSPL SDS = new SPLDetailtoSPL();

                                SDS.SPLDetail = SPLDetailFac.Retrieve(Result);
                                SDS.SPLDetailReference = SPDs.SPLDetailReference;
                                SDS.DiscountMaster = SPDs.DiscountMaster;
                                SDS.Discount = SPDs.Discount;

                                int Res = SDSFac.Insert(SDS);
                            }

                            SPL oSPL = new SPL();
                            oSPL = SPLFac.Retrieve(sd.SPL.ID);
                            oSPL.ValidTo = oSPL.ValidTo.AddMonths(1);

                            int Rs = SPLFac.Update(oSPL);
                        }
                    }
                }
            }
            
        }
    }
}
