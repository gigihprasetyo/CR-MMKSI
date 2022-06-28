Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports KTB.DNet.BusinessFacade.Helper
Imports OfficeOpenXml

Public Class FrmBabitEventReportList
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private deletePriv As Boolean
    Private addKuitansiPriv As Boolean
    Private oDealer As Dealer
    Private SessionGridDataLaporanEvent = "FrmBabitEventReportList.BabitEventReportHeaderList"
    Private SessionCriteriaLaporanEvent = "FrmBabitEventReportList.CriteriaFrmBabitEventReportList"
    Private Const strTypeCode As String = "L"
    Private Const strCategory As String = "EnumBabit.BabitParameterCategory"
    Private Const strValueCodeActivity As String = "Aktivitas"

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Authorization()
        If Not IsPostBack Then
            ViewState("currSortColumn") = "EventReportName"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            txtKodeDealer.Text = SesDealer().DealerCode
            hdnDealer.Value = SesDealer().DealerCode
            lblKodeDealer.Text = SesDealer().DealerCode & " / " & SesDealer().DealerName

            PageInit()
            BindDdlLs()
            BindDDLDealerCategory()
            BindDDLStatusReport()

            '-- Restore selection criteria
            ReadCriteria()

            If IsLoginAsDealer() Then
                txtKodeDealer.Attributes("style") = "display:none"
                lblPopUpDealer.Attributes("style") = "display:none"
                lblKodeDealer.Attributes("style") = "display:table-row"

                trCategory.Attributes("style") = "display:none"
                btnTransfer.Visible = False
                btnTransferUlang.Visible = False
            Else
                lblPopUpDealer.Attributes("style") = "display:table-row"
                txtKodeDealer.Attributes("style") = "display:table-row"
                lblKodeDealer.Attributes("style") = "display:none"

                trCategory.Attributes("style") = "display:table-row"
                txtKodeDealer.Text = ""
            End If
            ReadData()   '-- Read all data matching criteria
            BindGrid(dgEventReportList.CurrentPageIndex)  '-- Bind page-1
        End If

        lblPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
        lblPopUpTO.Attributes("onclick") = "ShowPopUpTO()"
    End Sub

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sesHelper.GetSession(SessionCriteriaLaporanEvent), Hashtable)
        If Not crit Is Nothing Then
            ddlCategory.SelectedValue = CStr(crit.Item("ddlCategory"))
            txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
            lblKodeDealer.Text = CStr(crit.Item("DealerCode"))
            txtKodeTempOut.Text = CStr(crit.Item("DealerBranchCode"))
            cbDate.Checked = CBool(crit.Item("cbDate"))
            icPeriodeStart.Value = CStr(crit.Item("PeriodStart"))
            icPeriodeEnd.Value = CStr(crit.Item("PeriodEnd"))
            txtEventReportName.Text = CStr(crit.Item("EventReportName"))
            txtEventRegNumberProposal.Text = CStr(crit.Item("EventRegNumberProposal"))
            lsStatus.SelectedValue = CStr(crit.Item("lsStatus"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgEventReportList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("ddlCategory", ddlCategory.SelectedValue)
        crit.Add("DealerCode", txtKodeDealer.Text)
        crit.Add("DealerBranchCode", txtKodeTempOut.Text)
        crit.Add("cbDate", cbDate.Checked)
        crit.Add("PeriodStart", icPeriodeStart.Value)
        crit.Add("PeriodEnd", icPeriodeEnd.Value)
        crit.Add("EventReportName", txtEventReportName.Text)
        crit.Add("EventRegNumberProposal", txtEventRegNumberProposal.Text)
        crit.Add("lsStatus", lsStatus.SelectedValue)

        crit.Add("PageIndex", dgEventReportList.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sesHelper.SetSession(SessionCriteriaLaporanEvent, crit)  '-- Store in session
    End Sub

    Private Sub BindDDLDealerCategory()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, "MMC"))
        arrDDL = New CategoryFacade(User).Retrieve(criterias)

        With ddlCategory
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "CategoryCode"
            .DataValueField = "ID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End With
    End Sub

    Private Sub BindDDLStatusReport()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumBabit.BabitEventReportStatus"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))

        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        Dim arrDDL2 As New ArrayList
        For Each obj As StandardCode In arrDDL
            If IsLoginAsDealer() Then
                Select Case obj.ValueCode
                    Case "Batal", "Validasi", "BatalValidasi"
                        arrDDL2.Add(obj)
                End Select
            Else
                Select Case obj.ValueCode
                    Case "Konfirmasi", "Revisi"
                        arrDDL2.Add(obj)
                End Select
            End If
        Next

        With ddlStatus
            .Items.Clear()
            .DataSource = arrDDL2
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Piih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub BindDdlLs()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumBabit.BabitEventReportStatus"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))

        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        Dim arrDDL2 As New ArrayList
        For Each obj As StandardCode In arrDDL
            If obj.ValueCode <> "BatalValidasi" Then
                arrDDL2.Add(obj)
            End If
        Next

        With lsStatus
            .Items.Clear()
            .DataSource = arrDDL2
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
        End With
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Laporan_Display_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - DAFTAR LAPORAN EVENT")
        Else
            displayPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Laporan_Display_Privilege)
            editPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Laporan_Edit_Privilege)
            deletePriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Laporan_Delete_Privilege)
            addKuitansiPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_Laporan_Buat_Kuitansi_Privilege)
        End If
    End Sub

    Private Sub PageInit()
        icPeriodeStart.Value = Date.Now.AddMonths(-1)
        icPeriodeEnd.Value = Date.Now
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrBabitEventReportHeaderList As ArrayList = CType(sesHelper.GetSession(SessionGridDataLaporanEvent), ArrayList)
        If arrBabitEventReportHeaderList.Count > 0 Then
            CommonFunction.SortListControl(arrBabitEventReportHeaderList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrBabitEventReportHeaderList, pageIndex, dgEventReportList.PageSize)
            dgEventReportList.DataSource = PagedList
            dgEventReportList.VirtualItemCount = arrBabitEventReportHeaderList.Count()
            dgEventReportList.DataBind()

            lblChangeStatus.Visible = True
            ddlStatus.Visible = True
            btnProses.Visible = True
        Else
            dgEventReportList.DataSource = New ArrayList
            dgEventReportList.VirtualItemCount = 0
            dgEventReportList.CurrentPageIndex = 0
            dgEventReportList.DataBind()

            lblChangeStatus.Visible = False
            ddlStatus.Visible = False
            btnProses.Visible = False
        End If
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If cbDate.Checked Then
            Dim tglFrom As New Date(icPeriodeStart.Value.Year, icPeriodeStart.Value.Month, icPeriodeStart.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icPeriodeEnd.Value.Year, icPeriodeEnd.Value.Month, icPeriodeEnd.Value.Day, 23, 59, 59)
            crit.opAnd(New Criteria(GetType(BabitEventReportHeader), "PeriodStart", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "((", True)
            crit.opAnd(New Criteria(GetType(BabitEventReportHeader), "PeriodStart", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), ")", False)

            crit.opOr(New Criteria(GetType(BabitEventReportHeader), "PeriodEnd", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "(", True)
            crit.opAnd(New Criteria(GetType(BabitEventReportHeader), "PeriodEnd", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), "))", False)
        End If

        If txtKodeDealer.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventReportHeader), "Dealer.DealerCode", MatchType.Partial, txtKodeDealer.Text))
        End If
        If txtKodeTempOut.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventReportHeader), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtKodeTempOut.Text.Replace(";", "','") & "')"))
        End If
        If txtEventReportName.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventReportHeader), "EventReportName", MatchType.Partial, txtEventReportName.Text))
        End If
        If txtEventRegNumberProposal.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventReportHeader), "BabitEventProposalHeader.EventRegNumber", MatchType.Exact, txtEventRegNumberProposal.Text))
        End If
        If lsStatus.SelectedValue <> "" Then
            crit.opAnd(New Criteria(GetType(BabitEventReportHeader), "Status", MatchType.Exact, lsStatus.SelectedValue))
        End If
        If ddlCategory.SelectedValue <> "-1" Then
            Dim strSQL As String = "SELECT DealerID FROM DealerCategory WHERE CategoryID IN (SELECT ID FROM Category WHERE ID = " & ddlCategory.SelectedValue & ")"
            crit.opAnd(New Criteria(GetType(BabitEventReportHeader), "Dealer.ID", MatchType.InSet, "(" & strSQL & ")"))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BabitEventReportHeader), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrBabitEventReportHeaderList As ArrayList = New BabitEventReportHeaderFacade(User).RetrieveByCriteria(crit, sortColl)
        sesHelper.SetSession(SessionGridDataLaporanEvent, arrBabitEventReportHeaderList)
        If arrBabitEventReportHeaderList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ReadData()
        dgEventReportList.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dgEventReportList.CurrentPageIndex)  '-- Bind page-1
        StoreCriteria()
    End Sub

    Private Sub dgEventReportList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgEventReportList.PageIndexChanged
        '-- Change datagrid page

        ReadData()
        dgEventReportList.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgEventReportList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgEventReportList.SortCommand
        '-- Sort datagrid rows based on a column header clicked

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

        '-- Bind page-1
        dgEventReportList.CurrentPageIndex = 0
        ReadData()
        BindGrid(dgEventReportList.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Protected Sub dgEventReportList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEventReportList.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                Response.Redirect("FrmInputBabitEventReport.aspx?Mode=Detail&BabitEventReportHeaderID=" & e.Item.Cells(0).Text)
            Case "Edit"
                Response.Redirect("FrmInputBabitEventReport.aspx?Mode=Edit&BabitEventReportHeaderID=" & e.Item.Cells(0).Text)
            Case "Delete"
                DeleteBabitEventReport(CInt(e.CommandArgument))
            Case "Download"
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)
            Case "ViewKuitansi"
                Dim strMode As String = "New"
                Dim intBabitEventReportReceiptID As Integer = 0
                Dim intBabitEventReportHeaderID As Integer = CInt(e.Item.Cells(0).Text)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitEventReportReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitEventReportReceipt), "BabitEventReportHeader.ID", MatchType.Exact, intBabitEventReportHeaderID))
                Dim arlBabitEventReportReceipt As ArrayList = New BabitEventReportReceiptFacade(User).Retrieve(criterias)
                If Not IsNothing(arlBabitEventReportReceipt) AndAlso arlBabitEventReportReceipt.Count > 0 Then
                    Dim objBabitEventReportReceipt As BabitEventReportReceipt = CType(arlBabitEventReportReceipt(0), BabitEventReportReceipt)
                    intBabitEventReportReceiptID = objBabitEventReportReceipt.ID
                    strMode = "Edit"
                End If

                Response.Redirect("FrmInputBabitEventReportReceipt.aspx?Mode=" & strMode & "&BabitEventReportHeaderID=" & intBabitEventReportHeaderID & "&BabitEventReportReceiptID=" & intBabitEventReportReceiptID)
        End Select
    End Sub

    Private Sub DeleteBabitEventReport(ByVal intBabitEventReportHdrID As Integer)
        Dim strSql As String = String.Empty
        Dim objBabitEventReportHeaderFacade As BabitEventReportHeaderFacade = New BabitEventReportHeaderFacade(User)
        Dim objBabitEventReportDetailFacade As BabitEventReportDetailFacade = New BabitEventReportDetailFacade(User)
        Dim objBabitEventReportDocumentFacade As BabitEventReportDocumentFacade = New BabitEventReportDocumentFacade(User)
        Dim objBabitEventReportActivityFacade As BabitEventReportActivityFacade = New BabitEventReportActivityFacade(User)

        Dim objBabitEventReportHeader As BabitEventReportHeader = objBabitEventReportHeaderFacade.Retrieve(intBabitEventReportHdrID)
        objBabitEventReportHeader.RowStatus = CType(DBRowStatus.Deleted, Short)

        Dim arrBabitEventReportDetail As ArrayList = New ArrayList
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitEventReportHeader.ID", MatchType.Exact, intBabitEventReportHdrID))
        'arrBabitEventReportDetail = objBabitEventReportDetailFacade.Retrieve(criterias)
        'For Each obj As BabitEventReportDetail In arrBabitEventReportDetail
        '    obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        'Next

        strSql = "select ID from BabitParameterDetail "
        strSql += "where BabitParameterHeaderID in ( "
        strSql += "select ID from BabitParameterHeader "
        strSql += "where BabitMasterEventTypeID in ( "
        strSql += "select ID from BabitMasterEventType where TypeCode = '" & strTypeCode & "') "
        strSql += "and ParameterCategory in ( "
        strSql += "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode <> '' and ValueCode <> '" & strValueCodeActivity & "') "
        strSql += ") "

        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitEventReportHeader.ID", MatchType.Exact, intBabitEventReportHdrID))
        criterias.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitParameterDetail.ID", MatchType.InSet, "(" & strSql & ")"))
        criterias.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitParameterDetail.BabitParameterHeader.Status", MatchType.Exact, 1))
        arrBabitEventReportDetail = New BabitEventReportDetailFacade(User).Retrieve(criterias)
        For Each obj As BabitEventReportDetail In arrBabitEventReportDetail
            obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        Next


        Dim arrBabitEventReportDoc As ArrayList = New ArrayList
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitEventReportDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(BabitEventReportDocument), "BabitEventReportHeader.ID", MatchType.Exact, intBabitEventReportHdrID))
        arrBabitEventReportDoc = objBabitEventReportDocumentFacade.Retrieve(criterias2)
        For Each obj As BabitEventReportDocument In arrBabitEventReportDoc
            obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        Next

        Dim arrBabitEventReportAct As ArrayList = New ArrayList
        'Dim criterias3 As New CriteriaComposite(New Criteria(GetType(BabitEventReportActivity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias3.opAnd(New Criteria(GetType(BabitEventReportActivity), "BabitEventReportHeader.ID", MatchType.Exact, intBabitEventReportHdrID))
        'arrBabitEventReportAct = objBabitEventReportActivityFacade.Retrieve(criterias3)
        'For Each obj As BabitEventReportActivity In arrBabitEventReportAct
        '    obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        'Next

        strSql = "select ID from BabitParameterDetail "
        strSql += "where BabitParameterHeaderID in ( "
        strSql += "select ID from BabitParameterHeader "
        strSql += "where BabitMasterEventTypeID in ( "
        strSql += "select ID from BabitMasterEventType where TypeCode = '" & strTypeCode & "') "
        strSql += "and ParameterCategory in ( "
        strSql += "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode= '" & strValueCodeActivity & "') "
        strSql += ") "

        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(BabitEventReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias3.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitEventReportHeader.ID", MatchType.Exact, intBabitEventReportHdrID))
        criterias3.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitParameterDetail.ID", MatchType.InSet, "(" & strSql & ")"))
        criterias3.opAnd(New Criteria(GetType(BabitEventReportDetail), "BabitParameterDetail.BabitParameterHeader.Status", MatchType.Exact, 1))
        arrBabitEventReportAct = New BabitEventReportDetailFacade(User).Retrieve(criterias3)
        For Each obj As BabitEventReportDetail In arrBabitEventReportAct
            obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        Next


        Dim _result As Integer = 0
        _result = New BabitEventReportDetailFacade(User).UpdateTransaction(objBabitEventReportHeader, arrBabitEventReportDetail, New ArrayList, arrBabitEventReportDoc, New ArrayList, arrBabitEventReportAct, New ArrayList)
        If _result > 0 Then
            MessageBox.Show("Delete Data Sukses")
        End If
        ReadData()
        BindGrid(dgEventReportList.CurrentPageIndex)
    End Sub

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click
        Dim sb As StringBuilder = New StringBuilder

        If ddlStatus.SelectedIndex = 0 Then
            MessageBox.Show("Pilih daftar status terlebih dahulu")
            Exit Sub
        End If
        Dim objBabitEventReportHeader As New BabitEventReportHeader
        Dim objBabitEventReportHeaderFacade As BabitEventReportHeaderFacade = New BabitEventReportHeaderFacade(User)
        Dim intBabitEventReportHdrID As Integer = 0

        Dim nResult As Integer
        Dim checkCounter As Integer = 0

        ' Status Report :
        '0: Baru
        '1: Batal
        '2: Validasi
        '3: Konfirmasi
        '4: Revisi
        '5: Proses
        '6: Setuju
        '7: Tidak Setuju
        '8: ProsesGroupware

        If dgEventReportList.Items.Count > 0 Then
            For Each item As DataGridItem In dgEventReportList.Items
                Dim chckbox As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                Dim lbtnView As LinkButton = CType(item.FindControl("lbtnView"), LinkButton)

                Dim StrVal As String = String.Empty
                intBabitEventReportHdrID = Convert.ToInt32(item.Cells(0).Text)
                objBabitEventReportHeader = objBabitEventReportHeaderFacade.Retrieve(intBabitEventReportHdrID)
                If chckbox.Checked Then
                    Dim strStatusName As String = CType(New StandardCodeFacade(User).RetrieveByValueId(objBabitEventReportHeader.Status.ToString(), "EnumBabit.BabitEventReportStatus")(0), StandardCode).ValueCode

                    If objBabitEventReportHeader.Status <> 0 And CByte(ddlStatus.SelectedValue) = 1 Then   'status Batal
                        StrVal = "Ubah status menjadi Batal hanya dapat dilakukan untuk data statusnya Baru"
                        sb.Append("Item " & item.ItemIndex + 1 & " : Status " & strStatusName & " tidak bisa diubah ke status Batal\n" & StrVal & "\n")
                    End If
                    If objBabitEventReportHeader.Status <> 0 And objBabitEventReportHeader.Status <> 5 And CByte(ddlStatus.SelectedValue) = 2 Then   'status Validasi
                        StrVal = "Ubah status menjadi Validasi hanya dapat dilakukan untuk data statusnya Baru"
                        sb.Append("Item " & item.ItemIndex + 1 & " : Status " & strStatusName & " tidak bisa diubah ke status Validasi\n" & StrVal & "\n")
                    End If
                    If objBabitEventReportHeader.Status <> 2 And CByte(ddlStatus.SelectedValue) = 3 Then   'status Batal Validasi
                        StrVal = "Ubah status Batal Validasi hanya dapat dilakukan untuk data statusnya Validasi"
                        sb.Append("Item " & item.ItemIndex + 1 & " : Status " & strStatusName & " tidak bisa Batal Validasi\n" & StrVal & "\n")
                    End If

                    If objBabitEventReportHeader.Status <> 2 And CByte(ddlStatus.SelectedValue) = 4 Then   'status Konfirmasi
                        StrVal = "Ubah status menjadi Konfirmasi hanya dapat dilakukan untuk data statusnya Validasi"
                        sb.Append("Item " & item.ItemIndex + 1 & " : Status " & strStatusName & " tidak bisa diubah ke status Konfirmasi\n" & StrVal & "\n")
                    End If
                    If objBabitEventReportHeader.Status <> 4 And CByte(ddlStatus.SelectedValue) = 5 Then   'status Revisi
                        StrVal = "Ubah status menjadi Revisi hanya dapat dilakukan untuk data statusnya Konfirmasi"
                        sb.Append("Item " & item.ItemIndex + 1 & " : Status " & strStatusName & " tidak bisa diubah ke status Revisi\n" & StrVal & "\n")
                    End If
                End If
            Next
            If (sb.ToString().Length > 0) Then
                MessageBox.Show(sb.ToString())
                Exit Sub
            End If

            For Each item As DataGridItem In dgEventReportList.Items
                Dim chckbox As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                intBabitEventReportHdrID = Convert.ToInt32(item.Cells(0).Text)
                objBabitEventReportHeader = objBabitEventReportHeaderFacade.Retrieve(intBabitEventReportHdrID)
                If chckbox.Checked Then
                    If ddlStatus.SelectedValue = 3 Then   'Jika ststus nya Batal Validasi
                        objBabitEventReportHeader.Status = 0
                    Else
                        objBabitEventReportHeader.Status = ddlStatus.SelectedValue
                    End If
                    nResult = objBabitEventReportHeaderFacade.Update(objBabitEventReportHeader)

                    If nResult = -1 Then
                        sb.Append("Laporan : " & objBabitEventReportHeader.EventReportName + 1 & " tidak bisa di update status\n")
                    End If
                    checkCounter = checkCounter + 1
                End If
            Next
        End If

        If (checkCounter > 0) Then
            If (sb.ToString().Length > 0) Then
                MessageBox.Show(sb.ToString())
            End If
            MessageBox.Show("Proses update status sukses")
        Else
            MessageBox.Show("Silahkan pilih item laporan")
        End If

        ddlStatus.ClearSelection()
        ReadData()
        BindGrid(0)
    End Sub

    Protected Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click, btnTransferUlang.Click
        Dim sb As StringBuilder = New StringBuilder
        Dim StrVal As String = String.Empty
        Dim checkCounter As Integer = 0
        Dim arrUpdate As ArrayList = New ArrayList

        For Each dgItem As DataGridItem In dgEventReportList.Items
            If CType(dgItem.FindControl("chkItemChecked"), CheckBox).Checked Then
                Dim objReport As BabitEventReportHeader = New BabitEventReportHeaderFacade(User).Retrieve(CType(dgItem.Cells(0).Text, Integer))
                If objReport.ConfirmedBudget <> 0 Then
                    If Not IsNothing(objReport) AndAlso objReport.ID > 0 Then
                        Dim strStatusName As String = CType(New StandardCodeFacade(User).RetrieveByValueId(objReport.Status.ToString(), "EnumBabit.BabitEventReportStatus")(0), StandardCode).ValueCode

                        If objReport.Status = 4 Then  'Selama Status masih Konfirmasi
                            'objReport.isTransfer = 1
                            objReport.Status = 6    'update ke status Proses
                            'Dim _return As Integer = New BabitEventReportHeaderFacade(User).Update(objReport)
                            arrUpdate.Add(objReport)
                        Else
                            sb.Append("Item " & dgItem.ItemIndex + 1 & " : Status " & strStatusName & " tidak bisa diubah ke status Proses\n")
                        End If
                    End If
                    checkCounter += 1
                Else
                    MessageBox.Show("Jumlah Subsidi Event " & objReport.BabitEventProposalHeader.EventRegNumber & " masih kosong")
                    Exit Sub
                End If
            End If
        Next

        If (checkCounter = 0) Then
            MessageBox.Show("Silahkan pilih item laporan")
            Exit Sub
        Else
            Dim _return As Integer = 0
            If arrUpdate.Count > 0 Then
                _return = New BabitEventReportHeaderFacade(User).UpdateTransaction(arrUpdate)
            End If

            If _return > 0 Then
                MessageBox.Show("Transfer Sukses")
            Else
                MessageBox.Show("Transfer Gagal")
            End If
        End If

        If (sb.ToString().Length > 0) Then
            StrVal = "Ubah status menjadi Revisi hanya dapat dilakukan untuk data statusnya Konfirmasi"
            MessageBox.Show(StrVal & "\n" & sb.ToString())
        End If
    End Sub

    Protected Sub dgEventReportList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgEventReportList.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
        Dim lblTempOut As Label = CType(e.Item.FindControl("lblTempOut"), Label)
        Dim lblEventProposalName As Label = CType(e.Item.FindControl("lblEventProposalName"), Label)
        Dim lblEventReportName As Label = CType(e.Item.FindControl("lblEventReportName"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Dim lblPrice As Label = CType(e.Item.FindControl("lblPrice"), Label)
        Dim lblSubsidyTarget As Label = CType(e.Item.FindControl("lblSubsidyTarget"), Label)
        Dim lblSubsidyAkhir As Label = CType(e.Item.FindControl("lblSubsidyAkhir"), Label)
        Dim lblEventRegNumber As Label = CType(e.Item.FindControl("lblEventRegNumber"), Label)
        Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)

        Dim lblPeriodStart As Label = CType(e.Item.FindControl("lblPeriodStart"), Label)
        Dim lblPeriodEnd As Label = CType(e.Item.FindControl("lblPeriodEnd"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As BabitEventReportHeader = CType(e.Item.DataItem, BabitEventReportHeader)

            lblNo.Text = (e.Item.ItemIndex + 1 + (dgEventReportList.PageSize * dgEventReportList.CurrentPageIndex)).ToString

            If IsNothing(oData.Dealer) Then
                'lblDealer.Text = oData.ID
            Else
                lblDealer.Text = oData.Dealer.DealerCode
            End If
            If IsNothing(oData.DealerBranch) Then
                'lblTempOut.Text = oData.ID
            Else
                lblTempOut.Text = oData.DealerBranch.DealerBranchCode
            End If

            lblEventRegNumber.Text = oData.BabitEventProposalHeader.EventRegNumber
            lblEventProposalName.Text = oData.BabitEventProposalHeader.EventProposalName
            lblEventReportName.Text = oData.EventReportName
            lblPeriodStart.Text = oData.PeriodStart
            lblPeriodEnd.Text = oData.PeriodEnd

            Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lnkbtnDetail"), LinkButton)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
            Dim lnkbtnDownload As LinkButton = CType(e.Item.FindControl("lnkbtnDownload"), LinkButton)
            Dim lnkbtnKuitansi As LinkButton = CType(e.Item.FindControl("lnkbtnKuitansi"), LinkButton)

            lnkbtnDetail.Visible = displayPriv
            lnkbtnEdit.Visible = False
            lnkbtnDelete.Visible = False
            lnkbtnKuitansi.Visible = False

            'Select Case oData.Status
            '    Case 0
            '        lblStatus.Text = "Baru"
            '    Case 1
            '        lblStatus.Text = "Batal"
            '    Case 2
            '        lblStatus.Text = "Validasi"
            '    Case 4
            '        lblStatus.Text = "Konfirmasi"
            '    Case 5
            '        lblStatus.Text = "Revisi"
            '    Case 6
            '        lblStatus.Text = "Proses"
            '    Case 7
            '        lblStatus.Text = "Proses Groupware"
            '    Case 8
            '        lblStatus.Text = "Setuju"
            '    Case 9
            '        lblStatus.Text = "Tidak Setuju"
            'End Select
            lblStatus.Text = CommonFunction.GetEnumDescription(oData.Status, "EnumBabit.BabitEventReportStatus")

            If IsLoginAsDealer() Then
                Select Case oData.Status
                    Case 0, 2, 5        '--Baru, Validasi, Revisi
                        chkItemChecked.Visible = True
                        lnkbtnEdit.Visible = editPriv
                        lnkbtnDelete.Visible = deletePriv
                    Case Else
                        chkItemChecked.Visible = False
                        lnkbtnEdit.Visible = False
                        lnkbtnDelete.Visible = False
                End Select

                If oData.Status = 8 Then    '--Setuju
                    lnkbtnKuitansi.Visible = addKuitansiPriv
                End If
            Else
                Select Case oData.Status
                    Case 2, 4       '---Validasi, Konfirmasi
                        chkItemChecked.Visible = True
                        lnkbtnEdit.Visible = editPriv
                        lnkbtnDelete.Visible = deletePriv
                    Case Else
                        chkItemChecked.Visible = False
                        lnkbtnEdit.Visible = False
                        lnkbtnDelete.Visible = False
                End Select
            End If

            Dim dsBabitEventReportHeader As DataSet = New BabitEventReportHeaderFacade(User).DoRetrieveDataset(oData.ID)
            Try
                Dim SubsidyTarget As Double = oData.BabitEventProposalHeader.EventDealerHeader.SubsidyTarget
                Dim SumPrice As Double = dsBabitEventReportHeader.Tables(0).Rows(0)("SumPrice")
                lblPrice.Text = SumPrice.ToString("#,##0")

                Dim maxSubsidy As Decimal = 0
                If oData.BabitEventProposalHeader.EventDealerHeader.MaxSubsidy > 0 Then
                    maxSubsidy = oData.BabitEventProposalHeader.EventDealerHeader.MaxSubsidy / 100
                End If
                Dim A As Double = 0
                Dim jmlSubsidy As Double = 0
                A = SumPrice * maxSubsidy

                If A < SubsidyTarget Then
                    jmlSubsidy = A
                ElseIf A > SubsidyTarget Then
                    jmlSubsidy = SubsidyTarget
                Else
                    jmlSubsidy = A
                End If
                'lblSubsidyTarget.Text = jmlSubsidy.ToString("#,##0")
                lblSubsidyTarget.Text = IIf(oData.ConfirmedBudget <= 0, jmlSubsidy.ToString("#,##0"), oData.ConfirmedBudget.ToString("#,##0"))

                'Dim dblSubsidyAkhir As Double = jmlSubsidy - SumPrice
                'lblSubsidyAkhir.Text = IIf(dblSubsidyAkhir <= 0, 0, dblSubsidyAkhir.ToString("#,##0"))
                lblSubsidyAkhir.Text = IIf(oData.ApprovedBudget <= 0, 0, oData.ApprovedBudget.ToString("#,##0"))
            Catch
            End Try

            Dim critsAppDoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventReportApprovalDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critsAppDoc.opAnd(New Criteria(GetType(BabitEventReportApprovalDoc), "BabitEventReportHeader.ID", MatchType.Exact, oData.ID))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(BabitEventReportApprovalDoc), "ID", Sort.SortDirection.DESC))
            Dim _result As ArrayList = New BabitEventReportApprovalDocFacade(User).Retrieve(critsAppDoc, sortColl)
            If _result.Count > 0 Then
                lnkbtnDownload.Visible = True
                lnkbtnDownload.CommandArgument = CType(_result(0), BabitEventReportApprovalDoc).Path
            Else
                lnkbtnDownload.Visible = False
            End If
        End If
    End Sub

    Protected Sub hdnDealer_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealer.ValueChanged
        Dim data As String() = hdnDealer.Value.Trim.Split(";")
        txtKodeDealer.Text = data(0)
    End Sub

    Protected Sub hdnTempOut_ValueChanged(sender As Object, e As EventArgs) Handles hdnTempOut.ValueChanged
        Dim data As String() = hdnTempOut.Value.Trim.Split(";")
        txtKodeTempOut.Text = data(0)
    End Sub


    Protected Sub btnDownloadExcel_Click(sender As Object, e As EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        If dgEventReportList.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        ' mengambil data yang dibutuhkan
        arrData = CType(sesHelper.GetSession(SessionGridDataLaporanEvent), ArrayList)
        If arrData.Count > 0 Then
            CreateExcel("DAFTAR LAPORAN EVENT", arrData)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Status"
            ws.Cells("C3").Value = "Kode Dealer"
            ws.Cells("D3").Value = "Temporary Outlet"
            ws.Cells("E3").Value = "Nama Proposal Event"
            ws.Cells("F3").Value = "Nama Laporan Event"
            ws.Cells("G3").Value = "Tgl Periode Mulai"
            ws.Cells("H3").Value = "Tgl Periode Selesai"
            ws.Cells("I3").Value = "Jumlah Subsidi"
            ws.Cells("J3").Value = "Biaya Diajukan"
            ws.Cells("K3").Value = "Subsidi Akhir"

            For i As Integer = 0 To Data.Count - 1
                Dim item As BabitEventReportHeader = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                ws.Cells(i + 4, 2).Value = IIf(item.Status = 0, "Baru", IIf(item.Status = 1, "Batal", "Konfirmasi"))
                If IsNothing(item.Dealer) Then
                    ws.Cells(i + 4, 3).Value = ""
                Else
                    ws.Cells(i + 4, 3).Value = item.Dealer.DealerCode
                End If
                If IsNothing(item.DealerBranch) Then
                    ws.Cells(i + 4, 4).Value = ""
                Else
                    ws.Cells(i + 4, 4).Value = item.DealerBranch.DealerBranchCode
                End If
                ws.Cells(i + 4, 5).Value = item.BabitEventProposalHeader.EventProposalName
                ws.Cells(i + 4, 6).Value = item.EventReportName
                ws.Cells(i + 4, 7).Value = Format(item.PeriodStart, "dd/MM/yyyy")
                ws.Cells(i + 4, 8).Value = Format(item.PeriodEnd, "dd/MM/yyyy")
                ws.Cells(i + 4, 9).Value = item.BabitEventProposalHeader.EventDealerHeader.SubsidyTarget.ToString("#,##0")

                Dim dblBiayadiAjukan As Double = 0
                Dim dblSubsidyAkhir As Double = 0
                Dim dsBabitEventReportHeader As DataSet = New BabitEventReportHeaderFacade(User).DoRetrieveDataset(item.ID)
                Try
                    dblBiayadiAjukan = dsBabitEventReportHeader.Tables(0).Rows(0)("SumPrice")
                    dblSubsidyAkhir = item.BabitEventProposalHeader.EventDealerHeader.SubsidyTarget - dblBiayadiAjukan
                    dblSubsidyAkhir = IIf(dblSubsidyAkhir <= 0, 0, dblSubsidyAkhir)
                Catch
                End Try
                ws.Cells(i + 4, 10).Value = dblBiayadiAjukan.ToString("#,##0")
                ws.Cells(i + 4, 11).Value = dblSubsidyAkhir.ToString("#,##0")
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using

    End Sub

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        'Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()
    End Sub

End Class