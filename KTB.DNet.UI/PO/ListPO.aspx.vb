#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Configuration
Imports System.Text
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region

Public Class ListPO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblAllocation As System.Web.UI.WebControls.Label
    Protected WithEvents dtgListPO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents icListPO1 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icListPO2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSalesOrg As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSalesOrg As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblOrdertype As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNoRegPO As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerPO As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoSO As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerPO As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblJmlHargaTebus As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblQuantity As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlFactoring As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNoRegPO As System.Web.UI.WebControls.Label
    Protected WithEvents lblFactoring As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrdertypeColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRegPOColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblFactoringColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblTEOP As System.Web.UI.WebControls.Label
    Protected WithEvents lblTEOPColon As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHargaTebus As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNoDebitCharge As System.Web.UI.WebControls.TextBox

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
    Dim arlTotalListPO As ArrayList
    Dim arlListPO = New ArrayList
    Dim ObjPOHeader = New POHeader
    Dim objSalesOrder As New SalesOrder
    Private objDealer As Dealer
    Private sessionHelper As New sessionHelper
    Private _sessDataToDownload As String = "ListPO._sessDataToDownload"
#End Region

#Region "Custom Method"


    Sub dtgListPO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgListPO.PageIndexChanged
        dtgListPO.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Private Sub BindGrid()
        BindToDataGrid(dtgListPO.CurrentPageIndex)
        dtgListPO.DataBind()
        TotalHargaTebus()
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


    Private Function GetProductCategoryID() As Short
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
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

    Private Sub BindFactoring()
        Me.ddlFactoring.Items.Clear()
        Me.ddlFactoring.Items.Add(New ListItem("Silahkan Pilih", 2))
        Me.ddlFactoring.Items.Add(New ListItem("Factoring", 1))
        Me.ddlFactoring.Items.Add(New ListItem("Non Factoring", 0))
    End Sub

    Private Sub SetControlFactoring()
        Dim IsDSF As Boolean = (CType(Session.Item("DEALER"), Dealer).Title = CType(EnumDealerTittle.DealerTittle.LEASING, String)) '(CType(Session.Item("DEALER"), Dealer).DealerCode.Trim.ToUpper = "DSF")

        lblOrdertype.Visible = Not IsDSF
        lblOrdertypeColon.Visible = Not IsDSF
        Me.ddlOrderType.Visible = Not IsDSF

        lblNoRegPO.Visible = Not IsDSF
        lblNoRegPOColon.Visible = Not IsDSF
        Me.txtNoRegPO.Visible = Not IsDSF

        lblFactoring.Visible = Not IsDSF
        lblFactoringColon.Visible = Not IsDSF
        ddlFactoring.Visible = Not IsDSF

        lblTEOP.Visible = Not IsDSF
        lblTEOPColon.Visible = Not IsDSF
        ddlTermOfPayment.Visible = Not IsDSF

        If IsDSF Then
            ddlFactoring.SelectedValue = 1 ' Factoring
            ddlFactoring.Enabled = False
        Else
            ddlFactoring.SelectedValue = 2 'Silahkan Pilih
            ddlFactoring.Enabled = True
        End If
        lblTotalHargaTebus.Text = IIf(IsDSF, "Total Nilai Piutang", "Total Harga Tebus")
        'Me.dtgListPO.Columns(4).Visible = IsDSF 'Credit Account
        'Me.dtgListPO.Columns(6).Visible = Not IsDSF 'No Reg PO
        'Me.dtgListPO.Columns(9).Visible = Not IsDSF 'Nama Pesanan Khusus
        'Me.dtgListPO.Columns(13).Visible = Not IsDSF 'Jenis Order
        'Me.dtgListPO.Columns(16).Visible = Not IsDSF 'Factoring
        'Me.dtgListPO.Columns(17).Visible = Not IsDSF 'Pembayaran Tunai

        Me.dtgListPO.Columns(4).Visible = IsDSF 'Credit Account
        Me.dtgListPO.Columns(7).Visible = Not IsDSF 'No Reg PO
        Me.dtgListPO.Columns(10).Visible = Not IsDSF 'Nama Pesanan Khusus
        Me.dtgListPO.Columns(14).Visible = Not IsDSF 'Jenis Order
        Me.dtgListPO.Columns(19).Visible = Not IsDSF 'Factoring
        Me.dtgListPO.Columns(20).Visible = Not IsDSF 'Pembayaran Tunai

        If IsDSF Then
            lblDealer.Text = "Credit Account"
            lblSearchDealer.Attributes("onclick") = "ShowPPAccountSelection();"
        Else
            lblDealer.Text = "Kode Dealer"
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        End If
        'If ddlFactoring.Visible Then
        '    Dim IsImplementFactoring As Boolean = IIf(KTB.DNet.Lib.WebConfig.GetValue("ImpelementFactoring") = 1, True, False)
        '    If IsImplementFactoring Then
        '        Dim oTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(CType(Session("DEALER"), Dealer).ID, EnumDealerTransType.DealerTransKind.Factoring)

        '        If Not (Not IsNothing(oTC) AndAlso oTC.Status = 1) Then
        '            IsImplementFactoring = False
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub BindToDropdownList()
        'Try
        'lboxStatus.Items.Clear()
        'For Each itemA As ListItem In LookUp.ArrayStatusPO
        'itemA.Selected = False
        'lboxStatus.Items.Add(itemA)
        'Next
        'Catch ex As Exception
        'MessageBox.Show("Error Binding lboxStatus, silahkan kirim error ini ke dnet admin")
        'End Try

        Try
            ddlOrderType.Items.Clear()
            If SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOJenisAll_Privilege) Then
                Dim listitemBlank As ListItem = New ListItem("Silahkan Pilih", -1)
                listitemBlank.Selected = False
                ddlOrderType.Items.Add(listitemBlank)
            End If
            For Each itemq As ListItem In LookUp.ArrayJenisPO
                itemq.Selected = False
                If itemq.Text = "Harian" Then
                    If SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOJenisAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Harian) Then
                        ddlOrderType.Items.Add(itemq)
                    End If
                ElseIf itemq.Text = "Tambahan" Then
                    If SecurityProvider.Authorize(Context.User, SR.DaftarStatusPOJenisAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PengajuanPOKind_Tambahan) Then
                        ddlOrderType.Items.Add(itemq)
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlOrderType, silahkan kirim error ini ke dnet admin")
        End Try
    End Sub

    'Private Sub BindToDataGrid(ByVal currentPageIndex As Integer)
    '    Dim total As Integer = 0
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    objDealer = Session("DEALER")
    '    If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
    '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
    '    End If
    '    If txtKodeDealer.Text <> String.Empty Then
    '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
    '    End If
    '    'If lboxStatus.SelectedIndex <> -1 Then
    '    'Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
    '    'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "Status", MatchType.InSet, "(" & SelectedStatus & ")"))
    '    'End If
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "Status", MatchType.Exact, "8"))
    '    If ddlSalesOrg.SelectedIndex <> 0 Then
    '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.Category.CategoryCode", MatchType.Exact, ddlSalesOrg.SelectedItem))
    '    End If
    '    If ddlOrderType.SelectedIndex <> 0 Then
    '        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.ContractType", MatchType.Exact, ddlOrderType.SelectedValue))
    '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "POType", MatchType.Exact, ddlOrderType.SelectedValue))
    '    End If

    '    '--Get Criterias From TextBox
    '    If txtDealerPO.Text <> "" Then
    '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "DealerPONumber", MatchType.Exact, txtDealerPO.Text))
    '    End If

    '    If txtNoRegPO.Text <> "" Then
    '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "PONumber", MatchType.Exact, txtNoRegPO.Text))
    '    End If

    '    'If txtNoMO.Text <> "" Then
    '    'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.ContractNumber", MatchType.Exact, txtNoMO.Text))
    '    'End If

    '    'Add by Andra AR - 26/11/2008
    '    If txtNoSO.Text <> "" Then
    '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "SONumber", MatchType.Exact, txtNoSO.Text))
    '    Else
    '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "SONumber", MatchType.No, ""))
    '    End If

    '    If ddlTermOfPayment.SelectedIndex <> 0 Then
    '        If ddlTermOfPayment.SelectedIndex = 1 Then
    '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "TermOfPayment.TermOfPaymentValue", MatchType.No, 0))
    '        Else
    '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "TermOfPayment.TermOfPaymentValue", MatchType.Exact, 0))
    '        End If
    '    End If
    '    'End of Add by Andra AR - 26/11/2008

    '    '--Get Criterias From Calendar
    '    If CType(icListPO1.Value, Date) <= CType(icListPO2.Value, Date) Then
    '        Dim TanggalAwal As New DateTime(CInt(icListPO1.Value.Year), CInt(icListPO1.Value.Month), CInt(icListPO1.Value.Day), 0, 0, 0)
    '        Dim TanggalAkhir As New DateTime(CInt(icListPO2.Value.Year), CInt(icListPO2.Value.Month), CInt(icListPO2.Value.Day), 23, 59, 59)
    '        Dim Time As TimeSpan = TanggalAkhir.Subtract(TanggalAwal)
    '        If Time.Days <= 65 Then
    '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
    '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, Format(TanggalAkhir, "yyyy-MM-dd HH:mm:ss")))


    '            arlListPO = New POHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgListPO.PageSize, _
    '                    total, CType(ViewState("CurrentSortColumn"), String), _
    '                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
    '            dtgListPO.DataSource = arlListPO

    '            arlTotalListPO = New POHeaderFacade(User).Retrieve(criterias)
    '            dtgListPO.VirtualItemCount = total

    '        Else
    '            MessageBox.Show("Periode Melebihi 65 Hari")
    '        End If
    '    Else
    '        MessageBox.Show(SR.InvalidRangeDate)
    '    End If

    'End Sub

    Private Sub BindToDataGrid(ByVal currentPageIndex As Integer, Optional ByVal IsForDownload As Boolean = False)
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")

        Dim objSalesOrder As SalesOrder

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.ContractHeader.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        If txtKodeDealer.Text <> String.Empty Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
            Else
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.ContractHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
            End If
        End If
        'If lboxStatus.SelectedIndex <> -1 Then
        'Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "Status", MatchType.InSet, "(" & SelectedStatus & ")"))
        'End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.Status", MatchType.Exact, "8"))
        If ddlSalesOrg.Items.Count > 0 AndAlso ddlSalesOrg.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.ContractHeader.Category.CategoryCode", MatchType.Exact, ddlSalesOrg.SelectedItem))
        End If
        If ddlOrderType.Items.Count > 0 AndAlso ddlOrderType.SelectedIndex <> 0 Then
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.ContractType", MatchType.Exact, ddlOrderType.SelectedValue))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.POType", MatchType.Exact, ddlOrderType.SelectedValue))
        End If

        '--Get Criterias From TextBox
        If txtDealerPO.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.DealerPONumber", MatchType.Exact, txtDealerPO.Text))
        End If

        If txtNoDebitCharge.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "LogisticDCHeader.DebitChargeNo", MatchType.Exact, txtNoDebitCharge.Text))
        End If

        If txtNoRegPO.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.PONumber", MatchType.Exact, txtNoRegPO.Text))
        End If

        'If txtNoMO.Text <> "" Then
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.ContractNumber", MatchType.Exact, txtNoMO.Text))
        'End If

        'Add by Andra AR - 26/11/2008
        If txtNoSO.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "SONumber", MatchType.Exact, txtNoSO.Text))
        Else
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.SONumber", MatchType.No, ""))
        End If

        'Start  :CR by Yurike:AddRTGS:dna:20091211
        'If ddlTermOfPayment.SelectedIndex <> 0 Then
        '    If ddlTermOfPayment.SelectedIndex = 1 Then
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.TermOfPayment.TermOfPaymentValue", MatchType.No, 0))
        '    Else
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.TermOfPayment.TermOfPaymentValue", MatchType.Exact, 0))
        '    End If
        'End If
        If ddlTermOfPayment.SelectedIndex <> 0 Then
            If ddlTermOfPayment.SelectedIndex = 1 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, CType(enumPaymentType.PaymentType.TOP, Short)))
            ElseIf ddlTermOfPayment.SelectedIndex = 2 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, CType(enumPaymentType.PaymentType.COD, Short)))
            ElseIf ddlTermOfPayment.SelectedIndex = 3 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, CType(enumPaymentType.PaymentType.RTGS, Short)))
            End If
        End If
        'Start  :CR by Yurike:AddRTGS:dna:20091211

        'End of Add by Andra AR - 26/11/2008

        'Start  :Factoring;by:dna;for:yurike;on:20101004
        If CType(ddlFactoring.SelectedValue, Short) <> 2 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.IsFactoring", MatchType.Exact, CType(ddlFactoring.SelectedValue, Short)))
        End If
        'End    :Factoring;by:dna;for:yurike;on:20101004

        Dim PCID As Short = Me.GetProductCategoryID()
        If PCID > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.ContractHeader.Category.ProductCategory.ID", MatchType.Exact, PCID))
        End If
        '--Get Criterias From Calendar
        If CType(icListPO1.Value, Date) <= CType(icListPO2.Value, Date) Then
            Dim TanggalAwal As New DateTime(CInt(icListPO1.Value.Year), CInt(icListPO1.Value.Month), CInt(icListPO1.Value.Day), 0, 0, 0)
            Dim TanggalAkhir As New DateTime(CInt(icListPO2.Value.Year), CInt(icListPO2.Value.Month), CInt(icListPO2.Value.Day), 0, 0, 0)
            Dim Time As TimeSpan = TanggalAkhir.Subtract(TanggalAwal)
            If Time.Days <= 65 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "POHeader.ReqAllocationDateTime", MatchType.Lesser, Format(TanggalAkhir.AddDays(1), "yyyy-MM-dd HH:mm:ss")))

                If IsForDownload Then
                    Dim aSOs As ArrayList
                    Dim oSorts As New SortCollection

                    oSorts.Add(New Sort(GetType(SalesOrder), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
                    aSOs = New SalesOrderFacade(User).Retrieve(criterias, oSorts)
                    Me.sessionHelper.SetSession(Me._sessDataToDownload, aSOs)

                Else
                    arlListPO = New SalesOrderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgListPO.PageSize, _
                            total, CType(ViewState("CurrentSortColumn"), String), _
                            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
                    dtgListPO.DataSource = arlListPO

                    arlTotalListPO = New SalesOrderFacade(User).Retrieve(criterias)
                    dtgListPO.VirtualItemCount = total
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
    Sub dtgListPO_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If Not (arlListPO Is Nothing) Then
            If Not (arlListPO.Count = 0 Or e.Item.ItemIndex = -1) Then

                If e.Item.ItemIndex = 0 Then
                    POViewPrivilege = SecurityProvider.Authorize(Context.User, SR.PengajuanPOView_Detail)
                    POSOViewPrivilege = SecurityProvider.Authorize(Context.User, SR.DaftarPOSOForm_Privilege)
                End If
                objSalesOrder = CType(arlListPO(e.Item.ItemIndex), SalesOrder)
                e.Item.Cells(0).Text = objSalesOrder.ID
                e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgListPO.PageSize * dtgListPO.CurrentPageIndex)).ToString
                e.Item.Cells(2).Text = CType(objSalesOrder.POHeader.Status, enumStatusPO.Status).ToString
                Dim lblDealerCode As Label = e.Item.FindControl("lblDealerCode")
                lblDealerCode.Text = objSalesOrder.POHeader.ContractHeader.Dealer.DealerCode
                If lblDealerCode.Text <> String.Empty Then
                    lblDealerCode.ToolTip = objSalesOrder.POHeader.ContractHeader.Dealer.SearchTerm1
                End If

                'e.Item.Cells(4).Text = objSalesOrder.POHeader.ContractHeader.Dealer.CreditAccount
                'e.Item.Cells(5).Text = objSalesOrder.SONumber
                'e.Item.Cells(6).Text = objSalesOrder.POHeader.PONumber
                'e.Item.Cells(7).Text = objSalesOrder.POHeader.DealerPONumber
                'e.Item.Cells(8).Text = Format(objSalesOrder.POHeader.ReqAllocationDateTime, "dd/MM/yyyy")
                'e.Item.Cells(9).Text = objSalesOrder.POHeader.ContractHeader.ProjectName
                'e.Item.Cells(12).Text = objSalesOrder.POHeader.ContractHeader.Category.CategoryCode
                'e.Item.Cells(13).Text = CType(objSalesOrder.POHeader.POType, LookUp.EnumJenisOrder).ToString()
                'e.Item.Cells(14).Text = objSalesOrder.POHeader.TermOfPayment.Description
                'e.Item.Cells(15).Text = FormatNumber(objSalesOrder.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'e.Item.Cells(16).Text = IIf(objSalesOrder.POHeader.IsFactoring = 1, "Ya", "Tidak")
                'e.Item.Cells(17).Text = FormatNumber(objSalesOrder.CashPayment, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                'e.Item.Cells(18).Text = FormatNumber(objSalesOrder.Amount - objSalesOrder.CashPayment, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(4).Text = objSalesOrder.POHeader.ContractHeader.Dealer.CreditAccount
                e.Item.Cells(5).Text = objSalesOrder.SONumber

                e.Item.Cells(7).Text = objSalesOrder.POHeader.PONumber
                e.Item.Cells(8).Text = objSalesOrder.POHeader.DealerPONumber
                e.Item.Cells(9).Text = Format(objSalesOrder.POHeader.ReqAllocationDateTime, "dd/MM/yyyy")
                e.Item.Cells(10).Text = objSalesOrder.POHeader.ContractHeader.ProjectName
                e.Item.Cells(13).Text = objSalesOrder.POHeader.ContractHeader.Category.CategoryCode
                e.Item.Cells(14).Text = CType(objSalesOrder.POHeader.POType, LookUp.EnumJenisOrder).ToString()
                e.Item.Cells(15).Text = objSalesOrder.POHeader.TermOfPayment.Description

                e.Item.Cells(16).Text = FormatNumber(objSalesOrder.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(19).Text = IIf(objSalesOrder.POHeader.IsFactoring = 1, "Ya", "Tidak")
                e.Item.Cells(20).Text = FormatNumber(objSalesOrder.CashPayment, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(21).Text = FormatNumber(objSalesOrder.Amount - objSalesOrder.CashPayment, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

                If Not IsNothing(objSalesOrder.LogisticDCHeader) Then
                    e.Item.Cells(6).Text = objSalesOrder.LogisticDCHeader.DebitChargeNo
                    e.Item.Cells(17).Text = FormatNumber(objSalesOrder.LogisticDCHeader.TotalLogisticCost, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    e.Item.Cells(18).Text = FormatNumber(objSalesOrder.Amount + objSalesOrder.LogisticDCHeader.TotalLogisticCost, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

                Else
                    e.Item.Cells(18).Text = FormatNumber(objSalesOrder.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                    Dim lblLihatdc As LinkButton = e.Item.FindControl("lbtnLihatdc")
                    lblLihatdc.Visible = False
                End If
                Me.dtgListPO.Columns(21).Visible = False
                e.Item.Cells(25).Visible = POViewPrivilege

                If objSalesOrder.POHeader.Status = enumStatusPO.Status.Rilis Then
                    e.Item.BackColor = System.Drawing.Color.Yellow
                End If

                Dim linkButton As LinkButton = e.Item.FindControl("lbtnFileName")
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                    '-- Confirm deletion
                    CType(e.Item.FindControl("lbtnDelete"), LinkButton).ToolTip = "Hapus"
                    CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Hapus record ini?');")
                End If

                Dim _Label As Label = e.Item.FindControl("lblString")
                If objSalesOrder.SONumber.ToString() <> String.Empty Then
                    _Label.Text = "sofile_" & objSalesOrder.SONumber.ToString() & ".pdf"
                    'linkButton.Visible = POSOViewPrivilege
                    'Else
                    'linkButton.Visible = False
                End If
                linkButton.Text = "<img src=""../images/download.gif"" border=""0"" alt=" & _Label.Text & ">"

                'Tambahan SLA Get filenamePDF file
                Dim _LabelDC As Label = e.Item.FindControl("lblStringDC")
                If Not IsNothing(objSalesOrder.LogisticDCHeader) Then
                    _LabelDC.Text = "fudclog_" & objSalesOrder.LogisticDCHeader.DebitChargeNo.ToString() & ".pdf"
                End If
                'End of SLA

                Dim UbahlinkButton As LinkButton = e.Item.FindControl("lbtnUbah")
                If objSalesOrder.POHeader.Status <> enumStatusPO.Status.Baru Then
                    UbahlinkButton.Visible = False
                End If
                Dim lblHistoryStatus As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
                lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistoryFU.aspx?DocType=" & LookUp.DocumentType.PO_Harian & "&DocNumber=" & objSalesOrder.POHeader.PONumber, "", 400, 400, "DealerSelection")

                Dim lblFlow As Label = CType(e.Item.FindControl("lblFlow"), Label)
                lblFlow.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDocumentFlow.aspx?flow=PO_" & objSalesOrder.SONumber & "_" & objSalesOrder.POHeader.PONumber, "", 500, 450, "ViewDailyPKFlow")
                Dim lblSOInterestID As Label = CType(e.Item.FindControl("lblSOInterestID"), Label)
                lblSOInterestID.Text = New VW_SalesOrderInterestFacade(User).RetrieveBySONumber(objSalesOrder.SONumber).SONumber
            End If
        End If

    End Sub

    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList
        objSSPO.Add(txtKodeDealer.Text)
        'objSSPO.Add(GetSelectedItem(lboxStatus))
        objSSPO.Add(icListPO1.Value)
        objSSPO.Add(icListPO2.Value)

        objSSPO.Add(txtDealerPO.Text)
        objSSPO.Add(ddlSalesOrg.SelectedIndex)
        objSSPO.Add(ddlOrderType.SelectedIndex)
        'objSSPO.Add(txtNoMO.Text)
        objSSPO.Add(dtgListPO.CurrentPageIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessionHelper.SetSession("SESSIONPO", objSSPO)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objSSPO As ArrayList = sessionHelper.GetSession("SESSIONPO")
        If Not objSSPO Is Nothing Then
            txtKodeDealer.Text = objSSPO.Item(0)
            'Dim str() As String = objSSPO.Item(1).ToString().Split(",")
            'For Each item As ListItem In lboxStatus.Items
            '    For i As Integer = 0 To Str.Length - 1
            '        If item.Value.ToString = Str(i).ToString Then
            '            item.Selected = True
            '            Exit For
            '        End If
            '    Next
            'Next
            icListPO1.Value = objSSPO.Item(2)
            icListPO2.Value = objSSPO.Item(3)
            txtDealerPO.Text = objSSPO.Item(4)
            ddlSalesOrg.SelectedIndex = objSSPO.Item(5)
            ddlOrderType.SelectedIndex = objSSPO.Item(6)
            'txtNoMO.Text = objSSPO.Item(7)
            dtgListPO.CurrentPageIndex = objSSPO.Item(8)
            ViewState("CurrentSortColumn") = objSSPO.Item(9)
            ViewState("CurrentSortDirect") = objSSPO.Item(10)
            Return True
        End If
        Return False
    End Function

    Private Function DeleteSOData(ByVal nID As Integer) As Integer
        Dim objSalesOrder As SalesOrder = New SalesOrderFacade(User).Retrieve(nID)
        Dim Facade As SalesOrderFacade = New SalesOrderFacade(User)
        Facade.Delete(objSalesOrder)
        dtgListPO.CurrentPageIndex = 0
        BindGrid()
    End Function

    Sub dtgListPO_edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        Select Case (e.CommandName)
            'Case "View"
            '    sessionHelper.SetSession("PrevPage", Request.Url.ToString())
            '    SetSessionCriteria()
            '    Response.Redirect("../PO/FrmSODetails.aspx?id=" & e.Item.Cells(0).Text)
            'Case "Edit"
            '    sessionHelper.SetSession("PrevPage", Request.Url.ToString())
            '    SetSessionCriteria()
            '    Response.Redirect("../PO/EditPO.aspx?id=" & e.Item.Cells(0).Text & "&count=0" & "&src=list")
            Case "Delete"
                Dim ErrMessage As String = "SO sudah terdapat pada transaksi lain (Invoice No "

                'cek Invoice 
                Dim criteriaInvoice As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Invoice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaInvoice.opAnd(New Criteria(GetType(KTB.DNet.Domain.Invoice), "SalesOrder.ID", MatchType.Exact, e.Item.Cells(0).Text))
                Dim arrInvoice As ArrayList = New InvoiceFacade(User).Retrieve(criteriaInvoice)
                Dim objInvoice As Invoice
                If arrInvoice.Count > 0 Then
                    objInvoice = CType(arrInvoice(0), Invoice)
                    ErrMessage &= objInvoice.InvoiceNumber.ToString
                End If

                'cek DO
                ErrMessage &= " atau DO No "
                Dim criteriaDeliveryOrder As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DeliveryOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaDeliveryOrder.opAnd(New Criteria(GetType(KTB.DNet.Domain.DeliveryOrder), "SalesOrder.ID", MatchType.Exact, e.Item.Cells(0).Text))
                Dim arrDeliveryOrder As ArrayList = New DeliveryOrderFacade(User).Retrieve(criteriaDeliveryOrder)
                Dim objDeliveryOrder As DeliveryOrder
                If arrDeliveryOrder.Count > 0 Then
                    objDeliveryOrder = CType(arrDeliveryOrder(0), DeliveryOrder)
                    ErrMessage &= objDeliveryOrder.DONumber.ToString
                End If

                'cek Giro
                ErrMessage &= " atau Giro "
                Dim criteriaDailyPayment As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaDailyPayment.opAnd(New Criteria(GetType(KTB.DNet.Domain.DailyPayment), "SalesOrder.ID", MatchType.Exact, e.Item.Cells(0).Text))
                Dim arrDailyPayment As ArrayList = New DailyPaymentFacade(User).Retrieve(criteriaDailyPayment)
                Dim objDailyPayment As DailyPayment
                If arrDailyPayment.Count > 0 Then
                    objDailyPayment = CType(arrDailyPayment(0), DailyPayment)
                    ErrMessage &= objDailyPayment.SlipNumber.ToString
                End If



                'Cek PPH Interest
                Dim so As SalesOrder = New SalesOrderFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                Dim crtIT As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VW_SalesOrderInterest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crtIT.opAnd(New Criteria(GetType(KTB.DNet.Domain.VW_SalesOrderInterest), "SONumber", MatchType.Exact, so.SONumber))

                Dim arrSOIT As ArrayList = New ArrayList
                arrSOIT = New VW_SalesOrderInterestFacade(User).Retrieve(crtIT)

                If arrSOIT.Count > 0 Then
                    For Each soIT As VW_SalesOrderInterest In arrSOIT
                        If soIT.Status = 0 OrElse soIT.Status = 1 OrElse soIT.Status = 3 OrElse soIT.Status = 4 Then
                            ErrMessage &= String.Format("Ada PPH interest atas SO {0} dengan NoReg {1}", soIT.SONumber, soIT.NoReg)
                            Exit For
                        End If
                    Next
                End If
                ErrMessage &= ")"

                If arrInvoice.Count = 0 And arrDeliveryOrder.Count = 0 And arrDailyPayment.Count = 0 Then
                    Try
                        'deleteSOInterest(e.Item.Cells(0).Text)
                        'Dim nResult = DeleteSOData(e.Item.Cells(0).Text)
                        Dim nResult = 0
                        If nResult = 2 Then
                            MessageBox.Show(SR.CannotDelete)
                        ElseIf nResult = -1 Then
                            MessageBox.Show(SR.DeleteFail)
                        Else
                            MessageBox.Show(SR.DeleteSucces)
                        End If
                    Catch ex As Exception
                        MessageBox.Show(SR.DeleteFail)
                    End Try
                Else
                    MessageBox.Show(ErrMessage)
                End If

            Case "View"
                Dim _Label As Label = e.Item.FindControl("lblString")
                Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SOFileDirectory").ToString & "\" & _Label.Text)

                'Dim fileInfo1 As New fileInfo(Server.MapPath(""))
                Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))

                Dim destFilePath As String = fileInfo1.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("SODestFileDirectory").ToString & "\" & _Label.Text
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False

                Try
                    success = imp.Start()
                    If success Then
                        If (fileInfo.Exists) Then
                            Dim destinationFileInfo As New FileInfo(destFilePath)
                            If Not destinationFileInfo.Directory.Exists Then
                                destinationFileInfo.Directory.Create()
                            End If
                            fileInfo.CopyTo(destFilePath, True)
                            imp.StopImpersonate()
                            imp = Nothing
                        Else
                            MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                        End If
                    End If
                    Response.Redirect("../Download.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("SODestFileDirectory").ToString & "\" & _Label.Text)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(_Label.Text))
                End Try
                'Tambahan SLA

            Case "ViewDC"
                Dim criteriaSO As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaSO.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesOrder), "ID", MatchType.Exact, e.Item.Cells(0).Text))
                Dim arrSO As ArrayList = New SalesOrderFacade(User).Retrieve(criteriaSO)
                Dim objSO As SalesOrder
                If arrSO.Count > 0 Then
                    objSO = CType(arrSO(0), SalesOrder)
                    If objSO.POHeader.Status = 5 Then
                        MessageBox.Show("Status SO is blocked")
                        Exit Sub
                    End If
                End If

                'disini download file Debit Charge :

                Dim _Label As Label = e.Item.FindControl("lblStringDC")
                Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("LogisticFileDirectory").ToString & "\" & _Label.Text)

                'Dim fileInfo1 As New fileInfo(Server.MapPath(""))
                Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))

                Dim destFilePath As String = fileInfo1.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("LogisticDestFileDirectory").ToString & "\" & _Label.Text
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False

                Try
                    success = imp.Start()
                    If success Then
                        If (fileInfo.Exists) Then
                            Response.Redirect("../Download.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("LogisticDestFileDirectory").ToString & "\" & _Label.Text)
                            imp.StopImpersonate()
                            imp = Nothing
                        Else
                            MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                        End If
                    End If

                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(_Label.Text))
                End Try

                'End of SLA


        End Select
    End Sub

    Private Sub deleteSOInterest(ByVal SOID As Integer)
        Dim crit As New CriteriaComposite(New Criteria(GetType(SalesOrderInterest), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(SalesOrderInterest), "SalesOrder.ID", MatchType.Exact, SOID))
        Dim objFacade As New SalesOrderInterestFacade(User)
        Dim arrSOInterest As ArrayList = objFacade.Retrieve(crit)
        If arrSOInterest.Count > 0 Then
            Dim objToDel As SalesOrderInterest = CType(arrSOInterest(0), SalesOrderInterest)
            objFacade.Delete(objToDel)
        End If
    End Sub

    Private Sub DoDownload()
        Dim arlData As New ArrayList
        Me.sessionHelper.SetSession(Me._sessDataToDownload, arlData)
        Me.BindToDataGrid(0, True)
        arlData = Me.sessionHelper.GetSession(Me._sessDataToDownload)
        If arlData.Count < 1 Then Exit Sub

        Dim sFileName As String
        Dim fullFileName As String
        sFileName = "Daftar SO" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

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
        Dim i As Integer = 1

        If Not IsNothing(arlData) Then
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Dealer" & tab)
            itemLine.Append("Nomor SO" & tab)
            itemLine.Append("Nomor Debit Charge" & tab)
            itemLine.Append("No Reg PO" & tab)
            itemLine.Append("Nomor PO" & tab)
            itemLine.Append("Tgl P.Kirim" & tab)
            itemLine.Append("Nama PK" & tab)
            itemLine.Append("Kategori" & tab)
            itemLine.Append("Jenis Order" & tab)
            itemLine.Append("Cara Pembayaran" & tab)
            itemLine.Append("Harga SO (Rp)" & tab)
            itemLine.Append("Total Biaya Kirim Incl. PPN (Rp)" & tab)
            itemLine.Append("Harga Tebus (Rp)" & tab)
            itemLine.Append("Factoring" & tab)
            itemLine.Append("Pembayaran Tunai" & tab)


            sw.WriteLine(itemLine.ToString())
            i = 1

            For i = 0 To arlData.Count - 1
                oSO = CType(arlData(i), SalesOrder)
                With oSO
                    itemLine.Remove(0, itemLine.Length)

                    itemLine.Append((i + 1).ToString & tab) 'No
                    itemLine.Append(.POHeader.Dealer.DealerCode & tab)    'Dealer
                    itemLine.Append(.SONumber & tab)   'No.SO
                    If Not IsNothing(.LogisticDCHeader) Then
                        itemLine.Append(.LogisticDCHeader.DebitChargeNo & tab)   'No.Debit Charge
                    Else
                        itemLine.Append("" & tab)   'Total Biaya Kirim Incl PPN
                    End If

                    itemLine.Append(.POHeader.PONumber & tab)   'No.RegPO
                    itemLine.Append(.POHeader.DealerPONumber & tab)   'No.PO
                    itemLine.Append(.POHeader.ReqAllocationDateTime.ToString("yyyy/MM/dd") & tab)   'Tgl Permintaan Krim
                    itemLine.Append(.POHeader.ContractHeader.ProjectName & tab)   'Nama PK
                    itemLine.Append(.POHeader.ContractHeader.Category.CategoryCode & tab)   'Kategori
                    itemLine.Append(CType(.POHeader.POType, LookUp.EnumJenisOrder).ToString() & tab)    'JenisOrder
                    itemLine.Append(.POHeader.TermOfPayment.Description & tab)    'CaraPembayaran
                    itemLine.Append(GetFormatNumber(.Amount) & tab)   'HargaSO
                    If Not IsNothing(.LogisticDCHeader) Then

                        itemLine.Append(GetFormatNumber(.LogisticDCHeader.TotalLogisticCost) & tab)   'Total Biaya Kirim Incl PPN
                        itemLine.Append(GetFormatNumber(.Amount + .LogisticDCHeader.TotalLogisticCost) & tab)   'HargaTebus
                    Else
                        itemLine.Append("" & tab)   'Total Biaya Kirim Incl PPN
                        itemLine.Append(GetFormatNumber(.Amount) & tab)   'HargaTebus
                    End If



                    itemLine.Append(IIf(.POHeader.IsFactoring = 1, "Ya", "Tidak") & tab)   'Factoring
                    itemLine.Append(GetFormatNumber(.CashPayment) & tab)   'PembayaranTunai

                End With
                sw.WriteLine(itemLine.ToString())
            Next
        End If
    End Sub

    Private Function GetFormatNumber(ByRef Amount As Decimal) As String
        Return FormatNumber(Amount, 0, TriState.False, TriState.UseDefault, TriState.False)
    End Function
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 300
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarPOView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar PO")
        End If
        Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            'dtgListPO.Columns(23).Visible = True
            dtgListPO.Columns(27).Visible = True
        Else
            'dtgListPO.Columns(23).Visible = False
            dtgListPO.Columns(27).Visible = False
        End If


        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        Me.lblTotalHargaTebus.Visible = isPriceVisible
        lblJmlHargaTebus.Visible = isPriceVisible
        'dtgListPO.Columns(12).Visible = isPriceVisible
        dtgListPO.Columns(13).Visible = isPriceVisible

        If Not IsPostBack Then
            BindToDropdownList()
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
            BindToddlCategory()
            'Start  :Factoring;by:dna;for:yurike;on:20101004
            BindFactoring()
            SetControlFactoring()

            'End    :Factoring;by:dna;for:yurike;on:20101004            
            InitiatePage()
            If GetSessionCriteria() Then
                'dtgListPO.CurrentPageIndex = 0
                BindGrid()
            End If
        End If
        'lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        Dim IsImplementFactoring As Boolean = IIf(KTB.DNet.Lib.WebConfig.GetValue("ImpelementFactoring") = 1, True, False)
        Me.lblFactoring.Visible = IsImplementFactoring
        Me.lblFactoringColon.Visible = IsImplementFactoring
        Me.ddlFactoring.Visible = IsImplementFactoring
        Dim ColIdx As Integer = CommonFunction.GetColumnIndexOfDTG(Me.dtgListPO, "Factoring")
        If ColIdx >= 0 Then Me.dtgListPO.Columns(ColIdx).Visible = IsImplementFactoring
    End Sub

    Private Sub TotalHargaTebus()
        'Todo Make view PO
        If Not IsNothing(arlTotalListPO) Then
            Dim tot As Double = 0
            Dim totQty As Integer = 0
            For Each item As SalesOrder In arlTotalListPO
                tot += item.Amount '  item.POHeader.TotalHarga + item.POHeader.TotalHargaPP
                totQty += item.POHeader.TotalQuantity

                If Not IsNothing(item.LogisticDCHeader) Then
                    tot += item.LogisticDCHeader.TotalLogisticCost
                End If
            Next
            lblJmlHargaTebus.Text = FormatNumber(tot, 0, , , TriState.UseDefault)
            lblQuantity.Text = FormatNumber(totQty, 0, , , TriState.UseDefault) & " Unit"
        End If
    End Sub
    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "POHeader.PONumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub
    Private Sub dtgListPO_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgListPO.SortCommand
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

        dtgListPO.SelectedIndex = -1
        dtgListPO.CurrentPageIndex = 0
        BindGrid()

    End Sub

#End Region


    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgListPO.CurrentPageIndex = 0
        BindGrid()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        DoDownload()
    End Sub

    Private Sub ddlProductCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlProductCategory.SelectedIndexChanged
        Me.BindToddlCategory()
    End Sub
End Class