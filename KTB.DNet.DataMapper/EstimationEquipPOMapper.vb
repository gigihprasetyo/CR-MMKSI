#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EstimationEquipPO Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/25/2009 - 10:00:38
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

    Public Class EstimationEquipPOMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEstimationEquipPO"
        Private m_UpdateStatement As String = "up_UpdateEstimationEquipPO"
        Private m_RetrieveStatement As String = "up_RetrieveEstimationEquipPO"
        Private m_RetrieveListStatement As String = "up_RetrieveEstimationEquipPOList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEstimationEquipPO"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim estimationEquipPO As EstimationEquipPO = Nothing
            While dr.Read

                estimationEquipPO = Me.CreateObject(dr)

            End While

            Return estimationEquipPO

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim estimationEquipPOList As ArrayList = New ArrayList

            While dr.Read
                Dim estimationEquipPO As EstimationEquipPO = Me.CreateObject(dr)
                estimationEquipPOList.Add(estimationEquipPO)
            End While

            Return estimationEquipPOList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim estimationEquipPO As EstimationEquipPO = CType(obj, EstimationEquipPO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, estimationEquipPO.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim estimationEquipPO As EstimationEquipPO = CType(obj, EstimationEquipPO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@Note", DbType.AnsiString, estimationEquipPO.Note)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, estimationEquipPO.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, estimationEquipPO.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, estimationEquipPO.LastUpdatedTime)

            DBCommandWrapper.AddInParameter("@EstimationEquipDetailID", DbType.Int32, Me.GetRefObject(estimationEquipPO.EstimationEquipDetail))
            DBCommandWrapper.AddInParameter("@IndentPartDetailID", DbType.Int32, Me.GetRefObject(estimationEquipPO.IndentPartDetail))

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

            Dim estimationEquipPO As EstimationEquipPO = CType(obj, EstimationEquipPO)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, estimationEquipPO.ID)
            DBCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, estimationEquipPO.Note)
            DBCommandWrapper.AddInParameter("@Note", DbType.AnsiString, estimationEquipPO.Note)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, estimationEquipPO.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, estimationEquipPO.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, estimationEquipPO.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@EstimationEquipDetailID", DbType.Int32, Me.GetRefObject(estimationEquipPO.EstimationEquipDetail))
            DBCommandWrapper.AddInParameter("@IndentPartDetailID", DbType.Int32, Me.GetRefObject(estimationEquipPO.IndentPartDetail))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EstimationEquipPO

            Dim estimationEquipPO As EstimationEquipPO = New EstimationEquipPO

            estimationEquipPO.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Note")) Then estimationEquipPO.Note = dr("Note").ToString()
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then estimationEquipPO.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then estimationEquipPO.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then estimationEquipPO.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then estimationEquipPO.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then estimationEquipPO.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EstimationEquipDetailID")) Then
                estimationEquipPO.EstimationEquipDetail = New EstimationEquipDetail(CType(dr("EstimationEquipDetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("IndentPartDetailID")) Then
                estimationEquipPO.IndentPartDetail = New IndentPartDetail(CType(dr("IndentPartDetailID"), Integer))
            End If

            Return estimationEquipPO

        End Function

        Private Sub SetTableName()

            If Not (GetType(EstimationEquipPO) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EstimationEquipPO), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EstimationEquipPO).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

