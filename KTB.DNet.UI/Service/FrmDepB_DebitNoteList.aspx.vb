Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.Security

Public Class FrmDepB_DebitNoteList
    Inherits System.Web.UI.Page

#Region "Private Variables"
    Private arlDepositBDebitNote As ArrayList = New ArrayList
    Private arlDepositBDebitNoteFilter As ArrayList = New ArrayList

    Dim sHelper As New SessionHelper
    Dim _lihat_daftar_debit_note_Privilege As Boolean = False
#End Region

#Region "Event Handlers"

    Private Sub InitiateAuthorization()

        _lihat_daftar_debit_note_Privilege = SecurityProvider.Authorize(Context.User, SR.lihat_daftar_debit_note_Privilege)

        If Not _lihat_daftar_debit_note_Privilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sparepart - Daftar Debit Note")
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then
            Initialize()
            BindStatus()
            ViewState("currentSortColumn") = "Dealer.DealerCode"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            'BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
    End Sub

    Private Sub dgDaftarDebitNote_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDaftarDebitNote.PageIndexChanged
        dgDaftarDebitNote.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgDaftarDebitNote.CurrentPageIndex)
    End Sub

    Private Sub dgDaftarDebitNote_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDaftarDebitNote.SortCommand
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

        dgDaftarDebitNote.SelectedIndex = -1
        dgDaftarDebitNote.CurrentPageIndex = 0
        BindDataGrid(dgDaftarDebitNote.CurrentPageIndex)
    End Sub
#End Region

