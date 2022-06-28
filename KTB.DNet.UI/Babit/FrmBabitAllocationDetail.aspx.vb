Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports OfficeOpenXml
Imports System.Collections.Generic

Public Class FrmBabitAllocationDetail
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private calculatePriv As Boolean
    Private oDealer As Dealer
    Private SessionGridData = "FrmBabitAllocationDetail.gridList"
    Private SessionCriteriaGrid = "FrmBabitAllocationDetail.CriteriaGrid"

#Region "Custome Method"
    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Babit_Alokasi_Detail_Display_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - BABIT ALOKASI DETAIL")
        Else
            displayPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Babit_Alokasi_Detail_Display_Privilege)
            editPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Babit_Alokasi_Detail_Edit_Privilege)
            calculatePriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Babit_Alokasi_Detail_Calculate_Privilege)
        End If
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub BindDDLBabitAllocationType()
        With ddlBabitAllocationType
            .Items.Clear()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            .Items.Insert(1, New ListItem("BABIT Reguler", 0))
            .Items.Insert(2, New ListItem("BABIT Tambahan", 1))
        End With
        ddlBabitAllocationType.SelectedIndex = 0
    End Sub

    Private Sub BindDDLPeriod()
        With ddlPeriodeMonth
            .Items.Clear()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            .Items.Insert(1, New ListItem("Apr - Jun  (Q1)", "04,05,06"))
            .Items.Insert(2, New ListItem("Jul - Sept (Q2)", "07,08,09"))
            .Items.Insert(3, New ListItem("Okt - Des  (Q3)", "10,11,12"))
            .Items.Insert(4, New ListItem("Jan - Mar  (Q4)", "01,02,03"))
        End With
        ddlBabitAllocationType.SelectedIndex = 0

        ddlPeriodeYear.Items.Clear()
        With ddlPeriodeYear.Items
            .Insert(0, New ListItem("Silahkan Pilih", -1))
            Dim _date As Integer = Date.Now.Year - 1
            For i As Integer = 0 To 4 Step 1
                .Add(New ListItem(_date, _date))
                _date = _date + 1
            Next
        End With
        ddlPeriodeYear.SelectedIndex = 0
    End Sub

    Private Sub BindDDLCategory()
        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, "MMC"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
        ddlCategory.SelectedIndex = 0
        BindVehicleSubCategoryToDDL(ddlModel, ddlCategory.SelectedValue)
    End Sub

    Function BindVehicleSubCategoryToDDL(ByVal ddlModel As DropDownList, ByVal intDdlCategoryID As Integer)
        ddlModel.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.Exact, ""))  '-- Type still active
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.Exact, intDdlCategoryID))
        Dim arrList As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(criterias)
        With ddlModel.Items
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each obj As SubCategoryVehicle In arrList
                .Add(New ListItem(obj.Name, obj.ID))
            Next
        End With
        ddlModel.SelectedIndex = 0
    End Function

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrBabitMasterRetailTargetList As ArrayList = CType(sesHelper.GetSession(SessionGridData), ArrayList)
        If arrBabitMasterRetailTargetList.Count <> 0 Then
            CommonFunction.SortListControl(arrBabitMasterRetailTargetList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrBabitMasterRetailTargetList, pageIndex, dgListBabitMasterRetailTarget.PageSize)
            dgListBabitMasterRetailTarget.DataSource = PagedList
            dgListBabitMasterRetailTarget.VirtualItemCount = arrBabitMasterRetailTargetList.Count()
            dgListBabitMasterRetailTarget.DataBind()
        Else
            dgListBabitMasterRetailTarget.DataSource = New ArrayList
            dgListBabitMasterRetailTarget.VirtualItemCount = 0
            dgListBabitMasterRetailTarget.CurrentPageIndex = 0
            dgListBabitMasterRetailTarget.DataBind()
        End If
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtKodeDealer.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(V_BabitMasterRetailTarget), "Dealer.DealerCode", MatchType.Partial, txtKodeDealer.Text))
        End If
        If txtKodeTempOut.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(V_BabitMasterRetailTarget), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtKodeTempOut.Text.Replace(";", "','") & "')"))
        End If

        If ddlCategory.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(V_BabitMasterRetailTarget), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        'If ddlModel.SelectedIndex <> 0 Then
        '    crit.opAnd(New Criteria(GetType(V_BabitMasterRetailTarget), "SubCategoryVehicleName", MatchType.Exact, ddlModel.SelectedItem.Text))
        'End If

        If ddlPeriodeMonth.SelectedIndex > 0 Then
            If ddlPeriodeYear.SelectedIndex = 0 Then
                MessageBox.Show(SR.DataNotFound("Pilih Periode Tahun dahulu"))
                sesHelper.SetSession(SessionGridData, New ArrayList)
                sesHelper.RemoveSession("FrmBabitAllocationDetail.criteriadownload")
                Exit Sub
            End If
            crit.opAnd(New Criteria(GetType(V_BabitMasterRetailTarget), "YearPeriod", MatchType.Exact, ddlPeriodeYear.SelectedValue))
            crit.opAnd(New Criteria(GetType(V_BabitMasterRetailTarget), "QuarterPeriodText", MatchType.Partial, ddlPeriodeMonth.SelectedValue))
        End If

        sesHelper.SetSession("FrmBabitAllocationDetail.criteriadownload", crit)

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(V_BabitMasterRetailTarget), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrV_BabitMasterRetailTargetList As ArrayList = New V_BabitMasterRetailTargetFacade(User).Retrieve(crit, sortColl)
        sesHelper.SetSession(SessionGridData, arrV_BabitMasterRetailTargetList)
        If arrV_BabitMasterRetailTargetList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

