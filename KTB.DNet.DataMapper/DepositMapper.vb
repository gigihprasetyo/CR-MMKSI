#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Deposit Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 15/11/2005 - 14:43:58
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

    Public Class DepositMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDeposit"
        Private m_UpdateStatement As String = "up_UpdateDeposit"
        Private m_RetrieveStatement As String = "up_RetrieveDeposit"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDeposit"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim deposit As Deposit = Nothing
            While dr.Read

                deposit = Me.CreateObject(dr)

            End While

            Return deposit

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositList As ArrayList = New ArrayList

            While dr.Read
                Dim deposit As Deposit = Me.CreateObject(dr)
                depositList.Add(deposit)
            End While

            Return depositList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim deposit As Deposit = CType(obj, Deposit)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, deposit.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim deposit As Deposit = CType(obj, Deposit)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Period", DbType.AnsiString, deposit.Period)
            DbCommandWrapper.AddInParameter("@BegBalance", DbType.Currency, deposit.BegBalance)
            DbCommandWrapper.AddInParameter("@EndBalance", DbType.Currency, deposit.EndBalance)
            DbCommandWrapper.AddInParameter("@TotalDebit", DbType.Currency, deposit.TotalDebit)
            DBCommandWrapper.AddInParameter("@TotalCredit", DbType.Currency, deposit.TotalCredit)
            DBCommandWrapper.AddInParameter("@AvailableDeposit", DbType.Currency, deposit.AvailableDeposit)
            DBCommandWrapper.AddInParameter("@GiroReceive", DbType.Currency, deposit.GiroReceive)
            DBCommandWrapper.AddInParameter("@RO", DbType.Currency, deposit.RO)
            DBCommandWrapper.AddInParameter("@Service", DbType.Currency, deposit.Service)
            DBCommandWrapper.AddInParameter("@InClearing", DbType.Currency, deposit.InClearing)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, deposit.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, deposit.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(deposit.Dealer))

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

            Dim deposit As Deposit = CType(obj, Deposit)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, deposit.ID)
            DbCommandWrapper.AddInParameter("@Period", DbType.AnsiString, deposit.Period)
            DbCommandWrapper.AddInParameter("@BegBalance", DbType.Currency, deposit.BegBalance)
            DbCommandWrapper.AddInParameter("@EndBalance", DbType.Currency, deposit.EndBalance)
            DbCommandWrapper.AddInParameter("@TotalDebit", DbType.Currency, deposit.TotalDebit)
            DBCommandWrapper.AddInParameter("@TotalCredit", DbType.Currency, deposit.TotalCredit)
            DBCommandWrapper.AddInParameter("@AvailableDeposit", DbType.Currency, deposit.AvailableDeposit)
            DBCommandWrapper.AddInParameter("@GiroReceive", DbType.Currency, deposit.GiroReceive)
            DBCommandWrapper.AddInParameter("@RO", DbType.Currency, deposit.RO)
            DBCommandWrapper.AddInParameter("@Service", DbType.Currency, deposit.Service)
            DBCommandWrapper.AddInParameter("@InClearing", DbType.Currency, deposit.InClearing)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, deposit.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, deposit.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(deposit.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Deposit

            Dim deposit As Deposit = New Deposit

            deposit.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Period")) Then deposit.Period = dr("Period").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BegBalance")) Then deposit.BegBalance = CType(dr("BegBalance"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("EndBalance")) Then deposit.EndBalance = CType(dr("EndBalance"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalDebit")) Then deposit.TotalDebit = CType(dr("TotalDebit"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalCredit")) Then deposit.TotalCredit = CType(dr("TotalCredit"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AvailableDeposit")) Then deposit.AvailableDeposit = CType(dr("AvailableDeposit"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("GiroReceive")) Then deposit.GiroReceive = CType(dr("GiroReceive"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RO")) Then deposit.RO = CType(dr("RO"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Service")) Then deposit.Service = CType(dr("Service"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("InClearing")) Then deposit.InClearing = CType(dr("InClearing"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then deposit.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then deposit.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then deposit.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then deposit.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then deposit.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                deposit.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return deposit

        End Function

        Private Sub SetTableName()

            If Not (GetType(Deposit) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Deposit), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Deposit).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
