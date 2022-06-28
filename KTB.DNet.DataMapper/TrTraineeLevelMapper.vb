#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrTraineeLevel Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/5/2019 - 10:11:56 AM
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

    Public Class TrTraineeLevelMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrTraineeLevel"
        Private m_UpdateStatement As String = "up_UpdateTrTraineeLevel"
        Private m_RetrieveStatement As String = "up_RetrieveTrTraineeLevel"
        Private m_RetrieveListStatement As String = "up_RetrieveTrTraineeLevelList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrTraineeLevel"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trTraineeLevel As TrTraineeLevel = Nothing
            While dr.Read

                trTraineeLevel = Me.CreateObject(dr)

            End While

            Return trTraineeLevel

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trTraineeLevelList As ArrayList = New ArrayList

            While dr.Read
                Dim trTraineeLevel As TrTraineeLevel = Me.CreateObject(dr)
                trTraineeLevelList.Add(trTraineeLevel)
            End While

            Return trTraineeLevelList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTraineeLevel As TrTraineeLevel = CType(obj, TrTraineeLevel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTraineeLevel.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTraineeLevel As TrTraineeLevel = CType(obj, TrTraineeLevel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trTraineeLevel.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTraineeLevel.RowStatus)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, trTraineeLevel.Sequence)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trTraineeLevel.Status)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trTraineeLevel.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@JobPositionCategoryid", DbType.Int32, Me.GetRefObject(trTraineeLevel.JobPositionCategory))


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

            Dim trTraineeLevel As TrTraineeLevel = CType(obj, TrTraineeLevel)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTraineeLevel.ID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trTraineeLevel.Description)
            DbCommandWrapper.AddInParameter("@JobPositionCategoryID", DbType.Int32, Me.GetRefObject(trTraineeLevel.JobPositionCategory))
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, trTraineeLevel.Sequence)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTraineeLevel.RowStatus)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trTraineeLevel.Status)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trTraineeLevel.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrTraineeLevel

            Dim trTraineeLevel As TrTraineeLevel = New TrTraineeLevel

            trTraineeLevel.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then trTraineeLevel.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionCategoryID")) Then
                trTraineeLevel.JobPositionCategory = New JobPositionCategory(CType(dr("JobPositionCategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trTraineeLevel.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then trTraineeLevel.Sequence = CType(dr("Sequence"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trTraineeLevel.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trTraineeLevel.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trTraineeLevel.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trTraineeLevel.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trTraineeLevel.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trTraineeLevel

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrTraineeLevel) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrTraineeLevel), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrTraineeLevel).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
