#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Utility
#End Region

Public Class PopUpCsTeam
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtSalesmanCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    'Protected WithEvents chxAllEmployee As System.Web.UI.WebControls.CheckBox
    Protected WithEvents dtgSalesmanHeader As System.Web.UI.WebControls.DataGrid

    Private _sessHelper As SessionHelper = New SessionHelper

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variable"
    Private strDefDate As String = "1753/01/01"
#End Region

    Private Sub ClearData()
        Me.txtSalesmanCode.Text = String.Empty
        Me.txtName.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "SalesmanCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) And Not IsNothing(CType(_sessHelper.GetSession("CriteriaSalesmanPart"), CriteriaComposite)) Then
            Dim arrSalesmanHeader As ArrayList = New SalesmanHeaderFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("CriteriaSalesmanPart"), CriteriaComposite), indexPage + 1, dtgSalesmanHeader.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgSalesmanHeader.DataSource = arrSalesmanHeader
            dtgSalesmanHeader.VirtualItemCount = totalRow
            dtgSalesmanHeader.DataBind()
        End If

        If dtgSalesmanHeader.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
            CreateCriteriaSearch()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CreateCriteriaSearch()
        dtgSalesmanHeader.CurrentPageIndex = 0
        BindDataGrid(dtgSalesmanHeader.CurrentPageIndex)

        If dtgSalesmanHeader.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub CreateCriteriaSearch()

        ' menampilkan data salesman yang aktif & telah memiliki kode
        Dim crits As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.CS, Byte)))
        If txtSalesmanCode.Text <> "" Then
            crits.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.[Partial], txtSalesmanCode.Text))
        End If
        If txtName.Text <> "" Then
            crits.opAnd(New Criteria(GetType(SalesmanHeader), "Name", MatchType.[Partial], txtName.Text))
        End If

        crits.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)))

        If Val(Request.QueryString("IsResign")) = 1 Then
            crits.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.Exact, CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String)))
        ElseIf Val(Request.QueryString("IsResign")) = 0 Then
            crits.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.No, CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String)))
        End If
        'If Val(Request.QueryString("IsSales")) = 4 Then

        '    Dim strSql As String = "( SELECT id FROM salesmanheader WHERE SalesIndicator = 4 and RowStatus = 0 )"
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.CS, Byte)))
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.InSet, strSql))
        'End If
        Dim objdealer As Dealer = New SessionHelper().GetSession("DEALER")
        If Val(Request.QueryString("IsSales")) <> 99 Then
            If Not objdealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If Val(Request.QueryString("IsGroupDealer")) = 0 Then
                    crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.Exact, objdealer.ID))
                Else
                    'For Faktur Get by dealer group
                    Dim DealerGroupID As Integer = objdealer.DealerGroup.ID
                    If DealerGroupID = 21 Then 'Single Dealer
                        crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.Exact, objdealer.ID))
                    Else
                        crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.InSet, "(select ID from Dealer where DealerGroupID=" & DealerGroupID.ToString & ")"))
                    End If
                End If
            End If
        End If
        'If chxAllEmployee.Checked = False Then
        '    Dim strSql As String = "( SELECT id FROM salesmanheader WHERE SalesIndicator = 4 and RowStatus = 0 )"
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.CS, Byte)))
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.InSet, strSql))
        'Else

        'End If

        _sessHelper.SetSession("CriteriaSalesmanPart", crits)

    End Sub

    Private Sub dtgSalesmanHeader_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSalesmanHeader.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanHeader As SalesmanHeader = e.Item.DataItem
            'Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectSalesman"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)

                '  Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
                Dim lblPosition As Label = CType(e.Item.FindControl("lblPosition"), Label)
                '  Dim lblLevel As Label = CType(e.Item.FindControl("lblLevel"), Label)

                lblPosition.Text = objSalesmanHeader.JobPosition.Description

                'If objSalesmanHeader.SalesmanAdditionalInfo.Count > 0 Then
                '    Try
                '        ' lblCategory.Text = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName
                '        lblPosition.Text = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.PositionName
                '        If objSalesmanHeader.SalesmanAdditionalInfo.Count > 0 Then
                '            Dim iLevel As Integer = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanLevel
                '            If iLevel <> 99 Then
                '                Dim EnumLevel As EnumSalesmanPartLevel.Level = iLevel
                '                lblLevel.Text = EnumLevel.ToString
                '            Else
                '                lblLevel.Text = String.Empty
                '            End If
                '        Else
                '            lblLevel.Text = String.Empty
                '        End If
                '    Catch ex As Exception

                '    End Try


                'Else
                '    'lblCategory.Text = String.Empty
                '    lblPosition.Text = String.Empty
                '    lblLevel.Text = String.Empty
                'End If
            End If
        End If
    End Sub

    Private Sub dtgSalesmanHeader_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSalesmanHeader.PageIndexChanged
        dtgSalesmanHeader.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgSalesmanHeader.CurrentPageIndex)
    End Sub

    Private Sub dtgSalesmanHeader_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSalesmanHeader.SortCommand
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
        dtgSalesmanHeader.CurrentPageIndex = 0
        BindDataGrid(dtgSalesmanHeader.CurrentPageIndex)
    End Sub

    'Protected Sub chxAllEmployee_CheckedChanged1(sender As Object, e As EventArgs)
    '    If chxAllEmployee.Checked = False Then
    '        CreateCriteriaSearch()
    '        BindDataGrid(0)
    '    Else
    '        CreateCriteriaSearch()
    '        BindDataGrid(0)
    '    End If
    'End Sub
End Class
