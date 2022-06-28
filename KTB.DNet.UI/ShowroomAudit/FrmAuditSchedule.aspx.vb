#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.ShowroomAudit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
'Imports System.IO
'Imports System.Text
'Imports System.Configuration
#End Region

Public Class FrmAuditSchedule
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAuditNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAuditSearch As System.Web.UI.WebControls.Label
    Protected WithEvents dtgAuditor As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents btnRilis As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents hdnFooterDealerID As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnFooterDealerName As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnSelectedDealer As System.Web.UI.HtmlControls.HtmlInputHidden

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
    Private arlAuditSchedule As ArrayList
    Private arlScheduleAuditor As ArrayList = New ArrayList
    Private arlScheduleDealer As ArrayList = New ArrayList
    'Private objAuditSchedule As AuditSchedule
    Private sHelper As SessionHelper = New SessionHelper

    Private Property SelectedDealerCode() As String
        Get
            Dim obj As Object = ViewState("SelectedDealerCode")
            If obj Is Nothing Or obj.Equals(String.Empty) Then
                Dim strDealerCode = String.Empty
                If txtDealerCode.Text.Trim().Length > 0 Then
                    Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
                    strDealerCode = objDealer.DealerCode
                    SelectedDealerName = objDealer.DealerName
                End If
                Return strDealerCode
            End If
            Return obj.ToString()
        End Get
        Set(ByVal Value As String)
            ViewState("SelectedDealerCode") = Value
        End Set
    End Property
    Private Property SelectedDealerName() As String
        Get
            Dim obj As Object = ViewState("SelectedDealerName")
            If obj Is Nothing Then
                Return String.Empty
            End If
            Return obj.ToString()
        End Get
        Set(ByVal Value As String)
            ViewState("SelectedDealerName") = Value
        End Set
    End Property
