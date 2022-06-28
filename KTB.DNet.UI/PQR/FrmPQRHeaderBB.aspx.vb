Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Imports System.Text
Imports System.IO
Imports System.Linq
Public Class FrmPQRHeaderBB
#Region "Private Variable"

    Inherits System.Web.UI.Page

    Private _dtKodeKerusakanStartDate As DateTime = New DateTime(2010, 6, 17, 17, 20, 0)

    Private sessHelper As New SessionHelper
    Private oDealer As New Dealer
    Private oLoginUser As New UserInfo

    Private oPQRHeaderBB As PQRHeaderBB
    Private oPQRHeaderBBFacade As New PQRHeaderBBFacade(User)

    Private oPQRDamageCodeBB As New PQRDamageCodeBB
    Private oPQRDamageCodeBBFacade As New PQRDamageCodeBBFacade(User)

    Private oPQRPartsCodeBB As New PQRPartsCodeBB
    Private oPQRPartsCodeBBFacade As New PQRPartsCodeBBFacade(User)

    Private oPQRAdditionalInfoBB As New PQRAdditionalInfoBB
    Private oPQRAdditionalInfoBBFacade As New PQRAdditionalInfoBBFacade(User)

    Private oPQRAttachmentBB As New PQRAttachmentBB
    Private oPQRAttachmentBBFacade As New PQRAttachmentBBFacade(User)

    Private oPQRChangesHistoryBB As New PQRChangesHistoryBB
    Private oPQRChangesHistoryBBFacade As New PQRChangesHistoryBBFacade(User)

    Private oPQRSolutionReferences As New PQRSolutionReferences
    Private oPQRSolutionReferencesFacade As New PQRSolutionReferencesFacade(User)

    Private oChassis As New ChassisMasterBB
    Private oChassisFacade As New ChassisMasterBBFacade(User)

    Private Mode As enumMode.Mode

    Private AttachmentDirectory As String
    Private TargetDirectory As String
    Protected WithEvents ddlKodeWSCA As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKodeWSCB As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKodeWSCC As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPqrType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents buttonInputWSCYes As System.Web.UI.WebControls.Button
    Protected WithEvents buttonInputWSCNo As System.Web.UI.WebControls.Button
    Protected WithEvents panelConfirmation As System.Web.UI.WebControls.Panel
    Protected WithEvents panelPage As System.Web.UI.WebControls.Panel
    Protected WithEvents tblKodeKerusakan As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents TableHeader As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents TableForm As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents Form1 As System.Web.UI.HtmlControls.HtmlForm
    Private TempDirectory As String

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPQRNoVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefPQRNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPembuatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPembuatanVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoChasis As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoMesin As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoMesinVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTypeColor As System.Web.UI.WebControls.Label
    Protected WithEvents lblTypeColorVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblThnProduksi As System.Web.UI.WebControls.Label
    Protected WithEvents lblThnProduksiVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglDelivery As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglDeliveryVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglFakturVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblKecepatan As System.Web.UI.WebControls.Label
    Protected WithEvents txtKecepatan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSubject As System.Web.UI.WebControls.Label
    Protected WithEvents txtSubkect As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblGejala As System.Web.UI.WebControls.Label
    Protected WithEvents txtGejala As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPenyebab As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHasil As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCatatan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerVal As System.Web.UI.WebControls.Label
    Protected WithEvents dgParts As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblAppliedByVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglJamVal As System.Web.UI.WebControls.Label
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents txtSolution As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgFileAttachmentTop As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgFileAttachmentBottom As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lnkbtnCheckChassis As System.Web.UI.WebControls.LinkButton
    Protected WithEvents dgKerusakan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ltrStatusAdditionalInfo As System.Web.UI.WebControls.Literal
    Protected WithEvents txtRefPQRNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblRefPQRNoVal As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoChasis As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNoChasisVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblOdometerVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblKecepatanVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubjectVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblPenyebab As System.Web.UI.WebControls.Label
    Protected WithEvents lblHasil As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglKerusakanVal As System.Web.UI.WebControls.Label
    Protected WithEvents btnStatusChange As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancelStatusChange As System.Web.UI.WebControls.Button
    Protected WithEvents lblLastPostedInfo As System.Web.UI.WebControls.Label
    Protected WithEvents lnkbtnAdditionalInfoPopUp As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblBobot As System.Web.UI.WebControls.Label
    Protected WithEvents ddlBobot As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblAppliedBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglJam As System.Web.UI.WebControls.Label
    Protected WithEvents lblProcessBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblProcessByVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglJamProcess As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglJamProcessVal As System.Web.UI.WebControls.Label
    Protected WithEvents lnkbtnPopUpInfoKendaraan As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbModel As System.Web.UI.WebControls.Label
    Protected WithEvents icTglKerusakan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtOdometer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTglKerusakan As System.Web.UI.WebControls.Label
    Protected WithEvents lblOdometer As System.Web.UI.WebControls.Label
    Protected WithEvents lblCatatan As System.Web.UI.WebControls.Label
    Protected WithEvents dgQRS As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCetak As System.Web.UI.WebControls.Button
    Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel
    Protected WithEvents rfvChassisNumber As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvOdometer As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvKecepatan As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvSubject As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvGejala As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvPenyebab As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents lnkBtnCheckWONumber As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtWONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerBranchCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBranchName As System.Web.UI.WebControls.TextBox
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        AttachmentDirectory = KTB.DNet.Lib.WebConfig.GetValue("PQRAttachmentBBDir")
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\PQRTemp\" + oDealer.ID.ToString + "-" + oLoginUser.ID.ToString + "\"


        If Request.QueryString("Mode").ToString() = "New" Then
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQRBB, Panel1, False)
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQRBB, Panel2, False)
        ElseIf Request.QueryString("Mode").ToString() = "Edit" OrElse Request.QueryString("Mode").ToString() = "View" Then
            oPQRHeaderBB = CType(sessHelper.GetSession("PQR"), PQRHeaderBB)
            If oDealer.Title = 0 Then 'Dealer
                If CType(oPQRHeaderBB.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Baru Then
                    'If oPQRHeaderBB.CreatedBy.Substring(0, 6) = "000002" Then
                    '    RenderProfilePanel(oPQRHeaderBB, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQRBB, Panel1, True)
                    '    RenderProfilePanel(oPQRHeaderBB, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQRBB, Panel2, True)
                    'Else
                    RenderProfilePanel(oPQRHeaderBB, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQRBB, Panel1, False)
                    RenderProfilePanel(oPQRHeaderBB, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQRBB, Panel2, False)
                    'End If
                Else
                    RenderProfilePanel(oPQRHeaderBB, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQRBB, Panel1, True)
                    RenderProfilePanel(oPQRHeaderBB, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQRBB, Panel2, True)
                End If
            ElseIf oDealer.Title = 1 Then ' KTB 
                If CType(oPQRHeaderBB.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Baru Then
                    RenderProfilePanel(oPQRHeaderBB, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQRBB, Panel1, False)
                    RenderProfilePanel(oPQRHeaderBB, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQRBB, Panel2, False)
                Else
                    RenderProfilePanel(oPQRHeaderBB, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQRBB, Panel1, True)
                    RenderProfilePanel(oPQRHeaderBB, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQRBB, Panel2, True)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If SecurityProvider.Authorize(context.User, SR.PQRNewView_Privilege) Then
            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If Request.QueryString("Mode").ToString() = "New" Then
                    If Not (SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege)) Then
                        Server.Transfer("../FrmAccessDenied.aspx?modulName=PRODUCT QUALITY REPORT - Buat PQR")
                    End If
                End If
            End If
        Else
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PRODUCT QUALITY REPORT - Buat PQR")
        End If
    End Sub

    Dim bCekPriv As Boolean = (SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege))

