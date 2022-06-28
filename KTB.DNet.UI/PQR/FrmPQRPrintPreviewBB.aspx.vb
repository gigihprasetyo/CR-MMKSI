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

Public Class FrmPQRPrintPreviewBB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblBobot As System.Web.UI.WebControls.Label
    Protected WithEvents ddlBobot As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgFileAttachmentBottom As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents lblTglRilis As System.Web.UI.WebControls.Label
    Protected WithEvents lblUserRilis As System.Web.UI.WebControls.Label
    Protected WithEvents lblSolution As System.Web.UI.WebControls.Label
    Protected WithEvents lblLampiranBawah As System.Web.UI.WebControls.Label
    Protected WithEvents lblLampiranBawahVal As System.Web.UI.WebControls.Label
    Protected WithEvents divKomunikasi As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lbModel As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPembuatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPembuatanVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglKerusakan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglKerusakanVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblOdometer As System.Web.UI.WebControls.Label
    Protected WithEvents lblOdometerVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglDelivery As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglDeliveryVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblPQRNoVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglFakturVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefPQRNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefPQRNoVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblThnProduksi As System.Web.UI.WebControls.Label
    Protected WithEvents lblThnProduksiVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoChasis As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoChasisVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTypeColor As System.Web.UI.WebControls.Label
    Protected WithEvents lblTypeColorVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoMesin As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoMesinVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaVal As System.Web.UI.WebControls.Label
    Protected WithEvents pnlProfile1 As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlProfile2 As System.Web.UI.WebControls.Panel
    Protected WithEvents lblKodePosisi As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodePosisiVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblPart As System.Web.UI.WebControls.Label
    Protected WithEvents lblPartVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblKecepatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblKecepatanVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubject As System.Web.UI.WebControls.Label
    Protected WithEvents lblSubjectVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblGejala As System.Web.UI.WebControls.Label
    Protected WithEvents lblGejalaVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblPenyebab As System.Web.UI.WebControls.Label
    Protected WithEvents lblPenyebabVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblHasil As System.Web.UI.WebControls.Label
    Protected WithEvents lblHasilVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblCatatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblCatatanVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblLampiranAtas As System.Web.UI.WebControls.Label
    Protected WithEvents lblLampiranAtasVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblCodeA As System.Web.UI.WebControls.Label
    Protected WithEvents lblCodeB As System.Web.UI.WebControls.Label
    Protected WithEvents lblCodeC As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        oPQRHeaderBB = oPQRHeaderBBFacade.Retrieve(CInt(Request.QueryString("PQRID")))
        sessHelper.SetSession("PrintPQR", oPQRHeaderBB)
        'oPQRHeaderBB = CType(sessHelper.GetSession("PQR"), PQRHeaderBB)

        RenderProfilePanel(oPQRHeaderBB, New ProfileGroupFacade(User).Retrieve("pqr_prf"), EnumProfileType.ProfileType.PQRBB, pnlProfile1, True)
        RenderProfilePanel(oPQRHeaderBB, New ProfileGroupFacade(User).Retrieve("pqr_prf_2"), EnumProfileType.ProfileType.PQRBB, pnlProfile2, True)
    End Sub

#End Region

#Region "Private Variable"

    Private sessHelper As New SessionHelper
    Private oDealer As New Dealer
    Private oLoginUser As New UserInfo

    Private oPQRHeaderBB As PQRHeaderBB
    Private oPQRHeaderBBFacade As New PQRHeaderBBFacade(User)

