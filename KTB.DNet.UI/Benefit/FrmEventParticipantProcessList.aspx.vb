Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General

#Region "DotNet Namespace"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmEventParticipantProcessList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgTable As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRegEventNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBenefitRegNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEventName As System.Web.UI.WebControls.TextBox
    Protected WithEvents cbDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icEventDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEventDateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnTambah As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpBenefitRegNo As System.Web.UI.WebControls.Label

    Protected WithEvents arrayCheck As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnStatus As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus2 As System.Web.UI.WebControls.DropDownList

    Protected WithEvents lblDelerSession As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private sessHelper As New SessionHelper
    Private objDomain As BenefitEventHeader = New BenefitEventHeader
    Private objDomainFacade As BenefitEventHeaderFacade = New BenefitEventHeaderFacade(User)




#Region "Private Property"
    Private objDealer As Dealer
    Private objSessionHelper As New SessionHelper


    Private inputeventparticipant_privillage As Boolean
    Private Vieweventparticipant_privillage As Boolean

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        'If Not SecurityProvider.Authorize(Context.User, SR.AlertManagementListView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=BENEFIT - Participant")
        'End If


        inputeventparticipant_privillage = False
        Vieweventparticipant_privillage = False
        inputeventparticipant_privillage = SecurityProvider.Authorize(Context.User, SR.inputeventparticipant_privillage)
        Vieweventparticipant_privillage = SecurityProvider.Authorize(Context.User, SR.Vieweventparticipant_privillage)

        If Not Vieweventparticipant_privillage Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - Daftar Peserta Event")
        End If
 
    End Sub

    'Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.AlertManagementListView_Privilege)
