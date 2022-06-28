
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPTransferPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/10/2018 - 9:57:15 AM
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

    Public Class TOPSPTransferPaymentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTOPSPTransferPayment"
        Private m_UpdateStatement As String = "up_UpdateTOPSPTransferPayment"
        Private m_RetrieveStatement As String = "up_RetrieveTOPSPTransferPayment"
        Private m_RetrieveListStatement As String = "up_RetrieveTOPSPTransferPaymentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTOPSPTransferPayment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim tOPSPTransferPayment As TOPSPTransferPayment = Nothing
            While dr.Read

                tOPSPTransferPayment = Me.CreateObject(dr)

            End While

            Return tOPSPTransferPayment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim tOPSPTransferPaymentList As ArrayList = New ArrayList

            While dr.Read
                Dim tOPSPTransferPayment As TOPSPTransferPayment = Me.CreateObject(dr)
                tOPSPTransferPaymentList.Add(tOPSPTransferPayment)
            End While

            Return tOPSPTransferPaymentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPTransferPayment As TOPSPTransferPayment = CType(obj, TOPSPTransferPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPTransferPayment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPTransferPayment As TOPSPTransferPayment = CType(obj, TOPSPTransferPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, tOPSPTransferPayment.CreditAccount)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, tOPSPTransferPayment.RegNumber)
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, tOPSPTransferPayment.DueDate)
            DbCommandWrapper.AddInParameter("@TransferPlanDate", DbType.DateTime, tOPSPTransferPayment.TransferPlanDate)
            DbCommandWrapper.AddInParameter("@TOPSPTransferPaymentIDReff", DbType.Int32, tOPSPTransferPayment.TOPSPTransferPaymentIDReff)
            DbCommandWrapper.AddInParameter("@BankID", DbType.Int32, tOPSPTransferPayment.BankID)
            DbCommandWrapper.AddInParameter("@IsAccelerated", DbType.Int16, tOPSPTransferPayment.IsAccelerated)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, tOPSPTransferPayment.Status)
            'DbCommandWrapper.AddInParameter("@ValidatedBy", DbType.AnsiString, tOPSPTransferPayment.ValidatedBy)
            'DbCommandWrapper.AddInParameter("@ValidatedTime", DbType.DateTime, tOPSPTransferPayment.ValidatedTime)
            'DbCommandWrapper.AddInParameter("@ConfirmedBy", DbType.AnsiString, tOPSPTransferPayment.ConfirmedBy)
            'DbCommandWrapper.AddInParameter("@ConfirmedTime", DbType.DateTime, tOPSPTransferPayment.ConfirmedTime)
            'DbCommandWrapper.AddInParameter("@CanceledBy", DbType.AnsiString, tOPSPTransferPayment.CanceledBy)
            'DbCommandWrapper.AddInParameter("@CanceledTime", DbType.DateTime, tOPSPTransferPayment.CanceledTime)
            DbCommandWrapper.AddInParameter("@TransferAmount", DbType.Currency, tOPSPTransferPayment.TransferAmount)
            DbCommandWrapper.AddInParameter("@TransferActualDate", DbType.DateTime, tOPSPTransferPayment.TransferActualDate)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPTransferPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, tOPSPTransferPayment.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(tOPSPTransferPayment.Dealer))
            'DbCommandWrapper.AddInParameter("@PaymentPurposeID", DbType.Byte, Me.GetRefObject(tOPSPTransferPayment.PaymentPurpose))
            DbCommandWrapper.AddInParameter("@PaymentPurposeID", DbType.Int32, tOPSPTransferPayment.PaymentPurposeID)


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

            Dim tOPSPTransferPayment As TOPSPTransferPayment = CType(obj, TOPSPTransferPayment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPTransferPayment.ID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, tOPSPTransferPayment.CreditAccount)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, tOPSPTransferPayment.RegNumber)
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, tOPSPTransferPayment.DueDate)
            DbCommandWrapper.AddInParameter("@TransferPlanDate", DbType.DateTime, tOPSPTransferPayment.TransferPlanDate)
            DbCommandWrapper.AddInParameter("@TOPSPTransferPaymentIDReff", DbType.Int32, tOPSPTransferPayment.TOPSPTransferPaymentIDReff)
            DbCommandWrapper.AddInParameter("@IsAccelerated", DbType.Int16, tOPSPTransferPayment.IsAccelerated)
            DbCommandWrapper.AddInParameter("@BankID", DbType.Int32, tOPSPTransferPayment.BankID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, tOPSPTransferPayment.Status)
            DbCommandWrapper.AddInParameter("@ValidatedBy", DbType.AnsiString, tOPSPTransferPayment.ValidatedBy)
            DbCommandWrapper.AddInParameter("@ValidatedTime", DbType.DateTime, tOPSPTransferPayment.ValidatedTime)
            DbCommandWrapper.AddInParameter("@ConfirmedBy", DbType.AnsiString, tOPSPTransferPayment.ConfirmedBy)
            DbCommandWrapper.AddInParameter("@ConfirmedTime", DbType.DateTime, tOPSPTransferPayment.ConfirmedTime)
            DbCommandWrapper.AddInParameter("@CanceledBy", DbType.AnsiString, tOPSPTransferPayment.CanceledBy)
            DbCommandWrapper.AddInParameter("@CanceledTime", DbType.DateTime, tOPSPTransferPayment.CanceledTime)
            DbCommandWrapper.AddInParameter("@TransferAmount", DbType.Currency, tOPSPTransferPayment.TransferAmount)
            DbCommandWrapper.AddInParameter("@TransferActualDate", DbType.DateTime, tOPSPTransferPayment.TransferActualDate)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPTransferPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, tOPSPTransferPayment.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(tOPSPTransferPayment.Dealer))
            DbCommandWrapper.AddInParameter("@PaymentPurposeID", DbType.Byte, Me.GetRefObject(tOPSPTransferPayment.PaymentPurpose))
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TOPSPTransferPayment

            Dim tOPSPTransferPayment As TOPSPTransferPayment = New TOPSPTransferPayment

            tOPSPTransferPayment.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then tOPSPTransferPayment.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then tOPSPTransferPayment.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DueDate")) Then tOPSPTransferPayment.DueDate = CType(dr("DueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferPlanDate")) Then tOPSPTransferPayment.TransferPlanDate = CType(dr("TransferPlanDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TOPSPTransferPaymentIDReff")) Then tOPSPTransferPayment.TOPSPTransferPaymentIDReff = CType(dr("TOPSPTransferPaymentIDReff"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BankID")) Then tOPSPTransferPayment.BankID = CType(dr("BankID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsAccelerated")) Then tOPSPTransferPayment.IsAccelerated = CType(dr("IsAccelerated"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then tOPSPTransferPayment.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidatedBy")) Then tOPSPTransferPayment.ValidatedBy = dr("ValidatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidatedTime")) Then tOPSPTransferPayment.ValidatedTime = CType(dr("ValidatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedBy")) Then tOPSPTransferPayment.ConfirmedBy = dr("ConfirmedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedTime")) Then tOPSPTransferPayment.ConfirmedTime = CType(dr("ConfirmedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CanceledBy")) Then tOPSPTransferPayment.CanceledBy = dr("CanceledBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CanceledTime")) Then tOPSPTransferPayment.CanceledTime = CType(dr("CanceledTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferAmount")) Then tOPSPTransferPayment.TransferAmount = CType(dr("TransferAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferActualDate")) Then tOPSPTransferPayment.TransferActualDate = CType(dr("TransferActualDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then tOPSPTransferPayment.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then tOPSPTransferPayment.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then tOPSPTransferPayment.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then tOPSPTransferPayment.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then tOPSPTransferPayment.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                tOPSPTransferPayment.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentPurposeID")) Then
                tOPSPTransferPayment.PaymentPurpose = New PaymentPurpose(CType(dr("PaymentPurposeID"), Byte))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("BankID")) Then
                tOPSPTransferPayment.Bank = New Bank(CType(dr("BankID"), Byte))
            End If

            Return tOPSPTransferPayment

        End Function

        Private Sub SetTableName()

            If Not (GetType(TOPSPTransferPayment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TOPSPTransferPayment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TOPSPTransferPayment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

