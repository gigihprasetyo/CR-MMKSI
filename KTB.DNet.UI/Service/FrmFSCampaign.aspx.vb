#Region " Summary "
'--------------------------------------------------'
'-- Program Code : FrmFSCampaign.aspx            --'
'-- Program Name : Daftar Parameter Free Service --'
'-- Description  :                               --'
'--------------------------------------------------'
'-- Programmer   : Anna Nurhayanto               --'
'-- Start Date   : Aug 27, 2010                  --'
'-- Update By    :                               --'
'-- Last Update  : Aug 27, 2010                  --'
'--------------------------------------------------'

#End Region

#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.Linq
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmFSCampaign
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    'Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgFSCampaign As System.Web.UI.WebControls.DataGrid
    Protected WithEvents icValidFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icValidTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ictglpktfrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ictglpktto As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMessage As System.Web.UI.WebControls.TextBox
    Protected WithEvents lboxVehicleType As System.Web.UI.WebControls.ListBox
    Protected WithEvents lboxFSType As System.Web.UI.WebControls.ListBox
    Protected WithEvents chkDealer As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkJenisFS As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkType As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkFaktur As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkPktDate As System.Web.UI.WebControls.CheckBox

    Protected WithEvents lboxCategory As System.Web.UI.WebControls.ListBox
    Protected WithEvents btnSerchCode As System.Web.UI.WebControls.Button
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents lblVehicleType As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"
    Private sessHelper As SessionHelper = New SessionHelper
    Private m_bFormPrivilege As Boolean = False
    Private m_bCreatePrivilege As Boolean = False
    Private m_bUpdatePrivilege As Boolean = False
    Private m_bActivatePrivilege As Boolean = False

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateCampaignPrivilege()
        If Not IsPostBack Then
            Initialize()
            BindDataGrid(0)
        End If

    End Sub
    Private Sub ActivateCampaignPrivilege()
        '-- Set user privileges
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.FreeServiceCampaignView_Privilege)
        If Not m_bFormPrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FREE SERVICE - Data Free Service Campaign")
        End If

        If SecurityProvider.Authorize(Context.User, SR.FreeServiceCampaignCreate_Privilege) Then
            btnSimpan.Visible = True
            btnBatal.Visible = True
        Else
            btnSimpan.Visible = False
            btnBatal.Visible = False
        End If

        If SecurityProvider.Authorize(Context.User, SR.FreeServiceCampaignActivate_Privilege) Then
            sessHelper.SetSession("ActivateCampaign", True)
        Else
            sessHelper.SetSession("ActivateCampaign", False)
        End If

        If SecurityProvider.Authorize(Context.User, SR.FreeServiceCampaignUpdate_Privilege) Then
            sessHelper.SetSession("UpdateCampaign", True)
        Else
            sessHelper.SetSession("UpdateCampaign", False)
        End If


        'If Not IsNothing(sessHelp.GetSession("DEALER")) Then
        '    Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)

        '    If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '        '-- As KTB user
        '        If Not SecurityProvider.Authorize(Context.User, SR.AdminViewListOfUserKTB_Privilege) Then
        '            Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Daftar User")
        '        End If
        '        sessHelp.SetSession("UserEditable", SecurityProvider.Authorize(Context.User, SR.AdminUpdateUserKTB_Privilege))
        '        Return

        '    ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '        '-- As ordinal user
        '        If Not SecurityProvider.Authorize(Context.User, SR.AdminViewListOfUserDealer_Privilege) Then
        '            Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Daftar User")
        '        End If
        '        sessHelp.SetSession("UserEditable", SecurityProvider.Authorize(Context.User, SR.AdminUpdateUserDealer_Privilege))
        '        Return

        '    End If
        'End If

    End Sub

    Private Sub Initialize()
        AssignAttributeControl()
        ClearData()
        SetControlPrivilege()
        ViewState("currSortColumn") = "ID"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
    End Sub

    Private Sub AssignAttributeControl()
        Dim lblPopUp As Label = CType(Page.FindControl("lblPopUpDealer"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection")
    End Sub

    Private Sub ClearData()
        lboxCategory.Items.Clear()
        txtKodeDealer.Text = String.Empty
        chkDealer.Checked = False
        chkJenisFS.Checked = False
        chkPktDate.Checked = False
        chkType.Checked = False
        chkFaktur.Checked = False
        BindVehicleType()
        'BindDdlCategory()
        BindLbFSType()
        BindLbVehicleType(-1)
        icValidFrom.Value = Date.Now
        icValidTo.Value = Date.Now
        txtDescription.Text = String.Empty
        ViewState("vsProcess") = "Insert"
        'btnSimpan.Enabled = False
        lboxCategory.Enabled = True
        'ddlCategory.Enabled = True
    End Sub

    Private Sub BindVehicleType()
        BindVehicleSubCategoryToDDL(lboxCategory)
    End Sub

    Public Sub BindVehicleSubCategoryToDDL(ByRef lboxCategory As ListBox)
        lboxCategory.Items.Clear()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        '-- SubCategoryVehicle criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.No, "X"))  '-- Status still active
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ProductCategory.Code", MatchType.Exact, companyCode))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(SubCategoryVehicle), "Name", Sort.SortDirection.ASC))  '-- Sort by Vechile type code

        '-- Bind ddlSubCategory dropdownlist
        lboxCategory.DataSource = New SubCategoryVehicleFacade(User).Retrieve(criterias, sortColl)
        lboxCategory.DataTextField = "Name"
        lboxCategory.DataValueField = "ID"
        lboxCategory.DataBind()
        'ddlSubCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item
    End Sub

    Private Sub lboxVehicleType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lboxVehicleType.SelectedIndexChanged
        Dim val As String
        lblVehicleType.Text = ""
        Dim arr As ArrayList = New ArrayList
        For Each Vt As Integer In lboxVehicleType.GetSelectedIndices()
            arr.Add(Vt)
            If lblVehicleType.Text.Trim = "" Then
                lblVehicleType.Text = lboxVehicleType.Items(Vt).Text
                val = lblVehicleType.Text
            Else
                lblVehicleType.Text += ";" & lboxVehicleType.Items(Vt).Text
            End If
        Next
        ScrollToLastIndex(val)
    End Sub

    Private Sub ScrollToLastIndex(val As String)
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "setFocus('" & val & "');", True)
    End Sub

    Protected Sub btnSerchCode_Click(sender As Object, e As EventArgs) Handles btnSerchCode.Click
        SearchVehicleType()
    End Sub

    Private Sub SearchVehicleType()
        lblCategory.Text = ""
        lblVehicleType.Text = ""
        Dim arr As ArrayList = New ArrayList
        For Each Vt As Integer In lboxCategory.GetSelectedIndices()
            'arr.Add(Vt)
            arr.Add(lboxCategory.Items(Vt).Value.ToString())
            If lblCategory.Text.Trim = "" Then
                'lblCategory.Text = GetSubOfExclCVString(Vt)
                lblCategory.Text = lboxCategory.Items(Vt).Text
            Else
                lblCategory.Text += ";" & lboxCategory.Items(Vt).Text
            End If
        Next
        lboxVehicleType.Items.Clear()
        For Each i As Integer In arr
            BindLbVehicleType(i)
        Next
    End Sub

    Private Sub BindLbVehicleType(ByVal val As String)
        If val > -1 Then
            Dim li As ListItem
            Dim sortCol As SortCollection = New SortCollection
            sortCol.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
            Dim objVehicleType As ArrayList = New VechileTypeFacade(User).Retrieve(CriteriaSearch(val), sortCol)
            For Each oneVehicleType As VechileType In objVehicleType
                li = New ListItem(oneVehicleType.VechileTypeCode, oneVehicleType.ID.ToString)
                lboxVehicleType.Items.Add(li)
            Next
        End If
    End Sub

    Public Function CriteriaSearch(ByVal val As String) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If val <> -1 Then
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & val
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        Return criterias
    End Function

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    'Private Sub BindDdlCategory()
    '    ddlCategory.Items.Clear()


    '    Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    Dim sortCol As SortCollection = New SortCollection
    '    sortCol.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))
    '    Dim objCategory As ArrayList = New CategoryFacade(User).Retrieve(critCol, sortCol)

    '    Dim li As ListItem
    '    For Each oneCategory As Category In objCategory
    '        li = New ListItem(oneCategory.CategoryCode, oneCategory.ID.ToString)
    '        ddlCategory.Items.Add(li)
    '    Next

    '    li = New ListItem("Silahkan pilih kategori", "0")
    '    ddlCategory.Items.Insert(0, li)
    'End Sub

    'Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
    '    If ddlCategory.SelectedIndex > 0 Then
    '        'btnSimpan.Enabled = True
    '        BindLbVehicleType()
    '    End If
    'End Sub

    'Private Sub BindLbVehicleType()
    '    lboxVehicleType.Items.Clear()

    '    Dim li As ListItem

    '    Dim CategoryID As Integer = Integer.Parse(ddlCategory.SelectedValue)

    '    Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    critCol.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, CategoryID))
    '    critCol.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))
    '    Dim sortCol As SortCollection = New SortCollection
    '    sortCol.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
    '    Dim objVehicleType As ArrayList = New VechileTypeFacade(User).Retrieve(critCol, sortCol)


    '    For Each oneVehicleType As VechileType In objVehicleType
    '        li = New ListItem(oneVehicleType.VechileTypeCode, oneVehicleType.ID.ToString)
    '        lboxVehicleType.Items.Add(li)
    '    Next

    'End Sub

    Private Sub BindLbFSType()
        lboxFSType.Items.Clear()
        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(FSKind), "KM", Sort.SortDirection.ASC))
        Dim objFSKind As ArrayList = New FSKindFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each oneFSKind As FSKind In objFSKind
            'li = New ListItem(oneFSKind.KindCode & " - " & oneFSKind.KindDescription, oneFSKind.ID.ToString)
            li = New ListItem(oneFSKind.ID & " - " & oneFSKind.KindDescription, oneFSKind.ID.ToString)
            lboxFSType.Items.Add(li)
        Next
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim data As ArrayList = New ArrayList
        Dim totalRow As Integer = 0
        'If (indexPage >= 0) Then
        '    dtgFSCampaign.DataSource = New FSCampaignFacade(User).RetrieveActiveList(indexPage + 1, dtgFSCampaign.PageSize, totalRow, CType(ViewState("currSortColumn"), String), _
        '      CType(ViewState("currSortDirection"), Sort.SortDirection))
        '   dtgFSCampaign.VirtualItemCount = totalRow
        '   dtgFSCampaign.DataBind()
        'End If

        Try
            If ViewState("vsProcess") = "Search" Then
                'Dim strID As String = String.Empty
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If chkDealer.Checked Then
                    Dim strDealer As String = String.Empty
                    Dim arlDealer As ArrayList = New ArrayList
                    Dim icount As Integer = 0
                    arlDealer = GetDealerList(txtKodeDealer.Text.Trim)
                    For Each str As String In arlDealer
                        icount = icount + 1
                        If icount < arlDealer.Count Then
                            strDealer &= "'" & str & "',"
                        Else
                            strDealer &= "'" & str & "'"
                        End If
                    Next
                    If strDealer <> String.Empty Then
                        Dim arlDealerList As ArrayList = New ArrayList
                        Dim oFSDealerFacade As FSCampaignDealerFacade = New FSCampaignDealerFacade(User)
                        Dim criteriasFSD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSCampaignDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasFSD.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSCampaignDealer), "DealerCode", MatchType.InSet, "(" & strDealer & ")"))
                        arlDealerList = oFSDealerFacade.Retrieve(criteriasFSD)
                        If arlDealerList.Count > 0 Then
                            Dim icountList As Integer = 0
                            Dim strID As String = String.Empty
                            For Each fscdealer As FSCampaignDealer In arlDealerList
                                icountList = icountList + 1
                                If icountList <= arlDealerList.Count Then
                                    strID &= fscdealer.FSCampaign.ID & ","
                                End If
                            Next
                            strID = strID.Substring(0, strID.Length - 1)
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSCampaign), "ID", MatchType.InSet, "(" & strID & ")"))
                        End If
                    End If

                End If

                If chkJenisFS.Checked Then
                    Dim strFSType As String = String.Empty
                    If lboxFSType.SelectedValue.Length > 0 Then
                        For Each item As ListItem In lboxFSType.Items
                            If item.Selected Then
                                strFSType &= CType(item.Value, String) & ","
                            End If
                        Next
                        If strFSType <> String.Empty Then
                            strFSType = strFSType.Substring(0, strFSType.Length - 1)
                            Dim arlFSCKindList As ArrayList = New ArrayList
                            Dim oFSCampaignKindFacade As FSCampaignKindFacade = New FSCampaignKindFacade(User)
                            Dim criteriasFSCKind As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSCampaignKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criteriasFSCKind.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSCampaignKind), "FSKind.ID", MatchType.InSet, "(" & strFSType & ")"))

                            arlFSCKindList = oFSCampaignKindFacade.Retrieve(criteriasFSCKind)

                            If arlFSCKindList.Count > 0 Then
                                Dim icountList As Integer = 0
                                Dim strID As String = String.Empty
                                For Each oFSCampaignKind As FSCampaignKind In arlFSCKindList
                                    icountList = icountList + 1
                                    If icountList <= arlFSCKindList.Count Then
                                        strID &= "'" & oFSCampaignKind.FSCampaign.ID & "',"
                                    End If
                                Next
                                strID = strID.Substring(0, strID.Length - 1)
                                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSCampaign), "ID", MatchType.InSet, "(" & strID & ")"))

                            End If
                        End If
                    End If

                End If

                If chkType.Checked Then
                    Dim strVehicleType As String = String.Empty
                    If lboxVehicleType.SelectedValue.Length > 0 Then
                        For Each item As ListItem In lboxVehicleType.Items
                            If item.Selected Then
                                strVehicleType &= CType(item.Value, String) & ","
                            End If
                        Next
                        If strVehicleType <> String.Empty Then
                            strVehicleType = strVehicleType.Substring(0, strVehicleType.Length - 1)
                            Dim arlFSCVehicleList As ArrayList = New ArrayList
                            Dim oFSCampaignVehicleFacade As FSCampaignVehicleFacade = New FSCampaignVehicleFacade(User)
                            Dim criteriasFSCVehicle As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSCampaignVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criteriasFSCVehicle.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSCampaignVehicle), "VechileType.ID", MatchType.InSet, "(" & strVehicleType & ")"))

                            arlFSCVehicleList = oFSCampaignVehicleFacade.Retrieve(criteriasFSCVehicle)

                            If arlFSCVehicleList.Count > 0 Then
                                Dim icountList As Integer = 0
                                Dim strID As String = String.Empty
                                For Each oFSCampaignVehicle As FSCampaignVehicle In arlFSCVehicleList
                                    icountList = icountList + 1
                                    If icountList <= arlFSCVehicleList.Count Then
                                        strID &= "'" & oFSCampaignVehicle.FSCampaign.ID & "',"
                                    End If
                                Next
                                strID = strID.Substring(0, strID.Length - 1)
                                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSCampaign), "ID", MatchType.InSet, "(" & strID & ")"))
                            End If
                        End If
                    End If
                End If

                If chkFaktur.Checked Then
                    Dim DateFrom As New DateTime(CInt(icValidFrom.Value.Year), CInt(icValidFrom.Value.Month), CInt(icValidFrom.Value.Day), 0, 0, 0)
                    Dim DateTo As New DateTime(CInt(icValidTo.Value.Year), CInt(icValidTo.Value.Month), CInt(icValidTo.Value.Day), 23, 59, 59)

                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSCampaign), "DateFrom", MatchType.Exact, DateFrom))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSCampaign), "DateTo", MatchType.Exact, DateTo))
                End If

                If chkPktDate.Checked Then
                    Dim DateFrom As New DateTime(CInt(ictglpktfrom.Value.Year), CInt(ictglpktfrom.Value.Month), CInt(ictglpktfrom.Value.Day), 0, 0, 0)
                    Dim DateTo As New DateTime(CInt(ictglpktto.Value.Year), CInt(ictglpktto.Value.Month), CInt(ictglpktto.Value.Day), 23, 59, 59)

                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSCampaign), "PKTDateFrom", MatchType.Exact, DateFrom))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSCampaign), "PKTDateTo", MatchType.Exact, DateTo))
                End If

                'descripton
                Dim strDeskripsi As String = txtDescription.Text.Trim
                If strDeskripsi <> String.Empty Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSCampaign), "Description", MatchType.[Partial], strDeskripsi))
                End If

                data = New FSCampaignFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgFSCampaign.PageSize, totalRow)
            Else
                data = New FSCampaignFacade(User).RetrieveActiveList(indexPage + 1, dtgFSCampaign.PageSize, totalRow, CType(ViewState("currSortColumn"), String), _
                        CType(ViewState("currSortDirection"), Sort.SortDirection))
            End If

            dtgFSCampaign.DataSource = data
            dtgFSCampaign.DataBind()
        Catch ex As Exception
            dtgFSCampaign.DataSource = data
            dtgFSCampaign.DataBind()
        End Try

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If chkDealer.Checked Then
            If txtKodeDealer.Text = "" Then
                MessageBox.Show("Silahkan pilih Dealer")
                Return
            End If
        End If

        If Not chkPktDate.Checked Then
            MessageBox.Show("Tanggal PKT harus di centang")
            Return
        End If

        'If chkFaktur.Checked = False Then
        '    MessageBox.Show("Faktur Valid Harus di Centang")
        '    Return
        'End If

        If chkJenisFS.Checked Then
            Dim arlJenisFS As ArrayList = New ArrayList
            For Each items As ListItem In lboxFSType.Items
                If items.Selected Then
                    arlJenisFS.Add(items)
                End If
            Next
            If arlJenisFS.Count < 1 Then
                MessageBox.Show("Silahkan Free Service Type")
                Return
            End If
        End If

        If chkType.Checked Then
            'Dim arlVehicleType As ArrayList = New ArrayList
            'If ddlCategory.SelectedIndex = 0 Then
            '    MessageBox.Show("Silahkan pilih Kategori")
            '    Return
            'Else
            '    For Each items As ListItem In lboxVehicleType.Items
            '        If items.Selected Then
            '            arlVehicleType.Add(items)
            '        End If
            '    Next
            '    If arlVehicleType.Count < 1 Then
            '        MessageBox.Show("Silahkan pilih Type Kendaraan")
            '        Return
            '    End If
            'End If


            Dim lboxVTypeCount As Integer = 0
            Try
                lboxVTypeCount = (From item In lboxVehicleType.Items Where item.Selected Select item).Count()
            Catch
                MessageBox.Show("Tipe kendaraan masih kosong")
                Return
            End Try
        End If


        'Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        'Dim categoryArr As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
        'Dim valid As Boolean = False
        'For Each item As Category In categoryArr
        '    If ddlCategory.SelectedValue = item.ID Then
        '        valid = True
        '    End If
        'Next
        'If Not valid Then
        '    MessageBox.Show("Kategori tidak sesuai")
        '    Return
        'End If

        Try
            SaveData(ViewState("vsProcess"))
            ClearData()
            dtgFSCampaign.CurrentPageIndex = 0
            dtgFSCampaign.SelectedIndex = -1
            BindDataGrid(dtgFSCampaign.CurrentPageIndex)
        Catch
            MessageBox.Show(SR.SaveFail)
        End Try

    End Sub

    Private Sub SaveData(ByVal vsProses As String)
        Dim objFsCampaign As FSCampaign = New FSCampaign
        Dim objFSCampaignFacade As FSCampaignFacade = New FSCampaignFacade(User)
        Dim nResult As Integer = -1

        If vsProses = "Edit" Then
            objFsCampaign = sessHelper.GetSession("vsFSCampaign")
            If IsNothing(objFsCampaign) Then
                MessageBox.Show(SR.DataNotFound("Tidak ada data parameter"))
                Return
            End If
        End If

        Dim DateFrom As New DateTime(CInt(icValidFrom.Value.Year), CInt(icValidFrom.Value.Month), CInt(icValidFrom.Value.Day), 0, 0, 0)
        Dim DateTo As New DateTime(CInt(icValidTo.Value.Year), CInt(icValidTo.Value.Month), CInt(icValidTo.Value.Day), 23, 59, 59)

        Dim PKTDateFrom As New DateTime(CInt(ictglpktfrom.Value.Year), CInt(ictglpktfrom.Value.Month), CInt(ictglpktfrom.Value.Day), 0, 0, 0)
        Dim PKTDateTo As New DateTime(CInt(ictglpktto.Value.Year), CInt(ictglpktto.Value.Month), CInt(ictglpktto.Value.Day), 23, 59, 59)

        objFsCampaign.Description = txtDescription.Text.Trim
        objFsCampaign.ErrMessage = txtMessage.Text.Trim
        'If chkFaktur.Checked Then
        objFsCampaign.DateFrom = DateFrom
        objFsCampaign.DateTo = DateTo
        'Else
        '    objFsCampaign.DateFrom = Date.Parse("1/1/1900 00:00:00")
        '    objFsCampaign.DateTo = Date.Parse("1/1/1900 23:59:59")
        'End If

        If chkPktDate.Checked Then
            objFsCampaign.PKTDateFrom = PKTDateFrom
            objFsCampaign.PKTDateTo = PKTDateTo
        Else
            objFsCampaign.PKTDateFrom = Date.Parse("1/1/1900 00:00:00")
            objFsCampaign.PKTDateTo = Date.Parse("1/1/1900 23:59:59")
        End If

        objFsCampaign.DealerChecked = chkDealer.Checked
        objFsCampaign.FSTypeChecked = chkJenisFS.Checked
        objFsCampaign.VehicleTypeChecked = chkType.Checked
        objFsCampaign.FakturDateChecked = chkFaktur.Checked
        objFsCampaign.PKTDateChecked = chkPktDate.Checked
        objFsCampaign.Status = 1 'inactive

        If vsProses = "Insert" Then
            nResult = objFSCampaignFacade.Insert(objFsCampaign)
            objFsCampaign.ID = nResult
        Else
            nResult = objFSCampaignFacade.Update(objFsCampaign)
        End If

        If nResult <= 0 Then
            MessageBox.Show(SR.SaveFail)
        Else
            Dim strDealer As String = txtKodeDealer.Text.Trim
            Try
                If (SaveCampaignDealer(chkDealer.Checked, vsProses, strDealer, objFsCampaign)) _
                    And (SaveCampaignFSKind(chkJenisFS.Checked, vsProses, objFsCampaign)) _
                    And (SaveCampaignVehicleType(chkType.Checked, vsProses, objFsCampaign)) Then
                    If vsProses = "Insert" Then
                        objFSCampaignFacade.InsertIntoMSPRegistration(nResult)
                    End If
                End If

                MessageBox.Show(SR.SaveSuccess)
            Catch ex As Exception
                MessageBox.Show(SR.SaveFail)
            End Try

        End If

    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Page_Load(sender, e)
        ClearData()
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        ViewState("vsProcess") = "Search"
        dtgFSCampaign.CurrentPageIndex = 0
        BindDataGrid(dtgFSCampaign.CurrentPageIndex)
    End Sub
    Private Function GetDealerList(ByVal strDealer As String) As ArrayList
        Dim arlDealer As ArrayList = New ArrayList
        Dim arrOfSpllitedRow() As String
        If strDealer.Length > 0 Then
            arrOfSpllitedRow = strDealer.Split(";")
        End If
        If arrOfSpllitedRow.Length > 0 Then
            For Each item As String In arrOfSpllitedRow
                arlDealer.Add(item)
            Next
        End If
        Return arlDealer
    End Function

    Private Function GetDealerListAll() As ArrayList
        Dim arlDealer As ArrayList = New ArrayList
        Dim arlDealerCode As ArrayList = New ArrayList
        Dim objDealerFacade As DealerFacade = New DealerFacade(User)
        arlDealer = objDealerFacade.RetrieveActiveList()
        For Each objDealer As Dealer In arlDealer
            If objDealer.DealerCode.Length > 3 Then
                arlDealerCode.Add(objDealer.DealerCode)
            End If
        Next
        Return arlDealerCode
    End Function

    Private Function SaveCampaignDealer(ByVal isChecked As Boolean, ByVal vsProses As String, ByVal strDealer As String, ByVal fsCampaign As FSCampaign) As Boolean
        Dim iReturn As Integer = -2
        Dim arlDealer As ArrayList = New ArrayList
        Dim objFSCampaignDealerFacade As FSCampaignDealerFacade = New FSCampaignDealerFacade(User)

        If isChecked = True Then
            arlDealer = GetDealerList(strDealer)
        Else
            arlDealer = GetDealerListAll()
        End If

        If vsProses = "Insert" Then
            iReturn = objFSCampaignDealerFacade.InsertTransaction(arlDealer, fsCampaign)
        Else
            iReturn = objFSCampaignDealerFacade.DeleteTransaction(fsCampaign)
            If iReturn = -2 Then
                iReturn = objFSCampaignDealerFacade.UpdateTransaction(arlDealer, fsCampaign)
            End If
        End If
        If iReturn = -1 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function SaveCampaignFSKind(ByVal isChecked As Boolean, ByVal vsProses As String, ByVal fsCampaign As FSCampaign) As Boolean
        Dim iReturn As Integer = -2
        Dim arlFSKind As ArrayList = New ArrayList
        Try
            Dim objFSCampaignKindFacade As FSCampaignKindFacade = New FSCampaignKindFacade(User)

            If lboxFSType.Items.Count > 0 Then
                For Each item As ListItem In lboxFSType.Items
                    If item.Selected Then
                        arlFSKind.Add(CType(item.Value, Integer))
                    End If
                Next
            End If

            If vsProses = "Insert" Then
                If isChecked = True Then
                    iReturn = objFSCampaignKindFacade.InsertTransaction(arlFSKind, fsCampaign)
                End If
            Else
                iReturn = objFSCampaignKindFacade.DeleteTransaction(fsCampaign)
                If iReturn = -2 Then
                    If isChecked = True Then
                        iReturn = objFSCampaignKindFacade.UpdateTransaction(arlFSKind, fsCampaign)
                    End If
                End If
            End If
        Catch ex As Exception
            Return False
        End Try
        If iReturn = -1 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function SaveCampaignVehicleType(ByVal isChecked As Boolean, ByVal vsProses As String, ByVal fsCampaign As FSCampaign) As Boolean
        Dim iReturn As Integer = -2
        Dim arlVehicleType As ArrayList = New ArrayList
        Try
            Dim objFSCampaignVehicleFacade As FSCampaignVehicleFacade = New FSCampaignVehicleFacade(User)

            If lboxVehicleType.Items.Count > 0 Then
                For Each item As ListItem In lboxVehicleType.Items
                    If item.Selected Then
                        arlVehicleType.Add(item.Value)
                    End If
                Next
            End If

            If vsProses = "Insert" Then
                If isChecked = True Then
                    iReturn = objFSCampaignVehicleFacade.InsertTransaction(arlVehicleType, fsCampaign)
                End If
            Else
                iReturn = objFSCampaignVehicleFacade.DeleteTransaction(fsCampaign)
                If iReturn = -2 Then
                    If isChecked = True Then
                        iReturn = objFSCampaignVehicleFacade.UpdateTransaction(arlVehicleType, fsCampaign)
                    End If
                End If
            End If
        Catch ex As Exception
            Return False
        End Try
        If iReturn = -1 Then
            Return False
        Else
            Return True
        End If
    End Function


    Private Function IsExist(ByVal obj As FSCampaign) As Boolean
        Dim DateFrom As New DateTime(CInt(icValidFrom.Value.Year), CInt(icValidFrom.Value.Month), CInt(icValidFrom.Value.Day), 0, 0, 0)
        Dim DateTo As New DateTime(CInt(icValidTo.Value.Year), CInt(icValidTo.Value.Month), CInt(icValidTo.Value.Day), 23, 59, 59)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(FSCampaign), "DateFrom", MatchType.Exact, Format(DateFrom, "yyyy-MM-dd HH:mm:ss")))
        criterias.opAnd(New Criteria(GetType(FSCampaign), "DateTo", MatchType.Exact, Format(DateTo, "yyyy-MM-dd HH:mm:ss")))

        Return New FSCampaignFacade(User).Retrieve(criterias).Count > 0
    End Function

    Private Sub dtgFSCampaign_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgFSCampaign.ItemCommand
        If e.CommandName = "Edit" Then
            Dim strDealer As String = String.Empty
            Dim iCount As Integer = 0
            Dim arlDealer As ArrayList = New ArrayList

            arlDealer = GetCampaignDealer(CType(e.Item.Cells(0).Text, Integer))
            If arlDealer.Count > 0 Then
                For Each fsDealer As FSCampaignDealer In arlDealer
                    iCount = iCount + 1
                    If iCount = arlDealer.Count Then
                        strDealer &= fsDealer.DealerCode
                    Else
                        strDealer &= fsDealer.DealerCode & ";"
                    End If
                    'If iCount > 5 Then Exit For
                Next
            End If
            ViewState.Add("vsProcess", "Edit")
            ViewFSCampaign(e.Item.Cells(0).Text, True, strDealer)

        ElseIf e.CommandName = "Active" Then
            ActivateCampaign(e.Item.Cells(0).Text)
            BindDataGrid(0)  '-- Bind page-1

        ElseIf e.CommandName = "Inactive" Then
            InactivateCampaign(e.Item.Cells(0).Text)
            BindDataGrid(0)  '-- Bind page-1

        ElseIf e.CommandName = "Delete" Then
            DeleteCampaign(e.Item.Cells(0).Text)
            BindDataGrid(0)  '-- Bind page-1
        ElseIf e.CommandName = "View" Then
            sessHelper.SetSession("backURL", "./FrmFSCampaign.aspx")
            sessHelper.SetSession("vsFSCampaignID", e.Item.Cells(0).Text)
            If Not IsNothing(sessHelper.GetSession("vsFSCampaignID")) Then
                Response.Redirect("FrmFSCampaignDetail.aspx?backURL=./FrmFSCampaign.aspx")
            End If
        End If
    End Sub

    Private Function GetCampaignDealer(ByVal nID As Integer) As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(FSCampaignDealer), "FSCampaign.ID", MatchType.Exact, nID))
        Return New FSCampaignDealerFacade(User).Retrieve(criterias)
    End Function

    Private Sub ViewFSCampaign(ByVal nID As Integer, ByVal EditStatus As Boolean, ByVal strDealer As String)
        Dim objFSCampaign As FSCampaign = New FSCampaignFacade(User).Retrieve(nID)
        sessHelper.SetSession("vsFSCampaign", objFSCampaign)

        chkDealer.Checked = objFSCampaign.DealerChecked
        chkJenisFS.Checked = objFSCampaign.FSTypeChecked
        chkType.Checked = objFSCampaign.VehicleTypeChecked
        chkFaktur.Checked = objFSCampaign.FakturDateChecked
        chkPktDate.Checked = objFSCampaign.PKTDateChecked
        txtKodeDealer.Text = strDealer
        'BindLbFSType()

        For Each itemList As ListItem In lboxFSType.Items
            itemList.Selected = False

        Next

        For Each itemFSCampaignKind As FSCampaignKind In objFSCampaign.FSCampaignKinds
            For Each itemList As ListItem In lboxFSType.Items
                If (itemFSCampaignKind.RowStatus = 0) And (itemFSCampaignKind.FSKind.ID.ToString() = itemList.Value) Then
                    itemList.Selected = True

                End If

            Next
        Next
        'BindDdlCategory()
        'Dim ctg As KTB.DNet.Domain.FSCampaignVehicle = New KTB.DNet.Domain.FSCampaignVehicle
        'If objFSCampaign.FSCampaignVehicles.Count > 0 Then
        '    Dim arlCat As ArrayList = New ArrayList
        '    arlCat = objFSCampaign.FSCampaignVehicles
        '    ctg = arlCat(0)
        '    ddlCategory.SelectedValue = ctg.VechileType.Category.ID
        'End If

        'BindLbVehicleType()
        'For Each itemFSCampaignVehicle As FSCampaignVehicle In objFSCampaign.FSCampaignVehicles
        '    For Each itemList As ListItem In lboxVehicleType.Items
        '        If (itemFSCampaignVehicle.RowStatus = 0) And (itemFSCampaignVehicle.VechileType.ID = itemList.Value) Then
        '            itemList.Selected = True
        '        End If
        '    Next
        'Next

        BindVehicleType()
        For Each itemFSCampaignVehicle As FSCampaignVehicle In objFSCampaign.FSCampaignVehicles
            If Not IsNothing(itemFSCampaignVehicle.VechileType) Then
                'selectCategoryListBox(item.VechileType.Description.Split(" "))
                selectCategoryListBox2(itemFSCampaignVehicle.VechileType.VechileModel.ID)
            End If
        Next

        SearchVehicleType()
        selectVehicleTypeListBox(nID)
        lboxVehicleType_SelectedIndexChanged(Nothing, Nothing)

        icValidFrom.Value = objFSCampaign.DateFrom
        icValidTo.Value = objFSCampaign.DateTo
        ictglpktfrom.Value = objFSCampaign.PKTDateFrom
        ictglpktto.Value = objFSCampaign.PKTDateTo
        txtDescription.Text = objFSCampaign.Description
        txtMessage.Text = objFSCampaign.ErrMessage
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub selectCategoryListBox2(ByVal modelID As Integer)
        If lboxCategory.GetSelectedIndices().Count = lboxCategory.Items.Count Then Exit Sub

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, modelID))
        Dim arlSubCategoryVehicleToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
        For Each item As SubCategoryVehicleToModel In arlSubCategoryVehicleToModel
            Dim iSubCategoryVehicleID As Integer = item.SubCategoryVehicle.ID

            Dim index As Integer
            For index = 0 To lboxCategory.Items.Count - 1
                Dim li As String = lboxCategory.Items(index).Value
                If iSubCategoryVehicleID = li Then
                    lboxCategory.Items(index).Selected = True
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub selectVehicleTypeListBox(ByVal ParamHeadID As Integer)
        Dim index As Integer
        For index = 0 To lboxVehicleType.Items.Count - 1
            Dim li As String = lboxVehicleType.Items(index).Value

            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(FSCampaignVehicle), "FSCampaign.ID", MatchType.Exact, ParamHeadID))
            criteria.opAnd(New Criteria(GetType(FSCampaignVehicle), "VechileType.ID", MatchType.Exact, li))
            Dim arlParamVehicle As ArrayList = New FSCampaignVehicleFacade(User).Retrieve(criteria)
            For Each item As FSCampaignVehicle In arlParamVehicle
                If li = item.VechileType.ID.ToString Then
                    lboxVehicleType.Items(index).Selected = True
                End If
            Next
        Next
    End Sub

    Private Sub ActivateCampaign(ByVal nID As Integer)
        '-- Activate Parameter
        Dim oFSCampaign As FSCampaign = New FSCampaignFacade(User).Retrieve(nID)
        oFSCampaign.Status = 0  '-- Parameter Aktif
        Dim nResult = New FSCampaignFacade(User).Update(oFSCampaign)
    End Sub

    Private Sub InactivateCampaign(ByVal nID As Integer)
        '-- Inactivate Parameter
        Dim oFSCampaign As FSCampaign = New FSCampaignFacade(User).Retrieve(nID)
        oFSCampaign.Status = 1  '-- Parameter Tidak Aktif
        Dim nResult = New FSCampaignFacade(User).Update(oFSCampaign)
    End Sub

    Private Sub DeleteCampaign(ByVal nID As Integer)
        '-- Inactivate Parameter
        Dim oFSCampaign As FSCampaign = New FSCampaignFacade(User).Retrieve(nID)
        oFSCampaign.RowStatus = 1  '-- Parameter Tidak Aktif
        Dim nResult = New FSCampaignFacade(User).Delete(oFSCampaign)

    End Sub

    Private Sub dtgFSCampaign_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFSCampaign.ItemDataBound
        If e.Item.ItemIndex <> -1 Then

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnActive As LinkButton = CType(e.Item.FindControl("lbtnActive"), LinkButton)
            Dim lbtnInactive As LinkButton = CType(e.Item.FindControl("lbtnInactive"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            Dim RowValue As FSCampaign = CType(e.Item.DataItem, FSCampaign)  '-- Current record

            If RowValue.Status = 0 Then
                If sessHelper.GetSession("ActivateCampaign") Then
                    lbtnInactive.Visible = True
                Else
                    lbtnInactive.Visible = False
                End If
                lbtnActive.Visible = False

            ElseIf RowValue.Status = 1 Then
                lbtnInactive.Visible = False
                If sessHelper.GetSession("ActivateCampaign") Then
                    lbtnActive.Visible = True
                Else
                    lbtnActive.Visible = False
                End If
            End If

            If sessHelper.GetSession("UpdateCampaign") Then
                lbtnEdit.Visible = True
                lbtnDelete.Visible = True
            Else
                lbtnEdit.Visible = False
                lbtnDelete.Visible = False
            End If

            Dim strDealer As String = String.Empty
            Dim iCount As Integer = 0
            If RowValue.FSCampaignDealers.Count > 0 Then
                For Each fsDealer As FSCampaignDealer In RowValue.FSCampaignDealers
                    iCount = iCount + 1
                    If iCount = RowValue.FSCampaignDealers.Count Then
                        strDealer &= fsDealer.DealerCode
                    Else
                        strDealer &= fsDealer.DealerCode & ";"
                    End If
                    If iCount > 5 Then Exit For
                Next
            Else
                strDealer = "     -     "
            End If

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1 + (dtgFSCampaign.CurrentPageIndex * dtgFSCampaign.PageSize), String)
            End If

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                If strDealer.Length > 15 Then
                    CType(e.Item.FindControl("lblDealer"), Label).Text = strDealer.Substring(0, 15) & ".."
                Else
                    CType(e.Item.FindControl("lblDealer"), Label).Text = strDealer
                End If
                CType(e.Item.FindControl("lblDealerFull"), Label).Text = strDealer
            End If



            Dim strFSKind As String = String.Empty
            Dim iCountFSKind As Integer = 0
            If RowValue.FSCampaignKinds.Count > 0 Then
                For Each fsKind As FSCampaignKind In RowValue.FSCampaignKinds
                    iCountFSKind = iCountFSKind + 1
                    If iCountFSKind = RowValue.FSCampaignKinds.Count Then
                        strFSKind &= fsKind.FSKind.KindDescription
                    Else
                        strFSKind &= fsKind.FSKind.KindDescription & ";"
                    End If
                    If iCountFSKind > 5 Then Exit For
                Next
            Else
                strFSKind = "     -     "
            End If
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                If strFSKind.Length > 15 Then
                    CType(e.Item.FindControl("lblFSType"), Label).Text = strFSKind.Substring(0, 15) & ".."
                Else
                    CType(e.Item.FindControl("lblFSType"), Label).Text = strFSKind
                End If
            End If

            Dim strVehicle As String = String.Empty
            Dim iCountVehicle As Integer = 0
            If RowValue.FSCampaignVehicles.Count > 0 Then
                For Each fsVehicle As FSCampaignVehicle In RowValue.FSCampaignVehicles
                    iCountVehicle = iCountVehicle + 1
                    If iCountVehicle = RowValue.FSCampaignKinds.Count Then
                        strVehicle &= fsVehicle.VechileType.VechileTypeCode
                    Else
                        strVehicle &= fsVehicle.VechileType.VechileTypeCode & ";"
                    End If
                    If iCountVehicle > 5 Then Exit For
                Next
            Else
                strVehicle = "     -     "
            End If
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                If strVehicle.Length > 15 Then
                    CType(e.Item.FindControl("lblVehicleType"), Label).Text = strVehicle.Substring(0, 15) & ".."
                Else
                    CType(e.Item.FindControl("lblVehicleType"), Label).Text = strVehicle
                End If
            End If

            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                '-- Confirm activation
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).ToolTip = "Edit"
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Attributes.Add("OnClick", "return confirm('Ubah record ini?');")
            End If
            If Not e.Item.FindControl("lbtnActive") Is Nothing Then
                '-- Confirm activation
                CType(e.Item.FindControl("lbtnActive"), LinkButton).ToolTip = "Aktifkan"
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Aktifkan record ini?');")
            End If
            If Not e.Item.FindControl("lbtnInactive") Is Nothing Then
                '-- Confirm inactivation
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).ToolTip = "Non Aktifkan"
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Attributes.Add("OnClick", "return confirm('Non-Aktifkan record ini?');")
            End If
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                '-- Confirm deletion
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).ToolTip = "Hapus"
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Hapus record ini?');")
            End If

        End If
    End Sub

    Private Sub dtgFSCampaign_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgFSCampaign.SortCommand
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
        dtgFSCampaign.SelectedIndex = -1
        dtgFSCampaign.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub

    Private Sub dtgFSCampaign_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgFSCampaign.PageIndexChanged
        BindDataGrid(e.NewPageIndex)
    End Sub

    Private Sub txtKodeDealer_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKodeDealer.TextChanged
        ViewState.Add("strDealer", txtKodeDealer.Text.Trim)
    End Sub
End Class
