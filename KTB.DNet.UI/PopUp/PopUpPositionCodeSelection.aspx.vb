#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
#End Region

Public Class PopUpPositionCodeSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodePosisi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgPosisi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents hdnRecCount As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
        End If
    End Sub

    Private Sub ClearData()
        Me.txtKodePosisi.Text = String.Empty
        Me.txtDeskripsi.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "KodePosition"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
        dtgPosisi.CurrentPageIndex = 0
        'BindDataGrid(dtgPosisi.CurrentPageIndex)
        BindDataGridByVehicleType(dtgPosisi.CurrentPageIndex)
    End Sub

    Private Sub BindDataGridByVehicleType(indexPage As Integer)
        Dim strSql As String = String.Empty
        Dim strCurrentSortColumn As String = CType(ViewState("CurrentSortColumn"), String)
        Dim strCurrentSortDirect As String = CType(ViewState("CurrentSortDirect"), Sort.SortDirection).ToString
        Dim paramTypeCode As String = ""
        If Not IsNothing(Request.QueryString("vCode")) Then
            paramTypeCode = Request.QueryString("vCode")
        End If
        Dim intRecCount As Integer = 0
        Dim totalRow As Integer = 0
        Dim arr As New ArrayList
        Dim intPageSize As Integer = dtgPosisi.PageSize
        indexPage += 1

        Dim filterKodePosisi As String = txtKodePosisi.Text
        Dim filterDeskripsi As String = txtDeskripsi.Text
        Dim strColumn As String = "ID,KodePosition,LaborCode,Description,Status,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime"

        strSql += "exec getSelectedPageData 'V_PopUpPositionCode', " & indexPage.ToString & ", " & intPageSize.ToString
        strSql += ", '" & strColumn & "', '" & strCurrentSortColumn & "', '" & strCurrentSortDirect & "'"
        strSql += ", '1=1 "
        strSql += "  and (substring(LaborCode,1,1) LIKE ''[0-9]%''"
        strSql += " or LaborCode IN (''XEE999'')) "

        If paramTypeCode <> "" Then
            strSql += "  and vechileTypeCode =''" & paramTypeCode & "''"
        End If


        If filterKodePosisi.Trim <> "" Then
            strSql += "  and KodePosition like ''%" & filterKodePosisi & "%''"
        End If
        If filterDeskripsi.Trim <> "" Then
            strSql += "  and Description like ''%" & filterDeskripsi & "%''"
        End If
        strSql += "'"

        Dim deskripsiPositionCodeList As ArrayList = New DeskripsiPositionCodeFacade(User).DoRetrieveArrayList(strSql)
        dtgPosisi.DataSource = deskripsiPositionCodeList

        If hdnRecCount.Value.Trim = "0" Then
            strSql = "select * from V_PopUpPositionCode "
            strSql += " where 1=1 "
            strSql += "  and (substring(LaborCode,1,1) LIKE '[0-9]%'"
            strSql += " or LaborCode IN ('XEE999')) "
            If paramTypeCode <> "" Then
                strSql += "  and vechileTypeCode ='" & paramTypeCode & "'"
            End If


            If filterKodePosisi.Trim <> "" Then
                strSql += "  and KodePosition like '%" & filterKodePosisi & "%'"
            End If
            If filterDeskripsi.Trim <> "" Then
                strSql += "  and Description like '%" & filterDeskripsi & "%'"
            End If

            Dim DSList As DataSet = New DeskripsiPositionCodeFacade(User).DoRetrieveDataset(strSql)

            If DSList.Tables(0).Rows.Count > 0 Then
                'intRecCount = CType(deskripsiPositionCodeList(0), DeskripsiKodePosisi).RecCount
                intRecCount = DSList.Tables(0).Rows.Count
            Else
                intRecCount = 0
            End If
            hdnRecCount.Value = intRecCount
        End If
        dtgPosisi.VirtualItemCount = CType(hdnRecCount.Value.Trim, Integer)
        dtgPosisi.DataBind()

        If dtgPosisi.VirtualItemCount > 0 Then
            btnChoose.Disabled = False
        End If
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arr As New ArrayList

        Try
            arr = New DeskripsiPositionCodeFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, _
                    dtgPosisi.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgPosisi.DataSource = arr
            dtgPosisi.VirtualItemCount = totalRow
            dtgPosisi.DataBind()

        Catch ex As HttpException
            dtgPosisi.CurrentPageIndex = 0
            arr = New DeskripsiPositionCodeFacade(User).RetrieveActiveList(CriteriaSearch(), dtgPosisi.CurrentPageIndex + 1, _
                    dtgPosisi.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgPosisi.DataSource = arr
            dtgPosisi.VirtualItemCount = totalRow
            dtgPosisi.DataBind()

        End Try

        If dtgPosisi.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodePosisi.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "KodePosition", MatchType.StartsWith, txtKodePosisi.Text))
        End If
        If txtDeskripsi.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "Description", MatchType.[Partial], txtDeskripsi.Text))
        End If
        If IsNothing(Request.QueryString("vCode")) Then
            Dim vCode As String = Request.QueryString("vCode")
            criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "Description", MatchType.[Partial], vCode))
        End If
        'criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "Status", MatchType.Exact, CType(EnumJobPositionDescription.JobPosition.Aktive , Integer)))
        Return criterias
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgPosisi.CurrentPageIndex = 0
        'BindDataGrid(dtgPosisi.CurrentPageIndex)
        BindDataGridByVehicleType(dtgPosisi.CurrentPageIndex)
    End Sub

    Private Sub dtgPosisi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPosisi.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
        End If

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As DeskripsiKodePosisi = CType(e.Item.DataItem, DeskripsiKodePosisi)
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

    Private Sub dtgPosisi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPosisi.PageIndexChanged
        dtgPosisi.CurrentPageIndex = e.NewPageIndex
        BindDataGridByVehicleType(dtgPosisi.CurrentPageIndex)
        'BindDataGrid(dtgPosisi.CurrentPageIndex)
    End Sub

    Private Sub dtgPosisi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPosisi.SortCommand
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
        dtgPosisi.CurrentPageIndex = 0
        BindDataGridByVehicleType(dtgPosisi.CurrentPageIndex)
        'BindDataGrid(dtgPosisi.CurrentPageIndex)
    End Sub

End Class
