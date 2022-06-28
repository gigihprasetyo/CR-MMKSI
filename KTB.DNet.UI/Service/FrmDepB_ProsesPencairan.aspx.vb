Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.BusinessFacade.IndentPart
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports KTB.DNet.BusinessValidation.Helpers

Public Class FrmDepB_ProsesPencairan
    Inherits System.Web.UI.Page

#Region "Private Variables"
    Private arlDepositBPencairan As ArrayList = New ArrayList
    Private arlDepositBPencairanFilter As ArrayList = New ArrayList

    Private _DepositBPencairanHeaderFacade As New DepositBPencairanHeaderFacade(User)
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

    Dim sHelper As New SessionHelper
    Dim objUserInfo As UserInfo
    Dim IsDealer As Boolean = True
    Const DocTypePengajuan = 1
    Const DocTypeKuitansi = 2

    Const TEMP_EMAIL_APPROVED = "../IndentPartEquipment/EmailTemplateApprovedSPPO.htm"
    Const TEMP_EMAIL_REJECT = "../IndentPartEquipment/EmailTemplateRejectSPPO.htm"
#End Region

#Region "Custom Method"
    Sub BindTipePengajuan(ByVal ddl As DropDownList)
        ddl.DataSource = [Enum].GetNames(GetType(DepositBEnum.TipePengajuan))
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
    End Sub

    Private Sub AddPeriodCriteria(ByVal criterias As CriteriaComposite, ByVal ColumnName As String)
        Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                    icPeriodeFrom.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                    icPeriodeTo.Value.Day, 0, 0, 0)
        dtEnd = dtEnd.AddDays(1)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), ColumnName, MatchType.GreaterOrEqual, dtStart))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), ColumnName, MatchType.Lesser, dtEnd))
    End Sub

    Sub BindDatagridDaftarPencairan(ByVal pageIndex As Integer)
        Dim oDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        Else
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If oDealer.DealerGroup.ID = 21 Then 'single dealer
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "Dealer.ID", MatchType.Exact, oDealer.ID))
                Else
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "Dealer.DealerGroup.ID", MatchType.Exact, oDealer.DealerGroup.ID))
                End If

            End If
        End If
        If cbPeriode.Checked Then
            AddPeriodCriteria(criterias, "CreatedTime")
        End If

        If CInt(ddlProductCategory.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        End If

        Dim selectedTipe As Integer = ddlTipePengajuan.SelectedIndex
        If selectedTipe > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "TipePengajuan", MatchType.Exact, selectedTipe))
        End If

        If txtNoPengajuan.Text.Trim.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "NoReferensi", MatchType.[Partial], txtNoPengajuan.Text.Trim))
        End If

        Dim selectedStatus As Integer = CInt(ddlStatusPengajuan.SelectedValue)
        If selectedStatus <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "Status", MatchType.Exact, selectedStatus))
        End If

        If txtNoReg.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "NoReg", MatchType.Exact, txtNoReg.Text))
        End If

        If txtNoPeng.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "IndentPartHeader.RequestNo", MatchType.Partial, txtNoPeng.Text.Trim))
        End If

        If txtNoRegKuitansi.Text.Trim <> String.Empty Then
            Dim strNoRegKuitansi As String = "('" & txtNoRegKuitansi.Text.Trim().Replace(";", "','") & "')"
            Dim Sql As String = ""
            'Sql = "(select DepositBPencairanHeaderID from DepositBReceipt where NoRegKuitansi like  '%" & txtNoRegKuitansi.Text.Trim & "%')"
            Sql = "(select DepositBPencairanHeaderID from DepositBReceipt where NoRegKuitansi in " & strNoRegKuitansi & ")"

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "ID", MatchType.InSet, Sql))
        End If


        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
            sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositBPencairanHeader), ViewState("currSortColumn"), ViewState("currSortDirection")))
        Else
            sortColl = Nothing
        End If

        arlDepositBPencairan = New DepositBPencairanHeaderFacade(User).Retrieve(criterias, sortColl)
        Dim DealerCode As String = String.Empty
        arlDepositBPencairanFilter.Clear()
        For Each item As DepositBPencairanHeader In arlDepositBPencairan
            arlDepositBPencairanFilter.Add(item)
        Next

        If (arlDepositBPencairanFilter.Count > 0) Then
            dgDaftarPengajuanPencairanDepositB.Visible = True
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arlDepositBPencairanFilter, pageIndex, dgDaftarPengajuanPencairanDepositB.PageSize)
            dgDaftarPengajuanPencairanDepositB.DataSource = PagedList
            dgDaftarPengajuanPencairanDepositB.VirtualItemCount = arlDepositBPencairanFilter.Count()
            dgDaftarPengajuanPencairanDepositB.DataBind()

            sHelper.SetSession("VDaftarPengajuan", arlDepositBPencairanFilter)
            sHelper.SetSession("VDaftarPengajuanPerPage", PagedList)

        Else
            dgDaftarPengajuanPencairanDepositB.Visible = True
            dgDaftarPengajuanPencairanDepositB.DataSource = New ArrayList
            dgDaftarPengajuanPencairanDepositB.VirtualItemCount = 0
            dgDaftarPengajuanPencairanDepositB.DataBind()

            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Private Sub InitNonMandatoryObject()
        cbPeriode.Checked = False
        icPeriodeFrom.Enabled = False
        icPeriodeTo.Enabled = False

    End Sub

    Sub BindStatusPencairan(ByVal ddl As DropDownList, ByVal iDealerTitle As Integer)
        Dim items As New ArrayList
        Dim _arrStatus As ArrayList = New DepositBEnum().RetrieveStatuspencairan(iDealerTitle, True, False)
        For Each item As EnumProperty In _arrStatus
            items.Add(item)
        Next
        ddl.DataSource = items
        ddl.DataTextField = "NameType"
        ddl.DataValueField = "ValType"
        ddl.DataBind()
    End Sub

    Private Function InsertHistory(ByVal NoSurat As String, ByVal OldStatus As Integer, ByVal NewStatus As Integer, ByVal DocType As Integer)
        Dim nResult As Integer = -1
        'Dim objHistoryDepositBPencairan As DepositBStatusHistory = New DepositBStatusHistory
        'objHistoryDepositBPencairan.DocNumber = NoSurat
        'objHistoryDepositBPencairan.OldStatus = OldStatus
        'objHistoryDepositBPencairan.NewStatus = NewStatus
        'objHistoryDepositBPencairan.DocType = DocType

        'nResult = New DepositBStatusHistoryFacade(User).Insert(objHistoryDepositBPencairan)
        Return nResult
    End Function

#End Region
    Dim _ubah_status_pengajuan_pencairan_Privilege As Boolean = False
    Dim _lihat_status_pengajuan_pencairan_Privilege As Boolean = False
    Dim _transfer_pengajuan_pencairan_Privilege As Boolean = False

