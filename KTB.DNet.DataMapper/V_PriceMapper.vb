#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : Price Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/29/2005 - 2:48:46 PM
'// Modified on 8/25/2014 - 10:02:58 AM
'//     Change Log :    -> Append DiscountReward Column
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

    Public Class V_PriceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPrice"
        Private m_UpdateStatement As String = "up_UpdatePrice"
        Private m_RetrieveStatement As String = "up_RetrievePrice"
        Private m_RetrieveListStatement As String = "up_RetrievePriceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePrice"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim price As V_Price = Nothing
            While dr.Read

                price = Me.CreateObject(dr)

            End While

            Return price

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim priceList As ArrayList = New ArrayList

            While dr.Read
                Dim price As V_Price = Me.CreateObject(dr)
                priceList.Add(price)
            End While

            Return priceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim price As V_Price = CType(obj, V_Price)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, price.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim price As V_Price = CType(obj, V_Price)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, price.ValidFrom)
            DbCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, price.BasePrice)
            DbCommandWrapper.AddInParameter("@OptionPrice", DbType.Currency, price.OptionPrice)
            DbCommandWrapper.AddInParameter("@PPN_BM", DbType.Currency, price.PPN_BM)
            DbCommandWrapper.AddInParameter("@PPN", DbType.Currency, price.PPN)
            DbCommandWrapper.AddInParameter("@PPh22", DbType.Currency, price.PPh22)
            DbCommandWrapper.AddInParameter("@Interest", DbType.Currency, price.Interest)
            DBCommandWrapper.AddInParameter("@FactoringInt", DbType.Currency, price.FactoringInt)
            DBCommandWrapper.AddInParameter("@PPh23", DbType.Currency, price.PPh23)
            DBCommandWrapper.AddInParameter("@Status", DbType.AnsiString, price.Status)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, price.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, price.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int32, iif(price.VechileColor Is Nothing, CType(DBNull.Value, Object), CType(price.VechileColor.ID, Object)))

            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, IIf(price.Dealer Is Nothing, CType(DBNull.Value, Object), CType(price.Dealer.ID, Object)))
            '' CR Sirkular Rewards
            '' by : ali Akbar
            '' 2014-09-24
            DBCommandWrapper.AddInParameter("@DiscountReward", DbType.Currency, price.DiscountReward)
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

            Dim price As V_Price = CType(obj, V_Price)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, price.ID)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, price.ValidFrom)
            DbCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, price.BasePrice)
            DbCommandWrapper.AddInParameter("@OptionPrice", DbType.Currency, price.OptionPrice)
            DbCommandWrapper.AddInParameter("@PPN_BM", DbType.Currency, price.PPN_BM)
            DbCommandWrapper.AddInParameter("@PPN", DbType.Currency, price.PPN)
            DbCommandWrapper.AddInParameter("@PPh22", DbType.Currency, price.PPh22)
            DbCommandWrapper.AddInParameter("@Interest", DbType.Currency, price.Interest)
            DBCommandWrapper.AddInParameter("@FactoringInt", DbType.Currency, price.FactoringInt)
            DBCommandWrapper.AddInParameter("@PPh23", DbType.Currency, price.PPh23)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, price.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, price.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, price.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@VechileColorID", DbType.Int32, IIf(price.VechileColor Is Nothing, CType(DBNull.Value, Object), CType(price.VechileColor.ID, Object)))

            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, IIf(price.Dealer Is Nothing, CType(DBNull.Value, Object), CType(price.Dealer.ID, Object)))

            '' CR Sirkular Rewards
            '' by : ali Akbar
            '' 2014-09-24
            DBCommandWrapper.AddInParameter("@DiscountReward", DbType.Currency, price.DiscountReward)
            '' END OF CR Sirkular Rewards
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_Price

            Dim price As V_Price = New V_Price

            price.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then price.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BasePrice")) Then price.BasePrice = CType(dr("BasePrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("OptionPrice")) Then price.OptionPrice = CType(dr("OptionPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPN_BM")) Then price.PPN_BM = CType(dr("PPN_BM"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPN")) Then price.PPN = CType(dr("PPN"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPh22")) Then price.PPh22 = CType(dr("PPh22"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Interest")) Then price.Interest = CType(dr("Interest"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("FactoringInt")) Then price.FactoringInt = CType(dr("FactoringInt"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPh23")) Then price.PPh23 = CType(dr("PPh23"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then price.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then price.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then price.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then price.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then price.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then price.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorID")) Then
                price.VechileColor = New VechileColor(CType(dr("VechileColorID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                price.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            '' CR Sirkular Rewards
            '' by : ali Akbar
            '' 2014-09-24
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountReward")) Then price.DiscountReward = CType(dr("DiscountReward"), Decimal)
            '' END OF CR Sirkular Rewards

            If Not dr.IsDBNull(dr.GetOrdinal("MaterialNumber")) Then price.MaterialNumber = CType(dr("MaterialNumber"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then price.DealerCode = CType(dr("DealerCode"), String)
            Return price

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_Price) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_Price), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_Price).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

