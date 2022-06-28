
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Vw_ChassisMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/02/2018 - 3:49:00 PM
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

    Public Class Vw_ChassisMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVw_ChassisMaster"
        Private m_UpdateStatement As String = "up_UpdateVw_ChassisMaster"
        Private m_RetrieveStatement As String = "up_RetrieveVw_ChassisMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveVw_ChassisMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVw_ChassisMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vw_ChassisMaster As Vw_ChassisMaster = Nothing
            While dr.Read

                vw_ChassisMaster = Me.CreateObject(dr)

            End While

            Return vw_ChassisMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vw_ChassisMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim vw_ChassisMaster As Vw_ChassisMaster = Me.CreateObject(dr)
                vw_ChassisMasterList.Add(vw_ChassisMaster)
            End While

            Return vw_ChassisMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vw_ChassisMaster As Vw_ChassisMaster = CType(obj, Vw_ChassisMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vw_ChassisMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vw_ChassisMaster As Vw_ChassisMaster = CType(obj, Vw_ChassisMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, vw_ChassisMaster.EndCustomerID)
            'DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vw_ChassisMaster.ChassisNumber)
            'DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, vw_ChassisMaster.CategoryID)
            'DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int16, vw_ChassisMaster.VechileColorID)
            'DbCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, vw_ChassisMaster.VehicleKindID)
            'DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int16, vw_ChassisMaster.SoldDealerID)
            'DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, vw_ChassisMaster.DONumber)
            'DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, vw_ChassisMaster.SONumber)
            'DbCommandWrapper.AddInParameter("@TOPID", DbType.Int32, vw_ChassisMaster.TOPID)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, vw_ChassisMaster.DiscountAmount)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, vw_ChassisMaster.PONumber)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, vw_ChassisMaster.EngineNumber)
            DbCommandWrapper.AddInParameter("@SerialNumber", DbType.AnsiString, vw_ChassisMaster.SerialNumber)
            DbCommandWrapper.AddInParameter("@DODate", DbType.DateTime, vw_ChassisMaster.DODate)
            DbCommandWrapper.AddInParameter("@GIDate", DbType.DateTime, vw_ChassisMaster.GIDate)
            DbCommandWrapper.AddInParameter("@ParkingDays", DbType.Int32, vw_ChassisMaster.ParkingDays)
            DbCommandWrapper.AddInParameter("@ParkingAmount", DbType.Currency, vw_ChassisMaster.ParkingAmount)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, vw_ChassisMaster.FakturStatus)
            DbCommandWrapper.AddInParameter("@PendingDesc", DbType.AnsiString, vw_ChassisMaster.PendingDesc)
            DbCommandWrapper.AddInParameter("@IsSAPDownload", DbType.AnsiString, vw_ChassisMaster.IsSAPDownload)
            DbCommandWrapper.AddInParameter("@StockDealer", DbType.Int16, vw_ChassisMaster.StockDealer)
            DbCommandWrapper.AddInParameter("@StockDate", DbType.DateTime, vw_ChassisMaster.StockDate)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, vw_ChassisMaster.ProductionYear)
            DbCommandWrapper.AddInParameter("@StockStatus", DbType.AnsiString, vw_ChassisMaster.StockStatus)
            DbCommandWrapper.AddInParameter("@LastUpdateProfile", DbType.DateTime, vw_ChassisMaster.LastUpdateProfile)
            DbCommandWrapper.AddInParameter("@AlreadySaled", DbType.Byte, vw_ChassisMaster.AlreadySaled)
            DbCommandWrapper.AddInParameter("@AlreadySaledTime", DbType.DateTime, vw_ChassisMaster.AlreadySaledTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vw_ChassisMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vw_ChassisMaster.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@ChassisMasterDocumentID", DbType.Int32, vw_ChassisMaster.ChassisMasterDocumentID)
            DbCommandWrapper.AddInParameter("@EngineImagePath", DbType.AnsiString, vw_ChassisMaster.EngineImagePath)
            DbCommandWrapper.AddInParameter("@ChassisImagePath", DbType.AnsiString, vw_ChassisMaster.ChassisImagePath)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, vw_ChassisMaster.UploadDate)
            DbCommandWrapper.AddInParameter("@ChassisMasterDocumentStatus", DbType.Int16, vw_ChassisMaster.ChassisMasterDocumentStatus)


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

            Dim vw_ChassisMaster As Vw_ChassisMaster = CType(obj, Vw_ChassisMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vw_ChassisMaster.ID)
            'DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, vw_ChassisMaster.EndCustomerID)
            'DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vw_ChassisMaster.ChassisNumber)
            'DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, vw_ChassisMaster.CategoryID)
            'DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int16, vw_ChassisMaster.VechileColorID)
            'DbCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, vw_ChassisMaster.VehicleKindID)
            'DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int16, vw_ChassisMaster.SoldDealerID)
            'DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, vw_ChassisMaster.DONumber)
            'DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, vw_ChassisMaster.SONumber)
            'DbCommandWrapper.AddInParameter("@TOPID", DbType.Int32, vw_ChassisMaster.TOPID)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, vw_ChassisMaster.DiscountAmount)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, vw_ChassisMaster.PONumber)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, vw_ChassisMaster.EngineNumber)
            DbCommandWrapper.AddInParameter("@SerialNumber", DbType.AnsiString, vw_ChassisMaster.SerialNumber)
            DbCommandWrapper.AddInParameter("@DODate", DbType.DateTime, vw_ChassisMaster.DODate)
            DbCommandWrapper.AddInParameter("@GIDate", DbType.DateTime, vw_ChassisMaster.GIDate)
            DbCommandWrapper.AddInParameter("@ParkingDays", DbType.Int32, vw_ChassisMaster.ParkingDays)
            DbCommandWrapper.AddInParameter("@ParkingAmount", DbType.Currency, vw_ChassisMaster.ParkingAmount)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, vw_ChassisMaster.FakturStatus)
            DbCommandWrapper.AddInParameter("@PendingDesc", DbType.AnsiString, vw_ChassisMaster.PendingDesc)
            DbCommandWrapper.AddInParameter("@IsSAPDownload", DbType.AnsiString, vw_ChassisMaster.IsSAPDownload)
            DbCommandWrapper.AddInParameter("@StockDealer", DbType.Int16, vw_ChassisMaster.StockDealer)
            DbCommandWrapper.AddInParameter("@StockDate", DbType.DateTime, vw_ChassisMaster.StockDate)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, vw_ChassisMaster.ProductionYear)
            DbCommandWrapper.AddInParameter("@StockStatus", DbType.AnsiString, vw_ChassisMaster.StockStatus)
            DbCommandWrapper.AddInParameter("@LastUpdateProfile", DbType.DateTime, vw_ChassisMaster.LastUpdateProfile)
            DbCommandWrapper.AddInParameter("@AlreadySaled", DbType.Byte, vw_ChassisMaster.AlreadySaled)
            DbCommandWrapper.AddInParameter("@AlreadySaledTime", DbType.DateTime, vw_ChassisMaster.AlreadySaledTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vw_ChassisMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vw_ChassisMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@ChassisMasterDocumentID", DbType.Int32, vw_ChassisMaster.ChassisMasterDocumentID)
            DbCommandWrapper.AddInParameter("@EngineImagePath", DbType.AnsiString, vw_ChassisMaster.EngineImagePath)
            DbCommandWrapper.AddInParameter("@ChassisImagePath", DbType.AnsiString, vw_ChassisMaster.ChassisImagePath)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, vw_ChassisMaster.UploadDate)
            DbCommandWrapper.AddInParameter("@ChassisMasterDocumentStatus", DbType.Int16, vw_ChassisMaster.ChassisMasterDocumentStatus)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Vw_ChassisMaster

            Dim vw_ChassisMaster As Vw_ChassisMaster = New Vw_ChassisMaster

            vw_ChassisMaster.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerID")) Then
                vw_ChassisMaster.EndCustomer = New EndCustomer(CType(dr("EndCustomerID"), Integer))
            End If

            'If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerID")) Then vw_ChassisMaster.EndCustomerID = CType(dr("EndCustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then vw_ChassisMaster.ChassisNumber = dr("ChassisNumber").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                vw_ChassisMaster.Category = New Category(CType(dr("CategoryID"), Integer))
            End If

            'If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then vw_ChassisMaster.CategoryID = CType(dr("CategoryID"), Byte)
            'If Not dr.IsDBNull(dr.GetOrdinal("VechileColorID")) Then vw_ChassisMaster.VechileColorID = CType(dr("VechileColorID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorID")) Then
                vw_ChassisMaster.VechileColor = New VechileColor(CType(dr("VechileColorID"), Integer))
            End If
            'If Not dr.IsDBNull(dr.GetOrdinal("VehicleKindID")) Then vw_ChassisMaster.VehicleKindID = CType(dr("VehicleKindID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleKindID")) Then
                vw_ChassisMaster.VehicleKind = New VehicleKind(CType(dr("VehicleKindID"), Integer))
            End If

            'If Not dr.IsDBNull(dr.GetOrdinal("SoldDealerID")) Then vw_ChassisMaster.SoldDealerID = CType(dr("SoldDealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDealerID")) Then
                vw_ChassisMaster.Dealer = New Dealer(CType(dr("SoldDealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then vw_ChassisMaster.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then vw_ChassisMaster.SONumber = dr("SONumber").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("TOPID")) Then vw_ChassisMaster.TOPID = CType(dr("TOPID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TOPID")) Then
                vw_ChassisMaster.TermOfPayment = New TermOfPayment(CType(dr("TOPID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountAmount")) Then vw_ChassisMaster.DiscountAmount = CType(dr("DiscountAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then vw_ChassisMaster.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EngineNumber")) Then vw_ChassisMaster.EngineNumber = dr("EngineNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SerialNumber")) Then vw_ChassisMaster.SerialNumber = dr("SerialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DODate")) Then vw_ChassisMaster.DODate = CType(dr("DODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GIDate")) Then vw_ChassisMaster.GIDate = CType(dr("GIDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ParkingDays")) Then vw_ChassisMaster.ParkingDays = CType(dr("ParkingDays"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ParkingAmount")) Then vw_ChassisMaster.ParkingAmount = CType(dr("ParkingAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturStatus")) Then vw_ChassisMaster.FakturStatus = dr("FakturStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PendingDesc")) Then vw_ChassisMaster.PendingDesc = dr("PendingDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsSAPDownload")) Then vw_ChassisMaster.IsSAPDownload = dr("IsSAPDownload").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StockDealer")) Then vw_ChassisMaster.StockDealer = CType(dr("StockDealer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StockDate")) Then vw_ChassisMaster.StockDate = CType(dr("StockDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then vw_ChassisMaster.ProductionYear = CType(dr("ProductionYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StockStatus")) Then vw_ChassisMaster.StockStatus = dr("StockStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateProfile")) Then vw_ChassisMaster.LastUpdateProfile = CType(dr("LastUpdateProfile"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AlreadySaled")) Then vw_ChassisMaster.AlreadySaled = CType(dr("AlreadySaled"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("AlreadySaledTime")) Then vw_ChassisMaster.AlreadySaledTime = CType(dr("AlreadySaledTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vw_ChassisMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vw_ChassisMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vw_ChassisMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vw_ChassisMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vw_ChassisMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterDocumentID")) AndAlso CType(dr("ChassisMasterDocumentID"), Integer) > 0 Then
                vw_ChassisMaster.ChassisMasterDocumentID = CType(dr("ChassisMasterDocumentID"), Integer)
                vw_ChassisMaster.ChassisMasterDocument = New ChassisMasterDocument(CType(dr("ChassisMasterDocumentID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("EngineImagePath")) Then vw_ChassisMaster.EngineImagePath = dr("EngineImagePath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisImagePath")) Then vw_ChassisMaster.ChassisImagePath = dr("ChassisImagePath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadDate")) Then vw_ChassisMaster.UploadDate = CType(dr("UploadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterDocumentStatus")) Then vw_ChassisMaster.ChassisMasterDocumentStatus = CType(dr("ChassisMasterDocumentStatus"), Short)

            Return vw_ChassisMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(Vw_ChassisMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Vw_ChassisMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Vw_ChassisMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

