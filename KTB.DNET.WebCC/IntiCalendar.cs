#region Summary 
////////////////////////////////////////////////////////////////////////////////
/// AUTHOR        : Herry Priadi
/// CREATE DATE   : 9 June 2004
/// PURPOSE       : Skeleton of IntiCalendar
/// SPECIAL NOTES : 
///		Internationalization Note : 
///			Each name of day of week should be 	different one another, 
///			because we pass disabled days of week to the javascript 
///			using the days of week name.
///			Days of Week are internationalized using 
///			string.Replace("@KEYWORD@", "..."), so don't put @KEYWORD@
///			in the javascript file.
/// ============================================================================
/// $History: $
/// 
////////////////////////////////////////////////////////////////////////////////
#endregion

#region QA Section
////////////////////////////////////////////////////////////////////////////////
/// KH Q : how should i expose to asp.net : disable certain day of week
/// JA A : using collection, but you need to provide the collection
/// NS A : using enum
/// HP A : using boolean flag for each day of week
/// keep it simple until necessary : we use boolean flag for external for now
///                                  default are true for each day of week
///                                  internally implemented as int bit set
///                                  of 2**DayOfWeek enum
///                                  jadi ada 7 property, banyak juga yah... :)
////////////////////////////////////////////////////////////////////////////////
#endregion

#region .NET Base Class Namespace Imports
using System;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Reflection;
using System.IO;
#endregion

#region Custom Class or Third Party Namespace Imports
#endregion

namespace KTB.DNet.WebCC
{
	/// <summary>
	/// Summary description for IntiCalendar.
	/// </summary>
//	[DefaultProperty("Text"), 
//		ToolboxData("<{0}:IntiCalendar runat=server></{0}:IntiCalendar>")]
	[ToolboxData("<{0}:IntiCalendar runat=server></{0}:IntiCalendar>")]
	public class IntiCalendar : Literal
	{
		#region Private Constants

		private const string TEXTBOX_NAME = "txtCalendar";
		private const string TEMPORARYFOCUS_NAME = "txtTemporaryFocus";
		private const string VW_KEY_HASDROPDOWN = "VW_HasDropDown";
		private const string VW_KEY_TARGETFORM = "VW_TargetForm";
		private const string VW_KEY_TARGETTEXTBOX = "VW_TargetTextBox";
		private const string VW_KEY_TARGETTEMPORARYFOCUS = "VW_TargetTemporaryFocus";
		private const string VW_KEY_FORMATDATE = "VW_FormatDate";
		private const string VW_KEY_VALUE = "VW_Value";
		private const string VW_KEY_ENABLED = "VW_Enabled";

		private const bool DEFAULT_VALUE_HASDROPDOWN = true;
		private const bool DEFAULT_VALUE_ENABLED = true;
		private const int DEFAULT_VALUE_HEIGHT = 20;
		private const int DEFAULT_VALUE_TABINDEX = 0;
		private const int DEFAULT_VALUE_TEXTBOX_WIDTH = 100;
		private const int DEFAULT_VALUE_BUTTON_WIDTH = 40;
		private const string DEFAULT_VALUE_ERROR_MESSAGE = "Format tanggal yang Anda masukkan salah";
		private const string DEFAULT_VALUE_URL_IMAGE = "../images/calendar.gif";
		
		//TODO: search if there is a const array initializer
		//private string[] longDayOfWeek = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
		//private string[] shortDayOfWeek = { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" };
		//private string[] longMonthOfYear = { "Januari", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

