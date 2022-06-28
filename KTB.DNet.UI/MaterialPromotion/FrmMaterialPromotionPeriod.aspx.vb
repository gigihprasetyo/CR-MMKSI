#Region "Custom Namespace Imports"
Imports System.DateTime
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region


Public Class FrmMaterialPromotionPeriod
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgtPromotionPeriod As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSImpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents icStartDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents IcEndDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtPeriodName As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Dim _sessHelper As New SessionHelper
    Private _createPriv As Boolean = False
#End Region

#Region "Custom Method"
    Private Sub InitiatePage()
        ViewState("currentSortColumn") = "ID"
        ViewState("currentSortDirection") = Sort.SortDirection.DESC
    End Sub
    Private Sub BindData(ByVal index As Integer)
        Dim totRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim _arr As ArrayList = New MaterialPromotionPeriodFacade(User).RetrieveActiveList(criterias, index + 1, dgtPromotionPeriod.PageSize, totRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        _sessHelper.SetSession("SortViewMPP", criterias)
        dgtPromotionPeriod.DataSource = _arr
        dgtPromotionPeriod.VirtualItemCount = totRow
        dgtPromotionPeriod.DataBind()
    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dgtPromotionPeriod.DataSource = New MaterialPromotionPeriodFacade(User).RetrieveActiveList(_sessHelper.GetSession("SortViewMPP"), indexPage + 1, dgtPromotionPeriod.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgtPromotionPeriod.VirtualItemCount = totalRow
            dgtPromotionPeriod.DataBind()
        End If

    End Sub
    Private Function CheckDuplicateI(ByVal id As Integer) As Boolean
        Dim objMPP As New MaterialPromotionPeriod
        objMPP = CType(_sessHelper.GetSession("objMPP"), MaterialPromotionPeriod)
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MaterialPromotionPeriod), "StartDate", Sort.SortDirection.ASC))

        Dim criterias As New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "StartDate", MatchType.GreaterOrEqual, icStartDate.Value))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "EndDate", MatchType.LesserOrEqual, IcEndDate.Value))

        If CType(ViewState("vsProcess"), String) = "Update" Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "ID", MatchType.No, id))
        End If

        Dim _arr As ArrayList = New MaterialPromotionPeriodFacade(User).Retrieve(criterias, sortColl)
        If _arr.Count > 0 Then
            MessageBox.Show("Duplikasi periode pada " + CType(_arr(0), MaterialPromotionPeriod).StartDate + "s.d" + CType(_arr(0), MaterialPromotionPeriod).EndDate)
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CheckDuplicateII(ByVal id As Integer) As Boolean
        Dim objMPP As New MaterialPromotionPeriod
        objMPP = CType(_sessHelper.GetSession("objMPP"), MaterialPromotionPeriod)
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MaterialPromotionPeriod), "StartDate", Sort.SortDirection.ASC))

        Dim criterias As New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "StartDate", MatchType.LesserOrEqual, icStartDate.Value))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "EndDate", MatchType.GreaterOrEqual, icStartDate.Value))

        If CType(ViewState("vsProcess"), String) = "Update" Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "ID", MatchType.No, id))
        End If

        Dim _arr As ArrayList = New MaterialPromotionPeriodFacade(User).Retrieve(criterias, sortColl)
        If _arr.Count > 0 Then
            MessageBox.Show("Duplikasi periode pada " + CType(_arr(0), MaterialPromotionPeriod).StartDate + "s.d" + CType(_arr(0), MaterialPromotionPeriod).EndDate)
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CheckDuplicateIII(ByVal id As Integer) As Boolean
        Dim objMPP As New MaterialPromotionPeriod
        objMPP = CType(_sessHelper.GetSession("objMPP"), MaterialPromotionPeriod)
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MaterialPromotionPeriod), "StartDate", Sort.SortDirection.ASC))

        Dim criterias As New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "StartDate", MatchType.LesserOrEqual, IcEndDate.Value))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "EndDate", MatchType.GreaterOrEqual, IcEndDate.Value))

        If CType(ViewState("vsProcess"), String) = "Update" Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "ID", MatchType.No, id))
        End If
        Dim _arr As ArrayList = New MaterialPromotionPeriodFacade(User).Retrieve(criterias, sortColl)
        If _arr.Count > 0 Then
            MessageBox.Show("Duplikasi periode pada " + CType(_arr(0), MaterialPromotionPeriod).StartDate + " s.d " + CType(_arr(0), MaterialPromotionPeriod).EndDate)
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CheckDuplicateName(ByVal id As Integer) As Boolean
        Dim objMPP As New MaterialPromotionPeriod
        objMPP = CType(_sessHelper.GetSession("objMPP"), MaterialPromotionPeriod)
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MaterialPromotionPeriod), "PeriodName", Sort.SortDirection.ASC))

        Dim criterias As New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "PeriodName", MatchType.Exact, txtPeriodName.Text.Trim))


        If CType(ViewState("vsProcess"), String) = "Update" Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionPeriod), "ID", MatchType.No, id))
        End If
        Dim _arr As ArrayList = New MaterialPromotionPeriodFacade(User).Retrieve(criterias, sortColl)
        If _arr.Count > 0 Then
            MessageBox.Show("Duplikasi Period's Name")
            Return True
        Else
            Return False
        End If
    End Function
    Private Function PageValid(ByVal id As Integer) As Boolean
        If Not (txtDesc.Text.Trim = String.Empty) Then
            'If CheckDuplicateI(id) Then
            '    Return False
            'End If
            'If CheckDuplicateII(id) Then
            '    Return False
            'End If
            'If CheckDuplicateIII(id) Then
            '    Return False
            'End If
            'If CheckDuplicateName(id) Then
            '    Return False
            'End If
            If DateDiff(DateInterval.Day, icStartDate.Value, IcEndDate.Value, Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1) <= 0 Then
                MessageBox.Show("Periode tidak valid")
                Return False
            End If
        Else
            If (txtPeriodName.Text.Trim = String.Empty) Then
                MessageBox.Show("Period's Name harus diisi")
                Return False
            End If
            MessageBox.Show("Deskripsi harus diisi")
            Return False
        End If
        Return True
    End Function
    Private Sub EditItem(ByVal i As Integer)
        Dim objMPP As New MaterialPromotionPeriod
        objMPP = New MaterialPromotionPeriodFacade(User).Retrieve(i)
        icStartDate.Value = objMPP.StartDate
        IcEndDate.Value = objMPP.EndDate
        txtDesc.Text = objMPP.Description
        txtPeriodName.Text = objMPP.PeriodName
        _sessHelper.SetSession("objMPP", objMPP)
    End Sub
    Private Sub DeleteItem(ByVal i As Integer)
        Dim objMPP As New MaterialPromotionPeriod
        objMPP = New MaterialPromotionPeriodFacade(User).Retrieve(i)
        Dim n As Integer = New MaterialPromotionPeriodFacade(User).Delete(objMPP)
        If n = 1 Then
            MessageBox.Show("Data sukses dihapus")
        Else
            MessageBox.Show("Data gagal dihapus")
        End If
    End Sub
    Private Sub ViewItem(ByVal id As Integer)
        Dim objMPP As New MaterialPromotionPeriod
        objMPP = New MaterialPromotionPeriodFacade(User).Retrieve(id)
        icStartDate.Value = objMPP.StartDate
        IcEndDate.Value = objMPP.EndDate
        txtDesc.Text = objMPP.Description
        txtPeriodName.Text = objMPP.PeriodName
    End Sub
    Private Sub ClearData()
        icStartDate.Value = Date.Today
        IcEndDate.Value = Date.Today.Add(System.TimeSpan.FromDays(1))
        txtPeriodName.Text = String.Empty
        txtDesc.Text = String.Empty
        btnSImpan.Enabled = _createPriv

    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        _createPriv = CheckCreatePriv()
        If Not IsPostBack() Then
            InitiatePage()
            ClearData()
            ViewState.Add("vsProcess", "Insert")
            BindData(0)
        End If
    End Sub
    Private Sub dgtPromotionPeriod_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgtPromotionPeriod.PageIndexChanged
        dgtPromotionPeriod.CurrentPageIndex = e.NewPageIndex
        BindData(dgtPromotionPeriod.CurrentPageIndex)
    End Sub
    Private Sub dgtPromotionPeriod_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgtPromotionPeriod.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgtPromotionPeriod.SelectedIndex = -1
        dgtPromotionPeriod.CurrentPageIndex = 0
        bindGridSorting(dgtPromotionPeriod.CurrentPageIndex)
    End Sub
    Private Sub btnSImpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSImpan.Click
        Dim n As Integer = -1
        Dim objMPP As New MaterialPromotionPeriod


        If (txtPeriodName.Text <> String.Empty) Then
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                If PageValid(0) Then
                    objMPP.StartDate = icStartDate.Value
                    objMPP.EndDate = IcEndDate.Value
                    objMPP.Description = txtDesc.Text
                    objMPP.PeriodName = txtPeriodName.Text

                    n = New MaterialPromotionPeriodFacade(User).Insert(objMPP)
                    If n = 1 Then
                        MessageBox.Show("Data Telah Tersimpan")
                        ClearData()
                        ViewState.Add("vsProcess", "Insert")
                    Else
                        MessageBox.Show("Simpan gagal")
                    End If
                    BindData(dgtPromotionPeriod.CurrentPageIndex)
                End If
            Else
                objMPP = CType(_sessHelper.GetSession("objMPP"), MaterialPromotionPeriod)
                If PageValid(objMPP.ID) Then

                    objMPP.StartDate = icStartDate.Value
                    objMPP.EndDate = IcEndDate.Value
                    objMPP.Description = txtDesc.Text
                    objMPP.PeriodName = txtPeriodName.Text

                    n = New MaterialPromotionPeriodFacade(User).Update(objMPP)
                    If n = 1 Then
                        MessageBox.Show("Data Telah Tersimpan")
                        ClearData()
                        ViewState.Add("vsProcess", "Insert")
                        _sessHelper.RemoveSession("objMPP")
                    Else
                        MessageBox.Show("Simpan gagal")
                    End If
                    BindData(dgtPromotionPeriod.CurrentPageIndex)
                End If
            End If
        Else
            MessageBox.Show("Nama Periode harus diisi")
        End If

    End Sub
    Private Sub dgtPromotionPeriod_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgtPromotionPeriod.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                ViewState.Add("vsProcess", "Update")
                EditItem(CInt(e.CommandArgument))
                btnSImpan.Enabled = True
            Case "Delete"
                DeleteItem(CInt(e.CommandArgument))
                BindData(dgtPromotionPeriod.CurrentPageIndex)
            Case "View"
                ViewItem(CInt(e.CommandArgument))
                btnSImpan.Enabled = False
        End Select
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        dgtPromotionPeriod.SelectedIndex = -1
    End Sub
    Private Sub dgtPromotionPeriod_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgtPromotionPeriod.ItemDataBound
        Dim objMPP As MaterialPromotionPeriod = e.Item.DataItem
        If Not e.Item.DataItem Is Nothing Then
            Dim lblStartMonth As Label = CType(e.Item.FindControl("lblStartMonth"), Label)
            lblStartMonth.Text = objMPP.StartDate.Day.ToString + " " + CType(objMPP.StartDate.Month - 1, enumMonth.Month).ToString.Replace("_", " ") + " " + objMPP.StartDate.Year.ToString

            Dim lblEndMonth As Label = CType(e.Item.FindControl("lblEndMonth"), Label)
            lblEndMonth.Text = objMPP.EndDate.Day.ToString + " " + CType(objMPP.EndDate.Month - 1, enumMonth.Month).ToString.Replace("_", " ") + " " + objMPP.EndDate.Year.ToString

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgtPromotionPeriod.CurrentPageIndex * dgtPromotionPeriod.PageSize)

            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            lbtnView.Visible = _createPriv
            lbtnEdit.Visible = _createPriv
            lbtnDelete.Visible = _createPriv

        End If

    End Sub
    Private Sub dgtPromotionPeriod_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgtPromotionPeriod.Init
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionViewPeriod_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Material Promosi - Periode Material Promosi")
        End If
    End Sub

    Private Function CheckCreatePriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionCreatePeriod_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    
#End Region
End Class
