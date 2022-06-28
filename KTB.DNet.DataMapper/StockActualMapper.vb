
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : StockActual Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/14/2015 - 10:53:40 AM
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

    Public Class StockActualMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertStockActual"
        Private m_UpdateStatement As String = "up_UpdateStockActual"
        Private m_RetrieveStatement As String = "up_RetrieveStockActual"
        Private m_RetrieveListStatement As String = "up_RetrieveStockActualList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteStockActual"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim stockActual As StockActual = Nothing
            While dr.Read

                stockActual = Me.CreateObject(dr)

            End While

            Return stockActual

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim stockActualList As ArrayList = New ArrayList

            While dr.Read
                Dim stockActual As StockActual = Me.CreateObject(dr)
                stockActualList.Add(stockActual)
            End While

            Return stockActualList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim stockActual As StockActual = CType(obj, StockActual)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, stockActual.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim stockActual As StockActual = CType(obj, StockActual)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Month", DbType.Int16, stockActual.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, stockActual.Year)
            DbCommandWrapper.AddInParameter("@Stock", DbType.Int32, stockActual.Stock)
            DbCommandWrapper.AddInParameter("@RatioEOM", DbType.Decimal, stockActual.RatioEOM)
            DbCommandWrapper.AddInParameter("@RatioSPM", DbType.Decimal, stockActual.RatioSPM)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, stockActual.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, stockActual.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(stockActual.Dealer))
            DbCommandWrapper.AddInParameter("@ModelID", DbType.Int16, Me.GetRefObject(stockActual.VechileModel))
            DbCommandWrapper.AddInParameter("@StockTargetID", DbType.Int32, Me.GetRefObject(stockActual.StockTarget))

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

            Dim stockActual As StockActual = CType(obj, StockActual)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, stockActual.ID)
            DbCommandWrapper.AddInParameter("@Month", DbType.Int16, stockActual.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int16, stockActual.Year)
            DbCommandWrapper.AddInParameter("@Stock", DbType.Int32, stockActual.Stock)
            DbCommandWrapper.AddInParameter("@RatioEOM", DbType.Decimal, stockActual.RatioEOM)
            DbCommandWrapper.AddInParameter("@RatioSPM", DbType.Decimal, stockActual.RatioSPM)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, stockActual.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, stockActual.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(stockActual.Dealer))
            DbCommandWrapper.AddInParameter("@ModelID", DbType.Int16, Me.GetRefObject(stockActual.VechileModel))
            DbCommandWrapper.AddInParameter("@StockTargetID", DbType.Int32, Me.GetRefObject(stockActual.StockTarget))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As StockActual

            Dim stockActual As StockActual = New StockActual

            stockActual.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Month")) Then stockActual.Month = CType(dr("Month"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then stockActual.Year = CType(dr("Year"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Stock")) Then stockActual.Stock = CType(dr("Stock"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RatioEOM")) Then stockActual.RatioEOM = CType(dr("RatioEOM"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RatioSPM")) Then stockActual.RatioSPM = CType(dr("RatioSPM"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then stockActual.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then stockActual.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then stockActual.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then stockActual.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then stockActual.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                stockActual.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ModelID")) Then
                stockActual.VechileModel = New VechileModel(CType(dr("ModelID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("StockTargetID")) Then
                stockActual.StockTarget = New StockTarget(CType(dr("StockTargetID"), Integer))
            End If
            PopulateData(stockActual)
            Return stockActual

        End Function

        Private Sub SetTableName()

            If Not (GetType(StockActual) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(StockActual), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(StockActual).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"
Public Function PopulateData(item As StockActual) As StockActual
    Try 'try for stocktarget
        If (IsNothing(item.StockTarget) OrElse item.StockTarget.ID < 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim firstday As Date = New Date(item.Year, item.Month, 1).AddMonths(1)
            Dim lastday As Date = New Date(item.Year, item.Month, Date.DaysInMonth(item.Year, item.Month)).AddMonths(1)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "ValidFrom", MatchType.GreaterOrEqual, firstday))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "ValidFrom", MatchType.Lesser, lastday))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "Dealer.DealerCode", MatchType.Exact, item.Dealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StockTarget), "VechileModel.Description", MatchType.Exact, item.VechileModel.Description))
            Dim arrlist As ArrayList = New StockTargetMapper().RetrieveByCriteria(criterias)
            If (arrlist IsNot Nothing AndAlso arrlist.Count > 0) Then
                item.StockTarget = arrlist.Item(0)
            End If
        End If

    Catch ex As Exception

    End Try
    Dim PKDetailTargetQty As Integer = 0
    Try 'try for pktarget qty
        Dim aggregates As New Aggregate(GetType(PKDetail), "TargetQty", AggregateType.Sum)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, item.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, item.Year))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.Dealer.DealerCode", MatchType.Exact, item.Dealer.DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.VechileType.VechileModel.ID", MatchType.Exact, item.StockTarget.VechileModel.ID))
        PKDetailTargetQty += New PKDetailMapper().RetrieveScalar(aggregates, criterias)

        Dim dateNextMonth As Date = New Date(item.Year, item.Month, 1).AddMonths(1)

        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, dateNextMonth.Month))
        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, dateNextMonth.Year))
        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.OrderType", MatchType.Exact, 1))
        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.Dealer.DealerCode", MatchType.Exact, item.Dealer.DealerCode))
        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "VechileColor.VechileType.VechileModel.ID", MatchType.Exact, item.StockTarget.VechileModel.ID))
        PKDetailTargetQty += New PKDetailMapper().RetrieveScalar(aggregates, criterias2)

    Catch ex As Exception

    End Try

    Dim OCCarryOver As Integer = 0
    Try 'try for OC.CarryOver
        Dim aggregates As New Aggregate(GetType(ContractDetail), "TargetQty", AggregateType.Sum)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.RefContractNumber", MatchType.IsNotNull, Nothing))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.RefContractNumber", MatchType.No, "''"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.RequestPeriodMonth", MatchType.Exact, item.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.RequestPeriodYear", MatchType.Exact, item.Year))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "ContractHeader.Dealer.DealerCode", MatchType.Exact, item.Dealer.DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractDetail), "VechileColor.VechileType.VechileModel.ID", MatchType.Exact, item.StockTarget.VechileModel.ID))
        OCCarryOver += New ContractDetailMapper().RetrieveScalar(aggregates, criterias)

    Catch ex As Exception

    End Try
    Dim currentItemStock As Integer = 0
    Try
        Dim aggregates As New Aggregate(GetType(ChassisMaster), "ID", AggregateType.Count)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "AlreadySaled", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Dealer.DealerCode", MatchType.Exact, item.Dealer.DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "VechileColor.VechileType.VechileModel.ID", MatchType.Exact, item.StockTarget.VechileModel.ID))
        currentItemStock += New ChassisMasterMapper().RetrieveScalar(aggregates, criterias)
    Catch ex As Exception

    End Try
    Try
        item.TotalUnitPK = PKDetailTargetQty + OCCarryOver
        item.CurrentRatio = (currentItemStock + PKDetailTargetQty + OCCarryOver - item.StockTarget.Target) / item.StockTarget.Target

    Catch ex As Exception

    End Try

    Return item
End Function
#End Region

    End Class
End Namespace

