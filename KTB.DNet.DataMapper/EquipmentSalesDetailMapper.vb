#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EquipmentSalesDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 1/9/2006 - 1:03:36 PM
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

    Public Class EquipmentSalesDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEquipmentSalesDetail"
        Private m_UpdateStatement As String = "up_UpdateEquipmentSalesDetail"
        Private m_RetrieveStatement As String = "up_RetrieveEquipmentSalesDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveEquipmentSalesDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEquipmentSalesDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim equipmentSalesDetail As EquipmentSalesDetail = Nothing
            While dr.Read

                equipmentSalesDetail = Me.CreateObject(dr)

            End While

            Return equipmentSalesDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim equipmentSalesDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim equipmentSalesDetail As EquipmentSalesDetail = Me.CreateObject(dr)
                equipmentSalesDetailList.Add(equipmentSalesDetail)
            End While

            Return equipmentSalesDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim equipmentSalesDetail As EquipmentSalesDetail = CType(obj, EquipmentSalesDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, equipmentSalesDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim equipmentSalesDetail As EquipmentSalesDetail = CType(obj, EquipmentSalesDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@LineItem", DbType.Int32, equipmentSalesDetail.LineItem)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, equipmentSalesDetail.Quantity)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Int16, equipmentSalesDetail.Discount)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, equipmentSalesDetail.Price)
            DbCommandWrapper.AddInParameter("@EstimatePrice", DbType.Currency, equipmentSalesDetail.EstimatePrice)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, equipmentSalesDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, equipmentSalesDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@EquipmentmasterID", DbType.Int32, Me.GetRefObject(equipmentSalesDetail.EquipmentMaster))
            DbCommandWrapper.AddInParameter("@EquipmentSalesHeaderID", DbType.Int32, Me.GetRefObject(equipmentSalesDetail.EquipmentSalesHeader))

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

            Dim equipmentSalesDetail As EquipmentSalesDetail = CType(obj, EquipmentSalesDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, equipmentSalesDetail.ID)
            DbCommandWrapper.AddInParameter("@LineItem", DbType.Int32, equipmentSalesDetail.LineItem)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, equipmentSalesDetail.Quantity)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Int16, equipmentSalesDetail.Discount)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, equipmentSalesDetail.Price)
            DbCommandWrapper.AddInParameter("@EstimatePrice", DbType.Currency, equipmentSalesDetail.EstimatePrice)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, equipmentSalesDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, equipmentSalesDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@EquipmentmasterID", DbType.Int32, Me.GetRefObject(equipmentSalesDetail.EquipmentMaster))
            DbCommandWrapper.AddInParameter("@EquipmentSalesHeaderID", DbType.Int32, Me.GetRefObject(equipmentSalesDetail.EquipmentSalesHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EquipmentSalesDetail

            Dim equipmentSalesDetail As EquipmentSalesDetail = New EquipmentSalesDetail

            equipmentSalesDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LineItem")) Then equipmentSalesDetail.LineItem = CType(dr("LineItem"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then equipmentSalesDetail.Quantity = CType(dr("Quantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then equipmentSalesDetail.Discount = CType(dr("Discount"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Price")) Then equipmentSalesDetail.Price = CType(dr("Price"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("EstimatePrice")) Then equipmentSalesDetail.EstimatePrice = CType(dr("EstimatePrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then equipmentSalesDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then equipmentSalesDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then equipmentSalesDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then equipmentSalesDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then equipmentSalesDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EquipmentmasterID")) Then
                equipmentSalesDetail.EquipmentMaster = New EquipmentMaster(CType(dr("EquipmentmasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EquipmentSalesHeaderID")) Then
                equipmentSalesDetail.EquipmentSalesHeader = New EquipmentSalesHeader(CType(dr("EquipmentSalesHeaderID"), Integer))
            End If

            Return equipmentSalesDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(EquipmentSalesDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EquipmentSalesDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EquipmentSalesDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

