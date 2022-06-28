Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
'Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
#End Region

Public Class FrmMCPHeader
    Inherits System.Web.UI.Page

#Region "PrivateVariables"
    Private _MCPHeaderFacade As New MCPHeaderFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _download As Boolean
    Private sessHelper As New SessionHelper

#End Region

#Region "EventHandler"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
            'Dim criterias As New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'CreateCriterias(criterias)
            'sessHelper.SetSession("CRITERIAS", criterias)
            'BindDataGrid(0)
            If GetSessionCriteria() Then
                CreateCriterias()
                BindDataGrid(dgMCPHeader.CurrentPageIndex)
            End If

        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"

        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            txtDealerCode.Attributes.Add("readonly", "readonly")
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'CreateCriterias(criterias)
        'sessHelper.SetSession("CRITERIAS", criterias)
        CreateCriterias()
        dgMCPHeader.CurrentPageIndex = 0
        BindDataGrid(dgMCPHeader.CurrentPageIndex)
    End Sub

    Protected Sub btnDownLoad_Click(sender As Object, e As EventArgs) Handles btnDownLoad.Click
        '-- Download data in datagrid to text file
        '-- Generate random number [0..9999]
        Dim sSuffix As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        '-- Temp file must be a randomly named text file!
        Dim InvoiceData As String = Server.MapPath("") & "\..\DataTemp\mcp_list_" & sSuffix & ".txt"
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
                DownloadMCP(sw)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
            '-- Download data to client!
            Response.Redirect("../downloadLocal.aspx?file=DataTemp\mcp_list_" & sSuffix & ".txt")
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub dgMCPHeader_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgMCPHeader.ItemCommand
        If e.CommandName = "View" Then
            SetSessionCriteria()
            sessHelper.SetSession("Status", "View")
            sessHelper.SetSession("IDMCPHeader", CInt(e.Item.Cells(0).Text))
            Response.Redirect("FrmMCPDetail.aspx")
        ElseIf e.CommandName = "Edit" Then
            SetSessionCriteria()
            sessHelper.SetSession("Status", "Update")
            sessHelper.SetSession("IDMCPHeader", CInt(e.Item.Cells(0).Text))
            Response.Redirect("FrmMCPDetail.aspx")
        ElseIf e.CommandName = "Delete" Then
            If IsValidToDelete(CInt(e.Item.Cells(0).Text)) Then
                Dim ObjMCPHeader As MCPHeader = _MCPHeaderFacade.Retrieve(CInt(e.Item.Cells(0).Text))
                Try
                    _MCPHeaderFacade.Delete(ObjMCPHeader)
                    'Initialize()
                    'BindDataGrid(0)
                    If GetSessionCriteria() Then
                        CreateCriterias()
                        BindDataGrid(dgMCPHeader.CurrentPageIndex)
                    End If
                    MessageBox.Show(SR.DeleteSucces)
                Catch ex As Exception
                    MessageBox.Show(SR.DeleteFail)
                End Try
            Else
                MessageBox.Show(SR.CannotDelete())
            End If
        ElseIf e.CommandName = "Download" Then
            Dim file As String = e.Item.Cells(6).Text
            Dim fInfo As New System.IO.FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("SPLAttachment") & file)
            Try
                Response.Redirect("../Download.aspx?file=" & file)
            Catch ex As Exception
                MessageBox.Show(SR.DownloadFail(file))
            End Try
        End If
    End Sub

    Private Sub dgMCPHeader_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgMCPHeader.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objMCP As MCPHeader = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgMCPHeader.CurrentPageIndex * dgMCPHeader.PageSize)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim strDealerCode = String.Empty
            If objMCP.MCPDealers.Count > 0 Then
                For Each objDealer As MCPDealer In objMCP.MCPDealers
                    If Not IsNothing(objDealer.Dealer) Then
                        strDealerCode = objDealer.Dealer.DealerCode + IIf(strDealerCode = "", "", ";" + strDealerCode)
                    End If
                Next
            End If
            lblDealerCode.Text = strDealerCode

            Dim lblLetterDate As Label = CType(e.Item.FindControl("lblLetterDate"), Label)
            lblLetterDate.Text = objMCP.LetterDate.ToString("dd/MM/yyyy")

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim isRemain As Boolean = False
            If objMCP.MCPDetails.Count > 0 Then
                For Each objDet As MCPDetail In objMCP.MCPDetails
                    If objDet.UnitRemain > 0 Then
                        isRemain = True
                        Exit For
                    End If
                Next
            End If
            If isRemain = False Then
                lblStatus.Text = "Selesai"
            End If

            Dim lbtnVw As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lbtnEd As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDel As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lbtnDown As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            lbtnVw.Visible = _view
            lbtnEd.Visible = _edit
            If objMCP.Status = EnumStatusMCP.StatusMCP.Tidak_Aktif Then
                lbtnDel.Visible = _edit
            Else
                lbtnDel.Visible = False
            End If

            'lbtnDel.Attributes("onclick") = "return confirm('" & SR.DeleteConfirmation() & "')"
            If Not CType(e.Item.DataItem, MCPHeader).Attachment = "" Then
                'lbtnDown.CommandArgument = CType(e.Item.DataItem, MCPHeader).Attachment
                lbtnDown.Visible = True
            Else
                lbtnDown.Visible = False
            End If
        End If
    End Sub

    Private Sub dgMCPHeader_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgMCPHeader.PageIndexChanged
        dgMCPHeader.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgMCPHeader.CurrentPageIndex)
    End Sub

    Private Sub dgMCPHeader_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgMCPHeader.SortCommand
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
        dgMCPHeader.SelectedIndex = -1
        dgMCPHeader.CurrentPageIndex = 0
        BindDataGrid(dgMCPHeader.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"

    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.MCP_List_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Daftar MCP")
        End If

        _create = SecurityProvider.Authorize(Context.User, SR.MCP_Input_Privilege)
        _edit = SecurityProvider.Authorize(Context.User, SR.MCP_Edit_Privilege)
        _download = SecurityProvider.Authorize(Context.User, SR.MCP_List_Privilege)
        _view = SecurityProvider.Authorize(Context.User, SR.MCP_List_Privilege)

        btnSearch.Visible = _view

    End Sub

    Private Sub Initialize()
        txtMCPNumber.Text = ""
        txtDealerCode.Text = ""
        txtCustName.Text = ""
        BindDdlStatus()
        icStartDate.Value = Date.Now.AddMonths(-1)
        ViewState("CurrentSortColumn") = "ReferenceNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = EnumStatusMCP.RetrieveStatus
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataBind()
    End Sub

    Private Sub CreateCriterias()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(MCPHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(MCPHeader), "Status", MatchType.Exact, CInt(ddlStatus.SelectedValue)))

        If txtMCPNumber.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(MCPHeader), "ReferenceNumber", MatchType.[Partial], txtMCPNumber.Text.Trim()))
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
                criterias.opAnd(New Criteria(GetType(MCPHeader), "ID", MatchType.InSet, "(select MCPHeaderID from MCPDealer where DealerID in (" & strDealerID & "))"))
            End If
        Else
            Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                Dim DealerGroupID As Integer = objDealer.DealerGroup.ID
                If DealerGroupID = 21 Then 'Single Dealer
                    criterias.opAnd(New Criteria(GetType(MCPHeader), "ID", MatchType.InSet, "(select MCPHeaderID from MCPDealer where RowStatus = 0 and DealerID= " & objDealer.ID.ToString & ")"))
                Else
                    criterias.opAnd(New Criteria(GetType(MCPHeader), "ID", MatchType.InSet, "(select MCPHeaderID from MCPDealer where RowStatus = 0 and DealerID in (select ID from Dealer where DealerGroupID=" & DealerGroupID.ToString & "))"))
                End If
            End If
        End If

        If txtCustName.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(MCPHeader), "GovInstName", MatchType.[Partial], txtCustName.Text.Trim()))
        End If

        criterias.opAnd(New Criteria(GetType(MCPHeader), "LetterDate", MatchType.GreaterOrEqual, icStartDate.Value))
        criterias.opAnd(New Criteria(GetType(MCPHeader), "LetterDate", MatchType.LesserOrEqual, icEndDate.Value))
        sessHelper.SetSession("CRITERIAS", criterias)
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        arrList = _MCPHeaderFacade.RetrieveByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), idxPage + 1, dgMCPHeader.PageSize, totalRow, _
    CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        sessHelper.SetSession("MCPHeaderList", arrList)

        dgMCPHeader.DataSource = arrList
        dgMCPHeader.VirtualItemCount = totalRow
        dgMCPHeader.DataBind()
    End Sub

    Private Function IsValidToDelete(ByVal idMCP As Integer) As Boolean
        Dim IsValid As Boolean = True
        Dim arrList As New ArrayList
        'Dim ObjMCP As MCPHeader = _MCPHeaderFacade.Retrieve(idMCP)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(EndCustomer), "MCPHeader.ID", MatchType.Exact, idMCP))
        arrList = New EndCustomerFacade(User).Retrieve(criterias)
        If arrList.Count > 0 Then
            IsValid = False
            Return IsValid
        End If

        Return IsValid
    End Function

    Private Sub DownloadMCP(ByRef sw As StreamWriter)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim MCPLine As StringBuilder = New StringBuilder
        Dim MCPHeaderList As ArrayList = CType(sessHelper.GetSession("MCPHeaderList"), ArrayList)

        MCPLine.Remove(0, MCPLine.Length)  '-- Empty MCP line
        MCPLine.Append("Kode Dealer" & tab)
        MCPLine.Append("Nomor MCP" & tab)
        MCPLine.Append("Tanggal Surat" & tab)
        MCPLine.Append("Nama Institusi" & tab)
        MCPLine.Append("Tipe Kendaraan" & tab)
        MCPLine.Append("Unit" & tab)
        sw.WriteLine(MCPLine.ToString())  '-- Write MCP line
        For Each objHeader As MCPHeader In MCPHeaderList
            For Each objDealer As MCPDealer In objHeader.MCPDealers
                For Each objDet As MCPDetail In objHeader.MCPDetails
                    MCPLine.Remove(0, MCPLine.Length)  '-- Empty MCP line
                    MCPLine.Append(objDealer.Dealer.DealerCode & tab)
                    MCPLine.Append(objHeader.ReferenceNumber & tab)
                    MCPLine.Append(Format(objHeader.LetterDate, "dd/MM/yyyy") & tab)
                    MCPLine.Append(objHeader.GovInstName & tab)
                    MCPLine.Append(objDet.VechileType.VechileTypeCode & tab)
                    MCPLine.Append(objDet.Unit & tab)
                    sw.WriteLine(MCPLine.ToString())  '-- Write MCP line
                Next
            Next
        Next

    End Sub

    Private Sub SetSessionCriteria()
        Dim objMCPList As ArrayList = New ArrayList
        objMCPList.Add(txtDealerCode.Text.Trim) '0
        objMCPList.Add(txtMCPNumber.Text.Trim) '1
        objMCPList.Add(txtCustName.Text.Trim) '2
        objMCPList.Add(ddlStatus.SelectedValue) '3
        objMCPList.Add(icStartDate.Value) '4
        objMCPList.Add(icEndDate.Value) '5
        objMCPList.Add(dgMCPHeader.CurrentPageIndex) '6
        objMCPList.Add(CType(ViewState("CurrentSortColumn"), String)) '7
        objMCPList.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection)) '8

        sessHelper.SetSession("SESSIONSEARCHMCP", objMCPList)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objMCPList As ArrayList = sessHelper.GetSession("SESSIONSEARCHMCP")
        If Not objMCPList Is Nothing Then
            txtDealerCode.Text = objMCPList.Item(0)
            txtMCPNumber.Text = objMCPList.Item(1)
            txtCustName.Text = objMCPList.Item(2)
            ddlStatus.SelectedValue = objMCPList.Item(3)
            icStartDate.Value = CType(objMCPList.Item(4), Date)
            icEndDate.Value = CType(objMCPList.Item(5), Date)
            dgMCPHeader.CurrentPageIndex = CType(objMCPList.Item(6), Integer)
            ViewState("CurrentSortColumn") = objMCPList.Item(7)
            ViewState("CurrentSortDirect") = objMCPList.Item(8)
            Return True
        End If
        Return False
    End Function

#End Region

End Class