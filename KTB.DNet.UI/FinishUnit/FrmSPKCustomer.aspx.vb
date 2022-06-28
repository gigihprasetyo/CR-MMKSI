#Region "Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.SAP
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Imports Newtonsoft
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json

#End Region

Public Class FrmSPKCustomer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipePerusahaan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTipePerusahaan As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodePelanggan As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefKodePlgn As System.Web.UI.WebControls.Label
    Protected WithEvents txtRefKodePelanggan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lbtnRefKode As System.Web.UI.WebControls.Label
    Protected WithEvents lnkReloadPlg As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblNama1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblNama2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNama2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblGedung As System.Web.UI.WebControls.Label
    Protected WithEvents txtGedung As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAlamat As System.Web.UI.WebControls.Label
    Protected WithEvents txtAlamat As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblKelurahan As System.Web.UI.WebControls.Label
    Protected WithEvents txtKelurahan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKecamatan As System.Web.UI.WebControls.Label
    Protected WithEvents txtKecamatan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKodePos As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodePos As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPropinsi As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPropinsi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCetakTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblCetak As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCetak As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPreArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKota As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCaraBayar As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKepemilikan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSbgKendaraan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlUsiaPemilik As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPemakaianUtama As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlBidangUsaha As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlDaerahOperasi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtOffice As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHome As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHp As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblOfficeNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblHomeNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblHpNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
    Protected WithEvents ddlNomerID As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents txtNomerID As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents pnlPerorangan As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPerusahaan As System.Web.UI.WebControls.Panel
    Protected WithEvents PnlBUMN As System.Web.UI.WebControls.Panel
    Protected WithEvents PnlLainnya As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlTambahan As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents CompareValidator1 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents Comparevalidator2 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents Comparevalidator3 As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents hdnMCPConfirmation As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnVerifyMCP As System.Web.UI.HtmlControls.HtmlInputHidden


    Protected WithEvents hdnLKPPConfirmation As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnVerifyLKPP As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected WithEvents tblInformasi As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents trUpload As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents ddlIdentity As DropDownList
    Protected WithEvents lblAttachment As Label
    Protected WithEvents fileUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents photoView As System.Web.UI.WebControls.Image
    Protected WithEvents txtJSon As TextBox
    Protected WithEvents btnUpload As Button
    Protected WithEvents hdnUpload As HiddenField
    Protected WithEvents hdnGetApi As HiddenField
    Protected WithEvents btnGetApi As Button
    Protected WithEvents btnUplouadJS As HtmlInputButton
    Protected WithEvents hdnEnabled As HiddenField
    Protected WithEvents hdnNameEnabled As HiddenField
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents Requiredfieldvalidator9 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RegularExpressionValidator6 As System.Web.UI.WebControls.RegularExpressionValidator
    'CR_SPK_SALESMAN_CUSTOMER_ENHANCE juni 2021
    Protected WithEvents ddlTipePerorangan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlIdentityAsing As DropDownList
    Protected WithEvents TxtFlag As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCountryCode As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtCountryName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchCountryName As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnSearchCountryName As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnExportToExcel As System.Web.UI.WebControls.Button
    'Protected WithEvents icTanggalLahir As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents TglLahir As System.Web.UI.WebControls.TextBox
    Protected WithEvents TglLahirCW As System.Web.UI.WebControls.TextBox
    'end

    Protected WithEvents txtLKPPNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblsearchLKPP As System.Web.UI.WebControls.Label
    Protected WithEvents trLKPPNumber As HtmlTableRow


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        Renderpanel()
    End Sub

#End Region

#Region " Private Variables"

    Private sessionHelper As New SessionHelper
    Private ReadOnly varUpload As String = "\OCR\"
    Private objSPKHeader As SPKHeader
    Private objSPKCustomer As SPKCustomer
    Private objLoginDealer As Dealer
    Private objUserInfo As UserInfo
    Private mode As enumMode.Mode
    Private _vstSPKCustomer As String = "_vstSPKCustomer"
    Private _vstSPKHeader As String = "_vstSPKHeader"
    Private _isMMC As Boolean
    Private emailMandatory As Boolean = False
    'CR SPK
    Private SelectedDate As String
    '
#End Region

