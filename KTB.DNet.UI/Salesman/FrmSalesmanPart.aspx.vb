Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Math
Imports System.Text

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

#End Region

Public Class FrmSalesmanPart
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
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
    Protected WithEvents ddlReligion As System.Web.UI.WebControls.DropDownList
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
    Protected WithEvents lblDealerBranchName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerBranchCode As System.Web.UI.WebControls.Label
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
    Protected WithEvents RequiredfieldvalidatorForIdentityId As System.Web.UI.WebControls.RequiredFieldValidator
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
    Protected WithEvents IsInsertSuccess As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ValAtasan As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtSalary As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlGrade As System.Web.UI.WebControls.DropDownList '
    Protected WithEvents lblGradeDealer As System.Web.UI.WebControls.Label '
    Protected WithEvents lblGrade As System.Web.UI.WebControls.Label
    Protected WithEvents Requiredfieldvalidator10 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblReligion As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalary As System.Web.UI.WebControls.Label
    Protected WithEvents lblLevelText As System.Web.UI.WebControls.Label
    Protected WithEvents lblLevelSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPartshop As System.Web.UI.WebControls.Label
    Protected WithEvents dgPartshop As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPopUpDealerBranch As Label
    Protected WithEvents txtDealerBranchCode As TextBox
    Protected WithEvents txtBranchName As TextBox
    Private sessImageByte As String = "SESSION_IMAGE_BYTE"
    '
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private isReadOnlyProfile As Boolean

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        SetProfile()
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
    Private _salesmanHeader As SalesmanHeader
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper

    'Dim arrHistoryToInsert As New ArrayList
    'Dim arrHistoryToDelete As New ArrayList
    Dim arrHistory As New ArrayList
    Dim arrArea As New ArrayList
    Dim arrPartShop As New ArrayList
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
    'Private isRegistered As Boolean

#End Region

