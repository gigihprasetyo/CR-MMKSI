#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PODetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 3/7/2006 - 9:32:40 AM
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

    Public Class PODetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPODetail"
        Private m_UpdateStatement As String = "up_UpdatePODetail"
        Private m_RetrieveStatement As String = "up_RetrievePODetail"
        Private m_RetrieveListStatement As String = "up_RetrievePODetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePODetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pODetail As PODetail = Nothing
            While dr.Read

                pODetail = Me.CreateObject(dr)

            End While

            Return pODetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pODetailList As ArrayList = New ArrayList

            While dr.Read
                Dim pODetail As PODetail = Me.CreateObject(dr)
                pODetailList.Add(pODetail)
            End While

            Return pODetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODetail As PODetail = CType(obj, PODetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pODetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODetail As PODetail = CType(obj, PODetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@LineItem", DbType.Int32, pODetail.LineItem)
            DbCommandWrapper.AddInParameter("@ReqQty", DbType.Int32, pODetail.ReqQty)
            DbCommandWrapper.AddInParameter("@ProposeQty", DbType.Int32, pODetail.ProposeQty)
            DbCommandWrapper.AddInParameter("@AllocQty", DbType.Int32, pODetail.AllocQty)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, pODetail.Price)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Currency, pODetail.Discount)
            DbCommandWrapper.AddInParameter("@Interest", DbType.Currency, pODetail.Interest)
            DbCommandWrapper.AddInParameter("@AllocationDateTime", DbType.DateTime, pODetail.AllocationDateTime)
            DbCommandWrapper.AddInParameter("@LogisticCost", DbType.Currency, pODetail.LogisticCost)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pODetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pODetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FreeDays", DbType.Int32, pODetail.FreeDays)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, pODetail.MaxTOPDay)

            DbCommandWrapper.AddInParameter("@ContractDetailID", DbType.Int32, Me.GetRefObject(pODetail.ContractDetail))
            DbCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, Me.GetRefObject(pODetail.POHeader))

            '' CR Sirkular Rewards
            '' by : ali Akbar
            '' 2014-09-24
            DBCommandWrapper.AddInParameter("@DiscountReward", DbType.Currency, pODetail.DiscountReward)
            DBCommandWrapper.AddInParameter("@AmountReward", DbType.Currency, pODetail.AmountReward)
            DBCommandWrapper.AddInParameter("@PPh22", DbType.Currency, pODetail.PPh22)
            DBCommandWrapper.AddInParameter("@AmountRewardDepA", DbType.Currency, pODetail.AmountRewardDepA)
            '' END OF CR Sirkular Rewards
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

            Dim pODetail As PODetail = CType(obj, PODetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pODetail.ID)
            DbCommandWrapper.AddInParameter("@LineItem", DbType.Int32, pODetail.LineItem)
            DbCommandWrapper.AddInParameter("@ReqQty", DbType.Int32, pODetail.ReqQty)
            DbCommandWrapper.AddInParameter("@ProposeQty", DbType.Int32, pODetail.ProposeQty)
            DbCommandWrapper.AddInParameter("@AllocQty", DbType.Int32, pODetail.AllocQty)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, pODetail.Price)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Currency, pODetail.Discount)
            DBCommandWrapper.AddInParameter("@Interest", DbType.Currency, pODetail.Interest)
            DbCommandWrapper.AddInParameter("@AllocationDateTime", DbType.DateTime, pODetail.AllocationDateTime)
            DbCommandWrapper.AddInParameter("@LogisticCost", DbType.Currency, pODetail.LogisticCost)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pODetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pODetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FreeDays", DbType.Int32, pODetail.FreeDays)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, pODetail.MaxTOPDay)

            DbCommandWrapper.AddInParameter("@ContractDetailID", DbType.Int32, Me.GetRefObject(pODetail.ContractDetail))
            DBCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, Me.GetRefObject(pODetail.POHeader))

            '' CR Sirkular Rewards
            '' by : ali Akbar
            '' 2014-09-24
            DBCommandWrapper.AddInParameter("@DiscountReward", DbType.Currency, pODetail.DiscountReward)
            DBCommandWrapper.AddInParameter("@AmountReward", DbType.Currency, pODetail.AmountReward)
            DBCommandWrapper.AddInParameter("@PPh22", DbType.Currency, pODetail.PPh22)
            DBCommandWrapper.AddInParameter("@AmountRewardDepA", DbType.Currency, pODetail.AmountRewardDepA)
            '' END OF CR Sirkular Rewards
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PODetail

            Dim pODetail As PODetail = New PODetail

            pODetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LineItem")) Then pODetail.LineItem = CType(dr("LineItem"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReqQty")) Then pODetail.ReqQty = CType(dr("ReqQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProposeQty")) Then pODetail.ProposeQty = CType(dr("ProposeQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocQty")) Then pODetail.AllocQty = CType(dr("AllocQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Price")) Then pODetail.Price = CType(dr("Price"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then pODetail.Discount = CType(dr("Discount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Interest")) Then pODetail.Interest = CType(dr("Interest"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationDateTime")) Then pODetail.AllocationDateTime = CType(dr("AllocationDateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pODetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pODetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pODetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pODetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pODetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ContractDetailID")) Then
                pODetail.ContractDetail = New ContractDetail(CType(dr("ContractDetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("POHeaderID")) Then
                pODetail.POHeader = New POHeader(CType(dr("POHeaderID"), Integer))
            End If
            '' CR Sirkular Rewards
            '' by : ali Akbar
            '' 2014-09-24
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountReward")) Then pODetail.DiscountReward = CType(dr("DiscountReward"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AmountReward")) Then pODetail.AmountReward = CType(dr("AmountReward"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPh22")) Then pODetail.PPh22 = CType(dr("PPh22"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AmountRewardDepA")) Then pODetail.AmountRewardDepA = CType(dr("AmountRewardDepA"), Decimal)
            '' END OF CR Sirkular Rewards
            If Not dr.IsDBNull(dr.GetOrdinal("LogisticCost")) Then pODetail.LogisticCost = CType(dr("LogisticCost"), Decimal)

            If Not dr.IsDBNull(dr.GetOrdinal("FreeDays")) Then pODetail.FreeDays = CType(dr("FreeDays"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDay")) Then pODetail.MaxTOPDay = CType(dr("MaxTOPDay"), Integer)
            Return pODetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(PODetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PODetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PODetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

