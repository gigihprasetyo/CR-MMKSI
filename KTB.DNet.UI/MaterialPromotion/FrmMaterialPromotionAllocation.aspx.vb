#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.MaterialPromotion
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
Imports System.io

#End Region

Public Class FrmMaterialPromotionAllocation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeBarang As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents chkAlokasi As System.Web.UI.WebControls.CheckBox
    Protected WithEvents BtnGetData As System.Web.UI.WebControls.Button
    Protected WithEvents dgAlokasi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblKodeBarang As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealers As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi Variabel"
    Private sHelper As SessionHelper = New SessionHelper
    Private totalRow As Integer = 0
    Private arlToDisplay As ArrayList = New ArrayList
    Private _createPriv As Boolean = False
    Private _downloadPriv As Boolean = False
#End Region

#Region "Custom Method"
    Private Sub BindDDl()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MaterialPromotionPeriod), "StartDate", Sort.SortDirection.ASC))
        'sortColl.Add(New Sort(GetType(MaterialPromotionPeriod), "StartMonth", Sort.SortDirection.ASC))
        'sortColl.Add(New Sort(GetType(MaterialPromotionPeriod), "EndMonth", Sort.SortDirection.ASC))

        Dim arlPeriod As ArrayList = New MaterialPromotionPeriodFacade(User).Retrieve(criterias, sortColl)

        Dim x As Integer = 0
        For Each item As MaterialPromotionPeriod In arlPeriod
            Dim ddlitem As DropDownList
            ddlPeriod.Items.Add("")
            ddlPeriod.Items(x).Text = item.PeriodName 'enumMonthGet.GetName(item.StartDate.Month) & " - " & enumMonthGet.GetName(item.EndDate.Month) & " " & item.EndDate.Year.ToString
            ddlPeriod.Items(x).Value = item.ID.ToString
            x = x + 1
        Next
        ddlPeriod.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))

        If ddlPeriod.Items.Count > 0 Then
            ddlPeriod.SelectedIndex = 0
        End If


    End Sub
    Private Sub BindDatagrid(ByVal idxpage As Integer)

        If (idxpage >= 0) Then

            If CType(sHelper.GetSession("arlToDisplay"), ArrayList).Count > 0 Then
                dgAlokasi.DataSource = CommonFunction.PageAndSortArraylist(CType(sHelper.GetSession("arlToDisplay"), ArrayList), idxpage, dgAlokasi.PageSize, GetType(MaterialPromotionAllocation), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
            Else
                dgAlokasi.DataSource = New ArrayList
            End If
            dgAlokasi.VirtualItemCount = CType(sHelper.GetSession("arlToDisplay"), ArrayList).Count
            dgAlokasi.DataBind()
        End If

    End Sub
    Private Function GetArrayBarangAll() As String
        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        Dim arlBarang As ArrayList
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            arlBarang = New MaterialPromotionFacade(User).RetrieveActiveListDealer()
        Else
            arlBarang = New MaterialPromotionFacade(User).RetrieveActiveList()
        End If

        Dim result As String = ""
        For Each item As MaterialPromotion In arlBarang
            result = result & item.GoodNo & ";"
        Next

        result = Left(result, result.Length - 1)
        Return result
    End Function
    Private Function GetArlToDisplay(ByVal idxPage As Integer) As ArrayList

        Dim arlDealer() As String = txtDealer.Text.Split(";".ToCharArray(), 1000)
        Dim arlBarang() As String

        If CType(sHelper.GetSession("KodeBarang"), String).Trim = "" Then
            arlBarang = GetArrayBarangAll.Split(";")
        Else
            arlBarang = CType(sHelper.GetSession("KodeBarang"), String).Split(";")
        End If


        If (arlDealer.Length * arlBarang.Length) > dgAlokasi.PageSize And Not _IsCari Then
            MessageBox.Show("Data yang ditampilkan tidak boleh melebihi " & dgAlokasi.PageSize)
            Return New ArrayList
        End If

        Dim objPeriod As MaterialPromotionPeriod = New MaterialPromotionPeriodFacade(User).Retrieve(CInt(ddlPeriod.SelectedValue))
        Dim sortColl As SortCollection = New SortCollection

        sortColl.Add(New Sort(GetType(MaterialPromotionAllocation), "ValidateQty", Sort.SortDirection.DESC))

        For Each itemDealer As String In arlDealer
            Dim objdealer As Dealer = New DealerFacade(User).Retrieve(itemDealer)
            If objdealer.ID = 0 Then
                MessageBox.Show("Dealer " & itemDealer & " Tidak Ada")
                sHelper.SetSession("ArlToDisplay", New ArrayList)
                Return New ArrayList
            End If
            For Each itemBarang As String In arlBarang

                Dim objBarang As MaterialPromotion = New MaterialPromotionFacade(User).RetrieveActive(itemBarang)
                If objBarang.ID = 0 Then
                    MessageBox.Show("No Barang " & itemBarang & " Tidak Ada/Tidak Aktif")
                    sHelper.SetSession("ArlToDisplay", New ArrayList)
                    Return New ArrayList
                End If

                Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
                If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    If objBarang.Status <> CByte(EnumMaterialPromotion.MasterMaterialPromotionStatus.Aktif) Then
                        MessageBox.Show("No Barang " & itemBarang & " Tidak Aktif")
                        sHelper.SetSession("ArlToDisplay", New ArrayList)
                        Return New ArrayList
                    End If
                End If

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "Dealer.DealerCode", MatchType.Exact, itemDealer))
                criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotion.GoodNo", MatchType.Exact, itemBarang))
                criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotion.Status", MatchType.Exact, CShort(EnumMaterialPromotion.MasterMaterialPromotionStatus.Aktif)))
                'criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "Qty", MatchType.Greater, 0))
                'If Not _IsCari Then
                criterias.opAnd(New Criteria(GetType(MaterialPromotionAllocation), "MaterialPromotionPeriod.ID", MatchType.Exact, objPeriod.ID.ToString))
                'End If


                Dim arlAlokasi As ArrayList = New MaterialPromotionAllocationFacade(User).Retrieve(criterias, sortColl)
                Dim objAlokasi As MaterialPromotionAllocation
                If arlAlokasi.Count = 0 Then
                    objAlokasi = New MaterialPromotionAllocation
                    objAlokasi.Dealer = objdealer
                    objAlokasi.MaterialPromotion = objBarang
                    objAlokasi.MaterialPromotionPeriod = objPeriod
                Else
                    objAlokasi = arlAlokasi(0)
                End If

                'Alokasi >0
                If chkAlokasi.Checked Then
                    'Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
                    If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        If objAlokasi.ValidateQty > 0 Then
                            arlToDisplay.Add(objAlokasi)
                        End If
                    Else
                        If objAlokasi.Qty > 0 Then
                            arlToDisplay.Add(objAlokasi)
                        End If

                    End If

                Else
                    arlToDisplay.Add(objAlokasi)
                End If

            Next
        Next


        totalRow = arlToDisplay.Count
        sHelper.SetSession("ArlToDisplay", arlToDisplay)

        'arlToDisplay = CommonFunction.PageAndSortArraylist(arlToDisplay, idxPage, dgAlokasi.PageSize, GetType(MaterialPromotionAllocation), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))

        Return arlToDisplay

    End Function
    Private _IsCari As Boolean = False
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()

        If Not IsPostBack Then
            BindDDl()
            dgAlokasi.DataSource = New ArrayList
            dgAlokasi.DataBind()
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        End If
        lblKodeBarang.Attributes("onclick") = "ShowMaterialPromotionSelection();"

        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        sHelper.SetSession("CurrentDealer", objUserInfo)
        _createPriv = CheckCreatePriv()
        _downloadPriv = CheckDownloadPriv()
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            'Bug 1159
            dgAlokasi.Columns(6).Visible = False
            lblDealers.Visible = False
            txtDealer.Enabled = False
            txtDealer.Text = objUserInfo.Dealer.DealerCode
            btnSimpan.Enabled = False
            Button1.Enabled = False
            BtnGetData.Enabled = False
            chkAlokasi.Checked = True
            chkAlokasi.Enabled = False
            btnDownload.Visible = _downloadPriv

        Else
            'Bug 1285 download enable
            'btnDownload.Enabled = False
            BtnGetData.Visible = _createPriv
            btnDownload.Visible = _downloadPriv
            lblDealers.Attributes("onclick") = "ShowPPDealerSelection();"

        End If
    End Sub
    Private Sub BtnGetData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGetData.Click

        If IsNothing(sender) Then
            Button1.Visible = False
            dgAlokasi.PageSize = 25
        Else
            Button1.Visible = _createPriv
            'Modified by Ikhsan, 20080829
            'Requested By Rince, 
            'to add more combination
            'dgAlokasi.PageSize = 15
            dgAlokasi.PageSize = 24
            'Button1.Visible = True
        End If

        If txtKodeBarang.Text = "" And Not IsNothing(sender) Then
            MessageBox.Show("Masukkan Data Barang Terlebih Dahulu")
        ElseIf txtDealer.Text = "" Then
            MessageBox.Show("Masukkan Data Dealer Terlebih Dahulu")
        Else
            If Not _IsCari Then
                If ddlPeriod.SelectedValue = "-1" Then
                    MessageBox.Show("Pilih periode terlebih dahulu")
                    Return
                End If

                btnDownload.Visible = _downloadPriv
                'btnDownload.Visible = True
                btnSimpan.Visible = _createPriv
                'btnSimpan.Visible = True
                dgAlokasi.Columns(dgAlokasi.Columns.Count - 1).Visible = True
                dgAlokasi.AllowSorting = False
                dgAlokasi.AllowPaging = False
            Else
                dgAlokasi.AllowSorting = True
                dgAlokasi.AllowPaging = True
            End If

            sHelper.SetSession("KodeBarang", txtKodeBarang.Text)
            arlToDisplay = GetArlToDisplay(0)
            If arlToDisplay.Count = 0 Then
                'If dgAlokasi.Items.Count = "0" Then
                dgAlokasi.DataSource = New ArrayList
                dgAlokasi.DataBind()
                MessageBox.Show(SR.DataNotFound(""))
                'End If
                Exit Sub
            End If
            BindDatagrid(0)
        End If
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        If dgAlokasi.Items.Count = 0 Then
            MessageBox.Show("Data Kosong")
            Exit Sub
        End If

        'Cek Qty
        Dim strError As String = ""
        Dim arlBarang() As String = CType(sHelper.GetSession("KodeBarang"), String).Split(";".ToCharArray(), 100)

        For Each itemBarang As String In arlBarang
            Dim objBarang As MaterialPromotion = New MaterialPromotionFacade(User).Retrieve(itemBarang)
            Dim Stock As Integer = objBarang.Stock
            Dim QtyAlokasi As Integer = 0
            For Each item As DataGridItem In dgAlokasi.Items
                Dim lblDealer As Label = item.FindControl("lblDealer")
                Dim lblGoodsNo As Label = item.FindControl("lblGoodsNo")
                Dim lblStock As Label = item.FindControl("lblStock")
                Dim txtAlokasi As TextBox = item.FindControl("txtAlokasi")

                If lblGoodsNo.Text = objBarang.GoodNo Then
                    Dim objalokasi As MaterialPromotionAllocation = CType(sHelper.GetSession("ArlToDisplay"), ArrayList)(item.ItemIndex)
                    If objalokasi.ID = 0 Then
                        QtyAlokasi = QtyAlokasi + Val(txtAlokasi.Text)
                    Else
                        QtyAlokasi = QtyAlokasi + Val(txtAlokasi.Text) - objalokasi.Qty
                    End If
                End If

                If QtyAlokasi > Stock Then
                    MessageBox.Show("Alokasi Material " & objBarang.GoodNo & " Melebihi Batas.")
                    Return
                End If

            Next

        Next



        'If strError <> "" Then
        '    strError.Remove(strError.Length - 2, 2)
        '    MessageBox.Show("Qty Alokasi Tidak Boleh Melebihi Stock ( Record No : " & strError & " )")
        '    Return
        'End If

        Dim ArlToInsert As ArrayList = New ArrayList
        For Each item As DataGridItem In dgAlokasi.Items
            Dim objAlokasi As MaterialPromotionAllocation = CType(sHelper.GetSession("ArlToDisplay"), ArrayList)(item.ItemIndex)
            Dim txtAlokasi As TextBox = item.FindControl("txtAlokasi")
            If (objAlokasi.Qty <> Val(txtAlokasi.Text)) Or (sender Is Me) Then
                objAlokasi.Qty = Val(txtAlokasi.Text)
                ArlToInsert.Add(objAlokasi)
            End If
        Next

        If ArlToInsert.Count > 0 Then
            Dim result As Integer
            If sender Is Me Then
                result = New MaterialPromotionAllocationFacade(User).InsertUpdateDeleteTransaction(ArlToInsert, True)
            Else
                result = New MaterialPromotionAllocationFacade(User).InsertUpdateDeleteTransaction(ArlToInsert, False)
            End If
            If result = 1 Then
                If sender Is Me Then
                    MessageBox.Show("Validasi Berhasil")
                Else
                    MessageBox.Show(SR.SaveSuccess)
                End If
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If

        GetArlToDisplay(dgAlokasi.CurrentPageIndex)
        BindDatagrid(dgAlokasi.CurrentPageIndex)
    End Sub
    Private Sub dgAlokasi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAlokasi.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim objAlokasi As MaterialPromotionAllocation = CType(sHelper.GetSession("ArlToDisplay"), ArrayList)(e.Item.ItemIndex)
            'Dim lblSisa As Label = e.Item.FindControl("lblSisa")
            'lblSisa.Text = objAlokasi.MaterialPromotion.Stock - objAlokasi.Qty
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            lblDealer.ToolTip = objAlokasi.Dealer.DealerName


            Dim lblAlokasi As Label = CType(e.Item.FindControl("lblAlokasi"), Label)
            lblAlokasi.Visible = _IsCari

            Dim txtAlokasi As TextBox = CType(e.Item.FindControl("txtAlokasi"), TextBox)
            txtAlokasi.Visible = Not _IsCari

            Dim objUserInfo As UserInfo = sHelper.GetSession("CurrentDealer")
            If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtAlokasi.Enabled = False
                e.Item.Cells(8).Visible = False
                txtAlokasi.Text = objAlokasi.ValidateQty
                lblAlokasi.Text = objAlokasi.ValidateQty
            End If

            If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                Dim Linkbutton2 As LinkButton = CType(e.Item.FindControl("Linkbutton2"), LinkButton)
                Linkbutton2.Visible = _createPriv
            End If

        End If

    End Sub
    Private Sub dgAlokasi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgAlokasi.SortCommand
        If dgAlokasi.Items.Count = 0 Then
            MessageBox.Show("Tidak ada data yang disorting")
            Exit Sub
        End If

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
        dgAlokasi.SelectedIndex = -1
        dgAlokasi.CurrentPageIndex = 0
        _IsCari = True
        BindDatagrid(dgAlokasi.CurrentPageIndex)

    End Sub
    Private Sub dgAlokasi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgAlokasi.PageIndexChanged
        dgAlokasi.CurrentPageIndex = e.NewPageIndex
        _IsCari = True
        BindDatagrid(dgAlokasi.CurrentPageIndex)

    End Sub
    Private Sub dgAlokasi_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAlokasi.ItemCommand
        If e.CommandName = "Delete" Then
            Dim objToDelete As MaterialPromotionAllocation = CType(sHelper.GetSession("ArlToDisplay"), ArrayList)(e.Item.ItemIndex)
            If objToDelete.ID <> 0 Then
                objToDelete.Qty = 0
                Dim arlToDelete As ArrayList = New ArrayList
                arlToDelete.Add(objToDelete)
                Dim result As Integer = New MaterialPromotionAllocationFacade(User).InsertUpdateDeleteTransaction(arlToDelete, False)
                If result = -1 Then
                    MessageBox.Show(SR.DeleteFail)
                    Return
                Else
                    MessageBox.Show(SR.DeleteSucces)
                End If
            Else
                MessageBox.Show(SR.DeleteSucces)
            End If
            GetArlToDisplay(dgAlokasi.CurrentPageIndex)
            arlToDisplay = CType(sHelper.GetSession("ArlToDisplay"), ArrayList)
            arlToDisplay.RemoveAt((dgAlokasi.CurrentPageIndex * dgAlokasi.PageSize) + e.Item.ItemIndex)
            sHelper.SetSession("ArlToDisplay", arlToDisplay)
            If (arlToDisplay.Count <= dgAlokasi.CurrentPageIndex * dgAlokasi.PageSize) And arlToDisplay.Count > 0 Then
                dgAlokasi.CurrentPageIndex = dgAlokasi.CurrentPageIndex - 1
            End If
            BindDatagrid(dgAlokasi.CurrentPageIndex)
        End If
    End Sub
    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        If dgAlokasi.Items.Count <= 0 Then
            MessageBox.Show("Data alokasi harus ada terlebih dahulu")
            Return
        End If

        If ddlPeriod.SelectedIndex = 0 Then
            MessageBox.Show("Pilih Periode Dulu")
            Return
        End If

        Dim i As Integer = 0
        Dim success As Boolean = False
        Dim Connect As Boolean = False
        Dim sw As StreamWriter
        Dim filename = String.Format("{0}{1}{2}", "AllocMatPromotion", Date.Now.ToString("ddMMyyyyHHmmss"), ".xls")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("DownloadAllocMatPromotion") & "\" & filename  '-- Destination file

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate
        If (Connect = False) Then
            imp = New SAPImpersonate(_user, _password, _webServer)
            Dim finfo As New FileInfo(DestFile)
            Try
                success = imp.Start()
                If success Then
                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    sw = New StreamWriter(DestFile)
                    Connect = True
                End If
            Catch ex As Exception
                Throw ex
                Exit Sub
            End Try
        End If

        ' Data periode jg ditulis
        Dim strTab As String = Chr(9)
        Dim objMaterialPromotionPeriod As MaterialPromotionPeriod = New MaterialPromotionPeriodFacade(User).Retrieve(CType(ddlPeriod.SelectedValue, Integer))
        sw.WriteLine("MATERIAL PROMOSI")
        sw.WriteLine("Periode Alokasi" + strTab + objMaterialPromotionPeriod.PeriodName)
        sw.WriteLine("Kode Dealer" + strTab + txtDealer.Text)

        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtDealer.Text.Trim)
        sw.WriteLine("Nama Dealer" + strTab + objDealer.DealerName)

        'refer bug 811
        'sw.WriteLine("Periode Alokasi" + strTab + "Kode Dealer" + strTab + "Nama Dealer" + strTab + "Kode Barang" + strTab + "Nama Barang" + strTab + "Jumlah Barang")
        sw.WriteLine("Kode Barang" + strTab + "Nama Barang" + strTab + "Jumlah Barang")

        For Each item As DataGridItem In dgAlokasi.Items
            Dim lblDealerGet As Label = CType(item.FindControl("lblDealer"), Label)
            Dim lblGoodsNoGet As Label = CType(item.FindControl("lblGoodsNo"), Label)
            Dim lblGoodsNameGet As Label = CType(item.FindControl("lblGoodsName"), Label)
            Dim lblStockGet As Label = CType(item.FindControl("lblStock"), Label)
            Dim txtAlokasiGet As TextBox = CType(item.FindControl("txtAlokasi"), TextBox)


            Dim strDealerCode = lblDealerGet.Text
            Dim strMaterialCode = lblGoodsNoGet.Text
            Dim strMaterialName = lblGoodsNameGet.Text
            Dim strStock = lblStockGet.Text
            Dim strQty = txtAlokasiGet.Text

            'Dim objDealer As Dealer = New DealerFacade(User).Retrieve(strDealerCode)
            Dim strDealerName As String = objDealer.DealerName


            Dim strLine As String = String.Empty
            Dim strStartPeriod As String = String.Empty
            Dim strEndPeriod As String = String.Empty
            If Not objMaterialPromotionPeriod Is Nothing Then
                strStartPeriod = objMaterialPromotionPeriod.StartDate.ToString("dd/MM/yyyy")
                strEndPeriod = objMaterialPromotionPeriod.EndDate.ToString("dd/MM/yyyy")
            End If

            'remark by ery (refer bug 811)
            'strLine += objMaterialPromotionPeriod.PeriodName + strTab 'strStartPeriod + strTab + strEndPeriod + strTab
            'strLine += strDealerCode + strTab + strDealerName + strTab + strMaterialCode + strTab + strMaterialName + strTab + strQty
            strLine += strMaterialCode + strTab + strMaterialName + strTab + strQty

            sw.WriteLine(strLine)
            i = i + 1
        Next
        sw.WriteLine("")
        sw.WriteLine("Catatan :")
        sw.WriteLine("1" + strTab + "Jadwal Pengambilan Barang" + strTab + "Setiap hari kerja ( Senin ~ Jumat )")
        sw.WriteLine("2" + strTab + "Waktu" + strTab + "09:00 ~ 16.00 Wib")
        ' Modified by Ikhsan, 21 Agustus 2008
        ' Requested by Rina, as part Of CR
        ' To modify Dowloaded file in excel 
        ' Start----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        'sw.WriteLine("3" + strTab + "Tanggal" + strTab + "15 ~ 25 Setiap bulan")
        'sw.WriteLine("4" + strTab + "Tempat" + strTab + "Gudang Promosi KTB")
        'sw.WriteLine("5" + strTab + "Telephone" + strTab + "4891608 Ext. 1166")
        'sw.WriteLine("6" + strTab + "Syarat Pengambilan Barang" + strTab + "Konfirmasi Alokasi Bulanan dan surat penunjukan (PIC) dari dealer")
        'sw.WriteLine("7" + strTab + "Barang yang tidak diambil sampai dengan tanggal 25 akan menjadi stock KTB atau dialihkan kepada dealer lain" + strTab + "" + strTab + "" + strTab + "" + strTab + "Jakarta ..............., 2007")
        'sw.WriteLine("" + strTab + "Kecuali untuk dealer yg tidak mempunyai perwakilan di Jakarta")

        sw.WriteLine("3" + strTab + "Tempat" + strTab + "Gudang Promosi MMKSI")
        sw.WriteLine("4" + strTab + "Telephone" + strTab + "4891608 Ext. 1166")
        sw.WriteLine("5" + strTab + "Maksimum Tanggal pengambilan alokasi bulanan adalah tanggal 20 setiap")
        sw.WriteLine(" " + strTab + "bulannya, apabila lewat dari tanggal tsb material tsb akan menjadi")
        sw.WriteLine(" " + strTab + "stock MMKSI kembali dan dapat dialokasikan kepada dealer lain.")
        sw.WriteLine("")
        sw.WriteLine("" + strTab + "Catatan :")
        sw.WriteLine("" + strTab + "Jadwal input alokasi bulanan 7 ~ 12 setiap bulannya")
        sw.WriteLine("" + strTab + "Jadwal informasi alokasi ke dealer paling lambat ")
        sw.WriteLine("" + strTab + "tanggal 13 setiap bulannya ( Sudah dapat dilihat di D-Net )")
        sw.WriteLine("")
        ' End------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        ' Modified by Ikhsan, 21 Agustus 2008
        ' Requested by Rina, as part Of CR
        ' To modify Dowloaded file in excel 
        ' Start----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        'sw.WriteLine("" + strTab + "" + strTab + "" + strTab + "" + strTab + "" + strTab + "Diketahui Oleh" + strTab + "Diterima Oleh" + strTab + "Diserahkan Oleh")
        'sw.WriteLine("")
        'sw.WriteLine("")
        'sw.WriteLine("")
        'sw.WriteLine("")
        'sw.WriteLine("" + strTab + "" + strTab + "" + strTab + "" + strTab + "" + strTab + "Pimpinan Dealer" + strTab + "PIC Dealer" + strTab + "Staff Gudang Promosi")


        sw.WriteLine("Jakarta ..............., " + Date.Now.Year.ToString)
        sw.WriteLine("")
        sw.WriteLine("Diketahui Oleh" + strTab + "Diterima Oleh                    Diserahkan Oleh")
        sw.WriteLine("")
        sw.WriteLine("")
        sw.WriteLine("")
        sw.WriteLine("")
        sw.WriteLine("Pimpinan Dealer" + strTab + "PIC Dealer                      Staff Gudang Promosi")

        ' Modified by Ikhsan, 21 Agustus 2008
        ' Requested by Rina, as part Of CR
        ' To modify Dowloaded file in excel 
        ' Start----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        'sw.WriteLine("" + strTab + "Catatan :")
        'sw.WriteLine("" + strTab + "Jadwal input alokasi bulanan 7 ~ 12 setiap bulannya")
        'sw.WriteLine("" + strTab + "Jadwal informasi alokasi ke dealer paling lambat tanggal 13 setiap bulannya ( Sudah dapat dilihat di D-Net )")
        ' End------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        If (success = True) Then
            sw.Close()
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("DownloadAllocMatPromotion") & "\" & filename
            imp.StopImpersonate()
            imp = Nothing
            Response.Redirect("../Download.aspx?file=" & PathFile)
        Else
            MessageBox.Show("Download file Material Promotion Allocation gagal")
        End If
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click

        Button1.Visible = False
        If ddlPeriod.SelectedIndex = 0 Or txtDealer.Text = "" Then
            MessageBox.Show("Masukkan Data Dealer dan Periode Terlebih Dahulu")
            Return
        End If
        _IsCari = True
        'btnDownload.Visible = False
        btnSimpan.Visible = False
        dgAlokasi.Columns(dgAlokasi.Columns.Count - 1).Visible = False
        BtnGetData_Click(Nothing, Nothing)
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If dgAlokasi.Items.Count = 0 Then
            MessageBox.Show("Tidak Ada Data")
            Exit Sub
        End If

        btnSimpan_Click(Me, System.EventArgs.Empty)

    End Sub
