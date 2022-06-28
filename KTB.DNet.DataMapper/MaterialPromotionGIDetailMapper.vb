#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionGIDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/7/2007 - 10:26:01 AM
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

    Public Class MaterialPromotionGIDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMaterialPromotionGIDetail"
        Private m_UpdateStatement As String = "up_UpdateMaterialPromotionGIDetail"
        Private m_RetrieveStatement As String = "up_RetrieveMaterialPromotionGIDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveMaterialPromotionGIDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMaterialPromotionGIDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim materialPromotionGIDetail As MaterialPromotionGIDetail = Nothing
            While dr.Read

                materialPromotionGIDetail = Me.CreateObject(dr)

            End While

            Return materialPromotionGIDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim materialPromotionGIDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim materialPromotionGIDetail As MaterialPromotionGIDetail = Me.CreateObject(dr)
                materialPromotionGIDetailList.Add(materialPromotionGIDetail)
            End While

            Return materialPromotionGIDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionGIDetail As MaterialPromotionGIDetail = CType(obj, MaterialPromotionGIDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionGIDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionGIDetail As MaterialPromotionGIDetail = CType(obj, MaterialPromotionGIDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, materialPromotionGIDetail.Qty)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, materialPromotionGIDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionGIDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, materialPromotionGIDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MaterialPromotionGIHeaderID", DbType.Int32, Me.GetRefObject(materialPromotionGIDetail.MaterialPromotionGIHeader))
            DbCommandWrapper.AddInParameter("@MaterialPromotionID", DbType.Int32, Me.GetRefObject(materialPromotionGIDetail.MaterialPromotion))

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

            Dim materialPromotionGIDetail As MaterialPromotionGIDetail = CType(obj, MaterialPromotionGIDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionGIDetail.ID)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, materialPromotionGIDetail.Qty)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, materialPromotionGIDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionGIDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, materialPromotionGIDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@MaterialPromotionGIHeaderID", DbType.Int32, Me.GetRefObject(materialPromotionGIDetail.MaterialPromotionGIHeader))
            DbCommandWrapper.AddInParameter("@MaterialPromotionID", DbType.Int32, Me.GetRefObject(materialPromotionGIDetail.MaterialPromotion))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MaterialPromotionGIDetail

            Dim materialPromotionGIDetail As MaterialPromotionGIDetail = New MaterialPromotionGIDetail

            materialPromotionGIDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then materialPromotionGIDetail.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then materialPromotionGIDetail.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then materialPromotionGIDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then materialPromotionGIDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then materialPromotionGIDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then materialPromotionGIDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then materialPromotionGIDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialPromotionGIHeaderID")) Then
                materialPromotionGIDetail.MaterialPromotionGIHeader = New MaterialPromotionGIHeader(CType(dr("MaterialPromotionGIHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialPromotionID")) Then
                materialPromotionGIDetail.MaterialPromotion = New MaterialPromotion(CType(dr("MaterialPromotionID"), Integer))
            End If

            Return materialPromotionGIDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(MaterialPromotionGIDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MaterialPromotionGIDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MaterialPromotionGIDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

