Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Math

#Region "Custom NameSpace"
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessValidation
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.Linq
Imports System.Collections.Generic

#End Region

Public Class FrmSalesmanHeader
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents chkLuarNegri As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblKotaLahir As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPlaceOfBirth As System.Web.UI.WebControls.TextBox
    Protected WithEvents photoView As System.Web.UI.WebControls.Image
    Protected WithEvents photoSrc As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents dtgArea As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgTraining As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgExperience As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgPrestasi As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtJobPositionAdd As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgTarget As System.Web.UI.WebControls.DataGrid
    Protected WithEvents SalesUnitIndicator As System.Web.UI.WebControls.CheckBox
    Protected WithEvents MechanicIndicator As System.Web.UI.WebControls.CheckBox
    Protected WithEvents SparePartIndicator As System.Web.UI.WebControls.CheckBox
    Protected WithEvents AdmIndicator As System.Web.UI.WebControls.CheckBox
    Protected WithEvents WHIndicator As System.Web.UI.WebControls.CheckBox
    Protected WithEvents CounterIndicator As System.Web.UI.WebControls.CheckBox
    Protected WithEvents SalesIndicator As System.Web.UI.WebControls.CheckBox
    Protected WithEvents pnlSparePart As System.Web.UI.WebControls.Panel
    Protected WithEvents btnRemoveFile As System.Web.UI.WebControls.Button
    Protected WithEvents ICDateOfBirth As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICStartWork As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICEndWork As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlGender As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtAlamat As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlSalesmanLevel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnRequestID As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblTraining As System.Web.UI.WebControls.Label
    Protected WithEvents lblPengalaman As System.Web.UI.WebControls.Label
    Protected WithEvents lblPrestasi As System.Web.UI.WebControls.Label
    Protected WithEvents lblTarget As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtResignReason As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents ddlMarriedStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSalesmanCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamat As System.Web.UI.WebControls.Label
    Protected WithEvents lblGender As System.Web.UI.WebControls.Label
    Protected WithEvents lblMarriedStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblJobPositionDesc As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesmanLevel As System.Web.UI.WebControls.Label
    Protected WithEvents lblStartWork As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndWork As System.Web.UI.WebControls.Label
    Protected WithEvents lblResignReason As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblPlaceOfBirth As System.Web.UI.WebControls.Label
    Protected WithEvents lblDateOfBirth As System.Web.UI.WebControls.Label
    Protected WithEvents valName As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidatorForPhotoSrc As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator7 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlPropinsi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPropinsi As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKota As System.Web.UI.WebControls.DropDownList
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlJobPositionDesc As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator8 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblRemoveImage As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlIndicator As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblIndicator As System.Web.UI.WebControls.Label
    Protected WithEvents Requiredfieldvalidator9 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblRef As System.Web.UI.WebControls.Label
    Protected WithEvents lnkReloadSalesman As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnRefSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents txtRefSalesman As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMode As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents dtg As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgHistory As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblAreaPemasaran As System.Web.UI.WebControls.Label
    Protected WithEvents TitleHistory As System.Web.UI.WebControls.Label
    Protected WithEvents lblAreaSingleTitle As System.Web.UI.WebControls.Label
    Protected WithEvents DdlArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Areafieldvalidator As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblArea As System.Web.UI.WebControls.Label
    Protected WithEvents lblAreasd As System.Web.UI.WebControls.Label
    Protected WithEvents txtJobPosition As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblSuperior As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSuperior As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnlSuperior As System.Web.UI.WebControls.Panel
    Protected WithEvents txtSuperiorName As System.Web.UI.WebControls.TextBox
    Protected WithEvents hdnVal As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnKotaLahir As System.Web.UI.WebControls.HiddenField
    Protected WithEvents IsInsertSuccess As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ValAtasan As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlDealerBranch As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblBranch As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerBranch As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        SetProfile()

        objDealerTmp = sessHelper.GetSession("DEALER")
        Dim isOnlyUploadPhoto = False
        If objDealerTmp.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealerTmp.DealerCode)
            If Not IsNothing(dealerSystems) Then
                isOnlyUploadPhoto = dealerSystems.isOnlyUploadPhotoTenagaPenjual
                sessHelper.SetSession("isOnlyUploadPhoto", isOnlyUploadPhoto)
            End If
        End If

        If Request.QueryString("ID") = String.Empty Then
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve(strCurrProfile), EnumProfileType.ProfileType.SALESMAN, Panel1)
        Else
            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("ID")))
            RenderProfilePanel(objSalesmanHeader, New ProfileGroupFacade(User).Retrieve(strCurrProfile), EnumProfileType.ProfileType.SALESMAN, Panel1)
        End If


    End Sub

#End Region

#Region "PrivateVariables"
    Private _SalesmanHeaderFacade As New SalesmanHeaderFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper

    'Dim arrHistoryToInsert As New ArrayList
    'Dim arrHistoryToDelete As New ArrayList
    Dim arrHistory As New ArrayList
    Dim arrArea As New ArrayList
    Dim arrExp As New ArrayList
    Dim arrTraining As New ArrayList
    Dim arrAccomp As New ArrayList
    Dim arrTarget As New ArrayList
    Dim blnSaved As Boolean
    Dim strCurrProfile As String
    Dim strIdSManCode As String
    Dim strIdBManCode As String
    Dim intIdLevelBlank As Integer
    Private objDealerTmp As Dealer

    Private isErrorWhileUpload As Boolean = False

#End Region

