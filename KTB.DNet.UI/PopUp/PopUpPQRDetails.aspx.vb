Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search


Imports System.Text
Imports System.IO

Public Class PopUpPQRDetails
#Region "Private Variable"

    Inherits System.Web.UI.Page

    Private sessHelper As New SessionHelper
    Private oDealer As New Dealer
    Private oLoginUser As New UserInfo

    Private oPQRHeader As New PQRHeader
    Private oPQRHeaderFacade As New PQRHeaderFacade(User)

    Private oChassis As New ChassisMaster
    Private oChassisFacade As New ChassisMasterFacade(User)
    Protected WithEvents pnlResult As System.Web.UI.WebControls.Panel

    '    Private Mode As enumMode.Mode

    Private AttachmentDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("PQRAttachmentDir")


#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPQRNo As System.Web.UI.WebControls.Label
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
    Protected WithEvents lblSubject As System.Web.UI.WebControls.Label
    Protected WithEvents lblGejala As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerVal As System.Web.UI.WebControls.Label
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents dgParts As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblAppliedByVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglJamVal As System.Web.UI.WebControls.Label
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
    Protected WithEvents lblOdometer As System.Web.UI.WebControls.Label
    Protected WithEvents lblOdometerVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblKecepatanVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubjectVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblPenyebab As System.Web.UI.WebControls.Label
    Protected WithEvents lblHasil As System.Web.UI.WebControls.Label
    Protected WithEvents lblCatatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglKerusakan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglKerusakanVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastPostedInfo As System.Web.UI.WebControls.Label
    Protected WithEvents lnkbtnAdditionalInfoPopUp As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblBobot As System.Web.UI.WebControls.Label
    Protected WithEvents lblAppliedBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglJam As System.Web.UI.WebControls.Label
    Protected WithEvents lblProcessBy As System.Web.UI.WebControls.Label
    Protected WithEvents lblProcessByVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglJamProcess As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglJamProcessVal As System.Web.UI.WebControls.Label
    Protected WithEvents txtGejala As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPenyebab As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHasil As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCatatan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSolution As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblBobotVal As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        oPQRHeader = New PQRHeaderFacade(User).Retrieve(Request.QueryString("PQRNo"))
        RenderProfilePanel(oPQRHeader, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQR, Panel1, True)
    End Sub

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If (oPQRHeader.PQRNo <> String.Empty) Then
            pnlResult.Visible = True
            sessHelper.SetSession("DAMAGECODE", oPQRHeader.PQRDamageCodes)
            sessHelper.SetSession("PARTS", oPQRHeader.PQRPartsCodes)
            sessHelper.SetSession("ATTACHMENTTOP", GetAttachmentList(oPQRHeader.PQRAttachments, EnumPQR.AttachmentLocation.Top))
            sessHelper.SetSession("ATTACHMENTBOTTOM", GetAttachmentList(oPQRHeader.PQRAttachments, EnumPQR.AttachmentLocation.Bottom))
            fillForm()
        Else
            pnlResult.Visible = False
            MessageBox.Show("PQR Header tidak ditemukan")
            Exit Sub
        End If

        If oDealer.Title = 0 Then 'Dealer
            lblBobotVal.Visible = False
        ElseIf oDealer.Title = 1 Then 'KTB
            lblBobotVal.Visible = True
        End If
    End Sub

    Private Sub RenderProfilePanel(ByVal objPQRHeader As PQRHeader, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel, ByVal isReadonly As Boolean)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(isReadonly)

        If Not objPQRHeader Is Nothing Then
            objRenderPanel.GeneratePanel(objPQRHeader.ID, objPanel, objGroup, profileType, User)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User)
        End If
    End Sub
    Private Sub LoadForm()
        lblPQRNoVal.Text = oPQRHeader.PQRNo
        lblRefPQRNoVal.Text = oPQRHeader.RefPQRNo
        lblTglPembuatanVal.Text = oPQRHeader.DocumentDate.ToString("dd/MM/yyyy")

        LoadChassisInfo(CType(oChassisFacade.Retrieve(oPQRHeader.ChassisMaster.ID), ChassisMaster))
        lblTglKerusakanVal.Text = oPQRHeader.PQRDate.ToString("dd/MM/yyyy")
        lblOdometerVal.Text = oPQRHeader.OdoMeter
        lblKecepatanVal.Text = oPQRHeader.Velocity
        lblBobotVal.Text = oPQRHeader.Bobot
        lblSubjectVal.Text = oPQRHeader.Subject
        txtGejala.Text = oPQRHeader.Symptomps
        txtPenyebab.Text = oPQRHeader.Causes
        txtHasil.Text = oPQRHeader.Results
        txtCatatan.Text = oPQRHeader.Notes
        txtSolution.Text = oPQRHeader.Solutions

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

    End Sub
    Private Sub fillForm()
        lblDealerVal.Text = oPQRHeader.Dealer.DealerCode & " - " & oPQRHeader.Dealer.SearchTerm1
        lblAppliedByVal.Text = CommonFunction.FormatSavedUser(oPQRHeader.CreatedBy, User)
        lblTglJamVal.Text = oPQRHeader.CreatedTime.ToString("dd/MM/yyyy HH:mm")

        If oPQRHeader.PQRAdditionalInfos.Count > 0 Then
            ltrStatusAdditionalInfo.Text = "Ada"
            lblLastPostedInfo.Visible = True

            Dim tempArr As ArrayList = oPQRHeader.PQRAdditionalInfos
            tempArr = KTB.DNet.Utility.CommonFunction.SortArraylist(tempArr, GetType(PQRAdditionalInfo), "CreatedTime", Sort.SortDirection.DESC)

            Dim obj As PQRAdditionalInfo = CType(tempArr(0), PQRAdditionalInfo)
            If obj.CreatedBy.Length > 6 Then
                lblLastPostedInfo.ToolTip = CommonFunction.FormatSavedUser(obj.CreatedBy, User) & " [" & obj.CreatedTime.ToString("dd/MM/yyyy HH:mm:ss") & "]"

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
        RefreshGrid()
    End Sub
    Private Sub RefreshGrid()
        BindDamageCode()
        BindParts()
        BindAttachmentTop()
        BindAttachmentBottom()
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

