Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility

Public Class PopUpMPDetailRequestNo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtReqNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgMReqNo As System.Web.UI.WebControls.DataGrid

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
    Dim sessHelp As SessionHelper = New SessionHelper
    Private objDealer As Dealer
#End Region

#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexpage As Integer)
        Dim totalRow As Integer = 0
        If indexpage >= 0 Then
            criterias = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If txtReqNo.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "RequestNo", MatchType.[Partial], txtReqNo.Text.Trim))
            End If

            objDealer = Session("DEALER")
            'Modified by Diana (confirm to SA)- Bugs 992
            If Not IsNothing(objDealer) Then
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    If Not IsNothing(Request.QueryString("From")) Then
                        If Request.QueryString("From") = "GR" Then
                            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
                        ElseIf Request.QueryString("From") = "GIGR" Then
                            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Dealer.DealerGroup.ID", MatchType.Exact, CInt(objDealer.DealerGroup.ID)))
                            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Dealer.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Dealer.Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
                            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                                criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
                            End If
                        Else
                        End If
                    End If
                End If
            End If

            'Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
            'If objUser.Dealer.Title <> "1" Then
            '    criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "Dealer.DealerCode", MatchType.Exact, objUser.Dealer.DealerCode))
            'End If

            Dim arlReq As ArrayList = New MaterialPromotionRequestFacade(User).RetrieveActiveList(indexpage + 1, dtgMReqNo.PageSize, totalRow, viewstate("SortColl"), viewstate("SortDirectionMP"), criterias)

            dtgMReqNo.DataSource = arlReq
            dtgMReqNo.VirtualItemCount = totalRow
            If indexpage = 0 Then
                dtgMReqNo.CurrentPageIndex = 0
            End If

            dtgMReqNo.DataBind()
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            viewstate.Add("SortColl", "RequestNo")
            viewState.Add("SortDirectionMP", Sort.SortDirection.ASC)
            BindToGrid(0)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgMReqNo.CurrentPageIndex = 0
        BindToGrid(dtgMReqNo.CurrentPageIndex)
    End Sub

    Private Sub dtgMReqNo_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMReqNo.PageIndexChanged
        dtgMReqNo.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgMReqNo.CurrentPageIndex)
    End Sub

    Private Sub dtgMReqNo_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMReqNo.SortCommand
        If e.SortExpression = viewstate("SotColl") Then
            If viewstate("SortDirectionMP") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirectionMP", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirectionMP", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SotColl", e.SortExpression)
        BindToGrid(0)
    End Sub
End Class
