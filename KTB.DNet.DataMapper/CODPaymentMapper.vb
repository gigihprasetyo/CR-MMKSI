#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CODPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/11/2020 - 10:49:59 AM
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

    Public Class CODPaymentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCODPayment"
        Private m_UpdateStatement As String = "up_UpdateCODPayment"
        Private m_RetrieveStatement As String = "up_RetrieveCODPayment"
        Private m_RetrieveListStatement As String = "up_RetrieveCODPaymentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCODPayment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim cODPayment As CODPayment = Nothing
            While dr.Read

                cODPayment = Me.CreateObject(dr)

            End While

            Return cODPayment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim cODPaymentList As ArrayList = New ArrayList

            While dr.Read
                Dim cODPayment As CODPayment = Me.CreateObject(dr)
                cODPaymentList.Add(cODPayment)
            End While

            Return cODPaymentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim cODPayment As CODPayment = CType(obj, CODPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, cODPayment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim cODPayment As CODPayment = CType(obj, CODPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, cODPayment.DealerCode)
            DbCommandWrapper.AddInParameter("@SalesOrderNo", DbType.AnsiString, cODPayment.SalesOrderNo)
            DbCommandWrapper.AddInParameter("@DeliveryNo", DbType.AnsiString, cODPayment.DeliveryNo)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, cODPayment.OrderType)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, cODPayment.SODate)
            DbCommandWrapper.AddInParameter("@RetailAmount", DbType.Currency, cODPayment.RetailAmount)
            DbCommandWrapper.AddInParameter("@DepositC2Amount", DbType.Currency, cODPayment.DepositC2Amount)
            DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, cODPayment.PPNAmount)
            DbCommandWrapper.AddInParameter("@Total", DbType.Currency, cODPayment.Total)
            DbCommandWrapper.AddInParameter("@RODeposit", DbType.Currency, cODPayment.RODeposit)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, cODPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, cODPayment.LastUpdateBy)
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

            Dim cODPayment As CODPayment = CType(obj, CODPayment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, cODPayment.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, cODPayment.DealerCode)
            DbCommandWrapper.AddInParameter("@SalesOrderNo", DbType.AnsiString, cODPayment.SalesOrderNo)
            DbCommandWrapper.AddInParameter("@DeliveryNo", DbType.AnsiString, cODPayment.DeliveryNo)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, cODPayment.OrderType)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, cODPayment.SODate)
            DbCommandWrapper.AddInParameter("@RetailAmount", DbType.Currency, cODPayment.RetailAmount)
            DbCommandWrapper.AddInParameter("@DepositC2Amount", DbType.Currency, cODPayment.DepositC2Amount)
            DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, cODPayment.PPNAmount)
            DbCommandWrapper.AddInParameter("@Total", DbType.Currency, cODPayment.Total)
            DbCommandWrapper.AddInParameter("@RODeposit", DbType.Currency, cODPayment.RODeposit)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, cODPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, cODPayment.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CODPayment

            Dim cODPayment As CODPayment = New CODPayment

            cODPayment.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then cODPayment.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderNo")) Then cODPayment.SalesOrderNo = dr("SalesOrderNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryNo")) Then cODPayment.DeliveryNo = dr("DeliveryNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then cODPayment.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SODate")) Then cODPayment.SODate = CType(dr("SODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RetailAmount")) Then cODPayment.RetailAmount = CType(dr("RetailAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DepositC2Amount")) Then cODPayment.DepositC2Amount = CType(dr("DepositC2Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPNAmount")) Then cODPayment.PPNAmount = CType(dr("PPNAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Total")) Then cODPayment.Total = CType(dr("Total"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RODeposit")) Then cODPayment.RODeposit = CType(dr("RODeposit"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then cODPayment.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then cODPayment.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then cODPayment.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then cODPayment.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then cODPayment.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return cODPayment

        End Function

        Private Sub SetTableName()

            If Not (GetType(CODPayment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CODPayment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CODPayment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
