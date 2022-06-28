Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper

Public Class FrmMasterLeasingList
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim _sHelper As New SessionHelper
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgTable As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtProvince As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCity As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAlamat As System.Web.UI.WebControls.TextBox
    Protected WithEvents hfID As System.Web.UI.WebControls.TextBox

    Protected WithEvents formLeasing As System.Web.UI.WebControls.Panel
    Protected WithEvents formGrid As System.Web.UI.WebControls.Panel

    Protected WithEvents btnCari As System.Web.UI.WebControls.Button


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
    Private objDomain As Leasing = New Leasing
    Private objFacade As LeasingFacade = New LeasingFacade(User)

#Region "Private Property"


#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.Sales_Umum_Master_Leasing_Lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Master Leasing")
        End If
    End Sub

#End Region




    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then

            ViewState("currentSortColumn") = "LeasingCode"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC


            dgTable.CurrentPageIndex = 0
            BindDataGrid(dgTable.CurrentPageIndex)
        End If
    End Sub



    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click

        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Leasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtCode.Text.Trim <> "" Then criterias.opAnd(New Criteria(GetType(Leasing), "LeasingCode", MatchType.Partial, txtCode.Text.Trim))
        If txtName.Text.Trim <> "" Then criterias.opAnd(New Criteria(GetType(Leasing), "LeasingName", MatchType.Partial, txtName.Text.Trim))
        If txtProvince.Text.Trim <> "" Then criterias.opAnd(New Criteria(GetType(Leasing), "Province", MatchType.Partial, txtProvince.Text.Trim))
        If txtCity.Text.Trim <> "" Then criterias.opAnd(New Criteria(GetType(Leasing), "City", MatchType.Partial, txtCity.Text.Trim))
        If txtAlamat.Text.Trim <> "" Then criterias.opAnd(New Criteria(GetType(Leasing), "Alamat", MatchType.Partial, txtAlamat.Text.Trim))

        _arrList = New LeasingFacade(User).RetrieveByCriteria(criterias, dgTable.CurrentPageIndex + 1, dgTable.PageSize, totalRow)
        dgTable.VirtualItemCount = totalRow
        ' _arrList = New BenefitTypeFacade(User).RetrieveActiveList()
        dgTable.DataSource = _arrList
        dgTable.DataBind()

    End Sub

    Private Sub BindDataGrid(ByVal index As Integer)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Leasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        _arrList = New LeasingFacade(User).RetrieveByCriteria(criterias, dgTable.CurrentPageIndex + 1, dgTable.PageSize,
                                                                           totalRow)
        dgTable.VirtualItemCount = totalRow
        '_arrList = New LeasingFacade(User).RetrieveActiveList()
        dgTable.DataSource = _arrList
        'dgTable.VirtualItemCount = totalRow
        dgTable.DataBind()
    End Sub

    Private Sub dgTable_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTable.ItemCommand
        Select Case e.CommandName
            Case "View"
                Response.Redirect("frmMasterLeasingDetail.aspx?id=" & e.Item.Cells(0).Text)
            Case "Edit"
                btnCari.Enabled = False
                ViewState("typeForm") = "Edit"
                ViewModel(CShort(e.CommandArgument))
                dgTable.SelectedIndex = e.Item.ItemIndex
                VisibleForm(2)
        End Select
    End Sub

    Private Sub dgTable_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTable.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As Leasing = CType(e.Item.DataItem, Leasing)


            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            'lblNo.Text = objDomain2.ID.ToString
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)).ToString()


            Dim lblCode As Label = CType(e.Item.FindControl("lblCode"), Label)
            lblCode.Text = objDomain2.LeasingCode

            Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)
            lblName.Text = objDomain2.LeasingName

            Dim lblProvince As Label = CType(e.Item.FindControl("lblProvince"), Label)
            lblProvince.Text = objDomain2.Province

            Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
            lblCity.Text = objDomain2.City

            Dim lblAlamat As Label = CType(e.Item.FindControl("lblAlamat"), Label)
            lblAlamat.Text = objDomain2.Alamat

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            If objDomain2.RowStatus = 0 AndAlso objDomain2.Status = 1 Then
                lblStatus.Text = "Aktif"
            Else
                lblStatus.Text = "Tidak Aktif"
            End If

            Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)
            'add privilige
            'lnkbtnView.Visible = bCekDetailPriv

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


    Private Sub VisibleForm(ByVal tipe As Integer)
        If tipe = 1 Then 'tipe search
            txtCode.Enabled = True
            txtName.Enabled = True
            txtProvince.Enabled = txtName.Enabled
            txtCity.Enabled = txtName.Enabled
            txtAlamat.Enabled = txtName.Enabled
        ElseIf tipe = 2 Then 'tipe edit
            'txtRevisionName.Enabled = False
            'txtAmount.Enabled = txtRevisionName.Enabled
            'icStartDate.Enabled = txtRevisionName.Enabled
            'icEndDate.Enabled = txtRevisionName.Enabled
            'ddlstatus.Enabled = Not txtRevisionName.Enabled
        Else 'tipe view
            txtCode.Enabled = False
            txtName.Enabled = False
            txtProvince.Enabled = txtName.Enabled
            txtCity.Enabled = txtName.Enabled
            txtAlamat.Enabled = txtName.Enabled
        End If
    End Sub


    Private Sub ViewModel(ByVal nID As Short)
        Dim objDomain As Leasing = New LeasingFacade(User).Retrieve(nID)
        'Todo session
        If IsNothing(objDomain) Then
            SetEmptyForm()
        Else
            txtCode.Text = objDomain.LeasingCode
            txtName.Text = objDomain.LeasingName
            hfID.Text = objDomain.ID.ToString
            txtProvince.Text = objDomain.Province
            txtCity.Text = objDomain.City
            txtAlamat.Text = objDomain.Alamat
        End If
    End Sub


    Private Sub SetEmptyForm()
        txtCode.Text = ""
        txtName.Text = ""
        txtProvince.Text = ""
        txtCity.Text = ""
        txtAlamat.Text = ""
    End Sub


    Protected Sub dgTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgTable.SelectedIndexChanged

    End Sub
End Class
