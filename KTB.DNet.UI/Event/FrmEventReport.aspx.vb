Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.WebCC

Imports System.IO
Imports System.Text

Public Class FrmEventReport
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSalesmanArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbPeriode As System.Web.UI.WebControls.CheckBox
    Protected WithEvents calDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlJenisKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlModel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlNamaKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEvent As System.Web.UI.WebControls.DataGrid
    Protected WithEvents tblCategoryModelType As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents dtmEvent As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents rbDealerCode As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbDealerGroup As System.Web.UI.WebControls.RadioButton
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchGroupDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtGroupDealer As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private epf As EventProposalFacade
    Private erf As EventReportFacade
    Private objDealer As Dealer
    Private objEp As EventProposal
    Dim _sesshelper As New SessionHelper
    Dim EPS As String = "EventProposalSessionFile"
    Dim DEALER As String = "DEALER"
    Dim ARL_DETAIL As String = "ArlDetail"

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
    Private Sub InitAuthorization()
        Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")
        If Not IsNothing(objUserInfo.Dealer) AndAlso objUserInfo.Dealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            txtDealerCode.Text = objUserInfo.Dealer.DealerCode
            txtDealerCode.Enabled = False
            lblSearchDealer.Enabled = True
            lblSearchDealer.Visible = False
        Else
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblSearchGroupDealer.Attributes("onclick") = "ShowPPDealerGroupSelection();"
        End If
    End Sub
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
    Private Function BuiltCriteria() As CriteriaComposite
        Dim objComposite As New CriteriaComposite(New Criteria(GetType(EventProposal), "RowStatus", _
            CType(DBRowStatus.Active, Short)))
        Dim objUserInfo As UserInfo = (New SessionHelper).GetSession("LOGINUSERINFO")

        If txtDealerCode.Text.Length > 0 Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "Dealer.DealerCode", MatchType.InSet, _
                String.Format("('{0}')", txtDealerCode.Text.Replace(";", "','"))))
        End If
        If ddlSalesmanArea.SelectedValue <> -1 Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.SalesmanArea", _
                ddlSalesmanArea.SelectedValue))
        End If
        If cbPeriode.Checked Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.EventDateStart", _
                MatchType.GreaterOrEqual, calDari.Value))
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.EventDateEnd", _
                MatchType.LesserOrEqual, calSampai.Value))
        End If
        If ddlJenisKegiatan.SelectedValue <> -1 Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.ActivityType", _
                ddlJenisKegiatan.SelectedValue))
        End If
        If ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Small_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Launching_Gathering, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Exhibition_Khusus, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Showroom_Event, Integer) _
            OrElse ddlJenisKegiatan.SelectedValue = CType(EnumActivityType.ActivityType.Others, Integer) Then
            If ddlCategory.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.Category", ddlCategory.SelectedValue))
            End If
            If ddlModel.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.VechileType.VechileModel", _
                    ddlModel.SelectedValue))
            End If
            If ddlType.SelectedValue <> -1 Then
                objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.VechileType", _
                    ddlType.SelectedValue))
            End If
        End If
        If ddlNamaKegiatan.SelectedIndex > 0 Then
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.EventName", _
                ddlNamaKegiatan.SelectedItem.Text))
            objComposite.opAnd(New Criteria(GetType(EventProposal), "EventParameter.EventYear", _
                ddlYear.SelectedValue))
        End If
        Return objComposite
    End Function
    Private Sub BindGrid()
        Dim arl As ArrayList = getArlDetailSession()
        dtgEvent.DataSource = arl
        dtgEvent.DataBind()
    End Sub

    Private Sub proposalEventDataToUI()
        If (IsNothing(Request.QueryString("id"))) Then
            MessageBox.Show("DEV ERR: id query string for EventProposalID is null")
            Return
        End If

        objEp = epf.Retrieve(CInt(Request.QueryString("id")))
        If (IsNothing(objEp)) Then
            MessageBox.Show("DEV ERR: EventProposal is null for ID=" & Request.QueryString("id"))
            Return
        End If

        _sesshelper.SetSession(EPS, objEp)
        txtDealerCode.Text = objEp.Dealer.DealerCode
        txtGroupDealer.Text = objEp.Dealer.DealerGroup.GroupName
        ddlJenisKegiatan.SelectedValue = objEp.ActivityType.ID
        FillNamaKegiatan()
        ddlNamaKegiatan.SelectedValue = objEp.EventParameter.EventName
        If Not IsNothing(objEp.EventParameter.SalesmanArea) Then
            ddlSalesmanArea.SelectedValue = objEp.EventParameter.SalesmanArea.ID
        End If
        calDari.Value = objEp.EventParameter.EventDateStart
        calSampai.Value = objEp.EventParameter.EventDateEnd
        dtmEvent.Value = objEp.ActivitySchedule
        Dim arl As ArrayList = erf.RetrieveByEventProposalId(CInt(Request.QueryString("id")))
        _sesshelper.SetSession(ARL_DETAIL, arl)
        BindGrid()
    End Sub

    Private Sub bindCarCategory(ByVal ddl As DropDownList)
        Dim cf As CategoryFacade = New CategoryFacade(User)
        Dim arl As ArrayList = cf.RetrieveList()
        ddl.DataSource = arl
        ddl.DataTextField = "Description"
        ddl.DataValueField = "ID"
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silahkan pilih", "-1"))
    End Sub

    Private Sub bindCarType(ByVal ddl As DropDownList, ByVal intCategoryID As Integer)
        Dim cf As VechileTypeFacade = New VechileTypeFacade(User)
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CInt(DBRowStatus.Active)))
        crits.opAnd(New Criteria(GetType(VechileType), "Category", MatchType.Exact, intCategoryID))
        Dim arl As ArrayList = cf.Retrieve(crits)
        ddl.DataSource = arl
        ddl.DataTextField = "Description"
        ddl.DataValueField = "ID"
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silahkan pilih", "-1"))
    End Sub

    Private Function getArlDetailSession() As ArrayList
        If (IsNothing(_sesshelper.GetSession(ARL_DETAIL))) Then
            Return New ArrayList
        Else
            Dim arl As ArrayList = CType(_sesshelper.GetSession(ARL_DETAIL), ArrayList)
            _sesshelper.SetSession(ARL_DETAIL, arl)
            Return arl
        End If
    End Function

    Private Function addArlDetailSession(ByVal obj As EventReport)
        Dim arl As ArrayList = getArlDetailSession()
        arl.Add(obj)
        _sesshelper.SetSession(ARL_DETAIL, arl)
    End Function

    Private Function editArlDetailSession(ByVal indeks As Integer, ByVal obj As EventReport)
        Dim arl As ArrayList = getArlDetailSession()
        Dim objArl As EventReport = arl(indeks)
        objArl.VechileType.ID = obj.VechileType.ID
        objArl.EventProposal.ID = obj.EventProposal.ID
        objArl.Description = obj.Description
        objArl.Jumlah = obj.Jumlah
        If (obj.ID > 0) Then
            erf.Update(objArl)
        End If
        _sesshelper.SetSession(ARL_DETAIL, arl)
    End Function

    Private Function deleteArlDetailSession(ByVal indeks As Integer, ByVal cmdArgs As String)
        Dim arl As ArrayList = getArlDetailSession()
        If (CInt(cmdArgs) > 0) Then
            Dim objArl As EventReport = erf.Retrieve(CInt(cmdArgs))
            erf.Delete(objArl)
        End If
        arl.RemoveAt(indeks)
        _sesshelper.SetSession(ARL_DETAIL, arl)
    End Function

