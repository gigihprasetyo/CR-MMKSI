Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports System.Drawing.Color
Imports System.IO
Imports System.Text
Public Class FrmPartShopUpload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dgPartShop As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnRequestID As System.Web.UI.WebControls.Button
    Protected WithEvents lblFileName As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents dfPartShop As System.Web.UI.HtmlControls.HtmlInputFile

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private sessHelper As New SessionHelper
    Private arlPartShop As New ArrayList
    Private objDealer As Dealer
    Private bIsError As Boolean = False
#End Region

#Region "Event"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblPageTitle.Text = "PARTSHOP - Upload Data Part Shop"
        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            txtDealerCode.Text = objDealer.DealerCode
            lblDealer.Visible = True
            lblDealer.Text = objDealer.DealerCode
            txtDealerCode.Visible = False
        End If
        If Not IsPostBack Then
            btnSave.Enabled = False
            btnRequestID.Enabled = False
            sessHelper.SetSession("CurrentSortColumn", "CityPart.CityName")
            sessHelper.SetSession("CurrentSortDirect", Sort.SortDirection.ASC)
        End If
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        bIsError = False
        BindUpload()
        bIsError = CType(sessHelper.GetSession("sessError"), Boolean)
        If Not bIsError And Path.GetFileName(dfPartShop.PostedFile.FileName).ToString.Trim <> String.Empty Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
        btnRequestID.Enabled = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim nResult As Integer = -1
        Try
            Dim newArlPartShop As New ArrayList
            Dim objPartShopFacade As PartShopFacade = New PartShopFacade(User)
            arlPartShop = CType(sessHelper.GetSession("PARTSHOPLIST"), ArrayList)
            If arlPartShop.Count > 0 Then
                For Each oPartShop As PartShop In arlPartShop
                    nResult = objPartShopFacade.Insert(oPartShop)
                    oPartShop.ID = nResult
                    newArlPartShop.Add(oPartShop)
                Next
            End If
            sessHelper.SetSession("PARTSHOPLIST", newArlPartShop)
            BindDataGridPartShop(dgPartShop.CurrentPageIndex)
        Catch ex As Exception
            nResult = -1
        End Try
        If nResult <> -1 Then
            MessageBox.Show(SR.SaveSuccess)
            btnRequestID.Enabled = True
            btnSave.Enabled = False
        Else
            MessageBox.Show(SR.SaveFail)
            btnRequestID.Enabled = False
            btnSave.Enabled = True
        End If
    End Sub

    Private Sub btnRequestID_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRequestID.Click
        Dim iChecked As Integer = 0
        arlPartShop = CType(sessHelper.GetSession("PARTSHOPLIST"), ArrayList)
        Dim arlRequestID As New ArrayList
        For Each item As DataGridItem In dgPartShop.Items
            Dim chkItem As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim status As Label = CType(item.FindControl("lblStatus"), Label)
            If (chkItem.Checked) And (status.Text = "Baru") Then
                iChecked += 1
                Dim id As Integer = item.Cells(0).Text
                If id > 0 Then
                    Dim objPartShop As PartShop = New PartShopFacade(User).Retrieve(id)
                    If Not objPartShop Is Nothing Then
                        objPartShop.Status = 1 'Request Registered
                        Dim nResult As Integer = New PartShopFacade(User).Update(objPartShop)
                        If nResult <> -1 Then
                            arlRequestID.Add(objPartShop)
                            For i As Integer = 0 To arlPartShop.Count - 1
                                If CType(arlPartShop(i), PartShop).ID = objPartShop.ID Then
                                    arlPartShop.Item(i) = objPartShop
                                End If
                            Next i
                        End If
                    End If
                End If
            End If
        Next
        sessHelper.SetSession("PARTSHOPLIST", arlPartShop)
        If arlRequestID.Count > 0 Then
            SendEmail(arlRequestID)
            BindDataGridPartShop(dgPartShop.CurrentPageIndex)
        End If

        If iChecked = 0 Then
            MessageBox.Show("Tidak ada data part shop baru yang dipilih")
        End If
    End Sub

    Private Sub dgPartShop_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartShop.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As PartShop = CType(e.Item.DataItem, PartShop)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
                Dim lblNamaPartShop As Label = CType(e.Item.FindControl("lblNamaPartShop"), Label)
                Dim lblAlamat As Label = CType(e.Item.FindControl("lblAlamat"), Label)
                Dim lblPropinsi As Label = CType(e.Item.FindControl("lblPropinsi"), Label)
                Dim lblKota As Label = CType(e.Item.FindControl("lblKota"), Label)
                Dim lblTelephone As Label = CType(e.Item.FindControl("lblTelephone"), Label)
                Dim lblFax As Label = CType(e.Item.FindControl("lblFax"), Label)
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                Dim lblKeterangan As Label = CType(e.Item.FindControl("lblKeterangan"), Label)
                Dim enumStatus As EnumPartShopStatus.PartShopStatus = CType(RowValue.Status, Integer)

                lblNo.Text = e.Item.ItemIndex + 1 + (dgPartShop.CurrentPageIndex * dgPartShop.PageSize)
                If Not RowValue.Dealer Is Nothing Then
                    lblKodeDealer.Text = RowValue.Dealer.DealerCode
                Else
                    lblKodeDealer.Text = String.Empty
                End If

                lblNamaPartShop.Text = RowValue.Name
                lblAlamat.Text = RowValue.Address
                If RowValue.CityPart Is Nothing Then
                    lblPropinsi.Text = String.Empty
                    lblKota.Text = String.Empty
                Else
                    lblPropinsi.Text = RowValue.CityPart.Province.ProvinceName
                    lblKota.Text = RowValue.CityPart.CityName
                End If
                lblTelephone.Text = RowValue.Phone
                lblFax.Text = RowValue.Fax
                lblStatus.Text = enumStatus.ToString

                If Not RowValue.ErrorMessage <> "" Then
                    lblKeterangan.Text = "OK"
                    lblKeterangan.BackColor = GreenYellow
                Else
                    lblKeterangan.Text = RowValue.ErrorMessage
                    lblKeterangan.BackColor = LightSalmon
                    bIsError = True
                End If
            End If
            sessHelper.SetSession("sessError", bIsError)
        End If
    End Sub

