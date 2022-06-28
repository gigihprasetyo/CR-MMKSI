
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKDetailCustomer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 3/7/2011 - 2:13:13 PM
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

    Public Class SPKDetailCustomerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPKDetailCustomer"
        Private m_UpdateStatement As String = "up_UpdateSPKDetailCustomer"
        Private m_RetrieveStatement As String = "up_RetrieveSPKDetailCustomer"
        Private m_RetrieveListStatement As String = "up_RetrieveSPKDetailCustomerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPKDetailCustomer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim SPKDetailCustomer As SPKDetailCustomer = Nothing
            While dr.Read

                SPKDetailCustomer = Me.CreateObject(dr)

            End While

            Return SPKDetailCustomer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim SPKDetailCustomerList As ArrayList = New ArrayList

            While dr.Read
                Dim SPKDetailCustomer As SPKDetailCustomer = Me.CreateObject(dr)
                SPKDetailCustomerList.Add(SPKDetailCustomer)
            End While

            Return SPKDetailCustomerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim SPKDetailCustomer As SPKDetailCustomer = CType(obj, SPKDetailCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, SPKDetailCustomer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim SPKDetailCustomer As SPKDetailCustomer = CType(obj, SPKDetailCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, SPKDetailCustomer.Code)
            DbCommandWrapper.AddInParameter("@ReffCode", DbType.AnsiString, SPKDetailCustomer.ReffCode)
            DbCommandWrapper.AddInParameter("@TipeCustomer", DbType.Int16, SPKDetailCustomer.TipeCustomer)
            DbCommandWrapper.AddInParameter("@TipePerusahaan", DbType.Int16, SPKDetailCustomer.TipePerusahaan)
            DbCommandWrapper.AddInParameter("@Name1", DbType.String, SPKDetailCustomer.Name1)
            DbCommandWrapper.AddInParameter("@Name2", DbType.String, SPKDetailCustomer.Name2)
            DbCommandWrapper.AddInParameter("@Name3", DbType.String, SPKDetailCustomer.Name3)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.String, SPKDetailCustomer.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.String, SPKDetailCustomer.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.String, SPKDetailCustomer.Kecamatan)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.String, SPKDetailCustomer.PostalCode)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, SPKDetailCustomer.PreArea)
            DbCommandWrapper.AddInParameter("@PrintRegion", DbType.AnsiString, SPKDetailCustomer.PrintRegion)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.String, SPKDetailCustomer.PhoneNo)
            DbCommandWrapper.AddInParameter("@OfficeNo", DbType.String, SPKDetailCustomer.OfficeNo)
            DbCommandWrapper.AddInParameter("@HomeNo", DbType.String, SPKDetailCustomer.HomeNo)
            DbCommandWrapper.AddInParameter("@HpNo", DbType.String, SPKDetailCustomer.HpNo)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, SPKDetailCustomer.Email)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, SPKDetailCustomer.Status)
            DbCommandWrapper.AddInParameter("@MCPStatus", DbType.Int16, SPKDetailCustomer.MCPStatus)
            DbCommandWrapper.AddInParameter("@LKPPStatus", DbType.Int16, SPKDetailCustomer.LKPPStatus)
            DbCommandWrapper.AddInParameter("@LKPPReference", DbType.String, SPKDetailCustomer.LKPPReference)
            DbCommandWrapper.AddInParameter("@BusinessSectorDetailID", DbType.Int32, Me.GetRefObject(SPKDetailCustomer.BusinessSectorDetail))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, SPKDetailCustomer.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, SPKDetailCustomer.LastUpdateBy)

            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(SPKDetailCustomer.City))
            DbCommandWrapper.AddInParameter("@SPKDetailID", DbType.Int32, Me.GetRefObject(SPKDetailCustomer.SPKDetail))
            DbCommandWrapper.AddInParameter("@SAPCustomerID", DbType.Int32, Me.GetRefObject(SPKDetailCustomer.SAPCustomer))
            DbCommandWrapper.AddInParameter("@CustomerRequestID", DbType.Int32, Me.GetRefObject(SPKDetailCustomer.CustomerRequest))

            DbCommandWrapper.AddInParameter("@ImagePath", DbType.AnsiString, SPKDetailCustomer.ImagePath)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, SPKDetailCustomer.Quantity)
            DbCommandWrapper.AddInParameter("@DMSSPKDetailNo", DbType.String, SPKDetailCustomer.DMSSPKDetailNo)
            DbCommandWrapper.AddInParameter("@LastUpdateCustomer", DbType.DateTime, SPKDetailCustomer.LastUpdateCustomer)
            'CR SPK
            DbCommandWrapper.AddInParameter("@TypeIdentitas", DbType.Int16, SPKDetailCustomer.TypeIdentitas)
            DbCommandWrapper.AddInParameter("@TypePerorangan", DbType.Int16, SPKDetailCustomer.TypePerorangan)
            DbCommandWrapper.AddInParameter("@CountryCode", DbType.AnsiString, SPKDetailCustomer.CountryCode)
            '
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

            Dim SPKDetailCustomer As SPKDetailCustomer = CType(obj, SPKDetailCustomer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, SPKDetailCustomer.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, SPKDetailCustomer.Code)
            DbCommandWrapper.AddInParameter("@ReffCode", DbType.AnsiString, SPKDetailCustomer.ReffCode)
            DbCommandWrapper.AddInParameter("@TipeCustomer", DbType.Int16, SPKDetailCustomer.TipeCustomer)
            DbCommandWrapper.AddInParameter("@TipePerusahaan", DbType.Int16, SPKDetailCustomer.TipePerusahaan)
            DbCommandWrapper.AddInParameter("@Name1", DbType.String, SPKDetailCustomer.Name1)
            DbCommandWrapper.AddInParameter("@Name2", DbType.String, SPKDetailCustomer.Name2)
            DbCommandWrapper.AddInParameter("@Name3", DbType.String, SPKDetailCustomer.Name3)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.String, SPKDetailCustomer.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.String, SPKDetailCustomer.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.String, SPKDetailCustomer.Kecamatan)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.String, SPKDetailCustomer.PostalCode)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, SPKDetailCustomer.PreArea)
            DbCommandWrapper.AddInParameter("@PrintRegion", DbType.AnsiString, SPKDetailCustomer.PrintRegion)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.String, SPKDetailCustomer.PhoneNo)
            DbCommandWrapper.AddInParameter("@OfficeNo", DbType.String, SPKDetailCustomer.OfficeNo)
            DbCommandWrapper.AddInParameter("@HomeNo", DbType.String, SPKDetailCustomer.HomeNo)
            DbCommandWrapper.AddInParameter("@HpNo", DbType.String, SPKDetailCustomer.HpNo)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, SPKDetailCustomer.Email)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, SPKDetailCustomer.Status)
            DbCommandWrapper.AddInParameter("@MCPStatus", DbType.Int16, SPKDetailCustomer.MCPStatus)
            DbCommandWrapper.AddInParameter("@LKPPStatus", DbType.Int16, SPKDetailCustomer.LKPPStatus)
            DbCommandWrapper.AddInParameter("@LKPPReference", DbType.String, SPKDetailCustomer.LKPPReference)
            DbCommandWrapper.AddInParameter("@BusinessSectorDetailID", DbType.Int32, Me.GetRefObject(SPKDetailCustomer.BusinessSectorDetail))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, SPKDetailCustomer.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, SPKDetailCustomer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, User)


            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(SPKDetailCustomer.City))
            DbCommandWrapper.AddInParameter("@SAPCustomerID", DbType.Int32, Me.GetRefObject(SPKDetailCustomer.SAPCustomer))
            DbCommandWrapper.AddInParameter("@ImagePath", DbType.AnsiString, SPKDetailCustomer.ImagePath)
            DbCommandWrapper.AddInParameter("@SPKDetailID", DbType.Int32, Me.GetRefObject(SPKDetailCustomer.SPKDetail))
            DbCommandWrapper.AddInParameter("@CustomerRequestID", DbType.Int32, Me.GetRefObject(SPKDetailCustomer.CustomerRequest))

            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, SPKDetailCustomer.Quantity)
            DbCommandWrapper.AddInParameter("@DMSSPKDetailNo", DbType.String, SPKDetailCustomer.DMSSPKDetailNo)
            DbCommandWrapper.AddInParameter("@LastUpdateCustomer", DbType.DateTime, SPKDetailCustomer.LastUpdateCustomer)
            'CR SPK
            DbCommandWrapper.AddInParameter("@TypeIdentitas", DbType.Int16, SPKDetailCustomer.TypeIdentitas)
            DbCommandWrapper.AddInParameter("@TypePerorangan", DbType.Int16, SPKDetailCustomer.TypePerorangan)
            DbCommandWrapper.AddInParameter("@CountryCode", DbType.AnsiString, SPKDetailCustomer.CountryCode)
            '
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPKDetailCustomer

            Dim SPKDetailCustomer As SPKDetailCustomer = New SPKDetailCustomer

            SPKDetailCustomer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then SPKDetailCustomer.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReffCode")) Then SPKDetailCustomer.ReffCode = dr("ReffCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TipeCustomer")) Then SPKDetailCustomer.TipeCustomer = CType(dr("TipeCustomer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TipePerusahaan")) Then SPKDetailCustomer.TipePerusahaan = CType(dr("TipePerusahaan"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Name1")) Then SPKDetailCustomer.Name1 = dr("Name1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name2")) Then SPKDetailCustomer.Name2 = dr("Name2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name3")) Then SPKDetailCustomer.Name3 = dr("Name3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alamat")) Then SPKDetailCustomer.Alamat = dr("Alamat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kelurahan")) Then SPKDetailCustomer.Kelurahan = dr("Kelurahan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kecamatan")) Then SPKDetailCustomer.Kecamatan = dr("Kecamatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostalCode")) Then SPKDetailCustomer.PostalCode = dr("PostalCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PreArea")) Then SPKDetailCustomer.PreArea = dr("PreArea").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PrintRegion")) Then SPKDetailCustomer.PrintRegion = dr("PrintRegion").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNo")) Then SPKDetailCustomer.PhoneNo = dr("PhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OfficeNo")) Then SPKDetailCustomer.OfficeNo = dr("OfficeNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HomeNo")) Then SPKDetailCustomer.HomeNo = dr("HomeNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HpNo")) Then SPKDetailCustomer.HpNo = dr("HpNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then SPKDetailCustomer.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then SPKDetailCustomer.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MCPStatus")) Then SPKDetailCustomer.MCPStatus = CType(dr("MCPStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LKPPStatus")) Then SPKDetailCustomer.LKPPStatus = CType(dr("LKPPStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LKPPReference")) Then SPKDetailCustomer.LKPPReference = CType(dr("LKPPReference"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("ImagePath")) Then SPKDetailCustomer.ImagePath = CType(dr("ImagePath"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then SPKDetailCustomer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then SPKDetailCustomer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then SPKDetailCustomer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then SPKDetailCustomer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then SPKDetailCustomer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                SPKDetailCustomer.City = New City(CType(dr("CityID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SAPCustomerID")) Then
                SPKDetailCustomer.SAPCustomer = New SAPCustomer(CType(dr("SAPCustomerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ImagePath")) Then SPKDetailCustomer.ImagePath = dr("ImagePath").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("BusinessSectorDetailID")) Then
                SPKDetailCustomer.BusinessSectorDetail = New BusinessSectorDetail(CType(dr("BusinessSectorDetailID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("SPKDetailID")) Then
                SPKDetailCustomer.SPKDetail = New SPKDetail(CType(dr("SPKDetailID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("CustomerRequestID")) Then
                SPKDetailCustomer.CustomerRequest = New CustomerRequest(CType(dr("CustomerRequestID"), Integer))
            End If
             
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then SPKDetailCustomer.Quantity = CInt(dr("Quantity"))
            If Not dr.IsDBNull(dr.GetOrdinal("DMSSPKDetailNo")) Then SPKDetailCustomer.DMSSPKDetailNo = CType(dr("DMSSPKDetailNo"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateCustomer")) Then SPKDetailCustomer.LastUpdateCustomer = CType(dr("LastUpdateCustomer"), DateTime)
            'CR SPK
            If Not dr.IsDBNull(dr.GetOrdinal("TypeIdentitas")) Then SPKDetailCustomer.TypeIdentitas = CType(dr("TypeIdentitas"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TypePerorangan")) Then SPKDetailCustomer.TypePerorangan = CType(dr("TypePerorangan"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CountryCode")) Then SPKDetailCustomer.CountryCode = dr("CountryCode").ToString
            '
            Return SPKDetailCustomer

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPKDetailCustomer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPKDetailCustomer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPKDetailCustomer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

