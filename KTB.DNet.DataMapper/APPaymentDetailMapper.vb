
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MitraisTeam
'// PURPOSE       : APPaymentDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 24/03/2018 - 10:36:52
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

    Public Class APPaymentDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAPPaymentDetail"
        Private m_UpdateStatement As String = "up_UpdateAPPaymentDetail"
        Private m_RetrieveStatement As String = "up_RetrieveAPPaymentDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveAPPaymentDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAPPaymentDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim aPPaymentDetail As APPaymentDetail = Nothing
            While dr.Read

                aPPaymentDetail = Me.CreateObject(dr)

            End While

            Return aPPaymentDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim aPPaymentDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim aPPaymentDetail As APPaymentDetail = Me.CreateObject(dr)
                aPPaymentDetailList.Add(aPPaymentDetail)
            End While

            Return aPPaymentDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim aPPaymentDetail As APPaymentDetail = CType(obj, APPaymentDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, aPPaymentDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim aPPaymentDetail As APPaymentDetail = CType(obj, APPaymentDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, aPPaymentDetail.Owner)
            DbCommandWrapper.AddInParameter("@APPaymentDetailNo", DbType.AnsiString, aPPaymentDetail.APPaymentDetailNo)
            DbCommandWrapper.AddInParameter("@APPaymentNo", DbType.AnsiString, aPPaymentDetail.APPaymentNo)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, aPPaymentDetail.BU)
            DbCommandWrapper.AddInParameter("@ChangeAmount", DbType.Currency, aPPaymentDetail.ChangeAmount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, aPPaymentDetail.Description)
            DbCommandWrapper.AddInParameter("@DifferenceValue", DbType.Double, aPPaymentDetail.DifferenceValue)
            DbCommandWrapper.AddInParameter("@ExternalDocumentNo", DbType.AnsiString, aPPaymentDetail.ExternalDocumentNo)
            DbCommandWrapper.AddInParameter("@ExternalDocumentType", DbType.Int16, aPPaymentDetail.ExternalDocumentType)
            DbCommandWrapper.AddInParameter("@APVoucherNo", DbType.AnsiString, aPPaymentDetail.APVoucherNo)
            DbCommandWrapper.AddInParameter("@OrderDate", DbType.DateTime, aPPaymentDetail.OrderDate)
            DbCommandWrapper.AddInParameter("@OrderNoNVSOReferral", DbType.AnsiString, aPPaymentDetail.OrderNoNVSOReferral)
            DbCommandWrapper.AddInParameter("@OrderNoOutsourceWorkOrder", DbType.AnsiString, aPPaymentDetail.OrderNoOutsourceWorkOrder)
            DbCommandWrapper.AddInParameter("@OrderNo", DbType.AnsiString, aPPaymentDetail.OrderNo)
            DbCommandWrapper.AddInParameter("@OrderNoUVSOReferral", DbType.AnsiString, aPPaymentDetail.OrderNoUVSOReferral)
            DbCommandWrapper.AddInParameter("@OutstandingBalance", DbType.Currency, aPPaymentDetail.OutstandingBalance)
            DbCommandWrapper.AddInParameter("@PaymentAmount", DbType.Currency, aPPaymentDetail.PaymentAmount)
            DbCommandWrapper.AddInParameter("@PaymentSlipNo", DbType.AnsiString, aPPaymentDetail.PaymentSlipNo)
            DbCommandWrapper.AddInParameter("@ReceiptFromVendor", DbType.Boolean, aPPaymentDetail.ReceiptFromVendor)
            DbCommandWrapper.AddInParameter("@RemainingBalance", DbType.Currency, aPPaymentDetail.RemainingBalance)
            DbCommandWrapper.AddInParameter("@SourceType", DbType.Int16, aPPaymentDetail.SourceType)
            DbCommandWrapper.AddInParameter("@TransactionDocument", DbType.AnsiString, aPPaymentDetail.TransactionDocument)
            DbCommandWrapper.AddInParameter("@Vendor", DbType.AnsiString, aPPaymentDetail.Vendor)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, aPPaymentDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, aPPaymentDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@APPaymentID", DbType.Int32, Me.GetRefObject(aPPaymentDetail.APPayment))

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

            Dim aPPaymentDetail As APPaymentDetail = CType(obj, APPaymentDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, aPPaymentDetail.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, aPPaymentDetail.Owner)
            DbCommandWrapper.AddInParameter("@APPaymentDetailNo", DbType.AnsiString, aPPaymentDetail.APPaymentDetailNo)
            DbCommandWrapper.AddInParameter("@APPaymentNo", DbType.AnsiString, aPPaymentDetail.APPaymentNo)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, aPPaymentDetail.BU)
            DbCommandWrapper.AddInParameter("@ChangeAmount", DbType.Currency, aPPaymentDetail.ChangeAmount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, aPPaymentDetail.Description)
            DbCommandWrapper.AddInParameter("@DifferenceValue", DbType.Double, aPPaymentDetail.DifferenceValue)
            DbCommandWrapper.AddInParameter("@ExternalDocumentNo", DbType.AnsiString, aPPaymentDetail.ExternalDocumentNo)
            DbCommandWrapper.AddInParameter("@ExternalDocumentType", DbType.Int16, aPPaymentDetail.ExternalDocumentType)
            DbCommandWrapper.AddInParameter("@APVoucherNo", DbType.AnsiString, aPPaymentDetail.APVoucherNo)
            DbCommandWrapper.AddInParameter("@OrderDate", DbType.DateTime, aPPaymentDetail.OrderDate)
            DbCommandWrapper.AddInParameter("@OrderNoNVSOReferral", DbType.AnsiString, aPPaymentDetail.OrderNoNVSOReferral)
            DbCommandWrapper.AddInParameter("@OrderNoOutsourceWorkOrder", DbType.AnsiString, aPPaymentDetail.OrderNoOutsourceWorkOrder)
            DbCommandWrapper.AddInParameter("@OrderNo", DbType.AnsiString, aPPaymentDetail.OrderNo)
            DbCommandWrapper.AddInParameter("@OrderNoUVSOReferral", DbType.AnsiString, aPPaymentDetail.OrderNoUVSOReferral)
            DbCommandWrapper.AddInParameter("@OutstandingBalance", DbType.Currency, aPPaymentDetail.OutstandingBalance)
            DbCommandWrapper.AddInParameter("@PaymentAmount", DbType.Currency, aPPaymentDetail.PaymentAmount)
            DbCommandWrapper.AddInParameter("@PaymentSlipNo", DbType.AnsiString, aPPaymentDetail.PaymentSlipNo)
            DbCommandWrapper.AddInParameter("@ReceiptFromVendor", DbType.Boolean, aPPaymentDetail.ReceiptFromVendor)
            DbCommandWrapper.AddInParameter("@RemainingBalance", DbType.Currency, aPPaymentDetail.RemainingBalance)
            DbCommandWrapper.AddInParameter("@SourceType", DbType.Int16, aPPaymentDetail.SourceType)
            DbCommandWrapper.AddInParameter("@TransactionDocument", DbType.AnsiString, aPPaymentDetail.TransactionDocument)
            DbCommandWrapper.AddInParameter("@Vendor", DbType.AnsiString, aPPaymentDetail.Vendor)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, aPPaymentDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, aPPaymentDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@APPaymentID", DbType.Int32, Me.GetRefObject(aPPaymentDetail.APPayment))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As APPaymentDetail

            Dim aPPaymentDetail As APPaymentDetail = New APPaymentDetail

            aPPaymentDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then aPPaymentDetail.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("APPaymentDetailNo")) Then aPPaymentDetail.APPaymentDetailNo = dr("APPaymentDetailNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("APPaymentNo")) Then aPPaymentDetail.APPaymentNo = dr("APPaymentNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BU")) Then aPPaymentDetail.BU = dr("BU").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChangeAmount")) Then aPPaymentDetail.ChangeAmount = CType(dr("ChangeAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then aPPaymentDetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DifferenceValue")) Then aPPaymentDetail.DifferenceValue = CType(dr("DifferenceValue"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("ExternalDocumentNo")) Then aPPaymentDetail.ExternalDocumentNo = dr("ExternalDocumentNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ExternalDocumentType")) Then aPPaymentDetail.ExternalDocumentType = CType(dr("ExternalDocumentType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("APVoucherNo")) Then aPPaymentDetail.APVoucherNo = dr("APVoucherNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderDate")) Then aPPaymentDetail.OrderDate = CType(dr("OrderDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OrderNoNVSOReferral")) Then aPPaymentDetail.OrderNoNVSOReferral = dr("OrderNoNVSOReferral").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderNoOutsourceWorkOrder")) Then aPPaymentDetail.OrderNoOutsourceWorkOrder = dr("OrderNoOutsourceWorkOrder").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderNo")) Then aPPaymentDetail.OrderNo = dr("OrderNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderNoUVSOReferral")) Then aPPaymentDetail.OrderNoUVSOReferral = dr("OrderNoUVSOReferral").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OutstandingBalance")) Then aPPaymentDetail.OutstandingBalance = CType(dr("OutstandingBalance"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentAmount")) Then aPPaymentDetail.PaymentAmount = CType(dr("PaymentAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentSlipNo")) Then aPPaymentDetail.PaymentSlipNo = dr("PaymentSlipNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptFromVendor")) Then aPPaymentDetail.ReceiptFromVendor = CType(dr("ReceiptFromVendor"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("RemainingBalance")) Then aPPaymentDetail.RemainingBalance = CType(dr("RemainingBalance"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SourceType")) Then aPPaymentDetail.SourceType = CType(dr("SourceType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDocument")) Then aPPaymentDetail.TransactionDocument = dr("TransactionDocument").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Vendor")) Then aPPaymentDetail.Vendor = dr("Vendor").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then aPPaymentDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then aPPaymentDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then aPPaymentDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then aPPaymentDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then aPPaymentDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("APPaymentID")) Then
                aPPaymentDetail.APPayment = New APPayment(CType(dr("APPaymentID"), Integer))
            End If

            Return aPPaymentDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(APPaymentDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(APPaymentDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(APPaymentDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

