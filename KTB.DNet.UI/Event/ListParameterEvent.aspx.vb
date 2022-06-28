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

Public Class ListParameterEvent
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlSalesmanArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbPeriode As System.Web.UI.WebControls.CheckBox
    Protected WithEvents calDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlJenisKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlModel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEvent As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSend As System.Web.UI.WebControls.Button
    Protected WithEvents tblCategoryModelType As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ddlStatusEvent As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlNamaKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchJenisKegiatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchNamaKegiatan As System.Web.UI.WebControls.Label
    Protected WithEvents trSearchJenisKegiatan As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trSearchNamaKegiatan As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trStatus As System.Web.UI.HtmlControls.HtmlTableRow

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"

#End Region

#Region " Private Variables"
    Private DealerCollection As ArrayList
    Private Property StatusEventSearch() As EnumEventParameter.StatusEventParameter
        Get
            Return ViewState("EventStatus")
        End Get
        Set(ByVal Value As EnumEventParameter.StatusEventParameter)
            ViewState("EventStatus") = Value
        End Set
    End Property
#End Region

#Region "Custom Method"
    Private Property StoreCriteria() As CriteriaComposite
        Get
            Return (New SessionHelper).GetSession("StorePageCriteria")
        End Get
        Set(ByVal Value As CriteriaComposite)
            Dim sHelper As New SessionHelper
            sHelper.SetSession("StorePageCriteria", Value)
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
            ddlNamaKegiatan.DataSource = objFacade.RetrieveNamaKegiatan(ddlJenisKegiatan.SelectedValue)
            ddlNamaKegiatan.DataTextField = "EventName"
            ddlNamaKegiatan.DataValueField = "EventName"
            ddlNamaKegiatan.DataBind()
            ddlNamaKegiatan.Items.Insert(0, "Silahkan Pilih")
        Else
            ddlNamaKegiatan.Items.Insert(0, "Pilih Jenis Kegiatan")
        End If
    End Sub
    Private Sub FillStatus()
        ddlStatusEvent.DataSource = EnumEventParameter.RetrieveStatus
        ddlStatusEvent.DataTextField = "Name"
        ddlStatusEvent.DataValueField = "ID"
        ddlStatusEvent.DataBind()
        ddlStatusEvent.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub
    Private Sub InitAuthorization()
        Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")
        If Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            ddlStatusEvent.SelectedValue = CType(EnumEventParameter.StatusEventParameter.Kirim, Short)
            trStatus.Visible = False
            txtDealerCode.Text = objUserInfo.Dealer.DealerCode
            txtDealerCode.Enabled = False
            lblSearchDealer.Enabled = True
            dtgEvent.Columns(3 + 2).Visible = False
            dtgEvent.Columns(4 + 2).Visible = False
            lblSearchDealer.Visible = False
            btnSend.Visible = False
        Else
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        End If
    End Sub
    Private Property GridSortColumn() As String
        Get
            If ViewState("SortColumn") Is Nothing Then
                ViewState("SortColumn") = "ID"
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
    Private Function BuiltCriteria() As CriteriaComposite
        Dim objComposite As New CriteriaComposite(New Criteria(GetType(EventParameter), "RowStatus", _
            CType(DBRowStatus.Active, Short)))
        If txtDealerCode.Text.Length > 0 Then
            objComposite.opAnd(New Criteria(GetType(EventParameter), "Dealer.DealerCode", MatchType.InSet, _
                String.Format("('{0}')", txtDealerCode.Text.Replace(";", "','"))))
        End If
        If ddlSalesmanArea.SelectedValue <> -1 Then
            objComposite.opAnd(New Criteria(GetType(EventParameter), "SalesmanArea", _
                ddlSalesmanArea.SelectedValue))
        End If
        If cbPeriode.Checked Then
            objComposite.opAnd(New Criteria(GetType(EventParameter), "EventDateStart", _
                MatchType.GreaterOrEqual, calDari.Value))
            objComposite.opAnd(New Criteria(GetType(EventParameter), "EventDateEnd", _
                MatchType.LesserOrEqual, calSampai.Value))
        End If
        If ddlJenisKegiatan.SelectedValue <> -1 Then
            objComposite.opAnd(New Criteria(GetType(EventParameter), "ActivityType", _
                ddlJenisKegiatan.SelectedValue))
        End If
        If ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Small_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Launching_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Exhibition_Khusus, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Showroom_Event, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Others, Integer) Then
            If ddlCategory.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventParameter), "Category", ddlCategory.SelectedValue))
            End If
            If ddlModel.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventParameter), "VechileType.VechileModel", _
                    ddlModel.SelectedValue))
            End If
            If ddlType.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventParameter), "VechileType", ddlType.SelectedValue))
            End If
        End If
        If ddlNamaKegiatan.SelectedIndex > 0 Then
            objComposite.opAnd(New Criteria(GetType(EventParameter), "EventName", _
                ddlNamaKegiatan.SelectedItem.Text))
            objComposite.opAnd(New Criteria(GetType(EventParameter), "EventYear", ddlYear.SelectedValue))
        End If

        If (ddlStatusEvent.SelectedValue <> "-1") Then
            objComposite.opAnd(New Criteria(GetType(EventParameter), "EventStatus", CType(ddlStatusEvent.SelectedValue, Short)))
        End If

        Return objComposite
    End Function
    Private Sub DisplaySearchCaption()
        If ddlJenisKegiatan.SelectedValue <> -1 Then
            trSearchJenisKegiatan.Visible = True
            lblSearchJenisKegiatan.Text = ddlJenisKegiatan.SelectedItem.Text
        Else
            trSearchJenisKegiatan.Visible = False
        End If
        If ddlNamaKegiatan.SelectedIndex > 0 Then
            trSearchNamaKegiatan.Visible = True
            lblSearchNamaKegiatan.Text = ddlNamaKegiatan.SelectedItem.Text
        Else
            trSearchNamaKegiatan.Visible = False
        End If
    End Sub
    Private Sub BindGrid()
        Dim objFacade As New EventParameterFacade(User)
        Dim totalRows As Integer = 0
        dtgEvent.DataSource = objFacade.RetrieveActiveList(StoreCriteria, dtgEvent.CurrentPageIndex + 1, dtgEvent.PageSize, _
            totalRows, GridSortColumn, GridSortDirection)
        dtgEvent.VirtualItemCount = totalRows
        dtgEvent.DataBind()
        If totalRows = 0 Then
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub
    Private Function IsFindValid() As Boolean
        Dim IsValid As Boolean = True
        If calDari.Value > calSampai.Value Then
            MessageBox.Show(SR.InvalidRangeDate)
            IsValid = False
        End If
        Return IsValid
    End Function
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            trSearchJenisKegiatan.Visible = False
            trSearchNamaKegiatan.Visible = False
            InitAuthorization()
            FillSalesmanArea()
            FillJenisKegiatan()
            FillNamaKegiatan()
            ShowModelCategoryType(False)
            FillCategory()
            ddlCategory_SelectedIndexChanged(Nothing, Nothing)
            ddlModel_SelectedIndexChanged(Nothing, Nothing)
            FillYear()
            FillStatus()
        End If
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
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If IsFindValid() Then
            dtgEvent.CurrentPageIndex = 0
            DisplaySearchCaption()
            If (ddlStatusEvent.SelectedValue <> "-1") Then
                StatusEventSearch = ddlStatusEvent.SelectedValue
                'btnSend.Visible = ddlStatusEvent.SelectedValue = CType(EnumEventInfo.EventInfoStatus.Baru, Integer)
                'Else
                'btnSend.Visible = True
            End If
            StoreCriteria = BuiltCriteria()
            BindGrid()
        End If
    End Sub
    Private Sub dtgEvent_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEvent.SortCommand
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
        BindGrid()
    End Sub
    Private Sub dtgEvent_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEvent.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
            Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgEvent.PageSize * dtgEvent.CurrentPageIndex)).ToString
            lbtnDelete.Visible = StatusEventSearch = EnumEventParameter.StatusEventParameter.Baru _
                AndAlso txtDealerCode.Enabled
            lbtnEdit.Visible = StatusEventSearch = EnumEventParameter.StatusEventParameter.Baru _
                AndAlso txtDealerCode.Enabled
            lbtnDelete.Attributes.Add("onclick", String.Format("return confirm('{0}');", SR.DeleteConfirmation))
        End If
    End Sub
    Private Sub dtgEvent_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEvent.ItemCommand
        Select Case e.CommandName
            Case "FileDownload"
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                Dim fileTarget As New FileInfo(String.Format("{0}{1}\{2}", KTB.DNet.Lib.WebConfig.GetValue("SAN"), _
                         KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), e.CommandArgument))
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                If imp.Start() Then
                    Response.Redirect(String.Format("~/download.aspx?file={0}", fileTarget.FullName))
                End If
                imp.StopImpersonate()
                imp = Nothing
            Case "Edit"
                Response.Redirect(String.Format("FrmParameterEvent.aspx?ID={0}", e.CommandArgument))
            Case "Delete"
                Dim objFacade As New EventParameterFacade(User)
                Dim chkSelect As CheckBox = e.Item.FindControl("chkSelect")
                Dim objEvent As EventParameter = objFacade.Retrieve(CInt(e.CommandArgument))
                If chkSelect.Checked Then
                    If objFacade.Delete(objEvent) = -1 Then
                        MessageBox.Show("Hapus Gagal")
                    Else
                        MessageBox.Show("Hapus Berhasil")
                        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                        Dim _SAN As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")
                        Dim _EventDestFileDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory")
                         
                        Dim fileMaterialTarget As New FileInfo(String.Format("{0}{1}\{2}\{3}", _
                          _SAN, _
                          _EventDestFileDirectory, _
                            objEvent.DirTarget, objEvent.FileNameMaterial))
                        Dim fileJuklakTarget As New FileInfo(String.Format("{0}{1}\{2}\{3}", _
                          _SAN, _
                          _EventDestFileDirectory, _
                            objEvent.DirTarget, objEvent.FileNameJuklak))
                        Dim filePendukungTarget1 As New FileInfo(String.Format("{0}{1}\{2}\{3}", _
                          _SAN, _
                          _EventDestFileDirectory, _
                            objEvent.DirTarget, objEvent.FileNamePendukung1))
                        Dim filePendukungTarget2 As New FileInfo(String.Format("{0}{1}\{2}\{3}", _
                          _SAN, _
                          _EventDestFileDirectory, _
                            objEvent.DirTarget, objEvent.FileNamePendukung2))
                        Dim filePendukungTarget3 As New FileInfo(String.Format("{0}{1}\{2}\{3}", _
                          _SAN, _
                          _EventDestFileDirectory, _
                            objEvent.DirTarget, objEvent.FileNamePendukung3))
                        Dim filePendukungTarget4 As New FileInfo(String.Format("{0}{1}\{2}\{3}", _
                          _SAN, _
                          _EventDestFileDirectory, _
                            objEvent.DirTarget, objEvent.FileNamePendukung4))
                        Dim filePendukungTarget5 As New FileInfo(String.Format("{0}{1}\{2}\{3}", _
                          _SAN, _
                          _EventDestFileDirectory, _
                            objEvent.DirTarget, objEvent.FileNamePendukung5))
                        Dim filePendukungTarget6 As New FileInfo(String.Format("{0}{1}\{2}\{3}", _
                          _SAN, _
                          _EventDestFileDirectory, _
                            objEvent.DirTarget, objEvent.FileNamePendukung6))
                        Dim filePendukungTarget7 As New FileInfo(String.Format("{0}{1}\{2}\{3}", _
                          _SAN, _
                          _EventDestFileDirectory, _
                            objEvent.DirTarget, objEvent.FileNamePendukung7))
                        Dim filePendukungTarget8 As New FileInfo(String.Format("{0}{1}\{2}\{3}", _
                          _SAN, _
                          _EventDestFileDirectory, _
                            objEvent.DirTarget, objEvent.FileNamePendukung8))
                        Dim filePendukungTarget9 As New FileInfo(String.Format("{0}{1}\{2}\{3}", _
                          _SAN, _
                          _EventDestFileDirectory, _
                            objEvent.DirTarget, objEvent.FileNamePendukung9))
                        Dim filePendukungTarget10 As New FileInfo(String.Format("{0}{1}\{2}\{3}", _
                          _SAN, _
                          _EventDestFileDirectory, _
                            objEvent.DirTarget, objEvent.FileNamePendukung10))
                        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                        If imp.Start() Then

                            If fileMaterialTarget.Exists Then
                                fileMaterialTarget.Delete()
                            End If

                            If fileJuklakTarget.Exists Then
                                fileJuklakTarget.Delete()
                            End If

                            If filePendukungTarget1.Exists Then
                                filePendukungTarget1.Delete()
                            End If

                            If filePendukungTarget2.Exists Then
                                filePendukungTarget2.Delete()
                            End If
                            If filePendukungTarget3.Exists Then
                                filePendukungTarget3.Delete()
                            End If
                            If filePendukungTarget4.Exists Then
                                filePendukungTarget4.Delete()
                            End If
                            If filePendukungTarget5.Exists Then
                                filePendukungTarget5.Delete()
                            End If
                            If filePendukungTarget6.Exists Then
                                filePendukungTarget6.Delete()
                            End If
                            If filePendukungTarget7.Exists Then
                                filePendukungTarget7.Delete()
                            End If
                            If filePendukungTarget8.Exists Then
                                filePendukungTarget8.Delete()
                            End If
                            If filePendukungTarget9.Exists Then
                                filePendukungTarget9.Delete()
                            End If
                            If filePendukungTarget10.Exists Then
                                filePendukungTarget10.Delete()
                            End If
                             
                            If fileMaterialTarget.Directory.Exists AndAlso fileMaterialTarget.Directory.GetFiles.Length = 0 Then
                                fileMaterialTarget.Directory.Delete()
                            End If
                            fileJuklakTarget.Refresh()
                            If fileJuklakTarget.Directory.Exists AndAlso fileJuklakTarget.Directory.GetFiles.Length = 0 Then
                                fileJuklakTarget.Directory.Delete()
                            End If
                            filePendukungTarget1.Refresh()
                            If filePendukungTarget1.Directory.Exists AndAlso filePendukungTarget1.Directory.GetFiles.Length = 0 Then
                                filePendukungTarget1.Directory.Delete()
                            End If
                            filePendukungTarget2.Refresh()
                            If filePendukungTarget2.Directory.Exists AndAlso filePendukungTarget2.Directory.GetFiles.Length = 0 Then
                                filePendukungTarget2.Directory.Delete()
                            End If
                            filePendukungTarget3.Refresh()
                            If filePendukungTarget3.Directory.Exists AndAlso filePendukungTarget3.Directory.GetFiles.Length = 0 Then
                                filePendukungTarget3.Directory.Delete()
                            End If
                            filePendukungTarget4.Refresh()
                            If filePendukungTarget4.Directory.Exists AndAlso filePendukungTarget4.Directory.GetFiles.Length = 0 Then
                                filePendukungTarget4.Directory.Delete()
                            End If
                            filePendukungTarget5.Refresh()
                            If filePendukungTarget5.Directory.Exists AndAlso filePendukungTarget5.Directory.GetFiles.Length = 0 Then
                                filePendukungTarget5.Directory.Delete()
                            End If
                            filePendukungTarget6.Refresh()
                            If filePendukungTarget6.Directory.Exists AndAlso filePendukungTarget6.Directory.GetFiles.Length = 0 Then
                                filePendukungTarget6.Directory.Delete()
                            End If
                            filePendukungTarget7.Refresh()
                            If filePendukungTarget7.Directory.Exists AndAlso filePendukungTarget7.Directory.GetFiles.Length = 0 Then
                                filePendukungTarget7.Directory.Delete()
                            End If
                            filePendukungTarget8.Refresh()
                            If filePendukungTarget8.Directory.Exists AndAlso filePendukungTarget8.Directory.GetFiles.Length = 0 Then
                                filePendukungTarget8.Directory.Delete()
                            End If
                            filePendukungTarget9.Refresh()
                            If filePendukungTarget9.Directory.Exists AndAlso filePendukungTarget9.Directory.GetFiles.Length = 0 Then
                                filePendukungTarget9.Directory.Delete()
                            End If
                            filePendukungTarget10.Refresh()
                            If filePendukungTarget10.Directory.Exists AndAlso filePendukungTarget10.Directory.GetFiles.Length = 0 Then
                                filePendukungTarget10.Directory.Delete()
                            End If
                        End If
                        imp.StopImpersonate()
                        imp = Nothing
                        BindGrid()
                    End If
                Else
                    MessageBox.Show("Tidak ada item yang dipilih")
                End If
        End Select
    End Sub
    Private Sub cbPeriode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbPeriode.CheckedChanged
        calDari.Enabled = cbPeriode.Checked
        calSampai.Enabled = cbPeriode.Checked
    End Sub
    Private Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim objFacade As New EventParameterFacade(User)
        Dim objProcessesEvent As New ArrayList
        For Each item As DataGridItem In dtgEvent.Items
            Dim chkSelect As CheckBox = item.FindControl("chkSelect")
            If chkSelect.Checked Then
                objProcessesEvent.Add(objFacade.Retrieve(Integer.Parse(item.Cells(0).Text.Trim)))
            End If
        Next
        If objProcessesEvent.Count > 0 Then
            For Each eventParam As EventParameter In objProcessesEvent
                eventParam.EventStatus = CType(EnumEventParameter.StatusEventParameter.Kirim, Integer)
            Next
            If objFacade.Update(objProcessesEvent) = objProcessesEvent.Count Then
                MessageBox.Show("Kirim berhasil")
            Else
                MessageBox.Show("Kirim gagal")
            End If
            BindGrid()
        Else
            MessageBox.Show("Tidak ada event yang dikirim")
        End If
    End Sub
    Private Sub dtgEvent_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEvent.PageIndexChanged
        dtgEvent.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub
#End Region

End Class