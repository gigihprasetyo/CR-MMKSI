using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Web;
using KTB.DNet.BusinessFacade;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain;
using Microsoft.SharePoint.Client;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using KTB.DNet.Domain.Search;
using System.Collections;

namespace KTB.DNet.WebApi.Models
{
    public static class BabitInterFace
    {

        public static bool GetJsonFileBabit(string username, string password, string weburl, string url, Microsoft.SharePoint.Client.SharePointOnlineCredentials credentials)
        {
            //string strJson = "";

            try
            {
                dynamic data = DownloadFile(weburl, username, password, url, credentials);
                return insertintodatabaseEventBabit(data);
            }
            catch (Exception aa)
            {
                Console.WriteLine("Error : " + aa.Message.ToString());
            }

            return false;
        }

        public static void GetFilesFolders(string dealercode, string babittype)
        {

            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);

            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            string username = appConfigFacade.Retrieve("BabitCognitoSharePoint.UserName").Value;
            string password = appConfigFacade.Retrieve("BabitCognitoSharePoint.Password").Value;
            string domain = appConfigFacade.Retrieve("BabitCognitoSharePoint.DomainSite").Value;
            string weburl = appConfigFacade.Retrieve("BabitCognitoSharePoint.Domain").Value;
            string foldersite = appConfigFacade.Retrieve("BabitCognitoSharePoint.FolderSite").Value;

            ClientContext context = new ClientContext(domain);

            var secure = new SecureString();
            foreach (char c in password)
            {
                secure.AppendChar(c);
            }

            var credentials = new Microsoft.SharePoint.Client.SharePointOnlineCredentials(username, secure);
            context.Credentials = credentials;

            List list = context.Web.Lists.GetByTitle("Documents");
            Folder fold = list.RootFolder;
            FolderCollection foldColl = list.RootFolder.Folders;
            List<string> strlistfile = new List<string>();
            FileCollection filecoll = list.RootFolder.Files;
            context.Load(fold);
            context.Load(list);
            context.Load(list.RootFolder);
            context.Load(list.RootFolder.Folders);
            context.Load(list.RootFolder.Files);

            context.ExecuteQuery();

