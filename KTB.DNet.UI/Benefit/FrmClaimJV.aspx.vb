Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessValidation.Helpers
Imports System.Linq
Imports System.Text
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class FrmClaimJV
    Inherits System.Web.UI.Page

    Private oDealer As Dealer
    Private sessHelper As New SessionHelper
    Private Mode As String = ""
    Private oBenefitClaimReceipt As BenefitClaimReceipt
    Private vsID As String = "VS_ID"
    Private vsMode As String = "VS_MODE"
    Private arrClaimJV As New ArrayList
    Const sessClaimJV As String = "FrmClaimJV.sessClaimJV"
    Const sessDeleteClaimJV As String = "FrmClaimJV.sessDeleteClaimJV"
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Authorization()
        PageInit()

        oBenefitClaimReceipt = New BenefitClaimReceiptFacade(User).Retrieve(CInt(ViewState(vsID)))
        Mode = ViewState(vsMode)

        If oBenefitClaimReceipt.Status > 0 Then  'status selain Baru
            icProcessDate.Enabled = False
        End If

        If Not IsPostBack Then
            sessHelper.SetSession(sessDeleteClaimJV, New ArrayList)
            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            BindData()
            BindGrid(0)

            If Mode = "Detail" Then
                btnSave.Visible = False
                icProcessDate.Enabled = False
                dgListJV.ShowFooter = False
                dgListJV.Columns(dgListJV.Columns.Count - 1).Visible = False
            End If
            If oBenefitClaimReceipt.BenefitClaimHeader.Status <> 2 And oBenefitClaimReceipt.Status <> 0 Then
                btnSave.Visible = False
                dgListJV.ShowFooter = False
                dgListJV.Columns(dgListJV.Columns.Count - 1).Visible = False
            End If
        End If
    End Sub

    Private Sub Authorization()
        'If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    lblRefReceipt.Visible = False
        '    lblRefText.Visible = False
        '    'ddlNoRek.Visible = False
        '    lblNoRek.Visible = True
        'Else
        '    lblRefReceipt.Visible = True
        '    lblRefText.Visible = True
        '    'ddlNoRek.Visible = True
        '    lblNoRek.Visible = True
        'End If
    End Sub

    Private Sub PageInit()
        ViewState(vsID) = Request.QueryString("BenefitClaimReceiptID")
        ViewState(vsMode) = Request.QueryString("Mode")
    End Sub

    Private Sub BindData()
        oBenefitClaimReceipt.BenefitClaimHeader.HasReceipt = True
        lblDealer.Text = oBenefitClaimReceipt.BenefitClaimHeader.Dealer.DealerCode & " / " & oDealer.DealerName
        lblReceiptNo.Text = oBenefitClaimReceipt.ReceiptNo
        If IsNothing(oBenefitClaimReceipt.DealerBankAccount) Then
            lblNoRek.Text = ""
        Else
            lblNoRek.Text = oBenefitClaimReceipt.DealerBankAccount.BankName & " / " & oBenefitClaimReceipt.DealerBankAccount.BankAccount
        End If

        Dim index% = 0
        Dim model As String = ""
        Dim strTextRefNo As String = String.Empty
        For Each cat As BenefitClaimDetails In oBenefitClaimReceipt.BenefitClaimHeader.BenefitClaimDetailss
            model = cat.ChassisMaster.VechileColor.VechileType.VechileModel.Description
        Next
        Dim arrBenefitClaimJV As ArrayList = New BenefitClaimJVFacade(User).RetrieveList(oBenefitClaimReceipt.BenefitClaimHeader.ID)
        For Each item As BenefitClaimJV In arrBenefitClaimJV
            If Not item Is Nothing Then
                If index > 0 Then Exit For

                If oBenefitClaimReceipt.BenefitClaimHeader.BenefitType.LeasingBox = 1 Then
                    If Not item.Month = Nothing Then
                        strTextRefNo = "Insentif Pembayaran " & model & " " & oBenefitClaimReceipt.BenefitClaimHeader.BenefitClaimDetailss.Count().ToString() & " Unit" _
                                 & " Periode " & getmonthDesc(item.Month)
                    Else
                        strTextRefNo = "Insentif Pembayaran " & model & " " & oBenefitClaimReceipt.BenefitClaimHeader.BenefitClaimDetailss.Count().ToString() & " Unit"
                    End If
                Else
                    If Not item.Month = Nothing Then
                        strTextRefNo = "Komisi Penjualan " & model & " " & oBenefitClaimReceipt.BenefitClaimHeader.BenefitClaimDetailss.Count().ToString() & " Unit" _
                                & " Periode " & getmonthDesc(item.Month)
                    Else
                        strTextRefNo = "Komisi Penjualan " & model & " " & oBenefitClaimReceipt.BenefitClaimHeader.BenefitClaimDetailss.Count().ToString() & " Unit"
                    End If
                End If

                index = index + 1
            End If
        Next
        lblRefText.Text = strTextRefNo
        icProcessDate.Enabled = False
        If (oBenefitClaimReceipt.Status = 0) Then
            lblStatus.Text = "Baru"
            icProcessDate.Enabled = True
        ElseIf (oBenefitClaimReceipt.Status = 1) Then
            lblStatus.Text = "Proses JV"
        ElseIf (oBenefitClaimReceipt.Status = 2) Then
            lblStatus.Text = "Selesai"
        End If
        icProcessDate.Value = Now

        Dim total As Decimal = 0, temptotal As Decimal = 0, nilaiPph As Decimal = 0, nilaiPpn As Decimal = 0
        For Each item As BenefitClaimDetails In oBenefitClaimReceipt.BenefitClaimHeader.BenefitClaimDetailss
            If item.DetailStatus = 1 Then
                total += item.BenefitMasterDetail.Amount
            End If
        Next
        temptotal = total

        Dim pphVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(oBenefitClaimReceipt.ReceiptDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
        Dim ppnVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(oBenefitClaimReceipt.ReceiptDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        If oBenefitClaimReceipt.BenefitClaimHeader.BenefitType.LeasingBox = 1 Then
            'nilaiPph = Math.Round(((temptotal / (1 - 0.15)) - temptotal))
            'nilaiPpn = Math.Round((0.1 * (nilaiPph + temptotal)))
            nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.Total, pphVal, total:=temptotal)
            nilaiPpn = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=CalcHelper.DPPCalculation(pphVal, temptotal))
            total = total + nilaiPph
        Else
            'nilaiPph = Math.Round(((temptotal / (1 - 0.15)) - temptotal))
            'nilaiPph = Math.Round(0.15 * temptotal)
            nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pphVal, dpp:=temptotal)
            nilaiPpn = 0 'Math.Round(0.1 * temptotal)
        End If


        lblTotalPencairan.Text = total.ToString("#,##0")
        lblPPh.Text = nilaiPph.ToString("#,##0")
        lblVat.Text = nilaiPpn.ToString("#,##0")
        lblDPP.Text = total.ToString("#,##0")

        Dim crit As New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader.ID", MatchType.Exact, oBenefitClaimReceipt.BenefitClaimHeader.ID))
        crit.opAnd(New Criteria(GetType(BenefitClaimJV), "TipeAccount", MatchType.No, "D"))

        Dim i% = 0
        arrClaimJV = New BenefitClaimJVFacade(User).Retrieve(crit)
        For Each obj As BenefitClaimJV In arrClaimJV
            i += 1
            obj.NoBaris = i
        Next

        sessHelper.SetSession(sessClaimJV, arrClaimJV)
        BindGrid(0)

    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim totalRec As Double = 0
        arrClaimJV = CType(sessHelper.GetSession(sessClaimJV), ArrayList)
        If IsNothing(arrClaimJV) Then
            arrClaimJV = New ArrayList()
        Else
            totalRec = arrClaimJV.Count
        End If

        CommonFunction.SortListControl(arrClaimJV, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
        Dim PagedList As ArrayList = ArrayListPager.DoPage(arrClaimJV, pageIndex, dgListJV.PageSize)

        If IsNothing(PagedList) Then
            PagedList = New ArrayList()
        End If
        dgListJV.CurrentPageIndex = pageIndex
        dgListJV.DataSource = PagedList
        dgListJV.VirtualItemCount = totalRec
        dgListJV.DataBind()
        sessHelper.SetSession(sessClaimJV, PagedList)
    End Sub

    Public Sub ddlDebitCreditIndicatorGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlDebitCreditIndicatorGrid As DropDownList = sender
        Dim gridItem As DataGridItem = ddlDebitCreditIndicatorGrid.Parent.Parent
        Dim ddlVechileTypeCodeGrid As DropDownList
        Dim ddlCostCenterGrid As DropDownList
        Dim ddlAccrualGrid As DropDownList
        Dim ddlMonthGrid As DropDownList
        Dim txtAmountGrid As TextBox
        Dim lblAmountGrid As Label

        If gridItem.DataSetIndex > -1 Then
            ddlVechileTypeCodeGrid = gridItem.FindControl("ddlVechileTypeCodeEditGrid")
            ddlCostCenterGrid = gridItem.FindControl("ddlCostCenterEditGrid")
            ddlAccrualGrid = gridItem.FindControl("ddlAccrualEditGrid")
            ddlMonthGrid = gridItem.FindControl("ddlMonthEditGrid")
            txtAmountGrid = gridItem.FindControl("txtAmountEditGrid")
            lblAmountGrid = gridItem.FindControl("lblAmountEditGrid")
            Exit Sub
        Else
            ddlVechileTypeCodeGrid = gridItem.FindControl("ddlVechileTypeCodeGrid")
            ddlCostCenterGrid = gridItem.FindControl("ddlCostCenterGrid")
            ddlAccrualGrid = gridItem.FindControl("ddlAccrualGrid")
            ddlMonthGrid = gridItem.FindControl("ddlMonthGrid")
            txtAmountGrid = gridItem.FindControl("txtAmountGrid")
            lblAmountGrid = gridItem.FindControl("lblAmountGrid")
        End If

        If ddlDebitCreditIndicatorGrid.SelectedValue = "1" Then '---> Credit
            txtAmountGrid.Visible = True
            lblAmountGrid.Visible = False
            txtAmountGrid.Text = 0
        Else
            txtAmountGrid.Visible = False
            lblAmountGrid.Visible = True
            lblAmountGrid.Text = 0
        End If

        ddlVechileTypeCodeGrid.Items.Clear()
        If ddlDebitCreditIndicatorGrid.SelectedIndex > 0 Then
            Dim strSql As String = String.Empty
            strSql = "select distinct g.ID"
            strSql += " from BenefitClaimDetails a"
            strSql += " join ChassisMaster b on b.ID = a.ChassisMasterID"
            strSql += " join VechileColor c on c.ID = b.VechileColorID"
            strSql += " join VechileType d on d.ID = c.VechileTypeID"
            strSql += " join VechileModel e on e.ID = d.ModelID"
            strSql += " join SubCategoryVehicleToModel f on f.VechileModelID = e.ID"
            strSql += " join SubCategoryVehicle g on g.id = f.SubCategoryVehicleID"
            strSql += " where"
            strSql += " a.RowStatus = 0"
            strSql += " and b.RowStatus = 0"
            strSql += " and c.RowStatus = 0"
            strSql += " and d.RowStatus = 0"
            strSql += " and e.RowStatus = 0"
            strSql += " and f.RowStatus = 0"
            strSql += " and g.RowStatus = 0"
            strSql += " and a.BenefitClaimHeaderID =" & oBenefitClaimReceipt.BenefitClaimHeader.ID

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If ddlDebitCreditIndicatorGrid.SelectedValue = "1" Then '---> Credit
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.InSet, "(1,2)"))
            Else
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "ID", MatchType.InSet, "(" & strSql & ")"))
            End If
            Dim arrSubCategoryVehicle As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(criterias)
            For Each obj As SubCategoryVehicle In arrSubCategoryVehicle
                With ddlVechileTypeCodeGrid.Items
                    .Add(New ListItem(obj.Name, obj.ID))
                End With
            Next
        End If
        ddlVechileTypeCodeGrid.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        ddlVechileTypeCodeGrid.SelectedIndex = 0

        ddlMonthGrid.Items.Clear()
        If ddlDebitCreditIndicatorGrid.SelectedValue = "1" Then '---> Credit
            GetMonthList(ddlMonthGrid)
        End If

        ddlCostCenterGrid.Items.Clear()
        ddlCostCenterGrid.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
        ddlCostCenterGrid.SelectedIndex = 0

        ddlAccrualGrid.Items.Clear()
        ddlAccrualGrid.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
        ddlAccrualGrid.SelectedIndex = 0

        If ddlDebitCreditIndicatorGrid.SelectedValue = "E" Then
            ddlAccrualGrid.Enabled = False
            ddlAccrualGrid.SelectedIndex = 0
        Else
            ddlAccrualGrid.Enabled = True
        End If
    End Sub

    Public Sub ddlTipeAccountGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlTipeAccountGrid As DropDownList = sender
        Dim gridItem As DataGridItem = ddlTipeAccountGrid.Parent.Parent
        Dim ddlDebitCreditIndicatorGrid As DropDownList
        Dim ddlVechileTypeCodeGrid As DropDownList
        Dim ddlCostCenterGrid As DropDownList
        Dim ddlAccrualGrid As DropDownList
        Dim ddlMonthGrid As DropDownList
        Dim txtAmountGrid As TextBox
        Dim lblAmountGrid As Label

        If gridItem.DataSetIndex > -1 Then
            ddlDebitCreditIndicatorGrid = gridItem.FindControl("ddlDebitCreditIndicatorEditGrid")
            ddlVechileTypeCodeGrid = gridItem.FindControl("ddlVechileTypeCodeEditGrid")
            ddlCostCenterGrid = gridItem.FindControl("ddlCostCenterEditGrid")
            ddlAccrualGrid = gridItem.FindControl("ddlAccrualEditGrid")
            ddlMonthGrid = gridItem.FindControl("ddlMonthEditGrid")
            txtAmountGrid = gridItem.FindControl("txtAmountEditGrid")
            lblAmountGrid = gridItem.FindControl("lblAmountEditGrid")
            Exit Sub
        Else
            ddlDebitCreditIndicatorGrid = gridItem.FindControl("ddlDebitCreditIndicatorGrid")
            ddlVechileTypeCodeGrid = gridItem.FindControl("ddlVechileTypeCodeGrid")
            ddlCostCenterGrid = gridItem.FindControl("ddlCostCenterGrid")
            ddlAccrualGrid = gridItem.FindControl("ddlAccrualGrid")
            ddlMonthGrid = gridItem.FindControl("ddlMonthGrid")
            txtAmountGrid = gridItem.FindControl("txtAmountGrid")
            lblAmountGrid = gridItem.FindControl("lblAmountGrid")
        End If

        If ddlDebitCreditIndicatorGrid.SelectedValue = "1" Then '---> Credit
            txtAmountGrid.Visible = True
            lblAmountGrid.Visible = False
            txtAmountGrid.Text = 0
        Else
            txtAmountGrid.Visible = False
            lblAmountGrid.Visible = True
            lblAmountGrid.Text = 0
        End If

        ddlVechileTypeCodeGrid.Items.Clear()
        If ddlTipeAccountGrid.SelectedIndex > 0 Then
            Dim strSql As String = String.Empty
            strSql = "select distinct g.ID"
            strSql += " from BenefitClaimDetails a"
            strSql += " join ChassisMaster b on b.ID = a.ChassisMasterID"
            strSql += " join VechileColor c on c.ID = b.VechileColorID"
            strSql += " join VechileType d on d.ID = c.VechileTypeID"
            strSql += " join VechileModel e on e.ID = d.ModelID"
            strSql += " join SubCategoryVehicleToModel f on f.VechileModelID = e.ID"
            strSql += " join SubCategoryVehicle g on g.id = f.SubCategoryVehicleID"
            strSql += " where"
            strSql += " a.RowStatus = 0"
            strSql += " and b.RowStatus = 0"
            strSql += " and c.RowStatus = 0"
            strSql += " and d.RowStatus = 0"
            strSql += " and e.RowStatus = 0"
            strSql += " and f.RowStatus = 0"
            strSql += " and g.RowStatus = 0"
            strSql += " and a.BenefitClaimHeaderID =" & oBenefitClaimReceipt.BenefitClaimHeader.ID

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If ddlDebitCreditIndicatorGrid.SelectedValue = "1" Then '---> Credit
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.InSet, "(1,2)"))
            Else
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "ID", MatchType.InSet, "(" & strSql & ")"))
            End If
            Dim arrSubCategoryVehicle As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(criterias)
            For Each obj As SubCategoryVehicle In arrSubCategoryVehicle
                With ddlVechileTypeCodeGrid.Items
                    .Add(New ListItem(obj.Name, obj.ID))
                End With
            Next
        End If
        ddlVechileTypeCodeGrid.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        ddlVechileTypeCodeGrid.SelectedIndex = 0

        ddlMonthGrid.Items.Clear()
        If ddlDebitCreditIndicatorGrid.SelectedValue = "1" Then '---> Credit
            GetMonthList(ddlMonthGrid)
        End If
        ddlCostCenterGrid.Items.Clear()
        ddlCostCenterGrid.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
        ddlCostCenterGrid.SelectedIndex = 0

        ddlAccrualGrid.Items.Clear()
        ddlAccrualGrid.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
        ddlAccrualGrid.SelectedIndex = 0

        If ddlTipeAccountGrid.SelectedValue = "E" Then
            ddlAccrualGrid.Enabled = False
            ddlAccrualGrid.SelectedIndex = 0
        Else
            ddlAccrualGrid.Enabled = True
        End If
    End Sub

    Public Sub ddlVechileTypeCodeGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlVechileTypeCodeGrid As DropDownList = sender
        Dim gridItem As DataGridItem = ddlVechileTypeCodeGrid.Parent.Parent
        Dim ddlDebitCreditIndicatorGrid As DropDownList
        Dim ddlCostCenterGrid As DropDownList
        Dim ddlAccrualGrid As DropDownList
        Dim ddlMonthGrid As DropDownList
        Dim lblAmountGrid As Label
        Dim txtAmountGrid As TextBox
        Dim lblBusinessAreaGrid As Label

        If gridItem.DataSetIndex > -1 Then
            ddlDebitCreditIndicatorGrid = gridItem.FindControl("ddlDebitCreditIndicatorEditGrid")
            ddlCostCenterGrid = gridItem.FindControl("ddlCostCenterEditGrid")
            ddlAccrualGrid = gridItem.FindControl("ddlAccrualEditGrid")
            ddlMonthGrid = gridItem.FindControl("ddlMonthEditGrid")
            lblAmountGrid = gridItem.FindControl("lblAmountEditGrid")
            txtAmountGrid = gridItem.FindControl("txtAmountEditGrid")
            lblBusinessAreaGrid = gridItem.FindControl("lblBusinessAreaEditGrid")
        Else
            ddlDebitCreditIndicatorGrid = gridItem.FindControl("ddlDebitCreditIndicatorGrid")
            ddlCostCenterGrid = gridItem.FindControl("ddlCostCenterGrid")
            ddlAccrualGrid = gridItem.FindControl("ddlAccrualGrid")
            ddlMonthGrid = gridItem.FindControl("ddlMonthGrid")
            lblAmountGrid = gridItem.FindControl("lblAmountGrid")
            txtAmountGrid = gridItem.FindControl("txtAmountGrid")
            lblBusinessAreaGrid = gridItem.FindControl("lblBusinessAreaGrid")
        End If

        If ddlDebitCreditIndicatorGrid.SelectedValue = "1" Then '---> Credit
            txtAmountGrid.Visible = True
            lblAmountGrid.Visible = False
            txtAmountGrid.Text = 0
        Else
            txtAmountGrid.Visible = False
            lblAmountGrid.Visible = True
            lblAmountGrid.Text = 0
        End If

        Dim ddlCostCenterGridValue As String = ddlCostCenterGrid.SelectedValue
        ddlCostCenterGrid.Items.Clear()
        ddlMonthGrid.Items.Clear()

        If ddlVechileTypeCodeGrid.SelectedIndex > 0 Then
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "SubCategoryVehicle.ID", MatchType.Exact, ddlVechileTypeCodeGrid.SelectedValue))
            criterias2.opAnd(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "MasterCostCenter.BussinessAreaCode", MatchType.Exact, lblBusinessAreaGrid.Text))
            Dim arrMasterCostCentertoSubCategoryVehicle As ArrayList = New MasterCostCentertoSubCategoryVehicleFacade(User).Retrieve(criterias2)
            Dim arrDDL = From obj As MasterCostCentertoSubCategoryVehicle In arrMasterCostCentertoSubCategoryVehicle
                                Group By obj.MasterCostCenter.ID Into Group
                         Select ID
            With ddlCostCenterGrid.Items
                For Each id As Integer In arrDDL
                    Dim obj As MasterCostCenter = New MasterCostCenterFacade(User).Retrieve(CType(id, Short))
                    .Add(New ListItem(obj.CostCenterCode, obj.CostCenterCode))
                Next
            End With

            If ddlDebitCreditIndicatorGrid.SelectedValue = "1" Then '---> Credit
                GetMonthList(ddlMonthGrid)
            Else
                Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias3.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitClaimHeader.ID", MatchType.Exact, oBenefitClaimReceipt.BenefitClaimHeader.ID))
                Dim strSql As String = String.Empty
                strSql = "select distinct a.VechileModelID"
                strSql += " from SubCategoryVehicleToModel a"
                strSql += " join SubCategoryVehicle b on b.id = a.SubCategoryVehicleID"
                strSql += " where"
                strSql += " a.RowStatus = 0"
                strSql += " and b.RowStatus = 0"
                strSql += " and b.ID =" & ddlVechileTypeCodeGrid.SelectedValue

                'GetMonthList(ddlMonthGrid)
                criterias3.opAnd(New Criteria(GetType(BenefitClaimDetails), "ChassisMaster.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
                Dim arrBenefitClaimDetails As ArrayList = New BenefitClaimDetailsFacade(User).Retrieve(criterias3)
                Dim arrL = From obj As BenefitClaimDetails In arrBenefitClaimDetails
                                    Group By obj.ChassisMaster.EndCustomer.ValidateTime.Month Into Group
                             Select Month

                Dim strMonthInset As String = ""
                ddlMonthGrid.Items.Clear()
                For Each _month As Short In arrL
                    Dim item As New ListItem
                    Select Case _month
                        Case 1
                            item = New ListItem("Januari", 1)
                        Case 2
                            item = New ListItem("Februari", 2)
                        Case 3
                            item = New ListItem("Maret", 3)
                        Case 4
                            item = New ListItem("April", 4)
                        Case 5
                            item = New ListItem("Mei", 5)
                        Case 6
                            item = New ListItem("Juni", 6)
                        Case 7
                            item = New ListItem("Juli", 7)
                        Case 8
                            item = New ListItem("Agustus", 8)
                        Case 9
                            item = New ListItem("September", 9)
                        Case 10
                            item = New ListItem("Oktober", 10)
                        Case 11
                            item = New ListItem("November", 11)
                        Case 12
                            item = New ListItem("Desember", 12)
                    End Select
                    If strMonthInset = "" Then
                        strMonthInset = _month.ToString().Trim
                    Else
                        strMonthInset += "," & _month.ToString().Trim
                    End If
                    item.Selected = False
                    ddlMonthGrid.Items.Add(item)
                    ddlMonthGrid.SelectedValue = _month
                Next
            End If

            If ddlMonthGrid.Items.Count > 0 Then
                ddlMonthGrid_SelectedIndexChanged(ddlMonthGrid, Nothing)
            End If

            ddlCostCenterGrid.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            If ddlCostCenterGridValue <> "-1" Then
                Try
                    ddlCostCenterGrid.SelectedValue = ddlCostCenterGridValue
                Catch
                    ddlCostCenterGrid.SelectedIndex = 0
                End Try
            Else
                ddlCostCenterGrid.SelectedIndex = 0
            End If
        Else
            ddlCostCenterGrid.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            ddlCostCenterGrid.SelectedIndex = 0
        End If
        ddlCostCenterGrid_SelectedIndexChanged(ddlCostCenterGrid, Nothing)

    End Sub

    Public Sub ddlMonthGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlMonthGrid As DropDownList = sender
        Dim gridItem As DataGridItem = ddlMonthGrid.Parent.Parent
        Dim ddlVechileTypeCodeGrid As DropDownList
        Dim lblAmountGrid As Label
        Dim txtAmountGrid As TextBox
        Dim ddlDebitCreditIndicatorGrid As DropDownList

        If gridItem.DataSetIndex > -1 Then
            ddlDebitCreditIndicatorGrid = gridItem.FindControl("ddlDebitCreditIndicatorEditGrid")
            ddlVechileTypeCodeGrid = gridItem.FindControl("ddlVechileTypeCodeEditGrid")
            lblAmountGrid = gridItem.FindControl("lblAmountEditGrid")
            txtAmountGrid = gridItem.FindControl("txtAmountEditGrid")
        Else
            ddlDebitCreditIndicatorGrid = gridItem.FindControl("ddlDebitCreditIndicatorGrid")
            ddlVechileTypeCodeGrid = gridItem.FindControl("ddlVechileTypeCodeGrid")
            lblAmountGrid = gridItem.FindControl("lblAmountGrid")
            txtAmountGrid = gridItem.FindControl("txtAmountGrid")
        End If

        If ddlDebitCreditIndicatorGrid.SelectedValue = "1" Then '---> Credit
            txtAmountGrid.Visible = True
            lblAmountGrid.Visible = False
            txtAmountGrid.Text = 0
        Else
            txtAmountGrid.Visible = False
            lblAmountGrid.Visible = True
            lblAmountGrid.Text = 0
        End If
        If ddlVechileTypeCodeGrid.SelectedIndex > 0 Then
            Dim criterias4 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias4.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitClaimHeader.ID", MatchType.Exact, oBenefitClaimReceipt.BenefitClaimHeader.ID))
            criterias4.opAnd(New Criteria(GetType(BenefitClaimDetails), "DetailStatus", MatchType.Exact, 1))

            Dim strSql As String = String.Empty
            strSql = "select distinct a.VechileModelID"
            strSql += " from SubCategoryVehicleToModel a"
            strSql += " join SubCategoryVehicle b on b.id = a.SubCategoryVehicleID"
            strSql += " where"
            strSql += " a.RowStatus = 0"
            strSql += " and b.RowStatus = 0"
            strSql += " and b.ID =" & ddlVechileTypeCodeGrid.SelectedValue

            criterias4.opAnd(New Criteria(GetType(BenefitClaimDetails), "ChassisMaster.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
            Dim arrBCD As ArrayList = New BenefitClaimDetailsFacade(User).Retrieve(criterias4)

            Dim pphVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(oBenefitClaimReceipt.ReceiptDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)

            Dim dblAmount As Double = 0
            For Each obj As BenefitClaimDetails In arrBCD
                If obj.ChassisMaster.EndCustomer.ValidateTime.Month = ddlMonthGrid.SelectedValue Then
                    Dim dblDPP As Double = 0
                    If obj.BenefitClaimHeader.BenefitType.LeasingBox = 1 Then
                        'dblDPP = Math.Round(IsDBNull(obj.BenefitMasterDetail.Amount, 0) / (1 - 0.14999999999999999))
                        dblDPP = CalcHelper.DPPCalculation(pphVal, IsDBNull(obj.BenefitMasterDetail.Amount, 0))
                    Else
                        dblDPP = Math.Round(IsDBNull(obj.BenefitMasterDetail.Amount, 0))
                    End If
                    dblAmount += dblDPP
                End If
            Next
            If ddlDebitCreditIndicatorGrid.SelectedValue = "1" Then '---> Credit
                txtAmountGrid.Text = dblAmount.ToString("#,##0")
            Else
                lblAmountGrid.Text = dblAmount.ToString("#,##0")
            End If
        End If
    End Sub

    Private Function IsDBNull(ByVal obj As Object, ByVal replacement As Decimal) As Decimal
        If obj Is DBNull.Value Then
            Return replacement
        End If
        Return CType(obj, Decimal)
    End Function

    Public Sub ddlCostCenterGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlCostCenterGrid As DropDownList = sender
        Dim gridItem As DataGridItem = ddlCostCenterGrid.Parent.Parent
        Dim ddlAccrualGrid As DropDownList
        Dim lblBusinessAreaGrid As Label
        Dim ddlTipeAccountGrid As DropDownList
        If gridItem.DataSetIndex > -1 Then
            ddlAccrualGrid = gridItem.FindControl("ddlAccrualEditGrid")
            lblBusinessAreaGrid = gridItem.FindControl("lblBusinessAreaEditGrid")
            ddlTipeAccountGrid = gridItem.FindControl("ddlTipeAccountEditGrid")
        Else
            ddlAccrualGrid = gridItem.FindControl("ddlAccrualGrid")
            lblBusinessAreaGrid = gridItem.FindControl("lblBusinessAreaGrid")
            ddlTipeAccountGrid = gridItem.FindControl("ddlTipeAccountGrid")
        End If

        Dim ddlAccrualGridValue As Decimal = ddlAccrualGrid.SelectedValue
        ddlAccrualGrid.Items.Clear()
        If ddlCostCenterGrid.SelectedIndex > 0 Then
            Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MasterAccrued), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(MasterAccrued), "BussinessAreaCode", MatchType.Exact, lblBusinessAreaGrid.Text))
            criterias3.opAnd(New Criteria(GetType(MasterAccrued), "MasterCostCenter.CostCenterCode", MatchType.Exact, ddlCostCenterGrid.SelectedValue))
            Dim arrMasterAccrued As ArrayList = New MasterAccruedFacade(User).Retrieve(criterias3)
            With ddlAccrualGrid.Items
                For Each obj As MasterAccrued In arrMasterAccrued
                    .Add(New ListItem(obj.AccKey, obj.ID))
                Next
            End With

            ddlAccrualGrid.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            If ddlAccrualGridValue <> -1 Then
                Try
                    ddlAccrualGrid.SelectedValue = ddlAccrualGridValue
                Catch
                    ddlAccrualGrid.SelectedIndex = 0
                End Try
            Else
                ddlAccrualGrid.SelectedIndex = 0
            End If
        Else
            ddlAccrualGrid.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            ddlAccrualGrid.SelectedIndex = 0
        End If

        If ddlTipeAccountGrid.SelectedValue = "E" Then
            ddlAccrualGrid.Enabled = False
            ddlAccrualGrid.SelectedIndex = 0
        Else
            ddlAccrualGrid.Enabled = True
        End If
    End Sub

    Private Sub dgListJV_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgListJV.ItemCommand
        Dim ddlDebitCreditIndicatorGrid As DropDownList
        Dim ddlDebitCreditIndicatorEditGrid As DropDownList
        Dim lblDebitCreditIndicatorGrid As Label

        Dim ddlTipeAccountGrid As DropDownList
        Dim ddlTipeAccountEditGrid As DropDownList
        Dim lblTipeAccount As Label

        Dim lblBusinessAreaEditGrid As Label
        Dim lblBusinessAreaGrid As Label
        Dim lblBusinessArea As Label

        Dim ddlVechileTypeCodeGrid As DropDownList
        Dim ddlVechileTypeCodeEditGrid As DropDownList
        Dim lblVechileTypeCode As Label

        Dim ddlCostCenterGrid As DropDownList
        Dim ddlCostCenterEditGrid As DropDownList
        Dim lblCostCenter As Label

        Dim lblAmountGrid As Label
        Dim lblAmountEditGrid As Label
        Dim lblAmount As Label

        Dim txtAmountGrid As TextBox
        Dim txtAmountEditGrid As TextBox

        Dim ddlMonthGrid As DropDownList
        Dim ddlMonthEditGrid As DropDownList
        Dim ddlMonth As Label

        Dim ddlAccrualGrid As DropDownList
        Dim ddlAccrualEditGrid As DropDownList
        Dim lblAccrual As Label

        Dim txtRemarksGrid As TextBox
        Dim txtRemarksEditGrid As TextBox
        Dim lblRemarks As Label

        arrClaimJV = CType(sessHelper.GetSession(sessClaimJV), ArrayList)

        Select Case e.CommandName
            Case "Add"
                ddlDebitCreditIndicatorGrid = CType(e.Item.FindControl("ddlDebitCreditIndicatorGrid"), DropDownList)
                ddlTipeAccountGrid = CType(e.Item.FindControl("ddlTipeAccountGrid"), DropDownList)
                ddlVechileTypeCodeGrid = CType(e.Item.FindControl("ddlVechileTypeCodeGrid"), DropDownList)
                ddlCostCenterGrid = CType(e.Item.FindControl("ddlCostCenterGrid"), DropDownList)
                ddlAccrualGrid = CType(e.Item.FindControl("ddlAccrualGrid"), DropDownList)
                lblBusinessAreaGrid = CType(e.Item.FindControl("lblBusinessAreaGrid"), Label)
                lblAmountGrid = CType(e.Item.FindControl("lblAmountGrid"), Label)
                txtAmountGrid = CType(e.Item.FindControl("txtAmountGrid"), TextBox)
                ddlMonthGrid = CType(e.Item.FindControl("ddlMonthGrid"), DropDownList)
                txtRemarksGrid = CType(e.Item.FindControl("txtRemarksGrid"), TextBox)

                If ddlDebitCreditIndicatorGrid.SelectedIndex = 0 Then
                    MessageBox.Show("Debit/ Credit Indicator harus dipilih.")
                    Return
                End If
                If ddlTipeAccountGrid.SelectedIndex = 0 Then
                    MessageBox.Show("Account harus dipilih.")
                    Return
                End If
                If ddlVechileTypeCodeGrid.SelectedIndex = 0 Then
                    MessageBox.Show("Tipe Kendaraan harus dipilih.")
                    Return
                End If
                If ddlCostCenterGrid.SelectedIndex = 0 Then
                    MessageBox.Show("Cost Center harus dipilih.")
                    Return
                End If
                If ddlTipeAccountGrid.SelectedValue <> "E" Then
                    If ddlAccrualGrid.SelectedIndex = 0 Then
                        MessageBox.Show("Accrual harus dipilih.")
                        Return
                    End If
                End If
                If lblTotalPencairan.Text.Trim = "0" Then
                    MessageBox.Show("Amount Claim masih kosong.")
                    Return
                End If
                If ddlTipeAccountGrid.SelectedValue = "E" Then
                    If txtRemarksGrid.Text.Trim = "" Then
                        MessageBox.Show("Keterangan harus diisi untuk Tipe Account Expense.")
                        Return
                    End If
                End If

                For Each obj As BenefitClaimJV In arrClaimJV
                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "MasterCostCenter.CostCenterCode", MatchType.Exact, obj.CostCenter))
                    Dim arrMasterCostCentertoSubCategoryVehicle2 As ArrayList = New MasterCostCentertoSubCategoryVehicleFacade(User).Retrieve(crit)
                    Dim oMasterCostCentertoSubCategoryVehicle As New MasterCostCentertoSubCategoryVehicle
                    If Not IsNothing(arrMasterCostCentertoSubCategoryVehicle2) AndAlso arrMasterCostCentertoSubCategoryVehicle2.Count > 0 Then
                        oMasterCostCentertoSubCategoryVehicle = CType(arrMasterCostCentertoSubCategoryVehicle2(0), MasterCostCentertoSubCategoryVehicle)
                    End If

                    If ddlVechileTypeCodeGrid.SelectedValue = oMasterCostCentertoSubCategoryVehicle.SubCategoryVehicle.ID Then
                        If ddlMonthGrid.SelectedValue = obj.Month Then
                            MessageBox.Show("Tipe kendaraan, Cost Center dan Month tidak boleh duplikat.")
                            Return
                        End If
                    End If
                Next

                If ddlDebitCreditIndicatorGrid.SelectedValue = "1" Then '---> Credit
                    If txtAmountGrid.Text.Trim = "0" Then
                        MessageBox.Show("Amount tidak boleh 0")
                        Return
                    End If
                End If

                Dim oMasterAccrued As MasterAccrued = New MasterAccruedFacade(User).Retrieve(CInt(ddlAccrualGrid.SelectedValue))
                Dim oBenefitClaimJV As BenefitClaimJV = New BenefitClaimJV
                oBenefitClaimJV.BenefitClaimHeader = oBenefitClaimReceipt.BenefitClaimHeader
                oBenefitClaimJV.TipeAccount = ddlTipeAccountGrid.SelectedValue
                oBenefitClaimJV.BusinessArea = lblBusinessAreaGrid.Text
                If ddlDebitCreditIndicatorGrid.SelectedValue = "1" Then '---> Credit
                    oBenefitClaimJV.Amount = -System.Math.Abs(CDbl(txtAmountGrid.Text))
                Else
                    oBenefitClaimJV.Amount = lblAmountGrid.Text
                End If
                oBenefitClaimJV.CostCenter = ddlCostCenterGrid.SelectedValue
                oBenefitClaimJV.AccuredMount = 0
                oBenefitClaimJV.TipeAccount = ddlTipeAccountGrid.SelectedValue
                oBenefitClaimJV.Month = ddlMonthGrid.SelectedValue
                oBenefitClaimJV.PaymentDate = icProcessDate.Value
                oBenefitClaimJV.VendorID = ""
                oBenefitClaimJV.JVNumber = ""
                oBenefitClaimJV.InternalOrder = "ZC11_43_0014"
                oBenefitClaimJV.MasterAccrued = oMasterAccrued
                oBenefitClaimJV.Remarks = txtRemarksGrid.Text

                oBenefitClaimJV.NoBaris = arrClaimJV.Count + 1

                arrClaimJV.Add(oBenefitClaimJV)

            Case "Save"
                ddlDebitCreditIndicatorEditGrid = CType(e.Item.FindControl("ddlDebitCreditIndicatorEditGrid"), DropDownList)
                ddlTipeAccountEditGrid = CType(e.Item.FindControl("ddlTipeAccountEditGrid"), DropDownList)
                ddlVechileTypeCodeEditGrid = CType(e.Item.FindControl("ddlVechileTypeCodeEditGrid"), DropDownList)
                ddlCostCenterEditGrid = CType(e.Item.FindControl("ddlCostCenterEditGrid"), DropDownList)
                ddlAccrualEditGrid = CType(e.Item.FindControl("ddlAccrualEditGrid"), DropDownList)
                lblBusinessAreaEditGrid = CType(e.Item.FindControl("lblBusinessAreaEditGrid"), Label)
                lblAmountEditGrid = CType(e.Item.FindControl("lblAmountEditGrid"), Label)
                txtAmountEditGrid = CType(e.Item.FindControl("txtAmountEditGrid"), TextBox)
                ddlMonthEditGrid = CType(e.Item.FindControl("ddlMonthEditGrid"), DropDownList)
                txtRemarksEditGrid = CType(e.Item.FindControl("txtRemarksEditGrid"), TextBox)

                If ddlDebitCreditIndicatorEditGrid.SelectedIndex = 0 Then
                    MessageBox.Show("Debit/ Credit Indicator harus dipilih.")
                    Return
                End If
                If ddlTipeAccountEditGrid.SelectedIndex = 0 Then
                    MessageBox.Show("Account harus dipilih.")
                    Return
                End If
                If ddlVechileTypeCodeEditGrid.SelectedIndex = 0 Then
                    MessageBox.Show("Tipe Kendaraan harus dipilih.")
                    Return
                End If
                If ddlCostCenterEditGrid.SelectedIndex = 0 Then
                    MessageBox.Show("Cost Center harus dipilih.")
                    Return
                End If
                If ddlTipeAccountEditGrid.SelectedValue <> "E" Then
                    If ddlAccrualEditGrid.SelectedIndex = 0 Then
                        MessageBox.Show("Accrual harus dipilih.")
                        Return
                    End If
                End If
                If ddlTipeAccountEditGrid.SelectedValue = "E" Then
                    If txtRemarksEditGrid.Text.Trim = "" Then
                        MessageBox.Show("Keterangan harus diisi untuk Tipe Account Expense.")
                        Return
                    End If
                End If
                If ddlDebitCreditIndicatorEditGrid.SelectedValue = "1" Then '---> Credit
                    If txtAmountEditGrid.Text.Trim = "0" Then
                        MessageBox.Show("Amount tidak boleh 0")
                        Return
                    End If
                End If

                Dim oBenefitClaimJV As BenefitClaimJV = CType(arrClaimJV(e.Item.ItemIndex), BenefitClaimJV)

                For Each obj As BenefitClaimJV In arrClaimJV
                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "MasterCostCenter.CostCenterCode", MatchType.Exact, obj.CostCenter))
                    Dim arrMasterCostCentertoSubCategoryVehicle2 As ArrayList = New MasterCostCentertoSubCategoryVehicleFacade(User).Retrieve(crit)
                    Dim oMasterCostCentertoSubCategoryVehicle As New MasterCostCentertoSubCategoryVehicle
                    If Not IsNothing(arrMasterCostCentertoSubCategoryVehicle2) AndAlso arrMasterCostCentertoSubCategoryVehicle2.Count > 0 Then
                        oMasterCostCentertoSubCategoryVehicle = CType(arrMasterCostCentertoSubCategoryVehicle2(0), MasterCostCentertoSubCategoryVehicle)
                    End If

                    If oBenefitClaimJV.NoBaris <> obj.NoBaris Then
                        If ddlVechileTypeCodeEditGrid.SelectedValue = oMasterCostCentertoSubCategoryVehicle.SubCategoryVehicle.ID Then
                            If ddlMonthEditGrid.SelectedValue = obj.Month Then
                                MessageBox.Show("Tipe kendaraan, Cost Center dan Month tidak boleh duplikat.")
                                Return
                            End If
                        End If
                    End If
                Next

                Dim oMasterAccrued As MasterAccrued = New MasterAccruedFacade(User).Retrieve(CInt(ddlAccrualEditGrid.SelectedValue))
                oBenefitClaimJV.BenefitClaimHeader = oBenefitClaimJV.BenefitClaimHeader
                oBenefitClaimJV.TipeAccount = ddlTipeAccountEditGrid.SelectedValue
                oBenefitClaimJV.BusinessArea = lblBusinessAreaEditGrid.Text
                If ddlDebitCreditIndicatorEditGrid.SelectedValue = "1" Then '---> Credit
                    oBenefitClaimJV.Amount = -System.Math.Abs(CDbl(txtAmountEditGrid.Text))
                Else
                    oBenefitClaimJV.Amount = lblAmountEditGrid.Text
                End If
                oBenefitClaimJV.CostCenter = ddlCostCenterEditGrid.SelectedValue
                oBenefitClaimJV.AccuredMount = 0
                oBenefitClaimJV.Month = ddlMonthEditGrid.SelectedValue
                oBenefitClaimJV.PaymentDate = icProcessDate.Value
                oBenefitClaimJV.VendorID = ""
                oBenefitClaimJV.JVNumber = ""
                oBenefitClaimJV.InternalOrder = "ZC11_43_0014"
                oBenefitClaimJV.MasterAccrued = oMasterAccrued
                oBenefitClaimJV.Remarks = txtRemarksEditGrid.Text

                dgListJV.EditItemIndex = -1
                dgListJV.ShowFooter = True

            Case "Edit"
                dgListJV.ShowFooter = False
                dgListJV.EditItemIndex = e.Item.ItemIndex

            Case "Delete"
                Try
                    Dim oBenefitClaimJV As BenefitClaimJV = CType(arrClaimJV(e.Item.ItemIndex), BenefitClaimJV)
                    If oBenefitClaimJV.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteClaimJV), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oBenefitClaimJV)
                        sessHelper.SetSession(sessDeleteClaimJV, arrDelete)
                    End If
                    arrClaimJV.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try

            Case "Cancel"
                dgListJV.EditItemIndex = -1
                dgListJV.ShowFooter = True
        End Select

        sessHelper.SetSession(sessClaimJV, arrClaimJV)
        dgListJV.CurrentPageIndex = 0
        BindGrid(dgListJV.CurrentPageIndex)
    End Sub

    Protected Sub dgListJV_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgListJV.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            Dim ddlDebitCreditIndicatorGrid As DropDownList = CType(e.Item.FindControl("ddlDebitCreditIndicatorGrid"), DropDownList)
            Dim ddlTipeAccountGrid As DropDownList = CType(e.Item.FindControl("ddlTipeAccountGrid"), DropDownList)
            Dim lblBusinessAreaGrid As Label = CType(e.Item.FindControl("lblBusinessAreaGrid"), Label)
            Dim ddlVechileTypeCodeGrid As DropDownList = CType(e.Item.FindControl("ddlVechileTypeCodeGrid"), DropDownList)
            Dim ddlCostCenterGrid As DropDownList = CType(e.Item.FindControl("ddlCostCenterGrid"), DropDownList)
            Dim ddlAccrualGrid As DropDownList = CType(e.Item.FindControl("ddlAccrualGrid"), DropDownList)
            Dim ddlMonthGrid As DropDownList = CType(e.Item.FindControl("ddlMonthGrid"), DropDownList)
            Dim lblAmountGrid As Label = CType(e.Item.FindControl("lblAmountGrid"), Label)
            Dim txtAmountGrid As TextBox = CType(e.Item.FindControl("txtAmountGrid"), TextBox)
            Dim txtRemarksGrid As TextBox = CType(e.Item.FindControl("txtRemarksGrid"), TextBox)

            CommonFunction.BindDDLFromStandartCode("EnumDebitCreditIndicator", ddlDebitCreditIndicatorGrid)

            With ddlTipeAccountGrid
                .Items.Clear()
                .Items.Add(New ListItem("Silahkan Pilih", "-1"))
                .Items.Add(New ListItem("Accrued", "A"))
                .Items.Add(New ListItem("Expense", "E"))
            End With
            ddlTipeAccountGrid.SelectedIndex = 0
            lblBusinessAreaGrid.Text = "ZC11"

            ddlVechileTypeCodeGrid.Items.Clear()
            ddlVechileTypeCodeGrid.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
            ddlVechileTypeCodeGrid.SelectedIndex = 0

            ddlCostCenterGrid.Items.Clear()
            ddlCostCenterGrid.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
            ddlCostCenterGrid.SelectedIndex = 0

            ddlAccrualGrid.Items.Clear()
            ddlAccrualGrid.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
            ddlAccrualGrid.SelectedIndex = 0

            ddlMonthGrid.SelectedIndex = 0
            lblAmountGrid.Text = 0
            txtAmountGrid.Text = 0
            txtRemarksGrid.Text = ""
        End If

        If e.Item.ItemType = ListItemType.EditItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim ddlDebitCreditIndicatorEditGrid As DropDownList = CType(e.Item.FindControl("ddlDebitCreditIndicatorEditGrid"), DropDownList)
            Dim ddlTipeAccountEditGrid As DropDownList = CType(e.Item.FindControl("ddlTipeAccountEditGrid"), DropDownList)
            Dim lblBusinessAreaEditGrid As Label = CType(e.Item.FindControl("lblBusinessAreaEditGrid"), Label)
            Dim ddlVechileTypeCodeEditGrid As DropDownList = CType(e.Item.FindControl("ddlVechileTypeCodeEditGrid"), DropDownList)
            Dim ddlCostCenterEditGrid As DropDownList = CType(e.Item.FindControl("ddlCostCenterEditGrid"), DropDownList)
            Dim ddlAccrualEditGrid As DropDownList = CType(e.Item.FindControl("ddlAccrualEditGrid"), DropDownList)
            Dim lblAmountEditGrid As Label = CType(e.Item.FindControl("lblAmountEditGrid"), Label)
            Dim txtAmountEditGrid As TextBox = CType(e.Item.FindControl("txtAmountEditGrid"), TextBox)
            Dim ddlMonthEditGrid As DropDownList = CType(e.Item.FindControl("ddlMonthEditGrid"), DropDownList)
            Dim txtRemarksEditGrid As TextBox = CType(e.Item.FindControl("txtRemarksEditGrid"), TextBox)

            Dim oBenefitClaimJV As BenefitClaimJV = CType(e.Item.DataItem, BenefitClaimJV)
            lblNo.Text = (dgListJV.PageSize * dgListJV.CurrentPageIndex) + e.Item.ItemIndex + 1

            CommonFunction.BindDDLFromStandartCode("EnumDebitCreditIndicator", ddlDebitCreditIndicatorEditGrid)
            ddlDebitCreditIndicatorEditGrid.SelectedValue = oBenefitClaimJV.DebitCreditIndicatorValue

            With ddlTipeAccountEditGrid
                .Items.Clear()
                .Items.Add(New ListItem("Silahkan Pilih", "-1"))
                .Items.Add(New ListItem("Accrued", "A"))
                .Items.Add(New ListItem("Expense", "E"))
            End With
            ddlTipeAccountEditGrid.SelectedValue = oBenefitClaimJV.TipeAccount
            lblBusinessAreaEditGrid.Text = "ZC11"

            ddlVechileTypeCodeEditGrid.Items.Clear()
            If ddlDebitCreditIndicatorEditGrid.SelectedValue = "1" Then '---> Credit
                Dim criterias5 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias5.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.InSet, "(1,2)"))
                Dim arrSubCategoryVehicle As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(criterias5)
                If Not IsNothing(arrSubCategoryVehicle) AndAlso arrSubCategoryVehicle.Count > 0 Then
                    For Each objSubCategoryVehicle As SubCategoryVehicle In arrSubCategoryVehicle
                        ddlVechileTypeCodeEditGrid.Items.Add(New ListItem(objSubCategoryVehicle.Name, objSubCategoryVehicle.ID))
                    Next
                End If
            Else
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitClaimHeader.ID", MatchType.Exact, oBenefitClaimJV.BenefitClaimHeader.ID))
                Dim arrBenefitClaimDetails As ArrayList = New BenefitClaimDetailsFacade(User).Retrieve(criterias)
                Dim arrDDL2 = From obj As BenefitClaimDetails In arrBenefitClaimDetails
                                    Group By obj.ChassisMaster.VechileColor.VechileType.VechileModel.SubCategoryVehicleToModel.SubCategoryVehicle.ID Into Group
                             Select ID

                For Each id As Short In arrDDL2
                    Dim objSubCategoryVehicle As SubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(id)
                    If Not IsNothing(objSubCategoryVehicle) AndAlso objSubCategoryVehicle.ID > 0 Then
                        ddlVechileTypeCodeEditGrid.Items.Add(New ListItem(objSubCategoryVehicle.Name, objSubCategoryVehicle.ID))
                    End If
                Next
            End If
            ddlVechileTypeCodeEditGrid.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item

            Try
                Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias3.opAnd(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "MasterCostCenter.CostCenterCode", MatchType.Exact, oBenefitClaimJV.CostCenter))
                Dim arrMasterCostCentertoSubCategoryVehicle2 As ArrayList = New MasterCostCentertoSubCategoryVehicleFacade(User).Retrieve(criterias3)
                Dim oMasterCostCentertoSubCategoryVehicle As New MasterCostCentertoSubCategoryVehicle
                If Not IsNothing(arrMasterCostCentertoSubCategoryVehicle2) AndAlso arrMasterCostCentertoSubCategoryVehicle2.Count > 0 Then
                    oMasterCostCentertoSubCategoryVehicle = CType(arrMasterCostCentertoSubCategoryVehicle2(0), MasterCostCentertoSubCategoryVehicle)
                End If
                ddlVechileTypeCodeEditGrid.SelectedValue = oMasterCostCentertoSubCategoryVehicle.SubCategoryVehicle.ID
            Catch
            End Try

            ddlCostCenterEditGrid.Items.Clear()
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "SubCategoryVehicle.ID", MatchType.Exact, ddlVechileTypeCodeEditGrid.SelectedValue))
            Dim arrMasterCostCentertoSubCategoryVehicle As ArrayList = New MasterCostCentertoSubCategoryVehicleFacade(User).Retrieve(criterias2)
            Dim arrDDL = From obj As MasterCostCentertoSubCategoryVehicle In arrMasterCostCentertoSubCategoryVehicle
                                Group By obj.MasterCostCenter.ID Into Group
                         Select ID
            With ddlCostCenterEditGrid.Items
                For Each id As Integer In arrDDL
                    Dim obj As MasterCostCenter = New MasterCostCenterFacade(User).Retrieve(CType(id, Short))
                    .Add(New ListItem(obj.CostCenterCode, obj.CostCenterCode))
                Next
            End With
            ddlCostCenterEditGrid.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
            ddlCostCenterEditGrid.SelectedValue = oBenefitClaimJV.CostCenter

            ddlAccrualEditGrid.Items.Clear()
            Dim criterias4 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MasterAccrued), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias4.opAnd(New Criteria(GetType(MasterAccrued), "BussinessAreaCode", MatchType.Exact, lblBusinessAreaEditGrid.Text))
            criterias4.opAnd(New Criteria(GetType(MasterAccrued), "MasterCostCenter.CostCenterCode", MatchType.Exact, ddlCostCenterEditGrid.SelectedValue))
            Dim arrMasterAccrued As ArrayList = New MasterAccruedFacade(User).Retrieve(criterias4)
            With ddlAccrualEditGrid.Items
                For Each obj As MasterAccrued In arrMasterAccrued
                    .Add(New ListItem(obj.AccKey, obj.ID))
                Next
            End With
            ddlAccrualEditGrid.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
            ddlAccrualEditGrid.SelectedValue = If(Not IsNothing(oBenefitClaimJV.MasterAccrued), oBenefitClaimJV.MasterAccrued.ID, "-1")

            If ddlDebitCreditIndicatorEditGrid.SelectedValue = "1" Then '---> Credit
                GetMonthList(ddlMonthEditGrid)
            Else
                Dim criterias5 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias5.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitClaimHeader.ID", MatchType.Exact, oBenefitClaimJV.BenefitClaimHeader.ID))
                Dim strSql As String = String.Empty
                strSql = "select distinct a.VechileModelID"
                strSql += " from SubCategoryVehicleToModel a"
                strSql += " join SubCategoryVehicle b on b.id = a.SubCategoryVehicleID"
                strSql += " where"
                strSql += " a.RowStatus = 0"
                strSql += " and b.RowStatus = 0"
                strSql += " and b.ID =" & ddlVechileTypeCodeEditGrid.SelectedValue

                criterias5.opAnd(New Criteria(GetType(BenefitClaimDetails), "ChassisMaster.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
                Dim arrBenefitClaimDetails2 As ArrayList = New BenefitClaimDetailsFacade(User).Retrieve(criterias5)
                Dim arrL = From obj As BenefitClaimDetails In arrBenefitClaimDetails2
                                    Group By obj.ChassisMaster.EndCustomer.ValidateTime.Month Into Group
                             Select Month

                ddlMonthEditGrid.Items.Clear()
                For Each _month As Short In arrL
                    Dim item As ListItem = New ListItem("Januari", 1)
                    Select Case _month
                        Case 1
                            item = New ListItem("Januari", 1)
                        Case 2
                            item = New ListItem("Februari", 2)
                        Case 3
                            item = New ListItem("Maret", 3)
                        Case 4
                            item = New ListItem("April", 4)
                        Case 5
                            item = New ListItem("Mei", 5)
                        Case 6
                            item = New ListItem("Juni", 6)
                        Case 7
                            item = New ListItem("Juli", 7)
                        Case 8
                            item = New ListItem("Agustus", 8)
                        Case 9
                            item = New ListItem("September", 9)
                        Case 10
                            item = New ListItem("Oktober", 10)
                        Case 11
                            item = New ListItem("November", 11)
                        Case 12
                            item = New ListItem("Desember", 12)
                    End Select
                    item.Selected = False
                    ddlMonthEditGrid.Items.Add(item)
                Next
            End If
            ddlMonthEditGrid.SelectedValue = oBenefitClaimJV.Month
            If ddlDebitCreditIndicatorEditGrid.SelectedValue = "1" Then '---> Credit
                txtAmountEditGrid.Visible = True
                lblAmountEditGrid.Visible = False
                txtAmountEditGrid.Text = Math.Abs(oBenefitClaimJV.Amount).ToString("#,##0")
            Else
                txtAmountEditGrid.Visible = False
                lblAmountEditGrid.Visible = True
                lblAmountEditGrid.Text = Math.Abs(oBenefitClaimJV.Amount).ToString("#,##0")
            End If

            txtRemarksEditGrid.Text = oBenefitClaimJV.Remarks
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblDebitCreditIndicator As Label = CType(e.Item.FindControl("lblDebitCreditIndicator"), Label)
            Dim lblTipeAccount As Label = CType(e.Item.FindControl("lblTipeAccount"), Label)
            Dim lblBusinessArea As Label = CType(e.Item.FindControl("lblBusinessArea"), Label)
            Dim lblVechileTypeCode As Label = CType(e.Item.FindControl("lblVechileTypeCode"), Label)
            Dim lblCostCenter As Label = CType(e.Item.FindControl("lblCostCenter"), Label)
            Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
            Dim lblAccrual As Label = CType(e.Item.FindControl("lblAccrual"), Label)
            Dim lblMonth As Label = CType(e.Item.FindControl("lblMonth"), Label)
            Dim lblRemarks As Label = CType(e.Item.FindControl("lblRemarks"), Label)

            Dim oBenefitClaimJV As BenefitClaimJV = CType(e.Item.DataItem, BenefitClaimJV)
            lblNo.Text = (dgListJV.PageSize * dgListJV.CurrentPageIndex) + e.Item.ItemIndex + 1

            lblTipeAccount.Text = If(oBenefitClaimJV.TipeAccount = "A", "Accrued", If(oBenefitClaimJV.TipeAccount = "E", "Expense", "Non Tipe"))
            lblBusinessArea.Text = oBenefitClaimJV.BusinessArea
            lblMonth.Text = getmonthDesc(oBenefitClaimJV.Month)
            lblCostCenter.Text = oBenefitClaimJV.CostCenter
            lblAmount.Text = Math.Abs(oBenefitClaimJV.Amount).ToString("#,##0")
            lblAccrual.Text = If(Not IsNothing(oBenefitClaimJV.MasterAccrued), oBenefitClaimJV.MasterAccrued.AccKey, "")
            lblRemarks.Text = oBenefitClaimJV.Remarks

            lblDebitCreditIndicator.Text = CommonFunction.GetEnumDescription(oBenefitClaimJV.DebitCreditIndicatorValue, "EnumDebitCreditIndicator")

            Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(MasterCostCentertoSubCategoryVehicle), "MasterCostCenter.CostCenterCode", MatchType.Exact, oBenefitClaimJV.CostCenter))
            Dim arrMasterCostCentertoSubCategoryVehicle2 As ArrayList = New MasterCostCentertoSubCategoryVehicleFacade(User).Retrieve(criterias3)
            Dim oMasterCostCentertoSubCategoryVehicle As New MasterCostCentertoSubCategoryVehicle
            If Not IsNothing(arrMasterCostCentertoSubCategoryVehicle2) AndAlso arrMasterCostCentertoSubCategoryVehicle2.Count > 0 Then
                oMasterCostCentertoSubCategoryVehicle = CType(arrMasterCostCentertoSubCategoryVehicle2(0), MasterCostCentertoSubCategoryVehicle)
            End If
            lblVechileTypeCode.Text = oMasterCostCentertoSubCategoryVehicle.SubCategoryVehicle.Name
        End If
    End Sub

    Private Function GetMonthList(ddlMonth As DropDownList)
        Try
            ddlMonth.Items.Clear()
            'ddlMonth.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayMonth()
                item.Selected = False
                ddlMonth.Items.Add(item)
            Next
        Catch ex As Exception
            'MessageBox.Show("Error Binding ddlMonth, silahkan kirim error ini ke dnet admin")
        End Try
    End Function

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        Dim tglBayar As New Date(icProcessDate.Value.Year, icProcessDate.Value.Month, icProcessDate.Value.Day, 0, 0, 0)
        Dim tglNow As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day, 0, 0, 0)

        If tglBayar < tglNow Then
            sb.Append("- Tgl Pembayaran kurang dari tanggal hari ini\n")
        End If
        If (sessHelper.GetSession(sessClaimJV) Is Nothing) Then
            sb.Append("- Data Journal Voucher belum ada\n")
        Else
            If CType(sessHelper.GetSession(sessClaimJV), ArrayList).Count = 0 Then
                sb.Append("- Data Journal Voucher belum ada\n")
            End If
        End If

        Return sb.ToString()
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim arlDelClaimJV As New ArrayList
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If

        Dim _BenefitClaimJV As New BenefitClaimJV
        If Mode = "Edit" Then
            Dim crit As New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(BenefitClaimJV), "BenefitClaimHeader.ID", MatchType.Exact, oBenefitClaimReceipt.BenefitClaimHeader.ID))
            crit.opAnd(New Criteria(GetType(BenefitClaimJV), "TipeAccount", MatchType.Exact, "D"))
            arrClaimJV = New BenefitClaimJVFacade(User).Retrieve(crit)
            If Not IsNothing(arrClaimJV) AndAlso arrClaimJV.Count > 0 Then
                _BenefitClaimJV = CType(arrClaimJV(0), BenefitClaimJV)
            End If
        End If

        _BenefitClaimJV.BenefitClaimHeader = oBenefitClaimReceipt.BenefitClaimHeader
        _BenefitClaimJV.TipeAccount = "D"
        _BenefitClaimJV.VendorID = ""

        Dim pphVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(oBenefitClaimReceipt.ReceiptDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
        Dim ppnVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(oBenefitClaimReceipt.ReceiptDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        Dim amount As Decimal = 0, nilaiPph As Decimal = 0, nilaiPpn As Decimal = 0
        Dim tempamount As Decimal = 0
        For Each items1 As BenefitClaimDetails In _BenefitClaimJV.BenefitClaimHeader.BenefitClaimDetailss
            If items1.DetailStatus = 1 Then
                amount += items1.BenefitMasterDetail.Amount
            End If
        Next
        tempamount = amount
        If _BenefitClaimJV.BenefitClaimHeader.BenefitType.LeasingBox = 1 Then
            'nilaiPph = Math.Round(((amount / (1 - 0.15)) - amount))
            'nilaiPpn = Math.Round((0.1 * (nilaiPph + tempamount)))
            nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.Total, pphVal, total:=amount)
            nilaiPpn = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=CalcHelper.DPPCalculation(pphVal, amount))
        Else
            'nilaiPph = Math.Round(((amount / (1 - 0.15)) - amount))
            'nilaiPph = Math.Round(0.15 * amount)
            nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pphVal, dpp:=amount)
            nilaiPpn = 0 'Math.Round(0.1 * tempamount)
        End If

        _BenefitClaimJV.Amount = amount + nilaiPpn

        '_BenefitClaimJV.Amount = lblDPP.Text + lblVat.Text - lblPPh.Text
        _BenefitClaimJV.AccuredMount = 0
        _BenefitClaimJV.BusinessArea = ""
        _BenefitClaimJV.CostCenter = ""
        _BenefitClaimJV.PaymentDate = icProcessDate.Value
        _BenefitClaimJV.JVNumber = ""
        _BenefitClaimJV.InternalOrder = ""
        _BenefitClaimJV.Month = 0

        Dim arrClaimJVFinal As New ArrayList
        arrClaimJVFinal.Add(_BenefitClaimJV)

        arrClaimJV = CType(sessHelper.GetSession(sessClaimJV), ArrayList)
        For Each objJV As BenefitClaimJV In arrClaimJV
            arrClaimJVFinal.Add(objJV)
        Next

        Dim _result As Integer = 0
        If Mode = "Edit" Then
            Dim arlDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteClaimJV), ArrayList)
            _result = New BenefitClaimJVFacade(User).UpdateTransaction(arrClaimJVFinal, arlDelete)
            sessHelper.SetSession(sessDeleteClaimJV, New ArrayList)
        Else
            _result = New BenefitClaimJVFacade(User).InsertTransaction(arrClaimJVFinal)
        End If

        Dim strJs As String = String.Empty
        If _result > -1 Then
            strJs = "alert('Simpan Data Berhasil');"
            strJs += "window.location = '../Benefit/FrmClaimJV.aspx?Mode=Edit&BenefitClaimReceiptID=" & oBenefitClaimReceipt.ID & "';"
        Else
            strJs = "alert('Simpan Data Gagal');"
        End If

        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmClaimJVList.aspx")
    End Sub

    Private Sub dgListJV_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgListJV.PageIndexChanged
        dgListJV.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

    Private Sub dgListJV_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgListJV.SortCommand
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
    End Sub

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

    Private Function getmonthValue(ByVal strBulan As String) As Short
        Select Case strBulan
            Case "Januari"
                Return 1
            Case "Februari"
                Return 2
            Case "Maret"
                Return 3
            Case "April"
                Return 4
            Case "Mei"
                Return 5
            Case "Juni"
                Return 6
            Case "Juli"
                Return 7
            Case "Agustus"
                Return 8
            Case "September"
                Return 9
            Case "Oktober"
                Return 10
            Case "November"
                Return 11
            Case "Desember"
                Return 12
        End Select
        Return 0

    End Function

End Class
