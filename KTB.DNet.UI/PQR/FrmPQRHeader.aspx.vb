Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports Newtonsoft.Json

Imports ICSharpCode.SharpZipLib.Core
Imports ICSharpCode.SharpZipLib.Zip

Imports System.Text
Imports System.IO
Imports System.Linq

Public Class FrmPQRHeader
#Region "Private Variable"

    Inherits System.Web.UI.Page

    Private _dtKodeKerusakanStartDate As DateTime = New DateTime(2010, 6, 17, 17, 20, 0)

    Private sessHelper As New SessionHelper
    Private oDealer As New Dealer
    Private oLoginUser As New UserInfo

    Private oPQRHeader As PQRHeader
    Private oPQRHeaderFacade As New PQRHeaderFacade(User)

    Private oPQRDamageCode As New PQRDamageCode
    Private oPQRDamageCodeFacade As New PQRDamageCodeFacade(User)

    Private oPQRPartsCode As New PQRPartsCode
    Private oPQRPartsCodeFacade As New PQRPartsCodeFacade(User)

    Private oPQRAdditionalInfo As New PQRAdditionalInfo
    Private oPQRAdditionalInfoFacade As New PQRAdditionalInfoFacade(User)

    Private oPQRAttachment As New PQRAttachment
    Private oPQRAttachmentFacade As New PQRAttachmentFacade(User)

    Private oPQRChangesHistory As New PQRChangesHistory
    Private oPQRChangesHistoryFacade As New PQRChangesHistoryFacade(User)

    Private oPQRSolutionReferences As New PQRSolutionReferences
    Private oPQRSolutionReferencesFacade As New PQRSolutionReferencesFacade(User)

    Private oChassis As New ChassisMaster
    Private oChassisFacade As New ChassisMasterFacade(User)

    Private Mode As enumMode.Mode
    Private hashPQRType As Hashtable


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
    Protected WithEvents lblNoMesinColon As System.Web.UI.WebControls.Label
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
    Protected WithEvents btnSimpan2 As System.Web.UI.WebControls.Button
    Protected WithEvents txtSubject As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgFileAttachmentTop As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgFileAttachmentBottom As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lnkbtnCheckChassis As System.Web.UI.WebControls.LinkButton
    Protected WithEvents dgKerusakan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ltrStatusAdditionalInfo As System.Web.UI.WebControls.Literal
    'Protected WithEvents txtRefPQRNo As System.Web.UI.WebControls.TextBox
    'Protected WithEvents lblRefPQRNoVal As System.Web.UI.WebControls.Label
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
    Protected WithEvents lblTglPemasangan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPemasanganVal As System.Web.UI.WebControls.Label
    Protected WithEvents icTglPemasangan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblPosKerusakan As System.Web.UI.WebControls.Label
    Protected WithEvents Div1 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblOdoPemasangan As System.Web.UI.WebControls.Label
    Protected WithEvents lblOdoPemasanganVal As System.Web.UI.WebControls.Label
    Protected WithEvents rfvOdoPemasangan As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtOdoPemasangan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerInvoice As System.Web.UI.WebControls.TextBox

    Protected WithEvents btnDownloadAll As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        AttachmentDirectory = KTB.DNet.Lib.WebConfig.GetValue("PQRAttachmentDir")
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\PQRTemp\" + oDealer.ID.ToString + "-" + oLoginUser.ID.ToString + "\"


        If Request.QueryString("Mode").ToString() = "New" Then
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQR, Panel1, False)
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQR, Panel2, False)
        ElseIf Request.QueryString("Mode").ToString() = "Edit" OrElse Request.QueryString("Mode").ToString() = "View" Then
            oPQRHeader = CType(sessHelper.GetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader)
            If oPQRHeader Is Nothing Then
                oPQRHeader = oPQRHeaderFacade.Retrieve(CType(Request.QueryString("PQRID").ToString(), Integer))
                sessHelper.SetSession("PQR" + Request.QueryString("PQRID").ToString(), oPQRHeader)

                sessHelper.SetSession("DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), oPQRHeader.PQRDamageCodes)
                sessHelper.SetSession("DELETEDDAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)

                sessHelper.SetSession("PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), oPQRHeader.PQRPartsCodes)
                sessHelper.SetSession("DELETEDPARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)

                sessHelper.SetSession("ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), GetAttachmentList(oPQRHeader.PQRAttachments, EnumPQR.AttachmentLocation.Top))
                sessHelper.SetSession("ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), GetAttachmentList(oPQRHeader.PQRAttachments, EnumPQR.AttachmentLocation.Bottom))
                sessHelper.SetSession("DELETEDATTACHMENT" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)

                sessHelper.SetSession("QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), oPQRHeader.PQRQRSs)
                sessHelper.SetSession("DELETEDQRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)
            End If
            If oDealer.Title = 0 Then 'Dealer
                If CType(oPQRHeader.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Baru Then
                    'If oPQRHeader.CreatedBy.Substring(0, 6) = "000002" Then
                    '    RenderProfilePanel(oPQRHeader, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQR, Panel1, True)
                    '    RenderProfilePanel(oPQRHeader, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQR, Panel2, True)
                    'Else
                    RenderProfilePanel(oPQRHeader, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQR, Panel1, False)
                    RenderProfilePanel(oPQRHeader, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQR, Panel2, False)
                    'End If
                Else
                    RenderProfilePanel(oPQRHeader, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQR, Panel1, True)
                    RenderProfilePanel(oPQRHeader, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQR, Panel2, True)
                End If
            ElseIf oDealer.Title = 1 Then ' KTB 
                If CType(oPQRHeader.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Baru Then
                    RenderProfilePanel(oPQRHeader, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQR, Panel1, False)
                    RenderProfilePanel(oPQRHeader, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQR, Panel2, False)
                Else
                    RenderProfilePanel(oPQRHeader, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQR, Panel1, True)
                    RenderProfilePanel(oPQRHeader, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQR, Panel2, True)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If SecurityProvider.Authorize(Context.User, SR.PQRNewView_Privilege) Then
            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If Request.QueryString("Mode").ToString() = "New" Then
                    If Not (SecurityProvider.Authorize(Context.User, SR.PQRNewSave_Privilege)) Then
                        Server.Transfer("../FrmAccessDenied.aspx?modulName=PRODUCT QUALITY REPORT - Buat PQR")
                    End If
                End If
            End If
        Else
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PRODUCT QUALITY REPORT - Buat PQR")
        End If
    End Sub

    Dim bCekPriv As Boolean = (SecurityProvider.Authorize(Context.User, SR.PQRNewSave_Privilege))

#End Region

    Private Sub RenderProfilePanel(ByVal objPQRHeader As PQRHeader, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel, ByVal isReadonly As Boolean)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(isReadonly)

        If Not objPQRHeader Is Nothing Then
            objRenderPanel.GeneratePanel(objPQRHeader.ID, objPanel, objGroup, profileType, User)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User)
        End If
    End Sub

    Private Sub ClearForm()

        lblPQRNoVal.Text = ""
        'txtRefPQRNo.Text = ""
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
        lblPQRNoVal.Text = oPQRHeader.PQRNo
        'txtRefPQRNo.Text = oPQRHeader.RefPQRNo
        'lblRefPQRNoVal.Text = oPQRHeader.RefPQRNo
        lblTglPembuatanVal.Text = oPQRHeader.DocumentDate.ToString("dd/MM/yyyy")

        LoadChassisInfo(CType(oChassisFacade.Retrieve(oPQRHeader.ChassisMaster.ID), ChassisMaster))
        icTglKerusakan.Value = oPQRHeader.PQRDate.ToString("dd/MM/yyyy")
        lblTglKerusakanVal.Text = oPQRHeader.PQRDate.ToString("dd/MM/yyyy")
        txtOdometer.Text = oPQRHeader.OdoMeter
        lblOdometerVal.Text = oPQRHeader.OdoMeter
        txtKecepatan.Text = oPQRHeader.Velocity
        lblKecepatanVal.Text = oPQRHeader.Velocity
        ddlBobot.SelectedValue = oPQRHeader.Bobot
        txtSubject.Text = oPQRHeader.Subject
        lblSubjectVal.Text = oPQRHeader.Subject
        txtGejala.Text = oPQRHeader.Symptomps
        txtPenyebab.Text = oPQRHeader.Causes
        txtHasil.Text = oPQRHeader.Results
        txtCatatan.Text = oPQRHeader.Notes
        txtSolution.Text = oPQRHeader.Solutions
        If Not IsNothing(oPQRHeader.DealerBranch) Then
            txtDealerBranchCode.Text = oPQRHeader.DealerBranch.DealerBranchCode
            txtBranchName.Text = oPQRHeader.DealerBranch.Name
        End If
        If Not IsNothing(oPQRHeader.WorkOrderNumber) Then
            txtWONumber.Text = oPQRHeader.WorkOrderNumber
        End If

        lbModel.Text = oPQRHeader.ChassisMaster.Category.Description
        lblStatusVal.Text = CType(oPQRHeader.RowStatus, EnumPQR.PQRStatus).ToString
        If CType(oPQRHeader.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Proses Or CType(oPQRHeader.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Rilis Or CType(oPQRHeader.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Selesai Then
            If oPQRHeader.ConfirmBy <> String.Empty Then
                lblProcessByVal.Text = CommonFunction.FormatSavedUser(oPQRHeader.ConfirmBy, User)
            Else
                lblProcessByVal.Text = ""
            End If
            If oPQRHeader.ConfirmTime <= New DateTime(1900, 1, 1) Then
                lblTglJamProcessVal.Text = ""
            Else
                lblTglJamProcessVal.Text = oPQRHeader.ConfirmTime.ToString("dd/MM/yyyy HH:mm:ss")
            End If
        Else
            lblProcessByVal.Text = ""
            lblTglJamProcessVal.Text = ""
        End If
        If oPQRHeader.CodeA.Trim <> "" Then
            Me.ddlKodeWSCA.SelectedValue = oPQRHeader.CodeA
        End If
        If oPQRHeader.CodeB.Trim <> "" Then
            Me.ddlKodeWSCB.SelectedValue = oPQRHeader.CodeB
        End If
        If oPQRHeader.CodeC.Trim <> "" Then
            Me.ddlKodeWSCC.SelectedValue = oPQRHeader.CodeC
        End If

        icTglPemasangan.Value = oPQRHeader.InstallDate
        txtOdoPemasangan.Text = oPQRHeader.InstallOdometer

    End Sub
    Private Sub fillForm()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        oPQRHeader = CType(sessHelper.GetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader)

        If Mode = enumMode.Mode.NewItemMode Then
            oPQRHeader = CType(sessHelper.GetSession("NEW_PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader)
            'ddlPqrType.SelectedIndex = oPQRHeader.PQRType + 1
            ddlPqrType.SelectedValue = oPQRHeader.PQRType
            ddlPqrType_SelectedIndexChanged(Nothing, Nothing)
            ClearForm()
            lblDealerVal.Text = oDealer.DealerCode & " - " & oDealer.DealerName
            lblAppliedByVal.Text = oDealer.DealerCode & " - " & oLoginUser.UserName
            lblTglJamVal.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm")

            ltrStatusAdditionalInfo.Text = "Tidak Ada"
            lnkbtnAdditionalInfoPopUp.Visible = False
            lblLastPostedInfo.Visible = False
            lblLastPostedInfo.ToolTip = ""
        ElseIf Mode = enumMode.Mode.EditMode OrElse Mode = enumMode.Mode.ViewMode Then
            lblDealerVal.Text = oPQRHeader.Dealer.DealerCode & " - " & oPQRHeader.Dealer.SearchTerm1
            lblAppliedByVal.Text = CommonFunction.FormatSavedUser(oPQRHeader.CreatedBy, User)
            lblTglJamVal.Text = oPQRHeader.CreatedTime.ToString("dd/MM/yyyy HH:mm")
            'ddlPqrType.SelectedIndex = oPQRHeader.PQRType + 1
            ddlPqrType.SelectedValue = oPQRHeader.PQRType

            If oPQRHeader.PQRAdditionalInfos.Count > 0 Then
                ltrStatusAdditionalInfo.Text = "Ada"
                lblLastPostedInfo.Visible = True

                Dim tempArr As ArrayList = oPQRHeader.PQRAdditionalInfos
                tempArr = KTB.DNet.Utility.CommonFunction.SortArraylist(tempArr, GetType(PQRAdditionalInfo), "CreatedTime", Sort.SortDirection.DESC)

                Dim obj As PQRAdditionalInfo = CType(tempArr(0), PQRAdditionalInfo)
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
            'lnkbtnAdditionalInfoPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PQR/FrmPQRAdditionalInfo.aspx?PQRID=" & oPQRHeader.ID, "", 710, 700, "ShowPopUp")
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
            If Not (Request.QueryString("Src") = "WSCDetail" Or Request.QueryString("Src") = "PQRListKondisi" Or Request.QueryString("Src") = "WSCList") Then
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
        oPQRHeader = CType(sessHelper.GetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader)
        ddlBobot.Visible = False
        If Mode = enumMode.Mode.NewItemMode OrElse Mode = enumMode.Mode.EditMode Then
            btnSimpan.Visible = bCekPriv
            oPQRHeader = CType(sessHelper.GetSession("NEW_PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader)
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
                '    'If oPQRHeader.CreatedBy.Substring(0, 6) = "000002" Then
                '    btnStatusChange.Visible = False
                '    btnCancelStatusChange.Visible = False
                '    btnBatal.Visible = False
                '    btnCetak.Visible = True
                '    'End If
                'End If


                If ddlPqrType.SelectedValue = "3" Then
                    dgKerusakan.Enabled = False
                    dgKerusakan.ShowFooter = False
                End If
            End If

            SetEntryArea(oDealer.Title)

            If oDealer.Title = 1 And Mode = enumMode.Mode.EditMode Then 'KTB
                If oPQRHeader.RowStatus = EnumPQR.PQRStatus.Rilis Then
                    ddlBobot.Visible = True
                End If
            End If

        ElseIf Mode = enumMode.Mode.ViewMode Then
            'txtRefPQRNo.Visible = False
            txtNoChasis.Visible = False
            icTglKerusakan.Visible = False
            txtOdometer.Visible = False
            txtKecepatan.Visible = False
            txtSubject.Visible = False
            lnkbtnCheckChassis.Visible = False

            'lblRefPQRNoVal.Visible = True
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

            btnSimpan.Visible = False
            btnCetak.Visible = True
            If oDealer.Title = 1 Then 'KTB
                ddlBobot.Visible = True
                ddlBobot.Enabled = False
            End If
            If Request.QueryString("Src") = "WSCDetail" Or Request.QueryString("Src") = "PQRListKondisi" Or Request.QueryString("Src") = "WSCList" Then
                btnBatal.Visible = True
                btnStatusChange.Visible = False
                btnCancelStatusChange.Visible = False
            Else
                btnBatal.Visible = True
                btnStatusChange.Visible = True
                btnCancelStatusChange.Visible = True
            End If

        End If
        If Not IsNothing(oPQRHeader) AndAlso oPQRHeader.ID > 0 AndAlso (oPQRHeader.RowStatus = EnumPQR.PQRStatus.Batal Or oPQRHeader.RowStatus = EnumPQR.PQRStatus.Selesai) Then
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
                    If SecurityProvider.Authorize(Context.User, SR.PQRNewSave_Privilege) Then
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

        If oDealer.Title = 0 Then 'Dealer
            lblNoMesin.Visible = False
            lblNoMesinVal.Visible = False
            lblNoMesinColon.Visible = False
        ElseIf oDealer.Title = 1 Then 'MKS
            lblNoMesin.Visible = True
            lblNoMesinVal.Visible = True
            lblNoMesinColon.Visible = True
        End If

    End Sub
    Private Sub SetEntryArea(ByVal DealerTitle As Integer)
        If DealerTitle = 0 Then ' Dealer
            txtSolution.ReadOnly = True
            dgFileAttachmentBottom.ShowFooter = False
            dgFileAttachmentBottom.Columns(dgFileAttachmentBottom.Columns.Count - 1).Visible = False
        ElseIf DealerTitle = 1 Then 'KTB
            If SecurityProvider.Authorize(Context.User, SR.PQRNewSave_Privilege) Then
                'txtRefPQRNo.Visible = True
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

                'lblRefPQRNoVal.Visible = False
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
                'txtRefPQRNo.Visible = False
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

                'lblRefPQRNoVal.Visible = True
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
            oPQRHeader = CType(sessHelper.GetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader)
            If Not IsNothing(oPQRHeader) AndAlso oPQRHeader.ID > 0 AndAlso CType(oPQRHeader.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Selesai Then
                ddlBobot.Visible = True
                ddlBobot.Enabled = False
            End If

        End If

    End Sub


    Private Sub setStatusAction()

        oPQRHeader = CType(sessHelper.GetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader)
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALERINFO"), Dealer)
        Select Case CType(oPQRHeader.RowStatus, EnumPQR.PQRStatus)
            Case EnumPQR.PQRStatus.Batal
                btnStatusChange.Text = "Restore"
                btnCancelStatusChange.Text = "-"
                btnStatusChange.Visible = False
                btnCancelStatusChange.Visible = False
            Case EnumPQR.PQRStatus.Baru
                btnStatusChange.Text = "Validasi"
                btnCancelStatusChange.Text = "Batal"
                If CType(sessHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
                    'If oPQRHeader.CreatedBy.Substring(0, 6) = "000002" Then
                    '    btnStatusChange.Visible = False
                    '    btnCancelStatusChange.Visible = False
                    'Else
                    btnStatusChange.Visible = True
                    btnCancelStatusChange.Visible = True
                    btnCancelStatusChange.Visible = SecurityProvider.Authorize(Context.User, SR.PQR_status_Batal_Privilege)
                    btnStatusChange.Visible = SecurityProvider.Authorize(Context.User, SR.PQR_status_validasi_Privilege)
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
                    If SecurityProvider.Authorize(Context.User, SR.PQRNewSave_Privilege) Then
                        btnSimpan.Visible = False
                        btnCancelStatusChange.Visible = False
                    End If
                End If
                If btnCancelStatusChange.Visible = True Then
                    btnCancelStatusChange.Visible = SecurityProvider.Authorize(Context.User, SR.PQR_status_validasi_batal_Privilege)
                End If
                If btnStatusChange.Visible = True Then
                    btnStatusChange.Visible = SecurityProvider.Authorize(Context.User, SR.PQR_status_proses_Privilege)
                End If
            Case EnumPQR.PQRStatus.Proses
                btnStatusChange.Text = "Rilis"
                btnCancelStatusChange.Text = "Batal"
                If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    If SecurityProvider.Authorize(Context.User, SR.PQRNewSave_Privilege) Then
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
                    btnCancelStatusChange.Visible = SecurityProvider.Authorize(Context.User, SR.PQR_status_proses_batal_Privilege)
                End If
                If btnStatusChange.Visible = True Then
                    btnStatusChange.Visible = SecurityProvider.Authorize(Context.User, SR.PQR_status_rilis_Privilege)
                End If
            Case EnumPQR.PQRStatus.Rilis
                btnStatusChange.Text = "Selesai"
                btnCancelStatusChange.Text = "Batal"
                btnStatusChange.Visible = True
                btnCancelStatusChange.Visible = True
                btnCancelStatusChange.Visible = SecurityProvider.Authorize(Context.User, SR.PQR_status_rilis_batal_Privilege)
                btnStatusChange.Visible = SecurityProvider.Authorize(Context.User, SR.PQR_status_selesai_Privilege)
                If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    If Not SecurityProvider.Authorize(Context.User, SR.PQRNewSave_Privilege) Then
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
                btnCancelStatusChange.Visible = SecurityProvider.Authorize(Context.User, SR.PQR_status_selesai_batal_Privilege)
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
        hashPQRType = New StandardCodeFacade(User).RetrieveHashByCategory("PQRType")
        ViewState("HashPQRType") = hashPQRType
        ddlPqrType.Items.Clear()
        ddlPqrType.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each key As Integer In hashPQRType.Keys
            ddlPqrType.Items.Add(New ListItem(hashPQRType(key), key))
        Next
    End Sub

    Private Sub BindPqrType_old()
        ddlPqrType.Items.Clear()
        ddlPqrType.Items.Add(New ListItem("Silahkan Pilih", Nothing)) 'PQR Spare Part & PQR Accessories
        ddlPqrType.Items.Add(New ListItem("PQR WSC", 0))
        ddlPqrType.Items.Add(New ListItem("PQR Only", 1))
        ddlPqrType.Items.Add(New ListItem("PQR Spare Part", 2))
        ddlPqrType.Items.Add(New ListItem("PQR Accessories", 3))
        ddlPqrType.Items.Add(New ListItem("PQR Extended Waranty", 4))
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

    Private Sub SetControlKodeKerusakan(ByVal oPQRH As PQRHeader)
        Dim IsImplementKodeKerusakan As Boolean = False
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        btnDownloadAll.Visible = False
        ddlKodeWSCA.Enabled = False
        ddlKodeWSCB.Enabled = False
        ddlKodeWSCC.Enabled = False
        If Mode = enumMode.Mode.NewItemMode OrElse Mode = enumMode.Mode.EditMode Then
            If CType(sessHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
                ddlKodeWSCA.Enabled = True
                ddlKodeWSCB.Enabled = True
                ddlKodeWSCC.Enabled = True
            Else
                If SecurityProvider.Authorize(Context.User, SR.PQRNewSave_Privilege) Then
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

    Private Sub ValidatePqrWSC()
        Dim result As Integer = 0
        Try
            Dim oPqrHeader As PQRHeader = sessHelper.GetSession("inputWSCSes" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""))
            result = oPqrHeader.ID
        Catch
        End Try
        Dim strJs As String = ""
        strJs += "var pqrWscConfirmation = confirm('Simpan Data Berhasil. \nInput WSC?');"
        strJs += "if(pqrWscConfirmation) {"
        strJs += "window.location = '../Service/FrmWSCHeader.aspx?screenFrom=PQR&PQRId=" & result & "';"
        strJs += "}"
        If ViewState("Mode") = enumMode.Mode.NewItemMode Then
            strJs += "else {"
            strJs += "window.location = 'FrmPQRList.aspx';"
            strJs += "}"
        End If

        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        InitiateAuthorization()
        If Not IsPostBack Then
            BindPqrType()
            BindBobot()
            BindKodePosisiWSC()
            If Request.QueryString("Mode").ToString() = "New" Then
                ViewState("Mode") = enumMode.Mode.NewItemMode
                oPQRHeader = New PQRHeader

                sessHelper.SetSession("NEW_PQR" + If(Request.QueryString("PQRID") IsNot Nothing, If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), ""), oPQRHeader)
                sessHelper.SetSession("NEW_DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)
                sessHelper.SetSession("NEW_PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)
                sessHelper.SetSession("NEW_ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)
                sessHelper.SetSession("NEW_ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)
                sessHelper.SetSession("NEW_QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)
            ElseIf Request.QueryString("Mode").ToString() = "Edit" OrElse Request.QueryString("Mode").ToString() = "View" Then
                If Request.QueryString("Mode").ToString() = "Edit" Then
                    ViewState("Mode") = enumMode.Mode.EditMode
                ElseIf Request.QueryString("Mode").ToString() = "View" Then
                    ViewState("Mode") = enumMode.Mode.ViewMode
                End If

                oPQRHeader = CType(sessHelper.GetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader)

                sessHelper.SetSession("DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), oPQRHeader.PQRDamageCodes)
                sessHelper.SetSession("DELETEDDAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)

                sessHelper.SetSession("PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), oPQRHeader.PQRPartsCodes)
                sessHelper.SetSession("DELETEDPARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)

                sessHelper.SetSession("ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), GetAttachmentList(oPQRHeader.PQRAttachments, EnumPQR.AttachmentLocation.Top))
                sessHelper.SetSession("ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), GetAttachmentList(oPQRHeader.PQRAttachments, EnumPQR.AttachmentLocation.Bottom))
                sessHelper.SetSession("DELETEDATTACHMENT" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)

                sessHelper.SetSession("QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), oPQRHeader.PQRQRSs)
                sessHelper.SetSession("DELETEDQRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)

                txtDealerInvoice.Enabled = False
                icTglPemasangan.Enabled = False
                txtOdoPemasangan.Enabled = False
            End If
            SetControlKodeKerusakan(oPQRHeader)
            txtBranchName.Attributes.Add("readonly", "readonly")
            fillForm()
        Else

        End If

        'BindDamageCode()
        'BindParts()
        'BindAttachmentTop()
        'BindAttachmentBottom()
    End Sub
    Private Function GetAttachmentList(ByVal attachmentCollection As ArrayList, ByVal location As EnumPQR.AttachmentLocation) As ArrayList
        Dim TempList As New ArrayList
        TempList.Clear()
        For Each obj As PQRAttachment In attachmentCollection
            If obj.Type = location Then
                TempList.Add(obj)
            End If
        Next
        Return TempList
    End Function
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        If Request.QueryString("Src") = "WSCDetail" Then
            Server.Transfer("~/Service/FrmWSCDetail.aspx")
        ElseIf Request.QueryString("Src") = "PQRListKondisi" Then
            Server.Transfer("~/PQR/FrmPQRListKondisi.aspx")
        ElseIf Request.QueryString("Src") = "WSCList" Then
            Server.Transfer("~/Service/FrmWSCHeader.aspx?screenFrom=WSC&WSCId=" & Request.QueryString("WSCId") & "&viewStateMode=" & Request.QueryString("State"))
        Else
            Server.Transfer("~/PQR/FrmPQRList.aspx")
        End If
    End Sub

    Private Sub lnkBtnCheckWONumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBtnCheckWONumber.Click


        Try
            ' Get DMSWOWarrantyClaim based on WO Number            
            Dim warrantyCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DMSWOWarrantyClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            warrantyCriteria.opAnd(New Criteria(GetType(DMSWOWarrantyClaim), "WorkOrderNumber", MatchType.Exact, txtWONumber.Text))
            warrantyCriteria.opAnd(New Criteria(GetType(DMSWOWarrantyClaim), "isBB", MatchType.Exact, 0))
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

    Private Function QRSIsExist(ByVal ChassisMasterID As Integer, ByVal QRSCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If Not QRSCollection Is Nothing Then
            If QRSCollection.Count > 0 Then
                For Each _PQRQRS As PQRQRS In QRSCollection
                    If _PQRQRS.ChassisMaster.ID = ChassisMasterID Then
                        bResult = True
                        Exit For
                    End If
                Next
            End If
        End If

        Return bResult
    End Function
    Private Function QRSIsExist(ByVal ChassisMasterID As Integer, ByVal QRSCollection As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim i As Integer = 0
        Dim bResult As Boolean = False

        If QRSCollection.Count > 0 Then
            For Each _PQRQRS As PQRQRS In QRSCollection
                If _PQRQRS.ChassisMaster.ID = ChassisMasterID AndAlso nIndeks <> i Then
                    bResult = True
                    Exit For
                End If
                i += 1
            Next
        End If
        Return bResult
    End Function
    Private Function QRSIsExist(ByVal ChassisNumber As String) As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, ChassisNumber.Trim))

        Dim tempArr As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)

        If tempArr.Count > 0 Then
            Return True
        End If

        Return False
    End Function

    Private Function QRSIsSameCategory(ByVal ChassisQRS As ChassisMaster, ByVal ChassisPQR As ChassisMaster) As Boolean
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
        _arrQRS = CType(sessHelper.GetSession("QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)

        Mode = CType(ViewState("Mode"), enumMode.Mode)

        If Mode = enumMode.Mode.NewItemMode Then
            _arrQRS = CType(sessHelper.GetSession("NEW_QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
        Else
            _arrQRS = CType(sessHelper.GetSession("QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
        End If
        Dim objChassisMain As ChassisMaster
        objChassisMain = New ChassisMasterFacade(User).Retrieve(txtNoChasis.Text.Trim())
        Mode = CType(ViewState("Mode"), enumMode.Mode)

        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid

                Dim txtNoRangkaFooter As TextBox = CType(e.Item.FindControl("txtNoRangkaFooter"), TextBox)
                Dim icTglKerusakanFooter As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icTglKerusakanFooter"), KTB.DNet.WebCC.IntiCalendar)
                Dim txtOdometerFooter As TextBox = CType(e.Item.FindControl("txtOdometerFooter"), TextBox)
                Dim txtCatatanFooter As TextBox = CType(e.Item.FindControl("txtCatatanFooter"), TextBox)

                Dim objChassisFooter As New ChassisMaster
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


                objChassisFooter = New ChassisMasterFacade(User).Retrieve(txtNoRangkaFooter.Text.Trim())

                If Not QRSIsSameCategory(objChassisFooter, objChassisMain) Then
                    MessageBox.Show("Kategori No Rangka di QRS berbeda dengan No Rangka PQR")
                    RefreshGrid()
                    Return
                End If

                If Not (IsNothing(objChassisFooter) OrElse objChassisFooter.ID = 0) Then
                    If Not QRSIsExist(objChassisFooter.ID, _arrQRS) Then
                        Dim objPQRQRS As PQRQRS = New PQRQRS

                        objPQRQRS.ChassisMaster = objChassisFooter
                        objPQRQRS.TglKerusakan = icTglKerusakanFooter.Value
                        objPQRQRS.Odometer = CInt(txtOdometerFooter.Text)
                        objPQRQRS.Note = txtCatatanFooter.Text

                        _arrQRS.Add(objPQRQRS)
                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrQRS)
                        Else
                            sessHelper.SetSession("QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrQRS)
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
                    Dim deletedQRS As PQRQRS = CType(_arrQRS(e.Item.ItemIndex), PQRQRS)
                    If deletedQRS.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDQRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
                        deletedArrLst.Add(deletedQRS)
                        sessHelper.SetSession("DELETEDQRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), deletedArrLst)
                    End If
                    _arrQRS.RemoveAt(e.Item.ItemIndex)
                End If


            Case "Save" 'Update this datagrid item                 

                Dim txtNoRangka As TextBox = CType(e.Item.FindControl("txtNoRangkaEdit"), TextBox)
                Dim icTglKerusakanEdit As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icTglKerusakanEdit"), KTB.DNet.WebCC.IntiCalendar)
                Dim txtOdometerEdit As TextBox = CType(e.Item.FindControl("txtOdometerEdit"), TextBox)
                Dim txtCatatanEdit As TextBox = CType(e.Item.FindControl("txtCatatanEdit"), TextBox)

                Dim objChassis As ChassisMaster

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

                objChassis = New ChassisMasterFacade(User).Retrieve(txtNoRangka.Text.Trim())

                If Not QRSIsSameCategory(objChassis, objChassisMain) Then
                    MessageBox.Show("Kategori No Rangka di QRS berbeda dengan No Rangka PQR")
                    RefreshGrid()
                    Return
                End If

                If Not (IsNothing(objChassis) OrElse objChassis.ID = 0) Then
                    If Not QRSIsExist(objChassis.ID, _arrQRS, e.Item.ItemIndex) Then
                        Dim objPQRQRS As PQRQRS = CType(_arrQRS(e.Item.ItemIndex), PQRQRS)
                        objPQRQRS.ChassisMaster = objChassis
                        objPQRQRS.TglKerusakan = icTglKerusakanEdit.Value
                        objPQRQRS.Odometer = CInt(txtOdometerEdit.Text)
                        objPQRQRS.Note = txtCatatanEdit.Text

                        _arrQRS(e.Item.ItemIndex) = objPQRQRS

                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrQRS)
                        Else
                            sessHelper.SetSession("QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrQRS)

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
            dgQRS.DataSource = CType(sessHelper.GetSession("NEW_QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
            dgQRS.DataBind()
        Else
            dgQRS.DataSource = CType(sessHelper.GetSession("QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
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
        Dim oPqrHeader As PQRHeader = sessHelper.GetSession("inputWSCSes" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""))
        Dim vCode As String = Nothing
        If txtNoChasis.Text.Trim <> "" Then
            vCode = CType(New ChassisMasterFacade(User).RetrieveByChassisNumbers(txtNoChasis.Text.Trim)(0), ChassisMaster).VechileColor.VechileType.VechileTypeCode
        End If
        Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblSearchDamageFooter"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpPositionCodeSelection.aspx?vCode=" & vCode, "", 710, 700, "GetSelectedDamageCode")
    End Sub
    Private Sub SetdgKerusakanItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim vCode As String = Nothing
        If txtNoChasis.Text.Trim <> "" Then
            vCode = CType(New ChassisMasterFacade(User).RetrieveByChassisNumbers(txtNoChasis.Text.Trim)(0), ChassisMaster).VechileColor.VechileType.VechileTypeCode
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
                For Each _PQRDamageCode As PQRDamageCode In DamageCodeCollection
                    If _PQRDamageCode.DeskripsiKodePosisi.ID = CodePositionID Then
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
                For Each _PQRDamageCode As PQRDamageCode In DamageCodeCollection
                    If _PQRDamageCode.DeskripsiKodePosisi.ID = CodePositionID AndAlso nIndeks <> i Then
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
        Dim _arrDamageCode As ArrayList = CType(sessHelper.GetSession("DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrDamageCode = CType(sessHelper.GetSession("NEW_DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
        End If
        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid

                Dim txtDamageCode As TextBox = CType(e.Item.FindControl("txtKodeDamageFooter"), TextBox)
                Dim objKodePosisi As DeskripsiKodePosisi

                If IsNothing(txtDamageCode) OrElse txtDamageCode.Text = String.Empty Then
                    MessageBox.Show("Posisi Kerusakan masih kosong")
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

                If oChassisFacade.IsExist(txtNoChasis.Text) Then
                    oChassis = oChassisFacade.Retrieve(txtNoChasis.Text)
                Else
                    MessageBox.Show("Nomor Rangka tidak terdaftar.")
                End If
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(LaborMaster), "VechileType.ID", MatchType.Exact, oChassis.VechileColor.VechileType.ID))
                crit.opAnd(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, txtDamageCode.Text))
                Dim PosCodeCount As ArrayList = New LaborMasterFacade(User).Retrieve(crit)
                If PosCodeCount.Count < 1 Then
                    MessageBox.Show("Posisi kerusakan " & txtDamageCode.Text & " untuk nomor rangka " & txtNoChasis.Text & " tidak terdaftar.\nSilahkan hubungi MMKSI")
                    RefreshGrid()
                    Return
                End If


                objKodePosisi = New DeskripsiPositionCodeFacade(User).Retrieve(txtDamageCode.Text.Trim())

                If Not (IsNothing(objKodePosisi) OrElse objKodePosisi.ID = 0) Then
                    If Not DamageCodeIsExist(objKodePosisi.ID, _arrDamageCode) Then
                        Dim objPQRDamageCode As PQRDamageCode = New PQRDamageCode

                        objPQRDamageCode.DeskripsiKodePosisi = objKodePosisi

                        _arrDamageCode.Add(objPQRDamageCode)


                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrDamageCode)
                        Else
                            sessHelper.SetSession("DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrDamageCode)
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
                    Dim deletedDamageCode As PQRDamageCode = CType(_arrDamageCode(e.Item.ItemIndex), PQRDamageCode)
                    If deletedDamageCode.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDDAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
                        deletedArrLst.Add(deletedDamageCode)
                        sessHelper.SetSession("DELETEDDAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), deletedArrLst)
                    End If
                    _arrDamageCode.RemoveAt(e.Item.ItemIndex)
                End If


            Case "Save" 'Update this datagrid item                 

                Dim txtDamageCode As TextBox = CType(e.Item.FindControl("txtKodeDamageEdit"), TextBox)

                '
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
                        Dim objPQRDamageCode As PQRDamageCode = CType(_arrDamageCode(e.Item.ItemIndex), PQRDamageCode)
                        objPQRDamageCode.DeskripsiKodePosisi = objKodePosisi

                        _arrDamageCode(e.Item.ItemIndex) = objPQRDamageCode
                        '_arrDamageCode.Insert(e.Item.ItemIndex, objPQRDamageCode)
                        '_arrDamageCode.Add(objPQRDamageCode)

                        sessHelper.SetSession("DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrDamageCode)
                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrDamageCode)
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
            dgKerusakan.DataSource = CType(sessHelper.GetSession("NEW_DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
            dgKerusakan.DataBind()
        Else
            dgKerusakan.DataSource = CType(sessHelper.GetSession("DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
            dgKerusakan.DataBind()

        End If
        If ddlPqrType.SelectedValue = "3" Then
            dgKerusakan.Columns(dgKerusakan.Columns.Count - 1).Visible = False
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
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpPartsCodeSelection.aspx?type=" & ddlPqrType.SelectedValue, "", 710, 700, "GetSelectedPartsCode")
    End Sub
    Private Sub SetdgPartsItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        'Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        'e.Item.Cells(0).Controls.Clear()
        'e.Item.Cells(0).Controls.Add(lNum)
        Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblSearchPartsEdit"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpPartsCodeSelection.aspx?type=" & ddlPqrType.SelectedValue, "", 710, 700, "GetSelectedPartsCode")
    End Sub
    Private Sub SetdgPartsItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        e.Item.Cells(0).Controls.Add(lNum)

    End Sub
    Private Function PartsCodeIsExist(ByVal SparePartID As Integer, ByVal PartsCodeCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If PartsCodeCollection.Count > 0 Then
            For Each _PQRPartsCode As PQRPartsCode In PartsCodeCollection
                If _PQRPartsCode.SparePartMaster.ID = SparePartID Then
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
            For Each _PQRPartsCode As PQRPartsCode In PartsCodeCollection
                If _PQRPartsCode.SparePartMaster.ID = SparePartID AndAlso nIndeks <> i Then
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
        Dim _arrPartsCode As ArrayList = CType(sessHelper.GetSession("PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrPartsCode = CType(sessHelper.GetSession("NEW_PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
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
                        Dim objPQRPartsCode As PQRPartsCode = New PQRPartsCode

                        objPQRPartsCode.SparePartMaster = objSparePartMaster

                        _arrPartsCode.Add(objPQRPartsCode)

                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrPartsCode)
                        Else
                            sessHelper.SetSession("PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrPartsCode)
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
                    Dim deletedPartsCode As PQRPartsCode = CType(_arrPartsCode(e.Item.ItemIndex), PQRPartsCode)
                    If deletedPartsCode.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDPARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
                        deletedArrLst.Add(deletedPartsCode)
                        sessHelper.SetSession("DELETEDPARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), deletedArrLst)
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
                        Dim objPQRPartsCode As PQRPartsCode = CType(_arrPartsCode(e.Item.ItemIndex), PQRPartsCode)

                        objPQRPartsCode.SparePartMaster = objSparePartMaster

                        _arrPartsCode(e.Item.ItemIndex) = objPQRPartsCode

                        sessHelper.SetSession("PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrPartsCode)
                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrPartsCode)
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
            dgParts.DataSource = CType(sessHelper.GetSession("NEW_PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
            dgParts.DataBind()
        Else
            dgParts.DataSource = CType(sessHelper.GetSession("PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
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
            For Each _PQRAttachment As PQRAttachment In AttachmentCollection
                If _PQRAttachment.FileName = FileName Then
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
            For Each _PQRAttachment As PQRAttachment In AttachmentCollection
                If _PQRAttachment.FileName = FileName AndAlso nIndeks <> i Then
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
        Dim _arrAttachmentTop As ArrayList = CType(sessHelper.GetSession("ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrAttachmentTop = CType(sessHelper.GetSession("NEW_ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
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

                        Dim objPQRAttachmentTop As PQRAttachment = New PQRAttachment
                        Dim FileName As String

                        objPQRAttachmentTop.NewItem = True
                        objPQRAttachmentTop.Type = EnumPQR.AttachmentLocation.Top
                        objPQRAttachmentTop.AttachmentType = objPostedData.ContentType
                        objPQRAttachmentTop.AttachmentData = objPostedData
                        FileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)
                        objPQRAttachmentTop.Attachment = AttachmentDirectory & "\" & DateTime.Today.ToString("dd-MM-yyyy") & "\" & DateTime.Now.ToString("ddMMyyyyHHmmss_") & objPQRAttachmentTop.Type.ToString & "_" & FileName

                        UploadAttachment(objPQRAttachmentTop, TempDirectory)

                        _arrAttachmentTop.Add(objPQRAttachmentTop)

                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrAttachmentTop)
                        Else
                            sessHelper.SetSession("ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrAttachmentTop)

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
                    RemovePQRAttachment(CType(_arrAttachmentTop(e.Item.ItemIndex), PQRAttachment), TempDirectory)
                    _arrAttachmentTop.RemoveAt(e.Item.ItemIndex)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    Dim deletedAttachment As PQRAttachment = CType(_arrAttachmentTop(e.Item.ItemIndex), PQRAttachment)
                    If deletedAttachment.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDATTACHMENT" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
                        deletedArrLst.Add(deletedAttachment)
                        sessHelper.SetSession("DELETEDATTACHMENT" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), deletedArrLst)
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
            dgFileAttachmentTop.DataSource = CType(sessHelper.GetSession("NEW_ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
            dgFileAttachmentTop.DataBind()
        Else
            dgFileAttachmentTop.DataSource = CType(sessHelper.GetSession("ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
            dgFileAttachmentTop.DataBind()
            If CType(sessHelper.GetSession("ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList).Count > 0 Then
                btnDownloadAll.Visible = True
            End If
        End If

    End Sub

    Protected Sub btnDownloadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownloadAll.Click
        Dim _arrPQREvidence As ArrayList = CType(sessHelper.GetSession("ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrPQREvidence = CType(sessHelper.GetSession("NEW_ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
        End If
        Dim nameGuid As String = Guid.NewGuid().ToString().Substring(0, 5)
        If txtNoChasis.Text.Trim.Length > 0 Then
            nameGuid = txtNoChasis.Text & "_" & lblPQRNoVal.Text.Replace("/", "_")
        End If

        Dim zipName = String.Empty
        If Not checkFilesOK(_arrPQREvidence) Then
            MessageBox.Show("File belum tersimpan!")
            Exit Sub
        End If
        ZipIt(_arrPQREvidence, nameGuid, zipName)
        Dim fInfo As FileInfo = New FileInfo(zipName)
        Try
            Response.Redirect("../Download.aspx?file=" & fInfo.FullName)
        Catch ex As Exception
            MessageBox.Show(SR.DownloadFail(fInfo.Name))
        End Try
    End Sub

    Private Sub ZipIt(ByVal arrLampiran As ArrayList, ByVal targetName As String, ByRef zipName As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False


        Try
            success = imp.Start()
            If success Then

                If arrLampiran.Count = 0 Then
                    MessageBox.Show("Tidak ada file yang akan didownload")
                    Return
                End If

                If targetName.Length = 0 Then
                    MessageBox.Show("Zip file name error")
                    Return
                End If

                zipName = TargetDirectory & KTB.DNet.Lib.WebConfig.GetValue("PQRAttachmentDir") & "\" & targetName & ".zip"
                Dim zipInfo As FileInfo = New FileInfo(zipName)
                If zipInfo.Exists Then
                    zipInfo.Delete()
                End If

                Using strmZipOutputStream As New ZipOutputStream(File.Create(zipName))
                    strmZipOutputStream.SetLevel(9) ' Highest Compression
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()
                End Using


                Using zipFile As New ZipFile(zipName)
                    zipFile.BeginUpdate()

                    For Each _wscEvidence As PQRAttachment In arrLampiran
                        Dim fInfo As FileInfo = New FileInfo(TargetDirectory & _wscEvidence.Attachment)
                        If fInfo.Exists Then
                            zipFile.Add(fInfo.FullName)
                        End If
                    Next

                    zipFile.CommitUpdate()
                    zipFile.Close()
                End Using

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            imp.StopImpersonate()
            imp = Nothing
            Throw ex
        End Try
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
        Dim _arrAttachmentBottom As ArrayList = CType(sessHelper.GetSession("ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrAttachmentBottom = CType(sessHelper.GetSession("NEW_ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
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

                        Dim objPQRAttachmentBottom As PQRAttachment = New PQRAttachment
                        Dim FileName As String

                        objPQRAttachmentBottom.NewItem = True
                        objPQRAttachmentBottom.Type = EnumPQR.AttachmentLocation.Bottom
                        objPQRAttachmentBottom.AttachmentType = objPostedData.ContentType
                        objPQRAttachmentBottom.AttachmentData = objPostedData
                        FileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)
                        objPQRAttachmentBottom.Attachment = AttachmentDirectory & "\" & DateTime.Today.ToString("dd-MM-yyyy") & "\" & DateTime.Now.ToString("ddMMyyyyHHmmss_") & objPQRAttachmentBottom.Type.ToString & "_" & FileName

                        UploadAttachment(objPQRAttachmentBottom, TempDirectory)

                        _arrAttachmentBottom.Add(objPQRAttachmentBottom)

                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrAttachmentBottom)
                        Else
                            sessHelper.SetSession("ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrAttachmentBottom)

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
                    RemovePQRAttachment(CType(_arrAttachmentBottom(e.Item.ItemIndex), PQRAttachment), TempDirectory)
                    _arrAttachmentBottom.RemoveAt(e.Item.ItemIndex)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    Dim deletedAttachment As PQRAttachment = CType(_arrAttachmentBottom(e.Item.ItemIndex), PQRAttachment)
                    If deletedAttachment.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDATTACHMENT" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
                        deletedArrLst.Add(deletedAttachment)
                        sessHelper.SetSession("DELETEDATTACHMENT" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), deletedArrLst)
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
            dgFileAttachmentBottom.DataSource = CType(sessHelper.GetSession("NEW_ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
            dgFileAttachmentBottom.DataBind()
        Else
            dgFileAttachmentBottom.DataSource = CType(sessHelper.GetSession("ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
            dgFileAttachmentBottom.DataBind()
        End If

    End Sub
#End Region

    'Private Sub Attachment_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim objSender As LinkButton = CType(sender, LinkButton)
    '    Response.Redirect("../Download.aspx?file=" & objSender.CommandArgument)
    'End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan2.Click
        Dim result As Integer
        Dim ErrMessage As String = String.Empty

        If Not Page.IsValid Then
            Return
        End If

        btnSimpan.Attributes("OnClientClick") = "this.disabled=true; this.value='Sending…';"
        If ValidateSaveData() Then
            If oChassisFacade.IsExist(txtNoChasis.Text) Then
                oChassis = oChassisFacade.Retrieve(txtNoChasis.Text)
            Else
                MessageBox.Show("Nomor Rangka tidak terdaftar.")
            End If

            'For Each item As DataGridItem In dgKerusakan.Items

            'Next

            BindPQRHeaderDomain()
            If Mode = enumMode.Mode.NewItemMode Then
                oPQRHeader = sessHelper.GetSession("NEW_PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""))
            End If
            If oPQRHeader.PQRQRSs.Count > 0 Then
                For Each item As PQRQRS In oPQRHeader.PQRQRSs
                    If Not QRSIsSameCategory(item.ChassisMaster, oPQRHeader.ChassisMaster) Then
                        MessageBox.Show("Kategori No Rangka di QRS berbeda dengan No Rangka PQR")
                        Return
                    End If
                Next
            End If

            Dim objRenderPanel As RenderingProfile = New RenderingProfile
            Dim ProfileCollection1 As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("pqr_prf"), CType(EnumProfileType.ProfileType.PQR, Short), User)
            Dim ProfileCollection2 As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), CType(EnumProfileType.ProfileType.PQR, Short), User)

            Mode = CType(ViewState("Mode"), enumMode.Mode)
            If Mode = enumMode.Mode.NewItemMode Then
                result = oPQRHeaderFacade.InsertTransaction(oPQRHeader, CType(sessHelper.GetSession("NEW_DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), CType(sessHelper.GetSession("NEW_PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), CType(sessHelper.GetSession("NEW_ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), CType(sessHelper.GetSession("NEW_ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), ProfileCollection1, ProfileCollection2, CType(sessHelper.GetSession("NEW_QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList))
            ElseIf Mode = enumMode.Mode.EditMode Then
                result = oPQRHeaderFacade.UpdateTransaction(oPQRHeader, CType(sessHelper.GetSession("DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), CType(sessHelper.GetSession("DELETEDDAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), CType(sessHelper.GetSession("PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), CType(sessHelper.GetSession("DELETEDPARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), CType(sessHelper.GetSession("ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), CType(sessHelper.GetSession("ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), CType(sessHelper.GetSession("DELETEDATTACHMENT" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), ProfileCollection1, ProfileCollection2, New ProfileGroupFacade(User).Retrieve("pqr_prf"), New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), CType(sessHelper.GetSession("QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), CType(sessHelper.GetSession("DELETEDQRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), ErrMessage)
            End If


            If result > 0 Then
                ' Proses Upload di pisahkan dari simpan 
                'UploadAttachment(CType(sessHelper.GetSession("ATTACHMENTBOTTOM"), ArrayList), TargetDirectory)
                'UploadAttachment(CType(sessHelper.GetSession("ATTACHMENTTOP"), ArrayList), TargetDirectory)
                If Mode = enumMode.Mode.NewItemMode Then
                    CommitAttachment(CType(sessHelper.GetSession("NEW_ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList))
                    CommitAttachment(CType(sessHelper.GetSession("NEW_ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList))

                Else
                    CommitAttachment(CType(sessHelper.GetSession("ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList))
                    CommitAttachment(CType(sessHelper.GetSession("ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList))

                End If
                If Mode = enumMode.Mode.EditMode Then
                    RemovePQRAttachment(CType(sessHelper.GetSession("DELETEDATTACHMENT" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList), TargetDirectory)
                End If
                'ClearTempData()

                'If (ddlPqrType.SelectedIndex - 1 = 1) Then
                If (ddlPqrType.SelectedValue = 1) Then
                    MessageBox.Show("Simpan Data Berhasil!")
                    'ReloadForm(result)
                    'If ViewState("Mode") = enumMode.Mode.NewItemMode Then
                    Server.Transfer("~/PQR/FrmPQRList.aspx")
                    'End If
                Else
                    MessageBox.Show("Simpan Data Berhasil!")

                    'Add by Reza
                    Dim _criterias As WSCHeader
                    Dim _criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim _PQRNo As String = genPQRNo(oPQRHeader.ID)
                    _criteria.opAnd(New Criteria(GetType(WSCHeader), "PQR", MatchType.Exact, _PQRNo))
                    Dim PQRF As ArrayList = New WSCHeaderFacade(User).Retrieve(_criteria)
                    'If ddlPqrType.SelectedIndex = 1 Then
                    'If ddlPqrType.SelectedValue = 0 Then
                    If ddlPqrType.SelectedValue = 0 Or ddlPqrType.SelectedValue = 4 Then  'edited 15122020
                        If PQRF.Count = 0 Then
                            sessHelper.SetSession("inputWSCSes" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), oPQRHeader)
                            ValidatePqrWSC()
                        Else
                            For Each asd As WSCHeader In PQRF
                                If asd.PQR = oPQRHeader.PQRNo Then
                                    Exit For
                                End If
                            Next
                        End If
                    Else
                        Server.Transfer("~/PQR/FrmPQRList.aspx")
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
        sessHelper.SetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), oPQRHeaderFacade.Retrieve(id))
        oPQRHeader = CType(sessHelper.GetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader)

        'sessHelper.SetSession("DAMAGECODE", oPQRHeader.PQRDamageCodes)
        'sessHelper.SetSession("DELETEDDAMAGECODE", New ArrayList)

        'sessHelper.SetSession("PARTS", oPQRHeader.PQRPartsCodes)
        'sessHelper.SetSession("DELETEDPARTS", New ArrayList)

        'sessHelper.SetSession("ATTACHMENTTOP", GetAttachmentList(oPQRHeader.PQRAttachments, EnumPQR.AttachmentLocation.Top))
        'sessHelper.SetSession("ATTACHMENTBOTTOM", GetAttachmentList(oPQRHeader.PQRAttachments, EnumPQR.AttachmentLocation.Bottom))
        'sessHelper.SetSession("DELETEDATTACHMENT", New ArrayList)

        If oDealer.Title = 0 Then 'Dealer
            Select Case CType(oPQRHeader.RowStatus, EnumPQR.PQRStatus)
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
            Select Case CType(oPQRHeader.RowStatus, EnumPQR.PQRStatus)
                Case EnumPQR.PQRStatus.Batal
                    ViewState("Mode") = enumMode.Mode.ViewMode
                Case EnumPQR.PQRStatus.Baru
                    If SecurityProvider.Authorize(Context.User, SR.PQRNewSave_Privilege) Then
                        ViewState("Mode") = enumMode.Mode.EditMode
                    Else
                        ViewState("Mode") = enumMode.Mode.ViewMode
                    End If
                Case EnumPQR.PQRStatus.Validasi
                    ViewState("Mode") = enumMode.Mode.ViewMode
                Case EnumPQR.PQRStatus.Proses
                    If SecurityProvider.Authorize(Context.User, SR.PQRNewSave_Privilege) Then
                        ViewState("Mode") = enumMode.Mode.ViewMode
                    Else
                        ViewState("Mode") = enumMode.Mode.EditMode
                    End If
                Case EnumPQR.PQRStatus.Rilis
                    If SecurityProvider.Authorize(Context.User, SR.PQRNewSave_Privilege) Then
                        ViewState("Mode") = enumMode.Mode.ViewMode
                    Else
                        ViewState("Mode") = enumMode.Mode.EditMode
                    End If
                Case EnumPQR.PQRStatus.Selesai
                    ViewState("Mode") = enumMode.Mode.ViewMode
            End Select
        End If

        If Not IsNothing(Request.QueryString("Src")) Then
            If Request.QueryString("Src").ToString.ToUpper = "WSCList".ToUpper Then
                Server.Transfer("~/Service/FrmWSCHeader.aspx?screenFrom=WSC&WSCId=" & Request.QueryString("WSCId") & "&viewStateMode=" & Request.QueryString("State"))
            End If
        End If

        If CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.EditMode Then
            Server.Transfer("~/PQR/FrmPQRHeader.aspx?Mode=Edit&PQRID=" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""))
        ElseIf CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.ViewMode Then
            Server.Transfer("~/PQR/FrmPQRHeader.aspx?Mode=View&PQRID=" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""))
        End If
        'fillForm()
    End Sub
    Private Sub RemovePQRAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As PQRAttachment In AttachmentCollection
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
    Private Sub RemovePQRAttachment(ByVal ObjAttachment As PQRAttachment, ByVal TargetPath As String)
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
                For Each obj As PQRAttachment In AttachmentCollection
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
    Private Sub UploadAttachment(ByVal ObjAttachment As PQRAttachment, ByVal TargetPath As String)
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
                For Each obj As PQRAttachment In AttachmentCollection
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

    Private Sub BindPQRHeaderDomain()
        Mode = CType(ViewState("Mode"), enumMode.Mode)

        If Mode = enumMode.Mode.NewItemMode Then
            oPQRHeader = CType(sessHelper.GetSession("NEW_PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader)
            oPQRHeader.Dealer = oDealer
            If oDealer.ID = 2 Then 'KTB STS
                oPQRHeader.PQRNo = "KTBSTS" & ";" & oChassis.Category.CategoryCode
            Else
                If ddlPqrType.SelectedValue = "2" Then
                    oPQRHeader.PQRNo = oDealer.DealerCode & ";SP"
                ElseIf ddlPqrType.SelectedValue = "3" Then
                    oPQRHeader.PQRNo = oDealer.DealerCode & ";ACC"
                Else
                    oPQRHeader.PQRNo = oDealer.DealerCode & ";" & oChassis.Category.CategoryCode
                End If
            End If

        ElseIf Mode = enumMode.Mode.EditMode Then
            oPQRHeader.PQRNo = lblPQRNoVal.Text
            'oPQRHeader.Dealer = oDealer 'lblDealer.Text.Trim.Split("-")(0)
        End If
        'oPQRHeader.PQRType = ddlPqrType.SelectedIndex - 1
        oPQRHeader.PQRType = ddlPqrType.SelectedValue
        'oPQRHeader.RefPQRNo = txtRefPQRNo.Text
        oPQRHeader.Category = oChassis.Category
        'oPQRHeader.DocumentDate = DateTime.Today
        oPQRHeader.ChassisMaster = oChassis
        oPQRHeader.Year = IIf(oChassis.ProductionYear <= 0, 0, oChassis.ProductionYear)
        oPQRHeader.PQRDate = icTglKerusakan.Value
        If Mode = enumMode.Mode.NewItemMode Then
            oPQRHeader.DocumentDate = Date.Now
        End If
        oPQRHeader.OdoMeter = CInt(txtOdometer.Text)
        oPQRHeader.Velocity = CInt(txtKecepatan.Text)
        If oChassis.EndCustomer Is Nothing Then
            oPQRHeader.CustomerName = String.Empty
            oPQRHeader.CustomerAddress = String.Empty
            oPQRHeader.SoldDate = New DateTime(1753, 1, 1)
        Else
            If Not oChassis.EndCustomer Is Nothing Then
                oPQRHeader.SoldDate = oChassis.EndCustomer.OpenFakturDate
                If Not oChassis.EndCustomer.Customer Is Nothing Then
                    oPQRHeader.CustomerName = oChassis.EndCustomer.Customer.Name1
                    oPQRHeader.CustomerAddress = oChassis.EndCustomer.Customer.Alamat
                End If
            End If
        End If
        oPQRHeader.Subject = txtSubject.Text
        oPQRHeader.Symptomps = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtGejala.Text.Trim)
        oPQRHeader.Causes = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtPenyebab.Text.Trim)
        oPQRHeader.Results = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtHasil.Text.Trim)
        oPQRHeader.Notes = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtCatatan.Text.Trim)
        oPQRHeader.Solutions = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtSolution.Text)
        oPQRHeader.InstallDate = icTglPemasangan.Value
        oPQRHeader.InstallOdometer = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtOdoPemasangan.Text.Trim)
        If Not IsNothing(txtWONumber.Text) AndAlso txtWONumber.Text <> "" Then
            oPQRHeader.WorkOrderNumber = txtWONumber.Text
        End If


        ' Add Branch
        If Not IsNothing(txtDealerBranchCode.Text) AndAlso txtDealerBranchCode.Text <> "" Then
            Dim branchCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            branchCriteria.opAnd(New Criteria(GetType(DealerBranch), "DealerBranchCode", MatchType.Exact, txtDealerBranchCode.Text))
            Dim branchList As ArrayList = New DealerBranchFacade(HttpContext.Current.User).Retrieve(branchCriteria)
            If branchList.Count = 1 Then
                ' Dealer Branch Found
                oPQRHeader.DealerBranch = branchList(0)
            End If
        End If


        If ddlBobot.Visible = True Then
            oPQRHeader.Bobot = ddlBobot.SelectedValue
        Else
            oPQRHeader.Bobot = 0
        End If
        If tblKodeKerusakan.Visible And ddlKodeWSCA.Enabled Then
            If Me.ddlKodeWSCA.SelectedValue <> "-1" Then
                oPQRHeader.CodeA = Me.ddlKodeWSCA.SelectedValue
            End If
            If Me.ddlKodeWSCB.SelectedValue <> "-1" Then
                oPQRHeader.CodeB = Me.ddlKodeWSCB.SelectedValue
            End If
            If Me.ddlKodeWSCC.SelectedValue <> "-1" Then
                oPQRHeader.CodeC = Me.ddlKodeWSCC.SelectedValue
            End If
        End If

        'oPQRHeader.PQRType = ddlPqrType.SelectedIndex - 1
        oPQRHeader.PQRType = ddlPqrType.SelectedValue
        If Mode = enumMode.Mode.NewItemMode Then
            sessHelper.SetSession("NEW_PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), oPQRHeader)

        End If

    End Sub


    Private Function ValidateSaveData() As Boolean

        'If txtRefPQRNo.Text = String.Empty Then
        '    MessageBox.Show("Silakan masukan no PQR Referensi ")
        '    Return False
        'End If
        Dim objCM As ChassisMaster
        If txtNoChasis.Text = String.Empty Then
            MessageBox.Show("No Rangka masih kosong")
            Return False
        End If

        If oChassisFacade.IsExist(txtNoChasis.Text) Then
            txtNoChasis.ForeColor = Color.Black
            objCM = CType(oChassisFacade.Retrieve(txtNoChasis.Text), ChassisMaster)
            LoadChassisInfo(objCM)
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

        'add validasi untuk check MSPExRegist
        If ddlPqrType.SelectedValue = 4 Then
            'txtNoChasis.Text
            'txtOdometer.Text
            Dim mspExReg As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByChassisNumber(txtNoChasis.Text.Trim)
            If mspExReg.ID = 0 Then
                MessageBox.Show("Nomor Rangka Tidak Terdaftar MSP Extended")
                Return False
            Else
                If mspExReg.ValidDateTo < Date.Now.ToShortDateString Then
                    MessageBox.Show("Masa warranty dengan Tipe PQR Extended Waranty untuk Chassis ini sudah lewat ")
                    Return False
                End If

                If mspExReg.WarrantyValidKMTo < CInt(txtOdometer.Text.Trim) Then
                    MessageBox.Show("Jarak warranty untuk Chassis ini dengan Tipe PQR Extended Waranty sudah habis ")
                    Return False
                End If

                If mspExReg.Status <> CType(EnumMSPEx.MSPExStatus.Selesai, Short) Then
                    MessageBox.Show("Data tidak dapat disimpan karena Status Registrasi MSP Extended belum Selesai ")
                    Return False
                End If

                If mspExReg.ValidDateTo < Now.ToShortDateString Then
                    MessageBox.Show("Data tidak dapat disimpan karena Valid Date Registrasi MSP Extended sudah habis ")
                    Return False
                End If

            End If

            'Dim ValidDate As Date
            'Dim StatusMSPEx As String
            'Dim critFSts As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'critFSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, txtNoChasis.Text))

            'Dim arlFSts As ArrayList = New MSPExRegistrationFacade(User).Retrieve(critFSts)
            'If arlFSts.Count > 0 Then
            '    StatusMSPEx = CType(arlFSts(0), MSPExRegistration).Status
            '    If StatusMSPEx <> CType(EnumMSPEx.MSPExStatus.Selesai, Short) Then
            '        MessageBox.Show("Data tidak dapat disimpan karena Status Registrasi MSP Extended belum Selesai ")
            '        Return False
            '    Else
            '        ValidDate = CType(arlFSts(0), MSPExRegistration).ValidDateTo
            '        If ValidDate < Now.ToShortDateString Then
            '            MessageBox.Show("Data tidak dapat disimpan karena Valid Date Registrasi MSP Extended sudah habis ")
            '            Return False
            '        End If
            '    End If
            'End If
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
                Dim arrKerusakan As ArrayList = CType(sessHelper.GetSession("NEW_DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
                If arrKerusakan.Count = 0 Then
                    MessageBox.Show("Posisi kerusakan harus diisi")
                    Return False
                End If
            Else
                Dim arrKerusakan As ArrayList = CType(sessHelper.GetSession("DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
                If arrKerusakan.Count = 0 Then
                    MessageBox.Show("Posisi kerusakan harus diisi")
                    Return False
                End If
            End If

            If ddlPqrType.SelectedValue = 2 OrElse ddlPqrType.SelectedValue = 3 Then
                Mode = CType(ViewState("Mode"), enumMode.Mode)
                Dim arrAttachment As New ArrayList
                If Mode = enumMode.Mode.NewItemMode Then
                    arrAttachment = CType(sessHelper.GetSession("NEW_ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)

                Else
                    arrAttachment = CType(sessHelper.GetSession("ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)

                End If
                If arrAttachment.Count = 0 Then
                    MessageBox.Show("Untuk PQR tipe Parts atau Aksesories, harus mengisi lampiran berupa Invoice atau Work Order")
                    Return False
                End If
            End If

            Dim dDiff As Integer = DateDiff(DateInterval.Day, icTglPemasangan.Value.Date, Date.Now.Date)
            If dDiff > 365 AndAlso ddlPqrType.SelectedValue = "2" Then
                MessageBox.Show("Spare Part melebihi waktu Warranty")
                Return False
            End If

            If dDiff > (365 * 3) AndAlso ddlPqrType.SelectedValue = "3" Then
                MessageBox.Show("Accessories melebihi waktu Warranty")
                Return False
            End If

            If txtOdometer.Text.Trim.Length <> 0 AndAlso txtOdoPemasangan.Text.Trim.Length <> 0 Then
                Try
                    Dim curOdo As Integer = CType(txtOdometer.Text.Trim, Integer)
                    Dim insOdo As Integer = CType(txtOdoPemasangan.Text.Trim, Integer)
                    If curOdo - insOdo > 20000 AndAlso ddlPqrType.SelectedValue = "2" Then
                        MessageBox.Show("Spare Part melebihi jarak Warranty")
                        Return False
                    End If

                    If curOdo - insOdo > 60000 AndAlso ddlPqrType.SelectedValue = "3" Then
                        MessageBox.Show("Accessories melebihi jarak Warranty")
                        Return False
                    End If

                Catch ex As Exception
                    MessageBox.Show("Periksa kembali kolom Odometer dan Odometer Pemasangan")
                    Return False
                End Try
            End If

            'TODO
            'If ddlPqrType.SelectedValue > 1 Then
            If ddlPqrType.SelectedValue > 1 And ddlPqrType.SelectedValue <> 4 Then 'edited15122020
                If dgParts.Items.Count > 0 Then
                    For Each item As DataGridItem In dgParts.Items
                        If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                            Dim lblKodeParts As Label = CType(item.FindControl("lblKodeParts"), Label)
                            If lblKodeParts.Text = "SPB02001" OrElse lblKodeParts.Text = "SPB02002" OrElse lblKodeParts.Text = "SPB07001" Then
                                If DateDiff(DateInterval.Year, icTglPemasangan.Value, Date.Now) > 1 Then
                                    MessageBox.Show("Part SPB02001, SPB02002, dan SPB07001 warranty maksimal 1 tahun dari tanggal Pemasangan")
                                    Return False
                                End If
                            End If
                        End If
                    Next
                End If

                If icTglPemasangan.Value < New DateTime(2019, 12, 1) Then
                    MessageBox.Show("Tanggal pemasangan minimal tanggal 1 Desember 2019")
                    Return False
                End If
                Dim isWarantyValid As Boolean = False
                Dim _OdoPemasangan As Integer = 0
                Dim _Odometer As Integer = 0

                Integer.TryParse(txtOdoPemasangan.Text.Trim().Replace(".", ""), _OdoPemasangan)
                Integer.TryParse(txtOdometer.Text.Trim().Replace(".", ""), _Odometer)

                If objCM.Category.CategoryCode = "PC" Then
                    If DateDiff(DateInterval.Month, objCM.EndCustomer.FakturDate, Date.Now) > 36 Then
                        isWarantyValid = True
                    End If

                    If _OdoPemasangan > 100000 OrElse _Odometer > 100000 Then
                        isWarantyValid = True
                    End If
                    'validasi type PQR Extended
                    If ddlPqrType.SelectedValue = 4 Then
                        isWarantyValid = True
                    End If

                    If (isWarantyValid = False) Then
                        MessageBox.Show("Warranty s/p & accessories berlaku setelah warranty kendaraan habis (PC 3 tahun/100.000km)")
                        Return False
                    End If
                    'If DateDiff(DateInterval.Month, objCM.EndCustomer.FakturDate, Date.Now) > 36 OrElse txtOdoPemasangan.Text > 100000 OrElse txtOdometer.Text > 100000 Then
                    '    MessageBox.Show("Kendaraan bertipe PC warranty hanya berlaku 3 tahun setelah tanggal faktur dan 100.000KM pada Odometer")
                    '    Return False
                    'End If
                ElseIf objCM.Category.CategoryCode = "LCV" Then
                    If DateDiff(DateInterval.Month, objCM.EndCustomer.FakturDate, Date.Now) > 24 Then
                        isWarantyValid = True
                    End If

                    If _OdoPemasangan > 50000 OrElse _Odometer > 50000 Then
                        isWarantyValid = True
                    End If
                    'validasi type PQR Extended
                    If ddlPqrType.SelectedValue = 4 Then
                        isWarantyValid = True
                    End If

                    If (isWarantyValid = False) Then
                        MessageBox.Show("Warranty s/p & accessories berlaku setelah warranty kendaraan habis (LCV 2 tahun/50.000km)")
                        Return False
                    End If

                    'If DateDiff(DateInterval.Year, objCM.EndCustomer.FakturDate, Date.Now) > 2 OrElse txtOdoPemasangan.Text > 50000 OrElse txtOdometer.Text > 50000 Then
                    '    MessageBox.Show("Kendaraan bertipe LCV warranty hanya berlaku 2 tahun setelah tanggal faktur dan 50.000KM pada Odometer")
                    '    Return False
                    'End If
                End If

                If txtDealerInvoice.Text = "" Then
                    MessageBox.Show("Dealer Invoice masih kosong")
                    Return False
                End If

                If txtDealerInvoice.Text <> oDealer.DealerCode Then
                    MessageBox.Show("Dealer Invoice tidak sama dengan Dealer Login")
                    Return False
                End If
            End If

        Else
            Mode = CType(ViewState("Mode"), enumMode.Mode)
            If Mode = enumMode.Mode.NewItemMode Then
                Dim arrAttachment As ArrayList = CType(sessHelper.GetSession("NEW_ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), ArrayList)
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
    Private Sub LoadChassisInfo(ByRef obj As ChassisMaster)
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

        lnkbtnPopUpInfoKendaraan.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpInfoKendaraan.aspx?cn=" & obj.ChassisNumber, "", 710, 700, "ShowPopUp")
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
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim strChassisNumber = txtNoChasis.Text.Trim()
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, strChassisNumber))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
            Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
            If ChassisColl.Count > 0 Then
                Dim oChaColl As ChassisMaster = ChassisColl(0)
                If IsNothing(oChaColl.EndCustomer) Then
                    MessageBox.Show("Nomor Rangka belum terjual")
                    Exit Sub
                ElseIf oChaColl.FakturStatus = 0 Then
                    MessageBox.Show("Nomor Rangka belum terjual")
                    Exit Sub
                End If
                LoadChassisInfo(CType(oChassisFacade.Retrieve(txtNoChasis.Text), ChassisMaster))
                If ddlPqrType.SelectedValue <> "3" Then
                    sessHelper.SetSession("NEW_DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)
                End If
                sessHelper.SetSession("NEW_PARTS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)
                sessHelper.SetSession("NEW_ATTACHMENTTOP" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)
                sessHelper.SetSession("NEW_ATTACHMENTBOTTOM" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)
                sessHelper.SetSession("NEW_QRS" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), New ArrayList)
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

        oPQRHeader = CType(sessHelper.GetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader)
        Select Case CType(oPQRHeader.RowStatus, EnumPQR.PQRStatus)
            Case EnumPQR.PQRStatus.Batal
                act = "Batal"
                MessageBox.Show("Invalid Action")
                Return
            Case EnumPQR.PQRStatus.Baru
                act = "Batal"
                result = oPQRHeaderFacade.UbahStatusPQRDocument(oPQRHeader, EnumPQR.PQRStatus.Batal, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Validasi
                act = "Batal Validasi"
                result = oPQRHeaderFacade.UbahStatusPQRDocument(oPQRHeader, EnumPQR.PQRStatus.Batal, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Proses
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Batal Proses"
                result = oPQRHeaderFacade.UbahStatusPQRDocument(oPQRHeader, EnumPQR.PQRStatus.Batal, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Rilis
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Batal Rilis"
                result = oPQRHeaderFacade.UbahStatusPQRDocument(oPQRHeader, EnumPQR.PQRStatus.Proses, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Selesai
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Batal Selesai"
                result = oPQRHeaderFacade.UbahStatusPQRDocument(oPQRHeader, EnumPQR.PQRStatus.Rilis, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
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

        oPQRHeader = CType(sessHelper.GetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader)
        Select Case CType(oPQRHeader.RowStatus, EnumPQR.PQRStatus)
            Case EnumPQR.PQRStatus.Batal
                act = "Batal"
                result = oPQRHeaderFacade.UbahStatusPQRDocument(oPQRHeader, EnumPQR.PQRStatus.Baru, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Baru
                act = "Validasi"
                result = oPQRHeaderFacade.UbahStatusPQRDocument(oPQRHeader, EnumPQR.PQRStatus.Validasi, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Validasi
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Proses"
                result = oPQRHeaderFacade.UbahStatusPQRDocument(oPQRHeader, EnumPQR.PQRStatus.Proses, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Proses
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Rilis"
                result = oPQRHeaderFacade.UbahStatusPQRDocument(oPQRHeader, EnumPQR.PQRStatus.Rilis, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
            Case EnumPQR.PQRStatus.Rilis
                If IsDealer Then
                    MessageBox.Show("Dealer tidak dapat melakukan proses ini")
                    Return
                End If
                act = "Selesai"
                result = oPQRHeaderFacade.UbahStatusPQRDocument(oPQRHeader, EnumPQR.PQRStatus.Selesai, ErrMessage, KTB.DNet.Lib.WebConfig.GetValue("PQREmailPIC"))
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
        Response.Redirect("../PQR/FrmPQRAdditionalInfo.aspx?Mode=" & sMode & "&PQRID=" & CType(sessHelper.GetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader).ID & "&Src=" & Request.QueryString("Src"))
        ReloadForm(CType(sessHelper.GetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader).ID)
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
        Response.Redirect("../PQR/FrmPQRPrintPreview.aspx?Mode=" & sMode & "&PQRID=" & CType(sessHelper.GetSession("PQR" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), "")), PQRHeader).ID.ToString & "&Src=" & Request.QueryString("Src"))
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
        Dim _crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        _crits.opAnd(New Criteria(GetType(PQRHeader), "ID", MatchType.Exact, Param))
        Dim _arList As ArrayList = New PQRHeaderFacade(User).Retrieve(_crits)
        For Each a As PQRHeader In _arList
            _return = a.PQRNo
            Exit For
        Next
        Return _return
    End Function

    Protected Sub ddlPqrType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPqrType.SelectedIndexChanged
        Dim _arrDamageCode As New ArrayList
        If ddlPqrType.SelectedValue = "3" Then
            'dgKerusakan.Visible = False
            'lblPosKerusakan.Visible = False
            'Div1.Style.Add("display", "none")

            dgKerusakan.Enabled = False
            For Each item As DataGridItem In dgKerusakan.Items
                If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                    Try
                        Dim lblPopUp As Label = CType(item.FindControl("lblSearchDamageFooter"), Label)
                        lblPopUp.Enabled = False
                    Catch ex As Exception
                        Dim debug = ex
                    End Try
                End If
            Next
            Dim objKodePosisi = New DeskripsiPositionCodeFacade(User).Retrieve("420000")
            Dim objPQRDamageCode As PQRDamageCode = New PQRDamageCode

            objPQRDamageCode.DeskripsiKodePosisi = objKodePosisi

            _arrDamageCode.Add(objPQRDamageCode)
            dgKerusakan.Columns(dgKerusakan.Columns.Count - 1).Visible = False
            dgKerusakan.ShowFooter = False
        Else
            'dgKerusakan.Visible = True
            'lblPosKerusakan.Visible = True
            'Div1.Style.Add("display", "inline")
            dgKerusakan.DataSource = Nothing
            dgKerusakan.Columns(dgKerusakan.Columns.Count - 1).Visible = True
            dgKerusakan.ShowFooter = True
        End If

        'If ddlPqrType.SelectedValue = "0" OrElse ddlPqrType.SelectedValue = "1" Then
        If ddlPqrType.SelectedValue = "0" OrElse ddlPqrType.SelectedValue = "1" OrElse ddlPqrType.SelectedValue = "4" Then 'edited 15122020
            icTglPemasangan.Enabled = False
            txtOdoPemasangan.Text = "0"
            txtOdoPemasangan.Enabled = False
            txtDealerInvoice.Enabled = False
        Else
            icTglPemasangan.Enabled = True
            icTglPemasangan.Enabled = True
            txtOdoPemasangan.Text = ""
            txtOdoPemasangan.Enabled = True
            txtDealerInvoice.Enabled = True
        End If

        BindParts()
        If Mode = enumMode.Mode.NewItemMode Then
            sessHelper.SetSession("NEW_DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrDamageCode)
            dgKerusakan.DataSource = sessHelper.GetSession("NEW_DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""))
            dgKerusakan.DataBind()
        Else
            sessHelper.SetSession("DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""), _arrDamageCode)
            dgKerusakan.DataSource = sessHelper.GetSession("DAMAGECODE" + If(Request.QueryString("PQRID") IsNot Nothing, Request.QueryString("PQRID").ToString(), ""))
            dgKerusakan.DataBind()
        End If
    End Sub

    Private Function checkFilesOK(arrPQREvidence As ArrayList) As Boolean
        Dim crit As New CriteriaComposite(New Criteria(GetType(PQRAttachment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(PQRAttachment), "PQRHeader.ID", MatchType.Exact, oPQRHeader.ID))
        Dim arlPQRAtt As ArrayList = New PQRAttachmentFacade(User).Retrieve(crit)
        If arlPQRAtt.Count = arrPQREvidence.Count Then
            Return True
        End If
        Return False
    End Function

End Class