#End Region

    Private noabjad As String = ""

    Private Sub RetrieveDealer()
        If Not objDealer Is Nothing Then
            If Not objDealer.DealerGroup Is Nothing Then 'dealer side
                lblDelerSession.Visible = True
                lblPopUpDealer.Visible = Not lblDelerSession.Visible
                txtKodeDealer.Attributes("style") = "display:none"
                lblDelerSession.Text = objDealer.DealerCode & " / " & objDealer.DealerName
                txtKodeDealer.Text = objDealer.DealerCode
            Else
                lblDelerSession.Visible = False
                lblPopUpDealer.Visible = Not lblDelerSession.Visible
                txtKodeDealer.Attributes("style") = "display:"
            End If
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        objDealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        InitiateAuthorization()
        If Not IsPostBack Then
            InitializeForm()
            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblPopUpBenefitRegNo.Attributes("onclick") = "ShowPPBenefitRegNoSelection();"

            If Not sessHelper.GetSession("SessionForDisplay") Is Nothing Then
                Dim list As New ArrayList
                list = CType(sessHelper.GetSession("SessionForDisplay"), ArrayList)
                txtKodeDealer.Text = list.Item(0).ToString
                txtBenefitRegNo.Text = list.Item(1).ToString
                txtRegEventNo.Text = list.Item(2).ToString
                txtEventName.Text = list.Item(3).ToString
                icEventDate.Text = list.Item(4).ToString
                icEventDateTo.Text = list.Item(5).ToString
                ddlStatus.SelectedValue = list.Item(6).ToString
            End If

            cbDate.Checked = False
            BindDdlStatus()
            RetrieveDealer()
            dgTable.CurrentPageIndex = 0
            GetSessionCriteria()
            GetValueFromDataBase(dgTable.CurrentPageIndex)
        End If
    End Sub


    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        ddlStatus2.Items.Clear()
        Dim _arrStatus As ArrayList = New BenefitEventEnumStatus().RetrieveStatus()
        ddlStatus.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each item As BenefitEventStatus In _arrStatus
            ddlStatus.Items.Add(New ListItem(item.NameStatus, item.ValStatus))
        Next

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Dim _arrStatusDealer As ArrayList = New BenefitEventEnumStatus().RetrieveStatusDealer()
            For Each item As BenefitEventStatus In _arrStatusDealer
                ddlStatus2.Items.Add(New ListItem(item.NameStatus, item.ValStatus))
            Next
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Dim _arrStatusKTB As ArrayList = New BenefitEventEnumStatus().RetrieveStatusKTB()
            For Each item As BenefitEventStatus In _arrStatusKTB
                ddlStatus2.Items.Add(New ListItem(item.NameStatus, item.ValStatus))
            Next
        End If
    End Sub

    Private Sub InitializeForm()

        sessHelper.RemoveSession("GridSession")
        'Dim list As ArrayList = CType(sessHelper.GetSession("DetailSession"), ArrayList)
        'sessHelper.SetSession("DetailSession", list)

    End Sub

    Private Sub SetSessionCriteria()
        Dim objEvenHeader As ArrayList = New ArrayList
        objEvenHeader.Add(txtKodeDealer.Text.Trim) '0
        objEvenHeader.Add(txtRegEventNo.Text) '1
        objEvenHeader.Add(txtEventName.Text) '2
        objEvenHeader.Add(icEventDate.Value) '3
        objEvenHeader.Add(ddlStatus.SelectedValue) '4
        objEvenHeader.Add(CType(ViewState("CurrentSortColumn"), String)) '5
        objEvenHeader.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection)) '6
        sessHelper.SetSession("SessionSearchBenefitEvent", objEvenHeader)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objEvenHeader As ArrayList = sessHelper.GetSession("SessionSearchBenefitEvent")
        If Not objEvenHeader Is Nothing Then
            txtKodeDealer.Text = objEvenHeader.Item(0)
            txtRegEventNo.Text = objEvenHeader.Item(1)
            txtEventName.Text = objEvenHeader.Item(2)
            icEventDate.Value = objEvenHeader.Item(3)
            ddlStatus.SelectedValue = objEvenHeader.Item(4)
            ViewState("CurrentSortColumn") = objEvenHeader.Item(5)
            ViewState("CurrentSortDirect") = objEvenHeader.Item(6)
            Return True
        End If
        Return False
    End Function

    Private Sub dgTable_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTable.ItemCommand
        sessHelper.SetSession("PrevPage", Request.Url.ToString())
        SetSessionCriteria()
        Select Case e.CommandName
            Case "View"
                Response.Redirect("~/Benefit/FrmInputEventParticipantList.aspx?Mode=View&id=" & CInt(e.CommandArgument))
            Case "Edit"
                Response.Redirect("~/Benefit/FrmInputEventParticipantList.aspx?Mode=Edit&id=" & CInt(e.CommandArgument))
            Case "Delete"
                Response.Redirect("~/Benefit/FrmInputEventParticipantList.aspx?Mode=Delete&id=" & CInt(e.CommandArgument))

        End Select
    End Sub



    Private Sub dgTable_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTable.ItemDataBound
        GenerateToGrid(e)
    End Sub

    Private Sub dgTable_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgTable.PageIndexChanged
        dgTable.CurrentPageIndex = e.NewPageIndex
        GetValueFromDataBase(dgTable.CurrentPageIndex)
    End Sub

    Private Sub dgTable_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgTable.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        End If

        dgTable.SelectedIndex = -1
        dgTable.CurrentPageIndex = 0
        GetValueFromDataBase(dgTable.CurrentPageIndex)
    End Sub



    Private Sub GenerateToGrid(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As BenefitEventHeader = CType(e.Item.DataItem, BenefitEventHeader)
            ' If e.Item.ItemType = ListItemType.Item Then
            If Not objDomain2 Is Nothing Then

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)).ToString

                Dim objBenefitMasterHeader As BenefitMasterHeader = objDomain2.BenefitMasterHeader
                'Dim alBenefitMasterDealers As ArrayList = objBenefitMasterHeader.BenefitMasterDealers
                'Dim idDealer As String = ""
                'For Each el As BenefitMasterDealer In alBenefitMasterDealers                    '
                '    idDealer += el.Dealer.DealerCode + " - " + el.Dealer.DealerName + "; "
                'Next
                Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
                lblKodeDealer.Text = objDomain2.Dealer.DealerCode

                Dim lblBenefitRegNo As Label = CType(e.Item.FindControl("lblBenefitRegNo"), Label)
                lblBenefitRegNo.Text = objDomain2.BenefitMasterHeader.BenefitRegNo

                Dim lblNoRegEvent As Label = CType(e.Item.FindControl("lblNoRegEvent"), Label)
                lblNoRegEvent.Text = objDomain2.EventRegNo

                Dim lblNamaEvent As Label = CType(e.Item.FindControl("lblNamaEvent"), Label)
                lblNamaEvent.Text = objDomain2.EventName

                Dim lblTanggalEvent As Label = CType(e.Item.FindControl("lblTanggalEvent"), Label)
                lblTanggalEvent.Text = objDomain2.EventDate.ToString("dd/MM/yyyy")

                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                lblStatus.Text = BenefitEventEnumStatus.GetString(objDomain2.Status)

                Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
                Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)

                'If Not lblDelerSession.Visible = True Then
                lnkbtnEdit.Visible = False
                lnkbtnDelete.Visible = False
                'End If

                If objDomain2.Status <> BenefitEventEnumStatus.Status.Baru Then
                    lnkbtnDelete.Visible = False
                Else
                    lnkbtnDelete.Visible = True
                    lnkbtnEdit.Visible = True
                End If

                If Not lblDelerSession.Visible = True Then 'jika ktb
                    lnkbtnEdit.Visible = False
                    lnkbtnDelete.Visible = False
                End If

                If Not inputeventparticipant_privillage Then
                    lnkbtnEdit.Visible = False
                    lnkbtnDelete.Visible = False
                End If

               

            End If
        End If
    End Sub



    Private Sub GetValueFromDataBase(ByVal index As Integer)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim objDealer As Dealer = Session("DEALER")

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            ' Dim strDealerIn As String = DealerFacade.GenerateDealerCodeSelection(objDealer, User)
            ' Dim strDealerIn As String = DealerFacade.GenerateDealerCodeSelection(objDealer, User)
            Dim strKodeDealerIn As String = "('" & objDealer.DealerCode & "')"
            criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
        Else
            If txtKodeDealer.Text.Trim() <> "" Then
                Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
                criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            End If
        End If

        'If txtKodeDealer.Text <> "" Then

        '    Dim value As String = ""
        '    For Each item As String In txtKodeDealer.Text.Replace(" ", "").Split(";")
        '        If Not item Is Nothing And Not item = "" Then
        '            value = value + "'" + item + "',"
        '        End If
        '    Next

        '    value = value + "'--'"

        '    Dim strSql As String = ""
        '    'strSql += " select ID from BenefitEventHeader as bmh where bmh.BenefitMasterHeaderID in "
        '    'strSql += " (select a.ID from BenefitMasterDealer a "
        '    'strSql += " left join Dealer b on a.DealerID = b.ID where "
        '    'strSql += " b.DealerCode in (" & txtKodeDealer.Text.Replace(";", ",") & "))"
        '    strSql += " select ID from BenefitEventHeader as bmh where bmh.BenefitMasterHeaderID in "
        '    strSql += " ( select distinct a.id from BenefitMasterheader a"
        '    strSql += " inner join BenefitMasterDealer b on a.ID = b.BenefitMasterHeaderID"
        '    strSql += " left join Dealer c on b.DealerID = c.ID"
        '    ' strSql += " where c.DealerCode in (" & txtKodeDealer.Text.Replace(";", ",") & ") )"
        '    strSql += " where c.DealerCode in (" & value & ") )"
        '    criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
        'End If
        If txtRegEventNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "EventRegNo", MatchType.[Partial], txtRegEventNo.Text.Trim))
        End If
        If txtKodeDealer.Text.Trim() <> "" Then
            Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
            criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
        End If
        If txtBenefitRegNo.Text.Trim() <> "" Then
            Dim strBenefitRegNo As String = txtBenefitRegNo.Text.Trim()
            criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "BenefitMasterHeader.BenefitRegNo", MatchType.Exact, strBenefitRegNo))
        End If
        If txtEventName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "EventName", MatchType.[Partial], txtEventName.Text.Trim))
        End If
        If ddlStatus.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Short)))
        End If
        'If icEventDate.Value.Year > 2000 Then
        '    Dim EventDate As New DateTime(CInt(icEventDate.Value.Year), CInt(icEventDate.Value.Month), CInt(icEventDate.Value.Day), 0, 0, 0)
        '    'criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "EventDate", MatchType.Exact, icEventDate.Value.ToString("yyyy-MM-dd")))
        '    criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "EventDate", MatchType.Exact, Format(EventDate, "yyyy-MM-dd HH:mm:ss")))
        'End If

        If cbDate.Checked = True Then
            'criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "EventDate", MatchType.Exact, Format(EventDate, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BenefitEventHeader), "EventDate", MatchType.GreaterOrEqual, icEventDate.Value))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BenefitEventHeader), "EventDate", MatchType.LesserOrEqual, icEventDateTo.Value.AddDays(1)))
        End If

        _arrList = objDomainFacade.RetrieveActiveList(criterias, index + 1, dgTable.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))


        Dim arForDisplay As New ArrayList
        arForDisplay.Add(txtKodeDealer.Text)
        arForDisplay.Add(txtBenefitRegNo.Text)
        arForDisplay.Add(txtRegEventNo.Text)
        arForDisplay.Add(txtEventName.Text)
        arForDisplay.Add(icEventDate.Text)
        arForDisplay.Add(icEventDateTo.Text)
        arForDisplay.Add(ddlStatus.SelectedValue)
        sessHelper.SetSession("SessionForDisplay", arForDisplay)

        sessHelper.SetSession("GridSession", _arrList)
        'Dim Obj As ArrayList = objDomainFacade.RetrieveActiveList
        dgTable.DataSource = _arrList
        dgTable.VirtualItemCount = totalRow
        dgTable.DataBind()

        ' To Download
        Dim _arrListToDownload As ArrayList = New ArrayList

        'Dim sortColl As SortCollection = New SortCollection
        'sortColl.Add(New Sort(GetType(BenefitEventHeader), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
        '_arrListToDownload = objDomainFacade.Retrieve(criterias, sortColl)
        _arrListToDownload = objDomainFacade.Retrieve(criterias)

        sessHelper.SetSession("FrmEventParticipantProcessList_Download", _arrListToDownload)
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        Dim dtStart As DateTime = icEventDate.Value
        Dim dtEnd As DateTime = icEventDateTo.Value
        If dtStart <= dtEnd Then
            If DateDiff(DateInterval.Day, dtStart, dtEnd) > 65 Then
                MessageBox.Show("Periode tanggal tidak boleh lebih besar dari 65 hari")
            Else
                dgTable.CurrentPageIndex = 0
                GetValueFromDataBase(dgTable.CurrentPageIndex)
            End If
        Else
            MessageBox.Show("Tanggal mulai harus lebih kecil atau sama dengan tanggal akhir")
        End If
    End Sub

    Private Sub btnTambah_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTambah.Click
        Response.Redirect("~/Benefit/FrmInputEventParticipantList.aspx?Mode=Insert")
    End Sub

    Private Sub btnStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStatus.Click
        'Start add by anh
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        Dim iChecked As Integer = 0

        For Each oDataGridItem In dgTable.Items
            chkExport = oDataGridItem.FindControl("cbAllGrid")
            If chkExport.Checked Then
                iChecked = iChecked + 1
                Exit For
            End If
        Next

        If iChecked > 0 Then
            Dim arlUpdate As ArrayList = PopulateEvents(ddlStatus2.SelectedValue)
            If arlUpdate.Count > 0 Then
                Dim n As Integer = objDomainFacade.Update(arlUpdate)
            End If
        Else
            MessageBox.Show("Tidak ada data yang dipilih. Pilih data terlebih dahulu.")
            Return
        End If


        'end add by anh

        'Start Remarks by anh

        'Dim list As New ArrayList
        'Dim listcheck As New ArrayList

        'For Each item As String In arrayCheck.Value.Replace(" ", "").Split(";")
        '    If Not item Is Nothing And Not item = "" Then
        '        listcheck.Add(item)
        '    End If
        'Next

        'If listcheck.Count < 1 Then
        '    MessageBox.Show("Check list data minimal satu")
        '    Return
        'End If



        'If Not sessHelper.GetSession("GridSession") Is Nothing Then
        '    list = CType(sessHelper.GetSession("GridSession"), ArrayList)
        'End If
        'Dim n As Integer = objDomainFacade.UpdateStatus(list, listcheck, ddlStatus2.SelectedValue)

        'End Remarks by anh

        GetValueFromDataBase(dgTable.CurrentPageIndex)

    End Sub

    Private Function PopulateEvents(ByVal iStatus As Integer) As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objBenefitEventHeaderFacade As New BenefitEventHeaderFacade(User)
        Dim iCount As Integer = 0
        Dim _statusReq As String
        Dim StrMsg As String = ""

        For Each oDataGridItem In dgTable.Items
            chkExport = oDataGridItem.FindControl("cbAllGrid")
            If chkExport.Checked Then

                Dim _id As Integer = oDataGridItem.Cells(1).Text 'CType(oDataGridItem.FindControl("lblID"), Label).Text
                Dim _statusCurrent As String = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                Dim _isAllow As Boolean = False



                Dim _beh As New KTB.DNet.Domain.BenefitEventHeader
                _beh = objBenefitEventHeaderFacade.Retrieve(_id)
                Select Case iStatus
                    Case BenefitEventEnumStatus.Status.Validasi
                        _statusReq = BenefitEventEnumStatus.GetString(BenefitEventEnumStatus.Status.Baru)
                        If _statusCurrent = BenefitEventEnumStatus.GetString(BenefitEventEnumStatus.Status.Baru) Then
                             _beh.Status = iStatus
                            _isAllow = True
                        End If
                    Case BenefitEventEnumStatus.Status.Batal_Validasi
                        _statusReq = BenefitEventEnumStatus.GetString(BenefitEventEnumStatus.Status.Validasi)
                        If _statusCurrent = BenefitEventEnumStatus.GetString(BenefitEventEnumStatus.Status.Validasi) Then
                            'If Not (_beh.EventDate.AddDays(1) >= Date.Now) Then
                            _beh.Status = BenefitEventEnumStatus.Status.Baru
                            _isAllow = True
                            'Else
                            '    _beh.ErrorMessage = "Tanggal event terlalu dekat dari hari ini."
                            '    StrMsg = _beh.EventRegNo + " ,Tanggal event terlalu dekat dari hari ini."
                            'End If
                        End If
                    Case BenefitEventEnumStatus.Status.Disetujui, BenefitEventEnumStatus.Status.Ditolak
                        _statusReq = BenefitEventEnumStatus.GetString(BenefitEventEnumStatus.Status.Validasi)
                        If _statusCurrent = BenefitEventEnumStatus.GetString(BenefitEventEnumStatus.Status.Validasi) Then
                            _beh.Status = iStatus
                            _isAllow = True
                        End If
                    Case BenefitEventEnumStatus.Status.Batal_Disetujui
                        _statusReq = BenefitEventEnumStatus.GetString(BenefitEventEnumStatus.Status.Disetujui)
                        If _statusCurrent = BenefitEventEnumStatus.GetString(BenefitEventEnumStatus.Status.Disetujui) Then
                            _beh.Status = BenefitEventEnumStatus.Status.Validasi
                            _isAllow = True
                        End If
                End Select
                If _isAllow Then
                    iCount = iCount + 1
                    oExArgs.Add(_beh)
                End If
            End If
        Next
        If iCount = 0 Then
            If StrMsg <> "" Then
                MessageBox.Show(StrMsg)
            Else
                MessageBox.Show("Tidak ada data dengan status : " + _statusReq)
            End If

        End If

        Return oExArgs
    End Function

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim arlData As ArrayList = New ArrayList
        If Not IsNothing(sessHelper.GetSession("FrmEventParticipantProcessList_Download")) Then
            arlData = CType(sessHelper.GetSession("FrmEventParticipantProcessList_Download"), ArrayList)
        Else
            MessageBox.Show("Tidak ada data yang akan di download")
            Exit Sub
        End If


        Dim sFileName As String = "Participant" & "_" & DateTime.Now.ToString("yyyyMMddhhmmss")
        Dim ParticipantData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(ParticipantData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                Dim fs As FileStream = New FileStream(ParticipantData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)

                WriteParticipantData(sw, arlData)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show(ex.Message) '"DownloadData data gagal")
        End Try

    End Sub

    Private Sub WriteParticipantData(ByRef sw As StreamWriter, ByVal arlData As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file

        If arlData.Count > 0 Then
            itemLine.Remove(0, itemLine.Length)       '-- Empty line
            itemLine.Append("Kode Dealer" & tab & ": ")
            itemLine.Append(txtKodeDealer.Text)
            sw.WriteLine(itemLine.ToString())         '-- Write to file

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Benefit Reg No" & tab & ": ")
            itemLine.Append(txtBenefitRegNo.Text)  '-- Nama dealer
            sw.WriteLine(itemLine.ToString())    '-- Write to file

            itemLine.Remove(0, itemLine.Length)   '-- Empty line
            itemLine.Append("No Reg Event" & tab & ": ")
            itemLine.Append(txtRegEventNo.Text)
            sw.WriteLine(itemLine.ToString())     '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Nama Event " & tab & ": ")
            itemLine.Append(txtEventName.Text & tab)
            sw.WriteLine(itemLine.ToString())      '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Tanggal Event " & tab & ": ")
            itemLine.Append(icEventDate.Value & " s.d " & icEventDateTo.Value & tab)
            sw.WriteLine(itemLine.ToString())      '-- Write to file

            itemLine.Remove(0, itemLine.Length)    '-- Empty line
            itemLine.Append("Status " & tab & ": ")
            itemLine.Append(ddlStatus.SelectedItem.Text & tab)  '-- 
            sw.WriteLine(itemLine.ToString())      '-- Write to file

            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            sw.WriteLine(itemLine.ToString())    '-- Write blank line

            '-- Write column header
            itemLine.Remove(0, itemLine.Length)     '-- Empty line
            itemLine.Append("No." & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Benefit Reg. No." & tab)
            itemLine.Append("No. Reg. Event" & tab)
            itemLine.Append("Nama Event" & tab)
            itemLine.Append("Tanggal Event" & tab)
            itemLine.Append("Status" & tab)

            itemLine.Append("Nama" & tab)
            itemLine.Append("KTP" & tab)
            itemLine.Append("Alamat" & tab)
            itemLine.Append("Keterangan" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim no As Integer = 0
            For Each item As BenefitEventHeader In arlData
                For Each detail As BenefitEventDetail In item.BenefitEventDetails
                    itemLine.Remove(0, itemLine.Length)  '-- Empty line
                    itemLine.Append((no + 1).ToString() & tab)
                    itemLine.Append(item.Dealer.DealerCode & tab)
                    itemLine.Append(item.BenefitMasterHeader.BenefitRegNo & tab)
                    itemLine.Append(item.EventRegNo & tab)
                    itemLine.Append(item.EventName & tab)
                    itemLine.Append(item.EventDate.ToString("dd/MM/yyyy") & tab)
                    itemLine.Append(BenefitEventEnumStatus.GetString(item.Status) & tab)

                    itemLine.Append(detail.BenefitParticipant.Nama & tab)
                    itemLine.Append(detail.BenefitParticipant.KTP & tab)
                    itemLine.Append(detail.BenefitParticipant.Alamat & tab)
                    itemLine.Append(detail.BenefitParticipant.Remarks & tab)

                    sw.WriteLine(itemLine.ToString())
                    no = no + 1
                Next
            Next

        End If



    End Sub
End Class
