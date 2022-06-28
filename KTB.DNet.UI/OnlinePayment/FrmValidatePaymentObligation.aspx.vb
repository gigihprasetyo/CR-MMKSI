#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.OnlinePayment
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports System.Text
Imports System.Web.Mail
Imports System.Security.Principal
Imports System.Web.Security
Imports System.Configuration
Imports System.IO
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Public Class FrmValidatePaymentObligation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents listParrent As System.Web.UI.WebControls.DataList
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtPassword As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnChekPassword As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents pnlPassword As System.Web.UI.WebControls.Panel
    Protected WithEvents lblIP As System.Web.UI.WebControls.Label
    Protected WithEvents lblDisclaimer As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblAssignmentType As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private _sesshelper As New SessionHelper
    Private ArlPaymentObl As ArrayList = New ArrayList
    Private ArlPaymentOblParent As ArrayList = New ArrayList
    Private TIPEDOC As String = String.Empty
    Private ASSIGNMENTTYPE As String = String.Empty
    Private TotalAmmount As Long

    Private bProcessValidatePriv As Boolean

    Private oLoginUserInfo As UserInfo
    Private identity As IIdentity
    Private Enum MailAddressType
        ToAdd
        CCAdd
    End Enum
#End Region


#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        TIPEDOC = Request.QueryString("tipe")
        ASSIGNMENTTYPE = Request.QueryString("tipeassign")
        InitiateAuthorization()
        oLoginUserInfo = CType(_sesshelper.GetSession("LOGINUSERINFO"), UserInfo)

        If Not IsPostBack Then
            lblKodeDealer.Text = oLoginUserInfo.Dealer.DealerCode
            lblAssignmentType.Text = ASSIGNMENTTYPE
            lblIP.Text = HttpContext.Current.Request.Params("REMOTE_ADDR")
            BindToGrid()
        End If

    End Sub
    Private Sub listParrent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listParrent.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim oPaymentObl As PaymentObligation = e.Item.DataItem
            Dim dtgDetail As DataGrid = CType(e.Item.FindControl("dtgListPaymentObligation"), DataGrid)
            Dim arlDetails As New ArrayList
            Dim criteriaDetails As CriteriaComposite = GetCriteriasForDetails(oPaymentObl.Dealer.ID, oPaymentObl.Assignment, oPaymentObl.PaymentAssignmentType.ID, oPaymentObl.SourceDocument, oPaymentObl.IsTOP)
            arlDetails = New PaymentObligationFacade(User).Retrieve(criteriaDetails)
            dtgDetail.DataSource = arlDetails
            dtgDetail.DataBind()

            TotalAmmount += oPaymentObl.TotalAmount

        End If
    End Sub
    Private Sub btnChekPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChekPassword.Click
        If IsUserAuthenticate(txtPassword.Text) Then
            Process()
            'MessageBox.Show("Login Success")

            Server.Transfer("~/OnlinePayment/FrmPaymentObligation.aspx?tipe=" & TIPEDOC)
        Else
            MessageBox.Show("Password tidak valid")
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Server.Transfer("~/OnlinePayment/FrmPaymentObligation.aspx?tipe=" & TIPEDOC)
    End Sub

#End Region

#Region "Custhom Method"
#Region "Check Privilage"
    Private Sub InitiateAuthorization()
        bProcessValidatePriv = SecurityProvider.Authorize(context.User, SR.Pembayaran_daftar_detail_pembayaran_sap_validasi_privilege)

        pnlPassword.Visible = bProcessValidatePriv
    End Sub

