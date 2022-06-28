#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionRequestDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/10/2007 - 01:24:51 PM
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

    Public Class MaterialPromotionRequestDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMaterialPromotionRequestDetail"
        Private m_UpdateStatement As String = "up_UpdateMaterialPromotionRequestDetail"
        Private m_RetrieveStatement As String = "up_RetrieveMaterialPromotionRequestDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveMaterialPromotionRequestDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMaterialPromotionRequestDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim materialPromotionRequestDetail As MaterialPromotionRequestDetail = Nothing
            While dr.Read

                materialPromotionRequestDetail = Me.CreateObject(dr)

            End While

            Return materialPromotionRequestDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim materialPromotionRequestDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim materialPromotionRequestDetail As MaterialPromotionRequestDetail = Me.CreateObject(dr)
                materialPromotionRequestDetailList.Add(materialPromotionRequestDetail)
            End While

            Return materialPromotionRequestDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionRequestDetail As MaterialPromotionRequestDetail = CType(obj, MaterialPromotionRequestDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionRequestDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionRequestDetail As MaterialPromotionRequestDetail = CType(obj, MaterialPromotionRequestDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RequestQty", DbType.Int32, materialPromotionRequestDetail.RequestQty)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, materialPromotionRequestDetail.Qty)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiStringFixedLength, materialPromotionRequestDetail.Description)
            DbCommandWrapper.AddInParameter("@StatusGI", DbType.Byte, materialPromotionRequestDetail.StatusGI)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionRequestDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, materialPromotionRequestDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MaterialPromotionID", DbType.Int32, Me.GetRefObject(materialPromotionRequestDetail.MaterialPromotion))
            DbCommandWrapper.AddInParameter("@MaterialPromotionRequestID", DbType.Int32, Me.GetRefObject(materialPromotionRequestDetail.MaterialPromotionRequest))

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

            Dim materialPromotionRequestDetail As MaterialPromotionRequestDetail = CType(obj, MaterialPromotionRequestDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionRequestDetail.ID)
            DbCommandWrapper.AddInParameter("@RequestQty", DbType.Int32, materialPromotionRequestDetail.RequestQty)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, materialPromotionRequestDetail.Qty)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiStringFixedLength, materialPromotionRequestDetail.Description)
            DbCommandWrapper.AddInParameter("@StatusGI", DbType.Byte, materialPromotionRequestDetail.StatusGI)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionRequestDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, materialPromotionRequestDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@MaterialPromotionID", DbType.Int32, Me.GetRefObject(materialPromotionRequestDetail.MaterialPromotion))
            DbCommandWrapper.AddInParameter("@MaterialPromotionRequestID", DbType.Int32, Me.GetRefObject(materialPromotionRequestDetail.MaterialPromotionRequest))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MaterialPromotionRequestDetail

            Dim materialPromotionRequestDetail As MaterialPromotionRequestDetail = New MaterialPromotionRequestDetail

            materialPromotionRequestDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestQty")) Then materialPromotionRequestDetail.RequestQty = CType(dr("RequestQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then materialPromotionRequestDetail.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then materialPromotionRequestDetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusGI")) Then materialPromotionRequestDetail.StatusGI = CType(dr("StatusGI"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then materialPromotionRequestDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then materialPromotionRequestDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then materialPromotionRequestDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then materialPromotionRequestDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then materialPromotionRequestDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialPromotionID")) Then
                materialPromotionRequestDetail.MaterialPromotion = New MaterialPromotion(CType(dr("MaterialPromotionID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialPromotionRequestID")) Then
                materialPromotionRequestDetail.MaterialPromotionRequest = New MaterialPromotionRequest(CType(dr("MaterialPromotionRequestID"), Integer))
            End If

            Return materialPromotionRequestDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(MaterialPromotionRequestDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MaterialPromotionRequestDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MaterialPromotionRequestDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

