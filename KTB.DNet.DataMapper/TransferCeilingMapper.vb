
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TransferCeiling Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 28/07/2016 - 10:59:27
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

    Public Class TransferCeilingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTransferCeiling"
        Private m_UpdateStatement As String = "up_UpdateTransferCeiling"
        Private m_RetrieveStatement As String = "up_RetrieveTransferCeiling"
        Private m_RetrieveListStatement As String = "up_RetrieveTransferCeilingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTransferCeiling"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim transferCeiling As TransferCeiling = Nothing
            While dr.Read

                transferCeiling = Me.CreateObject(dr)

            End While

            Return transferCeiling

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim transferCeilingList As ArrayList = New ArrayList

            While dr.Read
                Dim transferCeiling As TransferCeiling = Me.CreateObject(dr)
                transferCeilingList.Add(transferCeiling)
            End While

            Return transferCeilingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim transferCeiling As TransferCeiling = CType(obj, TransferCeiling)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, transferCeiling.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim transferCeiling As TransferCeiling = CType(obj, TransferCeiling)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, transferCeiling.CreditAccount)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, transferCeiling.PaymentType)
            DbCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, transferCeiling.EffectiveDate)
            DbCommandWrapper.AddInParameter("@BalanceBefore", DbType.Currency, transferCeiling.BalanceBefore)
            DbCommandWrapper.AddInParameter("@AvailableCeiling", DbType.Currency, transferCeiling.AvailableCeiling)
            DbCommandWrapper.AddInParameter("@LastSyncDate", DbType.DateTime, transferCeiling.LastSyncDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, transferCeiling.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, transferCeiling.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, transferCeiling.LastUpdatedTime)

            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(transferCeiling.ProductCategory))

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

            Dim transferCeiling As TransferCeiling = CType(obj, TransferCeiling)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, transferCeiling.ID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, transferCeiling.CreditAccount)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, transferCeiling.PaymentType)
            DbCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, transferCeiling.EffectiveDate)
            DbCommandWrapper.AddInParameter("@BalanceBefore", DbType.Currency, transferCeiling.BalanceBefore)
            DbCommandWrapper.AddInParameter("@AvailableCeiling", DbType.Currency, transferCeiling.AvailableCeiling)
            DbCommandWrapper.AddInParameter("@LastSyncDate", DbType.DateTime, transferCeiling.LastSyncDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, transferCeiling.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, transferCeiling.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(transferCeiling.ProductCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TransferCeiling

            Dim transferCeiling As TransferCeiling = New TransferCeiling

            transferCeiling.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then transferCeiling.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then transferCeiling.PaymentType = CType(dr("PaymentType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("EffectiveDate")) Then transferCeiling.EffectiveDate = CType(dr("EffectiveDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BalanceBefore")) Then transferCeiling.BalanceBefore = CType(dr("BalanceBefore"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AvailableCeiling")) Then transferCeiling.AvailableCeiling = CType(dr("AvailableCeiling"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LastSyncDate")) Then transferCeiling.LastSyncDate = CType(dr("LastSyncDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then transferCeiling.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then transferCeiling.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then transferCeiling.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then transferCeiling.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then transferCeiling.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                transferCeiling.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Short))
            End If

            Return transferCeiling

        End Function

        Private Sub SetTableName()

            If Not (GetType(TransferCeiling) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TransferCeiling), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TransferCeiling).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

