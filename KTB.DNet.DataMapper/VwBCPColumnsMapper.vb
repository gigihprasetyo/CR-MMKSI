
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VwBCPColumns Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 12/13/2012 - 3:19:05 PM
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

    Public Class VwBCPColumnsMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVwBCPColumns"
        Private m_UpdateStatement As String = "up_UpdateVwBCPColumns"
        Private m_RetrieveStatement As String = "up_RetrieveVwBCPColumns"
        Private m_RetrieveListStatement As String = "up_RetrieveVwBCPColumnsList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVwBCPColumns"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vwBCPColumns As VwBCPColumns = Nothing
            While dr.Read

                vwBCPColumns = Me.CreateObject(dr)

            End While

            Return vwBCPColumns

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vwBCPColumnsList As ArrayList = New ArrayList

            While dr.Read
                Dim vwBCPColumns As VwBCPColumns = Me.CreateObject(dr)
                vwBCPColumnsList.Add(vwBCPColumns)
            End While

            Return vwBCPColumnsList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vwBCPColumns As VwBCPColumns = CType(obj, VwBCPColumns)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@object_id", DbType.Int32, vwBCPColumns.object_id)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vwBCPColumns As VwBCPColumns = CType(obj, VwBCPColumns)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@object_id", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ViewName", DbType.String, vwBCPColumns.ViewName)
            DbCommandWrapper.AddInParameter("@ColumnID", DbType.Int32, vwBCPColumns.ColumnID)
            DbCommandWrapper.AddInParameter("@ColoumnName", DbType.String, vwBCPColumns.ColoumnName)
            DbCommandWrapper.AddInParameter("@ColumnType", DbType.Int32, vwBCPColumns.ColumnType)
            DbCommandWrapper.AddInParameter("@Maxlength", DbType.Int16, vwBCPColumns.Maxlength)


            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@object_id"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "object_id")

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
            DbCommandWrapper.AddInParameter("@object_id", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vwBCPColumns As VwBCPColumns = CType(obj, VwBCPColumns)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@object_id", DbType.Int32, vwBCPColumns.object_id)
            DbCommandWrapper.AddInParameter("@ViewName", DbType.String, vwBCPColumns.ViewName)
            DbCommandWrapper.AddInParameter("@ColumnID", DbType.Int32, vwBCPColumns.ColumnID)
            DbCommandWrapper.AddInParameter("@ColoumnName", DbType.String, vwBCPColumns.ColoumnName)
            DbCommandWrapper.AddInParameter("@ColumnType", DbType.Int32, vwBCPColumns.ColumnType)
            DbCommandWrapper.AddInParameter("@Maxlength", DbType.Int16, vwBCPColumns.Maxlength)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VwBCPColumns

            Dim vwBCPColumns As VwBCPColumns = New VwBCPColumns

            vwBCPColumns.object_id = CType(dr("object_id"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ViewName")) Then vwBCPColumns.ViewName = dr("ViewName").ToString
            vwBCPColumns.ColumnID = CType(dr("ColumnID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ColoumnName")) Then vwBCPColumns.ColoumnName = dr("ColoumnName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColumnType")) Then vwBCPColumns.ColumnType = CType(dr("ColumnType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Maxlength")) Then vwBCPColumns.Maxlength = CType(dr("Maxlength"), Short)

            Return vwBCPColumns

        End Function

        Private Sub SetTableName()

            If Not (GetType(VwBCPColumns) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VwBCPColumns), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VwBCPColumns).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

