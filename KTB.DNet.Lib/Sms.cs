using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data; 

namespace KTB.DNet.Lib
{
	/// <summary>
	/// static class for sending Sms
	/// </summary>
	public class Sms
	{
		static Database smsDatabase = null;
		static Database SmsDatabase 
		{
			get 
			{ 
				if (smsDatabase==null) 
				{
					lock (typeof(Sms)) 
					{
						if (smsDatabase==null) 
						{
                            smsDatabase = DatabaseFactory.CreateDatabase("SmsDatabaseSql");
						}
					}
				}
				return smsDatabase;
			}
		}

		/// <summary>
		/// Cek is connecntion to my sql OK 
		/// </summary>
		
		public static bool IsSMSGatewayLive()
		{

            return true;
            //string query = "Select count(*) from outbox";
            //bool result = false;
            //IDbConnection conn = null;
            //using (DBCommandWrapper cmd = SmsDatabase.GetSqlStringCommandWrapper(query)) 
            //{
            //    try
            //    {
            //        conn =smsDatabase.GetConnection();
            //        conn.Open(); 
            //        result =  true;
            //    }
            //    catch (Exception e)
            //    {
            //        result = false;
            //        throw new Exception("SMS Gateway tidak bisa diakses :" + e.Message.ToString() ); 
            //    }
            //    finally
            //    {
            //        if (conn !=null)
            //        {
            //            conn.Close();
            //            conn.Dispose(); 
            //        }
            //    }
					
            //}
            // return result;
		}

		/// <summary>
		/// Send Sms to mobile phone
		/// </summary>
		/// <param name="dest">recipient phone number eg +628158762583</param>
		/// <param name="message">Content of the message</param>
		public static bool Sendto(string dest, string message, int InboxID = 0)
		{
			bool status = true;

			if (dest != string.Empty)
			{
				if (dest.StartsWith("+"))
				{
				  if (dest.Substring(4,1).Equals("0"))
					{
						status=false;
					}
			    }
			}

			if (status==true)
			{
                //string query = "INSERT INTO outbox(remote_number, content) VALUES(@remote_number, @content)";
                string query = "INSERT INTO outbox(DestinationNumber, SmsText, SendDate, Category, Status, InboxId) VALUES(@remote_number, @content, @SendDate, @Category, @Status, @InboxId)";
				using (DBCommandWrapper cmd = SmsDatabase.GetSqlStringCommandWrapper(query)) 
				{
					cmd.AddInParameter("remote_number", DbType.String, dest);
					cmd.AddInParameter("content", DbType.String, message);
                    cmd.AddInParameter("SendDate", DbType.DateTime, DateTime.Now);
                    cmd.AddInParameter("Category", DbType.String, "OUTGOING");
                    cmd.AddInParameter("Status", DbType.String, "UNSENT");
                    cmd.AddInParameter("InboxId", DbType.Int32, InboxID);
					try
					{
						SmsDatabase.ExecuteNonQuery(cmd);
						return true;
					}
					catch (Exception e)
					{
						throw new Exception("Gagal Kirim SMS : " + e.Message.ToString()); 
//						`return false;
					}
					
				}
			}
			else
			{ return false; }
		}
	}
}
