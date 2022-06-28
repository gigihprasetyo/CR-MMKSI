
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventReportReceipt Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 03/10/2019 - 13:53:24
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

    Public Class BabitEventReportReceiptMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitEventReportReceipt"
        Private m_UpdateStatement As String = "up_UpdateBabitEventReportReceipt"
        Private m_RetrieveStatement As String = "up_RetrieveBabitEventReportReceipt"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitEventReportReceiptList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitEventReportReceipt"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitEventReportReceipt As BabitEventReportReceipt = Nothing
            While dr.Read

                babitEventReportReceipt = Me.CreateObject(dr)

            End While

            Return babitEventReportReceipt

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitEventReportReceiptList As ArrayList = New ArrayList

            While dr.Read
                Dim babitEventReportReceipt As BabitEventReportReceipt = Me.CreateObject(dr)
                babitEventReportReceiptList.Add(babitEventReportReceipt)
            End While

            Return babitEventReportReceiptList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventReportReceipt As BabitEventReportReceipt = CType(obj, BabitEventReportReceipt)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventReportReceipt.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventReportReceipt As BabitEventReportReceipt = CType(obj, BabitEventReportReceipt)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, babitEventReportReceipt.Status)
            DbCommandWrapper.AddInParameter("@ReceiptNo", DbType.AnsiString, babitEventReportReceipt.ReceiptNo)
            DbCommandWrapper.AddInParameter("@ReceiptDate", DbType.DateTime, babitEventReportReceipt.ReceiptDate)
            DbCommandWrapper.AddInParameter("@FakturPajakNo", DbType.AnsiString, babitEventReportReceipt.FakturPajakNo)
            DbCommandWrapper.AddInParameter("@FakturPajakDate", DbType.DateTime, babitEventReportReceipt.FakturPajakDate)
            DbCommandWrapper.AddInParameter("@ClaimAmount", DbType.Currency, babitEventReportReceipt.ClaimAmount)
            DbCommandWrapper.AddInParameter("@VATTotal", DbType.Currency, babitEventReportReceipt.VATTotal)
            DbCommandWrapper.AddInParameter("@PPHTotal", DbType.Currency, babitEventReportReceipt.PPHTotal)
            DbCommandWrapper.AddInParameter("@TotalReceiptAmount", DbType.Currency, babitEventReportReceipt.TotalReceiptAmount)
            DbCommandWrapper.AddInParameter("@SignName", DbType.AnsiString, babitEventReportReceipt.SignName)
            DbCommandWrapper.AddInParameter("@SignPosition", DbType.AnsiString, babitEventReportReceipt.SignPosition)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventReportReceipt.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitEventReportReceipt.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitEventReportHeaderID", DbType.Int32, Me.GetRefObject(babitEventReportReceipt.BabitEventReportHeader))
            DbCommandWrapper.AddInParameter("@DealerBankAccountID", DbType.Int16, Me.GetRefObject(babitEventReportReceipt.DealerBankAccount))
            DbCommandWrapper.AddInParameter("@MasterAccruedID", DbType.Int32, Me.GetRefObject(babitEventReportReceipt.MasterAccrued))

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

            Dim babitEventReportReceipt As BabitEventReportReceipt = CType(obj, BabitEventReportReceipt)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventReportReceipt.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, babitEventReportReceipt.Status)
            DbCommandWrapper.AddInParameter("@ReceiptNo", DbType.AnsiString, babitEventReportReceipt.ReceiptNo)
            DbCommandWrapper.AddInParameter("@ReceiptDate", DbType.DateTime, babitEventReportReceipt.ReceiptDate)
            DbCommandWrapper.AddInParameter("@FakturPajakNo", DbType.AnsiString, babitEventReportReceipt.FakturPajakNo)
            DbCommandWrapper.AddInParameter("@FakturPajakDate", DbType.DateTime, babitEventReportReceipt.FakturPajakDate)
            DbCommandWrapper.AddInParameter("@ClaimAmount", DbType.Currency, babitEventReportReceipt.ClaimAmount)
            DbCommandWrapper.AddInParameter("@VATTotal", DbType.Currency, babitEventReportReceipt.VATTotal)
            DbCommandWrapper.AddInParameter("@PPHTotal", DbType.Currency, babitEventReportReceipt.PPHTotal)
            DbCommandWrapper.AddInParameter("@TotalReceiptAmount", DbType.Currency, babitEventReportReceipt.TotalReceiptAmount)
            DbCommandWrapper.AddInParameter("@SignName", DbType.AnsiString, babitEventReportReceipt.SignName)
            DbCommandWrapper.AddInParameter("@SignPosition", DbType.AnsiString, babitEventReportReceipt.SignPosition)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventReportReceipt.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitEventReportReceipt.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BabitEventReportHeaderID", DbType.Int32, Me.GetRefObject(babitEventReportReceipt.BabitEventReportHeader))
            DbCommandWrapper.AddInParameter("@DealerBankAccountID", DbType.Int16, Me.GetRefObject(babitEventReportReceipt.DealerBankAccount))
            DbCommandWrapper.AddInParameter("@MasterAccruedID", DbType.Int32, Me.GetRefObject(babitEventReportReceipt.MasterAccrued))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitEventReportReceipt

            Dim babitEventReportReceipt As BabitEventReportReceipt = New BabitEventReportReceipt

            babitEventReportReceipt.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then babitEventReportReceipt.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptNo")) Then babitEventReportReceipt.ReceiptNo = dr("ReceiptNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptDate")) Then babitEventReportReceipt.ReceiptDate = CType(dr("ReceiptDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturPajakNo")) Then babitEventReportReceipt.FakturPajakNo = dr("FakturPajakNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturPajakDate")) Then babitEventReportReceipt.FakturPajakDate = CType(dr("FakturPajakDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimAmount")) Then babitEventReportReceipt.ClaimAmount = CType(dr("ClaimAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("VATTotal")) Then babitEventReportReceipt.VATTotal = CType(dr("VATTotal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPHTotal")) Then babitEventReportReceipt.PPHTotal = CType(dr("PPHTotal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalReceiptAmount")) Then babitEventReportReceipt.TotalReceiptAmount = CType(dr("TotalReceiptAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SignName")) Then babitEventReportReceipt.SignName = dr("SignName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SignPosition")) Then babitEventReportReceipt.SignPosition = dr("SignPosition").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitEventReportReceipt.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitEventReportReceipt.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitEventReportReceipt.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitEventReportReceipt.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitEventReportReceipt.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitEventReportHeaderID")) Then
                babitEventReportReceipt.BabitEventReportHeader = New BabitEventReportHeader(CType(dr("BabitEventReportHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBankAccountID")) Then
                babitEventReportReceipt.DealerBankAccount = New DealerBankAccount(CType(dr("DealerBankAccountID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MasterAccruedID")) Then
                babitEventReportReceipt.MasterAccrued = New MasterAccrued(CType(dr("MasterAccruedID"), Integer))
            End If

            Return babitEventReportReceipt

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitEventReportReceipt) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitEventReportReceipt), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitEventReportReceipt).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