		// must define because we only wants 2 alphabet of day to show it
		private string[] shortDayOfWeek = { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" };
		// must de6fine yourself because CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames 
		// not qualified short day name in Indonesia
		private string[] shortHari = { "Mg", "Sn", "Sl", "Rb", "Km", "Jm", "Sb" };
		private string[] longDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.DayNames;
		private string[] longMonthOfYear = new string[12];

		//DayOfWeek is not used because the number is not linear
		// and there is no guarantee that it must have that specific order
		private const int SUNDAY    = 1;
		private const int MONDAY    = SUNDAY << 1;
		private const int TUESDAY   = MONDAY << 1;
		private const int WEDNESDAY = TUESDAY << 1;
		private const int THURSDAY  = WEDNESDAY << 1;
		private const int FRIDAY    = THURSDAY << 1;
		private const int SATURDAY  = FRIDAY << 1;

		//const for regex
		private const string REGEX_dd_mmm_yyyy = "";
		private const string REGEX_dd_mm_yyyy = "^(?=\\\\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\\\\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\\\\x20|$))|(?:2[0-8]|1\\\\d|0?[1-9]))([-])(?:1[012]|0?[1-9])\\\\1(?:1[6-9]|[2-9]\\\\d)?\\\\d\\\\d(?:(?=\\\\x20\\\\d)\\\\x20|$))?(((0?[1-9]|1[012])(:[0-5]\\\\d){0,2}(\\\\x20[AP]M))|([01]\\\\d|2[0-3])(:[0-5]\\\\d){1,2})?$";
		private const string REGEX_yyyy_mm_dd = "^((((19|20)(([02468][048])|([13579][26]))-02-29))|((20[0-9][0-9])|(19[0-9][0-9]))-((((0[1-9])|(1[0-2]))-((0[1-9])|(1\\\\d)|(2[0-8])))|((((0[13578])|(1[02]))-31)|(((0[1,3-9])|(1[0-2]))-(29|30)))))$";
//		private const string REGEX_ddSmmSyyyy = "^(?=\\\\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\\\\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\\\\x20|$))|(?:2[0-8]|1\\\\d|0?[1-9]))([/])(?:1[012]|0?[1-9])\\\\1(?:1[6-9]|[2-9]\\\\d)?\\\\d\\\\d(?:(?=\\\\x20\\\\d)\\\\x20|$))?(((0?[1-9]|1[012])(:[0-5]\\\\d){0,2}(\\\\x20[AP]M))|([01]\\\\d|2[0-3])(:[0-5]\\\\d){1,2})?$";
		private const string REGEX_ddSmmSyyyy = "^(?=\\\\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\\\\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\\\\x20|$))|(?:2[0-8]|1\\\\d|0?[1-9]))([/])(?:1[012]|0?[1-9])\\\\1(?:1[6-9]|[2-9]\\\\d)\\\\d\\\\d(?:(?=\\\\x20\\\\d)\\\\x20|$))?(((0?[1-9]|1[012])(:[0-5]\\\\d){0,2}(\\\\x20[AP]M))|([01]\\\\d|2[0-3])(:[0-5]\\\\d){1,2})?$";
		private const string REGEX_ddTmmTyyyy = "^(?=\\\\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\\\\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\\\\x20|$))|(?:2[0-8]|1\\\\d|0?[1-9]))([.])(?:1[012]|0?[1-9])\\\\1(?:1[6-9]|[2-9]\\\\d)\\\\d\\\\d(?:(?=\\\\x20\\\\d)\\\\x20|$))?(((0?[1-9]|1[012])(:[0-5]\\\\d){0,2}(\\\\x20[AP]M))|([01]\\\\d|2[0-3])(:[0-5]\\\\d){1,2})?$";
		private const string REGEX_mmSddSyyyy = "";
		private const string REGEX_ddSmmmSyyyy = "";
		
		#endregion

		#region Private Variables

		private bool _bHasDropDown;
		private bool _bEnabled;
		private bool _canPostBack;

		private DateTime _dtValue;

		private string _strTargetForm;
		private string _strTargetTextBox;
		private string _strTargetTemporaryFocus;
		private string _strCssClassTextBox;
		private string _strRegexValue;
		private string _strErrorMessage;
		private string _strUrlImage;
		private string _strFormatDate;
		private string _strStyle;
		private string _strDateDelimiter;
		private string _strKeyCodeCheckSyntax;
		private string _strMostUsedKeyCode;
		private string _strScriptOnFocusOut;

		private int _intHeight;
		private int _intTextBoxWidth;
		private int _intButtonWidth;
		private int _intTabIndex;

		/// <summary>int bit set of 2**DayOfWeek enum, default to all enabled</summary>
		private int _disabledDayOfWeek = 0x0;

		#endregion

		#region Constructors/Destructors/Finalizers

		/// <summary>
		/// Initializes a new instance of KTB.DNet.WebCC.IntiCalendar class
		/// </summary>
		public IntiCalendar ()
		{
			Array.Copy(CultureInfo.CurrentCulture.DateTimeFormat.MonthNames,0,this.longMonthOfYear,0,12);
			this._bHasDropDown = true;
			this._canPostBack = false;
			this._bEnabled = true;
			this._dtValue = DateTime.Now;
			this._strTargetForm = "";
			this._strTargetTextBox = "";
			this._strTargetTemporaryFocus = "";
			this._strScriptOnFocusOut = "";
			//this._strTargetTextBox = IntiCalendar.TEXTBOX_NAME + this.GetHashCode().ToString();
			this._strErrorMessage = IntiCalendar.DEFAULT_VALUE_ERROR_MESSAGE;
			this._strFormatDate = IntiCalendarFormatDateConverter.DEFAULT_FORMAT;
			this._strDateDelimiter = IntiCalendarFormatDateConverter.DEFAULT_DELIMITER;
			this._strKeyCodeCheckSyntax = IntiCalendarFormatDateConverter.DEFAULT_KEYCODE_CHECK;
			this._strMostUsedKeyCode = IntiCalendarFormatDateConverter.DEFAULT_KEYCODE_MOSTUSED;
			this._intHeight = IntiCalendar.DEFAULT_VALUE_HEIGHT;
			this._intTextBoxWidth = IntiCalendar.DEFAULT_VALUE_TEXTBOX_WIDTH;
			this._intButtonWidth = IntiCalendar.DEFAULT_VALUE_BUTTON_WIDTH;
			this._strUrlImage = IntiCalendar.DEFAULT_VALUE_URL_IMAGE;
			this._strRegexValue = IntiCalendar.REGEX_ddSmmSyyyy;
		}

		#endregion

		#region Public Methods and Properties

		#region Public Properties

		/// <summary>
		/// A script will be run after loose focus.
		/// This property must be fill with "functionname(param)" without quotation and param is additional
		/// </summary>
		public string ScriptOnFocusOut
		{
			get 
			{ 
				return _strScriptOnFocusOut; 
			}
			set { _strScriptOnFocusOut = value; }
		}
		
		/// <summary>
		/// Enable you to select this day of week if true
		/// </summary>
		public bool Sunday 
		{ 
			get 
			{ 
				return (_disabledDayOfWeek & SUNDAY) == 0; 
			}
			set 
			{
				_disabledDayOfWeek = (_disabledDayOfWeek&~SUNDAY) | ((value)?0:SUNDAY);
			}
		}

		/// <summary>
		/// Enable you to select this day of week if true
		/// </summary>
		public bool Monday 
		{ 
			get 
			{ 
				return (_disabledDayOfWeek & MONDAY) == 0; 
			}
			set 
			{
				_disabledDayOfWeek = (_disabledDayOfWeek&~MONDAY) | ((value)?0:MONDAY);
			}
		}

		/// <summary>
		/// Enable you to select this day of week if true
		/// </summary>
		public bool Tuesday 
		{ 
			get 
			{ 
				return (_disabledDayOfWeek & TUESDAY) == 0; 
			}
			set 
			{
				_disabledDayOfWeek = (_disabledDayOfWeek&~TUESDAY) | ((value)?0:TUESDAY);
			}
		}

		/// <summary>
		/// Enable you to select this day of week if true
		/// </summary>
		public bool Wednesday 
		{ 
			get 
			{ 
				return (_disabledDayOfWeek & WEDNESDAY) == 0; 
			}
			set 
			{
				_disabledDayOfWeek = (_disabledDayOfWeek&~WEDNESDAY) | ((value)?0:WEDNESDAY);
			}
		}

		/// <summary>
		/// Enable you to select this day of week if true
		/// </summary>
		public bool Thursday 
		{ 
			get 
			{ 
				return (_disabledDayOfWeek & THURSDAY) == 0; 
			}
			set 
			{
				_disabledDayOfWeek = (_disabledDayOfWeek&~THURSDAY) | ((value)?0:THURSDAY);
			}
		}

		/// <summary>
		/// Enable you to select this day of week if true
		/// </summary>
		public bool Friday 
		{ 
			get 
			{ 
				return (_disabledDayOfWeek & FRIDAY) == 0; 
			}
			set 
			{
				_disabledDayOfWeek = (_disabledDayOfWeek&~FRIDAY) | ((value)?0:FRIDAY);
			}
		}

		/// <summary>
		/// Enable you to select this day of week if true
		/// </summary>
		public bool Saturday 
		{ 
			get 
			{ 
				return (_disabledDayOfWeek & SATURDAY) == 0; 
			}
			set 
			{
				_disabledDayOfWeek = (_disabledDayOfWeek&~SATURDAY) | ((value)?0:SATURDAY);
			}
		}


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

		public string TargetTemporaryFocus
		{
			get
			{
				return _strTargetTemporaryFocus;
			}
			set
			{
				_strTargetTemporaryFocus = value;
			}
		}

		/// <summary>
		/// CssClass of the TextBox 
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
		/// Insert regex into the TextBox to validate datetime. 
		/// </summary>
		[DefaultValue(IntiCalendar.REGEX_ddSmmSyyyy)]
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
		/// Insert regex into the TextBox to validate datetime. 
		/// </summary>
		[DefaultValue(IntiCalendar.DEFAULT_VALUE_ERROR_MESSAGE)]
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

		[DefaultValue(IntiCalendar.DEFAULT_VALUE_URL_IMAGE)]
		public string UrlImage
		{
			get
			{
				return _strUrlImage;
			}
			set
			{
				_strUrlImage = value;
			}
		}

		/// <summary>
		/// Whether the month and the year will be shown in DropDownList or not
		/// Default value is <c>True</c>
		/// </summary>
		[DefaultValue(IntiCalendar.DEFAULT_VALUE_HASDROPDOWN)]
		public bool HasDropDown
		{
			get
			{
				return _bHasDropDown;
			}
			set
			{
				_bHasDropDown = value;
			}
		}

		/// <summary>
		/// This property is used to define whether this such of control can do postback or not
		/// </summary>
		public bool CanPostBack
		{
			get
			{
				return _canPostBack;
			}
			set
			{
				_canPostBack = value;
			}
		}

		/// <summary>
		/// Gets or sets the format of the date displayed in the TextBox
		/// </summary>
		[TypeConverter(typeof(IntiCalendarFormatDateConverter))]
		[DefaultValue(IntiCalendarFormatDateConverter.DEFAULT_FORMAT)]
		public string FormatDate
		{
			get 
			{
				return _strFormatDate;
			}
			set
			{
//				_strFormatDate = value;
				switch (value)
				{
					case "dd-MMM-yyyy" : { _strRegexValue = REGEX_dd_mmm_yyyy; _strFormatDate = value; break; }
					case "dd-MM-yyyy" : { _strRegexValue = REGEX_dd_mm_yyyy; _strFormatDate = value; break; }
					case "yyyy-MM-dd" : { _strRegexValue = REGEX_yyyy_mm_dd; _strFormatDate = value; break; }
					case "dd/MM/yyyy" : { _strRegexValue = REGEX_ddSmmSyyyy; _strFormatDate = value; break; }
					case "MM/dd/yyyy" : { _strRegexValue = REGEX_mmSddSyyyy; _strFormatDate = value; break; }
					case "dd/MMM/yyyy" : { _strRegexValue = REGEX_ddSmmmSyyyy; _strFormatDate = value; break; }
					case "dd.MM.yyyy" : { _strRegexValue = REGEX_ddTmmTyyyy; _strFormatDate = value; break; }
					default:
						// Throw exception
						if (this.Context != null)
						{
							throw new Exception("IntiCalendarFormatDateConverter doesn't contain a definition for " + value);
						}
						break;
				}
			}
		}

		/// <summary>
		/// Gets or sets the caption displayed in the
		/// System.Web.UI.WebControls.Literal control
		/// </summary>
		[Browsable(false)]
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

		/// <summary>
		/// Gets or sets the date shown in the TextBox
		/// </summary>
		public DateTime Value
		{
			get
			{
				return this._dtValue;
			}
			set
			{
				this._dtValue = value;
			}
		}

		/// <summary>
		/// Gets or sets the Style of this Control
		/// </summary>
		public string Style
		{
			get
			{
				return this._strStyle;
			}
			set
			{
				this._strStyle = value;
			}
		}

		/// <summary>
		/// Enable or disable this control
		/// </summary>
		[DefaultValue(IntiCalendar.DEFAULT_VALUE_ENABLED)]
		public bool Enabled
		{
			get
			{				
				return this._bEnabled;
			}
			set
			{
				this._bEnabled = value;
			}
		}

		/// <summary>
		/// Gets or sets the height of this control
		/// </summary>
		[DefaultValue(IntiCalendar.DEFAULT_VALUE_HEIGHT)]
		public int Height
		{
			get
			{
				return this._intHeight;
			}
			set
			{
				this._intHeight = value;
			}
		}
		// 05-Sep-2007	Deddy H		Menambahkan properties TabIndex
		[DefaultValue(IntiCalendar.DEFAULT_VALUE_TABINDEX)]
		public int TabIndex
		{
			get
			{
				return this._intTabIndex;
			}
			set
			{
				this._intTabIndex = value;
			}
		}

		/// <summary>
		/// Gets or sets the width of TextBox inside this control
		/// </summary>
		[DefaultValue(IntiCalendar.DEFAULT_VALUE_TEXTBOX_WIDTH)]
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
		/// Gets or sets the width of Button inside this control
		/// </summary>
		[DefaultValue(IntiCalendar.DEFAULT_VALUE_BUTTON_WIDTH)]
		public int ButtonWidth
		{
			get
			{
				return this._intButtonWidth;
			}
			set
			{
				this._intButtonWidth = value;
			}
		}

		#endregion

		#region Public Methods
		#endregion

		#endregion

		#region Protected Methods and Properties

		#region Protected Properties
		#endregion

		#region Protected Methods

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void OnPreRender(System.EventArgs e)
		{
			if (Page.IsClientScriptBlockRegistered("CheckCalendar"))
			{

			}
			else
			{


				Page.RegisterClientScriptBlock("CheckCalendar", "");
				
				LiteralControl obj = new LiteralControl();

				Assembly assembly = this.GetType().Assembly;
				Stream stream =	assembly.GetManifestResourceStream(this.GetType(), "IntiCalendar.js");
				StreamReader reader = new StreamReader(stream);
				string js = reader.ReadToEnd();
				js = js.Replace("@LONG_DAY_OF_WEEK@", ToJsArray(longDayOfWeek));
				
				if (CultureInfo.CurrentCulture.Name == "id-ID" || CultureInfo.CurrentCulture.Name == "id")
				{ js = js.Replace("@SHORT_DAY_OF_WEEK@", ToJsArray(shortHari)); }
				else { js = js.Replace("@SHORT_DAY_OF_WEEK@", ToJsArray(shortDayOfWeek)); }
				
				js = js.Replace("@LONG_MONTH_OF_YEAR@", ToJsArray(longMonthOfYear));
				obj.Text = js;

				((LiteralControl)Page.Controls[0]).Text += obj.Text;

//				base.OnPreRender(e);
				}
		}
				
		protected override void Render(HtmlTextWriter output)
		{
			StringBuilder objRenderCal = new StringBuilder();
			//			objRenderCal.Append("<TABLE cellSpacing=\"0\" cellPadding=\"0\" border=\"0\" ID=\"Table1\">");
			//			objRenderCal.Append("<TR>");
			//			objRenderCal.Append("<TD><INPUT size=\"11\" name=\"ex5\" ID=\"Text1\" type=\"Text\"></TD>");
			//			objRenderCal.Append("<TD><A onmouseover=\"if (timeoutId) clearTimeout(timeoutId);window.status=\'Show Calendar\';return true;\"");
			//			objRenderCal.Append("onclick=\"g_Calendar.show(event,\'frm.ex5\',true,\'yyyy-mm-dd\'); return false;\" onmouseout=\"if (timeoutDelay) calendarTimeout();window.status=\'\';\"");
			//			objRenderCal.Append("href=\"javascript:%20void(0);\"><IMG height=\"21\" alt=\"\" src=\"images/calendar.gif\" width=\"34\" border=\"0\" name=\"imgCalendar\"></A></TD>");
			//			objRenderCal.Append("</TR>");
			//			objRenderCal.Append("</TABLE>");

//			if (this._strTargetForm.Trim() == "")
//			{
//				foreach (Control objControl in this.Page.Controls)
//				{
//					if (objControl is HtmlForm)
//					{
//						this._strTargetForm = ((HtmlForm)objControl).Name;
//						break;
//					}
//				}
//			}
//			
//			if (this._strTargetTextBox.Trim() == "")
//			{
//				this._strTargetTextBox = IntiCalendar.TEXTBOX_NAME + this.GetHashCode().ToString();
//			}
			// 21 July 2005
			// There's an exception for a callback function generated calendar, the saveviewstate & loadviewstate events are never called
			if (this.ViewState[VW_KEY_TARGETFORM] == null)
			{
				this.SaveViewState();
			}

			if (this.ViewState[VW_KEY_TARGETFORM] != null)
			{
				this._strTargetForm = this.ViewState[VW_KEY_TARGETFORM].ToString();
				this._strTargetTextBox = this.ViewState[VW_KEY_TARGETTEXTBOX].ToString();
				this._strTargetTemporaryFocus = this.ViewState[VW_KEY_TARGETTEMPORARYFOCUS].ToString();
			}

			// 21 July 2005
			// There's an exception for a callback function generated calendar, the saveviewstate & loadviewstate events are never called
			string strTarget = "";
			if (this._strTargetForm != "")
			{
				strTarget = this._strTargetForm + "." + this._strTargetTextBox;
                strTarget = "getElementById(\\'" + this._strTargetTextBox + "\\')";
			}
			else
			{
				strTarget = "getElementById(\\'" + this._strTargetTextBox + "\\')";
			}

			//default keycode allowed
			
			switch (this._strFormatDate){ 
				case "dd-MMM-yyyy": 
					this._strDateDelimiter = "-";
					this._strKeyCodeCheckSyntax = this._strMostUsedKeyCode + " || " + "(pressedKey == 45) || (pressedKey == 189 && shiftKey == false) || (pressedKey >= 48 && pressedKey <= 57 && shiftKey == false && altKey == false && ctrlKey == false) || (pressedKey >= 65 && pressedKey <= 90) || (pressedKey >= 97 && pressedKey <= 122)";
					break;
				case "dd-MM-yyyy": 
				case "yyyy-MM-dd": 
					this._strDateDelimiter = "-"; 
					this._strKeyCodeCheckSyntax = this._strMostUsedKeyCode + " || " + "(pressedKey == 45) || (pressedKey == 189 && shiftKey == false) || (pressedKey >= 48 && pressedKey <= 57 && shiftKey == false && altKey == false && ctrlKey == false) || (pressedKey >= 96 && pressedKey <= 105 && shiftKey == false && altKey == false && ctrlKey == false)";
					break; 
				case "dd/MM/yyyy": 
				case "MM/dd/yyyy": 
					this._strDateDelimiter = "/";
					this._strKeyCodeCheckSyntax = this._strMostUsedKeyCode + " || " +  "(pressedKey == 47) || (pressedKey == 111) || (pressedKey == 191 && shiftKey == false) || (pressedKey >= 48 && pressedKey <= 57 && shiftKey == false && altKey == false && ctrlKey == false) || (pressedKey >= 96 && pressedKey <= 105 && shiftKey == false && altKey == false && ctrlKey == false)";
					break;
				case "dd/MMM/yyyy": 
					this._strDateDelimiter = "/"; 
					this._strKeyCodeCheckSyntax = this._strMostUsedKeyCode + " || " +  "(pressedKey == 47) || (pressedKey == 111) || (pressedKey == 191 && shiftKey == false) || (pressedKey >= 48 && pressedKey <= 57 && shiftKey == false && altKey == false && ctrlKey == false) || (pressedKey >= 65 && pressedKey <= 90) || (pressedKey >= 97 && pressedKey <= 122)";
					break;
				case "dd.MM.yyyy": 
					this._strDateDelimiter = ".";
					this._strKeyCodeCheckSyntax = this._strMostUsedKeyCode + " || " +  "(pressedKey == 46) || (pressedKey == 111) || (pressedKey == 191 && shiftKey == false) || (pressedKey >= 48 && pressedKey <= 57 && shiftKey == false && altKey == false && ctrlKey == false) || (pressedKey >= 96 && pressedKey <= 105 && shiftKey == false && altKey == false && ctrlKey == false)";
					break;
			} 

			// 28 Nov 2005 
			// Adding function disableEnter and adding function to add zero (0) before 1 digit numeric (1-9), either day or month
			if (!this.Page.IsClientScriptBlockRegistered("ValidateCalendar"))
			{
				objRenderCal.Append("<script language=\"javascript\"> \n");
				objRenderCal.Append("lastSavedDate=\""+this._dtValue.ToString(this.StandardFormatDate)+"\"; \n");
				objRenderCal.Append("function ValidateCalendar(txtCalendar) { \n");
				objRenderCal.Append(" txtCalendar.value=Trim(txtCalendar.value);\n");
				objRenderCal.Append(" if (txtCalendar.value == \"\") \n");
				objRenderCal.Append("   return true;\n");
				objRenderCal.Append("	var re = new RegExp(\"" + this._strRegexValue + "\"); \n");
				objRenderCal.Append("	if (re == \" \") { \n");
				objRenderCal.Append("		return true; }\n");
				objRenderCal.Append("	else { \n");
				objRenderCal.Append("		if (txtCalendar.value.match(re)) { \n");
				objRenderCal.Append("			return true; } \n");
				objRenderCal.Append("		else { \n");
				objRenderCal.Append("			return false; } \n");
				objRenderCal.Append("	} \n");
				objRenderCal.Append("} \n");
				objRenderCal.Append("function checkKeyCode() { \n");
				objRenderCal.Append("   var pressedKey = event.keyCode; \n");
				objRenderCal.Append("	var shiftKey = event.shiftKey; \n");
				objRenderCal.Append("	var altKey = event.altKey; \n");
				objRenderCal.Append("	var ctrlKey = event.ctrlKey; \n");
				objRenderCal.Append("	if (" + this._strKeyCodeCheckSyntax + ") return true; \n ");
				objRenderCal.Append("	event.returnValue = false \n");
				objRenderCal.Append("} \n");
				objRenderCal.Append("function padZero(num) {  \n");
				objRenderCal.Append("	return ((num <= 9) ? (\"0\" + num) : num);  \n");
				objRenderCal.Append("}  \n");
				objRenderCal.Append("function addingZero(dateString) { \n");
				objRenderCal.Append("	if (dateString == \"\") return dateString; \n");
				objRenderCal.Append("   var tempParam = dateString.split('" + this._strDateDelimiter + "'); \n");
				objRenderCal.Append("	if (tempParam.length == 0) return dateString; \n");
				objRenderCal.Append("   var result = \"\";	 \n");
				objRenderCal.Append("   for(var x=0;x<tempParam.length;x++) { \n");
				objRenderCal.Append("		if (tempParam[x].length == 1) {\n");
				objRenderCal.Append("           result = result + padZero(parseInt(tempParam[x]));  \n");
				objRenderCal.Append("		}\n");
				objRenderCal.Append("		else \n");
				objRenderCal.Append("           result = result + tempParam[x];	 \n");
				objRenderCal.Append("       if (x < tempParam.length - 1) \n");
				objRenderCal.Append("		    result = result + \"" + this._strDateDelimiter + "\"; \n");
				objRenderCal.Append("    }	\n");
				objRenderCal.Append("    return result; \n");
				objRenderCal.Append("} \n");
				objRenderCal.Append("function checkZero(txtCal) { \n");
				objRenderCal.Append("   if (txtCal.value.length > 0 && txtCal.value.length < 10) \n");
				objRenderCal.Append("      txtCal.value = addingZero(txtCal.value); \n");
				objRenderCal.Append("} \n");
				objRenderCal.Append("function calFocus(txtCalendar) { \n");
				objRenderCal.Append("	if (txtCalendar.disabled) { txtCalendar.blur();return; } \n ");
				objRenderCal.Append("		txtCalendar.focus(); \n");
				objRenderCal.Append("		txtCalendar.select(); \n");
				objRenderCal.Append("} \n");
				objRenderCal.Append("function ResetDateValue(obj){ \n");
				objRenderCal.Append("   d = new Date(); \n");				
				objRenderCal.Append("	obj.value = lastSavedDate \n");
				objRenderCal.Append("} \n");
				objRenderCal.Append("function calBlur(txtCalendar, txtTemporaryFocus, onFocusOutEvent) {\n");
				objRenderCal.Append("	var curCalendar = document.getElementById(txtCalendar);\n");
				objRenderCal.Append("	if (ValidateCalendar(curCalendar)) { \n");
				objRenderCal.Append("		if (curCalendar.value.length > 0) { \n");
				objRenderCal.Append("			checkZero(curCalendar); \n");
				objRenderCal.Append("			lastSavedDate = curCalendar.value; \n");
				objRenderCal.Append("		if (onFocusOutEvent != null) onFocusOutEvent;\n");
				if (_canPostBack)
				{
					objRenderCal.Append("			__doPostBack(txtCalendar,''); \n");
				}
				objRenderCal.Append("		} \n");
				objRenderCal.Append("	}\n");
				objRenderCal.Append("	else {\n");
				objRenderCal.Append("		alert('" + this._strErrorMessage + "'); \n");
				objRenderCal.Append("		ResetDateValue(curCalendar); \n");
				objRenderCal.Append("		if (!document.getElementById(txtTemporaryFocus).disabled) {\n");
				objRenderCal.Append("			document.getElementById(txtTemporaryFocus).focus(); \n");
				objRenderCal.Append("			document.getElementById(txtTemporaryFocus).select(); \n");	
				objRenderCal.Append("		} \n");
				objRenderCal.Append("	} \n");
				objRenderCal.Append("} \n");
				objRenderCal.Append("</script>\n\n");
			}
			objRenderCal.Append("<TABLE cellSpacing=\"0\" cellPadding=\"0\" border=\"0\" ID=\"Table1\"");
			if (this.Context != null)
			{
				objRenderCal.Append(" style=\"" + this._strStyle + "\">\n");
			}
			else
			{
				objRenderCal.Append(">\n");
			}
			objRenderCal.Append("<TR>\n");
			objRenderCal.Append("<TD>\n");
			objRenderCal.Append("<INPUT name=\"" + _strTargetTemporaryFocus + "\" ID=\"" +_strTargetTemporaryFocus+ "\" type=\"Text\" style=\"width:0;border:0;\"  onfocus=\"calFocus(document.getElementById('" + this._strTargetTextBox + "'))\">\n");
			objRenderCal.Append("</TD><TD> \n");			
			objRenderCal.Append("<INPUT size=\"11\" name=\"" + _strTargetTextBox + "\" ID=\"" + _strTargetTextBox + "\" type=\"Text\" ");
			objRenderCal.Append(
				"style=\"width:" + this._intTextBoxWidth.ToString() + "px; " +
				"height:" + this._intHeight.ToString() + "px\" ");
			if (!this.Enabled)
			{
				objRenderCal.Append("disabled=\"" + this.HTMLDisabled + "\" ");
			}
			objRenderCal.Append("value=\"" + (this._dtValue!=new DateTime()?this._dtValue.ToString(
				(this.StandardFormatDate)):String.Empty) + "\"\n");
			// 05-Sep-2007		Deddy H		Add TabIndexProperties
			objRenderCal.Append("tabindex=\"" + this._intTabIndex.ToString() + "\"\n");
			if (this._strScriptOnFocusOut == string.Empty)
			{
				objRenderCal.Append(" onblur=\"calBlur('"+ this._strTargetTextBox+"','"+ this._strTargetTemporaryFocus+"',null)\"");
			}
			else
			{
				objRenderCal.Append(" onblur=\"calBlur('"+ this._strTargetTextBox+"','"+ this._strTargetTemporaryFocus+"',"+ this._strScriptOnFocusOut +")\"");
			}
			/*objRenderCal.Append(
				" onblur=\"if (ValidateCalendar(document.getElementById('" + this._strTargetTextBox + "'))) { \n" + 
				"    if (this.value.length > 0) { \n" +
				"		checkZero(document.getElementById('" + this._strTargetTextBox + "')); \n" +
				"		lastSavedDate = this.value; \n");
			if (_canPostBack)
			{
				objRenderCal.Append("__doPostBack('"+this._strTargetTextBox+"',''); \n");
			}
			objRenderCal.Append(
				"	 } \n" +
				" }\n" +
				" else {" + 
				" alert('" + this._strErrorMessage + "'); \n"+
				" ResetDateValue(this); \n" +
				" document.getElementById('_temporaryFocus').focus(); \n"+	
				" document.getElementById('_temporaryFocus').select(); \n"+	
				"}\" \n" +*/
			objRenderCal.Append(
				" onkeydown=\"checkKeyCode()\" " +
				" class=\"" + this._strCssClassTextBox + "\"></TD> ");
			objRenderCal.Append("<TD><A ");

			if (this.Enabled)
			{
				objRenderCal.Append(" onmouseover=\"if (timeoutId) clearTimeout(timeoutId);window.status=\'Show Calendar\';return true;\"");
				string disabled = "";
				for (int i=0; i<7; i++ )
				{
					if ((_disabledDayOfWeek & (1<<i))!=0) 
					{
						disabled += longDayOfWeek[i];
						disabled += ",";
					}
				}
				if (disabled!="") 
				{
					disabled = disabled.Substring(0, disabled.Length-1);
				}
				disabled = "'" + disabled + "'";
				//objRenderCal.Append(" onclick=\"if (Validate()) { g_Calendar.show(event,\'" + _strTargetForm + "." + _strTargetTextBox + "\'," + _bHasDropDown.ToString().ToLower() + ",\'" + _strFormatDate + "\'" + "," + disabled +  "); } else { alert('" + this._strErrorMessage + "'); } \n");
				objRenderCal.Append(" onclick=\"g_Calendar.show(event,\'" + strTarget + "\'," + _bHasDropDown.ToString().ToLower() + ",\'" + _strFormatDate + "\'" + "," + disabled +  ");\" \n");
				objRenderCal.Append(" onmouseout=\"if (timeoutDelay) calendarTimeout();window.status=\'\';\"");
			}
			objRenderCal.Append("href=\"javascript:%20void(0);\"><IMG height=\"" + this._intHeight.ToString() + "\" alt=\"\" src=\"" + this._strUrlImage + "\" width=\"" + this._intButtonWidth.ToString() + "\" border=\"0\" name=\"imgCalendar\"></A></TD>");
			
			objRenderCal.Append("</TR>");
			objRenderCal.Append("</TABLE>");

			output.Write(objRenderCal);
		}

		/// <summary>
		/// Saves any server control view-state changes that have occurred since
		/// the time the page was posted back to the server
		/// </summary>
		/// <returns></returns>
		protected override object SaveViewState() 
		{
			// setting default value
			// JA
			// 28 June 2005
			if (!this.Page.IsPostBack || this.ViewState[VW_KEY_TARGETFORM] == null)
			{
				this.TargetTextBox = IntiCalendar.TEXTBOX_NAME + this.GetHashCode().ToString();
				this.TargetTemporaryFocus = IntiCalendar.TEMPORARYFOCUS_NAME + this.GetHashCode().ToString();
				foreach (Control objControl in this.Page.Controls)
				{
					if (objControl is HtmlForm && objControl.ID != null)
					{
						this.TargetForm = ((HtmlForm)objControl).Name;
						break;
					}
				}
			}

			base.SaveViewState();
			this.ViewState.Add(VW_KEY_HASDROPDOWN, this.HasDropDown);
			this.ViewState.Add(VW_KEY_TARGETFORM, this.TargetForm);
			this.ViewState.Add(VW_KEY_TARGETTEXTBOX, this.TargetTextBox);
			this.ViewState.Add(VW_KEY_TARGETTEMPORARYFOCUS, this.TargetTemporaryFocus);
			this.ViewState.Add(VW_KEY_FORMATDATE, this.FormatDate);
			this.ViewState.Add(VW_KEY_VALUE, this.Value);
			this.ViewState.Add(VW_KEY_ENABLED, this.Enabled);

			return ((IStateManager)this.ViewState).SaveViewState();
		}

		/// <summary>
		/// Restores view-state information from a previous page request that was
		/// saved by the System.Web.UI.Control.SaveViewState method
		/// </summary>
		/// <param name="savedState"></param>
		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null) 
			{
				base.LoadViewState(savedState);
				((IStateManager)this.ViewState).LoadViewState(savedState);

				this.HasDropDown = (bool)this.ViewState[VW_KEY_HASDROPDOWN];
				this.TargetForm = this.ViewState[VW_KEY_TARGETFORM].ToString();
				this.TargetTextBox = this.ViewState[VW_KEY_TARGETTEXTBOX].ToString();
				this.TargetTemporaryFocus = this.ViewState[VW_KEY_TARGETTEMPORARYFOCUS].ToString();
				this.FormatDate = this.ViewState[VW_KEY_FORMATDATE].ToString();
				this.Enabled = (bool)this.ViewState[VW_KEY_ENABLED];
				if (this.Context.Request.Form != null &&
					this.Context.Request.Form[this.TargetTextBox] != null)
				{
					//this.Value = DateTime.Parse(this.Context.Request.Form[this.TargetTextBox]);					
					DateTimeFormatInfo objFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
					string o = this.Context.Request.Form[this.TargetTextBox];
					DateTime objDatetime = new DateTime();
					if (o.Trim() != string.Empty)
					{
						objDatetime = DateTime.ParseExact(o,this.FormatDate,objFormatInfo);
					}
					this.Value = objDatetime;
				}
				else
				{
					this.Value = (DateTime)this.ViewState[VW_KEY_VALUE];

				}
			}
		}
		#endregion

		#endregion

		#region Private Methods and Properties

		#region Private Properties

		/// <summary>
		/// Gets a .Net standard date format string
		/// </summary>
		private string StandardFormatDate
		{
			get
			{
				return this.FormatDate.Replace('m', 'M');
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

		#endregion
		
		#region Private Methods
		/// <summary>
		/// convert C# array into javascript array
		/// string[] test = {"a", "b", "c"};
		/// converted into
		/// ('a', 'b', 'c')
		/// the array content is limited to alphanumeric
		/// please don't put special character
		/// because it will be rendered to the client as javascript
		/// </summary>
		/// <param name="arr">array to be converted</param>
		/// <returns>the converted string</returns>
		private string ToJsArray(string[] arr) 
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("(");
			for (int i=0; i<arr.Length; i++) 
			{
				if (i!=0) 
				{
					sb.Append(", ");
				}
				sb.Append("'" + System.Web.HttpUtility.HtmlEncode(arr[i]) + "'");
			}
			sb.Append(")");
			return sb.ToString();
		}
		#endregion

		#endregion
	}
}