#Region "Event handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        trLKPPNumber.Visible = False
        If IsPostBack Then
            If Me.hdnMCPConfirmation.Value = "1" Then
                Me.btnSave_Click(Nothing, Nothing)
            ElseIf Me.hdnLKPPConfirmation.Value = "1" Then
                Me.btnSave_Click(Nothing, Nothing)

            End If

        End If
        objLoginDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        objUserInfo = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)

        If Not Page.IsPostBack Then
            If Not Request.Item("spkHeader") Is Nothing Then
                If IsNothing(ViewState.Item(Me._vstSPKHeader)) Then
                    ViewState.Add(Me._vstSPKHeader, Request.Item("spkHeader"))
                Else
                    ViewState.Item(Me._vstSPKHeader) = Request.Item("spkHeader")
                End If
            Else
                If IsNothing(ViewState.Item(Me._vstSPKHeader)) Then
                    ViewState.Add(Me._vstSPKHeader, "FrmSPKHeader.SPKCustomer" & Now.ToString("yyyyMMddhhmmssfff"))
                Else
                    ViewState.Item(Me._vstSPKHeader) = "FrmSPKHeader.SPKHeader" & Now.ToString("yyyyMMddhhmmssfff")
                End If
            End If
            'objSPKHeader = sessionHelper.GetSession(Request.QueryString("spkHeader"))
            objSPKHeader = sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader))
            ViewState("Mode") = CType(Request.QueryString("Mode"), enumMode.Mode)
            mode = ViewState("Mode")
            If Request.QueryString("Id") <> String.Empty And mode <> enumMode.Mode.NewItemMode Then
                objSPKCustomer = objSPKHeader.SPKCustomer
                sessionHelper.SetSession(ViewState.Item(Me._vstSPKCustomer), objSPKCustomer)
            Else
                If sessionHelper.GetSession(ViewState.Item(Me._vstSPKCustomer)) = "" Then
                    objSPKCustomer = objSPKHeader.SPKCustomer
                    sessionHelper.SetSession(ViewState.Item(Me._vstSPKCustomer), objSPKCustomer)
                Else
                    objSPKCustomer = CType(sessionHelper.GetSession(ViewState.Item(Me._vstSPKCustomer)), SPKCustomer)
                End If
            End If

            If (Request.QueryString("CustId") <> String.Empty) AndAlso IsNothing(objSPKCustomer) Then
                Dim objSAPCustomer As SAPCustomer
                Dim objSAPCustomerFacade As SAPCustomerFacade = New SAPCustomerFacade(User)
                objSAPCustomer = objSAPCustomerFacade.Retrieve(CInt(Request.QueryString("CustId")))

                objSPKCustomer = New SPKCustomer
                If Not IsNothing(objSAPCustomer) Then
                    objSPKCustomer.SAPCustomer = objSAPCustomer
                    objSPKCustomer.Name1 = objSAPCustomer.CustomerName
                    objSPKCustomer.Alamat = objSAPCustomer.CustomerAddress
                    objSPKCustomer.Email = objSAPCustomer.Email
                    objSPKCustomer.HpNo = objSAPCustomer.Phone
                    objSPKCustomer.TipeCustomer = objSAPCustomer.CustomerType
                End If

                sessionHelper.SetSession(ViewState.Item(Me._vstSPKCustomer), objSPKCustomer)
            End If

            If objSPKCustomer Is Nothing Then
                objSPKCustomer = New SPKCustomer
            End If

            BindDropDown()

            FillForm()
            'BindSPKCustomerID()

            If Not IsNothing(Request.QueryString("EmailMandatory")) Then
                emailMandatory = CType(Request.QueryString("EmailMandatory"), Boolean)
                If emailMandatory Then
                    Requiredfieldvalidator9.Enabled = True
                    RegularExpressionValidator6.Enabled = True
                Else
                    Requiredfieldvalidator9.Enabled = False
                    RegularExpressionValidator6.Enabled = False
                End If
            End If

            If Not objSPKHeader Is Nothing AndAlso Not IsNothing(objSPKHeader.Dealer) Then

                Dim criteriasDealerSystems As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasDealerSystems.opAnd(New Criteria(GetType(DealerSystems), "Dealer.ID", MatchType.Exact, objSPKHeader.Dealer.ID))
                Dim DealerSystemsFacade As DealerSystemsFacade = New DealerSystemsFacade(User)
                Dim arlDealerSystem As ArrayList = DealerSystemsFacade.Retrieve(criteriasDealerSystems)
                For Each objDealerSystem As DealerSystems In arlDealerSystem
                    If objDealerSystem.isSPKDNET Then
                    Else
                        If Not objSPKHeader Is Nothing Then
                            If Not CType(objSPKHeader.CreatedTime, Date) < objDealerSystem.GoLiveDate Then
                                ViewState("SPKDMS") = True
                                btnSave.Visible = False
                            End If
                        End If
                    End If
                Next
            End If

        End If
        'cr spk
        If (TxtFlag.Text = "Domestik") Then
            txtCountryCode.Text = "62"
        End If

        lblSearchCountryName.Attributes("onclick") = "ShowPopUpSPKMasterCountryCode();"
        lblsearchLKPP.Attributes("onClick") = "ShowLKPPSelection();"

    End Sub

    Private Sub lnkReloadPlg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkReloadPlg.Click
        If txtRefKodePelanggan.Text.Trim <> String.Empty Then
            Dim objCustomer As Customer = New CustomerDealerFacade(User).RetrieveByRefCode(txtRefKodePelanggan.Text.Trim, CType(sessionHelper.GetSession("DEALER"), Dealer).ID)
            If objCustomer.ID <> 0 Then
                'If GetCategoriForCustomer(txtRefKodePelanggan.Text.Trim).ID <> 0 Then
                '    ddlTipe.SelectedValue = GetCategoriForCustomer(txtRefKodePelanggan.Text.Trim).TipeCustomer
                'End If
                txtNama.Text = objCustomer.Name1.ToUpper
                txtNama2.Text = objCustomer.Name2.ToUpper
                txtGedung.Text = objCustomer.Name3.ToUpper
                txtAlamat.Text = objCustomer.Alamat.ToUpper
                txtKelurahan.Text = objCustomer.Kelurahan.ToUpper
                txtKecamatan.Text = objCustomer.Kecamatan.ToUpper
                ddlPropinsi.SelectedValue = objCustomer.City.Province.ID
                ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
                ddlKota.SelectedValue = objCustomer.City.ID
                If objCustomer.PreArea <> String.Empty And objCustomer.PreArea <> "blank" Then
                    Try
                        ddlPreArea.SelectedValue = objCustomer.PreArea.Substring(0, 1).ToUpper + objCustomer.PreArea.Substring(1).ToUpper
                    Catch ex As Exception
                        ddlPreArea.SelectedIndex = 0
                    End Try
                Else
                    ddlPreArea.SelectedIndex = 0
                End If
                If objCustomer.PrintRegion = "X" Then
                    ddlCetak.SelectedValue = 0
                Else
                    ddlCetak.SelectedValue = 1
                End If

                txtKodePos.Text = objCustomer.PostalCode
                txtEmail.Text = objCustomer.Email
            Else
                MessageBox.Show("Referensi kode pelanggan tidak valid")
            End If
        Else
            MessageBox.Show("Referensi kode pelanggan tidak valid")
        End If
    End Sub

    Private Sub ddlTipe_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlTipe.SelectedIndexChanged
        'If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan Then
        '    ddlTipePerusahaan.Visible = True
        '    Try
        '        ddlTipePerusahaan.Items.Clear()
        '        ddlTipePerusahaan.DataSource = New EnumTipePerusahaan().RetrieveType
        '        ddlTipePerusahaan.DataTextField = "NameTipe"
        '        ddlTipePerusahaan.DataValueField = "ValTipe"
        '        ddlTipePerusahaan.DataBind()
        '    Catch ex As Exception
        '        ddlTipePerusahaan.Visible = False
        '    End Try
        'End If
        trUpload.Style.Clear()
        trUpload.Style.Add("Display", "none")
        hdnUpload.Value = "0"
        ddlTipePerusahaan.Visible = False
        'CR_SPK_SALESMAN_CUSTOMER_ENHANCE juni 2021 By Endang
        ddlTipePerorangan.Visible = False
        ddlIdentityAsing.Visible = False
        'end
        If ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perorangan Then
            pnlPerorangan.Visible = True
            pnlPerusahaan.Visible = False
            PnlBUMN.Visible = False
            PnlLainnya.Visible = False
            'CR_SPK_SALESMAN_CUSTOMER_ENHANCE juni 2021
            ddlTipePerorangan.Visible = True

            ddlTipePerorangan.Items.Clear()
            ddlTipePerorangan.DataSource = New EnumTipePerorangan().RetrieveType
            ddlTipePerorangan.DataTextField = "NameTipe"
            ddlTipePerorangan.DataValueField = "ValTipe"
            ddlTipePerorangan.DataBind()
            'ddlTipePerorangan_SelectedIndexChanged(Me, System.EventArgs.Empty)
            txtCountryCode.Text = "62"
            lblSearchCountryName.Visible = True
            'end
            trUpload.Style.Clear()
            trUpload.Style.Add("Display", "block")
        ElseIf ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perusahaan Then
            pnlPerorangan.Visible = False
            pnlPerusahaan.Visible = True
            PnlBUMN.Visible = False
            PnlLainnya.Visible = False
            ddlTipePerusahaan.Visible = True

            ddlTipePerusahaan.Items.Clear()
            ddlTipePerusahaan.DataSource = New EnumTipePerusahaan().RetrieveType
            ddlTipePerusahaan.DataTextField = "NameTipe"
            ddlTipePerusahaan.DataValueField = "ValTipe"
            ddlTipePerusahaan.DataBind()
            ddlTipePerusahaan_SelectedIndexChanged(Me, System.EventArgs.Empty)

        ElseIf ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.BUMN_Pemerintah Then
            pnlPerorangan.Visible = False
            pnlPerusahaan.Visible = False
            PnlBUMN.Visible = True
            PnlLainnya.Visible = False
        ElseIf ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Lainnya Then
            pnlPerorangan.Visible = False
            pnlPerusahaan.Visible = False
            PnlBUMN.Visible = False
            PnlLainnya.Visible = True

        End If

        'add start changes 2017/12/27 by rudi
        If ddlTipe.SelectedValue <> EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perusahaan Then
            If txtNama.Text.Trim <> "" Then
                'PT or CV or UD 
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(0) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(0) + ". "), "")
                End If
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(1) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(1) + ". "), "")
                End If
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(2) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(2) + ". "), "")
                End If
                'cr spk
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(3) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(3) + ". "), "")
                End If
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(4) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(4) + ". "), "")
                End If
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(5) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(5) + ". "), "")
                End If
                If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(6) + ". ") Then
                    txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(6) + ". "), "")
                End If
                'end
            End If
        End If
        'add end changes 

        'BindNomerID()
    End Sub

    Private Sub ddlPropinsi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPropinsi.SelectedIndexChanged
        ddlKota.Items.Clear()
        If ddlPropinsi.SelectedIndex <> 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, ddlPropinsi.SelectedValue))
            criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

            ddlKota.DataSource = New CityFacade(User).RetrieveActiveList(criterias, "CityName", Sort.SortDirection.ASC)
            ddlKota.DataTextField = "CityName".ToUpper
            ddlKota.DataValueField = "ID"
            ddlKota.DataBind()
        End If
        ddlKota.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlKota.SelectedIndex = 0
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        ''objSPKCustomer = sessionHelper.GetSession(viewstate.Item(Me._vstSPKCustomer))
        'Dim id As Integer = CType(Request.QueryString("Id"), Integer)
        'If id <= 0 Then
        '    objSPKCustomer = New SPKCustomer

        '    objSPKCustomer.Name1 = txtNama.Text
        '    objSPKCustomer.Name2 = txtNama2.Text
        '    objSPKCustomer.Name3 = txtGedung.Text
        '    objSPKCustomer.Alamat = txtAlamat.Text
        '    objSPKCustomer.Kelurahan = txtKelurahan.Text
        '    objSPKCustomer.Kecamatan = txtKecamatan.Text
        '    objSPKCustomer.PostalCode = txtKodePos.Text
        '    objSPKCustomer.City = New CityFacade(User).Retrieve(CType(ddlKota.SelectedValue, Integer))
        '    objSPKCustomer.ReffCode = txtRefKodePelanggan.Text.ToUpper
        '    objSPKCustomer.PrintRegion = ddlCetak.SelectedValue
        '    objSPKCustomer.PreArea = ddlPreArea.SelectedValue.ToUpper

        '    objSPKCustomer.OfficeNo = txtOffice.Text
        '    objSPKCustomer.HomeNo = txtHome.Text
        '    objSPKCustomer.HpNo = txtHp.Text
        '    objSPKCustomer.Email = txtEmail.Text


        '    Dim alGroup1 As ArrayList
        '    Dim alGroup2 As ArrayList
        '    Dim alGroup3 As ArrayList
        '    Dim alGroup4 As ArrayList
        '    Dim alGroup5 As ArrayList

        '    Dim alGroupTotal As ArrayList = New ArrayList

        '    Dim objRenderPanel As RenderingProfile
        '    'Perorangan
        '    If ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perorangan Then
        '        objRenderPanel = New RenderingProfile
        '        alGroup1 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), CType(EnumProfileType.ProfileType.SPKCUSTOMER, Short), User, True)

        '    Else
        '        alGroup1 = New ArrayList
        '    End If

        '    'Perusahaan
        '    If ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perusahaan Then
        '        objRenderPanel = New RenderingProfile
        '        alGroup2 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), CType(EnumProfileType.ProfileType.SPKCUSTOMER, Short), User, True)
        '        'Show Dropdownlist Company Title

        '    Else
        '        alGroup2 = New ArrayList
        '    End If

        '    'BUMN
        '    If ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.BUMN_Pemerintah Then
        '        objRenderPanel = New RenderingProfile
        '        alGroup4 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), CType(EnumProfileType.ProfileType.SPKCUSTOMER, Short), User, True)
        '    Else
        '        alGroup4 = New ArrayList
        '    End If

        '    'Lainnya
        '    If ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.BUMN_Pemerintah Then
        '        objRenderPanel = New RenderingProfile
        '        alGroup5 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), CType(EnumProfileType.ProfileType.SPKCUSTOMER, Short), User, True)
        '    Else
        '        alGroup5 = New ArrayList
        '    End If

        '    'Tambahan
        '    objRenderPanel = New RenderingProfile
        '    alGroup3 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), CType(EnumProfileType.ProfileType.SPKCUSTOMER, Short), User, True)

        '    sessionHelper.SetSession(viewstate.Item(Me._vstSPKCustomer), objSPKCustomer)
        'End If


        mode = ViewState("Mode")
        If mode = enumMode.Mode.NewItemMode Then
            Response.Redirect("FrmSPKHeader.aspx?Mode=0&CustId=" & Request.QueryString("CustId") & "&isBack=1&sessionName=" & ViewState.Item(Me._vstSPKHeader))
        ElseIf mode = enumMode.Mode.EditMode Then
            If Not IsNothing(Me.ViewState.Item(Me._vstSPKHeader)) Then
                objSPKHeader = CType(sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader)), SPKHeader)
                Response.Redirect("FrmSPKHeader.aspx?Id=" & objSPKHeader.ID & "&Mode=1&CustId=" & Request.QueryString("CustId") & "&isBack=1&sessionName=" & ViewState.Item(Me._vstSPKHeader))
            Else
                Response.Redirect("FrmSPKHeader.aspx?&Mode=1&CustId=" & Request.QueryString("CustId") & "&isBack=1&sessionName=" & ViewState.Item(Me._vstSPKHeader))
            End If
        ElseIf mode = enumMode.Mode.ViewMode Then
            If Not IsNothing(Me.ViewState.Item(Me._vstSPKHeader)) Then
                objSPKHeader = CType(sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader)), SPKHeader)
                Response.Redirect("FrmSPKHeader.aspx?Id=" & objSPKHeader.ID & "&Mode=2&CustId=" & Request.QueryString("CustId") & "&isBack=1&sessionName=" & ViewState.Item(Me._vstSPKHeader))
            Else
                Response.Redirect("FrmSPKHeader.aspx?&Mode=2&CustId=" & Request.QueryString("CustId") & "&isBack=1&sessionName=" & ViewState.Item(Me._vstSPKHeader))
            End If
            'Response.Redirect("FrmSPKHeader.aspx?Id=" & ID & "&Mode=2&CustId" & Request.QueryString("CustId") & "&isBack=1&sessionName=" & ViewState.Item(Me._vstSPKHeader))
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'SetNomerIDValue()
        'CR SPK
        Dim Tgl As Date
        Dim TglAuto As Date
        Dim JK As Boolean = False
        Dim str As String
        Dim KTP As String
        'end CR SPK
        'validasi hanya jika kode negara 62
        If txtCountryCode.Text = "62" Then
            If txtHp.Text.Trim.Length < 9 Then
                MessageBox.Show("No Handphone minimal 9 digit")
                Exit Sub
            End If
            'remark SPK 
            If txtHp.Text.Trim.Substring(0, 2) <> "08" Then
                MessageBox.Show("format input No. Handphone  : 08XXXXXXXXX")
                Exit Sub
            End If
            'CR SPK remar phase 2
            'If txtHp.Text.Trim.Substring(0, 1) <> "8" Then
            '    MessageBox.Show("format input No. Handphone  : 8XXXXXXXXX")
            '    Exit Sub
            'End If
            If txtHp.Text.Trim.Length > 13 Then
                MessageBox.Show("No Handphone Maksimal 12 digit")
                Exit Sub
            End If
        End If
        
        If txtEmail.Text.Trim.Length > 0 Then
            If Not EmailAddressCheck(txtEmail.Text.Trim) Then
                MessageBox.Show("Format Email Salah")
                Exit Sub
            End If
        End If

        'end

        If txtNama.Text.Trim.Length > 40 Then
            MessageBox.Show("Nama 1 melebihi 40 karakter")
            Exit Sub
        End If

        If txtNama2.Text.Trim.Length > 35 Then
            MessageBox.Show("Nama 2 melebihi 35 karakter")
            Exit Sub
        End If

        If txtGedung.Text.Trim.Length > 40 Then
            MessageBox.Show("Gedung melebihi 40 karakter")
            Exit Sub
        End If

        If txtAlamat.Text.Trim.Length > 60 Then
            MessageBox.Show("Alamat melebihi 60 karakter")
            Exit Sub
        End If

        If txtKelurahan.Text.Trim.Length > 40 Then
            MessageBox.Show("Kelurahan melebihi 40 karakter")
            Exit Sub
        End If

        If txtKecamatan.Text.Trim.Length > 35 Then
            MessageBox.Show("Kecamatan melebihi 35 karakter")
            Exit Sub
        End If



        _isMMC = False
        If Not IsMCPValid(IsNothing(sender)) Then
            Exit Sub
        End If


        If _isMMC Then
            If Not IsLKPPValid(IsNothing(sender)) Then Exit Sub
        End If


        If ValidateSave() Then
            If ValidateRefCustomerCode() Then
                Try

                    Dim alGroup1 As ArrayList
                    Dim alGroup2 As ArrayList
                    Dim alGroup3 As ArrayList
                    Dim alGroup4 As ArrayList
                    Dim alGroup5 As ArrayList

                    Dim alGroupTotal As ArrayList = New ArrayList

                    Dim objRenderPanel As RenderingProfile
                    'Perorangan
                    If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan Then
                        objRenderPanel = New RenderingProfile
                        alGroup1 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), CType(EnumProfileType.ProfileType.SPKCUSTOMER, Short), User, True)
                        'CR SPK
                        For Each oCrp2 As SPKCustomerProfile In alGroup1
                            If oCrp2.ProfileHeader.Code = "NOKTP" Then
                                KTP = oCrp2.ProfileValue
                                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomerProfile), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
                                crit.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileValue", MatchType.Exact, KTP))
                                Dim arlRevDet As ArrayList = New SPKCustomerProfileFacade(User).Retrieve(crit)
                                If arlRevDet.Count > 0 Then
                                    MessageBox.Show("Identity Number Duplicate")
                                End If
                            End If

                            If oCrp2.ProfileHeader.Code = "JK" Then
                                If oCrp2.ProfileValue = "LK" Then
                                    JK = True
                                End If
                            End If
                            If oCrp2.ProfileHeader.Code = "TGLLAHIR" Then
                                TglAuto = oCrp2.ProfileValue

                                Dim dt As DateTime = Convert.ToDateTime(TglAuto)
                                Dim format As String = "ddMMyy"
                                str = dt.ToString(format)

                                TglLahirCW.Text = str


                            End If
                        Next
                        'End
                        For Each oCrp As SPKCustomerProfile In alGroup1
                            If oCrp.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar AndAlso (IsNothing(oCrp.ProfileValue) OrElse oCrp.ProfileValue.Trim = "") Then
                                MessageBox.Show(oCrp.ProfileHeader.Description.ToString & " Harus diisi")
                                Exit Sub
                            End If
                            If oCrp.ProfileHeader.Code = "NOKTP" Then
                                If (TxtFlag.Text = "domestik") Then
                                    'KTP validation
                                    If oCrp.ProfileValue.Length < 16 AndAlso ddlIdentity.SelectedValue.ToString() = "0" Then
                                        MessageBox.Show("Identity Number minimum 16 karakter")
                                        Exit Sub
                                    End If
                                    If oCrp.ProfileValue.Length > 16 AndAlso ddlIdentity.SelectedValue.ToString() = "0" Then
                                        MessageBox.Show("Identity Number maksimal 16 karakter")
                                        Exit Sub
                                    End If
                                    'KTP validation tgl lahir dan jenis kelamin
                                    If JK = True Then
                                        If ddlIdentity.SelectedValue.ToString() = "0" AndAlso oCrp.ProfileValue.Substring(6, 6) <> str Then
                                            MessageBox.Show("Identity Number tidak sesuai dengan tgl lahir")
                                            Exit Sub
                                        End If
                                    End If
                                    If JK = False Then
                                        Dim lahir As String = oCrp.ProfileValue.Substring(6, 2)
                                        Dim Cewe As Integer = Int64.Parse(lahir)
                                        Dim kurang40 As Integer = Cewe - 40
                                        Dim Tgllahir2digit As String = str.Substring(0, 2)
                                        Dim Tgllahir4digit As String = str.Substring(2, 4)
                                        Dim Tgllahir2digitINT As Integer = Int64.Parse(Tgllahir2digit)

                                        If ddlIdentity.SelectedValue.ToString() = "0" Then
                                            If Tgllahir2digitINT <> kurang40 Then
                                                MessageBox.Show("Identity Number tidak sesuai dengan tgl lahir")
                                                Exit Sub
                                            End If

                                        End If
                                    End If

                                End If

                                'Asing
                                If (TxtFlag.Text = "asing") Then
                                    'KITAS validation
                                    If oCrp.ProfileValue.Length < 11 AndAlso ddlIdentityAsing.SelectedValue.ToString() = "2" Then
                                        MessageBox.Show("Identity Number minimum 11 karakter")
                                        Exit Sub
                                    End If
                                    If oCrp.ProfileValue.Length > 11 AndAlso ddlIdentityAsing.SelectedValue.ToString() = "2" Then
                                        MessageBox.Show("Identity Number maksimal 11 karakter")
                                        Exit Sub
                                    End If
                                    'KITAP validation
                                    If oCrp.ProfileValue.Length < 11 AndAlso ddlIdentityAsing.SelectedValue.ToString() = "3" Then
                                        MessageBox.Show("Identity Number minimum 11 karakter")
                                        Exit Sub
                                    End If
                                    If oCrp.ProfileValue.Length > 11 AndAlso ddlIdentityAsing.SelectedValue.ToString() = "3" Then
                                        MessageBox.Show("Identity Number maksimal 11 karakter")
                                        Exit Sub
                                    End If
                                    'End validation
                                End If
                                'CR SPK
                            End If


                            If oCrp.ProfileHeader.Code = "NOTELP" Then
                                'CR SPK
                                'If oCrp.ProfileValue.Length < 10 OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                                If oCrp.ProfileValue.Length < 9 Then
                                    MessageBox.Show("No Telp minimum 9 digit")
                                    Exit Sub
                                End If
                                If oCrp.ProfileValue.Length > 12 Then
                                    MessageBox.Show("No Telp max 12 digit")
                                    Exit Sub
                                End If
                                ''remark for phase 1
                                'If oCrp.ProfileValue.Substring(0, 1) <> "8" Then
                                '    MessageBox.Show("format input No. Telp  Harus : 8XXXXXXXXX")
                                '    Exit Sub
                                'End If
                                'END
                            End If
                        Next
                    Else
                        alGroup1 = New ArrayList
                    End If

                    'Perusahaan
                    If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan Then
                        objRenderPanel = New RenderingProfile
                        alGroup2 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), CType(EnumProfileType.ProfileType.SPKCUSTOMER, Short), User, True)
                        'Show Dropdownlist Company Title
                        For Each oCrp As SPKCustomerProfile In alGroup2
                            If oCrp.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar AndAlso oCrp.ProfileValue.Trim = "" Then
                                MessageBox.Show(oCrp.ProfileHeader.Description.ToString & " Harus diisi")
                                Exit Sub
                            End If
                            If oCrp.ProfileHeader.Code = "NOKTP" Then
                                'cr spk validasi perusahaan type
                                If Not ddlTipePerusahaan.SelectedValue = EnumTipePerusahaan.EnumTipePerusahaan.Yayasan Then
                                    If oCrp.ProfileValue.Length < 10 Then
                                        If Not IdentitasCheck(oCrp.ProfileValue) Then
                                            MessageBox.Show("No IDENTITAS harus Alfanumerik dan min 10 digit")
                                            Exit Sub
                                        End If
                                    End If
                                    'phase 2
                                    If oCrp.ProfileValue.Length > 50 Then
                                        If Not IdentitasCheck(oCrp.ProfileValue) Then
                                            MessageBox.Show("No IDENTITAS harus Alfanumerik dan max 50 digit")
                                            Exit Sub
                                        End If

                                    End If
                                    'end
                                Else
                                    If oCrp.ProfileValue.Length < 9 Then
                                        If Not AngkaCheck(oCrp.ProfileValue) Then
                                            MessageBox.Show("No IDENTITAS min 9 dan harus angka")
                                            Exit Sub
                                        End If
                                    End If
                                    If oCrp.ProfileValue.Length > 40 Then
                                        If Not AngkaCheck(oCrp.ProfileValue) Then
                                            MessageBox.Show("No IDENTITAS max 40 dan harus angka")
                                            Exit Sub
                                        End If
                                    End If
                                End If

                            End If
                            If oCrp.ProfileHeader.Code = "NOTELP" Then
                                If oCrp.ProfileValue.Length < 8 OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                                    MessageBox.Show("No Telp tidak benar")
                                    Exit Sub
                                End If
                            End If
                        Next
                    Else
                        alGroup2 = New ArrayList
                    End If

                    'BUMN
                    If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
                        objRenderPanel = New RenderingProfile
                        alGroup4 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), CType(EnumProfileType.ProfileType.SPKCUSTOMER, Short), User, True)
                        For Each oCrp As SPKCustomerProfile In alGroup4
                            If oCrp.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar AndAlso oCrp.ProfileValue.Trim = "" Then
                                MessageBox.Show(oCrp.ProfileHeader.Description.ToString & " Harus diisi")
                                Exit Sub
                            End If
                            If oCrp.ProfileHeader.Code = "NOKTP" Then
                                If oCrp.ProfileValue.Length < 1 Then
                                    MessageBox.Show("Identity Number tidak benar")
                                    Exit Sub
                                End If
                                If oCrp.ProfileValue.Length > 40 Then
                                    MessageBox.Show("Identity Number maksimal 40 karakter")
                                    Exit Sub
                                End If
                            End If
                            If oCrp.ProfileHeader.Code = "NOTELP" Then
                                If oCrp.ProfileValue.Length < 6 OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                                    MessageBox.Show("No Telp tidak benar")
                                    Exit Sub
                                End If
                            End If
                        Next
                    Else
                        alGroup4 = New ArrayList
                    End If

                    'Lainnya
                    If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya Then
                        objRenderPanel = New RenderingProfile
                        alGroup5 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), CType(EnumProfileType.ProfileType.SPKCUSTOMER, Short), User, True)
                        For Each oCrp As SPKCustomerProfile In alGroup5
                            If oCrp.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar AndAlso oCrp.ProfileValue.Trim = "" Then
                                MessageBox.Show(oCrp.ProfileHeader.Description.ToString & " Harus diisi")
                                Exit Sub
                            End If
                            If oCrp.ProfileHeader.Code = "NOTELP" Then
                                If oCrp.ProfileValue.Length < 6 OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                                    MessageBox.Show("No Telp tidak benar")
                                    Exit Sub
                                End If
                                If oCrp.ProfileValue.Length > 40 Then
                                    MessageBox.Show("Identity Number maksimal 40 karakter")
                                    Exit Sub
                                End If
                            End If
                        Next
                    Else
                        alGroup5 = New ArrayList
                    End If

                    'Tambahan
                    objRenderPanel = New RenderingProfile
                    alGroup3 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), CType(EnumProfileType.ProfileType.SPKCUSTOMER, Short), User, True)
                    For Each oCrp As SPKCustomerProfile In alGroup3
                        If oCrp.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar AndAlso oCrp.ProfileValue.Trim = "" Then
                            MessageBox.Show(oCrp.ProfileHeader.Description.ToString & " Harus diisi")
                            Exit Sub
                        End If
                        If oCrp.ProfileHeader.Code = "NOTELP" Then
                            If oCrp.ProfileValue.Length < 6 OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                                MessageBox.Show("No Telp tidak benar")
                                Exit Sub
                            End If
                            If oCrp.ProfileValue.Length > 40 Then
                                MessageBox.Show("Identity Number maksimal 40 karakter")
                                Exit Sub
                            End If
                        End If
                    Next

                    Dim nResult As Integer
                    If sessionHelper.GetSession(ViewState.Item(Me._vstSPKCustomer)) Is Nothing Then
                        objSPKCustomer = New SPKCustomer
                    Else
                        objSPKCustomer = sessionHelper.GetSession(ViewState.Item(Me._vstSPKCustomer))
                    End If

                    'If Request.QueryString("CustId") <> String.Empty And mode = enumMode.Mode.NewItemMode Then
                    '    Dim objSAPCustomer As SAPCustomer
                    '    Dim objSAPCustomerFacade As SAPCustomerFacade = New SAPCustomerFacade(User)
                    '    objSAPCustomer = objSAPCustomerFacade.Retrieve(CInt(Request.QueryString("CustId")))
                    '    objSPKCustomer = New SPKCustomer
                    '    If Not IsNothing(objSAPCustomer) Then
                    '        objSPKCustomer.SAPCustomer = objSAPCustomer
                    '    End If
                    'End If

                    If IsNothing(objSPKCustomer.SAPCustomer) Then
                        MessageBox.Show("SAP Customer is null")
                        Exit Sub
                    End If

                    'Add start rudi
                    'code below commented by johan, request from miyuki
                    'ChangesNamaByTipeCustomer()
                    'Add end rudi

                    objSPKCustomer.TipeCustomer = CInt(ddlTipe.SelectedValue)
                    If ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perusahaan Then
                        objSPKCustomer.TipePerusahaan = CInt(ddlTipePerusahaan.SelectedValue)
                        'cr spk
                        If objSPKCustomer.TipePerusahaan < 6 Then ' PT,PP,CV,UD,PF
                            objSPKCustomer.TypeIdentitas = 4 'TDP
                        ElseIf objSPKCustomer.TipePerusahaan = 6 Then 'YAYASAN
                            objSPKCustomer.TypeIdentitas = 5 'TDY
                        Else ' KOPERASI
                            objSPKCustomer.TypeIdentitas = 6 'SIK
                        End If
                        '
                    End If
                    'CR SPK
                    If ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perorangan Then
                        objSPKCustomer.TypePerorangan = CInt(ddlTipePerorangan.SelectedValue)
                        If ddlTipePerorangan.SelectedValue = EnumTipePerorangan.EnumTipePerorangan.Domestik Then
                            objSPKCustomer.TypeIdentitas = CInt(ddlIdentity.SelectedValue)
                        ElseIf ddlTipePerorangan.SelectedValue = EnumTipePerorangan.EnumTipePerorangan.Asing Then
                            objSPKCustomer.TypeIdentitas = CInt(ddlIdentityAsing.SelectedValue)
                        End If
                    End If
                    'end
                    objSPKCustomer.Name1 = txtNama.Text.Trim
                    objSPKCustomer.Name2 = txtNama2.Text.Trim
                    objSPKCustomer.Name3 = txtGedung.Text.Trim
                    objSPKCustomer.Alamat = txtAlamat.Text.Trim
                    objSPKCustomer.Kelurahan = txtKelurahan.Text.Trim
                    objSPKCustomer.Kecamatan = txtKecamatan.Text.Trim
                    objSPKCustomer.PostalCode = IIf(txtKodePos.Text.Trim.Length < 5, "00000", txtKodePos.Text)
                    objSPKCustomer.City = New CityFacade(User).Retrieve(CType(ddlKota.SelectedValue, Integer))
                    objSPKCustomer.ReffCode = txtRefKodePelanggan.Text.ToUpper
                    objSPKCustomer.PrintRegion = ddlCetak.SelectedValue
                    objSPKCustomer.PreArea = ddlPreArea.SelectedValue.ToUpper

                    objSPKCustomer.OfficeNo = txtOffice.Text.Trim
                    objSPKCustomer.HomeNo = txtHome.Text.Trim
                    objSPKCustomer.HpNo = txtHp.Text.Trim
                    objSPKCustomer.Email = txtEmail.Text.Trim

                    objSPKCustomer.MCPStatus = hdnVerifyMCP.Value
                    objSPKCustomer.LKPPStatus = hdnVerifyLKPP.Value

                    'CR SPK
                    objSPKCustomer.CountryCode = txtCountryCode.Text.Trim
                    '

                    For Each item As SPKCustomerProfile In alGroup1
                        alGroupTotal.Add(item)
                    Next
                    For Each item As SPKCustomerProfile In alGroup2
                        alGroupTotal.Add(item)
                    Next
                    For Each item As SPKCustomerProfile In alGroup3
                        alGroupTotal.Add(item)
                    Next
                    For Each item As SPKCustomerProfile In alGroup4
                        alGroupTotal.Add(item)
                    Next
                    For Each item As SPKCustomerProfile In alGroup5
                        alGroupTotal.Add(item)
                    Next
                    Dim ocr As New OCRIdentity
                    If objSPKCustomer.ID = 0 Then

                        If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan Then
                            If Not IsNothing(sessionHelper.GetSession("OCR")) AndAlso 1 = 1 Then
                                ocr = sessionHelper.GetSession("OCR")
                            End If

                            objSPKCustomer.ImagePath = lblAttachment.Text
                        Else
                            objSPKCustomer.ImagePath = ""
                        End If
                        nResult = New SPKCustomerFacade(User).Insert(objSPKCustomer, alGroupTotal, ocr)
                        'ViewState("Mode") = enumMode.Mode.EditMode
                    Else
                        If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan Then
                            If Not IsNothing(sessionHelper.GetSession("OCR")) AndAlso 1 = 1 Then
                                ocr = sessionHelper.GetSession("OCR")
                            End If

                            objSPKCustomer.ImagePath = lblAttachment.Text
                        Else
                            objSPKCustomer.ImagePath = ""
                        End If

                        nResult = New SPKCustomerFacade(User).Update(objSPKCustomer, alGroup1, alGroup2, alGroup3, alGroup4, alGroup5, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), ocr)
                    End If

                    objSPKCustomer = New SPKCustomerFacade(User).Retrieve(nResult)
                    sessionHelper.SetSession(ViewState.Item(Me._vstSPKCustomer), objSPKCustomer)
                    objSPKHeader = CType(sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader)), SPKHeader)
                    objSPKHeader.SPKCustomer = objSPKCustomer
                    sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader)

                    MessageBox.Show("Data Konsumen Berhasil Disimpan")
                    btnSave.Enabled = False
                    txtEmail.Enabled = False
                Catch ex As Exception
                    MessageBox.Show("Data gagal disimpan. Periksa kembali data konsumen")
                    btnSave.Enabled = True
                End Try
            End If
        End If

    End Sub

    'Add start rudi
    Private Sub ddlTipePerusahaan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipePerusahaan.SelectedIndexChanged
        ChangesNamaByTipeCustomer()
    End Sub
    'Add end rudi
    'CR_SPK_SALESMAN_CUSTOMER_ENHANCE juni 2021
    Private Sub ddlTipePerorangan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipePerorangan.SelectedIndexChanged
        'ChangesNamaByTipeCustomer()


        If ddlTipePerorangan.SelectedValue = EnumTipePerorangan.EnumTipePerorangan.Domestik Then

            ddlIdentityAsing.Visible = False
            ddlIdentity.Visible = True
            TxtFlag.Text = "domestik"

            'CR SPK
            lblSearchCountryName.Visible = False
            txtCountryCode.Text = "62"
            '

        ElseIf ddlTipePerorangan.SelectedValue = EnumTipePerorangan.EnumTipePerorangan.Asing Then

            ddlIdentityAsing.Visible = True
            ddlIdentity.Visible = False
            TxtFlag.Text = "asing"
            'CR SPK
            lblSearchCountryName.Visible = True
            txtCountryCode.Text = ""
            '

        End If
    End Sub
    'END

