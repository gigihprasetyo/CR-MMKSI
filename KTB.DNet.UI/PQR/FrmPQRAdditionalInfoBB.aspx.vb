Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Security
Imports System.IO

Public Class FrmPQRAdditionalInfoBB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtPengirim As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents inFileLocation As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents pnlViewPQRAdditionalInfoBB As System.Web.UI.WebControls.Panel
    Protected WithEvents lblPQRNo As System.Web.UI.WebControls.Label
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents DivEntry As System.Web.UI.HtmlControls.HtmlGenericControl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
#Region "Private Variables"
    Private sessHelper As New SessionHelper
    Private _PQRHeaderBB As PQRHeaderBB
    Private AttachmentDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("PQRAddtionalInfoAttachment")
#End Region

#Region "Cek privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PQRReplyDiscuss_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PRODUCT QUALITY REPORT - Buat PQR")
        End If
    End Sub

    Dim bCekGridPriv As Boolean = SecurityProvider.Authorize(context.User, SR.PQRReplyDiscuss_Privilege)
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        _PQRHeaderBB = New PQRHeaderBBFacade(User).Retrieve(CInt(Request.QueryString("PQRID")))
        lblPQRNo.Text = _PQRHeaderBB.PQRNo
        If _PQRHeaderBB.PQRAdditionalInfoBBs.Count > 0 Then
            GenerateControl(_PQRHeaderBB.PQRAdditionalInfoBBs)
        End If

        If Not IsPostBack Then
            If Request.QueryString("Src") = "WSCDetail" Or Request.QueryString("Src") = "FrmPQRListKondisi" Then
                DivEntry.Visible = False
                btnSimpan.Visible = False
            Else
                If CType(_PQRHeaderBB.RowStatus, EnumPQR.PQRStatus) = EnumPQR.PQRStatus.Proses Then
                    DivEntry.Visible = True
                    btnSimpan.Visible = bCekGridPriv
                Else
                    DivEntry.Visible = False
                    btnSimpan.Visible = False
                End If
            End If
            txtPengirim.Text = ""
        End If

    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        If ValidateSave() Then

            Dim result As Integer
            '_PQRHeaderBB = CType(sessHelper.GetSession("AIPQRHeaderBB"), PQRHeaderBB)
            Dim _objPQRAdditionalInfoBB As New PQRAdditionalInfoBB
            _objPQRAdditionalInfoBB.Sender = txtPengirim.Text
            _objPQRAdditionalInfoBB.PQRHeaderBB = _PQRHeaderBB
            If Not IsNothing(inFileLocation) And inFileLocation.Value <> String.Empty Then
                Dim Filename As String = inFileLocation.PostedFile.FileName.Split("\")(inFileLocation.PostedFile.FileName.Split("\").Length - 1)
                _objPQRAdditionalInfoBB.Attachment = AttachmentDirectory & "\" & _PQRHeaderBB.PQRNo.Replace("/", "-") & "\" & DateTime.Now.ToString("ddMMyyyyHHmmss_") & Filename
            Else
                _objPQRAdditionalInfoBB.Attachment = ""
            End If

            If Not IsNothing(inFileLocation) And inFileLocation.Value <> String.Empty Then
                If UploadFile(inFileLocation.PostedFile, _objPQRAdditionalInfoBB.Attachment) = -1 Then
                    MessageBox.Show(SR.SaveFail)
                    Return
                End If
            End If

            result = New PQRAdditionalInfoBBFacade(User).Insert(_objPQRAdditionalInfoBB)
            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show("File berhasil disimpan")

                _PQRHeaderBB = New PQRHeaderBBFacade(User).Retrieve(CInt(Request.QueryString("PQRID")))
                sessHelper.SetSession("PQR", _PQRHeaderBB)
                GenerateControl(_PQRHeaderBB.PQRAdditionalInfoBBs)

                txtPengirim.Text = ""

            End If

        End If
    End Sub
    Private Function ValidateSave() As Boolean
        If txtPengirim.Text = String.Empty Then
            MessageBox.Show("Silakan di isi data Pengirim!")
            Return False
        End If

        If Not IsNothing(inFileLocation) And inFileLocation.Value <> String.Empty Then
            'If Not CheckExt(Path.GetExtension(inFileLocation.PostedFile.FileName)) Then
            '    MessageBox.Show("Tipe file salah!")
            '    Return False
            'End If
        End If
        Return True

    End Function
    Private Function UploadFile(ByRef SourceFile As HttpPostedFile, ByVal Target As String) As Integer
        'Dim SrcFile As String = Path.GetFileName(fileUpload.PostedFile.FileName)  '-- Source file name
        'Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ForumAttnDir") & "\Forum" & ForumTopicId & "\Hdr" & SrcFile   '-- Destination file
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(SourceFile) Then
                    finfo = New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") + Target)

                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    SourceFile.SaveAs(KTB.DNet.Lib.WebConfig.GetValue("SAN") + Target)
                    Return 1
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Private Function CheckExt(ByVal ext As String) As Boolean
        If ext.ToUpper() = ".PDF" Or ext.ToUpper() = ".XLS" Or ext.ToUpper() = ".DOC" Or ext.ToUpper() = ".ZIP" Or ext.ToUpper() = ".RAR" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub GenerateControl(ByVal _arrInfo As ArrayList)
        pnlViewPQRAdditionalInfoBB.Controls.Clear()

        With pnlViewPQRAdditionalInfoBB.Controls
            .Add(New LiteralControl("<table width='100%'><tr><td colspan=3><hr width = 100% style='HEIGHT: 2px'></td></tr>"))
            Dim x As Integer = 1
            For Each item As PQRAdditionalInfoBB In _arrInfo
                Dim sCode As New System.Text.StringBuilder
                'style="BORDER-RIGHT: black thin solid; BORDER-TOP: black thin solid; BORDER-LEFT: black thin solid; BORDER-BOTTOM: black thin solid"
                sCode.Append("<tr>")
                sCode.Append("<td colspan=2 ></td>")
                sCode.Append("<td >")
                sCode.Append("<span style=""FONT-SIZE: 0.8em; WIDTH: 100%; TEXT-ALIGN: right"">Posted by : " & CommonFunction.FormatSavedUser(item.CreatedBy, User) & "</span>")
                sCode.Append("<br>")
                sCode.Append("<span style=""FONT-SIZE: 0.8em; WIDTH: 100%; TEXT-ALIGN: right"">Posted at : " & item.CreatedTime.ToString("dd/MM/yyyy HH:mm:ss") & "</span>")
                sCode.Append("</td>")
                sCode.Append("</tr>")
                sCode.Append("<tr valign='top'>")
                sCode.Append("<td class='titleField' width=20% >Pertanyaan/Jawaban</td>")
                sCode.Append("<td width=1%>:</td>")
                sCode.Append("<td>")
                sCode.Append("<p>")
                sCode.Append(item.Sender.Replace((Chr(13) & Chr(10)).ToString, "<BR>"))
                sCode.Append("</p>")
                sCode.Append("</td>")
                sCode.Append("</tr>")
                sCode.Append("<tr valign='top'>")
                sCode.Append("<td class=""titleField"" valign='top' width=20%>Attachment</td>")
                sCode.Append("<td width=1%>:</td>")
                sCode.Append("<td>")
                .Add(New LiteralControl(sCode.ToString))

                If Not item.Attachment = String.Empty Then
                    Dim linkAttachment As New LinkButton
                    linkAttachment.ID = "linkAttachment" + x.ToString
                    linkAttachment.CommandArgument = item.Attachment
                    If item.Attachment.Substring(item.Attachment.LastIndexOf("\") + 1).Split("_").Length = 1 Then
                        linkAttachment.Text = item.Attachment.Substring(item.Attachment.LastIndexOf("\") + 1).Split("_")(0)
                    Else
                        linkAttachment.Text = item.Attachment.Substring(item.Attachment.LastIndexOf("\") + 1).Split("_")(1)
                    End If
                    'linkAttachment.Attributes("onclick") = "ShowDialog(" & linkAttachment.CommandArgument & ");"
                    AddHandler linkAttachment.Click, AddressOf Me.LinkButton_Click

                    .Add(linkAttachment)
                End If

                sCode = New System.Text.StringBuilder
                sCode.Append("</td>")
                sCode.Append("</tr>")
                sCode.Append("<tr>")
                sCode.Append("<td colspan=3><hr width = 100% style='HEIGHT: 2px'></td>")
                sCode.Append("</tr>")
                .Add(New LiteralControl(sCode.ToString))
                x = x + 1
            Next
            .Add(New LiteralControl("</table>"))
        End With
    End Sub

    Private Sub LinkButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As LinkButton = CType(sender, LinkButton)
        Response.Redirect("../Download.aspx?file=" & objSender.CommandArgument)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If Request.QueryString("Src") = "ListPQR" Then
            Server.Transfer("~/PQR/FrmPQRListBB.aspx")
        Else
            Server.Transfer("~/PQR/FrmPQRHeaderBB.aspx?Mode=" & Request.QueryString("Mode") & "&Src=" & Request.QueryString("Src"))
        End If
    End Sub
End Class
