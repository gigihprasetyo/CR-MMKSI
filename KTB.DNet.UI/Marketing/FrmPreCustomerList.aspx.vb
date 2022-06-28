Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text

Public Class FrmPreCustomerList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSalesmanID As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents icPaymentDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPaymentDateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCustomerType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents dgSAPCustomer As System.Web.UI.WebControls.DataGrid
    Protected WithEvents hdnID As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnName As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlGender As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlAge As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipe As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSource As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPurpose As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTotalRow As System.Web.UI.WebControls.Label
    Protected WithEvents lblVechileTypeCode As System.Web.UI.WebControls.Label
    Protected WithEvents txtVechileTypeCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCustomerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatusCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLeadStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStateCode As System.Web.UI.WebControls.DropDownList
    '
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private sessionHelper As New SessionHelper


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "PrivateVariables"
    Private _SAPCustomerFacade As New SAPCustomerFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.CustomerProspekListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SAP - Daftar Konsumen Prospek")
        End If
    End Sub

    Private Function CekListDeletePrivilege() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.CustomerProspekListDelete_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekListEditPrivilege() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.CustomerProspekCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    '
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            BindControlsAttribute()
            GetSessionCriteria()
            Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")

            If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                lblSearchDealer.Visible = False
                txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
                txtKodeDealer.Attributes.Add("readonly", "readonly")
                dgSAPCustomer.Columns(15).Visible = False
                dgSAPCustomer.Columns(16).Visible = True
            Else
                dgSAPCustomer.Columns(15).Visible = True
                dgSAPCustomer.Columns(16).Visible = False
            End If

            'If Not CekListDeletePrivilege() Then
            '    dgSAPCustomer.Columns(16).Visible = False    'icon delete
            'End If

        End If
    End Sub

    Private Sub dgSAPCustomer_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSAPCustomer.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSAPCustomer As SAPCustomer = e.Item.DataItem
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim oDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
                Dim lnkbtnEdit As LinkButton = e.Item.FindControl("lnkbtnEdit")
                lnkbtnEdit.Visible = False
                If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    If objSAPCustomer.Status <> CInt(EnumSAPCustomerResponse.SAPCustomerResponse.SPK) Then
                        lnkbtnEdit.Visible = CekListEditPrivilege() 'True
                    End If
                End If

                Dim lnkbtnSPK As LinkButton = e.Item.FindControl("lnkbtnSPK")
                Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
                lnkbtnSPK.Visible = False
                lbtnDelete.Visible = False
                If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    If (objSAPCustomer.Status < EnumSAPCustomerStatus.SAPCustomerStatus.Deal_SPK) Then
                        lnkbtnSPK.Visible = True
                        lbtnDelete.Visible = CekListDeletePrivilege() 'True
                    End If
                End If
                '
                If (objSAPCustomer.SalesforceID.Length > 0) Then
                    lbtnDelete.Visible = False
                End If

                CType(e.Item.FindControl("lblNomor"), Label).Text = e.Item.ItemIndex + 1 + (dgSAPCustomer.CurrentPageIndex * dgSAPCustomer.PageSize)

                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                lblDealerCode.Text = objSAPCustomer.Dealer.DealerCode

                Dim lblDealerBranch As Label = CType(e.Item.FindControl("lblDealerBranch"), Label)
                If Not IsNothing(objSAPCustomer.SalesmanHeader) Then
                    If Not IsNothing(objSAPCustomer.SalesmanHeader.DealerBranch) Then
                        lblDealerBranch.Text = objSAPCustomer.SalesmanHeader.DealerBranch.Name
                    End If
                Else
                    lblDealerBranch.Text = ""
                End If

                Dim lblNameNew As Label = CType(e.Item.FindControl("lblCustomerName"), Label)
                lblNameNew.Text = objSAPCustomer.CustomerName

                Dim lblCodeNew As Label = CType(e.Item.FindControl("lblCustomerCode"), Label)
                lblCodeNew.Text = objSAPCustomer.CustomerCode

                Dim lblCustomerAddress As Label = CType(e.Item.FindControl("lblCustomerAddress"), Label)
                lblCustomerAddress.Text = objSAPCustomer.CustomerAddress

                Dim lblPhone As Label = CType(e.Item.FindControl("lblPhone"), Label)
                lblPhone.Text = objSAPCustomer.Phone

                Dim lblEmail As Label = CType(e.Item.FindControl("lblEmail"), Label)
                lblEmail.Text = objSAPCustomer.Email

                Dim lblCustomerType As Label = CType(e.Item.FindControl("lblCustomerType"), Label)
                lblCustomerType.Text = EnumTipePelangganCustomerRequest.RetrieveTipePelangganCustomerRequest(objSAPCustomer.CustomerType)


                Dim lblGender As Label = CType(e.Item.FindControl("lblGender"), Label)
                lblGender.Text = EnumGender.GetStringGender(objSAPCustomer.Sex)
                Dim lblTipe As Label = CType(e.Item.FindControl("lblTipe"), Label)
                lblTipe.Text = EnumInformationType.GetStringInformationType(objSAPCustomer.InformationType)
                Dim lblPurpose As Label = CType(e.Item.FindControl("lblPurpose"), Label)
                lblPurpose.Text = EnumCustomerPurpose.GetStringCustomerPurpose(objSAPCustomer.CustomerPurpose)
                Dim lblSource As Label = CType(e.Item.FindControl("lblSource"), Label)
                lblSource.Text = EnumInformationSource.GetStringInformationSource(objSAPCustomer.InformationSource)

                Dim lblStatusNew As Label = CType(e.Item.FindControl("lblStatus"), Label)
                lblStatusNew.Text = CType(objSAPCustomer.Status, EnumSAPCustomerStatus.SAPCustomerStatus).ToString.Replace("_", " ")
                Dim lblQty As Label = CType(e.Item.FindControl("lblQty"), Label)
                lblQty.Text = objSAPCustomer.Qty.ToString()
                Dim lblVechileTypeCode As Label = CType(e.Item.FindControl("lblVechileTypeCode"), Label)
                If Not IsNothing(objSAPCustomer.VechileType) Then
                    lblVechileTypeCode.Text = objSAPCustomer.VechileType.VechileTypeCode
                End If
                Dim lblProspectDateNew As Label = CType(e.Item.FindControl("lblProspectDate"), Label)
                lblProspectDateNew.Text = objSAPCustomer.ProspectDate.ToString("dd/MM/yyyy")

                Dim lblSalesmanID As Label = CType(e.Item.FindControl("lblSalesmanID"), Label)
                Dim lblSalesmanName As Label = CType(e.Item.FindControl("lblSalesmanName"), Label)
                If Not IsNothing(objSAPCustomer.SalesmanHeader) Then
                    lblSalesmanID.Text = objSAPCustomer.SalesmanHeader.SalesmanCode
                    lblSalesmanName.Text = objSAPCustomer.SalesmanHeader.Name
                Else
                    lblSalesmanID.Text = String.Empty
                    lblSalesmanName.Text = String.Empty
                End If

            End If

        End If
    End Sub

    Private Sub dgSAPCustomer_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSAPCustomer.PageIndexChanged
        dgSAPCustomer.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSAPCustomer.CurrentPageIndex)
    End Sub

    Private Sub dgSAPCustomer_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSAPCustomer.SortCommand
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
        dgSAPCustomer.SelectedIndex = -1
        BindDataGrid(dgSAPCustomer.CurrentPageIndex)

    End Sub

    Private Sub dgSAPCustomer_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSAPCustomer.ItemCommand
        SetSessionCriteria()
        sessionHelper.SetSession("PrevPage", Request.Url.ToString())
        Select Case e.CommandName
            Case "Delete"
                Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                Dim objSAPCustomer As SAPCustomer = New SAPCustomerFacade(User).Retrieve(CInt(lblID.Text))
                If (New SAPCustomerFacade(User).DeleteStatus(objSAPCustomer) <> -1) Then
                    BindDataGrid(0)
                    MessageBox.Show("Data berhasil dihapus")
                Else
                    MessageBox.Show("Data gagal dihapus")
                End If
            Case "Edit"
                Server.Transfer("~/Marketing/FrmPreCustomerUpdateForm.aspx?mode=edit&CustId=" & e.CommandArgument)
            Case "View"
                Server.Transfer("~/Marketing/FrmPreCustomerUpdateForm.aspx?mode=view&CustId=" & e.CommandArgument)
            Case "SPK"
                Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                Dim objSAPCustomer As SAPCustomer = New SAPCustomerFacade(User).Retrieve(CInt(lblID.Text))
                If Not IsNothing(objSAPCustomer) AndAlso objSAPCustomer.ID > 0 Then
                    If Not IsNothing(objSAPCustomer.SalesmanHeader) AndAlso objSAPCustomer.SalesmanHeader.ID > 0 Then
                        Server.Transfer("~/FinishUnit/FrmSPKHeader.aspx?Mode=0&CustId=" & e.CommandArgument)
                    Else
                        MessageBox.Show("Kode Salesman kosong, mohon update data prospektif konsumen.")
                        Exit Sub

                    End If
                End If
        End Select
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtSalesmanID.Text <> "" Then
            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
            If objSalesmanHeader.ID = 0 Then
                MessageBox.Show("No Salesman yang anda masukkan tidak ada")
                Exit Sub
            End If
            lblNamaSalesman.Text = objSalesmanHeader.Name
        End If
        Dim startDate As Date = icPaymentDateFrom.Value
        Dim endDate As Date = icPaymentDateTo.Value
        If startDate > endDate Then
            MessageBox.Show("Tanggal awal tidak boleh lebih besar dari tanggal akhir")
            Exit Sub
        End If

        CreateCriteria()
        'lblNamaSalesman.Text = hdnName.Value
        dgSAPCustomer.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub


    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arrListToDownload As New ArrayList
        If Not sessHelper.GetSession("FRMPRECUSTOMERLIST") Is Nothing Then
            arrListToDownload = _SAPCustomerFacade.Retrieve(CType(sessHelper.GetSession("FRMPRECUSTOMERLIST"), CriteriaComposite))
            'sessHelper.SetSession("ViewSAPCustomer", arrListToDownload)
        End If
        If arrListToDownload.Count > 0 Then
            DoDownload(arrListToDownload)
        Else
            MessageBox.Show("Tidak ada data yang di download")
        End If
    End Sub

