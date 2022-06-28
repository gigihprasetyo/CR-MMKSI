Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class FrmSalesmanUniformAssign
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents icStartDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSelectSalesman As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dgSalesmanUniformAssigned As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnRilis As System.Web.UI.WebControls.Button
    Protected WithEvents ddlJobPositionDesc As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents TxtDistributionCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopupDistribution As System.Web.UI.WebControls.Label

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
    'Private _SalesmanAreaFacade As New SalesmanAreaFacade(User)
    'Private _DealerFacade As New DealerFacade(User)
    Private _SalesmanUniformAssignedFacade As New SalesmanUniformAssignedFacade(User)
    Private sessHelper As New SessionHelper
    Private _createPriv As Boolean = False
#End Region

#Region "PrivateCustomMethods"
    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "SalesmanHeader.SalesmanCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub ClearData()
        txtDealerCode.Text = String.Empty
        'ddlSalesmanUnifDistribution.SelectedIndex = 0
        TxtDistributionCode.Text = String.Empty
    End Sub
    Private Sub BindDropDownLists()
        'CommonFunction.BindSalesmanUnifDistributionCode(ddlSalesmanUnifDistribution, Me.User, True)
        'modified by anh July 26,2010 re by rna
        If Not IsNothing(Request.QueryString("Menu")) Then
            Dim iMenu As Integer = CType(Request.QueryString("Menu"), Integer)
            If iMenu > 0 Then
                CommonFunction.BindJobPositionByMenuAssigned(ddlJobPositionDesc, User, True, iMenu)
            End If
        Else
            CommonFunction.BindJobPosition(ddlJobPositionDesc, User, True, False)
        End If
        'end modified

        ddlYear.Items.Add(New ListItem("All Period", 0))
        For i As Integer = 2007 To Date.Today.Year
            ddlYear.Items.Add(New ListItem(i, i))
        Next

        ddlYear.SelectedIndex = 0

    End Sub
    Private Sub CreateCriteria()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        ' default criteria, salesman header must active and already have salesmanCode
        criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.RegisterStatus", MatchType.Exact, CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)))
        criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.RegisterStatus", MatchType.Exact, CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)))

        If ddlYear.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "CreatedTime", MatchType.GreaterOrEqual, New Date(CInt(ddlYear.SelectedValue), 1, 1)))
            criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "CreatedTime", MatchType.Lesser, New Date(CInt(ddlYear.SelectedValue) + 1, 1, 1)))
        End If

        If txtDealerCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Trim.Replace(";", "','") & "')"))
        End If
        If TxtDistributionCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanUniform.SalesmanUnifDistribution.SalesmanUnifDistributionCode", MatchType.InSet, "('" & TxtDistributionCode.Text.Trim.Replace(";", "','") & "')"))
        End If
        'If ddlSalesmanUnifDistribution.SelectedValue <> "-1" Then
        '    criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanUniform.SalesmanUnifDistribution.ID", MatchType.Exact, ddlSalesmanUnifDistribution.SelectedValue.ToString))
        'End If
        'If ddlSalesmanUniform.SelectedIndex > 0 Then
        '    criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanUniform.ID", MatchType.Exact, ddlSalesmanUniform.SelectedValue.ToString))
        'End If

        If ddlJobPositionDesc.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.JobPosition.ID", MatchType.Exact, ddlJobPositionDesc.SelectedValue.ToString))
        End If
        sessHelper.SetSession("CRITERIAS", criterias)
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        arrList = _SalesmanUniformAssignedFacade.RetrieveByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), idxPage + 1, dgSalesmanUniformAssigned.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgSalesmanUniformAssigned.DataSource = arrList
        dgSalesmanUniformAssigned.VirtualItemCount = totalRow
        dgSalesmanUniformAssigned.DataBind()

        For Each item As DataGridItem In dgSalesmanUniformAssigned.Items
            Dim lblGenderCode As Label = CType(item.FindControl("lblGenderCode"), Label)
            Dim lblGender As Label = CType(item.FindControl("lblGender"), Label)
            If Not lblGenderCode.Text Is Nothing Then
                If lblGenderCode.Text = "1" Then
                    lblGender.Text = "Pria"
                Else
                    lblGender.Text = "Wanita"
                End If
            End If
        Next
    End Sub
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsNothing(sessHelper.GetSession("LOGINUSERINFO")) Then
            Dim objUserInfo As UserInfo = New UserInfo
            objUserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            If objUserInfo.Dealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                CheckPrivilege()
                _createPriv = CheckCreatePrivilege()
                btnSearch.Visible = _createPriv
                btnRilis.Visible = _createPriv
                btnSelectSalesman.Visible = _createPriv
            End If
        End If
        
        If Not IsPostBack Then
            Initialize()
            BindDropDownLists()
            BindAtributes()
            CreateCriteria()
            'BindDataGrid(0)
        End If
    End Sub

    Private Sub BindAtributes()
        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
    End Sub

    Private Sub dgSalesmanUniformAssigned_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanUniformAssigned.SortCommand
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
        dgSalesmanUniformAssigned.SelectedIndex = -1
        dgSalesmanUniformAssigned.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanUniformAssigned.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanUniformAssigned_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanUniformAssigned.PageIndexChanged
        dgSalesmanUniformAssigned.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanUniformAssigned.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanUniformAssigned_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanUniformAssigned.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanUniformAssigned As SalesmanUniformAssigned = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSalesmanUniformAssigned.CurrentPageIndex * dgSalesmanUniformAssigned.PageSize)

            Dim chkSelectionNew As CheckBox = CType(e.Item.FindControl("chkSelection"), CheckBox)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            ' value 1 menandakan sudah release
            If objSalesmanUniformAssigned.IsReleased = 1 Then
                chkSelectionNew.Checked = True
                chkSelectionNew.Enabled = False
                lbtnDelete.Enabled = False
            Else
                chkSelectionNew.Checked = False
                chkSelectionNew.Enabled = True
                lbtnDelete.Enabled = True
            End If
            lbtnDelete.Visible = _createPriv
            chkSelectionNew.Visible = _createPriv
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgSalesmanUniformAssigned.CurrentPageIndex = 0
        CreateCriteria()
        BindDataGrid(0)
    End Sub
    Private Sub dgSalesmanUniformAssigned_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanUniformAssigned.ItemCommand
        Select Case e.CommandName
            Case "Delete" 'Delete this datagrid item 
                Dim _SalesmanAssigned As SalesmanUniformAssigned = _SalesmanUniformAssignedFacade.Retrieve(CType(e.CommandArgument, Integer))
                _SalesmanUniformAssignedFacade.Delete(_SalesmanAssigned)
                BindDataGrid(dgSalesmanUniformAssigned.CurrentPageIndex)
        End Select
    End Sub
    Private Sub btnRilis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRilis.Click
        Dim i As Integer = 0
        Dim arrList As ArrayList = New ArrayList
        Dim _SalesmanAssigned As SalesmanUniformAssigned

        For Each item As DataGridItem In dgSalesmanUniformAssigned.Items
            Dim chk As CheckBox = CType(item.FindControl("chkSelection"), CheckBox)
            Dim lbtnDelete As LinkButton = CType(item.FindControl("lbtnDelete"), LinkButton)

            ' handle kalau sdh dirilis tdk bs didelete
            If (chk.Checked) And (chk.Enabled = True) Then
                _SalesmanAssigned = _SalesmanUniformAssignedFacade.Retrieve(CInt(dgSalesmanUniformAssigned.DataKeys().Item(i)))
                arrList.Add(_SalesmanAssigned)
                lbtnDelete.Enabled = False
            End If
            i = i + 1
        Next

        If (arrList.Count > 0) Then
            If _SalesmanAssigned Is Nothing Then
                MessageBox.Show("Silakan pilih assign seragam yg akan di Rilis ")
                Return
            Else
                If (_SalesmanUniformAssignedFacade.RilisAssignUniform(arrList) > 0) Then
                    MessageBox.Show("Proses Rilis berhasil")
                    BindDataGrid(dgSalesmanUniformAssigned.CurrentPageIndex)
                Else
                    MessageBox.Show("Proses Rilis gagal")
                End If
            End If

        Else
            MessageBox.Show("Tidak ada data yg di pilih.")
        End If
    End Sub

    Private Sub btnSelectSalesman_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectSalesman.ServerClick
        dgSalesmanUniformAssigned.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanUniformAssigned.CurrentPageIndex)
    End Sub

#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.TPPSViewSelect_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pelatihan Tenaga Penjual - Pilih Tenaga Penjual Penerima Seragam")
        End If
    End Sub

    Private Function CheckCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.TPPSCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub dgSalesmanUniformAssigned_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgSalesmanUniformAssigned.SelectedIndexChanged

    End Sub
End Class