#End Region

    Private Sub RenderProfilePanel(ByVal objPQRHeaderBB As PQRHeaderBB, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel, ByVal isReadonly As Boolean)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(isReadonly)

        If Not objPQRHeaderBB Is Nothing Then
            objRenderPanel.GeneratePanel(objPQRHeaderBB.ID, objPanel, objGroup, profileType, User)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User)
        End If
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

    Private Sub LoadForm()
        oPQRHeaderBB = CType(sessHelper.GetSession("PrintPQR"), PQRHeaderBB)

        lbModel.Text = oPQRHeaderBB.ChassisMasterBB.Category.Description

        If oPQRHeaderBB.DocumentDate < New Date(1900, 1, 1) Then
            lblTglPembuatanVal.Text = ""
        Else
            lblTglPembuatanVal.Text = oPQRHeaderBB.DocumentDate.ToString("dd/MM/yyyy")
        End If

        If oPQRHeaderBB.ChassisMasterBB.DODate < New Date(1900, 1, 1) Then
            lblTglDeliveryVal.Text = ""
        Else
            lblTglDeliveryVal.Text = oPQRHeaderBB.ChassisMasterBB.DODate.ToString("dd/MM/yyyy")
        End If

        If oPQRHeaderBB.PQRDate < New Date(1900, 1, 1) Then
            lblTglKerusakanVal.Text = ""
        Else
            lblTglKerusakanVal.Text = oPQRHeaderBB.PQRDate.ToString("dd/MM/yyyy")
        End If

        If oPQRHeaderBB.ChassisMasterBB.EndCustomer Is Nothing OrElse oPQRHeaderBB.ChassisMasterBB.EndCustomer.OpenFakturDate < New Date(1900, 1, 1) Then
            lblTglFakturVal.Text = ""
        Else
            lblTglFakturVal.Text = oPQRHeaderBB.ChassisMasterBB.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy")
        End If

        If oPQRHeaderBB.ChassisMasterBB.ProductionYear <= 0 Then
            lblThnProduksiVal.Text = ""
        Else
            lblThnProduksiVal.Text = oPQRHeaderBB.ChassisMasterBB.ProductionYear.ToString()
        End If


        lblDealerVal.Text = oPQRHeaderBB.Dealer.DealerCode & " - " & oPQRHeaderBB.Dealer.SearchTerm1
        lblOdometerVal.Text = oPQRHeaderBB.OdoMeter.ToString("#,##0")
        lblPQRNoVal.Text = oPQRHeaderBB.PQRNo
        lblRefPQRNoVal.Text = oPQRHeaderBB.RefPQRNo

        lblNoChasisVal.Text = oPQRHeaderBB.ChassisMasterBB.ChassisNumber
        lblNoMesinVal.Text = oPQRHeaderBB.ChassisMasterBB.EngineNumber
        lblTypeColorVal.Text = oPQRHeaderBB.ChassisMasterBB.VechileColor.MaterialNumber & " - " & oPQRHeaderBB.ChassisMasterBB.VechileColor.MaterialDescription
        If oPQRHeaderBB.ChassisMasterBB.EndCustomer Is Nothing Then
            lblNamaVal.Text = ""
        Else
            If oPQRHeaderBB.ChassisMasterBB.EndCustomer.Customer Is Nothing Then
                lblNamaVal.Text = ""
            Else
                lblNamaVal.Text = oPQRHeaderBB.ChassisMasterBB.EndCustomer.Customer.Name1 & " - " & oPQRHeaderBB.ChassisMasterBB.EndCustomer.Customer.Alamat
            End If
        End If

        ' Load Profile Group 1
        ' Load Profile Group 2

        If oPQRHeaderBB.PQRDamageCodeBBs.Count > 0 Then
            Dim sKodePosisi As String = String.Empty
            For Each item As PQRDamageCodeBB In oPQRHeaderBB.PQRDamageCodeBBs
                sKodePosisi += item.DeskripsiKodePosisi.KodePosition & " - " & item.DeskripsiKodePosisi.Description & "<br>"
            Next
            lblKodePosisiVal.Text = sKodePosisi
        End If

        If oPQRHeaderBB.PQRPartsCodeBBs.Count > 0 Then
            Dim sPart As String = String.Empty
            For Each item As PQRPartsCodeBB In oPQRHeaderBB.PQRPartsCodeBBs
                sPart += item.SparePartMaster.PartNumber & " - " & item.SparePartMaster.PartName & "<br>"
            Next
            lblPartVal.Text = sPart
        End If

        lblKecepatanVal.Text = oPQRHeaderBB.Velocity.ToString()
        lblSubjectVal.Text = oPQRHeaderBB.Subject
        lblGejalaVal.Text = oPQRHeaderBB.Symptomps.Replace(Chr(13) & Chr(10), "<br>")
        lblPenyebabVal.Text = oPQRHeaderBB.Causes.Replace(Chr(13) & Chr(10), "<br>")
        lblHasilVal.Text = oPQRHeaderBB.Results.Replace(Chr(13) & Chr(10), "<br>")
        lblCatatanVal.Text = oPQRHeaderBB.Notes.Replace(Chr(13) & Chr(10), "<br>")

        If GetAttachmentList(oPQRHeaderBB.PQRAttachmentBBs, EnumPQR.AttachmentLocation.Top).Count > 0 Then
            lblLampiranAtasVal.Text = "Ada"
        Else
            lblLampiranAtasVal.Text = "Tidak Ada"
        End If

        If oPQRHeaderBB.ReleaseBy.Trim = String.Empty Then
            lblUserRilis.Text = ""
        Else
            lblUserRilis.Text = CommonFunction.FormatSavedUser(oPQRHeaderBB.ReleaseBy, User)
        End If

        If oPQRHeaderBB.RealeseTime < New DateTime(1900, 1, 1) Then
            lblTglRilis.Text = ""
        Else
            lblTglRilis.Text = oPQRHeaderBB.RealeseTime.ToString("ddMMyyyy")
        End If

        lblSolution.Text = oPQRHeaderBB.Solutions.Replace(Chr(13) & Chr(10), "<br>")

        If GetAttachmentList(oPQRHeaderBB.PQRAttachmentBBs, EnumPQR.AttachmentLocation.Bottom).Count > 0 Then
            lblLampiranBawahVal.Text = "Ada"
        Else
            lblLampiranBawahVal.Text = "Tidak Ada"
        End If
        lblCodeA.Text = oPQRHeaderBB.CodeA
        lblCodeB.Text = oPQRHeaderBB.CodeB
        lblCodeC.Text = oPQRHeaderBB.CodeC
        If oPQRHeaderBB.PQRAdditionalInfoBBs.Count > 0 Then ' generate additional info
            divKomunikasi.Controls.Clear()
            With divKomunikasi.Controls
                .Add(New LiteralControl("<table width=""100%"" border=""1"" cellspacing=""0"" bordercolor=""#cccccc"" cellpadding=""2""><tr bgcolor=""#cdcdcd"" align=""center""><td class=""titleField"" align=""center"" width=""10%"">Tanggal</td><td class=""titleField"" align=""center"" width=""20%"">PIC</td><td class=""titleField"" align=""center"" width=""60%"">Isi</td><td class=""titleField"" align=""center"" width=""10%"">Lampiran</td></tr>"))
                Dim x As Integer = 1
                For Each item As PQRAdditionalInfoBB In oPQRHeaderBB.PQRAdditionalInfoBBs
                    Dim sCode As New System.Text.StringBuilder
                    'style="BORDER-RIGHT: black thin solid; BORDER-TOP: black thin solid; BORDER-LEFT: black thin solid; BORDER-BOTTOM: black thin solid"
                    sCode.Append("<tr>")
                    sCode.Append("<td>")
                    sCode.Append("<span style=""WIDTH: 100%; TEXT-ALIGN: center"">" & item.CreatedTime.ToString("ddMMyyyy") & "</span>")
                    sCode.Append("</td>")
                    sCode.Append("<td >")
                    sCode.Append("<span style=""WIDTH: 100%; TEXT-ALIGN: ""center"">" & CommonFunction.FormatSavedUser(item.CreatedBy, User) & "</span>")
                    sCode.Append("</td>")
                    sCode.Append("<td >")
                    sCode.Append("<span style=""WIDTH: 100%; TEXT-ALIGN: ""left"">" & item.Sender.Replace(Chr(13) & Chr(10), "<br>") & "</span>")
                    sCode.Append("</td>")

                    sCode.Append("<td >")
                    If item.Attachment.Trim = String.Empty Then
                        sCode.Append("<span style=""WIDTH: 100%; TEXT-ALIGN: center"">Tidak Ada</span>")
                    Else
                        sCode.Append("<span style=""WIDTH: 100%; TEXT-ALIGN: center"">Ada</span>")
                    End If
                    sCode.Append("</td>")
                    sCode.Append("</tr>")
                    .Add(New LiteralControl(sCode.ToString))
                Next
                .Add(New LiteralControl("</table>"))
            End With
        End If

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            oPQRHeaderBB = CType(sessHelper.GetSession("PrintPQR"), PQRHeaderBB)
            LoadForm()
        End If
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        'If Request.QueryString("Src") = "WSCDetail" Then
        '    Server.Transfer("~/Service/FrmWSCDetail.aspx")
        'ElseIf Request.QueryString("Src") = "PQRListKondisi" Then
        '    Server.Transfer("~/PQR/FrmPQRListKondisi.aspx")
        'Else
        '    Server.Transfer("~/PQR/FrmPQRList.aspx")
        'End If

        'If Request.QueryString("Src") = "ListPQR" Then
        '    Server.Transfer("~/PQR/FrmPQRList.aspx")
        'Else
        '    Server.Transfer("~/PQR/FrmPQRHeaderBB.aspx?Mode=" & Request.QueryString("Mode") & "&Src=" & Request.QueryString("Src"))
        'End If

        Server.Transfer("~/PQR/FrmPQRHeaderBB.aspx?Mode=" & Request.QueryString("Mode") & "&Src=" & Request.QueryString("Src"))

    End Sub

End Class