#End Region

#Region "Standard Method"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Authorization()
        If Not IsPostBack Then
            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            txtKodeDealer.Text = SesDealer().DealerCode
            hdnDealer.Value = SesDealer().DealerCode
            lblKodeDealer.Text = SesDealer().DealerCode & " / " & SesDealer().DealerName

            lblPopUpDealer.Attributes("onclick") = "ShowPopUpDealer();"
            lblPopUpTO.Attributes("onclick") = "ShowPopUpTO();"

            BindDDLBabitAllocationType()
            BindDDLCategory()
            BindDDLPeriod()

            If IsLoginAsDealer() Then
                txtKodeDealer.Visible = False
                lblPopUpDealer.Visible = False
                lblKodeDealer.Visible = True
                btnCalculate.Visible = False
                dgListBabitMasterRetailTarget.Columns(5).Visible = False
                dgListBabitMasterRetailTarget.Columns(9).Visible = False
                dgListBabitMasterRetailTarget.Columns(dgListBabitMasterRetailTarget.Columns.Count - 1).Visible = False
            Else
                lblPopUpDealer.Visible = True
                txtKodeDealer.Visible = True
                lblKodeDealer.Visible = False
                txtKodeDealer.Text = ""
                btnCalculate.Visible = calculatePriv
                dgListBabitMasterRetailTarget.Columns(5).Visible = True
                dgListBabitMasterRetailTarget.Columns(9).Visible = True
                dgListBabitMasterRetailTarget.Columns(dgListBabitMasterRetailTarget.Columns.Count - 1).Visible = True
            End If
            ReadData()   '-- Read all data matching criteria
            BindGrid(dgListBabitMasterRetailTarget.CurrentPageIndex)  '-- Bind page-1
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ReadData()
        dgListBabitMasterRetailTarget.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dgListBabitMasterRetailTarget.CurrentPageIndex)  '-- Bind page-1
    End Sub

    Private Sub dgListBabitMasterRetailTarget_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgListBabitMasterRetailTarget.PageIndexChanged
        '-- Change datagrid page

        ReadData()
        dgListBabitMasterRetailTarget.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

    Private Sub dgListBabitMasterRetailTarget_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgListBabitMasterRetailTarget.SortCommand
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
        dgListBabitMasterRetailTarget.CurrentPageIndex = 0
        ReadData()
        BindGrid(dgListBabitMasterRetailTarget.CurrentPageIndex)
    End Sub

    Protected Sub dgListBabitMasterRetailTarget_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgListBabitMasterRetailTarget.ItemCommand
        Dim lblEDealerCode As Label
        Dim lblETempOut As Label
        Dim txtEBabitAdditionalAllocation As TextBox
        Dim hdnID As HiddenField
        Dim objBabitBudgetHeader As New BabitBudgetHeader
        Dim objBabitBudgetDetail As New BabitBudgetDetail

        Dim arrBabitMasterRetailTargetList As ArrayList = CType(sesHelper.GetSession(SessionGridData), ArrayList)
        If e.Item.ItemType = ListItemType.Item Then
            objBabitBudgetHeader = New BabitBudgetHeaderFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            hdnID = CType(e.Item.FindControl("hdnID"), HiddenField)
            objBabitBudgetHeader = New BabitBudgetHeaderFacade(User).Retrieve(CInt(hdnID.Value))
        End If

        Select Case e.CommandName
            Case "save" 'Update this datagrid item   
                lblEDealerCode = CType(e.Item.FindControl("lblEDealerCode"), Label)
                lblETempOut = CType(e.Item.FindControl("lblETempOut"), Label)
                txtEBabitAdditionalAllocation = CType(e.Item.FindControl("txtEBabitAdditionalAllocation"), TextBox)

                If Not IsNothing(objBabitBudgetHeader) AndAlso objBabitBudgetHeader.ID > 0 Then
                    objBabitBudgetHeader.AdditionalPrice = txtEBabitAdditionalAllocation.Text
                    objBabitBudgetHeader.TotalAllocationBabit = objBabitBudgetHeader.AllocationBabit + objBabitBudgetHeader.AdditionalPrice
                    Dim _result As Integer = New BabitBudgetHeaderFacade(User).Update(objBabitBudgetHeader)
                    If _result > 0 Then
                        MessageBox.Show("Simpan data berhasil")
                    Else
                        MessageBox.Show("Simpan data gagal")
                    End If
                End If
                dgListBabitMasterRetailTarget.EditItemIndex = -1
                dgListBabitMasterRetailTarget.ShowFooter = True

            Case "edit" 'Edit mode activated
                dgListBabitMasterRetailTarget.ShowFooter = False
                dgListBabitMasterRetailTarget.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitBudgetDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crits.opAnd(New Criteria(GetType(BabitBudgetDetail), "BabitBudgetHeader.ID", MatchType.Exact, objBabitBudgetHeader.ID))
                Dim arrBBD As ArrayList = New BabitBudgetDetailFacade(User).Retrieve(crits)
                Dim _result As Integer = New BabitBudgetDetailFacade(User).DeleteTransaction(objBabitBudgetHeader, arrBBD)
                If _result > 0 Then
                    arrBabitMasterRetailTargetList.RemoveAt(e.Item.ItemIndex)
                    MessageBox.Show("Hapus data berhasil")
                Else
                    MessageBox.Show("Hapus data gagal")
                End If

            Case "cancel" 'Cancel Update this datagrid item 
                dgListBabitMasterRetailTarget.EditItemIndex = -1
                dgListBabitMasterRetailTarget.ShowFooter = True
        End Select

        sesHelper.SetSession(SessionGridData, arrBabitMasterRetailTargetList)
        ReadData()   '-- Read all data matching criteria
        BindGrid(dgListBabitMasterRetailTarget.CurrentPageIndex)
    End Sub

    Protected Sub dgListBabitMasterRetailTarget_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgListBabitMasterRetailTarget.ItemDataBound

        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
        Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
        Dim lblTempOut As Label = CType(e.Item.FindControl("lblTempOut"), Label)
        Dim lblTargetRetail As Label = CType(e.Item.FindControl("lblTargetRetail"), Label)
        Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
        Dim lblModel As Label = CType(e.Item.FindControl("lblModel"), Label)
        Dim lblPeriode As Label = CType(e.Item.FindControl("lblPeriode"), Label)
        Dim lblBabitAllocation As Label = CType(e.Item.FindControl("lblBabitAllocation"), Label)
        Dim lblBabitAdditionalAllocation As Label = CType(e.Item.FindControl("lblBabitAdditionalAllocation"), Label)
        Dim lblBabitTotalAllocation As Label = CType(e.Item.FindControl("lblBabitTotalAllocation"), Label)
        Dim lblBabitTotalAllocation2 As Label = CType(e.Item.FindControl("lblBabitTotalAllocation2"), Label)
        Dim lblRemains As Label = CType(e.Item.FindControl("lblRemains"), Label)
        Dim lblOngoing As Label = CType(e.Item.FindControl("lblOngoing"), Label)

        Dim lblENo As Label = CType(e.Item.FindControl("lblENo"), Label)
        Dim lblEDealerCode As Label = CType(e.Item.FindControl("lblEDealerCode"), Label)
        Dim lblEDealerName As Label = CType(e.Item.FindControl("lblEDealerName"), Label)
        Dim lblETempOut As Label = CType(e.Item.FindControl("lblETempOut"), Label)
        Dim lblETargetRetail As Label = CType(e.Item.FindControl("lblETargetRetail"), Label)
        Dim lblECategory As Label = CType(e.Item.FindControl("lblECategory"), Label)
        Dim lblEModel As Label = CType(e.Item.FindControl("lblEModel"), Label)
        Dim lblEPeriode As Label = CType(e.Item.FindControl("lblEPeriode"), Label)
        Dim lblEBabitAllocation As Label = CType(e.Item.FindControl("lblEBabitAllocation"), Label)
        Dim txtEBabitAdditionalAllocation As TextBox = CType(e.Item.FindControl("txtEBabitAdditionalAllocation"), TextBox)
        Dim txtEBabitTotalAllocation As TextBox = CType(e.Item.FindControl("txtEBabitTotalAllocation"), TextBox)
        Dim lblERemains As Label = CType(e.Item.FindControl("lblERemains"), Label)
        Dim lblEOngoing As Label = CType(e.Item.FindControl("lblEOngoing"), Label)
        Dim hdnID As HiddenField = CType(e.Item.FindControl("hdnID"), HiddenField)

        If e.Item.ItemType = ListItemType.EditItem Then
            Dim oData As V_BabitMasterRetailTarget = CType(e.Item.DataItem, V_BabitMasterRetailTarget)
            hdnID.Value = oData.ID
            lblENo.Text = (e.Item.ItemIndex + 1 + (dgListBabitMasterRetailTarget.PageSize * dgListBabitMasterRetailTarget.CurrentPageIndex)).ToString

            If Not IsNothing(oData.Dealer) Then
                lblEDealerCode.Text = oData.Dealer.DealerCode
                lblEDealerName.Text = oData.Dealer.DealerName
            End If
            If Not IsNothing(oData.DealerBranch) Then
                lblETempOut.Text = oData.DealerBranch.DealerBranchCode
            End If
            lblETargetRetail.Text = oData.RetailTarget.ToString("#,##0")
            lblECategory.Text = oData.Category.CategoryCode
            If oData.SubCategoryVehicleID = 0 Then
                lblEModel.Text = "*"
            Else
                Dim objSubCategoryVehicle As SubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(oData.SubCategoryVehicleID)
                lblEModel.Text = objSubCategoryVehicle.Name
            End If

            Dim strMonthFirst As String = String.Empty
            Dim strMonthLast As String = String.Empty
            Dim strPeriode As String = String.Empty
            Dim arrMonthPeriod As String() = oData.QuarterPeriodText.Trim.Split(",")
            For i As Integer = 0 To arrMonthPeriod.Length - 1
                If strMonthFirst = String.Empty Then
                    strMonthFirst = arrMonthPeriod(i)
                End If
                If strMonthLast = String.Empty Then
                    strMonthLast = arrMonthPeriod(i)
                End If
                If CInt(strMonthFirst) > arrMonthPeriod(i) Then
                    strMonthFirst = arrMonthPeriod(i)
                End If
                If CInt(strMonthLast) < arrMonthPeriod(i) Then
                    strMonthLast = arrMonthPeriod(i)
                End If
            Next
            Dim strRangeMonth As String = String.Empty
            If strMonthFirst.Trim = strMonthLast.Trim Then
                strMonthFirst = enumMonthGet.GetName(CInt(strMonthFirst))
                strRangeMonth = strMonthFirst.Trim & " " & CStr(oData.YearPeriod)
            Else
                Dim strQ As String = String.Empty
                Select Case CInt(strMonthFirst)
                    Case 4, 5, 6
                        strQ = "(Q1)"
                    Case 7, 8, 9
                        strQ = "(Q2)"
                    Case 10, 11, 12
                        strQ = "(Q3)"
                    Case 1, 2, 3
                        strQ = "(Q4)"
                End Select
                strMonthFirst = enumMonthGet.GetName(CInt(strMonthFirst))
                strMonthLast = enumMonthGet.GetName(CInt(strMonthLast))
                strRangeMonth = strMonthFirst.Trim & " - " & strMonthLast.Trim & " " & CStr(oData.YearPeriod) & " " & strQ
            End If
            lblEPeriode.Text = strRangeMonth

            lblEBabitAllocation.Text = oData.AllocationBabit.ToString("#,##0")
            txtEBabitAdditionalAllocation.Text = oData.AdditionalPrice.ToString("#,##0")
            txtEBabitTotalAllocation.Text = oData.TotalAllocationBabit.ToString("#,##0")

            lblERemains.Text = (oData.TotalAllocationBabit - oData.SumSubsidyAmount).ToString("#,##0")
            lblEOngoing.Text = oData.OnGoing.ToString("#,##0")
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As V_BabitMasterRetailTarget = CType(e.Item.DataItem, V_BabitMasterRetailTarget)

            lblNo.Text = (e.Item.ItemIndex + 1 + (dgListBabitMasterRetailTarget.PageSize * dgListBabitMasterRetailTarget.CurrentPageIndex)).ToString

            If Not IsNothing(oData.Dealer) Then
                lblDealerCode.Text = oData.Dealer.DealerCode
                lblDealerName.Text = oData.Dealer.DealerName
            End If
            If Not IsNothing(oData.DealerBranch) Then
                lblTempOut.Text = oData.DealerBranch.DealerBranchCode
            End If
            lblTargetRetail.Text = oData.RetailTarget.ToString("#,##0")
            lblCategory.Text = oData.Category.CategoryCode
            If oData.SubCategoryVehicleID = 0 Then
                lblModel.Text = "*"
            Else
                Dim objSubCategoryVehicle As SubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(oData.SubCategoryVehicleID)
                lblModel.Text = objSubCategoryVehicle.Name
            End If

            Dim strMonthFirst As String = String.Empty
            Dim strMonthLast As String = String.Empty
            Dim strPeriode As String = String.Empty
            Dim arrMonthPeriod As String() = oData.QuarterPeriodText.Trim.Split(",")
            For i As Integer = 0 To arrMonthPeriod.Length - 1
                If strMonthFirst = String.Empty Then
                    strMonthFirst = arrMonthPeriod(i)
                End If
                If strMonthLast = String.Empty Then
                    strMonthLast = arrMonthPeriod(i)
                End If
                If CInt(strMonthFirst) > arrMonthPeriod(i) Then
                    strMonthFirst = arrMonthPeriod(i)
                End If
                If CInt(strMonthLast) < arrMonthPeriod(i) Then
                    strMonthLast = arrMonthPeriod(i)
                End If
            Next
            Dim strRangeMonth As String = String.Empty
            If strMonthFirst.Trim = strMonthLast.Trim Then
                strMonthFirst = enumMonthGet.GetName(CInt(strMonthFirst))
                strRangeMonth = strMonthFirst.Trim & " " & CStr(oData.YearPeriod)
            Else
                Dim strQ As String = String.Empty
                Select Case CInt(strMonthFirst)
                    Case 4, 5, 6
                        strQ = "(Q1)"
                    Case 7, 8, 9
                        strQ = "(Q2)"
                    Case 10, 11, 12
                        strQ = "(Q3)"
                    Case 1, 2, 3
                        strQ = "(Q4)"
                End Select

                strMonthFirst = enumMonthGet.GetName(CInt(strMonthFirst))
                strMonthLast = enumMonthGet.GetName(CInt(strMonthLast))
                strRangeMonth = strMonthFirst.Trim & " - " & strMonthLast.Trim & " " & CStr(oData.YearPeriod) & " " & strQ
            End If
            lblPeriode.Text = strRangeMonth
            lblBabitAllocation.Text = oData.AllocationBabit.ToString("#,##0")

            lblBabitAdditionalAllocation.Text = oData.AdditionalPrice.ToString("#,##0")
            lblBabitTotalAllocation.Text = oData.TotalAllocationBabit.ToString("#,##0")
            lblRemains.Text = (oData.TotalAllocationBabit - oData.SumSubsidyAmount).ToString("#,##0")
            lblOngoing.Text = oData.OnGoing.ToString("#,##0")

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            If Not IsNothing(lbtnEdit) Then
                lbtnEdit.Visible = editPriv
            End If
        End If
    End Sub

    Protected Sub hdnDealer_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealer.ValueChanged
        Dim data As String() = hdnDealer.Value.Trim.Split(";")
        txtKodeDealer.Text = data(0)
    End Sub

    'Protected Sub hdnTempOut_ValueChanged(sender As Object, e As EventArgs) Handles hdnTempOut.ValueChanged
    '    Dim data As String() = hdnTempOut.Value.Trim.Split(";")
    '    txtKodeTempOut.Text = data(0)
    'End Sub

    Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        BindVehicleSubCategoryToDDL(ddlModel, ddlCategory.SelectedValue)
    End Sub

