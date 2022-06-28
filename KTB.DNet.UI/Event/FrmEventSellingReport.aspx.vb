#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmEventSellingReport
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSalesmanArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbPeriode As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlJenisKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlModel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlNamaKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEvent As System.Web.UI.WebControls.DataGrid
    Protected WithEvents tblCategoryModelType As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ddlGroupDealer As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnNew As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCategoryItem As System.Web.UI.WebControls.DropDownList
    Protected WithEvents calDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents calEventDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents txtGroupDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchGroupCode As System.Web.UI.WebControls.Label
    Protected WithEvents rbGroupDealer As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbDealer As System.Web.UI.WebControls.RadioButton
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents rqvGroupDealer As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents cbEventDate As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Constants "
    Private Const DataSourceName As String = "EventSellingReportSource"
    Private Const PageModeName As String = "EventSellingReportPageMode"
    Private Const StoreCritName As String = "EventSellingReportStoreCrit"
    Private Const EventParameterIDName As String = "EventSellingReportEventParameterID"
#End Region

#Region " Private Variables "
    Private sesHelper As New SessionHelper
    Private objFacadeVechileType As New VechileTypeFacade(User)
    Private objFacadeCategory As New CategoryFacade(User)
    Private objFacadeDealerGroup As New DealerGroupFacade(User)
    Private IsLoginDealer As Boolean = False
#End Region

