#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports System.Collections.Generic
Imports System.Linq

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.UI.Helper
#End Region

Public Class FrmFreePassUsed
    Inherits System.Web.UI.Page

    Dim gridColNo As Integer = 0
    Private p_dealerCode As String = String.Empty
    Private p_fiscalYear As String = String.Empty
    Private p_dealer As Dealer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        p_dealerCode = Page.Request.QueryString("dealerCode")
        p_fiscalYear = Page.Request.QueryString("fiscalYear")
        p_dealer = New DealerFacade(User).Retrieve(p_dealerCode)
        If Not Page.IsPostBack Then
            ViewState.Add("CurrentSortColumn", "TrTrainee.ID")
            ViewState.Add("CurrentSortDirect", Sort.SortDirection.ASC)
            InitForm()

        End If
    End Sub

    Private Sub InitForm()
       
        Dim trFreePassData As TrFreePass = GetFreePass()

        lblDealerCode.Text = p_dealer.DealerCode & " - " & p_dealer.DealerName
        lblFiscalYear.Text = p_fiscalYear
        lblQtyUsed.Text = trFreePassData.QtyUsed
        BindDataGrid(0)
    End Sub

    Private Function GetFreePass() As TrFreePass
        Dim result As New TrFreePass
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrFreePass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrFreePass), "Dealer.ID", MatchType.Exact, p_dealer.ID))
        criterias.opAnd(New Criteria(GetType(TrFreePass), "FiscalYear", MatchType.Exact, p_fiscalYear))

        result = New TrFreePassFacade(User).Retrieve(criterias)(0)

        Return result
    End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        gridColNo = 0
        dtgFreePass.DataSource = New TrBookingCourseFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, dtgFreePass.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgFreePass.VirtualItemCount = totalRow
        dtgFreePass.DataBind()
    End Sub

    Private Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, p_fiscalYear))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.DealerCode", MatchType.Exact, p_dealerCode))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.ID", MatchType.IsNotNull, Nothing))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "ID", MatchType.InSet, CreateInsetID()))

        Return criterias
    End Function

    Private Function CreateInsetID() As String
        Dim result As String = String.Empty
        result = "( SELECT TrBookingCourseID FROM TrBillingDetail detail" & _
                    " INNER JOIN TrBillingHeader header ON detail.TrBillingHeaderID = header.id AND detail.RowStatus = 0" & _
                    " WHERE detail.IsVoucherUsed = 1 AND header.FiscalYear = '" & p_fiscalYear & "' AND header.DealerID = " & p_dealer.ID & ")"


        Return result
    End Function

    Private Sub dtgFreePass_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgFreePass.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then

            Dim RowValue As TrBookingCourse = CType(e.Item.DataItem, TrBookingCourse)
            gridColNo += 1

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            Dim lblTraineeID As Label = CType(e.Item.FindControl("lblTraineeID"), Label)
            Dim lblTraineeName As Label = CType(e.Item.FindControl("lblTraineeName"), Label)
            Dim lblClass As Label = CType(e.Item.FindControl("lblClass"), Label)
            Dim lblQtyUsed As Label = CType(e.Item.FindControl("lblQtyUsed"), Label)
            Dim lblTrainingDate As Label = CType(e.Item.FindControl("lblTrainingDate"), Label)
            Dim lblAddress As Label = CType(e.Item.FindControl("lblAddress"), Label)

            lblNo.Text = gridColNo
            lblTraineeID.Text = RowValue.TrTrainee.ID
            lblTraineeName.Text = RowValue.TrTrainee.Name
            lblClass.Text = RowValue.TrClassRegistration.TrClass.ClassCode
            lblQtyUsed.Text = RowValue.TrClassRegistration.TrClass.PaidDay
            lblTrainingDate.Text = RowValue.TrClassRegistration.TrClass.StartDate.ToString("dd/MM/yy") & " s/d " & RowValue.TrClassRegistration.TrClass.FinishDate.ToString("dd/MM/yy")
            lblAddress.Text = RowValue.TrClassRegistration.TrClass.TrMRTC.Code

        End If
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmTrFreePass.aspx")
    End Sub

    Private Sub dtgFreePass_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgFreePass.PageIndexChanged
        dtgFreePass.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgFreePass.CurrentPageIndex)
    End Sub

    Private Sub dtgFreePass_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgFreePass.SortCommand
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

        dtgFreePass.CurrentPageIndex = 0
        BindDataGrid(dtgFreePass.CurrentPageIndex)
    End Sub

End Class