
#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
#End Region

#Region " .NET Base Class Namespace Imports "
Imports System.IO
Imports System.Text
#End Region

Public Class FrmEntryEstimationStock
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblType As System.Web.UI.WebControls.Label
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents dgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents icContractDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtContractHour As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtContractMinute As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtContractSecond As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSetContract As System.Web.UI.WebControls.Button
    Protected WithEvents ddlTipeWarna As System.Web.UI.WebControls.DropDownList

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
    Private _sessHelper As New SessionHelper
    Private _allRetrievedData As String = "FrmEntryEstimationStock.AllRetrievedData"
    Private _totalPerColumns As String = "FrmEntryEstimationStock.TotalPerColumns"
#End Region

#Region "Custom Methods"

    Private Sub CheckPrivelege()
        If Not SecurityProvider.Authorize(Context.User, SR.input_estimation_stok_redemption_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Input Estimation Stock")
        End If
        Me.btnSetContract.Enabled = SecurityProvider.Authorize(Context.User, SR.ubah_max_contract_date_redemption_privilege)
        Me.btnSave.Enabled = SecurityProvider.Authorize(Context.User, SR.simpan_estimation_stok_redemption_privilege)
        Me.btnDownload.Enabled = SecurityProvider.Authorize(Context.User, SR.download_estimation_stok_redemption_privilege)
    End Sub
    Private Sub Initialization()
        viewstate.Add("nDays", 0)
        BindPeriod()
        BindDdlCategory()
        InitHeader()
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
        Me.ddlTipeWarna.Items.Insert(0, New ListItem("Pilih", ""))
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

    Private Sub BindDTG(ByVal PageIndex As Integer)
        Dim arlData As New ArrayList

        InitHeader()
        arlData = GetData()
        _sessHelper.SetSession(Me._allRetrievedData, arlData)
        dtgMain.CurrentPageIndex = PageIndex
        dtgMain.VirtualItemCount = arlData.Count
        dtgMain.DataSource = ArrayListPager.DoPage(arlData, dtgMain.CurrentPageIndex, dtgMain.PageSize)
        dtgMain.DataBind()
    End Sub

    Private Function GetData() As ArrayList

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

        Return ColorList

    End Function

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

    Private Sub InitHeader()
        Dim dtFrom As Date = DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1)
        Dim dtTo As Date = dtFrom.AddMonths(1) '.AddDays(-1)
        Dim nDays As Integer = DateDiff(DateInterval.Day, dtFrom, dtTo)
        Dim i As Integer
        Dim arlNH As ArrayList = GetHolidayDates()

        viewstate.Item("nDays") = nDays
        For i = 1 To 31
            If i <= nDays Then
                dtgMain.Columns(2 + i).Visible = True
                dtgMain.Columns(2 + i).HeaderText = i.ToString
                For Each oNH As NationalHoliday In arlNH
                    If oNH.HolidayDateTime.ToString("yyyy.MM.dd") = DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, i).ToString("yyyy.MM.dd") Then
                        dtgMain.Columns(2 + i).Visible = False
                    End If
                Next
            Else
                dtgMain.Columns(2 + i).Visible = False
            End If
        Next
        BindContractDate()
    End Sub

    Private Function GetHolidayDates() As ArrayList
        Dim oNHFac As NationalHolidayFacade = New NationalHolidayFacade(User)
        Dim cNH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        cNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayMonth", MatchType.Exact, ddlMonth.SelectedValue))
        cNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayYear", MatchType.Exact, ddlYear.SelectedValue))

        Return oNHFac.Retrieve(cNH)

    End Function

    Private Sub BindContractDate()
        Dim dtContract As Date = CommonFunction.GetMaxContractDateOfRedemption(ddlMonth.SelectedValue, ddlYear.SelectedValue) ' DateSerial(1990, 1, 1)

        icContractDate.Value = dtContract
        txtContractHour.Text = dtContract.Hour
        txtContractMinute.Text = dtContract.Minute
        txtContractSecond.Text = dtContract.Second
    End Sub

    Private Sub SetContractDate()
        Dim arlRC As New ArrayList
        Dim objRCFac As RedemptionCeilingFacade = New RedemptionCeilingFacade(User)
        Dim crtRC As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionCeiling), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim dtContract As Date = Date.MinValue
        crtRC.opAnd(New Criteria(GetType(RedemptionCeiling), "PeriodMonth", MatchType.Exact, ddlMonth.SelectedValue))
        crtRC.opAnd(New Criteria(GetType(RedemptionCeiling), "PeriodYear", MatchType.Exact, ddlYear.SelectedValue))

        arlRC = objRCFac.Retrieve(crtRC)
        If arlRC.Count > 0 Then
            For Each objRC As RedemptionCeiling In arlRC
                objRC.MaxContractDate = icContractDate.Value.AddHours(txtContractHour.Text).AddMinutes(txtContractMinute.Text).AddSeconds(txtContractSecond.Text)
                'objRC.MaxContractDate = icContractDate.Value & " " & TimeSerial(txtContractHour.Text, txtContractMinute.Text, txtContractSecond.Text)
                objRCFac.Update(objRC)
            Next
        End If
    End Sub

    Private Sub FillDTGRow(ByRef e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)
        Dim oVC As VechileColor
        Dim i As Integer
        Dim txtID As TextBox = e.Item.FindControl("txtID")
        Dim txtTotal As TextBox
        Dim lblCode As Label = e.Item.FindControl("lblCode")
        Dim txtSubTotal As TextBox = e.Item.FindControl("txtSubTotal")
        Dim TotPerRow As Integer = 0
        Dim TotTemp As Integer
        Dim TotPerColumns(31) As Integer

        TotPerColumns = Me._sessHelper.GetSession(Me._totalPerColumns)

        oVC = New VechileColorFacade(User).Retrieve(CType(txtID.Text, Integer))
        lblCode.Text = oVC.VechileType.VechileTypeCode & oVC.ColorCode
        For i = 1 To nDays
            txtTotal = e.Item.FindControl("txtS" & i.ToString)
            TotTemp = GetTotalVehicleColorPerDay(oVC, DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, i))
            txtTotal.Text = TotTemp
            TotPerRow += TotTemp
            TotPerColumns(i - 1) += TotTemp
        Next
        TotPerColumns(31) += TotPerRow
        txtSubTotal.Text = FormatNumber(TotPerRow, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
    End Sub

    Private Function GetTotalVehicleColorPerDay(ByVal pVC As VechileColor, ByVal pDay As Date) As Integer
        Dim Rsl As Integer = 0
        Dim arlRH As New ArrayList
        Dim crtRH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RedemptionHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        crtRH.opAnd(New Criteria(GetType(RedemptionHeader), "VechileColor.ID", MatchType.Exact, pVC.ID))
        crtRH.opAnd(New Criteria(GetType(RedemptionHeader), "PeriodDate", MatchType.Exact, pDay))
        arlRH = New RedemptionHeaderFacade(User).Retrieve(crtRH)
        If arlRH.Count = 0 Then
            Rsl = 0
        Else
            Rsl = CType(arlRH(0), RedemptionHeader).EstimationStock
        End If
        Return Rsl
    End Function

    Private Function IsValidToSave() As Boolean
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)
        Dim oVC As VechileColor
        Dim oVCFac As VechileColorFacade = New VechileColorFacade(User)
        Dim i As Integer
        Dim txtID As TextBox
        Dim txtTotal As TextBox
        Dim oRH As RedemptionHeader
        Dim oRHFac As RedemptionHeaderFacade = New RedemptionHeaderFacade(User)
        Dim nError As Integer = 0
        Dim dtTemp As Date
        Dim lblCode As Label

        For Each di As DataGridItem In dtgMain.Items
            txtID = di.FindControl("txtID")
            lblCode = di.FindControl("lblCode")
            oVC = oVCFac.Retrieve(CType(txtID.Text, Integer))
            For i = 1 To nDays
                dtTemp = DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, i)
                txtTotal = di.FindControl("txtS" & i.ToString)
                If txtTotal.Text.Trim = "" Then txtTotal.Text = "0"
                oRH = GetRH(oVC.ID, dtTemp)
                If IsNothing(oRH) OrElse oRH.ID < 1 Then
                Else
                    If CType(txtTotal.Text, Integer) <> oRH.EstimationStock Then 'berarti di-edit oleh user
                        If CType(txtTotal.Text, Integer) < oRH.TotalRequest Then
                            MessageBox.Show(" Stok Tidak Valid.\n Total Request " & lblCode.Text & " Tanggal " & dtTemp.ToString("dd/MM/yyyy") & " adalah " & oRH.TotalRequest.ToString)
                            Return False
                        End If
                    End If
                End If
            Next
        Next
        Return True
    End Function

    Private Function SaveData() As Boolean
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)
        Dim oVC As VechileColor
        Dim oVCFac As VechileColorFacade = New VechileColorFacade(User)
        Dim i As Integer
        Dim txtID As TextBox
        Dim txtTotal As TextBox
        Dim oRH As RedemptionHeader
        Dim oRHFac As RedemptionHeaderFacade = New RedemptionHeaderFacade(User)
        Dim nError As Integer = 0
        Dim dtTemp As Date

        For Each di As DataGridItem In dtgMain.Items
            txtID = di.FindControl("txtID")
            oVC = oVCFac.Retrieve(CType(txtID.Text, Integer))
            For i = 1 To nDays
                'If dtgMain.Columns(i).Visible = True Then
                dtTemp = DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, i)
                txtTotal = di.FindControl("txtS" & i.ToString)
                If txtTotal.Text.Trim = "" Then txtTotal.Text = "0"
                oRH = GetRH(oVC.ID, dtTemp)
                If IsNothing(oRH) OrElse oRH.ID < 1 Then
                    oRH.VechileColor = oVC
                    oRH.PeriodDate = dtTemp
                    oRH.EstimationStock = CType(txtTotal.Text, Integer)
                    nError += IIf(oRHFac.Insert(oRH) = -1, 1, 0)
                Else
                    oRH.VechileColor = oVC
                    oRH.PeriodDate = dtTemp
                    oRH.EstimationStock = CType(txtTotal.Text, Integer)
                    oRHFac.Update(oRH)
                End If
                'End If
            Next
        Next
        If nError > 0 Then
            Return False
        Else
            Return True
        End If
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


    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "Estimation Stock " & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

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
        Dim j As Integer = 0
        Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)
        Dim oVC As VechileColor
        Dim strTemp As String
        Dim TotPerRow As Integer = 0
        Dim TotTemp As Integer = 0
        Dim TotPerColumns(31) As Long

        If Not IsNothing(data) Then
            'Heading
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Estimation Stock")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)

            itemLine.Remove(0, itemLine.Length)
            'TableHeader 
            itemLine.Append("No" & tab)
            itemLine.Append("Tipe/Warna" & tab)
            itemLine.Append("Deskripsi" & tab)
            strTemp = ""
            For j = 1 To nDays
                If dtgMain.Columns(j + 2).Visible <> False Then
                    strTemp &= j.ToString & tab
                End If
            Next
            strTemp &= "Total"
            itemLine.Append(strTemp)
            sw.WriteLine(itemLine.ToString())
            'Data
            For j = 1 To nDays
                TotPerColumns(j - 1) = 0
            Next
            For i = 0 To data.Count - 1
                TotPerRow = 0
                oVC = CType(data(i), VechileColor)
                strTemp = (i + 1).ToString & tab & oVC.VechileType.VechileTypeCode & oVC.ColorCode & tab & oVC.MaterialDescription & tab
                For j = 1 To nDays
                    If dtgMain.Columns(j + 2).Visible <> False Then
                        TotTemp = GetTotalVehicleColorPerDay(oVC, DateSerial(ddlYear.SelectedValue, ddlMonth.SelectedValue, j))
                        strTemp &= TotTemp & tab
                        TotPerRow += TotTemp
                        TotPerColumns(j - 1) += TotTemp
                    End If
                Next
                strTemp &= TotPerRow
                TotPerColumns(31) += TotPerRow
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(strTemp)
                sw.WriteLine(itemLine.ToString())
            Next
            'Footer
            For i = 0 To 0 ' data.Count - 1
                strTemp = "" & tab & "TOTAL" & tab & "" & tab
                For j = 1 To nDays
                    If dtgMain.Columns(j + 2).Visible <> False Then
                        strTemp &= TotPerColumns(j - 1) & tab
                    End If
                Next
                strTemp &= TotPerColumns(31)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(strTemp)
                sw.WriteLine(itemLine.ToString())
            Next
        End If
    End Sub


