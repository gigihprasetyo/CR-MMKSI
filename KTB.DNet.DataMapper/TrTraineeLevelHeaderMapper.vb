#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrTraineeLevelHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 9/10/2019 - 4:22:25 PM
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

    Public Class TrTraineeLevelHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrTraineeLevelHeader"
        Private m_UpdateStatement As String = "up_UpdateTrTraineeLevelHeader"
        Private m_RetrieveStatement As String = "up_RetrieveTrTraineeLevelHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveTrTraineeLevelHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrTraineeLevelHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trTraineeLevelHeader As TrTraineeLevelHeader = Nothing
            While dr.Read

                trTraineeLevelHeader = Me.CreateObject(dr)

            End While

            Return trTraineeLevelHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trTraineeLevelHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim trTraineeLevelHeader As TrTraineeLevelHeader = Me.CreateObject(dr)
                trTraineeLevelHeaderList.Add(trTraineeLevelHeader)
            End While

            Return trTraineeLevelHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTraineeLevelHeader As TrTraineeLevelHeader = CType(obj, TrTraineeLevelHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTraineeLevelHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTraineeLevelHeader As TrTraineeLevelHeader = CType(obj, TrTraineeLevelHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TrTraineeID", DbType.Int32, Me.GetRefObject(trTraineeLevelHeader.TrTrainee))
            DbCommandWrapper.AddInParameter("@TrTraineeLevelID", DbType.Int32, Me.GetRefObject(trTraineeLevelHeader.TrTraineeLevel))
            DbCommandWrapper.AddInParameter("@TrCourseCategoryID", DbType.Int32, Me.GetRefObject(trTraineeLevelHeader.TrCourseCategory))
            DbCommandWrapper.AddInParameter("@TanggalLulus", DbType.DateTime, trTraineeLevelHeader.TanggalLulus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTraineeLevelHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trTraineeLevelHeader.Status)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trTraineeLevelHeader.LastUpdateBy)
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

            Dim trTraineeLevelHeader As TrTraineeLevelHeader = CType(obj, TrTraineeLevelHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTraineeLevelHeader.ID)
            DbCommandWrapper.AddInParameter("@TrTraineeID", DbType.Int32, Me.GetRefObject(trTraineeLevelHeader.TrTrainee))
            DbCommandWrapper.AddInParameter("@TrCourseCategoryID", DbType.Int32, Me.GetRefObject(trTraineeLevelHeader.TrCourseCategory))
            DbCommandWrapper.AddInParameter("@TrTraineeLevelID", DbType.Int32, Me.GetRefObject(trTraineeLevelHeader.TrTraineeLevel))
            DbCommandWrapper.AddInParameter("@TanggalLulus", DbType.DateTime, trTraineeLevelHeader.TanggalLulus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTraineeLevelHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trTraineeLevelHeader.Status)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trTraineeLevelHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrTraineeLevelHeader

            Dim trTraineeLevelHeader As TrTraineeLevelHeader = New TrTraineeLevelHeader

            trTraineeLevelHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TrTraineeID")) Then trTraineeLevelHeader.TrTrainee = New TrTrainee(CType(dr("TrTraineeID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("TrTraineeLevelID")) Then trTraineeLevelHeader.TrTraineeLevel = New TrTraineeLevel(CType(dr("TrTraineeLevelID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("TrCourseCategoryID")) Then trTraineeLevelHeader.TrCourseCategory = New TrCourseCategory(CType(dr("TrCourseCategoryID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("TanggalLulus")) Then trTraineeLevelHeader.TanggalLulus = CType(dr("TanggalLulus"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trTraineeLevelHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trTraineeLevelHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trTraineeLevelHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trTraineeLevelHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trTraineeLevelHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trTraineeLevelHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trTraineeLevelHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrTraineeLevelHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrTraineeLevelHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrTraineeLevelHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
