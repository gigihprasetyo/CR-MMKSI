#region Summary comment
////////////////////////////////////////////////////////////////////////////////
/// AUTHOR        : Ka Hoat, Lie
/// CREATE DATE   : Tue Jun  8 17:11:18 SE Asia Standard Time 2004
/// PURPOSE       : Skeleton of IntiMoneyTextBox
/// SPECIAL NOTES :
/// ============================================================================
/// $History: $
/// 
////////////////////////////////////////////////////////////////////////////////
#endregion
#region QA Section
////////////////////////////////////////////////////////////////////////////////
/// IntiMoneyTextBox behavior :
/// - format as you type : 2 digit for cents, separate every thousands
/// - support several negative format
/// sample : ( | -> caret )
/// typed	result
///			      0.00|
///	1		      0.01|
///	2		      0.12|
///	5		      1.25|
///	4		     12.54|
///	x		     12.54|	-> reject other character
///	bksp	      1.25|
///	0		     12.50|
///	0		    125.00|
///	0		  1,250.00|
///	0		 12,500.00|
///	truncate to cents
////////////////////////////////////////////////////////////////////////////////
/// KH Q : allow editing beyond backspace ? -> cut, paste, home+delete, etc.
/// JA A : allow normal editing as the above spec, 
///        invalid character, modified in unexpected way, 
///        alert in english on onChange
/// KH Q : i find it hard to implement on copy, cut, paste, delete
///        so, i let them unformatted
////////////////////////////////////////////////////////////////////////////////
#endregion

#region Imports
#region .NET Base Class Namespace Imports
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Reflection;
using System.IO;
#endregion
#region Custom Class or Third Party Namespace Imports
using KTB.DNet.WebCC;
#endregion
#endregion

namespace KTB.DNet.WebCC
{
	/// <summary>
	/// IntiMoneyTextBox is a IntiTextBox that
	/// - only accept numeric input and several special charater
	/// - automaticly formated
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData(
			"<{0}:IntiMoneyTextBox runat=server></{0}:IntiMoneyTextBox>")]
	//TODO: localization: make ,. mil separator and decimal point configurable
	public class IntiMoneyTextBox : IntiTextBox
	{
		#region Private Constants
		#endregion

		#region Private Variables
		#endregion

		#region Constructors/Destructors/Finalizers
		public IntiMoneyTextBox() 
		{
			m_IsNumeric = true;
		}
		#endregion

		#region Interface Implementations
		#endregion

#region Public Methods and Properties

#region Public Properties

		//TODO: use decimal to return value

		[Bindable(true), Category("Behavior"), DefaultValue("")] 
		public override bool IsNumeric
		{
			get
			{
				return m_IsNumeric;
			}
			set
			{
				if (value!=true) 
				{
					throw new Exception("design error: you cannot make a human to be a cow");
				}
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
		protected override void OnPreRender(EventArgs e) 
		{
			Assembly assembly = this.GetType().Assembly;
			Stream stream =	assembly.GetManifestResourceStream(this.GetType(), "IntiMoneyTextBox.js");
			StreamReader reader = new StreamReader(stream);
			string js = "<script type='text/javascript'>" + reader.ReadToEnd() + "</script>";
			Page.RegisterClientScriptBlock("IntiMoneyTextBox", js);
			base.Attributes.Add("onKeyUp", "IntiMoneyTextBox_Change(this, event);");
			base.Attributes.Add("onFocus", "IntiMoneyTextBox_Focus(this, event);");
			base.Style.Add("text-align", "right");
			base.OnPreRender(e);
		}

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output">The HTML writer to write out to</param>
		protected override void Render(HtmlTextWriter output)
		{
			output.WriteLine("<input type='hidden' name='" + this.ID + "_prev' value='" + base.Text + "'/>");
			base.Render(output);
			output.WriteLine();
			output.WriteLine("<script type='text/javascript'>IntiMoneyTextBox_Init(document.forms[0].elements['" + this.ID + "']);</script>");
		}

#endregion

#endregion


		#region Private Methods and Properties

		#region Private Properties
		#endregion
		
		#region Private Methods
		#endregion

		#endregion
#if false
		private string text;
	
		[Bindable(true), Category("Appearance"), DefaultValue("")] 
		public new string Text 
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
			}
		}
#endif

	}
}

