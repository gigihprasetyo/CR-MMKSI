
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : StandardCode Objects Mapper.
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

    Public Class StandardCodeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertStandardCode"
        Private m_UpdateStatement As String = "up_UpdateStandardCode"
        Private m_RetrieveStatement As String = "up_RetrieveStandardCode"
        Private m_RetrieveListStatement As String = "up_RetrieveStandardCodeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteStandardCode"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim standardCode As StandardCode = Nothing
            While dr.Read

                standardCode = Me.CreateObject(dr)

            End While

            Return standardCode

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim standardCodeList As ArrayList = New ArrayList

            While dr.Read
                Dim standardCode As StandardCode = Me.CreateObject(dr)
                standardCodeList.Add(standardCode)
            End While

            Return standardCodeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim standardCode As StandardCode = CType(obj, StandardCode)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, standardCode.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim standardCode As StandardCode = CType(obj, StandardCode)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Category", DbType.AnsiString, standardCode.Category)
            DbCommandWrapper.AddInParameter("@ValueId", DbType.Int32, standardCode.ValueId)
            DbCommandWrapper.AddInParameter("@ValueDesc", DbType.AnsiString, standardCode.ValueDesc)
            DbCommandWrapper.AddInParameter("@ValueCode", DbType.AnsiString, standardCode.ValueCode)
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

            Dim standardCode As StandardCode = CType(obj, StandardCode)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, standardCode.ID)
            DbCommandWrapper.AddInParameter("@Category", DbType.AnsiString, standardCode.Category)
            DbCommandWrapper.AddInParameter("@ValueId", DbType.Int32, standardCode.ValueId)
            DbCommandWrapper.AddInParameter("@ValueDesc", DbType.AnsiString, standardCode.ValueDesc)
            DbCommandWrapper.AddInParameter("@ValueCode", DbType.AnsiString, standardCode.ValueCode)
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

        Private Function CreateObject(ByVal dr As IDataReader) As StandardCode

            Dim standardCode As StandardCode = New StandardCode

            standardCode.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Category")) Then standardCode.Category = dr("Category").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValueId")) Then standardCode.ValueId = CType(dr("ValueId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValueDesc")) Then standardCode.ValueDesc = dr("ValueDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValueCode")) Then standardCode.ValueCode = dr("ValueCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then standardCode.Sequence = CType(dr("Sequence"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then standardCode.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then standardCode.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then standardCode.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then standardCode.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then standardCode.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return standardCode

        End Function

        Private Sub SetTableName()

            If Not (GetType(StandardCode) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(StandardCode), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(StandardCode).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

