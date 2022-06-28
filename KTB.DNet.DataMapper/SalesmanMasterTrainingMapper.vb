#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanMasterTraining Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/1/2007 - 4:40:58 PM
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

    Public Class SalesmanMasterTrainingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanMasterTraining"
        Private m_UpdateStatement As String = "up_UpdateSalesmanMasterTraining"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanMasterTraining"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanMasterTrainingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanMasterTraining"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanMasterTraining As SalesmanMasterTraining = Nothing
            While dr.Read

                salesmanMasterTraining = Me.CreateObject(dr)

            End While

            Return salesmanMasterTraining

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanMasterTrainingList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanMasterTraining As SalesmanMasterTraining = Me.CreateObject(dr)
                salesmanMasterTrainingList.Add(salesmanMasterTraining)
            End While

            Return salesmanMasterTrainingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanMasterTraining As SalesmanMasterTraining = CType(obj, SalesmanMasterTraining)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanMasterTraining.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanMasterTraining As SalesmanMasterTraining = CType(obj, SalesmanMasterTraining)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@TrainingCode", DbType.AnsiString, salesmanMasterTraining.TrainingCode)
            DbCommandWrapper.AddInParameter("@TrainingTitle", DbType.AnsiString, salesmanMasterTraining.TrainingTitle)
            DbCommandWrapper.AddInParameter("@StartingDate", DbType.DateTime, salesmanMasterTraining.StartingDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, salesmanMasterTraining.EndDate)
            DbCommandWrapper.AddInParameter("@Trainer1", DbType.AnsiString, salesmanMasterTraining.Trainer1)
            DbCommandWrapper.AddInParameter("@Trainer2", DbType.AnsiString, salesmanMasterTraining.Trainer2)
            DbCommandWrapper.AddInParameter("@Trainer3", DbType.AnsiString, salesmanMasterTraining.Trainer3)
            DbCommandWrapper.AddInParameter("@AttendanceTarget", DbType.Int16, salesmanMasterTraining.AttendanceTarget)
            DbCommandWrapper.AddInParameter("@TrainingPlace", DbType.AnsiString, salesmanMasterTraining.TrainingPlace)
            DbCommandWrapper.AddInParameter("@Prerequisite", DbType.AnsiString, salesmanMasterTraining.Prerequisite)
            DbCommandWrapper.AddInParameter("@AnnouncementContent", DbType.AnsiString, salesmanMasterTraining.AnnouncementContent)
            DbCommandWrapper.AddInParameter("@IsReleased", DbType.Int16, salesmanMasterTraining.IsReleased)
            DbCommandWrapper.AddInParameter("@AnnouncementFileName", DbType.AnsiString, salesmanMasterTraining.AnnouncementFileName)
            DBCommandWrapper.AddInParameter("@MaterialFileName", DbType.AnsiString, salesmanMasterTraining.MaterialFileName)
            DBCommandWrapper.AddInParameter("@RegisterStartingDate", DbType.DateTime, salesmanMasterTraining.RegisterStartingDate)
            DBCommandWrapper.AddInParameter("@RegisterEndDate", DbType.DateTime, salesmanMasterTraining.RegisterEndDate)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanMasterTraining.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanMasterTraining.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanTrainingTypeID", DbType.Int32, Me.GetRefObject(salesmanMasterTraining.SalesmanTrainingType))

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

            Dim salesmanMasterTraining As SalesmanMasterTraining = CType(obj, SalesmanMasterTraining)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanMasterTraining.ID)
            DbCommandWrapper.AddInParameter("@TrainingCode", DbType.AnsiString, salesmanMasterTraining.TrainingCode)
            DbCommandWrapper.AddInParameter("@TrainingTitle", DbType.AnsiString, salesmanMasterTraining.TrainingTitle)
            DbCommandWrapper.AddInParameter("@StartingDate", DbType.DateTime, salesmanMasterTraining.StartingDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, salesmanMasterTraining.EndDate)
            DbCommandWrapper.AddInParameter("@Trainer1", DbType.AnsiString, salesmanMasterTraining.Trainer1)
            DbCommandWrapper.AddInParameter("@Trainer2", DbType.AnsiString, salesmanMasterTraining.Trainer2)
            DbCommandWrapper.AddInParameter("@Trainer3", DbType.AnsiString, salesmanMasterTraining.Trainer3)
            DbCommandWrapper.AddInParameter("@AttendanceTarget", DbType.Int16, salesmanMasterTraining.AttendanceTarget)
            DbCommandWrapper.AddInParameter("@TrainingPlace", DbType.AnsiString, salesmanMasterTraining.TrainingPlace)
            DbCommandWrapper.AddInParameter("@Prerequisite", DbType.AnsiString, salesmanMasterTraining.Prerequisite)
            DbCommandWrapper.AddInParameter("@AnnouncementContent", DbType.AnsiString, salesmanMasterTraining.AnnouncementContent)
            DbCommandWrapper.AddInParameter("@IsReleased", DbType.Int16, salesmanMasterTraining.IsReleased)
            DbCommandWrapper.AddInParameter("@AnnouncementFileName", DbType.AnsiString, salesmanMasterTraining.AnnouncementFileName)
            DBCommandWrapper.AddInParameter("@MaterialFileName", DbType.AnsiString, salesmanMasterTraining.MaterialFileName)
            DBCommandWrapper.AddInParameter("@RegisterStartingDate", DbType.DateTime, salesmanMasterTraining.RegisterStartingDate)
            DBCommandWrapper.AddInParameter("@RegisterEndDate", DbType.DateTime, salesmanMasterTraining.RegisterEndDate)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanMasterTraining.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanMasterTraining.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanTrainingTypeID", DbType.Int32, Me.GetRefObject(salesmanMasterTraining.SalesmanTrainingType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanMasterTraining

            Dim salesmanMasterTraining As SalesmanMasterTraining = New SalesmanMasterTraining

            salesmanMasterTraining.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TrainingCode")) Then salesmanMasterTraining.TrainingCode = dr("TrainingCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TrainingTitle")) Then salesmanMasterTraining.TrainingTitle = dr("TrainingTitle").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartingDate")) Then salesmanMasterTraining.StartingDate = CType(dr("StartingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EndDate")) Then salesmanMasterTraining.EndDate = CType(dr("EndDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Trainer1")) Then salesmanMasterTraining.Trainer1 = dr("Trainer1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Trainer2")) Then salesmanMasterTraining.Trainer2 = dr("Trainer2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Trainer3")) Then salesmanMasterTraining.Trainer3 = dr("Trainer3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AttendanceTarget")) Then salesmanMasterTraining.AttendanceTarget = CType(dr("AttendanceTarget"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TrainingPlace")) Then salesmanMasterTraining.TrainingPlace = dr("TrainingPlace").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Prerequisite")) Then salesmanMasterTraining.Prerequisite = dr("Prerequisite").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AnnouncementContent")) Then salesmanMasterTraining.AnnouncementContent = dr("AnnouncementContent").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsReleased")) Then salesmanMasterTraining.IsReleased = CType(dr("IsReleased"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AnnouncementFileName")) Then salesmanMasterTraining.AnnouncementFileName = dr("AnnouncementFileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialFileName")) Then salesmanMasterTraining.MaterialFileName = dr("MaterialFileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RegisterStartingDate")) Then salesmanMasterTraining.RegisterStartingDate = CType(dr("RegisterStartingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RegisterEndDate")) Then salesmanMasterTraining.RegisterEndDate = CType(dr("RegisterEndDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanMasterTraining.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanMasterTraining.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanMasterTraining.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanMasterTraining.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanMasterTraining.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanTrainingTypeID")) Then
                salesmanMasterTraining.SalesmanTrainingType = New SalesmanTrainingType(CType(dr("SalesmanTrainingTypeID"), Integer))
            End If

            Return salesmanMasterTraining

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanMasterTraining) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanMasterTraining), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanMasterTraining).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