#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        lblAuditSearch.Attributes("onClick") = "ShowAuditParameter();"
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        Dim strDealer As String() = hdnSelectedDealer.Value.Split(",".ToCharArray())
        If strDealer.Length > 1 Then
            SelectedDealerCode = strDealer(0)
            SelectedDealerName = strDealer(1)
        ElseIf strDealer.Length > 0 Then
            SelectedDealerCode = strDealer(0)
        End If
        If Not IsNothing(Request.QueryString("id")) Then
            Select Case Request.QueryString("Mode")
                Case "Edit"
                    ViewState("Mode") = "Edit"
                Case "New"
                    ViewState("Mode") = "New"
                Case "View"
                    ViewState("Mode") = "View"
                    viewdata(Request.QueryString("id"))
                    btnCancel.Visible = False
                    btnCari.Visible = False
                    btnRilis.Visible = False
                    btnSimpan.Visible = False
                    lblSearchDealer.Visible = False
                    dtgAuditor.ShowFooter = False
                    dtgAuditor.Columns(dtgAuditor.Columns.Count - 1).Visible = False
                    txtAuditNo.Attributes.Add("readonly", "readonly")
                    txtDealerCode.Attributes.Add("readonly", "readonly")
                Case "Update"
                    ViewState("Mode") = "Update"
                    viewdata(Request.QueryString("id"))
                    btnCancel.Visible = False
                    btnCari.Visible = False
                    btnSimpan.Visible = False
                    txtAuditNo.ReadOnly = True
            End Select
        Else
            If Not IsPostBack Then
                ViewState("CurrentSortColumn") = "Dealer.DealerCode"
                ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
                lblAuditSearch.Attributes("onClick") = "ShowAuditParameter();"
                lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
                dtgAuditor.DataSource = arlScheduleAuditor
                dtgAuditor.DataBind()
            End If
        End If

        ' add security
        If Not CekScheduleListCreatePrivilege() Then
            dtgAuditor.ShowFooter = False
            btnSimpan.Enabled = False
            btnRilis.Enabled = False
        End If

    End Sub

    Private Sub dtgAuditor_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAuditor.ItemCreated
        If (e.Item.ItemType = ListItemType.Header) Then
            e.Item.SetRenderMethodDelegate(New RenderMethod(AddressOf NewRenderHeader))
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            'Dim lblFooterDealerCode As Label = CType(e.Item.FindControl("lblFooterDealerCode"), Label)
            'Dim lblFDealerName As Label = CType(e.Item.FindControl("lblFDealerName"), Label)
            'lblFooterDealerCode.Text = SelectedDealerCode
            'lblFDealerName.Text = SelectedDealerName

        End If
    End Sub

    Private Sub dtgAuditor_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgAuditor.ItemCommand
        Dim _boolErr As Boolean = False
        If Not IsNothing(sHelper.GetSession("AuditorItem")) Then
            arlScheduleAuditor = CType(sHelper.GetSession("AuditorItem"), ArrayList)
        Else
            arlScheduleAuditor = New ArrayList
        End If

        Select Case e.CommandName
            Case "add"
                If Not CheckGridData(e, e.CommandName) Then
                    Return
                End If

            Case "edit"
                ViewState("Process") = "Edit"
                btnSimpan.Enabled = False
                dtgAuditor.ShowFooter = False
                dtgAuditor.EditItemIndex = e.Item.ItemIndex

            Case "delete"
                Try
                    arlScheduleAuditor.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception

                End Try

            Case "save"
                If Not CheckGridData(e, e.CommandName) Then
                    Return
                End If
                ViewState("Process") = Nothing
                dtgAuditor.EditItemIndex = -1
                dtgAuditor.ShowFooter = True
                btnSimpan.Enabled = True

                ' add security
                If Not CekScheduleListCreatePrivilege() Then
                    btnSimpan.Enabled = False
                    dtgAuditor.ShowFooter = False
                End If

            Case "cancel"
                ViewState("Process") = Nothing
                dtgAuditor.EditItemIndex = -1
                dtgAuditor.ShowFooter = True
                btnSimpan.Enabled = True
                ' add security
                If Not CekScheduleListCreatePrivilege() Then
                    btnSimpan.Enabled = False
                    dtgAuditor.ShowFooter = False
                End If
        End Select

        sHelper.SetSession("AuditorItem", arlScheduleAuditor)
        BindtoGrid()

    End Sub

    Private Sub dtgAuditor_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAuditor.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            BindDDL(e, "ddlFAuditorType") '--bindAuditorType
            'CType(e.Item.FindControl("lblFPopUpDealer"), Label).Attributes("OnClick") = "ShowPopUpDealerOne();"
            Dim lblFooterDealerCode As Label = CType(e.Item.FindControl("lblFooterDealerCode"), Label)
            Dim lblFDealerName As Label = CType(e.Item.FindControl("lblFDealerName"), Label)
            hdnFooterDealerID.Value = lblFooterDealerCode.ClientID
            hdnFooterDealerName.Value = lblFDealerName.ClientID
            'lblFooterDealerCode.Text = SelectedDealerCode
            'lblFDealerName.Text = SelectedDealerName

        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                    New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
            End If
            CType(e.Item.FindControl("lblStartDate"), Label).Text = CType(e.Item.DataItem, AuditScheduleAuditor).StartDate.ToString("dd/MM/yyyy")
            CType(e.Item.FindControl("lblEndDate"), Label).Text = CType(e.Item.DataItem, AuditScheduleAuditor).EndDate.ToString("dd/MM/yyyy")

        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            BindDDL(e, "ddlEAuditorType") '--bindeAuditorype
            Dim ddlEAuditorType As DropDownList = CType(e.Item.FindControl("ddlEAuditorType"), DropDownList)
            Dim txtEAuditorName As TextBox = CType(e.Item.FindControl("txtEAuditorName"), TextBox)

            If ddlEAuditorType.SelectedItem.Text = EnumDealerTittle.DealerTittle.DEALER.ToString() Then
                txtEAuditorName.Enabled = False
            End If

        End If
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(0).Text = (e.Item.ItemIndex + 1 + (dtgAuditor.PageSize * dtgAuditor.CurrentPageIndex)).ToString
        End If
    End Sub

    Private Sub dtgAuditor_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgAuditor.SortCommand
        If (ViewState("Process") = "Edit") Then
            Return
        End If
        If IsNothing(sHelper.GetSession("AuditorItem")) Then
            Return
        Else
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
            BindtoGrid()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim nresult As Integer = 0

        If Not validateData() Then
            Return
        Else

            '--New
            If ViewState("Mode") = "New" Then
                Try
                    Dim _objSchedule As AuditSchedule = New AuditSchedule

                    '--Get AuditScheduleAuditor
                    arlScheduleAuditor = CType(sHelper.GetSession("AuditorItem"), ArrayList)

                    ''--Get AuditScheduledealer
                    arlScheduleDealer = New ArrayList
                    'For i As Integer = 0 To GetDealer().Count - 1
                    '    Dim _obj As AuditScheduleDealer = New AuditScheduleDealer
                    '    _obj.Dealer = CType(GetDealer(i), Dealer)
                    '    arlScheduleDealer.Add(_obj)
                    'Next

                    For Each item As AuditScheduleAuditor In arlScheduleAuditor
                        Dim _obj As AuditScheduleDealer = New AuditScheduleDealer
                        _obj.Dealer = CType(item.Dealer, Dealer)
                        arlScheduleDealer.Add(_obj)
                    Next



                    If Not IsNothing(sHelper.GetSession("SesAuditParam")) Then
                        _objSchedule.AuditParameter = CType(sHelper.GetSession("SesAuditParam"), AuditParameter)

                        nresult = New AuditScheduleFacade(User).InsertSchedule(_objSchedule, arlScheduleAuditor, arlScheduleDealer)
                        If nresult < 0 Then
                            MessageBox.Show(SR.SaveFail)
                            Return
                        Else
                            MessageBox.Show(SR.SaveSuccess)
                            btnCari_Click(Nothing, Nothing)
                        End If

                    End If

                Catch ex As Exception
                    MessageBox.Show(SR.SaveFail)
                    Return
                End Try

            ElseIf ViewState("Mode") = "Edit" Then
                Try
                    Dim _objSchedule As AuditSchedule = CType(sHelper.GetSession("SesAuditSchedule"), AuditSchedule)
                    If _objSchedule.IsRilis <> 1 Then
                        Dim arlSource As ArrayList = New ArrayList

                        '--Get AuditScheduleAuditor
                        'arlSource = CType(sHelper.GetSession("SesAuditSchedule"), AuditSchedule).AuditScheduleAuditors
                        arlScheduleAuditor = UpdateScheduleAuditor(_objSchedule.ID, CType(sHelper.GetSession("AuditorItem"), ArrayList))

                        '--Get AuditScheduledealer
                        arlSource = New ArrayList
                        arlSource = CType(sHelper.GetSession("SesAuditSchedule"), AuditSchedule).AuditScheduleDealers
                        arlScheduleDealer = UpdateScheduledealer(arlSource, GetDealer(arlScheduleAuditor))


                        If Not IsNothing(sHelper.GetSession("SesAuditParam")) Then
                            If Not (_objSchedule.AuditParameter.id = CType(sHelper.GetSession("SesAuditParam"), AuditParameter).ID) Then
                                _objSchedule.AuditParameter = CType(sHelper.GetSession("SesAuditParam"), AuditParameter)
                            End If

                            nresult = New AuditScheduleFacade(User).UpdateSchedule(_objSchedule, arlScheduleAuditor, arlScheduleDealer)
                            If nresult < 0 Then
                                MessageBox.Show(SR.UpdateFail)
                                Return
                            Else
                                MessageBox.Show(SR.UpdateSucces)
                                btnCari_Click(Nothing, Nothing)
                            End If
                        End If
                    Else
                        MessageBox.Show("Update gagal, Status Sudah Rilis")
                    End If
                Catch ex As Exception
                    MessageBox.Show(SR.UpdateFail)
                End Try
            End If

        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        lblAuditSearch.Enabled = True
        txtAuditNo.Text = String.Empty
        txtDealerCode.Text = String.Empty
        lblPeriod.Text = String.Empty
        removeAllSession()
        arlScheduleAuditor = New ArrayList
        dtgAuditor.DataSource = arlScheduleAuditor
        dtgAuditor.DataBind()
        ViewState("Mode") = "New"
        btnSimpan.Enabled = False
    End Sub

    Private Sub btnRilis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRilis.Click
        Dim nresult As Integer = 0
        If Not validateData() Then
            Return
        Else
            Try
                Dim _objSchedule As AuditSchedule = CType(sHelper.GetSession("SesAuditSchedule"), AuditSchedule)
                If _objSchedule.IsRilis <> 1 Then
                    Dim arlSource As ArrayList = New ArrayList

                    '--Get AuditScheduleAuditor
                    'arlSource = CType(sHelper.GetSession("SesAuditSchedule"), AuditSchedule).AuditScheduleAuditors
                    arlScheduleAuditor = UpdateScheduleAuditor(_objSchedule.ID, CType(sHelper.GetSession("AuditorItem"), ArrayList))

                    '--Get AuditScheduledealer
                    arlSource = New ArrayList
                    arlSource = CType(sHelper.GetSession("SesAuditSchedule"), AuditSchedule).AuditScheduleDealers

                    arlScheduleDealer = UpdateScheduledealer(arlSource, GetDealer(arlScheduleAuditor))


                    If Not IsNothing(sHelper.GetSession("SesAuditParam")) Then
                        If Not (_objSchedule.AuditParameter.ID = CType(sHelper.GetSession("SesAuditParam"), AuditParameter).ID) Then
                            _objSchedule.AuditParameter = CType(sHelper.GetSession("SesAuditParam"), AuditParameter)
                        End If
                        _objSchedule.IsRilis = 1
                        nresult = New AuditScheduleFacade(User).UpdateSchedule(_objSchedule, arlScheduleAuditor, arlScheduleDealer)
                        If nresult < 0 Then
                            MessageBox.Show(SR.UpdateFail)
                            Return
                        Else
                            MessageBox.Show(SR.UpdateSucces)
                            btnCari_Click(Nothing, Nothing)
                        End If
                    End If
                Else
                    MessageBox.Show("Gagal Rilis")
                End If
            Catch ex As Exception
                MessageBox.Show(SR.UpdateFail)
            End Try
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click

        If txtAuditNo.Text <> String.Empty Then
            Dim critAuditSchedule As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critAuditSchedule.opAnd(New Criteria(GetType(AuditSchedule), "AuditParameter.Code", MatchType.Exact, txtAuditNo.Text.Trim))

            Dim arrResult As ArrayList = New ArrayList
            arrResult = New AuditScheduleFacade(User).Retrieve(critAuditSchedule)
            If arrResult.Count > 0 Then
                Dim _objAuditSchedule As AuditSchedule = CType(arrResult(0), AuditSchedule)

                sHelper.SetSession("SesAuditSchedule", _objAuditSchedule)
                sHelper.SetSession("AuditorItem", _objAuditSchedule.AuditScheduleAuditors)

                'sHelper.SetSession("SesAuditParam", _objAuditSchedule.AuditParameter)
                'sHelper.SetSession("SesScheduleDealer", _objAuditSchedule.AuditScheduleDealers)

                '--bind AuditScheduleDealer
                bindAuditScheduleDealer(_objAuditSchedule.AuditScheduleDealers)

                '--bind AuditScheduleAuditor
                BindAuditScheduleAuditor(_objAuditSchedule.AuditScheduleAuditors)

                ViewState("Mode") = "Edit"
                sHelper.SetSession("idToEdit", _objAuditSchedule.ID & ";" & txtAuditNo.Text.Trim)
                If _objAuditSchedule.IsRilis <> 1 Then
                    btnSimpan.Enabled = True
                    btnRilis.Enabled = True
                Else
                    btnSimpan.Enabled = False
                    btnRilis.Enabled = False
                End If

                btnCancel.Enabled = True
            Else
                ViewState("Mode") = "New"
                txtDealerCode.Text = String.Empty
                dtgAuditor.DataSource = New ArrayList
                dtgAuditor.DataBind()
                removeAllSession()
                btnSimpan.Enabled = True
                btnRilis.Enabled = False
                btnCancel.Enabled = True

            End If

            ' add security
            If Not CekScheduleListCreatePrivilege() Then
                btnSimpan.Enabled = False
                btnRilis.Enabled = False
                dtgAuditor.ShowFooter = False
            End If
        Else
                MessageBox.Show("Isi Data No. Audit Dahulu ")
            End If

    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../ShowroomAudit/FrmListofuditSchedule.aspx", True)
    End Sub

