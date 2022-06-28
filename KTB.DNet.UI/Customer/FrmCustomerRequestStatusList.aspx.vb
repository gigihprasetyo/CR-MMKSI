#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security
#End Region

Imports System.IO

Public Class FrmCustomerRequestStatusList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAlamat As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTipePengajuan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKota As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgCustomerRequest As System.Web.UI.WebControls.DataGrid
    Protected WithEvents icTglPengajuan1 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglPengajuan2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtNoPengajuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents lstStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents txtSPKNumber As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim criterias As CriteriaComposite
    Dim arlCustomerRequest As ArrayList
    Dim objCustRequest As CustomerRequest
    Private oDealer As Dealer

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.CustomerListViewReg_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=KONSUMEN - Daftar Registrasi")
        End If
    End Sub

    Dim bCekGridBtnPriv As Boolean = SecurityProvider.Authorize(context.User, SR.CustomerEditReg_Privilege)
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        oDealer = CType(sessHelp.GetSession("DEALER"), Dealer)
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            BindDropDown()
            ClearForm()
            GetSessionCriteria()
            BindDatagrid(dtgCustomerRequest.CurrentPageIndex)
        End If
    End Sub

    Private Sub BindDropDown()
        ddlTipePengajuan.DataSource = New EnumTipePengajuanCustomerRequest().RetrieveType()
        ddlTipePengajuan.DataTextField = "NameTipe"
        ddlTipePengajuan.DataValueField = "ValTipe"
        ddlTipePengajuan.DataBind()
        ddlTipePengajuan.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlTipePengajuan.SelectedIndex = 0

        lstStatus.DataSource = New EnumStatusCustomerRequest().RetrieveType
        lstStatus.DataTextField = "NameTipe"
        lstStatus.DataValueField = "ValTipe"
        lstStatus.DataBind()
        'ddlStatus.DataSource = New EnumStatusCustomerRequest().RetrieveType
        'ddlStatus.DataTextField = "NameTipe"
        'ddlStatus.DataValueField = "ValTipe"
        'ddlStatus.DataBind()
        'ddlStatus.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        'ddlStatus.SelectedIndex = 0

        Dim criteriakota As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriakota.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))
        ddlKota.DataSource = New CityFacade(User).RetrieveActiveList(criteriakota, "CityName", Sort.SortDirection.ASC)
        ddlKota.DataTextField = "CityName"
        ddlKota.DataValueField = "ID"
        ddlKota.DataBind()
        ddlKota.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlKota.SelectedIndex = 0




    End Sub
    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        'dtgCustomerRequest.CurrentPageIndex = 0
        If CreateCriteria() Then
            sessHelp.SetSession("curPageIndex", indexPage)
            arlCustomerRequest = New CustomerRequestFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgCustomerRequest.PageSize, totalRow, ViewState.Item("SortCol").ToString, ViewState.Item("SortDirection"))
            dtgCustomerRequest.DataSource = arlCustomerRequest
            dtgCustomerRequest.VirtualItemCount = totalRow
            dtgCustomerRequest.DataBind()
        Else
            MessageBox.Show("Kode dealer tidak valid.")
        End If

    End Sub


    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgCustomerRequest.CurrentPageIndex = 0
        BindDatagrid(dtgCustomerRequest.CurrentPageIndex)
    End Sub

    Private Function CreateCriteria() As Boolean
        Dim objDealer As Dealer = New SessionHelper().GetSession("DEALER")
        criterias = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtNoPengajuan.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "RequestNo", MatchType.[Partial], txtNoPengajuan.Text.Trim))
        End If
        txtNama.Text = txtNama.Text.Trim
        If txtNama.Text.Trim <> String.Empty Then
            If txtNama.Text.Split(" ").Length > 1 Then
                Dim nama As String
                For i As Integer = 0 To txtNama.Text.Split(" ").Length - 1
                    nama = txtNama.Text.Split(" ")(i)
                    If i = 0 Then
                        criterias.opAnd(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], nama), "(", True)
                        criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], nama))
                    ElseIf i = txtNama.Text.Split(" ").Length - 1 Then
                        criterias.opOr(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], nama))
                        criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], nama), ")", False)
                    Else
                        criterias.opOr(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], nama))
                        criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], nama))
                    End If
                Next
            Else
                criterias.opAnd(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], txtNama.Text), "(", True)
                criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], txtNama.Text), ")", False)
            End If
        End If

        If (txtSPKNumber.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "SPKHeader.SPKNumber", MatchType.[Partial], txtSPKNumber.Text.Trim))
        End If

        If (txtAlamat.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "Alamat", MatchType.[Partial], txtAlamat.Text.Trim))
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (txtDealer.Text.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(CustomerRequest), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealer.Text, ";", "','") + "')"))
            End If
        Else
            If (txtDealer.Text.Trim <> String.Empty) Then
                If New DataOwner().IsdealerExistInGroup(txtDealer.Text.Trim, objDealer) Then
                    criterias.opAnd(New Criteria(GetType(CustomerRequest), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealer.Text, ";", "','") + "')"))
                Else
                    Return False
                End If
            Else
                Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
                criterias.opAnd(New Criteria(GetType(CustomerRequest), "Dealer.DealerCode", MatchType.InSet, strCrit))
            End If
        End If

        If ddlKota.SelectedValue <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "CityID", MatchType.Exact, ddlKota.SelectedValue))
        End If

        If ddlTipePengajuan.SelectedValue <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "RequestType", MatchType.Exact, ddlTipePengajuan.SelectedValue))
        End If

        'get the status
        Dim selectedId As String
        Dim li As ListItem
        For Each li In lstStatus.Items
            If li.Selected = True Then
                selectedId = selectedId & li.Value & ","
            End If
        Next

        If selectedId <> "" Then
            Dim getStatusID As String = Left(selectedId, selectedId.Length - 1)
            viewstate.Add("StatusID", getStatusID)
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "Status", MatchType.InSet, "(" & getStatusID & ")"))
        End If

        'If ddlStatus.SelectedValue <> String.Empty Then
        '    criterias.opAnd(New Criteria(GetType(CustomerRequest), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        'End If

        Dim sDate As Date = New Date(icTglPengajuan1.Value.Year, icTglPengajuan1.Value.Month, icTglPengajuan1.Value.Day, 0, 0, 0)
        Dim eDate As Date = New Date(icTglPengajuan2.Value.Year, icTglPengajuan2.Value.Month, icTglPengajuan2.Value.Day, 23, 59, 59)


        criterias.opAnd(New Criteria(GetType(CustomerRequest), "RequestDate", MatchType.GreaterOrEqual, sDate))
        criterias.opAnd(New Criteria(GetType(CustomerRequest), "RequestDate", MatchType.LesserOrEqual, eDate))

        Return True
    End Function


    Private Sub dtgCustomerRequest_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCustomerRequest.ItemDataBound

        If e.Item.ItemIndex <> -1 Then
            objCustRequest = arlCustomerRequest(e.Item.ItemIndex)
            Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            _lblNo.Text = CType(e.Item.ItemIndex + 1 + (dtgCustomerRequest.CurrentPageIndex * dtgCustomerRequest.PageSize), String)

            Dim _lblReqStatus As Label = CType(e.Item.FindControl("lblReqStatus"), Label)
            _lblReqStatus.Text = New EnumStatusCustomerRequest().RetrieveName(objCustRequest.Status)

            Dim reqType As Integer
            If Integer.TryParse(objCustRequest.RequestType, reqType) Then
                Dim _lblRequestType As Label = CType(e.Item.FindControl("lblRequestType"), Label)
                _lblRequestType.Text = New EnumTipePengajuanCustomerRequest().RetrieveName(reqType)
            End If

            Dim _lblKota As Label = CType(e.Item.FindControl("lblKota"), Label)
            _lblKota.Text = New CityFacade(User).Retrieve(CInt(objCustRequest.CityID)).CityName

            Dim _lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            _lblDealer.Text = objCustRequest.Dealer.DealerCode
            _lblDealer.ToolTip = objCustRequest.Dealer.SearchTerm1

            Dim _lblSPKNumber As Label = CType(e.Item.FindControl("lblSPKNumber"), Label)
            If Not objCustRequest.SPKHeader Is Nothing Then
                _lblSPKNumber.Text = objCustRequest.SPKHeader.SPKNumber
            Else

                If Not IsNothing(objCustRequest.SPKDetailCustomer) Then
                    If Not IsNothing(objCustRequest.SPKDetailCustomer.SPKDetail) AndAlso Not IsNothing(objCustRequest.SPKDetailCustomer.SPKDetail.SPKHeader) Then
                        _lblSPKNumber.Text = objCustRequest.SPKDetailCustomer.SPKDetail.SPKHeader.SPKNumber
                    End If

                Else
                    _lblSPKNumber.Text = ""
                End If

            End If
            '_lblSPKNumber.ToolTip = objCustRequest.SPKHeader

            Dim _lblAlamat As Label = CType(e.Item.FindControl("lblAlamat"), Label)
            _lblAlamat.Text = objCustRequest.Alamat

            Dim _lblNama As Label = CType(e.Item.FindControl("lblNama"), Label)

            _lblNama.Text = objCustRequest.Name1 & " " & objCustRequest.Name2



            Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)


            Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            _lbtnEdit.CommandArgument = objCustRequest.ID.ToString

            _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")

            _lbtnDelete.CommandArgument = objCustRequest.ID.ToString

            If objCustRequest.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Baru Then
                If objCustRequest.Dealer.ID = oDealer.ID Then
                    'cek privilege
                    _lbtnEdit.Visible = bCekGridBtnPriv
                    _lbtnDelete.Visible = bCekGridBtnPriv
                Else
                    _lbtnEdit.Visible = False
                    _lbtnDelete.Visible = False
                End If
            Else
                _lbtnEdit.Visible = False
                _lbtnDelete.Visible = False
            End If

            'Dim lblSPKNumber As Label = e.Item.FindControl("lblSPKNumber")

            'Dim criterias As CriteriaComposite
            'criterias = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(SPKHeader), "CustomerRequest.ID", MatchType.Exact, objCustRequest.ID))

            'Dim objPK As New ArrayList
            'objPK = New KTB.DNet.BusinessFacade.FinishUnit.SPKHeaderFacade(User).Retrieve(criterias)

            'If Not IsNothing(objPK) AndAlso objPK.Count > 0 Then
            '    lblSPKNumber.Text = CType(objPK(0), SPKHeader).SPKNumber
            'End If

            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            lbtnView.CommandArgument = objCustRequest.ID.ToString

            Dim _lbtnFileName As LinkButton = CType(e.Item.FindControl("lbtnFileName"), LinkButton)
            _lbtnFileName.CommandArgument = objCustRequest.Attachment
            If objCustRequest.Attachment <> "" Then
                _lbtnFileName.Visible = True
            Else
                _lbtnFileName.Visible = False
            End If
            'If objCustRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP Then
            '    e.Item.BackColor = System.Drawing.Color.Gainsboro
            'End If
        End If
    End Sub

    Private Sub dtgCustomerRequest_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgCustomerRequest.ItemCommand

        If e.CommandName = "Delete" Then
            Dim objCustomerRequestFacade As CustomerRequestFacade = New CustomerRequestFacade(User)
            objCustomerRequestFacade.Delete(objCustomerRequestFacade.Retrieve(CInt(e.CommandArgument)))
            'Add by anh,  req by myk - 20171003
            'set spkheader.CustumerRequestID = 0
            Dim objSPKHeaderFacade As KTB.DNet.BusinessFacade.FinishUnit.SPKHeaderFacade = New KTB.DNet.BusinessFacade.FinishUnit.SPKHeaderFacade(User)
            criterias = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKHeader), "CustomerRequestID", MatchType.Exact, CInt(e.CommandArgument)))
            Dim objSPKHeaderList As ArrayList = objSPKHeaderFacade.Retrieve(criterias)
            If objSPKHeaderList.Count > 0 Then
                For Each _spkHeader As SPKHeader In objSPKHeaderList
                    _spkHeader.CustomerRequestID = 0
                    objSPKHeaderFacade.Update(_spkHeader)
                Next
            End If
            'end added

            BindDatagrid(dtgCustomerRequest.CurrentPageIndex)
        ElseIf e.CommandName = "Edit" Then
            SetSessionCriteria()
            Response.Redirect("FrmCustomerRequest.aspx?mode=edit&id=" + e.CommandArgument.ToString)
        ElseIf e.CommandName = "download" Then
            SetSessionCriteria()
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("CustomerRequestDir") & "\" & e.CommandArgument.ToString
            Response.Redirect("../Download.aspx?file=" & PathFile)
        ElseIf e.CommandName = "View" Then
            SetSessionCriteria()
            Response.Redirect("FrmCustomerRequest.aspx?mode=view&id=" + e.CommandArgument.ToString)
        ElseIf e.CommandName = "HISTORY" Then
            SetSessionCriteria()
            Response.Redirect("FrmDaftarStatusPerRequest.aspx?qxctrvvyuotrpn=" & e.CommandArgument.ToString)

        End If


    End Sub

    Private Sub dtgCustomerRequest_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCustomerRequest.SortCommand


        If e.SortExpression = viewstate.Item("SortCol") Then
            If viewstate.Item("SortDirection") = Sort.SortDirection.ASC Then
                viewstate.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                viewstate.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        viewstate.Item("SortCol") = e.SortExpression
        dtgCustomerRequest.SelectedIndex = -1
        dtgCustomerRequest.CurrentPageIndex = 0
        BindDatagrid(dtgCustomerRequest.CurrentPageIndex)
    End Sub

    Private Sub dtgCustomerRequest_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCustomerRequest.PageIndexChanged
        dtgCustomerRequest.CurrentPageIndex = e.NewPageIndex

        BindDatagrid(dtgCustomerRequest.CurrentPageIndex)
        sessHelp.SetSession("curPageIndex", dtgCustomerRequest.CurrentPageIndex)
    End Sub

    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtNoPengajuan.Text)
        arrLastState.Add(txtNama.Text)
        arrLastState.Add(txtAlamat.Text)
        arrLastState.Add(icTglPengajuan1.Value) 'From
        arrLastState.Add(icTglPengajuan2.Value) ' To

        arrLastState.Add(txtDealer.Text)
        arrLastState.Add(ddlKota.SelectedIndex)
        arrLastState.Add(ddlTipePengajuan.SelectedIndex)
        arrLastState.Add(viewstate("StatusID"))
        'arrLastState.Add(ddlStatus.SelectedIndex)

        arrLastState.Add(dtgCustomerRequest.CurrentPageIndex)
        arrLastState.Add(CType(ViewState("SortCol"), String))
        arrLastState.Add(CType(ViewState("SortDirection"), Sort.SortDirection))
        sessHelp.SetSession("CRSESSIONLASTSTATE", arrLastState)
    End Sub

    Private Sub GetSessionCriteria()
        Dim arrLastState As ArrayList = CType(sessHelp.GetSession("CRSESSIONLASTSTATE"), ArrayList)
        If Not arrLastState Is Nothing Then
            txtNoPengajuan.Text = arrLastState.Item(0).ToString
            txtNama.Text = arrLastState.Item(1).ToString
            txtAlamat.Text = arrLastState.Item(2).ToString
            icTglPengajuan1.Value = CDate(arrLastState.Item(3))
            icTglPengajuan2.Value = CDate(arrLastState.Item(4))

            txtDealer.Text = arrLastState.Item(5).ToString
            ddlKota.SelectedIndex = CInt(arrLastState.Item(6))
            ddlTipePengajuan.SelectedIndex = CInt(arrLastState.Item(7))

            If Not arrLastState.Item(8) Is Nothing Then
                If arrLastState.Item(8).ToString().IndexOf(",") > 0 Then
                    Dim statusID() As String = arrLastState.Item(8).ToString.Split(",")
                    For Each item As String In statusID
                        lstStatus.Items.Item(CInt(item)).Selected = True
                    Next
                Else
                    lstStatus.Items.Item(CInt(arrLastState.Item(8).ToString())).Selected = True
                End If
            End If
            'ddlStatus.SelectedIndex = arrLastState.Item(8)

            dtgCustomerRequest.CurrentPageIndex = CInt(arrLastState.Item(9))
            ViewState("SortCol") = arrLastState.Item(10)
            ViewState("SortDirection") = arrLastState.Item(11)
        Else
            dtgCustomerRequest.CurrentPageIndex = 0
            ViewState("SortCol") = "Dealer.DealerCode"
            ViewState("SortDirection") = Sort.SortDirection.ASC
        End If
    End Sub

    Private Sub ClearForm()
        txtNoPengajuan.Text = ""
        txtNama.Text = ""
        txtAlamat.Text = ""
        icTglPengajuan1.Value = DateTime.Today
        icTglPengajuan2.Value = DateTime.Today

        txtDealer.Text = ""
        ddlKota.SelectedIndex = 0
        ddlTipePengajuan.SelectedIndex = 0
        lstStatus.SelectedIndex = -1
        'ddlStatus.SelectedIndex = 0
    End Sub

End Class
