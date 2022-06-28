#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmVehicleType
    Inherits System.Web.UI.Page
    Private m_bFormPrivilege As Boolean = False

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox
    Protected WithEvents TxtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgType As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlModel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlVehicleClass As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtMaxTOPDays As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkCriteriaTOP As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSegmentType As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvSegmentType As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtVariantType As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvVariantType As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtTransmitType As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvTransmitType As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDriveSystemType As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvDriveSystemType As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtSpeedType As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvSpeedType As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtFuelType As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvFuelType As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sHelper As New SessionHelper
    Private _vstTOP As String = "_vstTOP"
    Private _vstEdit As String = "_vstEdit"
    Private _vstSimpan As String = "_vstSimpan"

#Region "Custom Method"
    Private Sub SaveSearchCriterias()
        Dim objHash As Hashtable = SearchCriterias
        objHash.Remove(ddlCategory.ID)
        objHash.Add(ddlCategory.ID, ddlCategory.SelectedValue)

        objHash.Remove(ddlModel.ID)
        objHash.Add(ddlModel.ID, ddlModel.SelectedValue)

        objHash.Remove(ddlVehicleClass.ID)
        objHash.Add(ddlVehicleClass.ID, ddlVehicleClass.SelectedValue)

        objHash.Remove(txtKode.ID)
        objHash.Add(txtKode.ID, txtKode.Text)

        objHash.Remove(TxtDeskripsi.ID)
        objHash.Add(TxtDeskripsi.ID, TxtDeskripsi.Text)

        objHash.Remove(txtSegmentType.ID)
        objHash.Add(txtSegmentType.ID, txtSegmentType.Text)

        objHash.Remove(txtVariantType.ID)
        objHash.Add(txtVariantType.ID, txtVariantType.Text)

        objHash.Remove(txtTransmitType.ID)
        objHash.Add(txtTransmitType.ID, txtTransmitType.Text)

        objHash.Remove(txtDriveSystemType.ID)
        objHash.Add(txtDriveSystemType.ID, txtDriveSystemType.Text)

        objHash.Remove(txtSpeedType.ID)
        objHash.Add(txtSpeedType.ID, txtSpeedType.Text)

        objHash.Remove(txtFuelType.ID)
        objHash.Add(txtFuelType.ID, txtFuelType.Text)

        objHash.Remove(Me.txtMaxTOPDays.ID)
        objHash.Add(Me.txtMaxTOPDays.ID, Me.txtMaxTOPDays.Text)
    End Sub

    Private Sub RestoreCriterias()
        Dim objHash As Hashtable = SearchCriterias

        ddlCategory.SelectedValue = CStr(objHash(ddlCategory.ID))
        ddlModel.SelectedValue = objHash(ddlModel.ID)        
        ddlVehicleClass.SelectedValue = objHash(ddlVehicleClass.ID)
        txtKode.Text = objHash(txtKode.ID)
        TxtDeskripsi.Text = objHash(TxtDeskripsi.ID)
        txtSegmentType.Text = objHash(txtSegmentType.ID)
        txtVariantType.Text = objHash(txtVariantType.ID)
        txtTransmitType.Text = objHash(txtTransmitType.ID)
        txtDriveSystemType.Text = objHash(txtDriveSystemType.ID)
        txtSpeedType.Text = objHash(txtSpeedType.ID)
        txtFuelType.Text = objHash(txtFuelType.ID)
        Me.txtMaxTOPDays.Text = objHash(Me.txtMaxTOPDays.ID)
    End Sub
    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "VechileTypeCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    ''--Binding DataGrid
    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim cVT As New CriteriaComposite(New Criteria(GetType(VechileType), "ID", MatchType.Greater, 0))
            If IsNothing(sHelper.GetSession("CRITERIAS")) Then
                sHelper.SetSession("CRITERIAS", cVT)
            End If

            dtgType.DataSource = New VechileTypeFacade(User).RetrieveActiveList(CType(sHelper.GetSession("CRITERIAS"), CriteriaComposite), indexPage + 1, dtgType.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgType.VirtualItemCount = totalRow
            dtgType.DataBind()
        End If

    End Sub

    ''--Binding Data Down List Model
    Private Sub BindDdlModel()
        Dim listItemBlank As New ListItem("Silahkan Pilih", -1)
        Dim criteria As CriteriaComposite
        criteria = New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(VechileModel), "Category.ID", MatchType.Exact, CType(Me.ddlCategory.SelectedValue, Integer)))
        ddlModel.DataSource = New VechileModelFacade(User).RetrieveList("Description", Sort.SortDirection.ASC, criteria)
        ddlModel.DataValueField = "ID"
        ddlModel.DataTextField = "Description"
        ddlModel.DataBind()
        ddlModel.Items.Insert(0, listItemBlank)
    End Sub

    ''--Binding Data Down List Category
    Private Sub BindDdlCategory()
        Dim listItemBlank As New ListItem("Silahkan Pilih", -1)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveList("Description", Sort.SortDirection.ASC, criterias)
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataTextField = "Description"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, listItemBlank)
    End Sub

    ''--Clear Data
    Private Sub ClearData()
        txtKode.Enabled = True
        txtKode.Text = String.Empty
        TxtDeskripsi.Text = String.Empty
        txtSegmentType.Text = String.Empty
        txtVariantType.Text = String.Empty
        txtTransmitType.Text = String.Empty
        txtDriveSystemType.Text = String.Empty
        txtSpeedType.Text = String.Empty
        txtFuelType.Text = String.Empty

        Me.txtMaxTOPDays.Text = "0"
        'btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        txtKode.ReadOnly = False
        ddlCategory.Enabled = True
        ddlModel.Enabled = True
        ddlVehicleClass.Enabled = True
        TxtDeskripsi.ReadOnly = False
        txtSegmentType.ReadOnly = False
        txtVariantType.ReadOnly = False
        txtTransmitType.ReadOnly = False
        txtDriveSystemType.ReadOnly = False
        txtSpeedType.ReadOnly = False
        txtFuelType.ReadOnly = False

        Me.txtMaxTOPDays.ReadOnly = False
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal VechileTypeID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
                MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "VechileType", MatchType.Exact, VechileTypeID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    ''--Delete VechileType
    Private Sub DeleteVechileType(ByVal nID As Integer)

        If New HelperFacade(User, GetType(VechileColor)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(VechileColor), nID), _
            CreateAggreateForCheckRecord(GetType(VechileColor))) Then

            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(nID)
            'objVechileType.RowStatus = DBRowStatus.Deleted
            Dim facade As VechileTypeFacade = New VechileTypeFacade(User)
            facade.DeleteFromDB(objVechileType)
            dtgType.CurrentPageIndex = 0
            BindDatagrid(dtgType.CurrentPageIndex)
        End If

        'If Not checkIDVechileColor(nID) Then
        '    Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(nID)
        '    objVechileType.RowStatus = DBRowStatus.Deleted
        '    Dim facade As VechileTypeFacade = New VechileTypeFacade(User)
        '    facade.Delete(objVechileType)
        '    dtgType.CurrentPageIndex = 0
        '    BindDatagrid(dtgType.CurrentPageIndex)
        'Else
        '    MessageBox.Show("Tidak dapat dihapus referensi tabel lain")
        'End If
    End Sub

    Private Sub Activate(ByVal nID As Integer)
        Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(nID)
        Dim facade As VechileTypeFacade = New VechileTypeFacade(User)
        objVechileType.Status = "A"
        facade.Update(objVechileType)
        dtgType.CurrentPageIndex = 0
        BindDatagrid(dtgType.CurrentPageIndex)
        ActivateCompetitorType(objVechileType)
    End Sub

    Private Sub DeActivate(ByVal nID As Integer)
        Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(nID)
        Dim facade As VechileTypeFacade = New VechileTypeFacade(User)
        objVechileType.Status = "X"
        facade.Update(objVechileType)
        dtgType.CurrentPageIndex = 0
        BindDatagrid(dtgType.CurrentPageIndex)
        DeActivateCompetitorType(objVechileType)
    End Sub

    ''--Update VechileType
    Private Sub UpdateVechileType()
        Dim objVechileType As VechileType = CType(Session.Item("vsVechileType"), VechileType)
        objVechileType.VechileModel = New VechileModelFacade(User).Retrieve(CType(ddlModel.SelectedValue, Integer))
        objVechileType.VehicleClass = New DealerReport.VehicleClassFacade(User).Retrieve(CInt(ddlVehicleClass.SelectedValue))
        objVechileType.Category = New CategoryFacade(User).Retrieve(CType(ddlCategory.SelectedValue, Integer))
        objVechileType.Description = TxtDeskripsi.Text
        objVechileType.SegmentType = txtSegmentType.Text
        objVechileType.VariantType = txtVariantType.Text
        objVechileType.TransmitType = txtTransmitType.Text
        objVechileType.DriveSystemType = txtDriveSystemType.Text
        objVechileType.SpeedType = txtSpeedType.Text
        objVechileType.FuelType = txtFuelType.Text

        objVechileType.MaxTOPDays = Me.txtMaxTOPDays.Text
        Dim nResult = New VechileTypeFacade(User).Update(objVechileType)
    End Sub

    ''--View Vechile Type
    Private Sub ViewVechileType(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsVechileType", objVechileType)
        txtKode.Text = objVechileType.VechileTypeCode
        ddlCategory.SelectedValue = objVechileType.Category.ID
        BindDdlModel()
        Dim checkDdlModel = ddlModel.Items.FindByValue(objVechileType.VechileModel.ID)
        If Not IsNothing(checkDdlModel) Then
            ddlModel.SelectedValue = objVechileType.VechileModel.ID
            If Not objVechileType.VehicleClass Is Nothing Then
                ddlVehicleClass.SelectedValue = objVechileType.VehicleClass.ID
            End If

            TxtDeskripsi.Text = objVechileType.Description
            txtSegmentType.Text = objVechileType.SegmentType
            txtVariantType.Text = objVechileType.VariantType
            txtTransmitType.Text = objVechileType.TransmitType
            txtDriveSystemType.Text = objVechileType.DriveSystemType
            txtSpeedType.Text = objVechileType.SpeedType
            txtFuelType.Text = objVechileType.FuelType

            Me.txtMaxTOPDays.Text = objVechileType.MaxTOPDays
            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show("Kategori yang di pilih tidak mempunyai tipe model " & objVechileType.VechileModel.Description)
        End If
    End Sub

    'update Competitor Type
    Private Sub DeActivateCompetitorType(ByVal objDomain As VechileType)
        Dim code As String = objDomain.VechileTypeCode
        Dim objCompetitorType As CompetitorType = New CompetitorTypeFacade(User).Retrieve(code)
        objCompetitorType.Status = EnumStatusSPL.StatusSPL.Tidak_Aktif
        Dim result As Integer = New CompetitorTypeFacade(User).Update(objCompetitorType)
    End Sub

    Private Sub ActivateCompetitorType(ByVal objDomain As VechileType)
        Dim code As String = objDomain.VechileTypeCode
        Dim objCompetitorType As CompetitorType = New CompetitorTypeFacade(User).Retrieve(code)
        objCompetitorType.Status = EnumStatusSPL.StatusSPL.Aktif
        Dim result As Integer = New CompetitorTypeFacade(User).Update(objCompetitorType)
    End Sub

    Private Sub BindDdlClass()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(VehicleClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(VehicleClass), "Status", MatchType.Exact, CType(EnumStatusSPL.StatusSPL.Aktif, Short)))
        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(VehicleClass), "Description", Sort.SortDirection.ASC))
        Dim arl As ArrayList = New DealerReport.VehicleClassFacade(User).Retrieve(criterias, sortColl)
        ddlVehicleClass.DataSource = arl
        ddlVehicleClass.DataValueField = "ID"
        ddlVehicleClass.DataTextField = "Description"
        ddlVehicleClass.DataBind()

        ddlVehicleClass.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub
