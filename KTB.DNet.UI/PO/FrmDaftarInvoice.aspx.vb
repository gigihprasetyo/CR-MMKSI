#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Configuration
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessValidation.Helpers
Imports KTB.DNet.BusinessFacade
#End Region


Public Class FrmDaftarInvoice
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoMO As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblOrdertype As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerPO As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerPO As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlSalesOrg As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgDaftarInv As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNoSO As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoOC As System.Web.UI.WebControls.TextBox
    Protected WithEvents icListDI1 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icListDI2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtNoInvoice As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblJmlHargaTebus As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrderTypeColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerPOColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoMOColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalTebus As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNoDebitCharge As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoDebitMemo As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents ddlInvoiceKind As DropDownList
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

    Private arlDaftarInvoice As New ArrayList
    Private arlTotalDaftarInvoice As New ArrayList
    Private objInvoice As Invoice
    Private objDealer As Dealer
    Private sessionHelper As New sessionHelper
    Private _sessDataToDownload As String = "FrmDaftarInvoice._sessDataToDownload"
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
#End Region

#Region "Custom Method"


    Private Sub CommandDelete(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        Try

            Dim objInvoiceFacade As New InvoiceFacade(User)
            Dim varID As Integer = CInt(e.Item.Cells(0).Text)
            Dim _Invoice As New KTB.DNet.Domain.Invoice

            _Invoice.ID = varID
            _Invoice = objInvoiceFacade.Retrieve(_Invoice.ID)

            'Tambahan SLA, jika sudah ada LogisticFee dan masih aktif tidak boleh didelete
            If Not IsNothing(_Invoice.LogisticDN) Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.LogisticFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.LogisticFee), "LogisticDN.ID", MatchType.Exact, _Invoice.LogisticDN.ID))
                Dim arLogisticFee As ArrayList = New LogisticFeeFacade(User).Retrieve(criterias)
                If Not IsNothing(arLogisticFee) Then
                    'Jika ada Logistic Fee yang aktif dan baru maka diupdate manjadi non aktif
                    If arLogisticFee.Count > 0 Then
                        Dim logisticFee As LogisticFee = arLogisticFee(0)
                        If logisticFee.Status = EnumLogisticFeeStatus.LogisticFeeStatus.Baru Then
                            Dim objLogisticFeeFacade As New LogisticFeeFacade(User)
                            logisticFee.RowStatus = DBRowStatus.Deleted
                            objLogisticFeeFacade.Delete(logisticFee)
                        Else
                            MessageBox.Show("Tidak boleh delete. Masih ada Logistic Cost yang aktif")
                            Exit Sub
                        End If
                    End If
                End If
            End If
                'End Tambahan SLA

                _Invoice.RowStatus = DBRowStatus.Deleted

                objInvoiceFacade.Update(_Invoice)

                MessageBox.Show(SR.DeleteSucces)
                btnCari_Click(Me, Nothing)

        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try


    End Sub


    Private Sub dtgDaftarInv_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgDaftarInv.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "Delete".ToLower()
                CommandDelete(e)
        End Select
    End Sub

    Sub dtgDaftarInv_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDaftarInv.PageIndexChanged
        dtgDaftarInv.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Private Sub BindGrid()
        BindToDataGrid(dtgDaftarInv.CurrentPageIndex)
        dtgDaftarInv.DataBind()
        TotalHargaTebus()
        'TotalQuantity()
    End Sub

    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Sub BindToddlCategory()
        Try
            ddlSalesOrg.Items.Clear()
            Dim arrayListCategory As ArrayList = New PKHeaderFacade(User).RetrieveListCategory
            ddlSalesOrg.ClearSelection()
            If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                Dim listitemBlank As New ListItem("Silahkan Pilih", 0)
                listitemBlank.Selected = False
                ddlSalesOrg.Items.Add(listitemBlank)
            End If
            Dim PCID As Short = GetProductCategoryID()
            For Each item As Category In arrayListCategory
                Dim listItem As New ListItem(item.CategoryCode, item.ID)
                listItem.Selected = False
                If item.CategoryCode = "PC" Then
                    If SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                        If PCID = 0 OrElse PCID = item.ProductCategory.ID Then
                            ddlSalesOrg.Items.Add(listItem)
                        End If
                    End If
                ElseIf item.CategoryCode = "LCV" Then
                    If SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                        If PCID = 0 OrElse PCID = item.ProductCategory.ID Then
                            ddlSalesOrg.Items.Add(listItem)
                        End If
                    End If
                ElseIf item.CategoryCode = "CV" Then
                    If SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                        If PCID = 0 OrElse PCID = item.ProductCategory.ID Then
                            ddlSalesOrg.Items.Add(listItem)
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlSalesOrg, silahkan kirim error ini ke dnet admin")
        End Try
    End Sub

    Private Sub BindddlInvoiceKind()
        ddlInvoiceKind.Items.Clear()
        ddlInvoiceKind.DataSource = enumInvoice.GetListOfInvoiceKind()
        ddlInvoiceKind.DataTextField = "NameTipe"
        ddlInvoiceKind.DataValueField = "ValTipe"
        ddlInvoiceKind.DataBind()
        ddlInvoiceKind.Items.Insert(0, New ListItem("Silakan Piih", -1))
        ddlInvoiceKind.SelectedIndex = -1
    End Sub

    Private Function GetProductCategoryID() As Short
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
    End Function

    Private Sub BindToDataGrid(ByVal currentPageIndex As Integer, Optional ByVal IsForDownload As Boolean = False)
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Invoice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "SalesOrder.POHeader.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        If txtKodeDealer.Text <> String.Empty Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "SalesOrder.POHeader.Dealer.CreditAccount", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
            Else
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "SalesOrder.POHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
            End If
        End If
        Dim IsDSF As Boolean = (CType(Session.Item("DEALER"), Dealer).Title = CType(EnumDealerTittle.DealerTittle.LEASING, String))
        If IsDSF Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "SalesOrder.POHeader.IsFactoring", MatchType.Exact, "1"))
        End If
        If txtNoInvoice.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "InvoiceNumber", MatchType.Exact, txtNoInvoice.Text))
        End If

        If ddlSalesOrg.Items.Count > 0 AndAlso ddlSalesOrg.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "SalesOrder.POHeader.ContractHeader.Category.CategoryCode", MatchType.Exact, ddlSalesOrg.SelectedItem.Text))
        End If

        If ddlInvoiceKind.SelectedValue.ToString() <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "InvoiceKind", MatchType.Exact, CInt(ddlInvoiceKind.SelectedValue)))
        End If

        '--Get Criterias From TextBox
        If txtDealerPO.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "SalesOrder.POHeader.DealerPONumber", MatchType.Exact, txtDealerPO.Text))
        End If

        If txtNoSO.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "SalesOrder.SONumber", MatchType.Exact, txtNoSO.Text))
        End If

        If txtNoOC.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "SalesOrder.POHeader.ContractHeader.ContractNumber", MatchType.Exact, txtNoOC.Text))
        End If

        If txtNoDebitCharge.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "SalesOrder.LogisticDCHeader.DebitChargeNo", MatchType.Exact, txtNoDebitCharge.Text))
        End If

        If txtNoDebitMemo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "LogisticDN.DebitMemoNo", MatchType.Exact, txtNoDebitMemo.Text))
        End If
        
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "InvoiceType", MatchType.NotInSet, "('ZA7O','ZF7O')"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "InvoiceType", MatchType.NotInSet, "('ZA7O')"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "InvoiceType", MatchType.NotInSet, "('ZF7O')"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "InvoiceNumber", MatchType.NotInSet, "(select InvoiceRef from Invoice where InvoiceRef is not null)"))

        Dim PCID As Short = Me.GetProductCategoryID()
        If PCID > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "SalesOrder.POHeader.ContractHeader.Category.ProductCategory.ID", MatchType.Exact, PCID))
        End If


        '--Get Criterias From Calendar
        If CType(icListDI1.Value, Date) <= CType(icListDI2.Value, Date) Then
            Dim TanggalAwal As New DateTime(CInt(icListDI1.Value.Year), CInt(icListDI1.Value.Month), CInt(icListDI1.Value.Day), 0, 0, 0)
            Dim TanggalAkhir As New DateTime(CInt(icListDI2.Value.Year), CInt(icListDI2.Value.Month), CInt(icListDI2.Value.Day), 23, 59, 59)
            Dim Time As TimeSpan = TanggalAkhir.Subtract(TanggalAwal)
            If Time.Days <= 65 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "InvoiceDate", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "InvoiceDate", MatchType.LesserOrEqual, Format(TanggalAkhir, "yyyy-MM-dd HH:mm:ss")))

                If IsForDownload Then
                    Dim aIs As ArrayList
                    Dim oSorts As New SortCollection

                    oSorts.Add(New Sort(GetType(Invoice), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
                    aIs = New InvoiceFacade(User).Retrieve(criterias, oSorts)
                    Me.sessionHelper.SetSession(Me._sessDataToDownload, aIs)
                Else
                    arlDaftarInvoice = New InvoiceFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgDaftarInv.PageSize, _
                    total, CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

                    sessionHelper.SetSession("DaftarInvoice", arlDaftarInvoice)
                    dtgDaftarInv.DataSource = arlDaftarInvoice
                    Dim sorts As New SortCollection
                    sorts.Add(New Sort(GetType(Invoice), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
                    arlTotalDaftarInvoice = New InvoiceFacade(User).Retrieve(criterias, sorts)
                    sessionHelper.SetSession("DaftarInvoiceAll", arlTotalDaftarInvoice)
                    dtgDaftarInv.VirtualItemCount = total

                End If

            Else
                MessageBox.Show("Periode Melebihi 65 Hari")
            End If
        Else
            MessageBox.Show(SR.InvalidRangeDate)
        End If

    End Sub

    Private POViewPrivilege As Boolean
    Private POSOViewPrivilege As Boolean
    Sub dtgDaftarInv_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        'arlDaftarInvoice = sessionHelper.GetSession("DaftarInvoice")
        If Not (arlDaftarInvoice Is Nothing) Then
            If Not (e.Item.ItemIndex = -1) Then
                objInvoice = CType(arlDaftarInvoice(e.Item.ItemIndex), Invoice)
                e.Item.Cells(0).Text = objInvoice.ID
                e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgDaftarInv.PageSize * dtgDaftarInv.CurrentPageIndex)).ToString
                Dim lblDealerCode As Label = e.Item.FindControl("lblDealerCode")
                Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
                lblDealerCode.Text = objInvoice.SalesOrder.POHeader.ContractHeader.Dealer.DealerCode
                lblDealerCode.ToolTip = objInvoice.SalesOrder.POHeader.ContractHeader.Dealer.SearchTerm1
                lblCreditAccount.Text = objInvoice.SalesOrder.POHeader.ContractHeader.Dealer.CreditAccount

                'e.Item.Cells(2).Text = objInvoice.DealerCode.ToString
                ''e.Item.Cells(4).Text = objInvoice.InvoiceNumber.ToString
                ''e.Item.Cells(5).Text = objInvoice.SalesOrder.SONumber.ToString
                ''e.Item.Cells(6).Text = objInvoice.SalesOrder.POHeader.PONumber.ToString
                ''e.Item.Cells(7).Text = objInvoice.SalesOrder.POHeader.DealerPONumber.ToString
                ''e.Item.Cells(8).Text = Format(objInvoice.InvoiceDate, "dd/MM/yyyy")
                ''e.Item.Cells(9).Text = objInvoice.SalesOrder.POHeader.ContractHeader.ProjectName
                ''e.Item.Cells(10).Text = objInvoice.SalesOrder.POHeader.ContractHeader.Category.CategoryCode.ToString
                ''e.Item.Cells(11).Text = FormatNumber(objInvoice.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

                e.Item.Cells(5).Text = objInvoice.InvoiceNumber.ToString
                If Not IsNothing(objInvoice.SalesOrder.LogisticDCHeader) Then
                    e.Item.Cells(8).Text = objInvoice.SalesOrder.LogisticDCHeader.DebitChargeNo
                End If
                e.Item.Cells(9).Text = objInvoice.SalesOrder.SONumber.ToString
                e.Item.Cells(10).Text = objInvoice.SalesOrder.POHeader.PONumber.ToString
                e.Item.Cells(11).Text = objInvoice.SalesOrder.POHeader.DealerPONumber.ToString
                e.Item.Cells(12).Text = Format(objInvoice.InvoiceDate, "dd/MM/yyyy")
                e.Item.Cells(13).Text = objInvoice.SalesOrder.POHeader.ContractHeader.ProjectName
                e.Item.Cells(14).Text = objInvoice.SalesOrder.POHeader.ContractHeader.Category.CategoryCode.ToString
                e.Item.Cells(15).Text = FormatNumber(objInvoice.SalesOrder.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Dim ppnFromDbPPNMaster = CalcHelper.GetPPNMasterByTaxTypeId(objInvoice.InvoiceDate.Date, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                If Not IsNothing(objInvoice.LogisticDN) Then
                    e.Item.Cells(7).Text = objInvoice.LogisticDN.DebitMemoNo
                    'Dim PPN As Decimal = objInvoice.LogisticDN.TotalAmount * 0.1
                    Dim PPN As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnFromDbPPNMaster, dpp:=objInvoice.LogisticDN.TotalAmount)
                    e.Item.Cells(16).Text = FormatNumber((objInvoice.LogisticDN.TotalAmount + PPN), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    e.Item.Cells(17).Text = FormatNumber(objInvoice.SalesOrder.Amount + objInvoice.LogisticDN.TotalAmount + PPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Else
                    e.Item.Cells(16).Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    e.Item.Cells(17).Text = FormatNumber(objInvoice.SalesOrder.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                End If

                Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                If Not IsNothing(_lbtnDelete) Then
                    _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                End If

                'Dim linkButton As linkButton = e.Item.FindControl("lbtnFileName")
                'Dim _Label As Label = e.Item.FindControl("lblString")
                'If objInvoice.InvoiceNumber.ToString() <> String.Empty Then
                '    _Label.Text = "Invoice_" & objInvoice.InvoiceNumber.ToString() & ".pdf"                 
                'Else
                '    linkButton.Visible = False
                'End If
                'linkButton.Text = "<img src=""../images/download.gif"" border=""0"" alt=" & _Label.Text & ">"
                Dim lblInvoiceKind As Label = e.Item.FindControl("lblInvoiceKind")
                lblInvoiceKind.Text = CType(objInvoice.InvoiceKind, enumInvoice.InvoiceKind).ToString()
                Dim lbtnLihat As LinkButton = e.Item.FindControl("lbtnLihat")
                Dim _Label As Label = e.Item.FindControl("lblText")
                If objInvoice.InvoiceNumber.ToString() <> String.Empty Then
                    _Label.Text = "Invoice" & objInvoice.InvoiceNumber.ToString() & ".pdf"
                Else
                    lbtnLihat.Visible = False
                End If
                lbtnLihat.Text = "<img src=""../images/detail.gif"" border=""0"" alt=" & _Label.Text & ">"

                'Tambahan SLA
                Dim lbtnLihatDebitMemo As LinkButton = e.Item.FindControl("lbtnLihatDebitMemo")
                Dim _LabelDM As Label = e.Item.FindControl("lblTextDebitMemo")
                If Not IsNothing(objInvoice.LogisticDN) Then
                    _LabelDM.Text = "fudmemo_" & objInvoice.LogisticDN.DebitMemoNo.ToString() & ".pdf"
                End If
                lbtnLihatDebitMemo.Text = "<img src=""../images/detail.gif"" border=""0"" alt=" & _LabelDM.Text & ">"
                'End Tambahan SLA
                lbtnLihatDebitMemo.Visible = False
                If CType(objInvoice.InvoiceKind, enumInvoice.InvoiceKind) = enumInvoice.InvoiceKind.VH Then
                    lbtnLihatDebitMemo.Visible = True
                End If

                e.Item.Cells(5).Text = objInvoice.InvoiceNumber.ToString 'InvoiceNumber
                e.Item.Cells(7).Text = "" 'Nomor Debit Memo
                e.Item.Cells(8).Text = "" 'Nomor Debit Charge
                e.Item.Cells(12).Text = Format(objInvoice.InvoiceDate, "dd/MM/yyyy") ' InvoiceDate
                e.Item.Cells(16).Text = "" '"Total Biaya Kirim Incl PPN (Rp)
                lbtnLihatDebitMemo.Visible = False

                Select Case CType(objInvoice.InvoiceKind, enumInvoice.InvoiceKind)
                    Case enumInvoice.InvoiceKind.VH

                    Case enumInvoice.InvoiceKind.DP
                    Case enumInvoice.InvoiceKind.AC
                    Case enumInvoice.InvoiceKind.LC
                        lbtnLihat.Visible = False
                        e.Item.Cells(17).Text = "" '"Total Biaya Kirim Incl PPN (Rp)
                        e.Item.Cells(5).Text = String.Empty
                        If Not IsNothing(objInvoice.LogisticDN) Then
                            e.Item.Cells(7).Text = objInvoice.LogisticDN.DebitMemoNo
                            'Dim PPN As Decimal = objInvoice.LogisticDN.TotalAmount * 0.1
                            Dim PPN As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnFromDbPPNMaster, dpp:=objInvoice.LogisticDN.TotalAmount)
                            e.Item.Cells(16).Text = FormatNumber((objInvoice.LogisticDN.TotalAmount + PPN), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                        End If
                        lbtnLihatDebitMemo.Visible = True
                        If Not IsNothing(objInvoice.SalesOrder) AndAlso Not IsNothing(objInvoice.SalesOrder.LogisticDCHeader) Then
                            e.Item.Cells(8).Text = objInvoice.SalesOrder.LogisticDCHeader.DebitChargeNo
                        End If
                        '   e.Item.Cells(12).Text = ""
                        e.Item.Cells(15).Text = ""
                End Select

                Dim UbahlinkButton As LinkButton = e.Item.FindControl("lbtnUbah")
                'If ObjPOHeader.Status <> enumStatusPO.Status.Baru Then
                '    UbahlinkButton.Visible = False
                'End If
                Dim lblHistoryStatus As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
                lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistoryFU.aspx?DocType=" & LookUp.DocumentType.PO_Harian & "&DocNumber=" & objInvoice.ID, "", 400, 400, "DealerSelection")

                Dim lblFlow As Label = CType(e.Item.FindControl("lblFlow"), Label)
                lblFlow.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDocumentFlow.aspx?flow=Invoice_" & objInvoice.ID, "", 500, 450, "ViewDailyPKFlow")
            End If
        End If
    End Sub

    Private Sub SetSessionCriteria()
        Dim objSSDI As ArrayList = New ArrayList
        objSSDI.Add(txtKodeDealer.Text)
        objSSDI.Add(icListDI1.Value)
        objSSDI.Add(icListDI2.Value)
        objSSDI.Add(ddlSalesOrg.SelectedIndex)
        objSSDI.Add(txtNoInvoice.Text)
        objSSDI.Add(txtDealerPO.Text)
        objSSDI.Add(txtNoSO.Text)
        objSSDI.Add(txtNoOC.Text)
        objSSDI.Add(dtgDaftarInv.CurrentPageIndex)
        objSSDI.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSDI.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessionHelper.SetSession("SESSIONDI", objSSDI)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objSSDI As ArrayList = sessionHelper.GetSession("SESSIONDI")
        If Not objSSDI Is Nothing Then
            txtKodeDealer.Text = objSSDI.Item(0)
            'Dim str() As String = objSSDI.Item(1).ToString().Split(",")
            icListDI1.Value = objSSDI.Item(1)
            icListDI2.Value = objSSDI.Item(2)
            ddlSalesOrg.SelectedIndex = objSSDI.Item(3)
            txtNoInvoice.Text = objSSDI.Item(4)
            txtDealerPO.Text = objSSDI.Item(5)
            txtNoSO.Text = objSSDI.Item(6)
            txtNoOC.Text = objSSDI.Item(7)
            dtgDaftarInv.CurrentPageIndex = objSSDI.Item(8)
            ViewState("CurrentSortColumn") = objSSDI.Item(9)
            ViewState("CurrentSortDirect") = objSSDI.Item(10)
            Return True
        End If
        Return False
    End Function

    Sub dtgDaftarInv_edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        Select Case (e.CommandName)
            Case "View"
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../PO/FrmInvoiceDetails.aspx?id=" & e.Item.Cells(0).Text)
                'Case "Edit"
                '    sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                '    SetSessionCriteria()
                '    Response.Redirect("../PO/EditPO.aspx?id=" & e.Item.Cells(0).Text & "&count=0" & "&src=list")
            Case "Download"
                Dim _Label As Label = e.Item.FindControl("lblText")
                Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("InvoiceFileDirectory").ToString & "\" & _Label.Text)

                'Dim fileInfo1 As New fileInfo(Server.MapPath(""))
                Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN1"))

                Dim destFilePath As String = fileInfo1.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("InvoiceFileDirectory").ToString & "\" & _Label.Text

                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False

                Try
                    success = imp.Start()
                    If success Then
                        If (fileInfo.Exists) Then
                            'Dim destinationFileInfo As New FileInfo(destFilePath)
                            'If Not destinationFileInfo.Directory.Exists Then
                            '    destinationFileInfo.Directory.Create()
                            'End If
                            'fileInfo.CopyTo(destFilePath, True)
                            imp.StopImpersonate()
                            imp = Nothing
                        Else
                            MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                        End If
                    End If
                    Response.Redirect("../Download.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("InvoiceFileDirectory").ToString & "\" & _Label.Text)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(_Label.Text))
                End Try
            Case "DownloadDebitMemo"
                Dim _Label As Label = e.Item.FindControl("lblTextDebitMemo")
                Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("LogisticFileDirectory").ToString & "\" & _Label.Text)

                Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN1"))

                Dim destFilePath As String = fileInfo1.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("LogisticFileDirectory").ToString & "\" & _Label.Text

                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False

                Try
                    success = imp.Start()
                    If success Then
                        If (fileInfo.Exists) Then
                            imp.StopImpersonate()
                            imp = Nothing
                        Else
                            MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                            Exit Sub
                        End If
                    End If
                    Response.Redirect("../Download.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("LogisticFileDirectory").ToString & "\" & _Label.Text)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(_Label.Text))
                End Try
        End Select
    End Sub


    Private Sub DoDownload()
        Dim arlData As New ArrayList
        Me.sessionHelper.SetSession(Me._sessDataToDownload, arlData)
        Me.BindToDataGrid(0, True)
        arlData = Me.sessionHelper.GetSession(Me._sessDataToDownload)
        If arlData.Count < 1 Then Exit Sub

        Dim sFileName As String
        Dim fullFileName As String
        sFileName = "Daftar Invoice " & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        fullFileName = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(fullFileName)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(fullFileName, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteData(sw, arlData)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteData(ByVal sw As StreamWriter, ByVal arlData As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim DiscountAmount As Decimal, TotalCost As Decimal, Amount As Decimal
        Dim oSO As SalesOrder
        Dim oInv As Invoice
        Dim i As Integer = 1, row As Integer

        If Not IsNothing(arlData) Then
            'Header1
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Dealer" & tab)
            itemLine.Append("Credit Account" & tab)
            itemLine.Append("Nomor Invoice" & tab)
            itemLine.Append("Jenis Invoice" & tab)
            itemLine.Append("Nomor Debit Memo" & tab)
            itemLine.Append("Nomor Debit Charge" & tab)
            itemLine.Append("Nomor SO" & tab)
            itemLine.Append("No Reg PO" & tab)
            itemLine.Append("Nomor PO" & tab)
            itemLine.Append("Tanggal Invoice" & tab)
            itemLine.Append("Nama Pesanan Khusus" & tab)
            itemLine.Append("Kategori" & tab)
            itemLine.Append("Harga SO (Rp)" & tab)
            itemLine.Append("Total Biaya Kirim Incl PPN (Rp)" & tab)
            itemLine.Append("Harga Tebus (Rp)" & tab)
            itemLine.Append("No. DO" & tab)
            itemLine.Append("No. Chassis" & tab)
            itemLine.Append("Material Number" & tab)
            itemLine.Append("Tahun Perakitan" & tab)
            itemLine.Append("Deskripsi" & tab)
            sw.WriteLine(itemLine.ToString())

            i = 1
            row = 1
            For i = 0 To arlData.Count - 1
                oInv = CType(arlData(i), Invoice)
                oSO = oInv.SalesOrder
                 

                For Each oDO As DeliveryOrder In oSO.DeliveryOrders
                    With oSO
                        itemLine.Remove(0, itemLine.Length)

                        itemLine.Append((row).ToString & tab) 'No
                        itemLine.Append(.POHeader.Dealer.DealerCode & tab)    'Dealer
                        itemLine.Append(.POHeader.Dealer.CreditAccount & tab)    'Credit Account

                        Select Case CType(oInv.InvoiceKind, enumInvoice.InvoiceKind)    'No. Invoice
                            'Case enumInvoice.InvoiceKind.VH
                            'Case enumInvoice.InvoiceKind.DP
                            'Case enumInvoice.InvoiceKind.AC
                            Case enumInvoice.InvoiceKind.LC
                                itemLine.Append(String.Empty & tab)
                            Case Else
                                itemLine.Append(oInv.InvoiceNumber & tab)
                        End Select
                        itemLine.Append(CType(oInv.InvoiceKind, enumInvoice.InvoiceKind).ToString() & tab) ' InvoiceKind


                        Select Case CType(oInv.InvoiceKind, enumInvoice.InvoiceKind)    'DebitMemoNo
                            'Case enumInvoice.InvoiceKind.VH
                            'Case enumInvoice.InvoiceKind.DP
                            'Case enumInvoice.InvoiceKind.AC
                            Case enumInvoice.InvoiceKind.LC
                                If Not IsNothing(oInv.LogisticDN) Then
                                    itemLine.Append(oInv.LogisticDN.DebitMemoNo & tab)
                                Else
                                    itemLine.Append("" & tab)
                                End If
                            Case Else
                                itemLine.Append(String.Empty & tab)
                        End Select

                        Select Case CType(oInv.InvoiceKind, enumInvoice.InvoiceKind)     'No. Debit Charge
                            'Case enumInvoice.InvoiceKind.VH
                            'Case enumInvoice.InvoiceKind.DP
                            'Case enumInvoice.InvoiceKind.AC
                            Case enumInvoice.InvoiceKind.LC

                                If Not IsNothing(oInv.SalesOrder.LogisticDCHeader) Then
                                    itemLine.Append(oInv.SalesOrder.LogisticDCHeader.DebitChargeNo & tab)
                                Else
                                    itemLine.Append("" & tab)
                                End If
                            Case Else
                                itemLine.Append(String.Empty & tab)
                        End Select


                      

                        itemLine.Append(.SONumber & tab)   'No.SO
                        itemLine.Append(.POHeader.PONumber & tab)   'No.RegPO
                        itemLine.Append(.POHeader.DealerPONumber & tab)   'No.PO

                        Select Case CType(oInv.InvoiceKind, enumInvoice.InvoiceKind)     'Tgl Permintaan Krim
                            'Case enumInvoice.InvoiceKind.VH
                            'Case enumInvoice.InvoiceKind.DP
                            'Case enumInvoice.InvoiceKind.AC
                            Case enumInvoice.InvoiceKind.LC
                                'itemLine.Append("" & tab)
                                itemLine.Append(oInv.InvoiceDate.ToString("yyyy/MM/dd") & tab)
                            Case Else
                                itemLine.Append(oInv.InvoiceDate.ToString("yyyy/MM/dd") & tab)
                        End Select


                        itemLine.Append(.POHeader.ContractHeader.ProjectName & tab)   'Nama PK
                        itemLine.Append(.POHeader.ContractHeader.Category.CategoryCode & tab)   'Kategori

                        Select Case CType(oInv.InvoiceKind, enumInvoice.InvoiceKind)    'Harga SO
                            'Case enumInvoice.InvoiceKind.VH
                            'Case enumInvoice.InvoiceKind.DP
                            'Case enumInvoice.InvoiceKind.AC
                            Case enumInvoice.InvoiceKind.LC
                                itemLine.Append("" & tab)
                            Case Else
                                itemLine.Append(GetFormatNumber(oInv.Amount) & tab)
                        End Select


                        Select Case CType(oInv.InvoiceKind, enumInvoice.InvoiceKind)   'Total Biaya Kirim Incl PPN
                            'Case enumInvoice.InvoiceKind.VH
                            'Case enumInvoice.InvoiceKind.DP
                            'Case enumInvoice.InvoiceKind.AC
                            Case enumInvoice.InvoiceKind.LC
                                If Not IsNothing(oInv.LogisticDN) Then
                                    'Dim PPN As Decimal = oInv.LogisticDN.TotalAmount * 0.1
                                    Dim ppnFromMasterPPN = CalcHelper.GetPPNMasterByTaxTypeId(oInv.InvoiceDate.Date, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                                    Dim PPN As Decimal = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnFromMasterPPN, dpp:=oInv.LogisticDN.TotalAmount)
                                    itemLine.Append(GetFormatNumber(oInv.LogisticDN.TotalAmount + PPN) & tab)    'Total Biaya Kirim Incl PPN
                                Else
                                    itemLine.Append("" & tab)
                                End If
                            Case Else
                                itemLine.Append("" & tab)
                        End Select


                        Select Case CType(oInv.InvoiceKind, enumInvoice.InvoiceKind)    'HargaTebus
                            'Case enumInvoice.InvoiceKind.VH
                            'Case enumInvoice.InvoiceKind.DP
                            'Case enumInvoice.InvoiceKind.AC
                            Case enumInvoice.InvoiceKind.LC
                                itemLine.Append("" & tab)
                            Case Else
                                itemLine.Append(GetFormatNumber(oInv.Amount) & tab)
                        End Select
                         

                        itemLine.Append(oDO.DONumber & tab)    'DONumber
                        itemLine.Append(oDO.ChassisMaster.ChassisNumber & tab)   'ChassisNumber
                        itemLine.Append(oDO.ChassisMaster.VechileColor.MaterialNumber & tab)    'Tipe/Warna
                        itemLine.Append(oDO.ChassisMaster.ProductionYear.ToString() & tab)    'Production Year
                        itemLine.Append(oDO.ChassisMaster.VechileColor.MaterialDescription & tab)   'Deskripsi
                        sw.WriteLine(itemLine.ToString())
                    End With
                    row += 1
                Next
            Next
        End If
    End Sub
    Private Function GetFormatNumber(ByRef Amount As Decimal) As String
        Return FormatNumber(Amount, 0, TriState.False, TriState.UseDefault, TriState.False)
    End Function



    Private Sub PopulateDeleteConfirm()
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objInvoiceFacade As New InvoiceFacade(User)
        Dim Succes As Boolean = False
        Dim Deleted As Boolean = False

        For Each oDataGridItem In Me.dtgDaftarInv.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then

                Dim lblID As Label = CType(oDataGridItem.FindControl("lblID"), Label)
                Dim varID As Integer = CInt(lblID.Text)
                Dim _Invoice As New KTB.DNet.Domain.Invoice

                _Invoice.ID = varID
                _Invoice = objInvoiceFacade.Retrieve(_Invoice.ID)



                If 1 = 1 Then
                    Deleted = True
                    Try
                        _Invoice.RowStatus = DBRowStatus.Deleted

                        objInvoiceFacade.Update(_Invoice)
                        Succes = True
                    Catch ex As Exception
                        Succes = False
                    End Try

                Else
                    Succes = False
                    btnCari_Click(Me, Nothing)
                    Return
                End If




            End If
        Next


        If Succes Then
            btnCari_Click(Me, Nothing)
            MessageBox.Show("Invoice sudah berhasil dihapus")

        End If

        If Deleted = False Then
            MessageBox.Show("Tidak ada Invoice yang dihapus")

        End If
    End Sub



    Private Sub CheckPrivilege()
        Dim sesHelper As New SessionHelper
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        Dim idc As Integer = dtgDaftarInv.Columns.Count() - 1
        If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then


            dtgDaftarInv.Columns(idc).Visible = True
        Else

            dtgDaftarInv.Columns(idc).Visible = False

        End If
    End Sub


#End Region

#Region "EventHandler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 300
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarInvoice_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar PO")
        End If

        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        lblTotalTebus.Visible = isPriceVisible
        Label3.Visible = isPriceVisible
        lblJmlHargaTebus.Visible = isPriceVisible
        'Label4.Visible = isPriceVisible
        ' dtgDaftarInv.Columns(12).Visible = isPriceVisible

        If Not IsPostBack Then
            CheckPrivilege()
            btnDelete.Attributes.Add("Onclick", "return confirm('Anda yakin melakukan Hapus ??');")
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
            BindddlInvoiceKind()
            BindToddlCategory()
            InitiatePage()
            SetControls()
            If GetSessionCriteria() Then
                dtgDaftarInv.CurrentPageIndex = 0
                BindGrid()
            End If
        End If
        'lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"

    End Sub

    Private Sub SetControls()
        Dim IsDSF As Boolean = (CType(Session.Item("DEALER"), Dealer).Title = CType(EnumDealerTittle.DealerTittle.LEASING, String)) ' (CType(Session.Item("DEALER"), Dealer).DealerCode.Trim.ToUpper = "DSF")
        'lblOrdertype	lblOrderTypeColon	ddlSalesOrg
        'lblDealerPO	lblDealerPOColon	txtDealerPO
        'lblNoMO		lblNoMOColon		txtNoOC
        'If Me.dtgDaftarInv.Columns(1).HeaderText = "" Then
        '    Me.dtgDaftarInv. .Columns(1).
        'End If

        lblOrdertype.Visible = Not IsDSF
        lblOrderTypeColon.Visible = Not IsDSF
        ddlSalesOrg.Visible = Not IsDSF
        lblDealerPO.Visible = Not IsDSF
        lblDealerPOColon.Visible = Not IsDSF
        txtDealerPO.Visible = Not IsDSF
        lblNoMO.Visible = Not IsDSF
        lblNoMOColon.Visible = Not IsDSF
        txtNoOC.Visible = Not IsDSF
        lblTotalTebus.Text = IIf(IsDSF, "Total Nilai Piutang", "Total Harga Tebus")
        Me.lblDealer.Text = IIf(IsDSF, "Credit Account", "Kode Dealer")
        lblSearchDealer.Attributes("onclick") = IIf(IsDSF, "ShowPPAccountSelection();", "ShowPPDealerSelection();")
        Me.dtgDaftarInv.Columns(4).Visible = IsDSF 'Credit Account
        Me.dtgDaftarInv.Columns(10).Visible = Not IsDSF 'No Reg PO
        Me.dtgDaftarInv.Columns(11).Visible = Not IsDSF 'Nomor PO
        Me.dtgDaftarInv.Columns(13).Visible = Not IsDSF 'Nama Pesanan Khusus
        Me.dtgDaftarInv.Columns(14).Visible = Not IsDSF 'Kategori
        Me.dtgDaftarInv.Columns(17).HeaderText = IIf(IsDSF, "Nilai Piutang (Rp)", "Harga Tebus (Rp)") 'Harga Tebus (Rp)

    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgDaftarInv.CurrentPageIndex = 0
        BindGrid()
    End Sub

    Private Sub TotalHargaTebus()
        If Not IsNothing(Me.sessionHelper.GetSession("DaftarInvoiceAll")) Then
            arlTotalDaftarInvoice = Me.sessionHelper.GetSession("DaftarInvoiceAll")
            Dim tot As Double = 0
            Dim totQty As Integer = 0
            For Each item As Invoice In arlTotalDaftarInvoice
                tot += item.Amount
                Dim ppnFromDbPPNMaster = CalcHelper.GetPPNMasterByTaxTypeId(item.InvoiceDate.Date, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                If Not IsNothing(item.LogisticDN) Then
                    'tot += item.LogisticDN.TotalAmount + (item.LogisticDN.TotalAmount * 0.1)
                    tot += item.LogisticDN.TotalAmount + CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnFromDbPPNMaster, dpp:=item.LogisticDN.TotalAmount)
                End If

                'totQty += item.TotalQuantity
            Next
            lblJmlHargaTebus.Text = "Rp " & FormatNumber(tot, 0, , , TriState.UseDefault)
            'lblQuantity.Text = FormatNumber(totQty, 0, , , TriState.UseDefault) & " Unit"
        End If
        'If Not IsNothing(arlTotalDaftarInvoice) Then
        '    Dim tot As Double = 0
        '    Dim totQty As Integer = 0
        '    For Each item As Invoice In arlTotalDaftarInvoice
        '        tot += item.Amount
        '        'totQty += item.TotalQuantity
        '    Next
        '    lblJmlHargaTebus.Text = "Rp " & FormatNumber(tot, 0, , , TriState.UseDefault)
        '    'lblQuantity.Text = FormatNumber(totQty, 0, , , TriState.UseDefault) & " Unit"
        'End If
    End Sub

    Private Sub TotalQuantity()
        'If Not IsNothing(arlTotalDaftarInvoice) Then
        '    Dim totQty As Integer = 0
        '    For Each item As Invoice In arlTotalDaftarInvoice
        '        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.InvoiceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.InvoiceDetail), "InvoiceHeader.ID", MatchType.Exact, item.ID))
        '        Dim arlInvoiceDetail = New InvoiceDetailFacade(User).Retrieve(criterias)
        '        For Each row As InvoiceDetail In arlInvoiceDetail
        '            totQty += row.BilledQty
        '        Next
        '    Next
        '    lblQuantity.Text = FormatNumber(totQty, 0, , , TriState.UseDefault) & " Unit"
        'End If
    End Sub

#End Region


    Private Sub dtgDaftarInv_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDaftarInv.SortCommand
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

        dtgDaftarInv.SelectedIndex = -1
        dtgDaftarInv.CurrentPageIndex = 0
        BindGrid()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        DoDownload()
    End Sub

    Private Sub ddlProductCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlProductCategory.SelectedIndexChanged
        Me.BindToddlCategory()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        PopulateDeleteConfirm()
    End Sub
End Class
