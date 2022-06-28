
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TransferPaymentDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 28/07/2016 - 14:34:17
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

    Public Class TransferPaymentDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTransferPaymentDetail"
        Private m_UpdateStatement As String = "up_UpdateTransferPaymentDetail"
        Private m_RetrieveStatement As String = "up_RetrieveTransferPaymentDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveTransferPaymentDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTransferPaymentDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim transferPaymentDetail As TransferPaymentDetail = Nothing
            While dr.Read

                transferPaymentDetail = Me.CreateObject(dr)

            End While

            Return transferPaymentDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim transferPaymentDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim transferPaymentDetail As TransferPaymentDetail = Me.CreateObject(dr)
                transferPaymentDetailList.Add(transferPaymentDetail)
            End While

            Return transferPaymentDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim transferPaymentDetail As TransferPaymentDetail = CType(obj, TransferPaymentDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, transferPaymentDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim transferPaymentDetail As TransferPaymentDetail = CType(obj, TransferPaymentDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, transferPaymentDetail.Amount)
            DbCommandWrapper.AddInParameter("@TransferPaymentNewID", DbType.Int32, transferPaymentDetail.TransferPaymentNewID)
            DbCommandWrapper.AddInParameter("@IsCanceled", DbType.Int16, transferPaymentDetail.IsCanceled)
            DbCommandWrapper.AddInParameter("@ActualAmount", DbType.Currency, transferPaymentDetail.ActualAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, transferPaymentDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, transferPaymentDetail.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, transferPaymentDetail.LastUpdatedTime)

            DbCommandWrapper.AddInParameter("@TransferPaymentID", DbType.Int32, Me.GetRefObject(transferPaymentDetail.TransferPayment))
            DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, Me.GetRefObject(transferPaymentDetail.SalesOrder))

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

            Dim transferPaymentDetail As TransferPaymentDetail = CType(obj, TransferPaymentDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, transferPaymentDetail.ID)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, transferPaymentDetail.Amount)
            DbCommandWrapper.AddInParameter("@TransferPaymentNewID", DbType.Int32, transferPaymentDetail.TransferPaymentNewID)
            DbCommandWrapper.AddInParameter("@IsCanceled", DbType.Int16, transferPaymentDetail.IsCanceled)
            DbCommandWrapper.AddInParameter("@ActualAmount", DbType.Currency, transferPaymentDetail.ActualAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, transferPaymentDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, transferPaymentDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@TransferPaymentID", DbType.Int32, Me.GetRefObject(transferPaymentDetail.TransferPayment))
            DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, Me.GetRefObject(transferPaymentDetail.SalesOrder))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TransferPaymentDetail

            Dim transferPaymentDetail As TransferPaymentDetail = New TransferPaymentDetail

            transferPaymentDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then transferPaymentDetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferPaymentNewID")) Then transferPaymentDetail.TransferPaymentNewID = CType(dr("TransferPaymentNewID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsCanceled")) Then transferPaymentDetail.IsCanceled = CType(dr("IsCanceled"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ActualAmount")) Then transferPaymentDetail.ActualAmount = CType(dr("ActualAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then transferPaymentDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then transferPaymentDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then transferPaymentDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then transferPaymentDetail.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then transferPaymentDetail.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferPaymentID")) Then
                transferPaymentDetail.TransferPayment = New TransferPayment(CType(dr("TransferPaymentID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderID")) Then
                transferPaymentDetail.SalesOrder = New SalesOrder(CType(dr("SalesOrderID"), Integer))
            End If

            Return transferPaymentDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(TransferPaymentDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TransferPaymentDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TransferPaymentDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

