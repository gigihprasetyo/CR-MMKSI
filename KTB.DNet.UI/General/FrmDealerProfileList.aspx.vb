Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.Configuration


Public Class FrmDealerProfileList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblAddress As System.Web.UI.WebControls.Label
    Protected WithEvents lblCityName As System.Web.UI.WebControls.Label
    Protected WithEvents lblAreaCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblGroupName As System.Web.UI.WebControls.Label
    Protected WithEvents lblPhone As System.Web.UI.WebControls.Label
    Protected WithEvents lblFax As System.Web.UI.WebControls.Label
    Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerClass As System.Web.UI.WebControls.Label
    Protected WithEvents lblHeldYear As System.Web.UI.WebControls.Label
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents Image2 As System.Web.UI.WebControls.Image
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents PnlManajemen As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents pnlSlideShow As System.Web.UI.WebControls.Panel
    Protected WithEvents hlAudit As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hlOrganisasi As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hlSalesForce As System.Web.UI.WebControls.HyperLink
    Protected WithEvents lbtnShowroomAudit As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnStrukturOrganisasi As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnSalesForce As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblStatusAdd As System.Web.UI.WebControls.Label

    ' base on profilGroup table
    Private intProfileGroup_Dealer As Integer = 18

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        If Request.QueryString("DealerID") = "" Then
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("dealer_prf"), EnumProfileType.ProfileType.DEALER, PnlManajemen)
        Else
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CType(Request.QueryString("DealerID"), Integer))
            RenderProfilePanel(objDealer, New ProfileGroupFacade(User).Retrieve("dealer_prf"), EnumProfileType.ProfileType.DEALER, PnlManajemen)
        End If
    End Sub

#End Region

#Region "PrivateVariables"
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper

#End Region

#Region "PrivateCustomMethods"

    Private Sub GenerateHyperlink()
        Dim dealerID As Integer = CInt(Request.QueryString("DealerID"))

        Dim arlDealerAdditional As ArrayList = New DealerAdditionalFacade(User).RetrieveByDealerID(dealerID)
        If arlDealerAdditional.Count > 0 Then
            Dim objDealerAdditional As DealerAdditional = arlDealerAdditional(0)
            'lbtnSalesForce.Enabled = False
            'lbtnShowroomAudit.Enabled = False
            'lbtnStrukturOrganisasi.Enabled = False
            'sessHelper.SetSession("objDealerAdditional", objDealerAdditional)
            lbtnShowroomAudit.CommandArgument = "../Download.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("UploadDealerProfile") & objdealeradditional.ShowroomFile
            lbtnStrukturOrganisasi.CommandArgument = "../Download.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("UploadDealerProfile") & objdealeradditional.StuctureFile
            'lbtnSalesForce.CommandArgument = "../Download.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("UploadDealerProfile") & objdealeradditional.SalesForceFile
        Else
            Dim objDealerAdditional As New DealerAdditional
            'lbtnSalesForce.Enabled = False
            lbtnShowroomAudit.Enabled = False
            lbtnStrukturOrganisasi.Enabled = False
        End If
        '28-Sep-2007    Deddy H     update base bug 504
        lbtnSalesForce.CommandArgument = "../Salesman/frmSalesmanRekapSalesPerGroupDealer.aspx?Mode=unit&IsFromProfile=1"
    End Sub

    Private Sub SetStatusAdd(ByVal bytVal As Byte)
        lblStatusAdd.Text = CType(bytVal, String) & "S"
    End Sub
#End Region

#Region "EventHandlers"

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
            CheckQueryStr()

        End If
    End Sub

