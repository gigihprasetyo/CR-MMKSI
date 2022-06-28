
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

Public Class FrmEntryRedemptionPlan2
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
    Protected WithEvents txtRHID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDayIndex As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIsUpdated As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtsDay As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtsTotRow As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtsTotCol As System.Web.UI.WebControls.TextBox
    Protected WithEvents imgCheckedDefault As System.Web.UI.WebControls.Image

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "Variables"
    Private sessHelper As New SessionHelper
    Private arlDataToDisplay As New ArrayList
    Private sessTotalStockPerColumns As String = "FrmEntryRedemptionPlan.TotalPerColumns"
    Private TotalStockPerColumns(31) As Integer
    Private _sessData As String = "FrmEntryRedemptionPlan.arlDataToDisplay"
    Private _sessVCIDs As String = "FrmEntryRedemptionPlan._sessVCIDs"
    Private _sessTotReqPerCols As String = "FrmEntryRedemptionPlan._sessTotReqPerCols"
    Private _sessTotResPerCols As String = "FrmEntryRedemptionPlan._sessTotResPerCols"
    Private _TotReqPerCols(31) As Integer
    Private _TotResPerCols(31) As Integer
    Private _vstEnableDblClickByPrivilege As String = "_vstEnableDblClickByPrivilege"
    Private _sessCategoryCode As String = "FrmEntryRedemptionPlan._sessCategoryCode"
    Private _sessSubCategoryCode As String = "FrmEntryRedemptionPlan._sessSubCategoryCode"
#End Region

