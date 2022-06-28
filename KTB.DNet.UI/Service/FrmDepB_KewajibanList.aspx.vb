Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.IndentPartEquipment

Public Class FrmDepB_KewajibanList
    Inherits System.Web.UI.Page



#Region "Private Variables"

    Private sessHelper As New SessionHelper
    Dim _input_daftar_kewajiban_Privilege As Boolean = False
    Dim _transfer_daftar_kewajiban_Privilege As Boolean = False
    Dim _estimasi_daftar_kewajiban_Privilege As Boolean = False
#End Region

#Region "Private Variables"

    Private Sub InitiateAuthorization()
        Dim objDealer As Dealer = Me.sessHelper.GetSession("DEALER")
        Dim _lihat_daftar_kewajiban_Privilege As Boolean = False
        _lihat_daftar_kewajiban_Privilege = SecurityProvider.Authorize(Context.User, SR.lihat_daftar_kewajiban_Privilege)

        If Not _lihat_daftar_kewajiban_Privilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sparepart - Daftar Kewajiban")
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            _input_daftar_kewajiban_Privilege = SecurityProvider.Authorize(Context.User, SR.input_daftar_kewajiban_Privilege)
            _transfer_daftar_kewajiban_Privilege = SecurityProvider.Authorize(Context.User, SR.transfer_daftar_kewajiban_Privilege)

            If Not _transfer_daftar_kewajiban_Privilege Then
                btnTransfer.Visible = False
            End If
            btnEstimasi.Visible = False

        Else
            _estimasi_daftar_kewajiban_Privilege = SecurityProvider.Authorize(Context.User, SR.estimasi_daftar_kewajiban_Privilege)

            If Not _estimasi_daftar_kewajiban_Privilege Then
                btnEstimasi.Visible = False
            End If

            btnTransfer.Visible = False

        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            Initialize()
            If GetSessionCriteria() Then
                BindDataGrid(dgDaftarKewajiban.CurrentPageIndex)
            End If
            'ViewState("currentSortColumn") = "Dealer.DealerCode"
            'ViewState("currentSortDirection") = Sort.SortDirection.ASC
        End If
        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
        If Not (objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            btnEstimasi.Visible = False
            btnBatalEstimasi.Visible = False
        End If
        If Not (objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            btnTransfer.Visible = False
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        dgDaftarKewajiban.CurrentPageIndex = 0
        BindDataGrid(dgDaftarKewajiban.CurrentPageIndex)
    End Sub

    Private Sub dgDaftarKewajiban_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgDaftarKewajiban.ItemCommand
        Select Case e.CommandName.ToLower
            Case "edit"
                SetSessionCriteria()
                sessHelper.SetSession("DepositBKewajibanHeaderID", e.Item.Cells(0).Text)
                sessHelper.SetSession("DepositBKewajibanHeaderMode", "edit")
                Response.Redirect("FrmDepB_KewajibanInput.aspx")
            Case "detail"
                SetSessionCriteria()
                sessHelper.SetSession("DepositBKewajibanHeaderID", e.Item.Cells(0).Text)
                sessHelper.SetSession("DepositBKewajibanHeaderMode", "view")
                Response.Redirect("FrmDepB_KewajibanInput.aspx")
            Case "delete"
                DeleteKewajiban(CInt(e.CommandArgument)) 'Notyet
        End Select
    End Sub

    Private Sub dgDaftarKewajiban_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgDaftarKewajiban.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = CType(e.Item.DataItem, DepositBKewajibanHeader)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarKewajiban.CurrentPageIndex * dgDaftarKewajiban.PageSize)

            Dim lblQty As Label = CType(e.Item.FindControl("lblQty"), Label)
            Dim lblTotalHarga As Label = CType(e.Item.FindControl("lblTotalHarga"), Label)
            Dim lblTipeKewajiban As Label = CType(e.Item.FindControl("lblTipeKewajiban"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblPpn As Label = CType(e.Item.FindControl("lblPpn"), Label)

            Dim strValue As String = GetTotal(objDepositBKewajibanHeader)
            Dim val As String() = strValue.Split(";")

            lblQty.Text = FormatNumber(val(0))
            lblTotalHarga.Text = FormatNumber(val(1))
            lblTipeKewajiban.Text = DepositBEnum.GetStringValueKewajiban(objDepositBKewajibanHeader.TipeKewajiban)
            lblStatus.Text = DepositBEnum.GetStringValueStatusPengajuan(objDepositBKewajibanHeader.Status)
            'lblPpn.Text = FormatNumber(val(1) * 0.1)
            lblPpn.Text = FormatNumber(val(2))

            Dim lbnEdit As LinkButton = CType(e.Item.FindControl("lbnEdit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            lbtnDelete.Attributes.Add("onclick", "return confirm('Apakah anda akan menghapus data ini ?');")
            lbtnDelete.CommandArgument = objDepositBKewajibanHeader.ID.ToString

            Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            If (objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                lbnEdit.Visible = False
                lbtnDelete.Visible = False
            Else
                If objDepositBKewajibanHeader.Status <> DepositBEnum.StatusPengajuan.Baru Then
                    lbnEdit.Visible = False
                    lbtnDelete.Visible = False
                End If
                If Not _input_daftar_kewajiban_Privilege Then
                    lbnEdit.Visible = False
                    lbtnDelete.Visible = False
                End If

                '----start add by rudi 2018/01/31
                If objDepositBKewajibanHeader.Status = DepositBEnum.StatusPengajuan.Transfer AndAlso _
                    objDepositBKewajibanHeader.TipeKewajiban = DepositBEnum.TipeKewajiban.NonReguler Then
                    lbtnDelete.Visible = True
                End If
                '----end add by rudi

            End If
        End If
    End Sub

    Private Sub dgDaftarKewajiban_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgDaftarKewajiban.PageIndexChanged
        dgDaftarKewajiban.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgDaftarKewajiban.CurrentPageIndex)
    End Sub

    Private Sub dgDaftarKewajiban_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgDaftarKewajiban.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgDaftarKewajiban.SelectedIndex = -1
        dgDaftarKewajiban.CurrentPageIndex = 0
        BindDataGrid(dgDaftarKewajiban.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom Method"

  
    Private Sub Initialize()
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")

        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtKodeDealer.Attributes.Remove("readonly")
        Else
            txtKodeDealer.Attributes.Add("readonly", "readonly")
            txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
        End If

        BindTipeKewajiban()

        'Load Tahun
        BindYear()

        'Get Status
        BindStatus()
    End Sub

    Private Sub BindTipeKewajiban()
        Dim items As New ArrayList
        Dim _arrStatus As ArrayList = New DepositBEnum().RetrieveTipeKewajiban()
        For Each item As EnumProperty In _arrStatus
            items.Add(item)
        Next

        ddlKewajiban.DataSource = items
        ddlKewajiban.DataTextField = "NameType"
        ddlKewajiban.DataValueField = "ValType"
        ddlKewajiban.DataBind()
    End Sub

    Private Sub BindYear()
        Dim curYear As Integer = Date.Now.Year
        Dim startYear As Integer = curYear - 5
        Dim EndYear As Integer = curYear + 5
        Dim intYear As Integer = 0
        ddlYear.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For intYear = startYear To EndYear
            ddlYear.Items.Add(New ListItem(intYear.ToString, intYear.ToString))
        Next
        ddlYear.Items.FindByValue(Date.Now.Year.ToString).Selected = True
    End Sub

    Private Sub BindStatus()
        ddlStatus.Items.Clear()
        Dim items As New ArrayList
        Dim _arrStatus As ArrayList = New DepositBEnum().RetrieveStatusPengajuan(True)
        For Each item As EnumProperty In _arrStatus
            items.Add(item)
        Next
        ddlStatus.DataSource = items
        ddlStatus.DataTextField = "NameType"
        ddlStatus.DataValueField = "ValType"
        ddlStatus.DataBind()
    End Sub

    Private Sub BindDataGrid(ByVal pageIndex As Integer)
        Dim totalRow As Integer = 0
        Dim arlDepositBKewajiban As ArrayList

        Dim oDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If txtKodeDealer.Text.Trim = String.Empty Then
                MessageBox.Show("Tentukan kode dealer terlebih dahulu")
                Exit Sub
            End If
        End If
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If
        If (txtNoReg.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "NoRegKewajiban", MatchType.Partial, txtNoReg.Text.Trim))
        End If
        If (txtNoSO.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "NoSalesorder", MatchType.Partial, txtNoSO.Text.Trim))
        End If
        If CInt(ddlStatus.SelectedIndex) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "Status", MatchType.Exact, CInt(ddlStatus.SelectedValue)))
        End If
        If CInt(ddlKewajiban.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "TipeKewajiban", MatchType.Exact, CInt(ddlKewajiban.SelectedValue)))
        End If
        If CInt(ddlYear.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "PeriodYear", MatchType.Exact, CInt(ddlYear.SelectedValue)))
        End If
        If (chkTglpembuatan.Checked) Then
            Dim createdDateFrom As DateTime = New DateTime(icCreateDate.Value.Year, icCreateDate.Value.Month, icCreateDate.Value.Day, 0, 0, 0)
            Dim createdDateTo As DateTime = New DateTime(icCreateDate.Value.Year, icCreateDate.Value.Month, icCreateDate.Value.Day, 23, 59, 59)

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "CreatedTime", MatchType.GreaterOrEqual, Format(createdDateFrom, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "CreatedTime", MatchType.LesserOrEqual, Format(createdDateTo, "yyyy-MM-dd HH:mm:ss")))
        End If

        arlDepositBKewajiban = New DepositBKewajibanHeaderFacade(User).RetrieveActiveList(criterias, pageIndex + 1, dgDaftarKewajiban.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))

        If (arlDepositBKewajiban.Count > 0) Then
            dgDaftarKewajiban.Visible = True
            dgDaftarKewajiban.DataSource = arlDepositBKewajiban
            dgDaftarKewajiban.VirtualItemCount = totalRow
            dgDaftarKewajiban.DataBind()
        Else
            dgDaftarKewajiban.Visible = False

            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Private Function GetTotal(ByVal obj As DepositBKewajibanHeader) As String
        Dim vRet As String = String.Empty
        Dim vQty As Decimal = 0
        Dim vPrice As Decimal = 0
        Dim vTax As Decimal = 0
        For Each detail As DepositBKewajibanDetail In obj.DepositBKewajibanDetails
            vQty = detail.Qty + vQty
            vPrice = detail.Harga + vPrice
            vTax = detail.Tax + vTax
        Next
        vRet = vQty.ToString() + ";" + vPrice.ToString() + ";" + vTax.ToString()
        Return vRet
    End Function

    Private Sub DeleteKewajiban(ByVal nID As Integer)
        '----start add by rudi 2018/01/31
        Try
            Dim blnIsOrder As Boolean = False
            Dim arlEstimation As ArrayList
            Dim arlEstEquipPO As ArrayList
            Dim objEstEquipPO As EstimationEquipPO
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "Status", MatchType.No, CType(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru, Short)))
            criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "DepositBKewajibanHeader.ID", MatchType.Exact, nID))
            arlEstimation = New EstimationEquipHeaderFacade(User).Retrieve(criterias)
            If arlEstimation.Count > 0 Then
                For Each objEstimation As EstimationEquipHeader In arlEstimation
                    If objEstimation.ID > 0 Then
                        For Each objEstEquipDetail As EstimationEquipDetail In objEstimation.EstimationEquipDetails
                            arlEstEquipPO = objEstEquipDetail.EstimationEquipPO
                            If arlEstEquipPO.Count > 0 Then
                                objEstEquipPO = CType(arlEstEquipPO(0), EstimationEquipPO)
                                If objEstEquipPO.ID > 0 Then
                                    If Not objEstEquipPO.IndentPartDetail Is Nothing Then
                                        blnIsOrder = True
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                        If blnIsOrder = True Then
                            Exit For
                        End If
                    End If
                Next
            End If
            If blnIsOrder = True Then
                MessageBox.Show("Hapus data gagal. Estimasi sudah diajukan order.")
                Exit Sub
            End If
            '----end add by rudi

            Dim objFacade As DepositBKewajibanHeaderFacade = New DepositBKewajibanHeaderFacade(User)
            Dim objDomain As DepositBKewajibanHeader = objFacade.Retrieve(nID)
            objFacade.Delete(objDomain)
            MessageBox.Show(SR.DeleteSucces)
            dgDaftarKewajiban.CurrentPageIndex = 0
            BindDataGrid(dgDaftarKewajiban.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
            dgDaftarKewajiban.SelectedIndex = -1
        End Try
    End Sub

    Private Sub SetSessionCriteria()
        Dim arrSession As ArrayList = New ArrayList
        arrSession.Add(txtKodeDealer.Text.Trim) '0
        arrSession.Add(ddlKewajiban.SelectedValue) '1
        arrSession.Add(ddlYear.SelectedValue) '2
        arrSession.Add(ddlStatus.SelectedValue) '3
        arrSession.Add(txtNoReg.Text) '4
        arrSession.Add(dgDaftarKewajiban.CurrentPageIndex) '5
        arrSession.Add(CType(ViewState("CurrentSortColumn"), String)) '6
        arrSession.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection)) '7
        sessHelper.SetSession("ArrSessionKewajibanList", arrSession)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim arrSession As ArrayList = sessHelper.GetSession("ArrSessionKewajibanList")
        If Not arrSession Is Nothing Then
            txtKodeDealer.Text = arrSession.Item(0)
            ddlKewajiban.SelectedValue = arrSession.Item(1)
            ddlYear.SelectedValue = arrSession.Item(2)
            ddlStatus.SelectedValue = arrSession.Item(3)
            txtNoReg.Text = arrSession.Item(4)
            dgDaftarKewajiban.CurrentPageIndex = CType(arrSession.Item(5), Integer)
            ViewState("CurrentSortColumn") = arrSession.Item(6)
            ViewState("CurrentSortDirect") = arrSession.Item(7)
            Return True
        End If
        Return False
    End Function

    Private Sub CreateTextFile(ByVal arl As ArrayList)
        Dim _fileHelper As New KTB.DNET.UI.Helper.FileHelper
        Dim fileInfo As System.IO.FileInfo
        Try
            fileInfo = _fileHelper.TransferKewajibanDepositBToSAP(arl)
            MessageBox.Show(SR.UploadSucces(fileInfo.Name))
        Catch ex As Exception
            MessageBox.Show(SR.UploadFail(fileInfo.Name))
        End Try
    End Sub
