
#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.Pk
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
#End Region

Public Class FrmEntryRedemptionPlan
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblType As System.Web.UI.WebControls.Label
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lbl As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCodeColon As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnDistribute As System.Web.UI.WebControls.Button
    Protected WithEvents ddlTipeWarna As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblMaxContractDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblBreak As System.Web.UI.WebControls.Label
    Protected WithEvents btnRefreshGrid As System.Web.UI.WebControls.Button
    Protected WithEvents txtRowIndex As System.Web.UI.WebControls.TextBox

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
    Private sessHelper As New SessionHelper
    Private arlDataToDisplay As New ArrayList
    Private sessTotalStockPerColumns As String = "TotalPerColumns"
    Private TotalStockPerColumns(31) As Integer

#End Region

#Region "Custom Methods"

    Private Sub Initialization()
        Dim objD As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If objD.Title <> CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            lblTitle.Text = "Input "
            Me.txtKodeDealer.Text = objD.DealerCode
            lblDealerCode.Visible = False
            lblDealerCodeColon.Visible = False
            txtKodeDealer.Visible = False
            lblSearchDealer.Visible = False
            Me.btnDistribute.Visible = False
            Me.btnSave.Visible = False
        Else
            lblTitle.Text = "Respond "
            Me.txtKodeDealer.Text = ""
            Me.btnDistribute.Visible = True
            Me.btnSave.Visible = True
        End If

        viewstate.Add("nDays", 0)
        viewstate.Add("arlTotals", New ArrayList)
        viewstate.Add("arlTotalResponds", New ArrayList)
        viewstate.Add("AutoRefresh", "0")
        viewstate.Add("TransactionControl.Status", CType(EnumDealerStatus.DealerStatus.NonAktive, Short))
        viewstate.Add("arlDisplayedVehicleColor", Nothing)
        Me.sessHelper.SetSession("FrmEntryRedemptionPlan.arlDisplayedVehicleColor", Nothing)
        BindPeriod()
        BindDdlCategory()
        InitHeader()
        If Not IsNothing(Request.Item("AutoRefresh")) AndAlso CType(Request.Item("AutoRefresh"), String) = "1" Then
            If Not IsNothing(sessHelper.GetSession("FrmEntryRedemptionPlan.SelectedStatus")) AndAlso CType(sessHelper.GetSession("FrmEntryRedemptionPlan.SelectedStatus"), String).Trim <> "" Then
                Dim SelStatus() As String = CType(sessHelper.GetSession("FrmEntryRedemptionPlan.SelectedStatus"), String).Split("#")
                viewstate.Item("AutoRefresh") = "1"
                ddlMonth.SelectedIndex = SelStatus(0)
                ddlYear.SelectedIndex = SelStatus(1)
                ddlCategory.SelectedIndex = SelStatus(2)
                ddlCategory_SelectedIndexChanged(Nothing, Nothing)
                ddlSubCategory.SelectedIndex = SelStatus(3)
                ddlSubCategory_SelectedIndexChanged(Nothing, Nothing)
                ddlType.SelectedIndex = SelStatus(4)
                ddlType_SelectedIndexChanged(Nothing, Nothing)
                ddlTipeWarna.SelectedIndex = SelStatus(5)
                txtKodeDealer.Text = SelStatus(6)
                BindDTG(SelStatus(7))
            End If
        Else
            sessHelper.SetSession("FrmEntryRedemptionPlan.arlDataToDisplay", New ArrayList)
        End If
    End Sub

    Private Sub BindPeriod()
        Dim i As Integer

        ddlMonth.Items.Clear()
        For Each li As ListItem In CType(LookUp.ArrayBulan(), ArrayList)
            ddlMonth.Items.Add(li)
        Next
        ddlMonth.SelectedValue = Now.Month

        ddlYear.Items.Clear()
        For i = Now.Year - 2 To Now.Year + 2
            ddlYear.Items.Add(New ListItem(i.ToString, i))
        Next
        ddlYear.SelectedValue = Now.Year
    End Sub

    Private Sub BindDdlCategory()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Pilih", ""))

        ddlSubCategory.Items.Insert(0, New ListItem("Pilih", ""))
        ddlType.Items.Insert(0, New ListItem("Pilih", ""))
    End Sub

    Private Sub BindVehicleType(ByVal IsClearAll As Boolean)
        ddlType.Items.Clear()
        If ddlSubCategory.SelectedValue <> "-1" And Not IsClearAll Then

            '-- Vehicle criteria & sort
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))  '-- Type still active
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "VechileType")
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))  '-- Sort by Vechile type code

            '-- Bind Vehicle type dropdownlist
            ddlType.DataSource = New VechileTypeFacade(User).RetrieveByCriteria(criterias, sortColl)
            ddlType.DataTextField = "VechileTypeCode"
            ddlType.DataValueField = "VechileTypeCode"
            ddlType.DataBind()
        End If
        ddlType.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item
    End Sub

    Private Sub InitTotals()
        Dim arlTemp As New ArrayList
        Dim arlTempResponds As New ArrayList
        Dim i As Integer

        arlTemp.Clear()
        For i = 1 To CType(viewstate.Item("nDays"), Integer) + 1 '+1=sub total each row
            arlTemp.Add(0)
            arlTempResponds.Add(0)
        Next

        viewstate.Item("arlTotals") = arlTemp
        viewstate.Item("arlTotalResponds") = arlTempResponds
    End Sub

    Private Sub UpdateTotal(ByVal Day As Integer, ByVal Total As Integer, ByVal TotalRespond As Integer)
        Dim arlTemp As ArrayList = viewstate.Item("arlTotals")
        Dim arlTempRespond As ArrayList = viewstate.Item("arlTotalResponds")

        arlTemp(Day - 1) += Total
        arlTempRespond(Day - 1) += TotalRespond
    End Sub

    Private Sub BindDTG(ByVal PageIndex As Integer)
        Dim arlData As New ArrayList
        Dim SelStatus As String
        Dim TotData As Integer

        Me.sessHelper.SetSession("FrmEntryRedemptionPlan.arlDisplayedVehicleColor", Nothing)
        SelStatus = ddlMonth.SelectedIndex & "#" & ddlYear.SelectedIndex & "#" & _
            ddlCategory.SelectedIndex & "#" & ddlSubCategory.SelectedIndex & "#" & ddlType.SelectedIndex & "#" & _
            ddlTipeWarna.SelectedIndex & "#" & txtKodeDealer.Text.Trim & "#" & dtgMain.CurrentPageIndex
        sessHelper.SetSession("FrmEntryRedemptionPlan.SelectedStatus", SelStatus)
        InitTotals()
        InitHeader()
        dtgMain.CurrentPageIndex = PageIndex
        If viewstate.Item("AutoRefresh") = "1" Then
            arlDataToDisplay = sessHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
            TotData = arlDataToDisplay.Count
            arlData = arlDataToDisplay
        Else
            arlData = GetData(TotData)
        End If
        dtgMain.VirtualItemCount = TotData ' arlData.Count
        'DealerVehicle
        dtgMain.DataSource = arlData 'ArrayListPager.DoPage(arlData, dtgMain.CurrentPageIndex, dtgMain.PageSize)
        dtgMain.DataBind()
        viewstate.Item("AutoRefresh") = "0"
    End Sub

    Private Function GetData(ByRef TotData As Integer) As ArrayList

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlCategory.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        If ddlSubCategory.SelectedIndex > 0 Then
            'CommonFunction.SetVehicleSubCategoryCriterias(ddlSubCategory, ddlCategory.SelectedItem.Text, criterias, "VechileColor")
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If ddlType.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, ddlType.SelectedValue))
        End If
        If ddlTipeWarna.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(VechileColor), "ID", MatchType.Exact, ddlTipeWarna.SelectedValue))
        End If

        '-- Type still active
        criterias.opAnd(New Criteria(GetType(VechileColor), "VechileType.Status", MatchType.No, "X"))
        '-- SpecialFlag <> 'X'
        criterias.opAnd(New Criteria(GetType(VechileColor), "SpecialFlag", MatchType.No, "X"))
        '-- Status <> 'X'
        criterias.opAnd(New Criteria(GetType(VechileColor), "Status", MatchType.No, "X"))
        '-- Color code never have value of 'zzzz'
        criterias.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.No, "zzzz"))
        Dim ColorList As ArrayList = New VechileColorFacade(User).Retrieve(criterias)
        'TotData = ColorList.Count
        'ColorList = ArrayListPager.DoPage(ColorList, dtgMain.CurrentPageIndex, dtgMain.PageSize)
        Me.sessHelper.SetSession("FrmEntryRedemptionPlan.ColorList", ColorList)
        Dim arlAllData As New ArrayList
        Dim objDFac As DealerFacade = New DealerFacade(User)
        Dim objD As Dealer
        Dim objDVC As DealerVehicle
        Dim sDealerCodes As String = ""

        If txtKodeDealer.Text.Trim = "" Then
            Dim aD As New ArrayList
            Dim oDFac As DealerFacade = New DealerFacade(User)
            Dim cD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            cD.opAnd(New Criteria(GetType(Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
            'cD.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, oDFac.GenerateDealerCodeSelection(CType(Session("DEALER"), Dealer), User)))
            'cD.opAnd(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, 1))
            aD = oDFac.Retrieve(cD)
            For Each oD As Dealer In aD
                If oD.Title = EnumDealerTittle.DealerTittle.KTB OrElse oD.DealerCode.Trim.ToUpper = "DSF" OrElse oD.DealerCode.Trim.ToUpper = "BAS" Then
                Else
                    sDealerCodes &= IIf(sDealerCodes.Trim = "", "", ";") & oD.DealerCode
                End If
            Next
            txtKodeDealer.Text = sDealerCodes
        Else
            For Each sCode As String In txtKodeDealer.Text.Trim.Split(";")
                If sCode.Trim.ToUpper = "KTB" OrElse sCode.Trim.ToUpper = "MKS" OrElse sCode.Trim.ToUpper = "DSF" OrElse sCode.Trim.ToUpper = "BAS" Then
                Else
                    sDealerCodes &= IIf(sDealerCodes.Trim = "", "", ";") & sCode
                End If
            Next
            txtKodeDealer.Text = sDealerCodes
            'sDealerCodes = txtKodeDealer.Text.Trim
        End If
        For Each oVC As VechileColor In ColorList
            For Each strCode As String In sDealerCodes.Split(";")
                objD = objDFac.Retrieve(strCode)
                If Not IsNothing(objD) AndAlso objD.ID > 0 Then
                    objDVC = New DealerVehicle
                    objDVC.ID = oVC.ID
                    objDVC.Dealer = objD
                    objDVC.VechileColor = oVC
                    arlAllData.Add(objDVC)
                End If
            Next
        Next

        TotData = arlAllData.Count

        arlAllData = DataFiltering(arlAllData)

        arlDataToDisplay = ArrayListPager.DoPage(arlAllData, dtgMain.CurrentPageIndex, dtgMain.PageSize)
        If IsNothing(arlDataToDisplay) Then arlDataToDisplay = New ArrayList
        sessHelper.SetSession("FrmEntryRedemptionPlan.arlDataToDisplay", arlDataToDisplay)
        InitData()
        arlDataToDisplay = sessHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        TotData = arlDataToDisplay.Count
        Return arlDataToDisplay
    End Function

    Private Function DataFiltering(ByRef arlOriData) As ArrayList
        Dim arlTemp As New ArrayList
        Dim IsRespond As Boolean = False

        If CType(sessHelper.GetSession("DEALER"), Dealer).Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            IsRespond = True
        End If
        For Each oDV As DealerVehicle In arlOriData
            If IsRespond Then
                If IsVehicleHasStock(oDV.VechileColor.ID) AndAlso IsHavingRequest(oDV) Then
                    arlTemp.Add(oDV)
                End If
            Else
                If IsVehicleHasStock(oDV.VechileColor.ID) Then
                    arlTemp.Add(oDV)
                End If
            End If
            'If IsVehicleHasStock(oDV.VechileColor.ID) AndAlso GetDealerOC(oDV.VechileColor.ID, oDV.Dealer.DealerCode) > 0 Then
            '    If IsRespond = False OrElse IsHavingRequest(oDV) Then 'never run IsHavingRequest
            '        arlTemp.Add(oDV)
            '    End If
            'End If
        Next
        Return arlTemp
    End Function

    Private Function IsHavingRequest(ByRef oDV As DealerVehicle) As Boolean
        Dim IsHaving As Boolean = False
        Dim Sql As String = ""
        Dim dtStart As Date = DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1).AddDays(-1)
        Dim dtEnd As Date = DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1).AddMonths(1)

        Sql &= " Select rh.ID "
        Sql &= " from RedemptionHeader rh "
        Sql &= " , RedemptionDetail rd "
        Sql &= " where (1 = 1) "
        Sql &= " and rh.RowStatus=" & CType(DBRowStatus.Active, String) & " "
        Sql &= " and rd.RowStatus=" & CType(DBRowStatus.Active, String) & " "
        Sql &= " and rh.ID=rd.RedemptionHeaderID "
        Sql &= " and rh.PeriodDate>'" & dtStart.ToString("yyyy.MM.dd") & "' "
        Sql &= " and rh.PeriodDate<'" & dtEnd.ToString("yyyy.MM.dd") & "' "
        Sql &= " and rh.VehicleColorID= " & oDV.VechileColor.ID
        Sql &= " and rd.DealerID=" & oDV.Dealer.ID

        Dim oRHFac As RedemptionHeaderFacade = New RedemptionHeaderFacade(User)
        Dim cRH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionHeader), "ID", MatchType.InSet, "(" & Sql & ")"))
        Dim aRH As New ArrayList
        Dim aggRH As New Aggregate(GetType(RedemptionHeader), "ID", AggregateType.Count)
        Dim n As Integer
        Try
            n = oRHFac.RetrieveScalar(cRH, aggRH)
        Catch ex As Exception
            n = 0
        End Try
        Return (n > 0)
        'aRH = oRHFac.Retrieve(cRH)
        'If aRH.Count > 0 Then IsHaving = True
        'Return IsHaving
    End Function

    Private Function IsVehicleHasStock(ByVal pVCID As Integer) As Boolean
        Dim arlRH As New ArrayList
        Dim crtRH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim dtStart As Date = DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1).AddDays(-1)
        Dim dtEnd As Date = DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1).AddMonths(1)
        Dim aggRH As New Aggregate(GetType(RedemptionHeader), "ID", AggregateType.Count)

        crtRH.opAnd(New Criteria(GetType(RedemptionHeader), "VechileColor.ID", MatchType.Exact, pVCID))
        crtRH.opAnd(New Criteria(GetType(RedemptionHeader), "PeriodDate", MatchType.Greater, dtStart))
        crtRH.opAnd(New Criteria(GetType(RedemptionHeader), "PeriodDate", MatchType.Lesser, dtEnd))
        crtRH.opAnd(New Criteria(GetType(RedemptionHeader), "EstimationStock", MatchType.Greater, 0))
        Dim n As Integer
        Try
            n = CType(New RedemptionHeaderFacade(User).RetrieveScalar(crtRH, aggRH), Integer)
        Catch ex As Exception
            n = 0
        End Try
        Return (n > 0)
        'arlRH = New RedemptionHeaderFacade(User).Retrieve(crtRH)
        'If arlRH.Count = 0 Then
        '    Return False
        'Else
        '    Return True
        'End If
    End Function

    Private Sub InitHeader()
        Dim dtFrom As Date = DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1)
        Dim dtTo As Date = dtFrom.AddMonths(1) '.AddDays(-1)
        Dim nDays As Integer = DateDiff(DateInterval.Day, dtFrom, dtTo)
        Dim i As Integer

        viewstate.Item("nDays") = nDays
        'For i = 1 To 31

        '    If i <= nDays Then
        '        dtgMain.Columns(3 + i).Visible = True
        '        dtgMain.Columns(3 + i).HeaderText = i.ToString
        '    Else
        '        dtgMain.Columns(3 + i).Visible = False
        '    End If
        'Next
        InitTotals()
        Dim oRCFac As RedemptionCeilingFacade = New RedemptionCeilingFacade(User)
        Dim cRC As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aRC As New ArrayList

        cRC.opAnd(New Criteria(GetType(RedemptionCeiling), "PeriodMonth", MatchType.Exact, ddlMonth.SelectedValue))
        cRC.opAnd(New Criteria(GetType(RedemptionCeiling), "PeriodYear", MatchType.Exact, ddlYear.SelectedValue))
        aRC = oRCFac.Retrieve(cRC)
        If aRC.Count > 0 Then
            Me.lblMaxContractDate.Text = CType(aRC(0), RedemptionCeiling).MaxContractDate.ToString("dd MMM yyyy")
        Else
            Me.lblMaxContractDate.Text = ""
        End If

        'IsEnable Input RedemptionPlan by Transaction Control 
        Dim objD As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If objD.Title <> CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            Dim oTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(objD.ID, EnumDealerTransType.DealerTransKind.RedemptionPlan)
            
            viewstate.Item("TransactionControl.Status") = oTC.Status
        Else
            viewstate.Item("TransactionControl.Status") = EnumDealerStatus.DealerStatus.Aktive
        End If


        

    End Sub

    Private Sub FillDTGRow(ByRef e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)
        Dim oVC As VechileColor
        Dim i As Integer
        Dim txtID As TextBox = e.Item.FindControl("txtID")
        Dim lblTotal As Label
        Dim imgChecked As System.Web.UI.WebControls.Image
        Dim lblOC As Label = e.Item.FindControl("lblOC")
        Dim objRH As RedemptionHeader
        Dim TotalPerVehicle As Decimal = 0
        Dim TotalRespondPerVehicle As Decimal = 0
        Dim TotReq As Integer
        Dim TotRes As Integer
        Dim lblDealer As Label = e.Item.FindControl("lblDealer")
        Dim lblCode As Label = e.Item.FindControl("lblCode")
        Dim objD As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim IsVehicleHasStock As Boolean = False
        Dim IsVehicleHasRequest As Boolean = False
        Dim lblDescription As Label = e.Item.FindControl("lblDescription")
        Dim objDV As DealerVehicle


        arlDataToDisplay = sessHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        objDV = CType(arlDataToDisplay((dtgMain.CurrentPageIndex * dtgMain.PageSize) + e.Item.ItemIndex), DealerVehicle)
        lblCode.Text = objDV.VechileColor.VechileType.VechileTypeCode & objDV.VechileColor.ColorCode
        lblDescription.Text = objDV.VechileColor.MaterialDescription.Replace(" ", "_")
        For i = 1 To nDays
            lblTotal = e.Item.FindControl("lblS" & i.ToString)
            objRH = objDV.RedemptionHeaders(i - 1)

            If e.Item.ItemIndex = 0 Then
                If GetTotalStockDaily(i, False) < 1 Then ' objRH.EstimationStock < 1 Then
                    Me.dtgMain.Columns(i + 3).Visible = False
                End If
            End If
            'Start Get Total Of Estimation Stock
            Me.TotalStockPerColumns = Me.sessHelper.GetSession(Me.sessTotalStockPerColumns)
            Me.TotalStockPerColumns(i - 1) += objRH.EstimationStock
            Me.sessHelper.SetSession(Me.sessTotalStockPerColumns, Me.TotalStockPerColumns)
            'End Get Total Of Estimation Stock

            TotalPerVehicle += objRH.TotalRequest(objDV.Dealer.ID)
            TotalRespondPerVehicle += objRH.TotalRespond(objDV.Dealer.ID)
            UpdateTotal(i, objRH.TotalRequest(objDV.Dealer.ID), objRH.TotalRespond(objDV.Dealer.ID))
            lblOC.Text = GetDealerOC(objRH.VechileColor.ID, objDV.Dealer.DealerCode)
            If objRH.EstimationStock > 0 Then
                If objRH.TotalRequest(objDV.Dealer.ID) > 0 Then
                    lblTotal.Text = objRH.TotalRequest(objDV.Dealer.ID) & " | " & objRH.TotalRespond(objDV.Dealer.ID) ' & "=" & objRH.EstimationStock
                    If objD.Title <> CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                        If CType(viewstate.Item("TransactionControl.Status"), Short) = EnumDealerStatus.DealerStatus.Aktive Then
                            e.Item.Cells(3 + i).Attributes.Add("OnDblClick", "ShowDetail(" & e.Item.ItemIndex & "," & i - 1 & "," & objDV.Dealer.DealerCode & ")")
                            e.Item.Cells(3 + i).Style.Add("cursor", "hand")
                        End If
                    Else
                        e.Item.Cells(3 + i).Attributes.Add("OnDblClick", "ShowDetail(" & e.Item.ItemIndex & "," & i - 1 & "," & objDV.Dealer.DealerCode & ")")
                        e.Item.Cells(3 + i).Style.Add("cursor", "hand")
                    End If
                    IsVehicleHasRequest = True
                Else
                    If objD.Title <> CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                        If CType(viewstate.Item("TransactionControl.Status"), Short) = EnumDealerStatus.DealerStatus.Aktive Then
                            e.Item.Cells(3 + i).Attributes.Add("OnDblClick", "ShowDetail(" & e.Item.ItemIndex & "," & i - 1 & "," & objDV.Dealer.DealerCode & ")")
                            e.Item.Cells(3 + i).Style.Add("cursor", "hand")
                        End If
                    Else
                        lblTotal.Text = ""
                    End If
                End If
            Else
                lblTotal.Text = ""
            End If
        Next
        UpdateTotal(nDays + 1, TotalPerVehicle, TotalRespondPerVehicle)

        e.Item.Cells(dtgMain.Columns.Count - 1).Text = TotalRespondPerVehicle ' TotalPerVehicle & " | " & TotalRespondPerVehicle
        If TotalPerVehicle <= 0 AndAlso TotalRespondPerVehicle <= 0 Then
            '    e.Item.Visible = False
        End If
    End Sub

    Private Function GetTotalStockDaily(ByVal iDay As Integer, Optional ByVal IsAllVehicle As Boolean = True) As Integer
        Dim oRHFac As RedemptionHeaderFacade = New RedemptionHeaderFacade(User)
        Dim cRH As CriteriaComposite
        Dim aRH As New ArrayList
        Dim aVC As ArrayList
        Dim iResult As Integer = 0
        Dim arlVC As New ArrayList
        Dim aggRH As Aggregate = New Aggregate(GetType(RedemptionHeader), "EstimationStock", AggregateType.Sum)
        Dim i As Integer
        Dim sVCIDs As String = String.Empty
        Dim oVC As VechileColor

        If IsAllVehicle Then
            aVC = CType(Me.sessHelper.GetSession("FrmEntryRedemptionPlan.ColorList"), ArrayList)
        Else
            aVC = GetDisplayedVehicleColor()
        End If
        For i = 0 To aVC.Count - 1
            sVCIDs &= IIf(i = 0, "", ",") & CType(aVC(i), VechileColor).ID.ToString
        Next
        cRH = New CriteriaComposite(New Criteria(GetType(RedemptionHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cRH.opAnd(New Criteria(GetType(RedemptionHeader), "PeriodDate", MatchType.Exact, DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, iDay)))
        cRH.opAnd(New Criteria(GetType(RedemptionHeader), "VechileColor.ID", MatchType.InSet, "(" & sVCIDs & ")"))
        Try
            iResult = oRHFac.RetrieveScalar(cRH, aggRH)
        Catch ex As Exception
            iResult = 0
        End Try
        'For Each oVC As VechileColor In aVC
        '    cRH = New CriteriaComposite(New Criteria(GetType(RedemptionHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    cRH.opAnd(New Criteria(GetType(RedemptionHeader), "PeriodDate", MatchType.Exact, DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, iDay)))
        '    cRH.opAnd(New Criteria(GetType(RedemptionHeader), "VechileColor.ID", MatchType.Exact, oVC.ID))

        '    'aRH = oRHFac.Retrieve(cRH)
        '    'If aRH.Count > 0 Then
        '    '    iResult += CType(aRH(0), RedemptionHeader).EstimationStock
        '    'End If
        'Next
        Return iResult
    End Function

    Private Function GetDisplayedVehicleColor() As ArrayList
        Dim arlVC As New ArrayList
        Dim LastVCID As Integer = -1

        If IsNothing(Me.sessHelper.GetSession("FrmEntryRedemptionPlan.arlDisplayedVehicleColor")) Then
            For Each oDV As DealerVehicle In CType(sessHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay"), ArrayList)
                If LastVCID = -1 Then
                    LastVCID = oDV.VechileColor.ID
                    arlVC.Add(oDV.VechileColor)
                Else
                    If LastVCID <> oDV.VechileColor.ID Then
                        LastVCID = oDV.VechileColor.ID
                        arlVC.Add(oDV.VechileColor)
                    End If
                End If
            Next
            Me.sessHelper.SetSession("FrmEntryRedemptionPlan.arlDisplayedVehicleColor", arlVC)
        End If
        Return CType(Me.sessHelper.GetSession("FrmEntryRedemptionPlan.arlDisplayedVehicleColor"), ArrayList)
    End Function

    Private Sub FillDTGFooter(ByRef e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)
        Dim i As Integer
        Dim arlTemp = CType(viewstate.Item("arlTotals"), ArrayList)
        Dim arlTempRespond = CType(viewstate.Item("arlTotalResponds"), ArrayList)
        Dim TotalStock As Integer = 0
        Dim TotTemp As Integer = 0
        Dim TotDisplayed As Integer = 0
        Dim TotTempDisplayed As Integer = 0
        Dim objD As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)

        Me.TotalStockPerColumns = Me.sessHelper.GetSession(Me.sessTotalStockPerColumns)
        For i = 1 To nDays
            e.Item.Cells(3 + i).Text = FormatNumber(arlTemp(i - 1), 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " | " & FormatNumber(arlTempRespond(i - 1), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            TotTemp = GetTotalStockDaily(i) '  Me.TotalStockPerColumns(i - 1)
            TotTempDisplayed = GetTotalStockDaily(i, False)
            If objD.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                e.Item.Cells(3 + i).Text &= Me.lblBreak.Text & " (" & FormatNumber(TotTempDisplayed, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " of " & FormatNumber(TotTemp, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & ")"
            End If
            TotDisplayed += TotTempDisplayed
            TotalStock += TotTemp
        Next
        e.Item.Cells(dtgMain.Columns.Count - 1).Text = FormatNumber(arlTempRespond(nDays), 0, TriState.UseDefault, TriState.UseDefault, TriState.True) ' FormatNumber(arlTemp(nDays), 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " | " & FormatNumber(arlTempRespond(nDays), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        If objD.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            e.Item.Cells(dtgMain.Columns.Count - 1).Text &= Me.lblBreak.Text & " (" & FormatNumber(TotDisplayed, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " of " & FormatNumber(TotalStock, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & ")"
        End If
        Me.sessHelper.SetSession("FrmEntryRedemptionPlan.Footer", e)
    End Sub

    Public Function GetDealerOC(ByVal VCID As Integer, ByVal DealerCode As String) As Integer
        Dim oCDFac As ContractDetailFacade = New ContractDetailFacade(User)
        Dim cCD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aCD As New ArrayList
        Dim Total As Integer = 0
        Dim aggCD As New Aggregate(GetType(ContractDetail), "TargetQty", AggregateType.Sum)

        cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.ContractPeriodMonth", MatchType.Exact, ddlMonth.SelectedValue))
        cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.ContractPeriodYear", MatchType.Exact, ddlYear.SelectedValue))
        cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.Dealer.DealerCode", MatchType.Exact, DealerCode))
        cCD.opAnd(New Criteria(GetType(ContractDetail), "VechileColor.ID", MatchType.Exact, VCID))
        cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.CreatedTime", MatchType.LesserOrEqual, Format(CommonFunction.GetMaxContractDateOfRedemption(ddlMonth.SelectedValue, ddlYear.SelectedValue), "yyyy/MM/dd hh:mm:ss")))
        Try
            Total = oCDFac.RetrieveScalar(cCD, aggCD)
        Catch ex As Exception
            Total = 0
        End Try
        'aCD = oCDFac.Retrieve(cCD)
        'For Each oCD As ContractDetail In aCD
        '    Total += oCD.TargetQty
        'Next

        Return Total
    End Function

    Private Sub GetTotalPerDay(ByVal DealerCode As String, ByVal pVC As VechileColor, ByVal pDay As Date, ByVal objRH As RedemptionHeader, ByRef TotReq As Integer, ByRef TotRes As Integer)
        'Dim objD As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objRDFac As RedemptionDetailFacade = New RedemptionDetailFacade(User)
        Dim arlRD As New ArrayList
        Dim crtRD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim Total As Integer = 0
        Dim Total2 As Integer = 0

        crtRD.opAnd(New Criteria(GetType(RedemptionDetail), "RedemptionHeader.ID", MatchType.Exact, objRH.ID))
        crtRD.opAnd(New Criteria(GetType(RedemptionDetail), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        arlRD = objRDFac.Retrieve(crtRD)
        For Each oRD As RedemptionDetail In arlRD
            Total += oRD.RequestQty
            Total2 += oRD.RespondQty
        Next
        TotReq = Total
        TotRes = Total2
    End Sub

    Private Function SaveData() As Boolean
        Dim IsSuccess As Boolean = False
        Dim objRDFac As RedemptionDetailFacade = New RedemptionDetailFacade(User)
        Dim nFailedToSave As Integer = 0
        Dim nSuccessToSave As Integer = 0
        Dim nResult As Integer = -1
        Dim Idx As Integer = 0
        Dim lblDealer As Label
        Dim oDFac As DealerFacade = New DealerFacade(User)
        Dim oD As Dealer

        'Only Update RespondQty
        arlDataToDisplay = sessHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        For Each oDV As DealerVehicle In arlDataToDisplay
            lblDealer = dtgMain.Items(Idx).FindControl("lblDealer")
            oD = oDFac.Retrieve(lblDealer.Text)
            For Each oRH As RedemptionHeader In oDV.RedemptionHeaders
                For Each oRD As RedemptionDetail In oRH.RedemptionDetails(oD.ID)
                    nResult = objRDFac.Update(oRD)
                    If nResult = -1 Then
                        nFailedToSave += 1
                    Else
                        nSuccessToSave += 1
                        IsSuccess = True
                    End If
                Next
            Next
            Idx += 1
        Next
        If nFailedToSave > 0 And nSuccessToSave > 0 Then
            MessageBox.Show("Simpan berhasil. berhasil : " & nSuccessToSave & ", gagal : " & nFailedToSave)
        ElseIf nFailedToSave > 0 And nSuccessToSave = 0 Then
            MessageBox.Show(SR.SaveFail)
        ElseIf nFailedToSave = 0 And nSuccessToSave > 0 Then
            MessageBox.Show(SR.SaveSuccess)
        End If
        Return IsSuccess
    End Function

    Private Function GetRH(ByVal pVCID As Integer, ByVal pDay As Date) As RedemptionHeader
        Dim arlRH As New ArrayList
        Dim crtRH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        crtRH.opAnd(New Criteria(GetType(RedemptionHeader), "VechileColor.ID", MatchType.Exact, pVCID))
        crtRH.opAnd(New Criteria(GetType(RedemptionHeader), "PeriodDate", MatchType.Exact, pDay))
        arlRH = New RedemptionHeaderFacade(User).Retrieve(crtRH)
        If arlRH.Count = 0 Then
            Return New RedemptionHeader
        Else
            Return CType(arlRH(0), RedemptionHeader)
        End If
    End Function

    Private Function GetCeiling(ByVal pPT As Integer, ByVal pDate As Date) As Decimal
        Dim pY As Integer = pDate.Year
        Dim pM As Integer = pDate.Month
        Dim pD As Integer = pDate.Day
        Dim oRHFac As RedemptionHeaderFacade = New RedemptionHeaderFacade(User)
        Dim cRH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        cRH.opAnd(New Criteria(GetType(RedemptionHeader), "RowStatus", MatchType.Exact, 1))
        cRH.opAnd(New Criteria(GetType(RedemptionHeader), "RowStatus", MatchType.Exact, 1))
        cRH.opAnd(New Criteria(GetType(RedemptionHeader), "RowStatus", MatchType.Exact, 1))
        cRH.opAnd(New Criteria(GetType(RedemptionHeader), "RowStatus", MatchType.Exact, 1))

    End Function

    Private Sub AutoDistribute()
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim l As Integer
        Dim RemainStock As Integer
        Dim TotEstStock As Integer
        Dim TotReqByDealers As Integer
        Dim AllocQty As Integer
        Dim RemainAllocQty As Integer
        Dim objRH As RedemptionHeader
        Dim objRD As RedemptionDetail
        Dim CurVCID As Integer
        Dim oDV1 As DealerVehicle
        Dim oDV2 As DealerVehicle
        Dim arlDataToDisplayCopy As New ArrayList
        Dim oDV1Copy As DealerVehicle
        Dim oDV2Copy As DealerVehicle
        Dim objRHCopy As RedemptionHeader
        Dim objRDCopy As RedemptionDetail
        Dim decTemp As Decimal = 0

        'InitData()

        arlDataToDisplay = sessHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        'arlDataToDisplayCopy = sessHelper.GetSession("arlDataToDisplayCopy")


        For i = 1 To nDays
            For j = 0 To arlDataToDisplay.Count - 1
                TotReqByDealers = 0
                RemainStock = 0
                oDV1 = CType(arlDataToDisplay(j), DealerVehicle)
                TotEstStock = CType(oDV1.RedemptionHeaders(i - 1), RedemptionHeader).EstimationStock
                For k = j To j + oDV1.TotalSameDV - 1
                    oDV2 = CType(arlDataToDisplay(k), DealerVehicle)
                    TotReqByDealers += CType(oDV2.RedemptionHeaders(i - 1), RedemptionHeader).TotalRequest(oDV2.Dealer.ID)
                Next
                If TotReqByDealers > 0 Then
                    RemainStock = TotEstStock
                    For k = j To j + oDV1.TotalSameDV - 1 'distribute allocqty to each RH & RD
                        oDV2 = CType(arlDataToDisplay(k), DealerVehicle)
                        objRH = oDV2.RedemptionHeaders(i - 1)
                        AllocQty = (objRH.TotalRequest(oDV2.Dealer.ID) / TotReqByDealers) * TotEstStock
                        decTemp = (objRH.TotalRequest(oDV2.Dealer.ID) / TotReqByDealers) * TotEstStock
                        If decTemp >= (CType(AllocQty, Decimal) + 0.5) Then
                            AllocQty += 1
                        End If
                        If AllocQty > objRH.TotalRequest(oDV2.Dealer.ID) Then
                            AllocQty = objRH.TotalRequest(oDV2.Dealer.ID)
                        End If
                        If RemainStock < AllocQty Then
                            AllocQty = RemainStock
                        End If
                        RemainAllocQty = AllocQty
                        'Distribute to each RD
                        l = 0
                        For Each oRD As RedemptionDetail In objRH.RedemptionDetails(oDV2.Dealer.ID)
                            If RemainAllocQty >= oRD.RequestQty Then
                                oRD.RespondQty = oRD.RequestQty
                            Else
                                Dim UnAllocatedRequest As Integer = 0
                                UnAllocatedRequest = oRD.RequestQty - RemainAllocQty
                                oRD.RespondQty = RemainAllocQty
                                'put the unallocated request to the following date (which has estimationStock>0) 
                                If UnAllocatedRequest > 0 Then
                                    Dim m As Integer
                                    For m = i To nDays - 1
                                        If CType(oDV2.RedemptionHeaders(m), RedemptionHeader).EstimationStock > 0 Then
                                            If CType(oDV2.RedemptionHeaders(m), RedemptionHeader).RedemptionDetails(oDV2.Dealer.ID).Count > 0 Then
                                                CType(CType(oDV2.RedemptionHeaders(m), RedemptionHeader).RedemptionDetails(oDV2.Dealer.ID)(0), RedemptionDetail).RequestQty += UnAllocatedRequest
                                                Exit For
                                            Else
                                                Dim oRDNew As New RedemptionDetail
                                                oRDNew.RedemptionHeader = oRD.RedemptionHeader
                                                oRDNew.TermOfPayment = oRD.TermOfPayment
                                                oRDNew.Dealer = oRD.Dealer
                                                oRDNew.RowStatus = oRD.RowStatus
                                                oRDNew.RequestQty = UnAllocatedRequest
                                                oRDNew.RespondQty = 0
                                                CType(oDV2.RedemptionHeaders(m), RedemptionHeader).SetRedemptionDetail(oRDNew, -1)  ' .RedemptionDetails(oDV2.Dealer.ID)
                                                Exit For
                                            End If
                                       End If
                                    Next
                                End If
                            End If
                            RemainAllocQty -= oRD.RespondQty
                            l += 1
                        Next
                        RemainStock -= AllocQty
                        oDV2.SetRedemptionHeader(objRH, i - 1)
                        arlDataToDisplay(k) = oDV2
                    Next 'distribute allocqty to each RH & RD
                End If
                j += oDV1.TotalSameDV - 1 'Jump to next objDV with different DV
            Next 'arlDataToDisplay

            If RemainStock > 0 Then 'put it into next day, which is having estimationc stock >0 
                Dim IsFloatingProblem As Boolean = False
                If RemainStock = 1 Then
                    Dim TotRequest As Integer = 0
                    Dim TotRespond As Integer = 0

                    For Each oDV4Update As DealerVehicle In arlDataToDisplay
                        TotRequest += CType(oDV4Update.RedemptionHeaders(i - 1), RedemptionHeader).TotalRequest(oDV4Update.Dealer.ID)
                        TotRespond += CType(oDV4Update.RedemptionHeaders(i - 1), RedemptionHeader).TotalRespond(oDV4Update.Dealer.ID)
                    Next
                    If TotRespond < TotRequest Then
                        Dim IsUpdated As Boolean = False
                        For Each oDV4Update As DealerVehicle In arlDataToDisplay
                            Dim IdxRH4Update As Integer = 0
                            For Each oRH4Update As RedemptionHeader In odv4update.RedemptionHeaders
                                If IdxRH4Update = i - 1 Then
                                    Dim IdxRD4Update As Integer = 0
                                    For Each oRD4Update As RedemptionDetail In oRH4Update.RedemptionDetails(odv4update.Dealer.ID)
                                        If oRD4Update.RespondQty < oRD4Update.RequestQty Then
                                            CType(CType(oDV4Update.RedemptionHeaders(IdxRH4Update), RedemptionHeader).RedemptionDetails(odv4update.Dealer.ID)(IdxRD4Update), RedemptionDetail).RespondQty += 1
                                            'remove RD in next day which has been automatically added with unAllocatedRequest
                                            If (i - 1) < (nDays - 1) Then
                                                'CType(CType(oDV4Update.RedemptionHeaders(IdxRH4Update + 1), RedemptionHeader).RedemptionDetails(odv4update.Dealer.ID)(IdxRD4Update - IdxRD4Update), RedemptionDetail).RequestQty -= 1
                                                If CType(oDV4Update.RedemptionHeaders(nDays - 1), RedemptionHeader).RedemptionDetails.Count > 0 Then
                                                    CType(CType(oDV4Update.RedemptionHeaders(nDays - 1), RedemptionHeader).RedemptionDetails(odv4update.Dealer.ID)(IdxRD4Update - IdxRD4Update), RedemptionDetail).RequestQty -= 1
                                                End If
                                            End If
                                            IsUpdated = True
                                            IsFloatingProblem = True
                                            Exit For
                                        End If
                                        If IsUpdated Then Exit For
                                        IdxRD4Update += 1
                                    Next
                                    Exit For
                                End If
                                IdxRH4Update += 1
                            Next
                            If IsUpdated Then Exit For
                        Next
                    End If
                End If

                If Not IsFloatingProblem Then
                    For k = i + 1 To nDays - 1
                        objRH = CType(oDV1.RedemptionHeaders(k), RedemptionHeader)
                        If objRH.EstimationStock > 0 Then
                            For l = 0 To arlDataToDisplay.Count - 1 ' l = j To   oDV1.TotalSameDV - 1
                                Dim oDVTemp As DealerVehicle = CType(arlDataToDisplay(l), DealerVehicle)
                                Dim objRHTemp As RedemptionHeader = oDVTemp.RedemptionHeaders(k)

                                objRHTemp.EstimationStock += RemainStock
                                oDVTemp.SetRedemptionHeader(objRHTemp, k)
                                arlDataToDisplay(l) = oDVTemp
                            Next
                            Exit For
                        End If
                    Next
                End If

            End If 'end if RemainStock>0
        Next 'ndays

        sessHelper.SetSession("FrmEntryRedemptionPlan.arlDataToDisplay", arlDataToDisplay)
        BindDTGDistribute(dtgMain.CurrentPageIndex)
    End Sub

    Private Sub BindDTGDistribute(ByVal PageIndex As Integer)
        Dim arlData As New ArrayList
        Dim SelStatus As String
        Dim TotData As Integer

        sessHelper.SetSession("OnAutoDistribute", "1")
        arlData = sessHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        InitTotals()

        dtgMain.DataSource = arlData
        dtgMain.DataBind()
        sessHelper.SetSession("OnAutoDistribute", "0")
    End Sub

    Private Sub InitData()
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim dtTemp As Date
        Dim Stock As Integer
        Dim Request As Integer
        Dim objRH As RedemptionHeader
        Dim arlTemp As New ArrayList
        Dim arlTempCopy As New ArrayList
        Dim nTemp As Integer
        Dim oDV1 As DealerVehicle
        Dim oDV2 As DealerVehicle

        arlDataToDisplay = sessHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        If IsNothing(arlDataToDisplay) OrElse arlDataToDisplay.Count = 0 Then Exit Sub
        For Each oDV As DealerVehicle In arlDataToDisplay
            oDV.InitRedemptionHeaders(nDays)
            For i = 1 To nDays
                dtTemp = DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, i)
                objRH = GetRH(oDV.VechileColor.ID, dtTemp)
                oDV.SetRedemptionHeader(objRH, i - 1)
            Next
            arlTemp.Add(oDV)
            arlTempCopy.Add(oDV)
        Next

        For i = 0 To arlTemp.Count - 1
            oDV1 = CType(arlTemp(i), DealerVehicle)
            nTemp = 0
            For j = i To arlTemp.Count - 1
                oDV2 = CType(arlTemp(j), DealerVehicle)
                If oDV2.VechileColor.ID = oDV1.VechileColor.ID Then 'AndAlso oDV2.Dealer.ID = oDV1.Dealer.ID Then
                    nTemp += 1
                Else
                    Exit For
                End If
            Next
            For j = i To i + nTemp - 1
                oDV1 = CType(arlTemp(j), DealerVehicle)
                oDV1.TotalSameDV = nTemp
                arlTemp(j) = oDV1
                'arlTempCopy(j) = oDV1
            Next
        Next
        ''Simplify
        'For i = 0 To arlTemp.Count - 1
        '    oDV1 = CType(arlTemp(i), DealerVehicle)
        '    oDV1.TotalSameDV = txtKodeDealer.Text.Trim.Split(";").Length 
        'Next

        sessHelper.SetSession("FrmEntryRedemptionPlan.arlDataToDisplay", arlTemp)
        sessHelper.SetSession("arlDataToDisplayCopy", arlTempCopy)
    End Sub

    Private Sub BindddlTipeWarna(ByVal KodeTipe As String)
        ddlTipeWarna.Items.Clear()
        If ddlType.SelectedIndex > 0 Then

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, KodeTipe))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("MaterialNumber")) Then
                sortColl.Add(New Sort(GetType(VechileColor), "MaterialNumber", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            ddlTipeWarna.DataSource = New VechileColorFacade(User).Retrieve(criterias, sortColl)
            ddlTipeWarna.DataTextField = "MaterialNumber"
            ddlTipeWarna.DataValueField = "id"
            ddlTipeWarna.DataBind()
            ddlTipeWarna.Items.Insert(0, "Silahkan Pilih")
        End If
    End Sub


    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "Redemption Plan " & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim TraineeData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(TraineeData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(TraineeData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteData(sw, data)

                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If



            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder
        Dim i As Integer = 1
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Redemption Plan")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)

            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Dealer" & tab)
            itemLine.Append("Tipe/Warna" & tab)
            itemLine.Append("Deskripsi" & tab)
            itemLine.Append("OC/Unit" & tab)
            For i = 1 To nDays
                If dtgMain.Columns(i + 3).Visible Then
                    'itemLine.Append(dtgMain.Columns(i + 3).HeaderText & tab)
                    itemLine.Append(i.ToString & tab)
                End If
            Next
            itemLine.Append("Total" & tab)
            sw.WriteLine(itemLine.ToString())

            For Each di As DataGridItem In dtgMain.Items
                itemLine.Remove(0, itemLine.Length)

                itemLine.Append((di.ItemIndex + 1).ToString & tab)
                itemLine.Append(CType(di.FindControl("lblDealer"), Label).Text & tab)
                itemLine.Append(CType(di.FindControl("lblCode"), Label).Text & tab)
                itemLine.Append(CType(di.FindControl("lblDescription"), Label).Text & tab)
                itemLine.Append(CType(di.FindControl("lblOC"), Label).Text & tab)
                For i = 1 To nDays
                    If Me.dtgMain.Columns(i + 3).Visible Then
                        itemLine.Append(CType(di.FindControl("lblS" & i.ToString), Label).Text & tab)
                    End If
                Next
                itemLine.Append(di.Cells(dtgMain.Columns.Count - 1).Text & tab)
                sw.WriteLine(itemLine.ToString())
            Next
            If Not IsNothing(sessHelper.GetSession("FrmEntryRedemptionPlan.Footer")) Then
                Dim e As WebControls.DataGridItemEventArgs = CType(sessHelper.GetSession("FrmEntryRedemptionPlan.Footer"), WebControls.DataGridItemEventArgs)
                Dim sTemp As String = ""

                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                For i = 1 To nDays
                    If Me.dtgMain.Columns(i + 3).Visible Then
                        sTemp = e.Item.Cells(i + 3).Text
                        'sTemp = sTemp.Substring(0, sTemp.IndexOf(lblBreak.Text))
                        itemLine.Append(sTemp & tab)
                    End If
                Next
                sTemp = e.Item.Cells(dtgMain.Columns.Count - 1).Text
                'sTemp = sTemp.Substring(0, sTemp.IndexOf(lblBreak.Text))
                itemLine.Append(sTemp & tab)
                sw.WriteLine(itemLine.ToString())

                'itemLine.Remove(0, itemLine.Length)
                'itemLine.Append("" & tab)
                'itemLine.Append("" & tab)
                'itemLine.Append("" & tab)
                'itemLine.Append("" & tab)
                'itemLine.Append("" & tab)
                'For i = 1 To nDays
                '    If Me.dtgMain.Columns(i + 3).Visible Then
                '        sTemp = e.Item.Cells(i + 3).Text
                '        'sTemp = sTemp.Substring(sTemp.IndexOf(lblBreak.Text) + lblBreak.Text.Length)
                '        itemLine.Append(sTemp & tab)
                '    End If
                'Next
                'sTemp = e.Item.Cells(dtgMain.Columns.Count - 1).Text
                ''sTemp = sTemp.Substring(sTemp.IndexOf(lblBreak.Text) + lblBreak.Text.Length)
                'itemLine.Append(sTemp & tab)
                'sw.WriteLine(itemLine.ToString())
            End If
        End If
    End Sub



#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 700
        Response.Redirect("FrmEntryRedemptionPlan2.aspx")
        If Not IsPostBack Then
            Initialization()
        End If
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
        BindVehicleType(True)
    End Sub

    Private Sub ddlSubCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        BindVehicleType(False)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDTG(0)
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            'InitTotals()
            Dim i As Integer
            For i = 0 To 31
                Me.TotalStockPerColumns(i) = 0
            Next
            Me.sessHelper.SetSession(Me.sessTotalStockPerColumns, Me.TotalStockPerColumns)

            Dim nDays As Integer = viewstate.Item("nDays")
            For i = 1 To 31
                If i <= nDays Then
                    dtgMain.Columns(3 + i).Visible = True
                    'dtgMain.Columns(3 + i).HeaderText = i.ToString
                    Dim lblH As Label = e.Item.FindControl("lblH" & i.ToString)
                    lblH.Text = i.ToString
                Else
                    dtgMain.Columns(3 + i).Visible = False
                End If
            Next

        ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            FillDTGRow(e)
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            FillDTGFooter(e)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not SaveData() Then
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Sub dtgMain_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMain.PageIndexChanged
        BindDTG(e.NewPageIndex)
    End Sub

    Private Sub btnDistribute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDistribute.Click
        AutoDistribute()
    End Sub

    Private Sub ddlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlType.SelectedIndexChanged
        BindddlTipeWarna(ddlType.SelectedItem.Text)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        arlDataToDisplay = sessHelper.GetSession("FrmEntryRedemptionPlan.arlDataToDisplay")
        DoDownload(arlDataToDisplay)
    End Sub

    Private Sub btnRefreshGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshGrid.Click
        Dim RowIndex As Integer = 0

        Try
            RowIndex = CType(Me.txtRowIndex.Text, Integer)
        Catch ex As Exception
            RowIndex = -1
        End Try
        If RowIndex >= 0 Then
            Dim dgie As New System.Web.UI.WebControls.DataGridItemEventArgs(Me.dtgMain.Items(RowIndex))

            Me.FillDTGRow(dgie)
            Me.txtRowIndex.Text = "-1"
        End If
    End Sub

#End Region

#Region "Custom Class"
    Public Class DealerVehicle

        Private _iD As Integer
        Private _dealer As Dealer
        Private _vechileColor As VechileColor
        Private _temp As Integer
        Private _redemptionHeaders As New ArrayList
        Private _totalSameDV As Integer

        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal Value As Integer)
                _iD = Value
            End Set
        End Property
        Public Property Dealer() As Dealer
            Get
                Return _dealer
            End Get
            Set(ByVal Value As Dealer)
                _dealer = Value
            End Set
        End Property

        Public Property VechileColor() As VechileColor
            Get
                Return _vechileColor
            End Get
            Set(ByVal Value As VechileColor)
                _vechileColor = Value
            End Set
        End Property

        Public Property RedemptionHeaders() As ArrayList
            Get
                Return Me._redemptionHeaders
            End Get
            Set(ByVal Value As ArrayList)
                Me._redemptionHeaders = Value
            End Set
        End Property

        Public Function SetRedemptionHeader(ByVal oRH As RedemptionHeader, ByVal Idx As Integer)
            If Idx < 0 Then
                Me._redemptionHeaders.Add(oRH)
            Else
                Me._redemptionHeaders(Idx) = oRH
            End If
        End Function

        Public Sub InitRedemptionHeaders(ByVal nDay As Integer)
            Dim i As Integer

            Me._redemptionHeaders.Clear()
            For i = 0 To nDay - 1
                Me._redemptionHeaders.Add(New RedemptionHeader)
            Next
        End Sub

        Public Property TotalSameDV() As Integer
            Get
                Return _totalSameDV
            End Get
            Set(ByVal Value As Integer)
                _totalSameDV = Value
            End Set
        End Property

    End Class
#End Region
End Class
