Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Service
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.WebApi.Models

Public Class FrmPreCustomerUpdateForm
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SAPCustomerFacade As New SAPCustomerFacade(User)
    Private _stdcodeFacade As New StandardCodeFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper
#End Region

#Region "Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'InitiateAuthorization()

        If Not IsPostBack Then
            BindCustomerType(ddlCostumerType)
            BindAgeSegment(ddlAge)
            BindInformationType(ddlType)
            BindCustomerPurpose(ddlPurpose)
            'BindInformationSource(ddlSource, False)
            BindGender(ddlGender)
            BindStatusResponse(ddlStatusDetail)
            BindCustomerStatus(ddlStatus)
            BindStateCode()
            BindLeadStatus()
            BindBusinessSector()
            BindStatusCode()
            BindDdlBabitEventType(ddlBabitEventType)
            'CR SFID
            BindLeadTypeIdentitas(ddlTipeKartuIdentitas, 0)
            BindLeadTypePekerjaan(ddlPekerjaan)
            BindLeadTypeBBN(ddlTypeBBN)
            BindLeadTypeRating(ddlRating)
            TxtBlanko.Visible = False


            '
            'CommonFunction.BindFromEnum("SAPCustomerStatus", ddlStatus, User, True, "NameStatus", "ValStatus")

            lblPopUpEvent.Attributes("onclick") = "ShowPPEventDealerSelection();"

            Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            lblDealerCode.Text = objuser.Dealer.DealerCode
            lblDealerName.Text = objuser.Dealer.DealerName
            BindControlsAttribute()
            'If Request.QueryString("isBack") <> "1" Then
            '    Initialize()
            '    BindControlsAttribute()
            'Else
            '    Dim arlDataGet As ArrayList = sessHelper.GetSession("DataForSAPReturn")
            '    txtSalesmanID.Text = arlDataGet(1)
            '    txtSalesmanName.Text = arlDataGet(2)
            '    'lblDateFrom.Text = arlDataGet(3)
            '    'lblDateUntil.Text = arlDataGet(4)
            '    txtSalesmanCode.Value = arlDataGet(3)
            '    BindControlsAttribute()
            'End If

            If Not IsNothing(Request.QueryString("CustId")) Then

                Dim _customerID As Integer = CInt(Request.QueryString("CustId"))
                sessHelper.SetSession("SessSAPCustomerID", _customerID)
                If Not IsNothing(Request.QueryString("mode")) Then
                    Dim _mode As String = Request.QueryString("mode")
                    BindDataCustomer(_customerID, _mode)
                Else
                    BindDataCustomer(_customerID, "view")
                End If
            End If

        Else
        End If


        lblVehicleType.Attributes("onclick") = "ShowPopUpVechileType();"
        txtSalesmanID.Attributes.Add("readonly", "readonly")
        txtSalesmanName.Attributes.Add("readonly", "readonly")
        'txtVehicleType.Attributes.Add("readonly", "readonly")
        'CR SPK
        txtCountryCode.Attributes.Add("readonly", "readonly")
        'txtCountryName.Attributes.Add("readonly", "readonly")
        lblSearchCountryCode.Attributes("onclick") = "ShowPopUpSPKMasterCountryCode();"
        '
    End Sub
    'cr sfid
    Private Sub BindLeadTypeIdentitas(ByVal ddl As DropDownList, ByVal type As Integer)
        ddlTipeKartuIdentitas.DataSource = New StandardCodeFacade(User).RetrieveByValueType(type, "IdentityTypeSPK")
        ddlTipeKartuIdentitas.DataTextField = "ValueCode"
        ddlTipeKartuIdentitas.DataValueField = "ValueId"
        ddlTipeKartuIdentitas.DataBind()
        ddlTipeKartuIdentitas.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))
        ddlTipeKartuIdentitas.SelectedIndex = 0
    End Sub
    Private Sub BindLeadTypeBBN(ByVal ddl As DropDownList)
        ddlTypeBBN.DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumSAPCustomer.BBNType")
        ddlTypeBBN.DataTextField = "ValueDesc"
        ddlTypeBBN.DataValueField = "ValueId"
        ddlTypeBBN.DataBind()
        ddlTypeBBN.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlTypeBBN.SelectedIndex = 0
    End Sub
    Private Sub BindLeadTypeRating(ByVal ddl As DropDownList)
        ddlRating.DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumSAPCustomerStatus.SAPCustomerStatus")
        ddlRating.DataTextField = "ValueDesc"
        ddlRating.DataValueField = "ValueId"
        ddlRating.DataBind()
        ddlRating.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlRating.SelectedIndex = 0
    End Sub
    Private Sub BindLeadPrice(ByVal ddl As DropDownList, ByVal Type As String, ByVal Category As String)

        Dim arrVechileType As ArrayList
        Dim TipeId As String
        Dim TypeID As Integer
        Dim objVechileType As DealerVehiclePriceDetail
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "VechileColorCode", MatchType.Exact, Type))
        criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "VechileTypeCode", MatchType.Exact, Category))
        arrVechileType = New DealerVehiclePriceDetailFacade(User).Retrieve(criterias)
        If Not IsNothing(arrVechileType) Then
            If arrVechileType.Count = 0 Then
                TipeId = ""
            Else
                'TypeID = arrVechileType(0)
                objVechileType = CType(arrVechileType(0), DealerVehiclePriceDetail)
                TipeId = "1"
            End If

        Else
            TipeId = ""
        End If
        If TipeId = "" Then
            TypeID = 0
        Else
            TypeID = objVechileType.ID
        End If

        Dim objSAPCustomer As SAPCustomer = sessHelper.GetSession("SessSAPCustomer")
        Dim arlNew As New ArrayList
        If Not IsNothing(Session("CURRENTCOLOR")) Then
            If Session("CURRENTCOLOR") <> ddlWarna.SelectedValue Then
                arlNew = New DealerVehiclePriceDetailFacade(User).RetrieveByCategory(Type, Category, ddlCostumerType.SelectedValue, objSAPCustomer.Dealer.ID)
            Else
                arlNew = New DealerVehiclePriceDetailFacade(User).RetrieveByCategory(Type, Category, ddlCostumerType.SelectedValue, objSAPCustomer.Dealer.ID, objSAPCustomer.DealerVehiclePriceDetail.ID)
            End If
        Else
            arlNew = New DealerVehiclePriceDetailFacade(User).RetrieveByCategory(Type, Category, ddlCostumerType.SelectedValue, objSAPCustomer.Dealer.ID)
        End If
        'arlNew = New DealerVehiclePriceDetailFacade(User).RetrieveByCategory(Type, Category, ddlCostumerType.SelectedValue, objSAPCustomer.Dealer.ID, objSAPCustomer.DealerVehiclePriceDetail.ID)
        ddlHargaPriceListDealer.Items.Clear()
        ddlHargaPriceListDealer.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlHargaPriceListDealer.SelectedIndex = 0

        For Each itemNew As DealerVehiclePriceDetail In arlNew
            Dim objDealerVehiclePrice As New DealerVehiclePrice
            Dim arrDealerVehiclePrice As ArrayList
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(DealerVehiclePrice), "GUID", MatchType.Exact, itemNew.DealerVehiclePriceGUID))
            criterias2.opAnd(New Criteria(GetType(DealerVehiclePrice), "Dealer.DealerCode", MatchType.Exact, lblDealerCode.Text))
            arrDealerVehiclePrice = New DealerVehiclePriceFacade(User).Retrieve(criterias2)
            If Not IsNothing(arrDealerVehiclePrice) Then
                If arrDealerVehiclePrice.Count > 0 Then
                    objDealerVehiclePrice = CType(arrDealerVehiclePrice(0), DealerVehiclePrice)
                    If Not IsNothing(objDealerVehiclePrice) Then
                        Dim listItem As New ListItem(itemNew.OTR.ToString("#,##0"), itemNew.ID)
                        ddlHargaPriceListDealer.Items.Add(listItem)
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub BindDataCustomer(ByVal _customerID As Integer, ByVal mode As String)
        Dim arr As New ArrayList
        Dim total As Integer
        Dim isDNET As Boolean = False
        Session("CURRENTCOLOR") = Nothing

        Dim objSAPCustomer As SAPCustomer = New SAPCustomerFacade(User).Retrieve(_customerID)
        If Not IsNothing(objSAPCustomer) AndAlso objSAPCustomer.ID > 0 Then
            sessHelper.SetSession("SessSAPCustomer", objSAPCustomer)

            Dim _criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            _criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumInformationSource.InformationSource.DNet"))
            _criterias.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, objSAPCustomer.InformationSource))
            isDNET = _stdcodeFacade.Retrieve(_criterias).Count > 0

            If Not isDNET Then
                BindInformationSource(ddlSource, True)
            Else
                BindInformationSource(ddlSource, False)
            End If

            'If objSAPCustomer.SalesforceID.Length > 0 Then
            'BindInformationSource(ddlSource, True)
            'Else
            'BindInformationSource(ddlSource, False)
            'End If
            If Not IsNothing(objSAPCustomer.SalesmanHeader) Then
                txtSalesmanCode.Value = objSAPCustomer.SalesmanHeader.ID
                txtSalesmanID.Text = objSAPCustomer.SalesmanHeader.SalesmanCode
                txtSalesmanName.Text = objSAPCustomer.SalesmanHeader.Name
            End If
            If Not IsNothing(objSAPCustomer.Dealer) Then
                lblDealerCode.Text = objSAPCustomer.Dealer.DealerCode
                lblDealerName.Text = objSAPCustomer.Dealer.DealerName
            End If
            txtCustomerName.Text = objSAPCustomer.CustomerName
            txtCustomerAddress.Text = objSAPCustomer.CustomerAddress
            txtEmail.Text = objSAPCustomer.Email

            txtTelp.Text = objSAPCustomer.Phone
            If Not IsNothing(objSAPCustomer.VechileType) Then
                txtVehicleType.Text = objSAPCustomer.VechileType.VechileTypeCode
            Else
                txtVehicleType.Text = String.Empty
            End If
            'CR SPK
            txtCountryCode.Text = objSAPCustomer.CountryCode
            '
            txtQty.Text = objSAPCustomer.Qty
            txtCurrVehicleBrand.Text = objSAPCustomer.CurrVehicleBrand
            txtCurrVehicleType.Text = objSAPCustomer.CurrVehicleType
            txtNote.Text = objSAPCustomer.Note
            txtWebID.Text = objSAPCustomer.WebID

            ddlAge.SelectedValue = objSAPCustomer.AgeSegment
            ddlCostumerType.SelectedValue = objSAPCustomer.CustomerType
            ddlCostumerType_SelectedIndexChanged(ddlCostumerType, Nothing)
            ddlGender.SelectedValue = objSAPCustomer.Sex
            ddlPurpose.SelectedValue = objSAPCustomer.CustomerPurpose
            ddlSource.SelectedValue = objSAPCustomer.InformationSource
            ddlStatus.SelectedValue = objSAPCustomer.Status
            'If objSAPCustomer.Status = CInt(EnumSAPCustomerStatus.SAPCustomerStatus.Deal_SPK) Then
            '    Dim arl As ArrayList = EnumSAPCustStatus.RetriveSAPCustomerStatus(True, True)
            '    ddlStatus.Items.Clear()
            '    For Each item As EnumSAPCustStatus In arl
            '        ddlStatus.Items.Add(New ListItem(item.NameStatus, item.ValStatus))
            '    Next
            '    ddlStatus.SelectedValue = objSAPCustomer.Status
            '    ddlStatus.Enabled = False
            'Else
            '    ddlStatus.SelectedValue = objSAPCustomer.Status
            '    ddlStatus.Enabled = True
            'End If

            IntcalBirthDate.Value = objSAPCustomer.BirthDate
            IntcalEstimatedCloseDate.Value = objSAPCustomer.EstimatedCloseDate
            txtCampaignName.Text = objSAPCustomer.CampaignName

            txtDesc.Text = objSAPCustomer.Description

            ddlStateCode.SelectedValue = objSAPCustomer.StateCode
            ddlStatusCode.SelectedValue = objSAPCustomer.StatusCode
            ddlLeadStatus.SelectedValue = objSAPCustomer.LeadStatus
            If Not IsNothing(objSAPCustomer.BusinessSectorDetail) Then
                ddlBusinessSector.SelectedValue = objSAPCustomer.BusinessSectorDetail.ID
            End If
            ddlType.SelectedValue = objSAPCustomer.InformationType
            'cr sfid
            TxtNamaBelakang.Text = objSAPCustomer.Name2
            TxtNoTelp.Text = objSAPCustomer.Telp
            TxtNomorIdentitas.Text = objSAPCustomer.IdentityNumber
            If Not IsNothing(objSAPCustomer.VechileColor) Then
                BindLeadTypeColor(ddlWarna, txtVehicleType.Text)

                ddlWarna.SelectedValue = objSAPCustomer.VechileColor.ColorCode
                If Not IsNothing(objSAPCustomer.DealerVehiclePriceDetail) Then
                    Session("CURRENTCOLOR") = objSAPCustomer.VechileColor.ColorCode
                End If

                'BindLeadTypeColorView(ddlWarna, objSAPCustomer.VechileColor.ID)
            Else
                BindLeadTypeColor(ddlWarna, txtVehicleType.Text)
            End If
            'ddlWarna_SelectedIndexChanged(ddlWarna, Nothing)

            If Not IsNothing(objSAPCustomer.DealerVehiclePriceDetail) Then
                BindLeadPrice(ddlHargaPriceListDealer, ddlWarna.SelectedValue, txtVehicleType.Text)
                'Dim hargaPrice As Single = Single.Parse(objSAPCustomer.DealerVehiclePriceDetail.ID)
                ddlHargaPriceListDealer.SelectedValue = objSAPCustomer.DealerVehiclePriceDetail.ID
            Else
                BindLeadPrice(ddlHargaPriceListDealer, ddlWarna.SelectedValue, txtVehicleType.Text)
            End If
            'ddlHargaPriceListDealer_SelectedIndexChanged(ddlHargaPriceListDealer, Nothing)

            If Not IsNothing(objSAPCustomer.BlankoSPKDoc) AndAlso objSAPCustomer.BlankoSPKDoc <> String.Empty Then
                LnkDownloadTemplate.Visible = True
            Else
                LnkDownloadTemplate.Visible = False
            End If

            ddlTipeKartuIdentitas.SelectedValue = objSAPCustomer.IdentityType

            Dim priceReq As Single = Single.Parse(objSAPCustomer.CusReqPrice)
            TxtHargaPermintaanKonsumen.Text = priceReq.ToString("#,##0")
            Dim price As Single = Single.Parse(objSAPCustomer.CusReqDiscount)
            TxtJumlahDiskon.Text = price.ToString("#,##0")
            Dim priceBO As Single = Single.Parse(objSAPCustomer.BookingFee)
            TxtBookingFee.Text = priceBO.ToString("#,##0")
            ddlTypeBBN.SelectedValue = objSAPCustomer.BBNType
            ddlRating.SelectedValue = objSAPCustomer.Rating
            TxtNoBlankoSPK.Text = objSAPCustomer.BlankoSPKNo
            TxtKendaraanPembanding.Text = objSAPCustomer.VehicleComparison
            TxtBlanko.Text = objSAPCustomer.BlankoSPKDoc
            ddlPekerjaan.SelectedValue = objSAPCustomer.JobKind
            ddlTypeBBN.SelectedValue = objSAPCustomer.BBNType
            '
            sessHelper.SetSession("CurrentSalesmanHeader", objSAPCustomer)
        End If

        If mode = "edit" Then
            pnlEntry.Visible = True
            EnabledControl(True)
            ddlSource.Enabled = isDNET
            If objSAPCustomer.SalesforceID.Length > 0 Then
                ddlCostumerType.Enabled = False
                ddlType.Enabled = False
                ddlPurpose.Enabled = False
                'Edit 14/1/2021 Digital Lead
                'ddlStatus.Enabled = False
                txtWebID.Enabled = False
                'ddlAge.Enabled = False
                'ddlGender.Enabled = False
            End If
        Else
            pnlEntry.Visible = False
            EnabledControl(False)
        End If

        If txtCampaignName.Text.Trim <> "" Then
            Dim _oDealer As Dealer = New DealerFacade(User).Retrieve(lblDealerCode.Text)
            If IsNothing(_oDealer) Then _oDealer = New Dealer

            Dim oBabitHeader As New BabitHeader
            Dim dsBabitEvent As DataSet = New DataSet
            dsBabitEvent = New BabitHeaderFacade(User).RetrieveFromSPByPopUp(_oDealer.ID, txtCampaignName.Text, "", Nothing, Nothing, False, "")
            If dsBabitEvent.Tables.Count > 0 Then
                If dsBabitEvent.Tables(0).Rows.Count > 0 Then
                    txtCampaignName.Visible = True
                    lblPopUpEvent.Visible = True
                    ddlBabitEventType.SelectedValue = "0"  '--- Babit & Event
                Else
                    txtCampaignName.Visible = True
                    lblPopUpEvent.Visible = False
                    ddlBabitEventType.SelectedValue = "1"   '--- Lain - lain
                End If
            Else
                txtCampaignName.Visible = True
                lblPopUpEvent.Visible = False
                ddlBabitEventType.SelectedValue = "1"        '--- Lain - lain
            End If
            If Not mode = "edit" Then
                lblPopUpEvent.Visible = False
            End If
        Else
            txtCampaignName.Visible = False
            lblPopUpEvent.Visible = False
            ddlBabitEventType.SelectedIndex = 0     '---Silahkan Pilih
        End If

        BindDataResponse(dgCase.CurrentPageIndex)

    End Sub

    Private Sub BindLeadStatus()
        ddlLeadStatus.DataSource = New StandardCodeFacade(User).RetrieveByCategory("LeadStatus")
        ddlLeadStatus.DataTextField = "ValueDesc"
        ddlLeadStatus.DataValueField = "ValueId"
        ddlLeadStatus.DataBind()
        ddlLeadStatus.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlLeadStatus.SelectedIndex = 0
    End Sub

    Private Sub BindStateCode()
        ddlStateCode.DataSource = New StandardCodeFacade(User).RetrieveByCategory("LeadStateCode")
        ddlStateCode.DataTextField = "ValueDesc"
        ddlStateCode.DataValueField = "ValueId"
        ddlStateCode.DataBind()
        ddlStateCode.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlStateCode.SelectedIndex = 0
    End Sub

    Private Sub BindStatusCode()
        ddlStatusCode.DataSource = New StandardCodeFacade(User).RetrieveByCategory("LeadStatusCode")
        ddlStatusCode.DataTextField = "ValueDesc"
        ddlStatusCode.DataValueField = "ValueId"
        ddlStatusCode.DataBind()
        ddlStatusCode.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlStatusCode.SelectedIndex = 0
    End Sub

    Private Sub BindDdlBabitEventType(ByVal ddl As DropDownList)
        With ddl
            .Items.Add(New ListItem("Silahkan Pilih", "-1"))
            .Items.Add(New ListItem("Babit dan Event", "0"))
            .Items.Add(New ListItem("Lain - lain", "1"))
            .SelectedIndex = 0
        End With
        txtCampaignName.Visible = False
        lblPopUpEvent.Visible = False
    End Sub

    Private Sub BindBusinessSector()
        ddlBusinessSector.DataSource = New VWI_BusinessSectorFacade(User).RetrieveList()
        ddlBusinessSector.DataValueField = "ID"
        ddlBusinessSector.DataTextField = "BusinessName"
        ddlBusinessSector.DataBind()

        ddlBusinessSector.Items.Insert(0, New ListItem("Silahkan Pilih", ""))
    End Sub

    Private Sub BindDataResponse(ByVal iPage As Integer)
        Dim arr As New ArrayList
        Dim total As Integer

        Dim objSAPCustomer As SAPCustomer = CType(sessHelper.GetSession("SessSAPCustomer"), SAPCustomer)
        Dim _criterias As New CriteriaComposite(New Criteria(GetType(SAPCustomerResponse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        _criterias.opAnd(New Criteria(GetType(SAPCustomerResponse), "SAPCustomer.ID", MatchType.Exact, objSAPCustomer.ID))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(SAPCustomerResponse), "CreatedTime", Sort.SortDirection.ASC))

        'arr = New SAPCustomerResponseFacade(User).RetrieveByCriteria(_criterias, iPage, dgCase.PageSize, total)
        arr = New SAPCustomerResponseFacade(User).Retrieve(_criterias, sortColl)

        dgCase.VirtualItemCount = total
        dgCase.DataSource = arr
        dgCase.DataBind()

        If arr.Count > 0 Then
            Dim objCustResp As SAPCustomerResponse = CType(arr(arr.Count - 1), SAPCustomerResponse)
            ddlStatusDetail.SelectedValue = CType(objCustResp.Status, String)
        End If

    End Sub

    Private Sub BindCustomerType(ByVal ddl As DropDownList)
        ddl.Items.Clear()
        Dim arrList As ArrayList = New EnumTipePelangganCustomerRequest().RetrieveType
        For Each item As EnumTipePelanggan In arrList
            Dim listItem As New ListItem(item.NameTipe, item.ValTipe)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub


    Private Sub BindCustomerStatus(ByVal ddl As DropDownList)
        Dim arl As ArrayList = EnumSAPCustStatus.RetriveSAPCustomerStatus(True, False)
        ddlStatus.Items.Clear()
        For Each item As EnumSAPCustStatus In arl
            'If item.ValStatus <> EnumSAPCustomerStatus.SAPCustomerStatus.Deal_SPK Then
            ddlStatus.Items.Add(New ListItem(item.NameStatus, item.ValStatus))
            'End If
        Next
    End Sub

    Private Sub BindGender(ByVal ddl As DropDownList)
        ddl.Items.Clear()
        Dim arrList As ArrayList = EnumGenderOp.RetriveSalesGender(True)
        For Each item As EnumGenderOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindStatusResponse(ByVal ddl As DropDownList)
        ddl.Items.Clear()
        Dim arrList As ArrayList = EnumSAPCustomerResponseOp.RetriveResponse(True)
        For Each item As EnumSAPCustomerResponseOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindAgeSegment(ByVal ddl As DropDownList)
        ddl.Items.Clear()
        Dim arrList As ArrayList = EnumAgeSegmentOp.RetriveAgeSegment(True)
        For Each item As EnumAgeSegmentOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindInformationType(ByVal ddl As DropDownList)
        ddl.Items.Clear()
        Dim arrList As ArrayList = EnumInformationTypeOp.RetriveInformationType(True)
        For Each item As EnumInformationTypeOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next

    End Sub

    Private Sub BindInformationSource(ByVal ddl As DropDownList, ByVal IsSF_data As Boolean)
        Dim arrList As ArrayList = EnumInformationSourceOp.RetriveInformationSource(True, True)
        If IsSF_data Then
            arrList = EnumInformationSourceOp.RetriveInformationSource(True, False)
        Else
            arrList = EnumInformationSourceOp.RetriveInformationSource(True, True)
        End If

        For Each item As EnumInformationSourceOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindCustomerPurpose(ByVal ddl As DropDownList)
        ddl.Items.Clear()
        Dim arrList As ArrayList = EnumCustomerPurposeOp.RetriveCustomerPurpose(True)
        For Each item As EnumCustomerPurposeOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

#End Region

#Region "Custom"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.CustomerProspekView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SAP - Konsumen Prospek")
        End If
    End Sub

    Private Function CekProspekCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.CustomerProspekCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub BindControlsAttribute()
        lblSearchSalesman.Attributes("onclick") = "ShowPopUpSAPRegisterSalesman();"
    End Sub

    Private Sub Initialize()
        ClearData()
        'ViewState("CurrentSortColumn") = "AreaCode"
        'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        'ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub ClearData()

        txtSalesmanCode.Value = String.Empty
        txtSalesmanID.Text = String.Empty
        txtSalesmanName.Text = String.Empty

        txtCustomerName.Text = String.Empty
        txtCustomerAddress.Text = String.Empty
        txtEmail.Text = String.Empty
        txtTelp.Text = String.Empty
        'CR SPK
        txtCountryCode.Text = String.Empty
        '
        txtVehicleType.Text = String.Empty
        txtQty.Text = String.Empty
        txtCurrVehicleBrand.Text = String.Empty
        txtCurrVehicleType.Text = String.Empty
        txtNote.Text = String.Empty
        txtWebID.Text = String.Empty

        ddlAge.SelectedIndex = 0
        ddlCostumerType.SelectedIndex = 0
        ddlGender.SelectedIndex = 0
        ddlPurpose.SelectedIndex = 0
        ddlSource.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        ddlType.SelectedIndex = 0

        ddlStatusDetail.SelectedIndex = 0
        txtComment.Text = String.Empty
        ddlBabitEventType.SelectedIndex = 0
        txtCampaignName.Text = String.Empty
        'cr SFID
        ddlTipeKartuIdentitas.SelectedIndex = 0
        ddlPekerjaan.SelectedIndex = 0
        ddlTypeBBN.SelectedIndex = 0
        ddlRating.SelectedIndex = 0
        ddlWarna.SelectedIndex = 0
        ddlHargaPriceListDealer.SelectedIndex = 0
        TxtNamaBelakang.Text = String.Empty
        TxtNoTelp.Text = String.Empty
        TxtNomorIdentitas.Text = String.Empty
        TxtHargaPermintaanKonsumen.Text = String.Empty
        TxtJumlahDiskon.Text = String.Empty
        TxtBookingFee.Text = String.Empty
        TxtNoBlankoSPK.Text = String.Empty
        TxtKendaraanPembanding.Text = String.Empty

        '

    End Sub

    Private Sub EnabledControl(ByVal vBoolean As Boolean)

        txtCustomerName.Enabled = vBoolean
        txtCustomerAddress.Enabled = vBoolean
        txtEmail.Enabled = vBoolean
        txtTelp.Enabled = vBoolean
        'CR SPK
        txtCountryCode.Enabled = vBoolean
        '
        txtVehicleType.Enabled = vBoolean
        lblVehicleType.Visible = vBoolean
        txtQty.Enabled = vBoolean
        txtCurrVehicleBrand.Enabled = vBoolean
        txtCurrVehicleType.Enabled = vBoolean
        txtNote.Enabled = vBoolean
        txtWebID.Enabled = vBoolean
        ddlBabitEventType.Enabled = vBoolean
        txtCampaignName.Enabled = vBoolean
        lblPopUpEvent.Visible = vBoolean
        txtDesc.Enabled = vBoolean

        IntcalBirthDate.Enabled = vBoolean
        IntcalEstimatedCloseDate.Enabled = vBoolean

        ddlLeadStatus.Enabled = vBoolean
        ddlBusinessSector.Enabled = vBoolean
        ddlStateCode.Enabled = vBoolean
        ddlStatusCode.Enabled = vBoolean
        ddlAge.Enabled = vBoolean
        ddlCostumerType.Enabled = vBoolean
        ddlGender.Enabled = vBoolean
        ddlPurpose.Enabled = vBoolean
        ddlSource.Enabled = vBoolean
        ddlStatus.Enabled = vBoolean
        ddlType.Enabled = vBoolean
        ddlStatusDetail.Enabled = vBoolean
        txtComment.Enabled = vBoolean
        lblSearchSalesman.Visible = vBoolean
        lbtnRefKode.Visible = vBoolean
        lnkReloadPlg.Visible = vBoolean
        'cr sfid
        TxtNamaBelakang.Enabled = vBoolean
        TxtNoTelp.Enabled = vBoolean
        ddlTipeKartuIdentitas.Enabled = vBoolean
        TxtNomorIdentitas.Enabled = vBoolean
        ddlPekerjaan.Enabled = vBoolean
        ddlWarna.Enabled = vBoolean
        ddlHargaPriceListDealer.Enabled = vBoolean
        TxtHargaPermintaanKonsumen.Enabled = vBoolean
        TxtJumlahDiskon.Enabled = vBoolean
        TxtBookingFee.Enabled = vBoolean
        ddlTypeBBN.Enabled = vBoolean
        ddlRating.Enabled = vBoolean
        TxtNoBlankoSPK.Enabled = vBoolean
        TxtKendaraanPembanding.Enabled = vBoolean

        '
        DataFile.Visible = vBoolean

        btnSave.Visible = vBoolean
    End Sub

    Private Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function

    Private Function IsPhoneValid(ByVal phoneNo As String, Optional ByVal phoneType As String = "") As String
        Dim strMessage As String = String.Empty

        'If phoneType = "Handphone" Then
        '    If Len(phoneNo) > 5 AndAlso Left(phoneNo, 2) <> "08" Then
        '        strMessage += "No. Handphone harus diawali dengan '08' (nol)"
        '    End If
        'End If
        'If phoneNo.Length > 20 Then
        '    If phoneType <> "" Then
        '        strMessage += "No " & phoneType & " cukup satu nomor."
        '    Else
        '        strMessage += "Cukup masukkan satu nomor."
        '    End If
        'End If
        'cr spk
        If phoneType = "Handphone" Then
            If Len(phoneNo) < 10 Then
                strMessage += "No. Handphone minimum 10 digit"
            End If
            If Len(phoneNo) > 13 Then
                strMessage += "No. Handphone maksiman 13 digit"
            End If
            ''remark for phase 2 req halimi
            'If Left(phoneNo, 1) <> "8" Then
            '    strMessage += "No. Handphone harus diawali dengan '8' (tanpa nol)"
            'End If
        End If
        'end
        Return strMessage
    End Function

#End Region

    Protected Sub lnkReloadPlg_Click(sender As Object, e As EventArgs) Handles lnkReloadPlg.Click
        If txtRefKodePelanggan.Text.Trim <> String.Empty Then
            Dim objCustomer As Customer = New CustomerDealerFacade(User).RetrieveByRefCode(txtRefKodePelanggan.Text.Trim, CType(sessHelper.GetSession("DEALER"), Dealer).ID)
            If objCustomer.ID <> 0 Then
                txtCustomerName.Text = objCustomer.Name1.ToUpper
                txtCustomerAddress.Text = objCustomer.Alamat.ToUpper
                txtEmail.Text = objCustomer.Email
                txtTelp.Text = objCustomer.PhoneNo
            Else
                MessageBox.Show("Referensi kode pelanggan tidak valid")
            End If
        Else
            MessageBox.Show("Referensi kode pelanggan tidak valid")
        End If
    End Sub

    Private Sub SaveOld()
        'Dim strMessage As String = ""
        'Dim facade As SAPCustomerFacade = New SAPCustomerFacade(User)
        'Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        ''If txtSalesmanID.Text = "" Then
        ''    MessageBox.Show("Salesman ID harus diisi dahulu !")
        ''    Return
        ''End If

        'If txtCustomerName.Text = "" Then
        '    strMessage &= "Nama customer harus diisi. \n"
        'End If

        'If txtCustomerAddress.Text = "" Then
        '    strMessage &= "Alamat customer harus diisi. \n"
        'End If

        'If ddlStatus.SelectedItem.Text = "" Then
        '    strMessage &= "Status harus dipilih.  \n"
        'End If

        'If ddlGender.SelectedIndex = -1 Then
        '    strMessage &= "Gender harus dipilih. \n"
        'End If

        'If ddlType.SelectedIndex = 0 Then
        '    strMessage &= "Tipe informasi harus dipilih. \n"
        'End If

        'If ddlPurpose.SelectedIndex = 0 Then
        '    strMessage &= "Tujuan konsumen harus dipilih. \n"
        'End If

        'If txtVehicleType.Text = "" Then
        '    strMessage &= "Tipe kendaraan harus dipilih. \n"
        'Else
        '    ' cek apakah data valid saat diinput
        '    Dim arrVechileType As ArrayList
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, txtVehicleType.Text))
        '    arrVechileType = New VechileTypeFacade(User).Retrieve(criterias)
        '    If Not IsNothing(arrVechileType) Then
        '        If arrVechileType.Count < 1 Then
        '            strMessage &= "Tipe kendaraan tidak valid. \n"
        '        End If
        '    End If
        'End If


        'If Val(txtQty.Text) = 0 Then
        '    strMessage &= "Qty harus diisi. \n"
        'End If

        'If ddlSource.SelectedIndex = 0 Then
        '    strMessage &= "Sumber informasi harus dipilih. \n"
        'End If

        'If txtTelp.Text.Trim.Length < 10 Then
        '    strMessage &= "Nomor telp tidak valid. \n"
        'End If

        'If Not EmailAddressCheck(txtEmail.Text.Trim) Then
        '    strMessage &= "Format email salah. \n"
        'End If

        'If strMessage.Trim <> "" Then
        '    MessageBox.Show(strMessage)
        '    Return
        'End If

        'Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtVehicleType.Text.Trim)

        'Dim objSAPCustomer As SAPCustomer = New SAPCustomer
        'Dim objSalesmanHeader As SalesmanHeader
        'If txtSalesmanID.Text.Trim <> String.Empty Then
        '    objSalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
        '    objSAPCustomer.SalesmanHeader = objSalesmanHeader
        'Else
        '    objSAPCustomer.SalesmanHeader = Nothing
        'End If

        'If Not IsNothing(objuser) Then
        '    objSAPCustomer.Dealer = objuser.Dealer
        'End If

        'objSAPCustomer.CustomerType = ddlCostumerType.SelectedValue
        'objSAPCustomer.Qty = Val(txtQty.Text)
        'objSAPCustomer.VechileType = objVechileType
        'objSAPCustomer.CustomerName = txtCustomerName.Text
        'objSAPCustomer.CustomerAddress = txtCustomerAddress.Text
        'objSAPCustomer.CustomerType = ddlCostumerType.SelectedValue
        'objSAPCustomer.Email = txtEmail.Text
        'objSAPCustomer.Status = ddlStatus.SelectedValue
        'objSAPCustomer.Phone = txtTelp.Text.Trim
        'objSAPCustomer.Sex = ddlGender.SelectedValue
        'objSAPCustomer.AgeSegment = ddlAge.SelectedValue
        'objSAPCustomer.InformationType = ddlType.SelectedValue
        'objSAPCustomer.InformationSource = ddlSource.SelectedValue
        'objSAPCustomer.CustomerPurpose = ddlPurpose.SelectedValue
        'objSAPCustomer.ProspectDate = New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)
        'objSAPCustomer.CurrVehicleBrand = txtCurrVehicleBrand.Text
        'objSAPCustomer.CurrVehicleType = txtCurrVehicleType.Text
        'objSAPCustomer.Note = txtNote.Text



        'Dim arrSAPCustomer As ArrayList
        'Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criteria.opAnd(New Criteria(GetType(SAPCustomer), "CustomerType", MatchType.Exact, ddlCostumerType.SelectedValue))
        'criteria.opAnd(New Criteria(GetType(SAPCustomer), "CustomerName", MatchType.Exact, txtCustomerName.Text))
        'criteria.opAnd(New Criteria(GetType(SAPCustomer), "CustomerAddress", MatchType.Exact, txtCustomerAddress.Text))
        'criteria.opAnd(New Criteria(GetType(SAPCustomer), "Phone", MatchType.Exact, txtTelp.Text))
        'criteria.opAnd(New Criteria(GetType(SAPCustomer), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        'criteria.opAnd(New Criteria(GetType(SAPCustomer), "VechileType.ID", MatchType.Exact, objVechileType.ID))
        'criteria.opAnd(New Criteria(GetType(SAPCustomer), "Sex", MatchType.Exact, ddlGender.SelectedValue))
        'criteria.opAnd(New Criteria(GetType(SAPCustomer), "InformationType", MatchType.Exact, ddlType.SelectedValue))
        'criteria.opAnd(New Criteria(GetType(SAPCustomer), "InformationSource", MatchType.Exact, ddlSource.SelectedValue))
        'criteria.opAnd(New Criteria(GetType(SAPCustomer), "CustomerPurpose", MatchType.Exact, ddlPurpose.SelectedValue))
        'criteria.opAnd(New Criteria(GetType(SAPCustomer), "AgeSegment", MatchType.Exact, ddlAge.SelectedValue))
        'criteria.opAnd(New Criteria(GetType(SAPCustomer), "ProspectDate", MatchType.Exact, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)))
        'If txtSalesmanID.Text.Trim <> String.Empty Then
        '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "SalesmanHeader.SalesmanCode", MatchType.Exact, txtSalesmanID.Text.Trim))
        'End If
        'If Not IsNothing(objuser) Then
        '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "Dealer.ID", MatchType.Exact, objuser.Dealer.ID))
        'End If

        'Dim result As Integer = -1

        'arrSAPCustomer = New SAPCustomerFacade(User).Retrieve(criteria)
        'If Not IsNothing(arrSAPCustomer) Then
        '    If arrSAPCustomer.Count > 0 Then
        '        'MessageBox.Show("Data sudah ada, tidak bisa di masukan lagi !")
        '        'Return
        '        For Each item As SAPCustomer In arrSAPCustomer
        '            objSAPCustomer.ID = item.ID
        '            objSAPCustomer.CustomerType = ddlCostumerType.SelectedValue
        '            objSAPCustomer.Qty = Val(txtQty.Text)
        '            objSAPCustomer.VechileType = objVechileType
        '            objSAPCustomer.CustomerName = txtCustomerName.Text
        '            objSAPCustomer.CustomerAddress = txtCustomerAddress.Text
        '            objSAPCustomer.CustomerType = ddlCostumerType.SelectedValue
        '            objSAPCustomer.Email = txtEmail.Text
        '            objSAPCustomer.Status = ddlStatus.SelectedValue
        '            objSAPCustomer.Phone = txtTelp.Text.Trim
        '            objSAPCustomer.Sex = ddlGender.SelectedValue
        '            objSAPCustomer.AgeSegment = ddlAge.SelectedValue
        '            objSAPCustomer.InformationType = ddlType.SelectedValue
        '            objSAPCustomer.InformationSource = ddlSource.SelectedValue
        '            objSAPCustomer.CustomerPurpose = ddlPurpose.SelectedValue
        '            objSAPCustomer.ProspectDate = New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)
        '            objSAPCustomer.CurrVehicleBrand = txtCurrVehicleBrand.Text
        '            objSAPCustomer.CurrVehicleType = txtCurrVehicleType.Text
        '            objSAPCustomer.Note = txtNote.Text
        '        Next

        '        result = facade.Update(objSAPCustomer)
        '    Else
        '        result = facade.Insert(objSAPCustomer)
        '    End If
        'Else
        '    result = facade.Insert(objSAPCustomer)
        'End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim strMessage As String = ""
        'cr sfid
        Dim valueOTR As String
        Dim intOTR As Integer
        '
        If Not Page.IsValid Then  '-- Postback validation
            Exit Sub
        End If
        'If txtTelp.Text.Trim.Length < 10 Then
        '    strMessage &= "Nomor telp tidak valid. \n"
        'End If

        strMessage &= IsPhoneValid(txtTelp.Text.Trim, "handphone")

        'CR SFID

        If Not (ddlCostumerType.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan) Then
            If TxtNomorIdentitas.Text.Trim.Length > 50 Then
                strMessage &= "Nomor Identitas Max 50 karakter. \n"
            End If
        Else
            If TxtNomorIdentitas.Text.Trim.Length > 16 Then
                strMessage &= "Nomor Identitas Max 16 karakter. \n"
            End If
        End If
        '

        If txtEmail.Text.Trim.Length > 0 Then
            If Not EmailAddressCheck(txtEmail.Text.Trim) Then
                strMessage &= "Format email salah. \n"
            End If
        End If

        If txtCustomerAddress.Text.Trim.Length > 60 Then
            strMessage &= "Alamat melebihi 60 karakter. \n"
        End If

        Dim objVechileType As VechileType
        If txtVehicleType.Text = "" Then
            strMessage &= "Tipe kendaraan harus dipilih. \n"
        Else
            ' cek apakah data valid saat diinput
            Dim arrVechileType As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, txtVehicleType.Text))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))
            arrVechileType = New VechileTypeFacade(User).Retrieve(criterias)
            If Not IsNothing(arrVechileType) Then
                If arrVechileType.Count = 0 Then
                    strMessage &= "Tipe kendaraan tidak valid. \n"
                Else
                    objVechileType = CType(arrVechileType(0), VechileType)
                End If
                'objVechileType = CType(arrVechileType(0), VechileType)
                'If arrVechileType.Count < 1 Then
                '    strMessage &= "Tipe kendaraan tidak valid. \n"
                'End If
            Else
                strMessage &= "Tipe kendaraan tidak valid. \n"
            End If
        End If

        'If IsCustomerExist() Then
        '    strMessage &= "Data konsumen dengan no telp : " & txtTelp.Text & " sudah ada dan belum SPK. \n"
        'End If
        If pnlEntry.Visible = True Then
            If (ddlStatusDetail.SelectedValue = 0) Then
                strMessage &= "Pilih status response. \n"
            End If
        End If

        'If objSAPCustomer.SalesforceID.Trim <> "" Then
        If (ddlStatusDetail.SelectedValue >= 4) Then
            strMessage &= "Untuk status respon " & EnumSAPCustomerResponse.GetStringValue(CInt(ddlStatusDetail.SelectedValue)) & " otomatis dari proses SPK. \n"
        End If
        'End If
        If (ddlStatus.SelectedValue >= 4) Then
            strMessage &= "Untuk status konsumen" & EnumSAPCustomerResponse.GetStringValue(CInt(ddlStatus.SelectedValue)) & " otomatis dari proses SPK. \n"
        End If

        If strMessage.Trim <> "" Then
            MessageBox.Show(strMessage)
            Exit Sub
        End If

        Dim objSAPCustomer As SAPCustomer = New SAPCustomer
        If Not IsNothing(sessHelper.GetSession("CurrentSalesmanHeader")) Then
            objSAPCustomer = CType(sessHelper.GetSession("CurrentSalesmanHeader"), SAPCustomer)
        End If

        Dim objSalesmanHeader As SalesmanHeader
        If txtSalesmanID.Text.Trim <> String.Empty Then
            objSalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
            objSAPCustomer.SalesmanHeader = objSalesmanHeader
        Else
            objSAPCustomer.SalesmanHeader = Nothing
        End If

        If Not IsNothing(objuser) Then
            objSAPCustomer.Dealer = objuser.Dealer
        End If

        objSAPCustomer.CustomerType = ddlCostumerType.SelectedValue
        objSAPCustomer.Qty = Val(txtQty.Text)
        If Not IsNothing(objVechileType) Then
            objSAPCustomer.VechileType = objVechileType
        Else
            objSAPCustomer.VechileType = Nothing
        End If
        objSAPCustomer.CustomerName = txtCustomerName.Text
        objSAPCustomer.CustomerAddress = txtCustomerAddress.Text
        objSAPCustomer.CustomerType = ddlCostumerType.SelectedValue
        objSAPCustomer.Email = txtEmail.Text
        objSAPCustomer.Status = ddlStatus.SelectedValue
        'CR SPK
        objSAPCustomer.CountryCode = txtCountryCode.Text.Trim
        '
        objSAPCustomer.Phone = txtTelp.Text.Trim
        objSAPCustomer.Sex = ddlGender.SelectedValue
        objSAPCustomer.AgeSegment = ddlAge.SelectedValue
        objSAPCustomer.InformationType = ddlType.SelectedValue
        objSAPCustomer.InformationSource = ddlSource.SelectedValue
        objSAPCustomer.CustomerPurpose = ddlPurpose.SelectedValue
        'objSAPCustomer.ProspectDate = New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)
        objSAPCustomer.CurrVehicleBrand = txtCurrVehicleBrand.Text
        objSAPCustomer.CurrVehicleType = txtCurrVehicleType.Text
        objSAPCustomer.Note = txtNote.Text
        objSAPCustomer.WebID = txtWebID.Text

        'Save additional data
        Dim objBusinessSectorDetail As BusinessSectorDetail
        If ddlBusinessSector.SelectedValue <> String.Empty Then
            objBusinessSectorDetail = New BusinessSectorDetailFacade(User).Retrieve(Integer.Parse(ddlBusinessSector.SelectedValue))
            objSAPCustomer.BusinessSectorDetail = objBusinessSectorDetail
        Else
            objSAPCustomer.BusinessSectorDetail = Nothing
        End If

        objSAPCustomer.BirthDate = IntcalBirthDate.Value
        objSAPCustomer.Description = txtDesc.Text
        objSAPCustomer.EstimatedCloseDate = IntcalEstimatedCloseDate.Value

        If ddlStateCode.SelectedValue <> String.Empty Then
            objSAPCustomer.StateCode = ddlStateCode.SelectedValue
        End If
        If ddlStatusCode.SelectedValue <> String.Empty Then
            objSAPCustomer.StatusCode = ddlStatusCode.SelectedValue
        End If
        If ddlLeadStatus.SelectedValue <> String.Empty Then
            objSAPCustomer.LeadStatus = ddlLeadStatus.SelectedValue
        End If

        objSAPCustomer.CampaignName = txtCampaignName.Text

        'cr sfid
        objSAPCustomer.Name2 = TxtNamaBelakang.Text
        objSAPCustomer.Telp = TxtNoTelp.Text
        objSAPCustomer.IdentityNumber = TxtNomorIdentitas.Text
        objSAPCustomer.CusReqPrice = TxtHargaPermintaanKonsumen.Text
        objSAPCustomer.CusReqDiscount = TxtJumlahDiskon.Text
        objSAPCustomer.BookingFee = TxtBookingFee.Text
        objSAPCustomer.BlankoSPKNo = TxtNoBlankoSPK.Text
        objSAPCustomer.VehicleComparison = TxtKendaraanPembanding.Text

        objSAPCustomer.IdentityType = ddlTipeKartuIdentitas.SelectedValue
        If ddlPekerjaan.SelectedValue <> String.Empty Then
            objSAPCustomer.JobKind = ddlPekerjaan.SelectedValue
        End If
        If ddlTypeBBN.SelectedValue <> String.Empty Then
            objSAPCustomer.BBNType = ddlTypeBBN.SelectedValue
        End If
        If ddlRating.SelectedValue <> String.Empty Then
            objSAPCustomer.Rating = ddlRating.SelectedValue
        End If

        Dim objDealerVehiclePriceDetail As DealerVehiclePriceDetail
        If ddlHargaPriceListDealer.SelectedIndex > 0 Then
            'Dim objDealerPriceListDealer As ArrayList
            'Dim objDealerPrice As DealerVehiclePriceDetail
            'Dim Price As Integer
            'valueOTR = ddlHargaPriceListDealer.SelectedValue
            'intOTR = Convert.ToInt64(valueOTR)

            'objDealerPriceListDealer = New DealerVehiclePriceDetailFacade(User).RetrieveByID(intOTR)
            'If Not IsNothing(objDealerPriceListDealer) Then
            '    If objDealerPriceListDealer.Count > 0 Then
            '        objDealerPrice = CType(objDealerPriceListDealer(0), DealerVehiclePriceDetail)
            '    End If
            'End If
            'Price = objDealerPrice.ID
            objDealerVehiclePriceDetail = New DealerVehiclePriceDetailFacade(User).Retrieve(CType(ddlHargaPriceListDealer.SelectedValue, Integer))
            objSAPCustomer.DealerVehiclePriceDetail = objDealerVehiclePriceDetail
        Else
            objSAPCustomer.DealerVehiclePriceDetail = Nothing
        End If

        Dim objColor As VechileColor
        Dim IDColor As Integer
        objColor = New VechileColorFacade(User).RetrieveByName(ddlWarna.SelectedValue)
        If Not IsNothing(objColor) Then
            objSAPCustomer.VechileColor = objColor
        End If

        'Cek Blanko SPK 
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            Dim maxFileSize As Integer = 2048000
            'Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

            If DataFile.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            End If
            'cek ext
            Dim ext As String = System.IO.Path.GetExtension(DataFile.PostedFile.FileName)

            If Not CheckExt(ext.Substring(1)) Then
                MessageBox.Show("Extension file tidak sesuai. Ubah ke *.PDF/DOC/DOCX/JPG/PGN,.")
                Exit Sub
            End If

            Dim strFileName As String = Path.GetFileName(DataFile.PostedFile.FileName)   '-- Source file name
            strFileName = New Date().Now.ToString("yyyyMMddhhmmss") & strFileName
            Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\" & strFileName  '-- Temporary file
            Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("BlankoSPK") & "\" & Year(Now) & "\" & strFileName

            '-- Impersonation to manipulate file in server
            Dim fileInfoDestination As New FileInfo(DestFile)
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim success As Boolean = False
            Try
                If imp.Start() Then
                    '-- Copy data file from client to server temporary folder
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, TempFile)
                    If Not fileInfoDestination.Exists Then
                        fileInfoDestination.Directory.Create()
                    End If
                    DataFile.PostedFile.SaveAs(DestFile)
                    imp.StopImpersonate()
                    imp = Nothing
                    success = True

                    If success Then
                        objSAPCustomer.BlankoSPKDoc = DestFile
                    End If
                Else

                End If
            Catch ex As Exception
                MessageBox.Show(SR.UploadFail(strFileName))
            End Try
        Else
            'MessageBox.Show(SR.FileNotSelected)
        End If
        '

        'end CR SFID

        Dim result As Integer = -1
        Dim facade As SAPCustomerFacade = New SAPCustomerFacade(User)
        If objSAPCustomer.ID > 0 Then
            result = facade.Update(objSAPCustomer)
        Else
            result = facade.Insert(objSAPCustomer)
        End If
        If result = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            If (CInt(ddlStatusDetail.SelectedValue) <> 0) Then
                SaveResponse()
            End If
            MessageBox.Show(SR.SaveSuccess)
            btnSave.Enabled = False
            EnabledControl(False)
        End If
    End Sub
    Private Function CheckExt(ByVal ext As String) As Boolean
        Dim retValue As Boolean = False
        If ext.ToUpper() = "PDF" Or ext.ToUpper() = "JPG" Or ext.ToUpper() = "doc" Or ext.ToUpper() = "docx" Or ext.ToUpper() = "PNG" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function

    Private Function IsCustomerExist() As Boolean
        Dim vReturn As Boolean = False
        Try
            Dim arrSAPCustomer As ArrayList
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "Phone", MatchType.Exact, txtTelp.Text))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "Status", MatchType.Exact, CInt(EnumSAPCustomerStatus.SAPCustomerStatus.Deal_SPK)))

            Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            If Not IsNothing(objuser) Then
                criteria.opAnd(New Criteria(GetType(SAPCustomer), "Dealer.ID", MatchType.Exact, objuser.Dealer.ID))
            End If

            arrSAPCustomer = New SAPCustomerFacade(User).Retrieve(criteria)

            If arrSAPCustomer.Count > 0 Then
                vReturn = True
            End If
        Catch ex As Exception
            vReturn = True
        End Try
        Return vReturn
    End Function

    Private Sub SaveResponse()
        Dim objFacade As SAPCustomerResponseFacade = New SAPCustomerResponseFacade(User)
        Dim objCustomerCase As SAPCustomer = CType(sessHelper.GetSession("sessSAPCustomer"), SAPCustomer)
        If Not IsNothing(objCustomerCase) Then
            Dim objresponse As SAPCustomerResponse = New SAPCustomerResponse
            objresponse.SAPCustomer = objCustomerCase
            objresponse.Description = txtComment.Text
            objresponse.Status = ddlStatusDetail.SelectedValue

            'If lblResponseID.Text = String.Empty Then
            Dim iresult As Integer = 0
            iresult = objFacade.Insert(objresponse)
            'Else
            '    objFacade.Update(objresponse)
            'End If

            If iresult <> -1 Then
                BindDataResponse(dgCase.CurrentPageIndex)

                'Edit 14/1/2021 Digital Lead
                If objCustomerCase.SalesforceID <> "" Then

                    Dim sf As SalesForceInterface = New SalesForceInterface()
                    Dim msg As String = String.Empty
                    Dim vSFreturn As Boolean = False
                    vSFreturn = sf.UpdateOportunity(objresponse, CType(ddlStatusDetail.SelectedValue, Integer), False)

                    If vSFreturn Then
                        'Update IsSend
                        Dim objResponseNew As SAPCustomerResponse = New SAPCustomerResponse()
                        objResponseNew = New SAPCustomerResponseFacade(User).Retrieve(iresult)
                        If Not IsNothing(objResponseNew) Then
                            objResponseNew.IsSend = 1 'sent
                            iresult = New SAPCustomerResponseFacade(User).Update(objResponseNew)
                        End If
                    End If
                End If

            End If

            '--------------------------------------
            'GetConfig()

            'If _valTransferToSF = "1" Then
            '    Try
            '        If objresponse.SAPCustomer.SalesforceID.Length > 5 Then

            '            'Update oportunity
            '            Dim objParam As paramUpdateOpportunity = New paramUpdateOpportunity()
            '            objParam.id = objresponse.SAPCustomer.SalesforceID
            '            objParam.StageName = EnumSAPCustomerResponse.GetStringValue(CType(ddlStatusDetail.SelectedValue, Integer))
            '            If CType(ddlStatusDetail.SelectedValue, Integer) >= 4 Then
            '                objParam.SPK_Status__c = EnumSAPCustomerResponse.GetStringValue(CType(ddlStatusDetail.SelectedValue, Integer))
            '                Dim _criterias As New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '                _criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKCustomer.SAPCustomer.ID", MatchType.Exact, objresponse.SAPCustomer.ID))
            '                Dim arrSPKHeader As ArrayList = New SPKHeaderFacade(User).Retrieve(_criterias)
            '                If Not IsNothing(arrSPKHeader) AndAlso arrSPKHeader.Count > 0 Then
            '                    Dim objSPKHeader As SPKHeader = CType(arrSPKHeader(0), SPKHeader)
            '                    If objSPKHeader.ID > 0 Then
            '                        objParam.SPK_No__c = objSPKHeader.SPKNumber
            '                        objParam.Validation_Key__c = objSPKHeader.ValidationKey
            '                    End If
            '                Else
            '                    objParam.SPK_No__c = ""
            '                    objParam.Validation_Key__c = ""
            '                End If
            '            Else
            '                objParam.SPK_Status__c = ""
            '                objParam.SPK_No__c = ""
            '                objParam.Validation_Key__c = ""
            '            End If

            '            System.Threading.Tasks.Task.Run(Sub() KTB.DNet.WebApi.Models.SalesForce.SalesForce.Send(User, String.Concat("services/apexrest/", KTB.DNet.WebApi.Models.paramUpdateOpportunity.SObjectTypeName), objParam).Wait())

            '            If KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess Then
            '                objresponse.IsSend = 1
            '                objFacade.Update(objresponse)
            '            Else
            '                MessageBox.Show(KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message)
            '            End If
            '        End If

            '    Catch ex As Exception
            '        MessageBox.Show("Gagal update data di salesforce")
            '    End Try

            'End If
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmPreCustomerList.aspx?isBack=1")
    End Sub

    Private Sub dgCase_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgCase.ItemDataBound
        If e.Item.ItemIndex >= 0 Then
            Dim RowValue As SAPCustomerResponse = CType(e.Item.DataItem, SAPCustomerResponse)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            'Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            'Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1) + (dgCase.CurrentPageIndex * dgCase.PageSize)
            lblStatus.Text = EnumSAPCustomerResponse.GetStringValue(RowValue.Status)
        End If
    End Sub

    Private Sub txtCampaignName_TextChanged(sender As Object, e As EventArgs) Handles txtCampaignName.TextChanged
        Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim oBabitHeader As New BabitHeader
        Dim dsBabitEvent As DataSet = New DataSet
        If ddlBabitEventType.SelectedValue = "0" Then   'Tipe Babit & Event
            dsBabitEvent = New BabitHeaderFacade(User).RetrieveFromSPByPopUp(objuser.Dealer.ID, txtCampaignName.Text, "", Nothing, Nothing, False, "")
            If dsBabitEvent.Tables.Count > 0 Then
                If dsBabitEvent.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("No Reg Campaign : " & txtCampaignName.Text & " tidak valid")
                    txtCampaignName.Text = ""
                    txtCampaignName.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub ddlBabitEventType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBabitEventType.SelectedIndexChanged
        txtCampaignName.Text = ""
        If ddlBabitEventType.SelectedIndex = 0 Then
            txtCampaignName.Visible = False
            lblPopUpEvent.Visible = False
        ElseIf ddlBabitEventType.SelectedValue = "0" Then
            txtCampaignName.Visible = True
            lblPopUpEvent.Visible = True
        Else
            txtCampaignName.Visible = True
            lblPopUpEvent.Visible = False
        End If
    End Sub

    Protected Sub ddlCostumerType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCostumerType.SelectedIndexChanged
        If Not (ddlCostumerType.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan) Then
            RequiredFieldValidator11.Enabled = False
            ddlGender.SelectedIndex = 0
            ddlGender.Enabled = False
            'cr sfid
            BindLeadTypeIdentitas(ddlTipeKartuIdentitas, ddlCostumerType.SelectedValue)
            ddlPekerjaan.Enabled = False
        Else
            RequiredFieldValidator11.Enabled = True
            ddlGender.Enabled = True
            'cr sfid
            BindLeadTypeIdentitas(ddlTipeKartuIdentitas, ddlCostumerType.SelectedValue)
            ddlPekerjaan.Enabled = True
        End If

        If ddlWarna.SelectedValue <> String.Empty Then
            Dim TipeKendaraan As String
            TipeKendaraan = txtVehicleType.Text
            TxtJumlahDiskon.Text = 0
            BindLeadPrice(ddlHargaPriceListDealer, ddlWarna.SelectedValue, TipeKendaraan)
        End If

    End Sub

    Protected Sub ddlTipeKartuIdentitas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipeKartuIdentitas.SelectedIndexChanged
        'Dim TipeKendaraan As String
        'TipeKendaraan = txtVehicleType.Text

        'If Not (ddlTipeKartuIdentitas.SelectedValue = "") Then
        '    BindLeadTypeColor(ddlWarna, TipeKendaraan)
        'Else
        '    BindLeadTypeColor(ddlWarna, TipeKendaraan)
        'End If
    End Sub
    Private Sub BindLeadTypeColor(ByVal ddl As DropDownList, ByVal Type As String)

        Dim arrVechileType As ArrayList
        Dim TipeId As String
        Dim TypeID As Integer
        Dim objVechileType As VechileType
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, Type))
        criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))
        arrVechileType = New VechileTypeFacade(User).Retrieve(criterias)
        If Not IsNothing(arrVechileType) Then
            If arrVechileType.Count = 0 Then
                TipeId = ""
            Else
                'TypeID = arrVechileType(0)
                objVechileType = CType(arrVechileType(0), VechileType)
                TipeId = "1"
            End If

        Else
            TipeId = ""
        End If
        If TipeId = "" Then
            TypeID = 0
        Else
            TypeID = objVechileType.ID
        End If

        ddlWarna.DataSource = New VechileColorFacade(User).RetrieveByCategory(TypeID)
        ddlWarna.DataTextField = "ColorIndName"
        ddlWarna.DataValueField = "ColorCode"
        ddlWarna.DataBind()
        ddlWarna.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlWarna.SelectedIndex = 0
        ddlWarna_SelectedIndexChanged(ddlWarna, Nothing)
    End Sub
    Private Sub BindLeadTypeColorView(ByVal ddl As DropDownList, ByVal Type As Integer)
        ddlWarna.DataSource = New VechileColorFacade(User).RetrieveByCategoryView(Type)
        ddlWarna.DataTextField = "ColorIndName"
        ddlWarna.DataValueField = "ColorCode"
        ddlWarna.DataBind()
        ddlWarna.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlWarna.SelectedIndex = 0
    End Sub
    Private Sub BindLeadTypePekerjaan(ByVal ddl As DropDownList)
        ddlPekerjaan.DataSource = New StandardCodeFacade(User).RetrieveByCategory("SAPCustomer.JobKind")
        ddlPekerjaan.DataTextField = "ValueDesc"
        ddlPekerjaan.DataValueField = "ValueId"
        ddlPekerjaan.DataBind()
        ddlPekerjaan.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlPekerjaan.SelectedIndex = 0
    End Sub


    Protected Sub LnkDownloadTemplate_Click(sender As Object, e As EventArgs) Handles LnkDownloadTemplate.Click
        Dim LinkBlanko As String
        LinkBlanko = TxtBlanko.Text
        Response.Redirect("../Download.aspx?file=" & LinkBlanko)
    End Sub

    Protected Sub txtVehicleType_TextChanged(sender As Object, e As EventArgs) Handles txtVehicleType.TextChanged
        Dim TipeKendaraan As String
        TipeKendaraan = txtVehicleType.Text

        If Not (ddlTipeKartuIdentitas.SelectedIndex = 0) Then
            BindLeadTypeColor(ddlWarna, TipeKendaraan)
        Else
            BindLeadTypeColor(ddlWarna, TipeKendaraan)
        End If
    End Sub

    Private Sub ddlHargaPriceListDealer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHargaPriceListDealer.SelectedIndexChanged
        Dim dblHargaPriceListDealer As Double = If(ddlHargaPriceListDealer.SelectedItem.Text = "Silahkan Pilih", 0, ddlHargaPriceListDealer.SelectedItem.Text)
        Dim dblHargaPermintaanKonsumen As Double = If(TxtHargaPermintaanKonsumen.Text.Trim = "", 0, TxtHargaPermintaanKonsumen.Text)
        If TxtHargaPermintaanKonsumen.Text.Trim = "" Then TxtHargaPermintaanKonsumen.Text = "0"

        Dim dblJumlahDiskon As Double = 0
        If ddlHargaPriceListDealer.SelectedIndex > 0 Then
            If dblHargaPriceListDealer > dblHargaPermintaanKonsumen Then
                dblJumlahDiskon = dblHargaPriceListDealer - dblHargaPermintaanKonsumen
            End If
        End If
        TxtJumlahDiskon.Text = dblJumlahDiskon.ToString("#,##0")
    End Sub

    Protected Sub ddlWarna_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlWarna.SelectedIndexChanged
        Dim TipeKendaraan As String
        TipeKendaraan = txtVehicleType.Text
        TxtJumlahDiskon.Text = 0
        BindLeadPrice(ddlHargaPriceListDealer, ddlWarna.SelectedValue, TipeKendaraan)

        'If Not (ddlHargaPriceListDealer.SelectedIndex = 0) Then
        '    BindLeadPrice(ddlHargaPriceListDealer, ddlWarna.SelectedValue, TipeKendaraan)
        'Else
        '    BindLeadPrice(ddlHargaPriceListDealer, ddlWarna.SelectedValue, TipeKendaraan)
        'End If
    End Sub

    Protected Sub TxtHargaPermintaanKonsumen_TextChanged(sender As Object, e As EventArgs) Handles TxtHargaPermintaanKonsumen.TextChanged
        Dim valueRet As String
        Dim valueOTR As String
        Dim intOTR As Integer

        If ddlHargaPriceListDealer.SelectedValue <> String.Empty Then
            If TxtHargaPermintaanKonsumen.Text <> String.Empty Then
                valueRet = TxtHargaPermintaanKonsumen.Text
                valueOTR = ddlHargaPriceListDealer.SelectedValue
                intOTR = Convert.ToInt64(valueOTR)

                Dim arrVechileType As ArrayList
                Dim TipeId As String
                Dim TypeID As Integer
                Dim objVechileType As DealerVehiclePriceDetail
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "ID", MatchType.Exact, intOTR))

                arrVechileType = New DealerVehiclePriceDetailFacade(User).Retrieve(criterias)
                If Not IsNothing(arrVechileType) Then
                    If arrVechileType.Count = 0 Then
                        TipeId = ""
                    Else
                        'TypeID = arrVechileType(0)
                        objVechileType = CType(arrVechileType(0), DealerVehiclePriceDetail)
                        TipeId = "1"
                    End If

                Else
                    TipeId = ""
                End If
                If TipeId = "1" Then
                    If objVechileType.OTR > valueRet Then
                        TxtJumlahDiskon.Text = (objVechileType.OTR - valueRet).ToString("#,##0")
                    Else
                        TxtJumlahDiskon.Text = 0
                    End If
                End If

                TxtJumlahDiskon.Enabled = False
            Else
                TxtJumlahDiskon.Text = 0
                TxtJumlahDiskon.Enabled = False
            End If
        Else
            TxtJumlahDiskon.Text = 0
            TxtJumlahDiskon.Enabled = False
        End If
    End Sub

End Class
