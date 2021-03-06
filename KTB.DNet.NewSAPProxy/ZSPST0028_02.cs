
//------------------------------------------------------------------------------
// 
//     This code was generated by a SAP. NET Connector Proxy Generator Version 2.0
//     Created at 11/9/2007
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
using System.Xml.Schema;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using SAP.Connector;

namespace KTB.DNet.NewSAPProxy
{

  /// <summary>
  /// Structure for RFC_CHECK_MATERIAL
  /// </summary>
  [RfcStructure(AbapName ="ZSPST0028_02" , Length = 159)]
  [Serializable]
  public class ZSPST0028_02 : SAPStructure
  {
   

    /// <summary>
    /// Material number
    /// </summary>
 
    [RfcField(AbapName = "MATNR1", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 18, Offset = 0)]
    [XmlElement("MATNR1", Form=XmlSchemaForm.Unqualified)]
    public string Matnr1
    { 
       get
       {
          return _Matnr1;
       }
       set
       {
          _Matnr1 = value;
       }
    }
    private string _Matnr1;


    /// <summary>
    /// Material number
    /// </summary>
 
    [RfcField(AbapName = "MATNR2", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 18, Offset = 18)]
    [XmlElement("MATNR2", Form=XmlSchemaForm.Unqualified)]
    public string Matnr2
    { 
       get
       {
          return _Matnr2;
       }
       set
       {
          _Matnr2 = value;
       }
    }
    private string _Matnr2;


    /// <summary>
    /// Material description
    /// </summary>
 
    [RfcField(AbapName = "MAKTX", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 40, Offset = 36)]
    [XmlElement("MAKTX", Form=XmlSchemaForm.Unqualified)]
    public string Maktx
    { 
       get
       {
          return _Maktx;
       }
       set
       {
          _Maktx = value;
       }
    }
    private string _Maktx;


    /// <summary>
    /// Material group
    /// </summary>
 
    [RfcField(AbapName = "MATKL", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 9, Offset = 76)]
    [XmlElement("MATKL", Form=XmlSchemaForm.Unqualified)]
    public string Matkl
    { 
       get
       {
          return _Matkl;
       }
       set
       {
          _Matkl = value;
       }
    }
    private string _Matkl;


    /// <summary>
    /// Retail Price
    /// </summary>
 
    [RfcField(AbapName = "RTLPR", RfcType = RFCTYPE.RFCTYPE_BCD, Length = 6, Offset = 85)]
    [XmlElement("RTLPR", Form=XmlSchemaForm.Unqualified)]
    public Decimal Rtlpr
    { 
       get
       {
          return _Rtlpr;
       }
       set
       {
          _Rtlpr = value;
       }
    }
    private Decimal _Rtlpr;


    /// <summary>
    /// Stock Description
    /// </summary>
 
    [RfcField(AbapName = "STOCK", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 50, Offset = 91)]
    [XmlElement("STOCK", Form=XmlSchemaForm.Unqualified)]
    public string Stock
    { 
       get
       {
          return _Stock;
       }
       set
       {
          _Stock = value;
       }
    }
    private string _Stock;


    /// <summary>
    /// Industry Standard Description (such as ANSI or ISO)
    /// </summary>
 
    [RfcField(AbapName = "NORMT", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 18, Offset = 141)]
    [XmlElement("NORMT", Form=XmlSchemaForm.Unqualified)]
    public string Normt
    { 
       get
       {
          return _Normt;
       }
       set
       {
          _Normt = value;
       }
    }
    private string _Normt;

  }

}
