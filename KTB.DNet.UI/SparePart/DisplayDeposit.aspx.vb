#Region "Summary"
'------------------------------------------------------------------'
'-- Author Name   : Agus Pirnadi                                 --'
'-- PURPOSE       :                                              --'
'-- SPECIAL NOTES :                                              --'
'------------------------------------------------------------------'
'-- Copyright ? 2005                                             --'
'------------------------------------------------------------------'
'-- $History      : $                                            --'
'-- Generated on 25/10/2005                                      --'
'------------------------------------------------------------------'
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
#End Region

Public Class DisplayDeposit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlDealerCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblClaimNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtClaimNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblCreatePeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCreateMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCreateYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblVehicleType As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon5 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlVehicleType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblDecidePeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlDecideMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlDecideYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon6 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgStatusList As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Method"

    Private Sub BindDropdownList()

        Dim listItemBlank As ListItem  '-- dummy item denoting ALL
        listItemBlank = New ListItem("Pilih", -1)

        '-- Dealer criteria & sort
        Dim criterias1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl1 As SortCollection = New SortCollection
        sortColl1.Add(New Sort(GetType(Dealer), "DealerCode", Sort.SortDirection.ASC))  '-- Sort by Dealer code
        Dim Dealers As ArrayList = New DealerFacade(User).RetrieveByCriteria(criterias1, sortColl1)

        '-- Dealer dropdownlist
        ddlDealerCode.Items.Add(listItemBlank)
        For Each item As Dealer In Dealers
            Dim listItem As New ListItem(item.DealerCode, item.ID)
            ddlDealerCode.Items.Add(listItem)
        Next

        '-- Create period: month
        ddlCreateMonth.Items.Add(listItemBlank)
        For Each item As ListItem In LookUp.ArrayMonth
            ddlCreateMonth.Items.Add(item)
        Next
        ddlCreateMonth.SelectedValue = DateTime.Now.Month

        '-- Create period: year
        ddlCreateYear.Items.Add(listItemBlank)
        For Each item As ListItem In LookUp.ArrayYear(True, 10, 5, DateTime.Now.Year.ToString)
            ddlCreateYear.Items.Add(item)
        Next
        ddlCreateYear.SelectedValue = DateTime.Now.Year

        '-- Decide period: month
        ddlDecideMonth.Items.Add(listItemBlank)
        For Each item As ListItem In LookUp.ArrayMonth
            ddlDecideMonth.Items.Add(item)
        Next

        '-- Decide period: year
        ddlDecideYear.Items.Add(listItemBlank)
        For Each item As ListItem In LookUp.ArrayYear(True, 10, 5, DateTime.Now.Year.ToString)
            ddlDecideYear.Items.Add(item)
        Next

        '-- Vehicle type criteria & sort
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl2 As SortCollection = New SortCollection
        sortColl2.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))  '-- Sort by vechile type code
        Dim VehicleTypes As ArrayList = New VechileTypeFacade(User).RetrieveByCriteria(criterias2, sortColl2)

        '-- Vehicle type dropdownlist
        ddlVehicleType.Items.Add(listItemBlank)
        For Each item As VechileType In VehicleTypes
            Dim listItem As New ListItem(item.VechileTypeCode, item.ID)
            ddlVehicleType.Items.Add(listItem)
        Next

        Dim listItemBlank2 As ListItem  '-- dummy item denoting ALL
        listItemBlank2 = New ListItem("Pilih", "")

        '-- Claim status dropdownlist
        ddlStatus.Items.Add(listItemBlank2)
        For Each item As ListItem In LookUp.ArrayClaimStatus
            ddlStatus.Items.Add(item)
        Next

    End Sub

    Private Function ReasonText(ByVal ReasonCode As String) As String
        '-- Return description of the reason code if any

        Dim TheReason As Reason = New ReasonFacade(User).Retrieve(ReasonCode)
        Return TheReason.Description
    End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsPostBack Then
            '-- Init dropdownlist the first time this form is loaded
            BindDropdownList()

            If Not IsNothing(Session("WSCStatusList")) Then
                '-- Retrieve WSC status list from session
                Dim WSCStatusList As ArrayList = CType(Session("WSCStatusList"), ArrayList)
                '-- Bind and display
                dgStatusList.DataSource = WSCStatusList
                dgStatusList.DataBind()
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        '-- At least one of Create period or Decide period must be filled in
        If (ddlCreateMonth.SelectedValue = -1 Or ddlCreateYear.SelectedValue = -1) And _
           (ddlDecideMonth.SelectedValue = -1 Or ddlDecideYear.SelectedValue = -1) Then

            MessageBox.Show(SR.InvalidCreatePeriod)
            Return
        End If

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '-- Dealer code
        If ddlDealerCode.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "Dealer.ID", MatchType.Exact, ddlDealerCode.SelectedValue))
        End If
        '-- Periode create
        If (ddlCreateMonth.SelectedValue <> -1) AndAlso (ddlCreateYear.SelectedValue <> -1) Then
            '-- Pass its period range
            criterias.opAnd(New Criteria(GetType(WSCHeader), "CreatedTime", MatchType.GreaterOrEqual, _
                            New Date(ddlCreateYear.SelectedItem.Value, ddlCreateMonth.SelectedValue, 1)))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "CreatedTime", MatchType.LesserOrEqual, _
                            EndPeriodDate.EndDate(ddlCreateMonth.SelectedValue, ddlCreateYear.SelectedItem.Value)))
        End If
        '-- Periode decide
        If (ddlDecideMonth.SelectedValue <> -1) AndAlso (ddlDecideYear.SelectedValue <> -1) Then
            '-- Pass its period range
            criterias.opAnd(New Criteria(GetType(WSCHeader), "DecideDate", MatchType.GreaterOrEqual, _
                            New Date(ddlDecideYear.SelectedItem.Value, ddlDecideMonth.SelectedValue, 1)))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "DecideDate", MatchType.LesserOrEqual, _
                            EndPeriodDate.EndDate(ddlDecideMonth.SelectedValue, ddlDecideYear.SelectedItem.Value)))
        End If
        '-- Nomor klaim (LIKE '%%')
        If txtClaimNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimNumber", MatchType.[Partial], txtClaimNo.Text.Trim()))
        End If
        '-- Tipe Kendaraan
        If ddlVehicleType.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ChassisMaster.VechileColor.VechileType.ID", MatchType.Exact, ddlVehicleType.SelectedValue))
        End If
        '-- Status klaim
        If ddlStatus.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimStatus", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(WSCHeader), "Dealer.DealerCode", Sort.SortDirection.ASC))  '-- Kode dealer
        sortColl.Add(New Sort(GetType(WSCHeader), "ClaimNumber", Sort.SortDirection.ASC))        '-- Nomor klaim

        '-- Retrieve recordset
        Dim WSCStatusList As ArrayList = New WSCHeaderFacade(User).RetrieveByCriteria(criterias, sortColl)

        '-- Store WSC status list for later refresh after coming back from FrmWSCDetail.aspx
        'Todo session
        Session.Add("WSCStatusList", WSCStatusList)

        '-- Bind and display
        dgStatusList.DataSource = WSCStatusList
        dgStatusList.DataBind()

        If WSCStatusList.Count = 0 Then MessageBox.Show(SR.DataNotFound("Data"))

    End Sub

    Private Sub dgStatusList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgStatusList.ItemCommand

        '-- Retrieve WSC header and its details of the chosen header
        Dim WSCHead As WSCHeader = New WSCHeaderFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

        '-- Store WSC header and its details for display on form FrmWSCDetail
        'Todo session
        Session.Add("WSCHead", WSCHead)

        If e.CommandName = "lnkClaimNumber" Then
            '-- Display WSC header and its details on WSC Detail Info page
            Response.Redirect("FrmWSCDetail.aspx")
        ElseIf e.CommandName = "lnkEmail" Then
            '-- Display WSC send email
            Response.Redirect("FrmWSCSendEmail.aspx")
        End If

    End Sub

    Private Sub dgStatusList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgStatusList.ItemDataBound
        Dim lblReason As Label = CType(e.Item.FindControl("lblReason"), Label)

        If Not IsNothing(lblReason) Then
            lblReason.ToolTip = ReasonText(lblReason.Text)
        End If
    End Sub

#End Region

End Class
