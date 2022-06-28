#region Summary
/////////////////////////////////////////////////////
/// AUTHOR			: Herry Priadi
/// CREATE DATE		: Mei 16, 2005
/// PURPOSE			:
/// SPECIAL NOTES	:
/// ================================================
/// $History: $
/// 
///
////////////////////////////////////////////////////
#endregion

#region .NET Base Class Namespace Imports
using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.ComponentModel;
#endregion

namespace KTB.DNet.WebCC
{
	/// <summary>
	/// Summary description for IntiTime.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:IntiTime runat=server></{0}:IntiTime>")]
	public class IntiTime : System.Web.UI.WebControls.Literal 
	{
	
		#region Private Constants

		private const string TEXTBOX_NAME = "txtTime";
		private const string DEFAULT_VALUE_REGEX = "^(20|21|22|23|[01]\\\\d|\\\\d)(([:.][0-5]\\\\d){1,2})$";
		private const string DEFAULT_VALUE_ERROR_MESSAGE = "Format waktu yang Anda masukkan salah";
		private const int DEFAULT_VALUE_TEXTBOX_HEIGHT = 20;
		private const int DEFAULT_VALUE_TEXTBOX_WIDTH = 80;
		private const bool DEFAULT_VALUE_ENABLED = true;

		#endregion

		#region Private Variables

		private string _strTargetForm;
		private string _strTargetTextBox;
		private string _strCssClassTextBox;
		private int _intTextBoxWidth;
		private int _intTextBoxHeight;
		private string _strStyle;
		private string _strRegexValue;
		private string _strErrorMessage;
		private string _strValue;
		private bool _boolEnabled;

		#endregion
		
		#region Constructors/Destructors/Finalizers
		/// <summary>
		/// Initializes a new instance of KTB.DNet.WebCC.IntiTime class
		/// </summary>
		public IntiTime()
		{
			this._strTargetForm = "";
			this._strTargetTextBox = "";
			//this._strTargetTextBox = IntiTime.TEXTBOX_NAME + this.GetHashCode().ToString();
			this._intTextBoxWidth = IntiTime.DEFAULT_VALUE_TEXTBOX_WIDTH;
			this._intTextBoxHeight = IntiTime.DEFAULT_VALUE_TEXTBOX_HEIGHT;
			this._strRegexValue = IntiTime.DEFAULT_VALUE_REGEX;
			this._strErrorMessage = IntiTime.DEFAULT_VALUE_ERROR_MESSAGE;
			this._strValue = "12:00";
			this._boolEnabled = true;
		}
		#endregion
		
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output">The HTML writer to write out to</param>
		protected override void Render(HtmlTextWriter output)
		{
			StringBuilder objRenderTime = new StringBuilder();

			if (this._strTargetForm.Trim() == "")
			{
				foreach (Control objControl in this.Page.Controls)
				{
					if (objControl is HtmlForm)
					{
						this._strTargetForm = ((HtmlForm)objControl).Name;
						break;
					}
				}
			}

			if (this._strTargetTextBox.Trim() == "" || this._strTargetTextBox == null)
			{
				this._strTargetTextBox = IntiTime.TEXTBOX_NAME + this.GetHashCode().ToString();
			}

			objRenderTime.Append("<script language=\"javascript\"> \n");
			objRenderTime.Append("function Validate" + this._strTargetTextBox + "() { \n");
			objRenderTime.Append("	var re = new RegExp(\"" + this._strRegexValue + "\"); \n");
			objRenderTime.Append("	if (re == \" \") { \n");
			objRenderTime.Append("		return true; }\n");
			objRenderTime.Append("	else { \n");
			objRenderTime.Append("		if (document." +  this._strTargetForm + "." + this._strTargetTextBox + ".value.match(re)) { \n");
			objRenderTime.Append("			return true; } \n");
			objRenderTime.Append("		else { \n");
			objRenderTime.Append("			return false; } \n");
			objRenderTime.Append("	} \n");
			objRenderTime.Append("} \n");
			objRenderTime.Append("</script>");

			objRenderTime.Append("<TABLE cellSpacing=\"0\" cellPadding=\"0\" border=\"0\" ID=\"Table1\"");
			if (this.Context != null)
			{
				objRenderTime.Append(" style=\"" + this._strStyle + "\">");
			}
			else
			{
				objRenderTime.Append(">");
			}
			objRenderTime.Append("<TR>");
			objRenderTime.Append("<TD><INPUT size=\"11\" name=\"" + _strTargetTextBox + "\" ID=\"" + _strTargetTextBox + "\" type=\"Text\" ");
			objRenderTime.Append(
				"style=\"width:" + this._intTextBoxWidth.ToString() + "px; " +
				"height:" + this._intTextBoxHeight.ToString() + "px\" ");
			if (!this.Enabled)
			{
				objRenderTime.Append("disabled=\"" + this.HTMLDisabled + "\" ");
			}

			objRenderTime.Append("value=\"" + this._strValue.ToString() + "\"" +
				" onfocusout=\"if (! Validate" + this._strTargetTextBox + "()) { alert('" + this._strErrorMessage + "'); }\" " +
				" class=\"" + this._strCssClassTextBox + "\"></TD>");
			objRenderTime.Append("<TD class=\"" + _strCssClassTextBox + "\">(hh:mm)</TD></TR></TABLE>");

			output.Write(objRenderTime);
		}

