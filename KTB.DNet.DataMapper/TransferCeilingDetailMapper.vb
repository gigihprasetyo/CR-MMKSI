
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TransferCeilingDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 28/07/2016 - 11:00:37
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

    Public Class TransferCeilingDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTransferCeilingDetail"
        Private m_UpdateStatement As String = "up_UpdateTransferCeilingDetail"
        Private m_RetrieveStatement As String = "up_RetrieveTransferCeilingDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveTransferCeilingDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTransferCeilingDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim transferCeilingDetail As TransferCeilingDetail = Nothing
            While dr.Read

                transferCeilingDetail = Me.CreateObject(dr)

            End While

            Return transferCeilingDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim transferCeilingDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim transferCeilingDetail As TransferCeilingDetail = Me.CreateObject(dr)
                transferCeilingDetailList.Add(transferCeilingDetail)
            End While

            Return transferCeilingDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim transferCeilingDetail As TransferCeilingDetail = CType(obj, TransferCeilingDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, transferCeilingDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim transferCeilingDetail As TransferCeilingDetail = CType(obj, TransferCeilingDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, transferCeilingDetail.SalesOrderID)
            'DbCommandWrapper.AddInParameter("@TransferPaymentID", DbType.Int32, transferCeilingDetail.TransferPaymentID)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, transferCeilingDetail.Amount)
            DbCommandWrapper.AddInParameter("@IsIncome", DbType.Int16, transferCeilingDetail.IsIncome)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, transferCeilingDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, transferCeilingDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, transferCeilingDetail.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, transferCeilingDetail.LastUpdatedTime)

            DbCommandWrapper.AddInParameter("@TransferCeilingID", DbType.Int32, Me.GetRefObject(transferCeilingDetail.TransferCeiling))
            DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, Me.GetRefObject(transferCeilingDetail.SalesOrder))
            DbCommandWrapper.AddInParameter("@TransferPaymentID", DbType.Int32, Me.GetRefObject(transferCeilingDetail.TransferPayment))

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

            Dim transferCeilingDetail As TransferCeilingDetail = CType(obj, TransferCeilingDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, transferCeilingDetail.ID)
            'DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, transferCeilingDetail.SalesOrderID)
            'DbCommandWrapper.AddInParameter("@TransferPaymentID", DbType.Int32, transferCeilingDetail.TransferPaymentID)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, transferCeilingDetail.Amount)
            DbCommandWrapper.AddInParameter("@IsIncome", DbType.Int16, transferCeilingDetail.IsIncome)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, transferCeilingDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, transferCeilingDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, transferCeilingDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, transferCeilingDetail.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@TransferCeilingID", DbType.Int32, Me.GetRefObject(transferCeilingDetail.TransferCeiling))
            DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, Me.GetRefObject(transferCeilingDetail.SalesOrder))
            DbCommandWrapper.AddInParameter("@TransferPaymentID", DbType.Int32, Me.GetRefObject(transferCeilingDetail.TransferPayment))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TransferCeilingDetail

            Dim transferCeilingDetail As TransferCeilingDetail = New TransferCeilingDetail

            transferCeilingDetail.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderID")) Then transferCeilingDetail.SalesOrderID = CType(dr("SalesOrderID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("TransferPaymentID")) Then transferCeilingDetail.TransferPaymentID = CType(dr("TransferPaymentID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then transferCeilingDetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("IsIncome")) Then transferCeilingDetail.IsIncome = CType(dr("IsIncome"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then transferCeilingDetail.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then transferCeilingDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then transferCeilingDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then transferCeilingDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then transferCeilingDetail.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then transferCeilingDetail.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferCeilingID")) Then
                transferCeilingDetail.TransferCeiling = New TransferCeiling(CType(dr("TransferCeilingID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderID")) Then
                transferCeilingDetail.SalesOrder = New SalesOrder(CType(dr("SalesOrderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TransferPaymentID")) Then
                transferCeilingDetail.TransferPayment = New TransferPayment(CType(dr("TransferPaymentID"), Integer))
            End If

            Return transferCeilingDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(TransferCeilingDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TransferCeilingDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TransferCeilingDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

