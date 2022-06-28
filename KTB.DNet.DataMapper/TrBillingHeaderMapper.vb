#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrBillingHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2019 - 4:10:21 PM
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

    Public Class TrBillingHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrBillingHeader"
        Private m_UpdateStatement As String = "up_UpdateTrBillingHeader"
        Private m_RetrieveStatement As String = "up_RetrieveTrBillingHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveTrBillingHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrBillingHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim TrBillingHeader As TrBillingHeader = Nothing
            While dr.Read

                TrBillingHeader = Me.CreateObject(dr)

            End While

            Return TrBillingHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim TrBillingHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim TrBillingHeader As TrBillingHeader = Me.CreateObject(dr)
                TrBillingHeaderList.Add(TrBillingHeader)
            End While

            Return TrBillingHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim TrBillingHeader As TrBillingHeader = CType(obj, TrBillingHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, TrBillingHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim TrBillingHeader As TrBillingHeader = CType(obj, TrBillingHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RequestID", DbType.AnsiString, TrBillingHeader.RequestID)
            DbCommandWrapper.AddInParameter("@FiscalYear", DbType.AnsiString, TrBillingHeader.FiscalYear)
            DbCommandWrapper.AddInParameter("@PathFaktur", DbType.AnsiString, TrBillingHeader.PathFaktur)
            DbCommandWrapper.AddInParameter("@PathDebitNote", DbType.AnsiString, TrBillingHeader.PathDebitNote)
            DbCommandWrapper.AddInParameter("@DebitNoteNumber", DbType.AnsiString, TrBillingHeader.DebitNoteNumber)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, CType(Me.GetRefObject(TrBillingHeader.Dealer), Integer))
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, TrBillingHeader.PaymentType)
            DbCommandWrapper.AddInParameter("@TotalPrice", DbType.Currency, TrBillingHeader.TotalPrice)
            DbCommandWrapper.AddInParameter("@TotalVoucher", DbType.Currency, TrBillingHeader.TotalVoucher)
            DbCommandWrapper.AddInParameter("@PPN", DbType.Currency, TrBillingHeader.PPN)
            DbCommandWrapper.AddInParameter("@Total", DbType.Currency, TrBillingHeader.Total)
            DbCommandWrapper.AddInParameter("@PathSuratKuasa", DbType.AnsiString, TrBillingHeader.PathSuratKuasa)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, TrBillingHeader.DealerCode)
            DbCommandWrapper.AddInParameter("@JVNumber", DbType.AnsiString, TrBillingHeader.JVNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, TrBillingHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, TrBillingHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, TrBillingHeader.LastUpdateBy)
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

            Dim TrBillingHeader As TrBillingHeader = CType(obj, TrBillingHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, TrBillingHeader.ID)
            DbCommandWrapper.AddInParameter("@RequestID", DbType.AnsiString, TrBillingHeader.RequestID)
            DbCommandWrapper.AddInParameter("@FiscalYear", DbType.AnsiString, TrBillingHeader.FiscalYear)
            DbCommandWrapper.AddInParameter("@PathDebitNote", DbType.AnsiString, TrBillingHeader.PathDebitNote)
            DbCommandWrapper.AddInParameter("@PathFaktur", DbType.AnsiString, TrBillingHeader.PathFaktur)
            DbCommandWrapper.AddInParameter("@DebitNoteNumber", DbType.AnsiString, TrBillingHeader.DebitNoteNumber)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, CType(Me.GetRefObject(TrBillingHeader.Dealer), Integer))
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, TrBillingHeader.PaymentType)
            DbCommandWrapper.AddInParameter("@TotalPrice", DbType.Currency, TrBillingHeader.TotalPrice)
            DbCommandWrapper.AddInParameter("@TotalVoucher", DbType.Currency, TrBillingHeader.TotalVoucher)
            DbCommandWrapper.AddInParameter("@PPN", DbType.Currency, TrBillingHeader.PPN)
            DbCommandWrapper.AddInParameter("@Total", DbType.Currency, TrBillingHeader.Total)
            DbCommandWrapper.AddInParameter("@PostedDate", DbType.DateTime, TrBillingHeader.PostedDate)
            DbCommandWrapper.AddInParameter("@PathSuratKuasa", DbType.AnsiString, TrBillingHeader.PathSuratKuasa)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, TrBillingHeader.DealerCode)
            DbCommandWrapper.AddInParameter("@JVNumber", DbType.AnsiString, TrBillingHeader.JVNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, TrBillingHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, TrBillingHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, TrBillingHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrBillingHeader

            Dim TrBillingHeader As TrBillingHeader = New TrBillingHeader

            TrBillingHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestID")) Then TrBillingHeader.RequestID = dr("RequestID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FiscalYear")) Then TrBillingHeader.FiscalYear = dr("FiscalYear").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PathFaktur")) Then TrBillingHeader.PathFaktur = dr("PathFaktur").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PathDebitNote")) Then TrBillingHeader.PathDebitNote = dr("PathDebitNote").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DebitNoteNumber")) Then TrBillingHeader.DebitNoteNumber = dr("DebitNoteNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PathSuratKuasa")) Then TrBillingHeader.PathSuratKuasa = dr("PathSuratKuasa").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then TrBillingHeader.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JVNumber")) Then TrBillingHeader.JVNumber = dr("JVNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then TrBillingHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then TrBillingHeader.PaymentType = CType(dr("PaymentType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalPrice")) Then TrBillingHeader.TotalPrice = CType(dr("TotalPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalVoucher")) Then TrBillingHeader.TotalVoucher = CType(dr("TotalVoucher"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPN")) Then TrBillingHeader.PPN = CType(dr("PPN"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Total")) Then TrBillingHeader.Total = CType(dr("Total"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PostedDate")) Then TrBillingHeader.PostedDate = CType(dr("PostedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then TrBillingHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then TrBillingHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then TrBillingHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then TrBillingHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then TrBillingHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then TrBillingHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return TrBillingHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrBillingHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrBillingHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrBillingHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
