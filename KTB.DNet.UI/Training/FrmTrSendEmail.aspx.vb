#Region "Custom Namespace Imports"
Imports System.Text

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Utility
Imports Ktb.DNet.BusinessFacade.General

#End Region

Public Class FrmTrSendEmail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lsbUp As System.Web.UI.WebControls.ListBox
    Protected WithEvents lsbUPvisible As System.Web.UI.WebControls.ListBox
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCC As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblerror As System.Web.UI.WebControls.Label
    Protected WithEvents btnkirim As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label

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
    Private objTraining As TrUserEmail
    Private arlPartDetail As ArrayList
    Private sessionhelper As New SessionHelper

#End Region

#Region "Custom Method"
    Private Sub GetFormID()
        Dim _Form As String = Request.QueryString("Form")
        If _Form = "Registration" Then
            Dim _arlRegistration As ArrayList = sessionhelper.GetSession("arlRegistration")
            If Not IsNothing(_arlRegistration) Then

            End If
        Else
            Dim _arlAllocation As ArrayList = sessionhelper.GetSession("arlAllocation")
            If Not IsNothing(_arlAllocation) Then

            End If

        End If


        '--Get Form Fill From Session Dealer
        Dim _idDealer As Integer
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        If Not objDealer Is Nothing Then
            _idDealer = objDealer.ID
            lblDealer.Text = objDealer.DealerCode & " " & "/" & " " & objDealer.DealerName
            lblKota.Text = objDealer.City.CityName
            viewstate("Kota") = lblKota.Text
            viewstate("Dealer") = lblDealer.Text
        End If


        '--Get EmailFrom From UserInfo
        If User.Identity.Name <> String.Empty Then
            If Not IsNothing(Session("LOGINUSERINFO")) Then
                viewstate("_User") = CType(Session("LOGINUSERINFO"), UserInfo).Email
                If CStr(viewstate("_User")) = String.Empty Then
                    setbutton()
                End If
            Else
                setbutton()
            End If
        Else
            setbutton()
        End If

    End Sub
