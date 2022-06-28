#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MarketPrice Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 25/09/2007 - 14:56:16
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

    Public Class MarketPriceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMarketPrice"
        Private m_UpdateStatement As String = "up_UpdateMarketPrice"
        Private m_RetrieveStatement As String = "up_RetrieveMarketPrice"
        Private m_RetrieveListStatement As String = "up_RetrieveMarketPriceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMarketPrice"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim marketPrice As MarketPrice = Nothing
            While dr.Read

                marketPrice = Me.CreateObject(dr)

            End While

            Return marketPrice

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim marketPriceList As ArrayList = New ArrayList

            While dr.Read
                Dim marketPrice As MarketPrice = Me.CreateObject(dr)
                marketPriceList.Add(marketPrice)
            End While

            Return marketPriceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim marketPrice As MarketPrice = CType(obj, MarketPrice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, marketPrice.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim marketPrice As MarketPrice = CType(obj, MarketPrice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ValidDate", DbType.DateTime, marketPrice.ValidDate)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, marketPrice.PostingDate)
            DbCommandWrapper.AddInParameter("@MarketCategory", DbType.Int16, marketPrice.MarketCategory)
            DbCommandWrapper.AddInParameter("@OnTheRoadPrice", DbType.Currency, marketPrice.OnTheRoadPrice)
            DbCommandWrapper.AddInParameter("@BBN", DbType.Currency, marketPrice.BBN)
            DbCommandWrapper.AddInParameter("@OtherInfo", DbType.AnsiString, marketPrice.OtherInfo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, marketPrice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, marketPrice.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(marketPrice.Dealer))
            DbCommandWrapper.AddInParameter("@CompetitorTypeID", DbType.Int32, Me.GetRefObject(marketPrice.CompetitorType))

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

            Dim marketPrice As MarketPrice = CType(obj, MarketPrice)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, marketPrice.ID)
            DbCommandWrapper.AddInParameter("@ValidDate", DbType.DateTime, marketPrice.ValidDate)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, marketPrice.PostingDate)
            DbCommandWrapper.AddInParameter("@MarketCategory", DbType.Int16, marketPrice.MarketCategory)
            DbCommandWrapper.AddInParameter("@OnTheRoadPrice", DbType.Currency, marketPrice.OnTheRoadPrice)
            DbCommandWrapper.AddInParameter("@BBN", DbType.Currency, marketPrice.BBN)
            DbCommandWrapper.AddInParameter("@OtherInfo", DbType.AnsiString, marketPrice.OtherInfo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, marketPrice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, marketPrice.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(marketPrice.Dealer))
            DbCommandWrapper.AddInParameter("@CompetitorTypeID", DbType.Int32, Me.GetRefObject(marketPrice.CompetitorType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MarketPrice

            Dim marketPrice As MarketPrice = New MarketPrice

            marketPrice.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidDate")) Then marketPrice.ValidDate = CType(dr("ValidDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PostingDate")) Then marketPrice.PostingDate = CType(dr("PostingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MarketCategory")) Then marketPrice.MarketCategory = CType(dr("MarketCategory"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("OnTheRoadPrice")) Then marketPrice.OnTheRoadPrice = CType(dr("OnTheRoadPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BBN")) Then marketPrice.BBN = CType(dr("BBN"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("OtherInfo")) Then marketPrice.OtherInfo = dr("OtherInfo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then marketPrice.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then marketPrice.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then marketPrice.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then marketPrice.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then marketPrice.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                marketPrice.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CompetitorTypeID")) Then
                marketPrice.CompetitorType = New CompetitorType(CType(dr("CompetitorTypeID"), Integer))
            End If

            Return marketPrice

        End Function

        Private Sub SetTableName()

            If Not (GetType(MarketPrice) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MarketPrice), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MarketPrice).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

