using System;

namespace KTB.DNet.Lib
{
	public enum DNetLogFormatStatus
	{
		Direct
		, Deferred
	}
	/// <summary>
	/// Summary description for SysLogXMLMessage.
	/// </summary>
	public class SysLogXMLMessage
	{
		public SysLogXMLMessage()
		{		
		}
		
		private DNetLogFormatStatus _status = DNetLogFormatStatus.Direct;
		
		public string ToXML()
		{
			System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
			xmlString.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
			
			if ( _status == DNetLogFormatStatus.Direct )
			{
				//xmlString.AppendFormat("<direct>");
				xmlString.AppendFormat("<direct><ts>{0}</ts>",DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss"));
				xmlString.AppendFormat("<module>{0}</module>",this.ModuleName );
				msgXml(ref xmlString);
				xmlString.AppendFormat("<rnd>{0}</rnd>", this.Rnd );
				xmlString.AppendFormat("<hash>{0}</hash></direct>",this.Hash );
				//xmlString.AppendFormat("</direct>");
			}
			else
			{
				xmlString.AppendFormat("<deferred>");
				xmlString.AppendFormat("<ts>{0}</ts>",DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss"));
				xmlString.AppendFormat("<module>{0}</module>",this.ModuleName );
				msgXml(ref xmlString);
				xmlString.AppendFormat("<rnd>{0}</rnd>", this.Rnd);
				xmlString.AppendFormat("<hash>{0}</hash>",this.Hash);
				xmlString.AppendFormat("</deferred>");
			
			}
			
//			xmlString.AppendFormat("<Status>{0}</Status>", this.Status.ToString());
//			xmlString.AppendFormat("<ModuleName>{0}</ModuleName>", this.ModuleName);
//			xmlString.AppendFormat("<RemoteIPAddress>{0}</RemoteIPAddress>", this.RemoteIPAddress);
//			xmlString.AppendFormat("<UserName>{0}</UserName>", this.UserName);
//			xmlString.AppendFormat("<Pages>{0}</Pages>", this.Pages);
//			xmlString.AppendFormat("<BlockName>{0}</BlockName>", this.BlockName);
//			xmlString.AppendFormat("<SubBlockName>{0}</SubBlockName>", this.SubBlockName);
//			xmlString.AppendFormat("<Action>{0}</Action>", this.Action);
//			xmlString.AppendFormat("<Result>{0}</Result>", this.Result);
//			xmlString.AppendFormat("<FullMessage>{0}</FullMessage>", this.FullMessage);			

			return xmlString.ToString();
		}

		private void msgXml(ref System.Text.StringBuilder xmlString ) 
		{
			xmlString.AppendFormat("<msg>");
			xmlString.AppendFormat("<lvl>{0}</lvl>", this.WarningLevel);
			xmlString.AppendFormat("<rip>{0}</rip>", this.RemoteIPAddress);
			xmlString.AppendFormat("<usr>{0}</usr>", this.UserName);
			xmlString.AppendFormat("<dlr>{0}</dlr>", this.Dealer);
			xmlString.AppendFormat("<pg>{0}</pg>", this.Pages);
			xmlString.AppendFormat("<mbl>{0}</mbl>", this.BlockName);
			xmlString.AppendFormat("<sbl>{0}</sbl>", this.SubBlockName);
			xmlString.AppendFormat("<act>{0}</act>", this.Action);
			xmlString.AppendFormat("<sta>{0}</sta>", this.StatusResult);
			xmlString.AppendFormat("<cod>{0}</cod>",this.CodeResult);
			xmlString.AppendFormat("<det>{0}</det>", this.FullMessage);
			xmlString.AppendFormat("</msg>");
			
		}
																							  

		
		public DNetLogFormatStatus Status
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
			}
		}

		private string _moduleName = string.Empty;
		public string ModuleName
		{
			get
			{
				return _moduleName;
			}
			set
			{
				_moduleName = value;
			}
		}

		private string _remoteIPAddress = string.Empty;
		public string RemoteIPAddress
		{
			get
			{
				return _remoteIPAddress;
			}
			set
			{
				_remoteIPAddress = value;
			}
		}

		private string _userName = string.Empty;
		public string UserName
		{
			get
			{
				return _userName;
			}
			set
			{
				_userName = value;
			}
		}

		private string _pages = string.Empty;
		public string Pages
		{
			get
			{
				return _pages;
			}
			set
			{
				_pages = value;
			}
		}

		private string _blockName = string.Empty;
		public string BlockName
		{
			get
			{
				return _blockName;
			}
			set
			{
				_blockName = value;
			}
		}

		private string _subBlockName = string.Empty;
		public string SubBlockName
		{
			get
			{
				return _subBlockName;
			}
			set
			{
				_subBlockName = value;
			}
		}

		private string _action = string.Empty;
		public string Action
		{
			get
			{
				return _action;
			}
			set
			{
				_action = value;
			}
		}

		private string _result = string.Empty;
		public string Result
		{
			get
			{
				return _result;
			}
			set
			{
				_result = value;
			}
		}

		private string _message = string.Empty;
		public string FullMessage
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
			}
		}

		private string _rnd = string.Empty;
		public string Rnd
		{
			get
			{
				return _rnd;
			}
			set
			{
				_rnd = value;
			}
		}

		private string _hash = string.Empty;
		public string Hash
		{
			get
			{
				return _hash;
			}
			set
			{
				_hash = value;
			}
		}

		private string _warning = string.Empty;
		public string WarningLevel
		{
			get
			{
				return _warning;
			}
			set
			{
				_warning = value;
			}
		}

		private string _statusresult = string.Empty;
		public string StatusResult
		{
			get
			{
				return _statusresult;
			}
			set
			{
				_statusresult = value;
			}
		}

		private string _coderesult = string.Empty;
		public string CodeResult
		{
			get
			{
				return _coderesult;
			}
			set
			{
				_coderesult = value;
			}
		}

		private string _dealer = string.Empty;
		public string Dealer
		{
			get
			{
				return _dealer;
			}
			set
			{
				_dealer = value;
			}
		}

	}
}
