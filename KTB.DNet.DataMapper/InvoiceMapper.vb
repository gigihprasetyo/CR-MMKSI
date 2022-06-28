
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Invoice Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/22/2008 - 2:51:47 PM
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

    Public Class InvoiceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertInvoice"
        Private m_UpdateStatement As String = "up_UpdateInvoice"
        Private m_RetrieveStatement As String = "up_RetrieveInvoice"
        Private m_RetrieveListStatement As String = "up_RetrieveInvoiceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteInvoice"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim invoice As Invoice = Nothing
            While dr.Read

                invoice = Me.CreateObject(dr)

            End While

            Return invoice

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim invoiceList As ArrayList = New ArrayList

            While dr.Read
                Dim invoice As Invoice = Me.CreateObject(dr)
                invoiceList.Add(invoice)
            End While

            Return invoiceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim invoice As Invoice = CType(obj, Invoice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, invoice.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim invoice As Invoice = CType(obj, Invoice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@InvoiceNumber", DbType.AnsiString, invoice.InvoiceNumber)
            DbCommandWrapper.AddInParameter("@InvoiceDate", DbType.DateTime, invoice.InvoiceDate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, invoice.Amount)
            DBCommandWrapper.AddInParameter("@InvoiceType", DbType.AnsiString, invoice.InvoiceType)
            DbCommandWrapper.AddInParameter("@InvoiceRef", DbType.AnsiString, invoice.InvoiceRef)
            DbCommandWrapper.AddInParameter("@InvoiceKind", DbType.Int32, invoice.InvoiceKind)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, invoice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, invoice.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, Me.GetRefObject(invoice.SalesOrder))
            DbCommandWrapper.AddInParameter("@LogisticDNID", DbType.Int32, Me.GetRefObject(invoice.LogisticDN))

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

            Dim invoice As Invoice = CType(obj, Invoice)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, invoice.ID)
            DbCommandWrapper.AddInParameter("@InvoiceNumber", DbType.AnsiString, invoice.InvoiceNumber)
            DbCommandWrapper.AddInParameter("@InvoiceDate", DbType.DateTime, invoice.InvoiceDate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, invoice.Amount)
            DBCommandWrapper.AddInParameter("@InvoiceType", DbType.AnsiString, invoice.InvoiceType)
            DbCommandWrapper.AddInParameter("@InvoiceRef", DbType.AnsiString, invoice.InvoiceRef)
            DbCommandWrapper.AddInParameter("@InvoiceKind", DbType.Int32, invoice.InvoiceKind)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, invoice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, invoice.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, Me.GetRefObject(invoice.SalesOrder))
            DbCommandWrapper.AddInParameter("@LogisticDNID", DbType.Int32, Me.GetRefObject(invoice.LogisticDN))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Invoice

            Dim invoice As Invoice = New Invoice

            invoice.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceNumber")) Then invoice.InvoiceNumber = dr("InvoiceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceDate")) Then invoice.InvoiceDate = CType(dr("InvoiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then invoice.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceType")) Then invoice.InvoiceType = dr("InvoiceType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceRef")) Then invoice.InvoiceRef = dr("InvoiceRef").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then invoice.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then invoice.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then invoice.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then invoice.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then invoice.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceKind")) Then invoice.InvoiceKind = CType(dr("InvoiceKind"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderID")) Then
                invoice.SalesOrder = New SalesOrder(CType(dr("SalesOrderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("LogisticDNID")) Then
                invoice.LogisticDN = New LogisticDN(CType(dr("LogisticDNID"), Integer))
            End If

            Return invoice

        End Function

        Private Sub SetTableName()

            If Not (GetType(Invoice) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Invoice), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Invoice).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

