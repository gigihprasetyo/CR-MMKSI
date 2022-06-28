#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PaymentObligation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/25/2007 - 3:22:48 PM
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

    Public Class PaymentObligationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPaymentObligation"
        Private m_UpdateStatement As String = "up_UpdatePaymentObligation"
        Private m_RetrieveStatement As String = "up_RetrievePaymentObligation"
        Private m_RetrieveListStatement As String = "up_RetrievePaymentObligationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePaymentObligation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim paymentObligation As PaymentObligation = Nothing
            While dr.Read

                paymentObligation = Me.CreateObject(dr)

            End While

            Return paymentObligation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim paymentObligationList As ArrayList = New ArrayList

            While dr.Read
                Dim paymentObligation As PaymentObligation = Me.CreateObject(dr)
                paymentObligationList.Add(paymentObligation)
            End While

            Return paymentObligationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim paymentObligation As PaymentObligation = CType(obj, PaymentObligation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, paymentObligation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim paymentObligation As PaymentObligation = CType(obj, PaymentObligation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@SourceDocument", DbType.Int32, paymentObligation.SourceDocument)
            DbCommandWrapper.AddInParameter("@Assignment", DbType.AnsiString, paymentObligation.Assignment)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, paymentObligation.Sequence)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, paymentObligation.Amount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, paymentObligation.Description)
            DbCommandWrapper.AddInParameter("@DocDate", DbType.DateTime, paymentObligation.DocDate)
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, paymentObligation.DueDate)
            DbCommandWrapper.AddInParameter("@TransactionDueDate", DbType.DateTime, paymentObligation.TransactionDueDate)
            DbCommandWrapper.AddInParameter("@PaidDate", DbType.DateTime, paymentObligation.PaidDate)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, paymentObligation.ValidateTime)
            DbCommandWrapper.AddInParameter("@ValidateBy", DbType.AnsiString, paymentObligation.ValidateBy)
            DbCommandWrapper.AddInParameter("@ConfirmedTime", DbType.DateTime, paymentObligation.ConfirmedTime)
            DbCommandWrapper.AddInParameter("@ConfirmedBy", DbType.AnsiString, paymentObligation.ConfirmedBy)
            DbCommandWrapper.AddInParameter("@IsTOP", DbType.Int16, paymentObligation.IsTOP)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, paymentObligation.Status)
            DbCommandWrapper.AddInParameter("@ValidateMD5Code", DbType.AnsiString, paymentObligation.ValidateMD5Code)
            DbCommandWrapper.AddInParameter("@ValidateIPAddress", DbType.AnsiString, paymentObligation.ValidateIPAddress)
            DbCommandWrapper.AddInParameter("@Pinalty", DbType.Currency, paymentObligation.Pinalty)
            DbCommandWrapper.AddInParameter("@PinaltyReal", DbType.Currency, paymentObligation.PinaltyReal)
            DbCommandWrapper.AddInParameter("@PaidAmount", DbType.Currency, paymentObligation.PaidAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, paymentObligation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, paymentObligation.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(paymentObligation.Dealer))
            DbCommandWrapper.AddInParameter("@PaymentObligationTypeID", DbType.Int32, Me.GetRefObject(paymentObligation.PaymentObligationType))
            DbCommandWrapper.AddInParameter("@PaymentAssignmentTypeID", DbType.Int32, Me.GetRefObject(paymentObligation.PaymentAssignmentType))
            DbCommandWrapper.AddInParameter("@PaymentRegDocID", DbType.Int32, Me.GetRefObject(paymentObligation.PaymentRegDoc))

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

            Dim paymentObligation As PaymentObligation = CType(obj, PaymentObligation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, paymentObligation.ID)
            DbCommandWrapper.AddInParameter("@SourceDocument", DbType.Int32, paymentObligation.SourceDocument)
            DbCommandWrapper.AddInParameter("@Assignment", DbType.AnsiString, paymentObligation.Assignment)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, paymentObligation.Sequence)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, paymentObligation.Amount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, paymentObligation.Description)
            DbCommandWrapper.AddInParameter("@DocDate", DbType.DateTime, paymentObligation.DocDate)
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, paymentObligation.DueDate)
            DbCommandWrapper.AddInParameter("@TransactionDueDate", DbType.DateTime, paymentObligation.TransactionDueDate)
            DbCommandWrapper.AddInParameter("@PaidDate", DbType.DateTime, paymentObligation.PaidDate)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, paymentObligation.ValidateTime)
            DbCommandWrapper.AddInParameter("@ValidateBy", DbType.AnsiString, paymentObligation.ValidateBy)
            DbCommandWrapper.AddInParameter("@ConfirmedTime", DbType.DateTime, paymentObligation.ConfirmedTime)
            DbCommandWrapper.AddInParameter("@ConfirmedBy", DbType.AnsiString, paymentObligation.ConfirmedBy)
            DbCommandWrapper.AddInParameter("@IsTOP", DbType.Int16, paymentObligation.IsTOP)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, paymentObligation.Status)
            DbCommandWrapper.AddInParameter("@ValidateMD5Code", DbType.AnsiString, paymentObligation.ValidateMD5Code)
            DbCommandWrapper.AddInParameter("@ValidateIPAddress", DbType.AnsiString, paymentObligation.ValidateIPAddress)
            DbCommandWrapper.AddInParameter("@Pinalty", DbType.Currency, paymentObligation.Pinalty)
            DbCommandWrapper.AddInParameter("@PinaltyReal", DbType.Currency, paymentObligation.PinaltyReal)
            DbCommandWrapper.AddInParameter("@PaidAmount", DbType.Currency, paymentObligation.PaidAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, paymentObligation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, paymentObligation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(paymentObligation.Dealer))
            DbCommandWrapper.AddInParameter("@PaymentObligationTypeID", DbType.Int32, Me.GetRefObject(paymentObligation.PaymentObligationType))
            DbCommandWrapper.AddInParameter("@PaymentAssignmentTypeID", DbType.Int32, Me.GetRefObject(paymentObligation.PaymentAssignmentType))
            DbCommandWrapper.AddInParameter("@PaymentRegDocID", DbType.Int32, Me.GetRefObject(paymentObligation.PaymentRegDoc))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PaymentObligation

            Dim paymentObligation As PaymentObligation = New PaymentObligation

            paymentObligation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SourceDocument")) Then paymentObligation.SourceDocument = CType(dr("SourceDocument"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Assignment")) Then paymentObligation.Assignment = dr("Assignment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then paymentObligation.Sequence = CType(dr("Sequence"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then paymentObligation.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then paymentObligation.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocDate")) Then paymentObligation.DocDate = CType(dr("DocDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DueDate")) Then paymentObligation.DueDate = CType(dr("DueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDueDate")) Then paymentObligation.TransactionDueDate = CType(dr("TransactionDueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PaidDate")) Then paymentObligation.PaidDate = CType(dr("PaidDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateTime")) Then paymentObligation.ValidateTime = CType(dr("ValidateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateBy")) Then paymentObligation.ValidateBy = dr("ValidateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedTime")) Then paymentObligation.ConfirmedTime = CType(dr("ConfirmedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedBy")) Then paymentObligation.ConfirmedBy = dr("ConfirmedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsTOP")) Then paymentObligation.IsTOP = CType(dr("IsTOP"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then paymentObligation.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateMD5Code")) Then paymentObligation.ValidateMD5Code = dr("ValidateMD5Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateIPAddress")) Then paymentObligation.ValidateIPAddress = dr("ValidateIPAddress").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Pinalty")) Then paymentObligation.Pinalty = CType(dr("Pinalty"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PinaltyReal")) Then paymentObligation.PinaltyReal = CType(dr("PinaltyReal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaidAmount")) Then paymentObligation.PaidAmount = CType(dr("PaidAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then paymentObligation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then paymentObligation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then paymentObligation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then paymentObligation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then paymentObligation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                paymentObligation.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentObligationTypeID")) Then
                paymentObligation.PaymentObligationType = New PaymentObligationType(CType(dr("PaymentObligationTypeID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentAssignmentTypeID")) Then
                paymentObligation.PaymentAssignmentType = New PaymentAssignmentType(CType(dr("PaymentAssignmentTypeID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentRegDocID")) Then
                paymentObligation.PaymentRegDoc = New PaymentRegDoc(CType(dr("PaymentRegDocID"), Integer))
            End If

            Return paymentObligation

        End Function

        Private Sub SetTableName()

            If Not (GetType(PaymentObligation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PaymentObligation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PaymentObligation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"
        
#End Region

    End Class
End Namespace

