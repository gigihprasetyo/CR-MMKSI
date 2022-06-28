#region Summary 
////////////////////////////////////////////////////////////////////////////////
/// AUTHOR        : Herry Priadi
/// CREATE DATE   : 9 June 2004
/// PURPOSE       : Skeleton of IntiLookUp
/// SPECIAL NOTES :
/// ============================================================================
/// $History: $
/// 
////////////////////////////////////////////////////////////////////////////////
#endregion

#region QA Section
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
#endregion

#region .NET Base Class Namespace Imports
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Collections;
using System.IO;
#endregion

#region Custom Class or Third Party Namespace Imports
#endregion

namespace KTB.DNet.WebCC
{
	/// <summary>
	/// Summary description for IntiLookUp.
	/// </summary>
	
	//	[DefaultProperty("Text"), 
	//		ToolboxData("<{0}:IntiLookUp runat=server></{0}:IntiLookUp>")]
	[ToolboxData("<{0}:IntiLookUp style=POSITION:absolute runat=server></{0}:IntiLookUp>")]
	public class IntiLookUp : Literal 
	{
		
		#region Private Constants
		
		private const string TEXTBOX_NAME = "LookUp";
		private const int DEFAULT_VALUE_TARGET_WIDTH = 400;
		private const int DEFAULT_VALUE_TARGET_HEIGHT = 400;
		private const int DEFAULT_VALUE_TARGET_TOP = 100;
		private const int DEFAULT_VALUE_TARGET_LEFT = 100;

		#endregion 

		#region Private Variables

		private string _strUrlImage;
		private string _strCssClass;

		private string _strSourceFormName;
		private string _strTargetFile;
		private string _strTargetTextBox;
		private string _strTargetToolbars;
		private string _strTargetScrollbars;
		private string _strTargetStatus;
		private string _strTargetResizable;
		private string _strTargetLocation;
		private string _strTargetMenubars;
		
		private int _intTargetWidth;
		private int _intTargetHeight;
		private int _intTargetTopWindow;
		private int _intTargetLeftWindow;

		private ArrayList _arr;

		#endregion

		#region Constructors/Destructors/Finalizers

		/// <summary>
		/// Initializes a new instance of KTB.DNet.WebCC.IntiLookUp class
		/// </summary>
		public IntiLookUp()
		{
			this._intTargetWidth = IntiLookUp.DEFAULT_VALUE_TARGET_WIDTH;
			this._intTargetHeight = IntiLookUp.DEFAULT_VALUE_TARGET_HEIGHT;
			this._intTargetTopWindow = IntiLookUp.DEFAULT_VALUE_TARGET_TOP;
			this._intTargetLeftWindow = IntiLookUp.DEFAULT_VALUE_TARGET_LEFT;
			this._strSourceFormName = "";
			this._strTargetTextBox = IntiLookUp.TEXTBOX_NAME + this.GetHashCode().ToString();
			this._strTargetLocation = IntiLookUpFeaturesConverter.DEFAULT_FORMAT;
			this._strTargetMenubars = IntiLookUpFeaturesConverter.DEFAULT_FORMAT;
			this._strTargetResizable = IntiLookUpFeaturesConverter.DEFAULT_FORMAT;
			this._strTargetScrollbars = IntiLookUpFeaturesConverter.DEFAULT_FORMAT;
			this._strTargetStatus = IntiLookUpFeaturesConverter.DEFAULT_FORMAT;
			this._strTargetToolbars = IntiLookUpFeaturesConverter.DEFAULT_FORMAT;
		}

		#endregion

		#region Public Methods and Properties

		#region Public Methods
		#endregion

		#region Public Properties

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
		/// Gets or sets the url of image displayed beside the TextBox
		/// </summary>
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
		/// Gets or sets the style of the TextBox
		/// </summary>
		public string CssClass
		{
			get
			{
				return _strCssClass;
			}
			set
			{
				_strCssClass = value;
			}
		}

		/// <summary>
		/// The Name of the Parent Form where the TextBox is placed
		/// If this is left blank, it will be the first form found on the page
		/// </summary>
		public string SourceFormName
		{
			get 
			{
				return _strSourceFormName;
			}
			set
			{
				_strSourceFormName = value;
			}
		}

