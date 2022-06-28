Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.OnlinePayment

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.UI.Helper
Imports System.IO
Imports System.Text
Imports System.Web.Mail

Public Class FrmDueDate
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents icTransDateStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTransDateEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoValidasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgDaftarJatuhTempo As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlTipeObligasi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipeAssignment As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblMKodeDealer As System.Web.UI.WebControls.Label

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
    Private oPaymentObligation As PaymentObligation
    Private oPaymentObligationFacade As New PaymentObligationFacade(User)
    Private sessHelper As New SessionHelper
    Private objDealer As Dealer
    Private criterias As CriteriaComposite
    Private Enum MailAddressType
        ToAdd
        CCAdd
    End Enum
    Private oLoginUserInfo As UserInfo
#End Region

#Region "Custom Methods"

    Private Function GetDistinctEmail(ByVal strEmails As String)
        If Not IsNothing(strEmails) Or strEmails.Length <> 0 Then
            strEmails = strEmails.Substring(0, strEmails.Length - 1)
            Dim _tmpEmails() As String = strEmails.Split(",")
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim bcheck As Boolean = False
            Dim _result As String = String.Empty
            While i <= _tmpEmails.Length - 1
                j = i + 1
                While j <= _tmpEmails.Length - 1
                    If _tmpEmails(i) = _tmpEmails(j) Then
                        bcheck = False
                        Exit While
                    Else
                        bcheck = True
                    End If
                    j = j + 1
                End While
                If i = _tmpEmails.Length - 1 Then
                    bcheck = True
                End If
                If bcheck Then
                    _result += _tmpEmails(i) + ","
                End If
                i = i + 1
            End While
            If _result = String.Empty Then
                Return strEmails
            Else
                Return _result.Substring(0, _result.LastIndexOf(","))
            End If

        Else
            Return ""
        End If
    End Function
    Private Function MessageContent(ByVal objDomain As PaymentObligation, ByVal listdata As ArrayList) As String
        Dim sb As StringBuilder = New StringBuilder
        sb.Append("<table>")
        sb.Append("<tr><td colspan=2>Kepada</td></tr>")
        sb.Append("<tr><td colspan=2>Bapak/Ibu</td></tr>")
        sb.Append("<tr><td colspan=2>Konsumen<br><br></td></tr>")
        sb.Append("<tr><td colspan=2>Kami telah menyelesaikan Validasi Pembayaran <strong>" & objDomain.Dealer.SearchTerm1 & _
                " </strong> dengan detail sebagai berikut.<br></td></tr>")
        sb.Append("<tr><td colspan=2>")

        sb.Append("<table>")
        sb.Append("<tr><td colspan=2></td></tr>")
        sb.Append("<tr><td colspan=2>")
        sb.Append("</table>")

        sb.Append("</td></tr>")

        sb.Append("<tr><td colspan=2>Untuk lebih detail, silahkan dilihat di daftar pembayaran obligasi.<br><br></td></tr>")
        sb.Append("<tr><td colspan=2>Terima Kasih<br><br><br></td></tr>")
        sb.Append("<tr><td></td>")
        sb.Append("<td align=center>D-NET Administrator<br></td></tr>")
        sb.Append("<tr><td></td>")
        sb.Append("<td align=center>PT Mitsubishi Motors Krama Yudha Sales Indonesia</td></tr>")
        sb.Append("<table>")
        Return sb.ToString
    End Function
    Private Function SendEMail(ByVal sendTo As String, ByVal ccTo1 As String, ByVal ccTo2 As String, ByVal from As String, ByVal subject As String, ByVal format As MailFormat, ByVal body As String, ByVal smtp As String) As Boolean
        Dim isSuccess As Boolean = False
        Try
            Dim msgMail As New MailMessage
            msgMail.To = sendTo
            Dim cc As String = String.Empty

            If ccTo1.Trim <> "" Then
                cc = ccTo1
            End If

            If Not IsNothing(ccTo2) Then
                If ccTo2.Trim <> "" Then
                    cc += ";" & ccTo2
                End If
            End If

            msgMail.Cc = cc
            msgMail.From = from
            msgMail.Subject = subject
            msgMail.BodyFormat = format
            msgMail.Body = body
            'msgMail.Priority = _priority
            SmtpMail.SmtpServer = smtp
            SmtpMail.Send(msgMail)

            isSuccess = True

        Catch ex As Exception
            Throw ex
            isSuccess = False
        End Try
        Return isSuccess
    End Function
    Private Function SetFormatEmail(ByVal list As ArrayList) As String
        'Dim sb As StringBuilder = New StringBuilder
        ''Dim strDate As String = Format(DateTime.Now, "dd-MMM-yyyy hh:mm:ss")
        'sb.Append("<html><body><table><tr><td colspan='4'><p>Kepada Yth,</p><p>Melalui email ini disampaikan dokumen obligasi..bla..bla..</p><br></td></tr>")
        'sb.Append("<tr><td>Tipe Assignment</td><td>Tipe Payment</td><td>Tgl Jatuh Tempu</td><td>Jumlah</td></tr>")
        'For Each obj As PaymentObligation In list
        '    sb.Append("<tr><td>" + obj.PaymentAssignmentType.Description + "</td>")
        '    sb.Append("<td>" + obj.PaymentObligationType.Description + "</td>")
        '    sb.Append("<td>" + obj.DueDate.ToString("dd/MM/yyyy") + "</td>")
        '    sb.Append("<td>" + obj.Amount.ToString + "</td></tr>")
        'Next
        'sb.Append("</table></body></html>")
        'Return sb.ToString()
    End Function
    Private Function GetEmailDealer() As String
        Dim retValue As String
        If Not IsNothing(sessHelper.GetSession("DEALER")) Then
            Dim ObjDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            Dim strUserName As String = User.Identity.Name.Trim().Substring(6)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserInfo), "Dealer.ID", MatchType.Exact, ObjDealer.ID))
            criterias.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, strUserName))
            Dim ArrListUserInfo As ArrayList = New UserInfoFacade(User).Retrieve(criterias)
            If ArrListUserInfo.Count > 0 Then
                retValue = CType(ArrListUserInfo(0), UserInfo).Email
            Else
                retValue = ""
            End If
        Else
            retValue = ""
        End If
        Return retValue
    End Function
    Private Function GetEmailId(ByVal objDomain As PaymentObligation, ByVal listdata As ArrayList, ByVal AddType As MailAddressType) As String
        Dim emailAddress As String = String.Empty

        Select Case AddType
            Case MailAddressType.ToAdd
                Dim arrToRecipient As ArrayList
                arrToRecipient = objDomain.PaymentAssignmentType.PaymentAssignmentTypeReffs
                If arrToRecipient.Count > 0 Then
                    For Each assignreff As PaymentAssignmentTypeReff In arrToRecipient
                        If assignreff.UserInfo.Email.Trim <> String.Empty Then
                            emailAddress += assignreff.UserInfo.Email & ","
                        End If
                    Next
                End If
            Case MailAddressType.CCAdd
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BusinessArea), "Dealer.ID", MatchType.Exact, objDomain.Dealer.ID))


                Dim arrCCRecipient As ArrayList = New BusinessAreaFacade(User).Retrieve(criterias)

                If arrCCRecipient.Count > 0 Then
                    For Each _BusinessArea As BusinessArea In arrCCRecipient
                        If _BusinessArea.Email <> String.Empty Then
                            emailAddress += _BusinessArea.Email & ","
                        End If
                    Next
                End If

                Dim ValidateUserID As Integer
                ValidateUserID = CInt(objDomain.ValidateBy.Substring(0, 6))
                If emailAddress.Length > 0 Then
                    emailAddress += New UserInfoFacade(User).Retrieve(ValidateUserID).Email & ","
                Else
                    emailAddress = New UserInfoFacade(User).Retrieve(ValidateUserID).Email & ","
                End If
        End Select

        If emailAddress.Length > 0 Then
            emailAddress = emailAddress.Substring(0, emailAddress.Length - 1)
        End If

        Return emailAddress
    End Function
    Private Function SendNotification(ByVal list As ArrayList, ByVal errMsg As String) As Boolean
        Dim objDomain As PaymentObligation = CType(list(0), PaymentObligation)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim msgTitle As String = "[D-NET-TEST] Validasi Pembayaran " & objDomain.Dealer.SearchTerm1
        Dim msgFormat As String = MessageContent(objDomain, list)
        Dim emailTo As String = GetEmailId(objDomain, list, MailAddressType.ToAdd)
        If Not emailTo.Length > 0 Then
            errMsg += "Alamat Tujuan Tidak Valid \n"
            Return False
        End If
        Dim emailCC As String = GetEmailId(objDomain, list, MailAddressType.CCAdd)
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        '-----------------------------------
        Return SendEMail(emailTo, emailCC, "", emailFrom, msgTitle, MailFormat.Html, msgFormat, smtp)

        'Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        'Dim ObjEmail As DNetMail = New DNetMail(smtp)
        'Dim RetValue As Integer = 0
        'Try
        '    Dim _tempSendTo, _tempCCTo, sendTo, ccTo As String
        '    Dim emailFrom As String = GetEmailDealer()
        '    Dim Subject As String = String.Empty
        '    Dim contentEmail As String = SetFormatEmail(list)
        '    For Each obj As PaymentObligation In list
        '        For Each _user As PaymentAssignmentTypeReff In obj.PaymentAssignmentType.PaymentAssignmentTypeReffs
        '            _tempSendTo += _user.UserInfo.Email.Trim + ","
        '        Next
        '        Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        criteria.opAnd(New criteria(GetType(BusinessArea), "Dealer.DealerCode", MatchType.Exact, obj.Dealer.DealerCode))
        '        criteria.opAnd(New criteria(GetType(BusinessArea), "Kind", MatchType.Exact, CByte(EnumDealerTransKind.DealerTransKind.SalesUnit)))
        '        Dim _arrUserSales As ArrayList = New BusinessAreaFacade(User).Retrieve(criteria)
        '        _tempCCTo += CType(_arrUserSales(0), BusinessArea).Email.Trim + ","
        '        Subject = obj.Dealer.SearchTerm1

        '    Next
        '    Subject = "[D-NET] Validasi Pembayaran " + Subject

        '    sendTo = GetDistinctEmail(_tempSendTo)
        '    ccTo = GetDistinctEmail(_tempCCTo)
        '    Try
        '        ObjEmail.sendMail(sendTo, ccTo, emailFrom, Subject, Mail.MailFormat.Html, contentEmail)
        '        RetValue = 1
        '    Catch ex As Exception
        '        RetValue = -1
        '    End Try

        'Catch ex As Exception
        '    RetValue = -1
        'End Try
        'Return RetValue
    End Function
    Private Function ValidateObligationType(ByVal ObligationType As String) As Boolean
        Dim bcheck As Boolean = True
        Dim i As Integer
        Dim items() As String = ObligationType.Split(";")
        For i = 0 To items.Length - 2
            Dim objObliType As PaymentObligationType = New PaymentObligationTypeFacade(User).Retrieve(items(i))
            If objObliType.ID = 0 Then
                MessageBox.Show("Tipe obligasi " + items(i) + "tidak valid")
                bcheck = False
                Exit For
            End If

        Next
        Return bcheck
    End Function

    Private Sub BindDataGrid(ByVal IndexPage As Integer)
        CreateCriteria()
        Dim totalRow As Integer = 0
        Dim arl As New ArrayList
        arl = oPaymentObligationFacade.RetrieveByCriteria(CType(sessHelper.GetSession("Criteria"), CriteriaComposite), IndexPage, 1 + dgDaftarJatuhTempo.PageSize, totalRow, CType(sessHelper.GetSession("CurrentSortColumn"), String), CType(sessHelper.GetSession("CurrentSortDirect"), Sort.SortDirection))
        dgDaftarJatuhTempo.DataSource = arl
        dgDaftarJatuhTempo.VirtualItemCount = totalRow
        dgDaftarJatuhTempo.DataBind()
        If arl.Count = 0 Then
            btnProses.Enabled = False
        Else
            btnProses.Enabled = CekPrivCreate()
        End If
    End Sub
    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If oLoginUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "Dealer.DealerCode", MatchType.Exact, lblMKodeDealer.Text))
        ElseIf oLoginUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If txtDealerCode.Text.Trim <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(PaymentObligation), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
            End If
        End If
        
        If ddlTipeObligasi.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "PaymentObligationType.ID", MatchType.Exact, ddlTipeObligasi.SelectedValue))
        End If

        If txtNoValidasi.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "ValidateMD5Code", MatchType.Exact, txtNoValidasi.Text.Trim))
        End If

        If ddlTipeAssignment.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(PaymentObligation), "PaymentAssignmentType.ID", MatchType.Exact, ddlTipeAssignment.SelectedValue))
        End If

        criterias.opAnd(New Criteria(GetType(PaymentObligation), "DueDate", MatchType.GreaterOrEqual, icTransDateStart.Value))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "DueDate", MatchType.LesserOrEqual, icTransDateEnd.Value))

        'Ceriteria untuk jenis obligasi blom ada
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "Status", MatchType.Exact, CByte(EnumOnlinePayment.StatusOnlinePayment.Validasi)))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "SourceDocument", MatchType.Exact, CByte(EnumOnlinePayment.SourceDocument.SAP)))
        sessHelper.SetSession("Criteria", criterias)
    End Sub
    Private Sub BindDDLTipe()
        ddlTipeObligasi.Items.Clear()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PaymentObligationType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PaymentObligationType), "Status", MatchType.Exact, CByte(EnumObligationType.ObligationTypeStatus.Aktif)))

        Dim _arrType As New ArrayList
        _arrType = New PaymentObligationTypeFacade(User).Retrieve(criterias)
        For Each liType As PaymentObligationType In _arrType
            ddlTipeObligasi.Items.Add(New ListItem(liType.Description, liType.ID))
        Next
        ddlTipeObligasi.Items.Insert(0, New ListItem("Pilih Tipe Obligasi", -1))
        ddlTipeObligasi.DataBind()
    End Sub
    Private Sub BindDDLJenis()
        ddlTipeAssignment.Items.Clear()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PaymentAssignmentType), "Status", MatchType.Exact, CByte(EnumObligationType.ObligationTypeStatus.Aktif)))
        criterias.opAnd(New Criteria(GetType(PaymentAssignmentType), "SourceDocument", MatchType.Exact, CByte(EnumOnlinePayment.SourceDocument.SAP)))
        Dim _arrType As New ArrayList
        _arrType = New PaymentAssignmentTypeFacade(User).Retrieve(criterias)
        For Each liType As PaymentAssignmentType In _arrType
            ddlTipeAssignment.Items.Add(New ListItem(liType.Description, liType.ID))
        Next
        ddlTipeAssignment.Items.Insert(0, New ListItem("Pilih Tipe Assignment", -1))
        ddlTipeAssignment.DataBind()
    End Sub
    Private Sub BindStatus()
        ddlStatus.Items.Add(New ListItem(EnumOnlinePayment.StatusOnlinePayment.Validasi.ToString, CByte(EnumOnlinePayment.StatusOnlinePayment.Validasi)))
    End Sub

    Private Function uploadToSAP(ByVal listData As ArrayList) As Boolean
        Dim isSucceess As Boolean = False
        Dim sb As New StringBuilder
        Dim filename = String.Format("{0}{1}{2}", "DNTREC", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\" & filename

        For Each item As PaymentObligation In listData

            Dim RegDocStr As String
            If item.PaymentRegDoc Is Nothing Then
                RegDocStr = ""
            Else
                RegDocStr = item.PaymentRegDoc.ID.ToString
            End If

            Dim strProcessBy As String = String.Empty
            If item.CreatedBy.Trim = String.Empty Then
                strProcessBy = "SAP"
            Else
                strProcessBy = CommonFunction.FormatSavedUser(item.CreatedBy, User).Replace(" ", "")
            End If


            sb.Append(item.Dealer.DealerCode & Chr(9) & "TEMP" & item.ID.ToString & Chr(9) & RegDocStr & Chr(9) & item.PaymentObligationType.Code & Chr(9) & item.Assignment & Chr(9) & item.Amount.ToString("#0") & Chr(9) & item.DocDate.ToString("ddMMyyyy") & Chr(9) & item.TransactionDueDate.ToString("ddMMyyyy") & Chr(9) & strProcessBy & Chr(13) & Chr(10))
        Next

        If (sb.Length > 0) Then
            Dim strData As String
            strData = sb.ToString()
            strData = strData.Substring(0, strData.Length - 2)

            isSucceess = Transfer(DestFile, strData)
        End If

        Return isSucceess

    End Function
    Private Function Transfer(ByVal DestFile As String, ByVal Val As String) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                sw = New StreamWriter(DestFile)
                sw.Write(Val)
                sw.Close()

                imp.StopImpersonate()
                imp = Nothing
                Return True
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return False
        End Try
    End Function

    Private Function PopulateObligation() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        For Each oDataGridItem In dgDaftarJatuhTempo.Items
            chkExport = oDataGridItem.FindControl("chkSelection")
            If chkExport.Checked Then
                Dim _obligation As PaymentObligation = oPaymentObligationFacade.Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                _obligation.Status = EnumOnlinePayment.StatusOnlinePayment.Proses
                _obligation.ConfirmedBy = User.Identity.Name
                _obligation.ConfirmedTime = Today.Date
                oExArgs.Add(_obligation)
            End If
        Next
        Return oExArgs
    End Function
#End Region

#Region "Privilage"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.Pembayaran_daftar_jatuh_tempo_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=INFORMASI PEMBAYARAN - Daftar Jatuh Tempo")
        End If
    End Sub

    Private Function CekPrivCreate() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.Pembayaran_daftar_jatuh_tempo_proses_privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Event Handlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objDealer = CType(sessHelper.GetSession("Dealer"), Dealer)
        oLoginUserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        InitiateAuthorization()
        If oLoginUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            txtDealerCode.Text = oLoginUserInfo.Dealer.DealerCode
            txtDealerCode.Visible = False
            lblPopUpDealer.Visible = False

            lblMKodeDealer.Visible = True
            lblMKodeDealer.Text = oLoginUserInfo.Dealer.DealerCode
        Else
            txtDealerCode.Visible = True
            lblPopUpDealer.Visible = True

            lblMKodeDealer.Visible = False
            lblMKodeDealer.Text = ""
            lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        End If

        If Not IsPostBack Then
            BindStatus()
            BindDDLTipe()
            BindDDLJenis()
            sessHelper.SetSession("CurrentSortColumn", "Dealer.DealerCode")
            sessHelper.SetSession("CurrentSortDirect", Sort.SortDirection.ASC)
            BindDataGrid(0)
        End If
        btnProses.Enabled = CekPrivCreate()
    End Sub
    Private Sub dgDaftarJatuhTempo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDaftarJatuhTempo.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim lblNo As Label = e.Item.FindControl("lblNo")
                lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarJatuhTempo.CurrentPageIndex * dgDaftarJatuhTempo.PageSize)
            End If

        End If
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If icTransDateStart.Value > icTransDateEnd.Value Then
            MessageBox.Show("Tgl jatuh tempo dari tidak boleh lebih besar dari tgl jatuh tempo sampai.")
            Return
        End If

        dgDaftarJatuhTempo.CurrentPageIndex = 0
        BindDataGrid(dgDaftarJatuhTempo.CurrentPageIndex)
    End Sub
    Private Sub btnProses_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProses.Click
        Dim list As ArrayList = PopulateObligation()
        If list.Count > 0 Then
            Try

                Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
                Dim i As Integer = New PaymentRegDocFacade(User).UpdateList(list, objUserInfo, CType(sessHelper.GetSession("DEALER"), Dealer))
                Dim errMsg As String = String.Empty
                If i <> -1 Then
                    If Not uploadToSAP(PopulateObligation()) Then
                        errMsg = "Simpan Data berhasil \n Proses Upload data gagal"
                    End If

                    If Not SendNotification(list, errMsg) Then
                        errMsg = "Simpan Data berhasil \n Proses Upload data Berhasil \n Kirim Email gagal :" & errMsg
                    End If
                Else
                    errMsg = "Proses selesai"
                End If

                If errMsg.Length > 0 Then
                    MessageBox.Show(errMsg)
                Else
                    MessageBox.Show("Proses selesai")
                    BindDataGrid(0)
                End If
            Catch ex As Exception
                MessageBox.Show("Proses gagal")
            End Try
        Else
            MessageBox.Show("Tidak ada data yang di pilih.")
        End If
    End Sub

    Private Sub dgDaftarJatuhTempo_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDaftarJatuhTempo.PageIndexChanged
        dgDaftarJatuhTempo.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgDaftarJatuhTempo.CurrentPageIndex)
    End Sub

    Private Sub dgDaftarJatuhTempo_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDaftarJatuhTempo.SortCommand
        If CType(sessHelper.GetSession("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(sessHelper.GetSession("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    sessHelper.SetSession("CurrentSortDirect", Sort.SortDirection.DESC)

                Case Sort.SortDirection.DESC
                    sessHelper.SetSession("CurrentSortDirect", Sort.SortDirection.ASC)
            End Select
        Else
            sessHelper.SetSession("CurrentSortColumn", e.SortExpression)
            sessHelper.SetSession("CurrentSortDirect", Sort.SortDirection.ASC)
        End If
        BindDataGrid(dgDaftarJatuhTempo.CurrentPageIndex)
    End Sub

#End Region
End Class
