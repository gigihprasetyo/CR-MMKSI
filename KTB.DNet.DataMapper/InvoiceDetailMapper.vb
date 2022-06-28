
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : InvoiceDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/9/2008 - 8:49:05 AM
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

    Public Class InvoiceDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertInvoiceDetail"
        Private m_UpdateStatement As String = "up_UpdateInvoiceDetail"
        Private m_RetrieveStatement As String = "up_RetrieveInvoiceDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveInvoiceDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteInvoiceDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim invoiceDetail As InvoiceDetail = Nothing
            While dr.Read

                invoiceDetail = Me.CreateObject(dr)

            End While

            Return invoiceDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim invoiceDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim invoiceDetail As InvoiceDetail = Me.CreateObject(dr)
                invoiceDetailList.Add(invoiceDetail)
            End While

            Return invoiceDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim invoiceDetail As InvoiceDetail = CType(obj, InvoiceDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, invoiceDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim invoiceDetail As InvoiceDetail = CType(obj, InvoiceDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@InvoiceItem", DbType.Int64, invoiceDetail.InvoiceItem)
            DbCommandWrapper.AddInParameter("@BilledQty", DbType.Int64, invoiceDetail.BilledQty)
            DbCommandWrapper.AddInParameter("@ItemAmount", DbType.Currency, invoiceDetail.ItemAmount)
            DbCommandWrapper.AddInParameter("@PPH22", DbType.Currency, invoiceDetail.PPH22)
            DbCommandWrapper.AddInParameter("@Interest", DbType.Currency, invoiceDetail.Interest)
            DbCommandWrapper.AddInParameter("@Category", DbType.AnsiString, invoiceDetail.Category)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, invoiceDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, invoiceDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@InvoiceHeaderID", DbType.Int32, Me.GetRefObject(invoiceDetail.InvoiceHeader))
            DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int32, Me.GetRefObject(invoiceDetail.VechileColor))

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

            Dim invoiceDetail As InvoiceDetail = CType(obj, InvoiceDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, invoiceDetail.ID)
            DbCommandWrapper.AddInParameter("@InvoiceItem", DbType.Int64, invoiceDetail.InvoiceItem)
            DbCommandWrapper.AddInParameter("@BilledQty", DbType.Int64, invoiceDetail.BilledQty)
            DbCommandWrapper.AddInParameter("@ItemAmount", DbType.Currency, invoiceDetail.ItemAmount)
            DbCommandWrapper.AddInParameter("@PPH22", DbType.Currency, invoiceDetail.PPH22)
            DbCommandWrapper.AddInParameter("@Interest", DbType.Currency, invoiceDetail.Interest)
            DbCommandWrapper.AddInParameter("@Category", DbType.AnsiString, invoiceDetail.Category)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, invoiceDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, invoiceDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@InvoiceHeaderID", DbType.Int32, Me.GetRefObject(invoiceDetail.InvoiceHeader))
            DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int32, Me.GetRefObject(invoiceDetail.VechileColor))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As InvoiceDetail

            Dim invoiceDetail As InvoiceDetail = New InvoiceDetail

            invoiceDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceItem")) Then invoiceDetail.InvoiceItem = CType(dr("InvoiceItem"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("BilledQty")) Then invoiceDetail.BilledQty = CType(dr("BilledQty"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("ItemAmount")) Then invoiceDetail.ItemAmount = CType(dr("ItemAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPH22")) Then invoiceDetail.PPH22 = CType(dr("PPH22"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Interest")) Then invoiceDetail.Interest = CType(dr("Interest"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Category")) Then invoiceDetail.Category = dr("Category").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then invoiceDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then invoiceDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then invoiceDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then invoiceDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then invoiceDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceHeaderID")) Then
                invoiceDetail.InvoiceHeader = New InvoiceHeader(CType(dr("InvoiceHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorID")) Then
                invoiceDetail.VechileColor = New VechileColor(CType(dr("VechileColorID"), Integer))
            End If

            Return invoiceDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(InvoiceDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(InvoiceDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(InvoiceDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

