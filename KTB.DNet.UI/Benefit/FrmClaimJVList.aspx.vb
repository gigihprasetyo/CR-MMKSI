Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessValidation.Helpers

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports OfficeOpenXml
Imports System.Linq

Public Class FrmClaimJVList
    Inherits System.Web.UI.Page

    Private oDealer As Dealer
    Private sessHelper As New SessionHelper
    Private arlCheckedItemColl As ArrayList = New ArrayList
    Private SessionCriteriaBabit = "FrmClaimJVList.CriteriaList"
    Private PageSession = "FrmClaimJVList"
    Private GridIndexSessionBefore = "FrmClaimJVList_PgIdxBefore"
    Private GridIndexSessionNext = "FrmClaimJVList_PgIdxNext"

    Private listProcessSession = "sessClaimJVListProcess"
    Private listProcessSession2 = "sessClaimJVListProcess2"
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private transferSAPPriv As Boolean
    Private arlBenefitClaimReceipt As ArrayList = New ArrayList
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Authorization()

        If Not IsPostBack Then
            PageInit()
            ReadCriteria()
            BindGrid(dgListJV.CurrentPageIndex)
        End If
        lnkBtnPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
    End Sub

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.Sales_Campaign_Daftar_Pencairan_Claim_Lihat_privillage) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - DAFTAR PENCAIRAN CLAIM")
        Else
            displayPriv = SecurityProvider.Authorize(Context.User, SR.Sales_Campaign_Daftar_Pencairan_Claim_Detail_privillage)
            editPriv = SecurityProvider.Authorize(Context.User, SR.Sales_Campaign_Daftar_Pencairan_Claim_Edit_privillage)
            transferSAPPriv = SecurityProvider.Authorize(Context.User, SR.Sales_Campaign_Daftar_Pencairan_Claim_Transfer_SAP_privillage)
        End If

        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtKodeDealer.Visible = True
            lblKodeDealer.Visible = False
            lnkBtnPopUpDealer.Visible = True
            btnTransfer.Visible = transferSAPPriv
        Else
            txtKodeDealer.Text = oDealer.DealerCode
            lblKodeDealer.Text = oDealer.DealerCode & " / " & oDealer.DealerName

            txtKodeDealer.Visible = False
            lblKodeDealer.Visible = True
            lnkBtnPopUpDealer.Visible = False
            btnTransfer.Visible = False
        End If
    End Sub

    Private Sub PageInit()
        bindDDLs()
        ViewState("currSortColumn") = "ID"
        ViewState("currSortDirection") = Sort.SortDirection.DESC
        dgListJV.CurrentPageIndex = 0
    End Sub

    Private Sub bindDDLs()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "BenefitClaimReceiptStatus"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        With ddlStatus
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
        End With
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Piih ", -1))
        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim total As Integer = 0
        Dim crit As CriteriaComposite
        If Not IsNothing(ViewState("Back")) Then
            ReadCriteria()
            crit = SearchCriteria()
            ViewState.Remove("Back")
        Else
            crit = SearchCriteria()
        End If

        If Not IsNothing(CType(sessHelper.GetSession(PageSession), ArrayList)) Then
            sessHelper.SetSession(GridIndexSessionBefore, CType(sessHelper.GetSession(GridIndexSessionNext), Integer))
            sessHelper.SetSession(GridIndexSessionNext, pageIndex)

            arlCheckedItemColl = GetCheckedItem()
            Dim currentPage As String = CType(sessHelper.GetSession(GridIndexSessionBefore), String)
            sessHelper.SetSession(listProcessSession + currentPage, arlCheckedItemColl)

            Dim arlUnCheckedItemColl As ArrayList = GetUnCheckedItem()
            currentPage = CType(sessHelper.GetSession(GridIndexSessionBefore), String)
            sessHelper.SetSession(listProcessSession2 + currentPage, arlUnCheckedItemColl)
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(BenefitClaimReceipt), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        Dim listSource As ArrayList = New BenefitClaimReceiptFacade(User).Retrieve(crit, sortColl)
        If listSource.Count <> 0 Then
            CommonFunction.SortListControl(listSource, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(listSource, pageIndex, dgListJV.PageSize)
            sessHelper.SetSession(PageSession, PagedList)
            dgListJV.DataSource = PagedList
            dgListJV.VirtualItemCount = listSource.Count
            dgListJV.DataBind()
        Else
            dgListJV.DataSource = New ArrayList
            dgListJV.VirtualItemCount = 0
            dgListJV.CurrentPageIndex = 0
            dgListJV.DataBind()
        End If
    End Sub

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sessHelper.GetSession(SessionCriteriaBabit), Hashtable)
        If Not crit Is Nothing Then
            txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
            lblKodeDealer.Text = CStr(crit.Item("DealerCode"))
            txtNoReceipt.Text = CStr(crit.Item("NoReceipt"))
            txtNoJV.Text = CStr(crit.Item("NoJV"))
            txtNoReg.Text = CStr(crit.Item("NoReg"))
            ddlStatus.SelectedValue = CStr(crit.Item("ddlStatus"))
            cbDate.Checked = CBool(crit.Item("cbDate"))
            cbPaymentDate.Checked = CBool(crit.Item("cbPaymentDate"))
            icPeriodeStart.Value = CStr(crit.Item("PeriodStart"))
            icPeriodeEnd.Value = CStr(crit.Item("PeriodEnd"))
            icPaymentDateStart.Value = CStr(crit.Item("PaymentDateStart"))
            icPaymentDateEnd.Value = CStr(crit.Item("PaymentDateEnd"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgListJV.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Function SearchCriteria() As CriteriaComposite
        Dim strSql As String = String.Empty
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If txtKodeDealer.Text <> "" Then
                Dim strKodeDealerIn As String = String.Empty
                Dim str() As String = txtKodeDealer.Text.Trim().Split(";")
                For Each strCode As String In str
                    If strKodeDealerIn = String.Empty Then
                        strKodeDealerIn = "('" & strCode.Trim & "'"
                    Else
                        strKodeDealerIn += ",'" & strCode.Trim & "'"
                    End If
                Next
                strKodeDealerIn += ")"
                crit.opAnd(New Criteria(GetType(BenefitClaimReceipt), "BenefitClaimHeader.Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            End If
        Else
            crit.opAnd(New Criteria(GetType(BenefitClaimReceipt), "BenefitClaimHeader.Dealer.DealerCode", MatchType.Partial, lblKodeDealer.Text.Trim.Split("/")(0).Trim))
        End If

        If txtNoReceipt.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BenefitClaimReceipt), "ReceiptNo", MatchType.Exact, txtNoReceipt.Text.Trim))
        End If
        If txtNoJV.Text.Trim <> "" Then
            strSql = "SELECT a.ID FROM BenefitClaimHeader a "
            strSql += "Join BenefitClaimJV b on a.ID = b.BenefitClaimHeaderID "
            strSql += "Where a.RowStatus = 0 and b.RowStatus = 0 "
            strSql += "and b.TipeAccount in ('D', 'K') and b.JVNumber = '" & txtNoJV.Text.Trim & "'"
            crit.opAnd(New Criteria(GetType(BenefitClaimReceipt), "BenefitClaimHeader.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If txtNoReg.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(BenefitClaimReceipt), "BenefitClaimHeader.ClaimRegNo", MatchType.Partial, txtNoReg.Text.Trim))
        End If

        'TODO
        If ddlStatus.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(BenefitClaimReceipt), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If cbDate.Checked Then
            Dim tglFrom As New Date(icPeriodeStart.Value.Year, icPeriodeStart.Value.Month, icPeriodeStart.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icPeriodeEnd.Value.Year, icPeriodeEnd.Value.Month, icPeriodeEnd.Value.Day, 23, 59, 59)

            crit.opAnd(New Criteria(GetType(BenefitClaimReceipt), "ReceiptDate", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")))
            crit.opAnd(New Criteria(GetType(BenefitClaimReceipt), "ReceiptDate", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")))
        End If

        If cbPaymentDate.Checked Then
            Dim tglFrom As New Date(icPaymentDateStart.Value.Year, icPaymentDateStart.Value.Month, icPaymentDateStart.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icPaymentDateEnd.Value.Year, icPaymentDateEnd.Value.Month, icPaymentDateEnd.Value.Day, 23, 59, 59)

            strSql = "Select Distinct BenefitClaimHeaderID From BenefitClaimJV "
            strSql += "Where RowStatus = 0 "
            strSql += "and PaymentDate >= '" & Format(tglFrom, "yyyy-MM-dd HH:mm:ss") & "' "
            strSql += "and PaymentDate <= '" & Format(tglTo, "yyyy-MM-dd HH:mm:ss") & "' "
            crit.opAnd(New Criteria(GetType(BenefitClaimReceipt), "BenefitClaimHeader.ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        sessHelper.SetSession("criteriadownload", crit)
        Return crit
    End Function

    Private Function GetCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession(PageSession), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgListJV.Items
            nIndeks = dtgItem.ItemIndex
            Dim objBCR As BenefitClaimReceipt = CType(arrGrid(nIndeks), BenefitClaimReceipt)
            If CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                arlCheckedItem.Add(objBCR)
            End If
        Next

        Return arlCheckedItem
    End Function

    Private Function GetUnCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession(PageSession), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgListJV.Items
            nIndeks = dtgItem.ItemIndex
            Dim objCR As BenefitClaimReceipt = CType(arrGrid(nIndeks), BenefitClaimReceipt)
            If Not CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                arlCheckedItem.Add(objCR)
            End If
        Next

        Return arlCheckedItem
    End Function

    Protected Sub dgListJV_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgListJV.ItemDataBound
        Dim objBCR As BenefitClaimReceipt = New BenefitClaimReceipt
        Dim chkSelect As CheckBox = CType(e.Item.FindControl("chkSelect"), CheckBox)
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
        Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
        Dim lblClaimRegNo As Label = CType(e.Item.FindControl("lblClaimRegNo"), Label)
        Dim lblNoJV As Label = CType(e.Item.FindControl("lblNoJV"), Label)
        Dim lblNoReceive As LinkButton = CType(e.Item.FindControl("lblNoReceive"), LinkButton)
        Dim lnkJV As LinkButton = CType(e.Item.FindControl("lnkJV"), LinkButton)
        Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lnkbtnDetail"), LinkButton)
        Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
        Dim lblPaymentDate As Label = CType(e.Item.FindControl("lblPaymentDate"), Label)
        Dim lblActualPaymentDate As Label = CType(e.Item.FindControl("lblActualPaymentDate"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As BenefitClaimReceipt = CType(e.Item.DataItem, BenefitClaimReceipt)
            chkSelect.Checked = False
            Dim currentPage As String = CType(sessHelper.GetSession(GridIndexSessionNext), String)
            Dim arrGridDF As ArrayList = CType(sessHelper.GetSession(listProcessSession + currentPage), ArrayList)
            If IsNothing(arrGridDF) Then arrGridDF = New ArrayList
            For Each oDF As BenefitClaimReceipt In arrGridDF
                If objBCR.ID = oDF.ID Then
                    chkSelect.Checked = True
                    Exit For
                End If
            Next

            lblNo.Text = (dgListJV.PageSize * dgListJV.CurrentPageIndex) + e.Item.ItemIndex + 1
            If Not IsNothing(oData.BenefitClaimHeader.Dealer) Then
                lblDealerCode.Text = oData.BenefitClaimHeader.Dealer.DealerCode
                lblDealerName.Text = oData.BenefitClaimHeader.Dealer.SearchTerm1
            End If

            lblClaimRegNo.Text = oData.BenefitClaimHeader.ClaimRegNo

            Dim criterias As New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader.ID", MatchType.Exact, oData.BenefitClaimHeader.ID))
            Dim arrObj As ArrayList = New BenefitClaimJVFacade(User).Retrieve(criterias)
            Dim objBenefitClaimJV As New BenefitClaimJV
            If Not IsNothing(arrObj) AndAlso arrObj.Count > 0 Then
                objBenefitClaimJV = CType(arrObj(0), BenefitClaimJV)
                lblNoJV.Text = objBenefitClaimJV.JVNumber
            End If

            lblNoReceive.Text = oData.ReceiptNo

            lnkJV.Visible = False
            Select Case oData.BenefitClaimHeader.Status
                Case 2, 4, 5  '---2=Status Konfirmasi, 4=Status Proses, 5=Status Selesai untuk BenefitClaimHeader.Status
                    lnkJV.Visible = editPriv
                Case Else
            End Select
            lnkbtnDetail.Visible = displayPriv

            Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "BenefitClaimReceiptStatus"))
            criterias3.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, oData.Status))
            Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3)
            Dim objStandardCode As New StandardCode
            If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
                objStandardCode = CType(arrDDL(0), StandardCode)
                lblStatus.Text = objStandardCode.ValueDesc
            End If

            'jika loginnya dealer maka button JV is disable
            If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                lnkJV.Visible = False
            End If

            lblAmount.Text = 0
            Dim criterias4 As New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias4.opAnd(New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader.ID", MatchType.Exact, oData.BenefitClaimHeader.ID))
            criterias4.opAnd(New Criteria(GetType(BenefitClaimJV), "TipeAccount", MatchType.Exact, "D"))
            Dim arrlist As ArrayList = New BenefitClaimJVFacade(User).Retrieve(criterias4)
            If Not IsNothing(arrlist) AndAlso arrlist.Count > 0 Then
                Dim objJV As BenefitClaimJV = CType(arrlist(0), BenefitClaimJV)
                lblAmount.Text = objJV.Amount.ToString("#,##0")
                Dim strPaymentDate As String = String.Empty
                Dim strActualPaymentDate As String = String.Empty
                If objJV.PaymentDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                    strPaymentDate = objJV.PaymentDate.ToString("dd/MM/yyyy")
                End If
                If objJV.ActualPaymentDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                    strActualPaymentDate = objJV.ActualPaymentDate.ToString("dd/MM/yyyy")
                End If

                lblPaymentDate.Text = strPaymentDate
                lblActualPaymentDate.Text = strActualPaymentDate
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindGrid(dgListJV.CurrentPageIndex)
        StoreCriteria()
        If dgListJV.Items.Count = 0 Then
            MessageBox.Show("Data list tidak ditemukan")
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("DealerCode", txtKodeDealer.Text)
        crit.Add("NoReceipt", txtNoReceipt.Text)
        crit.Add("NoJV", txtNoJV.Text)
        crit.Add("NoReg", txtNoReg.Text)
        crit.Add("ddlStatus", ddlStatus.SelectedValue)
        crit.Add("cbDate", cbDate.Checked)
        crit.Add("cbPaymentDate", cbPaymentDate.Checked)
        crit.Add("PeriodStart", icPeriodeStart.Value)
        crit.Add("PeriodEnd", icPeriodeEnd.Value)
        crit.Add("PaymentDateStart", icPaymentDateStart.Value)
        crit.Add("PaymentDateEnd", icPaymentDateEnd.Value)

        crit.Add("PageIndex", dgListJV.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sessHelper.SetSession(SessionCriteriaBabit, crit)  '-- Store in session
    End Sub

    Protected Sub btnDownloadExcelJV_Click(sender As Object, e As EventArgs) Handles btnDownloadExcelJV.Click
        SetDownloadJV()
    End Sub

    Private Sub SetDownloadJV()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgListJV.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelper.GetSession("criteriadownload")) Then
            crits = CType(sessHelper.GetSession("criteriadownload"), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New BenefitClaimReceiptFacade(User).Retrieve(crits)
        If arrData.Count > 0 Then
            CreateExcelJV("Daftar_Actual_Accrual_JV", arrData)
        End If
    End Sub

    Private Sub CreateExcelJV(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName.Replace("_", " ")
            ws.Cells("A3").Value = "No."
            ws.Cells("B3").Value = "Kode Dealer"
            ws.Cells("C3").Value = "Term Cari 1"
            ws.Cells("D3").Value = "No. Claim"
            ws.Cells("E3").Value = "Account"
            ws.Cells("F3").Value = "Tipe Kendaraan"
            ws.Cells("G3").Value = "Month"
            ws.Cells("H3").Value = "Cost Center"
            ws.Cells("I3").Value = "Accrual"
            ws.Cells("J3").Value = "Amount"
            ws.Cells("K3").Value = "Keterangan"

            Dim i As Integer = 0
            For Each oBenefitClaimReceipt As BenefitClaimReceipt In Data
                Dim criterias4 As New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias4.opAnd(New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader.ID", MatchType.Exact, oBenefitClaimReceipt.BenefitClaimHeader.ID))
                criterias4.opAnd(New Criteria(GetType(BenefitClaimJV), "TipeAccount", MatchType.No, "D"))
                Dim arrBenefitClaimJV As ArrayList = New BenefitClaimJVFacade(User).Retrieve(criterias4)
                For Each objJV As BenefitClaimJV In arrBenefitClaimJV
                    ws.Cells(i + 4, 1).Value = i + 1
                    If IsNothing(oBenefitClaimReceipt.BenefitClaimHeader.Dealer) Then
                        ws.Cells(i + 4, 2).Value = ""
                        ws.Cells(i + 4, 3).Value = ""
                    Else
                        ws.Cells(i + 4, 2).Value = oBenefitClaimReceipt.BenefitClaimHeader.Dealer.DealerCode
                        ws.Cells(i + 4, 3).Value = oBenefitClaimReceipt.BenefitClaimHeader.Dealer.SearchTerm1
                    End If
                    ws.Cells(i + 4, 4).Value = oBenefitClaimReceipt.BenefitClaimHeader.ClaimRegNo

                    Dim strTipeAccount As String = String.Empty
                    Select Case objJV.TipeAccount
                        Case "A"
                            strTipeAccount = "Accrued"
                        Case "E"
                            strTipeAccount = "Expense"
                    End Select
                    ws.Cells(i + 4, 5).Value = strTipeAccount

                    Dim strVechileType As String = String.Empty
                    Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias3.opAnd(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "MasterCostCenter.CostCenterCode", MatchType.Exact, objJV.CostCenter))
                    Dim arrMasterCostCentertoSubCategoryVehicle As ArrayList = New MasterCostCentertoSubCategoryVehicleFacade(User).Retrieve(criterias3)
                    Dim objMasterCostCentertoSubCategoryVehicle As New MasterCostCentertoSubCategoryVehicle
                    If Not IsNothing(arrMasterCostCentertoSubCategoryVehicle) AndAlso arrMasterCostCentertoSubCategoryVehicle.Count > 0 Then
                        objMasterCostCentertoSubCategoryVehicle = CType(arrMasterCostCentertoSubCategoryVehicle(0), MasterCostCentertoSubCategoryVehicle)
                    End If
                    If Not IsNothing(objMasterCostCentertoSubCategoryVehicle.SubCategoryVehicle) Then
                        If objMasterCostCentertoSubCategoryVehicle.SubCategoryVehicle.ID > 0 Then
                            strVechileType = objMasterCostCentertoSubCategoryVehicle.SubCategoryVehicle.Name
                        End If
                    End If
                    ws.Cells(i + 4, 6).Value = strVechileType
                    ws.Cells(i + 4, 7).Value = getmonthDesc(objJV.Month)
                    ws.Cells(i + 4, 8).Value = objJV.CostCenter
                    ws.Cells(i + 4, 9).Value = If(Not IsNothing(objJV.MasterAccrued), objJV.MasterAccrued.AccKey, "")
                    ws.Cells(i + 4, 10).Value = objJV.Amount
                    ws.Cells(i + 4, 11).Value = objJV.Remarks

                    i += 1
                Next
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using

    End Sub

    Protected Sub btnDownloadExcel_Click(sender As Object, e As EventArgs) Handles btnDownloadExcel.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgListJV.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelper.GetSession("criteriadownload")) Then
            crits = CType(sessHelper.GetSession("criteriadownload"), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New BenefitClaimReceiptFacade(User).Retrieve(crits)
        If arrData.Count > 0 Then
            CreateExcel("Daftar_Pencairan_Claim", arrData)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName.Replace("_", " ")
            ws.Cells("A3").Value = "No."
            ws.Cells("B3").Value = "Kode Dealer"
            ws.Cells("C3").Value = "Term Cari 1"
            ws.Cells("D3").Value = "No. Claim"
            ws.Cells("E3").Value = "Tgl Rencana Pembayaran"
            ws.Cells("F3").Value = "Tgl Aktual Pembayaran"
            ws.Cells("G3").Value = "Status Pencairan"
            ws.Cells("H3").Value = "No. Kuitansi"
            ws.Cells("I3").Value = "Total Nilai Kuitansi"
            ws.Cells("J3").Value = "Total PPh"
            ws.Cells("K3").Value = "Total PPn"
            ws.Cells("L3").Value = "Amount Claim"

            For i As Integer = 0 To Data.Count - 1
                Dim oBenefitClaimReceipt As BenefitClaimReceipt = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                If IsNothing(oBenefitClaimReceipt.BenefitClaimHeader.Dealer) Then
                    ws.Cells(i + 4, 2).Value = ""
                    ws.Cells(i + 4, 3).Value = ""
                Else
                    ws.Cells(i + 4, 2).Value = oBenefitClaimReceipt.BenefitClaimHeader.Dealer.DealerCode
                    ws.Cells(i + 4, 3).Value = oBenefitClaimReceipt.BenefitClaimHeader.Dealer.SearchTerm1
                End If
                ws.Cells(i + 4, 4).Value = oBenefitClaimReceipt.BenefitClaimHeader.ClaimRegNo

                Dim strPaymentDate As String = String.Empty
                Dim dtePaymentDateX As DateTime = New DateTime
                Dim strActualPaymentDate As String = String.Empty
                Dim dteActualPaymentDateX As DateTime = New DateTime
                If Not IsNothing(oBenefitClaimReceipt.BenefitClaimHeader.BenefitClaimJV) Then
                    dtePaymentDateX = oBenefitClaimReceipt.BenefitClaimHeader.BenefitClaimJV.PaymentDate
                    dteActualPaymentDateX = oBenefitClaimReceipt.BenefitClaimHeader.BenefitClaimJV.ActualPaymentDate
                End If
                If dtePaymentDateX <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                    strPaymentDate = dtePaymentDateX.ToString("dd/MM/yyyy")
                End If
                If dteActualPaymentDateX <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                    strActualPaymentDate = dteActualPaymentDateX.ToString("dd/MM/yyyy")
                End If
                ws.Cells(i + 4, 5).Value = strPaymentDate
                ws.Cells(i + 4, 6).Value = strActualPaymentDate

                ws.Cells(i + 4, 7).Value = CommonFunction.GetEnumDescription(oBenefitClaimReceipt.Status, "BenefitClaimReceiptStatus")
                ws.Cells(i + 4, 8).Value = oBenefitClaimReceipt.ReceiptNo

                Dim dblReceiptAmount As Double = oBenefitClaimReceipt.ReceiptAmountDeducted
                Dim dblPPHTotal As Double = oBenefitClaimReceipt.PPHTotal
                Dim dblVATTotal As Double = oBenefitClaimReceipt.VATTotal

                ws.Cells(i + 4, 9).Value = dblReceiptAmount
                ws.Cells(i + 4, 10).Value = dblPPHTotal
                ws.Cells(i + 4, 11).Value = dblVATTotal

                Dim dblAmount As Double = 0
                Dim criterias4 As New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias4.opAnd(New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader.ID", MatchType.Exact, oBenefitClaimReceipt.BenefitClaimHeader.ID))
                criterias4.opAnd(New Criteria(GetType(BenefitClaimJV), "TipeAccount", MatchType.Exact, "D"))
                Dim arrlist As ArrayList = New BenefitClaimJVFacade(User).Retrieve(criterias4)
                If Not IsNothing(arrlist) AndAlso arrlist.Count > 0 Then
                    Dim objJV As BenefitClaimJV = CType(arrlist(0), BenefitClaimJV)
                    dblAmount = objJV.Amount
                End If

                ws.Cells(i + 4, 12).Value = dblAmount.ToString("#,##0")
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
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()
    End Sub

    Private Function getmonth(ByVal i As Integer) As String
        Select Case i
            Case 1
                Return "Januari"
            Case 2
                Return "Februari"
            Case 3
                Return "Maret"
            Case 4
                Return "April"
            Case 5
                Return "Mei"
            Case 6
                Return "Juni"
            Case 7
                Return "Juli"
            Case 8
                Return "Agustus"
            Case 9
                Return "September"
            Case 10
                Return "Oktober"
            Case 11
                Return "November"
            Case 12
                Return "Desember"
        End Select
        Return ""
    End Function

    Private Function WriteSAPData(ByRef lines As System.Text.StringBuilder)
        Dim list As ArrayList
        If Not sessHelper.GetSession(PageSession) Is Nothing Then
            list = CType(sessHelper.GetSession(PageSession), ArrayList)
        End If

        arlBenefitClaimReceipt = New ArrayList
        Dim line As New System.Text.StringBuilder
        Dim checkDetailStatus As Boolean = False
        Dim separator As String = ";"

        Dim listAktif As String = ""
        Dim n As Integer = -1
        For i As Integer = 0 To dgListJV.Items.Count - 1
            line.Clear()
            checkDetailStatus = False
            Dim objDomain As BenefitClaimReceipt = CType(list(i), BenefitClaimReceipt)
            If CType(dgListJV.Items(i).FindControl("chkSelect"), CheckBox).Checked Then

                If objDomain.BenefitClaimHeader.Status = 4 And (objDomain.BenefitClaimHeader.BenefitType.ReceiptBox = 1 _
                    Or objDomain.BenefitClaimHeader.BenefitType.LeasingBox = 1) Then

                    If Not objDomain.FakturPajakNo.Trim() = "" Then
                        Dim kodedealer As String = objDomain.BenefitClaimHeader.Dealer.DealerCode

                        Dim total As Decimal = 0
                        Dim model As String = ""
                        For Each cat As BenefitClaimDetails In objDomain.BenefitClaimHeader.BenefitClaimDetailss
                            total += cat.BenefitMasterDetail.Amount
                            model = cat.ChassisMaster.VechileColor.VechileType.VechileModel.Description
                        Next

                        Dim index As Integer = 0
                        Dim dpp As Decimal = 0
                        Dim textDetil As String = ""

                        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader.ID", MatchType.Exact, objDomain.BenefitClaimHeader.ID))
                        crit.opAnd(New Criteria(GetType(BenefitClaimJV), "TipeAccount", MatchType.InSet, "('A','E')"))
                        Dim aggregateSum As Aggregate = New Aggregate(GetType(BenefitClaimJV), "Amount", AggregateType.Sum)
                        Dim sumAmountJV As Decimal = IsDBNull(New BenefitClaimJVFacade(User).RetrieveScalar(crit, aggregateSum), 0)

                        Dim crit2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit2.opAnd(New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader.ID", MatchType.Exact, objDomain.BenefitClaimHeader.ID))
                        crit2.opAnd(New Criteria(GetType(BenefitClaimJV), "TipeAccount", MatchType.InSet, "('A','E')"))
                        crit2.opAnd(New Criteria(GetType(BenefitClaimJV), "Amount", MatchType.Greater, 0))
                        Dim aggregateSumPlus As Aggregate = New Aggregate(GetType(BenefitClaimJV), "Amount", AggregateType.Sum)
                        Dim sumAmountJVPlus As Decimal = IsDBNull(New BenefitClaimJVFacade(User).RetrieveScalar(crit2, aggregateSumPlus), 0)

                        Dim cutOffDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                        If Not objDomain.BenefitClaimHeader Is Nothing Then
                            For Each el As BenefitClaimReceipt In objDomain.BenefitClaimHeader.BenefitClaimReceipts
                                cutOffDate = el.ReceiptDate
                            Next

                            If cutOffDate.Year < 1900 Then
                                cutOffDate = objDomain.BenefitClaimHeader.ClaimDate
                            End If
                        Else
                            cutOffDate = DateTime.Now.Date
                        End If

                        Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(cutOffDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
                        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(cutOffDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

                        Dim dblPPH As Double = 0
                        Dim dblVAT As Double = 0
                        Dim dblAmountHdr As Double = 0
                        If objDomain.BenefitClaimHeader.BenefitType.ReceiptBox = 1 Then
                            dblVAT = 0
                        Else
                            'dblVAT = Math.Round(sumAmountJVPlus * 0.1)
                            dblVAT = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=sumAmountJVPlus)
                        End If

                        'dblPPH = Math.Round(sumAmountJVPlus * 0.15)
                        dblPPH = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=sumAmountJVPlus)
                        dblAmountHdr = sumAmountJV + dblVAT - dblPPH

                        Dim arrBenefitClaimJV As ArrayList = New BenefitClaimJVFacade(User).RetrieveList(objDomain.BenefitClaimHeader.ID)
                        For Each item As BenefitClaimJV In arrBenefitClaimJV
                            line.Clear()
                            If Not item Is Nothing Then
                                If index = 0 Then
                                    dpp = item.Amount
                                    line.Append("H")
                                    line.Append(separator)

                                    If item.TipeAccount = "D" Then
                                        line.Append("D")
                                    Else
                                        line.Append("K")
                                    End If
                                    line.Append(separator)

                                    line.Append(objDomain.BenefitClaimHeader.ClaimRegNo)
                                    line.Append(separator)

                                    If item.TipeAccount = "D" Then
                                        line.Append(objDomain.BenefitClaimHeader.Dealer.DealerCode)
                                    ElseIf item.TipeAccount = "V" Then
                                        line.Append(objDomain.BenefitClaimHeader.LeasingCompany.VendorID)
                                    End If
                                    line.Append(separator)

                                    line.Append(objDomain.ReceiptDate.ToString("yyyyMMdd"))
                                    line.Append(separator)

                                    'Dim dblReceiptAmount As Double = 0
                                    'Try
                                    '    dblReceiptAmount = IsDBNull(objDomain.ReceiptAmountDeducted, 0) - IsDBNull(objDomain.PPHTotal, 0)
                                    'Catch
                                    'End Try
                                    line.Append(dblAmountHdr.ToString("##0"))
                                    line.Append(separator)

                                    line.Append(item.PaymentDate.ToString("yyyyMMdd"))
                                    line.Append(separator)

                                    line.Append(objDomain.FakturPajakNo)
                                    line.Append(separator)

                                    line.Append(objDomain.FakturPajakDate.ToString("yyyyMMdd"))
                                    line.Append(separator)

                                    If objDomain.BenefitClaimHeader.BenefitType.LeasingBox = 1 Then
                                        If Not item.Month = Nothing Then
                                            line.Append("Insentif Pembayaran " & model & " " & objDomain.BenefitClaimHeader.BenefitClaimDetailss.Count().ToString() & " Unit" _
                                                    & " Periode " & getmonth(item.Month))
                                            textDetil = "Insentif Pembayaran"
                                        Else
                                            line.Append("Insentif Pembayaran " & model & " " & objDomain.BenefitClaimHeader.BenefitClaimDetailss.Count().ToString() & " Unit")
                                            textDetil = "Insentif Pembayaran"
                                        End If
                                    Else
                                        If Not item.Month = Nothing Then
                                            line.Append("Komisi Penjualan " & model & " " & objDomain.BenefitClaimHeader.BenefitClaimDetailss.Count().ToString() & " Unit" _
                                                    & " Periode " & getmonth(item.Month))
                                            textDetil = "Komisi Penjualan"
                                        Else
                                            line.Append("Komisi Penjualan " & model & " " & objDomain.BenefitClaimHeader.BenefitClaimDetailss.Count().ToString() & " Unit")
                                            textDetil = "Komisi Penjualan"
                                        End If
                                    End If
                                    line.Append(separator)
                                    If Not IsNothing(objDomain.DealerBankAccount) Then
                                        line.Append(objDomain.DealerBankAccount.BankAccount)
                                    Else
                                        line.Append(String.Empty)
                                    End If

                                    line.Append(vbNewLine)
                                    lines.Append(line)

                                ElseIf index = 1 Then
                                    Dim arrStrTipe As String = "VAT;WHT"
                                    For Each strTipe As String In arrStrTipe.Split(";")
                                        line.Clear()
                                        line.Append("D")
                                        line.Append(separator)

                                        line.Append(strTipe)
                                        line.Append(separator)

                                        If strTipe = "VAT" Then
                                            line.Append(dblVAT.ToString("##0"))
                                            'line.Append(objDomain.VATTotal.ToString("##0"))

                                        ElseIf strTipe = "WHT" Then
                                            line.Append(dblPPH.ToString("##0"))
                                            'line.Append(objDomain.PPHTotal.ToString("##0"))
                                        End If
                                        line.Append(separator)

                                        line.Append("")
                                        line.Append(separator)
                                        line.Append("")
                                        line.Append(separator)
                                        line.Append("")
                                        line.Append(separator)
                                        line.Append(objDomain.FakturPajakNo)
                                        line.Append(separator)
                                        line.Append(textDetil)

                                        line.Append(vbNewLine)
                                        lines.Append(line)
                                    Next
                                End If

                                If index > 0 Then
                                    line.Clear()
                                    line.Append("D")
                                    line.Append(separator)
                                    If item.TipeAccount = "E" Then
                                        line.Append("EXP")
                                    ElseIf item.TipeAccount = "V" Then
                                        line.Append(item.VendorID)
                                    ElseIf item.TipeAccount = "O" Then
                                        line.Append("OTH")
                                    ElseIf item.TipeAccount = "A" Then
                                        line.Append("ACC")
                                    Else
                                        line.Append(kodedealer)
                                    End If
                                    line.Append(separator)
                                    line.Append(item.Amount.ToString("##0"))
                                    line.Append(separator)
                                    line.Append(item.CostCenter)
                                    line.Append(separator)
                                    line.Append(item.BusinessArea)
                                    line.Append(separator)
                                    line.Append(item.InternalOrder)
                                    line.Append(separator)

                                    If item.TipeAccount = "E" Then
                                        line.Append(item.Remarks)
                                    ElseIf item.TipeAccount = "A" Then
                                        line.Append(item.MasterAccrued.AccKey)
                                    Else
                                        line.Append("")
                                    End If
                                    line.Append(separator)
                                    Dim _textDetail As String = IIf(item.Amount >= 0, textDetil, "Reduksi Komisi")
                                    line.Append(_textDetail)

                                    line.Append(vbNewLine)
                                    lines.Append(line)
                                End If

                                index = index + 1
                            End If
                        Next
                        If index > 0 Then
                            arlBenefitClaimReceipt.Add(objDomain)
                        End If
                    End If
                End If
            End If
        Next

    End Function

    Private Sub DoDownload1()
        Dim lines As New System.Text.StringBuilder
        WriteSAPData(lines)

        If Not lines.ToString = "" Then
            Dim datetimenow As String = Now.ToString("yyyyMMddHmmss")
            Dim ClaimDataPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\campaign\JVCMPGN" & datetimenow & ".txt"

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            imp.Start()
            Try
                Dim dirInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(Path.GetDirectoryName(ClaimDataPath))
                If Not dirInfo.Exists Then
                    dirInfo.Create()
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(ClaimDataPath, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                sw.WriteLine(lines.ToString)

                '-- Close stream & file
                sw.Close()
                fs.Close()

                Dim n As Short = New BenefitClaimReceiptFacade(User).UpdateStatusTransfer(arlBenefitClaimReceipt)
                If n = 0 Then
                    n = New BenefitClaimHeaderFacade(User).UpdateStatusTransfer(arlBenefitClaimReceipt)
                End If
                arlBenefitClaimReceipt = New ArrayList

                imp.StopImpersonate()
                imp = Nothing
                MessageBox.Show("Data berhasil diupload ke SAP")

            Catch ex As Exception
                Dim errMess As String = ex.Message
            End Try
        Else
            MessageBox.Show("Tidak ada data yang diupload ke SAP. Cek faktur pajak, status proses dan merupakan cashback atau leasing")
        End If
    End Sub

    Private Sub btnTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Dim list As ArrayList
        If Not sessHelper.GetSession(PageSession) Is Nothing Then
            list = CType(sessHelper.GetSession(PageSession), ArrayList)
        End If

        Dim checkChecked As Boolean = False
        Dim checkProses As Boolean = False
        For i As Integer = 0 To dgListJV.Items.Count - 1
            Dim objDomain As BenefitClaimReceipt = CType(list(i), BenefitClaimReceipt)
            If CType(dgListJV.Items(i).FindControl("chkSelect"), CheckBox).Checked Then
                checkChecked = True

                If objDomain.BenefitClaimHeader.Status = 4 And (objDomain.BenefitClaimHeader.BenefitType.ReceiptBox = 1 Or objDomain.BenefitClaimHeader.BenefitType.LeasingBox = 1) Then
                    checkProses = True
                End If
            End If
        Next

        If checkChecked = False Then
            MessageBox.Show("Check list data minimal satu")
            Return
        End If

        If checkProses = False Then
            MessageBox.Show("Check list data claim yang berstatus Proses, Cashbask atau Leasing minimal satu")
            Return
        End If

        DoDownload1()
    End Sub

    Protected Sub dgListJV_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgListJV.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                Response.Redirect("FrmClaimJV.aspx?Mode=Detail&BenefitClaimReceiptID=" & e.Item.Cells(0).Text)

            Case "ViewKuitansi"
                Dim strBenefitClaimHeaderID As String = "0"
                Dim objBenefitClaimReceipt As BenefitClaimReceipt = New BenefitClaimReceiptFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                If Not IsNothing(objBenefitClaimReceipt) AndAlso objBenefitClaimReceipt.ID > 0 Then
                    strBenefitClaimHeaderID = objBenefitClaimReceipt.BenefitClaimHeader.ID
                End If
                Response.Redirect("~/Benefit/FrmReceipt.aspx?id=" & strBenefitClaimHeaderID & "&justview=1&redirectFrom=FrmClaimJVList.aspx")

            Case "addJv"
                Dim strMode As String = "New"
                Dim objBenefitClaimReceipt As BenefitClaimReceipt = New BenefitClaimReceiptFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                Dim crit As New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader.ID", MatchType.Exact, objBenefitClaimReceipt.BenefitClaimHeader.ID))
                Dim arrJV As ArrayList = New BenefitClaimJVFacade(User).Retrieve(crit)
                If Not IsNothing(arrJV) AndAlso arrJV.Count > 0 Then
                    strMode = "Edit"
                End If

                Response.Redirect("~/Benefit/FrmClaimJV.aspx?Mode=" & strMode & "&BenefitClaimReceiptID=" & e.Item.Cells(0).Text)

        End Select
    End Sub

    Private Sub dgListJV_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgListJV.PageIndexChanged
        '-- Change datagrid page

        dgListJV.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgListJV_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgListJV.SortCommand
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
        dgListJV.CurrentPageIndex = 0
        BindGrid(dgListJV.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Private Function IsDBNull(ByVal obj As Object, ByVal replacement As Decimal) As Decimal
        If obj Is DBNull.Value Then
            Return replacement
        End If
        Return CType(obj, Decimal)
    End Function

    Private Function getmonthDesc(ByVal i As Integer) As String
        Select Case i
            Case 1
                Return "Januari"
            Case 2
                Return "Februari"
            Case 3
                Return "Maret"
            Case 4
                Return "April"
            Case 5
                Return "Mei"
            Case 6
                Return "Juni"
            Case 7
                Return "Juli"
            Case 8
                Return "Agustus"
            Case 9
                Return "September"
            Case 10
                Return "Oktober"
            Case 11
                Return "November"
            Case 12
                Return "Desember"
        End Select
        Return ""
    End Function

End Class