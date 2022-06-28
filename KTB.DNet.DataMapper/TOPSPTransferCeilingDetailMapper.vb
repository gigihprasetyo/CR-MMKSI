
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPTransferCeilingDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/14/2018 - 10:52:48 AM
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

    Public Class TOPSPTransferCeilingDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTOPSPTransferCeilingDetail"
        Private m_UpdateStatement As String = "up_UpdateTOPSPTransferCeilingDetail"
        Private m_RetrieveStatement As String = "up_RetrieveTOPSPTransferCeilingDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveTOPSPTransferCeilingDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTOPSPTransferCeilingDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim tOPSPTransferCeilingDetail As TOPSPTransferCeilingDetail = Nothing
            While dr.Read

                tOPSPTransferCeilingDetail = Me.CreateObject(dr)

            End While

            Return tOPSPTransferCeilingDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim tOPSPTransferCeilingDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim tOPSPTransferCeilingDetail As TOPSPTransferCeilingDetail = Me.CreateObject(dr)
                tOPSPTransferCeilingDetailList.Add(tOPSPTransferCeilingDetail)
            End While

            Return tOPSPTransferCeilingDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPTransferCeilingDetail As TOPSPTransferCeilingDetail = CType(obj, TOPSPTransferCeilingDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPTransferCeilingDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPTransferCeilingDetail As TOPSPTransferCeilingDetail = CType(obj, TOPSPTransferCeilingDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@TOPSPTransferCeilingID", DbType.Int32, tOPSPTransferCeilingDetail.TOPSPTransferCeilingID)
            'DbCommandWrapper.AddInParameter("@SparepartBillingID", DbType.Int32, tOPSPTransferCeilingDetail.SparepartBillingID)
            'DbCommandWrapper.AddInParameter("@TOPSPTransferPaymentID", DbType.Int32, tOPSPTransferCeilingDetail.TOPSPTransferPaymentID)

            DbCommandWrapper.AddInParameter("@TOPSPTransferCeilingID", DbType.Int32, Me.GetRefObject(tOPSPTransferCeilingDetail.TOPSPTransferCeiling))
            DbCommandWrapper.AddInParameter("@SparepartBillingID", DbType.Int32, Me.GetRefObject(tOPSPTransferCeilingDetail.SparePartBilling))
            DbCommandWrapper.AddInParameter("@TOPSPTransferPaymentID", DbType.Int32, Me.GetRefObject(tOPSPTransferCeilingDetail.TOPSPTransferPayment))


            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, tOPSPTransferCeilingDetail.Amount)
            DbCommandWrapper.AddInParameter("@IsIncome", DbType.Int16, tOPSPTransferCeilingDetail.IsIncome)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, tOPSPTransferCeilingDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPTransferCeilingDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, tOPSPTransferCeilingDetail.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, tOPSPTransferCeilingDetail.LastUpdatedTime)


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

            Dim tOPSPTransferCeilingDetail As TOPSPTransferCeilingDetail = CType(obj, TOPSPTransferCeilingDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPTransferCeilingDetail.ID)
            'DbCommandWrapper.AddInParameter("@TOPSPTransferCeilingID", DbType.Int32, tOPSPTransferCeilingDetail.TOPSPTransferCeilingID)
            'DbCommandWrapper.AddInParameter("@SparepartBillingID", DbType.Int32, tOPSPTransferCeilingDetail.SparepartBillingID)
            'DbCommandWrapper.AddInParameter("@TOPSPTransferPaymentID", DbType.Int32, tOPSPTransferCeilingDetail.TOPSPTransferPaymentID)
            DbCommandWrapper.AddInParameter("@TOPSPTransferCeilingID", DbType.Int32, Me.GetRefObject(tOPSPTransferCeilingDetail.TOPSPTransferCeiling))
            DbCommandWrapper.AddInParameter("@SparepartBillingID", DbType.Int32, Me.GetRefObject(tOPSPTransferCeilingDetail.SparePartBilling))
            DbCommandWrapper.AddInParameter("@TOPSPTransferPaymentID", DbType.Int32, Me.GetRefObject(tOPSPTransferCeilingDetail.TOPSPTransferPayment))

            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, tOPSPTransferCeilingDetail.Amount)
            DbCommandWrapper.AddInParameter("@IsIncome", DbType.Int16, tOPSPTransferCeilingDetail.IsIncome)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, tOPSPTransferCeilingDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPTransferCeilingDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, tOPSPTransferCeilingDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, tOPSPTransferCeilingDetail.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TOPSPTransferCeilingDetail

            Dim tOPSPTransferCeilingDetail As TOPSPTransferCeilingDetail = New TOPSPTransferCeilingDetail

            tOPSPTransferCeilingDetail.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("TOPSPTransferCeilingID")) Then tOPSPTransferCeilingDetail.TOPSPTransferCeilingID = CType(dr("TOPSPTransferCeilingID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("TOPSPTransferCeilingID")) Then
                tOPSPTransferCeilingDetail.TOPSPTransferCeiling = New TOPSPTransferCeiling(CType(dr("TOPSPTransferCeilingID"), Integer))
            End If

            'If Not dr.IsDBNull(dr.GetOrdinal("SparepartBillingID")) Then tOPSPTransferCeilingDetail.SparepartBillingID = CType(dr("SparepartBillingID"), Integer)


            If Not dr.IsDBNull(dr.GetOrdinal("SparepartBillingID")) Then
                tOPSPTransferCeilingDetail.SparePartBilling = New SparePartBilling(CType(dr("SparepartBillingID"), Integer))
            End If


            'If Not dr.IsDBNull(dr.GetOrdinal("TOPSPTransferPaymentID")) Then tOPSPTransferCeilingDetail.TOPSPTransferPaymentID = CType(dr("TOPSPTransferPaymentID"), Integer)


            If Not dr.IsDBNull(dr.GetOrdinal("TOPSPTransferPaymentID")) Then
                tOPSPTransferCeilingDetail.TOPSPTransferPayment = New TOPSPTransferPayment(CType(dr("TOPSPTransferPaymentID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then tOPSPTransferCeilingDetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("IsIncome")) Then tOPSPTransferCeilingDetail.IsIncome = CType(dr("IsIncome"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then tOPSPTransferCeilingDetail.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then tOPSPTransferCeilingDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then tOPSPTransferCeilingDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then tOPSPTransferCeilingDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then tOPSPTransferCeilingDetail.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then tOPSPTransferCeilingDetail.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            Return tOPSPTransferCeilingDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(TOPSPTransferCeilingDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TOPSPTransferCeilingDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TOPSPTransferCeilingDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

