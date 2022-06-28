
//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a SAP. NET Connector Proxy Generator Version 2.0
//     Created at 12/2/2009
//     Created from Windows
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------
using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
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
  /// Client SAP proxy class
  /// </summary>
  [WebServiceBinding(Name="dummy.Binding", Namespace="urn:sap-com:document:sap:rfc:functions")]
  [Serializable]
  public class NewSAPProxy : SAPClient
  {
    /// <summary>
    /// Initializes a new NewSAPProxy.
    /// </summary>
    public NewSAPProxy(){}

    /// <summary>
    /// Initializes a new NewSAPProxy with a new connection based on the specified connection string.
    /// </summary>
    /// <param name="connectionString">A connection string (e.g. RFC or URL) specifying the system where the proxy should connect to.</param>
    public NewSAPProxy(string connectionString) : base(connectionString){}

    /// <summary>
    /// Initializes a new NewSAPProxy instance and adds it to the given container.
    /// This allows automated connection mananged by VS component designer:
    /// If container is disposed, it will also dispose this SAPClient instance,
    /// which will dispose a contained connection if needed.
    /// </summary>
    /// <param name="container"<The container where the new SAPClient instance is to be added.>/param<
    public NewSAPProxy(System.ComponentModel.IContainer container) : base(container) {}    
  
    /// <summary>
    /// Remote Function Module ZRFC_CHECK_MATERIAL.  
    /// Check Material for DNET
    /// </summary>
    /// <param name="Flag">Single-character flag</param>
    /// <param name="Main_Part">Structure for RFC_MATERIAL_CHECK</param>
    /// <param name="Substitution_Part">Structure for RFC_MATERIAL_CHECK</param>
    [RfcMethod(AbapName = "ZRFC_CHECK_MATERIAL")]
    [SoapDocumentMethodAttribute("http://tempuri.org/ZRFC_CHECK_MATERIAL",
     RequestNamespace = "urn:sap-com:document:sap:rfc:functions",
     RequestElementName = "ZRFC_CHECK_MATERIAL",
     ResponseNamespace = "urn:sap-com:document:sap:rfc:functions",
     ResponseElementName = "ZRFC_CHECK_MATERIAL.Response")]
    public virtual void Zrfc_Check_Material (

     [RfcParameter(AbapName = "FLAG",RfcType=RFCTYPE.RFCTYPE_CHAR, Optional = true, Direction = RFCINOUT.IN, Length = 1)]
     [XmlElement("FLAG", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     string Flag,
     [RfcParameter(AbapName = "MAIN_PART",RfcType=RFCTYPE.RFCTYPE_ITAB, Optional = false, Direction = RFCINOUT.INOUT)]
     [XmlArray("MAIN_PART", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     [XmlArrayItem("item", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     ref ZSPST0028_01Table Main_Part,
     [RfcParameter(AbapName = "SUBSTITUTION_PART",RfcType=RFCTYPE.RFCTYPE_ITAB, Optional = false, Direction = RFCINOUT.INOUT)]
     [XmlArray("SUBSTITUTION_PART", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     [XmlArrayItem("item", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     ref ZSPST0028_02Table Substitution_Part)
    {
        object[]results = null;
        results = this.SAPInvoke("Zrfc_Check_Material",new object[] {
                            Flag,Main_Part,Substitution_Part });
        Main_Part = (ZSPST0028_01Table) results[0];
        Substitution_Part = (ZSPST0028_02Table) results[1];

    }

    /// <summary>
    /// Remote Function Module ZRFC_CREDIT_CEILING.  
    /// Credit Ceiling for DNET
    /// </summary>
    /// <param name="Cr_Ceiling">Credit Ceiling for RFC Credit Ceiling</param>
    [RfcMethod(AbapName = "ZRFC_CREDIT_CEILING")]
    [SoapDocumentMethodAttribute("http://tempuri.org/ZRFC_CREDIT_CEILING",
     RequestNamespace = "urn:sap-com:document:sap:rfc:functions",
     RequestElementName = "ZRFC_CREDIT_CEILING",
     ResponseNamespace = "urn:sap-com:document:sap:rfc:functions",
     ResponseElementName = "ZRFC_CREDIT_CEILING.Response")]
    public virtual void Zrfc_Credit_Ceiling (

     [RfcParameter(AbapName = "CR_CEILING",RfcType=RFCTYPE.RFCTYPE_ITAB, Optional = false, Direction = RFCINOUT.INOUT)]
     [XmlArray("CR_CEILING", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     [XmlArrayItem("item", IsNullable=false, Form=XmlSchemaForm.Unqualified)]
     ref ZFUST0042Table Cr_Ceiling)
    {
        object[]results = null;
        results = this.SAPInvoke("Zrfc_Credit_Ceiling",new object[] {
                            Cr_Ceiling });
        Cr_Ceiling = (ZFUST0042Table) results[0];

    }

  } 

}