#End Region

#Region "Custom Method"
    'add start rudi
    Private Sub ChangesNamaByTipeCustomer()
        mode = ViewState("Mode")
        If mode <> enumMode.Mode.EditMode Then
            If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan Then
                If Not txtNama.Text.Trim = "" Then
                    'PT or CV or UD 
                    If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(0) + ". ") Then
                        txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(0) + ". "), "")
                    End If
                    If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(1) + ". ") Then
                        txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(1) + ". "), "")
                    End If
                    If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(2) + ". ") Then
                        txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(2) + ". "), "")
                    End If
                    'cr spk
                    If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(3) + ". ") Then
                        txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(3) + ". "), "")
                    End If
                    If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(4) + ". ") Then
                        txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(4) + ". "), "")
                    End If
                    If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(5) + ". ") Then
                        txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(5) + ". "), "")
                    End If
                    If txtNama.Text.Trim.Contains(EnumTipePerusahaan.RetrieveNameTipePerusahaan(6) + ". ") Then
                        txtNama.Text = txtNama.Text.Replace((EnumTipePerusahaan.RetrieveNameTipePerusahaan(6) + ". "), "")
                    End If
                    'end
                End If
                'cr spk
                If Not ddlTipePerusahaan.SelectedValue = EnumTipePerusahaan.EnumTipePerusahaan.Koperasi Then
                    txtNama.Text = ddlTipePerusahaan.SelectedItem.Text.Trim + ". " + txtNama.Text.Trim.ToUpper
                End If
                If ddlTipePerusahaan.SelectedValue = EnumTipePerusahaan.EnumTipePerusahaan.Koperasi Then
                    txtNama.Text = ddlTipePerusahaan.SelectedItem.Text.Trim + ". " + txtNama.Text.Trim.ToUpper
                End If
                'end
            End If
        End If
    End Sub
    'add end rudi

    Private Sub Renderpanel()
        If Not Request.Item("spkHeader") Is Nothing Then
            objSPKHeader = CType(sessionHelper.GetSession(Request.Item("spkHeader")), SPKHeader)
        Else
            objSPKHeader = CType(sessionHelper.GetSession(Me.ViewState.Item(Me._vstSPKHeader)), SPKHeader)
        End If

        If IsNothing(objSPKHeader) Then
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), EnumProfileType.ProfileType.SPKCUSTOMER, pnlPerorangan)
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), EnumProfileType.ProfileType.SPKCUSTOMER, pnlPerusahaan)
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), EnumProfileType.ProfileType.SPKCUSTOMER, pnlTambahan)
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), EnumProfileType.ProfileType.SPKCUSTOMER, PnlBUMN)
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), EnumProfileType.ProfileType.SPKCUSTOMER, PnlLainnya)
        Else
            RenderProfilePanel(objSPKHeader.SPKCustomer, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), EnumProfileType.ProfileType.SPKCUSTOMER, pnlPerorangan)
            RenderProfilePanel(objSPKHeader.SPKCustomer, New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), EnumProfileType.ProfileType.SPKCUSTOMER, pnlPerusahaan)
            RenderProfilePanel(objSPKHeader.SPKCustomer, New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), EnumProfileType.ProfileType.SPKCUSTOMER, pnlTambahan)
            RenderProfilePanel(objSPKHeader.SPKCustomer, New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), EnumProfileType.ProfileType.SPKCUSTOMER, PnlBUMN)
            RenderProfilePanel(objSPKHeader.SPKCustomer, New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), EnumProfileType.ProfileType.SPKCUSTOMER, PnlLainnya)
        End If
    End Sub

    Private Sub RenderProfilePanel(ByVal objSPKCustomer As SPKCustomer, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel)
        Dim objRenderPanel As RenderingProfile

        If Not objSPKCustomer Is Nothing Then
            mode = ViewState("Mode")
            If mode = enumMode.Mode.ViewMode Then
                objRenderPanel = New RenderingProfile(True)
            Else
                objRenderPanel = New RenderingProfile(False)
            End If
            objRenderPanel.GeneratePanel(objSPKCustomer.ID, objPanel, objGroup, profileType, User)
        Else
            objRenderPanel = New RenderingProfile(False)
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User)
        End If


    End Sub

    Private Sub BindDropDown()
        ddlTipe.Items.Clear()
        ddlTipe.DataSource = New EnumTipePelangganCustomerRequest().RetrieveType
        ddlTipe.DataTextField = "NameTipe"
        ddlTipe.DataValueField = "ValTipe"
        ddlTipe.DataBind()
        ddlTipe_SelectedIndexChanged(Me, System.EventArgs.Empty)

        ddlCetak.Items.Clear()
        ddlCetak.Items.Add(New ListItem("YA", 0))
        ddlCetak.Items.Add(New ListItem("TIDAK", 1))
        ddlCetak.SelectedIndex = 1

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim list As ArrayList = New ProvinceFacade(User).RetrieveActiveList(criterias, "ProvinceName", Sort.SortDirection.ASC)
        ddlPropinsi.Items.Clear()
        ddlPropinsi.DataSource = list
        ddlPropinsi.DataTextField = "ProvinceName"
        ddlPropinsi.DataValueField = "ID"
        ddlPropinsi.DataBind()
        ddlPropinsi.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlPropinsi.SelectedIndex = 0
        ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
    End Sub

    Private Function GetCategoriForCustomer(ByVal code As String) As SPKCustomer
        Dim objSPKCust As SPKCustomer
        objSPKCust = New SPKCustomerFacade(User).Retrieve(code)
        Return objSPKCust
    End Function

    Private Sub FillForm()
        mode = ViewState("Mode")
        If mode = enumMode.Mode.NewItemMode Then
            'ClearForm()
            LoadForm()
        ElseIf mode = enumMode.Mode.EditMode Then
            LoadForm()
            ddlTipe.Enabled = False
            ddlTipePerusahaan.Enabled = False
        ElseIf mode = enumMode.Mode.ViewMode Then
            LoadFormView()
        End If
    End Sub

    Private Sub ClearForm()
        lblDealer.Text = objLoginDealer.DealerCode & "/" & objLoginDealer.SearchTerm1
        lblKodePelanggan.Text = ""  'Kapan di isi ???

        ddlTipe.SelectedIndex = 0
        txtNama.Text = ""
        txtGedung.Text = ""
        txtAlamat.Text = ""
        txtKelurahan.Text = ""
        txtKecamatan.Text = ""
        ddlPropinsi.SelectedValue = 0
        ddlKota.SelectedValue = 0

        txtRefKodePelanggan.Text = ""
        txtKodePos.Text = ""
        txtOffice.Text = ""
        txtHome.Text = ""
        txtHp.Text = ""
        txtEmail.Text = ""
        ddlCetak.SelectedIndex = 1
        'CR SPK
        txtCountryCode.Text = ""
        '
    End Sub

    Private Sub LoadForm()
        MakeVisibleLabel(False)
        objSPKCustomer = CType(sessionHelper.GetSession(ViewState.Item(Me._vstSPKCustomer)), SPKCustomer)
        'objSPKCustomer = CType(sessionHelper.GetSession(Request.QueryString("spkHeader")), SPKHeader).SPKCustomer
        If Not IsNothing(objSPKCustomer) Then
            ddlTipe.SelectedValue = objSPKCustomer.TipeCustomer
            ddlTipe_SelectedIndexChanged(Me, System.EventArgs.Empty)
            trUpload.Style.Clear()
            trUpload.Style.Add("Display", "none")
            If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan Then
                ddlTipePerusahaan.Visible = True
                Try
                    ddlTipePerusahaan.Items.Clear()
                    ddlTipePerusahaan.DataSource = New EnumTipePerusahaan().RetrieveType
                    ddlTipePerusahaan.DataTextField = "NameTipe"
                    ddlTipePerusahaan.DataValueField = "ValTipe"
                    ddlTipePerusahaan.DataBind()

                    ddlTipePerusahaan.SelectedValue = objSPKCustomer.TipePerusahaan
                Catch ex As Exception
                    ddlTipePerusahaan.Visible = False
                End Try

            ElseIf ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan Then
                trUpload.Style.Clear()
                trUpload.Style.Add("Display", "block")
                Dim ocr As OCRIdentity = objSPKCustomer.OCRIdentity
                lblAttachment.Text = objSPKCustomer.ImagePath
                If Not IsNothing(ocr) AndAlso ocr.ID > 0 Then

                    ViewState("UploadId") = ocr.ImageID
                    ddlIdentity.SelectedValue = ocr.Type.ToString()
                    sessionHelper.SetSession("OCR", ocr)
                End If
                If lblAttachment.Text <> "" Then
                    photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=0&file=" & lblAttachment.Text & "&type=" & "SPKCustomer"

                End If
                photoView.CssClass = "ShowControl"
            End If

            txtNama.Text = objSPKCustomer.Name1.ToUpper()
            txtNama2.Text = objSPKCustomer.Name2.ToUpper()
            txtGedung.Text = objSPKCustomer.Name3.ToUpper()
            txtAlamat.Text = objSPKCustomer.Alamat.ToUpper()
            txtKelurahan.Text = objSPKCustomer.Kelurahan.ToUpper()
            txtKecamatan.Text = objSPKCustomer.Kecamatan.ToUpper()

            ddlTipePerusahaan.SelectedValue = objSPKCustomer.TipePerusahaan
            'CR SPK
            ddlTipePerorangan.SelectedValue = objSPKCustomer.TypePerorangan
            txtCountryCode.Text = objSPKCustomer.CountryCode
            If objSPKCustomer.TypeIdentitas < 2 Then
                ddlIdentity.SelectedValue = objSPKCustomer.TypeIdentitas
                ddlIdentity.Visible = True
                ddlIdentityAsing.Visible = False
            ElseIf objSPKCustomer.TypeIdentitas > 1 Then
                ddlIdentityAsing.SelectedValue = objSPKCustomer.TypeIdentitas
                ddlIdentity.Visible = False
                ddlIdentityAsing.Visible = True
            End If
            '
            If objSPKCustomer.ID = 0 Then
                ddlTipePerusahaan_SelectedIndexChanged(Me, System.EventArgs.Empty)
            End If

            If Not IsNothing(objSPKCustomer.City) Then
                ddlPropinsi.SelectedValue = objSPKCustomer.City.Province.ID
                ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
                ddlKota.SelectedValue = objSPKCustomer.City.ID
            End If
            If objSPKCustomer.PreArea <> String.Empty And objSPKCustomer.PreArea <> "blank" Then
                Try
                    ddlPreArea.SelectedValue = objSPKCustomer.PreArea.Substring(0, 1).ToUpper + objSPKCustomer.PreArea.Substring(1).ToUpper
                Catch ex As Exception
                    ddlPreArea.SelectedIndex = 0
                End Try
            Else
                ddlPreArea.SelectedIndex = 0
            End If

            txtRefKodePelanggan.Text = objSPKCustomer.ReffCode.ToUpper
            txtKodePos.Text = objSPKCustomer.PostalCode
            If objSPKCustomer.PrintRegion <> "" Then
                ddlCetak.SelectedValue = CInt(objSPKCustomer.PrintRegion)
            End If
            txtOffice.Text = objSPKCustomer.OfficeNo
            txtHome.Text = objSPKCustomer.HomeNo
            txtHp.Text = objSPKCustomer.HpNo
            txtEmail.Text = objSPKCustomer.Email

            'start add by anh 20171024
            'objSPKHeader = sessionHelper.GetSession(Request.QueryString("spkHeader"))
            If Not IsNothing(objSPKHeader) AndAlso objSPKHeader.ID > 0 Then
                txtNama.Enabled = False
                txtHp.Enabled = False
                hdnNameEnabled.Value = "0"
                btnUplouadJS.Attributes.Add("disabled", "disabled")
            End If

            'end add by anh 20171024

            If objSPKHeader.Dealer.ID = objLoginDealer.ID Then
                If (Not (objSPKHeader.Status = EnumStatusSPK.Status.Selesai Or _
                            objSPKHeader.Status = EnumStatusSPK.Status.Batal) AndAlso objSPKHeader.CustomerRequestID = 0) Then
                    If DealerSystemCheck() Then
                        btnSave.Enabled = True
                    Else
                        btnSave.Enabled = False
                    End If
                Else
                    btnSave.Enabled = False
                End If

                'If objSPKHeader.ID > 0 Then
                '    btnSave.Enabled = False
                'Else
                '    btnSave.Enabled = True
                'End If
            Else
                btnSave.Enabled = False

            End If
            If btnSave.Enabled Then
                hdnEnabled.Value = "1"
            Else
                hdnEnabled.Value = "0"
            End If
        Else
            ClearForm()
        End If
    End Sub

    Private Function DealerSystemCheck() As Boolean
        Dim dDMS As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objLoginDealer.DealerCode)

        If dDMS.SystemID = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub LoadFormView()
        objSPKHeader = sessionHelper.GetSession(Request.QueryString("spkHeader"))
        MakeVisibleLabel(True)
        lblDealer.Text = objSPKHeader.Dealer.DealerCode & "/" & objSPKHeader.Dealer.SearchTerm1

        lblKodePelanggan.Text = objSPKHeader.SPKCustomer.Code
        lblDealer.Text = objSPKHeader.Dealer.DealerCode

        ddlTipe.Visible = False
        lblTipe.Text = CType(objSPKHeader.SPKCustomer.TipeCustomer, EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer).ToString
        If 1 = 2 Then
            ddlTipe.SelectedValue = CType(objSPKHeader.SPKCustomer.TipeCustomer, EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer).ToString
            ddlTipe_SelectedIndexChanged(Nothing, Nothing)
        End If
        If objSPKHeader.SPKCustomer.TipePerusahaan >= 0 Then
            Me.lblTipePerusahaan.Text = CType(objSPKHeader.SPKCustomer.TipePerusahaan, EnumTipePerusahaan.EnumTipePerusahaan).ToString
            lblTipePerusahaan.Visible = True
        End If
        trUpload.Style.Clear()
        trUpload.Style.Add("Display", "none")

        If CType(objSPKHeader.SPKCustomer.TipePerusahaan, EnumTipePerusahaan.EnumTipePerusahaan) = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perusahaan Then
            btnUplouadJS.Style.Add("Display", "none")
            trUpload.Style.Clear()
            trUpload.Style.Add("Display", "block")
            Dim ocr As OCRIdentity = objSPKHeader.SPKCustomer.OCRIdentity

            lblAttachment.Text = objSPKHeader.SPKCustomer.ImagePath
            If Not IsNothing(ocr) AndAlso ocr.ID > 0 Then
                ddlIdentity.SelectedValue = ocr.Type.ToString()

                ViewState("UploadId") = ocr.ImageID

                sessionHelper.SetSession("OCR", ocr)
                btnUpload.Enabled = False
            End If
            If lblAttachment.Text <> "" Then
                photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=0&file=" & lblAttachment.Text & "&type=" & "SPKCustomer"
            End If

            photoView.CssClass = "ShowControl"

        End If

        lblNama1.Text = objSPKHeader.SPKCustomer.Name1.Trim.ToUpper
        lblNama2.Text = objSPKHeader.SPKCustomer.Name2.Trim.ToUpper
        txtNama.Text = objSPKHeader.SPKCustomer.Name1.ToUpper()
        txtNama2.Text = objSPKHeader.SPKCustomer.Name2.ToUpper()
        txtNama.Visible = False
        txtNama2.Visible = False

        lblGedung.Text = objSPKHeader.SPKCustomer.Name3
        txtGedung.Visible = False

        lblAlamat.Text = objSPKHeader.SPKCustomer.Alamat
        txtAlamat.Text = objSPKHeader.SPKCustomer.Alamat.ToUpper()
        txtAlamat.Visible = False

        lblKelurahan.Text = objSPKHeader.SPKCustomer.Kelurahan
        txtKelurahan.Visible = False

        lblKecamatan.Text = objSPKHeader.SPKCustomer.Kecamatan
        txtKecamatan.Visible = False

        Dim objCity As City = New CityFacade(User).Retrieve(objSPKHeader.SPKCustomer.City.ID)

        lblPropinsi.Text = objCity.Province.ProvinceName
        ddlPropinsi.Visible = False

        ddlKota.Visible = False
        ddlPreArea.Visible = False
        If objSPKHeader.SPKCustomer.PreArea <> String.Empty And objSPKHeader.SPKCustomer.PreArea <> "blank" Then
            lblKota.Text = objSPKHeader.SPKCustomer.PreArea.ToUpper + " " + objCity.CityName
        Else
            lblKota.Text = objCity.CityName
        End If


        lblRefKodePlgn.Text = objSPKHeader.SPKCustomer.ReffCode
        txtRefKodePelanggan.Visible = False
        lbtnRefKode.Visible = False

        lblKodePos.Text = objSPKHeader.SPKCustomer.PostalCode
        txtKodePos.Visible = False

        lblOfficeNo.Text = objSPKHeader.SPKCustomer.OfficeNo
        txtOffice.Visible = False

        lblHomeNo.Text = objSPKHeader.SPKCustomer.HomeNo
        txtHome.Visible = False

        lblHpNo.Text = objSPKHeader.SPKCustomer.HpNo
        txtHp.Visible = False

        lblEmail.Text = objSPKHeader.SPKCustomer.Email
        txtEmail.Visible = False

        If CInt(objSPKCustomer.PrintRegion) = 0 Then
            lblCetak.Text = "Ya"
        Else
            lblCetak.Text = "Tidak"
        End If

        lblCetak.Visible = True
        ddlCetak.Visible = False
        lnkReloadPlg.Visible = False
        'btnSave.Enabled = False
        btnSave.Visible = False

    End Sub

    Private Sub MakeVisibleLabel(ByVal isVisible As Boolean)
        lblTipe.Visible = isVisible
        lblTipePerusahaan.Visible = isVisible
        lblRefKodePlgn.Visible = isVisible
        lblKodePelanggan.Visible = isVisible
        lblNama1.Visible = isVisible
        lblNama2.Visible = isVisible
        lblKodePos.Visible = isVisible
        lblKota.Visible = isVisible
        lblPropinsi.Visible = isVisible
        lblKecamatan.Visible = isVisible
        lblKelurahan.Visible = isVisible
        lblCetak.Visible = isVisible
        lblAlamat.Visible = isVisible
        lblGedung.Visible = isVisible
        lblOfficeNo.Visible = isVisible
        lblHomeNo.Visible = isVisible
        lblHpNo.Visible = isVisible
        lblEmail.Visible = isVisible

    End Sub

    Private Function ValidateSave() As Boolean
        hdnMCPConfirmation.Value = "-1"
        hdnLKPPConfirmation.Value = "-1"
        Dim _return As Boolean = True
        If ddlPropinsi.SelectedValue = "0" Then
            _return = False
            MessageBox.Show("Silahkan pilih Propinsi")
        End If

        If ddlKota.SelectedValue = "0" Then
            _return = False
            MessageBox.Show("Silahkan pilih Kota")
        End If

        Dim spkS As New SPKCustomer

        Dim NeedKTP As Boolean = True
        Try
            If Not sessionHelper.GetSession(ViewState.Item(Me._vstSPKCustomer)) Is Nothing Then
                spkS = New SPKCustomer

                spkS = sessionHelper.GetSession(ViewState.Item(Me._vstSPKCustomer))
                If spkS.ID > 0 Then
                    NeedKTP = False
                End If
            End If
        Catch ex As Exception

        End Try

        If ddlTipe.SelectedValue = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan AndAlso NeedKTP Then
            If lblAttachment.Text = "" Then
                _return = False
                MessageBox.Show("Silahkan Upload Identitas")
            End If
        End If

        If txtKodePos.Text.Trim() <> String.Empty Then
            If Not IsNumeric(txtKodePos.Text) Then
                _return = False
            End If
            If Not (txtKodePos.Text.Trim.Length = 5) Then
                _return = False
            End If
            If _return = False Then
                MessageBox.Show("Kode Pos tidak valid")
            End If
        End If

        If txtAlamat.Text.Trim = String.Empty Then
            _return = False
        End If


        Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        If txtRefKodePelanggan.Text.Trim <> String.Empty Then
            Dim objCustomer As New Customer
            objCustomer = New CustomerDealerFacade(User).RetrieveByRefCode(txtRefKodePelanggan.Text.Trim, objDealer.ID)
            If objCustomer.ID <= 0 Then
                _return = False
            End If
        End If

        'If txtNomerID.Text.Trim = String.Empty Then
        '    _return = False
        'End If
        Dim city As KTB.DNet.Domain.City = New CityFacade(User).Retrieve(CType(ddlKota.SelectedValue, Integer))
        Dim area As String = String.Empty
        area = ddlPreArea.SelectedValue.ToUpper
        If Not IsNothing(city) Then
            Dim msg As String = ""
            Select Case Len(city.CityName)
                Case Is > 31
                    If ddlPreArea.SelectedIndex > 0 Then
                        msg = "Kosongkan (Kota / Kabupaten)"
                    End If
                Case Is = 30
                    If ddlPreArea.SelectedIndex > 0 AndAlso Len(area) > 4 Then
                        msg = "Gunakan KAB / KOTA atau kosongkan (Kota / Kabupaten)"
                    End If
                Case Is = 31
                    If ddlPreArea.SelectedIndex > 0 AndAlso Len(area) > 3 Then
                        msg = "Gunakan KAB atau kosongkan (Kota / Kabupaten)"
                    End If
                Case Is < 30
                    If ddlPreArea.SelectedIndex > 0 AndAlso Len(area) > 5 Then
                        If Len(city.CityName) > 25 Then
                            msg = "Gunakan KAB / KOTA / KODYA"
                        End If
                    End If
            End Select
            If msg <> "" Then
                MessageBox.Show(msg)
                _return = False
            End If
        End If
        Return _return

    End Function

    Private Function ValidateRefCustomerCode() As Boolean
        Dim refKode As String = txtRefKodePelanggan.Text
        Dim objCustomer As Customer = New CustomerFacade(User).RetrieveCustReq(refKode.Trim)
        If objCustomer Is Nothing Then
            MessageBox.Show("Ref Kode Pelanggan tidak valid")
            Return False
        ElseIf objCustomer.RowStatus = -1 Then
            MessageBox.Show("Ref Kode Pelanggan tidak valid")
            Return False
        Else
            Return True
        End If
    End Function

    Private Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function

    'cr spk format type perusahaan
    Private Function IdentitasCheck(ByVal identitas As String) As Boolean
        Dim pattern As String = "[a-zA-Z0-9]+[a-zA-Z0-9 ]+"
        Dim IdentitasMatch As Match = Regex.Match(identitas, pattern)
        If IdentitasMatch.Success Then
            IdentitasCheck = True
        Else
            IdentitasCheck = False
        End If
    End Function
    Private Function AngkaCheck(ByVal angka As String) As Boolean
        Dim pattern As String = "1234567890"
        Dim IdentitasMatch As Match = Regex.Match(angka, pattern)
        If IdentitasMatch.Success Then
            AngkaCheck = True
        Else
            AngkaCheck = False
        End If
    End Function
    'end
    '

