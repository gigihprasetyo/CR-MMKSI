Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.String
Imports System.IO
Imports System.Text


Public Class FrmSparePartMaster
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgSparePartMaster As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cfSparePartMaster As FilterCompositeControl.CompositeFilter
    Protected WithEvents txtModel As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label 'add 290918
    Protected WithEvents btnSearchOnSAP As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnDnLoad As System.Web.UI.WebControls.Button
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private lbtnActivate_priv As Boolean
    Private lbtnDeactive_priv As Boolean
    Private Ubah_status_material_privilege As Boolean


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variable"
    Private arrSparePartMaster As ArrayList = New ArrayList
    Private _sessHelper As SessionHelper = New SessionHelper
    Private _showdetail As Boolean = False
    Private _btnCari As Boolean = False
    Private idxPage As Integer
    Private _vstIsEditAccessories As String = "_vstIsEditAccessories"
    Dim sHelper As New SessionHelper
    Dim _sesData As String = "_frmSparepartMaster.download"
    Dim _sesProses As String = "_frmSparepartMaster.proses"
    Dim objSparePartMaster As SparePartMaster = New SparePartMaster
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        
        InitiateAuthorization()
        If Not IsPostBack Then
            'Put user code to initialize the page here
            ViewState("ProsesAwal") = True
            InitialPage()
        End If
        lblSearchDealer.Attributes("onclick") = "ShowModelSelection();"
    End Sub

    'Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
    '    If Not CType(ViewState("ProsesAwal"), Boolean) Then
    '        ViewState("ProsesAwal") = True
    '        InitialPage()
    '    End If

    'End Sub

    Private Sub cfSparePartMaster_Filter(ByVal sender As Object, ByVal FilterArg As FilterCompositeControl.OnFilterArgs) Handles cfSparePartMaster.Filter

        dtgSparePartMaster.CurrentPageIndex = 0
        ViewState("ProsesAwal") = False
        'MessageBox.Show(cfSparePartMaster.)

        If cfSparePartMaster.ColumnName = "Stock" And cfSparePartMaster.OperatorName <> 0 And cfSparePartMaster.OperatorName <> 1 Then
            MessageBox.Show("Pencarian stock hanya bisa dilakukan dengan kategori <Sama Dengan> dan <Tdk Sama Dengan>")
            Exit Sub
        End If

        Try
            'Todo session
            Session.Add("Back", False)
            AddSession()
            _sessHelper.SetSession("ModelTemp", txtModel.Text.Trim)
            BindDtgSparePartMaster(dtgSparePartMaster.CurrentPageIndex)
        Catch ex As Exception
            ClearSession()
            BindDtgSparePartMaster()
        End Try

        If dtgSparePartMaster.VirtualItemCount = 0 Then MessageBox.Show("Data Tidak Ditemukan")


    End Sub

    Private Sub BindDDL()
        Dim _arrStatus As ArrayList = New EnumSparePartActiveStatus().GetList()
        ddlStatus.DataSource = _arrStatus
        ddlStatus.DataBind()
    End Sub

    Private Sub dtgSparePartMaster_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSparePartMaster.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(2).Text = e.Item.ItemIndex + 1 + (dtgSparePartMaster.CurrentPageIndex * dtgSparePartMaster.PageSize)

            'Dim objSparePartMaster As SparePartMaster = New SparePartMaster
            objSparePartMaster = CType(arrSparePartMaster(e.Item.ItemIndex + (dtgSparePartMaster.CurrentPageIndex * dtgSparePartMaster.PageSize)), SparePartMaster)

            'objSparePartMaster = CType(arrSparePartMaster(e.Item.ItemIndex), SparePartMaster)

            Dim lblProduct As Label = e.Item.FindControl("lblProduct")
            If Not IsNothing(objSparePartMaster.ProductCategory) Then
                lblProduct.Text = objSparePartMaster.ProductCategory.Code
            End If

            'stock
            If objSparePartMaster.Stock = 0 Then
                e.Item.Cells(9).Text = "Tidak Ada"
            ElseIf objSparePartMaster.Stock > 0 Then
                e.Item.Cells(9).Text = "Ada"
            End If
            'stock

            'icon altPartNumber dan status
            If objSparePartMaster.PartCode = "O" Or objSparePartMaster.PartCode = "N" Then
                e.Item.FindControl("lblView").Visible = True
                'Dim lblPartCode As Label = e.Item.FindControl("labelPartCode")
                'lblPartCode.Text = objSparePartMaster.PartCode.Trim
                e.Item.Cells(5).Text = objSparePartMaster.PartCode.Trim
                e.Item.Cells(3).Text = objSparePartMaster.PartNumber.Trim
            Else
                e.Item.FindControl("lblView").Visible = False
            End If
            'icon altPartNumber dan status

            'teks altPartNumber
            'Dim lblAltPartNumber As Label = e.Item.FindControl("labelAlternatifPartNumber")

            If objSparePartMaster.AltPartNumber.Trim <> String.Empty Or objSparePartMaster.AltPartNumber.Trim <> "" Then
                'lblAltPartNumber.Text = objSparePartMaster.AltPartNumber.Trim
                e.Item.Cells(10).Text = objSparePartMaster.AltPartNumber.Trim

            ElseIf objSparePartMaster.AltPartNumber.Trim = String.Empty Or objSparePartMaster.AltPartNumber.Trim = "" Then
                e.Item.Cells(10).Text = ""
                'lblAltPartNumber.Text = ""
            End If
            ''teks altPartNumber
            'ActiveStatus
            Dim labelStatus As Label = e.Item.FindControl("labelStatus")
            labelStatus.Text = (New EnumSparePartActiveStatus).GetStringValue(objSparePartMaster.ActiveStatus)
            'e.Item.Cells(10).Text = (New EnumSparePartActiveStatus).GetStringValue(objSparePartMaster.ActiveStatus)
            'Dim lbtnActivate As LinkButton = e.Item.FindControl("lbtnActivate")
            'Dim lbtnDeactive As LinkButton = e.Item.FindControl("lbtnDeactive")
            'If objSparePartMaster.ActiveStatus = EnumSparePartActiveStatus.SparePartActiveStatus.Active Then
            '    lbtnActivate.Visible = False
            '    lbtnDeactive.Visible = True
            'Else
            '    lbtnActivate.Visible = True
            '    lbtnDeactive.Visible = False
            'End If
            'ActiveStatus
            'popup
            If objSparePartMaster.PartCode = "O" Or objSparePartMaster.PartCode = "N" Then
                If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                    SetSparePartMasterAltButton(e)
                End If
            End If
            'popup
            'Ubah_status_material_privilege = SecurityProvider.Authorize(Context.User, SR.Ubah_status_material_privilege)

            If Not Ubah_status_material_privilege Then
                CType(e.Item.FindControl("lbtnActivate"), LinkButton).Visible = lbtnActivate_priv 'SecurityProvider.Authorize(Context.User, SR.Ubah_status_material_privilege)
                CType(e.Item.FindControl("lbtnDeactive"), LinkButton).Visible = lbtnDeactive_priv  'SecurityProvider.Authorize(Context.User, SR.Ubah_status_material_privilege)
            End If

            Dim ddlAccessoriesType As DropDownList = e.Item.FindControl("ddlAccessoriesType")
            Dim btnUpdateAccType As Button = e.Item.FindControl("btnUpdateAccType")
            Dim aIs As ArrayList = enumAccessoriesType.GetList()
            Dim IsEdit As Boolean = False

            ddlAccessoriesType.Items.Clear()
            For Each li As ListItem In aIs
                ddlAccessoriesType.Items.Add(li)
            Next
            Try
                ddlAccessoriesType.SelectedValue = objSparePartMaster.AccessoriesType
            Catch ex As Exception

            End Try
            If Not IsNothing(Me.ViewState.Item(Me._vstIsEditAccessories)) AndAlso Me.ViewState.Item(Me._vstIsEditAccessories) = "1" Then
                IsEdit = True
            End If
            ddlAccessoriesType.Enabled = IsEdit

            If IsEdit Then ddlAccessoriesType.Attributes.Add("OnChange", "document.getElementById('" & btnUpdateAccType.ClientID & "').click();")
        End If

    End Sub

    Private Sub dtgSparePartMaster_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSparePartMaster.SortCommand
        If Session.Item("DataSourceDtg") > 0 Then
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
            'ViewState("ProsesAwal") = False
            'Todo session
            Session.Add("SortColumn", CType(ViewState("currSortColumn"), String))
            'Todo session
            Session.Add("SortDirection", CType(ViewState("currSortDirection"), Sort.SortDirection))
            dtgSparePartMaster.SelectedIndex = -1
            dtgSparePartMaster.CurrentPageIndex = 0
            BindDtgSparePartMaster(dtgSparePartMaster.CurrentPageIndex)
        End If
    End Sub

    Private Sub dtgSparePartMaster_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSparePartMaster.PageIndexChanged
        dtgSparePartMaster.CurrentPageIndex = e.NewPageIndex
        'ViewState("ProsesAwal") = False
        'Todo session
        Session.Add("IndexPage", e.NewPageIndex + 1)
        'BindDtgSparePartMaster(dtgSparePartMaster.CurrentPageIndex + 1)
        BindDtgSparePartMaster(e.NewPageIndex + 1)
    End Sub

    Private Sub dtgSparePartMaster_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSparePartMaster.ItemCommand
        If e.CommandName.ToUpper = "activate".ToUpper Or e.CommandName.ToUpper = "deactivate".ToUpper Then
            Dim objSPMFac As SparePartMasterFacade = New SparePartMasterFacade(User)
            Dim objSPM As SparePartMaster = objSPMFac.Retrieve(CType(e.Item.Cells(0).Text, Integer)) '  CType(arrSparePartMaster(e.Item.ItemIndex), SparePartMaster)
            If e.CommandName.ToUpper = "activate".ToUpper Then
                objSPM.ActiveStatus = EnumSparePartActiveStatus.SparePartActiveStatus.Active
            Else
                objSPM.ActiveStatus = EnumSparePartActiveStatus.SparePartActiveStatus.InActive
            End If
            objSPMFac.Update(objSPM)
            ViewState("currSortColumn") = "PartName"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            _sessHelper.SetSession("ModelTemp", txtModel.Text.Trim)
            BindDtgSparePartMaster(dtgSparePartMaster.CurrentPageIndex)
        End If
        'If e.CommandName = "View" Then
        '    If e.Item.Cells(8).Text.Trim <> "" Or e.Item.Cells(8).Text.Trim <> String.Empty Then
        '        Session.Add("NoSparePartAlt", e.Item.Cells(8).Text.Trim)
        '        Response.Redirect("../SparePart/FrmSparePartMasterAlt.aspx")
        '    ElseIf e.Item.Cells(8).Text.Trim = "" Or e.Item.Cells(8).Text.Trim = String.Empty Then
        '        MessageBox.Show(SR.DataNotFound("Part Alternatif"))
        '    End If
        'End If
        If e.CommandName.Trim.ToUpper = "UpdateAccType".ToUpper Then
            Dim objSPMFac As SparePartMasterFacade = New SparePartMasterFacade(User)
            Dim objSPM As SparePartMaster = objSPMFac.Retrieve(CType(e.Item.Cells(0).Text, Integer)) '  CType(arrSparePartMaster(e.Item.ItemIndex), SparePartMaster)
            Dim ddlAccessoriesType As DropDownList = e.Item.FindControl("ddlAccessoriesType")
            Dim ID As Integer = 0

            objSPM.AccessoriesType = CType(ddlAccessoriesType.SelectedValue, Short)
            ID = objSPMFac.Update(objSPM)
            If ID > 0 Then
                MessageBox.Show(SR.SaveSuccess)
            Else
                ddlAccessoriesType.SelectedValue = objspm.AccessoriesType
                MessageBox.Show(SR.SaveFail)
            End If
        End If
    End Sub

