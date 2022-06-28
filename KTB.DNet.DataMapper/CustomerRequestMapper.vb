#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CustomerRequest Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/26/2007 - 9:18:54 AM
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

    Public Class CustomerRequestMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCustomerRequest"
        Private m_UpdateStatement As String = "up_UpdateCustomerRequest"
        Private m_RetrieveStatement As String = "up_RetrieveCustomerRequest"
        Private m_RetrieveListStatement As String = "up_RetrieveCustomerRequestList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCustomerRequest"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim customerRequest As CustomerRequest = Nothing
            While dr.Read

                customerRequest = Me.CreateObject(dr)

            End While

            Return customerRequest

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim customerRequestList As ArrayList = New ArrayList

            While dr.Read
                Dim customerRequest As CustomerRequest = Me.CreateObject(dr)
                customerRequestList.Add(customerRequest)
            End While

            Return customerRequestList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim customerRequest As CustomerRequest = CType(obj, CustomerRequest)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, customerRequest.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim customerRequest As CustomerRequest = CType(obj, CustomerRequest)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, customerRequest.RequestNo)
            DbCommandWrapper.AddInParameter("@RefRequestNo", DbType.AnsiString, customerRequest.RefRequestNo)
            DbCommandWrapper.AddInParameter("@RequestType", DbType.AnsiString, customerRequest.RequestType)
            DbCommandWrapper.AddInParameter("@RequestUserID", DbType.Int32, customerRequest.RequestUserID)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, customerRequest.RequestDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, customerRequest.Status)
            DbCommandWrapper.AddInParameter("@ProcessUserID", DbType.AnsiString, customerRequest.ProcessUserID)
            DbCommandWrapper.AddInParameter("@ProcessDate", DbType.DateTime, customerRequest.ProcessDate)
            DbCommandWrapper.AddInParameter("@CustomerCode", DbType.AnsiString, customerRequest.CustomerCode)
            DbCommandWrapper.AddInParameter("@ReffCode", DbType.AnsiString, customerRequest.ReffCode)
            DbCommandWrapper.AddInParameter("@Name1", DbType.AnsiString, customerRequest.Name1)
            DbCommandWrapper.AddInParameter("@Name2", DbType.AnsiString, customerRequest.Name2)
            DbCommandWrapper.AddInParameter("@Name3", DbType.AnsiString, customerRequest.Name3)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, customerRequest.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, customerRequest.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, customerRequest.Kecamatan)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, customerRequest.PostalCode)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, customerRequest.PreArea)
            DbCommandWrapper.AddInParameter("@PrintRegion", DbType.AnsiString, customerRequest.PrintRegion)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, customerRequest.PhoneNo)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, customerRequest.Email)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, customerRequest.Attachment)
            DBCommandWrapper.AddInParameter("@Status1", DbType.Int16, customerRequest.Status1)
            DBCommandWrapper.AddInParameter("@TipePerusahaan", DbType.Int16, customerRequest.TipePerusahaan)

            DBCommandWrapper.AddInParameter("@MCPStatus", DbType.Int16, customerRequest.MCPStatus)
            DbCommandWrapper.AddInParameter("@LKPPStatus", DbType.Int16, customerRequest.LKPPStatus)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, customerRequest.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, customerRequest.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, customerRequest.CityID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(customerRequest.Dealer))
            'CR SPK
            DbCommandWrapper.AddInParameter("@TypeIdentitas", DbType.Int32, customerRequest.TypeIdentitas)
            DbCommandWrapper.AddInParameter("@TypePerorangan", DbType.Int32, customerRequest.TypePerorangan)
            DbCommandWrapper.AddInParameter("@CountryCode", DbType.AnsiString, customerRequest.CountryCode)
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

            Dim customerRequest As CustomerRequest = CType(obj, CustomerRequest)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, customerRequest.ID)
            DbCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, customerRequest.RequestNo)
            DbCommandWrapper.AddInParameter("@RefRequestNo", DbType.AnsiString, customerRequest.RefRequestNo)
            DbCommandWrapper.AddInParameter("@RequestType", DbType.AnsiString, customerRequest.RequestType)
            DbCommandWrapper.AddInParameter("@RequestUserID", DbType.Int32, customerRequest.RequestUserID)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, customerRequest.RequestDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, customerRequest.Status)
            DbCommandWrapper.AddInParameter("@ProcessUserID", DbType.AnsiString, customerRequest.ProcessUserID)
            DbCommandWrapper.AddInParameter("@ProcessDate", DbType.DateTime, customerRequest.ProcessDate)
            DbCommandWrapper.AddInParameter("@CustomerCode", DbType.AnsiString, customerRequest.CustomerCode)
            DbCommandWrapper.AddInParameter("@ReffCode", DbType.AnsiString, customerRequest.ReffCode)
            DbCommandWrapper.AddInParameter("@Name1", DbType.AnsiString, customerRequest.Name1)
            DbCommandWrapper.AddInParameter("@Name2", DbType.AnsiString, customerRequest.Name2)
            DbCommandWrapper.AddInParameter("@Name3", DbType.AnsiString, customerRequest.Name3)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, customerRequest.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, customerRequest.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, customerRequest.Kecamatan)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, customerRequest.PostalCode)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, customerRequest.PreArea)
            DbCommandWrapper.AddInParameter("@PrintRegion", DbType.AnsiString, customerRequest.PrintRegion)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, customerRequest.PhoneNo)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, customerRequest.Email)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, customerRequest.Attachment)
            DBCommandWrapper.AddInParameter("@Status1", DbType.Int16, customerRequest.Status1)
            DBCommandWrapper.AddInParameter("@TipePerusahaan", DbType.Int16, customerRequest.TipePerusahaan)

            DBCommandWrapper.AddInParameter("@MCPStatus", DbType.Int16, customerRequest.MCPStatus)
            DbCommandWrapper.AddInParameter("@LKPPStatus", DbType.Int16, customerRequest.LKPPStatus)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, customerRequest.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, customerRequest.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, customerRequest.CityID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(customerRequest.Dealer))
            'CR SPK
            DbCommandWrapper.AddInParameter("@TypeIdentitas", DbType.Int32, customerRequest.TypeIdentitas)
            DbCommandWrapper.AddInParameter("@TypePerorangan", DbType.Int32, customerRequest.TypePerorangan)
            DbCommandWrapper.AddInParameter("@CountryCode", DbType.AnsiString, customerRequest.CountryCode)
            '
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CustomerRequest

            Dim customerRequest As CustomerRequest = New CustomerRequest

            customerRequest.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestNo")) Then customerRequest.RequestNo = dr("RequestNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RefRequestNo")) Then customerRequest.RefRequestNo = dr("RefRequestNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RequestType")) Then customerRequest.RequestType = dr("RequestType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RequestUserID")) Then customerRequest.RequestUserID = CType(dr("RequestUserID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestDate")) Then customerRequest.RequestDate = CType(dr("RequestDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then customerRequest.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProcessUserID")) Then customerRequest.ProcessUserID = dr("ProcessUserID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProcessDate")) Then customerRequest.ProcessDate = CType(dr("ProcessDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerCode")) Then customerRequest.CustomerCode = dr("CustomerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReffCode")) Then customerRequest.ReffCode = dr("ReffCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name1")) Then customerRequest.Name1 = dr("Name1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name2")) Then customerRequest.Name2 = dr("Name2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name3")) Then customerRequest.Name3 = dr("Name3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alamat")) Then customerRequest.Alamat = dr("Alamat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kelurahan")) Then customerRequest.Kelurahan = dr("Kelurahan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kecamatan")) Then customerRequest.Kecamatan = dr("Kecamatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostalCode")) Then customerRequest.PostalCode = dr("PostalCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PreArea")) Then customerRequest.PreArea = dr("PreArea").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PrintRegion")) Then customerRequest.PrintRegion = dr("PrintRegion").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNo")) Then customerRequest.PhoneNo = dr("PhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then customerRequest.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then customerRequest.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status1")) Then customerRequest.Status1 = CType(dr("Status1"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TipePerusahaan")) Then customerRequest.TipePerusahaan = CType(dr("TipePerusahaan"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("MCPStatus")) Then customerRequest.MCPStatus = CType(dr("MCPStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LKPPStatus")) Then customerRequest.LKPPStatus = CType(dr("LKPPStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then customerRequest.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then customerRequest.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then customerRequest.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then customerRequest.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then customerRequest.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                customerRequest.CityID = CType(dr("CityID"), Integer)
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                customerRequest.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            'If Not dr.IsDBNull(dr.GetOrdinal("ID")) Then
            '    customerRequest.SPKHeader = customerRequest.GetSPKHeader(CType(dr("ID"), Integer))
            'End If

            'CR SPK
            If Not dr.IsDBNull(dr.GetOrdinal("TypeIdentitas")) Then customerRequest.TypeIdentitas = CType(dr("TypeIdentitas"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TypePerorangan")) Then customerRequest.TypePerorangan = CType(dr("TypePerorangan"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CountryCode")) Then customerRequest.CountryCode = dr("CountryCode").ToString
            '

            Return customerRequest

        End Function

        Private Sub SetTableName()

            If Not (GetType(CustomerRequest) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CustomerRequest), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CustomerRequest).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

