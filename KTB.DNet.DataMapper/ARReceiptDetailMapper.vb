
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MitraisTeam
'// PURPOSE       : ARReceiptDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 23/03/2018 - 16:17:09
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

    Public Class ARReceiptDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertARReceiptDetail"
        Private m_UpdateStatement As String = "up_UpdateARReceiptDetail"
        Private m_RetrieveStatement As String = "up_RetrieveARReceiptDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveARReceiptDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteARReceiptDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim aRReceiptDetail As ARReceiptDetail = Nothing
            While dr.Read

                aRReceiptDetail = Me.CreateObject(dr)

            End While

            Return aRReceiptDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim aRReceiptDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim aRReceiptDetail As ARReceiptDetail = Me.CreateObject(dr)
                aRReceiptDetailList.Add(aRReceiptDetail)
            End While

            Return aRReceiptDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim aRReceiptDetail As ARReceiptDetail = CType(obj, ARReceiptDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, aRReceiptDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim aRReceiptDetail As ARReceiptDetail = CType(obj, ARReceiptDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, aRReceiptDetail.Owner)
            DbCommandWrapper.AddInParameter("@DetailNo", DbType.AnsiString, aRReceiptDetail.DetailNo)
            DbCommandWrapper.AddInParameter("@ARReceiptNo", DbType.AnsiString, aRReceiptDetail.ARReceiptNo)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, aRReceiptDetail.BU)
            DbCommandWrapper.AddInParameter("@ChangeAmount", DbType.Currency, aRReceiptDetail.ChangeAmount)
            DbCommandWrapper.AddInParameter("@Customer", DbType.AnsiString, aRReceiptDetail.Customer)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, aRReceiptDetail.Description)
            DbCommandWrapper.AddInParameter("@DifferenceValue", DbType.Double, aRReceiptDetail.DifferenceValue)
            DbCommandWrapper.AddInParameter("@InvoiceNo", DbType.AnsiString, aRReceiptDetail.InvoiceNo)
            DbCommandWrapper.AddInParameter("@OrderDate", DbType.DateTime, aRReceiptDetail.OrderDate)
            DbCommandWrapper.AddInParameter("@OrderNo", DbType.AnsiString, aRReceiptDetail.OrderNo)
            DbCommandWrapper.AddInParameter("@OrderNoSO", DbType.AnsiString, aRReceiptDetail.OrderNoSO)
            DbCommandWrapper.AddInParameter("@OrderNoUVSO", DbType.AnsiString, aRReceiptDetail.OrderNoUVSO)
            DbCommandWrapper.AddInParameter("@OrderNoWO", DbType.AnsiString, aRReceiptDetail.OrderNoWO)
            DbCommandWrapper.AddInParameter("@OutstandingBalance", DbType.Currency, aRReceiptDetail.OutstandingBalance)
            DbCommandWrapper.AddInParameter("@PaidBackToCustomer", DbType.Boolean, aRReceiptDetail.PaidBackToCustomer)
            DbCommandWrapper.AddInParameter("@ReceiptAmount", DbType.Currency, aRReceiptDetail.ReceiptAmount)
            DbCommandWrapper.AddInParameter("@RemainingBalance", DbType.Currency, aRReceiptDetail.RemainingBalance)
            DbCommandWrapper.AddInParameter("@SourceType", DbType.Int16, aRReceiptDetail.SourceType)
            DbCommandWrapper.AddInParameter("@TransactionDocument", DbType.AnsiString, aRReceiptDetail.TransactionDocument)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, aRReceiptDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, aRReceiptDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ARReceiptID", DbType.Int32, Me.GetRefObject(aRReceiptDetail.ARReceipt))

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

            Dim aRReceiptDetail As ARReceiptDetail = CType(obj, ARReceiptDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, aRReceiptDetail.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, aRReceiptDetail.Owner)
            DbCommandWrapper.AddInParameter("@DetailNo", DbType.AnsiString, aRReceiptDetail.DetailNo)
            DbCommandWrapper.AddInParameter("@ARReceiptNo", DbType.AnsiString, aRReceiptDetail.ARReceiptNo)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, aRReceiptDetail.BU)
            DbCommandWrapper.AddInParameter("@ChangeAmount", DbType.Currency, aRReceiptDetail.ChangeAmount)
            DbCommandWrapper.AddInParameter("@Customer", DbType.AnsiString, aRReceiptDetail.Customer)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, aRReceiptDetail.Description)
            DbCommandWrapper.AddInParameter("@DifferenceValue", DbType.Double, aRReceiptDetail.DifferenceValue)
            DbCommandWrapper.AddInParameter("@InvoiceNo", DbType.AnsiString, aRReceiptDetail.InvoiceNo)
            DbCommandWrapper.AddInParameter("@OrderDate", DbType.DateTime, aRReceiptDetail.OrderDate)
            DbCommandWrapper.AddInParameter("@OrderNo", DbType.AnsiString, aRReceiptDetail.OrderNo)
            DbCommandWrapper.AddInParameter("@OrderNoSO", DbType.AnsiString, aRReceiptDetail.OrderNoSO)
            DbCommandWrapper.AddInParameter("@OrderNoUVSO", DbType.AnsiString, aRReceiptDetail.OrderNoUVSO)
            DbCommandWrapper.AddInParameter("@OrderNoWO", DbType.AnsiString, aRReceiptDetail.OrderNoWO)
            DbCommandWrapper.AddInParameter("@OutstandingBalance", DbType.Currency, aRReceiptDetail.OutstandingBalance)
            DbCommandWrapper.AddInParameter("@PaidBackToCustomer", DbType.Boolean, aRReceiptDetail.PaidBackToCustomer)
            DbCommandWrapper.AddInParameter("@ReceiptAmount", DbType.Currency, aRReceiptDetail.ReceiptAmount)
            DbCommandWrapper.AddInParameter("@RemainingBalance", DbType.Currency, aRReceiptDetail.RemainingBalance)
            DbCommandWrapper.AddInParameter("@SourceType", DbType.Int16, aRReceiptDetail.SourceType)
            DbCommandWrapper.AddInParameter("@TransactionDocument", DbType.AnsiString, aRReceiptDetail.TransactionDocument)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, aRReceiptDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, aRReceiptDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ARReceiptID", DbType.Int32, Me.GetRefObject(aRReceiptDetail.ARReceipt))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ARReceiptDetail

            Dim aRReceiptDetail As ARReceiptDetail = New ARReceiptDetail

            aRReceiptDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then aRReceiptDetail.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DetailNo")) Then aRReceiptDetail.DetailNo = dr("DetailNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ARReceiptNo")) Then aRReceiptDetail.ARReceiptNo = dr("ARReceiptNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BU")) Then aRReceiptDetail.BU = dr("BU").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChangeAmount")) Then aRReceiptDetail.ChangeAmount = CType(dr("ChangeAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Customer")) Then aRReceiptDetail.Customer = dr("Customer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then aRReceiptDetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DifferenceValue")) Then aRReceiptDetail.DifferenceValue = CType(dr("DifferenceValue"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceNo")) Then aRReceiptDetail.InvoiceNo = dr("InvoiceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderDate")) Then aRReceiptDetail.OrderDate = CType(dr("OrderDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OrderNo")) Then aRReceiptDetail.OrderNo = dr("OrderNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderNoSO")) Then aRReceiptDetail.OrderNoSO = dr("OrderNoSO").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderNoUVSO")) Then aRReceiptDetail.OrderNoUVSO = dr("OrderNoUVSO").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderNoWO")) Then aRReceiptDetail.OrderNoWO = dr("OrderNoWO").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OutstandingBalance")) Then aRReceiptDetail.OutstandingBalance = CType(dr("OutstandingBalance"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaidBackToCustomer")) Then aRReceiptDetail.PaidBackToCustomer = CType(dr("PaidBackToCustomer"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptAmount")) Then aRReceiptDetail.ReceiptAmount = CType(dr("ReceiptAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RemainingBalance")) Then aRReceiptDetail.RemainingBalance = CType(dr("RemainingBalance"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SourceType")) Then aRReceiptDetail.SourceType = CType(dr("SourceType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDocument")) Then aRReceiptDetail.TransactionDocument = dr("TransactionDocument").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then aRReceiptDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then aRReceiptDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then aRReceiptDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then aRReceiptDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then aRReceiptDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ARReceiptID")) Then
                aRReceiptDetail.ARReceipt = New ARReceipt(CType(dr("ARReceiptID"), Integer))
            End If

            Return aRReceiptDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(ARReceiptDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ARReceiptDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ARReceiptDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