#Region "PrivateCustomMethods"
    Private Function msgErrorNOHPValidation() As String
        Dim objRenderPanel As RenderingProfile = New RenderingProfile
        Dim errorMessage As String = String.Empty
        Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
        Dim objProfileHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve("NO_HP")

        Dim strNOHP As String = objRenderPanel.RetrieveValueByProfileHeaderID(Me, objProfileGroup, CType(EnumProfileType.ProfileType.SALESMAN, Short), User, objProfileHeader.ID)

        Dim isValidNOHP As Boolean = False
        isValidNOHP = ValidateNOHP(strNOHP)

        If strNOHP = "" Then
            errorMessage = "NO HP harus diisi !"
        ElseIf isValidNOHP = False Then
            errorMessage = "Format Nomor HP salah !"
        ElseIf strNOHP.Length < 10 Then
            errorMessage = "NO HP harus lebih dari 10 digit !"
        ElseIf strNOHP.Length > 15 Then
            errorMessage = "NO HP tidak boleh lebih dari 15 digit !"
        End If

        Return errorMessage
    End Function

    Public Shared Function ValidateNOHP(ByVal noHP As String) As Boolean
        If Not IsNumeric(noHP) Then
            Return False
        End If

        Dim noHPCheck As Boolean
        Dim pattern As String = "([\[\(])?(?:(\+62)|62|0)\1? ?-? ?8(?!0|4|6)\d(?!0)\d\1? ?-? ?\d{3,4} ?-? ?\d{3,5}(?: ?-? ?\d{3})?\b"
        Dim noHPMatch As Match = Regex.Match(noHP, pattern)
        If noHPMatch.Success Then
            noHPCheck = True
        Else
            noHPCheck = False
        End If

        Return noHPCheck
    End Function

    Private Function msgErrorEmailValidation() As String
        Dim errorMessage As String
        If Not isReadOnlyProfile Then

            Dim objRenderPanel As RenderingProfile = New RenderingProfile
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

        End If
        Return errorMessage
    End Function

    Public Shared Function ValidateEmail(ByVal emailAddress As String) As Boolean
        Dim emailAddressCheck As Boolean
        emailAddressCheck = False
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            emailAddressCheck = True
        Else
            emailAddressCheck = False
        End If

        Return emailAddressCheck
    End Function

    Private Sub SetSetting()
        lblPageTitle.Text = "PART EMPLOYEE"
        ddlIndicator.SelectedValue = EnumSalesmanUnit.SalesmanUnit.Sparepart
        lblKodeSalesman.Text = "Kode Part Employee"
        TitleHistory.Visible = False
        dtgHistory.Visible = False
        lblAreaSingleTitle.Visible = False
        lblAreasd.Visible = False
        DdlArea.Visible = False
        lblAreaPemasaran.Visible = False
        dtgArea.Visible = True
        Areafieldvalidator.Enabled = False
        lblArea.Visible = False
        pnlSuperior.Visible = False
        ddlIndicator.Enabled = False
        txtMode.Value = ddlIndicator.SelectedValue
        Dim lblKtp As Label
        If Request.QueryString("edit") = "true" Then
            lblKtp = Me.Panel1.FindControl("LABEL29_12")
        Else
            lblKtp = Me.Panel1.FindControl("LABEL57_12")
        End If
        If Not lblKtp Is Nothing Then
            lblKtp.Text = "NO KTP"
        End If
    End Sub

    Private Sub SetProfile()
        strCurrProfile = "sals_dbs_parts"
    End Sub

    Private Sub SetLabel(ByVal Visible As Boolean)
        lblPartshop.Visible = Visible
        lblTarget.Visible = False 'Visible
        lblAreaPemasaran.Visible = Visible
        lblTraining.Visible = Visible
        lblPengalaman.Visible = Visible
        lblPrestasi.Visible = Visible
    End Sub

    Private Sub BindDropDown()
        Dim iMenu As Integer
        CommonFunction.BindFromEnum("SalesmanGender", ddlGender, User, False, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("SalesmanStatus", ddlStatus, User, False, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("MarriedStatus", ddlMarriedStatus, User, False, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("SalesmanUnit", ddlIndicator, User, False, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("Religion", ddlReligion, User, False, "NameStatus", "ValStatus")

        'ddlJobPositionDesc.Items.Clear()
        Dim arlCtgLevel As ArrayList = New SalesmanCategoryLevelFacade(User).RetrieveActiveList()

        BindDropDownListLevel()

        CommonFunction.BindProvince(ddlPropinsi, User, True, False)


        Dim arlArea As ArrayList = New SalesmanAreaFacade(User).RetrieveList("AreaCode", Sort.SortDirection.ASC)

        DdlArea.DataTextField = "AreaDesc"
        DdlArea.DataValueField = "ID"
        DdlArea.DataSource = arlArea
        DdlArea.DataBind()
        DdlArea.Items.Insert(0, New ListItem("Silahkan Pilih", ""))
        DdlArea.SelectedIndex = 0

        ddlGender.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlMarriedStatus.Items.Insert(0, New ListItem("Silakan Pilih", ""))
        ddlGender.SelectedIndex = 0
        ddlMarriedStatus.SelectedIndex = 0


    End Sub

    Private Sub BindDropDownListLevel()
        CommonFunction.BindFromEnum("SalesmanPartLevel", ddlGrade, User, False, "NameStatus", "ValStatus")
    End Sub

    Private Function msgErrorSuperiorAndKTP() As String
        If txtSuperior.Text.Trim <> "" Then
            Dim objSalesmanHeaderFacade As New SalesmanHeaderFacade(User)
            Dim objSuperior As SalesmanHeader = objSalesmanHeaderFacade.RetrieveByCode(txtSuperior.Text.Trim)
            If objSuperior.ID = 0 Then
                Return "Data atasan dengan kode " & txtSuperior.Text.Trim & " tidak ada."
            End If

            'Salesman and salesman counter treated as a same level
            Dim CurrentPosition As Integer
            CurrentPosition = CInt(ddlJobPositionDesc.SelectedValue)
            If CurrentPosition = 2 Then CurrentPosition = 3

            If CurrentPosition + 2 < objSuperior.JobPosition.ID Or CurrentPosition >= objSuperior.JobPosition.ID Then
                Return "Posisi atasan maksimal 2 tingkat diatas salesman ybs."
            End If
        End If



        'KTP data should get from profile
        If Not isReadOnlyProfile Then

            Dim objRenderPanel As RenderingProfile = New RenderingProfile

            Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
            Dim strKTP As String = objRenderPanel.RetrieveValueByProfileHeaderID(Me, objProfileGroup, CType(EnumProfileType.ProfileType.SALESMAN, Short), User, 29)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, strKTP.ToString.Trim))
            If CType(ViewState("vsProcess"), String) <> "Insert" Then
                criterias.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.No, Request.QueryString("ID")))
            End If

            Dim arrKTPExist As ArrayList = New SalesmanProfileFacade(User).Retrieve(criterias)

            If arrKTPExist.Count > 0 Then
                Dim objSalesmanProfile As SalesmanProfile = arrKTPExist(0)
                Dim objSalesmanExist As SalesmanHeader = objSalesmanProfile.SalesmanHeader
                If objSalesmanExist.RowStatus = CType(DBRowStatus.Active, Short) Then
                    Return "Data KTP yang anda masukkan sudah ada atas nama " & objSalesmanExist.SalesmanCode & " " & objSalesmanExist.Name
                End If

            End If

        End If

        Return ""

    End Function

    Private Function IsEmployeePartChange(ByVal salesmanHeader As SalesmanHeader) As Boolean
        Dim oSalesInfoFacade As SalesmanAdditionalInfoFacade = New SalesmanAdditionalInfoFacade(User)
        Dim oSalesmanCategoryLevelFacade As SalesmanCategoryLevelFacade = New SalesmanCategoryLevelFacade(User)
        Dim oSalemenLevelFacade As SalesmanLevelFacade = New SalesmanLevelFacade(User)
        Dim oSalesAddInfo As SalesmanAdditionalInfo
        'do2l
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", MatchType.Exact, salesmanHeader.ID))
        Dim arlSalesInfo As ArrayList = oSalesInfoFacade.Retrieve(criterias)
        If arlSalesInfo.Count > 0 Then
            oSalesAddInfo = CType(arlSalesInfo(0), SalesmanAdditionalInfo)
            ' jika kategory dan posisi berubah
            If oSalesAddInfo.SalesmanCategoryLevel.ID <> CType(Me.ddlSalesmanLevel.SelectedValue, Integer) Then
                Return True
            End If
            'jika level berubah
            If oSalesAddInfo.SalesmanLevel <> CType(Me.ddlGrade.SelectedValue, Integer) Then
                Return True
            End If
        End If
        Return False
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

    'Private Sub BindDgPartshop(ByVal idxPage As Integer)
    '    Dim objDealer As Dealer = Session.Item("DEALER")
    '    'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
    '    dgPartshop.Visible = True
    '    'If Request.QueryString("id") <> "" Then

    '    If ddlSalesmanLevel.SelectedValue = 12 Then
    '        dgPartshop.Visible = True
    '        Dim totalRow As Integer = 0
    '        If Request.QueryString("id") <> "" Then
    '            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartShop), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '            criterias.opAnd(New Criteria(GetType(SalesmanPartShop), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))

    '            arrPartShop = New SalesmanPartShopFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgPartshop.PageSize, totalRow, _
    '            sessHelper.GetSession("SortColPartshop"), sessHelper.GetSession("SortDirectionPartshop"))
    '        Else
    '            arrPartShop = New ArrayList
    '            arrPartShop = sessHelper.GetSession("EmpPartshop")
    '        End If

    '        dgPartshop.CurrentPageIndex = idxPage
    '        dgPartshop.DataSource = arrPartShop
    '        dgPartshop.VirtualItemCount = totalRow
    '        dgPartshop.DataBind()

    '    Else
    '        dgPartshop.DataSource = Nothing
    '        dgPartshop.DataBind()
    '    End If
    '    'Else
    '    '    dgPartshop.Visible = False
    '    'End If

    '    'Else
    '    '    dgPartshop.Visible = False
    '    'End If
    'End Sub

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
            'Else
            '    Dim arrTmp As ArrayList
            '    Dim criterias As New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    criterias.opAnd(New Criteria(GetType(JobPosition), "Description", MatchType.Exact, ddlJobPositionDesc.SelectedItem.Text))
            '    arrTmp = New JobPositionFacade(User).Retrieve(criterias)

            '    If Not IsNothing(arrTmp) Then
            '        If arrTmp.Count <= 0 Then
            '            blnValid = False
            '            MessageBox.Show("Silakan masukan data Posisi jabatan yg valid, gunakan list")
            '            Return (blnValid)
            '        End If
            '    End If
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

        'add by anh 20151016 -masih remarks blm release 
        'If (ICStartWork.Value > Date.Now) Then
        '    blnValid = False
        '    MessageBox.Show("Tanggal Masuk tidak boleh lebih dari tanggal hari ini.")
        '    Return (blnValid)
        'End If

        'check ktp
        'Dim ktp As String = ""
        'Dim objRenderPanel As RenderingProfile = New RenderingProfile
        'Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
        'Dim objListProfile As ArrayList = objRenderPanel.RetrieveProfileValue(Me, objProfileGroup, CType(EnumProfileType.ProfileType.SALESMAN, Short), User)
        'For Each item As SalesmanProfile In objListProfile
        '    If item.ProfileHeader.ID = 29 Then
        '        ktp = item.ProfileValue
        '        Exit For
        '    End If
        'Next

        'If (Len(ktp) > 0) Then
        '    ktp = ktp.Replace(".", "")
        '    ktp = ktp.Replace(" ", "")
        '    If Len(ktp) < 16 Then
        '        blnValid = False
        '        MessageBox.Show("Format Nomor KTP salah, (minimal 16 karakter).")
        '        Return (blnValid)

        '    End If
        'End If
        'end added by anh
        Dim objDealer As Dealer = Session.Item("DEALER")
        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            Dim alGroup1 As New ArrayList
            Dim objRenderPanel As New RenderingProfile

            alGroup1 = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve(strCurrProfile), CType(EnumProfileType.ProfileType.SALESMAN, Short), User, True)
            For Each oCrp As SalesmanProfile In alGroup1
                If oCrp.ProfileHeader.Mandatory = EnumMandatory.MandatoryMode.Benar AndAlso (IsNothing(oCrp.ProfileValue) OrElse oCrp.ProfileValue.Trim = "") Then
                    MessageBox.Show(oCrp.ProfileHeader.Description.ToString & " Harus diisi")
                    Return False
                End If
                If oCrp.ProfileHeader.Code = "NO_HP" Then
                    If oCrp.ProfileValue.Length < 6 OrElse oCrp.ProfileValue.Substring(0, 2) = "00" Then
                        MessageBox.Show("No HandPhone tidak benar")
                        Return False
                    End If
                End If
            Next
        End If

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
        txtName.Text = ""
        txtPlaceOfBirth.Text = ""
        txtResignReason.Text = ""
        'txtLeadJobPositionDesc.Text = ""
        txtJobPosition.Value = ""
        'txtLeadJobPosition.Value = ""
        txtAlamat.Text = ""
        'txtLeaderName.Text = ""
        'txtJumlahToko.Text = ""

        ddlGender.SelectedIndex = -1
        ddlSalesmanLevel.SelectedIndex = -1
        ddlMarriedStatus.SelectedIndex = -1
        ddlPropinsi.SelectedIndex = -1
        ddlKota.SelectedIndex = -1
        'ddlIndicator.SelectedIndex = -1

        ICDateOfBirth.Value = Date.Now
        ICStartWork.Value = Date.Now
        ICEndWork.Value = Date.Now

        'photoView.Visible = False

        'SalesUnitIndicator.Checked = False
        'MechanicIndicator.Checked = False
        'SparePartIndicator.Checked = False

        AdmIndicator.Checked = False
        WHIndicator.Checked = False
        CounterIndicator.Checked = False
        SalesIndicator.Checked = False

        'btnSimpan.Enabled = True
        blnSaved = False

        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub ProcessSave()
        Dim objDealer As Dealer = Session.Item("DEALER")
        SetVarFromSession()

        If (CheckValidation() = True) Then
            UpperControl(True)
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                Insert()
            Else
                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    UpdateByKTB()
                Else
                    Update()
                End If
            End If
        End If
    End Sub

    Private Sub Insert()
        Dim objSalesHeaderFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Try
            Dim idReturn As Integer = InsertSalesmanHeader()
            If idReturn > 0 Then
                Dim objSalesHeader As SalesmanHeader = objSalesHeaderFacade.Retrieve(idReturn)
                If Not IsNothing(objSalesHeader) Then
                    If InsertSalesmanAdditionalInfo(objSalesHeader) <> -1 Then
                        If Not IsNothing(Request.QueryString("SalesResign")) Then
                            SaveSalesmanPartHistoryRef(objSalesHeader)
                            'ElseIf Not IsNothing(sessHelper.GetSession("SALESREFERENCE")) Then
                            '    SaveSalesmanPartHistoryRef(objSalesHeader)
                        Else
                            SaveSalesmanPartHistory(objSalesHeader, HistoryStatus.Baru)
                        End If
                        'Save SalesmanPartshop
                        'Dim arlSalesmanPartshop As ArrayList = sessHelper.GetSession("EmpPartshop")
                        'If arlSalesmanPartshop.Count > 0 Then
                        '    SaveSalesmanPartshop(arlSalesmanPartshop, objSalesHeader)
                        'End If
                    Else
                        objSalesHeaderFacade.DeleteFromDB(objSalesHeader)
                        MessageBox.Show(SR.SaveFail)
                    End If
                End If
                'photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & idReturn.ToString & "&type=" & "SalesmanHeader"
                sessHelper.SetSession("IsInsertSuccess", 1)
                Response.Redirect("FrmSalesmanPart.aspx?Mode=" & Request.QueryString("mode") & "&id=" + idReturn.ToString + "&edit=true")
                MessageBox.Show(SR.SaveSuccess)
                btnRequestID.Visible = True
                btnRequestID.Enabled = True
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
    End Sub

    Private Sub Update()
        Dim objSalesHeaderFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Try
            Dim idReturn As Integer = UpdateSalesmanHeader()
            If idReturn <> -1 Then
                Dim objSalesHeader As SalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader)
                If Not IsNothing(objSalesHeader) Then

                    Dim isChanged As Boolean = False
                    Dim isCategoryChanged As Boolean = False
                    Dim isSalesmanLevelChanged As Boolean = False
                    Dim oSalesAddInfo As New SalesmanAdditionalInfo

                    Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", MatchType.Exact, objSalesHeader.ID))
                    Dim arlSalesInfo As ArrayList = New SalesmanAdditionalInfoFacade(User).Retrieve(criterias)

                    If arlSalesInfo.Count > 0 Then
                        oSalesAddInfo = CType(arlSalesInfo(0), SalesmanAdditionalInfo)
                        'oSalesAddInfo.SalesmanCategoryLevel.SalesmanCategoryLevel.ID
                        Dim oSalesmanCategoryLevel As SalesmanCategoryLevel = New SalesmanCategoryLevelFacade(User).Retrieve(CType(Me.ddlSalesmanLevel.SelectedValue, Integer))
                        If Not IsNothing(oSalesmanCategoryLevel) Then
                            'kategori
                            If oSalesAddInfo.SalesmanCategoryLevel.SalesmanCategoryLevel.ID <> oSalesmanCategoryLevel.SalesmanCategoryLevel.ID Then
                                isChanged = True
                                isCategoryChanged = True
                            End If
                            'posisi
                            If oSalesAddInfo.SalesmanCategoryLevel.ID <> oSalesmanCategoryLevel.ID Then
                                isChanged = True
                            End If
                            'jika level berubah
                            If Not isChanged Then
                                'If ddlGrade.Visible = True Then
                                If oSalesAddInfo.SalesmanLevel <> CType(Me.ddlGrade.SelectedValue, Integer) Then
                                    isSalesmanLevelChanged = True
                                End If
                                'End If
                            End If

                        End If
                    End If

                    If isCategoryChanged Then
                        SaveSalesmanPartHistory(objSalesHeader, HistoryStatus.Pengajuan)
                        UpdateSalesmanAdditionalInfoPartial(objSalesHeader)
                    Else
                        SaveSalesmanPartHistory(objSalesHeader, HistoryStatus.Disetujui)
                        UpdateSalesmanAdditionalInfo(objSalesHeader)
                    End If
                    If Not isSalesmanLevelChanged Then
                        SendEmail(objSalesHeader, False)
                    End If
                End If
                MessageBox.Show(SR.UpdateSucces)
                If objSalesHeader.SalesmanCode = String.Empty Then

                    btnRequestID.Visible = True
                    btnRequestID.Enabled = True
                End If
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
    End Sub

    Private Sub UpdateByKTB()
        Dim objSalesmanHeader As New SalesmanHeader
        Dim objSalesmanHistory As New SalesmanPartHistory
        Dim oSalesInfoFacade As SalesmanAdditionalInfoFacade = New SalesmanAdditionalInfoFacade(User)

        Dim oSalesAddInfo As SalesmanAdditionalInfo
        Try
            objSalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader)

            'Insert SalesmanHistory
            If SaveSalesmanPartHistory(objSalesmanHeader, HistoryStatus.Disetujui) <> -1 Then
                objSalesmanHistory = sessHelper.GetSession("SALESHISTORY")

                'Update SalesmanHeader
                Dim nUpdateHeader As Integer
                Dim IsCategoryChanged As Boolean = False
                Dim IsPositionChanged As Boolean = False
                Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", MatchType.Exact, objSalesmanHistory.SalesmanHeader.ID))
                Dim arlSalesInfo As ArrayList = oSalesInfoFacade.Retrieve(criterias)
                If arlSalesInfo.Count > 0 Then
                    oSalesAddInfo = CType(arlSalesInfo(0), SalesmanAdditionalInfo)
                    If oSalesAddInfo.SalesmanCategoryLevel.SalesmanCategoryLevel.ID <> objSalesmanHistory.SalesmanCategoryLevel.SalesmanCategoryLevel.ID Then
                        objSalesmanHeader.SalesmanCode = "update_code"
                        IsCategoryChanged = True
                    End If
                    If oSalesAddInfo.SalesmanCategoryLevel.ID <> objSalesmanHistory.SalesmanCategoryLevel.ID Then
                        oSalesAddInfo.SalesmanCategoryLevel = objSalesmanHistory.SalesmanCategoryLevel
                        oSalesAddInfo.SalesmanLevel = objSalesmanHistory.SalesmanLevel
                        IsPositionChanged = True
                    End If

                    'objSalesmanHeader.RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register ' set supaya diregister
                    'objSalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Aktif
                    nUpdateHeader = UpdateSalesmanHeader(objSalesmanHeader)

                End If

                'Update SalesmanAdditionalInfo
                If nUpdateHeader <> -1 Then
                    If oSalesInfoFacade.Update(oSalesAddInfo) <> -1 Then
                        If UpdateSalesmanCodeInHistory(objSalesmanHistory) <> -1 Then
                            If IsCategoryChanged Or IsPositionChanged Then
                                Dim newSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(objSalesmanHistory.SalesmanHeader.ID)
                                SendEmail(newSalesmanHeader, False)
                            End If
                            MessageBox.Show(SR.UpdateSucces)
                        Else
                            MessageBox.Show("Gagal update histori kode employee")
                        End If
                    Else
                        MessageBox.Show("Gagal update kategori employee")
                    End If
                Else
                    MessageBox.Show("Gagal update part employee")
                End If
            Else
                MessageBox.Show("Gagal insert histori kode employee")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'MessageBox.Show(SR.UpdateFail)
        End Try

    End Sub

    Private Function InsertSalesmanHeader() As Integer

        Dim nResult As Integer = -1

        Dim objSalesmanHeaderFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)

        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeader
        With objSalesmanHeader
            .Dealer = New DealerFacade(User).Retrieve(lblDealerCode.Text)
            .SalesmanCode = lblSalesmanCode.Text
            .Name = txtName.Text
            .PlaceOfBirth = txtPlaceOfBirth.Text
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

            Dim jPosition As SalesmanCategoryLevel = New SalesmanCategoryLevelFacade(User).Retrieve(CType(ddlSalesmanLevel.SelectedValue, Integer))
            .JobPosition = New JobPositionFacade(User).Retrieve(jPosition.Kode)

            If Not String.IsNullOrEmpty(txtDealerBranchCode.Text.Trim()) Then
                Dim objDealerBranch As DealerBranch = New DealerBranchFacade(User).Retrieve(txtDealerBranchCode.Text, lblDealerCode.Text)
                If Not IsNothing(objDealerBranch) Then
                    .DealerBranch = objDealerBranch
                Else
                    MessageBox.Show("Kode cabang tidak terdaftar pada dealer")
                    Return nResult
                End If
            End If

            If txtSuperior.Text = "" Then
                .LeaderId = 0
            Else
                Dim objSuperior As SalesmanHeader = objSalesmanHeaderFacade.RetrieveByCode(txtSuperior.Text.Trim)
                .LeaderId = objSuperior.ID
            End If

            If CType(ddlJobPositionDesc.SelectedValue, Integer) Then
                If (ddlJobPositionDesc.SelectedValue = strIdSManCode) Or (ddlJobPositionDesc.SelectedValue = strIdBManCode) Then
                    ' handle untuk bug 894, level tdk ada untuk salesman counter & branch manager
                    .SalesmanLevel = objSalesmanHeaderFacade.RetrieveSalesmanLevelByID(intIdLevelBlank)
                Else
                    .SalesmanLevel = objSalesmanHeaderFacade.RetrieveSalesmanLevelByID(ddlGrade.SelectedValue)
                End If
            Else
                .SalesmanLevel = Nothing
            End If

            If Not IsNothing(sessHelper.GetSession(sessImageByte)) Then
                .Image = CType(sessHelper.GetSession(sessImageByte), Byte())
            End If
            ' penambahan image menggunakan method conversi
            'Dim filePic As HttpPostedFile = CType(sessHelper.GetSession("IMAGEPATH"), HttpPostedFile)
            'If Not IsNothing(filePic) Then
            '    If (filePic.FileName <> String.Empty) Then 'If (photoSrc.PostedFile.FileName <> String.Empty) Then
            '        Dim imageFile As Byte()
            '        imageFile = UploadFile()
            '        .Image = imageFile
            '        lblRemoveImage.Visible = True
            '        lblRemoveImage.Enabled = True
            '    Else
            '        lblRemoveImage.Visible = False
            '        lblRemoveImage.Enabled = False
            '    End If
            'End If
        End With

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
                If (arrTmp.Count > 0) AndAlso (Not isReadOnlyProfile) Then
                    Dim al As ArrayList = objRenderPanel.RetrieveProfileValue(Me, objProfileGroup, CType(EnumProfileType.ProfileType.SALESMAN, Short), User)
                    If Not ValidKTPInProfile(al) Then
                        Return nResult
                    End If
                    nResult = New SalesmanHeaderFacade(User).Insert(objSalesmanHeader, al, sessHelper.GetSession("arrHistoryToInsert"))
                Else
                    nResult = New SalesmanHeaderFacade(User).InsertTransaction(objSalesmanHeader, sessHelper.GetSession("arrHistoryToInsert"))
                End If
            Else
                nResult = New SalesmanHeaderFacade(User).InsertTransaction(objSalesmanHeader, sessHelper.GetSession("arrHistoryToInsert"))
            End If
        End If
        Return nResult
    End Function

    Private Function ValidKTPInProfile(objListProfile As ArrayList) As Boolean
        Dim ktp As String = ""
        For Each item As SalesmanProfile In objListProfile
            If item.ProfileHeader.ID = 29 Then
                ktp = item.ProfileValue
                Exit For
            End If
        Next

        If ktp.Replace(" ", "").Trim().Length <> 16 Then
            MessageBox.Show("Format No KTP Salah")
            Return False
        Else
            Dim valid As Boolean = False

            For Each ObjChar As Char In ktp
                If Not IsNumeric(ObjChar) Then
                    MessageBox.Show("Format No KTP Salah")
                    Return False
                End If
            Next
        End If
        Return True
    End Function

    Private Function UpdateSalesmanHeader(Optional ByVal objSalesmanHeader As SalesmanHeader = Nothing) As Integer
        Dim nresult As Integer = -1
        Try
            'Dim objSalesmanHeader As New SalesmanHeader
            Dim objJPosition As New JobPosition
            Dim objJobPosition As New JobPosition

            If IsNothing(objSalesmanHeader) Then
                objSalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader)
            End If

            With objSalesmanHeader
                .Dealer = New DealerFacade(User).Retrieve(lblDealerCode.Text)
                .SalesmanCode = lblSalesmanCode.Text
                .Name = txtName.Text
                .PlaceOfBirth = txtPlaceOfBirth.Text
                .DateOfBirth = ICDateOfBirth.Value
                .HireDate = ICStartWork.Value
                .SalesIndicator = CType(ddlIndicator.SelectedValue, Byte)
                .SalesmanArea = New SalesmanAreaFacade(User).Retrieve(CInt(Val(DdlArea.SelectedValue)))

                Dim jPosition As SalesmanCategoryLevel = New SalesmanCategoryLevelFacade(User).Retrieve(CType(ddlSalesmanLevel.SelectedValue, Integer))
                objJPosition = New JobPositionFacade(User).Retrieve(jPosition.Kode)

                If Not String.IsNullOrEmpty(txtDealerBranchCode.Text.Trim()) Then
                    Dim objDealerBranch As DealerBranch = New DealerBranchFacade(User).Retrieve(txtDealerBranchCode.Text, lblDealerCode.Text)
                    If Not IsNothing(objDealerBranch) Then
                        .DealerBranch = objDealerBranch
                    Else
                        MessageBox.Show("Kode cabang tidak terdaftar pada dealer")
                        Return nresult
                    End If
                Else
                    .DealerBranch = Nothing
                End If

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

                If Not IsNothing(Request.QueryString("SalesResign")) Then
                    .RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Belum_Register
                    .ResignDate = Date.MinValue
                    .ResignReason = String.Empty
                End If

                .Status = ddlStatus.SelectedValue

                If CType(ddlSalesmanLevel.SelectedValue, Integer) = 12 Then 'Sales
                    If (ddlSalesmanLevel.SelectedValue = strIdSManCode) Or (ddlJobPositionDesc.SelectedValue = strIdBManCode) Then
                        .SalesmanLevel = New SalesmanHeaderFacade(User).RetrieveSalesmanLevelByID(intIdLevelBlank)
                    Else
                        .SalesmanLevel = New SalesmanHeaderFacade(User).RetrieveSalesmanLevelByID(ddlGrade.SelectedValue)
                    End If
                Else
                    .SalesmanLevel = Nothing
                End If

                Dim blnRemovePic As Boolean
                If Not IsNothing(sessHelper.GetSession("RemovePic")) Then
                    blnRemovePic = sessHelper.GetSession("RemovePic")
                    If blnRemovePic = True Then
                        .Image = Nothing
                    End If
                End If

                ' penambahan image menggunakan method conversi
                Dim filePic As HttpPostedFile = CType(sessHelper.GetSession("IMAGEPATH"), HttpPostedFile)
                If Not IsNothing(filePic) Then
                    If (filePic.FileName <> String.Empty) Then 'If (photoSrc.PostedFile.FileName <> String.Empty) Then
                        Dim imageFile As Byte()
                        imageFile = UploadFile()
                        .Image = imageFile
                        lblRemoveImage.Visible = True
                        lblRemoveImage.Enabled = True
                    Else
                        lblRemoveImage.Visible = False
                        lblRemoveImage.Enabled = False
                    End If
                End If
            End With

            Dim arrTmp As ArrayList
            Dim objRenderPanel As RenderingProfile = New RenderingProfile
            ' sebelum melakukan update, pastikan data ProfileHeaderToGroup sdh ada / belum
            Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
            If Not IsNothing(objProfileGroup) Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, objProfileGroup.ID))
                arrTmp = New ProfileHeaderToGroupFacade(User).RetrieveByCriteria(criterias)

                If Not IsNothing(arrTmp) Then
                    If (arrTmp.Count > 0) AndAlso (Not isReadOnlyProfile) Then
                        Dim al As ArrayList = objRenderPanel.RetrieveProfileValue(Me, objProfileGroup, CType(EnumProfileType.ProfileType.SALESMAN, Short), User)
                        If Not ValidKTPInProfile(al) Then
                            Return nresult
                        End If
                        nresult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader, al, New ProfileGroupFacade(User).Retrieve(strCurrProfile))
                    Else

                        Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

                        If vr.IsValid = False Then
                            MessageBox.Show(vr.Message)
                            Exit Function
                        End If

                        nresult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
                    End If
                Else

                    Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

                    If vr.IsValid = False Then
                        MessageBox.Show(vr.Message)
                        Exit Function
                    End If

                    nresult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
                End If
                sessHelper.SetSession("vsSalesmanHeader", objSalesmanHeader)
            End If
            ' photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & Request.QueryString("id") & "&type=" & "SalesmanHeader"
            Return nresult
        Catch ex As Exception
            Return nresult
        End Try
    End Function

    Private Function InsertSalesmanAdditionalInfo(ByVal salemanHeader As SalesmanHeader) As Integer

        Dim iReturn As Integer = -1
        Dim oSalesInfoFacade As SalesmanAdditionalInfoFacade = New SalesmanAdditionalInfoFacade(User)
        Dim oSalesmanCategoryLevelFacade As SalesmanCategoryLevelFacade = New SalesmanCategoryLevelFacade(User)
        Dim oSalemenLevelFacade As SalesmanLevelFacade = New SalesmanLevelFacade(User)

        Dim oSalesAddInfo As SalesmanAdditionalInfo = New SalesmanAdditionalInfo
        If Not IsNothing(Request.QueryString("SalesResign")) Then
            If Not IsNothing(Request.QueryString("ID")) Then
                Dim oSalesHeaderRef As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CType(Request.QueryString("ID"), Integer))
                If Not IsNothing(oSalesHeaderRef) Then
                    oSalesAddInfo.SalesmanHeader_Ref = oSalesHeaderRef
                End If
            End If
            'ElseIf Not IsNothing(sessHelper.GetSession("SALESREFERENCE")) Then
            '    Dim oSalesHeaderRef As SalesmanHeader = CType(sessHelper.GetSession("SALESREFERENCE"), SalesmanHeader)
            '    If Not IsNothing(oSalesHeaderRef) Then
            '        oSalesAddInfo.SalesmanHeader_Ref = oSalesHeaderRef
            '    End If
        Else
            oSalesAddInfo.SalesmanHeader_Ref = Nothing
        End If
        oSalesAddInfo.SalesmanHeader = salemanHeader
        oSalesAddInfo.SalesmanCategoryLevel = oSalesmanCategoryLevelFacade.Retrieve(CType(Me.ddlSalesmanLevel.SelectedValue, Integer))
        If oSalesAddInfo.SalesmanCategoryLevel.ID = 12 Then '12 = sales
            'penentuan level berdasarkan lama kerja dari tgl masuk
            'ICStartWork.Value
            Dim iYears As Integer = DateDiff(DateInterval.Year, ICStartWork.Value, Date.Now)
            oSalesAddInfo.SalesmanLevel = GetSalesmanLevelByYears(Math.Abs(iYears))

            Dim objDealer As Dealer = Session.Item("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                oSalesAddInfo.SalesmanLevel = CType(ddlGrade.SelectedValue, Integer)
            End If
        Else
            oSalesAddInfo.SalesmanLevel = 99
        End If
        oSalesAddInfo.ReligionID = CType(Me.ddlReligion.SelectedValue, Integer)
        oSalesAddInfo.Salary = CType(IIf(txtSalary.Text.Trim = String.Empty, "0", txtSalary.Text.Trim), Decimal)
        iReturn = oSalesInfoFacade.Insert(oSalesAddInfo)
        Return iReturn
    End Function

    Private Function UpdateSalesmanAdditionalInfo(ByVal salemanHeader As SalesmanHeader) As Integer

        Dim iReturn As Integer
        Dim oSalesInfoFacade As SalesmanAdditionalInfoFacade = New SalesmanAdditionalInfoFacade(User)
        Dim oSalesmanCategoryLevelFacade As SalesmanCategoryLevelFacade = New SalesmanCategoryLevelFacade(User)
        Dim oSalemenLevelFacade As SalesmanLevelFacade = New SalesmanLevelFacade(User)
        Dim oSalesAddInfo As SalesmanAdditionalInfo

        Dim isChanged As Boolean = False

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", MatchType.Exact, salemanHeader.ID))
        Dim arlSalesInfo As ArrayList = New SalesmanAdditionalInfoFacade(User).Retrieve(criterias)

        If arlSalesInfo.Count < 1 Then
            oSalesAddInfo = New SalesmanAdditionalInfo
        Else
            Dim BeforeSalesmanCategoryLevel As Integer = 0

            oSalesAddInfo = CType(arlSalesInfo(0), SalesmanAdditionalInfo)
            oSalesAddInfo.ReligionID = CType(Me.ddlReligion.SelectedValue, Integer)
            oSalesAddInfo.Salary = CType(IIf(txtSalary.Text.Trim = String.Empty, "0", txtSalary.Text.Trim), Decimal)
            'If salemanHeader.SalesmanCode = "" Or salemanHeader.SalesmanCode = "request_part" Then
            BeforeSalesmanCategoryLevel = oSalesAddInfo.SalesmanCategoryLevel.ID
            oSalesAddInfo.SalesmanCategoryLevel = oSalesmanCategoryLevelFacade.Retrieve(CType(Me.ddlSalesmanLevel.SelectedValue, Integer))
            If oSalesAddInfo.SalesmanCategoryLevel.ID = 12 Then
                'oSalesAddInfo.SalesmanLevel = CType(Me.ddlGrade.SelectedValue, Integer)
                'penentuan level berdasarkan lama kerja dari tgl masuk
                'ICStartWork.Value
                Dim iYears As Integer = DateDiff(DateInterval.Year, ICStartWork.Value, Date.Now)
                oSalesAddInfo.SalesmanLevel = GetSalesmanLevelByYears(Math.Abs(iYears))
            Else
                oSalesAddInfo.SalesmanLevel = 99
            End If
            'End If
            iReturn = oSalesInfoFacade.Update(oSalesAddInfo)

            'If iReturn <> -1 Then
            '    If BeforeSalesmanCategoryLevel <> CType(Me.ddlSalesmanLevel.SelectedValue, Integer) Then
            '        'Fixing
            '        'If oSalesAddInfo.SalesmanCategoryLevel.ID <> CType(Me.ddlSalesmanLevel.SelectedValue, Integer) Then
            '        isChanged = True
            '    End If
            '    'jika level berubah
            '    If ddlGrade.Visible = True Then
            '        If oSalesAddInfo.SalesmanLevel <> CType(Me.ddlGrade.SelectedValue, Integer) Then
            '            isChanged = True
            '        End If
            '    End If
            '    oSalesAddInfo.SalesmanCategoryLevel = oSalesmanCategoryLevelFacade.Retrieve(CType(Me.ddlSalesmanLevel.SelectedValue, Integer))
            '    If oSalesAddInfo.SalesmanCategoryLevel.ID = 12 Then
            '        oSalesAddInfo.SalesmanLevel = CType(Me.ddlGrade.SelectedValue, Integer)
            '    Else
            '        oSalesAddInfo.SalesmanLevel = 99
            '    End If
            '    If isChanged Then
            '        SendEmail(salemanHeader, False)
            '    End If
            'End If
            Return iReturn
            Exit Function
        End If
        If Not IsNothing(Request.QueryString("SalesResign")) Then
            If Not IsNothing(Request.QueryString("ID")) Then
                Dim oSalesHeaderRef As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CType(Request.QueryString("ID"), Integer))
                If Not IsNothing(oSalesHeaderRef) Then
                    oSalesAddInfo.SalesmanHeader_Ref = oSalesHeaderRef
                End If
            End If
        Else
            oSalesAddInfo.SalesmanHeader_Ref = Nothing
        End If
        oSalesAddInfo.SalesmanHeader = salemanHeader

        oSalesAddInfo.SalesmanCategoryLevel = oSalesmanCategoryLevelFacade.Retrieve(CType(Me.ddlSalesmanLevel.SelectedValue, Integer))

        If oSalesAddInfo.SalesmanCategoryLevel.ID = 12 Then '12 = sales
            Dim objDealer As Dealer = Session.Item("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                oSalesAddInfo.SalesmanLevel = CType(ddlGrade.SelectedValue, Integer)
            Else
                'penentuan level berdasarkan lama kerja dari tgl masuk
                Dim iYears As Integer = DateDiff(DateInterval.Year, ICStartWork.Value, Date.Now)
                oSalesAddInfo.SalesmanLevel = GetSalesmanLevelByYears(Math.Abs(iYears))
            End If
        Else
            oSalesAddInfo.SalesmanLevel = 99
        End If
        oSalesAddInfo.ReligionID = CType(Me.ddlReligion.SelectedValue, Integer)
        oSalesAddInfo.Salary = CType(IIf(txtSalary.Text.Trim = String.Empty, "0", txtSalary.Text.Trim), Decimal)
        If arlSalesInfo.Count < 1 Then
            iReturn = oSalesInfoFacade.Insert(oSalesAddInfo)
        End If
        If iReturn <> -1 Then
            oSalesAddInfo.ID = iReturn
            SaveSalesmanPartHistoryRef(salemanHeader)
        End If
        Return iReturn
    End Function

    Private Function GetSalesmanLevelByYears(ByVal iYears As Integer) As Integer
        Dim iReturn As Integer = 0

        'If Math.Abs(iYears) < 5 Then
        '    iReturn = EnumSalesmanPartLevel.Level.Junior
        'ElseIf 5 <= Math.Abs(iYears) < 10 Then
        '    iReturn = EnumSalesmanPartLevel.Level.Senior
        'ElseIf Math.Abs(iYears) > 10 Then
        '    iReturn = EnumSalesmanPartLevel.Level.Top
        'End If

        Select Case iYears
            Case Is < 5
                iReturn = EnumSalesmanPartLevel.Level.Junior
            Case Is < 10
                iReturn = EnumSalesmanPartLevel.Level.Senior
            Case Is >= 10
                iReturn = EnumSalesmanPartLevel.Level.Top
        End Select

        Return iReturn
    End Function

    Private Function UpdateSalesmanAdditionalInfoPartial(ByVal salemanHeader As SalesmanHeader) As Integer

        Dim iReturn As Integer
        Dim oSalesInfoFacade As SalesmanAdditionalInfoFacade = New SalesmanAdditionalInfoFacade(User)
        Dim oSalesAddInfo As SalesmanAdditionalInfo

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", MatchType.Exact, salemanHeader.ID))
        Dim arlSalesInfo As ArrayList = New SalesmanAdditionalInfoFacade(User).Retrieve(criterias)

        If arlSalesInfo.Count < 1 Then
            oSalesAddInfo = New SalesmanAdditionalInfo

            InsertSalesmanAdditionalInfo(salemanHeader)
        Else
            oSalesAddInfo = CType(arlSalesInfo(0), SalesmanAdditionalInfo)
            oSalesAddInfo.ReligionID = CType(Me.ddlReligion.SelectedValue, Integer)
            oSalesAddInfo.Salary = CType(IIf(txtSalary.Text.Trim = String.Empty, "0", txtSalary.Text.Trim), Decimal)
            iReturn = oSalesInfoFacade.Update(oSalesAddInfo)

            Return iReturn
            Exit Function
        End If
        If Not IsNothing(Request.QueryString("SalesResign")) Then
            If Not IsNothing(Request.QueryString("ID")) Then
                Dim oSalesHeaderRef As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CType(Request.QueryString("ID"), Integer))
                If Not IsNothing(oSalesHeaderRef) Then
                    oSalesAddInfo.SalesmanHeader_Ref = oSalesHeaderRef
                End If
            End If
        Else
            oSalesAddInfo.SalesmanHeader_Ref = Nothing
        End If
        oSalesAddInfo.SalesmanHeader = salemanHeader
        oSalesAddInfo.ReligionID = CType(Me.ddlReligion.SelectedValue, Integer)
        oSalesAddInfo.Salary = CType(IIf(txtSalary.Text.Trim = String.Empty, "0", txtSalary.Text.Trim), Decimal)
        If arlSalesInfo.Count < 1 Then
            iReturn = oSalesInfoFacade.Insert(oSalesAddInfo)
        End If
        If iReturn <> -1 Then
            oSalesAddInfo.ID = iReturn
            SaveSalesmanPartHistoryRef(salemanHeader)
        End If
        Return iReturn
    End Function

    Private Function UpdateSalesmanAdditionalInfoByKTB(ByVal salemanHeader As SalesmanHeader) As Integer

        Dim iReturn As Integer
        Dim oSalesInfoFacade As SalesmanAdditionalInfoFacade = New SalesmanAdditionalInfoFacade(User)
        Dim oSalesmanCategoryLevelFacade As SalesmanCategoryLevelFacade = New SalesmanCategoryLevelFacade(User)
        Dim oSalesAddInfo As SalesmanAdditionalInfo

        Dim isChanged As Boolean = False

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", MatchType.Exact, salemanHeader.ID))
        Dim arlSalesInfo As ArrayList = New SalesmanAdditionalInfoFacade(User).Retrieve(criterias)

        oSalesAddInfo = CType(arlSalesInfo(0), SalesmanAdditionalInfo)
        oSalesAddInfo.SalesmanCategoryLevel = oSalesmanCategoryLevelFacade.Retrieve(CType(Me.ddlSalesmanLevel.SelectedValue, Integer))
        If oSalesAddInfo.SalesmanCategoryLevel.ID = 12 Then
            oSalesAddInfo.SalesmanLevel = CType(Me.ddlGrade.SelectedValue, Integer)
        Else
            oSalesAddInfo.SalesmanLevel = 99
        End If

        iReturn = oSalesInfoFacade.Update(oSalesAddInfo)

        'If iReturn <> -1 Then
        '    SendEmail(salemanHeader, False)
        'End If
        Return iReturn
    End Function

    Private Sub SaveSalesmanPartHistoryRef(ByVal salesmanHeader As SalesmanHeader)
        If Not IsNothing(salesmanHeader.SalesmanAdditionalInfo) Then
            Dim oSai As SalesmanAdditionalInfo = CType(salesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo)
            If Not IsNothing(oSai.SalesmanHeader_Ref) Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SalesmanPartHistory), "SalesmanHeader.ID", MatchType.Exact, oSai.SalesmanHeader_Ref.ID))
                Dim arlSalesHistory As ArrayList = New SalesmanPartHistoryFacade(User).Retrieve(criterias)
                Dim iReturn As Integer = 0
                If arlSalesHistory.Count > 0 Then
                    For Each oSH As SalesmanPartHistory In arlSalesHistory
                        oSH.SalesmanHeader = salesmanHeader
                        oSH.Status = HistoryStatus.Resign
                        iReturn = New SalesmanPartHistoryFacade(User).Insert(oSH)
                    Next
                End If
            End If
        End If
    End Sub

    Private Function UpdateSalesmanCodeInHistory(ByVal objSalesmanHistory As SalesmanPartHistory) As Integer
        Dim ireturn As Integer = -1
        Dim newSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(objSalesmanHistory.SalesmanHeader.ID)
        If Not IsNothing(newSalesmanHeader) Then
            objSalesmanHistory.SalesmanCode = newSalesmanHeader.SalesmanCode
            ireturn = New SalesmanPartHistoryFacade(User).Update(objSalesmanHistory)
        End If
        Return ireturn
    End Function

    Private Function SaveSalesmanPartHistory(ByVal salemanHeader As SalesmanHeader, ByVal status As HistoryStatus) As Integer
        Dim iReturn As Integer
        Dim oSalesHistoryFacade As SalesmanPartHistoryFacade = New SalesmanPartHistoryFacade(User)
        Dim oSalesmanCategoryLevelFacade As SalesmanCategoryLevelFacade = New SalesmanCategoryLevelFacade(User)
        Dim oSalemenLevelFacade As SalesmanLevelFacade = New SalesmanLevelFacade(User)

        Try
            Dim oSalesHistory As SalesmanPartHistory
            'If status = HistoryStatus.Baru Then
            oSalesHistory = New SalesmanPartHistory
            'Else

            '    Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    criterias.opAnd(New Criteria(GetType(SalesmanPartHistory), "SalesmanHeader.ID", MatchType.Exact, salemanHeader.ID))
            '    Dim arlSalesHistory As ArrayList = oSalesHistoryFacade.Retrieve(criterias)
            '    If arlSalesHistory.Count > 0 Then
            '        If status.Resign Then
            '            For Each hist As SalesmanPartHistory In arlSalesHistory
            '                If hist.Status = HistoryStatus.Pengajuan Then
            '                    hist.Status = HistoryStatus.Dibatalkan
            '                    oSalesHistoryFacade.Update(hist)
            '                End If
            '            Next
            '        End If
            '        oSalesHistory = CType(arlSalesHistory(0), SalesmanPartHistory)
            '    Else
            '        oSalesHistory = New SalesmanPartHistory
            '    End If
            'End If
            oSalesHistory.SalesmanCode = salemanHeader.SalesmanCode
            oSalesHistory.ChangedDate = Date.Now
            oSalesHistory.Status = status
            oSalesHistory.Dealer = salemanHeader.Dealer
            oSalesHistory.SalesmanHeader = salemanHeader
            oSalesHistory.SalesmanCategoryLevel = oSalesmanCategoryLevelFacade.Retrieve(CType(Me.ddlSalesmanLevel.SelectedValue, Integer))
            If oSalesHistory.SalesmanCategoryLevel.ID = 12 Then '12 = sales
                'penentuan level berdasarkan lama kerja dari tgl masuk
                'ICStartWork.Value
                Dim iYears As Integer = DateDiff(DateInterval.Year, ICStartWork.Value, Date.Now)
                oSalesHistory.SalesmanLevel = GetSalesmanLevelByYears(Math.Abs(iYears))

                Dim objDealer As Dealer = Session.Item("DEALER")
                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    oSalesHistory.SalesmanLevel = CType(ddlGrade.SelectedValue, Integer)
                End If
            Else
                oSalesHistory.SalesmanLevel = 99
            End If


            iReturn = oSalesHistoryFacade.Insert(oSalesHistory)
            If iReturn <> -1 Then
                oSalesHistory = oSalesHistoryFacade.Retrieve(iReturn)
                sessHelper.SetSession("SALESHISTORY", oSalesHistory)
            Else
                sessHelper.SetSession("SALESHISTORY", Nothing)
            End If
        Catch ex As Exception
            iReturn = -1
        End Try

        Return iReturn
    End Function

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
        Dim objDealer As Dealer = Session.Item("DEALER")
        'If EditStatus = True And objSalesmanHeader.IsRequestID = 0 Then
        If objSalesmanHeader.SalesmanCode = String.Empty Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                btnRequestID.Enabled = False
            Else
                btnRequestID.Enabled = True
            End If
        Else
            'If Request.QueryString("SalesResign") <> String.Empty Then
            '   btnRequestID.Enabled = True
            'Else
            btnRequestID.Enabled = False
            'End If

        End If

        'If EditStatus = True And Not (IsNothing(objSalesmanHeader.Image)) Then
        '    lblRemoveImage.Visible = True
        '    lblRemoveImage.Enabled = True
        'Else
        '    lblRemoveImage.Visible = False
        '    lblRemoveImage.Enabled = False
        'End If

        If Not IsNothing(objSalesmanHeader.Image) Then
            sessHelper.SetSession(sessImageByte, objSalesmanHeader.Image)
        End If


        'Todo session
        'Session.Add("vsSalesmanHeader", objSalesmanHeader)
        sessHelper.SetSession("vsSalesmanHeader", objSalesmanHeader)
        With objSalesmanHeader
            'Dim objDealer As Dealer = New DealerFacade(User).Retrieve(.Dealer.ID)
            If Not IsNothing(Request.QueryString("SalesResign")) Then
                lblSalesmanCode.Text = ""
            Else
                lblSalesmanCode.Text = .SalesmanCode
            End If

            lblName.Text = .Name
            lblArea.Text = .SalesmanArea.AreaDesc
            lblPlaceOfBirth.Text = .PlaceOfBirth
            lblDateOfBirth.Text = .DateOfBirth
            lblStartWork.Text = .HireDate

            If Not IsNothing(Request.QueryString("Konfirmasi")) Then
                Try
                    Dim oDealerAdditional As DealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(objDealerTmp.ID)(0)
                    If Not oDealerAdditional Is Nothing Then
                        lblGradeDealer.Text = oDealerAdditional.SparePartGrade
                    End If
                Catch
                    lblGradeDealer.Text = String.Empty
                End Try

                ' mengambil dari user yang login
                lblDealerCode.Text = objDealerTmp.DealerCode
                lblDealerName.Text = objDealerTmp.DealerName


            Else
                Try
                    Dim oDealerAdditional As DealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(.Dealer.ID)(0)
                    If Not oDealerAdditional Is Nothing Then
                        lblGradeDealer.Text = oDealerAdditional.SparePartGrade
                    End If
                Catch
                    lblGradeDealer.Text = String.Empty
                End Try

                lblDealerCode.Text = .Dealer.DealerCode
                lblDealerName.Text = .Dealer.DealerName

            End If

            If Not IsNothing(.DealerBranch) Then
                txtDealerBranchCode.Text = .DealerBranch.DealerBranchCode
                lblDealerBranchCode.Text = .DealerBranch.DealerBranchCode
                txtBranchName.Text = .DealerBranch.Name
                lblDealerBranchName.Text = .DealerBranch.Name
            End If

            txtName.Text = .Name
            txtPlaceOfBirth.Text = .PlaceOfBirth
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
                txtSuperior.Text = objSuperior.SalesmanCode
                txtSuperiorName.Text = objSuperior.Name
            End If


            DdlArea.SelectedValue = .SalesmanArea.ID.ToString

            Dim objSalesmanHeaderLead As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(.LeaderId)

            txtAlamat.Text = .Address

            ' mengambil data propinsi yg bersangkutan
            Dim objCity As City
            Dim objCityFacade As CityFacade = New CityFacade(User)
            If .City.Trim <> "" Then
                objCity = objCityFacade.Retrieve(.City, 1)
                ddlPropinsi.SelectedValue = objCity.Province.ID
                ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
                lblPropinsi.Text = ddlPropinsi.SelectedItem.Text
                ddlKota.SelectedValue = objCity.ID
            Else
                ddlPropinsi.SelectedIndex = 0
                ddlKota.SelectedIndex = 0
            End If

            ddlStatus.SelectedValue = .Status.ToString
            lblStatus.Text = ddlStatus.SelectedItem.Text

            Dim salesInfo As SalesmanAdditionalInfo
            If .SalesmanAdditionalInfo.Count > 0 Then
                salesInfo = .SalesmanAdditionalInfo(0)
            Else
                salesInfo = Nothing
            End If
            If Not salesInfo Is Nothing Then
                If salesInfo.ReligionID.Trim <> "" Then
                    Dim religion As EnumReligion.Religion = salesInfo.ReligionID.ToString
                    ddlReligion.SelectedItem.Text = religion.ToString
                    lblReligion.Text = religion.ToString
                Else
                    ddlReligion.SelectedIndex = 0
                    lblReligion.Text = ""
                End If


                If Not IsNothing(salesInfo.SalesmanCategoryLevel.SalesmanCategoryLevel) Then
                    ddlJobPositionDesc.SelectedValue = salesInfo.SalesmanCategoryLevel.SalesmanCategoryLevel.ID
                    lblJobPositionDesc.Text = salesInfo.SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName
                Else
                    ddlJobPositionDesc.SelectedIndex = 0
                End If
                ddlJobPositionDesc_SelectedIndexChanged(Nothing, Nothing)

                If Not IsNothing(salesInfo.SalesmanCategoryLevel) Then
                    ddlSalesmanLevel.SelectedValue = salesInfo.SalesmanCategoryLevel.ID
                    lblSalesmanLevel.Text = salesInfo.SalesmanCategoryLevel.PositionName
                Else
                    ddlSalesmanLevel.SelectedIndex = 0
                End If
                ddlSalesmanLevel_SelectedIndexChanged(Nothing, Nothing)
                If salesInfo.SalesmanLevel <> 99 Then
                    ddlGrade.SelectedValue = salesInfo.SalesmanLevel
                    lblGrade.Text = EnumSalesmanPartLevel.RetrieveName(salesInfo.SalesmanLevel)
                    lblLevelText.Visible = True
                    lblLevelSeparator.Visible = True
                    ddlGrade.Visible = True
                    'BindDgPartshop(0)
                Else
                    ddlGrade.SelectedIndex = 0
                    lblLevelText.Visible = False
                    lblLevelSeparator.Visible = False
                    ddlGrade.Visible = False
                End If

                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    ddlGrade.Enabled = True
                Else
                    ddlGrade.Enabled = False
                End If
                'ddlGrade.Visible = EditStatus
                txtSalary.Text = FormatNumber(salesInfo.Salary, 0, TriState.True, TriState.True, TriState.True)
                lblSalary.Text = FormatNumber(salesInfo.Salary, 0, TriState.True, TriState.True, TriState.True)
            End If

            If IsNothing(Request.QueryString("SalesResign")) Then
                ICEndWork.Value = .ResignDate
                txtResignReason.Text = .ResignReason
                If .ResignDate.Year < 1900 Then
                    lblEndWork.Text = "-"
                Else
                    lblEndWork.Text = .ResignDate
                End If
                lblResignReason.Text = .ResignReason
            Else
                ICEndWork.Value = Date.Now
                txtResignReason.Text = String.Empty
                lblEndWork.Text = String.Empty
                lblResignReason.Text = String.Empty
            End If


            ' Show image
            'photoView.Visible = True
            'photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & nID & "&type=" & "SalesmanHeader"

        End With

        Me.btnSimpan.Enabled = EditStatus
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            EnablingControl(EditStatus)
        Else
            EnablingControl(False)
            txtRefSalesman.Enabled = False
            lbtnRefSalesman.Visible = False
            lnkReloadSalesman.Visible = False

            'CR Part Employee By jon, assigne to mr ali
            ddlJobPositionDesc.Enabled = True
            ddlSalesmanLevel.Enabled = True
            ddlGrade.Enabled = True
            ' ddlStatus.Enabled = True
            btnBatal.Enabled = False
            RequiredFieldValidator1.Enabled = False
            RequiredFieldValidator2.Enabled = False
            RequiredFieldValidator3.Enabled = False
            Requiredfieldvalidator8.Enabled = False

        End If

    End Sub

    Private Sub EnablingControl(ByVal isEnabled As Boolean)

        txtName.Enabled = isEnabled
        txtAlamat.Enabled = isEnabled
        txtPlaceOfBirth.Enabled = isEnabled
        ICDateOfBirth.Enabled = isEnabled
        ddlGender.Enabled = isEnabled
        ddlMarriedStatus.Enabled = isEnabled
        photoSrc.Disabled = Not isEnabled
        ddlPropinsi.Enabled = isEnabled
        ddlKota.Enabled = isEnabled
        ddlJobPositionDesc.Enabled = isEnabled
        ddlSalesmanLevel.Enabled = isEnabled
        'ddlGrade.Enabled = isEnabled
        txtSalary.Enabled = isEnabled
        ddlReligion.Enabled = isEnabled
        ddlStatus.Enabled = False 'isEnabled
        ICStartWork.Enabled = isEnabled
        ICDateOfBirth.Enabled = isEnabled
        DdlArea.Enabled = isEnabled
        ddlSalesmanLevel.Enabled = isEnabled
        Requiredfieldvalidator6.Enabled = isEnabled
        ValAtasan.Enabled = isEnabled

        ICEndWork.Enabled = False
        txtResignReason.Enabled = False

        dtgHistory.Columns(4).Visible = isEnabled
        dtgHistory.ShowFooter = isEnabled

        dtgTarget.Columns(4).Visible = False 'isEnabled    ' kolom aksi
        dtgTarget.ShowFooter = False 'isEnabled

        dtgArea.Columns(3).Visible = isEnabled      ' kolom aksi
        dtgArea.ShowFooter = isEnabled

        dtgTraining.Columns(4).Visible = isEnabled  ' kolom aksi
        dtgTraining.ShowFooter = isEnabled

        dtgExperience.Columns(4).Visible = isEnabled    ' kolom aksi
        dtgExperience.ShowFooter = isEnabled

        dtgPrestasi.Columns(3).Visible = isEnabled      ' kolom aksi
        dtgPrestasi.ShowFooter = isEnabled


    End Sub



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
        ddlSalesmanLevel.Visible = blnVisible
        ddlJobPositionDesc.Visible = blnVisible
        'ddlGrade.Visible = blnVisible
        ddlReligion.Visible = blnVisible
        ICStartWork.Visible = blnVisible
        ICEndWork.Visible = blnVisible
        txtResignReason.Visible = blnVisible
        ddlStatus.Visible = blnVisible
        ddlIndicator.Visible = blnVisible
        txtSalary.Visible = blnVisible
        lblRef.Visible = blnVisible
        txtRefSalesman.Visible = blnVisible
        lbtnRefSalesman.Visible = blnVisible
        lnkReloadSalesman.Visible = blnVisible
        txtDealerBranchCode.Visible = blnVisible
        txtBranchName.Visible = blnVisible
        lblPopUpDealerBranch.Visible = blnVisible
        '---
        lblDealerBranchCode.Visible = Not blnVisible
        lblDealerBranchName.Visible = Not blnVisible
        lblName.Visible = Not blnVisible
        lblArea.Visible = Not blnVisible
        lblPlaceOfBirth.Visible = Not blnVisible
        lblDateOfBirth.Visible = Not blnVisible
        lblGender.Visible = Not blnVisible
        lblMarriedStatus.Visible = Not blnVisible
        lblAlamat.Visible = Not blnVisible
        lblPropinsi.Visible = Not blnVisible
        lblKota.Visible = Not blnVisible
        lblSalesmanLevel.Visible = Not blnVisible
        lblJobPositionDesc.Visible = Not blnVisible
        lblGrade.Visible = Not blnVisible
        lblSalary.Visible = Not blnVisible
        lblReligion.Visible = Not blnVisible
        lblStartWork.Visible = Not blnVisible
        lblEndWork.Visible = Not blnVisible
        lblResignReason.Visible = Not blnVisible
        lblStatus.Visible = Not blnVisible
        lblIndicator.Visible = Not blnVisible

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
        Dim objDealer As Dealer = Session.Item("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtRefSalesman.Enabled = False
            lbtnRefSalesman.Visible = False
            lnkReloadSalesman.Visible = False
        End If
    End Sub

    Private Function UploadFile() As Byte()
        Dim nResult() As Byte

        Try
            'Dim filePic As HttpPostedFile = CType(sessHelper.GetSession("IMAGEPATH"), HttpPostedFile)
            If IsValidPhoto(photoSrc) Then
                Dim inStream As System.IO.Stream = photoSrc.PostedFile.InputStream()
                Dim ByteRead(SalesmanHeader.MAX_PHOTO_SIZE) As Byte
                Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, SalesmanHeader.MAX_PHOTO_SIZE)
                If ReadCount = 0 Then
                    Throw New InvalidConstraintException(SR.DataNotFound("Photo"))
                End If
                ReDim nResult(ReadCount)
                Array.Copy(ByteRead, nResult, ReadCount)
                sessHelper.SetSession(sessImageByte, nResult)
            Else
                sessHelper.SetSession(sessImageByte, Nothing)
                'Throw New DataException("Bukan file gambar atau Ukuran file > " & _
                '                        CType(SalesmanHeader.MAX_PHOTO_SIZE / 1024, String) & "KB")
                MessageBox.Show("Bukan file gambar atau Ukuran file > " & _
                                        CType(SalesmanHeader.MAX_PHOTO_SIZE / 1024, String) & "KB")
            End If
        Catch
            'Throw
        End Try

        Return nResult
    End Function

    ' untuk keperluan penyimpanan photo
    Private Function IsValidPhoto(ByVal file As HtmlInputFile) As Boolean
        Dim containImage As Boolean = (file.PostedFile.ContentType.ToUpper.IndexOf(SalesmanHeader.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.PostedFile.ContentLength <= SalesmanHeader.MAX_PHOTO_SIZE)
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


    Private Sub SendEmail(ByVal objSalesmanHeader As SalesmanHeader, ByVal bStatus As Boolean) ' bStatus = New (true) or Update(false) Employee
        Dim appConfigFacade As New AppConfigFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_FROM_ASS).Value
        Dim emailTo As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_TO_SP_ADMIN).Value
        Dim UrlPartEmpGenerate As String = KTB.DNet.Lib.WebConfig.GetValue("UrlPartEmpGenerate")
        Dim urlPartEmpList As String = KTB.DNet.Lib.WebConfig.GetValue("UrlPartEmpList")

        Dim valueEmail As String
        If bStatus Then
            valueEmail = GenerateEmailNewEmployee(objSalesmanHeader, UrlPartEmpGenerate)
            ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet] Parts - Request Employee Part Code ", Mail.MailFormat.Html, valueEmail)
        Else
            valueEmail = GenerateEmailUpdateEmployee(objSalesmanHeader, urlPartEmpList)
            ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet] Parts - Update Employee Part Code ", Mail.MailFormat.Html, valueEmail)
        End If

    End Sub

    Private Function GenerateEmailNewEmployee(ByVal objSalesmanHeader As SalesmanHeader, ByVal urlRequest As String) As String

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder("")
        sb.Append("<FONT face=Arial size=1>")
        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 align=center><b>Request Part Employee Code</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50>")
        sb.Append("Dengan hormat,&nbsp;")
        sb.Append("<br><br>Berikut data Part Employee baru :")
        sb.Append("</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=10></td>")
        sb.Append("</tr>")
        sb.Append("</table>")

        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")

        sb.Append("<tr>")
        sb.Append("<td width='35%'>Dealer</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.Dealer.DealerCode & " / " & objSalesmanHeader.Dealer.SearchTerm2 & "</b></td>")
        sb.Append("</tr>")

        sb.Append("<tr>")
        sb.Append("<td width='35%'>Nama</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.Name & "</b></td>")
        sb.Append("</tr>")

        If objSalesmanHeader.SalesmanAdditionalInfo.Count > 0 Then
            sb.Append("<tr>")
            sb.Append("<td width='35%'>Kategori</td>")
            sb.Append("<td width='5%'>:</td>")
            sb.Append("<td width='60%'><b>" & CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName & "</b></td>")
            sb.Append("</tr>")

            sb.Append("<tr>")
            sb.Append("<td width='35%'>Posisi</td>")
            sb.Append("<td width='5%'>:</td>")
            sb.Append("<td width='60%'><b>" & CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.PositionName & "</b></td>")
            sb.Append("</tr>")

            Dim EnumLevel As EnumSalesmanPartLevel.Level = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanLevel
            If EnumLevel <> 99 Then
                sb.Append("<tr>")
                sb.Append("<td width='35%'>Level</td>")
                sb.Append("<td width='5%'>:</td>")
                sb.Append("<td width='60%'><b>" & EnumLevel.ToString & "</b></td>")
                sb.Append("</tr>")
            End If

        End If

        sb.Append("<tr>")
        sb.Append("<td width='35%'>Tanggal Masuk</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.HireDate.ToString("dd MMM yyyy") & "</b></td>")
        sb.Append("</tr>")
        Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        sb.Append("<tr>")
        sb.Append("<td width='35%'>Diajukan oleh</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objUser.Dealer.DealerCode & " - " & objUser.UserName & "</b></td>")
        sb.Append("</tr>")

        sb.Append("</table>")

        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>Untuk registrasi data Part Employee diatas, dapat diakses pada link dibawah ini :</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'><font color='blue'><a href='" & urlRequest & "'>MMKSI DNet Sparepart</a></font></td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("</FONT>")

        Return sb.ToString

    End Function

    Private Function GenerateEmailUpdateEmployee(ByVal objSalesmanHeader As SalesmanHeader, ByVal urlPartEmpList As String) As String
        Try
            Dim oSalesPartHistory As SalesmanPartHistory = CType(sessHelper.GetSession("SALESHISTORY"), SalesmanPartHistory)
            Dim sb As StringBuilder = New StringBuilder("")
            sb.Append("<FONT face=Arial size=1>")
            sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
            sb.Append("<tr>")
            sb.Append("<td colspan=3 align=center><b>Perubahan Data Jabatan Part Employee </b></td>")
            sb.Append("</tr>")
            sb.Append("<tr>")
            sb.Append("<td colspan=3 height=50></td>")
            sb.Append("</tr>")
            sb.Append("<tr>")
            sb.Append("<td colspan=3 height=50>")
            sb.Append("Dengan hormat,&nbsp;")
            sb.Append("<br><br>Berikut perubahan data Part Employee  :")
            sb.Append("</td>")
            sb.Append("</tr>")
            sb.Append("<tr>")
            sb.Append("<td colspan=3 height=10></td>")
            sb.Append("</tr>")
            sb.Append("<tr>") 'Dealer
            sb.Append("<td height=10 width='25%'>Nama Dealer</td>")
            sb.Append("<td height=10 width='5%'>:</td>")
            sb.Append("<td height=10 width='75%'>" & objSalesmanHeader.Dealer.DealerCode & " / " & objSalesmanHeader.Dealer.DealerName & "</td>")
            sb.Append("</tr>")
            sb.Append("<tr>") 'Nama
            sb.Append("<td height=10 width='25%'>Nama Employee</td>")
            sb.Append("<td height=10 width='5%'>:</td>")
            sb.Append("<td height=10 width='75%'>" & objSalesmanHeader.Name & "</td>")
            sb.Append("</tr>")
            sb.Append("<tr>") 'SalesmanCode
            sb.Append("<td height=10 width='25%'>Kode Employee</td>")
            sb.Append("<td height=10 width='5%'>:</td>")
            sb.Append("<td height=10 width='75%'>" & objSalesmanHeader.SalesmanCode & "</td>")
            sb.Append("</tr>")
            sb.Append("<tr>")
            sb.Append("<td colspan=5 height=10></td>")
            sb.Append("</tr>")

            sb.Append("</table>")
            sb.Append("<table border='0' cellpadding='1' cellspacing='0' width='100%'>")
            sb.Append("<tr>")
            sb.Append("<td width='20%' align='center' bgcolor='#CCCCCC' style='border-width: thin; border-color: #000000; border-style: solid solid solid solid'><b>Item Data</b></td>")
            sb.Append("<td width='40%' align='center' bgcolor='#CCCCCC' style='border-width: thin; border-color: #000000; border-style: solid solid solid none'><b>Data Lama</b></td>")
            sb.Append("<td width='40%' align='center' bgcolor='#CCCCCC' style='border-width: thin; border-color: #000000; border-style: solid solid solid none'><b>Data Baru</b></td>")
            sb.Append("</tr>")

            sb.Append("<tr>")
            sb.Append("<td width='20%' style='border-width: thin; border-color: #000000; border-style: none solid solid solid'>Kategori</td>")
            sb.Append("<td width='40%' style='border-width: thin; border-color: #000000; border-style: none solid solid none'>" & CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName & "</td>")
            sb.Append("<td width='40%' style='border-width: thin; border-color: #000000; border-style: none solid solid none'>" & oSalesPartHistory.SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName & "</td>")
            sb.Append("</tr>")

            sb.Append("<tr>")
            sb.Append("<td width='20%' style='border-width: thin; border-color: #000000; border-style: none solid solid solid'>Posisi</td>")
            sb.Append("<td width='40%' style='border-width: thin; border-color: #000000; border-style: none solid solid none'>" & CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.PositionName & "</td>")
            sb.Append("<td width='40%' style='border-width: thin; border-color: #000000; border-style: none solid solid none'>" & oSalesPartHistory.SalesmanCategoryLevel.PositionName & "</td>")
            sb.Append("</tr>")

            sb.Append("<tr>")
            sb.Append("<td width='20%'style='border-width: thin; border-color: #000000; border-style: none solid solid solid'>Level</td>")
            Dim EnumOldLevel As EnumSalesmanPartLevel.Level = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanLevel
            If EnumOldLevel <> 99 Then
                sb.Append("<td width='40%' style='border-width: thin; border-color: #000000; border-style: none solid solid none'>" & EnumOldLevel.ToString & "</td>")
            Else
                sb.Append("<td width='40%' style='border-width: thin; border-color: #000000; border-style: none solid solid none'>&nbsp;</td>")
            End If
            Dim EnumNewLevel As EnumSalesmanPartLevel.Level = oSalesPartHistory.SalesmanLevel
            If EnumNewLevel <> 99 Then
                sb.Append("<td width='40%' style='border-width: thin; border-color: #000000; border-style: none solid solid none'>" & EnumNewLevel.ToString & "</td>")
            Else
                sb.Append("<td width='40%' style='border-width: thin; border-color: #000000; border-style: none solid solid none'>&nbsp;</td>")
            End If
            sb.Append("</tr>")
            sb.Append("<tr>")
            sb.Append("<td colspan='3'>&nbsp;</td>")
            sb.Append("</tr>")

            Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            sb.Append("<tr>")
            sb.Append("<td width='35%'>Diajukan oleh</td>")
            sb.Append("<td width='5%'>:</td>")
            sb.Append("<td width='60%'><b>" & objUser.Dealer.DealerCode & " - " & objUser.UserName & "</b></td>")
            sb.Append("</tr>")

            sb.Append("</table>")

            sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")

            sb.Append("<tr>")
            sb.Append("<td width='100%'>Untuk menyetujui atau menolak perubahan data Part Employee diatas, dapat diakses pada link dibawah ini:</td>")
            sb.Append("</tr>")

            sb.Append("<tr>")
            sb.Append("<td >&nbsp;</td>")
            sb.Append("</tr>")

            sb.Append("<tr>")
            sb.Append("<td width='100%'><font color='blue'><a href='" & urlPartEmpList & "'>MMKSI DNet Sparepart</a></font></td>")
            sb.Append("</tr>")
            sb.Append("</table>")
            sb.Append("</FONT>")

            Return sb.ToString
        Catch ex As Exception
            Return String.Empty
        End Try


    End Function

    Private Function IsKTPExist() As Boolean
        Dim ktp As String = ""
        Dim objExist As SalesmanHeader

        Dim critKTP As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
        critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, ktp))

        Dim sortCollKTP As SortCollection = New SortCollection
        sortCollKTP.Add(New Sort(GetType(SalesmanProfile), "CreatedTime", Sort.SortDirection.DESC))

        Dim arrKTP As ArrayList = New SalesmanProfileFacade(User).Retrieve(critKTP, sortCollKTP)
        If arrKTP.Count > 0 Then
            ktp = ktp.Trim.Replace(".", "")
            For Each sp As SalesmanProfile In arrKTP
                Dim ktpValue As String = sp.ProfileValue.Trim.Replace(".", "")
                If (ktpValue = ktp) Then
                    objExist = sp.SalesmanHeader
                    Exit For
                End If
            Next
        End If

        If (objExist.ID > 0) Then
            If objExist.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String) Then
                'sessHelper.SetSession("SALESREFERENCE", objExist)
                MessageBox.Confirm("Nomor KTP atas nama " & objExist.Name & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & ") - " & objExist.SalesmanCode & " di dealer " & objExist.Dealer.DealerName & " )\n Apakah anda akan tetap menginput data?", "hdnVal")
            Else
                MessageBox.Show("Nomor KTP atas nama  " & objExist.Name & " - " & objExist.SalesmanCode & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & "), \n masih aktif terdaftar di dealer " & objExist.Dealer.DealerName)
                Return False
            End If
        End If

        Return False
    End Function

#End Region

#Region "EventHandlers"
    'Dim objTmpSalesmanHeader As SalesmanHeader

    Private Sub RenderProfilePanel(ByVal objSalesmanHeader As SalesmanHeader, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel)
        If Request.QueryString("ID") = String.Empty Or Request.QueryString("ID") = "" Then
            isReadOnlyProfile = False
        Else
            If Request.QueryString("edit") = String.Empty Or Request.QueryString("edit") = "" Then
                isReadOnlyProfile = True
            Else
                Dim objDealer As Dealer
                objDealer = Session.Item("DEALER")
                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    isReadOnlyProfile = True
                Else
                    isReadOnlyProfile = False
                End If
            End If
        End If

        Dim objRenderPanel As RenderingProfile = New RenderingProfile(isReadOnlyProfile)

        If Not objSalesmanHeader Is Nothing Then
            objRenderPanel.GeneratePanel(objSalesmanHeader.ID, objPanel, objGroup, profileType, User, isReadOnlyProfile)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User, isReadOnlyProfile)
        End If
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        'SmartNavigation = True
        If Val(sessHelper.GetSession("IsInsertSuccess")) = 1 Then
            IsInsertSuccess.Value = "1"
            sessHelper.SetSession("IsInsertSuccess", "")
        Else
            IsInsertSuccess.Value = ""
            sessHelper.SetSession("IsInsertSuccess", "")
        End If

        If Not IsPostBack Then
            If Not IsExistDealerAdditional() Then
                'btnSimpan.Enabled = False
                'btnRequestID.Enabled = False
                'btnBatal.Enabled = False
                'btnSearch.Enabled = False
                'Exit Sub
            End If
            SetSession()
            BindDropDown()
            BindControlsAttribute()
            Initialize()
            objDealerTmp = sessHelper.GetSession("DEALER")

            If CType(Request.QueryString("id"), Integer) < 0 Or Request.QueryString("id") = String.Empty Then
                _salesmanHeader = New SalesmanHeader
                ViewState("vsProcess") = "Insert"
                btnRequestID.Visible = False
                lblRemoveImage.Visible = False
                ddlStatus.Visible = False
                btnBatal.Visible = True
                Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
                lblDealerCode.Text = objuser.Dealer.DealerCode
                lblDealerName.Text = objuser.Dealer.DealerName

                Try
                    Dim oDealerAdditional As DealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(objuser.Dealer.ID)(0)
                    If Not oDealerAdditional Is Nothing Then
                        lblGradeDealer.Text = oDealerAdditional.SparePartGrade
                    End If
                Catch
                    lblGradeDealer.Text = String.Empty
                End Try


                SetLabel(False)

                sessHelper.SetSession("arrHistoryToInsert", New ArrayList)
                BindDgHistory(0)

                If blnSaved Then
                    MessageBox.Show(SR.SaveSuccess & ", Silakan melengkapi data lainnya")
                End If
            Else
                SetLabel(False)
                'lblPartshop.Visible = True

                If Request.QueryString("edit") <> String.Empty Then
                    View(CInt(Request.QueryString("id")), True)
                    VisibleControl(True)
                    If Request.QueryString("edit") = "true" Then
                        txtDealerBranchCode.Enabled = False
                        lblPopUpDealerBranch.Visible = False
                        ViewState("vsProcess") = "Edit"
                    Else
                        ViewState("vsProcess") = "Insert"
                    End If
                Else
                    View(CInt(Request.QueryString("id")), False)
                    VisibleControl(False)
                    ViewState("vsProcess") = "View"
                End If
                btnBatal.Visible = True
                sessHelper.SetSession("HeaderID", Request.QueryString("ID"))
                _salesmanHeader = New SalesmanHeaderFacade(User).Retrieve(Request.QueryString("ID"))
                'BindDgPartshop(0)

            End If

            If objDealerTmp.Title <> EnumDealerTittle.DealerTittle.KTB And ViewState("vsProcess") = "Edit" Then
                ddlJobPositionDesc.Enabled = False
            End If
        Else
            If Not String.IsNullOrEmpty(txtDealerBranchCode.Text.Trim()) Then
                Dim objDealerBranch As DealerBranch = New DealerBranchFacade(User).Retrieve(txtDealerBranchCode.Text, lblDealerCode.Text)
                If Not IsNothing(objDealerBranch) Then
                    txtBranchName.Text = objDealerBranch.Name
                End If
            End If
        End If
        If Not IsNothing(Request.QueryString("SalesResign")) Then
            txtRefSalesman.Text = Request.QueryString("SalesResign")
            ddlStatus.SelectedValue = EnumSalesmanStatus.SalesmanStatus.Baru
        End If
        ProcessPhoto()
        SetSetting()

        If Val(hdnVal.Value) = 1 Then
            hdnVal.Value = "0"
            'Dim objSH As SalesmanHeader = CType(sessHelper.GetSession("SALESREFERENCE"), SalesmanHeader)
            'Response.Redirect("FrmSalesmanPart.aspx?ID=" + objSH.ID.ToString + "&edit=false" + "&Mode=part&Konfirmasi=True&SalesResign=" + objSH.SalesmanCode)
            'Page.RegisterStartupScript("test", "<script language=JavaScript> showPopUp('../PopUp/PopUpDownloadEstimate.aspx?term2=" & objDealer.SearchTerm2.ToUpper.Trim & ",'',400,400,KodeTipe); </script>")
            ProcessSave()
        End If

        txtRefSalesman.Attributes.Add("readonly", "readonly")
        lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
    End Sub

    Private Function IsExistDealerAdditional() As Boolean
        Dim objDealer As Dealer
        Dim oDealerAdditional As DealerAdditional
        Try
            If IsNothing(Request.QueryString("ID")) Then
                objDealer = Session.Item("DEALER")
                oDealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(objDealer.ID)(0)
            Else
                If IsNothing(Request.QueryString("SalesResign")) Then
                    Dim salesHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CType(Request.QueryString("ID"), Integer))
                    If Not salesHeader Is Nothing Then
                        oDealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(salesHeader.Dealer.ID)(0)
                    Else
                        objDealer = Session.Item("DEALER")
                        oDealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(objDealer.ID)(0)
                    End If
                Else
                    objDealer = Session.Item("DEALER")
                    oDealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(objDealer.ID)(0)
                End If
            End If

            'If oDealerAdditional.ID > 0 Then
            '    Return True
            'Else
            '    MessageBox.Show("Data Informasi Grade Dealer belum ditentukan. Hubungi admin DNET.")
            '    Return False
            'End If
        Catch
        End Try
        If True Then

        End If


        Dim criterias As CriteriaComposite
        Dim sparePartGrade As String = String.Empty
        If Not IsNothing(oDealerAdditional) Then
            sparePartGrade = oDealerAdditional.SparePartGrade
        End If

        If sparePartGrade.IsNullorEmpty Then
            criterias = New CriteriaComposite(New Criteria(GetType(V_SparePartOrganization), "Grade", MatchType.Exact, "A"))
        Else
            criterias = New CriteriaComposite(New Criteria(GetType(V_SparePartOrganization), "Grade", MatchType.Exact, sparePartGrade))
        End If
        criterias.opAnd(New Criteria(GetType(V_SparePartOrganization), "LevelNumber", MatchType.Exact, 1))
        Dim arlCtgLevel As ArrayList = New V_SparePartOrganizationFacade(User).Retrieve(criterias)

        ddlJobPositionDesc.Items.Clear()
        For Each obj As V_SparePartOrganization In arlCtgLevel
            'If obj.LevelNumber = 1 Then
            ddlJobPositionDesc.Items.Add(New ListItem(obj.PositionName, obj.SalesmanCategoryLevelID))
            'End If
        Next
        If ddlJobPositionDesc.Items.Count > 0 Then
            ddlJobPositionDesc.SelectedIndex = 0
            ddlJobPositionDesc_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Function

    Private Sub ddlPropinsi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPropinsi.SelectedIndexChanged
        If ddlPropinsi.SelectedValue <> "" Then
            CommonFunction.BindCity(ddlKota, User, True, ddlPropinsi.SelectedValue, False)
        Else
            ddlKota.Items.Clear()
        End If
    End Sub

    'Private Sub dgPartshop_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPartshop.ItemCommand
    '    If e.CommandName = "delete" Then
    '        Dim facade As SalesmanPartShopFacade = New SalesmanPartShopFacade(User)
    '        Dim objSalesmanPartShop As SalesmanPartShop = facade.Retrieve(CInt(e.CommandArgument))
    '        Dim result As Integer = facade.DeleteFromDB(objSalesmanPartShop)
    '        'BindDgPartshop(0)
    '    End If

    '    If e.CommandName = "add" Then
    '        Dim facade As SalesmanPartShopFacade = New SalesmanPartShopFacade(User)
    '        Dim txtPartShopCode As TextBox = e.Item.FindControl("txtPartShopCode")
    '        If txtPartShopCode.Text = "" Then
    '            MessageBox.Show("Isi Kode Part Shop terlebih dahulu !")
    '            Return
    '        End If

    '        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartShop), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '        criterias.opAnd(New Criteria(GetType(SalesmanPartShop), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))
    '        criterias.opAnd(New Criteria(GetType(SalesmanPartShop), "PartShop.PartShopCode", MatchType.Exact, txtPartShopCode.Text))

    '        Dim arlSalesmanPartShop As ArrayList = facade.RetrieveByCriteria(criterias)

    '        If arlSalesmanPartShop.Count > 0 Then
    '            MessageBox.Show("Area Tidak Boleh Dobel !")
    '        Else
    '            Dim criteriasPart As New CriteriaComposite(New Criteria(GetType(PartShop), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '            criteriasPart.opAnd(New Criteria(GetType(PartShop), "PartShopCode", MatchType.Exact, txtPartShopCode.Text))

    '            Dim objPartShop As PartShop = New PartShopFacade(User).RetrieveByCriteria(criteriasPart)(0)
    '            If objPartShop Is Nothing Then
    '                MessageBox.Show("Area Yang Anda Masukkan Tidak Ada !")
    '            Else
    '                Dim objSalesmanPartShop As SalesmanPartShop = New SalesmanPartShop
    '                objSalesmanPartShop.PartShop = objPartShop
    '                objSalesmanPartShop.SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
    '                Dim result As Integer = facade.Insert(objSalesmanPartShop)
    '                'BindDgPartshop(0)
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub dgPartshop_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPartshop.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortColPartshop") Then
            If sessHelper.GetSession("SortDirectionPartshop") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirectionPartshop", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirectionPartshop", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortColPartshop", e.SortExpression)
        dgPartshop.SelectedIndex = -1
        'BindDgPartshop(dgPartshop.CurrentPageIndex)
    End Sub

    'Private Sub dgPartshop_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPartshop.PageIndexChanged
    '    dgPartshop.CurrentPageIndex = e.NewPageIndex
    '    'BindDgPartshop(dgPartshop.CurrentPageIndex)
    'End Sub

    'Private Sub dgPartshop_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartshop.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Footer Then
    '        Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblPartshopCode"), Label)
    '        lblPopUp.Attributes("onclick") = "ShowPopUpPartShop();"

    '    End If

    '    'If e.Item.ItemIndex <> -1 Then
    '    '    If Not (arrArea Is Nothing) Then
    '    '        Dim objSalesmanAreaAssign As SalesmanAreaAssign
    '    '        objSalesmanAreaAssign = arrArea(e.Item.ItemIndex)

    '    '        e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgArea.CurrentPageIndex * dtgArea.PageSize)

    '    '        Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
    '    '        _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
    '    '        _lbtnDelete.CommandArgument = objSalesmanAreaAssign.ID

    '    '    End If
    '    'End If
    'End Sub

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
    Private Sub dtgTraining_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
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
    Private Sub dtgTraining_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
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
        Dim objSalesmanHeader As SalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader)
        objSalesmanHeader.IsRequestID = EnumSalesmanIsRequest.SalesmanIsRequest.Sudah_Request

        Dim nResult As Integer = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)

        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
            btnRequestID.Enabled = True
        Else
            MessageBox.Show(SR.SaveSuccess)
            SendEmail(objSalesmanHeader, True)
            btnRequestID.Enabled = False
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
    Private Sub dtgTraining_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
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
    Private Sub dtgTraining_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs)
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
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Response.Redirect("FrmSalesmanPartList.aspx?Mode=part")
    End Sub
    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub
    Private Sub lblRemoveImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblRemoveImage.Click
        sessHelper.SetSession(sessImageByte, Nothing)
        photoView.ImageUrl = String.Empty
        lblRemoveImage.Visible = False
        photoSrc.Disabled = False
    End Sub
    Private Sub ddlJobPositionDesc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlJobPositionDesc.SelectedIndexChanged
        ddlSalesmanLevel.Items.Clear()
        Dim criterias As CriteriaComposite
        Dim salesHeader As SalesmanHeader

        Dim objDealer As Dealer
        Dim oDealerAdditional As DealerAdditional
        Try
            If IsNothing(Request.QueryString("ID")) Then
                objDealer = Session.Item("DEALER")
                oDealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(objDealer.ID)(0)
            Else
                If IsNothing(Request.QueryString("SalesResign")) Then
                    salesHeader = New SalesmanHeaderFacade(User).Retrieve(CType(Request.QueryString("ID"), Integer))
                    If Not salesHeader Is Nothing Then
                        oDealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(salesHeader.Dealer.ID)(0)
                    Else
                        objDealer = Session.Item("DEALER")
                        oDealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(objDealer.ID)(0)
                    End If
                Else
                    objDealer = Session.Item("DEALER")
                    oDealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(objDealer.ID)(0)
                End If


            End If
        Catch
        End Try
        Dim partGrade As String = String.Empty
        If Not IsNothing(oDealerAdditional) Then
            partGrade = oDealerAdditional.SparePartGrade
        End If


        'Dim objDealer As Dealer = Session.Item("DEALER")
        'Dim oDealerAdditional As DealerAdditional = New DealerAdditionalFacade(User).RetrieveByDealerID(objDealer.ID)(0)
        If partGrade.IsNullorEmpty Then
            criterias = New CriteriaComposite(New Criteria(GetType(V_SparePartOrganization), "Grade", MatchType.Exact, "A"))
        Else
            criterias = New CriteriaComposite(New Criteria(GetType(V_SparePartOrganization), "Grade", MatchType.Exact, partGrade))
        End If
        criterias.opAnd(New Criteria(GetType(V_SparePartOrganization), "LevelNumber", MatchType.Exact, 2))
        Dim arlCtgLevel As ArrayList = New V_SparePartOrganizationFacade(User).Retrieve(criterias)
        'ddlSalesmanLevel.Items.Add(New ListItem("Silahkan Pilih", 0))
        For Each obj As V_SparePartOrganization In arlCtgLevel
            If ddlJobPositionDesc.SelectedValue <> "" Then
                If obj.ParentID = CType(ddlJobPositionDesc.SelectedValue, Integer) Then
                    ddlSalesmanLevel.Items.Add(New ListItem(obj.PositionName, obj.SalesmanCategoryLevelID))
                End If
            End If
        Next
        If ddlSalesmanLevel.Items.Count > 0 Then
            ddlSalesmanLevel.SelectedIndex = 0
            ddlSalesmanLevel_SelectedIndexChanged(Nothing, Nothing)
        End If

    End Sub

    Private Sub ddlSalesmanLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSalesmanLevel.SelectedIndexChanged
        'khusus untuk sales yang ada level nya
        If ddlSalesmanLevel.SelectedValue <> "" Then
            If CType(ddlSalesmanLevel.SelectedValue, Integer) = 12 Then
                lblLevelText.Visible = True
                lblLevelSeparator.Visible = True
                ddlGrade.Visible = True
                'BindDgPartshop(0)
            Else
                lblLevelText.Visible = False
                lblLevelSeparator.Visible = False
                ddlGrade.Visible = False
                'dgPartshop.DataSource = Nothing
                'dgPartshop.DataBind()
            End If
            Dim objDealer As Dealer = Session.Item("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                ddlGrade.Enabled = False
            Else
                ddlGrade.Enabled = True
            End If

        End If


    End Sub

    Private Sub lnkReloadSalesman_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkReloadSalesman.Click
        If txtRefSalesman.Text = "" Then
            MessageBox.Show("Pilih user yang telah resign terlebih dahulu")
        Else
            'isRegistered = True
            Dim objSalesmanHeaderReload As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtRefSalesman.Text)
            If Not IsNothing(objSalesmanHeaderReload) Then
                If Request.QueryString("SalesResign") = "" Then
                    Response.Redirect("FrmSalesmanPart.aspx?ID=" + objSalesmanHeaderReload.ID.ToString + "&edit=false" + "&Mode=part&Konfirmasi=True&SalesResign=" + txtRefSalesman.Text)
                End If
            Else
                MessageBox.Show("User Reference tidak valid, silakan gunakan pop up")
            End If
        End If
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        'Validation
        If Not Page.IsValid Then
            MessageBox.Show("Data Belum Lengkap")
            Return
        End If
        If ICStartWork.Value > DateTime.Now Then
            MessageBox.Show("Tanggal Masuk Tidak boleh lebih besar dari Hari ini")
            Return
        End If
        Dim ktp As String = ""
        Dim email As String = ""

        Dim objDealer As Dealer = Session.Item("DEALER")

        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            ' Validate email address
            Dim emailAddressValidationMessage As String
            emailAddressValidationMessage = msgErrorEmailValidation()
            If (emailAddressValidationMessage <> "") Then
                MessageBox.Show(emailAddressValidationMessage)
                Return
            End If

            ' Validate No HP
            Dim noHPValidationMessage As String
            noHPValidationMessage = msgErrorNOHPValidation()
            If (noHPValidationMessage <> "") Then
                MessageBox.Show(noHPValidationMessage)
                Return
            End If
        End If
        If 1 = 1 Then

            'Dim objRenderPanel As RenderingProfile = New RenderingProfile
            'Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
            'Dim objListProfile As ArrayList = objRenderPanel.RetrieveProfileValue(Me, objProfileGroup, CType(EnumProfileType.ProfileType.SALESMAN, Short), User)
            'For Each item As SalesmanProfile In objListProfile
            '    If item.ProfileHeader.ID = 29 Then
            '        ktp = item.ProfileValue
            '        Exit For
            '    End If
            'Next

            'If ktp.Replace(" ", "").Trim().Length <> 16 Then
            '    MessageBox.Show("Format No KTP Salah")
            '    Return
            'Else
            '    Dim valid As Boolean = False

            '    For Each ObjChar As Char In ktp
            '        If Not IsNumeric(ObjChar) Then
            '            MessageBox.Show("Format No KTP Salah")
            '            Return
            '        End If
            '    Next


            'End If

            If CType(ViewState("vsProcess"), String) <> "Insert" AndAlso objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                Dim objExist As New SalesmanHeader
                Dim ObjSalesmanHeaderID As Integer = Session.Item("HeaderID")


                Dim critKTP As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, ktp))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.No, ObjSalesmanHeaderID))

                Dim sortCollKTP As SortCollection = New SortCollection
                sortCollKTP.Add(New Sort(GetType(SalesmanProfile), "CreatedTime", Sort.SortDirection.DESC))

                Dim arrKTP As ArrayList = New SalesmanProfileFacade(User).Retrieve(critKTP, sortCollKTP)
                If arrKTP.Count > 0 Then
                    ktp = ktp.Trim.Replace(".", "")
                    For Each sp As SalesmanProfile In arrKTP
                        Dim ktpValue As String = sp.ProfileValue.Trim.Replace(".", "")
                        If (ktpValue = ktp) Then
                            objExist = sp.SalesmanHeader
                            Exit For
                        End If
                    Next
                End If

                If (objExist.ID > 0 AndAlso objExist.RowStatus = 0) Then
                    If objExist.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String) Then
                        'sessHelper.SetSession("SALESREFERENCE", objExist)
                        MessageBox.Confirm("Nomor KTP atas nama " & objExist.Name & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & ") - " & objExist.SalesmanCode & " di dealer " & objExist.Dealer.DealerName & " )\n Apakah anda akan tetap menginput data?", "hdnVal")
                    Else
                        MessageBox.Show("Nomor KTP atas nama  " & objExist.Name & " - " & objExist.SalesmanCode & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & "), \n masih aktif terdaftar di dealer " & objExist.Dealer.DealerName)
                        Return
                    End If

                End If
            End If
        End If
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            Dim commonFunction As CommonFunction = New CommonFunction()
            Dim objRenderPanel As RenderingProfile = New RenderingProfile
            Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
            Dim objListProfile As ArrayList = objRenderPanel.RetrieveProfileValue(Me, objProfileGroup, CType(EnumProfileType.ProfileType.SALESMAN, Short), User)
            Dim isValidEmail As Boolean

            For Each item As SalesmanProfile In objListProfile
                If item.ProfileHeader.ID = 29 Then
                    ktp = item.ProfileValue
                    Exit For
                End If
            Next

            For Each item As SalesmanProfile In objListProfile
                If item.ProfileHeader.ID = 26 Then
                    email = item.ProfileValue
                    Exit For
                End If
            Next

            If (email = "") Then
                MessageBox.Show("Email harus diisi!")
                Return
            End If

            isValidEmail = ValidateEmail(email)
            If (isValidEmail = False) Then
                MessageBox.Show("Format 'Email' Salah!")
                Return
            End If

            If ktp.Replace(" ", "").Trim().Length <> 16 Then
                MessageBox.Show("Format No KTP Salah")
                Return
            Else
                Dim valid As Boolean = False

                For Each ObjChar As Char In ktp
                    If Not IsNumeric(ObjChar) Then
                        MessageBox.Show("Format No KTP Salah")
                        Return
                    End If
                Next


            End If

            Dim objExist As New SalesmanHeader

            Dim critKTP As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
            critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, ktp))

            Dim sortCollKTP As SortCollection = New SortCollection
            sortCollKTP.Add(New Sort(GetType(SalesmanProfile), "CreatedTime", Sort.SortDirection.DESC))

            Dim arrKTP As ArrayList = New SalesmanProfileFacade(User).Retrieve(critKTP, sortCollKTP)
            If arrKTP.Count > 0 Then
                ktp = ktp.Trim.Replace(".", "")
                For Each sp As SalesmanProfile In arrKTP
                    Dim ktpValue As String = sp.ProfileValue.Trim.Replace(".", "")
                    If (ktpValue = ktp) Then
                        objExist = sp.SalesmanHeader
                        Exit For
                    End If
                Next
            End If

            ProcessSave()
            If (objExist.ID > 0) Then
                If objExist.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String) Or objExist.RowStatus <> 0 Then
                    'sessHelper.SetSession("SALESREFERENCE", objExist)
                    MessageBox.Confirm("Nomor KTP atas nama " & objExist.Name & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & ") - " & objExist.SalesmanCode & " di dealer " & objExist.Dealer.DealerName & " )\n Apakah anda akan tetap menginput data?", "hdnVal")
                Else
                    MessageBox.Show("Nomor KTP atas nama  " & objExist.Name & " - " & objExist.SalesmanCode & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & "), \n sudah pernah terdaftar di dealer " & objExist.Dealer.DealerName)
                    Return
                End If
            Else
                ProcessSave()
            End If

        Else
            ProcessSave()
        End If

    End Sub

    'Private Sub dgPartshop_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPartshop.ItemCommand
    '    Select Case (e.CommandName)
    '        Case "add"
    '            AddPartshopCommand(e)
    '        Case "update"
    '            UpdatePartshopCommand(e)
    '        Case "delete"
    '            Dim lShouldReturn As Boolean
    '            DeletePartshopCommand(e, lShouldReturn)
    '            If lShouldReturn Then
    '                Return
    '            End If
    '    End Select
    'End Sub

    'Private Sub AddPartshopCommand(ByVal e As DataGridCommandEventArgs)
    '    'If IsValidParameter() Then
    '    Dim arl As ArrayList = New ArrayList
    '    arl = sessHelper.GetSession("EmpPartshop")

    '    Dim txtPartShopCode As TextBox = e.Item.FindControl("txtPartShopCode")
    '    If txtPartShopCode.Text.Trim = "" Then
    '        MessageBox.Show("Kode Partshop tidak boleh kosong")
    '        Exit Sub
    '    End If

    '    Dim objPartshop As PartShop = New PartShopFacade(User).Retrieve(txtPartShopCode.Text)
    '    If Not objPartshop Is Nothing Then
    '        Dim objSalesmanPartshop As New SalesmanPartShop
    '        If (Not _salesmanHeader Is Nothing) AndAlso _salesmanHeader.ID > 0 Then
    '            objSalesmanPartshop.SalesmanHeader = _salesmanHeader
    '        End If
    '        objSalesmanPartshop.PartShop = objPartshop
    '        'Insert the third person, but first check if they already exists.
    '        Dim isExist As Boolean = False
    '        'If Not arl Is Nothing Then
    '        Try
    '            If arl.Count > 0 Then
    '                For Each item As SalesmanPartShop In arl
    '                    If item.PartShop.PartShopCode = objSalesmanPartshop.PartShop.PartShopCode Then
    '                        isExist = True
    '                    End If
    '                Next
    '            Else
    '                arl = New ArrayList
    '            End If
    '        Catch ex As Exception
    '            arl = New ArrayList
    '        End Try

    '        If Not isExist Then
    '            arl.Add(objPartshop)
    '            sessHelper.SetSession("EmpPartshop", arl)
    '            'BindDgPartshop(0)
    '        Else
    '            MessageBox.Show("Kode partshop sudah ada di daftar")
    '        End If
    '        'End If

    '    Else
    '        MessageBox.Show("Tidak ada partshop dengan kode tersebut")
    '    End If

    '    'End If

    'End Sub

    'Private Sub UpdatePartshopCommand(ByVal e As DataGridCommandEventArgs)
    '    'If IsValidParameter() Then
    '    '    Dim arl As ArrayList = sessHelper.GetSession("EmpPerformance")

    '    '    Dim txtSalesmanCode As TextBox = e.Item.FindControl("txtSalesmanCodeE")
    '    '    Dim txtHargaJual As TextBox = e.Item.FindControl("txtHargaJualE")
    '    '    Dim txtHargaPokok As TextBox = e.Item.FindControl("txtHargaPokokE")
    '    '    'Dim txtIncentive As TextBox = e.Item.FindControl("txtIncentiveE")


    '    '    If txtSalesmanCode.Text.Trim = "" Then
    '    '        MessageBox.Show("Kode salesman tidak boleh kosong")
    '    '        Exit Sub
    '    '    End If
    '    '    If txtHargaJual.Text.Trim = "" Then
    '    '        MessageBox.Show("Nilai Harga Jual tidak boleh kosong")
    '    '        Exit Sub
    '    '    End If
    '    '    If txtHargaPokok.Text.Trim = "" Then
    '    '        MessageBox.Show("Nilai Harga Pokok tidak boleh kosong")
    '    '        Exit Sub
    '    '    End If

    '    '    'If txtIncentive.Text.Trim = "" Then
    '    '    '    MessageBox.Show("Nilai Incentive tidak boleh kosong")
    '    '    '    Exit Sub
    '    '    'End If

    '    '    Dim objSales As SalesmanHeader = New SalesmanHeaderFacade(User).RetrieveByCode(txtSalesmanCode.Text)
    '    '    If Not objSales Is Nothing Then
    '    '        Dim objPerformance As New SalesmanPartPerformance

    '    '        objPerformance = CType(arl.Item(e.Item.ItemIndex), SalesmanPartPerformance)
    '    '        objPerformance.Year = ddlYear.SelectedValue
    '    '        objPerformance.Month = ddlMonth.SelectedValue
    '    '        objPerformance.Period = New Date(CType(ddlYear.SelectedValue, Integer), CType(ddlMonth.SelectedValue, Integer), 1, 0, 0, 0)
    '    '        objPerformance.HargaJual = CType(txtHargaJual.Text.Trim, Decimal)
    '    '        objPerformance.HargaPokok = CType(txtHargaPokok.Text.Trim, Decimal)
    '    '        objPerformance.Profit = objPerformance.HargaJual - objPerformance.HargaPokok
    '    '        If objPerformance.HargaJual > 0 Then
    '    '            objPerformance.Percentage = System.Math.Round((objPerformance.Profit / objPerformance.HargaJual) * 100, 2)
    '    '        Else
    '    '            objPerformance.Percentage = 0
    '    '        End If

    '    '        arl.Item(e.Item.ItemIndex) = objPerformance

    '    '        dgSalesmanPerformance.ShowFooter = True
    '    '        dgSalesmanPerformance.EditItemIndex = -1
    '    '        BindEmployeePerformance(dgSalesmanPerformance.CurrentPageIndex)
    '    '    Else
    '    '        MessageBox.Show("Tidak ada Salesman dengan kode tersebut")
    '    '    End If

    '    'End If

    'End Sub

    'Private Sub DeletePartshopCommand(ByVal e As DataGridCommandEventArgs, ByRef shouldReturn As Boolean)
    '    'shouldReturn = False
    '    'Dim lblSalesmanCode As Label = e.Item.FindControl("lblSalesmanCode")

    '    'Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartPerformance), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    'criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "SalesmanHeader.SalesmanCode", MatchType.Exact, lblSalesmanCode.Text.Trim))
    '    'criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "SalesmanHeader.Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
    '    'criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "Year", MatchType.Exact, ddlYear.SelectedValue))
    '    'criterias.opAnd(New Criteria(GetType(SalesmanPartPerformance), "Month", MatchType.Exact, ddlMonth.SelectedValue))

    '    'Dim arlSalesPerformance As ArrayList = oSalesPerformanceFacade.RetrieveByCriteria(criterias)
    '    'If arlSalesPerformance.Count > 0 Then
    '    '    Dim objSalesPerformance As SalesmanPartPerformance = arlSalesPerformance(0)
    '    '    oSalesPerformanceFacade.Delete(objSalesPerformance)
    '    'End If
    '    'Dim arl As ArrayList = sessHelper.GetSession("EmpPerformance")
    '    'arl.RemoveAt(e.Item.ItemIndex)
    '    'BindEmployeePerformance(dgSalesmanPerformance.CurrentPageIndex)
    'End Sub


