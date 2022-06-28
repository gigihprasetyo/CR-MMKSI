#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventParameter Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/24/2009 - 2:09:37 PM
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

    Public Class EventParameterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventParameter"
        Private m_UpdateStatement As String = "up_UpdateEventParameter"
        Private m_RetrieveStatement As String = "up_RetrieveEventParameter"
        Private m_RetrieveListStatement As String = "up_RetrieveEventParameterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventParameter"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventParameter As EventParameter = Nothing
            While dr.Read

                eventParameter = Me.CreateObject(dr)

            End While

            Return eventParameter

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventParameterList As ArrayList = New ArrayList

            While dr.Read
                Dim eventParameter As EventParameter = Me.CreateObject(dr)
                eventParameterList.Add(eventParameter)
            End While

            Return eventParameterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventParameter As EventParameter = CType(obj, EventParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventParameter.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventParameter As EventParameter = CType(obj, EventParameter)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@EventDateStart", DbType.DateTime, eventParameter.EventDateStart)
            DbCommandWrapper.AddInParameter("@EventDateEnd", DbType.DateTime, eventParameter.EventDateEnd)
            DbCommandWrapper.AddInParameter("@EventName", DbType.AnsiString, eventParameter.EventName)
            DbCommandWrapper.AddInParameter("@EventYear", DbType.Int32, eventParameter.EventYear)
            DbCommandWrapper.AddInParameter("@DirTarget", DbType.AnsiString, eventParameter.DirTarget)
            DbCommandWrapper.AddInParameter("@FileNameMaterial", DbType.AnsiString, eventParameter.FileNameMaterial)
            DbCommandWrapper.AddInParameter("@FileNameJuklak", DbType.AnsiString, eventParameter.FileNameJuklak)
            DbCommandWrapper.AddInParameter("@FileNamePendukung1", DbType.AnsiString, eventParameter.FileNamePendukung1)
            DbCommandWrapper.AddInParameter("@FileNamePendukung2", DbType.AnsiString, eventParameter.FileNamePendukung2)
            DbCommandWrapper.AddInParameter("@FileNamePendukung3", DbType.AnsiString, eventParameter.FileNamePendukung3)
            DbCommandWrapper.AddInParameter("@FileNamePendukung4", DbType.AnsiString, eventParameter.FileNamePendukung4)
            DbCommandWrapper.AddInParameter("@FileNamePendukung5", DbType.AnsiString, eventParameter.FileNamePendukung5)
            DbCommandWrapper.AddInParameter("@FileNamePendukung6", DbType.AnsiString, eventParameter.FileNamePendukung6)
            DbCommandWrapper.AddInParameter("@FileNamePendukung7", DbType.AnsiString, eventParameter.FileNamePendukung7)
            DbCommandWrapper.AddInParameter("@FileNamePendukung8", DbType.AnsiString, eventParameter.FileNamePendukung8)
            DbCommandWrapper.AddInParameter("@FileNamePendukung9", DbType.AnsiString, eventParameter.FileNamePendukung9)
            DbCommandWrapper.AddInParameter("@FileNamePendukung10", DbType.AnsiString, eventParameter.FileNamePendukung10)
            DbCommandWrapper.AddInParameter("@EventStatus", DbType.Byte, eventParameter.EventStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, eventParameter.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ActivityTypeID", DbType.Int32, Me.GetRefObject(eventParameter.ActivityType))
            DbCommandWrapper.AddInParameter("@SalesmanAreaID", DbType.Int32, Me.GetRefObject(eventParameter.SalesmanArea))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, Me.GetRefObject(eventParameter.Category))
            DbCommandWrapper.AddInParameter("@VehicleTypeID", DbType.Int16, Me.GetRefObject(eventParameter.VechileType))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(eventParameter.Dealer))

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

            Dim eventParameter As EventParameter = CType(obj, EventParameter)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventParameter.ID)
            DbCommandWrapper.AddInParameter("@EventDateStart", DbType.DateTime, eventParameter.EventDateStart)
            DbCommandWrapper.AddInParameter("@EventDateEnd", DbType.DateTime, eventParameter.EventDateEnd)
            DbCommandWrapper.AddInParameter("@EventName", DbType.AnsiString, eventParameter.EventName)
            DbCommandWrapper.AddInParameter("@EventYear", DbType.Int32, eventParameter.EventYear)
            DbCommandWrapper.AddInParameter("@DirTarget", DbType.AnsiString, eventParameter.DirTarget)
            DbCommandWrapper.AddInParameter("@FileNameMaterial", DbType.AnsiString, eventParameter.FileNameMaterial)
            DbCommandWrapper.AddInParameter("@FileNameJuklak", DbType.AnsiString, eventParameter.FileNameJuklak)
            DbCommandWrapper.AddInParameter("@FileNamePendukung1", DbType.AnsiString, eventParameter.FileNamePendukung1)
            DbCommandWrapper.AddInParameter("@FileNamePendukung2", DbType.AnsiString, eventParameter.FileNamePendukung2)
            DbCommandWrapper.AddInParameter("@FileNamePendukung3", DbType.AnsiString, eventParameter.FileNamePendukung3)
            DbCommandWrapper.AddInParameter("@FileNamePendukung4", DbType.AnsiString, eventParameter.FileNamePendukung4)
            DbCommandWrapper.AddInParameter("@FileNamePendukung5", DbType.AnsiString, eventParameter.FileNamePendukung5)
            DbCommandWrapper.AddInParameter("@FileNamePendukung6", DbType.AnsiString, eventParameter.FileNamePendukung6)
            DbCommandWrapper.AddInParameter("@FileNamePendukung7", DbType.AnsiString, eventParameter.FileNamePendukung7)
            DbCommandWrapper.AddInParameter("@FileNamePendukung8", DbType.AnsiString, eventParameter.FileNamePendukung8)
            DbCommandWrapper.AddInParameter("@FileNamePendukung9", DbType.AnsiString, eventParameter.FileNamePendukung9)
            DbCommandWrapper.AddInParameter("@FileNamePendukung10", DbType.AnsiString, eventParameter.FileNamePendukung10)
            DbCommandWrapper.AddInParameter("@EventStatus", DbType.Byte, eventParameter.EventStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventParameter.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eventParameter.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ActivityTypeID", DbType.Int32, Me.GetRefObject(eventParameter.ActivityType))
            DbCommandWrapper.AddInParameter("@SalesmanAreaID", DbType.Int32, Me.GetRefObject(eventParameter.SalesmanArea))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, Me.GetRefObject(eventParameter.Category))
            DbCommandWrapper.AddInParameter("@VehicleTypeID", DbType.Int16, Me.GetRefObject(eventParameter.VechileType))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(eventParameter.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventParameter

            Dim eventParameter As EventParameter = New EventParameter

            eventParameter.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventDateStart")) Then eventParameter.EventDateStart = CType(dr("EventDateStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EventDateEnd")) Then eventParameter.EventDateEnd = CType(dr("EventDateEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EventName")) Then eventParameter.EventName = dr("EventName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventYear")) Then eventParameter.EventYear = CType(dr("EventYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DirTarget")) Then eventParameter.DirTarget = dr("DirTarget").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNameMaterial")) Then eventParameter.FileNameMaterial = dr("FileNameMaterial").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNameJuklak")) Then eventParameter.FileNameJuklak = dr("FileNameJuklak").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNamePendukung1")) Then eventParameter.FileNamePendukung1 = dr("FileNamePendukung1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNamePendukung2")) Then eventParameter.FileNamePendukung2 = dr("FileNamePendukung2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNamePendukung3")) Then eventParameter.FileNamePendukung3 = dr("FileNamePendukung3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNamePendukung4")) Then eventParameter.FileNamePendukung4 = dr("FileNamePendukung4").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNamePendukung5")) Then eventParameter.FileNamePendukung5 = dr("FileNamePendukung5").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNamePendukung6")) Then eventParameter.FileNamePendukung6 = dr("FileNamePendukung6").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNamePendukung7")) Then eventParameter.FileNamePendukung7 = dr("FileNamePendukung7").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNamePendukung8")) Then eventParameter.FileNamePendukung8 = dr("FileNamePendukung8").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNamePendukung9")) Then eventParameter.FileNamePendukung9 = dr("FileNamePendukung9").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNamePendukung10")) Then eventParameter.FileNamePendukung10 = dr("FileNamePendukung10").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventStatus")) Then eventParameter.EventStatus = CType(dr("EventStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventParameter.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventParameter.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventParameter.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventParameter.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventParameter.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityTypeID")) Then
                eventParameter.ActivityType = New ActivityType(CType(dr("ActivityTypeID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanAreaID")) Then
                eventParameter.SalesmanArea = New SalesmanArea(CType(dr("SalesmanAreaID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                eventParameter.Category = New Category(CType(dr("CategoryID"), Byte))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeID")) Then
                eventParameter.VechileType = New VechileType(CType(dr("VehicleTypeID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                eventParameter.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return eventParameter

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventParameter) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventParameter), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventParameter).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