#Region "panel nomor ID"
    'Private Sub BindNomerID()
    '    Dim oPG As ProfileGroup
    '    Dim aPHTG As ArrayList

    '    Me.txtNomerID.Text = ""
    '    If ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perorangan Then
    '        oPG = New ProfileGroupFacade(User).Retrieve("cust_dbs_2")
    '    ElseIf ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perusahaan Then
    '        oPG = New ProfileGroupFacade(User).Retrieve("cust_dbs_3")
    '    ElseIf ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.BUMN_Pemerintah Then
    '        oPG = New ProfileGroupFacade(User).Retrieve("cust_dbs_4")
    '    ElseIf ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Lainnya Then
    '        oPG = New ProfileGroupFacade(User).Retrieve("cust_dbs_5")
    '    End If
    '    aPHTG = Me.GetNomerIDProfileHeaders(oPG.ID)
    '    Me.ddlNomerID.Items.Clear()
    '    For Each oPHTG As ProfileHeaderToGroup In aPHTG
    '        Me.ddlNomerID.Items.Add(New ListItem(oPHTG.ProfileHeader.Description, oPHTG.ID))
    '    Next
    'End Sub

    Private Function GetNomerIDProfileHeaders(ByVal PGID As Integer) As ArrayList
        Dim cPHTG As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim oPHTGFac As ProfileHeaderToGroupFacade = New ProfileHeaderToGroupFacade(User)
        Dim aPHTG As ArrayList
        Dim Sql As String

        Sql = "select count(*) from ProfileHeader ph where ph.ID=ProfileHeaderToGroup.ProfileHeaderID and ph.Code in ('NOKTP','NONPWP','NOSIUP','NOTDP')"

        cPHTG.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, PGID))
        cPHTG.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.No, "(" & Sql & ")"))
        aPHTG = oPHTGFac.Retrieve(cPHTG)
        Return aPHTG
    End Function

    'Private Sub SetNomerIDValue()
    '    Me.FillPanelAuto(pnlPerorangan, True)
    '    Me.FillPanelAuto(pnlPerusahaan, True)
    '    Me.FillPanelAuto(PnlBUMN, True)
    '    Me.FillPanelAuto(PnlLainnya, True)
    '    Me.FillPanelAuto(GetPanel, False)
    'End Sub

    'Private Sub FillPanelAuto(ByRef pnl As Panel, ByVal IsSetToEmpty As Boolean)
    '    If ddlNomerID.Items.Count > 0 Then
    '        Dim oPHTG As ProfileHeaderToGroup = New ProfileHeaderToGroupFacade(User).Retrieve(CType(Me.ddlNomerID.SelectedValue, Integer))
    '        Dim sCode As String = oPHTG.ProfileHeader.Description
    '        Dim lblCode As String
    '        Dim i As Integer

    '        For Each oC As Object In pnl.Controls
    '            If TypeOf oC Is Label Then
    '                lblCode = CType(oC, Label).Text.Trim.ToUpper
    '                Select Case lblCode
    '                    Case "NO KTP", "NO NPWP", "NO SIUP", "NO TDP"
    '                        If IsSetToEmpty Then
    '                            CType(pnl.Controls(i + 3), TextBox).Text = ""
    '                        Else
    '                            CType(pnl.Controls(i + 3), TextBox).Text = IIf(sCode = lblCode, Me.txtNomerID.Text, "")
    '                        End If
    '                End Select
    '            End If
    '            i += 1
    '        Next
    '    End If
    'End Sub

    Private Function GetPanel() As Panel
        If ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perorangan Then
            Return Me.pnlPerorangan
        ElseIf ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perusahaan Then
            Return Me.pnlPerusahaan
        ElseIf ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.BUMN_Pemerintah Then
            Return Me.PnlBUMN
        ElseIf ddlTipe.SelectedValue = EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Lainnya Then
            Return Me.PnlLainnya
        End If
    End Function

    'Private Sub BindSPKCustomerID()
    '    'If Request.QueryString("Id") = String.Empty Then Exit Sub
    '    objSPKCustomer = CType(sessionHelper.GetSession(viewstate.Item(Me._vstSPKCustomer)), SPKCustomer)
    '    If objSPKCustomer Is Nothing Then Exit Sub

    '    Dim pgCode As String
    '    Dim sqlPH As String
    '    Dim sqlPG As String
    '    Dim cCRP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKCustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    Dim aCRP As ArrayList

    '    objSPKCustomer = sessionHelper.GetSession(viewstate.Item(Me._vstSPKCustomer))
    '    Me.ddlTipe.SelectedValue = objSPKCustomer.TipeCustomer
    '    ddlTipe_SelectedIndexChanged(Nothing, Nothing)
    '    cCRP.opAnd(New Criteria(GetType(SPKCustomerProfile), "SPKCustomer.ID", MatchType.Exact, objSPKCustomer.ID))
    '    Select Case objSPKCustomer.Status
    '        Case EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perorangan
    '            pgCode = "cust_dbs_2"
    '        Case EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Perusahaan
    '            pgCode = "cust_dbs_3"
    '        Case EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.BUMN_Pemerintah
    '            pgCode = "cust_dbs_4"
    '        Case EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer.Lainnya
    '            pgCode = "cust_dbs_5"
    '    End Select
    '    sqlPH = "select ph.ID from ProfileHeader ph where ph.Code in ('NOKTP','NONPWP','NOSIUP','NOTDP')"
    '    sqlPG = "select pg.ID from ProfileGroup pg where pg.Code='" & pgCode & "'"
    '    cCRP.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileHeader.ID", MatchType.InSet, "(" & sqlPH & ")"))
    '    cCRP.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileGroup.ID", MatchType.InSet, "(" & sqlPG & ")"))
    '    'cCRP.opAnd(New Criteria(GetType(SPKCustomerProfile), "ProfileGroup.ID", MatchType.InSet, "(" & sqlPG & ")"))
    '    aCRP = New SPKCustomerProfileFacade(User).Retrieve(cCRP)
    '    If aCRP.Count > 0 Then
    '        Dim oCRP As SPKCustomerProfile = CType(aCRP(0), SPKCustomerProfile)
    '        For Each oCRPTemp As SPKCustomerProfile In aCRP
    '            If oCRPTemp.ProfileValue.Trim <> "" Then
    '                oCRP = oCRPTemp
    '                Exit For
    '            End If
    '        Next
    '        Me.txtNomerID.Text = oCRP.ProfileValue

    '        Dim cPHTG As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '        Dim aPHTG As ArrayList

    '        cPHTG.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileHeader.ID", MatchType.Exact, oCRP.ProfileHeader.ID))
    '        cPHTG.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, oCRP.ProfileGroup.ID))
    '        aPHTG = New ProfileHeaderToGroupFacade(User).Retrieve(cPHTG)
    '        If aPHTG.Count > 0 Then
    '            Me.ddlNomerID.SelectedValue = CType(aPHTG(0), ProfileHeaderToGroup).ID 
    '        End If
    '    End If
    'End Sub