#Region "PrivateCustomMethods"
    Private Sub SetSetting()
        If Not IsNothing(Request.QueryString("Mode")) Then
            Select Case Request.QueryString("Mode")
                Case "unit"
                    lblPageTitle.Text = "SALESMAN DATABASE - Entry Salesman"
                    ddlIndicator.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Unit
                    lblKodeSalesman.Text = "Kode Salesman"
                    'CR
                    TitleHistory.Visible = True
                    dtgHistory.Visible = True
                    lblAreaSingleTitle.Visible = True
                    lblAreasd.Visible = True
                    'lblDealerBranch.Visible = True
                    'lblBranch.Visible = True
                    'DdlArea.Visible = True
                    lblAreaPemasaran.Visible = False
                    dtgArea.Visible = False
                    Areafieldvalidator.Enabled = True
                    pnlSuperior.Visible = True


                Case "part"
                    lblPageTitle.Text = "SALESMAN PART DATABASE - Salesman Part"
                    ddlIndicator.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Sparepart
                    lblKodeSalesman.Text = "Kode Salesman Part"
                    'CR
                    TitleHistory.Visible = False
                    dtgHistory.Visible = False
                    lblAreaSingleTitle.Visible = False
                    lblAreasd.Visible = False
                    DdlArea.Visible = False
                    ddlDealerBranch.Visible = False
                    lblDealerBranch.Visible = False
                    lblBranch.Visible = False
                    lblAreaPemasaran.Visible = True
                    dtgArea.Visible = True
                    Areafieldvalidator.Enabled = False
                    lblArea.Visible = False
                    pnlSuperior.Visible = False


                Case "servis"
                    lblPageTitle.Text = "MECHANIC DATABASE - Staff Service"
                    ddlIndicator.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Mekanik
                    lblKodeSalesman.Text = "Kode Staff Service"
                    'CR
                    TitleHistory.Visible = False
                    dtgHistory.Visible = False
                    lblAreaSingleTitle.Visible = False
                    lblAreasd.Visible = False
                    DdlArea.Visible = False
                    ddlDealerBranch.Visible = False
                    lblDealerBranch.Visible = False
                    lblBranch.Visible = False
                    lblAreaPemasaran.Visible = True
                    dtgArea.Visible = True
                    Areafieldvalidator.Enabled = False
                    lblArea.Visible = False
                    pnlSuperior.Visible = False


            End Select
            ddlIndicator.Enabled = False
            txtMode.Value = ddlIndicator.SelectedValue
        Else
            lblPageTitle.Text = "SALESMAN DATABASE - Entry Salesman ..."

            'CR
            TitleHistory.Visible = True
            dtgHistory.Visible = True
            lblAreaSingleTitle.Visible = True
            lblAreasd.Visible = True
            'DdlArea.Visible = True
            lblAreaPemasaran.Visible = False
            dtgArea.Visible = False
            Areafieldvalidator.Enabled = True
            pnlSuperior.Visible = True
            ddlDealerBranch.Visible = True
            'lblDealerBranch.Visible = True
            'lblBranch.Visible = True

        End If
    End Sub

    Private Sub SetProfile()
        If Not IsNothing(Request.QueryString("Mode")) Then
            Select Case Request.QueryString("Mode")
                Case "unit"
                    strCurrProfile = "sals_dbs_unit"
                Case "part"
                    strCurrProfile = "sals_dbs_parts"
                Case "servis"
                    strCurrProfile = "sals_dbs_mekanik"
            End Select
        Else
            strCurrProfile = "sals_dbs_unit" ' default
            'strCurrProfile = "sals_dbs_1" ' default
        End If
    End Sub

    Private Sub SetLabel(ByVal Visible As Boolean)
        lblTarget.Visible = Visible
        lblAreaPemasaran.Visible = Visible
        lblTraining.Visible = Visible
        lblPengalaman.Visible = Visible
        lblPrestasi.Visible = Visible
    End Sub

    Private Sub BindDealerBranch(ByVal objDealer As Dealer)
        CommonFunction.BindDealerBranchByDealerID(ddlDealerBranch, User, objDealer.ID, True)
    End Sub

    Private Sub BindDropDown()

        CommonFunction.BindFromEnum("SalesmanGender", ddlGender, User, False, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("SalesmanStatus", ddlStatus, User, False, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("MarriedStatus", ddlMarriedStatus, User, False, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("SalesmanUnit", ddlIndicator, User, False, "NameStatus", "ValStatus")
        'modified by anh July 26,2010 re by rna
        If Not IsNothing(Request.QueryString("Menu")) Then
            Dim iMenu As Integer = CType(Request.QueryString("Menu"), Integer)
            If iMenu > 0 Then
                CommonFunction.BindJobPositionByMenuAssigned(ddlJobPositionDesc, User, True, iMenu)
            End If
        Else
            CommonFunction.BindJobPosition(ddlJobPositionDesc, User, True, False)
        End If
        'end modified
        CommonFunction.BindProvince(ddlPropinsi, User, True, False)

        CommonFunction.BindSalesmanLevel(ddlSalesmanLevel, User, True)

        Dim arlArea As ArrayList = New SalesmanAreaFacade(User).RetrieveList("AreaCode", Sort.SortDirection.ASC)

        DdlArea.DataTextField = "AreaDesc"
        DdlArea.DataValueField = "ID"
        DdlArea.DataSource = arlArea
        DdlArea.DataBind()
        DdlArea.Items.Insert(0, New ListItem("Silahkan Pilih", ""))
        DdlArea.SelectedIndex = 0

        ddlGender.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlMarriedStatus.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        'ddlIndicator.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlGender.SelectedIndex = 0
        ddlMarriedStatus.SelectedIndex = 0

    End Sub

    Private Sub BindDgTarget(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanSalesTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanSalesTarget), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))

        arrTarget = New SalesmanSalesTargetFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dtgTarget.PageSize, totalRow, _
        sessHelper.GetSession("SortColTarget"), sessHelper.GetSession("SortDirectionTarget"))

        dtgTarget.CurrentPageIndex = idxPage
        dtgTarget.DataSource = arrTarget
        dtgTarget.VirtualItemCount = totalRow
        dtgTarget.DataBind()

    End Sub
    Private Sub BindDgPrestasi(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAccomplish), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanAccomplish), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))

        arrAccomp = New SalesmanAccomplishFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dtgPrestasi.PageSize, totalRow, _
        sessHelper.GetSession("SortColPrestasi"), sessHelper.GetSession("SortDirectionPrestasi"))

        dtgPrestasi.CurrentPageIndex = idxPage
        dtgPrestasi.DataSource = arrAccomp
        dtgPrestasi.VirtualItemCount = totalRow
        dtgPrestasi.DataBind()

    End Sub
    Private Sub BindDgTraining(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanTraining), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanTraining), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))

        arrTraining = New SalesmanTrainingFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dtgTraining.PageSize, totalRow, _
         sessHelper.GetSession("SortColTraining"), sessHelper.GetSession("SortDirectionTraining"))

        dtgTraining.CurrentPageIndex = idxPage
        dtgTraining.DataSource = arrTraining
        dtgTraining.VirtualItemCount = totalRow
        dtgTraining.DataBind()

    End Sub
    Private Sub BindDgExperience(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanExperience), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanExperience), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))

        arrExp = New SalesmanExperienceFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dtgTraining.PageSize, totalRow, _
        sessHelper.GetSession("SortColExperience"), sessHelper.GetSession("SortDirectionExperience"))

        dtgExperience.CurrentPageIndex = idxPage
        dtgExperience.DataSource = arrExp
        dtgExperience.VirtualItemCount = totalRow
        dtgExperience.DataBind()

    End Sub

    Private Sub BindDgHistory(ByVal idxPage As Integer)

        If CType(Request.QueryString("id"), Integer) < 0 Or Request.QueryString("id") = String.Empty Then
            dtgHistory.DataSource = sessHelper.GetSession("arrHistoryToInsert")
            dtgHistory.DataBind()

        Else

            Dim totalRow As Integer = 0

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanDealerHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanDealerHistory), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))

            arrHistory = New SalesmanDealerHistoryFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dtgHistory.PageSize, totalRow, _
            sessHelper.GetSession("SortColHistory"), sessHelper.GetSession("SortDirectionHistory"))

            dtgHistory.CurrentPageIndex = idxPage
            dtgHistory.DataSource = arrHistory
            dtgHistory.VirtualItemCount = totalRow
            dtgHistory.DataBind()
        End If



    End Sub

    Private Sub BindDgArea(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))

        arrArea = New SalesmanAreaAssignFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dtgArea.PageSize, totalRow, _
        sessHelper.GetSession("SortColArea"), sessHelper.GetSession("SortDirectionArea"))

        dtgArea.CurrentPageIndex = idxPage
        dtgArea.DataSource = arrArea
        dtgArea.VirtualItemCount = totalRow
        dtgArea.DataBind()

    End Sub
    Private Sub BindControlsAttribute()
        lbtnRefSalesman.Attributes("onclick") = "ShowSalesmanResign();"
    End Sub
    Private Function CheckValidation() As Boolean
        Dim blnValid As Boolean = True

        If txtName.Text = "" Then
            blnValid = False
            MessageBox.Show("Silakan input nama Salesman terlebih dahulu")
            Return (blnValid)
        End If

        If txtPlaceOfBirth.Text = "" Then
            blnValid = False
            MessageBox.Show("Silakan input tempat lahir Salesman terlebih dahulu")
            Return (blnValid)
        End If

        If txtAlamat.Text = "" Then
            blnValid = False
            MessageBox.Show("Silakan input alamat Salesman terlebih dahulu")
            Return (blnValid)
        End If

        If ddlKota.SelectedValue = "" Then
            blnValid = False
            MessageBox.Show("Silakan pilih kota Salesman terlebih dahulu")
            Return (blnValid)
        Else
            Dim arrCity As ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, ddlKota.SelectedItem.Text))
            arrCity = New CityFacade(User).Retrieve(criterias)

            If Not IsNothing(arrCity) Then
                If arrCity.Count <= 0 Then
                    blnValid = False
                    MessageBox.Show("Silakan masukan data Kota yg valid, gunakan list")
                    Return (blnValid)
                End If
            End If
        End If

        If ddlMarriedStatus.SelectedValue = "" Then
            blnValid = False
            MessageBox.Show("Silakan pilih Status Pernikahan Salesman terlebih dahulu")
            Return (blnValid)
        End If

        If ddlJobPositionDesc.SelectedValue = "" Then
            blnValid = False
            MessageBox.Show("Silakan dipilih posisi Salesman terlebih dahulu")
            Return (blnValid)
        Else
            Dim arrTmp As ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(JobPosition), "Description", MatchType.Exact, ddlJobPositionDesc.SelectedItem.Text))
            arrTmp = New JobPositionFacade(User).Retrieve(criterias)

            If Not IsNothing(arrTmp) Then
                If arrTmp.Count <= 0 Then
                    blnValid = False
                    MessageBox.Show("Silakan masukan data Posisi jabatan yg valid, gunakan list")
                    Return (blnValid)
                End If
            End If
        End If


        If (ICDateOfBirth.Value = ICStartWork.Value) Then
            blnValid = False
            MessageBox.Show("Tanggal Lahir tidak boleh sama dengan Tanggal Masuk")
            Return (blnValid)
        End If

        If (ICStartWork.Value < ICDateOfBirth.Value) Then
            blnValid = False
            MessageBox.Show("Tanggal Masuk tidak boleh lebih kecil dari Tanggal Lahir")
            Return (blnValid)
        End If

        If (ddlSalesmanLevel.SelectedValue = "") And (ddlSalesmanLevel.Enabled) Then
            blnValid = False
            MessageBox.Show("Salesman level belum dipilih")
            Return (blnValid)
        End If

        Dim alGroup1 As New ArrayList
        Dim objRenderPanel As New RenderingProfile

        alGroup1 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(strCurrProfile), CType(EnumProfileType.ProfileType.SALESMAN, Short), User, True)
        For Each oCrp As SalesmanProfile In alGroup1
            If oCrp.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar AndAlso (IsNothing(oCrp.ProfileValue) OrElse oCrp.ProfileValue.Trim = "") Then
                MessageBox.Show(oCrp.ProfileHeader.Description.ToString & " Harus diisi")
                Exit Function
            End If
            If oCrp.ProfileHeader.Code = "NOKTP" Then
                If oCrp.ProfileValue.Length < 10 Then
                    MessageBox.Show("No KTP tidak benar")
                    Exit Function
                End If
            End If
            If oCrp.ProfileHeader.Code = "NOTELP" Then
                If (oCrp.ProfileValue.Length < 6 Or oCrp.ProfileValue.Length > 16) OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                    MessageBox.Show("No Telp tidak benar")
                    Exit Function
                End If

                If Not (oCrp.ProfileValue.StartsWith("0") Or oCrp.ProfileValue.StartsWith("62")) Then
                    MessageBox.Show("No Telp tidak benar")
                    Exit Function
                End If

                Dim isNumeric As Integer
                If Not Integer.TryParse(oCrp.ProfileValue, isNumeric) Then
                    MessageBox.Show("No Telp tidak benar")
                    Exit Function
                End If

            End If
            If oCrp.ProfileHeader.Code = "NO_HP" Then
                'If (oCrp.ProfileValue.Length < 8 Or oCrp.ProfileValue.Length > 16) OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                '    MessageBox.Show("Panjang karakter No HP harus lebih dari 6 digit dan kurang dari 16 digit")
                '    Exit Function
                'End If

                'If Not (oCrp.ProfileValue.StartsWith("08")) Then
                '    MessageBox.Show("No HP tidak benar. isi dengan format 08xxx")
                '    Exit Function
                'End If

                'If Not IsNumeric(oCrp.ProfileValue) Then
                '    MessageBox.Show("No HP tidak benar. isi dengan angka")
                '    Exit Function
                'End 
                'CR Salesman
                If (oCrp.ProfileValue.Substring(0, 2) <> "08") Then
                    MessageBox.Show("Mohon isi Nomor HP yang sesuai, format input No. Telp  : 08XXXXXXXXX")
                    Exit Function
                End If
                If Not IsNumeric(oCrp.ProfileValue) Then
                    MessageBox.Show("No HP tidak benar. isi dengan angka")
                    Exit Function
                End If
                If oCrp.ProfileValue.Length < 10 Then
                    MessageBox.Show("No HP minimum 10 karakter")
                    Exit Function
                End If
                'end CR Salesman

            End If
        Next

        Return blnValid

    End Function
    Private Sub UpperControl(ByVal blnIsUpper As Boolean)
        CommonFunction.UpperTextControl(txtName, blnIsUpper)
        CommonFunction.UpperTextControl(txtPlaceOfBirth, blnIsUpper)
        CommonFunction.UpperTextControl(txtAlamat, blnIsUpper)
    End Sub
    ' keperluan untuk set variable from session
    Private Sub SetVarFromSession()
        If Not IsNothing(sessHelper.GetSession("strIdSManCode")) Then
            strIdSManCode = CType(sessHelper.GetSession("strIdSManCode"), String)
        End If
        If Not IsNothing(sessHelper.GetSession("strIdBManCode")) Then
            strIdBManCode = CType(sessHelper.GetSession("strIdBManCode"), String)
        End If
        If Not IsNothing(sessHelper.GetSession("intIdLevelBlank")) Then
            intIdLevelBlank = CType(sessHelper.GetSession("intIdLevelBlank"), Integer)
        End If
    End Sub
    ' keperluan untuk membuat salesman level bayangan
    Private Sub GetIdLevelBlank()
        Dim arrSalesmanLevel As ArrayList
        Dim crits As New CriteriaComposite(New Criteria(GetType(SalesmanLevel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Deleted, Short)))
        crits.opAnd(New Criteria(GetType(SalesmanLevel), "Description", MatchType.Exact, ""))
        arrSalesmanLevel = New SalesmanHeaderFacade(User).RetrieveSalesmanLevel(crits)

        For Each item As SalesmanLevel In arrSalesmanLevel
            sessHelper.SetSession("intIdLevelBlank", item.ID)
            Exit For
        Next
    End Sub
    ' penambahan untuk initialize data
    Private Sub ClearData()
        lblSalesmanCode.Text = ""
        lblDealerBranch.Text = ""
        txtName.Text = ""
        txtPlaceOfBirth.Text = ""
        txtResignReason.Text = ""
        'txtLeadJobPositionDesc.Text = ""
        txtJobPosition.Value = ""
        'txtLeadJobPosition.Value = ""
        txtAlamat.Text = ""
        'txtLeaderName.Text = ""
        'txtJumlahToko.Text = ""
        ddlDealerBranch.SelectedIndex = -1
        ddlGender.SelectedIndex = -1
        ddlSalesmanLevel.SelectedIndex = -1
        ddlMarriedStatus.SelectedIndex = -1
        ddlPropinsi.SelectedIndex = -1
        ddlKota.SelectedIndex = -1
        'ddlIndicator.SelectedIndex = -1

        ICDateOfBirth.Value = Date.Now
        ICStartWork.Value = Date.Now
        ICEndWork.Value = Date.Now

        photoView.Visible = False

        'SalesUnitIndicator.Checked = False
        'MechanicIndicator.Checked = False
        'SparePartIndicator.Checked = False

        AdmIndicator.Checked = False
        WHIndicator.Checked = False
        CounterIndicator.Checked = False
        SalesIndicator.Checked = False

        btnSimpan.Enabled = True
        blnSaved = False

        ViewState.Add("vsProcess", "Insert")
    End Sub
    ' untuk update data yg sdh ada sebelumnya
    Private Sub Update()

        Dim objSalesmanHeader As New SalesmanHeader
        Dim objJPosition As New JobPosition
        Dim objJobPosition As New JobPosition

        objSalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader" + CInt(Request.QueryString("id")).ToString()), SalesmanHeader)
        If objSalesmanHeader Is Nothing Then
            Dim objSalesmanHeaderFacade As New SalesmanHeaderFacade(User)
            objSalesmanHeader = objSalesmanHeaderFacade.Retrieve(CInt(Request.QueryString("id")))
        End If
        Dim isNew As Boolean = True
        Dim objAdditionalIfo As New SalesmanAdditionalInfo
        If objSalesmanHeader.SalesmanAdditionalInfo.Count > 0 Then
            objAdditionalIfo = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo)
            isNew = False
        End If

        With objSalesmanHeader

            .Dealer = New DealerFacade(User).Retrieve(lblDealerCode.Text)
            .SalesmanCode = lblSalesmanCode.Text
            If ddlDealerBranch.SelectedIndex > 0 Then
                .DealerBranch = New DealerBranchFacade(User).Retrieve(CInt(ddlDealerBranch.SelectedValue))
            Else
                .DealerBranch = Nothing
            End If
            .Name = txtName.Text
            .PlaceOfBirth = txtPlaceOfBirth.Text

            If String.IsNullorEmpty(hdnKotaLahir.Value) Then
                Try
                    Dim crits As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, txtPlaceOfBirth.Text))
                    crits.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))
                    hdnKotaLahir.Value = CType(New CityFacade(Me.User).Retrieve(crits)(0), City).CityCode

                    objAdditionalIfo.BirthCity = New CityFacade(Me.User).Retrieve(hdnKotaLahir.Value)
                Catch
                End Try
            End If

            .DateOfBirth = ICDateOfBirth.Value
            .HireDate = ICStartWork.Value
            .SalesIndicator = CType(ddlIndicator.SelectedValue, Byte)
            .SalesmanArea = New SalesmanAreaFacade(User).Retrieve(CInt(Val(DdlArea.SelectedValue)))
            objJPosition = New JobPositionFacade(User).Retrieve(CType(ddlJobPositionDesc.SelectedValue, Integer))
            If Not objJPosition Is Nothing Then
                .JobPosition = objJPosition
            End If

            If txtSuperior.Text = "" Then
                .LeaderId = 0
            Else
                Dim objSuperior As SalesmanHeader = New SalesmanHeaderFacade(User).RetrieveByCode(txtSuperior.Text.Trim)
                .LeaderId = objSuperior.ID
            End If

            If Not IsNothing(objJobPosition) Then
                .JobPositionId_Leader = objJobPosition.ID
            End If

            .Gender = ddlGender.SelectedValue
            .MarriedStatus = ddlMarriedStatus.SelectedValue
            .Address = txtAlamat.Text
            .City = ddlKota.SelectedItem.Text
            objAdditionalIfo.AddressCity = New CityFacade(Me.User).Retrieve(CInt(ddlKota.SelectedValue))

            If .Status <> EnumSalesmanStatus.SalesmanStatus.Baru Then
                If ddlStatus.SelectedValue = EnumSalesmanStatus.SalesmanStatus.Baru Then
                    MessageBox.Show("Status tidak bisa kembali ke status Baru lagi, harus unregister dahulu")
                    Return
                End If
            End If
            If (lblSalesmanCode.Text = "") And (ddlStatus.SelectedValue <> EnumSalesmanStatus.SalesmanStatus.Baru) Then
                MessageBox.Show("Salesman yang bersangkutan harus diregister terlebih dahulu, untuk mengaktifkannya")
                Return
            End If
            If ddlStatus.SelectedValue = EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif AndAlso .Status <> EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif Then
                MessageBox.Show("Perubahan Status menjadi Tidak aktif hanya melalui pengunduran diri")
                Return
            End If

            .Status = ddlStatus.SelectedValue
            If (ddlJobPositionDesc.SelectedValue = strIdSManCode) Or (ddlJobPositionDesc.SelectedValue = strIdBManCode) Then
                ' handle untuk bug 894
                .SalesmanLevel = New SalesmanHeaderFacade(User).RetrieveSalesmanLevelByID(intIdLevelBlank)
            Else
                .SalesmanLevel = New SalesmanHeaderFacade(User).RetrieveSalesmanLevelByID(ddlSalesmanLevel.SelectedValue)
            End If

            Dim blnRemovePic As Boolean
            If Not IsNothing(sessHelper.GetSession("RemovePic")) Then
                blnRemovePic = sessHelper.GetSession("RemovePic")
                If blnRemovePic = True Then
                    .Image = Nothing
                End If
            End If

            ' penambahan image menggunakan method conversi
            If Not IsNothing(sessHelper.GetSession(IMGUPLOAD)) Then
                .Image = CType(sessHelper.GetSession(IMGUPLOAD), Byte())
                lblRemoveImage.Visible = False
                lblRemoveImage.Enabled = False
                sessHelper.RemoveSession(IMGUPLOAD)
            Else
                lblRemoveImage.Visible = False
                lblRemoveImage.Enabled = False
            End If

            If (isErrorWhileUpload = True) Then
                Return
            End If

            Dim isThereExistingImage As Boolean = CheckIfThereIsExistingImage()

            If (Not .Image Is Nothing Or isThereExistingImage = True) Then
                RequiredFieldValidatorForPhotoSrc.Enabled = False
                RequiredFieldValidatorForPhotoSrc.Visible = False
            Else
                'RequiredFieldValidatorForPhotoSrc.Enabled = True
                'RequiredFieldValidatorForPhotoSrc.Visible = True
                'MessageBox.Show("Scan KTP harus diisi!")
                'Return

                RequiredFieldValidatorForPhotoSrc.Enabled = False
                RequiredFieldValidatorForPhotoSrc.Visible = False

            End If
        End With

        Dim nresult As Integer = -1
        Dim arrTmp As ArrayList
        Dim objRenderPanel As RenderingProfile = New RenderingProfile
        ' sebelum melakukan update, pastikan data ProfileHeaderToGroup sdh ada / belum
        Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
        If Not IsNothing(objProfileGroup) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, objProfileGroup.ID))
            arrTmp = New ProfileHeaderToGroupFacade(User).RetrieveByCriteria(criterias)

            If Not IsNothing(arrTmp) Then
                If arrTmp.Count > 0 Then
                    Dim al As ArrayList = objRenderPanel.RetrieveProfileValue(Me, objProfileGroup, CType(EnumProfileType.ProfileType.SALESMAN, Short), User)
                    nresult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader, al, New ProfileGroupFacade(User).Retrieve(strCurrProfile))
                Else
                    Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

                    If vr.IsValid = False Then
                        MessageBox.Show(vr.Message)
                        Exit Sub
                    End If

                    nresult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
                End If
            Else

                Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

                If vr.IsValid = False Then
                    MessageBox.Show(vr.Message)
                    Exit Sub
                End If

                nresult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
            End If
        End If

        photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & Request.QueryString("id") & "&type=" & "SalesmanHeader"
        If nresult = -1 Then
            MessageBox.Show(SR.UpdateFail)
        Else
            Dim crits As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, objSalesmanHeader.PlaceOfBirth))
            crits.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

            Dim crits2 As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits2.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, objSalesmanHeader.City))
            crits2.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

            objAdditionalIfo.AddressCity = CType(New CityFacade(Me.User).Retrieve(crits2)(0), City)

            Try
                objAdditionalIfo.BirthCity = CType(New CityFacade(Me.User).Retrieve(crits)(0), City)
            Catch
            End Try
            If isNew Then
                objAdditionalIfo.SalesmanHeader = objSalesmanHeader
                nresult = New SalesmanAdditionalInfoFacade(Me.User).Insert(objAdditionalIfo)
            Else
                nresult = New SalesmanAdditionalInfoFacade(Me.User).Update(objAdditionalIfo)
            End If

            Dim crits3 As New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits3.opAnd(New Criteria(GetType(SalesmanDSE), "SalesmanHeader.ID", MatchType.Exact, objAdditionalIfo.SalesmanHeader.ID))
            crits3.opAnd(New Criteria(GetType(SalesmanDSE), "Dealer.ID", MatchType.Exact, objAdditionalIfo.SalesmanHeader.Dealer.ID))

            Dim funcdse As New SalesmanDSEFacade(Me.User)
            Dim arrDSE As ArrayList = funcdse.Retrieve(crits3)
            If arrDSE.Count > 0 Then
                Dim objDSE As SalesmanDSE = CType(arrDSE(0), SalesmanDSE)
                objDSE.PhoneNumber = objAdditionalIfo.SalesmanHeader.PhoneNumber
                funcdse.Update(objDSE)
            End If

            MessageBox.Show("Data berhasil diupdate !")
        End If
    End Sub
    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "Name"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Insert")
        Dim objJobPosition As New JobPosition

        objJobPosition = New JobPositionFacade(User).Retrieve(KTB.DNet.Lib.WebConfig.GetValue("SManCode"))
        If Not objJobPosition Is Nothing Then
            sessHelper.SetSession("strIdSManCode", objJobPosition.ID.ToString)
            strIdSManCode = objJobPosition.ID.ToString
        End If

        objJobPosition = New JobPositionFacade(User).Retrieve(KTB.DNet.Lib.WebConfig.GetValue("BManCode"))
        If Not objJobPosition Is Nothing Then
            sessHelper.SetSession("strIdBManCode", objJobPosition.ID.ToString)
            strIdBManCode = objJobPosition.ID.ToString
        End If

        GetIdLevelBlank()
    End Sub
    ' penambahan untuk delete data
    Private Sub Delete(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(nID)
        Dim facade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Dim iReturn As Integer = -1
        'iReturn = facade.DeleteTransaction(objSalesmanArea)
        iReturn = facade.DeleteFromDB(objSalesmanHeader)
        If iReturn <= 0 Then
            MessageBox.Show("Record Gagal Dihapus")
        End If

    End Sub
    ' penambahan untuk view data
    Private Sub View(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objSalesmanHeaderFacade As New SalesmanHeaderFacade(User)
        Dim objSalesmanHeader As SalesmanHeader = objSalesmanHeaderFacade.Retrieve(nID)
        Dim isOnlyUploadPhoto As Boolean = CType(sessHelper.GetSession("isOnlyUploadPhoto"), Boolean)

        If EditStatus = True And objSalesmanHeader.IsRequestID = 0 Then
            btnRequestID.Enabled = True
        ElseIf EditStatus = False AndAlso isOnlyUploadPhoto AndAlso objSalesmanHeader.IsRequestID = 0 Then
            btnRequestID.Enabled = True
        Else
            btnRequestID.Enabled = False
        End If

        If EditStatus = True And Not (IsNothing(objSalesmanHeader.Image)) Then
            lblRemoveImage.Visible = False
            lblRemoveImage.Enabled = False
        Else
            lblRemoveImage.Visible = False
            lblRemoveImage.Enabled = False
        End If

        'Todo session
        'Session.Add("vsSalesmanHeader", objSalesmanHeader)
        sessHelper.SetSession("vsSalesmanHeader" + CInt(Request.QueryString("id")).ToString(), objSalesmanHeader)
        With objSalesmanHeader
            'Dim objDealer As Dealer = New DealerFacade(User).Retrieve(.Dealer.ID)
            If objSalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif Then
                Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
                If objuser.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    BindDealerBranch(objuser.Dealer)
                Else
                    BindDealerBranch(objSalesmanHeader.Dealer)
                End If
            Else
                BindDealerBranch(objSalesmanHeader.Dealer)
            End If

            lblSalesmanCode.Text = .SalesmanCode
            If Not IsNothing(.DealerBranch) Then
                If objSalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif Then
                    ddlDealerBranch.SelectedIndex = -1
                    lblDealerBranch.Text = ""
                Else
                    ddlDealerBranch.SelectedValue = .DealerBranch.ID
                    lblDealerBranch.Text = ddlDealerBranch.SelectedItem.Text
                End If
            Else
                ddlDealerBranch.SelectedIndex = -1
                lblDealerBranch.Text = ""
            End If
            If EditStatus Then
                lblDealerBranch.Visible = Not EditStatus
            Else
                lblDealerBranch.Visible = EditStatus
            End If
            lblName.Text = .Name
            lblArea.Text = .SalesmanArea.AreaDesc
            lblPlaceOfBirth.Text = .PlaceOfBirth
            lblDateOfBirth.Text = .DateOfBirth
            lblStartWork.Text = .HireDate

            If Not IsNothing(Request.QueryString("Konfirmasi")) Then
                ' mengambil dari user yang login
                lblDealerCode.Text = objDealerTmp.DealerCode
                lblDealerName.Text = objDealerTmp.DealerName
            Else
                lblDealerCode.Text = .Dealer.DealerCode
                lblDealerName.Text = .Dealer.DealerName
            End If

            txtName.Text = .Name
            txtPlaceOfBirth.Text = .PlaceOfBirth

            Dim objSalesmanAdd As SalesmanAdditionalInfo = New SalesmanAdditionalInfoFacade(Me.User).RetrieveBySalesmanHeader(.ID)
            If objSalesmanAdd.ID = 0 Then
                chkLuarNegri.Checked = True
                lblKotaLahir.Visible = False
            Else
                chkLuarNegri.Checked = False
                lblKotaLahir.Visible = True
            End If


            ICDateOfBirth.Value = .DateOfBirth
            ICStartWork.Value = .HireDate
            ddlIndicator.SelectedValue = .SalesIndicator
            lblIndicator.Text = ddlIndicator.SelectedItem.Text
            ddlGender.SelectedValue = .Gender
            lblGender.Text = ddlGender.SelectedItem.Text
            ddlMarriedStatus.SelectedValue = .MarriedStatus
            lblMarriedStatus.Text = ddlMarriedStatus.SelectedItem.Text
            lblAlamat.Text = .Address
            lblKota.Text = .City

            If .LeaderId > 0 Then
                Dim objSuperior As SalesmanHeader = objSalesmanHeaderFacade.Retrieve(.LeaderId)
                If objSuperior.Status = EnumSalesmanStatus.SalesmanStatus.Aktif Then
                    txtSuperior.Text = objSuperior.SalesmanCode
                    txtSuperiorName.Text = objSuperior.Name
                End If
            End If




            DdlArea.SelectedValue = .SalesmanArea.ID.ToString

            Dim objSalesmanHeaderLead As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(.LeaderId)

            txtAlamat.Text = .Address

            ' mengambil data propinsi yg bersangkutan
            Dim objCity As City
            Dim objCityFacade As CityFacade = New CityFacade(User)
            objCity = objCityFacade.Retrieve(.City, 1)

            If Not objCity Is Nothing Then
                ddlPropinsi.SelectedValue = objCity.Province.ID
                ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
                lblPropinsi.Text = ddlPropinsi.SelectedItem.Text

                ddlKota.SelectedValue = objCity.ID
            End If

            ddlStatus.SelectedValue = .Status.ToString
            lblStatus.Text = ddlStatus.SelectedItem.Text

            Dim objJobPosition As JobPosition = New JobPositionFacade(User).Retrieve(.JobPosition.ID)
            If Not IsNothing(objJobPosition) Then
                If objJobPosition.RowStatus = DBRowStatus.Active Then
                    txtJobPosition.Value = objJobPosition.Code
                    ddlJobPositionDesc.SelectedValue = objJobPosition.ID
                    lblJobPositionDesc.Text = objJobPosition.Description
                Else
                    txtJobPosition.Value = ""
                    ddlJobPositionDesc.SelectedIndex = -1
                    lblJobPositionDesc.Text = ""
                End If
            End If

            If .SalesmanLevel.RowStatus = CType(DBRowStatus.Active, Short) Then
                ddlSalesmanLevel.SelectedValue = .SalesmanLevel.ID.ToString
                lblSalesmanLevel.Text = ddlSalesmanLevel.SelectedItem.Text
            Else
                lblSalesmanLevel.Text = ""
            End If

            Dim dtmResignNull As DateTime = New DateTime(1753, 1, 1)

            ICEndWork.Value = IIf(.ResignDate = dtmResignNull, Nothing, .ResignDate)
            txtResignReason.Text = .ResignReason

            lblEndWork.Text = IIf(.ResignDate = dtmResignNull, String.Empty, .ResignDate)
            lblResignReason.Text = .ResignReason

            ' Show image
            photoView.Visible = True
            photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & nID & "&type=" & "SalesmanHeader"

        End With

        Me.btnSimpan.Enabled = EditStatus
        EnablingControl(EditStatus)


    End Sub
    Private Sub EnablingControl(ByVal isEnabled As Boolean)
        chkLuarNegri.Enabled = isEnabled
        txtName.Enabled = isEnabled
        txtAlamat.Enabled = isEnabled
        txtPlaceOfBirth.Enabled = isEnabled
        ICDateOfBirth.Enabled = isEnabled
        ddlGender.Enabled = isEnabled
        ddlMarriedStatus.Enabled = isEnabled
        photoSrc.Disabled = Not isEnabled
        ddlKota.Enabled = isEnabled
        ddlDealerBranch.Enabled = isEnabled
        ddlJobPositionDesc.Enabled = isEnabled
        ddlStatus.Enabled = isEnabled
        ICStartWork.Enabled = isEnabled
        ICDateOfBirth.Enabled = isEnabled
        DdlArea.Enabled = isEnabled
        If (ddlJobPositionDesc.SelectedValue = strIdSManCode) Or (ddlJobPositionDesc.SelectedValue = strIdBManCode) Then
            ddlSalesmanLevel.Enabled = False
            Requiredfieldvalidator6.Enabled = False ' untuk handle salesman level validator
            ValAtasan.Enabled = False
        Else
            ddlSalesmanLevel.Enabled = isEnabled
            Requiredfieldvalidator6.Enabled = isEnabled
            ValAtasan.Enabled = isEnabled
        End If

        txtSuperior.Enabled = isEnabled

        ICEndWork.Enabled = False
        txtResignReason.Enabled = False

        dtgHistory.Columns(4).Visible = isEnabled
        dtgHistory.ShowFooter = isEnabled

        dtgTarget.Columns(4).Visible = isEnabled    ' kolom aksi
        dtgTarget.ShowFooter = isEnabled

        dtgArea.Columns(3).Visible = isEnabled      ' kolom aksi
        dtgArea.ShowFooter = isEnabled

        dtgTraining.Columns(4).Visible = isEnabled  ' kolom aksi
        dtgTraining.ShowFooter = isEnabled

        dtgExperience.Columns(4).Visible = isEnabled    ' kolom aksi
        dtgExperience.ShowFooter = isEnabled

        dtgPrestasi.Columns(3).Visible = isEnabled      ' kolom aksi
        dtgPrestasi.ShowFooter = isEnabled


    End Sub
    'Private Sub InsertImage()
    '    Dim oPhisingGuardImage As SalesmanHeader = New SalesmanHeader
    '    If photoSrc.PostedFile.FileName = String.Empty Then
    '        MessageBox.Show("Tidak ada file gambar")
    '        Return  '-- No photo defined
    '    End If

    '    '-- Split into array of strings. The last element is the file name
    '    Dim sFileNames() As String = photoSrc.PostedFile.FileName.Split("\")
    '    Dim sFileName As String = sFileNames(sFileNames.Length - 1)

    '    Try
    '        Dim imageFile As Byte()
    '        imageFile = UploadFile()

    '        oPhisingGuardImage.Image = imageFile
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    'End Sub
    Private Sub VisibleControl(ByVal blnVisible As Boolean)
        txtName.Visible = blnVisible
        DdlArea.Visible = blnVisible
        txtPlaceOfBirth.Visible = blnVisible
        ICDateOfBirth.Visible = blnVisible
        ddlGender.Visible = blnVisible
        ddlMarriedStatus.Visible = blnVisible
        txtAlamat.Visible = blnVisible
        ddlPropinsi.Visible = blnVisible
        ddlKota.Visible = blnVisible
        ddlDealerBranch.Visible = blnVisible
        ddlSalesmanLevel.Visible = blnVisible
        ddlJobPositionDesc.Visible = blnVisible
        ICStartWork.Visible = blnVisible
        ICEndWork.Visible = blnVisible
        txtResignReason.Visible = blnVisible
        ddlStatus.Visible = blnVisible
        ddlIndicator.Visible = blnVisible

        lblRef.Visible = blnVisible
        txtRefSalesman.Visible = blnVisible
        lbtnRefSalesman.Visible = blnVisible
        lnkReloadSalesman.Visible = blnVisible
        '---
        lblName.Visible = Not blnVisible
        lblArea.Visible = Not blnVisible
        lblDealerBranch.Visible = Not blnVisible
        lblPlaceOfBirth.Visible = Not blnVisible
        lblDateOfBirth.Visible = Not blnVisible
        lblGender.Visible = Not blnVisible
        lblMarriedStatus.Visible = Not blnVisible
        lblAlamat.Visible = Not blnVisible
        lblPropinsi.Visible = Not blnVisible
        lblKota.Visible = Not blnVisible
        lblSalesmanLevel.Visible = Not blnVisible
        lblJobPositionDesc.Visible = Not blnVisible
        lblStartWork.Visible = Not blnVisible
        lblEndWork.Visible = Not blnVisible
        lblResignReason.Visible = Not blnVisible
        lblStatus.Visible = Not blnVisible
        lblIndicator.Visible = Not blnVisible
        Label4.Visible = blnVisible

        ' yang status konfirmasi hanya bs simpan, tp change status
        If Not IsNothing(Request.QueryString("Konfirmasi")) Then
            If blnVisible = True Then
                ddlStatus.SelectedValue = EnumSalesmanStatus.SalesmanStatus.Konfirmasi
                ddlStatus.Enabled = False
                txtRefSalesman.Text = lblSalesmanCode.Text
            End If
        Else
            If ddlStatus.SelectedValue = CType(EnumSalesmanStatus.SalesmanStatus.Konfirmasi, String) Then
                ddlStatus.Enabled = False
            End If
        End If
    End Sub
    Private Function UploadFile(ByVal uploadedFile As HttpPostedFile) As Byte()
        Dim nResult() As Byte
        isErrorWhileUpload = False

        Try
            ' If IsValidPhoto(photoSrc.PostedFile) Then
            Dim inStream As System.IO.Stream = uploadedFile.InputStream()
            Dim ByteRead(SalesmanHeader.MAX_PHOTO_SIZE) As Byte
            Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, SalesmanHeader.MAX_PHOTO_SIZE)
            If ReadCount = 0 Then
                Throw New InvalidConstraintException(SR.DataNotFound("Photo"))
            End If
            ReDim nResult(ReadCount)
            Array.Copy(ByteRead, nResult, ReadCount)
            ' Else
            'MessageBox.Show("Bukan file gambar atau Ukuran file > " & _
            'CType(SalesmanHeader.MAX_PHOTO_SIZE / 1024, String) & "KB. Gambar gagal diupload.")
            'End If
        Catch
            'Throw
            isErrorWhileUpload = True
        End Try

        Return nResult
    End Function
    ' untuk keperluan penyimpanan photo
    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(SalesmanHeader.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= SalesmanHeader.MAX_PHOTO_SIZE)
        Return (containImage AndAlso sizeValid)
    End Function
    Private Sub SetSession()
        sessHelper.SetSession("SortColHistory", "DateIn")
        sessHelper.SetSession("SortDirectionHistory", Sort.SortDirection.DESC)

        sessHelper.SetSession("SortColArea", "SalesmanArea.AreaCode")
        sessHelper.SetSession("SortDirectionArea", Sort.SortDirection.ASC)
        sessHelper.SetSession("SortColExperience", "YearExperience")
        sessHelper.SetSession("SortDirectionExperience", Sort.SortDirection.ASC)
        sessHelper.SetSession("SortColPrestasi", "")
        sessHelper.SetSession("SortDirectionPrestasi", Sort.SortDirection.ASC)
        sessHelper.SetSession("SortColTarget", "")
        sessHelper.SetSession("SortDirectionTarget", Sort.SortDirection.ASC)
        sessHelper.SetSession("SortColTraining", "")
        sessHelper.SetSession("SortDirectionTraining", Sort.SortDirection.ASC)
        sessHelper.SetSession("RemovePic", False)
    End Sub

#End Region

#Region "EventHandlers"
    'Dim objTmpSalesmanHeader As SalesmanHeader

    Private Sub RenderProfilePanel(ByVal objSalesmanHeader As SalesmanHeader, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel)
        Dim isReadOnly As Boolean

        If Request.QueryString("ID") = String.Empty Or Request.QueryString("ID") = "" Then
            isReadOnly = False
        Else
            If Request.QueryString("edit") = String.Empty Or Request.QueryString("edit") = "" Then
                isReadOnly = True
            Else
                Dim isOnlyUploadPhoto As Boolean = CType(sessHelper.GetSession("isOnlyUploadPhoto"), Boolean)

                If isOnlyUploadPhoto Then
                    isReadOnly = True
                Else
                    isReadOnly = False
                End If
            End If
        End If

        Dim objRenderPanel As RenderingProfile = New RenderingProfile(isReadOnly)

        If Not objSalesmanHeader Is Nothing Then
            objRenderPanel.GeneratePanel(objSalesmanHeader.ID, objPanel, objGroup, profileType, User, isReadOnly)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User, isReadOnly)
        End If
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()

        If Val(sessHelper.GetSession("IsInsertSuccess")) = 1 Then
            IsInsertSuccess.Value = "1"
            sessHelper.SetSession("IsInsertSuccess", "")
        Else
            IsInsertSuccess.Value = ""
            sessHelper.SetSession("IsInsertSuccess", "")
        End If

        If Not IsPostBack Then
            SetSession()
            BindDropDown()
            BindControlsAttribute()
            Initialize()
            objDealerTmp = sessHelper.GetSession("DEALER")

            Dim isOnlyUploadPhoto As Boolean = CType(sessHelper.GetSession("isOnlyUploadPhoto"), Boolean)

            If CType(Request.QueryString("id"), Integer) < 0 Or Request.QueryString("id") = String.Empty Then
                ViewState("vsProcess") = "Insert"
                btnRequestID.Visible = False
                lblRemoveImage.Visible = False
                ddlStatus.Visible = False
                btnBatal.Visible = True
                Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
                lblDealerCode.Text = objuser.Dealer.DealerCode
                lblDealerName.Text = objuser.Dealer.DealerName
                BindDealerBranch(objuser.Dealer)

                SetLabel(False)

                sessHelper.SetSession("arrHistoryToInsert", New ArrayList)

                BindDgHistory(0)

                If blnSaved Then
                    MessageBox.Show(SR.SaveSuccess & ", Silakan melengkapi data lainnya")
                End If
            Else
                SetLabel(True)

                If Request.QueryString("edit") <> String.Empty Then
                    ViewState("vsProcess") = "Edit"

                    If isOnlyUploadPhoto Then
                        View(CInt(Request.QueryString("id")), False)
                        VisibleControl(False)
                        photoSrc.Disabled = False
                        btnSimpan.Enabled = True
                    Else
                        View(CInt(Request.QueryString("id")), True)
                        VisibleControl(True)
                    End If

                    ' case bug 1393, 1a.tipe Unit & user Dealer, beberapa yg tdk bs diedit
                    If Request.QueryString("Mode") = "unit" Then
                        If objDealerTmp.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                            txtName.ReadOnly = True

                            'Tambahan 11-11-2020 untuk merubah kota lahir jika tidak terdaftam di master kota
                            If Not String.IsNullorEmpty(Request.QueryString("Konfirmasi")) Then
                                Dim critCity As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                critCity.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, txtPlaceOfBirth.Text))
                                critCity.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

                                Dim arrCity As ArrayList = New CityFacade(Me.User).Retrieve(critCity)
                                If arrCity.Count = 0 Then
                                    txtPlaceOfBirth.ReadOnly = False
                                Else
                                    txtPlaceOfBirth.ReadOnly = True
                                End If
                            Else
                                txtPlaceOfBirth.ReadOnly = True
                            End If

                            'txtPlaceOfBirth.ReadOnly = True 
                            ICDateOfBirth.Enabled = False
                            ICStartWork.Enabled = False
                        End If
                    End If
                Else
                    ViewState("vsProcess") = "View"
                    View(CInt(Request.QueryString("id")), False)
                    VisibleControl(False)
                End If
                btnBatal.Visible = False
                sessHelper.SetSession("HeaderID", Request.QueryString("ID"))
                BindDgHistory(0)
                BindDgArea(0)
                BindDgExperience(0)
                BindDgTraining(0)
                BindDgPrestasi(0)
                BindDgTarget(0)

            End If
        Else
            SetImage()
        End If



        SetSetting()

        If Val(hdnVal.Value) = 1 Then
            hdnVal.Value = "0"
            ProcessSave()
        End If

        If Me.IsDealer() Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(DealerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerCategory), "Dealer.ID", MatchType.Exact, objDealerTmp.ID))
            criterias.opAnd(New Criteria(GetType(DealerCategory), "Category.ProductCategory.ID", MatchType.Exact, 1))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(DealerCategory), "Category.ID", Sort.SortDirection.ASC))

            Dim arrDealerCtg As ArrayList = New DealerCategoryFacade(Me.User).Retrieve(criterias, sortColl)
            Dim valueSet As String = String.Empty
            Try
                Dim ddl As DropDownList = Panel1.FindControl("DDLIST88_13")
                For Each iItem As ListItem In ddl.Items
                    If iItem.Selected Then
                        valueSet = iItem.Value
                        Exit For
                    End If
                Next
                ddl.ClearSelection()
                ddl.Items.Clear()
                ddl.Items.Add(New ListItem("Silahkan Pilih", ""))
                For Each iCtg As DealerCategory In arrDealerCtg
                    ddl.Items.Add(New ListItem(iCtg.Category.CategoryCode, iCtg.Category.CategoryCode))
                Next
                If Not String.IsNullorEmpty(valueSet) Then
                    If Not IsNothing(ddl.Items.FindByValue(valueSet)) Then
                        ddl.Items.FindByValue(valueSet).Selected = True
                    End If
                End If

            Catch
            End Try
        End If

    End Sub

    Private Function GetListDDL(ByVal ctrl As Control) As List(Of DropDownList)
        Dim rest As New List(Of DropDownList)
        If ctrl.HasControls() Then
            For Each iCtrl As Control In ctrl.Controls
                rest.AddRange(GetListDDL(iCtrl))
            Next
        Else
            If TypeOf ctrl Is System.Web.UI.WebControls.DropDownList Then
                rest.Add(CType(ctrl, DropDownList))
            End If
        End If

        Return rest
    End Function

    Const IMGUPLOAD As String = "ImageUpload"
    Const POSTEDFILE As String = "PostedFile"

    Private Sub SetImage()
        'Get image dulu karena akan hilang jika ada postback lagi dibawah

        If Not IsNothing(photoSrc.PostedFile) Then
            If (photoSrc.PostedFile.FileName <> String.Empty) Then
                sessHelper.SetSession(POSTEDFILE, photoSrc.PostedFile)
            End If
        End If

    End Sub

    Private Sub ddlPropinsi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPropinsi.SelectedIndexChanged
        If ddlPropinsi.SelectedValue <> "" Then
            CommonFunction.BindCity(ddlKota, User, True, ddlPropinsi.SelectedValue, False)
        Else
            ddlKota.Items.Clear()
        End If
    End Sub
    Private Sub dtgArea_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgArea.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblPopUpSalesManArea"), Label)
            lblPopUp.Attributes("onclick") = "ShowPopUpSalesManArea();"

        End If

        If e.Item.ItemIndex <> -1 Then
            If Not (arrArea Is Nothing) Then
                Dim objSalesmanAreaAssign As SalesmanAreaAssign
                objSalesmanAreaAssign = arrArea(e.Item.ItemIndex)

                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgArea.CurrentPageIndex * dtgArea.PageSize)

                Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                _lbtnDelete.CommandArgument = objSalesmanAreaAssign.ID

            End If
        End If

    End Sub
    Private Sub dtgArea_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgArea.ItemCommand

        If e.CommandName = "delete" Then
            Dim facade As SalesmanAreaAssignFacade = New SalesmanAreaAssignFacade(User)
            Dim objSalesmanAreaAssign As SalesmanAreaAssign = facade.Retrieve(CInt(e.CommandArgument))
            Dim result As Integer = facade.DeleteFromDB(objSalesmanAreaAssign)
            BindDgArea(0)

        End If

        If e.CommandName = "add" Then
            Dim facade As SalesmanAreaAssignFacade = New SalesmanAreaAssignFacade(User)
            Dim txtAreaCode As TextBox = e.Item.FindControl("txtAreaCode")
            If txtAreaCode.Text = "" Then
                MessageBox.Show("Isi Kode Area Dulu !")
                Return
            End If

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAreaAssign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))
            criterias.opAnd(New Criteria(GetType(SalesmanAreaAssign), "SalesmanArea.AreaCode", MatchType.Exact, txtAreaCode.Text))

            Dim arlSalesmanAreaAssign As ArrayList = facade.RetrieveByCriteria(criterias)

            If arlSalesmanAreaAssign.Count > 0 Then
                MessageBox.Show("Area Tidak Boleh Dobel !")
            Else
                Dim objArea As SalesmanArea = New SalesmanAreaFacade(User).Retrieve(txtAreaCode.Text)
                If objArea Is Nothing Then
                    MessageBox.Show("Area Yang Anda Masukkan Tidak Ada !")
                Else
                    Dim objSalesmanAreaAssign As SalesmanAreaAssign = New SalesmanAreaAssign
                    objSalesmanAreaAssign.SalesmanArea = objArea
                    objSalesmanAreaAssign.SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
                    Dim result As Integer = facade.Insert(objSalesmanAreaAssign)
                    BindDgArea(0)
                End If


            End If
        End If


    End Sub
    Private Sub dtgExperience_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgExperience.ItemDataBound

        If e.Item.ItemIndex <> -1 Then
            If Not (arrExp Is Nothing) Then
                Dim objSalesmanExperience As SalesmanExperience
                objSalesmanExperience = arrExp(e.Item.ItemIndex)

                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgExperience.CurrentPageIndex * dtgExperience.PageSize)
                If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDeleteExp"), LinkButton)
                    _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                    _lbtnDelete.CommandArgument = objSalesmanExperience.ID

                End If

                If e.Item.ItemType = ListItemType.EditItem Then
                    Dim _lbtnSave As LinkButton = CType(e.Item.FindControl("lbtnSave"), LinkButton)
                    _lbtnSave.CommandArgument = objSalesmanExperience.ID

                End If
            End If
        End If

    End Sub
    Private Sub dtgExperience_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgExperience.ItemCommand
        If e.CommandName = "deleteExp" Then
            Dim facade As SalesmanExperienceFacade = New SalesmanExperienceFacade(User)
            Dim objSalesmanExperience As SalesmanExperience = facade.Retrieve(CInt(e.CommandArgument))
            Dim result As Integer = facade.DeleteFromDB(objSalesmanExperience)
            BindDgExperience(0)

        End If

        If e.CommandName = "addExp" Then
            Dim facade As SalesmanExperienceFacade = New SalesmanExperienceFacade(User)
            Dim txtYearExperienceAdd As TextBox = e.Item.FindControl("txtYearExperienceAdd")
            Dim txtCompanyAdd As TextBox = e.Item.FindControl("txtCompanyAdd")
            Dim txtJobPositionAdd As TextBox = e.Item.FindControl("txtJobPositionAdd")

            If txtYearExperienceAdd.Text = "" Or txtCompanyAdd.Text = "" Or txtJobPositionAdd.Text = "" Then
                MessageBox.Show("Lengkapi Data Dulu !")
                Return
            End If

            Dim objSalesmanExperience As SalesmanExperience = New SalesmanExperience
            objSalesmanExperience.SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
            objSalesmanExperience.YearExperience = txtYearExperienceAdd.Text
            objSalesmanExperience.Company = txtCompanyAdd.Text
            objSalesmanExperience.JobPosition = txtJobPositionAdd.Text

            Dim result As Integer = facade.Insert(objSalesmanExperience)

            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
            BindDgExperience(0)


        End If

        If e.CommandName = "editExp" Then
            dtgExperience.ShowFooter = False
            dtgExperience.EditItemIndex = e.Item.ItemIndex
            BindDgExperience(0)
        End If

        If e.CommandName = "cancelExp" Then
            dtgExperience.ShowFooter = True
            dtgExperience.EditItemIndex = -1
            BindDgExperience(0)
        End If

        If e.CommandName = "saveExp" Then
            Dim objSalesmanExperience As SalesmanExperience
            objSalesmanExperience = New SalesmanExperienceFacade(User).Retrieve(CInt(e.CommandArgument))

            Dim txtYearExperienceEdit As TextBox = e.Item.FindControl("txtYearExperienceEdit")
            Dim txtCompanyEdit As TextBox = e.Item.FindControl("txtCompanyEdit")
            Dim txtJobPositionEdit As TextBox = e.Item.FindControl("txtJobPositionEdit")
            objSalesmanExperience.JobPosition = txtJobPositionEdit.Text
            objSalesmanExperience.Company = txtCompanyEdit.Text
            objSalesmanExperience.YearExperience = txtYearExperienceEdit.Text

            Dim facade As SalesmanExperienceFacade = New SalesmanExperienceFacade(User)
            Dim result As Integer = facade.Update(objSalesmanExperience)

            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
            dtgExperience.ShowFooter = True
            dtgExperience.EditItemIndex = -1
            BindDgExperience(0)

        End If


    End Sub
    Private Sub dtgTraining_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgTraining.ItemCommand
        If e.CommandName = "deleteTrain" Then
            Dim facade As SalesmanTrainingFacade = New SalesmanTrainingFacade(User)
            Dim objSalesmanTraining As SalesmanTraining = facade.Retrieve(CInt(e.CommandArgument))
            Dim result As Integer = facade.DeleteFromDB(objSalesmanTraining)
            BindDgTraining(0)

        End If

        If e.CommandName = "addTrain" Then
            Dim facade As SalesmanTrainingFacade = New SalesmanTrainingFacade(User)
            Dim txtModulTrainingAdd As TextBox = e.Item.FindControl("txtModulTrainingAdd")
            Dim txtTempatTanggalAdd As TextBox = e.Item.FindControl("txtTempatTanggalAdd")
            Dim txtPenyelenggaraAdd As TextBox = e.Item.FindControl("txtPenyelenggaraAdd")

            If txtModulTrainingAdd.Text = "" Or txtTempatTanggalAdd.Text = "" Or txtPenyelenggaraAdd.Text = "" Then
                MessageBox.Show("Lengkapi Data Dulu !")
                Return
            End If

            Dim objSalesmanTraining As SalesmanTraining = New SalesmanTraining
            objSalesmanTraining.SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
            objSalesmanTraining.TrainingModule = txtModulTrainingAdd.Text
            objSalesmanTraining.TrainingPlaceAndDate = txtTempatTanggalAdd.Text
            objSalesmanTraining.TrainingProvider = txtPenyelenggaraAdd.Text

            Dim result As Integer = facade.Insert(objSalesmanTraining)

            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
            BindDgTraining(0)


        End If

        If e.CommandName = "editTrain" Then
            dtgTraining.ShowFooter = False
            dtgTraining.EditItemIndex = e.Item.ItemIndex
            BindDgTraining(0)
        End If

        If e.CommandName = "cancelTrain" Then
            dtgTraining.ShowFooter = True
            dtgTraining.EditItemIndex = -1
            BindDgTraining(0)
        End If

        If e.CommandName = "saveTrain" Then
            Dim objSalesmanTraining As SalesmanTraining
            objSalesmanTraining = New SalesmanTrainingFacade(User).Retrieve(CInt(e.CommandArgument))

            Dim txtModulTrainingEdit As TextBox = e.Item.FindControl("txtModulTrainingEdit")
            Dim txtTempatTanggalEdit As TextBox = e.Item.FindControl("txtTempatTanggalEdit")
            Dim txtPenyelenggaraEdit As TextBox = e.Item.FindControl("txtPenyelenggaraEdit")
            objSalesmanTraining.TrainingModule = txtModulTrainingEdit.Text
            objSalesmanTraining.TrainingPlaceAndDate = txtTempatTanggalEdit.Text
            objSalesmanTraining.TrainingProvider = txtPenyelenggaraEdit.Text

            Dim facade As SalesmanTrainingFacade = New SalesmanTrainingFacade(User)
            Dim result As Integer = facade.Update(objSalesmanTraining)

            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
            dtgTraining.ShowFooter = True
            dtgTraining.EditItemIndex = -1
            BindDgTraining(0)

        End If

    End Sub
    Private Sub dtgTraining_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTraining.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arrTraining Is Nothing) Then
                Dim objSalesmanTraining As SalesmanTraining
                objSalesmanTraining = arrTraining(e.Item.ItemIndex)

                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgTraining.CurrentPageIndex * dtgTraining.PageSize)
                If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDeleteTrain"), LinkButton)
                    _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                    _lbtnDelete.CommandArgument = objSalesmanTraining.ID

                End If

                If e.Item.ItemType = ListItemType.EditItem Then
                    Dim _lbtnSave As LinkButton = CType(e.Item.FindControl("lbtnSaveTrain"), LinkButton)
                    _lbtnSave.CommandArgument = objSalesmanTraining.ID

                End If
            End If
        End If

    End Sub
    Private Sub dtgPrestasi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPrestasi.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arrAccomp Is Nothing) Then
                Dim objSalesmanAccomplish As SalesmanAccomplish
                objSalesmanAccomplish = arrAccomp(e.Item.ItemIndex)

                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgPrestasi.CurrentPageIndex * dtgPrestasi.PageSize)
                If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDeleteAccomp"), LinkButton)
                    _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                    _lbtnDelete.CommandArgument = objSalesmanAccomplish.ID

                End If

                If e.Item.ItemType = ListItemType.EditItem Then
                    Dim _lbtnSave As LinkButton = CType(e.Item.FindControl("lbtnSaveAccomp"), LinkButton)
                    _lbtnSave.CommandArgument = objSalesmanAccomplish.ID

                End If
            End If
        End If
    End Sub
    Private Sub dtgPrestasi_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPrestasi.ItemCommand
        If e.CommandName = "deleteAccomp" Then
            Dim facade As SalesmanAccomplishFacade = New SalesmanAccomplishFacade(User)
            Dim objSalesmanAccomplish As SalesmanAccomplish = facade.Retrieve(CInt(e.CommandArgument))
            Dim result As Integer = facade.DeleteFromDB(objSalesmanAccomplish)
            BindDgPrestasi(0)

        End If

        If e.CommandName = "addAccomp" Then
            Dim facade As SalesmanAccomplishFacade = New SalesmanAccomplishFacade(User)
            Dim txtAccomplishYearAdd As TextBox = e.Item.FindControl("txtAccomplishYearAdd")
            Dim txtAccomplishmentAdd As TextBox = e.Item.FindControl("txtAccomplishmentAdd")

            If txtAccomplishYearAdd.Text = "" Or txtAccomplishmentAdd.Text = "" Then
                MessageBox.Show("Lengkapi Data Dulu !")
                Return
            End If

            Dim objSalesmanAccomplish As SalesmanAccomplish = New SalesmanAccomplish
            objSalesmanAccomplish.SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
            objSalesmanAccomplish.AccomplishYear = txtAccomplishYearAdd.Text
            objSalesmanAccomplish.Accomplishment = txtAccomplishmentAdd.Text

            Dim result As Integer = facade.Insert(objSalesmanAccomplish)

            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
            BindDgPrestasi(0)


        End If

        If e.CommandName = "editAccomp" Then
            dtgPrestasi.ShowFooter = False
            dtgPrestasi.EditItemIndex = e.Item.ItemIndex
            BindDgPrestasi(0)
        End If

        If e.CommandName = "cancelAccomp" Then
            dtgPrestasi.ShowFooter = True
            dtgPrestasi.EditItemIndex = -1
            BindDgPrestasi(0)
        End If

        If e.CommandName = "saveAccomp" Then
            Dim objSalesmanAccomplish As SalesmanAccomplish
            objSalesmanAccomplish = New SalesmanAccomplishFacade(User).Retrieve(CInt(e.CommandArgument))

            Dim txtAccomplishYearEdit As TextBox = e.Item.FindControl("txtAccomplishYearEdit")
            Dim txtAccomplishmentEdit As TextBox = e.Item.FindControl("txtAccomplishmentEdit")
            objSalesmanAccomplish.AccomplishYear = txtAccomplishYearEdit.Text
            objSalesmanAccomplish.Accomplishment = txtAccomplishmentEdit.Text

            Dim facade As SalesmanAccomplishFacade = New SalesmanAccomplishFacade(User)
            Dim result As Integer = facade.Update(objSalesmanAccomplish)

            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
            dtgPrestasi.ShowFooter = True
            dtgPrestasi.EditItemIndex = -1
            BindDgPrestasi(0)

        End If
    End Sub
    Private Sub dtgTarget_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTarget.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arrTarget Is Nothing) Then
                Dim objSalesmanSalesTarget As SalesmanSalesTarget
                objSalesmanSalesTarget = arrTarget(e.Item.ItemIndex)

                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgTarget.CurrentPageIndex * dtgTarget.PageSize)
                If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDeleteTarget"), LinkButton)
                    _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                    _lbtnDelete.CommandArgument = objSalesmanSalesTarget.ID
                    Dim _lblMonthTarget As Label = CType(e.Item.FindControl("lblMonthTarget"), Label)
                    _lblMonthTarget.Text = enumMonthGet.GetName(objSalesmanSalesTarget.MonthTarget)
                End If

                If e.Item.ItemType = ListItemType.EditItem Then
                    Dim _lbtnSave As LinkButton = CType(e.Item.FindControl("lbtnSaveTarget"), LinkButton)
                    _lbtnSave.CommandArgument = objSalesmanSalesTarget.ID

                    Dim _ddlMonthTargetEdit As DropDownList = CType(e.Item.FindControl("ddlMonthTargetEdit"), DropDownList)
                    _ddlMonthTargetEdit.DataSource = enumMonthGet.RetriveMonth()
                    _ddlMonthTargetEdit.DataValueField = "ValStatus"
                    _ddlMonthTargetEdit.DataTextField = "NameStatus"
                    _ddlMonthTargetEdit.DataBind()
                    _ddlMonthTargetEdit.SelectedValue = objSalesmanSalesTarget.MonthTarget

                    Dim _ddlYearTargetEdit As DropDownList = CType(e.Item.FindControl("ddlYearTargetEdit"), DropDownList)

                    Dim i As Integer
                    For i = Date.Today.Year - 1 To Date.Today.Year + 1
                        _ddlYearTargetEdit.Items.Add(New System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()))
                    Next
                    _ddlYearTargetEdit.SelectedValue = objSalesmanSalesTarget.YearTarget

                    Dim txtValueTargetEditNew As TextBox = CType(e.Item.FindControl("txtValueTargetEdit"), TextBox)
                    txtValueTargetEditNew.Text = CType(Format(objSalesmanSalesTarget.ValueTarget, "#,##0"), String)
                End If

            End If
        End If

        If e.Item.ItemType = ListItemType.Footer Then

            Dim _ddlMonthTargetAdd As DropDownList = CType(e.Item.FindControl("ddlMonthTargetAdd"), DropDownList)
            _ddlMonthTargetAdd.DataSource = enumMonthGet.RetriveMonth()
            _ddlMonthTargetAdd.DataValueField = "ValStatus"
            _ddlMonthTargetAdd.DataTextField = "NameStatus"
            _ddlMonthTargetAdd.DataBind()
            _ddlMonthTargetAdd.SelectedValue = Date.Today.Month.ToString

            Dim _ddlYearTargetAdd As DropDownList = CType(e.Item.FindControl("ddlYearTargetAdd"), DropDownList)

            Dim i As Integer
            For i = Date.Today.Year - 1 To Date.Today.Year + 1
                _ddlYearTargetAdd.Items.Add(New System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()))
            Next
            _ddlYearTargetAdd.SelectedValue = Date.Today.Year.ToString

        End If

    End Sub
    Private Sub dtgTarget_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgTarget.ItemCommand
        If e.CommandName = "deleteTarget" Then
            Dim facade As SalesmanSalesTargetFacade = New SalesmanSalesTargetFacade(User)
            Dim objSalesmanSalesTarget As SalesmanSalesTarget = facade.Retrieve(CInt(e.CommandArgument))
            Dim result As Integer = facade.DeleteFromDB(objSalesmanSalesTarget)
            BindDgTarget(0)

        End If

        If e.CommandName = "addTarget" Then
            Dim facade As SalesmanSalesTargetFacade = New SalesmanSalesTargetFacade(User)
            Dim ddlYearTargetAdd As DropDownList = e.Item.FindControl("ddlYearTargetAdd")
            Dim ddlMonthTargetAdd As DropDownList = e.Item.FindControl("ddlMonthTargetAdd")
            Dim txtValueTargetAdd As TextBox = e.Item.FindControl("txtValueTargetAdd")

            If txtValueTargetAdd.Text = "" Then
                MessageBox.Show("Lengkapi Data Dulu !")
                Return
            End If

            Dim objSalesmanSalesTarget As SalesmanSalesTarget = New SalesmanSalesTarget
            objSalesmanSalesTarget.SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
            If Year(objSalesmanSalesTarget.SalesmanHeader.HireDate) = CType(ddlYearTargetAdd.SelectedValue, Integer) Then
                If CType(ddlMonthTargetAdd.SelectedValue, Integer) < Month(objSalesmanSalesTarget.SalesmanHeader.HireDate) Then
                    MessageBox.Show("Bulan target tidak bisa lebih kecil dari bulan Salesman masuk !")
                    Return
                End If
            Else
                If CType(ddlYearTargetAdd.SelectedValue, Integer) < Year(objSalesmanSalesTarget.SalesmanHeader.HireDate) Then
                    MessageBox.Show("Tahun target tidak bisa lebih kecil dari Tahun Salesman masuk !")
                    Return
                End If
            End If

            objSalesmanSalesTarget.YearTarget = ddlYearTargetAdd.SelectedValue
            objSalesmanSalesTarget.MonthTarget = ddlMonthTargetAdd.SelectedValue
            objSalesmanSalesTarget.ValueTarget = CType(Format(CType(txtValueTargetAdd.Text, Decimal), "#,##0"), Decimal)

            Dim result As Integer = facade.Insert(objSalesmanSalesTarget)

            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
            BindDgTarget(0)


        End If

        If e.CommandName = "editTarget" Then
            dtgTarget.ShowFooter = False
            dtgTarget.EditItemIndex = e.Item.ItemIndex
            BindDgTarget(0)
        End If

        If e.CommandName = "cancelTarget" Then
            dtgTarget.ShowFooter = True
            dtgTarget.EditItemIndex = -1
            BindDgTarget(0)
        End If

        If e.CommandName = "saveTarget" Then
            Dim objSalesmanSalesTarget As SalesmanSalesTarget
            objSalesmanSalesTarget = New SalesmanSalesTargetFacade(User).Retrieve(CInt(e.CommandArgument))

            Dim ddlYearTargetEdit As DropDownList = e.Item.FindControl("ddlYearTargetEdit")
            Dim ddlMonthTargetEdit As DropDownList = e.Item.FindControl("ddlMonthTargetEdit")
            Dim txtValueTargetEdit As TextBox = e.Item.FindControl("txtValueTargetEdit")

            If Year(objSalesmanSalesTarget.SalesmanHeader.HireDate) = CType(ddlYearTargetEdit.SelectedValue, Integer) Then
                If CType(ddlMonthTargetEdit.SelectedValue, Integer) < Month(objSalesmanSalesTarget.SalesmanHeader.HireDate) Then
                    MessageBox.Show("Bulan target tidak bisa lebih kecil dari bulan Salesman masuk !")
                    Return
                End If
            Else
                If CType(ddlYearTargetEdit.SelectedValue, Integer) < Year(objSalesmanSalesTarget.SalesmanHeader.HireDate) Then
                    MessageBox.Show("Tahun target tidak bisa lebih kecil dari Tahun Salesman masuk !")
                    Return
                End If
            End If

            objSalesmanSalesTarget.YearTarget = ddlYearTargetEdit.SelectedValue
            objSalesmanSalesTarget.MonthTarget = ddlMonthTargetEdit.SelectedValue
            objSalesmanSalesTarget.ValueTarget = CType(Format(CType(txtValueTargetEdit.Text, Decimal), "#,##0"), Decimal)

            Dim facade As SalesmanSalesTargetFacade = New SalesmanSalesTargetFacade(User)
            Dim result As Integer = facade.Update(objSalesmanSalesTarget)

            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
            dtgTarget.ShowFooter = True
            dtgTarget.EditItemIndex = -1
            BindDgTarget(0)
        End If

    End Sub
    Private Sub btnRequestID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequestID.Click
        If Not Page.IsValid Then
            MessageBox.Show("Data Belum Lengkap")
            Return
        End If
        Dim objSalesmanHeader As SalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader" + CInt(Request.QueryString("id")).ToString()), SalesmanHeader)


        If Not IsNothing(sessHelper.GetSession(POSTEDFILE)) Then
            Dim fUploadedFile As HttpPostedFile = CType(sessHelper.GetSession(POSTEDFILE), HttpPostedFile)
            If IsValidPhoto(fUploadedFile) Then
                Dim imageFile As Byte()
                imageFile = UploadFile(fUploadedFile)
                sessHelper.SetSession(IMGUPLOAD, imageFile)
                sessHelper.RemoveSession(POSTEDFILE)
            Else
                MessageBox.Show("Bukan file gambar atau Ukuran file > " & _
                                       CType(SalesmanHeader.MAX_PHOTO_SIZE / 1024, String) & "KB. Gambar gagal diupload.")

                sessHelper.RemoveSession(POSTEDFILE)
                Exit Sub
            End If
        Else
            If IsNothing(objSalesmanHeader.Image) Then
                MessageBox.Show("Harap upload identitas")
                Return
            End If
        End If

        'Disisni
        Dim crits As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, txtPlaceOfBirth.Text))
        crits.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))
        If Not chkLuarNegri.Checked Then
            Dim arrCity As ArrayList = New CityFacade(Me.User).Retrieve(crits)
            If arrCity.Count > 0 Then
                hdnKotaLahir.Value = CType(arrCity(0), City).CityCode
            Else
                MessageBox.Show("Tempat lahir tidak sesuai dengan nama kota/kabupaten di master data.")
                Return
            End If


        End If

        If objSalesmanHeader Is Nothing Then
            Dim objSalesmanHeaderFacade As New SalesmanHeaderFacade(User)
            objSalesmanHeader = objSalesmanHeaderFacade.Retrieve(CInt(request.QueryString("id")))
        End If
        objSalesmanHeader.IsRequestID = EnumSalesmanIsRequest.SalesmanIsRequest.Sudah_Request

        Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

        If vr.IsValid = False Then
            MessageBox.Show(vr.Message)
            Exit Sub
        End If

        Dim nResult As Integer = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)

        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
            btnRequestID.Enabled = False

            Dim funcSAI As New SalesmanAdditionalInfoFacade(Me.User)
            Dim objAdInfo As New SalesmanAdditionalInfo

            Dim critsSA As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critsSA.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", MatchType.Exact, objSalesmanHeader.ID))
            Dim arrSA As ArrayList = funcSAI.Retrieve(critsSA)
            If arrSA.Count > 0 Then
                objAdInfo = CType(arrSA(0), SalesmanAdditionalInfo)
            End If


            Dim crits3 As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits3.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, objSalesmanHeader.PlaceOfBirth))
            crits.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

            Dim crits2 As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits2.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, objSalesmanHeader.City))
            crits2.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

            objAdInfo.SalesmanHeader = objSalesmanHeader
            Try
                objAdInfo.BirthCity = CType(New CityFacade(Me.User).Retrieve(crits3)(0), City)
            Catch
            End Try
            objAdInfo.AddressCity = CType(New CityFacade(Me.User).Retrieve(crits2)(0), City)
            If arrSA.Count > 0 Then
                Dim mResult As Integer = funcSAI.Update(objAdInfo)
            Else
                Dim mResult As Integer = funcSAI.Insert(objAdInfo)
            End If


        End If

    End Sub
    Private Sub dtgArea_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgArea.PageIndexChanged
        dtgArea.CurrentPageIndex = e.NewPageIndex
        BindDgArea(dtgArea.CurrentPageIndex)
    End Sub
    Private Sub dtgExperience_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgExperience.PageIndexChanged
        dtgExperience.CurrentPageIndex = e.NewPageIndex
        BindDgExperience(dtgExperience.CurrentPageIndex)
    End Sub
    Private Sub dtgPrestasi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPrestasi.PageIndexChanged
        dtgPrestasi.CurrentPageIndex = e.NewPageIndex
        BindDgPrestasi(dtgPrestasi.CurrentPageIndex)
    End Sub
    Private Sub dtgTarget_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgTarget.PageIndexChanged
        dtgTarget.CurrentPageIndex = e.NewPageIndex
        BindDgTarget(dtgTarget.CurrentPageIndex)
    End Sub
    Private Sub dtgTraining_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgTraining.PageIndexChanged
        dtgTraining.CurrentPageIndex = e.NewPageIndex
        BindDgTraining(dtgTraining.CurrentPageIndex)
    End Sub
    Private Sub dtgArea_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgArea.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortColArea") Then
            If sessHelper.GetSession("SortDirectionArea") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirectionArea", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirectionArea", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortColArea", e.SortExpression)
        dtgArea.SelectedIndex = -1
        BindDgArea(dtgArea.CurrentPageIndex)
    End Sub
    Private Sub dtgExperience_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgExperience.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortColExperience") Then
            If sessHelper.GetSession("SortDirectionExperience") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirectionExperience", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirectionExperience", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortColExperience", e.SortExpression)
        dtgExperience.SelectedIndex = -1
        BindDgExperience(dtgExperience.CurrentPageIndex)
    End Sub
    Private Sub dtgPrestasi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPrestasi.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortColPrestasi") Then
            If sessHelper.GetSession("SortDirectionPrestasi") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirectionPrestasi", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirectionPrestasi", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortColPrestasi", e.SortExpression)
        dtgPrestasi.SelectedIndex = -1
        BindDgPrestasi(dtgPrestasi.CurrentPageIndex)
    End Sub
    Private Sub dtgTarget_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgTarget.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortColTarget") Then
            If sessHelper.GetSession("SortDirectionTarget") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirectionTarget", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirectionTarget", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortColTarget", e.SortExpression)
        dtgTarget.SelectedIndex = -1
        BindDgTarget(dtgTarget.CurrentPageIndex)
    End Sub
    Private Sub dtgTraining_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgTraining.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortColTraining") Then
            If sessHelper.GetSession("SortDirectionTraining") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirectionTraining", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirectionTraining", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortColTraining", e.SortExpression)
        dtgTraining.SelectedIndex = -1
        BindDgTraining(dtgTraining.CurrentPageIndex)
    End Sub

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Private ReadOnly Property BuatID As String
        Get
            Return Request.QueryString("buatID")
        End Get
    End Property

    Private ReadOnly Property PageSource As String
        Get
            Return Request.QueryString("Source")
        End Get
    End Property

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        If PageSource.NotNullorEmpty Then
            Response.Redirect("../CallCenter/FrmCcCSTeamList.aspx")
        End If

        If AreaId.NotNullorEmpty Then
            Response.Redirect("../Training/FrmDataSiswa.aspx?area=" + AreaId)
        End If

        If BuatID.NotNullorEmpty Then
            Response.Redirect("../Salesman/FrmSalesmanRegister.aspx?Mode=rsd")
        End If

        Dim strMode As String
        If Not IsNothing(Request.QueryString("Mode")) Then
            strMode = Request.QueryString("Mode")
            Response.Redirect("frmSalesmanList.aspx?Menu=1&Mode=" & strMode)
        Else
            Response.Redirect("frmSalesmanList.aspx?Menu=1&Mode=unit")
        End If
    End Sub
    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub
    Private photoRemoved As Boolean = False
    Private Sub lblRemoveImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblRemoveImage.Click
        If Not IsNothing(sessHelper.GetSession(IMGUPLOAD)) Then
            sessHelper.RemoveSession(IMGUPLOAD)
        End If

        sessHelper.SetSession("RemovePic", True)
        MessageBox.Show("Tekan button Simpan, untuk eksekusi remove image")
    End Sub
    Private Sub ddlJobPositionDesc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlJobPositionDesc.SelectedIndexChanged
        ' pastikan data di webconfig untuk code ada
        If Not IsNothing(sessHelper.GetSession("strIdSManCode")) Then
            strIdSManCode = CType(sessHelper.GetSession("strIdSManCode"), String)
        End If
        If Not IsNothing(sessHelper.GetSession("strIdBManCode")) Then
            strIdBManCode = CType(sessHelper.GetSession("strIdBManCode"), String)
        End If
        If (ddlJobPositionDesc.SelectedValue = strIdSManCode) Or (ddlJobPositionDesc.SelectedValue = strIdBManCode) Then
            ddlSalesmanLevel.Enabled = False
            ddlSalesmanLevel.SelectedValue = "4"
            Requiredfieldvalidator6.Enabled = False ' untuk handle salesman level validator
            ValAtasan.Enabled = False
        Else
            ddlSalesmanLevel.Enabled = True
            Requiredfieldvalidator6.Enabled = True ' untuk handle salesman level validator
            ValAtasan.Enabled = True
        End If
    End Sub
    Private Sub lnkReloadSalesman_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkReloadSalesman.Click
        Dim strMode As String
        If Not IsNothing(Request.QueryString("Mode")) Then
            strMode = Request.QueryString("Mode")
        Else
            strMode = "unit"
        End If

        If txtRefSalesman.Text = "" Then
            MessageBox.Show("Pilih user yang telah resign terlebih dahulu")
        Else
            Dim objSalesmanHeaderReload As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtRefSalesman.Text)
            If Not IsNothing(objSalesmanHeaderReload) Then
                Response.Redirect("FrmSalesmanHeader.aspx?ID=" + objSalesmanHeaderReload.ID.ToString + "&edit=true" + "&Mode=" + strMode + "&Konfirmasi=True")
            Else
                MessageBox.Show("User Reference tidak valid, silakan gunakan pop up")
            End If
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            MessageBox.Show("Data Belum Lengkap")
            Return
        End If

        Try
            'Validate File
            If Not IsNothing(sessHelper.GetSession(POSTEDFILE)) Then
                Dim fUploadedFile As HttpPostedFile = CType(sessHelper.GetSession(POSTEDFILE), HttpPostedFile)
                If IsValidPhoto(fUploadedFile) Then
                    Dim imageFile As Byte()
                    imageFile = UploadFile(fUploadedFile)
                    sessHelper.SetSession(IMGUPLOAD, imageFile)
                    sessHelper.RemoveSession(POSTEDFILE)
                Else
                    MessageBox.Show("Bukan file gambar atau Ukuran file > " & _
                                           CType(SalesmanHeader.MAX_PHOTO_SIZE / 1024, String) & "KB. Gambar gagal diupload.")

                    sessHelper.RemoveSession(POSTEDFILE)
                    Exit Sub
                End If
            Else
                If CType(ViewState("vsProcess"), String) = "Insert" Then
                    MessageBox.Show("Harap upload identitas")
                    Return
                End If
            End If

            Dim isOnlyUploadPhoto As Boolean = CType(sessHelper.GetSession("isOnlyUploadPhoto"), Boolean)
            If isOnlyUploadPhoto And CType(ViewState("vsProcess"), String) = "Edit" Then
                SavePhoto()
                Exit Sub
            End If

            Dim isUnit As Boolean = False
            If Not IsNothing(Request.QueryString("Mode")) Then
                Select Case Request.QueryString("Mode")
                    Case "unit"
                        isUnit = True
                End Select
            Else
                isUnit = True
            End If

            ' Validate email address
            Dim emailAddressValidationMessage As String
            emailAddressValidationMessage = msgErrorEmailValidation()
            If (emailAddressValidationMessage <> "") Then
                MessageBox.Show(emailAddressValidationMessage)
                Return
            End If

            'Validate photo if already inserted
            'Disisni
            Dim crits As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, txtPlaceOfBirth.Text))
            crits.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))
            If Not chkLuarNegri.Checked Then
                Dim arrCity As ArrayList = New CityFacade(Me.User).Retrieve(crits)
                If arrCity.Count > 0 Then
                    hdnKotaLahir.Value = CType(arrCity(0), City).CityCode
                Else
                    MessageBox.Show("Tempat lahir tidak sesuai dengan nama kota/kabupaten di master data.")
                    Return
                End If


            End If


            If isUnit Then
                Dim err As String = msgErrorSuperiorAndKTP()
                If err <> "" Then
                    MessageBox.Show(err)
                    Return
                End If

                ProcessSave()
            Else
                ProcessSave()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



    End Sub


