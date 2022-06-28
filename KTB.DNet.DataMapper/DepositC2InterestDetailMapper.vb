#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositC2InterestDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/17/2020 - 10:56:27 AM
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

    Public Class DepositC2InterestDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositC2InterestDetail"
        Private m_UpdateStatement As String = "up_UpdateDepositC2InterestDetail"
        Private m_RetrieveStatement As String = "up_RetrieveDepositC2InterestDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositC2InterestDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositC2InterestDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositC2InterestDetail As DepositC2InterestDetail = Nothing
            While dr.Read

                depositC2InterestDetail = Me.CreateObject(dr)

            End While

            Return depositC2InterestDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositC2InterestDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim depositC2InterestDetail As DepositC2InterestDetail = Me.CreateObject(dr)
                depositC2InterestDetailList.Add(depositC2InterestDetail)
            End While

            Return depositC2InterestDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositC2InterestDetail As DepositC2InterestDetail = CType(obj, DepositC2InterestDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositC2InterestDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositC2InterestDetail As DepositC2InterestDetail = CType(obj, DepositC2InterestDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Month", DbType.AnsiString, depositC2InterestDetail.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, depositC2InterestDetail.Year)
            DbCommandWrapper.AddInParameter("@InterestAmount", DbType.Currency, depositC2InterestDetail.InterestAmount)
            DbCommandWrapper.AddInParameter("@NettoAmount", DbType.Currency, depositC2InterestDetail.NettoAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositC2InterestDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositC2InterestDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DepositC2InterestHeaderID", DbType.Int32, Me.GetRefObject(depositC2InterestDetail.DepositC2InterestHeader))

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

            Dim depositC2InterestDetail As DepositC2InterestDetail = CType(obj, DepositC2InterestDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositC2InterestDetail.ID)
            DbCommandWrapper.AddInParameter("@Month", DbType.AnsiString, depositC2InterestDetail.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, depositC2InterestDetail.Year)
            DbCommandWrapper.AddInParameter("@InterestAmount", DbType.Currency, depositC2InterestDetail.InterestAmount)
            DbCommandWrapper.AddInParameter("@NettoAmount", DbType.Currency, depositC2InterestDetail.NettoAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositC2InterestDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositC2InterestDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DepositC2InterestHeaderID", DbType.Int32, Me.GetRefObject(depositC2InterestDetail.DepositC2InterestHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositC2InterestDetail

            Dim depositC2InterestDetail As DepositC2InterestDetail = New DepositC2InterestDetail

            depositC2InterestDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Month")) Then depositC2InterestDetail.Month = dr("Month").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then depositC2InterestDetail.Year = CType(dr("Year"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("InterestAmount")) Then depositC2InterestDetail.InterestAmount = CType(dr("InterestAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("NettoAmount")) Then depositC2InterestDetail.NettoAmount = CType(dr("NettoAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositC2InterestDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositC2InterestDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositC2InterestDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositC2InterestDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositC2InterestDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DepositC2InterestHeaderID")) Then
                depositC2InterestDetail.DepositC2InterestHeader = New DepositC2InterestHeader(CType(dr("DepositC2InterestHeaderID"), Integer))
            End If

            Return depositC2InterestDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositC2InterestDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositC2InterestDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositC2InterestDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
