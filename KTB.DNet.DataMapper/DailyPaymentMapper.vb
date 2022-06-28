
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DailyPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 12/3/2010 - 9:14:37 PM
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

    Public Class DailyPaymentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDailyPayment"
        Private m_UpdateStatement As String = "up_UpdateDailyPayment"
        Private m_RetrieveStatement As String = "up_RetrieveDailyPayment"
        Private m_RetrieveListStatement As String = "up_RetrieveDailyPaymentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDailyPayment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dailyPayment As DailyPayment = Nothing
            While dr.Read

                dailyPayment = Me.CreateObject(dr)

            End While

            Return dailyPayment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dailyPaymentList As ArrayList = New ArrayList

            While dr.Read
                Dim dailyPayment As DailyPayment = Me.CreateObject(dr)
                dailyPaymentList.Add(dailyPayment)
            End While

            Return dailyPaymentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dailyPayment As DailyPayment = CType(obj, DailyPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dailyPayment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dailyPayment As DailyPayment = CType(obj, DailyPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)

            DBCommandWrapper.AddInParameter("@DocNumber", DbType.AnsiString, dailyPayment.DocNumber)
            DbCommandWrapper.AddInParameter("@FiscalYear", DbType.Int16, dailyPayment.FiscalYear)
            DbCommandWrapper.AddInParameter("@DocDate", DbType.DateTime, dailyPayment.DocDate)
            DbCommandWrapper.AddInParameter("@BaselineDate", DbType.DateTime, dailyPayment.BaselineDate)
            DbCommandWrapper.AddInParameter("@SlipNumber", DbType.AnsiString, dailyPayment.SlipNumber)
            DbCommandWrapper.AddInParameter("@ReceiptNumber", DbType.AnsiString, dailyPayment.ReceiptNumber)
            DbCommandWrapper.AddInParameter("@SAPCreator", DbType.AnsiString, dailyPayment.SAPCreator)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, dailyPayment.Amount)
            DbCommandWrapper.AddInParameter("@RejectStatus", DbType.Byte, dailyPayment.RejectStatus)
            DbCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, dailyPayment.EffectiveDate)
            DbCommandWrapper.AddInParameter("@AcceleratedGyro", DbType.Int16, dailyPayment.AcceleratedGyro)
            DbCommandWrapper.AddInParameter("@AcceleratedDate", DbType.DateTime, dailyPayment.AcceleratedDate)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, dailyPayment.Remark)
            DbCommandWrapper.AddInParameter("@IsReversed", DbType.Int16, dailyPayment.IsReversed)
            DbCommandWrapper.AddInParameter("@IsCleared", DbType.Int16, dailyPayment.IsCleared)
            DbCommandWrapper.AddInParameter("@Reason", DbType.AnsiString, dailyPayment.Reason)
            DbCommandWrapper.AddInParameter("@EntryDate", DbType.DateTime, dailyPayment.EntryDate)
            DbCommandWrapper.AddInParameter("@PIC", DbType.AnsiString, dailyPayment.PIC)
            DbCommandWrapper.AddInParameter("@GyroType", DbType.Int16, dailyPayment.GyroType)
            DbCommandWrapper.AddInParameter("@EntryType", DbType.Int16, dailyPayment.EntryType)
            DbCommandWrapper.AddInParameter("@Ref1", DbType.AnsiString, dailyPayment.Ref1)
            DbCommandWrapper.AddInParameter("@Ref2", DbType.AnsiString, dailyPayment.Ref2)
            DbCommandWrapper.AddInParameter("@Ref3", DbType.AnsiString, dailyPayment.Ref3)
            DbCommandWrapper.AddInParameter("@AcceleratorID", DbType.Int32, dailyPayment.AcceleratorID)
            DbCommandWrapper.AddInParameter("@CessieID", DbType.Int32, dailyPayment.CessieID)
            DBCommandWrapper.AddInParameter("@CessieTime", DbType.DateTime, dailyPayment.CessieTime)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, dailyPayment.Status)
            DBCommandWrapper.AddInParameter("@BankID", DbType.Int32, dailyPayment.BankID)
            DBCommandWrapper.AddInParameter("@NumOfTransfered", DbType.Int32, dailyPayment.NumOfTransfered)
            DBCommandWrapper.AddInParameter("@RemarkStatus", DbType.Int16, dailyPayment.RemarkStatus)
            DBCommandWrapper.AddInParameter("@ReUpload", DbType.Int16, dailyPayment.ReUpload)
            DBCommandWrapper.AddInParameter("@LastUploadedTime", DbType.DateTime, dailyPayment.LastUploadedTime)
            DBCommandWrapper.AddInParameter("@LastUploadedBy", DbType.AnsiString, dailyPayment.LastUploadedBy)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dailyPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dailyPayment.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PaymentPurposeID", DbType.Byte, Me.GetRefObject(dailyPayment.PaymentPurpose))
            DbCommandWrapper.AddInParameter("@POID", DbType.Int32, Me.GetRefObject(dailyPayment.POHeader))
            DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, Me.GetRefObject(dailyPayment.SalesOrder))
            DBCommandWrapper.AddInParameter("@DailyPaymentHeaderID", DbType.Int32, Me.GetRefObject(dailyPayment.DailyPaymentHeader))
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

            Dim dailyPayment As DailyPayment = CType(obj, DailyPayment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dailyPayment.ID)

            DBCommandWrapper.AddInParameter("@DocNumber", DbType.AnsiString, dailyPayment.DocNumber)
            DbCommandWrapper.AddInParameter("@FiscalYear", DbType.Int16, dailyPayment.FiscalYear)
            DbCommandWrapper.AddInParameter("@DocDate", DbType.DateTime, dailyPayment.DocDate)
            DbCommandWrapper.AddInParameter("@BaselineDate", DbType.DateTime, dailyPayment.BaselineDate)
            DbCommandWrapper.AddInParameter("@SlipNumber", DbType.AnsiString, dailyPayment.SlipNumber)
            DbCommandWrapper.AddInParameter("@ReceiptNumber", DbType.AnsiString, dailyPayment.ReceiptNumber)
            DbCommandWrapper.AddInParameter("@SAPCreator", DbType.AnsiString, dailyPayment.SAPCreator)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, dailyPayment.Amount)
            DbCommandWrapper.AddInParameter("@RejectStatus", DbType.Byte, dailyPayment.RejectStatus)
            DbCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, dailyPayment.EffectiveDate)
            DbCommandWrapper.AddInParameter("@AcceleratedGyro", DbType.Int16, dailyPayment.AcceleratedGyro)
            DbCommandWrapper.AddInParameter("@AcceleratedDate", DbType.DateTime, dailyPayment.AcceleratedDate)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, dailyPayment.Remark)
            DbCommandWrapper.AddInParameter("@IsReversed", DbType.Int16, dailyPayment.IsReversed)
            DbCommandWrapper.AddInParameter("@IsCleared", DbType.Int16, dailyPayment.IsCleared)
            DbCommandWrapper.AddInParameter("@Reason", DbType.AnsiString, dailyPayment.Reason)
            DbCommandWrapper.AddInParameter("@EntryDate", DbType.DateTime, dailyPayment.EntryDate)
            DbCommandWrapper.AddInParameter("@PIC", DbType.AnsiString, dailyPayment.PIC)
            DbCommandWrapper.AddInParameter("@GyroType", DbType.Int16, dailyPayment.GyroType)
            DbCommandWrapper.AddInParameter("@EntryType", DbType.Int16, dailyPayment.EntryType)
            DbCommandWrapper.AddInParameter("@Ref1", DbType.AnsiString, dailyPayment.Ref1)
            DbCommandWrapper.AddInParameter("@Ref2", DbType.AnsiString, dailyPayment.Ref2)
            DbCommandWrapper.AddInParameter("@Ref3", DbType.AnsiString, dailyPayment.Ref3)
            DbCommandWrapper.AddInParameter("@AcceleratorID", DbType.Int32, dailyPayment.AcceleratorID)
            DbCommandWrapper.AddInParameter("@CessieID", DbType.Int32, dailyPayment.CessieID)
            DBCommandWrapper.AddInParameter("@CessieTime", DbType.DateTime, dailyPayment.CessieTime)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, dailyPayment.Status)
            DbCommandWrapper.AddInParameter("@BankID", DbType.Int32, dailyPayment.BankID)
            DBCommandWrapper.AddInParameter("@NumOfTransfered", DbType.Int32, dailyPayment.NumOfTransfered)
            DBCommandWrapper.AddInParameter("@RemarkStatus", DbType.Int16, dailyPayment.RemarkStatus)
            DBCommandWrapper.AddInParameter("@ReUpload", DbType.Int16, dailyPayment.ReUpload)
            DBCommandWrapper.AddInParameter("@LastUploadedTime", DbType.DateTime, dailyPayment.LastUploadedTime)
            DBCommandWrapper.AddInParameter("@LastUploadedBy", DbType.AnsiString, dailyPayment.LastUploadedBy)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dailyPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dailyPayment.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@PaymentPurposeID", DbType.Byte, Me.GetRefObject(dailyPayment.PaymentPurpose))
            DbCommandWrapper.AddInParameter("@POID", DbType.Int32, Me.GetRefObject(dailyPayment.POHeader))
            DbCommandWrapper.AddInParameter("@SalesOrderID", DbType.Int32, Me.GetRefObject(dailyPayment.SalesOrder))
            DBCommandWrapper.AddInParameter("@DailyPaymentHeaderID", DbType.Int32, Me.GetRefObject(dailyPayment.DailyPaymentHeader))
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DailyPayment

            Dim dailyPayment As DailyPayment = New DailyPayment

            dailyPayment.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("DocNumber")) Then dailyPayment.DocNumber = dr("DocNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FiscalYear")) Then dailyPayment.FiscalYear = CType(dr("FiscalYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DocDate")) Then dailyPayment.DocDate = CType(dr("DocDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BaselineDate")) Then dailyPayment.BaselineDate = CType(dr("BaselineDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SlipNumber")) Then dailyPayment.SlipNumber = dr("SlipNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptNumber")) Then dailyPayment.ReceiptNumber = dr("ReceiptNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SAPCreator")) Then dailyPayment.SAPCreator = dr("SAPCreator").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then dailyPayment.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RejectStatus")) Then dailyPayment.RejectStatus = CType(dr("RejectStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("EffectiveDate")) Then dailyPayment.EffectiveDate = CType(dr("EffectiveDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AcceleratedGyro")) Then dailyPayment.AcceleratedGyro = CType(dr("AcceleratedGyro"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AcceleratedDate")) Then dailyPayment.AcceleratedDate = CType(dr("AcceleratedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then dailyPayment.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsReversed")) Then dailyPayment.IsReversed = CType(dr("IsReversed"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsCleared")) Then dailyPayment.IsCleared = CType(dr("IsCleared"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Reason")) Then dailyPayment.Reason = dr("Reason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EntryDate")) Then dailyPayment.EntryDate = CType(dr("EntryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PIC")) Then dailyPayment.PIC = dr("PIC").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GyroType")) Then dailyPayment.GyroType = CType(dr("GyroType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("EntryType")) Then dailyPayment.EntryType = CType(dr("EntryType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Ref1")) Then dailyPayment.Ref1 = dr("Ref1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Ref2")) Then dailyPayment.Ref2 = dr("Ref2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Ref3")) Then dailyPayment.Ref3 = dr("Ref3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AcceleratorID")) Then dailyPayment.AcceleratorID = CType(dr("AcceleratorID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CessieID")) Then dailyPayment.CessieID = CType(dr("CessieID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CessieTime")) Then dailyPayment.CessieTime = CType(dr("CessieTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then dailyPayment.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("BankID")) Then dailyPayment.BankID = CType(dr("BankID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NumOfTransfered")) Then dailyPayment.NumOfTransfered = CType(dr("NumOfTransfered"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RemarkStatus")) Then dailyPayment.RemarkStatus = CType(dr("RemarkStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ReUpload")) Then dailyPayment.ReUpload = CType(dr("ReUpload"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUploadedTime")) Then dailyPayment.LastUploadedTime = CType(dr("LastUploadedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUploadedBy")) Then dailyPayment.LastUploadedBy = dr("LastUploadedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dailyPayment.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dailyPayment.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dailyPayment.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dailyPayment.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dailyPayment.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentPurposeID")) Then
                dailyPayment.PaymentPurpose = New PaymentPurpose(CType(dr("PaymentPurposeID"), Byte))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("POID")) Then
                dailyPayment.POHeader = New POHeader(CType(dr("POID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderID")) Then
                dailyPayment.SalesOrder = New SalesOrder(CType(dr("SalesOrderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DailyPaymentHeaderID")) Then
                'dailyPayment.DailyPaymentHeader.ID = CType(dr("DailyPaymentHeaderID"), Integer)
                dailyPayment.DailyPaymentHeader = New DailyPaymentHeader(CType(dr("DailyPaymentHeaderID"), Integer))
                dailyPayment.DailyPaymentHeader.MarkUnLoaded()
            End If

            Return dailyPayment

        End Function

        Private Sub SetTableName()

            If Not (GetType(DailyPayment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DailyPayment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DailyPayment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

