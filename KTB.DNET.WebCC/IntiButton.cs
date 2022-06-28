#region Summary
/////////////////////////////////////////////////////
/// AUTHOR			: Halbert Widjaja
/// CREATE DATE		: 8 June 2004
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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
#endregion

namespace KTB.DNet.WebCC
{
	/// <summary>
	/// Summary description for IntiButton.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:IntiButton runat=server></{0}:IntiButton>")]
	public class IntiButton : System.Web.UI.WebControls.Button
	{
//		private string text;
//	
//		[Bindable(true), 
//			Category("Appearance"), 
//			DefaultValue("")] 
//		public string Text 
//		{
//			get
//			{
//				return text;
//			}
//
//			set
//			{
//				text = value;
//			}
//		}

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
//		protected override void Render(HtmlTextWriter output)
//		{
//			output.Write(Text);
//		}

	}
}
