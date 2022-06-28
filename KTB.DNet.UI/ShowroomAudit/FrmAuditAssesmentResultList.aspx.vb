#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.ShowroomAudit
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region
Public Class FrmAuditAssesmentResultList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtAuditCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchAudit As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgFiles As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgFotoAwal As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgFotoPerbaikan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents txtUser As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"    
    Dim sHelper As New SessionHelper
#End Region

#Region "CustomMethods"
    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function
    Private Property SesDealer() As Dealer
        Get
            Return CType(sHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sHelper.SetSession("DEALER", Value)
        End Set
    End Property
    Private Sub BindDataToGrid()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strDealerCode As String = txtDealerCode.Text.Trim()
        Dim strAuditCode As String = txtAuditCode.Text.Trim()

        If IsLoginAsDealer() Then
            strDealerCode = SesDealer.DealerCode
        End If
        If strDealerCode.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(AuditScheduleDealer), "Dealer.DealerCode", MatchType.Exact, strDealerCode))
        Else
            MessageBox.Show("Kode Dealer harus diisi.")
            Return
        End If
        If strAuditCode.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(AuditScheduleDealer), "AuditSchedule.AuditParameter.Code", MatchType.Exact, strAuditCode))
        Else
            MessageBox.Show("Kode Audit harus diisi.")
            Return
        End If

        Dim arl As ArrayList = New AuditScheduleDealerFacade(User).Retrieve(criterias)
        Dim strAuditParameterPhotoIDs As String = String.Empty
        For Each scheduleDealer As AuditScheduleDealer In arl
            dtgFotoAwal.DataSource = scheduleDealer.AuditScheduleDealerReports
            Dim sHelper As New SessionHelper
            sHelper.SetSession("SES_objAuditScheduleDealer", scheduleDealer)

            dtgFotoPerbaikan.DataSource = scheduleDealer.AuditScheduleDealerReports
            dtgFotoPerbaikan.DataBind()
        Next
        dtgFotoAwal.DataBind()

        dtgFiles.DataSource = arl
        dtgFiles.DataBind()
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.AuditViewResult_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SHOWROOM AUDIT - Daftar Parameter Audit")
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            lblDealerCode.Visible = IsLoginAsDealer()
            txtDealerCode.Visible = Not IsLoginAsDealer()
            lblSearchDealer.Visible = Not IsLoginAsDealer()
            If IsLoginAsDealer() Then
                lblDealerCode.Text = SesDealer.DealerCode + " - " + SesDealer.DealerName
                txtUser.Value = "Dealer"
            Else
                txtUser.Value = SesDealer.DealerCode
                'txtUser.Value = "KTB"
            End If

            dtgFiles.DataSource = New ArrayList
            dtgFiles.DataBind()

            dtgFotoAwal.DataSource = New ArrayList
            dtgFotoAwal.DataBind()

            dtgFotoPerbaikan.DataSource = New ArrayList
            dtgFotoPerbaikan.DataBind()
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindDataToGrid()
    End Sub

    Private Sub dtgFiles_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFiles.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            'linkbutton
            Dim scheduleDealer As AuditScheduleDealer = CType(e.Item.DataItem, AuditScheduleDealer)

            Dim lbtnJuklakFile As LinkButton = CType(e.Item.FindControl("lbtnJuklakFile"), LinkButton)

            lbtnJuklakFile.Text = System.IO.Path.GetFileName(scheduleDealer.AssessmentFile)
        End If
    End Sub

    Private Sub dtgFiles_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgFiles.ItemCommand
        If e.CommandName = "DownloadJukLak" Then
            Dim strLink As String = e.CommandArgument
            Dim objAuditScheduleDealer As AuditScheduleDealer = sHelper.GetSession("SES_objAuditScheduleDealer")
            Dim link As String = KTB.DNet.Lib.WebConfig.GetValue("AuditAssesmentResult")
            Response.Redirect("../Download.aspx?file=" & link & "\" & objAuditScheduleDealer.ID & "\" & strLink)
        End If
    End Sub

    Private Sub dtgFotoAwal_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFotoAwal.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ctlImg As System.Web.UI.WebControls.Image = e.Item.FindControl("imgItemImage")

            Dim objAuditReport As AuditScheduleDealerReport = CType(e.Item.DataItem, AuditScheduleDealerReport)
            If Not objAuditReport.ItemImage Is Nothing Then
                If (Not ctlImg Is Nothing) Then
                    ctlImg.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & objAuditReport.ID.ToString & "&type=" & "AuditScheduleDealer"
                    ctlImg.Attributes.Add("onclick", "showPopUp('../Popup/PopUpPicture.aspx?ID=" & objAuditReport.ID.ToString & "&Jenis=3', '', 520, 520, KTBNote);return false;")

                End If
            Else
                If (Not ctlImg Is Nothing) Then
                    ctlImg.Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub dtgFotoPerbaikan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFotoPerbaikan.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ctlImg As System.Web.UI.WebControls.Image = e.Item.FindControl("imgItemImage")

            Dim objAuditReport As AuditScheduleDealerReport = CType(e.Item.DataItem, AuditScheduleDealerReport)
            If Not objAuditReport.ItemImageReparation Is Nothing Then
                If (Not ctlImg Is Nothing) Then
                    ctlImg.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & e.Item.ItemIndex.ToString & "&type=" & "FotoPerbaikanAuditScheduleDealer"
                    ctlImg.Attributes.Add("onclick", "showPopUp('../Popup/PopUpPicture.aspx?ID=" & e.Item.ItemIndex.ToString & "&Jenis=4', '', 520, 520, KTBNote);return false;")

                End If
            Else
                If (Not ctlImg Is Nothing) Then
                    ctlImg.Visible = False
                End If
            End If
        End If
    End Sub
End Class
