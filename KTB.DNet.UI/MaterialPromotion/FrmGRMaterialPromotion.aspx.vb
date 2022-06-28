Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports KTB.DNet.BusinessFacade.General

Public Class FrmGRMaterialPromotion
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealers As System.Web.UI.WebControls.Label
    Protected WithEvents txtRequestNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeBarang As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKodeBarang As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dgAlokasi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoGI As System.Web.UI.WebControls.TextBox
    Protected WithEvents icTglGIStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglGIEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblRequestNoGI As System.Web.UI.WebControls.Label
    Protected WithEvents lblRequestNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoGI As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label

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
        'Put user code to initialize the page here
        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Dim ssHelper As SessionHelper = New SessionHelper
            Dim objDealer As Dealer = ssHelper.GetSession("DEALER")
            lblDealer.Text = objDealer.DealerCode
            lblDealers.Attributes("onclick") = "ShowPPDealerSelection()"
            lblRequestNo.Attributes("onclick") = "ShowPPReqNo()"
            lblKodeBarang.Attributes("onclick") = "ShowMaterialPromotionSelection()"
            lblNoGI.Attributes("onclick") = "ShowPPGINo()"
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        'TODO HANDLE CANCELLED STATUS
        BindDataGrid(0)
    End Sub

    Private Sub BindDataGrid(ByVal idxpage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(MaterialPromotionGIGR), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Dim ssHelper As SessionHelper = New SessionHelper
        'Dim objDealer As Dealer = ssHelper.GetSession("DEALER")
        'criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "NoGR", MatchType.Exact, ""))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "CreatedTime", MatchType.GreaterOrEqual, icTglGIStart.Value))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "CreatedTime", MatchType.Lesser, icTglGIEnd.Value.AddDays(1)))

        'If txtDealer.Text <> "" Then
        '    criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealer.Text.Replace(";", "','") & "')"))
        'End If
        'lblDealer.Text = objDealer.DealerCode
        criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Dealer.DealerCode", MatchType.Exact, lblDealer.Text))

        If txtKodeBarang.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "MaterialPromotion.GoodNo", MatchType.InSet, "('" & txtKodeBarang.Text.Replace(";", "','") & "')"))
        End If

        If txtNoGI.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "NoGI", MatchType.InSet, "('" & txtNoGI.Text.Replace(";", "','") & "')"))
        End If

        If txtRequestNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "RequestNo", MatchType.InSet, "('" & txtRequestNo.Text.Replace(";", "','") & "')"))
        End If

        'arrList = New SalesmanHeaderFacade(User).RetrieveByCriteria(criterias, idxpage + 1, dgSalesmanHeader.PageSize, totalRow, _
        'CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgAlokasi.CurrentPageIndex = idxpage
        dgAlokasi.DataSource = New MaterialPromotionGIGRFacade(User).RetrieveActiveList(criterias, idxpage + 1, dgAlokasi.PageSize, totalRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
        dgAlokasi.VirtualItemCount = totalRow
        dgAlokasi.DataBind()

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

        Dim objGIGRFacade As MaterialPromotionGIGRFacade = New MaterialPromotionGIGRFacade(User)

        ' Populate & Check Data
        Dim Counter As Integer = 0
        For Each item As DataGridItem In dgAlokasi.Items
            Dim chkPO As CheckBox = item.FindControl("chkPO")
            If chkPO.Checked Then
                Counter += 1
                Exit For
            End If
        Next

        If Counter = 0 Then
            MessageBox.Show("Tidak ada data yang dipilih")
            Exit Sub
        End If

        'Update Data
        For Each item As DataGridItem In dgAlokasi.Items
            Dim chkPO As CheckBox = item.FindControl("chkPO")
            If chkPO.Checked Then
                Dim IDGI As LinkButton = item.FindControl("IDGI")
                Dim objGIGR As MaterialPromotionGIGR = objGIGRFacade.Retrieve(CInt(IDGI.CommandArgument))
                objGIGR.NoGR = "generated"
                Dim result As Integer = objGIGRFacade.Update(objGIGR)
                Dim lblKeterangan As Label = item.FindControl("lblKeterangan")
                lblKeterangan.Text = objGIGRFacade.Retrieve(CInt(IDGI.CommandArgument)).NoGR
                chkPO.Checked = False
                chkPO.Visible = False
            End If
        Next





    End Sub

    Private Sub dgAlokasi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAlokasi.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim objGIGR As MaterialPromotionGIGR = e.Item.DataItem
            Dim chkPO As CheckBox = e.Item.FindControl("chkPO")

            chkPO.Visible = (objGIGR.NoGR = "")

            Dim lbldealercode As Label = CType(e.Item.FindControl("lblDealer"), Label)
            lbldealercode.ToolTip = objGIGR.Dealer.DealerName
            'e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgAlokasi.CurrentPageIndex * dgAlokasi.PageSize)

        End If

    End Sub


End Class
