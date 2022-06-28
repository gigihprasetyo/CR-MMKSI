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
'cr sfid
Imports KTB.DNet.Parser
'


Public Class FrmPreCustomerEntryForm
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents icProspectDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblVehicleType As System.Web.UI.WebControls.Label
    Protected WithEvents txtSalesmanID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtVehicleType As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSalesmanName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustomerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTelp As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtQty As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCurrVehicleBrand As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNote As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustomerAddress As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCurrVehicleType As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnSearchSalesman As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    'Protected WithEvents lblDateFrom As System.Web.UI.WebControls.Label
    'Protected WithEvents lblDateUntil As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnAdd As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dgSAPCustomer As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtSalesmanCode As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtPeriod As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblTotalRow As System.Web.UI.WebControls.Label
    Protected WithEvents btnNoSales As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCostumerType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAge As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPurpose As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSource As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlGender As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lnkReloadPlg As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtRefKodePelanggan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWebID As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnlInterface As System.Web.UI.WebControls.Panel
    Protected WithEvents IntcalBirthDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents IntcalEstimatedCloseDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlStatusCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLeadStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStateCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCampaignName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlBusinessSector As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPopUpEvent As System.Web.UI.WebControls.Label
    Protected WithEvents ddlBabitEventType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents RequiredFieldValidator11 As Global.System.Web.UI.WebControls.RequiredFieldValidator
    'CR SFID
    Protected WithEvents ddlTipeKartuIdentitas As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPekerjaan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTypeBBN As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlRating As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlWarna As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlHargaPriceListDealer As System.Web.UI.WebControls.DropDownList
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents TxtNomorIdentitas As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtNamaBelakang As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtNoTelp As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtHargaPermintaanKonsumen As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtJumlahDiskon As System.Web.UI.WebControls.TextBox
    'Protected WithEvents btnSetup As System.Web.UI.WebControls.Button
    'Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents TxtBookingFee As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtNoBlankoSPK As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtKendaraanPembanding As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtBlanko As System.Web.UI.WebControls.TextBox
    '

    'CR SPK
    Protected WithEvents txtCountryCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchCountryCode As System.Web.UI.WebControls.Label
    '
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
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper
#End Region

