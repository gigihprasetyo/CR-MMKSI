#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotion Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/8/2007 - 12:29:45 PM
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

    Public Class MaterialPromotionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMaterialPromotion"
        Private m_UpdateStatement As String = "up_UpdateMaterialPromotion"
        Private m_RetrieveStatement As String = "up_RetrieveMaterialPromotion"
        Private m_RetrieveListStatement As String = "up_RetrieveMaterialPromotionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMaterialPromotion"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim materialPromotion As MaterialPromotion = Nothing
            While dr.Read

                materialPromotion = Me.CreateObject(dr)

            End While

            Return materialPromotion

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim materialPromotionList As ArrayList = New ArrayList

            While dr.Read
                Dim materialPromotion As MaterialPromotion = Me.CreateObject(dr)
                materialPromotionList.Add(materialPromotion)
            End While

            Return materialPromotionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotion As MaterialPromotion = CType(obj, MaterialPromotion)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotion.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotion As MaterialPromotion = CType(obj, MaterialPromotion)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@GoodNo", DbType.AnsiString, materialPromotion.GoodNo)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, materialPromotion.Name)
            DbCommandWrapper.AddInParameter("@Unit", DbType.AnsiString, materialPromotion.Unit)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, materialPromotion.Price)
            DbCommandWrapper.AddInParameter("@Stock", DbType.Int32, materialPromotion.Stock)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, materialPromotion.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotion.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, materialPromotion.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(materialPromotion.ProductCategory))


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

            Dim materialPromotion As MaterialPromotion = CType(obj, MaterialPromotion)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotion.ID)
            DbCommandWrapper.AddInParameter("@GoodNo", DbType.AnsiString, materialPromotion.GoodNo)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, materialPromotion.Name)
            DbCommandWrapper.AddInParameter("@Unit", DbType.AnsiString, materialPromotion.Unit)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, materialPromotion.Price)
            DbCommandWrapper.AddInParameter("@Stock", DbType.Int32, materialPromotion.Stock)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, materialPromotion.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotion.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, materialPromotion.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(materialPromotion.ProductCategory))



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MaterialPromotion

            Dim materialPromotion As MaterialPromotion = New MaterialPromotion

            materialPromotion.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("GoodNo")) Then materialPromotion.GoodNo = dr("GoodNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then materialPromotion.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Unit")) Then materialPromotion.Unit = dr("Unit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Price")) Then materialPromotion.Price = CType(dr("Price"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Stock")) Then materialPromotion.Stock = CType(dr("Stock"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then materialPromotion.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then materialPromotion.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then materialPromotion.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then materialPromotion.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then materialPromotion.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then materialPromotion.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                materialPromotion.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Short))
            End If

            Return materialPromotion

        End Function

        Private Sub SetTableName()

            If Not (GetType(MaterialPromotion) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MaterialPromotion), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MaterialPromotion).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

