#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
#End Region



Public Class FrmSalesmanTurnOver
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ddlSalesmanUnit As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgSalesmanTurnOver As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents icEndDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icStartDate As KTB.DNet.WebCC.IntiCalendar

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    'Private _SalesmanAreaFacade As New SalesmanAreaFacade(User)
    'Private _DealerFacade As New DealerFacade(User)
    Private _SalesmanHeaderFacade As New SalesmanHeaderFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper

#End Region

#Region "Custom Methods"
    Private Function CheckValidation() As Boolean
        Dim blnValid As Boolean = True
        If icStartDate.Value > icEndDate.Value Then
            blnValid = False
            MessageBox.Show("Periode mulai tidak boleh lebih besar dari Periode akhir")
            Return (blnValid)
        End If
        Return blnValid
    End Function
    Private Sub ClearData()
        ddlSalesmanUnit.SelectedIndex = -1
        txtDealerCode.Text = String.Empty
        icStartDate.Value = DateTime.Now
        icEndDate.Value = DateTime.Now

        If dgSalesmanTurnOver.Items.Count > 0 Then
            dgSalesmanTurnOver.SelectedIndex = -1
        End If
        'ViewState.Add("vsProcess", "Insert")
    End Sub
    ' penambahan untuk initialize data
    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "Dealer.DealerCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub BindControlsAttribute()
        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
    End Sub
    ' Untuk bind data yg bersangkutan - related
    Private Sub BindDropDownLists()
        CommonFunction.BindFromEnum("SalesmanUnit", ddlSalesmanUnit, Me.User, True, "NameStatus", "ValStatus")
    End Sub
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("SalesmanUnit", ddlSalesmanUnit.SelectedValue.ToString)
        crits.Add("DealerCode", txtDealerCode.Text)
        crits.Add("CreatedTimeGreaterOrEqual", icStartDate.Value)
        crits.Add("CreatedTimeLesserOrEqual", icEndDate.Value)
        sessHelper.SetSession("TurnOver", crits)
    End Sub
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sessHelper.GetSession("TurnOver"), Hashtable)
        If Not IsNothing(crits) Then
            ddlSalesmanUnit.SelectedValue = CStr(crits.Item("SalesmanUnit"))
            txtDealerCode.Text = CStr(crits.Item("DealerCode"))
            icStartDate.Value = CDate(crits.Item("CreatedTimeGreaterOrEqual"))
            icEndDate.Value = CDate(crits.Item("CreatedTimeLesserOrEqual"))
        End If
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "Dealer.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strDefDate As String = "1753/01/01"

        ' menentukan sales unit
        'If ddlSalesmanUnit.SelectedItem.Text <> String.Empty Then
        '    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, ddlSalesmanUnit.SelectedValue))
        'End If
        'default kriteria - sudah harus teregister [salesman code generated]
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, "1"))

        'If ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Sparepart Then
        '    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SparePartIndicator", MatchType.Exact, 1))
        'ElseIf ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Unit Then
        '    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesUnitIndicator", MatchType.Exact, 1))
        'ElseIf ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Mekanik Then
        '    criterias.opAnd(New Criteria(GetType(SalesmanHeader), "MechanicIndicator", MatchType.Exact, 1))
        'End If
        If ddlSalesmanUnit.SelectedItem.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, ddlSalesmanUnit.SelectedValue))
        End If

        ' menentukan dealer kode
        If txtDealerCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))
        End If
        ' menentukan tanggal masuk mulai
        If icStartDate.Value <> New DateTime Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.GreaterOrEqual, icStartDate.Value))
        End If
        ' menentukan tanggal masuk akhir
        If icEndDate.Value <> New DateTime Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.LesserOrEqual, icEndDate.Value))
        End If

        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing(CType(ViewState("CurrentSortColumn"), String))) And (Not IsNothing(CType(ViewState("CurrentSortColumn"), String))) Then
            sortColl.Add(New Sort(GetType(SalesmanHeader), "Dealer.DealerCode", CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
        Else
            sortColl = Nothing
        End If

        arrList = _SalesmanHeaderFacade.Retrieve(criterias, sortColl)
        'arrList = _SalesmanHeaderFacade.RetrieveByCriteria(criterias, idxPage + 1, dgSalesmanTurnOver.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        Dim arrFilter As ArrayList = New ArrayList
        Dim tmpDealerCode As String = ""
        ' menghitung jumlah yg ada
        For Each item As SalesmanHeader In arrList
            ' jika sdh ada sebelumnya
            If item.Dealer.DealerCode = tmpDealerCode Then
                'update
                Dim objTmpSalesman As SalesmanHeader = CType(arrFilter(arrFilter.Count - 1), SalesmanHeader)
                If item.ResignDate <> Date.Parse(strDefDate) Then
                    objTmpSalesman.TotalResign = objTmpSalesman.TotalResign + 1
                Else
                    objTmpSalesman.TotalHire = objTmpSalesman.TotalHire + 1
                End If
            Else
                'insert
                If item.ResignDate <> Date.Parse(strDefDate) Then
                    item.TotalHire = 0
                    item.TotalResign = 1
                Else
                    item.TotalHire = 1
                    item.TotalResign = 0
                End If
                arrFilter.Add(item)

            End If
            tmpDealerCode = item.Dealer.DealerCode
        Next

        dgSalesmanTurnOver.DataSource = CommonFunction.PageAndSortArraylist(arrFilter, idxPage, dgSalesmanTurnOver.PageSize, GetType(SalesmanHeader), "Dealer.DealerCode", ViewState("CurrentSortDirect"))
        'dgSalesmanTurnOver.DataSource = arrFilter
        dgSalesmanTurnOver.VirtualItemCount = arrFilter.Count
        dgSalesmanTurnOver.DataBind()
    End Sub
    Private Sub SetSetting()
        If Not IsNothing(Request.QueryString("Mode")) Then
            Select Case Request.QueryString("Mode")
                Case "unit"
                    ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Unit
                Case "part"
                    ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Sparepart
                Case "servis"
                    ddlSalesmanUnit.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Mekanik
            End Select
            ddlSalesmanUnit.Enabled = False
        End If

    End Sub

#End Region

#Region "EventHandlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            SetSetting()
            Initialize()
            BindDropDownLists()
            BindControlsAttribute()
            ReadCriteria()
            BindDataGrid(0)
        End If
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SaveCriteria()
        If CheckValidation() Then
            dgSalesmanTurnOver.CurrentPageIndex = 0
            BindDataGrid(0)
        End If
    End Sub
    Private Sub dgSalesmanTurnOver_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanTurnOver.SortCommand
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
        dgSalesmanTurnOver.SelectedIndex = -1
        'dgSalesmanTurnOver.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanTurnOver.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanTurnOver_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanTurnOver.PageIndexChanged
        dgSalesmanTurnOver.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanTurnOver.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanTurnOver_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanTurnOver.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanHeader As SalesmanHeader = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSalesmanTurnOver.CurrentPageIndex * dgSalesmanTurnOver.PageSize)

            Dim lblDealerCodeNew As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            lblDealerCodeNew.Text = objSalesmanHeader.Dealer.DealerCode

            Dim lblSalesHireNew As Label = CType(e.Item.FindControl("lblSalesHire"), Label)
            lblSalesHireNew.Text = CType(objSalesmanHeader.TotalHire, String)

            Dim lblSalesResignNew As Label = CType(e.Item.FindControl("lblSalesResign"), Label)
            lblSalesResignNew.Text = CType(objSalesmanHeader.TotalResign, String)

            Dim lblEditNew As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lblEditNew.Attributes("onclick") = "showPopUp('../Salesman/FrmSalesmanTurnOverAnalisa.aspx?DealerCode=" & objSalesmanHeader.Dealer.DealerCode & "');"

            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            lbtnView.CommandArgument = CType(objSalesmanHeader.Dealer.DealerCode, String)
        End If

        If (e.Item.ItemType = ListItemType.Footer) Then

            Dim total As Integer = 0
            For Each x As WebControls.DataGridItem In dgSalesmanTurnOver.Items
                total = total + CType(CType(x.FindControl("lblSalesHire"), Label).Text, Integer)
            Next
            CType(e.Item.FindControl("lblTotalSalesHire"), Label).Text = total.ToString("#,##0")

            total = 0
            For Each x As WebControls.DataGridItem In dgSalesmanTurnOver.Items
                total = total + CType(CType(x.FindControl("lblSalesResign"), Label).Text, Integer)
            Next
            CType(e.Item.FindControl("lblTotalSalesResign"), Label).Text = total.ToString("#,##0")
        End If
    End Sub
    Private Sub dgSalesmanTurnOver_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanTurnOver.ItemCommand

        Dim crits As Hashtable = New Hashtable
        'Dealer
        crits.Add("DealerCode", e.CommandArgument)
        crits.Add("CreatedTimeGreaterOrEqual", icStartDate.Value)
        crits.Add("CreatedTimeLesserOrEqual", icEndDate.Value)
        sessHelper.SetSession("TO", crits)

        Dim mode As String = String.Empty
        If e.CommandName = "View" Then

            sessHelper.SetSession("mode", "View")
            If ddlSalesmanUnit.SelectedValue = 0 Then
                mode = "part"
            ElseIf ddlSalesmanUnit.SelectedValue = 1 Then
                mode = "unit"
            ElseIf ddlSalesmanUnit.SelectedValue = 2 Then
                mode = "servis"
            End If

            'Response.Redirect("../Salesman/FrmSalesmanEntryResign.aspx?Mode=" & sessHelper.GetSession("mode") & "&From=TO")
            Response.Redirect("../Salesman/FrmSalesmanEntryResign.aspx?Mode=" & mode & "&From=TO")
        End If
    End Sub
    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender

    End Sub

#End Region

#Region "Need To Add"

    
#End Region

#Region "Check Privilege"
    ' ini perlu set security
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ResignViewRecap_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Tenaga Penjual - Rekap Pengunduran Diri Tenaga Penjual")
        End If
    End Sub
#End Region
End Class
