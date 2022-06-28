Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.ShowroomAudit
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement

Public Class PopUpAuditParameter
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgDealerSelection As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dtgAuditParameterSelection As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ViewState("currSortColumn") = "ID"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        If Not IsPostBack Then
            bindtoGrid(0)
        End If
    End Sub

    Private Sub bindtoGrid(ByVal idx As Integer)
        Dim total As Integer = 0
        Dim arlAudit As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(AuditParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(AuditParameter), "IsRilis", MatchType.Exact, 1))

        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            Dim criteriaAuditScheduleDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "Dealer.ID", MatchType.Exact, objDealer.ID.ToString))
            Dim arlAuditScheduleDealer As ArrayList = New AuditScheduleDealerFacade(User).Retrieve(criteriaAuditScheduleDealer)

            Dim IDAuditParameter As String = ""
            For Each itemAuditScheduleDealer As AuditScheduleDealer In arlAuditScheduleDealer
                IDAuditParameter &= itemAuditScheduleDealer.AuditSchedule.AuditParameter.ID.ToString & ","
            Next

            If IDAuditParameter <> "" Then
                IDAuditParameter = Left(IDAuditParameter, IDAuditParameter.Length - 1)
                criterias.opAnd(New Criteria(GetType(AuditParameter), "ID", MatchType.InSet, "(" & IDAuditParameter & ")"))
            End If
        Else
            ' dari KTB, handle bug 1638
            Dim strDealerCode As String
            If Not IsNothing(Request.QueryString("DealerCode")) Then
                strDealerCode = Trim(Request.QueryString("DealerCode"))
                If strDealerCode <> "" Then
                    Dim criteriaAuditScheduleDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "Dealer.DealerCode", MatchType.InSet, CommonFunction.GetStrValue(strDealerCode, ";", ",")))
                    Dim arlAuditScheduleDealer As ArrayList = New AuditScheduleDealerFacade(User).Retrieve(criteriaAuditScheduleDealer)

                    Dim IDAuditParameter As String = ""
                    For Each itemAuditScheduleDealer As AuditScheduleDealer In arlAuditScheduleDealer
                        IDAuditParameter &= itemAuditScheduleDealer.AuditSchedule.AuditParameter.ID.ToString & ","
                    Next

                    If IDAuditParameter <> "" Then
                        IDAuditParameter = Left(IDAuditParameter, IDAuditParameter.Length - 1)
                        criterias.opAnd(New Criteria(GetType(AuditParameter), "ID", MatchType.InSet, "(" & IDAuditParameter & ")"))
                    Else
                        criterias.opAnd(New Criteria(GetType(AuditParameter), "ID", MatchType.Exact, -1))
                    End If
                End If
            End If
        End If

        'dipindah ke atas
        'If Request.QueryString("isinput") <> String.Empty Then
        '    If Request.QueryString("isinput") = "1" Then
        '        Dim oDealer As Dealer = CType(Session("DEALER"), Dealer)
        '        If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
        '            Dim criteriaAuditScheduleDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "Dealer.ID", MatchType.Exact, oDealer.ID.ToString))
        '            Dim arlAuditScheduleDealer As ArrayList = New AuditScheduleDealerFacade(User).Retrieve(criterias)

        '            Dim IDAuditParameter As String = ""
        '            For Each itemAuditScheduleDealer As AuditScheduleDealer In arlAuditScheduleDealer
        '                IDAuditParameter &= itemAuditScheduleDealer.AuditSchedule.AuditParameter.ID.ToString & ","
        '            Next

        '            If IDAuditParameter <> "" Then
        '                IDAuditParameter = Left(IDAuditParameter, IDAuditParameter.Length - 1)
        '                criterias.opAnd(New Criteria(GetType(AuditParameter), "ID", MatchType.InSet, "(" & IDAuditParameter & ")"))
        '            End If
        '        End If

        '    End If
        'End If

        arlAudit = New AuditParameterFacade(User).RetrieveActiveList(criterias, idx + 1, dtgAuditParameterSelection.PageSize, total, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgAuditParameterSelection.DataSource = arlAudit
        dtgAuditParameterSelection.VirtualItemCount = total
        dtgAuditParameterSelection.DataBind()
        If arlAudit.Count > 0 Then
            btnChoose.Disabled = False
        End If
    End Sub

    Private Sub dtgAuditParameterSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAuditParameterSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As AuditParameter = CType(e.Item.DataItem, AuditParameter)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If
        End If
    End Sub

    Private Sub dtgAuditParameterSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgAuditParameterSelection.SortCommand
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
        bindtoGrid(dtgAuditParameterSelection.CurrentPageIndex)
    End Sub

    Private Sub dtgAuditParameterSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAuditParameterSelection.PageIndexChanged
        dtgAuditParameterSelection.CurrentPageIndex = e.NewPageIndex
        bindtoGrid(e.NewPageIndex)
    End Sub
End Class
