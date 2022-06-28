
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitMarketingBox Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 03/10/2019 - 14:05:46
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

    Public Class BabitMarketingBoxMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitMarketingBox"
        Private m_UpdateStatement As String = "up_UpdateBabitMarketingBox"
        Private m_RetrieveStatement As String = "up_RetrieveBabitMarketingBox"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitMarketingBoxList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitMarketingBox"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitMarketingBox As BabitMarketingBox = Nothing
            While dr.Read

                babitMarketingBox = Me.CreateObject(dr)

            End While

            Return babitMarketingBox

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitMarketingBoxList As ArrayList = New ArrayList

            While dr.Read
                Dim babitMarketingBox As BabitMarketingBox = Me.CreateObject(dr)
                babitMarketingBoxList.Add(babitMarketingBox)
            End While

            Return babitMarketingBoxList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitMarketingBox As BabitMarketingBox = CType(obj, BabitMarketingBox)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, babitMarketingBox.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitMarketingBox As BabitMarketingBox = CType(obj, BabitMarketingBox)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            DbCommandWrapper.AddInParameter("@SubMissionID", DbType.AnsiString, babitMarketingBox.SubMissionID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, babitMarketingBox.DealerID)
            DbCommandWrapper.AddInParameter("@BabitType", DbType.AnsiString, babitMarketingBox.BabitType)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, babitMarketingBox.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, babitMarketingBox.EndDate)
            DbCommandWrapper.AddInParameter("@EventName", DbType.AnsiString, babitMarketingBox.EventName)
            DbCommandWrapper.AddInParameter("@EventLocation", DbType.AnsiString, babitMarketingBox.EventLocation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitMarketingBox.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@FileTimeLastModified", DbType.DateTime, babitMarketingBox.FileTimeLastModified)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitMarketingBox.LastUpdateBy)
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

            Dim babitMarketingBox As BabitMarketingBox = CType(obj, BabitMarketingBox)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, babitMarketingBox.ID)
            DbCommandWrapper.AddInParameter("@SubMissionID", DbType.AnsiString, babitMarketingBox.SubMissionID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, babitMarketingBox.DealerCode)
            DbCommandWrapper.AddInParameter("@BabitType", DbType.AnsiString, babitMarketingBox.BabitType)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, babitMarketingBox.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, babitMarketingBox.EndDate)
            DbCommandWrapper.AddInParameter("@EventName", DbType.AnsiString, babitMarketingBox.EventName)
            DbCommandWrapper.AddInParameter("@EventLocation", DbType.AnsiString, babitMarketingBox.EventLocation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitMarketingBox.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitMarketingBox.CreatedBy)
            DbCommandWrapper.AddInParameter("@FileTimeLastModified", DbType.DateTime, babitMarketingBox.FileTimeLastModified)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitMarketingBox

            Dim babitMarketingBox As BabitMarketingBox = New BabitMarketingBox

            babitMarketingBox.ID = CType(dr("ID"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("SubMissionID")) Then babitMarketingBox.SubMissionID = dr("SubMissionID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then babitMarketingBox.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitType")) Then babitMarketingBox.BabitType = dr("BabitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartDate")) Then babitMarketingBox.StartDate = CType(dr("StartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EndDate")) Then babitMarketingBox.EndDate = CType(dr("EndDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EventName")) Then babitMarketingBox.EventName = dr("EventName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventLocation")) Then babitMarketingBox.EventLocation = dr("EventLocation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitMarketingBox.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitMarketingBox.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitMarketingBox.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitMarketingBox.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitMarketingBox.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FileTimeLastModified")) Then babitMarketingBox.FileTimeLastModified = CType(dr("FileTimeLastModified"), DateTime)
            Return babitMarketingBox

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitMarketingBox) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitMarketingBox), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitMarketingBox).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