#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            GetFormID()
            BindToListBox()
            BindToDropDownList()
        End If
        CheckDropDownList()
    End Sub

    Private Sub BindToListBox()

        '-- Bind To ListBox Up
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrUserEmail), "Tipe", MatchType.Exact, "TO"))
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("UserName")) Then
            sortColl.Add(New Sort(GetType(TrUserEmail), "UserName", Sort.SortDirection.ASC))
        Else
            sortColl = Nothing
        End If
        Dim arlUserName As ArrayList = New TrUserEmailFacade(User).Retrieve(criterias, sortColl)
        If arlUserName.Count > 0 Then
            For Each item As TrUserEmail In arlUserName
                Dim listItem As New listItem(item.UserName, item.ID)
                Dim listItem1 As New listItem(item.Email, item.ID)
                lsbUp.Items.Add(listItem)
                lsbUPvisible.Items.Add(listItem1)
            Next
        End If
    End Sub

    Private Sub BindToDropDownList() '--DropDownlist EmailCCTo
        Dim ArrayListForDDl As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrUserEmail), "Tipe", MatchType.Exact, "CC"))
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("UserName")) Then
            sortColl.Add(New Sort(GetType(TrUserEmail), "UserName", Sort.SortDirection.ASC))
        Else
            sortColl = Nothing
        End If
        ArrayListForDDl = New TrUserEmailFacade(User).Retrieve(criterias, sortColl)
        If ArrayListForDDl.Count > 0 Then
            For Each item As TrUserEmail In ArrayListForDDl
                Dim listItem As New listItem(item.UserName, item.Email)
                ddlCC.Items.Add(listItem)
                viewstate("cc") = ddlCC.Items(0).Value
            Next
        End If

    End Sub

    Private Sub CheckDropDownList()
        If lsbUp.SelectedIndex = -1 Then
            btnkirim.Enabled = False
        Else
            btnkirim.Enabled = True
        End If
        If ddlCC.SelectedValue <> String.Empty Then
            viewstate("cc") = ddlCC.SelectedValue.ToString
            viewstate("CCName") = ddlCC.SelectedItem.ToString
        End If
    End Sub

    Private Sub setbutton()
        btnkirim.Visible = False
        lblerror.Visible = True
    End Sub

    Private Function GetSelectedItem(ByVal listboxName As ListBox) As String
        Dim _strName As String = String.Empty
        Dim i As Integer = 0
        For Each item As ListItem In listboxName.Items
            If item.Selected Then
                If _strName = String.Empty Then
                    _strName = item.Text
                Else
                    _strName = _strName & "," & item.Text
                End If
            End If
            i += 1
        Next
        Return _strName
    End Function

    Private Function GetSelectedEmail(ByVal listboxName As ListBox, ByVal listBoxEmail As ListBox) As String
        Dim _strEmail As String = String.Empty
        Dim i As Integer = 0
        For Each item As ListItem In listboxName.Items
            If item.Selected Then
                If _strEmail = String.Empty Then
                    _strEmail = listBoxEmail.Items(i).Text
                Else
                    _strEmail = _strEmail & ";" & listBoxEmail.Items(i).Text
                End If
            End If
            i += 1
        Next
        Return _strEmail
    End Function

    Private Sub ddlCC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCC.SelectedIndexChanged
        Dim CC As String = ddlCC.SelectedItem.Text
        viewstate("cc") = CC
    End Sub

    Private Sub btnkirim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnkirim.Click
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim subject As String = "[MMKSI-DNet] Service - Training"
        Dim email As DNetMail = New DNetMail(smtp)
        Dim sb As StringBuilder = New StringBuilder
        Dim _Form As String = Request.QueryString("Form")

        Dim _Dealer As String = viewstate("Dealer")
        Dim _CC As String = viewstate("cc")
        Dim _User As String = viewstate("_User")
        Dim _Kota As String = viewstate("Kota")
        Dim _DateNow As DateTime = DateTime.Now
        Dim _Date As String = Format(_DateNow, "dd-MMM-yyyy hh:mm:ss")

        If _Form = "Registration" Then
            Dim _arlRegistration As ArrayList = sessionhelper.GetSession("arlRegistration")
            If Not IsNothing(_arlRegistration) OrElse (_arlRegistration.Count > 0) Then

                '-- HTML Body For Registration Training
                sb.Append("<html><body><Table width=600px><h1><td colspan = 4 align=center><b>")
                sb.Append("PT Mitsubishi Motors Krama Yudha Sales Indonesia")
                sb.Append("</b></td> </h1><tr><td><br></td></tr><tr><td>")
                sb.Append("Jakarta, ")
                sb.Append(_Date)
                sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
                sb.Append("Kepada Yth,")
                sb.Append("</td> </tr><tr><td>")
                sb.Append(_Dealer)
                sb.Append("</td></tr><tr><td>")
                sb.Append(_Kota)
                sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
                sb.Append("Hal: Penolakan calon siswa")
                sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
                sb.Append("Sehubungan dengan adanya perubahan alokasi dealer Anda, maka kami akan menghapus/menolak siswa yang telah terdaftar dengan data:")
                sb.Append("</td></tr><tr><td><br>	<table width=600px bgcolor=black cellspacing=1 border=1 cellpadding=2><tr bgcolor=white ><td align=center>")
                sb.Append("Noreg")
                sb.Append("</td><td align=center>")
                sb.Append("Nama Siswa")
                sb.Append("</td><td align=center>")
                sb.Append("Kode Kelas")
                sb.Append("</td></tr>")

                For Each item As TrClassRegistration In _arlRegistration
                    sb.Append("<tr bgcolor=white><td>")
                    sb.Append(item.TrTrainee.ID)
                    sb.Append("</td><td>")
                    sb.Append(item.TrTrainee.Name)
                    sb.Append("</td><td>")
                    sb.Append(item.TrClass.ClassName)
                    sb.Append("</td></tr>")
                Next

                sb.Append("</table><tr><td><br></td></tr><tr><td>")
                sb.Append("Pendaftaran ulang dapat dilakukan setelah Anda menerima email perihal Pemberitahuan Perubahan Alokasi dari kami.")
                sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
                sb.Append("Atas perhatian dan kerjasamanya kami ucapkan terima kasih.")
                sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
                sb.Append("</td></tr><tr><td>")
                sb.Append("Hormat kami,")
                sb.Append("</td></tr><tr><td>")
                sb.Append("Training Dept.")
                sb.Append("</td></tr></table></body></table></html>")

                '-- Delete From Database
                'Dim facade As TrClassRegistrationFacade = New TrClassRegistrationFacade(User)
                'For Each item As TrClassRegistration In _arlRegistration
                '    facade.DeleteFromDB(item)
                'Next
            End If

        Else

            '-- HTML Body For Allocation Training
            sb.Append("<html><body><Table width=600px><h1><td colspan = 4 align=center><b>")
            sb.Append("PT Mitsubishi Motors Krama Yudha Sales Indonesia")
            sb.Append("</b></td></h1><tr><td><br></td></tr><tr><td>")
            sb.Append("Jakarta,")
            sb.Append(_Date)
            sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
            sb.Append("Kepada Yth,")
            sb.Append("</td> </tr><tr><td>")
            sb.Append(_Dealer)
            sb.Append("</td> </tr><tr><td>")
            sb.Append(_Kota)
            sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
            sb.Append("Hal: Pemberitahuan perubahan alokasi")
            sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
            sb.Append("Sehubungan dengan adanya perubahan alokasi yang telak kami beritahukan sebelumnya via e-mail, maka alokasi dealer Anda mengalami perubahan sbb:")
            sb.Append("</td></tr><tr><td><table width=400px bgcolor=black cellspacing=1 border=1 cellpadding=2><tr bgcolor=white ><td width=25%></td> <td align=center>")
            sb.Append("Alokasi Awal")
            sb.Append("</td> <td align=center>")
            sb.Append("Alokasi Akhir")
            sb.Append("</td></tr><tr bgcolor=white><td width=25%>")
            sb.Append("Kode Kelas")
            sb.Append("</td><td align=right>5</td><td align=right>4</td></tr></table><tr><td><br></td></tr><tr><td>")
            sb.Append("Saat ini Anda dapat melakukan pendaftaran ulang untuk alokasi dan kode kelas seperti tersebut diatas.")
            sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
            sb.Append("Terima kasih.")
            sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
            sb.Append("Hormat kami,")
            sb.Append("</td></tr><tr><td>")
            sb.Append("Training Dept.")
            sb.Append("</td></tr></table></body></table></html>")

        End If

        Dim strBody As String = sb.ToString
        Try
            Dim strEmailTo As String = GetSelectedEmail(lsbUp, lsbUPvisible)
            email.sendMail(strEmailTo, _CC, _User, subject, Mail.MailFormat.Html, strBody)
            MessageBox.Show("Email Berhasil dikirim")
        Catch ex As Exception
            MessageBox.Show("Proses Kirim Email Tidak Berhasil")
        End Try
    End Sub

#End Region


End Class