#Region "Event Handlers"
    Private Sub InitiateAuthorization()
        Dim objDealer As Dealer = Me.sHelper.GetSession("DEALER")

        _lihat_status_pengajuan_pencairan_Privilege = SecurityProvider.Authorize(Context.User, SR.lihat_status_pengajuan_pencairan_Privilege)
        _ubah_status_pengajuan_pencairan_Privilege = SecurityProvider.Authorize(Context.User, SR.ubah_status_pengajuan_pencairan_Privilege)
        _transfer_pengajuan_pencairan_Privilege = SecurityProvider.Authorize(Context.User, SR.transfer_pengajuan_pencairan_Privilege)

        If Not _lihat_status_pengajuan_pencairan_Privilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sparepart - Daftar Pengajuan pencairan")
        End If


        If Not _ubah_status_pengajuan_pencairan_Privilege Then
            btnProses.Visible = False
        End If



        If Not _transfer_pengajuan_pencairan_Privilege Then
            btnTransfer.Visible = False
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then


        Else
            btnTransfer.Visible = False
            PopUpExpired()
        End If

        'If Not SecurityProvider.Authorize(Context.User, SR.DepositA_proses_pengajuan_pencairan_depoA_lihat_Privilege) Then
        '    Response.Redirect("../frmAccessDenied.aspx?modulName=Deposit A - Lihat Proses Pengajuan Pencairan Deposit A")
        '    Me.btnProses.Visible = False
        'End If

        'If Not SecurityProvider.Authorize(Context.User, SR.DepositA_proses_pengajuan_pencairan_depoA_lihat_detail_Privilege) Then
        '    Me.dgDaftarPengajuanPencairanDepositB.Columns(12).Visible = False
        'End If

    End Sub

    Private Sub PopUpExpired()
        Dim objDealer As Dealer = Me.sHelper.GetSession("DEALER")
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        objUserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            IsDealer = False
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        Else
            IsDealer = True
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            txtKodeDealer.Attributes.Add("readonly", "readonly")
        End If

        If Not IsPostBack Then
            BindTipePengajuan(ddlTipePengajuan)
            BindStatusPencairan(ddlStatusPengajuan, EnumDealerTittle.DealerTittle.KTB_DEALER)
            If IsDealer Then
                BindStatusPencairan(ddlAction, EnumDealerTittle.DealerTittle.DEALER)
            Else
                BindStatusPencairan(ddlAction, EnumDealerTittle.DealerTittle.KTB)
                btnTransfer.Visible = True
            End If
            InitNonMandatoryObject()
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)

        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgDaftarPengajuanPencairanDepositB.CurrentPageIndex = 0
        BindDatagridDaftarPencairan(dgDaftarPengajuanPencairanDepositB.CurrentPageIndex)
    End Sub

    Private Sub dgDaftarPengajuanPencairanDepositB_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDaftarPengajuanPencairanDepositB.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarPengajuanPencairanDepositB.CurrentPageIndex * dgDaftarPengajuanPencairanDepositB.PageSize)

            Dim objPencairanDepositBH As DepositBPencairanHeader = CType(e.Item.DataItem, DepositBPencairanHeader)

            Dim lblTipe As Label = CType(e.Item.FindControl("lblTipe"), Label)
            Dim selectedTipe As DepositBEnum.TipePengajuan = CType([Enum].Parse(GetType(DepositBEnum.TipePengajuan), objPencairanDepositBH.TipePengajuan), DepositBEnum.TipePengajuan)
            Dim SelectedTipeName As String = selectedTipe.GetName(GetType(DepositBEnum.TipePengajuan), objPencairanDepositBH.TipePengajuan)
            lblTipe.Text = SelectedTipeName

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

            Dim selectedStatus As DepositBEnum.StatusPencairan = CType([Enum].Parse(GetType(DepositBEnum.StatusPencairan), objPencairanDepositBH.Status), DepositBEnum.StatusPencairan)
            Dim SelectedStatusName As String = selectedStatus.GetName(GetType(DepositBEnum.StatusPencairan), objPencairanDepositBH.Status)
            lblStatus.Text = SelectedStatusName

            Dim lblJumlahPengajuan As Label = CType(e.Item.FindControl("lblJumlahPengajuan"), Label)
            Select Case selectedTipe
                Case DepositBEnum.TipePengajuan.Interest
                    lblJumlahPengajuan.Text = Format(objPencairanDepositBH.DepositBInterestHeader.NettoAmount, "#,###")
                Case Else
                    lblJumlahPengajuan.Text = Format(objPencairanDepositBH.DealerAmount, "#,###")
            End Select

            Dim txtJumlahDisetujui As TextBox = CType(e.Item.FindControl("txtJumlahDisetujui"), TextBox)

            If CInt(objPencairanDepositBH.Status) < CInt(DepositBEnum.StatusPencairan.Konfirmasi) Then
                txtJumlahDisetujui.Text = Format(objPencairanDepositBH.DealerAmount, "#,###")
                Select Case selectedTipe
                    Case DepositBEnum.TipePengajuan.Interest
                        txtJumlahDisetujui.Text = Format(objPencairanDepositBH.DepositBInterestHeader.NettoAmount, "#,###")
                    Case Else
                        txtJumlahDisetujui.Text = Format(objPencairanDepositBH.DealerAmount, "#,###")
                End Select
            Else
                txtJumlahDisetujui.Text = Format(objPencairanDepositBH.ApprovalAmount, "#,###")
            End If

            Dim lblNoRegKuitansi As Label = CType(e.Item.FindControl("lblNoRegKuitansi"), Label)
            Dim lblNoPengajuan As Label = CType(e.Item.FindControl("lblNoPengajuan"), Label)
            Dim lblJVNumber As Label = CType(e.Item.FindControl("lblJVNumber"), Label)
            Dim lblPaymentDate As Label = CType(e.Item.FindControl("lblPaymentDate"), Label)
            '
            If objPencairanDepositBH.DepositBReceipts.Count > 0 Then
                Dim objKuitansi As DepositBReceipt = CType(objPencairanDepositBH.DepositBReceipts(0), DepositBReceipt)
                If Not IsNothing(objKuitansi) Then
                    lblNoRegKuitansi.Text = objKuitansi.NoRegKuitansi
                    lblJVNumber.Text = objKuitansi.JVNumber
                    If Not IsNothing(objKuitansi.TanggalPelunasan) AndAlso objKuitansi.TanggalPelunasan.Year > 1900 Then
                        lblPaymentDate.Text = objKuitansi.TanggalPelunasan.ToString("dd/MM/yyyy")
                    End If
                End If
            End If



            Dim txtAlasan As TextBox = CType(e.Item.FindControl("txtAlasan"), TextBox)
            txtAlasan.Text = objPencairanDepositBH.KTBReason.Trim

            Dim lbViewDetail As LinkButton = CType(e.Item.FindControl("lbViewDetail"), LinkButton)
            lbViewDetail.Attributes("OnClick") = "showPopUp('../Service/FrmDepB_PopupPencairan.aspx?id=" & objPencairanDepositBH.ID & " ','',500,760,'');"
            lbViewDetail.ToolTip = "Lihat Detail"

            If IsDealer Then
                txtAlasan.ReadOnly = True
                txtJumlahDisetujui.ReadOnly = True
            Else
                txtAlasan.ReadOnly = False
                If CInt(objPencairanDepositBH.Status) = CInt(DepositBEnum.StatusPencairan.Validasi) Then
                    txtJumlahDisetujui.ReadOnly = False
                End If
            End If

            If objPencairanDepositBH.TipePengajuan = DepositBEnum.TipePengajuan.Offset_SP AndAlso Not IsNothing(objPencairanDepositBH.IndentPartHeader) Then
                Try
                    lblNoPengajuan.Text = objPencairanDepositBH.IndentPartHeader.RequestNo
                Catch ex As Exception

                End Try
            End If

            'Dim lbViewFlow As LinkButton = CType(e.Item.FindControl("lbViewFlow"), LinkButton)
            'lbViewFlow.Attributes("OnClick") = "showPopUp('../PopUp/PopUpFlowPencairanDepositB.aspx?DealerID=" & objPencairanDepositBH.DealerBankAccount.Dealer.ID & "&NoReg=" & objPencairanDepositBH.NoReg & "&NoSurat=" & objPencairanDepositBH.NoReferensi.ToString & " ','',500,600,'');"
            'lbViewFlow.ToolTip = "Flow Dok"


            Dim lbViewStatusHistory As LinkButton = CType(e.Item.FindControl("lbViewStatus"), LinkButton)
            lbViewStatusHistory.Attributes("OnClick") = "showPopUp('../Service/FrmDepB_PopupHistory.aspx?statusType=1" & "&id=" & objPencairanDepositBH.ID & "','',500,500,'');"
            lbViewStatusHistory.ToolTip = "History"

            Dim lbDelete As LinkButton = CType(e.Item.FindControl("lbDelete"), LinkButton)
            lbDelete.Attributes.Add("onclick", "return confirm('Anda yakin menghapus pencairan ini?');")
            lbDelete.ToolTip = "Hapus"
            lbDelete.CommandArgument = objPencairanDepositBH.ID

            If objPencairanDepositBH.Status = 0 Then
                If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    lbDelete.Visible = True
                End If
            End If

            If objPencairanDepositBH.Flag = 1 Then
                e.Item.BackColor = Color.Red
            End If

        ElseIf (e.Item.ItemType = ListItemType.Footer) Then
        End If
    End Sub

    Private Sub dgDaftarPengajuanPencairanDepositB_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDaftarPengajuanPencairanDepositB.PageIndexChanged
        dgDaftarPengajuanPencairanDepositB.CurrentPageIndex = e.NewPageIndex
        BindDatagridDaftarPencairan(e.NewPageIndex)
    End Sub

    Private Sub dgDaftarPengajuanPencairanDepositB_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDaftarPengajuanPencairanDepositB.ItemCommand
        If e.CommandName = "Delete" Then
            Dim objDepositPencairan As New DepositBPencairanHeader
            objDepositPencairan = New DepositBPencairanHeaderFacade(User).Retrieve(CInt(e.CommandArgument))
            If objDepositPencairan.ID > 0 Then
                objDepositPencairan.RowStatus = -1

                Dim intResult = New DepositBPencairanHeaderFacade(User).Update(objDepositPencairan)
                If intResult > -1 Then
                    UpdateStatusDataSource(objDepositPencairan)
                    MessageBox.Show(SR.DeleteSucces)
                    BindDatagridDaftarPencairan(dgDaftarPengajuanPencairanDepositB.CurrentPageIndex)
                Else
                    MessageBox.Show(SR.DeleteFail)
                End If
            End If
        ElseIf e.CommandName = "ViewDetail" Then
            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub cbPeriode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPeriode.CheckedChanged
        If cbPeriode.Checked Then
            icPeriodeFrom.Enabled = True
            icPeriodeTo.Enabled = True
        Else
            icPeriodeFrom.Enabled = False
            icPeriodeTo.Enabled = False
        End If
    End Sub

