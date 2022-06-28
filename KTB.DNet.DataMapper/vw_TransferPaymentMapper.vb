
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : vw_TransferPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 02/12/2018 - 1:35:50 PM
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

    Public Class vw_TransferPaymentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertvw_TransferPayment"
        Private m_UpdateStatement As String = "up_Updatevw_TransferPayment"
        Private m_RetrieveStatement As String = "up_Retrievevw_TransferPayment"
        Private m_RetrieveListStatement As String = "up_Retrievevw_TransferPaymentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletevw_TransferPayment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vw_TransferPayment As vw_TransferPayment = Nothing
            While dr.Read

                vw_TransferPayment = Me.CreateObject(dr)

            End While

            Return vw_TransferPayment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vw_TransferPaymentList As ArrayList = New ArrayList

            While dr.Read
                Dim vw_TransferPayment As vw_TransferPayment = Me.CreateObject(dr)
                vw_TransferPaymentList.Add(vw_TransferPayment)
            End While

            Return vw_TransferPaymentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vw_TransferPayment As vw_TransferPayment = CType(obj, vw_TransferPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vw_TransferPayment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vw_TransferPayment As vw_TransferPayment = CType(obj, vw_TransferPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddInParameter("@PaymentPemercepat", DbType.AnsiString, vw_TransferPayment.PaymentPemercepat)
            DbCommandWrapper.AddInParameter("@PaymentPemercepatID", DbType.Int32, vw_TransferPayment.PaymentPemercepatID)
            DbCommandWrapper.AddInParameter("@PaymentDiPercepatID", DbType.Int32, vw_TransferPayment.PaymentDiPercepatID)
            DbCommandWrapper.AddInParameter("@PaymentDiPercepat", DbType.AnsiString, vw_TransferPayment.PaymentDiPercepat)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(vw_TransferPayment.Dealer))
            DbCommandWrapper.AddInParameter("@PaymentPurposeID", DbType.Byte, Me.GetRefObject(vw_TransferPayment.PaymentPurpose))
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, vw_TransferPayment.DueDate)
            DbCommandWrapper.AddInParameter("@PlanTransferDate", DbType.DateTime, vw_TransferPayment.PlanTransferDate)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, vw_TransferPayment.RegNumber)
            DbCommandWrapper.AddInParameter("@IsNotOnTime", DbType.Int16, vw_TransferPayment.IsNotOnTime)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, vw_TransferPayment.Status)
            DbCommandWrapper.AddInParameter("@ValidatedBy", DbType.AnsiString, vw_TransferPayment.ValidatedBy)
            DbCommandWrapper.AddInParameter("@ValidatedTime", DbType.DateTime, vw_TransferPayment.ValidatedTime)
            DbCommandWrapper.AddInParameter("@ConfirmedBy", DbType.AnsiString, vw_TransferPayment.ConfirmedBy)
            DbCommandWrapper.AddInParameter("@ConfirmedTime", DbType.DateTime, vw_TransferPayment.ConfirmedTime)
            DbCommandWrapper.AddInParameter("@TotalActualAmount", DbType.Currency, vw_TransferPayment.TotalActualAmount)
            DbCommandWrapper.AddInParameter("@ActualTrfDate", DbType.DateTime, vw_TransferPayment.ActualTrfDate)
            DbCommandWrapper.AddInParameter("@TransferAmount", DbType.Currency, vw_TransferPayment.TransferAmount)
            DbCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, vw_TransferPayment.TransferDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vw_TransferPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, vw_TransferPayment.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, vw_TransferPayment.LastUpdatedTime)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vw_TransferPayment.ID)

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

            Dim vw_TransferPayment As vw_TransferPayment = CType(obj, vw_TransferPayment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@PaymentPemercepat", DbType.AnsiString, vw_TransferPayment.PaymentPemercepat)
            DbCommandWrapper.AddInParameter("@PaymentPemercepatID", DbType.Int32, vw_TransferPayment.PaymentPemercepatID)
            DbCommandWrapper.AddInParameter("@PaymentDiPercepatID", DbType.Int32, vw_TransferPayment.PaymentDiPercepatID)
            DbCommandWrapper.AddInParameter("@PaymentDiPercepat", DbType.AnsiString, vw_TransferPayment.PaymentDiPercepat)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(vw_TransferPayment.Dealer))
            DbCommandWrapper.AddInParameter("@PaymentPurposeID", DbType.Byte, Me.GetRefObject(vw_TransferPayment.PaymentPurpose))
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, vw_TransferPayment.DueDate)
            DbCommandWrapper.AddInParameter("@PlanTransferDate", DbType.DateTime, vw_TransferPayment.PlanTransferDate)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, vw_TransferPayment.RegNumber)
            DbCommandWrapper.AddInParameter("@IsNotOnTime", DbType.Int16, vw_TransferPayment.IsNotOnTime)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, vw_TransferPayment.Status)
            DbCommandWrapper.AddInParameter("@ValidatedBy", DbType.AnsiString, vw_TransferPayment.ValidatedBy)
            DbCommandWrapper.AddInParameter("@ValidatedTime", DbType.DateTime, vw_TransferPayment.ValidatedTime)
            DbCommandWrapper.AddInParameter("@ConfirmedBy", DbType.AnsiString, vw_TransferPayment.ConfirmedBy)
            DbCommandWrapper.AddInParameter("@ConfirmedTime", DbType.DateTime, vw_TransferPayment.ConfirmedTime)
            DbCommandWrapper.AddInParameter("@TotalActualAmount", DbType.Currency, vw_TransferPayment.TotalActualAmount)
            DbCommandWrapper.AddInParameter("@ActualTrfDate", DbType.DateTime, vw_TransferPayment.ActualTrfDate)
            DbCommandWrapper.AddInParameter("@TransferAmount", DbType.Currency, vw_TransferPayment.TransferAmount)
            DbCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, vw_TransferPayment.TransferDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vw_TransferPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vw_TransferPayment.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, vw_TransferPayment.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vw_TransferPayment.ID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As vw_TransferPayment

            Dim vw_TransferPayment As vw_TransferPayment = New vw_TransferPayment

            If Not dr.IsDBNull(dr.GetOrdinal("PaymentPemercepat")) Then vw_TransferPayment.PaymentPemercepat = dr("PaymentPemercepat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentPemercepatID")) Then vw_TransferPayment.PaymentPemercepatID = CType(dr("PaymentPemercepatID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentDiPercepatID")) Then vw_TransferPayment.PaymentDiPercepatID = CType(dr("PaymentDiPercepatID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentDiPercepat")) Then vw_TransferPayment.PaymentDiPercepat = dr("PaymentDiPercepat").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then vw_TransferPayment.DealerID = CType(dr("DealerID"), Short)
            'If Not dr.IsDBNull(dr.GetOrdinal("PaymentPurposeID")) Then vw_TransferPayment.PaymentPurposeID = CType(dr("PaymentPurposeID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("DueDate")) Then vw_TransferPayment.DueDate = CType(dr("DueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanTransferDate")) Then vw_TransferPayment.PlanTransferDate = CType(dr("PlanTransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then vw_TransferPayment.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsNotOnTime")) Then vw_TransferPayment.IsNotOnTime = CType(dr("IsNotOnTime"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vw_TransferPayment.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidatedBy")) Then vw_TransferPayment.ValidatedBy = dr("ValidatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidatedTime")) Then vw_TransferPayment.ValidatedTime = CType(dr("ValidatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedBy")) Then vw_TransferPayment.ConfirmedBy = dr("ConfirmedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedTime")) Then vw_TransferPayment.ConfirmedTime = CType(dr("ConfirmedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalActualAmount")) Then vw_TransferPayment.TotalActualAmount = CType(dr("TotalActualAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ActualTrfDate")) Then vw_TransferPayment.ActualTrfDate = CType(dr("ActualTrfDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferAmount")) Then vw_TransferPayment.TransferAmount = CType(dr("TransferAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferDate")) Then vw_TransferPayment.TransferDate = CType(dr("TransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vw_TransferPayment.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vw_TransferPayment.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vw_TransferPayment.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then vw_TransferPayment.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then vw_TransferPayment.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DebitNumber")) Then vw_TransferPayment.DebitNumber = dr("DebitNumber").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("ID")) Then
                vw_TransferPayment.ID = CType(dr("ID"), Integer)
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                vw_TransferPayment.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("PaymentPurposeID")) Then
                vw_TransferPayment.PaymentPurpose = New PaymentPurpose(CType(dr("PaymentPurposeID"), Byte))
            End If

            Return vw_TransferPayment

        End Function

        Private Sub SetTableName()

            If Not (GetType(vw_TransferPayment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(vw_TransferPayment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(vw_TransferPayment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

