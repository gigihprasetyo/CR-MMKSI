#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EquipUser Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 9/3/2009 - 11:59:56
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

    Public Class EquipUserMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEquipUser"
        Private m_UpdateStatement As String = "up_UpdateEquipUser"
        Private m_RetrieveStatement As String = "up_RetrieveEquipUser"
        Private m_RetrieveListStatement As String = "up_RetrieveEquipUserList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEquipUser"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim equipUser As EquipUser = Nothing
            While dr.Read

                equipUser = Me.CreateObject(dr)

            End While

            Return equipUser

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim equipUserList As ArrayList = New ArrayList

            While dr.Read
                Dim equipUser As EquipUser = Me.CreateObject(dr)
                equipUserList.Add(equipUser)
            End While

            Return equipUserList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim equipUser As EquipUser = CType(obj, EquipUser)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@id", DbType.Int32, equipUser.id)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim equipUser As EquipUser = CType(obj, EquipUser)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@id", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, equipUser.UserName)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, equipUser.Email)
            DbCommandWrapper.AddInParameter("@Tipe", DbType.AnsiString, equipUser.Tipe)
            DbCommandWrapper.AddInParameter("@PositionCC", DbType.AnsiString, equipUser.PositionCC)
            DbCommandWrapper.AddInParameter("@GroupType", DbType.Int16, equipUser.GroupType)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, equipUser.Note)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, equipUser.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, equipUser.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            Return DbCommandWrapper

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

            Dim equipUser As EquipUser = CType(obj, EquipUser)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@id", DbType.Int32, equipUser.id)
            DbCommandWrapper.AddInParameter("@UserName", DbType.AnsiString, equipUser.UserName)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, equipUser.Email)
            DbCommandWrapper.AddInParameter("@Tipe", DbType.AnsiString, equipUser.Tipe)
            DbCommandWrapper.AddInParameter("@PositionCC", DbType.AnsiString, equipUser.PositionCC)
            DbCommandWrapper.AddInParameter("@GroupType", DbType.Int16, equipUser.GroupType)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, equipUser.Note)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, equipUser.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, equipUser.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EquipUser

            Dim equipUser As EquipUser = New EquipUser

            equipUser.id = CType(dr("id"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UserName")) Then equipUser.UserName = dr("UserName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then equipUser.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Tipe")) Then equipUser.Tipe = dr("Tipe").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PositionCC")) Then equipUser.PositionCC = dr("PositionCC").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GroupType")) Then equipUser.GroupType = CType(dr("GroupType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Note")) Then equipUser.Note = dr("Note").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then equipUser.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then equipUser.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then equipUser.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then equipUser.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then equipUser.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return equipUser

        End Function

        Private Sub SetTableName()

            If Not (GetType(EquipUser) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EquipUser), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EquipUser).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

