#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PQRProfileBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/23/2007 - 10:48:44 AM
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

    Public Class PQRProfileBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPQRProfileBB"
        Private m_UpdateStatement As String = "up_UpdatePQRProfileBB"
        Private m_RetrieveStatement As String = "up_RetrievePQRProfileBB"
        Private m_RetrieveListStatement As String = "up_RetrievePQRProfileBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePQRProfileBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim PQRProfileBB As PQRProfileBB = Nothing
            While dr.Read

                PQRProfileBB = Me.CreateObject(dr)

            End While

            Return PQRProfileBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim PQRProfileBBList As ArrayList = New ArrayList

            While dr.Read
                Dim PQRProfileBB As PQRProfileBB = Me.CreateObject(dr)
                PQRProfileBBList.Add(PQRProfileBB)
            End While

            Return PQRProfileBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRProfileBB As PQRProfileBB = CType(obj, PQRProfileBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRProfileBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim PQRProfileBB As PQRProfileBB = CType(obj, PQRProfileBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ProfileValue", DbType.AnsiString, PQRProfileBB.ProfileValue)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, PQRProfileBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, PQRProfileBB.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PQRHeaderBBID", DbType.Int32, Me.GetRefObject(PQRProfileBB.PQRHeaderBB))
            DbCommandWrapper.AddInParameter("@ProfileHeaderID", DbType.Int32, Me.GetRefObject(PQRProfileBB.ProfileHeader))
            DbCommandWrapper.AddInParameter("@GroupID", DbType.Int32, Me.GetRefObject(PQRProfileBB.ProfileGroup))

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

            Dim PQRProfileBB As PQRProfileBB = CType(obj, PQRProfileBB)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, PQRProfileBB.ID)
            DbCommandWrapper.AddInParameter("@ProfileValue", DbType.AnsiString, PQRProfileBB.ProfileValue)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, PQRProfileBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, PQRProfileBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@PQRHeaderBBID", DbType.Int32, Me.GetRefObject(PQRProfileBB.PQRHeaderBB))
            DbCommandWrapper.AddInParameter("@ProfileHeaderID", DbType.Int32, Me.GetRefObject(PQRProfileBB.ProfileHeader))
            DbCommandWrapper.AddInParameter("@GroupID", DbType.Int32, Me.GetRefObject(PQRProfileBB.ProfileGroup))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PQRProfileBB

            Dim PQRProfileBB As PQRProfileBB = New PQRProfileBB

            PQRProfileBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileValue")) Then PQRProfileBB.ProfileValue = dr("ProfileValue").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then PQRProfileBB.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then PQRProfileBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then PQRProfileBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then PQRProfileBB.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then PQRProfileBB.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PQRHeaderBBID")) Then
                PQRProfileBB.PQRHeaderBB = New PQRHeaderBB(CType(dr("PQRHeaderBBID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileHeaderID")) Then
                PQRProfileBB.ProfileHeader = New ProfileHeader(CType(dr("ProfileHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("GroupID")) Then
                PQRProfileBB.ProfileGroup = New ProfileGroup(CType(dr("GroupID"), Integer))
            End If

            Return PQRProfileBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(PQRProfileBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PQRProfileBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PQRProfileBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