#Region "Need To Add"

    Private Sub CheckQueryStr()
        If (Request.QueryString("DealerID") <> "") Then
            ' ambil data dr database
            Dim intDealerId As Integer = CType(Request.QueryString("DealerID"), Integer)
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(intDealerId)
            Dim objDealerAdditional As DealerAdditional = New DealerAdditionalFacade(User).Retrieve(CType(intDealerId, String), True)

            If Not objDealer Is Nothing Then
                lblDealerCode.Text = objDealer.DealerCode
                lblDealerName.Text = objDealer.DealerName
                lblAddress.Text = objDealer.Address
                lblCityName.Text = objDealer.City.CityName
                lblGroupName.Text = objDealer.DealerGroup.GroupName
                lblPhone.Text = objDealer.Phone
                lblFax.Text = objDealer.Fax
                lblEmail.Text = objDealer.Email
                lblDealerStatus.Text = CType(objDealer.Status, EnumDealerStatus.DealerStatus).ToString
                SetStatusAdd(CType(objDealer.SalesUnitFlag, Byte) + CType(objDealer.ServiceFlag, Byte) + CType(objDealer.SparepartFlag, Byte))
                If Not IsNothing(objDealer.Area1) Then
                    lblAreaCode.Text = objDealer.Area1.AreaCode
                Else
                    lblAreaCode.Text = ""
                End If
            End If

            If Not objDealerAdditional Is Nothing Then
                lblDealerClass.Text = objDealerAdditional.Classification
                lblHeldYear.Text = objDealerAdditional.HeldYear.ToString
            End If

            ' ambil data gambar
            GeneratePhoto(CType(Request.QueryString("DealerID"), Integer))

            'generate hyperlink to download
            GenerateHyperlink()

            'for later use
            sessHelper.SetSession("vDealerID", Request.QueryString("DealerID"))
        End If
    End Sub

    ' penambahan untuk initialize data
    Private Sub ClearData()
        lblDealerCode.Text = String.Empty
        lblDealerName.Text = String.Empty
        lblAddress.Text = String.Empty
        lblCityName.Text = String.Empty
        lblAreaCode.Text = String.Empty
        lblGroupName.Text = String.Empty
        lblPhone.Text = String.Empty
        lblFax.Text = String.Empty
        lblEmail.Text = String.Empty
        lblDealerStatus.Text = String.Empty
        lblDealerClass.Text = String.Empty
        lblHeldYear.Text = String.Empty
    End Sub

    Private Sub Initialize()
        ClearData()
    End Sub

    ' Generate photo from database, table DealerProfilePhoto
    Private Sub GeneratePhoto(ByVal intDealerId As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerProfilePhoto), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DealerProfilePhoto), "Dealer.ID", MatchType.Exact, intDealerId))

        Dim arlImage As ArrayList = New DealerProfilePhotoFacade(User).Retrieve(criterias)
        Dim counter As Integer = 0
        Dim intImageHeight As Integer = 250
        Dim intImageWidth As Integer = 250

        For Each item As DealerProfilePhoto In arlImage
            counter += 1
            Dim objimg As System.Web.UI.WebControls.Image = New System.Web.UI.WebControls.Image
            objimg.ID = "MyImage" & counter.ToString
            objimg.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & item.ID & "&type=" & "DealerSlideShow"

            objimg.Height = Unit.Pixel(intImageHeight)
            objimg.Width = Unit.Pixel(intImageWidth)
            If counter > 1 Then
                objimg.Style.Add("display", "none")
            Else
                objimg.Style.Add("display", "blocked")
            End If

            pnlSlideShow.Controls.Add(objimg)

        Next

    End Sub

    ' ini perlu set security
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Daftar SPL")
        End If

        _create = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiBuat_Privilege)
        _edit = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiUbah_Privilege)
        _view = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiDetail_Privilege)

        'lbtnNew.Visible = _create
        'btnSearch.Visible = _view

    End Sub


    Private Sub RenderProfilePanel(ByVal objDealer As Dealer, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel)
        Dim isReadOnly As Boolean = False
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(isReadOnly)

        If Not objDealer Is Nothing Then
            objRenderPanel.GeneratePanel(objDealer.ID, objPanel, objGroup, profileType, User, True)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User, True)
        End If
    End Sub

#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Redirect("../General/FrmDealerList.aspx?isback=1")
    End Sub

    Private Sub lbtnShowroomAudit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnShowroomAudit.Click
        Response.Redirect(lbtnShowroomAudit.CommandArgument.ToString)
    End Sub

    Private Sub lbtnStrukturOrganisasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnStrukturOrganisasi.Click
        Response.Redirect(lbtnStrukturOrganisasi.CommandArgument.ToString)
    End Sub

    Private Sub lbtnSalesForce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnSalesForce.Click
        Response.Redirect(lbtnSalesForce.CommandArgument.ToString)
    End Sub
End Class
