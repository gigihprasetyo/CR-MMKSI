#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : DealerVehiclePriceDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2021 - 3:28:54 PM
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

    Public Class DealerVehiclePriceDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerVehiclePriceDetail"
        Private m_UpdateStatement As String = "up_UpdateDealerVehiclePriceDetail"
        Private m_RetrieveStatement As String = "up_RetrieveDealerVehiclePriceDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerVehiclePriceDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerVehiclePriceDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerVehiclePriceDetail As DealerVehiclePriceDetail = Nothing
            While dr.Read

                dealerVehiclePriceDetail = Me.CreateObject(dr)

            End While

            Return dealerVehiclePriceDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerVehiclePriceDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerVehiclePriceDetail As DealerVehiclePriceDetail = Me.CreateObject(dr)
                dealerVehiclePriceDetailList.Add(dealerVehiclePriceDetail)
            End While

            Return dealerVehiclePriceDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerVehiclePriceDetail As DealerVehiclePriceDetail = CType(obj, DealerVehiclePriceDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerVehiclePriceDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerVehiclePriceDetail As DealerVehiclePriceDetail = CType(obj, DealerVehiclePriceDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerVehiclePriceGUID", DbType.AnsiString, dealerVehiclePriceDetail.DealerVehiclePriceGUID)
            DbCommandWrapper.AddInParameter("@GUID", DbType.AnsiString, dealerVehiclePriceDetail.GUID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, dealerVehiclePriceDetail.Name)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, dealerVehiclePriceDetail.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@VechileColorCode", DbType.AnsiString, dealerVehiclePriceDetail.VechileColorCode)
            DbCommandWrapper.AddInParameter("@OTR", DbType.Currency, dealerVehiclePriceDetail.OTR)
            DbCommandWrapper.AddInParameter("@RegistrationFee", DbType.Currency, dealerVehiclePriceDetail.RegistrationFee)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1", DbType.AnsiString, dealerVehiclePriceDetail.ConsumptionTax1)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2", DbType.AnsiString, dealerVehiclePriceDetail.ConsumptionTax2)
            DbCommandWrapper.AddInParameter("@OffTR", DbType.Currency, dealerVehiclePriceDetail.OffTR)
            DbCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, dealerVehiclePriceDetail.BasePrice)
            DbCommandWrapper.AddInParameter("@ConsumptionTaxAmount1", DbType.Currency, dealerVehiclePriceDetail.ConsumptionTaxAmount1)
            DbCommandWrapper.AddInParameter("@ConsumptionTaxAmount2", DbType.Currency, dealerVehiclePriceDetail.ConsumptionTaxAmount2)
            DbCommandWrapper.AddInParameter("@SpecialColorPrice", DbType.Currency, dealerVehiclePriceDetail.SpecialColorPrice)
            DbCommandWrapper.AddInParameter("@BookingFee", DbType.Currency, dealerVehiclePriceDetail.BookingFee)
            DbCommandWrapper.AddInParameter("@LastUpdateTimeinDMS", DbType.DateTime, dealerVehiclePriceDetail.LastUpdateTimeinDMS)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerVehiclePriceDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerVehiclePriceDetail.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, dealerVehiclePriceDetail.LastUpdateTime)


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

            Dim dealerVehiclePriceDetail As DealerVehiclePriceDetail = CType(obj, DealerVehiclePriceDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerVehiclePriceDetail.ID)
            DbCommandWrapper.AddInParameter("@DealerVehiclePriceGUID", DbType.AnsiString, dealerVehiclePriceDetail.DealerVehiclePriceGUID)
            DbCommandWrapper.AddInParameter("@GUID", DbType.AnsiString, dealerVehiclePriceDetail.GUID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, dealerVehiclePriceDetail.Name)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, dealerVehiclePriceDetail.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@VechileColorCode", DbType.AnsiString, dealerVehiclePriceDetail.VechileColorCode)
            DbCommandWrapper.AddInParameter("@OTR", DbType.Currency, dealerVehiclePriceDetail.OTR)
            DbCommandWrapper.AddInParameter("@RegistrationFee", DbType.Currency, dealerVehiclePriceDetail.RegistrationFee)
            DbCommandWrapper.AddInParameter("@ConsumptionTax1", DbType.AnsiString, dealerVehiclePriceDetail.ConsumptionTax1)
            DbCommandWrapper.AddInParameter("@ConsumptionTax2", DbType.AnsiString, dealerVehiclePriceDetail.ConsumptionTax2)
            DbCommandWrapper.AddInParameter("@OffTR", DbType.Currency, dealerVehiclePriceDetail.OffTR)
            DbCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, dealerVehiclePriceDetail.BasePrice)
            DbCommandWrapper.AddInParameter("@ConsumptionTaxAmount1", DbType.Currency, dealerVehiclePriceDetail.ConsumptionTaxAmount1)
            DbCommandWrapper.AddInParameter("@ConsumptionTaxAmount2", DbType.Currency, dealerVehiclePriceDetail.ConsumptionTaxAmount2)
            DbCommandWrapper.AddInParameter("@SpecialColorPrice", DbType.Currency, dealerVehiclePriceDetail.SpecialColorPrice)
            DbCommandWrapper.AddInParameter("@BookingFee", DbType.Currency, dealerVehiclePriceDetail.BookingFee)
            DbCommandWrapper.AddInParameter("@LastUpdateTimeinDMS", DbType.DateTime, dealerVehiclePriceDetail.LastUpdateTimeinDMS)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerVehiclePriceDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerVehiclePriceDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerVehiclePriceDetail.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, dealerVehiclePriceDetail.LastUpdateTime)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerVehiclePriceDetail

            Dim dealerVehiclePriceDetail As DealerVehiclePriceDetail = New DealerVehiclePriceDetail

            dealerVehiclePriceDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerVehiclePriceGUID")) Then dealerVehiclePriceDetail.DealerVehiclePriceGUID = dr("DealerVehiclePriceGUID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GUID")) Then dealerVehiclePriceDetail.GUID = dr("GUID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then dealerVehiclePriceDetail.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeCode")) Then dealerVehiclePriceDetail.VechileTypeCode = dr("VechileTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorCode")) Then dealerVehiclePriceDetail.VechileColorCode = dr("VechileColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OTR")) Then dealerVehiclePriceDetail.OTR = CType(dr("OTR"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RegistrationFee")) Then dealerVehiclePriceDetail.RegistrationFee = CType(dr("RegistrationFee"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax1")) Then dealerVehiclePriceDetail.ConsumptionTax1 = dr("ConsumptionTax1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTax2")) Then dealerVehiclePriceDetail.ConsumptionTax2 = dr("ConsumptionTax2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OffTR")) Then dealerVehiclePriceDetail.OffTR = CType(dr("OffTR"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BasePrice")) Then dealerVehiclePriceDetail.BasePrice = CType(dr("BasePrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTaxAmount1")) Then dealerVehiclePriceDetail.ConsumptionTaxAmount1 = CType(dr("ConsumptionTaxAmount1"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ConsumptionTaxAmount2")) Then dealerVehiclePriceDetail.ConsumptionTaxAmount2 = CType(dr("ConsumptionTaxAmount2"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SpecialColorPrice")) Then dealerVehiclePriceDetail.SpecialColorPrice = CType(dr("SpecialColorPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BookingFee")) Then dealerVehiclePriceDetail.BookingFee = CType(dr("BookingFee"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTimeinDMS")) Then dealerVehiclePriceDetail.LastUpdateTimeinDMS = CType(dr("LastUpdateTimeinDMS"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerVehiclePriceDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerVehiclePriceDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerVehiclePriceDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerVehiclePriceDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerVehiclePriceDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return dealerVehiclePriceDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerVehiclePriceDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerVehiclePriceDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerVehiclePriceDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
