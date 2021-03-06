Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class PopUpRequestMaterialPromotion
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtRequestNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgRequestNumber As System.Web.UI.WebControls.DataGrid
    Protected WithEvents chkTanggalPermintaan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icTanggalPermintaan As KTB.DNet.WebCC.IntiCalendar

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
    Dim objMaterialPromotionRequest As MaterialPromotionRequest
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            sHelper.SetSession("SortColPopUp", "ID")
            sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
            BindSearch(0)
            ClearData()
        End If
    End Sub

    Public Sub BindSearch(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            CreateCriteria()
            dtgRequestNumber.DataSource = New MaterialPromotionRequestFacade(User).RetrieveActiveList(indexPage + 1, dtgRequestNumber.PageSize, totalRow, sHelper.GetSession("SortColPopUp"), sHelper.GetSession("SortDirectionPopUp"), criterias)
            dtgRequestNumber.VirtualItemCount = totalRow
            dtgRequestNumber.DataBind()
        End If

        'If dtgRequestNumber.Items.Count > 0 Then
        '    btnChoose.Disabled = False
        'Else
        '    btnChoose.Disabled = True
        'End If
    End Sub

    Public Sub CreateCriteria()
        Dim objDealer As Dealer
        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotionRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim objUser As UserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Status", MatchType.GreaterOrEqual, CInt(EnumMaterialPromotion.MaterialPromotionStatus.Validasi)))
        End If


        If txtRequestNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "RequestNo", MatchType.[Partial], txtRequestNo.Text))
        End If
        If chkTanggalPermintaan.Checked Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "RequestDate", MatchType.Exact, icTanggalPermintaan.Value))
        End If

        If Not IsNothing(sHelper.GetSession("DEALER")) Then
            objDealer = CType(Session("DEALER"), Dealer)
        End If

        criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "RequestNo", MatchType.StartsWith, objDealer.DealerCode))

    End Sub

    Private Sub ClearData()
        txtRequestNo.Text = String.Empty
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgRequestNumber.CurrentPageIndex = 0
        BindSearch(0)
    End Sub

    Private Sub dtgRequestNumber_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgRequestNumber.PageIndexChanged
        dtgRequestNumber.CurrentPageIndex = e.NewPageIndex
        BindSearch(dtgRequestNumber.CurrentPageIndex)
    End Sub

    Private Sub dtgRequestNumber_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgRequestNumber.SortCommand
        If e.SortExpression = sHelper.GetSession("SortColPopUp") Then
            If sHelper.GetSession("SortDirectionPopUp") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortColPopUp", e.SortExpression)
        dtgRequestNumber.SelectedIndex = -1
        dtgRequestNumber.CurrentPageIndex = 0
        BindSearch(0)
    End Sub

    Private Sub dtgRequestNumber_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgRequestNumber.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim lblRequestNo As Label = CType(e.Item.FindControl("lblRequestNo"), Label)

            objMaterialPromotionRequest = e.Item.DataItem
            lblRequestNo.Text = objMaterialPromotionRequest.RequestNo

            Dim lblRequestDate As Label = CType(e.Item.FindControl("lblRequestDate"), Label)
            lblRequestDate.Text = objMaterialPromotionRequest.RequestDate.ToString("dd/MM/yyyy")
        End If
    End Sub
End Class
