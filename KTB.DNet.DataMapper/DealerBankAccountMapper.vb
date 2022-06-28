
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerBankAccount Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/4/2008 - 9:18:48 AM
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

    Public Class DealerBankAccountMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerBankAccount"
        Private m_UpdateStatement As String = "up_UpdateDealerBankAccount"
        Private m_RetrieveStatement As String = "up_RetrieveDealerBankAccount"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerBankAccountList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerBankAccount"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerBankAccount As DealerBankAccount = Nothing
            While dr.Read

                dealerBankAccount = Me.CreateObject(dr)

            End While

            Return dealerBankAccount

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerBankAccountList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerBankAccount As DealerBankAccount = Me.CreateObject(dr)
                dealerBankAccountList.Add(dealerBankAccount)
            End While

            Return dealerBankAccountList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerBankAccount As DealerBankAccount = CType(obj, DealerBankAccount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerBankAccount.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerBankAccount As DealerBankAccount = CType(obj, DealerBankAccount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@BankKey", DbType.AnsiString, dealerBankAccount.BankKey)
            DbCommandWrapper.AddInParameter("@BankAccount", DbType.AnsiString, dealerBankAccount.BankAccount)
            DbCommandWrapper.AddInParameter("@BankName", DbType.AnsiString, dealerBankAccount.BankName)
            DbCommandWrapper.AddInParameter("@BeneficiaryName", DbType.AnsiString, dealerBankAccount.BeneficiaryName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, dealerBankAccount.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerBankAccount.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerBankAccount.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerBankAccount.Dealer))

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

            Dim dealerBankAccount As DealerBankAccount = CType(obj, DealerBankAccount)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerBankAccount.ID)
            DbCommandWrapper.AddInParameter("@BankKey", DbType.AnsiString, dealerBankAccount.BankKey)
            DbCommandWrapper.AddInParameter("@BankAccount", DbType.AnsiString, dealerBankAccount.BankAccount)
            DbCommandWrapper.AddInParameter("@BankName", DbType.AnsiString, dealerBankAccount.BankName)
            DbCommandWrapper.AddInParameter("@BeneficiaryName", DbType.AnsiString, dealerBankAccount.BeneficiaryName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, dealerBankAccount.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerBankAccount.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerBankAccount.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerBankAccount.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerBankAccount

            Dim dealerBankAccount As DealerBankAccount = New DealerBankAccount

            dealerBankAccount.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BankKey")) Then dealerBankAccount.BankKey = dr("BankKey").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BankAccount")) Then dealerBankAccount.BankAccount = dr("BankAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BankName")) Then dealerBankAccount.BankName = dr("BankName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BeneficiaryName")) Then dealerBankAccount.BeneficiaryName = dr("BeneficiaryName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then dealerBankAccount.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerBankAccount.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerBankAccount.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerBankAccount.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerBankAccount.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerBankAccount.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerBankAccount.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return dealerBankAccount

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerBankAccount) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerBankAccount), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerBankAccount).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

