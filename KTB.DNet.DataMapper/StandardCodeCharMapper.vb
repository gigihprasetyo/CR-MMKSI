
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : StandardCodeChar Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 09/02/2018 - 15:15:03
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

    Public Class StandardCodeCharMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertStandardCodeChar"
        Private m_UpdateStatement As String = "up_UpdateStandardCodeChar"
        Private m_RetrieveStatement As String = "up_RetrieveStandardCodeChar"
        Private m_RetrieveListStatement As String = "up_RetrieveStandardCodeCharList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteStandardCodeChar"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim standardCode As StandardCodeChar = Nothing
            While dr.Read

                standardCode = Me.CreateObject(dr)

            End While

            Return standardCode

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim standardCodeList As ArrayList = New ArrayList

            While dr.Read
                Dim standardCode As StandardCodeChar = Me.CreateObject(dr)
                standardCodeList.Add(standardCode)
            End While

            Return standardCodeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim standardCode As StandardCodeChar = CType(obj, StandardCodeChar)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, standardCode.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim standardCode As StandardCodeChar = CType(obj, StandardCodeChar)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Category", DbType.AnsiString, standardCode.Category)
            DbCommandWrapper.AddInParameter("@ValueId", DbType.AnsiString, standardCode.ValueId)
            DbCommandWrapper.AddInParameter("@ValueCode", DbType.AnsiString, standardCode.ValueCode)
            DbCommandWrapper.AddInParameter("@ValueDesc", DbType.AnsiString, standardCode.ValueDesc)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, standardCode.Sequence)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, standardCode.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, standardCode.LastUpdateBy)
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

            Dim standardCode As StandardCodeChar = CType(obj, StandardCodeChar)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, standardCode.ID)
            DbCommandWrapper.AddInParameter("@Category", DbType.AnsiString, standardCode.Category)
            DbCommandWrapper.AddInParameter("@ValueId", DbType.AnsiString, standardCode.ValueId)
            DbCommandWrapper.AddInParameter("@ValueCode", DbType.AnsiString, standardCode.ValueCode)
            DbCommandWrapper.AddInParameter("@ValueDesc", DbType.AnsiString, standardCode.ValueDesc)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int32, standardCode.Sequence)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, standardCode.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, standardCode.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As StandardCodeChar

            Dim standardCode As StandardCodeChar = New StandardCodeChar

            standardCode.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Category")) Then standardCode.Category = dr("Category").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValueId")) Then standardCode.ValueId = dr("ValueId").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValueCode")) Then standardCode.ValueCode = dr("ValueCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValueDesc")) Then standardCode.ValueDesc = dr("ValueDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then standardCode.Sequence = CType(dr("Sequence"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then standardCode.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then standardCode.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then standardCode.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then standardCode.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then standardCode.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return standardCode

        End Function

        Private Sub SetTableName()

            If Not (GetType(StandardCodeChar) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(StandardCodeChar), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(StandardCodeChar).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

