using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.BusinessFacade.PO;
using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain.Search;
using System.Net;
using System.Net.Http;
using KTB.DNET.BusinessFacade.SparePart;
using System.Net.Mail;
using System.Web.Configuration;
using Microsoft.SharePoint.Client;
using System.Security;
using System.IO;
using OfficeOpenXml;
using KTB.DNet.BusinessFacade.SAP;
using OfficeOpenXml.DataValidation;
using KTB.DNET.BusinessFacade;
using KTB.DNet.BusinessFacade.FinishUnit;
using System.Collections;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class LeadCustomerLogic
    {

        private static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        public static void WriteLeadSource()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string username = appConfigFacade.Retrieve("BabitCognitoSharePoint.UserName").Value;
            string password = appConfigFacade.Retrieve("BabitCognitoSharePoint.Password").Value;
            string domain = appConfigFacade.Retrieve("BabitCognitoSharePoint.DomainSite").Value;
            string weburl = appConfigFacade.Retrieve("BabitCognitoSharePoint.Domain").Value;
            string foldersite = appConfigFacade.Retrieve("LeadQXCognitoSharePoint.FolderSite").Value;
            //string foldersite = "DataLeadQX_Dev";

            List<Microsoft.SharePoint.Client.File> listFileOpen = new List<Microsoft.SharePoint.Client.File>();

            string ProxyAddress = WebConfigurationManager.AppSettings["ProxyAddress"];
            string ProxyPort = WebConfigurationManager.AppSettings["ProxyPort"];
            System.Net.WebProxy wp = new System.Net.WebProxy();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            if (!string.IsNullOrEmpty(ProxyAddress))
            {
                wp = new System.Net.WebProxy(ProxyAddress, Convert.ToInt32(ProxyPort));
            }


            using (ClientContext clientContext = new ClientContext(domain))
            {
                if (!string.IsNullOrEmpty(ProxyAddress))
                {
                    clientContext.ExecutingWebRequest += (s, e) =>
                    {
                        e.WebRequestExecutor.WebRequest.Proxy = wp;
                    };
                }
                SecureString passWord = new SecureString();

                foreach (char c in password.ToCharArray()) passWord.AppendChar(c);

                clientContext.Credentials = new SharePointOnlineCredentials(username, passWord);
                List list = clientContext.Web.Lists.GetByTitle("Documents");
                Folder fold = list.RootFolder;
                FolderCollection foldColl = list.RootFolder.Folders;
                List<string> strlistfile = new List<string>();
                FileCollection filecoll = list.RootFolder.Files;
                clientContext.Load(fold);
                clientContext.Load(list);
                clientContext.Load(list.RootFolder);
                clientContext.Load(list.RootFolder.Folders);
                clientContext.Load(list.RootFolder.Files);
                clientContext.ExecuteQuery();
                try
                {
                    foreach (Folder fol in foldColl)
                    {
                        if (fol.Name.ToLower() == foldersite.ToLower())
                        {
                            clientContext.Load(fol.Files);
                            clientContext.ExecuteQuery();
                            var fc = fol.Files.ToList();
                            SAPCustomerFacade func = new SAPCustomerFacade(User);

                            DataSet ds = func.GetLeadCustomerQXWrite();
                            List<DataTable> listDT = ds.Tables.Cast<DataTable>().ToList();
                            DataTable dt = listDT[0];
                            foreach (DataRow dRow in dt.Rows)
                            {
                                string dealerCode = dRow["Dealer_Code_Locked"].ToString();
                                int SapCustomerMappingID = Convert.ToInt32(dRow["ID"]);

                                Microsoft.SharePoint.Client.File fn = fc.FirstOrDefault(
                                    x =>
                                        x.Name.StartsWith("QX" + dealerCode) &&
                                        (
                                            x.Name.IndexOf(".xls") > -1 ||
                                            x.Name.IndexOf(".xlsx") > -1
                                        ));
                                if (fn != null)
                                {
                                    if (listFileOpen.Where(x => x.Name == fn.Name).Count() == 0)
                                    {
                                        listFileOpen.Add(fn);
                                    }

                                    var fileRef = fn.ServerRelativeUrl;
                                    var fileInfo = Microsoft.SharePoint.Client.File.OpenBinaryDirect(clientContext, fileRef);
                                    using (var memory = new MemoryStream())
                                    {
                                        byte[] buffer = new byte[1024 * 64];
                                        int nread = 0;
                                        while ((nread = fileInfo.Stream.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            memory.Write(buffer, 0, nread);
                                        }
                                        memory.Seek(0, SeekOrigin.Begin);

                                        using (ExcelPackage exc = new ExcelPackage(memory))
                                        {
                                            ExcelWorksheet sheet = exc.Workbook.Worksheets.First();
                                            int countRows = sheet.Dimension.End.Row;
                                            int countColumn = sheet.Dimension.End.Column;
                                            sheet.Protection.IsProtected = true;
                                            int rowsInsert = countRows + 1;

                                            int column = 1;
                                            sheet.Cells[rowsInsert, column].Value = (rowsInsert - 3).ToString();
                                            int ddlColumns = 1;
                                            for (int i = column; i < dt.Columns.Count; i++)
                                            {
                                                sheet.Cells[rowsInsert, column + 1].Style.Locked = false;
                                                if (dt.Columns[column].ColumnName.StartsWith("DDL"))
                                                {
                                                    try
                                                    {
                                                        string address = String.Format("{0}{1}:{0}{1}", GetExcelColumnName(column + 1), rowsInsert.ToString());
                                                        var val = sheet.DataValidations.AddListValidation(address);
                                                        foreach (DataRow rItem in listDT[ddlColumns].Rows)
                                                        {
                                                            val.Formula.Values.Add(rItem[0].ToString());
                                                        }
                                                    }
                                                    catch { }
                                                    sheet.Cells[rowsInsert, column + 1].Value = dRow[i].ToString();
                                                    ddlColumns += 1;
                                                }
                                                else
                                                {

                                                    sheet.Cells[rowsInsert, column + 1].Value = dRow[i].ToString();
                                                }

                                                if (dt.Columns[column].ColumnName.EndsWith("Locked"))
                                                {
                                                    sheet.Cells[rowsInsert, column + 1].Style.Locked = true;
                                                }
                                                else
                                                {
                                                    sheet.Cells[rowsInsert, column + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                    sheet.Cells[rowsInsert, column + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Turquoise);
                                                }

                                                column += 1;
                                            }

                                            var filestream = new MemoryStream(exc.GetAsByteArray(), true);
                                            if (clientContext.HasPendingRequest)
                                            {
                                                clientContext.ExecuteQuery();
                                            }
                                            try
                                            {
                                                Microsoft.SharePoint.Client.File.SaveBinaryDirect(clientContext, fileRef, filestream, true);
                                                SAPCustomerMappingFacade FuncSCM = new SAPCustomerMappingFacade(User);
                                                SAPCustomerMapping objSCM = FuncSCM.Retrieve(SapCustomerMappingID);
                                                int result = FuncSCM.Update(objSCM);

                                            }
                                            catch (Exception ex)
                                            {
                                                string err = ex.Message;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                }
                finally
                {
                    foreach (var fnItem in listFileOpen)
                    {
                        fnItem.CheckOut();
                    }
                }
            }
        }

        public static void WriteLeadSourcebyData(string filepath, DataRow[] arrDr, DataSet ds)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            FileInfo fi = new FileInfo(filepath);
            List<DataTable> listDT = ds.Tables.Cast<DataTable>().ToList();
            DataTable dt = listDT[0];
            using (ExcelPackage exc = new ExcelPackage(fi))
            {
                ExcelWorksheet sheet = exc.Workbook.Worksheets.First();
                int countRows = sheet.Dimension.End.Row;
                int countColumn = sheet.Dimension.End.Column;
                sheet.Protection.IsProtected = true;
                int rowsInsert = countRows + 1;

                
                foreach (DataRow dRow in arrDr)
                {
                    int column = 1;
                    sheet.Cells[rowsInsert, column].Value = (rowsInsert - 3).ToString();
                    int ddlColumns = 1;
                    int SapCustomerMappingID = Convert.ToInt32(dRow["ID"]);
                    for (int i = column; i < dt.Columns.Count; i++)
                    {
                        sheet.Cells[rowsInsert, column + 1].Style.Locked = false;
                        if (dt.Columns[column].ColumnName.StartsWith("DDL"))
                        {
                            try
                            {
                                string address = String.Format("{0}{1}:{0}{1}", GetExcelColumnName(column + 1), rowsInsert.ToString());
                                var val = sheet.DataValidations.AddListValidation(address);
                                foreach (DataRow rItem in listDT[ddlColumns].Rows)
                                {
                                    val.Formula.Values.Add(rItem[0].ToString());
                                }
                            }
                            catch { }
                            sheet.Cells[rowsInsert, column + 1].Value = dRow[i].ToString();
                            ddlColumns += 1;
                        }
                        else
                        {

                            sheet.Cells[rowsInsert, column + 1].Value = dRow[i].ToString();
                        }

                        if (dt.Columns[column].ColumnName.EndsWith("Locked"))
                        {
                            sheet.Cells[rowsInsert, column + 1].Style.Locked = true;
                        }
                        else
                        {
                            sheet.Cells[rowsInsert, column + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            sheet.Cells[rowsInsert, column + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Turquoise);
                        }

                        column += 1;
                    }
                    try
                    {
                        SAPCustomerMappingFacade FuncSCM = new SAPCustomerMappingFacade(User);
                        SAPCustomerMapping objSCM = FuncSCM.Retrieve(SapCustomerMappingID);
                        int result = FuncSCM.Update(objSCM);

                    }
                    catch (Exception ex)
                    {
                        string err = ex.Message;
                    }
                    rowsInsert = rowsInsert + 1;
                }
               

                exc.Save();
            }

        }

        public static void Autofitcolumn(string filepath)
        {
             FileInfo fi = new FileInfo(filepath);
            using (ExcelPackage exc = new ExcelPackage(fi))
            {
                ExcelWorksheet sheet = exc.Workbook.Worksheets.First();
                int countColumn = sheet.Dimension.End.Column;
                sheet.Protection.IsProtected = false;
                for (int i = 4; i < countColumn; i++)
                {
                    sheet.Column(i).AutoFit();
                }
                sheet.Protection.IsProtected = true;
                exc.Save();
            }
        }

        public static void Write()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string username = appConfigFacade.Retrieve("BabitCognitoSharePoint.UserName").Value;
            string password = appConfigFacade.Retrieve("BabitCognitoSharePoint.Password").Value;
            string domain = appConfigFacade.Retrieve("BabitCognitoSharePoint.DomainSite").Value;
            string weburl = appConfigFacade.Retrieve("BabitCognitoSharePoint.Domain").Value;
            string foldersite = appConfigFacade.Retrieve("LeadCognitoSharePoint.FolderSite").Value;

            List<Microsoft.SharePoint.Client.File> listFileOpen = new List<Microsoft.SharePoint.Client.File>();

            string ProxyAddress = WebConfigurationManager.AppSettings["ProxyAddress"];
            string ProxyPort = WebConfigurationManager.AppSettings["ProxyPort"];
            System.Net.WebProxy wp = new System.Net.WebProxy();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            if (!string.IsNullOrEmpty(ProxyAddress))
            {
                wp = new System.Net.WebProxy(ProxyAddress, Convert.ToInt32(ProxyPort));
            }


            using (ClientContext clientContext = new ClientContext(domain))
            {
                if (!string.IsNullOrEmpty(ProxyAddress))
                {
                    clientContext.ExecutingWebRequest += (s, e) =>
                    {
                        e.WebRequestExecutor.WebRequest.Proxy = wp;
                    };
                }
                SecureString passWord = new SecureString();

                foreach (char c in password.ToCharArray()) passWord.AppendChar(c);

                clientContext.Credentials = new SharePointOnlineCredentials(username, passWord);
                List list = clientContext.Web.Lists.GetByTitle("Documents");
                Folder fold = list.RootFolder;
                FolderCollection foldColl = list.RootFolder.Folders;
                List<string> strlistfile = new List<string>();
                FileCollection filecoll = list.RootFolder.Files;
                clientContext.Load(fold);
                clientContext.Load(list);
                clientContext.Load(list.RootFolder);
                clientContext.Load(list.RootFolder.Folders);
                clientContext.Load(list.RootFolder.Files);
                clientContext.ExecuteQuery();
                try
                {
                    foreach (Folder fol in foldColl)
                    {
                        if (fol.Name.ToLower() == foldersite)
                        {
                            clientContext.Load(fol.Files);
                            clientContext.ExecuteQuery();
                            var fc = fol.Files.ToList();
                            SAPCustomerFacade func = new SAPCustomerFacade(User);

                            DataSet ds = func.GetLeadCustomerWrite();
                            List<DataTable> listDT = ds.Tables.Cast<DataTable>().ToList();
                            DataTable dt = listDT[0];
                            foreach (DataRow dRow in dt.Rows)
                            {
                                string dealerCode = dRow["Dealer_Code_Locked"].ToString();
                                int SapCustomerMappingID = Convert.ToInt32(dRow["ID"]);

                                Microsoft.SharePoint.Client.File fn = fc.FirstOrDefault(
                                    x =>
                                        x.Name.StartsWith(dealerCode) &&
                                        (
                                            x.Name.IndexOf(".xls") > -1 ||
                                            x.Name.IndexOf(".xlsx") > -1
                                        ));
                                if (fn != null)
                                {
                                    if (listFileOpen.Where(x => x.Name == fn.Name).Count() == 0)
                                    {
                                        listFileOpen.Add(fn);
                                    }

                                    var fileRef = fn.ServerRelativeUrl;
                                    var fileInfo = Microsoft.SharePoint.Client.File.OpenBinaryDirect(clientContext, fileRef);
                                    using (var memory = new MemoryStream())
                                    {
                                        byte[] buffer = new byte[1024 * 64];
                                        int nread = 0;
                                        while ((nread = fileInfo.Stream.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            memory.Write(buffer, 0, nread);
                                        }
                                        memory.Seek(0, SeekOrigin.Begin);

                                        using (ExcelPackage exc = new ExcelPackage(memory))
                                        {
                                            ExcelWorksheet sheet = exc.Workbook.Worksheets.First();
                                            int countRows = sheet.Dimension.End.Row;
                                            int countColumn = sheet.Dimension.End.Column;
                                            sheet.Protection.IsProtected = true;
                                            int rowsInsert = countRows + 1;

                                            int column = 1;
                                            sheet.Cells[rowsInsert, column].Value = (rowsInsert - 1).ToString();
                                            int ddlColumns = 1;
                                            for (int i = column; i < dt.Columns.Count; i++)
                                            {
                                                sheet.Cells[rowsInsert, column + 1].Style.Locked = false;
                                                if (dt.Columns[column].ColumnName.StartsWith("DDL"))
                                                {
                                                    try
                                                    {
                                                        string address = String.Format("{0}{1}:{0}{1}", GetExcelColumnName(column + 1), rowsInsert.ToString());
                                                        var val = sheet.DataValidations.AddListValidation(address);
                                                        foreach (DataRow rItem in listDT[ddlColumns].Rows)
                                                        {
                                                            val.Formula.Values.Add(rItem[0].ToString());
                                                        }
                                                    }
                                                    catch { }
                                                    sheet.Cells[rowsInsert, column + 1].Value = dRow[i].ToString();
                                                    ddlColumns += 1;
                                                }
                                                else
                                                {

                                                    sheet.Cells[rowsInsert, column + 1].Value = dRow[i].ToString();
                                                }

                                                if (dt.Columns[column].ColumnName.EndsWith("Locked"))
                                                {
                                                    sheet.Cells[rowsInsert, column + 1].Style.Locked = true;
                                                }
                                                else
                                                {
                                                    sheet.Cells[rowsInsert, column + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                    sheet.Cells[rowsInsert, column + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Turquoise);
                                                }

                                                column += 1;
                                            }

                                            var filestream = new MemoryStream(exc.GetAsByteArray(), true);
                                            if (clientContext.HasPendingRequest)
                                            {
                                                clientContext.ExecuteQuery();
                                            }
                                            try
                                            {
                                                Microsoft.SharePoint.Client.File.SaveBinaryDirect(clientContext, fileRef, filestream, true);
                                                SAPCustomerMappingFacade FuncSCM = new SAPCustomerMappingFacade(User);
                                                SAPCustomerMapping objSCM = FuncSCM.Retrieve(SapCustomerMappingID);
                                                int result = FuncSCM.Update(objSCM);

                                            }
                                            catch (Exception ex)
                                            {
                                                string err = ex.Message;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                }
                finally
                {
                    foreach (var fnItem in listFileOpen)
                    {
                        fnItem.CheckOut();
                    }
                }
            }
        }

        public static void Read()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string username = appConfigFacade.Retrieve("BabitCognitoSharePoint.UserName").Value;
            string password = appConfigFacade.Retrieve("BabitCognitoSharePoint.Password").Value;
            string domain = appConfigFacade.Retrieve("BabitCognitoSharePoint.DomainSite").Value;
            string weburl = appConfigFacade.Retrieve("BabitCognitoSharePoint.Domain").Value;
            string foldersite = appConfigFacade.Retrieve("LeadCognitoSharePoint.FolderSite").Value;
            List<Microsoft.SharePoint.Client.File> listFileOpen = new List<Microsoft.SharePoint.Client.File>();

            string ProxyAddress = WebConfigurationManager.AppSettings["ProxyAddress"];
            string ProxyPort = WebConfigurationManager.AppSettings["ProxyPort"];
            System.Net.WebProxy wp = new System.Net.WebProxy();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            if (!string.IsNullOrEmpty(ProxyAddress))
            {
                wp = new System.Net.WebProxy(ProxyAddress, Convert.ToInt32(ProxyPort));
            }

            using (ClientContext clientContext = new ClientContext(domain))
            {
                if (!string.IsNullOrEmpty(ProxyAddress))
                {
                    clientContext.ExecutingWebRequest += (s, e) =>
                    {
                        e.WebRequestExecutor.WebRequest.Proxy = wp;
                    };
                }
                SecureString passWord = new SecureString();

                foreach (char c in password.ToCharArray()) passWord.AppendChar(c);

                clientContext.Credentials = new SharePointOnlineCredentials(username, passWord);
                List list = clientContext.Web.Lists.GetByTitle("Documents");
                Folder fold = list.RootFolder;
                FolderCollection foldColl = list.RootFolder.Folders;
                List<string> strlistfile = new List<string>();
                FileCollection filecoll = list.RootFolder.Files;
                clientContext.Load(fold);
                clientContext.Load(list);
                clientContext.Load(list.RootFolder);
                clientContext.Load(list.RootFolder.Folders);
                clientContext.Load(list.RootFolder.Files);
                clientContext.ExecuteQuery();

                try
                {
                    foreach (Folder fol in foldColl)
                    {
                        if (fol.Name.ToLower() == foldersite)
                        {
                            clientContext.Load(fol.Files);
                            clientContext.ExecuteQuery();
                            var fc = fol.Files.Where(x =>
                                    (
                                        x.Name.IndexOf(".xls") > -1 ||
                                        x.Name.IndexOf(".xlsx") > -1
                                    )
                                ).ToList();
                            SAPCustomerFacade func = new SAPCustomerFacade(User);
                            DealerFacade funcDealer = new DealerFacade(User);
                            StandardCodeFacade funcSD = new StandardCodeFacade(User);
                            VechileTypeFacade funcVT = new VechileTypeFacade(User);

                            List<StandardCode> listFollowUp = funcSD.RetrieveByCategory("LeadStatus").Cast<StandardCode>().ToList();
                            List<StandardCode> listLeadStatus = funcSD.RetrieveByCategory("EnumSAPCustomerStatus.SAPCustomerStatus.Excel").Cast<StandardCode>().ToList();

                            foreach (Microsoft.SharePoint.Client.File fn in fc)
                            {
                                if (fn != null)
                                {
                                    if (listFileOpen.Where(x => x.Name == fn.Name).Count() == 0)
                                    {
                                        listFileOpen.Add(fn);
                                    }
                                    string dealerCode = fn.Name.Split('.')[0].Trim();
                                    Dealer objDealer = funcDealer.Retrieve(dealerCode);
                                    if (objDealer.ID == 0) { continue; }
                                    try
                                    {
                                        var fileRef = fn.ServerRelativeUrl;
                                        var fileInfo = Microsoft.SharePoint.Client.File.OpenBinaryDirect(clientContext, fileRef);
                                        using (var memory = new MemoryStream())
                                        {
                                            byte[] buffer = new byte[1024 * 64];
                                            int nread = 0;
                                            while ((nread = fileInfo.Stream.Read(buffer, 0, buffer.Length)) > 0)
                                            {
                                                memory.Write(buffer, 0, nread);
                                            }
                                            memory.Seek(0, SeekOrigin.Begin);

                                            using (ExcelPackage exc = new ExcelPackage(memory))
                                            {
                                                ExcelWorksheet sheet = exc.Workbook.Worksheets.First();
                                                int countRows = sheet.Dimension.End.Row;
                                                int countColumn = sheet.Dimension.End.Column;

                                                if (countRows == 1) { continue; }
                                                for (int i = 2; i <= countRows; i++)
                                                {
                                                    string action = sheet.Cells[i, (countColumn - 1)].Value.ToString();
                                                    int sapCustomerID = Convert.ToInt32(sheet.Cells[i, ExcelColumn.SapCustomerID].Value);
                                                    string FollowUpStatus = sheet.Cells[i, ExcelColumn.FollowUp_Status].Value.ToString();
                                                    string LeadStatus = sheet.Cells[i, ExcelColumn.Lead_Status].Value.ToString();
                                                    string SpkNumber = sheet.Cells[i, ExcelColumn.SPKNumber].Value.ToString();
                                                    string Catatan = sheet.Cells[i, ExcelColumn.Note].Value.ToString();
                                                    string message = string.Empty;

                                                    SAPCustomer objCust = func.Retrieve(sapCustomerID);
                                                    //if (!action.Equals("V"))
                                                    //{
                                                    //    bool isMatch = true;
                                                    //    if (objCust.Status != Convert.ToByte(listLeadStatus.FirstOrDefault(x => x.ValueDesc == LeadStatus).ValueId))
                                                    //    {
                                                    //        string statusDNet = listLeadStatus.FirstOrDefault(x => x.ValueId == objCust.Status).ValueDesc;
                                                    //        message += "Lead Status di D-Net : "+statusDNet+", ";
                                                    //        isMatch = false;
                                                    //    }

                                                    //    if (!string.IsNullOrEmpty(FollowUpStatus) && objCust.LeadStatus != 0)
                                                    //    {
                                                    //        if (objCust.LeadStatus != Convert.ToByte(listFollowUp.FirstOrDefault(x => x.ValueDesc == FollowUpStatus).ValueId))
                                                    //        {
                                                    //            string followupDNet = listFollowUp.FirstOrDefault(x => x.ValueId == objCust.LeadStatus).ValueDesc;
                                                    //            message += "Status FollowUp di D-Net : " + followupDNet+", ";
                                                    //            isMatch = false;
                                                    //        }
                                                    //    }


                                                    //    if (!string.IsNullOrEmpty(message))
                                                    //    {
                                                    //        sheet.Cells[i, countColumn - 1].Value = "-";
                                                    //        sheet.Cells[i, countColumn].Style.Locked = false;
                                                    //        sheet.Cells[i, countColumn].Value = message.Remove(message.Length - 2, 2) ;
                                                    //        sheet.Cells[i, countColumn].Style.Locked = true;
                                                    //        continue;
                                                    //    }
                                                    //}
                                                    //else
                                                    if (action.Equals("V"))
                                                    {
                                                        if (objCust.Status == Convert.ToByte(4))
                                                        {
                                                            sheet.Cells[i, countColumn - 1].Value = "-";
                                                            sheet.Cells[i, countColumn].Style.Locked = false;
                                                            sheet.Cells[i, countColumn].Value = "Sudah Deal SPK.";
                                                            sheet.Cells[i, countColumn].Style.Locked = true;
                                                            continue;
                                                        }

                                                        if (string.IsNullOrEmpty(FollowUpStatus))
                                                        {
                                                            message += "Status FollowUp harus diisi, ";
                                                        }
                                                        if (string.IsNullOrEmpty(LeadStatus))
                                                        {
                                                            message += "Lead Status harus diisi, ";
                                                        }

                                                        if (!string.IsNullOrEmpty(message))
                                                        {
                                                            sheet.Cells[i, countColumn - 1].Value = "-";
                                                            sheet.Cells[i, countColumn].Style.Locked = false;
                                                            sheet.Cells[i, countColumn].Value = message.Remove(message.Length - 2, 2);
                                                            sheet.Cells[i, countColumn].Style.Locked = true;

                                                            continue;
                                                        }

                                                        if (FollowUpStatus.Equals("Cancelled") || LeadStatus.Equals("Cancelled"))
                                                        {
                                                            objCust.Status = Convert.ToByte(listLeadStatus.FirstOrDefault(x => x.ValueDesc == LeadStatus).ValueId);
                                                            objCust.LeadStatus = Convert.ToByte(listFollowUp.FirstOrDefault(x => x.ValueDesc == FollowUpStatus).ValueId);
                                                            objCust.Note = Catatan;
                                                            int result = func.Update(objCust);
                                                            message = "Success, ";
                                                        }
                                                        else
                                                        {
                                                            objCust.Status = Convert.ToByte(listLeadStatus.FirstOrDefault(x => x.ValueDesc == LeadStatus).ValueId);
                                                            objCust.LeadStatus = Convert.ToByte(listFollowUp.FirstOrDefault(x => x.ValueDesc == FollowUpStatus).ValueId);
                                                            objCust.Note = Catatan;
                                                            int result = func.Update(objCust);
                                                            message = "Success, ";
                                                        }
                                                        sheet.Cells[i, countColumn - 1].Value = "-";
                                                        sheet.Cells[i, countColumn].Style.Locked = false;
                                                        sheet.Cells[i, countColumn].Value = message.Remove(message.Length - 2, 2);
                                                        sheet.Cells[i, countColumn].Style.Locked = true;
                                                        continue;

                                                    }
                                                }

                                                var filestream = new MemoryStream(exc.GetAsByteArray(), true);
                                                if (clientContext.HasPendingRequest)
                                                {
                                                    clientContext.ExecuteQuery();
                                                }
                                                try
                                                {
                                                    Microsoft.SharePoint.Client.File.SaveBinaryDirect(clientContext, fileRef, filestream, true);
                                                }
                                                catch (Exception ex)
                                                {
                                                    string err = ex.Message;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception exc)
                                    {
                                        string g = exc.Message;
                                        continue;
                                    }
                                }
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    string error = e.Message;
                }
                finally
                {
                    foreach (var iFile in listFileOpen)
                    {
                        iFile.CheckOut();
                    }
                }
            }
        }
    }

    public static class ExcelColumn
    {
        public static int SapCustomerID = 5;
        public static int FollowUp_Status = 10;
        public static int Lead_Status = 11;
        public static int SPKNumber = 12;
        public static int Note = 13;
        public static int Action = 14;
    }
}