#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Initialization()
        End If
        CheckPrivelege()
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
        BindVehicleType(True)
        BindContractDate()
    End Sub

    Private Sub ddlSubCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubCategory.SelectedIndexChanged
        BindVehicleType(False)
        BindContractDate()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDTG(0)
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim TotPerColumns(31) As Integer
            Dim i As Integer

            For i = 0 To 31
                TotPerColumns(i) = 0
            Next
            Me._sessHelper.SetSession(Me._totalPerColumns, TotPerColumns)
        ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = (dtgMain.PageSize * dtgMain.CurrentPageIndex) + e.Item.ItemIndex + 1
            FillDTGRow(e)
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Dim i As Integer = 0
            Dim nDays As Integer = CType(viewstate.Item("nDays"), Integer)
            Dim TotPerColumns(31) As Integer

            TotPerColumns = Me._sessHelper.GetSession(Me._totalPerColumns)
            e.Item.Cells(1).Text = "Total"
            For i = 1 To nDays
                e.Item.Cells(i + 2).Text = TotPerColumns(i - 1)
            Next
            e.Item.Cells(Me.dtgMain.Columns.Count - 2).Text = FormatNumber(TotPerColumns(31), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.IsValidToSave() = False Then
            Exit Sub
        End If
        If Not SaveData() Then
            MessageBox.Show(SR.SaveFail)
        Else
            Me.BindDTG(Me.dtgMain.CurrentPageIndex)
        End If
    End Sub

    Private Sub dtgMain_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMain.PageIndexChanged
        BindDTG(e.NewPageIndex)
    End Sub

    Private Sub btnSetContract_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetContract.Click
        Me.SetContractDate()
    End Sub

    Private Sub ddlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlType.SelectedIndexChanged
        BindddlTipeWarna(ddlType.SelectedItem.Text)
        BindContractDate()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        DoDownload(CType(Me._sessHelper.GetSession(Me._allRetrievedData), ArrayList))
    End Sub

#End Region
End Class
