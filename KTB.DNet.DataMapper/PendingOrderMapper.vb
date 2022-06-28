#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PendingOrder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 12/19/2007 - 9:20:34 AM
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

    Public Class PendingOrderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPendingOrder"
        Private m_UpdateStatement As String = "up_UpdatePendingOrder"
        Private m_RetrieveStatement As String = "up_RetrievePendingOrder"
        Private m_RetrieveListStatement As String = "up_RetrievePendingOrderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePendingOrder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pendingOrder As PendingOrder = Nothing
            While dr.Read

                pendingOrder = Me.CreateObject(dr)

            End While

            Return pendingOrder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pendingOrderList As ArrayList = New ArrayList

            While dr.Read
                Dim pendingOrder As PendingOrder = Me.CreateObject(dr)
                pendingOrderList.Add(pendingOrder)
            End While

            Return pendingOrderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pendingOrder As PendingOrder = CType(obj, PendingOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pendingOrder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pendingOrder As PendingOrder = CType(obj, PendingOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@AvailableDeposit", DbType.Currency, pendingOrder.AvailableDeposit)
            DBCommandWrapper.AddInParameter("@GiroReceive", DbType.Currency, pendingOrder.GiroReceive)
            DBCommandWrapper.AddInParameter("@RO", DbType.Currency, pendingOrder.RO)
            DBCommandWrapper.AddInParameter("@Service", DbType.Currency, pendingOrder.Service)
            DbCommandWrapper.AddInParameter("@Retail", DbType.Currency, pendingOrder.Retail)
            DbCommandWrapper.AddInParameter("@PPN", DbType.Currency, pendingOrder.PPN)
            DbCommandWrapper.AddInParameter("@DepositC2", DbType.Currency, pendingOrder.DepositC2)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, pendingOrder.TotalAmount)
            DbCommandWrapper.AddInParameter("@IssueDate", DbType.DateTime, pendingOrder.IssueDate)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, pendingOrder.SONumber)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, pendingOrder.BillingNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pendingOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pendingOrder.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparepartPOID", DbType.Int32, Me.GetRefObject(pendingOrder.SparePartPO))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pendingOrder.Dealer))

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

            Dim pendingOrder As PendingOrder = CType(obj, PendingOrder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pendingOrder.ID)
            DBCommandWrapper.AddInParameter("@AvailableDeposit", DbType.Currency, pendingOrder.AvailableDeposit)
            DBCommandWrapper.AddInParameter("@GiroReceive", DbType.Currency, pendingOrder.GiroReceive)
            DBCommandWrapper.AddInParameter("@RO", DbType.Currency, pendingOrder.RO)
            DBCommandWrapper.AddInParameter("@Service", DbType.Currency, pendingOrder.Service)
            DbCommandWrapper.AddInParameter("@Retail", DbType.Currency, pendingOrder.Retail)
            DbCommandWrapper.AddInParameter("@PPN", DbType.Currency, pendingOrder.PPN)
            DbCommandWrapper.AddInParameter("@DepositC2", DbType.Currency, pendingOrder.DepositC2)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, pendingOrder.TotalAmount)
            DbCommandWrapper.AddInParameter("@IssueDate", DbType.DateTime, pendingOrder.IssueDate)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, pendingOrder.SONumber)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, pendingOrder.BillingNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pendingOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pendingOrder.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparepartPOID", DbType.Int32, Me.GetRefObject(pendingOrder.SparePartPO))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pendingOrder.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PendingOrder

            Dim pendingOrder As PendingOrder = New PendingOrder

            pendingOrder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AvailableDeposit")) Then pendingOrder.AvailableDeposit = CType(dr("AvailableDeposit"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("GiroReceive")) Then pendingOrder.GiroReceive = CType(dr("GiroReceive"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RO")) Then pendingOrder.RO = CType(dr("RO"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Service")) Then pendingOrder.Service = CType(dr("Service"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Retail")) Then pendingOrder.Retail = CType(dr("Retail"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPN")) Then pendingOrder.PPN = CType(dr("PPN"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DepositC2")) Then pendingOrder.DepositC2 = CType(dr("DepositC2"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then pendingOrder.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("IssueDate")) Then pendingOrder.IssueDate = CType(dr("IssueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then pendingOrder.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNumber")) Then pendingOrder.BillingNumber = dr("BillingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pendingOrder.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pendingOrder.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pendingOrder.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pendingOrder.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pendingOrder.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparepartPOID")) Then
                pendingOrder.SparePartPO = New SparePartPO(CType(dr("SparepartPOID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                pendingOrder.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return pendingOrder

        End Function

        Private Sub SetTableName()

            If Not (GetType(PendingOrder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PendingOrder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PendingOrder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

