Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class PopUpCustomerSelection
    Inherits System.Web.UI.Page

    Dim criterias As CriteriaComposite
    Private sHelper As SessionHelper = New SessionHelper
    Private facFleetCustomer As New FleetCustomerFacade(User)
    Private ReadOnly varSessFCCustomer As String = "sessFleettoCustomer"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            Dim indexPage As Integer = 0
            Dim totalRow As Integer = 0
            Dim fleetCustomerID As String = Request.QueryString("Tyjiuy678")
            Dim sessCustomerList As ArrayList = sHelper.GetSession(varSessFCCustomer)
            Dim strCityID As String = String.Empty
            Dim strCustomerIdNotIn As String = String.Empty

            If Not String.IsNullorEmpty(fleetCustomerID) Then
                Dim objFleetCustomer As FleetCustomer = facFleetCustomer.Retrieve(CInt(fleetCustomerID))

                ' if cv or ud add filter by city
                If (objFleetCustomer.TypeIndex = 1 Or objFleetCustomer.TypeIndex = 2) Then
                    strCityID = objFleetCustomer.City.ID
                End If

                ' exclude customer name already selected
                If sessCustomerList.Count > 0 Then
                    For i As Integer = 0 To sessCustomerList.Count - 1
                        Dim objFCCustomer As FleetCustomerToCustomer = sessCustomerList(i)
                        If i = sessCustomerList.Count - 1 Then
                            strCustomerIdNotIn += objFCCustomer.CustomerID.ID.ToString()
                        Else
                            strCustomerIdNotIn += objFCCustomer.CustomerID.ID.ToString() + ","
                        End If
                    Next
                End If

                Dim dtSet As DataSet = facFleetCustomer.RetrieveSp("EXEC sp_FCD_GetCustomerByFleetCustomerName " & objFleetCustomer.Name & ",'" & strCustomerIdNotIn & "'," & strCityID & "")
                If dtSet.Tables.Count > 0 Then
                    dtgCustomerSelection.DataSource = dtSet.Tables(0)
                    dtgCustomerSelection.VirtualItemCount = totalRow
                    sHelper.SetSession("DATA", dtSet.Tables(0))
                    dtgCustomerSelection.DataBind()
                End If

                If dtgCustomerSelection.Items.Count > 0 Then
                    btnChoose.Disabled = False
                Else
                    btnChoose.Disabled = True
                End If
            Else
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If txtKode.Text.Trim.Length > 0 Then
                    crit.opAnd(New Criteria(GetType(Customer), "Code", MatchType.[Partial], txtKode.Text))
                End If
                If txtNama.Text.Trim.Length > 0 Then
                    crit.opAnd(New Criteria(GetType(Customer), "Name1", MatchType.[Partial], txtNama.Text))
                End If

                sHelper.SetSession("SortColPopUp", "ID")
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.DESC)

                Dim arlCustomer As ArrayList = New CustomerFacade(User).RetrieveByCriteria(crit, indexPage, dtgCustomerSelection.PageSize, totalRow, "ID", Sort.SortDirection.DESC)

                If arlCustomer.Count > 0 Then
                    dtgCustomerSelection.DataSource = arlCustomer
                    dtgCustomerSelection.VirtualItemCount = totalRow
                    dtgCustomerSelection.DataBind()
                End If

                If dtgCustomerSelection.Items.Count > 0 Then
                    btnChoose.Disabled = False
                Else
                    btnChoose.Disabled = True
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
        Dim sessCustomerList As ArrayList = sHelper.GetSession(varSessFCCustomer)
        Dim _arr As New ArrayList

        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not String.IsNullorEmpty(txtKode.Text) Then
            crt.opAnd(New Criteria(GetType(Customer), "Code", MatchType.Partial, txtKode.Text))
        End If
        If Not String.IsNullorEmpty(txtNama.Text) Then
            crt.opAnd(New Criteria(GetType(Customer), "CompleteName", MatchType.Partial, txtNama.Text.Replace(" ", "%")))
        End If

        ' exclude customer name already selected
        If Not IsNothing(sessCustomerList) Then
            If sessCustomerList.Count > 0 Then
                Dim str As String = String.Empty
                For i As Integer = 0 To sessCustomerList.Count - 1
                    Dim objFCCustomer As FleetCustomerToCustomer = sessCustomerList(i)
                    If i = sessCustomerList.Count - 1 Then
                        str += objFCCustomer.CustomerID.ID.ToString()
                    Else
                        str += objFCCustomer.CustomerID.ID.ToString() + ","
                    End If
                Next

                crt.opAnd(New Criteria(GetType(Customer), "ID", MatchType.NotInSet, str))
            End If
        End If

        _arr = New CustomerFacade(User).RetrieveByCriteria(crt, indexPage + 1, dtgCustomerSelection.PageSize, totalRow, sHelper.GetSession("SortColPopUp"), sHelper.GetSession("SortDirectionPopUp"))

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
                Dim RowValue As Customer = CType(e.Item.DataItem, Customer)
                Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
                If Not IsNothing(RowValue.City) Then
                    lblCity.Text = RowValue.City.CityName
                End If

            End If

        End If
    End Sub

End Class