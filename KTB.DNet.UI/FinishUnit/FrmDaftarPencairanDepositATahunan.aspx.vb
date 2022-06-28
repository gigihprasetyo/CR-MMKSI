Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class FrmDaftarPencairanDepositA
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgDaftarDepositA As System.Web.UI.WebControls.DataGrid
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
    Private arlAnnualDepositAHeader As ArrayList = New ArrayList
    Private arlAnnualDepositAHeaderFilter As ArrayList = New ArrayList

    Dim sHelper As New SessionHelper
#End Region

    Private Sub Initialize()
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")

        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = 1 Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        Else
            lblSearchDealer.Visible = False
            txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
            'txtKodeDealer.ReadOnly = True
            txtKodeDealer.Attributes.Add("readonly", "readonly")
        End If
    End Sub

#Region "Event Handlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'InitiateAuthorization()
        If Not IsPostBack Then
            Initialize()
            ViewState("currentSortColumn") = "Dealer.DealerCode"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
            'BindDataGrid(0)
            BindStatus()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
    End Sub

    Private Sub dgDaftarDepositA_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDaftarDepositA.PageIndexChanged
        dgDaftarDepositA.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgDaftarDepositA.CurrentPageIndex)
    End Sub

    Private Sub dgDaftarDepositA_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDaftarDepositA.SortCommand
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

        dgDaftarDepositA.SelectedIndex = -1
        dgDaftarDepositA.CurrentPageIndex = 0
        BindDataGrid(dgDaftarDepositA.CurrentPageIndex)
    End Sub

#End Region


#Region "Custom Method"

    Private Sub InitiateAuthorization()

        'If Not SecurityProvider.Authorize(context.User, SR.DepositAView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - Debit Note")
        'End If        
        If Not SecurityProvider.Authorize(context.User, SR.DepositA_debit_note_lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - Daftar Pencairan Deposit A Tahunan")
            Me.btnSearch.Visible = False
        End If
    End Sub

    Private Sub AddPeriodCriteria(ByVal criterias As CriteriaComposite, ByVal ColumnName As String)
        Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, 1, 0, 0, 0)
        Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, Date.DaysInMonth(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month), 0, 0, 0)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "FromDate", MatchType.GreaterOrEqual, Format(dtStart, "yyyy-MM-dd HH:mm:ss")))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "ToDate", MatchType.LesserOrEqual, Format(dtEnd, "yyyy-MM-dd HH:mm:ss")))
    End Sub


    Private Function IsExist(ByVal DealerCode As String, ByVal arl As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As AnnualDepositAHeader In arl
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
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If

        'CR Deposit A, By Ali
        If CInt(ddlProductCategory.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        End If

        If CInt(ddlStatus.SelectedValue) > 0 Then
            Dim strSql As String = ""

            Dim dtStart As DateTime = New DateTime(icPeriodeFrom.Value.Year, icPeriodeFrom.Value.Month, 1, 0, 0, 0)
            Dim dtEnd As DateTime = New DateTime(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month, Date.DaysInMonth(icPeriodeTo.Value.Year, icPeriodeTo.Value.Month), 0, 0, 0)

            strSql = EnumDepositA.RetrieveAnnual(dtStart, dtEnd, CType(ddlStatus.SelectedValue, EnumDepositA.StatusPencairanAnnual))

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDepositAHeader), "ID", MatchType.InSet, "(" & strSql & ")"))

        End If

        'End of CR DEposit A

        AddPeriodCriteria(criterias, "PostingDate")
        Dim totalRow As Integer = 0
        arlAnnualDepositAHeader = New FinishUnit.AnnualDepositAHeaderFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgDaftarDepositA.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        If (arlAnnualDepositAHeader.Count > 0) Then
            dgDaftarDepositA.Visible = True
            dgDaftarDepositA.DataSource = arlAnnualDepositAHeader
            dgDaftarDepositA.VirtualItemCount = totalRow
            dgDaftarDepositA.DataBind()
        Else
            dgDaftarDepositA.Visible = False

            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Private TotalAmount As Double = 0

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
    Private Sub dgDaftarDepositA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDaftarDepositA.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim objAnnualDepositAHeader As AnnualDepositAHeader = CType(e.Item.DataItem, AnnualDepositAHeader)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarDepositA.CurrentPageIndex * dgDaftarDepositA.PageSize)
            Dim lblPeriode As Label = CType(e.Item.FindControl("lblPeriode"), Label)
            lblPeriode.Text = objAnnualDepositAHeader.FromDate.ToString("MMM yyyy") & " - " & objAnnualDepositAHeader.ToDate.ToString("MMM yyyy")
            Dim lblKeterangan As Label = CType(e.Item.FindControl("lblKeterangan"), Label)
            'If Not IsNothing(objAnnualDepositAHeader.Status) Then
            '    Dim lblKeterangan As Label = CType(e.Item.FindControl("lblKeterangan"), Label)
            '    Select Case objAnnualDepositAHeader.Status
            '        Case StatusPencairanTahunan.DiAjukan
            '            lblKeterangan.Text = "Belum dicairkan"
            '        Case StatusPencairanTahunan.DiCairkan
            '            lblKeterangan.Text = "Sudah dicairkan"
            '    End Select
            'End If


            'CR Deposit A, By Ali



            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "AnnualDepositAHeader.ID", MatchType.Exact, objAnnualDepositAHeader.ID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "Type", MatchType.Exact, CInt(EnumDepositA.TipePengajuan.CashAnnual)))

            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
            sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositAPencairanH), "NoReg", Sort.SortDirection.DESC))

            Dim arlDepositAPencarianH As ArrayList = New FinishUnit.DepositAPencairanHFacade(User).Retrieve(criterias, sortColl)

            Dim objDepositPencairanH As DepositAPencairanH
            If arlDepositAPencarianH.Count > 0 Then
                objDepositPencairanH = arlDepositAPencarianH(0)
            End If

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
                                lblKeterangan.Text = "Proses Cair"
                            ElseIf objKuitansi.Status = EnumDepositA.StatusKuitansiKTB.Cair Then
                                lblKeterangan.Text = "Sudah Cair"
                            Else
                                lblKeterangan.Text = "Proses"
                            End If
                        Else
                            lblKeterangan.Text = "Proses"
                        End If

                    ElseIf objDepositPencairanH.Status = EnumDepositA.StatusPencairanDealer.Blok Or objDepositPencairanH.Status = EnumDepositA.StatusPencairanDealer.Tolak Then
                        lblKeterangan.Text = "Belum Cair"
                    Else
                        lblKeterangan.Text = "Proses"
                    End If
                Else
                    lblKeterangan.Text = "Belum Cair"
                End If
            Else
                lblKeterangan.Text = "Belum Cair"
            End If

            'End off CR Deposit A

        ElseIf (e.Item.ItemType = ListItemType.Footer) Then
            'e.Item.Cells(3).HorizontalAlign = HorizontalAlign.Right
            'e.Item.Cells(3).Text = "Total:"
            'e.Item.Cells(6).Text = IIf(TotalAmount = 0, 0, TotalAmount.ToString("#,###"))
        End If
    End Sub

#End Region

#Region "Internal Enum"
    'Private Enum StatusPencairanTahunan
    '    DiAjukan = 0
    '    DiCairkan = 1
    'End Enum
#End Region

End Class