#Region "private sub/function"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ViewSparePartMaster_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MASTER BARANG - Master Barang")
        End If

        Ubah_status_material_privilege = SecurityProvider.Authorize(Context.User, SR.Ubah_status_material_privilege)
        If Not Ubah_status_material_privilege Then
            lbtnActivate_priv = SecurityProvider.Authorize(Context.User, SR.Ubah_status_material_privilege)
            lbtnDeactive_priv = SecurityProvider.Authorize(Context.User, SR.Ubah_status_material_privilege)

            '-- Add Req by Benny 290918
            Label2.Visible = SecurityProvider.Authorize(Context.User, SR.Ubah_status_material_privilege)
            btnProses.Visible = SecurityProvider.Authorize(Context.User, SR.Ubah_status_material_privilege)
            ddlStatus.Visible = SecurityProvider.Authorize(Context.User, SR.Ubah_status_material_privilege)
            '-- end

        End If

        'Me.btnFind.Visible = SecurityProvider.Authorize(context.User, SR.SearchSPPO_Status_Privilege)

        '--exclude  this privilege from Asra (BA)
        'cfSparePartMaster.Visible = SecurityProvider.Authorize(Context.User, SR.SearchSparePartMaster_Privilege)

        _showdetail = SecurityProvider.Authorize(Context.User, SR.ViewSparePartMasterAltPage_Privilege)
        '_isShowDetailAllowed = SecurityProvider.Authorize(context.User, SR.ViewSPPO_StatusDetail_Privilege)
        'If _isPrintAllowed = False And _isShowDetailAllowed = False Then
        '    Me.dtgPOStatus.Columns(7).Visible = False
        'End If

        Me.dtgSparePartMaster.Columns(9).Visible = SecurityProvider.Authorize(context.User, SR.Ubah_status_material_privilege)
        Dim IsAcc As Boolean = False
        If Not IsNothing(Me.Request.Item("IsAccessories")) Then
            If Me.Request.Item("IsAccessories") = "1" Then
                IsAcc = True
            End If
        End If
        Me.dtgSparePartMaster.Columns(Me.dtgSparePartMaster.Columns.Count - 1).Visible = IsAcc
        If IsAcc Then
            Dim IsEdit As Boolean = False
            Me.dtgSparePartMaster.Columns(Me.dtgSparePartMaster.Columns.Count - 1).Visible = True
            If Not SecurityProvider.Authorize(Context.User, SR.Lihat_master_barang_accessories_privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=MASTER BARANG - Master Barang (Accessories)")
            Else
                IsEdit = SecurityProvider.Authorize(Context.User, SR.Ubah_master_barang_accessories_privilege)
            End If
            viewstate.Add(Me._vstIsEditAccessories, IIf(IsEdit, "1", "0"))
        End If
    End Sub

    Private Sub InitialPage()
        If Not Session.Item("Back") Then
            ClearSession()
        ElseIf Session.Item("Back") Then
            'Todo session
            Session.Add("Back", False)
        End If
        ViewState("currSortColumn") = "PartName"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        _sessHelper.SetSession("ModelTemp", txtModel.Text.Trim)
        dtgSparePartMaster.CurrentPageIndex = 0
        BindDtgSparePartMaster(dtgSparePartMaster.CurrentPageIndex)
        BindDDL()
    End Sub

    Private Sub AddSession()
        'Todo session
        Session.Add("IndexPage", idxPage)
        'Todo session
        Session.Add("ColName", cfSparePartMaster.ColumnName)
        If cfSparePartMaster.ColumnName = "Stock" Then
            If (cfSparePartMaster.OperatorName = 0 And CType(cfSparePartMaster.KeyWord, String).ToUpper = "ADA") Or _
               (cfSparePartMaster.OperatorName = 1 And CType(cfSparePartMaster.KeyWord, String).ToUpper = "TIDAK ADA") Then
                'Todo session
                Session.Add("OpName", 1)
                'Todo session
                Session.Add("KWord", "0")
            ElseIf (cfSparePartMaster.OperatorName = 0 And CType(cfSparePartMaster.KeyWord, String).ToUpper = "TIDAK ADA") Or _
                (cfSparePartMaster.OperatorName = 1 And CType(cfSparePartMaster.KeyWord, String).ToUpper = "ADA") Then
                'Todo session
                Session.Add("OpName", 0)
                'Todo session
                Session.Add("KWord", "0")
            End If
        ElseIf cfSparePartMaster.ColumnName = "RetalPrice" Then
            'Todo session
            Session.Add("OpName", cfSparePartMaster.OperatorName)
            'Todo session
            Session.Add("KWord", CType(cfSparePartMaster.KeyWord, Integer))
        Else
            Session.Add("OpName", cfSparePartMaster.OperatorName)
            Session.Add("KWord", cfSparePartMaster.KeyWord)
        End If
        'Session.Add("OpName", cfSparePartMaster.OperatorName)
        'Session.Add("KWord", cfSparePartMaster.KeyWord)
        'Todo session
        Session.Add("SortColumn", CType(ViewState("currSortColumn"), String))
        'Todo session
        Session.Add("SortDirection", CType(ViewState("currSortDirection"), Sort.SortDirection))
    End Sub

    Private Sub ClearSession()
        Session.Remove("IndexPage")
        Session.Remove("ColName")
        Session.Remove("OpName")
        Session.Remove("KWord")
        Session.Remove("SortColumn")
        Session.Remove("SortDirection")
    End Sub

    Private Sub BindDtgSparePartMaster()
        dtgSparePartMaster.DataSource = New ArrayList
        dtgSparePartMaster.VirtualItemCount = 0
        dtgSparePartMaster.DataBind()
        'Todo session
        Session.Add("DataSourceDtg", CType(dtgSparePartMaster.DataSource, ArrayList).Count)
        
    End Sub

    Private Sub BindDtgSparePartMaster(ByVal pageIndeks As Integer)
        idxPage = pageIndeks 'ini buat simpen session pas balik lagi ke halaman ini
        Dim totalRow As Integer = 0
        Dim ModelTemp As String = CType(_sessHelper.GetSession("ModelTemp"), String)
        If CType(ViewState("ProsesAwal"), Boolean) Then
            If Not Session.Item("ColName") Is Nothing Then
                If Session.Item("ColName") = "ALL" Then
                    dtgSparePartMaster.DataSource = New SparePartMasterFacade(User).RetrieveActiveList(ModelTemp, Session.Item("IndexPage"), dtgSparePartMaster.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
                ElseIf Session.Item("ColName") <> "ALL" Then
                    dtgSparePartMaster.DataSource = New SparePartMasterFacade(User).RetrieveWithOneCriteriaExtModel(ModelTemp, Session.Item("IndexPage"), dtgSparePartMaster.PageSize, totalRow, Session.Item("ColName"), Session.Item("OpName"), Session.Item("KWord"), Session.Item("SortColumn"), Session.Item("SortDirection"))
                    arrSparePartMaster = CType(New SparePartMasterFacade(User).RetrieveWithOneCriteriaExtModelAllPages(ModelTemp, Session.Item("ColName"), Session.Item("OpName"), Session.Item("KWord"), ViewState("currSortColumn"), ViewState("currSortDirection")), ArrayList)
                End If
            ElseIf Session.Item("ColName") Is Nothing Then
                'dtgSparePartMaster.DataSource = New ArrayList
                'dtgSparePartMaster.DataSource = New SparePartMasterFacade(User).RetrieveWithOneCriteriaExtModel(ModelTemp, pageIndeks, dtgSparePartMaster.PageSize, totalRow, "Stock", MatchType.GreaterOrEqual, 0, ViewState("currSortColumn"), ViewState("currSortDirection"))
                dtgSparePartMaster.DataSource = New SparePartMasterFacade(User).RetrieveWithOneCriteriaExtModel(ModelTemp, pageIndeks, dtgSparePartMaster.PageSize, totalRow, "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short), ViewState("currSortColumn"), ViewState("currSortDirection"))
                arrSparePartMaster = CType(New SparePartMasterFacade(User).RetrieveWithOneCriteriaExtModelAllPages(ModelTemp, "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short), ViewState("currSortColumn"), ViewState("currSortDirection")), ArrayList)
            End If
        ElseIf Not CType(ViewState("ProsesAwal"), Boolean) Then
            If Session.Item("ColName") = "ALL" Then
                dtgSparePartMaster.DataSource = New SparePartMasterFacade(User).RetrieveWithOneCriteriaExtModel(ModelTemp, pageIndeks, dtgSparePartMaster.PageSize, totalRow, "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short), ViewState("currSortColumn"), ViewState("currSortDirection"))
                arrSparePartMaster = CType(New SparePartMasterFacade(User).RetrieveWithOneCriteriaExtModelAllPages(ModelTemp, "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short), ViewState("currSortColumn"), ViewState("currSortDirection")), ArrayList)
            ElseIf Session.Item("ColName") <> "ALL" Then
                dtgSparePartMaster.DataSource = New SparePartMasterFacade(User).RetrieveWithOneCriteriaExtModel(ModelTemp, Session.Item("IndexPage"), dtgSparePartMaster.PageSize, totalRow, Session.Item("ColName"), Session.Item("OpName"), Session.Item("KWord"), Session.Item("SortColumn"), Session.Item("SortDirection"))
                arrSparePartMaster = CType(New SparePartMasterFacade(User).RetrieveWithOneCriteriaExtModelAllPages(ModelTemp, Session.Item("ColName"), Session.Item("OpName"), Session.Item("KWord"), ViewState("currSortColumn"), ViewState("currSortDirection")), ArrayList)
            End If

            'If cfSparePartMaster.ColumnName = "ALL" Then
            '    dtgSparePartMaster.DataSource = New SparePartMasterFacade(User).RetrieveActiveList(pageIndeks, dtgSparePartMaster.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
            'Else
            '    dtgSparePartMaster.DataSource = New SparePartMasterFacade(User).RetrieveWithOneCriteria(pageIndeks, dtgSparePartMaster.PageSize, totalRow, cfSparePartMaster.ColumnName, cfSparePartMaster.OperatorName, cfSparePartMaster.KeyWord, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
            'End If
        End If

        sHelper.SetSession(_sesData, arrSparePartMaster)
        sHelper.SetSession(_sesProses, arrSparePartMaster)

        dtgSparePartMaster.VirtualItemCount = totalRow
        'Todo session
        Session.Add("DataSourceDtg", CType(dtgSparePartMaster.DataSource, ArrayList).Count)
        dtgSparePartMaster.DataBind()

    End Sub

    Private Function CreateCriteriaByAltPartNumber(ByVal NoAltPart As String) As CriteriaComposite
        Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(SparePartMaster), "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New criteria(GetType(SparePartMaster), "AltPartNumber", MatchType.Exact, NoAltPart))
        Return criteria
    End Function

    Private Function CreateCriteria() As CriteriaComposite


    End Function

    Private Sub SetSparePartMasterAltButton(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If e.Item.Cells(5).Text.Trim = "O" Or e.Item.Cells(5).Text.Trim = "N" Then
            'If _showdetail Then
            e.Item.FindControl("lblView").Visible = True '_showdetail
            If e.Item.Cells(5).Text.Trim = "O" Then
                CType(e.Item.FindControl("lblView"), Label).Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                  "../SparePart/frmSparePartMasterAlt.aspx?NoSparePartAlt=" & e.Item.Cells(10).Text + "", "", 600, 800, "null")
            ElseIf e.Item.Cells(5).Text.Trim = "N" Then
                CType(e.Item.FindControl("lblView"), Label).Attributes("onclick") = GeneralScript.GetPopUpEventReference( _
                  "../SparePart/frmSparePartMasterAlt.aspx?NoSparePartAlt=" & e.Item.Cells(3).Text + "", "", 600, 800, "null")
            End If
            'lblSearchDealer.Attributes("onClick") = "ShowPPDealerSelection();"
        ElseIf Not _showdetail Then
            e.Item.FindControl("lblView").Visible = False '_showdetail
        End If
    End Sub
#End Region


    
    Private Sub btnSearchOnSAP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchOnSAP.Click

    End Sub

    Protected Sub btnDnLoad_Click(sender As Object, e As EventArgs) Handles btnDnLoad.Click
        Dim data As ArrayList = CType(sHelper.GetSession(_sesData), ArrayList)

        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList, Optional ByVal isAll As Boolean = False)
        Dim sFileName As String
        sFileName = "SparePart" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim DepositAData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(DepositAData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(DepositAData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                'If isAll Then
                WriteAllSparePart(sw, data)
                'Else
                '    WriteSparePart(sw, data)
                'End If


                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal" & ex.Message)
        End Try
    End Sub

    Private Sub WriteAllSparePart(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Umum - Daftar Spare Part")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Nomor Barang" & tab)
            itemLine.Append("Nama Barang" & tab)
            itemLine.Append("Status" & tab)
            itemLine.Append("Model" & tab)
            itemLine.Append("Harga Eceran" & tab)
            itemLine.Append("Barang Pengganti" & tab)
            itemLine.Append("Aktif" & tab)
            sw.WriteLine(itemLine.ToString())

            'Dim i As Integer = 1
            'Dim DealerCode As String = ""
            Dim Number As Integer

            For Each item As SparePartMaster In data
                If item.RowStatus = 0 Then
                    Number += 1

                    Dim status As String = (New EnumSparePartActiveStatus).GetStringValue(item.ActiveStatus)

                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(Number.ToString & tab)
                    itemLine.Append(item.PartNumber & tab)
                    itemLine.Append(item.PartName & tab)
                    itemLine.Append(item.PartStatus & tab)
                    itemLine.Append(item.ModelCode & tab)
                    itemLine.Append(FormatNumber(item.RetalPrice, 2) & tab)
                    itemLine.Append(item.AltPartName & tab)
                    itemLine.Append(status & tab)

                    sw.WriteLine(itemLine.ToString())
                End If
            Next
        End If
    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        Dim arlData As ArrayList = CType(Me._sessHelper.GetSession(Me._sesProses), ArrayList)
        Dim arlToUpdate As New ArrayList
        Dim oSparePartMaster As SparePartMaster
        Dim oSparePartMasterFacade As New SparePartMasterFacade(User)
        Dim nFailed As Integer = 0

        For Each di As DataGridItem In Me.dtgSparePartMaster.Items
            Dim chkSparePart As CheckBox = di.FindControl("chkItemChecked")

            If chkSparePart.Checked Then
                oSparePartMaster = CType(arlData(di.ItemIndex + (dtgSparePartMaster.CurrentPageIndex * dtgSparePartMaster.PageSize)), SparePartMaster)
                oSparePartMaster.ActiveStatus = CType(EnumSparePartActiveStatus.GetEnumValue(ddlStatus.SelectedValue), Short)
                If oSparePartMasterFacade.Update(oSparePartMaster) = -1 Then
                    nFailed += 1
                End If
            End If
        Next

        If nFailed > 0 Then
            If Me.dtgSparePartMaster.Items.Count = nFailed Then
                MessageBox.Show("Data Gagal disimpan")
            Else
                MessageBox.Show("Data berhasil disimpan dan " & nFailed.ToString & " gagal.")
            End If
        Else
            MessageBox.Show("Data Berhasil disimpan")
        End If
        Me.BindDtgSparePartMaster(dtgSparePartMaster.CurrentPageIndex)
    End Sub

   
End Class
