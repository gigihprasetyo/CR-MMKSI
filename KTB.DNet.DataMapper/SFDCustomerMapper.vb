#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFDCustomer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/31/2021 - 1:40:50 PM
'//
'// ===========================================================================	
#End Region


#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper

    Public Class SFDCustomerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSFDCustomer"
        Private m_UpdateStatement As String = "up_UpdateSFDCustomer"
        Private m_RetrieveStatement As String = "up_RetrieveSFDCustomer"
        Private m_RetrieveListStatement As String = "up_RetrieveSFDCustomerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSFDCustomer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sFDCustomer As SFDCustomer = Nothing
            While dr.Read

                sFDCustomer = Me.CreateObject(dr)

            End While

            Return sFDCustomer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sFDCustomerList As ArrayList = New ArrayList

            While dr.Read
                Dim sFDCustomer As SFDCustomer = Me.CreateObject(dr)
                sFDCustomerList.Add(sFDCustomer)
            End While

            Return sFDCustomerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sFDCustomer As SFDCustomer = CType(obj, SFDCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, sFDCustomer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sFDCustomer As SFDCustomer = CType(obj, SFDCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, sFDCustomer.SalesmanCode)
            DbCommandWrapper.AddInParameter("@CustomerType", DbType.AnsiString, sFDCustomer.CustomerType)
            DbCommandWrapper.AddInParameter("@ClassType", DbType.AnsiString, sFDCustomer.ClassType)
            DbCommandWrapper.AddInParameter("@LevelData", DbType.AnsiString, sFDCustomer.LevelData)
            DbCommandWrapper.AddInParameter("@CustomerClass", DbType.AnsiString, sFDCustomer.CustomerClass)
            DbCommandWrapper.AddInParameter("@CustomerTypeDNET", DbType.Int16, sFDCustomer.CustomerTypeDNET)
            DbCommandWrapper.AddInParameter("@CustomerSubClass", DbType.Int16, sFDCustomer.CustomerSubClass)
            DbCommandWrapper.AddInParameter("@CustomerNo", DbType.AnsiString, sFDCustomer.CustomerNo)
            DbCommandWrapper.AddInParameter("@ParentCustomerNo", DbType.AnsiString, sFDCustomer.ParentCustomerNo)
            DbCommandWrapper.AddInParameter("@FirstName", DbType.AnsiString, sFDCustomer.FirstName)
            DbCommandWrapper.AddInParameter("@LastName", DbType.AnsiString, sFDCustomer.LastName)
            DbCommandWrapper.AddInParameter("@BirthDate", DbType.DateTime, sFDCustomer.BirthDate)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Int16, sFDCustomer.Gender)
            DbCommandWrapper.AddInParameter("@HPNo", DbType.AnsiString, sFDCustomer.HPNo)
            DbCommandWrapper.AddInParameter("@OtherPhoneType", DbType.Int16, sFDCustomer.OtherPhoneType)
            DbCommandWrapper.AddInParameter("@OtherPhoneNo", DbType.AnsiString, sFDCustomer.OtherPhoneNo)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, sFDCustomer.Email)
            DbCommandWrapper.AddInParameter("@Gedung", DbType.AnsiString, sFDCustomer.Gedung)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, sFDCustomer.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, sFDCustomer.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, sFDCustomer.Kecamatan)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, sFDCustomer.PreArea)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, sFDCustomer.PostalCode)
            DbCommandWrapper.AddInParameter("@POBox", DbType.AnsiString, sFDCustomer.POBox)
            DbCommandWrapper.AddInParameter("@IdentityType", DbType.Int16, sFDCustomer.IdentityType)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, sFDCustomer.IdentityNumber)
            DbCommandWrapper.AddInParameter("@IdentityURLPath", DbType.AnsiString, sFDCustomer.IdentityURLPath)
            DbCommandWrapper.AddInParameter("@NPWPNo", DbType.AnsiString, sFDCustomer.NPWPNo)
            DbCommandWrapper.AddInParameter("@NPWPName", DbType.AnsiString, sFDCustomer.NPWPName)
            DbCommandWrapper.AddInParameter("@PrintRegion", DbType.Int16, sFDCustomer.PrintRegion)
            DbCommandWrapper.AddInParameter("@OCRIdentityID", DbType.Int32, sFDCustomer.OCRIdentityID)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, sFDCustomer.Notes)
            DbCommandWrapper.AddInParameter("@InterfaceStatus", DbType.Int16, sFDCustomer.InterfaceStatus)
            DbCommandWrapper.AddInParameter("@InterfaceMessage", DbType.AnsiString, sFDCustomer.InterfaceMessage)
            DbCommandWrapper.AddInParameter("@InterfaceCustSales", DbType.Int16, sFDCustomer.InterfaceCustSales)
            DbCommandWrapper.AddInParameter("@GUID", DbType.AnsiString, sFDCustomer.GUID)
            DbCommandWrapper.AddInParameter("@GUIDUpdate", DbType.AnsiString, sFDCustomer.GUIDUpdate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sFDCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sFDCustomer.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(sFDCustomer.City))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sFDCustomer.Dealer))
            'DbCommandWrapper.AddInParameter("@SPKMasterCountryCodePhoneID", DbType.Int32, Me.GetRefObject(sFDCustomer.SPKMasterCountryCodePhone))
            DbCommandWrapper.AddInParameter("@SPKMasterCountryCodePhoneID", DbType.Int32, sFDCustomer.SPKMasterCountryCodePhoneID)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sFDCustomer As SFDCustomer = CType(obj, SFDCustomer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, sFDCustomer.ID)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, sFDCustomer.SalesmanCode)
            DbCommandWrapper.AddInParameter("@CustomerType", DbType.AnsiString, sFDCustomer.CustomerType)
            DbCommandWrapper.AddInParameter("@ClassType", DbType.AnsiString, sFDCustomer.ClassType)
            DbCommandWrapper.AddInParameter("@LevelData", DbType.AnsiString, sFDCustomer.LevelData)
            DbCommandWrapper.AddInParameter("@CustomerClass", DbType.AnsiString, sFDCustomer.CustomerClass)
            DbCommandWrapper.AddInParameter("@CustomerTypeDNET", DbType.Int16, sFDCustomer.CustomerTypeDNET)
            DbCommandWrapper.AddInParameter("@CustomerSubClass", DbType.Int16, sFDCustomer.CustomerSubClass)
            DbCommandWrapper.AddInParameter("@CustomerNo", DbType.AnsiString, sFDCustomer.CustomerNo)
            DbCommandWrapper.AddInParameter("@ParentCustomerNo", DbType.AnsiString, sFDCustomer.ParentCustomerNo)
            DbCommandWrapper.AddInParameter("@FirstName", DbType.AnsiString, sFDCustomer.FirstName)
            DbCommandWrapper.AddInParameter("@LastName", DbType.AnsiString, sFDCustomer.LastName)
            DbCommandWrapper.AddInParameter("@BirthDate", DbType.DateTime, sFDCustomer.BirthDate)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Int16, sFDCustomer.Gender)
            DbCommandWrapper.AddInParameter("@HPNo", DbType.AnsiString, sFDCustomer.HPNo)
            DbCommandWrapper.AddInParameter("@OtherPhoneType", DbType.Int16, sFDCustomer.OtherPhoneType)
            DbCommandWrapper.AddInParameter("@OtherPhoneNo", DbType.AnsiString, sFDCustomer.OtherPhoneNo)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, sFDCustomer.Email)
            DbCommandWrapper.AddInParameter("@Gedung", DbType.AnsiString, sFDCustomer.Gedung)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, sFDCustomer.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, sFDCustomer.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, sFDCustomer.Kecamatan)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, sFDCustomer.PreArea)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, sFDCustomer.PostalCode)
            DbCommandWrapper.AddInParameter("@POBox", DbType.AnsiString, sFDCustomer.POBox)
            DbCommandWrapper.AddInParameter("@IdentityType", DbType.Int16, sFDCustomer.IdentityType)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, sFDCustomer.IdentityNumber)
            DbCommandWrapper.AddInParameter("@IdentityURLPath", DbType.AnsiString, sFDCustomer.IdentityURLPath)
            DbCommandWrapper.AddInParameter("@NPWPNo", DbType.AnsiString, sFDCustomer.NPWPNo)
            DbCommandWrapper.AddInParameter("@NPWPName", DbType.AnsiString, sFDCustomer.NPWPName)
            DbCommandWrapper.AddInParameter("@PrintRegion", DbType.Int16, sFDCustomer.PrintRegion)
            DbCommandWrapper.AddInParameter("@OCRIdentityID", DbType.Int32, sFDCustomer.OCRIdentityID)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, sFDCustomer.Notes)
            DbCommandWrapper.AddInParameter("@InterfaceStatus", DbType.Int16, sFDCustomer.InterfaceStatus)
            DbCommandWrapper.AddInParameter("@InterfaceMessage", DbType.AnsiString, sFDCustomer.InterfaceMessage)
            DbCommandWrapper.AddInParameter("@InterfaceCustSales", DbType.Int16, sFDCustomer.InterfaceCustSales)
            DbCommandWrapper.AddInParameter("@GUID", DbType.AnsiString, sFDCustomer.GUID)
            DbCommandWrapper.AddInParameter("@GUIDUpdate", DbType.AnsiString, sFDCustomer.GUIDUpdate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sFDCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sFDCustomer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(sFDCustomer.City))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sFDCustomer.Dealer))
            'DbCommandWrapper.AddInParameter("@SPKMasterCountryCodePhoneID", DbType.Int32, Me.GetRefObject(sFDCustomer.SPKMasterCountryCodePhone))
            DbCommandWrapper.AddInParameter("@SPKMasterCountryCodePhoneID", DbType.Int32, sFDCustomer.SPKMasterCountryCodePhoneID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SFDCustomer

            Dim sFDCustomer As SFDCustomer = New SFDCustomer

            sFDCustomer.ID = CType(dr("ID"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then sFDCustomer.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerType")) Then sFDCustomer.CustomerType = dr("CustomerType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClassType")) Then sFDCustomer.ClassType = dr("ClassType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LevelData")) Then sFDCustomer.LevelData = dr("LevelData").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerClass")) Then sFDCustomer.CustomerClass = dr("CustomerClass").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerTypeDNET")) Then sFDCustomer.CustomerTypeDNET = CType(dr("CustomerTypeDNET"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerSubClass")) Then sFDCustomer.CustomerSubClass = CType(dr("CustomerSubClass"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerNo")) Then sFDCustomer.CustomerNo = dr("CustomerNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ParentCustomerNo")) Then sFDCustomer.ParentCustomerNo = dr("ParentCustomerNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FirstName")) Then sFDCustomer.FirstName = dr("FirstName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastName")) Then sFDCustomer.LastName = dr("LastName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BirthDate")) Then sFDCustomer.BirthDate = CType(dr("BirthDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Gender")) Then sFDCustomer.Gender = CType(dr("Gender"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("HPNo")) Then sFDCustomer.HPNo = dr("HPNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OtherPhoneType")) Then sFDCustomer.OtherPhoneType = CType(dr("OtherPhoneType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("OtherPhoneNo")) Then sFDCustomer.OtherPhoneNo = dr("OtherPhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then sFDCustomer.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Gedung")) Then sFDCustomer.Gedung = dr("Gedung").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alamat")) Then sFDCustomer.Alamat = dr("Alamat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kelurahan")) Then sFDCustomer.Kelurahan = dr("Kelurahan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kecamatan")) Then sFDCustomer.Kecamatan = dr("Kecamatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PreArea")) Then sFDCustomer.PreArea = dr("PreArea").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostalCode")) Then sFDCustomer.PostalCode = dr("PostalCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("POBox")) Then sFDCustomer.POBox = dr("POBox").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityType")) Then sFDCustomer.IdentityType = CType(dr("IdentityType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityNumber")) Then sFDCustomer.IdentityNumber = dr("IdentityNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityURLPath")) Then sFDCustomer.IdentityURLPath = dr("IdentityURLPath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NPWPNo")) Then sFDCustomer.NPWPNo = dr("NPWPNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NPWPName")) Then sFDCustomer.NPWPName = dr("NPWPName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PrintRegion")) Then sFDCustomer.PrintRegion = CType(dr("PrintRegion"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("OCRIdentityID")) Then sFDCustomer.OCRIdentityID = CType(dr("OCRIdentityID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then sFDCustomer.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InterfaceStatus")) Then sFDCustomer.InterfaceStatus = CType(dr("InterfaceStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("InterfaceMessage")) Then sFDCustomer.InterfaceMessage = dr("InterfaceMessage").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InterfaceCustSales")) Then sFDCustomer.InterfaceCustSales = CType(dr("InterfaceCustSales"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("GUID")) Then sFDCustomer.GUID = dr("GUID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GUIDUpdate")) Then sFDCustomer.GUIDUpdate = dr("GUIDUpdate").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sFDCustomer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sFDCustomer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sFDCustomer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sFDCustomer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sFDCustomer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                sFDCustomer.City = New City(CType(dr("CityID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sFDCustomer.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPKMasterCountryCodePhoneID")) Then
                'sFDCustomer.SPKMasterCountryCodePhone = New SPKMasterCountryCodePhone(CType(dr("SPKMasterCountryCodePhoneID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPKMasterCountryCodePhoneID")) Then sFDCustomer.SPKMasterCountryCodePhoneID = CType(dr("SPKMasterCountryCodePhoneID"), Integer)

            Return sFDCustomer

        End Function

        Private Sub SetTableName()

            If Not (GetType(SFDCustomer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SFDCustomer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SFDCustomer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
