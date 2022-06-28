#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.IndentPart
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class PopUpIndentPartSelectionOne
    Inherits System.Web.UI.Page

#Region "Deklarasi"
    Dim criterias As CriteriaComposite
    Dim sHelper As New SessionHelper
#End Region

#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexpage As Integer)
        Dim totalRow As Integer = 0
        If indexpage >= 0 Then
            Dim arlHeader As ArrayList = New IndentPartHeaderFacade(User).RetrieveActiveList(indexpage + 1, dtgIndentPart.PageSize, totalRow, ViewState("SortColumn"), ViewState("SortDirection"), sHelper.GetSession("crits"))
            dtgIndentPart.DataSource = arlHeader
            dtgIndentPart.VirtualItemCount = totalRow
            dtgIndentPart.DataBind()
        End If
    End Sub

    Private Sub CreateCriteria()
        Dim kdDealer As String = txtDealerCode.Text.Replace(";", "','")
        Dim tglPOStart As Date = icPODateFrom.Value
        Dim tglPOUntil As Date = icPODateUntil.Value

        criterias = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtDealerCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "Dealer.DealerCode", MatchType.InSet, "('" & kdDealer & "')"))
        End If

        If Request.QueryString("isAlokasi") <> String.Empty Then
            If Request.QueryString("isAlokasi") = "1" Then
                criterias.opAnd(New Criteria(GetType(IndentPartHeader), "StatusKTB", MatchType.GreaterOrEqual, CInt(EnumIndentPartStatus.IndentPartStatusKTB.Baru)))
                criterias.opAnd(New Criteria(GetType(IndentPartHeader), "StatusKTB", MatchType.LesserOrEqual, CInt(EnumIndentPartStatus.IndentPartStatusKTB.Rilis)))
            End If
        End If

        If Request.QueryString("MaterialType") <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "StatusKTB", MatchType.Exact, CInt(EnumIndentPartEquipStatus.EnumStatusKTB.Proses_Order)))
        Else
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "StatusKTB", MatchType.Exact, CInt(EnumIndentPartStatus.IndentPartStatusKTB.Proses)))
        End If

        criterias.opAnd(New Criteria(GetType(IndentPartHeader), "RequestDate", MatchType.GreaterOrEqual, tglPOStart))
        criterias.opAnd(New Criteria(GetType(IndentPartHeader), "RequestDate", MatchType.LesserOrEqual, tglPOUntil))

        If ddlMaterialType.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "MaterialType", MatchType.Exact, ddlMaterialType.SelectedValue))
        End If
        sHelper.SetSession("crits", criterias)
    End Sub

    Private Sub BindMaterial()
        Dim arl As ArrayList = New EnumMaterialType().RetrieveType()
        ddlMaterialType.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
        For Each imat As EnumMaterial In arl
            ddlMaterialType.Items.Add(New ListItem(imat.NameType, imat.ValType.ToString))
        Next
        ddlMaterialType.SelectedIndex = -1
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim oDealer As Dealer = sHelper.GetSession("DEALER")
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblSearchDealer.Visible = False
            txtDealerCode.Enabled = False
            txtDealerCode.BorderStyle = BorderStyle.None
            txtDealerCode.Text = oDealer.DealerCode
        End If
        If Not IsPostBack Then
            ViewState.Add("SortColumn", "RequestDate")
            ViewState.Add("SortDirection", Sort.SortDirection.ASC)
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
            BindMaterial()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgIndentPart.CurrentPageIndex = 0
        CreateCriteria()
        BindToGrid(dtgIndentPart.CurrentPageIndex)
        If dtgIndentPart.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgIndentPart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgIndentPart.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dtgIndentPart.PageSize * dtgIndentPart.CurrentPageIndex) + e.Item.ItemIndex + 1
        End If

        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As IndentPartHeader = CType(e.Item.DataItem, IndentPartHeader)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If
        End If
    End Sub

    Private Sub dtgIndentPart_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgIndentPart.SortCommand
        If e.SortExpression = ViewState("SortColumn") Then
            If ViewState("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirection", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColumn", e.SortExpression)
        BindToGrid(0)
    End Sub

    Private Sub dtgIndentPart_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgIndentPart.PageIndexChanged
        dtgIndentPart.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgIndentPart.CurrentPageIndex)
    End Sub

End Class