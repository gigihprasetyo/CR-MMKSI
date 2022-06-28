Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.BabitSalesComm
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text

Public Class FrmListPengajuanBabit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnProcess As System.Web.UI.WebControls.Button
    Protected WithEvents txtNoPengajuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStart As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlEnd As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lbStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents pnlChangeStatus As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlSearch As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlStatusPersetujuan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents hdnValNew As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblTotalAlokasiBabit As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlokasiTambahan As System.Web.UI.WebControls.Label
    Protected WithEvents lblJumlahDisetujui As System.Web.UI.WebControls.Label
    Protected WithEvents pnlHeadKTB As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlTahun As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hdnValDel As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ddlTahunTo As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variables Declaration"

    Dim en As EnumBabit
    Dim objDealer As New Dealer
    Dim sessHelper As New SessionHelper
#End Region

#Region "Custom Methods"

    Sub BindJenisKegiatan()
        en = New EnumBabit
        ddlJenisKegiatan.DataTextField = "BabitValue"
        ddlJenisKegiatan.DataValueField = "BabitCode"
        ddlJenisKegiatan.DataSource = en.BabitProposalTypeList()
        ddlJenisKegiatan.DataBind()
        ddlJenisKegiatan.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))
    End Sub

    Private Sub BindPeriode(ByRef _control As DropDownList)
        _control.Items.Clear()
        _control.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        _control.Items.Add(New ListItem("Januari", 1))
        _control.Items.Add(New ListItem("Februari", 2))
        _control.Items.Add(New ListItem("Maret", 3))
        _control.Items.Add(New ListItem("April", 4))
        _control.Items.Add(New ListItem("Mei", 5))
        _control.Items.Add(New ListItem("Juni", 6))
        _control.Items.Add(New ListItem("Juli", 7))
        _control.Items.Add(New ListItem("Agustus", 8))
        _control.Items.Add(New ListItem("September", 9))
        _control.Items.Add(New ListItem("Oktober", 10))
        _control.Items.Add(New ListItem("November", 11))
        _control.Items.Add(New ListItem("Desember", 12))
    End Sub

    Sub BindMonth()
        ' CommonFunction.BindFromEnum("Month", ddlStart, User, False, "NameStatus", "ValStatus")
        ' CommonFunction.BindFromEnum("Month", ddlEnd, User, False, "NameStatus", "ValStatus")

        BindPeriode(ddlStart)
        BindPeriode(ddlEnd)

        ddlStart.SelectedValue = -1
        ddlEnd.SelectedValue = -1
    End Sub

    Sub BindTahun()
        ddlTahun.Items.Clear()
        ddlTahun.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahun.Items.Add(New ListItem(i.ToString, i))
        Next

        ddlTahunTo.Items.Clear()
        ddlTahunTo.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahunTo.Items.Add(New ListItem(i.ToString, i))
        Next
    End Sub

    Sub BindStatusSearch()
        en = New EnumBabit
        lbStatus.DataTextField = "BabitValue"
        lbStatus.DataValueField = "BabitCode"
        lbStatus.DataSource = en.StatusBabitProposalList
        lbStatus.DataBind()
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            lbStatus.Items.RemoveAt(0)
            lbStatus.Items.RemoveAt(0)
        End If
    End Sub

    Sub BindChangeStatus()
        en = New EnumBabit
        ddlStatus.DataTextField = "BabitValue"
        ddlStatus.DataValueField = "BabitCode"

        Dim arl As New ArrayList
        Dim arlTmp As New ArrayList

        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            arlTmp = en.StatusBabitProposalListDealer
            For Each item As BabitItem In arlTmp
                If (CekPrivilegeStatusBatal()) And (item.BabitValue = EnumBabit.StatusBabitProposal.Batal.ToString) Then
                    arl.Add(item)
                ElseIf (CekPrivilegeStatusValidation()) And (item.BabitValue = EnumBabit.StatusBabitProposal.Validasi.ToString) Then
                    arl.Add(item)
                End If
            Next
        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            arlTmp = en.StatusBabitProposalListKTB
            For Each item As BabitItem In arlTmp
                If (CekPrivilegeStatusProses()) And (item.BabitValue = EnumBabit.StatusBabitProposal.Proses.ToString) Then
                    arl.Add(item)
                ElseIf (CekPrivilegeStatusTolak()) And (item.BabitValue = EnumBabit.StatusBabitProposal.Tolak.ToString) Then
                    arl.Add(item)
                ElseIf (CekPrivilegeStatusDisetujui()) And (item.BabitValue = EnumBabit.StatusBabitProposal.Disetujui.ToString) Then
                    arl.Add(item)
                End If
            Next
        End If

        ddlStatus.DataSource = arl
        ddlStatus.DataTextField = "BabitValue"
        ddlStatus.DataValueField = "BabitCode"

        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))
        ddlStatus.SelectedIndex = 0
    End Sub
    Private Sub CreateCriteria()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitKhususAmount", MatchType.Exact, 0))

        If (txtKodeDealer.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        If (ddlJenisKegiatan.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "ActivityType", MatchType.Exact, ddlJenisKegiatan.SelectedValue))
        End If

        If (txtNoPengajuan.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "NoPengajuan", MatchType.Exact, txtNoPengajuan.Text.Trim))
        End If

        Dim _strStatus As String = String.Empty
        For Each item As ListItem In lbStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next

        If _strStatus <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(BabitProposal), "Status", MatchType.InSet, "(" & _strStatus & ")"))
        Else
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                criterias.opAnd(New Criteria(GetType(BabitProposal), "Status", MatchType.InSet, "(0,2,3,4,5)"))
            End If
        End If

        If (ddlStart.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitAllocation.Babit.StartPeriod", MatchType.LesserOrEqual, ddlStart.SelectedValue))
        End If

        'If (ddlEnd.SelectedValue <> "-1") Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitAllocation.Babit.EndPeriod", MatchType.GreaterOrEqual, ddlEnd.SelectedValue))
        'End If

        If ddlTahun.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitAllocation.Babit.BabitYear", MatchType.LesserOrEqual, ddlTahun.SelectedValue))
        End If

        If ddlTahunTo.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitAllocation.Babit.BabitYearEnd", MatchType.GreaterOrEqual, ddlTahunTo.SelectedValue))
        End If

        If ddlEnd.SelectedValue <> "-1" Then
            If (ddlStart.SelectedValue = "-1") Or (ddlTahun.SelectedValue = "-1") Or (ddlTahunTo.SelectedValue = "-1") Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "BabitAllocation.Babit.EndPeriod", MatchType.GreaterOrEqual, ddlEnd.SelectedValue))
            End If
        End If

        If ddlStatusPersetujuan.SelectedValue <> "-1" Then
            If ddlStatusPersetujuan.SelectedValue = "1" Then
                'BELUM DISETUJUI, TDK ADA ENUM, BELUM ADA NO PERSETUJUANNYA
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "NoPersetujuan", MatchType.InSet, "(null, '')"))
            Else
                'SDH DISETUJUI
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "NoPersetujuan", MatchType.No, ""))
            End If
        End If
        sessHelper.SetSession("crits", criterias)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arl As ArrayList = New ArrayList
        Dim bfacade As BabitSalesComm.BabitProposalFacade = New BabitSalesComm.BabitProposalFacade(User)
        Dim arlFirst As ArrayList = bfacade.RetrieveByCriteria(CType(sessHelper.GetSession("crits"), CriteriaComposite), indexPage + 1, dtgList.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))

        For Each obj As BabitProposal In arlFirst
            If objDealer.TitleDealer = EnumDealerTittle.DealerTittle.KTB.ToString() Then
                If (bfacade.IsCreatedByKTB(obj, objDealer)) Then
                    arl.Add(obj)
                Else
                    If obj.Status <> EnumBabit.StatusBabitProposal.Baru Then
                        arl.Add(obj)
                    End If
                End If
            Else
                arl.Add(obj)
            End If
        Next

        If (ddlStart.SelectedValue <> "-1" And ddlTahun.SelectedValue <> "-1" And ddlEnd.SelectedValue <> "-1" And ddlTahunTo.SelectedValue <> "-1") Then
            Dim dtmFrom2 As DateTime = New DateTime(CInt(ddlTahun.SelectedValue), CInt(ddlStart.SelectedValue), 1)
            Dim dtmTo2 As DateTime = New DateTime(CInt(ddlTahunTo.SelectedValue), CInt(ddlEnd.SelectedValue), 1)
            Dim arl2 As ArrayList = bfacade.FilterBabitProposalByPeriodeList(arl, dtmFrom2, dtmTo2)
            arl = New ArrayList
            arl = arl2
            If IsNothing(arl) Then
                arl = New ArrayList
            End If
        End If

        dtgList.DataSource = arl
        dtgList.VirtualItemCount = totalRow
        dtgList.DataBind()
        If arl.Count > 0 Then
            btnDownload.Enabled = CekPrivilegeDownload()
            pnlChangeStatus.Visible = True
        Else
            dtgList.DataSource = New ArrayList
            dtgList.DataBind()
            btnDownload.Enabled = False
            pnlChangeStatus.Visible = False
            MessageBox.Show("Data tidak ditemukan")
        End If

        Dim totalAlokasi As Decimal = 0
        Dim totalTambahan As Decimal = 0
        Dim totalDisetujui As Decimal = 0

        Dim tmpArl As ArrayList = CommonFunction.SortArraylist(arl, GetType(BabitProposal), "Dealer.ID", Sort.SortDirection.ASC)
        Dim tmpDealerId As Integer = 0
        For Each obj As BabitProposal In tmpArl
            If (tmpDealerId <> obj.Dealer.ID) Then
                tmpDealerId = obj.Dealer.ID
                totalAlokasi += obj.BabitAllocation.DanaBabit
                totalTambahan += obj.BabitAllocation.TotalDanaBabit(EnumBabit.BabitAllocationType.Alokasi_Tambahan)
            End If
            totalDisetujui += obj.KTBApprovalAmount
        Next

        lblTotalAlokasiBabit.Text = String.Format("Alokasi Babit : Rp.{0}", totalAlokasi.ToString("#,##0"))
        lblAlokasiTambahan.Text = String.Format("Alokasi Tambahan : Rp.{0}", totalTambahan.ToString("#,##0"))
        lblJumlahDisetujui.Text = String.Format("Jumlah Disetujui : Rp.{0}", totalDisetujui.ToString("#,##0"))

        ' add security
        If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then

            'If Not CekPrivilegeStatusProses() Then
            '    dtgList.Columns(1).Visible = False  ' kolom centang item
            'Else
            '    dtgList.Columns(1).Visible = True
            'End If
            'If Not CekPrivilegeStatusTolak() Then
            '    dtgList.Columns(1).Visible = False  ' kolom centang item
            'Else
            '    dtgList.Columns(1).Visible = True
            'End If
            'If Not CekPrivilegeStatusDisetujui() Then
            '    dtgList.Columns(1).Visible = False  ' kolom centang item
            'Else
            '    dtgList.Columns(1).Visible = True
            'End If
        Else
            ' case dealer
            'If Not CekPrivilegeStatusBatal() Then
            '    dtgList.Columns(1).Visible = False  ' kolom centang item
            'Else
            '    dtgList.Columns(1).Visible = True
            'End If

            'If Not CekPrivilegeStatusValidation() Then
            '    dtgList.Columns(1).Visible = False  ' kolom centang item
            'Else
            '    dtgList.Columns(1).Visible = True
            'End If
        End If
    End Sub

    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtKodeDealer.Text)
        arrLastState.Add(ddlJenisKegiatan.SelectedValue)
        arrLastState.Add(txtNoPengajuan.Text)
        arrLastState.Add(ddlStart.SelectedValue)
        arrLastState.Add(ddlEnd.SelectedValue)
        arrLastState.Add(dtgList.CurrentPageIndex)
        arrLastState.Add(lbStatus.SelectedIndex)
        arrLastState.Add(ddlStatusPersetujuan.SelectedIndex)
        sessHelper.SetSession("BABITPROPOSALSESSIONLASTSTATE", arrLastState)
    End Sub

    Private Sub GetSessionCriteria()
        If Not Session("BABITPROPOSALSESSIONLASTSTATE") Is Nothing Then
            Dim arrLastState As ArrayList = Session("BABITPROPOSALSESSIONLASTSTATE")
            If Not arrLastState Is Nothing Then
                txtKodeDealer.Text = arrLastState.Item(0)
                ddlJenisKegiatan.SelectedValue = arrLastState.Item(1)
                txtNoPengajuan.Text = arrLastState.Item(2)
                ddlStart.SelectedValue = arrLastState.Item(3)
                ddlEnd.SelectedValue = arrLastState.Item(4)
                dtgList.CurrentPageIndex = arrLastState.Item(5)
                lbStatus.SelectedIndex = arrLastState.Item(6)
                ddlStatusPersetujuan.SelectedIndex = arrLastState.Item(7)
                BindDataGrid(dtgList.CurrentPageIndex)
            End If
        End If
    End Sub

    Private Sub BindStatusPersetujuan()
        ddlStatusPersetujuan.Items.Clear()
        ddlStatusPersetujuan.Items.Add(New ListItem("Silakan Pilih", "-1"))
        ddlStatusPersetujuan.Items.Add(New ListItem("Belum Disetujui", "1"))
        ddlStatusPersetujuan.Items.Add(New ListItem("Sudah Disetujui", "2"))
    End Sub
    Private Function getDisticnt(ByVal str As String) As String
        str = str.Substring(0, str.Length - 1).Trim
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.Category), "Description", MatchType.InSet, "(" + str + ")"))
        Dim arr As ArrayList = New CategoryFacade(User).Retrieve(criterias2)
        Dim strs As String = String.Empty
        For Each items As Category In arr
            strs = strs + items.Description + ","
        Next
        If strs.Length > 1 Then
            Return strs.Substring(0, strs.Length - 1)
        Else
            Return String.Empty
        End If
    End Function

    Private Sub WriteTraineeData(ByVal sw As StreamWriter, ByVal dataEvent As ArrayList, ByVal dataIklan As ArrayList, ByVal dataPameran As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("BABIT - DAFTAR PENGAJUAN BABIT")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        '=======IKLAN======
        If (dataIklan.Count > 0) Then
            itemLine.Append("IKLAN" & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("TGL INPUT" & tab)
            itemLine.Append("NO" & tab)
            itemLine.Append("PERIODE" & tab)
            itemLine.Append("DEALER" & tab)
            itemLine.Append("NAMA DEALER" & tab)
            itemLine.Append("KOTA" & tab)
            itemLine.Append("AREA" & tab)
            itemLine.Append("NO PENGAJUAN" & tab)
            itemLine.Append("NO PERSETUJUAN" & tab)
            itemLine.Append("TGL TRM EVD." & tab)
            itemLine.Append("TGL KRM INV." & tab)
            itemLine.Append("BIAYA PENGAJUAN" & tab)
            itemLine.Append("BIAYA PERSETUJUAN" & tab)
            itemLine.Append("TOTAL PENGUNAAN DANA BABIT" & tab)
            itemLine.Append("SISA ALOKASI BABIT" & tab)
            itemLine.Append("MEDIA" & tab)
            itemLine.Append("NAMA MEDIA" & tab)
            itemLine.Append("TGL MULAI" & tab)
            itemLine.Append("TGL SELESAI" & tab)
            itemLine.Append("PRODUK KATEGORI" & tab)
            itemLine.Append("KENDARAAN DISPLAY" & tab)
            itemLine.Append("BIAYA IKLAN" & tab)
            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            For Each item As BabitProposal In dataIklan
                Dim BiayaPengajuan As Double = 0
                For Each _content As BPIklan In item.BPIklans
                    BiayaPengajuan += _content.Expense
                Next
                For Each _content As BPIklan In item.BPIklans
                    itemLine.Remove(0, itemLine.Length)  '-- Empty line
                    itemLine.Append(_content.CreatedTime.ToString("dd MMM yyyy") & tab)
                    itemLine.Append(i.ToString & tab)
                    Dim monthFrom As String = New DateTime(item.BabitAllocation.Babit.BabitYear, item.BabitAllocation.Babit.StartPeriod, 1).ToString("MMM")
                    Dim monthTo As String = New DateTime(item.BabitAllocation.Babit.BabitYearEnd, item.BabitAllocation.Babit.EndPeriod, 1).ToString("MMM")
                    Dim year As String = item.BabitAllocation.Babit.BabitYear.ToString()
                    itemLine.Append(String.Format("{0} - {1} {2}", monthFrom, monthTo, year) & tab)
                    itemLine.Append(item.Dealer.DealerCode & tab)
                    itemLine.Append(item.Dealer.DealerName & tab)
                    itemLine.Append(item.Dealer.City.CityName & tab)
                    itemLine.Append(item.Dealer.Province.ProvinceName & tab)
                    itemLine.Append(item.NoPengajuan & tab)
                    itemLine.Append(item.NoPersetujuan & tab)
                    If (item.TglTerimaEvidance <> "1/1/1753") Then
                        itemLine.Append(item.TglTerimaEvidance.ToString("dd MMM yyyy") & tab)
                    Else
                        itemLine.Append(String.Empty & tab)
                    End If
                    If (item.BabitPayments.Count > 0) Then
                        If (CType(item.BabitPayments(0), BabitPayment).PaymentDate <> "1/1/1753") Then
                            itemLine.Append(CType(item.BabitPayments(0), BabitPayment).PaymentDate.ToString("dd MMM yyyy") & tab)
                        Else
                            itemLine.Append(String.Empty & tab)
                        End If
                    Else
                        itemLine.Append(String.Empty & tab)
                    End If
                    itemLine.Append(Convert.ToDouble(BiayaPengajuan) & tab)
                    itemLine.Append(Convert.ToDouble(item.KTBApprovalAmount) & tab)
                    If item.BabitAllocation Is Nothing Then
                        itemLine.Append("0" & tab)
                        itemLine.Append("0" & tab)
                    Else
                        itemLine.Append(Convert.ToDouble(item.BabitAllocation.PenggunaanBabit).ToString() & tab)
                        itemLine.Append(Convert.ToDouble(item.BabitAllocation.SisaBabit).ToString() & tab)
                    End If
                    itemLine.Append(IIf(_content.MediaType = EnumBabit.MediaType.Cetak, "Cetak", "Elektronik") & tab)
                    itemLine.Append(_content.MediaName & tab)
                    itemLine.Append(_content.StartDate.ToString("dd MMM yyyy") & tab)
                    itemLine.Append(_content.EndDate.ToString("dd MMM yyyy") & tab)
                    itemLine.Append(_content.Category.Description & tab)
                    itemLine.Append(_content.VechileType.Description & tab)
                    itemLine.Append(Convert.ToDouble(_content.Expense).ToString() & tab)
                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            Next
        End If
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        '======PAMERAN======
        If (dataPameran.Count > 0) Then
            itemLine.Append("PAMERAN" & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("TGL INPUT" & tab)
            itemLine.Append("NO" & tab)
            itemLine.Append("NO & TGL SURAT DEALER" & tab)
            itemLine.Append("TANGGAL" & tab)
            itemLine.Append("DEALER" & tab)
            itemLine.Append("KOTA" & tab)
            itemLine.Append("AREA" & tab)
            itemLine.Append("TEMPAT PAMERAN" & tab)
            itemLine.Append("PERIODE/TGL PAMERAN" & tab)
            itemLine.Append("DISPLAY KENDARAAN" & tab)
            itemLine.Append("LUAS TEMPAT PAMERAN (m2)" & tab)
            itemLine.Append("LAMA PAMERAN (day)" & tab)
            itemLine.Append("KATEGORI PRODUK" & tab)
            itemLine.Append("PERIODE" & tab)
            itemLine.Append("JUMLAH CLAIM" & tab)
            itemLine.Append("APPROVAL" & tab)
            itemLine.Append("GROUP" & tab)
            itemLine.Append("HARGA /m2/day" & tab)
            itemLine.Append("PAYMENT DATA BY CASHEER" & tab)
            itemLine.Append("KWT/SRT KONTRAK" & tab)
            itemLine.Append("BUDGET" & tab)
            itemLine.Append("NO SURAT PERSETUJUAN MMKSI" & tab)
            itemLine.Append("KETERANGAN/TERIMA KWITANSI" & tab)
            itemLine.Append("APPROVAL" & tab)
            itemLine.Append("NO KWITANSI DEALER" & tab)
            itemLine.Append("TGL TERIMA KWITANSI" & tab)
            itemLine.Append("LAPORAN" & tab)
            itemLine.Append("DOKUMENTASI" & tab)
            itemLine.Append("TARGET PENJUALAN" & tab)
            itemLine.Append("REALISASI PENJUALAN" & tab)
            itemLine.Append("PROSPEK" & tab)
            itemLine.Append("HOT PROSPEK" & tab)
            itemLine.Append("EVENT ORGANIZER" & tab)
            itemLine.Append("ALAMAT" & tab)
            itemLine.Append("REASON" & tab)
            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            For Each item As BabitProposal In dataPameran
                Dim szVehicle As String = ""
                Dim szCat As String = ""
                For Each _item2 As PameranDisplay In item.BPPameran.PameranDisplays
                    szVehicle &= _item2.VechileType.Description & ", "
                    szCat &= _item2.Category.Description & ", "
                Next
                If (szVehicle.Length > 1) Then
                    szVehicle = szVehicle.Substring(0, szVehicle.Length - 2)
                End If
                If (szCat.Length > 1) Then
                    szCat = szCat.Substring(0, szCat.Length - 2)
                End If
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(item.CreatedTime.ToString("dd MMM yyyy") & tab)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.NoSuratDealer & tab)
                itemLine.Append(item.BPPameran.CreatedTime.ToString("dd MMM yyyy") & tab)
                itemLine.Append(item.BabitAllocation.Dealer.DealerName & tab)
                itemLine.Append(item.BabitAllocation.Dealer.City.CityName & tab)
                itemLine.Append(item.BabitAllocation.Dealer.City.Province.ProvinceName & tab)
                itemLine.Append(item.BPPameran.Place & tab)
                itemLine.Append(String.Format("{0} - {1}", item.BPPameran.StartExhibitionDate.ToString("dd MMM yyyy"), item.BPPameran.EndExhibitionDate.ToString("dd MMM yyyy")) & tab)
                itemLine.Append(szVehicle & tab)
                itemLine.Append(item.BPPameran.ExhibitionSize & tab)
                itemLine.Append(item.BPPameran.NumberOfDay & tab)
                itemLine.Append(szCat & tab)
                itemLine.Append(New DateTime(item.BabitAllocation.Babit.BabitYear, item.BabitAllocation.Babit.StartPeriod, 1).ToString("MMMM") & tab)
                'itemLine.Append(Convert.ToDouble(item.BPPameran.TotalExpense) & tab)
                Dim totalPameranCost As Decimal = 0
                If (Not IsNothing(item.BPPameran.PameranDisplays)) Then
                    For Each item2 As PameranDisplay In item.BPPameran.PameranDisplays
                        totalPameranCost += item2.Others
                    Next
                End If
                totalPameranCost += item.BPPameran.Expense
                itemLine.Append(Convert.ToDouble(totalPameranCost) & tab)
                itemLine.Append(Convert.ToDouble(item.KTBApprovalAmount) & tab)
                itemLine.Append(item.Dealer.DealerGroup.GroupName & tab)
                itemLine.Append(Convert.ToDouble(item.BPPameran.Expense) & tab)
                itemLine.Append(item.TglTerimaEvidance.ToString("dd MMM yyyy") & tab)
                itemLine.Append(item.NoPengajuan & tab)
                itemLine.Append("" & tab)
                itemLine.Append(item.NoPersetujuan & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append(Convert.ToDouble(item.BPPameran.SalesTarget) & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next
        End If
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        '======EVENT=======
        If (dataEvent.Count > 0) Then
            itemLine.Append("EVENT" & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("TGL INPUT" & tab)
            itemLine.Append("NO" & tab)
            itemLine.Append("NO & TGL SURAT DEALER" & tab)
            itemLine.Append("TANGGAL" & tab)
            itemLine.Append("DEALER" & tab)
            itemLine.Append("KOTA" & tab)
            itemLine.Append("AREA" & tab)
            itemLine.Append("TEMPAT EVENT" & tab)
            itemLine.Append("PERIODE/TGL PAMERAN" & tab)
            itemLine.Append("DISPLAY KENDARAAN" & tab)
            itemLine.Append("LUAS TEMPAT PAMERAN (m2)" & tab)
            itemLine.Append("LAMA PAMERAN (day)" & tab)
            itemLine.Append("KATEGORI PRODUK" & tab)
            itemLine.Append("PERIODE" & tab)
            itemLine.Append("JUMLAH CLAIM" & tab)
            itemLine.Append("APPROVAL" & tab)
            itemLine.Append("GROUP" & tab)
            itemLine.Append("HARGA /m2/day" & tab)
            itemLine.Append("PAYMENT DATA BY CASHEER" & tab)
            itemLine.Append("KWT/SRT KONTRAK" & tab)
            itemLine.Append("BUDGET" & tab)
            itemLine.Append("NO SURAT PERSETUJUAN MMKSI" & tab)
            itemLine.Append("KETERANGAN/TERIMA KWITANSI" & tab)
            itemLine.Append("APPROVAL" & tab)
            itemLine.Append("NO KWITANSI DEALER" & tab)
            itemLine.Append("TGL TERIMA KWITANSI" & tab)
            itemLine.Append("LAPORAN" & tab)
            itemLine.Append("DOKUMENTASI" & tab)
            itemLine.Append("TARGET PENJUALAN" & tab)
            itemLine.Append("REALISASI PENJUALAN" & tab)
            itemLine.Append("PROSPEK" & tab)
            itemLine.Append("HOT PROSPEK" & tab)
            itemLine.Append("EVENT ORGANIZER" & tab)
            itemLine.Append("ALAMAT" & tab)
            itemLine.Append("REASON" & tab)
            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            For Each item As BabitProposal In dataEvent
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(item.CreatedTime.ToString("dd MMM yyyy") & tab)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.NoSuratDealer & tab)
                itemLine.Append(item.BPEvent.CreatedTime.ToString("dd MMM yyyy") & tab)
                itemLine.Append(item.BabitAllocation.Dealer.DealerName & tab)
                itemLine.Append(item.BabitAllocation.Dealer.City.CityName & tab)
                itemLine.Append(item.BabitAllocation.Dealer.City.Province.ProvinceName & tab)
                itemLine.Append(item.BPEvent.Place & tab)
                itemLine.Append(String.Format("{0} - {1}", item.BPEvent.StartEventDate.ToString("dd MMM yyyy"), item.BPEvent.EndEventDate.ToString("dd MMM yyyy")) & tab)
                itemLine.Append("" & tab)
                itemLine.Append(item.BPEvent.EventSize & tab)
                itemLine.Append(item.BPEvent.NumberOfDay & tab)
                itemLine.Append("" & tab)
                itemLine.Append(New DateTime(item.BabitAllocation.Babit.BabitYear, item.BabitAllocation.Babit.StartPeriod, 1).ToString("MMMM") & tab)
                itemLine.Append(Convert.ToDouble(item.BPEvent.PlaceExpense) & tab)
                itemLine.Append(Convert.ToDouble(item.KTBApprovalAmount) & tab)
                itemLine.Append(item.Dealer.DealerGroup.GroupName & tab)
                itemLine.Append(Convert.ToDouble(item.BPEvent.TotalExpense) & tab)
                itemLine.Append(item.TglTerimaEvidance.ToString("dd MMM yyyy") & tab)
                itemLine.Append(item.NoPengajuan & tab)
                itemLine.Append("" & tab)
                itemLine.Append(item.NoPersetujuan & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append(Convert.ToDouble(item.BPEvent.SalesTarget) & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next
        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub

    Private Sub DoDownload(ByVal dataEvent As ArrayList, ByVal dataIklan As ArrayList, ByVal dataPameran As ArrayList)
        Dim sFileName As String
        sFileName = "Daftar Pengajuan Babit[" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "]"

        '-- Temp file must be a randomly named file!
        Dim TraineeData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        'Try
        If imp.Start() Then

            Dim finfo As FileInfo = New FileInfo(TraineeData)
            If finfo.Exists Then
                finfo.Delete()  '-- Delete temp file if exists
            End If

            '-- Create file stream
            Dim fs As FileStream = New FileStream(TraineeData, FileMode.CreateNew)
            '-- Create stream writer
            Dim sw As StreamWriter = New StreamWriter(fs)

            '-- Write data to temp file
            WriteTraineeData(sw, dataEvent, dataIklan, dataPameran)

            sw.Close()
            fs.Close()

            imp.StopImpersonate()
            imp = Nothing

        End If

        '-- Download invoice data to client!
        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        'Catch ex As Exception
        'MessageBox.Show("Download data gagal")
        'End Try
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Pengajuan Babit")
        End If
    End Sub


    Private blnPrivilegeDetails As Boolean
    Private blnPrivilegeStatus As Boolean
    Private blnPrivilegeUploadRealization As Boolean
    Private blnPrivilegeDownloadRealization As Boolean
    Private blnPrivilegeLinkAgreement As Boolean = False

    Private Sub InitiateActionIcon()
        blnPrivilegeDetails = CekPrivilegeDetails()
        blnPrivilegeStatus = CekPrivilegeStatus()
        blnPrivilegeUploadRealization = CekPrivilegeUploadRealization()
        blnPrivilegeLinkAgreement = CekLinkAgreementPrivilege()
        'blnPrivilegeDownloadRealization = bCekBtnDLPriv
    End Sub

    Private Function CekPrivilegeDetails() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitListViewDetail_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeStatus() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitListViewStatus_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeUploadRealization() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitListUploadRealization_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Dim bCekBtnDLPriv As Boolean = SecurityProvider.Authorize(context.User, SR.PengajuanBabitListDownloadRealization_Privilege)
    'Private Function CekPrivilegeDownloadRealization() As Boolean
    '    If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitListDownloadRealization_Privilege) Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function

    Private Function CekPrivilegeStatusProses() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitListStatusProses_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeStatusTolak() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitListStatusTolak_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeStatusDisetujui() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitListStatusDisetujui_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeStatusBatal() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.Status_batal_babit_privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeStatusValidation() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.Status_validasi_babit_privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPrivilegeDownload() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitListDownload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekLinkAgreementPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, sr.BabitViewLinkPersetujuan_privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        InitiateActionIcon()
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        objDealer = CType(Session("DEALER"), Dealer)
        If (Not IsPostBack) Then
            ViewState("currSortColumn") = "NoPengajuan"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                txtKodeDealer.Text = objDealer.DealerCode
                txtKodeDealer.Enabled = False
                pnlSearch.Visible = False
                pnlHeadKTB.Visible = False
            ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                lblKodeDealer.Attributes("onclick") = "ShowPPDealerSelection();"
                txtKodeDealer.Enabled = True
                pnlHeadKTB.Visible = True
            End If
            BindStatusPersetujuan()
            BindJenisKegiatan()
            BindMonth()
            BindTahun()
            BindStatusSearch()
            BindChangeStatus()
            If Session("BABITPROPOSALSESSIONLASTSTATE") Is Nothing Then
                SetSessionCriteria()
            Else
                GetSessionCriteria()
            End If
        Else
            If Request.Form("hdnValNew") = "1" Then
                btnProcess_Click(Nothing, Nothing)
                hdnValNew.Value = "-1"
            ElseIf Request.Form("hdnValNew") = "0" Then
                hdnValNew.Value = "-1"
            End If

            If (Request.Form("hdnValDel")) = "1" Then
                Dim obj As BabitProposal = CType(sessHelper.GetSession("BabitProposalDelete"), BabitProposal)
                If (Not IsNothing(obj)) Then
                    Dim i As Integer = New BabitProposalFacade(User).Delete(obj)
                    sessHelper.RemoveSession("BabitProposalDelete")
                    hdnValDel.Value = "-1"
                    BindDataGrid(dtgList.CurrentPageIndex)
                End If
            ElseIf (Request.Form("hdnValDel")) = "0" Then
                sessHelper.RemoveSession("BabitProposalDelete")
                hdnValDel.Value = "-1"
            End If
        End If

        'add security
        'If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
        '    'If Not CekPrivilegeStatusProses() Or Not CekPrivilegeStatusTolak() Or Not CekPrivilegeStatusDisetujui() Then
        '    '    dtgList.Columns(1).Visible = False  ' kolom centang item
        '    'Else
        '    '    dtgList.Columns(1).Visible = True
        '    'End If
        '    btnDownload.Enabled = CekPrivilegeDownload()
        '    'Else
        '    '    ' case dealer
        '    '    If Not CekPrivilegeStatusBatal() Or Not CekPrivilegeStatusValidation() Then
        '    '        dtgList.Columns(1).Visible = False  ' kolom centang item
        '    '    Else
        '    '        dtgList.Columns(1).Visible = True
        '    '    End If
        'End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If (CInt(ddlTahun.SelectedValue) <> -1 And CInt(ddlTahunTo.SelectedValue) <> -1) Then
            If (CInt(ddlTahun.SelectedValue) = CInt(ddlTahunTo.SelectedValue)) Then
                If (CInt(ddlStart.SelectedValue) <> -1 And CInt(ddlEnd.SelectedValue) <> -1) Then
                    If CInt(ddlStart.SelectedValue) > CInt(ddlEnd.SelectedValue) Then
                        MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
                        Exit Sub
                    End If
                End If
            ElseIf (CInt(ddlTahun.SelectedValue) > CInt(ddlTahunTo.SelectedValue)) Then
                MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
                Exit Sub
            End If
        End If
        dtgList.CurrentPageIndex = 0
        CreateCriteria()
        BindDataGrid(dtgList.CurrentPageIndex)
    End Sub
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arlEven As New ArrayList
        Dim arlIklan As New ArrayList
        Dim arlPameran As New ArrayList
        Dim facade As BabitProposalFacade = New BabitSalesComm.BabitProposalFacade(User)

        Dim i As Integer = 0
        For Each item As DataGridItem In dtgList.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If (chk.Checked) Then
                Dim obj As BabitProposal = facade.Retrieve(Convert.ToInt32(dtgList.DataKeys().Item(i)))
                If (obj.ActivityType = EnumBabit.BabitProposalType.Iklan) Then
                    arlIklan.Add(obj)
                ElseIf (obj.ActivityType = EnumBabit.BabitProposalType.Even) Then
                    arlEven.Add(obj)
                ElseIf (obj.ActivityType = EnumBabit.BabitProposalType.Pameran) Then
                    arlPameran.Add(obj)
                End If
            End If
            i += 1
        Next

        ''Iklan
        'Dim criteriasIklan As CriteriaComposite = CType(sessHelper.GetSession("crits"), CriteriaComposite)
        'criteriasIklan.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "ActivityType", MatchType.Exact, CType(EnumBabit.BabitProposalType.Iklan, Short)))
        ''CreateCriteria(criteriasIklan)
        'arlIklan = New BabitSalesComm.BabitProposalFacade(User).Retrieve(criteriasIklan)
        ''Even
        'Dim criteriasEven As CriteriaComposite = CType(sessHelper.GetSession("crits"), CriteriaComposite)
        'criteriasEven.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "ActivityType", MatchType.Exact, CType(EnumBabit.BabitProposalType.Even, Short)))
        ''CreateCriteria(criteriasEven)
        'arlEven = New BabitSalesComm.BabitProposalFacade(User).Retrieve(criteriasEven)
        ''PAMERAN
        'Dim criteriasPameran As CriteriaComposite = CType(sessHelper.GetSession("crits"), CriteriaComposite)
        'criteriasPameran.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitProposal), "ActivityType", MatchType.Exact, CType(EnumBabit.BabitProposalType.Pameran, Short)))
        ''CreateCriteria(criteriasPameran)
        'arlPameran = New BabitSalesComm.BabitProposalFacade(User).Retrieve(criteriasPameran)

        DoDownload(arlEven, arlIklan, arlPameran)
    End Sub
    Private Sub dtgList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgList.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then

            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1 + (dtgList.CurrentPageIndex * dtgList.PageSize)).ToString())
            e.Item.Cells(2).Controls.Add(lNum)

            Dim oBabitProposal As BabitProposal = CType(e.Item.DataItem, BabitProposal)
            Dim imgStatus As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgStatus"), System.Web.UI.WebControls.Image)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblActivityType As Label = CType(e.Item.FindControl("lblActivityType"), Label)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnStatus As LinkButton = CType(e.Item.FindControl("lbtnStatus"), LinkButton)
            Dim lblSendEvc As Label = CType(e.Item.FindControl("lblSendEvc"), Label)
            Dim lblCost As Label = CType(e.Item.FindControl("lblCost"), Label)
            Dim lblSendInv As Label = CType(e.Item.FindControl("lblSendInv"), Label)
            Dim lblBiayaPersetujuan As Label = CType(e.Item.FindControl("lblBiayaPersetujuan"), Label)
            Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            Dim lbtnPersetujuanBabit As LinkButton = CType(e.Item.FindControl("lbtnPersetujuanBabit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            If (oBabitProposal.FileName <> String.Empty) Then
                lbtnDownload.Visible = True
            End If

            lblBiayaPersetujuan.Text = oBabitProposal.KTBApprovalAmount.ToString("#,##0")
            lbtnStatus.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpStatusChangeBabitProposal.aspx?ID=" & oBabitProposal.ID & "', '', 500, 500, null);return false;")

            If (oBabitProposal.TglTerimaEvidance <> "1/1/1753") Then
                lblSendEvc.Text = oBabitProposal.TglTerimaEvidance.ToString("dd/MM/yyyy")
            End If

            If (oBabitProposal.BabitPayments.Count > 0) Then
                If (CType(oBabitProposal.BabitPayments(0), BabitPayment).PaymentDate <> "1/1/1753") Then
                    lblSendInv.Text = CType(oBabitProposal.BabitPayments(0), BabitPayment).PaymentDate.ToString("dd/MM/yyyy")
                    If (CType(oBabitProposal.BabitPayments(0), BabitPayment).NomorPembayaran.ToLower().StartsWith(BabitPayment.REJECTED_ALL_DESC.ToLower())) Then
                        e.Item.BackColor = System.Drawing.Color.LightBlue
                    ElseIf (CType(oBabitProposal.BabitPayments(0), BabitPayment).NomorPembayaran.ToLower().StartsWith(BabitPayment.REJECTED_ALL_DESC.ToLower())) Then
                        e.Item.BackColor = System.Drawing.Color.LightBlue
                    End If
                End If
            End If

            If oBabitProposal.NoPersetujuan.Trim = "" Then
                imgStatus.ToolTip = "Belum Bayar"
                imgStatus.ImageUrl = "../images/red.gif"
            Else
                imgStatus.ToolTip = "Sudah Bayar"
                imgStatus.ImageUrl = "../images/green.gif"

            End If

            'If (Not IsNothing(oBabitProposal.GLAccount)) Then
            '    imgStatus.ToolTip = "Sudah Bayar"
            '    imgStatus.ImageUrl = "../images/green.gif"
            'Else
            '    imgStatus.ToolTip = "Belum Bayar"
            '    imgStatus.ImageUrl = "../images/red.gif"
            'End If

            If (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Baru, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Baru.ToString()
                lbtnDelete.Visible = True
            ElseIf (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Batal, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Batal.ToString()
            ElseIf (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Disetujui, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Disetujui.ToString()
            ElseIf (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Proses, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Proses.ToString()
                If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    lblBiayaPersetujuan.Text = "0"
                End If
            ElseIf (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Tolak, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Tolak.ToString()
            ElseIf (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Validasi, Short)) Then
                lblStatus.Text = EnumBabit.StatusBabitProposal.Validasi.ToString()
            End If

            If (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Even, Short)) Then
                lblActivityType.Text = "Event" 'EnumBabit.BabitProposalType.Even.ToString()
                Dim EventCost As Integer = 0
                If (Not IsNothing(oBabitProposal.BPEvent.EventActivitys)) Then
                    For Each item As EventActivity In oBabitProposal.BPEvent.EventActivitys
                        Dim PlaceValue As Decimal
                        If IsNumeric(item.Place) Then
                            PlaceValue = CDec(item.Place)
                        Else
                            PlaceValue = 0
                        End If
                        EventCost += item.Comsumption + item.Entertainment + item.Equipment + item.Others + PlaceValue
                    Next
                End If
                lblCost.Text = EventCost.ToString("#,##0")
                EventCost = 0
            ElseIf (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
                lblActivityType.Text = EnumBabit.BabitProposalType.Iklan.ToString()
                Dim arlIklan As New ArrayList
                arlIklan = New BabitSalesComm.BabitProposalFacade(User).RetrieveIklanByBabitProposalID(oBabitProposal.ID)
                Dim IklanCost As Integer = 0
                If Not IsNothing(arlIklan) Then
                    For Each item As BPIklan In arlIklan
                        IklanCost += item.Expense
                    Next
                End If
                lblCost.Text = IklanCost.ToString("#,##0")
                IklanCost = 0
            ElseIf (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
                lblActivityType.Text = EnumBabit.BabitProposalType.Pameran.ToString()
                Dim totalPameranCost As Decimal = 0
                If (Not IsNothing(oBabitProposal.BPPameran.PameranDisplays)) Then
                    For Each item As PameranDisplay In oBabitProposal.BPPameran.PameranDisplays
                        totalPameranCost += item.Others
                    Next
                End If
                totalPameranCost += oBabitProposal.BPPameran.Expense
                lblCost.Text = totalPameranCost.ToString("#,##0")
                totalPameranCost = 0
            End If

            Dim lbtnUploadReal As LinkButton = CType(e.Item.FindControl("lbtnUploadReal"), LinkButton)
            lbtnUploadReal.Attributes("onClick") = GeneralScript.GetPopUpEventReference("../Babit/FrmBabitProposalUploadRealization.aspx?id=" & oBabitProposal.ID, "", 360, 700, "ViewForm")

            'Dim lbtnDownloadReal As LinkButton = CType(e.Item.FindControl("lbtnDownloadReal"), LinkButton)
            'If oBabitProposal.BabitRealizationFile.Trim = String.Empty Then
            '    lbtnDownloadReal.Visible = False
            'Else
            '    lbtnDownloadReal.Visible = True
            'End If

            lbtnDownload.Visible = bCekBtnDLPriv

            If objDealer.TitleDealer = EnumDealerTittle.DealerTittle.KTB.ToString() Then
                lbtnEdit.Visible = True
                'Dim ktbid As Integer = 0
                ''cek if the proposal CreatedBy ktb user, so it can be edit by ktb
                'Try
                '    ktbid = CInt(oBabitProposal.CreatedBy.Substring(0, 6))
                '    If (ktbid = objDealer.ID) Then
                '        If (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Baru, Short)) Then
                '            lbtnEdit.Visible = True
                '        End If
                '    Else
                '        lbtnEdit.Visible = False
                '    End If
                'Catch ex As Exception
                '    lbtnEdit.Visible = False
                'End Try
            Else
                'Modified by : Diana - Bugs 1310
                If (oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Baru, Short)) Then
                    lbtnEdit.Visible = True
                Else
                    lbtnEdit.Visible = False
                End If
            End If

            'Modified by : Diana - Hanya yang memiliki status proses yang boleh disetujui
            If oBabitProposal.Status <> CType(EnumBabit.StatusBabitProposal.Proses, Integer) Then
                lbtnPersetujuanBabit.Visible = False
            Else
                If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                    lbtnPersetujuanBabit.Visible = blnPrivilegeLinkAgreement
                Else
                    lbtnPersetujuanBabit.Visible = False
                End If
            End If

            '-- add security
            If Not blnPrivilegeDetails Then
                Dim lbtnDetails As LinkButton = CType(e.Item.FindControl("lbtnDetails"), LinkButton)
                lbtnDetails.Visible = False
            End If
            If Not blnPrivilegeStatus Then
                lbtnStatus.Visible = False
            End If
            If Not blnPrivilegeUploadRealization Then
                lbtnUploadReal.Visible = False
            End If
            'If blnPrivilegeDownloadRealization Then
            '    lbtnDownloadReal.Visible = False
            'End If

        End If
    End Sub

    Private Sub dtgList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgList.SortCommand
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
        BindDataGrid(dtgList.CurrentPageIndex)
    End Sub

    Private Sub dtgList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgList.PageIndexChanged
        dtgList.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgList.CurrentPageIndex)
    End Sub

    Private Sub dtgList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgList.ItemCommand
        Select Case e.CommandName
            Case "edit"
                SetSessionCriteria()
                Response.Redirect("~/Babit/FrmPengajuanBabit.aspx?id=" & e.CommandArgument & "&Mode=Edit&Src=ListPengajuan")
            Case "detail"
                SetSessionCriteria()
                Response.Redirect("~/Babit/FrmPengajuanBabit.aspx?id=" & e.CommandArgument & "&Mode=View&Src=ListPengajuan")
            Case "download"
                Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("PengajuanBabit") & "\" & e.CommandArgument
                Response.Redirect("../Download.aspx?file=" & PathFile)
            Case "uploadReal"
                BindDataGrid(dtgList.CurrentPageIndex)
                'Popup
            Case "downloadReal"
                Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("RealisasiBabit") & "\" & e.CommandArgument
                Response.Redirect("../Download.aspx?file=" & PathFile)
            Case "Persetujuan"
                SetSessionCriteria(CType(e.CommandArgument, Integer))
                Response.Redirect("../BABIT/FrmPemotonganAlokasiBabit.aspx?src=ListProposalBabit&id=" & e.CommandArgument)
            Case "Del"
                Dim bpf As BabitProposalFacade = New BabitProposalFacade(User)
                Dim obj As BabitProposal = bpf.Retrieve(CInt(e.CommandArgument))
                If (hdnValDel.Value = "-1") Then
                    MessageBox.Confirm("Yakin ingin menghapus nomor pengajuan " & obj.NoPengajuan, "hdnValDel")
                    sessHelper.SetSession("BabitProposalDelete", obj)
                    Return
                End If
        End Select
    End Sub

    Private Sub SetSessionCriteria(ByVal BabitProposalID As Integer)
        Dim arrLastState As ArrayList = New ArrayList
        Dim obj As BabitProposal

        obj = New BabitProposalFacade(User).Retrieve(BabitProposalID)

        arrLastState.Add(obj.CreatedTime)
        arrLastState.Add(obj.CreatedTime)
        arrLastState.Add(obj.NoPengajuan)
        arrLastState.Add(0)
        arrLastState.Add(obj.Dealer.DealerCode)
        arrLastState.Add(obj.BabitAllocation.Babit.AllocationType)
        arrLastState.Add(-1)
        arrLastState.Add(obj.NoPersetujuan)
        Session("BABITALLOCATIONPTGSESSIONLASTSTATE") = arrLastState
    End Sub

    Private Function ValidateProcess(ByVal StatusHid As String, ByVal StatusGrid As String, ByRef Status As String) As String
        Dim oDPA As DealerProposalAction
        oDPA = New DealerProposalAction(StatusHid)

        If (ddlStatus.SelectedValue = CType(EnumBabit.StatusBabitProposal.Validasi, Short)) Then
            If (oDPA.Submit = -1) Then
                Status = String.Empty
                Return "Status Babit proposal harus baru"
            End If
        End If
        If (ddlStatus.SelectedValue = CType(EnumBabit.StatusBabitProposal.Batal, Short)) Then
            If (oDPA.Submit = -1) Then
                Status = String.Empty
                Return "Status Babit proposal harus baru"
            End If
        End If
        If (ddlStatus.SelectedValue = CType(EnumBabit.StatusBabitProposal.Tolak, Short)) Then
            If (oDPA.Verify = -1) Then
                Status = String.Empty
                Return "Status Babit proposal harus Validasi"
            End If
        End If
        If (ddlStatus.SelectedValue = CType(EnumBabit.StatusBabitProposal.Proses, Short)) Then
            If (oDPA.Verify = -1) Then
                Status = String.Empty
                Return "Status Babit proposal harus validasi"
            End If
        End If
        If (ddlStatus.SelectedValue = CType(EnumBabit.StatusBabitProposal.Disetujui, Short)) Then
            If (oDPA.Approve = -1) Then
                Status = String.Empty
                Return "Status Babit proposal harus proses"
            End If
        End If
        Status = StatusHid
    End Function

    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If ddlStatus.SelectedValue = "-1" Then
            MessageBox.Show("Silakan pilih Status ")
            BindDataGrid(dtgList.CurrentPageIndex)
            Return
        End If

        If (hdnValNew.Value = "-1") Then
            MessageBox.Confirm(String.Format("Apakah anda yakin ingin {0} ?", ddlStatus.SelectedItem.Text), "hdnValNew")
            Return
        End If

        Dim arl As ArrayList = New ArrayList
        Dim oBabitProposal As BabitProposal
        Dim i As Integer = 0
        Dim Status As String = String.Empty

        For Each item As DataGridItem In dtgList.Items
            Dim strErrMsg As String
            Dim chkItemChecked As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim lblStatus As Label = CType(item.FindControl("lblStatus"), Label)
            Dim lblStatusHid As Label = CType(item.FindControl("lblStatusHid"), Label)

            If (chkItemChecked.Checked) Then
                If (Status = String.Empty) Then
                    strErrMsg = ValidateProcess(lblStatusHid.Text.Trim, lblStatus.Text.Trim, Status)
                    If (strErrMsg <> String.Empty) Then
                        MessageBox.Show(strErrMsg)
                        BindDataGrid(dtgList.CurrentPageIndex)
                        Return

                    End If
                Else
                    If (lblStatusHid.Text.Trim = Status) Then
                        strErrMsg = ValidateProcess(lblStatusHid.Text.Trim, lblStatus.Text.Trim, Status)
                        If (strErrMsg <> String.Empty) Then
                            MessageBox.Show(strErrMsg)
                            BindDataGrid(dtgList.CurrentPageIndex)
                            Return
                        End If
                    Else
                        MessageBox.Show("Status Babit Proposal yang dipilih tidak sama")
                        Status = String.Empty
                        BindDataGrid(dtgList.CurrentPageIndex)
                        Return
                    End If
                End If
                oBabitProposal = New BabitProposal
                oBabitProposal = New BabitSalesComm.BabitProposalFacade(User).Retrieve(Convert.ToInt32(dtgList.DataKeys().Item(i)))

                If ddlStatus.SelectedValue = EnumBabit.StatusBabitProposal.Disetujui Then
                    If oBabitProposal.KTBApprovalAmount <= 0 OrElse oBabitProposal.TglTerimaEvidance < New DateTime(1900, 1, 1) Then
                        MessageBox.Show("Silakan isi Nilai persetujuan dahulu. Proses dibatalkan.")
                        BindDataGrid(dtgList.CurrentPageIndex)
                        Return
                    End If
                ElseIf ddlStatus.SelectedValue = EnumBabit.StatusBabitProposal.Validasi Then
                    If (oBabitProposal.ActivityType = EnumBabit.BabitProposalType.Even) Then
                        If (DateTime.Now > oBabitProposal.BPEvent.StartEventDate.AddDays(-7)) Then
                            MessageBox.Show("Ubah status menjadi validasi tidak bisa dilakukan karena batas waktu sudah habis")
                            Exit Sub
                        End If
                    ElseIf (oBabitProposal.ActivityType = EnumBabit.BabitProposalType.Pameran) Then
                        If (DateTime.Now > oBabitProposal.BPPameran.StartExhibitionDate.AddDays(-7)) Then
                            MessageBox.Show("Ubah status menjadi validasi tidak bisa dilakukan karena batas waktu sudah habis")
                            Exit Sub
                        End If
                    End If
                End If
                oBabitProposal.Status = ddlStatus.SelectedValue
                arl.Add(oBabitProposal)
            End If
            i = i + 1
        Next

        If (arl.Count > 0) Then
            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitProposal(arl) = 1) Then
                MessageBox.Show(SR.UpdateSucces)
                BindDataGrid(dtgList.CurrentPageIndex)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            MessageBox.Show("Data Babit Proposal belum di pilih")
        End If

    End Sub

#End Region
End Class