#End Region

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgListBabitMasterRetailTarget.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sesHelper.GetSession("FrmBabitAllocationDetail.criteriadownload")) Then
            crits = CType(sesHelper.GetSession("FrmBabitAllocationDetail.criteriadownload"), CriteriaComposite)
            arrData = New V_BabitMasterRetailTargetFacade(User).Retrieve(crits)
        End If

        ' mengambil data yang dibutuhkan
        If arrData.Count > 0 Then
            CreateExcel("Daftar BABIT Allocation Detail", arrData)
        Else
            MessageBox.Show("Tidak ada data yang di download")
            Return
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
            ws.Cells("B3").Value = "Kode Dealer"
            ws.Cells("C3").Value = "Nama Dealer"
            ws.Cells("D3").Value = "Kode Temporary Outlet"
            ws.Cells("E3").Value = "Retail Target"
            ws.Cells("F3").Value = "Kategori"
            ws.Cells("G3").Value = "Model"
            ws.Cells("H3").Value = "Periode"
            'ws.Cells("I3").Value = "Harga/Unit"
            ws.Cells("J3").Value = "Alokasi BABIT"
            ws.Cells("K3").Value = "Alokasi Tambahan"
            ws.Cells("L3").Value = "Total Alokasi"
            ws.Cells("M3").Value = "Sisa"
            ws.Cells("N3").Value = "Ongoing"

            For i As Integer = 0 To Data.Count - 1
                Dim item As V_BabitMasterRetailTarget = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                If IsNothing(item.Dealer) Then
                    ws.Cells(i + 4, 2).Value = ""
                    ws.Cells(i + 4, 3).Value = ""
                Else
                    ws.Cells(i + 4, 2).Value = item.Dealer.DealerCode
                    ws.Cells(i + 4, 3).Value = item.Dealer.DealerName
                End If
                If IsNothing(item.DealerBranch) Then
                    ws.Cells(i + 4, 4).Value = ""
                Else
                    ws.Cells(i + 4, 4).Value = item.DealerBranch.DealerBranchCode
                End If
                ws.Cells(i + 4, 5).Value = item.RetailTarget
                ws.Cells(i + 4, 6).Value = item.Category.CategoryCode
                If item.SubCategoryVehicleID = 0 Then
                    ws.Cells(i + 4, 7).Value = "*"
                Else
                    Dim objSubCategoryVehicle As SubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(item.SubCategoryVehicleID)
                    ws.Cells(i + 4, 7).Value = objSubCategoryVehicle.Name
                End If

                Dim strMonthFirst As String = String.Empty
                Dim strMonthLast As String = String.Empty
                Dim strPeriode As String = String.Empty
                Dim arrMonthPeriod As String() = item.QuarterPeriodText.Trim.Split(",")
                For j As Integer = 0 To arrMonthPeriod.Length - 1
                    If strMonthFirst = String.Empty Then
                        strMonthFirst = arrMonthPeriod(j)
                    End If
                    If strMonthLast = String.Empty Then
                        strMonthLast = arrMonthPeriod(j)
                    End If
                    If CInt(strMonthFirst) > arrMonthPeriod(j) Then
                        strMonthFirst = arrMonthPeriod(j)
                    End If
                    If CInt(strMonthLast) < arrMonthPeriod(j) Then
                        strMonthLast = arrMonthPeriod(j)
                    End If
                Next
                Dim strRangeMonth As String = String.Empty
                If strMonthFirst.Trim = strMonthLast.Trim Then
                    strMonthFirst = enumMonthGet.GetName(CInt(strMonthFirst))
                    strRangeMonth = strMonthFirst.Trim & " " & CStr(item.YearPeriod)
                Else

                    Dim strQ As String = String.Empty
                    Select Case CInt(strMonthFirst)
                        Case 4, 5, 6
                            strQ = "(Q1)"
                        Case 7, 8, 9
                            strQ = "(Q2)"
                        Case 10, 11, 12
                            strQ = "(Q3)"
                        Case 1, 2, 3
                            strQ = "(Q4)"
                    End Select
                    strMonthFirst = enumMonthGet.GetName(CInt(strMonthFirst))
                    strMonthLast = enumMonthGet.GetName(CInt(strMonthLast))
                    strRangeMonth = strMonthFirst.Trim & " - " & strMonthLast.Trim & " " & CStr(item.YearPeriod) & " " & strQ
                End If
                ws.Cells(i + 4, 8).Value = strRangeMonth
                ws.Cells(i + 4, 10).Value = item.AllocationBabit.ToString("#,##0")
                ws.Cells(i + 4, 11).Value = item.AdditionalPrice.ToString("#,##0")

                Dim dblBabitTotalAllocation As Double = item.AllocationBabit + item.AdditionalPrice
                ws.Cells(i + 4, 12).Value = dblBabitTotalAllocation.ToString("#,##0")
                ws.Cells(i + 4, 13).Value = (item.TotalAllocationBabit - item.SumSubsidyAmount).ToString("#,##0")
                ws.Cells(i + 4, 14).Value = item.OnGoing.ToString("#,##0")
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

    Protected Sub AddBabitBudgetHeaderSpecial(intQuarterPeriod As Short, strYearPeriod As String)
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitBudgetHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strSQL As String = String.Empty
        Dim _result As Short = 0

        strSQL = "select ID "
        strSQL += "from BabitBudgetHeader "
        strSQL += "where RowStatus = 0 "
        strSQL += "and DealerID Is null "
        strSQL += "and CategoryID is null "
        strSQL += "and QuarterPeriod = " & intQuarterPeriod & " "
        strSQL += "and YearPeriod = " & strYearPeriod & " "

        Dim objBBH As New BabitBudgetHeader
        crits.opAnd(New Criteria(GetType(BabitBudgetHeader), "ID", MatchType.InSet, "(" & strSQL & ")"))
        Dim arrBBH As ArrayList = New BabitBudgetHeaderFacade(User).Retrieve(crits)
        If arrBBH.Count > 0 Then
            objBBH = CType(arrBBH(0), BabitBudgetHeader)
        End If
        With objBBH
            .QuarterPeriod = intQuarterPeriod
            .YearPeriod = strYearPeriod
        End With
        If objBBH.ID = 0 Then
            _result = New BabitBudgetHeaderFacade(User).Insert(objBBH)
        Else
            _result = New BabitBudgetHeaderFacade(User).Update(objBBH)
        End If

    End Sub

    Protected Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        If ddlPeriodeMonth.SelectedIndex = 0 Then
            MessageBox.Show("Harap pilih Periode Bulan dahulu")
            Exit Sub
        End If
        If ddlPeriodeYear.SelectedIndex = 0 Then
            MessageBox.Show("Harap pilih Periode Tahun dahulu")
            Exit Sub
        End If

        Dim mode As String = "New"
        Dim intCategoryID As Integer = 0
        Dim intCategoryID2 As Integer = 0
        Dim strYearPeriod As String = ""
        Dim strYearPeriod2 As String = ""
        Dim intDealerID As Integer = 0
        Dim intDealerID2 As Integer = 0
        Dim intDealerBranchID As Integer = 0
        Dim intDealerBranchID2 As Integer = 0
        Dim intSubCategoryVehicleID As Integer = 0
        Dim intSubCategoryVehicleID2 As Integer = 0
        Dim strSpecialCategoryFlag As String = ""
        Dim strSpecialCategoryFlag2 As String = ""
        Dim intQuarterPeriod As Integer = 0
        Dim intQuarterPeriod2 As Integer = 0
        Dim dblAllocationBabit As Double = 0

        Dim arrBabitBudgetDetail As New ArrayList
        Dim arrDelBabitBudgetDetail As New ArrayList
        Dim objBabitBudgetHeader As BabitBudgetHeader

        Dim strPeriodMonth As String = ddlPeriodeMonth.SelectedValue.Replace("; ", ",")

        If ddlPeriodeMonth.SelectedItem.Text.Contains("Q1") Then
            intQuarterPeriod2 = 1
        ElseIf ddlPeriodeMonth.SelectedItem.Text.Contains("Q2") Then
            intQuarterPeriod2 = 2
        ElseIf ddlPeriodeMonth.SelectedItem.Text.Contains("Q3") Then
            intQuarterPeriod2 = 3
        ElseIf ddlPeriodeMonth.SelectedItem.Text.Contains("Q4") Then
            intQuarterPeriod2 = 4
        End If

        '--- Insert Babit Budget Special
        AddBabitBudgetHeaderSpecial(intQuarterPeriod2, ddlPeriodeYear.SelectedValue)

        Dim arrBMRT As ArrayList = New BabitMasterRetailTargetFacade(User).RetrieveFromSPAlloc(txtKodeDealer.Text.Trim, ddlPeriodeYear.SelectedValue, strPeriodMonth, 1)
        If arrBMRT.Count > 0 Then
            For Each objBabitMasterRetailTarget As BabitMasterRetailTarget In arrBMRT
                Dim objBabitBudgetDetail As BabitBudgetDetail

                intDealerID2 = objBabitMasterRetailTarget.Dealer.ID
                If Not IsNothing(objBabitMasterRetailTarget.DealerBranch) Then
                    intDealerBranchID2 = objBabitMasterRetailTarget.DealerBranch.ID
                Else
                    intDealerBranchID2 = 0
                End If
                intSubCategoryVehicleID2 = objBabitMasterRetailTarget.SubCategoryVehicle.ID
                strYearPeriod2 = objBabitMasterRetailTarget.YearPeriod
                intCategoryID2 = objBabitMasterRetailTarget.CategoryID
                strSpecialCategoryFlag2 = objBabitMasterRetailTarget.SpecialCategoryFlag

                If intDealerID <> intDealerID2 OrElse
                    intDealerBranchID <> intDealerBranchID2 OrElse
                    intCategoryID <> intCategoryID2 OrElse
                    strSpecialCategoryFlag <> strSpecialCategoryFlag2 OrElse
                    strYearPeriod <> strYearPeriod2 OrElse
                    intQuarterPeriod <> intQuarterPeriod2 Then

                    If Not IsNothing(objBabitBudgetHeader) AndAlso arrBabitBudgetDetail.Count > 0 Then
                        Dim _result As Integer = 0
                        objBabitBudgetHeader.AllocationBabit = dblAllocationBabit
                        objBabitBudgetHeader.TotalAllocationBabit = dblAllocationBabit + objBabitBudgetHeader.AdditionalPrice
                        If mode = "Edit" Then
                            _result = New BabitBudgetDetailFacade(User).UpdateTransaction(objBabitBudgetHeader, arrBabitBudgetDetail, arrDelBabitBudgetDetail)
                        Else
                            _result = New BabitBudgetDetailFacade(User).InsertTransaction(objBabitBudgetHeader, arrBabitBudgetDetail)
                        End If
                        arrBabitBudgetDetail = New ArrayList
                    End If

                    If objBabitMasterRetailTarget.SpecialCategoryFlag = 0 Then
                        intSubCategoryVehicleID2 = 0
                    End If

                    intDealerID = intDealerID2
                    intDealerBranchID = intDealerBranchID2
                    intCategoryID = intCategoryID2
                    intSubCategoryVehicleID = intSubCategoryVehicleID2
                    strSpecialCategoryFlag = strSpecialCategoryFlag2
                    strYearPeriod = strYearPeriod2
                    intQuarterPeriod = intQuarterPeriod2
                    dblAllocationBabit = 0

                    Dim arrBBH As ArrayList = GetDataBabitBudgetHeader(intDealerID, intDealerBranchID, intCategoryID, intSubCategoryVehicleID, intQuarterPeriod, strYearPeriod)
                    If Not IsNothing(arrBBH) And arrBBH.Count > 0 Then
                        mode = "Edit"
                        objBabitBudgetHeader = CType(arrBBH(0), BabitBudgetHeader)
                    Else
                        mode = "New"
                        objBabitBudgetHeader = New BabitBudgetHeader
                    End If
                    With objBabitBudgetHeader
                        .Dealer = objBabitMasterRetailTarget.Dealer
                        .DealerBranch = New DealerBranchFacade(User).Retrieve(intDealerBranchID)
                        .Category = objBabitMasterRetailTarget.SubCategoryVehicle.Category
                        .QuarterPeriod = intQuarterPeriod
                        .YearPeriod = strYearPeriod
                        .SubCategoryVehicleID = intSubCategoryVehicleID
                        .SubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(CType(intSubCategoryVehicleID, Short))
                    End With
                    If mode = "Edit" Then
                        Dim crits4 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitBudgetDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crits4.opAnd(New Criteria(GetType(BabitBudgetDetail), "BabitBudgetHeader.ID", MatchType.Exact, objBabitBudgetHeader.ID))
                        Dim arrBBD As ArrayList = New BabitBudgetDetailFacade(User).Retrieve(crits4)
                        If Not IsNothing(arrBBD) AndAlso arrBBD.Count > 0 Then
                            arrDelBabitBudgetDetail = arrBBD
                        End If
                    End If
                End If

                objBabitBudgetDetail = New BabitBudgetDetail
                If mode = "Edit" Then
                    Dim crits5 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitBudgetDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits5.opAnd(New Criteria(GetType(BabitBudgetDetail), "BabitBudgetHeader.ID", MatchType.Exact, objBabitBudgetHeader.ID))
                    crits5.opAnd(New Criteria(GetType(BabitBudgetDetail), "BabitMasterRetailTarget.ID", MatchType.Exact, objBabitMasterRetailTarget.ID))
                    crits5.opAnd(New Criteria(GetType(BabitBudgetDetail), "BabitMasterPrice.ID", MatchType.Exact, objBabitMasterRetailTarget.BabitMasterPriceID))
                    Dim arrBBD As ArrayList = New BabitBudgetDetailFacade(User).Retrieve(crits5)
                    If Not IsNothing(arrBBD) AndAlso arrBBD.Count > 0 Then
                        objBabitBudgetDetail = CType(arrBBD(0), BabitBudgetDetail)
                    End If
                End If
                With objBabitBudgetDetail
                    .BabitMasterRetailTarget = objBabitMasterRetailTarget
                    .BabitMasterPrice = New BabitMasterPriceFacade(User).Retrieve(objBabitMasterRetailTarget.BabitMasterPriceID)
                    .TotalAmount = objBabitMasterRetailTarget.UnitPrice * objBabitMasterRetailTarget.RetailTarget
                    dblAllocationBabit += .TotalAmount
                End With

                arrBabitBudgetDetail.Add(objBabitBudgetDetail)
            Next

            Dim _result2 As Integer = 0
            Dim arrBBH2 As ArrayList = GetDataBabitBudgetHeader(intDealerID2, intDealerBranchID2, intCategoryID2, intSubCategoryVehicleID2, intQuarterPeriod2, strYearPeriod2)
            If Not IsNothing(arrBBH2) And arrBBH2.Count > 0 Then
                objBabitBudgetHeader = CType(arrBBH2(0), BabitBudgetHeader)
                objBabitBudgetHeader.AllocationBabit = dblAllocationBabit
                objBabitBudgetHeader.TotalAllocationBabit = dblAllocationBabit + objBabitBudgetHeader.AdditionalPrice
                _result2 = New BabitBudgetDetailFacade(User).UpdateTransaction(objBabitBudgetHeader, arrBabitBudgetDetail, arrDelBabitBudgetDetail)
            Else
                objBabitBudgetHeader.AllocationBabit = dblAllocationBabit
                objBabitBudgetHeader.TotalAllocationBabit = dblAllocationBabit + objBabitBudgetHeader.AdditionalPrice
                _result2 = New BabitBudgetDetailFacade(User).InsertTransaction(objBabitBudgetHeader, arrBabitBudgetDetail)
            End If

            MessageBox.Show("Rekalkulasi Data Sukses")
        Else
            MessageBox.Show("Rekalkulasi Data Gagal")
        End If

        ClearData()
        ReadData()   '-- Read all data matching criteria
        BindGrid(dgListBabitMasterRetailTarget.CurrentPageIndex)  '-- Bind page-1
    End Sub

    Function GetDataBabitBudgetHeader(ByVal intDealerID As Integer, ByVal intDealerBranchID As Integer, ByVal intCategoryID As Integer, ByVal intSubCategoryVehicleID As Integer, ByVal intQuarterPeriod As Integer, ByVal strYearPeriod As String) As ArrayList

        Dim crits3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitBudgetHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits3.opAnd(New Criteria(GetType(BabitBudgetHeader), "Dealer.ID", MatchType.Exact, intDealerID))
        If intDealerBranchID > 0 Then
            crits3.opAnd(New Criteria(GetType(BabitBudgetHeader), "DealerBranch.ID", MatchType.Exact, intDealerBranchID))
        End If
        crits3.opAnd(New Criteria(GetType(BabitBudgetHeader), "Category.ID", MatchType.Exact, intCategoryID))
        If intSubCategoryVehicleID = 0 Then
            Dim strSQL As String = "Select ID From BabitBudgetHeader Where RowStatus = 0 and SubCategoryVehicleID = 0"
            crits3.opAnd(New Criteria(GetType(BabitBudgetHeader), "ID", MatchType.InSet, "(" & strSQL & ")"))
        Else
            crits3.opAnd(New Criteria(GetType(BabitBudgetHeader), "SubCategoryVehicle.ID", MatchType.Exact, intSubCategoryVehicleID))
        End If
        crits3.opAnd(New Criteria(GetType(BabitBudgetHeader), "QuarterPeriod", MatchType.Exact, intQuarterPeriod))
        crits3.opAnd(New Criteria(GetType(BabitBudgetHeader), "YearPeriod", MatchType.Exact, strYearPeriod))
        Dim arrBBH As ArrayList = New BabitBudgetHeaderFacade(User).Retrieve(crits3)

        Return arrBBH
    End Function

    Private Sub ClearData()
        txtKodeDealer.Text = ""
        hdnDealer.Value = ""
        lblKodeDealer.Text = ""

        txtKodeTempOut.Text = ""
        'hdnTempOut.Value = ""
        ddlCategory.SelectedIndex = 0
        ddlModel.SelectedIndex = 0
        ddlPeriodeMonth.SelectedIndex = 0
        ddlPeriodeYear.SelectedIndex = 0
        ddlBabitAllocationType.SelectedIndex = 0
    End Sub
End Class