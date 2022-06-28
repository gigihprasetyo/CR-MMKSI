#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility

#End Region

Public Class PopUpChassisMasterMultiSelection
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper
    Dim arrChkSelectionID As ArrayList = New ArrayList

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtMoMesin As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgChassisMaster As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNoRangka As System.Web.UI.WebControls.TextBox
    Protected WithEvents hdnIndent As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnChooseIndent As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents vCode As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents ArrayChassis As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkItemChecked As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkAll As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer. vCode 
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Private Variable"
    Dim _sessHelper As New SessionHelper

#End Region

#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim LoginDealer As Dealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim Sql As String = ""
        'sesHelper.SetSession("ArlRecallChasissMasterID", arrChkSelectionID)
        Dim totalRow As Integer = 0
        If indexPage >= 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If txtNoRangka.Text <> String.Empty Then
                Sql = " select ID from RecallChassisMaster Where ID in(select MAX(ID) as ID from RecallChassisMaster where RowStatus = 0 and ChassisNo like '%" & txtNoRangka.Text.Trim & "%' group by ChassisNo)"
                criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ID", MatchType.InSet, "(" & Sql & ")"))
                'criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ChassisNo", MatchType.Partial, txtNoRangka.Text))
            End If

            If txtMoMesin.Text <> String.Empty Then
                Sql = " select distinct(ChassisNumber) from ChassisMaster where RowStatus = 0 and EngineNumber like '%" & txtMoMesin.Text.Trim & "%'"
                criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ChassisNo", MatchType.InSet, "(" & Sql & ")"))
            End If

            If txtNoRangka.Text = String.Empty AndAlso txtMoMesin.Text = String.Empty Then
                Sql = " select top 100 ID from RecallChassisMaster where RowStatus = 0 Order By CreatedTime Desc "
                criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ID", MatchType.InSet, "(" & Sql & ")"))
            End If

            Dim arlChassisMaster As ArrayList = New RecallChassisMasterFacade(User).RetrieveActiveList(indexPage, dtgChassisMaster.PageSize, totalRow, CType(ViewState("SortColumn"), String), CType(ViewState("SortDirection"), Sort.SortDirection), criterias)
            'Dim arlChassisMaster As ArrayList = New RecallChassisMasterFacade(User).Retrieve(criterias)
            dtgChassisMaster.DataSource = arlChassisMaster
            dtgChassisMaster.VirtualItemCount = totalRow
            dtgChassisMaster.DataBind()

            sesHelper.SetSession("ArlRecallChasissMaster", arlChassisMaster)
            If arlChassisMaster.Count > 0 Then
                dtgChassisMaster.DataSource = arlChassisMaster
                dtgChassisMaster.DataBind()
                btnChoose.Disabled = False
            Else
                dtgChassisMaster.DataSource = New ArrayList
                dtgChassisMaster.DataBind()
                btnChoose.Disabled = True
            End If

            If indexPage = 0 Then
                dtgChassisMaster.CurrentPageIndex = 0
            End If

        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'ArrayChassis.Visible = False
        If Not IsPostBack Then
            sesHelper.SetSession("ArlRecallChasissMasterID", arrChkSelectionID)
            hdnIndent.Value = Val(Request.QueryString("Indent"))
            If Val(hdnIndent.Value) = 0 Then
                btnChooseIndent.Visible = False
                btnChoose.Visible = True
            Else
                btnChooseIndent.Visible = True
                btnChoose.Visible = False
            End If

            ViewState.Add("SortColumn", "ChassisNo")
            ViewState.Add("SortDirection", Sort.SortDirection.ASC)

            dtgChassisMaster.CurrentPageIndex = 0
            BindToGrid(dtgChassisMaster.CurrentPageIndex)
            'chkAll.Attributes.Add("onClick", "javascript:CheckAll('chkItemChecked', true)")
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgChassisMaster.CurrentPageIndex = 0
        BindToGrid(dtgChassisMaster.CurrentPageIndex)
    End Sub

    Private Sub dtgChassisMaster_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgChassisMaster.PageIndexChanged
        dtgChassisMaster.CurrentPageIndex = e.NewPageIndex
        BindToGrid(e.NewPageIndex + 1)
    End Sub

    Private Sub dtgChassisMaster_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgChassisMaster.SortCommand
        If e.SortExpression = CType(ViewState("SortColumn"), String) Then
            Select Case CType(ViewState("SortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("SortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("SortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("SortColumn") = e.SortExpression
            ViewState("SortDirection") = Sort.SortDirection.ASC
        End If
        ViewState.Add("SortColumn", e.SortExpression)
        BindToGrid(dtgChassisMaster.CurrentPageIndex)

    End Sub

    Private Sub dtgChassisMaster_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgChassisMaster.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim RowData As RecallChassisMaster = CType(e.Item.DataItem, RecallChassisMaster)
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim chkSelection As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
            'Dim chkSelecs As CheckBox = CType(e.Item.FindControl("chkAll"), CheckBox)
            arrChkSelectionID = CType(sesHelper.GetSession("ArlRecallChasissMaster"), ArrayList)

            'If Not IsNothing(chkAll) Then
            '    If chkAll.Checked = True Then
            '        chkSelection.Checked = True
            '    Else
            '        chkSelection.Checked = False
            '    End If

            'End If

            'For Each itemID As String In arrChkSelectionID
            '    If itemID.Trim = lblID.Text Then
            '        chkSelection.Checked = True
            '        Exit For
            '    End If
            'Next

            Dim lblNoMesin As Label = CType(e.Item.FindControl("lblNoMesin"), Label)
            Dim objChasis As ChassisMaster = New ChassisMasterFacade(User).Retrieve(RowData.ChassisNo)
            If objChasis.ID > 0 And Not IsNothing(objChasis) Then
                lblNoMesin.Text = objChasis.EngineNumber
            Else
                lblNoMesin.Text = ""
            End If

        End If

    End Sub

    Protected Sub chkSelection_CheckedChanged(sender As Object, e As EventArgs)
        Dim chk As CheckBox = CType(sender, CheckBox)
        Dim dgItem As DataGridItem = CType(chk.NamingContainer, DataGridItem)
        Dim lblID As Label = CType(dgItem.FindControl("lblID"), Label)
        Dim txtNoRangka As Label = CType(dgItem.FindControl("lblNoRangka"), Label)
        Dim strID As String = lblID.Text
        Dim txtChassis As String = txtNoRangka.Text
        ArrayChassis.Text = ""

        arrChkSelectionID = CType(sesHelper.GetSession("ArlRecallChasissMasterID"), ArrayList)
        If chk.Checked = True Then
            'arrChkSelectionID.Add(strID)
            arrChkSelectionID.Add(txtChassis)
        Else
            'arrChkSelectionID.Remove(strID)
            arrChkSelectionID.Remove(txtChassis)
        End If
        sesHelper.SetSession("ArlRecallChasissMasterID", arrChkSelectionID)

        For Each itemID As String In arrChkSelectionID
            ArrayChassis.Text += itemID + ";"
        Next
        If ArrayChassis.Text <> "" Then
            ArrayChassis.Text = ArrayChassis.Text.Remove(ArrayChassis.Text.Length - 1, 1)
        End If


    End Sub

    Protected Sub chkSelection_All(sender As Object, e As EventArgs)

        Dim chk As CheckBox = CType(sender, CheckBox)
        Dim dgItem As DataGridItem = CType(chk.NamingContainer, DataGridItem)
        arrChkSelectionID = CType(sesHelper.GetSession("ArlRecallChasissMaster"), ArrayList)
        ArrayChassis.Text = ""

        Try
            dtgChassisMaster.DataSource = arrChkSelectionID
            If arrChkSelectionID.Count > 0 Then
                Dim chkSelection As CheckBox = CType(dtgChassisMaster.FindControl("chkItemChecked"), CheckBox)
                Dim chkSelecs As CheckBox = CType(dgItem.FindControl("chkAll"), CheckBox)

                If chkSelecs.Checked Then
                    For Each itemID As RecallChassisMaster In arrChkSelectionID
                        Dim txtChassis As String = itemID.ChassisNo
                        'chkSelection.Checked = True
                        ArrayChassis.Text += txtChassis + ";"
                    Next
                    ArrayChassis.Text = ArrayChassis.Text.Remove(ArrayChassis.Text.Length - 1, 1)
                    chkSelecs.Checked = True

                Else
                    ArrayChassis.Text = ""
                End If
            End If
            'dtgChassisMaster.DataBind()

        Catch ex As Exception
        End Try

    End Sub

    
End Class