#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        Dim objDealer As Dealer = Session.Item("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Request.QueryString("Mode") = "part" Then
                If Not SecurityProvider.Authorize(Context.User, SR.Input_data_salesman_part_privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=Salesman Part - Buat daftar salesman")
                End If
            End If
            If Request.QueryString("Mode") = "unit" Then
                If Not SecurityProvider.Authorize(Context.User, SR.Input_data_tenaga_penjual_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=Tenaga Penjual - Buat tenaga penjual")
                End If
            End If
            If Request.QueryString("Mode") = "service" Then
                If Not SecurityProvider.Authorize(Context.User, SR.Staff_service_input_data_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=Staff Service - Buat Data")
                End If
            End If
        End If

    End Sub

#End Region

    Private Sub dtgHistory_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgHistory.ItemCommand
        If e.CommandName = "add" Then
            Dim dtDateIn As KTB.DNet.WebCC.IntiCalendar = e.Item.FindControl("dtDateIn")
            Dim dtDateOut As KTB.DNet.WebCC.IntiCalendar = e.Item.FindControl("dtDateOut")
            Dim txtDealerHistory As TextBox = e.Item.FindControl("txtDealerHistory")

            Dim objDealerFacade As New DealerFacade(User)
            Dim objDealer As Dealer = objDealerFacade.Retrieve(txtDealerHistory.Text.Trim)

            If objDealer.ID = 0 Then
                MessageBox.Show("Dealer code not valid")
                Return
            End If

            If dtDateOut.Value < dtDateIn.Value Then
                MessageBox.Show("Date out should be bigger than date in")
                Return
            End If

            Dim strError As String = MsgErrorDateRangeHistory(dtDateIn.Value, dtDateOut.Value)
            If strError <> "" Then
                MessageBox.Show(strError)
                Return
            End If


            Dim obj As New SalesmanDealerHistory
            obj.Dealer = objDealer
            obj.DateIn = dtDateIn.Value
            obj.DateOut = dtDateOut.Value

            If CType(Request.QueryString("id"), Integer) < 0 Or Request.QueryString("id") = String.Empty Then
                Dim arrHistoryToInsert As ArrayList = sessHelper.GetSession("arrHistoryToInsert")
                arrHistoryToInsert.Add(obj)
                sessHelper.SetSession("arrHistoryToInsert", arrHistoryToInsert)
                BindDgHistory(0)

            Else

                Dim objSalesman As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("ID")))
                obj.SalesmanHeader = objSalesman

                Dim result As Integer = New SalesmanDealerHistoryFacade(User).Insert(obj)
                BindDgHistory(0)
            End If

        ElseIf e.CommandName = "delete" Then

            If CType(Request.QueryString("id"), Integer) < 0 Or Request.QueryString("id") = String.Empty Then
                Dim arrHistoryToInsert As ArrayList = sessHelper.GetSession("arrHistoryToInsert")
                arrHistoryToInsert.RemoveAt(e.Item.ItemIndex)
                sessHelper.SetSession("arrHistoryToInsert", arrHistoryToInsert)
                BindDgHistory(0)

            Else
                Dim obj As SalesmanDealerHistory = New SalesmanDealerHistoryFacade(User).Retrieve(CInt(e.CommandArgument))
                Dim result As Integer = New SalesmanDealerHistoryFacade(User).DeleteFromDB(obj)
                BindDgHistory(0)
            End If

        End If
    End Sub

    Private Sub dtgHistory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgHistory.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgHistory.CurrentPageIndex * dtgHistory.PageSize)
            Dim LbtnDeleteHistory As LinkButton = e.Item.FindControl("LbtnDeleteHistory")
            LbtnDeleteHistory.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If
    End Sub

    Private Function msgErrorEmailValidation() As String
        Dim objRenderPanel As RenderingProfile = New RenderingProfile
        Dim errorMessage As String
        Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
        Dim strEmail As String = objRenderPanel.RetrieveValueByProfileHeaderID(Me, objProfileGroup, CType(EnumProfileType.ProfileType.SALESMAN, Short), User, 26)

        Dim isValidEmailAddress As Boolean = False
        isValidEmailAddress = ValidateEmail(strEmail)

        If (strEmail = "") Then
            errorMessage = "Email harus diisi!"
        Else
            If (isValidEmailAddress = False) Then
                errorMessage = "Format email salah!"
            End If
        End If

        Return errorMessage
    End Function

    Public Shared Function ValidateEmail(ByVal emailAddress As String) As Boolean
        Dim emailAddressCheck As Boolean
        Dim pattern As String = "^[0-9a-zA-Z][\w\.-]*[0-9a-zA-Z0-9]@[0-9a-zA-Z0-9][\w\.-]*[0-9a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            emailAddressCheck = True
        Else
            emailAddressCheck = False
        End If

        Return emailAddressCheck
    End Function

    Private Function msgErrorSuperiorAndKTP() As String
        If txtSuperior.Text.Trim <> "" Then
            Dim objSalesmanHeaderFacade As New SalesmanHeaderFacade(User)
            Dim objSuperior As SalesmanHeader = objSalesmanHeaderFacade.RetrieveByCode(txtSuperior.Text.Trim)
            If IsNothing(objSuperior) OrElse objSuperior.ID = 0 Then
                Return "Data atasan dengan kode " & txtSuperior.Text.Trim & " tidak ada."
            End If
            If objSuperior.Status = CInt(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif).ToString Then
                Return "Data atasan dengan kode " & txtSuperior.Text.Trim & " tidak aktif."
            Else
                'Salesman and salesman counter treated as a same level
                Dim CurrentPosition As Integer
                CurrentPosition = CInt(ddlJobPositionDesc.SelectedValue)
                If CurrentPosition = 2 Then CurrentPosition = 3

                If CurrentPosition + 2 < objSuperior.JobPosition.ID Or CurrentPosition >= objSuperior.JobPosition.ID Then
                    Return "Posisi atasan maksimal 2 tingkat diatas salesman ybs."
                End If
            End If
        End If

        'KTP data should get from profile
        Dim objRenderPanel As RenderingProfile = New RenderingProfile

        Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
        Dim strKTP As String = objRenderPanel.RetrieveValueByProfileHeaderID(Me, objProfileGroup, CType(EnumProfileType.ProfileType.SALESMAN, Short), User, 29)

        If strKTP.Length <> 16 Then
            Return "KTP harus 16 digit"
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
        criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, strKTP.ToString.Trim))

        If CType(ViewState("vsProcess"), String) <> "Insert" Then
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.No, Request.QueryString("ID")))
            'criterias.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.Status", MatchType.No, CInt(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif)))
        End If


        Dim arrKTPExist As ArrayList = New SalesmanProfileFacade(User).Retrieve(criterias)

        If arrKTPExist.Count > 0 Then

            Dim salesmanProfileList As List(Of SalesmanProfile) = arrKTPExist.Cast(Of SalesmanProfile)().ToList()

            For Each objSalesmanProfile As SalesmanProfile In salesmanProfileList
                Dim objSalesmanExist As SalesmanHeader = objSalesmanProfile.SalesmanHeader

                If objSalesmanExist.RowStatus = CType(DBRowStatus.Active, Short) And objSalesmanExist.Status <> 3 Then
                    Return "Data KTP yang anda masukkan sudah ada atas nama " & objSalesmanExist.SalesmanCode & " " & objSalesmanExist.Name
                ElseIf objSalesmanExist.RowStatus = CType(DBRowStatus.Active, Short) And objSalesmanExist.Status = 3 Then
                    Return "Data KTP yang anda masukkan sudah ada atas nama " & objSalesmanExist.SalesmanCode & " " & objSalesmanExist.Name & ". Silahkan isi dan reload Referensi salesman untuk melanjutkan input data."
                End If
            Next
        End If


        Return ""

    End Function

    Private Sub ProcessSave()
        Try
            SetVarFromSession()

            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeader
            Dim objSalesmanHeaderFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
            Dim nResult As Integer = -1
            Dim objJPosition As JobPosition = New JobPosition
            Dim objJobPosition As JobPosition = New JobPosition

            'If (photoRemoved = True) Then
            '    MessageBox.Show("Scan KTP harus diisi!")
            '    Return
            'End If

            If (CheckValidation() = True) Then
                UpperControl(True)
                If CType(ViewState("vsProcess"), String) = "Insert" Then

                    With objSalesmanHeader
                        .Dealer = New DealerFacade(User).Retrieve(lblDealerCode.Text)
                        If ddlDealerBranch.SelectedIndex > 0 Then
                            .DealerBranch = New DealerBranchFacade(User).Retrieve(CInt(ddlDealerBranch.SelectedValue))
                        End If
                        .SalesmanCode = lblSalesmanCode.Text
                        .Name = txtName.Text
                        .PlaceOfBirth = txtPlaceOfBirth.Text
                        Try
                            Dim crits As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crits.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, txtPlaceOfBirth.Text))
                            crits.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))
                            hdnKotaLahir.Value = CType(New CityFacade(Me.User).Retrieve(crits)(0), City).CityCode
                        Catch
                        End Try


                        .DateOfBirth = ICDateOfBirth.Value
                        .HireDate = ICStartWork.Value
                        .SalesIndicator = CType(ddlIndicator.SelectedValue, Byte)
                        .Gender = ddlGender.SelectedValue
                        .MarriedStatus = ddlMarriedStatus.SelectedValue
                        .Address = txtAlamat.Text
                        .City = ddlKota.SelectedItem.Text
                        .Status = EnumSalesmanStatus.SalesmanStatus.Baru
                        .RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Belum_Register
                        .IsRequestID = EnumSalesmanIsRequest.SalesmanIsRequest.Belum_Request
                        .SalesmanArea = New SalesmanAreaFacade(User).Retrieve(CInt(Val(DdlArea.SelectedValue)))

                        objJPosition = New JobPositionFacade(User).Retrieve(CType(ddlJobPositionDesc.SelectedValue, Integer))
                        If Not objJPosition Is Nothing Then
                            .JobPosition = objJPosition
                        End If

                        If txtSuperior.Text = "" Then
                            .LeaderId = 0
                        Else
                            Dim objSuperior As SalesmanHeader = objSalesmanHeaderFacade.RetrieveByCode(txtSuperior.Text.Trim)
                            .LeaderId = objSuperior.ID
                        End If

                        '

                        If (ddlJobPositionDesc.SelectedValue = strIdSManCode) Or (ddlJobPositionDesc.SelectedValue = strIdBManCode) Then
                            ' handle untuk bug 894, level tdk ada untuk salesman counter & branch manager
                            .SalesmanLevel = objSalesmanHeaderFacade.RetrieveSalesmanLevelByID(intIdLevelBlank)
                        Else
                            .SalesmanLevel = objSalesmanHeaderFacade.RetrieveSalesmanLevelByID(ddlSalesmanLevel.SelectedValue)
                        End If


                        ' penambahan image menggunakan method conversi
                        'If (photoSrc.PostedFile.FileName <> String.Empty) Then
                        If Not IsNothing(sessHelper.GetSession(IMGUPLOAD)) Then
                            .Image = CType(sessHelper.GetSession(IMGUPLOAD), Byte())
                            lblRemoveImage.Visible = False
                            lblRemoveImage.Enabled = False
                            sessHelper.RemoveSession(IMGUPLOAD)
                        Else
                            lblRemoveImage.Visible = False
                            lblRemoveImage.Enabled = False
                        End If

                        If (isErrorWhileUpload = True) Then
                            Return
                        End If


                        ' Check if existing image is null
                        Dim isThereExistingImage As Boolean = CheckIfThereIsExistingImage()
                        If (isThereExistingImage = True) Then
                            RequiredFieldValidatorForPhotoSrc.Enabled = False
                            RequiredFieldValidatorForPhotoSrc.Visible = False
                        Else
                            'If (Not .Image Is Nothing) Then
                            '    RequiredFieldValidatorForPhotoSrc.Enabled = False
                            '    RequiredFieldValidatorForPhotoSrc.Visible = False
                            'Else
                            '    RequiredFieldValidatorForPhotoSrc.Enabled = True
                            '    RequiredFieldValidatorForPhotoSrc.Visible = True
                            '    MessageBox.Show("Scan KTP harus diisi!")
                            '    Return
                            'End If
                        End If
                    End With
                    RequiredFieldValidatorForPhotoSrc.Enabled = False
                    RequiredFieldValidatorForPhotoSrc.Visible = False
                    Dim objRenderPanel As RenderingProfile = New RenderingProfile
                    ' sebelum melakukan update, pastikan data ProfileHeaderToGroup sdh ada / belum
                    Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
                    If Not IsNothing(objProfileGroup) Then
                        Dim arrTmp As ArrayList
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, objProfileGroup.ID))
                        arrTmp = New ProfileHeaderToGroupFacade(User).RetrieveByCriteria(criterias)

                        'To make sure that if edit operation doesn't pass dealerhistory
                        If Not (CType(Request.QueryString("id"), Integer) < 0 Or Request.QueryString("id") = String.Empty) Then
                            sessHelper.SetSession("arrHistoryToInsert", New ArrayList)
                        End If

                        If Not IsNothing(arrTmp) Then
                            If arrTmp.Count > 0 Then
                                Dim al As ArrayList = objRenderPanel.RetrieveProfileValue(Me, objProfileGroup, CType(EnumProfileType.ProfileType.SALESMAN, Short), User)
                                'nresult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader, al, New ProfileGroupFacade(User).Retrieve(strCurrProfile))
                                nResult = New SalesmanHeaderFacade(User).Insert(objSalesmanHeader, al, sessHelper.GetSession("arrHistoryToInsert"))
                            Else
                                nResult = New SalesmanHeaderFacade(User).InsertTransaction(objSalesmanHeader, sessHelper.GetSession("arrHistoryToInsert"))
                            End If
                        Else
                            nResult = New SalesmanHeaderFacade(User).InsertTransaction(objSalesmanHeader, sessHelper.GetSession("arrHistoryToInsert"))
                        End If
                    End If


                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        Dim objAdInfo As New SalesmanAdditionalInfo

                        Dim crits As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crits.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, objSalesmanHeader.PlaceOfBirth))
                        crits.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

                        Dim crits2 As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crits2.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, objSalesmanHeader.City))
                        crits2.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

                        objAdInfo.SalesmanHeader = New SalesmanHeaderFacade(Me.User).Retrieve(nResult)
                        Try
                            objAdInfo.BirthCity = CType(New CityFacade(Me.User).Retrieve(crits)(0), City)
                        Catch
                        End Try
                        objAdInfo.AddressCity = CType(New CityFacade(Me.User).Retrieve(crits2)(0), City)

                        Dim crits3 As New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crits3.opAnd(New Criteria(GetType(SalesmanDSE), "SalesmanHeader.ID", MatchType.Exact, objAdInfo.SalesmanHeader.ID))
                        crits3.opAnd(New Criteria(GetType(SalesmanDSE), "Dealer.ID", MatchType.Exact, objAdInfo.SalesmanHeader.Dealer.ID))

                        Dim funcdse As New SalesmanDSEFacade(Me.User)
                        Dim arrDSE As ArrayList = funcdse.Retrieve(crits3)
                        If arrDSE.Count > 0 Then
                            Dim objDSE As SalesmanDSE = CType(arrDSE(0), SalesmanDSE)
                            objDSE.PhoneNumber = objAdInfo.SalesmanHeader.PhoneNumber
                            funcdse.Update(objDSE)
                        End If

                        Dim mResult As Integer = New SalesmanAdditionalInfoFacade(Me.User).Insert(objAdInfo)

                        If Not mResult = -1 Then
                            photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & nResult.ToString & "&type=" & "SalesmanHeader"
                            sessHelper.SetSession("IsInsertSuccess", 1)
                            Response.Redirect("FrmSalesmanHeader.aspx?Mode=" & Request.QueryString("mode") & "&id=" + nResult.ToString + "&edit=true")
                            blnSaved = True
                        Else
                            MessageBox.Show(SR.SaveFail)
                        End If
                    End If

                Else
                    ' melakukan update salesmanHeader
                    Update()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function GetImageSalesmanHeader(ByVal nID As Integer) As Boolean
        Dim objDomain As SalesmanHeader = New SalesmanHeader
        Dim objFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        objDomain = objFacade.Retrieve(nID)
        If Not IsNothing(objDomain) AndAlso Not IsNothing(objDomain.Image) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function CheckIfThereIsExistingImage() As Boolean
        Dim personId As Integer = Convert.ToInt32(Request.QueryString("id"))
        Dim isExistingImage As Boolean = GetImageSalesmanHeader(personId)

        Return isExistingImage
    End Function

    Private Function MsgErrorDateRangeHistory(ByVal DtIn As Date, ByVal DtOut As Date) As String

        Dim arrHistory As ArrayList
        If CType(Request.QueryString("id"), Integer) < 0 Or Request.QueryString("id") = String.Empty Then
            arrHistory = sessHelper.GetSession("arrHistoryToInsert")
            'dtgHistory.DataBind()

        Else

            Dim totalRow As Integer = 0

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanDealerHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanDealerHistory), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))

            arrHistory = New SalesmanDealerHistoryFacade(User).RetrieveByCriteria(criterias)
        End If

        For Each item As SalesmanDealerHistory In arrHistory
            If (DtIn < item.DateOut And DtIn > item.DateIn) Or _
                (DtOut <= item.DateOut And DtOut >= item.DateIn) Or _
                (DtIn < item.DateIn And DtOut > item.DateOut) Then
                Return "Tanggal overlap dengan history pada dealer " & item.Dealer.DealerCode & " ( " & item.DateIn.ToString("dd/MM/yyyy") & " - " & item.DateOut.ToString("dd/MM/yyyy") & " )"
            End If
        Next

        Return ""

    End Function

    Private Sub SavePhoto()
        Dim objSalesmanHeader As New SalesmanHeader
        objSalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader" + CInt(Request.QueryString("id")).ToString()), SalesmanHeader)
        Dim nresult As Integer = -1

        With objSalesmanHeader
            Dim blnRemovePic As Boolean
            If Not IsNothing(sessHelper.GetSession("RemovePic")) Then
                blnRemovePic = sessHelper.GetSession("RemovePic")
                If blnRemovePic = True Then
                    .Image = Nothing
                End If
            End If

            ' penambahan image menggunakan method conversi
            If Not IsNothing(sessHelper.GetSession(IMGUPLOAD)) Then
                .Image = CType(sessHelper.GetSession(IMGUPLOAD), Byte())
                lblRemoveImage.Visible = False
                lblRemoveImage.Enabled = False
                sessHelper.RemoveSession(IMGUPLOAD)
            Else
                lblRemoveImage.Visible = False
                lblRemoveImage.Enabled = False
            End If

            If (isErrorWhileUpload = True) Then
                Return
            End If

            Dim isThereExistingImage As Boolean = CheckIfThereIsExistingImage()

            If (Not .Image Is Nothing Or isThereExistingImage = True) Then
                RequiredFieldValidatorForPhotoSrc.Enabled = False
                RequiredFieldValidatorForPhotoSrc.Visible = False
            Else
                RequiredFieldValidatorForPhotoSrc.Enabled = False
                RequiredFieldValidatorForPhotoSrc.Visible = False

            End If
        End With

        nresult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)

        photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & Request.QueryString("id") & "&type=" & "SalesmanHeader"
        If nresult = -1 Then
            MessageBox.Show(SR.UpdateFail)
        Else
            MessageBox.Show("Data berhasil diupdate !")
        End If
    End Sub

    Private Sub chkLuarNegri_CheckedChanged(sender As Object, e As EventArgs) Handles chkLuarNegri.CheckedChanged
        If chkLuarNegri.Checked Then
            lblKotaLahir.Visible = False
        Else
            lblKotaLahir.Visible = True
        End If

    End Sub
End Class