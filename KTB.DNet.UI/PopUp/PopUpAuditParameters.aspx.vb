#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.ShowroomAudit
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class PopUpAuditParameters
    Inherits System.Web.UI.Page

#Region "Deklarasi"
    Dim criterias As CriteriaComposite
    Dim sHelper As New SessionHelper
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgParamAuditList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton

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
        'BindYear()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            viewstate.Add("SortColPL", "Code")
            viewstate.Add("SortDirectionPL", Sort.SortDirection.ASC)
        End If
        BindToGrid(0)
        '  End If
    End Sub

    Private Sub dtgParamAuditList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgParamAuditList.PageIndexChanged
        dtgParamAuditList.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgParamAuditList.CurrentPageIndex)
    End Sub

    Private Sub dtgParamAuditList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgParamAuditList.SortCommand
        If e.SortExpression = viewstate("SortColPL") Then
            If viewstate("SortDirectionPL") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirectionPL", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirectionPL", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColPL", e.SortExpression)
        BindToGrid(0)
    End Sub

    Private Sub dtgParamAuditList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgParamAuditList.ItemDataBound
        Dim arlAllData As ArrayList = sHelper.GetSession("arlAllData")
        'If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
        '    e.Item.Cells(1).Text = (dtgParamAuditList.CurrentPageIndex * dtgParamAuditList.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No
        'End If
    End Sub

    'Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    dtgParamAuditList.CurrentPageIndex = 0
    '    BindDataToGrid(dtgParamAuditList.CurrentPageIndex)
    'End Sub

#End Region

#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer
        If indexPage >= 0 Then

            CreateCriteria()
            Dim arlAllData As ArrayList = New AuditParameterFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgParamAuditList.PageSize, totalRow, viewstate("SortColPL"), viewstate("SortDirectionPL"))
            dtgParamAuditList.VirtualItemCount = totalRow
            If arlAllData.Count > 0 Then
                dtgParamAuditList.DataSource = arlAllData
                btnChoose.Disabled = False
            Else
                btnChoose.Disabled = True
                dtgParamAuditList.DataSource = New ArrayList
            End If
            If indexPage = 0 Then
                dtgParamAuditList.CurrentPageIndex = 0
            End If

            sHelper.SetSession("arlAllData", arlAllData)
            dtgParamAuditList.DataBind()
        End If
    End Sub

    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(AuditParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(AuditParameter), "IsRilis", MatchType.Exact, 1))

        Dim oDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            Dim criteriaAuditScheduleDealer As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealer), "Dealer.ID", MatchType.Exact, oDealer.ID.ToString))
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
            Dim strDealerCode As String
            ' case from KTB,  fix bug 1638
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
                        criterias.opAnd(New Criteria(GetType(AuditParameter), "ID", MatchType.Exact, -1))  ' -1 , jadi data tdk ada
                    End If
                End If
            End If
        End If
    End Sub

    'Private Sub BindYear()
    '    Dim arlPeriode As ArrayList
    '    Dim i As Integer
    '    Dim crPeriod As New CriteriaComposite(New Criteria(GetType(AuditParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    crPeriod.opAnd(New Criteria(GetType(AuditParameter), "IsRilis", MatchType.Exact, 1))

    '    arlPeriode = New AuditParameterFacade(User).Retrieve(crPeriod)

    '    ddlYearPeriod.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    '    For i = 0 To arlPeriode.Count - 1
    '        ddlYearPeriod.Items.Add(CType(arlPeriode(i), AuditParameter).Period.ToString)
    '    Next
    '    ddlYearPeriod.SelectedIndex = -1
    'End Sub

    Private Sub BindDataToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer
        If indexPage >= 0 Then

            criterias = New CriteriaComposite(New Criteria(GetType(AuditParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(AuditParameter), "Period", MatchType.Exact, ddlYearPeriod.SelectedValue))
            criterias.opAnd(New Criteria(GetType(AuditParameter), "IsRilis", MatchType.Exact, 1))
            Dim arlAllData As ArrayList = New AuditParameterFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgParamAuditList.PageSize, totalRow, viewstate("SortColPL"), viewstate("SortDirectionPL"))
            dtgParamAuditList.VirtualItemCount = totalRow
            If arlAllData.Count > 0 Then
                dtgParamAuditList.DataSource = arlAllData
                btnChoose.Disabled = False
            Else
                btnChoose.Disabled = True
                dtgParamAuditList.DataSource = New ArrayList
            End If
            If indexPage = 0 Then
                dtgParamAuditList.CurrentPageIndex = 0
            End If

            sHelper.SetSession("arlAllData", arlAllData)
            dtgParamAuditList.DataBind()
        End If
    End Sub

#End Region
End Class