#Region "Methods"

    Private Sub CheckPrivelege()
        Dim oDealer As Dealer = CType(Session.Item("DEALER"), Dealer)

        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(Context.User, SR.input_redemption_plan_privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Input Redemption Plan")
            End If
            Me.btnSearch.Enabled = SecurityProvider.Authorize(Context.User, SR.input_redemption_plan_lihat_privilege)
            If IsNothing(Me.ViewState.Item(Me._vstEnableDblClickByPrivilege)) Then
                Me.ViewState.Add(Me._vstEnableDblClickByPrivilege, 0)
            End If
            If SecurityProvider.Authorize(Context.User, SR.input_redemption_plan_ubah_privilege) Then
                Me.ViewState.Item(Me._vstEnableDblClickByPrivilege) = 1
            Else
                Me.ViewState.Item(Me._vstEnableDblClickByPrivilege) = 0
            End If
            Me.btnDownload.Enabled = SecurityProvider.Authorize(Context.User, SR.input_redemption_plan_download_privilege)
        ElseIf oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If Not SecurityProvider.Authorize(Context.User, SR.respon_redemption_plan_privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Respon Redemption Plan")
            End If
            Me.btnSearch.Enabled = SecurityProvider.Authorize(context.User, SR.respon_redemption_plan_lihat_privilege)
            If IsNothing(Me.ViewState.Item(Me._vstEnableDblClickByPrivilege)) Then
                Me.ViewState.Add(Me._vstEnableDblClickByPrivilege, 0)
            End If
            If SecurityProvider.Authorize(Context.User, SR.respon_redemption_plan_ubah_privilege) Then
                Me.ViewState.Item(Me._vstEnableDblClickByPrivilege) = 1
            Else
                Me.ViewState.Item(Me._vstEnableDblClickByPrivilege) = 0
            End If
            Me.btnDistribute.Enabled = SecurityProvider.Authorize(context.User, SR.respon_redemption_plan_auto_privilege)
            Me.btnSave.Enabled = SecurityProvider.Authorize(context.User, SR.respon_redemption_plan_auto_privilege)

            Me.btnDownload.Enabled = SecurityProvider.Authorize(context.User, SR.respon_redemption_plan_download_privilege)
        Else
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Input Redemption Plan")
        End If
        'Me.btnSetContract.Enabled = SecurityProvider.Authorize(Context.User, SR.ubah_max_contract_date_redemption_privilege)
        'Me.btnSave.Enabled = SecurityProvider.Authorize(Context.User, SR.simpan_estimation_stok_redemption_privilege)
        'Me.btnDownload.Enabled = SecurityProvider.Authorize(Context.User, SR.download_estimation_stok_redemption_privilege)
    End Sub

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
            If Me.IsStillOpen() = False Then
                Me.btnDistribute.Enabled = False
                Me.btnSave.Enabled = False
            End If
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
        If Me.IsStillOpen() = False Then
            Me.btnSave.Enabled = False
            Me.btnDistribute.Enabled = False
        End If
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
                BindDTG()
            End If
        Else
            sessHelper.SetSession("FrmEntryRedemptionPlan.arlDataToDisplay", New ArrayList)
        End If
    End Sub

    Private Sub ClearTempRedDetail()
        Dim cRD As New CriteriaComposite(New Criteria(GetType(RedemptionDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim oRDFac As New RedemptionDetailFacade(User)
        Dim aRDs As ArrayList
        Dim i As Integer
        Dim oRD As RedemptionDetail
        Dim Sql As String = ""

        SetInProcess(0)

        'Sql &= " select ID from RedemptionHeader rh "
        'Sql &= " where 1=1 "
        'Sql &= " " & "and rh.RowStatus=0" & " "
        'Sql &= " " & "and Year(rh.PeriodDate) = " & CType(Me.ddlYear.SelectedValue, Integer).ToString & " "
        'Sql &= " " & "and Month(rh.PeriodDate) = " & CType(Me.ddlMonth.SelectedValue, Integer).ToString & " "


        'cRD.opAnd(New Criteria(GetType(RedemptionDetail), "IsInProcess", MatchType.Exact, 1))
        'cRD.opAnd(New Criteria(GetType(RedemptionDetail), "RedemptionHeader.ID", MatchType.InSet, "(" & Sql & ")"))
        'aRDs = oRDFac.Retrieve(cRD)
        'For i = 0 To aRDs.Count - 1
        '    oRD = CType(aRDs(i), RedemptionDetail)
        '    oRD.IsInProcess = 0
        '    oRD.RequestQty = oRD.OriRequestQty
        '    oRD.RespondQty = oRD.OriRespondQty
        '    oRD.OriRequestQty = 0
        '    oRD.OriRespondQty = 0
        '    oRDFac.Update(oRD)
        'Next
    End Sub

    Private Sub CommitTempRedDetail()
        Dim cRD As New CriteriaComposite(New Criteria(GetType(RedemptionDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim oRDFac As New RedemptionDetailFacade(User)
        Dim aRDs As ArrayList
        Dim i As Integer
        Dim oRD As RedemptionDetail
        Dim Sql As String = ""

        Sql &= " select ID from RedemptionHeader rh "
        Sql &= " where 1=1 "
        Sql &= " " & "and rh.RowStatus=0" & " "
        Sql &= " " & "and Year(rh.PeriodDate) = " & CType(Me.ddlYear.SelectedValue, Integer).ToString & " "
        Sql &= " " & "and Month(rh.PeriodDate) = " & CType(Me.ddlMonth.SelectedValue, Integer).ToString & " "


        cRD.opAnd(New Criteria(GetType(RedemptionDetail), "IsInProcess", MatchType.Exact, 1))
        cRD.opAnd(New Criteria(GetType(RedemptionDetail), "RedemptionHeader.ID", MatchType.InSet, "(" & Sql & ")"))
        aRDs = oRDFac.Retrieve(cRD)
        For i = 0 To aRDs.Count - 1
            oRD = CType(aRDs(i), RedemptionDetail)
            oRD.IsInProcess = 0
            oRDFac.Update(oRD)
        Next
    End Sub


    Private Function IsAnotherAlsoProcessing() As Boolean
        
        Dim oRSFac As New RedemptionStatusFacade(User)
        Dim IsOpen As Boolean
        Dim dtPeriod As Date

        Try
            dtPeriod = DateSerial(CType(Me.ddlYear.SelectedValue, Integer), CType(Me.ddlMonth.SelectedValue, Integer), 1)
        Catch ex As Exception
            dtPeriod = DateSerial(3000, 1, 1)
        End Try

        IsOpen = oRSFac.IsInProcess(dtPeriod, Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text)

        Return IsOpen
    End Function

    Private Sub BindDTG(Optional ByVal IsForDownload As Boolean = False)
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
        If viewstate.Item("AutoRefresh") = "1" Then
            arlDataToDisplay = sessHelper.GetSession(Me._sessData)
            arlData = arlDataToDisplay
        Else
            arlData = GetData(TotData)
        End If
        dtgMain.DataSource = arlData
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
        'criterias.opAnd(New Criteria(GetType(VechileColor), "SpecialFlag", MatchType.Exact, ""))
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
        Dim objD As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
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
        Dim cVR As New CriteriaComposite(New Criteria(GetType(v_Redemption), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sVR As New SortCollection
        Dim oVRFac As New v_RedemptionFacade(User)
        Dim aVRs As ArrayList
        Dim sqlD As String = "('" & Me.txtKodeDealer.Text.Trim.Replace(";", "','") & "')"
        Dim sqlVC As String = ""
        Dim i As Integer
        Dim oVC As VechileColor

        For i = 0 To ColorList.Count - 1
            oVC = CType(ColorList(i), VechileColor)
            sqlVC &= IIf(sqlVC.Trim = "", "", ",") & oVC.ID.ToString
        Next
        sqlVC = "(" & sqlVC & ")"
        Me.sessHelper.SetSession(Me._sessVCIDs, sqlVC)
        'Row Filtering
        If objD.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            'Stock>0 handled by view /SQL
        Else
            'Stock>0 handled by view /SQL
            'HavingRequest
            cVR.opAnd(New Criteria(GetType(v_Redemption), "TotalRequest", MatchType.Greater, 0))
        End If

        cVR.opAnd(New Criteria(GetType(v_Redemption), "PeriodYear", MatchType.Exact, CType(Me.ddlYear.SelectedValue, Integer)))
        cVR.opAnd(New Criteria(GetType(v_Redemption), "PeriodMonth", MatchType.Exact, CType(Me.ddlMonth.SelectedValue, Integer)))
        If sqlD.Trim <> String.Empty AndAlso sqlD.Trim <> "()" Then
            cVR.opAnd(New Criteria(GetType(v_Redemption), "DealerCode", MatchType.InSet, sqlD))
        End If
        If sqlVC.Trim = "()" Then sqlVC = "(0)"
        If sqlVC.Trim <> String.Empty AndAlso sqlVC.Trim <> "()" Then
            cVR.opAnd(New Criteria(GetType(v_Redemption), "VechileColorID", MatchType.InSet, sqlVC))
        End If

        sVR.Add(New Sort(GetType(v_Redemption), "VehicleCode", Sort.SortDirection.ASC))
        sVR.Add(New Sort(GetType(v_Redemption), "DealerCode", Sort.SortDirection.ASC))

        Dim aTemps As ArrayList = oVRFac.Retrieve(cVR, sVR)
        'remove TotalOC=0
        Dim oVR As v_Redemption
        aVRs = New ArrayList
        For i = 0 To aTemps.Count - 1
            oVR = DirectCast(aTemps(i), v_Redemption)
            If oVR.TotalOC > 0 Then aVRs.Add(oVR)
        Next
        sessHelper.SetSession(Me._sessData, aVRs)
        Return aVRs
    End Function

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

            Dim aTemps As ArrayList = New VechileTypeFacade(User).RetrieveByCriteria(criterias, sortColl)
            Dim aVTs As ArrayList = New ArrayList
            Dim oVT As VechileType
            Dim cVC As CriteriaComposite
            Dim oVCFac As New VechileColorFacade(User)
            Dim aVCs As ArrayList

            For i As Integer = 0 To aTemps.Count - 1
                oVT = CType(aTemps(i), VechileType)
                cVC = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cVC.opAnd(New Criteria(GetType(VechileColor), "Status", MatchType.No, "X"))
                cVC.opAnd(New Criteria(GetType(VechileColor), "SpecialFlag", MatchType.Exact, ""))
                cVC.opAnd(New Criteria(GetType(VechileColor), "ColorCode", MatchType.No, "zzzz"))
                cVC.opAnd(New Criteria(GetType(VechileColor), "VechileType.ID", MatchType.Exact, oVT.ID))

                aVCs = oVCFac.Retrieve(cVC)
                If aVCs.Count > 0 Then
                    aVTs.Add(oVT)
                End If
            Next


            '-- Bind Vehicle type dropdownlist
            ddlType.DataSource = aVTs
            ddlType.DataTextField = "VechileTypeCode"
            ddlType.DataValueField = "VechileTypeCode"
            ddlType.DataBind()
        End If
        ddlType.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item
    End Sub


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
            If Not IsNothing(oTC) AndAlso oTC.ID > 0 Then
                viewstate.Item("TransactionControl.Status") = oTC.Status
            Else
                viewstate.Item("TransactionControl.Status") = EnumDealerStatus.DealerStatus.NonAktive
            End If
        Else
            viewstate.Item("TransactionControl.Status") = EnumDealerStatus.DealerStatus.Aktive
        End If

    End Sub

    Private Sub BindddlTipeWarna(ByVal KodeTipe As String)
        ddlTipeWarna.Items.Clear()
        If ddlType.SelectedIndex > 0 Then

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, KodeTipe))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "Status", MatchType.No, "X"))

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

    Private Function IsHavingStock(ByVal Day As Integer) As Boolean
        Dim cVR As New CriteriaComposite(New Criteria(GetType(v_Redemption), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aVR As New Aggregate(GetType(v_Redemption), "E" & Day.ToString, AggregateType.Sum)
        Dim nStock As Integer = 0
        Dim sqlVC As String = Me.sessHelper.GetSession(Me._sessVCIDs)

        cVR.opAnd(New Criteria(GetType(v_Redemption), "PeriodYear", MatchType.Exact, CType(Me.ddlYear.SelectedValue, Integer)))
        cVR.opAnd(New Criteria(GetType(v_Redemption), "PeriodMonth", MatchType.Exact, CType(Me.ddlMonth.SelectedValue, Integer)))
        cVR.opAnd(New Criteria(GetType(v_Redemption), "VechileColorID", MatchType.InSet, sqlVC))
        Try
            nStock = CType(New v_RedemptionFacade(User).RetrieveScalar(cVR, aVR), Integer)
        Catch ex As Exception
            nStock = 0
        End Try
        Return (nStock > 0)
    End Function


    Private Function IsHavingRequest(ByVal Day As Integer) As Boolean
        Dim cVR As New CriteriaComposite(New Criteria(GetType(v_Redemption), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aVR As New Aggregate(GetType(v_Redemption), "R" & Day.ToString, AggregateType.Sum)
        Dim nStock As Integer = 0
        Dim sqlVC As String = Me.sessHelper.GetSession(Me._sessVCIDs)

        cVR.opAnd(New Criteria(GetType(v_Redemption), "PeriodYear", MatchType.Exact, CType(Me.ddlYear.SelectedValue, Integer)))
        cVR.opAnd(New Criteria(GetType(v_Redemption), "PeriodMonth", MatchType.Exact, CType(Me.ddlMonth.SelectedValue, Integer)))
        cVR.opAnd(New Criteria(GetType(v_Redemption), "VechileColorID", MatchType.InSet, sqlVC))
        Try
            nStock = CType(New v_RedemptionFacade(User).RetrieveScalar(cVR, aVR), Integer)
        Catch ex As Exception
            nStock = 0
        End Try
        Return (nStock > 0)
    End Function
    Private Sub GetRedComponent(ByRef oVR As v_Redemption, ByVal Day As Integer, ByRef RHID As Integer, ByRef Plan As Integer, ByRef Req As Integer, ByRef Res As Integer)
        If Day < 1 OrElse Day > 31 Then Day = 0
        Select Case Day
            Case 0 : RHID = 0 : Plan = 0 : Req = 0 : Res = 0
            Case 1 : RHID = oVR.RH1 : Plan = oVR.E1 : Req = oVR.R1 : Res = oVR.A1
            Case 2 : RHID = oVR.RH2 : Plan = oVR.E2 : Req = oVR.R2 : Res = oVR.A2
            Case 3 : RHID = oVR.RH3 : Plan = oVR.E3 : Req = oVR.R3 : Res = oVR.A3
            Case 4 : RHID = oVR.RH4 : Plan = oVR.E4 : Req = oVR.R4 : Res = oVR.A4
            Case 5 : RHID = oVR.RH5 : Plan = oVR.E5 : Req = oVR.R5 : Res = oVR.A5
            Case 6 : RHID = oVR.RH6 : Plan = oVR.E6 : Req = oVR.R6 : Res = oVR.A6
            Case 7 : RHID = oVR.RH7 : Plan = oVR.E7 : Req = oVR.R7 : Res = oVR.A7
            Case 8 : RHID = oVR.RH8 : Plan = oVR.E8 : Req = oVR.R8 : Res = oVR.A8
            Case 9 : RHID = oVR.RH9 : Plan = oVR.E9 : Req = oVR.R9 : Res = oVR.A9
            Case 10 : RHID = oVR.RH10 : Plan = oVR.E10 : Req = oVR.R10 : Res = oVR.A10
            Case 11 : RHID = oVR.RH11 : Plan = oVR.E11 : Req = oVR.R11 : Res = oVR.A11
            Case 12 : RHID = oVR.RH12 : Plan = oVR.E12 : Req = oVR.R12 : Res = oVR.A12
            Case 13 : RHID = oVR.RH13 : Plan = oVR.E13 : Req = oVR.R13 : Res = oVR.A13
            Case 14 : RHID = oVR.RH14 : Plan = oVR.E14 : Req = oVR.R14 : Res = oVR.A14
            Case 15 : RHID = oVR.RH15 : Plan = oVR.E15 : Req = oVR.R15 : Res = oVR.A15
            Case 16 : RHID = oVR.RH16 : Plan = oVR.E16 : Req = oVR.R16 : Res = oVR.A16
            Case 17 : RHID = oVR.RH17 : Plan = oVR.E17 : Req = oVR.R17 : Res = oVR.A17
            Case 18 : RHID = oVR.RH18 : Plan = oVR.E18 : Req = oVR.R18 : Res = oVR.A18
            Case 19 : RHID = oVR.RH19 : Plan = oVR.E19 : Req = oVR.R19 : Res = oVR.A19
            Case 20 : RHID = oVR.RH20 : Plan = oVR.E20 : Req = oVR.R20 : Res = oVR.A20
            Case 21 : RHID = oVR.RH21 : Plan = oVR.E21 : Req = oVR.R21 : Res = oVR.A21
            Case 22 : RHID = oVR.RH22 : Plan = oVR.E22 : Req = oVR.R22 : Res = oVR.A22
            Case 23 : RHID = oVR.RH23 : Plan = oVR.E23 : Req = oVR.R23 : Res = oVR.A23
            Case 24 : RHID = oVR.RH24 : Plan = oVR.E24 : Req = oVR.R24 : Res = oVR.A24
            Case 25 : RHID = oVR.RH25 : Plan = oVR.E25 : Req = oVR.R25 : Res = oVR.A25
            Case 26 : RHID = oVR.RH26 : Plan = oVR.E26 : Req = oVR.R26 : Res = oVR.A26
            Case 27 : RHID = oVR.RH27 : Plan = oVR.E27 : Req = oVR.R27 : Res = oVR.A27
            Case 28 : RHID = oVR.RH28 : Plan = oVR.E28 : Req = oVR.R28 : Res = oVR.A28
            Case 29 : RHID = oVR.RH29 : Plan = oVR.E29 : Req = oVR.R29 : Res = oVR.A29
            Case 30 : RHID = oVR.RH30 : Plan = oVR.E30 : Req = oVR.R30 : Res = oVR.A30
            Case 31 : RHID = oVR.RH31 : Plan = oVR.E31 : Req = oVR.R31 : Res = oVR.A31

        End Select
    End Sub

    Public Sub UpdateTotalQtyPublic(ByRef oVR As v_Redemption, ByVal DayIdx As Integer, ByVal DiffEst As Integer, ByVal DiffReq As Integer, ByVal DiffRes As Integer)
        UpdateTotalQty(oVR, DayIdx, DiffEst, DiffReq, DiffRes)
    End Sub

    Private Sub UpdateTotalQty(ByRef oVR As v_Redemption, ByVal DayIdx As Integer, ByVal DiffEst As Integer, ByVal DiffReq As Integer, ByVal DiffRes As Integer)
        'DiffEst = Math.Abs(DiffEst)
        'DiffReq = Math.Abs(DiffReq)
        'DiffRes = Math.Abs(DiffRes)
        If Not IsNothing(DiffEst) Then
            Select Case (DayIdx + 1)
                Case 1 : oVR.E1 = oVR.E1 + DiffEst : oVR.R1 = oVR.R1 + DiffReq : oVR.A1 = oVR.A1 + DiffRes
                Case 2 : oVR.E2 = oVR.E2 + DiffEst : oVR.R2 = oVR.R2 + DiffReq : oVR.A2 = oVR.A2 + DiffRes
                Case 3 : oVR.E3 = oVR.E3 + DiffEst : oVR.R3 = oVR.R3 + DiffReq : oVR.A3 = oVR.A3 + DiffRes
                Case 4 : oVR.E4 = oVR.E4 + DiffEst : oVR.R4 = oVR.R4 + DiffReq : oVR.A4 = oVR.A4 + DiffRes
                Case 5 : oVR.E5 = oVR.E5 + DiffEst : oVR.R5 = oVR.R5 + DiffReq : oVR.A5 = oVR.A5 + DiffRes
                Case 6 : oVR.E6 = oVR.E6 + DiffEst : oVR.R6 = oVR.R6 + DiffReq : oVR.A6 = oVR.A6 + DiffRes
                Case 7 : oVR.E7 = oVR.E7 + DiffEst : oVR.R7 = oVR.R7 + DiffReq : oVR.A7 = oVR.A7 + DiffRes
                Case 8 : oVR.E8 = oVR.E8 + DiffEst : oVR.R8 = oVR.R8 + DiffReq : oVR.A8 = oVR.A8 + DiffRes
                Case 9 : oVR.E9 = oVR.E9 + DiffEst : oVR.R9 = oVR.R9 + DiffReq : oVR.A9 = oVR.A9 + DiffRes
                Case 10 : oVR.E10 = oVR.E10 + DiffEst : oVR.R10 = oVR.R10 + DiffReq : oVR.A10 = oVR.A10 + DiffRes
                Case 11 : oVR.E11 = oVR.E11 + DiffEst : oVR.R11 = oVR.R11 + DiffReq : oVR.A11 = oVR.A11 + DiffRes
                Case 12 : oVR.E12 = oVR.E12 + DiffEst : oVR.R12 = oVR.R12 + DiffReq : oVR.A12 = oVR.A12 + DiffRes
                Case 13 : oVR.E13 = oVR.E13 + DiffEst : oVR.R13 = oVR.R13 + DiffReq : oVR.A13 = oVR.A13 + DiffRes
                Case 14 : oVR.E14 = oVR.E14 + DiffEst : oVR.R14 = oVR.R14 + DiffReq : oVR.A14 = oVR.A14 + DiffRes
                Case 15 : oVR.E15 = oVR.E15 + DiffEst : oVR.R15 = oVR.R15 + DiffReq : oVR.A15 = oVR.A15 + DiffRes
                Case 16 : oVR.E16 = oVR.E16 + DiffEst : oVR.R16 = oVR.R16 + DiffReq : oVR.A16 = oVR.A16 + DiffRes
                Case 17 : oVR.E17 = oVR.E17 + DiffEst : oVR.R17 = oVR.R17 + DiffReq : oVR.A17 = oVR.A17 + DiffRes
                Case 18 : oVR.E18 = oVR.E18 + DiffEst : oVR.R18 = oVR.R18 + DiffReq : oVR.A18 = oVR.A18 + DiffRes
                Case 19 : oVR.E19 = oVR.E19 + DiffEst : oVR.R19 = oVR.R19 + DiffReq : oVR.A19 = oVR.A19 + DiffRes
                Case 20 : oVR.E20 = oVR.E20 + DiffEst : oVR.R20 = oVR.R20 + DiffReq : oVR.A20 = oVR.A20 + DiffRes
                Case 21 : oVR.E21 = oVR.E21 + DiffEst : oVR.R21 = oVR.R21 + DiffReq : oVR.A21 = oVR.A21 + DiffRes
                Case 22 : oVR.E22 = oVR.E22 + DiffEst : oVR.R22 = oVR.R22 + DiffReq : oVR.A22 = oVR.A22 + DiffRes
                Case 23 : oVR.E23 = oVR.E23 + DiffEst : oVR.R23 = oVR.R23 + DiffReq : oVR.A23 = oVR.A23 + DiffRes
                Case 24 : oVR.E24 = oVR.E24 + DiffEst : oVR.R24 = oVR.R24 + DiffReq : oVR.A24 = oVR.A24 + DiffRes
                Case 25 : oVR.E25 = oVR.E25 + DiffEst : oVR.R25 = oVR.R25 + DiffReq : oVR.A25 = oVR.A25 + DiffRes
                Case 26 : oVR.E26 = oVR.E26 + DiffEst : oVR.R26 = oVR.R26 + DiffReq : oVR.A26 = oVR.A26 + DiffRes
                Case 27 : oVR.E27 = oVR.E27 + DiffEst : oVR.R27 = oVR.R27 + DiffReq : oVR.A27 = oVR.A27 + DiffRes
                Case 28 : oVR.E28 = oVR.E28 + DiffEst : oVR.R28 = oVR.R28 + DiffReq : oVR.A28 = oVR.A28 + DiffRes
                Case 29 : oVR.E29 = oVR.E29 + DiffEst : oVR.R29 = oVR.R29 + DiffReq : oVR.A29 = oVR.A29 + DiffRes
                Case 30 : oVR.E30 = oVR.E30 + DiffEst : oVR.R30 = oVR.R30 + DiffReq : oVR.A30 = oVR.A30 + DiffRes
                Case 31 : oVR.E31 = oVR.E31 + DiffEst : oVR.R31 = oVR.R31 + DiffReq : oVR.A31 = oVR.A31 + DiffRes

            End Select
        End If
    End Sub

    Private Sub AutoDistribute()
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)
        Dim aVRs As ArrayList = CType(Me.sessHelper.GetSession(Me._sessData), ArrayList)
        Dim DayIdx As Integer = 0, RowIdx As Integer = 0, RowIdxSameVC As Integer = 0, RDIdx As Integer = 0, NextDayIdx As Integer = 0, NextRDIdx As Integer = 0, nSame As Integer = 0, OriRowIdx As Integer = 0
        Dim CurVehicleCode As String = String.Empty, EstStock As Integer = 0, EstStockRemain As Integer = 0, TotAllocForDealerInDec As Decimal = 0, TotAllocForDealer As Integer = 0, TotAllocForDealerRemain As Integer = 0, nDiff As Integer = 0
        Dim TotUnAllocatedForDealer As Integer = 0, IsAlreadyMoved As Boolean = False
        Dim TotReqVCPerDealer As Integer = 0, TotReqVCAllDealer As Integer = 0
        Dim oVR As v_Redemption, oVRSameVC As v_Redemption, oRD As RedemptionDetail, oRHNextDay As RedemptionHeader, oRDNextDay As RedemptionDetail
        Dim nDiffByCeiling As Integer
        Dim oRDFac As New RedemptionDetailFacade(User)
        Dim oRSFac As New redemptionstatusfacade(user)
        Dim LastStatus As Integer

        LastStatus = oRSFac.GetCurrentStatus(Me.GetSelectedDate(), Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text)

        oRSFac.SetInProcessStatus(Me.GetSelectedDate(), Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text, 1)
        Dim sIDs As String = String.Empty
        Dim Sql As String
        Dim oSPFac As New SPFacade(User)
        Dim LastCode As String
        Dim Ceiling As Decimal = 0
        Dim Sequence As Integer = 0

        For DayIdx = 0 To nDays - 1
            If Me.dtgMain.Columns(5 + DayIdx).Visible Then
                For RowIdx = 0 To aVRs.Count - 1
                    oVR = CType(aVRs(RowIdx), v_Redemption)
                    If LastCode <> oVR.VehicleCode Then
                        LastCode = oVR.VehicleCode
                        For Each oRH As RedemptionHeader In oVR.RedemptionHeaders
                            If orh.PeriodDate.Day = (DayIdx + 1) Then
                                sIDs &= IIf(sIDs.Trim = String.Empty, "", ",") & orh.ID.ToString

                                If sIDs.Length > 7500 Then
                                    Sql = "exec sp_SetRedemptionOriginal '" & sIDs & "'," & IIf(LastStatus = 0, 1, 0) & " "
                                    If oSPFac.ExecuteSP(Sql) = False Then
                                        MessageBox.Show("Proses Auto-Distribute Gagal. Silahkan Tunggu Beberapa Saat Atau Hubungi Admin DNET")
                                        oRSFac.SetInProcessStatus(Me.GetSelectedDate(), Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text, 2)
                                        Exit Sub
                                    End If
                                    sIDs = String.Empty
                                End If

                                'For Each oRDTemp As RedemptionDetail In orh.RedemptionDetails
                                '    oRDTemp.IsInProcess = 1
                                '    oRDTemp.OriRequestQty = oRDTemp.RequestQty
                                '    oRDTemp.OriRespondQty = oRDTemp.RespondQty
                                '    oRDFac.Update(oRDTemp)
                                'Next
                            End If
                        Next
                    End If
                Next
                'Exit For
            End If
        Next
        If sIDs.Length > 0 Then
            Sql = "exec sp_SetRedemptionOriginal '" & sIDs & "'," & IIf(LastStatus = 0, 1, 0) & " "
            If oSPFac.ExecuteSP(Sql) = False Then
                MessageBox.Show("Proses Auto-Distribute Gagal. Silahkan Tunggu Beberapa Saat Atau Hubungi Admin DNET")
                oRSFac.SetInProcessStatus(Me.GetSelectedDate(), Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text, 2)
                Exit Sub
            End If
            sIDs = String.Empty
        End If

        'Exit Sub
        Me.ClearUsedCeiling()
        'move est stock to the first visible column
        IsAlreadyMoved = False
        For DayIdx = 0 To nDays - 1
            If Me.dtgMain.Columns(5 + DayIdx).Visible Then
                Exit For
            Else
                If (DayIdx + 1) + 1 <= nDays Then
                    For RowIdx = 0 To aVRs.Count - 1
                        oVR = CType(aVRs(RowIdx), v_Redemption)
                        Dim oRH As RedemptionHeader = CType(oVR.RedemptionHeaders(DayIdx), RedemptionHeader)
                        EstStock = oRH.EstimationStock
                        If EstStock > 0 Then
                            'decrease current day
                            nDiff = EstStock
                            oRH.estimationstock -= EstStock
                            CType(aVRs(RowIdx), v_Redemption).RedemptionHeaders(DayIdx) = oRH
                            Me.UpdateTotalQty(aVRs(RowIdx), DayIdx, -nDiff, 0, 0)

                            'increase next day
                            orh = CType(oVR.RedemptionHeaders(DayIdx + 1), RedemptionHeader)
                            orh.EstimationStock += EstStock
                            CType(aVRs(RowIdx), v_Redemption).RedemptionHeaders(DayIdx + 1) = oRH
                            Me.UpdateTotalQty(aVRs(RowIdx), DayIdx + 1, nDiff, 0, 0)
                        End If
                    Next
                End If
            End If
        Next
        For DayIdx = 0 To nDays - 1
            If Me.dtgMain.Columns(5 + DayIdx).Visible Then
                For RowIdx = 0 To aVRs.Count - 1
                    oVR = CType(aVRs(RowIdx), v_Redemption)
                    OriRowIdx = RowIdx
                    If oVR.VehicleCode <> CurVehicleCode Then CurVehicleCode = oVR.VehicleCode
                    EstStock = CType(oVR.RedemptionHeaders(DayIdx), RedemptionHeader).EstimationStock
                    EstStock -= CType(oVR.RedemptionHeaders(DayIdx), RedemptionHeader).TotalRespondManual()
                    If EstStock > 0 Then
                        EstStockRemain = EstStock
                        nSame = 0
                        Sequence = 0
                        For RowIdxSameVC = RowIdx To aVRs.Count - 1
                            oVRSameVC = CType(aVRs(RowIdxSameVC), v_Redemption)
                            If oVRSameVC.VehicleCode = oVR.VehicleCode Then
                                If RowIdxSameVC > RowIdx Then RowIdx = RowIdxSameVC
                                nSame += 1
                                If CType(oVRSameVC.RedemptionHeaders(DayIdx), RedemptionHeader).ID > 0 Then
                                    'Initiate AllocQty for each Dealer
                                    TotReqVCPerDealer = CType(oVRSameVC.RedemptionHeaders(DayIdx), RedemptionHeader).TotalRequest(oVRSameVC.DealerID, False)
                                    TotReqVCAllDealer = CType(oVRSameVC.RedemptionHeaders(DayIdx), RedemptionHeader).TotalRequest(0, False)
                                    'EstStock -= CType(oVRSameVC.RedemptionHeaders(DayIdx), RedemptionHeader).TotalRespondManual()
                                    Try
                                        TotAllocForDealer = TotReqVCPerDealer / TotReqVCAllDealer * EstStock

                                        TotAllocForDealerInDec = CType(TotReqVCPerDealer, Decimal) / CType(TotReqVCAllDealer, Decimal) * CType(EstStock, Decimal)
                                        If TotAllocForDealerInDec >= (CType(TotAllocForDealer, Decimal) + 0.5) Then TotAllocForDealer += 1
                                    Catch ex As Exception
                                        TotAllocForDealer = 0
                                        TotAllocForDealerInDec = 0
                                    End Try
                                    If TotAllocForDealer > TotReqVCPerDealer Then TotAllocForDealer = TotReqVCPerDealer
                                    If TotAllocForDealer > EstStockRemain Then TotAllocForDealer = EstStockRemain

                                    TotAllocForDealerRemain = TotAllocForDealer
                                    TotUnAllocatedForDealer = 0
                                    Dim StockBefore As Integer = EstStockRemain
                                    EstStockRemain -= TotAllocForDealer
                                    For RDIdx = 0 To CType(oVRSameVC.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVRSameVC.DealerID).Count - 1
                                        'Distribute to each RD
                                        oRD = CType(CType(oVRSameVC.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVRSameVC.DealerID)(RDIdx), RedemptionDetail)
                                        If oRD.IsManualAlloc <> 1 Then
                                            Sequence += 1
                                            If oRD.RequestQty <= TotAllocForDealerRemain Then
                                                'oRD.IsInProcess = 1 : oRD.OriRequestQty = oRD.RequestQty : oRD.OriRespondQty = oRD.RespondQty 'initiate->for ceiling calculation purpose
                                                nDiff = oRD.RequestQty - oRD.RespondQty
                                                oRD.RespondQty = oRD.RequestQty
                                                Me.IsEnoughBasedCeiling(oRD, nDiff, Ceiling)

                                                oRD.Sequence = Sequence
                                                oRD.Ceiling = Ceiling
                                                oRD.Stock = StockBefore
                                                StockBefore -= oRD.RespondQty

                                                'oRDFac.Update(oRD) 'for ceiling calculation purpose
                                                'AutoUpdate1
                                                Me.UpdateTotalQty(aVRs(RowIdxSameVC), DayIdx, 0, 0, nDiff)
                                                CType(CType(aVRs(RowIdxSameVC), v_Redemption).RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVRSameVC.DealerID)(RDIdx) = oRD
                                                TotAllocForDealerRemain -= oRD.RespondQty
                                                'EstStockRemain -= oRD.RespondQty
                                            Else
                                                'oRD.IsInProcess = 1 : oRD.OriRequestQty = oRD.RequestQty : oRD.OriRespondQty = oRD.RespondQty 'initiate->for ceiling calculation purpose
                                                nDiff = TotAllocForDealerRemain - oRD.RespondQty
                                                oRD.RespondQty = TotAllocForDealerRemain

                                                Me.IsEnoughBasedCeiling(oRD, nDiff, Ceiling)
                                                oRD.Sequence = Sequence
                                                oRD.Ceiling = Ceiling
                                                oRD.Stock = StockBefore
                                                StockBefore -= oRD.RespondQty
                                                'oRDFac.Update(oRD) 'for ceiling calculation purpose
                                                'AutoUpdate2
                                                Me.UpdateTotalQty(aVRs(RowIdxSameVC), DayIdx, 0, 0, nDiff)
                                                CType(CType(aVRs(RowIdxSameVC), v_Redemption).RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVRSameVC.DealerID)(RDIdx) = oRD
                                                TotAllocForDealerRemain -= oRD.RespondQty
                                                'EstStockRemain -= oRD.RespondQty
                                                TotUnAllocatedForDealer = oRD.RequestQty - oRD.RespondQty
                                            End If
                                        End If
                                    Next 'For RDIdx = 0 To CType(oVRSameVC.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVRSameVC.DealerID).Count - 1
                                End If
                            Else
                                'RowIdx = OriRowIdx + 1000 + nSame ' RowIdxSameVC ' - 1
                                Exit For
                            End If
                        Next 'For RowIdxSameVC = RowIdx To aVRs.Count - 1
                        If EstStockRemain > 0 Then
                            'Cek, jika emang sisa, apakah semuanya bener2 udah dapet
                            EstStockRemain = AllocByOCQty(aVRs, RowIdx - (nSame - 1), RowIdx, DayIdx, EstStockRemain)
                            If EstStockRemain > 0 Then
                                EstStockRemain = AllocByCreatedTime(aVRs, RowIdx - (nSame - 1), RowIdx, DayIdx, EstStockRemain)
                            End If
                        End If
                        If 1 = 1 OrElse EstStockRemain > 0 Then
                            'Move this KTB Stock to followind day which's having EstimationStock>0
                            Dim IsAlreadyDecreased As Boolean = False
                            For NextDayIdx = DayIdx + 1 To nDays - 1
                                oRHNextDay = CType(oVR.RedemptionHeaders(NextDayIdx), RedemptionHeader)
                                If oRHNextDay.EstimationStock > 0 Then
                                    If Me.dtgMain.Columns(NextDayIdx + 5).Visible = False Then
                                        If Not IsAlreadyDecreased Then
                                            Dim oRH As RedemptionHeader = CType(CType(aVRs(OriRowIdx), v_Redemption).RedemptionHeaders(DayIdx), RedemptionHeader)
                                            oRH.EstimationStock -= EstStockRemain
                                            Me.UpdateTotalQty(aVRs(OriRowIdx), DayIdx, -EstStockRemain, 0, 0)
                                            CType(aVRs(OriRowIdx), v_Redemption).RedemptionHeaders(DayIdx) = oRH
                                            IsAlreadyDecreased = True
                                        End If
                                        EstStockRemain += oRHNextDay.EstimationStock
                                        nDiff = -oRHNextDay.EstimationStock
                                        oRHNextDay.EstimationStock = 0
                                        'AutoUpdate5
                                        Me.UpdateTotalQty(aVRs(OriRowIdx), NextDayIdx, nDiff, 0, 0)
                                        CType(aVRs(OriRowIdx), v_Redemption).RedemptionHeaders(NextDayIdx) = oRHNextDay

                                    Else
                                        If Not IsAlreadyDecreased Then
                                            Dim oRH As RedemptionHeader = CType(CType(aVRs(OriRowIdx), v_Redemption).RedemptionHeaders(DayIdx), RedemptionHeader)
                                            oRH.EstimationStock -= EstStockRemain
                                            Me.UpdateTotalQty(aVRs(OriRowIdx), DayIdx, -EstStockRemain, 0, 0)
                                            CType(aVRs(OriRowIdx), v_Redemption).RedemptionHeaders(DayIdx) = oRH
                                            IsAlreadyDecreased = True
                                        End If
                                        nDiff = EstStockRemain
                                        oRHNextDay.EstimationStock += EstStockRemain
                                        'AutoUpdate5
                                        Me.UpdateTotalQty(aVRs(OriRowIdx), NextDayIdx, nDiff, 0, 0)
                                        CType(aVRs(OriRowIdx), v_Redemption).RedemptionHeaders(NextDayIdx) = oRHNextDay


                                        'Dim oRH As RedemptionHeader = CType(CType(aVRs(OriRowIdx), v_Redemption).RedemptionHeaders(DayIdx), RedemptionHeader)
                                        'oRH.EstimationStock -= EstStockRemain
                                        'Me.UpdateTotalQty(aVRs(OriRowIdx), DayIdx, -EstStockRemain, 0, 0)
                                        'CType(aVRs(OriRowIdx), v_Redemption).RedemptionHeaders(DayIdx) = oRH
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                        'move unallocated request to the following day
                        Me.MoveUnAllocatedRequest(aVRs, RowIdx - (nSame - 1), RowIdx, DayIdx)
                    End If
                Next 'For RowIdx = 0 To aVRs.Count - 1
            End If
        Next 'For DayIdx = 0 To nDays - 1

        oRSFac.SetInProcessStatus(Me.GetSelectedDate(), Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text, 2)
        Me.sessHelper.SetSession(Me._sessData, aVRs)
        Me.ViewState.Item("AutoRefresh") = "1"
        Me.BindDTG()
        Me.btnDistribute.Enabled = False
    End Sub

    Private Sub MoveUnAllocatedRequest(ByRef aVRs As ArrayList, ByVal IdxStart As Integer, ByVal IdxEnd As Integer, ByVal DayIdx As Integer)
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)
        Dim RowIdx As Integer = 0, RowIdxSameVC As Integer = 0, RDIdx As Integer = 0, NextDayIdx As Integer = 0, NextRDIdx As Integer = 0, nSame As Integer = 0, OriRowIdx As Integer = 0
        Dim CurVCID As Integer = 0, EstStock As Integer = 0, EstStockRemain As Integer = 0, TotAllocForDealerInDec As Decimal = 0, TotAllocForDealer As Integer = 0, TotAllocForDealerRemain As Integer = 0, nDiff As Integer = 0
        Dim TotUnAllocatedForDealer As Integer = 0, IsAlreadyMoved As Boolean = False
        Dim TotReqVCPerDealer As Integer = 0, TotReqVCAllDealer As Integer = 0
        Dim oVR As v_Redemption, oVRSameVC As v_Redemption, oRD As RedemptionDetail, oRHNextDay As RedemptionHeader, oRDNextDay As RedemptionDetail
        Dim oRDFac As New RedemptionDetailFacade(User)

        For RowIdxSameVC = IdxStart To IdxEnd
            oVRSameVC = CType(aVRs(RowIdxSameVC), v_Redemption)
            For RDIdx = 0 To CType(oVRSameVC.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVRSameVC.DealerID).Count - 1
                oRD = CType(CType(oVRSameVC.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVRSameVC.DealerID)(RDIdx), RedemptionDetail)
                TotUnAllocatedForDealer = oRD.RequestQty - oRD.RespondQty
                If TotUnAllocatedForDealer > 0 Then ' AndAlso oRD.IsManualAlloc <> 1 Then
                    'Move This Dealer Request to following day which is having EstimationStock>0
                    IsAlreadyMoved = False
                    For NextDayIdx = DayIdx + 1 To nDays - 1
                        oRHNextDay = CType(oVRSameVC.RedemptionHeaders(NextDayIdx), RedemptionHeader)
                        If oRHNextDay.EstimationStock > 0 AndAlso Me.dtgMain.Columns(NextDayIdx + 5).Visible Then
                            'IsAlreadyMoved = True
                            If oRHNextDay.RedemptionDetails(oVRSameVC.DealerID).Count > 0 Then
                                For NextRDIdx = 0 To oRHNextDay.RedemptionDetails(oVRSameVC.DealerID).Count - 1
                                    oRDNextDay = CType(oRHNextDay.RedemptionDetails(oVRSameVC.DealerID)(NextRDIdx), RedemptionDetail)
                                    If oRDNextDay.IsManualAlloc <> 1 Then
                                        IsAlreadyMoved = True
                                        nDiff = TotUnAllocatedForDealer
                                        'oRDNextDay.IsInProcess = 1 : oRDNextDay.OriRequestQty = oRDNextDay.RequestQty : oRDNextDay.OriRespondQty = oRDNextDay.RespondQty 'initiate->for ceiling calculation purpose
                                        oRDNextDay.RequestQty += TotUnAllocatedForDealer
                                        'oRDFac.Update(oRDNextDay) 'for ceiling calculation purpose
                                        'AutoUpdate3
                                        Me.UpdateTotalQty(aVRs(RowIdxSameVC), NextDayIdx, 0, nDiff, 0)
                                        CType(CType(aVRs(RowIdxSameVC), v_Redemption).RedemptionHeaders(NextDayIdx), RedemptionHeader).RedemptionDetails(oVRSameVC.DealerID)(NextRDIdx) = oRDNextDay

                                        'oRD.IsInProcess = 1 : oRD.OriRequestQty = oRD.RequestQty : oRD.OriRespondQty = oRD.RespondQty 'initiate->for ceiling calculation purpose
                                        oRD.RequestQty -= TotUnAllocatedForDealer
                                        'oRDFac.Update(oRD) 'for ceiling calculation purpose
                                        Me.UpdateTotalQty(aVRs(RowIdxSameVC), DayIdx, 0, -1 * TotUnAllocatedForDealer, 0)
                                        CType(CType(aVRs(RowIdxSameVC), v_Redemption).RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVRSameVC.DealerID)(RDIdx) = oRD

                                        TotUnAllocatedForDealer -= TotUnAllocatedForDealer
                                        Exit For
                                    End If
                                Next
                            Else
                                IsAlreadyMoved = True
                                oRDNextDay = New RedemptionDetail
                                oRDNextDay.RedemptionHeader = oRHNextDay
                                oRDNextDay.Dealer = New DealerFacade(User).Retrieve(oVRSameVC.DealerID)
                                oRDNextDay.TermOfPayment = oRD.TermOfPayment
                                oRDNextDay.RequestQty = TotUnAllocatedForDealer
                                oRDNextDay.RespondQty = 0
                                oRHNextDay.SetRedemptionDetail(oRDNextDay, -1)
                                nDiff = TotUnAllocatedForDealer

                                'oRDFac.Update(oRDNextDay) 'for ceiling calculation purpose

                                'AutoUpdate4
                                Me.UpdateTotalQty(aVRs(RowIdxSameVC), NextDayIdx, 0, nDiff, 0)
                                CType(aVRs(RowIdxSameVC), v_Redemption).RedemptionHeaders(NextDayIdx) = oRHNextDay

                                'oRD.IsInProcess = 1 : oRD.OriRequestQty = oRD.RequestQty : oRD.OriRespondQty = oRD.RespondQty 'initiate->for ceiling calculation purpose
                                oRD.RequestQty -= TotUnAllocatedForDealer
                                'oRDFac.Update(oRD) 'for ceiling calculation purpose
                                Me.UpdateTotalQty(aVRs(RowIdxSameVC), DayIdx, 0, -1 * TotUnAllocatedForDealer, 0)
                                CType(CType(aVRs(RowIdxSameVC), v_Redemption).RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVRSameVC.DealerID)(RDIdx) = oRD

                                TotUnAllocatedForDealer -= TotUnAllocatedForDealer
                            End If
                        End If
                        If IsAlreadyMoved Then Exit For
                    Next
                End If
            Next
        Next
    End Sub

    Private Function AllocByOCQty(ByRef aVRs As ArrayList, ByVal IdxStart As Integer, ByVal IdxEnd As Integer, ByVal DayIdx As Integer, ByVal EstStockRemain As Integer) As Integer
        Dim oVR As v_Redemption, oVR2 As v_Redemption, oRD As RedemptionDetail
        Dim i As Integer, j As Integer, RHID As Integer, Plan As Integer, Req As Integer, Res As Integer, RowIdx As Integer, nDiff As Integer, RowIdxRD As Integer
        Dim aTemps As New ArrayList, aTempsOK As New ArrayList
        Dim IsInserted As Boolean = False

        For i = IdxStart To IdxEnd
            oVR = CType(aVRs(i), v_Redemption)
            Me.GetRedComponent(oVR, DayIdx + 1, RHID, Plan, Req, Res)
            If Req > Res Then
                IsInserted = False
                'aTemps = aTempsOK
                aTemps = New ArrayList
                For j = 0 To aTempsOK.Count - 1
                    aTemps.Add(aTempsOK(j))
                Next
                For j = 0 To aTemps.Count - 1
                    oVR2 = CType(aVRs(CType(aTemps(j), Integer)), v_Redemption)
                    If oVR2.TotalOC >= oVR.TotalOC Then
                        aTempsOK.Insert(j, i)
                        IsInserted = True
                        Exit For
                    End If
                Next
                If Not IsInserted Then
                    aTempsOK.Add(i)
                End If
            End If
        Next
        For i = 0 To aTemps.Count - 1
            If EstStockRemain > 0 Then
                RowIdx = CType(aTemps(i), Integer)
                oVR = CType(aVRs(RowIdx), v_Redemption)
                For RowIdxRD = 0 To CType(oVR.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVR.DealerID).Count - 1
                    oRD = CType(CType(oVR.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVR.DealerID)(RowIdxRD), RedemptionDetail)
                    If oRD.RespondQty < oRD.RequestQty Then
                        If oRD.RequestQty - oRD.RespondQty <= EstStockRemain Then
                            nDiff = (oRD.RequestQty - oRD.RespondQty)
                        Else
                            nDiff = EstStockRemain
                        End If
                        oRD.RespondQty += nDiff
                        EstStockRemain -= nDiff

                        Me.UpdateTotalQty(oVR, DayIdx, 0, 0, nDiff)
                        CType(CType(aVRs(RowIdx), v_Redemption).RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVR.DealerID)(RowIdxRD) = oRD
                    End If
                Next
            Else
                Exit For
            End If
        Next

        Return EstStockRemain
    End Function

    Private Function AllocByCreatedTime(ByRef aVRs As ArrayList, ByVal IdxStart As Integer, ByVal IdxEnd As Integer, ByVal DayIdx As Integer, ByVal EstStockRemain As Integer) As Integer
        Dim oVR As v_Redemption, oVR2 As v_Redemption, oRD As RedemptionDetail
        Dim i As Integer, j As Integer, k As Integer, RHID As Integer, Plan As Integer, Req As Integer, Res As Integer, RowIdx As Integer, nDiff As Integer, RowIdxRD As Integer
        Dim aTemps As New ArrayList, aTempsOK As New ArrayList
        Dim IsInserted As Boolean = False
        Dim dtCreated1 As DateTime, dtCreated2 As DateTime
        Dim nTemp As Integer

        For i = IdxStart To IdxEnd
            oVR = CType(aVRs(i), v_Redemption)
            Me.GetRedComponent(oVR, DayIdx + 1, RHID, Plan, Req, Res)
            If Req > Res Then
                IsInserted = False
                'aTemps = aTempsOK
                aTemps = New ArrayList
                For j = 0 To aTempsOK.Count - 1
                    aTemps.Add(aTempsOK(j))
                Next
                For j = 0 To aTemps.Count - 1
                    oVR2 = CType(aVRs(CType(aTemps(j), Integer)), v_Redemption)
                    dtCreated1 = DateSerial(1900, 1, 1)
                    For k = 0 To CType(oVR.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails.Count - 1
                        oRD = CType(CType(oVR.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVR.DealerID)(k), RedemptionDetail)
                        If oRD.CreatedTime > dtCreated1 Then dtCreated1 = oRD.CreatedTime
                    Next
                    For k = 0 To CType(oVR2.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails.Count - 1
                        oRD = CType(CType(oVR2.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVR.DealerID)(k), RedemptionDetail)
                        If oRD.CreatedTime > dtCreated2 Then dtCreated2 = oRD.CreatedTime
                    Next
                    If dtCreated2 >= dtCreated1 Then
                        aTemps.Insert(j, i)
                        IsInserted = True
                        Exit For
                    End If
                Next
                If Not IsInserted Then
                    aTemps.Add(i)
                End If
            End If
        Next
        For i = 0 To aTemps.Count - 1
            If EstStockRemain > 0 Then
                RowIdx = CType(aTemps(i), Integer)
                oVR = CType(aVRs(RowIdx), v_Redemption)
                For RowIdxRD = 0 To CType(oVR.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVR.DealerID).Count - 1
                    oRD = CType(CType(oVR.RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVR.DealerID)(RowIdxRD), RedemptionDetail)
                    If oRD.RespondQty < oRD.RequestQty Then
                        If oRD.RequestQty - oRD.RespondQty <= EstStockRemain Then
                            nDiff = (oRD.RequestQty - oRD.RespondQty)
                        Else
                            nDiff = EstStockRemain
                        End If
                        oRD.RespondQty += nDiff
                        EstStockRemain -= nDiff

                        Me.UpdateTotalQty(oVR, DayIdx, 0, 0, nDiff)
                        CType(CType(aVRs(RowIdx), v_Redemption).RedemptionHeaders(DayIdx), RedemptionHeader).RedemptionDetails(oVR.DealerID)(RowIdxRD) = oRD
                    End If
                Next
            Else
                Exit For
            End If
        Next

        Return EstStockRemain
    End Function


    Private Sub SaveData()
        Dim VRIdx As Integer = 0, RHIdx As Integer = 0, RDIdx As Integer = 0
        Dim aVRs As ArrayList = CType(Me.sessHelper.GetSession(Me._sessData), ArrayList)
        Dim oVR As v_Redemption, oRH As RedemptionHeader, oRD As RedemptionDetail
        Dim oVRFac As New v_RedemptionFacade(User), oRHFac As New RedemptionHeaderFacade(User), oRDFac As New RedemptionDetailFacade(User)
        Dim i As Integer
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)

        'If dtgMain.Columns(i + 4).Visible Then
        For VRIdx = 0 To aVRs.Count - 1
            oVR = CType(aVRs(VRIdx), v_Redemption)
            For i = 1 To nDays
                If dtgMain.Columns(i + 4).Visible Then
                    oRH = CType(oVR.RedemptionHeaders(i - 1), RedemptionHeader)
                    For RDIdx = 0 To oRH.RedemptionDetails(oVR.DealerID).Count - 1
                        oRD = CType(oRH.RedemptionDetails(oVR.DealerID)(RDIdx), RedemptionDetail)
                        If oRD.ID > 0 Then
                            oRDFac.Update(oRD)
                        Else
                            oRDFac.Insert(oRD)
                        End If
                    Next
                    oRH = oRHFac.Retrieve(oRH.ID)
                    If Not IsNothing(oRH) AndAlso oRH.ID > 0 Then
                        oRHFac.Update(oRH)
                    End If
                End If
            Next
        Next
        Me.CommitTempRedDetail()

        'Update Redemption Status
        Dim oRSFac As New RedemptionStatusFacade(User)
        oRSFac.SetInProcessStatus(Me.GetSelectedDate(), Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text, 3)
        Me.btnDistribute.Enabled = False
        Me.btnSave.Enabled = False
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

    Private Sub WriteDataOld(ByVal sw As StreamWriter, ByVal data As ArrayList)
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
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Tipe/Warna" & tab)
            itemLine.Append("Deskripsi" & tab)
            itemLine.Append("OC/Unit" & tab)
            For i = 1 To nDays
                If dtgMain.Columns(i + 4).Visible Then
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
                itemLine.Append(di.Cells(1).ToolTip & tab) 'DealerName
                itemLine.Append(CType(di.FindControl("lblCode"), Label).Text & tab)
                itemLine.Append(CType(di.FindControl("lblDescription"), Label).Text & tab)
                itemLine.Append(CType(di.FindControl("lblOC"), Label).Text & tab)
                For i = 1 To nDays
                    If Me.dtgMain.Columns(i + 4).Visible Then
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
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Tipe/Warna" & tab)
            itemLine.Append("Deskripsi" & tab)
            itemLine.Append("OC/Unit" & tab)
            For i = 1 To nDays
                If dtgMain.Columns(i + 4).Visible Then
                    'itemLine.Append(dtgMain.Columns(i + 3).HeaderText & tab)
                    itemLine.Append(i.ToString & tab)
                End If
            Next
            itemLine.Append("Total" & tab)
            sw.WriteLine(itemLine.ToString())

            For Each di As DataGridItem In dtgMain.Items
                itemLine.Remove(0, itemLine.Length)

                itemLine.Append((di.ItemIndex + 1).ToString & tab)
                itemLine.Append(di.Cells(1).Text & tab)
                itemLine.Append(di.Cells(1).ToolTip & tab) 'DealerName
                itemLine.Append(di.Cells(2).Text & tab)
                itemLine.Append(di.Cells(3).Text & tab)
                itemLine.Append(di.Cells(4).Text & tab)
                For i = 1 To nDays
                    If Me.dtgMain.Columns(i + 4).Visible Then
                        itemLine.Append(CType(di.FindControl("lblS" & i.ToString), Label).Text & tab)
                    End If
                Next
                itemLine.Append(di.Cells(dtgMain.Columns.Count - 1).Text & tab)
                sw.WriteLine(itemLine.ToString())
            Next '
            If Not IsNothing(sessHelper.GetSession("FrmEntryRedemptionPlan.Footer")) Then
                Dim e As WebControls.DataGridItemEventArgs = CType(sessHelper.GetSession("FrmEntryRedemptionPlan.Footer"), WebControls.DataGridItemEventArgs)
                Dim sTemp As String = ""

                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                For i = 1 To nDays
                    If Me.dtgMain.Columns(i + 4).Visible Then
                        sTemp = e.Item.Cells(i + 4).Text
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

    Private Function IsStillOpen() As Boolean
        Dim oRSFac As New RedemptionStatusFacade(User)
        Dim IsOpen As Boolean
        Dim dtPeriod As Date

        Try
            dtPeriod = DateSerial(CType(Me.ddlYear.SelectedValue, Integer), CType(Me.ddlMonth.SelectedValue, Integer), 1)
        Catch ex As Exception
            dtPeriod = DateSerial(3000, 1, 1)
        End Try

        If Me.ddlCategory.Items.Count > 0 And Me.ddlSubCategory.Items.Count > 0 Then 'Handle PageLoad
            IsOpen = oRSFac.IsStatusOpen(dtPeriod, Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text)
        Else
            IsOpen = oRSFac.IsStatusOpen(dtPeriod, "", "")
        End If

        Return IsOpen
    End Function

    Private Sub SetInProcess(ByVal ProcessStatus As Integer)
        Dim oRSFac As New RedemptionStatusFacade(User)
        Dim IsOpen As Boolean
        Dim dtPeriod As Date

        Try
            dtPeriod = DateSerial(CType(Me.ddlYear.SelectedValue, Integer), CType(Me.ddlMonth.SelectedValue, Integer), 1)
        Catch ex As Exception
            dtPeriod = DateSerial(3000, 1, 1)
        End Try

        oRSFac.SetInProcessStatus(dtPeriod, Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text, ProcessStatus)

    End Sub

#End Region

#Region "Classes"
    Private _aCDs As New ArrayList

    Private Function GetUsedCeilingName(ByRef oRD As RedemptionDetail) As String
        Return "FrmEntryRedemptionPlan2._vstUsedCeiling_" & oRD.Dealer.ID & "_" & oRD.TermOfPayment.ID & "_" & oRD.RedemptionHeader.PeriodDate.Day
    End Function

    Private Sub ClearUsedCeiling()
        'Me.ViewState.Clear()
    End Sub

    Private Sub AddUsedCeiling(ByRef oRD As RedemptionDetail)
        If IsNothing(Me.ViewState.Item(Me.GetUsedCeilingName(oRD))) Then
            Me.ViewState.Add(Me.GetUsedCeilingName(oRD), 0)
        End If

        Dim Price As Decimal = Me.GetItemPrice(oRD)
        Me.ViewState.Item(Me.GetUsedCeilingName(oRD)) = CType(Me.ViewState.Item(Me.GetUsedCeilingName(oRD)), Decimal) + (oRD.RespondQty * Price)
    End Sub

    Private Function GetUsedCeiling(ByRef oRD As RedemptionDetail) As Decimal
        Dim Ceiling As Decimal
        If IsNothing(Me.ViewState.Item(Me.GetUsedCeilingName(oRD))) Then
            Ceiling = 0
        Else
            Ceiling = CType(Me.ViewState.Item(Me.GetUsedCeilingName(oRD)), Decimal)
        End If

        Return Ceiling
    End Function


    Private Function GetPriceName(ByRef oRD As RedemptionDetail) As String
        Return "P_" & oRD.Dealer.ID.ToString & "_" & oRD.RedemptionHeader.VechileColor.ID.ToString
    End Function
    Private Function GetItemPrice(ByRef oRD As RedemptionDetail) As Decimal
        Dim Price As Decimal = 0

        If IsNothing(Me.ViewState.Item(GetPriceName(oRD))) Then
            Dim cCD As New CriteriaComposite(New Criteria(GetType(ContractDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim aCDs As ArrayList

            cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.Dealer.ID", oRD.Dealer.ID))
            cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.ContractPeriodYear", oRD.RedemptionHeader.PeriodDate.Year))
            cCD.opAnd(New Criteria(GetType(ContractDetail), "ContractHeader.ContractPeriodMonth", oRD.RedemptionHeader.PeriodDate.Month))
            cCD.opAnd(New Criteria(GetType(ContractDetail), "VechileColor.ID", oRD.RedemptionHeader.VechileColor.ID))
            aCDs = New ContractDetailFacade(User).Retrieve(cCD)
            If aCDs.Count > 0 Then
                Price = CType(aCDs(0), ContractDetail).Amount
            End If
            Me.ViewState.Add(Me.GetPriceName(oRD), Price)
        Else
            Try
                Price = CType(Me.ViewState.Item(Me.GetPriceName(oRD)), Decimal)
            Catch ex As Exception
                Price = 0
            End Try
        End If

        Return Price
    End Function
    Private Function GetCeilingName(ByRef oRD As RedemptionDetail) As String
        Return "C_" & oRD.Dealer.ID.ToString & "_" & oRD.TermOfPayment.PaymentType.ToString
    End Function

    Private Function CeilingPosition(ByRef oRD As RedemptionDetail) As Decimal
        Dim Ceiling As Decimal = 0

        If IsNothing(Me.ViewState.Item(Me.GetCeilingName(oRD))) Then
            Dim oSRCFac As sp_RedemptionCeilingFacade = New sp_RedemptionCeilingFacade(User)
            Dim oSRC As sp_RedemptionCeiling
            Dim arlSRC As New ArrayList

            If oRD.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS Then
                Ceiling = 0
            Else
                arlSRC = oSRCFac.RetrieveFromSP(oRD.Dealer.CreditAccount, oRD.TermOfPayment.PaymentType, oRD.RedemptionHeader.PeriodDate)

                If arlSRC.Count <= 0 Then
                    Ceiling = 0
                Else
                    oSRC = CType(arlSRC(0), sp_RedemptionCeiling)
                    'Ceiling = oSRC.InitialCeiling + oSRC.TotalLiquified '- oSRC.TotalProposed
                    Ceiling = oSRC.InitialCeiling + oSRC.TotalLiquified - oSRC.TotalProposed
                End If
            End If

            Me.ViewState.Add(Me.GetCeilingName(oRD), Ceiling) 'Initiate Ceiling

        Else
            Try
                Ceiling = CType(Me.ViewState.Item(Me.GetCeilingName(oRD)), Decimal)
            Catch ex As Exception
                Ceiling = 0
            End Try
        End If
        Ceiling = Ceiling - Me.GetUsedCeiling(oRD)
        Return Ceiling
    End Function

    Private Sub UpdateCeilingPosition(ByRef oRD As RedemptionDetail, ByRef ItemPrice As Decimal)
        Dim Ceiling As Decimal
        Dim TotalRequest As Decimal = 0

        Try
            Ceiling = CType(Me.ViewState.Item(Me.GetCeilingName(oRD)), Decimal)
        Catch ex As Exception
            Ceiling = 0
        End Try
        TotalRequest = oRD.RespondQty * ItemPrice
        Ceiling -= TotalRequest

        Me.ViewState.Item(Me.GetCeilingName(oRD)) = Ceiling
    End Sub

    Private Function IsEnoughBasedCeiling(ByRef oRD As RedemptionDetail, ByRef DiffQty As Integer, ByRef Ceiling As Decimal) As Boolean
        Dim nEnoughQty As Integer
        Dim IsEnough As Boolean = False
        Dim Price As Decimal = Me.GetItemPrice(oRD)

        If oRD.RespondQty > 0 Then
            Ceiling = Me.CeilingPosition(oRD)
            nEnoughQty = Math.Floor(Ceiling / Price)
            If nEnoughQty < 1 Then
                DiffQty += -oRD.RespondQty
                oRD.RespondQty = 0
                IsEnough = False
            Else
                IsEnough = True
                If nEnoughQty < oRD.RespondQty Then
                    DiffQty += (nEnoughQty - oRD.RespondQty)
                    oRD.RespondQty = nEnoughQty
                    'oRD.RequestQty = nEnoughQty
                End If
                Me.AddUsedCeiling(oRD)
                Me.UpdateCeilingPosition(oRD, Price)
            End If
        End If

        Return IsEnough
    End Function



#End Region

#Region "Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 7000
        If Not IsPostBack Then
            Initialization()
        End If
        CheckPrivelege()
    End Sub
    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
        BindVehicleType(True)
    End Sub

    Private Sub ddlSubCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        BindVehicleType(False)
    End Sub

    Private Sub ddlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlType.SelectedIndexChanged
        BindddlTipeWarna(ddlType.SelectedItem.Text)
    End Sub

    Private Function IsValidCriteria() As Boolean
        If Me.ddlCategory.SelectedIndex = 0 Then
            MessageBox.Show("Kategori Harus dipilih")
            Return False
        End If
        If Me.ddlSubCategory.SelectedIndex = 0 Then
            MessageBox.Show("Sub Kategori Harus dipilih")
            Return False
        End If
        'If Me.ddlType.SelectedIndex > 0 Then
        '    MessageBox.Show("Untuk Distribusi Harus Kategori & Sub Kategori Saja Yang Dipilih")
        '    Return False
        'End If
        'If Me.ddlTipeWarna.SelectedIndex > 0 Then
        '    MessageBox.Show("Untuk Distribusi Harus Kategori & Sub Kategori Saja Yang Dipilih")
        '    Return False
        'End If
        Return True
    End Function


    Private Function GetSelectedDate() As Date
        Dim dt As Date
        Try
            dt = DateSerial(Me.ddlYear.SElectedValue, Me.DdlMonth.SelectedValue, 1)
        Catch ex As Exception
            dt = DateSerial(1900, 1, 1)
        End Try
        Return dt
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If IsValidCriteria() = False Then Exit Sub

        Dim oRSFac As New RedemptionStatusFacade(User)
        If oRSFac.IsAllowToUpdateStatus(Me.GetSelectedDate(), Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text, 0, New RedemptionStatus) = False Then
            Me.btnDistribute.Enabled = False
            Me.btnSave.Enabled = False
            Me.ViewState.Item(Me._vstEnableDblClickByPrivilege) = 0
        Else
            oRSFac.SetInProcessStatus(Me.GetSelectedDate(), Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text, 0)
        End If
        Me.sessHelper.SetSession(Me._sessCategoryCode, Me.ddlCategory.SelectedItem.Text)
        Me.sessHelper.SetSession(Me._sessSubCategoryCode, Me.ddlSubCategory.SelectedItem.Text)

        BindDTG()
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim i As Integer
            Dim oD As Dealer = Session.Item("DEALER")

            For i = 0 To 31
                Me._TotReqPerCols(i) = 0
                Me._TotResPerCols(i) = 0
            Next
            Me.sessHelper.SetSession(Me._sessTotReqPerCols, Me._TotReqPerCols)
            Me.sessHelper.SetSession(Me._sessTotResPerCols, Me._TotResPerCols)

            Dim nDays As Integer = viewstate.Item("nDays")
            For i = 1 To 31
                dtgMain.Columns(4 + i).Visible = False
                If i <= nDays Then
                    If IsHavingStock(i) Then
                        If oD.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                            If IsHavingRequest(i) Then
                                dtgMain.Columns(4 + i).Visible = True
                            End If
                        ElseIf oD.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                            dtgMain.Columns(4 + i).Visible = True
                        End If
                        Dim lblH As Label = e.Item.FindControl("lblH" & i.ToString)
                        lblH.Text = i.ToString
                    End If
                End If
            Next
        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim aVRs As ArrayList = CType(Me.sessHelper.GetSession(Me._sessData), ArrayList)
            Dim oVR As v_Redemption = CType(aVRs(e.Item.ItemIndex), v_Redemption)
            Dim oD As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            Dim RHID As Integer, Plan As Integer, Req As Integer, Res As Integer
            Dim i As Integer, TotReqPerRow As Integer = 0, TotResPerRow As Integer = 0
            Dim sUrl As String
            Dim lblDay As Label
            Dim IsShowLink As Boolean


            e.Item.Cells(0).Text = (e.Item.ItemIndex + 1).ToString
            Try
                e.Item.Cells(1).ToolTip = New DealerFacade(User).Retrieve(oVR.DealerID).DealerName
            Catch ex As Exception
                e.Item.Cells(1).ToolTip = ""
            End Try

            For i = 1 To 31
                If Me.dtgMain.Columns(4 + i).Visible Then
                    Me.GetRedComponent(oVR, i, RHID, Plan, Req, Res)
                    lblDay = e.Item.FindControl("lblS" & i.ToString)

                    Me._TotReqPerCols = Me.sessHelper.GetSession(Me._sessTotReqPerCols)
                    Me._TotResPerCols = Me.sessHelper.GetSession(Me._sessTotResPerCols)
                    Me._TotReqPerCols(i - 1) += Req
                    Me._TotResPerCols(i - 1) += Res
                    Me.sessHelper.SetSession(Me._sessTotReqPerCols, Me._TotReqPerCols)
                    Me.sessHelper.SetSession(Me._sessTotResPerCols, Me._TotResPerCols)
                    TotReqPerRow += Req
                    TotResPerRow += Res

                    If Plan > 0 Then
                        IsShowLink = False
                        'sUrl = "ShowDetail(" & e.Item.ItemIndex & "," & (i - 1) & ",'" & oVR.DealerCode & "')"
                        sUrl = "ShowDetail(" & RHID.ToString & "," & e.Item.ItemIndex & "," & (i - 1) & ",'" & oVR.DealerCode & "')"
                        If oD.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                            IsShowLink = (Req > 0)
                        ElseIf oD.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                            If 1 = 1 Then 'CType(viewstate.Item("TransactionControl.Status"), Short) = EnumDealerStatus.DealerStatus.Aktive Then
                                IsShowLink = (Plan > 0)
                            End If
                        End If
                        If IsShowLink Then
                            If Me.ViewState.Item(Me._vstEnableDblClickByPrivilege) = "1" Then
                                e.Item.Cells(4 + i).Attributes.Add("OnDblClick", sUrl)
                                e.Item.Cells(4 + i).Style.Add("cursor", "hand")
                            Else
                                e.Item.Cells(4 + i).Attributes.Remove("OnDblClick")
                                e.Item.Cells(4 + i).Style.remove("cursor")
                            End If
                            If Req > 0 Then
                                lblDay.Text = Req.ToString & "|" & Res.ToString
                                If oD.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                                    'if first row of vehicle
                                    'If e.Item.ItemIndex = 0 OrElse oVR.VechileColorID <> CType(aVRs(e.Item.ItemIndex - 1), v_Redemption).VechileColorID Then
                                    '    lblDay.Text &= "(" & Plan.ToString & ")"
                                    'End If
                                End If
                            End If
                        Else
                            e.Item.Cells(4 + i).Attributes.Remove("OnDblClick")
                            e.Item.Cells(4 + i).Style.remove("cursor")
                            lblDay.Style.Add("display", "none")
                        End If
                    Else
                        e.Item.Cells(4 + i).Attributes.Remove("OnDblClick")
                        e.Item.Cells(4 + i).Style.remove("cursor")
                        lblDay.Style.Add("display", "none")
                    End If

                    If viewstate.Item("AutoRefresh") = "1" Then
                        lblDay.ToolTip = "Seq:"
                    End If
                End If
            Next

            e.Item.Cells(Me.dtgMain.Columns.Count - 1).Text = TotReqPerRow.ToString & "|" & TotResPerRow.ToString
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Dim i As Integer, TotReqPerRow As Integer = 0, TotResPerRow As Integer = 0

            Me._TotReqPerCols = Me.sessHelper.GetSession(Me._sessTotReqPerCols)
            Me._TotResPerCols = Me.sessHelper.GetSession(Me._sessTotResPerCols)

            For i = 1 To 31
                If Me.dtgMain.Columns(4 + i).Visible Then
                    TotReqPerRow += Me._TotReqPerCols(i - 1)
                    TotResPerRow += Me._TotResPerCols(i - 1)

                    e.Item.Cells(4 + i).Text = Me._TotReqPerCols(i - 1).ToString & "|" & Me._TotResPerCols(i - 1).ToString
                End If
            Next
            e.Item.Cells(Me.dtgMain.Columns.Count - 1).Text = TotReqPerRow.ToString & "|" & TotResPerRow.ToString

            Me.sessHelper.SetSession("FrmEntryRedemptionPlan.Footer", e)
        End If
    End Sub

    Private Sub btnRefreshGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshGrid.Click
        'Me.ViewState.Item("AutoRefresh") = "1" '"0"
        'Me.BindDTG()
        Dim RowIdx As Integer = Me.txtRowIndex.Text
        Dim DayIdx As Integer = Me.txtDayIndex.Text
        Dim sDay As String, sTotRow As String, sTotCol As String
        Dim i As Integer, j As Integer
        Dim aVRs As ArrayList = CType(Me.sessHelper.GetSession(Me._sessData), ArrayList)
        Dim Est As Integer, Req As Integer, Res As Integer
        Dim oVR As v_Redemption
        Dim TempReq As Integer, TempRes As Integer

        Me.GetRedComponent(CType(aVRs(RowIdx), v_Redemption), DayIdx + 1, CType(CType(aVRs(RowIdx), v_Redemption).RedemptionHeaders(DayIdx), RedemptionHeader).ID, Est, Req, Res)
        If Req > 0 OrElse Res > 0 Then
            sDay = Req.ToString & "|" & Res.ToString
        End If
        'Req = CType(aVRs(RowIdx), v_Redemption).TotalRequest()
        'Res = CType(aVRs(RowIdx), v_Redemption).TotalRespond()
        Req = 0
        Res = 0
        oVR = aVRs(RowIdx)
        For i = 5 To Me.dtgMain.Columns.Count - 2
            If Me.dtgMain.Columns(i).Visible Then
                Me.GetRedComponent(oVR, i - 4, 0, 0, TempReq, TempRes)
                Req += TempReq
                Res += TempRes
            End If
        Next
        sTotRow = Req.ToString() & "|" & Res.ToString

        Req = 0
        Res = 0
        For i = 0 To aVRs.Count - 1
            oVR = aVRs(i)

            Me.GetRedComponent(oVR, DayIdx + 1, CType(oVR.RedemptionHeaders(DayIdx), RedemptionHeader).ID, 0, TempReq, TempRes)
            Req += TempReq
            Res += TempRes
        Next

        sTotCol = Req.ToString & "|" & Res.ToString

        RegisterStartupScript("OpenWindow", "<script>UpdateDisplay('" & sDay & "','" & sTotRow & "','" & sTotCol & "')</script>")
    End Sub

    Private Sub btnDistribute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDistribute.Click
        If IsValidCriteria() = False Then Exit Sub
        Dim oRSFac As New RedemptionStatusFacade(User)

        If oRSFac.IsAllowToUpdateStatus(Me.GetSelectedDate(), Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text, 1, New RedemptionStatus) = False Then
            MessageBox.Show("Auto Distribute Gagal. Data Sudah Diproses")
            Me.btnSave.Enabled = False
            Me.btnDistribute.Enabled = False
            Exit Sub
        End If
        AutoDistribute()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim oRSFac As New RedemptionStatusFacade(User)

        If oRSFac.IsAllowToUpdateStatus(Me.GetSelectedDate(), Me.ddlCategory.SelectedItem.Text, Me.ddlSubCategory.SelectedItem.Text, 3, New RedemptionStatus) = False Then
            MessageBox.Show("Status Redemption Plan Sudah Closed")
            Me.btnSave.Enabled = False
            Me.btnDistribute.Enabled = False
            Exit Sub
        End If
        SaveData()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        'CommonFunction.DownloadDTGToExcel(Me.Page, Me.dtgMain, "Redemption Plan.xls")
        'Exit Sub
        'Dim TotData As Integer = 0
        viewstate.Item("AutoRefresh") = "0"
        'Dim aDatas As ArrayList = Me.GetData(TotData) ' Me.sessHelper.GetSession(Me._sessData)
        Me.btnSearch_Click(sender, e)
        Dim aDatas As ArrayList = Me.sessHelper.GetSession(Me._sessData)
        DoDownload(aDatas)
    End Sub

#End Region
End Class
