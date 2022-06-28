#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class frmDisplayPKReleaseDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblKategori As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategoriValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriodeAlokasi As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriodeAlokasiValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipeValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunPerakitan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunPerakitanValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblMaterialNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblMaterialNumberValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalProduksi As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalProduksiValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblModelTipeWarna As System.Web.UI.WebControls.Label
    Protected WithEvents lblMaterialDescriptionValue As System.Web.UI.WebControls.Label
    Protected WithEvents dtgEntryAllocation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private ArrListPKDetail As ArrayList
    Private objPKdetail As PKDetail
    Private sessionHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "PKHeader.PKNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub BindDataToGrid()
        objPKdetail = sessionHelper.GetSession("AllocatedPKDetail")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
        criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PKDetail), "VechileColor.ID", MatchType.Exact, objPKdetail.VechileColor.ID))
        criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PKDetail), "PKHeader.ProductionYear", MatchType.Exact, objPKdetail.PKHeader.ProductionYear))
        criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, objPKdetail.PKHeader.RequestPeriodeMonth))
        criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, objPKdetail.PKHeader.RequestPeriodeYear))
        ArrListPKDetail = New PKDetailFacade(User).Retrieve(criterias, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        'ArrListPKDetail = New PKDetailFacade(User).Retrieve(criterias)
        dtgEntryAllocation.DataSource = ArrListPKDetail
        sessionHelper.SetSession("EntryPKDetail", ArrListPKDetail)
        dtgEntryAllocation.DataBind()
        'lblSisaProduksiValue.Text = (CInt(lblTotalProduksiValue.Text) - HitungSisaProduksi()).ToString
    End Sub

    Private Function HitungSisaProduksi() As Integer
        Dim int As Integer = 0
        For i As Integer = 0 To ArrListPKDetail.Count - 1
            Dim lblAlokasi As Label = dtgEntryAllocation.Items.Item(i).FindControl("lblAlokasiQty")
            int = int + CInt(lblAlokasi.Text)
        Next
        Return int
    End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        If Not IsPostBack Then
            ViewState("OrderType") = Request.QueryString("OrderType")
            ViewState("Periode") = Request.QueryString("Periode")
            ViewState("Category") = Request.QueryString("Category")
            ViewState("Type") = Request.QueryString("Type")
            ViewState("MaterialNumber") = Request.QueryString("MaterialNumber")
            objPKdetail = sessionHelper.GetSession("AllocatedPKDetail")
            lblKategoriValue.Text = objPKdetail.PKHeader.Category.CategoryCode
            lblMaterialDescriptionValue.Text = objPKdetail.VechileColor.MaterialDescription
            lblMaterialNumberValue.Text = objPKdetail.VechileColor.MaterialNumber
            lblPeriodeAlokasiValue.Text = CType(CInt(objPKdetail.PKHeader.RequestPeriodeMonth) - 1, enumMonth.Month).ToString & " " & objPKdetail.PKHeader.RequestPeriodeYear.ToString
            lblTahunPerakitanValue.Text = objPKdetail.PKHeader.ProductionYear
            lblTipeValue.Text = objPKdetail.VechileColor.VechileType.VechileTypeCode
            lblTotalProduksiValue.Text = CInt(Request.QueryString("TotalPP"))

            InitiatePage()
            BindDataToGrid()
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarStatusRilisViewDetail_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Status Rilis Detail")
        End If
    End Sub

    Private Sub dtgEntryAllocation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEntryAllocation.SortCommand
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
        dtgEntryAllocation.SelectedIndex = -1
        BindDataToGrid()
    End Sub

    Sub dtgEntryAllocation_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs)
        If Not (ArrListPKDetail.Count = 0 Or E.Item.ItemIndex = -1) Then
            Dim objPKDetail1 As PKDetail = ArrListPKDetail(E.Item.ItemIndex)
            E.Item.Cells(1).Text = (E.Item.ItemIndex + 1 + (dtgEntryAllocation.PageSize * dtgEntryAllocation.CurrentPageIndex)).ToString
            E.Item.Cells(3).Text = objPKDetail1.PKHeader.Dealer.DealerCode & " - " & objPKDetail1.PKHeader.Dealer.SearchTerm1
            E.Item.Cells(4).Text = objPKDetail1.PKHeader.Dealer.DealerName
            E.Item.Cells(5).Text = objPKDetail1.PKHeader.ProjectName
            E.Item.Cells(8).Text = objPKDetail1.PKHeader.Purpose

        End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        sessionHelper.RemoveSession("EntryPKDetail")
        Session.Remove("AllocatedPKDetail")
        Response.Redirect("../PK/frmDisplayPKReleaseHeader.aspx?OrderType=" & ViewState("OrderType") & "&Periode=" & ViewState("Periode") & "&Category=" & ViewState("Category") & "&Type=" & ViewState("Type") & "&MaterialNumber=" & ViewState("MaterialNumber"))
    End Sub

#End Region

    Sub dtgEntryAllocation_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs)
        If e.CommandName = "View" Then
            Dim lblNoRegPK As LinkButton = e.Item.FindControl("lbtnNoRegPK")
            Dim DealerCode As String = e.Item.Cells(3).Text.Split("-")(0)
            If e.Item.Cells(8).Text = "1" Then
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                Response.Redirect("../PK/PesananKendaraanBiasa.aspx?PKNumber=" & lblNoRegPK.Text & "&DealerCode=" & DealerCode)
            Else
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                Response.Redirect("../PK/PesananKendaraanKhusus.aspx?PKNumber=" & lblNoRegPK.Text & "&DealerCode=" & DealerCode)
            End If
        End If
    End Sub

End Class
