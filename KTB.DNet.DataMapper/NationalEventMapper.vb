#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : NationalEvent Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 4/21/2021 - 11:26:15 AM
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

    Public Class NationalEventMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertNationalEvent"
        Private m_UpdateStatement As String = "up_UpdateNationalEvent"
        Private m_RetrieveStatement As String = "up_RetrieveNationalEvent"
        Private m_RetrieveListStatement As String = "up_RetrieveNationalEventList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteNationalEvent"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim nationalEvent As NationalEvent = Nothing
            While dr.Read

                nationalEvent = Me.CreateObject(dr)

            End While

            Return nationalEvent

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim nationalEventList As ArrayList = New ArrayList

            While dr.Read
                Dim nationalEvent As NationalEvent = Me.CreateObject(dr)
                nationalEventList.Add(nationalEvent)
            End While

            Return nationalEventList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim nationalEvent As NationalEvent = CType(obj, NationalEvent)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, nationalEvent.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim nationalEvent As NationalEvent = CType(obj, NationalEvent)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, nationalEvent.RegNumber)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, nationalEvent.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, nationalEvent.PeriodEnd)
            DbCommandWrapper.AddInParameter("@DealerCityID", DbType.AnsiString, nationalEvent.DealerCityID)
            DbCommandWrapper.AddInParameter("@TargetProspect", DbType.Int32, nationalEvent.TargetProspect)
            DbCommandWrapper.AddInParameter("@TargetSPK", DbType.Int32, nationalEvent.TargetSPK)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, nationalEvent.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, nationalEvent.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@NationalEventTypeID", DbType.Int32, Me.GetRefObject(nationalEvent.NationalEventType))
            DbCommandWrapper.AddInParameter("@NationalEventCityID", DbType.Int32, Me.GetRefObject(nationalEvent.NationalEventCity))
            DbCommandWrapper.AddInParameter("@NationalEventVenueID", DbType.Int32, Me.GetRefObject(nationalEvent.NationalEventVenue))
            DbCommandWrapper.AddInParameter("@DealerArea1ID", DbType.Int32, Me.GetRefObject(nationalEvent.DealerArea1))

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

            Dim nationalEvent As NationalEvent = CType(obj, NationalEvent)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, nationalEvent.ID)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, nationalEvent.RegNumber)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, nationalEvent.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, nationalEvent.PeriodEnd)
            DbCommandWrapper.AddInParameter("@DealerCityID", DbType.AnsiString, nationalEvent.DealerCityID)
            DbCommandWrapper.AddInParameter("@TargetProspect", DbType.Int32, nationalEvent.TargetProspect)
            DbCommandWrapper.AddInParameter("@TargetSPK", DbType.Int32, nationalEvent.TargetSPK)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, nationalEvent.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, nationalEvent.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@NationalEventTypeID", DbType.Int32, Me.GetRefObject(nationalEvent.NationalEventType))
            DbCommandWrapper.AddInParameter("@NationalEventCityID", DbType.Int32, Me.GetRefObject(nationalEvent.NationalEventCity))
            DbCommandWrapper.AddInParameter("@NationalEventVenueID", DbType.Int32, Me.GetRefObject(nationalEvent.NationalEventVenue))
            DbCommandWrapper.AddInParameter("@DealerArea1ID", DbType.Int32, Me.GetRefObject(nationalEvent.DealerArea1))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As NationalEvent

            Dim nationalEvent As NationalEvent = New NationalEvent

            nationalEvent.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then nationalEvent.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodStart")) Then nationalEvent.PeriodStart = CType(dr("PeriodStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodEnd")) Then nationalEvent.PeriodEnd = CType(dr("PeriodEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCityID")) Then nationalEvent.DealerCityID = dr("DealerCityID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TargetProspect")) Then nationalEvent.TargetProspect = CType(dr("TargetProspect"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TargetSPK")) Then nationalEvent.TargetSPK = CType(dr("TargetSPK"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then nationalEvent.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then nationalEvent.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then nationalEvent.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then nationalEvent.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then nationalEvent.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("NationalEventTypeID")) Then
                nationalEvent.NationalEventType = New NationalEventType(CType(dr("NationalEventTypeID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("NationalEventCityID")) Then
                nationalEvent.NationalEventCity = New NationalEventCity(CType(dr("NationalEventCityID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("NationalEventVenueID")) Then
                nationalEvent.NationalEventVenue = New NationalEventVenue(CType(dr("NationalEventVenueID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerArea1ID")) Then
                nationalEvent.DealerArea1 = New Area1(CType(dr("DealerArea1ID"), Integer))
            End If

            Return nationalEvent

        End Function

        Private Sub SetTableName()

            If Not (GetType(NationalEvent) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(NationalEvent), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(NationalEvent).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
