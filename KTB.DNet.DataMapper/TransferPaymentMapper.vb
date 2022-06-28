
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TransferPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 28/07/2016 - 11:02:02
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

    Public Class TransferPaymentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTransferPayment"
        Private m_UpdateStatement As String = "up_UpdateTransferPayment"
        Private m_RetrieveStatement As String = "up_RetrieveTransferPayment"
        Private m_RetrieveListStatement As String = "up_RetrieveTransferPaymentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTransferPayment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim transferPayment As TransferPayment = Nothing
            While dr.Read

                transferPayment = Me.CreateObject(dr)

            End While

            Return transferPayment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim transferPaymentList As ArrayList = New ArrayList

            While dr.Read
                Dim transferPayment As TransferPayment = Me.CreateObject(dr)
                transferPaymentList.Add(transferPayment)
            End While

            Return transferPaymentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim transferPayment As TransferPayment = CType(obj, TransferPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, transferPayment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim transferPayment As TransferPayment = CType(obj, TransferPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, transferPayment.DueDate)
            DbCommandWrapper.AddInParameter("@PlanTransferDate", DbType.DateTime, transferPayment.PlanTransferDate)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, transferPayment.RegNumber)
            DbCommandWrapper.AddInParameter("@IsNotOnTime", DbType.Int16, transferPayment.IsNotOnTime)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, transferPayment.Status)
            DbCommandWrapper.AddInParameter("@ValidatedBy", DbType.AnsiString, transferPayment.ValidatedBy)
            DbCommandWrapper.AddInParameter("@ValidatedTime", DbType.DateTime, transferPayment.ValidatedTime)
            DbCommandWrapper.AddInParameter("@ConfirmedBy", DbType.AnsiString, transferPayment.ConfirmedBy)
            DbCommandWrapper.AddInParameter("@ConfirmedTime", DbType.DateTime, transferPayment.ConfirmedTime)
            DbCommandWrapper.AddInParameter("@ActualTrfDate", DbType.DateTime, transferPayment.ActualTrfDate)
            DbCommandWrapper.AddInParameter("@TotalActualAmount", DbType.Currency, transferPayment.TotalActualAmount)
            DbCommandWrapper.AddInParameter("@TransferAmount", DbType.Currency, transferPayment.TransferAmount)
            DbCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, transferPayment.TransferDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, transferPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, transferPayment.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, transferPayment.LastUpdatedTime)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(transferPayment.Dealer))
            DbCommandWrapper.AddInParameter("@PaymentPurposeID", DbType.Byte, Me.GetRefObject(transferPayment.PaymentPurpose))

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

            Dim transferPayment As TransferPayment = CType(obj, TransferPayment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, transferPayment.ID)
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, transferPayment.DueDate)
            DbCommandWrapper.AddInParameter("@PlanTransferDate", DbType.DateTime, transferPayment.PlanTransferDate)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, transferPayment.RegNumber)
            DbCommandWrapper.AddInParameter("@IsNotOnTime", DbType.Int16, transferPayment.IsNotOnTime)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, transferPayment.Status)
            DbCommandWrapper.AddInParameter("@ValidatedBy", DbType.AnsiString, transferPayment.ValidatedBy)
            DbCommandWrapper.AddInParameter("@ValidatedTime", DbType.DateTime, transferPayment.ValidatedTime)
            DbCommandWrapper.AddInParameter("@ConfirmedBy", DbType.AnsiString, transferPayment.ConfirmedBy)
            DbCommandWrapper.AddInParameter("@ConfirmedTime", DbType.DateTime, transferPayment.ConfirmedTime)
            DbCommandWrapper.AddInParameter("@ActualTrfDate", DbType.DateTime, transferPayment.ActualTrfDate)
            DbCommandWrapper.AddInParameter("@TotalActualAmount", DbType.Currency, transferPayment.TotalActualAmount)
            DbCommandWrapper.AddInParameter("@TransferAmount", DbType.Currency, transferPayment.TransferAmount)
            DbCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, transferPayment.TransferDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, transferPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, transferPayment.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(transferPayment.Dealer))
            DbCommandWrapper.AddInParameter("@PaymentPurposeID", DbType.Byte, Me.GetRefObject(transferPayment.PaymentPurpose))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TransferPayment

            Dim transferPayment As TransferPayment = New TransferPayment

            transferPayment.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DueDate")) Then transferPayment.DueDate = CType(dr("DueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanTransferDate")) Then transferPayment.PlanTransferDate = CType(dr("PlanTransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then transferPayment.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsNotOnTime")) Then transferPayment.IsNotOnTime = CType(dr("IsNotOnTime"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then transferPayment.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidatedBy")) Then transferPayment.ValidatedBy = dr("ValidatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidatedTime")) Then transferPayment.ValidatedTime = CType(dr("ValidatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedBy")) Then transferPayment.ConfirmedBy = dr("ConfirmedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedTime")) Then transferPayment.ConfirmedTime = CType(dr("ConfirmedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ActualTrfDate")) Then transferPayment.ActualTrfDate = CType(dr("ActualTrfDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalActualAmount")) Then transferPayment.TotalActualAmount = CType(dr("TotalActualAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferAmount")) Then transferPayment.TransferAmount = CType(dr("TransferAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferDate")) Then transferPayment.TransferDate = CType(dr("TransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then transferPayment.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then transferPayment.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then transferPayment.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then transferPayment.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then transferPayment.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                transferPayment.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("PaymentPurposeID")) Then
                transferPayment.PaymentPurpose = New PaymentPurpose(CType(dr("PaymentPurposeID"), Byte))
            End If

            Return transferPayment

        End Function

        Private Sub SetTableName()

            If Not (GetType(TransferPayment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TransferPayment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TransferPayment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

