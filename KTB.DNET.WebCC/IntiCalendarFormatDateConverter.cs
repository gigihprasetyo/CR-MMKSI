#region Summary
////////////////////////////////////////////////////////////////////////////////
/// AUTHOR        : Julius Anton
/// CREATE DATE   : 18 June 2004
/// PURPOSE       : Type converter for IntiCalendar.FormatDate property
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
	/// Type converter for IntiCalendar.FormatDate property
	/// </summary>
	public class IntiCalendarFormatDateConverter :
		System.ComponentModel.TypeConverter
	{
		#region Private Constants
		#endregion

		#region Public Constants

		public const string DEFAULT_FORMAT = "dd/MM/yyyy";
		public const string DEFAULT_DELIMITER = "/";
		//notify that variable name use in function checkKeyCode is "pressedKey" :) 
		//mostly user can use this keyCode = backspace,shift,ctrl,alt,caps,esc,space,all arrow,ins,del,etc
		public const string DEFAULT_KEYCODE_MOSTUSED = "(pressedKey == 8) || (pressedKey == 9) || (pressedKey >= 16 && pressedKey <= 20) || (pressedKey == 27) || (pressedKey >= 32 && pressedKey <= 40) || (pressedKey >= 44 && pressedKey <= 46)";
		public const string DEFAULT_KEYCODE_CHECK = DEFAULT_KEYCODE_MOSTUSED + " || " +
				"(pressedKey == 47) || (pressedKey == 111) || (pressedKey == 191) || (pressedKey >= 48 && pressedKey <= 57) || (pressedKey >= 96 && pressedKey <= 105)";

		#endregion

		#region Private Variables

		private ArrayList objValues;

		#endregion

		#region Constructors/Destructors/Finalizers

		/// <summary>
		/// Initializes a new instance of
		/// KTB.DNet.WebCC.IntiCalendarFormatDateConverter class
		/// </summary>
		public IntiCalendarFormatDateConverter()
		{
			// Initializes the standard objValues list with defaults.
			objValues = new ArrayList
			(
				new string[]
				{
					"dd-MMM-yyyy",
					"dd-MM-yyyy",
					"yyyy-MM-dd",
					"dd/MM/yyyy",
					"MM/dd/yyyy",
					"dd/MMM/yyyy",
					"dd.MM.yyyy"
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
