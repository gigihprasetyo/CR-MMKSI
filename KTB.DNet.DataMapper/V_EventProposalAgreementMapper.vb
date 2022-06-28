#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_EventProposalAgreement Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/23/2009 - 3:17:19 PM
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

    Public Class V_EventProposalAgreementMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_EventProposalAgreement"
        Private m_UpdateStatement As String = "up_UpdateV_EventProposalAgreement"
        Private m_RetrieveStatement As String = "up_RetrieveV_EventProposalAgreement"
        Private m_RetrieveListStatement As String = "up_RetrieveV_EventProposalAgreementList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_EventProposalAgreement"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_EventProposalAgreement As V_EventProposalAgreement = Nothing
            While dr.Read

                v_EventProposalAgreement = Me.CreateObject(dr)

            End While

            Return v_EventProposalAgreement

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_EventProposalAgreementList As ArrayList = New ArrayList

            While dr.Read
                Dim v_EventProposalAgreement As V_EventProposalAgreement = Me.CreateObject(dr)
                v_EventProposalAgreementList.Add(v_EventProposalAgreement)
            End While

            Return v_EventProposalAgreementList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_EventProposalAgreement As V_EventProposalAgreement = CType(obj, V_EventProposalAgreement)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_EventProposalAgreement.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_EventProposalAgreement As V_EventProposalAgreement = CType(obj, V_EventProposalAgreement)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_EventProposalAgreement.DealerID)
            DbCommandWrapper.AddInParameter("@ActivityTypeID", DbType.Int32, v_EventProposalAgreement.ActivityTypeID)
            DbCommandWrapper.AddInParameter("@EventParameterID", DbType.Int32, v_EventProposalAgreement.EventParameterID)
            DbCommandWrapper.AddInParameter("@ActivitySchedule", DbType.DateTime, v_EventProposalAgreement.ActivitySchedule)
            DbCommandWrapper.AddInParameter("@ActivityPlace", DbType.AnsiString, v_EventProposalAgreement.ActivityPlace)
            DbCommandWrapper.AddInParameter("@InvitationNumber", DbType.Int32, v_EventProposalAgreement.InvitationNumber)
            DbCommandWrapper.AddInParameter("@AttendantNumber", DbType.Int32, v_EventProposalAgreement.AttendantNumber)
            DbCommandWrapper.AddInParameter("@EventProposalStatus", DbType.Byte, v_EventProposalAgreement.EventProposalStatus)
            DbCommandWrapper.AddInParameter("@EventAgreementStatus", DbType.Byte, v_EventProposalAgreement.EventAgreementStatus)
            DbCommandWrapper.AddInParameter("@Comment", DbType.AnsiString, v_EventProposalAgreement.Comment)
            DbCommandWrapper.AddInParameter("@Ravine", DbType.AnsiString, v_EventProposalAgreement.Ravine)
            DbCommandWrapper.AddInParameter("@SubDistrict", DbType.AnsiString, v_EventProposalAgreement.SubDistrict)
            DbCommandWrapper.AddInParameter("@Owner", DbType.Int32, v_EventProposalAgreement.Owner)
            DbCommandWrapper.AddInParameter("@Driver", DbType.Int32, v_EventProposalAgreement.Driver)
            DbCommandWrapper.AddInParameter("@TotalCost", DbType.Currency, v_EventProposalAgreement.TotalCost)
            DbCommandWrapper.AddInParameter("@ApproveCost", DbType.Currency, v_EventProposalAgreement.ApproveCost)
            DbCommandWrapper.AddInParameter("@SubsidiFile", DbType.AnsiString, v_EventProposalAgreement.SubsidiFile)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_EventProposalAgreement.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_EventProposalAgreement.LastUpdateBy)
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

            Dim v_EventProposalAgreement As V_EventProposalAgreement = CType(obj, V_EventProposalAgreement)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_EventProposalAgreement.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_EventProposalAgreement.DealerID)
            DbCommandWrapper.AddInParameter("@ActivityTypeID", DbType.Int32, v_EventProposalAgreement.ActivityTypeID)
            DbCommandWrapper.AddInParameter("@EventParameterID", DbType.Int32, v_EventProposalAgreement.EventParameterID)
            DbCommandWrapper.AddInParameter("@ActivitySchedule", DbType.DateTime, v_EventProposalAgreement.ActivitySchedule)
            DbCommandWrapper.AddInParameter("@ActivityPlace", DbType.AnsiString, v_EventProposalAgreement.ActivityPlace)
            DbCommandWrapper.AddInParameter("@InvitationNumber", DbType.Int32, v_EventProposalAgreement.InvitationNumber)
            DbCommandWrapper.AddInParameter("@AttendantNumber", DbType.Int32, v_EventProposalAgreement.AttendantNumber)
            DbCommandWrapper.AddInParameter("@EventProposalStatus", DbType.Byte, v_EventProposalAgreement.EventProposalStatus)
            DbCommandWrapper.AddInParameter("@EventAgreementStatus", DbType.Byte, v_EventProposalAgreement.EventAgreementStatus)
            DbCommandWrapper.AddInParameter("@Comment", DbType.AnsiString, v_EventProposalAgreement.Comment)
            DbCommandWrapper.AddInParameter("@Ravine", DbType.AnsiString, v_EventProposalAgreement.Ravine)
            DbCommandWrapper.AddInParameter("@SubDistrict", DbType.AnsiString, v_EventProposalAgreement.SubDistrict)
            DbCommandWrapper.AddInParameter("@Owner", DbType.Int32, v_EventProposalAgreement.Owner)
            DbCommandWrapper.AddInParameter("@Driver", DbType.Int32, v_EventProposalAgreement.Driver)
            DbCommandWrapper.AddInParameter("@TotalCost", DbType.Currency, v_EventProposalAgreement.TotalCost)
            DbCommandWrapper.AddInParameter("@ApproveCost", DbType.Currency, v_EventProposalAgreement.ApproveCost)
            DbCommandWrapper.AddInParameter("@SubsidiFile", DbType.AnsiString, v_EventProposalAgreement.SubsidiFile)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_EventProposalAgreement.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_EventProposalAgreement.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_EventProposalAgreement

            Dim v_EventProposalAgreement As V_EventProposalAgreement = New V_EventProposalAgreement

            v_EventProposalAgreement.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_EventProposalAgreement.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityTypeID")) Then v_EventProposalAgreement.ActivityTypeID = CType(dr("ActivityTypeID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventParameterID")) Then v_EventProposalAgreement.EventParameterID = CType(dr("EventParameterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ActivitySchedule")) Then v_EventProposalAgreement.ActivitySchedule = CType(dr("ActivitySchedule"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityPlace")) Then v_EventProposalAgreement.ActivityPlace = dr("ActivityPlace").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvitationNumber")) Then v_EventProposalAgreement.InvitationNumber = CType(dr("InvitationNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AttendantNumber")) Then v_EventProposalAgreement.AttendantNumber = CType(dr("AttendantNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventProposalStatus")) Then v_EventProposalAgreement.EventProposalStatus = CType(dr("EventProposalStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("EventAgreementStatus")) Then v_EventProposalAgreement.EventAgreementStatus = CType(dr("EventAgreementStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Comment")) Then v_EventProposalAgreement.Comment = dr("Comment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Ravine")) Then v_EventProposalAgreement.Ravine = dr("Ravine").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubDistrict")) Then v_EventProposalAgreement.SubDistrict = dr("SubDistrict").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Owner")) Then v_EventProposalAgreement.Owner = CType(dr("Owner"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Driver")) Then v_EventProposalAgreement.Driver = CType(dr("Driver"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalCost")) Then v_EventProposalAgreement.TotalCost = CType(dr("TotalCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ApproveCost")) Then v_EventProposalAgreement.ApproveCost = CType(dr("ApproveCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SubsidiFile")) Then v_EventProposalAgreement.SubsidiFile = dr("SubsidiFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_EventProposalAgreement.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_EventProposalAgreement.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_EventProposalAgreement.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_EventProposalAgreement.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_EventProposalAgreement.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_EventProposalAgreement

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_EventProposalAgreement) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_EventProposalAgreement), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_EventProposalAgreement).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

