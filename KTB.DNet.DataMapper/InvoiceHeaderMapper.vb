
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : InvoiceHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 11/27/2008 - 4:41:51 PM
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

    Public Class InvoiceHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertInvoiceHeader"
        Private m_UpdateStatement As String = "up_UpdateInvoiceHeader"
        Private m_RetrieveStatement As String = "up_RetrieveInvoiceHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveInvoiceHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteInvoiceHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim invoiceHeader As InvoiceHeader = Nothing
            While dr.Read

                invoiceHeader = Me.CreateObject(dr)

            End While

            Return invoiceHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim invoiceHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim invoiceHeader As InvoiceHeader = Me.CreateObject(dr)
                invoiceHeaderList.Add(invoiceHeader)
            End While

            Return invoiceHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim invoiceHeader As InvoiceHeader = CType(obj, InvoiceHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, invoiceHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim invoiceHeader As InvoiceHeader = CType(obj, InvoiceHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@InvoiceNumber", DbType.AnsiString, invoiceHeader.InvoiceNumber)
            DbCommandWrapper.AddInParameter("@InvoiceDate", DbType.DateTime, invoiceHeader.InvoiceDate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, invoiceHeader.Amount)
            DbCommandWrapper.AddInParameter("@Cancelled", DbType.AnsiString, invoiceHeader.Cancelled)
            DbCommandWrapper.AddInParameter("@InvoiceType", DbType.AnsiString, invoiceHeader.InvoiceType)
            DbCommandWrapper.AddInParameter("@PaymentRef", DbType.AnsiString, invoiceHeader.PaymentRef)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, invoiceHeader.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, invoiceHeader.CreatedBy)
            'DBCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, invoiceHeader.CreatedTime)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, invoiceHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, Me.GetRefObject(invoiceHeader.POHeader))

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim invoiceHeader As invoiceHeader = CType(obj, invoiceHeader)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, invoiceHeader.ID)
            DBCommandWrapper.AddInParameter("@InvoiceNumber", DbType.AnsiString, invoiceHeader.InvoiceNumber)
            DBCommandWrapper.AddInParameter("@InvoiceDate", DbType.DateTime, invoiceHeader.InvoiceDate)
            DBCommandWrapper.AddInParameter("@Amount", DbType.Currency, invoiceHeader.Amount)
            DBCommandWrapper.AddInParameter("@Cancelled", DbType.AnsiString, invoiceHeader.Cancelled)
            DBCommandWrapper.AddInParameter("@InvoiceType", DbType.AnsiString, invoiceHeader.InvoiceType)
            DBCommandWrapper.AddInParameter("@PaymentRef", DbType.AnsiString, invoiceHeader.PaymentRef)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, invoiceHeader.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, invoiceHeader.CreatedBy)
            'DBCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, invoiceHeader.CreatedTime)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, Me.GetRefObject(invoiceHeader.POHeader))

            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As InvoiceHeader

            Dim invoiceHeader As invoiceHeader = New invoiceHeader

            invoiceHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceNumber")) Then invoiceHeader.InvoiceNumber = dr("InvoiceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceDate")) Then invoiceHeader.InvoiceDate = CType(dr("InvoiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then invoiceHeader.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Cancelled")) Then invoiceHeader.Cancelled = dr("Cancelled").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceType")) Then invoiceHeader.InvoiceType = dr("InvoiceType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentRef")) Then invoiceHeader.PaymentRef = dr("PaymentRef").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then invoiceHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then invoiceHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then invoiceHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then invoiceHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then invoiceHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("POHeaderID")) Then
                invoiceHeader.POHeader = New POHeader(CType(dr("POHeaderID"), Integer))
            End If

            Return invoiceHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(InvoiceHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(InvoiceHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(InvoiceHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

