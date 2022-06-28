
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_Price Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2014 
'// ---------------------
'// $History      : $
'// Generated on 10/23/2014 - 5:47:31 PM
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

    Public Class sp_PriceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "SP_InsertPrice" '"up_Insertsp_Price"
        Private m_UpdateStatement As String = "up_Updatesp_Price"
        Private m_RetrieveStatement As String = "up_Retrievesp_Price"
        Private m_RetrieveListStatement As String = "up_Retrievesp_PriceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletesp_Price"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sp_Price As sp_Price = Nothing
            While dr.Read

                sp_Price = Me.CreateObject(dr)

            End While

            Return sp_Price

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sp_PriceList As ArrayList = New ArrayList

            While dr.Read
                Dim sp_Price As sp_Price = Me.CreateObject(dr)
                sp_PriceList.Add(sp_Price)
            End While

            Return sp_PriceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_Price As sp_Price = CType(obj, sp_Price)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_Price.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_Price As sp_Price = CType(obj, sp_Price)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DBCommandWrapper.AddInParameter("@VechileColorID", DbType.Int32, sp_Price.VechileColorID)
            DBCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, sp_Price.DealerCode)
            DBCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, sp_Price.ValidFrom)
            DBCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, sp_Price.BasePrice)
            DBCommandWrapper.AddInParameter("@OptionPrice", DbType.Currency, sp_Price.OptionPrice)
            DBCommandWrapper.AddInParameter("@PPN_BM", DbType.Currency, sp_Price.PPN_BM)
            DBCommandWrapper.AddInParameter("@PPN", DbType.Currency, sp_Price.PPN)
            DBCommandWrapper.AddInParameter("@PPh22", DbType.Currency, sp_Price.PPh22)
            DBCommandWrapper.AddInParameter("@Interest", DbType.Currency, sp_Price.Interest)
            DBCommandWrapper.AddInParameter("@FactoringInt", DbType.Currency, sp_Price.FactoringInt)
            DBCommandWrapper.AddInParameter("@PPh23", DbType.Currency, sp_Price.PPh23)
            DBCommandWrapper.AddInParameter("@Status", DbType.AnsiString, sp_Price.Status)
            DBCommandWrapper.AddInParameter("@DiscountReward", DbType.Currency, sp_Price.DiscountReward)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sp_Price.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sp_Price.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_Price As sp_Price = CType(obj, sp_Price)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_Price.ID)
            DBCommandWrapper.AddInParameter("@VechileColorID", DbType.Int32, sp_Price.VechileColorID)
            DBCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, sp_Price.DealerCode)
            DBCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, sp_Price.ValidFrom)
            DBCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, sp_Price.BasePrice)
            DBCommandWrapper.AddInParameter("@OptionPrice", DbType.Currency, sp_Price.OptionPrice)
            DBCommandWrapper.AddInParameter("@PPN_BM", DbType.Currency, sp_Price.PPN_BM)
            DBCommandWrapper.AddInParameter("@PPN", DbType.Currency, sp_Price.PPN)
            DBCommandWrapper.AddInParameter("@PPh22", DbType.Currency, sp_Price.PPh22)
            DBCommandWrapper.AddInParameter("@Interest", DbType.Currency, sp_Price.Interest)
            DBCommandWrapper.AddInParameter("@FactoringInt", DbType.Currency, sp_Price.FactoringInt)
            DBCommandWrapper.AddInParameter("@PPh23", DbType.Currency, sp_Price.PPh23)
            DBCommandWrapper.AddInParameter("@Status", DbType.AnsiString, sp_Price.Status)
            DBCommandWrapper.AddInParameter("@DiscountReward", DbType.Currency, sp_Price.DiscountReward)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sp_Price.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sp_Price.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As sp_Price

            Dim sp_Price As sp_Price = New sp_Price

            sp_Price.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorID")) Then sp_Price.VechileColorID = CType(dr("VechileColorID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then sp_Price.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then sp_Price.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BasePrice")) Then sp_Price.BasePrice = CType(dr("BasePrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("OptionPrice")) Then sp_Price.OptionPrice = CType(dr("OptionPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPN_BM")) Then sp_Price.PPN_BM = CType(dr("PPN_BM"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPN")) Then sp_Price.PPN = CType(dr("PPN"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPh22")) Then sp_Price.PPh22 = CType(dr("PPh22"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Interest")) Then sp_Price.Interest = CType(dr("Interest"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("FactoringInt")) Then sp_Price.FactoringInt = CType(dr("FactoringInt"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPh23")) Then sp_Price.PPh23 = CType(dr("PPh23"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sp_Price.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountReward")) Then sp_Price.DiscountReward = CType(dr("DiscountReward"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sp_Price.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sp_Price.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sp_Price.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sp_Price.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sp_Price.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sp_Price

        End Function

        Private Sub SetTableName()

            If Not (GetType(sp_Price) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(sp_Price), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(sp_Price).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

