
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BCPQuery Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 12/17/2012 - 11:35:30 AM
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

    Public Class BCPQueryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBCPQuery"
        Private m_UpdateStatement As String = "up_UpdateBCPQuery"
        Private m_RetrieveStatement As String = "up_RetrieveBCPQuery"
        Private m_RetrieveListStatement As String = "up_RetrieveBCPQueryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBCPQuery"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim bCPQuery As BCPQuery = Nothing
            While dr.Read

                bCPQuery = Me.CreateObject(dr)

            End While

            Return bCPQuery

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim bCPQueryList As ArrayList = New ArrayList

            While dr.Read
                Dim bCPQuery As BCPQuery = Me.CreateObject(dr)
                bCPQueryList.Add(bCPQuery)
            End While

            Return bCPQueryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bCPQuery As BCPQuery = CType(obj, BCPQuery)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bCPQuery.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bCPQuery As BCPQuery = CType(obj, BCPQuery)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@IsKTB", DbType.Byte, bCPQuery.IsKTB)
            DbCommandWrapper.AddInParameter("@IsDealer", DbType.Byte, bCPQuery.IsDealer)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, bCPQuery.Title)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, bCPQuery.Description)
            DbCommandWrapper.AddInParameter("@ViewName", DbType.AnsiString, bCPQuery.ViewName)
            DbCommandWrapper.AddInParameter("@SPName", DbType.AnsiString, bCPQuery.SPName)
            DbCommandWrapper.AddInParameter("@FlName", DbType.AnsiString, bCPQuery.FlName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bCPQuery.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, bCPQuery.LastUpdateBy)
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

            Dim bCPQuery As BCPQuery = CType(obj, BCPQuery)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bCPQuery.ID)
            DbCommandWrapper.AddInParameter("@IsKTB", DbType.Byte, bCPQuery.IsKTB)
            DbCommandWrapper.AddInParameter("@IsDealer", DbType.Byte, bCPQuery.IsDealer)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, bCPQuery.Title)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, bCPQuery.Description)
            DbCommandWrapper.AddInParameter("@ViewName", DbType.AnsiString, bCPQuery.ViewName)
            DbCommandWrapper.AddInParameter("@SPName", DbType.AnsiString, bCPQuery.SPName)
            DbCommandWrapper.AddInParameter("@FlName", DbType.AnsiString, bCPQuery.FlName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bCPQuery.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, bCPQuery.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BCPQuery

            Dim bCPQuery As BCPQuery = New BCPQuery

            bCPQuery.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsKTB")) Then bCPQuery.IsKTB = CType(dr("IsKTB"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsDealer")) Then bCPQuery.IsDealer = CType(dr("IsDealer"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Title")) Then bCPQuery.Title = dr("Title").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then bCPQuery.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ViewName")) Then bCPQuery.ViewName = dr("ViewName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPName")) Then bCPQuery.SPName = dr("SPName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FlName")) Then bCPQuery.FlName = dr("FlName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then bCPQuery.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then bCPQuery.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then bCPQuery.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then bCPQuery.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then bCPQuery.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return bCPQuery

        End Function

        Private Sub SetTableName()

            If Not (GetType(BCPQuery) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BCPQuery), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BCPQuery).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

