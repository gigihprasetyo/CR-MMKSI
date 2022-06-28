
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ChassisMasterBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 3/20/2006 - 11:37:42 AM
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

    Public Class ChassisMasterBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertChassisMasterBB"
        Private m_UpdateStatement As String = "up_UpdateChassisMasterBB"
        Private m_RetrieveStatement As String = "up_RetrieveChassisMasterBB"
        Private m_RetrieveListStatement As String = "up_RetrieveChassisMasterBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteChassisMasterBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ChassisMasterBB As ChassisMasterBB = Nothing
            While dr.Read

                ChassisMasterBB = Me.CreateObject(dr)

            End While

            Return ChassisMasterBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ChassisMasterBBList As ArrayList = New ArrayList

            While dr.Read
                Dim ChassisMasterBB As ChassisMasterBB = Me.CreateObject(dr)
                ChassisMasterBBList.Add(ChassisMasterBB)
            End While

            Return ChassisMasterBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ChassisMasterBB As ChassisMasterBB = CType(obj, ChassisMasterBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ChassisMasterBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ChassisMasterBB As ChassisMasterBB = CType(obj, ChassisMasterBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, ChassisMasterBB.ChassisNumber)
            'DBCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, ChassisMasterBB.VehicleKindID)
            DBCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, ChassisMasterBB.DONumber)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, ChassisMasterBB.SONumber)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, ChassisMasterBB.DiscountAmount)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, ChassisMasterBB.PONumber)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, ChassisMasterBB.EngineNumber)
            DbCommandWrapper.AddInParameter("@SerialNumber", DbType.AnsiString, ChassisMasterBB.SerialNumber)
            DbCommandWrapper.AddInParameter("@DODate", DbType.DateTime, ChassisMasterBB.DODate)
            DbCommandWrapper.AddInParameter("@GIDate", DbType.DateTime, ChassisMasterBB.GIDate)
            DbCommandWrapper.AddInParameter("@ParkingDays", DbType.Int32, ChassisMasterBB.ParkingDays)
            DbCommandWrapper.AddInParameter("@ParkingAmount", DbType.Currency, ChassisMasterBB.ParkingAmount)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, ChassisMasterBB.FakturStatus)
            DbCommandWrapper.AddInParameter("@PendingDesc", DbType.AnsiString, ChassisMasterBB.PendingDesc)
            DbCommandWrapper.AddInParameter("@IsSAPDownload", DbType.AnsiString, ChassisMasterBB.IsSAPDownload)
            DbCommandWrapper.AddInParameter("@StockDealer", DbType.Int32, ChassisMasterBB.StockDealer)
            DbCommandWrapper.AddInParameter("@StockDate", DbType.DateTime, ChassisMasterBB.StockDate)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int32, ChassisMasterBB.ProductionYear)
            DbCommandWrapper.AddInParameter("@StockStatus", DbType.AnsiString, ChassisMasterBB.StockStatus)
            DBCommandWrapper.AddInParameter("@LastUpdateProfile", DbType.DateTime, ChassisMasterBB.LastUpdateProfile)
            DBCommandWrapper.AddInParameter("@AlreadySaled", DbType.Byte, ChassisMasterBB.AlreadySaled)

            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ChassisMasterBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ChassisMasterBB.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int32, Me.GetRefObject(ChassisMasterBB.Dealer))
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, Me.GetRefObject(ChassisMasterBB.EndCustomer))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(ChassisMasterBB.Category))
            DbCommandWrapper.AddInParameter("@TOPID", DbType.Int32, Me.GetRefObject(ChassisMasterBB.TermOfPayment))
            DBCommandWrapper.AddInParameter("@VechileColorID", DbType.Int32, Me.GetRefObject(ChassisMasterBB.VechileColor))
            DBCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, Me.GetRefObject(ChassisMasterBB.VehicleKind))

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

            Dim ChassisMasterBB As ChassisMasterBB = CType(obj, ChassisMasterBB)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ChassisMasterBB.ID)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, ChassisMasterBB.ChassisNumber)
            'DBCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, ChassisMasterBB.VehicleKindID)
            DBCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, ChassisMasterBB.DONumber)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, ChassisMasterBB.SONumber)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, ChassisMasterBB.DiscountAmount)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, ChassisMasterBB.PONumber)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, ChassisMasterBB.EngineNumber)
            DbCommandWrapper.AddInParameter("@SerialNumber", DbType.AnsiString, ChassisMasterBB.SerialNumber)
            DbCommandWrapper.AddInParameter("@DODate", DbType.DateTime, ChassisMasterBB.DODate)
            DbCommandWrapper.AddInParameter("@GIDate", DbType.DateTime, ChassisMasterBB.GIDate)
            DbCommandWrapper.AddInParameter("@ParkingDays", DbType.Int32, ChassisMasterBB.ParkingDays)
            DbCommandWrapper.AddInParameter("@ParkingAmount", DbType.Currency, ChassisMasterBB.ParkingAmount)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, ChassisMasterBB.FakturStatus)
            DbCommandWrapper.AddInParameter("@PendingDesc", DbType.AnsiString, ChassisMasterBB.PendingDesc)
            DbCommandWrapper.AddInParameter("@IsSAPDownload", DbType.AnsiString, ChassisMasterBB.IsSAPDownload)
            DbCommandWrapper.AddInParameter("@StockDealer", DbType.Int32, ChassisMasterBB.StockDealer)
            DbCommandWrapper.AddInParameter("@StockDate", DbType.DateTime, ChassisMasterBB.StockDate)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int32, ChassisMasterBB.ProductionYear)
            DbCommandWrapper.AddInParameter("@StockStatus", DbType.AnsiString, ChassisMasterBB.StockStatus)
            DBCommandWrapper.AddInParameter("@LastUpdateProfile", DbType.DateTime, ChassisMasterBB.LastUpdateProfile)
            DBCommandWrapper.AddInParameter("@AlreadySaled", DbType.Byte, ChassisMasterBB.AlreadySaled)

            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ChassisMasterBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ChassisMasterBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int32, Me.GetRefObject(ChassisMasterBB.Dealer))
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, Me.GetRefObject(ChassisMasterBB.EndCustomer))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(ChassisMasterBB.Category))
            DbCommandWrapper.AddInParameter("@TOPID", DbType.Int32, Me.GetRefObject(ChassisMasterBB.TermOfPayment))
            DBCommandWrapper.AddInParameter("@VechileColorID", DbType.Int32, Me.GetRefObject(ChassisMasterBB.VechileColor))
            DBCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, Me.GetRefObject(ChassisMasterBB.VehicleKind))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ChassisMasterBB

            Dim ChassisMasterBB As ChassisMasterBB = New ChassisMasterBB

            ChassisMasterBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then ChassisMasterBB.ChassisNumber = dr("ChassisNumber").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("VehicleKindID")) Then ChassisMasterBB.VehicleKindID = CType(dr("VehicleKindID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then ChassisMasterBB.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then ChassisMasterBB.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountAmount")) Then ChassisMasterBB.DiscountAmount = CType(dr("DiscountAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then ChassisMasterBB.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EngineNumber")) Then ChassisMasterBB.EngineNumber = dr("EngineNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SerialNumber")) Then ChassisMasterBB.SerialNumber = dr("SerialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DODate")) Then ChassisMasterBB.DODate = CType(dr("DODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GIDate")) Then ChassisMasterBB.GIDate = CType(dr("GIDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ParkingDays")) Then ChassisMasterBB.ParkingDays = CType(dr("ParkingDays"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ParkingAmount")) Then ChassisMasterBB.ParkingAmount = CType(dr("ParkingAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturStatus")) Then ChassisMasterBB.FakturStatus = dr("FakturStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PendingDesc")) Then ChassisMasterBB.PendingDesc = dr("PendingDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsSAPDownload")) Then ChassisMasterBB.IsSAPDownload = dr("IsSAPDownload").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StockDealer")) Then ChassisMasterBB.StockDealer = CType(dr("StockDealer"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StockDate")) Then ChassisMasterBB.StockDate = CType(dr("StockDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then ChassisMasterBB.ProductionYear = CType(dr("ProductionYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StockStatus")) Then ChassisMasterBB.StockStatus = dr("StockStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateProfile")) Then ChassisMasterBB.LastUpdateProfile = CType(dr("LastUpdateProfile"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AlreadySaled")) Then ChassisMasterBB.AlreadySaled = CType(dr("AlreadySaled"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ChassisMasterBB.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ChassisMasterBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ChassisMasterBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ChassisMasterBB.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ChassisMasterBB.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDealerID")) Then
                ChassisMasterBB.Dealer = New Dealer(CType(dr("SoldDealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerID")) Then
                ChassisMasterBB.EndCustomer = New EndCustomer(CType(dr("EndCustomerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                ChassisMasterBB.Category = New Category(CType(dr("CategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TOPID")) Then
                ChassisMasterBB.TermOfPayment = New TermOfPayment(CType(dr("TOPID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorID")) Then
                ChassisMasterBB.VechileColor = New VechileColor(CType(dr("VechileColorID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleKindID")) Then
                ChassisMasterBB.VehicleKind = New VehicleKind(CType(dr("VehicleKindID"), Integer))

            End If

            Return ChassisMasterBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(ChassisMasterBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ChassisMasterBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ChassisMasterBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

