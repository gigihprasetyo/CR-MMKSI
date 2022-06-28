#Region "Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security
#End Region

Public Class FrmDPFleetCustomerList
    Inherits System.Web.UI.Page

    Private _sessHelper As New SessionHelper()
    Private _sessCri As String = "FrmDPFleetCustomerList._sessCriteria"

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.DP_DaftarFleetCustomer_Lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=DISCOUNT PROPOSAL - Daftar Fleet Customer")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("IsBack")) Then
                ViewState("IsBack") = Request.QueryString("IsBack")
            End If
            ViewState.Add("SortColFrmDPFleetCustomerList", "ID")
            ViewState.Add("SortDirFrmDPFleetCustomerList", Sort.SortDirection.DESC)
            BindDDLTipe()
            BindGrid(0)
        End If
    End Sub

    Private Sub BindDDLTipe()
        ddlTipe.Items.Clear()
        ddlTipe.DataSource = New EnumTipePelangganCustomerRequest().RetrieveTypeDiscountProposal()
        ddlTipe.DataTextField = "NameTipe"
        ddlTipe.DataValueField = "ValTipe"
        ddlTipe.DataBind()
        ddlTipe.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlTipe.SelectedIndex = 0
    End Sub

    Private Sub BindGrid(ByVal index As Integer)
        Dim crit As CriteriaComposite = getCriteria()
        If Not IsNothing(ViewState("IsBack")) Then
            crit = CType(Me._sessHelper.GetSession(_sessCri), CriteriaComposite)
            ViewState("IsBack") = Nothing
        End If
        Dim totalrow As Integer = 0
        Dim arlFleetCustomerHeader As ArrayList = New FleetCustomerHeaderFacade(User).RetrieveActiveList(crit, index + 1,
                               dtgMain.PageSize,
                               totalrow,
                               CType(ViewState("SortColFrmDPFleetCustomerList"), String),
                               CType(ViewState("SortDirFrmDPFleetCustomerList"), Sort.SortDirection))

        dtgMain.DataSource = arlFleetCustomerHeader
        dtgMain.VirtualItemCount = totalrow
        dtgMain.DataBind()
        Me._sessHelper.SetSession(_sessCri, crit)
    End Sub

    Private Function getCriteria() As CriteriaComposite
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlTipe.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(FleetCustomerHeader), "FleetCustomerType", MatchType.Exact, ddlTipe.SelectedValue))
        End If

        If txtKode.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(FleetCustomerHeader), "FleetCode", MatchType.[Partial], txtKode.Text))
        End If

        If txtNama.Text.Trim.Length > 0 Then
            crit.opAnd(New Criteria(GetType(FleetCustomerHeader), "FleetCustomerName", MatchType.[Partial], txtNama.Text))
        End If
        Return crit
    End Function

    Protected Sub dtgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oFCH As FleetCustomerHeader = CType(e.Item.DataItem, FleetCustomerHeader)

            Dim lblGridKategori As Label = CType(e.Item.FindControl("lblGridKategori"), Label)
            Dim arlCat As ArrayList = New StandardCodeFacade(User).RetrieveByValueId(oFCH.FleetCompanyCategory, "EnumFleetMaster.CategoryFleet")
            If arlCat.Count > 0 Then
                lblGridKategori.Text = CType(arlCat(0), StandardCode).ValueCode
            Else
                lblGridKategori.Text = ""
            End If
            Dim lblGridTipe As Label = CType(e.Item.FindControl("lblGridTipe"), Label)
            Dim stdCode As ArrayList = New StandardCodeFacade(User).RetrieveByValueId(oFCH.FleetCustomerType, "EnumDiscountProposal.CustomerType")
            If stdCode.Count > 0 Then
                lblGridTipe.Text = CType(stdCode(0), StandardCode).ValueDesc
            End If
        End If
    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)
        Select Case e.CommandName
            Case "Detail"
                Response.Redirect("FrmDPFleetCustomerDetail.aspx?Mode=VIEW&FleetHeaderID=" & hdnID.Value)
            Case "Edit"
                Response.Redirect("FrmDPFleetCustomerDetail.aspx?Mode=EDIT&FleetHeaderID=" & hdnID.Value)
            Case "Mapping"
                Response.Redirect("FrmDPFleetCustomerMapping.aspx?FleetHeaderID=" & hdnID.Value)
            Case "Delete"
                DeleteData(hdnID)
        End Select
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        BindGrid(0)
    End Sub

    Private Sub DeleteData(ByVal hdnID As HiddenField)
        Dim objFCH As FleetCustomerHeader = New FleetCustomerHeaderFacade(User).Retrieve(CInt(hdnID.Value))
        If Not isUsedData(objFCH) Then
            objFCH.RowStatus = CType(DBRowStatus.Deleted, Short)
            Dim arlDetail As ArrayList = New FleetCustomerDetailFacade(User).Retrieve(objFCH)
            Dim arlMapping As ArrayList = New FleetCustomerDetailMappingFacade(User).Retrieve(objFCH)
            Dim del As Integer = New FleetCustomerHeaderFacade(User).UpdateTransactionDelete(objFCH, arlDetail, arlMapping)
            If del > 0 Then
                MessageBox.Show("Data Fleet berhasil dihapus")
                BindGrid(dtgMain.CurrentPageIndex)
            End If
        Else
            MessageBox.Show("Fleet Customer Data sudah digunakan pada pengajuan Discount Proposal")
        End If
    End Sub

    Private Function isUsedData(ByVal objFCH As FleetCustomerHeader) As Boolean
        Dim arlDataDP As ArrayList = New DiscountProposalHeaderFacade(User).RetrieveByFleetCustomerHeader(objFCH)
        If arlDataDP.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    Protected Sub dtgMain_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMain.PageIndexChanged
        dtgMain.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub
End Class