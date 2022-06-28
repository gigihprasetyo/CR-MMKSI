#Region "Custom Namespace Imports"
Imports System.Text

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Utility
Imports Ktb.DNet.BusinessFacade.General

#End Region


Public Class FrmPartIncidentalSendEmail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnkirim As System.Web.UI.WebControls.Button
    Protected WithEvents lblas As System.Web.UI.WebControls.Label
    Protected WithEvents lsbUp As System.Web.UI.WebControls.ListBox
    Protected WithEvents lsbUPvisible As System.Web.UI.WebControls.ListBox
    Protected WithEvents lblerror As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPositionCC As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCC As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblJabatan As System.Web.UI.WebControls.Label

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

    Private objPartHeader As PartIncidentalHeader
    Private objPartDetail As PartIncidentalDetail
    Private arlPartDetail As ArrayList
    Private sessionhelper As New sessionhelper

#End Region

#Region "Custom Method"

    Private Sub GetHeaderID()
        Dim _id As String = Request.QueryString("Id")
        If _id <> String.Empty Then
            objPartHeader = New PartIncidentalHeaderFacade(User).Retrieve(CInt(_id))
            'Todo session
            Session("PartHeader") = objPartHeader
            'lblDealerName.Text = objPartHeader.Dealer.DealerName
            'lblDealerValue.Text = objPartHeader.Dealer.SearchTerm2
            'lblNomorValue.Text = objPartHeader.DealerMailNumber
            'lblTanggalValue.Text = Format(objPartHeader.CreatedTime, "dd-MM-yyyy")
            'lblTelpValue.Text = objPartHeader.Phone
            lblas.Text = objPartHeader.Status

            '---View State For Button Send Email
            ViewState("_DealerName") = objPartHeader.Dealer.DealerName
            ViewState("_DealerSearch2") = objPartHeader.Dealer.SearchTerm2
            ViewState("_Nomor") = objPartHeader.DealerMailNumber
            ViewState("_Tanggal") = Format(objPartHeader.CreatedTime, "dd-MM-yyyy")
            ViewState("_Telp") = objPartHeader.Phone
            ViewState("_as") = lblas.Text
            ViewState("_NoPol") = objPartHeader.PoliceNumber
            ViewState("_WO") = objPartHeader.WorkOrder
            ViewState("_PIC") = objPartHeader.PIC
            ViewState("ChassisNumber") = objPartHeader.ChassisNumber
            ViewState("VehicleType") = objPartHeader.VehicleType
            ViewState("AssemblyYear") = objPartHeader.AssemblyYear

            '--Get DealerCode From Session Dealer
            Dim _idDealer As Integer
            Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
            If Not objDealer Is Nothing Then
                _idDealer = objDealer.ID
            End If


            '--Get EmailFrom From UserInfo
            Dim criterias As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.UserInfo), "Dealer.ID", MatchType.Exact, _idDealer))
            If User.Identity.Name <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.UserInfo), "UserName", MatchType.Exact, User.Identity.Name.Substring(6)))
                Dim arlUser As ArrayList = New UserInfoFacade(User).Retrieve(criterias)
                If arlUser.Count <> 0 Then
                    viewstate("_User") = CType(arlUser(0), UserInfo).Email
                End If
            Else
                setbutton()
            End If

        End If

    End Sub

#End Region