#End Region

#Region "EventHandler"

    ''--Page Loading
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDdlCategory()
            BindDdlModel()
            BindDdlClass()
            InitiatePage()
            'BindDatagrid(0)
        End If
    End Sub
    Private Sub SetControlPrivilege()
        Dim m_bFormPrivilege_ori As Boolean = m_bFormPrivilege
        If Me.ViewState.Item(Me._vstSimpan) = "1" Then m_bFormPrivilege = True
        btnSimpan.Visible = m_bFormPrivilege
        'btnBatal.Visible = m_bFormPrivilege
        m_bFormPrivilege = m_bFormPrivilege_ori
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ChangeType_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ViewType_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - TIPE")
        End If
        Me.ViewState.Add(Me._vstTOP, "0")
        Me.ViewState.Add(Me._vstEdit, "0")
        Me.ViewState.Add(Me._vstSimpan, "0")
        If SecurityProvider.Authorize(Context.User, SR.Ubah_tipe_hari_TOP_privilege) Then
            Me.ViewState.Item(Me._vstTOP) = "1"
        End If

        If SecurityProvider.Authorize(Context.User, SR.Ubah_tipe_kendaraan) Then
            Me.ViewState.Item(Me._vstEdit) = "1"
        End If

        If SecurityProvider.Authorize(Context.User, SR.Simpan_tipe_kendaraan) Then
            Me.ViewState.Item(Me._vstSimpan) = "1"
        End If

    End Sub

    ''--Button Simpan Click
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        If Not Page.IsValid Then
            Return
        End If
        Dim objVechileType As VechileType = New VechileType
        Dim objVechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
        Dim nResult As Integer = -1
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim categoryArr As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
        Dim valid As Boolean = False
        For Each item As Category In categoryArr
            If ddlCategory.SelectedValue = item.ID Then
                valid = True
            End If
        Next


        If valid Then
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                If objVechileTypeFacade.ValidateCode(txtKode.Text) <= 0 Then
                    objVechileType.VechileTypeCode = txtKode.Text
                    objVechileType.Status = "A"
                    objVechileType.VechileModel = New VechileModelFacade(User).Retrieve(CType(ddlModel.SelectedValue, Integer))
                    objVechileType.VehicleClass = New DealerReport.VehicleClassFacade(User).Retrieve(CInt(ddlVehicleClass.SelectedValue))
                    objVechileType.Category = New CategoryFacade(User).Retrieve(CType(ddlCategory.SelectedValue, Integer))
                    objVechileType.Description = TxtDeskripsi.Text
                    objVechileType.MaxTOPDays = Me.txtMaxTOPDays.Text
                    nResult = New VechileTypeFacade(User).Insert(objVechileType)
                    If nResult = -1 Then
                        MessageBox.Show("Simpan Gagal")
                    Else
                        MessageBox.Show("Simpan Sukses")
                    End If
                Else
                    MessageBox.Show("Kode Sudah Ada / Pernah Dihapus")
                End If
            Else
                UpdateVechileType()
            End If
            If Not ViewState("vsProcess") Is Nothing Then
                If ViewState("vsProcess").ToString() = "Edit" Then
                    RestoreCriterias()
                End If
            End If
            ClearData()
            dtgType.SelectedIndex = -1
            dtgType.CurrentPageIndex = 0
            BindDatagrid(dtgType.CurrentPageIndex)
        Else
            MessageBox.Show("Kategori tidak sesuai")
        End If
    End Sub

    ''--Datagrid Type ItemDataBound
    Private Sub dtgType_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgType.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As VechileType = CType(e.Item.DataItem, VechileType)
            Dim m_bFormPrivilege_ori As Boolean = m_bFormPrivilege
            If Me.ViewState.Item(Me._vstEdit) = "1" Then
                m_bFormPrivilege = True
            End If

            'tambahan privilege
            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
            End If

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
            End If

            If Not e.Item.FindControl("linkButonActive") Is Nothing Then
                CType(e.Item.FindControl("linkButonActive"), LinkButton).Visible = m_bFormPrivilege
            End If

            If Not e.Item.FindControl("LinkButtonNonActive") Is Nothing Then
                CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton).Visible = m_bFormPrivilege
            End If


            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            CType(e.Item.FindControl("linkButonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diAktifkan?');")
            CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diNonAktifkan?');")
            Dim actLb As LinkButton = CType(e.Item.FindControl("linkButonActive"), LinkButton)
            Dim nactLb As LinkButton = CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton)
            actLb.Visible = False
            nactLb.Visible = False

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            If RowValue.Status.ToUpper = "X" Then
                actLb.Visible = m_bFormPrivilege
                lblStatus.Text = "<img src='../images/in-aktif.gif' border='0' title='In Aktif'>"
            Else
                nactLb.Visible = m_bFormPrivilege
                lblStatus.Text = "<img src='../images/aktif.gif' border='0' title='Aktif'>"
            End If
            m_bFormPrivilege = m_bFormPrivilege_ori
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgType.CurrentPageIndex * dtgType.PageSize)
        End If




    End Sub



    ''--datagrid Type ItemCommand
    Private Sub dtgType_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgType.ItemCommand
        Dim IsEnabled As Boolean = True
        Dim IsTOP As Boolean = Not (Me.ViewState.Item(Me._vstTOP) = "1")
        
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewVechileType(e.Item.Cells(0).Text, False)
            '
            txtKode.ReadOnly = True
            ddlCategory.Enabled = False
            ddlModel.Enabled = False
            ddlVehicleClass.Enabled = False
            TxtDeskripsi.ReadOnly = True
            txtSegmentType.ReadOnly = True
            txtVariantType.ReadOnly = True
            txtTransmitType.ReadOnly = True
            txtDriveSystemType.ReadOnly = True
            txtSpeedType.ReadOnly = True
            txtFuelType.ReadOnly = True

            Me.txtMaxTOPDays.ReadOnly = True
        ElseIf e.CommandName = "Edit" Then
            SaveSearchCriterias()
            ViewState.Add("vsProcess", "Edit")
            ViewVechileType(e.Item.Cells(0).Text, True)
            dtgType.SelectedIndex = e.Item.ItemIndex
            '
            txtKode.ReadOnly = True
            ddlCategory.Enabled = Me.m_bFormPrivilege '  True
            ddlModel.Enabled = Me.m_bFormPrivilege ' True
            ddlVehicleClass.Enabled = Me.m_bFormPrivilege ' True
            TxtDeskripsi.ReadOnly = Not Me.m_bFormPrivilege ' False
            txtSegmentType.ReadOnly = Not Me.m_bFormPrivilege
            txtVariantType.ReadOnly = Not Me.m_bFormPrivilege
            txtTransmitType.ReadOnly = Not Me.m_bFormPrivilege
            txtDriveSystemType.ReadOnly = Not Me.m_bFormPrivilege
            txtSpeedType.ReadOnly = Not Me.m_bFormPrivilege
            txtFuelType.ReadOnly = Not Me.m_bFormPrivilege

            Me.txtMaxTOPDays.ReadOnly = IsTOP ' False
        ElseIf e.CommandName = "Delete" Then
            DeleteVechileType(e.Item.Cells(0).Text)
            '
            txtKode.ReadOnly = Not Me.m_bFormPrivilege ' False
            ddlCategory.Enabled = Me.m_bFormPrivilege ' True
            ddlModel.Enabled = Me.m_bFormPrivilege ' True
            TxtDeskripsi.ReadOnly = Not Me.m_bFormPrivilege '  False
            txtSegmentType.ReadOnly = Not Me.m_bFormPrivilege
            txtVariantType.ReadOnly = Not Me.m_bFormPrivilege
            txtTransmitType.ReadOnly = Not Me.m_bFormPrivilege
            txtDriveSystemType.ReadOnly = Not Me.m_bFormPrivilege
            txtSpeedType.ReadOnly = Not Me.m_bFormPrivilege
            txtFuelType.ReadOnly = Not Me.m_bFormPrivilege

            Me.txtMaxTOPDays.ReadOnly = IsTOP ' False
            ClearData()
        ElseIf e.CommandName = "Activate" Then
            Activate(e.Item.Cells(0).Text)
            txtKode.ReadOnly = Not Me.m_bFormPrivilege '  False 
            ddlCategory.Enabled = Me.m_bFormPrivilege ' True
            ddlModel.Enabled = Me.m_bFormPrivilege '  True
            TxtDeskripsi.ReadOnly = Not Me.m_bFormPrivilege ' False
            txtSegmentType.ReadOnly = Not Me.m_bFormPrivilege
            txtVariantType.ReadOnly = Not Me.m_bFormPrivilege
            txtTransmitType.ReadOnly = Not Me.m_bFormPrivilege
            txtDriveSystemType.ReadOnly = Not Me.m_bFormPrivilege
            txtSpeedType.ReadOnly = Not Me.m_bFormPrivilege
            txtFuelType.ReadOnly = Not Me.m_bFormPrivilege

            Me.txtMaxTOPDays.ReadOnly = IsTOP 'False
            ClearData()
        ElseIf e.CommandName = "Deactivate" Then
            DeActivate(e.Item.Cells(0).Text)

            txtKode.ReadOnly = Not Me.m_bFormPrivilege ' False
            ddlCategory.Enabled = Me.m_bFormPrivilege 'True
            ddlModel.Enabled = Me.m_bFormPrivilege '  True
            TxtDeskripsi.ReadOnly = Not Me.m_bFormPrivilege ' False
            txtSegmentType.ReadOnly = Not Me.m_bFormPrivilege
            txtVariantType.ReadOnly = Not Me.m_bFormPrivilege
            txtTransmitType.ReadOnly = Not Me.m_bFormPrivilege
            txtDriveSystemType.ReadOnly = Not Me.m_bFormPrivilege
            txtSpeedType.ReadOnly = Not Me.m_bFormPrivilege
            txtFuelType.ReadOnly = Not Me.m_bFormPrivilege
            Me.txtMaxTOPDays.ReadOnly = IsTOP '  False
            ClearData()
        End If

    End Sub

    'Private Function checkIDVechileColor(ByVal nID As Integer) As Boolean
    '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.ID", MatchType.Exact, nID))
    '    Dim arlVechileColor As ArrayList = New VechileColorFacade(User).Retrieve(criterias)
    '    Return arlVechileColor.Count > 0
    'End Function

    ''--Button Tutup Click

    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewState.Clear()
        Response.Redirect("../default.aspx")
    End Sub

    ''--Button Batal Click
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgType_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgType.PageIndexChanged
        'Try
        '    dtgType.SelectedIndex = -1
        dtgType.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgType.CurrentPageIndex)
        ClearData()
        'Catch ex As Exception
        '    dtgType.SelectedIndex = 0
        '    dtgType.CurrentPageIndex = e.NewPageIndex
        '    BindDatagrid(dtgType.CurrentPageIndex)
        '    ClearData()
        'End Try
    End Sub

    Private Sub dtgType_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgType.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        dtgType.SelectedIndex = -1
        dtgType.CurrentPageIndex = 0
        BindDatagrid(dtgType.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub ddlModel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlModel.SelectedIndexChanged
        If ddlModel.SelectedValue < 0 Or ddlCategory.SelectedValue < 0 Then
            btnSimpan.Enabled = False
        Else
            btnSimpan.Enabled = True
        End If
    End Sub

#End Region

    Private ReadOnly Property SearchCriterias() As Hashtable
        Get
            If ViewState("SearchCriterias") Is Nothing Then
                Dim objHash As Hashtable = New Hashtable
                
                ViewState("SearchCriterias") = objHash
            End If

            Return CType(ViewState("SearchCriterias"), Hashtable)
        End Get
    End Property


    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        If ddlModel.SelectedValue < 0 Or ddlCategory.SelectedValue < 0 Then
            btnSimpan.Enabled = False
        Else
            btnSimpan.Enabled = True
        End If
        BindDdlModel()
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        sHelper.SetSession("CRITERIAS", criterias)
        dtgType.CurrentPageIndex = 0
        BindDatagrid(dtgType.CurrentPageIndex)
    End Sub

    'Private Sub btnSimpan_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles btnSimpan.Command

    'End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        If ddlCategory.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        If ddlModel.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "VechileModel.ID", MatchType.Exact, ddlModel.SelectedValue))
        End If
        If ddlVehicleClass.SelectedValue > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "VehicleClass.ID", MatchType.Exact, ddlVehicleClass.SelectedValue))
        End If
        If txtKode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "VechileTypeCode", MatchType.Exact, txtKode.Text.Trim))
        End If
        If TxtDeskripsi.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Description", MatchType.[Partial], TxtDeskripsi.Text.Trim))
        End If
        If txtSegmentType.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "SegmentType", MatchType.Exact, txtSegmentType.Text.Trim))
        End If
        If txtVariantType.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "VariantType", MatchType.Exact, txtVariantType.Text.Trim))
        End If
        If txtTransmitType.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "TransmitType", MatchType.Exact, txtTransmitType.Text.Trim))
        End If
        If txtDriveSystemType.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "DriveSystemType", MatchType.Exact, txtDriveSystemType.Text.Trim))
        End If
        If txtSpeedType.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "SpeedType", MatchType.Exact, txtSpeedType.Text.Trim))
        End If
        If txtFuelType.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "FuelType", MatchType.Exact, txtFuelType.Text.Trim))
        End If
        Dim n As Integer
        Try
            n = Me.txtMaxTOPDays.Text
        Catch ex As Exception
            n = 0
        End Try
        If Me.chkCriteriaTOP.Checked = True Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "MaxTOPDays", MatchType.Exact, n))
        End If
    End Sub
End Class