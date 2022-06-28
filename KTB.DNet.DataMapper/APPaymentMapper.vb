
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MitraisTeam
'// PURPOSE       : APPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 24/03/2018 - 10:36:16
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

    Public Class APPaymentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAPPayment"
        Private m_UpdateStatement As String = "up_UpdateAPPayment"
        Private m_RetrieveStatement As String = "up_RetrieveAPPayment"
        Private m_RetrieveListStatement As String = "up_RetrieveAPPaymentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAPPayment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim aPPayment As APPayment = Nothing
            While dr.Read

                aPPayment = Me.CreateObject(dr)

            End While

            Return aPPayment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim aPPaymentList As ArrayList = New ArrayList

            While dr.Read
                Dim aPPayment As APPayment = Me.CreateObject(dr)
                aPPaymentList.Add(aPPayment)
            End While

            Return aPPaymentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim aPPayment As APPayment = CType(obj, APPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, aPPayment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim aPPayment As APPayment = CType(obj, APPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, aPPayment.Owner)
            DbCommandWrapper.AddInParameter("@APPaymentNo", DbType.AnsiString, aPPayment.APPaymentNo)
            DbCommandWrapper.AddInParameter("@APReferenceNo", DbType.AnsiString, aPPayment.APReferenceNo)
            DbCommandWrapper.AddInParameter("@APVoucherReferenceNo", DbType.AnsiString, aPPayment.APVoucherReferenceNo)
            DbCommandWrapper.AddInParameter("@AppliedToDocument", DbType.Currency, aPPayment.AppliedToDocument)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, aPPayment.BU)
            DbCommandWrapper.AddInParameter("@Cancelled", DbType.Boolean, aPPayment.Cancelled)
            DbCommandWrapper.AddInParameter("@CashAndBank", DbType.AnsiString, aPPayment.CashAndBank)
            DbCommandWrapper.AddInParameter("@MethodOfPayment", DbType.AnsiString, aPPayment.MethodOfPayment)
            DbCommandWrapper.AddInParameter("@AvailableBalance", DbType.Currency, aPPayment.AvailableBalance)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, aPPayment.State)
            DbCommandWrapper.AddInParameter("@TotalChangeAmount", DbType.Currency, aPPayment.TotalChangeAmount)
            DbCommandWrapper.AddInParameter("@TotalPaymentAmount", DbType.Currency, aPPayment.TotalPaymentAmount)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, aPPayment.TransactionDate)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, aPPayment.Type)
            DbCommandWrapper.AddInParameter("@VendorDescription", DbType.AnsiString, aPPayment.VendorDescription)
            DbCommandWrapper.AddInParameter("@Vendor", DbType.AnsiString, aPPayment.Vendor)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, aPPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, aPPayment.LastUpdateBy)
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

            Dim aPPayment As APPayment = CType(obj, APPayment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, aPPayment.ID)
            DbCommandWrapper.AddInParameter("@Owner", DbType.AnsiString, aPPayment.Owner)
            DbCommandWrapper.AddInParameter("@APPaymentNo", DbType.AnsiString, aPPayment.APPaymentNo)
            DbCommandWrapper.AddInParameter("@APReferenceNo", DbType.AnsiString, aPPayment.APReferenceNo)
            DbCommandWrapper.AddInParameter("@APVoucherReferenceNo", DbType.AnsiString, aPPayment.APVoucherReferenceNo)
            DbCommandWrapper.AddInParameter("@AppliedToDocument", DbType.Currency, aPPayment.AppliedToDocument)
            DbCommandWrapper.AddInParameter("@BU", DbType.AnsiString, aPPayment.BU)
            DbCommandWrapper.AddInParameter("@Cancelled", DbType.Boolean, aPPayment.Cancelled)
            DbCommandWrapper.AddInParameter("@CashAndBank", DbType.AnsiString, aPPayment.CashAndBank)
            DbCommandWrapper.AddInParameter("@MethodOfPayment", DbType.AnsiString, aPPayment.MethodOfPayment)
            DbCommandWrapper.AddInParameter("@AvailableBalance", DbType.Currency, aPPayment.AvailableBalance)
            DbCommandWrapper.AddInParameter("@State", DbType.Int16, aPPayment.State)
            DbCommandWrapper.AddInParameter("@TotalChangeAmount", DbType.Currency, aPPayment.TotalChangeAmount)
            DbCommandWrapper.AddInParameter("@TotalPaymentAmount", DbType.Currency, aPPayment.TotalPaymentAmount)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, aPPayment.TransactionDate)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, aPPayment.Type)
            DbCommandWrapper.AddInParameter("@VendorDescription", DbType.AnsiString, aPPayment.VendorDescription)
            DbCommandWrapper.AddInParameter("@Vendor", DbType.AnsiString, aPPayment.Vendor)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, aPPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, aPPayment.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As APPayment

            Dim aPPayment As APPayment = New APPayment

            aPPayment.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then aPPayment.Owner = dr("Owner").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("APPaymentNo")) Then aPPayment.APPaymentNo = dr("APPaymentNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("APReferenceNo")) Then aPPayment.APReferenceNo = dr("APReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("APVoucherReferenceNo")) Then aPPayment.APVoucherReferenceNo = dr("APVoucherReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AppliedToDocument")) Then aPPayment.AppliedToDocument = CType(dr("AppliedToDocument"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BU")) Then aPPayment.BU = dr("BU").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Cancelled")) Then aPPayment.Cancelled = CType(dr("Cancelled"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("CashAndBank")) Then aPPayment.CashAndBank = dr("CashAndBank").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MethodOfPayment")) Then aPPayment.MethodOfPayment = dr("MethodOfPayment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AvailableBalance")) Then aPPayment.AvailableBalance = CType(dr("AvailableBalance"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("State")) Then aPPayment.State = CType(dr("State"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalChangeAmount")) Then aPPayment.TotalChangeAmount = CType(dr("TotalChangeAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalPaymentAmount")) Then aPPayment.TotalPaymentAmount = CType(dr("TotalPaymentAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then aPPayment.TransactionDate = CType(dr("TransactionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then aPPayment.Type = CType(dr("Type"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("VendorDescription")) Then aPPayment.VendorDescription = dr("VendorDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Vendor")) Then aPPayment.Vendor = dr("Vendor").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then aPPayment.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then aPPayment.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then aPPayment.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then aPPayment.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then aPPayment.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return aPPayment

        End Function

        Private Sub SetTableName()

            If Not (GetType(APPayment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(APPayment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(APPayment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

