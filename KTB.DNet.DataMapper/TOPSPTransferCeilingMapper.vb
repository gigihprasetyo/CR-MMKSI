
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPTransferCeiling Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/14/2018 - 10:45:10 AM
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

    Public Class TOPSPTransferCeilingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTOPSPTransferCeiling"
        Private m_UpdateStatement As String = "up_UpdateTOPSPTransferCeiling"
        Private m_RetrieveStatement As String = "up_RetrieveTOPSPTransferCeiling"
        Private m_RetrieveListStatement As String = "up_RetrieveTOPSPTransferCeilingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTOPSPTransferCeiling"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim tOPSPTransferCeiling As TOPSPTransferCeiling = Nothing
            While dr.Read

                tOPSPTransferCeiling = Me.CreateObject(dr)

            End While

            Return tOPSPTransferCeiling

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim tOPSPTransferCeilingList As ArrayList = New ArrayList

            While dr.Read
                Dim tOPSPTransferCeiling As TOPSPTransferCeiling = Me.CreateObject(dr)
                tOPSPTransferCeilingList.Add(tOPSPTransferCeiling)
            End While

            Return tOPSPTransferCeilingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPTransferCeiling As TOPSPTransferCeiling = CType(obj, TOPSPTransferCeiling)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPTransferCeiling.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPTransferCeiling As TOPSPTransferCeiling = CType(obj, TOPSPTransferCeiling)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, tOPSPTransferCeiling.CreditAccount)
            'DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, tOPSPTransferCeiling.ProductCategoryID)
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(tOPSPTransferCeiling.ProductCategory))

            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, tOPSPTransferCeiling.PaymentType)
            DbCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, tOPSPTransferCeiling.EffectiveDate)
            DbCommandWrapper.AddInParameter("@BalanceBefore", DbType.Currency, tOPSPTransferCeiling.BalanceBefore)
            DbCommandWrapper.AddInParameter("@AvailableCeiling", DbType.Currency, tOPSPTransferCeiling.AvailableCeiling)
            DbCommandWrapper.AddInParameter("@LastSyncDate", DbType.DateTime, tOPSPTransferCeiling.LastSyncDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPTransferCeiling.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, tOPSPTransferCeiling.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, tOPSPTransferCeiling.LastUpdatedTime)


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

            Dim tOPSPTransferCeiling As TOPSPTransferCeiling = CType(obj, TOPSPTransferCeiling)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPTransferCeiling.ID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, tOPSPTransferCeiling.CreditAccount)
            'DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, tOPSPTransferCeiling.ProductCategoryID)
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(tOPSPTransferCeiling.ProductCategory))
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, tOPSPTransferCeiling.PaymentType)
            DbCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, tOPSPTransferCeiling.EffectiveDate)
            DbCommandWrapper.AddInParameter("@BalanceBefore", DbType.Currency, tOPSPTransferCeiling.BalanceBefore)
            DbCommandWrapper.AddInParameter("@AvailableCeiling", DbType.Currency, tOPSPTransferCeiling.AvailableCeiling)
            DbCommandWrapper.AddInParameter("@LastSyncDate", DbType.DateTime, tOPSPTransferCeiling.LastSyncDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPTransferCeiling.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, tOPSPTransferCeiling.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, tOPSPTransferCeiling.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TOPSPTransferCeiling

            Dim tOPSPTransferCeiling As TOPSPTransferCeiling = New TOPSPTransferCeiling

            tOPSPTransferCeiling.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then tOPSPTransferCeiling.CreditAccount = dr("CreditAccount").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then tOPSPTransferCeiling.ProductCategoryID = CType(dr("ProductCategoryID"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                tOPSPTransferCeiling.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Short))
            End If


            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then tOPSPTransferCeiling.PaymentType = CType(dr("PaymentType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("EffectiveDate")) Then tOPSPTransferCeiling.EffectiveDate = CType(dr("EffectiveDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BalanceBefore")) Then tOPSPTransferCeiling.BalanceBefore = CType(dr("BalanceBefore"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AvailableCeiling")) Then tOPSPTransferCeiling.AvailableCeiling = CType(dr("AvailableCeiling"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LastSyncDate")) Then tOPSPTransferCeiling.LastSyncDate = CType(dr("LastSyncDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then tOPSPTransferCeiling.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then tOPSPTransferCeiling.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then tOPSPTransferCeiling.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then tOPSPTransferCeiling.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then tOPSPTransferCeiling.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            Return tOPSPTransferCeiling

        End Function

        Private Sub SetTableName()

            If Not (GetType(TOPSPTransferCeiling) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TOPSPTransferCeiling), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TOPSPTransferCeiling).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

