
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalDetailPrice Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 19/06/2020 - 14:56:27
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

    Public Class DiscountProposalDetailPriceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDiscountProposalDetailPrice"
        Private m_UpdateStatement As String = "up_UpdateDiscountProposalDetailPrice"
        Private m_RetrieveStatement As String = "up_RetrieveDiscountProposalDetailPrice"
        Private m_RetrieveListStatement As String = "up_RetrieveDiscountProposalDetailPriceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDiscountProposalDetailPrice"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim discountProposalDetailPrice As DiscountProposalDetailPrice = Nothing
            While dr.Read

                discountProposalDetailPrice = Me.CreateObject(dr)

            End While

            Return discountProposalDetailPrice

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim discountProposalDetailPriceList As ArrayList = New ArrayList

            While dr.Read
                Dim discountProposalDetailPrice As DiscountProposalDetailPrice = Me.CreateObject(dr)
                discountProposalDetailPriceList.Add(discountProposalDetailPrice)
            End While

            Return discountProposalDetailPriceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalDetailPrice As DiscountProposalDetailPrice = CType(obj, DiscountProposalDetailPrice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalDetailPrice.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalDetailPrice As DiscountProposalDetailPrice = CType(obj, DiscountProposalDetailPrice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RedemptionPrice", DbType.Currency, discountProposalDetailPrice.RedemptionPrice)
            DbCommandWrapper.AddInParameter("@BBN", DbType.Currency, discountProposalDetailPrice.BBN)
            DbCommandWrapper.AddInParameter("@OtherCost", DbType.Currency, discountProposalDetailPrice.OtherCost)
            DbCommandWrapper.AddInParameter("@DiscountRequest", DbType.Currency, discountProposalDetailPrice.DiscountRequest)
            DbCommandWrapper.AddInParameter("@LogisticCost", DbType.Currency, discountProposalDetailPrice.LogisticCost)
            DbCommandWrapper.AddInParameter("@RetailPriceOnRoad", DbType.Currency, discountProposalDetailPrice.RetailPriceOnRoad)
            DbCommandWrapper.AddInParameter("@DeliveryCost", DbType.Currency, discountProposalDetailPrice.DeliveryCost)
            DbCommandWrapper.AddInParameter("@Accessories", DbType.Currency, discountProposalDetailPrice.Accessories)
            DbCommandWrapper.AddInParameter("@DealPriceEstimation", DbType.Currency, discountProposalDetailPrice.DealPriceEstimation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalDetailPrice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, discountProposalDetailPrice.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DiscountProposalHeaderID", DbType.Int32, Me.GetRefObject(discountProposalDetailPrice.DiscountProposalHeader))
            DbCommandWrapper.AddInParameter("@DiscountProposalDetailID", DbType.Int32, Me.GetRefObject(discountProposalDetailPrice.DiscountProposalDetail))

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

            Dim discountProposalDetailPrice As DiscountProposalDetailPrice = CType(obj, DiscountProposalDetailPrice)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalDetailPrice.ID)
            DbCommandWrapper.AddInParameter("@RedemptionPrice", DbType.Currency, discountProposalDetailPrice.RedemptionPrice)
            DbCommandWrapper.AddInParameter("@BBN", DbType.Currency, discountProposalDetailPrice.BBN)
            DbCommandWrapper.AddInParameter("@OtherCost", DbType.Currency, discountProposalDetailPrice.OtherCost)
            DbCommandWrapper.AddInParameter("@DiscountRequest", DbType.Currency, discountProposalDetailPrice.DiscountRequest)
            DbCommandWrapper.AddInParameter("@LogisticCost", DbType.Currency, discountProposalDetailPrice.LogisticCost)
            DbCommandWrapper.AddInParameter("@RetailPriceOnRoad", DbType.Currency, discountProposalDetailPrice.RetailPriceOnRoad)
            DbCommandWrapper.AddInParameter("@DeliveryCost", DbType.Currency, discountProposalDetailPrice.DeliveryCost)
            DbCommandWrapper.AddInParameter("@Accessories", DbType.Currency, discountProposalDetailPrice.Accessories)
            DbCommandWrapper.AddInParameter("@DealPriceEstimation", DbType.Currency, discountProposalDetailPrice.DealPriceEstimation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalDetailPrice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, discountProposalDetailPrice.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DiscountProposalHeaderID", DbType.Int32, Me.GetRefObject(discountProposalDetailPrice.DiscountProposalHeader))
            DbCommandWrapper.AddInParameter("@DiscountProposalDetailID", DbType.Int32, Me.GetRefObject(discountProposalDetailPrice.DiscountProposalDetail))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DiscountProposalDetailPrice

            Dim discountProposalDetailPrice As DiscountProposalDetailPrice = New DiscountProposalDetailPrice

            discountProposalDetailPrice.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RedemptionPrice")) Then discountProposalDetailPrice.RedemptionPrice = CType(dr("RedemptionPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BBN")) Then discountProposalDetailPrice.BBN = CType(dr("BBN"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("OtherCost")) Then discountProposalDetailPrice.OtherCost = CType(dr("OtherCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountRequest")) Then discountProposalDetailPrice.DiscountRequest = CType(dr("DiscountRequest"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LogisticCost")) Then discountProposalDetailPrice.LogisticCost = CType(dr("LogisticCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RetailPriceOnRoad")) Then discountProposalDetailPrice.RetailPriceOnRoad = CType(dr("RetailPriceOnRoad"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryCost")) Then discountProposalDetailPrice.DeliveryCost = CType(dr("DeliveryCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Accessories")) Then discountProposalDetailPrice.Accessories = CType(dr("Accessories"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DealPriceEstimation")) Then discountProposalDetailPrice.DealPriceEstimation = CType(dr("DealPriceEstimation"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then discountProposalDetailPrice.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then discountProposalDetailPrice.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then discountProposalDetailPrice.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then discountProposalDetailPrice.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then discountProposalDetailPrice.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountProposalHeaderID")) Then
                discountProposalDetailPrice.DiscountProposalHeader = New DiscountProposalHeader(CType(dr("DiscountProposalHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountProposalDetailID")) Then
                discountProposalDetailPrice.DiscountProposalDetail = New DiscountProposalDetail(CType(dr("DiscountProposalDetailID"), Integer))
            End If

            Return discountProposalDetailPrice

        End Function

        Private Sub SetTableName()

            If Not (GetType(DiscountProposalDetailPrice) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DiscountProposalDetailPrice), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DiscountProposalDetailPrice).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

