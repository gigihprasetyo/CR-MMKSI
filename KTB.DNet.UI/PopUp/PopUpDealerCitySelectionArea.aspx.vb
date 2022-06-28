#Region "Custom NameSpace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
#End Region

Public Class PopUpDealerCitySelectionArea
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState.Add("SortCol", "CityCode")
            ViewState.Add("SortDir", Sort.SortDirection.ASC)
            ViewState.Add("Area1ID", CStr(Request.QueryString("Area1ID")))
            ViewState.Add("DealerCityID", CStr(Request.QueryString("DealerCityID")))
            ClearData()
            BindSearch()
        End If
    End Sub

    Private Sub ClearData()
        Me.txtCityName.Text = String.Empty
        BindSearch()
    End Sub

    Public Sub BindSearch()
        Dim strAreaID1 As String = ViewState("Area1ID")

        Dim sqlCmd As String = String.Empty
        sqlCmd = "SELECT DISTINCT b.ID "
        sqlCmd += "FROM Dealer a "
        sqlCmd += "JOIN City b on b.ID = a.CityID AND b.RowStatus = 0 "
        sqlCmd += "JOIN Area1 c on c.ID = a.Area1ID AND c.RowStatus = 0 "
        sqlCmd += "JOIN Province d on d.ID = b.ProvinceID AND d.RowStatus = 0 "
        sqlCmd += "WHERE A.RowStatus = 0 "
        sqlCmd += String.Format("AND c.ID = {0} ", strAreaID1)
        If txtCityName.Text.Trim <> String.Empty Then
            sqlCmd += String.Format("AND b.CityName LIKE '%{0}%' ", txtCityName.Text.Trim)
        End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(City), "ID", MatchType.InSet, "(" & sqlCmd & ")"))

        dtgDealerCitySelection.DataSource = New CityFacade(User).RetrieveActiveList(criterias, ViewState("SortCol"), ViewState("SortDir"))
        dtgDealerCitySelection.DataBind()
        If dtgDealerCitySelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgDealerCitySelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerCitySelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As City = CType(e.Item.DataItem, City)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim strDealerCityID As String() = CType(ViewState("DealerCityID"), String).Split(";")
                Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)

                For Each strCityCode As String In strDealerCityID
                    If strCityCode = RowValue.CityCode Then
                        chkItemChecked.Checked = True
                        Exit For
                    End If
                Next

                Dim lblCityCode As Label = CType(e.Item.FindControl("lblCityCode"), Label)
                If Not IsNothing(RowValue) Then
                    lblCityCode.Text = RowValue.CityCode
                End If
                Dim lblCityName As Label = CType(e.Item.FindControl("lblCityName"), Label)
                If Not IsNothing(RowValue) Then
                    lblCityName.Text = RowValue.CityName
                End If
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()
    End Sub

    Private Sub dtgDealerCitySelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerCitySelection.SortCommand
        If e.SortExpression = ViewState("SortCol") Then
            If ViewState("SortDir") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDir", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDir", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortCol", e.SortExpression)
        BindSearch()
    End Sub

    Private Sub dtgDealerCitySelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerCitySelection.PageIndexChanged
        dtgDealerCitySelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub
End Class