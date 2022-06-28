Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Event

Public Class FrmListEventInfo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ddlEventType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlConfirmed As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnProcess As System.Web.UI.WebControls.Button
    Protected WithEvents pnlApproval As System.Web.UI.WebControls.Panel
    Protected WithEvents icDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icDateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndEventReq As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icStartEventReq As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndEventConfirm As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icStartEventConfirm As KTB.DNet.WebCC.IntiCalendar


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser.Dealer.Title = "1" Then
            isKTB = True
        Else
            isKTB = False
        End If

    End Sub

#End Region

#Region "Custom Variables Declaration"
    Private objDealer As Dealer
    Dim oDealer As Dealer
    Dim facEI As New EventInfoFacade(User)
    Private isKTB As Boolean
    Private sesHelper As SessionHelper = New SessionHelper
#End Region

#Region "Custom Methods"

    Sub BindEventType()
        Dim arlEventType As New ArrayList
        arlEventType = New [Event].EventTypeFacade(User).RetrieveActiveList()
        ddlEventType.DataTextField = "Description"
        ddlEventType.DataValueField = "ID"
        ddlEventType.DataSource = arlEventType
        ddlEventType.DataBind()
        ddlEventType.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    'Sub RenderTitle(ByVal writer As HtmlTextWriter, ByVal ctl As Control)
    '    Try
    '        Dim grd As DataGrid = ctl.Parent.Parent

    '        writer.AddAttribute("style", "color:#F7F7F7;background-color:#CC3333;font-weight:bold;")
    '        writer.RenderBeginTag("TR")
    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "3")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("<input id=""chkAllItems"" type=""checkbox"" onclick=""CheckAll('chkItemChecked',document.forms[0].chkAllItems.checked)""/>")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("colspan", "11")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Pengajuan MMKSI")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("colspan", "5")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Konfirmasi Dealer")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "3")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("")
    '        writer.RenderEndTag()

    '        writer.RenderEndTag()

    '        writer.AddAttribute("style", "color:#F7F7F7;background-color:#CC3333;font-weight:bold;")
    '        writer.RenderBeginTag("TR")
    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Kode Dealer")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Nama Dealer")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Kota")
    '        writer.RenderEndTag()


    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("No Pengajuan Event")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("No Persetujuan Event")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Jenis Event")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("colspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Jadwal Event")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Jumlah Undangan")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Area Koordinator")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Manajemen KTB/Observer")
    '        writer.RenderEndTag()

    '        'writer.AddAttribute("class", "titleTablePromo")
    '        'writer.AddAttribute("rowspan", "2")
    '        'writer.RenderBeginTag("TD")
    '        'writer.Write("Nomor Alokasi")
    '        'writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("colspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Jadwal Fix Event")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Tempat")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Jumlah Undangan")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.AddAttribute("rowspan", "2")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Komentar")
    '        writer.RenderEndTag()
    '        writer.RenderEndTag()

    '        writer.AddAttribute("style", "color:#F7F7F7;background-color:#CC3333;font-weight:bold;")
    '        writer.RenderBeginTag("TR")
    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Mulai")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Selesai")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Mulai")
    '        writer.RenderEndTag()

    '        writer.AddAttribute("class", "titleTablePromo")
    '        writer.RenderBeginTag("TD")
    '        writer.Write("Selesai")
    '        writer.RenderEndTag()
    '        writer.RenderEndTag()

    '        grd.HeaderStyle.AddAttributesToRender(writer)
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Sub NewRenderHeader(ByVal writer As HtmlTextWriter, ByVal ctl As Control)
        Dim cell As TableCell

        Dim idx As Integer = 0

        '---row pertama
        'cell(0)
        cell = CType(ctl.Controls(0), TableCell)
        cell.RowSpan = 3
        cell.MergeStyle(dtgList.HeaderStyle)
        cell.RenderControl(writer)

        writer.Write("<TD colspan='11' align='center' Class='titleTablePromo' style='color:" & System.Drawing.ColorTranslator.ToHtml(dtgList.HeaderStyle.ForeColor) & ";background-color:" & System.Drawing.ColorTranslator.ToHtml(dtgList.HeaderStyle.BackColor) & "'>Pengajuan KTB</TD>")
        writer.Write("<TD colspan='6' align='center' Class='titleTablePromo' style='color:" & System.Drawing.ColorTranslator.ToHtml(dtgList.HeaderStyle.ForeColor) & ";background-color:" & System.Drawing.ColorTranslator.ToHtml(dtgList.HeaderStyle.BackColor) & "'>Konfirmasi Dealer</TD>")

        cell = CType(ctl.Controls(18), TableCell)
        cell.RowSpan = 3
        cell.MergeStyle(dtgList.HeaderStyle)
        cell.RenderControl(writer)

        writer.Write("</TR>")

        dtgList.HeaderStyle.AddAttributesToRender(writer)


        '--- row kedua
        writer.Write("<TR>")
        dtgList.HeaderStyle.AddAttributesToRender(writer)

        For idx = 1 To 6
            cell = CType(ctl.Controls(idx), TableCell)
            cell.RowSpan = 2
            cell.RenderControl(writer)
            dtgList.HeaderStyle.AddAttributesToRender(writer)

        Next

        writer.Write("<TD colspan='2' align='center' style='color:" & System.Drawing.ColorTranslator.ToHtml(dtgList.HeaderStyle.ForeColor) & ";background-color:" & System.Drawing.ColorTranslator.ToHtml(dtgList.HeaderStyle.BackColor) & "' Class='titleTablePromo'>Jadwal Event</TD>")
        'dtgList.HeaderStyle.AddAttributesToRender(writer)

        For idx = 9 To 11
            cell = CType(ctl.Controls(idx), TableCell)
            cell.RowSpan = 2
            cell.RenderControl(writer)
            dtgList.HeaderStyle.AddAttributesToRender(writer)
        Next

        writer.Write("<TD colspan='2' align='center' style='color:" & System.Drawing.ColorTranslator.ToHtml(dtgList.HeaderStyle.ForeColor) & ";background-color:" & System.Drawing.ColorTranslator.ToHtml(dtgList.HeaderStyle.BackColor) & "' Class='titleTablePromo'>Jadwal Event</TD>")
        'dtgList.HeaderStyle.AddAttributesToRender(writer)


        For idx = 14 To 17
            cell = CType(ctl.Controls(idx), TableCell)
            cell.RowSpan = 2
            cell.RenderControl(writer)
            dtgList.HeaderStyle.AddAttributesToRender(writer)

        Next
        writer.Write("</TR>")


        ''--- row ketiga
        writer.Write("<TR>")
        For idx = 7 To 8
            ctl.Controls(idx).RenderControl(writer)
            dtgList.HeaderStyle.AddAttributesToRender(writer)

        Next
        For idx = 12 To 13
            ctl.Controls(idx).RenderControl(writer)
            dtgList.HeaderStyle.AddAttributesToRender(writer)
        Next


    End Sub

    Private Function SetCriteria() As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text.Trim()))
        Else
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            End If
        End If

        If (txtNo.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "EventRequestNo", MatchType.Exact, txtNo.Text.Trim()))
        End If

        If (ddlEventType.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "EventType.ID", MatchType.Exact, ddlEventType.SelectedValue))
        End If

        If (icDateFrom.Value <= icDateUntil.Value) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "DateStart", MatchType.GreaterOrEqual, icDateFrom.Value))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "DateEnd", MatchType.LesserOrEqual, icDateUntil.Value))
        End If

        'If (icStartEventReq.Value <= icEndEventReq.Value) Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "DateStart", MatchType.GreaterOrEqual, icStartEventReq.Value))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "DateEnd", MatchType.LesserOrEqual, icEndEventReq.Value))
        'End If

        'If (icStartEventConfirm.Value <= icEndEventConfirm.Value) Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "DateStart", MatchType.GreaterOrEqual, icStartEventConfirm.Value))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "DateEnd", MatchType.LesserOrEqual, icEndEventConfirm.Value))
        'End If
        Return criterias
    End Function

    Private Sub BindDDLApproval()
        Dim arl As ArrayList
        Dim arlNew As New ArrayList
        Dim en As New EnumEventInfo

        If isKTB Then
            ddlConfirmed.DataSource = New EnumEventInfo().RetrieveStatusKTB
        Else
            ddlConfirmed.DataSource = New EnumEventInfo().RetrieveStatusDealer
        End If

        ddlConfirmed.DataTextField = "NameStatus"
        ddlConfirmed.DataValueField = "ValStatus"
        ddlConfirmed.DataBind()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite
        criterias = SetCriteria()
        Dim arl As New ArrayList
        arl = New [Event].EventInfoFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgList.PageSize, totalRow)
        If (arl.Count > 0) Then
            'Todo session
            Session("listEventInfo") = arl
            pnlApproval.Visible = True
            dtgList.Visible = True
            dtgList.DataSource = arl
            dtgList.VirtualItemCount = totalRow
            dtgList.DataBind()
        Else
            Session("listEventInfo") = Nothing
            pnlApproval.Visible = False
            dtgList.Visible = False
            MessageBox.Show(SR.DataNotFound("Event Info"))
        End If
    End Sub

    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtKodeDealer.Text)
        arrLastState.Add(txtNo.Text)
        arrLastState.Add(icDateFrom.Value)
        arrLastState.Add(icDateUntil.Value)
        arrLastState.Add(ddlEventType.SelectedValue)
        arrLastState.Add(dtgList.CurrentPageIndex)
        'Todo session
        Session("EVENTINFOSESSIONLASTSTATE") = arrLastState
    End Sub

    Private Sub GetSessionCriteria()
        If Not Session("EVENTINFOSESSIONLASTSTATE") Is Nothing Then
            Dim arrLastState As ArrayList = Session("EVENTINFOSESSIONLASTSTATE")
            If Not arrLastState Is Nothing Then
                txtKodeDealer.Text = arrLastState.Item(0)
                txtNo.Text = arrLastState.Item(1)
                icDateFrom.Value = arrLastState.Item(2)
                icDateUntil.Value = arrLastState.Item(3)
                ddlEventType.SelectedValue = arrLastState.Item(4)
                dtgList.CurrentPageIndex = arrLastState.Item(5)
                Session("EVENTINFOSESSIONLASTSTATE") = Nothing
                BindDataGrid(dtgList.CurrentPageIndex)
            End If
        End If
    End Sub
