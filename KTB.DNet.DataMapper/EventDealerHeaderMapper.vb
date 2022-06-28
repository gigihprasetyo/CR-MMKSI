
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventDealerHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 15/05/2019 - 8:07:20
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

    Public Class EventDealerHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventDealerHeader"
        Private m_UpdateStatement As String = "up_UpdateEventDealerHeader"
        Private m_RetrieveStatement As String = "up_RetrieveEventDealerHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveEventDealerHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventDealerHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventDealerHeader As EventDealerHeader = Nothing
            While dr.Read

                eventDealerHeader = Me.CreateObject(dr)

            End While

            Return eventDealerHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventDealerHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim eventDealerHeader As EventDealerHeader = Me.CreateObject(dr)
                eventDealerHeaderList.Add(eventDealerHeader)
            End While

            Return eventDealerHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventDealerHeader As EventDealerHeader = CType(obj, EventDealerHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventDealerHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventDealerHeader As EventDealerHeader = CType(obj, EventDealerHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, eventDealerHeader.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, eventDealerHeader.PeriodEnd)
            DbCommandWrapper.AddInParameter("@EventName", DbType.AnsiString, eventDealerHeader.EventName)
            DbCommandWrapper.AddInParameter("@SubsidyTarget", DbType.Currency, eventDealerHeader.SubsidyTarget)
            DbCommandWrapper.AddInParameter("@MaxSubsidy", DbType.Currency, eventDealerHeader.MaxSubsidy)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventDealerHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, eventDealerHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@EventCategoryHeaderID", DbType.Int32, eventDealerHeader.EventCategoryHeaderID)
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(eventDealerHeader.Category))

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

            Dim eventDealerHeader As EventDealerHeader = CType(obj, EventDealerHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventDealerHeader.ID)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, eventDealerHeader.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, eventDealerHeader.PeriodEnd)
            DbCommandWrapper.AddInParameter("@EventName", DbType.AnsiString, eventDealerHeader.EventName)
            DbCommandWrapper.AddInParameter("@SubsidyTarget", DbType.Currency, eventDealerHeader.SubsidyTarget)
            DbCommandWrapper.AddInParameter("@MaxSubsidy", DbType.Currency, eventDealerHeader.MaxSubsidy)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventDealerHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eventDealerHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(eventDealerHeader.Category))
            'DbCommandWrapper.AddInParameter("@EventCategoryHeaderID", DbType.Int32, eventDealerHeader.EventCategoryHeaderID)

            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventDealerHeader

            Dim eventDealerHeader As EventDealerHeader = New EventDealerHeader

            eventDealerHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodStart")) Then eventDealerHeader.PeriodStart = CType(dr("PeriodStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodEnd")) Then eventDealerHeader.PeriodEnd = CType(dr("PeriodEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EventName")) Then eventDealerHeader.EventName = dr("EventName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubsidyTarget")) Then eventDealerHeader.SubsidyTarget = CType(dr("SubsidyTarget"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxSubsidy")) Then eventDealerHeader.MaxSubsidy = CType(dr("MaxSubsidy"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventDealerHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventDealerHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventDealerHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventDealerHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventDealerHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("EventCategoryHeaderID")) Then
            '    eventDealerHeader.EventCategoryHeaderID = CType(dr("EventCategoryHeaderID"), Integer)
            'End If

            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                eventDealerHeader.Category = New Category(CType(dr("CategoryID"), Integer))
            End If
            Return eventDealerHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventDealerHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventDealerHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventDealerHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

