#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ContractDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2006 - 8:47:20 AM
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

    Public Class ContractDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertContractDetail"
        Private m_UpdateStatement As String = "up_UpdateContractDetail"
        Private m_RetrieveStatement As String = "up_RetrieveContractDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveContractDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteContractDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim contractDetail As ContractDetail = Nothing
            While dr.Read

                contractDetail = Me.CreateObject(dr)

            End While

            Return contractDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim contractDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim contractDetail As ContractDetail = Me.CreateObject(dr)
                contractDetailList.Add(contractDetail)
            End While

            Return contractDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim contractDetail As ContractDetail = CType(obj, ContractDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, contractDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim contractDetail As ContractDetail = CType(obj, ContractDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@LineItem", DbType.Int32, contractDetail.LineItem)
            DbCommandWrapper.AddInParameter("@TargetQty", DbType.Int32, contractDetail.TargetQty)
            DbCommandWrapper.AddInParameter("@ConfirmQty", DbType.Int32, contractDetail.ConfirmQty)
            DbCommandWrapper.AddInParameter("@InProcessQty", DbType.Int32, contractDetail.InProcessQty)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, contractDetail.Amount)
            DbCommandWrapper.AddInParameter("@PPh22", DbType.Currency, contractDetail.PPh22)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Currency, contractDetail.Discount)
            DbCommandWrapper.AddInParameter("@SalesSurcharge", DbType.Currency, contractDetail.SalesSurcharge)
            DBCommandWrapper.AddInParameter("@GuaranteeAmount", DbType.Currency, contractDetail.GuaranteeAmount)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, contractDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, contractDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PKDetailID", DbType.Int32, Me.GetRefObject(contractDetail.PKDetail))
            DbCommandWrapper.AddInParameter("@ContractHeaderID", DbType.Int32, Me.GetRefObject(contractDetail.ContractHeader))
            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int32, Me.GetRefObject(contractDetail.VechileColor))

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

            Dim contractDetail As ContractDetail = CType(obj, ContractDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, contractDetail.ID)
            DbCommandWrapper.AddInParameter("@LineItem", DbType.Int32, contractDetail.LineItem)
            DbCommandWrapper.AddInParameter("@TargetQty", DbType.Int32, contractDetail.TargetQty)
            DbCommandWrapper.AddInParameter("@ConfirmQty", DbType.Int32, contractDetail.ConfirmQty)
            DbCommandWrapper.AddInParameter("@InProcessQty", DbType.Int32, contractDetail.InProcessQty)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, contractDetail.Amount)
            DbCommandWrapper.AddInParameter("@PPh22", DbType.Currency, contractDetail.PPh22)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Currency, contractDetail.Discount)
            DbCommandWrapper.AddInParameter("@SalesSurcharge", DbType.Currency, contractDetail.SalesSurcharge)
            DBCommandWrapper.AddInParameter("@GuaranteeAmount", DbType.Currency, contractDetail.GuaranteeAmount)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, contractDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, contractDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@PKDetailID", DbType.Int32, Me.GetRefObject(contractDetail.PKDetail))
            DbCommandWrapper.AddInParameter("@ContractHeaderID", DbType.Int32, Me.GetRefObject(contractDetail.ContractHeader))
            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int32, Me.GetRefObject(contractDetail.VechileColor))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ContractDetail

            Dim contractDetail As ContractDetail = New ContractDetail

            contractDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LineItem")) Then contractDetail.LineItem = CType(dr("LineItem"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TargetQty")) Then contractDetail.TargetQty = CType(dr("TargetQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmQty")) Then contractDetail.ConfirmQty = CType(dr("ConfirmQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("InProcessQty")) Then contractDetail.InProcessQty = CType(dr("InProcessQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then contractDetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPh22")) Then contractDetail.PPh22 = CType(dr("PPh22"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then contractDetail.Discount = CType(dr("Discount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesSurcharge")) Then contractDetail.SalesSurcharge = CType(dr("SalesSurcharge"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("GuaranteeAmount")) Then contractDetail.GuaranteeAmount = CType(dr("GuaranteeAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then contractDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then contractDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then contractDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then contractDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then contractDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PKDetailID")) Then
                contractDetail.PKDetail = New PKDetail(CType(dr("PKDetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ContractHeaderID")) Then
                contractDetail.ContractHeader = New ContractHeader(CType(dr("ContractHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorID")) Then
                contractDetail.VechileColor = New VechileColor(CType(dr("VehicleColorID"), Integer))
            End If

            Return contractDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(ContractDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ContractDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ContractDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

