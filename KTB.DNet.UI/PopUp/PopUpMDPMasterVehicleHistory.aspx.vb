#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.MDP
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
#End Region

Public Class PopUpMDPMasterVehicleHistory
    Inherits System.Web.UI.Page

#Region " Private Variables "
    Private sessHelp As SessionHelper = New SessionHelper
    Dim arrMVHistory As ArrayList
#End Region

#Region "Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            If Not IsNothing(Request.QueryString("ID")) Then
                BinddtgStatusChange(CType(Request.QueryString("ID"), Integer))
            End If
            If Not IsNothing(Request.QueryString("MaterialDescription")) Then
                lblNamaKendaraan.Text = Request.QueryString("MaterialDescription")
            End If

        End If      
    End Sub

    Protected Sub dtgStatusChange_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgStatusChange.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim objMDPMVH As MDPMasterVehicleHistory
            Dim lblNoHistory As Label = e.Item.FindControl("lblNoHistory")
            Dim lblTglProses As Label = e.Item.FindControl("lblTglProses")

            lblNoHistory.Text = e.Item.ItemIndex + 1 + (dtgStatusChange.CurrentPageIndex * dtgStatusChange.PageSize)
            objMDPMVH = CType(arrMVHistory(e.Item.ItemIndex), MDPMasterVehicleHistory)

            Dim lblStatusLama As Label = CType(e.Item.FindControl("lblStatusLama"), Label)
            Dim lblStatusBaru As Label = CType(e.Item.FindControl("lblStatusBaru"), Label)

            If objMDPMVH.StatusFrom = 1 Then
                lblStatusLama.Text = "Active"
            Else
                lblStatusLama.Text = "Non Active"
            End If

            If objMDPMVH.StatusTo = 1 Then
                lblStatusBaru.Text = "Active"
            Else
                lblStatusBaru.Text = "Non Active"
            End If

            lblTglProses.Text = objMDPMVH.CreatedTime.ToString("dd/MM/yyyy")
        End If
    End Sub

    Private Sub BinddtgStatusChange(ByVal ID As Integer)
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicleHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(MDPMasterVehicleHistory), "MDPMasterVehicle.ID", MatchType.Exact, ID))

        arrMVHistory = New MDPMasterVehicleHistoryFacade(User).RetrieveActiveList(0 + 1, _
                                        dtgStatusChange.PageSize, total, CType(ViewState("CurrentSortColumn"), String), _
                                        CType(ViewState("CurrentSortDirect"), Sort.SortDirection), criterias)

        dtgStatusChange.DataSource = arrMVHistory
        dtgStatusChange.VirtualItemCount = total

        dtgStatusChange.DataBind()
    End Sub
#End Region

#Region "Custom Method"

#End Region

End Class