#Region "Event Hendlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            GetHeaderID()
            BindToListBox()
            BindToDropDownList()
        End If
        CheckDropDownList()
    End Sub
    Private Sub CheckDropDownList()
        If lsbUp.SelectedIndex = -1 Then
            btnkirim.Enabled = False
        Else
            btnkirim.Enabled = True
        End If
        If ddlCC.SelectedValue <> String.Empty Then
            Dim objPartIndicentalUser As PartIncidentalUser = New PartIncidentalUserFacade(User).Retrieve(CInt(ddlCC.SelectedValue))
            Viewstate("Position") = objPartIndicentalUser.PositionCC

            viewstate("cc") = objPartIndicentalUser.Email 'ddlCC.SelectedValue.ToString
            viewstate("CCName") = ddlCC.SelectedItem.ToString
        End If


        'If ddlPositionCC.SelectedValue <> String.Empty Then
        '    Viewstate("Position") = ddlPositionCC.SelectedValue.ToString
        'End If
    End Sub

    Private Sub setbutton()
        btnkirim.Visible = False
        lblerror.Visible = True
    End Sub

    Private Sub BindToListBox()

        '-- Bind To ListBox Up
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PartIncidentalUser), "Tipe", MatchType.Exact, "TO"))
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("UserName")) Then
            sortColl.Add(New Sort(GetType(PartIncidentalUser), "UserName", Sort.SortDirection.ASC))
        Else
            sortColl = Nothing
        End If
        Dim arlUserName As ArrayList = New PartIncidentalUserFacade(User).Retrieve(criterias, sortColl)
        If arlUserName.Count > 0 Then
            For Each item As PartIncidentalUser In arlUserName
                Dim listItem As New listItem(item.UserName, item.id)
                Dim listItem1 As New listItem(item.Email, item.id)
                lsbUp.Items.Add(listItem)
                lsbUPvisible.Items.Add(listItem1)
            Next
            For Each item As listItem In lsbUp.Items
                item.Selected = True
            Next
        End If
    End Sub

    Private Sub BindToDropDownList() '--DropDownlist EmailCCTo
        Dim ArrayListForDDl As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PartIncidentalUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PartIncidentalUser), "Tipe", MatchType.Exact, "CC"))
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("UserName")) Then
            sortColl.Add(New Sort(GetType(PartIncidentalUser), "UserName", Sort.SortDirection.ASC))
        Else
            sortColl = Nothing
        End If
        ArrayListForDDl = New PartIncidentalUserFacade(User).Retrieve(criterias, sortColl)
        If ArrayListForDDl.Count > 0 Then
            For Each item As PartIncidentalUser In ArrayListForDDl
                Dim listItem As New listItem(item.UserName, item.id)
                ddlCC.Items.Add(listItem)
                viewstate("cc") = ddlCC.Items(0).Value
            Next
        End If

    End Sub
    Private Sub ChangeStatus()
        Dim ObjPartHeaderFacade As New PartIncidentalHeaderFacade(User)
        objPartHeader = Session("PartHeader")
        objPartHeader.EmailStatus = PartIncidentalStatus.PartIncidentalEmailStatusEnum.Dikirim
        ObjPartHeaderFacade.Update(objPartHeader)
        MessageBox.Show("Kirim E-mail Berhasil")
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

    Private Sub btnkirim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnkirim.Click
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim subject As String = "[MMKSI-DNet] Parts - Form Permintaan Khusus"
        Dim email As DNetMail = New DNetMail(smtp)
        Dim sb As StringBuilder = New StringBuilder

        'Dim _Dealer As String = ViewState("_DealerName")
        'Dim _DealerSearch2 As String = ViewState("_DealerSearch2")
        'Dim _Nomor As String = ViewState("_Nomor")
        'Dim _Tanggal As String = ViewState("_Tanggal")
        'Dim _Telp As String = ViewState("_Telp")
        'Dim _as As String = ViewState("_as")
        'Dim _NoPol As String = ViewState("_NoPol")
        'Dim _Wo As String = ViewState("_WO")
        'Dim _PIC As String = ViewState("_PIC")
        'Dim _User As String = viewstate("_User")
        'Dim _Position As String = Viewstate("Position")
        'Dim _CC As String = viewstate("cc")
        'Dim _CCName As String = viewstate("CCName")
        ''If Not IsNothing(ViewState("Position")) Then
        ''    _Position = ViewState("Position")
        ''End If

        ''-- Email HTML Body
        ''-- Header Email
        'sb.Append("<html><body><table width=600px cellpadding=5><tr><td colspan=3 align=center><b><font face=Verdana>")
        'sb.Append("FORM PART INCIDENTIAL")
        'sb.Append("</font></b></td></tr><tr><td colspan=3><table bgcolor=black cellspacing=1 width=600px border=0 cellpadding=3><tr bgcolor=white ><td ><table width=300px  border=0 cellpadding=3><tr><td colspan=3><b>")

        ''--Header Data Part Incidential User
        'sb.Append("Kepada Yth :")
        'sb.Append("</b></td></tr><tr><td colspan=3><b><font size=3> ")
        'sb.Append("TECHNICAL INFORMATION SEC.")
        'sb.Append("</font></b></td></tr><tr><td colspan=3><b><font size=3>")
        'sb.Append("KTB SPARE PART PULOGADUNG")
        'sb.Append("</font></b></td></tr><tr>")

        ''--Email SendTo
        'If lsbUp.SelectedIndex <> -1 Then
        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    Dim SelectedName As String = GetSelectedItem(lsbUp)
        '    Dim strName As String() = SelectedName.Split(",")
        '    For i As Integer = 0 To strName.Length - 1
        '        sb.Append("<td width=12%>")
        '        If i = 0 Then
        '            sb.Append("Up :")
        '        End If
        '        sb.Append("</td><td colspan=2>")
        '        sb.Append(strName(i))
        '        sb.Append("</td></tr>")

        '    Next
        'End If

        ''--Header Data Dealer
        'sb.Append("<tr><td>&nbsp;</td><td colspan=2></td></tr></table></td><td ><table width=300px height=170px border=0 cellpadding=3 ><tr><td colspan=3 align=center><b> ")
        'sb.Append(_Dealer)
        'sb.Append("</b></td></tr><tr><td>")
        'sb.Append("Dealer")
        'sb.Append("</td><td>")
        'sb.Append(":")
        'sb.Append("</td><td >")
        'sb.Append(_DealerSearch2)
        'sb.Append("</td></tr><tr><td>")
        'sb.Append("Nomor")
        'sb.Append("</td><td>")
        'sb.Append(":")
        'sb.Append("</td><td> ")
        'sb.Append(_Nomor)
        'sb.Append("</td></tr><tr><td>")
        'sb.Append(" Tanggal")
        'sb.Append("</td><td>")
        'sb.Append(":")
        'sb.Append("</td><td> ")
        'sb.Append(_Tanggal)
        'sb.Append("</td></tr><tr><td>")
        'sb.Append("Telp")
        'sb.Append("</td><td>")
        'sb.Append(":")
        'sb.Append("</td><td> ")
        'sb.Append(_Telp)

        ''--Body 
        'sb.Append(" </td></tr></table></td></tr></table></td></tr><tr><td colspan=3><br>")
        'sb.Append(" Harap dicatat dan untuk selanjutnya diproses atas nama kami untuk perbaikan kendaraan")
        'sb.Append(" </td></tr><tr><td colspan=3>")
        'sb.Append(" Dengan No. Pol : ")
        'sb.Append(" <b> ")
        'sb.Append(_NoPol)
        'sb.Append("</b> ")
        'sb.Append("di workshop kami dengan W/O No. <b> ")
        'sb.Append(_Wo)
        'sb.Append(" </b></td></tr><tr><td colspan=3>")
        'sb.Append("Sebagai :")
        'sb.Append(" <b>")
        'sb.Append(_as)

        ''--Header grid
        'sb.Append("</b></td></tr><tr><td colspan=3><br><table bgcolor=black cellspacing=1 border=1 cellpadding=3><tr bgcolor=white><td width=3%><div align=center><b>")
        'sb.Append("No")
        'sb.Append("</b></div></td><td width=18%><div align=center><b>")
        'sb.Append("Nomor Part")
        'sb.Append("</b></div></td><td width=31%><div align=center><b>")
        'sb.Append("Nama Part")
        'sb.Append("</b></div></td><td width=10%><div align=center><b> ")
        'sb.Append("Model")
        'sb.Append("</b></div></td><td width=7%><div align=center><b> ")
        'sb.Append("Jumlah")
        'sb.Append("</b></div></td><td width=32%><div align=center><b> ")
        'sb.Append("Keterangan (Diisi Oleh KTB)")

        ''--Body Grid
        'Dim _i As Integer = 0
        'Dim _idHeader As String = Request.QueryString("Id")
        'If _idHeader <> String.Empty Then
        '    objPartHeader = New PartIncidentalHeaderFacade(User).Retrieve(CInt(_idHeader))
        '    For Each item As PartIncidentalDetail In objPartHeader.PartIncidentalDetails
        '        _i = _i + 1
        '        Dim _PartNumber As String = item.SparePartMaster.PartNumber
        '        Dim _PartName As String = item.SparePartMaster.PartName
        '        Dim _Model As String = item.SparePartMaster.ModelCode
        '        Dim _Qty As Integer = item.Quantity
        '        sb.Append("</b></div></td></tr><tr bgcolor=white><td>")
        '        sb.Append(_i)
        '        sb.Append("</td><td> ")
        '        sb.Append(_PartNumber)
        '        sb.Append("</td><td> ")
        '        sb.Append(_PartName)
        '        sb.Append("</td><td> ")
        '        sb.Append(_Model)
        '        sb.Append("</td><td align= right> ")
        '        sb.Append(_Qty)
        '        sb.Append("</td><td>&nbsp;</td></tr>")
        '    Next
        '    sb.Append("</table></td></tr><tr><td colspan=3>")
        '    sb.Append("Demikian atas kerjasama dan bantuannya diucapkan terima kasih")
        'End If

        ''--Author
        'sb.Append("</td></tr><tr><td></td><td><table bgcolor=black cellspacing=1 width=170px align=right border=0 cellpadding=3><tr bgcolor=white ><td align=center><b>")
        'sb.Append("Hormat Kami ")
        'sb.Append("</b></td></tr><tr bgcolor=white><td align=center><b><br><br><br> ")
        'sb.Append(_PIC)

        ''--CC Email
        'sb.Append("</b></td></tr></table></td></tr><tr>")
        ''If lsbCC.SelectedIndex <> -1 Then
        ''    Dim criterias As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.PartIncidentalUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ''    Dim SelectedName As String = GetSelectedItem(lsbCC)
        ''    Dim strName As String() = selectedName.Split(",")
        ''    For i As Integer = 0 To strName.Length - 1
        'sb.Append("<td width=3%>")
        ''        If i = 0 Then
        'sb.Append("CC :")
        'sb.Append("</td><td>")
        'sb.Append(_CCName)
        ''        End If
        ''        sb.Append("</td><td>")
        ''        sb.Append(strName(i))
        ''        sb.Append("</td></tr>")
        ''    Next

        ''End If
        'sb.Append("</td></tr><tr><td>&nbsp;</td><td colspan=2><b>")
        'sb.Append(_Position)
        'sb.Append("</b></td></tr></table></body></html>")
        CheckDropDownList()
        Dim _CC As String = ViewState("cc")

        Dim _Dealer As String = ViewState("_DealerName")
        Dim _DealerSearch2 As String = ViewState("_DealerSearch2")
        Dim _Nomor As String = ViewState("_Nomor")
        Dim _Tanggal As String = ViewState("_Tanggal")
        Dim _Telp As String = ViewState("_Telp")
        Dim _as As String = ViewState("_as")
        Dim _NoPol As String = ViewState("_NoPol")
        Dim _Wo As String = ViewState("_WO")
        Dim _PIC As String = ViewState("_PIC")
        Dim _User As String = ViewState("_User")
        'Dim _Position As String = Viewstate("_Position")
        Dim _Position As String = ViewState("Position")
        'Dim _CC As String = viewstate("cc")
        Dim _CCName As String = ViewState("CCName")
        Dim _chassisNumber As String = ViewState("ChassisNumber")
        Dim _vehicleType As String = ViewState("VehicleType")
        Dim _assemblyYear As String = ViewState("AssemblyYear")

        '-- HTML Body
        '-- Header
        sb.Append("<html><body><table width=500px cellpadding=5><tr><td colspan=3 align=center><b><font face=Verdana>")
        sb.Append("FORM PERMINTAAN KHUSUS")
        sb.Append("</font></b></td></tr><tr><td colspan=3></td></tr><tr><td colspan=3><table bgcolor=black cellspacing=1 width=600px border=0 cellpadding=0><tr bgcolor=white ><td ><table width=300px  border=0 cellpadding=3><tr><td colspan=3><b>")

        '--Header Data Part Incidential User
        sb.Append("Kepada Yth :")
        sb.Append("</b></td></tr><tr><td colspan=3><b><font size=3> ")
        sb.Append("TECHNICAL INFORMATION SEC.")
        sb.Append("</font></b></td></tr><tr><td colspan=3><b><font size=3>")
        sb.Append("MMKSI SPARE PART CIBITUNG")
        sb.Append("</font></b></td></tr><tr>")

        '--Email SendTo
        If lsbUp.SelectedIndex <> -1 Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SelectedName As String = GetSelectedItem(lsbUp)
            Dim strName As String() = SelectedName.Split(",")
            For i As Integer = 0 To strName.Length - 1
                sb.Append("<td width=12%><font size=3><b>")
                If i = 0 Then
                    sb.Append("Up :")
                End If
                sb.Append("</b></font></td><td colspan=2><font size=3><b><u>")
                sb.Append(strName(i))
                sb.Append("</font></u></b></td></tr>")
            Next
        End If

        '--Header Data Dealer
        sb.Append("<tr><td>&nbsp;</td><td colspan=2></td></tr></table></td><td ><table width=300px height=170px border=0 cellspacing = 0 cellpadding=3 ><tr><td colspan=3 align=center style=""border-left: black 1px solid;""><b> ")
        sb.Append(_Dealer)
        sb.Append("</b></td></tr><tr><td style=""border-top: black 1px solid; border-left: black 1px solid;"">")
        sb.Append("Dealer")
        sb.Append("</td><td style=""border-top: black 1px solid; border-left: black 1px solid;"">")
        sb.Append(_DealerSearch2)
        sb.Append("</td></tr><tr><td style=""border-top: black 1px solid; border-left: black 1px solid;"">")
        sb.Append("Nomor")
        sb.Append("</td><td style=""border-top: black 1px solid; border-left: black 1px solid;"">")
        sb.Append(_Nomor)
        sb.Append("</td></tr><tr><td style=""border-top: black 1px solid; border-left: black 1px solid;"">")
        sb.Append(" Tanggal")
        sb.Append("</td><td style=""border-top: black 1px solid; border-left: black 1px solid;"">")
        sb.Append(_Tanggal)
        sb.Append("</td></tr><tr><td style=""border-top: black 1px solid; border-left: black 1px solid;"">")
        sb.Append("Telp")
        sb.Append("</td><td style=""border-top: black 1px solid; border-left: black 1px solid;"">")
        If _Telp.Trim = "" Then
            sb.Append("-")
        Else
            sb.Append(_Telp)
        End If


        '--Body 
        sb.Append(" </td></tr></table></td></tr></table></td></tr><tr><td colspan=3><br>")
        'sb.Append(" Harap dicatat dan untuk selanjutnya diproses atas nama kami untuk perbaikan kendaraan")
        sb.Append(" Harap dicatat dan untuk selanjutnya diproses atas nama kami untuk perbaikan kendaraan")
        sb.Append(" </td></tr>")  '<tr><td colspan=3>")
        'sb.Append(" Dengan No. Pol : ")
        'sb.Append(" di workshop kami dengan W/O No. ")
        'sb.Append("<table border=1 cellspacing = 0 cellpadding=3 >")
        'sb.Append(" <b> ")
        'sb.Append(_NoPol)
        'sb.Append("</b> ")
        'sb.Append("di workshop kami dengan W/O No. <b> ")
        'sb.Append(_Wo)
        'sb.Append(" </b></table></td></tr>

        sb.Append(" <tr><table><tr><td style=""width: 250px"">di workshop kami dengan W/O No. </td>")
        sb.Append(" <td style=""width: 100px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;""> ")
        sb.Append(_Wo)
        sb.Append("</td></tr></table></tr>")


        sb.Append("<tr><td colspan=3>")
        'sb.Append("Sebagai :")
        'sb.Append(" <b>")
        'sb.Append(_as)

        '--Header grid
        sb.Append("</b></td></tr><tr><td colspan=3><br><table bgcolor=white cellspacing=0 border=0 cellpadding=3 style=""border-top: black 1px solid; border-bottom: black 1px solid; border-left: black 1px solid; border-right: black 1px solid;""><tr bgcolor=white><td width=3%  style=""border-bottom: black 1px solid; border-right: black 1px solid;""><div align=center><b>")
        sb.Append("No")
        sb.Append("</b></div></td><td width=12% style=""border-bottom: black 1px solid; border-right: black 1px solid;""><div align=center><b>")
        sb.Append("Part No")
        sb.Append("</b></div></td><td width=25% style=""border-bottom: black 1px solid; border-right: black 1px solid;""><div align=center><b>")
        sb.Append("Part Name")
        sb.Append("</b></div></td><td width=10% style=""border-bottom: black 1px solid; border-right: black 1px solid;""><div align=center><b> ")
        sb.Append("Model")
        sb.Append("</b></div></td><td width=7% style=""border-bottom: black 1px solid; border-right: black 1px solid;""><div align=center><b> ")
        sb.Append("QTY")
        sb.Append("</b></div></td><td width=10% style=""border-bottom: black 1px solid;""><div align=center><b> ")
        'sb.Append("No.Chasis")
        'sb.Append("</b></div></td><td width=10%><div align=center><b> ")
        'sb.Append("Tahun Perakitan")
        'sb.Append("</b></div></td><td width=23%><div align=center><b> ")
        sb.Append("Keterangan (Diisi Oleh MMKSI)")

        '--Body Grid
        Dim _i As Integer = 0
        Dim _idHeader As String = Request.QueryString("Id")
        If _idHeader <> String.Empty Then
            objPartHeader = New PartIncidentalHeaderFacade(User).Retrieve(CInt(_idHeader))
            For Each item As PartIncidentalDetail In objPartHeader.PartIncidentalDetails
                _i = _i + 1
                Dim _PartNumber As String = item.SparePartMaster.PartNumber
                Dim _PartName As String = item.SparePartMaster.PartName
                Dim _Model As String = item.SparePartMaster.ModelCode
                Dim _Qty As Integer = item.Quantity
                'Dim _chassisNumber As String = item.ChassisNumber
                'Dim _assemblyYear As String = item.AssemblyYear
                Dim _keterangan As String = item.Remark
                sb.Append("</b></div></td></tr><tr bgcolor=white><td  style=""border-right: black 1px solid;"">")
                sb.Append(_i) 'No
                sb.Append("</td><td  style=""border-right: black 1px solid;""> ")
                sb.Append(_PartNumber)
                sb.Append("</td><td  style=""border-right: black 1px solid;""> ")
                sb.Append(_PartName)
                sb.Append("</td><td  style=""border-right: black 1px solid;""> ")
                sb.Append(_Model)
                sb.Append("</td><td align= right  style=""border-right: black 1px solid;""> ")
                sb.Append(_Qty)
                sb.Append("</td><td> ")
                'by heru CR
                'sb.Append(_chassisNumber)
                'sb.Append("</td><td> ")
                'sb.Append(_assemblyYear)
                'sb.Append("</td><td> ")
                sb.Append(_keterangan)
                'sb.Append("</td><td>&nbsp;</td></tr>")
            Next
            sb.Append("</table></td></tr>")

            sb.Append("<tr><td colspan=3><br>No rangka : ")
            sb.Append(_chassisNumber)
            sb.Append("</td></tr><br><tr><td colspan=3><br>Tipe : ")
            sb.Append(_vehicleType)
            sb.Append("</td></tr><br><tr><td colspan=3><br>Tahun Produksi : ")
            sb.Append(_assemblyYear)
            sb.Append("</td></tr><br>")

            sb.Append("<tr><td colspan=3><br>")
            sb.Append("Demikian atas kerjasama dan bantuannya diucapkan terima kasih")
        End If

        '--Author
        sb.Append("</td></tr><tr><td></td><td><table bgcolor=white cellspacing=1 width=170px align=right border=0 cellpadding=3><tr bgcolor=white ><td align=center><b>")
        sb.Append("Hormat Kami ")
        sb.Append("</b></td></tr><tr bgcolor=white><td align=center><b><br><br><br> ")
        'sb.Append(_PIC)
        sb.Append("<b>Manager Spare Part</b>")


        '--CC Email
        sb.Append("</b></td></tr></table></td></tr><br><br><br><br><br><br><br><br><br><tr>")
        sb.Append("<td width=3%>")
        sb.Append("CC : ")
        sb.Append("</td><td>")
        sb.Append(_CCName)
        sb.Append("</td></tr><br><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td colspan=2><b>")
        sb.Append(_Position)
        sb.Append("</b></td></tr></table></body></html>")

        Dim strBody As String = sb.ToString
        Try
            Dim strEmailTo As String = GetSelectedEmail(lsbUp, lsbUPvisible)
            email.sendMail(strEmailTo, _CC, _User, subject, Mail.MailFormat.Html, strBody)
            ChangeStatus()
        Catch ex As Exception
            MessageBox.Show("Proses Kirim Email Tidak Berhasil")
        End Try
    End Sub

    Private Sub ddlCC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCC.SelectedIndexChanged
        Dim CC As String = ddlCC.SelectedItem.Text
        viewstate("cc") = CC
    End Sub

#End Region

End Class
