#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionGIGR Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 11/08/2007 - 22:26
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

    Public Class MaterialPromotionGIGRMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMaterialPromotionGIGR"
        Private m_UpdateStatement As String = "up_UpdateMaterialPromotionGIGR"
        Private m_RetrieveStatement As String = "up_RetrieveMaterialPromotionGIGR"
        Private m_RetrieveListStatement As String = "up_RetrieveMaterialPromotionGIGRList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMaterialPromotionGIGR"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim materialPromotionGIGR As MaterialPromotionGIGR = Nothing
            While dr.Read

                materialPromotionGIGR = Me.CreateObject(dr)

            End While

            Return materialPromotionGIGR

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim materialPromotionGIGRList As ArrayList = New ArrayList

            While dr.Read
                Dim materialPromotionGIGR As MaterialPromotionGIGR = Me.CreateObject(dr)
                materialPromotionGIGRList.Add(materialPromotionGIGR)
            End While

            Return materialPromotionGIGRList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionGIGR As MaterialPromotionGIGR = CType(obj, MaterialPromotionGIGR)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionGIGR.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionGIGR As MaterialPromotionGIGR = CType(obj, MaterialPromotionGIGR)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, materialPromotionGIGR.RequestNo)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, materialPromotionGIGR.Qty)
            DbCommandWrapper.AddInParameter("@NoGI", DbType.AnsiString, materialPromotionGIGR.NoGI)
            DbCommandWrapper.AddInParameter("@NoGR", DbType.AnsiString, materialPromotionGIGR.NoGR)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, materialPromotionGIGR.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionGIGR.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, materialPromotionGIGR.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(materialPromotionGIGR.Dealer))
            DbCommandWrapper.AddInParameter("@MaterialPromotionID", DbType.Int32, Me.GetRefObject(materialPromotionGIGR.MaterialPromotion))
            DbCommandWrapper.AddInParameter("@MaterialPromotionPeriodID", DbType.Int32, Me.GetRefObject(materialPromotionGIGR.MaterialPromotionPeriod))

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

            Dim materialPromotionGIGR As MaterialPromotionGIGR = CType(obj, MaterialPromotionGIGR)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionGIGR.ID)
            DbCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, materialPromotionGIGR.RequestNo)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, materialPromotionGIGR.Qty)
            DbCommandWrapper.AddInParameter("@NoGI", DbType.AnsiString, materialPromotionGIGR.NoGI)
            DbCommandWrapper.AddInParameter("@NoGR", DbType.AnsiString, materialPromotionGIGR.NoGR)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, materialPromotionGIGR.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionGIGR.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, materialPromotionGIGR.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(materialPromotionGIGR.Dealer))
            DbCommandWrapper.AddInParameter("@MaterialPromotionID", DbType.Int32, Me.GetRefObject(materialPromotionGIGR.MaterialPromotion))
            DbCommandWrapper.AddInParameter("@MaterialPromotionPeriodID", DbType.Int32, Me.GetRefObject(materialPromotionGIGR.MaterialPromotionPeriod))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MaterialPromotionGIGR

            Dim materialPromotionGIGR As MaterialPromotionGIGR = New MaterialPromotionGIGR

            materialPromotionGIGR.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestNo")) Then materialPromotionGIGR.RequestNo = dr("RequestNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then materialPromotionGIGR.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoGI")) Then materialPromotionGIGR.NoGI = dr("NoGI").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoGR")) Then materialPromotionGIGR.NoGR = dr("NoGR").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then materialPromotionGIGR.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then materialPromotionGIGR.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then materialPromotionGIGR.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then materialPromotionGIGR.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then materialPromotionGIGR.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then materialPromotionGIGR.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                materialPromotionGIGR.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialPromotionID")) Then
                materialPromotionGIGR.MaterialPromotion = New MaterialPromotion(CType(dr("MaterialPromotionID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialPromotionPeriodID")) Then
                materialPromotionGIGR.MaterialPromotionPeriod = New MaterialPromotionPeriod(CType(dr("MaterialPromotionPeriodID"), Integer))
            End If

            Return materialPromotionGIGR

        End Function

        Private Sub SetTableName()

            If Not (GetType(MaterialPromotionGIGR) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MaterialPromotionGIGR), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MaterialPromotionGIGR).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

