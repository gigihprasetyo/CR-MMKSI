Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility

Public Class PopupSalesTrainingParticipant
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgParticipant As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button

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
        'Put user code to initialize the page here
        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "SalesmanHeader.SalesmanCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            bindgrid(0)
        End If
    End Sub

    Private Sub bindgrid(ByVal idxPage As Integer)

        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanTrainingParticipant), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanMasterTraining.ID", MatchType.Exact, CInt(Request.QueryString("id"))))
        ' Modified by Ikhsan, 12 Agustus 2008
        ' Requested by Rina, as Part Of CR
        ' To show confirmed and un-confirmed salesman
        ' ------------------------------------------------------------------------------------------------------
        'criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "IsConfirm", MatchType.Exact, 1))
        ' ------------------------------------------------------------------------------------------------------

        arrList = New SalesmanTrainingParticipantFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgParticipant.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgParticipant.CurrentPageIndex = idxPage
        dgParticipant.DataSource = arrList
        dgParticipant.VirtualItemCount = totalRow
        dgParticipant.DataBind()

    End Sub

    Private Sub dgParticipant_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgParticipant.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanTrainingParticipant As SalesmanTrainingParticipant = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgParticipant.CurrentPageIndex * dgParticipant.PageSize)
            ' Modified by Ikhsan, 12 Agustus 2008
            ' Requested by Rina, as Part Of CR
            ' To give list background's color red if Confirm and white if not confirm
            ' ------------------------------------------------------------------------------------------------------

            If objSalesmanTrainingParticipant.IsConfirm = 1 Then
                e.Item.BackColor = System.Drawing.Color.FromName("#CDCDCD")
            Else
                e.Item.BackColor = Color.White
            End If
            ' ------------------------------------------------------------------------------------------------------


        End If

    End Sub

    Private Sub dgParticipant_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgParticipant.SortCommand
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
        dgParticipant.SelectedIndex = -1
        dgParticipant.CurrentPageIndex = 0
        bindgrid(dgParticipant.CurrentPageIndex)

    End Sub

    Private Sub dgParticipant_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgParticipant.PageIndexChanged
        dgParticipant.CurrentPageIndex = e.NewPageIndex
        bindgrid(dgParticipant.CurrentPageIndex)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        RegisterClientScriptBlock("WindowClose", "<script language=JavaScript>window.close();</script>")
    End Sub
End Class
