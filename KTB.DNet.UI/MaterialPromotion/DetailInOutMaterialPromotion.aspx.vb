#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class DetailInOutMaterialPromotion
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodeBarang As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpMPMaster As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlAdjustType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icTglStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents dtgDetail As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Dim criterias As CriteriaComposite
    Dim sHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub MapToDLL()
        Dim adjType() As String = {"Semua", "Adjustment In", "Adjustment Out"}
        For i As Integer = 0 To adjType.Length - 1
            ddlAdjustType.Items.Add(adjType(i))
        Next
        ddlAdjustType.SelectedIndex = 0
    End Sub

    Private Function GetDDLValue() As Integer
        Select Case ddlAdjustType.SelectedValue
            Case "Semua"
                Return 0
            Case "Adjustment In"
                Return 1
            Case "Adjustment Out"
                Return 2
        End Select
    End Function

    Private Sub CreateCriteria()
        Dim kdDealer As String = txtKodeDealer.Text.Replace(";", "','")
        Dim kdBarang As String = txtKodeBarang.Text.Replace(";", "','")
        Dim adjTypeID As Integer = GetDDLValue()
        Dim dateStart As DateTime = icTglStart.Value
        Dim dateEnd As DateTime = icTglEnd.Value.AddDays(1)

        criterias = New CriteriaComposite(New Criteria(GetType(MaterialPromotionStockAdjustment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionStockAdjustment), "Dealer.DealerCode", MatchType.InSet, "('" & kdDealer & "')"))
        End If
        If txtKodeBarang.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionStockAdjustment), "MaterialPromotion.GoodNo", MatchType.InSet, "('" & kdBarang & "')"))
        End If
        If adjTypeID <> 0 Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionStockAdjustment), "AdjustType", MatchType.Exact, adjTypeID))
        End If
        criterias.opAnd(New Criteria(GetType(MaterialPromotionStockAdjustment), "CreatedTime", MatchType.GreaterOrEqual, dateStart))
        criterias.opAnd(New Criteria(GetType(MaterialPromotionStockAdjustment), "CreatedTime", MatchType.LesserOrEqual, dateEnd))
        sHelper.SetSession("CRITERIAS", criterias)
    End Sub

    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If indexPage >= 0 Then
            Dim arlMPStockAdj As ArrayList = New MaterialPromotionStockAdjustmentFacade(User).RetrieveActiveList(indexPage + 1, dtgDetail.PageSize, totalRow, viewstate("SortCol"), viewstate("SortDirection"), CType(sHelper.GetSession("CRITERIAS"), CriteriaComposite))
            dtgDetail.DataSource = arlMPStockAdj
            dtgDetail.VirtualItemCount = totalRow
            If indexPage = 0 Then
                dtgDetail.CurrentPageIndex = 0
            End If
            dtgDetail.DataBind()
        End If
    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            viewstate.Add("SortCol", "CreatedTime")
            viewstate.Add("SortDirection", Sort.SortDirection.ASC)

            lblDealer.Attributes.Add("onclick", "ShowPPDealerSelection()")
            lblPopUpMPMaster.Attributes.Add("onclick", "ShowPPMatrialPromotion()")
            MapToDLL()
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        CreateCriteria()
        BindToGrid(0)
    End Sub

    Private Sub dtgDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDetail.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dtgDetail.CurrentPageIndex * dtgDetail.PageSize)

            Dim lblSisaStock As Label = CType(e.Item.FindControl("lblSisaStock"), Label)
            Dim lblStockAwal As Label = CType(e.Item.FindControl("lblStockAwal"), Label)
            Dim lblAdjQty As Label = CType(e.Item.FindControl("lblAdjQty"), Label)
            Dim lblAdjType As Label = CType(e.Item.FindControl("lblAdjType"), Label)
            Dim _intSisaStock As Integer = 0
            If lblAdjType.Text = 1 Then
                lblAdjType.Text = "In"
                _intSisaStock = CInt(lblStockAwal.Text) + CInt(lblAdjQty.Text)
                lblSisaStock.Text = _intSisaStock.ToString("#,##0")
            ElseIf lblAdjType.Text = 2 Then
                lblAdjType.Text = "Out"
                _intSisaStock = CInt(lblStockAwal.Text) - CInt(lblAdjQty.Text)
                lblSisaStock.Text = _intSisaStock.ToString("#,##0")
            End If

        End If
    End Sub

    Private Sub dtgDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDetail.PageIndexChanged
        dtgDetail.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgDetail.CurrentPageIndex)
    End Sub

    Private Sub dtgDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDetail.SortCommand
        If e.SortExpression = viewstate("SortCol") Then
            If viewstate("SortDirection") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirection", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortCol", e.SortExpression)
        BindToGrid(0)
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionViewInOutDetailList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Material Promosi - Detail In-Out Material Promosi")
        End If
    End Sub

#End Region

End Class
