Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Benefit

Public Class PopUpType
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgCompetitorType As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private sessHelper As New SessionHelper

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack() Then
            ViewState("currentSortColumn") = "Code"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            BindResult(0)
        End If
    End Sub
    Private Sub BindResult(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim dimModel As Short = CShort(Request.QueryString("model"))
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))

        If Not IsNothing(dimModel) Then
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & dimModel
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

            'Dim strSql As String = ""
            'strSql += " select ID from VechileType "
            'strSql += " where ModelID in ('" & dimModel & "') "
            ''criterias.opAnd(New Criteria(GetType(VechileType), "ModelID", MatchType.Exact, dimModel))
            'criterias.opAnd(New Criteria(GetType(VechileType), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If


        'criterias.opAnd(New Criteria(GetType(CompetitorBrand), "Code", MatchType.No, "MITSUBISHI"), "(", True)
        'criterias.opOr(New Criteria(GetType(CompetitorBrand), "Code", MatchType.No, "mitsubishi"), ")", False)

        'Dim arrList As ArrayList = New VechileTypeFacade(User).Retrieve(criterias)
        Dim arrList As ArrayList = New VechileTypeFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgCompetitorType.PageSize, totalRow)
        dgCompetitorType.DataSource = arrList
        dgCompetitorType.VirtualItemCount = totalRow
        dgCompetitorType.DataBind()
        If dgCompetitorType.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dgCompetitorType_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgCompetitorType.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As VechileType = CType(e.Item.DataItem, VechileType)

            Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)

            Dim detail As BenefitMasterDetail = CType(sessHelper.GetSession("DetailIDSession"), BenefitMasterDetail)
            If Not IsNothing(detail) Then
                Dim objBenefitVehicleTypeFacade As BenefitMasterVehicleTypeFacade = New BenefitMasterVehicleTypeFacade(User)
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterVehicleType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BenefitMasterVehicleType), "BenefitMasterDetail.ID", MatchType.Exact, CInt(detail.ID)))


                For Each item As BenefitMasterVehicleType In objBenefitVehicleTypeFacade.Retrieve(criterias)
                    If RowValue.ID = item.VechileType.ID Then
                        chkItemChecked.Checked = True
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub dgCompetitorType_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgCompetitorType.PageIndexChanged
        dgCompetitorType.CurrentPageIndex = e.NewPageIndex
        BindResult(dgCompetitorType.CurrentPageIndex)
    End Sub

    Private Sub dgCompetitorType_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgCompetitorType.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgCompetitorType.SelectedIndex = -1
        dgCompetitorType.CurrentPageIndex = 0
        BindResult(dgCompetitorType.CurrentPageIndex)
    End Sub

End Class
