#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : StallWorkingTime Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:05:06 PM
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

    Public Class StallWorkingTimeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertStallWorkingTime"
        Private m_UpdateStatement As String = "up_UpdateStallWorkingTime"
        Private m_RetrieveStatement As String = "up_RetrieveStallWorkingTime"
        Private m_RetrieveListStatement As String = "up_RetrieveStallWorkingTimeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteStallWorkingTime"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim StallWorkingTime As StallWorkingTime = Nothing
            While dr.Read

                StallWorkingTime = Me.CreateObject(dr)

            End While

            Return StallWorkingTime

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim StallWorkingTimeList As ArrayList = New ArrayList

            While dr.Read
                Dim StallWorkingTime As StallWorkingTime = Me.CreateObject(dr)
                StallWorkingTimeList.Add(StallWorkingTime)
            End While

            Return StallWorkingTimeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim StallWorkingTime As StallWorkingTime = CType(obj, StallWorkingTime)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, StallWorkingTime.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim StallWorkingTime As StallWorkingTime = CType(obj, StallWorkingTime)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Tanggal", DbType.DateTime, StallWorkingTime.Tanggal)
            DbCommandWrapper.AddInParameter("@TimeStart", DbType.Time, StallWorkingTime.TimeStart)
            DbCommandWrapper.AddInParameter("@TimeEnd", DbType.Time, StallWorkingTime.TimeEnd)
            DbCommandWrapper.AddInParameter("@RestTimeStart", DbType.Time, StallWorkingTime.RestTimeStart)
            DbCommandWrapper.AddInParameter("@RestTimeEnd", DbType.Time, StallWorkingTime.RestTimeEnd)
            DbCommandWrapper.AddInParameter("@IsHoliday", DbType.Int16, StallWorkingTime.IsHoliday)
            DbCommandWrapper.AddInParameter("@VisitType", DbType.Int16, StallWorkingTime.VisitType)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, StallWorkingTime.Notes)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, StallWorkingTime.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, StallWorkingTime.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(StallWorkingTime.Dealer))
            DbCommandWrapper.AddInParameter("@StallMasterID", DbType.Int32, Me.GetRefObject(StallWorkingTime.StallMaster))

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

            Dim StallWorkingTime As StallWorkingTime = CType(obj, StallWorkingTime)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, StallWorkingTime.ID)
            DbCommandWrapper.AddInParameter("@Tanggal", DbType.DateTime, StallWorkingTime.Tanggal)
            DbCommandWrapper.AddInParameter("@TimeStart", DbType.Time, StallWorkingTime.TimeStart)
            DbCommandWrapper.AddInParameter("@TimeEnd", DbType.Time, StallWorkingTime.TimeEnd)
            DbCommandWrapper.AddInParameter("@RestTimeStart", DbType.Time, StallWorkingTime.RestTimeStart)
            DbCommandWrapper.AddInParameter("@RestTimeEnd", DbType.Time, StallWorkingTime.RestTimeEnd)
            DbCommandWrapper.AddInParameter("@IsHoliday", DbType.Int16, StallWorkingTime.IsHoliday)
            DbCommandWrapper.AddInParameter("@VisitType", DbType.Int16, StallWorkingTime.VisitType)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, StallWorkingTime.Notes)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, StallWorkingTime.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, StallWorkingTime.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(StallWorkingTime.Dealer))
            DbCommandWrapper.AddInParameter("@StallMasterID", DbType.Int32, Me.GetRefObject(StallWorkingTime.StallMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As StallWorkingTime

            Dim StallWorkingTime As StallWorkingTime = New StallWorkingTime

            StallWorkingTime.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Tanggal")) Then StallWorkingTime.Tanggal = CType(dr("Tanggal").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TimeStart")) Then StallWorkingTime.TimeStart = CType(dr("TimeStart").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TimeEnd")) Then StallWorkingTime.TimeEnd = CType(dr("TimeEnd").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RestTimeStart")) Then StallWorkingTime.RestTimeStart = CType(dr("RestTimeStart").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RestTimeEnd")) Then StallWorkingTime.RestTimeEnd = CType(dr("RestTimeEnd").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsHoliday")) Then StallWorkingTime.IsHoliday = CType(dr("IsHoliday"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("VisitType")) Then StallWorkingTime.VisitType = CType(dr("VisitType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then StallWorkingTime.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then StallWorkingTime.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then StallWorkingTime.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then StallWorkingTime.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then StallWorkingTime.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then StallWorkingTime.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                StallWorkingTime.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("StallMasterID")) Then
                StallWorkingTime.StallMaster = New StallMaster(CType(dr("StallMasterID"), Integer))
            End If

            Return StallWorkingTime

        End Function

        Private Sub SetTableName()

            If Not (GetType(StallWorkingTime) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(StallWorkingTime), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(StallWorkingTime).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