#End Region

    Private Function IsMCPValid(ByVal IsAfterConfirmation As Boolean) As Boolean
        Dim IsCV As Boolean = False
        Dim IsPcLcv As Boolean = False
        Dim oSPKH As SPKHeader = sessionHelper.GetSession(Request.QueryString("spkHeader"))

        Try
            If Not IsNothing(oSPKH) Then

                Me.hdnVerifyMCP.Value = EnumMCPStatus.MCPStatus.NonMCP
                Me.hdnVerifyLKPP.Value = EnumLKPPStatus.LKPPStatus.NonLKPP

                If oSPKH.SPKDetails.Count > 0 Then
                    For Each oSPKD As SPKDetail In oSPKH.SPKDetails
                        If oSPKD.Category.CategoryCode = "CV" Then
                            IsCV = True
                            Exit For
                        End If
                    Next
                End If
                If IsCV Then
                    _isMMC = False
                    If CType(Me.ddlTipe.SelectedValue, Integer) = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
                        If IsAfterConfirmation = False Then
                            If 1 = 1 Then 'OrElse (IsGovernmentInstitution(Me.txtNama.Text) OrElse IsGovernmentInstitution(Me.txtNama2.Text)) Then
                                MessageBox.Confirm("Konsumen terdeteksi MCP, Apakah proses pengajuan tetap dilanjutkan?", "hdnMCPConfirmation")
                                Return False
                            Else
                                Return True
                            End If
                        Else
                            If 1 = 1 Then 'OrElse (IsGovernmentInstitution(Me.txtNama.Text) OrElse IsGovernmentInstitution(Me.txtNama2.Text)) Then
                                If Me.hdnMCPConfirmation.Value <> "1" Then 'User click No in confirmation box
                                    Return False
                                Else
                                    hdnVerifyMCP.Value = EnumMCPStatus.MCPStatus.NotVerifiedMCP
                                    Return True 'user click ok in confirmation box
                                End If
                            Else
                                Return True 'never
                            End If
                        End If
                    Else
                        Return True
                    End If
                Else
                    _isMMC = True
                    Return True
                End If
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function


    Private Function IsLKPPValid(ByVal IsAfterConfirmation As Boolean) As Boolean
        Dim IsPcLcv As Boolean = False

        Dim oSPKH As SPKHeader = sessionHelper.GetSession(Request.QueryString("spkHeader"))

        Try
            Me.hdnVerifyLKPP.Value = EnumLKPPStatus.LKPPStatus.NonLKPP 'EnumMCPStatus.MCPStatus.NonMCP

            If Not IsNothing(oSPKH) Then
                For Each oSPKD As SPKDetail In oSPKH.SPKDetails
                    If oSPKD.Category.CategoryCode = "PC" OrElse oSPKD.Category.CategoryCode = "LCV" Then
                        IsPcLcv = True
                        Exit For
                    End If
                Next
            End If

            If IsPcLcv Then
                If CType(Me.ddlTipe.SelectedValue, Integer) = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
                    If IsAfterConfirmation = False Then
                        If 1 = 1 Then 'OrElse (IsGovernmentInstitution(Me.txtNama.Text) OrElse IsGovernmentInstitution(Me.txtNama2.Text)) Then
                            MessageBox.Confirm("Konsumen terdeteksi LKPP, Apakah proses pengajuan tetap dilanjutkan?", "hdnLKPPConfirmation")
                            Return False
                        Else
                            Return True
                        End If
                    Else
                        If 1 = 1 Then 'OrElse (IsGovernmentInstitution(Me.txtNama.Text) OrElse IsGovernmentInstitution(Me.txtNama2.Text)) Then
                            If Me.hdnLKPPConfirmation.Value <> "1" Then 'User click No in confirmation box
                                Return False
                            Else
                                hdnVerifyLKPP.Value = EnumLKPPStatus.LKPPStatus.NotVerifiedLKPP
                                Return True 'user click ok in confirmation box
                            End If
                        Else
                            Return True 'never
                        End If
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function IsGovernmentInstitution(ByVal sName As String) As Boolean
        Dim sMCPList() As String = KTB.DNet.Lib.WebConfig.GetValue("ListOfMCPName").Split(";")
        Dim i As Integer
        Dim sTemp As String = ""
        If sName.Trim() = String.Empty Then
            Return False
        End If
        For i = 0 To sMCPList.Length - 1
            sTemp = sMCPList(i).Trim
            If sTemp.IndexOf("*") >= 0 Then
                If sTemp.Split("*").Length > 2 Then

                    Dim dtValue As DataTable = New DataTable
                    Dim Wc() As String = sTemp.Split("*")

                    Dim Filter As String = String.Empty

                    dtValue.Columns.Add(New DataColumn("Value"))
                    Dim dr As DataRow = dtValue.NewRow()
                    dr(0) = sName
                    dtValue.Rows.Add(dr)

                    For F As Integer = 0 To Wc.Length - 1
                        If Wc(F) <> "" Then
                            If F = 0 Then
                                Filter = Filter & "Value LIKE '%" & Wc(F) + "%' "
                            Else
                                If Filter <> "" Then
                                    Filter = Filter & " AND Value LIKE '%" & Wc(F) & "%' "
                                Else
                                    Filter = Filter & "Value LIKE '%" & Wc(F) + "%' "
                                End If


                            End If

                        End If
                    Next
                    Dim drs As DataRow() = dtValue.Select(Filter)
                    If Not IsNothing(drs) AndAlso drs.Length > 0 Then Return True


                ElseIf sTemp.StartsWith("*") Then
                    If sName.EndsWith(sTemp.Replace("*", "")) Then Return True
                ElseIf sTemp.EndsWith("*") Then
                    If sName.StartsWith(sTemp.Replace("*", "")) Then Return True
                Else
                    If sName.StartsWith(sTemp.Substring(0, sTemp.IndexOf("*"))) _
                        AndAlso sName.EndsWith(sTemp.Substring(sTemp.IndexOf("*") + 1)) Then
                        Return True
                    End If
                End If
            Else
                If sName = sTemp Then Return True
            End If
        Next

        Return False
    End Function