#End Region

    Private Sub EnableControl(ByVal IsEnable As Boolean)
        rbDealerCode.Enabled = IsEnable
        txtDealerCode.Enabled = IsEnable
        lblSearchDealer.Visible = IsEnable
        ddlSalesmanArea.Enabled = IsEnable
        rbDealerGroup.Enabled = IsEnable
        txtGroupDealer.Enabled = IsEnable
        lblSearchGroupDealer.Visible = IsEnable
        dtmEvent.Enabled = IsEnable
        cbPeriode.Enabled = IsEnable
        calDari.Enabled = IsEnable
        calSampai.Enabled = IsEnable
        ddlJenisKegiatan.Enabled = IsEnable
        ddlNamaKegiatan.Enabled = IsEnable
        ddlYear.Enabled = IsEnable
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        epf = New EventProposalFacade(User)
        erf = New EventReportFacade(User)
        objDealer = _sesshelper.GetSession("DEALER")
        objEp = epf.Retrieve(CInt(Request.QueryString("id")))
        If (IsNothing(objEp)) Then
            MessageBox.Show("DEV ERR: EventProposal is null for ID=" & Request.QueryString("id"))
            Return
        End If

        If Not IsNothing(Request.QueryString("act")) AndAlso _
            Request.QueryString("act") = "1" Then
            EnableControl(False)
            btnCari.Visible = False
        Else
            EnableControl(False)
            btnCari.Visible = True
        End If

        If Not IsPostBack Then
            InitAuthorization()
            FillSalesmanArea()
            FillJenisKegiatan()
            FillNamaKegiatan()
            FillYear()
            proposalEventDataToUI()
        End If
    End Sub

    Private Sub ddlJenisKegiatan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlJenisKegiatan.SelectedIndexChanged
        FillNamaKegiatan()
    End Sub
    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        FillModel(ddlCategory.SelectedValue)
        FillType(ddlModel.SelectedValue)
    End Sub
    Private Sub ddlModel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlModel.SelectedIndexChanged
        FillType(ddlModel.SelectedValue)
    End Sub

    Private Sub dtgEvent_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEvent.ItemCommand
        If (e.CommandName = "Add") Then
            Dim ddlfVechileType As DropDownList = CType(e.Item.FindControl("ddlfVechileType"), DropDownList)
            Dim txtfDesc As TextBox = CType(e.Item.FindControl("txtfDesc"), TextBox)
            Dim txtfQty As TextBox = CType(e.Item.FindControl("txtfQty"), TextBox)
            If (ddlfVechileType.SelectedValue = "" Or ddlfVechileType.SelectedValue = "-1") Then
                MessageBox.Show("Type kendaraan belum dipilih")
                Return
            End If
            If (txtfQty.Text = "0") Then
                MessageBox.Show("Jumlah belum disi")
                Return
            End If
            Dim obj As EventReport = New EventReport
            obj.EventProposal = objEp
            obj.VechileType = New VechileTypeFacade(User).Retrieve(CInt(ddlfVechileType.SelectedValue))
            obj.Description = txtfDesc.Text
            obj.Jumlah = txtfQty.Text
            addArlDetailSession(obj)
            BindGrid()
        ElseIf (e.CommandName = "Edit") Then
            dtgEvent.EditItemIndex = e.Item.ItemIndex
            BindGrid()
        ElseIf (e.CommandName = "Save") Then
            Dim ddleVechileType As DropDownList = CType(e.Item.FindControl("ddleVechileType"), DropDownList)
            Dim txteDesc As TextBox = CType(e.Item.FindControl("txteDesc"), TextBox)
            Dim txteQty As TextBox = CType(e.Item.FindControl("txteQty"), TextBox)
            If (ddleVechileType.SelectedValue = -1 Or ddleVechileType.SelectedValue = "") Then
                MessageBox.Show("Type kendaraan belum dipilih")
                Return
            End If
            If (txteQty.Text = "0") Then
                MessageBox.Show("Jumlah belum disi")
                Return
            End If
            Dim obj As EventReport = New EventReport
            obj.ID = CInt(e.CommandArgument)
            obj.EventProposal = objEp
            obj.VechileType = New VechileTypeFacade(User).Retrieve(CInt(ddleVechileType.SelectedValue))
            obj.Description = txteDesc.Text
            obj.Jumlah = txteQty.Text
            editArlDetailSession(e.Item.ItemIndex, obj)
            dtgEvent.EditItemIndex = -1
            BindGrid()
        ElseIf (e.CommandName = "Cancel") Then
            dtgEvent.EditItemIndex = -1
            BindGrid()
        ElseIf (e.CommandName = "Delete") Then
            deleteArlDetailSession(e.Item.ItemIndex, e.CommandArgument)
            BindGrid()
        End If
    End Sub

    Private Sub dtgEvent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEvent.ItemDataBound
        If (e.Item.ItemType = ListItemType.Footer) Then
            Dim ddlfVechileCategory As DropDownList = CType(e.Item.FindControl("ddlfVechileCategory"), DropDownList)
            If ddlfVechileCategory.Items.Count <= 0 Then
                bindCarCategory(ddlfVechileCategory)
            End If
        ElseIf (e.Item.ItemType = ListItemType.EditItem) Then
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgEvent.PageSize * dtgEvent.CurrentPageIndex)).ToString
            Dim arl As ArrayList = CType(_sesshelper.GetSession(ARL_DETAIL), ArrayList)
            Dim obj As EventReport = CType(arl(e.Item.ItemIndex), EventReport)
            Dim ddleVechileCategory As DropDownList = CType(e.Item.FindControl("ddleVechileCategory"), DropDownList)
            bindCarCategory(ddleVechileCategory)
            ddleVechileCategory.SelectedValue = obj.VechileType.Category.ID
            Dim ddleVechileType As DropDownList = CType(e.Item.FindControl("ddleVechileType"), DropDownList)
            If (ddleVechileCategory.SelectedValue <> "") Then
                bindCarType(ddleVechileType, CInt(ddleVechileCategory.SelectedValue))
                ddleVechileType.SelectedValue = obj.VechileType.ID
            End If
        ElseIf (e.Item.ItemType = ListItemType.Item) Then
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgEvent.PageSize * dtgEvent.CurrentPageIndex)).ToString
        ElseIf (e.Item.ItemType = ListItemType.AlternatingItem) Then
            e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dtgEvent.PageSize * dtgEvent.CurrentPageIndex)).ToString
        End If
        'sory ri, editnya nih yg masih error, tolong ya ...
    End Sub

    Protected Sub ddlCarCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ddlCarCategory As DropDownList = CType(sender, DropDownList)
        Dim cell As TableCell = CType(ddlCarCategory.Parent, TableCell)
        Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
        If (item.ItemType = ListItemType.Footer) Then
            Dim ddlfVechileType As DropDownList = CType(item.FindControl("ddlfVechileType"), DropDownList)
            bindCarType(ddlfVechileType, CInt(ddlCarCategory.SelectedValue))
        ElseIf (item.ItemType = ListItemType.EditItem) Then
            Dim ddleVechileType As DropDownList = CType(item.FindControl("ddleVechileType"), DropDownList)
            bindCarType(ddleVechileType, CInt(ddlCarCategory.SelectedValue))
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim arl As ArrayList = getArlDetailSession()
        If (arl.Count <= 0) Then
            MessageBox.Show("Data belum dipilih")
            Return
        End If

        Dim i As Integer = 0
        For Each obj As EventReport In arl
            If (obj.ID > 0) Then
                If (erf.Update(obj) <> -1) Then
                    i += 1
                Else
                    MessageBox.Show("Data gagal disimpan")
                    Return
                End If
            Else
                If (erf.Insert(obj) <> -1) Then
                    i += 1
                Else
                    MessageBox.Show("Data gagal disimpan")
                    Return
                End If
            End If
        Next
        If (i >= arl.Count) Then
            MessageBox.Show("Data berhasil disimpan")
        Else
            MessageBox.Show("Data gagal disimpan")
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click

    End Sub

End Class