#Region "Custom Method"


    Private Sub Initialize()
        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)

        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"

        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            'lblSearchDealer.Visible = False
            txtKodeDealer.Attributes.Add("readonly", "readonly")
        End If
    End Sub

    Private Sub AddPeriodCriteria(ByVal criterias As CriteriaComposite, ByVal ColumnName As String)
        Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                    icPeriodeFrom.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                    icPeriodeTo.Value.Day, 0, 0, 0)
        ' dtEnd = dtEnd.AddDays(1)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), ColumnName, MatchType.GreaterOrEqual, Format(dtStart, "yyyy-MM-dd HH:mm:ss")))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), ColumnName, MatchType.LesserOrEqual, Format(dtEnd, "yyyy-MM-dd HH:mm:ss")))
    End Sub


    Private Function IsExist(ByVal DealerCode As String, ByVal arl As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As DepositBDebitNote In arl
            If item.Dealer.DealerCode.Trim().ToUpper() = DealerCode.Trim().ToUpper() Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Sub BindDataGrid(ByVal IndexPage As Integer)
        Dim oDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        'If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    If txtKodeDealer.Text.Trim = String.Empty Then
        '        MessageBox.Show("Tentukan kode dealer terlebih dahulu")
        '        Exit Sub
        '    End If
        'End If
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        Else
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If oDealer.DealerGroup.ID = 21 Then 'single dealer
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "Dealer.ID", MatchType.Exact, oDealer.ID))
                Else
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "Dealer.DealerGroup.ID", MatchType.Exact, oDealer.DealerGroup.ID))
                End If
            End If
        End If
        If CInt(ddlProductCategory.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        End If
        If CInt(ddlStatus.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "Status", MatchType.Exact, CInt(ddlStatus.SelectedValue)))
        End If

        If txtNoDN.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "DNNumber", MatchType.Exact, txtNoDN.Text))
        End If

        AddPeriodCriteria(criterias, "PostingDate")

        Dim strSql As String = ""
        Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                 icPeriodeFrom.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                    icPeriodeTo.Value.Day, 0, 0, 0)


        'strSql = DepositBEnum.RetrieveDebitNote(dtStart, dtEnd, CType(ddlStatus.SelectedValue, DepositBEnum.StatusPencairanDebitNote))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBDebitNote), "ID", MatchType.InSet, "(" & strSql & ")"))

        Dim totalRow As Integer = 0
        arlDepositBDebitNote = New DepositBDebitNoteFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgDaftarDebitNote.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))

        If (arlDepositBDebitNote.Count > 0) Then
            dgDaftarDebitNote.Visible = True
            dgDaftarDebitNote.CurrentPageIndex = IndexPage 
            dgDaftarDebitNote.DataSource = arlDepositBDebitNote
            dgDaftarDebitNote.VirtualItemCount = totalRow
            dgDaftarDebitNote.DataBind()
        Else
            dgDaftarDebitNote.Visible = False

            MessageBox.Show("Data tidak ditemukan")
        End If

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

    Private TotalAmount As Double = 0
    Private Sub dgDaftarDebitNote_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDaftarDebitNote.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim objDepositBDebitNote As DepositBDebitNote = CType(e.Item.DataItem, DepositBDebitNote)
            If Not IsNothing(objDepositBDebitNote) Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarDebitNote.CurrentPageIndex * dgDaftarDebitNote.PageSize)

                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                lblDealerCode.Text = objDepositBDebitNote.Dealer.DealerCode
                Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
                lblDealerName.Text = objDepositBDebitNote.Dealer.DealerName
                Dim lblProduct As Label = CType(e.Item.FindControl("lblProduct"), Label)
                lblProduct.Text = objDepositBDebitNote.ProductCategory.Code

                'Dim lnkAccount As LinkButton = CType(e.Item.FindControl("lnkAccount"), LinkButton)
                'lnkAccount.Attributes("OnClick") = "showPopUp('../PopUp/PopUpDealerBankAccount.aspx?DealerID=" & objDepositBDebitNote.Dealer.ID & "','',500,760,'');"

                'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "DepositBDebitNote.ID", MatchType.Exact, objDepositBDebitNote.ID))
                ''criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBPencairanHeader), "TipePengajuan", MatchType.Exact, CInt(DepositBEnum.TipePengajuan.KewajibanReguler)))

                'Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
                'sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositBPencairanHeader), "NoReg", Sort.SortDirection.DESC))

                'Dim arlDepositBPencarianHeader As ArrayList = New DepositBPencairanHeaderFacade(User).Retrieve(criterias, sortColl)

                'Dim objDepositPencairanHeader As DepositBPencairanHeader
                'If arlDepositBPencarianHeader.Count > 0 Then
                '    objDepositPencairanHeader = arlDepositBPencarianHeader(0)
                'End If

                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                lblStatus.Text = DepositBEnum.GetStringValueStatusPengajuan(objDepositBDebitNote.Status)
                '    If Not IsNothing(objDepositPencairanHeader) Then
                '        If Not IsNothing(objDepositPencairanHeader.Status) Then
                '            If objDepositPencairanHeader.Status = DepositBEnum.StatusPencairan.Selesai Then
                '                Dim critKuitansi As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositBKuitansiPencairan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                '                critKuitansi.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositBKuitansiPencairan), "NoReg", MatchType.Exact, objDepositPencairanHeader.NoReg))
                '                Dim arlKuitansi As ArrayList = New FinishUnit.DepositBKuitansiPencairanFacade(User).Retrieve(critKuitansi)
                '                Dim objKuitansi As DepositBPencairanHeader
                '                If arlKuitansi.Count > 0 Then
                '                    objKuitansi = arlKuitansi(0)
                '                    If objKuitansi.Status = DepositBEnum.StatusKuitansiKTB.Selesai Then
                '                        lblStatus.Text = "Sudah Cair" ' "Proses Cair"
                '                    ElseIf objKuitansi.Status = DepositBEnum.StatusKuitansiKTB.Cair Then
                '                        lblStatus.Text = "Sudah Cair"
                '                    Else
                '                        lblStatus.Text = "Proses"
                '                    End If
                '                Else
                '                    lblStatus.Text = "Proses"
                '                End If

                '            ElseIf objDepositPencairanHeader.Status = DepositBEnum.StatusPencairan.Blok Or objDepositPencairanHeader.Status = DepositBEnum.StatusPencairan.Tolak Then
                '                lblStatus.Text = "Belum Cair"
                '            Else
                '                lblStatus.Text = "Proses"
                '            End If
                '        Else
                '            lblStatus.Text = "Belum Cair"
                '        End If
                '    Else
                '        lblStatus.Text = "Belum Cair"
                '    End If

                'ElseIf (e.Item.ItemType = ListItemType.Footer) Then
                '    e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Right
                '    e.Item.Cells(4).Text = "Total:"
                '    e.Item.Cells(7).Text = IIf(TotalAmount = 0, 0, TotalAmount.ToString("#,###"))
                'End If
            End If
        End If
    End Sub

#End Region
End Class
