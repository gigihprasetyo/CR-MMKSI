Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security

Imports System.IO
Imports System.Textddlstatu
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Linq
Imports System.Data.Linq.SqlClient
Imports OfficeOpenXml
Imports System.Text

Public Class FrmBabitReportEventList
    Inherits System.Web.UI.Page

    Dim objLoginUser As UserInfo
    Private sesHelper As New SessionHelper
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private deletePriv As Boolean
    Private addKuitansiPriv As Boolean
    Private oDealer As Dealer
    Private SessionGridDataBabitReport = "FrmBabitReportEventList.GridData"
    Private SessionCriteriaBabitReport = "FrmBabitReportEventList.CriteriaList"

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        objLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Authorization()
        If Not IsPostBack Then
            ViewState("currSortColumn") = "BabitHeader.BabitRegNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            txtKodeDealer.Text = SesDealer().DealerCode
            hdnDealer.Value = SesDealer().DealerCode
            lblKodeDealer.Text = SesDealer().DealerCode & " / " & SesDealer().DealerName

            PageInit()
            BindDdlLs()
            BindDDLStatusReport()
            BindDDLAllocationBabit()

            '-- Restore selection criteria
            ReadCriteria()

            If IsLoginAsDealer() Then
                txtKodeDealer.Attributes("style") = "display:none"
                lblPopUpDealer.Attributes("style") = "display:none"
                lblKodeDealer.Attributes("style") = "display:table-row"
            Else
                lblPopUpDealer.Attributes("style") = "display:table-row"
                txtKodeDealer.Attributes("style") = "display:table-row"
                lblKodeDealer.Attributes("style") = "display:none"
                txtKodeDealer.Text = ""
            End If
            ReadData()   '-- Read all data matching criteria
            BindGrid(dgBabitReportList.CurrentPageIndex)  '-- Bind page-1
        End If

        lblPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
        lblPopUpTO.Attributes("onclick") = "ShowPopUpTO()"
    End Sub

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sesHelper.GetSession(SessionCriteriaBabitReport), Hashtable)
        If Not crit Is Nothing Then
            txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
            lblKodeDealer.Text = CStr(crit.Item("DealerCode"))
            txtKodeTempOut.Text = CStr(crit.Item("DealerBranchCode"))
            cbDate.Checked = CBool(crit.Item("cbDate"))
            icPeriodeStart.Value = CStr(crit.Item("PeriodStart"))
            icPeriodeEnd.Value = CStr(crit.Item("PeriodEnd"))

            ddlBabitType.SelectedValue = CStr(crit.Item("BabitType"))
            ddlCategoryAlloc.SelectedValue = CStr(crit.Item("CategoryAlloc"))
            txtBabitRegNo.Text = CStr(crit.Item("BabitRegNo"))
            txtNomorSurat.Text = CStr(crit.Item("NomorSurat"))
            lsStatus.SelectedValue = CStr(crit.Item("lsStatus"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgBabitReportList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("DealerCode", txtKodeDealer.Text)
        crit.Add("DealerBranchCode", txtKodeTempOut.Text)
        crit.Add("cbDate", cbDate.Checked)
        crit.Add("PeriodStart", icPeriodeStart.Value)
        crit.Add("PeriodEnd", icPeriodeEnd.Value)

        crit.Add("BabitType", ddlBabitType.SelectedValue)
        crit.Add("CategoryAlloc", ddlCategoryAlloc.SelectedValue)
        crit.Add("BabitRegNo", txtBabitRegNo.Text)
        crit.Add("NomorSurat", txtNomorSurat.Text)
        crit.Add("lsStatus", lsStatus.SelectedValue)

        crit.Add("PageIndex", dgBabitReportList.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sesHelper.SetSession(SessionCriteriaBabitReport, crit)  '-- Store in session
    End Sub

    Private Sub BindDDLStatusReport()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumBabit.BabitReportStatus"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))

        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        Dim arrDDL2 As New ArrayList
        For Each obj As StandardCode In arrDDL
            If IsLoginAsDealer() Then
                Select Case obj.ValueCode
                    Case "Validasi"
                        arrDDL2.Add(obj)
                End Select
            Else
                Select Case obj.ValueCode
                    Case "Konfirmasi"
                        arrDDL2.Add(obj)
                End Select
            End If
        Next

        ddlStatus.Items.Clear()
        ddlStatus.Items.Insert(0, New ListItem("Silakan Piih ", -1))
        For Each obj As StandardCode In arrDDL2
            ddlStatus.Items.Add(New ListItem(obj.ValueDesc, obj.ValueId))
        Next
        If IsLoginAsDealer() Then
            ddlStatus.Items.Add(New ListItem("Batal Validasi", 99))
        End If

        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub BindDDLAllocationBabit()
        With ddlCategoryAlloc
            .Items.Clear()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))

            Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code
            Dim arrDDL As ArrayList = New CategoryFacade(User).Retrieve(criterias, sortColl)
            Dim i% = 1
            For Each objCategory As Category In arrDDL
                .Items.Insert(i, New ListItem("BABIT " & objCategory.CategoryCode, objCategory.CategoryCode))
                i += 1
            Next
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitMasterPrice), "SpecialCategoryFlag", MatchType.Exact, 1))
            Dim arrDDL2 As ArrayList = New BabitMasterPriceFacade(User).Retrieve(criterias2)
            Dim newArrDDL2 = From obj As BabitMasterPrice In arrDDL2
                                         Group By obj.SubCategoryVehicle.ID Into Group
                                    Select ID
            For Each id As Integer In newArrDDL2
                Dim obj As SubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(CType(id, Short))
                .Items.Insert(i, New ListItem("BABIT " & obj.Name, obj.Name.Replace(" ", "_")))
                i += 1
            Next
        End With

        ddlCategoryAlloc.SelectedIndex = 0
    End Sub

    Private Sub BindDdlLs()
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlBMET As ArrayList = New BabitMasterEventTypeFacade(User).Retrieve(crits)
        ddlBabitType.Items.Clear()
        With ddlBabitType.Items
            .Add(New ListItem("Silahkan Pilih", "-1", True))
            For Each type As BabitMasterEventType In arlBMET
                If type.FormType < 4 Then
                    .Add(New ListItem(type.TypeName, type.ID))
                End If
            Next
        End With

        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumBabit.BabitReportStatus"))
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
        If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Laporan_Display_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - DAFTAR LAPORAN BABIT")
        Else
            displayPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Laporan_Display_Privilege)
            editPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Laporan_Edit_Privilege)
            deletePriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Laporan_Delete_Privilege)
            addKuitansiPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Laporan_Buat_Kuitansi_Privilege)
        End If
    End Sub

    Private Sub PageInit()
        icPeriodeStart.Value = Date.Now.AddMonths(-1)
        icPeriodeEnd.Value = Date.Now
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrBabitReportHeaderList As ArrayList = CType(sesHelper.GetSession(SessionGridDataBabitReport), ArrayList)
        If arrBabitReportHeaderList.Count > 0 Then
            CommonFunction.SortListControl(arrBabitReportHeaderList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrBabitReportHeaderList, pageIndex, dgBabitReportList.PageSize)
            dgBabitReportList.DataSource = PagedList
            dgBabitReportList.VirtualItemCount = arrBabitReportHeaderList.Count()
            dgBabitReportList.DataBind()

            lblChangeStatus.Visible = True
            ddlStatus.Visible = True
            btnProses.Visible = True
        Else
            dgBabitReportList.DataSource = New ArrayList
            dgBabitReportList.VirtualItemCount = 0
            dgBabitReportList.CurrentPageIndex = 0
            dgBabitReportList.DataBind()

            lblChangeStatus.Visible = False
            ddlStatus.Visible = False
            btnProses.Visible = False
        End If
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If cbDate.Checked Then
            Dim tglFrom As New Date(icPeriodeStart.Value.Year, icPeriodeStart.Value.Month, icPeriodeStart.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icPeriodeEnd.Value.Year, icPeriodeEnd.Value.Month, icPeriodeEnd.Value.Day, 23, 59, 59)
            crit.opAnd(New Criteria(GetType(BabitReportHeader), "PeriodStart", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "((", True)
            crit.opAnd(New Criteria(GetType(BabitReportHeader), "PeriodStart", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), ")", False)

            crit.opOr(New Criteria(GetType(BabitReportHeader), "PeriodEnd", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "(", True)
            crit.opAnd(New Criteria(GetType(BabitReportHeader), "PeriodEnd", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), "))", False)
        End If

        If Not IsLoginAsDealer() Then
            If (txtKodeDealer.Text.Trim <> String.Empty) Then
                crit.opAnd(New Criteria(GetType(BabitReportHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
            End If
        Else
            If txtKodeDealer.Text.Trim <> String.Empty Then
                crit.opAnd(New Criteria(GetType(BabitReportHeader), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text.Trim))
            End If
        End If
        If txtKodeTempOut.Text.Trim <> String.Empty Then
            crit.opAnd(New Criteria(GetType(BabitReportHeader), "BabitHeader.DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtKodeTempOut.Text.Replace(";", "','") & "')"))
        End If

        If ddlBabitType.SelectedIndex <> 0 Then
            Dim strSql As String = "(SELECT ID FROM BabitHeader WHERE BabitMasterEventTypeID ='" & ddlBabitType.SelectedValue & "')"
            crit.opAnd(New Criteria(GetType(BabitReportHeader), "BabitHeader.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If ddlCategoryAlloc.SelectedIndex <> 0 Then
            Dim strSql As String = "(SELECT BabitHeaderID FROM BabitDealerAllocation WHERE BabitCategory ='" & ddlCategoryAlloc.SelectedValue & "')"
            crit.opAnd(New Criteria(GetType(BabitReportHeader), "BabitHeader.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If txtBabitRegNo.Text.Trim <> "" Then
            Dim strSql As String = "(SELECT ID FROM BabitHeader WHERE BabitRegNumber like '%" & txtBabitRegNo.Text.Trim & "%')"
            crit.opAnd(New Criteria(GetType(BabitReportHeader), "BabitHeader.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If txtNomorSurat.Text.Trim <> "" Then
            Dim strSql As String = "(SELECT ID FROM BabitHeader WHERE BabitDealerNumber like '%" & txtNomorSurat.Text.Trim & "%')"
            crit.opAnd(New Criteria(GetType(BabitReportHeader), "BabitHeader.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If lsStatus.SelectedValue <> "" Then
            crit.opAnd(New Criteria(GetType(BabitReportHeader), "BabitReportStatus", MatchType.Exact, lsStatus.SelectedValue))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BabitReportHeader), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrBabitReportHeaderList As ArrayList = New BabitReportHeaderFacade(User).RetrieveByCriteria(crit, sortColl)
        sesHelper.SetSession(SessionGridDataBabitReport, arrBabitReportHeaderList)
        If arrBabitReportHeaderList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ReadData()
        dgBabitReportList.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dgBabitReportList.CurrentPageIndex)  '-- Bind page-1
        StoreCriteria()
    End Sub

    Private Sub dgBabitReportList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgBabitReportList.PageIndexChanged
        '-- Change datagrid page

        ReadData()
        dgBabitReportList.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgBabitReportList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgBabitReportList.SortCommand
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
        dgBabitReportList.CurrentPageIndex = 0
        ReadData()
        BindGrid(dgBabitReportList.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Protected Sub dgBabitReportList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBabitReportList.ItemCommand
        Dim oBabitReportHeader As BabitReportHeader = New BabitReportHeader
        If e.Item.ItemType <> ListItemType.Pager AndAlso e.Item.ItemType <> ListItemType.Header Then
            oBabitReportHeader = New BabitReportHeaderFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
        End If
        Select Case e.CommandName
            Case "Detail"
                Select Case oBabitReportHeader.BabitHeader.BabitMasterEventType.FormType
                    Case 1
                        Response.Redirect("FrmInputBabitReportEvent.aspx?Mode=Detail&BabitReportHeaderID=" & e.Item.Cells(0).Text)
                    Case 2
                        Response.Redirect("FrmInputBabitPameranReport.aspx?Mode=Detail&BabitReportHeaderID=" & e.Item.Cells(0).Text)
                    Case 3
                        Response.Redirect("FrmInputBabitIklanReport.aspx?Mode=Detail&BabitReportHeaderID=" & e.Item.Cells(0).Text)
                End Select
            Case "Edit"
                Select Case oBabitReportHeader.BabitHeader.BabitMasterEventType.FormType
                    Case 1
                        Response.Redirect("FrmInputBabitReportEvent.aspx?Mode=Edit&BabitReportHeaderID=" & e.Item.Cells(0).Text)
                    Case 2
                        Response.Redirect("FrmInputBabitPameranReport.aspx?Mode=Edit&BabitReportHeaderID=" & e.Item.Cells(0).Text)
                    Case 3
                        Response.Redirect("FrmInputBabitIklanReport.aspx?Mode=Edit&BabitReportHeaderID=" & e.Item.Cells(0).Text)
                End Select
            Case "Delete"
                DeleteBabitReportEvent(CInt(e.Item.Cells(0).Text))
            Case "ViewKuitansi"
                Dim strMode As String = "New"
                Dim intBabitReportReceiptID As Integer = 0
                Dim intBabitReportHeaderID As Integer = CInt(e.Item.Cells(0).Text)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitReportReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitReportReceipt), "BabitReportHeader.ID", MatchType.Exact, intBabitReportHeaderID))
                Dim arlBabitReportReceipt As ArrayList = New BabitReportReceiptFacade(User).Retrieve(criterias)
                If Not IsNothing(arlBabitReportReceipt) AndAlso arlBabitReportReceipt.Count > 0 Then
                    Dim objBabitReportReceipt As BabitReportReceipt = CType(arlBabitReportReceipt(0), BabitReportReceipt)
                    intBabitReportReceiptID = objBabitReportReceipt.ID
                    strMode = "Edit"
                End If

                Response.Redirect("FrmInputBabitReportReceipt.aspx?Mode=" & strMode & "&BabitReportHeaderID=" & intBabitReportHeaderID & "&BabitReportReceiptID=" & intBabitReportReceiptID)

        End Select
    End Sub

    Private Sub DeleteBabitReportEvent(ByVal intBabitReportHdrID As Integer)
        Dim objBabitReportHeaderFacade As BabitReportHeaderFacade = New BabitReportHeaderFacade(User)
        Dim objBabitReportEventDetailFacade As BabitReportEventDetailFacade = New BabitReportEventDetailFacade(User)
        Dim objBabitReportDocumentFacade As BabitReportDocumentFacade = New BabitReportDocumentFacade(User)

        Dim objBabitReportHeader As BabitReportHeader = objBabitReportHeaderFacade.Retrieve(intBabitReportHdrID)
        objBabitReportHeader.RowStatus = CType(DBRowStatus.Deleted, Short)

        Dim arrBabitReportEventDetail As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitReportEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitReportEventDetail), "BabitReportHeader.ID", MatchType.Exact, intBabitReportHdrID))
        arrBabitReportEventDetail = New BabitReportEventDetailFacade(User).Retrieve(criterias)
        For Each obj As BabitReportEventDetail In arrBabitReportEventDetail
            obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        Next

        Dim arrBabitReportEventDoc As ArrayList = New ArrayList
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitReportDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(BabitReportDocument), "BabitReportHeader.ID", MatchType.Exact, intBabitReportHdrID))
        arrBabitReportEventDoc = objBabitReportDocumentFacade.Retrieve(criterias2)
        For Each obj As BabitReportDocument In arrBabitReportEventDoc
            obj.RowStatus = CType(DBRowStatus.Deleted, Short)
        Next

        Dim _result As Integer = 0
        _result = New BabitReportEventDetailFacade(User).UpdateTransaction(objBabitReportHeader, arrBabitReportEventDetail, New ArrayList, arrBabitReportEventDoc, New ArrayList, New ArrayList, New ArrayList)
        If _result > 0 Then
            MessageBox.Show("Delete Data Sukses")
        End If

        ReadData()
        BindGrid(dgBabitReportList.CurrentPageIndex)
    End Sub

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click
        Dim sb As StringBuilder = New StringBuilder

        If ddlStatus.SelectedIndex = 0 Then
            MessageBox.Show("Pilih daftar status terlebih dahulu")
            Exit Sub
        End If
        Dim objBabitReportHeader As New BabitReportHeader
        Dim objBabitReportHeaderFacade As BabitReportHeaderFacade = New BabitReportHeaderFacade(User)
        Dim intBabitReportHdrID As Integer = 0

        Dim nResult As Integer
        Dim checkCounter As Integer = 0

        ' Status Babit Report :
        '0: Baru
        '1: Validasi
        '2: Konfirmasi

        If dgBabitReportList.Items.Count > 0 Then
            For Each item As DataGridItem In dgBabitReportList.Items
                Dim chckbox As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)

                Dim StrVal As String = String.Empty
                intBabitReportHdrID = Convert.ToInt32(item.Cells(0).Text)
                objBabitReportHeader = objBabitReportHeaderFacade.Retrieve(intBabitReportHdrID)
                If chckbox.Checked Then
                    Dim strStatusName As String = CType(New StandardCodeFacade(User).RetrieveByValueId(objBabitReportHeader.BabitReportStatus.ToString(), "EnumBabit.BabitReportStatus")(0), StandardCode).ValueCode

                    If objBabitReportHeader.BabitReportStatus = 0 And CByte(ddlStatus.SelectedValue) = 2 Then   'status Batal
                        StrVal = "Ubah status menjadi Konfirmasi hanya dapat dilakukan untuk data yang statusnya Validasi"
                        sb.Append("Item " & item.ItemIndex + 1 & " : Status " & strStatusName & " tidak bisa diubah ke status Konfirmasi\n" & StrVal & "\n")
                    End If
                    If objBabitReportHeader.BabitReportStatus = 2 And CByte(ddlStatus.SelectedValue) = 1 Then   'status Validasi
                        StrVal = "Ubah status menjadi Validasi hanya dapat dilakukan untuk data yang statusnya Baru"
                        sb.Append("Item " & item.ItemIndex + 1 & " : Status " & strStatusName & " tidak bisa diubah ke status Validasi\n" & StrVal & "\n")
                    End If
                    If objBabitReportHeader.BabitReportStatus = 0 And CByte(ddlStatus.SelectedValue) = 99 Then   'status Batal Validasi
                        StrVal = "Batal Validasi hanya dapat dilakukan untuk data yang statusnya Validasi"
                        sb.Append("Item " & item.ItemIndex + 1 & " : Status " & strStatusName & " tidak bisa Batal Validasi\n" & StrVal & "\n")
                    End If
                End If
            Next
            If (sb.ToString().Length > 0) Then
                MessageBox.Show(sb.ToString())
                Exit Sub
            End If

            For Each item As DataGridItem In dgBabitReportList.Items
                Dim chckbox As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)

                intBabitReportHdrID = Convert.ToInt32(item.Cells(0).Text)
                objBabitReportHeader = objBabitReportHeaderFacade.Retrieve(intBabitReportHdrID)
                If chckbox.Checked Then
                    If ddlStatus.SelectedValue = 99 Then   'Jika ststus nya Batal Validasi
                        objBabitReportHeader.BabitReportStatus = 0
                    Else
                        objBabitReportHeader.BabitReportStatus = ddlStatus.SelectedValue
                    End If
                    nResult = objBabitReportHeaderFacade.Update(objBabitReportHeader)

                    If nResult = -1 Then
                        sb.Append("Laporan untuk Nomor Reg Babit: " & objBabitReportHeader.BabitHeader.BabitRegNumber + 1 & " tidak bisa di update status\n")
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

    Protected Sub dgBabitReportList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgBabitReportList.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
        Dim lblTempOut As Label = CType(e.Item.FindControl("lblTempOut"), Label)
        Dim lblBabitRegNumber As Label = CType(e.Item.FindControl("lblBabitRegNumber"), Label)
        Dim lblBabitDealerNumber As Label = CType(e.Item.FindControl("lblBabitDealerNumber"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Dim lblBabitType As Label = CType(e.Item.FindControl("lblBabitType"), Label)
        Dim lblSubsidyAmount As Label = CType(e.Item.FindControl("lblSubsidyAmount"), Label)
        Dim lnkbtnKuitansi As LinkButton = CType(e.Item.FindControl("lnkbtnKuitansi"), LinkButton)

        Dim lblPeriodStart As Label = CType(e.Item.FindControl("lblPeriodStart"), Label)
        Dim lblPeriodEnd As Label = CType(e.Item.FindControl("lblPeriodEnd"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As BabitReportHeader = CType(e.Item.DataItem, BabitReportHeader)

            lblNo.Text = (e.Item.ItemIndex + 1 + (dgBabitReportList.PageSize * dgBabitReportList.CurrentPageIndex)).ToString

            If Not IsNothing(oData.Dealer) Then
                lblDealer.Text = oData.Dealer.DealerCode
            End If
            If Not IsNothing(oData.BabitHeader.DealerBranch) Then
                lblTempOut.Text = oData.BabitHeader.DealerBranch.DealerBranchCode
            End If
            'Select Case oData.BabitReportStatus
            '    Case 0
            '        lblStatus.Text = "Baru"
            '    Case 1
            '        lblStatus.Text = "Validasi"
            '    Case 2
            '        lblStatus.Text = "Konfirmasi"
            'End Select
            lblStatus.Text = CommonFunction.GetEnumDescription(oData.BabitReportStatus, "EnumBabit.BabitReportStatus")

            lblBabitRegNumber.Text = oData.BabitHeader.BabitRegNumber
            lblBabitDealerNumber.Text = oData.BabitHeader.BabitDealerNumber
            lblBabitType.Text = oData.BabitHeader.BabitMasterEventType.TypeName
            lblPeriodStart.Text = oData.PeriodStart
            lblPeriodEnd.Text = oData.PeriodEnd

            Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lnkbtnDetail"), LinkButton)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)

            If Not IsNothing(lnkbtnDetail) Then
                lnkbtnDetail.Visible = displayPriv
            End If
            If Not IsNothing(lnkbtnEdit) Then
                lnkbtnEdit.Visible = editPriv
            End If
            If Not IsNothing(lnkbtnDelete) Then
                lnkbtnDelete.Visible = deletePriv
            End If

            If IsLoginAsDealer() Then
                Select Case oData.BabitReportStatus
                    Case 0  '--Status Baru
                        lnkbtnEdit.Visible = editPriv
                        lnkbtnDelete.Visible = deletePriv
                    Case 1, 2    '---Status Validasi and Konfirmasi
                        lnkbtnEdit.Visible = False
                        lnkbtnDelete.Visible = False
                End Select

                If oData.BabitReportStatus = 2 Then     '--> Konfirmasi
                    lnkbtnKuitansi.Visible = addKuitansiPriv
                Else
                    lnkbtnKuitansi.Visible = False
                End If
            Else
                lnkbtnEdit.Visible = False
                lnkbtnDelete.Visible = False
                lnkbtnKuitansi.Visible = False
            End If

            Dim dblSubsidyAmount As Double = 0
            Dim dsBabitReportHeader As DataSet = New BabitReportHeaderFacade(User).DoRetrieveDataSet(oData.BabitHeader.ID)

            Try
                dblSubsidyAmount = dsBabitReportHeader.Tables(0).Rows(0)("SumSubsidyAmount")
                lblSubsidyAmount.Text = IIf(dblSubsidyAmount <= 0, 0, dblSubsidyAmount.ToString("#,##0"))
            Catch
            End Try
        End If
    End Sub

    Protected Sub hdnDealer_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealer.ValueChanged
        'Dim data As String() = hdnDealer.Value.Trim.Split(";")
        txtKodeDealer.Text = hdnDealer.Value.Trim
    End Sub

    Protected Sub hdnTempOut_ValueChanged(sender As Object, e As EventArgs) Handles hdnTempOut.ValueChanged
        'Dim data As String() = hdnTempOut.Value.Trim.Split(";")
        txtKodeTempOut.Text = hdnTempOut.Value.Trim
    End Sub


    Protected Sub btnDownloadExcel_Click(sender As Object, e As EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        If dgBabitReportList.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        ' mengambil data yang dibutuhkan
        arrData = CType(sesHelper.GetSession(SessionGridDataBabitReport), ArrayList)
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
            ws.Cells("E3").Value = "Nomor Reg Babit"
            ws.Cells("F3").Value = "Nomor Surat"
            ws.Cells("G3").Value = "Tipe Babit"
            ws.Cells("H3").Value = "Tgl Periode Mulai"
            ws.Cells("I3").Value = "Tgl Periode Selesai"
            ws.Cells("J3").Value = "Biaya Disetujui"

            For i As Integer = 0 To Data.Count - 1
                Dim item As BabitReportHeader = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                ws.Cells(i + 4, 2).Value = IIf(item.BabitReportStatus = 0, "Baru", IIf(item.BabitReportStatus = 1, "Batal", "Konfirmasi"))
                If IsNothing(item.Dealer) Then
                    ws.Cells(i + 4, 3).Value = ""
                Else
                    ws.Cells(i + 4, 3).Value = item.Dealer.DealerCode
                End If
                If IsNothing(item.BabitHeader.DealerBranch) Then
                    ws.Cells(i + 4, 4).Value = ""
                Else
                    ws.Cells(i + 4, 4).Value = item.BabitHeader.DealerBranch.DealerBranchCode
                End If
                ws.Cells(i + 4, 5).Value = item.BabitHeader.BabitRegNumber
                ws.Cells(i + 4, 6).Value = item.BabitHeader.BabitDealerNumber
                ws.Cells(i + 4, 7).Value = item.BabitHeader.BabitMasterEventType.TypeName
                ws.Cells(i + 4, 8).Value = Format(item.PeriodStart, "dd/MM/yyyy")
                ws.Cells(i + 4, 9).Value = Format(item.PeriodEnd, "dd/MM/yyyy")

                Dim dblSubsidyAmount As Double = 0
                Dim dsBabitReportHeader As DataSet = New BabitReportHeaderFacade(User).DoRetrieveDataSet(item.BabitHeader.ID)
                Try
                    dblSubsidyAmount = dsBabitReportHeader.Tables(0).Rows(0)("SumSubsidyAmount")
                Catch
                End Try
                ws.Cells(i + 4, 10).Value = dblSubsidyAmount.ToString("#,##0")
            Next

            '=========================================================
            FileName = "DAFTAR HASIL SPK"
            ws = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Nomor Reg Babit"
            ws.Cells("C3").Value = "Nomor Surat"
            ws.Cells("D3").Value = "Tgl Periode Mulai"
            ws.Cells("E3").Value = "Tgl Periode Selesai"
            ws.Cells("F3").Value = "Kode Dealer"
            ws.Cells("G3").Value = "Nama Dealer"
            ws.Cells("H3").Value = "Kategori Kendaraan"
            ws.Cells("I3").Value = "Qty Unit"

            Dim idx As Integer = 0
            Dim arrDataDownload1 As New ArrayList
            For j As Integer = 0 To Data.Count - 1
                Dim item As BabitReportHeader = Data(j)

                Dim arrDataDownload12 As New ArrayList
                If Left(item.BabitHeader.BabitRegNumber, 1) <> "I" Then
                    arrDataDownload1 = BindGridBabitEventSPK(item.BabitHeader.BabitRegNumber)
                    arrDataDownload12 = New System.Collections.ArrayList((From _BabitHeader In arrDataDownload1
                                           Group _BabitHeader By keys = New With {Key _BabitHeader.BabitRegNumber, Key _BabitHeader.DealerCode, _
                                                                                  Key _BabitHeader.DealerName, Key _BabitHeader.VechileTypeKind}
                                                Into Group
                                           Select New BabitHeader With {.BabitRegNumber = keys.BabitRegNumber, _
                                                                        .DealerCode = keys.DealerCode, _
                                                                        .DealerName = keys.DealerName, _
                                                                        .VechileTypeKind = keys.VechileTypeKind, _
                                                                        .QtyUnit = Group.Sum(Function(x) x.QtyUnit)}).ToList())

                    For i As Integer = 0 To arrDataDownload12.Count - 1
                        Dim itemDtl As BabitHeader = arrDataDownload12(i)
                        ws.Cells(idx + 4, 1).Value = idx + 1
                        ws.Cells(idx + 4, 2).Value = itemDtl.BabitRegNumber
                        ws.Cells(idx + 4, 3).Value = item.BabitHeader.BabitDealerNumber
                        ws.Cells(idx + 4, 4).Value = Format(item.BabitHeader.PeriodStart, "dd/MM/yyyy")
                        ws.Cells(idx + 4, 5).Value = Format(item.BabitHeader.PeriodEnd, "dd/MM/yyyy")
                        ws.Cells(idx + 4, 6).Value = itemDtl.DealerCode
                        ws.Cells(idx + 4, 7).Value = itemDtl.DealerName
                        ws.Cells(idx + 4, 8).Value = itemDtl.VechileTypeKind
                        ws.Cells(idx + 4, 9).Value = itemDtl.QtyUnit
                        idx += 1
                    Next
                End If
            Next


            '=========================================================
            FileName = "DAFTAR HASIL PROSPEK"
            ws = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Nomor Reg Babit"
            ws.Cells("C3").Value = "Nomor Surat"
            ws.Cells("D3").Value = "Tgl Periode Mulai"
            ws.Cells("E3").Value = "Tgl Periode Selesai"
            ws.Cells("F3").Value = "Kode Dealer"
            ws.Cells("G3").Value = "Nama Dealer"
            ws.Cells("H3").Value = "Kategori Kendaraan"
            ws.Cells("I3").Value = "Qty Unit"

            idx = 0
            Dim arrDataDownload2 As New ArrayList
            For j As Integer = 0 To Data.Count - 1
                Dim item As BabitReportHeader = Data(j)

                Dim arrDataDownload22 As New ArrayList
                If Left(item.BabitHeader.BabitRegNumber, 1) <> "I" Then
                    arrDataDownload2 = BindGridBabitEventSPKProspek(item.BabitHeader.BabitRegNumber)
                    arrDataDownload22 = New System.Collections.ArrayList((From _BabitHeader In arrDataDownload2
                                           Group _BabitHeader By keys = New With {Key _BabitHeader.BabitRegNumber, Key _BabitHeader.DealerCode, _
                                                                                  Key _BabitHeader.DealerName, Key _BabitHeader.VechileTypeKind}
                                                Into Group
                                           Select New BabitHeader With {.BabitRegNumber = keys.BabitRegNumber, _
                                                                        .DealerCode = keys.DealerCode, _
                                                                        .DealerName = keys.DealerName, _
                                                                        .VechileTypeKind = keys.VechileTypeKind, _
                                                                        .QtyUnit = Group.Sum(Function(x) x.QtyUnit)}).ToList())

                    For i As Integer = 0 To arrDataDownload22.Count - 1
                        Dim itemDtl As BabitHeader = arrDataDownload22(i)
                        ws.Cells(idx + 4, 1).Value = idx + 1
                        ws.Cells(idx + 4, 2).Value = itemDtl.BabitRegNumber
                        ws.Cells(idx + 4, 3).Value = item.BabitHeader.BabitDealerNumber
                        ws.Cells(idx + 4, 4).Value = Format(item.BabitHeader.PeriodStart, "dd/MM/yyyy")
                        ws.Cells(idx + 4, 5).Value = Format(item.BabitHeader.PeriodEnd, "dd/MM/yyyy")
                        ws.Cells(idx + 4, 6).Value = itemDtl.DealerCode
                        ws.Cells(idx + 4, 7).Value = itemDtl.DealerName
                        ws.Cells(idx + 4, 8).Value = itemDtl.VechileTypeKind
                        ws.Cells(idx + 4, 9).Value = itemDtl.QtyUnit
                        idx += 1
                    Next
                End If
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using

    End Sub

    Function BindGridBabitEventSPK(ByVal strBabitRegNumber As String) As ArrayList
        Dim dSBabitSPKList As DataSet = New BabitHeaderFacade(User).RetrieveFromSPSPK(strBabitRegNumber)
        Dim _babitHeader As New BabitHeader
        Dim row As DataRow
        Dim i As Integer = 0
        Dim arrBabitSPKList As ArrayList = New ArrayList
        For i = 0 To dSBabitSPKList.Tables(0).Rows.Count - 1
            row = dSBabitSPKList.Tables(0).Rows(i)
            Try
                _babitHeader = New BabitHeader
                _babitHeader.ID = row("ID")
                _babitHeader.BabitRegNumber = row("BabitRegNumber")
                _babitHeader.VechileTypeKind = row("VechileTypeKind")
                _babitHeader.VechileTypeName = row("VechileTypeName")
                _babitHeader.DealerCode = row("DealerCode")
                _babitHeader.DealerName = row("DealerName")
                _babitHeader.QtyUnit = row("QtyUnit")
                arrBabitSPKList.Add(_babitHeader)

            Catch ex As Exception
            End Try
        Next

        Return arrBabitSPKList
    End Function

    Function BindGridBabitEventSPKProspek(ByVal strBabitRegNumber As String) As ArrayList
        Dim dSBabitSPKList As DataSet = New BabitHeaderFacade(User).RetrieveFromSPSPKProspek(strBabitRegNumber)
        Dim _babitHeader As New BabitHeader
        Dim row As DataRow
        Dim i As Integer = 0
        Dim arrBabitSPKList As ArrayList = New ArrayList
        For i = 0 To dSBabitSPKList.Tables(0).Rows.Count - 1
            row = dSBabitSPKList.Tables(0).Rows(i)
            Try
                _babitHeader = New BabitHeader
                _babitHeader.ID = row("ID")
                _babitHeader.BabitRegNumber = row("BabitRegNumber")
                _babitHeader.VechileTypeKind = row("VechileTypeKind")
                _babitHeader.VechileTypeName = row("VechileTypeName")
                _babitHeader.DealerCode = row("DealerCode")
                _babitHeader.DealerName = row("DealerName")
                _babitHeader.QtyUnit = row("QtyUnit")
                arrBabitSPKList.Add(_babitHeader)

            Catch ex As Exception
            End Try
        Next

        Return arrBabitSPKList
    End Function

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