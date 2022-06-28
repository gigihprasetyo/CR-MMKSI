Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.enumMode
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.LKPP

'Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Web.UI.WebControls

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
#End Region

Public Class FrmLKPPHeader
    Inherits System.Web.UI.Page

#Region "PrivateVariables"
    Private _LKPPHeaderFacade As New LKPPHeaderFacade(User)
    Private _create As Boolean
    'Private _edit As Boolean
    'Private _view As Boolean
    'Private _download As 
    Private _edit As Boolean = True
    Private _view As Boolean = True
    Private _download As Boolean = True
    Private sessHelper As New SessionHelper
    Private isDealer As Boolean = False
    Private DMSDealer As Integer = 0
    Private DMSGoLive As Boolean = False
#End Region

#Region "EventHandler"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        TemporaryPrivilageChecker()
        If Not IsPostBack Then

            If Request.QueryString("Mode") = "New" Then
                Session("SESSIONSEARCHLKPP") = Nothing
            End If

            Initialize()
            'Dim criterias As New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'CreateCriterias(criterias)
            'sessHelper.SetSession("CRITERIAS", criterias)
            'BindDataGrid(0)
            If GetSessionCriteria() Then
                CreateCriterias()
                BindDataGrid(dgLKPPHeader.CurrentPageIndex)
            End If
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'CreateCriterias(criterias)
        'sessHelper.SetSession("CRITERIAS", criterias)
        If ddlStatus.SelectedValue > -2 Then
            CreateCriterias()
            dgLKPPHeader.CurrentPageIndex = 0
            BindDataGrid(dgLKPPHeader.CurrentPageIndex)
        End If
    End Sub

    Protected Sub btnDownLoad_Click(sender As Object, e As EventArgs) Handles btnDownLoad.Click
        '-- Download data in datagrid to text file
        '-- Generate random number [0..9999]
        Dim sSuffix As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        '-- Temp file must be a randomly named text file!
        Dim InvoiceData As String = Server.MapPath("") & "\..\DataTemp\LKPP_list_" & sSuffix & ".xls"
        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Try
            If imp.Start() Then
                Dim finfo As FileInfo = New FileInfo(InvoiceData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If
                '-- Create file stream
                Dim fs As FileStream = New FileStream(InvoiceData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)
                '-- Write data to temp file
                DownloadLKPP(sw)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
            '-- Download data to client!
            Response.Redirect("../downloadLocal.aspx?file=DataTemp\LKPP_list_" & sSuffix & ".xls")
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub dgLKPPHeader_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgLKPPHeader.ItemCommand
        If e.CommandName = "View" Then
            SetSessionCriteria()
            sessHelper.SetSession("Status", "View")
            sessHelper.SetSession("IDLKPPHeader", CInt(e.Item.Cells(0).Text))
            Response.Redirect("FrmLKPPDetail.aspx?id=" & e.Item.Cells(0).Text & "&mode=View")
        ElseIf e.CommandName = "Edit" Then
            SetSessionCriteria()
            If isDealer = True Then
                sessHelper.SetSession("Status", "Update")
                sessHelper.SetSession("IDLKPPHeader", CInt(e.Item.Cells(0).Text))
                Response.Redirect("FrmLKPPDetail.aspx?id=" & e.Item.Cells(0).Text & "&mode=Update")
            Else
                SetSessionCriteria()
                sessHelper.SetSession("Status", "View")
                sessHelper.SetSession("IDLKPPHeader", CInt(e.Item.Cells(0).Text))
                Response.Redirect("FrmLKPPDetail.aspx?id=" & e.Item.Cells(0).Text)

            End If
        ElseIf e.CommandName = "Delete" Then
            If IsValidToDelete(CInt(e.Item.Cells(0).Text)) Then
                Dim ObjLKPPHeader As LKPPHeader = _LKPPHeaderFacade.Retrieve(CInt(e.Item.Cells(0).Text))
                Try
                    _LKPPHeaderFacade.Delete(ObjLKPPHeader)
                    'Initialize()
                    'BindDataGrid(0)
                    'If GetSessionCriteria() Then
                    '    CreateCriterias()
                    '    BindDataGrid(dgLKPPHeader.CurrentPageIndex)
                    'End If
                    MessageBox.Show(SR.DeleteSucces)
                    btnSearch_Click(Me, Nothing)

                Catch ex As Exception
                    MessageBox.Show(SR.DeleteFail)
                End Try
            Else
                MessageBox.Show(SR.CannotDelete())
            End If
        ElseIf e.CommandName = "Download" Then
            Dim file As String = e.Item.Cells(8).Text
            Dim fInfo As New System.IO.FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("SPLAttachment") & "LKPP\" & file)
            Try
                Response.Redirect("../Download.aspx?file=" & file)
            Catch ex As Exception
                MessageBox.Show(SR.DownloadFail(file))
            End Try
        End If
    End Sub

    Private Sub dgLKPPHeader_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgLKPPHeader.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objLKPP As LKPPHeader = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgLKPPHeader.CurrentPageIndex * dgLKPPHeader.PageSize)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim strDealerCode = String.Empty
            If objLKPP.LKPPDealers.Count > 0 Then
                For Each objDealer As LKPPDealer In objLKPP.LKPPDealers
                    If Not IsNothing(objDealer.Dealer) Then
                        strDealerCode = objDealer.Dealer.DealerCode + IIf(strDealerCode = "", "", ";" + strDealerCode)
                    End If
                Next
            End If
            lblDealerCode.Text = strDealerCode

            Dim lblLetterDate As Label = CType(e.Item.FindControl("lblLetterDate"), Label)
            lblLetterDate.Text = objLKPP.LetterDate.ToString("dd/MM/yyyy")

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus0"), Label)
            Dim lblStatusUnit As Label = CType(e.Item.FindControl("lblStatus1"), Label)
            Dim isRemain As Boolean = False
            If objLKPP.LKPPDetails.Count > 0 Then
                For Each objDet As LKPPDetail In objLKPP.LKPPDetails
                    If objDet.UnitRemain > 0 Then
                        isRemain = True
                        Exit For
                    End If
                Next
            End If

            Dim oDealer As Dealer = Session.Item("DEALER")
            Dim lbtnVw As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lbtnEd As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDel As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lbtnDown As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            lbtnVw.Visible = CBool(ViewState("LKPP_List_Privilege"))

            If DMSGoLive AndAlso DMSDealer <> 1 Then
                lbtnEd.Visible = CBool(ViewState("LKPP_Edit_Privilege"))
            End If

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            End If


            If isDealer = True Then
                lbtnDel.Visible = False
                lbtnEd.Visible = False
                Select Case objLKPP.Status

                    Case 0
                        lblStatus.Text = EnumStatusLKPP.StatusLKPP.Baru.ToString()
                        lbtnDel.Visible = CBool(ViewState("LKPP_Edit_Privilege")) '_edit
                        lbtnEd.Visible = CBool(ViewState("LKPP_Edit_Privilege")) '_edit
                    Case 1
                        lblStatus.Text = EnumStatusLKPP.StatusLKPP.Validasi.ToString()
                    Case 2
                        lblStatus.Text = EnumStatusLKPP.StatusLKPP.Setuju.ToString()
                    Case 3
                        lblStatus.Text = EnumStatusLKPP.StatusLKPP.Tolak.ToString()
                End Select
            Else
                lbtnDel.Visible = False
                lbtnEd.Visible = CBool(ViewState("LKPP_Edit_Privilege")) '_edit
                Select Case objLKPP.Status
                    Case 0
                        lblStatus.Text = EnumStatusLKPP.StatusLKPP.Baru.ToString()
                        lbtnEd.Visible = False
                    Case 1
                        lblStatus.Text = EnumStatusLKPP.StatusLKPP.Validasi.ToString()
                    Case 2
                        lblStatus.Text = EnumStatusLKPP.StatusLKPP.Setuju.ToString()
                    Case 3
                        lblStatus.Text = EnumStatusLKPP.StatusLKPP.Tolak.ToString()
                End Select
            End If

            If isRemain = False Then
                lblStatusUnit.Text = "Selesai"
            End If
            If objLKPP.LKPPDetails.Count < 1 Then
                lblStatusUnit.Text = ""
            End If

            If Not CType(e.Item.DataItem, LKPPHeader).Attachment = "" Then
                lbtnDown.Visible = True
            Else
                lbtnDown.Visible = False
            End If

            Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(oDealer.DealerCode)
            If Not dealerSystems.SystemID = 1 Then
                lbtnEd.Visible = False
            End If
        End If

    End Sub

    Private Sub dgLKPPHeader_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgLKPPHeader.PageIndexChanged
        dgLKPPHeader.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgLKPPHeader.CurrentPageIndex)
    End Sub

    Private Sub dgLKPPHeader_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgLKPPHeader.SortCommand
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
        dgLKPPHeader.SelectedIndex = -1
        dgLKPPHeader.CurrentPageIndex = 0
        BindDataGrid(dgLKPPHeader.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"

    Private Sub CheckPrivilege()
        Dim objDealer As Dealer = Session.Item("DEALER")
        Dim B_LKPP_List_Privilege As Boolean = SecurityProvider.Authorize(Context.User, SR.LKPP_List_Privilege)
        If Not B_LKPP_List_Privilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Daftar LKPP")
        End If

        _create = SecurityProvider.Authorize(Context.User, SR.LKPP_Input_Privilege)
        _edit = SecurityProvider.Authorize(Context.User, SR.LKPP_Edit_Privilege)
        _download = B_LKPP_List_Privilege
        _view = B_LKPP_List_Privilege
        ViewState("LKPP_List_Privilege") = B_LKPP_List_Privilege
        ViewState("LKPP_Edit_Privilege") = _edit
        btnSearch.Visible = _view
        btnProses.Visible = CBool(ViewState("LKPP_Edit_Privilege"))


        Dim objAppConfig As AppConfig = New AppConfigFacade(User).Retrieve("LKPPDMSGoLive")
        Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)
        DMSGoLive = CBool(objAppConfig.Value)
        DMSDealer = dealerSystems.SystemID

        If DMSGoLive AndAlso DMSDealer <> 1 Then
            btnProses.Visible = False
        End If
    End Sub

    Private Sub Initialize()
        txtLKPPNumber.Text = ""
        txtDealerCode.Text = ""
        txtCustName.Text = ""
        dealerTextBox(isDealer, txtDealerCode)
        bindActions(isDealer)
        bindStatus(isDealer)
        If isDealer = True And ddlStatus.SelectedValue <> 0 Then
            Dim dl As Integer = ddlAction.Items.Count
            ddlAction.Items.RemoveAt(dl - 1)
        End If

        icStartDate.Value = Date.Now.AddMonths(-1)
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    '
    Private Sub TemporaryPrivilageChecker()
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            txtDealerCode.Text = objDealer.DealerCode
            lblSearchDealer.Visible = False
            isDealer = True
        Else
            lblSearchDealer.Visible = True
            isDealer = False
        End If

    End Sub
    Private Sub dealerTextBox(ByVal isdealer As Boolean, ByVal tx As TextBox)
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If isdealer = True Then
            txtDealerCode.Text = objDealer.DealerCode
            tx.Attributes.Add("readonly", "readonly")
            tx.BackColor = Color.Transparent
            tx.BorderStyle = BorderStyle.None
        Else
            tx.Attributes.Remove("readonly")
            tx.BorderStyle = BorderStyle.NotSet
            tx.BackColor = Color.White
        End If
    End Sub

    Private Sub bindStatus(ByVal isdealer As Boolean)
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = EnumStatusLKPP.RetrieveStatusForFilter(isdealer)
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataBind()
    End Sub

    Private Sub bindActions(ByVal isdealer As Boolean)
        ddlAction.Items.Clear()
        ddlAction.DataSource = EnumStatusLKPP.RetrieveStatusForAction(isdealer)
        ddlAction.DataValueField = "ValStatus"
        ddlAction.DataTextField = "NameStatus"
        ddlAction.DataBind()
    End Sub

    Private Sub bindRules(ByVal dg As DataGrid)
        Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)
        If dg.Items.Count > 0 Then
            For Each item As DataGridItem In dg.Items
                Dim lkpp As New LKPPHeader
                Dim chk As CheckBox
                Dim notes As TextBox
                lkpp.ID = CType(item.FindControl("lblID"), Label).Text
                chk = item.FindControl("ChkExport")
                notes = CType(item.FindControl("txtNotes"), TextBox)
                lkpp = objLKPPHeaderFacade.Retrieve(lkpp.ID)
                If isDealer = True Then
                    If lkpp.Status = EnumStatusLKPP.StatusLKPP.Setuju Or lkpp.Status = EnumStatusLKPP.StatusLKPP.Tolak Then
                        chk.Checked = False
                        chk.Enabled = False
                    End If
                Else
                    If lkpp.Status = EnumStatusLKPP.StatusLKPP.Setuju Or lkpp.Status = EnumStatusLKPP.StatusLKPP.Tolak Then
                        chk.Enabled = True
                    End If
                End If
                dealerTextBox(isDealer, notes)
            Next

        End If
    End Sub

    Private Sub CreateCriterias()
        Dim val As String
        Dim criterias As New CriteriaComposite(New Criteria(GetType(LKPPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlStatus.SelectedValue < 0 Then
            If isDealer = True Then
                criterias.opAnd(New Criteria(GetType(LKPPHeader), "Status", MatchType.InSet, "(" & EnumStatusLKPP.StatusLKPP.Baru & "," & _
                                                                                                                   EnumStatusLKPP.StatusLKPP.Validasi & "," & _
                                                                                                                   EnumStatusLKPP.StatusLKPP.Setuju & "," & _
                                                                                                                   EnumStatusLKPP.StatusLKPP.Tolak & ")"))
            Else
                'criterias.opAnd(New Criteria(GetType(LKPPHeader), "Status", MatchType.InSet, "(" & EnumStatusLKPP.StatusLKPP.Validasi & "," & _
                '                                                                                    EnumStatusLKPP.StatusLKPP.Setuju & "," & _
                '                                                                                    EnumStatusLKPP.StatusLKPP.Tolak & ")"))
            End If

        Else
            val = ddlStatus.SelectedValue
            criterias.opAnd(New Criteria(GetType(LKPPHeader), "Status", MatchType.Exact, CInt(val)))
        End If

        If txtLKPPNumber.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(LKPPHeader), "ReferenceNumber", MatchType.[Partial], txtLKPPNumber.Text.Trim()))
        End If

        If txtDealerCode.Text.Trim <> String.Empty Then
            Dim list() As String = txtDealerCode.Text.Split(";")
            Dim strDealerID As String = String.Empty
            If list.Length > 0 Then
                For Each item As String In txtDealerCode.Text.Split(";")
                    Dim objDealer As Dealer = New DealerFacade(User).Retrieve(item)
                    If Not IsNothing(objDealer) Then
                        If strDealerID = String.Empty Then
                            strDealerID = objDealer.ID
                        Else
                            strDealerID = strDealerID & "," & objDealer.ID
                        End If
                    End If
                Next
            End If
            If strDealerID <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(LKPPHeader), "ID", MatchType.InSet, "(select LKPPHeaderID from LKPPDealer where DealerID in (" & strDealerID & "))"))
            End If
        Else
            Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                Dim DealerGroupID As Integer = objDealer.DealerGroup.ID
                If DealerGroupID = 21 Then 'Single Dealer
                    criterias.opAnd(New Criteria(GetType(LKPPHeader), "ID", MatchType.InSet, "(select LKPPHeaderID from LKPPDealer where RowStatus = 0 and DealerID= " & objDealer.ID.ToString & ")"))
                Else
                    criterias.opAnd(New Criteria(GetType(LKPPHeader), "ID", MatchType.InSet, "(select LKPPHeaderID from LKPPDealer where RowStatus = 0 and DealerID in (select ID from Dealer where ID=" & objDealer.ID.ToString & "))"))
                End If
            End If
        End If

        If txtCustName.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(LKPPHeader), "GovInstName", MatchType.[Partial], txtCustName.Text.Trim()))
        End If

        criterias.opAnd(New Criteria(GetType(LKPPHeader), "LetterDate", MatchType.GreaterOrEqual, icStartDate.Value))
        criterias.opAnd(New Criteria(GetType(LKPPHeader), "LetterDate", MatchType.LesserOrEqual, icEndDate.Value))


        sessHelper.SetSession("CRITERIAS", criterias)
    End Sub


    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        arrList = _LKPPHeaderFacade.RetrieveByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), idxPage + 1, dgLKPPHeader.PageSize, totalRow, _
                                                       CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        sessHelper.SetSession("LKPPHeaderList", arrList)
        Dim ar As ArrayList = CType(sessHelper.GetSession("LKPPHeaderList"), ArrayList)
        dgLKPPHeader.DataSource = arrList
        dgLKPPHeader.VirtualItemCount = totalRow
        dgLKPPHeader.DataBind()

        'For Each item As LKPPHeader In ar
        '    'Wording status 
        '    If isDealer = True Then
        '        Select Case item.Status
        '            Case 0
        '                lblStatus.Text = "Tidak/Belum Valid"
        '            Case 1
        '                lblStatus.Text = "Valid"
        '            Case Else
        '                lblStatus.Text = "Tidak/Belum Valid"
        '        End Select
        '    Else
        '        Select Case item.Status
        '            Case 2
        '                lblStatus.Text = "Disetujui"
        '            Case 3
        '                lblStatus.Text = "Ditolak"
        '        End Select
        '    End If
        '    Dim isRemain As Boolean = False
        '    If item.LKPPDetails.Count > 0 Then
        '        For Each objDet As LKPPDetail In item.LKPPDetails
        '            If objDet.UnitRemain > 0 Then
        '                isRemain = True
        '                Exit For
        '            End If
        '        Next
        '        If isRemain = False Then
        '            lblStatus.Text = "Selesai"
        '        End If
        '    Else
        '        lblStatus.Text = "Tidak/Belum Valid"
        '    End If

        'Next
        bindRules(dgLKPPHeader)
    End Sub

    Private Function IsValidToDelete(ByVal idLKPP As Integer) As Boolean
        Dim IsValid As Boolean = True
        Dim arrList As New ArrayList
        'Dim ObjLKPP As LKPPHeader = _LKPPHeaderFacade.Retrieve(idLKPP)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(EndCustomer), "LKPPHeader.ID", MatchType.Exact, idLKPP))
        arrList = New EndCustomerFacade(User).Retrieve(criterias)
        If arrList.Count > 0 Then
            IsValid = False
            Return IsValid
        End If

        Return IsValid
    End Function


    Private Function cekFaktur() As Boolean
        Dim result As Boolean = False
        Dim o As New EndCustomer
        Dim _sessHelper As New SessionHelper

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New EnumStatusLKPP
        Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)

        For Each oDataGridItem In dgLKPPHeader.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _lkpp As New LKPPHeader
                _lkpp.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                _lkpp = objLKPPHeaderFacade.Retrieve(_lkpp.ID)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(EndCustomer), "LKPPHeader.ID", MatchType.Exact, _lkpp.ID))
                criterias.opAnd(New Criteria(GetType(EndCustomer), "ChassisMaster.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                Dim _result As ArrayList = New EndCustomerFacade(User).RetrieveByCriteria(criterias, CType(ViewState("currentSortColumn"), String), _
                                                                                          CType(ViewState("currentSortDirection"), Sort.SortDirection))
                If _result.Count > 0 Then
                    result = True
                    Return result
                    Exit For
                End If
            End If
        Next
        Return result
    End Function

    Private Function cekFaktur(ByVal ObjIdLkpp As Integer) As Boolean
        Dim result As Boolean = False
        Dim o As New EndCustomer
        Dim _sessHelper As New SessionHelper

        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New EnumStatusLKPP
        Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)

        'For Each oDataGridItem In dgLKPPHeader.Items
        '    chkExport = oDataGridItem.FindControl("ChkExport")
        '    If chkExport.Checked Then
        Dim _lkpp As New LKPPHeader
        _lkpp.ID = ObjIdLkpp
        _lkpp = objLKPPHeaderFacade.Retrieve(_lkpp.ID)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(EndCustomer), "LKPPHeader.ID", MatchType.Exact, _lkpp.ID))
        criterias.opAnd(New Criteria(GetType(EndCustomer), "ChassisMaster.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim _result As ArrayList = New EndCustomerFacade(User).RetrieveByCriteria(criterias, CType(ViewState("currentSortColumn"), String), _
                                                                                  CType(ViewState("currentSortDirection"), Sort.SortDirection))
        If _result.Count > 0 Then
            result = True
            Return result
        End If
        '    End If
        'Next
        Return result
    End Function
    Private Sub DownloadLKPP(ByRef sw As StreamWriter)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim LKPPLine As StringBuilder = New StringBuilder
        Dim LKPPHeaderList As ArrayList = CType(sessHelper.GetSession("LKPPHeaderList"), ArrayList)
        LKPPHeaderList = _LKPPHeaderFacade.RetrieveByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If LKPPHeaderList Is Nothing Then

            LKPPHeaderList = New ArrayList
        End If
        LKPPLine.Remove(0, LKPPLine.Length)  '-- Empty LKPP line
        LKPPLine.Append("Kode Dealer" & tab)
        LKPPLine.Append("Nomor Pengadaan" & tab)
        LKPPLine.Append("Tanggal Pengajuan" & tab)
        LKPPLine.Append("Nama Institusi Pemerintah" & tab)
        LKPPLine.Append("Status" & tab)
        LKPPLine.Append("Tipe Kendaraan" & tab)
        LKPPLine.Append("Unit" & tab)
        LKPPLine.Append("Sisa Unit" & tab)
        sw.WriteLine(LKPPLine.ToString())  '-- Write LKPP line
        For Each objHeader As LKPPHeader In LKPPHeaderList
            For Each objDealer As LKPPDealer In objHeader.LKPPDealers
                For Each objDet As LKPPDetail In objHeader.LKPPDetails
                    LKPPLine.Remove(0, LKPPLine.Length)  '-- Empty LKPP line
                    LKPPLine.Append(objDealer.Dealer.DealerCode & tab)
                    LKPPLine.Append(objHeader.ReferenceNumber & tab)
                    LKPPLine.Append(Format(objHeader.LetterDate, "dd/MM/yyyy") & tab)
                    LKPPLine.Append(objHeader.GovInstName & tab)
                    LKPPLine.Append(CType(objHeader.Status, EnumStatusLKPP.StatusLKPP).ToString() & tab)
                    LKPPLine.Append(objDet.VechileType.VechileTypeCode & tab)
                    LKPPLine.Append(objDet.Unit & tab)
                    LKPPLine.Append(objDet.UnitRemain & tab)
                    sw.WriteLine(LKPPLine.ToString())  '-- Write LKPP line
                Next
            Next
        Next

    End Sub

    Private Sub SetSessionCriteria()
        Dim objLKPPList As ArrayList = New ArrayList
        objLKPPList.Add(txtDealerCode.Text.Trim) '0
        objLKPPList.Add(txtLKPPNumber.Text.Trim) '1
        objLKPPList.Add(txtCustName.Text.Trim) '2
        objLKPPList.Add(ddlStatus.SelectedValue) '3
        objLKPPList.Add(icStartDate.Value) '4
        objLKPPList.Add(icEndDate.Value) '5
        objLKPPList.Add(dgLKPPHeader.CurrentPageIndex) '6
        objLKPPList.Add(CType(ViewState("CurrentSortColumn"), String)) '7
        objLKPPList.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection)) '8

        Session("SESSIONSEARCHLKPP") = objLKPPList
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objLKPPList As ArrayList = Session("SESSIONSEARCHLKPP")
        If Not objLKPPList Is Nothing Then
            txtDealerCode.Text = objLKPPList.Item(0)
            txtLKPPNumber.Text = objLKPPList.Item(1)
            txtCustName.Text = objLKPPList.Item(2)
            ddlStatus.SelectedValue = objLKPPList.Item(3)
            icStartDate.Value = CType(objLKPPList.Item(4), Date)
            icEndDate.Value = CType(objLKPPList.Item(5), Date)
            dgLKPPHeader.CurrentPageIndex = CType(objLKPPList.Item(6), Integer)
            ViewState("CurrentSortColumn") = objLKPPList.Item(7)
            ViewState("CurrentSortDirect") = objLKPPList.Item(8)

            Return True
        End If
        Return False
    End Function

    'Konfirm status harus validasi
    Private Function PopulateLKPP_Validasi() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New EnumStatusLKPP
        Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)

        For Each oDataGridItem In dgLKPPHeader.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _lkpp As New LKPPHeader
                _lkpp.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                _lkpp = objLKPPHeaderFacade.Retrieve(_lkpp.ID)
                If _lkpp.Status = EnumStatusLKPP.StatusLKPP.Baru Then
                    _lkpp.Status = status.StatusLKPP.Validasi
                    _lkpp.Notes = CType(oDataGridItem.FindControl("txtNotes"), TextBox).Text
                    oExArgs.Add(_lkpp)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulateLKPP_Setuju() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New EnumStatusLKPP
        Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)

        For Each oDataGridItem In dgLKPPHeader.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _lkpp As New LKPPHeader
                _lkpp.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                _lkpp = objLKPPHeaderFacade.Retrieve(_lkpp.ID)
                If _lkpp.Status = EnumStatusLKPP.StatusLKPP.Validasi Then
                    _lkpp.Status = status.StatusLKPP.Setuju
                    _lkpp.Notes = CType(oDataGridItem.FindControl("txtNotes"), TextBox).Text
                    oExArgs.Add(_lkpp)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulateLKPP_Tolak() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New EnumStatusLKPP
        Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)

        For Each oDataGridItem In dgLKPPHeader.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _lkpp As New LKPPHeader
                _lkpp.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                _lkpp = objLKPPHeaderFacade.Retrieve(_lkpp.ID)
                If _lkpp.Status = EnumStatusLKPP.StatusLKPP.Validasi Then
                    _lkpp.Status = status.StatusLKPP.Tolak
                    _lkpp.Notes = CType(oDataGridItem.FindControl("txtNotes"), TextBox).Text
                    oExArgs.Add(_lkpp)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulateLKPP_BatalTolak() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New EnumStatusLKPP
        Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)

        For Each oDataGridItem In dgLKPPHeader.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _lkpp As New LKPPHeader
                _lkpp.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                _lkpp = objLKPPHeaderFacade.Retrieve(_lkpp.ID)
                If _lkpp.Status = EnumStatusLKPP.StatusLKPP.Tolak Then
                    _lkpp.Status = status.StatusLKPP.Validasi
                    _lkpp.Notes = CType(oDataGridItem.FindControl("txtNotes"), TextBox).Text
                    oExArgs.Add(_lkpp)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulateLKPP_BatalSetuju() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New EnumStatusLKPP
        Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)

        For Each oDataGridItem In dgLKPPHeader.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _lkpp As New LKPPHeader
                _lkpp.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                _lkpp = objLKPPHeaderFacade.Retrieve(_lkpp.ID)
                If _lkpp.Status = EnumStatusLKPP.StatusLKPP.Setuju Then
                    _lkpp.Status = status.StatusLKPP.Validasi
                    _lkpp.Notes = CType(oDataGridItem.FindControl("txtNotes"), TextBox).Text
                    oExArgs.Add(_lkpp)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulateLKPP_BatalValidasi() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New EnumStatusLKPP
        Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)

        For Each oDataGridItem In dgLKPPHeader.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _lkpp As New LKPPHeader
                _lkpp.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                _lkpp = objLKPPHeaderFacade.Retrieve(_lkpp.ID)
                If _lkpp.Status = status.StatusLKPP.Validasi Then
                    _lkpp.Status = status.StatusLKPP.Baru
                    _lkpp.Notes = CType(oDataGridItem.FindControl("txtNotes"), TextBox).Text
                    oExArgs.Add(_lkpp)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulateHapus() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New EnumStatusLKPP
        Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)

        For Each oDataGridItem In dgLKPPHeader.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _lkpp As New LKPPHeader
                _lkpp.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                _lkpp = objLKPPHeaderFacade.Retrieve(_lkpp.ID)
                If _lkpp.Status = status.StatusLKPP.Baru Then
                    _lkpp.RowStatus = 1
                    oExArgs.Add(_lkpp)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Sub Validasi()
        Dim listLKPP As ArrayList = PopulateLKPP_Validasi()
        If listLKPP.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("LKPP", "Baru"))
        Else
            For Each item As LKPPHeader In listLKPP

            Next
            Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)
            objLKPPHeaderFacade.validateLKPP(listLKPP)
            'RecordStatusChangeHistory(listLKPP, EnumStatusLKPP.Status.Validasi)
            dgLKPPHeader.SelectedIndex = -1
            dgLKPPHeader.CurrentPageIndex = 0
            BindDataGrid(dgLKPPHeader.CurrentPageIndex)
            MessageBox.Show("Status sudah berubah menjadi Validasi")
        End If
    End Sub

    Private Sub Setuju()
        Dim listLKPP As ArrayList = PopulateLKPP_Setuju()
        If listLKPP.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("LKPP", "Validasi"))
        Else
            Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)
            objLKPPHeaderFacade.validateLKPP(listLKPP)
            'RecordStatusChangeHistory(listLKPP, EnumStatusLKPP.Status.Validasi)
            dgLKPPHeader.SelectedIndex = -1
            dgLKPPHeader.CurrentPageIndex = 0
            BindDataGrid(dgLKPPHeader.CurrentPageIndex)
            MessageBox.Show("Status sudah berubah menjadi Setuju")
        End If
    End Sub

    Private Sub Tolak()
        Dim listLKPP As ArrayList = PopulateLKPP_Tolak()
        If listLKPP.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("LKPP", "Validasi"))
        Else
            Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)
            objLKPPHeaderFacade.validateLKPP(listLKPP)
            'RecordStatusChangeHistory(listLKPP, EnumStatusLKPP.Status.Validasi)
            dgLKPPHeader.SelectedIndex = -1
            dgLKPPHeader.CurrentPageIndex = 0
            BindDataGrid(dgLKPPHeader.CurrentPageIndex)
            MessageBox.Show("Status sudah berubah menjadi Tolak")
        End If
    End Sub

    Private Sub BatalTolak()
        Dim listLKPP As ArrayList = PopulateLKPP_BatalTolak()
        If listLKPP.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("LKPP", "Tolak"))
        Else
            For Each LKPP As LKPPHeader In listLKPP
                If cekFaktur() = True Then
                    MessageBox.Show("Faktur LKPP No. " & LKPP.ReferenceNumber & " telah digunakan")
                    Exit Sub
                End If
            Next
            Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)
            objLKPPHeaderFacade.validateLKPP(listLKPP)
            'RecordStatusChangeHistory(listLKPP, EnumStatusLKPP.Status.Validasi)
            dgLKPPHeader.SelectedIndex = -1
            dgLKPPHeader.CurrentPageIndex = 0
            BindDataGrid(dgLKPPHeader.CurrentPageIndex)
            MessageBox.Show("Status tolak sudah dibatalkan, berubah menjadi Validasi")
        End If
    End Sub

    Private Sub BatalSetuju()
        Dim listLKPP As ArrayList = PopulateLKPP_BatalSetuju()
        If listLKPP.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("LKPP", "Setuju"))
        Else
            Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)
            For Each LKPP As LKPPHeader In listLKPP
                If cekFaktur(LKPP.ID) = True Then
                    MessageBox.Show("Faktur LKPP No. " & LKPP.ReferenceNumber & " telah digunakan")
                    Exit Sub
                End If
            Next
            objLKPPHeaderFacade.validateLKPP(listLKPP)
            'RecordStatusChangeHistory(listLKPP, EnumStatusLKPP.Status.Validasi)
            dgLKPPHeader.SelectedIndex = -1
            dgLKPPHeader.CurrentPageIndex = 0
            BindDataGrid(dgLKPPHeader.CurrentPageIndex)
            MessageBox.Show("Status setuju sudah dibatalkan, berubah menjadi Validasi")
        End If
    End Sub

    Private Sub BatalValidasi()
        Dim listLKPP As ArrayList = PopulateLKPP_BatalValidasi()
        If listLKPP.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("LKPP", " Validasi"))
        Else
            For Each LKPP As LKPPHeader In listLKPP
                If cekFaktur() = True Then
                    MessageBox.Show("Faktur LKPP No. " & LKPP.ReferenceNumber & " telah digunakan")
                    Exit Sub
                End If
            Next
            Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)
            objLKPPHeaderFacade.validateLKPP(listLKPP)
            'RecordStatusChangeHistory(listLKPP, EnumStatusLKPP.Status.Validasi)
            dgLKPPHeader.SelectedIndex = -1
            dgLKPPHeader.CurrentPageIndex = 0
            BindDataGrid(dgLKPPHeader.CurrentPageIndex)
            MessageBox.Show("Status Validasi sudah dibatalkan, berubah menjadi Baru")
        End If
    End Sub

    Private Sub hapus()
        Dim listLKPP As ArrayList = PopulateHapus()
        If listLKPP.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("LKPP", "Baru"))
        Else
            Dim objLKPPHeaderFacade As New LKPPHeaderFacade(User)
            If (hdnValDel.Value = "-1") Then
                MessageBox.Confirm("Yakin ingin menghapus No.LKPP yang dipilih ?", "hdnValDel")
                Return
            End If
            objLKPPHeaderFacade.validateLKPP(listLKPP)
            dgLKPPHeader.SelectedIndex = -1
            dgLKPPHeader.CurrentPageIndex = 0
            BindDataGrid(dgLKPPHeader.CurrentPageIndex)
        End If
    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        If Me.ddlAction.SelectedValue > 0 Then
            Dim act As Integer = ddlAction.SelectedValue
            Select Case act
                Case 1
                    Validasi()
                Case 2
                    Setuju()
                Case 3
                    Tolak()
                Case 4
                    BatalValidasi()
                Case 5
                    BatalSetuju()
                Case 6
                    BatalTolak()
                Case 7
                    hapus()
            End Select

            For Each oDataGridItem As DataGridItem In dgLKPPHeader.Items
                Dim chkExport As CheckBox
                chkExport = oDataGridItem.FindControl("ChkExport")
                chkExport.Checked = False
            Next
        End If
    End Sub

#End Region


    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        bindActions(isDealer)
        If isDealer = True And ddlStatus.SelectedValue <> 0 Then
            Dim dl As Integer = ddlAction.Items.Count
            ddlAction.Items.RemoveAt(dl - 1)
        Else

        End If
    End Sub

End Class