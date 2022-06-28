
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SubCategoryVehicle Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 22/01/2019 - 10:08:18
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

    Public Class SubCategoryVehicleMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSubCategoryVehicle"
        Private m_UpdateStatement As String = "up_UpdateSubCategoryVehicle"
        Private m_RetrieveStatement As String = "up_RetrieveSubCategoryVehicle"
        Private m_RetrieveListStatement As String = "up_RetrieveSubCategoryVehicleList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSubCategoryVehicle"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim subCategoryVehicle As SubCategoryVehicle = Nothing
            While dr.Read

                subCategoryVehicle = Me.CreateObject(dr)

            End While

            Return subCategoryVehicle

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim subCategoryVehicleList As ArrayList = New ArrayList

            While dr.Read
                Dim subCategoryVehicle As SubCategoryVehicle = Me.CreateObject(dr)
                subCategoryVehicleList.Add(subCategoryVehicle)
            End While

            Return subCategoryVehicleList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim subCategoryVehicle As SubCategoryVehicle = CType(obj, SubCategoryVehicle)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, subCategoryVehicle.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim subCategoryVehicle As SubCategoryVehicle = CType(obj, SubCategoryVehicle)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, subCategoryVehicle.Name)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, subCategoryVehicle.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, subCategoryVehicle.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, subCategoryVehicle.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, subCategoryVehicle.CategoryID)

            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(subCategoryVehicle.Category))

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
            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim subCategoryVehicle As SubCategoryVehicle = CType(obj, SubCategoryVehicle)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, subCategoryVehicle.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, subCategoryVehicle.Name)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiStringFixedLength, subCategoryVehicle.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, subCategoryVehicle.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, subCategoryVehicle.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            'DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, subCategoryVehicle.CategoryID)

            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(subCategoryVehicle.Category))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SubCategoryVehicle

            Dim subCategoryVehicle As SubCategoryVehicle = New SubCategoryVehicle

            subCategoryVehicle.ID = CType(dr("ID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then subCategoryVehicle.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then subCategoryVehicle.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then subCategoryVehicle.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then subCategoryVehicle.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then subCategoryVehicle.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then subCategoryVehicle.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then subCategoryVehicle.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
            '    subCategoryVehicle.CategoryID = CType(dr("CategoryID"), Byte)
            'End If
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                subCategoryVehicle.Category = New Category(CType(dr("CategoryID"), Integer))
            End If

            Return subCategoryVehicle

        End Function

        Private Sub SetTableName()

            If Not (GetType(SubCategoryVehicle) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SubCategoryVehicle), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SubCategoryVehicle).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

