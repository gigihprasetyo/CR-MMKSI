#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ProfileHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/18/2007 - 8:54:00 AM
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

    Public Class ProfileHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertProfileHeader"
        Private m_UpdateStatement As String = "up_UpdateProfileHeader"
        Private m_RetrieveStatement As String = "up_RetrieveProfileHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveProfileHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteProfileHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim profileHeader As profileHeader = Nothing
            While dr.Read

                profileHeader = Me.CreateObject(dr)

            End While

            Return profileHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim profileHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim profileHeader As profileHeader = Me.CreateObject(dr)
                profileHeaderList.Add(profileHeader)
            End While

            Return profileHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim profileHeader As profileHeader = CType(obj, profileHeader)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, profileHeader.ID)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim profileHeader As profileHeader = CType(obj, profileHeader)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@Code", DbType.AnsiString, profileHeader.Code)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, profileHeader.Description)
            DBCommandWrapper.AddInParameter("@DataType", DbType.Int16, profileHeader.DataType)
            DBCommandWrapper.AddInParameter("@DataLength", DbType.Int32, profileHeader.DataLength)
            DBCommandWrapper.AddInParameter("@ControlType", DbType.Int16, profileHeader.ControlType)
            DBCommandWrapper.AddInParameter("@SelectionMode", DbType.Int16, profileHeader.SelectionMode)
            DBCommandWrapper.AddInParameter("@Mandatory", DbType.Int16, profileHeader.Mandatory)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, profileHeader.Status)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, profileHeader.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, profileHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim profileHeader As profileHeader = CType(obj, profileHeader)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, profileHeader.ID)
            DBCommandWrapper.AddInParameter("@Code", DbType.AnsiString, profileHeader.Code)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, profileHeader.Description)
            DBCommandWrapper.AddInParameter("@DataType", DbType.Int16, profileHeader.DataType)
            DBCommandWrapper.AddInParameter("@DataLength", DbType.Int32, profileHeader.DataLength)
            DBCommandWrapper.AddInParameter("@ControlType", DbType.Int16, profileHeader.ControlType)
            DBCommandWrapper.AddInParameter("@SelectionMode", DbType.Int16, profileHeader.SelectionMode)
            DBCommandWrapper.AddInParameter("@Mandatory", DbType.Int16, profileHeader.Mandatory)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, profileHeader.Status)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, profileHeader.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, profileHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ProfileHeader

            Dim profileHeader As profileHeader = New profileHeader

            profileHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then profileHeader.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then profileHeader.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DataType")) Then profileHeader.DataType = CType(dr("DataType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DataLength")) Then profileHeader.DataLength = CType(dr("DataLength"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ControlType")) Then profileHeader.ControlType = CType(dr("ControlType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("SelectionMode")) Then profileHeader.SelectionMode = CType(dr("SelectionMode"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Mandatory")) Then profileHeader.Mandatory = CType(dr("Mandatory"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then profileHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then profileHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then profileHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then profileHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then profileHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then profileHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return profileHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(ProfileHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ProfileHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ProfileHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