#End Region

    Private Sub RenderProfilePanel(ByVal objPQRHeaderBB As PQRHeaderBB, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel, ByVal isReadonly As Boolean)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(isReadonly)

        If Not objPQRHeaderBB Is Nothing Then
            objRenderPanel.GeneratePanel(objPQRHeaderBB.ID, objPanel, objGroup, profileType, User)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User)
        End If
    End Sub

    Private Sub ClearForm()

        lblPQRNoVal.Text = ""
        txtRefPQRNo.Text = ""
        lblTglPembuatanVal.Text = DateTime.Today.ToString("dd/MM/yyyy")

        txtNoChasis.Text = ""
        ClearChassisInfo()
        icTglKerusakan.Value = DateTime.Today
        txtOdometer.Text = ""
        txtKecepatan.Text = ""
        ddlBobot.SelectedIndex = 0
        txtSubject.Text = ""
        txtGejala.Text = ""
        txtPenyebab.Text = ""
        txtHasil.Text = ""
        txtCatatan.Text = ""
        txtSolution.Text = ""

        lblStatusVal.Text = EnumPQR.PQRStatus.Baru.ToString
        lblProcessByVal.Text = ""
        lblTglJamProcessVal.Text = ""
        txtDealerBranchCode.Text = ""
        txtBranchName.Text = ""

    End Sub
    Private Sub LoadForm()
        lblPQRNoVal.Text = oPQRHeaderBB.PQRNo
        txtRefPQRNo.Text = oPQRHeaderBB.RefPQRNo
        lblRefPQRNoVal.Text = oPQRHeaderBB.RefPQRNo
        lblTglPembuatanVal.Text = oPQRHeaderBB.DocumentDate.ToString("dd/MM/yyyy")

        LoadChassisInfo(CType(oChassisFacade.Retrieve(oPQRHeaderBB.ChassisMasterBB.ID), ChassisMasterBB))
        icTglKerusakan.Value = oPQRHeaderBB.PQRDate.ToString("dd/MM/yyyy")
        lblTglKerusakanVal.Text = oPQRHeaderBB.PQRDate.ToString("dd/MM/yyyy")
        txtOdometer.Text = oPQRHeaderBB.OdoMeter
        lblOdometerVal.Text = oPQRHeaderBB.OdoMeter
        txtKecepatan.Text = oPQRHeaderBB.Velocity
        lblKecepatanVal.Text = oPQRHeaderBB.Velocity
        ddlBobot.SelectedValue = oPQRHeaderBB.Bobot
        txtSubject.Text = oPQRHeaderBB.Subject
        lblSubjectVal.Text = oPQRHeaderBB.Subject
        txtGejala.Text = oPQRHeaderBB.Symptomps
        txtPenyebab.Text = oPQRHeaderBB.Causes
        txtHasil.Text = oPQRHeaderBB.Results
        txtCatatan.Text = oPQRHeaderBB.Notes
        txtSolution.Text = oPQRHeaderBB.Solutions
        If Not IsNothing(oPQRHeaderBB.DealerBranch) Then
            txtDealerBranchCode.Text = oPQRHeaderBB.DealerBranch.DealerBranchCode
            txtBranchName.Text = oPQRHeaderBB.DealerBranch.Name
        End If
        If Not IsNothing(oPQRHeaderBB.WorkOrderNumber) Then
            txtWONumber.Text = oPQRHeaderBB.WorkOrderNumber
        End If
        lbModel.Text = oPQRHeaderBB.ChassisMasterBB.Category.Description
        lblStatusVal.Text = CType(oPQRHeaderBB.RowStatus, EnumPQR.PQRStatus).ToString
        If CType(oPQRHeaderBB.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Proses Or CType(oPQRHeaderBB.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Rilis Or CType(oPQRHeaderBB.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Selesai Then
            If oPQRHeaderBB.ConfirmBy <> String.Empty Then
                lblProcessByVal.Text = CommonFunction.FormatSavedUser(oPQRHeaderBB.ConfirmBy, User)
            Else
                lblProcessByVal.Text = ""
            End If
            If oPQRHeaderBB.ConfirmTime <= New DateTime(1900, 1, 1) Then
                lblTglJamProcessVal.Text = ""
            Else
                lblTglJamProcessVal.Text = oPQRHeaderBB.ConfirmTime.ToString("dd/MM/yyyy HH:mm:ss")
            End If
        Else
            lblProcessByVal.Text = ""
            lblTglJamProcessVal.Text = ""
        End If
        If oPQRHeaderBB.CodeA.Trim <> "" Then
            Me.ddlKodeWSCA.SelectedValue = oPQRHeaderBB.CodeA
        End If
        If oPQRHeaderBB.CodeB.Trim <> "" Then
            Me.ddlKodeWSCB.SelectedValue = oPQRHeaderBB.CodeB
        End If
        If oPQRHeaderBB.CodeC.Trim <> "" Then
            Me.ddlKodeWSCC.SelectedValue = oPQRHeaderBB.CodeC
        End If
    End Sub
    Private Sub fillForm()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        oPQRHeaderBB = CType(sessHelper.GetSession("PQR"), PQRHeaderBB)

        If Mode = enumMode.Mode.NewItemMode Then
            oPQRHeaderBB = CType(sessHelper.GetSession("NEW_PQR"), PQRHeaderBB)
            ddlPqrType.SelectedIndex = oPQRHeaderBB.PQRType + 1
            ClearForm()
            lblDealerVal.Text = oDealer.DealerCode & " - " & oDealer.DealerName
            lblAppliedByVal.Text = oDealer.DealerCode & " - " & oLoginUser.UserName
            lblTglJamVal.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm")

            ltrStatusAdditionalInfo.Text = "Tidak Ada"
            lnkbtnAdditionalInfoPopUp.Visible = False
            lblLastPostedInfo.Visible = False
            lblLastPostedInfo.ToolTip = ""
        ElseIf Mode = enumMode.Mode.EditMode OrElse Mode = enumMode.Mode.ViewMode Then
            lblDealerVal.Text = oPQRHeaderBB.Dealer.DealerCode & " - " & oPQRHeaderBB.Dealer.SearchTerm1
            lblAppliedByVal.Text = CommonFunction.FormatSavedUser(oPQRHeaderBB.CreatedBy, User)
            lblTglJamVal.Text = oPQRHeaderBB.CreatedTime.ToString("dd/MM/yyyy HH:mm")
            ddlPqrType.SelectedIndex = oPQRHeaderBB.PQRType + 1
            If oPQRHeaderBB.PQRAdditionalInfoBBs.Count > 0 Then
                ltrStatusAdditionalInfo.Text = "Ada"
                lblLastPostedInfo.Visible = True

                Dim tempArr As ArrayList = oPQRHeaderBB.PQRAdditionalInfoBBs
                tempArr = KTB.DNet.Utility.CommonFunction.SortArraylist(tempArr, GetType(PQRAdditionalInfoBB), "CreatedTime", Sort.SortDirection.DESC)

                Dim obj As PQRAdditionalInfoBB = CType(tempArr(0), PQRAdditionalInfoBB)
                lblLastPostedInfo.ToolTip = CommonFunction.FormatSavedUser(obj.CreatedBy, User) & " [" & obj.CreatedTime.ToString("dd/MM/yyyy HH:mm:ss") & "]"

                If obj.CreatedBy.Length > 6 Then
                    Dim LastPostedUser As Dealer = New DealerFacade(User).Retrieve(CInt(obj.CreatedBy.Substring(0, 6)))
                    If Not LastPostedUser Is Nothing Then
                        Dim img As HtmlImage = CType(lblLastPostedInfo.FindControl("img"), HtmlImage)
                        If LastPostedUser.Title = 0 Then    'Dealer
                            img.Src = "../images/icon_mail_1.gif"
                        ElseIf LastPostedUser.Title = 1 Then    'KTB
                            img.Src = "../images/icon_mail.gif"
                        End If
                    End If
                End If

            Else
                ltrStatusAdditionalInfo.Text = "Tidak Ada"
                lblLastPostedInfo.Visible = False
                lblLastPostedInfo.ToolTip = ""
            End If
            lnkbtnAdditionalInfoPopUp.Visible = True
            'lnkbtnAdditionalInfoPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PQR/FrmPQRAdditionalInfoBB.aspx?PQRID=" & oPQRHeaderBB.ID, "", 710, 700, "ShowPopUp")
            LoadForm()

            'add by reza
            If lblPQRNoVal.Text <> "" Then
                ddlPqrType.Enabled = False
            Else
                ddlPqrType.Enabled = True
            End If
            'end
        End If

        RefreshGrid()
        setFormView()
        If Not Mode = enumMode.Mode.NewItemMode Then
            If Not (Request.QueryString("Src") = "WSCDetail" Or Request.QueryString("Src") = "PQRListKondisi") Then
                setStatusAction()
            End If
        End If
    End Sub
    Private Sub RefreshGrid()
        BindDamageCode()
        BindParts()
        BindAttachmentTop()
        BindAttachmentBottom()
        BindQRS()
    End Sub
    Private Sub setFormView()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        oPQRHeaderBB = CType(sessHelper.GetSession("PQR"), PQRHeaderBB)
        ddlBobot.Visible = False
        If Mode = enumMode.Mode.NewItemMode OrElse Mode = enumMode.Mode.EditMode Then
            btnSimpan.Visible = bCekPriv
            oPQRHeaderBB = CType(sessHelper.GetSession("NEW_PQR"), PQRHeaderBB)
            If Mode = enumMode.Mode.NewItemMode Then
                btnStatusChange.Visible = False
                btnCancelStatusChange.Visible = False
                btnBatal.Visible = False
                btnCetak.Visible = False
            ElseIf Mode = enumMode.Mode.EditMode Then
                btnStatusChange.Visible = True
                btnCancelStatusChange.Visible = True
                btnBatal.Visible = True
                btnCetak.Visible = True
                'If oLoginUser.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                '    'If oPQRHeaderBB.CreatedBy.Substring(0, 6) = "000002" Then
                '    btnStatusChange.Visible = False
                '    btnCancelStatusChange.Visible = False
                '    btnBatal.Visible = False
                '    btnCetak.Visible = True
                '    'End If
                'End If
            End If

            SetEntryArea(oDealer.Title)

            If oDealer.Title = 1 And Mode = enumMode.Mode.EditMode Then 'KTB
                If oPQRHeaderBB.RowStatus = EnumPQR.PQRStatus.Rilis Then
                    ddlBobot.Visible = True
                End If
            End If

        ElseIf Mode = enumMode.Mode.ViewMode Then
            txtRefPQRNo.Visible = False
            txtNoChasis.Visible = False
            icTglKerusakan.Visible = False
            txtOdometer.Visible = False
            txtKecepatan.Visible = False
            txtSubject.Visible = False
            lnkbtnCheckChassis.Visible = False

            lblRefPQRNoVal.Visible = True
            lblNoChasisVal.Visible = True
            lblTglKerusakanVal.Visible = True
            lblOdometerVal.Visible = True
            lblKecepatanVal.Visible = True
            lblSubjectVal.Visible = True

            txtGejala.ReadOnly = True
            txtPenyebab.ReadOnly = True
            txtHasil.ReadOnly = True
            txtCatatan.ReadOnly = True
            txtSolution.ReadOnly = True

            dgKerusakan.ShowFooter = False
            dgKerusakan.Columns(dgKerusakan.Columns.Count - 1).Visible = False
            dgParts.ShowFooter = False
            dgParts.Columns(dgParts.Columns.Count - 1).Visible = False
            dgFileAttachmentTop.ShowFooter = False
            dgFileAttachmentTop.Columns(dgFileAttachmentTop.Columns.Count - 1).Visible = False
            dgFileAttachmentBottom.ShowFooter = False
            dgFileAttachmentBottom.Columns(dgFileAttachmentBottom.Columns.Count - 1).Visible = False

            If Request.QueryString("Src") = "WSCDetail" Or Request.QueryString("Src") = "PQRListKondisi" Then
                btnBatal.Visible = True
                btnStatusChange.Visible = False
                btnCancelStatusChange.Visible = False
            Else
                btnBatal.Visible = True
                btnStatusChange.Visible = True
                btnCancelStatusChange.Visible = True
            End If
            btnSimpan.Visible = False
            btnCetak.Visible = True
            If oDealer.Title = 1 Then 'KTB
                ddlBobot.Visible = True
                ddlBobot.Enabled = False
            End If

        End If
        If Not IsNothing(oPQRHeaderBB) AndAlso oPQRHeaderBB.ID > 0 AndAlso (oPQRHeaderBB.RowStatus = EnumPQR.PQRStatus.Batal Or oPQRHeaderBB.RowStatus = EnumPQR.PQRStatus.Selesai) Then
            dgQRS.ShowFooter = False
            dgQRS.Columns(dgQRS.Columns.Count - 1).Visible = False
        Else
            If Not Mode = enumMode.Mode.NewItemMode Then
                If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    dgQRS.ShowFooter = True
                    dgQRS.Columns(dgQRS.Columns.Count - 1).Visible = True
                    btnSimpan.Visible = bCekPriv
                    ViewState("Mode") = enumMode.Mode.EditMode
                Else
                    If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                        dgQRS.ShowFooter = True
                        dgQRS.Columns(dgQRS.Columns.Count - 1).Visible = True
                        btnSimpan.Visible = bCekPriv
                        ViewState("Mode") = enumMode.Mode.EditMode
                    End If
                    dgQRS.ShowFooter = False
                    dgQRS.Columns(dgQRS.Columns.Count - 1).Visible = False
                End If
            End If
        End If

    End Sub
    Private Sub SetEntryArea(ByVal DealerTitle As Integer)
        If DealerTitle = 0 Then ' Dealer
            txtSolution.ReadOnly = True
            dgFileAttachmentBottom.ShowFooter = False
            dgFileAttachmentBottom.Columns(dgFileAttachmentBottom.Columns.Count - 1).Visible = False
        ElseIf DealerTitle = 1 Then 'KTB
            If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                txtRefPQRNo.Visible = True
                txtNoChasis.Visible = True
                icTglKerusakan.Visible = True
                txtOdometer.Visible = True
                txtKecepatan.Visible = True
                txtSubject.Visible = True

                lnkbtnCheckChassis.Visible = True
                dgKerusakan.ShowFooter = True
                dgKerusakan.Columns(dgKerusakan.Columns.Count - 1).Visible = True
                dgParts.ShowFooter = True
                dgParts.Columns(dgParts.Columns.Count - 1).Visible = True
                dgFileAttachmentTop.ShowFooter = True
                dgFileAttachmentTop.Columns(dgFileAttachmentTop.Columns.Count - 1).Visible = True

                txtSolution.ReadOnly = True
                dgFileAttachmentBottom.ShowFooter = False
                dgFileAttachmentBottom.Columns(dgFileAttachmentBottom.Columns.Count - 1).Visible = False

                lblRefPQRNoVal.Visible = False
                lblNoChasisVal.Visible = False
                lblTglKerusakanVal.Visible = False
                lblOdometerVal.Visible = False
                lblKecepatanVal.Visible = False
                lblSubjectVal.Visible = False

                txtGejala.ReadOnly = False
                txtPenyebab.ReadOnly = False
                txtHasil.ReadOnly = False
                txtCatatan.ReadOnly = False
            Else
                txtRefPQRNo.Visible = False
                txtNoChasis.Visible = False
                icTglKerusakan.Visible = False
                txtOdometer.Visible = False
                txtKecepatan.Visible = False
                txtSubject.Visible = False
                lnkbtnCheckChassis.Visible = False

                dgKerusakan.ShowFooter = False
                dgKerusakan.Columns(dgKerusakan.Columns.Count - 1).Visible = False
                dgParts.ShowFooter = False
                dgParts.Columns(dgParts.Columns.Count - 1).Visible = False
                dgFileAttachmentTop.ShowFooter = False
                dgFileAttachmentTop.Columns(dgFileAttachmentTop.Columns.Count - 1).Visible = False

                dgQRS.ShowFooter = False
                dgQRS.Columns(dgQRS.Columns.Count - 1).Visible = False

                txtSolution.ReadOnly = False
                dgFileAttachmentBottom.ShowFooter = True
                dgFileAttachmentBottom.Columns(dgFileAttachmentBottom.Columns.Count - 1).Visible = True

                lblRefPQRNoVal.Visible = True
                lblNoChasisVal.Visible = True
                lblTglKerusakanVal.Visible = True
                lblOdometerVal.Visible = True
                lblKecepatanVal.Visible = True
                lblSubjectVal.Visible = True

                txtGejala.ReadOnly = True
                txtPenyebab.ReadOnly = True
                txtHasil.ReadOnly = True
                txtCatatan.ReadOnly = True
            End If
            oPQRHeaderBB = CType(sessHelper.GetSession("PQR"), PQRHeaderBB)
            If Not IsNothing(oPQRHeaderBB) AndAlso oPQRHeaderBB.ID > 0 AndAlso CType(oPQRHeaderBB.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Selesai Then
                ddlBobot.Visible = True
                ddlBobot.Enabled = False
            End If

        End If

    End Sub


    Private Sub setStatusAction()

        oPQRHeaderBB = CType(sessHelper.GetSession("PQR"), PQRHeaderBB)
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALERINFO"), Dealer)
        Select Case CType(oPQRHeaderBB.RowStatus, EnumPQR.PQRStatus)
            Case EnumPQR.PQRStatus.Batal
                btnStatusChange.Text = "Restore"
                btnCancelStatusChange.Text = "-"
                btnStatusChange.Visible = False
                btnCancelStatusChange.Visible = False
            Case EnumPQR.PQRStatus.Baru
                btnStatusChange.Text = "Validasi"
                btnCancelStatusChange.Text = "Batal"
                If CType(sessHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
                    'If oPQRHeaderBB.CreatedBy.Substring(0, 6) = "000002" Then
                    '    btnStatusChange.Visible = False
                    '    btnCancelStatusChange.Visible = False
                    'Else
                    btnStatusChange.Visible = True
                    btnCancelStatusChange.Visible = True
                    btnCancelStatusChange.Visible = SecurityProvider.Authorize(context.User, SR.PQR_status_Batal_Privilege)
                    btnStatusChange.Visible = SecurityProvider.Authorize(context.User, SR.PQR_status_validasi_Privilege)
                    'End If
                End If
            Case EnumPQR.PQRStatus.Validasi
                btnStatusChange.Text = "Proses"
                btnCancelStatusChange.Text = "Batal"
                btnStatusChange.Visible = True
                btnCancelStatusChange.Visible = True
                If CType(sessHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
                    btnStatusChange.Visible = False
                    btnCancelStatusChange.Visible = False
                    btnSimpan.Visible = False
                Else
                    If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                        btnSimpan.Visible = False
                        btnCancelStatusChange.Visible = False
                    End If
                End If
                If btnCancelStatusChange.Visible = True Then
                    btnCancelStatusChange.Visible = SecurityProvider.Authorize(context.User, SR.PQR_status_validasi_batal_Privilege)
                End If
                If btnStatusChange.Visible = True Then
                    btnStatusChange.Visible = SecurityProvider.Authorize(context.User, SR.PQR_status_proses_Privilege)
                End If
            Case EnumPQR.PQRStatus.Proses
                btnStatusChange.Text = "Rilis"
                btnCancelStatusChange.Text = "Batal"
                If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                        btnCancelStatusChange.Visible = True
                        btnStatusChange.Visible = False
                        btnSimpan.Visible = False
                        txtSolution.Visible = True 'bug 1847
                    Else
                        btnSimpan.Visible = True
                        btnStatusChange.Visible = True
                        btnCancelStatusChange.Visible = False
                    End If
                Else
                    '000n
                    btnCancelStatusChange.Visible = True
                    btnStatusChange.Visible = False
                    btnSimpan.Visible = False
                    txtSolution.Visible = True 'bug 1847
                End If
                If btnCancelStatusChange.Visible = True Then
                    btnCancelStatusChange.Visible = SecurityProvider.Authorize(context.User, SR.PQR_status_proses_batal_Privilege)
                End If
                If btnStatusChange.Visible = True Then
                    btnStatusChange.Visible = SecurityProvider.Authorize(context.User, SR.PQR_status_rilis_Privilege)
                End If
            Case EnumPQR.PQRStatus.Rilis
                btnStatusChange.Text = "Selesai"
                btnCancelStatusChange.Text = "Batal"
                btnStatusChange.Visible = True
                btnCancelStatusChange.Visible = True
                btnCancelStatusChange.Visible = SecurityProvider.Authorize(context.User, SR.PQR_status_rilis_batal_Privilege)
                btnStatusChange.Visible = SecurityProvider.Authorize(context.User, SR.PQR_status_selesai_Privilege)
                If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    If Not SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                        btnSimpan.Visible = True
                    Else
                        btnSimpan.Visible = False
                    End If
                Else
                    btnSimpan.Visible = False
                End If
            Case EnumPQR.PQRStatus.Selesai
                btnStatusChange.Text = "-"
                btnCancelStatusChange.Text = "Batal"
                btnStatusChange.Visible = False
                btnCancelStatusChange.Visible = True
                btnCancelStatusChange.Visible = SecurityProvider.Authorize(context.User, SR.PQR_status_selesai_batal_Privilege)
        End Select
    End Sub
    Private Sub BindBobot()
        ddlBobot.Items.Clear()
        ddlBobot.Items.Add(New ListItem("Silakan Pilih", 0))
        ddlBobot.Items.Add(New ListItem("0", 0))
        ddlBobot.Items.Add(New ListItem("5", 5))
        ddlBobot.Items.Add(New ListItem("10", 10))
        ddlBobot.Items.Add(New ListItem("20", 20))
        'ddlBobot.Items.Add(New ListItem("5", 5))
    End Sub

    Private Sub BindPqrType()
        ddlPqrType.Items.Clear()
        ddlPqrType.Items.Add(New ListItem("Silahkan Pilih", Nothing))
        ddlPqrType.Items.Add(New ListItem("PQR WSC", 0))
        ddlPqrType.Items.Add(New ListItem("PQR Only", 1))
    End Sub

    Private Sub BindKodePosisiWSC()
        BindPosisiWSC(ddlKodeWSCA, "A")
        BindPosisiWSC(ddlKodeWSCB, "B")
        BindPosisiWSC(ddlKodeWSCC, "C")
    End Sub

    Private Sub BindPosisiWSC(ByRef ddl As DropDownList, ByVal PositionCategory As String)
        Dim oPWFac As PositionWSCFacade = New PositionWSCFacade(User)
        Dim cPW As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PositionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aPW As New ArrayList
        Dim arlItems As New ArrayList

        cPW.opAnd(New Criteria(GetType(PositionWSC), "PositionCategory", MatchType.Exact, PositionCategory))
        aPW = oPWFac.Retrieve(cPW)
        With ddl.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each oPW As PositionWSC In aPW
                .Add(New ListItem(oPW.PositionCode & " - " & oPW.Description, oPW.PositionCode))
            Next
        End With
    End Sub

    Private Sub SetControlKodeKerusakan(ByVal oPQRH As PQRHeaderBB)
        Dim IsImplementKodeKerusakan As Boolean = False
        Mode = CType(ViewState("Mode"), enumMode.Mode)

        ddlKodeWSCA.Enabled = False
        ddlKodeWSCB.Enabled = False
        ddlKodeWSCC.Enabled = False
        If Mode = enumMode.Mode.NewItemMode OrElse Mode = enumMode.Mode.EditMode Then
            If CType(sessHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
                ddlKodeWSCA.Enabled = True
                ddlKodeWSCB.Enabled = True
                ddlKodeWSCC.Enabled = True
            Else
                If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                    If Mode = enumMode.Mode.NewItemMode OrElse Mode = enumMode.Mode.EditMode Then
                        ddlKodeWSCA.Enabled = True
                        ddlKodeWSCB.Enabled = True
                        ddlKodeWSCC.Enabled = True
                    End If
                End If
            End If
        End If

        If Mode = enumMode.Mode.NewItemMode And Now >= _dtKodeKerusakanStartDate Then
            IsImplementKodeKerusakan = True
        Else
            If Not IsNothing(oPQRH) AndAlso oPQRH.ID > 0 Then
                If oPQRH.CreatedTime >= _dtKodeKerusakanStartDate Then
                    IsImplementKodeKerusakan = True
                End If
            End If
        End If
        If IsImplementKodeKerusakan Then
            tblKodeKerusakan.Visible = True
        Else
            tblKodeKerusakan.Visible = False
        End If
    End Sub


    Private Sub ShowConfirmation(ByVal isShowed As Boolean)
        'If isShowed = True Then
        '    TableForm.Visible = True
        '    panelConfirmation.Visible = False
        'Else
        'panelConfirmation.Visible = False
        'TableForm.Visible = True
        'End If
    End Sub

    Private Sub ValidatePqrWSC()
        Dim result As Integer = 0
        Try
            Dim oPQRHeaderBB As PQRHeaderBB = sessHelper.GetSession("inputWSCSes")
            result = oPQRHeaderBB.ID
        Catch
        End Try
        Dim strJs As String = ""
        strJs += "var pqrWscConfirmation = confirm('Simpan Data Berhasil. \nInput WSC?');"
        strJs += "if(pqrWscConfirmation) {"
        strJs += "window.location = '../Service/FrmWSCHeaderBB.aspx?screenFrom=PQR&PQRId=" & result & "';"
        strJs += "}"
        strJs += "else {"
        strJs += "window.location = 'FrmPQRListBB.aspx';"
        strJs += "}"

        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ShowConfirmation(False)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        InitiateAuthorization()
        If Not IsPostBack Then
            BindPqrType()
            BindBobot()
            BindKodePosisiWSC()
            If Request.QueryString("Mode").ToString() = "New" Then
                ViewState("Mode") = enumMode.Mode.NewItemMode
                oPQRHeaderBB = New PQRHeaderBB

                sessHelper.SetSession("NEW_PQR", oPQRHeaderBB)
                sessHelper.SetSession("NEW_DAMAGECODE", New ArrayList)
                sessHelper.SetSession("NEW_PARTS", New ArrayList)
                sessHelper.SetSession("NEW_ATTACHMENTTOP", New ArrayList)
                sessHelper.SetSession("NEW_ATTACHMENTBOTTOM", New ArrayList)
                sessHelper.SetSession("NEW_QRS", New ArrayList)
            ElseIf Request.QueryString("Mode").ToString() = "Edit" OrElse Request.QueryString("Mode").ToString() = "View" Then
                If Request.QueryString("Mode").ToString() = "Edit" Then
                    ViewState("Mode") = enumMode.Mode.EditMode
                ElseIf Request.QueryString("Mode").ToString() = "View" Then
                    ViewState("Mode") = enumMode.Mode.ViewMode
                End If
                oPQRHeaderBB = CType(sessHelper.GetSession("PQR"), PQRHeaderBB)

                sessHelper.SetSession("DAMAGECODE", oPQRHeaderBB.PQRDamageCodeBBs)
                sessHelper.SetSession("DELETEDDAMAGECODE", New ArrayList)

                sessHelper.SetSession("PARTS", oPQRHeaderBB.PQRPartsCodeBBs)
                sessHelper.SetSession("DELETEDPARTS", New ArrayList)

                sessHelper.SetSession("ATTACHMENTTOP", GetAttachmentList(oPQRHeaderBB.PQRAttachmentBBs, EnumPQR.AttachmentLocation.Top))
                sessHelper.SetSession("ATTACHMENTBOTTOM", GetAttachmentList(oPQRHeaderBB.PQRAttachmentBBs, EnumPQR.AttachmentLocation.Bottom))
                sessHelper.SetSession("DELETEDATTACHMENT", New ArrayList)

                sessHelper.SetSession("QRS", oPQRHeaderBB.PQRQRSBBs)
                sessHelper.SetSession("DELETEDQRS", New ArrayList)
            End If
            SetControlKodeKerusakan(oPQRHeaderBB)
            txtBranchName.Attributes.Add("readonly", "readonly")
            fillForm()

        End If

        'BindDamageCode()
        'BindParts()
        'BindAttachmentTop()
        'BindAttachmentBottom()

    End Sub
    Private Function GetAttachmentList(ByVal attachmentCollection As ArrayList, ByVal location As EnumPQR.AttachmentLocation) As ArrayList
        Dim TempList As New ArrayList
        TempList.Clear()
        For Each obj As PQRAttachmentBB In attachmentCollection
            If obj.Type = location Then
                TempList.Add(obj)
            End If
        Next
        Return TempList
    End Function
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        If Request.QueryString("Src") = "WSCDetail" Then
            Server.Transfer("~/Service/FrmWSCDetailBB.aspx")
        ElseIf Request.QueryString("Src") = "PQRListKondisi" Then
            Server.Transfer("~/PQR/FrmPQRListKondisiBB.aspx")
        ElseIf Request.QueryString("Src") = "WSCList" Then
            Server.Transfer("~/Service/FrmWSCHeaderBB.aspx?screenFrom=WSC&WSCId=" & Request.QueryString("WSCId") & "&viewStateMode=" & Request.QueryString("State"))
        Else
            Server.Transfer("~/PQR/FrmPQRListBB.aspx")
        End If
    End Sub

    Private Sub lnkBtnCheckWONumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBtnCheckWONumber.Click


        Try
            ' Get DMSWOWarrantyClaim based on WO Number            
            Dim warrantyCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DMSWOWarrantyClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            warrantyCriteria.opAnd(New Criteria(GetType(DMSWOWarrantyClaim), "WorkOrderNumber", MatchType.Exact, txtWONumber.Text))
            warrantyCriteria.opAnd(New Criteria(GetType(DMSWOWarrantyClaim), "isBB", MatchType.Exact, 1))
            Dim warrantyList As ArrayList = New DMSWOWarrantyClaimFacade(HttpContext.Current.User).Retrieve(warrantyCriteria)
            If warrantyList.Count = 0 Then
                ' DMSWOWarrantyClaim not found
                MessageBox.Show("Nomor WO Tidak Valid")
            Else

                ' Check Warranty Claim with DealerCode
                Dim dmsWOWarrantyClaim As DMSWOWarrantyClaim = warrantyList(0)
                If dmsWOWarrantyClaim.Dealer.DealerCode <> lblDealerVal.Text.Split("-")(0).Trim Then
                    ' WO Number not valid
                    MessageBox.Show("Nomor WO tidak sesuai dengan Dealer Code")
                Else
                    ' Copy Value to UI
                    If Not IsNothing(dmsWOWarrantyClaim.DealerBranch) Then
                        txtDealerBranchCode.Text = dmsWOWarrantyClaim.DealerBranch.DealerBranchCode
                    End If
                    txtNoChasis.Text = dmsWOWarrantyClaim.ChassisNumber
                    icTglKerusakan.Value = dmsWOWarrantyClaim.FailureDate
                    txtOdometer.Text = dmsWOWarrantyClaim.Mileage
                    txtGejala.Text = dmsWOWarrantyClaim.Symptoms
                    txtPenyebab.Text = dmsWOWarrantyClaim.Causes
                    txtHasil.Text = dmsWOWarrantyClaim.Results
                    txtCatatan.Text = dmsWOWarrantyClaim.Notes
                    lnkbtnCheckChassis_Click(sender, e)
                    MessageBox.Show("Penyalinan data dari Klaim Garansi berhasil")
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

#Region "Datagrid QRS"
    Private Sub dgQRS_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgQRS.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            SetdgQRSItemFooter(e)
        End If
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            SetdgQRSItem(e)
        End If
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetdgQRSItemEdit(e)
        End If

    End Sub
    Private Sub SetdgQRSItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        ' prepare for future use
    End Sub
    Private Sub SetdgQRSItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        ' prepare for future use
    End Sub
    Private Sub SetdgQRSItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Add(lNum)
    End Sub

    Private Function QRSIsExist(ByVal ChassisMasterBBID As Integer, ByVal QRSCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If Not QRSCollection Is Nothing Then
            If QRSCollection.Count > 0 Then
                For Each _PQRQRSBB As PQRQRSBB In QRSCollection
                    If _PQRQRSBB.ChassisMasterBB.ID = ChassisMasterBBID Then
                        bResult = True
                        Exit For
                    End If
                Next
            End If
        End If

        Return bResult
    End Function
    Private Function QRSIsExist(ByVal ChassisMasterBBID As Integer, ByVal QRSCollection As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim i As Integer = 0
        Dim bResult As Boolean = False

        If QRSCollection.Count > 0 Then
            For Each _PQRQRSBB As PQRQRSBB In QRSCollection
                If _PQRQRSBB.ChassisMasterBB.ID = ChassisMasterBBID AndAlso nIndeks <> i Then
                    bResult = True
                    Exit For
                End If
                i += 1
            Next
        End If
        Return bResult
    End Function
    Private Function QRSIsExist(ByVal ChassisNumber As String) As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, ChassisNumber.Trim))

        Dim tempArr As ArrayList = New ChassisMasterBBFacade(User).Retrieve(criterias)

        If tempArr.Count > 0 Then
            Return True
        End If

        Return False
    End Function

    Private Function QRSIsSameCategory(ByVal ChassisQRS As ChassisMasterBB, ByVal ChassisPQR As ChassisMasterBB) As Boolean
        Dim result As Boolean = False
        If Not (ChassisPQR Is Nothing Or ChassisPQR.ID = 0) And Not (ChassisQRS Is Nothing Or ChassisQRS.ID = 0) Then
            If ChassisPQR.Category.CategoryCode = ChassisQRS.Category.CategoryCode Then
                result = True
            End If
        End If
        Return result

    End Function


    Private Sub dgQRS_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgQRS.ItemCommand
        Dim _arrQRS As New ArrayList
        _arrQRS = CType(sessHelper.GetSession("QRS"), ArrayList)

        Mode = CType(ViewState("Mode"), enumMode.Mode)

        If Mode = enumMode.Mode.NewItemMode Then
            _arrQRS = CType(sessHelper.GetSession("NEW_QRS"), ArrayList)
        Else
            _arrQRS = CType(sessHelper.GetSession("QRS"), ArrayList)
        End If
        Dim objChassisMain As ChassisMasterBB
        objChassisMain = New ChassisMasterBBFacade(User).Retrieve(txtNoChasis.Text.Trim())
        Mode = CType(ViewState("Mode"), enumMode.Mode)

        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid

                Dim txtNoRangkaFooter As TextBox = CType(e.Item.FindControl("txtNoRangkaFooter"), TextBox)
                Dim icTglKerusakanFooter As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icTglKerusakanFooter"), KTB.DNet.WebCC.IntiCalendar)
                Dim txtOdometerFooter As TextBox = CType(e.Item.FindControl("txtOdometerFooter"), TextBox)
                Dim txtCatatanFooter As TextBox = CType(e.Item.FindControl("txtCatatanFooter"), TextBox)

                Dim objChassisFooter As New ChassisMasterBB
                If IsNothing(txtNoRangkaFooter) OrElse txtNoRangkaFooter.Text = String.Empty Then
                    MessageBox.Show("No Rangka masih kosong")
                    RefreshGrid()
                    Return
                End If

                If IsNothing(txtOdometerFooter) OrElse txtOdometerFooter.Text = String.Empty Then
                    MessageBox.Show("Odometer masih kosong")
                    RefreshGrid()
                    Return
                End If


                If Not QRSIsExist(txtNoRangkaFooter.Text) Then
                    MessageBox.Show("No Rangka tidak terdaftar")
                    RefreshGrid()
                    Return
                End If


                objChassisFooter = New ChassisMasterBBFacade(User).Retrieve(txtNoRangkaFooter.Text.Trim())

                If Not QRSIsSameCategory(objChassisFooter, objChassisMain) Then
                    MessageBox.Show("Kategori No Rangka di QRS berbeda dengan No Rangka PQR")
                    RefreshGrid()
                    Return
                End If

                If Not (IsNothing(objChassisFooter) OrElse objChassisFooter.ID = 0) Then
                    If Not QRSIsExist(objChassisFooter.ID, _arrQRS) Then
                        Dim objPQRQRSBB As PQRQRSBB = New PQRQRSBB

                        objPQRQRSBB.ChassisMasterBB = objChassisFooter
                        objPQRQRSBB.TglKerusakan = icTglKerusakanFooter.Value
                        objPQRQRSBB.Odometer = CInt(txtOdometerFooter.Text)
                        objPQRQRSBB.Note = txtCatatanFooter.Text

                        _arrQRS.Add(objPQRQRSBB)
                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_QRS", _arrQRS)
                        Else
                            sessHelper.SetSession("QRS", _arrQRS)
                        End If
                    Else
                        MessageBox.Show(SR.DataIsExist("No Rangka"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("No Rangka"))
                End If


            Case "Edit" 'Edit mode activated
                dgQRS.ShowFooter = False
                btnSimpan.Enabled = False
                dgQRS.EditItemIndex = e.Item.ItemIndex

            Case "Delete" 'Delete this datagrid item 
                If Mode = enumMode.Mode.NewItemMode Then
                    _arrQRS.RemoveAt(e.Item.ItemIndex)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    Dim deletedQRS As PQRQRSBB = CType(_arrQRS(e.Item.ItemIndex), PQRQRSBB)
                    If deletedQRS.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDQRS"), ArrayList)
                        deletedArrLst.Add(deletedQRS)
                        sessHelper.SetSession("DELETEDQRS", deletedArrLst)
                    End If
                    _arrQRS.RemoveAt(e.Item.ItemIndex)
                End If


            Case "Save" 'Update this datagrid item                 

                Dim txtNoRangka As TextBox = CType(e.Item.FindControl("txtNoRangkaEdit"), TextBox)
                Dim icTglKerusakanEdit As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icTglKerusakanEdit"), KTB.DNet.WebCC.IntiCalendar)
                Dim txtOdometerEdit As TextBox = CType(e.Item.FindControl("txtOdometerEdit"), TextBox)
                Dim txtCatatanEdit As TextBox = CType(e.Item.FindControl("txtCatatanEdit"), TextBox)

                Dim objChassis As ChassisMasterBB

                If IsNothing(txtNoRangka) OrElse txtNoRangka.Text = String.Empty Then
                    MessageBox.Show("No Rangka masih kosong")
                    RefreshGrid()
                    Return
                End If

                If Not QRSIsExist(txtNoRangka.Text) Then
                    MessageBox.Show("No Rangka tidak terdaftar")
                    RefreshGrid()
                    Return
                End If

                objChassis = New ChassisMasterBBFacade(User).Retrieve(txtNoRangka.Text.Trim())

                If Not QRSIsSameCategory(objChassis, objChassisMain) Then
                    MessageBox.Show("Kategori No Rangka di QRS berbeda dengan No Rangka PQR")
                    RefreshGrid()
                    Return
                End If

                If Not (IsNothing(objChassis) OrElse objChassis.ID = 0) Then
                    If Not QRSIsExist(objChassis.ID, _arrQRS, e.Item.ItemIndex) Then
                        Dim objPQRQRSBB As PQRQRSBB = CType(_arrQRS(e.Item.ItemIndex), PQRQRSBB)
                        objPQRQRSBB.ChassisMasterBB = objChassis
                        objPQRQRSBB.TglKerusakan = icTglKerusakanEdit.Value
                        objPQRQRSBB.Odometer = CInt(txtOdometerEdit.Text)
                        objPQRQRSBB.Note = txtCatatanEdit.Text

                        _arrQRS(e.Item.ItemIndex) = objPQRQRSBB

                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_QRS", _arrQRS)
                        Else
                            sessHelper.SetSession("QRS", _arrQRS)

                        End If
                        dgQRS.EditItemIndex = -1
                        dgQRS.ShowFooter = True
                        btnSimpan.Enabled = bCekPriv
                    Else
                        MessageBox.Show(SR.DataIsExist("No Rangka"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("No Rangka"))
                End If


            Case "Cancel" 'Cancel Update this datagrid item 
                dgQRS.EditItemIndex = -1
                dgQRS.ShowFooter = True
                btnSimpan.Enabled = bCekPriv
                'Case "Cancel" 'Cancel Update this datagrid item 
                '    dgParts.EditItemIndex = -1
                '    dgParts.ShowFooter = True

        End Select
        RefreshGrid()
    End Sub
    Private Sub BindQRS()
        If Mode = enumMode.Mode.NewItemMode Then
            dgQRS.DataSource = CType(sessHelper.GetSession("NEW_QRS"), ArrayList)
            dgQRS.DataBind()
        Else
            dgQRS.DataSource = CType(sessHelper.GetSession("QRS"), ArrayList)
            dgQRS.DataBind()
        End If

    End Sub
#End Region


#Region "Datagrid Kerusakan"
    Private Sub dgKerusakan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgKerusakan.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            SetdgKerusakanItemFooter(e)
        End If
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            SetdgKerusakanItem(e)
        End If
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetdgKerusakanItemEdit(e)
        End If

    End Sub
    Private Sub SetdgKerusakanItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim vCode As String = Nothing
        If txtNoChasis.Text.Trim <> "" Then
            vCode = CType(New ChassisMasterBBFacade(User).RetrieveByChassisNumbers(txtNoChasis.Text.Trim)(0), ChassisMasterBB).VechileColor.VechileType.VechileTypeCode
        End If
        Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblSearchDamageFooter"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpPositionCodeSelection.aspx?vCode=" & vCode, "", 710, 700, "GetSelectedDamageCode")
    End Sub
    Private Sub SetdgKerusakanItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim vCode As String = Nothing
        If txtNoChasis.Text.Trim <> "" Then
            vCode = CType(New ChassisMasterBBFacade(User).RetrieveByChassisNumbers(txtNoChasis.Text.Trim)(0), ChassisMasterBB).VechileColor.VechileType.VechileTypeCode
        End If
        Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblSearchDamageEdit"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpPositionCodeSelection.aspx?vCode=" & vCode, "", 710, 700, "GetSelectedDamageCode")
    End Sub
    Private Sub SetdgKerusakanItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Add(lNum)
    End Sub
    Private Function DamageCodeIsExist(ByVal CodePositionID As Integer, ByVal DamageCodeCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False

        Try
            If DamageCodeCollection.Count > 0 Then
                For Each _PQRDamageCodeBB As PQRDamageCodeBB In DamageCodeCollection
                    If _PQRDamageCodeBB.DeskripsiKodePosisi.ID = CodePositionID Then
                        bResult = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception

        End Try

        Return bResult
    End Function
    Private Function DamageCodeIsExist(ByVal CodePositionID As Integer, ByVal DamageCodeCollection As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim i As Integer = 0
        Dim bResult As Boolean = False
        Try
            If DamageCodeCollection.Count > 0 Then
                For Each _PQRDamageCodeBB As PQRDamageCodeBB In DamageCodeCollection
                    If _PQRDamageCodeBB.DeskripsiKodePosisi.ID = CodePositionID AndAlso nIndeks <> i Then
                        bResult = True
                        Exit For
                    End If
                    i += 1
                Next
            End If
        Catch ex As Exception

        End Try

        Return bResult
    End Function

    Private Function DamageCodeIsExist(ByVal KodePosisi As String) As Boolean
        Try
            Dim criterias As New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "KodePosition", MatchType.Exact, KodePosisi.Trim))

            Dim tempArr As ArrayList = New DeskripsiPositionCodeFacade(User).Retrieve(criterias)

            If tempArr.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
        End Try


        Return False
    End Function


    Private Sub dgKerusakan_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgKerusakan.ItemCommand
        Dim _arrDamageCode As ArrayList = CType(sessHelper.GetSession("DAMAGECODE"), ArrayList)
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrDamageCode = CType(sessHelper.GetSession("NEW_DAMAGECODE"), ArrayList)
        End If
        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid

                Dim txtDamageCode As TextBox = CType(e.Item.FindControl("txtKodeDamageFooter"), TextBox)
                Dim objKodePosisi As DeskripsiKodePosisi

                If IsNothing(txtDamageCode) OrElse txtDamageCode.Text = String.Empty Then
                    MessageBox.Show("Posisi Kerusakan masih kosong")
                    Return
                    RefreshGrid()
                    Return
                Else
                    If dgKerusakan.Items.Count > 0 Then
                        Dim lblKodeDamage As Label = CType(dgKerusakan.Items(0).FindControl("lblKodeDamage"), Label)
                        If txtDamageCode.Text.Substring(0, 2) <> lblKodeDamage.Text.Substring(0, 2) Then
                            MessageBox.Show("2 huruf pertama Kode Kerusakan harus sama dengan kode yg pertama")
                            RefreshGrid()
                            Return
                        End If
                    End If
                End If


                If Not DamageCodeIsExist(txtDamageCode.Text) Then
                    MessageBox.Show("Kode Posisi tidak terdaftar")
                    RefreshGrid()
                    Return
                End If

                objKodePosisi = New DeskripsiPositionCodeFacade(User).Retrieve(txtDamageCode.Text.Trim())

                If Not (IsNothing(objKodePosisi) OrElse objKodePosisi.ID = 0) Then
                    If Not DamageCodeIsExist(objKodePosisi.ID, _arrDamageCode) Then
                        Dim objPQRDamageCodeBB As PQRDamageCodeBB = New PQRDamageCodeBB

                        objPQRDamageCodeBB.DeskripsiKodePosisi = objKodePosisi

                        _arrDamageCode.Add(objPQRDamageCodeBB)


                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_DAMAGECODE", _arrDamageCode)
                        Else
                            sessHelper.SetSession("DAMAGECODE", _arrDamageCode)

                        End If
                        'Page.RegisterStartupScript("test", "<script language=JavaScript> focusSave(); </script>")
                    Else
                        MessageBox.Show(SR.DataIsExist("Damage Code"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Damage Code"))
                End If


            Case "Edit" 'Edit mode activated
                dgKerusakan.ShowFooter = False
                btnSimpan.Enabled = False
                dgKerusakan.EditItemIndex = e.Item.ItemIndex
            Case "Delete" 'Delete this datagrid item 
                If Mode = enumMode.Mode.NewItemMode Then
                    _arrDamageCode.RemoveAt(e.Item.ItemIndex)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    Dim deletedDamageCode As PQRDamageCodeBB = CType(_arrDamageCode(e.Item.ItemIndex), PQRDamageCodeBB)
                    If deletedDamageCode.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDDAMAGECODE"), ArrayList)
                        deletedArrLst.Add(deletedDamageCode)
                        sessHelper.SetSession("DELETEDDAMAGECODE", deletedArrLst)
                    End If
                    _arrDamageCode.RemoveAt(e.Item.ItemIndex)
                End If


            Case "Save" 'Update this datagrid item                 

                Dim txtDamageCode As TextBox = CType(e.Item.FindControl("txtKodeDamageEdit"), TextBox)
                Dim objKodePosisi As DeskripsiKodePosisi

                If IsNothing(txtDamageCode) OrElse txtDamageCode.Text = String.Empty Then
                    MessageBox.Show("Posisi Kerusakan masih kosong")
                    RefreshGrid()
                    Return
                Else
                    If dgKerusakan.Items.Count > 1 Then
                        If e.Item.ItemIndex > 0 Then
                            Dim lblKodeDamage As Label = CType(dgKerusakan.Items(0).FindControl("lblKodeDamage"), Label)
                            If txtDamageCode.Text.Substring(0, 2) <> lblKodeDamage.Text.Substring(0, 2) Then
                                MessageBox.Show("2 huruf pertama Kode Kerusakan harus sama dengan kode yg pertama")
                                RefreshGrid()
                                Return
                            End If
                        End If
                    End If
                End If

                If Not DamageCodeIsExist(txtDamageCode.Text) Then
                    MessageBox.Show("Kode Posisi tidak terdaftar")
                    RefreshGrid()
                    Return
                End If

                objKodePosisi = New DeskripsiPositionCodeFacade(User).Retrieve(txtDamageCode.Text.Trim())

                If Not (IsNothing(objKodePosisi) OrElse objKodePosisi.ID = 0) Then
                    If Not DamageCodeIsExist(objKodePosisi.ID, _arrDamageCode, e.Item.ItemIndex) Then
                        Dim objPQRDamageCodeBB As PQRDamageCodeBB = CType(_arrDamageCode(e.Item.ItemIndex), PQRDamageCodeBB)
                        objPQRDamageCodeBB.DeskripsiKodePosisi = objKodePosisi

                        _arrDamageCode(e.Item.ItemIndex) = objPQRDamageCodeBB
                        '_arrDamageCode.Insert(e.Item.ItemIndex, objPQRDamageCodeBB)
                        '_arrDamageCode.Add(objPQRDamageCodeBB)

                        sessHelper.SetSession("DAMAGECODE", _arrDamageCode)
                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_DAMAGECODE", _arrDamageCode)
                        End If
                        dgKerusakan.EditItemIndex = -1
                        dgKerusakan.ShowFooter = True
                        btnSimpan.Enabled = bCekPriv

                        'Page.RegisterStartupScript("test", "<script language=JavaScript> focusSave(); </script>")
                    Else
                        MessageBox.Show(SR.DataIsExist("Damage Code"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Damage Code"))
                End If


            Case "Cancel" 'Cancel Update this datagrid item 
                dgKerusakan.EditItemIndex = -1
                dgKerusakan.ShowFooter = True
                btnSimpan.Enabled = bCekPriv
                'Case "Cancel" 'Cancel Update this datagrid item 
                '    dgParts.EditItemIndex = -1
                '    dgParts.ShowFooter = True

        End Select
        RefreshGrid()
    End Sub
    Private Sub BindDamageCode()
        If Mode = enumMode.Mode.NewItemMode Then
            dgKerusakan.DataSource = CType(sessHelper.GetSession("NEW_DAMAGECODE"), ArrayList)
            dgKerusakan.DataBind()
        Else
            dgKerusakan.DataSource = CType(sessHelper.GetSession("DAMAGECODE"), ArrayList)
            dgKerusakan.DataBind()

        End If

    End Sub
#End Region

#Region "Datagrid Parts"
    Private Sub dgParts_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgParts.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            SetdgPartsItemFooter(e)
        End If
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            SetdgPartsItem(e)
        End If
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetdgPartsItemEdit(e)
        End If

    End Sub
    Private Sub SetdgPartsItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblSearchPartsFooter"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpPartsCodeSelection.aspx", "", 710, 700, "GetSelectedPartsCode")
    End Sub
    Private Sub SetdgPartsItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        'Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        'e.Item.Cells(0).Controls.Clear()
        'e.Item.Cells(0).Controls.Add(lNum)
        Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblSearchPartsEdit"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpPartsCodeSelection.aspx", "", 710, 700, "GetSelectedPartsCode")
    End Sub
    Private Sub SetdgPartsItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Add(lNum)

    End Sub
    Private Function PartsCodeIsExist(ByVal SparePartID As Integer, ByVal PartsCodeCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If PartsCodeCollection.Count > 0 Then
            For Each _PQRPartsCodeBB As PQRPartsCodeBB In PartsCodeCollection
                If _PQRPartsCodeBB.SparePartMaster.ID = SparePartID Then
                    bResult = True
                    Exit For
                End If
            Next
        End If
        Return bResult
    End Function
    Private Function PartsCodeIsExist(ByVal SparePartID As Integer, ByVal PartsCodeCollection As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim i As Integer = 0
        Dim bResult As Boolean = False

        If PartsCodeCollection.Count > 0 Then
            For Each _PQRPartsCodeBB As PQRPartsCodeBB In PartsCodeCollection
                If _PQRPartsCodeBB.SparePartMaster.ID = SparePartID AndAlso nIndeks <> i Then
                    bResult = True
                    Exit For
                End If
                i += 1
            Next
        End If
        Return bResult
    End Function
    Private Function PartsCodeIsExist(ByVal SparePartCode As String) As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, SparePartCode.Trim))

        Dim tempArr As ArrayList = New SparePartMasterFacade(User).Retrieve(criterias)

        If tempArr.Count > 0 Then
            Return True
        End If

        Return False
    End Function

    Private Sub dgParts_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgParts.ItemCommand
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        Dim _arrPartsCode As ArrayList = CType(sessHelper.GetSession("PARTS"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrPartsCode = CType(sessHelper.GetSession("NEW_PARTS"), ArrayList)
        End If
        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid

                Dim txtPartsCode As TextBox = CType(e.Item.FindControl("txtKodePartsFooter"), TextBox)
                Dim objSparePartMaster As SparePartMaster

                If IsNothing(txtPartsCode) OrElse txtPartsCode.Text = String.Empty Then
                    MessageBox.Show("Posisi Part masih kosong")
                    RefreshGrid()
                    Return
                End If

                If Not PartsCodeIsExist(txtPartsCode.Text) Then
                    MessageBox.Show("Kode Part tidak terdaftar")
                    RefreshGrid()
                    Return
                End If

                objSparePartMaster = New SparePartMasterFacade(User).Retrieve(txtPartsCode.Text.Trim())

                If Not (IsNothing(objSparePartMaster) OrElse objSparePartMaster.ID = 0) Then
                    If Not PartsCodeIsExist(objSparePartMaster.ID, _arrPartsCode) Then
                        Dim objPQRPartsCodeBB As PQRPartsCodeBB = New PQRPartsCodeBB

                        objPQRPartsCodeBB.SparePartMaster = objSparePartMaster

                        _arrPartsCode.Add(objPQRPartsCodeBB)

                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_PARTS", _arrPartsCode)
                        Else
                            sessHelper.SetSession("PARTS", _arrPartsCode)
                        End If
                        'Page.RegisterStartupScript("test", "<script language=JavaScript> focusSave(); </script>")
                    Else
                        MessageBox.Show(SR.DataIsExist("Parts Code"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Parts Code"))
                End If


            Case "Edit" 'Edit mode activated
                dgParts.ShowFooter = False
                btnSimpan.Enabled = False
                dgParts.EditItemIndex = e.Item.ItemIndex
            Case "Delete" 'Delete this datagrid item 
                If Mode = enumMode.Mode.NewItemMode Then
                    _arrPartsCode.RemoveAt(e.Item.ItemIndex)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    Dim deletedPartsCode As PQRPartsCodeBB = CType(_arrPartsCode(e.Item.ItemIndex), PQRPartsCodeBB)
                    If deletedPartsCode.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDPARTS"), ArrayList)
                        deletedArrLst.Add(deletedPartsCode)
                        sessHelper.SetSession("DELETEDPARTS", deletedArrLst)
                    End If
                    _arrPartsCode.RemoveAt(e.Item.ItemIndex)
                End If
            Case "Save" 'Update this datagrid item                 

                Dim txtPartsCode As TextBox = CType(e.Item.FindControl("txtKodePartsEdit"), TextBox)
                Dim objSparePartMaster As SparePartMaster

                If IsNothing(txtPartsCode) OrElse txtPartsCode.Text = String.Empty Then
                    MessageBox.Show("Posisi Part masih kosong")
                    RefreshGrid()
                    Return
                End If

                If Not PartsCodeIsExist(txtPartsCode.Text) Then
                    MessageBox.Show("Kode Part tidak terdaftar")
                    RefreshGrid()
                    Return
                End If

                objSparePartMaster = New SparePartMasterFacade(User).Retrieve(txtPartsCode.Text.Trim())

                If Not (IsNothing(objSparePartMaster) OrElse objSparePartMaster.ID = 0) Then
                    If Not PartsCodeIsExist(objSparePartMaster.ID, _arrPartsCode, e.Item.ItemIndex) Then
                        Dim objPQRPartsCodeBB As PQRPartsCodeBB = CType(_arrPartsCode(e.Item.ItemIndex), PQRPartsCodeBB)

                        objPQRPartsCodeBB.SparePartMaster = objSparePartMaster

                        _arrPartsCode(e.Item.ItemIndex) = objPQRPartsCodeBB

                        sessHelper.SetSession("PARTS", _arrPartsCode)
                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_PARTS", _arrPartsCode)
                        End If
                        dgParts.EditItemIndex = -1
                        dgParts.ShowFooter = True
                        btnSimpan.Enabled = bCekPriv

                        'Page.RegisterStartupScript("test", "<script language=JavaScript> focusSave(); </script>")
                    Else
                        MessageBox.Show(SR.DataIsExist("Parts Code"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Parts Code"))
                End If


            Case "Cancel" 'Cancel Update this datagrid item 
                dgParts.EditItemIndex = -1
                dgParts.ShowFooter = True
                btnSimpan.Enabled = bCekPriv
                'Case "Cancel" 'Cancel Update this datagrid item 
                '    dgParts.EditItemIndex = -1
                '    dgParts.ShowFooter = True

        End Select
        RefreshGrid()
    End Sub
    Private Sub BindParts()
        If Mode = enumMode.Mode.NewItemMode Then
            dgParts.DataSource = CType(sessHelper.GetSession("NEW_PARTS"), ArrayList)
            dgParts.DataBind()
        Else
            dgParts.DataSource = CType(sessHelper.GetSession("PARTS"), ArrayList)
            dgParts.DataBind()
        End If

    End Sub
#End Region

#Region "Datagrid Attachment Top"
    Private Sub dgFileAttachmentTop_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgFileAttachmentTop.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)

            'Dim lnkbtnFileAttachmentTop As LinkButton = CType(e.Item.FindControl("lnkbtnFileAttachmentTop"), LinkButton)
            'AddHandler lnkbtnFileAttachmentTop.Click, AddressOf Attachment_Click
        End If
    End Sub
    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each _PQRAttachmentBB As PQRAttachmentBB In AttachmentCollection
                If _PQRAttachmentBB.FileName = FileName Then
                    bResult = True
                    Exit For
                End If
            Next
        End If
        Return bResult
    End Function
    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim i As Integer = 0
        Dim bResult As Boolean = False

        If AttachmentCollection.Count > 0 Then
            For Each _PQRAttachmentBB As PQRAttachmentBB In AttachmentCollection
                If _PQRAttachmentBB.FileName = FileName AndAlso nIndeks <> i Then
                    bResult = True
                    Exit For
                End If
                i += 1
            Next
        End If
        Return bResult
    End Function
    Private Sub dgFileAttachmentTop_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgFileAttachmentTop.ItemCommand
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        Dim _arrAttachmentTop As ArrayList = CType(sessHelper.GetSession("ATTACHMENTTOP"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrAttachmentTop = CType(sessHelper.GetSession("NEW_ATTACHMENTTOP"), ArrayList)
        End If
        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid

                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("iFileAttachmentTop"), HtmlInputFile)
                Dim objPostedData As HttpPostedFile
                Dim sFileName As String

                If IsNothing(FileUpload) OrElse FileUpload.Value = String.Empty Then
                    MessageBox.Show("Lampiran masih kosong")
                    RefreshGrid()
                    Return
                Else
                    objPostedData = FileUpload.PostedFile
                End If



                If Not (IsNothing(objPostedData)) Then
                    sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                    If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                        MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                        RefreshGrid()
                        Return
                    End If

                    If Not FileIsExist(sFileName, _arrAttachmentTop) Then

                        Dim objPQRAttachmentBBTop As PQRAttachmentBB = New PQRAttachmentBB
                        Dim FileName As String

                        objPQRAttachmentBBTop.NewItem = True
                        objPQRAttachmentBBTop.Type = EnumPQR.AttachmentLocation.Top
                        objPQRAttachmentBBTop.AttachmentType = objPostedData.ContentType
                        objPQRAttachmentBBTop.AttachmentData = objPostedData
                        FileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)
                        objPQRAttachmentBBTop.Attachment = AttachmentDirectory & "\" & DateTime.Today.ToString("dd-MM-yyyy") & "\" & DateTime.Now.ToString("ddMMyyyyHHmmss_") & objPQRAttachmentBBTop.Type.ToString & "_" & FileName

                        UploadAttachment(objPQRAttachmentBBTop, TempDirectory)

                        _arrAttachmentTop.Add(objPQRAttachmentBBTop)

                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_ATTACHMENTTOP", _arrAttachmentTop)
                        Else
                            sessHelper.SetSession("ATTACHMENTTOP", _arrAttachmentTop)

                        End If
                        'Page.RegisterStartupScript("test", "<script language=JavaScript> focusSave(); </script>")
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If


            Case "Delete" 'Delete this datagrid item 
                If Mode = enumMode.Mode.NewItemMode Then
                    RemovePQRAttachmentBB(CType(_arrAttachmentTop(e.Item.ItemIndex), PQRAttachmentBB), TempDirectory)
                    _arrAttachmentTop.RemoveAt(e.Item.ItemIndex)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    Dim deletedAttachment As PQRAttachmentBB = CType(_arrAttachmentTop(e.Item.ItemIndex), PQRAttachmentBB)
                    If deletedAttachment.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDATTACHMENT"), ArrayList)
                        deletedArrLst.Add(deletedAttachment)
                        sessHelper.SetSession("DELETEDATTACHMENT", deletedArrLst)
                    End If
                    _arrAttachmentTop.RemoveAt(e.Item.ItemIndex)
                End If

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)

        End Select
        RefreshGrid()
    End Sub
    Private Sub BindAttachmentTop()
        If Mode = enumMode.Mode.NewItemMode Then
            dgFileAttachmentTop.DataSource = CType(sessHelper.GetSession("NEW_ATTACHMENTTOP"), ArrayList)
            dgFileAttachmentTop.DataBind()
        Else
            dgFileAttachmentTop.DataSource = CType(sessHelper.GetSession("ATTACHMENTTOP"), ArrayList)
            dgFileAttachmentTop.DataBind()
        End If

    End Sub
#End Region

#Region "Datagrid Attachment Bottom"
    Private Sub dgFileAttachmentBottom_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgFileAttachmentBottom.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)

            'Dim lnkbtnFileAttachmentBottom As LinkButton = CType(e.Item.FindControl("lnkbtnFileAttachmentBottom"), LinkButton)
            'AddHandler lnkbtnFileAttachmentBottom.Click, AddressOf Attachment_Click
        End If
    End Sub
    Private Sub dgFileAttachmentBottom_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgFileAttachmentBottom.ItemCommand
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        Dim _arrAttachmentBottom As ArrayList = CType(sessHelper.GetSession("ATTACHMENTBOTTOM"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrAttachmentBottom = CType(sessHelper.GetSession("NEW_ATTACHMENTBOTTOM"), ArrayList)
        End If
        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid

                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("iFileAttachmentBottom"), HtmlInputFile)
                Dim objPostedData As HttpPostedFile
                Dim sFileName As String

                If IsNothing(FileUpload) OrElse FileUpload.Value = String.Empty Then
                    MessageBox.Show("Lampiran masih kosong")
                    RefreshGrid()
                    Return
                Else
                    objPostedData = FileUpload.PostedFile
                End If

                If Not (IsNothing(objPostedData)) Then
                    sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)
                    If Not FileIsExist(sFileName, _arrAttachmentBottom) Then

                        Dim objPQRAttachmentBBBottom As PQRAttachmentBB = New PQRAttachmentBB
                        Dim FileName As String

                        objPQRAttachmentBBBottom.NewItem = True
                        objPQRAttachmentBBBottom.Type = EnumPQR.AttachmentLocation.Bottom
                        objPQRAttachmentBBBottom.AttachmentType = objPostedData.ContentType
                        objPQRAttachmentBBBottom.AttachmentData = objPostedData
                        FileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)
                        objPQRAttachmentBBBottom.Attachment = AttachmentDirectory & "\" & DateTime.Today.ToString("dd-MM-yyyy") & "\" & DateTime.Now.ToString("ddMMyyyyHHmmss_") & objPQRAttachmentBBBottom.Type.ToString & "_" & FileName

                        UploadAttachment(objPQRAttachmentBBBottom, TempDirectory)

                        _arrAttachmentBottom.Add(objPQRAttachmentBBBottom)

                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_ATTACHMENTBOTTOM", _arrAttachmentBottom)
                        Else
                            sessHelper.SetSession("ATTACHMENTBOTTOM", _arrAttachmentBottom)

                        End If
                        'Page.RegisterStartupScript("test", "<script language=JavaScript> focusSave(); </script>")
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If


            Case "Delete" 'Delete this datagrid item 
                If Mode = enumMode.Mode.NewItemMode Then
                    RemovePQRAttachmentBB(CType(_arrAttachmentBottom(e.Item.ItemIndex), PQRAttachmentBB), TempDirectory)
                    _arrAttachmentBottom.RemoveAt(e.Item.ItemIndex)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    Dim deletedAttachment As PQRAttachmentBB = CType(_arrAttachmentBottom(e.Item.ItemIndex), PQRAttachmentBB)
                    If deletedAttachment.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDATTACHMENT"), ArrayList)
                        deletedArrLst.Add(deletedAttachment)
                        sessHelper.SetSession("DELETEDATTACHMENT", deletedArrLst)
                    End If
                    _arrAttachmentBottom.RemoveAt(e.Item.ItemIndex)
                End If

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)

        End Select
        RefreshGrid()
    End Sub
    Private Sub BindAttachmentBottom()
        If Mode = enumMode.Mode.NewItemMode Then
            dgFileAttachmentBottom.DataSource = CType(sessHelper.GetSession("NEW_ATTACHMENTBOTTOM"), ArrayList)
            dgFileAttachmentBottom.DataBind()
        Else
            dgFileAttachmentBottom.DataSource = CType(sessHelper.GetSession("ATTACHMENTBOTTOM"), ArrayList)
            dgFileAttachmentBottom.DataBind()
        End If

    End Sub
#End Region

    'Private Sub Attachment_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim objSender As LinkButton = CType(sender, LinkButton)
    '    Response.Redirect("../Download.aspx?file=" & objSender.CommandArgument)
    'End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim result As Integer
        Dim ErrMessage As String = String.Empty

        If Not Page.IsValid Then
            Return
        End If

        If ValidateSaveData() Then
            If oChassisFacade.IsExist(txtNoChasis.Text) Then
                oChassis = oChassisFacade.Retrieve(txtNoChasis.Text)
            Else
                MessageBox.Show("Nomor Rangka tidak terdaftar.")
            End If

            For Each item As DataGridItem In dgKerusakan.Items

            Next

            BindPQRHeaderBBDomain()
            If Mode = enumMode.Mode.NewItemMode Then
                oPQRHeaderBB = sessHelper.GetSession("NEW_PQR")
            End If
            If oPQRHeaderBB.PQRQRSBBs.Count > 0 Then
                For Each item As PQRQRSBB In oPQRHeaderBB.PQRQRSBBs
                    If Not QRSIsSameCategory(item.ChassisMasterBB, oPQRHeaderBB.ChassisMasterBB) Then
                        MessageBox.Show("Kategori No Rangka di QRS berbeda dengan No Rangka PQR")
                        Return
                    End If
                Next
            End If

            Dim objRenderPanel As RenderingProfile = New RenderingProfile
            Dim ProfileCollection1 As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("pqr_prf"), CType(EnumProfileType.ProfileType.PQRBB, Short), User)
            Dim ProfileCollection2 As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), CType(EnumProfileType.ProfileType.PQRBB, Short), User)

            Mode = CType(ViewState("Mode"), enumMode.Mode)
            If Mode = enumMode.Mode.NewItemMode Then 'o2n
                result = oPQRHeaderBBFacade.InsertTransaction(oPQRHeaderBB, CType(sessHelper.GetSession("NEW_DAMAGECODE"), ArrayList), CType(sessHelper.GetSession("NEW_PARTS"), ArrayList), CType(sessHelper.GetSession("NEW_ATTACHMENTTOP"), ArrayList), CType(sessHelper.GetSession("NEW_ATTACHMENTBOTTOM"), ArrayList), ProfileCollection1, ProfileCollection2, CType(sessHelper.GetSession("NEW_QRS"), ArrayList))
            ElseIf Mode = enumMode.Mode.EditMode Then
                result = oPQRHeaderBBFacade.UpdateTransaction(oPQRHeaderBB, CType(sessHelper.GetSession("DAMAGECODE"), ArrayList), CType(sessHelper.GetSession("DELETEDDAMAGECODE"), ArrayList), CType(sessHelper.GetSession("PARTS"), ArrayList), CType(sessHelper.GetSession("DELETEDPARTS"), ArrayList), CType(sessHelper.GetSession("ATTACHMENTTOP"), ArrayList), CType(sessHelper.GetSession("ATTACHMENTBOTTOM"), ArrayList), CType(sessHelper.GetSession("DELETEDATTACHMENT"), ArrayList), ProfileCollection1, ProfileCollection2, New ProfileGroupFacade(User).Retrieve("pqr_prf"), New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), CType(sessHelper.GetSession("QRS"), ArrayList), CType(sessHelper.GetSession("DELETEDQRS"), ArrayList), ErrMessage)
            End If


            If result > 0 Then
                ' Proses Upload di pisahkan dari simpan 
                'UploadAttachment(CType(sessHelper.GetSession("ATTACHMENTBOTTOM"), ArrayList), TargetDirectory)
                'UploadAttachment(CType(sessHelper.GetSession("ATTACHMENTTOP"), ArrayList), TargetDirectory)
                If Mode = enumMode.Mode.NewItemMode Then
                    CommitAttachment(CType(sessHelper.GetSession("NEW_ATTACHMENTTOP"), ArrayList))
                    CommitAttachment(CType(sessHelper.GetSession("NEW_ATTACHMENTBOTTOM"), ArrayList))

                Else
                    CommitAttachment(CType(sessHelper.GetSession("ATTACHMENTTOP"), ArrayList))
                    CommitAttachment(CType(sessHelper.GetSession("ATTACHMENTBOTTOM"), ArrayList))

                End If
                If Mode = enumMode.Mode.EditMode Then
                    RemovePQRAttachmentBB(CType(sessHelper.GetSession("DELETEDATTACHMENT"), ArrayList), TargetDirectory)
                End If
                ClearTempData()

                If (ddlPqrType.SelectedIndex = 2) Then
                    MessageBox.Show("Simpan Data Berhasil!")
                    ReloadForm(result)
                    'ReloadForm(result)
                    Server.Transfer("~/PQR/FrmPQRList.aspx")
                Else
                    'MessageBox.Show("Simpan Data Berhasil!")
                    'ShowConfirmation(True)
                    'Add by Reza
                    Dim _criterias As WSCHeaderBB
                    Dim _criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeaderBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim _PQRNo As String = genPQRNo(oPQRHeaderBB.ID)
                    _criteria.opAnd(New Criteria(GetType(WSCHeaderBB), "PQR", MatchType.Exact, _PQRNo))
                    Dim PQRF As ArrayList = New WSCHeaderBBFacade(User).Retrieve(_criteria)
                    If ddlPqrType.SelectedIndex = 1 Then
                        If PQRF.Count = 0 Then
                            sessHelper.SetSession("inputWSCSes", oPQRHeaderBB)
                            'ShowConfirmation(True)
                            ValidatePqrWSC()
                        Else
                            For Each asd As WSCHeaderBB In PQRF
                                If asd.PQR = oPQRHeaderBB.PQRNo Then
                                    'ShowConfirmation(False)
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                    'End
                End If

                'MessageBox.Show("Simpan Data Berhasil!")
                'Server.Transfer("~/PQR/FrmPQRList.aspx")
            Else
                If ErrMessage = String.Empty Then
                    MessageBox.Show("Simpan Data Gagal!")
                Else
                    MessageBox.Show(ErrMessage)
                End If

            End If

        Else
            'MessageBox.Show("Data tidak lengkap. Silakan diperiksa lagi ")
        End If


    End Sub
    Private Sub ReloadForm(ByVal id As Integer)
        sessHelper.SetSession("PQR", oPQRHeaderBBFacade.Retrieve(id))
        oPQRHeaderBB = CType(sessHelper.GetSession("PQR"), PQRHeaderBB)

        'sessHelper.SetSession("DAMAGECODE", oPQRHeaderBB.PQRDamageCodeBBs)
        'sessHelper.SetSession("DELETEDDAMAGECODE", New ArrayList)

        'sessHelper.SetSession("PARTS", oPQRHeaderBB.PQRPartsCodeBBs)
        'sessHelper.SetSession("DELETEDPARTS", New ArrayList)

        'sessHelper.SetSession("ATTACHMENTTOP", GetAttachmentList(oPQRHeaderBB.PQRAttachmentBBs, EnumPQR.AttachmentLocation.Top))
        'sessHelper.SetSession("ATTACHMENTBOTTOM", GetAttachmentList(oPQRHeaderBB.PQRAttachmentBBs, EnumPQR.AttachmentLocation.Bottom))
        'sessHelper.SetSession("DELETEDATTACHMENT", New ArrayList)

        If oDealer.Title = 0 Then 'Dealer
            Select Case CType(oPQRHeaderBB.RowStatus, EnumPQR.PQRStatus)
                Case EnumPQR.PQRStatus.Batal
                    ViewState("Mode") = enumMode.Mode.ViewMode
                Case EnumPQR.PQRStatus.Baru
                    ViewState("Mode") = enumMode.Mode.EditMode
                Case EnumPQR.PQRStatus.Validasi
                    ViewState("Mode") = enumMode.Mode.ViewMode
                Case EnumPQR.PQRStatus.Proses
                    ViewState("Mode") = enumMode.Mode.ViewMode
                Case EnumPQR.PQRStatus.Rilis
                    ViewState("Mode") = enumMode.Mode.ViewMode
                Case EnumPQR.PQRStatus.Selesai
                    ViewState("Mode") = enumMode.Mode.ViewMode
            End Select
        ElseIf oDealer.Title = 1 Then ' KTB
            Select Case CType(oPQRHeaderBB.RowStatus, EnumPQR.PQRStatus)
                Case EnumPQR.PQRStatus.Batal
                    ViewState("Mode") = enumMode.Mode.ViewMode
                Case EnumPQR.PQRStatus.Baru
                    If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                        ViewState("Mode") = enumMode.Mode.EditMode
                    Else
                        ViewState("Mode") = enumMode.Mode.ViewMode
                    End If
                Case EnumPQR.PQRStatus.Validasi
                    ViewState("Mode") = enumMode.Mode.ViewMode
                Case EnumPQR.PQRStatus.Proses
                    If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                        ViewState("Mode") = enumMode.Mode.ViewMode
                    Else
                        ViewState("Mode") = enumMode.Mode.EditMode
                    End If
                Case EnumPQR.PQRStatus.Rilis
                    If SecurityProvider.Authorize(context.User, SR.PQRNewSave_Privilege) Then
                        ViewState("Mode") = enumMode.Mode.ViewMode
                    Else
                        ViewState("Mode") = enumMode.Mode.EditMode
                    End If
                Case EnumPQR.PQRStatus.Selesai
                    ViewState("Mode") = enumMode.Mode.ViewMode
            End Select
        End If


        If CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.EditMode Then
            Server.Transfer("~/PQR/FrmPQRHeaderBB.aspx?Mode=Edit")
        ElseIf CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.ViewMode Then
            Server.Transfer("~/PQR/FrmPQRHeaderBB.aspx?Mode=View")
        End If
        'fillForm()
        'ShowConfirmation(True)
    End Sub
    Private Sub RemovePQRAttachmentBB(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As PQRAttachmentBB In AttachmentCollection
                    finfo = New FileInfo(TargetPath + obj.Attachment)
                    If finfo.Exists Then
                        finfo.Delete()
                    End If
                Next
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub RemovePQRAttachmentBB(ByVal ObjAttachment As PQRAttachmentBB, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(TargetPath + ObjAttachment.Attachment)
                If finfo.Exists Then
                    finfo.Delete()
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub UploadAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As PQRAttachmentBB In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Then
                        finfo = New FileInfo(TargetPath + obj.Attachment)

                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        obj.AttachmentData.SaveAs(TargetPath + obj.Attachment)
                    End If
                Next
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try


    End Sub
    Private Sub UploadAttachment(ByVal ObjAttachment As PQRAttachmentBB, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(ObjAttachment.AttachmentData) Then
                    finfo = New FileInfo(TargetPath + ObjAttachment.Attachment)

                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.Attachment)
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    'TODO
    Private Sub CommitAttachment(ByVal AttachmentCollection As ArrayList)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim TargetFInfo As FileInfo
        Dim TempFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As PQRAttachmentBB In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Then
                        TargetFInfo = New FileInfo(TargetDirectory + obj.Attachment)
                        TempFInfo = New FileInfo(TempDirectory + obj.Attachment)

                        If TempFInfo.Exists Then
                            If Not TargetFInfo.Directory.Exists Then
                                Directory.CreateDirectory(TargetFInfo.DirectoryName)
                            End If
                            TempFInfo.MoveTo(TargetFInfo.FullName)
                        End If
                        obj.AttachmentData.SaveAs(TargetDirectory + obj.Attachment)
                    End If
                Next

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub ClearTempData()
        'Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        'Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        'Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        'Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        'Dim success As Boolean = False

        'Try
        '    success = imp.Start()
        '    If success Then
        '        Dim dir As New DirectoryInfo(TempDirectory)
        '        dir.Delete(True)
        '    End If
        'Catch ex As Exception
        '    'Throw ex
        'End Try
    End Sub

    Private Sub BindPQRHeaderBBDomain()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        oPQRHeaderBB = CType(sessHelper.GetSession("PQR"), PQRHeaderBB)
        If Mode = enumMode.Mode.NewItemMode Then
            oPQRHeaderBB = CType(sessHelper.GetSession("NEW_PQR"), PQRHeaderBB)
            oPQRHeaderBB.Dealer = oDealer
            If oDealer.ID = 2 Then 'KTB STS
                oPQRHeaderBB.PQRNo = "KTBSTS" & ";" & oChassis.Category.CategoryCode
            Else
                oPQRHeaderBB.PQRNo = oDealer.DealerCode & ";" & oChassis.Category.CategoryCode
            End If

        ElseIf Mode = enumMode.Mode.EditMode Then
            oPQRHeaderBB.PQRNo = lblPQRNoVal.Text
            'oPQRHeaderBB.Dealer = oDealer 'lblDealer.Text.Trim.Split("-")(0)
        End If
        oPQRHeaderBB.PQRType = ddlPqrType.SelectedIndex - 1
        oPQRHeaderBB.RefPQRNo = txtRefPQRNo.Text
        oPQRHeaderBB.Category = oChassis.Category
        'oPQRHeaderBB.DocumentDate = DateTime.Today
        oPQRHeaderBB.ChassisMasterBB = oChassis
        oPQRHeaderBB.Year = IIf(oChassis.ProductionYear <= 0, 0, oChassis.ProductionYear)
        oPQRHeaderBB.PQRDate = icTglKerusakan.Value
        If Mode = enumMode.Mode.NewItemMode Then
            oPQRHeaderBB.DocumentDate = Date.Now
        End If
        oPQRHeaderBB.OdoMeter = CInt(txtOdometer.Text)
        oPQRHeaderBB.Velocity = CInt(txtKecepatan.Text)
        If oChassis.EndCustomer Is Nothing Then
            oPQRHeaderBB.CustomerName = String.Empty
            oPQRHeaderBB.CustomerAddress = String.Empty
            oPQRHeaderBB.SoldDate = New DateTime(1753, 1, 1)
        Else
            If Not oChassis.EndCustomer Is Nothing Then
                oPQRHeaderBB.SoldDate = oChassis.EndCustomer.OpenFakturDate
                If Not oChassis.EndCustomer.Customer Is Nothing Then
                    oPQRHeaderBB.CustomerName = oChassis.EndCustomer.Customer.Name1
                    oPQRHeaderBB.CustomerAddress = oChassis.EndCustomer.Customer.Alamat
                End If
            End If
        End If
        oPQRHeaderBB.Subject = txtSubject.Text
        oPQRHeaderBB.Symptomps = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtGejala.Text.Trim)
        oPQRHeaderBB.Causes = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtPenyebab.Text.Trim)
        oPQRHeaderBB.Results = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtHasil.Text.Trim)
        oPQRHeaderBB.Notes = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtCatatan.Text.Trim)
        oPQRHeaderBB.Solutions = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtSolution.Text)

	If Not IsNothing(txtWONumber.Text) AndAlso txtWONumber.Text <> "" Then
            oPQRHeaderBB.WorkOrderNumber = txtWONumber.Text
        End If


        ' Add Branch
        If Not IsNothing(txtDealerBranchCode.Text) AndAlso txtDealerBranchCode.Text <> "" Then
            Dim branchCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            branchCriteria.opAnd(New Criteria(GetType(DealerBranch), "DealerBranchCode", MatchType.Exact, txtDealerBranchCode.Text))
            Dim branchList As ArrayList = New DealerBranchFacade(HttpContext.Current.User).Retrieve(branchCriteria)
            If branchList.Count = 1 Then
                ' Dealer Branch Found
                oPQRHeaderBB.DealerBranch = branchList(0)
            End If
        End If
        If ddlBobot.Visible = True Then
            oPQRHeaderBB.Bobot = ddlBobot.SelectedValue
        Else
            oPQRHeaderBB.Bobot = 0
        End If
        If tblKodeKerusakan.Visible And ddlKodeWSCA.Enabled Then
            If Me.ddlKodeWSCA.SelectedValue <> "-1" Then
                oPQRHeaderBB.CodeA = Me.ddlKodeWSCA.SelectedValue
            End If
            If Me.ddlKodeWSCB.SelectedValue <> "-1" Then
                oPQRHeaderBB.CodeB = Me.ddlKodeWSCB.SelectedValue
            End If
            If Me.ddlKodeWSCC.SelectedValue <> "-1" Then
                oPQRHeaderBB.CodeC = Me.ddlKodeWSCC.SelectedValue
            End If
        End If

        oPQRHeaderBB.PQRType = ddlPqrType.SelectedIndex - 1
        If Mode = enumMode.Mode.NewItemMode Then
            sessHelper.SetSession("NEW_PQR", oPQRHeaderBB)

        End If

    End Sub


    Private Function ValidateSaveData() As Boolean

        'If txtRefPQRNo.Text = String.Empty Then
        '    MessageBox.Show("Silakan masukan no PQR Referensi ")
        '    Return False
        'End If

        If txtNoChasis.Text = String.Empty Then
            MessageBox.Show("No Rangka masih kosong")
            Return False
        End If

        If oChassisFacade.IsExist(txtNoChasis.Text) Then
            txtNoChasis.ForeColor = Color.Black
            LoadChassisInfo(CType(oChassisFacade.Retrieve(txtNoChasis.Text), ChassisMasterBB))
        Else
            txtNoChasis.ForeColor = Color.Red
            ClearChassisInfo()
            MessageBox.Show("No Rangka tidak terdaftar")
            Return False
        End If


        If lblNoMesin.Text = String.Empty Then
            MessageBox.Show("No Rangka tidak terdaftar")
            Return False
        End If

        If txtOdometer.Text = String.Empty Then
            MessageBox.Show("Odometer masih kosong")
            Return False
        End If

        If txtKecepatan.Text = String.Empty Then
            MessageBox.Show("Kecepatan masih kosong")
            Return False
        End If

        If txtSubject.Text = String.Empty Then
            MessageBox.Show("Subject masih kosong")
            Return False
        End If

        If txtGejala.Text = String.Empty Then
            MessageBox.Show("Gejala masih kosong")
            Return False
        End If

        If txtPenyebab.Text = String.Empty Then
            MessageBox.Show("Penyebab masih kosong")
            Return False
        End If

        If ddlBobot.Visible = True Then
            If ddlBobot.SelectedIndex = 0 Then
                MessageBox.Show("Silakan Pilih Bobot.")
                Return False
            End If
        End If

        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If tblKodeKerusakan.Visible And ddlKodeWSCA.Enabled Then
                If ddlKodeWSCA.SelectedIndex < 1 Then
                    MessageBox.Show("Silahkan isi kode kerusakan terlebih dahulu ")
                    Return False
                End If
                If ddlKodeWSCB.SelectedIndex < 1 Then
                    MessageBox.Show("Silahkan isi kode kerusakan terlebih dahulu ")
                    Return False
                End If
                If ddlKodeWSCC.SelectedIndex < 1 Then
                    MessageBox.Show("Silahkan isi kode kerusakan terlebih dahulu ")
                    Return False
                End If

                If dgKerusakan.Items.Count < 1 Then
                    MessageBox.Show("Posisi kerusakan harus diisi")
                    Return False
                End If
            End If
            Mode = CType(ViewState("Mode"), enumMode.Mode)
            If Mode = enumMode.Mode.NewItemMode Then
                Dim arrKerusakan As ArrayList = CType(sessHelper.GetSession("NEW_DAMAGECODE"), ArrayList)
                If arrKerusakan.Count = 0 Then
                    MessageBox.Show("Posisi kerusakan harus diisi")
                    Return False
                End If
            Else
                Dim arrKerusakan As ArrayList = CType(sessHelper.GetSession("DAMAGECODE"), ArrayList)
                If arrKerusakan.Count = 0 Then
                    MessageBox.Show("Posisi kerusakan harus diisi")
                    Return False
                End If
            End If

        Else
            Mode = CType(ViewState("Mode"), enumMode.Mode)
            If Mode = enumMode.Mode.NewItemMode Then
                Dim arrAttachment As ArrayList = CType(sessHelper.GetSession("NEW_ATTACHMENTTOP"), ArrayList)
                If arrAttachment.Count = 0 Then
                    MessageBox.Show("Lampiran harus diisi")
                    Return False
                End If
            End If

        End If


        'If txtHasil.Text = String.Empty Then
        '    MessageBox.Show("Silakan masukan Hasil ")
        '    Return False
        'End If

        'If txtCatatan.Text = String.Empty Then
        '    MessageBox.Show("Silakan masukan catatan ")
        '    Return False
        'End If

        'If txtSolution.Text = String.Empty Then
        '    MessageBox.Show("Silakan masukan Solusi")
        '    Return False
        'End If
        Return True

    End Function
    Private Sub LoadChassisInfo(ByRef obj As ChassisMasterBB)
        txtNoChasis.Text = obj.ChassisNumber
        lblNoChasisVal.Text = obj.ChassisNumber
        lblNoMesinVal.Text = obj.EngineNumber
        If Not obj.VechileColor Is Nothing Then
            lblTypeColorVal.Text = obj.VechileColor.MaterialNumber & " - " & obj.VechileColor.MaterialDescription
        Else
            lblTypeColorVal.Text = ""
        End If

        If obj.EndCustomer Is Nothing Then
            lblNamaVal.Text = ""
            lblTglFakturVal.Text = ""
        Else
            If obj.EndCustomer.OpenFakturDate.Year < 1970 Then
                lblNamaVal.Text = ""
                lblTglFakturVal.Text = ""
            Else
                lblTglFakturVal.Text = obj.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy")
                If Not obj.EndCustomer.Customer Is Nothing Then
                    If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        lblNamaVal.Text = obj.EndCustomer.Customer.Name1 & " - " & obj.EndCustomer.Customer.Alamat
                    Else
                        If obj.Dealer.ID = oDealer.ID Then
                            lblNamaVal.Text = obj.EndCustomer.Customer.Name1 & " - " & obj.EndCustomer.Customer.Alamat
                        Else
                            lblNamaVal.Text = ""
                        End If
                    End If
                Else
                    lblNamaVal.Text = ""
                End If
            End If
        End If
        lblThnProduksiVal.Text = IIf(obj.ProductionYear <= 1900, "", obj.ProductionYear)
        lblTglDeliveryVal.Text = obj.DODateText
        If Not obj.Category Is Nothing Then
            lbModel.Text = obj.Category.Description
        Else
            lbModel.Text = ""
        End If

        lnkbtnPopUpInfoKendaraan.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpInfoKendaraanBB.aspx?cn=" & obj.ChassisNumber, "", 710, 700, "ShowPopUp")
        lnkbtnPopUpInfoKendaraan.Visible = True
    End Sub
    Private Sub ClearChassisInfo()
        txtNoChasis.Text = ""
        lblNoMesinVal.Text = ""
        lbModel.Text = ""
        lblTypeColorVal.Text = ""
        lblNamaVal.Text = ""
        lblThnProduksiVal.Text = "" ' Temporaly not available
        lblTglDeliveryVal.Text = ""
        lblTglFakturVal.Text = "" ' Temporaly not available

        lnkbtnPopUpInfoKendaraan.Attributes.Clear()
        lnkbtnPopUpInfoKendaraan.Visible = False
    End Sub
    Private Sub lnkbtnCheckChassis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnCheckChassis.Click
        If oChassisFacade.IsExist(txtNoChasis.Text) Then
            txtNoChasis.ForeColor = Color.Black
            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim strChassisNumber = txtNoChasis.Text.Trim()
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, strChassisNumber))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
            Dim ChassisColl As ArrayList = New ChassisMasterBBFacade(User).Retrieve(criterias)
            If ChassisColl.Count > 0 Then
                'Dim oChaColl As ChassisMasterBB = ChassisColl(0)
                'If IsNothing(oChaColl.EndCustomer) Then
                '    MessageBox.Show("Nomor Rangka belum terjual")
                '    Exit Sub
                'ElseIf oChaColl.FakturStatus = 0 Then
                '    MessageBox.Show("Nomor Rangka belum terjual")
                '    Exit Sub
                'End If
                LoadChassisInfo(CType(oChassisFacade.Retrieve(txtNoChasis.Text), ChassisMasterBB))
                sessHelper.SetSession("NEW_DAMAGECODE", New ArrayList)
                sessHelper.SetSession("NEW_PARTS", New ArrayList)
                sessHelper.SetSession("NEW_ATTACHMENTTOP", New ArrayList)
                sessHelper.SetSession("NEW_ATTACHMENTBOTTOM", New ArrayList)
                sessHelper.SetSession("NEW_QRS", New ArrayList)
            Else
                txtNoChasis.ForeColor = Color.Red
                ClearChassisInfo()
                MessageBox.Show("No Rangka tidak terdaftar.")
            End If
        Else
            txtNoChasis.ForeColor = Color.Red
            ClearChassisInfo()
            MessageBox.Show("No Rangka tidak terdaftar.")
        End If
            RefreshGrid()
    End Sub

    Private Sub btnCancelStatusChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelStatusChange.Click
        Dim result As Integer
        Dim act As String = String.Empty
        Dim ErrMessage As String = String.Empty

        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        Dim IsDealer As Boolean = False
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            IsDealer = True
        End If

        oPQRHeaderBB = CType(sessHelper.GetSession("PQR"), PQRHeaderBB)
        Select Case CType(oPQRHeaderBB.RowStatus, EnumPQR.PQRStatus)
            Case EnumPQR.PQRStatus.Batal
                act = "Batal"
                MessageBox.Show("Invalid Action")
                Return
            Case EnumPQR.PQRStatus.Baru
                act = "Batal"
                result = oPQRHeaderBBFacade.UbahStatusPQRDocument(oPQRHeaderBB, EnumPQR.PQRStatus.Batal, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Validasi
                act = "Batal Validasi"
                result = oPQRHeaderBBFacade.UbahStatusPQRDocument(oPQRHeaderBB, EnumPQR.PQRStatus.Batal, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Proses
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Batal Proses"
                result = oPQRHeaderBBFacade.UbahStatusPQRDocument(oPQRHeaderBB, EnumPQR.PQRStatus.Batal, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Rilis
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Batal Rilis"
                result = oPQRHeaderBBFacade.UbahStatusPQRDocument(oPQRHeaderBB, EnumPQR.PQRStatus.Proses, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Selesai
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Batal Selesai"
                result = oPQRHeaderBBFacade.UbahStatusPQRDocument(oPQRHeaderBB, EnumPQR.PQRStatus.Rilis, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
        End Select
        If (result = -1) Then
            If ErrMessage = String.Empty Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(ErrMessage)
            End If
        Else
            MessageBox.Show("Proses " & act & " Berhasil dilakukan.")
            ReloadForm(result)
        End If
    End Sub

    Private Sub btnStatusChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStatusChange.Click
        Dim result As Integer
        Dim act As String = String.Empty
        Dim ErrMessage As String = String.Empty

        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        Dim IsDealer As Boolean = False
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            IsDealer = True
        End If

        oPQRHeaderBB = CType(sessHelper.GetSession("PQR"), PQRHeaderBB)
        Select Case CType(oPQRHeaderBB.RowStatus, EnumPQR.PQRStatus)
            Case EnumPQR.PQRStatus.Batal
                act = "Batal"
                result = oPQRHeaderBBFacade.UbahStatusPQRDocument(oPQRHeaderBB, EnumPQR.PQRStatus.Baru, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Baru
                act = "Validasi"
                result = oPQRHeaderBBFacade.UbahStatusPQRDocument(oPQRHeaderBB, EnumPQR.PQRStatus.Validasi, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Validasi
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Proses"
                result = oPQRHeaderBBFacade.UbahStatusPQRDocument(oPQRHeaderBB, EnumPQR.PQRStatus.Proses, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Proses
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Rilis"
                result = oPQRHeaderBBFacade.UbahStatusPQRDocument(oPQRHeaderBB, EnumPQR.PQRStatus.Rilis, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Rilis
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Selesai"
                result = oPQRHeaderBBFacade.UbahStatusPQRDocument(oPQRHeaderBB, EnumPQR.PQRStatus.Selesai, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Selesai
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Selesai"
                MessageBox.Show("Invalid Action")
                Return
        End Select
        If (result = -1) Then
            If ErrMessage = String.Empty Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(ErrMessage)
            End If
        Else
            MessageBox.Show("Proses " & act & " Berhasil dilakukan.")
            ReloadForm(result)
        End If
    End Sub

    Private Sub lnkbtnAdditionalInfoPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAdditionalInfoPopUp.Click
        Dim sMode As String
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        Select Case Mode
            Case enumMode.Mode.NewItemMode
                sMode = "New"
            Case enumMode.Mode.EditMode
                sMode = "Edit"
            Case enumMode.Mode.ViewMode
                sMode = "View"
        End Select
        Response.Redirect("../PQR/FrmPQRAdditionalInfoBB.aspx?Mode=" & sMode & "&PQRID=" & CType(sessHelper.GetSession("PQR"), PQRHeaderBB).ID & "&Src=" & Request.QueryString("Src"))
        ReloadForm(CType(sessHelper.GetSession("PQR"), PQRHeaderBB).ID)
    End Sub

    Private Sub btnCetak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCetak.Click
        Dim sMode As String
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        Select Case Mode
            Case enumMode.Mode.NewItemMode
                sMode = "New"
            Case enumMode.Mode.EditMode
                sMode = "Edit"
            Case enumMode.Mode.ViewMode
                sMode = "View"
        End Select
        Response.Redirect("../PQR/FrmPQRPrintPreviewBB.aspx?Mode=" & sMode & "&PQRID=" & CType(sessHelper.GetSession("PQR"), PQRHeaderBB).ID.ToString & "&Src=" & Request.QueryString("Src"))
    End Sub

    Protected Sub buttonInputWSCYes_Click(sender As Object, e As EventArgs) Handles buttonInputWSCYes.Click
        'Dim result = ValidatePqrWSC()
        'Server.Transfer("../Service/FrmWSCHeader.aspx?screenFrom=PQR&PQRId=" & result)
    End Sub

    Protected Sub buttonInputWSCNo_Click(sender As Object, e As EventArgs) Handles buttonInputWSCNo.Click
        'Server.Transfer("../PQR/FrmPQRList.aspx")
    End Sub

    Private Function genPQRNo(Param As Integer) As String
        Dim _return As String
        Dim _crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        _crits.opAnd(New Criteria(GetType(PQRHeaderBB), "ID", MatchType.Exact, Param))
        Dim _arList As ArrayList = New PQRHeaderBBFacade(User).Retrieve(_crits)
        For Each a As PQRHeaderBB In _arList
            _return = a.PQRNo
            Exit For
        Next
        Return _return
    End Function

End Class
