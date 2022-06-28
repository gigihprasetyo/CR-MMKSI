#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrainingCode Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 11:12:41 AM
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

    Public Class TrainingCodeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrainingCode"
        Private m_UpdateStatement As String = "up_UpdateTrainingCode"
        Private m_RetrieveStatement As String = "up_RetrieveTrainingCode"
        Private m_RetrieveListStatement As String = "up_RetrieveTrainingCodeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrainingCode"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trainingCode As TrainingCode = Nothing
            While dr.Read

                trainingCode = Me.CreateObject(dr)

            End While

            Return trainingCode

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trainingCodeList As ArrayList = New ArrayList

            While dr.Read
                Dim trainingCode As TrainingCode = Me.CreateObject(dr)
                trainingCodeList.Add(trainingCode)
            End While

            Return trainingCodeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trainingCode As TrainingCode = CType(obj, TrainingCode)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trainingCode.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trainingCode As TrainingCode = CType(obj, TrainingCode)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@TrainingCode", DbType.AnsiString, trainingCode.TrainingCode)
            DbCommandWrapper.AddInParameter("@TitleOfTraining", DbType.AnsiString, trainingCode.TitleOfTraining)
            DbCommandWrapper.AddInParameter("@KindOfTraining", DbType.AnsiString, trainingCode.KindOfTraining)
            DbCommandWrapper.AddInParameter("@Schedule", DbType.DateTime, trainingCode.Schedule)
            DbCommandWrapper.AddInParameter("@Trainer1", DbType.AnsiString, trainingCode.Trainer1)
            DbCommandWrapper.AddInParameter("@Trainer2", DbType.AnsiString, trainingCode.Trainer2)
            DbCommandWrapper.AddInParameter("@Trainer3", DbType.AnsiString, trainingCode.Trainer3)
            DbCommandWrapper.AddInParameter("@TargetMember", DbType.Int32, trainingCode.TargetMember)
            DbCommandWrapper.AddInParameter("@Place", DbType.AnsiString, trainingCode.Place)
            DbCommandWrapper.AddInParameter("@Prerequisite", DbType.AnsiString, trainingCode.Prerequisite)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, trainingCode.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, trainingCode.PeriodEnd)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trainingCode.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trainingCode.LastUpdateBy)
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

            Dim trainingCode As TrainingCode = CType(obj, TrainingCode)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trainingCode.ID)
            DbCommandWrapper.AddInParameter("@TrainingCode", DbType.AnsiString, trainingCode.TrainingCode)
            DbCommandWrapper.AddInParameter("@TitleOfTraining", DbType.AnsiString, trainingCode.TitleOfTraining)
            DbCommandWrapper.AddInParameter("@KindOfTraining", DbType.AnsiString, trainingCode.KindOfTraining)
            DbCommandWrapper.AddInParameter("@Schedule", DbType.DateTime, trainingCode.Schedule)
            DbCommandWrapper.AddInParameter("@Trainer1", DbType.AnsiString, trainingCode.Trainer1)
            DbCommandWrapper.AddInParameter("@Trainer2", DbType.AnsiString, trainingCode.Trainer2)
            DbCommandWrapper.AddInParameter("@Trainer3", DbType.AnsiString, trainingCode.Trainer3)
            DbCommandWrapper.AddInParameter("@TargetMember", DbType.Int32, trainingCode.TargetMember)
            DbCommandWrapper.AddInParameter("@Place", DbType.AnsiString, trainingCode.Place)
            DbCommandWrapper.AddInParameter("@Prerequisite", DbType.AnsiString, trainingCode.Prerequisite)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, trainingCode.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, trainingCode.PeriodEnd)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trainingCode.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trainingCode.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrainingCode

            Dim trainingCode As TrainingCode = New TrainingCode

            trainingCode.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TrainingCode")) Then trainingCode.TrainingCode = dr("TrainingCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TitleOfTraining")) Then trainingCode.TitleOfTraining = dr("TitleOfTraining").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindOfTraining")) Then trainingCode.KindOfTraining = dr("KindOfTraining").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Schedule")) Then trainingCode.Schedule = CType(dr("Schedule"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Trainer1")) Then trainingCode.Trainer1 = dr("Trainer1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Trainer2")) Then trainingCode.Trainer2 = dr("Trainer2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Trainer3")) Then trainingCode.Trainer3 = dr("Trainer3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TargetMember")) Then trainingCode.TargetMember = CType(dr("TargetMember"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Place")) Then trainingCode.Place = dr("Place").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Prerequisite")) Then trainingCode.Prerequisite = dr("Prerequisite").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodStart")) Then trainingCode.PeriodStart = CType(dr("PeriodStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodEnd")) Then trainingCode.PeriodEnd = CType(dr("PeriodEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trainingCode.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trainingCode.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trainingCode.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trainingCode.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trainingCode.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trainingCode

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrainingCode) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrainingCode), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrainingCode).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