#End Region

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        Dim cntFalse As Integer = 0
        Dim item As DataGridItem
        Dim arlChecked As ArrayList = New ArrayList
        Dim objDomain As DepositBKewajibanHeader
        Dim objFacade As DepositBKewajibanHeaderFacade = New DepositBKewajibanHeaderFacade(User)
        For Each item In Me.dgDaftarKewajiban.Items
            If CType(item.FindControl("chkItem"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                objDomain = objFacade.Retrieve(id)
                If Not objDomain Is Nothing Then
                    If objDomain.TipeKewajiban = DepositBEnum.TipeKewajiban.Regular AndAlso _
                        objDomain.Status = DepositBEnum.StatusPengajuan.Baru Then
                        arlChecked.Add(objDomain)
                    Else
                        cntFalse = cntFalse + 1
                    End If
                End If
            End If
        Next
        If cntFalse > 0 Then
            MessageBox.Show("Data yang ditransfer hanya untuk tipe kewajiban REGULER dan status Belum Diajukan")
            Return
        End If

        'Process
        If arlChecked.Count > 0 Then
            CreateTextFile(arlChecked)
            'Update kewajiban jadi Transfer
            For Each objDepositBKewajibanHeader As DepositBKewajibanHeader In arlChecked
                Dim vUpdateResult As Integer
                objDepositBKewajibanHeader.Status = DepositBEnum.StatusPengajuan.Transfer
                vUpdateResult = New DepositBKewajibanHeaderFacade(User).Update(objDepositBKewajibanHeader)
                If vUpdateResult <> -1 Then
                    Dim objHistFacade As DepositBStatusHistoryFacade = New DepositBStatusHistoryFacade(User)
                    objHistFacade.SaveHistoricalPengajuan(DepositBEnum.StatusType.Kewajiban, objDepositBKewajibanHeader.ID, DepositBEnum.StatusPengajuan.Transfer, DepositBEnum.StatusPengajuan.Baru)
                End If
            Next
            BindDataGrid(dgDaftarKewajiban.CurrentPageIndex)
        Else
            MessageBox.Show("Tidak ada data untuk Transfer Ulang")
        End If
    End Sub

    Private Sub btnEstimasi_Click(sender As Object, e As EventArgs) Handles btnEstimasi.Click
        
        Dim item As DataGridItem
        Dim arlChecked As ArrayList = New ArrayList
        Dim objDomain As DepositBKewajibanHeader
        Dim objFacade As DepositBKewajibanHeaderFacade = New DepositBKewajibanHeaderFacade(User)
        Dim cntFalse As Integer = 0
        For Each item In Me.dgDaftarKewajiban.Items
            If ((item.ItemType = ListItemType.Item) Or (item.ItemType = ListItemType.AlternatingItem)) Then
                If CType(item.FindControl("chkItem"), CheckBox).Checked Then
                    'objDomain = CType(item.DataItem, DepositBKewajibanHeader)
                    Dim id As Integer = item.Cells(0).Text
                    objDomain = objFacade.Retrieve(id)
                    If Not objDomain Is Nothing Then
                        If objDomain.TipeKewajiban = DepositBEnum.TipeKewajiban.NonReguler AndAlso _
                            objDomain.Status = DepositBEnum.StatusPengajuan.Baru Then
                            arlChecked.Add(objDomain)
                        Else
                            cntFalse = cntFalse + 1
                        End If
                    End If
                End If
            End If
            
        Next
        If cntFalse > 0 Then
            MessageBox.Show("Data yang dibuatkan estimasi hanya untuk tipe kewajiban NON REGULER dan status Belum Diajukan")
            Return
        End If
        'Process
        If arlChecked.Count > 0 Then
            Try
                CopyToSP(arlChecked)
                BindDataGrid(dgDaftarKewajiban.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show("Buat Estimasi gagal.")
            End Try

        Else
            MessageBox.Show("Tidak ada data untuk dibuatkan estimasi")
        End If
    End Sub


    Private Sub btnBatalEstimasi_Click(sender As Object, e As EventArgs) Handles btnBatalEstimasi.Click
        Dim item As DataGridItem
        Dim arlChecked As ArrayList = New ArrayList
        Dim objDomain As DepositBKewajibanHeader
        Dim objFacade As DepositBKewajibanHeaderFacade = New DepositBKewajibanHeaderFacade(User)
        Dim cntFalse As Integer = 0
        Dim cntCek As Integer = 0
        Dim intCountNoBatalEstimasi As Integer = 0

        '-- start add by rudi
        If Me.dgDaftarKewajiban.Items.Count = 0 Then
            MessageBox.Show("Daftar Kewajiban tidak ada.")
            Return
        End If
        '-- end add by rudi

        For Each item In Me.dgDaftarKewajiban.Items
            If ((item.ItemType = ListItemType.Item) Or (item.ItemType = ListItemType.AlternatingItem)) Then
                If CType(item.FindControl("chkItem"), CheckBox).Checked Then
                    'objDomain = CType(item.DataItem, DepositBKewajibanHeader)
                    Dim id As Integer = item.Cells(0).Text
                    objDomain = objFacade.Retrieve(id)
                    If Not objDomain Is Nothing Then
                        cntCek += 1
                        If objDomain.TipeKewajiban = DepositBEnum.TipeKewajiban.NonReguler AndAlso _
                            objDomain.Status = DepositBEnum.StatusPengajuan.Transfer Then
                            arlChecked.Add(objDomain)
                        Else
                            cntFalse = cntFalse + 1
                        End If

                        '-- start add by rudi
                        Dim arlEstimation As ArrayList
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "DepositBKewajibanHeader.ID", MatchType.Exact, objDomain.ID))
                        arlEstimation = New EstimationEquipHeaderFacade(User).Retrieve(criterias)
                        If arlEstimation.Count > 0 Then
                            For Each objEstimation As EstimationEquipHeader In arlEstimation
                                If objEstimation.ID > 0 Then
                                    If objEstimation.Status = CByte(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru) OrElse _
                                        objEstimation.Status = CByte(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim) OrElse _
                                        objEstimation.Status = CByte(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal) Then
                                    Else

                                        intCountNoBatalEstimasi += 1
                                    End If
                                End If
                            Next
                        End If
                        '-- end add by rudi

                    End If
                End If
            End If
        Next

        '-- start add by rudi
        If cntCek <= 0 Then
            MessageBox.Show("Mohon pilih salah satu Daftar Kewajiban.")
            Return
        End If
        '-- end add by rudi

        If cntFalse > 0 Then
            MessageBox.Show("Pembatalan estimasi hanya untuk tipe kewajiban NON REGULER dan status Transfer/Estimasi")
            Return
        End If

        '-- start add by rudi
        If intCountNoBatalEstimasi > 0 Then
            MessageBox.Show("Batal Estimasi hanya bisa dilakukan untuk status Estimasi baru atau kirim.")
            Return
        End If
        '-- end add by rudi

        'Process
        If arlChecked.Count > 0 Then
            Try
                BatalEstimasi(arlChecked)
                BindDataGrid(dgDaftarKewajiban.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show("Buat Estimasi gagal.")
            End Try

        Else
            MessageBox.Show("Tidak ada data untuk dibuatkan estimasi")
        End If
    End Sub

    Private Sub CopyToSP(ByVal arl As ArrayList)
        Dim arlEstimation As New ArrayList
        Dim obj As EstimationEquipHeader

        For Each item As DepositBKewajibanHeader In arl
            obj = New EstimationEquipHeader
            obj.Dealer = item.Dealer
            obj.DepositBKewajibanHeader = item
            obj.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru

            For Each itemDetail As DepositBKewajibanDetail In item.DepositBKewajibanDetails
                Dim objDetail As New EstimationEquipDetail
                objDetail.SparePartMaster = itemDetail.SparePartMaster
                objDetail.EstimationUnit = itemDetail.Qty
                objDetail.Harga = itemDetail.Harga
                objDetail.Discount = 0

                obj.EstimationEquipDetails.Add(objDetail)
            Next
            
            arlEstimation.Add(obj)
        Next

        If arlEstimation.Count > 0 Then
            'Save to EstimationEquipHeader
            Dim objKewajibanHeaderFacade As DepositBKewajibanHeaderFacade = New DepositBKewajibanHeaderFacade(User)
            Dim vResult As Integer = -1
            Dim vResult2 As Integer = -1
            Dim strResult As String = ""
            Dim strMessageKirim As String = ""
            For Each item As EstimationEquipHeader In arlEstimation
                vResult = New KTB.DNet.BusinessFacade.IndentPartEquipment.EstimationEquipHeaderFacade(User).InsertEstimationEquipHeader(item, item.EstimationEquipDetails)
                If vResult = -1 Then
                    If strResult.Trim = "" Then
                        strResult = strResult + item.DepositBKewajibanHeader.NoRegKewajiban
                    Else
                        strResult = strResult + "," + item.DepositBKewajibanHeader.NoRegKewajiban
                    End If
                Else
                    'Update kewajiban jadi Transfer
                    Dim objKewajiban As DepositBKewajibanHeader = item.DepositBKewajibanHeader
                    If Not IsNothing(objKewajiban) Then
                        Dim vUpdateResult As Integer
                        objKewajiban.Status = DepositBEnum.StatusPengajuan.Transfer
                        vUpdateResult = objKewajibanHeaderFacade.Update(objKewajiban)

                        Dim objHistFacade As DepositBStatusHistoryFacade = New DepositBStatusHistoryFacade(User)
                        objHistFacade.SaveHistoricalPengajuan(DepositBEnum.StatusType.Kewajiban, objKewajiban.ID, DepositBEnum.StatusPengajuan.Transfer, DepositBEnum.StatusPengajuan.Baru)
                    End If

                    '2017-12-07 Add coding for update status from "Baru" to "Kirim" (ByPass to status "Kirim") 
                    vResult2 = UpdateStatusEstimasiEquip(item)
                    '----------------------------------------
                End If
            Next

            If arlEstimation.Count > 0 Then
                If strResult <> "" Then
                    MessageBox.Show("Daftar kewajiban, dengan nomor registrasi : " & strResult & ", gagal dibuatkan estimasi.")
                End If
                If vResult2 = 1 Then
                    strMessageKirim = "\n Ubah status kirim berhasil."
                End If
                If arlEstimation.Count > 1 Then
                    MessageBox.Show("Berhasil dibuatkan data estimasi. " + strMessageKirim)
                Else
                    Dim objFacade As New KTB.DNet.BusinessFacade.IndentPartEquipment.EstimationEquipHeaderFacade(User)
                    Dim objEstimationEquipHeader As EstimationEquipHeader = objFacade.Retrieve(vResult)
                    If Not objEstimationEquipHeader Is Nothing Then
                        MessageBox.Show("Berhasil dibuatkan estimasi. " + strMessageKirim + " \n Dengan No Pengajuan : " + objEstimationEquipHeader.EstimationNumber)
                    End If
                End If
            End If

        End If
    End Sub

    Private Function UpdateStatusEstimasiEquip(ByVal objEstimationEquipHeader As EstimationEquipHeader) As Integer
        'only for dealer
        Dim intUpdateStatus As Integer = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim
        Dim intSuksesKirim As Integer = 0

        Try
            Dim hf As EstimationEquipHeaderFacade = New EstimationEquipHeaderFacade(User)
            Dim oDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                Dim oldStatus As Integer = objEstimationEquipHeader.Status
                objEstimationEquipHeader.Status = intUpdateStatus
                objEstimationEquipHeader.CreatedTime = DateTime.Now
                Dim result As Integer = hf.Update(objEstimationEquipHeader)
                If objEstimationEquipHeader.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim Then
                    Dim objFrm As New FrmEstimationEquipment
                    objFrm.SendEmailEstimasi(objEstimationEquipHeader)
                End If
                hf.RecordStatusChangeHistory(objEstimationEquipHeader, oldStatus)
                intSuksesKirim = 1
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return intSuksesKirim
    End Function

    Private Sub BatalEstimasi(ByVal arl As ArrayList)
        Dim strMessage As String = ""
        Dim vResult As Integer = -1
        'Dim objEstimation As EstimationEquipHeader
        Dim arlEstimation As New ArrayList

        '-- start add by rudi
        Dim intCountBatalEstimasi As Integer = 0
        Dim strEstimationNumberList As String = String.Empty
        '-- end add by rudi

        Try
            For Each item As DepositBKewajibanHeader In arl
                intCountBatalEstimasi = 0
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "DepositBKewajibanHeader.ID", MatchType.Exact, item.ID))
                arlEstimation = New KTB.DNet.BusinessFacade.IndentPartEquipment.EstimationEquipHeaderFacade(User).Retrieve(criterias)
                If arlEstimation.Count > 0 Then
                    'objEstimation = CType(arlEstimation(0), EstimationEquipHeader)

                    '-- start add by rudi
                    For Each objEstimation As EstimationEquipHeader In arlEstimation
                        If objEstimation.ID > 0 Then
                            If objEstimation.Status = CByte(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru) OrElse _
                                objEstimation.Status = CByte(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim) OrElse _
                                objEstimation.Status = CByte(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal) Then

                                If objEstimation.Status = CByte(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru) OrElse _
                                    objEstimation.Status = CByte(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Kirim) Then
                                    If strEstimationNumberList = "" Then
                                        strEstimationNumberList = " - " + objEstimation.EstimationNumber
                                    Else
                                        strEstimationNumberList = strEstimationNumberList + "\n - " + objEstimation.EstimationNumber
                                    End If

                                    objEstimation.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal
                                    vResult = New KTB.DNet.BusinessFacade.IndentPartEquipment.EstimationEquipHeaderFacade(User).Update(objEstimation)
                                End If
                                intCountBatalEstimasi += 1
                                '-- end add by rudi

                                'item.Status = DepositBEnum.StatusPengajuan.Baru
                                'vResult = New DepositBKewajibanHeaderFacade(User).Update(item)
                                'If vResult <> -1 Then
                                '    Dim objHistFacade As DepositBStatusHistoryFacade = New DepositBStatusHistoryFacade(User)
                                '    objHistFacade.SaveHistoricalPengajuan(DepositBEnum.StatusType.Kewajiban, item.ID, DepositBEnum.StatusPengajuan.Baru, DepositBEnum.StatusPengajuan.Transfer)
                                'End If

                                'MessageBox.Show("Pengajuan Estimasi dengan nomor : " & objEstimation.EstimationNumber & " berhasil dibatalkan.")
                                'Else
                                '    Dim desc As New EnumEstimationEquipStatus
                                '    MessageBox.Show("Pengajuan Estimasi dengan nomor : " & objEstimation.EstimationNumber & " tidak bisa dibatalkan. \n Status estimasi : " & desc.GetEstimationEquipStatusHeader(objEstimation.Status))
                            End If
                        End If
                    Next
                    '-- end add by rudi
                End If

                '-- start add by rudi
                If intCountBatalEstimasi > 0 Then
                    If intCountBatalEstimasi = arlEstimation.Count Then
                        item.Status = DepositBEnum.StatusPengajuan.Baru
                        vResult = New DepositBKewajibanHeaderFacade(User).Update(item)
                        If vResult <> -1 Then
                            Dim objHistFacade As DepositBStatusHistoryFacade = New DepositBStatusHistoryFacade(User)
                            objHistFacade.SaveHistoricalPengajuan(DepositBEnum.StatusType.Kewajiban, item.ID, DepositBEnum.StatusPengajuan.Baru, DepositBEnum.StatusPengajuan.Transfer)
                        Else
                            MessageBox.Show("Pengajuan Estimasi dengan Nomor Register Kewajiban : " & item.NoRegKewajiban & " gagal dibatalkan.")
                        End If
                    End If
                End If
            Next
            If strEstimationNumberList.Trim <> "" Then
                MessageBox.Show("Pengajuan Estimasi dengan nomor : \n" & strEstimationNumberList & " \n berhasil dibatalkan.")
            End If
            '-- end add by rudi

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

End Class