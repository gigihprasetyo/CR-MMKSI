#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Customer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 1/16/2008 - 2:45:27 PM
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

    Public Class CustomerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCustomer"
        Private m_UpdateStatement As String = "up_UpdateCustomer"
        Private m_RetrieveStatement As String = "up_RetrieveCustomer"
        Private m_RetrieveListStatement As String = "up_RetrieveCustomerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCustomer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim customer As Customer = Nothing
            While dr.Read

                customer = Me.CreateObject(dr)

            End While

            Return customer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim customerList As ArrayList = New ArrayList

            While dr.Read
                Dim customer As Customer = Me.CreateObject(dr)
                customerList.Add(customer)
            End While

            Return customerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim customer As Customer = CType(obj, Customer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, customer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim customer As Customer = CType(obj, Customer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, customer.Code)
            DbCommandWrapper.AddInParameter("@ReffCode", DbType.AnsiString, customer.ReffCode)
            DbCommandWrapper.AddInParameter("@Name1", DbType.AnsiString, customer.Name1)
            DbCommandWrapper.AddInParameter("@Name2", DbType.AnsiString, customer.Name2)
            DbCommandWrapper.AddInParameter("@Name3", DbType.AnsiString, customer.Name3)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, customer.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, customer.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, customer.Kecamatan)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, customer.PostalCode)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, customer.PreArea)
            DbCommandWrapper.AddInParameter("@PrintRegion", DbType.AnsiString, customer.PrintRegion)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, customer.PhoneNo)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, customer.Email)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, customer.Attachment)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, customer.Status)
            DbCommandWrapper.AddInParameter("@DeletionMark", DbType.Int16, customer.DeletionMark)
            DBCommandWrapper.AddInParameter("@CompleteName", DbType.AnsiString, customer.CompleteName)

            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, customer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, customer.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(customer.City))

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

            Dim customer As Customer = CType(obj, Customer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, customer.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, customer.Code)
            DbCommandWrapper.AddInParameter("@ReffCode", DbType.AnsiString, customer.ReffCode)
            DbCommandWrapper.AddInParameter("@Name1", DbType.AnsiString, customer.Name1)
            DbCommandWrapper.AddInParameter("@Name2", DbType.AnsiString, customer.Name2)
            DbCommandWrapper.AddInParameter("@Name3", DbType.AnsiString, customer.Name3)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, customer.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, customer.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, customer.Kecamatan)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, customer.PostalCode)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, customer.PreArea)
            DbCommandWrapper.AddInParameter("@PrintRegion", DbType.AnsiString, customer.PrintRegion)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, customer.PhoneNo)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, customer.Email)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, customer.Attachment)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, customer.Status)
            DbCommandWrapper.AddInParameter("@DeletionMark", DbType.Int16, customer.DeletionMark)
            DBCommandWrapper.AddInParameter("@CompleteName", DbType.AnsiString, customer.CompleteName)

            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, customer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, customer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(customer.City))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Customer

            Dim customer As Customer = New Customer

            customer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then customer.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReffCode")) Then customer.ReffCode = dr("ReffCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name1")) Then customer.Name1 = dr("Name1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name2")) Then customer.Name2 = dr("Name2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name3")) Then customer.Name3 = dr("Name3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alamat")) Then customer.Alamat = dr("Alamat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kelurahan")) Then customer.Kelurahan = dr("Kelurahan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kecamatan")) Then customer.Kecamatan = dr("Kecamatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostalCode")) Then customer.PostalCode = dr("PostalCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PreArea")) Then customer.PreArea = dr("PreArea").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PrintRegion")) Then customer.PrintRegion = dr("PrintRegion").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNo")) Then customer.PhoneNo = dr("PhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then customer.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then customer.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then customer.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DeletionMark")) Then customer.DeletionMark = CType(dr("DeletionMark"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CompleteName")) Then customer.CompleteName = dr("CompleteName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then customer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then customer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then customer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then customer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then customer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                customer.City = New City(CType(dr("CityID"), Integer))
            End If

            Return customer

        End Function

        Private Sub SetTableName()

            If Not (GetType(Customer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Customer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Customer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

