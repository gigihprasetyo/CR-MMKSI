#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : Category Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/22/2005 - 5:16:31 AM
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

    Public Class CategoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCategory"
        Private m_UpdateStatement As String = "up_UpdateCategory"
        Private m_RetrieveStatement As String = "up_RetrieveCategory"
        Private m_RetrieveListStatement As String = "up_RetrieveCategoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCategory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim category As category = Nothing
            While dr.Read

                category = Me.CreateObject(dr)

            End While

            Return category

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim categoryList As ArrayList = New ArrayList

            While dr.Read
                Dim category As category = Me.CreateObject(dr)
                categoryList.Add(category)
            End While

            Return categoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim category As category = CType(obj, category)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, category.ID)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim category As category = CType(obj, category)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, category.CategoryCode)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, category.Description)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, category.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, category.LastUpdateBy)
            'DBCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, category.LastUpdateTime)

            DBCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(category.ProductCategory))


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

            Dim category As category = CType(obj, category)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, category.ID)
            DBCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, category.CategoryCode)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, category.Description)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, category.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, category.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DBCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, category.LastUpdateTime)


            DBCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(category.ProductCategory))


            Return DBCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Category

            Dim category As category = New category

            category.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("CategoryCode")) Then category.CategoryCode = dr("CategoryCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then category.Description = dr("Description").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then category.RowStatus = CType(dr("RowStatus"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then category.CreatedBy = dr("CreatedBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then category.CreatedTime = CType(dr("CreatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then category.LastUpdateBy = dr("LastUpdateBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then category.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                category.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If

            Return category

        End Function

        Private Sub SetTableName()

            If Not (GetType(Category) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Category), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Category).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace