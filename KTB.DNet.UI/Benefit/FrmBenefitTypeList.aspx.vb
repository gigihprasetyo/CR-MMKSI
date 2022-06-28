Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.BusinessFacade.Helper

Public Class FrmBenefitTypeList
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim _sHelper As New SessionHelper
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgTable As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtBenefitName As System.Web.UI.WebControls.TextBox
    Protected WithEvents hfID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnTambah As System.Web.UI.WebControls.Button
    Protected WithEvents cbLeasing As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbAssy As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbReceipt As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbEvent As System.Web.UI.WebControls.CheckBox
    Protected WithEvents formBenefitType As System.Web.UI.WebControls.Panel
    Protected WithEvents formGrid As System.Web.UI.WebControls.Panel

    Protected WithEvents cbDiskon As System.Web.UI.WebControls.CheckBox


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
    Private objDomain As BenefitType = New BenefitType
    Private objFacade As BenefitTypeFacade = New BenefitTypeFacade(User)
    Private inputtipebenefit_privillage As Boolean
    Private Viewtipebenefit_privillage As Boolean
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
            Return CType(_sHelper.GetSession("BenefitTypeMode"), enumMode.Mode)
        End Get
        Set(ByVal Value As enumMode.Mode)
            _sHelper.SetSession("BenefitTypeMode", Value)
        End Set
    End Property

    'Private Property SesName() As enumMode.Mode
    '    Get
    '        Return CType(_sHelper.GetSession("BenefitTypeName"), enumMode.Mode)
    '    End Get
    '    Set(ByVal Value As enumMode.Mode)
    '        _sHelper.SetSession("BenefitTypeMode", Value)
    '    End Set
    'End Property

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        inputtipebenefit_privillage = False
        Viewtipebenefit_privillage = False
        inputtipebenefit_privillage = SecurityProvider.Authorize(Context.User, SR.inputtipebenefit_privillage)

        If Not inputtipebenefit_privillage Then
            Viewtipebenefit_privillage = SecurityProvider.Authorize(Context.User, SR.Viewtipebenefit_privillage)
            If Not Viewtipebenefit_privillage Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - Tipe Benefit")
            End If

        End If

        If Not inputtipebenefit_privillage Then
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
            'ViewState("typeForm") = "View"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC


            ViewState("typeForm") = "Insert"
            ViewState("_NoBaris") = 0

            dgTable.CurrentPageIndex = 0
            BindDataGrid(dgTable.CurrentPageIndex)


            'If Request.QueryString("tipe") Then

            'End If


            'VisibleForm(2)
        Else

            'If ViewState("typeForm") = "Insert" Or ViewState("typeForm") = "Edit" Then
            '    VisibleForm(1)
            'End If

            'ViewState("typeForm") = "Insert"

        End If
    End Sub

    Private Sub BindDataGrid(ByVal index As Integer)
        'Dim _arrList As New ArrayList
        'Dim totalRow As Integer = 0

        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '_arrList = New BenefitTypeFacade(User).RetrieveByCriteria(criterias, dgTable.CurrentPageIndex + 1, dgTable.PageSize,
        '                                                                   totalRow)
        'dgTable.VirtualItemCount = totalRow
        '' _arrList = New BenefitTypeFacade(User).RetrieveActiveList()
        'dgTable.DataSource = _arrList

        'dgTable.DataBind()


        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtBenefitName.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(BenefitType), "Name", MatchType.Partial, txtBenefitName.Text))
        End If

        Dim strSql As String = ""

        If cbAssy.Checked = True Then

            criterias.opAnd(New Criteria(GetType(BenefitType), "AssyYearBox", MatchType.Exact, "1"))
        End If

        If cbLeasing.Checked = True Then

            criterias.opAnd(New Criteria(GetType(BenefitType), "LeasingBox", MatchType.Exact, "1"))
        End If

        If cbReceipt.Checked = True Then

            criterias.opAnd(New Criteria(GetType(BenefitType), "ReceiptBox", MatchType.Exact, "1"))
        End If

        If cbEvent.Checked = True Then

            criterias.opAnd(New Criteria(GetType(BenefitType), "EventValidation", MatchType.Exact, "1"))
        End If

        If cbDiskon.Checked = True Then

            criterias.opAnd(New Criteria(GetType(BenefitType), "WSDiscount", MatchType.Exact, "1"))
        End If
        _arrList = New BenefitTypeFacade(User).RetrieveByCriteria(criterias, dgTable.CurrentPageIndex + 1, dgTable.PageSize,
                                                                           totalRow)
        dgTable.VirtualItemCount = totalRow
        ' _arrList = New BenefitTypeFacade(User).RetrieveActiveList()
        dgTable.DataSource = _arrList

        dgTable.DataBind()
    End Sub

    Private Sub dgTable_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTable.ItemCommand
        Select Case e.CommandName
            Case "View"
                'Response.Redirect("~/AlertManagement/FrmTransactionalAlert.aspx?Mode=View;" & Request.QueryString("Type") & "&id=" & CInt(e.CommandArgument))
                btnSimpan.Enabled = False
                btnCari.Enabled = btnSimpan.Enabled
                ' btnBatal.Text = "Kembali"
                ViewModel(CShort(e.CommandArgument))
                ViewState("typeForm") = "View"
                ViewState("_NoBaris") = e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)
                dgTable.SelectedIndex = e.Item.ItemIndex
                VisibleForm(2)
            Case "Edit"

                'Dim _arrList As New ArrayList
                'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(BenefitMasterDetail), "BenefitType", MatchType.Exact, CShort(e.CommandArgument)))
                '_arrList = New BenefitMasterDetailFacade(User).Retrieve(criterias)
                'If _arrList.Count > 0 Then
                '    MessageBox.Show("Type Benefit masih digunakan di modul lain (Benefit Master).")
                '    Return
                'End If

                'Response.Redirect("~/AlertManagement/FrmTransactionalAlert.aspx?Mode=Edit;" & Request.QueryString("Type") & "&id=" & CInt(e.CommandArgument))
                btnSimpan.Enabled = True
                btnCari.Enabled = Not btnSimpan.Enabled
                '  btnBatal.Text = "Batal"
                ViewState("typeForm") = "Edit"
                ViewState("_NoBaris") = e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)
                dgTable.SelectedIndex = e.Item.ItemIndex
                ViewModel(CShort(e.CommandArgument))
                VisibleForm(1)
            Case "Delete"





                Dim _arrList As New ArrayList
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BenefitMasterDetail), "BenefitType", MatchType.Exact, CShort(e.CommandArgument)))
                _arrList = New BenefitMasterDetailFacade(User).Retrieve(criterias)
                If _arrList.Count > 0 Then
                    MessageBox.Show("Type Benefit masih digunakan di modul lain (Benefit Master).")
                    Return
                End If

                ViewState("typeForm") = "Delete"
                objDomain = objFacade.Retrieve(CShort(e.CommandArgument))
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                objFacade.UpdateForDelete(objDomain)
                ' BindDataGrid(dgTable.CurrentPageIndex)
                'Case "AlertSound"
                'Response.Write("<script language='javascript'></script>")
                'Response.Write("<script language='javascript'>showPopUp('../PopUp/PopUpAlertSound.aspx?id='" + CInt(e.CommandArgument) + ",'',500,760,'');</script>")


                cbLeasing.Checked = False
                cbAssy.Checked = cbLeasing.Checked
                cbReceipt.Checked = cbLeasing.Checked
                cbEvent.Checked = cbLeasing.Checked
                cbDiskon.Checked = cbLeasing.Checked
                txtBenefitName.Text = ""
                btnSimpan.Enabled = True
                btnCari.Enabled = True
                ViewState("typeForm") = "Insert"
                'SesMode = enumMode.Mode.NewItemMode
                'dgTable.Visible = True
                dgTable.SelectedIndex = -1
                BindDataGrid(dgTable.CurrentPageIndex)
                VisibleForm(1)

        End Select
    End Sub

    Private Sub dgTable_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTable.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As BenefitType = CType(e.Item.DataItem, BenefitType)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)
                '  Dim lnkbtnPopUp As LinkButton = CType(e.Item.FindControl("lnkbtnPopUp"), LinkButton)
                '  lnkbtnPopUp.Attributes("OnClick") = "showPopUp('../PopUp/PopUpAlertSound.aspx?ID=" & objAlert2.ID & "','',500,760,'');"

                Dim lblBenefitName As Label = CType(e.Item.FindControl("lblBenefitName"), Label)
                lblBenefitName.Text = objDomain2.Name

                Dim cbEventGrid As CheckBox = CType(e.Item.FindControl("cbEventGrid"), CheckBox)
                If objDomain2.EventValidation = 1 Then
                    cbEventGrid.Checked = True
                Else
                    cbEventGrid.Checked = False
                End If

                Dim cbLeassingGrid As CheckBox = CType(e.Item.FindControl("cbLeassingGrid"), CheckBox)
                If objDomain2.LeasingBox = 1 Then
                    cbLeassingGrid.Checked = True
                Else
                    cbLeassingGrid.Checked = False
                End If
                Dim cbAssyGrid As CheckBox = CType(e.Item.FindControl("cbAssyGrid"), CheckBox)
                If objDomain2.AssyYearBox = 1 Then
                    cbAssyGrid.Checked = True
                Else
                    cbAssyGrid.Checked = False
                End If
                Dim cbReciptGrid As CheckBox = CType(e.Item.FindControl("cbReciptGrid"), CheckBox)
                If objDomain2.ReceiptBox = 1 Then
                    cbReciptGrid.Checked = True
                Else
                    cbReciptGrid.Checked = False
                End If

                Dim cbDiskonGrid As CheckBox = CType(e.Item.FindControl("cbDiskonGrid"), CheckBox)
                If objDomain2.WSDiscount = 1 Then
                    cbDiskonGrid.Checked = True
                Else
                    cbDiskonGrid.Checked = False
                End If

                Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)
                Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
                Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
                'add privilige
                'lnkbtnView.Visible = bCekDetailPriv
                lnkbtnEdit.Visible = inputtipebenefit_privillage
                lnkbtnDelete.Visible = inputtipebenefit_privillage
            End If
            End If
    End Sub

    Private Sub dgTable_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgTable.PageIndexChanged
        If ViewState("typeForm") = "Edit" OrElse ViewState("typeForm") = "View" Then Return

        dgTable.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgTable.CurrentPageIndex)
        setColorRow()
        dgTable.SelectedIndex = -1
    End Sub

    Private Sub setColorRow()
        If ViewState("typeForm") = "Edit" OrElse ViewState("typeForm") = "View" Then
            Dim GridItem As DataGridItem
            For intRow As Integer = 0 To dgTable.Items.Count - 1
                GridItem = dgTable.Items(intRow)
                Dim lblNo As Label = GridItem.FindControl("lblNo")
                If ViewState("_NoBaris") = lblNo.Text Then
                    dgTable.SelectedIndex = GridItem.ItemIndex
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub dgTable_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgTable.SortCommand
        If ViewState("typeForm") = "Edit" OrElse ViewState("typeForm") = "View" Then Return

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


        If txtBenefitName.Text = "" Then
            MessageBox.Show("Isi Nama benefit")
            Return

        End If


        Dim objDomain As BenefitType
        objDomain = New BenefitType

        objDomain.Name = txtBenefitName.Text

        If cbAssy.Checked = False Then
            objDomain.AssyYearBox = 0
        Else
            objDomain.AssyYearBox = 1
        End If

        If cbLeasing.Checked = False Then
            objDomain.LeasingBox = 0
        Else
            objDomain.LeasingBox = 1
        End If

        If cbReceipt.Checked = False Then
            objDomain.ReceiptBox = 0
        Else
            objDomain.ReceiptBox = 1
        End If

        If cbEvent.Checked = False Then
            objDomain.EventValidation = 0
        Else
            objDomain.EventValidation = 1
        End If

        If cbDiskon.Checked = False Then
            objDomain.WSDiscount = 0
        Else
            objDomain.WSDiscount = 1
        End If

        objDomain.Status = 0
        objDomain.RowStatus = 0

        Dim result As Integer
        Dim facade As New BenefitTypeFacade(User)


        If ViewState("typeForm") = "Insert" Then
            result = facade.Insert(objDomain)
        Else
            objDomain.ID = hfID.Value
            result = facade.Update(objDomain)
        End If



        If result <> -1 Then

            'BindDataGrid(dgTable.CurrentPageIndex)
            btnBatal_Click(sender, e)
            'dgTable.SelectedIndex = -1
            MessageBox.Show(SR.SaveSuccess)
        Else
            MessageBox.Show(SR.SaveFail)
        End If


    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dgTable.CurrentPageIndex = 0
        BindDataGrid(dgTable.CurrentPageIndex)

        'Dim _arrList As New ArrayList
        'Dim totalRow As Integer = 0

        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'criterias.opAnd(New Criteria(GetType(BenefitType), "Name", MatchType.Partial, txtBenefitName.Text))

        'Dim strSql As String = ""

        'If cbAssy.Checked = True Then

        '    criterias.opAnd(New Criteria(GetType(BenefitType), "AssyYearBox", MatchType.Exact, "1"))
        'End If

        'If cbLeasing.Checked = True Then

        '    criterias.opAnd(New Criteria(GetType(BenefitType), "LeasingBox", MatchType.Exact, "1"))
        'End If

        'If cbReceipt.Checked = True Then

        '    criterias.opAnd(New Criteria(GetType(BenefitType), "ReceiptBox", MatchType.Exact, "1"))
        'End If

        'If cbEvent.Checked = True Then

        '    criterias.opAnd(New Criteria(GetType(BenefitType), "EventValidation", MatchType.Exact, "1"))
        'End If

        'If cbDiskon.Checked = True Then

        '    criterias.opAnd(New Criteria(GetType(BenefitType), "WSDiscount", MatchType.Exact, "1"))
        'End If



        '_arrList = New BenefitTypeFacade(User).RetrieveByCriteria(criterias, dgTable.CurrentPageIndex + 1, dgTable.PageSize,
        '                                                                   totalRow)
        'dgTable.VirtualItemCount = totalRow
        '' _arrList = New BenefitTypeFacade(User).RetrieveActiveList()
        'dgTable.DataSource = _arrList

        'dgTable.DataBind()


        btnSimpan.Enabled = False
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        cbLeasing.Checked = False
        cbAssy.Checked = cbLeasing.Checked
        cbReceipt.Checked = cbLeasing.Checked
        cbEvent.Checked = cbLeasing.Checked
        cbDiskon.Checked = cbLeasing.Checked
        txtBenefitName.Text = ""
        btnSimpan.Enabled = True
        btnCari.Enabled = True
        ViewState("typeForm") = "Insert"
        'SesMode = enumMode.Mode.NewItemMode
        'dgTable.Visible = True
        dgTable.SelectedIndex = -1
        dgTable.CurrentPageIndex = 0
        BindDataGrid(dgTable.CurrentPageIndex)
        VisibleForm(1)
        ViewState("_NoBaris") = 0
    End Sub

    Private Sub btnTambah_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTambah.Click
        cbLeasing.Checked = False
        cbAssy.Checked = cbLeasing.Checked
        cbReceipt.Checked = cbLeasing.Checked
        cbEvent.Checked = cbLeasing.Checked
        cbDiskon.Checked = cbLeasing.Checked
        txtBenefitName.Text = ""
        VisibleForm(1)
        ViewState("typeForm") = "Insert"
    End Sub

    Private Sub VisibleForm(ByVal tipe As Integer)
        If tipe = 1 Then 'tipe insert / edit
            'formBenefitType.Visible = True
            ' formGrid.Visible = False
            txtBenefitName.Enabled = True
            cbLeasing.Enabled = txtBenefitName.Enabled
            cbAssy.Enabled = txtBenefitName.Enabled
            cbReceipt.Enabled = txtBenefitName.Enabled
            cbEvent.Enabled = txtBenefitName.Enabled
            cbDiskon.Enabled = txtBenefitName.Enabled
        Else 'tipe view
            ' formBenefitType.Visible = False
            ' formGrid.Visible = True
            txtBenefitName.Enabled = False
            cbLeasing.Enabled = txtBenefitName.Enabled
            cbAssy.Enabled = txtBenefitName.Enabled
            cbReceipt.Enabled = txtBenefitName.Enabled
            cbEvent.Enabled = txtBenefitName.Enabled
            cbDiskon.Enabled = txtBenefitName.Enabled
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Short)
        Dim objDomain As BenefitType = New BenefitTypeFacade(User).Retrieve(nID)
        'Todo session
        If IsNothing(objDomain) Then
            txtBenefitName.Text = ""
            cbAssy.Checked = False
            cbLeasing.Checked = cbAssy.Checked
            cbReceipt.Checked = cbAssy.Checked
            cbEvent.Checked = cbAssy.Checked
            cbDiskon.Checked = cbAssy.Checked
            hfID.Value = ""
        Else
            txtBenefitName.Text = objDomain.Name
            hfID.Value = objDomain.ID
            If objDomain.AssyYearBox = 0 Then
                cbAssy.Checked = False
            Else
                cbAssy.Checked = True
            End If

            If objDomain.LeasingBox = 0 Then
                cbLeasing.Checked = False
            Else
                cbLeasing.Checked = True
            End If

            If objDomain.ReceiptBox = 0 Then
                cbReceipt.Checked = False
            Else
                cbReceipt.Checked = True
            End If

            If objDomain.EventValidation = 0 Then
                cbEvent.Checked = False
            Else
                cbEvent.Checked = True
            End If

            If objDomain.WSDiscount = 0 Then
                cbDiskon.Checked = False
            Else
                cbDiskon.Checked = True
            End If

            
        End If


    End Sub

End Class
