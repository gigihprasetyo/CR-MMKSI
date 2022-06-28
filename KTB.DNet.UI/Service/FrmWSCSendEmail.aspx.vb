#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
Imports System.Drawing
Imports System.Drawing.Imaging


#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.Text
Imports System.IO
#End Region

Public Class FrmWSCSendEmail
    Inherits System.Web.UI.Page
    Private Const MAX_WIDTH As Integer = 400
    Private Const MAX_HEIGHT As Integer = 200

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblClaimNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents btnSend As System.Web.UI.WebControls.Button
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTerm As System.Web.UI.WebControls.Label
    Protected WithEvents lblClaimNo As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents dtgSendEmail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents OpClient1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents OpClient2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lblDi As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKepadaYthValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerNameValue As System.Web.UI.WebControls.Label
    Protected WithEvents opClient3 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lblEmailTo As System.Web.UI.WebControls.Label
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Declaration"
    Dim WSCHead As WSCHeader  '-- WSC header and its details
    Dim arlSendEmail As ArrayList
    Private sessionHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub RetrieveMaster()
        If Not IsNothing(Session("WSCHead")) Then
            WSCHead = CType(Session("WSCHead"), WSCHeader)
            lblKodeDealer.Text = WSCHead.Dealer.DealerCode
            lblSearchTerm.Text = WSCHead.Dealer.SearchTerm1
            lblClaimNo.Text = WSCHead.ClaimNumber
            lblDealerNameValue.Text = WSCHead.Dealer.DealerName
            viewstate("ClaimNo") = WSCHead.ClaimNumber
            viewstate("ClaimNo1") = Left(WSCHead.ClaimNumber, 4)
            viewstate("ClaimNo2") = Right(WSCHead.ClaimNumber, 2)
            ViewState("Date") = Format(DateTime.Now.Date, "dd MMMMMMMMMMMM yyyy")
            '--Get DealerCode From Session Dealer
            Dim _id As Integer
            Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
            If Not objDealer Is Nothing Then
                _id = objDealer.ID
                viewstate("ID") = _id
            End If
            '--Get EmailSendTo From User Info
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "Dealer.ID", MatchType.Exact, WSCHead.Dealer.ID))
            If WSCHead.CreatedBy.Length > 6 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "UserName", MatchType.Exact, WSCHead.CreatedBy.Substring(6)))
            Else
                setButton()
            End If
            Dim arlUserTo As ArrayList = New UserInfoFacade(User).Retrieve(criterias)
            If arlUserTo.Count <> 0 Then
                ViewState("to") = CType(arlUserTo(0), UserInfo).Email
                If Not ViewState("to") Is Nothing Then
                    lblEmailTo.Text = ViewState("to")
                    lblKepadaYthValue.Text = CType(arlUserTo(0), UserInfo).FirstName & " " & CType(arlUserTo(0), UserInfo).LastName
                    ViewState("Create") = lblKepadaYthValue.Text
                End If
            End If
            '--Get EmailFrom From UserInfo
            Dim criterias1 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "Dealer.ID", MatchType.Exact, _id))
            If User.Identity.Name <> String.Empty Then
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "UserName", MatchType.Exact, User.Identity.Name.Substring(6)))
            Else
                setButton()
            End If
            Dim arlUserCC1 As ArrayList = New UserInfoFacade(User).Retrieve(criterias1)
            If arlUserCC1.Count <> 0 Then
                viewstate("cc1") = CType(arlUserCC1(0), UserInfo).Email
            End If
            '--Get EmailCC From BusinessArea Table
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.BusinessArea), "Dealer.ID", MatchType.Exact, WSCHead.Dealer.ID))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.BusinessArea), "Kind", MatchType.Exact, 2)) '--Kind as Service Unit 
            Dim arlUserCC2 As ArrayList = New BusinessAreaFacade(User).Retrieve(criterias2)
            If arlUserCC2.Count <> 0 Then
                viewstate("cc2") = CType(arlUserCC2(0), BusinessArea).Email
            End If
            '--Check EmailSendTo and EmailFrom
            If lblEmailTo.Text = String.Empty Or viewstate("cc1") = "" Then
                setButton()
            End If
        End If
    End Sub
    Private Sub setButton()
        lblError.Visible = True
        btnSend.Enabled = False
        txtDescription.ReadOnly = True
    End Sub
    Private Sub SaveToDataBase()
        Dim objWSC As WSCDamageRequestPart = New WSCDamageRequestPart
        Dim objWSCMaster As WSCHeader = Session("WSCHead")
        Dim objWSCFac As WSCDamageRequestPartFacade = New WSCDamageRequestPartFacade(User)
        Dim nResult As Integer = -1
        Dim _to As String = ViewState("to")
        Dim _cc1 As String = ViewState("cc1")
        Dim _cc2 As String = ViewState("cc2")
        Dim cc As String = _cc1 & ";" & _cc2
        objWSC.WSCHeader = objWSCMaster
        objWSC.Sender = _cc1
        objWSC.CCSendTo = cc
        objWSC.SendTo = _to
        objWSC.Subject = "MMKSI - Bukti Request WSC" & lblClaimNo.Text
        objWSC.Description = txtDescription.Text.Replace("<BR>", (Chr(13) & Chr(10)).ToString)
        nResult = New WSCDamageRequestPartFacade(User).Insert(objWSC)
        If nResult = -1 Then
            MessageBox.Show("Kirim E-mail Gagal")
        Else
            MessageBox.Show("Kirim E-mail Sukses")
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("Count") = 0
            RetrieveMaster()
            Dim type As String = Request.QueryString("type")
            If type = "ReadOnly" Then
                BindToDataGrid()
                OpClient2.Visible = False
                txtDescription.ReadOnly = True
                lblError.Visible = False
            Else
                BindToDataGrid()
                OpClient1.Visible = False
                dtgSendEmail.Visible = False
                opClient3.Visible = False
            End If
        End If
    End Sub
    Private Sub BindToDataGrid()
        WSCHead = CType(Session("WSCHead"), WSCHeader)
        dtgSendEmail.DataSource = WSCHead.WSCDamageRequestParts
        sessionHelper.SetSession("WSCDamageRequestPart", WSCHead.WSCDamageRequestParts)
        dtgSendEmail.DataBind()
        If Not WSCHead Is Nothing Then
            Dim arlsendEmail As ArrayList = WSCHead.WSCDamageRequestParts
            If arlsendEmail.Count > 0 Then
                Dim x As Int16 = arlsendEmail.Count - 1
                txtDescription.Text = arlsendEmail(x).Description
            End If
        End If
    End Sub
    Private Sub dtgSendEmail_EditCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSendEmail.ItemCommand
        If e.CommandName = "No" Then
            arlSendEmail = sessionHelper.GetSession("WSCDamageRequestPart")
            If Not arlSendEmail Is Nothing Then
                txtDescription.Text = e.Item.Cells(3).Text
            End If
        End If
    End Sub
    Private Function GetBarcode(ByVal dealerCode As String, ByVal wscNumber As String) As String
        'Dim bc As BarcodeNETWorkShop.BarcodeNETImage = New BarcodeNETWorkShop.BarcodeNETImage
        'bc.BarcodeText = dealerCode & wscNumber
        'bc.BarcodeType = BarcodeNETWorkShop.Core.BARCODE_TYPE.CODE39
        'Dim path As String = Server.MapPath("..") & "\" & KTB.DNet.Lib.WebConfig.GetValue("BARCODE")
        'Dim Filelocation As String = path & bc.BarcodeText & ".bmp"
        'Dim dir As DirectoryInfo = New DirectoryInfo(path)
        'If Not dir.Exists Then
        '    dir.Create()
        'End If
        'bc.ShowBorder = True
        'bc.ShowBarcodeText = True
        'bc.SaveToFile(Filelocation, BarcodeNETWorkShop.Core.FILE_FORMAT.BMP)
        Return String.Empty 'Filelocation
    End Function
    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim WSCTeam As String = KTB.DNet.Lib.WebConfig.GetValue("WSCTeamEmail")
        Dim subject As String = "[MMKSI-DNet] Service - Bukti Request WSC " & lblClaimNo.Text
        Dim _to As String = ViewState("to")
        Dim _cc1 As String = ViewState("cc1")
        Dim _cc2 As String = ViewState("cc2")
        Dim _Create As String = ViewState("Create")
        Dim _date As String = viewState("Date")
        Dim _ClaimNo As String = viewstate("ClaimNo")
        Dim _ClaimNo1 As String = viewstate("ClaimNo1")
        Dim _ClaimNo2 As String = viewstate("ClaimNo2")

        Dim email As DNetMail = New DNetMail(smtp)
        Dim sb As StringBuilder = New StringBuilder
        sb.Append("<html><body>")
        sb.Append("<Table width=600px>")
        sb.Append("<h1><td colspan = 4 align=left><b>")
        sb.Append("Kepada Yth :")
        sb.Append("</b></td></h1><tr><td colspan = 4 align=left><b>")
        sb.Append(_Create)
        sb.Append("</td></tr>")
        sb.Append("<tr><td width=10 align=left ><b>")
        sb.Append("di")
        sb.Append("</b></td> <td colspan = 3 align=left ><b>")
        sb.Append(lblKodeDealer.Text)
        sb.Append(" / ")
        sb.Append(lblSearchTerm.Text)
        sb.Append("</b></td> </tr> <tr><td width=10></td> <td colspan= 3><b>")
        sb.Append(lblDealerNameValue.Text)
        sb.Append("</b></td></tr><tr><td width=10></td> <td colspan = 3><b><br>")
        sb.Append("Hal : Permintaan Kirim Bukti WSC")
        sb.Append("</b></td></tr><tr><td width=10></td> <td colspan = 3> ")
        sb.Append("Sehubungan dengan claim Anda no :")
        sb.Append(" <b>")
        sb.Append(lblKodeDealer.Text)
        sb.Append("  ")
        sb.Append(_ClaimNo1)
        sb.Append("  ")
        sb.Append(_ClaimNo2)
        sb.Append("<b>, </td></tr><tr><td width=10></td> <td colspan = 3>")
        sb.Append("Agar dikirimkan bukti terkait dengan claim tersebut")
        sb.Append("</td></tr><tr><td width=10></td> <td colspan = 3>")
        sb.Append("Catatan : ")
        sb.Append("</td></tr><tr><td width=10></td> <td colspan=4 width 500><br>")
        sb.Append(txtDescription.Text.Replace((Chr(13) & Chr(10)).ToString, "<BR>"))
        sb.Append("</td></tr><tr><td width=10></td> <td colspan = 3><br>")
        sb.Append("Agar bukti diatas dapat dikirim Selambat-lambatnya 3 hari setelah email ini dikirim")
        sb.Append("</td></tr><tr><td width=10></td> <td colspan = 3>")
        sb.Append("Atas perhatian dan kerjasamanya, kami ucapkan Terima Kasih")
        sb.Append("<br><br><br> </td></tr><tr><td width=10></td> <td colspan=2 width=400></td><td> <Table width=375 align = right border=0><tr><td align=center colspan=4 >")
        sb.Append("</td></tr><tr><td align=center colspan=4 >")
        sb.Append("Jakarta,")
        sb.Append(_date)
        sb.Append("</td></tr><tr><td align=center colspan=4 >")
        sb.Append("Hormat Kami,")
        sb.Append("</td></tr><tr><td align=center colspan=4 >")
        sb.Append("Service Department")
        sb.Append("<br><br><br><br></td></tr><tr><td align=center colspan=4 >")
        sb.Append("( Service Manager )")
        sb.Append("</td></tr><tr><td width=50px></td><td align=left colspan=3 ><b>")
        sb.Append("PT Mitsubishi Motors Krama Yudha Sales Indonesia")
        sb.Append("<b></td></tr><tr><td width=50px></td><td align=left width=1px></td><td align=left >")
        sb.Append("Diminta oleh :")
        sb.Append("</td><td align=left width=223px>")
        Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
        If User.Identity.Name <> String.Empty Then
            Dim NamaPengirim As String = ""

            Dim objUser As UserInfo = New UserInfoFacade(User).Retrieve(User.Identity.Name.Substring(6), objDealer.DealerCode)
            If (Not objUser Is Nothing) And (Not objDealer Is Nothing) Then
                sb.Append(objUser.FirstName & " " & objUser.LastName)
            Else
                sb.Append(" ")
            End If
        End If

        sb.Append("</td></tr></Table></td></tr></body></table></html>")
        Dim strBody As String = sb.ToString
        Try
            ' Dim attach As String = DrawImage(lblKodeDealer.Text, _ClaimNo)
            Dim list As ArrayList = New ArrayList
            ' list.Add(attach.Split(";")(1))
            'email.sendMail(_to, _cc1, _cc2, _cc1, WSCTeam, subject, Mail.MailFormat.Html, strBody, list)
            email.sendMail(_to, _cc1, _cc2, _cc1, WSCTeam, subject, Mail.MailFormat.Html, strBody)
            SaveToDataBase()
            'If list.Count > 0 Then
            '    Dim finfo As FileInfo = New FileInfo(attach.Split(";")(0))
            '    If finfo.Exists Then
            '        Try
            '            finfo.Delete()
            '        Catch ex As Exception
            '        End Try
            '    End If
            '    Dim finfo2 As FileInfo = New FileInfo(attach.Split(";")(1))
            '    If finfo2.Exists Then
            '        Try
            '            finfo2.Delete()
            '        Catch ex As Exception
            '        End Try
            '    End If
            'End If
        Catch ex As Exception
            MessageBox.Show("Proses Kirim Email Tidak Berhasil")
        End Try
    End Sub

