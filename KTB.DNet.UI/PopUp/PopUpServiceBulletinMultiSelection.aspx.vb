Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Buletin
Imports KTB.DNet.BusinessFacade.Service

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class PopUpServiceBulletinMultiSelection
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper
    Dim arrChkSelectionID As ArrayList = New ArrayList

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    'Protected WithEvents dgBulletin As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents txtPQRNo As System.Web.UI.WebControls.TextBox
    'Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    'Protected WithEvents txtRecallRegNo As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtBuletinNumber As TextBox
    'Protected WithEvents txtBuletinDescription As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private arrList As New ArrayList
    Private sesshelper As SessionHelper = New SessionHelper

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'Put user code to initialize the page here
        If Not IsPostBack Then
            sesHelper.SetSession("ArlBuletin", arrChkSelectionID)
            Initialize()
            ViewState("CurrentSortColumn") = "CreatedTime"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            dgBulletin.CurrentPageIndex = 0
            BindDataGrid(dgBulletin.CurrentPageIndex)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgBulletin.CurrentPageIndex = 0
        BindDataGrid(dgBulletin.CurrentPageIndex)

    End Sub

    Private Sub dgBulletin_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgBulletin.PageIndexChanged
        dgBulletin.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgBulletin.CurrentPageIndex)
    End Sub

    Private Sub dgBulletin_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgBulletin.SortCommand
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
        BindDataGrid(dgBulletin.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtBuletinNumber.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(Buletin), "Title", MatchType.Partial, txtBuletinNumber.Text))
        End If
        If txtBuletinDescription.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(Buletin), "Description", MatchType.Partial, txtBuletinDescription.Text))
        End If
        criterias.opAnd(New Criteria(GetType(Buletin), "BuletinCategory.ID", MatchType.InSet, "(355,405,406,447,470,470,484,497,503)"))

        arrList = New BuletinFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgBulletin.PageSize, totalRow, _
                        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgBulletin.DataSource = arrList
        dgBulletin.VirtualItemCount = totalRow
        dgBulletin.DataBind()
    End Sub

    Private Sub Initialize()

        ViewState("CurrentSortColumn") = "BuletinDescription"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Protected Sub chkSelection_CheckedChanged(sender As Object, e As EventArgs)
        Dim chk As CheckBox = CType(sender, CheckBox)
        Dim dgItem As DataGridItem = CType(chk.NamingContainer, DataGridItem)
        Dim txtNoRangka As Label = CType(dgItem.FindControl("lblTitle"), Label)
        Dim txtChassis As String = txtNoRangka.Text
        ArrayBulletin.Text = ""

        arrChkSelectionID = CType(sesHelper.GetSession("ArlBuletin"), ArrayList)
        If chk.Checked = True Then
            arrChkSelectionID.Add(txtChassis)
        Else
            arrChkSelectionID.Remove(txtChassis)
        End If
        sesHelper.SetSession("ArlBuletin", arrChkSelectionID)

        For Each itemID As String In arrChkSelectionID
            ArrayBulletin.Text += itemID + ";"
        Next
        If ArrayBulletin.Text <> "" Then
            ArrayBulletin.Text = ArrayBulletin.Text.Remove(ArrayBulletin.Text.Length - 1, 1)
        End If


    End Sub

End Class