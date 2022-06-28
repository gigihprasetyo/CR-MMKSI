#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmEventFinishing
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblEventName As System.Web.UI.WebControls.Label
    Protected WithEvents lblEventDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblEventCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblEventPlace As System.Web.UI.WebControls.Label
    Protected WithEvents txtInvitation As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPercentage As System.Web.UI.WebControls.Label
    Protected WithEvents dtgUpload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidate As System.Web.UI.WebControls.Button
    Protected WithEvents pnlDealer As System.Web.UI.WebControls.Panel
    Protected WithEvents btnUploadKTB As System.Web.UI.WebControls.Button
    Protected WithEvents lblPenilaianKTB As System.Web.UI.WebControls.Label
    Protected WithEvents pnlKTB As System.Web.UI.WebControls.Panel
    Protected WithEvents fuKTB As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents hdnSave As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnValidate As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblInviteNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblUndanganKTB As System.Web.UI.WebControls.Label
    Protected WithEvents lblDotKTB As System.Web.UI.WebControls.Label
    Protected WithEvents lblOwnerInfo As System.Web.UI.WebControls.Label
    Protected WithEvents lblDotOwner As System.Web.UI.WebControls.Label
    Protected WithEvents lblDriverInfo As System.Web.UI.WebControls.Label
    Protected WithEvents lblDotDriver As System.Web.UI.WebControls.Label
    Protected WithEvents txtOwner As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDriver As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBack As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private epf As EventProposalFacade
    Private eff As EventProposalFileFacade
    Private objDealer As DEALER
    Private objEp As EventProposal
    Dim _sesshelper As New SessionHelper
    Dim EPS As String = "EventProposalSessionFile"
    Dim DEALER As String = "DEALER"

#Region "function"

    Private Sub proposalEventDataToUI()
        If (IsNothing(Request.QueryString("id"))) Then
            MessageBox.Show("DEV ERR: id query string for EventProposalID is null")
            Return
        End If

        objEp = epf.Retrieve(CInt(Request.QueryString("id")))
        If (IsNothing(objEp)) Then
            MessageBox.Show("DEV ERR: EventProposal is null for ID=" & Request.QueryString("id"))
            Return
        End If

        _sesshelper.SetSession(EPS, objEp)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            lblTitle.Text = String.Format("{0} - Penyelesaian Dealer", objEp.EventParameter.ActivityType.ActivityName)
        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            lblTitle.Text = String.Format("{0} - Penilaian MMKSI", objEp.EventParameter.ActivityType.ActivityName)
        End If

        lblEventName.Text = objEp.EventParameter.EventName
        lblEventDate.Text = objEp.ActivitySchedule.ToString("dd MMM yyyy")
        lblEventCity.Text = objEp.Dealer.City.CityName
        lblEventPlace.Text = objEp.ActivityPlace
        lblInviteNumber.Text = objEp.InvitationNumber
        txtInvitation.Text = objEp.AttendantNumber.ToString()
        lblPercentage.Text = String.Format("Persentase : {0}%", objEp.PercentageAttendent.ToString("#,##0"))

        bindGrid()
    End Sub

    Private Sub bindGrid()
        Dim arl As ArrayList = New ArrayList

        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            If (Not IsNothing(Request.QueryString("view"))) Then
                arl = eff.RetrieveByEventProposal_Dealer(CInt(Request.QueryString("id")), True)
            Else
                arl = eff.RetrieveByEventProposal_Dealer(CInt(Request.QueryString("id")), False)
            End If

            If (IsNothing(arl) Or arl.Count <= 0) Then
                arl = New ArrayList
                For i As Integer = 0 To 12
                    Dim objFile As EventProposalFile = New EventProposalFile
                    objFile.ID = 0
                    objFile.FileName = ""
                    arl.Add(objFile)
                Next
            ElseIf (arl.Count < 12) Then
                For i As Integer = arl.Count To 12
                    Dim objFile As EventProposalFile = New EventProposalFile
                    objFile.ID = 0
                    objFile.FileName = ""
                    arl.Add(objFile)
                Next
            End If

            Dim cntValidate As Integer = 0
            For Each obj As EventProposalFile In arl
                If obj.Status = EventProposalFile.EnumStatus.Validasi_Dealer Then
                    cntValidate += 1
                End If
            Next
            If cntValidate = arl.Count Then
                btnValidate.Enabled = False
                btnSave.Enabled = False
                txtDriver.ReadOnly = True
                txtInvitation.ReadOnly = True
                txtOwner.ReadOnly = True
            End If

        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            If (Not IsNothing(Request.QueryString("view"))) Then
                arl = eff.RetrieveByEventProposal_KTB(CInt(Request.QueryString("id")), False)
            Else
                arl = eff.RetrieveByEventProposal_KTB(CInt(Request.QueryString("id")), True)
            End If

            If (IsNothing(arl) Or arl.Count <= 0) Then
                arl = New ArrayList
                Dim objFile As EventProposalFile = New EventProposalFile
                objFile.ID = 0
                objFile.FileName = ""
                arl.Add(objFile)
            End If
            lblUndanganKTB.Visible = False
            lblDotKTB.Visible = False
            txtInvitation.Visible = False
            lblPercentage.Visible = False

            Dim cntValidate As Integer = 0
            For Each obj As EventProposalFile In arl
                If obj.Status = EventProposalFile.EnumStatus.Validasi_KTB Then
                    cntValidate += 1
                End If
            Next
            If cntValidate = arl.Count Then
                btnValidate.Enabled = False
                btnSave.Enabled = False
                txtDriver.ReadOnly = True
                txtInvitation.ReadOnly = True
                txtOwner.ReadOnly = True
            End If
        End If

        dtgUpload.DataSource = arl
        dtgUpload.DataBind()
    End Sub

