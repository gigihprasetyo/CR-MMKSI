#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CODOutstanding Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/11/2020 - 10:50:29 AM
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

    Public Class CODOutstandingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCODOutstanding"
        Private m_UpdateStatement As String = "up_UpdateCODOutstanding"
        Private m_RetrieveStatement As String = "up_RetrieveCODOutstanding"
        Private m_RetrieveListStatement As String = "up_RetrieveCODOutstandingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCODOutstanding"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim cODOutstanding As CODOutstanding = Nothing
            While dr.Read

                cODOutstanding = Me.CreateObject(dr)

            End While

            Return cODOutstanding

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim cODOutstandingList As ArrayList = New ArrayList

            While dr.Read
                Dim cODOutstanding As CODOutstanding = Me.CreateObject(dr)
                cODOutstandingList.Add(cODOutstanding)
            End While

            Return cODOutstandingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim cODOutstanding As CODOutstanding = CType(obj, CODOutstanding)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, cODOutstanding.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim cODOutstanding As CODOutstanding = CType(obj, CODOutstanding)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, cODOutstanding.DealerCode)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.AnsiString, cODOutstanding.PaymentType)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, cODOutstanding.DONumber)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, cODOutstanding.BillingNumber)
            DbCommandWrapper.AddInParameter("@BIllingDate", DbType.DateTime, cODOutstanding.BIllingDate)
            DbCommandWrapper.AddInParameter("@BillingCreateDate", DbType.DateTime, cODOutstanding.BillingCreateDate)
            DbCommandWrapper.AddInParameter("@NetAmount", DbType.Currency, cODOutstanding.NetAmount)
            DbCommandWrapper.AddInParameter("@TaxAmount", DbType.Currency, cODOutstanding.TaxAmount)
            DbCommandWrapper.AddInParameter("@C2Amount", DbType.Currency, cODOutstanding.C2Amount)
            DbCommandWrapper.AddInParameter("@Total", DbType.Currency, cODOutstanding.Total)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, cODOutstanding.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, cODOutstanding.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim cODOutstanding As CODOutstanding = CType(obj, CODOutstanding)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, cODOutstanding.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, cODOutstanding.DealerCode)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.AnsiString, cODOutstanding.PaymentType)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, cODOutstanding.DONumber)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, cODOutstanding.BillingNumber)
            DbCommandWrapper.AddInParameter("@BIllingDate", DbType.DateTime, cODOutstanding.BIllingDate)
            DbCommandWrapper.AddInParameter("@BillingCreateDate", DbType.DateTime, cODOutstanding.BillingCreateDate)
            DbCommandWrapper.AddInParameter("@NetAmount", DbType.Currency, cODOutstanding.NetAmount)
            DbCommandWrapper.AddInParameter("@TaxAmount", DbType.Currency, cODOutstanding.TaxAmount)
            DbCommandWrapper.AddInParameter("@C2Amount", DbType.Currency, cODOutstanding.C2Amount)
            DbCommandWrapper.AddInParameter("@Total", DbType.Currency, cODOutstanding.Total)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, cODOutstanding.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, cODOutstanding.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CODOutstanding

            Dim cODOutstanding As CODOutstanding = New CODOutstanding

            cODOutstanding.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then cODOutstanding.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then cODOutstanding.PaymentType = dr("PaymentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then cODOutstanding.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNumber")) Then cODOutstanding.BillingNumber = dr("BillingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BIllingDate")) Then cODOutstanding.BIllingDate = CType(dr("BIllingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingCreateDate")) Then cODOutstanding.BillingCreateDate = CType(dr("BillingCreateDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NetAmount")) Then cODOutstanding.NetAmount = CType(dr("NetAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TaxAmount")) Then cODOutstanding.TaxAmount = CType(dr("TaxAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("C2Amount")) Then cODOutstanding.C2Amount = CType(dr("C2Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Total")) Then cODOutstanding.Total = CType(dr("Total"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then cODOutstanding.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then cODOutstanding.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then cODOutstanding.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then cODOutstanding.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then cODOutstanding.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return cODOutstanding

        End Function

        Private Sub SetTableName()

            If Not (GetType(CODOutstanding) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CODOutstanding), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CODOutstanding).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
