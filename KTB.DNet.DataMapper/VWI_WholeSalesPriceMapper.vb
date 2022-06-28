
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WholeSalesPrice Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 06/06/2018 - 13:48:46
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

    Public Class WholeSalesPriceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWholeSalesPrice"
        Private m_UpdateStatement As String = "up_UpdateWholeSalesPrice"
        Private m_RetrieveStatement As String = "up_RetrieveWholeSalesPrice"
        Private m_RetrieveListStatement As String = "up_RetrieveWholeSalesPriceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWholeSalesPrice"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim wholeSalesPrice As VWI_WholeSalesPrice = Nothing
            While dr.Read

                wholeSalesPrice = Me.CreateObject(dr)

            End While

            Return wholeSalesPrice

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim wholeSalesPriceList As ArrayList = New ArrayList

            While dr.Read
                Dim wholeSalesPrice As VWI_WholeSalesPrice = Me.CreateObject(dr)
                wholeSalesPriceList.Add(wholeSalesPrice)
            End While

            Return wholeSalesPriceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wholeSalesPrice As VWI_WholeSalesPrice = CType(obj, VWI_WholeSalesPrice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wholeSalesPrice.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wholeSalesPrice As VWI_WholeSalesPrice = CType(obj, VWI_WholeSalesPrice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, wholeSalesPrice.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, wholeSalesPrice.DealerCode)
            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int16, wholeSalesPrice.VehicleColorID)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, wholeSalesPrice.MaterialNumber)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, wholeSalesPrice.MaterialDescription)
            DbCommandWrapper.AddInParameter("@VehicleColorCode", DbType.AnsiString, wholeSalesPrice.VehicleColorCode)
            DbCommandWrapper.AddInParameter("@VehicleColorName", DbType.AnsiString, wholeSalesPrice.VehicleColorName)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, wholeSalesPrice.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, wholeSalesPrice.VehicleTypeDesc)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, wholeSalesPrice.ValidFrom)
            DbCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, wholeSalesPrice.BasePrice)
            DbCommandWrapper.AddInParameter("@OptionPrice", DbType.Currency, wholeSalesPrice.OptionPrice)
            DbCommandWrapper.AddInParameter("@PPN_BM", DbType.Currency, wholeSalesPrice.PPN_BM)
            DbCommandWrapper.AddInParameter("@PPN", DbType.Currency, wholeSalesPrice.PPN)
            DbCommandWrapper.AddInParameter("@PPh22", DbType.Currency, wholeSalesPrice.PPh22)
            DbCommandWrapper.AddInParameter("@PPh23", DbType.Currency, wholeSalesPrice.PPh23)
            DbCommandWrapper.AddInParameter("@FactoringInt", DbType.Currency, wholeSalesPrice.FactoringInt)
            DbCommandWrapper.AddInParameter("@DiscountReward", DbType.Currency, wholeSalesPrice.DiscountReward)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim wholeSalesPrice As VWI_WholeSalesPrice = CType(obj, VWI_WholeSalesPrice)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wholeSalesPrice.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, wholeSalesPrice.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, wholeSalesPrice.DealerCode)
            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int16, wholeSalesPrice.VehicleColorID)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, wholeSalesPrice.MaterialNumber)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, wholeSalesPrice.MaterialDescription)
            DbCommandWrapper.AddInParameter("@VehicleColorCode", DbType.AnsiString, wholeSalesPrice.VehicleColorCode)
            DbCommandWrapper.AddInParameter("@VehicleColorName", DbType.AnsiString, wholeSalesPrice.VehicleColorName)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, wholeSalesPrice.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, wholeSalesPrice.VehicleTypeDesc)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, wholeSalesPrice.ValidFrom)
            DbCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, wholeSalesPrice.BasePrice)
            DbCommandWrapper.AddInParameter("@OptionPrice", DbType.Currency, wholeSalesPrice.OptionPrice)
            DbCommandWrapper.AddInParameter("@PPN_BM", DbType.Currency, wholeSalesPrice.PPN_BM)
            DbCommandWrapper.AddInParameter("@PPN", DbType.Currency, wholeSalesPrice.PPN)
            DbCommandWrapper.AddInParameter("@PPh22", DbType.Currency, wholeSalesPrice.PPh22)
            DbCommandWrapper.AddInParameter("@PPh23", DbType.Currency, wholeSalesPrice.PPh23)
            DbCommandWrapper.AddInParameter("@FactoringInt", DbType.Currency, wholeSalesPrice.FactoringInt)
            DbCommandWrapper.AddInParameter("@DiscountReward", DbType.Currency, wholeSalesPrice.DiscountReward)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_WholeSalesPrice

            Dim wholeSalesPrice As VWI_WholeSalesPrice = New VWI_WholeSalesPrice

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then wholeSalesPrice.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then wholeSalesPrice.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorID")) Then wholeSalesPrice.VehicleColorID = CType(dr("VehicleColorID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialNumber")) Then wholeSalesPrice.MaterialNumber = dr("MaterialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialDescription")) Then wholeSalesPrice.MaterialDescription = dr("MaterialDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorCode")) Then wholeSalesPrice.VehicleColorCode = dr("VehicleColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorName")) Then wholeSalesPrice.VehicleColorName = dr("VehicleColorName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then wholeSalesPrice.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeDesc")) Then wholeSalesPrice.VehicleTypeDesc = dr("VehicleTypeDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then wholeSalesPrice.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BasePrice")) Then wholeSalesPrice.BasePrice = CType(dr("BasePrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("OptionPrice")) Then wholeSalesPrice.OptionPrice = CType(dr("OptionPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPN_BM")) Then wholeSalesPrice.PPN_BM = CType(dr("PPN_BM"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPN")) Then wholeSalesPrice.PPN = CType(dr("PPN"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPh22")) Then wholeSalesPrice.PPh22 = CType(dr("PPh22"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPh23")) Then wholeSalesPrice.PPh23 = CType(dr("PPh23"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("FactoringInt")) Then wholeSalesPrice.FactoringInt = CType(dr("FactoringInt"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountReward")) Then wholeSalesPrice.DiscountReward = CType(dr("DiscountReward"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then wholeSalesPrice.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return wholeSalesPrice

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_WholeSalesPrice) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_WholeSalesPrice), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_WholeSalesPrice).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