#End Region

#Region " Custom method"

    Private Sub NewRenderHeader(ByVal writer As HtmlTextWriter, ByVal ctl As Control)
        Dim cell As TableCell

        Dim idx As Integer = 0

        '---row pertama
        For idx = 0 To 2
            cell = CType(ctl.Controls(idx), TableCell)
            cell.Attributes.Add("rowspan", "2")
            cell.RenderControl(writer)
        Next
        writer.Write("<TD colspan='2' align='center' Class='titleTableGeneral'>Jadwal Audit</TD>")

        writer.Write("<TD colspan='2' align='center' Class='titleTableGeneral'>Auditor</TD>")

        writer.Write("</TR>")

        dtgAuditor.HeaderStyle.AddAttributesToRender(writer)

        '--- row kedua
        writer.RenderBeginTag("TR")

        For idx = 3 To 6
            ctl.Controls(idx).RenderControl(writer)
        Next

    End Sub

    Private Sub BindDDL(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal ddlId As String)
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim ddlAuditor As DropDownList = CType(e.Item.FindControl(ddlId), DropDownList)
        Dim arlAuditorType As ArrayList = New EnumDealerTittle().RetrieveTitleForShowroomAudit(companyCode)
        ddlAuditor.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlAuditor.Attributes("onchange") = "checkSelectedAuditorType(this);"
        If arlAuditorType.Count > 0 Then
            For i As Integer = 0 To arlAuditorType.Count - 1
                ddlAuditor.Items.Insert(i + 1, New ListItem(CType(arlAuditorType(i), EnumTitle).NameTitle, CType(arlAuditorType(i), EnumTitle).ValTitle.ToString))
            Next
        End If
        If e.Item.ItemType = ListItemType.EditItem Then
            ddlAuditor.SelectedValue = CType(arlScheduleAuditor(e.Item.ItemIndex), AuditScheduleAuditor).AuditorType.ToString
        End If
    End Sub

    'Private Function GetAuditParam() As Boolean
    '    Dim objParam As AuditParameter
    '    'If IsNothing(sHelper.GetSession("SesAuditParam")) Then
    '    If txtAuditNo.Text <> String.Empty Then
    '        objParam = New AuditParameterFacade(User).Retrieve(txtAuditNo.Text)
    '        If IsNothing(objParam) Then
    '            ' sHelper.SetSession("SesAuditParam", Nothing)
    '            Return False
    '        Else
    '            'sHelper.SetSession("SesAuditParam", objParam)
    '            Return True
    '        End If
    '    End If
    '    'Else
    '    'Return True
    '    ' End If

    'End Function

    Private Function CheckGridData(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs, ByVal _state As String) As Boolean
        Dim objAuditor As AuditScheduleAuditor
        Dim _startDate As KTB.DNet.WebCC.IntiCalendar
        Dim _endDate As KTB.DNet.WebCC.IntiCalendar
        Dim _AuditorType As DropDownList
        Dim _AuditorName As TextBox
        Dim lblDealerCode As Label
        Dim lblDealerName As Label

        If _state = "add" Then
            _startDate = CType(e.Item.FindControl("icFStartDate"), KTB.DNet.WebCC.IntiCalendar)
            _endDate = CType(e.Item.FindControl("icFEndDate"), KTB.DNet.WebCC.IntiCalendar)
            _AuditorType = CType(e.Item.FindControl("ddlFAuditorType"), DropDownList)
            _AuditorName = CType(e.Item.FindControl("txtFAuditorName"), TextBox)

            lblDealerCode = CType(e.Item.FindControl("lblFooterDealerCode"), Label)
            lblDealerName = CType(e.Item.FindControl("lblFDealerName"), Label)
        Else
            _startDate = CType(e.Item.FindControl("icEStartDate"), KTB.DNet.WebCC.IntiCalendar)
            _endDate = CType(e.Item.FindControl("icEEndDate"), KTB.DNet.WebCC.IntiCalendar)
            _AuditorType = CType(e.Item.FindControl("ddlEAuditorType"), DropDownList)
            _AuditorName = CType(e.Item.FindControl("txtEAuditorName"), TextBox)

            lblDealerCode = CType(e.Item.FindControl("lblEditDealerCode"), Label)
            lblDealerName = CType(e.Item.FindControl("lblEDealerName"), Label)
        End If

        lblDealerCode.Text = SelectedDealerCode
        lblDealerName.Text = SelectedDealerName

        If Not GetAuditorDealer(SelectedDealerCode) Then
            MessageBox.Show("Dealer Tidak Ditemukan")
            Return False
        End If

        If _startDate.Value > _endDate.Value Then
            MessageBox.Show("Tanggal Mulai Tidak Boleh Melebihi Tanggal Akhir")
            Return False
        End If

        If Not (_AuditorType.SelectedIndex > 0) Then
            MessageBox.Show("Pilih Tipe Auditor Dahulu")
            Return False
        End If

        If _AuditorType.SelectedItem.Text = EnumDealerTittle.DealerTittle.DEALER.ToString() Then
            _AuditorName.Text = SelectedDealerCode
            _AuditorName.Enabled = False
        End If

        If _AuditorName.Text = String.Empty Then
            MessageBox.Show("Isi Nama Auditor")
            Return False
        End If

        If _state = "add" Then
            objAuditor = New AuditScheduleAuditor
            objAuditor.AuditorType = CInt(_AuditorType.SelectedValue)
            objAuditor.Auditor = _AuditorName.Text
            objAuditor.StartDate = _startDate.Value
            objAuditor.EndDate = _endDate.Value
            Dim arld As ArrayList = CType(sHelper.GetSession("SesAuditorDealer"), ArrayList)
            If arld.Count > 0 Then
                objAuditor.Dealer = CType(arld(0), Dealer)
            End If
            arlScheduleAuditor.Add(objAuditor)

        End If

        If _state = "save" Then
            objAuditor = CType(arlScheduleAuditor(e.Item.ItemIndex), AuditScheduleAuditor)
            objAuditor.AuditorType = CInt(_AuditorType.SelectedValue)
            objAuditor.Auditor = _AuditorName.Text
            objAuditor.StartDate = _startDate.Value
            objAuditor.EndDate = _endDate.Value
            Dim arld As ArrayList = CType(sHelper.GetSession("SesAuditorDealer"), ArrayList)
            If arld.Count > 0 Then
                objAuditor.Dealer = CType(arld(0), Dealer)
            End If
            arlScheduleAuditor(e.Item.ItemIndex) = objAuditor
        End If
        sHelper.SetSession("AuditorItem", arlScheduleAuditor)
        Return True
    End Function

    Private Function GetAuditorDealer(ByVal strDealer As String) As Boolean
        Dim arlAuditorDealer As ArrayList
        If strDealer <> String.Empty Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, strDealer))
            arlAuditorDealer = New DealerFacade(User).Retrieve(criterias)
            sHelper.SetSession("SesAuditorDealer", arlAuditorDealer)
            Return True
        Else
            sHelper.SetSession("SesAuditorDealer", Nothing)
            Return False
        End If
    End Function

    Private Sub BindtoGrid()
        If Not IsNothing(sHelper.GetSession("AuditorItem")) Then
            arlScheduleAuditor = CType(sHelper.GetSession("AuditorItem"), ArrayList)
            dtgAuditor.DataSource = CommonFunction.SortArraylist(arlScheduleAuditor, GetType(AuditScheduleAuditor), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
            dtgAuditor.DataBind()
            sHelper.SetSession("AuditorItem", arlScheduleAuditor)
        End If
    End Sub

    'Private Sub DisplayTransactionResult(ByVal _id As Integer)
    '    Dim oSchedule As AuditSchedule = New AuditScheduleFacade(User).Retrieve(_id)
    '    arlScheduleAuditor = oSchedule.AuditScheduleAuditors
    '    arlScheduleDealer = oSchedule.AuditScheduleDealers
    '    sHelper.SetSession("AuditorItem", arlScheduleAuditor)
    '    sHelper.SetSession("SesScheduleDealer", arlScheduleDealer)
    '    sHelper.SetSession("SesAuditParam", oSchedule.AuditParameter)
    '    sHelper.SetSession("SesAuditSchedule", oSchedule)
    '    dtgAuditor.DataSource = arlScheduleAuditor
    '    dtgAuditor.DataBind()
    'End Sub

    Private Sub removeAllSession()
        sHelper.RemoveSession("AuditorItem")
        sHelper.RemoveSession("SesScheduleDealer")
        sHelper.RemoveSession("SesAuditParam")
        sHelper.RemoveSession("SesAuditorDealer")
        sHelper.RemoveSession("SesAuditSchedule")

    End Sub

    'Private Function getArlScheduleDealer(ByVal objSchedule As AuditSchedule) As ArrayList
    '    Dim arrData As ArrayList
    '    Dim arrDealer As ArrayList
    '    Dim _sts As Boolean = False
    '    arlScheduleDealer = New ArrayList

    '    Dim critDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    critDealer.opAnd(New Criteria(GetType(AuditScheduleDealer), "AuditSchedule.ID", MatchType.Exact, objSchedule.ID.ToString))

    '    arrData = New AuditScheduleDealerFacade(User).Retrieve(critDealer)
    '    arrDealer = CType(sHelper.GetSession("SesScheduleDealer"), ArrayList)

    '    For Each itemdealer As Dealer In arrDealer
    '        _sts = False
    '        For Each itemUpdate As AuditScheduleDealer In arrData
    '            If (itemUpdate.Dealer.ID = itemdealer.ID) Then
    '                arlScheduleDealer.Add(itemUpdate)
    '                _sts = True
    '                Exit For
    '            End If
    '        Next
    '        If Not _sts Then
    '            Dim objNewdealer As AuditScheduleDealer = New AuditScheduleDealer
    '            objNewdealer.Dealer = itemdealer
    '            arlScheduleDealer.Add(objNewdealer)
    '        End If
    '    Next
    '    Return arlScheduleDealer
    'End Function

    Private Sub bindAuditScheduleDealer(ByVal _item As ArrayList)
        'If _item.Count > 0 Then
        '    Dim _str As String = String.Empty
        '    For Each _obj As AuditScheduleDealer In _item
        '        _str = _str & _obj.Dealer.DealerCode.Trim & ";"
        '    Next
        '    txtDealerCode.Text = _str.Remove(_str.Length - 1, 1)
        'Else
            txtDealerCode.Text = String.Empty
        'End If
    End Sub

    Private Sub BindAuditScheduleAuditor(ByVal _item As ArrayList)
        dtgAuditor.DataSource = _item
        dtgAuditor.DataBind()
    End Sub

    'Private Function isexistschedule() As String
    '    Dim critAuditSchedule As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    critAuditSchedule.opAnd(New Criteria(GetType(AuditSchedule), "AuditParameter.Code", MatchType.Exact, txtAuditNo.Text.Trim))
    '    Dim arlresult As ArrayList = New ArrayList

    '    Try
    '        arlresult = New AuditScheduleFacade(User).Retrieve(critAuditSchedule)
    '        If arlresult.Count > 0 Then
    '            Return "Exist"
    '        Else
    '            Return "Not Exist"
    '        End If
    '    Catch ex As Exception
    '        Return "Error"
    '    End Try

    'End Function

    Private Function validateData(Optional Status As String = "Rilis") As Boolean

        '--cek audit parameter
        If Not cekParam() Then
            MessageBox.Show("No Audit Tidak Ditemukan")
            Return False
        End If

        '--cek auditschedule
        If isExistAuditSchedule() Then
            MessageBox.Show("Data Sudah Ada")
            Return False
        End If

        '--cek dealer u/ auditschedule Dealer
        If Not (Status = "Rilis") Then
            If Not isDealerFound() Then
                MessageBox.Show(SR.DataNotFound("Dealer"))
                Return False
            End If
        End If
        

        '--cek auditor
        arlScheduleAuditor = CType(Session("AuditorItem"), ArrayList)
        If arlScheduleAuditor Is Nothing Or arlScheduleAuditor.Count < 1 Then
            MessageBox.Show("Belum ada data auditor yang diisi.")
            Return False
        End If

        Return True
    End Function

    'Private Sub updatedata(ByVal _id As Integer)
    '    Dim nresult As Integer = 0
    '    Dim arrTemp As ArrayList = New ArrayList
    '    Dim critAuditSchedule As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "ID", MatchType.Exact, _id))
    '    arrTemp = New AuditScheduleFacade(User).Retrieve(critAuditSchedule)
    '    Dim _objSchedule As AuditSchedule = arrTemp(0)

    '    '--get object AuditSchedule
    '    _objSchedule.AuditParameter = CType(sHelper.GetSession("SesAuditParam"), AuditParameter)

    '    '--get object AuditScheduleAuditor
    '    arlScheduleAuditor = CType(Session("AuditorItem"), ArrayList)

    '    '--get object AuditScheduleDealer
    '    Dim arlFixDealer As ArrayList = New ArrayList
    '    listtoDelete(_id, arlFixDealer)
    '    listtoinsert(_id, arlFixDealer)

    '    Try
    '        nresult = New AuditScheduleFacade(User).UpdateSchedule(_objSchedule, arlScheduleAuditor, arlFixDealer)
    '        If nresult > 0 Then
    '            MessageBox.Show(SR.UpdateSucces)
    '        Else
    '            MessageBox.Show(SR.UpdateFail)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(SR.UpdateFail)
    '    End Try

    'End Sub

    'Private Sub listtoDelete(ByVal _id As Integer, ByRef arlFixDealer As ArrayList)
    '    Dim critAuditScheduleDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "RowStatus", CType(DBRowStatus.Active, Short)))
    '    critAuditScheduleDealer.opAnd(New Criteria(GetType(AuditSchedule), "AuditParameter.ID", MatchType.Exact, _id))
    '    critAuditScheduleDealer.opAnd(New Criteria(GetType(AuditSchedule), "Dealer.DealerCode", MatchType.NotInSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))

    '    arlFixDealer = New AuditScheduleDealerFacade(User).Retrieve(critAuditScheduleDealer)

    '    If arlFixDealer.Count > 0 Then
    '        For Each _item As AuditScheduleDealer In arlFixDealer
    '            If _item.IsRilisReport <> 1 Then
    '                _item.RowStatus = CType(DBRowStatus.Deleted, Short)
    '            End If
    '        Next
    '    Else
    '        arlFixDealer = New ArrayList
    '    End If

    'End Sub

    'Private Sub listtoinsert(ByVal _id As Integer, ByRef arlFixDealer As ArrayList)

    '    Dim arltocompare As ArrayList = New ArrayList

    '    arltocompare = CType(sHelper.GetSession("SesScheduleDealer"), ArrayList)

    '    If arltocompare.Count > 0 Then
    '        For i As Integer = 0 To arltocompare.Count - 1
    '            Dim arlresult As ArrayList = New ArrayList

    '            Dim critAuditScheduleDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "RowStatus", CType(DBRowStatus.Active, Short)))
    '            critAuditScheduleDealer.opAnd(New Criteria(GetType(AuditSchedule), "AuditParameter.ID", MatchType.Exact, _id))
    '            critAuditScheduleDealer.opAnd(New Criteria(GetType(AuditSchedule), "Dealer.DealerCode", CType(arltocompare(i), Dealer).DealerCode))
    '            arlresult = New AuditScheduleDealerFacade(User).Retrieve(critAuditScheduleDealer)
    '            If arlresult.Count = 0 Then
    '                Dim _obj As AuditScheduleDealer = New AuditScheduleDealer
    '                _obj.Dealer = CType(arltocompare(i), Dealer)
    '                arlFixDealer.Add(_obj)
    '            End If
    '        Next
    '    End If

    'End Sub

    Private Sub cekTxtAuditNo()
        '--cek parameter

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(AuditSchedule), "AuditParameter.Code", MatchType.Exact, txtAuditNo.Text.Trim))

        Dim arrResult As ArrayList = New ArrayList
        arrResult = New AuditScheduleFacade(User).Retrieve(criterias)
        If arrResult.Count > 0 Then
            Dim _objAuditSchedule As AuditSchedule = CType(arrResult(0), AuditSchedule)

            sHelper.SetSession("SesAuditParam", _objAuditSchedule.AuditParameter)
            sHelper.SetSession("SesScheduleDealer", _objAuditSchedule.AuditScheduleDealers)
            sHelper.SetSession("AuditorItem", _objAuditSchedule.AuditScheduleAuditors)

            '--bind AuditScheduleDealer
            bindAuditScheduleDealer(_objAuditSchedule.AuditScheduleDealers)

            '--bind AuditScheduleAuditor
            BindAuditScheduleAuditor(_objAuditSchedule.AuditScheduleAuditors)

            ViewState("Mode") = "Edit"
            sHelper.SetSession("idToEdit", _objAuditSchedule.ID & ";" & txtAuditNo.Text.Trim)
            btnSimpan.Enabled = True
            btnRilis.Enabled = False
            btnCancel.Enabled = True
        End If
    End Sub

    Private Function cekParam() As Boolean
        Dim nResult As Boolean = False
        Try
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AuditParameter), "Code", MatchType.Exact, txtAuditNo.Text.Trim))
            criterias.opAnd(New Criteria(GetType(AuditParameter), "IsRilis", MatchType.Exact, 1))

            Dim arrResult As ArrayList = New ArrayList

            arrResult = New AuditParameterFacade(User).Retrieve(criterias)

            If arrResult.Count > 0 Then
                sHelper.SetSession("SesAuditParam", arrResult(0))
                nResult = True
            Else
                sHelper.SetSession("SesAuditParam", Nothing)
            End If
        Catch ex As Exception
            nResult = False
        End Try

        Return nResult
    End Function

    Private Function isExistAuditSchedule() As Boolean
        Dim nResult As Boolean = False
        Dim _objAuditSchedule As AuditSchedule
        Try

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AuditSchedule), "AuditParameter.Code", MatchType.Exact, txtAuditNo.Text.Trim))

            Dim arrResult As ArrayList = New ArrayList

            arrResult = New AuditScheduleFacade(User).Retrieve(criterias)

            If arrResult.Count > 0 Then
                If Not IsNothing(sHelper.GetSession("SesAuditSchedule")) Then
                    _objAuditSchedule = CType(sHelper.GetSession("SesAuditSchedule"), AuditSchedule)
                    If CType(arrResult(0), AuditSchedule).AuditParameter.ID = _objAuditSchedule.AuditParameter.ID Then
                        nResult = False
                    Else
                        nResult = True
                    End If
                Else
                    nResult = True
                End If
            Else
                nResult = False
            End If
        Catch ex As Exception
            nResult = True
        End Try
    End Function

    Private Function isDealerFound() As Boolean
        If txtDealerCode.Text = String.Empty Then
            Return False
        End If

        Try
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
            Dim arlDealer As ArrayList = New DealerFacade(User).Retrieve(criterias)

            If arlDealer.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Function GetDealer(ByVal arrlst As ArrayList) As ArrayList

        Dim strdealer As String = String.Empty

        For i As Integer = 0 To arrlst.Count - 1
            strdealer = strdealer & CType(arrlst(i), AuditScheduleAuditor).Dealer.DealerCode & ";"
        Next

        strdealer = Left(strdealer, Len(strdealer) - 1)

        Try
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, "('" & strdealer.Replace(";", "','") & "')"))
            Dim arlDealer As ArrayList = New DealerFacade(User).Retrieve(criterias)

            If arlDealer.Count > 0 Then
                Return arlDealer
            Else
                Return New ArrayList
            End If
        Catch ex As Exception
            Return New ArrayList
        End Try

    End Function

    Private Function UpdateScheduledealer(ByVal arOld As ArrayList, ByVal arNew As ArrayList) As ArrayList
        Dim arrFix As ArrayList = New ArrayList

        '--get delete auditscheduledealer
        For i As Integer = 0 To arOld.Count - 1
            Dim _exist As Boolean = False
            Dim jpos As Integer = -1
            Dim _obji As AuditScheduleDealer = arOld(i)
            For j As Integer = 0 To arNew.Count - 1
                Dim _objj As Dealer = arNew(j)
                If _obji.Dealer.ID = _objj.ID Then
                    _exist = True
                    jpos = j
                    j = arNew.Count - 1
                End If
            Next

            If Not _exist Then
                _obji.RowStatus = CType(DBRowStatus.Deleted, Short)
                arrFix.Add(_obji)
            Else
                arNew.RemoveAt(jpos)
            End If
        Next

        For j As Integer = 0 To arNew.Count - 1
            Dim _obj As AuditScheduleDealer = New AuditScheduleDealer
            _obj.Dealer = arNew(j)
            arrFix.Add(_obj)
        Next

        Return arrFix
    End Function

    Private Function UpdateScheduleAuditor(ByVal _id As Integer, ByVal arNew As ArrayList) As ArrayList
        Dim arrFix As ArrayList = New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleAuditor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(AuditScheduleAuditor), "AuditSchedule.ID", MatchType.Exact, _id))


        Dim _str As String = String.Empty
        If arNew.Count > 0 Then
            For i As Integer = 0 To arNew.Count - 1
                If CType(arNew(i), AuditScheduleAuditor).ID > 0 Then
                    _str = _str & CType(arNew(i), AuditScheduleAuditor).ID & ","
                End If
            Next
            criterias.opAnd(New Criteria(GetType(AuditScheduleAuditor), "ID", MatchType.NotInSet, _str.Remove(_str.Length - 1, 1)))
        End If
        arrFix = New AuditScheduleAuditorFacade(User).Retrieve(criterias)
        If arrFix.Count > 0 Then
            For Each _obj As AuditScheduleAuditor In arrFix
                _obj.RowStatus = CType(DBRowStatus.Deleted, Short)
                arNew.Add(_obj)
            Next
        End If

        Return arNew

        ''--get delete auditscheduledealer
        'For i As Integer = 0 To arOld.Count - 1
        '    Dim _exist As Boolean = False
        '    Dim jpos As Integer = -1
        '    Dim _obji As AuditScheduleAuditor = arOld(i)
        '    For j As Integer = 0 To arNew.Count - 1
        '        Dim _objj As AuditScheduleAuditor = arNew(j)
        '        If _obji.ID = _objj.ID Then
        '            _exist = True
        '            arrFix.Add(_objj)
        '            jpos = j
        '            j = arNew.Count - 1
        '        End If
        '    Next

        '    If Not _exist Then
        '        _obji.RowStatus = CType(DBRowStatus.Deleted, Short)
        '        arrFix.Add(_obji)
        '    Else
        '        arNew.RemoveAt(jpos)
        '    End If
        'Next

        'For j As Integer = 0 To arNew.Count - 1
        '    arrFix.Add(CType(arNew(j), AuditScheduleAuditor))
        'Next

        'Return arrFix
    End Function

    Private Sub viewdata(ByVal _id As String)
        Dim _strdealer As String = String.Empty
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "ID", MatchType.Exact, CType(_id.Trim, Integer)))

        Dim arlView As ArrayList = New AuditScheduleFacade(User).Retrieve(criterias)

        If arlView.Count > 0 Then
            Dim _obj As AuditSchedule = CType(arlView(0), AuditSchedule)

            '---Audit Schedule
            txtAuditNo.Text = _obj.AuditParameter.Code
            lblPeriod.Text = _obj.AuditParameter.Period

            '---AuditScheduleDealer
            bindAuditScheduleDealer(_obj.AuditScheduleDealers)

            '--AuditScheduleAuditor
            BindAuditScheduleAuditor(_obj.AuditScheduleAuditors)

            sHelper.SetSession("SesAuditSchedule", _obj)
            sHelper.SetSession("AuditorItem", _obj.AuditScheduleAuditors)

            If _obj.IsRilis = 1 Then
                btnRilis.Enabled = False
            Else
                btnRilis.Enabled = True
            End If
        End If

    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ScheduleListViewInput_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SHOWROOM AUDIT- Input Jadwal")
        End If
    End Sub

    Private Function CekScheduleListCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ScheduleListCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

    Private Sub txtDealerCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDealerCode.TextChanged
        Dim enteredDealerCode As String = txtDealerCode.Text.Trim()
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(enteredDealerCode)
        If Not objDealer Is Nothing Then
            SelectedDealerCode = objDealer.DealerCode
            SelectedDealerName = objDealer.DealerName
        Else
            SelectedDealerCode = String.Empty
            SelectedDealerName = String.Empty
        End If
    End Sub
End Class
