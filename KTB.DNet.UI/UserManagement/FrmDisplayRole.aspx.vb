Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI

Public Class FrmDisplayRole
    Inherits System.Web.UI.Page
    Dim sHRole As SessionHelper = New SessionHelper
    Private objDealer As Dealer

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtGroupCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGroupName As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents lblOrgCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrgName As System.Web.UI.WebControls.Label
    Protected WithEvents dtgRole As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents cfRole As FilterCompositeControl.CompositeFilter

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim bEditPrivilege As Boolean = False

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            assignDealer()
            AssignAttributeControl()
            If Not IsNothing(sHRole.GetSession("roleDealer")) Then
                dtgRole.CurrentPageIndex = 0
                BindDataGrid(0)
            End If
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not IsNothing(sHRole.GetSession("DEALER")) Then
            Dim objDealer As Dealer = CType(sHRole.GetSession("DEALER"), Dealer)
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If Not SecurityProvider.Authorize(Context.User, SR.AdminViewListRoleKTB_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Daftar Role")
                Else
                    bEditPrivilege = SecurityProvider.Authorize(Context.User, SR.AdminUpdateRoleKTB_Privilege)
                End If
                Return
            ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.DEALER OrElse objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                If Not SecurityProvider.Authorize(Context.User, SR.AdminViewListRoleDealer_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Daftar Role")
                Else
                    bEditPrivilege = SecurityProvider.Authorize(Context.User, SR.AdminUpdateRoleDealer_Privilege)
                End If
                Return
            End If
        End If
        Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Daftar Role")
    End Sub

    Private Sub AssignAttributeControl()
        Dim lblPopUp As Label = CType(Page.FindControl("lblPopUpDealer"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection")
    End Sub
    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub assignDealer()
        If Not sHRole.GetSession("DEALER") Is Nothing Then
            Dim arDeal As ArrayList = New ArrayList
            Dim objDeal As Dealer = CType(sHRole.GetSession("DEALER"), Dealer)
            If Not objDeal Is Nothing Then
                lblOrgCode.Text = objDeal.DealerCode & " / " & objDeal.SearchTerm1
                lblOrgName.Text = objDeal.DealerName
                If objDeal.Title = "0" Then
                    lblPopUpDealer.Visible = False
                    txtKodeDealer.Visible = False
                    lblDealer.Visible = False
                    'btnCari.Visible = False
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Role), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Role), "Dealer.ID", MatchType.Exact, objDeal.ID))
                    sHRole.SetSession("roleDealer", criterias)
                    dtgRole.Columns(2).Visible = False
                    dtgRole.Columns(2).SortExpression = ""
                    dtgRole.CurrentPageIndex = 0
                    BindDataGrid(0)
                ElseIf objDeal.Title = "1" Then
                    lblPopUpDealer.Visible = True
                    txtKodeDealer.Visible = True
                    lblDealer.Visible = True
                    'btnCari.Visible = True
                End If
            End If
        End If
    End Sub
    Private Function ConvertKodeDealer(ByVal sKodeDealerColl As String)
        Dim sKodeDealerTemp() As String = sKodeDealerColl.Split(New Char() {";"})
        Dim sKodeDealer As String = ""
        For i As Integer = 0 To sKodeDealerTemp.Length - 1
            sKodeDealer = sKodeDealer & "'" & sKodeDealerTemp(i).Trim() & "'"

            If Not (i = sKodeDealerTemp.Length - 1) Then
                sKodeDealer = sKodeDealer & ","
            End If
        Next
        sKodeDealer = "(" & sKodeDealer & ")"
        Return sKodeDealer
    End Function
    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim dt As ArrayList
            Try
                dt = New RoleFacade(User).RetrieveActiveList(CType(sHRole.GetSession("roleDealer"), CriteriaComposite), indexPage + 1, dtgRole.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            Catch
                MessageBox.Show("Harap periksa kembali kategori pencarian anda")
                dt = New ArrayList
                totalRow = dt.Count
            End Try
            dtgRole.DataSource = dt
            dtgRole.VirtualItemCount = totalRow
            dtgRole.DataBind()
        End If
    End Sub


    Private Sub dtgRole_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgRole.SortCommand
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

        dtgRole.SelectedIndex = -1
        dtgRole.CurrentPageIndex = 0
        BindDataGrid(dtgRole.CurrentPageIndex)

    End Sub

    Private Sub dtgRole_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgRole.PageIndexChanged
        dtgRole.SelectedIndex = -1
        dtgRole.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgRole.CurrentPageIndex)
    End Sub


    Private Sub dtgRole_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgRole.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim rowvalue2 As Role = CType(e.Item.DataItem, Role)

            Dim lblKodeOrgs As Label = CType(e.Item.FindControl("lblKodeOrgs"), Label)
            If Not IsNothing(rowvalue2.Dealer.DealerCode) Then
                lblKodeOrgs.Text = rowvalue2.Dealer.DealerCode
            End If

            Dim lblstatus As Label = CType(e.Item.FindControl("lblstatus"), Label)
            If Not IsNothing(rowvalue2.RoleStatus) Then
                If rowvalue2.RoleStatus = False Then
                    lblstatus.Text = "Non aktif"
                Else
                    lblstatus.Text = "Aktif"
                End If
            End If

            Dim lblTgl As Label = CType(e.Item.FindControl("lblTgl"), Label)
            If Not IsNothing(rowvalue2.LastUpdateTime) Then
                lblTgl.Text = rowvalue2.LastUpdateTime.ToString("dd/MM/yyyy")
            End If

            Dim lblUpdateBy As Label = CType(e.Item.FindControl("lblUpdateBy"), Label)
            If Not IsNothing(rowvalue2.LastUpdateBy) Then
                If rowvalue2.LastUpdateBy = "" Then
                    lblUpdateBy.Text = rowvalue2.CreatedBy
                Else
                    lblUpdateBy.Text = rowvalue2.LastUpdateBy
                End If

            End If

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgRole.CurrentPageIndex * dtgRole.PageSize)

            Dim btnUbah As LinkButton = CType(e.Item.FindControl("btnUbah"), LinkButton)
            btnUbah.Visible = bEditPrivilege
        End If
    End Sub

    Private Sub dtgRole_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgRole.ItemCommand
        sHRole.SetSession("backURL", "./FrmDisplayRole.aspx")
        If (e.CommandName = "Edit") Then
            sHRole.SetSession("vsProcess", "Edit")
            editRole(e.Item.Cells(0).Text, False)
            If Not IsNothing(sHRole.GetSession("editRole")) Then
                Response.Redirect("frmEditViewRole.aspx?backURL=./FrmDisplayRole.aspx")
            Else
                dtgRole.CurrentPageIndex = 0
                BindDataGrid(dtgRole.CurrentPageIndex)
            End If
        ElseIf (e.CommandName = "View") Then
            sHRole.SetSession("vsProcess", "View")
            editRole(e.Item.Cells(0).Text, False)
            If Not IsNothing(sHRole.GetSession("editRole")) Then
                Response.Redirect("frmEditViewRole.aspx?backURL=./FrmDisplayRole.aspx")
            Else
                dtgRole.CurrentPageIndex = 0
                BindDataGrid(dtgRole.CurrentPageIndex)
            End If
        End If
    End Sub

    Private Sub editRole(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objRole As Role = New RoleFacade(User).Retrieve(nID)
        If Not IsNothing(objRole) And objRole.ID > 0 Then
            sHRole.SetSession("editRole", objRole)
        Else
            sHRole.RemoveSession("editRole")
        End If
    End Sub

    Private Sub Cari()
        Dim crit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Role), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(crit)
        sHRole.SetSession("roleDealer", crit)
        dtgRole.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub

    Private Sub CreateCriteria(ByVal crit As CriteriaComposite)
        If txtKodeDealer.Visible Then
            objDealer = Session("DEALER")
            If CType(objDealer.Title, Short) <> CType(EnumDealerTittle.DealerTittle.KTB, Short) Then
                crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Role), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            End If
            If txtKodeDealer.Text.Trim() <> String.Empty Then
                crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Role), "Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
            End If
        Else
            Dim objDeal As Dealer = CType(sHRole.GetSession("DEALER"), Dealer)
            crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Role), "Dealer.ID", MatchType.Exact, objDeal.ID))
        End If
        If cfRole.ColumnName <> "ALL" Then
            Dim myArray() As String = Split(cfRole.KeyWord, ";")
            If myArray.Length > 1 Then
                If cfRole.ColumnName = dtgRole.Columns(7).SortExpression Then
                    For i As Integer = 0 To myArray.Length - 1
                        Select Case (myArray(i).ToUpper)
                            Case "AKTIF"
                                If i = 0 Then
                                    crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Role), cfRole.ColumnName, cfRole.OperatorName, 1), "(", True)
                                ElseIf i = myArray.Length - 1 Then
                                    crit.opOr(New Criteria(GetType(KTB.DNet.Domain.Role), cfRole.ColumnName, cfRole.OperatorName, 1), ")", False)
                                Else
                                    crit.opOr(New Criteria(GetType(KTB.DNet.Domain.Role), cfRole.ColumnName, cfRole.OperatorName, 1))
                                End If
                            Case "NON AKTIF"
                                If i = 0 Then
                                    crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Role), cfRole.ColumnName, cfRole.OperatorName, 0), "(", True)
                                ElseIf i = myArray.Length - 1 Then
                                    crit.opOr(New Criteria(GetType(KTB.DNet.Domain.Role), cfRole.ColumnName, cfRole.OperatorName, 0), ")", False)
                                Else
                                    crit.opOr(New Criteria(GetType(KTB.DNet.Domain.Role), cfRole.ColumnName, cfRole.OperatorName, 0))
                                End If
                        End Select
                    Next
                Else
                    For i As Integer = 0 To myArray.Length - 1
                        If i = 0 Then
                            crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Role), cfRole.ColumnName, cfRole.OperatorName, myArray(i)), "(", 1)
                        ElseIf i = myArray.Length - 1 Then
                            crit.opOr(New Criteria(GetType(KTB.DNet.Domain.Role), cfRole.ColumnName, cfRole.OperatorName, myArray(i)), ")", 0)
                        Else
                            crit.opOr(New Criteria(GetType(KTB.DNet.Domain.Role), cfRole.ColumnName, cfRole.OperatorName, myArray(i)))
                        End If
                    Next
                End If
            Else
                If cfRole.ColumnName = dtgRole.Columns(7).SortExpression Then
                    Select Case (myArray(0).ToUpper)
                        Case "AKTIF"
                            crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Role), cfRole.ColumnName, cfRole.OperatorName, 1))
                        Case "NON AKTIF"
                            crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Role), cfRole.ColumnName, cfRole.OperatorName, 0))
                    End Select
                Else
                    crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Role), cfRole.ColumnName, cfRole.OperatorName, myArray(0)))
                End If
            End If
        End If
    End Sub

    Private Sub cfRole_Filter(ByVal sender As Object, ByVal FilterArg As FilterCompositeControl.OnFilterArgs) Handles cfRole.Filter
        Cari()
    End Sub
End Class
