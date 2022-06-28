Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Collections.Generic
'Imports System.Linq
Imports OfficeOpenXml
Imports System.Linq

Public Class FrmBabitList
    Inherits System.Web.UI.Page
    Private sessHelper As New SessionHelper
    Private arlCheckedItemColl As ArrayList = New ArrayList
    Private editPriv As Boolean
    Private deletePriv As Boolean
    Private oDealer As Dealer
    Dim en As EnumBabit
    Private SessionCriteriaBabit = "FrmBabitList.CriteriaList"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)

        ViewState("currSortColumn") = "ID"
        ViewState("currSortDirection") = Sort.SortDirection.DESC

        Authorization()
        'txtBabitRegNo.Text = "I051900035"
        'txtBabitRegNo.Text = "E051900003"
        'txtBabitRegNo.Text = "P051900001"
        If Not IsPostBack Then
            PageInit()

            If Not IsNothing(Request.QueryString("Success")) Then
                MessageBox.Show(Request.QueryString("Success").ToString())
            End If

            '-- Restore selection criteria
            ReadCriteria()
            BindGrid(0)
        End If

        lblPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
        lblPopUpTO.Attributes("onclick") = "ShowPopUpTO()"
        If Not IsNothing(ViewState("Done")) Then
            MessageBox.Show("Simpan Berhasil")
            ViewState.Remove("Done")
        End If
    End Sub

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sessHelper.GetSession(SessionCriteriaBabit), Hashtable)
        If Not crit Is Nothing Then
            ddlCategoryAlloc.SelectedValue = CStr(crit.Item("ddlCategoryAlloc"))
            txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
            lblKodeDealer.Text = CStr(crit.Item("DealerCode"))
            txtKodeTempOut.Text = CStr(crit.Item("DealerBranchCode"))
            cbDate.Checked = CBool(crit.Item("cbDate"))
            icPeriodeStart.Value = CStr(crit.Item("PeriodStart"))
            icPeriodeEnd.Value = CStr(crit.Item("PeriodEnd"))
            ddlBabitType.SelectedValue = CStr(crit.Item("ddlBabitType"))
            txtBabitRegNo.Text = CStr(crit.Item("txtBabitRegNo"))
            txtNomorSurat.Text = CStr(crit.Item("txtNomorSurat"))
            lsStatus.SelectedValue = CStr(crit.Item("lsStatus"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgListBabit.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("ddlCategoryAlloc", ddlCategoryAlloc.SelectedValue)
        crit.Add("DealerCode", txtKodeDealer.Text)
        crit.Add("DealerBranchCode", txtKodeTempOut.Text)
        crit.Add("cbDate", cbDate.Checked)
        crit.Add("PeriodStart", icPeriodeStart.Value)
        crit.Add("PeriodEnd", icPeriodeEnd.Value)
        crit.Add("ddlBabitType", ddlBabitType.SelectedValue)
        crit.Add("txtBabitRegNo", txtBabitRegNo.Text)
        crit.Add("txtNomorSurat", txtNomorSurat.Text)
        crit.Add("lsStatus", lsStatus.SelectedValue)

        crit.Add("PageIndex", dgListBabit.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sessHelper.SetSession(SessionCriteriaBabit, crit)  '-- Store in session
    End Sub

    Private Sub Authorization()
        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtKodeDealer.Visible = True
            lblKodeDealer.Visible = False
            lblPopUpDealer.Visible = True
            Exit Sub
        Else
            txtKodeDealer.Visible = False
            lblKodeDealer.Visible = True
            txtKodeDealer.Text = oDealer.DealerCode
            lblKodeDealer.Text = oDealer.DealerCode & " / " & oDealer.DealerName
            lblPopUpDealer.Visible = False
        End If
        If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Babit_Display_Privilege) OrElse
            Not SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Babit_Edit_Privilege) OrElse
            Not SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Babit_Delete_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - DAFTAR PENGAJUAN BABIT")
        Else
            editPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Babit_Edit_Privilege)
            deletePriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Babit_Delete_Privilege)
        End If
    End Sub

    Private Sub PageInit()
        BindDdlLs()
        BindDDLAllocationBabit()

        icPeriodeStart.Value = Date.Now.AddMonths(-1)
        icPeriodeEnd.Value = Date.Now
        If Not IsNothing(Request.QueryString("Done")) Then
            ViewState("Done") = "OK"
        End If
        If Not IsNothing(Request.QueryString("Back")) Then
            ViewState("Back") = "OK"
        End If

        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Me.btnTransfer.Visible = True
            Me.btnTransferUlang.Visible = True
        Else
            Me.btnTransfer.Visible = False
            Me.btnTransferUlang.Visible = False
        End If
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
        en = New EnumBabit
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
            '.Add(New ListItem("Event", 1))
            '.Add(New ListItem("Pameran", 2))
            '.Add(New ListItem("Iklan", 3))
        End With

        lsStatus.Items.Clear()
        lsStatus.DataTextField = "BabitValue"
        lsStatus.DataValueField = "BabitCode"
        lsStatus.DataSource = en.StatusGroupware()
        lsStatus.DataBind()

        ddlAllocStatus.Items.Clear()
        With ddlAllocStatus.Items
            .Add(New ListItem("Silahkan Pilih", "0"))
            .Add(New ListItem("Belum Dialokasi", "1"))
            .Add(New ListItem("Sudah Dialokasi", "2"))
        End With

        ddlAction.Items.Clear()
        With ddlAction.Items
            .Add(New ListItem("Silahkan Pilih", "-1", True))
            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                .Add(New ListItem("Konfirmasi", "2", True))
                .Add(New ListItem("Revisi", "3", True))
            Else
                .Add(New ListItem("Validasi", "1", True))
                .Add(New ListItem("Batal Validasi", "0", True))
            End If
        End With
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim total As Integer = 0
        Dim crit As CriteriaComposite
        If Not IsNothing(ViewState("Back")) Then
            'crit = sessHelper.GetSession("criteriadownload")
            ReadCriteria()
            crit = SearchCriteria()
            ViewState.Remove("Back")
        Else
            crit = SearchCriteria()
        End If

        If Not IsNothing(CType(sessHelper.GetSession("FrmBabitList"), ArrayList)) Then
            sessHelper.SetSession("_PgIdxBefore", CType(sessHelper.GetSession("_PgIdxNext"), Integer))
            sessHelper.SetSession("_PgIdxNext", pageIndex)

            arlCheckedItemColl = GetCheckedItem()
            Dim currentPage As String = CType(sessHelper.GetSession("_PgIdxBefore"), String)
            sessHelper.SetSession("babitSessProcess" + currentPage, arlCheckedItemColl)

            Dim arlUnCheckedItemColl As ArrayList = GetUnCheckedItem()
            currentPage = CType(sessHelper.GetSession("_PgIdxBefore"), String)
            sessHelper.SetSession("babitSessProcess2" + currentPage, arlUnCheckedItemColl)
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BabitHeader), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        Dim listSource As ArrayList = New BabitHeaderFacade(User).Retrieve(crit, sortColl)
        'Dim listSource As ArrayList = New BabitHeaderFacade(User).RetrieveActiveList(crit, pageIndex + 1, dgListBabit.PageSize, _
        'total, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If listSource.Count <> 0 Then
            CommonFunction.SortListControl(listSource, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(listSource, pageIndex, dgListBabit.PageSize)
            sessHelper.SetSession("FrmBabitList", PagedList)
            dgListBabit.CurrentPageIndex = pageIndex
            dgListBabit.DataSource = PagedList
            dgListBabit.VirtualItemCount = listSource.Count
            dgListBabit.DataBind()
        Else
            dgListBabit.DataSource = New ArrayList
            dgListBabit.VirtualItemCount = 0
            dgListBabit.CurrentPageIndex = 0
            dgListBabit.DataBind()
        End If
        'dgListBabit.DataSource = listSource
        'dgListBabit.DataBind()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGrid(0)
        StoreCriteria()
        If dgListBabit.Items.Count = 0 Then
            MessageBox.Show("Data list tidak ditemukan")
        End If
    End Sub

    Protected Sub dgListBabit_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgListBabit.ItemCommand
        Dim type As String = ""
        If e.Item.ItemType <> ListItemType.Pager AndAlso e.Item.ItemType <> ListItemType.Header Then
            Dim objHeader As BabitHeader = New BabitHeaderFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
            Select Case objHeader.BabitMasterEventType.FormType
                Case 1
                    type = "Event"
                Case 2
                    type = "Pameran"
                Case 3
                    type = "Iklan"
            End Select
        End If

        Select Case e.CommandName
            Case "Detail"
                Response.Redirect("FrmInputBabit" & type & ".aspx?Mode=Detail&BabitHeaderID=" & e.Item.Cells(0).Text)
            Case "Edit"
                Response.Redirect("FrmInputBabit" & type & ".aspx?Mode=Edit&BabitHeaderID=" & e.Item.Cells(0).Text)
            Case "Delete"
                Dim result As Integer = 0
                Dim objBabitHeader As BabitHeader = New BabitHeaderFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                If objBabitHeader.BabitStatus <> 0 Then
                    MessageBox.Show("Delete hanya bisa dilakukan pada babit dengan status Baru")
                    Exit Select
                End If
                Dim crit As CriteriaComposite
                Select Case type
                    Case "Iklan"
                        crit = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                        Dim arrIDetail As ArrayList = New BabitIklanDetailFacade(User).Retrieve(crit)
                        crit = New CriteriaComposite(New Criteria(GetType(BabitDocument), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                        Dim arrDoc As ArrayList = New BabitDocumentFacade(User).Retrieve(crit)
                        crit = New CriteriaComposite(New Criteria(GetType(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                        Dim arrBabitDealerAllocation As ArrayList = New BabitDealerAllocationFacade(User).Retrieve(crit)
                        Dim objBabitDealerAllocation As BabitDealerAllocation
                        If arrBabitDealerAllocation.Count > 0 Then
                            objBabitDealerAllocation = CType(arrBabitDealerAllocation(0), BabitDealerAllocation)
                            result = New BabitIklanDetailFacade(User).DeleteTransaction(objBabitHeader, arrIDetail, arrDoc, objBabitDealerAllocation)
                        Else
                            result = New BabitIklanDetailFacade(User).DeleteTransaction(objBabitHeader, arrIDetail, arrDoc)
                        End If

                    Case "Event"
                        crit = New CriteriaComposite(New Criteria(GetType(BabitEventDetail), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                        Dim arrEDetail As ArrayList = New BabitEventDetailFacade(User).Retrieve(crit)
                        crit = New CriteriaComposite(New Criteria(GetType(BabitDocument), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                        Dim arrDoc As ArrayList = New BabitDocumentFacade(User).Retrieve(crit)
                        crit = New CriteriaComposite(New Criteria(GetType(BabitDisplayCar), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                        Dim arrDisplayCar As ArrayList = New BabitDisplayCarFacade(User).Retrieve(crit)
                        crit = New CriteriaComposite(New Criteria(GetType(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                        Dim arrBabitDealerAllocation As ArrayList = New BabitDealerAllocationFacade(User).Retrieve(crit)
                        Dim objBabitDealerAllocation As BabitDealerAllocation
                        If arrBabitDealerAllocation.Count > 0 Then
                            objBabitDealerAllocation = CType(arrBabitDealerAllocation(0), BabitDealerAllocation)
                            result = New BabitEventDetailFacade(User).DeleteTransaction(objBabitHeader, arrEDetail, arrDoc, arrDisplayCar, objBabitDealerAllocation)
                        Else
                            result = New BabitEventDetailFacade(User).DeleteTransaction(objBabitHeader, arrEDetail, arrDoc, arrDisplayCar)
                        End If

                    Case "Pameran"
                        crit = New CriteriaComposite(New Criteria(GetType(BabitPameranDetail), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                        Dim arrPDetail As ArrayList = New BabitPameranDetailFacade(User).Retrieve(crit)
                        crit = New CriteriaComposite(New Criteria(GetType(BabitPameranExpense), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                        Dim arrPExpense As ArrayList = New BabitPameranExpenseFacade(User).Retrieve(crit)
                        crit = New CriteriaComposite(New Criteria(GetType(BabitDocument), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                        Dim arrDoc As ArrayList = New BabitDocumentFacade(User).Retrieve(crit)
                        crit = New CriteriaComposite(New Criteria(GetType(BabitDisplayCar), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                        Dim arrDisplay As ArrayList = New BabitDisplayCarFacade(User).Retrieve(crit)
                        crit = New CriteriaComposite(New Criteria(GetType(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                        Dim arrBabitDealerAllocation As ArrayList = New BabitDealerAllocationFacade(User).Retrieve(crit)
                        If arrBabitDealerAllocation.Count > 0 Then
                            Dim objBabitDealerAllocation As BabitDealerAllocation = CType(arrBabitDealerAllocation(0), BabitDealerAllocation)
                            result = New BabitPameranDetailFacade(User).DeleteTransaction(objBabitHeader, arrPDetail, arrPExpense, arrDoc, arrDisplay, objBabitDealerAllocation)
                        Else
                            result = New BabitPameranDetailFacade(User).DeleteTransaction(objBabitHeader, arrPDetail, arrPExpense, arrDoc, arrDisplay)
                        End If

                End Select
            Case "Download"
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)

        End Select
        If Not IsNothing(ViewState("qweqweq")) Then
            BindGrid(ViewState("qweqweq"))
        Else
            BindGrid(0)
        End If
    End Sub

    Protected Sub dgListBabit_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgListBabit.ItemDataBound

        Dim objBH As BabitHeader = New BabitHeader
        Dim chkSelect As CheckBox = CType(e.Item.FindControl("chkSelect"), CheckBox)
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
        Dim lblTempOut As Label = CType(e.Item.FindControl("lblTempOut"), Label)
        Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
        Dim lblNoSurat As Label = CType(e.Item.FindControl("lblNoSurat"), Label)
        Dim lblBabitType As Label = CType(e.Item.FindControl("lblBabitType"), Label)
        Dim lblPeriodeStart As Label = CType(e.Item.FindControl("lblPeriodeStart"), Label)
        Dim lblPeriodeEnd As Label = CType(e.Item.FindControl("lblPeriodeEnd"), Label)
        Dim lblPengajuanBiaya As Label = CType(e.Item.FindControl("lblPengajuanBiaya"), Label)
        Dim lblSubsidyAmount As Label = CType(e.Item.FindControl("lblSubsidyAmount"), Label)
        Dim lblApprovalNumber As Label = CType(e.Item.FindControl("lblApprovalNumber"), Label)
        Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
        Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
        Dim lnkbtnDownload As LinkButton = CType(e.Item.FindControl("lnkbtnDownload"), LinkButton)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As BabitHeader = CType(e.Item.DataItem, BabitHeader)
            'lblNo.Text = e.Item.ItemIndex + 1
            chkSelect.Checked = False
            Dim currentPage As String = CType(sessHelper.GetSession("_PgIdxNext"), String)
            Dim arrGridDF As ArrayList = CType(sessHelper.GetSession("babitSessProcess" + currentPage), ArrayList)
            If IsNothing(arrGridDF) Then arrGridDF = New ArrayList
            For Each oDF As BabitHeader In arrGridDF
                If objBH.ID = oDF.ID Then
                    chkSelect.Checked = True
                    Exit For
                End If
            Next
            lblNo.Text = (dgListBabit.PageSize * dgListBabit.CurrentPageIndex) + e.Item.ItemIndex + 1
            'lblStatus.Text = IIf(Not oData.BabitStatus, "Aktif", "Tidak Aktif")
            'lblDealer.Text = IIf(IsNothing(oData.Dealer), "ID : " & oData.ID, oData.Dealer.DealerCode)
            If IsNothing(oData.Dealer) Then
                lblDealer.Text = oData.ID
            Else
                lblDealer.Text = oData.Dealer.DealerCode
            End If
            'lblTempOut.Text = IIf(IsNothing(oData.DealerBranch), "ID : " & oData.ID, oData.DealerBranch.DealerBranchCode)
            If IsNothing(oData.DealerBranch) Then
                lblTempOut.Text = ""
            Else
                lblTempOut.Text = oData.DealerBranch.DealerBranchCode
            End If
            lblNoReg.Text = oData.BabitRegNumber
            lblNoSurat.Text = oData.BabitDealerNumber
            'lblBabitType.Text = IIf(oData.BabitType.Trim = "E", "Event", IIf(oData.BabitType.Trim = "P", "Pameran", IIf(oData.BabitType.Trim = "I", "Iklan", "Other")))
            lblBabitType.Text = oData.BabitMasterEventType.TypeName
            lblApprovalNumber.Text = oData.ApprovalNumber

            'Select Case oData.BabitType
            '    Case "E"
            '        lblBabitType.Text = "Event"
            '    Case "P"
            '        lblBabitType.Text = "Pameran"
            '    Case "I"
            '        lblBabitType.Text = "Iklan"
            '    Case Else
            '        lblBabitType.Text = "Other"
            'End Select

            lblPeriodeStart.Text = oData.PeriodStart
            lblPeriodeEnd.Text = oData.PeriodEnd
            lnkbtnDelete.Visible = deletePriv
            lnkbtnEdit.Visible = editPriv

            Dim sData As BabitHeader = New BabitHeaderFacade(User).Retrieve(CInt(oData.ID))
            If sData.BabitStatus <> 0 AndAlso deletePriv Then
                lnkbtnDelete.Visible = False
            End If

            Select Case sData.BabitStatus
                Case 0
                    lblStatus.Text = "Baru"
                    If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        lnkbtnEdit.Visible = False
                    End If
                Case 1
                    lblStatus.Text = "Validasi"
                    lnkbtnEdit.Visible = False
                Case 2
                    lblStatus.Text = "Konfirmasi"
                    If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        lnkbtnEdit.Visible = True
                    Else
                        lnkbtnEdit.Visible = False
                    End If
                Case 3
                    lblStatus.Text = "Revisi"
                    If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        lnkbtnEdit.Visible = False
                    End If
                Case 4
                    lblStatus.Text = "Proses"
                    lnkbtnEdit.Visible = False
                Case 5
                    lblStatus.Text = "Setuju"
                    lnkbtnEdit.Visible = False
                Case 6
                    lblStatus.Text = "Tidak Setuju"
                    lnkbtnEdit.Visible = False
                Case 7
                    lblStatus.Text = "Proses Groupware"
                    lnkbtnEdit.Visible = False
            End Select
            lblStatus.Text = CommonFunction.GetEnumDescription(sData.BabitStatus, "EnumBabit.BabitStatus")

            Dim Biaya As Integer = 0
            Select Case oData.BabitMasterEventType.FormType
                Case 1
                    Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(BabitEventDetail), "BabitHeader.ID", MatchType.Exact, oData.ID))
                    Dim eventArr As ArrayList = New BabitEventDetailFacade(User).Retrieve(crits)
                    For Each bed As BabitEventDetail In eventArr
                        Biaya += CInt(bed.Price * bed.Qty)
                    Next
                Case 2
                    Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranExpense), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(BabitPameranExpense), "BabitHeader.ID", MatchType.Exact, oData.ID))
                    Dim arrBPE As ArrayList = New BabitPameranExpenseFacade(User).Retrieve(crits)
                    For Each bpe As BabitPameranExpense In arrBPE
                        Biaya += CInt(bpe.Price * bpe.Qty)
                    Next
                Case 3
                    Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(BabitIklanDetail), "BabitHeader.ID", MatchType.Exact, oData.ID))
                    Dim iklanArr As ArrayList = New BabitIklanDetailFacade(User).Retrieve(crits)
                    For Each bid As BabitIklanDetail In iklanArr
                        Biaya += CInt(bid.SubmissionAmount)
                    Next

            End Select
            lblPengajuanBiaya.Text = Format(Biaya, "###,###")

            Dim dblSubsidyAmount As Double = 0
            Dim arrBDA As ArrayList = New BabitDealerAllocationFacade(User).RetrieveByBabitHeader(oData.ID)
            If Not IsNothing(arrBDA) AndAlso arrBDA.Count > 0 Then
                For Each obj As BabitDealerAllocation In arrBDA
                    dblSubsidyAmount += obj.SubsidyAmount
                Next
            End If
            lblSubsidyAmount.Text = dblSubsidyAmount.ToString("#,##0")

            Dim critsAppDoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitApprovalDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critsAppDoc.opAnd(New Criteria(GetType(BabitApprovalDocument), "BabitHeader.ID", MatchType.Exact, oData.ID))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(BabitApprovalDocument), "ID", Sort.SortDirection.DESC))
            Dim _result As ArrayList = New BabitApprovalDocumentFacade(User).Retrieve(critsAppDoc, sortColl)
            If _result.Count > 0 Then
                lnkbtnDownload.Visible = True
                lnkbtnDownload.CommandArgument = CType(_result(0), BabitApprovalDocument).Path
            Else
                lnkbtnDownload.Visible = False
            End If
        End If
    End Sub

    Protected Sub hdnDealer_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealer.ValueChanged
        Dim data As String() = hdnDealer.Value.Trim.Split(";")
        txtKodeDealer.Text = data(0)
        lblKodeDealer.Text = data(0)
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
        Dim crits As CriteriaComposite
        If dgListBabit.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelper.GetSession("criteriadownload")) Then
            crits = CType(sessHelper.GetSession("criteriadownload"), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New BabitHeaderFacade(User).Retrieve(crits)
        If arrData.Count > 0 Then
            CreateExcel("DaftarBabit", arrData)
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
            ws.Cells("D3").Value = "Nama Dealer"
            ws.Cells("E3").Value = "Kode Cabang"
            ws.Cells("F3").Value = "Nama Cabang"
            ws.Cells("G3").Value = "No. Reg Babit"
            ws.Cells("H3").Value = "No. Surat"
            ws.Cells("I3").Value = "Tipe Babit"
            ws.Cells("J3").Value = "Tgl Periode Mulai"
            ws.Cells("K3").Value = "Tgl Periode Selesai"   'added by Benny 20190401
            ws.Cells("L3").Value = "Provinsi"
            ws.Cells("M3").Value = "Kota"
            ws.Cells("N3").Value = "Lokasi Kegiatan"
            ws.Cells("O3").Value = "Target SPK"
            ws.Cells("P3").Value = "Jumlah Pengajuan Biaya"
            ws.Cells("Q3").Value = "Kategori Babit"
            ws.Cells("R3").Value = "Jumlah Subsidi"
            ws.Cells("S3").Value = "Waktu Pengajuan Dealer"
            Dim standardCodeList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("EnumBabit.BabitStatus").Cast(Of  _
                                                StandardCode).ToList()

            Dim idx As Integer = 0
            For i As Integer = 0 To Data.Count - 1
                Dim item As BabitHeader = Data(i)
                Dim arrBDA As ArrayList = New BabitDealerAllocationFacade(User).RetrieveByBabitHeader(item.ID)
                If arrBDA.Count > 0 Then
                    For j As Integer = 0 To arrBDA.Count - 1
                        ws.Cells(idx + 4, 1).Value = idx + 1
                        'ws.Cells(idx + 4, 2).Value = IIf(item.BabitStatus = 0, "Baru", IIf(item.BabitStatus = 1, "Batal", "Konfirmasi"))
                        Try
                            ws.Cells(idx + 4, 2).Value = standardCodeList.FirstOrDefault(Function(x) x.ValueId = item.BabitStatus).ValueDesc
                        Catch
                        End Try

                        If IsNothing(item.Dealer) Then
                            ws.Cells(idx + 4, 3).Value = ""
                            ws.Cells(idx + 4, 4).Value = ""
                        Else
                            ws.Cells(idx + 4, 3).Value = item.Dealer.DealerCode
                            ws.Cells(idx + 4, 4).Value = item.Dealer.DealerName
                        End If
                        'ws.Cells(idx + 4, 3).Value = item.Dealer.DealerCode
                        If IsNothing(item.DealerBranch) Then
                            ws.Cells(idx + 4, 5).Value = ""
                            ws.Cells(idx + 4, 6).Value = ""
                        Else
                            ws.Cells(idx + 4, 5).Value = item.DealerBranch.DealerBranchCode
                            ws.Cells(idx + 4, 6).Value = item.DealerBranch.Name
                        End If
                        'ws.Cells(idx + 4, 4).Value = IIf(IsNothing(item.DealerBranch), "", item.DealerBranch.DealerBranchCode)
                        ws.Cells(idx + 4, 7).Value = item.BabitRegNumber
                        ws.Cells(idx + 4, 8).Value = item.BabitDealerNumber
                        'ws.Cells(idx + 4, 7).Value = IIf(item.BabitType.Trim = "E", "Event", IIf(item.BabitType.Trim = "P", "Pameran", IIf(item.BabitType.Trim = "I", "Iklan", "Other")))
                        ws.Cells(idx + 4, 9).Value = item.BabitMasterEventType.TypeName
                        ws.Cells(idx + 4, 10).Value = item.PeriodStart
                        ws.Column(10).Style.Numberformat.Format = "DD/MM/YY"
                        ws.Cells(idx + 4, 11).Value = item.PeriodEnd  'added by Benny 20190401
                        ws.Column(11).Style.Numberformat.Format = "DD/MM/YY"
                        Dim Biaya As Decimal = 0
                        Select Case item.BabitMasterEventType.FormType
                            Case 1
                                Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                crits.opAnd(New Criteria(GetType(BabitEventDetail), "BabitHeader.ID", MatchType.Exact, item.ID))
                                Dim eventArr As ArrayList = New BabitEventDetailFacade(User).Retrieve(crits)
                                For Each bed As BabitEventDetail In eventArr
                                    Biaya += CDec(bed.Price * bed.Qty)
                                Next
                            Case 2
                                Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranExpense), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                crits.opAnd(New Criteria(GetType(BabitPameranExpense), "BabitHeader.ID", MatchType.Exact, item.ID))
                                Dim arrBPE As ArrayList = New BabitPameranExpenseFacade(User).Retrieve(crits)
                                For Each bpe As BabitPameranExpense In arrBPE
                                    Biaya += CDec(bpe.Price * bpe.Qty)
                                Next
                            Case 3
                                Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                crits.opAnd(New Criteria(GetType(BabitIklanDetail), "BabitHeader.ID", MatchType.Exact, item.ID))
                                Dim iklanArr As ArrayList = New BabitIklanDetailFacade(User).Retrieve(crits)
                                For Each bid As BabitIklanDetail In iklanArr
                                    Biaya += CDec(bid.SubmissionAmount)
                                Next

                        End Select
                        If Not IsNothing(item.Dealer) Then
                            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                                If item.Dealer.Province.ProvinceName <> String.Empty Or item.Dealer.Province.ProvinceName <> "" Then
                                    ws.Cells(idx + 4, 12).Value = item.Dealer.Province.ProvinceName
                                End If
                                If item.Dealer.City.CityName <> String.Empty Or item.Dealer.City.CityName <> "" Then
                                    ws.Cells(idx + 4, 13).Value = item.Dealer.City.CityName
                                End If

                            Else
                                If oDealer.Province.ProvinceName <> String.Empty Or oDealer.Province.ProvinceName <> "" Then
                                    ws.Cells(idx + 4, 12).Value = oDealer.Province.ProvinceName
                                End If
                                If oDealer.City.CityName <> String.Empty Or oDealer.City.CityName <> "" Then
                                    ws.Cells(idx + 4, 13).Value = oDealer.City.CityName
                                End If
                            End If
                        Else
                            ws.Cells(idx + 4, 12).Value = ""
                            ws.Cells(idx + 4, 13).Value = ""
                        End If

                        ws.Cells(idx + 4, 14).Value = item.Location
                        ws.Cells(idx + 4, 15).Value = item.ProspectTarget
                        ws.Cells(idx + 4, 16).Value = Biaya

                        Dim Subsidi As Integer = 0
                        Dim bda As BabitDealerAllocation = CType(arrBDA(j), BabitDealerAllocation)
                        'For Each bda As BabitDealerAllocation In arrBDA
                        '    Subsidi = Subsid + bda.SubsidyAmount
                        'Next
                        'If arrBDA.Count > 0 Then

                        'End If
                        ws.Cells(idx + 4, 17).Value = CType(arrBDA(j), BabitDealerAllocation).BabitCategory
                        ws.Cells(idx + 4, 18).Value = bda.SubsidyAmount.ToString()
                        ws.Cells(idx + 4, 19).Value = item.CreatedTime
                        ws.Column(19).Style.Numberformat.Format = "DD/MM/YY"
                        idx = idx + 1
                    Next

                Else
                    ws.Cells(idx + 4, 1).Value = idx + 1
                    Try
                        ws.Cells(idx + 4, 2).Value = standardCodeList.FirstOrDefault(Function(x) x.ValueId = item.BabitStatus).ValueDesc
                    Catch
                    End Try
                    If IsNothing(item.Dealer) Then
                        ws.Cells(idx + 4, 3).Value = ""
                        ws.Cells(idx + 4, 4).Value = ""
                    Else
                        ws.Cells(idx + 4, 3).Value = item.Dealer.DealerCode
                        ws.Cells(idx + 4, 4).Value = item.Dealer.DealerName
                    End If
                    'ws.Cells(idx + 4, 3).Value = item.Dealer.DealerCode
                    If IsNothing(item.DealerBranch) Then
                        ws.Cells(idx + 4, 5).Value = ""
                        ws.Cells(idx + 4, 6).Value = ""
                    Else
                        ws.Cells(idx + 4, 5).Value = item.DealerBranch.DealerBranchCode
                        ws.Cells(idx + 4, 6).Value = item.DealerBranch.Name
                    End If
                    'ws.Cells(idx + 4, 4).Value = IIf(IsNothing(item.DealerBranch), "", item.DealerBranch.DealerBranchCode)
                    ws.Cells(idx + 4, 7).Value = item.BabitRegNumber
                    ws.Cells(idx + 4, 8).Value = item.BabitDealerNumber
                    'ws.Cells(idx + 4, 7).Value = IIf(item.BabitType.Trim = "E", "Event", IIf(item.BabitType.Trim = "P", "Pameran", IIf(item.BabitType.Trim = "I", "Iklan", "Other")))
                    ws.Cells(idx + 4, 9).Value = item.BabitMasterEventType.TypeName
                    ws.Cells(idx + 4, 10).Value = item.PeriodStart
                    ws.Column(10).Style.Numberformat.Format = "DD/MM/YY"
                    ws.Cells(idx + 4, 11).Value = item.PeriodEnd  'added by Benny 20190401
                    ws.Column(11).Style.Numberformat.Format = "DD/MM/YY"
                    Dim Biaya As Decimal = 0
                    Select Case item.BabitMasterEventType.FormType
                        Case 1
                            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crits.opAnd(New Criteria(GetType(BabitEventDetail), "BabitHeader.ID", MatchType.Exact, item.ID))
                            Dim eventArr As ArrayList = New BabitEventDetailFacade(User).Retrieve(crits)
                            For Each bed As BabitEventDetail In eventArr
                                Biaya += CDec(bed.Price * bed.Qty)
                            Next
                        Case 2
                            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitPameranExpense), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crits.opAnd(New Criteria(GetType(BabitPameranExpense), "BabitHeader.ID", MatchType.Exact, item.ID))
                            Dim arrBPE As ArrayList = New BabitPameranExpenseFacade(User).Retrieve(crits)
                            For Each bpe As BabitPameranExpense In arrBPE
                                Biaya += CDec(bpe.Price * bpe.Qty)
                            Next
                        Case 3
                            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crits.opAnd(New Criteria(GetType(BabitIklanDetail), "BabitHeader.ID", MatchType.Exact, item.ID))
                            Dim iklanArr As ArrayList = New BabitIklanDetailFacade(User).Retrieve(crits)
                            For Each bid As BabitIklanDetail In iklanArr
                                Biaya += CDec(bid.SubmissionAmount)
                            Next

                    End Select
                    If Not IsNothing(item.Dealer) Then
                        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                            If item.Dealer.Province.ProvinceName <> String.Empty Or item.Dealer.Province.ProvinceName <> "" Then
                                ws.Cells(idx + 4, 12).Value = item.Dealer.Province.ProvinceName
                            End If
                            If item.Dealer.City.CityName <> String.Empty Or item.Dealer.City.CityName <> "" Then
                                ws.Cells(idx + 4, 13).Value = item.Dealer.City.CityName
                            End If

                        Else
                            If oDealer.Province.ProvinceName <> String.Empty Or oDealer.Province.ProvinceName <> "" Then
                                ws.Cells(idx + 4, 12).Value = oDealer.Province.ProvinceName
                            End If
                            If oDealer.City.CityName <> String.Empty Or oDealer.City.CityName <> "" Then
                                ws.Cells(idx + 4, 13).Value = oDealer.City.CityName
                            End If
                        End If
                    Else
                        ws.Cells(idx + 4, 12).Value = ""
                        ws.Cells(idx + 4, 13).Value = ""
                    End If

                    ws.Cells(idx + 4, 14).Value = item.Location
                    ws.Cells(idx + 4, 15).Value = item.ProspectTarget
                    ws.Cells(idx + 4, 16).Value = Biaya

                    Dim Subsidi As Integer = 0
                    'Dim bda As BabitDealerAllocation = CType(arrBDA(j), BabitDealerAllocation)
                    'For Each bda As BabitDealerAllocation In arrBDA
                    '    Subsidi = Subsid + bda.SubsidyAmount
                    'Next
                    'If arrBDA.Count > 0 Then

                    'End If
                    ws.Cells(idx + 4, 17).Value = "" 'CType(arrBDA(j), BabitDealerAllocation).BabitCategory
                    ws.Cells(idx + 4, 18).Value = "" 'bda.SubsidyAmount.ToString()
                    ws.Cells(idx + 4, 19).Value = item.CreatedTime
                    ws.Column(19).Style.Numberformat.Format = "DD/MM/YY"
                    idx = idx + 1
                End If

                
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

    Private Function SearchCriteria() As CriteriaComposite
        Dim strSql As String = String.Empty
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If cbDate.Checked Then
            Dim tglFrom As New Date(icPeriodeStart.Value.Year, icPeriodeStart.Value.Month, icPeriodeStart.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icPeriodeEnd.Value.Year, icPeriodeEnd.Value.Month, icPeriodeEnd.Value.Day, 23, 59, 59)

            crit.opAnd(New Criteria(GetType(BabitHeader), "PeriodStart", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "((", True)
            crit.opAnd(New Criteria(GetType(BabitHeader), "PeriodStart", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), ")", False)

            crit.opOr(New Criteria(GetType(BabitHeader), "PeriodEnd", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "(", True)
            crit.opAnd(New Criteria(GetType(BabitHeader), "PeriodEnd", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), "))", False)
        End If

        If txtNomorSurat.Text <> "" Then
            crit.opAnd(New Criteria(GetType(BabitHeader), "BabitDealerNumber", MatchType.Partial, txtNomorSurat.Text))
        End If

        If txtKodeDealer.Text <> "" Then
            crit.opAnd(New Criteria(GetType(BabitHeader), "Dealer.DealerCode", MatchType.Partial, txtKodeDealer.Text))
        End If

        If Not oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            crit.opAnd(New Criteria(GetType(BabitHeader), "Dealer.DealerCode", MatchType.Partial, lblKodeDealer.Text.Trim.Split("/")(0).Trim))
        End If

        If txtKodeTempOut.Text <> "" Then
            crit.opAnd(New Criteria(GetType(BabitHeader), "DealerBranch.DealerBranchCode", MatchType.Partial, txtKodeTempOut.Text))
        End If
        If ddlBabitType.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(BabitHeader), "BabitMasterEventType.ID", MatchType.Exact, ddlBabitType.SelectedValue))
        End If
        If ddlCategoryAlloc.SelectedIndex <> 0 Then
            strSql = "(SELECT BabitHeaderID FROM BabitDealerAllocation WHERE RowStatus = 0 and BabitCategory ='" & ddlCategoryAlloc.SelectedValue & "')"
            crit.opAnd(New Criteria(GetType(BabitHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If txtBabitRegNo.Text <> "" Then
            crit.opAnd(New Criteria(GetType(BabitHeader), "BabitRegNumber", MatchType.Partial, txtBabitRegNo.Text))
        End If
        If lsStatus.SelectedValue <> "" Then
            crit.opAnd(New Criteria(GetType(BabitHeader), "BabitStatus", MatchType.Exact, lsStatus.SelectedValue))
        End If
        If ddlAllocStatus.SelectedIndex <> 0 Then
            If ddlAllocStatus.SelectedValue = "1" Then
                strSql = "(SELECT BabitHeaderID FROM BabitDealerAllocation WHERE RowStatus = 0 AND BabitCategory <> '-1')"
                crit.opAnd(New Criteria(GetType(BabitHeader), "ID", MatchType.NotInSet, strSql))
            ElseIf ddlAllocStatus.SelectedValue = "2" Then
                strSql = "(SELECT BabitHeaderID FROM BabitDealerAllocation WHERE RowStatus = 0 AND BabitCategory <> '-1')"
                crit.opAnd(New Criteria(GetType(BabitHeader), "ID", MatchType.InSet, strSql))
            End If
        End If

        sessHelper.SetSession("criteriadownload", crit)
        Return crit
    End Function

    Protected Sub dgListBabit_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgListBabit.PageIndexChanged
        dgListBabit.CurrentPageIndex = e.NewPageIndex
        ViewState("qweqweq") = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Protected Sub dgListBabit_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgListBabit.SortCommand
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
        dgListBabit.CurrentPageIndex = 0
        SearchCriteria()
        BindGrid(dgListBabit.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        Dim arlBH As New ArrayList
        Dim ok As Boolean = True
        For Each dgItem As DataGridItem In dgListBabit.Items
            If CType(dgItem.FindControl("chkSelect"), CheckBox).Checked Then
                Dim objPOH As BabitHeader = New BabitHeaderFacade(User).Retrieve(CType(dgItem.Cells(0).Text, Integer))

                Select Case ddlAction.SelectedValue
                    Case 0, 2 'Batal Validasi
                        If objPOH.BabitStatus <> 1 Then
                            ok = False
                        Else
                            Continue For
                        End If
                        'MessageBox.Show("Status Batal Validasi hanya untuk data berstatus Validasi")
                        'MessageBox.Show("Status Konfirmasi hanya untuk data berstatus Validasi ")
                    Case 1 'Validasi
                        If objPOH.BabitStatus <> 0 AndAlso objPOH.BabitStatus <> 3 Then
                            ok = False
                        Else
                            Continue For
                        End If
                        'MessageBox.Show("Status Validasi hanya untuk data berstatus Baru dan Revisi")
                    Case 3 'Revisi
                        If objPOH.BabitStatus <> 2 Then
                            ok = False
                        Else
                            Continue For
                        End If
                        'MessageBox.Show("Status Revisi hanya untuk data berstatus Konfirmasi")
                    Case Else
                        ok = False
                End Select
            End If
        Next

        If ok Then
            For Each dgItem As DataGridItem In dgListBabit.Items
                If CType(dgItem.FindControl("chkSelect"), CheckBox).Checked Then
                    Dim objPOH As BabitHeader = New BabitHeaderFacade(User).Retrieve(CType(dgItem.Cells(0).Text, Integer))
                    arlBH.Add(objPOH)
                End If
            Next
        Else
            Select Case ddlAction.SelectedValue
                Case 0 'Batal Validasi
                    MessageBox.Show("Status Batal Validasi hanya untuk data berstatus Validasi")
                Case 1 'Validasi
                    MessageBox.Show("Status Validasi hanya untuk data berstatus Baru dan Revisi")
                Case 2 'Konfirmasi
                    MessageBox.Show("Status Konfirmasi hanya untuk data berstatus Validasi ")
                Case 3 'Revisi
                    MessageBox.Show("Status Revisi hanya untuk data berstatus Konfirmasi")
                Case Else       'Silahkan Pilih
                    MessageBox.Show("Pilih status terlebih darhulu")
            End Select
            Exit Sub
        End If

        If arlBH.Count > 0 Then
            Dim sb As StringBuilder = New StringBuilder
            Dim intGagal As Short = 0
            For Each bh As BabitHeader In arlBH
                bh.BabitStatus = ddlAction.SelectedValue
                Dim _return As Integer = New BabitHeaderFacade(User).Update(bh)
                If _return = 0 Then
                    intGagal += 1
                    sb.Append("- Update Status untuk No. Reg Babit : " & bh.BabitRegNumber & " = Gagal\n")
                Else
                    sb.Append("- Update Status untuk No. Reg Babit : " & bh.BabitRegNumber & " = Sukses\n")
                End If
            Next
            If (sb.ToString().Length > 0) AndAlso intGagal > 0 Then
                MessageBox.Show(sb.ToString())
            Else
                MessageBox.Show("Update Status Sukses")
            End If
        End If
        BindGrid(ViewState("qweqweq"))
    End Sub

    Protected Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click, btnTransferUlang.Click
        Dim errorMessage As StringBuilder = New StringBuilder
        Dim arrCheckedHeader As New ArrayList
        For Each dgItem As DataGridItem In dgListBabit.Items
            If CType(dgItem.FindControl("chkSelect"), CheckBox).Checked Then
                Dim oData As BabitHeader = New BabitHeaderFacade(User).Retrieve(CType(dgItem.Cells(0).Text, Integer))
                Dim arrBDA As ArrayList = New BabitDealerAllocationFacade(User).RetrieveByBabitHeader(oData.ID)
                If Not IsNothing(arrBDA) AndAlso arrBDA.Count > 0 Then
                    Dim intCountErr As Integer = 0
                    Dim objBabitDealerAllocation As BabitDealerAllocation = CType(arrBDA(0), BabitDealerAllocation)
                    If objBabitDealerAllocation.BabitHeader.BabitStatus < 2 OrElse objBabitDealerAllocation.BabitHeader.BabitStatus > 3 Then
                        intCountErr += 1
                        errorMessage.Append("Nomor Reg Babit " & oData.BabitRegNumber & " statusnya bukan Konfirmasi atau Revisi\n")
                    End If
                    For Each obj As BabitDealerAllocation In arrBDA
                        If obj.BabitCategory = "-1" Then
                            intCountErr += 1
                            errorMessage.Append("Tipe Alokasi Babit " & oData.BabitRegNumber & " masih kosong untuk Kategori : " & obj.BabitCategory & "\n")
                            Exit For
                        ElseIf obj.SubsidyAmount = 0 Then
                            intCountErr += 1
                            errorMessage.Append("Jumlah Alokasi Subsidi Babit " & oData.BabitRegNumber & " masih kosong untuk Kategori : " & obj.BabitCategory & "\n")
                        End If
                    Next
                    If intCountErr = 0 Then
                        arrCheckedHeader.Add(oData)
                    End If
                Else
                    errorMessage.Append("Tidak ada data alokasi untuk Babit " & oData.BabitRegNumber & "\n")
                End If
                'objPOH.isTransfer = 1
                'oData.BabitStatus = 4
                'Dim _return As Integer = New BabitHeaderFacade(User).Update(oData)
            End If
        Next
        If errorMessage.Length = 0 Then
            Dim _return As Integer = New BabitHeaderFacade(User).UpdateTransaction(arrCheckedHeader)
            If _return = 1 Then
                'MessageBox.Show("Data berhasil ditransfer ke Groupware")
                'Response.Redirect(Request.RawUrl)
                Response.Redirect("FrmBabitList.aspx?Message=Data berhasil ditransfer ke Groupware")
            Else
                MessageBox.Show("Data gagal ditransfer ke Groupware")
            End If
        Else
            MessageBox.Show(errorMessage.ToString())
        End If
    End Sub

    Private Function GetCheckedItemAllPages() As ArrayList
        Dim arlCheckedItemAllPages As ArrayList = New ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession("FrmBabitList"), ArrayList)
        Dim intPageCount As Integer = 0
        If Not IsNothing(ViewState.Item("GridPageCount")) Then
            intPageCount = CType(ViewState.Item("GridPageCount"), Integer)
        End If
        If Not IsNothing(Request.QueryString("GridPageCount")) AndAlso Not (Request.QueryString("GridPageCount") = "") Then
            intPageCount = CType(Request.QueryString("GridPageCount"), Integer)
            ViewState.Add("GridPageCount", intPageCount)
        End If
        Dim nGridCount As Integer = intPageCount - 1
        For idx As Integer = 0 To nGridCount
            Dim currentPage As String = CType(idx, String)
            Dim arrGrid2 As ArrayList = CType(sessHelper.GetSession("babitSessProcess" + currentPage), ArrayList)
            If Not IsNothing(arrGrid2) Then
                For i As Integer = 0 To arrGrid2.Count - 1
                    arlCheckedItemAllPages.Add(arrGrid2(i))
                Next i
            End If
        Next
        Return arlCheckedItemAllPages

    End Function

    Private Sub SetCheckedItemAllPages()
        Dim TotRow As Integer = 0
        Dim srtColumn As String = ""
        Dim srtDirection As Sort.SortDirection
        srtColumn = ViewState.Item("SortColumn")
        srtDirection = ViewState.Item("SortDirection")

        Dim objVRDFFac As BabitHeaderFacade = New BabitHeaderFacade(User)
        Dim arlPerPages As ArrayList = New ArrayList

        Dim arlCheckedItemAllPages As ArrayList = New ArrayList
        Dim nPageGridCount As Integer = dgListBabit.PageCount - 1
        For idx As Integer = 0 To nPageGridCount
            Dim currentPage As String = CType(idx, String)
            arlPerPages = objVRDFFac.RetrieveActiveList(idx + 1, dgListBabit.PageSize, TotRow, srtColumn, srtDirection, sessHelper.GetSession("criteriadownload"))
            sessHelper.SetSession("babitSessProcess" + currentPage, arlPerPages)
            sessHelper.SetSession("babitSessProcess2" + currentPage, New ArrayList)
        Next

        '-- Check all checkbox at current page
        For Each dtgItem As DataGridItem In dgListBabit.Items
            Dim chkItem As CheckBox = CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox)
            chkItem.Checked = True
        Next

    End Sub

    Private Sub ClearCheckedItemAllPages()
        Dim arlPerPages As ArrayList = New ArrayList
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(v_RetrieveDummyFaktur), ViewState.Item("SortColumn"), ViewState.Item("SortDirection")))
        Dim iColl As ICollection = CType(sortCol, SortCollection)

        Dim objVRDFFac As BabitHeaderFacade = New BabitHeaderFacade(User)
        Dim arlVRDF As ArrayList = New ArrayList
        arlVRDF = objVRDFFac.Retrieve(sessHelper.GetSession("criteriadownload"), iColl)

        Dim arlCheckedItemAllPages As ArrayList = New ArrayList
        Dim nPageGridCount As Integer = dgListBabit.PageCount - 1
        For idx As Integer = 0 To nPageGridCount
            Dim currentPage As String = CType(idx, String)
            sessHelper.SetSession("babitSessProcess" + currentPage, arlPerPages)
            sessHelper.SetSession("babitSessProcess2" + currentPage, New ArrayList)
        Next

        '-- UnCheck all checkbox at current page
        For Each dtgItem As DataGridItem In dgListBabit.Items
            Dim chkItem As CheckBox = CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox)
            chkItem.Checked = False
        Next
    End Sub

    Private Function GetCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession("FrmBabitList"), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgListBabit.Items
            nIndeks = dtgItem.ItemIndex
            Dim objCM As BabitHeader = CType(arrGrid(nIndeks), BabitHeader)
            If CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                arlCheckedItem.Add(objCM)
            End If
        Next

        Return arlCheckedItem
    End Function

    Private Function GetUnCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession("FrmBabitList"), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgListBabit.Items
            nIndeks = dtgItem.ItemIndex
            Dim objCM As BabitHeader = CType(arrGrid(nIndeks), BabitHeader)
            If Not CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                arlCheckedItem.Add(objCM)
            End If
        Next

        Return arlCheckedItem
    End Function
End Class