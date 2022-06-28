Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.BusinessFacade.IndentPart
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessValidation.Helpers

Public Class FrmDepB_PengajuanPencairan
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim sessHelper As SessionHelper = New SessionHelper
    Dim oDealer As Dealer
    'Dim objPencairan As DataTable
    'Dim dtPengajuan As New DataTable("dt_PengajuanDepositB")
    'Dim gridEdited As Boolean = False

    Private TotalAmount As Double = 0
#End Region
    Dim _input_pengajuan_pencairan_Privilege As Boolean = False
    Dim _validasi_pengajuan_pencairan_Privilege As Boolean = False
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

#Region "Events"

    Private Sub InitiateAuthorization()
        Dim objDealer As Dealer = Me.sessHelper.GetSession("DEALER")

        _input_pengajuan_pencairan_Privilege = SecurityProvider.Authorize(Context.User, SR.input_pengajuan_pencairan_Privilege)
        _validasi_pengajuan_pencairan_Privilege = SecurityProvider.Authorize(Context.User, SR.validasi_pengajuan_pencairan_Privilege)

        If Not _input_pengajuan_pencairan_Privilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sparepart - Pengajuan Pencairan")
        End If

        If Not _validasi_pengajuan_pencairan_Privilege Then
            btnValidasi.Visible = False

        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then

            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sparepart - Pengajuan Pencairan")
        Else
            PopUpExpired()

        End If

    End Sub

    Private Sub PopUpExpired()
        Dim objDealer As Dealer = Me.sessHelper.GetSession("DEALER")
        Dim fcdIndenPartH As IndentPartHeaderFacade = New IndentPartHeaderFacade(User)
        Dim objIndentPartH As IndentPartHeader
        Dim sts As String = "'0','2'"
        Dim arrMess As String = ""
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(v_EquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(v_EquipPO), "PaymentType", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(v_EquipPO), "DealerCode", MatchType.Exact, objDealer.DealerCode))
        criterias.opAnd(New Criteria(GetType(v_EquipPO), "Status", MatchType.InSet, "(" & sts & ")"))

        Dim arlEquipPo As ArrayList = New v_EquipPOFacade(User).Retrieve(criterias)
        If Not IsNothing(arlEquipPo) AndAlso arlEquipPo.Count > 0 Then
            For Each dataExp As v_EquipPO In arlEquipPo
                If dataExp.CreatedTime.AddDays(14) > Date.Now Then
                    objIndentPartH = fcdIndenPartH.Retrieve(dataExp.ID)
                    If Not IsNothing(objIndentPartH) AndAlso objIndentPartH.PaymentType = EnumIndentPartStatus.IndentPartPaymentType.Deposit_B _
                        AndAlso (objIndentPartH.Status = EnumIndentPartEquipStatus.EnumStatusDealer.Baru Or objIndentPartH.Status = EnumIndentPartEquipStatus.EnumStatusDealer.Kirim) Then
                        'MessageBox.Show(" Silahkan melakukan pencairan deposit B untuk pengajuan part: <xxxxxx>!")
                        
                        If arrMess.Trim.Length = 0 Then
                            arrMess = "Silahkan melakukan pencairan deposit B untuk pengajuan part :\nNo Pengajuan | No Estimasi\n"
                        End If
                        arrMess = arrMess & String.Format("{0} | {1}\n", dataExp.RequestNo, dataExp.EstimationNumber)

                    End If
                End If
            Next
        End If
        If arrMess.Trim.Length > 0 Then
            MessageBox.Show(arrMess)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        If Not IsPostBack Then
            Me.hdnMCPConfirmation.Value = "-1"
            Initialize()
            btnSave.Attributes.Add("onclick", "return confirm('Anda yakin menyimpan pencairan ini?');")
            btnValidasi.Enabled = False

        Else
            If Me.hdnMCPConfirmation.Value = "1" Then
                SimpanPengajuanPencairan()
            End If
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Validation() Then
            SimpanPengajuanPencairan()
        End If
    End Sub

    Private Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        Dim objDepositBPencairanHeader As DepositBPencairanHeader = CType(sessHelper.GetSession("sessDepositeBPencairanHeader"), DepositBPencairanHeader)
        If Not IsNothing(objDepositBPencairanHeader) Then
            objDepositBPencairanHeader.Status = DepositBEnum.StatusPencairan.Validasi
            Dim objFacade As DepositBPencairanHeaderFacade = New DepositBPencairanHeaderFacade(User)
            Dim intResult As Integer = objFacade.Update(objDepositBPencairanHeader)
            If intResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                'Dim objDomain As DepositBPencairanHeader = objFacade.Retrieve(objDepositBPencairanHeader.ID)
                Dim objHistFacade As DepositBStatusHistoryFacade = New DepositBStatusHistoryFacade(User)
                'If objDomain.ID > 0 Then
                objHistFacade.SaveHistoricalPengajuan(DepositBEnum.StatusType.Pencairan, objDepositBPencairanHeader.ID, DepositBEnum.StatusPencairan.Validasi, DepositBEnum.StatusPencairan.Baru)
                'End If
                MessageBox.Show(SR.UpdateSucces)
                btnValidasi.Enabled = False
                btnNew.Enabled = True
                sessHelper.RemoveSession("sessDepositeBPencairanHeader")
            End If

        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Initialize()
        SetControl(True)
        Me.btnSave.Enabled = True
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Page_Load(sender, e)
    End Sub

    Private Sub ddlTipePengajuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipePengajuan.SelectedIndexChanged
        'If ddlProductCategory.SelectedIndex > 0 Then
        BindDataPengajuan()
        'End If
    End Sub

    Private Sub ddlProductCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProductCategory.SelectedIndexChanged
        'If ddlProductCategory.SelectedIndex > 0 AndAlso ddlPeriode.SelectedIndex > 0 Then
        BindDataPengajuan()
        'End If
    End Sub

    Private Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        'If ddlProductCategory.SelectedIndex > 0 AndAlso ddlPeriode.SelectedIndex > 0 Then
        BindDataPengajuan()
        'End If
    End Sub

    Private Sub ddlPeriode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPeriode.SelectedIndexChanged
        'If ddlProductCategory.SelectedIndex > 0 AndAlso ddlPeriode.SelectedIndex > 0 Then
        BindDataPengajuan()
        'End If
    End Sub

    Private Sub dgTransfer_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgTransfer.ItemDataBound

        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgTransfer.CurrentPageIndex * dgTransfer.PageSize)

        ElseIf (e.Item.ItemType = ListItemType.Footer) Then

        End If

    End Sub

    Private Sub dgTransfer_AddCommand(ByVal e As DataGridCommandEventArgs)
        Try
            Dim _arrDepositBTransfer As ArrayList = sessHelper.GetSession("ArrDepositBTransfer")
            If IsNothing(_arrDepositBTransfer) Then
                _arrDepositBTransfer = New ArrayList
            End If

            Dim txtJumlahPencairan As TextBox = CType(e.Item.FindControl("txtJumlahPencairan"), TextBox)
            Dim txtPenjelasanEntry As TextBox = CType(e.Item.FindControl("txtPenjelasanEntry"), TextBox)

            If txtJumlahPencairan.Text.Trim = "" Then
                MessageBox.Show("Silahkan input jumlah pencairan")
                Exit Sub
            End If

            If Not IsNumeric(txtJumlahPencairan.Text.Trim) Then
                MessageBox.Show("Input jumlah pencairan harus angka")
                Exit Sub
            End If

            Dim objDetail As New DepositBTransfer
            objDetail.ID = 1
            objDetail.Amount = CDbl(txtJumlahPencairan.Text.Trim)
            objDetail.Desc = txtPenjelasanEntry.Text.Trim

            _arrDepositBTransfer.Add(objDetail)

            sessHelper.SetSession("ArrDepositBTransfer", _arrDepositBTransfer)

            BindDataTransfer()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgTransfer_EditCommand(ByVal e As DataGridCommandEventArgs)
        Try
            dgTransfer.ShowFooter = False
            dgTransfer.EditItemIndex = CInt(e.Item.ItemIndex)

            Dim _arrDepositBTransfer As ArrayList = sessHelper.GetSession("ArrDepositBTransfer")

            dgTransfer.DataSource = _arrDepositBTransfer
            dgTransfer.DataBind()
            btnSave.Enabled = False

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgTransfer_UpdateCommand(ByVal e As DataGridCommandEventArgs)
        Try
            Dim _ArrDepositBTransfer As ArrayList = CType(sessHelper.GetSession("ArrDepositBTransfer"), ArrayList)
            Dim objDetail As DepositBTransfer = New DepositBTransfer
            objDetail = CType(_ArrDepositBTransfer(e.Item.ItemIndex), DepositBTransfer)

            Dim txtJumlahPencairanEdit As TextBox = CType(e.Item.FindControl("txtJumlahPencairanEdit"), TextBox)
            Dim txtPenjelasanEntryEdit As TextBox = CType(e.Item.FindControl("txtPenjelasanEntryEdit"), TextBox)

            objDetail.Amount = CDbl(txtJumlahPencairanEdit.Text.Trim)
            objDetail.Desc = txtPenjelasanEntryEdit.Text.Trim

            sessHelper.SetSession("ArrDepositBTransfer", _ArrDepositBTransfer)

            dgTransfer.EditItemIndex = -1

            BindDataTransfer()

            dgTransfer.ShowFooter = True
            btnSave.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub dgTransfer_CancelCommand(ByVal E As DataGridCommandEventArgs)
        dgTransfer.EditItemIndex = -1

        BindDataTransfer()

        dgTransfer.ShowFooter = True
    End Sub

    Private Sub dgTransfer_DeleteCommand(ByVal e As DataGridCommandEventArgs)
        Try
            Dim _ArrDepositBTransfer As ArrayList = CType(sessHelper.GetSession("ArrDepositBTransfer"), ArrayList)
            Dim objDetail As DepositBTransfer = New DepositBTransfer
            objDetail = CType(_ArrDepositBTransfer(e.Item.ItemIndex), DepositBTransfer)

            _ArrDepositBTransfer.Remove(objDetail)
            sessHelper.SetSession("ArrDepositBTransfer", _ArrDepositBTransfer)

            BindDataTransfer()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgTransfer_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgTransfer.ItemCommand

        Select Case e.CommandName
            Case "Add"
                dgTransfer_AddCommand(e)
            Case "Edit"
                dgTransfer_EditCommand(e)
            Case "Update"
                dgTransfer_UpdateCommand(e)
            Case "Cancel"
                dgTransfer_CancelCommand(e)
            Case "Delete"
                dgTransfer_DeleteCommand(e)

        End Select

    End Sub

    Private Sub dgInterest_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgInterest.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'Dim obj As DepositBInterestHeader = CType(e.Item.DataItem, DepositBInterestHeader)
            Dim lblDescription As Label = e.Item.FindControl("lblDescription")
            lblDescription.Text = "Pencairan bunga saldo Deposit B Periode " & ddlPeriode.SelectedItem.Text & " Tahun " & ddlYear.SelectedItem.Text
        End If

    End Sub

    Private Sub dgKewajiban_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgKewajiban.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = CType(e.Item.DataItem, DepositBKewajibanHeader)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgKewajiban.CurrentPageIndex * dgKewajiban.PageSize)

            Dim lblQty As Label = CType(e.Item.FindControl("lblQty"), Label)
            Dim lblTotalHarga As Label = CType(e.Item.FindControl("lblTotalHarga"), Label) 'lblTotalHargaHide
            Dim lblTipeKewajiban As Label = CType(e.Item.FindControl("lblTipeKewajiban"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblPpn As Label = CType(e.Item.FindControl("lblPpn"), Label)

            Dim strValue As String = String.Empty
            If objDepositBKewajibanHeader.TipeKewajiban = DepositBEnum.TipeKewajiban.Regular Then
                strValue = GetAmountKewajibanReguler(objDepositBKewajibanHeader)
            End If
            If objDepositBKewajibanHeader.TipeKewajiban = DepositBEnum.TipeKewajiban.NonReguler Then
                strValue = GetAmountKewajibanNonReguler(objDepositBKewajibanHeader)
            End If

            Dim val As String() = strValue.Split(";")
            Dim totalHarga As Decimal = CDec(val(1)) + CDec(val(2))

            lblQty.Text = FormatNumber(val(0))
            lblTotalHarga.Text = FormatNumber(totalHarga)
            lblTipeKewajiban.Text = DepositBEnum.GetStringValueKewajiban(objDepositBKewajibanHeader.TipeKewajiban)
            lblStatus.Text = DepositBEnum.GetStringValueStatusPengajuan(objDepositBKewajibanHeader.Status)
            'lblPpn.Text = FormatNumber(val(1) * 0.1)
            lblPpn.Text = FormatNumber(val(2))

            Dim lblTotalHargaHide As Label = CType(e.Item.FindControl("lblTotalHargaHide"), Label) '
            Dim lblPpnHide As Label = CType(e.Item.FindControl("lblPpnHide"), Label) '

            lblTotalHargaHide.Text = totalHarga
            lblPpnHide.Text = val(2)
        End If
    End Sub


    Private Sub dgOffset_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgOffset.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dgOffset.PageSize * dgOffset.CurrentPageIndex)).ToString
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            'Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbOffset"">")
            'e.Item.Cells(0).Controls.Add(rdbChoice)

            oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)

            Dim obj As v_EquipPO = CType(e.Item.DataItem, v_EquipPO)
            Dim lblTotalTagihan As Label = e.Item.FindControl("lblTotalTagihan")

            Dim maxday As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("BlockedDaysKTBConfirm"))
            Dim confirmDate As Date = obj.CreatedTime
            Dim currentDate As Date = Date.Today
            Dim range As Date = currentDate.AddDays(-14)

            'Dim urlParams As String = ""
            'Dim oEEH As EstimationEquipHeader = New EstimationEquipHeaderFacade(User).Retrieve(obj.EstimationNumber)

            'urlParams &= "TableName=" & GetType(EstimationEquipHeader).Name
            'urlParams &= "&TableID=" & oEEH.ID.ToString()
            'urlParams &= "&TableCode=" & oEEH.EstimationNumber
            'urlParams &= "&FieldName=" & "Status"

            'urlParams &= "&TableName2=" & GetType(IndentPartHeader).Name
            'urlParams &= "&TableID2=" & obj.ID.ToString()
            'urlParams &= "&TableCode2=" & obj.RequestNo
            'If oDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            '    urlParams &= "&FieldName2=" & "StatusKTB"
            'Else
            '    urlParams &= "&FieldName2=" & "Status"
            'End If

            Dim nAllocated As Integer = 0
            Dim TotalTagihan As Decimal = 0
            Try
                If Not IsNothing(obj) Then
                    If obj.IndentPartDetails.Count > 0 Then
                        For Each objIPD As IndentPartDetail In obj.IndentPartDetails
                            nAllocated += IIf(objIPD.Qty > 0, 1, 0)
                            If Not IsNothing(objIPD.EstimationEquipDetail) Then
                                If objIPD.EstimationEquipDetail.ID > 0 Then
                                    TotalTagihan = TotalTagihan + ((objIPD.Qty * objIPD.EstimationEquipDetail.Harga) - ((objIPD.EstimationEquipDetail.Discount / 100) * objIPD.Qty * objIPD.EstimationEquipDetail.Harga))
                                End If
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception

            End Try
            'TotalTagihan = TotalTagihan * 1.1
            Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(obj.CreatedTime, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
            TotalTagihan = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=TotalTagihan) + TotalTagihan
            lblTotalTagihan.Text = FormatNumber(TotalTagihan, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

#End Region

#Region "Custom"

    Sub Initialize()

        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        imgDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblDealerS.Attributes("onclick") = "ShowPPDealerSelection();"
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")

        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            txtKodeDealer.Text = oDealer.DealerCode
            txtKodeDealer.Attributes.Add("readonly", "readonly")
            lblDealerS.Attributes("style") = "display:none"
        Else
            lblDealerS.Attributes("style") = "display:table-row"
        End If

        txtNomorRekening.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")
        txtNomorRekening.Attributes.Add("readonly", "readonly")
        lblPostingDate.Text = Date.Today.ToShortDateString.ToString()
        lblBankAccount.Attributes("OnClick") = "ShowPPDealerBankAccountSelection(" & txtKodeDealer.Text & ")"


        BindTipePengajuan(ddlTipePengajuan)
        BindYear()
        BindPeriode()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, False, companyCode)

        txtNomerSuratPengajuan.Text = ""
        txtNomorRekening.Text = ""
        lblNoRegValue.Text = ""
        lblTotal.Text = ""

        lblPeriode.Visible = False
        lblBulan.Visible = False
        rbPeriode.Visible = False
        rbBulan.Visible = False

        If txtKodeDealer.Text.Trim <> String.Empty Then
            oDealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
            If oDealer.ID > 0 Then
                FillDataDealer(oDealer)
            End If

        End If

        dgInterest.DataSource = Nothing
        dgInterest.DataBind()
        dgTransfer.DataSource = Nothing
        dgTransfer.DataBind()
        dgKewajiban.DataSource = Nothing
        dgKewajiban.DataBind()
        dgOffset.DataSource = Nothing
        dgOffset.DataBind()

        btnValidasi.Enabled = False
        btnNew.Enabled = False

    End Sub

    Private Sub BindTipePengajuan(ByVal ddl As DropDownList)
        ddl.Items.Clear()

        Dim items As New ArrayList
        Dim _arrStatus As ArrayList = New DepositBEnum().RetrieveTipePengajuan()
        For Each item As EnumProperty In _arrStatus
            items.Add(item)
        Next
        ddl.DataSource = items
        ddl.DataTextField = "NameType"
        ddl.DataValueField = "ValType"
        ddl.DataBind()

        ddl.Items.FindByValue("3").Enabled = False
    End Sub

    Private Sub BindYear()
        Dim curYear As Integer = Date.Now.Year
        Dim startYear As Integer = curYear - 5
        Dim EndYear As Integer = curYear + 5
        Dim intYear As Integer = 0
        ddlYear.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlYear.Items.Clear()
        For intYear = startYear To EndYear
            ddlYear.Items.Add(intYear.ToString)
        Next
        ddlYear.Items.FindByValue(Date.Now.Year.ToString).Selected = True
    End Sub

    Private Sub BindPeriode()
        ddlPeriode.Items.Clear()
        ddlPeriode.Items.Insert(0, New ListItem("Please Select", "0"))
        ddlPeriode.Items.Insert(1, New ListItem("Jan - Mar", "1"))
        ddlPeriode.Items.Insert(2, New ListItem("Apr - Jun", "2"))
        ddlPeriode.Items.Insert(3, New ListItem("Jul - Sep", "3"))
        ddlPeriode.Items.Insert(4, New ListItem("Okt - Des", "4"))
    End Sub

    Sub FillDataDealer(ByVal oD As Dealer)

        Dim Tgl As String

        ltrDealerCode.Text = String.Format("{0} / {1}", oD.DealerCode.ToString(), oD.SearchTerm2)
        ltrDealerCode.Visible = False
        ltrDealerName.Text = oD.DealerName
        ltrDealerName.Visible = False
        lblDealerName.Text = oD.DealerName
        txtDealerID.Text = oD.ID

        If oD.AgreementNo Is Nothing Then
            oD.AgreementNo = String.Empty
        End If

        If oD.AgreementDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
            Tgl = String.Empty
        Else
            Tgl = Format(oD.AgreementDate, "dd/MM/yyyy").ToString()
        End If
    End Sub

    Private Sub BindDataPengajuan()
        Dim objDealer As Dealer
        If txtKodeDealer.Text.Trim = "" Then
            MessageBox.Show("Tentukan dealer terlebih dahulu")
            Return
        Else
            objDealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
            If IsNothing(objDealer) Then
                MessageBox.Show("Kode dealer tidak sesuai")
                Return
                'Else
                '    lblDealerName.Text = objDealer.DealerName
            End If
        End If
        TotalAmount = 0
        lblTotal.Text = "0"

        dgTransfer.Visible = False
        dgInterest.Visible = False
        dgKewajiban.Visible = False
        dgOffset.Visible = False
        dgProject.Visible = False

        sessHelper.SetSession("sessDepositeBPencairanHeader", Nothing)

        Dim selectedTipe As DepositBEnum.TipePengajuan = CType([Enum].Parse(GetType(DepositBEnum.TipePengajuan), ddlTipePengajuan.SelectedValue), DepositBEnum.TipePengajuan)
        Select Case selectedTipe
            Case DepositBEnum.TipePengajuan.Transfer
                SetControl_Transfer()
            Case DepositBEnum.TipePengajuan.ProjectService
                SetControl_ProjectService()
            Case DepositBEnum.TipePengajuan.Interest
                SetControl_Interest()
            Case DepositBEnum.TipePengajuan.Offset_SP
                SetControl_Offset()
            Case DepositBEnum.TipePengajuan.KewajibanReguler
                SetControl_KewajibanReguler()
            Case DepositBEnum.TipePengajuan.Kewajiban_NonReguler
                SetControl_KewajibanNonReguler()
        End Select

    End Sub

    Private Sub SetControl_Transfer()
        ddlYear.Visible = False
        ddlPeriode.Visible = False
        rbPeriode.Visible = False
        rbBulan.Visible = False
        lblPeriode.Visible = False
        lblBulan.Visible = False
        lblProduk.Visible = True
        rbProduk.Visible = True
        ddlProductCategory.Visible = True

        BindDataTransfer()
        dgTransfer.Columns(3).Visible = True
        dgTransfer.ShowFooter = True
    End Sub

    Private Sub SetControl_ProjectService()
        lblProduk.Text = "Produk"
        ddlYear.Visible = False
        ddlPeriode.Visible = False
        rbPeriode.Visible = False
        rbBulan.Visible = False
        lblPeriode.Visible = False
        lblBulan.Visible = False
        'ddlPeriode.SelectedIndex = 0

        BindDataProject()
    End Sub

    Private Sub SetControl_Interest()
        ddlYear.Visible = True
        ddlPeriode.Visible = True
        rbPeriode.Visible = True
        rbBulan.Visible = True
        lblPeriode.Visible = True
        lblBulan.Visible = True
        lblProduk.Visible = True
        rbProduk.Visible = True
        ddlProductCategory.Visible = True
        lnkAccount.Visible = True
        'If ddlProductCategory.SelectedIndex = 0 Then
        '    MessageBox.Show("Tentukan kategori produk.")
        '    Exit Sub
        'End If
        'If ddlPeriode.SelectedIndex = 0 Then
        '    MessageBox.Show("Tentukan periode.")
        '    Exit Sub
        'End If
        BindDataInterest()
    End Sub

    Private Sub SetControl_Offset()
        ddlYear.Visible = False
        ddlPeriode.Visible = False
        rbPeriode.Visible = False
        rbBulan.Visible = False
        lblPeriode.Visible = False
        lblBulan.Visible = False
        lblProduk.Visible = True
        rbProduk.Visible = True
        ddlProductCategory.Visible = True
        'ddlPeriode.SelectedIndex = 0

        BindDataOffset()
    End Sub

    Private Sub SetControl_KewajibanReguler()
        ddlYear.Visible = False
        ddlPeriode.Visible = False
        rbPeriode.Visible = False
        rbBulan.Visible = False
        lblPeriode.Visible = False
        lblBulan.Visible = False
        lblProduk.Text = "Produk"
        rbProduk.Visible = False
        'ddlPeriode.SelectedIndex = 0

        BindDataKewajibanReguler()
    End Sub

    Private Sub SetControl_KewajibanNonReguler()
        ddlYear.Visible = False
        ddlPeriode.Visible = False
        rbPeriode.Visible = False
        rbBulan.Visible = False
        lblPeriode.Visible = False
        lblBulan.Visible = False
        lblProduk.Text = "Produk"
        rbProduk.Visible = False
        'ddlPeriode.SelectedIndex = 0

        BindDataKewajibanNonReguler()
    End Sub

    Private Sub BindDataTransfer()
        Try
            dgTransfer.Visible = True

            Dim _ArrDepositBTransfer As ArrayList = New ArrayList
            _ArrDepositBTransfer = CType(sessHelper.GetSession("ArrDepositBTransfer"), ArrayList)

            If IsNothing(_ArrDepositBTransfer) Then
                Dim dtPengajuan = New DataTable
                dgTransfer.DataSource = dtPengajuan
                dgTransfer.DataBind()
            Else
                dgTransfer.DataSource = _ArrDepositBTransfer
                dgTransfer.DataBind()

                If _ArrDepositBTransfer.Count > 0 Then
                    For Each item As DepositBTransfer In _ArrDepositBTransfer
                        TotalAmount = TotalAmount + CDbl(item.Amount)
                    Next
                End If
            End If

            lblTotal.Text = FormatNumber(TotalAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BindDataInterest()
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBInterestHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBInterestHeader), "Year", MatchType.Exact, ddlYear.SelectedItem.Text))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBInterestHeader), "Periode", MatchType.Exact, ddlPeriode.SelectedValue))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBInterestHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBInterestHeader), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBInterestHeader), "Status", MatchType.Exact, CInt(DepositBEnum.StatusPengajuan.Baru))) 'Baru

        Dim arrDeposit As ArrayList = New DepositBInterestHeaderFacade(User).Retrieve(criterias)
        dgInterest.DataSource = arrDeposit
        dgInterest.DataBind()

        dgInterest.Visible = True
        dgInterest.ShowFooter = False

        TotalAmount = 0
        For Each item As DepositBInterestHeader In arrDeposit
            TotalAmount = TotalAmount + item.NettoAmount
        Next
        lblTotal.Text = FormatNumber(TotalAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)

    End Sub

    Private Sub BindDataProject()
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "ProductCategory.ID", MatchType.Exact, CInt(ddlProductCategory.SelectedValue)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "Status", MatchType.Exact, CInt(DepositBEnum.StatusPengajuan.Baru)))

        Dim arlDebitNote As ArrayList = New DepositBDebitNoteFacade(User).Retrieve(criterias)
        Me.dgProject.Visible = True
        Me.dgProject.DataSource = arlDebitNote
        Me.dgProject.DataBind()


    End Sub

    Private Sub BindDataOffset()
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.v_EquipPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.v_EquipPO), "DealerCode", MatchType.Exact, objDealer.DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.v_EquipPO), "PaymentType", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.v_EquipPO), "MaterialType", MatchType.Exact, 4))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.v_EquipPO), "StatusKTB", MatchType.Exact, 1))
        Dim strSql As String = "(select IndentPartEqHeaderID from DepositBPencairanHeader where IndentPartEqHeaderID > 0 AND Status <>3 and DealerID = " & objDealer.ID & " and RowStatus = 0)"
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.v_EquipPO), "ID", MatchType.NotInSet, strSql))
        Dim strSql2 As String = "(select a.EstimationNumber from EstimationEquipHeader a where a.DepositBKewajibanHeaderID is not null)"
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.v_EquipPO), "EstimationNumber", MatchType.NotInSet, strSql2))

        Dim arlOffset As ArrayList = New v_EquipPOFacade(User).Retrieve(criterias)
        Me.dgOffset.DataSource = arlOffset
        Me.dgOffset.DataBind()
        Me.dgOffset.Visible = True

    End Sub

    Private Sub BindDataKewajibanReguler()
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "ProductCategory.ID", MatchType.Exact, CInt(ddlProductCategory.SelectedValue)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "TipeKewajiban", MatchType.Exact, CInt(DepositBEnum.TipeKewajiban.Regular)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "Status", MatchType.Exact, CInt(DepositBEnum.StatusPengajuan.Transfer))) 'Baru
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "NoSalesorder", MatchType.IsNotNull, Nothing))

        Dim arlDepositBKewajiban As ArrayList = New DepositBKewajibanHeaderFacade(User).Retrieve(criterias)
        Me.dgKewajiban.DataSource = arlDepositBKewajiban
        Me.dgKewajiban.DataBind()
        Me.dgKewajiban.Visible = True

    End Sub

    Private Sub BindDataKewajibanNonReguler()
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
        'Dim strDealerCodeColl As String = DealerFacade.GenerateDealerCodeSelection(objDealer, User)
        '1. Filter di Kewajiban :
        '   - Dealer            : group dealer
        '   - Tipe kewajiban    : Non Reg
        '   - Status            : Proses

        Dim strFilter As New System.Text.StringBuilder
        strFilter.Append("select distinct(h.DepositBKewajibanHeaderID)")
        strFilter.Append(" from EstimationEquipHeader h")
        strFilter.Append(", EstimationEquipDetail d ")
        strFilter.Append(", (")
        strFilter.Append("        Select eep.EstimationEquipDetailID")
        strFilter.Append("		from EstimationEquipPO eep")
        strFilter.Append("		, (")
        strFilter.Append("		select * from EstimationEquipDetail ")
        strFilter.Append("		where EstimationEquipHeaderID  in")
        strFilter.Append("		(select id from EstimationEquipHeader where 1 = 1 and Status = 4 )")
        strFilter.Append("		) a")
        strFilter.Append("		, (")
        strFilter.Append("		select * from IndentPartDetail ")
        strFilter.Append("		where IndentPartHeaderID  in")
        strFilter.Append("		(select id from IndentPartHeader where 1 = 1 and MaterialType = 4 and PaymentType = 1 and StatusKTB = 1)")
        strFilter.Append("		) b")
        strFilter.Append("        where 1 = 1")
        strFilter.Append("		and eep.EstimationEquipDetailID = a.ID")
        strFilter.Append("		and eep.IndentPartDetailID = b.ID")
        strFilter.Append(" ) c ")
        strFilter.Append(" where h.ID = d.EstimationEquipHeaderID")
        strFilter.Append(" and d.ID = c.EstimationEquipDetailID")

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "ProductCategory.ID", MatchType.Exact, CInt(ddlProductCategory.SelectedValue)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "TipeKewajiban", MatchType.Exact, CInt(DepositBEnum.TipeKewajiban.NonReguler)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "Status", MatchType.Exact, CInt(DepositBEnum.StatusPengajuan.Transfer))) 'Baru
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "ID", MatchType.InSet, "(" & strFilter.ToString & ")"))

        Dim arlDepositBKewajiban As ArrayList = New DepositBKewajibanHeaderFacade(User).Retrieve(criterias)
        Me.dgKewajiban.DataSource = arlDepositBKewajiban
        Me.dgKewajiban.DataBind()
        Me.dgKewajiban.Visible = True
    End Sub


    Private Function GetAmountKewajibanNonReguler(ByVal obj As DepositBKewajibanHeader) As String
        Dim vRet As String = String.Empty

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EstimationEquipHeader), "DepositBKewajibanHeader.ID", MatchType.Exact, obj.ID))

        Dim arlEqHeader As ArrayList = New EstimationEquipHeaderFacade(User).Retrieve(criterias)
        If arlEqHeader.Count > 0 Then
            Dim harga As Decimal = 0
            Dim qty As Decimal = 0
            Dim totalQty As Decimal = 0
            Dim disc As Decimal = 0
            Dim subTotal As Decimal = 0
            Dim totalHarga As Decimal = 0

            For Each header As EstimationEquipHeader In arlEqHeader
                For Each detail As EstimationEquipDetail In header.EstimationEquipDetails
                    Dim arlEqPO As ArrayList = detail.EstimationEquipPO
                    For Each eqPO As EstimationEquipPO In arlEqPO
                        qty = eqPO.IndentPartDetail.Qty
                        harga = eqPO.IndentPartDetail.EstimationEquipDetail.Harga
                        disc = eqPO.IndentPartDetail.EstimationEquipDetail.Discount
                        subTotal = (harga * qty) - ((harga * disc / 100) * qty)
                        totalHarga = totalHarga + subTotal
                        totalQty = qty + totalQty
                    Next
                Next
            Next

            vRet = totalQty.ToString() + ";" + totalHarga.ToString() + ";" + (totalHarga * 0.1).ToString
        End If

        Return vRet
        'Select sum(((ed.harga * ip.Qty) - ((ed.Harga * ed.Discount / 100) * ip.Qty)))
        'from estimationequipheader eh
        'join EstimationEquipDetail ed on eh.ID = ed.EstimationEquipHeaderID
        'join EstimationEquipPO po on po.EstimationEquipDetailID = ed.ID
        'join IndentPartDetail ip on ip.ID = po.IndentPartDetailID
        'join DepositBKewajibanHeader kh on kh.ID = eh.DepositBKewajibanHeaderID
        'where kh.ID = 297
    End Function

    Private Function GetAmountKewajibanReguler(ByVal obj As DepositBKewajibanHeader) As String
        Dim vRet As String = String.Empty
        Dim vQty As Decimal = 0
        Dim vPrice As Decimal = 0
        Dim vTax As Decimal = 0
        If obj.TipeKewajiban = DepositBEnum.TipeKewajiban.Regular Then
            For Each detail As DepositBKewajibanDetail In obj.DepositBKewajibanDetails
                vQty = detail.Qty + vQty
                vPrice = detail.Harga + vPrice
                vTax = detail.Tax + vTax
            Next
            vRet = vQty.ToString() + ";" + vPrice.ToString() + ";" + vTax.ToString()
        Else

        End If

        Return vRet
    End Function

    'Private Function GetTotalPDepositB(ByVal dealerID As Integer, ByVal productCategoryID As Integer, ByVal periode As Integer) As Decimal
    '    Dim totalDepositB As Decimal = 0
    '    Dim transactioDateStart As DateTime = New DateTime(periode, 1, 1, 0, 0, 0)
    '    Dim transactioDateTo As DateTime = New DateTime(periode, 12, 31, 23, 59, 59)

    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "Dealer.ID", MatchType.Exact, dealerID))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "ProductCategory.ID", MatchType.Exact, productCategoryID))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.GreaterOrEqual, transactioDateStart))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBHeader), "TransactionDate", MatchType.Lesser, transactioDateTo))

    '    Dim sortColl As SortCollection = New SortCollection
    '    sortColl.Add(New Sort(GetType(DepositBHeader), "TransactionDate", Sort.SortDirection.DESC))

    '    Dim arlDepositB As ArrayList = New DepositBHeaderFacade(User).Retrieve(criterias, sortColl)
    '    If arlDepositB.Count > 0 Then
    '        Dim objDepositB As DepositBHeader = CType(arlDepositB(0), DepositBHeader)
    '        totalDepositB = objDepositB.EndBalance + totalDepositB
    '    End If
    '    Return totalDepositB
    'End Function

    'Private Function GetTotalYangSudahDiAjukan(ByVal dealerID As Integer, ByVal iTipePengajuan As Integer, ByVal productCategoryID As Integer) As Decimal
    '    Dim totalPengajuan As Decimal = 0
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "Dealer.ID", MatchType.Exact, dealerID))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "ProductCategory.ID", MatchType.Exact, productCategoryID))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "Status", MatchType.InSet, "(0, 1, 4, 6 )"))

    '    Dim arlPencairan As ArrayList = New DepositBPencairanHeaderFacade(User).Retrieve(criterias)
    '    For Each pencairan As DepositBPencairanHeader In arlPencairan
    '        If pencairan.Status = DepositBEnum.StatusPencairan.Baru Or _
    '            pencairan.Status = DepositBEnum.StatusPencairan.Validasi Then
    '            totalPengajuan = pencairan.DealerAmount + totalPengajuan
    '        Else
    '            totalPengajuan = pencairan.ApprovalAmount + totalPengajuan
    '        End If
    '    Next
    '    Return totalPengajuan

    'End Function

    'Private Function GetTotalPlafon(ByVal dealerID As Integer, ByVal productCategoryID As Integer, ByVal periode As Integer) As Decimal
    '    Dim totalPlafon As Decimal = 0

    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "Dealer.ID", MatchType.Exact, dealerID))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "ProductCategory.ID", MatchType.Exact, productCategoryID))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPlafon), "PeriodePlafon", MatchType.Exact, periode))

    '    Dim arlPlafon As ArrayList = New DepositBPlafonFacade(User).Retrieve(criterias)
    '    For Each plafon As DepositBPlafon In arlPlafon
    '        totalPlafon = plafon.JumlahPlafon + totalPlafon
    '    Next
    '    Return totalPlafon
    'End Function

    'Private Function PlafonChecking(ByVal dealerID As Integer, ByVal productCategoryID As Integer, _
    '                                ByVal tipePengajuan As Integer, ByVal periode As Integer, ByRef msg As String) As Boolean

    '    Dim totalDeposit As Decimal = GetTotalPDepositB(dealerID, productCategoryID, periode)
    '    If Not (totalDeposit > 0) Then
    '        msg = "Dealer tidak mempunyai Deposit B untuk produk " & ddlProductCategory.SelectedItem.Text
    '        Return False
    '    End If

    '    Dim totalPlafon = GetTotalPlafon(dealerID, productCategoryID, periode)
    '    If totalPlafon = 0 Then
    '        msg = "Plafon belum dibuat."
    '        Return False
    '    End If

    '    Dim totalDone = GetTotalYangSudahDiAjukan(dealerID, tipePengajuan, productCategoryID)

    '    Dim maxPencairan = totalDeposit - totalPlafon

    '    Dim totalMaxPengajuan As Decimal = 0

    '    totalMaxPengajuan = maxPencairan - totalDone

    '    If TotalAmount > totalMaxPengajuan Then
    '        'msg = "Plafon tidak mencukupi, maximal nilai pengajuan : " & FormatNumber(totalMaxPengajuan, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) + ". "
    '        msg = "Plafon tidak mencukupi, maximal total pengajuan : " & FormatNumber(maxPencairan, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) + ". \n"
    '        msg = msg + " Nilai pengajuan dalam proses : " & FormatNumber(totalDone, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) + ". \n"
    '        msg = msg + " Sehingga maximal pengajuan : " & FormatNumber(totalMaxPengajuan, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) + ". \n"

    '        Return True
    '    Else
    '        msg = String.Empty
    '        Return True
    '    End If

    'End Function

    Private Function Validation() As Boolean
        'Start Validasi.....

        Dim iTipePengajuan As DepositBEnum.TipePengajuan = CType([Enum].Parse(GetType(DepositBEnum.TipePengajuan), ddlTipePengajuan.SelectedValue), DepositBEnum.TipePengajuan)

        If txtKodeDealer.Text.Trim = String.Empty Then
            MessageBox.Show("Tentukan kode dealer")
            Return False
        End If
        If CInt(ddlTipePengajuan.SelectedIndex) = 0 Then
            MessageBox.Show("Tentukan tipe pengajuan")
            Return False
        End If
        'If txtNomerSuratPengajuan.Text = String.Empty Then
        '    MessageBox.Show("Error : Silahkan isi nomor surat pengajuan terlebih dahulu!")
        '    Return False
        'End If
        'If txtNomorRekening.Text = String.Empty And iTipePengajuan <> DepositBEnum.TipePengajuan.Offset_SP Then
        If txtNomorRekening.Text.Trim = String.Empty Then
            MessageBox.Show("Nomor Rekening harus diisi")
            Return False
        End If

        'If ddlProductCategory.SelectedIndex = 0 Then
        '    MessageBox.Show("Error : Silahkan pilih produk terlebih dahulu!")
        '    Return False
        'End If


        Dim objDealer As Dealer = New General.DealerFacade(User).GetDealer(txtKodeDealer.Text.Trim)
        Dim iPeriod As Integer = CInt(ddlYear.SelectedValue)
        Dim ProductCategoryID As Integer = CInt(ddlProductCategory.SelectedValue)

        Dim GridItem As DataGridItem
        Dim isChecked As Boolean = False

        Select Case iTipePengajuan
            Case DepositBEnum.TipePengajuan.Transfer
                If dgTransfer.Items.Count = 0 Then
                    MessageBox.Show("Belum tambah data (icon plus hijau belum dipilih)")
                    Return False
                End If
                TotalAmount = CDbl(lblTotal.Text)
            Case DepositBEnum.TipePengajuan.Interest
                For intRow As Integer = 0 To dgInterest.Items.Count - 1
                    GridItem = dgInterest.Items(intRow)
                    Dim rb As RadioButton = GridItem.FindControl("rbInterest")
                    If rb.Checked Then
                        isChecked = True
                        Dim lblTotalHarga As Label = GridItem.FindControl("lblInterestAmount")
                        TotalAmount = CDec(lblTotalHarga.Text)
                        Me.lblTotal.Text = FormatNumber(CDec(lblTotalHarga.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                    End If
                Next

                If isChecked = False Then
                    MessageBox.Show("Tidak ada Daftar interest yang dipilih")
                    Return False
                End If

            Case DepositBEnum.TipePengajuan.ProjectService 'oon
                For intRow As Integer = 0 To dgProject.Items.Count - 1
                    GridItem = dgProject.Items(intRow)
                    Dim rb As RadioButton = GridItem.FindControl("rbProject")
                    If rb.Checked Then
                        isChecked = True
                        Dim lblTotalHarga As Label = GridItem.FindControl("lblAmount")
                        TotalAmount = CDec(lblTotalHarga.Text)
                        Me.lblTotal.Text = FormatNumber(CDec(lblTotalHarga.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                    End If
                Next

                If isChecked = False Then
                    MessageBox.Show("Tidak ada daftar Debit Note yang dipilih")
                    Return False
                End If

            Case DepositBEnum.TipePengajuan.Offset_SP
                For intRow As Integer = 0 To dgOffset.Items.Count - 1
                    GridItem = dgOffset.Items(intRow)
                    Dim rb As RadioButton = GridItem.FindControl("rbOffset")
                    If rb.Checked Then
                        isChecked = True
                        Dim lblTotalHarga As Label = GridItem.FindControl("lblTotalTagihan")
                        TotalAmount = CDec(lblTotalHarga.Text)
                        Me.lblTotal.Text = FormatNumber(CDec(lblTotalHarga.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                    End If
                Next

                If isChecked = False Then
                    MessageBox.Show("Tidak ada Nomor Pengajuan yang dipilih")
                    Return False
                End If

            Case DepositBEnum.TipePengajuan.Kewajiban_NonReguler
                For intRow As Integer = 0 To dgKewajiban.Items.Count - 1
                    GridItem = dgKewajiban.Items(intRow)
                    Dim rb As RadioButton = GridItem.FindControl("rbKewajiban")
                    If rb.Checked Then
                        isChecked = True
                        Dim lblTotalHarga As Label = GridItem.FindControl("lblTotalHarga")
                        TotalAmount = CDec(lblTotalHarga.Text)
                        Me.lblTotal.Text = FormatNumber(CDec(lblTotalHarga.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                    End If
                Next

                If isChecked = False Then
                    MessageBox.Show("Tidak ada Kewajiban Non Reguler Deposit B yang dipilih")
                    Return False
                End If

            Case DepositBEnum.TipePengajuan.KewajibanReguler
                For intRow As Integer = 0 To dgKewajiban.Items.Count - 1
                    GridItem = dgKewajiban.Items(intRow)
                    Dim rb As RadioButton = GridItem.FindControl("rbKewajiban")
                    If rb.Checked Then
                        isChecked = True
                        Dim lblTotalHarga As Label = GridItem.FindControl("lblTotalHarga")
                        TotalAmount = CDec(lblTotalHarga.Text)
                        Me.lblTotal.Text = FormatNumber(CDec(lblTotalHarga.Text), 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
                    End If
                Next

                If isChecked = False Then
                    MessageBox.Show("Tidak ada Kewajiban Reguler Deposit B yang dipilih")
                    Return False
                End If

        End Select

        Dim isCekKewajiban As Boolean
        Dim isCekPlafon As Boolean
        Select Case iTipePengajuan
            Case DepositBEnum.TipePengajuan.Offset_SP, _
                DepositBEnum.TipePengajuan.ProjectService, _
                DepositBEnum.TipePengajuan.Transfer

                isCekKewajiban = True
                isCekPlafon = True

            Case DepositBEnum.TipePengajuan.Interest, _
                DepositBEnum.TipePengajuan.Kewajiban_NonReguler, _
                DepositBEnum.TipePengajuan.KewajibanReguler

                isCekKewajiban = False
                isCekPlafon = False

        End Select

        If isCekKewajiban Then
            Dim bCekKewajiban As Boolean = CekKewajiban()
            If bCekKewajiban = False Then
                MessageBox.Show("Tidak bisa mengajukan pencairan. Masih ada kewajiban yang belum dipenuhi")
                Return False
            End If
        End If

        Dim bPlafonChecking As Boolean
        Dim msgPlafonChecking As String
        If isCekPlafon Then
            Dim objDepositBHelper As DepositBHelper = New DepositBHelper
            bPlafonChecking = objDepositBHelper.PlafonChecking(objDealer.ID, ProductCategoryID, iTipePengajuan, iPeriod, TotalAmount, msgPlafonChecking)

            If bPlafonChecking = True And msgPlafonChecking <> "" Then
                'If Me.hdnMCPConfirmation.Value = "-1" Then
                'MessageBox.Confirm(msgPlafonChecking + ". Apakah akan dilanjutkan?", "hdnMCPConfirmation-")
                MessageBox.Show(msgPlafonChecking)
                Return False
                'End If
            End If

            If bPlafonChecking = False And msgPlafonChecking <> "" Then
                MessageBox.Show(msgPlafonChecking)
                Return False
            End If
        End If


        Dim dblAmount As Double = 0
        For intRow As Integer = 0 To dgTransfer.Items.Count - 1
            GridItem = dgTransfer.Items(intRow)
            dblAmount += CDbl(DirectCast(GridItem.FindControl("lblJumlahPencairan"), Label).Text())
        Next
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBHeader), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(DepositBHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
        Dim dateTimePrev As DateTime = DateTime.Now.AddMonths(-1)
        Dim prevMonth As String = String.Format("{0}/{1}/1", dateTimePrev.Year, dateTimePrev.Month)
        crit.opAnd(New Criteria(GetType(DepositBHeader), "TransactionDate", MatchType.Greater, prevMonth))
        Dim dateTimeNext As DateTime = DateTime.Now.AddMonths(1)
        Dim nextMonth As String = String.Format("{0}/{1}/1", dateTimeNext.Year, dateTimeNext.Month)
        crit.opAnd(New Criteria(GetType(DepositBHeader), "TransactionDate", MatchType.Lesser, nextMonth))
        Dim objDepBHeader As DepositBHeader = New DepositBHeaderFacade(User).Retrieve(crit)(0)

        If objDepBHeader.EndBalance + dblAmount < 0 Then
            MessageBox.Show("Saldo deposit kurang")
            Return False
        End If

        Return True

    End Function

    Private Function validateDepositAmount() As Boolean
        Dim crit As CriteriaComposite
    End Function

    Private Function CekKewajiban() As Boolean
        Dim vReturn As Boolean = True
        Try
            'oDealer = sessHelper.GetSession("DEALER")
            Dim objDealer As Dealer
            If txtKodeDealer.Text.Trim = "" Then
                MessageBox.Show("Pilih dealer terlebih dahulu")
                Return False
            Else
                objDealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
                If IsNothing(objDealer) Then
                    MessageBox.Show("Dealer tidak terdaftar")
                    Return False
                End If
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.DepositBKewajibanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "ProductCategory.ID", MatchType.Exact, CInt(ddlProductCategory.SelectedValue)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKewajibanHeader), "Status", MatchType.No, CInt(DepositBEnum.StatusPengajuan.Selesai)))

            Dim arlKewajiban As ArrayList = New DepositBKewajibanHeaderFacade(User).Retrieve(criterias)
            If arlKewajiban.Count > 0 Then
                'For Each kewajiban As DepositBKewajibanHeader In arlKewajiban
                '    Dim criteriasPencairan As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                '    criteriasPencairan.opAnd(New Criteria(GetType(KTB.DNET.Domain.DepositBPencairanHeader), "Dealer.ID", MatchType.Exact, kewajiban.Dealer.ID))
                '    criteriasPencairan.opAnd(New Criteria(GetType(KTB.DNET.Domain.DepositBPencairanHeader), "DepositBKewajibanHeader.ID", MatchType.Exact, kewajiban.ID))

                '    Dim arlPencairan As ArrayList = New DepositBPencairanHeaderFacade(User).Retrieve(criteriasPencairan)
                '    For Each pencairan As DepositBPencairanHeader In arlPencairan
                '        If pencairan.Status < CInt(DepositBEnum.StatusPencairan.Konfirmasi) Then
                '            vReturn = False
                '        End If
                '    Next
                'Next
                vReturn = False
            End If

        Catch ex As Exception
            vReturn = False
        End Try
        Return vReturn
    End Function

    Private Sub SimpanPengajuanPencairan()
        Try
            'Simpan
            Dim iTipePengajuan As DepositBEnum.TipePengajuan = CType([Enum].Parse(GetType(DepositBEnum.TipePengajuan), ddlTipePengajuan.SelectedValue), DepositBEnum.TipePengajuan)
            Dim objDealer As Dealer = New General.DealerFacade(User).GetDealer(txtKodeDealer.Text.Trim)
            Dim iPeriod As Integer = CInt(ddlYear.SelectedValue)
            Dim ProductCategoryID As Integer = CInt(ddlProductCategory.SelectedValue)

            'DealerBankAccount
            Dim objDealerBankAccount As DealerBankAccount
            If txtNomorRekening.Text <> String.Empty Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "BankAccount", MatchType.Exact, txtNomorRekening.Text))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBankAccount), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
                Dim arlAccount As ArrayList = New FinishUnit.DealerBankAccountFacade(User).Retrieve(criterias)
                If arlAccount.Count > 0 Then
                    objDealerBankAccount = arlAccount(0)
                Else
                    objDealerBankAccount = New DealerBankAccountFacade(User).Retrieve(0)
                End If
            Else
                objDealerBankAccount = New DealerBankAccountFacade(User).Retrieve(0)
            End If

            'ProductCategory
            Dim objProductCategory As ProductCategory
            objProductCategory = New ProductCategoryFacade(User).Retrieve(CInt(ddlProductCategory.SelectedValue))

            'DepositBPencairanHeader
            Dim nResult As Integer

            Dim objH As New DepositBPencairanHeader
            objH.Dealer = objDealer
            objH.TipePengajuan = iTipePengajuan
            objH.DealerBankAccount = objDealerBankAccount
            objH.ProductCategory = objProductCategory
            objH.NoReferensi = txtNomerSuratPengajuan.Text
            objH.Status = DepositBEnum.StatusPencairan.Baru
            If Me.hdnMCPConfirmation.Value = "1" Then
                objH.Flag = 1 'over plafon
            Else
                objH.Flag = 0 'accepted
            End If

            Dim intRow As Integer = 0
            Dim GridItem As DataGridItem
            Dim objDepositBPencairanDetails As ArrayList = objH.DepositBPencairanDetails
            Dim dealerAmount As Double = 0
            Dim isChecked As Boolean = False
            Select Case iTipePengajuan
                Case DepositBEnum.TipePengajuan.Transfer

                    Dim intRows As Integer = dgTransfer.Items.Count - 1
                    For intRow = 0 To intRows
                        GridItem = dgTransfer.Items(intRow)

                        Dim objD As DepositBPencairanDetail = New DepositBPencairanDetail
                        objD.DealerAmount = CDbl(DirectCast(GridItem.FindControl("lblJumlahPencairan"), Label).Text())
                        objD.Description = DirectCast(GridItem.FindControl("lblPenjelasan"), Label).Text.Trim

                        objDepositBPencairanDetails.Add(objD)

                        dealerAmount = dealerAmount + CDbl(DirectCast(GridItem.FindControl("lblJumlahPencairan"), Label).Text())
                    Next
                    objH.DealerAmount = dealerAmount

                Case DepositBEnum.TipePengajuan.Interest

                    Dim intRows As Integer = dgInterest.Items.Count - 1
                    For intRow = 0 To intRows
                        GridItem = dgInterest.Items(intRow)

                        Dim lblID As Label = GridItem.FindControl("lblID")

                        Dim objDepositBInterestHeader As DepositBInterestHeader = New DepositBInterestHeader
                        objDepositBInterestHeader = New DepositBInterestHeaderFacade(User).Retrieve(CInt(lblID.Text))
                        If objDepositBInterestHeader.ID > 0 Then
                            objH.DepositBInterestHeader = objDepositBInterestHeader
                        End If

                        Dim objDepositBPencairanDetail As DepositBPencairanDetail = New DepositBPencairanDetail

                        objDepositBPencairanDetail.DealerAmount = CDbl(DirectCast(GridItem.FindControl("lblInterestAmount"), Label).Text())
                        objDepositBPencairanDetail.Description = DirectCast(GridItem.FindControl("lblDescription"), Label).Text.Trim

                        objDepositBPencairanDetails.Add(objDepositBPencairanDetail)

                        dealerAmount = dealerAmount + CDbl(DirectCast(GridItem.FindControl("lblInterestAmount"), Label).Text())

                    Next
                    objH.DealerAmount = dealerAmount

                Case DepositBEnum.TipePengajuan.Offset_SP
                    Dim intRows As Integer = dgOffset.Items.Count - 1
                    For intRow = 0 To intRows
                        GridItem = dgOffset.Items(intRow)
                        Dim rb As RadioButton = GridItem.FindControl("rbOffset")
                        If rb.Checked Then
                            isChecked = True

                            Dim strID As String = GridItem.Cells(2).Text
                            Dim objIndentPartHeader As IndentPartHeader = New IndentPartHeader
                            objIndentPartHeader = New KTB.DNet.BusinessFacade.IndentPart.IndentPartHeaderFacade(User).Retrieve(CInt(strID))
                            If objIndentPartHeader.ID > 0 Then
                                objH.IndentPartHeader = objIndentPartHeader
                            End If

                            Dim objDepositBPencairanDetail As DepositBPencairanDetail = New DepositBPencairanDetail
                            objDepositBPencairanDetail.DealerAmount = CDbl(DirectCast(GridItem.FindControl("lblTotalTagihan"), Label).Text())
                            objDepositBPencairanDetail.Description = DirectCast(GridItem.FindControl("lblDescription"), Label).Text.Trim
                            objDepositBPencairanDetails.Add(objDepositBPencairanDetail)

                            dealerAmount = dealerAmount + CDbl(DirectCast(GridItem.FindControl("lblTotalTagihan"), Label).Text())
                        End If

                    Next
                    objH.DealerAmount = dealerAmount

                Case DepositBEnum.TipePengajuan.ProjectService
                    Dim intRows As Integer = dgProject.Items.Count - 1
                    For intRow = 0 To intRows
                        GridItem = dgProject.Items(intRow)
                        Dim rb As RadioButton = GridItem.FindControl("rbProject")
                        If rb.Checked Then
                            isChecked = True

                            Dim lblID As Label = GridItem.FindControl("lblID")
                            Dim lblAmount As Label = GridItem.FindControl("lblAmount")
                            Dim lblDescription As Label = GridItem.FindControl("lblDescription")

                            Dim objDepBDebitNote As DepositBDebitNote = New DepositBDebitNote
                            objDepBDebitNote = New DepositBDebitNoteFacade(User).Retrieve(CInt(lblID.Text))
                            If objDepBDebitNote.ID > 0 Then
                                objH.DepositBDebitNote = objDepBDebitNote
                            End If

                            Dim objDepositBPencairanDetail As DepositBPencairanDetail = New DepositBPencairanDetail
                            objDepositBPencairanDetail.DealerAmount = CDec(lblAmount.Text)
                            objDepositBPencairanDetail.Description = lblDescription.Text
                            objDepositBPencairanDetails.Add(objDepositBPencairanDetail)

                            dealerAmount = dealerAmount + CDec(lblAmount.Text)
                        End If
                    Next

                    objH.DealerAmount = dealerAmount

                Case DepositBEnum.TipePengajuan.KewajibanReguler
                    For intRow = 0 To dgKewajiban.Items.Count - 1
                        GridItem = dgKewajiban.Items(intRow)
                        Dim rb As RadioButton = GridItem.FindControl("rbKewajiban")
                        If rb.Checked Then
                            isChecked = True

                            Dim lblID As Label = GridItem.FindControl("lblID")
                            Dim lblAmount As Label = GridItem.FindControl("lblTotalHarga")
                            Dim lblPpn As Label = GridItem.FindControl("lblPpn")
                            Dim total As Decimal = CDec(lblAmount.Text) '+ CDec(lblPpn.Text)

                            Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = New DepositBKewajibanHeader
                            objDepositBKewajibanHeader = New DepositBKewajibanHeaderFacade(User).Retrieve(CInt(lblID.Text))
                            If objDepositBKewajibanHeader.ID > 0 Then
                                objH.DepositBKewajibanHeader = objDepositBKewajibanHeader
                            End If

                            Dim objDepositBPencairanDetail As DepositBPencairanDetail = New DepositBPencairanDetail
                            objDepositBPencairanDetail.DealerAmount = total
                            objDepositBPencairanDetail.Description = "Pembayaran kewajiban"

                            objDepositBPencairanDetails.Add(objDepositBPencairanDetail)

                            dealerAmount = dealerAmount + CDbl(total)
                        End If
                    Next

                    objH.DealerAmount = dealerAmount

                Case DepositBEnum.TipePengajuan.Kewajiban_NonReguler
                    For intRow = 0 To dgKewajiban.Items.Count - 1
                        GridItem = dgKewajiban.Items(intRow)
                        Dim rb As RadioButton = GridItem.FindControl("rbKewajiban")
                        If rb.Checked Then
                            isChecked = True

                            Dim lblID As Label = GridItem.FindControl("lblID")
                            Dim lblAmount As Label = GridItem.FindControl("lblTotalHarga")
                            Dim lblPpn As Label = GridItem.FindControl("lblPpn")
                            Dim total As Decimal = CDec(lblAmount.Text) ' + CDec(lblPpn.Text)

                            Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = New DepositBKewajibanHeader
                            objDepositBKewajibanHeader = New DepositBKewajibanHeaderFacade(User).Retrieve(CInt(lblID.Text))
                            If objDepositBKewajibanHeader.ID > 0 Then
                                objH.DepositBKewajibanHeader = objDepositBKewajibanHeader
                            End If

                            Dim objDepositBPencairanDetail As DepositBPencairanDetail = New DepositBPencairanDetail
                            objDepositBPencairanDetail.DealerAmount = total
                            objDepositBPencairanDetail.Description = "Pembayaran kewajiban"

                            objDepositBPencairanDetails.Add(objDepositBPencairanDetail)

                            dealerAmount = dealerAmount + CDbl(total)
                        End If
                    Next

                    objH.DealerAmount = dealerAmount
            End Select

            nResult = New DepositBPencairanHeaderFacade(User).InsertTransaction(objH)

            If nResult <> -1 Then
                'MessageBox.Show("Pencairan Nomor " & txtNomerSuratPengajuan.Text & " berhasil disimpan. ")

                Dim objHeader As DepositBPencairanHeader = New DepositBPencairanHeader
                objHeader = New DepositBPencairanHeaderFacade(User).Retrieve(nResult)
                If objHeader.ID > 0 Then
                    MessageBox.Show("Pencairan Nomor " & objHeader.NoReg & " berhasil disimpan. ")
                    lblNoRegValue.Text = objHeader.NoReg
                    sessHelper.SetSession("sessDepositeBPencairanHeader", objHeader)

                    Select Case iTipePengajuan
                        Case DepositBEnum.TipePengajuan.Transfer
                            sessHelper.SetSession("ArrDepositBTransfer", Nothing)
                            dgTransfer.Columns(3).Visible = False
                            dgTransfer.ShowFooter = False

                        Case DepositBEnum.TipePengajuan.Interest
                            Dim objDepositBInterestHeader As DepositBInterestHeader = New DepositBInterestHeader
                            objDepositBInterestHeader = New DepositBInterestHeaderFacade(User).Retrieve(objHeader.DepositBInterestHeader.ID)
                            If objDepositBInterestHeader.ID > 0 Then
                                objDepositBInterestHeader.Status = DepositBEnum.StatusPengajuan.Proses
                                Dim vReturn As Integer = New DepositBInterestHeaderFacade(User).Update(objDepositBInterestHeader)
                            End If

                        Case DepositBEnum.TipePengajuan.Offset_SP


                        Case DepositBEnum.TipePengajuan.ProjectService
                            Dim objDepBDebitNote As DepositBDebitNote = New DepositBDebitNote
                            objDepBDebitNote = New DepositBDebitNoteFacade(User).Retrieve(objHeader.DepositBDebitNote.ID)
                            If objDepBDebitNote.ID > 0 Then
                                objDepBDebitNote.Status = DepositBEnum.StatusPengajuan.Proses
                                Dim vReturn As Integer = New DepositBDebitNoteFacade(User).Update(objDepBDebitNote)
                            End If

                        Case DepositBEnum.TipePengajuan.KewajibanReguler
                            Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = New DepositBKewajibanHeader
                            objDepositBKewajibanHeader = New DepositBKewajibanHeaderFacade(User).Retrieve(objHeader.DepositBKewajibanHeader.ID)
                            If objDepositBKewajibanHeader.ID > 0 Then
                                objDepositBKewajibanHeader.Status = DepositBEnum.StatusPengajuan.Proses
                                Dim vReturn As Integer = New DepositBKewajibanHeaderFacade(User).Update(objDepositBKewajibanHeader)
                            End If

                        Case DepositBEnum.TipePengajuan.Kewajiban_NonReguler
                            Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = New DepositBKewajibanHeader
                            objDepositBKewajibanHeader = New DepositBKewajibanHeaderFacade(User).Retrieve(objHeader.DepositBKewajibanHeader.ID)
                            If objDepositBKewajibanHeader.ID > 0 Then
                                objDepositBKewajibanHeader.Status = DepositBEnum.StatusPengajuan.Proses
                                Dim vReturn As Integer = New DepositBKewajibanHeaderFacade(User).Update(objDepositBKewajibanHeader)
                            End If
                    End Select

                    Dim objHistFacade As DepositBStatusHistoryFacade = New DepositBStatusHistoryFacade(User)
                    objHistFacade.SaveHistoricalPengajuan(DepositBEnum.StatusType.Pencairan, objHeader.ID, DepositBEnum.StatusPencairan.Baru, DepositBEnum.StatusPencairan.Baru)

                End If
                Me.hdnMCPConfirmation.Value = "-1"
                SetControl(False)
                btnSave.Enabled = False
                btnCancel.Enabled = False
                btnNew.Enabled = True
                btnValidasi.Enabled = True
            Else
                btnSave.Enabled = True
                btnCancel.Enabled = True
                btnNew.Enabled = False
                btnValidasi.Enabled = False
                MessageBox.Show(SR.SaveFail)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub SetControl(ByVal isEnabled As Boolean)
        txtKodeDealer.Enabled = isEnabled
        ddlTipePengajuan.Enabled = isEnabled
        ddlProductCategory.Enabled = isEnabled
        ddlYear.Enabled = isEnabled
        ddlPeriode.Enabled = isEnabled
        txtNomerSuratPengajuan.Enabled = isEnabled
        txtNomorRekening.Enabled = isEnabled
        lblBankAccount.Enabled = isEnabled

    End Sub


#End Region

End Class

Public Class DepositBTransfer
    Private _id As Integer
    Private _amount As Double
    Private _desc As String

    Public Property ID() As Integer
        Get
            Return _iD
        End Get
        Set(ByVal value As Integer)
            _iD = value
        End Set
    End Property

    Public Property Amount() As Double
        Get
            Return _amount
        End Get
        Set(ByVal value As Double)
            _amount = value
        End Set
    End Property

    Public Property Desc() As String
        Get
            Return _desc
        End Get
        Set(ByVal value As String)
            _desc = value
        End Set
    End Property

End Class