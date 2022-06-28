#region NameSpace
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
using KTB.DNet.BusinessFacade;
using Newtonsoft.Json.Linq;
using KTB.DNet.DataMapper.Framework;

#endregion

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class PDIPKTService
    {

        public static void Monitoring()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string[] exceptionHour = appConfigFacade.Retrieve("PDIPKTMonitoring.ExcpHour").Value.Split(';');
            string curHour = DateTime.Now.Hour.ToString();
            int idx = Array.IndexOf(exceptionHour, curHour);
            if (idx > -1)
            {
                return;
            }
            string username = appConfigFacade.Retrieve("BabitCognitoSharePoint.UserName").Value;
            string password = appConfigFacade.Retrieve("BabitCognitoSharePoint.Password").Value;
            string domain = appConfigFacade.Retrieve("BabitCognitoSharePoint.DomainSite").Value;
            string weburl = appConfigFacade.Retrieve("BabitCognitoSharePoint.Domain").Value;

            IMapper i_Mapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            DataSet dSet = i_Mapper.RetrieveDataSet("sp_DealerNotificationSheetName");
            if (dSet.Tables.Count > 1)
            {
                string folderName = dSet.Tables[1].Rows[0][0].ToString();
                DataTable dtSheetName = dSet.Tables[0];

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
                            if (fol.Name.ToLower() == folderName.ToLower())
                            {
                                clientContext.Load(fol.Files);
                                clientContext.ExecuteQuery();
                                var fc = fol.Files.ToList();

                                DataSet dSetMain = i_Mapper.RetrieveDataSet("sp_DealerNotificationWriteExcel");
                                if (dSetMain.Tables.Count > 0)
                                {

                                    int NotificationID = 0;
                                    DateTime dtLastUpdate = DateTime.Now;
                                    string fileRef = string.Empty;
                                    DataTable dtMain = dSetMain.Tables[0];
                                    foreach (DataRow iRow in dtMain.Rows)
                                    {
                                        ExcelPackage exc = new ExcelPackage();
                                        bool IsFileAlready = false;
                                        NotificationID = int.Parse(iRow["ID"].ToString());

                                        Microsoft.SharePoint.Client.File fn = fc.FirstOrDefault(
                                        x =>
                                        x.Name.ToLower().StartsWith(iRow["GroupName"].ToString().ToLower()));
                                        if (fn != null)
                                        {
                                            if (listFileOpen.Where(x => x.Name == fn.Name).Count() == 0)
                                            {
                                                listFileOpen.Add(fn);
                                            }

                                            fileRef = fn.ServerRelativeUrl;
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

                                                exc = new ExcelPackage(memory);
                                            }
                                            IsFileAlready = true;
                                        }

                                        using (exc)
                                        {
                                            if (IsFileAlready)
                                            {
                                                int sheetTemp = exc.Workbook.Worksheets.Count;
                                                for (int i = sheetTemp; i > 0; i--)
                                                {
                                                    exc.Workbook.Worksheets.Delete(i);
                                                }
                                            }

                                            foreach (DataRow sRow in dtSheetName.Rows)
                                            {

                                                ArrayList arrParam = new ArrayList();
                                                arrParam.Add(new SqlParameter("@dealerid", iRow["DealerID"]));

                                                DataTable dtData = i_Mapper.RetrieveDataSet(sRow[1].ToString(), arrParam).Tables[0];
                                                ExcelWorksheet sheet = exc.Workbook.Worksheets.Add(sRow[2].ToString());

                                                sheet.Protection.IsProtected = false;

                                                int rowsInsert = 1;
                                                int column = 1;

                                                string lastUpdate = string.Format("Data ini diproses terakhir pada tanggal {0}",
                                                    dtLastUpdate.ToString("dd MMM yyyy HH:mm"));

                                                sheet.Cells[rowsInsert, column].Style.Font.Bold = true;
                                                sheet.Cells[rowsInsert, column].Style.Font.Name = "Arial";
                                                sheet.Cells[rowsInsert, column].Style.Font.Size = 12;
                                                sheet.Cells[rowsInsert, column].Value = lastUpdate;

                                                rowsInsert += 1;
                                                sheet.Cells[rowsInsert, column].Style.Font.Bold = true;
                                                sheet.Cells[rowsInsert, column].Style.Font.Name = "Arial";
                                                sheet.Cells[rowsInsert, column].Style.Font.Size = 12;
                                                sheet.Cells[rowsInsert, column].Value = sRow[3].ToString();
                                                rowsInsert += 1;
                                                rowsInsert += 1;

                                                sheet.Cells[rowsInsert, column].SetHeaderValue("NO", 1);
                                                column += 1;

                                                foreach (DataColumn iColumn in dtData.Columns)
                                                {
                                                    if (iColumn.ColumnName.ToLower().IndexOf("hidden") < 0)
                                                    {
                                                        string columnName = iColumn.ColumnName.ToUpper().Replace("_WARNING", "");
                                                        sheet.Cells[rowsInsert, column].SetHeaderValue(columnName, 1);
                                                        column += 1;
                                                    }
                                                }

                                                foreach (DataRow vRow in dtData.Rows)
                                                {
                                                    rowsInsert += 1;
                                                    column = 1;
                                                    bool allWarning = false;
                                                    try
                                                    {
                                                        if (vRow["infowarningall_hidden"].ToString() == "1")
                                                        {
                                                            allWarning = true;
                                                        }
                                                    }
                                                    catch { allWarning = false; }

                                                    sheet.Cells[rowsInsert, column].SetValue((rowsInsert - 4).ToString());
                                                    if (allWarning)
                                                    {
                                                        sheet.Cells[rowsInsert, column].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                        sheet.Cells[rowsInsert, column].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                                                    }

                                                    column += 1;

                                                    foreach (DataColumn vColm in dtData.Columns)
                                                    {
                                                        if (vColm.ColumnName.ToLower().IndexOf("hidden") < 0)
                                                        {
                                                            if (vRow[vColm.ColumnName] is DateTime)
                                                            {
                                                                DateTime dValue = DateTime.Parse(vRow[vColm.ColumnName].ToString());
                                                                sheet.Cells[rowsInsert, column].SetValue(dValue.ToString("yyyy-MM-dd"));
                                                            }
                                                            else
                                                            {
                                                                sheet.Cells[rowsInsert, column].SetValue(vRow[vColm.ColumnName].ToString());
                                                            }


                                                            if (vColm.ColumnName.ToLower().IndexOf("_warning") > 0)
                                                            {
                                                                try
                                                                {
                                                                    if (vRow["infowarning_hidden"].ToString() == "1")
                                                                    {
                                                                        sheet.Cells[rowsInsert, column].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                                        sheet.Cells[rowsInsert, column].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                                                                    }
                                                                }
                                                                catch { }
                                                            }

                                                            if (allWarning)
                                                            {
                                                                sheet.Cells[rowsInsert, column].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                                sheet.Cells[rowsInsert, column].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                                                            }

                                                            column += 1;
                                                        }

                                                    }
                                                }

                                                //Auto Filter Data
                                                sheet.Cells[4, 2, rowsInsert, (column - 1)].AutoFilter = true;

                                                //Autofit Column Size
                                                for (int i = 2; i < column; i++)
                                                {
                                                    sheet.Column(i).AutoFit();
                                                }
                                            }
                                            var filestream = new MemoryStream(exc.GetAsByteArray(), true);
                                            if (clientContext.HasPendingRequest)
                                            {
                                                clientContext.ExecuteQuery();
                                            }

                                            if (IsFileAlready)
                                            {
                                                try
                                                {
                                                    Microsoft.SharePoint.Client.File.SaveBinaryDirect(clientContext, fileRef, filestream, true);

                                                    ArrayList arrUpdate = new ArrayList();
                                                    arrUpdate.Add(new SqlParameter("@id", NotificationID));
                                                    arrUpdate.Add(new SqlParameter("@DateProc", dtLastUpdate));

                                                    i_Mapper.ExecuteSP("sp_DealerNotificationUpdateLastProcessed", arrUpdate);
                                                }
                                                catch { }
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    FileCreationInformation newFile = new FileCreationInformation();
                                                    newFile.ContentStream = filestream;
                                                    newFile.Url = String.Format("{0}/{1}.xlsx", fol.ServerRelativeUrl, iRow["GroupName"].ToString());

                                                    Microsoft.SharePoint.Client.File uploadFile = fol.Files.Add(newFile);

                                                    //Load file
                                                    clientContext.Load(uploadFile);

                                                    //Execute context into SP
                                                    clientContext.ExecuteQuery();

                                                    listFileOpen.Add(uploadFile);


                                                    ArrayList arrUpdate = new ArrayList();
                                                    arrUpdate.Add(new SqlParameter("@id", NotificationID));
                                                    arrUpdate.Add(new SqlParameter("@DateProc", dtLastUpdate));

                                                    i_Mapper.ExecuteSP("sp_DealerNotificationUpdateLastProcessed", arrUpdate);

                                                }
                                                catch { }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e){}
                    finally
                    {
                        foreach (var fnItem in listFileOpen)
                        {
                            fnItem.CheckOut();
                        }
                    }

                }

            }
        }
    }
}