#End Region

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessionHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If
    End Sub
    Private Function DrawImage(ByVal dealerCode As String, ByVal wsc As String) As String
        Dim objGraphics As Graphics
        Dim fname As String = dealerCode & wsc
        Dim path As String = Server.MapPath("..") & "\" & KTB.DNet.Lib.WebConfig.GetValue("BARCODE")
        Dim location As String = path & wsc & ".jpg"
        Dim GrayBrush = New SolidBrush(Color.Gray)
        Dim BlackBrush = New SolidBrush(Color.Black)
        Dim WhiteBrush = New SolidBrush(Color.White)
        Dim drawBmp As Bitmap = New Bitmap(800, 1000)
        Dim Filelocation As String = GetBarcode(dealerCode, wsc)
        Dim objBitmap As Bitmap = New Bitmap(Filelocation)
        Try
            Response.ContentType = "Image/Jpeg"
            objGraphics = Graphics.FromImage(drawBmp)
            objGraphics.DrawRectangle(New Pen(Color.Black, 5), 0, 0, 800, 1000)
            objGraphics.FillRectangle(WhiteBrush, 5, 5, 790, 990)
            objGraphics.DrawString("ABCDEF123456789012", New Font(FontFamily.GenericMonospace, 36, FontStyle.Bold), BlackBrush, 20, 20)
            objGraphics.DrawString("123456", New Font(FontFamily.GenericMonospace, 36, FontStyle.Bold), BlackBrush, 600, 20)
            objGraphics.DrawString("Part Number : ", New Font(FontFamily.GenericMonospace, 36, FontStyle.Bold), BlackBrush, 20, 120)
            Dim length As Integer = 35
            Dim height As Integer = 100
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 410, 110, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 410 + length, 110, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 410 + (2 * length), 110, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 410 + (3 * length), 110, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 410 + (4 * length), 110, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 410 + (5 * length), 110, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 410 + (6 * length), 110, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 410 + (7 * length), 110, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 410 + (8 * length), 110, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 410 + (9 * length), 110, length, height)

            objGraphics.DrawString("QTY:", New Font(FontFamily.GenericMonospace, 30, FontStyle.Bold), BlackBrush, 20, 230)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 150, 225, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 150 + length, 225, length, height)

            objGraphics.DrawString("Prod Code:", New Font(FontFamily.GenericMonospace, 30, FontStyle.Bold), BlackBrush, 230, 230)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 500, 225, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 500 + length, 225, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 500 + (2 * length), 225, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 500 + (3 * length), 225, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 500 + (4 * length), 225, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 500 + (5 * length), 225, length, height)
            objGraphics.DrawRectangle(New Pen(Color.Black, 2), 500 + (6 * length), 225, length, height)
            objGraphics.DrawImage(objBitmap, 0, 400, 800, 600)

            ResizePhoto(drawBmp).Save(location, ImageFormat.Jpeg)
        Finally
            objBitmap.Dispose()
            drawBmp.Dispose()
        End Try
        Return Filelocation & ";" & location
    End Function
    Private Function ResizePhoto(ByVal bmp As Bitmap) As Bitmap
        If (bmp.Width <= MAX_WIDTH) AndAlso (bmp.Height <= MAX_HEIGHT) Then
            Return bmp
        End If

        Dim w, h As Integer
        If bmp.Height > bmp.Width Then
            w = MAX_WIDTH * bmp.Width / bmp.Height
            h = MAX_HEIGHT * bmp.Height / bmp.Height
        Else
            w = MAX_WIDTH * bmp.Width / bmp.Width
            h = MAX_HEIGHT * bmp.Height / bmp.Width
        End If
        Return bmp.GetThumbnailImage(w, h, New System.Drawing.Image.GetThumbnailImageAbort(AddressOf Abort), IntPtr.Zero)
    End Function
    Private Function Abort() As Boolean
        Return False
    End Function

End Class