#End Region

#Region "Custom"



    Private Sub ClearData()
        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblSearchDealer.Visible = False
            txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
            txtKodeDealer.Enabled = False
        Else
            lblSearchDealer.Visible = True
            txtKodeDealer.Text = String.Empty
            txtKodeDealer.Enabled = True
        End If
        txtSalesmanID.Text = String.Empty
        txtCustomerName.Text = String.Empty
        txtVechileTypeCode.Text = String.Empty
        lblNamaSalesman.Text = String.Empty
        ddlCustomerType.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        ddlTipe.SelectedIndex = 0
        ddlGender.SelectedIndex = 0
        ddlAge.SelectedIndex = 0
        ddlSource.SelectedIndex = 0
        ddlPurpose.SelectedIndex = 0
        ddlLeadStatus.SelectedIndex = 0
        ddlStateCode.SelectedIndex = 0
        ddlStatusCode.SelectedIndex = 0
        dgSAPCustomer.DataSource = Nothing
        lblTotalRow.Text = String.Empty
        btnDownload.Enabled = False
        icPaymentDateFrom.Value = Date.Now
        icPaymentDateTo.Value = Date.Now
        dgSAPCustomer.VirtualItemCount = 0
        dgSAPCustomer.DataBind()

    End Sub

    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtKodeDealer.Text)
        arrLastState.Add(txtSalesmanID.Text)
        arrLastState.Add(txtVechileTypeCode.Text)
        arrLastState.Add(icPaymentDateFrom.Value)
        arrLastState.Add(icPaymentDateTo.Value)
        arrLastState.Add(ddlCustomerType.SelectedIndex)
        arrLastState.Add(ddlStatus.SelectedIndex)
        arrLastState.Add(ddlGender.SelectedIndex)
        arrLastState.Add(ddlAge.SelectedIndex)
        arrLastState.Add(ddlTipe.SelectedIndex)
        arrLastState.Add(ddlSource.SelectedIndex)
        arrLastState.Add(dgSAPCustomer.CurrentPageIndex)
        arrLastState.Add(CType(ViewState("currSortColumn"), String))
        arrLastState.Add(CType(ViewState("currSortDirection"), Sort.SortDirection))
        arrLastState.Add(txtCustomerName.Text)
        arrLastState.Add(ddlPurpose.SelectedIndex)
        sessHelper.SetSession("CustomerSessionState", arrLastState)
    End Sub
    Private Sub GetSessionCriteria()
        Dim arrLastState As ArrayList = sessHelper.GetSession("CustomerSessionState")
        If Not arrLastState Is Nothing Then
            txtKodeDealer.Text = arrLastState.Item(0)
            txtSalesmanID.Text = arrLastState.Item(1)
            txtVechileTypeCode.Text = arrLastState.Item(2)
            icPaymentDateFrom.Value = arrLastState.Item(3)
            icPaymentDateTo.Value = arrLastState.Item(4)
            ddlCustomerType.SelectedIndex = arrLastState.Item(5)
            ddlStatus.SelectedIndex = arrLastState.Item(6)
            ddlGender.SelectedIndex = arrLastState.Item(7)
            ddlAge.SelectedIndex = arrLastState.Item(8)
            ddlTipe.SelectedIndex = arrLastState.Item(9)
            ddlSource.SelectedIndex = arrLastState.Item(10)
            dgSAPCustomer.CurrentPageIndex = CInt(arrLastState.Item(11))
            ViewState("currSortColumn") = arrLastState.Item(12)
            ViewState("currSortDirection") = arrLastState.Item(13)
            txtCustomerName.Text = arrLastState(14)
            ddlPurpose.SelectedIndex = arrLastState(15)
            BindDataGrid(dgSAPCustomer.CurrentPageIndex)
        Else
            If CType(sessHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtKodeDealer.Text = CType(sessHelper.GetSession("DEALER"), Dealer).DealerCode
            End If
            ViewState("currSortColumn") = "CreatedTime"
            ViewState("currSortDirection") = Sort.SortDirection.DESC
            dgSAPCustomer.CurrentPageIndex = 0
        End If


    End Sub

    Private Sub BindControlsAttribute()
        lblVechileTypeCode.Attributes("onclick") = "ShowPopUpVechileType();"
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblPopUpSalesman.Attributes("onclick") = "ShowPPSAP();"
        BindStatus()
        BindCustomerType()
        BindGender(ddlGender)
        BindAgeSegment(ddlAge)
        BindInformationType(ddlTipe)
        BindInformationSource(ddlSource)
        BindCustomerPurpose(ddlPurpose)
        BindStatusCode(ddlStatusCode)
        BindStateCode(ddlStateCode)
        BindLeadStatus(ddlLeadStatus)
        btnDownload.Enabled = False
    End Sub

    Private Sub BindCustomerType()
        ddlCustomerType.Items.Clear()
        ddlCustomerType.DataSource = New EnumTipePelangganCustomerRequest().RetrieveType(True)
        ddlCustomerType.DataTextField = "NameTipe"
        ddlCustomerType.DataValueField = "ValTipe"
        ddlCustomerType.DataBind()
        'ddlCustomerType.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub

    Private Sub BindStatus()
        'CommonFunction.BindFromEnum("SAPCustomerStatus", ddlStatus, User, True, "NameStatus", "ValStatus")
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = New EnumDNET().RetrieveSAPCustomerList()
        ddlStatus.DataTextField = "NameType"
        ddlStatus.DataValueField = "ValType"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
    End Sub

    Private Sub BindGender(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumGenderOp.RetriveSalesGender(True)
        For Each item As EnumGenderOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindAgeSegment(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumAgeSegmentOp.RetriveAgeSegment(True)
        For Each item As EnumAgeSegmentOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindInformationType(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumInformationTypeOp.RetriveInformationType(True)
        For Each item As EnumInformationTypeOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next

    End Sub

    Private Sub BindInformationSource(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumInformationSourceOp.RetriveInformationSource(True)
        For Each item As EnumInformationSourceOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindCustomerPurpose(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumCustomerPurposeOp.RetriveCustomerPurpose(True)
        For Each item As EnumCustomerPurposeOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindStatusCode(ByVal ddl As DropDownList)
        ddlStatusCode.DataSource = New StandardCodeFacade(User).RetrieveByCategory("LeadStatusCode")
        ddlStatusCode.DataTextField = "ValueDesc"
        ddlStatusCode.DataValueField = "ValueId"
        ddlStatusCode.DataBind()
        ddlStatusCode.Items.Insert(0, New ListItem("Silakan Piih", ""))
        ddlStatusCode.SelectedIndex = 0

    End Sub

    Private Sub BindLeadStatus(ByVal ddl As DropDownList)
        ddlLeadStatus.DataSource = New StandardCodeFacade(User).RetrieveByCategory("LeadStatus")
        ddlLeadStatus.DataTextField = "ValueDesc"
        ddlLeadStatus.DataValueField = "ValueId"
        ddlLeadStatus.DataBind()
        ddlLeadStatus.Items.Insert(0, New ListItem("Silakan Piih", ""))
        ddlLeadStatus.SelectedIndex = 0
    End Sub

    Private Sub BindStateCode(ByVal ddl As DropDownList)
        ddlStateCode.DataSource = New StandardCodeFacade(User).RetrieveByCategory("LeadStateCode")
        ddlStateCode.DataTextField = "ValueDesc"
        ddlStateCode.DataValueField = "ValueId"
        ddlStateCode.DataBind()
        ddlStateCode.Items.Insert(0, New ListItem("Silakan Piih", ""))
        ddlStateCode.SelectedIndex = 0

    End Sub

    Private Sub CreateCriteria()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SAPCustomer), "ProspectDate", MatchType.GreaterOrEqual, icPaymentDateFrom.Value))
        criterias.opAnd(New Criteria(GetType(SAPCustomer), "ProspectDate", MatchType.Lesser, icPaymentDateTo.Value.AddDays(1)))

        If txtKodeDealer.Text.Trim() <> "" Then
            Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
            'criterias.opAnd(New Criteria(GetType(SAPCustomer), "SalesmanHeader.Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
        End If

        If txtCustomerName.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "CustomerName", MatchType.[Partial], txtCustomerName.Text.Trim()))
        End If

        If txtSalesmanID.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "SalesmanHeader.SalesmanCode", MatchType.[Partial], txtSalesmanID.Text.Trim()))
        End If

        If txtVechileTypeCode.Text.Length > 0 Then
            Dim strVechileTypeCodeIn As String = "('" & txtVechileTypeCode.Text.Trim().Replace(";", "','") & "')"
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "VechileType.VechileTypeCode", MatchType.InSet, strVechileTypeCodeIn))
        End If

        If CInt(ddlCustomerType.SelectedValue) >= 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "CustomerType", MatchType.Exact, ddlCustomerType.SelectedValue))
        End If

        If CInt(ddlStatus.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If
        If CInt(ddlGender.SelectedIndex) > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "Sex", MatchType.Exact, ddlGender.SelectedValue))
        End If
        If CInt(ddlAge.SelectedIndex) > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "AgeSegment", MatchType.Exact, ddlAge.SelectedValue))
        End If
        If CInt(ddlTipe.SelectedIndex) > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "InformationType", MatchType.Exact, ddlTipe.SelectedValue))
        End If
        If CInt(ddlSource.SelectedIndex) > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "InformationSource", MatchType.Exact, ddlSource.SelectedValue))
        End If
        If CInt(ddlPurpose.SelectedIndex) > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "CustomerPurpose", MatchType.Exact, ddlPurpose.SelectedValue))
        End If
        If CInt(ddlLeadStatus.SelectedIndex) > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "LeadStatus", MatchType.Exact, ddlLeadStatus.SelectedValue))
        End If
        If CInt(ddlStateCode.SelectedIndex) > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "StateCode", MatchType.Exact, ddlStateCode.SelectedValue))
        End If
        If CInt(ddlStatusCode.SelectedIndex) > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "StatusCode", MatchType.Exact, ddlStatusCode.SelectedValue))
        End If
        sessHelper.SetSession("FRMPRECUSTOMERLIST", criterias)
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        arrList = _SAPCustomerFacade.RetrieveByCriteria(CType(sessHelper.GetSession("FRMPRECUSTOMERLIST"), CriteriaComposite), idxPage + 1, dgSAPCustomer.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSAPCustomer.DataSource = arrList
        dgSAPCustomer.VirtualItemCount = totalRow
        dgSAPCustomer.DataBind()
        If arrList.Count > 0 Then
            btnDownload.Enabled = True
        Else
            btnDownload.Enabled = False
        End If
        lblTotalRow.Text = "Jumlah record : " & totalRow.ToString
    End Sub


    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "ProspectCustomer" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        'Dim SAPCustomerData As String = Server.MapPath("").Replace("\SAP", "") & "\DataTemp\" & sFileName & ".xls"
        Dim SAPCustomerData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SAPCustomerData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(SAPCustomerData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteSAPKonsumenData(sw, data)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
            Exit Sub
        End Try

        'Response.Write("<script language='javascript'>window.open('../downloadlocal.aspx?file=" & "DataTemp/" & sFileName & ".xls" & "');</script>")

        Response.Redirect("../downloadlocal.aspx?file=" & "DataTemp\" & sFileName & ".xls", False)
    End Sub

    Private Sub WriteSAPKonsumenData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Daftar Konsumen Awal")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Kode Salesman" & tab)
            itemLine.Append("Nama Salesman" & tab)
            itemLine.Append("Kode Konsumen" & tab)
            itemLine.Append("Nama Konsumen" & tab)
            itemLine.Append("Alamat" & tab)
            itemLine.Append("Telepon" & tab)
            itemLine.Append("Jenis Kelamin" & tab)
            itemLine.Append("Usia" & tab)
            itemLine.Append("Status" & tab)
            itemLine.Append("Sumber Informasi" & tab)
            itemLine.Append("Tujuan Konsumen" & tab)
            itemLine.Append("Kuantitas" & tab)
            itemLine.Append("Kode Kendaraan" & tab)
            itemLine.Append("Tipe Kendaraan" & tab)
            itemLine.Append("Sumber Lead" & tab)
            itemLine.Append("Tanggal Prospect" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As SAPCustomer In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                If Not IsNothing(item.SalesmanHeader) Then
                    itemLine.Append(item.SalesmanHeader.Dealer.DealerCode.ToString() & tab)
                    itemLine.Append(item.SalesmanHeader.Dealer.DealerName & tab)
                    itemLine.Append(item.SalesmanHeader.SalesmanCode.ToString() & tab)
                    itemLine.Append(item.SalesmanHeader.Name & tab)
                Else
                    itemLine.Append(item.Dealer.DealerCode.ToString() & tab)
                    itemLine.Append(item.Dealer.DealerName & tab)
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab)
                End If
                itemLine.Append(item.CustomerCode & tab)
                itemLine.Append(item.CustomerName & tab)
                itemLine.Append(item.CustomerAddress.Replace(vbCrLf, " ").Replace(vbCr, " ").Replace(Chr(13) & Chr(10), "") & tab) 'itemLine.Replace(Chr(13) & Chr(10), "")
                itemLine.Append("'" & item.Phone.ToString() & tab)
                itemLine.Append(EnumGender.GetStringGender(item.Sex) & tab)
                itemLine.Append(EnumAgeSegment.GetStringAgeSegment(item.AgeSegment) & tab)
                If item.Status = 1 Then
                    itemLine.Append("Hot Prospect" & tab)
                ElseIf item.Status = 2 Then
                    itemLine.Append("Prospect" & tab)
                ElseIf item.Status = 3 Then
                    itemLine.Append("Suspect" & tab)
                ElseIf item.Status = 4 Then
                    itemLine.Append("Deal/SPK" & tab)
                End If
                itemLine.Append(EnumInformationType.GetStringInformationType(item.InformationType) & tab)
                itemLine.Append(EnumCustomerPurpose.GetStringCustomerPurpose(item.CustomerPurpose) & tab)
                itemLine.Append(item.Qty & tab)
                If Not IsNothing(item.VechileType) Then
                    itemLine.Append(item.VechileType.VechileTypeCode & tab)
                    itemLine.Append(item.VechileType.Description & tab)
                Else
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab)
                End If
                itemLine.Append(EnumInformationSource.GetStringInformationSource(item.InformationSource) & tab)
                itemLine.Append(item.ProspectDate.ToString("yyyy-MM-dd" & tab))
                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub

#End Region

End Class