#Region " Custom Method "
    Private Property PageMode() As enumMode.Mode
        Get
            If IsNothing(sesHelper.GetSession(PageModeName)) Then
                sesHelper.SetSession(PageModeName, enumMode.Mode.ViewMode)
            End If
            Return sesHelper.GetSession(PageModeName)
        End Get
        Set(ByVal Value As enumMode.Mode)
            sesHelper.SetSession(PageModeName, Value)
        End Set
    End Property
    Private Property GridDataSource() As ArrayList
        Get
            If IsNothing(sesHelper.GetSession(DataSourceName)) Then
                sesHelper.SetSession(DataSourceName, New ArrayList)
            End If
            Return sesHelper.GetSession(DataSourceName)
        End Get
        Set(ByVal Value As ArrayList)
            sesHelper.SetSession(DataSourceName, Value)
        End Set
    End Property
    Private Property EventParameterID() As Int32
        Get
            If sesHelper.GetSession(EventParameterIDName) Is Nothing Then
                sesHelper.SetSession(EventParameterIDName, -1)
            End If
            Return sesHelper.GetSession(EventParameterIDName)
        End Get
        Set(ByVal Value As Int32)
            sesHelper.SetSession(EventParameterIDName, Value)
        End Set
    End Property
    Private Property StoreCondition() As CriteriaComposite
        Get
            If sesHelper.GetSession(StoreCritName) Is Nothing Then
                sesHelper.SetSession(StoreCritName, BuiltCriteriaComposite)
            End If
            Return sesHelper.GetSession(StoreCritName)
        End Get
        Set(ByVal Value As CriteriaComposite)
            sesHelper.SetSession(StoreCritName, Value)
        End Set
    End Property
    Private Sub FillSalesmanArea()
        ddlSalesmanArea.Items.Clear()
        Dim objArea As New SalesmanAreaFacade(User)
        ddlSalesmanArea.DataSource = objArea.RetrieveActiveList
        ddlSalesmanArea.DataTextField = "AreaDesc"
        ddlSalesmanArea.DataValueField = "ID"
        ddlSalesmanArea.DataBind()
        ddlSalesmanArea.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub FillJenisKegiatan()
        ddlJenisKegiatan.Items.Clear()
        Dim objActivityType As New ActivityTypeFacade(User)
        ddlJenisKegiatan.DataSource = objActivityType.RetrieveActiveList()
        ddlJenisKegiatan.DataTextField = "ActivityName"
        ddlJenisKegiatan.DataValueField = "ID"
        ddlJenisKegiatan.DataBind()
        ddlJenisKegiatan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub ShowModelCategoryType(ByVal IsVisible As Boolean)
        tblCategoryModelType.Visible = IsVisible
    End Sub
    Private Sub FillCategory()
        ddlCategory.Items.Clear()
        Dim objCategory As New CategoryFacade(User)
        ddlCategory.DataSource = objCategory.RetrieveActiveList
        ddlCategory.DataTextField = "Description"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Sub FillModel(ByVal CategoryID As Integer)
        ddlModel.Items.Clear()
        If CategoryID > -1 Then
            Dim objModel As New VechileModelFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VechileModel), "Category", CategoryID))
            ddlModel.DataSource = objModel.RetrieveList("Description", Sort.SortDirection.ASC, crit)
            ddlModel.DataTextField = "Description"
            ddlModel.DataValueField = "ID"
            ddlModel.DataBind()
            ddlModel.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlModel.Items.Insert(0, New ListItem("Pilih Kategori", -1))
        End If
    End Sub
    Private Sub FillType(ByVal ModelID As Integer)
        ddlType.Items.Clear()
        If ModelID > -1 Then
            Dim objType As New VechileTypeFacade(User)
            Dim crit As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", CType(DBRowStatus.Active, Short)))
            Dim sorts As New SortCollection
            sorts.Add(New Sort(GetType(VechileType), "Description", Sort.SortDirection.ASC))
            crit.opAnd(New Criteria(GetType(VechileType), "VechileModel", ModelID))
            ddlType.DataSource = objType.RetrieveByCriteria(crit, sorts)
            ddlType.DataTextField = "Description"
            ddlType.DataValueField = "ID"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlType.Items.Insert(0, New ListItem("Pilih Model", -1))
        End If
    End Sub
    Private Sub FillYear()
        ddlYear.Items.Clear()
        For i As Int32 = Now.Year - 5 To Now.Year + 5
            ddlYear.Items.Add(i)
        Next
        ddlYear.SelectedValue = Now.Year
    End Sub
    Private Sub FillNamaKegiatan()
        Dim objFacade As New EventParameterFacade(User)
        ddlNamaKegiatan.Items.Clear()
        If ddlJenisKegiatan.SelectedValue <> -1 Then
            If IsLoginDealer Then
                ddlNamaKegiatan.DataSource = objFacade.RetrieveNamaKegiatan(ddlJenisKegiatan.SelectedValue, _
                    txtDealerCode.Text)
            Else
                ddlNamaKegiatan.DataSource = objFacade.RetrieveNamaKegiatan(ddlJenisKegiatan.SelectedValue)
            End If
            ddlNamaKegiatan.DataTextField = "EventName"
            ddlNamaKegiatan.DataValueField = "ID"
            ddlNamaKegiatan.DataBind()
            ddlNamaKegiatan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlNamaKegiatan.Items.Insert(0, New ListItem("Pilih Jenis Kegiatan", -1))
        End If
    End Sub
    Private Sub FillDealerGroup()
        Dim objFacade As New DealerGroupFacade(User)
        ddlGroupDealer.DataSource = objFacade.RetrieveActiveList
        ddlGroupDealer.DataTextField = "GroupName"
        ddlGroupDealer.DataValueField = "ID"
        ddlGroupDealer.DataBind()
        ddlGroupDealer.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub
    Private Property GridSortColumn() As String
        Get
            If ViewState("SortColumn") Is Nothing Then
                ViewState("SortColumn") = "ID"
            ElseIf ViewState("SortColumn") = "Dealer.DealerName" AndAlso rbGroupDealer.Checked Then
                ViewState("SortColumn") = "Dealer.GroupName"
            ElseIf ViewState("SortColumn") = "Dealer.GroupName" AndAlso rbDealer.Checked Then
                ViewState("SortColumn") = "Dealer.DealerName"
            End If
            Return ViewState("SortColumn")
        End Get
        Set(ByVal Value As String)
            ViewState("SortColumn") = Value
        End Set
    End Property
    Private Property GridSortDirection() As Sort.SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = Sort.SortDirection.ASC
            End If
            Return ViewState("SortDirection")
        End Get
        Set(ByVal Value As Sort.SortDirection)
            ViewState("SortDirection") = Value
        End Set
    End Property
    Private Function IsFindValid() As Boolean
        Dim IsValid As Boolean = True
        If cbPeriode.Checked AndAlso calDari.Value > calSampai.Value Then
            MessageBox.Show(SR.InvalidRangeDate)
            IsValid = False
        End If
        Return IsValid
    End Function
    Private Function FindDealerGroup(ByVal DealerCode As String) As Int32
        Dim objFacade As New DealerFacade(User)
        Dim dealer As dealer = objFacade.GetDealer(DealerCode)
        If Not IsNothing(dealer) Then
            If Not IsNothing(dealer.DealerGroup) Then
                Return dealer.DealerGroup.ID
            End If
        End If
        Return -1
    End Function
    Private Function BuiltCriteriaComposite() As CriteriaComposite
        Dim objComposite As CriteriaComposite
        If rbDealer.Checked Then
            objComposite = New CriteriaComposite(New Criteria(GetType(EventLaporanPenjualan), "RowStatus", _
                CType(DBRowStatus.Active, Short)))
            If txtDealerCode.Text.Length > 0 Then
                'For Each dealerCode As String In txtDealerCode.Text.Split(";"c)
                '    objComposite.opAnd(New Criteria(GetType(EventLaporanPenjualan), _
                '        "Dealer.DealerCode", dealerCode))
                'Next


                objComposite.opAnd(New Criteria(GetType(EventLaporanPenjualan), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
            End If
            If ddlSalesmanArea.SelectedIndex > 0 Then
                objComposite.opAnd(New Criteria(GetType(EventLaporanPenjualan), _
                    "EventParameter.SalesmanArea", ddlSalesmanArea.SelectedValue))
            End If
            If cbPeriode.Checked Then
                objComposite.opAnd(New Criteria(GetType(EventLaporanPenjualan), _
                    "EventParameter.EventDateStart", MatchType.GreaterOrEqual, _
                    calDari.Value))
                objComposite.opAnd(New Criteria(GetType(EventLaporanPenjualan), _
                    "EventParameter.EventDateEnd", MatchType.LesserOrEqual, _
                    calSampai.Value))
            End If
            If ddlJenisKegiatan.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventLaporanPenjualan), "EventParameter.ActivityType", _
                    ddlJenisKegiatan.SelectedValue))
            End If
            If ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Small_Gathering, Integer) _
                OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Launching_Gathering, Integer) _
                OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Exhibition_Khusus, Integer) _
                OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Showroom_Event, Integer) _
                OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Others, Integer) Then
                If ddlCategory.SelectedValue <> -1 Then
                    objComposite.opAnd(New Criteria(GetType(EventLaporanPenjualan), "EventParameter.Category", ddlCategory.SelectedValue))
                End If
                If ddlModel.SelectedValue <> -1 Then
                    objComposite.opAnd(New Criteria(GetType(EventLaporanPenjualan), "EventParameter.VechileType.VechileModel", _
                        ddlModel.SelectedValue))
                End If
                If ddlType.SelectedValue <> -1 Then
                    objComposite.opAnd(New Criteria(GetType(EventLaporanPenjualan), "EventParameter.VechileType", _
                        ddlType.SelectedValue))
                End If
            End If
            If ddlNamaKegiatan.SelectedIndex > 0 Then
                objComposite.opAnd(New Criteria(GetType(EventLaporanPenjualan), "EventParameter.EventName", _
                    ddlNamaKegiatan.SelectedItem.Text))
                objComposite.opAnd(New Criteria(GetType(EventLaporanPenjualan), "EventParameter.EventYear", _
                    ddlYear.SelectedValue))
            End If
            If cbEventDate.Checked Then
                Dim objFacade As New EventProposalFacade(User)
                Dim crit As New CriteriaComposite(New Criteria(GetType(EventProposal), _
                    "RowStatus", CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(EventProposal), _
                    "ActivitySchedule", calEventDate.Value))
                Dim paramId As New StringBuilder("(")
                For Each prop As EventProposal In objFacade.Retrieve(crit)
                    If paramId.Length > 1 Then
                        paramId.Append(",")
                    End If
                    paramId.Append(prop.EventParameter.ID)
                Next
                paramId.Append(")")
                objComposite.opAnd(New Criteria(GetType(EventLaporanPenjualan), _
                    "EventParameter", MatchType.InSet, paramId.ToString))
            End If
        Else
            objComposite = New CriteriaComposite(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), _
                "RowStatus", CType(DBRowStatus.Active, Short)))
            If txtGroupDealer.Text.Trim().Length > 0 Then
                'For Each groupCode As String In txtGroupDealer.Text.Split(";"c)
                '    objComposite.opAnd(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), _
                '        "Dealer.DealerGroupCode", groupCode))
                'Next
                objComposite.opAnd(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), "Dealer.DealerGroupCode", MatchType.InSet, "('" + Replace(txtGroupDealer.Text.Trim(), ";", "','") + "')"))


            End If
            If ddlSalesmanArea.SelectedIndex > 0 Then
                objComposite.opAnd(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), _
                    "EventParameter.SalesmanArea", ddlSalesmanArea.SelectedValue))
            End If
            If cbPeriode.Checked Then
                objComposite.opAnd(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), _
                    "EventParameter.EventDateStart", MatchType.GreaterOrEqual, _
                    calDari.Value))
                objComposite.opAnd(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), _
                    "EventParameter.EventDateEnd", MatchType.LesserOrEqual, _
                    calSampai.Value))
            End If
            If ddlJenisKegiatan.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), "EventParameter.ActivityType", _
                    ddlJenisKegiatan.SelectedValue))
            End If
            If ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Small_Gathering, Integer) _
                OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Launching_Gathering, Integer) _
                OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Exhibition_Khusus, Integer) _
                OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Showroom_Event, Integer) _
                OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Others, Integer) Then
                If ddlCategory.SelectedValue <> -1 Then
                    objComposite.opAnd(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), "EventParameter.Category", ddlCategory.SelectedValue))
                End If
                If ddlModel.SelectedValue <> -1 Then
                    objComposite.opAnd(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), "EventParameter.VechileType.VechileModel", _
                        ddlModel.SelectedValue))
                End If
                If ddlType.SelectedValue <> -1 Then
                    objComposite.opAnd(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), "EventParameter.VechileType", _
                        ddlType.SelectedValue))
                End If
            End If
            If ddlNamaKegiatan.SelectedIndex > 0 Then
                objComposite.opAnd(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), "EventParameter.EventName", _
                    ddlNamaKegiatan.SelectedItem.Text))
                objComposite.opAnd(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), "EventParameter.EventYear", _
                    ddlYear.SelectedValue))
            End If
            If cbEventDate.Checked Then
                Dim objFacade As New EventProposalFacade(User)
                Dim crit As New CriteriaComposite(New Criteria(GetType(EventProposal), _
                    "RowStatus", CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(EventProposal), _
                    "ActivitySchedule", calEventDate.Value))
                Dim paramId As New StringBuilder("(")
                For Each prop As EventProposal In objFacade.Retrieve(crit)
                    If paramId.Length > 1 Then
                        paramId.Append(",")
                    End If
                    paramId.Append(prop.EventParameter.ID)
                Next
                paramId.Append(")")
                objComposite.opAnd(New Criteria(GetType(V_EventLaporanPenjualanGroupDealer), _
                    "EventParameter", MatchType.InSet, paramId.ToString))
            End If
        End If
        Return objComposite
    End Function
    Private Sub InitDisplay()
        Dim objDealer As Dealer = sesHelper.GetSession("DEALER")
        IsLoginDealer = objDealer.Title <> EnumDealerTittle.DealerTittle.KTB
        btnNew.Visible = IsLoginDealer AndAlso Not btnSave.Visible
        If IsLoginDealer Then
            txtDealerCode.Text = objDealer.DealerCode
            txtDealerCode.Enabled = False
            lblSearchDealer.Visible = False
            txtGroupDealer.Text = objDealer.DealerGroup.DealerGroupCode
            txtGroupDealer.Enabled = False
            lblSearchGroupCode.Visible = False
            ddlGroupDealer.SelectedValue = objDealer.DealerGroup.ID
            ddlGroupDealer.Enabled = False
            rbGroupDealer.Enabled = False
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblSearchGroupCode.Attributes("onclick") = "ShowPPDealerGroupSelection();"
    End Sub
    Private Function RetrieveDataBaseOnCrit(ByRef totalRows As Int32) As ArrayList
        Dim objFacade As New EventLaporanPenjualanFacade(User)
        If rbDealer.Checked Then
            Return objFacade.RetrieveActiveList(StoreCondition, _
                dtgEvent.CurrentPageIndex + 1, dtgEvent.PageSize, _
                totalRows, GridSortColumn, GridSortDirection)
        Else
            Return objFacade.RetrieveActiveListForGroupCrit(StoreCondition, _
                dtgEvent.CurrentPageIndex + 1, dtgEvent.PageSize, _
                totalRows, GridSortColumn, GridSortDirection)
        End If
    End Function
    Private Sub BindGrid()
        dtgEvent.DataSource = GridDataSource
        dtgEvent.DataBind()
    End Sub
    Private Sub EnableControl(ByVal IsEnable As Boolean)
        txtGroupDealer.Enabled = IsEnable AndAlso Not IsLoginDealer
        txtDealerCode.Enabled = IsEnable AndAlso Not IsLoginDealer
        cbPeriode.Enabled = IsEnable
        calDari.Enabled = IsEnable AndAlso cbPeriode.Checked
        calSampai.Enabled = IsEnable AndAlso cbPeriode.Checked
        ddlSalesmanArea.Enabled = IsEnable
        ddlJenisKegiatan.Enabled = IsEnable
        ddlNamaKegiatan.Enabled = IsEnable
        ddlYear.Enabled = IsEnable
        rbDealer.Enabled = IsEnable
        rbGroupDealer.Enabled = IsEnable AndAlso Not IsLoginDealer
        lblSearchDealer.Visible = IsEnable AndAlso Not IsLoginDealer
        lblSearchGroupCode.Visible = IsEnable AndAlso Not IsLoginDealer
        cbEventDate.Enabled = IsEnable
        calEventDate.Enabled = IsEnable AndAlso cbEventDate.Checked
    End Sub
    Private Sub ChangeLayout(ByVal display As enumMode.Mode)
        PageMode = display
        Select Case PageMode
            Case enumMode.Mode.NewItemMode
                btnNew.Visible = False
                btnSave.Visible = True
                btnCancel.Visible = True
                dtgEvent.AllowPaging = False
                dtgEvent.AllowSorting = False
                dtgEvent.Columns(1).Visible = False
                dtgEvent.Columns(3).Visible = False
                dtgEvent.Columns(4).Visible = False
                txtGroupDealer.Visible = False
                lblSearchGroupCode.Visible = False
                ddlGroupDealer.Visible = True
                EnableControl(False)
                dtgEvent.ShowFooter = True
                GridDataSource = Nothing
                dtgEvent.Columns(9).Visible = True
                BindGridForNew()
            Case enumMode.Mode.ViewMode
                btnNew.Visible = True AndAlso IsLoginDealer
                btnSave.Visible = False
                btnCancel.Visible = False
                dtgEvent.AllowPaging = True
                dtgEvent.AllowSorting = True
                dtgEvent.Columns(1).Visible = True
                dtgEvent.Columns(3).Visible = True AndAlso rbDealer.Checked AndAlso Not IsLoginDealer
                If rbDealer.Checked Then
                    dtgEvent.Columns(4).HeaderText = "Nama Dealer"
                Else
                    dtgEvent.Columns(4).HeaderText = "Nama Group"
                End If
                dtgEvent.Columns(4).Visible = True
                txtGroupDealer.Visible = True
                lblSearchGroupCode.Visible = True
                ddlGroupDealer.Visible = False
                EnableControl(True)
                dtgEvent.ShowFooter = False
                dtgEvent.Columns(9).Visible = False
        End Select
    End Sub
    Private Sub BindGridForSearch()
        dtgEvent.EditItemIndex = -1
        BindGrid()
    End Sub
    Private Sub BindGridForNew()
        BindGrid()
    End Sub
    Private Function DeleteRow(ByVal index As Int32) As ArrayList
        Dim arr As ArrayList = DirectCast(sesHelper.GetSession(DataSourceName), _
            ArrayList)
        arr.RemoveAt(index)
        Return arr
    End Function
    Private Function AddRow(ByVal laporan As EventLaporanPenjualan) As ArrayList
        Dim arr As ArrayList = DirectCast(sesHelper.GetSession(DataSourceName), _
            ArrayList)
        arr.Add(laporan)
        Return arr
    End Function
    Private Sub ProcessRowToDomain(ByVal e As DataGridItem, ByVal laporan As EventLaporanPenjualan)
        laporan.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
        Dim ddlVehicleType As DropDownList
        Dim txtCarDescription As TextBox
        Dim txtJumlah As TextBox
        If e.DataSetIndex > -1 Then
            ddlVehicleType = e.FindControl("ddlVehicleTypeItem")
            txtCarDescription = e.FindControl("txtCarDescriptionItem")
            txtJumlah = e.FindControl("txtJumlahItem")
        Else
            ddlVehicleType = e.FindControl("ddlVehicleTypeFooter")
            txtCarDescription = e.FindControl("txtCarDescriptionFooter")
            txtJumlah = e.FindControl("txtJumlahFooter")
        End If
        laporan.EventParameter = RetrieveEventParameter()
        laporan.Description = txtCarDescription.Text
        laporan.Jumlah = txtJumlah.Text
        laporan.VechileType = New VechileTypeFacade(User).Retrieve(Int32.Parse(ddlVehicleType.SelectedValue))
    End Sub
    Private Function GetVehicleType(ByVal model As Int32) As ArrayList
        Dim crits As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(VechileType), "VechileModel.Category", model))
        Dim sorts As New SortCollection
        sorts.Add(New Sort(GetType(VechileType), "Description", Sort.SortDirection.ASC))
        Dim hsType As New Hashtable
        For Each vType As VechileType In objFacadeVechileType.RetrieveByCriteria(crits, sorts)
            If Not hsType.ContainsKey(vType.Description) Then
                hsType.Add(vType.Description, vType)
            End If
        Next
        Dim objArrayData As New ArrayList
        For Each data As VechileType In hsType.Values
            objArrayData.Add(data)
        Next
        Return objArrayData
    End Function
    Private Function IsValidRow(ByVal item As DataGridItem, ByVal IsAdd As Boolean) As Boolean
        Dim valid As Boolean = True
        Dim ddlVehicleType As DropDownList
        Dim txtJumlah As TextBox
        If IsAdd Then
            ddlVehicleType = item.FindControl("ddlVehicleTypeFooter")
            txtJumlah = item.FindControl("txtJumlahFooter")
        Else
            ddlVehicleType = item.FindControl("ddlVehicleTypeItem")
            txtJumlah = item.FindControl("txtJumlahItem")
        End If
        If ddlGroupDealer.SelectedIndex = 0 Then
            valid = False
            MessageBox.Show("Pilih Group Dealer")
        End If
        If ddlVehicleType.SelectedIndex = 0 Then
            valid = False
            MessageBox.Show("Pilih Tipe Kendaraan")
        End If
        If txtJumlah.Text.Length = 0 Then
            valid = False
            MessageBox.Show("Masukkan Jumlah")
        End If
        Return valid
    End Function
    Private Function IsValidSave() As Boolean
        Dim IsValid As Boolean = True
        If GridDataSource.Count = 0 Then
            MessageBox.Show("Tidak ada data yang di proses")
            IsValid = False
        End If
        Return IsValid
    End Function
    Private Function IsNotValidEventParameter() As Boolean
        Return IsNothing(RetrieveEventParameter)
    End Function
    Private Function RetrieveEventParameter() As EventParameter
        Dim objFacade As New EventParameterFacade(User)
        Dim crits As New CriteriaComposite(New Criteria(GetType(EventParameter), _
            "RowStatus", CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(EventParameter), "ActivityType", ddlJenisKegiatan.SelectedValue))
        crits.opAnd(New Criteria(GetType(EventParameter), "EventName", ddlNamaKegiatan.SelectedItem.Text))
        crits.opAnd(New Criteria(GetType(EventParameter), "EventYear", ddlYear.SelectedValue))
        Dim arr As ArrayList = objFacade.Retrieve(crits)
        If arr.Count > 0 Then
            Return arr(0)
        End If
    End Function
    Private Function IsValidNew() As Boolean
        Dim IsValid As Boolean = True
        If ddlNamaKegiatan.SelectedIndex = 0 Then
            IsValid = False
            MessageBox.Show("Pilih Nama Kegiatan")
        ElseIf IsNotValidEventParameter() Then
            IsValid = False
            MessageBox.Show(String.Format("Tidak ada Parameter untuk kegiatan \'{0}\' pada tahun {1}", _
                ddlNamaKegiatan.SelectedItem.Text, ddlYear.SelectedValue))
        End If
        Return IsValid
    End Function