		#region Public Properties

		/// <summary>
		/// The Name of the Form where the TextBox is placed
		/// If this is left blank, it will be the first form found on the page
		/// </summary>
		public string TargetForm
		{
			get 
			{
				return _strTargetForm;
			}
			set
			{
				_strTargetForm = value;
			}
		}

		/// <summary>
		/// The Name of the TextBox created. Default value is
		/// <c>TextBox + this.GetHashCode().ToString()</c>
		/// </summary>
		public string TargetTextBox
		{
			get
			{
				return _strTargetTextBox;
			}
			set
			{
				_strTargetTextBox = value;
			}
		}

		/// <summary>
		/// CssClass for the TextBox
		/// </summary>
		public string CssClassTextBox
		{
			get
			{
				return _strCssClassTextBox;
			}
			set
			{
				_strCssClassTextBox = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the height of this control
		/// </summary>
		[DefaultValue(IntiTime.DEFAULT_VALUE_TEXTBOX_HEIGHT)]
		public int TextBoxHeight
		{
			get
			{
				return this._intTextBoxHeight;
			}
			set
			{
				this._intTextBoxHeight = value;
			}
		}

		/// <summary>
		/// Gets or sets the width of TextBox inside this control
		/// </summary>
		[DefaultValue(IntiTime.DEFAULT_VALUE_TEXTBOX_WIDTH)]
		public int TextBoxWidth
		{
			get
			{
				return this._intTextBoxWidth;
			}
			set
			{
				this._intTextBoxWidth = value;
			}
		}

		/// <summary>
		/// Style of TextBox 
		/// </summary>
		public string Style
		{
			get
			{
				return _strStyle;
			}
			set
			{
				_strStyle = value;
			}
		}
		
		/// <summary>
		/// Insert regex into the TextBox to validate. 
		/// </summary>
		[DefaultValue(IntiTime.DEFAULT_VALUE_REGEX)]
		public string RegexValue
		{
			get
			{
				return _strRegexValue;
			}
			set
			{
				_strRegexValue = value;
			}

		}

		/// <summary>
		/// Default Error Message will be occur if validate by regex failed. 
		/// </summary>
		[DefaultValue(IntiTime.DEFAULT_VALUE_ERROR_MESSAGE)]
		public string ErrorMessage
		{
			get
			{
				return _strErrorMessage;
			}
			set
			{
				_strErrorMessage = value;
			}
		}

		/// <summary>
		/// Get a standard HTML Disabled attribute
		/// </summary>
		private string HTMLDisabled
		{
			get
			{
				if (this.Enabled)
				{
					return "enabled";
				}
				else
				{
					return "disabled";
				}
			}
		}

		/// <summary>
		/// Enable or disable this control
		/// </summary>
		[DefaultValue(IntiTime.DEFAULT_VALUE_ENABLED)]
		public bool Enabled
		{
			get
			{
				return this._boolEnabled;
			}
			set
			{
				this._boolEnabled = value;
			}
		}

		/// <summary>
		/// Gets or sets the date shown in the TextBox
		/// </summary>
		public string Value
		{
			get
			{
				return this._strValue;
			}
			set
			{
				this._strValue = value;
			}
		}

		/// <summary>
		/// Gets or sets the caption displayed in the
		/// System.Web.UI.WebControls.Literal control
		/// </summary>
		public new string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		#endregion

		#region Public Methods
		#endregion

	
	}
}
