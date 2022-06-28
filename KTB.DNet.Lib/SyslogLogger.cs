using System;
using System.Net;          
using System.Net.Sockets;  
using System.Diagnostics;
using System.Reflection;   

namespace KTB.DNet.Lib
{
	namespace Syslog 
	{
		public enum Level 
		{
			Emergency= 0,
			Alert=1, 
			Critical=2, 
			Error=3, 
			Warning=4, 
			Notice=5, 
			Information=6, 
			Debug=7, 
		}


		public enum Facility 
		{
			Kernel=0, 
			User=1, 
			Mail=2, 
			Daemon=3, 
			Auth=4, 
			Syslog=5, 
			Lpr=6, 
			News=7, 
			UUCP=8, 
			Cron=9, 
			Local0=10, 
			Local1=11, 
			Local2=12, 
			Local3=13, 
			Local4=14, 
			Local5=15, 
			Local6=16, 
			Local7=17, 
		}
	}

	/// <summary>
	/// Summary description for SyslogLogger.
	/// </summary>
	public class SyslogLogger
	{
		UdpClient c = null; 
		TcpClient _tcpConnection = null;
				
		private int PortNumber = 1468;
		private string host = string.Empty;
		private bool UseUDPProtocol = false;

		public SyslogLogger(string host, int PortNumber, bool UseUDPProtocol)
		{
			this.host = host;
			this.PortNumber = PortNumber;
			this.UseUDPProtocol = UseUDPProtocol;
		}

		public void Log(SysLogXMLMessage xmlMessage)
		{
			IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
			IPAddress ipAddress = ipHostInfo.AddressList[0];
			IPEndPoint  ipLocalEndPoint = new IPEndPoint(ipAddress, 0);

			try
			{
				if (UseUDPProtocol)
				{
					c = new UdpClient(ipLocalEndPoint); 
					c.Connect(host, PortNumber);
				}			
				else
				{
					_tcpConnection = new TcpClient(ipLocalEndPoint);
					_tcpConnection.Connect(host, PortNumber);
				}

				string syslogMsg= System.String.Format("<{0}>{1}",
					(int)Syslog.Facility.User*8 + Syslog.Level.Information
					, xmlMessage.ToXML());
				syslogMsg = syslogMsg.Replace("\r","").Replace("\n","") ;
				byte[] bytes = System.Text.Encoding.ASCII.GetBytes(syslogMsg);
				if(c != null)
				{
					c.Send(bytes, bytes.Length);
				}
				else
				{
					_tcpConnection.GetStream().Write(bytes, 0, bytes.Length);
				}
			}
			catch(SocketException ex)
			{
				throw new SysLogServerNotAvailableException("Syslog server is not available", ex);
			}
			finally
			{
				if(c != null)
				{
					c.Close();
				}
				else
				{
					_tcpConnection.Close();
				}
			}				
		}
	}
}
