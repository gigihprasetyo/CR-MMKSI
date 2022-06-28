Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility

Public Class PopUpMPGoodIssueNo
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtGINo As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMPGINo As System.Web.UI.WebControls.DataGrid

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
    Dim shelp As SessionHelper
    Dim objDealer As New Dealer
#End Region

    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If indexPage >= 0 Then
            criterias = New CriteriaComposite(New Criteria(GetType(MaterialPromotionGIGR), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If txtGINo.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "NoGI", MatchType.[Partial], txtGINo.Text.Trim))
            End If
            'Dealer.DealerCode

            objDealer = CType(Session("DEALER"), Dealer)
            'Modified by Diana (confirm to SA)- Bugs 992
            If Not IsNothing(objDealer) Then
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then

                    If Not IsNothing(Request.QueryString("From")) Then
                        If Request.QueryString("From") = "GR" Then
                            criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
                        ElseIf Request.QueryString("From") = "GIGR" Then
                            criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Dealer.DealerGroup", MatchType.Exact, CInt(objDealer.DealerGroup.ID)))
                            criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Dealer.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Dealer.Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
                            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                                criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
                            End If
                        Else
                        End If
                    End If

                End If
            End If


            Dim arlGI As ArrayList = New MaterialPromotionGIGRFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgMPGINo.PageSize, totalRow, viewstate("SortColl"), viewstate("SortDirectionMP"))

            dtgMPGINo.DataSource = arlGI
            dtgMPGINo.VirtualItemCount = totalRow
            If indexPage = 0 Then
                dtgMPGINo.CurrentPageIndex = 0
            End If

            dtgMPGINo.DataBind()
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            viewstate.Add("SortColl", "RequestNo")
            viewState.Add("SortDirectionMP", Sort.SortDirection.ASC)
            BindToGrid(0)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgMPGINo.CurrentPageIndex = 0
        BindToGrid(0)
    End Sub

    Private Sub dtgMPGINo_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMPGINo.PageIndexChanged
        dtgMPGINo.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgMPGINo.CurrentPageIndex)
    End Sub

    Private Sub dtgMPGINo_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMPGINo.SortCommand
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
