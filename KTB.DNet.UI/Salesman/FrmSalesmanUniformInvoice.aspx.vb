Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.Globalization
Imports System.IO
Imports System.Text

Public Class FrmSalesmanUniformInvoice
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlKodePSeragam As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNoOrder As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblPopUpOrderNo As System.Web.UI.WebControls.Label
    Protected WithEvents ltrlDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents dtgKwitansi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDownloadExcel As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGetDealer As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lbtnPrintInvoice As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblGrandTotalTHarga As System.Web.UI.WebControls.Label
    Protected WithEvents lblGrandTotalTHargaPPN As System.Web.UI.WebControls.Label
    Protected WithEvents chkNoInvoice As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtNoInvoice As System.Web.UI.WebControls.TextBox

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
    Private sHelper As New SessionHelper
    Private criterias As CriteriaComposite
    Private criteriaDDL As CriteriaComposite
    Dim strFileNm As String
    Dim strFileNmHeader As String
    Private _downloadPriv As Boolean = False
    Private _downloadDealerPriv As Boolean = False
    Private _printPriv As Boolean = False
#End Region

#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If indexPage >= 0 Then
            If Not IsNothing(sHelper.GetSession("ModeBack")) Then
                indexPage = ReadCriteria()
                dtgKwitansi.CurrentPageIndex = indexPage
                sHelper.RemoveSession("ModeBack")
            End If

            Dim arlSalesmanUnifOrderHeader As ArrayList = New SalesmanUniformOrderHeaderFacade(User).RetrieveActiveList(indexPage + 1, dtgKwitansi.PageSize, totalRow, viewstate("SortColUI"), viewstate("SortDirectionUI"), CType(sHelper.GetSession("criterias"), CriteriaComposite))

            dtgKwitansi.DataSource = arlSalesmanUnifOrderHeader
            dtgKwitansi.VirtualItemCount = totalRow

            If indexPage = 0 Then
                dtgKwitansi.CurrentPageIndex = 0
            End If

            dtgKwitansi.DataBind()
            SaveCriteria(indexPage)

            Dim GrandTotalHarga As Decimal = 0
            Dim GrandTotalHargaPlusPPN As Decimal = 0

            Dim arlSalesmanUnifOrderHeaderTmp As ArrayList = New SalesmanUniformOrderHeaderFacade(User).Retrieve(CType(sHelper.GetSession("criterias"), CriteriaComposite))
            If arlSalesmanUnifOrderHeaderTmp.Count > 0 Then
                ' mendapatkan grandtotal dari value, request bug 1373
                For Each item As SalesmanUniformOrderHeader In arlSalesmanUnifOrderHeaderTmp
                    GrandTotalHarga += item.TotalHarga
                    GrandTotalHargaPlusPPN += (0.1 * item.TotalHarga) + (item.TotalHarga)
                Next
            End If

            lblGrandTotalTHarga.Text = Format(GrandTotalHarga, "#,##0")
            lblGrandTotalTHargaPPN.Text = Format(GrandTotalHargaPlusPPN, "#,##0")

        End If
    End Sub
    Private Sub SaveCriteria(ByVal IntCurrentPageIndex As Integer)
        Dim crits As Hashtable = New Hashtable
        If txtGetDealer.Value = "KTB" OrElse txtGetDealer.Value = "MKS" Then
            crits.Add("DealerCode", txtDealerCode.Text)
        Else
            crits.Add("DealerCode", ltrlDealerCode.Text)
        End If
        crits.Add("SalesmanUnifDistributionCode", ddlKodePSeragam.SelectedValue)
        crits.Add("OrderNo", txtNoOrder.Text)
        crits.Add("SortCol", viewstate("SortColUI"))
        crits.Add("SortDir", viewstate("SortDirectionUI"))
        crits.Add("CurrentPageIndex", dtgKwitansi.CurrentPageIndex)
        sHelper.SetSession("UniformInvoice", crits)

    End Sub
    Private Function ReadCriteria() As Integer
        Dim crits As Hashtable
        Dim intCurIndex As Integer
        intCurIndex = 0
        crits = CType(sHelper.GetSession("UniformInvoice"), Hashtable)
        If Not IsNothing(crits) Then
            If txtGetDealer.Value = "KTB" OrElse txtGetDealer.Value = "MKS" Then
                txtDealerCode.Text = CStr(crits.Item("DealerCode"))
            Else
                ltrlDealerCode.Text = CStr(crits.Item("DealerCode"))
            End If
            ddlKodePSeragam.SelectedValue = CStr(crits.Item("SalesmanUnifDistributionCode"))
            txtNoOrder.Text = CStr(crits.Item("OrderNo"))
            viewstate("SortColUI") = crits.Item("SortCol")
            viewstate("SortDirectionUI") = crits.Item("SortDir")
            intCurIndex = CInt(crits.Item("CurrentPageIndex"))
        End If
        Return intCurIndex
    End Function
    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objDealer As Dealer = CType(sHelper.GetSession("Dealer"), Dealer)

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            ' Kalau KTB dapat browse data dealer yg lainnya
            If txtDealerCode.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "Dealer.DealerCode", MatchType.InSet, CommonFunction.GetStrValue(txtDealerCode.Text, ";", ",")))
            End If
        Else
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "Dealer.DealerCode", MatchType.Exact, ltrlDealerCode.Text))
        End If

        If ddlKodePSeragam.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "SalesmanUnifDistribution.ID", MatchType.Exact, ddlKodePSeragam.SelectedValue))
        End If

        If txtNoOrder.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "OrderNumber", MatchType.InSet, CommonFunction.GetStrValue(txtNoOrder.Text, ";", ",")))
        End If

        If chkNoInvoice.Checked = True Then            
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "InvoiceNo", MatchType.Exact, txtNoInvoice.Text))
        End If

        sHelper.SetSession("criterias", criterias)
    End Sub
    Private Sub BindDDL()
        CommonFunction.BindSalesmanUnifDistributionCode(ddlKodePSeragam, Me.User, True)
    End Sub
    Private Sub Initialize()
        viewstate.Add("SortColUI", "SalesmanUnifDistribution.SalesmanUnifDistributionCode")
        viewstate.Add("SortDirectionUI", Sort.SortDirection.ASC)

        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        ltrlDealerCode.Text = objUserInfo.Dealer.DealerCode

        Dim objDealer As Dealer = sHelper.GetSession("DEALER")
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            lbtnPrintInvoice.Visible = _printPriv
            'dtgKwitansi.Columns(1).Visible = True
        Else
            lbtnPrintInvoice.Visible = False
            'dtgKwitansi.Columns(1).Visible = False
        End If
    End Sub
    Private Sub SetSetting()
        sHelper.SetSession("strFileNm", "RekapPesanan")
        sHelper.SetSession("strFileNmHeader", "Rekap Pesanan")

        Dim objDealer As Dealer = CType(sHelper.GetSession("Dealer"), Dealer)

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            VisibleControl(True)
            txtGetDealer.Value = objDealer.DealerCode '"KTB"   ' hidden file
        Else
            VisibleControl(False)
            txtGetDealer.Value = "Other"
        End If
    End Sub
    Private Sub VisibleControl(ByVal blnVisible As Boolean)
        txtDealerCode.Visible = blnVisible
        lblSearchDealer.Visible = blnVisible
        ltrlDealerCode.Visible = Not blnVisible
    End Sub
    Private Sub BindControlsAttribute()
        lblPopUpOrderNo.Attributes("onclick") = "ShowPPSalesmanUniformOrderSelection()"
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub
    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dtgKwitansi.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sHelper.GetSession("criterias")) Then
            crits = CType(sHelper.GetSession("criterias"), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New SalesmanUniformOrderHeaderFacade(User).Retrieve(crits)

        If arrData.Count > 0 Then
            DoDownload(arrData)
        End If
    End Sub
    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        If Not IsNothing(sHelper.GetSession("strFileNm")) Then
            strFileNm = CType(sHelper.GetSession("strFileNm"), String)
        End If
        If Not IsNothing(sHelper.GetSession("strFileNmHeader")) Then
            strFileNmHeader = CType(sHelper.GetSession("strFileNmHeader"), String)
        End If

        sFileName = strFileNm & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Dim ListData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(ListData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(ListData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteListData(sw, data)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub
    Private Sub WriteListData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder
        Dim sumTotalHarga As Decimal
        Dim sumTotalHargaPPN As Decimal

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(strFileNmHeader)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Kode Pesanan" & tab)
            itemLine.Append("Deskripsi " & tab)
            itemLine.Append("No Order" & tab)
            itemLine.Append("Total Harga" & tab)
            itemLine.Append("PPN" & tab)
            itemLine.Append("Total Harga + PPN" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            sumTotalHarga = 0
            sumTotalHargaPPN = 0
            'Dim myculture As CultureInfo = New CultureInfo("ID-id")

            For Each item As SalesmanUniformOrderHeader In data
                sumTotalHarga = sumTotalHarga + item.TotalHarga
                sumTotalHargaPPN = sumTotalHargaPPN + (item.TotalHarga + (0.1 * item.TotalHarga))
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.SalesmanUnifDistribution.SalesmanUnifDistributionCode & tab)
                itemLine.Append(item.SalesmanUnifDistribution.Description & tab)
                itemLine.Append(item.OrderNumber & tab)
                'itemLine.Append(item.TotalHarga.ToString("#,##0", myculture) & tab)
                itemLine.Append(CLng(item.TotalHarga) & tab)
                itemLine.Append("10%" & tab)
                itemLine.Append((CLng(item.TotalHarga) + CLng((0.1 * item.TotalHarga))) & tab)
                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

            ' menambahkan jumlah total dibagian bawah
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(tab & tab & tab & tab & CLng(sumTotalHarga) & tab & tab) ' disesuaikan dengan urutan kolom
            itemLine.Append(CLng(sumTotalHargaPPN)) ' disesuaikan dengan urutan kolom
            sw.WriteLine(itemLine.ToString())

        End If
    End Sub
#End Region

#Region "Event handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsNothing(sHelper.GetSession("LOGINUSERINFO")) Then
            Dim objUserInfo As UserInfo = New UserInfo
            objUserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)
            If objUserInfo.Dealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                CheckPrivilege()
                _downloadPriv = CheckDownloadPrivilege()
                btnDownloadExcel.Visible = _downloadPriv
            ElseIf objUserInfo.Dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                CheckDealerPrivilege()
                _downloadDealerPriv = CheckDownloadDealerPrivilege()
                btnDownloadExcel.Visible = _downloadDealerPriv
                _printPriv = CheckPrintDealerPrivilege()
                lbtnPrintInvoice.Visible = _printPriv
            End If
        End If
        If Not IsPostBack Then
            Initialize()
            BindDDL()
            BindControlsAttribute()
            SetSetting()
            CreateCriteria()
            BindToGrid(0)
        End If
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgKwitansi.CurrentPageIndex = 0
        CreateCriteria()
        BindToGrid(dtgKwitansi.CurrentPageIndex)
    End Sub
    Private Sub dtgKwitansi_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgKwitansi.PageIndexChanged
        dtgKwitansi.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgKwitansi.CurrentPageIndex)
    End Sub
    Private Sub dtgKwitansi_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgKwitansi.SortCommand
        If e.SortExpression = viewstate("SortColUI") Then
            If viewstate("SortDirectionUI") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirectionUI", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirectionUI", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColUI", e.SortExpression)
        BindToGrid(0)
    End Sub
    Private Sub dtgKwitansi_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgKwitansi.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim objSalesmanUniformOrderHeader As SalesmanUniformOrderHeader = e.Item.DataItem

            Dim lblno As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblno.Text = e.Item.ItemIndex + 1 + (dtgKwitansi.CurrentPageIndex * dtgKwitansi.PageSize)

            'Dim lblPPN As Label = CType(e.Item.FindControl("lblPPN"), Label)
            Dim lblTotalHargaPPN As Label = CType(e.Item.FindControl("lblTotalHargaPPN"), Label)
            Dim lblTotalHarga As Label = CType(e.Item.FindControl("lblTotalHarga"), Label)
            Dim ttlHarga As Decimal = CType(lblTotalHarga.Text, Decimal)
            lblTotalHarga.Text = ttlHarga.ToString("#,##0")
            lblTotalHargaPPN.Text = (ttlHarga + (0.1 * CType(lblTotalHarga.Text, Decimal))).ToString("#,##0")

            Dim chkSelection As CheckBox = CType(e.Item.FindControl("chkSelection"), CheckBox)
            If objSalesmanUniformOrderHeader.InvoiceNo = "" Then
                chkSelection.Checked = False
                chkSelection.Enabled = True

            Else
                chkSelection.Checked = True
                chkSelection.Enabled = False
                ' yang sudah dicetak akan font bold, dan backcolor beda
                e.Item.Font.Bold = True
                e.Item.BackColor = Color.FromKnownColor(KnownColor.AliceBlue)
            End If
            chkSelection.Visible = _printPriv

        End If

        If (e.Item.ItemType = ListItemType.Footer) Then
            Dim total As Double = 0
            For Each x As WebControls.DataGridItem In dtgKwitansi.Items
                total = total + CType(CType(x.FindControl("lblTotalHarga"), Label).Text, Double)
            Next
            CType(e.Item.FindControl("lblSumTotalHarga"), Label).Text = total.ToString("#,##0")

            total = 0
            For Each x As WebControls.DataGridItem In dtgKwitansi.Items
                total = total + CType(CType(x.FindControl("lblTotalHargaPPN"), Label).Text, Double)
            Next
            CType(e.Item.FindControl("lblSumTotalHargaPPN"), Label).Text = total.ToString("#,##0")
        End If
    End Sub
    Private Sub dtgKwitansi_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgKwitansi.ItemCommand
        If e.CommandName = "Print" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim id As Integer = CInt(lblID.Text)
            Response.Redirect("FrmSalesmanUniformInvoice2.aspx?id=" & id)
        End If
    End Sub
    Private Sub btnDownloadExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub
    Private Sub lbtnPrintInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnPrintInvoice.Click
        Dim arr As ArrayList
        Dim strId As String
        Dim arrCetak As New ArrayList
        Dim critsTmp As CriteriaComposite

        'For Each item As DataGridItem In dtgKwitansi.Items
        '    Dim chkSelection As CheckBox = CType(item.FindControl("chkSelection"), CheckBox)
        '    Dim lblID As Label = CType(item.FindControl("lblID"), Label)

        '    If chkSelection.Enabled And chkSelection.Checked Then
        '        If strId = "" Then
        '            strId = lblID.Text
        '        Else
        '            strId = strId & ";" & lblID.Text

        '        End If
        '    End If
        'Next

        If dtgKwitansi.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data dalam pencarian")
        End If

        ' 16-Nov-2007   Deddy H     Refer to bug 1234, print secara keseluruhan yg belum diprint
        If Not IsNothing(sHelper.GetSession("criterias")) Then
            critsTmp = CType(sHelper.GetSession("criterias"), CriteriaComposite)
        End If

        ' mengambil data yang dibutuhkan
        arrCetak = New SalesmanUniformOrderHeaderFacade(User).Retrieve(critsTmp)
        For Each item As SalesmanUniformOrderHeader In arrCetak
            If item.InvoiceNo = "" Then
                If strId = "" Then
                    strId = item.ID.ToString
                Else
                    strId = strId & ";" & item.ID.ToString
                End If
            Else
                If chkNoInvoice.Checked = True And item.InvoiceNo = txtNoInvoice.Text.Trim Then
                    If strId = "" Then
                        strId = item.ID.ToString
                    Else
                        strId = strId & ";" & item.ID.ToString
                    End If
                End If
            End If
        Next

        '-------------
        If strId <> "" Then
            ' mengambil data yg akan dicetak saja
            Response.Redirect("FrmSalesmanUniformInvoice2.aspx?id=" & strId)
        Else
            MessageBox.Show("Tidak ada data untuk dicetak, data yang telah dicetak tdk bisa dicetak kembali")
        End If

    End Sub

#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.UniformListRecapView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pelatihan Tenaga Penjual - Rekap Pesanan")
        End If
    End Sub

    Private Function CheckDownloadPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.UniformListRecapDonwload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function


    Private Sub CheckDealerPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.KwitansiView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Seragam Tenaga Penjual - Kwitansi")
        End If
    End Sub

    Private Function CheckDownloadDealerPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.KwitansiDonwload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckPrintDealerPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PrintKwitansi_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region
End Class
