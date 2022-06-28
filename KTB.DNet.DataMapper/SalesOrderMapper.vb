
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesOrder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/18/2008 - 2:18:49 PM
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

    Public Class SalesOrderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesOrder"
        Private m_UpdateStatement As String = "up_UpdateSalesOrder"
        Private m_RetrieveStatement As String = "up_RetrieveSalesOrder"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesOrderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesOrder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesOrder As SalesOrder = Nothing
            While dr.Read

                salesOrder = Me.CreateObject(dr)

            End While

            Return salesOrder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesOrderList As ArrayList = New ArrayList

            While dr.Read
                Dim salesOrder As SalesOrder = Me.CreateObject(dr)
                salesOrderList.Add(salesOrder)
            End While

            Return salesOrderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesOrder As SalesOrder = CType(obj, SalesOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesOrder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesOrder As SalesOrder = CType(obj, SalesOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, salesOrder.SONumber)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, salesOrder.SODate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Decimal, salesOrder.Amount)
            DbCommandWrapper.AddInParameter("@PaymentRef", DbType.AnsiString, salesOrder.PaymentRef)
            DbCommandWrapper.AddInParameter("@SOType", DbType.AnsiString, salesOrder.SOType)
            DbCommandWrapper.AddInParameter("@CashPayment", DbType.Currency, salesOrder.CashPayment)
            DbCommandWrapper.AddInParameter("@TotalVH", DbType.Currency, salesOrder.TotalVH)
            DbCommandWrapper.AddInParameter("@TotalPP", DbType.Currency, salesOrder.TotalPP)
            DbCommandWrapper.AddInParameter("@TotalIT", DbType.Currency, salesOrder.TotalIT)
            'DbCommandWrapper.AddInParameter("@LogisticDCHeaderID", DbType.Int32, salesOrder.LogisticDCHeader.ID)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesOrder.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, Me.GetRefObject(salesOrder.POHeader))
            DbCommandWrapper.AddInParameter("@LogisticDCHeaderID", DbType.Int32, Me.GetRefObject(salesOrder.LogisticDCHeader))
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

            Dim salesOrder As SalesOrder = CType(obj, SalesOrder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesOrder.ID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, salesOrder.SONumber)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, salesOrder.SODate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Decimal, salesOrder.Amount)
            DbCommandWrapper.AddInParameter("@PaymentRef", DbType.AnsiString, salesOrder.PaymentRef)
            DbCommandWrapper.AddInParameter("@SOType", DbType.AnsiString, salesOrder.SOType)
            DbCommandWrapper.AddInParameter("@CashPayment", DbType.Currency, salesOrder.CashPayment)
            DbCommandWrapper.AddInParameter("@TotalVH", DbType.Currency, salesOrder.TotalVH)
            DbCommandWrapper.AddInParameter("@TotalPP", DbType.Currency, salesOrder.TotalPP)
            DbCommandWrapper.AddInParameter("@TotalIT", DbType.Currency, salesOrder.TotalIT)
            'DbCommandWrapper.AddInParameter("@LogisticDCHeaderID", DbType.Int32, salesOrder.LogisticDCHeader.ID)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesOrder.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@LogisticDCHeaderID", DbType.Int32, Me.GetRefObject(salesOrder.LogisticDCHeader))
            DbCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, Me.GetRefObject(salesOrder.POHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesOrder

            Dim salesOrder As SalesOrder = New SalesOrder

            salesOrder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then salesOrder.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SODate")) Then salesOrder.SODate = CType(dr("SODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then salesOrder.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentRef")) Then salesOrder.PaymentRef = dr("PaymentRef").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SOType")) Then salesOrder.SOType = dr("SOType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CashPayment")) Then salesOrder.CashPayment = CType(dr("CashPayment"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalVH")) Then salesOrder.TotalVH = CType(dr("TotalVH"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalPP")) Then salesOrder.TotalPP = CType(dr("TotalPP"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalIT")) Then salesOrder.TotalIT = CType(dr("TotalIT"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesOrder.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesOrder.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesOrder.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesOrder.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesOrder.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("POHeaderID")) Then
                salesOrder.POHeader = New POHeader(CType(dr("POHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("LogisticDCHeaderID")) Then
                salesOrder.LogisticDCHeader = New LogisticDCHeader(CType(dr("LogisticDCHeaderID"), Integer))
            End If

            Return salesOrder

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesOrder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesOrder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesOrder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

