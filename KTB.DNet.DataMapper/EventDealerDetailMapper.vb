
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventDealerDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 15/05/2019 - 8:12:14
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

    Public Class EventDealerDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventDealerDetail"
        Private m_UpdateStatement As String = "up_UpdateEventDealerDetail"
        Private m_RetrieveStatement As String = "up_RetrieveEventDealerDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveEventDealerDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventDealerDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventDealerDetail As EventDealerDetail = Nothing
            While dr.Read

                eventDealerDetail = Me.CreateObject(dr)

            End While

            Return eventDealerDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventDealerDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim eventDealerDetail As EventDealerDetail = Me.CreateObject(dr)
                eventDealerDetailList.Add(eventDealerDetail)
            End While

            Return eventDealerDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventDealerDetail As EventDealerDetail = CType(obj, EventDealerDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventDealerDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventDealerDetail As EventDealerDetail = CType(obj, EventDealerDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventDealerDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, eventDealerDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(eventDealerDetail.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(eventDealerDetail.DealerBranch))
            DbCommandWrapper.AddInParameter("@EventDealerHeaderID", DbType.Int32, Me.GetRefObject(eventDealerDetail.EventDealerHeader))
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, eventDealerDetail.DealerID)
            'DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, eventDealerDetail.DealerBranchID)
            'DbCommandWrapper.AddInParameter("@EventDealerHeaderID", DbType.Int32, eventDealerDetail.EventDealerHeaderID)

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

            Dim eventDealerDetail As EventDealerDetail = CType(obj, EventDealerDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventDealerDetail.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventDealerDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eventDealerDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(eventDealerDetail.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(eventDealerDetail.DealerBranch))

            'If Not IsNothing(eventDealerDetail.DealerBranch) Then
            'Else
            '    DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Nothing)
            'End If
            DbCommandWrapper.AddInParameter("@EventDealerHeaderID", DbType.Int32, Me.GetRefObject(eventDealerDetail.EventDealerHeader))
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, eventDealerDetail.DealerID)
            'DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, eventDealerDetail.DealerBranchID)
            'DbCommandWrapper.AddInParameter("@EventDealerHeaderID", DbType.Int32, eventDealerDetail.EventDealerHeaderID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventDealerDetail

            Dim eventDealerDetail As EventDealerDetail = New EventDealerDetail

            eventDealerDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventDealerDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventDealerDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventDealerDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventDealerDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventDealerDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
            '    eventDealerDetail.DealerID = CType(dr("DealerID"), Short)
            'End If
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
            '    eventDealerDetail.DealerBranchID = CType(dr("DealerBranchID"), Integer)
            'End If
            'If Not dr.IsDBNull(dr.GetOrdinal("EventDealerHeaderID")) Then
            '    eventDealerDetail.EventDealerHeaderID = CType(dr("EventDealerHeaderID"), Integer)
            'End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                eventDealerDetail.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                eventDealerDetail.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EventDealerHeaderID")) Then
                eventDealerDetail.EventDealerHeader = New EventDealerHeader(CType(dr("EventDealerHeaderID"), Integer))
            End If
            Return eventDealerDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventDealerDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventDealerDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventDealerDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

