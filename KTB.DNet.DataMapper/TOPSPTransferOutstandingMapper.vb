#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : TOPSPTransferOutstanding Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 10/5/2021 - 10:42:05 AM
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

    Public Class TOPSPTransferOutstandingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTOPSPTransferOutstanding"
        Private m_UpdateStatement As String = "up_UpdateTOPSPTransferOutstanding"
        Private m_RetrieveStatement As String = "up_RetrieveTOPSPTransferOutstanding"
        Private m_RetrieveListStatement As String = "up_RetrieveTOPSPTransferOutstandingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTOPSPTransferOutstanding"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim tOPSPTransferOutstanding As TOPSPTransferOutstanding = Nothing
            While dr.Read

                tOPSPTransferOutstanding = Me.CreateObject(dr)

            End While

            Return tOPSPTransferOutstanding

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim tOPSPTransferOutstandingList As ArrayList = New ArrayList

            While dr.Read
                Dim tOPSPTransferOutstanding As TOPSPTransferOutstanding = Me.CreateObject(dr)
                tOPSPTransferOutstandingList.Add(tOPSPTransferOutstanding)
            End While

            Return tOPSPTransferOutstandingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPTransferOutstanding As TOPSPTransferOutstanding = CType(obj, TOPSPTransferOutstanding)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPTransferOutstanding.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPTransferOutstanding As TOPSPTransferOutstanding = CType(obj, TOPSPTransferOutstanding)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, tOPSPTransferOutstanding.RegNumber)
            DbCommandWrapper.AddInParameter("@RefBank", DbType.AnsiString, tOPSPTransferOutstanding.RefBank)
            DbCommandWrapper.AddInParameter("@TransferAmount", DbType.Currency, tOPSPTransferOutstanding.TransferAmount)
            DbCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, tOPSPTransferOutstanding.TransferDate)
            DbCommandWrapper.AddInParameter("@TRNo", DbType.AnsiString, tOPSPTransferOutstanding.TRNo)
            DbCommandWrapper.AddInParameter("@IDTransaction", DbType.Int16, tOPSPTransferOutstanding.IDTransaction)
            DbCommandWrapper.AddInParameter("@Narrative", DbType.AnsiString, tOPSPTransferOutstanding.Narrative)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPTransferOutstanding.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, tOPSPTransferOutstanding.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, tOPSPTransferOutstanding.DealerID)
            'DbCommandWrapper.AddInParameter("@BankID", DbType.Int32, tOPSPTransferOutstanding.BankID)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(tOPSPTransferOutstanding.Dealer))
            DbCommandWrapper.AddInParameter("@BankID", DbType.Int32, Me.GetRefObject(tOPSPTransferOutstanding.Bank))


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

            Dim tOPSPTransferOutstanding As TOPSPTransferOutstanding = CType(obj, TOPSPTransferOutstanding)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPTransferOutstanding.ID)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, tOPSPTransferOutstanding.RegNumber)
            DbCommandWrapper.AddInParameter("@RefBank", DbType.AnsiString, tOPSPTransferOutstanding.RefBank)
            DbCommandWrapper.AddInParameter("@TransferAmount", DbType.Currency, tOPSPTransferOutstanding.TransferAmount)
            DbCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, tOPSPTransferOutstanding.TransferDate)
            DbCommandWrapper.AddInParameter("@TRNo", DbType.AnsiString, tOPSPTransferOutstanding.TRNo)
            DbCommandWrapper.AddInParameter("@IDTransaction", DbType.Int16, tOPSPTransferOutstanding.IDTransaction)
            DbCommandWrapper.AddInParameter("@Narrative", DbType.AnsiString, tOPSPTransferOutstanding.Narrative)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPTransferOutstanding.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, tOPSPTransferOutstanding.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, tOPSPTransferOutstanding.DealerID)
            'DbCommandWrapper.AddInParameter("@BankID", DbType.Int32, tOPSPTransferOutstanding.BankID)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(tOPSPTransferOutstanding.Dealer))
            DbCommandWrapper.AddInParameter("@BankID", DbType.Int32, Me.GetRefObject(tOPSPTransferOutstanding.Bank))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TOPSPTransferOutstanding

            Dim tOPSPTransferOutstanding As TOPSPTransferOutstanding = New TOPSPTransferOutstanding

            tOPSPTransferOutstanding.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then tOPSPTransferOutstanding.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RefBank")) Then tOPSPTransferOutstanding.RefBank = dr("RefBank").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransferAmount")) Then tOPSPTransferOutstanding.TransferAmount = CType(dr("TransferAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferDate")) Then tOPSPTransferOutstanding.TransferDate = CType(dr("TransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TRNo")) Then tOPSPTransferOutstanding.TRNo = dr("TRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IDTransaction")) Then tOPSPTransferOutstanding.IDTransaction = CType(dr("IDTransaction"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Narrative")) Then tOPSPTransferOutstanding.Narrative = dr("Narrative").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then tOPSPTransferOutstanding.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then tOPSPTransferOutstanding.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then tOPSPTransferOutstanding.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then tOPSPTransferOutstanding.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then tOPSPTransferOutstanding.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then tOPSPTransferOutstanding.DealerID = CType(dr("DealerID"), Short)
            'If Not dr.IsDBNull(dr.GetOrdinal("BankID")) Then tOPSPTransferOutstanding.BankID = CType(dr("BankID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                tOPSPTransferOutstanding.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("BankID")) Then
                tOPSPTransferOutstanding.Bank = New Bank(CType(dr("BankID"), Integer))
            End If

            Return tOPSPTransferOutstanding

        End Function

        Private Sub SetTableName()

            If Not (GetType(TOPSPTransferOutstanding) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TOPSPTransferOutstanding), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TOPSPTransferOutstanding).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
