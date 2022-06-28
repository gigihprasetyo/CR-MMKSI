Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Event

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Imports System.IO

Public Class FrmConfirmInfoEvent
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblEventType As System.Web.UI.WebControls.Label
    Protected WithEvents icEventDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEventDateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtEventLocation As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtInvitationQty As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTotalCost As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerComment As System.Web.UI.WebControls.TextBox
    Protected WithEvents fuEstimatedCostFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents lnkbtnPopUp As System.Web.UI.WebControls.LinkButton
    Protected WithEvents txtEventProposeNo As System.Web.UI.WebControls.TextBox

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

    Private sessHelper As New SessionHelper
    Private oDealer As New Dealer
    Private oLoginUser As New UserInfo

    Private UploadDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("EventDir")

    Private oEventInfo As EventInfo
    Private oEventInfoFacade As New EventInfoFacade(User)
#End Region

#Region "PrivateCustomMethods"
    Private Sub fillForm()
        txtEventProposeNo.Text = oEventInfo.EventRequestNo
        If oEventInfo.EventType Is Nothing Then
            lblEventType.Text = ""
        Else
            lblEventType.Text = oEventInfo.EventType.Description
        End If
        If oEventInfo.IsConfirmed = 0 Then
            icEventDateFrom.Value = oEventInfo.DateStart
            icEventDateTo.Value = oEventInfo.DateEnd
            txtEventLocation.Text = oEventInfo.Location
            txtInvitationQty.Text = oEventInfo.NumOfInvitation.ToString("#,##0")
            txtTotalCost.Text = ""
            txtDealerComment.Text = ""
        Else
            icEventDateFrom.Value = oEventInfo.ConfirmedDateStart
            icEventDateTo.Value = oEventInfo.ConfirmedDateEnd
            txtEventLocation.Text = oEventInfo.ConfirmedLocation
            txtInvitationQty.Text = oEventInfo.ConfirmedNumOfInvitation.ToString("#,##0")
            txtTotalCost.Text = oEventInfo.ConfirmedTotalCost.ToString("#,##0")
            txtDealerComment.Text = oEventInfo.ConfirmedComment
        End If
    End Sub
    Private Function ValidateSave() As Boolean
        If txtEventLocation.Text = String.Empty Then
            MessageBox.Show("Silakan isi Lokasi Event.")
            Return False
        End If

        If txtInvitationQty.Text = String.Empty Then
            MessageBox.Show("Silakan isi jml undangan.")
            Return False
        End If

        If Not IsNumeric(txtInvitationQty.Text) Then
            MessageBox.Show("jml Undangan harus numeric.")
            Return False
        Else
            If CInt(txtInvitationQty.Text) < 0 Then
                MessageBox.Show("jml Undangan tidak boleh di bawah 0")
                Return False
            End If
        End If

        If txtTotalCost.Text = String.Empty Then
            MessageBox.Show("Silakan isi Total Biaya.")
            Return False
        End If

        If Not IsNumeric(txtTotalCost.Text) Then
            MessageBox.Show("Total Biaya harus numeric.")
            Return False
        Else
            If CType(txtTotalCost.Text, Decimal) < 0.0 Then
                MessageBox.Show("Total Biaya tidak boleh di bawah 0")
                Return False
            End If
        End If

        If (icEventDateFrom.Value > icEventDateTo.Value) Then
            MessageBox.Show("Tgl Event Dari tidak boleh lebih besar dari Tgl Event Sampai")
            Return False
        End If

        Return True

    End Function



    Private Sub SendEmailToReceiver(ByVal oEventInfo As EventInfo)
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailAdminDNET")
        Dim dealerName As String = CStr(viewstate("DealerName"))

        Dim arlEventEmailReceiver As ArrayList = New EventEmailReceiverFacade(User).RetrieveActiveList()

        For Each item As EventEmailReceiver In arlEventEmailReceiver
            Dim emailTo As String = item.Email
            Dim msgEmailContent As String = "Dealer: " & dealerName & " telah melakukan konfirmasi Event dengan Nomor Pengajuan: " & oEventInfo.EventRequestNo

            SendEmail(emailTo, emailFrom, msgEmailContent)
        Next
    End Sub

    Private Sub SendEmail(ByVal emailTo As String, ByVal emailFrom As String, ByVal msgContent As String)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet] Promotion - Konfirmasi Event DNET", Mail.MailFormat.Html, msgContent)
    End Sub

    Private Function UploadFile(ByVal oEventInfo As EventInfo, ByVal fileUploadValue As String) As Integer
        If fileUploadValue = "" OrElse fileUploadValue = String.Empty Then
            MessageBox.Show("Silahkan masukkan file yang akan disimpan")
            Return -1
        Else
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
            If fuEstimatedCostFile.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Return -1
            Else
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                'oEventInfo = CType(sessHelper.GetSession("EventInfo"), EventInfo)
                Dim DestFile As String = UploadDirectory & "\" & oEventInfo.EventRequestNo.Replace("/", "-") & "\EstimatedCost\" & fuEstimatedCostFile.PostedFile.FileName.Split("\")(fuEstimatedCostFile.PostedFile.FileName.Split("\").Length - 1)
                Dim success As Boolean = False

                Dim finfo As FileInfo

                Try
                    success = imp.Start()
                    If success Then
                        finfo = New FileInfo(DestFile)

                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        fuEstimatedCostFile.PostedFile.SaveAs(DestFile)
                        oEventInfo.ConfirmedEstFileUpload = fuEstimatedCostFile.PostedFile.FileName.Split("\")(fuEstimatedCostFile.PostedFile.FileName.Split("\").Length - 1)
                        sessHelper.SetSession("EventInfo", oEventInfo)
                        MessageBox.Show("Upload Complete")
                        imp.StopImpersonate()
                        imp = Nothing
                        Return 1
                    Else
                        MessageBox.Show("Access Denied")
                        Return -1
                    End If
                Catch ex As Exception
                    Throw ex
                    MessageBox.Show("Upload Fail")
                    Return -1
                End Try
            End If
        End If
    End Function
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        lnkbtnPopUp.Attributes("onClick") = "ShowPPEventInfoSelection()"

        lblDealer.Text = oDealer.DealerCode & "/" & oDealer.DealerName
        viewstate.Add("DealerName", oDealer.DealerName)
        'If Not IsPostBack Then
        '   oEventInfo = oEventInfoFacade.Retrieve(CInt(Request.Params("ID")))
        '   sessHelper.SetSession("EventInfo", oEventInfo)
        '   fillForm()
        'End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Server.Transfer("~/Event/FrmListEventInfo.aspx")
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim result As Integer
        If Not Page.IsValid Then
            Return
        Else
            If ValidateSave() Then
                oEventInfo = CType(sessHelper.GetSession("EventInfo"), EventInfo)

                If UploadFile(oEventInfo, fuEstimatedCostFile.Value) <> -1 Then
                    oEventInfo.IsConfirmed = 1
                    oEventInfo.ConfirmedDateStart = icEventDateFrom.Value
                    oEventInfo.ConfirmedDateEnd = icEventDateTo.Value
                    oEventInfo.ConfirmedLocation = txtEventLocation.Text
                    oEventInfo.ConfirmedNumOfInvitation = CInt(txtInvitationQty.Text)
                    oEventInfo.ConfirmedTotalCost = CType(txtTotalCost.Text, Decimal)
                    oEventInfo.ConfirmedComment = txtDealerComment.Text

                    result = oEventInfoFacade.Update(oEventInfo)

                    If result > 0 Then
                        MessageBox.Show("Data Berhasil Disimpan")

                        'send email
                        SendEmailToReceiver(oEventInfo)
                    Else
                        MessageBox.Show("Save Fail!")
                    End If
                Else
                    MessageBox.Show("Gagal upload file")
                End If
            Else
                MessageBox.Show("Data tidak lengkap. Silakan diperiksa lagi ")
            End If
        End If
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        oEventInfo = CType(sessHelper.GetSession("EventInfo"), EventInfo)
        Dim DestFile As String = UploadDirectory & "\" & oEventInfo.EventRequestNo.Replace("/", "-") & "\EstimatedCost\" & fuEstimatedCostFile.PostedFile.FileName.Split("\")(fuEstimatedCostFile.PostedFile.FileName.Split("\").Length - 1)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(DestFile)

                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                fuEstimatedCostFile.PostedFile.SaveAs(DestFile)
                imp.StopImpersonate()
                imp = Nothing
                MessageBox.Show("Upload Complete")
            Else
                MessageBox.Show("Access Denied")
            End If
        Catch ex As Exception
            Throw ex
            MessageBox.Show("Upload Fail")
        End Try
    End Sub
    Private Sub lnkbtnPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPopUp.Click
        oEventInfo = oEventInfoFacade.Retrieve(txtEventProposeNo.Text)
        If Not oEventInfo Is Nothing And oEventInfo.ID > 0 Then
            sessHelper.SetSession("EventInfo", oEventInfo)
            fillForm()
            If oEventInfo.EventInfoStatus = EnumEventInfo.EventInfoStatus.Disetujui Then
                btnSimpan.Enabled = True
            Else
                MessageBox.Show("Event Info masih belum di setujui atau di tolak.")
                btnSimpan.Enabled = False
            End If
        End If
    End Sub
#End Region

End Class
