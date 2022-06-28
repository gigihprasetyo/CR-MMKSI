#region Summary
////////////////////////////////////////////////////////////////////////////////
/// AUTHOR        : Herry Priadi
/// CREATE DATE   : July 7, 2004
/// PURPOSE       : Type converter for IntiLookUp property (location, menubar, 
///					toolbar, scrollbars, status, resizable)
/// SPECIAL NOTES :
/// ============================================================================
/// $History: $
/// 
////////////////////////////////////////////////////////////////////////////////
#endregion

#region QA Comments
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

#endregion

#region Custom Class or Third Party Namespace Imports
#endregion

namespace KTB.DNet.WebCC
{
	/// <summary>
	/// Summary description for IntiLookUpFeaturesConverter.
	/// </summary>
	public class IntiLookUpFeaturesConverter : 
		System.ComponentModel.TypeConverter
	{
		#region Private Constants
		#endregion

		#region Public Constants

		public const string DEFAULT_FORMAT = "yes";

		#endregion

		#region Private Variables

		private ArrayList objValues;

		#endregion

		#region Constructors/Destructors/Finalizers

		/// <summary>
		/// Initializes a new instance of
		/// KTB.DNet.WebCC.IntiCalendarFormatDateConverter class
		/// </summary>
		public IntiLookUpFeaturesConverter()
		{
			// Initializes the standard objValues list with defaults.
			objValues = new ArrayList
				(
				new string[]
				{
					"yes",
					"no"
				}
				);
		}

		#endregion

		#region Interface Implementations
		#endregion

		#region Public Methods and Properties

		#region Public Properties
		#endregion

		#region Public Methods

		/// <summary>
		/// Returns whether changing a value on this object requires a call to
		/// System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesSupported
			(System.ComponentModel.ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>
		/// Returns a collection of standard values for the data type this
		/// type converter is designed for when provided with a format context
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override System.ComponentModel.TypeConverter.StandardValuesCollection
			GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
		{
			// Passes the local string array.
			StandardValuesCollection svc =
				new StandardValuesCollection(objValues);       
			return svc;
		}

		/// <summary>
		/// Returns whether this converter can convert an object of the given
		/// type to the type of this converter, using the specified context
		/// </summary>
		/// <param name="context"></param>
		/// <param name="sourceType"></param>
		/// <returns></returns>
		public override bool CanConvertFrom
			(System.ComponentModel.ITypeDescriptorContext context,
			System.Type sourceType)
		{
			if( sourceType == typeof(string) )
				return true;
			else 
				return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>
		/// Converts the given object to the type of this converter, using the
		/// specified context and culture information
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object ConvertFrom
			(System.ComponentModel.ITypeDescriptorContext context,
			System.Globalization.CultureInfo culture, object value)
		{
			if( value.GetType() == typeof(string) )
			{
				// Tests whether new value is already in the list.
				if( !objValues.Contains(value) )
				{
					// If the value is not in list, adds it in order.
					objValues.Add(value);
					objValues.Sort();
				}                                
				// Returns the value to assign to the property.
				return value;
			}
			else
				return base.ConvertFrom(context, culture, value);
		}

		#endregion

		#endregion

		#region Protected Methods and Properties
		#endregion

		#region Private Methods and Properties
		#endregion
	}
}
