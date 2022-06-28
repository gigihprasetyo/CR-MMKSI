Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class PopUpMaterialPromotion
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dtgMaterialPromotionSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtGoodNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox

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
    Dim objMaterialPromotion As MaterialPromotion

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            sHelper.SetSession("SortColPopUp", "Name")
            sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
            BindSearch(0)
            ClearData()
        End If
    End Sub

    Private Sub ClearData()
        txtGoodNo.Text = String.Empty
        txtName.Text = String.Empty
    End Sub

    Public Sub BindSearch(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            CreateCriteria()
            dtgMaterialPromotionSelection.DataSource = New MaterialPromotionFacade(User).RetrieveActiveList(indexPage + 1, dtgMaterialPromotionSelection.PageSize, totalRow, sHelper.GetSession("SortColPopUp"), sHelper.GetSession("SortDirectionPopUp"), criterias)
            dtgMaterialPromotionSelection.VirtualItemCount = totalRow
            dtgMaterialPromotionSelection.DataBind()
        End If

        If dtgMaterialPromotionSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub
    Public Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MaterialPromotion), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'Note : using coma(",") to separate
        If txtName.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(MaterialPromotion), "Name", MatchType.[Partial], txtName.Text))
        End If

        If txtGoodNo.Text <> "" Then
            Dim str As String = "('"
            If txtGoodNo.Text <> "" Then
                For Each item As String In txtGoodNo.Text.Split(",")
                    str += item & "','"
                Next
                str = str.Trim(",")

            End If
            str = str.Substring(0, str.Length - 2)
            str += ")"

            criterias.opAnd(New Criteria(GetType(MaterialPromotion), "GoodNo", MatchType.InSet, str))
            Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
            If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(MaterialPromotion), "Status", MatchType.Exact, CShort(EnumMaterialPromotion.MasterMaterialPromotionStatus.Aktif)))
            End If

        End If

    End Sub


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If (sHelper.GetSession("Name") = "") Then
            sHelper.SetSession("SortColPopUp", "Name")
            sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
        End If
        BindSearch(0)
    End Sub

    Private Sub dtgMaterialPromotionSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMaterialPromotionSelection.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim lblGoodNo As Label = CType(e.Item.FindControl("lblGoodNo"), Label)
            Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)

            objMaterialPromotion = e.Item.DataItem
            lblGoodNo.Text = objMaterialPromotion.GoodNo
            lblName.Text = objMaterialPromotion.Name
        End If
    End Sub

    Private Sub dtgMaterialPromotionSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMaterialPromotionSelection.SortCommand
        If e.SortExpression = sHelper.GetSession("SortColPopUp") Then
            If sHelper.GetSession("SortDirectionPopUp") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirectionPopUp", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortColPopUp", e.SortExpression)
        dtgMaterialPromotionSelection.SelectedIndex = -1
        dtgMaterialPromotionSelection.CurrentPageIndex = 0
        BindSearch(0)
    End Sub

    Private Sub dtgMaterialPromotionSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMaterialPromotionSelection.PageIndexChanged
        dtgMaterialPromotionSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch(dtgMaterialPromotionSelection.CurrentPageIndex)
    End Sub
End Class
