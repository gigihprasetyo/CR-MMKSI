
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitReportReceipt Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 19/09/2019 - 8:43:34
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

    Public Class BabitReportReceiptMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitReportReceipt"
        Private m_UpdateStatement As String = "up_UpdateBabitReportReceipt"
        Private m_RetrieveStatement As String = "up_RetrieveBabitReportReceipt"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitReportReceiptList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitReportReceipt"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitReportReceipt As BabitReportReceipt = Nothing
            While dr.Read

                babitReportReceipt = Me.CreateObject(dr)

            End While

            Return babitReportReceipt

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitReportReceiptList As ArrayList = New ArrayList

            While dr.Read
                Dim babitReportReceipt As BabitReportReceipt = Me.CreateObject(dr)
                babitReportReceiptList.Add(babitReportReceipt)
            End While

            Return babitReportReceiptList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitReportReceipt As BabitReportReceipt = CType(obj, BabitReportReceipt)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitReportReceipt.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitReportReceipt As BabitReportReceipt = CType(obj, BabitReportReceipt)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ReceiptNo", DbType.AnsiString, babitReportReceipt.ReceiptNo)
            DbCommandWrapper.AddInParameter("@ReceiptDate", DbType.DateTime, babitReportReceipt.ReceiptDate)
            DbCommandWrapper.AddInParameter("@FakturPajakNo", DbType.AnsiString, babitReportReceipt.FakturPajakNo)
            DbCommandWrapper.AddInParameter("@FakturPajakDate", DbType.DateTime, babitReportReceipt.FakturPajakDate)
            DbCommandWrapper.AddInParameter("@ClaimAmount", DbType.Currency, babitReportReceipt.ClaimAmount)
            DbCommandWrapper.AddInParameter("@VATTotal", DbType.Currency, babitReportReceipt.VATTotal)
            DbCommandWrapper.AddInParameter("@PPHTotal", DbType.Currency, babitReportReceipt.PPHTotal)
            DbCommandWrapper.AddInParameter("@TotalReceiptAmount", DbType.Currency, babitReportReceipt.TotalReceiptAmount)
            DbCommandWrapper.AddInParameter("@SignName", DbType.AnsiString, babitReportReceipt.SignName)
            DbCommandWrapper.AddInParameter("@SignPosition", DbType.AnsiString, babitReportReceipt.SignPosition)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, babitReportReceipt.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitReportReceipt.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitReportReceipt.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitReportHeaderID", DbType.Int32, Me.GetRefObject(babitReportReceipt.BabitReportHeader))
            DbCommandWrapper.AddInParameter("@MasterAccruedID", DbType.Int32, Me.GetRefObject(babitReportReceipt.MasterAccrued))
            DbCommandWrapper.AddInParameter("@DealerBankAccountID", DbType.Int32, Me.GetRefObject(babitReportReceipt.DealerBankAccount))

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

            Dim babitReportReceipt As BabitReportReceipt = CType(obj, BabitReportReceipt)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitReportReceipt.ID)
            DbCommandWrapper.AddInParameter("@ReceiptNo", DbType.AnsiString, babitReportReceipt.ReceiptNo)
            DbCommandWrapper.AddInParameter("@ReceiptDate", DbType.DateTime, babitReportReceipt.ReceiptDate)
            DbCommandWrapper.AddInParameter("@FakturPajakNo", DbType.AnsiString, babitReportReceipt.FakturPajakNo)
            DbCommandWrapper.AddInParameter("@FakturPajakDate", DbType.DateTime, babitReportReceipt.FakturPajakDate)
            DbCommandWrapper.AddInParameter("@ClaimAmount", DbType.Currency, babitReportReceipt.ClaimAmount)
            DbCommandWrapper.AddInParameter("@VATTotal", DbType.Currency, babitReportReceipt.VATTotal)
            DbCommandWrapper.AddInParameter("@PPHTotal", DbType.Currency, babitReportReceipt.PPHTotal)
            DbCommandWrapper.AddInParameter("@TotalReceiptAmount", DbType.Currency, babitReportReceipt.TotalReceiptAmount)
            DbCommandWrapper.AddInParameter("@SignName", DbType.AnsiString, babitReportReceipt.SignName)
            DbCommandWrapper.AddInParameter("@SignPosition", DbType.AnsiString, babitReportReceipt.SignPosition)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, babitReportReceipt.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitReportReceipt.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitReportReceipt.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BabitReportHeaderID", DbType.Int32, Me.GetRefObject(babitReportReceipt.BabitReportHeader))
            DbCommandWrapper.AddInParameter("@MasterAccruedID", DbType.Int32, Me.GetRefObject(babitReportReceipt.MasterAccrued))
            DbCommandWrapper.AddInParameter("@DealerBankAccountID", DbType.Int32, Me.GetRefObject(babitReportReceipt.DealerBankAccount))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitReportReceipt

            Dim babitReportReceipt As BabitReportReceipt = New BabitReportReceipt

            babitReportReceipt.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptNo")) Then babitReportReceipt.ReceiptNo = dr("ReceiptNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptDate")) Then babitReportReceipt.ReceiptDate = CType(dr("ReceiptDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturPajakNo")) Then babitReportReceipt.FakturPajakNo = dr("FakturPajakNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturPajakDate")) Then babitReportReceipt.FakturPajakDate = CType(dr("FakturPajakDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimAmount")) Then babitReportReceipt.ClaimAmount = CType(dr("ClaimAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("VATTotal")) Then babitReportReceipt.VATTotal = CType(dr("VATTotal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPHTotal")) Then babitReportReceipt.PPHTotal = CType(dr("PPHTotal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalReceiptAmount")) Then babitReportReceipt.TotalReceiptAmount = CType(dr("TotalReceiptAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SignName")) Then babitReportReceipt.SignName = dr("SignName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SignPosition")) Then babitReportReceipt.SignPosition = dr("SignPosition").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then babitReportReceipt.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitReportReceipt.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitReportReceipt.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitReportReceipt.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitReportReceipt.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitReportReceipt.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitReportHeaderID")) Then
                babitReportReceipt.BabitReportHeader = New BabitReportHeader(CType(dr("BabitReportHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MasterAccruedID")) Then
                babitReportReceipt.MasterAccrued = New MasterAccrued(CType(dr("MasterAccruedID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBankAccountID")) Then
                babitReportReceipt.DealerBankAccount = New DealerBankAccount(CType(dr("DealerBankAccountID"), Integer))
            End If

            Return babitReportReceipt

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitReportReceipt) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitReportReceipt), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitReportReceipt).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

