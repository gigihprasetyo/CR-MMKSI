#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
#End Region

Public Class PopUpJobPosition
    Inherits System.Web.UI.Page


#Region "Private Variables"
    Private sHelper As New SessionHelper
#End Region


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgJobPosition As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub ClearData()
        Me.txtKode.Text = String.Empty
        Me.txtDeskripsi.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        dtgJobPosition.DataSource = New JobPositionFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, _
            dtgJobPosition.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgJobPosition.VirtualItemCount = totalRow
        dtgJobPosition.DataBind()
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'UnRemark by anh on 20110619
        'Remark by anh for Rina on 20100708
        'Start  :CR;by:dna;for:Rina;On:20100520;Desc:Only specified Job is allowed to select in training module
        'Dim sAllowedJob As String = "'MKN' ,'FL' ,'FC' ,'LDR' ,'HLP' ,'OM' ,'TM' ,'INS' ,'SVC_MGR' ,'CS' ,'CSH' ,'CRD' ,'OTH','ADM_SVC'"
        Dim sNotAllowedJob As String = "'HLP' ,'SSM' ,'SPV' ,'SLM' ,'SCN' ,'SBM' ,'CSH'"

        If Not IsNothing(Request.Item("ServiceOnly")) AndAlso CType(Request.Item("ServiceOnly"), String) = "1" Then
            criterias.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.NotInSet, sNotAllowedJob))
        End If

        If Not IsNothing(Request.Item("category")) AndAlso CType(Request.Item("category"), String) = "2" Then
            criterias.opAnd(New Criteria(GetType(JobPosition), "Category", MatchType.Exact, "2"))
        End If


        'End    :CR;by:dna;for:Rina;On:20100520;Desc:Only specified Job is allowed to select in training module
        'End Remark by anh for Rina on 20100708
        If txtKode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.[Partial], txtKode.Text))
        End If
        If txtDeskripsi.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(JobPosition), "Description", MatchType.[Partial], txtDeskripsi.Text))
        End If

        If Me.IsDealer Then
            criterias.opAnd(New Criteria(GetType(JobPosition), "Code", MatchType.NotInSet, "'oth'"))
        End If

        'Start  :CR;by:anh;for:Rina;On:20100708;Desc:Only specified Job is allowed to select in training module
        Dim critJptm As New CriteriaComposite(New Criteria(GetType(JobPositionToMenu), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not IsNothing(Request.Item("Menu")) Then
            Dim iMenu As Integer = CType(Request.Item("Menu"), Integer)
            If iMenu > 0 Then
                critJptm.opAnd(New Criteria(GetType(JobPositionToMenu), "JobPositionMenu.ID", MatchType.Exact, iMenu))
            End If
        End If
        'If found, then build a list of JobPositonToMenu id so we can use
        'InSet criteria to search for JobPositon 
        Dim distinctIDs As New Hashtable
        Dim arrJptmList As ArrayList = New JobPositionToMenuFacade(User).Retrieve(critJptm)
        Dim strIDJptm As String = ""
        For i As Integer = 0 To arrJptmList.Count - 1
            If strIDJptm.Length > 0 Then
                strIDJptm += ","
            End If
            Dim jptm As JobPositionToMenu = CType(arrJptmList(i), JobPositionToMenu)
            If distinctIDs(jptm.ID) Is Nothing Then
                strIDJptm += jptm.JobPosition.ID.ToString()
                distinctIDs.Add(jptm.ID, jptm.JobPosition.ID)
            End If
        Next
        If strIDJptm.Length > 0 Then
            strIDJptm = "(" + strIDJptm + ")"
            criterias.opAnd(New Criteria(GetType(JobPosition), "ID", MatchType.InSet, strIDJptm))
        End If
        'End  :CR;by:anh;for:Rina;On:20100708;Desc:Only specified Job is allowed to select in training module

        Return criterias
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
        If dtgJobPosition.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgPosisi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgJobPosition.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)

        End If

    End Sub

    Private Sub dtgPosisi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgJobPosition.PageIndexChanged
        dtgJobPosition.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgJobPosition.CurrentPageIndex)
    End Sub

    Private Sub dtgPosisi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgJobPosition.SortCommand
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
        dtgJobPosition.CurrentPageIndex = 0
        BindDataGrid(dtgJobPosition.CurrentPageIndex)
    End Sub

End Class
