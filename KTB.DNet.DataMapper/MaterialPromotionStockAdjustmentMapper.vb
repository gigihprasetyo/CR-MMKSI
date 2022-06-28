#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionStockAdjustment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2007 - 8:43:59 AM
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

    Public Class MaterialPromotionStockAdjustmentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMaterialPromotionStockAdjustment"
        Private m_UpdateStatement As String = "up_UpdateMaterialPromotionStockAdjustment"
        Private m_RetrieveStatement As String = "up_RetrieveMaterialPromotionStockAdjustment"
        Private m_RetrieveListStatement As String = "up_RetrieveMaterialPromotionStockAdjustmentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMaterialPromotionStockAdjustment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim materialPromotionStockAdjustment As MaterialPromotionStockAdjustment = Nothing
            While dr.Read

                materialPromotionStockAdjustment = Me.CreateObject(dr)

            End While

            Return materialPromotionStockAdjustment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim materialPromotionStockAdjustmentList As ArrayList = New ArrayList

            While dr.Read
                Dim materialPromotionStockAdjustment As MaterialPromotionStockAdjustment = Me.CreateObject(dr)
                materialPromotionStockAdjustmentList.Add(materialPromotionStockAdjustment)
            End While

            Return materialPromotionStockAdjustmentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionStockAdjustment As MaterialPromotionStockAdjustment = CType(obj, MaterialPromotionStockAdjustment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionStockAdjustment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionStockAdjustment As MaterialPromotionStockAdjustment = CType(obj, MaterialPromotionStockAdjustment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@AdjustType", DbType.Byte, materialPromotionStockAdjustment.AdjustType)
            DBCommandWrapper.AddInParameter("@StockAwal", DbType.Int32, materialPromotionStockAdjustment.StockAwal)
            DBCommandWrapper.AddInParameter("@Qty", DbType.Int32, materialPromotionStockAdjustment.Qty)
            DBCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, materialPromotionStockAdjustment.Keterangan)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, materialPromotionStockAdjustment.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionStockAdjustment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, materialPromotionStockAdjustment.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MaterialPromotionID", DbType.Int32, Me.GetRefObject(materialPromotionStockAdjustment.MaterialPromotion))
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(materialPromotionStockAdjustment.Dealer))

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

            Dim materialPromotionStockAdjustment As MaterialPromotionStockAdjustment = CType(obj, MaterialPromotionStockAdjustment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionStockAdjustment.ID)
            DBCommandWrapper.AddInParameter("@AdjustType", DbType.Byte, materialPromotionStockAdjustment.AdjustType)
            DBCommandWrapper.AddInParameter("@StockAwal", DbType.Int32, materialPromotionStockAdjustment.StockAwal)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, materialPromotionStockAdjustment.Qty)
            DBCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, materialPromotionStockAdjustment.Keterangan)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, materialPromotionStockAdjustment.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionStockAdjustment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, materialPromotionStockAdjustment.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@MaterialPromotionID", DbType.Int32, Me.GetRefObject(materialPromotionStockAdjustment.MaterialPromotion))
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(materialPromotionStockAdjustment.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MaterialPromotionStockAdjustment

            Dim materialPromotionStockAdjustment As MaterialPromotionStockAdjustment = New MaterialPromotionStockAdjustment

            materialPromotionStockAdjustment.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AdjustType")) Then materialPromotionStockAdjustment.AdjustType = CType(dr("AdjustType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("StockAwal")) Then materialPromotionStockAdjustment.StockAwal = CType(dr("StockAwal"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then materialPromotionStockAdjustment.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Keterangan")) Then materialPromotionStockAdjustment.Keterangan = dr("Keterangan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then materialPromotionStockAdjustment.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then materialPromotionStockAdjustment.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then materialPromotionStockAdjustment.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then materialPromotionStockAdjustment.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then materialPromotionStockAdjustment.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then materialPromotionStockAdjustment.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialPromotionID")) Then
                materialPromotionStockAdjustment.MaterialPromotion = New MaterialPromotion(CType(dr("MaterialPromotionID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                materialPromotionStockAdjustment.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return materialPromotionStockAdjustment

        End Function

        Private Sub SetTableName()

            If Not (GetType(MaterialPromotionStockAdjustment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MaterialPromotionStockAdjustment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MaterialPromotionStockAdjustment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

