
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPCreditAccount Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 16/08/2018 - 10:22:33
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

    Public Class TOPCreditAccountMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTOPCreditAccount"
        Private m_UpdateStatement As String = "up_UpdateTOPCreditAccount"
        Private m_RetrieveStatement As String = "up_RetrieveTOPCreditAccount"
        Private m_RetrieveListStatement As String = "up_RetrieveTOPCreditAccountList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTOPCreditAccount"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim tOPCreditAccount As TOPCreditAccount = Nothing
            While dr.Read

                tOPCreditAccount = Me.CreateObject(dr)

            End While

            Return tOPCreditAccount

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim tOPCreditAccountList As ArrayList = New ArrayList

            While dr.Read
                Dim tOPCreditAccount As TOPCreditAccount = Me.CreateObject(dr)
                tOPCreditAccountList.Add(tOPCreditAccount)
            End While

            Return tOPCreditAccountList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPCreditAccount As TOPCreditAccount = CType(obj, TOPCreditAccount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPCreditAccount.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPCreditAccount As TOPCreditAccount = CType(obj, TOPCreditAccount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@KelipatanPembayaran", DbType.Int32, tOPCreditAccount.KelipatanPembayaran)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, tOPCreditAccount.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPCreditAccount.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, tOPCreditAccount.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(tOPCreditAccount.Dealer))
            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, Me.GetRefObject(tOPCreditAccount.TermOfPayment))
            DbCommandWrapper.AddInParameter("@PrevTermOfPaymentID", DbType.Int32, Me.GetRefObject(tOPCreditAccount.prevTermOfPayment))

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

            Dim tOPCreditAccount As TOPCreditAccount = CType(obj, TOPCreditAccount)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPCreditAccount.ID)
            DbCommandWrapper.AddInParameter("@KelipatanPembayaran", DbType.Int32, tOPCreditAccount.KelipatanPembayaran)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, tOPCreditAccount.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPCreditAccount.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, tOPCreditAccount.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(tOPCreditAccount.Dealer))
            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, Me.GetRefObject(tOPCreditAccount.TermOfPayment))
            DbCommandWrapper.AddInParameter("@PrevTermOfPaymentID", DbType.Int32, Me.GetRefObject(tOPCreditAccount.PrevTermOfPayment))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TOPCreditAccount

            Dim tOPCreditAccount As TOPCreditAccount = New TOPCreditAccount

            tOPCreditAccount.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KelipatanPembayaran")) Then tOPCreditAccount.KelipatanPembayaran = CType(dr("KelipatanPembayaran"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then tOPCreditAccount.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then tOPCreditAccount.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then tOPCreditAccount.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then tOPCreditAccount.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then tOPCreditAccount.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then tOPCreditAccount.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                tOPCreditAccount.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentID")) Then
                tOPCreditAccount.TermOfPayment = New TermOfPayment(CType(dr("TermOfPaymentID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PrevTermOfPaymentID")) Then
                tOPCreditAccount.PrevTermOfPayment = New TermOfPayment(CType(dr("PrevTermOfPaymentID"), Integer))
            End If

            Return tOPCreditAccount

        End Function

        Private Sub SetTableName()

            If Not (GetType(TOPCreditAccount) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TOPCreditAccount), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TOPCreditAccount).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