#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        Dim objDealer As Dealer = Session.Item("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Request.QueryString("Mode") = "part" Then
                If CType(Request.QueryString("id"), Integer) = 0 Then
                    If Not SecurityProvider.Authorize(Context.User, SR.Input_data_salesman_part_privilege) Then
                        Server.Transfer("../FrmAccessDenied.aspx?modulName=Part Employee - Buat Part Employee")
                    End If
                    btnSimpan.Enabled = SecurityProvider.Authorize(Context.User, SR.Input_data_salesman_part_privilege)
                ElseIf CType(Request.QueryString("id"), Integer) > 0 AndAlso Request.QueryString("edit") = "true" Then
                    If Not SecurityProvider.Authorize(Context.User, SR.Edit_detail_daftar_salesman_part_privilege) Then
                        Server.Transfer("../FrmAccessDenied.aspx?modulName=Part Employee - Edit Part Employee")
                    End If
                    btnSimpan.Enabled = SecurityProvider.Authorize(Context.User, SR.Input_data_salesman_part_privilege)
                End If
            Else
                btnSimpan.Enabled = SecurityProvider.Authorize(Context.User, SR.Buat_buatid_salesman_part_privilege)
            End If
        End If
    End Sub

#End Region

#Region "Internal Enum"
    Private Enum HistoryStatus
        Baru = 0
        Pengajuan = 1
        Disetujui = 2
        Ditolak = 3
        Dibatalkan = 4
        Resign = 5
    End Enum

#End Region

    Private Sub ProcessPhoto()

        If photoSrc.Value <> "" Then
            uploadFile()
        End If

        If Not IsNothing(sessHelper.GetSession(sessImageByte)) Then
            Dim imageByteFromSession As Byte() = CType(sessHelper.GetSession(sessImageByte), Byte())
            photoView.ImageUrl = "data:image;base64," + Convert.ToBase64String(imageByteFromSession)

            lblRemoveImage.Visible = True
            photoSrc.Disabled = True
        Else
            lblRemoveImage.Visible = False
        End If

    End Sub

End Class