#Region "Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()

        If Not IsPostBack Then
            BindCustomerType(ddlCostumerType)
            BindAgeSegment(ddlAge)
            BindInformationType(ddlType)
            BindCustomerPurpose(ddlPurpose)
            BindInformationSource(ddlSource)
            BindGender(ddlGender)
            BindStatusCode(ddlStatusCode)
            BindStateCode(ddlStateCode)
            BindLeadStatus(ddlLeadStatus)
            BindBusinessSector(ddlBusinessSector)
            BindDdlBabitEventType(ddlBabitEventType)
            'CR SFID
            BindLeadTypeIdentitas(ddlTipeKartuIdentitas, 0)
            BindLeadTypePekerjaan(ddlPekerjaan)
            BindLeadTypeBBN(ddlTypeBBN)
            BindLeadTypeRating(ddlRating)
            TxtBlanko.Visible = False
            '

            Dim arl As ArrayList = EnumSAPCustStatus.RetriveSAPCustomerStatus(True, True)
            For Each item As EnumSAPCustStatus In arl
                If item.ValStatus <> EnumSAPCustomerStatus.SAPCustomerStatus.Deal_SPK Then
                    ddlStatus.Items.Add(New ListItem(item.NameStatus, item.ValStatus))
                End If
            Next
            'CommonFunction.BindFromEnum("SAPCustomerStatus", ddlStatus, User, True, "NameStatus", "ValStatus")

            lblPopUpEvent.Attributes("onclick") = "ShowPPEventDealerSelection();"

            Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            lblDealerCode.Text = objuser.Dealer.DealerCode
            lblDealerName.Text = objuser.Dealer.DealerName
            If Request.QueryString("isBack") <> "1" Then
                Initialize()
                BindControlsAttribute()
                BindDataGrid(0)
                dgSAPCustomer.ShowFooter = True
            Else
                Dim arlDataGet As ArrayList = sessHelper.GetSession("DataForSAPReturn")
                txtSalesmanID.Text = arlDataGet(1)
                txtSalesmanName.Text = arlDataGet(2)
                'lblDateFrom.Text = arlDataGet(3)
                'lblDateUntil.Text = arlDataGet(4)
                txtSalesmanCode.Value = arlDataGet(3)
                txtPeriod.Value = arlDataGet(4)
                dgSAPCustomer.CurrentPageIndex = 0
                BindDataGrid(0)
                BindControlsAttribute()
                dgSAPCustomer.ShowFooter = True
            End If
        Else
            'Postback from jscript
            If Request("__EVENTARGUMENT") = "searchsalesman" Then
                btnSearch_Click(Me, System.EventArgs.Empty)
            End If
        End If
        lblVehicleType.Attributes("onclick") = "ShowPopUpVechileType();"
        txtSalesmanID.Attributes.Add("readonly", "readonly")
        txtSalesmanName.Attributes.Add("readonly", "readonly")
        'txtVehicleType.Attributes.Add("readonly", "readonly")
        'CR SPK
        txtCountryCode.Attributes.Add("readonly", "readonly")
        txtCountryCode.Text = "62"
        'txtCountryName.Attributes.Add("readonly", "readonly")
        lblSearchCountryCode.Attributes("onclick") = "ShowPopUpSPKMasterCountryCode();"
        '
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        sessHelper.SetSession("CurrentSalesmanHeader", txtSalesmanID.Text)
        dgSAPCustomer.CurrentPageIndex = 0
        dgSAPCustomer.ShowFooter = True
        dgSAPCustomer.EditItemIndex = -1
        BindDataGrid(0)
        'If txtPeriod.Value <> "" Then
        '    ' mengembalikan value hidden field ke label ybs
        '    Dim strTmp As String() = txtPeriod.Value.Split(";")
        '    'lblDateFrom.Text = strTmp(0)
        '    'lblDateUntil.Text = strTmp(1)
        'End If

        ' add security
        'If Not CekProspekCreatePrivilege() Then
        '    dgSAPCustomer.ShowFooter = False
        '    dgSAPCustomer.Columns(8).Visible = False    'kolom aksi
        'End If
    End Sub

    Private Sub dgSAPCustomer_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSAPCustomer.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSAPCustomer As SAPCustomer = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSAPCustomer.CurrentPageIndex * dgSAPCustomer.PageSize)
            ' untuk bagian item / alternate item
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                ' mengisi value

                Dim lbtnDeleteNew As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnDeleteNew.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                lbtnDeleteNew.CommandArgument = objSAPCustomer.ID

                Dim lblNameNew As Label = CType(e.Item.FindControl("lblName"), Label)
                'lblNameNew.Text = objSAPCustomer.SAPRegister.SalesmanHeader.Name

                Dim lblSalesmanCodeNew As Label = CType(e.Item.FindControl("lblSalesmanCode"), Label)
                'lblSalesmanCodeNew.Text = objSAPCustomer.SAPRegister.SalesmanHeader.SalesmanCode

                Dim lblVechileTypeCodeNew As Label = CType(e.Item.FindControl("lblVechileTypeCode"), Label)
                If Not IsNothing(objSAPCustomer.VechileType) Then
                    lblVechileTypeCodeNew.Text = objSAPCustomer.VechileType.VechileTypeCode
                End If


                Dim lblCustomerNameNew As Label = CType(e.Item.FindControl("lblCustomerName"), Label)
                lblCustomerNameNew.Text = objSAPCustomer.CustomerName

                Dim lblCustomerAddressNew As Label = CType(e.Item.FindControl("lblCustomerAddress"), Label)
                lblCustomerAddressNew.Text = objSAPCustomer.CustomerAddress

                Dim lblStatusNew As Label = CType(e.Item.FindControl("lblStatus"), Label)
                lblStatusNew.Text = CType(objSAPCustomer.Status, EnumSAPCustomerStatus.SAPCustomerStatus).ToString.Replace("_", " ")

                Dim lblEmail As Label = CType(e.Item.FindControl("lblEmail"), Label)
                lblEmail.Text = objSAPCustomer.Email

                Dim lblPhone As Label = CType(e.Item.FindControl("lblPhone"), Label)
                lblPhone.Text = objSAPCustomer.Phone

                Dim lblCustomerType As Label = CType(e.Item.FindControl("lblCustomerType"), Label)
                lblCustomerType.Text = EnumTipePelangganCustomerRequest.RetrieveTipePelangganCustomerRequest(objSAPCustomer.CustomerType)

                Dim lblGender As Label = CType(e.Item.FindControl("lblGender"), Label)
                lblGender.Text = EnumGender.GetStringGender(objSAPCustomer.Sex)

                Dim lblAge As Label = CType(e.Item.FindControl("lblAge"), Label)
                lblAge.Text = EnumAgeSegment.GetStringAgeSegment(objSAPCustomer.AgeSegment)

                Dim lblType As Label = CType(e.Item.FindControl("lblType"), Label)
                lblType.Text = EnumInformationType.GetStringInformationType(objSAPCustomer.InformationType)

                Dim lblPurpose As Label = CType(e.Item.FindControl("lblPurpose"), Label)
                lblPurpose.Text = EnumCustomerPurpose.GetStringCustomerPurpose(objSAPCustomer.CustomerPurpose)

                Dim lblSource As Label = CType(e.Item.FindControl("lblSource"), Label)
                lblSource.Text = EnumInformationSource.GetStringInformationSource(objSAPCustomer.InformationSource)

                Dim lblCurrVehicleBrand As Label = CType(e.Item.FindControl("lblCurrVehicleBrand"), Label)
                lblCurrVehicleBrand.Text = objSAPCustomer.CurrVehicleBrand

                Dim lblCurrVehicleType As Label = CType(e.Item.FindControl("lblCurrVehicleType"), Label)
                lblCurrVehicleType.Text = objSAPCustomer.CurrVehicleType

                Dim lblNote As Label = CType(e.Item.FindControl("lblNote"), Label)
                lblNote.Text = objSAPCustomer.Note

            End If

            '    ' untuk bagian edit item
            '    If e.Item.ItemType = ListItemType.EditItem Then

            '        Dim lbtnSaveNew As LinkButton = CType(e.Item.FindControl("lbtnSave"), LinkButton)
            '        lbtnSaveNew.CommandArgument = objSAPCustomer.ID

            '        Dim lblEditVechileTypeCodeNew As Label = CType(e.Item.FindControl("lblEditVechileTypeCode"), Label)
            '        lblEditVechileTypeCodeNew.Attributes("onclick") = "ShowPopUpVechileType();"

            '        Dim txtEditVechileTypeCodeNew As TextBox = CType(e.Item.FindControl("txtEditVechileTypeCode"), TextBox)
            '        txtEditVechileTypeCodeNew.Text = objSAPCustomer.VechileType.VechileTypeCode

            '        Dim txtEditCustomerNameNew As TextBox = CType(e.Item.FindControl("txtEditCustomerName"), TextBox)
            '        txtEditCustomerNameNew.Text = objSAPCustomer.CustomerName

            '        Dim txtEditCustomerAddressNew As TextBox = CType(e.Item.FindControl("txtEditCustomerAddress"), TextBox)
            '        txtEditCustomerAddressNew.Text = objSAPCustomer.CustomerAddress

            '        Dim ddlEditStatusNew As DropDownList = CType(e.Item.FindControl("ddlEditStatus"), DropDownList)
            '        CommonFunction.BindFromEnum("SAPCustomerStatus", ddlEditStatusNew, User, True, "NameStatus", "ValStatus")
            '        ddlEditStatusNew.SelectedValue = objSAPCustomer.Status

            '        Dim txtPhoneE As TextBox = CType(e.Item.FindControl("txtPhoneE"), TextBox)
            '        txtPhoneE.Text = objSAPCustomer.Phone

            '        Dim txtEmailE As TextBox = CType(e.Item.FindControl("txtEmailE"), TextBox)
            '        txtEmailE.Text = objSAPCustomer.Email

            '        Dim ddlCustomerTypeE As DropDownList = CType(e.Item.FindControl("ddlCustomerTypeE"), DropDownList)
            '        BindCustomerType(ddlCustomerTypeE)
            '        If objSAPCustomer.CustomerType > 0 Then
            '            ddlCustomerTypeE.SelectedValue = objSAPCustomer.CustomerType
            '        Else
            '            ddlCustomerTypeE.SelectedIndex = 0
            '        End If


            '        Dim ddlGenderE As DropDownList = CType(e.Item.FindControl("ddlGenderE"), DropDownList)
            '        BindGender(ddlGenderE)
            '        If objSAPCustomer.Sex > 0 Then
            '            ddlGenderE.SelectedValue = objSAPCustomer.Sex
            '        Else
            '            ddlGenderE.SelectedIndex = 1
            '        End If


            '        Dim ddlAgeE As DropDownList = CType(e.Item.FindControl("ddlAgeE"), DropDownList)
            '        BindAgeSegment(ddlAgeE)
            '        ddlAgeE.SelectedValue = objSAPCustomer.AgeSegment

            '        Dim ddlTypeE As DropDownList = CType(e.Item.FindControl("ddlTypeE"), DropDownList)
            '        BindInformationType(ddlTypeE)
            '        ddlTypeE.SelectedValue = objSAPCustomer.InformationType

            '        Dim ddlPurposeE As DropDownList = CType(e.Item.FindControl("ddlPurposeE"), DropDownList)
            '        BindCustomerPurpose(ddlPurposeE)
            '        ddlPurposeE.SelectedValue = objSAPCustomer.CustomerPurpose

            '        Dim ddlSourceE As DropDownList = CType(e.Item.FindControl("ddlSourceE"), DropDownList)
            '        BindInformationSource(ddlSourceE)
            '        ddlSourceE.SelectedValue = objSAPCustomer.InformationSource

            '        Dim txtCurrVehicleBrandE As TextBox = CType(e.Item.FindControl("txtCurrVehicleBrandE"), TextBox)
            '        txtCurrVehicleBrandE.Text = objSAPCustomer.CurrVehicleBrand

            '        Dim txtCurrVehicleTypeE As TextBox = CType(e.Item.FindControl("txtCurrVehicleTypeE"), TextBox)
            '        txtCurrVehicleTypeE.Text = objSAPCustomer.CurrVehicleType

            '        Dim txtNoteE As TextBox = CType(e.Item.FindControl("txtNoteE"), TextBox)
            '        txtNoteE.Text = objSAPCustomer.Note

            '    End If
        End If

        '' untuk bagian footer
        'If e.Item.ItemType = ListItemType.Footer Then

        '    Dim ArrCopy As New ArrayList
        '    If Not sessHelper.GetSession("DataCopy") Is Nothing Then
        '        ArrCopy = sessHelper.GetSession("DataCopy")
        '    End If


        '    Dim lblAddVechileTypeCodeNew As Label = CType(e.Item.FindControl("lblAddVechileTypeCode"), Label)
        '    lblAddVechileTypeCodeNew.Attributes("onclick") = "ShowPopUpVechileType();"

        '    Dim txtAddVechileTypeCodeNew As TextBox = CType(e.Item.FindControl("txtAddVechileTypeCode"), TextBox)
        '    txtAddVechileTypeCodeNew.Text = ""

        '    Dim txtAddCustomerNameNew As TextBox = CType(e.Item.FindControl("txtAddCustomerName"), TextBox)
        '    Dim txtAddCustomerAddressNew As TextBox = CType(e.Item.FindControl("txtAddCustomerAddress"), TextBox)
        '    Dim txtPhoneF As TextBox = CType(e.Item.FindControl("txtPhoneF"), TextBox)
        '    If (ArrCopy.Count > 0) Then
        '        txtAddCustomerNameNew.Text = ArrCopy(1).ToString()
        '        txtAddCustomerAddressNew.Text = ArrCopy(2).ToString()
        '        txtPhoneF.Text = ArrCopy(5).ToString()

        '    Else
        '        txtAddCustomerNameNew.Text = ""
        '        txtAddCustomerAddressNew.Text = ""
        '        txtPhoneF.Text = ""
        '    End If

        '    Dim ddlAddStatusNew As DropDownList = CType(e.Item.FindControl("ddlAddStatus"), DropDownList)
        '    CommonFunction.BindFromEnum("SAPCustomerStatus", ddlAddStatusNew, User, True, "NameStatus", "ValStatus")
        '    If (ArrCopy.Count > 0) Then
        '        ddlAddStatusNew.SelectedIndex = CInt(ArrCopy(3).ToString())
        '    Else
        '        ddlAddStatusNew.SelectedIndex = -1
        '    End If

        '    Dim ddlCustomerTypeF As DropDownList = CType(e.Item.FindControl("ddlCustomerTypeF"), DropDownList)
        '    BindCustomerType(ddlCustomerTypeF)
        '    If ddlCustomerTypeF.Items.Count > 0 Then
        '        If (ArrCopy.Count > 0) Then
        '            ddlCustomerTypeF.SelectedValue = CInt(ArrCopy(4).ToString())
        '        Else
        '            ddlCustomerTypeF.SelectedIndex = 0
        '        End If
        '    End If


        '    Dim ddlGenderF As DropDownList = CType(e.Item.FindControl("ddlGenderF"), DropDownList)
        '    BindGender(ddlGenderF)
        '    If ddlGenderF.Items.Count > 0 Then
        '        If (ArrCopy.Count > 0) Then
        '            ddlGenderF.SelectedValue = CInt(ArrCopy(7).ToString())
        '        Else
        '            ddlGenderF.SelectedIndex = 0
        '        End If
        '    End If

        '    Dim ddlAgeF As DropDownList = CType(e.Item.FindControl("ddlAgeF"), DropDownList)
        '    BindAgeSegment(ddlAgeF)
        '    If ddlAgeF.Items.Count > 0 Then
        '        If (ArrCopy.Count > 0) Then
        '            ddlAgeF.SelectedValue = CInt(ArrCopy(8).ToString())
        '        Else
        '            ddlAgeF.SelectedIndex = 0
        '        End If
        '    Else
        '        ddlAgeF.ClearSelection()
        '    End If
        '    '
        '    Dim ddlTypeF As DropDownList = CType(e.Item.FindControl("ddlTypeF"), DropDownList)
        '    BindInformationType(ddlTypeF)
        '    If ddlTypeF.Items.Count > 0 Then
        '        If (ArrCopy.Count > 0) Then
        '            ddlTypeF.SelectedValue = CInt(ArrCopy(9).ToString())
        '        Else
        '            ddlTypeF.SelectedIndex = 0
        '        End If
        '    Else
        '        ddlTypeF.ClearSelection()
        '    End If

        '    Dim ddlPurposeF As DropDownList = CType(e.Item.FindControl("ddlPurposeF"), DropDownList)
        '    BindCustomerPurpose(ddlPurposeF)
        '    If ddlPurposeF.Items.Count > 0 Then
        '        If (ArrCopy.Count > 0) Then
        '            ddlPurposeF.SelectedValue = CInt(ArrCopy(10).ToString())
        '        Else
        '            ddlPurposeF.SelectedIndex = 0
        '        End If
        '    Else
        '        ddlPurposeF.ClearSelection()
        '    End If

        '    Dim ddlSourceF As DropDownList = CType(e.Item.FindControl("ddlSourceF"), DropDownList)
        '    BindInformationSource(ddlSourceF)
        '    If ddlSourceF.Items.Count > 0 Then
        '        If (ArrCopy.Count > 0) Then
        '            ddlSourceF.SelectedValue = CInt(ArrCopy(11).ToString())
        '        Else
        '            ddlSourceF.SelectedIndex = 0
        '        End If
        '    Else
        '        ddlSourceF.ClearSelection()
        '    End If
        'End If
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

    Private Sub BindCustomerType(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = New EnumTipePelangganCustomerRequest().RetrieveType
        For Each item As EnumTipePelanggan In arrList
            Dim listItem As New ListItem(item.NameTipe, item.ValTipe)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindGender(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumGenderOp.RetriveSalesGender(True)
        For Each item As EnumGenderOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindStatusCode(ByVal ddl As DropDownList)
        ddlStatusCode.DataSource = New StandardCodeFacade(User).RetrieveByCategory("LeadStatusCode")
        ddlStatusCode.DataTextField = "ValueDesc"
        ddlStatusCode.DataValueField = "ValueId"
        ddlStatusCode.DataBind()
        ddlStatusCode.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlStatusCode.SelectedIndex = 0

    End Sub

    Private Sub BindLeadStatus(ByVal ddl As DropDownList)
        ddlLeadStatus.DataSource = New StandardCodeFacade(User).RetrieveByCategory("LeadStatus")
        ddlLeadStatus.DataTextField = "ValueDesc"
        ddlLeadStatus.DataValueField = "ValueId"
        ddlLeadStatus.DataBind()
        ddlLeadStatus.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlLeadStatus.SelectedIndex = 0
    End Sub
    'CR SFID
    Private Sub BindLeadTypeIdentitas(ByVal ddl As DropDownList, ByVal type As Integer)
        ddlTipeKartuIdentitas.DataSource = New StandardCodeFacade(User).RetrieveByValueType(type, "IdentityTypeSPK")
        ddlTipeKartuIdentitas.DataTextField = "ValueCode"
        ddlTipeKartuIdentitas.DataValueField = "ValueId"
        ddlTipeKartuIdentitas.DataBind()
        ddlTipeKartuIdentitas.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))
        ddlTipeKartuIdentitas.SelectedIndex = 0
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

        Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        Dim arlNew As New ArrayList
        arlNew = New DealerVehiclePriceDetailFacade(User).RetrieveByCategory(Type, Category, ddlCostumerType.SelectedValue, objuser.Dealer.ID)
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
        ddlHargaPriceListDealer_SelectedIndexChanged(ddlHargaPriceListDealer, Nothing)

    End Sub

    Private Sub BindLeadTypePekerjaan(ByVal ddl As DropDownList)
        ddlPekerjaan.DataSource = New StandardCodeFacade(User).RetrieveByCategory("SAPCustomer.JobKind")
        ddlPekerjaan.DataTextField = "ValueDesc"
        ddlPekerjaan.DataValueField = "ValueId"
        ddlPekerjaan.DataBind()
        ddlPekerjaan.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlPekerjaan.SelectedIndex = 0
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

    '

    Private Sub BindStateCode(ByVal ddl As DropDownList)
        ddlStateCode.DataSource = New StandardCodeFacade(User).RetrieveByCategory("LeadStateCode")
        ddlStateCode.DataTextField = "ValueDesc"
        ddlStateCode.DataValueField = "ValueId"
        ddlStateCode.DataBind()
        ddlStateCode.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlStateCode.SelectedIndex = 0

    End Sub

    Private Sub BindBusinessSector(ByVal ddl As DropDownList)
        ddlBusinessSector.DataSource = New VWI_BusinessSectorFacade(User).RetrieveList()
        ddlBusinessSector.DataValueField = "ID"
        ddlBusinessSector.DataTextField = "BusinessName"
        ddlBusinessSector.DataBind()

        ddlBusinessSector.Items.Insert(0, New ListItem("Silahkan Pilih", ""))
    End Sub

    Private Sub BindAgeSegment(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumAgeSegmentOp.RetriveAgeSegment(True)
        For Each item As EnumAgeSegmentOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindInformationType(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumInformationTypeOp.RetriveInformationType(True)
        For Each item As EnumInformationTypeOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next

    End Sub

    Private Sub BindInformationSource(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumInformationSourceOp.RetriveInformationSource(True, True)
        For Each item As EnumInformationSourceOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindCustomerPurpose(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumCustomerPurposeOp.RetriveCustomerPurpose(True)
        For Each item As EnumCustomerPurposeOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    'Private Sub AddRawData(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs, Optional ByVal IsCopy As Boolean = False)
    '    Dim facade As SAPCustomerFacade = New SAPCustomerFacade(User)
    '    Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

    '    Dim txtAddQty As TextBox = CType(e.Item.FindControl("txtAddQty"), TextBox)
    '    Dim txtAddCustomerNameNew As TextBox = CType(e.Item.FindControl("txtAddCustomerName"), TextBox)
    '    Dim txtAddCustomerAddressNew As TextBox = CType(e.Item.FindControl("txtAddCustomerAddress"), TextBox)
    '    Dim txtAddVechileTypeCodeNew As TextBox = CType(e.Item.FindControl("txtAddVechileTypeCode"), TextBox)
    '    Dim txtPhoneF As TextBox = CType(e.Item.FindControl("txtPhoneF"), TextBox)
    '    Dim ddlCustomerTypeF As DropDownList = CType(e.Item.FindControl("ddlCustomerTypeF"), DropDownList)
    '    Dim txtEmailF As TextBox = CType(e.Item.FindControl("txtEmailF"), TextBox)
    '    Dim ddlAddStatusNew As DropDownList = CType(e.Item.FindControl("ddlAddStatus"), DropDownList)
    '    Dim ddlGenderF As DropDownList = CType(e.Item.FindControl("ddlGenderF"), DropDownList)
    '    Dim ddlAgeF As DropDownList = CType(e.Item.FindControl("ddlAgeF"), DropDownList)
    '    Dim ddlTypeF As DropDownList = CType(e.Item.FindControl("ddlTypeF"), DropDownList)
    '    Dim ddlPurposeF As DropDownList = CType(e.Item.FindControl("ddlPurposeF"), DropDownList)
    '    Dim ddlSourceF As DropDownList = CType(e.Item.FindControl("ddlSourceF"), DropDownList)
    '    Dim txtCurrVehicleBrandF As TextBox = CType(e.Item.FindControl("txtCurrVehicleBrandF"), TextBox)
    '    Dim txtCurrVehicleTypeF As TextBox = CType(e.Item.FindControl("txtCurrVehicleTypeF"), TextBox)
    '    Dim txtNoteF As TextBox = CType(e.Item.FindControl("txtNoteF"), TextBox)

    '    Dim strMessage As String = ""

    '    'If txtSalesmanID.Text = "" Then
    '    '    MessageBox.Show("Salesman ID harus diisi dahulu !")
    '    '    Return
    '    'End If

    '    If txtAddCustomerNameNew.Text = "" Then
    '        'MessageBox.Show("Nama Customer harus diisi dahulu !")
    '        'Return
    '        strMessage &= "Nama customer harus diisi. \n"
    '    End If

    '    If txtAddCustomerAddressNew.Text = "" Then
    '        'MessageBox.Show("Alamat Customer harus diisi dahulu !")
    '        'Return
    '        strMessage &= "Alamat customer harus diisi. \n"
    '    End If

    '    If ddlAddStatusNew.SelectedItem.Text = "" Then
    '        'MessageBox.Show("Silakan memilih status terlebih dahulu !")
    '        'Return
    '        strMessage &= "Status harus dipilih.  \n"
    '    End If

    '    If ddlGenderF.SelectedIndex = -1 Then
    '        'MessageBox.Show("Silakan memilih gender terlebih dahulu !")
    '        'Return
    '        strMessage &= "Gender harus dipilih. \n"
    '    End If

    '    If ddlTypeF.SelectedIndex = 0 Then
    '        'MessageBox.Show("Silakan memilih tipe informasi terlebih dahulu !")
    '        'Return
    '        strMessage &= "Tipe informasi harus dipilih. \n"
    '    End If

    '    If ddlPurposeF.SelectedIndex = 0 Then
    '        'MessageBox.Show("Silakan memilih tujuan konsumen terlebih dahulu !")
    '        'Return
    '        strMessage &= "Tujuan konsumen harus dipilih. \n"
    '    End If

    '    If txtAddVechileTypeCodeNew.Text = "" Then
    '        'MessageBox.Show("Tipe kendaraan harus diisi dahulu !")
    '        'Return
    '        strMessage &= "Tipe kendaraan harus dipilih. \n"
    '    Else
    '        ' cek apakah data valid saat diinput
    '        Dim arrVechileType As ArrayList
    '        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '        criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, txtAddVechileTypeCodeNew.Text))
    '        arrVechileType = New VechileTypeFacade(User).Retrieve(criterias)
    '        If Not IsNothing(arrVechileType) Then
    '            If arrVechileType.Count < 1 Then
    '                'MessageBox.Show("Tipe kendaraan harus diisi dengan data yang valid, gunakan pop up !")
    '                'Return
    '                strMessage &= "Tipe kendaraan tidak valid. \n"
    '            End If
    '        End If
    '    End If


    '    If Val(txtAddQty.Text) = 0 Then
    '        'MessageBox.Show("Silakan isi qty terlebih dahulu !")
    '        'Return
    '        strMessage &= "Qty harus diisi. \n"
    '    End If

    '    If ddlSourceF.SelectedIndex = 0 Then
    '        'MessageBox.Show("Silakan memilih sumber informasi terlebih dahulu !")
    '        'Return
    '        strMessage &= "Sumber informasi harus dipilih. \n"
    '    End If

    '    If strMessage.Trim <> "" Then
    '        MessageBox.Show(strMessage)
    '        Return
    '    End If

    '    Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtAddVechileTypeCodeNew.Text.Trim)

    '    Dim arrSAPCustomer As ArrayList
    '    Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "CustomerName", MatchType.Exact, txtAddCustomerNameNew.Text))
    '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "CustomerAddress", MatchType.Exact, txtAddCustomerAddressNew.Text))
    '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "Phone", MatchType.Exact, txtPhoneF.Text))
    '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "Status", MatchType.Exact, ddlAddStatusNew.SelectedValue))
    '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "VechileType.ID", MatchType.Exact, objVechileType.ID))
    '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "Sex", MatchType.Exact, ddlGenderF.SelectedValue))
    '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "InformationType", MatchType.Exact, ddlTypeF.SelectedValue))
    '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "InformationSource", MatchType.Exact, ddlSourceF.SelectedValue))
    '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "CustomerPurpose", MatchType.Exact, ddlPurposeF.SelectedValue))
    '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "AgeSegment", MatchType.Exact, ddlAgeF.SelectedValue))
    '    criteria.opAnd(New Criteria(GetType(SAPCustomer), "ProspectDate", MatchType.Exact, New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)))
    '    If txtSalesmanID.Text.Trim <> String.Empty Then
    '        criteria.opAnd(New Criteria(GetType(SAPCustomer), "SalesmanHeader.SalesmanCode", MatchType.Exact, txtSalesmanID.Text.Trim))
    '    End If
    '    If Not IsNothing(objuser) Then
    '        criteria.opAnd(New Criteria(GetType(SAPCustomer), "Dealer.ID", MatchType.Exact, objuser.Dealer.ID))
    '    End If

    '    arrSAPCustomer = New SAPCustomerFacade(User).Retrieve(criteria)
    '    If Not IsNothing(arrSAPCustomer) Then
    '        If arrSAPCustomer.Count > 0 Then
    '            MessageBox.Show("Data sudah ada, tidak bisa di masukan lagi !")
    '            Return
    '        End If
    '    End If


    '    Dim objSAPCustomer As SAPCustomer = New SAPCustomer
    '    Dim objSalesmanHeader As SalesmanHeader
    '    If txtSalesmanID.Text.Trim <> String.Empty Then
    '        objSalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
    '        objSAPCustomer.SalesmanHeader = objSalesmanHeader
    '    Else
    '        objSAPCustomer.SalesmanHeader = Nothing
    '    End If

    '    If Not IsNothing(objuser) Then
    '        objSAPCustomer.Dealer = objuser.Dealer
    '    End If
    '    objSAPCustomer.Qty = Val(txtAddQty.Text)
    '    objSAPCustomer.VechileType = objVechileType
    '    objSAPCustomer.CustomerName = txtAddCustomerNameNew.Text
    '    objSAPCustomer.CustomerAddress = txtAddCustomerAddressNew.Text
    '    objSAPCustomer.CustomerType = ddlCustomerTypeF.SelectedValue
    '    objSAPCustomer.Email = txtEmailF.Text
    '    objSAPCustomer.Status = ddlAddStatusNew.SelectedValue
    '    objSAPCustomer.Phone = txtPhoneF.Text.Trim
    '    objSAPCustomer.Sex = ddlGenderF.SelectedValue
    '    objSAPCustomer.AgeSegment = ddlAgeF.SelectedValue
    '    objSAPCustomer.InformationType = ddlTypeF.SelectedValue
    '    objSAPCustomer.InformationSource = ddlSourceF.SelectedValue
    '    objSAPCustomer.CustomerPurpose = ddlPurposeF.SelectedValue
    '    objSAPCustomer.ProspectDate = New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)
    '    objSAPCustomer.CurrVehicleBrand = txtCurrVehicleBrandF.Text
    '    objSAPCustomer.CurrVehicleType = txtCurrVehicleTypeF.Text
    '    objSAPCustomer.Note = txtNoteF.Text



    '    Dim result As Integer = facade.Insert(objSAPCustomer)

    '    If result = -1 Then
    '        MessageBox.Show(SR.SaveFail)
    '    Else
    '        MessageBox.Show(SR.SaveSuccess)
    '        If IsCopy Then
    '            Dim ArrCopy As New ArrayList
    '            ArrCopy.Add(txtAddQty.Text)
    '            ArrCopy.Add(txtAddCustomerNameNew.Text)
    '            ArrCopy.Add(txtAddCustomerAddressNew.Text)
    '            ArrCopy.Add(ddlAddStatusNew.SelectedValue)
    '            ArrCopy.Add(ddlCustomerTypeF.SelectedValue)
    '            ArrCopy.Add(txtPhoneF.Text)
    '            ArrCopy.Add(txtEmailF.Text)
    '            ArrCopy.Add(ddlGenderF.SelectedValue)
    '            ArrCopy.Add(ddlAgeF.SelectedValue)
    '            ArrCopy.Add(ddlTypeF.SelectedValue)
    '            ArrCopy.Add(ddlPurposeF.SelectedValue)
    '            ArrCopy.Add(ddlSourceF.SelectedValue)
    '            ArrCopy.Add(txtCurrVehicleBrandF.Text)
    '            ArrCopy.Add(txtCurrVehicleTypeF.Text)
    '            ArrCopy.Add(txtNoteF.Text)

    '            sessHelper.SetSession("DataCopy", ArrCopy)
    '        Else
    '            sessHelper.SetSession("DataCopy", Nothing)
    '        End If
    '    End If

    '    If dgSAPCustomer.Items.Count = dgSAPCustomer.PageSize And dgSAPCustomer.CurrentPageIndex = (dgSAPCustomer.PageCount - 1) Then
    '        BindDataGrid(dgSAPCustomer.PageCount)
    '    Else
    '        If dgSAPCustomer.PageCount > 1 Then
    '            BindDataGrid(dgSAPCustomer.PageCount - 1)
    '        Else
    '            BindDataGrid(0)
    '        End If
    '    End If
    'End Sub

    Private Sub dgSAPCustomer_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSAPCustomer.ItemCommand
        Dim facade As SAPCustomerFacade = New SAPCustomerFacade(User)

        If e.CommandName = "Delete" Then
            Dim objSAPCustomer As SAPCustomer = facade.Retrieve(CInt(e.CommandArgument))
            Dim result As Integer = facade.DeleteFromDB(objSAPCustomer)
            BindDataGrid(0)
        End If
        If e.CommandName = "Edit" Then
            Dim objSAPCustomer As SAPCustomer = facade.Retrieve(CInt(e.Item.Cells(0).Text))
            icProspectDate.Value = objSAPCustomer.ProspectDate
            txtSalesmanID.Text = objSAPCustomer.SalesmanHeader.ID
            txtSalesmanName.Text = objSAPCustomer.SalesmanHeader.Name
            txtSalesmanCode.Value = objSAPCustomer.SalesmanHeader.SalesmanCode
            ddlCostumerType.SelectedValue = objSAPCustomer.CustomerType
            txtCustomerName.Text = objSAPCustomer.CustomerName
            txtCustomerAddress.Text = objSAPCustomer.CustomerAddress
            txtTelp.Text = objSAPCustomer.Phone
            txtEmail.Text = objSAPCustomer.Email
            txtVehicleType.Text = objSAPCustomer.VechileType.VechileTypeCode
            txtCurrVehicleBrand.Text = objSAPCustomer.CurrVehicleBrand
            txtCurrVehicleType.Text = objSAPCustomer.CurrVehicleType
            txtQty.Text = objSAPCustomer.Qty
            txtNote.Text = objSAPCustomer.Note
            txtWebID.Text = objSAPCustomer.WebID
            ddlGender.SelectedValue = objSAPCustomer.Sex
            ddlAge.SelectedValue = objSAPCustomer.AgeSegment
            ddlPurpose.SelectedValue = objSAPCustomer.CustomerPurpose
            ddlSource.SelectedValue = objSAPCustomer.InformationSource
            ddlStatus.SelectedValue = objSAPCustomer.Status
            ddlType.SelectedValue = objSAPCustomer.InformationType
            'CR SPK
            txtCountryCode.Text = objSAPCustomer.CountryCode
            '
            sessHelper.SetSession("CurrentSAPCustomer", objSAPCustomer)
        End If

    End Sub

    Private Sub dgSAPCustomer_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSAPCustomer.PageIndexChanged
        BindDataGrid(e.NewPageIndex)
    End Sub

    Private Sub dgSAPCustomer_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSAPCustomer.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        dgSAPCustomer.SelectedIndex = -1
        BindDataGrid(dgSAPCustomer.CurrentPageIndex)
    End Sub
#End Region

#Region "Custom"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.CustomerProspekCreate_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Marketing - Input Prospektif Konsumen")
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
        txtSalesmanID.Text = String.Empty
        txtSalesmanName.Text = String.Empty
        txtSalesmanCode.Value = String.Empty

        'txtPeriod.Value = String.Empty

        dgSAPCustomer.DataSource = New ArrayList
        dgSAPCustomer.DataBind()
        lblSearchSalesman.Visible = True
        txtCustomerName.Text = String.Empty
        txtCustomerAddress.Text = String.Empty
        txtEmail.Text = String.Empty
        txtTelp.Text = String.Empty
        txtVehicleType.Text = String.Empty
        txtVehicleType_TextChanged(txtVehicleType, Nothing)
        txtQty.Text = String.Empty
        txtCurrVehicleBrand.Text = String.Empty
        txtCurrVehicleType.Text = String.Empty
        txtNote.Text = String.Empty
        txtWebID.Text = String.Empty
        ddlBabitEventType.SelectedIndex = 0
        txtCampaignName.Text = String.Empty
        txtDesc.Text = String.Empty

        IntcalBirthDate.Value = Date.Today
        IntcalEstimatedCloseDate.Value = Date.Today

        ddlAge.SelectedIndex = 0
        ddlCostumerType.SelectedIndex = 0
        ddlGender.SelectedIndex = 0
        ddlPurpose.SelectedIndex = 0
        ddlSource.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        ddlType.SelectedIndex = 0
        ddlBusinessSector.SelectedIndex = 0
        ddlLeadStatus.SelectedIndex = 0
        ddlStateCode.SelectedIndex = 0
        ddlStatusCode.SelectedIndex = 0

        'cr SFID
        ddlTipeKartuIdentitas.SelectedIndex = 0
        ddlPekerjaan.SelectedIndex = 0
        ddlTypeBBN.SelectedIndex = 0
        ddlRating.SelectedIndex = 0
        ddlWarna.SelectedIndex = 0
        ddlHargaPriceListDealer.SelectedIndex = 0
        ddlWarna_SelectedIndexChanged(ddlWarna, Nothing)
        ddlHargaPriceListDealer_SelectedIndexChanged(ddlHargaPriceListDealer, Nothing)

        TxtNamaBelakang.Text = String.Empty
        TxtNoTelp.Text = String.Empty
        TxtNomorIdentitas.Text = String.Empty
        TxtHargaPermintaanKonsumen.Text = String.Empty
        TxtJumlahDiskon.Text = String.Empty
        TxtBookingFee.Text = String.Empty
        TxtNoBlankoSPK.Text = String.Empty
        TxtKendaraanPembanding.Text = String.Empty

        'CR SPK
        txtCountryCode.Text = String.Empty
        lblSearchCountryCode.Visible = True
        '
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SAPCustomer), "ID", MatchType.InSet, "('" + Replace(sessHelper.GetSession("dataInsert"), ";", "','") + "')"))

        If (sessHelper.GetSession("dataInsert") IsNot Nothing) Then
            arrList = _SAPCustomerFacade.RetrieveByCriteria(criterias, idxPage + 1, dgSAPCustomer.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        End If

        dgSAPCustomer.CurrentPageIndex = idxPage
        dgSAPCustomer.DataSource = arrList
        dgSAPCustomer.VirtualItemCount = totalRow
        dgSAPCustomer.DataBind()
        lblTotalRow.Text = "Jumlah record : " & totalRow.ToString
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
        '        strMessage += "Nomor ."
        '    End If
        'End If
        If phoneType = "Handphone" Then
            If Len(phoneNo) < 10 Then
                strMessage += "No. Handphone minimum 10 digit"
            End If
            If Len(phoneNo) > 13 Then
                strMessage += "No. Handphone maksiman 13 digit"
            End If
            ''remark for phase 2
            'If Left(phoneNo, 1) <> "8" Then
            '    strMessage += "No. Handphone harus diawali dengan '8' (tanpa nol)"
            'End If
        End If
        Return strMessage
    End Function

#End Region

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        btnSearch.Enabled = True
        sessHelper.SetSession("CurrentSalesmanHeader", Nothing)
    End Sub

    Private Sub btnNoSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoSales.Click
        ClearData()

        'txtSalesmanID.Enabled = False
        'txtSalesmanName.Enabled = False

        dgSAPCustomer.SelectedIndex = -1
        dgSAPCustomer.DataSource = New ArrayList
        dgSAPCustomer.DataBind()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim strMessage As String = ""
        'cr sfid
        Dim valueOTR As String
        Dim intOTR As Integer
        '
        If Not Page.IsValid Then  '-- Postback validation
            Exit Sub
        End If
        'cr spk
        If txtTelp.Text.Trim.Length > 0 Then
            ''strMessage &= "Nomor telp min 10 digit. \n"
            strMessage &= IsPhoneValid(txtTelp.Text.Trim, "handphone")
        End If

        If txtEmail.Text.Trim.Length > 0 Then
            If Not EmailAddressCheck(txtEmail.Text.Trim) Then
                strMessage &= "Format email salah. \n"
            End If
        End If
        'end spk
        If txtCustomerAddress.Text.Trim.Length > 60 Then
            strMessage &= "Alamat melebihi 60 karakter. \n"
        End If

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

        If (ddlStatus.SelectedValue >= 4) Then
            strMessage &= "Untuk status konsumen" & EnumSAPCustomerResponse.GetStringValue(CInt(ddlStatus.SelectedValue)) & " otomatis dari proses SPK. \n"
        End If

        Dim objSAPCustomer As SAPCustomer = New SAPCustomer
        If Not IsNothing(sessHelper.GetSession("CurrentSAPCustomer")) Then
            objSAPCustomer = CType(sessHelper.GetSession("CurrentSAPCustomer"), SAPCustomer)
        End If

        If IsCustomerExist() Then
            strMessage &= "Data konsumen dengan no telp : " & txtTelp.Text & " sudah ada dan belum SPK. \n"
        End If

        If strMessage.Trim <> "" Then
            MessageBox.Show(strMessage)
            Exit Sub
        End If

        Dim objSalesmanHeader As SalesmanHeader
        If txtSalesmanID.Text.Trim <> String.Empty Then
            objSalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
            objSAPCustomer.SalesmanHeader = objSalesmanHeader
        Else
            objSAPCustomer.SalesmanHeader = Nothing
        End If

        Dim objBusinessSectorDetail As BusinessSectorDetail
        If ddlBusinessSector.SelectedValue <> String.Empty Then
            objBusinessSectorDetail = New BusinessSectorDetailFacade(User).Retrieve(Integer.Parse(ddlBusinessSector.SelectedValue))
            objSAPCustomer.BusinessSectorDetail = objBusinessSectorDetail
        Else
            objSAPCustomer.BusinessSectorDetail = Nothing
        End If

        If Not IsNothing(objuser) Then
            objSAPCustomer.Dealer = objuser.Dealer
        End If

        objSAPCustomer.CustomerType = ddlCostumerType.SelectedValue
        objSAPCustomer.Qty = Val(txtQty.Text)
        objSAPCustomer.VechileType = objVechileType
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
        objSAPCustomer.ProspectDate = icProspectDate.Value 'New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)
        objSAPCustomer.CurrVehicleBrand = txtCurrVehicleBrand.Text
        objSAPCustomer.CurrVehicleType = txtCurrVehicleType.Text
        objSAPCustomer.Note = txtNote.Text
        objSAPCustomer.WebID = txtWebID.Text
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
        'cr sfid
        objSAPCustomer.Name2 = TxtNamaBelakang.Text
        objSAPCustomer.Telp = TxtNoTelp.Text
        objSAPCustomer.IdentityNumber = TxtNomorIdentitas.Text
        If TxtHargaPermintaanKonsumen.Text <> "" Then
            objSAPCustomer.CusReqPrice = TxtHargaPermintaanKonsumen.Text
        End If
        If TxtJumlahDiskon.Text <> "" Then
            objSAPCustomer.CusReqDiscount = TxtJumlahDiskon.Text
        End If
        If TxtBookingFee.Text <> "" Then
            objSAPCustomer.BookingFee = TxtBookingFee.Text
        End If

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
        Dim objColor As New VechileColor
        Dim IDColor As Integer
        If ddlWarna.SelectedValue <> String.Empty Then
            objColor = New VechileColorFacade(User).RetrieveByName(ddlWarna.SelectedValue)
            If Not IsNothing(objColor) Then
                objSAPCustomer.VechileColor = objColor
            Else
                objSAPCustomer.VechileColor = Nothing
            End If
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
            'Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("BlankoSPK") & "\" & SrcFile
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
                    btnAdd.Enabled = True
                    If success Then
                        objSAPCustomer.BlankoSPKDoc = DestFile
                    End If
                Else
                    btnAdd.Enabled = False
                End If
            Catch ex As Exception
                MessageBox.Show(SR.UploadFail(strFileName))
            End Try
        Else
            'MessageBox.Show(SR.FileNotSelected)
        End If
        '

        'end CR SFID

        objSAPCustomer.CampaignName = txtCampaignName.Text

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
            MessageBox.Show(SR.SaveSuccess)
            Dim dataInsert As String = CStr(sessHelper.GetSession("dataInsert")) + ";" + result.ToString
            sessHelper.SetSession("dataInsert", dataInsert)
            ClearData()
        End If

        'BindDataGrid(dgSAPCustomer.CurrentPageIndex)
        If dgSAPCustomer.Items.Count = dgSAPCustomer.PageSize And dgSAPCustomer.CurrentPageIndex = (dgSAPCustomer.PageCount - 1) Then
            BindDataGrid(dgSAPCustomer.PageCount)
        Else
            If dgSAPCustomer.PageCount > 1 Then
                BindDataGrid(dgSAPCustomer.PageCount - 1)
            Else
                BindDataGrid(0)
            End If
        End If
    End Sub

    Private Function IsCustomerExist() As Boolean
        Dim vReturn As Boolean = False
        Try
            Dim arrSAPCustomer As ArrayList
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "Phone", MatchType.Exact, txtTelp.Text))
            criteria.opAnd(New Criteria(GetType(SAPCustomer), "Status", MatchType.No, CInt(EnumSAPCustomerStatus.SAPCustomerStatus.Deal_SPK)))
            If txtVehicleType.Text.Trim <> String.Empty Then
                criteria.opAnd(New Criteria(GetType(SAPCustomer), "VechileType.VechileTypeCode", MatchType.Exact, txtVehicleType.Text.Trim))
            End If
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
    Protected Sub ddlTipeKartuIdentitas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipeKartuIdentitas.SelectedIndexChanged
        'Dim TipeKendaraan As String
        'TipeKendaraan = txtVehicleType.Text

        'If Not (ddlTipeKartuIdentitas.SelectedValue = "") Then
        '    BindLeadTypeColor(ddlWarna, TipeKendaraan)
        'Else
        '    BindLeadTypeColor(ddlWarna, TipeKendaraan)
        'End If
    End Sub
    Protected Sub ddlCostumerType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCostumerType.SelectedIndexChanged
        If Not (ddlCostumerType.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan) Then
            RequiredFieldValidator11.Enabled = False
            ddlGender.SelectedIndex = 0
            ddlGender.Enabled = False
            'cr sfid
            BindLeadTypeIdentitas(ddlTipeKartuIdentitas, ddlCostumerType.SelectedValue)
            ddlPekerjaan.Enabled = False
            'BindLeadTypeColor(ddlWarna, txtVehicleType.Text)
            '
        Else
            RequiredFieldValidator11.Enabled = True
            ddlGender.Enabled = True
            'cr sfid
            BindLeadTypeIdentitas(ddlTipeKartuIdentitas, ddlCostumerType.SelectedValue)
            ddlPekerjaan.Enabled = True
            'BindLeadTypeColor(ddlWarna, txtVehicleType.Text)
            '
        End If

        If ddlWarna.SelectedValue <> String.Empty Then
            Dim TipeKendaraan As String
            TipeKendaraan = txtVehicleType.Text
            TxtJumlahDiskon.Text = 0
            BindLeadPrice(ddlHargaPriceListDealer, ddlWarna.SelectedValue, TipeKendaraan)
        End If
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

    Protected Sub ddlTypeBBN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTypeBBN.SelectedIndexChanged
        'Cek Blanko
        'Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        'If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
        '    Dim maxFileSize As Integer = 2048000
        '    'Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

        '    If DataFile.PostedFile.ContentLength > maxFileSize Then
        '        MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
        '        Exit Sub
        '    End If
        '    'cek ext
        '    Dim ext As String = System.IO.Path.GetExtension(DataFile.PostedFile.FileName)

        '    If Not CheckExt(ext.Substring(1)) Then
        '        MessageBox.Show("Extension file tidak sesuai. Ubah ke *.PDF/DOC/DOCX/JPG/PGN,.")
        '        Exit Sub
        '    End If

        '    Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)   '-- Source file name
        '    SrcFile = New Date().Now.ToString("yyyyMMddhhmmss") & SrcFile
        '    Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile  '-- Temporary file

        '    '-- Impersonation to manipulate file in server
        '    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        '    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        '    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        '    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        '    Try
        '        If imp.Start() Then
        '            '-- Copy data file from client to server temporary folder
        '            Dim objUpload As New UploadToWebServer
        '            objUpload.Upload(DataFile.PostedFile.InputStream, TempFile)

        '            imp.StopImpersonate()
        '            imp = Nothing

        '            btnAdd.Enabled = True
        '        Else
        '            btnAdd.Enabled = False
        '        End If
        '    Catch ex As Exception
        '        MessageBox.Show(SR.UploadFail(SrcFile))
        '    End Try
        'Else
        '    MessageBox.Show(SR.FileNotSelected)
        'End If
        ''
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
End Class
