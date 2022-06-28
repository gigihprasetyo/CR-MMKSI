
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPPenalty Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 05/13/2019 - 1:58:01 PM
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

    Public Class TOPSPPenaltyMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTOPSPPenalty"
        Private m_UpdateStatement As String = "up_UpdateTOPSPPenalty"
        Private m_RetrieveStatement As String = "up_RetrieveTOPSPPenalty"
        Private m_RetrieveListStatement As String = "up_RetrieveTOPSPPenaltyList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTOPSPPenalty"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim TOPSPPenalty As TOPSPPenalty = Nothing
            While dr.Read

                TOPSPPenalty = Me.CreateObject(dr)

            End While

            Return TOPSPPenalty

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim TOPSPPenaltyList As ArrayList = New ArrayList

            While dr.Read
                Dim TOPSPPenalty As TOPSPPenalty = Me.CreateObject(dr)
                TOPSPPenaltyList.Add(TOPSPPenalty)
            End While

            Return TOPSPPenaltyList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim TOPSPPenalty As TOPSPPenalty = CType(obj, TOPSPPenalty)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, TOPSPPenalty.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim _tOPSPPenalty As TOPSPPenalty = CType(obj, TOPSPPenalty)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, _tOPSPPenalty.Amount)
            DbCommandWrapper.AddInParameter("@LastDownloadby", DbType.AnsiString, _tOPSPPenalty.LastDownloadby)
            DbCommandWrapper.AddInParameter("@DownloadedDate", DbType.DateTime, _tOPSPPenalty.DownloadedDate)
            DbCommandWrapper.AddInParameter("@DebitMemoNumber", DbType.AnsiString, _tOPSPPenalty.DebitMemoNumber)
            DbCommandWrapper.AddInParameter("@DebitMemoDate", DbType.DateTime, _tOPSPPenalty.DebitMemoDate)
            DbCommandWrapper.AddInParameter("@AccountingNumber", DbType.AnsiString, _tOPSPPenalty.AccountingNumber)
            DbCommandWrapper.AddInParameter("@ClearingNumber", DbType.AnsiString, _tOPSPPenalty.ClearingNumber)
            DbCommandWrapper.AddInParameter("@PaymentDate", DbType.DateTime, _tOPSPPenalty.PaymentDate)
            DbCommandWrapper.AddInParameter("@AmountPayment", DbType.Currency, _tOPSPPenalty.AmountPayment)
            DbCommandWrapper.AddInParameter("@DebitMemoPath", DbType.AnsiString, _tOPSPPenalty.DebitMemoPath)
            DbCommandWrapper.AddInParameter("@NoRegPengembalian", DbType.AnsiString, _tOPSPPenalty.NoRegPengembalian)
            DbCommandWrapper.AddInParameter("@NoBuktiPotong", DbType.AnsiString, _tOPSPPenalty.NoBuktiPotong)
            DbCommandWrapper.AddInParameter("@BuktiPotongDate", DbType.DateTime, _tOPSPPenalty.BuktiPotongDate)
            DbCommandWrapper.AddInParameter("@AmountPPh", DbType.Currency, _tOPSPPenalty.AmountPPh)
            DbCommandWrapper.AddInParameter("@JVNumber", DbType.AnsiString, _tOPSPPenalty.JVNumber)
            DbCommandWrapper.AddInParameter("@UploadFilePath", DbType.AnsiString, _tOPSPPenalty.UploadFilePath)
            DbCommandWrapper.AddInParameter("@StatusPenalty", DbType.Int16, _tOPSPPenalty.StatusPenalty)
            DbCommandWrapper.AddInParameter("@StatusPengembalian", DbType.Int16, _tOPSPPenalty.StatusPengembalian)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, _tOPSPPenalty.Message)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, _tOPSPPenalty.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, _tOPSPPenalty.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(_tOPSPPenalty.Dealer))
            DbCommandWrapper.AddInParameter("@TOPSPTransferPaymentID", DbType.Int32, Me.GetRefObject(_tOPSPPenalty.TOPSPTransferPayment))

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

            Dim _tOPSPPenalty As TOPSPPenalty = CType(obj, TOPSPPenalty)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, _tOPSPPenalty.ID)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, _tOPSPPenalty.Amount)
            DbCommandWrapper.AddInParameter("@LastDownloadby", DbType.AnsiString, _tOPSPPenalty.LastDownloadby)
            DbCommandWrapper.AddInParameter("@DownloadedDate", DbType.DateTime, _tOPSPPenalty.DownloadedDate)
            DbCommandWrapper.AddInParameter("@DebitMemoNumber", DbType.AnsiString, _tOPSPPenalty.DebitMemoNumber)
            DbCommandWrapper.AddInParameter("@DebitMemoDate", DbType.DateTime, _tOPSPPenalty.DebitMemoDate)
            DbCommandWrapper.AddInParameter("@AccountingNumber", DbType.AnsiString, _tOPSPPenalty.AccountingNumber)
            DbCommandWrapper.AddInParameter("@ClearingNumber", DbType.AnsiString, _tOPSPPenalty.ClearingNumber)
            DbCommandWrapper.AddInParameter("@PaymentDate", DbType.DateTime, _tOPSPPenalty.PaymentDate)
            DbCommandWrapper.AddInParameter("@AmountPayment", DbType.Currency, _tOPSPPenalty.AmountPayment)
            DbCommandWrapper.AddInParameter("@DebitMemoPath", DbType.AnsiString, _tOPSPPenalty.DebitMemoPath)
            DbCommandWrapper.AddInParameter("@NoRegPengembalian", DbType.AnsiString, _tOPSPPenalty.NoRegPengembalian)
            DbCommandWrapper.AddInParameter("@NoBuktiPotong", DbType.AnsiString, _tOPSPPenalty.NoBuktiPotong)
            DbCommandWrapper.AddInParameter("@BuktiPotongDate", DbType.DateTime, _tOPSPPenalty.BuktiPotongDate)
            DbCommandWrapper.AddInParameter("@AmountPPh", DbType.Currency, _tOPSPPenalty.AmountPPh)
            DbCommandWrapper.AddInParameter("@JVNumber", DbType.AnsiString, _tOPSPPenalty.JVNumber)
            DbCommandWrapper.AddInParameter("@UploadFilePath", DbType.AnsiString, _tOPSPPenalty.UploadFilePath)
            DbCommandWrapper.AddInParameter("@StatusPenalty", DbType.Int16, _tOPSPPenalty.StatusPenalty)
            DbCommandWrapper.AddInParameter("@StatusPengembalian", DbType.Int16, _tOPSPPenalty.StatusPengembalian)
            DbCommandWrapper.AddInParameter("@Message", DbType.AnsiString, _tOPSPPenalty.Message)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, _tOPSPPenalty.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, _tOPSPPenalty.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(_tOPSPPenalty.Dealer))
            DbCommandWrapper.AddInParameter("@TOPSPTransferPaymentID", DbType.Int32, Me.GetRefObject(_tOPSPPenalty.TOPSPTransferPayment))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TOPSPPenalty

            Dim _tOPSPPenalty As TOPSPPenalty = New TOPSPPenalty

            _tOPSPPenalty.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then _tOPSPPenalty.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LastDownloadby")) Then _tOPSPPenalty.LastDownloadby = dr("LastDownloadby").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadedDate")) Then _tOPSPPenalty.DownloadedDate = CType(dr("DownloadedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DebitMemoNumber")) Then _tOPSPPenalty.DebitMemoNumber = dr("DebitMemoNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DebitMemoDate")) Then _tOPSPPenalty.DebitMemoDate = CType(dr("DebitMemoDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AccountingNumber")) Then _tOPSPPenalty.AccountingNumber = dr("AccountingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClearingNumber")) Then _tOPSPPenalty.ClearingNumber = dr("ClearingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentDate")) Then _tOPSPPenalty.PaymentDate = CType(dr("PaymentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AmountPayment")) Then _tOPSPPenalty.AmountPayment = CType(dr("AmountPayment"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DebitMemoPath")) Then _tOPSPPenalty.DebitMemoPath = dr("DebitMemoPath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoRegPengembalian")) Then _tOPSPPenalty.NoRegPengembalian = dr("NoRegPengembalian").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoBuktiPotong")) Then _tOPSPPenalty.NoBuktiPotong = dr("NoBuktiPotong").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BuktiPotongDate")) Then _tOPSPPenalty.BuktiPotongDate = CType(dr("BuktiPotongDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AmountPPh")) Then _tOPSPPenalty.AmountPPh = CType(dr("AmountPPh"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("JVNumber")) Then _tOPSPPenalty.JVNumber = dr("JVNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadFilePath")) Then _tOPSPPenalty.UploadFilePath = dr("UploadFilePath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusPenalty")) Then _tOPSPPenalty.StatusPenalty = CType(dr("StatusPenalty"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusPengembalian")) Then _tOPSPPenalty.StatusPengembalian = CType(dr("StatusPengembalian"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Message")) Then _tOPSPPenalty.Message = dr("Message").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then _tOPSPPenalty.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then _tOPSPPenalty.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then _tOPSPPenalty.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then _tOPSPPenalty.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then _tOPSPPenalty.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                _tOPSPPenalty.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TOPSPTransferPaymentID")) Then
                _tOPSPPenalty.TOPSPTransferPayment = New TOPSPTransferPayment(CType(dr("TOPSPTransferPaymentID"), Integer))
            End If

            Return _tOPSPPenalty

        End Function

        Private Sub SetTableName()

            If Not (GetType(TOPSPPenalty) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TOPSPPenalty), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TOPSPPenalty).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

