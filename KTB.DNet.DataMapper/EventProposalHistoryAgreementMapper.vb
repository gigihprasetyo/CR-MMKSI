#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventProposalHistoryAgreement Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 9/15/2009 - 10:47:37 AM
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

    Public Class EventProposalHistoryAgreementMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventProposalHistoryAgreement"
        Private m_UpdateStatement As String = "up_UpdateEventProposalHistoryAgreement"
        Private m_RetrieveStatement As String = "up_RetrieveEventProposalHistoryAgreement"
        Private m_RetrieveListStatement As String = "up_RetrieveEventProposalHistoryAgreementList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventProposalHistoryAgreement"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventProposalHistoryAgreement As EventProposalHistoryAgreement = Nothing
            While dr.Read

                eventProposalHistoryAgreement = Me.CreateObject(dr)

            End While

            Return eventProposalHistoryAgreement

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventProposalHistoryAgreementList As ArrayList = New ArrayList

            While dr.Read
                Dim eventProposalHistoryAgreement As EventProposalHistoryAgreement = Me.CreateObject(dr)
                eventProposalHistoryAgreementList.Add(eventProposalHistoryAgreement)
            End While

            Return eventProposalHistoryAgreementList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventProposalHistoryAgreement As EventProposalHistoryAgreement = CType(obj, EventProposalHistoryAgreement)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventProposalHistoryAgreement.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventProposalHistoryAgreement As EventProposalHistoryAgreement = CType(obj, EventProposalHistoryAgreement)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@EventName", DbType.AnsiString, eventProposalHistoryAgreement.EventName)
            DbCommandWrapper.AddInParameter("@ActivitySchedule", DbType.DateTime, eventProposalHistoryAgreement.ActivitySchedule)
            DbCommandWrapper.AddInParameter("@ProposedCost", DbType.Currency, eventProposalHistoryAgreement.ProposedCost)
            DbCommandWrapper.AddInParameter("@ApprovedCost", DbType.Currency, eventProposalHistoryAgreement.ApprovedCost)
            DbCommandWrapper.AddInParameter("@UpdateBy", DbType.AnsiString, eventProposalHistoryAgreement.UpdateBy)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventProposalHistoryAgreement.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, eventProposalHistoryAgreement.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@EventProposalID", DbType.Int32, Me.GetRefObject(eventProposalHistoryAgreement.EventProposal))

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

            Dim eventProposalHistoryAgreement As EventProposalHistoryAgreement = CType(obj, EventProposalHistoryAgreement)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventProposalHistoryAgreement.ID)
            DbCommandWrapper.AddInParameter("@EventName", DbType.AnsiString, eventProposalHistoryAgreement.EventName)
            DbCommandWrapper.AddInParameter("@ActivitySchedule", DbType.DateTime, eventProposalHistoryAgreement.ActivitySchedule)
            DbCommandWrapper.AddInParameter("@ProposedCost", DbType.Currency, eventProposalHistoryAgreement.ProposedCost)
            DbCommandWrapper.AddInParameter("@ApprovedCost", DbType.Currency, eventProposalHistoryAgreement.ApprovedCost)
            DbCommandWrapper.AddInParameter("@UpdateBy", DbType.AnsiString, eventProposalHistoryAgreement.UpdateBy)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventProposalHistoryAgreement.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eventProposalHistoryAgreement.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@EventProposalID", DbType.Int32, Me.GetRefObject(eventProposalHistoryAgreement.EventProposal))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventProposalHistoryAgreement

            Dim eventProposalHistoryAgreement As EventProposalHistoryAgreement = New EventProposalHistoryAgreement

            eventProposalHistoryAgreement.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventName")) Then eventProposalHistoryAgreement.EventName = dr("EventName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActivitySchedule")) Then eventProposalHistoryAgreement.ActivitySchedule = CType(dr("ActivitySchedule"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProposedCost")) Then eventProposalHistoryAgreement.ProposedCost = CType(dr("ProposedCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovedCost")) Then eventProposalHistoryAgreement.ApprovedCost = CType(dr("ApprovedCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("UpdateBy")) Then eventProposalHistoryAgreement.UpdateBy = dr("UpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventProposalHistoryAgreement.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventProposalHistoryAgreement.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventProposalHistoryAgreement.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventProposalHistoryAgreement.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventProposalHistoryAgreement.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EventProposalID")) Then
                eventProposalHistoryAgreement.EventProposal = New EventProposal(CType(dr("EventProposalID"), Integer))
            End If

            Return eventProposalHistoryAgreement

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventProposalHistoryAgreement) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventProposalHistoryAgreement), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventProposalHistoryAgreement).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

