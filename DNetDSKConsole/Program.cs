using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
//using MMKSI.DNet.Upload.Utility;

namespace DNetDSKConsole
{
    public class WsLog
    {
        public string ID { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Body { get; set; }
        public int RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }

    public class ConsoleLog
    {
        public String WSKeyName { get; set; }
        public DateTime DateTimeLog { get; set; }
        public int CountDataLog { get; set; }
        public List<String> DataListLog { get; set; }
        public Boolean StatusErrorLog { get; set; }
        public String ErrorLog { get; set; }
        public string WsLogID { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();

        }

        static async Task<Uri> ResendWsLogAsync(String wsLogBody)
        {
            // Send the String As Raw Body Post
            var httpContent = new StringContent(wsLogBody, Encoding.UTF8, "text/plain");
            string ApiUrl = ConfigurationManager.AppSettings.Get("APIADDRESS").ToString();
            HttpResponseMessage response = await client.PostAsync(ApiUrl, httpContent);

            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        static async Task RunAsync()
        {

            try
            {
                string strWsLogID = "";
                int intNo = 0;
                WsLog wslog;
                bool bolstatusErrorLog;
                string strErrorLog = "-";
                List<String> lstBody;
                int cntLog = 0;
                string WsUrl = ConfigurationManager.AppSettings.Get("URLADDRESS").ToString();
                client.BaseAddress = new Uri(WsUrl);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                ArrayList arrWsKey = new ArrayList();
                arrWsKey.Add("SPPACKING");
                arrWsKey.Add("SPPRINTPACKING");
                arrWsKey.Add("SPBILLING");
                arrWsKey.Add("SPPAYMENT");
                arrWsKey.Add("SPDODELETE");
                arrWsKey.Add("SPBILLINGDELETE");

                foreach (object wsKey in arrWsKey)
                {
                    strErrorLog = "-";
                    strWsLogID = "";
                    bolstatusErrorLog = false;
                    lstBody = new List<String>();
                    cntLog = 0;
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectDB"].ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("up_ResendWSLog", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@WSKey", SqlDbType.VarChar, 50).Value = wsKey;
                        cmd.Parameters.Add("@TimeStart", SqlDbType.DateTime).Value = GetCutOff().Item1;  // DtTimeStart;
                        cmd.Parameters.Add("@TimeEnd", SqlDbType.DateTime).Value = GetCutOff().Item2; //DtTimeEnd;

                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                // Create a new WsLog
                                wslog = new WsLog
                                {
                                    ID = dr["ID"].ToString(),
                                    Source = dr["Source"].ToString(),
                                    Status = dr["Status"].ToString(),
                                    Message = dr["Message"].ToString(),
                                    Body = dr["Body"].ToString(),
                                    RowStatus = Convert.ToInt32(dr["RowStatus"]),
                                    CreatedBy = dr["CreatedBy"].ToString(),
                                    CreatedTime = (DateTime)(dr["CreatedTime"]),
                                    LastUpdatedBy = dr["LastUpdatedBy"].ToString(),
                                    LastUpdatedTime = (DateTime)(dr["LastUpdatedTime"])
                                };

                                try 
	                            {	        
	                                var url = await ResendWsLogAsync(wslog.Body);
                                    string strBody = wslog.Body;
                                    lstBody.Add(strBody);
                                    cntLog += 1;
                                    strWsLogID += wslog.ID;

                                }
                                catch (Exception ex)
                                {
                                    bolstatusErrorLog = true; strErrorLog = ex.Message;
                                }
                            }
                        }

                        conn.Close();
                    }

                    ConsoleLog consLog = new ConsoleLog();
                    consLog.WSKeyName = wsKey.ToString();
                    consLog.DateTimeLog = DateTime.Now;
                    consLog.CountDataLog = cntLog;
                    consLog.DataListLog = lstBody;
                    consLog.StatusErrorLog = bolstatusErrorLog;
                    consLog.ErrorLog = strErrorLog;
                    consLog.WsLogID = strWsLogID;

                    intNo += 1;
                    CreateTextFile(consLog, intNo);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(@"Created at {0}", DateTime.Now);

