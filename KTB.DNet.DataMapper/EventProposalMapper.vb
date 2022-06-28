#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventProposal Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/23/2009 - 3:00:17 PM
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

    Public Class EventProposalMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventProposal"
        Private m_UpdateStatement As String = "up_UpdateEventProposal"
        Private m_RetrieveStatement As String = "up_RetrieveEventProposal"
        Private m_RetrieveListStatement As String = "up_RetrieveEventProposalList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventProposal"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventProposal As EventProposal = Nothing
            While dr.Read

                eventProposal = Me.CreateObject(dr)

            End While

            Return eventProposal

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventProposalList As ArrayList = New ArrayList

            While dr.Read
                Dim eventProposal As EventProposal = Me.CreateObject(dr)
                eventProposalList.Add(eventProposal)
            End While

            Return eventProposalList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventProposal As EventProposal = CType(obj, EventProposal)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventProposal.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventProposal As EventProposal = CType(obj, EventProposal)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ActivitySchedule", DbType.DateTime, eventProposal.ActivitySchedule)
            DbCommandWrapper.AddInParameter("@ActivityPlace", DbType.AnsiString, eventProposal.ActivityPlace)
            DbCommandWrapper.AddInParameter("@InvitationNumber", DbType.Int32, eventProposal.InvitationNumber)
            DbCommandWrapper.AddInParameter("@AttendantNumber", DbType.Int32, eventProposal.AttendantNumber)
            DbCommandWrapper.AddInParameter("@EventProposalStatus", DbType.Byte, eventProposal.EventProposalStatus)
            DbCommandWrapper.AddInParameter("@EventAgreementStatus", DbType.Byte, eventProposal.EventAgreementStatus)
            DbCommandWrapper.AddInParameter("@Comment", DbType.AnsiString, eventProposal.Comment)
            DbCommandWrapper.AddInParameter("@Ravine", DbType.AnsiString, eventProposal.Ravine)
            DbCommandWrapper.AddInParameter("@SubDistrict", DbType.AnsiString, eventProposal.SubDistrict)
            DbCommandWrapper.AddInParameter("@Owner", DbType.Int32, eventProposal.Owner)
            DbCommandWrapper.AddInParameter("@Driver", DbType.Int32, eventProposal.Driver)
            DbCommandWrapper.AddInParameter("@ApproveCost", DbType.Currency, eventProposal.ApproveCost)
            DbCommandWrapper.AddInParameter("@SubsidiFile", DbType.AnsiString, eventProposal.SubsidiFile)
            DbCommandWrapper.AddInParameter("@OwnerAttendant", DbType.Int32, eventProposal.OwnerAttendant)
            DbCommandWrapper.AddInParameter("@DriverAttendant", DbType.Int32, eventProposal.DriverAttendant)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventProposal.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, eventProposal.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@EventParameterID", DbType.Int32, Me.GetRefObject(eventProposal.EventParameter))
            DbCommandWrapper.AddInParameter("@ActivityTypeID", DbType.Int32, Me.GetRefObject(eventProposal.ActivityType))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(eventProposal.Dealer))

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

            Dim eventProposal As EventProposal = CType(obj, EventProposal)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventProposal.ID)
            DbCommandWrapper.AddInParameter("@ActivitySchedule", DbType.DateTime, eventProposal.ActivitySchedule)
            DbCommandWrapper.AddInParameter("@ActivityPlace", DbType.AnsiString, eventProposal.ActivityPlace)
            DbCommandWrapper.AddInParameter("@InvitationNumber", DbType.Int32, eventProposal.InvitationNumber)
            DbCommandWrapper.AddInParameter("@AttendantNumber", DbType.Int32, eventProposal.AttendantNumber)
            DbCommandWrapper.AddInParameter("@EventProposalStatus", DbType.Byte, eventProposal.EventProposalStatus)
            DbCommandWrapper.AddInParameter("@EventAgreementStatus", DbType.Byte, eventProposal.EventAgreementStatus)
            DbCommandWrapper.AddInParameter("@Comment", DbType.AnsiString, eventProposal.Comment)
            DbCommandWrapper.AddInParameter("@Ravine", DbType.AnsiString, eventProposal.Ravine)
            DbCommandWrapper.AddInParameter("@SubDistrict", DbType.AnsiString, eventProposal.SubDistrict)
            DbCommandWrapper.AddInParameter("@Owner", DbType.Int32, eventProposal.Owner)
            DbCommandWrapper.AddInParameter("@Driver", DbType.Int32, eventProposal.Driver)
            DbCommandWrapper.AddInParameter("@ApproveCost", DbType.Currency, eventProposal.ApproveCost)
            DbCommandWrapper.AddInParameter("@SubsidiFile", DbType.AnsiString, eventProposal.SubsidiFile)
            DbCommandWrapper.AddInParameter("@OwnerAttendant", DbType.Int32, eventProposal.OwnerAttendant)
            DbCommandWrapper.AddInParameter("@DriverAttendant", DbType.Int32, eventProposal.DriverAttendant)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventProposal.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eventProposal.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@EventParameterID", DbType.Int32, Me.GetRefObject(eventProposal.EventParameter))
            DbCommandWrapper.AddInParameter("@ActivityTypeID", DbType.Int32, Me.GetRefObject(eventProposal.ActivityType))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(eventProposal.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventProposal

            Dim eventProposal As EventProposal = New EventProposal

            eventProposal.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ActivitySchedule")) Then eventProposal.ActivitySchedule = CType(dr("ActivitySchedule"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityPlace")) Then eventProposal.ActivityPlace = dr("ActivityPlace").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvitationNumber")) Then eventProposal.InvitationNumber = CType(dr("InvitationNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AttendantNumber")) Then eventProposal.AttendantNumber = CType(dr("AttendantNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventProposalStatus")) Then eventProposal.EventProposalStatus = CType(dr("EventProposalStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("EventAgreementStatus")) Then eventProposal.EventAgreementStatus = CType(dr("EventAgreementStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Comment")) Then eventProposal.Comment = dr("Comment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Ravine")) Then eventProposal.Ravine = dr("Ravine").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubDistrict")) Then eventProposal.SubDistrict = dr("SubDistrict").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then eventProposal.Owner = CType(dr("Owner"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Driver")) Then eventProposal.Driver = CType(dr("Driver"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ApproveCost")) Then eventProposal.ApproveCost = CType(dr("ApproveCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SubsidiFile")) Then eventProposal.SubsidiFile = dr("SubsidiFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OwnerAttendant")) Then eventProposal.OwnerAttendant = CType(dr("OwnerAttendant"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DriverAttendant")) Then eventProposal.DriverAttendant = CType(dr("DriverAttendant"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventProposal.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventProposal.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventProposal.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventProposal.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventProposal.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EventParameterID")) Then
                eventProposal.EventParameter = New EventParameter(CType(dr("EventParameterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityTypeID")) Then
                eventProposal.ActivityType = New ActivityType(CType(dr("ActivityTypeID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                eventProposal.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return eventProposal

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventProposal) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventProposal), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventProposal).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

