#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Dealer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2006 - 8:49:16 AM
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

    Public Class DealerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealer"
        Private m_UpdateStatement As String = "up_UpdateDealer"
        Private m_RetrieveStatement As String = "up_RetrieveDealer"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealer As Dealer = Nothing
            While dr.Read

                dealer = Me.CreateObject(dr)

            End While

            Return dealer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerList As ArrayList = New ArrayList

            While dr.Read
                Dim dealer As Dealer = Me.CreateObject(dr)
                dealerList.Add(dealer)
            End While

            Return dealerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealer As Dealer = CType(obj, Dealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealer As Dealer = CType(obj, Dealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, dealer.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, dealer.DealerName)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, dealer.Status)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, dealer.Title)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, dealer.SearchTerm1)
            DbCommandWrapper.AddInParameter("@SearchTerm2", DbType.AnsiString, dealer.SearchTerm2)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, dealer.Address)
            DbCommandWrapper.AddInParameter("@ZipCode", DbType.AnsiString, dealer.ZipCode)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, dealer.Phone)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, dealer.Fax)
            DbCommandWrapper.AddInParameter("@Website", DbType.AnsiString, dealer.Website)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, dealer.Email)
            DbCommandWrapper.AddInParameter("@SalesUnitFlag", DbType.AnsiString, dealer.SalesUnitFlag)
            DbCommandWrapper.AddInParameter("@ServiceFlag", DbType.AnsiString, dealer.ServiceFlag)
            DbCommandWrapper.AddInParameter("@SparepartFlag", DbType.AnsiString, dealer.SparepartFlag)
            DbCommandWrapper.AddInParameter("@SPANumber", DbType.AnsiString, dealer.SPANumber)
            DbCommandWrapper.AddInParameter("@SPADate", DbType.DateTime, dealer.SPADate)
            DbCommandWrapper.AddInParameter("@FreePPh22Indicator", DbType.Int32, dealer.FreePPh22Indicator)
            DbCommandWrapper.AddInParameter("@FreePPh22From", DbType.DateTime, dealer.FreePPh22From)
            DbCommandWrapper.AddInParameter("@FreePPh22To", DbType.DateTime, dealer.FreePPh22To)
            DBCommandWrapper.AddInParameter("@LegalStatus", DbType.AnsiString, dealer.LegalStatus)
            DBCommandWrapper.AddInParameter("@DueDate", DbType.Int32, dealer.DueDate)
            DBCommandWrapper.AddInParameter("@AgreementNo", DbType.AnsiString, dealer.AgreementNo)
            DBCommandWrapper.AddInParameter("@AgreementDate", DbType.DateTime, dealer.AgreementDate)
            DBCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, dealer.CreditAccount)

            DbCommandWrapper.AddInParameter("@OrganizationBranchType", DbType.Int16, dealer.OrganizationBranchType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealer.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MainDealerID", DbType.Int32, Me.GetRefObject(dealer.MainDealer))
            DbCommandWrapper.AddInParameter("@ParentDealerID", DbType.Int16, Me.GetRefObject(dealer.ParentDealer))
            DbCommandWrapper.AddInParameter("@MainAreaID", DbType.Int32, Me.GetRefObject(dealer.MainArea))
            DbCommandWrapper.AddInParameter("@Area1ID", DbType.Int32, Me.GetRefObject(dealer.Area1))
            DbCommandWrapper.AddInParameter("@Area2ID", DbType.Int32, Me.GetRefObject(dealer.Area2))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(dealer.City))
            DbCommandWrapper.AddInParameter("@DealerGroupID", DbType.Int32, Me.GetRefObject(dealer.DealerGroup))
            DbCommandWrapper.AddInParameter("@ProvinceID", DbType.Int32, Me.GetRefObject(dealer.Province))

            DbCommandWrapper.AddInParameter("@WSCNO", DbType.AnsiString, dealer.WSCNO)
            DbCommandWrapper.AddInParameter("@ReconAccount", DbType.AnsiString, dealer.ReconAccount)
            DbCommandWrapper.AddInParameter("@SortKey", DbType.AnsiString, dealer.SortKey)
            DbCommandWrapper.AddInParameter("@CashManagementGroup", DbType.AnsiString, dealer.CashManagementGroup)
            DbCommandWrapper.AddInParameter("@PaymentBlock", DbType.AnsiString, dealer.PaymentBlock)
            DbCommandWrapper.AddInParameter("@CustomerLegal", DbType.Int32, dealer.CustomerLegal)
            DbCommandWrapper.AddInParameter("@TaxCode1", DbType.AnsiString, dealer.TaxCode1)
            DbCommandWrapper.AddInParameter("@NickNameDigital", DbType.AnsiString, dealer.NickNameDigital)
            DbCommandWrapper.AddInParameter("@NickNameEcommerce", DbType.AnsiString, dealer.NickNameEcommerce)
            DbCommandWrapper.AddInParameter("@Longitude", DbType.AnsiString, dealer.Longitude)
            DbCommandWrapper.AddInParameter("@Latitude", DbType.AnsiString, dealer.Latitude)
            DbCommandWrapper.AddInParameter("@Publish", DbType.Boolean, dealer.Publish)

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

            Dim dealer As Dealer = CType(obj, Dealer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealer.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, dealer.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, dealer.DealerName)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, dealer.Status)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, dealer.Title)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, dealer.SearchTerm1)
            DbCommandWrapper.AddInParameter("@SearchTerm2", DbType.AnsiString, dealer.SearchTerm2)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, dealer.Address)
            DbCommandWrapper.AddInParameter("@ZipCode", DbType.AnsiString, dealer.ZipCode)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, dealer.Phone)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, dealer.Fax)
            DbCommandWrapper.AddInParameter("@Website", DbType.AnsiString, dealer.Website)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, dealer.Email)
            DbCommandWrapper.AddInParameter("@SalesUnitFlag", DbType.AnsiString, dealer.SalesUnitFlag)
            DbCommandWrapper.AddInParameter("@ServiceFlag", DbType.AnsiString, dealer.ServiceFlag)
            DbCommandWrapper.AddInParameter("@SparepartFlag", DbType.AnsiString, dealer.SparepartFlag)
            DbCommandWrapper.AddInParameter("@SPANumber", DbType.AnsiString, dealer.SPANumber)
            DbCommandWrapper.AddInParameter("@SPADate", DbType.DateTime, dealer.SPADate)
            DbCommandWrapper.AddInParameter("@FreePPh22Indicator", DbType.Int32, dealer.FreePPh22Indicator)
            DbCommandWrapper.AddInParameter("@FreePPh22From", DbType.DateTime, dealer.FreePPh22From)
            DbCommandWrapper.AddInParameter("@FreePPh22To", DbType.DateTime, dealer.FreePPh22To)
            DBCommandWrapper.AddInParameter("@LegalStatus", DbType.AnsiString, dealer.LegalStatus)
            DBCommandWrapper.AddInParameter("@DueDate", DbType.Int32, dealer.DueDate)
            DBCommandWrapper.AddInParameter("@AgreementNo", DbType.AnsiString, dealer.AgreementNo)
            DBCommandWrapper.AddInParameter("@AgreementDate", DbType.DateTime, dealer.AgreementDate)
            DBCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, dealer.CreditAccount)

            DbCommandWrapper.AddInParameter("@OrganizationBranchType", DbType.Int16, dealer.OrganizationBranchType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ParentDealerID", DbType.Int16, Me.GetRefObject(dealer.ParentDealer))
            DbCommandWrapper.AddInParameter("@MainDealerID", DbType.Int32, Me.GetRefObject(dealer.MainDealer))
            DbCommandWrapper.AddInParameter("@MainAreaID", DbType.Int32, Me.GetRefObject(dealer.MainArea))
            DbCommandWrapper.AddInParameter("@Area1ID", DbType.Int32, Me.GetRefObject(dealer.Area1))
            DbCommandWrapper.AddInParameter("@Area2ID", DbType.Int32, Me.GetRefObject(dealer.Area2))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(dealer.City))
            DbCommandWrapper.AddInParameter("@DealerGroupID", DbType.Int32, Me.GetRefObject(dealer.DealerGroup))
            DbCommandWrapper.AddInParameter("@ProvinceID", DbType.Int32, Me.GetRefObject(dealer.Province))

            DbCommandWrapper.AddInParameter("@WSCNO", DbType.AnsiString, dealer.WSCNO)
            DbCommandWrapper.AddInParameter("@ReconAccount", DbType.AnsiString, dealer.ReconAccount)
            DbCommandWrapper.AddInParameter("@SortKey", DbType.AnsiString, dealer.SortKey)
            DbCommandWrapper.AddInParameter("@CashManagementGroup", DbType.AnsiString, dealer.CashManagementGroup)
            DbCommandWrapper.AddInParameter("@PaymentBlock", DbType.AnsiString, dealer.PaymentBlock)
            DbCommandWrapper.AddInParameter("@CustomerLegal", DbType.Int32, dealer.CustomerLegal)
            DbCommandWrapper.AddInParameter("@TaxCode1", DbType.AnsiString, dealer.TaxCode1)
            DbCommandWrapper.AddInParameter("@NickNameDigital", DbType.AnsiString, dealer.NickNameDigital)
            DbCommandWrapper.AddInParameter("@NickNameEcommerce", DbType.AnsiString, dealer.NickNameEcommerce)
            DbCommandWrapper.AddInParameter("@Longitude", DbType.AnsiString, dealer.Longitude)
            DbCommandWrapper.AddInParameter("@Latitude", DbType.AnsiString, dealer.Latitude)
            DbCommandWrapper.AddInParameter("@Publish", DbType.Boolean, dealer.Publish)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Dealer

            Dim dealer As Dealer = New Dealer

            dealer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then dealer.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then dealer.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then dealer.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Title")) Then dealer.Title = dr("Title").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SearchTerm1")) Then dealer.SearchTerm1 = dr("SearchTerm1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SearchTerm2")) Then dealer.SearchTerm2 = dr("SearchTerm2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then dealer.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ZipCode")) Then dealer.ZipCode = dr("ZipCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then dealer.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Fax")) Then dealer.Fax = dr("Fax").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Website")) Then dealer.Website = dr("Website").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then dealer.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesUnitFlag")) Then dealer.SalesUnitFlag = dr("SalesUnitFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceFlag")) Then dealer.ServiceFlag = dr("ServiceFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SparepartFlag")) Then dealer.SparepartFlag = dr("SparepartFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPANumber")) Then dealer.SPANumber = dr("SPANumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPADate")) Then dealer.SPADate = CType(dr("SPADate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FreePPh22Indicator")) Then dealer.FreePPh22Indicator = CType(dr("FreePPh22Indicator"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FreePPh22From")) Then dealer.FreePPh22From = CType(dr("FreePPh22From"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FreePPh22To")) Then dealer.FreePPh22To = CType(dr("FreePPh22To"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LegalStatus")) Then dealer.LegalStatus = dr("LegalStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DueDate")) Then dealer.DueDate = CType(dr("DueDate"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AgreementNo")) Then dealer.AgreementNo = dr("AgreementNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AgreementDate")) Then dealer.AgreementDate = CType(dr("AgreementDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then dealer.CreditAccount = dr("CreditAccount").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("OrganizationBranchType")) Then dealer.OrganizationBranchType = CType(dr("OrganizationBranchType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("WSCNO")) Then dealer.WSCNO = dr("WSCNO").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SortKey")) Then dealer.SortKey = dr("SortKey").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CashManagementGroup")) Then dealer.CashManagementGroup = dr("CashManagementGroup").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentBlock")) Then dealer.PaymentBlock = dr("PaymentBlock").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerLegal")) Then dealer.CustomerLegal = CInt(dr("CustomerLegal"))
            If Not dr.IsDBNull(dr.GetOrdinal("TaxCode1")) Then dealer.TaxCode1 = dr("TaxCode1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NickNameDigital")) Then dealer.NickNameDigital = dr("NickNameDigital").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NickNameEcommerce")) Then dealer.NickNameEcommerce = dr("NickNameEcommerce").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Longitude")) Then dealer.Longitude = dr("Longitude").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Latitude")) Then dealer.Latitude = dr("Latitude").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Publish")) Then dealer.Publish = CType(dr("Publish"), Boolean)

            If Not dr.IsDBNull(dr.GetOrdinal("MainDealerID")) Then
                dealer.MainDealer = New dealer(CType(dr("MainDealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ParentDealerID")) Then
                dealer.ParentDealer = New Dealer(CType(dr("ParentDealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MainAreaID")) Then
                dealer.MainArea = New MainArea(CType(dr("MainAreaID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("Area1ID")) Then
                dealer.Area1 = New Area1(CType(dr("Area1ID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("Area2ID")) Then
                dealer.Area2 = New Area2(CType(dr("Area2ID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                dealer.City = New City(CType(dr("CityID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerGroupID")) Then
                dealer.DealerGroup = New DealerGroup(CType(dr("DealerGroupID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceID")) Then
                dealer.Province = New Province(CType(dr("ProvinceID"), Integer))
            End If


            Return dealer

        End Function

        Private Sub SetTableName()

            If Not (GetType(Dealer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Dealer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Dealer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