		/// <summary>
		/// The Name of the TextBox created. Default value is
		/// <c>TextBox + this.GetHashCode().ToString()</c>
		/// </summary>
		[Browsable(false)]
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
		/// Gets or sets the name of file that want to look up
		/// </summary>
		public string TargetFile
		{
			get 
			{
				return _strTargetFile;
			}
			set
			{
				_strTargetFile = value;
			}
		}

		/// <summary>
		/// Gets or sets the width of child window (look up window)
		/// </summary>
		[DefaultValue(IntiLookUp.DEFAULT_VALUE_TARGET_WIDTH)]
		public int TargetWidth
		{
			get
			{
				return _intTargetWidth;
			}
			set
			{
				_intTargetWidth = value;
			}
		}

		/// <summary>
		/// Gets or sets the height of child window (look up window)
		/// </summary>
		[DefaultValue(IntiLookUp.DEFAULT_VALUE_TARGET_HEIGHT)]
		public int TargetHeight
		{
			get
			{
				return _intTargetHeight;
			}
			set
			{
				_intTargetHeight = value;
			}
		}

		/// <summary>
		/// Gets or sets how long from the top of monitor, 
		/// child window (look up window) will be appear 
		/// </summary>
		[DefaultValue(IntiLookUp.DEFAULT_VALUE_TARGET_TOP)]
		public int TargetTopWindow
		{
			get
			{
				return _intTargetTopWindow;
			}
			set
			{
				_intTargetTopWindow = value;
			}
		}

		/// <summary>
		/// Gets or sets how long from the left of monitor, 
		/// child window (look up window) will be appear 
		/// </summary>
		[DefaultValue(IntiLookUp.DEFAULT_VALUE_TARGET_LEFT)]
		public int TargetLeftWindow
		{
			get
			{
				return _intTargetLeftWindow;
			}
			set
			{
				_intTargetLeftWindow = value;
			}
		}

		/// <summary>
		/// Gets or sets the locationbar/addressbar of child window 
		/// (look up window) will be visible or not
		/// </summary>
		[TypeConverter(typeof(IntiLookUpFeaturesConverter))]
		[DefaultValue(IntiLookUpFeaturesConverter.DEFAULT_FORMAT)]
		public string TargetLocation
		{
			get
			{
				return _strTargetLocation;
			}
			set
			{
				_strTargetLocation = value;
			}
		}

		/// <summary>
		/// Gets or sets the menubar of child window (look up window)
		/// will be visible or not
		/// </summary>
		[TypeConverter(typeof(IntiLookUpFeaturesConverter))]
		[DefaultValue(IntiLookUpFeaturesConverter.DEFAULT_FORMAT)]
		public string TargetMenubars
		{
			get
			{
				return _strTargetMenubars;
			}
			set
			{
				_strTargetMenubars = value;
			}
		}

		/// <summary>
		/// Gets or sets the child window (look up window)
		/// will be resizable or not
		/// </summary>
		[TypeConverter(typeof(IntiLookUpFeaturesConverter))]
		[DefaultValue(IntiLookUpFeaturesConverter.DEFAULT_FORMAT)]
		public string TargetResizable
		{
			get
			{
				return _strTargetResizable;
			}
			set
			{
				_strTargetResizable = value;
			}
		}

		/// <summary>
		/// Gets or sets the scrollbar of child window (look up window)
		/// will be visible or not
		/// </summary>
		[TypeConverter(typeof(IntiLookUpFeaturesConverter))]
		[DefaultValue(IntiLookUpFeaturesConverter.DEFAULT_FORMAT)]
		public string TargetScrollbars
		{
			get
			{
				return _strTargetScrollbars;
			}
			set
			{
				_strTargetScrollbars = value;
			}
		}

		/// <summary>
		/// Gets or sets the statusbar of child window (look up window)
		/// will be visible or not
		/// </summary>
		[TypeConverter(typeof(IntiLookUpFeaturesConverter))]
		[DefaultValue(IntiLookUpFeaturesConverter.DEFAULT_FORMAT)]
		public string TargetStatus
		{
			get
			{
				return _strTargetStatus;
			}
			set
			{
				_strTargetStatus = value;
			}
		}

