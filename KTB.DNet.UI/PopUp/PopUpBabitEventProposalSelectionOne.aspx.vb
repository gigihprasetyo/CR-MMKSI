#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports System.Collections.Generic
#End Region

Public Class PopUpBabitEventProposalSelectionOne
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgBabitEvent As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtEventRegNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEventName As System.Web.UI.WebControls.TextBox
    Protected WithEvents icEventDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEventDateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents chkTanggal As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlBabitMasterEventType As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " custom Declaration "
    Private sesHelper As New SessionHelper
    Private SessionGridData = "PopUpBabitEventProposalSelectionOne.gridList"
    Private objDealer As Dealer
#End Region

#Region " Custom Method "

    Private Sub ClearData()
        Me.txtEventRegNumber.Text = String.Empty
        Me.txtEventName.Text = String.Empty
        Me.icEventDateFrom.Value = Date.Now
        Me.icEventDateTo.Value = Date.Now
        chkTanggal.Checked = False
        dtgBabitEvent.DataSource = New ArrayList
        dtgBabitEvent.DataBind()
    End Sub

#End Region

#Region " Event Hendler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            ViewState("currSortColumn") = "BabitRegNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            objDealer = Session("DEALER")
            ClearData()
            ReadData()   '-- Read all data matching criteria
            BindGrid(dtgBabitEvent.CurrentPageIndex)  '-- Bind page-1
            BindBabitMasterEventType()
        End If
    End Sub

    Private Sub BindBabitMasterEventType()
        ddlBabitMasterEventType.Items.Clear()
        ddlBabitMasterEventType.Items.Add(New ListItem("Silahkan Pilih", ""))

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "FormType", MatchType.InSet, "(1,2,4,6)"))
        Dim arlBabitMasterEventType As ArrayList = New BabitMasterEventTypeFacade(User).Retrieve(criterias)
        If Not IsNothing(arlBabitMasterEventType) AndAlso arlBabitMasterEventType.Count > 0 Then
            For Each obj As BabitMasterEventType In arlBabitMasterEventType
                ddlBabitMasterEventType.Items.Add(New ListItem(obj.TypeName, obj.ID))
            Next
        End If
    End Sub

    Public Sub ReadData()
        objDealer = Session("DEALER")

        Dim dsBabitEvent As DataSet = New DataSet

        dsBabitEvent = New BabitHeaderFacade(User).RetrieveFromSPByPopUp(objDealer.ID, txtEventRegNumber.Text, txtEventName.Text, icEventDateFrom.Value, icEventDateTo.Value, chkTanggal.Checked, ddlBabitMasterEventType.SelectedValue)

        Dim _babitHeader As New BabitHeader
        Dim arrBabitEvent As New ArrayList
        Dim row As DataRow
        Dim i As Integer = 0
        For i = 0 To dsBabitEvent.Tables(0).Rows.Count - 1
            row = dsBabitEvent.Tables(0).Rows(i)
            Try
                _babitHeader = New BabitHeader
                _babitHeader.ID = row("ID")
                _babitHeader.BabitRegNumber = row("BabitRegNumber")
                _babitHeader.BabitMasterEventType = New BabitMasterEventTypeFacade(User).Retrieve(CInt(row("BabitMasterEventTypeID")))
                _babitHeader.BabitDealerNumber = row("BabitDealerNumber")
                _babitHeader.PeriodStart = row("PeriodStart")
                _babitHeader.PeriodEnd = row("PeriodEnd")
                _babitHeader.EventTypeID = row("EventTypeID")
                arrBabitEvent.Add(_babitHeader)

            Catch ex As Exception
            End Try
        Next

        sesHelper.SetSession(SessionGridData, arrBabitEvent)
        If arrBabitEvent.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrBabitMasterRetailTargetList As ArrayList = CType(sesHelper.GetSession(SessionGridData), ArrayList)

        If arrBabitMasterRetailTargetList.Count <> 0 Then
            CommonFunction.SortListControl(arrBabitMasterRetailTargetList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            dtgBabitEvent.DataSource = arrBabitMasterRetailTargetList
            dtgBabitEvent.VirtualItemCount = arrBabitMasterRetailTargetList.Count()
            dtgBabitEvent.DataBind()
            btnChoose.Disabled = False
        Else
            dtgBabitEvent.DataSource = New ArrayList
            dtgBabitEvent.VirtualItemCount = 0
            dtgBabitEvent.CurrentPageIndex = 0
            dtgBabitEvent.DataBind()
        End If
    End Sub

    Private Sub dtgBabitEvent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgBabitEvent.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As BabitHeader = CType(e.Item.DataItem, BabitHeader)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)

                Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                lblID.Text = RowValue.EventTypeID
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ReadData()
        dtgBabitEvent.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dtgBabitEvent.CurrentPageIndex)  '-- Bind page-1
        If dtgBabitEvent.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

#End Region

    Private Sub dtgBabitEvent_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgBabitEvent.SortCommand

        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        dtgBabitEvent.CurrentPageIndex = 0
        ReadData()
        BindGrid(dtgBabitEvent.CurrentPageIndex)
    End Sub

    Private Sub dtgBabitEvent_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgBabitEvent.PageIndexChanged

        ReadData()
        dtgBabitEvent.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

End Class
