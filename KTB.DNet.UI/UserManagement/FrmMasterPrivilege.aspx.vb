#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search

#End Region

Public Class FrmMasterPrivilege
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtHakAkses As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents ddlHakAkses As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgPrivilege As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtdescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescSearch As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Declaration"
    Private objPrivilege As Privilege
    Private arlprivilege As ArrayList
    Private companyCode As String
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "Name"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Function DefaultCriteria() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Privilege), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(Privilege), "Title", MatchType.Exact, ddlHakAkses.SelectedValue))

        If txtDescSearch.Text <> "" Then
            Dim strDesc() As String = txtDescSearch.Text.Trim.Split(";")
            For i As Integer = 0 To strDesc.Length - 1
                criterias.opAnd(New Criteria(GetType(Privilege), "Description", MatchType.[Partial], strDesc(i)))
            Next
        End If
        Return criterias
    End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            dtgPrivilege.DataSource = New PrivilegeFacade(User).RetrieveActiveList(DefaultCriteria(), indexPage + 1, dtgPrivilege.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgPrivilege.VirtualItemCount = totalRow
            dtgPrivilege.DataBind()
        End If
    End Sub

    Private Sub ClearData()
        txtHakAkses.Text = String.Empty
        txtdescription.Text = String.Empty
        btnSimpan.Enabled = True
        txtHakAkses.ReadOnly = False
        txtdescription.ReadOnly = False
        ddlHakAkses.Enabled = True
        If dtgPrivilege.Items.Count > 0 Then
            dtgPrivilege.SelectedIndex = -1
        End If
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub UpdateArea()
        Dim objPrivilege As Privilege = CType(Session.Item("vsPrivilege"), Privilege)
        If objPrivilege.Title = ddlHakAkses.SelectedValue Then
            objPrivilege.Description = txtdescription.Text
            Dim nResult = New PrivilegeFacade(User).Update(objPrivilege)
        Else
            Dim facade As PrivilegeFacade = New PrivilegeFacade(User)
            objPrivilege.Description = txtdescription.Text
            objPrivilege.Title = ddlHakAkses.SelectedValue
            Dim nReturn As Integer = -1
            nReturn = facade.UpdateTransaction(objPrivilege)
            If nReturn <= 0 Then
                MessageBox.Show("Record Gagal Diupdate")
            End If
        End If


    End Sub

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal Privilege As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "Privilege.ID", MatchType.Exact, Privilege))
        Return criterias
    End Function

    Private Sub DeleteArea(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0
        If New HelperFacade(User, GetType(OrganizationPrivilege)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(OrganizationPrivilege), nID), _
            CreateAggreateForCheckRecord(GetType(OrganizationPrivilege))) Then
            iRecordCount = iRecordCount + 1
        End If

        If iRecordCount > 0 Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objPrivilege As Privilege = New PrivilegeFacade(User).Retrieve(nID)
            Dim facade As PrivilegeFacade = New PrivilegeFacade(User)
            Dim iReturn As Integer = -1
            'iReturn = facade.Delete(objPrivilege)
            iReturn = facade.DeleteTransaction(objPrivilege)
            If iReturn <= 0 Then
                MessageBox.Show("Record Gagal Dihapus")
                'MessageBox.Show("Data sudah digunakan. Data ini tidak dapat dihapus!")
            End If

            dtgPrivilege.CurrentPageIndex = 0
            BindDataGrid(dtgPrivilege.CurrentPageIndex)
        End If
    End Sub

    Private Sub ViewArea(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objPrivilege As Privilege = New PrivilegeFacade(User).Retrieve(nID)
        Session.Add("vsPrivilege", objPrivilege)
        txtHakAkses.Text = objPrivilege.Name
        txtdescription.Text = objPrivilege.Description
        ddlHakAkses.SelectedValue = objPrivilege.Title
        Me.btnSimpan.Enabled = EditStatus
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            companyCode = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            If Not SecurityProvider.Authorize(Context.User, SR.AdminViewListHakAkses_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Master User Privilege")
            End If
            BindToDropDownlist()
            BindDataGrid(0)
            InitiatePage()
        End If
    End Sub

    Private Sub BindToDropDownlist()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        ddlHakAkses.DataSource = EnumDealerTittle.RetrieveTitleForPrivilege(companyCode)
        ddlHakAkses.DataTextField = "NameTitle"
        ddlHakAkses.DataValueField = "ValTitle"
        ddlHakAkses.DataBind()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objPrivilege As Privilege = New Privilege
        Dim objPrivilegeFacade As PrivilegeFacade = New PrivilegeFacade(User)
        Dim nResult As Integer = -1
        txtHakAkses.ReadOnly = False
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtHakAkses.Text = String.Empty Then
                If objPrivilegeFacade.ValidateCode(txtHakAkses.Text) <= 0 Then
                    objPrivilege.Name = txtHakAkses.Text.Trim
                    objPrivilege.Description = txtdescription.Text.Trim
                    objPrivilege.Title = ddlHakAkses.SelectedValue
                    nResult = New PrivilegeFacade(User).Insert(objPrivilege)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Privilege"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Privilege"))
            End If
        Else
            UpdateArea()
        End If

        ClearData()
        dtgPrivilege.CurrentPageIndex = 0
        BindDataGrid(dtgPrivilege.CurrentPageIndex)
    End Sub

    Private Sub dtgPrivilege_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPrivilege.ItemDataBound
        Dim RowValue As Privilege = CType(e.Item.DataItem, Privilege)

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            e.Item.Cells(3).Text = EnumDealerTittle.DealerTitleGetStringValue(CType(RowValue.Title, String), companyCode)
            'If RowValue.Title = "0" Then
            '    e.Item.Cells(3).Text = "DEALER"
            'ElseIf RowValue.Title = "1" Then
            '    e.Item.Cells(3).Text = "KTB"
            'ElseIf RowValue.Title = "2" Then
            '    e.Item.Cells(3).Text = "KTB DEALER"
            'ElseIf RowValue.Title = "3" Then
            '    e.Item.Cells(3).Text = "LEASING"
            'ElseIf RowValue.Title = "4" Then
            '    e.Item.Cells(3).Text = "KTB LEASING"
            'ElseIf RowValue.Title = "5" Then
            '    e.Item.Cells(3).Text = "DEALER LEASING"
            'ElseIf RowValue.Title = "6" Then
            '    e.Item.Cells(3).Text = "KTB DEALER LEASING"
            'End If
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgPrivilege.CurrentPageIndex * dtgPrivilege.PageSize)
        End If

    End Sub

    Private Sub dtgPrivilege_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPrivilege.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewArea(e.Item.Cells(0).Text, False)
            txtHakAkses.ReadOnly = True
            txtdescription.ReadOnly = True
            ddlHakAkses.Enabled = False
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewArea(e.Item.Cells(0).Text, True)
            dtgPrivilege.SelectedIndex = e.Item.ItemIndex
            txtHakAkses.ReadOnly = True
            txtdescription.ReadOnly = False
            ddlHakAkses.Enabled = True
        ElseIf e.CommandName = "Delete" Then
            DeleteArea(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub

    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewState.Clear()
        Response.Redirect("../default.aspx")
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        txtHakAkses.ReadOnly = False
    End Sub

    Private Sub dtgPrivilege_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPrivilege.SortCommand
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

        dtgPrivilege.SelectedIndex = -1
        dtgPrivilege.CurrentPageIndex = 0
        BindDataGrid(dtgPrivilege.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgPrivilege_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPrivilege.PageIndexChanged
        dtgPrivilege.SelectedIndex = -1
        dtgPrivilege.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPrivilege.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgPrivilege.CurrentPageIndex = 0
        BindDataGrid(dtgPrivilege.CurrentPageIndex)
        ClearData()
    End Sub
#End Region

  
End Class