#End Region

#Region " Event Handler "
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            FillSalesmanArea()
            FillJenisKegiatan()
            FillNamaKegiatan()
            ShowModelCategoryType(False)
            FillCategory()
            ddlCategory_SelectedIndexChanged(Nothing, Nothing)
            ddlModel_SelectedIndexChanged(Nothing, Nothing)
            FillYear()
            FillDealerGroup()
        End If
        InitDisplay()
    End Sub
    Private Sub ddlJenisKegiatan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlJenisKegiatan.SelectedIndexChanged
        ShowModelCategoryType(ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Small_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Launching_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Exhibition_Khusus, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Showroom_Event, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Others, Integer))
        FillNamaKegiatan()
    End Sub
    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        FillModel(ddlCategory.SelectedValue)
        FillType(ddlModel.SelectedValue)
    End Sub
    Private Sub ddlModel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlModel.SelectedIndexChanged
        FillType(ddlModel.SelectedValue)
    End Sub
    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If IsFindValid() Then
            ChangeLayout(enumMode.Mode.ViewMode)
            GridSortColumn = "ID"
            GridSortDirection = Sort.SortDirection.ASC
            StoreCondition = BuiltCriteriaComposite()
            Dim totalRows As Int32 = 0
            GridDataSource = RetrieveDataBaseOnCrit(totalRows)
            EventParameterID = ddlNamaKegiatan.SelectedValue
            If totalRows = 0 Then
                MessageBox.Show("Data tidak ditemukan")
            End If
            dtgEvent.VirtualItemCount = totalRows
            BindGridForSearch()
        End If
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        If IsValidNew() Then
            GridDataSource = Nothing
            Dim objFacade As New EventParameterFacade(User)
            Dim keg As EventParameter = objFacade.Retrieve(CType(ddlNamaKegiatan.SelectedValue, Int32))
            cbPeriode.Checked = True
            calDari.Value = keg.EventDateStart
            calSampai.Value = keg.EventDateEnd
            ChangeLayout(enumMode.Mode.NewItemMode)
        End If
    End Sub
    Private Sub dtgEvent_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEvent.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                dtgEvent.EditItemIndex = e.Item.ItemIndex
                BindGridForNew()
            Case "Delete"
                DeleteRow(e.Item.DataSetIndex)
                BindGridForNew()
            Case "Add"
                If IsValidRow(e.Item, True) Then
                    Dim laporan As New EventLaporanPenjualan
                    ProcessRowToDomain(e.Item, laporan)
                    AddRow(laporan)
                    BindGridForNew()
                End If
            Case "Update"
                If IsValidRow(e.Item, False) Then
                    Dim laporan As EventLaporanPenjualan = GridDataSource(e.Item.DataSetIndex)
                    ProcessRowToDomain(e.Item, laporan)
                    dtgEvent.EditItemIndex = -1
                    BindGridForNew()
                End If
            Case "Cancel"
                dtgEvent.EditItemIndex = -1
                BindGridForNew()
        End Select
    End Sub
    Private Sub dtgEvent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEvent.ItemDataBound
        If e.Item.ItemType <> ListItemType.Header AndAlso e.Item.ItemType <> ListItemType.Footer Then
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgEvent.PageSize * dtgEvent.CurrentPageIndex)).ToString
            If e.Item.ItemType = ListItemType.EditItem Then
                Dim ddlDealerGroupFooter As DropDownList = e.Item.FindControl("ddlDealerGroupFooter")
                Dim ddlCategoryItem As DropDownList = e.Item.FindControl("ddlCategoryItem")
                Dim ddlVehicleTypeItem As DropDownList = e.Item.FindControl("ddlVehicleTypeItem")
                ddlCategoryItem.DataSource = objFacadeCategory.RetrieveActiveList
                ddlCategoryItem.DataTextField = "Description"
                ddlCategoryItem.DataValueField = "ID"
                ddlCategoryItem.DataBind()
                ddlCategoryItem.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                ddlVehicleTypeItem.Items.Clear()
                Dim laporan As EventLaporanPenjualan = DirectCast(e.Item.DataItem, EventLaporanPenjualan)
                If e.Item.DataSetIndex > -1 Then
                    ddlCategoryItem.SelectedValue = laporan.VechileType.Category.ID
                    ddlVehicleTypeItem.DataSource = GetVehicleType(ddlCategoryItem.SelectedValue)
                    ddlVehicleTypeItem.DataTextField = "Description"
                    ddlVehicleTypeItem.DataValueField = "ID"
                    ddlVehicleTypeItem.DataBind()
                    ddlVehicleTypeItem.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                    ddlvehicletypeitem.SelectedValue = laporan.VechileType.ID
                Else
                    ddlVehicleTypeItem.Items.Insert(0, New ListItem("Pilih Kategori", -1))
                End If
            Else
                Dim lnbCarDelete As LinkButton = e.Item.FindControl("lnbCarDelete")
                Dim lblDealerName As Label = e.Item.FindControl("lblDealerName")
                Dim lblDealerCode As Label = e.Item.FindControl("lblDealerCode")
                If e.Item.DataItem.GetType Is GetType(EventLaporanPenjualan) Then
                    lblDealerCode.Text = DirectCast(e.Item.DataItem, EventLaporanPenjualan).Dealer.DealerCode
                    lblDealerName.Text = DirectCast(e.Item.DataItem, EventLaporanPenjualan).Dealer.DealerName
                Else
                    lblDealerName.Text = DirectCast(e.Item.DataItem, V_EventLaporanPenjualanGroupDealer).Dealer.GroupName
                End If
                lnbCarDelete.Attributes.Add("onclick", String.Format("return confirm('{0}');", SR.DeleteConfirmation))
            End If
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Dim ddlCategoryItem As DropDownList = e.Item.FindControl("ddlCategoryFooter")
            Dim ddlVehicleTypeItem As DropDownList = e.Item.FindControl("ddlVehicleTypeFooter")
            ddlCategoryItem.DataSource = objFacadeCategory.RetrieveActiveList
            ddlCategoryItem.DataTextField = "Description"
            ddlCategoryItem.DataValueField = "ID"
            ddlCategoryItem.DataBind()
            ddlCategoryItem.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            ddlVehicleTypeItem.Items.Insert(0, New ListItem("Pilih Model", -1))
        End If
    End Sub
    Public Sub ddlCarModel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlCategory As DropDownList = sender
        Dim gridItem As DataGridItem = ddlCategory.Parent.Parent
        Dim ddlVehicleType As DropDownList
        If gridItem.DataSetIndex > -1 Then
            ddlVehicleType = gridItem.FindControl("ddlVehicleTypeItem")
        Else
            ddlVehicleType = gridItem.FindControl("ddlVehicleTypeFooter")
        End If
        If ddlCategory.SelectedIndex > 0 Then
            ddlVehicleType.DataSource = GetVehicleType(ddlCategory.SelectedValue)
            ddlVehicleType.DataTextField = "Description"
            ddlVehicleType.DataValueField = "ID"
            ddlVehicleType.DataBind()
            ddlVehicleType.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlVehicleType.Items.Insert(0, New ListItem("Pilih Model", -1))
        End If
    End Sub
    Private Sub dtgEvent_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEvent.PageIndexChanged
        dtgEvent.CurrentPageIndex = e.NewPageIndex
        GridDataSource = RetrieveDataBaseOnCrit(0)
        BindGridForSearch()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ChangeLayout(enumMode.Mode.ViewMode)
        BindGridForSearch()
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If IsValidSave() Then
            Dim objFacade As New EventLaporanPenjualanFacade(User)
            If objFacade.Insert(GridDataSource) <> -1 Then
                MessageBox.Show(SR.SaveSuccess)
                ChangeLayout(enumMode.Mode.ViewMode)
                BindGridForSearch()
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If
    End Sub
    Private Sub cbPeriode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPeriode.CheckedChanged
        calDari.Enabled = cbPeriode.Checked
        calSampai.Enabled = cbPeriode.Checked
    End Sub
    Private Sub cbEventDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEventDate.CheckedChanged
        calEventDate.Enabled = cbEventDate.Checked
    End Sub
    Private Sub dtgEvent_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEvent.SortCommand
        If GridSortColumn = e.SortExpression Then
            Select Case GridSortDirection
                Case Sort.SortDirection.ASC
                    GridSortDirection = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    GridSortDirection = Sort.SortDirection.ASC
            End Select
        Else
            GridSortColumn = e.SortExpression
            GridSortDirection = Sort.SortDirection.ASC
        End If
        GridDataSource = RetrieveDataBaseOnCrit(dtgEvent.VirtualItemCount)
        BindGridForSearch()
    End Sub
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim objArray As New ArrayList
        Dim count As Int32 = 0
        Dim ds As ArrayList = GridDataSource
        For Each item As DataGridItem In dtgEvent.Items
            Dim chkSelect As CheckBox = item.FindControl("chkSelect")
            If chkSelect.Checked Then
                objArray.Add(ds(item.DataSetIndex))
                count += 1
            End If
        Next
        If count > 0 Then
            sesHelper.SetSession("EventSellingReportSourceExcel", objArray)
            Response.Redirect(String.Format("FrmEventLaporanPenjualanExcel.aspx?EventParameterId={0}&Type={1}", _
                EventParameterID, IIf(rbDealer.Checked, "dealer", "group")))
        Else
            MessageBox.Show(SR.DataNotChooseYet("Event Laporan"))
        End If
    End Sub
#End Region

End Class