#region Summary comment
////////////////////////////////////////////////////////////////////////////////
/// AUTHOR        : Ka Hoat, Lie
/// CREATE DATE   : Tue Jun  8 17:11:18 SE Asia Standard Time 2004
/// PURPOSE       : Skeleton of IntiTextBox
/// SPECIAL NOTES : Numeric TextBox  must allow for empty value and returned
///					as null from Value property
/// ============================================================================
/// $History: $
/// 
////////////////////////////////////////////////////////////////////////////////
#endregion
#region QA Section
////////////////////////////////////////////////////////////////////////////////
/// KH Q : Why Should we have writable IsNumeric property, 
///        why not just make a NumericTextBox and do the checking there
///        because usually a NumericTextBox do not need to be changed 
///        to non-numeric after design
///    A : The interface provide IsNumeric property, but the implementation
///        may provide separated class
/// KH Q : If we embed the javascript in the dll, it is easier to add contols
///        to your page, but there are disadvantages to this trick, that is
///        - caching : the proxy of the client will not cache the javascript
///                    embeded in a generated aspx, control of asp.net uses
///                    C:\Inetpub\wwwroot\aspnet_client\system_web\1_1_4322\.
///                    Whidbey uses WebResource, but that's all i know.
///        - hard to maintain : there is no intelisens, no code guidelines.
///                    the javascript code is mixed in asp.net code.
/// KH Q : IsNumeric behavior reject all character except number
///        but it also reject numeric keypad and editing key
///        lets see if i get caught >:)
///        microsoft put too much non portable feature here
///        they just send key code without proper documentation on the
///        function provided that can help translate the key code to string
///        i wonder if dvorak keyboard send different key code :)
///        i cannot imagine if i have to maintain those code if it was diffrent
/// NS A : check on lost focus to guard for validation
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
	/// IntiTextBox provide the same service as ASP.NET TextBox
	/// but provide strange IsNumeric boolean property that you can set
	/// to change the behavior of the control to reject any character
	/// accept numbers
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData(
			"<{0}:IntiTextBox runat=server></{0}:IntiTextBox>")]
	public class IntiTextBox : System.Web.UI.WebControls.TextBox
	{
		/// <summary>
		/// the default is to allow any character keyed in
		/// </summary>
		public IntiTextBox()
		{
			m_IsNumeric = false;
		}

		/// <summary>
		/// to indicate wether we are currently numeric
		/// It is protected so that inherited classes can set this 
		/// variable directly to indicate the capability of the class
		/// </summary>
		protected bool m_IsNumeric;
		[Bindable(true), Category("Behavior"), DefaultValue("")] 
		public virtual bool IsNumeric
		{
			get
			{
				return m_IsNumeric;
			}
			set
			{
				if (this.GetType()!=typeof(IntiTextBox)) 
				{
					// the result of badly comunicated design
					throw new Exception("Only IntiTextBox.IsNumeric can be set\n" + 
						"If your class want to be able to set also please override IsNumeric set property");
				}
				m_IsNumeric = value;
			}
		}

		protected override void OnPreRender(EventArgs e) 
		{
			if (this.GetType()!=typeof(IntiTextBox)) 
			{
				// the result of badly comunicated design
				base.OnPreRender(e);
				return ;
			}
			base.Attributes.Add("onFocus", "this.select();");
			if (m_IsNumeric) 
			{
				Assembly assembly = this.GetType().Assembly;
				Stream stream =	assembly.GetManifestResourceStream(this.GetType(), "IntiTextBox.js");
				StreamReader reader = new StreamReader(stream);
				string js = "<script type='text/javascript'>" + reader.ReadToEnd() + "</script>";
				Page.RegisterClientScriptBlock("IntiTextBox", js);

				base.Style.Add("text-align", "right");
				base.Attributes.Add("onKeyDown", "IntiTextBox_Numeric_KeyDown(this, event);");
			}
			else 
			{
				base.Style.Remove("text-align");
				base.Attributes.Remove("onKeyDown");
			}
			base.OnPreRender(e);
		}

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output">The HTML writer to write out to</param>
		protected override void Render(HtmlTextWriter output)
		{
			base.Render(output);
		}
	}
}

