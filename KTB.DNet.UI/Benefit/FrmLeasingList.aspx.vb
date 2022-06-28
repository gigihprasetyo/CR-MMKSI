Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.BusinessFacade.Helper

Public Class FrmLeasingList
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim _sHelper As New SessionHelper
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgTable As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtLeasingName As System.Web.UI.WebControls.TextBox

    Protected WithEvents txtVendorId As System.Web.UI.WebControls.TextBox

    Protected WithEvents hfID As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnTambah As System.Web.UI.WebControls.Button

    Protected WithEvents formLeasing As System.Web.UI.WebControls.Panel
    Protected WithEvents formGrid As System.Web.UI.WebControls.Panel

    Protected WithEvents btnCari As System.Web.UI.WebControls.Button

    'Protected WithEvents chkCreatedDate As System.Web.UI.WebControls.CheckBox
    'Protected WithEvents icStartDate As KTB.DNet.WebCC.IntiCalendar
    'Protected WithEvents icEndDate As KTB.DNet.WebCC.IntiCalendar
    'Protected WithEvents ddlAlertCategory As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlAlertModul As System.Web.UI.WebControls.DropDownList

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
    Private objDomain As LeasingCompany = New LeasingCompany
    Private objFacade As LeasingCompanyFacade = New LeasingCompanyFacade(User)
    Private Viewbenefitleasing_privillage As Boolean
    Private inputbenefitleasing_privillage As Boolean

#Region "Private Property"
    'Private Property SesType() As EnumAlertManagement.AlertManagementType
    '    Get
    '        Return CType(sessHelper.GetSession("ListAlertMasterType"), EnumAlertManagement.AlertManagementType)
    '    End Get
    '    Set(ByVal Value As EnumAlertManagement.AlertManagementType)
    '        sessHelper.SetSession("ListAlertMasterType", Value)
    '    End Set
    'End Property
    Private Property SesMode() As enumMode.Mode
        Get
            Return CType(_sHelper.GetSession("LeasingMode"), enumMode.Mode)
        End Get
        Set(ByVal Value As enumMode.Mode)
            _sHelper.SetSession("LeasingMode", Value)
        End Set
    End Property

    'Private Property SesName() As enumMode.Mode
    '    Get
    '        Return CType(_sHelper.GetSession("LeasingName"), enumMode.Mode)
    '    End Get
    '    Set(ByVal Value As enumMode.Mode)
    '        _sHelper.SetSession("LeasingMode", Value)
    '    End Set
    'End Property

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        'If Not SecurityProvider.Authorize(context.User, SR.AlertManagementListView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - BENEFIT TYPE")
        'End If
        inputbenefitleasing_privillage = False
        Viewbenefitleasing_privillage = False
        inputbenefitleasing_privillage = SecurityProvider.Authorize(Context.User, SR.inputbenefitleasing_privillage)

        If Not inputbenefitleasing_privillage Then
            Viewbenefitleasing_privillage = SecurityProvider.Authorize(Context.User, SR.Viewbenefitleasing_privillage)
            If Not Viewbenefitleasing_privillage Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - Leasing")
            End If

        End If

        If Not inputbenefitleasing_privillage Then
            btnSimpan.Visible = False


        End If
    End Sub

    'Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(context.User, SR.AlertManagementListView_Privilege)
