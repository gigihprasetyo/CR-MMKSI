
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBPencairanHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 5/13/2016 - 8:55:59 AM
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

    Public Class DepositBPencairanHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositBPencairanHeader"
        Private m_UpdateStatement As String = "up_UpdateDepositBPencairanHeader"
        Private m_RetrieveStatement As String = "up_RetrieveDepositBPencairanHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositBPencairanHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositBPencairanHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositBPencairanHeader As DepositBPencairanHeader = Nothing
            While dr.Read

                depositBPencairanHeader = Me.CreateObject(dr)

            End While

            Return depositBPencairanHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositBPencairanHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim depositBPencairanHeader As DepositBPencairanHeader = Me.CreateObject(dr)
                depositBPencairanHeaderList.Add(depositBPencairanHeader)
            End While

            Return depositBPencairanHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBPencairanHeader As DepositBPencairanHeader = CType(obj, DepositBPencairanHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBPencairanHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBPencairanHeader As DepositBPencairanHeader = CType(obj, DepositBPencairanHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@NoReferensi", DbType.AnsiString, depositBPencairanHeader.NoReferensi)
            DbCommandWrapper.AddInParameter("@TipePengajuan", DbType.Byte, depositBPencairanHeader.TipePengajuan)
            DbCommandWrapper.AddInParameter("@DealerAmount", DbType.Currency, depositBPencairanHeader.DealerAmount)
            DbCommandWrapper.AddInParameter("@ApprovalAmount", DbType.Currency, depositBPencairanHeader.ApprovalAmount)
            DbCommandWrapper.AddInParameter("@KTBReason", DbType.AnsiString, depositBPencairanHeader.KTBReason)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, depositBPencairanHeader.Keterangan)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, depositBPencairanHeader.Status)
            DbCommandWrapper.AddInParameter("@NoReg", DbType.AnsiString, depositBPencairanHeader.NoReg)
            DbCommandWrapper.AddInParameter("@Flag", DbType.Int16, depositBPencairanHeader.Flag)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBPencairanHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositBPencairanHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerBankAccountID", DbType.Int32, Me.GetRefObject(depositBPencairanHeader.DealerBankAccount))
            DbCommandWrapper.AddInParameter("@DepositBDebitNoteID", DbType.Int32, Me.GetRefObject(depositBPencairanHeader.DepositBDebitNote))
            DbCommandWrapper.AddInParameter("@DepositBInterestHID", DbType.Int32, Me.GetRefObject(depositBPencairanHeader.DepositBInterestHeader))
            DbCommandWrapper.AddInParameter("@KewajibanHeaderID", DbType.Int32, Me.GetRefObject(depositBPencairanHeader.DepositBKewajibanHeader))
            DbCommandWrapper.AddInParameter("@IndentPartEqHeaderID", DbType.Int32, Me.GetRefObject(depositBPencairanHeader.IndentPartHeader))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(depositBPencairanHeader.ProductCategory))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(depositBPencairanHeader.Dealer))

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

            Dim depositBPencairanHeader As DepositBPencairanHeader = CType(obj, DepositBPencairanHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBPencairanHeader.ID)
            DbCommandWrapper.AddInParameter("@NoReferensi", DbType.AnsiString, depositBPencairanHeader.NoReferensi)
            DbCommandWrapper.AddInParameter("@TipePengajuan", DbType.Byte, depositBPencairanHeader.TipePengajuan)
            DbCommandWrapper.AddInParameter("@DealerAmount", DbType.Currency, depositBPencairanHeader.DealerAmount)
            DbCommandWrapper.AddInParameter("@ApprovalAmount", DbType.Currency, depositBPencairanHeader.ApprovalAmount)
            DbCommandWrapper.AddInParameter("@KTBReason", DbType.AnsiString, depositBPencairanHeader.KTBReason)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, depositBPencairanHeader.Keterangan)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, depositBPencairanHeader.Status)
            DbCommandWrapper.AddInParameter("@NoReg", DbType.AnsiString, depositBPencairanHeader.NoReg)
            DbCommandWrapper.AddInParameter("@Flag", DbType.Int16, depositBPencairanHeader.Flag)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBPencairanHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositBPencairanHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerBankAccountID", DbType.Int32, Me.GetRefObject(depositBPencairanHeader.DealerBankAccount))
            DbCommandWrapper.AddInParameter("@DepositBDebitNoteID", DbType.Int32, Me.GetRefObject(depositBPencairanHeader.DepositBDebitNote))
            DbCommandWrapper.AddInParameter("@DepositBInterestHID", DbType.Int32, Me.GetRefObject(depositBPencairanHeader.DepositBInterestHeader))
            DbCommandWrapper.AddInParameter("@KewajibanHeaderID", DbType.Int32, Me.GetRefObject(depositBPencairanHeader.DepositBKewajibanHeader))
            DbCommandWrapper.AddInParameter("@IndentPartEqHeaderID", DbType.Int32, Me.GetRefObject(depositBPencairanHeader.IndentPartHeader))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(depositBPencairanHeader.ProductCategory))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(depositBPencairanHeader.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositBPencairanHeader

            Dim depositBPencairanHeader As DepositBPencairanHeader = New DepositBPencairanHeader

            depositBPencairanHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoReferensi")) Then depositBPencairanHeader.NoReferensi = dr("NoReferensi").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TipePengajuan")) Then depositBPencairanHeader.TipePengajuan = CType(dr("TipePengajuan"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerAmount")) Then depositBPencairanHeader.DealerAmount = CType(dr("DealerAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovalAmount")) Then depositBPencairanHeader.ApprovalAmount = CType(dr("ApprovalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("KTBReason")) Then depositBPencairanHeader.KTBReason = dr("KTBReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Keterangan")) Then depositBPencairanHeader.Keterangan = dr("Keterangan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then depositBPencairanHeader.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("NoReg")) Then depositBPencairanHeader.NoReg = dr("NoReg").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Flag")) Then depositBPencairanHeader.Flag = dr("Flag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositBPencairanHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositBPencairanHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositBPencairanHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositBPencairanHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositBPencairanHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBankAccountID")) Then
                depositBPencairanHeader.DealerBankAccount = New DealerBankAccount(CType(dr("DealerBankAccountID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DepositBDebitNoteID")) Then
                depositBPencairanHeader.DepositBDebitNote = New DepositBDebitNote(CType(dr("DepositBDebitNoteID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DepositBInterestHID")) Then
                depositBPencairanHeader.DepositBInterestHeader = New DepositBInterestHeader(CType(dr("DepositBInterestHID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("KewajibanHeaderID")) Then
                depositBPencairanHeader.DepositBKewajibanHeader = New DepositBKewajibanHeader(CType(dr("KewajibanHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("IndentPartEqHeaderID")) Then
                depositBPencairanHeader.IndentPartHeader = New IndentPartHeader(CType(dr("IndentPartEqHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                depositBPencairanHeader.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                depositBPencairanHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return depositBPencairanHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositBPencairanHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositBPencairanHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositBPencairanHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

