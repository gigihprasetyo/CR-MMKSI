Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class PopUpSPL
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSPLNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSPLNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblCustName As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtCustName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgSPLHeader As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnPilih As System.Web.UI.WebControls.Button

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
    Private _SPLFacade As New SPLFacade(User)
    Private sessHelper As New SessionHelper
    Private RencanaTebus As DateTime
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        RencanaTebus = System.Convert.ToDateTime(Request.QueryString("rencanatebus"))
        If Not IsPostBack Then
            ViewState("PopUpSPL.IndexSession") = Guid.NewGuid().ToString()
            Initialize()
            txtCustName.Text = Request.QueryString("projectname").ToString().Replace(" ", ";")
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
        If dgSPLHeader.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub
    Private Sub dgSPLHeader_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSPLHeader.ItemDataBound
        'Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowData As SPL = CType(e.Item.DataItem, SPL)
            'If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
            '    Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            '    e.Item.Cells(0).Controls.Add(rdbChoice)
            'End If

            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            lblDealerName.Text = FormatDealerName(RowData.DealerName)

            Dim lblSPLNumber As Label = CType(e.Item.FindControl("lblSPLNumber"), Label)
            lblSPLNumber.Text = RowData.SPLNumber

        End If
    End Sub
    Private Function FormatDealerName(ByVal _DealerName As String) As String
        Dim result As String
        Dim DealerCount As Integer = _DealerName.Split(";").Length - 1
        Dim PosToInsert As Integer = 0
        For i As Integer = 1 To DealerCount
            PosToInsert = _DealerName.IndexOf(";", PosToInsert) + 1
            If i Mod 10 = 0 Then
                _DealerName = _DealerName.Insert(PosToInsert, "<br>")
            End If
        Next

        Return _DealerName

    End Function

    Private Sub dgSPLHeader_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSPLHeader.PageIndexChanged
        dgSPLHeader.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSPLHeader.CurrentPageIndex)
    End Sub
    Private Sub dgSPLHeader_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSPLHeader.SortCommand
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
        dgSPLHeader.SelectedIndex = -1
        dgSPLHeader.CurrentPageIndex = 0
        BindDataGrid(dgSPLHeader.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPL), "Status", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(SPL), "ApprovalStatus", MatchType.Exact, 5))
        If txtSPLNumber.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SPL), "SPLNumber", MatchType.Exact, txtSPLNumber.Text.Trim()))
        End If
        If txtDealerName.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SPL), "DealerName", MatchType.Exact, txtDealerName.Text.Trim()))
        End If
        If txtCustName.Text.Length > 0 Then
            Dim str() As String = txtCustName.Text.Split(";".ToCharArray())
            For i As Integer = 0 To str.Length - 1
                criterias.opAnd(New Criteria(GetType(SPL), "CustomerName", MatchType.[Partial], str(i)))
            Next
        End If
        criterias.opAnd(New Criteria(GetType(SPL), "ValidFrom", MatchType.LesserOrEqual, RencanaTebus))
        criterias.opAnd(New Criteria(GetType(SPL), "ValidTo", MatchType.GreaterOrEqual, RencanaTebus))
        arrList = _SPLFacade.RetrieveByCriteria(criterias, idxPage + 1, dgSPLHeader.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSPLHeader.DataSource = arrList
        dgSPLHeader.VirtualItemCount = totalRow
        dgSPLHeader.DataBind()
        sessHelper.SetSession("PopUpSPL.SessGrid." & ViewState("PopUpSPL.IndexSession"), arrList)
    End Sub

    Private Sub Initialize()
        txtSPLNumber.Text = ""
        txtDealerName.Text = ""
        txtCustName.Text = ""
        ViewState("CurrentSortColumn") = "SPLNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        btnSearch_Click(Nothing, Nothing)
    End Sub

    Private Sub btnPilih_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPilih.Click
        Dim bcheck As Boolean = False
        For Each dtgItem As DataGridItem In dgSPLHeader.Items
            Dim rdo As RadioButton = CType(dtgItem.FindControl("radioBtn"), RadioButton)
            If (rdo.Checked) Then
                bcheck = True
                Exit For
            End If
        Next

        If bcheck Then
            If Not GetCheckedItem() Is Nothing Then
                Dim objSPL As SPL = GetCheckedItem()
                Dim returnValue As String = objSPL.SPLNumber
                Dim strScriptJS As String = String.Empty
                strScriptJS = "if ('" & returnValue & "' != '') {"
                strScriptJS += "if (navigator.appName != 'Microsoft Internet Explorer') {"
                strScriptJS += "window.close();"
                strScriptJS += "window.opener.dialogWin.returnFunc('" & returnValue & "');"
                strScriptJS += "}"
                strScriptJS += "else {"
                strScriptJS += "window.returnValue = '" & returnValue & "';"
                strScriptJS += "window.close();"
                strScriptJS += "}"
                strScriptJS += "}"
                strScriptJS += "else {"
                strScriptJS += "alert('Pilih Nomor SPL');"
                strScriptJS += "}"
                Response.Write("<script language='javascript'>" & strScriptJS & "</script>")
            Else
                MessageBox.Show("Pilih Nomor SPL")
            End If
        Else
            MessageBox.Show("Pilih Nomor SPL")
        End If
    End Sub
    Private Function GetCheckedItem() As SPL
        Dim arlCheckedItem As ArrayList = New ArrayList
        dgSPLHeader.DataSource = CType(sessHelper.GetSession("PopUpSPL.SessGrid." & ViewState("PopUpSPL.IndexSession")), ArrayList)
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgSPLHeader.Items
            nIndeks = dtgItem.ItemIndex
            Dim objSPL As SPL = CType(CType(dgSPLHeader.DataSource, ArrayList)(nIndeks), SPL)
            Dim rdo As RadioButton = CType(dtgItem.FindControl("radioBtn"), RadioButton)
            If (rdo.Checked) Then
                arlCheckedItem.Add(objSPL)
                Exit For
            End If
        Next
        Dim _objSPL As New SPL
        If arlCheckedItem.Count > 0 Then
            _objSPL = CType(arlCheckedItem(0), SPL)
        End If
        Return _objSPL
    End Function

End Class