#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        lblSearchDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        oDealer = CType(Session("DEALER"), Dealer)

        If (Not IsPostBack) Then
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                txtKodeDealer.Text = oDealer.DealerCode
                txtKodeDealer.Enabled = False
                lblSearchDealer.Visible = False
            ElseIf (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                lblSearchDealer.Visible = True
                txtKodeDealer.Enabled = True
            End If
            BindDDLApproval()
            BindEventType()
            GetSessionCriteria()
        End If
    End Sub

    Private Sub dtgList_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgList.ItemCreated
        If e.Item.ItemType = ListItemType.Header Then
            ' e.Item.SetRenderMethodDelegate(New RenderMethod(AddressOf RenderTitle))
            e.Item.SetRenderMethodDelegate(New RenderMethod(AddressOf NewRenderHeader))
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
    End Sub

    Private Sub dtgList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgList.ItemCommand
        Select Case e.CommandName
            Case "edit"
                SetSessionCriteria()
                Response.Redirect("../Event/FrmInsertEvent.aspx?id=" & e.CommandArgument & "&Mode=Edit", True)
            Case "detail"
                SetSessionCriteria()
                Response.Redirect("../Event/FrmInsertEvent.aspx?id=" & e.CommandArgument & "&Mode=View", True)
            Case "delete"
                Dim x As EventInfo = facEI.Retrieve(CInt(e.CommandArgument()))

                x.RowStatus = CShort(DBRowStatus.Deleted)
                If facEI.Update(x) > 0 Then
                    MessageBox.Show(SR.DeleteSucces)
                Else
                    MessageBox.Show(SR.DeleteFail)
                End If
                BindDataGrid(0)
        End Select
    End Sub

    Private Sub dtgList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgList.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim oInfo As EventInfo = CType(e.Item.DataItem, EventInfo)

            Dim lblStartFix As Label = CType(e.Item.FindControl("lblStartFix"), Label)
            Dim lblEndFix As Label = CType(e.Item.FindControl("lblEndFix"), Label)
            Dim btnShowComment As Button = CType(e.Item.FindControl("btnShowComment"), Button)

            If (oInfo.EventInfoStatus = EnumEventInfo.EventInfoStatus.Baru) Then
                e.Item.FindControl("lbtnDelete").Visible = True
            Else
                e.Item.FindControl("lbtnDelete").Visible = False
            End If

            btnShowComment.Attributes.Add("onclick", "showPopUp('../Event/FrmViewComment.aspx?id=" & oInfo.ID & "','',300,500,null);return false;")
            If (oInfo.ConfirmedDateStart <> "1/1/1753") Then
                lblStartFix.Text = oInfo.ConfirmedDateStart.ToString("dd/MM/yyyy")
            End If
            If (oInfo.ConfirmedDateEnd <> "1/1/1753") Then
                lblEndFix.Text = oInfo.ConfirmedDateEnd.ToString("dd/MM/yyyy")
            End If
        End If
    End Sub

    Private Sub dtgList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgList.PageIndexChanged
        dtgList.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgList.CurrentPageIndex)
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        'TO DO CHECK PROSES STATUS DENGAN PROSES PAGE
        Dim isChecked As Boolean = False
        Dim i As Integer = 0
        Dim arl As New ArrayList
        For Each item As DataGridItem In dtgList.Items
            Dim chkItemChecked As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If (chkItemChecked.Checked) Then
                isChecked = True
                Dim oEvent As New EventInfo
                oEvent = New [Event].EventInfoFacade(User).Retrieve(CInt(dtgList.DataKeys.Item(i)))
                If (ddlConfirmed.SelectedValue = EnumEventInfo.EventInfoStatus.Disetujui) Then
                    If (oEvent.EventApprovalNo.Trim() = String.Empty And oEvent.EventInfoStatus = EnumEventInfo.EventInfoStatus.Baru) Then
                        oEvent.EventInfoStatus = ddlConfirmed.SelectedValue
                        oEvent.EventApprovalNo = "request"
                        arl.Add(oEvent)
                    End If
                ElseIf (ddlConfirmed.SelectedValue = EnumEventInfo.EventInfoStatus.Ditolak) Then
                    If (oEvent.EventApprovalNo.Trim() = String.Empty And oEvent.EventInfoStatus = EnumEventInfo.EventInfoStatus.Baru) Then
                        oEvent.EventInfoStatus = ddlConfirmed.SelectedValue
                        arl.Add(oEvent)
                    End If
                End If
            End If
            i = i + 1
        Next


        If (isChecked) Then
            If (New [Event].EventInfoFacade(User).UpdateEventInfoForApproval(arl) = 1) Then
                BindDataGrid(dtgList.CurrentPageIndex)
                MessageBox.Show(SR.UpdateSucces)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            MessageBox.Show("Data Event Info belum dipilih")
        End If
    End Sub

    Private Sub dtgList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgList.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        Dim arrlst As ArrayList = New ArrayList
        arrlst = CType(Session("listEventInfo"), ArrayList)
        arrlst = CommonFunction.PageAndSortArraylist(arrlst, dtgList.CurrentPageIndex, dtgList.PageSize, GetType(EventInfo), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
        dtgList.DataSource = arrlst
        dtgList.DataBind()
    End Sub

#End Region

End Class
