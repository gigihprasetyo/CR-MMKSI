#region Summary 
////////////////////////////////////////////////////////////////////////////////
/// AUTHOR        : Herry Priadi
/// CREATE DATE   : 10 June 2004
/// PURPOSE       : Skeleton of IntiLookUp
/// SPECIAL NOTES :
/// ============================================================================
/// $History: $
/// 1. On July 7, change all code, inherited from DropDownList control. 
///	   For lastest code using CRM design please check history at SourceSafe
/// 
////////////////////////////////////////////////////////////////////////////////
#endregion

#region QA Section
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
#endregion

#region Imports
#region .NET Base Class Namespace Imports
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
#endregion

#region Custom Class or Third Party Namespace Imports
#endregion
#endregion

namespace KTB.DNet.WebCC
{
	/// <summary>
	/// Summary description for IntiDropDownList.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:IntiDropDownList runat=server></{0}:IntiDropDownList>")]
	public class IntiDropDownList : DropDownList
	{
		#region Private Contants
		#endregion

		#region Private Variables

		#endregion

		#region Constructors/Destructors/Finalizers
		#endregion

		#region Public Methods and Properties
		#endregion

		#region Protected Methods and Properties
		#endregion

		#region Private Methods and Properties

		#endregion

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
