#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit

#End Region

Public Class PopUpWorkCode
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtKodeKerja As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgKerja As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private paramPositionCode As String
    Private paramTypeCode As String

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub ClearData()
        Me.txtKodeKerja.Text = String.Empty
        Me.txtDeskripsi.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "KodeKerja"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        dtgKerja.DataSource = New DeskripsiWorkPositionFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, _
            dtgKerja.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgKerja.VirtualItemCount = totalRow
        dtgKerja.DataBind()

        If dtgKerja.VirtualItemCount > 0 Then
            btnChoose.Disabled = False
        End If
    End Sub

    Private Sub BindGridByPositionCode(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        Dim deskripsiWorkCodeList As ArrayList = GetDataByPositionCode()

        dtgKerja.DataSource = deskripsiWorkCodeList
        dtgKerja.VirtualItemCount = deskripsiWorkCodeList.Count
        dtgKerja.DataBind()

        If dtgKerja.VirtualItemCount > 0 Then
            btnChoose.Disabled = False
        End If
    End Sub

    Private Function GetDataByPositionCode()
        Dim filterKodeKerja As String = txtKodeKerja.Text
        Dim filterDeskripsi As String = txtDeskripsi.Text
        Dim strSql As String

        strSql += "select distinct isnull(d.ID, 0) AS ID ,isnull(d.KodeKerja, b.WorkCode) AS KodeKerja ,isnull(d.Description, '') AS Description ,isnull(d.Status, 0) AS Status ,isnull(d.RowStatus, 0) AS RowStatus ,isnull(d.CreatedBy, '') AS CreatedBy ,isnull(d.CreatedTime, GETDATE()) AS CreatedTime ,isnull(d.LastUpdateBy, '') AS LastUpdateBy ,isnull(d.LastUpdateTime, GETDATE()) AS LastUpdateTime "
        strSql += "from VechileType a "
        strSql += "inner join LaborMaster b "
        strSql += "on a.ID = b.VechileTypeID "
        strSql += "left join DeskripsiKodePosisi c "
        strSql += "on c.KodePosition = b.LaborCode "
        strSql += "left join DeskripsiKodeKerja d "
        strSql += "on b.WorkCode = d.KodeKerja "
        strSql += "where 1=1 "
        If paramTypeCode.Trim <> "" Then
            strSql += " and a.VechileTypeCode = '" & paramTypeCode & "'"
        End If
        If paramPositionCode.Trim <> "" Then
            strSql += "  and b.LaborCode like '" & paramPositionCode & "'"
        End If
        If filterKodeKerja <> "" Then
            strSql += "  and b.WorkCode like '%" & filterKodeKerja & "%'"
        End If
        If filterDeskripsi <> "" Then
            strSql += "  and d.Description like '%" & filterDeskripsi & "%'"
        End If

        Dim deskripsiWorkCodeListDataSet As DataSet = New DeskripsiWorkPositionFacade(User).DoRetrieveDataset(strSql)
        Dim deskripsiWorkCodeList As ArrayList = New ArrayList()
        Dim objDeskripsiWorkCode As DeskripsiKodeKerja

        If deskripsiWorkCodeListDataSet.Tables.Count > 0 Then
            For Each dr As DataRow In deskripsiWorkCodeListDataSet.Tables(0).Rows
                objDeskripsiWorkCode = New DeskripsiKodeKerja()
                objDeskripsiWorkCode.ID = CType(dr("ID"), Integer)
                objDeskripsiWorkCode.KodeKerja = CType(dr("KodeKerja"), String)
                objDeskripsiWorkCode.Description = CType(dr("Description"), String)
                objDeskripsiWorkCode.Status = CType(dr("Status"), Integer)
                objDeskripsiWorkCode.RowStatus = CType(dr("RowStatus"), Integer)
                objDeskripsiWorkCode.CreatedBy = CType(dr("CreatedBy"), String)
                objDeskripsiWorkCode.CreatedTime = CType(dr("CreatedTime"), DateTime)
                objDeskripsiWorkCode.LastUpdateBy = CType(dr("LastUpdateBy"), String)
                objDeskripsiWorkCode.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

                deskripsiWorkCodeList.Add(objDeskripsiWorkCode)
            Next
        End If

        Return deskripsiWorkCodeList
    End Function

    Public Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodeKerja.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(DeskripsiKodeKerja), "KodeKerja", MatchType.[Partial], txtKodeKerja.Text))
        End If
        If txtDeskripsi.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(DeskripsiKodeKerja), "Description", MatchType.[Partial], txtDeskripsi.Text))
        End If
        ' criterias.opAnd(New Criteria(GetType(DeskripsiKodeKerja), "Status", MatchType.Exact, CType(EnumJobPositionDescription.JobPosition.Aktive, Integer)))
        Return criterias
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        paramPositionCode = Request.QueryString("positionCode")
        paramTypeCode = Request.QueryString("typeCode")
        If Not IsNothing(Request.QueryString("ChassisNumber")) Then
            Try
                Dim chassisMaster As ChassisMaster = New ChassisMaster
                chassisMaster = New ChassisMasterFacade(User).RetrieveByChassisNumbers(Request.QueryString("ChassisNumber").Trim())(0)
                paramTypeCode = chassisMaster.VechileColor.VechileType.VechileTypeCode
            Catch ex As Exception
                Dim chassisMaster As ChassisMasterBB = New ChassisMasterBB
                chassisMaster = New ChassisMasterBBFacade(User).RetrieveByChassisNumbers(Request.QueryString("ChassisNumber").Trim())(0)
                paramTypeCode = chassisMaster.VechileColor.VechileType.VechileTypeCode
            End Try
        End If
        Try
            paramPositionCode = paramPositionCode.Trim
            paramTypeCode = paramTypeCode.Trim
        Catch
        End Try
        If Not Page.IsPostBack Then
            InitiatePage()

            If (Not IsNothing(paramTypeCode) AndAlso Not IsNothing(paramPositionCode) AndAlso paramPositionCode <> "" AndAlso paramTypeCode <> "") Then
                BindGridByPositionCode(0)
            Else
                BindDataGrid(0)
                'If dtgKerja.Items.Count > 0 Then
                '    btnChoose.Disabled = False
                'Else
                '    btnChoose.Disabled = True
                '    MessageBox.Show("Data tidak ditemukan")
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'paramTypeCode = Request.QueryString("typeCode").Trim()
        'paramPositionCode = Request.QueryString("positionCode").Trim()
        If (Not IsNothing(paramTypeCode) And Not IsNothing(paramPositionCode)) Then
            BindGridByPositionCode(0)
        Else
            BindDataGrid(0)
        End If

        If dtgKerja.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgKerja_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgKerja.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As DeskripsiKodeKerja = CType(e.Item.DataItem, DeskripsiKodeKerja)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

                If Not IsNothing(RowValue.Status) Then
                    If RowValue.Status = "1" Then
                        lblStatus.Text = "Aktif"
                    Else
                        lblStatus.Text = "Tidak Aktif"
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dtgKerja_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgKerja.PageIndexChanged
        dtgKerja.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgKerja.CurrentPageIndex)
    End Sub

    Private Sub dtgKerja_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgKerja.SortCommand
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
        dtgKerja.CurrentPageIndex = 0
        BindDataGrid(dtgKerja.CurrentPageIndex)
    End Sub

End Class
