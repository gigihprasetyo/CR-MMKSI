#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility

#End Region

Public Class PopUpInputMSPExtendedSelection
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            dtgMSPRegistration.DataSource = New ArrayList
            dtgMSPRegistration.DataBind()
        End If
    End Sub

    Protected Sub dtgMSPRegistration_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPRegistration.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim RowValue As MSPExRegistration = CType(e.Item.DataItem, MSPExRegistration)
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblNoMSP As Label = CType(e.Item.FindControl("lblNoMSP"), Label)
            Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
            Dim lblVehicleName As Label = CType(e.Item.FindControl("lblVehicleName"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dtgMSPRegistration.CurrentPageIndex * dtgMSPRegistration.PageSize)
            lblNoMSP.Text = RowValue.RegNumber
            lblChassisNumber.Text = RowValue.ChassisMaster.ChassisNumber
            lblVehicleName.Text = RowValue.ChassisMaster.VechileColor.VechileType.Description
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGrid(0)
    End Sub

    Private Sub BindGrid(ByVal index As Integer)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CriteriaSearch(crit)
        Dim totalRow As Integer = 0
        Dim arlData As ArrayList = New MSPExRegistrationFacade(User).RetrieveByCriteria(crit, index, dtgMSPRegistration.PageSize, totalRow)

        dtgMSPRegistration.CurrentPageIndex = index
        dtgMSPRegistration.DataSource = arlData
        dtgMSPRegistration.VirtualItemCount = totalRow
        dtgMSPRegistration.DataBind()
    End Sub

    Private Sub CriteriaSearch(ByRef crit As CriteriaComposite)
        crit.opAnd(New Criteria(GetType(MSPExRegistration), "CreatedTime", MatchType.GreaterOrEqual, DateFrom.Value))
        crit.opAnd(New Criteria(GetType(MSPExRegistration), "CreatedTime", MatchType.LesserOrEqual, DateTo.Value.AddDays(1)))
        If txtChassisNumber.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(MSPExRegistration), "RegNumber", MatchType.Exact, txtChassisNumber.Text))
        End If
    End Sub

    Protected Sub dtgMSPRegistration_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMSPRegistration.PageIndexChanged
        dtgMSPRegistration.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub
End Class