
//------------------------------------------------------------------------------
// 
//     This code was generated by a SAP. NET Connector Proxy Generator Version 2.0
//     Created at 12/2/2009
//     Created from Windows
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// 
//------------------------------------------------------------------------------
using System;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using SAP.Connector;

namespace KTB.DNet.NewSAPProxy
{
  /// <summary>
  /// A typed collection of ZKTB_INQ_OUT elements.
  /// </summary>
  [Serializable]
  public class ZKTB_INQ_OUTTable : SAPTable 
  {
  
    /// <summary>
    /// Returns the element type ZKTB_INQ_OUT.
    /// </summary>
    /// <returns>The type ZKTB_INQ_OUT.</returns>
    public override Type GetElementType() 
    {
        return (typeof(ZKTB_INQ_OUT));
    }

    /// <summary>
    /// Creates an empty new row of type ZKTB_INQ_OUT.
    /// </summary>
    /// <returns>The newZKTB_INQ_OUT.</returns>
    public override object CreateNewRow()
    { 
        return new ZKTB_INQ_OUT();
    }
     
    /// <summary>
    /// The indexer of the collection.
    /// </summary>
    public ZKTB_INQ_OUT this[int index] 
    {
        get 
        {
            return ((ZKTB_INQ_OUT)(List[index]));
        }
        set 
        {
            List[index] = value;
        }
    }
        
    /// <summary>
    /// Adds a ZKTB_INQ_OUT to the end of the collection.
    /// </summary>
    /// <param name="value">The ZKTB_INQ_OUT to be added to the end of the collection.</param>
    /// <returns>The index of the newZKTB_INQ_OUT.</returns>
    public int Add(ZKTB_INQ_OUT value) 
    {
        return List.Add(value);
    }
        
    /// <summary>
    /// Inserts a ZKTB_INQ_OUT into the collection at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which value should be inserted.</param>
    /// <param name="value">The ZKTB_INQ_OUT to insert.</param>
    public void Insert(int index, ZKTB_INQ_OUT value) 
    {
        List.Insert(index, value);
    }
        
    /// <summary>
    /// Searches for the specified ZKTB_INQ_OUT and returnes the zero-based index of the first occurrence in the collection.
    /// </summary>
    /// <param name="value">The ZKTB_INQ_OUT to locate in the collection.</param>
    /// <returns>The index of the object found or -1.</returns>
    public int IndexOf(ZKTB_INQ_OUT value) 
    {
        return List.IndexOf(value);
    }
        
    /// <summary>
    /// Determines wheter an element is in the collection.
    /// </summary>
    /// <param name="value">The ZKTB_INQ_OUT to locate in the collection.</param>
    /// <returns>True if found; else false.</returns>
    public bool Contains(ZKTB_INQ_OUT value) 
    {
        return List.Contains(value);
    }
        
    /// <summary>
    /// Removes the first occurrence of the specified ZKTB_INQ_OUT from the collection.
    /// </summary>
    /// <param name="value">The ZKTB_INQ_OUT to remove from the collection.</param>
    public void Remove(ZKTB_INQ_OUT value) 
    {
        List.Remove(value);
    }

    /// <summary>
    /// Copies the contents of the ZKTB_INQ_OUTTable to the specified one-dimensional array starting at the specified index in the target array.
    /// </summary>
    /// <param name="array">The one-dimensional destination array.</param>           
    /// <param name="index">The zero-based index in array at which copying begins.</param>           
    public void CopyTo(ZKTB_INQ_OUT[] array, int index) 
    {
        List.CopyTo(array, index);
	}
  }
}
