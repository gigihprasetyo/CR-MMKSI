#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerActivityPlanning Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 9/8/2006 - 1:46:43 PM
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

    Public Class DealerActivityPlanningMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerActivityPlanning"
        Private m_UpdateStatement As String = "up_UpdateDealerActivityPlanning"
        Private m_RetrieveStatement As String = "up_RetrieveDealerActivityPlanning"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerActivityPlanningList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerActivityPlanning"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerActivityPlanning As DealerActivityPlanning = Nothing
            While dr.Read

                dealerActivityPlanning = Me.CreateObject(dr)

            End While

            Return dealerActivityPlanning

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerActivityPlanningList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerActivityPlanning As DealerActivityPlanning = Me.CreateObject(dr)
                dealerActivityPlanningList.Add(dealerActivityPlanning)
            End While

            Return dealerActivityPlanningList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerActivityPlanning As DealerActivityPlanning = CType(obj, DealerActivityPlanning)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerActivityPlanning.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerActivityPlanning As DealerActivityPlanning = CType(obj, DealerActivityPlanning)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@BabitActivitiy", DbType.AnsiString, dealerActivityPlanning.BabitActivitiy)
            DbCommandWrapper.AddInParameter("@ActivityPlanning", DbType.AnsiString, dealerActivityPlanning.ActivityPlanning)
            DbCommandWrapper.AddInParameter("@StartPeriodMonth", DbType.Int32, dealerActivityPlanning.StartPeriodMonth)
            DbCommandWrapper.AddInParameter("@EndPeriodMonth", DbType.Int32, dealerActivityPlanning.EndPeriodMonth)
            DBCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, dealerActivityPlanning.PeriodYear)
            DBCommandWrapper.AddInParameter("@PeriodYearEnd", DbType.Int32, dealerActivityPlanning.PeriodYearEnd)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, dealerActivityPlanning.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerActivityPlanning.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerActivityPlanning.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerActivityPlanning.Dealer))

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

            Dim dealerActivityPlanning As DealerActivityPlanning = CType(obj, DealerActivityPlanning)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerActivityPlanning.ID)
            DbCommandWrapper.AddInParameter("@BabitActivitiy", DbType.AnsiString, dealerActivityPlanning.BabitActivitiy)
            DbCommandWrapper.AddInParameter("@ActivityPlanning", DbType.AnsiString, dealerActivityPlanning.ActivityPlanning)
            DbCommandWrapper.AddInParameter("@StartPeriodMonth", DbType.Int32, dealerActivityPlanning.StartPeriodMonth)
            DbCommandWrapper.AddInParameter("@EndPeriodMonth", DbType.Int32, dealerActivityPlanning.EndPeriodMonth)
            DBCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, dealerActivityPlanning.PeriodYear)
            DBCommandWrapper.AddInParameter("@PeriodYearEnd", DbType.Int32, dealerActivityPlanning.PeriodYearEnd)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, dealerActivityPlanning.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerActivityPlanning.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerActivityPlanning.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerActivityPlanning.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerActivityPlanning

            Dim dealerActivityPlanning As DealerActivityPlanning = New DealerActivityPlanning

            dealerActivityPlanning.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitActivitiy")) Then dealerActivityPlanning.BabitActivitiy = dr("BabitActivitiy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityPlanning")) Then dealerActivityPlanning.ActivityPlanning = dr("ActivityPlanning").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartPeriodMonth")) Then dealerActivityPlanning.StartPeriodMonth = CType(dr("StartPeriodMonth"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EndPeriodMonth")) Then dealerActivityPlanning.EndPeriodMonth = CType(dr("EndPeriodMonth"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYear")) Then dealerActivityPlanning.PeriodYear = CType(dr("PeriodYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYearEnd")) Then dealerActivityPlanning.PeriodYearEnd = CType(dr("PeriodYearEnd"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then dealerActivityPlanning.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerActivityPlanning.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerActivityPlanning.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerActivityPlanning.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerActivityPlanning.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerActivityPlanning.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerActivityPlanning.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return dealerActivityPlanning

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerActivityPlanning) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerActivityPlanning), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerActivityPlanning).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

