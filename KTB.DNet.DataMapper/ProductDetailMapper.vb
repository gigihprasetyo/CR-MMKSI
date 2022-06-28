#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : ProductDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/22/2005 - 6:52:22 AM
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

    Public Class ProductDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertProductDetail"
        Private m_UpdateStatement As String = "up_UpdateProductDetail"
        Private m_RetrieveStatement As String = "up_RetrieveProductDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveProductDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteProductDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim productDetail As productDetail = Nothing
            While dr.Read

                productDetail = Me.CreateObject(dr)

            End While

            Return productDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim productDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim productDetail As productDetail = Me.CreateObject(dr)
                productDetailList.Add(productDetail)
            End While

            Return productDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim productDetail As productDetail = CType(obj, productDetail)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, productDetail.ID)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim productDetail As productDetail = CType(obj, productDetail)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@ProductID", DbType.Int32, ProductDetail.ProductID)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, ProductDetail.Description)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, ProductDetail.LastUpdatedBy)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ProductDetail.RowStatus)
            DBCommandWrapper.AddInParameter("@LockedBy", DbType.AnsiString, ProductDetail.LockedBy)

            DBCommandWrapper.AddInParameter("@BasicProductID", DbType.Int32, IIf(productDetail.BasicProduct Is Nothing, CType(DBNull.Value, Object), CType(productDetail, Object)).BasicProduct.ID)

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

            Dim productDetail As productDetail = CType(obj, productDetail)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, productDetail.ID)
            DBCommandWrapper.AddInParameter("@ProductID", DbType.Int32, productDetail.ProductID)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, productDetail.Description)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, productDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, productDetail.LastUpdatedBy)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, productDetail.RowStatus)
            DBCommandWrapper.AddInParameter("@LockedBy", DbType.AnsiString, productDetail.LockedBy)


            DBCommandWrapper.AddInParameter("@BasicProductID", DbType.Int32, IIf(productDetail.BasicProduct Is Nothing, CType(DBNull.Value, Object), CType(productDetail.BasicProduct.ID, Object)))

            Return DBCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ProductDetail

            Dim productDetail As productDetail = New productDetail

            productDetail.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("ProductID")) Then productDetail.ProductID = CType(dr("ProductID"), Integer)


            productDetail.BasicProductID = CType(dr("BasicProductID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then productDetail.Description = dr("Description").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then productDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then productDetail.CreatedBy = dr("CreatedBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then productDetail.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then productDetail.LastUpdatedBy = dr("LastUpdatedBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then productDetail.RowStatus = CType(dr("RowStatus"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("LockedBy")) Then productDetail.LockedBy = dr("LockedBy").ToString


            If Not dr.IsDBNull(dr.GetOrdinal("BasicProductID")) Then
                productDetail.BasicProduct = New BasicProduct(CType(dr("BasicProductID"), Integer))
            End If

            Return productDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(ProductDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ProductDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ProductDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace