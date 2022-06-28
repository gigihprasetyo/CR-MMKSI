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

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class BabitCognitoLogic
    {
        public static bool Read(string dealercode, string babittype)
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("BabitCognito"), null);
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string username = appConfigFacade.Retrieve("BabitCognitoSharePoint.UserName").Value;
            string password = appConfigFacade.Retrieve("BabitCognitoSharePoint.Password").Value;
            string domain = appConfigFacade.Retrieve("BabitCognitoSharePoint.DomainSite").Value;
            string weburl = appConfigFacade.Retrieve("BabitCognitoSharePoint.Domain").Value;
            string foldersite = appConfigFacade.Retrieve("BabitCognitoSharePoint.FOLDERSITE").Value;
            string FolderID = appConfigFacade.Retrieve("BabitCognitoSharePoint.FOLDERID").Value;
            string ProxyAddress = appConfigFacade.Retrieve("BabitCognitoSharePoint.PROXYADDRESS").Value;
            string ProxyPort = appConfigFacade.Retrieve("BabitCognitoSharePoint.PROXYPORT").Value;

            List<Microsoft.SharePoint.Client.File> listFileOpen = new List<Microsoft.SharePoint.Client.File>();

            //string ProxyAddress = WebConfigurationManager.AppSettings["ProxyAddress"];
            //string ProxyPort = WebConfigurationManager.AppSettings["ProxyPort"];
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

                Folder _folderMarbox = null;
                foreach (Folder f in foldColl)//  foreach (Folder f in fcol)
                {
                    if (f.Name.ToUpper() == foldersite.ToString().ToUpper().Trim())
                    {
                        _folderMarbox = f;
                        break;
                    }
                }
                try
                {
                    clientContext.Load(_folderMarbox.Files);
                    clientContext.ExecuteQuery();

                    //TODO di isi dengan data [BabitMarketingBox.TimeLastModified] atas dealer & Tipe babit parameter secara Descending
                    DateTime dt = new DateTime(1900, 1, 1);
                    BabitMarketingBoxFacade objBMBFac = new BabitMarketingBoxFacade(User);
                    ArrayList arr = new ArrayList();
                    arr = objBMBFac.getLastModifiedDateFile(dealercode, babittype);

                    if (arr.Count > 0)
                    {
                        BabitMarketingBox obj = arr.Cast<BabitMarketingBox>().ToList()[0];
                        dt = obj.FileTimeLastModified;
                    }
                    //Get Files in folder
                    //please add where statement as needed
                    var fileJson = from Microsoft.SharePoint.Client.File _jsonFile in _folderMarbox.Files
                                   where _jsonFile.Name.Length > 5
                                           && _jsonFile.Name.ToUpper().Contains(dealercode.ToUpper())//yg ngandung kode dealer
                                             && _jsonFile.Name.ToUpper().Substring(7, babittype.Length).Contains(babittype.ToUpper())//yg ngandung tipe babit
                                            && _jsonFile.Name.ToUpper().Contains("APPROVED")//yg sudah di Approved APM kode dealer
                                              && _jsonFile.TimeLastModified > dt
                                           && _jsonFile.Name.Substring(_jsonFile.Name.Length - 5, 5).ToUpper().Equals(".JSON")//yg pake extension *.JSON tok
                                   orderby _jsonFile.TimeLastModified descending
                                   select _jsonFile;




                    if (fileJson != null)
                    {
                        foreach (Microsoft.SharePoint.Client.File file in fileJson)
                        {
                            Console.WriteLine(" File Name : " + file.Name);
                            FileInformation fileInformation = Microsoft.SharePoint.Client.File.OpenBinaryDirect(clientContext, (string)file.ServerRelativeUrl);

                            using (System.IO.StreamReader sr = new System.IO.StreamReader(fileInformation.Stream))
                            {
                                // Read the stream to a string, and write the string to the console.
                                String line = sr.ReadToEnd();
                                dynamic data = JObject.Parse(line);
                                MergeDatabaseEventBabit(data, file.TimeLastModified);
                                //Console.WriteLine(line);
                            }
                        }


                    }
                    else
                    {
                        Console.WriteLine("File ora ono");
                    }
                }
                catch (Exception e)
                {
                    string error = e.Message;
                }
                
            }
            return true;
        }

        private static bool MergeDatabaseEventBabit(dynamic data, DateTime FileTimeLastModified)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("BabitCognito"), null);
            BabitMarketingBoxFacade bmbFacade = new BabitMarketingBoxFacade(User);


            string strQuery = string.Format("exec InsertMarboxFromSharePoint @submissionID='{0}'", data.MarketingBox.SubmissionID);
            strQuery = strQuery + string.Format(", @StartDate='{0}'", data.MarketingBox.StartDate);
            strQuery = strQuery + string.Format(", @EndDate='{0}'", data.MarketingBox.EndDate);
            strQuery = strQuery + string.Format(", @EventName='{0}'", data.MarketingBox.NamaEvent.ToString().Replace("'", ""));
            strQuery = strQuery + string.Format(", @TipeEvent='{0}'", data.TipeEvent.ToString().Replace("'", ""));
            strQuery = strQuery + string.Format(", @DealerCode='{0}'", data.MarketingBox.IDDealer.Label);
            strQuery = strQuery + string.Format(", @EventLocation='{0}'", data.MarketingBox.TempatEvent.ToString().Replace("'", ""));
            strQuery = strQuery + string.Format(", @FileTimeLastModified='{0}'", FileTimeLastModified.ToString("yyyyMMdd HH:mm:ss"));

            return bmbFacade.InsertMarbox(strQuery);
            //return bmbFacade.InsertMarbox2(data.MarketingBox.SubmissionID, data.MarketingBox.StartDate, data.MarketingBox.EndDate, data.MarketingBox.NamaEvent, data.TipeEvent, data.MarketingBox.IDDealer.Label, data.MarketingBox.TempatEvent, FileTimeLastModified);

        }
    }
}
