using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using KTB.DNet.BusinessFacade.PO;
using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.BusinessFacade.Service;
using KTB.DNet.Domain.Search;
using KTB.DNET.BusinessFacade.Service;
using KTB.DNet.SFIntegration.Parser;
using KTB.DNET.BusinessFacade;



namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class ServiceReminderMaster
    {
        static GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire3"), null);
        static ArrayList arrPMKind = new ArrayList();
        private static int batasMaxLastService = Convert.ToInt32(new AppConfigFacade(User).Retrieve("GSR.BatasMaxLastService").Value);
        private static int timeAddLastService = Convert.ToInt32(new AppConfigFacade(User).Retrieve("GSR.TimeAddLastService").Value);
        private static int defaultDeltaDuration = Convert.ToInt32(new AppConfigFacade(User).Retrieve("GSR.DefaultDeltaDuration").Value);

        private static PMKind getPMKind(int id)
        {
            if (arrPMKind.Count == 0)
            {
                CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(PMKind), "RowStatus", MatchType.Exact, 0));
                arrPMKind = new PMKindFacade(User).Retrieve(crit);
            }

            return (from PMKind p in arrPMKind where p.ID == id select p).FirstOrDefault<PMKind>();
        }

        private static PMKindDuration getPMKindDurationByPMKindID(int pMKindID)
        {
            CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(PMKindDuration), "RowStatus", MatchType.Exact, 0));
            crit.opAnd(new Criteria(typeof(PMKindDuration), "PMKindID", MatchType.Exact, pMKindID));
            arrPMKind = new PMKindFacade(User).Retrieve(crit);

            return (PMKindDuration)arrPMKind[0];
        }

        private static int getStringKM(string str)
        {
            var kmIndex = -1;
            var kmIndex1 = str.IndexOf("Km");
            var kmIndex2 = str.IndexOf("KM");

            if (kmIndex1 >= 0)
                kmIndex = kmIndex1;
            else if (kmIndex2 >= 0)
                kmIndex = kmIndex2;
            else
                return -1;

            var startSub = 0;

            for (int i = kmIndex - 1; i >= 0; i--)
            {
                if (Char.IsDigit(str[i]) || str[i] == '.' || char.IsWhiteSpace(str[i])) { }
                else
                {
                    startSub = i + 1;
                    break;
                }
            }

            string temp = str.Substring(startSub, kmIndex - startSub);
            int ret = 0;
            NumberStyles styles = NumberStyles.Integer | NumberStyles.AllowThousands;
            CultureInfo provider = CultureInfo.InvariantCulture;

            bool bb = Int32.TryParse(temp.Replace(".", ","), styles, provider, out ret);

            if (bb)
                return ret;
            else
                return -1;
        }

        private static double calculate(List<KTB.DNet.SFIntegration.Model.chassisHistoricalData> datas, int nextKM)
        {
            int divider = 1;
            if (datas.Last().ActualKM > 1000)
                divider = 1000;
        TRY:
            try
            {
                int n = datas.Count;
                double[] x = new double[n];
                double[] y = new double[n];
                double[] sqrX = new double[n];
                double[] sqrY = new double[n];
                double[] XY = new double[n];

                for (int i = 0; i < datas.Count; i++)
                {
                    x[i] = (double)datas[i].ActualKM;
                    y[i] = (double)datas[i].DeltaDays;
                    sqrX[i] = x[i] * x[i];
                    sqrY[i] = y[i] * y[i];
                    XY[i] = x[i] * y[i];
                }

                double ySum = y.Sum();
                double xSum = x.Sum();
                double sqrXSum = sqrX.Sum();
                double sqrYSum = sqrY.Sum();
                double XYSum = XY.Sum();

                double a = (ySum * sqrXSum - xSum * XYSum) /
                            (n * sqrXSum - xSum * xSum);

                double b = ((n * XYSum) - (xSum * ySum)) /
                            ((n * sqrXSum) - (xSum * xSum));

                double prediction = a + b * nextKM;

                return prediction;
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("ERROR CALCULATION");
            }
        }

        private static List<PMKind> getPMKind()
        {
            List<PMKind> listPMKind = new List<PMKind>();
            DataSet datas = new ServiceReminderFacade(User).RetrieveSp("exec sp_getPMKindByCategory @Category=1");
            listPMKind = ServiceReminderParser.ParsePMKind(datas.Tables[0]);
            datas = new ServiceReminderFacade(User).RetrieveSp("exec sp_getPMKindByCategory @Category=2");
            listPMKind.AddRange(ServiceReminderParser.ParsePMKind(datas.Tables[0]));

            return listPMKind;
        }

        private static bool validateChassis(int ID, int chassisMasterID)
        {
            ServiceReminderFacade svcReminderFacade = new ServiceReminderFacade(User);
            CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(ServiceReminder), "RowStatus", MatchType.Exact, 0));
            crit.opAnd(new Criteria(typeof(ServiceReminder), "Status", MatchType.Lesser, 5));
            crit.opAnd(new Criteria(typeof(ServiceReminder), "ChassisMaster", MatchType.Exact, chassisMasterID));
            ArrayList arrSvcRemidner = svcReminderFacade.Retrieve(crit);

            if (arrSvcRemidner.IsNotNull() && arrSvcRemidner.Count > 0)
            {
                ServiceReminder temp = svcReminderFacade.Retrieve(ID);
                temp.Status = 6;
                var res = svcReminderFacade.Update(temp);

                return false;
            }

            return true;
        }

        private static ServiceReminder copySvcReminder(ServiceReminder svc)
        {
            ServiceReminder newSvc = new ServiceReminder();
            newSvc.ActualKM = svc.ActualKM;
            if (svc.ActualServiceDealer.IsNotNull())
                newSvc.ActualServiceDealer = svc.ActualServiceDealer;
            if (svc.ActualServiceDealerBranch.IsNotNull())
                newSvc.ActualServiceDealerBranch = svc.ActualServiceDealerBranch;
            if (svc.AssistServiceIncoming.IsNotNull())
                newSvc.AssistServiceIncoming = svc.AssistServiceIncoming;
            newSvc.BookingDate = svc.BookingDate;
            newSvc.BookingTime = svc.BookingTime;
            newSvc.CaseNumber = svc.CaseNumber;
            if (svc.Category.IsNotNull())
                newSvc.Category = svc.Category;
            if (svc.ChassisMaster.IsNotNull())
                newSvc.ChassisMaster = svc.ChassisMaster;
            newSvc.ChassisNumber = svc.ChassisNumber;
            newSvc.ContactPersonName = svc.ContactPersonName;
            newSvc.ContactPersonPhoneNumber = svc.ContactPersonPhoneNumber;
            newSvc.CustomerName = svc.CustomerName;
            newSvc.CustomerPhoneNumber = svc.CustomerPhoneNumber;
            if (svc.Dealer.IsNotNull())
                newSvc.Dealer = svc.Dealer;
            if (svc.DealerBranch.IsNotNull())
                newSvc.DealerBranch = svc.DealerBranch;
            newSvc.EngineNumber = svc.EngineNumber;
            newSvc.PKTDate = svc.PKTDate;
            if (svc.PMKind.IsNotNull())
                newSvc.PMKind = svc.PMKind;
            newSvc.Remark = svc.Remark;
            newSvc.SalesforceID = svc.SalesforceID;
            newSvc.ServiceActualDate = svc.ServiceActualDate;
            newSvc.ServiceReminderDate = svc.ServiceReminderDate;
            newSvc.Status = svc.Status;
            newSvc.TransactionType = svc.TransactionType;
            if (svc.VehicleType.IsNotNull())
                newSvc.VehicleType = svc.VehicleType;
            newSvc.WONumber = svc.WONumber;


            newSvc = svc;
            return newSvc;
        }

        //---------------------------------------------------------------------------------------------------//

        private static void generate(DataTable svcReminderTabel, ref int errCounter, string procesName, int processID)
        {
            int count = 0;
            int res = 0;
            ServiceReminderFacade svcFacade = new ServiceReminderFacade(User);
            CriteriaComposite crit;

            foreach (DataRow row in svcReminderTabel.Rows)
            {
                if (row["NextDuration"] == DBNull.Value)
                    break;

                string tempChassis = row["ChassisNumber"].ToString();
                try
                {
                    if (!validateChassis((int)row["ID"], (int)row["ChassisMasterID"]))
                        continue;

                    ServiceReminder svcReminderToInsert = ServiceReminderParser.Parse(row);
                    if (svcReminderToInsert.ActualServiceDealer.IsNotNull())
                        svcReminderToInsert.Dealer = svcReminderToInsert.ActualServiceDealer;
                    string remark = row["NewRemark"].ToString();
                    svcReminderToInsert.ActualKM = 0;
                    svcReminderToInsert.ServiceActualDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                    svcReminderToInsert.ActualServiceDealer = null;
                    svcReminderToInsert.ActualServiceDealerBranch = null;

                    int maxDuration = (int)row["MaxDuration"];
                    //if ((DateTime.Today - svcReminderToInsert.PKTDate).Days > maxDuration)
                    //{
                    //    var gapDate = (DateTime.Now - svcReminderToInsert.PKTDate).TotalDays - 40;
                    //    crit = new CriteriaComposite(new Criteria(typeof(PMKindDuration), "RowStatus", MatchType.Exact, 0));
                    //    crit.opAnd(new Criteria(typeof(PMKindDuration), "Category", MatchType.Exact, svcReminderToInsert.Category.ID));
                    //    crit.opAnd(new Criteria(typeof(PMKindDuration), "Duration", MatchType.LesserOrEqual, gapDate));
                    //    SortCollection sort = new SortCollection();
                    //    sort.Add(new Sort(typeof(PMKindDuration), "Duration", Sort.SortDirection.DESC));

                    //    ArrayList arrPMKindDuration = new PMKindDurationFacade(User).Retrieve(crit, sort);

                    //    if (arrPMKindDuration.IsNull() || arrPMKindDuration.Count == 0)
                    //        svcReminderToInsert.Status = (int)EnumGlobalServiceReminder.ServiceReminderStatus.Archive;
                    //    else
                    //    {
                    //        svcReminderToInsert.PMKind = ((PMKindDuration)arrPMKindDuration[0]).PMKind;
                    //        svcReminderToInsert.Status = (int)EnumGlobalServiceReminder.ServiceReminderStatus.Finish;
                    //    }

                    //    svcReminderToInsert.ServiceReminderDate = DateTime.Now;
                    //    svcReminderToInsert.MaxFUDealerDate = svcReminderToInsert.ServiceReminderDate.AddDays(30);
                    //    res = new ServiceReminderFacade(User).Insert(svcReminderToInsert);
                    //    new ServiceReminderFacade(User).RetrieveSp("exec sp_ServiceReminder_UpdateStatus @ID=" + (int)row["ID"] + ", @Status=" + "6");
                    //    continue;
                    //}

                    var arrHistSvcReminder = svcFacade.RetrieveSp("sp_ServiceReminder_RetrieveHistorical @ChassisNumber=" + tempChassis);

                    if (arrHistSvcReminder.Tables.Count > 0)
                    {
                        var arrHistoricalData = ServiceReminderParser.ParseHistoricalSvcReminder(arrHistSvcReminder.Tables[0]);
                        int additionalDay = 0;
                        int lrAdditionalDay = 0;
                        int nextDurationByPM = (int)row["NextDuration"];
                        DateTime lastReminderDate = svcReminderToInsert.ServiceReminderDate;

                        svcReminderToInsert.Status = 1;

                        //perhihtungan regresi linear
                        if (arrHistoricalData.Count > 1)
                        {
                            additionalDay = (int)calculate(arrHistoricalData, (int)row["PMKM"]);
                            lrAdditionalDay = additionalDay;
                            int delta = Convert.ToInt32(row["DeltaDuration"]);
                            int maxLastService = (delta <= 180 ? 0 : delta) + batasMaxLastService;
                            int additionalDuration = delta <= 180 ? 0 : delta;

                            if (additionalDay > nextDurationByPM)
                                additionalDay = nextDurationByPM;

                            if (additionalDay - (int)row["LastSvcDuration"] <= maxLastService)
                                additionalDay = (int)row["LastSvcDuration"] + additionalDuration + timeAddLastService;
                        }

                        else if (arrHistoricalData.Count <= 1)
                            additionalDay = nextDurationByPM;

                        if (svcReminderToInsert.PKTDate.AddDays(additionalDay + 40) < DateTime.Today)
                        {
                            if (svcReminderToInsert.PKTDate.AddDays(maxDuration) > DateTime.Today)
                            {
                                svcReminderToInsert.ServiceReminderDate = DateTime.Now;
                                svcReminderToInsert.MaxFUDealerDate = svcReminderToInsert.ServiceReminderDate.AddDays(30);
                                svcReminderToInsert.Status = (int)EnumGlobalServiceReminder.ServiceReminderStatus.Baru;
                            }
                            else
                            {
                                svcReminderToInsert.Status = (int)EnumGlobalServiceReminder.ServiceReminderStatus.Finish;
                                svcReminderToInsert.ServiceReminderDate = DateTime.Now;
                                svcReminderToInsert.MaxFUDealerDate = svcReminderToInsert.ServiceReminderDate.AddDays(30);
                            }
                        }
                        else
                        {
                            svcReminderToInsert.ServiceReminderDate = svcReminderToInsert.PKTDate.AddDays(additionalDay);
                            svcReminderToInsert.MaxFUDealerDate = svcReminderToInsert.ServiceReminderDate.AddDays(30);
                            svcReminderToInsert.Status = (int)EnumGlobalServiceReminder.ServiceReminderStatus.Baru;
                        }

                        svcReminderToInsert.Remark = remark;
                    }
                    else
                    {
                        svcReminderToInsert.ServiceReminderDate = svcReminderToInsert.PKTDate.AddDays((int)row["NextDuration"]);
                        svcReminderToInsert.MaxFUDealerDate = svcReminderToInsert.ServiceReminderDate.AddDays(30);
                        svcReminderToInsert.Status = (int)EnumGlobalServiceReminder.ServiceReminderStatus.Baru;
                    }

                    svcFacade.RetrieveSp("exec sp_ServiceReminder_UpdateStatus @ID=" + (int)row["ID"] + ", @Status=" + "6");
                    //migrasi ke prod


                    var datas = svcFacade.RetrieveSp(string.Format("EXEC [sp_ServiceReminder_Exists] {0}, {1}, {2}, {3}",
                                    svcReminderToInsert.Dealer.ID,
                                    svcReminderToInsert.ChassisMaster.ID,
                                    svcReminderToInsert.PMKind.ID,
                                    svcReminderToInsert.TransactionType));
                            
                    if (datas.Tables.Count > 0 && datas.Tables[0].Rows.Count > 0)
                    {
                        svcReminderToInsert.ID = Convert.ToInt32(datas.Tables[0].Rows[0]["ID"]);
                        res = svcFacade.Update(svcReminderToInsert);
                        //exec SP untuk migrasi update ke Prod
                    }
                    else
                        res = svcFacade.Insert(svcReminderToInsert);
                        //exec SP untuk migrasi insert ke Prod

                    if (res < 1)
                        throw new System.ArgumentException("Error insert/update", "original");
                }
                catch (Exception ex)
                {
                    errCounter++;
                    Debug.WriteLine("ERR : " + tempChassis + " : " + ex.Message.ToString());
                }

                count++;
                //if (count == 20)
                //    break;
            }

            new ServiceReminderFacade(User).RetrieveSp("exec sp_ServiceReminder_CleanDouble");

        }

        //---------------------------------------------------------------------------------------------------//

        private static void getSvcReminderData()
        {
            List<string> listInfo = new List<string>();
            DateTime start = DateTime.Now;
            int totalData = 0;
            int totalSucess = 0;
            int totalFailed = 0;
            listInfo.Add("Process started at " + start);

            //var fs01 = new ServiceReminderFacade(User).RetrieveSp("exec sp_ServiceReminder_FS01");

            GenericPrincipal User1 = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfireCalculation"), null);
            //List<KTB.DNet.SFIntegration.Model.FSKindByKMDesc> listFSKind = new List<KTB.DNet.SFIntegration.Model.FSKindByKMDesc>();

            DataSet ds = new ServiceReminderFacade(User1).RetrieveSp("EXEC [sp_ServiceReminder_RetrieveRemindByChassis]");
            DataTable dtChassis = ds.Tables[0];

            string sqlCommand;
            DataSet svcReminderDataSet;

            foreach (DataRow row in dtChassis.Rows)
            {
                sqlCommand =
                        string.Format("EXEC [sp_ServiceReminder_RetrieveForCalculationPM] @PMKindID = {0}, @PMKindIDPrev = {1}, @ChassisNumber = '{2}'",
                            Convert.ToInt32(row["NextPMKindID"].ToString()), Convert.ToInt32(row["PrevPMKindID"].ToString()), row["ChassisNumber"].ToString());
                svcReminderDataSet = new ServiceReminderFacade(User).RetrieveSp(sqlCommand);

                if (svcReminderDataSet.Tables.Count > 0)
                {
                    var svcReminderTabel = svcReminderDataSet.Tables[0];
                    if (svcReminderTabel.Rows.Count > 0)
                    {
                        DataRow rowCurr = svcReminderTabel.Rows[0];
                        int errCounter = 0;
                        generate(svcReminderTabel, ref errCounter, rowCurr["KindType"].ToString(), Convert.ToInt32(rowCurr["KindID"].ToString()));
                        listInfo.Add(string.Format("Processing : {0}Kind ({1}); Error : {2}", rowCurr["KindType"].ToString(), rowCurr["KindID"].ToString(), errCounter.ToString()));
                        totalData += svcReminderTabel.Rows.Count;
                        totalFailed += errCounter;
                    }

                }
            }

            var followUp = new ServiceReminderFacade(User).RetrieveSp("exec sp_GenerateServiceReminderFollowUp");


            foreach (string r in listInfo)
                Debug.WriteLine(r);
            Debug.WriteLine("Total Data : " + totalData.ToString());
            Debug.WriteLine("Total Data created : " + (totalData - totalFailed).ToString());
            Debug.WriteLine("Total Data fail : " + totalFailed.ToString());
        }

        public static async Task GenerateServiceReminder()
        {
            getSvcReminderData();
        }

        //SEND SERVICE REMINDER TO SF TASK//

        private static void generateFollowUp(ServiceReminder svcReminder)
        {
            ServiceReminderFollowUp svcReminderFU = new ServiceReminderFollowUp();
            svcReminderFU.ServiceReminder = svcReminder;
            svcReminderFU.FollowUpAction = "Send to Salesforce";
            svcReminderFU.FollowUpDate = DateTime.Now;
            svcReminderFU.FollowUpStatus = 4;

            var res = new ServiceReminderFollowUpFacade(User).Insert(svcReminderFU);
            //migrasi followup
        }

        public static async Task SendSvcReminderToSF()
        {
            var svcReminderDataSet = new ServiceReminderFacade(User).RetrieveSp("sp_GetServiceReminder_ForSalesForce");

            if (svcReminderDataSet.Tables.Count > 0)
            {
                var svcReminderTable = svcReminderDataSet.Tables[0];
                string vReturn = "";
                string msg = "";
                string logCategory = "K;INSERTSERVICEREMINDERSF\n";
                string strJson = "";
                int count = 0;
                int result = 0;
                int insertCounter = 0;
                int updateCounter = 0;
                var data = new KTB.DNet.SFIntegration.Model.ServiceReminder();
                var svcReminderFacade = new ServiceReminderFacade(User);
                var tempSvcReminder = new ServiceReminder();

                foreach (DataRow row in svcReminderTable.Rows)
                {
                    try
                    {
                        data = ServiceReminderParser.ParseToSFObj(row, User);
                        strJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                        MainParser.SendData(User, String.Concat("services/apexrest/", KTB.DNet.SFIntegration.Model.ServiceReminder.SObjectTypeName), data).Wait();
                        vReturn = MainParser.IsSuccess.ToString();
                        msg = MainParser.Message.ToString();

                        if (MainParser.IsSuccess && data.Service_Status__c == "New" && !string.IsNullOrWhiteSpace(msg))
                        {
                            tempSvcReminder = svcReminderFacade.Retrieve((int)row["ID"]);
                            tempSvcReminder.SalesforceID = msg;
                            tempSvcReminder.Status = 4;
                            result = svcReminderFacade.Update(tempSvcReminder);
                            generateFollowUp(tempSvcReminder);
                            insertCounter++;
                        }
                        else if (MainParser.IsSuccess && data.Service_Status__c != "New" && !string.IsNullOrWhiteSpace(msg))
                            updateCounter++;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    CreateLog(vReturn, msg, logCategory, strJson);
                    count++;
                }

                svcReminderFacade.RetrieveSp("sp_Insert_ServiceReminderLog @Remark='Send Salesforce', @InsD=" + insertCounter + ", @UpD=" + updateCounter);
            }


            return;
        }

        static void CreateLog(string vReturn, string msg, string logCategory, string strJson)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            WsLog wslog = new WsLog();
            wslog.Source = "Internal";
            wslog.Status = vReturn.ToString();
            wslog.Message = msg;
            wslog.Body = String.Concat(logCategory, strJson);
            wslog.RowStatus = 0;
            wslog.CreatedBy = "WebService";

            WsLogFacade wslogfacade = new WsLogFacade(User);
            wslogfacade.Insert(wslog);

        }

        private static DateTime getPKT(string chassisNumber, System.Security.Principal.IPrincipal _user)
        {
            var PKTDate = new DateTime();
            var pktDataSet = new ServiceReminderFacade(_user).RetrieveSp("exec sp_ServiceReminder_Get_PKT @ChassisNumber=" + chassisNumber);
            if (pktDataSet.Tables.Count > 0)
            {
                var pktTbl = pktDataSet.Tables[0];
                if (pktTbl.Rows[0]["PKTDate"] != DBNull.Value)
                    PKTDate = Convert.ToDateTime(pktTbl.Rows[0]["PKTDate"]);
            }

            return PKTDate;
        }

        //REPAIR DATA//
        public static void repairData()
        {
            GenericPrincipal UserRepair = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire4_r"), null);
            ServiceReminderFacade srFacade = new ServiceReminderFacade(User);
            ServiceReminderFacade srRepairFacade = new ServiceReminderFacade(UserRepair);
            ServiceReminderFollowUpFacade srFUFacade = new ServiceReminderFollowUpFacade(UserRepair);
            DataSet ds = srFacade.RetrieveSp("EXEC [sp_ServiceReminder_RetrieveRepairData]");
            DataTable dtChassis = ds.Tables[0];

            foreach (DataRow row in dtChassis.Rows)
            {
                ServiceReminder svcReminderToRepair = srFacade.Retrieve(Convert.ToInt32(row["ID"]));
                DataSet arrHistSvcReminder = srFacade.RetrieveSp("sp_ServiceReminder_RetrieveHistorical @ChassisNumber=" + svcReminderToRepair.ChassisNumber);
                List<Model.chassisHistoricalData> arrHistoricalData = new List<Model.chassisHistoricalData>();
                
                if (arrHistSvcReminder.Tables.Count > 0) {
                    arrHistoricalData = ServiceReminderParser.ParseHistoricalSvcReminder(arrHistSvcReminder.Tables[0]);
                }
                
                string strSql = "sp_ServiceReminder_RetrieveForReCalculation @id=" + svcReminderToRepair.ID.ToString();
                DataSet rawSvcReminder = srFacade.RetrieveSp(strSql);
                if (rawSvcReminder.Tables.Count > 0)
                {
                    DataTable rawTable = rawSvcReminder.Tables[0];
                    if (rawTable.Rows.Count > 0)
                    {
                        DataRow svcReminderDataRow = rawTable.Rows[0];
                        //if (!validateReClculationRetrieval(svcReminderDataRow))
                        //{
                        //    var ano = new ServiceReminderAnomaly()
                        //    {
                        //        ChassisNumber = svcReminderToRepair.ChassisNumber,
                        //        ChassisMasterID = svcReminderToRepair.ChassisMaster.ID,
                        //        PMKindID = svcReminderToRepair.PMKind.ID,
                        //        PredictionDate = svcReminderToRepair.ServiceReminderDate
                        //    };

                        //    var res = new ServiceReminderAnomalyFacade(UserRepair).Insert(ano);
                        //    continue;
                        //}

                        int additionalDay = 0;
                        int lrAdditionalDay = 0;
                        int nextDurationByPM = (int)svcReminderDataRow["NextDuration"];
                        DateTime lastReminderDate = svcReminderToRepair.ServiceReminderDate;
                        byte lastStatus = svcReminderToRepair.Status;

                        //perhihtungan regresi linear
                        if (arrHistoricalData.Count > 1)
                        {
                            additionalDay = (int)calculate(arrHistoricalData, (int)svcReminderDataRow["PMKM"]);
                            lrAdditionalDay = additionalDay;
                            int delta = Convert.ToInt32(svcReminderDataRow["DeltaDuration"]);
                            int maxLastService = (delta <= 180 ? 0 : delta) + batasMaxLastService;
                            int additionalDuration = delta <= 180 ? 0 : delta;

                            if (additionalDay > nextDurationByPM)
                                additionalDay = nextDurationByPM;

                            if (additionalDay - (int)svcReminderDataRow["LastSvcDuration"] <= maxLastService)
                                additionalDay = (int)svcReminderDataRow["LastSvcDuration"] + additionalDuration + timeAddLastService;
                        }
                        else
                        {
                            svcReminderToRepair.ServiceReminderDate = Convert.ToDateTime(svcReminderDataRow["MaxFUDealerDate"]).AddDays(defaultDeltaDuration);
                            additionalDay = svcReminderToRepair.ServiceReminderDate.Subtract(svcReminderToRepair.PKTDate).Days;
                        }

                        if (svcReminderToRepair.PKTDate.AddDays(additionalDay + 40) < DateTime.Today)
                        {
                            if (svcReminderToRepair.PKTDate.AddDays((int)svcReminderDataRow["MaxDuration"]) > DateTime.Today)
                                svcReminderToRepair.ServiceReminderDate = DateTime.Now;
                            else
                            {
                                svcReminderToRepair.ServiceReminderDate = DateTime.Now;
                                svcReminderToRepair.Status = (int)EnumGlobalServiceReminder.ServiceReminderStatus.Finish;
                            }
                        }
                        //else
                        //    svcReminderToRepair.ServiceReminderDate = svcReminderToRepair.PKTDate.AddDays(additionalDay);

                        if (svcReminderToRepair.ServiceReminderDate < lastReminderDate)
                            svcReminderToRepair.ServiceReminderDate = lastReminderDate;

                        ServiceReminder statusCheck = srRepairFacade.Retrieve(svcReminderToRepair.ID);
                        if (statusCheck.Status == (int)EnumGlobalServiceReminder.ServiceReminderStatus.Baru
                            || statusCheck.Status == (int)EnumGlobalServiceReminder.ServiceReminderStatus.InProgress
                            || statusCheck.Status == (int)EnumGlobalServiceReminder.ServiceReminderStatus.CSMMKSI)
                        {
                            if (statusCheck.Status == (int)EnumGlobalServiceReminder.ServiceReminderStatus.CSMMKSI 
                                && svcReminderToRepair.ServiceReminderDate > lastReminderDate)
                            {
                                svcReminderToRepair.Status = (int)EnumGlobalServiceReminder.ServiceReminderStatus.Complete;
                                ServiceReminderFollowUp objFU = new ServiceReminderFollowUp()
                                {
                                    ServiceReminder = svcReminderToRepair,
                                    FollowUpStatus = (int)EnumGlobalServiceReminder.ServiceReminderFollowUpStatus.Complete,
                                    FollowUpAction = "",
                                    FollowUpDate = DateTime.Now
                                };
                                int fuRes = srFUFacade.Insert(objFU);
                            }

                            svcReminderToRepair.MaxFUDealerDate = svcReminderToRepair.ServiceReminderDate.AddDays(30);
                            int res = srRepairFacade.Update(svcReminderToRepair);
                        }
                    }

                }
            }
        }

        private static bool validateReClculationRetrieval(DataRow dataRow)
        {
            bool result = false;

            result = (dataRow["ID"] != DBNull.Value) ? true : false;
            result = (dataRow["PMKM"] != DBNull.Value) ? true : false;
            result = (dataRow["PMKindIDNext"] != DBNull.Value) ? true : false;
            result = (dataRow["NextDuration"] != DBNull.Value) ? true : false;
            result = (dataRow["MaxDuration"] != DBNull.Value) ? true : false;
            result = (dataRow["LastSvcDuration"] != DBNull.Value) ? true : false;

            return result;
        }
    }
}
