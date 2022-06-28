#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositLine Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 15/11/2005 - 14:45:55
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

    Public Class DepositLineMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositLine"
        Private m_UpdateStatement As String = "up_UpdateDepositLine"
        Private m_RetrieveStatement As String = "up_RetrieveDepositLine"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositLineList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositLine"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositLine As DepositLine = Nothing
            While dr.Read

                depositLine = Me.CreateObject(dr)

            End While

            Return depositLine

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositLineList As ArrayList = New ArrayList

            While dr.Read
                Dim depositLine As DepositLine = Me.CreateObject(dr)
                depositLineList.Add(depositLine)
            End While

            Return depositLineList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositLine As DepositLine = CType(obj, DepositLine)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositLine.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositLine As DepositLine = CType(obj, DepositLine)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DocumentNo", DbType.AnsiString, depositLine.DocumentNo)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, depositLine.PostingDate)
            DbCommandWrapper.AddInParameter("@ClearingDate", DbType.DateTime, depositLine.ClearingDate)
            DbCommandWrapper.AddInParameter("@Debit", DbType.Currency, depositLine.Debit)
            DbCommandWrapper.AddInParameter("@Credit", DbType.Currency, depositLine.Credit)
            DbCommandWrapper.AddInParameter("@ReferenceNo", DbType.AnsiString, depositLine.ReferenceNo)
            DbCommandWrapper.AddInParameter("@InvoiceNo", DbType.AnsiString, depositLine.InvoiceNo)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, depositLine.Remark)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, depositLine.PaymentType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositLine.RowStatus)

            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositLine.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DepositID", DbType.Int32, Me.GetRefObject(depositLine.Deposit))

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

            Dim depositLine As DepositLine = CType(obj, DepositLine)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositLine.ID)
            DbCommandWrapper.AddInParameter("@DocumentNo", DbType.AnsiString, depositLine.DocumentNo)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, depositLine.PostingDate)
            DbCommandWrapper.AddInParameter("@ClearingDate", DbType.DateTime, depositLine.ClearingDate)
            DbCommandWrapper.AddInParameter("@Debit", DbType.Currency, depositLine.Debit)
            DbCommandWrapper.AddInParameter("@Credit", DbType.Currency, depositLine.Credit)
            DbCommandWrapper.AddInParameter("@ReferenceNo", DbType.AnsiString, depositLine.ReferenceNo)
            DbCommandWrapper.AddInParameter("@InvoiceNo", DbType.AnsiString, depositLine.InvoiceNo)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, depositLine.Remark)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, depositLine.PaymentType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositLine.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositLine.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DepositID", DbType.Int32, Me.GetRefObject(depositLine.Deposit))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositLine

            Dim depositLine As DepositLine = New DepositLine

            depositLine.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentNo")) Then depositLine.DocumentNo = dr("DocumentNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostingDate")) Then depositLine.PostingDate = CType(dr("PostingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ClearingDate")) Then depositLine.ClearingDate = CType(dr("ClearingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Debit")) Then depositLine.Debit = CType(dr("Debit"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Credit")) Then depositLine.Credit = CType(dr("Credit"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNo")) Then depositLine.ReferenceNo = dr("ReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceNo")) Then depositLine.InvoiceNo = dr("InvoiceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then depositLine.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then depositLine.PaymentType = CType(dr("PaymentType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositLine.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositLine.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositLine.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositLine.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositLine.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DepositID")) Then
                depositLine.Deposit = New Deposit(CType(dr("DepositID"), Integer))
            End If

            Return depositLine

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositLine) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositLine), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositLine).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
