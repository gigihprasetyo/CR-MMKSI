#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.IndentPart
Imports Ktb.DNet.BusinessFacade.SparePart
Imports Ktb.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

Public Class FrmAllocIndentByMaterial
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents icPODateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPODateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtNoPO As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgIndentPart As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents txtSparePartName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchProduk As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnGeneratePO As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSparePartNo As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"

    'Dim IPAD As IndentPartAllocationDetail
    'Dim IPAH As IndentPartAllocationHeader
    'Dim IPAHfcd As IndentPartAllocationHeaderFacade
    Private _sesshelper As New SessionHelper
    Private _arlIPAD As ArrayList = New ArrayList
    Private _arlIPDetail As ArrayList = New ArrayList

    ' Dim _IPAHID As Integer
    'Dim _state As Boolean
    Private _arlDetailProcess As ArrayList
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            'BindMaterialType()
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
            lblSearchProduk.Attributes("OnClick") = "ShowPPSparePartSelection()"
            lblPopUpPengajuan.Attributes("onclick") = "ShowPPNomorPengajuanSelection()"
            ViewState("currSortColumn") = "IndentPartHeader.Dealer.DealerCode"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindIPAD(_arlIPAD)
        End If
    End Sub

    Private Sub BindIPAD(ByVal arlst As ArrayList)
    End Sub

    'Private Sub BindMaterialType()
    '    Dim arl As ArrayList = New EnumMaterialType().RetrieveType()
    '    ddlMaterialType.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    '    For Each imat As EnumMaterial In arl
    '        ddlMaterialType.Items.Add(New ListItem(imat.NameType, imat.ValType.ToString))
    '    Next
    '    ddlMaterialType.SelectedIndex = -1
    'End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        viewstate("currSortColumn") = "IndentPartHeader.Dealer.DealerCode"
        viewstate("currSortDirection") = Sort.SortDirection.ASC

        BindToGrid(0)
    End Sub

    Private Sub BindToGrid(ByVal currentPageIndex As Integer)

        If icPODateFrom.Value > icPODateUntil.Value Then
            MessageBox.Show("Tanggal PO 'Dari' tidak boleh lebih Besar dari Tanggal PO 'Sampai' ")
            Exit Sub
        End If

        Dim total As Integer = 0
        Dim xsum As IndentPartDetail

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'Status KTB 1-5 Baru, Konfirmasi, BatalKonfirmasi, Rilis, BatalRilis
        criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.StatusKTB", MatchType.InSet, "(1,2,3,4,5)"))

        'Dealer 2 Level Criteria
        If txtDealerCode.Text <> "" Then
            Dim iPHeaderIdStr As String = ""
            Dim arlIPHeader As ArrayList

            Dim criteriaIPHeader As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "Dealer.DealerCode", MatchType.InSet, "('" & Replace(txtDealerCode.Text, ";", "','") & "')"))
            arlIPHeader = New IndentPartHeaderFacade(User).Retrieve(criteriaIPHeader)

            For Each itemIPHeader As IndentPartHeader In arlIPHeader
                iPHeaderIdStr = iPHeaderIdStr & itemIPHeader.ID.ToString & ","
            Next

            If iPHeaderIdStr <> "" Then
                iPHeaderIdStr = Left(iPHeaderIdStr, iPHeaderIdStr.Length - 1)
                criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.ID", MatchType.InSet, "(" & iPHeaderIdStr & ")"))
            End If

        End If

        'No Pengajuan
        If txtNoPO.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.RequestNo", MatchType.InSet, "('" & Replace(txtNoPO.Text, ";", "','") & "')"))
        End If

        'Tgl Pengajuan
        criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.RequestDate", MatchType.GreaterOrEqual, icPODateFrom.Value))
        criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.RequestDate", MatchType.LesserOrEqual, icPODateUntil.Value.AddDays(1)))

        'Tipe Barang
        'If ddlMaterialType.SelectedIndex > 0 Then
        '    criterias.opAnd(New Criteria(GetType(IndentPartDetail), "IndentPartHeader.MaterialType", MatchType.Exact, ddlMaterialType.SelectedValue))
        'End If

        If txtSparePartNo.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(IndentPartDetail), "SparePartMaster.PartName", MatchType.[Partial], txtSparePartNo.Text.Trim))
        End If

        Dim totalRow As Integer = 0
        If (currentPageIndex >= 0) Then
            '_arlIPDetail = New IndentPartDetailFacade(User).RetrieveByCriteria(criterias, currentPageIndex + 1, dtgIndentPart.PageSize, totalRow)
            _arlIPDetail = New IndentPartDetailFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgIndentPart.PageSize, totalRow, ViewState("currSortColumn"), viewstate("SortDirection"))
            dtgIndentPart.DataSource = _arlIPDetail
            dtgIndentPart.VirtualItemCount = totalRow
            dtgIndentPart.DataBind()
        End If

    End Sub


    Private Sub dtgIndentPart_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgIndentPart.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim oID As IndentPartDetail = CType(e.Item.DataItem, IndentPartDetail)
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgIndentPart.PageSize * dtgIndentPart.CurrentPageIndex)).ToString

        End If
    End Sub

    Private Sub dtgIndentPart_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgIndentPart.SortCommand
        'ViewState("currSortColumn"), viewstate("SortDirection")
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("SortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("SortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("SortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("SortDirection") = Sort.SortDirection.ASC
        End If
        BindToGrid(dtgIndentPart.CurrentPageIndex)
    End Sub

    Private Sub dtgIndentPart_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgIndentPart.ItemCommand

        If e.CommandName = "Edit" Then
            dtgIndentPart.EditItemIndex = e.Item.ItemIndex
            BindToGrid(dtgIndentPart.CurrentPageIndex)
        End If

        If e.CommandName = "Cancel" Then
            dtgIndentPart.EditItemIndex = -1
            BindToGrid(dtgIndentPart.CurrentPageIndex)
        End If

        If e.CommandName = "Save" Then
            'Cek Sisa
            Dim lblRemain As Label = e.Item.FindControl("lblRemainQty")
            Dim lblTxtAlokasi As TextBox = e.Item.FindControl("txtQtyAllocation")
            If CInt(Val(lblTxtAlokasi.Text)) > CInt(lblRemain.Text) Then
                MessageBox.Show("Quantity Alokasi Tidak Boleh Melebihi Qty Sisa")
                Exit Sub
            End If

            Dim facadeIPDetail As IndentPartDetailFacade = New IndentPartDetailFacade(User)
            Dim objIndentPartDetail As IndentPartDetail = facadeIPDetail.Retrieve(CInt(e.CommandArgument))
            objIndentPartDetail.AllocationQty = CInt(Val(lblTxtAlokasi.Text))
            Dim result = facadeIPDetail.Update(objIndentPartDetail)
            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If

            dtgIndentPart.EditItemIndex = -1
            BindToGrid(dtgIndentPart.CurrentPageIndex)


        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Select Case CType(ViewState("vsAccess"), String)

        End Select
    End Sub

    Private Sub btnGeneratePO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGeneratePO.Click
        If dtgIndentPart.EditItemIndex > 0 Then
            MessageBox.Show("Simpan Data Terlebih Dulu")
            Return
        End If

        Dim objIPDetailfacade As IndentPartDetailFacade = New IndentPartDetailFacade(User)
        Dim idIPdetail As String = ""

        For Each item As DataGridItem In dtgIndentPart.Items

            Dim chk As CheckBox = item.FindControl("chkPO")
            If chk.Checked Then
                Dim txtQty As Label = item.FindControl("txtQty")
                If Val(txtQty.Text) = 0 Then
                    MessageBox.Show("Alokasi 0 Tidak Dapat Dibuat PO")
                    Exit Sub
                End If
                Dim lbtnEdit As LinkButton = item.FindControl("lbtnEdit")
                idIPdetail = idIPdetail & lbtnEdit.CommandArgument & ","
            End If

        Next

        If idIPdetail = "" Then
            MessageBox.Show("Pilih Data Terlebih Dulu")
            Exit Sub
        Else
            idIPdetail = Left(idIPdetail, idIPdetail.Length - 1)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartDetail), "ID", MatchType.InSet, "(" & idIPdetail & ")"))

            Dim arlIPDetail As ArrayList = objIPDetailfacade.RetrieveByCriteria(criterias, "IndentPartHeader.Dealer.DealerCode", Sort.SortDirection.ASC)

            Dim Retval As String = objIPDetailfacade.GeneratePO(arlIPDetail)

            If Retval <> "" Then
                MessageBox.Show("Generate PO Dengan No " & Retval & " Berhasil")
                BindToGrid(dtgIndentPart.CurrentPageIndex)
            End If

        End If
    End Sub
End Class
