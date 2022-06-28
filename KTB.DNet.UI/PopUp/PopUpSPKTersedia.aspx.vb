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
Imports KTB.DNet.BusinessFacade.Helper
#End Region

Public Class PopUpSPKTersedia
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtSPKDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dtgSPK As System.Web.UI.WebControls.DataGrid

    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents txtSPKNumber As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub ClearData()
        Me.txtSPKNumber.Text = String.Empty
        Me.txtSPKDealer.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "SPKNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) And Not IsNothing(CType(_sessHelper.GetSession("CriteriaSPKTersedia"), CriteriaComposite)) Then

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(V_SPKTersedia), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
            Dim arrSPKHeader As ArrayList = New V_SPKTersediaFacade(User).RetrieveActiveList(indexPage + 1, dtgSPK.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection), CType(_sessHelper.GetSession("CriteriaSPKTersedia"), CriteriaComposite))

            dtgSPK.DataSource = arrSPKHeader
            dtgSPK.VirtualItemCount = totalRow
            dtgSPK.DataBind()
        End If

        If dtgSPK.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CreateCriteriaSearch()
        dtgSPK.CurrentPageIndex = 0
        BindDataGrid(dtgSPK.CurrentPageIndex)

        If dtgSPK.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub CreateCriteriaSearch()

        Dim crits As New CriteriaComposite(New Criteria(GetType(V_SPKTersedia), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'remarks by anh 201707 indent system, diubah ke view
        'crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_SPKTersedia), "Status", MatchType.Exact, CType(EnumStatusSPK.Status.Selesai, Integer)))
        If txtSPKNumber.Text <> "" Then
            crits.opAnd(New Criteria(GetType(V_SPKTersedia), "SPKNumber", MatchType.[Partial], txtSPKNumber.Text))
        End If
        If txtSPKDealer.Text <> "" Then
            crits.opAnd(New Criteria(GetType(V_SPKTersedia), "DealerSPKNumber", MatchType.[Partial], txtSPKDealer.Text))
        End If

        Dim objdealer As Dealer = New SessionHelper().GetSession("DEALER")
        If Not objdealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If Val(Request.QueryString("IsGroupDealer")) = 0 Then
                crits.opAnd(New Criteria(GetType(V_SPKTersedia), "Dealer.ID", MatchType.Exact, objdealer.ID))
            Else
                'For SPK Get by dealer group
                Dim DealerGroupID As Integer = objdealer.DealerGroup.ID
                If DealerGroupID = 21 Then 'Single Dealer
                    crits.opAnd(New Criteria(GetType(V_SPKTersedia), "Dealer.ID", MatchType.Exact, objdealer.ID))
                Else
                    crits.opAnd(New Criteria(GetType(V_SPKTersedia), "Dealer.ID", MatchType.InSet, "(select ID from Dealer where DealerGroupID=" & DealerGroupID.ToString & ")"))
                End If
            End If
        End If


        _sessHelper.SetSession("CriteriaSPKTersedia", crits)

    End Sub

    Private Sub dtgSPK_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSPK.PageIndexChanged
        dtgSPK.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgSPK.CurrentPageIndex)
    End Sub

    Private Sub dtgSPK_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSPK.SortCommand
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
        dtgSPK.CurrentPageIndex = 0
        BindDataGrid(dtgSPK.CurrentPageIndex)
    End Sub

    Private Sub dtgSPK_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPK.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objV_SPKTersedia As V_SPKTersedia = e.Item.DataItem

            e.Item.Cells(5).Text = objV_SPKTersedia.CreatedTime.ToString("dd-MM-yyyy")
            e.Item.Cells(6).Text = objV_SPKTersedia.PlanDeliveryMonth.ToString() + "-" + objV_SPKTersedia.PlanDeliveryYear.ToString() 'Periode Pengajuan SPK
            e.Item.Cells(7).Text = objV_SPKTersedia.PlanInvoiceMonth.ToString() + "-" + objV_SPKTersedia.PlanInvoiceYear.ToString() 'Rencana Pengajuan Faktur
            'e.Item.Cells(9).Text = FormatNumber(objV_SPKTersedia.Quantity, 0, TriState.UseDefault, TriState.True, TriState.UseDefault)

            Dim lblCabang As Label = e.Item.FindControl("lblCabang")

            If Not IsNothing(objV_SPKTersedia.SalesmanHeader) AndAlso Not IsNothing(objV_SPKTersedia.SalesmanHeader.DealerBranch) Then

                If (objV_SPKTersedia.SalesmanHeader.DealerBranch.DealerBranchCode <> "") Then
                    lblCabang.Text = objV_SPKTersedia.SalesmanHeader.DealerBranch.DealerBranchCode & " / " & objV_SPKTersedia.SalesmanHeader.DealerBranch.Term1
                End If

            End If

        End If
    End Sub


End Class
