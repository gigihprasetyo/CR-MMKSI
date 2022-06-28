#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
#End Region

Public Class PopUpPositionCodeSelectionWSC
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

    Private paramVehicleTypeId As Integer
    Private paramClaimType As String
    Private paramTypeCode As String
    Private paramRefDoc As String
    Private strSql As String
    Private wscBBFlag As String = String.Empty

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Try
            wscBBFlag = Request.QueryString("wscBBFlag")
        Catch
        End Try

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

        paramClaimType = Request.QueryString("ClaimType")
        If IsNothing(Request.QueryString("chaNum")) Then
            paramTypeCode = Request.QueryString("typeCode").ToString
        Else
            Try
                paramTypeCode = New ChassisMasterFacade(User).Retrieve(Request.QueryString("chaNum").ToString).VechileColor.VechileType.VechileTypeCode
            Catch ex As Exception
                paramTypeCode = New ChassisMasterBBFacade(User).Retrieve(Request.QueryString("chaNum").ToString).VechileColor.VechileType.VechileTypeCode
            End Try
        End If
        paramRefDoc = Request.QueryString("refDoc")

        dtgPosisi.CurrentPageIndex = 0
        If (Not IsNothing(paramClaimType)) Then
            BindDataGridByVehicleType(dtgPosisi.CurrentPageIndex)
        ElseIf (IsNothing(paramClaimType)) Then
            BindDataGridByVehicleType2(dtgPosisi.CurrentPageIndex)
        Else
            BindDataGrid(dtgPosisi.CurrentPageIndex)
        End If
    End Sub

    Private Sub BindDataGridByVehicleType(indexPage As Integer)
        Dim strCurrentSortColumn As String = CType(ViewState("CurrentSortColumn"), String)
        Dim strCurrentSortDirect As String = CType(ViewState("CurrentSortDirect"), Sort.SortDirection).ToString

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
        If (paramClaimType.Trim = "Z2" OrElse paramClaimType.Trim = "Z6") AndAlso paramRefDoc.Trim = "1" Then  '---> PQR Ref
            strSql += "  and (substring(LaborCode,1,1) LIKE ''[0-9]%''"
            strSql += " or LaborCode IN (''XEE999'')) "
        End If
        If (paramClaimType.Trim = "Z4" OrElse paramClaimType.Trim = "Z6") AndAlso paramRefDoc.Trim = "0" Then   '---> Bulletin Ref
            strSql += "  and (LaborCode NOT LIKE ''[0-9]%''"
            strSql += "  and LaborCode NOT IN (''XEE999'')) "
        End If

        strSql += "  and vechileTypeCode =''" & paramTypeCode & "''"

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
            If (paramClaimType.Trim = "Z2" OrElse paramClaimType.Trim = "Z6") AndAlso paramRefDoc.Trim = "1" Then  '---> PQR Ref
                strSql += "  and (substring(LaborCode,1,1) LIKE '[0-9]%'"
                strSql += " or LaborCode IN ('XEE999')) "
            End If
            If (paramClaimType.Trim = "Z4" OrElse paramClaimType.Trim = "Z6") AndAlso paramRefDoc.Trim = "0" Then   '---> Bulletin Ref
                strSql += "  and (LaborCode NOT LIKE '[0-9]%'"
                strSql += "  and LaborCode NOT IN ('XEE999')) "
            End If

            strSql += "  and vechileTypeCode ='" & paramTypeCode & "'"

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

    Private Sub BindDataGridByVehicleType2(indexPage As Integer)
        Dim strCurrentSortColumn As String = CType(ViewState("CurrentSortColumn"), String)
        Dim strCurrentSortDirect As String = CType(ViewState("CurrentSortDirect"), Sort.SortDirection).ToString

        Dim intRecCount As Integer = 0
        Dim totalRow As Integer = 0
        Dim arr As New ArrayList
        Dim intPageSize As Integer = 10
        indexPage += 1

        Dim filterKodePosisi As String = txtKodePosisi.Text
        Dim filterDeskripsi As String = txtDeskripsi.Text
        'Dim strColumn As String = "KodePosition,Description"
        Dim strColumn As String = "ID,KodePosition,LaborCode,VechileTypeCode,Description,Status,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime"

        strSql += "exec getSelectedPageData 'V_PopUpPositionCode', " & indexPage.ToString & ", " & intPageSize.ToString
        strSql += ", '" & strColumn & "', '" & strCurrentSortColumn & "', '" & strCurrentSortDirect & "'"
        strSql += ", '1=1 "
        strSql += " and VechileTypeCode = ''" & paramTypeCode & "''"
        'strSql += " and (LaborCode NOT LIKE ''[0-9]%''"
        'strSql += " and LaborCode NOT IN (''XEE999'')) "
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
            strSql += " and VechileTypeCode = '" & paramTypeCode & "'"
            'strSql += " and (LaborCode NOT LIKE '[0-9]%'"
            'strSql += " and LaborCode NOT IN ('XEE999')) "
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

        If dtgPosisi.VirtualItemCount > 0 Then
            btnChoose.Disabled = False
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

        If (Not IsNothing(paramVehicleTypeId)) Then
            criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "Description", MatchType.[Partial], txtDeskripsi.Text))
        End If

        'criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "Status", MatchType.Exact, CType(EnumJobPositionDescription.JobPosition.Aktive , Integer)))
        Return criterias
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim objCM As New ChassisMaster
        Dim objCMBB As New ChassisMasterBB
        dtgPosisi.CurrentPageIndex = 0
        hdnRecCount.Value = "0"
        paramClaimType = Request.QueryString("ClaimType")
        If IsNothing(Request.QueryString("chaNum")) Then
            paramTypeCode = Request.QueryString("typeCode").ToString
        Else
            'paramTypeCode = New ChassisMasterFacade(User).Retrieve(Request.QueryString("chaNum").ToString).VechileColor.VechileType.VechileTypeCode
            If wscBBFlag = "0" Then
                objCM = New ChassisMasterFacade(User).Retrieve(Request.QueryString("chaNum").ToString)
                If Not IsNothing(objCM) Then
                    If Not IsNothing(objCM.VechileColor) Then
                        If Not IsNothing(objCM.VechileColor.VechileType) Then
                            paramTypeCode = objCM.VechileColor.VechileType.VechileTypeCode
                        End If
                    End If
                End If
            ElseIf wscBBFlag = "1" Then
                objCMBB = New ChassisMasterBBFacade(User).Retrieve(Request.QueryString("chaNum").ToString)
                If Not IsNothing(objCMBB) Then
                    If Not IsNothing(objCMBB.VechileColor) Then
                        If Not IsNothing(objCMBB.VechileColor.VechileType) Then
                            paramTypeCode = objCMBB.VechileColor.VechileType.VechileTypeCode
                        End If
                    End If
                End If
            End If
        End If
        paramRefDoc = Request.QueryString("refDoc")

        If (Not IsNothing(paramClaimType)) Then
            BindDataGridByVehicleType(dtgPosisi.CurrentPageIndex)
        ElseIf (IsNothing(paramClaimType)) Then
            BindDataGridByVehicleType2(dtgPosisi.CurrentPageIndex)
        Else
            BindDataGrid(dtgPosisi.CurrentPageIndex)
        End If

        If dtgPosisi.VirtualItemCount > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If

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

        paramClaimType = Request.QueryString("ClaimType")
        If IsNothing(Request.QueryString("chaNum")) Then
            paramTypeCode = Request.QueryString("typeCode").ToString
        Else
            paramTypeCode = New ChassisMasterFacade(User).Retrieve(Request.QueryString("chaNum").ToString).VechileColor.VechileType.VechileTypeCode
        End If
        paramRefDoc = Request.QueryString("refDoc")

        If (Not IsNothing(paramClaimType)) Then
            BindDataGridByVehicleType(dtgPosisi.CurrentPageIndex)
        ElseIf (IsNothing(paramClaimType)) Then
            BindDataGridByVehicleType2(dtgPosisi.CurrentPageIndex)
        Else
            BindDataGrid(dtgPosisi.CurrentPageIndex)
        End If
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

        paramClaimType = Request.QueryString("ClaimType")
        If IsNothing(Request.QueryString("chaNum")) Then
            paramTypeCode = Request.QueryString("typeCode").ToString
        Else
            paramTypeCode = New ChassisMasterFacade(User).Retrieve(Request.QueryString("chaNum").ToString).VechileColor.VechileType.VechileTypeCode
        End If
        paramRefDoc = Request.QueryString("refDoc")

        If (Not IsNothing(paramClaimType)) Then
            BindDataGridByVehicleType(dtgPosisi.CurrentPageIndex)
        ElseIf (IsNothing(paramClaimType)) Then
            BindDataGridByVehicleType2(dtgPosisi.CurrentPageIndex)
        Else
            BindDataGrid(dtgPosisi.CurrentPageIndex)
        End If
    End Sub

End Class
