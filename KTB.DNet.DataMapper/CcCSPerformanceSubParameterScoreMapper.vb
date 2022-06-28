
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceSubParameterScore Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 5/18/2018 - 2:05:20 PM
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

    Public Class CcCSPerformanceSubParameterScoreMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcCSPerformanceSubParameterScore"
        Private m_UpdateStatement As String = "up_UpdateCcCSPerformanceSubParameterScore"
        Private m_RetrieveStatement As String = "up_RetrieveCcCSPerformanceSubParameterScore"
        Private m_RetrieveListStatement As String = "up_RetrieveCcCSPerformanceSubParameterScoreList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcCSPerformanceSubParameterScore"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccCSPerformanceSubParameterScore As CcCSPerformanceSubParameterScore = Nothing
            While dr.Read

                ccCSPerformanceSubParameterScore = Me.CreateObject(dr)

            End While

            Return ccCSPerformanceSubParameterScore

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccCSPerformanceSubParameterScoreList As ArrayList = New ArrayList

            While dr.Read
                Dim ccCSPerformanceSubParameterScore As CcCSPerformanceSubParameterScore = Me.CreateObject(dr)
                ccCSPerformanceSubParameterScoreList.Add(ccCSPerformanceSubParameterScore)
            End While

            Return ccCSPerformanceSubParameterScoreList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceSubParameterScore As CcCSPerformanceSubParameterScore = CType(obj, CcCSPerformanceSubParameterScore)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceSubParameterScore.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceSubParameterScore As CcCSPerformanceSubParameterScore = CType(obj, CcCSPerformanceSubParameterScore)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CcPeriodID", DbType.Int32, ccCSPerformanceSubParameterScore.CcPeriodID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, ccCSPerformanceSubParameterScore.DealerID)
            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int32, ccCSPerformanceSubParameterScore.CcCustomerCategoryID)
            DbCommandWrapper.AddInParameter("@CcVehicleCategoryID", DbType.Int32, ccCSPerformanceSubParameterScore.CcVehicleCategoryID)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceSubParameterCode", DbType.AnsiString, ccCSPerformanceSubParameterScore.CcCSPerformanceSubParameterCode)
            DbCommandWrapper.AddInParameter("@SubFunction", DbType.AnsiString, ccCSPerformanceSubParameterScore.SubFunction)
            DbCommandWrapper.AddInParameter("@ParameterScore", DbType.Decimal, ccCSPerformanceSubParameterScore.ParameterScore)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceSubParameterScore.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccCSPerformanceSubParameterScore.LastUpdateBy)
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

            Dim ccCSPerformanceSubParameterScore As CcCSPerformanceSubParameterScore = CType(obj, CcCSPerformanceSubParameterScore)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceSubParameterScore.ID)
            DbCommandWrapper.AddInParameter("@CcPeriodID", DbType.Int32, ccCSPerformanceSubParameterScore.CcPeriodID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, ccCSPerformanceSubParameterScore.DealerID)
            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int32, ccCSPerformanceSubParameterScore.CcCustomerCategoryID)
            DbCommandWrapper.AddInParameter("@CcVehicleCategoryID", DbType.Int32, ccCSPerformanceSubParameterScore.CcVehicleCategoryID)
            DbCommandWrapper.AddInParameter("@CcCSPerformanceSubParameterCode", DbType.AnsiString, ccCSPerformanceSubParameterScore.CcCSPerformanceSubParameterCode)
            DbCommandWrapper.AddInParameter("@SubFunction", DbType.AnsiString, ccCSPerformanceSubParameterScore.SubFunction)
            DbCommandWrapper.AddInParameter("@ParameterScore", DbType.Decimal, ccCSPerformanceSubParameterScore.ParameterScore)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceSubParameterScore.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccCSPerformanceSubParameterScore.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcCSPerformanceSubParameterScore

            Dim ccCSPerformanceSubParameterScore As CcCSPerformanceSubParameterScore = New CcCSPerformanceSubParameterScore

            ccCSPerformanceSubParameterScore.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcPeriodID")) Then ccCSPerformanceSubParameterScore.CcPeriodID = CType(dr("CcPeriodID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then ccCSPerformanceSubParameterScore.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcCustomerCategoryID")) Then ccCSPerformanceSubParameterScore.CcCustomerCategoryID = CType(dr("CcCustomerCategoryID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcVehicleCategoryID")) Then ccCSPerformanceSubParameterScore.CcVehicleCategoryID = CType(dr("CcVehicleCategoryID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceSubParameterCode")) Then ccCSPerformanceSubParameterScore.CcCSPerformanceSubParameterCode = dr("CcCSPerformanceSubParameterCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CcCSPerformanceSubParameterCode")) Then ccCSPerformanceSubParameterScore.SubFunction = dr("SubFunction").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ParameterScore")) Then ccCSPerformanceSubParameterScore.ParameterScore = CType(dr("ParameterScore"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccCSPerformanceSubParameterScore.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccCSPerformanceSubParameterScore.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccCSPerformanceSubParameterScore.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccCSPerformanceSubParameterScore.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccCSPerformanceSubParameterScore.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                ccCSPerformanceSubParameterScore.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CcPeriodID")) Then
                ccCSPerformanceSubParameterScore.CcPeriod = New CcPeriod(CType(dr("CcPeriodID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CcCustomerCategoryID")) Then
                ccCSPerformanceSubParameterScore.CcCustomerCategory = New CcCustomerCategory(CType(dr("CcCustomerCategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CcVehicleCategoryID")) Then
                ccCSPerformanceSubParameterScore.CcVehicleCategory = New CcVehicleCategory(CType(dr("CcVehicleCategoryID"), Integer))
            End If

            Return ccCSPerformanceSubParameterScore

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcCSPerformanceSubParameterScore) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcCSPerformanceSubParameterScore), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcCSPerformanceSubParameterScore).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

