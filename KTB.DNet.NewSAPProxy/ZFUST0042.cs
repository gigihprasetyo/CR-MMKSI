
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
  /// Credit Ceiling for RFC Credit Ceiling
  /// </summary>
  [RfcStructure(AbapName ="ZFUST0042" , Length = 121)]
  [Serializable]
  public class ZFUST0042 : SAPStructure
  {
   

    /// <summary>
    /// Customer's account number with credit limit reference
    /// </summary>
 
    [RfcField(AbapName = "KNKLI", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 10, Offset = 0)]
    [XmlElement("KNKLI", Form=XmlSchemaForm.Unqualified)]
    public string Knkli
    { 
       get
       {
          return _Knkli;
       }
       set
       {
          _Knkli = value;
       }
    }
    private string _Knkli;


    /// <summary>
    /// SPL Number
    /// </summary>
 
    [RfcField(AbapName = "SPLNM", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 20, Offset = 10)]
    [XmlElement("SPLNM", Form=XmlSchemaForm.Unqualified)]
    public string Splnm
    { 
       get
       {
          return _Splnm;
       }
       set
       {
          _Splnm = value;
       }
    }
    private string _Splnm;


    /// <summary>
    /// Period Year
    /// </summary>
 
    [RfcField(AbapName = "KLYEAR", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 4, Offset = 30)]
    [XmlElement("KLYEAR", Form=XmlSchemaForm.Unqualified)]
    public string Klyear
    { 
       get
       {
          return _Klyear;
       }
       set
       {
          _Klyear = value;
       }
    }
    private string _Klyear;


    /// <summary>
    /// Period Month
    /// </summary>
 
    [RfcField(AbapName = "KLMONTH", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 2, Offset = 34)]
    [XmlElement("KLMONTH", Form=XmlSchemaForm.Unqualified)]
    public string Klmonth
    { 
       get
       {
          return _Klmonth;
       }
       set
       {
          _Klmonth = value;
       }
    }
    private string _Klmonth;


    /// <summary>
    /// Temporary Code
    /// </summary>
 
    [RfcField(AbapName = "TMCOD", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 10, Offset = 36)]
    [XmlElement("TMCOD", Form=XmlSchemaForm.Unqualified)]
    public string Tmcod
    { 
       get
       {
          return _Tmcod;
       }
       set
       {
          _Tmcod = value;
       }
    }
    private string _Tmcod;


    /// <summary>
    /// Temporary Type
    /// </summary>
 
    [RfcField(AbapName = "TMTYP", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Offset = 46)]
    [XmlElement("TMTYP", Form=XmlSchemaForm.Unqualified)]
    public string Tmtyp
    { 
       get
       {
          return _Tmtyp;
       }
       set
       {
          _Tmtyp = value;
       }
    }
    private string _Tmtyp;


    /// <summary>
    /// Temporary Kind
    /// </summary>
 
    [RfcField(AbapName = "TMKND", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Offset = 47)]
    [XmlElement("TMKND", Form=XmlSchemaForm.Unqualified)]
    public string Tmknd
    { 
       get
       {
          return _Tmknd;
       }
       set
       {
          _Tmknd = value;
       }
    }
    private string _Tmknd;


    /// <summary>
    /// Credit Ceiling Amount
    /// </summary>
 
    [RfcField(AbapName = "KLIMK", RfcType = RFCTYPE.RFCTYPE_BCD, Length = 7, Offset = 48)]
    [XmlElement("KLIMK", Form=XmlSchemaForm.Unqualified)]
    public Decimal Klimk
    { 
       get
       {
          return _Klimk;
       }
       set
       {
          _Klimk = value;
       }
    }
    private Decimal _Klimk;


    /// <summary>
    /// Credit Blocked Amount
    /// </summary>
 
    [RfcField(AbapName = "BLIMK", RfcType = RFCTYPE.RFCTYPE_BCD, Length = 7, Offset = 55)]
    [XmlElement("BLIMK", Form=XmlSchemaForm.Unqualified)]
    public Decimal Blimk
    { 
       get
       {
          return _Blimk;
       }
       set
       {
          _Blimk = value;
       }
    }
    private Decimal _Blimk;


    /// <summary>
    /// Valid Date From
    /// </summary>
 
    [RfcField(AbapName = "KLFRM", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 8, Offset = 62)]
    [XmlElement("KLFRM", Form=XmlSchemaForm.Unqualified)]
    public string Klfrm
    { 
       get
       {
          return _Klfrm;
       }
       set
       {
          _Klfrm = value;
       }
    }
    private string _Klfrm;


    /// <summary>
    /// Valid Date To
    /// </summary>
 
    [RfcField(AbapName = "KLDTO", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 8, Offset = 70)]
    [XmlElement("KLDTO", Form=XmlSchemaForm.Unqualified)]
    public string Kldto
    { 
       get
       {
          return _Kldto;
       }
       set
       {
          _Kldto = value;
       }
    }
    private string _Kldto;


    /// <summary>
    /// Blocked Status
    /// </summary>
 
    [RfcField(AbapName = "BLKST", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 1, Offset = 78)]
    [XmlElement("BLKST", Form=XmlSchemaForm.Unqualified)]
    public string Blkst
    { 
       get
       {
          return _Blkst;
       }
       set
       {
          _Blkst = value;
       }
    }
    private string _Blkst;


    /// <summary>
    /// Blocked Date
    /// </summary>
 
    [RfcField(AbapName = "BLDAT", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 8, Offset = 79)]
    [XmlElement("BLDAT", Form=XmlSchemaForm.Unqualified)]
    public string Bldat
    { 
       get
       {
          return _Bldat;
       }
       set
       {
          _Bldat = value;
       }
    }
    private string _Bldat;


    /// <summary>
    /// Blocked name
    /// </summary>
 
    [RfcField(AbapName = "BLNAM", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 10, Offset = 87)]
    [XmlElement("BLNAM", Form=XmlSchemaForm.Unqualified)]
    public string Blnam
    { 
       get
       {
          return _Blnam;
       }
       set
       {
          _Blnam = value;
       }
    }
    private string _Blnam;


    /// <summary>
    /// Modified Date
    /// </summary>
 
    [RfcField(AbapName = "MFDAT", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 8, Offset = 97)]
    [XmlElement("MFDAT", Form=XmlSchemaForm.Unqualified)]
    public string Mfdat
    { 
       get
       {
          return _Mfdat;
       }
       set
       {
          _Mfdat = value;
       }
    }
    private string _Mfdat;


    /// <summary>
    /// Modified Time
    /// </summary>
 
    [RfcField(AbapName = "MFTIM", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 6, Offset = 105)]
    [XmlElement("MFTIM", Form=XmlSchemaForm.Unqualified)]
    public string Mftim
    { 
       get
       {
          return _Mftim;
       }
       set
       {
          _Mftim = value;
       }
    }
    private string _Mftim;


    /// <summary>
    /// Modified Name
    /// </summary>
 
    [RfcField(AbapName = "MFNAM", RfcType = RFCTYPE.RFCTYPE_CHAR, Length = 10, Offset = 111)]
    [XmlElement("MFNAM", Form=XmlSchemaForm.Unqualified)]
    public string Mfnam
    { 
       get
       {
          return _Mfnam;
       }
       set
       {
          _Mfnam = value;
       }
    }
    private string _Mfnam;

  }

}
