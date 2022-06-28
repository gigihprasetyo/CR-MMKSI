#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PartIncidentalUser Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 12/6/2005 - 11:05:41 AM
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

    Public Class PartIncidentalUserMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPartIncidentalUser"
        Private m_UpdateStatement As String = "up_UpdatePartIncidentalUser"
        Private m_RetrieveStatement As String = "up_RetrievePartIncidentalUser"
        Private m_RetrieveListStatement As String = "up_RetrievePartIncidentalUserList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePartIncidentalUser"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim partIncidentalUser As PartIncidentalUser = Nothing
            While dr.Read

                partIncidentalUser = Me.CreateObject(dr)

            End While

            Return partIncidentalUser

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim partIncidentalUserList As ArrayList = New ArrayList

            While dr.Read
                Dim partIncidentalUser As PartIncidentalUser = Me.CreateObject(dr)
                partIncidentalUserList.Add(partIncidentalUser)
            End While

            Return partIncidentalUserList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partIncidentalUser As PartIncidentalUser = CType(obj, PartIncidentalUser)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@id", DbType.Int32, partIncidentalUser.id)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partIncidentalUser As PartIncidentalUser = CType(obj, PartIncidentalUser)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@id", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, partIncidentalUser.UserName)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, partIncidentalUser.Email)
            DBCommandWrapper.AddInParameter("@Tipe", DbType.AnsiString, partIncidentalUser.Tipe)
            DBCommandWrapper.AddInParameter("@PositionCC", DbType.AnsiString, partIncidentalUser.PositionCC)            
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, partIncidentalUser.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, partIncidentalUser.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@id"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "id")

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
            DbCommandWrapper.AddInParameter("@id", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partIncidentalUser As PartIncidentalUser = CType(obj, PartIncidentalUser)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@id", DbType.Int32, partIncidentalUser.id)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, partIncidentalUser.UserName)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, partIncidentalUser.Email)
            DBCommandWrapper.AddInParameter("@Tipe", DbType.AnsiString, partIncidentalUser.Tipe)
            DBCommandWrapper.AddInParameter("@PositionCC", DbType.AnsiString, partIncidentalUser.PositionCC)            
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, partIncidentalUser.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, partIncidentalUser.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            Return DBCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PartIncidentalUser

            Dim partIncidentalUser As partIncidentalUser = New partIncidentalUser

            partIncidentalUser.id = CType(dr("id"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UserName")) Then partIncidentalUser.UserName = dr("UserName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then partIncidentalUser.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Tipe")) Then partIncidentalUser.Tipe = dr("Tipe").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PositionCC")) Then partIncidentalUser.PositionCC = dr("PositionCC").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then partIncidentalUser.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then partIncidentalUser.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then partIncidentalUser.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then partIncidentalUser.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then partIncidentalUser.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return partIncidentalUser

        End Function

        Private Sub SetTableName()

            If Not (GetType(PartIncidentalUser) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PartIncidentalUser), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PartIncidentalUser).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace