#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventInfo Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/9/2007 - 2:15:31 PM
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

    Public Class EventInfoMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventInfo"
        Private m_UpdateStatement As String = "up_UpdateEventInfo"
        Private m_RetrieveStatement As String = "up_RetrieveEventInfo"
        Private m_RetrieveListStatement As String = "up_RetrieveEventInfoList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventInfo"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventInfo As EventInfo = Nothing
            While dr.Read

                eventInfo = Me.CreateObject(dr)

            End While

            Return eventInfo

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventInfoList As ArrayList = New ArrayList

            While dr.Read
                Dim eventInfo As EventInfo = Me.CreateObject(dr)
                eventInfoList.Add(eventInfo)
            End While

            Return eventInfoList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventInfo As EventInfo = CType(obj, EventInfo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventInfo.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventInfo As EventInfo = CType(obj, EventInfo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@BabitAllocationID", DbType.Int32, eventInfo.BabitAllocationID)
            DbCommandWrapper.AddInParameter("@EventRequestNo", DbType.AnsiString, eventInfo.EventRequestNo)
            DbCommandWrapper.AddInParameter("@EventApprovalNo", DbType.AnsiString, eventInfo.EventApprovalNo)
            DbCommandWrapper.AddInParameter("@DateStart", DbType.DateTime, eventInfo.DateStart)
            DbCommandWrapper.AddInParameter("@DateEnd", DbType.DateTime, eventInfo.DateEnd)
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, eventInfo.Location)
            DbCommandWrapper.AddInParameter("@NumOfInvitation", DbType.Int32, eventInfo.NumOfInvitation)
            DbCommandWrapper.AddInParameter("@AreaCoordinator", DbType.AnsiString, eventInfo.AreaCoordinator)
            DbCommandWrapper.AddInParameter("@Observer", DbType.AnsiString, eventInfo.Observer)
            DbCommandWrapper.AddInParameter("@EventInfoStatus", DbType.Byte, eventInfo.EventInfoStatus)
            DbCommandWrapper.AddInParameter("@IsConfirmed", DbType.Byte, eventInfo.IsConfirmed)
            DbCommandWrapper.AddInParameter("@ConfirmedDateStart", DbType.DateTime, eventInfo.ConfirmedDateStart)
            DbCommandWrapper.AddInParameter("@ConfirmedDateEnd", DbType.DateTime, eventInfo.ConfirmedDateEnd)
            DbCommandWrapper.AddInParameter("@ConfirmedLocation", DbType.AnsiString, eventInfo.ConfirmedLocation)
            DbCommandWrapper.AddInParameter("@ConfirmedNumOfInvitation", DbType.Int32, eventInfo.ConfirmedNumOfInvitation)
            DbCommandWrapper.AddInParameter("@ConfirmedTotalCost", DbType.Currency, eventInfo.ConfirmedTotalCost)
            DbCommandWrapper.AddInParameter("@ConfirmedEstFileUpload", DbType.AnsiString, eventInfo.ConfirmedEstFileUpload)
            DbCommandWrapper.AddInParameter("@ConfirmedComment", DbType.AnsiString, eventInfo.ConfirmedComment)
            DbCommandWrapper.AddInParameter("@IsRealization", DbType.Byte, eventInfo.IsRealization)
            DbCommandWrapper.AddInParameter("@EventRealizationNo", DbType.AnsiString, eventInfo.EventRealizationNo)
            DbCommandWrapper.AddInParameter("@RealDateStart", DbType.DateTime, eventInfo.RealDateStart)
            DbCommandWrapper.AddInParameter("@RealDateEnd", DbType.DateTime, eventInfo.RealDateEnd)
            DbCommandWrapper.AddInParameter("@RealLocation", DbType.AnsiString, eventInfo.RealLocation)
            DbCommandWrapper.AddInParameter("@RealNumOfInvitation", DbType.Int32, eventInfo.RealNumOfInvitation)
            DbCommandWrapper.AddInParameter("@RealNumOfParticipants", DbType.Int32, eventInfo.RealNumOfParticipants)
            DbCommandWrapper.AddInParameter("@RealTotalCost", DbType.Currency, eventInfo.RealTotalCost)
            DbCommandWrapper.AddInParameter("@RealCostDetailFile", DbType.AnsiString, eventInfo.RealCostDetailFile)
            DbCommandWrapper.AddInParameter("@RealVideoFile", DbType.AnsiString, eventInfo.RealVideoFile)
            DbCommandWrapper.AddInParameter("@RealMatPromoFile", DbType.AnsiString, eventInfo.RealMatPromoFile)
            DbCommandWrapper.AddInParameter("@RealComment", DbType.AnsiString, eventInfo.RealComment)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, eventInfo.RequestDate)
            DbCommandWrapper.AddInParameter("@RequestTotalCost", DbType.Currency, eventInfo.RequestTotalCost)
            DbCommandWrapper.AddInParameter("@ApprovalCost", DbType.Currency, eventInfo.ApprovalCost)
            DbCommandWrapper.AddInParameter("@RealApprovalCost", DbType.Currency, eventInfo.RealApprovalCost)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventInfo.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, eventInfo.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(eventInfo.Dealer))
            DbCommandWrapper.AddInParameter("@EventTypeID", DbType.Int32, Me.GetRefObject(eventInfo.EventType))
            DbCommandWrapper.AddInParameter("@EventMasterID", DbType.Int32, Me.GetRefObject(eventInfo.EventMaster))

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

            Dim eventInfo As EventInfo = CType(obj, EventInfo)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventInfo.ID)
            DbCommandWrapper.AddInParameter("@BabitAllocationID", DbType.Int32, eventInfo.BabitAllocationID)
            DbCommandWrapper.AddInParameter("@EventRequestNo", DbType.AnsiString, eventInfo.EventRequestNo)
            DbCommandWrapper.AddInParameter("@EventApprovalNo", DbType.AnsiString, eventInfo.EventApprovalNo)
            DbCommandWrapper.AddInParameter("@DateStart", DbType.DateTime, eventInfo.DateStart)
            DbCommandWrapper.AddInParameter("@DateEnd", DbType.DateTime, eventInfo.DateEnd)
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, eventInfo.Location)
            DbCommandWrapper.AddInParameter("@NumOfInvitation", DbType.Int32, eventInfo.NumOfInvitation)
            DbCommandWrapper.AddInParameter("@AreaCoordinator", DbType.AnsiString, eventInfo.AreaCoordinator)
            DbCommandWrapper.AddInParameter("@Observer", DbType.AnsiString, eventInfo.Observer)
            DbCommandWrapper.AddInParameter("@EventInfoStatus", DbType.Byte, eventInfo.EventInfoStatus)
            DbCommandWrapper.AddInParameter("@IsConfirmed", DbType.Byte, eventInfo.IsConfirmed)
            DbCommandWrapper.AddInParameter("@ConfirmedDateStart", DbType.DateTime, eventInfo.ConfirmedDateStart)
            DbCommandWrapper.AddInParameter("@ConfirmedDateEnd", DbType.DateTime, eventInfo.ConfirmedDateEnd)
            DbCommandWrapper.AddInParameter("@ConfirmedLocation", DbType.AnsiString, eventInfo.ConfirmedLocation)
            DbCommandWrapper.AddInParameter("@ConfirmedNumOfInvitation", DbType.Int32, eventInfo.ConfirmedNumOfInvitation)
            DbCommandWrapper.AddInParameter("@ConfirmedTotalCost", DbType.Currency, eventInfo.ConfirmedTotalCost)
            DbCommandWrapper.AddInParameter("@ConfirmedEstFileUpload", DbType.AnsiString, eventInfo.ConfirmedEstFileUpload)
            DbCommandWrapper.AddInParameter("@ConfirmedComment", DbType.AnsiString, eventInfo.ConfirmedComment)
            DbCommandWrapper.AddInParameter("@IsRealization", DbType.Byte, eventInfo.IsRealization)
            DbCommandWrapper.AddInParameter("@EventRealizationNo", DbType.AnsiString, eventInfo.EventRealizationNo)
            DbCommandWrapper.AddInParameter("@RealDateStart", DbType.DateTime, eventInfo.RealDateStart)
            DbCommandWrapper.AddInParameter("@RealDateEnd", DbType.DateTime, eventInfo.RealDateEnd)
            DbCommandWrapper.AddInParameter("@RealLocation", DbType.AnsiString, eventInfo.RealLocation)
            DbCommandWrapper.AddInParameter("@RealNumOfInvitation", DbType.Int32, eventInfo.RealNumOfInvitation)
            DbCommandWrapper.AddInParameter("@RealNumOfParticipants", DbType.Int32, eventInfo.RealNumOfParticipants)
            DbCommandWrapper.AddInParameter("@RealTotalCost", DbType.Currency, eventInfo.RealTotalCost)
            DbCommandWrapper.AddInParameter("@RealCostDetailFile", DbType.AnsiString, eventInfo.RealCostDetailFile)
            DbCommandWrapper.AddInParameter("@RealVideoFile", DbType.AnsiString, eventInfo.RealVideoFile)
            DbCommandWrapper.AddInParameter("@RealMatPromoFile", DbType.AnsiString, eventInfo.RealMatPromoFile)
            DbCommandWrapper.AddInParameter("@RealComment", DbType.AnsiString, eventInfo.RealComment)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, eventInfo.RequestDate)
            DbCommandWrapper.AddInParameter("@RequestTotalCost", DbType.Currency, eventInfo.RequestTotalCost)
            DbCommandWrapper.AddInParameter("@ApprovalCost", DbType.Currency, eventInfo.ApprovalCost)
            DbCommandWrapper.AddInParameter("@RealApprovalCost", DbType.Currency, eventInfo.RealApprovalCost)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventInfo.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eventInfo.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(eventInfo.Dealer))
            DbCommandWrapper.AddInParameter("@EventTypeID", DbType.Int32, Me.GetRefObject(eventInfo.EventType))
            DbCommandWrapper.AddInParameter("@EventMasterID", DbType.Int32, Me.GetRefObject(eventInfo.EventMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventInfo

            Dim eventInfo As EventInfo = New EventInfo

            eventInfo.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitAllocationID")) Then eventInfo.BabitAllocationID = CType(dr("BabitAllocationID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventRequestNo")) Then eventInfo.EventRequestNo = dr("EventRequestNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventApprovalNo")) Then eventInfo.EventApprovalNo = dr("EventApprovalNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DateStart")) Then eventInfo.DateStart = CType(dr("DateStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DateEnd")) Then eventInfo.DateEnd = CType(dr("DateEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Location")) Then eventInfo.Location = dr("Location").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NumOfInvitation")) Then eventInfo.NumOfInvitation = CType(dr("NumOfInvitation"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AreaCoordinator")) Then eventInfo.AreaCoordinator = dr("AreaCoordinator").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Observer")) Then eventInfo.Observer = dr("Observer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventInfoStatus")) Then eventInfo.EventInfoStatus = CType(dr("EventInfoStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsConfirmed")) Then eventInfo.IsConfirmed = CType(dr("IsConfirmed"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedDateStart")) Then eventInfo.ConfirmedDateStart = CType(dr("ConfirmedDateStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedDateEnd")) Then eventInfo.ConfirmedDateEnd = CType(dr("ConfirmedDateEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedLocation")) Then eventInfo.ConfirmedLocation = dr("ConfirmedLocation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedNumOfInvitation")) Then eventInfo.ConfirmedNumOfInvitation = CType(dr("ConfirmedNumOfInvitation"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedTotalCost")) Then eventInfo.ConfirmedTotalCost = CType(dr("ConfirmedTotalCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedEstFileUpload")) Then eventInfo.ConfirmedEstFileUpload = dr("ConfirmedEstFileUpload").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedComment")) Then eventInfo.ConfirmedComment = dr("ConfirmedComment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsRealization")) Then eventInfo.IsRealization = CType(dr("IsRealization"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("EventRealizationNo")) Then eventInfo.EventRealizationNo = dr("EventRealizationNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RealDateStart")) Then eventInfo.RealDateStart = CType(dr("RealDateStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RealDateEnd")) Then eventInfo.RealDateEnd = CType(dr("RealDateEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RealLocation")) Then eventInfo.RealLocation = dr("RealLocation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RealNumOfInvitation")) Then eventInfo.RealNumOfInvitation = CType(dr("RealNumOfInvitation"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RealNumOfParticipants")) Then eventInfo.RealNumOfParticipants = CType(dr("RealNumOfParticipants"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RealTotalCost")) Then eventInfo.RealTotalCost = CType(dr("RealTotalCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RealCostDetailFile")) Then eventInfo.RealCostDetailFile = dr("RealCostDetailFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RealVideoFile")) Then eventInfo.RealVideoFile = dr("RealVideoFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RealMatPromoFile")) Then eventInfo.RealMatPromoFile = dr("RealMatPromoFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RealComment")) Then eventInfo.RealComment = dr("RealComment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RequestDate")) Then eventInfo.RequestDate = CType(dr("RequestDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestTotalCost")) Then eventInfo.RequestTotalCost = CType(dr("RequestTotalCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovalCost")) Then eventInfo.ApprovalCost = CType(dr("ApprovalCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RealApprovalCost")) Then eventInfo.RealApprovalCost = CType(dr("RealApprovalCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventInfo.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventInfo.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventInfo.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventInfo.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventInfo.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                eventInfo.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EventTypeID")) Then
                eventInfo.EventType = New EventType(CType(dr("EventTypeID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EventMasterID")) Then
                eventInfo.EventMaster = New EventMaster(CType(dr("EventMasterID"), Integer))
            End If

            Return eventInfo

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventInfo) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventInfo), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventInfo).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

