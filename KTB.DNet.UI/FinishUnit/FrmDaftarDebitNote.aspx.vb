Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class FrmDaftarDebitNote
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgDaftarDebitNote As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCode As System.Web.UI.WebControls.Label
    Protected WithEvents icPeriodeFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPeriodeTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Private arlDebitNote As ArrayList = New ArrayList
    Private arlDebitNoteFilter As ArrayList = New ArrayList

    Dim sHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub AddPeriodCriteria(ByVal criterias As CriteriaComposite, ByVal ColumnName As String)
        Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                    icPeriodeFrom.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                    icPeriodeTo.Value.Day, 0, 0, 0)
        ' dtEnd = dtEnd.AddDays(1)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DebitNote), ColumnName, MatchType.GreaterOrEqual, Format(dtStart, "yyyy-MM-dd HH:mm:ss")))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DebitNote), ColumnName, MatchType.LesserOrEqual, Format(dtEnd, "yyyy-MM-dd HH:mm:ss")))
    End Sub


    Private Function IsExist(ByVal DealerCode As String, ByVal arl As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As DebitNote In arl
            If item.Dealer.DealerCode.Trim().ToUpper() = DealerCode.Trim().ToUpper() Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Sub BindDataGrid(ByVal IndexPage As Integer)
        Dim oDealer As Dealer = CType(sHelper.GetSession("DEALER"), Dealer)
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If txtKodeDealer.Text.Trim = String.Empty Then
                MessageBox.Show("Tentukan kode dealer terlebih dahulu")
                Exit Sub
            End If
        End If
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If
        If CInt(ddlProductCategory.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        End If


        AddPeriodCriteria(criterias, "PostingDate")

        'CR DEposit A, By Ali
        Dim strSql As String = ""
        Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, _
                                 icPeriodeFrom.Value.Day, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, _
                                    icPeriodeTo.Value.Day, 0, 0, 0)


        strSql = EnumDepositA.RetrieveDebitNote(dtStart, dtEnd, CType(ddlStatus.SelectedValue, EnumDepositA.StatusPencairanDebitNote))

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "ID", MatchType.InSet, "(" & strSql & ")"))
        'ENd Of CR Deposit A, By Ali

        Dim totalRow As Integer = 0
        arlDebitNote = New FinishUnit.DebitNoteFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgDaftarDebitNote.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        'Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        'If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
        '    sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DebitNote), ViewState("currSortColumn"), ViewState("currSortDirection")))
        'Else
        '    sortColl = Nothing
        'End If
        'arlDebitNote = New FinishUnit.DebitNoteFacade(User).Retrieve(criterias, sortColl)
        'arlDebitNote = New FinishUnit.DebitNoteFacade(User).RetrieveList()
        'Dim DealerCode As String = String.Empty

        'For Each item As DebitNote In arlDebitNote
        '    If (Not IsExist(item.Dealer.DealerCode, arlDebitNoteFilter)) Then
        '        arlDebitNoteFilter.Add(item)
        '    End If
        'Next

        'If (arlDebitNoteFilter.Count > 0) Then
        If (arlDebitNote.Count > 0) Then
            dgDaftarDebitNote.Visible = True
            'dgDaftarDebitNote.DataSource = arlDebitNoteFilter
            dgDaftarDebitNote.DataSource = arlDebitNote
            dgDaftarDebitNote.VirtualItemCount = totalRow
            dgDaftarDebitNote.DataBind()
        Else
            dgDaftarDebitNote.Visible = False

            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Private Sub BindStatus()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Insert(0, New ListItem("Please Select", CInt(EnumDepositA.StatusPencairanDebitNote.All).ToString()))
        ddlStatus.Items.Insert(1, New ListItem("Belum Cair", CInt(EnumDepositA.StatusPencairanDebitNote.BelumCair).ToString()))
        ddlStatus.Items.Insert(2, New ListItem("Proses", CInt(EnumDepositA.StatusPencairanDebitNote.Proses).ToString()))

        ddlStatus.Items.Insert(3, New ListItem("Proses Cair", CInt(EnumDepositA.StatusPencairanDebitNote.ProsesCair).ToString()))
        ddlStatus.Items.Insert(4, New ListItem("Sudah Cair", CInt(EnumDepositA.StatusPencairanDebitNote.SudahCair).ToString()))

        'ddlStatus.Items.Insert(0, New ListItem("Please Select", "0"))
        'ddlStatus.Items.Insert(1, New ListItem("Belum Cair", "1"))
        'ddlStatus.Items.Insert(2, New ListItem("Proses", "2"))

        'ddlStatus.Items.Insert(3, New ListItem("Proses Cair", "3"))
        'ddlStatus.Items.Insert(4, New ListItem("Sudah Cair", "4"))

    End Sub

    Private TotalAmount As Double = 0
    Private Sub dgDaftarDebitNote_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDaftarDebitNote.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim objDebitNote As DebitNote = CType(e.Item.DataItem, DebitNote)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarDebitNote.CurrentPageIndex * dgDaftarDebitNote.PageSize)
            Dim lnkAccount As LinkButton = CType(e.Item.FindControl("lnkAccount"), LinkButton)
            lnkAccount.Attributes("OnClick") = "showPopUp('../PopUp/PopUpDealerBankAccount.aspx?DealerID=" & objDebitNote.Dealer.ID & "','',500,760,'');"


            TotalAmount = TotalAmount + Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Amount"))


            'CR Deposit A, By Ali



            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "DNNumber", MatchType.Exact, objDebitNote.DNNumber))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Type", MatchType.Exact, CInt(EnumDepositA.TipePengajuan.Offset)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Dealer.ID", MatchType.Exact, objDebitNote.Dealer.ID))

            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
            sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositAPencairanH), "NoReg", Sort.SortDirection.DESC))

            Dim arlDepositAPencarianH As ArrayList = New FinishUnit.DepositAPencairanHFacade(User).Retrieve(criterias, sortColl)

            Dim objDepositPencairanH As DepositAPencairanH
            If arlDepositAPencarianH.Count > 0 Then
                objDepositPencairanH = arlDepositAPencarianH(0)
            End If
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            If Not IsNothing(objDepositPencairanH) Then
                If Not IsNothing(objDepositPencairanH.Status) Then
                    If objDepositPencairanH.Status = EnumDepositA.StatusPencairanDealer.Selesai Then
                        Dim critKuitansi As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critKuitansi.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAKuitansiPencairan), "NoReg", MatchType.Exact, objDepositPencairanH.NoReg))
                        Dim arlKuitansi As ArrayList = New FinishUnit.DepositAKuitansiPencairanFacade(User).Retrieve(critKuitansi)
                        Dim objKuitansi As DepositAKuitansiPencairan
                        If arlKuitansi.Count > 0 Then
                            objKuitansi = arlKuitansi(0)
                            If objKuitansi.Status = EnumDepositA.StatusKuitansiKTB.Selesai Then
                                lblStatus.Text = "Sudah Cair" ' "Proses Cair"
                            ElseIf objKuitansi.Status = EnumDepositA.StatusKuitansiKTB.Cair Then
                                lblStatus.Text = "Sudah Cair"
                            Else
                                lblStatus.Text = "Proses"
                            End If
                        Else
                            lblStatus.Text = "Proses"
                        End If

                    ElseIf objDepositPencairanH.Status = EnumDepositA.StatusPencairanDealer.Blok Or objDepositPencairanH.Status = EnumDepositA.StatusPencairanDealer.Tolak Then
                        lblStatus.Text = "Belum Cair"
                    Else
                        lblStatus.Text = "Proses"
                    End If
                Else
                    lblStatus.Text = "Belum Cair"
                End If
            Else
                lblStatus.Text = "Belum Cair"
            End If

            'End off CR Deposit A
        ElseIf (e.Item.ItemType = ListItemType.Footer) Then
            e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Item.Cells(4).Text = "Total:"
            e.Item.Cells(7).Text = IIf(TotalAmount = 0, 0, TotalAmount.ToString("#,###"))
        End If
    End Sub

#End Region

    Private Sub Initialize()
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")
        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = 1 Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        Else
            'lblSearchDealer.Visible = False
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            'txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
            'txtKodeDealer.ReadOnly = True
            txtKodeDealer.Attributes.Add("readonly", "readonly")
        End If
    End Sub

#Region "Event Handlers"
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
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()

        'If Not SecurityProvider.Authorize(context.User, SR.DepositAView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - Debit Note")
        'End If        
        If Not SecurityProvider.Authorize(context.User, SR.DepositA_debit_note_lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - Daftar Pengajuan Deposit A")
            Me.btnSearch.Visible = False
        End If      
    End Sub
#End Region

    Private Sub dgDaftarDebitNote_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDaftarDebitNote.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgDaftarDebitNote.SelectedIndex = -1
        dgDaftarDebitNote.CurrentPageIndex = 0
        BindDataGrid(dgDaftarDebitNote.CurrentPageIndex)
    End Sub
End Class
