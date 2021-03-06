Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Public Class PopUpCustomerList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dtgCustomerSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

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
    Dim criterias As CriteriaComposite
    Private sHelper As SessionHelper = New SessionHelper
    Private strSQL As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            Dim term As String = Request.QueryString("Tyjiuy678")
            Dim tipe As String = Request.QueryString("tipe")
            If tipe <> "" Then
                txtNama.Text = tipe
            End If
            If term = "code" OrElse term = "code2" Then
                sHelper.SetSession("SortColPopUp", "Code")
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
                BindSearch(0)
                If dtgCustomerSelection.Items.Count < 1 Then
                    Response.Write("<script languange='javascript'>alert('Tidak Ada Referensi Kode Pelanggan');window.close();</script>")
                End If
            ElseIf term = "customercode" Then
                sHelper.SetSession("SortColPopUp", "CustomerCode")
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
                BindSearch(0)
                If dtgCustomerSelection.Items.Count < 1 Then
                    Response.Write("<script languange='javascript'>alert('Tidak Ada Referensi Kode Pelanggan');window.close();</script>")
                End If
            ElseIf term = "number" Then
                sHelper.SetSession("SortColPopUp", "RequestNo")
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
                BindSearch(0)
                If dtgCustomerSelection.Items.Count < 1 Then
                    Response.Write("<script languange='javascript'>alert('Tidak Ada Referensi Nomor Pengajuan');window.close();</script>")
                End If
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch(0)
        If dtgCustomerSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub
    Private Sub ClearData()
        txtKode.Text = String.Empty
        txtNama.Text = String.Empty
    End Sub
    Public Sub BindSearch(ByVal indexPage As Integer)
        dtgCustomerSelection.CurrentPageIndex = indexPage
        Dim totalRow As Integer = 0
        Dim _arr As New ArrayList
        Dim term As String = Request.QueryString("Tyjiuy678")


        If term = "code" OrElse term = "code2" Then
            CreateCriteriaForCode()
            If Not IsNothing(criterias) Then
                _arr = New CustomerFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgCustomerSelection.PageSize, totalRow, sHelper.GetSession("SortColPopUp"), sHelper.GetSession("SortDirectionPopUp"))
            End If
        ElseIf term = "customercode" Then
            CreateCriteriaForCustomerCode()
            _arr = New CustomerRequestFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgCustomerSelection.PageSize, totalRow, sHelper.GetSession("SortColPopUp"), sHelper.GetSession("SortDirectionPopUp"))
        ElseIf term = "number" Then
            CreateCriteriaForNumber()
            _arr = New CustomerRequestFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgCustomerSelection.PageSize, totalRow, sHelper.GetSession("SortColPopUp"), sHelper.GetSession("SortDirectionPopUp"))
        End If
        If _arr.Count > 0 Then
            If indexPage >= 0 Then
                dtgCustomerSelection.DataSource = _arr
                dtgCustomerSelection.VirtualItemCount = totalRow
                sHelper.SetSession("DATA", _arr)
                dtgCustomerSelection.DataBind()
            End If
        Else
            dtgCustomerSelection.DataSource = New ArrayList
            MessageBox.Show("Data tidak ditemukan")
        End If


        If dtgCustomerSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub
    Private Sub CreateCriteriaForNumber()
        criterias = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtNama.Text.Trim <> "" Then
            Dim i As Integer = 0
            For Each item As String In txtNama.Text.Split(" ")
                If i = 0 Then
                    criterias.opAnd(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], item))
                    criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], item))
                Else
                    criterias.opOr(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], item))
                    criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], item))
                End If
                i = i + 1
            Next
        End If

        If txtKode.Text <> "" Then
            'BUG 1750
            'criterias.opAnd(New Criteria(GetType(CustomerRequest), "ReffCode", MatchType.Partial, txtKode.Text.Trim))
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "RequestNo", MatchType.[Partial], txtKode.Text.Trim))
        End If
        criterias.opAnd(New Criteria(GetType(CustomerRequest), "Dealer.ID", MatchType.Exact, CType(sHelper.GetSession("Dealer"), Dealer).ID))
    End Sub
    Private Sub CreateCriteriaForCustomerCode()
        criterias = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CustomerRequest), "CustomerCode", MatchType.No, String.Empty))
        If txtNama.Text.Trim <> "" Then
            Dim i As Integer = 0
            For Each item As String In txtNama.Text.Split(" ")
                If i = 0 Then
                    criterias.opAnd(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], item))
                    criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], item))
                Else
                    criterias.opOr(New Criteria(GetType(CustomerRequest), "Name1", MatchType.[Partial], item))
                    criterias.opOr(New Criteria(GetType(CustomerRequest), "Name2", MatchType.[Partial], item))
                End If
                i = i + 1
            Next
        End If

        If txtKode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(CustomerRequest), "CustomerCode", MatchType.[Partial], txtKode.Text.Trim))
        End If

    End Sub
    Private Sub CreateCriteriaForCode()
        Dim term As String = Request.QueryString("Tyjiuy678")
        Dim strDealerCode As String = Request.QueryString("DealerCode")
        Dim _containerID As String = String.Empty
        'Dim _criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '_criterias.opAnd(New Criteria(GetType(CustomerDealer), "Dealer.ID", MatchType.Exact, CType(sHelper.GetSession("Dealer"), Dealer).ID))

        'If txtNama.Text.Trim <> "" Then
        '    Dim i As Integer = 0
        '    For Each item As String In txtNama.Text.Split(" ")
        '        If i = 0 Then
        '            _criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.Name1", MatchType.Partial, item))
        '            _criterias.opOr(New Criteria(GetType(CustomerDealer), "Customer.Name2", MatchType.Partial, item))
        '        Else
        '            _criterias.opOr(New Criteria(GetType(CustomerDealer), "Customer.Name1", MatchType.Partial, item))
        '            _criterias.opOr(New Criteria(GetType(CustomerDealer), "Customer.Name2", MatchType.Partial, item))
        '        End If
        '        i = i + 1
        '    Next
        'End If
        'If txtKode.Text <> "" Then
        '    _criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.Code", MatchType.Partial, txtKode.Text.Trim))
        'End If

        Dim critNama As String = ""

        If txtNama.Text.Trim <> "" Then
            Dim i As Integer = 0
            For Each item As String In txtNama.Text.Split(" ")
                If i = 0 Then
                    critNama += " and c.Name1 like '%" & item & "%' "
                    critNama += " or c.Name2 like '%" & item & "%' "
                Else
                    critNama += " or c.Name1 like '%" & item & "%' "
                    critNama += " or c.Name2 like '%" & item & "%' "

                End If
                i = i + 1
            Next
        End If

        Dim critKode As String = ""

        If txtKode.Text <> "" Then
            critKode = " and c.Code like '%" & txtKode.Text.Trim & "%' "
        End If

        Dim oDealer As Dealer = New DealerFacade(User).Retrieve(strDealerCode)
        If term = "code2" Then
            strSQL = "select NewCustomerID from CustomerDealer cd join Customer c on(cd.NewCustomerID=C.ID) where cd.RowStatus=" & CType(DBRowStatus.Active, Short) & " and cd.DealerID=" & oDealer.ID & critKode & critNama
        Else
            strSQL = "select NewCustomerID from CustomerDealer cd join Customer c on(cd.NewCustomerID=C.ID) where cd.RowStatus=" & CType(DBRowStatus.Active, Short) & " and cd.DealerID=" & CType(sHelper.GetSession("Dealer"), Dealer).ID & critKode & critNama
        End If

        criterias = New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Customer), "ID", MatchType.InSet, "(" & strSQL & ")"))

        'Dim temp As ArrayList = New CustomerDealerFacade(User).RetrieveByCriteria(_criterias)
        'If temp.Count > 0 Then
        '    For Each item As CustomerDealer In temp
        '        _containerID += item.Customer.ID.ToString + ","
        '    Next
        '    _containerID = _containerID.Substring(0, _containerID.Length - 1)
        '    criterias.opAnd(New Criteria(GetType(Customer), "ID", MatchType.InSet, "(" & _containerID & ")"))
        'Else
        '    criterias = Nothing
        'End If
    End Sub
    Private Sub dtgCustomerSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCustomerSelection.PageIndexChanged
        dtgCustomerSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch(dtgCustomerSelection.CurrentPageIndex)
    End Sub
    Private Sub dtgCustomerSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCustomerSelection.SortCommand
        If e.SortExpression = sHelper.GetSession("SortColPopUp") Then
            If sHelper.GetSession("SortDirectionPopUp") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortColPopUp", e.SortExpression)
        dtgCustomerSelection.SelectedIndex = -1
        dtgCustomerSelection.CurrentPageIndex = 0
        BindSearch(dtgCustomerSelection.CurrentPageIndex)
    End Sub

    Private Sub dtgCustomerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCustomerSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim term As String = Request.QueryString("Tyjiuy678")
        If Not e.Item.DataItem Is Nothing Then
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)

                If term = "code" OrElse term = "code2" Then
                    dtgCustomerSelection.Columns(2).Visible = False
                    dtgCustomerSelection.Columns(3).Visible = False
                    Dim lblTempCode As Label = CType(e.Item.FindControl("lblTempCode"), Label)
                    Dim RowValue As Customer = CType(e.Item.DataItem, Customer)
                    lblCity.Text = RowValue.City.CityName
                    lblTempCode.Text = RowValue.Code
                ElseIf term = "customercode" Then
                    dtgCustomerSelection.Columns(1).Visible = False
                    dtgCustomerSelection.Columns(2).Visible = False
                    Dim lblCustomerCode As Label = CType(e.Item.FindControl("lblTempCustomerCode"), Label)
                    Dim RowValue As CustomerRequest = CType(e.Item.DataItem, CustomerRequest)
                    lblCity.Text = New CityFacade(User).Retrieve(RowValue.CityID).CityName
                    lblCustomerCode.Text = RowValue.CustomerCode
                ElseIf term = "number" Then
                    dtgCustomerSelection.Columns(1).Visible = False
                    dtgCustomerSelection.Columns(3).Visible = False
                    Dim lblTempNumber As Label = CType(e.Item.FindControl("lblTempNumber"), Label)
                    Dim RowValue As CustomerRequest = CType(e.Item.DataItem, CustomerRequest)
                    lblCity.Text = New CityFacade(User).Retrieve(RowValue.CityID).CityName
                    lblTempNumber.Text = RowValue.RequestNo
                End If
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If

        End If
    End Sub
End Class
