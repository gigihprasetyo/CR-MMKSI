#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : Product Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/22/2005 - 6:51:18 AM
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

    Public Class ProductMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertProduct"
        Private m_UpdateStatement As String = "up_UpdateProduct"
        Private m_RetrieveStatement As String = "up_RetrieveProduct"
        Private m_RetrieveListStatement As String = "up_RetrieveProductList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteProduct"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim product As product = Nothing
            While dr.Read

                product = Me.CreateObject(dr)

            End While

            Return product

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim productList As ArrayList = New ArrayList

            While dr.Read
                Dim product As product = Me.CreateObject(dr)
                productList.Add(product)
            End While

            Return productList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim product As product = CType(obj, product)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, product.ID)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim product As product = CType(obj, product)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddInParameter("@ProductCode", DbType.AnsiString, Product.ProductCode)
            DBCommandWrapper.AddInParameter("@ProductName", DbType.AnsiString, Product.ProductName)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, Product.Description)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, Product.LastUpdatedBy)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, Product.RowStatus)
            DBCommandWrapper.AddInParameter("@LockedBy", DbType.AnsiString, Product.LockedBy)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, IIf(product.ProductDetail Is Nothing, CType(DBNull.Value, Object), CType(product, Object)).ProductDetail.ID)

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

            Dim product As product = CType(obj, product)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ProductCode", DbType.AnsiString, product.ProductCode)
            DBCommandWrapper.AddInParameter("@ProductName", DbType.AnsiString, product.ProductName)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, product.Description)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, product.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, product.LastUpdatedBy)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, product.RowStatus)
            DBCommandWrapper.AddInParameter("@LockedBy", DbType.AnsiString, product.LockedBy)


            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, IIf(product.ProductDetail Is Nothing, CType(DBNull.Value, Object), CType(product.ProductDetail, Object)))

            Return DBCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Product

            Dim product As product = New product


            product.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCode")) Then product.ProductCode = dr("ProductCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("ProductName")) Then product.ProductName = dr("ProductName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then product.Description = dr("Description").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then product.CreatedTime = CType(dr("CreatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then product.CreatedBy = dr("CreatedBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then product.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then product.LastUpdatedBy = dr("LastUpdatedBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then product.RowStatus = CType(dr("RowStatus"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("LockedBy")) Then product.LockedBy = dr("LockedBy").ToString



            Return product

        End Function

        Private Sub SetTableName()

            If Not (GetType(Product) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Product), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Product).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace