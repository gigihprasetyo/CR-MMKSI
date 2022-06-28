
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PODealer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 08/03/2018 - 21:59:25
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

    Public Class VWI_PODealerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPODealer"
        Private m_UpdateStatement As String = "up_UpdatePODealer"
        Private m_RetrieveStatement As String = "up_RetrievePODealer"
        Private m_RetrieveListStatement As String = "up_RetrievePODealerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePODealer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pODealer As VWI_PODealer = Nothing
            While dr.Read

                pODealer = Me.CreateObject(dr)

            End While

            Return pODealer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pODealerList As ArrayList = New ArrayList

            While dr.Read
                Dim pODealer As VWI_PODealer = Me.CreateObject(dr)
                pODealerList.Add(pODealer)
            End While

            Return pODealerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODealer As VWI_PODealer = CType(obj, VWI_PODealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@POHeaderId", DbType.Int32, pODealer.POHeaderId)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODealer As VWI_PODealer = CType(obj, VWI_PODealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@POHeaderId", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int16, pODealer.DealerId)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, pODealer.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, pODealer.DealerName)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, pODealer.PONumber)
            DbCommandWrapper.AddInParameter("@POType", DbType.AnsiString, pODealer.POType)
            DbCommandWrapper.AddInParameter("@NumOfInstallment", DbType.Int32, pODealer.NumOfInstallment)
            DbCommandWrapper.AddInParameter("@AllocQty", DbType.Int32, pODealer.AllocQty)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, pODealer.Price)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Currency, pODealer.Discount)
            DbCommandWrapper.AddInParameter("@Interest", DbType.Currency, pODealer.Interest)
            DbCommandWrapper.AddInParameter("@ContractNumber", DbType.AnsiString, pODealer.ContractNumber)
            DbCommandWrapper.AddInParameter("@PKNumber", DbType.AnsiString, pODealer.PKNumber)
            DbCommandWrapper.AddInParameter("@DealerPKNumber", DbType.AnsiString, pODealer.DealerPKNumber)
            DbCommandWrapper.AddInParameter("@DealerPONumber", DbType.AnsiString, pODealer.DealerPONumber)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, pODealer.ProjectName)
            DbCommandWrapper.AddInParameter("@SalesOrderId", DbType.Int32, pODealer.SalesOrderId)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, pODealer.SONumber)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, pODealer.SODate)
            DbCommandWrapper.AddInParameter("@PaymentRef", DbType.AnsiString, pODealer.PaymentRef)
            DbCommandWrapper.AddInParameter("@SOType", DbType.AnsiString, pODealer.SOType)
            DbCommandWrapper.AddInParameter("@TermOfPaymentCode", DbType.AnsiString, pODealer.TermOfPaymentCode)
            DbCommandWrapper.AddInParameter("@TOPDescription", DbType.AnsiString, pODealer.TOPDescription)
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, pODealer.DueDate)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@VehicleColorCode", DbType.AnsiString, pODealer.VehicleColorCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, pODealer.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, pODealer.MaterialNumber)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, pODealer.MaterialDescription)
            DbCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, pODealer.BasePrice)
            DbCommandWrapper.AddInParameter("@OptionPrice", DbType.Currency, pODealer.OptionPrice)
            DbCommandWrapper.AddInParameter("@DiscountBeforeTax", DbType.Decimal, pODealer.DiscountBeforeTax)
            DbCommandWrapper.AddInParameter("@NetPrice", DbType.Decimal, pODealer.NetPrice)
            DbCommandWrapper.AddInParameter("@TotalHarga", DbType.Decimal, pODealer.TotalHarga)
            DbCommandWrapper.AddInParameter("@PPN", DbType.Decimal, pODealer.PPN)
            DbCommandWrapper.AddInParameter("@TotalHargaPPN", DbType.Decimal, pODealer.TotalHargaPPN)
            DbCommandWrapper.AddInParameter("@TotalHargaPP", DbType.Decimal, pODealer.TotalHargaPP)
            DbCommandWrapper.AddInParameter("@TotalHargaLC", DbType.Decimal, pODealer.TotalHargaLC)
            DbCommandWrapper.AddInParameter("@TotalDeposit", DbType.Decimal, pODealer.TotalDeposit)
            DbCommandWrapper.AddInParameter("@TotalInterest", DbType.Decimal, pODealer.TotalInterest)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@POHeaderId"), Integer)

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
            DbCommandWrapper.AddInParameter("@POHeaderId", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODealer As VWI_PODealer = CType(obj, VWI_PODealer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pODealer.ID)
            DbCommandWrapper.AddInParameter("@POHeaderId", DbType.Int32, pODealer.POHeaderId)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int16, pODealer.DealerId)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, pODealer.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, pODealer.DealerName)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, pODealer.PONumber)
            DbCommandWrapper.AddInParameter("@POType", DbType.AnsiString, pODealer.POType)
            DbCommandWrapper.AddInParameter("@NumOfInstallment", DbType.Int32, pODealer.NumOfInstallment)
            DbCommandWrapper.AddInParameter("@AllocQty", DbType.Int32, pODealer.AllocQty)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, pODealer.Price)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Currency, pODealer.Discount)
            DbCommandWrapper.AddInParameter("@Interest", DbType.Currency, pODealer.Interest)
            DbCommandWrapper.AddInParameter("@ContractNumber", DbType.AnsiString, pODealer.ContractNumber)
            DbCommandWrapper.AddInParameter("@PKNumber", DbType.AnsiString, pODealer.PKNumber)
            DbCommandWrapper.AddInParameter("@DealerPKNumber", DbType.AnsiString, pODealer.DealerPKNumber)
            DbCommandWrapper.AddInParameter("@DealerPONumber", DbType.AnsiString, pODealer.DealerPONumber)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, pODealer.ProjectName)
            DbCommandWrapper.AddInParameter("@SalesOrderId", DbType.Int32, pODealer.SalesOrderId)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, pODealer.SONumber)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, pODealer.SODate)
            DbCommandWrapper.AddInParameter("@PaymentRef", DbType.AnsiString, pODealer.PaymentRef)
            DbCommandWrapper.AddInParameter("@SOType", DbType.AnsiString, pODealer.SOType)
            DbCommandWrapper.AddInParameter("@TermOfPaymentCode", DbType.AnsiString, pODealer.TermOfPaymentCode)
            DbCommandWrapper.AddInParameter("@TOPDescription", DbType.AnsiString, pODealer.TOPDescription)
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, pODealer.DueDate)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@VehicleColorCode", DbType.AnsiString, pODealer.VehicleColorCode)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, pODealer.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, pODealer.MaterialNumber)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, pODealer.MaterialDescription)
            DbCommandWrapper.AddInParameter("@BasePrice", DbType.Currency, pODealer.BasePrice)
            DbCommandWrapper.AddInParameter("@OptionPrice", DbType.Currency, pODealer.OptionPrice)
            DbCommandWrapper.AddInParameter("@DiscountBeforeTax", DbType.Decimal, pODealer.DiscountBeforeTax)
            DbCommandWrapper.AddInParameter("@NetPrice", DbType.Decimal, pODealer.NetPrice)
            DbCommandWrapper.AddInParameter("@TotalHarga", DbType.Decimal, pODealer.TotalHarga)
            DbCommandWrapper.AddInParameter("@PPN", DbType.Decimal, pODealer.PPN)
            DbCommandWrapper.AddInParameter("@TotalHargaPPN", DbType.Decimal, pODealer.TotalHargaPPN)
            DbCommandWrapper.AddInParameter("@TotalHargaPP", DbType.Decimal, pODealer.TotalHargaPP)
            DbCommandWrapper.AddInParameter("@TotalHargaLC", DbType.Decimal, pODealer.TotalHargaLC)
            DbCommandWrapper.AddInParameter("@TotalDeposit", DbType.Decimal, pODealer.TotalDeposit)
            DbCommandWrapper.AddInParameter("@TotalInterest", DbType.Decimal, pODealer.TotalInterest)

            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_PODealer

            Dim pODealer As VWI_PODealer = New VWI_PODealer

            pODealer.POHeaderId = CType(dr("POHeaderId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then pODealer.DealerId = CType(dr("DealerId"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then pODealer.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then pODealer.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DestinationDealerCode")) Then pODealer.DestinationDealerCode = dr("DestinationDealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then pODealer.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("POType")) Then pODealer.POType = dr("POType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NumOfInstallment")) Then pODealer.NumOfInstallment = CType(dr("NumOfInstallment"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocQty")) Then pODealer.AllocQty = CType(dr("AllocQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Price")) Then pODealer.Price = CType(dr("Price"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then pODealer.Discount = CType(dr("Discount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Interest")) Then pODealer.Interest = CType(dr("Interest"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ContractNumber")) Then pODealer.ContractNumber = dr("ContractNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PKNumber")) Then pODealer.PKNumber = dr("PKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerPKNumber")) Then pODealer.DealerPKNumber = dr("DealerPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerPONumber")) Then pODealer.DealerPONumber = dr("DealerPONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectName")) Then pODealer.ProjectName = dr("ProjectName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesOrderId")) Then pODealer.SalesOrderId = CType(dr("SalesOrderId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then pODealer.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SODate")) Then pODealer.SODate = CType(dr("SODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentRef")) Then pODealer.PaymentRef = dr("PaymentRef").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SOType")) Then pODealer.SOType = dr("SOType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentCode")) Then pODealer.TermOfPaymentCode = dr("TermOfPaymentCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TOPDescription")) Then pODealer.TOPDescription = dr("TOPDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DueDate")) Then pODealer.DueDate = CType(dr("DueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pODealer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorCode")) Then pODealer.VehicleColorCode = dr("VehicleColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then pODealer.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialNumber")) Then pODealer.MaterialNumber = dr("MaterialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialDescription")) Then pODealer.MaterialDescription = dr("MaterialDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BasePrice")) Then pODealer.BasePrice = CType(dr("BasePrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("OptionPrice")) Then pODealer.OptionPrice = CType(dr("OptionPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountBeforeTax")) Then pODealer.DiscountBeforeTax = CType(dr("DiscountBeforeTax"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("NetPrice")) Then pODealer.NetPrice = CType(dr("NetPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalHarga")) Then pODealer.TotalHarga = CType(dr("TotalHarga"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPN")) Then pODealer.PPN = CType(dr("PPN"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalHargaPPN")) Then pODealer.TotalHargaPPN = CType(dr("TotalHargaPPN"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalHargaPP")) Then pODealer.TotalHargaPP = CType(dr("TotalHargaPP"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalHargaLC")) Then pODealer.TotalHargaLC = CType(dr("TotalHargaLC"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalDeposit")) Then pODealer.TotalDeposit = CType(dr("TotalDeposit"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalInterest")) Then pODealer.TotalInterest = CType(dr("TotalInterest"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SPLNumber")) Then pODealer.SPLNumber = dr("SPLNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ETDDate")) Then pODealer.ETDDate = CType(dr("ETDDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EffectiveDate")) Then pODealer.EffectiveDate = CType(dr("EffectiveDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then pODealer.ReleaseDate = CType(dr("ReleaseDate"), DateTime)
            Return pODealer

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_PODealer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_PODealer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_PODealer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