            //Console.ReadKey();
            Environment.Exit(-1);
            
        }


          
        static Tuple<DateTime, DateTime> GetCutOff()
        {
            DateTime dtFrom = DateTime.Now;
            DateTime DtTo = DateTime.Now;
            //TimeSpan start = new TimeSpan(0, 0, 0); 
            //TimeSpan end = new TimeSpan(0, 0, 0); 
            //TimeSpan now = DateTime.Now.TimeOfDay;

            ////Schedule jam 00
            //start = new TimeSpan(0, 0, 0); 
            //end = new TimeSpan(0, 1, 0); 
            //if ((now >= start) && (now <= end))
            //{
            //    dtFrom = new DateTime(DtTo.Year, DtTo.Month, DtTo.Day, 18, 0, 0).AddDays(-1);
            //}

            ////Schedule jam 6
            //start = new TimeSpan(6, 0, 0); 
            //end = new TimeSpan(6, 1, 0); 
            //if ((now >= start) && (now <= end))
            //{
            //    dtFrom = new DateTime(DtTo.Year, DtTo.Month, DtTo.Day, 0, 0, 0);
            //}

            ////Schedule jam 18
            //start = new TimeSpan(18, 0, 0); 
            //end = new TimeSpan(18, 1, 0); 
            //if ((now >= start) && (now <= end))
            //{
            //    dtFrom = new DateTime(DtTo.Year, DtTo.Month, DtTo.Day, 6, 0, 0);
            //}


            //Schedule jam 00
            if (DtTo.Hour >= 0 && DtTo.Hour <= 1)
            {
                dtFrom = new DateTime(DtTo.Year, DtTo.Month, DtTo.Day, 18, 0, 0).AddDays(-1);
            }
            //Schedule jam 6
            if (DtTo.Hour >= 5 && DtTo.Hour <= 6 && DtTo.Minute >= 5 && DtTo.Minute <= 6)
            {
                dtFrom = new DateTime(DtTo.Year, DtTo.Month, DtTo.Day, 0, 0, 0);
            }
            //Schedule jam 18
            if (DtTo.Hour >= 17 && DtTo.Hour <= 18)
            {
                dtFrom = new DateTime(DtTo.Year, DtTo.Month, DtTo.Day, 6, 0, 0);
            }

            return Tuple.Create(dtFrom, DtTo);
        }

        static void CreateTextFile(ConsoleLog consLog, int intNo)
        {
           

            try
            {
                string WSLogFolder = ConfigurationManager.AppSettings.Get("WSLogFolder").ToString();
                DateTime datetimeWsLog = DateTime.Now.AddSeconds(intNo);
                string filename = String.Format("{0}{1}{2}", "WSLog_", datetimeWsLog.ToString("ddMMyyyyHHmmsstt") + "_" + intNo.ToString() + "_" + consLog.WsLogID, ".txt").ToLower();
                filename = string.Concat("\\", filename);

                string FileNameWithFolder = WSLogFolder + filename;

                CreateFolder(WSLogFolder);
                if (File.Exists(FileNameWithFolder))
                {
                    File.Delete(FileNameWithFolder);
                }
                FileStream fs = new FileStream(FileNameWithFolder, FileMode.CreateNew);
                StreamWriter w  = new StreamWriter(fs);

                WriteLogToFile(w, consLog);

                w.Close();
                fs.Close();
            }
            catch (Exception)
            {
                
               
            }
        }

        static void CreateFolder(string folderName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folderName);
            if (!dirInfo.Exists)
	        {
                dirInfo.Create();
            }
        }

        static void WriteLogToFile(StreamWriter w, ConsoleLog consoleLog)
        {
            StringBuilder itemLine = new StringBuilder();
            itemLine.Append("Nama WsKey : " + consoleLog.WSKeyName);
            itemLine.Append(Environment.NewLine);
            itemLine.Append("Tanggal Resend : " + String.Format("{0:yyyyMMdd}", consoleLog.DateTimeLog));
            itemLine.Append(Environment.NewLine);
            itemLine.Append("Count Data WS : " + consoleLog.CountDataLog);
            itemLine.Append(Environment.NewLine);
            itemLine.Append(Environment.NewLine);
            itemLine.Append("Data WS : ");
            itemLine.Append(Environment.NewLine);
            foreach (var item in consoleLog.DataListLog)
            {
                itemLine.Append(item);
                itemLine.Append(Environment.NewLine);
                itemLine.Append(Environment.NewLine);
            }
            itemLine.Append("Status Error : " + consoleLog.StatusErrorLog.ToString());
            itemLine.Append(Environment.NewLine);
            itemLine.Append("Error : " + consoleLog.ErrorLog);

            w.WriteLine(itemLine.ToString());
        }

    }
}