#End Region

#Region "Cek Privilege"

#End Region
    Private Function GetSelectedPengajuans(ByVal dg As DataGrid) As String
        Dim i As Integer = 0
        Dim strResult As String = "("

        For Each item As DataGridItem In dgDaftarPengajuanPencairanDepositB.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItem"), CheckBox)
            If (chk.Checked) Then
                strResult = strResult & dgDaftarPengajuanPencairanDepositB.DataKeys().Item(i) & ","
            End If
            i = i + 1
        Next
        strResult = Left(strResult, strResult.Length - 1) & ")"
    End Function

    Private Function IsDataInDTGBValid(ByVal nStatus As Integer) As Boolean
        Select Case nStatus
            Case CType(DepositBEnum.StatusPencairan.Selesai, Integer)
                For Each di As DataGridItem In Me.dgDaftarPengajuanPencairanDepositB.Items
                    Dim txtJumlahDisetujui As TextBox = di.FindControl("txtJumlahDisetujui")
                    Dim chkItemChecked As CheckBox = di.FindControl("chkItem")
                    If chkItemChecked.Checked AndAlso (txtJumlahDisetujui.Text.Trim = "" OrElse Val(txtJumlahDisetujui.Text.Trim) <= 0) Then
                        Return False
                    End If
                Next
            Case 10000

        End Select
        Return True
    End Function

    Private Sub ProsesPengajuan(ByVal iStatusProposed As Integer)
        Dim nResult As Integer = -1
        Dim Message As String
        Dim iStatusNew As Integer

        Dim iStatusOld As Integer
        Dim isChecked As Boolean = False
        Dim checkedCount As Integer = 0
        For Each item As DataGridItem In dgDaftarPengajuanPencairanDepositB.Items
            If ((item.ItemType = ListItemType.Item) Or (item.ItemType = ListItemType.AlternatingItem)) Then
                Dim chkCek As CheckBox = CType(item.FindControl("chkItem"), CheckBox)
                If chkCek.Checked Then
                    Dim ArrPengajuan = New ArrayList
                    'If Not sHelper.GetSession("VDaftarPengajuan") Is Nothing Then
                    '    ArrPengajuan = sHelper.GetSession("VDaftarPengajuan")
                    If Not sHelper.GetSession("VDaftarPengajuanPerPage") Is Nothing Then
                        ArrPengajuan = sHelper.GetSession("VDaftarPengajuanPerPage")
                        Dim objDepositBPencairanHeader As DepositBPencairanHeader = CType(ArrPengajuan(item.ItemIndex), DepositBPencairanHeader)
                        iStatusOld = objDepositBPencairanHeader.Status
                        iStatusNew = iStatusProposed

                        Select Case iStatusProposed
                            'Case DepositBEnum.StatusPencairan.Baru
                            '    If iStatusOld = DepositBEnum.StatusPencairan.Validasi Then
                            '        isChecked = True
                            '    End If
                            Case DepositBEnum.StatusPencairan.Validasi
                                If iStatusOld = DepositBEnum.StatusPencairan.Baru Then
                                    isChecked = True
                                Else
                                    Message = DepositBEnum.GetStringValueStatusPencairan(DepositBEnum.StatusPencairan.Baru)
                                End If
                            Case DepositBEnum.StatusPencairan.Batal_Validasi
                                If iStatusOld = DepositBEnum.StatusPencairan.Validasi Then
                                    isChecked = True
                                    iStatusNew = DepositBEnum.StatusPencairan.Baru
                                Else
                                    Message = DepositBEnum.GetStringValueStatusPencairan(DepositBEnum.StatusPencairan.Validasi)
                                End If
                            Case DepositBEnum.StatusPencairan.Konfirmasi
                                If iStatusOld = DepositBEnum.StatusPencairan.Validasi Then
                                    isChecked = True
                                Else
                                    Message = DepositBEnum.GetStringValueStatusPencairan(DepositBEnum.StatusPencairan.Validasi)
                                End If
                            Case DepositBEnum.StatusPencairan.Batal_Konfirmasi
                                If iStatusOld = DepositBEnum.StatusPencairan.Konfirmasi Then
                                    isChecked = True
                                    iStatusNew = DepositBEnum.StatusPencairan.Validasi
                                Else
                                    Message = DepositBEnum.GetStringValueStatusPencairan(DepositBEnum.StatusPencairan.Konfirmasi)
                                End If
                            Case DepositBEnum.StatusPencairan.Tolak
                                If iStatusOld = DepositBEnum.StatusPencairan.Validasi Then
                                    isChecked = True
                                Else
                                    Message = DepositBEnum.GetStringValueStatusPencairan(DepositBEnum.StatusPencairan.Validasi)
                                End If
                            Case DepositBEnum.StatusPencairan.Proses
                                If iStatusOld = DepositBEnum.StatusPencairan.Konfirmasi Then
                                    isChecked = True
                                Else
                                    Message = DepositBEnum.GetStringValueStatusPencairan(DepositBEnum.StatusPencairan.Konfirmasi)
                                End If
                            Case DepositBEnum.StatusPencairan.Selesai
                                If iStatusOld = DepositBEnum.StatusPencairan.Proses Then
                                    isChecked = True
                                Else
                                    Message = DepositBEnum.GetStringValueStatusPencairan(DepositBEnum.StatusPencairan.Proses)
                                End If
                            Case DepositBEnum.StatusPencairan.Cair
                                If iStatusOld = DepositBEnum.StatusPencairan.Selesai Then
                                    isChecked = True
                                Else
                                    Message = DepositBEnum.GetStringValueStatusPencairan(DepositBEnum.StatusPencairan.Selesai)
                                End If
                        End Select
                        If isChecked = False Then
                            MessageBox.Show("Syarat ubah status menjadi " & DepositBEnum.GetStringValueStatusPencairan(iStatusProposed) & " : " & Message)
                            Exit Sub
                        End If
                        Dim vSaved As Integer = 1
                        If isChecked Then
                            checkedCount = checkedCount + 1

                            objDepositBPencairanHeader.Status = iStatusNew
                            If iStatusNew = DepositBEnum.StatusPencairan.Konfirmasi Then

                                Dim txtJumlahDisetujui As TextBox = CType(item.FindControl("txtJumlahDisetujui"), TextBox)
                                If txtJumlahDisetujui.Text.Trim <> String.Empty Then
                                    If Not IsNumeric(txtJumlahDisetujui.Text.Trim) Then
                                        MessageBox.Show("Jumlah disetujui harus angka")
                                        Exit Sub
                                    End If

                                    Select Case CType(objDepositBPencairanHeader.TipePengajuan, DepositBEnum.TipePengajuan)
                                        Case DepositBEnum.TipePengajuan.Interest
                                            If CDec(txtJumlahDisetujui.Text) > objDepositBPencairanHeader.DepositBInterestHeader.NettoAmount Then
                                                MessageBox.Show("Jumlah Disetujui tidak boleh melebihi Jumlah Pengajuan")
                                                Exit Sub
                                            End If
                                        Case Else
                                            If CDec(txtJumlahDisetujui.Text) > objDepositBPencairanHeader.DealerAmount Then
                                                MessageBox.Show("Jumlah Disetujui tidak boleh melebihi Jumlah Pengajuan")
                                                Exit Sub
                                            End If
                                    End Select



                                    objDepositBPencairanHeader.ApprovalAmount = txtJumlahDisetujui.Text

                                    Dim txtAlasan As TextBox = CType(item.FindControl("txtAlasan"), TextBox)
                                    objDepositBPencairanHeader.KTBReason = txtAlasan.Text

                                    Dim bPlafonChecking As Boolean = True
                                    Dim msgPlafonChecking As String
                                    Dim iPeriod As Integer
                                    Dim IsCheckPlafon As Boolean = True
                                    Select Case CType(objDepositBPencairanHeader.TipePengajuan, DepositBEnum.TipePengajuan)
                                        Case DepositBEnum.TipePengajuan.Transfer
                                            iPeriod = objDepositBPencairanHeader.CreatedTime.Year
                                        Case DepositBEnum.TipePengajuan.Interest
                                            iPeriod = objDepositBPencairanHeader.DepositBInterestHeader.Year
                                            IsCheckPlafon = False
                                        Case DepositBEnum.TipePengajuan.ProjectService
                                            iPeriod = objDepositBPencairanHeader.DepositBDebitNote.PostingDate.Year
                                        Case DepositBEnum.TipePengajuan.Offset_SP
                                            iPeriod = objDepositBPencairanHeader.CreatedTime.Year
                                        Case Else
                                            iPeriod = objDepositBPencairanHeader.DepositBKewajibanHeader.PeriodYear
                                    End Select

                                    If iPeriod < 2016 Then 'untuk case periode < 2016 skip pengecekan deposit & plafon
                                        IsCheckPlafon = False
                                    End If

                                    Dim objDepositBHelper As DepositBHelper = New DepositBHelper
                                    If IsCheckPlafon Then
                                        bPlafonChecking = objDepositBHelper.PlafonChecking(objDepositBPencairanHeader.Dealer.ID, _
                                                                                      objDepositBPencairanHeader.ProductCategory.ID, _
                                                                                      objDepositBPencairanHeader.TipePengajuan, _
                                                                                      iPeriod, objDepositBPencairanHeader.ApprovalAmount, msgPlafonChecking)
                                    Else
                                        msgPlafonChecking = ""
                                        bPlafonChecking = False
                                    End If


                                    If bPlafonChecking = False And msgPlafonChecking <> "" Then
                                        MessageBox.Show(msgPlafonChecking)
                                        Exit Sub
                                    End If

                                    If bPlafonChecking = True Then
                                        If msgPlafonChecking <> "" Then
                                            'MessageBox.Confirm(msgPlafonChecking + ". Apakah akan dilanjutkan?", "hdnMCPConfirmation")
                                            objDepositBPencairanHeader.Flag = 1 'over max
                                        Else
                                            objDepositBPencairanHeader.Flag = 0 'accepted
                                        End If
                                    Else
                                        If msgPlafonChecking <> "" Then
                                            MessageBox.Show(msgPlafonChecking)
                                            Exit Sub
                                        End If
                                    End If
                                Else
                                    MessageBox.Show("Jumlah Disetujui tidak boleh kosong")
                                    Exit Sub
                                End If

                                'If objDepositBPencairanHeader.TipePengajuan = CInt(DepositBEnum.TipePengajuan.Offset_SP) Then
                                '    Dim objfacade As KTB.DNet.BusinessFacade.IndentPart.IndentPartHeaderFacade = New KTB.DNet.BusinessFacade.IndentPart.IndentPartHeaderFacade(User)
                                '    Dim objIndenPartHeader As IndentPartHeader = objfacade.Retrieve(objDepositBPencairanHeader.IndentPartHeader.ID)
                                '    If Not IsNothing(objIndenPartHeader) Then
                                '        objIndenPartHeader.StatusKTB = CInt(EnumIndentPartEquipStatus.EnumStatusKTB.Rilis)
                                '        vSaved = objfacade.Update(objIndenPartHeader)
                                '    End If
                                'End If

                            End If

                            'If vSaved > 0 Then
                            nResult = _DepositBPencairanHeaderFacade.Update(objDepositBPencairanHeader)

                            If nResult > -1 Then
                                Dim objHistFacade As DepositBStatusHistoryFacade = New DepositBStatusHistoryFacade(User)
                                objHistFacade.SaveHistoricalPencairan(objDepositBPencairanHeader, iStatusNew, iStatusOld)
                                'Update data source dari proses - > baru
                                If iStatusNew = CInt(DepositBEnum.StatusPencairan.Tolak) Then
                                    UpdateStatusDataSource(objDepositBPencairanHeader)
                                End If

                            End If
                            'End If

                        End If
                    End If

                End If
            End If
        Next
        If nResult > -1 Then
            MessageBox.Show("Ubah Status berhasil")
            ddlStatusPengajuan.SelectedValue = iStatusNew
            dgDaftarPengajuanPencairanDepositB.CurrentPageIndex = 0
            BindDatagridDaftarPencairan(dgDaftarPengajuanPencairanDepositB.CurrentPageIndex)
        Else
            If checkedCount = 0 Then
                MessageBox.Show("Tidak ada data yang memenuhi untuk diubah status ke :" + DepositBEnum.GetStringValueStatusPencairan(CInt(ddlAction.SelectedValue)))
            Else
                MessageBox.Show(SR.UpdateFail())
            End If

            Exit Sub
        End If

    End Sub

    Private Sub UpdateStatusDataSource(ByVal objDepositBPencairanHeader As DepositBPencairanHeader)
        'Dari status Proses --> Baru, karena proses delete atau tolak.
        Dim id As Integer
        Dim vResult As Integer = -1
        If Not IsNothing(objDepositBPencairanHeader.DepositBDebitNote) Then
            id = objDepositBPencairanHeader.DepositBDebitNote.ID
            Dim objDepositBDebitNote As DepositBDebitNote = New DepositBDebitNoteFacade(User).Retrieve(id)
            If Not IsNothing(objDepositBDebitNote) Then
                objDepositBDebitNote.Status = DepositBEnum.StatusPengajuan.Baru
                vResult = New DepositBDebitNoteFacade(User).Update(objDepositBDebitNote)

                'Jika DN Number ada di tagihan Training maka status tagihan berubah menjadi Transfer
                Dim func As New TrBillingHeaderFacade(Me.User)
                func.UpdateBillChangesTransfer(objDepositBDebitNote)
            End If
        End If
        If Not IsNothing(objDepositBPencairanHeader.DepositBInterestHeader) Then
            id = objDepositBPencairanHeader.DepositBInterestHeader.ID
            Dim objDepositBInterestHeader As DepositBInterestHeader = New DepositBInterestHeaderFacade(User).Retrieve(id)
            If Not IsNothing(objDepositBInterestHeader) Then
                objDepositBInterestHeader.Status = DepositBEnum.StatusPengajuan.Baru
                vResult = New DepositBInterestHeaderFacade(User).Update(objDepositBInterestHeader)
            End If
        End If
        'If Not IsNothing(objDepositBPencairanHeader.DepositBKewajibanHeader) Then
        '    id = objDepositBPencairanHeader.DepositBKewajibanHeader.ID
        '    Dim objDepositBKewajibanHeader As DepositBKewajibanHeader = New DepositBKewajibanHeaderFacade(User).Retrieve(id)
        '    If Not IsNothing(objDepositBKewajibanHeader) Then
        '        objDepositBKewajibanHeader.Status = DepositBEnum.StatusPengajuan.Baru
        '        vResult = New DepositBKewajibanHeaderFacade(User).Update(objDepositBKewajibanHeader)
        '    End If
        'End If
        If Not IsNothing(objDepositBPencairanHeader.DepositBKewajibanHeader) Then
            Dim arlEstimation As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "DepositBKewajibanHeader.ID", MatchType.Exact, objDepositBPencairanHeader.DepositBKewajibanHeader.ID))
            arlEstimation = New KTB.DNet.BusinessFacade.IndentPartEquipment.EstimationEquipHeaderFacade(User).Retrieve(criterias)
            If arlEstimation.Count > 0 Then
                Dim objEstimation As EstimationEquipHeader = CType(arlEstimation(0), EstimationEquipHeader)
                If objEstimation.ID > 0 Then
                    objEstimation.Status = EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru
                    vResult = New KTB.DNet.BusinessFacade.IndentPartEquipment.EstimationEquipHeaderFacade(User).Update(objEstimation)

                    If vResult <> -1 And Not IsNothing(objDepositBPencairanHeader.IndentPartHeader) Then
                        Dim objIndentPartHeaderFacade As KTB.DNet.BusinessFacade.IndentPart.IndentPartHeaderFacade = New KTB.DNet.BusinessFacade.IndentPart.IndentPartHeaderFacade(User)
                        Dim objIndenPartHeader As IndentPartHeader = objIndentPartHeaderFacade.Retrieve(objDepositBPencairanHeader.IndentPartHeader.ID)
                        If Not IsNothing(objIndenPartHeader) Then
                            sendRejectEmail(objIndenPartHeader)
                        End If
                    End If
                End If

            End If
        End If
    End Sub

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click

        If ddlAction.SelectedValue <> String.Empty AndAlso ddlAction.SelectedValue <> "-1" Then
            ProsesPengajuan(CInt(ddlAction.SelectedValue))
        Else
            MessageBox.Show("Tentukan status terlebih dahulu")
        End If

    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click

        Dim item As DataGridItem
        Dim arlReceiptChecked As ArrayList = New ArrayList
        Dim objDomain As DepositBPencairanHeader
        Dim objFacade As DepositBPencairanHeaderFacade = New DepositBPencairanHeaderFacade(User)
        For Each item In Me.dgDaftarPengajuanPencairanDepositB.Items
            If CType(item.FindControl("chkItem"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                objDomain = objFacade.Retrieve(id)
                If Not objDomain Is Nothing Then
                    If objDomain.Status = DepositBEnum.StatusPencairan.Konfirmasi Then
                        If objDomain.DepositBReceipts.Count > 0 Then
                            arlReceiptChecked.Add(objDomain)
                        End If

                    End If
                End If
            End If
        Next
        'Process
        If arlReceiptChecked.Count > 0 Then
            CreateTextFile(arlReceiptChecked)
        Else
            MessageBox.Show("Tidak ada data untuk Transfer. \n Syarat Transfer : \n - Status : Konfirmasi. \n - Sudah dibuatkan kuitansi.")
        End If
    End Sub

    Private Sub CreateTextFile(ByVal arl As ArrayList)
        Dim _fileHelper As New KTB.DNet.UI.Helper.FileHelper
        Dim fileInfo As System.IO.FileInfo
        Try
            fileInfo = _fileHelper.TransferPencairanDepositBToSAP(arl)
            MessageBox.Show(SR.UploadSucces(fileInfo.Name))
            'set status jadi proses
            Dim objFacade As DepositBPencairanHeaderFacade = New DepositBPencairanHeaderFacade(User)
            Dim objReceiptFacade As DepositBReceiptFacade = New DepositBReceiptFacade(User)
            Dim nReturn As Integer = -1
            Dim objIndentPartHeaderFacade As KTB.DNet.BusinessFacade.IndentPart.IndentPartHeaderFacade = New KTB.DNet.BusinessFacade.IndentPart.IndentPartHeaderFacade(User)
            Dim vSaved As Integer
            For Each item As DepositBPencairanHeader In arl
                item.Status = DepositBEnum.StatusPencairan.Proses
                nReturn = objFacade.Update(item)
                If nReturn <> -1 Then

                    For Each rec As DepositBReceipt In item.DepositBReceipts
                        rec.TanggalTransfer = Date.Now
                        nReturn = objReceiptFacade.Update(rec)
                    Next

                    Dim objHistFacade As DepositBStatusHistoryFacade = New DepositBStatusHistoryFacade(User)
                    objHistFacade.SaveHistoricalPencairan(item, DepositBEnum.StatusPencairan.Proses, DepositBEnum.StatusPencairan.Konfirmasi)

                End If

                If item.TipePengajuan = CInt(DepositBEnum.TipePengajuan.Offset_SP) Then
                    Dim objIndenPartHeader As IndentPartHeader = objIndentPartHeaderFacade.Retrieve(item.IndentPartHeader.ID)
                    If Not IsNothing(objIndenPartHeader) Then
                        objIndenPartHeader.StatusKTB = CInt(EnumIndentPartEquipStatus.EnumStatusKTB.Rilis)
                        vSaved = objIndentPartHeaderFacade.Update(objIndenPartHeader)
                        sendApprovedEmail(objIndenPartHeader)
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(SR.UploadFail(fileInfo.Name))
        End Try
    End Sub

    Private Function sendApprovedEmail(ByVal objSppo As IndentPartHeader)
        Dim oDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)

        Dim euf As EquipUserFacade = New EquipUserFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim mail As DNetMail = New DNetMail(smtp)
        Dim szTos As String ' = euf.CreateEmailToString(EquipUser.EquipUserGroup.Approved)
        Dim szCcs As String ' = euf.CreateEmailCcString(EquipUser.EquipUserGroup.Approved)
        Dim szItems As String = ""
        Dim myculture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ID-id")
        Dim i As Integer = 1
        Dim objEED As EstimationEquipDetail
        Dim TotalPrice As Double = 0
        Dim ItemPrice As Double = 0

        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Approved, szTos, szCcs, objSppo.Dealer)
        For Each objItem As IndentPartDetail In objSppo.IndentPartDetails
            objEED = GetEED(objItem)
            szItems &= "<tr>"
            szItems &= String.Format("<td align='center'>{0}</td>", i)
            szItems &= String.Format("<td>{0}</td>", IIf(objItem.SparePartMaster.PartNumber.Trim = "", " ", objItem.SparePartMaster.PartNumber))
            szItems &= String.Format("<td align='center'>{0}</td>", IIf(objItem.SparePartMaster.PartName.Trim = "", " ", objItem.SparePartMaster.PartName))
            szItems &= String.Format("<td align='center'>{0}</td>", objItem.Qty)
            szItems &= String.Format("<td align='center'>{0}</td>", FormatNumber(objItem.Price, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            szItems &= String.Format("<td align='center'>{0}</td>", FormatNumber(objEED.Discount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            ItemPrice = (objItem.Qty * objItem.Price) - objEED.Discount / 100 * (objItem.Qty * objItem.Price)
            szItems &= String.Format("<td align='right'>{0}</td>", FormatNumber(ItemPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            TotalPrice += ItemPrice
            szItems += "</tr>"
            i += 1
        Next

        Dim ppnFromMasterPPN = CalcHelper.GetPPNMasterByTaxTypeId(objSppo.RequestDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
        'Dim TotalPPN As Decimal = 0.1 * TotalPrice
        Dim TotalPPN = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnFromMasterPPN, dpp:=TotalPrice)
        szItems += "<tr>"
        szItems += "<td colspan='5' rowspan='3'>. &nbsp;</td>"
        szItems += "<td><b>Total Order</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>PPN</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>Total Tagihan</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPrice + TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        'Dim szFormats() As String = {objSppo.Dealer.DealerName, objSppo.Dealer.Address, objSppo.RequestNo, String.Format("Rp.{0}", objSppo.AmountBeforeTax.ToString("#,##0")), szItems}
        'Dim szFormats() As String = {objSppo.Dealer.DealerName, objSppo.Dealer.Address, objSppo.Dealer.City.CityName, objSppo.RequestNo, szItems}
        Dim sRequestNo As String = ""
        If objSppo.IndentPartDetails.Count > 0 Then
            Dim oEED As EstimationEquipDetail = GetEED(CType(objSppo.IndentPartDetails(0), IndentPartDetail))
            If Not IsNothing(oEED) AndAlso oEED.ID > 0 Then
                sRequestNo = oEED.EstimationEquipHeader.EstimationNumber
            End If
        End If

        Dim sCC As String = ""

        Dim EmailIndentCC1 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC1")
        Dim EmailIndentCC2 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC2")
        Dim EmailIndentCC3 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC3")
        Dim EmailIndentCC4 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC4")
        Dim EmailIndentCC5 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC5")

        sCC &= "<tr><td>" & EmailIndentCC1 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC2 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC3 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC4 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC5 & "</td></tr>"

        sCC = Now.ToString("dd MMM yyyy")
        Dim szFormats() As String = {objSppo.Dealer.DealerCode _
        , objSppo.Dealer.DealerName _
        , objSppo.Dealer.City.CityName _
        , Format(objSppo.RequestDate, "dd/MM/yyyy") _
        , objSppo.RequestNo _
        , objSppo.Dealer.DealerName, objSppo.Dealer.Address, objSppo.Dealer.City.CityName, objSppo.RequestNo, szItems, sCC, sRequestNo}
        mail.sendMail(Server.MapPath(TEMP_EMAIL_APPROVED), szTos, szCcs, "[MMKSI-DNet] Parts - Approval " & objSppo.RequestNo & " - " & objSppo.Dealer.DealerCode, szFormats)
    End Function


    Private Function sendRejectEmail(ByVal objSppo As IndentPartHeader)
        Dim oDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)

        Dim euf As EquipUserFacade = New EquipUserFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim mail As DNetMail = New DNetMail(smtp)
        Dim szTos As String ' = euf.CreateEmailToString(EquipUser.EquipUserGroup.Reject)
        Dim szCcs As String ' = euf.CreateEmailCcString(EquipUser.EquipUserGroup.Reject)
        Dim szItems As String = ""
        Dim objEED As EstimationEquipDetail
        Dim myculture As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("ID-id")
        Dim ItemPrice As Double = 0
        Dim TotalPrice As Double = 0 'ok

        CommonFunction.SetIndentPartEmailRecipient(EquipUser.EquipUserGroup.Reject, szTos, szCcs, objSppo.Dealer)
        For Each objItem As IndentPartDetail In objSppo.IndentPartDetails
            objEED = GetEED(objItem)
            szItems &= "<tr>"
            szItems &= String.Format("<td>{0}</td>", objItem.SparePartMaster.PartNumber)
            szItems &= String.Format("<td align='center'>{0}</td>", objItem.Qty)
            szItems &= String.Format("<td align='right'>{0}</td>", FormatNumber(objItem.Price, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            szItems &= String.Format("<td align='right'>{0}</td>", FormatNumber(objEED.Discount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            ItemPrice = (objItem.Price * objItem.Qty) - (objEED.Discount / 100) * (objItem.Price * objItem.Qty)
            szItems &= String.Format("<td align='right'>{0}</td>", FormatNumber(ItemPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True))
            TotalPrice += ItemPrice
            szItems &= "</tr>"
        Next
        Dim ppnFromMasterPPN = CalcHelper.GetPPNMasterByTaxTypeId(objSppo.RequestDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
        'Dim TotalPPN As Decimal = 0.1 * TotalPrice
        Dim TotalPPN = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnFromMasterPPN, dpp:=TotalPrice)
        szItems += "<tr>"
        szItems += "<td colspan='3' rowspan='3'>. &nbsp;</td>"
        szItems += "<td><b>Total Order</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPrice, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>PPN</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"
        szItems += "<tr>"
        szItems += "<td><b>Total Tagihan</b></td>"
        szItems += "<td align='right'>Rp. " & FormatNumber(TotalPrice + TotalPPN, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & "</td>"
        szItems += "</tr>"

        Dim objESPPOI As EquipSPPOInfo = New EquipSPPOInfoFacade(User).RetrieveByHeaderID(objSppo.ID)
        Dim sReason As String = IIf(IsNothing(objESPPOI), "", objESPPOI.Description)

        Dim sCC As String = ""

        Dim EmailIndentCC1 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC1")
        Dim EmailIndentCC2 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC2")
        Dim EmailIndentCC3 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC3")
        Dim EmailIndentCC4 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC4")
        Dim EmailIndentCC5 As String = KTB.DNet.Lib.WebConfig.GetValue("EmailIndentCC5")

        sCC &= "<tr><td>" & EmailIndentCC1 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC2 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC3 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC4 & "</td></tr>"
        sCC &= "<tr><td>" & EmailIndentCC5 & "</td></tr>"

        sCC = Now.ToString("dd MMM yyyy")
        'Dim szFormats() As String = {objSppo.Dealer.DealerName, objSppo.Dealer.Address, objSppo.RequestNo, String.Format("Rp.{0}", objSppo.AmountBeforeTax.ToString("#,##0")), sReason, szItems}
        Dim szFormats() As String = {objSppo.Dealer.DealerCode _
        , objSppo.Dealer.DealerName _
        , objSppo.Dealer.City.CityName _
        , Format(objSppo.RequestDate, "dd/MM/yyyy") _
        , objSppo.RequestNo _
        , objSppo.Dealer.DealerName, objSppo.Dealer.Address, objSppo.Dealer.City.CityName, objSppo.RequestNo, sReason, szItems, sCC}
        mail.sendMail(Server.MapPath(TEMP_EMAIL_REJECT), szTos, szCcs, "[MMKSI-DNet] Parts - Tolak " & objSppo.RequestNo & " - " & objSppo.Dealer.DealerCode, szFormats)
    End Function

    Private Function GetEED(ByVal ipd As IndentPartDetail) As EstimationEquipDetail
        Dim Sql As String = ""
        Dim objEEDFac As EstimationEquipDetailFacade = New EstimationEquipDetailFacade(User)
        Dim crtEED As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EstimationEquipDetail), "RowStatus", CType(DBRowStatus.Active, Short)))
        Dim objEED As EstimationEquipDetail
        Dim arlEED As New ArrayList


        Sql &= " select distinct(EstimationEquipDetailID)    "
        Sql &= " from EstimationEquipPO eepo   "
        Sql &= " where 1=1   "
        Sql &= "  and eepo.IndentPartDetailID in (  "
        Sql &= "   select ipd.ID   "
        Sql &= "   from IndentPartDetail ipd "
        Sql &= "   where 1=1 "
        Sql &= "    and ipd.IndentPartHeaderID=" & ipd.IndentPartHeader.ID
        Sql &= "    and ipd.SparePartMasterID=" & ipd.SparePartMaster.ID
        Sql &= " )"

        crtEED.opAnd(New Criteria(GetType(EstimationEquipDetail), "ID", MatchType.InSet, "(" & Sql & ")"))
        arlEED = objEEDFac.Retrieve(crtEED)
        If arlEED.Count > 0 Then
            Return CType(arlEED(0), EstimationEquipDetail)
        Else
            Dim oEED As EstimationEquipDetail = New EstimationEquipDetail
            oEED.ID = -1
            oEED.Harga = ipd.Price
            oEED.ConfirmedDate = Now
            oEED.Discount = 0
            Return oEED  ' New IndentPartDetailFacade(User).Retrieve(ipd.ID)
        End If

    End Function

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgDaftarPengajuanPencairanDepositB.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If
        If Not IsNothing(sHelper.GetSession("VDaftarPengajuan")) Then
            arrData = sHelper.GetSession("VDaftarPengajuan")
        End If

        'arrData = New SalesmanHeaderFacade(User).RetrieveByCriteria(crits, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If arrData.Count > 0 Then
            DoDownloadExcel(arrData)
        End If

    End Sub

    Private Sub DoDownloadExcel(ByVal data As ArrayList)
        Dim pck As ExcelPackage = New ExcelPackage()
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("DEPOSIT ""B"" WITHDRAWAL - " & Now.Year)

        Dim headerCell = ws.Cells("A3:P3")
        headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid
        headerCell.Style.Fill.BackgroundColor.SetColor(Color.LightGray)
        Dim headerFont = headerCell.Style.Font
        headerFont.Bold = True

        'ws
        ws.Cells("A3").Value = "NO"
        'ws.Cells("A3").Style.Font.Bold = True
        'ws.Cells("A3").Style.Fill.BackgroundColor.SetColor(Color.Gray)
        ws.Cells("B3").Value = "NO JV"
        'ws.Cells("B3").Style.Font.Bold = True
        'ws.Cells("B3").Style.Fill.BackgroundColor.SetColor(Color.Gray)
        ws.Cells("C3").Value = "DEALER CODE"
        'ws.Cells("B3").Style.Font.Bold = True
        'ws.Cells("B3").Style.Fill.BackgroundColor.SetColor(Color.Gray)
        ws.Cells("D3").Value = "DEALER NAME"
        ws.Cells("E3").Value = "DEALER CITY"
        ws.Cells("F3").Value = "GRADE"
        ws.Cells("G3").Value = "DEALER STATUS"
        ws.Cells("H3").Value = "TANGGAL"
        ws.Cells("I3").Value = "SALDO DEP. B"
        ws.Cells("J3").Value = "JUMLAH PENGAJUAN"
        ws.Cells("K3").Value = "MINIMAL SALDO"
        ws.Cells("L3").Value = "REMAIN DEP. B AMOUNT(RP)"
        ws.Cells("M3").Value = "TIPE"
        ws.Cells("N3").Value = "NO. REG"
        ws.Cells("O3").Value = "NO. PENGAJUAN/ SO/ DN"
        ws.Cells("P3").Value = "KET"
        ws.Cells("Q3").Value = "APPROVE/DISAPPROVE"

        Dim rowStart As Integer = 4

        For Each item As DepositBPencairanHeader In data
            For Each itemDep As DepositBReceipt In item.DepositBReceipts
                If Not IsNothing(itemDep.JVNumber) Then
                    ws.Cells(String.Format("B{0}", rowStart)).Value = itemDep.JVNumber
                Else
                    ws.Cells(String.Format("B{0}", rowStart)).Value = ""
                End If
            Next

            'GRADE
            Dim objDepBPlafonFac As DepositBPlafonFacade = New DepositBPlafonFacade(User)
            Dim objDepositBPlafon As DepositBPlafon = objDepBPlafonFac.RetrieveByDealerID(item.Dealer.ID, Now.Year)

            'DEALER STATUS
            Dim dealerStatus As String = String.Empty
            If item.ProductCategory.ID = 1 Then
                dealerStatus = "PC Dealer"
            Else
                dealerStatus = "Non PC Dealer"
            End If

            'SALDO DEP B
            Dim objDepBHeaderFac As DepositBHeaderFacade = New DepositBHeaderFacade(User)
            Dim objDepBHeader As DepositBHeader = objDepBHeaderFac.RetrieveByDealerID(item.Dealer.ID, "TransactionDate", Sort.SortDirection.DESC)

            'Tipe Penggajuan
            Dim selectedTipe As DepositBEnum.TipePengajuan = CType([Enum].Parse(GetType(DepositBEnum.TipePengajuan), item.TipePengajuan), DepositBEnum.TipePengajuan)
            Dim SelectedTipeName As String = selectedTipe.GetName(GetType(DepositBEnum.TipePengajuan), item.TipePengajuan)

            ws.Cells(String.Format("A{0}", rowStart)).Value = rowStart - 3
            ws.Cells(String.Format("C{0}", rowStart)).Value = item.Dealer.DealerCode
            ws.Cells(String.Format("D{0}", rowStart)).Value = item.Dealer.DealerName
            If Not IsNothing(item.Dealer.City) Then
                ws.Cells(String.Format("E{0}", rowStart)).Value = item.Dealer.City.CityName
            Else
                ws.Cells(String.Format("E{0}", rowStart)).Value = "-"
            End If
            'ws.Cells(String.Format("E{0}", rowStart)).Value = IIf(Not IsNothing(item.Dealer.City), item.Dealer.City.CityName, "-")
            ws.Cells(String.Format("F{0}", rowStart)).Value = objDepositBPlafon.GradeDealer & " DIAMONDS"
            ws.Cells(String.Format("G{0}", rowStart)).Value = dealerStatus
            ws.Cells(String.Format("H{0}", rowStart)).Value = Format(item.CreatedTime, "dd/MM/yyyy")
            ws.Cells(String.Format("I{0}", rowStart)).Value = Format(objDepBHeader.EndBalance, "#,###")
            ws.Cells(String.Format("J{0}", rowStart)).Value = Format(item.DealerAmount, "#,###")
            ws.Cells(String.Format("K{0}", rowStart)).Value = Format(objDepositBPlafon.JumlahPlafon, "#,###")
            ws.Cells(String.Format("L{0}", rowStart)).Value = Format((objDepBHeader.EndBalance - item.DealerAmount - objDepositBPlafon.JumlahPlafon), "#,###")
            ws.Cells(String.Format("M{0}", rowStart)).Value = SelectedTipeName
            ws.Cells(String.Format("N{0}", rowStart)).Value = item.NoReg

            If item.TipePengajuan = DepositBEnum.TipePengajuan.Offset_SP AndAlso Not IsNothing(item.IndentPartHeader) Then
                ws.Cells(String.Format("O{0}", rowStart)).Value = item.IndentPartHeader.RequestNo
            Else
                ws.Cells(String.Format("O{0}", rowStart)).Value = ""
            End If
            For Each itemDep As DepositBPencairanDetail In item.DepositBPencairanDetails
                If Not IsNothing(itemDep.Description) Then
                    ws.Cells(String.Format("P{0}", rowStart)).Value = itemDep.Description
                Else
                    ws.Cells(String.Format("P{0}", rowStart)).Value = ""
                End If
            Next
            ws.Cells(String.Format("Q{0}", rowStart)).Value = ""
            rowStart += 1
        Next

        ws.Cells("A:AZ").AutoFitColumns()

        Dim sFileName As String
        sFileName = "DEPOSIT B PENCAIRAN " & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second

        Response.Clear()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" + sFileName + ".xlsx")
        Response.BinaryWrite(pck.GetAsByteArray())
        Response.End()
    End Sub
End Class