		/// <summary>
		/// Gets or sets the toolbar of child window (look up window)
		/// will be visible or not
		/// </summary>
		[TypeConverter(typeof(IntiLookUpFeaturesConverter))]
		[DefaultValue(IntiLookUpFeaturesConverter.DEFAULT_FORMAT)]
		public string TargetToolbars
		{
			get
			{
				return _strTargetToolbars;
			}
			set
			{
				_strTargetToolbars = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the parameter send from parent window 
		/// to child window (look up window)
		/// </summary>
		[Browsable(false)]
		public ArrayList ParameterList
		{
			get
			{
				return _arr;
			}
			set
			{
				_arr = value;
			}
		}


		#endregion
        		
		#endregion

		#region Protected Methods and Properties
		
		#region Protected Methods

		protected override void OnPreRender(System.EventArgs e)
		{
			StringBuilder objOpenWindowScript = new StringBuilder();

			objOpenWindowScript.Append("<script language=\"javascript\">");
			objOpenWindowScript.Append("function OpenNewWindow(strFormName)");
			objOpenWindowScript.Append("{");
			objOpenWindowScript.Append("var " + this.ID + "childWindow;");
			objOpenWindowScript.Append("" + this.ID + "childWindow = window.open(\'" + _strTargetFile + "");
			objOpenWindowScript.Append("?SourceFormName=\'+strFormName+"); 
			objOpenWindowScript.Append("\'&TargetTextBox=" + _strTargetTextBox + "\',"); 
			objOpenWindowScript.Append("\'" + this.ID + "frmWindow" + "\',");
			objOpenWindowScript.Append("\'width=" + _intTargetWidth+ ",");
			objOpenWindowScript.Append("height=" + _intTargetHeight + ",");
			objOpenWindowScript.Append("top=" + _intTargetTopWindow + ",");
			objOpenWindowScript.Append("left=" + _intTargetLeftWindow + ",");
			objOpenWindowScript.Append("location=" + _strTargetLocation + ",");
			objOpenWindowScript.Append("menubar=" + _strTargetMenubars + ",");
			objOpenWindowScript.Append("toolbar=" + _strTargetToolbars + ",");
			objOpenWindowScript.Append("scrollbars=" + _strTargetScrollbars + ",");
			objOpenWindowScript.Append("status=" + _strTargetStatus + ",");
			objOpenWindowScript.Append("resizable=" + _strTargetResizable + "\');");
			objOpenWindowScript.Append("}");
			objOpenWindowScript.Append("</script>");

			LiteralControl obj = new LiteralControl();
			obj.Text = objOpenWindowScript.ToString();
			((LiteralControl)Page.Controls[0]).Text += obj.Text;
		}

		protected override void Render(HtmlTextWriter output)
		{
			if (this._strSourceFormName.Trim() == "")
			{
				foreach (Control objControl in this.Page.Controls)
				{
					if (objControl is HtmlForm)
					{
						this._strSourceFormName = ((HtmlForm)objControl).Name;
						break;
					}
				}
			}

			StringBuilder objRenderLookUp = new StringBuilder();

			objRenderLookUp.Append("<TABLE cellSpacing=\"0\" cellPadding=\"0\" border=\"0\" ID=\"tblLookUp\">");
			objRenderLookUp.Append("<TR>");
			objRenderLookUp.Append("<TD><input type=\"text\" name=\"" + this.ID + "txtLookUp" + "\"" +
				"id=\"txtLookUp1\" class=\"" + _strCssClass + "\">");
			objRenderLookUp.Append("<TD><A ");
			objRenderLookUp.Append("href=\"javascript:OpenNewWindow(\'" + this._strSourceFormName + "\');\">");
			objRenderLookUp.Append("<img alt=\"Click here for Look Up\" src=\"" + _strUrlImage + "\" border=0></A></TD>");
			objRenderLookUp.Append("</TR>");
			objRenderLookUp.Append("</TABLE>");

			output.Write(objRenderLookUp);
		}

		#endregion

		#region Protected Properties
		#endregion
        		
		#endregion

		#region Private Methods and Properties

		#region Private Methods
		#endregion

		#region Private Properties
		#endregion
		
		#endregion

		
	}
}