#End Region

#Region "Custom"


    Private Sub BindDataGridPartShop(ByVal idxPage As Integer)
        Dim arList = CType(sessHelper.GetSession("PARTSHOPLIST"), ArrayList)
        arlPartShop = ArrayListPager.DoPage(arList, idxPage, dgPartShop.PageSize)
        dgPartShop.DataSource = arlPartShop
        dgPartShop.VirtualItemCount = arlPartShop.Count
        dgPartShop.CurrentPageIndex = 0
        dgPartShop.DataBind()
    End Sub

    Private Sub BindUpload()
        dgPartShop.CurrentPageIndex = 0
        Dim strError As String = ""

        If (Not dfPartShop.PostedFile Is Nothing) And (dfPartShop.PostedFile.ContentLength > 0) And _
        ((dfPartShop.PostedFile.ContentType.ToLower() = "text/plain") Or (dfPartShop.PostedFile.ContentType.ToLower() = "text/csv") _
        Or (dfPartShop.PostedFile.ContentType.ToLower() = "application/octet-stream") Or (dfPartShop.PostedFile.ContentType.ToLower() = "application/vnd.ms-excel")) Then
            Dim Extension As String = Path.GetExtension(dfPartShop.PostedFile.FileName)

            If Extension.ToUpper = ".CSV" Or Extension.ToUpper = ".TXT" Then

                Dim SrcFile As String = Format(Date.Now, "yyyyMMddHHmmss") & Path.GetFileName(dfPartShop.PostedFile.FileName)
                Dim DestFile As String = Server.MapPath("") & "\..\DataFile\" & SrcFile
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim parser As New PartShopParser
                Dim arList As ArrayList

                Try
                    'If imp.Start() Then
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(dfPartShop.PostedFile.InputStream, DestFile)

                    '    imp.StopImpersonate()
                    '    imp = Nothing

                    parser.Dealer = CType(Session("DEALER"), Dealer)
                    arList = CType(parser.ParsingNoTrasaction(DestFile, User.Identity.Name), ArrayList)

                    'End If
                Catch ex As Exception

                End Try
                If (Not arList Is Nothing) AndAlso arList.Count > 0 Then
                    'arlPartShop = ArrayListPager.DoPage(arList, 0, dgPartShop.PageSize)
                    'dgPartShop.DataSource = arlPartShop
                    'dgPartShop.VirtualItemCount = arlPartShop.Count
                    'dgPartShop.CurrentPageIndex = 0
                    'dgPartShop.DataBind()

                    sessHelper.SetSession("PARTSHOPLIST", arList)
                    BindDataGridPartShop(dgPartShop.CurrentPageIndex)
                Else
                    dgPartShop.DataSource = New ArrayList
                    dgPartShop.DataBind()
                End If

                If parser.IsAllowToSave() Then
                    Me.btnSave.Enabled = False
                Else
                    Me.btnSave.Enabled = True
                End If
            Else
                MessageBox.Show("Jenis file tidak sesuai (file yang diterima plain/text)")
            End If
        Else
            MessageBox.Show("Pilih file yang akan di-upload.")
        End If

    End Sub

    Private Sub RequestID()
        Dim iChecked As Integer = 0
        Dim arlPartShop As New ArrayList

        For Each item As DataGridItem In dgPartShop.Items
            Dim chkItem As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim status As Label = CType(item.FindControl("lblStatus"), Label)
            If (chkItem.Checked) And (status.Text = "Baru") Then
                iChecked += 1
                Dim id As Integer = item.Cells(0).Text
                If id > 0 Then
                    Dim objPartShop As PartShop = New PartShopFacade(User).Retrieve(id)
                    If Not objPartShop Is Nothing Then
                        objPartShop.Status = 1 'Request Registered
                        Dim nResult As Integer = New PartShopFacade(User).Update(objPartShop)
                        arlPartShop.Add(objPartShop)
                    End If
                End If
            End If
        Next
        If arlPartShop.Count > 0 Then
            SendEmail(arlPartShop)
            'BindDataGridPartShop(dgPartShop.CurrentPageIndex)
        End If

        If iChecked = 0 Then
            MessageBox.Show("Tidak ada data part shop baru yang dipilih")
        End If
    End Sub

    Private Sub SendEmail(ByVal arlPartShop As ArrayList)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        Dim emailTo As String = KTB.DNet.Lib.WebConfig.GetValue("EmailSPAdmin")
        Dim valueEmail As String = GenerateEmail(arlPartShop)

        ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet] Parts - Request Part Shop Code ", Mail.MailFormat.Html, valueEmail)
    End Sub

    Private Function GenerateEmail(ByVal arlPartShop As ArrayList) As String

        Dim sb As StringBuilder = New StringBuilder("")
        sb.Append("<FONT face=Arial size=1>")
        sb.Append("<table width=700>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 align=center><b>Request Part Shop Code</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50>")
        sb.Append("Dengan hormat,&nbsp;")
        sb.Append("<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Berikut daftar Part Shop untuk dibuatkan kode :")
        sb.Append("</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=10></td>")
        sb.Append("</tr>")

        sb.Append("<tr>")
        sb.Append("<td height=50 colspan=6 align=center><hr><b>Daftar Part Shop</b></td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("<table border=1 width=700 cellpadding=0>")
        sb.Append("<tr>")
        sb.Append("<td width=30>No</td>")
        sb.Append("<td width=295>Nama</td>")
        sb.Append("<td width=295>Alamat</td>")
        sb.Append("<td width=100>Telephone/Pc</td>")
        sb.Append("<td width=100>Fax</td>")
        sb.Append("<td width=295>Kota</td>")
        sb.Append("<td width=295>Propinsi</td>")
        sb.Append("</tr>")

        Dim counter As Integer = 0

        For Each itemDetail As PartShop In arlPartShop
            counter += 1
            sb.Append("<tr>")
            sb.Append("<td>" & counter.ToString & "</td>")
            sb.Append("<td>" & itemDetail.Name & "</td>")
            sb.Append("<td>" & itemDetail.Address & "</td>")
            sb.Append("<td>" & itemDetail.Phone & "</td>")
            sb.Append("<td>" & itemDetail.Fax & "</td>")
            sb.Append("<td>" & itemDetail.CityPart.CityName & "</td>")
            sb.Append("<td>" & itemDetail.CityPart.Province.ProvinceName & "</td>")
            sb.Append("</tr>")
        Next
        sb.Append("</table>")

        sb.Append("<table width=700>")

        sb.Append("<tr>")
        sb.Append("<td></td>")
        sb.Append("<td colspan=4><font color='blue'>- Mohon untuk dibuatkan kode untuk daftar part shop diatas</font></td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("</FONT>")

        Return sb.ToString

    End Function
#End Region

End Class