#End Region

    'Private Function PagingArraylist(ByVal ArlToPage As ArrayList, ByVal IdxPage As Integer, ByVal PageSize As Integer, ByVal SortColumn As String, ByVal SortDirection As Sort.SortDirection) As ArrayList


    '    If SortColumn = "Dealer.DealerCode" Then
    '        CommonFunction.SortArraylist(ArlToPage, GetType(MaterialPromotionAllocation), "MaterialPromotion.GoodNo", SortDirection.ASC)

    '    ElseIf SortColumn = "MaterialPromotion.GoodNo" Or SortColumn = "MaterialPromotion.Name" Then
    '        CommonFunction.SortArraylist(ArlToPage, GetType(MaterialPromotionAllocation), "Dealer.DealerCode", SortDirection.ASC)
    '    End If

    '    ArlToPage = CommonFunction.SortArraylist(ArlToPage, GetType(MaterialPromotionAllocation), SortColumn, SortDirection)

    '    ArlToPage = CommonFunction.PageArraylist(ArlToPage, IdxPage, dgAlokasi.PageSize)

    '    Return ArlToPage

    'End Function
#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionViewAllocation_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Material Promosi - Alokasi Material Promosi")
        End If
    End Sub

    Private Function CheckCreatePriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionCreateJumlahAllocation_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckDownloadPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.MaterialPromotionDownloadAllocation_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region





End Class