#End Region

#Region "event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        epf = New EventProposalFacade(User)
        eff = New EventProposalFileFacade(User)
        objDealer = _sesshelper.GetSession("DEALER")

        objEp = epf.Retrieve(CInt(Request.QueryString("id")))
        If (IsNothing(objEp)) Then
            MessageBox.Show("DEV ERR: EventProposal is null for ID=" & Request.QueryString("id"))
            Return
        End If
        If (objEp.ActivityType.GroupCode = EnumActivityType.ActivityGroupCode.TruckCampaign OrElse objEp.ActivityType.GroupCode = EnumActivityType.ActivityGroupCode.Others) Then
            lblDotDriver.Visible = False
            lblDotOwner.Visible = False
            lblDriverInfo.Visible = False
            lblOwnerInfo.Visible = False
            txtDriver.Visible = False
            txtOwner.Visible = False
        Else
            txtInvitation.ReadOnly = True
        End If

        If (IsPostBack) Then
            If (Request.Form("hdnSave") = "1") Then
                btnSave_Click(Nothing, Nothing)
            End If
            If (Request.Form("hdnValidate") = "1") Then
                btnValidate_Click(Nothing, Nothing)
            End If
            hdnSave.Value = "-1"
            hdnValidate.Value = "-1"
            Return
        End If

        proposalEventDataToUI()

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (hdnSave.Value = "-1") Then
            MessageBox.Confirm("Yakin ingin save ?", "hdnSave")
            Return
        End If

        objEp = CType(_sesshelper.GetSession(EPS), EventProposal)
        objEp.AttendantNumber = CInt(txtInvitation.Text)
        objEp.DriverAttendant = CInt(txtDriver.Text)
        objEp.OwnerAttendant = CInt(txtOwner.Text)
        Dim i As Integer = epf.Update(objEp)
        _sesshelper.SetSession(EPS, objEp)
        lblPercentage.Text = String.Format("Persentase : {0}%", objEp.PercentageAttendent.ToString("#,##0"))
        If (i <> -1) Then
            MessageBox.Show("Penyelesain Proposal Berhasil Disimpan")
        Else
            MessageBox.Show("Penyelesain Proposal Gagal Disimpan")
        End If
    End Sub

    Private Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        If (hdnValidate.Value = "-1") Then
            MessageBox.Confirm("Yakin ingin validasi ?", "hdnValidate")
            Return
        End If

        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            Dim arl As ArrayList = eff.RetrieveByEventProposal_Dealer(CInt(Request.QueryString("id")), False)
            If (arl.Count < 12) Then
                MessageBox.Show("Penyelesaian belum bisa di validasi, karena jumlah file laporan belum cukup 12")
                Return
            End If

            For Each obj As EventProposalFile In arl
                obj.Status = EventProposalFile.EnumStatus.Validasi_Dealer
                eff.Update(obj)
            Next
        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            Dim arl As ArrayList = eff.RetrieveByEventProposal_KTB(CInt(Request.QueryString("id")), False)
            If (arl.Count < 1) Then
                MessageBox.Show("Penilaian belum bisa di validasi, karena jumlah file penilaian belum diupload")
                Return
            End If

            For Each obj As EventProposalFile In arl
                obj.Status = EventProposalFile.EnumStatus.Validasi_KTB
                eff.Update(obj)
            Next
        End If

        objEp = CType(_sesshelper.GetSession(EPS), EventProposal)
        objEp.EventProposalStatus = EnumEventProposalStatus.EventProposalStatus.Validasi
        Dim i As Integer = epf.Update(objEp)
        _sesshelper.SetSession(EPS, objEp)
        lblPercentage.Text = String.Format("Persentase : {0}%", objEp.PercentageAttendent.ToString("#,##0"))
        If (i <> -1) Then
            bindGrid()
            MessageBox.Show("Penyelesain Proposal Berhasil Divalidasi")
        Else
            MessageBox.Show("Penyelesain Proposal Gagal Divalidasi")
        End If
    End Sub

    Private Sub dtgUpload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUpload.ItemDataBound
        If (IsNothing(e.Item.DataItem)) Then Return

        Dim obj As EventProposalFile = CType(e.Item.DataItem, EventProposalFile)
        Dim btnUpload As Button = CType(e.Item.FindControl("btnUpload"), Button)
        Dim fu As HtmlInputFile = CType(e.Item.FindControl("fu"), HtmlInputFile)

        Dim lblInfo As Label = CType(e.Item.FindControl("lblInfo"), Label)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            If (obj.ID <> 0) Then
                If (obj.ContentType.ToLower() = CInt(EventProposalFile.EnumContentType.Laporan_Acara).ToString().ToLower()) Then
                    lblInfo.Text = String.Format("<b>Upload</b> Laporan Acara {0}", obj.EventProposal.EventParameter.EventName)
                ElseIf (obj.ContentType.ToLower() = CInt(EventProposalFile.EnumContentType.Laporan_Penjualan).ToString().ToLower()) Then
                    lblInfo.Text = String.Format("<b>Upload</b> Laporan Penjualan {0}", obj.EventProposal.EventParameter.EventName)
                ElseIf (obj.ContentType.ToLower() = CInt(EventProposalFile.EnumContentType.Penilaian_KTB).ToString().ToLower()) Then
                    lblInfo.Text = "<b>Upload</b> File Penilaian (jpg, bmp, gif)"
                End If
            Else
                lblInfo.Text = "<b>Upload</b> File Penilaian (jpg, bmp, gif)"
            End If
            If (obj.Status = EventProposalFile.EnumStatus.Validasi_KTB) Then
                btnUpload.Enabled = False
                fu.Visible = False
            End If
        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            If (obj.ID <> 0) Then
                If (obj.ContentType.ToLower() = CInt(EventProposalFile.EnumContentType.Laporan_Acara).ToString().ToLower()) Then
                    lblInfo.Text = String.Format("<b>Upload</b> Laporan Acara {0} (doc, pdf)", objEp.EventParameter.EventName)
                ElseIf (obj.ContentType.ToLower() = CInt(EventProposalFile.EnumContentType.Laporan_Penjualan).ToString().ToLower()) Then
                    lblInfo.Text = String.Format("<b>Upload</b> Laporan Penjualan {0} (xls)", objEp.EventParameter.EventName)
                    e.Item.Visible = False
                ElseIf (obj.ContentType.ToLower() = CInt(EventProposalFile.EnumContentType.Penilaian_KTB).ToString().ToLower()) Then
                    lblInfo.Text = "<b>Upload</b> File Penilaian (jpg, bmp, gif)"
                End If
            Else
                If (e.Item.ItemIndex = 0) Then
                    lblInfo.Text = String.Format("<b>Upload</b> Laporan Acara {0} (doc, pdf)", objEp.EventParameter.EventName)
                ElseIf (e.Item.ItemIndex = 1) Then
                    lblInfo.Text = String.Format("<b>Upload</b> Laporan Penjualan {0} (xls)", objEp.EventParameter.EventName)
                    e.Item.Visible = False
                End If
            End If
            If (obj.Status = EventProposalFile.EnumStatus.Validasi_Dealer) Then
                btnUpload.Enabled = False
                fu.Visible = False
            End If
        End If
    End Sub

    Private Sub dtgUpload_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgUpload.ItemCommand
        If (e.CommandName = "upload") Then
            Dim fu As HtmlInputFile = CType(e.Item.FindControl("fu"), HtmlInputFile)
            If (fu.PostedFile.FileName = "") Then
                MessageBox.Show("File belum diisi")
                Return
            End If

            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
            If (fu.PostedFile.ContentLength > maxFileSize) Then
                MessageBox.Show("Maksimal File " & maxFileSize & "kb")
                Return
            End If

            Dim szFileFormatName As String = "{0}_{1}_{2}"
            objEp = _sesshelper.GetSession(EPS)
            Dim obj As EventProposalFile = New EventProposalFile

            If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                szFileFormatName = "Penilaian_{0}_{1}_{2}"
                obj.ContentType = CInt(EventProposalFile.EnumContentType.Penilaian_KTB).ToString()
                obj.Status = EventProposalFile.EnumStatus.Baru_KTB
            ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                obj.Status = EventProposalFile.EnumStatus.Baru_Dealer
                If (e.Item.ItemIndex = 0) Then
                    If (Not fu.PostedFile.FileName.ToLower().EndsWith(".pdf") And Not fu.PostedFile.FileName.ToLower().EndsWith(".doc")) Then
                        MessageBox.Show("File Laporan Acara Truck Campaign Harus Ektensi .pdf atau .doc")
                        Return
                    End If
                    obj.ContentType = CInt(EventProposalFile.EnumContentType.Laporan_Acara).ToString()
                    szFileFormatName = "LaporanAcara_{0}_{1}_{2}"
                ElseIf (e.Item.ItemIndex = 1) Then
                    If (Not fu.PostedFile.FileName.ToLower().EndsWith(".xls")) Then
                        MessageBox.Show("File Laporan Penjualan Truck Campaign Harus Ektensi .xls")
                        Return
                    End If
                    obj.ContentType = CInt(EventProposalFile.EnumContentType.Laporan_Penjualan).ToString()
                    szFileFormatName = "LaporanPenjualan_{0}_{1}_{2}"
                Else
                    If (Not fu.PostedFile.FileName.ToLower().EndsWith(".jpg") And _
                        Not fu.PostedFile.FileName.ToLower().EndsWith(".jpeg") And _
                        Not fu.PostedFile.FileName.ToLower().EndsWith(".bmp") And _
                        Not fu.PostedFile.FileName.ToLower().EndsWith(".gif") And _
                        Not fu.PostedFile.FileName.ToLower().EndsWith(".png")) Then

                        MessageBox.Show("File Foto Truck Campaign Harus Berekstensi .jpg, .jpeg, .bmp, .png, .gif")
                        Return
                    End If
                    obj.ContentType = CInt(EventProposalFile.EnumContentType.Foto).ToString()
                    szFileFormatName = "Foto_{0}_{1}_{2}"
                End If
            End If

            Dim iresult As Integer = -1
            Dim oldFile As String = ""
            If (e.CommandArgument = "0") Then
                obj.EventProposal = CType(_sesshelper.GetSession(EPS), EventProposal)
                Dim id As Integer = eff.Insert(obj)
                If (id <> -1) Then
                    obj = eff.Retrieve(id)
                    obj.FileName = String.Format(szFileFormatName, objEp.EventParameter.ActivityType.ActivityName, id, Path.GetFileName(fu.PostedFile.FileName)).Replace(" ", "")
                    iresult = eff.Update(obj)
                End If
            Else
                obj = eff.Retrieve(CInt(e.CommandArgument))
                obj.EventProposal = CType(_sesshelper.GetSession(EPS), EventProposal)
                oldFile = obj.FileName
                obj.FileName = String.Format(szFileFormatName, objEp.EventParameter.ActivityType.ActivityName, obj.ID, Path.GetFileName(fu.PostedFile.FileName)).Replace(" ", "")
                iresult = eff.Update(obj)
            End If

            lblPercentage.Text = String.Format("Persentase : {0}%", obj.EventProposal.PercentageAttendent.ToString("#,##0"))

            If (iresult <> -1) Then
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory") & "\" & obj.FileName
                Dim oldDestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory") & "\" & oldFile
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim finfo As New FileInfo(DestFile)
                If (imp.Start()) Then
                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    If (oldFile <> "") Then
                        Dim finfoold As New FileInfo(oldDestFile)
                        Try
                            If (oldFile.ToLower() <> obj.FileName.ToLower()) Then
                                If (finfoold.Exists()) Then
                                    finfoold.Delete()
                                End If
                            End If
                        Catch ex As Exception
                        End Try
                    End If
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(fu.PostedFile.InputStream, DestFile)
                    Dim lblFileName As Label = CType(e.Item.FindControl("lblFileName"), Label)
                    lblFileName.Text = obj.FileName
                    imp.StopImpersonate()
                    imp = Nothing
                    MessageBox.Show("File berhasil di upload")
                End If
            Else
                MessageBox.Show("File gagal di upload")
            End If
        End If

    End Sub

#End Region


End Class
