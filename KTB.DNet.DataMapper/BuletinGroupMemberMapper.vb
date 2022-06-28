
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BuletinGroupMember Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 16/09/2016 - 9:45:31
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

    Public Class BuletinGroupMemberMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBuletinGroupMember"
        Private m_UpdateStatement As String = "up_UpdateBuletinGroupMember"
        Private m_RetrieveStatement As String = "up_RetrieveBuletinGroupMember"
        Private m_RetrieveListStatement As String = "up_RetrieveBuletinGroupMemberList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBuletinGroupMember"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim buletinGroupMember As BuletinGroupMember = Nothing
            While dr.Read

                buletinGroupMember = Me.CreateObject(dr)

            End While

            Return buletinGroupMember

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim buletinGroupMemberList As ArrayList = New ArrayList

            While dr.Read
                Dim buletinGroupMember As BuletinGroupMember = Me.CreateObject(dr)
                buletinGroupMemberList.Add(buletinGroupMember)
            End While

            Return buletinGroupMemberList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim buletinGroupMember As BuletinGroupMember = CType(obj, BuletinGroupMember)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, buletinGroupMember.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim buletinGroupMember As BuletinGroupMember = CType(obj, BuletinGroupMember)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ReadStatus", DbType.Byte, buletinGroupMember.ReadStatus)
            DbCommandWrapper.AddInParameter("@ReadTime", DbType.DateTime, buletinGroupMember.ReadTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, buletinGroupMember.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, buletinGroupMember.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BuletinID", DbType.Int32, Me.GetRefObject(buletinGroupMember.Buletin))
            DbCommandWrapper.AddInParameter("@UserGroupID", DbType.Int16, Me.GetRefObject(buletinGroupMember.UserGroup))

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

            Dim buletinGroupMember As BuletinGroupMember = CType(obj, BuletinGroupMember)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, buletinGroupMember.ID)
            DbCommandWrapper.AddInParameter("@ReadStatus", DbType.Byte, buletinGroupMember.ReadStatus)
            DbCommandWrapper.AddInParameter("@ReadTime", DbType.DateTime, buletinGroupMember.ReadTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, buletinGroupMember.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, buletinGroupMember.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BuletinID", DbType.Int32, Me.GetRefObject(buletinGroupMember.Buletin))
            DbCommandWrapper.AddInParameter("@UserGroupID", DbType.Int16, Me.GetRefObject(buletinGroupMember.UserGroup))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BuletinGroupMember

            Dim buletinGroupMember As BuletinGroupMember = New BuletinGroupMember

            buletinGroupMember.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReadStatus")) Then buletinGroupMember.ReadStatus = CType(dr("ReadStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("ReadTime")) Then buletinGroupMember.ReadTime = CType(dr("ReadTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then buletinGroupMember.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then buletinGroupMember.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then buletinGroupMember.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then buletinGroupMember.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then buletinGroupMember.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BuletinID")) Then
                buletinGroupMember.Buletin = New Buletin(CType(dr("BuletinID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("UserGroupID")) Then
                buletinGroupMember.UserGroup = New UserGroup(CType(dr("UserGroupID"), Short))
            End If

            Return buletinGroupMember

        End Function

        Private Sub SetTableName()

            If Not (GetType(BuletinGroupMember) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BuletinGroupMember), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BuletinGroupMember).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

