#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FlatRateMasterFS Objects Mapper.
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

    Public Class FlatRateMasterFSMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFlatRateMasterFS"
        Private m_UpdateStatement As String = "up_UpdateFlatRateMasterFS"
        Private m_RetrieveStatement As String = "up_RetrieveFlatRateMasterFS"
        Private m_RetrieveListStatement As String = "up_RetrieveFlatRateMasterFSList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFlatRateMasterFS"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim FlatRateMasterFS As FlatRateMasterFS = Nothing
            While dr.Read

                FlatRateMasterFS = Me.CreateObject(dr)

            End While

            Return FlatRateMasterFS

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim FlatRateMasterFSList As ArrayList = New ArrayList

            While dr.Read
                Dim FlatRateMasterFS As FlatRateMasterFS = Me.CreateObject(dr)
                FlatRateMasterFSList.Add(FlatRateMasterFS)
            End While

            Return FlatRateMasterFSList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim FlatRateMasterFS As FlatRateMasterFS = CType(obj, FlatRateMasterFS)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, FlatRateMasterFS.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim FlatRateMasterFS As FlatRateMasterFS = CType(obj, FlatRateMasterFS)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Varian", DbType.AnsiString, FlatRateMasterFS.Varian)
            DbCommandWrapper.AddInParameter("@FlatRate", DbType.Decimal, FlatRateMasterFS.FlatRate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, FlatRateMasterFS.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, FlatRateMasterFS.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, FlatRateMasterFS.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Int16, Me.GetRefObject(FlatRateMasterFS.FSKind))

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

            Dim FlatRateMasterFS As FlatRateMasterFS = CType(obj, FlatRateMasterFS)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, FlatRateMasterFS.ID)
            DbCommandWrapper.AddInParameter("@Varian", DbType.AnsiString, FlatRateMasterFS.Varian)
            DbCommandWrapper.AddInParameter("@FlatRate", DbType.Decimal, FlatRateMasterFS.FlatRate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, FlatRateMasterFS.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, FlatRateMasterFS.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, FlatRateMasterFS.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Int16, Me.GetRefObject(FlatRateMasterFS.FSKind))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FlatRateMasterFS

            Dim FlatRateMasterFS As FlatRateMasterFS = New FlatRateMasterFS

            FlatRateMasterFS.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Varian")) Then FlatRateMasterFS.Varian = dr("Varian").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FlatRate")) Then FlatRateMasterFS.FlatRate = CDec(dr("FlatRate").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then FlatRateMasterFS.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then FlatRateMasterFS.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then FlatRateMasterFS.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then FlatRateMasterFS.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then FlatRateMasterFS.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then FlatRateMasterFS.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FSKindID")) Then
                FlatRateMasterFS.FSKind = New FSKind(CType(dr("FSKindID"), Short))
            End If

            Return FlatRateMasterFS

        End Function

        Private Sub SetTableName()

            If Not (GetType(FlatRateMasterFS) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FlatRateMasterFS), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FlatRateMasterFS).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