#End Region

    Private Sub BindToGrid()

        Dim ArlSelectedPaymentObl = New ArrayList
        ArlSelectedPaymentObl = CType(_sesshelper.GetSession("ListSelectedPaymentOblParent"), ArrayList)

        TotalAmmount = 0
        If (ArlSelectedPaymentObl.Count > 0) Then
            listParrent.DataSource = ArlSelectedPaymentObl
            listParrent.DataBind()
        Else
            listParrent.DataSource = New ArrayList
            listParrent.DataBind()
        End If
        lblTotal.Text = TotalAmmount.ToString("#,##0")

    End Sub
    Private Function GetCriteriasForDetails(ByVal _dealerID As Integer, ByVal _Assignment As String, ByVal _PaymentAssignmentTypeID As Integer, ByVal _SourceDocument As Integer, ByVal _isTOP As Integer) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentObligation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "Status", MatchType.Exact, CInt(EnumOnlinePayment.StatusOnlinePayment.Baru)))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "Dealer.ID", MatchType.Exact, _dealerID))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "Assignment", MatchType.Exact, _Assignment))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "SourceDocument", MatchType.Exact, _SourceDocument))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "PaymentAssignmentType.ID", MatchType.Exact, _PaymentAssignmentTypeID))
        criterias.opAnd(New Criteria(GetType(PaymentObligation), "IsTOP", MatchType.Exact, _isTOP))
        Return criterias
    End Function

    Private Function IsUserAuthenticate(ByVal password As String) As Boolean
        Dim result As Boolean = False
        Dim ssHelper As SessionHelper = New SessionHelper
        Dim objUser As UserInfo = ssHelper.GetSession("LOGINUSERINFO")
        Dim objDealer As Dealer = ssHelper.GetSession("DEALER")
        If (Not objUser Is Nothing) And (Not objDealer Is Nothing) Then
            Dim pwdBytes As Byte() = Encoding.Unicode.GetBytes(password)
            Dim strDealerID As String = objDealer.ID.ToString.PadLeft(6, "0")
            Dim authenticated As Boolean = SecurityProvider.Authenticate(strDealerID & objUser.UserName.Trim, pwdBytes, identity)
            If authenticated Then
                result = True
            End If
        End If
        Return result
    End Function

    Private Sub Process()
        Dim ListToProcess As New ArrayList
        Dim listChild As ArrayList = CType(_sesshelper.GetSession("ListPaymentOblDetail"), ArrayList)
        Dim listSelected As ArrayList = CType(_sesshelper.GetSession("ListSelectedPaymentOblParent"), ArrayList)

        For Each Selectedparent As PaymentObligation In listSelected
            For Each child As PaymentObligation In listChild
                If child.Dealer.DealerCode.Trim.ToUpper = Selectedparent.Dealer.DealerCode.Trim.ToUpper And _
                child.Assignment.Trim.ToUpper = Selectedparent.Assignment.Trim.ToUpper And _
                child.PaymentAssignmentType.ID = Selectedparent.PaymentAssignmentType.ID And _
                child.IsTOP = Selectedparent.IsTOP Then
                    ListToProcess.Add(child)
                End If
            Next
        Next

        Dim iResult As Integer
        Dim errMsg As String = String.Empty
        If New PaymentObligationFacade(User).ValidatePaymentObligation(ListToProcess, CType(_sesshelper.GetSession("LOGINUSERINFO"), UserInfo), CType(_sesshelper.GetSession("DEALER"), Dealer)) > 0 Then
            If Not uploadToSAP(ListToProcess) Then
                errMsg = "Simpan Data berhasil \n Proses Upload data gagal"
            End If
            If Not sendNotification(ListToProcess, errMsg) Then
                errMsg = "Simpan Data berhasil \n Proses Upload data Berhasil \n Kirim Email gagal :" & errMsg
            End If
        Else
            errMsg = "Simpan data gagal. Proses Validasi di batalkan."
        End If

        If errMsg.Length > 0 Then
            MessageBox.Show(errMsg)
        Else
            MessageBox.Show("Proses Validasi Selesai")
        End If

    End Sub

    Private Function uploadToSAP(ByVal listData As ArrayList) As Boolean
        Dim isSucceess As Boolean = False
        Dim sb As New StringBuilder
        Dim filename = String.Format("{0}{1}{2}", "TMPREC", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
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

    Private Function sendNotification(ByVal listData As ArrayList, ByRef errMsg As String) As Boolean
        Dim objDomain As PaymentObligation = CType(listData(0), PaymentObligation)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim msgTitle As String = "[D-NET-TEST] Validasi Pembayaran " & objDomain.Dealer.SearchTerm1
        Dim msgFormat As String = MessageContent(objDomain, listData)
        Dim emailTo As String = GetEmailId(objDomain, listData, MailAddressType.ToAdd)
        If Not emailTo.Length > 0 Then
            errMsg += "Alamat Tujuan Tidak Valid \n"
            Return False
        End If
        Dim emailCC As String = GetEmailId(objDomain, listData, MailAddressType.CCAdd)
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        '-----------------------------------
        Return SendEMail(emailTo, emailCC, "", emailFrom, msgTitle, MailFormat.Html, msgFormat, smtp)

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

#End Region

End Class