#Region "Datagrid Kerusakan"
    Private Sub dgKerusakan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgKerusakan.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
        End If
    End Sub
    Private Sub BindDamageCode()
        dgKerusakan.DataSource = CType(sessHelper.GetSession("DAMAGECODE"), ArrayList)
        dgKerusakan.DataBind()
    End Sub
#End Region

#Region "Datagrid Parts"
    Private Sub dgParts_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgParts.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
        End If

    End Sub
    Private Sub BindParts()
        dgParts.DataSource = CType(sessHelper.GetSession("PARTS"), ArrayList)
        dgParts.DataBind()
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
    Private Sub BindAttachmentTop()
        dgFileAttachmentTop.DataSource = CType(sessHelper.GetSession("ATTACHMENTTOP"), ArrayList)
        dgFileAttachmentTop.DataBind()
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
    Private Sub BindAttachmentBottom()
        dgFileAttachmentBottom.DataSource = CType(sessHelper.GetSession("ATTACHMENTBOTTOM"), ArrayList)
        dgFileAttachmentBottom.DataBind()
    End Sub
#End Region

    'Private Sub Attachment_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim objSender As LinkButton = CType(sender, LinkButton)
    '    Response.Redirect("../Download.aspx?file=" & objSender.CommandArgument)
    'End Sub

    Private Sub LoadChassisInfo(ByRef obj As ChassisMaster)
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
                lblNamaVal.Text = obj.EndCustomer.Customer.Name1 & " - " & obj.EndCustomer.Customer.Alamat
                lblTglFakturVal.Text = obj.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy")
            End If
        End If
        lblThnProduksiVal.Text = IIf(obj.ProductionYear <= 1900, "", obj.ProductionYear)
        lblTglDeliveryVal.Text = obj.DODateText
    End Sub

    Private Sub lnkbtnAdditionalInfoPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAdditionalInfoPopUp.Click
        Dim sMode As String
        sMode = "View"
        Response.Redirect("../PQR/FrmPQRAdditionalInfo.aspx?Mode=" & sMode & "&PQRID=" & oPQRHeader.ID.ToString())
    End Sub

End Class
