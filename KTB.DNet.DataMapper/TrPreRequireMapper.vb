#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrPreRequire Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/11/2005 - 2:12:07 PM
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

    Public Class TrPreRequireMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrPreRequire"
        Private m_UpdateStatement As String = "up_UpdateTrPreRequire"
        Private m_RetrieveStatement As String = "up_RetrieveTrPreRequire"
        Private m_RetrieveListStatement As String = "up_RetrieveTrPreRequireList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrPreRequire"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trPreRequire As TrPreRequire = Nothing
            While dr.Read

                trPreRequire = Me.CreateObject(dr)

            End While

            Return trPreRequire

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trPreRequireList As ArrayList = New ArrayList

            While dr.Read
                Dim trPreRequire As TrPreRequire = Me.CreateObject(dr)
                trPreRequireList.Add(trPreRequire)
            End While

            Return trPreRequireList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trPreRequire As TrPreRequire = CType(obj, TrPreRequire)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trPreRequire.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trPreRequire As TrPreRequire = CType(obj, TrPreRequire)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trPreRequire.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trPreRequire.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trPreRequire.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PreRequireCourseID", DbType.Int32, trPreRequire.PreRequireCourseID)
            'DBCommandWrapper.AddInParameter("@PreRequireCourseID", DbType.Int32, Me.GetRefObject(trPreRequire.PreRequireCourse))
            DbCommandWrapper.AddInParameter("@CourseID", DbType.Int32, Me.GetRefObject(trPreRequire.TrCourse))
            DbCommandWrapper.AddInParameter("@RequireType", DbType.Int16, trPreRequire.RequireType)
            DbCommandWrapper.AddInParameter("@Prerequireduration", DbType.Int32, trPreRequire.Prerequireduration)

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

            Dim trPreRequire As TrPreRequire = CType(obj, TrPreRequire)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trPreRequire.ID)
            DbCommandWrapper.AddInParameter("@CourseID", DbType.Int32, Me.GetRefObject(trPreRequire.TrCourse))
            DbCommandWrapper.AddInParameter("@PreRequireCourseID", DbType.Int32, trPreRequire.PreRequireCourseID)
            DbCommandWrapper.AddInParameter("@Prerequreduration", DbType.Int32, trPreRequire.Prerequireduration)
            DbCommandWrapper.AddInParameter("@RequireType", DbType.Int16, trPreRequire.RequireType)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, trPreRequire.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trPreRequire.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trPreRequire.CreatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrPreRequire

            Dim trPreRequire As TrPreRequire = New TrPreRequire

            trPreRequire.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequireType")) Then trPreRequire.RequireType = CType(dr("RequireType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then trPreRequire.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trPreRequire.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trPreRequire.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trPreRequire.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trPreRequire.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trPreRequire.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PreRequireCourseID")) Then
                trPreRequire.PreRequireCourseID = CType(dr("PreRequireCourseID"), Integer)
                'trPreRequire.PreRequireCourse = New TrCourse(CType(dr("PreRequireCourseID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CourseID")) Then
                trPreRequire.TrCourse = New TrCourse(CType(dr("CourseID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("Prerequireduration")) Then
                trPreRequire.Prerequireduration = CType(dr("Prerequireduration"), Integer)
            End If

            Return trPreRequire

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrPreRequire) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrPreRequire), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrPreRequire).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

