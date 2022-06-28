#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.FinishUnit

#End Region

Public Class PopUpPQRSelectionSingle
    Inherits System.Web.UI.Page

#Region " custom Declaration "
    Protected countChk As Integer = 0
    Private objDealer As Dealer
    Private hashPQRType As Hashtable
    Private oCategoryFacade As New CategoryFacade(User)
    Private oLoginUser As UserInfo
    Private _sesHelper As New SessionHelper()
#End Region


#Region " Custom Method "
    Private Sub BindDDl()
        hashPQRType = New StandardCodeFacade(User).RetrieveHashByCategory("PQRType")
        ViewState("HashPQRType") = hashPQRType
        ddlPqrType.Items.Clear()
        ddlPqrType.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each key As Integer In hashPQRType.Keys
            Select Case key
                Case 0, 2, 3
                    ddlPqrType.Items.Add(New ListItem(hashPQRType(key), key))
            End Select
        Next

        Dim arlStatusPQR As ArrayList = New EnumPQR().RetrievePQRStatus()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each key As EnumPQRStatus In arlStatusPQR
            Select Case key.ValStatus
                Case 1, 2, 3, 4   'Rilis & Selesai
                    ddlStatus.Items.Add(New ListItem(key.NameStatus, key.ValStatus))
            End Select
        Next
        'ddlStatus.DataSource = New EnumPQR().RetrievePQRStatus()
        'ddlStatus.DataTextField = "NameStatus"
        'ddlStatus.DataValueField = "ValStatus"
        'ddlStatus.DataBind()
        'ddlStatus.Items.Insert(0, New ListItem("Silahkan Pilih", -1))

        ddlKategori.Items.Clear()
        ddlKategori.DataSource = oCategoryFacade.RetrieveActiveList("MMC")
        ddlKategori.DataTextField = "CategoryCode"
        ddlKategori.DataValueField = "ID"
        ddlKategori.DataBind()
        ddlKategori.Items.Insert(0, New ListItem("Silahkan Pilih", -1))

    End Sub

    Private Sub ClearData()
        Me.lblDealerCode.Text = CType(Session("DEALER"), Dealer).DealerCode & " / " & CType(Session("sessDealer"), Dealer).DealerName
        Me.txtPQRNo.Text = String.Empty
        Me.chkFilterTanggal.Checked = False
        Me.icTglApplyDari.Value = Now
        Me.icTglApplySampai.Value = Now
        Me.ddlPqrType.SelectedIndex = 0
        Me.ddlStatus.SelectedIndex = 0
        Me.ddlKategori.SelectedIndex = 0
    End Sub

#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        oLoginUser = _sesHelper.GetSession("LOGINUSERINFO")

        If Not IsPostBack Then
            ViewState.Add("CurrentSortColumn", "PQRNo")
            ViewState.Add("CurrentSortDirect", Sort.SortDirection.ASC)

            BindDDl()
            ClearData()
            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub

    Public Sub BindSearch(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.InSet, "(1,2,3,4)"))

        If ddlPqrType.SelectedIndex > 0 Then
            Dim pqrType As Integer = CInt(ddlPqrType.SelectedValue)
            If pqrType >= 0 Then
                criterias.opAnd(New Criteria(GetType(PQRHeader), "PQRType", MatchType.Exact, pqrType))
            End If
        Else
            criterias.opAnd(New Criteria(GetType(PQRHeader), "PQRType", MatchType.InSet, "(0,2,3,4)"))
        End If
        If lblDealerCode.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(PQRHeader), "Dealer.DealerCode", MatchType.[Partial], lblDealerCode.Text.Split(" / ")(0)))
        If txtPQRNo.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(PQRHeader), "PQRNo", MatchType.[Partial], txtPQRNo.Text))

        'criterias.opAnd(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.No, CType(EnumPQR.PQRStatus.Batal, Short)))
        If chkFilterTanggal.Checked Then
            criterias.opAnd(New Criteria(GetType(PQRHeader), "DocumentDate", MatchType.GreaterOrEqual, icTglApplyDari.Value.AddDays(0)))
            criterias.opAnd(New Criteria(GetType(PQRHeader), "DocumentDate", MatchType.Lesser, icTglApplySampai.Value.AddDays(1)))
        End If

        If ddlStatus.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.Exact, ddlStatus.SelectedValue))
        If ddlKategori.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(PQRHeader), "Category.ID", MatchType.Exact, ddlKategori.SelectedValue))

        Dim _arrPQRHeader As ArrayList = New PQRHeaderFacade(User).RetrieveByCriteria(criterias, currentPageIndex + 1, dgPQRList.PageSize, total, _
                                          CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgPQRList.VirtualItemCount = total
        dgPQRList.DataSource = _arrPQRHeader
        dgPQRList.DataBind()
    End Sub

    Private Sub dgPQRList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPQRList.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim objPQRHeader As PQRHeader = CType(e.Item.DataItem, PQRHeader)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (e.Item.ItemIndex + 1) + (dgPQRList.CurrentPageIndex * dgPQRList.PageSize)

                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                Dim lblPQRType As Label = CType(e.Item.FindControl("lblPQRType"), Label)
                hashPQRType = ViewState("HashPQRType")
                lblPQRType.Text = hashPQRType(objPQRHeader.PQRType)

                Select Case CType(objPQRHeader.RowStatus, EnumPQR.PQRStatus)
                    Case EnumPQR.PQRStatus.Baru
                        lblStatus.Text = EnumPQR.PQRStatus.Baru.ToString
                    Case EnumPQR.PQRStatus.Batal
                        lblStatus.Text = EnumPQR.PQRStatus.Batal.ToString
                    Case EnumPQR.PQRStatus.Proses
                        lblStatus.Text = EnumPQR.PQRStatus.Proses.ToString
                    Case EnumPQR.PQRStatus.Rilis
                        lblStatus.Text = EnumPQR.PQRStatus.Rilis.ToString
                    Case EnumPQR.PQRStatus.Selesai
                        lblStatus.Text = EnumPQR.PQRStatus.Selesai.ToString
                    Case EnumPQR.PQRStatus.Validasi
                        lblStatus.Text = EnumPQR.PQRStatus.Validasi.ToString
                    Case Else
                End Select
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch(dgPQRList.CurrentPageIndex)
        If dgPQRList.Items.Count > 0 Then
            'btnChoose.Disabled = False
            'Else
            '    btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dgPQRList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPQRList.SortCommand
        If e.SortExpression = ViewState("CurrentSortColumn") Then
            If ViewState("CurrentSortDirect") = Sort.SortDirection.ASC Then
                ViewState.Add("CurrentSortDirect", Sort.SortDirection.DESC)
            Else
                ViewState.Add("CurrentSortDirect", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("CurrentSortColumn", e.SortExpression)
        BindSearch(dgPQRList.CurrentPageIndex)
    End Sub

    Private Sub dgPQRList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPQRList.PageIndexChanged
        dgPQRList.CurrentPageIndex = e.NewPageIndex
        BindSearch(dgPQRList.CurrentPageIndex)
    End Sub

#End Region


End Class