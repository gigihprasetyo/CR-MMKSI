#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPPenaltyDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 12/16/2020 - 4:05:14 PM
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

    Public Class TOPSPPenaltyDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTOPSPPenaltyDetail"
        Private m_UpdateStatement As String = "up_UpdateTOPSPPenaltyDetail"
        Private m_RetrieveStatement As String = "up_RetrieveTOPSPPenaltyDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveTOPSPPenaltyDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTOPSPPenaltyDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim tOPSPPenaltyDetail As TOPSPPenaltyDetail = Nothing
            While dr.Read

                tOPSPPenaltyDetail = Me.CreateObject(dr)

            End While

            Return tOPSPPenaltyDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim tOPSPPenaltyDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim tOPSPPenaltyDetail As TOPSPPenaltyDetail = Me.CreateObject(dr)
                tOPSPPenaltyDetailList.Add(tOPSPPenaltyDetail)
            End While

            Return tOPSPPenaltyDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPPenaltyDetail As TOPSPPenaltyDetail = CType(obj, TOPSPPenaltyDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPPenaltyDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPPenaltyDetail As TOPSPPenaltyDetail = CType(obj, TOPSPPenaltyDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AccountingDocNo", DbType.AnsiString, tOPSPPenaltyDetail.AccountingDocNo)
            DbCommandWrapper.AddInParameter("@ActualTransferAmount", DbType.Currency, tOPSPPenaltyDetail.ActualTransferAmount)
            DbCommandWrapper.AddInParameter("@ActualTransferDate", DbType.DateTime, tOPSPPenaltyDetail.ActualTransferDate)
            DbCommandWrapper.AddInParameter("@PenaltyDays", DbType.Int32, tOPSPPenaltyDetail.PenaltyDays)
            DbCommandWrapper.AddInParameter("@AmountPenalty", DbType.Currency, tOPSPPenaltyDetail.AmountPenalty)
            DbCommandWrapper.AddInParameter("@PPh", DbType.Currency, tOPSPPenaltyDetail.PPh)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int32, tOPSPPenaltyDetail.PaymentType)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPPenaltyDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, tOPSPPenaltyDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, Me.GetRefObject(tOPSPPenaltyDetail.SparePartBilling))
            DbCommandWrapper.AddInParameter("@TOPSPPenaltyID", DbType.Int32, Me.GetRefObject(tOPSPPenaltyDetail.TOPSPPenalty))

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

            Dim tOPSPPenaltyDetail As TOPSPPenaltyDetail = CType(obj, TOPSPPenaltyDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPPenaltyDetail.ID)
            DbCommandWrapper.AddInParameter("@AccountingDocNo", DbType.AnsiString, tOPSPPenaltyDetail.AccountingDocNo)
            DbCommandWrapper.AddInParameter("@ActualTransferAmount", DbType.Currency, tOPSPPenaltyDetail.ActualTransferAmount)
            DbCommandWrapper.AddInParameter("@ActualTransferDate", DbType.DateTime, tOPSPPenaltyDetail.ActualTransferDate)
            DbCommandWrapper.AddInParameter("@PenaltyDays", DbType.Int32, tOPSPPenaltyDetail.PenaltyDays)
            DbCommandWrapper.AddInParameter("@AmountPenalty", DbType.Currency, tOPSPPenaltyDetail.AmountPenalty)
            DbCommandWrapper.AddInParameter("@PPh", DbType.Currency, tOPSPPenaltyDetail.PPh)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int32, tOPSPPenaltyDetail.PaymentType)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPPenaltyDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, tOPSPPenaltyDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, Me.GetRefObject(tOPSPPenaltyDetail.SparePartBilling))
            DbCommandWrapper.AddInParameter("@TOPSPPenaltyID", DbType.Int32, Me.GetRefObject(tOPSPPenaltyDetail.TOPSPPenalty))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TOPSPPenaltyDetail

            Dim tOPSPPenaltyDetail As TOPSPPenaltyDetail = New TOPSPPenaltyDetail

            tOPSPPenaltyDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AccountingDocNo")) Then tOPSPPenaltyDetail.AccountingDocNo = dr("AccountingDocNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActualTransferAmount")) Then tOPSPPenaltyDetail.ActualTransferAmount = CType(dr("ActualTransferAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ActualTransferDate")) Then tOPSPPenaltyDetail.ActualTransferDate = CType(dr("ActualTransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PenaltyDays")) Then tOPSPPenaltyDetail.PenaltyDays = CType(dr("PenaltyDays"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AmountPenalty")) Then tOPSPPenaltyDetail.AmountPenalty = CType(dr("AmountPenalty"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPh")) Then tOPSPPenaltyDetail.PPh = CType(dr("PPh"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then tOPSPPenaltyDetail.PaymentType = CType(dr("PaymentType"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then tOPSPPenaltyDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then tOPSPPenaltyDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then tOPSPPenaltyDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then tOPSPPenaltyDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then tOPSPPenaltyDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("SparePartBillingID")) Then
                tOPSPPenaltyDetail.SparePartBilling = New SparePartBilling(CType(dr("SparePartBillingID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TOPSPPenaltyID")) Then
                tOPSPPenaltyDetail.TOPSPPenalty = New TOPSPPenalty(CType(dr("TOPSPPenaltyID"), Integer))
            End If

            Return tOPSPPenaltyDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(TOPSPPenaltyDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TOPSPPenaltyDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TOPSPPenaltyDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