#End Region

   


   

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then

            ViewState("currentSortColumn") = "Name"
            ViewState("typeForm") = "View"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC


            ViewState("typeForm") = "Insert"

            dgTable.CurrentPageIndex = 0
            BindDataGrid(dgTable.CurrentPageIndex)

           
        Else

            If Convert.ToString(ViewState("typeForm")) = "Insert" Or Convert.ToString(ViewState("typeForm")) = "Edit" Then
                'VisibleForm(1)
            End If

        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click

        'Dim _arrList As New ArrayList
        'Dim totalRow As Integer = 0

        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LeasingCompany), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(LeasingCompany), "LeasingName", MatchType.Partial, txtLeasingName.Text.Trim))
        'criterias.opAnd(New Criteria(GetType(LeasingCompany), "VendorID", MatchType.Partial, txtVendorId.Text.Trim))
        '_arrList = New LeasingCompanyFacade(User).RetrieveByCriteria(criterias, dgTable.CurrentPageIndex + 1, dgTable.PageSize,
        '                                                                   totalRow)
        'dgTable.VirtualItemCount = totalRow
        '' _arrList = New BenefitTypeFacade(User).RetrieveActiveList()
        'dgTable.DataSource = _arrList
        'dgTable.DataBind()

        BindDataGrid(dgTable.CurrentPageIndex)
        btnSimpan.Enabled = False
    End Sub

    Private Sub BindDataGrid(ByVal index As Integer)
        'Dim _arrList As New ArrayList
        'Dim totalRow As Integer = 0

        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LeasingCompany), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '_arrList = New LeasingCompanyFacade(User).RetrieveByCriteria(criterias, dgTable.CurrentPageIndex + 1, dgTable.PageSize,
        '                                                                   totalRow)
        'dgTable.VirtualItemCount = totalRow
        ''_arrList = New LeasingCompanyFacade(User).RetrieveActiveList()
        'dgTable.DataSource = _arrList
        ''dgTable.VirtualItemCount = totalRow
        'dgTable.DataBind()

        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LeasingCompany), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(LeasingCompany), "LeasingName", MatchType.Partial, txtLeasingName.Text.Trim))
        criterias.opAnd(New Criteria(GetType(LeasingCompany), "VendorID", MatchType.Partial, txtVendorId.Text.Trim))
        _arrList = New LeasingCompanyFacade(User).RetrieveByCriteria(criterias, dgTable.CurrentPageIndex + 1, dgTable.PageSize, totalRow)

        dgTable.VirtualItemCount = totalRow
        ' _arrList = New BenefitTypeFacade(User).RetrieveActiveList()
        dgTable.DataSource = _arrList
        dgTable.DataBind()
    End Sub
    Private Sub dgTable_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTable.ItemCommand
        Select Case e.CommandName
            Case "View"
                btnSimpan.Enabled = False
                btnCari.Enabled = btnSimpan.Enabled
                ' btnBatal.Text = "Kembali"
                ViewModel(CShort(e.CommandArgument))
                dgTable.SelectedIndex = e.Item.ItemIndex
                VisibleForm(2)
            Case "Edit"


                Dim _arrList As New ArrayList
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterLeasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BenefitMasterLeasing), "LeasingCompany", MatchType.Exact, CShort(e.CommandArgument)))
                _arrList = New BenefitMasterLeasingFacade(User).Retrieve(criterias)
                If _arrList.Count > 0 Then
                    MessageBox.Show("Leasing masih digunakan di modul lain (Benefit Master).")
                    Return
                End If

                Dim _arrList1 As New ArrayList
                Dim criterias1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(BenefitClaimHeader), "LeasingCompany", MatchType.Exact, CShort(e.CommandArgument)))
                _arrList1 = New BenefitClaimHeaderFacade(User).Retrieve(criterias1)
                If _arrList1.Count > 0 Then
                    MessageBox.Show("Leasing masih digunakan di modul lain (Benefit Claim).")
                    Return
                End If

                btnSimpan.Enabled = True
                btnBatal.Text = "Batal"
                btnCari.Enabled = Not btnSimpan.Enabled
                ViewState("typeForm") = "Edit"
                ViewModel(CShort(e.CommandArgument))
                dgTable.SelectedIndex = e.Item.ItemIndex
                VisibleForm(1)

                'Dim _arrList As New ArrayList
                'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterLeasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(BenefitMasterLeasing), "LeasingCompany", MatchType.Exact, CShort(e.CommandArgument)))
                '_arrList = New BenefitMasterLeasingFacade(User).Retrieve(criterias)
                'If _arrList.Count > 0 Then
                '    MessageBox.Show("Leasing masih digunakan di modul lain (Benefit Master).")
                '    Return
                'End If

                'Dim _arrList1 As New ArrayList
                'Dim criterias1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias1.opAnd(New Criteria(GetType(BenefitClaimHeader), "LeasingCompany", MatchType.Exact, CShort(e.CommandArgument)))
                '_arrList1 = New BenefitClaimHeaderFacade(User).Retrieve(criterias1)
                'If _arrList1.Count > 0 Then
                '    MessageBox.Show("Leasing masih digunakan di modul lain (Benefit Claim).")
                '    Return
                'End If

            Case "Delete"


                Dim _arrList As New ArrayList
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterLeasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BenefitMasterLeasing), "LeasingCompany", MatchType.Exact, CShort(e.CommandArgument)))
                _arrList = New BenefitMasterLeasingFacade(User).Retrieve(criterias)
                If _arrList.Count > 0 Then
                    MessageBox.Show("Leasing masih digunakan di modul lain (Benefit Master).")
                    Return
                End If

                Dim _arrList1 As New ArrayList
                Dim criterias1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(BenefitClaimHeader), "LeasingCompany", MatchType.Exact, CShort(e.CommandArgument)))
                _arrList1 = New BenefitClaimHeaderFacade(User).Retrieve(criterias1)
                If _arrList1.Count > 0 Then
                    MessageBox.Show("Leasing masih digunakan di modul lain (Benefit Claim).")
                    Return
                End If



                ViewState("typeForm") = "Delete"

                objDomain = objFacade.Retrieve(CShort(e.CommandArgument))
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                objFacade.UpdateForDelete(objDomain)


                txtLeasingName.Text = ""
                txtVendorId.Text = ""
                hfID.Text = ""
                
                VisibleForm(1)

                dgTable.SelectedIndex = -1
                ViewState("typeForm") = "Insert"
                btnSimpan.Enabled = True
                btnCari.Enabled = btnSimpan.Enabled
                BindDataGrid(dgTable.CurrentPageIndex)

        End Select
    End Sub

    Private Sub dgTable_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTable.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As LeasingCompany = CType(e.Item.DataItem, LeasingCompany)


            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            'lblNo.Text = objDomain2.ID.ToString
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)).ToString()


            Dim lblLeasingName As Label = CType(e.Item.FindControl("lblLeasingName"), Label)
            lblLeasingName.Text = objDomain2.LeasingName

            Dim lblVendorId As Label = CType(e.Item.FindControl("lblVendorId"), Label)
            lblVendorId.Text = objDomain2.VendorID


            Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
            'add privilige
            'lnkbtnView.Visible = bCekDetailPriv
            lnkbtnEdit.Visible = inputbenefitleasing_privillage
            lnkbtnDelete.Visible = inputbenefitleasing_privillage

            End If
    End Sub

    Private Sub dgTable_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgTable.PageIndexChanged
        dgTable.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgTable.CurrentPageIndex)
    End Sub

    Private Sub dgTable_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgTable.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgTable.SelectedIndex = -1
        dgTable.CurrentPageIndex = 0
        BindDataGrid(dgTable.CurrentPageIndex)
    End Sub


    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
		dim strMessage as string = ""
        if txtLeasingName.Text.Trim = "" then
			strMessage = "Nama leasing tidak boleh kosong."
		end if
		if txtVendorId.Text.Trim = "" then
			if strMessage <> "" then 
                strMessage = strMessage + "\n ID Vendor tidak boleh kosong."
			else
				strMessage = "ID Vendor tidak boleh kosong"
            End If
        Else
            If ViewState("typeForm") = "Insert" Then
                Dim _arrList1 As New ArrayList
                Dim criterias1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LeasingCompany), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(LeasingCompany), "VendorID", MatchType.Exact, txtVendorId.Text))
                _arrList1 = New LeasingCompanyFacade(User).Retrieve(criterias1)
                If _arrList1.Count > 0 Then
                    strMessage = "ID Vendor Tidak Boleh sama dengan yang sudah ada"
                End If
            End If
        End If
		
       
        If strMessage.Trim <> "" Then
            MessageBox.Show(strMessage)
            Exit Sub
        End If
		
		Dim objDomain As LeasingCompany
        objDomain = New LeasingCompany

		
        objDomain.LeasingName = txtLeasingName.Text

        objDomain.VendorID = txtVendorId.Text

        objDomain.RowStatus = 0

        Dim result As Integer
        Dim facade As New LeasingCompanyFacade(User)


        If Convert.ToString(ViewState("typeForm")) = "Insert" Then
            result = facade.Insert(objDomain)
        Else
            objDomain.ID = CShort(hfID.Text)
            result = facade.Update(objDomain)
        End If



        If result <> -1 Then

            ' BindDataGrid(dgTable.CurrentPageIndex)
            ' VisibleForm(1)
            btnBatal_Click(sender, e)
            MessageBox.Show(SR.SaveSuccess)
        Else
            MessageBox.Show(SR.SaveFail)
        End If


    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        
        txtLeasingName.Text = ""
        txtVendorId.Text = ""
        hfID.Text = ""
        'SesMode = enumMode.Mode.NewItemMode
        'dgTable.Visible = True
        VisibleForm(1)

        dgTable.SelectedIndex = -1
        ViewState("typeForm") = "Insert"
        btnSimpan.Enabled = True
        btnCari.Enabled = btnSimpan.Enabled
        BindDataGrid(dgTable.CurrentPageIndex)
    End Sub

    Private Sub btnTambah_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTambah.Click
        
        txtLeasingName.Text = ""
        txtVendorId.Text = ""
        hfID.Text = ""
        VisibleForm(1)
        ViewState("typeForm") = "Insert"
        btnSimpan.Enabled = True
    End Sub

    Private Sub VisibleForm(ByVal tipe As Integer)
        If tipe = 1 Then 'tipe insert / edit
            ' formLeasing.Visible = True
            '  formGrid.Visible = False
            txtLeasingName.Enabled = True
            txtVendorId.Enabled = txtLeasingName.Enabled
            If ViewState("typeForm") = "Edit" Then
                txtVendorId.Enabled = False
            End If
        Else 'tipe view
            '  formLeasing.Visible = False
            '  formGrid.Visible = True
            txtLeasingName.Enabled = False
            txtVendorId.Enabled = txtLeasingName.Enabled
        End If

    End Sub

    Private Sub ViewModel(ByVal nID As Short)
        Dim objDomain As LeasingCompany = New LeasingCompanyFacade(User).Retrieve(nID)
        'Todo session
        If IsNothing(objDomain) Then
            txtLeasingName.Text = ""
            txtVendorId.Text = ""
            hfID.Text = ""
        Else
            txtLeasingName.Text = objDomain.LeasingName
            hfID.Text = objDomain.ID.ToString
            txtVendorId.Text = objDomain.VendorID

        End If


    End Sub

End Class
