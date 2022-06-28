#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.ShowroomAudit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"

#End Region

Public Class FrmListofAuditSchedule
    Inherits System.Web.UI.Page

#Region "Custom Variable Declaration"
    Dim _sHelper As New SessionHelper
#End Region


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlPeriode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgAuditScheduleDealer As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            lblSearchDealer.Attributes.Add("onClick", "ShowPPDealerSelection();")

            BindDropdownPeriode()


        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        Try
            dtgAuditScheduleDealer.CurrentPageIndex = 0
            bindDatatoGrid(dtgAuditScheduleDealer.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dtgAuditScheduleDealer_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAuditScheduleDealer.PageIndexChanged
        dtgAuditScheduleDealer.CurrentPageIndex = e.NewPageIndex
        bindDatatoGrid(dtgAuditScheduleDealer.CurrentPageIndex)
    End Sub

    Private Sub dtgAuditScheduleDealer_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgAuditScheduleDealer.SortCommand
        If e.SortExpression = ViewState("CurrentSortColumn") Then
            If ViewState("CurrentSortDirect") = Sort.SortDirection.ASC Then
                ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            Else
                ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End If
        End If
        ViewState("CurrentSortColumn") = e.SortExpression
        bindDatatoGrid(dtgAuditScheduleDealer.CurrentPageIndex)
    End Sub

#End Region


#Region " Custom method"
    Private Sub BindDropdownPeriode()

        Dim i As Integer
        Dim yearMax As Integer = Year(Date.Now) + 1
        For i = Year(Date.Now) To yearMax
            ddlPeriode.Items.Add(i.ToString)
        Next
        ddlPeriode.SelectedIndex = -1

    End Sub

    Private Sub bindDatatoGrid(ByVal idx As Integer)
        If idx >= 0 Then
            Dim arlAllData As ArrayList = FindData(idx)
            dtgAuditScheduleDealer.DataSource = arlAllData
            dtgAuditScheduleDealer.VirtualItemCount = arlAllData.Count
            dtgAuditScheduleDealer.DataBind()
            If arlAllData.Count <= 0 Then
                MessageBox.Show(SR.DataNotFound("Audit Schedule Dealer"))
            End If
        End If
    End Sub

    Private Function FindData(ByVal indexPage As Integer) As ArrayList
        Dim criteriaForAuditSchedule As CriteriaComposite
        criteriaForAuditSchedule = New CriteriaComposite(New Criteria(GetType(AuditSchedule), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaForAuditSchedule.opAnd(New Criteria(GetType(AuditSchedule), "AuditParameter.Period", MatchType.Exact, ddlPeriode.SelectedValue))

        'If txtDealerCode.Text.Trim.Length > 0 Then
        '    criteriaForAuditSchedule.opAnd(New Criteria(GetType(AuditSchedule), "AuditScheduleDealer.Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Trim.Replace(";", "','") & "')"))
        'End If

        Dim arlAuditSchedule As ArrayList = New AuditScheduleDealerFacade(User).Retrieve(criteriaForAuditSchedule, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
        Return arlAuditSchedule
    End Function

#End Region


    Private Sub dtgAuditScheduleDealer_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgAuditScheduleDealer.ItemCommand
        Select Case e.CommandName
            Case "detail"
                Response.Redirect("../ShowroomAudit/FrmAuditSchedule.aspx?id=" & e.CommandArgument & "&Mode=View", True)
            Case "edit"
                Response.Redirect("../ShowroomAudit/FrmAuditSchedule.aspx?id=" & e.CommandArgument & "&Mode=Update", True)
        End Select
    End Sub
End Class
