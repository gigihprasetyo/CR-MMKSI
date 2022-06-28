
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 19/06/2020 - 14:54:31
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

    Public Class DiscountProposalHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDiscountProposalHeader"
        Private m_UpdateStatement As String = "up_UpdateDiscountProposalHeader"
        Private m_RetrieveStatement As String = "up_RetrieveDiscountProposalHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveDiscountProposalHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDiscountProposalHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim discountProposalHeader As DiscountProposalHeader = Nothing
            While dr.Read

                discountProposalHeader = Me.CreateObject(dr)

            End While

            Return discountProposalHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim discountProposalHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim discountProposalHeader As DiscountProposalHeader = Me.CreateObject(dr)
                discountProposalHeaderList.Add(discountProposalHeader)
            End While

            Return discountProposalHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalHeader As DiscountProposalHeader = CType(obj, DiscountProposalHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalHeader As DiscountProposalHeader = CType(obj, DiscountProposalHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CustomerType", DbType.Int16, discountProposalHeader.CustomerType)
            DbCommandWrapper.AddInParameter("@ProposalRegNo", DbType.AnsiString, discountProposalHeader.ProposalRegNo)
            DbCommandWrapper.AddInParameter("@DealerProposalNo", DbType.AnsiString, discountProposalHeader.DealerProposalNo)
            DbCommandWrapper.AddInParameter("@FleetCategory", DbType.Int16, discountProposalHeader.FleetCategory)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, discountProposalHeader.ProjectName)
            DbCommandWrapper.AddInParameter("@LastPurchaseDate", DbType.DateTime, discountProposalHeader.LastPurchaseDate)
            DbCommandWrapper.AddInParameter("@IsDealerDirectSales", DbType.Int16, discountProposalHeader.IsDealerDirectSales)
            DbCommandWrapper.AddInParameter("@ContractorName", DbType.AnsiString, discountProposalHeader.ContractorName)
            DbCommandWrapper.AddInParameter("@PurchaseMethod", DbType.Int16, discountProposalHeader.PurchaseMethod)
            DbCommandWrapper.AddInParameter("@PaymentMethod", DbType.Int16, discountProposalHeader.PaymentMethod)
            DbCommandWrapper.AddInParameter("@PurchaseKind", DbType.Int16, discountProposalHeader.PurchaseKind)
            DbCommandWrapper.AddInParameter("@ProjectKindMethod", DbType.Int16, discountProposalHeader.ProjectKindMethod)
            DbCommandWrapper.AddInParameter("@ProjectKindMethodOther", DbType.AnsiString, discountProposalHeader.ProjectKindMethodOther)
            DbCommandWrapper.AddInParameter("@DeliveryPlanDate", DbType.DateTime, discountProposalHeader.DeliveryPlanDate)
            DbCommandWrapper.AddInParameter("@DeliveryRegionCode", DbType.AnsiString, discountProposalHeader.DeliveryRegionCode)
            DbCommandWrapper.AddInParameter("@IsAPMSubsidy", DbType.Int16, discountProposalHeader.IsAPMSubsidy)
            DbCommandWrapper.AddInParameter("@DealerNotes", DbType.AnsiString, discountProposalHeader.DealerNotes)
            DbCommandWrapper.AddInParameter("@SubmitDate", DbType.DateTime, discountProposalHeader.SubmitDate)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, discountProposalHeader.ValidFrom)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, discountProposalHeader.ValidTo)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, discountProposalHeader.Status)
            DbCommandWrapper.AddInParameter("@BusinessSectorDetailID", DbType.Int32, discountProposalHeader.BusinessSectorDetailID)
            DbCommandWrapper.AddInParameter("@Consideration", DbType.AnsiString, discountProposalHeader.Consideration)
            DbCommandWrapper.AddInParameter("@MMKSINotes", DbType.AnsiString, discountProposalHeader.MMKSINotes)
            DbCommandWrapper.AddInParameter("@FinalApproval", DbType.Int16, discountProposalHeader.FinalApproval)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, discountProposalHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(discountProposalHeader.Dealer))
            DbCommandWrapper.AddInParameter("@FleetCustomerDetailID", DbType.Int32, Me.GetRefObject(discountProposalHeader.FleetCustomerDetail))
            DbCommandWrapper.AddInParameter("@SPLID", DbType.Int32, Me.GetRefObject(discountProposalHeader.SPL))
            DbCommandWrapper.AddInParameter("@LeasingID", DbType.Int32, Me.GetRefObject(discountProposalHeader.LeasingCompany))
            DbCommandWrapper.AddInParameter("@BBNAreaProvinceID", DbType.Int16, Me.GetRefObject(discountProposalHeader.BBNAreaProvince))

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

            Dim discountProposalHeader As DiscountProposalHeader = CType(obj, DiscountProposalHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalHeader.ID)
            DbCommandWrapper.AddInParameter("@CustomerType", DbType.Int16, discountProposalHeader.CustomerType)
            DbCommandWrapper.AddInParameter("@ProposalRegNo", DbType.AnsiString, discountProposalHeader.ProposalRegNo)
            DbCommandWrapper.AddInParameter("@DealerProposalNo", DbType.AnsiString, discountProposalHeader.DealerProposalNo)
            DbCommandWrapper.AddInParameter("@FleetCategory", DbType.Int16, discountProposalHeader.FleetCategory)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, discountProposalHeader.ProjectName)
            DbCommandWrapper.AddInParameter("@LastPurchaseDate", DbType.DateTime, discountProposalHeader.LastPurchaseDate)
            DbCommandWrapper.AddInParameter("@IsDealerDirectSales", DbType.Int16, discountProposalHeader.IsDealerDirectSales)
            DbCommandWrapper.AddInParameter("@ContractorName", DbType.AnsiString, discountProposalHeader.ContractorName)
            DbCommandWrapper.AddInParameter("@PurchaseMethod", DbType.Int16, discountProposalHeader.PurchaseMethod)
            DbCommandWrapper.AddInParameter("@PaymentMethod", DbType.Int16, discountProposalHeader.PaymentMethod)
            DbCommandWrapper.AddInParameter("@PurchaseKind", DbType.Int16, discountProposalHeader.PurchaseKind)
            DbCommandWrapper.AddInParameter("@ProjectKindMethod", DbType.Int16, discountProposalHeader.ProjectKindMethod)
            DbCommandWrapper.AddInParameter("@ProjectKindMethodOther", DbType.AnsiString, discountProposalHeader.ProjectKindMethodOther)
            DbCommandWrapper.AddInParameter("@DeliveryPlanDate", DbType.DateTime, discountProposalHeader.DeliveryPlanDate)
            DbCommandWrapper.AddInParameter("@IsAPMSubsidy", DbType.Int16, discountProposalHeader.IsAPMSubsidy)
            DbCommandWrapper.AddInParameter("@DealerNotes", DbType.AnsiString, discountProposalHeader.DealerNotes)
            DbCommandWrapper.AddInParameter("@DeliveryRegionCode", DbType.AnsiString, discountProposalHeader.DeliveryRegionCode)
            DbCommandWrapper.AddInParameter("@SubmitDate", DbType.DateTime, discountProposalHeader.SubmitDate)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, discountProposalHeader.ValidFrom)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, discountProposalHeader.ValidTo)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, discountProposalHeader.Status)
            DbCommandWrapper.AddInParameter("@BusinessSectorDetailID", DbType.Int32, discountProposalHeader.BusinessSectorDetailID)
            DbCommandWrapper.AddInParameter("@Consideration", DbType.AnsiString, discountProposalHeader.Consideration)
            DbCommandWrapper.AddInParameter("@MMKSINotes", DbType.AnsiString, discountProposalHeader.MMKSINotes)
            DbCommandWrapper.AddInParameter("@FinalApproval", DbType.Int16, discountProposalHeader.FinalApproval)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, discountProposalHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(discountProposalHeader.Dealer))
            DbCommandWrapper.AddInParameter("@FleetCustomerDetailID", DbType.Int32, Me.GetRefObject(discountProposalHeader.FleetCustomerDetail))
            DbCommandWrapper.AddInParameter("@SPLID", DbType.Int32, Me.GetRefObject(discountProposalHeader.SPL))
            DbCommandWrapper.AddInParameter("@LeasingID", DbType.Int32, Me.GetRefObject(discountProposalHeader.LeasingCompany))
            DbCommandWrapper.AddInParameter("@BBNAreaProvinceID", DbType.Int16, Me.GetRefObject(discountProposalHeader.BBNAreaProvince))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DiscountProposalHeader

            Dim discountProposalHeader As DiscountProposalHeader = New DiscountProposalHeader

            discountProposalHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerType")) Then discountProposalHeader.CustomerType = CType(dr("CustomerType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProposalRegNo")) Then discountProposalHeader.ProposalRegNo = dr("ProposalRegNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerProposalNo")) Then discountProposalHeader.DealerProposalNo = dr("DealerProposalNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCategory")) Then discountProposalHeader.FleetCategory = CType(dr("FleetCategory"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectName")) Then discountProposalHeader.ProjectName = dr("ProjectName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastPurchaseDate")) Then discountProposalHeader.LastPurchaseDate = CType(dr("LastPurchaseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsDealerDirectSales")) Then discountProposalHeader.IsDealerDirectSales = CType(dr("IsDealerDirectSales"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ContractorName")) Then discountProposalHeader.ContractorName = dr("ContractorName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseMethod")) Then discountProposalHeader.PurchaseMethod = CType(dr("PurchaseMethod"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentMethod")) Then discountProposalHeader.PaymentMethod = CType(dr("PaymentMethod"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseKind")) Then discountProposalHeader.PurchaseKind = CType(dr("PurchaseKind"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectKindMethod")) Then discountProposalHeader.ProjectKindMethod = CType(dr("ProjectKindMethod"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectKindMethodOther")) Then discountProposalHeader.ProjectKindMethodOther = dr("ProjectKindMethodOther").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryPlanDate")) Then discountProposalHeader.DeliveryPlanDate = CType(dr("DeliveryPlanDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsAPMSubsidy")) Then discountProposalHeader.IsAPMSubsidy = CType(dr("IsAPMSubsidy"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerNotes")) Then discountProposalHeader.DealerNotes = dr("DealerNotes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryRegionCode")) Then discountProposalHeader.DeliveryRegionCode = dr("DeliveryRegionCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubmitDate")) Then discountProposalHeader.SubmitDate = CType(dr("SubmitDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then discountProposalHeader.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then discountProposalHeader.ValidTo = CType(dr("ValidTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then discountProposalHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("BusinessSectorDetailID")) Then discountProposalHeader.BusinessSectorDetailID = CType(dr("BusinessSectorDetailID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Consideration")) Then discountProposalHeader.Consideration = dr("Consideration").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MMKSINotes")) Then discountProposalHeader.MMKSINotes = dr("MMKSINotes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FinalApproval")) Then discountProposalHeader.FinalApproval = CType(dr("FinalApproval"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then discountProposalHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then discountProposalHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then discountProposalHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then discountProposalHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then discountProposalHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("BBNAreaProvinceID")) Then
                discountProposalHeader.BBNAreaProvince = New Province(CType(dr("BBNAreaProvinceID"), Short))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                discountProposalHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("FleetCustomerDetailID")) Then
                discountProposalHeader.FleetCustomerDetail = New FleetCustomerDetail(CType(dr("FleetCustomerDetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPLID")) Then
                discountProposalHeader.SPL = New SPL(CType(dr("SPLID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("LeasingID")) Then
                discountProposalHeader.LeasingCompany = New LeasingCompany(CType(dr("LeasingID"), Integer))
            End If

            Return discountProposalHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(DiscountProposalHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DiscountProposalHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DiscountProposalHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

