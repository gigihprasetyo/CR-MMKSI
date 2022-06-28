Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.PO
Public Class PopUpCreditAccountSelection2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCreditAccount As System.Web.UI.WebControls.TextBox
    Protected WithEvents lboxGroup As System.Web.UI.WebControls.ListBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgDealerSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Protected countChk As Integer = 0
    Private objDealer As Dealer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            'Added by Ery for sorting
            Viewstate.Add("SortColDealer", "DealerName")
            Viewstate.Add("SortDirDealer", Sort.SortDirection.ASC)

            '''  - BEGIN : Added by MU, used in FrmUserOrganizationAssignment.aspx
            ViewState.Add("Group", CStr(Request.QueryString("Group")))
            ViewState.Add("Dealer", CStr(Request.QueryString("Dealer")))
            '''  - END   : Added by MU, used in FrmUserOrganizationAssignment.aspx
            objDealer = Session("DEALER")
            If Not objDealer.DealerGroup Is Nothing OrElse objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                BindDdlGroup()
            End If

            ClearData()
        End If
    End Sub
    Private Sub ClearData()
        Me.txtCreditAccount.Text = String.Empty
        Me.txtDealerName.Text = String.Empty
        Me.lboxGroup.SelectedIndex = -1
    End Sub
    Private Sub BindDdlGroup()
        'Dim nTotRow As Integer = 0
        'Dim nPageNumber As Integer = 1
        'Dim nPageSize As Integer = 50

        objDealer = Session("DEALER")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '''  - BEGIN : Added by MU, used in FrmUserOrganizationAssignment.aspx
        If Not ViewState("Group") Is Nothing Then
            criterias.opAnd(New Criteria(GetType(DealerGroup), "ID", MatchType.Exact, CInt(ViewState("Group"))))
        End If
        '''  - END   : Added by MU, used in FrmUserOrganizationAssignment.aspx

        ' 08-Nov-2007   Deddy H     Handle bug 1383, bila dari send message bs send ke semua dealer
        If IsNothing(Request.QueryString("All")) Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(DealerGroup), "ID", MatchType.Exact, CInt(objDealer.DealerGroup.ID)))
            End If
        End If

        'ddlGroup.DataSource = New DealerGroupFacade(User).RetrieveActiveList(criterias, nPageNumber, nPageSize, nTotRow, "GroupName", Sort.SortDirection.ASC)
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DealerGroup), "GroupName", Sort.SortDirection.ASC))
        lboxGroup.DataSource = New DealerGroupFacade(User).Retrieve(criterias, sortColl)
        lboxGroup.DataTextField = "GroupName"
        lboxGroup.DataValueField = "ID"
        lboxGroup.DataBind()

        ''  - BEGIN : Added by MU, used in FrmUserOrganizationAssignment.aspx
        'If ViewState("Group") Is Nothing Then
        'lboxGroup.Items.Insert(0, New ListItem("Pilih Group", ""))
        'End If
        ''  - END   : Added by MU, used in FrmUserOrganizationAssignment.aspx
    End Sub

    Private Function GetSelectedItem(ByVal listboxGroup As ListBox) As String
        Dim _strGroup As String = String.Empty
        If Not listboxGroup.SelectedIndex = -1 Then
            'For Each item As ListItem In listboxGroup.Items
            '    If _strGroup = String.Empty Then
            '        _strGroup = item.Value
            '    Else
            '        _strGroup = _strGroup & "," & item.Value
            '    End If
            'Next
            'Else

            For Each item As ListItem In listboxGroup.Items
                If item.Selected Then
                    If _strGroup = String.Empty Then
                        _strGroup = item.Text
                    Else
                        _strGroup = _strGroup & "," & item.Text
                    End If
                End If
            Next
        End If

        Return _strGroup
    End Function

    Public Sub BindSearch()
        Dim crtVCA As CriteriaComposite
        Dim objVCAFac As v_CreditAccountFacade = New v_CreditAccountFacade(User)
        Dim arlVCA As ArrayList = New ArrayList

        crtVCA = New CriteriaComposite(New Criteria(GetType(v_CreditAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtDealerName.Text.Trim <> "" Then
            crtVCA.opAnd(New Criteria(GetType(v_CreditAccount), "DealerName", MatchType.[Partial], txtDealerName.Text.Trim))
        End If
        If txtCreditAccount.Text.Trim <> "" Then
            crtVCA.opAnd(New Criteria(GetType(v_CreditAccount), "CreditAccount", MatchType.[Partial], txtCreditAccount.Text.Trim))
        End If
        Dim SelectedGroup As String = GetSelectedItem(lboxGroup)
        If SelectedGroup.Trim <> "" Then
            crtVCA.opAnd(New Criteria(GetType(v_CreditAccount), "GroupName", MatchType.InSet, "('" & SelectedGroup.Trim.Replace(",", "','") & "')"))
        End If
        'added by anh 20140925 
        objDealer = Session("DEALER")
        If Not objDealer Is Nothing Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If objDealer.DealerGroup.ID = 21 Then
                    crtVCA.opAnd(New Criteria(GetType(v_CreditAccount), "CreditAccount", MatchType.Exact, objDealer.CreditAccount))
                Else
                    crtVCA.opAnd(New Criteria(GetType(KTB.DNet.Domain.v_CreditAccount), "CreditAccount", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
                End If
            End If
        End If
        'end added by anh 20140925
        arlVCA = objVCAFac.RetrieveActiveList(crtVCA, viewstate("SortColDealer"), viewstate("SortDirDealer"))
        dtgDealerSelection.DataSource = arlVCA
        dtgDealerSelection.DataBind()
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        'If ViewState("Group") Is Nothing Then
        '    objDealer = Session("DEALER")
        '    ' 08-Nov-2007   Deddy H     Handle bug 1383, bila dari send message bs send ke semua dealer
        '    If IsNothing(Request.QueryString("All")) Then
        '        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        '        End If
        '    End If
        'End If


        'If Not txtDealerName.Text = "" Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerName", MatchType.Partial, txtDealerName.Text))
        'End If

        'If Not txtCreditAccount.Text = "" Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm1", MatchType.Partial, txtCreditAccount.Text))
        'End If

        'If Not ViewState("Dealer") Is Nothing Then
        '    Try
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.No, CInt(ViewState("Dealer"))))
        '    Catch ex As Exception
        '        'None
        '    End Try
        'End If

        'Dim SelectedGroup As String = GetSelectedItem(lboxGroup)
        'If SelectedGroup <> "" Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerGroup.ID", MatchType.InSet, "(" & SelectedGroup & ")"))
        'End If
        'dtgDealerSelection.DataSource = New DealerFacade(User).RetrieveActiveList(criterias, viewstate("SortColDealer"), viewstate("SortDirDealer"))
        'dtgDealerSelection.DataBind()
    End Sub


    Private Sub dtgDealerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        'If Not e.Item.DataItem Is Nothing Then
        '    e.Item.DataItem.GetType().ToString()
        '    Dim RowValue As Dealer = CType(e.Item.DataItem, Dealer)
        '    If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
        '        Dim lblGroup As Label = CType(e.Item.FindControl("lblGroup"), Label)
        '        If Not IsNothing(RowValue.DealerGroup) Then
        '            lblGroup.Text = RowValue.DealerGroup.GroupName
        '        End If
        '        Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
        '        If Not IsNothing(RowValue.City) Then
        '            lblCity.Text = RowValue.City.CityName
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()

        If dtgDealerSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgDealerSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerSelection.SortCommand
        If e.SortExpression = viewstate("SortColDealer") Then
            If viewstate("SortDirDealer") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirDealer", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirDealer", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColDealer", e.SortExpression)
        BindSearch()
    End Sub

    Private Sub dtgDealerSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerSelection.PageIndexChanged
        dtgDealerSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub
End Class
