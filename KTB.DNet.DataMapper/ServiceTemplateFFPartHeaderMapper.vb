#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceTemplateFFPartHeader Objects Mapper.
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

    Public Class ServiceTemplateFFPartHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceTemplateFFPartHeader"
        Private m_UpdateStatement As String = "up_UpdateServiceTemplateFFPartHeader"
        Private m_RetrieveStatement As String = "up_RetrieveServiceTemplateFFPartHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceTemplateFFPartHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceTemplateFFPartHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ServiceTemplateFFPartHeader As ServiceTemplateFFPartHeader = Nothing
            While dr.Read

                ServiceTemplateFFPartHeader = Me.CreateObject(dr)

            End While

            Return ServiceTemplateFFPartHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ServiceTemplateFFPartHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim ServiceTemplateFFPartHeader As ServiceTemplateFFPartHeader = Me.CreateObject(dr)
                ServiceTemplateFFPartHeaderList.Add(ServiceTemplateFFPartHeader)
            End While

            Return ServiceTemplateFFPartHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceTemplateFFPartHeader As ServiceTemplateFFPartHeader = CType(obj, ServiceTemplateFFPartHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceTemplateFFPartHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceTemplateFFPartHeader As ServiceTemplateFFPartHeader = CType(obj, ServiceTemplateFFPartHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Varian", DbType.AnsiString, ServiceTemplateFFPartHeader.Varian)
            'DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, ServiceTemplateFFPartHeader.ValidFrom)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceTemplateFFPartHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, ServiceTemplateFFPartHeader.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@RecallCategoryID", DbType.Int32, Me.GetRefObject(ServiceTemplateFFPartHeader.RecallCategory))
            'DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(ServiceTemplateFFPartHeader.VechileType))

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

            Dim ServiceTemplateFFPartHeader As ServiceTemplateFFPartHeader = CType(obj, ServiceTemplateFFPartHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceTemplateFFPartHeader.ID)
            DbCommandWrapper.AddInParameter("@Varian", DbType.AnsiString, ServiceTemplateFFPartHeader.Varian)
            'DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, ServiceTemplateFFPartHeader.ValidFrom)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceTemplateFFPartHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ServiceTemplateFFPartHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@RecallCategoryID", DbType.Int32, Me.GetRefObject(ServiceTemplateFFPartHeader.RecallCategory))
            'DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(ServiceTemplateFFPartHeader.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceTemplateFFPartHeader

            Dim ServiceTemplateFFPartHeader As ServiceTemplateFFPartHeader = New ServiceTemplateFFPartHeader

            ServiceTemplateFFPartHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Varian")) Then ServiceTemplateFFPartHeader.Varian = dr("Varian").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then ServiceTemplateFFPartHeader.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ServiceTemplateFFPartHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ServiceTemplateFFPartHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ServiceTemplateFFPartHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then ServiceTemplateFFPartHeader.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then ServiceTemplateFFPartHeader.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RecallCategoryID")) Then
                ServiceTemplateFFPartHeader.RecallCategory = New RecallCategory(CType(dr("RecallCategoryID"), Integer))
            End If
            'If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
            '    ServiceTemplateFFPartHeader.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            'End If

            Return ServiceTemplateFFPartHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceTemplateFFPartHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceTemplateFFPartHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceTemplateFFPartHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

