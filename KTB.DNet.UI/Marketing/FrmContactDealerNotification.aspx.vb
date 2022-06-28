Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text


Public Class FrmContactDealerNotification
    Inherits System.Web.UI.Page

#Region "EVENT HANDLER"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        initAuthorization()
        initField()
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If Not IsPostBack Then
            bindDdlPosition()
            bindDataGrid(0)
        End If
    End Sub

    Protected Sub dgContactDealer_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgContactDealer.ItemDataBound
        If e.Item.ItemIndex >= 0 Then '
            Dim obj As DealerContactNotificationCase = CType(e.Item.DataItem, DealerContactNotificationCase)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim lblPhone As Label = CType(e.Item.FindControl("lblPhone"), Label)
            Dim lblPosition As Label = CType(e.Item.FindControl("lblPosition"), Label)
            Dim lblChgLocation As Label = CType(e.Item.FindControl("lblChgLocation"), Label)

            lblDealerCode.Text = obj.DealerCode
            lblDealerName.Text = obj.DealerName
            lblPhone.Text = obj.Phone
            lblPosition.Text = obj.JobPosisi
            lblChgLocation.Text = obj.LokasiUbah

        End If
    End Sub

    Protected Sub dgContactDealer_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgContactDealer.PageIndexChanged
        dgContactDealer.CurrentPageIndex = e.NewPageIndex
        bindDataGrid(dgContactDealer.CurrentPageIndex)
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bindDataGrid(0)
    End Sub

#End Region


#Region "CUSTOM METHOD"

    Private Sub initAuthorization()
        'MessageBox.Show("This page is under construction")
        'Server.Transfer("../FrmAccessDenied.aspx?modulName=Kontak Dealer Notifiaksi Customer Case")
        If Not SecurityProvider.Authorize(Context.User, SR.Customer_Case_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Kontak Dealer Notifiaksi Customer Case")
        End If
    End Sub

    Private Sub initField()
        lblDealerInfo.Visible = False
        Dim dealer As Dealer = CType(Session("DEALER"), Dealer)
        If Not dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtKodeDealer.Text = dealer.DealerCode
            txtKodeDealer.Enabled = False
            lblDealerInfo.Visible = True
            lblDealerInfo.Text = dealer.DealerName
        End If
    End Sub

    Private Sub bindDdlPosition()
        Dim arrJobPosition As ArrayList = New DealerContactNotificationCaseFacade(User).RetrieveJobPosition()
        ddlPosition.Items.Add(New ListItem("Silahkan Pilih", 0))
        For Each lst As ArrayList In arrJobPosition
            ddlPosition.Items.Add(New ListItem(lst(0), lst(1)))
        Next
    End Sub

    Private Sub bindDataGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim crit As CriteriaComposite = buildSearchCriteria()
        Dim arr As ArrayList = New DealerContactNotificationCaseFacade(User).RetrieveByCriteria(crit, currentPageIndex + 1, dgContactDealer.PageSize, total, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        dgContactDealer.VirtualItemCount = total
        dgContactDealer.DataSource = arr
        dgContactDealer.DataBind()
    End Sub

    Private Function buildSearchCriteria()
        Dim crit As New CriteriaComposite(New Criteria(GetType(DealerContactNotificationCase), "ID", MatchType.No, 0))
        If Not txtKodeDealer.Text.Trim = String.Empty Then
            crit.opAnd(New Criteria(GetType(DealerContactNotificationCase), "DealerCode", MatchType.Exact, txtKodeDealer.Text.Trim))
        End If
        If Not ddlPosition.SelectedValue = "0" Then
            crit.opAnd(New Criteria(GetType(DealerContactNotificationCase), "jobpositionid", MatchType.Exact, ddlPosition.SelectedValue))
        End If
        If Not txtPhoneNumber.Text.Trim = String.Empty Then
            crit.opAnd(New Criteria(GetType(DealerContactNotificationCase), "Phone", MatchType.Partial, txtPhoneNumber.Text.Trim))
        End If
        Return crit
    End Function

#End Region


End Class