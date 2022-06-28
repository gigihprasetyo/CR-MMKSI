#region Summary comment
////////////////////////////////////////////////////////////////////////////////
/// AUTHOR        : Ka Hoat, Lie
/// CREATE DATE   : Tue Jun  8 17:11:18 SE Asia Standard Time 2004
/// PURPOSE       : Skeleton of IntiPercentTextBox
/// SPECIAL NOTES :
/// ============================================================================
/// $History: $
/// 
////////////////////////////////////////////////////////////////////////////////
#endregion
#region QA Section
////////////////////////////////////////////////////////////////////////////////
/// IntiPercentTextBox behavior :
/// - format as you type
/// sample :
///    0.00|	1
///    0.01|	-
///   -0.01|	0
///   -0.10|	0
///   -1.00|	9
///  -10.09|	0
///  -10.09|	bksp
///   -1.00|	0
///  -10.00|	0
/// -100.00|	home
/// |-100.00	->
/// -|100.00	del
///   -|0.00	9
///  -9|0.00	-
///   9|0.00	end
///   90.00|	9
///---------------- the following is not implemented
///  90.009|	9
///90.009,9|
///---------------- for the following reason
///90.109,9|	home
///|90.109,9	home
///   |10.99	del  -- this might not what is intended
///---------------- so IntiPercentTextBox is limited to two decimal digit
/// and uses the implementation of IntiMoneyTextBox
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
	/// Summary description for IntiPercentTextBox
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData(
			"<{0}:IntiPercentTextBox runat=server></{0}:IntiPercentTextBox>")]
	public class IntiPercentTextBox : IntiTextBox
	{
		public IntiPercentTextBox() 
		{
			m_IsNumeric = true;
		}

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
					throw new Exception("design error: berkatalah pada anak adam : jadilah sapi");
				}
			}
		}


		protected override void OnPreRender(EventArgs e) 
		{
			Assembly assembly = this.GetType().Assembly;

			// IntiPercentTextBox uses two function from IntiMoneyTextBox
			Stream stream1 =	assembly.GetManifestResourceStream(this.GetType(), "IntiMoneyTextBox.js");
			StreamReader reader1 = new StreamReader(stream1);
			string js1 = "<script type='text/javascript'>" + reader1.ReadToEnd() + "</script>";
			Page.RegisterClientScriptBlock("IntiMoneyTextBox", js1);

			Stream stream2 =	assembly.GetManifestResourceStream(this.GetType(), "IntiPercentTextBox.js");
			StreamReader reader2 = new StreamReader(stream2);
			string js2 = "<script type='text/javascript'>" + reader2.ReadToEnd() + "</script>";
			Page.RegisterClientScriptBlock("IntiPercentTextBox", js2);
			base.Attributes.Add("onKeyUp", "IntiPercentTextBox_Change(this, event);");
			base.Attributes.Add("onFocus", "IntiPercentTextBox_Focus(this, event);");
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
			output.WriteLine("<script type='text/javascript'>IntiPercentTextBox_Init(document.forms[0].elements['" + this.ID + "']);</script>");
		}
	}
}

