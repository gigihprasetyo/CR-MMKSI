#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmMasterCode
    Inherits System.Web.UI.Page
    Private m_bFormPrivilege As Boolean = False
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgCategory As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ValidateCode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValidateDescription As System.Web.UI.WebControls.RequiredFieldValidator
    'Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _edit As Boolean
    Private _view As Boolean
    Dim arrSPKMasterList As New ArrayList
    Private SPKMasterFacade As New SPKMasterCountryCodePhoneFacade(User)
    Private sessHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "CountryName"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgCategory.DataSource = New SPKMasterCountryCodePhoneFacade(User).RetrieveActiveList(indexPage + 1, dtgCategory.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgCategory.VirtualItemCount = totalRow
            dtgCategory.DataBind()
        End If
    End Sub

    Private Sub BindDatagridCari(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKMasterCountryCodePhone), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtCode.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SPKMasterCountryCodePhone), "CountryCode", MatchType.[Partial], txtCode.Text.Trim()))
        End If
        If txtDescription.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SPKMasterCountryCodePhone), "CountryName", MatchType.[Partial], txtDescription.Text.Trim()))
        End If
        
        arrSPKMasterList = SPKMasterFacade.RetrieveByCriteria(criterias, indexPage + 1, dtgCategory.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If arrSPKMasterList.Count > 0 Then
            dtgCategory.CurrentPageIndex = indexPage
            dtgCategory.DataSource = arrSPKMasterList
            dtgCategory.VirtualItemCount = totalRow
            dtgCategory.DataBind()
        Else
            MessageBox.Show(SR.DataNotFound("Kode Negara"))
        End If
    End Sub

    Private Sub InsertCategory()
        Dim objSPKMasterCountryCodePhoneFacade As SPKMasterCountryCodePhoneFacade = New SPKMasterCountryCodePhoneFacade(User)
        Dim objCategory As SPKMasterCountryCodePhone = New SPKMasterCountryCodePhone
        Dim nResult As Integer

        If objSPKMasterCountryCodePhoneFacade.ValidateCode(txtCode.Text) = 0 Then
            objCategory.CountryCode = txtCode.Text
            objCategory.CountryName = txtDescription.Text
            nResult = New SPKMasterCountryCodePhoneFacade(User).Insert(objCategory)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)

            End If
        Else
            MessageBox.Show(SR.DataIsExist("Kode Negara"))
        End If

    End Sub

    Private Sub UpdateCategory()
        Dim objCategory As SPKMasterCountryCodePhone = CType(Session.Item("vsCategory"), SPKMasterCountryCodePhone)
        objCategory.CountryCode = txtCode.Text
        objCategory.CountryName = txtDescription.Text
        Dim nResult = New SPKMasterCountryCodePhoneFacade(User).Update(objCategory)
        
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal CategoryID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "Category", MatchType.Exact, CategoryID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteCategory(ByVal nID As Integer)

        Dim objSPKMasterCountryCodePhone As SPKMasterCountryCodePhone = New SPKMasterCountryCodePhoneFacade(User).Retrieve(nID)

        Dim facade As SPKMasterCountryCodePhoneFacade = New SPKMasterCountryCodePhoneFacade(User)

        facade.DeleteFromDB(objSPKMasterCountryCodePhone)

        dtgCategory.CurrentPageIndex = 0
        BindDatagrid(dtgCategory.CurrentPageIndex)

    End Sub

    Private Sub ViewCategory(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objSPKMasterCountryCodePhone As SPKMasterCountryCodePhone = New SPKMasterCountryCodePhoneFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsCategory", objSPKMasterCountryCodePhone)

        txtCode.Text = objSPKMasterCountryCodePhone.CountryCode
        txtDescription.Text = objSPKMasterCountryCodePhone.CountryName

        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub ValidateData()
        ValidateCode.IsValid = True
        ValidateDescription.IsValid = True
        ValidateCode.Validate()
        ValidateDescription.Validate()
    End Sub

    Private Sub ClearData()
        txtCode.Text = String.Empty
        txtDescription.Text = String.Empty
        btnSimpan.Enabled = True
        btnCari.Enabled = True
        dtgCategory.SelectedIndex = -1
        
        ViewState.Add("vsProcess", "Insert")
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            'GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory)
            BindDatagrid(0)
            InitiatePage()
        End If
        'Put user code to initialize the page here
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
        btnCari.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ChangeCategory_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ViewCategory_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Kategori")
        End If

    End Sub



    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        txtCode.ReadOnly = False
        txtDescription.ReadOnly = False
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertCategory()
        Else
            UpdateCategory()
            MessageBox.Show("Ubah Sukses")
        End If

        ClearData()
        dtgCategory.CurrentPageIndex = 0
        BindDatagrid(dtgCategory.CurrentPageIndex)
    End Sub

    Private Sub dtgCategory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCategory.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCategory.CurrentPageIndex * dtgCategory.PageSize)
            'CType(e.Item.FindControl("lblProductCategory"), Label).Text = CType(e.Item.DataItem, Category).ProductCategory.Code
        End If

        'tambahan privilege
        If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
        End If
    End Sub

    Private Sub dtgCategory_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgCategory.ItemCommand
        If (e.CommandName = "View") Then
            txtCode.ReadOnly = True
            txtDescription.ReadOnly = True
            ViewState.Add("vsProcess", "View")
            ViewCategory(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            txtCode.ReadOnly = True
            txtDescription.ReadOnly = False
            ViewState.Add("vsProcess", "Edit")
            ViewCategory(e.Item.Cells(0).Text, True)
            dtgCategory.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            DeleteCategory(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewState.Clear()
        Response.Redirect("../default.aspx")
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtCode.ReadOnly = False
        txtDescription.ReadOnly = False
        ClearData()
    End Sub

    Private Sub dtgCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgCategory.SelectedIndexChanged

    End Sub

    Private Sub dtgCategory_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCategory.SortCommand
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

        dtgCategory.CurrentPageIndex = 0
        BindDatagrid(dtgCategory.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgCategory_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCategory.PageIndexChanged
        dtgCategory.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgCategory.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        BindDatagridCari(0)
    End Sub
End Class