#End Region

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            UploadFile()
        Catch ex As Exception
            sessionHelper.SetSession("OCr", Nothing)
            hdnUpload.Value = "0"
            hdnGetApi.Value = ""
        End Try

    End Sub


    Private Sub RetrieveOCR()

        Dim Ocrd As New OCRIdentity



        Dim myclient As New WebClient
        myclient.Headers.Add(HttpRequestHeader.Accept, "application\json")
        If KTB.DNet.Lib.WebConfig.GetValue("ProxyAddress") <> "" Then
            Dim wp = New WebProxy(KTB.DNet.Lib.WebConfig.GetValue("ProxyAddress"), CInt(KTB.DNet.Lib.WebConfig.GetValue("ProxyPort")))
            wp.BypassProxyOnLocal = True
            myclient.Proxy = wp
        End If


        Dim UploadId As String = ViewState("UploadId")
        Ocrd.ImageID = ViewState("UploadId")
        Ocrd.ImagePath = lblAttachment.Text
        Ocrd.Type = CInt(ddlIdentity.SelectedValue)

        Dim UriData As String = GetApiURL(OCRApi.Data, CType(ddlIdentity.SelectedValue, IdentityType))
        UriData = String.Format(UriData, ViewState("UploadId").ToString())
        myclient.Headers.Add(HttpRequestHeader.Accept, "application\json")
        myclient.Headers.Add(HttpRequestHeader.ContentType, "text/plain")
        'Dim data As String = myclient.DownloadString("https://api.datareader.online/v2/Ktp/e32ef8f4-54bc-43fd-84ce-eacc78f3b43x/data")
        Dim strJson As String = myclient.DownloadString(UriData)

        '
        Dim jResultKTP As Object = JsonConvert.DeserializeObject(Of Object)(strJson)
        sessionHelper.SetSession("OCr", Nothing)
        If Not IsNothing(jResultKTP("Errors")) AndAlso jResultKTP("Errors").ToString() = "" Then
            txtJSon.Text = txtJSon.Text & strJson
            SetData(CType(ddlIdentity.SelectedValue, IdentityType), strJson, Ocrd)
            sessionHelper.SetSession("OCr", Ocrd)
            hdnUpload.Value = "0"
            hdnGetApi.Value = ""
            MessageBox.Show("Upload Sukses")

        ElseIf Not IsNothing(jResultKTP("Errors")) AndAlso jResultKTP("Errors").ToString().ToLower().Contains("progress") Then
            hdnUpload.Value = "1"
            hdnGetApi.Value = "0"
        ElseIf 1 = 1 Then
            Dim str As String = "Gambar tidak teridentifikasi. Silahkan upload kembali dengan resolusi dan kualitas yang lebih bagus"
            hdnUpload.Value = "0"
            hdnGetApi.Value = ""

            txtJSon.Text = txtJSon.Text & strJson
            MessageBox.Show(str)
        Else
            hdnUpload.Value = "0"
            hdnGetApi.Value = ""
            txtJSon.Text = txtJSon.Text & strJson
        End If

        If IsNothing(ViewState("uploadcount")) Then
            ViewState("uploadcount") = 0
        Else
            ViewState("uploadcount") = CInt(ViewState("uploadcount")) + 1
        End If

        If CInt(ViewState("uploadcount")) = 2 Then
            hdnUpload.Value = "0"
            hdnGetApi.Value = ""
        End If

    End Sub

    Private Sub UploadFile()

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim EnableOCR As Boolean = CBool(KTB.DNet.Lib.WebConfig.GetValue("OCROn"))
        objLoginDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)

        Dim UniqueName As String = objLoginDealer.DealerCode & "_" & DateTime.Now.ToString("yyyyMMddHHmmss") & Guid.NewGuid().ToString().Substring(0, 3)

        Try
            If (Not Me.fileUpload.PostedFile Is Nothing) And (Me.fileUpload.PostedFile.ContentLength > 0) Then

                If Me.fileUpload.PostedFile.ContentLength > 1024000 Then
                    hdnUpload.Value = "0"
                    hdnGetApi.Value = ""
                    MessageBox.Show(" (Ukuran maksimum 1 Mb)")
                    Return
                End If

                Dim ext As String = System.IO.Path.GetExtension(Me.fileUpload.PostedFile.FileName)
                If Not (ext.ToUpper() = ".JPG" OrElse ext.ToUpper() = ".JPEG" OrElse ext.ToUpper() = ".PNG") Then
                    hdnUpload.Value = "0"
                    hdnGetApi.Value = ""
                    MessageBox.Show("Hanya menerima file format Gambar")
                    Return
                End If
                If ext.ToUpper() = ".JPEG" Then
                    ext = ".jpg"
                End If

                If imp.Start() Then
                    Dim NewFileLocation As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload & DateTime.Now.Year.ToString() & "\" & UniqueName
                    Dim strFileName As String = Path.GetFileName(Me.fileUpload.PostedFile.FileName)
                    NewFileLocation = NewFileLocation & ext
                    If Not IO.Directory.Exists(NewFileLocation) Then
                        IO.Directory.CreateDirectory(Path.GetDirectoryName(NewFileLocation))
                    End If


                    If IO.File.Exists(NewFileLocation) Then
                        IO.File.Delete(Path.GetDirectoryName(NewFileLocation))
                    End If



                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(Me.fileUpload.PostedFile.InputStream, NewFileLocation)

                    lblAttachment.Text = DateTime.Now.Year.ToString() & "\" & UniqueName & ext
                    photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=0&file=" & lblAttachment.Text & "&type=" & "SPKCustomer"
                    photoView.CssClass = "ShowControl"


                    '''Upload to API
                    '''

                    Dim myclient As New WebClient
                    myclient.Headers.Add(HttpRequestHeader.Accept, "application\json")

                    If EnableOCR = False Then
                        hdnUpload.Value = "0"
                        hdnGetApi.Value = ""
                        sessionHelper.SetSession("OCr", Nothing)
                        MessageBox.Show(SR.UploadSucces(strFileName))
                        Return
                    End If

                    If KTB.DNet.Lib.WebConfig.GetValue("ProxyAddress") <> "" Then
                        Dim wp = New WebProxy(KTB.DNet.Lib.WebConfig.GetValue("ProxyAddress"), CInt(KTB.DNet.Lib.WebConfig.GetValue("ProxyPort")))
                        wp.BypassProxyOnLocal = True
                        myclient.Proxy = wp
                    End If
                    Dim myres As Byte() = myclient.UploadFile(GetApiURL(OCRApi.Upload, CType(ddlIdentity.SelectedValue, IdentityType)), NewFileLocation)
                    Dim JResultUpload As String = System.Text.Encoding.ASCII.GetString(myres)

                    txtJSon.Text = ""
                    txtJSon.Text = JResultUpload & vbCrLf & " || " & vbCrLf

                    Dim jResult As Object = JsonConvert.DeserializeObject(Of Object)(JResultUpload)


                    '''Result of Upload to API
                    If (jResult("success").ToString().ToLower().Equals("true")) Then


                        Dim jResultData As Object = JsonConvert.DeserializeObject(Of Object)(jResult("data").ToString())
                        Dim UploadId As String = jResultData("UploadId").ToString()
                        ViewState("UploadId") = UploadId


                    Else
                        sessionHelper.SetSession("OCr", Nothing)
                        hdnUpload.Value = "0"
                        hdnGetApi.Value = ""
                        MessageBox.Show("Repository " & SR.UploadSucces(strFileName))
                        Return
                    End If

                    hdnGetApi.Value = "0"


                    imp.StopImpersonate()
                    imp = Nothing
                Else
                    hdnUpload.Value = "0"
                    hdnGetApi.Value = ""
                End If


            Else
                hdnUpload.Value = "0"
                hdnGetApi.Value = ""
                MessageBox.Show("Tidak Ada File yang dipilih")
            End If
        Catch ex As Exception
            hdnUpload.Value = "0"
            hdnGetApi.Value = ""
            MessageBox.Show(SR.UploadFail(ex.Message.ToString()))
        End Try
    End Sub

    Private Sub SetData(ByVal OCRIdentityType As IdentityType, ByVal strJson As String, ByRef Ocrd As OCRIdentity)

        Ocrd.JSon = strJson

        Ocrd.Type = CInt(OCRIdentityType).ToString()
        Try
            Dim jResultKTP As Object = JsonConvert.DeserializeObject(Of Object)(strJson)
            Ocrd.BirthOfDate = IIf(Not IsNothing(jResultKTP("TanggalLahir")) AndAlso jResultKTP("TanggalLahir").ToString().ToLower() <> "null", jResultKTP("TanggalLahir").ToString(), "").ToString().Trim()
            Ocrd.BirthOfPlace = IIf(Not IsNothing(jResultKTP("TempatLahir")) AndAlso jResultKTP("TempatLahir").ToString().ToLower() <> "null", jResultKTP("TempatLahir").ToString(), "").ToString().Trim()
            Ocrd.ProcessingTime = CDbl(IIf(Not IsNothing(jResultKTP("ProcessingTime")) AndAlso jResultKTP("TempatLahir").ToString().ToLower() <> "null", jResultKTP("ProcessingTime").ToString(), "0"))

            If OCRIdentityType = IdentityType.KTP Then

                txtNama.Text = IIf(Not IsNothing(jResultKTP("Nama")) AndAlso jResultKTP("Nama").ToString().ToLower() <> "null", jResultKTP("Nama").ToString(), "").ToString().Trim()
                Ocrd.Name = txtNama.Text
                txtAlamat.Text = IIf(Not IsNothing(jResultKTP("Alamat")) AndAlso jResultKTP("Alamat").ToString().ToLower() <> "null", jResultKTP("Alamat").ToString(), "").ToString().Trim()
                Ocrd.Address = txtAlamat.Text
                txtKelurahan.Text = IIf(Not IsNothing(jResultKTP("Kelurahan")) AndAlso jResultKTP("Kelurahan").ToString().ToLower() <> "null", jResultKTP("Kelurahan").ToString(), "").ToString().Trim()
                Ocrd.Subdistrict = txtKelurahan.Text
                txtKecamatan.Text = IIf(Not IsNothing(jResultKTP("Kecamatan")) AndAlso jResultKTP("Kecamatan").ToString().ToLower() <> "null", jResultKTP("Kecamatan").ToString(), "").ToString().Trim()
                Ocrd.District = txtKecamatan.Text
                Dim Prov As String = If(Not IsNothing(jResultKTP("Propinsi")) AndAlso jResultKTP("Propinsi").ToString().ToLower() <> "null", jResultKTP("Propinsi").ToString(), "").ToString().Trim()

                ddlPropinsi.SelectedValue = "0"
                Me.ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
                Try
                    ddlKota.SelectedValue = "0"
                Catch ex As Exception

                End Try


                Dim Cit As String = If(Not IsNothing(jResultKTP("Kotakab")) AndAlso jResultKTP("Kotakab").ToString().ToLower() <> "null", jResultKTP("Kotakab").ToString(), "")
                Ocrd.Province = Prov
                Ocrd.Regency = Cit
                Try

                    If Prov <> "" Then
                        ddlPropinsi.SelectedValue = GetProvinceID(Prov).ToString()

                        Me.ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)




                        If (ddlPropinsi.SelectedValue.ToString <> "0") Then
                            ddlKota.SelectedValue = GetCityID(Cit, CInt(ddlPropinsi.SelectedValue)).ToString()
                        End If
                    Else
                        ddlPropinsi.SelectedValue = "0"
                        Me.ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
                    End If
                Catch ex As Exception

                End Try



                Dim txtKTP As TextBox = Me.FindControl("TEXTBOX1_1")

                Dim NIK As String = If(Not IsNothing(jResultKTP("NIK")) AndAlso jResultKTP("NIK").ToString().ToLower() <> "null", jResultKTP("NIK").ToString(), "").Trim()
                Ocrd.IdentityNumber = NIK
                If Not IsNothing(txtKTP) Then
                    txtKTP.Text = NIK
                End If

                Dim ddlGender As DropDownList = Me.FindControl("DDLIST4_1")


                Dim Gender As String = If(Not IsNothing(jResultKTP("JenisKelamin")) AndAlso jResultKTP("JenisKelamin").ToString().ToLower() <> "null", jResultKTP("JenisKelamin").ToString(), "").Trim()
                Ocrd.Gender = Gender

                If Not IsNothing(ddlGender) Then

                    Dim gvalue As String = ""
                    If Gender.ToLower().Contains("la") OrElse Gender.ToLower().Contains("pria") Then
                        ddlGender.SelectedValue = "LK"
                    End If

                    If Gender.ToLower().Contains("puan") OrElse Gender.ToLower().Contains("wa") Then
                        ddlGender.SelectedValue = "PR"
                    End If
                End If

            Else


                txtNama.Text = IIf(Not IsNothing(jResultKTP("Nama")) AndAlso jResultKTP("Nama").ToString().ToLower() <> "null", jResultKTP("Nama").ToString(), "").ToString().Trim()
                Ocrd.Name = txtNama.Text

                Dim strAlamat As String = IIf(Not IsNothing(jResultKTP("Alamat")) AndAlso jResultKTP("Alamat").ToString().ToLower() <> "null", jResultKTP("Alamat").ToString(), "").ToString().Trim()
                Ocrd.Address = strAlamat


                Try
                    If strAlamat <> "" Then
                        strAlamat = strAlamat.Split(New String() {Environment.NewLine},
                                       StringSplitOptions.None)(0)
                    End If
                Catch ex As Exception

                End Try


                txtAlamat.Text = strAlamat


                Dim txtKTP As TextBox = Me.FindControl("TEXTBOX1_1")

                Dim NIK As String = If(Not IsNothing(jResultKTP("NomorSim")) AndAlso jResultKTP("NomorSim").ToString().ToLower() <> "null", jResultKTP("NomorSim").ToString(), "").Trim()
                Ocrd.IdentityNumber = NIK
                If Not IsNothing(txtKTP) Then
                    txtKTP.Text = NIK
                End If



                Dim Gender As String = If(Not IsNothing(jResultKTP("JenisKelamin")) AndAlso jResultKTP("JenisKelamin").ToString().ToLower() <> "null", jResultKTP("JenisKelamin").ToString(), "")
                Ocrd.Gender = Gender
                Dim ddlGender As DropDownList = Me.FindControl("DDLIST4_1")

                If Not IsNothing(ddlGender) Then

                    Dim gvalue As String = ""
                    If Gender.ToLower().Contains("la") OrElse Gender.ToLower().Contains("pria") Then
                        ddlGender.SelectedValue = "LK"
                    End If

                    If Gender.ToLower().Contains("puan") OrElse Gender.ToLower().Contains("wa") Then
                        ddlGender.SelectedValue = "PR"
                    End If
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function GetProvinceID(ByVal ProvinceName As String) As Integer
        Dim opProv As New Province

        Dim opProF As New ProvinceFacade(User)
        opProv = opProF.RetrieveByName(ProvinceName)

        Return opProv.ID

        Return 0
    End Function

    Private Function GetCityID(ByVal ProvinceName As String, ByVal ProvinceID As Integer) As Integer
        Dim opProv As New City

        Dim opProF As New CityFacade(User)
        opProv = opProF.RetrieveByName(ProvinceName, ProvinceID)

        Return opProv.ID

        Return 0
    End Function

    Private Function GetApiURL(ByVal OCRPath As OCRApi, ByVal OCRIdentityType As IdentityType) As String



        If OCRPath = OCRApi.Upload Then
            If OCRIdentityType = IdentityType.KTP Then
                Return KTB.DNet.Lib.WebConfig.GetValue("OCRUrlKTPUpload")
            Else
                Return KTB.DNet.Lib.WebConfig.GetValue("OCRUrlSIMUpload")
            End If
        Else
            If OCRIdentityType = IdentityType.KTP Then
                Return KTB.DNet.Lib.WebConfig.GetValue("OCRUrlKTPApi")
            Else
                Return KTB.DNet.Lib.WebConfig.GetValue("OCRUrlSIMApi")
            End If
        End If



        Return ""
    End Function

    Public Enum IdentityType
        KTP
        SIM
    End Enum

    Public Enum OCRApi
        Upload
        Data
    End Enum

    Protected Sub btnGetApi_Click(sender As Object, e As EventArgs) Handles btnGetApi.Click
        Try
            RetrieveOCR()
        Catch ex As Exception
            hdnGetApi.Value = ""
            hdnUpload.Value = "0"
            MessageBox.Show("Upload Gagal")
        End Try

    End Sub
End Class