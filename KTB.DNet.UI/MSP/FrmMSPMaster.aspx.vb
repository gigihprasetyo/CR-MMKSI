Imports KTB.DNet.BusinessFacade
Imports KTB.DNET.BusinessFacade.FinishUnit
Imports KTB.DNET.Domain
Imports KTB.DNET.Utility
Imports KTB.DNET.UI.Helper
Imports KTB.DNET.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessValidation.Helpers

Public Class FrmMSPMaster
    Inherits System.Web.UI.Page
    Private _view As Boolean = False
    Private _sessHelper As New SessionHelper
    Private _strSessSearch As String = "CRITERIAS"
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Dim crt As CriteriaComposite
    Dim arr As ArrayList
    Dim sorts As SortCollection

    Private Sub CheckPrivilege()
        _view = SecurityProvider.Authorize(Context.User, SR.MSPMaster_view_Privilege)
        If Not _view Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MSP - Master MSP")
        End If

        btnSearch.Visible = _view
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            BindDropDown()
            ViewState("CurrentSortColumn") = "StartDate"
            ViewState("CurrentSortDirect") = KTB.DNet.Domain.Search.Sort.SortDirection.DESC
        End If
    End Sub

    Private Sub BindDropDown()
        'dropdown category
        crt = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.No, "CV"))
        sorts = New SortCollection
        sorts.Add(New Sort(GetType(Category), "CategoryCode", Search.Sort.SortDirection.ASC))
        arr = New CategoryFacade(User).RetrieveByCriteria(crt, sorts)

        ddlCategory.Items.Clear()
        ddlCategory.DataSource = arr
        ddlCategory.DataTextField = "CategoryCode".ToUpper
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlCategory.SelectedIndex = 0
        ddlCategory_SelectedIndexChanged(Me, System.EventArgs.Empty)

        ' dropdown status
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = New EnumMSPMasterStatus().Retrieve()
        ddlStatus.DataTextField = "NameTitle".ToUpper
        ddlStatus.DataValueField = "ValTitle"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silakan Pilih", -1))
        ddlStatus.SelectedIndex = 0

        ' dropdown msptype
        crt = New CriteriaComposite(New Criteria(GetType(MSPType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(MSPType), "Sequence", Search.Sort.SortDirection.ASC))
        ddlMSPType.Items.Clear()
        ddlMSPType.DataSource = New MSPTypeFacade(User).Retrieve(crt, sortColl)
        ddlMSPType.DataTextField = "Description"
        ddlMSPType.DataValueField = "ID"
        ddlMSPType.DataBind()
        ddlMSPType.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlMSPType.SelectedIndex = 0

    End Sub

    Private Sub BindddlTipe(ByVal Category As String)
        ddlVechileType.Items.Clear()
        If ddlVechileModel.SelectedValue <> "-1" Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Category.CategoryCode", MatchType.Exact, Category))
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlVechileModel, ddlCategory.SelectedItem.Text, criterias, "VechileType")

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlVechileModel.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("VechileTypeCode")) Then
                sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            ddlVechileType.DataSource = New VechileTypeFacade(User).Retrieve(criterias, sortColl)
            ddlVechileType.DataTextField = "VechileTypeCode"
            ddlVechileType.DataValueField = "ID"
            ddlVechileType.DataBind()
        End If
        ddlVechileType.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        ddlVechileModel.Items.Clear()
        ddlVechileType.Items.Clear()
        If ddlCategory.SelectedIndex <> 0 Then
            'crt = New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'crt.opAnd(New Criteria(GetType(VechileModel), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))

            'ddlVechileModel.DataSource = New VechileModelFacade(User).Retrieve(crt)
            'ddlVechileModel.DataTextField = "Description".ToUpper
            'ddlVechileModel.DataValueField = "ID"
            'ddlVechileModel.DataBind()
            'ddlVechileModel_SelectedIndexChanged(Me, System.EventArgs.Empty)

            CommonFunction.BindVehicleSubCategoryToDDL2(ddlVechileModel, ddlCategory.SelectedItem.Text)
            BindddlTipe(ddlCategory.SelectedItem.ToString)

            ddlVechileModel.SelectedIndex = 0
        End If
    End Sub

    Protected Sub ddlVechileModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVechileModel.SelectedIndexChanged
        ddlVechileType.Items.Clear()
        'If ddlVechileModel.SelectedIndex <> 0 Then
        'crt = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'crt.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.Exact, ddlVechileModel.SelectedValue))

        'ddlVechileType.DataSource = New VechileTypeFacade(User).Retrieve(crt)
        'ddlVechileType.DataTextField = "VechileTypeCode".ToUpper
        'ddlVechileType.DataValueField = "ID"
        'End If

        BindddlTipe(ddlCategory.SelectedItem.ToString)
        ddlVechileType.DataBind()
        ddlVechileType.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlVechileType.SelectedIndex = 0
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        crt = New CriteriaComposite(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(crt)
        _sessHelper.SetSession(_strSessSearch, crt)
        dtgMSPMaster.CurrentPageIndex = 0
        BindDatagrid(dtgMSPMaster.CurrentPageIndex)
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        Dim str As String = String.Empty
        Dim strValidate As String = String.Empty
        Dim isStrSql As Integer = 0
        Dim strSql As String = "SELECT ID FROM MSPMaster WHERE 0=0 "

        If ddlCategory.SelectedIndex <> 0 Then
            If ddlVechileModel.SelectedValue <> "-1" Then
                'str = "(SELECT ID FROM VechileType WHERE ModelID = " + ddlVechileModel.SelectedValue + ")"
                'criterias.opAnd(New Criteria(GetType(MSPMaster), "VehicleType", MatchType.InSet, str))

                'CommonFunction.SetVehicleSubCategoryCriterias(ddlVechileModel, ddlCategory.SelectedItem.Text, criterias, "MSPMaster")

                Dim strSql2 As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlVechileModel.SelectedValue
                criterias.opAnd(New Criteria(GetType(MSPMaster), "VehicleType.VechileModel.ID", MatchType.InSet, "(" & strSql2 & ")"))

                If ddlVechileType.SelectedIndex <> 0 Then
                    criterias.opAnd(New Criteria(GetType(MSPMaster), "VehicleType", MatchType.Exact, ddlVechileType.SelectedValue))
                End If
            Else
                str = "(SELECT ID FROM VechileType WHERE CategoryID = " + ddlCategory.SelectedValue + ")"
                criterias.opAnd(New Criteria(GetType(MSPMaster), "VehicleType", MatchType.InSet, str))
            End If
        End If

        If ddlMSPType.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(MSPMaster), "MSPType", MatchType.Exact, ddlMSPType.SelectedValue))
        End If
        If ddlStatus.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(MSPMaster), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If
        If txtDuration.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(MSPMaster), "Duration", MatchType.Exact, txtDuration.Text.Trim))
        End If
        If txtKm.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(MSPMaster), "MSPKm", MatchType.Exact, txtKm.Text.Trim))
        End If

        If chkStartDate.Checked Then
            If 1 = 1 Then
                strSql += " AND (StartDate BETWEEN '" + Format(StartDateFrom.Value, "yyyy-MM-dd") + "' AND '" + Format(StartDateTo.Value, "yyyy-MM-dd") + "')"
                isStrSql += 1

            End If
        End If

        If chkEndDate.Checked Then
            If 1 = 1 Then
                strSql += " AND (EndDate BETWEEN '" + Format(EndDateFrom.Value, "yyyy-MM-dd") + "' AND '" + Format(EndDateTo.Value, "yyyy-MM-dd") + "')"
                isStrSql += 1
            
            End If
        End If

        If isStrSql > 0 Then
            crt.opAnd(New Criteria(GetType(MSPMaster), "ID", MatchType.InSet, "(" + strSql + ")"))
        End If

        If strValidate <> String.Empty Then
            MessageBox.Show(strValidate.Substring(2, strValidate.Length - 2))
            Return
        End If

    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        arr = New MSPMasterFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession(_strSessSearch), CriteriaComposite), indexPage + 1, dtgMSPMaster.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgMSPMaster.DataSource = arr
        dtgMSPMaster.VirtualItemCount = totalRow
        dtgMSPMaster.DataBind()
    End Sub

    Private Sub dtgMSPMaster_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPMaster.ItemDataBound
        'set no
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgMSPMaster.CurrentPageIndex * dtgMSPMaster.PageSize)
        End If

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim rowValue As MSPMaster = CType(e.Item.DataItem, MSPMaster)
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then
                ' set tipe kendaraan
                Dim lblVechileTypeCode As Label = CType(e.Item.FindControl("lblVechileTypeCode"), Label)
                If Not IsNothing(lblVechileTypeCode) Then
                    lblVechileTypeCode.Text = rowValue.VehicleType.VechileTypeCode
                End If
                ' set nama kendaraan 
                Dim lblDescription As Label = CType(e.Item.FindControl("lblDescription"), Label)
                If Not IsNothing(lblDescription) Then
                    lblDescription.Text = rowValue.VehicleType.Description
                End If
                ' set msp type 
                Dim lblMSPType As Label = CType(e.Item.FindControl("lblMSPType"), Label)
                If Not IsNothing(lblMSPType) Then
                    lblMSPType.Text = rowValue.MSPType.Description
                End If
                ' set tgl mulai berlaku
                Dim lblStartDate As Label = CType(e.Item.FindControl("lblStartDate"), Label)
                If Not IsNothing(lblStartDate) Then
                    lblStartDate.Text = rowValue.StartDate.ToString("dd MMM yyyy")
                End If
                ' set tgl berlaku sampai
                Dim lblEndDate As Label = CType(e.Item.FindControl("lblEndDate"), Label)
                If Not IsNothing(lblEndDate) Then
                    lblEndDate.Text = rowValue.EndDate.ToString("dd MMM yyyy")
                End If
                ' set km
                Dim lblKm As Label = CType(e.Item.FindControl("lblKm"), Label)
                If Not IsNothing(lblKm) Then
                    lblKm.Text = String.Format("{0:#,##0}", Convert.ToDouble(rowValue.MSPKm))
                End If
                ' set amount
                Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
                If Not IsNothing(lblAmount) Then
                    lblAmount.Text = rowValue.Amount.ToString("C")

                    Dim lblPPN As Label = CType(e.Item.FindControl("lblPPN"), Label)
                    Dim lblTotalAmountPPN As Label = CType(e.Item.FindControl("lblTotalAmountPPN"), Label)
                    Dim ppnVal As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(rowValue.StartDate.Date, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)
                    If Not IsNothing(lblPPN) Then
                        'lblPPN.Text = (rowValue.Amount / 10).ToString("C")
                        lblPPN.Text = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=rowValue.Amount).ToString("C")
                    End If
                    If Not IsNothing(lblTotalAmountPPN) Then
                        'lblTotalAmountPPN.Text = (rowValue.Amount + (rowValue.Amount / 10)).ToString("C")
                        lblTotalAmountPPN.Text = (rowValue.Amount + CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppnVal, dpp:=rowValue.Amount)).ToString("C")
                    End If
                End If
                ' set status
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                If Not IsNothing(lblStatus) Then
                    lblStatus.Text = New EnumMSPMasterStatus().GetStatus(rowValue.Status)
                End If
            End If
        End If
    End Sub

    Private Sub dtgMSPMaster_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMSPMaster.PageIndexChanged
        dtgMSPMaster.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgMSPMaster.CurrentPageIndex)
    End Sub

    Private Sub dtgMSPMaster_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgMSPMaster.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = sort.SortDirection.DESC

                Case sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = sort.SortDirection.ASC
        End If

        dtgMSPMaster.SelectedIndex = -1
        dtgMSPMaster.CurrentPageIndex = 0
        BindDatagrid(dtgMSPMaster.CurrentPageIndex)
    End Sub
End Class