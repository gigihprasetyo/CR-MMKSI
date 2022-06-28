
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 3/14/2016 - 11:19:30 AM
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

    Public Class DepositBDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositBDetail"
        Private m_UpdateStatement As String = "up_UpdateDepositBDetail"
        Private m_RetrieveStatement As String = "up_RetrieveDepositBDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositBDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositBDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositBDetail As DepositBDetail = Nothing
            While dr.Read

                depositBDetail = Me.CreateObject(dr)

            End While

            Return depositBDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositBDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim depositBDetail As DepositBDetail = Me.CreateObject(dr)
                depositBDetailList.Add(depositBDetail)
            End While

            Return depositBDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBDetail As DepositBDetail = CType(obj, DepositBDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBDetail As DepositBDetail = CType(obj, DepositBDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Tipe", DbType.AnsiString, depositBDetail.Tipe)
            DbCommandWrapper.AddInParameter("@StatusDebet", DbType.Int32, depositBDetail.StatusDebet)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, depositBDetail.Amount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, depositBDetail.Description)
            DbCommandWrapper.AddInParameter("@Reff", DbType.AnsiString, depositBDetail.Reff)
            DbCommandWrapper.AddInParameter("@DocumentNumber", DbType.AnsiString, depositBDetail.DocumentNumber)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, depositBDetail.TransactionDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositBDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DepositBID", DbType.Int32, Me.GetRefObject(depositBDetail.DepositBHeader))

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

            Dim depositBDetail As DepositBDetail = CType(obj, DepositBDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBDetail.ID)
            DbCommandWrapper.AddInParameter("@Tipe", DbType.AnsiString, depositBDetail.Tipe)
            DbCommandWrapper.AddInParameter("@StatusDebet", DbType.Int32, depositBDetail.StatusDebet)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, depositBDetail.Amount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, depositBDetail.Description)
            DbCommandWrapper.AddInParameter("@Reff", DbType.AnsiString, depositBDetail.Reff)
            DbCommandWrapper.AddInParameter("@DocumentNumber", DbType.AnsiString, depositBDetail.DocumentNumber)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, depositBDetail.TransactionDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositBDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DepositBID", DbType.Int32, Me.GetRefObject(depositBDetail.DepositBHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositBDetail

            Dim depositBDetail As DepositBDetail = New DepositBDetail

            depositBDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Tipe")) Then depositBDetail.Tipe = dr("Tipe").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusDebet")) Then depositBDetail.StatusDebet = CType(dr("StatusDebet"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then depositBDetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then depositBDetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Reff")) Then depositBDetail.Reff = dr("Reff").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentNumber")) Then depositBDetail.DocumentNumber = dr("DocumentNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then depositBDetail.TransactionDate = CType(dr("TransactionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositBDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositBDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositBDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositBDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositBDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DepositBID")) Then
                depositBDetail.DepositBHeader = New DepositBHeader(CType(dr("DepositBID"), Integer))
            End If

            Return depositBDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositBDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositBDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositBDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

