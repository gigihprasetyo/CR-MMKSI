Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility

Public Class FrmDealerSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSearch1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSearch2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCity As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgDealerSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.WebControls.Button
    Protected WithEvents txtSelectResult As System.Web.UI.WebControls.TextBox
    Protected countChk As Integer = 0
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
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
        If Not IsPostBack Then
            BindDdlGroup()
            BindDdlCity()
            ClearData()
        End If
    End Sub
    Private Sub ClearData()
        Me.txtSearch1.Text = String.Empty
        Me.txtSearch2.Text = String.Empty
        Me.txtDealerName.Text = String.Empty
        Me.ddlCity.SelectedIndex = 0
        Me.ddlGroup.SelectedIndex = 0


    End Sub

    Private Sub BindDdlGroup()
        Dim nTotRow As Integer = 0
        Dim nPageNumber As Integer = 1
        Dim nPageSize As Integer = 50
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ddlGroup.DataSource = New DealerGroupFacade(User).RetrieveActiveList(criterias, nPageNumber, nPageSize, nTotRow, "GroupName", Sort.SortDirection.ASC)
        ddlGroup.DataTextField = "GroupName"
        ddlGroup.DataValueField = "ID"
        ddlGroup.DataBind()
        ddlGroup.Items.Insert(0, New ListItem("Pilih Group", ""))
    End Sub

    Private Sub BindDdlCity()
        Dim nTotRow As Integer = 0
        Dim nPageNumber As Integer = 1
        Dim nPageSize As Integer = 50
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ddlCity.DataSource = New CityFacade(User).RetrieveActiveList(criterias, nPageNumber, nPageSize, nTotRow, "CityName", Sort.SortDirection.ASC)

        Me.ddlCity.DataValueField = "ID"
        Me.ddlCity.DataTextField = "CityName"
        Me.ddlCity.DataBind()
        Me.ddlCity.Items.Insert(0, New ListItem("Pilih Kota", ""))

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()
    End Sub
    Public Sub BindSearch()
        Dim nTotRow As Integer = 0
        Dim nPageNumber As Integer = 1
        Dim nPageSize As Integer = 50
        Dim TotRef As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not txtDealerName.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerName", MatchType.[Partial], txtDealerName.Text))
        End If
        If Not ddlGroup.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerGroup.ID", MatchType.Exact, ddlGroup.SelectedValue))
        End If
        If Not ddlCity.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "City.ID", MatchType.Exact, ddlCity.SelectedValue))
        End If
        If Not txtSearch1.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm1", MatchType.[Partial], txtSearch1.Text))
        End If
        If Not txtSearch2.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm2", MatchType.[Partial], txtSearch2.Text))
        End If
        dtgDealerSelection.DataSource = New DealerFacade(User).RetrieveActiveList(criterias, nPageNumber, nPageSize, nTotRow, "DealerCode", Sort.SortDirection.ASC)
        dtgDealerSelection.DataBind()
        If Me.dtgDealerSelection.Items.Count = 0 Then
            Me.btnChoose.Enabled = False
        Else
            Me.btnChoose.Enabled = True
        End If
    End Sub

    Private Sub btnChoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChoose.Click
        txtSelectResult.Text = FindSelectedDealer()
    End Sub

    Private Function FindSelectedDealer() As String
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim lblDealerCd As System.Web.UI.WebControls.Label
        Dim _dealers As New StringBuilder

        For Each oDataGridItem In dtgDealerSelection.Items
            chkExport = oDataGridItem.FindControl("CbSelection")
            If chkExport.Checked Then
                _dealers.Append(";" & CType(oDataGridItem.FindControl("lblDealerCode"), Label).Text)
                lblDealerCd = oDataGridItem.FindControl("lblDealerCode")
                countChk = countChk + 1
                Dim CBSelection As CheckBox = CType(oDataGridItem.FindControl("CBSelection"), CheckBox)
                oDataGridItem.BackColor = Color.GreenYellow
            Else
                If oDataGridItem.ItemIndex Mod 2 = 1 Then
                    oDataGridItem.BackColor = Color.LavenderBlush
                Else
                    oDataGridItem.BackColor = Color.White
                End If
            End If
        Next
        If countChk > 0 Then
            Return _dealers.ToString().Remove(0, 1)
        Else
            MessageBox.Show("Pilih salah satu atau klik Tutup")
            Exit Function
        End If
    End Function

    Private Sub dtgDealerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgDealerSelection.CurrentPageIndex * dtgDealerSelection.PageSize)
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As Dealer = CType(e.Item.DataItem, Dealer)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblGroup As Label = CType(e.Item.FindControl("lblGroup"), Label)
                If Not IsNothing(RowValue.DealerGroup) Then
                    lblGroup.Text = RowValue.DealerGroup.GroupName
                End If
                Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
                If Not IsNothing(RowValue.City) Then
                    lblCity.Text = RowValue.City.CityName
                End If
            End If
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeUpl()
    End Sub
    Private Sub closeUpl()
        Dim language As String = "javascript"
        Dim sbScript As System.Text.StringBuilder = New System.Text.StringBuilder
        sbScript.Append("<SCRIPT language=" & language & ">")
        sbScript.Append(Environment.NewLine)
        sbScript.Append("window.opener.document.all.")
        sbScript.Append(Request.QueryString("src"))
        sbScript.Append(".value = '")
        sbScript.Append(txtSelectResult.Text)
        sbScript.Append("';")
        sbScript.Append(Environment.NewLine)
        sbScript.Append("window.close();")
        sbScript.Append(Environment.NewLine)
        sbScript.Append("</SCRIPT> ")
        Me.Page.Controls.Add(New LiteralControl(sbScript.ToString()))
    End Sub


End Class