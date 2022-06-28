
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ChassisMaster Objects Mapper.
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

    Public Class ChassisMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertChassisMaster"
        Private m_UpdateStatement As String = "up_UpdateChassisMaster"
        Private m_RetrieveStatement As String = "up_RetrieveChassisMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveChassisMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteChassisMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim chassisMaster As ChassisMaster = Nothing
            While dr.Read

                chassisMaster = Me.CreateObject(dr)

            End While

            Return chassisMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim chassisMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim chassisMaster As ChassisMaster = Me.CreateObject(dr)
                chassisMasterList.Add(chassisMaster)
            End While

            Return chassisMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim chassisMaster As ChassisMaster = CType(obj, ChassisMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, chassisMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim chassisMaster As ChassisMaster = CType(obj, ChassisMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, chassisMaster.ChassisNumber)
            'DBCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, chassisMaster.VehicleKindID)
            DBCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, chassisMaster.DONumber)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, chassisMaster.SONumber)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, chassisMaster.DiscountAmount)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, chassisMaster.PONumber)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, chassisMaster.EngineNumber)
            DbCommandWrapper.AddInParameter("@SerialNumber", DbType.AnsiString, chassisMaster.SerialNumber)
            DbCommandWrapper.AddInParameter("@DODate", DbType.DateTime, chassisMaster.DODate)
            DbCommandWrapper.AddInParameter("@GIDate", DbType.DateTime, chassisMaster.GIDate)
            DbCommandWrapper.AddInParameter("@ParkingDays", DbType.Int32, chassisMaster.ParkingDays)
            DbCommandWrapper.AddInParameter("@ParkingAmount", DbType.Currency, chassisMaster.ParkingAmount)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, chassisMaster.FakturStatus)
            DbCommandWrapper.AddInParameter("@PendingDesc", DbType.AnsiString, chassisMaster.PendingDesc)
            DbCommandWrapper.AddInParameter("@IsSAPDownload", DbType.AnsiString, chassisMaster.IsSAPDownload)
            DbCommandWrapper.AddInParameter("@StockDealer", DbType.Int32, chassisMaster.StockDealer)
            DbCommandWrapper.AddInParameter("@StockDate", DbType.DateTime, chassisMaster.StockDate)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int32, chassisMaster.ProductionYear)
            DbCommandWrapper.AddInParameter("@StockStatus", DbType.AnsiString, chassisMaster.StockStatus)
            DBCommandWrapper.AddInParameter("@LastUpdateProfile", DbType.DateTime, chassisMaster.LastUpdateProfile)
            DbCommandWrapper.AddInParameter("@AlreadySaled", DbType.Byte, chassisMaster.AlreadySaled)
            DbCommandWrapper.AddInParameter("@AlreadySaledTime", DbType.DateTime, chassisMaster.AlreadySaledTime)

            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, chassisMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, chassisMaster.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int32, Me.GetRefObject(chassisMaster.Dealer))
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, Me.GetRefObject(chassisMaster.EndCustomer))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(chassisMaster.Category))
            DbCommandWrapper.AddInParameter("@TOPID", DbType.Int32, Me.GetRefObject(chassisMaster.TermOfPayment))
            DBCommandWrapper.AddInParameter("@VechileColorID", DbType.Int32, Me.GetRefObject(chassisMaster.VechileColor))
            DBCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, Me.GetRefObject(chassisMaster.VehicleKind))

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

            Dim chassisMaster As ChassisMaster = CType(obj, ChassisMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, chassisMaster.ID)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, chassisMaster.ChassisNumber)
            'DBCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, chassisMaster.VehicleKindID)
            DBCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, chassisMaster.DONumber)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, chassisMaster.SONumber)
            DbCommandWrapper.AddInParameter("@DiscountAmount", DbType.Currency, chassisMaster.DiscountAmount)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, chassisMaster.PONumber)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, chassisMaster.EngineNumber)
            DbCommandWrapper.AddInParameter("@SerialNumber", DbType.AnsiString, chassisMaster.SerialNumber)
            DbCommandWrapper.AddInParameter("@DODate", DbType.DateTime, chassisMaster.DODate)
            DbCommandWrapper.AddInParameter("@GIDate", DbType.DateTime, chassisMaster.GIDate)
            DbCommandWrapper.AddInParameter("@ParkingDays", DbType.Int32, chassisMaster.ParkingDays)
            DbCommandWrapper.AddInParameter("@ParkingAmount", DbType.Currency, chassisMaster.ParkingAmount)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, chassisMaster.FakturStatus)
            DbCommandWrapper.AddInParameter("@PendingDesc", DbType.AnsiString, chassisMaster.PendingDesc)
            DbCommandWrapper.AddInParameter("@IsSAPDownload", DbType.AnsiString, chassisMaster.IsSAPDownload)
            DbCommandWrapper.AddInParameter("@StockDealer", DbType.Int32, chassisMaster.StockDealer)
            DbCommandWrapper.AddInParameter("@StockDate", DbType.DateTime, chassisMaster.StockDate)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int32, chassisMaster.ProductionYear)
            DbCommandWrapper.AddInParameter("@StockStatus", DbType.AnsiString, chassisMaster.StockStatus)
            DBCommandWrapper.AddInParameter("@LastUpdateProfile", DbType.DateTime, chassisMaster.LastUpdateProfile)
            DbCommandWrapper.AddInParameter("@AlreadySaled", DbType.Byte, chassisMaster.AlreadySaled)
            DbCommandWrapper.AddInParameter("@AlreadySaledTime", DbType.DateTime, chassisMaster.AlreadySaledTime)

            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, chassisMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, chassisMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int32, Me.GetRefObject(chassisMaster.Dealer))
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, Me.GetRefObject(chassisMaster.EndCustomer))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(chassisMaster.Category))
            DbCommandWrapper.AddInParameter("@TOPID", DbType.Int32, Me.GetRefObject(chassisMaster.TermOfPayment))
            DBCommandWrapper.AddInParameter("@VechileColorID", DbType.Int32, Me.GetRefObject(chassisMaster.VechileColor))
            DBCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, Me.GetRefObject(chassisMaster.VehicleKind))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ChassisMaster

            Dim chassisMaster As ChassisMaster = New ChassisMaster

            chassisMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then chassisMaster.ChassisNumber = dr("ChassisNumber").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("VehicleKindID")) Then chassisMaster.VehicleKindID = CType(dr("VehicleKindID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then chassisMaster.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then chassisMaster.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountAmount")) Then chassisMaster.DiscountAmount = CType(dr("DiscountAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then chassisMaster.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EngineNumber")) Then chassisMaster.EngineNumber = dr("EngineNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SerialNumber")) Then chassisMaster.SerialNumber = dr("SerialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DODate")) Then chassisMaster.DODate = CType(dr("DODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GIDate")) Then chassisMaster.GIDate = CType(dr("GIDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ParkingDays")) Then chassisMaster.ParkingDays = CType(dr("ParkingDays"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ParkingAmount")) Then chassisMaster.ParkingAmount = CType(dr("ParkingAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturStatus")) Then chassisMaster.FakturStatus = dr("FakturStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PendingDesc")) Then chassisMaster.PendingDesc = dr("PendingDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsSAPDownload")) Then chassisMaster.IsSAPDownload = dr("IsSAPDownload").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StockDealer")) Then chassisMaster.StockDealer = CType(dr("StockDealer"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StockDate")) Then chassisMaster.StockDate = CType(dr("StockDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then chassisMaster.ProductionYear = CType(dr("ProductionYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StockStatus")) Then chassisMaster.StockStatus = dr("StockStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateProfile")) Then chassisMaster.LastUpdateProfile = CType(dr("LastUpdateProfile"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AlreadySaled")) Then chassisMaster.AlreadySaled = CType(dr("AlreadySaled"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("AlreadySaledTime")) Then chassisMaster.AlreadySaledTime = CType(dr("AlreadySaledTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then chassisMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then chassisMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then chassisMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then chassisMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then chassisMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDealerID")) Then
                chassisMaster.Dealer = New Dealer(CType(dr("SoldDealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerID")) Then
                chassisMaster.EndCustomer = New EndCustomer(CType(dr("EndCustomerID"), Integer))
                chassisMaster.EndCustomerID = CType(dr("EndCustomerID"), Integer)
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                chassisMaster.Category = New Category(CType(dr("CategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TOPID")) Then
                chassisMaster.TermOfPayment = New TermOfPayment(CType(dr("TOPID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorID")) Then
                chassisMaster.VechileColor = New VechileColor(CType(dr("VechileColorID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleKindID")) Then
                chassisMaster.VehicleKind = New VehicleKind(CType(dr("VehicleKindID"), Integer))

            End If

            Return chassisMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(ChassisMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ChassisMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ChassisMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

