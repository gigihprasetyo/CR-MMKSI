#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionAllocation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 11/4/2007 - 01:46:47 PM
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

    Public Class MaterialPromotionAllocationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMaterialPromotionAllocation"
        Private m_UpdateStatement As String = "up_UpdateMaterialPromotionAllocation"
        Private m_RetrieveStatement As String = "up_RetrieveMaterialPromotionAllocation"
        Private m_RetrieveListStatement As String = "up_RetrieveMaterialPromotionAllocationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMaterialPromotionAllocation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim materialPromotionAllocation As MaterialPromotionAllocation = Nothing
            While dr.Read

                materialPromotionAllocation = Me.CreateObject(dr)

            End While

            Return materialPromotionAllocation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim materialPromotionAllocationList As ArrayList = New ArrayList

            While dr.Read
                Dim materialPromotionAllocation As MaterialPromotionAllocation = Me.CreateObject(dr)
                materialPromotionAllocationList.Add(materialPromotionAllocation)
            End While

            Return materialPromotionAllocationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionAllocation As MaterialPromotionAllocation = CType(obj, MaterialPromotionAllocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionAllocation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionAllocation As MaterialPromotionAllocation = CType(obj, MaterialPromotionAllocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, materialPromotionAllocation.Qty)
            DbCommandWrapper.AddInParameter("@ValidateQty", DbType.Int32, materialPromotionAllocation.ValidateQty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionAllocation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, materialPromotionAllocation.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(materialPromotionAllocation.Dealer))
            DbCommandWrapper.AddInParameter("@MaterialPromotionID", DbType.Int32, Me.GetRefObject(materialPromotionAllocation.MaterialPromotion))
            DbCommandWrapper.AddInParameter("@MaterialPromotionPeriodID", DbType.Int32, Me.GetRefObject(materialPromotionAllocation.MaterialPromotionPeriod))

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

            Dim materialPromotionAllocation As MaterialPromotionAllocation = CType(obj, MaterialPromotionAllocation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionAllocation.ID)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, materialPromotionAllocation.Qty)
            DbCommandWrapper.AddInParameter("@ValidateQty", DbType.Int32, materialPromotionAllocation.ValidateQty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionAllocation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, materialPromotionAllocation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(materialPromotionAllocation.Dealer))
            DbCommandWrapper.AddInParameter("@MaterialPromotionID", DbType.Int32, Me.GetRefObject(materialPromotionAllocation.MaterialPromotion))
            DbCommandWrapper.AddInParameter("@MaterialPromotionPeriodID", DbType.Int32, Me.GetRefObject(materialPromotionAllocation.MaterialPromotionPeriod))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MaterialPromotionAllocation

            Dim materialPromotionAllocation As MaterialPromotionAllocation = New MaterialPromotionAllocation

            materialPromotionAllocation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then materialPromotionAllocation.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateQty")) Then materialPromotionAllocation.ValidateQty = CType(dr("ValidateQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then materialPromotionAllocation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then materialPromotionAllocation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then materialPromotionAllocation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then materialPromotionAllocation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then materialPromotionAllocation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                materialPromotionAllocation.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialPromotionID")) Then
                materialPromotionAllocation.MaterialPromotion = New MaterialPromotion(CType(dr("MaterialPromotionID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialPromotionPeriodID")) Then
                materialPromotionAllocation.MaterialPromotionPeriod = New MaterialPromotionPeriod(CType(dr("MaterialPromotionPeriodID"), Integer))
            End If

            Return materialPromotionAllocation

        End Function

        Private Sub SetTableName()

            If Not (GetType(MaterialPromotionAllocation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MaterialPromotionAllocation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MaterialPromotionAllocation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

