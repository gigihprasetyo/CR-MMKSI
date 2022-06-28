
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SPKDetailInfo Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 6/7/2012 - 5:29:14 PM
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

    Public Class V_SPKDetailInfoMapper
        Inherits AbstractMapper


#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_SPKDetailInfo"
        Private m_UpdateStatement As String = "up_UpdateV_SPKDetailInfo"
        Private m_RetrieveStatement As String = "up_RetrieveV_SPKDetailInfo"
        Private m_RetrieveListStatement As String = "up_RetrieveV_SPKDetailInfoList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_SPKDetailInfo"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_SPKDetailInfo As V_SPKDetailInfo = Nothing
            While dr.Read

                v_SPKDetailInfo = Me.CreateObject(dr)

            End While

            Return v_SPKDetailInfo

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_SPKDetailInfoList As ArrayList = New ArrayList

            While dr.Read
                Dim v_SPKDetailInfo As V_SPKDetailInfo = Me.CreateObject(dr)
                v_SPKDetailInfoList.Add(v_SPKDetailInfo)
            End While

            Return v_SPKDetailInfoList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SPKDetailInfo As V_SPKDetailInfo = CType(obj, V_SPKDetailInfo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@SPKDetailID", DbType.Int32, v_SPKDetailInfo.SPKDetailID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SPKDetailInfo As V_SPKDetailInfo = CType(obj, V_SPKDetailInfo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@SPKDetailID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@LineItem", DbType.Int16, v_SPKDetailInfo.LineItem)
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, v_SPKDetailInfo.CategoryID)
            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int16, v_SPKDetailInfo.VehicleColorID)
            DbCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, v_SPKDetailInfo.VehicleKindID)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, v_SPKDetailInfo.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleColorCode", DbType.AnsiString, v_SPKDetailInfo.VehicleColorCode)
            DbCommandWrapper.AddInParameter("@VehicleColorName", DbType.AnsiString, v_SPKDetailInfo.VehicleColorName)
            DbCommandWrapper.AddInParameter("@ProfileDetailID", DbType.Int32, v_SPKDetailInfo.ProfileDetailID)
            DbCommandWrapper.AddInParameter("@ProfileDescription", DbType.AnsiString, v_SPKDetailInfo.ProfileDescription)
            DbCommandWrapper.AddInParameter("@Additional", DbType.Byte, v_SPKDetailInfo.Additional)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.String, v_SPKDetailInfo.Remarks)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, v_SPKDetailInfo.Quantity)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, v_SPKDetailInfo.Amount)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, v_SPKDetailInfo.TotalAmount)
            DbCommandWrapper.AddInParameter("@RejectedReasonDetail", DbType.String, v_SPKDetailInfo.RejectedReasonDetail)
            DbCommandWrapper.AddInParameter("@DetailStatus", DbType.Byte, v_SPKDetailInfo.DetailStatus)
            DbCommandWrapper.AddInParameter("@SPKHeaderID", DbType.Int32, v_SPKDetailInfo.SPKHeaderID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_SPKDetailInfo.DealerID)
            DbCommandWrapper.AddInParameter("@HeaderStatus", DbType.AnsiString, v_SPKDetailInfo.HeaderStatus)
            DBCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, v_SPKDetailInfo.SPKNumber)
            DBCommandWrapper.AddInParameter("@IndentNumber", DbType.AnsiString, v_SPKDetailInfo.IndentNumber)
            DbCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, v_SPKDetailInfo.DealerSPKNumber)
            DbCommandWrapper.AddInParameter("@PlanDeliveryMonth", DbType.Byte, v_SPKDetailInfo.PlanDeliveryMonth)
            DbCommandWrapper.AddInParameter("@PlanDeliveryYear", DbType.Int16, v_SPKDetailInfo.PlanDeliveryYear)
            DbCommandWrapper.AddInParameter("@PlanDeliveryDate", DbType.DateTime, v_SPKDetailInfo.PlanDeliveryDate)
            DbCommandWrapper.AddInParameter("@PlanInvoiceMonth", DbType.Byte, v_SPKDetailInfo.PlanInvoiceMonth)
            DbCommandWrapper.AddInParameter("@PlanInvoiceYear", DbType.Int16, v_SPKDetailInfo.PlanInvoiceYear)
            DbCommandWrapper.AddInParameter("@PlanInvoiceDate", DbType.DateTime, v_SPKDetailInfo.PlanInvoiceDate)
            DbCommandWrapper.AddInParameter("@CustomerRequestID", DbType.Int32, v_SPKDetailInfo.CustomerRequestID)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, v_SPKDetailInfo.ValidateTime)
            DbCommandWrapper.AddInParameter("@ValidateBy", DbType.String, v_SPKDetailInfo.ValidateBy)
            DbCommandWrapper.AddInParameter("@RejectedReasonHeader", DbType.String, v_SPKDetailInfo.RejectedReasonHeader)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, v_SPKDetailInfo.SalesmanHeaderID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_SPKDetailInfo.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, v_SPKDetailInfo.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, v_SPKDetailInfo.CustomerID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, v_SPKDetailInfo.Code)
            DbCommandWrapper.AddInParameter("@ReffCode", DbType.AnsiString, v_SPKDetailInfo.ReffCode)
            DbCommandWrapper.AddInParameter("@TipeCustomer", DbType.Int16, v_SPKDetailInfo.TipeCustomer)
            DbCommandWrapper.AddInParameter("@TipePerusahaan", DbType.Int16, v_SPKDetailInfo.TipePerusahaan)
            DbCommandWrapper.AddInParameter("@Name1", DbType.String, v_SPKDetailInfo.Name1)
            DbCommandWrapper.AddInParameter("@Name2", DbType.String, v_SPKDetailInfo.Name2)
            DbCommandWrapper.AddInParameter("@Name3", DbType.String, v_SPKDetailInfo.Name3)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.String, v_SPKDetailInfo.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.String, v_SPKDetailInfo.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.String, v_SPKDetailInfo.Kecamatan)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.String, v_SPKDetailInfo.PostalCode)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, v_SPKDetailInfo.PreArea)
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, v_SPKDetailInfo.CityID)
            DbCommandWrapper.AddInParameter("@PrintRegion", DbType.AnsiString, v_SPKDetailInfo.PrintRegion)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, v_SPKDetailInfo.PhoneNo)
            DbCommandWrapper.AddInParameter("@OfficeNo", DbType.String, v_SPKDetailInfo.OfficeNo)
            DbCommandWrapper.AddInParameter("@HomeNo", DbType.String, v_SPKDetailInfo.HomeNo)
            DbCommandWrapper.AddInParameter("@HpNo", DbType.AnsiString, v_SPKDetailInfo.HpNo)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, v_SPKDetailInfo.Email)
            DbCommandWrapper.AddInParameter("@CustStatus", DbType.Int32, v_SPKDetailInfo.CustStatus)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SPKDetailInfo.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SPKDetailInfo.DealerName)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, v_SPKDetailInfo.CityName)
            DbCommandWrapper.AddInParameter("@ProvinceID", DbType.Int32, v_SPKDetailInfo.ProvinceID)
            DbCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, v_SPKDetailInfo.ProvinceName)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, v_SPKDetailInfo.CategoryCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, v_SPKDetailInfo.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, v_SPKDetailInfo.Description)
            DbCommandWrapper.AddInParameter("@ColorEngName", DbType.AnsiString, v_SPKDetailInfo.ColorEngName)
            DbCommandWrapper.AddInParameter("@SalesmanId", DbType.Int16, v_SPKDetailInfo.SalesmanId)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, v_SPKDetailInfo.SalesmanCode)
            DbCommandWrapper.AddInParameter("@SalesName", DbType.AnsiString, v_SPKDetailInfo.SalesName)
            DbCommandWrapper.AddInParameter("@SalesPosition", DbType.AnsiString, v_SPKDetailInfo.SalesPosition)
            DbCommandWrapper.AddInParameter("@SalesLevel", DbType.AnsiString, v_SPKDetailInfo.SalesLevel)
            DbCommandWrapper.AddInParameter("@Supervisor", DbType.AnsiString, v_SPKDetailInfo.Supervisor)
            DbCommandWrapper.AddInParameter("@Manager", DbType.AnsiString, v_SPKDetailInfo.Manager)
            DbCommandWrapper.AddInParameter("@CaraPembayaran", DbType.AnsiString, v_SPKDetailInfo.CaraPembayaran)
            DbCommandWrapper.AddInParameter("@KepemilikanKendaraan", DbType.AnsiString, v_SPKDetailInfo.KepemilikanKendaraan)
            DbCommandWrapper.AddInParameter("@KendaraanSebagai", DbType.AnsiString, v_SPKDetailInfo.KendaraanSebagai)
            DbCommandWrapper.AddInParameter("@PenggunaanUtamaKendaraan", DbType.AnsiString, v_SPKDetailInfo.PenggunaanUtamaKendaraan)
            DbCommandWrapper.AddInParameter("@UsiaPemilik", DbType.AnsiString, v_SPKDetailInfo.UsiaPemilik)
            DbCommandWrapper.AddInParameter("@BidangUsaha", DbType.AnsiString, v_SPKDetailInfo.BidangUsaha)
            DbCommandWrapper.AddInParameter("@DaerahUtama", DbType.AnsiString, v_SPKDetailInfo.DaerahUtama)
            DbCommandWrapper.AddInParameter("@BentukBodyCV", DbType.AnsiString, v_SPKDetailInfo.BentukBodyCV)
            DbCommandWrapper.AddInParameter("@BentukBodyLCV", DbType.AnsiString, v_SPKDetailInfo.BentukBodyLCV)
            DbCommandWrapper.AddInParameter("@JenisKendaraan", DbType.AnsiString, v_SPKDetailInfo.JenisKendaraan)
            DbCommandWrapper.AddInParameter("@ModelKendaraan", DbType.AnsiString, v_SPKDetailInfo.ModelKendaraan)
            DbCommandWrapper.AddInParameter("@DealerSPKDate", DbType.DateTime, v_SPKDetailInfo.DealerSPKDate)
            DbCommandWrapper.AddInParameter("@KTP", DbType.AnsiString, v_SPKDetailInfo.KTP)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@SPKDetailID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "SPKDetailID")

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
            DbCommandWrapper.AddInParameter("@SPKDetailID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SPKDetailInfo As V_SPKDetailInfo = CType(obj, V_SPKDetailInfo)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@SPKDetailID", DbType.Int32, v_SPKDetailInfo.SPKDetailID)
            DbCommandWrapper.AddInParameter("@LineItem", DbType.Int16, v_SPKDetailInfo.LineItem)
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, v_SPKDetailInfo.CategoryID)
            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int16, v_SPKDetailInfo.VehicleColorID)
            DbCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, v_SPKDetailInfo.VehicleKindID)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, v_SPKDetailInfo.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleColorCode", DbType.AnsiString, v_SPKDetailInfo.VehicleColorCode)
            DbCommandWrapper.AddInParameter("@VehicleColorName", DbType.AnsiString, v_SPKDetailInfo.VehicleColorName)
            DbCommandWrapper.AddInParameter("@ProfileDetailID", DbType.Int32, v_SPKDetailInfo.ProfileDetailID)
            DbCommandWrapper.AddInParameter("@ProfileDescription", DbType.AnsiString, v_SPKDetailInfo.ProfileDescription)
            DbCommandWrapper.AddInParameter("@Additional", DbType.Byte, v_SPKDetailInfo.Additional)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.String, v_SPKDetailInfo.Remarks)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, v_SPKDetailInfo.Quantity)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, v_SPKDetailInfo.Amount)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, v_SPKDetailInfo.TotalAmount)
            DbCommandWrapper.AddInParameter("@RejectedReasonDetail", DbType.String, v_SPKDetailInfo.RejectedReasonDetail)
            DbCommandWrapper.AddInParameter("@DetailStatus", DbType.Byte, v_SPKDetailInfo.DetailStatus)
            DbCommandWrapper.AddInParameter("@SPKHeaderID", DbType.Int32, v_SPKDetailInfo.SPKHeaderID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_SPKDetailInfo.DealerID)
            DbCommandWrapper.AddInParameter("@HeaderStatus", DbType.AnsiString, v_SPKDetailInfo.HeaderStatus)
            DBCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, v_SPKDetailInfo.SPKNumber)
            DBCommandWrapper.AddInParameter("@IndentNumber", DbType.AnsiString, v_SPKDetailInfo.IndentNumber)
            DbCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, v_SPKDetailInfo.DealerSPKNumber)
            DbCommandWrapper.AddInParameter("@PlanDeliveryMonth", DbType.Byte, v_SPKDetailInfo.PlanDeliveryMonth)
            DbCommandWrapper.AddInParameter("@PlanDeliveryYear", DbType.Int16, v_SPKDetailInfo.PlanDeliveryYear)
            DbCommandWrapper.AddInParameter("@PlanDeliveryDate", DbType.DateTime, v_SPKDetailInfo.PlanDeliveryDate)
            DbCommandWrapper.AddInParameter("@PlanInvoiceMonth", DbType.Byte, v_SPKDetailInfo.PlanInvoiceMonth)
            DbCommandWrapper.AddInParameter("@PlanInvoiceYear", DbType.Int16, v_SPKDetailInfo.PlanInvoiceYear)
            DbCommandWrapper.AddInParameter("@PlanInvoiceDate", DbType.DateTime, v_SPKDetailInfo.PlanInvoiceDate)
            DbCommandWrapper.AddInParameter("@CustomerRequestID", DbType.Int32, v_SPKDetailInfo.CustomerRequestID)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, v_SPKDetailInfo.ValidateTime)
            DbCommandWrapper.AddInParameter("@ValidateBy", DbType.String, v_SPKDetailInfo.ValidateBy)
            DbCommandWrapper.AddInParameter("@RejectedReasonHeader", DbType.String, v_SPKDetailInfo.RejectedReasonHeader)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, v_SPKDetailInfo.SalesmanHeaderID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_SPKDetailInfo.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, v_SPKDetailInfo.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, User)
            DbCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, v_SPKDetailInfo.CustomerID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, v_SPKDetailInfo.Code)
            DbCommandWrapper.AddInParameter("@ReffCode", DbType.AnsiString, v_SPKDetailInfo.ReffCode)
            DbCommandWrapper.AddInParameter("@TipeCustomer", DbType.Int16, v_SPKDetailInfo.TipeCustomer)
            DbCommandWrapper.AddInParameter("@TipePerusahaan", DbType.Int16, v_SPKDetailInfo.TipePerusahaan)
            DbCommandWrapper.AddInParameter("@Name1", DbType.String, v_SPKDetailInfo.Name1)
            DbCommandWrapper.AddInParameter("@Name2", DbType.String, v_SPKDetailInfo.Name2)
            DbCommandWrapper.AddInParameter("@Name3", DbType.String, v_SPKDetailInfo.Name3)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.String, v_SPKDetailInfo.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.String, v_SPKDetailInfo.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.String, v_SPKDetailInfo.Kecamatan)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.String, v_SPKDetailInfo.PostalCode)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, v_SPKDetailInfo.PreArea)
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, v_SPKDetailInfo.CityID)
            DbCommandWrapper.AddInParameter("@PrintRegion", DbType.AnsiString, v_SPKDetailInfo.PrintRegion)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, v_SPKDetailInfo.PhoneNo)
            DbCommandWrapper.AddInParameter("@OfficeNo", DbType.String, v_SPKDetailInfo.OfficeNo)
            DbCommandWrapper.AddInParameter("@HomeNo", DbType.String, v_SPKDetailInfo.HomeNo)
            DbCommandWrapper.AddInParameter("@HpNo", DbType.AnsiString, v_SPKDetailInfo.HpNo)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, v_SPKDetailInfo.Email)
            DbCommandWrapper.AddInParameter("@CustStatus", DbType.Int32, v_SPKDetailInfo.CustStatus)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SPKDetailInfo.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SPKDetailInfo.DealerName)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, v_SPKDetailInfo.CityName)
            DbCommandWrapper.AddInParameter("@ProvinceID", DbType.Int32, v_SPKDetailInfo.ProvinceID)
            DbCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, v_SPKDetailInfo.ProvinceName)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, v_SPKDetailInfo.CategoryCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, v_SPKDetailInfo.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, v_SPKDetailInfo.Description)
            DbCommandWrapper.AddInParameter("@ColorEngName", DbType.AnsiString, v_SPKDetailInfo.ColorEngName)
            DbCommandWrapper.AddInParameter("@SalesmanId", DbType.Int16, v_SPKDetailInfo.SalesmanId)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, v_SPKDetailInfo.SalesmanCode)
            DbCommandWrapper.AddInParameter("@SalesName", DbType.AnsiString, v_SPKDetailInfo.SalesName)
            DbCommandWrapper.AddInParameter("@SalesPosition", DbType.AnsiString, v_SPKDetailInfo.SalesPosition)
            DbCommandWrapper.AddInParameter("@SalesLevel", DbType.AnsiString, v_SPKDetailInfo.SalesLevel)
            DbCommandWrapper.AddInParameter("@Supervisor", DbType.AnsiString, v_SPKDetailInfo.Supervisor)
            DbCommandWrapper.AddInParameter("@Manager", DbType.AnsiString, v_SPKDetailInfo.Manager)
            DbCommandWrapper.AddInParameter("@CaraPembayaran", DbType.AnsiString, v_SPKDetailInfo.CaraPembayaran)
            DbCommandWrapper.AddInParameter("@KepemilikanKendaraan", DbType.AnsiString, v_SPKDetailInfo.KepemilikanKendaraan)
            DbCommandWrapper.AddInParameter("@KendaraanSebagai", DbType.AnsiString, v_SPKDetailInfo.KendaraanSebagai)
            DbCommandWrapper.AddInParameter("@PenggunaanUtamaKendaraan", DbType.AnsiString, v_SPKDetailInfo.PenggunaanUtamaKendaraan)
            DbCommandWrapper.AddInParameter("@UsiaPemilik", DbType.AnsiString, v_SPKDetailInfo.UsiaPemilik)
            DbCommandWrapper.AddInParameter("@BidangUsaha", DbType.AnsiString, v_SPKDetailInfo.BidangUsaha)
            DbCommandWrapper.AddInParameter("@DaerahUtama", DbType.AnsiString, v_SPKDetailInfo.DaerahUtama)
            DbCommandWrapper.AddInParameter("@BentukBodyCV", DbType.AnsiString, v_SPKDetailInfo.BentukBodyCV)
            DbCommandWrapper.AddInParameter("@BentukBodyLCV", DbType.AnsiString, v_SPKDetailInfo.BentukBodyLCV)
            DbCommandWrapper.AddInParameter("@JenisKendaraan", DbType.AnsiString, v_SPKDetailInfo.JenisKendaraan)
            DbCommandWrapper.AddInParameter("@ModelKendaraan", DbType.AnsiString, v_SPKDetailInfo.ModelKendaraan)
            DbCommandWrapper.AddInParameter("@DealerSPKDate", DbType.DateTime, v_SPKDetailInfo.DealerSPKDate)


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_SPKDetailInfo

            Dim v_SPKDetailInfo As V_SPKDetailInfo = New V_SPKDetailInfo

            v_SPKDetailInfo.SPKDetailID = CType(dr("SPKDetailID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LineItem")) Then v_SPKDetailInfo.LineItem = CType(dr("LineItem"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then v_SPKDetailInfo.CategoryID = CType(dr("CategoryID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorID")) Then v_SPKDetailInfo.VehicleColorID = CType(dr("VehicleColorID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleKindID")) Then v_SPKDetailInfo.VehicleKindID = CType(dr("VehicleKindID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then v_SPKDetailInfo.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorCode")) Then v_SPKDetailInfo.VehicleColorCode = dr("VehicleColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorName")) Then v_SPKDetailInfo.VehicleColorName = dr("VehicleColorName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileDetailID")) Then v_SPKDetailInfo.ProfileDetailID = CType(dr("ProfileDetailID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileDescription")) Then v_SPKDetailInfo.ProfileDescription = dr("ProfileDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Additional")) Then v_SPKDetailInfo.Additional = CType(dr("Additional"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Remarks")) Then v_SPKDetailInfo.Remarks = dr("Remarks").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then v_SPKDetailInfo.Quantity = CType(dr("Quantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then v_SPKDetailInfo.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then v_SPKDetailInfo.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RejectedReasonDetail")) Then v_SPKDetailInfo.RejectedReasonDetail = dr("RejectedReasonDetail").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DetailStatus")) Then v_SPKDetailInfo.DetailStatus = CType(dr("DetailStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SPKHeaderID")) Then v_SPKDetailInfo.SPKHeaderID = CType(dr("SPKHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_SPKDetailInfo.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("HeaderStatus")) Then v_SPKDetailInfo.HeaderStatus = dr("HeaderStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) Then v_SPKDetailInfo.SPKNumber = dr("SPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IndentNumber")) Then v_SPKDetailInfo.IndentNumber = dr("IndentNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSPKNumber")) Then v_SPKDetailInfo.DealerSPKNumber = dr("DealerSPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PlanDeliveryMonth")) Then v_SPKDetailInfo.PlanDeliveryMonth = CType(dr("PlanDeliveryMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanDeliveryYear")) Then v_SPKDetailInfo.PlanDeliveryYear = CType(dr("PlanDeliveryYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanDeliveryDate")) Then v_SPKDetailInfo.PlanDeliveryDate = CType(dr("PlanDeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanInvoiceMonth")) Then v_SPKDetailInfo.PlanInvoiceMonth = CType(dr("PlanInvoiceMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanInvoiceYear")) Then v_SPKDetailInfo.PlanInvoiceYear = CType(dr("PlanInvoiceYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanInvoiceDate")) Then v_SPKDetailInfo.PlanInvoiceDate = CType(dr("PlanInvoiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerRequestID")) Then v_SPKDetailInfo.CustomerRequestID = CType(dr("CustomerRequestID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateTime")) Then v_SPKDetailInfo.ValidateTime = CType(dr("ValidateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateBy")) Then v_SPKDetailInfo.ValidateBy = dr("ValidateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RejectedReasonHeader")) Then v_SPKDetailInfo.RejectedReasonHeader = dr("RejectedReasonHeader").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then v_SPKDetailInfo.SalesmanHeaderID = CType(dr("SalesmanHeaderID"), Int32)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_SPKDetailInfo.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_SPKDetailInfo.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_SPKDetailInfo.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_SPKDetailInfo.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_SPKDetailInfo.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerID")) Then v_SPKDetailInfo.CustomerID = CType(dr("CustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then v_SPKDetailInfo.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReffCode")) Then v_SPKDetailInfo.ReffCode = dr("ReffCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TipeCustomer")) Then v_SPKDetailInfo.TipeCustomer = CType(dr("TipeCustomer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TipePerusahaan")) Then v_SPKDetailInfo.TipePerusahaan = CType(dr("TipePerusahaan"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Name1")) Then v_SPKDetailInfo.Name1 = dr("Name1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name2")) Then v_SPKDetailInfo.Name2 = dr("Name2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name3")) Then v_SPKDetailInfo.Name3 = dr("Name3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alamat")) Then v_SPKDetailInfo.Alamat = dr("Alamat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kelurahan")) Then v_SPKDetailInfo.Kelurahan = dr("Kelurahan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kecamatan")) Then v_SPKDetailInfo.Kecamatan = dr("Kecamatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostalCode")) Then v_SPKDetailInfo.PostalCode = dr("PostalCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PreArea")) Then v_SPKDetailInfo.PreArea = dr("PreArea").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then v_SPKDetailInfo.CityID = CType(dr("CityID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PrintRegion")) Then v_SPKDetailInfo.PrintRegion = dr("PrintRegion").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNo")) Then v_SPKDetailInfo.PhoneNo = dr("PhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OfficeNo")) Then v_SPKDetailInfo.OfficeNo = dr("OfficeNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HomeNo")) Then v_SPKDetailInfo.HomeNo = dr("HomeNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HpNo")) Then v_SPKDetailInfo.HpNo = dr("HpNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then v_SPKDetailInfo.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustStatus")) Then v_SPKDetailInfo.CustStatus = CType(dr("CustStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_SPKDetailInfo.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_SPKDetailInfo.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then v_SPKDetailInfo.CityName = dr("CityName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceID")) Then v_SPKDetailInfo.ProvinceID = CType(dr("ProvinceID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceName")) Then v_SPKDetailInfo.ProvinceName = dr("ProvinceName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryCode")) Then v_SPKDetailInfo.CategoryCode = dr("CategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeCode")) Then v_SPKDetailInfo.VechileTypeCode = dr("VechileTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then v_SPKDetailInfo.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorEngName")) Then v_SPKDetailInfo.ColorEngName = dr("ColorEngName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanId")) Then v_SPKDetailInfo.SalesmanId = CType(dr("SalesmanId"), Int32)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then v_SPKDetailInfo.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesName")) Then v_SPKDetailInfo.SalesName = dr("SalesName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesPosition")) Then v_SPKDetailInfo.SalesPosition = dr("SalesPosition").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesLevel")) Then v_SPKDetailInfo.SalesLevel = dr("SalesLevel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Supervisor")) Then v_SPKDetailInfo.Supervisor = dr("Supervisor").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Manager")) Then v_SPKDetailInfo.Manager = dr("Manager").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CaraPembayaran")) Then v_SPKDetailInfo.CaraPembayaran = dr("CaraPembayaran").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KepemilikanKendaraan")) Then v_SPKDetailInfo.KepemilikanKendaraan = dr("KepemilikanKendaraan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KendaraanSebagai")) Then v_SPKDetailInfo.KendaraanSebagai = dr("KendaraanSebagai").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PenggunaanUtamaKendaraan")) Then v_SPKDetailInfo.PenggunaanUtamaKendaraan = dr("PenggunaanUtamaKendaraan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UsiaPemilik")) Then v_SPKDetailInfo.UsiaPemilik = dr("UsiaPemilik").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BidangUsaha")) Then v_SPKDetailInfo.BidangUsaha = dr("BidangUsaha").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DaerahUtama")) Then v_SPKDetailInfo.DaerahUtama = dr("DaerahUtama").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BentukBodyCV")) Then v_SPKDetailInfo.BentukBodyCV = dr("BentukBodyCV").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BentukBodyLCV")) Then v_SPKDetailInfo.BentukBodyLCV = dr("BentukBodyLCV").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JenisKendaraan")) Then v_SPKDetailInfo.JenisKendaraan = dr("JenisKendaraan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ModelKendaraan")) Then v_SPKDetailInfo.ModelKendaraan = dr("ModelKendaraan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KTP")) Then v_SPKDetailInfo.KTP = dr("KTP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSPKDate")) Then v_SPKDetailInfo.DealerSPKDate = CType(dr("DealerSPKDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CityCustomer")) Then v_SPKDetailInfo.CityCustomer = dr("CityCustomer").ToString

            Return v_SPKDetailInfo


        End Function

        Private Sub SetTableName()

            If Not (GetType(V_SPKDetailInfo) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_SPKDetailInfo), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_SPKDetailInfo).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

