Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.IO
Imports System.Text

Public Class FrmDepB_KuitansiPencairanList
    Inherits System.Web.UI.Page


#Region "Private Variables"
    Private arlDepositBReceipt As ArrayList = New ArrayList
    Private arlDepositBReceiptFilter As ArrayList = New ArrayList

    Private _DepositBReceiptFacade As New DepositBReceiptFacade(User)

    Dim sHelper As New SessionHelper
    Dim objUserInfo As UserInfo
    Dim IsDealer As Boolean = True
    Const DocTypePengajuan = 1
    Const DocTypeKuitansi = 2

    Dim NoKuitansi As String
    Dim PeriodeFromKuitansi As String
    Dim PeriodeToKuitansi As String
    Dim NoPengajuan As String
    Dim PeriodeFromPengajuan As String
    Dim PeriodeToPengajuan As String
#End Region

#Region "Custom Method"

    Private Sub AddPeriodCriteria(ByVal criterias As CriteriaComposite, ByVal ColumnName As String, ByVal icstart As KTB.DNet.WebCC.IntiCalendar, ByVal icEnd As KTB.DNet.WebCC.IntiCalendar)
        Dim dtStart As DateTime = New DateTime(icstart.Value.Year, icstart.Value.Month, _
                                    icstart.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icEnd.Value.Year, icEnd.Value.Month, _
                                    icEnd.Value.Day, 0, 0, 0)
        dtEnd = dtEnd.AddDays(1)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), ColumnName, MatchType.GreaterOrEqual, dtStart))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), ColumnName, MatchType.Lesser, dtEnd))
    End Sub

    ''Private Function IsExist(ByVal DealerCode As String, ByVal arl As ArrayList) As Boolean
    ''    Dim bResult As Boolean = False
    ''    For Each item As DepositBReceipt In arl
    ''        If item.Dealer.DealerCode.Trim().ToUpper() = DealerCode.Trim().ToUpper() Then
    ''            bResult = True
    ''            Exit For
    ''        End If
    ''    Next
    ''    Return bResult
    ''End Function

    Sub BindDatagridDaftarPencairan(ByVal idxPage As Integer)

        Dim oDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "DepositBPencairanHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        Else
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If oDealer.DealerGroup.ID = 21 Then 'single dealer
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "DepositBPencairanHeader.Dealer.ID", MatchType.Exact, oDealer.ID))
                Else
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "DepositBPencairanHeader.Dealer.DealerGroup.ID", MatchType.Exact, oDealer.DealerGroup.ID))
                End If

            End If
        End If
        Dim o As New DepositBReceipt
        'o.NoRegKuitansi

        If txtNoKuitansi.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "NomorKuitansi", MatchType.[Partial], txtNoKuitansi.Text.Trim))
        End If

        If txtNoReg.Text.Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "NoRegKuitansi", MatchType.[Partial], txtNoReg.Text.Trim()))
        End If

        If rbPeriodeKuitansi.Checked Then
            AddPeriodCriteria(criterias, "TanggalKuitansi", icPeriodeFromKuitansi, icPeriodeToKuitansi)
        End If

        If rbPeriodePengajuan.Checked Then
            AddPeriodCriteria(criterias, "DepositBPencairanHeader.CreatedTime", icPeriodeFromPengajuan, icPeriodeToPengajuan)
        End If

        'If txtNoPengajuan.Text.Length > 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "DepositBPencairanHeader.NoSurat", MatchType.[Partial], txtNoPengajuan.Text.Trim))
        'End If

        If ddlJenisStatus.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "DepositBPencairanHeader.Status", MatchType.Exact, ddlJenisStatus.SelectedValue))
        End If

        If ddlTipePengajuan.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "DepositBPencairanHeader.TipePengajuan", MatchType.Exact, ddlTipePengajuan.SelectedValue))
        End If

        If CInt(ddlProductCategory.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "DepositBPencairanHeader.ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        End If

        'If chkTglPencairan.Checked = True Then
        '    Dim TglPencairan As New DateTime(CInt(icTglPencairan.Value.Year), CInt(icTglPencairan.Value.Month), CInt(icTglPencairan.Value.Day), 0, 0, 0)
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "TglPencairan", MatchType.Exact, Format(TglPencairan, "yyyy-MM-dd HH:mm:ss")))
        'End If

        'If IsDealer Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "Status", MatchType.InSet, GetStatusPencairanDealerEnumValues()))
        'Else
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBReceipt), "Status", MatchType.InSet, GetStatusPencairanKTBEnumValues()))
        'End If

        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
            sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositBReceipt), ViewState("currSortColumn"), ViewState("currSortDirection")))
        Else
            sortColl = Nothing
        End If

        Dim totalRow As Integer
        arlDepositBReceipt = New DepositBReceiptFacade(User).RetrieveActiveList(criterias, idxPage + 1, dgDaftarPengajuanKuitansiDepositB.PageSize, totalRow, ViewState("currSortColumn"), ViewState("currSortDirection"))
        'For Each item As DepositBReceipt In arlDepositBReceipt
        '    'If (Not IsExist(item.Dealer.DealerCode, arlDepositBReceiptFilter)) Then
        '    arlDepositBReceiptFilter.Add(item)
        '    'End If
        'Next
        sHelper.SetSession("VDepositBkuntansi", arlDepositBReceipt)

        Dim arlDepositBReceiptDL As ArrayList = New DepositBReceiptFacade(User).Retrieve(criterias)
        sHelper.SetSession("VDepositBkuntansiDL", arlDepositBReceiptDL)

        If (arlDepositBReceipt.Count > 0) Then
            dgDaftarPengajuanKuitansiDepositB.Visible = True
            dgDaftarPengajuanKuitansiDepositB.DataSource = arlDepositBReceipt
            dgDaftarPengajuanKuitansiDepositB.VirtualItemCount = totalRow
            dgDaftarPengajuanKuitansiDepositB.DataBind()
            btnDownload.Enabled = True
        Else
            dgDaftarPengajuanKuitansiDepositB.Visible = False
            btnDownload.Enabled = False
            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Private Sub InitNonMandatoryObject()
        txtNoKuitansi.Enabled = True
        rbPeriodeKuitansi.Checked = False
        icPeriodeFromKuitansi.Enabled = False
        icPeriodeToKuitansi.Enabled = False
        rbPeriodePengajuan.Checked = False
        icPeriodeFromPengajuan.Enabled = False
        icPeriodeToPengajuan.Enabled = False
    End Sub

    Private Function GetStatusPencairanKTBEnumValues() As String
        Dim strResult As String = " ("
        Dim item As String = String.Empty

        For Each item In [Enum].GetValues(GetType(DepositBEnum.StatusPencairan))
            strResult = strResult & item.ToString & ","
        Next
        strResult = Left(strResult, strResult.Length - 1) & ")"
        Return strResult
    End Function

    Private Function GetStatusPencairanDealerEnumValues() As String
        Dim strResult As String = " ("
        Dim item As String = String.Empty

        For Each item In [Enum].GetValues(GetType(DepositBEnum.StatusPencairan))
            strResult = strResult & item.ToString & ","
        Next
        strResult = Left(strResult, strResult.Length - 1) & ")"
        Return strResult
    End Function

    Private Function InsertHistory(ByVal NoSurat As String, ByVal OldStatus As Integer, ByVal NewStatus As Integer, ByVal DocType As Integer)
        'Dim objHistoryDepositBReceipt As DepositBStatusHistory = New DepositBStatusHistory
        'objHistoryDepositBReceipt.DocNumber = NoSurat
        'objHistoryDepositBReceipt.OldStatus = OldStatus
        'objHistoryDepositBReceipt.NewStatus = NewStatus
        'objHistoryDepositBReceipt.DocType = DocType
        'Dim nResult As Integer = -1
        'nResult = New DepositBStatusHistoryFacade(User).Insert(objHistoryDepositBReceipt)
        'Return nResult
        Return 1
    End Function

    Private Function GetSelectedKuitansis(ByVal dg As DataGrid) As String
        Dim i As Integer = 0
        Dim strResult As String = "("

        For Each item As DataGridItem In dgDaftarPengajuanKuitansiDepositB.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If (chk.Checked) Then
                strResult = strResult & dgDaftarPengajuanKuitansiDepositB.DataKeys().Item(i) & ","
            End If
            i = i + 1
        Next
        strResult = Left(strResult, strResult.Length - 1) & ")"
        Return strResult
    End Function

