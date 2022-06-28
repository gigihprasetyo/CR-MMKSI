#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VDH Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 9/3/2006 - 10:08:18 AM
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

    Public Class VDHMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVDH"
        Private m_UpdateStatement As String = "up_UpdateVDH"
        Private m_RetrieveStatement As String = "up_RetrieveVDH"
        Private m_RetrieveListStatement As String = "up_RetrieveVDHList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVDH"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vDH As VDH = Nothing
            While dr.Read

                vDH = Me.CreateObject(dr)

            End While

            Return vDH

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vDHList As ArrayList = New ArrayList

            While dr.Read
                Dim vDH As VDH = Me.CreateObject(dr)
                vDHList.Add(vDH)
            End While

            Return vDHList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vDH As VDH = CType(obj, VDH)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vDH.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vDH As VDH = CType(obj, VDH)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ChassisNo", DbType.AnsiString, vDH.ChassisNo)
            DbCommandWrapper.AddInParameter("@ItemNo", DbType.AnsiString, vDH.ItemNo)
            DbCommandWrapper.AddInParameter("@EngineNo", DbType.AnsiString, vDH.EngineNo)
            DbCommandWrapper.AddInParameter("@MMCLotNo", DbType.AnsiString, vDH.MMCLotNo)
            DbCommandWrapper.AddInParameter("@InvoiceBuy", DbType.AnsiString, vDH.InvoiceBuy)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.AnsiString, vDH.ProductionYear)
            DbCommandWrapper.AddInParameter("@NIKNo", DbType.AnsiString, vDH.NIKNo)
            DbCommandWrapper.AddInParameter("@ReceiptCBUDate", DbType.DateTime, vDH.ReceiptCBUDate)
            DbCommandWrapper.AddInParameter("@CarrosseryTransferDate", DbType.DateTime, vDH.CarrosseryTransferDate)
            DbCommandWrapper.AddInParameter("@ReceiptCarrosseryDate", DbType.DateTime, vDH.ReceiptCarrosseryDate)
            DbCommandWrapper.AddInParameter("@Serial", DbType.AnsiString, vDH.Serial)
            DbCommandWrapper.AddInParameter("@Customer", DbType.AnsiString, vDH.Customer)
            DbCommandWrapper.AddInParameter("@EndCustomerName", DbType.AnsiString, vDH.EndCustomerName)
            DbCommandWrapper.AddInParameter("@EndCustomerAddress", DbType.AnsiString, vDH.EndCustomerAddress)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, vDH.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, vDH.Kecamatan)
            DbCommandWrapper.AddInParameter("@Kabupaten", DbType.AnsiString, vDH.Kabupaten)
            DbCommandWrapper.AddInParameter("@Propinsi", DbType.AnsiString, vDH.Propinsi)
            DbCommandWrapper.AddInParameter("@R", DbType.AnsiString, vDH.R)
            DbCommandWrapper.AddInParameter("@Type", DbType.AnsiString, vDH.Type)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, vDH.RequestDate)
            DbCommandWrapper.AddInParameter("@DOPrintDate", DbType.DateTime, vDH.DOPrintDate)
            DbCommandWrapper.AddInParameter("@ScheduleShipDate", DbType.DateTime, vDH.ScheduleShipDate)
            DbCommandWrapper.AddInParameter("@SCVDate1", DbType.DateTime, vDH.SCVDate1)
            DbCommandWrapper.AddInParameter("@SVCDate2", DbType.DateTime, vDH.SVCDate2)
            DbCommandWrapper.AddInParameter("@SVCCust1", DbType.AnsiString, vDH.SVCCust1)
            DbCommandWrapper.AddInParameter("@SVCCust2", DbType.AnsiString, vDH.SVCCust2)
            DbCommandWrapper.AddInParameter("@FactureOpenDate", DbType.DateTime, vDH.FactureOpenDate)
            DbCommandWrapper.AddInParameter("@FactureDate", DbType.DateTime, vDH.FactureDate)
            DbCommandWrapper.AddInParameter("@FactureNo", DbType.AnsiString, vDH.FactureNo)
            DbCommandWrapper.AddInParameter("@FactureComment", DbType.AnsiString, vDH.FactureComment)
            DbCommandWrapper.AddInParameter("@VATNo", DbType.AnsiString, vDH.VATNo)
            DbCommandWrapper.AddInParameter("@VATDate", DbType.DateTime, vDH.VATDate)
            DbCommandWrapper.AddInParameter("@StockOutDate", DbType.DateTime, vDH.StockOutDate)
            DbCommandWrapper.AddInParameter("@Orders", DbType.AnsiString, vDH.Orders)
            DbCommandWrapper.AddInParameter("@PIUDNo", DbType.AnsiString, vDH.PIUDNo)
            DbCommandWrapper.AddInParameter("@PIUDDate", DbType.AnsiString, vDH.PIUDDate)
            DbCommandWrapper.AddInParameter("@IncoiveSell", DbType.AnsiString, vDH.IncoiveSell)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vDH.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vDH.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@VDHCustomerID", DbType.Int32, Me.GetRefObject(vDH.VDHCustomer))

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

            Dim vDH As VDH = CType(obj, VDH)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vDH.ID)
            DbCommandWrapper.AddInParameter("@ChassisNo", DbType.AnsiString, vDH.ChassisNo)
            DbCommandWrapper.AddInParameter("@ItemNo", DbType.AnsiString, vDH.ItemNo)
            DbCommandWrapper.AddInParameter("@EngineNo", DbType.AnsiString, vDH.EngineNo)
            DbCommandWrapper.AddInParameter("@MMCLotNo", DbType.AnsiString, vDH.MMCLotNo)
            DbCommandWrapper.AddInParameter("@InvoiceBuy", DbType.AnsiString, vDH.InvoiceBuy)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.AnsiString, vDH.ProductionYear)
            DbCommandWrapper.AddInParameter("@NIKNo", DbType.AnsiString, vDH.NIKNo)
            DbCommandWrapper.AddInParameter("@ReceiptCBUDate", DbType.DateTime, vDH.ReceiptCBUDate)
            DbCommandWrapper.AddInParameter("@CarrosseryTransferDate", DbType.DateTime, vDH.CarrosseryTransferDate)
            DbCommandWrapper.AddInParameter("@ReceiptCarrosseryDate", DbType.DateTime, vDH.ReceiptCarrosseryDate)
            DbCommandWrapper.AddInParameter("@Serial", DbType.AnsiString, vDH.Serial)
            DbCommandWrapper.AddInParameter("@Customer", DbType.AnsiString, vDH.Customer)
            DbCommandWrapper.AddInParameter("@EndCustomerName", DbType.AnsiString, vDH.EndCustomerName)
            DbCommandWrapper.AddInParameter("@EndCustomerAddress", DbType.AnsiString, vDH.EndCustomerAddress)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, vDH.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, vDH.Kecamatan)
            DbCommandWrapper.AddInParameter("@Kabupaten", DbType.AnsiString, vDH.Kabupaten)
            DbCommandWrapper.AddInParameter("@Propinsi", DbType.AnsiString, vDH.Propinsi)
            DbCommandWrapper.AddInParameter("@R", DbType.AnsiString, vDH.R)
            DbCommandWrapper.AddInParameter("@Type", DbType.AnsiString, vDH.Type)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, vDH.RequestDate)
            DbCommandWrapper.AddInParameter("@DOPrintDate", DbType.DateTime, vDH.DOPrintDate)
            DbCommandWrapper.AddInParameter("@ScheduleShipDate", DbType.DateTime, vDH.ScheduleShipDate)
            DbCommandWrapper.AddInParameter("@SCVDate1", DbType.DateTime, vDH.SCVDate1)
            DbCommandWrapper.AddInParameter("@SVCDate2", DbType.DateTime, vDH.SVCDate2)
            DbCommandWrapper.AddInParameter("@SVCCust1", DbType.AnsiString, vDH.SVCCust1)
            DbCommandWrapper.AddInParameter("@SVCCust2", DbType.AnsiString, vDH.SVCCust2)
            DbCommandWrapper.AddInParameter("@FactureOpenDate", DbType.DateTime, vDH.FactureOpenDate)
            DbCommandWrapper.AddInParameter("@FactureDate", DbType.DateTime, vDH.FactureDate)
            DbCommandWrapper.AddInParameter("@FactureNo", DbType.AnsiString, vDH.FactureNo)
            DbCommandWrapper.AddInParameter("@FactureComment", DbType.AnsiString, vDH.FactureComment)
            DbCommandWrapper.AddInParameter("@VATNo", DbType.AnsiString, vDH.VATNo)
            DbCommandWrapper.AddInParameter("@VATDate", DbType.DateTime, vDH.VATDate)
            DbCommandWrapper.AddInParameter("@StockOutDate", DbType.DateTime, vDH.StockOutDate)
            DbCommandWrapper.AddInParameter("@Orders", DbType.AnsiString, vDH.Orders)
            DbCommandWrapper.AddInParameter("@PIUDNo", DbType.AnsiString, vDH.PIUDNo)
            DbCommandWrapper.AddInParameter("@PIUDDate", DbType.AnsiString, vDH.PIUDDate)
            DbCommandWrapper.AddInParameter("@IncoiveSell", DbType.AnsiString, vDH.IncoiveSell)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vDH.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vDH.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@VDHCustomerID", DbType.Int32, Me.GetRefObject(vDH.VDHCustomer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VDH

            Dim vDH As VDH = New VDH

            vDH.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNo")) Then vDH.ChassisNo = dr("ChassisNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ItemNo")) Then vDH.ItemNo = dr("ItemNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EngineNo")) Then vDH.EngineNo = dr("EngineNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MMCLotNo")) Then vDH.MMCLotNo = dr("MMCLotNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceBuy")) Then vDH.InvoiceBuy = dr("InvoiceBuy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then vDH.ProductionYear = dr("ProductionYear").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NIKNo")) Then vDH.NIKNo = dr("NIKNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptCBUDate")) Then vDH.ReceiptCBUDate = CType(dr("ReceiptCBUDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CarrosseryTransferDate")) Then vDH.CarrosseryTransferDate = CType(dr("CarrosseryTransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptCarrosseryDate")) Then vDH.ReceiptCarrosseryDate = CType(dr("ReceiptCarrosseryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Serial")) Then vDH.Serial = dr("Serial").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Customer")) Then vDH.Customer = dr("Customer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerName")) Then vDH.EndCustomerName = dr("EndCustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerAddress")) Then vDH.EndCustomerAddress = dr("EndCustomerAddress").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kelurahan")) Then vDH.Kelurahan = dr("Kelurahan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kecamatan")) Then vDH.Kecamatan = dr("Kecamatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kabupaten")) Then vDH.Kabupaten = dr("Kabupaten").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Propinsi")) Then vDH.Propinsi = dr("Propinsi").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("R")) Then vDH.R = dr("R").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then vDH.Type = dr("Type").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RequestDate")) Then vDH.RequestDate = CType(dr("RequestDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DOPrintDate")) Then vDH.DOPrintDate = CType(dr("DOPrintDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ScheduleShipDate")) Then vDH.ScheduleShipDate = CType(dr("ScheduleShipDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SCVDate1")) Then vDH.SCVDate1 = CType(dr("SCVDate1"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SVCDate2")) Then vDH.SVCDate2 = CType(dr("SVCDate2"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SVCCust1")) Then vDH.SVCCust1 = dr("SVCCust1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SVCCust2")) Then vDH.SVCCust2 = dr("SVCCust2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FactureOpenDate")) Then vDH.FactureOpenDate = CType(dr("FactureOpenDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FactureDate")) Then vDH.FactureDate = CType(dr("FactureDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FactureNo")) Then vDH.FactureNo = dr("FactureNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FactureComment")) Then vDH.FactureComment = dr("FactureComment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VATNo")) Then vDH.VATNo = dr("VATNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VATDate")) Then vDH.VATDate = CType(dr("VATDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("StockOutDate")) Then vDH.StockOutDate = CType(dr("StockOutDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Orders")) Then vDH.Orders = dr("Orders").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PIUDNo")) Then vDH.PIUDNo = dr("PIUDNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PIUDDate")) Then vDH.PIUDDate = dr("PIUDDate").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IncoiveSell")) Then vDH.IncoiveSell = dr("IncoiveSell").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vDH.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vDH.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vDH.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vDH.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vDH.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VDHCustomerID")) Then
                vDH.VDHCustomer = New VDHCustomer(CType(dr("VDHCustomerID"), Integer))
            End If

            Return vDH

        End Function

        Private Sub SetTableName()

            If Not (GetType(VDH) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VDH), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VDH).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

