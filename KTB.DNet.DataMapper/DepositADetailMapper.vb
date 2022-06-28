#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositADetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 12/11/2007 - 1:10:43 PM
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

    Public Class DepositADetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositADetail"
        Private m_UpdateStatement As String = "up_UpdateDepositADetail"
        Private m_RetrieveStatement As String = "up_RetrieveDepositADetail"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositADetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositADetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositADetail As DepositADetail = Nothing
            While dr.Read

                depositADetail = Me.CreateObject(dr)

            End While

            Return depositADetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositADetailList As ArrayList = New ArrayList

            While dr.Read
                Dim depositADetail As DepositADetail = Me.CreateObject(dr)
                depositADetailList.Add(depositADetail)
            End While

            Return depositADetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositADetail As DepositADetail = CType(obj, DepositADetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositADetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositADetail As DepositADetail = CType(obj, DepositADetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Tipe", DbType.AnsiString, depositADetail.Tipe)
            DbCommandWrapper.AddInParameter("@StatusDebet", DbType.Int32, depositADetail.StatusDebet)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, depositADetail.Amount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, depositADetail.Description)
            DbCommandWrapper.AddInParameter("@Reff", DbType.AnsiString, depositADetail.Reff)
            DbCommandWrapper.AddInParameter("@DocumentNumber", DbType.AnsiString, depositADetail.DocumentNumber)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, depositADetail.TransactionDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositADetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositADetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DepositAID", DbType.Int32, Me.GetRefObject(depositADetail.DepositA))

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

            Dim depositADetail As DepositADetail = CType(obj, DepositADetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositADetail.ID)
            DbCommandWrapper.AddInParameter("@Tipe", DbType.AnsiString, depositADetail.Tipe)
            DbCommandWrapper.AddInParameter("@StatusDebet", DbType.Int32, depositADetail.StatusDebet)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, depositADetail.Amount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, depositADetail.Description)
            DbCommandWrapper.AddInParameter("@Reff", DbType.AnsiString, depositADetail.Reff)
            DbCommandWrapper.AddInParameter("@DocumentNumber", DbType.AnsiString, depositADetail.DocumentNumber)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, depositADetail.TransactionDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositADetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositADetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DepositAID", DbType.Int32, Me.GetRefObject(depositADetail.DepositA))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositADetail

            Dim depositADetail As DepositADetail = New DepositADetail

            depositADetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Tipe")) Then depositADetail.Tipe = dr("Tipe").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusDebet")) Then depositADetail.StatusDebet = CType(dr("StatusDebet"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then depositADetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then depositADetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Reff")) Then depositADetail.Reff = dr("Reff").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentNumber")) Then depositADetail.DocumentNumber = dr("DocumentNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then depositADetail.TransactionDate = CType(dr("TransactionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositADetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositADetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositADetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositADetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositADetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DepositAID")) Then
                depositADetail.DepositA = New DepositA(CType(dr("DepositAID"), Integer))
            End If

            Return depositADetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositADetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositADetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositADetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