#End Region

#Region "Event Handlers"

    Dim _input_kuitansi_pencairan_Privilege As Boolean = False
    Dim _lihat_daftar_kuitansi_pencairan_Privilege As Boolean = False

    Private Sub InitiateAuthorization()
        _input_kuitansi_pencairan_Privilege = SecurityProvider.Authorize(Context.User, SR.input_kuitansi_pencairan_Privilege)
        _lihat_daftar_kuitansi_pencairan_Privilege = SecurityProvider.Authorize(Context.User, SR.lihat_daftar_kuitansi_pencairan_Privilege)


        If Not (_lihat_daftar_kuitansi_pencairan_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Deposit A - Lihat Kuitansi Pencairan")
        End If

        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then

        Else


        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)


        objUserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            IsDealer = False
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        Else
            IsDealer = True
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            txtKodeDealer.Attributes.Add("readonly", "readonly")
        End If

        InitiateAuthorization()
        'btnTransferSAP.Attributes("onclick") = "GetSelectedKuitansi();"

        If Not IsPostBack Then
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
            btnDownload.Enabled = False
            BindTipePengajuan()
            BindJenisStatus()
            InitNonMandatoryObject()
            If Session("IsBindDataGrid") Then
                If Request.QueryString("NoKuitansi") Is Nothing = False Then
                    If Request.QueryString("NoKuitansi").Length > 0 Then
                        txtNoKuitansi.Enabled = True
                        txtNoKuitansi.Text = Request.QueryString("NoKuitansi")
                    End If
                End If
                If Request.QueryString("PeriodeFromKuitansi") Is Nothing = False Then
                    If Request.QueryString("PeriodeFromKuitansi").Length > 0 Then
                        icPeriodeFromKuitansi.Value = Format(CDate(Request.QueryString("PeriodeFromKuitansi")), "dd/MM/yyyy")
                        If icPeriodeFromKuitansi.Value <> Now.Date Then
                            rbPeriodeKuitansi.Checked = True
                            icPeriodeFromKuitansi.Enabled = True
                            txtNoKuitansi.Enabled = False
                        End If
                    End If
                End If
                If Request.QueryString("PeriodeToKuitansi") Is Nothing = False Then
                    If Request.QueryString("PeriodeToKuitansi").Length > 0 Then
                        icPeriodeToKuitansi.Value = Format(CDate(Request.QueryString("PeriodeToKuitansi")), "dd/MM/yyyy")
                        If icPeriodeToKuitansi.Value <> Now.Date Then
                            rbPeriodeKuitansi.Checked = True
                            icPeriodeToKuitansi.Enabled = True
                            txtNoKuitansi.Enabled = False
                        End If
                    End If
                End If
                If Request.QueryString("PeriodeFromPengajuan") Is Nothing = False Then
                    If Request.QueryString("PeriodeFromPengajuan").Length > 0 Then
                        icPeriodeFromPengajuan.Value = Format(CDate(Request.QueryString("PeriodeFromPengajuan")), "dd/MM/yyyy")
                        If icPeriodeFromPengajuan.Value <> Now.Date Then
                            rbPeriodePengajuan.Checked = True
                            icPeriodeFromPengajuan.Enabled = True
                            txtNoKuitansi.Enabled = False
                        End If
                    End If
                End If
                If Request.QueryString("PeriodeToPengajuan") Is Nothing = False Then
                    If Request.QueryString("PeriodeToPengajuan").Length > 0 Then
                        icPeriodeToPengajuan.Value = Format(CDate(Request.QueryString("PeriodeToPengajuan")), "dd/MM/yyyy")
                        If icPeriodeToPengajuan.Value <> Now.Date Then
                            rbPeriodePengajuan.Checked = True
                            icPeriodeToPengajuan.Enabled = True
                            txtNoKuitansi.Enabled = False
                        End If
                    End If
                End If
                Session("IsBindDataGrid") = False
                BindDatagridDaftarPencairan(0)
            End If
        Else
        End If
    End Sub

    Sub BindTipePengajuan()

        ddlTipePengajuan.Items.Clear()

        Dim items As New ArrayList
        Dim _arrStatus As ArrayList = New DepositBEnum().RetrieveTipePengajuan()
        For Each item As EnumProperty In _arrStatus
            items.Add(item)
        Next
        ddlTipePengajuan.DataSource = items
        ddlTipePengajuan.DataTextField = "NameType"
        ddlTipePengajuan.DataValueField = "ValType"
        ddlTipePengajuan.DataBind()
    End Sub

    Sub BindJenisStatus()
        ddlJenisStatus.Items.Clear()

        Dim items As New ArrayList
        'Dim _arrStatus As ArrayList = New DepositBEnum().RetrieveStatuspencairan(EnumDealerTittle.DealerTittle.KTB_DEALER, True)
        Dim _arrStatus As ArrayList = New DepositBEnum().RetrieveStatuspencairanForMenuKuitansi()
        For Each item As EnumProperty In _arrStatus
            items.Add(item)
        Next
        ddlJenisStatus.DataSource = items
        ddlJenisStatus.DataTextField = "NameType"
        ddlJenisStatus.DataValueField = "ValType"
        ddlJenisStatus.DataBind()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'Dim oDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        'If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    If txtKodeDealer.Text.Trim = String.Empty Then
        dgDaftarPengajuanKuitansiDepositB.CurrentPageIndex = 0
        BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositB.CurrentPageIndex)
        '    End If
        'End If

    End Sub

    Private Sub dgDaftarPengajuanKuitansiDepositB_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDaftarPengajuanKuitansiDepositB.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarPengajuanKuitansiDepositB.CurrentPageIndex * dgDaftarPengajuanKuitansiDepositB.PageSize)

            Dim objDepositBReceipt As DepositBReceipt = CType(e.Item.DataItem, DepositBReceipt)

            'If objDepositBReceipt.IsTransfer = 1 Then
            '    e.Item.BackColor = Color.FromArgb(232, 232, 232)
            'End If

            Dim intStatus As Integer = CInt(objDepositBReceipt.DepositBPencairanHeader.Status)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblTglpengajuan As Label = CType(e.Item.FindControl("lblTglpengajuan"), Label)

            lblTglpengajuan.Text = objDepositBReceipt.DepositBPencairanHeader.CreatedTime.ToString("dd-MM-yyyy")

            If IsDealer Then
                Dim selectedStatusDealer As DepositBEnum.StatusPencairan = CType([Enum].Parse(GetType(DepositBEnum.StatusPencairan), intStatus), DepositBEnum.StatusPencairan)
                Dim SelectedStatusDealerName As String = selectedStatusDealer.GetName(GetType(DepositBEnum.StatusPencairan), selectedStatusDealer)
                lblStatus.Text = SelectedStatusDealerName
            Else
                Dim selectedStatusKTB As DepositBEnum.StatusPencairan = CType([Enum].Parse(GetType(DepositBEnum.StatusPencairan), intStatus), DepositBEnum.StatusPencairan)
                Dim SelectedStatusKTBName As String = selectedStatusKTB.GetName(GetType(DepositBEnum.StatusPencairan), selectedStatusKTB)
                lblStatus.Text = SelectedStatusKTBName
            End If

            Dim lblTipe As Label = CType(e.Item.FindControl("lblTipe"), Label)
            lblTipe.Text = DepositBEnum.GetStringValueTipePengajuan(objDepositBReceipt.DepositBPencairanHeader.TipePengajuan)

            'Dim lbViewFlow As LinkButton = CType(e.Item.FindControl("lbViewFlow"), LinkButton)
            'lbViewFlow.Attributes("OnClick") = "showPopUp('../PopUp/PopUpFlowPencairanDepositB.aspx?DealerID=" & objDepositBReceipt.DepositBPencairanHeader.Dealer.ID & "&NoReg=" & objDepositBReceipt.NoRegKuitansi.ToString & "&NoSurat=" & objDepositBReceipt.NomorKuitansi.ToString & " ','',500,760,'');"

            'Dim lbViewStatusHistory As LinkButton = CType(e.Item.FindControl("lbViewStatus"), LinkButton)
            'lbViewStatusHistory.Attributes("OnClick") = "showPopUp('../PopUp/PopUpHistoryKuitansiPencairanDepositB.aspx?DealerID=" & objDepositBReceipt.DepositBPencairanHeader.Dealer.ID & "&NoReg=" & objDepositBReceipt.NoRegKuitansi.ToString & "&NoSurat=" & objDepositBReceipt.DepositBPencairanHeader.NoReferensi.ToString & "','',500,760,'');"

        ElseIf (e.Item.ItemType = ListItemType.Footer) Then
        End If
    End Sub

    Private Sub dgDaftarPengajuanKuitansiDepositB_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDaftarPengajuanKuitansiDepositB.PageIndexChanged
        dgDaftarPengajuanKuitansiDepositB.CurrentPageIndex = e.NewPageIndex
        BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositB.CurrentPageIndex)
    End Sub

    Private Sub dgDaftarPengajuanKuitansiDepositB_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDaftarPengajuanKuitansiDepositB.ItemCommand
        Dim nResult As Integer = -1

        Select Case e.CommandName
            'Case "Validasi"
            '    Dim objDepositBReceipt As DepositBReceipt = _DepositBReceiptFacade.Retrieve(CInt(e.CommandArgument))
            '    'insert history
            '    nResult = InsertHistory(objDepositBReceipt.NoSurat, objDepositBReceipt.DepositBPencairanHeader.Status, DepositBEnum.StatusPencairan.Validasi, DocTypeKuitansi)
            '    If nResult > -1 Then
            '        objDepositBReceipt.DepositBPencairanHeader.Status = DepositBEnum.StatusPencairan.Validasi
            '        nResult = _DepositBReceiptFacade.Update(objDepositBReceipt)
            '        If nResult <> -1 Then
            '            'MessageBox.Show("Berhasil update menjadi " & objDepositBReceipt.DepositBPencairanHeader.Status.ToString)
            '            MessageBox.Show("Ubah Status berhasil")
            '        Else
            '            MessageBox.Show(SR.UpdateFail())
            '        End If
            '    End If
            '    BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositB.CurrentPageIndex)
            'Case "BatalValidasi"
            '    Dim objDepositBReceipt As DepositBReceipt = _DepositBReceiptFacade.Retrieve(CInt(e.CommandArgument))
            '    nResult = InsertHistory(objDepositBReceipt.NoSurat, objDepositBReceipt.DepositBPencairanHeader.Status, DepositBEnum.StatusPencairan.Baru, DocTypeKuitansi)
            '    If nResult > -1 Then
            '        objDepositBReceipt.DepositBPencairanHeader.Status = DepositBEnum.StatusPencairan.Baru
            '        nResult = _DepositBReceiptFacade.Update(objDepositBReceipt)
            '        If nResult <> -1 Then
            '            'MessageBox.Show("Berhasil update menjadi " & objDepositBReceipt.DepositBPencairanHeader.Status.ToString)
            '            MessageBox.Show("Ubah Status berhasil")
            '        Else
            '            MessageBox.Show(SR.UpdateFail())
            '        End If
            '    End If
            '    BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositB.CurrentPageIndex)
            'Case "Konfirmasi"
            '    Dim objDepositBReceipt As DepositBReceipt = _DepositBReceiptFacade.Retrieve(CInt(e.CommandArgument))
            '    nResult = InsertHistory(objDepositBReceipt.NoSurat, objDepositBReceipt.DepositBPencairanHeader.Status, DepositBEnum.StatusPencairan.Konfirmasi, DocTypeKuitansi)
            '    If nResult > -1 Then
            '        'Save Header
            '        objDepositBReceipt.DepositBPencairanHeader.Status = DepositBEnum.StatusPencairan.Konfirmasi
            '        nResult = _DepositBReceiptFacade.Update(objDepositBReceipt)
            '        If nResult <> -1 Then
            '            'MessageBox.Show("Berhasil update menjadi " & objDepositBReceipt.DepositBPencairanHeader.Status.ToString)
            '            MessageBox.Show("Ubah Status berhasil")
            '        Else
            '            MessageBox.Show(SR.UpdateFail())
            '        End If
            '    End If
            '    BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositB.CurrentPageIndex)
            Case "ViewDetail"
                NoKuitansi = txtNoKuitansi.Text
                PeriodeFromKuitansi = Format(icPeriodeFromKuitansi.Value, "dd-MM-yyyy").ToString
                PeriodeToKuitansi = Format(icPeriodeToKuitansi.Value, "dd-MM-yyyy").ToString
                PeriodeFromPengajuan = Format(icPeriodeFromPengajuan.Value, "dd-MM-yyyy").ToString
                PeriodeToPengajuan = Format(icPeriodeToPengajuan.Value, "dd-MM-yyyy").ToString
                Dim strKwitansi As String = "../Service/FrmDepB_KuitansiPencairan.aspx?id=" & e.CommandArgument & "&NoKuitansi=" & NoKuitansi & "&PeriodeFromKuitansi=" & PeriodeFromKuitansi & "&PeriodeToKuitansi=" & PeriodeToKuitansi & "&PeriodeFromPengajuan=" & PeriodeFromPengajuan & "&PeriodeToPengajuan=" & PeriodeToPengajuan
                sHelper.SetSession("BackKwitansiDepositB", strKwitansi)
                Server.Transfer(sHelper.GetSession("BackKwitansiDepositB"))
                'Server.Transfer("../Service/FrmDepB_KuitansiPencairan.aspx?id=" & e.CommandArgument & "&NoKuitansi=" & NoKuitansi & "&PeriodeFromKuitansi=" & PeriodeFromKuitansi & "&PeriodeToKuitansi=" & PeriodeToKuitansi & "&PeriodeFromPengajuan=" & PeriodeFromPengajuan & "&PeriodeToPengajuan=" & PeriodeToPengajuan)
        End Select
    End Sub

    Private Sub rbPeriodeKuitansi_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbPeriodeKuitansi.CheckedChanged
        If rbPeriodeKuitansi.Checked Then
            txtNoKuitansi.Enabled = False
            txtNoKuitansi.Text = ""
            icPeriodeFromKuitansi.Enabled = True
            icPeriodeToKuitansi.Enabled = True
            icPeriodeFromPengajuan.Enabled = False
            icPeriodeToPengajuan.Enabled = False
        End If
    End Sub

    Private Sub rbPeriodePengajuan_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbPeriodePengajuan.CheckedChanged
        If rbPeriodePengajuan.Checked Then
            txtNoKuitansi.Enabled = False
            txtNoKuitansi.Text = ""
            icPeriodeFromKuitansi.Enabled = False
            icPeriodeToKuitansi.Enabled = False
            icPeriodeFromPengajuan.Enabled = True
            icPeriodeToPengajuan.Enabled = True
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arlPF As New ArrayList
        Dim strKuitansiIDs As String = GetSelectedKuitansis(dgDaftarPengajuanKuitansiDepositB)
        If strKuitansiIDs = ")" Then
            arlPF = CType(sHelper.GetSession("VDepositBkuntansiDL"), ArrayList)
        End If

        If strKuitansiIDs.Length > 1 Then
            Dim arlDepositBReceipt As ArrayList

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositBReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositBReceipt), "ID", MatchType.InSet, strKuitansiIDs))
            arlDepositBReceipt = New DepositBReceiptFacade(User).Retrieve(criterias)

            DoDownload(arlDepositBReceipt)
        Else
            DoDownload(arlPF)
        End If
    End Sub

    Private Sub DoDownload(ByVal arlDPK As ArrayList)
        Dim sFileName As String
        sFileName = "Daftar Kuitansi Pencairan [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "]"

        '-- Temp file must be a randomly named file!
        Dim oFile As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(oFile)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(oFile, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteDataToExcell(sw, arlDPK)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub WriteDataToExcell(ByVal sw As StreamWriter, ByVal arlDPK As ArrayList)

        Dim dealer As String = txtKodeDealer.Text
        Dim tipePengajuan As String = IIf(ddlTipePengajuan.SelectedIndex > 0, ddlTipePengajuan.SelectedItem.Text, "Semua")
        Dim produk As String = IIf(ddlProductCategory.SelectedIndex > 0, ddlProductCategory.SelectedItem.Text, "Semua")
        Dim status As String = IIf(ddlJenisStatus.SelectedIndex > 0, ddlJenisStatus.SelectedItem.Text, "Semua")
        Dim tglKuitansi As String = IIf(rbPeriodeKuitansi.Checked, icPeriodeFromKuitansi.Value.ToString("dd/MM/yyyy") & " - " & icPeriodeToKuitansi.Value.ToString("dd/MM/yyyy"), "-")
        Dim tglpengajuan As String = IIf(rbPeriodePengajuan.Checked, icPeriodeFromPengajuan.Value.ToString("dd/MM/yyyy") & " - " & icPeriodeToPengajuan.Value.ToString("dd/MM/yyyy"), "-")

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("DAFTAR STATUS PENCAIRAN")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append("Dealer" & tab)
        itemLine.Append(": " & dealer & tab & tab)
        itemLine.Append("" & tab & tab)
        itemLine.Append("Tgl. Kuitansi" & tab)
        itemLine.Append(": " & tglKuitansi & tab & tab)
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append("Tipe Pengajuan" & tab)
        itemLine.Append(": " & tipePengajuan & tab & tab)
        itemLine.Append("" & tab & tab)
        itemLine.Append("Tgl. Pengajuan" & tab)
        itemLine.Append(": " & tglpengajuan & tab & tab)
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append("Produk" & tab)
        itemLine.Append(": " & produk & tab & tab)
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append("Status" & tab)
        itemLine.Append(": " & status & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")

        If (arlDPK.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("N0" & tab)
            itemLine.Append("KODE DEALER" & tab)
            itemLine.Append("NAMA DEALER" & tab)
            itemLine.Append("PRODUK" & tab)
            itemLine.Append("TIPE" & tab)
            itemLine.Append("STATUS" & tab)
            itemLine.Append("NO. KUITANSI" & tab)
            itemLine.Append("NO. REG. KUITANSI" & tab)
            itemLine.Append("TGL. KUITANSI (DD/MM/YYYY)" & tab)
            itemLine.Append("NO. REF PENGAJUAN" & tab)
            itemLine.Append("TGL. PENGAJUAN (DD/MM/YYYY)" & tab)
            itemLine.Append("NO. JV" & tab)
            itemLine.Append("AMOUNT" & tab)
            itemLine.Append("KETERANGAN" & tab)

            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Try
                For Each item As DepositBReceipt In arlDPK
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item.DepositBPencairanHeader.Dealer.DealerCode & tab)
                    itemLine.Append(item.DepositBPencairanHeader.Dealer.DealerName & tab)
                    itemLine.Append(item.DepositBPencairanHeader.ProductCategory.Code & tab)
                    Dim tipe As String = DepositBEnum.GetStringValueTipePengajuan(item.DepositBPencairanHeader.TipePengajuan)
                    itemLine.Append(tipe & tab)
                    If IsDealer Then
                        Dim selectedStatusDealer As DepositBEnum.StatusPencairan = CType([Enum].Parse(GetType(DepositBEnum.StatusPencairan), CInt(item.DepositBPencairanHeader.Status)), DepositBEnum.StatusPencairan)
                        Dim SelectedStatusDealerName As String = selectedStatusDealer.GetName(GetType(DepositBEnum.StatusPencairan), selectedStatusDealer)
                        itemLine.Append(SelectedStatusDealerName & tab)
                    Else
                        Dim selectedStatusKTB As DepositBEnum.StatusPencairan = CType([Enum].Parse(GetType(DepositBEnum.StatusPencairan), CInt(item.DepositBPencairanHeader.Status)), DepositBEnum.StatusPencairan)
                        Dim SelectedStatusKTBName As String = selectedStatusKTB.GetName(GetType(DepositBEnum.StatusPencairan), selectedStatusKTB)
                        itemLine.Append(SelectedStatusKTBName & tab)
                    End If
                    If Not IsNothing(item.NomorKuitansi) Then
                        itemLine.Append(item.NomorKuitansi.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.NoRegKuitansi) Then
                        itemLine.Append(item.NoRegKuitansi.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.TanggalKuitansi) Then
                        itemLine.Append(item.TanggalKuitansi.ToString("dd/MM/yyyy") & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.DepositBPencairanHeader) Then
                        itemLine.Append(item.DepositBPencairanHeader.NoReferensi.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.DepositBPencairanHeader) Then
                        itemLine.Append(item.DepositBPencairanHeader.CreatedTime.ToString("dd/MM/yyyy") & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.JVNumber) Then
                        itemLine.Append(item.JVNumber.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    If Not IsNothing(item.DepositBPencairanHeader) Then
                        itemLine.Append(Decimal.Round(item.DepositBPencairanHeader.ApprovalAmount, 0).ToString() & tab)
                    Else
                        itemLine.Append("0" & tab)
                    End If
                    If Not IsNothing(item.Keterangan) Then
                        itemLine.Append(item.Keterangan.ToString & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If

                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub

    Private Sub UpdateStatus(ByVal JenisStatus As String)
        Dim nResult As Integer = -1
        Dim intStatus As Integer
        Dim iSuccess As Integer = 0

        'Select Case JenisStatus
        '    Case "Validasi"
        '        intStatus = DepositBEnum.StatusPencairan.Validasi
        '    Case "BatalValidasi"
        '        intStatus = DepositBEnum.StatusPencairan.Baru
        '    Case "Konfirmasi"
        '        intStatus = DepositBEnum.StatusPencairan.Konfirmasi
        '    Case "BatalKonfirmasi"
        '        intStatus = DepositBEnum.StatusPencairan.Validasi
        '    Case "Hapus"
        '        intStatus = DepositBEnum.StatusPencairan.Hapus
        'End Select

        For Each item As DataGridItem In dgDaftarPengajuanKuitansiDepositB.Items
            Dim chkCek As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If chkCek.Checked Then
                Dim ArrPengajuan = New ArrayList
                If Not sHelper.GetSession("VDepositBkuntansi") Is Nothing Then
                    ArrPengajuan = sHelper.GetSession("VDepositBkuntansi")
                    Dim objDepositBReceipt As DepositBReceipt = CType(ArrPengajuan(item.ItemIndex), DepositBReceipt)
                    Dim Valid As Boolean = True
                    If objDepositBReceipt.DepositBPencairanHeader.Status <> DepositBEnum.StatusPencairan.Selesai AndAlso objDepositBReceipt.DepositBPencairanHeader.Status <> DepositBEnum.StatusPencairan.Cair Then
                        'Select Case intStatus
                        '    Case DepositBEnum.StatusPencairan.Validasi
                        '        If objDepositBReceipt.DepositBPencairanHeader.Status = DepositBEnum.StatusPencairan.Baru Or objDepositBReceipt.DepositBPencairanHeader.Status = DepositBEnum.StatusPencairan.Konfirmasi Then
                        '            objDepositBReceipt.DepositBPencairanHeader.Status = intStatus
                        '        End If
                        '    Case DepositBEnum.StatusPencairan.Baru
                        '        If objDepositBReceipt.DepositBPencairanHeader.Status = DepositBEnum.StatusPencairan.Validasi Then
                        '            objDepositBReceipt.DepositBPencairanHeader.Status = intStatus
                        '        End If
                        '    Case DepositBEnum.StatusPencairan.Konfirmasi
                        '        If objDepositBReceipt.DepositBPencairanHeader.Status = DepositBEnum.StatusPencairan.Validasi Then
                        '            objDepositBReceipt.DepositBPencairanHeader.Status = intStatus
                        '        End If
                        '    Case Else

                        '        Valid = False
                        'End Select
                        objDepositBReceipt.DepositBPencairanHeader.Status = intStatus
                        'If Valid Then
                        nResult = _DepositBReceiptFacade.Update(objDepositBReceipt)
                        'End If

                        'If nResult <> -1 AndAlso Valid Then
                        '    'khusus untuk yg hapus, ubah status di DepositBPencairanHeader menjadi setuju(11) untuk diajukan kembali
                        '    If objDepositBReceipt.RowStatus = -1 Then
                        '        Dim objDepositBPencairanHFacade As DepositBPencairanHeaderFacade = New DepositBPencairanHeaderFacade(User)
                        '        Dim objDepositBPencairanH As DepositBPencairanHeader = objDepositBPencairanHFacade.Retrieve(objDepositBReceipt.DepositBPencairanHeader.ID)
                        '        Dim iUpdate As Integer
                        '        objDepositBPencairanH.Status = DepositBEnum.StatusPencairan.Setuju ' 11 ' Status setuju
                        '        iUpdate = objDepositBPencairanHFacade.Update(objDepositBPencairanH)
                        '    End If


                        '    'insert history              
                        '    nResult = InsertHistory(objDepositBReceipt.NamaKuitansi, objDepositBReceipt.DepositBPencairanHeader.Status, intStatus, DocTypeKuitansi)
                        '    If nResult > -1 Then
                        '        'MessageBox.Show("Berhasil update menjadi " & objDepositBReceipt.DepositBPencairanHeader.Status.ToString)
                        '        'MessageBox.Show("Ubah Status berhasil")
                        '    End If
                        'Else
                        '    MessageBox.Show(SR.UpdateFail())
                        '    Exit Sub
                        'End If
                    End If
                End If
            End If
        Next
        BindDatagridDaftarPencairan(dgDaftarPengajuanKuitansiDepositB.CurrentPageIndex)

    End Sub

#End Region

#Region "Cek Privilege"

#End Region


End Class

