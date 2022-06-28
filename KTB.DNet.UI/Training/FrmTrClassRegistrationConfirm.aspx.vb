#Region "Custom Namespace Imports"
Imports System.Text

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Utility

#End Region

Public Class FrmTrClassRegistrationConfirm
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
    Protected WithEvents dtgClassRegistration As System.Web.UI.WebControls.DataGrid
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
    Private objTraining As TrUserEmail
    Private arlPartDetail As ArrayList
    Private sessionhelper As New SessionHelper

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        GetDealerInfomation()
        BindDataGrid()
    End Sub

#Region "Custom Method"

    Private Sub BindDataGrid()
        Dim data As ArrayList = sessionhelper.GetSession("arlRegistration")
        If IsNothing(data) Then
            Response.Redirect("./FrmViewTrClassRegistration1.aspx")
        End If
        dtgClassRegistration.DataSource = data
        dtgClassRegistration.DataBind()
    End Sub

    Private Sub GetDealerInfomation()

        '--Get Form Fill From Session Dealer
        Dim _idDealer As Integer
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        If Not objDealer Is Nothing Then
            _idDealer = objDealer.ID
            viewstate("Dealer") = objDealer.DealerCode & " " & "/" & " " & objDealer.DealerName
            viewstate("Kota") = objDealer.City.CityName
        End If

        '--Get EmailFrom From UserInfo
        If User.Identity.Name <> String.Empty Then
            If Not IsNothing(Session("LOGINUSERINFO")) Then
                viewstate("_User") = CType(Session("LOGINUSERINFO"), UserInfo).Email
            Else
                viewstate("_User") = String.Empty
            End If
        End If

    End Sub

    '--Send Email To
    Private Function SendTo(ByVal nDealerID As Integer) As String
        Dim _ArlSendTo As ArrayList
        Dim Send As String
        Dim critTo As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critTo.opAnd(New Criteria(GetType(TrUserEmail), "Tipe", MatchType.Exact, "TO"))
        _ArlSendTo = New TrUserEmailFacade(User).Retrieve(critTo)

        Send = GetServiceManagerEmail(nDealerID)
        If Not IsNothing(_ArlSendTo) OrElse _ArlSendTo.Count > 0 Then
            For Each item As TrUserEmail In _ArlSendTo
                If Send = String.Empty Then
                    Send = item.Email
                Else
                    Send = Send & ";" & item.Email
                End If
            Next
        End If
        Return Send
    End Function

    Private Function GetServiceManagerEmail(ByVal nDealerID As Integer) As String
        'Dim objDealer As Dealer = sessionhelper.GetSession("DEALER")
        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCol.opAnd(New Criteria(GetType(BusinessArea), "Dealer.ID", MatchType.Exact, nDealerID))
        critCol.opAnd(New Criteria(GetType(BusinessArea), "Kind", MatchType.Exact, CType(EnumDealerTransKind.DealerTransKind.ServiceUnit, Short)))
        Dim objBusinessArea As ArrayList = New BusinessAreaFacade(User).Retrieve(critCol)
        If Not IsNothing(objBusinessArea) And objBusinessArea.Count > 0 Then
            Return objBusinessArea(0).Email
        End If
        Return ""
    End Function

    '--Send Email CC
    Private Function CCTo() As String
        Dim _ArlCC As ArrayList
        Dim CC As String
        Dim critCC As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCC.opAnd(New Criteria(GetType(TrUserEmail), "Tipe", MatchType.Exact, "CC"))
        _ArlCC = New TrUserEmailFacade(User).Retrieve(critCC)
        If Not IsNothing(_ArlCC) OrElse _ArlCC.Count > 0 Then
            For Each item As TrUserEmail In _ArlCC
                If CC = String.Empty Then
                    CC = item.Email
                Else
                    CC = CC & ";" & item.Email
                End If
            Next
        End If
        Return CC
    End Function

    Private Sub SetTextColumnNo(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgClassRegistration.CurrentPageIndex * dtgClassRegistration.PageSize)
    End Sub

    Private Sub SetTextColumnStatus(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Select Case CType(DataBinder.Eval(e.Item.DataItem, "Status"), String)
            Case "0"
                lblStatus.Text = GetEnumClassRegText(EnumTrClassRegistration.DataStatusType.Register)
            Case "1"
                lblStatus.Text = GetEnumClassRegText(EnumTrClassRegistration.DataStatusType.Pass)
            Case "2"
                lblStatus.Text = GetEnumClassRegText(EnumTrClassRegistration.DataStatusType.Fail)
            Case "3"
                lblStatus.Text = GetEnumClassRegText(EnumTrClassRegistration.DataStatusType.Reject)
            Case Else
                lblStatus.Text = ""
        End Select
    End Sub

    Private Function GetEnumClassRegText(ByVal index As Integer) As String
        Dim obj As New EnumTrClassRegistration
        Return obj.StatusByIndex(index)
    End Function

    Private Sub DeleteAllClassRegistration()
        Dim objTrClassRegistration As TrClassRegistration
        Dim objFacade As TrClassRegistrationFacade = New TrClassRegistrationFacade(User)
        For Each objTrClassRegistration In CType(sessionhelper.GetSession("arlRegistration"), ArrayList)
            Try
                objFacade.DeleteFromDB(objTrClassRegistration)
            Catch
            End Try
        Next
        dtgClassRegistration.Visible = False
        btnSimpan.Visible = False
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click        

        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim subject As String = "[MMKSI-DNet] Service - Penolakan Calon Peserta Training"
        Dim email As DNetMail = New DNetMail(smtp)
        Dim sb As StringBuilder = New StringBuilder
        Dim _Form As String = Request.QueryString("Form")
        Dim _arlRegistration As ArrayList = sessionhelper.GetSession("arlRegistration")
        If _arlRegistration.Count = 0 Then
            MessageBox.Show(SR.DataNotFound("Penolakan Pendaftaran"))
            Return
        End If

        Dim nDealerID As Integer = CType(_arlRegistration(0), TrClassRegistration).Dealer.ID

        Dim _TO As String = SendTo(nDealerID)
        Dim _CC As String = CCTo()
        If _TO <> String.Empty Or _CC <> String.Empty Then 'Tidak Boleh Kosong Kedua-duanya
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(nDealerID)
            Dim _Dealer As String = objDealer.DealerCode + " / " + objDealer.DealerName
            Dim _User As String = viewstate("_User")
            If _User = String.Empty Then
                MessageBox.Show("Asal Email Kosong")
                Return
            End If
            Dim _Kota As String = objDealer.City.CityName
            Dim _DateNow As DateTime = DateTime.Now
            Dim _Date As String = Format(_DateNow, "dd-MMM-yyyy hh:mm:ss")

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
                sb.Append("Hal: Penolakan calon peserta")
                sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
                sb.Append("Sehubungan dengan adanya perubahan alokasi dealer Anda, maka kami akan menghapus/menolak peserta yang telah terdaftar dengan data:")
                sb.Append("</td></tr><tr><td><br>	<table width=600px bgcolor=black cellspacing=1 border=1 cellpadding=2><tr bgcolor=white ><td align=center>")
                sb.Append("Noreg")
                sb.Append("</td><td align=center>")
                sb.Append("Nama Peserta")
                sb.Append("</td><td align=center>")
                sb.Append("Kode Kelas")
                sb.Append("</td></tr>")

                For Each item As TrClassRegistration In _arlRegistration
                    sb.Append("<tr bgcolor=white align=center><td>")
                    sb.Append(item.TrTrainee.ID)
                    sb.Append("</td><td align=left>")
                    sb.Append(item.TrTrainee.Name)
                    sb.Append("</td><td align=center>")
                    sb.Append(item.TrClass.ClassCode)
                    sb.Append("</td></tr>")
                Next

                sb.Append("</table><tr><td><br></td></tr><tr><td>")
                'sb.Append("Pendaftaran ulang dapat dilakukan setelah Anda menerima email perihal ""Pemberitahuan Perubahan Alokasi"" dari kami.")
                sb.Append("Silahkan  lakukan pendaftaran ulang sesuai dengan prasyarat kelas yang telah ditentukan.")
                sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
                sb.Append("Atas perhatian dan kerjasamanya kami ucapkan terima kasih.")
                sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
                sb.Append("</td></tr><tr><td>")
                sb.Append("Hormat kami")
                sb.Append("</td></tr><tr><td>")
                sb.Append("Training Dept.")
                sb.Append("</td></tr></table></body></table></html>")
            End If
            Dim strBody As String = sb.ToString
            Try
                email.sendMailTraining(_TO, _CC, _User, subject, Mail.MailFormat.Html, strBody)
                DeleteAllClassRegistration()
                MessageBox.Show("Proses Ubah Status Selesai dan Proses Email Berhasil dikirim ")
            Catch ex As Exception
                MessageBox.Show("Proses Ubah Status Selesai dan Proses Kirim Email Tidak Berhasil")
            End Try

        Else
            MessageBox.Show("Tujuan dan CC Email Kosong")
        End If

    End Sub

    Private Sub dtgClassRegistration_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegistration.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            SetTextColumnNo(e)
            SetTextColumnStatus(e)
            Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)

            Dim RowValue As TrClassRegistration = CType(e.Item.DataItem, TrClassRegistration)

            Dim lblClass As Label = CType(e.Item.FindControl("lblClass"), Label)

            If Not IsNothing(RowValue.TrClass) And Not IsNothing(lblClass) Then
                lblClass.Text = RowValue.TrClass.ClassCode
            End If
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("./FrmDaftarPendaftaranSales.aspx")
    End Sub

#End Region

End Class