            foreach (Folder fol in foldColl)
            {
                if (fol.Name == foldersite)
                {
                    Console.WriteLine("Folder Name : " + fol.Name);
                    context.Load(fol.Files);
                    context.ExecuteQuery();
                    FileCollection fc = fol.Files;
                    foreach (var fn in fc)
                    {
                        if (fn.Name.ToUpper().IndexOf("APPROVED") != -1)
                        {
                            if (fn.Name.Substring(0, 6) == dealercode)
                            {
                                if (fn.Name.Substring(7, babittype.Length) == babittype)
                                {
                                    if (fn.Name.Substring(fn.Name.Length - 5, 5) == ".JSON")
                                    {
                                        strlistfile.Add(fn.Name);

                                        BabitSharePointFileAttributesFacade att = new BabitSharePointFileAttributesFacade(User);
                                        CriteriaComposite cri = new CriteriaComposite(new Criteria(typeof(BabitSharePointFileAttributes), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
                                        cri.opAnd(new Criteria(typeof(BabitSharePointFileAttributes), "FileName", MatchType.Exact, fn.Name));
                                        //cri.opAnd(new Criteria(typeof(BabitSharePointFileAttributes), "FileUpdatedTime", MatchType.Exact, fn.TimeLastModified.ToString("yyyyMMdd HH:mm:ss")));

                                        ArrayList arr = att.Retrieve(cri);

                                        string url = appConfigFacade.Retrieve("BabitCognitoSharePoint.URLFile").Value;

                                        if (arr.Count == 0)
                                        {

                                            string fina = fn.Name;
                                            fina = fina.Replace("_", "%5F");
                                            fina = fina.Replace(".", "%2E");
                                            url = url + fina.Replace(" ", "%20");
                                            //FileInformation fileinfo = Microsoft.SharePoint.Client.File.OpenBinaryDirect(context, (string)fn.ServerRelativeUrl);

                                            GetJsonFileBabit(username, password, weburl, url, credentials);
                                            insertintoBabitSharePointFileAttributes(fn);
                                        }
                                        else
                                        {
                                            BabitSharePointFileAttributes objBSPFA = (BabitSharePointFileAttributes)arr[0];
                                            if (objBSPFA.FileUpdatedTime.ToString("yyyyMMdd hh:mm:ss") != fn.TimeLastModified.ToString("yyyyMMdd hh:mm:ss"))
                                            {
                                                GetJsonFileBabit(username, password, weburl, url, credentials);
                                                UpdateintoBabitSharePointFileAttributes(fn, arr);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                }
            }
        }

        private static void insertintoBabitSharePointFileAttributes(File fn)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            BabitSharePointFileAttributesFacade BabitShareFac = new BabitSharePointFileAttributesFacade(User);
            BabitSharePointFileAttributes objBabitShare = new BabitSharePointFileAttributes();
            objBabitShare.FileName = fn.Name;
            objBabitShare.FileCreatedTime = fn.TimeCreated;
            objBabitShare.FileUpdatedTime = fn.TimeLastModified;
            objBabitShare.DownloadedTime = DateTime.Now;
            objBabitShare.RowStatus = 0;

            int result = 0;
            result = BabitShareFac.Insert(objBabitShare);

        }

        private static void UpdateintoBabitSharePointFileAttributes(File fn, ArrayList arr)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            BabitSharePointFileAttributesFacade BabitShareFac = new BabitSharePointFileAttributesFacade(User);

            BabitSharePointFileAttributes objBabitShare = new BabitSharePointFileAttributes();
            foreach (BabitSharePointFileAttributes ad in arr)
            {
                objBabitShare = ad;
                break;
            }

            objBabitShare.FileUpdatedTime = fn.TimeLastModified;
            objBabitShare.DownloadedTime = DateTime.Now;

            int result = 0;
            result = BabitShareFac.Update(objBabitShare);

        }

        private static bool insertintodatabaseFileBabit(dynamic data)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);

            string strQuery = string.Format("exec InsertMarboxFromSharePoint @submissionID='{0}'", data.MarketingBox.SubmissionID);
            strQuery = strQuery + string.Format(", @StartDate='{0}'", data.MarketingBox.StartDate);
            strQuery = strQuery + string.Format(", @EndDate='{0}'", data.MarketingBox.EndDate);
            strQuery = strQuery + string.Format(", @EventName='{0}'", data.MarketingBox.NamaEvent);
            strQuery = strQuery + string.Format(", @TipeEvent='{0}'", data.TipeEvent);
            strQuery = strQuery + string.Format(", @DealerCode='{0}'", data.MarketingBox.IDDealer.Label);
            strQuery = strQuery + string.Format(", @EventLocation='{0}'", data.MarketingBox.TempatEvent);


            return appConfigFacade.InsertMarbox(strQuery);
        }

        private static bool insertintodatabaseEventBabit(dynamic data)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            BabitMarketingBoxFacade bmbFacade = new BabitMarketingBoxFacade(User);

            CriteriaComposite cri = new CriteriaComposite(new Criteria(typeof(BabitMarketingBox), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
            cri.opAnd(new Criteria(typeof(BabitMarketingBox), "SubMissionID", MatchType.Exact, data.MarketingBox.SubmissionID));

            ArrayList arr = bmbFacade.Retrieve(cri);

            if (arr.Count > 0)
            {
                BabitMarketingBox objbmb = new BabitMarketingBox();

                objbmb.SubMissionID = data.MarketingBox.SubmissionID;
                objbmb.StartDate = data.MarketingBox.StartDate;
                objbmb.EndDate = data.MarketingBox.EndDate;
                objbmb.EventName = data.MarketingBox.NamaEvent;
                objbmb.BabitType = data.TipeEvent;
                objbmb.DealerCode = data.MarketingBox.IDDealer.Label;
                objbmb.EventLocation = data.MarketingBox.TempatEvent;
                int res = 0;
                res = bmbFacade.Update(objbmb);
                if (res > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string strQuery = string.Format("exec InsertMarboxFromSharePoint @submissionID='{0}'", data.MarketingBox.SubmissionID);
                strQuery = strQuery + string.Format(", @StartDate='{0}'", data.MarketingBox.StartDate);
                strQuery = strQuery + string.Format(", @EndDate='{0}'", data.MarketingBox.EndDate);
                strQuery = strQuery + string.Format(", @EventName='{0}'", data.MarketingBox.NamaEvent);
                strQuery = strQuery + string.Format(", @TipeEvent='{0}'", data.TipeEvent);
                strQuery = strQuery + string.Format(", @DealerCode='{0}'", data.MarketingBox.IDDealer.Label);
                strQuery = strQuery + string.Format(", @EventLocation='{0}'", data.MarketingBox.TempatEvent);

                return bmbFacade.InsertMarbox(strQuery);
            }
        }

        private static dynamic DownloadFile(String webUri, String userName, String password, string file, Microsoft.SharePoint.Client.SharePointOnlineCredentials credentials)
        {
            System.Net.WebProxy wp = new System.Net.WebProxy("172.17.240.84", 9090);

            using (var client = new WebClient())
            {

                client.Proxy = wp;
                //var securePassword = new SecureString();
                //foreach (var c in password) { securePassword.AppendChar(c); }

                //var credentials = new Microsoft.SharePoint.Client.SharePointOnlineCredentials(userName, securePassword);
                //credentials.GetAuthenticationCookie(new Uri(webUri));

                client.Credentials = credentials;
                client.Headers.Add("X-FORMS_BASED_AUTH_ACCEPTED", "f");
                client.Headers.Add("User-Agent: Other");

                var resultjson = Guid.NewGuid().ToString();
                var strjson = client.DownloadString(@file);
                //   client.DownloadFile(file,result.ToString()+".json");


                dynamic data = JObject.Parse(strjson);

                //dynamic Result = JsonConvert.DeserializeObject<paramBabitType>(strjson);
                //Console.WriteLine(data.MarketingBox.SubmissionID);
                //var value = Result.marketingbox__c.SubmissionID__c;
                return data;
            }
        }
    }



    public static class BabitInterFace2
    {



        public static void GetFilesFolders(string dealercode, string babittype)
        {

            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("BabitInterface"), null);

            AppConfigFacade appConfigFacade = new AppConfigFacade(User);
            CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(AppConfig), "RowStatus", MatchType.Exact, Convert.ToInt16(DBRowStatus.Active)));
            crit.opAnd(new Criteria(typeof(AppConfig), "Name", MatchType.Partial, "BabitCognitoSharePoint"));

            List<AppConfig> lstApp = new List<AppConfig>();

            lstApp = appConfigFacade.Retrieve(crit).Cast<AppConfig>().ToList();

            //string username =  appConfigFacade.Retrieve("BabitCognitoSharePoint.UserName").Value;
            //string password = appConfigFacade.Retrieve("BabitCognitoSharePoint.Password").Value;
            //string domain = appConfigFacade.Retrieve("BabitCognitoSharePoint.DomainSite").Value;
            //string weburl = appConfigFacade.Retrieve("BabitCognitoSharePoint.Domain").Value;
            //string foldersite = appConfigFacade.Retrieve("BabitCognitoSharePoint.FolderSite").Value;
            //string FolderID = appConfigFacade.Retrieve("BabitCognitoSharePoint.FolderID").Value;

            var username = (from AppConfig ap in lstApp
                            where ap.Name.ToUpper().Equals("BABITCOGNITOSHAREPOINT.USERNAME")
                            select ap.Value).FirstOrDefault().ToString();

            var password = (from AppConfig ap in lstApp
                            where ap.Name.ToUpper().Equals("BABITCOGNITOSHAREPOINT.PASSWORD")
                            select ap.Value).FirstOrDefault().ToString();

            var domain = (from AppConfig ap in lstApp
                          where ap.Name.ToUpper().Equals("BABITCOGNITOSHAREPOINT.DOMAIN")
                          select ap.Value).FirstOrDefault().ToString();

            var weburl = (from AppConfig ap in lstApp
                          where ap.Name.ToUpper().Equals("BABITCOGNITOSHAREPOINT.DOMAINSITE")
                          select ap.Value).FirstOrDefault().ToString();

            var foldersite = (from AppConfig ap in lstApp
                              where ap.Name.ToUpper().Equals("BABITCOGNITOSHAREPOINT.FOLDERSITE")
                              select ap.Value).FirstOrDefault().ToString();

            var FolderID = (from AppConfig ap in lstApp
                            where ap.Name.ToUpper().Equals("BABITCOGNITOSHAREPOINT.FOLDERID")
                            select ap.Value).FirstOrDefault().ToString();

            var ProxyAddress = (from AppConfig ap in lstApp
                                where ap.Name.ToUpper().Equals("BABITCOGNITOSHAREPOINT.PROXYADDRESS")
                                select ap.Value).FirstOrDefault().ToString();

            var ProxyPort = (from AppConfig ap in lstApp
                             where ap.Name.ToUpper().Equals("BABITCOGNITOSHAREPOINT.PROXYPORT")
                             select ap.Value).FirstOrDefault().ToString();

            ClientContext context = new ClientContext(weburl);

            var secure = new SecureString();
            foreach (char c in password)
            {
                secure.AppendChar(c);
            }

            if (ProxyAddress != null && ProxyAddress.ToString() != "")
            {
                System.Net.WebProxy wp = new System.Net.WebProxy(ProxyAddress.ToString(), Convert.ToInt32(ProxyPort));
                context.ExecutingWebRequest += (sen, ags) =>
                {
                    ags.WebRequestExecutor.WebRequest.Proxy = wp;
                };
            }

            var credentials = new Microsoft.SharePoint.Client.SharePointOnlineCredentials(username, secure);
            context.Credentials = credentials;

            context.ExecuteQuery();

            List list = context.Web.Lists.GetByTitle("Documents");

            Folder _folderMarbox = null;
            if (FolderID!=null && FolderID !="")
            {
                _folderMarbox = context.Web.GetFolderById(new Guid(FolderID));
                context.Load(_folderMarbox);
                context.ExecuteQuery();
            }else
            {
                Folder ff = list.RootFolder;
                FolderCollection fcol = list.RootFolder.Folders;
                List<string> lstFile = new List<string>();
                FileCollection ficol = list.RootFolder.Files;
                context.Load(ff);
                context.Load(list);
                context.Load(list.RootFolder);
                context.Load(list.RootFolder.Folders);
                context.Load(list.RootFolder.Files);
                context.ExecuteQuery();

                foreach (Folder f in fcol)//  foreach (Folder f in fcol)
                {
                    if (f.Name.ToUpper() == foldersite.ToString().ToUpper().Trim())
                    {
                        _folderMarbox = f;
                        break;
                    }
                }
            }
          

            // Console.WriteLine(_folderMarbox.Name);
            context.Load(_folderMarbox.Files);
            context.ExecuteQuery();

            //TODO di isi dengan data [BabitMarketingBox.TimeLastModified] atas dealer & Tipe babit parameter secara Descending
            DateTime dt = new DateTime(1900, 1, 1);
            BabitMarketingBoxFacade objBMBFac = new BabitMarketingBoxFacade(User);
            ArrayList arr = new ArrayList();
            arr = objBMBFac.getLastModifiedDateFile(dealercode , babittype);

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
                    FileInformation fileInformation = Microsoft.SharePoint.Client.File.OpenBinaryDirect(context, (string)file.ServerRelativeUrl);

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

        private static void insertintoBabitSharePointFileAttributes(File fn)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            BabitSharePointFileAttributesFacade BabitShareFac = new BabitSharePointFileAttributesFacade(User);
            BabitSharePointFileAttributes objBabitShare = new BabitSharePointFileAttributes();
            objBabitShare.FileName = fn.Name;
            objBabitShare.FileCreatedTime = fn.TimeCreated;
            objBabitShare.FileUpdatedTime = fn.TimeLastModified;
            objBabitShare.DownloadedTime = DateTime.Now;
            objBabitShare.RowStatus = 0;

            int result = 0;
            result = BabitShareFac.Insert(objBabitShare);

        }

        private static void UpdateintoBabitSharePointFileAttributes(File fn, ArrayList arr)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            BabitSharePointFileAttributesFacade BabitShareFac = new BabitSharePointFileAttributesFacade(User);

            BabitSharePointFileAttributes objBabitShare = new BabitSharePointFileAttributes();
            foreach (BabitSharePointFileAttributes ad in arr)
            {
                objBabitShare = ad;
                break;
            }

            objBabitShare.FileUpdatedTime = fn.TimeLastModified;
            objBabitShare.DownloadedTime = DateTime.Now;

            int result = 0;
            result = BabitShareFac.Update(objBabitShare);

        }

        private static bool insertintodatabaseFileBabit(dynamic data)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);

            string strQuery = string.Format("exec InsertMarboxFromSharePoint @submissionID='{0}'", data.MarketingBox.SubmissionID);
            strQuery = strQuery + string.Format(", @StartDate='{0}'", data.MarketingBox.StartDate);
            strQuery = strQuery + string.Format(", @EndDate='{0}'", data.MarketingBox.EndDate);
            strQuery = strQuery + string.Format(", @EventName='{0}'", data.MarketingBox.NamaEvent);
            strQuery = strQuery + string.Format(", @TipeEvent='{0}'", data.TipeEvent);
            strQuery = strQuery + string.Format(", @DealerCode='{0}'", data.MarketingBox.IDDealer.Label);
            strQuery = strQuery + string.Format(", @EventLocation='{0}'", data.MarketingBox.TempatEvent);


            return appConfigFacade.InsertMarbox(strQuery);
        }

        private static bool MergeDatabaseEventBabit(dynamic data, DateTime FileTimeLastModified)
        {
            var User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("SalesForceService"), null);
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