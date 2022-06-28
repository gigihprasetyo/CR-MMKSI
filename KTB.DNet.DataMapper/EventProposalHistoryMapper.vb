#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventProposalHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 9/9/2009 - 8:50:23 AM
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

    Public Class EventProposalHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventProposalHistory"
        Private m_UpdateStatement As String = "up_UpdateEventProposalHistory"
        Private m_RetrieveStatement As String = "up_RetrieveEventProposalHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveEventProposalHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventProposalHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventProposalHistory As EventProposalHistory = Nothing
            While dr.Read

                eventProposalHistory = Me.CreateObject(dr)

            End While

            Return eventProposalHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventProposalHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim eventProposalHistory As EventProposalHistory = Me.CreateObject(dr)
                eventProposalHistoryList.Add(eventProposalHistory)
            End While

            Return eventProposalHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventProposalHistory As EventProposalHistory = CType(obj, EventProposalHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventProposalHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventProposalHistory As EventProposalHistory = CType(obj, EventProposalHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ActivityPlaceOld", DbType.AnsiString, eventProposalHistory.ActivityPlaceOld)
            DbCommandWrapper.AddInParameter("@ActivityScheduleOld", DbType.DateTime, eventProposalHistory.ActivityScheduleOld)
            DbCommandWrapper.AddInParameter("@ActivityPlaceNew", DbType.AnsiString, eventProposalHistory.ActivityPlaceNew)
            DbCommandWrapper.AddInParameter("@ActivityScheduleNew", DbType.DateTime, eventProposalHistory.ActivityScheduleNew)
            DbCommandWrapper.AddInParameter("@UpdateBy", DbType.AnsiString, eventProposalHistory.UpdateBy)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventProposalHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, eventProposalHistory.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@EventProposalID", DbType.Int32, Me.GetRefObject(eventProposalHistory.EventProposal))

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

            Dim eventProposalHistory As EventProposalHistory = CType(obj, EventProposalHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventProposalHistory.ID)
            DbCommandWrapper.AddInParameter("@ActivityPlaceOld", DbType.AnsiString, eventProposalHistory.ActivityPlaceOld)
            DbCommandWrapper.AddInParameter("@ActivityScheduleOld", DbType.DateTime, eventProposalHistory.ActivityScheduleOld)
            DbCommandWrapper.AddInParameter("@ActivityPlaceNew", DbType.AnsiString, eventProposalHistory.ActivityPlaceNew)
            DbCommandWrapper.AddInParameter("@ActivityScheduleNew", DbType.DateTime, eventProposalHistory.ActivityScheduleNew)
            DbCommandWrapper.AddInParameter("@UpdateBy", DbType.AnsiString, eventProposalHistory.UpdateBy)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventProposalHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eventProposalHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@EventProposalID", DbType.Int32, Me.GetRefObject(eventProposalHistory.EventProposal))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventProposalHistory

            Dim eventProposalHistory As EventProposalHistory = New EventProposalHistory

            eventProposalHistory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityPlaceOld")) Then eventProposalHistory.ActivityPlaceOld = dr("ActivityPlaceOld").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityScheduleOld")) Then eventProposalHistory.ActivityScheduleOld = CType(dr("ActivityScheduleOld"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityPlaceNew")) Then eventProposalHistory.ActivityPlaceNew = dr("ActivityPlaceNew").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityScheduleNew")) Then eventProposalHistory.ActivityScheduleNew = CType(dr("ActivityScheduleNew"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UpdateBy")) Then eventProposalHistory.UpdateBy = dr("UpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventProposalHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventProposalHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventProposalHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventProposalHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventProposalHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EventProposalID")) Then
                eventProposalHistory.EventProposal = New EventProposal(CType(dr("EventProposalID"), Integer))
            End If

            Return eventProposalHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventProposalHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventProposalHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventProposalHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

