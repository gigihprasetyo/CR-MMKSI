#Region "Custom Namsepace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.UI.Helper
#End Region


Public Class FrmListGRMaterialPromotion
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealers As System.Web.UI.WebControls.Label
    Protected WithEvents txtRequestNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblRequestNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeBarang As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKodeBarang As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoGI As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dgAlokasi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents icTglGIStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglGIEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents DdlStatusUbah As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNoGI As System.Web.UI.WebControls.Label
    Protected WithEvents lblNo As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variable"
    Private objDealer As Dealer
    Private sessHelper As SessionHelper = New SessionHelper
    Dim mode As Integer = 1
#End Region

#Region "Custom Method"
    Private Sub bindddl()
        DdlStatusUbah.DataSource = EnumStatusMatPromotion.RetrieveStatus()
        DdlStatusUbah.DataTextField = "NameStatus"
        DdlStatusUbah.DataValueField = "ValStatus"
        DdlStatusUbah.SelectedIndex = 0
        DdlStatusUbah.DataBind()
    End Sub

    Private Sub CreateCriteria()
        objDealer = Session("DEALER")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(MaterialPromotionGIGR), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "NoGI", MatchType.No, ""))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "CreatedTime", MatchType.GreaterOrEqual, icTglGIStart.Value))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "CreatedTime", MatchType.Lesser, icTglGIEnd.Value.AddDays(1)))


        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (txtDealer.Text.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealer.Text, ";", "','") + "')"))
            End If
        Else
            If (txtDealer.Text.Trim <> String.Empty) Then
                If New DataOwner().IsdealerExistInGroup(txtDealer.Text.Trim, objDealer) Then
                    criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealer.Text, ";", "','") + "')"))
                Else
                    mode = 0
                End If
            Else
                Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
                criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Dealer.DealerCode", MatchType.InSet, strCrit))
            End If
        End If

        'If txtDealer.Text <> "" Then
        '    criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealer.Text.Replace(";", "','") & "')"))
        'End If

        If txtKodeBarang.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "MaterialPromotion.GoodNo", MatchType.InSet, "('" & txtKodeBarang.Text.Replace(";", "','") & "')"))
        End If

        If txtNoGI.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "NoGI", MatchType.InSet, "('" & txtNoGI.Text.Replace(";", "','") & "')"))
        End If

        If txtRequestNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "RequestNo", MatchType.InSet, "('" & txtRequestNo.Text.Replace(";", "','") & "')"))
        End If

        sessHelper.SetSession("crits", criterias)

    End Sub

    Private Sub BindDataGrid(ByVal idxpage As Integer)


        Dim totalRow As Integer = 0

        'Dim arrList As New ArrayList


        'arrList = New SalesmanHeaderFacade(User).RetrieveByCriteria(criterias, idxpage + 1, dgSalesmanHeader.PageSize, totalRow, _
        'CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If mode <> 0 Then
            dgAlokasi.CurrentPageIndex = idxpage
            dgAlokasi.DataSource = New MaterialPromotionGIGRFacade(User).RetrieveActiveList(sessHelper.GetSession("crits"), idxpage + 1, dgAlokasi.PageSize, totalRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
            dgAlokasi.VirtualItemCount = totalRow
            dgAlokasi.DataBind()
        Else
            dgAlokasi.DataSource = Nothing
            dgAlokasi.DataBind()
            MessageBox.Show("Kode dealer tidak valid.")
        End If

    End Sub
#End Region

#Region "Event handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            lblDealers.Attributes("onclick") = "ShowPPDealerSelection()"
            lblKodeBarang.Attributes("onclick") = "ShowMaterialPromotionSelection()"
            lblNoGI.Attributes("onclick") = "ShowPPGINo()"
            lblRequestNo.Attributes("onclick") = "ShowPPReqNo()"
            bindddl()
        End If
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        CreateCriteria()
        BindDataGrid(0)
    End Sub
    Private Sub dgAlokasi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgAlokasi.PageIndexChanged
        dgAlokasi.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgAlokasi.CurrentPageIndex)
    End Sub
    Private Sub dgAlokasi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgAlokasi.SortCommand
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
        dgAlokasi.SelectedIndex = -1
        dgAlokasi.CurrentPageIndex = 0
        BindDataGrid(dgAlokasi.CurrentPageIndex)

    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        'Checkbox checker
        Dim counter As Integer = 0
        For Each item As DataGridItem In dgAlokasi.Items
            Dim chkPO As CheckBox = item.FindControl("chkPO")
            If chkPO.Checked Then
                counter += 1
                Exit For
            End If
        Next

        If counter = 0 Then
            MessageBox.Show("Tidak Ada Data Yang Dipilih")
            Exit Sub
        End If

        Dim objFacade As MaterialPromotionGIGRFacade = New MaterialPromotionGIGRFacade(User)
        For Each item As DataGridItem In dgAlokasi.Items
            Dim chkPO As CheckBox = item.FindControl("chkPO")
            If chkPO.Checked Then
                Dim IDGI As LinkButton = item.FindControl("IDGI")
                Dim objToUpdate As MaterialPromotionGIGR = objFacade.Retrieve(CInt(IDGI.CommandArgument))

                If objToUpdate.Status = EnumStatusMatPromotion.StatusMatPromotion.Ditolak Or objToUpdate.Status = EnumStatusMatPromotion.StatusMatPromotion.Batal Then
                    If Not (DdlStatusUbah.SelectedValue = EnumStatusMatPromotion.StatusMatPromotion.Ditolak Or DdlStatusUbah.SelectedValue = EnumStatusMatPromotion.StatusMatPromotion.Batal) Then
                        MessageBox.Show("GI / GR yang sudah dibatalkan atau ditolak tidak dapat diaktifkan kembali ( Record No " & (item.ItemIndex + 1).ToString & ")")
                    Else
                        objToUpdate.Status = CInt(DdlStatusUbah.SelectedValue)
                        Dim result As Integer = objFacade.Update(objToUpdate)
                    End If
                Else
                    objToUpdate.Status = CInt(DdlStatusUbah.SelectedValue)
                    Dim result As Integer = objFacade.Update(objToUpdate)
                End If

            End If
        Next

    End Sub
    Private Sub dgAlokasi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAlokasi.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim objGIGR As MaterialPromotionGIGR = e.Item.DataItem
            Dim imgStatus As System.Web.UI.WebControls.Image = e.Item.FindControl("imgStatus")

            If objGIGR.NoGR = "" Then
                imgStatus.ImageUrl = "../images/red.gif"
            Else
                imgStatus.ImageUrl = "../images/green.gif"
            End If

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgAlokasi.CurrentPageIndex * dgAlokasi.PageSize)

        End If

    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.GoodIssueListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Material Promosi - Daftar Good Issue/Good Receive")
        End If
    End Sub
#End Region

End Class
