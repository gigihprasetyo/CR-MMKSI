#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Benefit

Imports System.Text
Imports System.Drawing.Color
Imports System.Collections.Generic
#End Region

Public Class PopUpRefBenefitSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgBenefitClaimSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNomorSurat As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBenefitRegNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRemarks As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
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

#Region " custom Declaration "
    Private sesHelper As New SessionHelper
    Private SessionGridData = "PopUpRefBenefitSelection.gridList"
#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            ViewState("currSortColumn") = "BenefitRegNo"
            ViewState("currSortDirection") = Sort.SortDirection.DESC

            ClearData()
            ReadData()   '-- Read all data matching criteria
            BindGrid(dtgBenefitClaimSelection.CurrentPageIndex)  '-- Bind page-1
        End If
    End Sub

    Public Sub ReadData()
        Dim strSQL As String = String.Empty

        Dim criterias As New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Not txtBenefitRegNo.Text.Trim = "" Then
            criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "BenefitRegNo", MatchType.Exact, txtBenefitRegNo.Text.Trim))
        End If
        If Not txtNomorSurat.Text.Trim = "" Then
            criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "NomorSurat", MatchType.[Partial], txtNomorSurat.Text.Trim))
        End If
        If Not txtRemarks.Text.Trim = "" Then
            criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "Remarks", MatchType.[Partial], txtRemarks.Text.Trim))
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BenefitMasterHeader), ViewState("currSortColumn").ToString(), [Enum].Parse(GetType(Sort.SortDirection), ViewState("currSortDirection").ToString())))

        Dim arrBenefitClaimList As ArrayList = New ArrayList
        arrBenefitClaimList = New BenefitMasterHeaderFacade(User).RetrieveActiveList(criterias, sortColl)

        sesHelper.SetSession(SessionGridData, arrBenefitClaimList)
        If arrBenefitClaimList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrBabitEventList As ArrayList = CType(sesHelper.GetSession(SessionGridData), ArrayList)

        If arrBabitEventList.Count <> 0 Then
            CommonFunction.SortListControl(arrBabitEventList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            dtgBenefitClaimSelection.DataSource = arrBabitEventList
            dtgBenefitClaimSelection.VirtualItemCount = arrBabitEventList.Count()
            dtgBenefitClaimSelection.DataBind()
            btnChoose.Disabled = False
        Else
            dtgBenefitClaimSelection.DataSource = New ArrayList
            dtgBenefitClaimSelection.VirtualItemCount = 0
            dtgBenefitClaimSelection.CurrentPageIndex = 0
            dtgBenefitClaimSelection.DataBind()
        End If
    End Sub

    Private Sub dtgBenefitClaimSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBenefitClaimSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As BenefitMasterHeader = CType(e.Item.DataItem, BenefitMasterHeader)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)

                'Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
                'hdnID.Value = RowValue.ID

                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                If RowValue.Status = 0 Then
                    lblStatus.Text = "Aktif"
                ElseIf RowValue.Status = 1 Then
                    lblStatus.Text = "Tidak Aktif"
                Else
                    lblStatus.Text = "Tidak Aktif"
                End If
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ReadData()
        dtgBenefitClaimSelection.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dtgBenefitClaimSelection.CurrentPageIndex)  '-- Bind page-1
        If dtgBenefitClaimSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgBenefitClaimSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgBenefitClaimSelection.SortCommand

        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        dtgBenefitClaimSelection.CurrentPageIndex = 0
        ReadData()
        BindGrid(dtgBenefitClaimSelection.CurrentPageIndex)
    End Sub

    Private Sub dtgBenefitClaimSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgBenefitClaimSelection.PageIndexChanged

        ReadData()
        dtgBenefitClaimSelection.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

#End Region

#Region " Custom Method "

    Private Sub ClearData()
        Me.txtBenefitRegNo.Text = String.Empty
        Me.txtNomorSurat.Text = String.Empty
        Me.txtRemarks.Text = String.Empty
        dtgBenefitClaimSelection.DataSource = New ArrayList
        dtgBenefitClaimSelection.DataBind()
    End Sub

#End Region

End Class
