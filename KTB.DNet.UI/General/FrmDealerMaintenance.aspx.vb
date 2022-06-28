Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.Utility.GlobalExtensions
Imports System.Core
Imports System.Linq
Imports System.Collections.Generic

Public Class FrmDealerMaintenance
    Inherits System.Web.UI.Page
    Private m_bChangeDealer_Privilege As Boolean = False
    Private m_bChangeCI_Dealer_Privilege As Boolean = False
    Private m_bChangeFU_Dealer_Privilege As Boolean = False
    Private m_bChangeSV_Dealer_Privilege As Boolean = False
    Private m_bChangeSP_Dealer_Privilege As Boolean = False
    Private m_bChangeFreePPh22_Privilege As Boolean = False
    Private m_DealerColor As Color = White
    Private m_CI_DealerColor As Color = White
    Private m_FU_DealerColor As Color = White
    Private m_SV_DealerColor As Color = White
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents chbxFreePPh As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chlVehicleCategory As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents txtValidFrom As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtValidTo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblFormat As System.Web.UI.WebControls.Label
    Private m_SP_DealerColor As Color = White
    Dim ValidFromDate As DateTime = DateTime.Now
    Protected WithEvents txtLegalStatus As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoPersetujuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents icTglPersetujuan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblMainDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtMainDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNameBM As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmailBM As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHPBM As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPICRegion As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPICArea As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPICSubArea As System.Web.UI.WebControls.TextBox
    Protected WithEvents rdOrgTipeBranch As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents chkSPart As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents chkSvc As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents chkSU As System.Web.UI.WebControls.CheckBoxList

    Protected WithEvents txtNickNameDigital As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNickNameEcomm As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLongitude As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLatitude As System.Web.UI.WebControls.TextBox
    Protected WithEvents rdPublish As System.Web.UI.WebControls.RadioButtonList

    Dim ValidToDate As DateTime = DateTime.Now

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cbSalesUnit As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbService As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbSpareParts As System.Web.UI.WebControls.CheckBox
    Protected WithEvents pnlSales As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlService As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlSpareparts As System.Web.UI.WebControls.Panel
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtAddress As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlProvince As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCity As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPostCode As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtTelpArea As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtTelpNo As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtFaxArea As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtFaxNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtfullNoTelepon As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtfullFax As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmailAdd As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWeb As System.Web.UI.WebControls.TextBox

    Protected WithEvents txtContactPerson11 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP11 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail11 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtContactPerson12 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP12 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail12 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtContactPerson13 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP13 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail13 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtContactPerson14 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP14 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail14 As System.Web.UI.WebControls.TextBox

    Protected WithEvents txtContactPerson21 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP21 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail21 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtContactPerson22 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP22 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail22 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtContactPerson23 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP23 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail23 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtContactPerson24 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP24 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail24 As System.Web.UI.WebControls.TextBox

    Protected WithEvents txtContactPerson31 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP31 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail31 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtContactPerson32 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP32 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail32 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtContactPerson33 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP33 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail33 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtContactPerson34 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHP34 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail34 As System.Web.UI.WebControls.TextBox


    Protected WithEvents trtxtHP12 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtHP13 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtHP14 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtHP22 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtHP23 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtHP24 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtHP32 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtHP33 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtHP34 As System.Web.UI.HtmlControls.HtmlTableRow

    Protected WithEvents trtxtEmail12 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtEmail13 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtEmail14 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtEmail22 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtEmail23 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtEmail24 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtEmail32 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtEmail33 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trtxtEmail34 As System.Web.UI.HtmlControls.HtmlTableRow


    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents txtSearch1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTitle As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSearch2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents ddlArea1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMainArea As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlArea2 As System.Web.UI.WebControls.DropDownList
    Private _dealerID As Integer
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator3 As System.Web.UI.WebControls.RegularExpressionValidator
    'Protected WithEvents RegularExpressionValidator4 As System.Web.UI.WebControls.RegularExpressionValidator
    'Protected WithEvents RegularExpressionValidator5 As System.Web.UI.WebControls.RegularExpressionValidator
    'Protected WithEvents RegularExpressionValidator6 As System.Web.UI.WebControls.RegularExpressionValidator
    'Protected WithEvents RegularExpressionValidator7 As System.Web.UI.WebControls.RegularExpressionValidator
    'Protected WithEvents RegularExpressionValidator9 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblLastUpdate21 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastUpdate22 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastUpdate23 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastUpdate24 As System.Web.UI.WebControls.Label

    Protected WithEvents lblLastUpdate31 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastUpdate32 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastUpdate33 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastUpdate34 As System.Web.UI.WebControls.Label

    Protected WithEvents lblLastUpdate11 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastUpdate12 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastUpdate13 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLastUpdate14 As System.Web.UI.WebControls.Label

    Protected WithEvents LblLastChange As System.Web.UI.WebControls.Label
    Protected WithEvents RegularExpressionValidator8 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator10 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator11 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents RegularExpressionValidator12 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents txtSPANumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSPADate As System.Web.UI.WebControls.TextBox
    Protected WithEvents RegularExpressionValidator13 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RequiredFieldValidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator7 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator8 As System.Web.UI.WebControls.RequiredFieldValidator
    'Protected WithEvents RequiredFieldValidator9 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator10 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator11 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RegularExpressionValidator14 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator15 As System.Web.UI.WebControls.RegularExpressionValidator
    'Protected WithEvents RegularExpressionValidator16 As System.Web.UI.WebControls.RegularExpressionValidator
    'Protected WithEvents RegularExpressionValidator17 As System.Web.UI.WebControls.RegularExpressionValidator
    'Protected WithEvents RegularExpressionValidator18 As System.Web.UI.WebControls.RegularExpressionValidator
    'Protected WithEvents RegularExpressionValidator19 As System.Web.UI.WebControls.RegularExpressionValidator
    'Protected WithEvents RegularExpressionValidator20 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator21 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator22 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator23 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator24 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator25 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator26 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents btnReset As System.Web.UI.WebControls.Button
    Private sessHelper As SessionHelper = New SessionHelper
    Private CreateDealer_Privilege As Boolean = False





    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private intPostBack As Integer = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDdl()
            BindRdOrgTipeBranch()
            BindRdPublish()
            If HasQueryString() Then
                _dealerID = CType(CType(Request.QueryString("dealerid"), String).Trim(), Integer)
                PanelBussinesAreaCheck()
                ShowDataDealer(_dealerID)
                ViewState("vsAction") = "edit"
                lblTitle.Text = "Ubah Organisasi"
                btnReset.Enabled = False
                txtSearch2.MaxLength = 6
                txtSearch2.Width = 60
                ModeReadOnly(Me.Page)
                'RegularExpressionValidator4.Enabled = False
                'RegularExpressionValidator4.IsValid = True
                'RegularExpressionValidator5.Enabled = False
                'RegularExpressionValidator5.IsValid = True
                'RegularExpressionValidator6.Enabled = False
                'RegularExpressionValidator6.IsValid = True
                'RegularExpressionValidator7.Enabled = False
                'RegularExpressionValidator7.IsValid = True
                If Request.QueryString("isupdate") = "1" Then
                    ddlMainArea.Enabled = True
                    ddlArea1.Enabled = True
                    ddlArea2.Enabled = True
                    cbSalesUnit.Enabled = True
                    cbService.Enabled = True
                    cbSpareParts.Enabled = True
                    For Each item As ListItem In chkSU.Items
                        item.Enabled = True
                    Next
                    For Each item As ListItem In chkSvc.Items
                        item.Enabled = True
                    Next
                    For Each item As ListItem In chkSPart.Items
                        item.Enabled = True
                    Next
                    btnSave.Visible = True
                    btnSave.Enabled = True
                End If

                lblSearchDealer.Visible = False
                lblMainDealer.Visible = False
            Else
                'RegularExpressionValidator4.Enabled = True
                'RegularExpressionValidator5.Enabled = True
                'RegularExpressionValidator6.Enabled = True
                'RegularExpressionValidator7.Enabled = True
                ddlTitle.Items.Remove(ddlTitle.Items.FindByValue("0"))
                InitialDealer()
                PanelBussinesAreaCheck()
                ViewState("vsAction") = "insert"
                lblTitle.Text = "Organisasi Baru"
                btnReset.Enabled = True

            End If
            HiddenEmailHP()
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelectionOne();"
            lblMainDealer.Attributes("onclick") = "ShowPPDealerSelectionMainDealer();"
            'intPostBack = intPostBack + 1
        End If
    End Sub

    Private Sub HiddenEmailHP()
        For idy As Integer = 1 To 3
            For idx As Integer = 2 To 4
                Dim trHP As HtmlTableRow = Me.Form.FindControl("trtxtHP" + idy.ToString() + idx.ToString())
                Dim trEmail As HtmlTableRow = Me.Form.FindControl("trtxtEmail" + idy.ToString() + idx.ToString)
                If Not IsNothing(trHP) Then
                    trHP.Visible = False
                End If
                If Not IsNothing(trEmail) Then
                    trEmail.Visible = False
                End If
            Next
        Next

    End Sub


    Public Sub ModeReadOnly(ByVal ctrl As Control)
        If ctrl.HasControls Then
            For Each childCtrl As Control In ctrl.Controls
                ModeReadOnly(childCtrl)
            Next
        Else
            If TypeOf ctrl Is System.Web.UI.WebControls.TextBox Then
                Dim txtBx As TextBox = DirectCast(ctrl, System.Web.UI.WebControls.TextBox)
                txtBx.ReadOnly = 1
                txtBx.BackColor = Color.Gainsboro
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.DropDownList Then
                DirectCast(ctrl, System.Web.UI.WebControls.DropDownList).Enabled = False
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.CheckBox Then
                Dim chk As CheckBox = DirectCast(ctrl, System.Web.UI.WebControls.CheckBox)
                If TypeOf chk.Parent Is CheckBoxList Then
                    For Each iListItem As ListItem In DirectCast(chk.Parent, CheckBoxList).Items
                        iListItem.Enabled = False
                    Next
                Else
                    chk.Enabled = False
                End If
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.Label Then
                Dim lblform As System.Web.UI.WebControls.Label = DirectCast(ctrl, System.Web.UI.WebControls.Label)
                If lblform.HasAttributes Then
                    lblform.Visible = False
                End If
            ElseIf TypeOf ctrl Is System.Web.UI.HtmlControls.HtmlImage Then
                Dim imgform As System.Web.UI.HtmlControls.HtmlImage = DirectCast(ctrl, System.Web.UI.HtmlControls.HtmlImage)
                If imgform.Attributes.Count > 0 Then
                    imgform.Visible = False
                End If
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.FileUpload Then
                DirectCast(ctrl, System.Web.UI.WebControls.FileUpload).Visible = False
            ElseIf TypeOf ctrl Is System.Web.UI.HtmlControls.HtmlInputFile Then
                DirectCast(ctrl, System.Web.UI.HtmlControls.HtmlInputFile).Disabled = True
            ElseIf TypeOf ctrl Is KTB.DNet.WebCC.IntiCalendar Then
                DirectCast(ctrl, KTB.DNet.WebCC.IntiCalendar).Enabled = False
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.LinkButton Then
                Dim lbtn As LinkButton = DirectCast(ctrl, LinkButton)
                If lbtn.Parent IsNot Nothing Then
                    If Not (TypeOf (lbtn.Parent) Is DataGridItem Or TypeOf (lbtn.Parent) Is GridViewRow Or TypeOf (lbtn.Parent) Is TableCell) Then
                        DirectCast(ctrl, LinkButton).Visible = False
                    End If
                Else
                    DirectCast(ctrl, LinkButton).Visible = False
                End If
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.ImageButton Then
                DirectCast(ctrl, System.Web.UI.WebControls.ImageButton).Visible = False
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.RadioButtonList Then
                DirectCast(ctrl, System.Web.UI.WebControls.RadioButtonList).Enabled = False
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.CheckBoxList Then
                Dim chkList As CheckBoxList = DirectCast(ctrl, System.Web.UI.WebControls.CheckBoxList)
                chkList.Enabled = False
                For Each iListItem As ListItem In chkList.Items
                    iListItem.Enabled = False
                Next
            ElseIf TypeOf ctrl Is Label Then
                Dim lblform As Label = DirectCast(ctrl, Label)
                If lblform.HasAttributes Then
                    lblform.Visible = False
                End If
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.Button Then
                Dim btn As System.Web.UI.WebControls.Button = CType(ctrl, System.Web.UI.WebControls.Button)
                If Not btn.Text.ToLower.Contains("kembali") Then
                    btn.Visible = False
                End If

            End If
        End If
    End Sub

    Private Sub ActivateUserPrivilege()

        If Not HasQueryString() Then
            PermitInsertMode()
        Else
            PermitEditMode()
        End If
        m_bChangeFreePPh22_Privilege = SecurityProvider.Authorize(Context.User, SR.ENHAdminMaintainFreePPh22_Privilege)
        'ChangeDealer_Privilege 
        m_bChangeDealer_Privilege = SecurityProvider.Authorize(Context.User, SR.ChangeDealer_Privilege)

        If m_bChangeDealer_Privilege Then
            m_DealerColor = White
        Else
            m_DealerColor = LavenderBlush
        End If

        'ChangeCI_Dealer_Privilege 
        m_bChangeCI_Dealer_Privilege = SecurityProvider.Authorize(Context.User, SR.ChangeDealerContactInfo_Privilege)

        If m_bChangeCI_Dealer_Privilege Then
            m_CI_DealerColor = White
        Else
            m_CI_DealerColor = LavenderBlush
        End If


        'ChangeFU_Dealer_Privilege 
        m_bChangeFU_Dealer_Privilege = SecurityProvider.Authorize(Context.User, SR.ChangeFU_Dealer_Privilege)

        If m_bChangeFU_Dealer_Privilege Then
            m_FU_DealerColor = White
        Else
            m_FU_DealerColor = LavenderBlush
        End If

        'ChangeSV_Dealer_Privilege  
        m_bChangeSV_Dealer_Privilege = SecurityProvider.Authorize(Context.User, SR.ChangeSV_Dealer_Privilege)

        If m_bChangeSV_Dealer_Privilege Then
            m_SV_DealerColor = White
        Else
            m_SV_DealerColor = LavenderBlush
        End If

        'ChangeSP_Dealer_Privilege   
        m_bChangeSP_Dealer_Privilege = SecurityProvider.Authorize(Context.User, SR.ChangeSP_Dealer_Privilege)
        If m_bChangeSP_Dealer_Privilege Then
            m_SP_DealerColor = White
        Else
            m_SP_DealerColor = LavenderBlush
        End If
        chbxFreePPh.Enabled = m_bChangeFreePPh22_Privilege
        txtValidFrom.Enabled = m_bChangeFreePPh22_Privilege
        txtValidTo.Enabled = m_bChangeFreePPh22_Privilege
    End Sub

    Private Sub PermitInsertMode()
        If Not SecurityProvider.Authorize(Context.User, SR.AdminCreateNewOrganization_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SYSTEM - Organisasi Baru")
        End If
    End Sub

    Private Sub PermitEditMode()
        If Not SecurityProvider.Authorize(Context.User, SR.AdminUpdateOrganization_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SYSTEM - Ubah Organisasi")
        End If
    End Sub

    Private Function HasQueryString() As Boolean
        If IsNothing(Request.QueryString("dealerid")) Then
            Return False
        End If
        If Request.QueryString.Count = 0 Then
            Return False
        End If
        Return True
    End Function

    Private Sub InitialDealer()
        Dim objDealer As Dealer = New Dealer
        sessHelper.SetSession("sesDealer", objDealer)
        Dim arrBusinessArea As ArrayList = New ArrayList
        sessHelper.SetSession("sessBusinessArea", arrBusinessArea)

    End Sub
    Private Sub BindDdl()
        BindChlVehicleCategory()
        BindDdlGroup()
        BindDdlProvince()
        BindDdlCity()
        BindDdlTitle()
        BindDdlMainArea()
        BindDdlArea1()
        BindDdlArea2()
        BindDdlStatus()
    End Sub


    Private Sub BindRdOrgTipeBranch()
        Dim category As String = "EnumOrgBranchtype"
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, category))

        Dim sortColl As New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", Sort.SortDirection.ASC))

        rdOrgTipeBranch.DataSource = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        rdOrgTipeBranch.DataTextField = "ValueDesc"
        rdOrgTipeBranch.DataValueField = "ValueID"
        rdOrgTipeBranch.DataBind()
    End Sub

    Private Sub BindRdPublish()
        Dim category As String = "EnumDiscountProposal.DealerDirectSales"
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, category))

        Dim sortColl As New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", Sort.SortDirection.ASC))

        rdPublish.DataSource = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        rdPublish.DataTextField = "ValueDesc"
        rdPublish.DataValueField = "ValueID"
        rdPublish.DataBind()
    End Sub

    Private Sub BindChlVehicleCategory()

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

        Dim al As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
        chlVehicleCategory.DataSource = al
        chlVehicleCategory.DataTextField = "Description"
        chlVehicleCategory.DataValueField = "ID"
        chlVehicleCategory.DataBind()
    End Sub

    Private Sub BindDdlTitle()
        Dim al As ArrayList = New EnumDealerTittle().RetrieveTitle
        For i As Integer = 0 To al.Count - 1
            ddlTitle.Items.Insert(i, New ListItem(al(i).NameTitle, al(i).ValTitle))
        Next
        ddlTitle.Items.Insert(0, New ListItem("Pilih Tipe Organisasi", "-1"))
    End Sub

    Private Sub BindDdlStatus()
        Dim listStatus As New EnumDealerStatus
        Dim al As ArrayList = listStatus.RetrieveStatus
        For Each item As EnumDealer In al
            ddlStatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        Next

    End Sub

    Private Sub BindDdlGroup()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ddlGroup.DataSource = New DealerGroupFacade(User).RetrieveActiveList(criterias, "GroupName", Sort.SortDirection.ASC)
        ddlGroup.DataTextField = "GroupName"
        ddlGroup.DataValueField = "ID"
        ddlGroup.DataBind()
        ddlGroup.Items.Insert(0, New ListItem("", "0"))
    End Sub
    Private Sub BindDdlCity()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A")) 'A = Aktif; X = Tidak Aktif
        ddlCity.DataSource = New CityFacade(User).RetrieveActiveList(criterias, "CityName", Sort.SortDirection.ASC)
        ddlCity.DataTextField = "CityName"
        ddlCity.DataValueField = "ID"
        ddlCity.DataBind()
        ddlCity.Items.Insert(0, New ListItem("", "0"))
    End Sub
    Private Sub BindDdlProvince()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ddlProvince.DataSource = New ProvinceFacade(User).RetrieveActiveList(criterias, "ProvinceName", Sort.SortDirection.ASC)
        ddlProvince.DataTextField = "ProvinceName"
        ddlProvince.DataValueField = "ID"
        ddlProvince.DataBind()
        ddlProvince.Items.Insert(0, New ListItem("", "0"))
    End Sub
    Private Sub BindDdlMainArea()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ddlMainArea.DataSource = New MainAreaFacade(User).RetrieveActiveList(criterias, "Description", Sort.SortDirection.ASC)
        ddlMainArea.DataTextField = "Description"
        ddlMainArea.DataValueField = "ID"
        ddlMainArea.DataBind()
        ddlMainArea.Items.Insert(0, New ListItem("", "0"))
    End Sub
    Private Sub BindDdlArea1()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area1), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Area1), "MainArea.ID", MatchType.Exact, CType(ddlMainArea.SelectedValue, Integer)))
        ddlArea1.DataSource = New Area1Facade(User).RetrieveActiveList(criterias, "Description", Sort.SortDirection.ASC)
        ddlArea1.DataTextField = "Description"
        ddlArea1.DataValueField = "ID"
        ddlArea1.DataBind()
        ddlArea1.Items.Insert(0, New ListItem("", "0"))
    End Sub
    Private Sub BindDdlArea2()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Area2), "Area1.ID", MatchType.Exact, CType(ddlArea1.SelectedValue, Integer)))
        ddlArea2.DataSource = New Area2Facade(User).RetrieveActiveList(criterias, "Description", Sort.SortDirection.ASC)
        ddlArea2.DataTextField = "Description"
        ddlArea2.DataValueField = "ID"
        ddlArea2.DataBind()
        ddlArea2.Items.Insert(0, New ListItem("", "0"))
    End Sub
    Private Sub PopulateDealerToObject()
        Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
        objDealer.DealerCode = txtDealerCode.Text
        objDealer.DealerName = txtName.Text
        objDealer.NickNameDigital = txtNickNameDigital.Text
        objDealer.NickNameEcommerce = txtNickNameEcomm.Text
        objDealer.Longitude = txtLongitude.Text
        objDealer.Latitude = txtLatitude.Text
        objDealer.Publish = IIf(rdPublish.SelectedValue = 0, True, False)
        objDealer.Title = ddlTitle.SelectedValue.ToString
        objDealer.SearchTerm1 = txtSearch1.Text
        objDealer.SearchTerm2 = txtSearch2.Text
        objDealer.DealerGroup = New DealerGroupFacade(User).Retrieve(CType(ddlGroup.SelectedValue, Integer))
        objDealer.Address = txtAddress.Text
        objDealer.City = New CityFacade(User).Retrieve(CType(ddlCity.SelectedValue, Integer))
        objDealer.Province = New ProvinceFacade(User).Retrieve(CType(ddlProvince.SelectedValue, Integer))
        objDealer.MainArea = New MainAreaFacade(User).Retrieve(CType(ddlMainArea.SelectedValue, Integer))
        objDealer.Area1 = New Area1Facade(User).Retrieve(CType(ddlArea1.SelectedValue, Integer))
        objDealer.Area2 = New Area2Facade(User).Retrieve(CType(ddlArea2.SelectedValue, Integer))
        objDealer.Website = txtWeb.Text
        objDealer.ZipCode = txtPostCode.Text
        objDealer.LegalStatus = txtLegalStatus.Text
        'tambahan main dealer 20140422 by anh
        If txtMainDealer.Text.Trim <> String.Empty Then
            Dim objMainDealer As Dealer
            If CType(ViewState("vsAction"), String) = "insert" Then
                objMainDealer = New DealerFacade(User).Retrieve("100001")
            Else
                objMainDealer = New DealerFacade(User).Retrieve(txtMainDealer.Text.Trim)
            End If
            If Not IsNothing(objMainDealer) Then
                objDealer.MainDealer = objMainDealer
            End If
        End If
        'tambahan SPA dari CRF
        objDealer.SPANumber = txtSPANumber.Text
        If txtSPADate.Text.Trim = "" Then
            objDealer.SPADate = System.Data.SqlTypes.SqlDateTime.MinValue.Value
        Else
            Dim tgl As String
            If Len(txtSPADate.Text.Trim) = 8 Then
                Try
                    Dim spaDate As String = txtSPADate.Text.Trim
                    objDealer.SPADate = New Date(spaDate.Substring(4, 4), spaDate.Substring(2, 2), spaDate.Substring(0, 2))
                Catch ex As Exception
                    MessageBox.Show("Tanggal SPA tidak valid.")
                End Try
            Else
                MessageBox.Show("Tanggal SPA tidak valid.")
            End If
        End If
        'akhir dari tambahan SPA

        objDealer.AgreementNo = txtNoPersetujuan.Text
        objDealer.AgreementDate = icTglPersetujuan.Value

        If ddlStatus.SelectedValue = "" Then
            objDealer.Status = CType(EnumDealerStatus.DealerStatus.NonAktive, String)
        Else
            objDealer.Status = ddlStatus.SelectedValue.ToString
        End If
        objDealer.SalesUnitFlag = IIf(cbSalesUnit.Checked, 1, 0)
        objDealer.SparepartFlag = IIf(cbSpareParts.Checked, 1, 0)
        objDealer.ServiceFlag = IIf(cbService.Checked, 1, 0)
        objDealer.Email = txtEmailAdd.Text
        objDealer.FreePPh22Indicator = CInt(Not (chbxFreePPh.Checked)) * -1
        objDealer.FreePPh22From = ValidFromDate
        objDealer.FreePPh22To = ValidToDate
        objDealer.Phone = txtfullNoTelepon.Text ' txtTelpArea.Text + "-" + txtTelpNo.Text
        objDealer.Fax = txtfullFax.Text ' txtFaxArea.Text + "-" + txtFaxNo.Text
        If Not User.Identity.Name = String.Empty Then
            objDealer.LastUpdateBy = User.Identity.Name
        End If
        sessHelper.SetSession("sesDealer", objDealer)
        PopulateBusinessAreaToObject()
    End Sub

    Private Function IsValidDate(ByVal strdate As String) As Boolean
        Dim strtgl As String = strdate.Substring(2, 2).ToString & "-" & strdate.Substring(0, 2) & "-" & strdate.Substring(4, 4)
        If IsDate(strtgl) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ToDate(ByVal strdate As String) As Date
        Return CType(strdate.Substring(2, 2).ToString & "-" & strdate.Substring(0, 2) & "-" & strdate.Substring(4, 4), Date)
    End Function


    Private Sub ShowDataDealer(ByVal dealerID As Integer)
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(dealerID)
        Dim listTitle As New EnumDealerTittle
        Dim al As ArrayList = listTitle.RetrieveTitle
        sessHelper.SetSession("sesDealer", objDealer)

        'tambahan privilege
        '===== daerah identitas dealer yang diisi o/ ktb
        'ActivateUserPrivilege()

        txtDealerCode.Text = objDealer.DealerCode
        txtDealerCode.Enabled = False
        txtDealerCode.BackColor = m_DealerColor

        If objDealer.OrganizationBranchType > 0 Then
            rdOrgTipeBranch.SelectedValue = objDealer.OrganizationBranchType.ToString()
        End If


        txtName.Text = objDealer.DealerName
        txtName.BackColor = m_DealerColor
        txtName.Enabled = m_bChangeDealer_Privilege

        txtNickNameDigital.Text = objDealer.NickNameDigital.ToString
        txtNickNameEcomm.Text = objDealer.NickNameEcommerce.ToString
        txtLongitude.Text = objDealer.Longitude.ToString
        txtLatitude.Text = objDealer.Latitude.ToString
        rdPublish.SelectedValue = IIf(objDealer.Publish = True, 0, 1)

        If Not (objDealer.Title Is Nothing Or objDealer.Title = "") Then
            For Each item As EnumTitle In al
                If objDealer.Title = item.ValTitle Then
                    ddlTitle.SelectedValue = item.ValTitle
                End If
            Next
        End If
        ddlTitle.Enabled = False
        ddlTitle.BackColor = m_DealerColor

        txtSearch1.Text = objDealer.SearchTerm1
        txtSearch1.Enabled = m_bChangeDealer_Privilege
        txtSearch1.BackColor = m_DealerColor

        txtSearch2.Text = objDealer.SearchTerm2
        txtSearch2.Enabled = m_bChangeDealer_Privilege
        txtSearch2.BackColor = m_DealerColor

        If Not objDealer.DealerGroup Is Nothing Then
            If objDealer.DealerGroup.ID.ToString = String.Empty Then
                ddlGroup.SelectedValue = "0"
            Else
                ddlGroup.SelectedValue = objDealer.DealerGroup.ID.ToString
            End If
        End If
        ddlGroup.Enabled = m_bChangeDealer_Privilege
        ddlGroup.BackColor = m_DealerColor

        txtAddress.Text = objDealer.Address
        txtAddress.Enabled = m_bChangeDealer_Privilege
        txtAddress.BackColor = m_DealerColor


        If Not objDealer.City Is Nothing Then
            If objDealer.City.ID.ToString = String.Empty Then
                ddlCity.SelectedValue = "0"
            Else
                ddlCity.ClearSelection()
                Dim cityList As ListItem = ddlCity.Items.FindByValue(objDealer.City.ID.ToString)
                If Not IsNothing(cityList) Then
                    cityList.Selected = True
                End If

                'ddlCity.Items.FindByValue(objDealer.City.ID).Selected = True
                'ddlCity.SelectedValue = objDealer.City.ID.ToString
            End If
        End If
        ddlCity.Enabled = m_bChangeDealer_Privilege
        ddlCity.BackColor = m_DealerColor



        'tambahan SPA dari CRF
        txtSPANumber.Text = objDealer.SPANumber
        txtSPANumber.Enabled = m_bChangeDealer_Privilege
        txtSPANumber.BackColor = m_DealerColor

        txtNoPersetujuan.Text = objDealer.AgreementNo
        txtNoPersetujuan.Enabled = m_bChangeDealer_Privilege
        txtNoPersetujuan.BackColor = m_DealerColor

        icTglPersetujuan.Value = objDealer.AgreementDate
        icTglPersetujuan.Enabled = m_bChangeDealer_Privilege

        If objDealer.SPADate = Nothing Or objDealer.SPADate.Date = System.Data.SqlTypes.SqlDateTime.MinValue.Value Then
            txtSPADate.Text = ""
        Else
            txtSPADate.Text = objDealer.SPADate.Date.ToString("ddMMyyyy")
        End If
        txtSPADate.Enabled = m_bChangeDealer_Privilege
        txtSPADate.BackColor = m_DealerColor

        'akhir dari tambahan SPA


        If Not objDealer.Province Is Nothing Then
            If objDealer.Province.ID.ToString = String.Empty Then
                ddlProvince.SelectedValue = "0"
            Else
                ddlProvince.SelectedValue = objDealer.Province.ID.ToString
            End If
        End If
        ddlProvince.Enabled = m_bChangeDealer_Privilege
        ddlProvince.BackColor = m_DealerColor

        If Not objDealer.MainArea Is Nothing Then
            If objDealer.MainArea.ID.ToString = String.Empty Then
                ddlMainArea.SelectedValue = "0"
            Else
                ddlMainArea.SelectedValue = CType(objDealer.MainArea.ID, Integer)
            End If
        End If
        ddlMainArea.Enabled = m_bChangeDealer_Privilege
        ddlMainArea.BackColor = m_DealerColor
        BindDdlArea1()
        If Not objDealer.Area1 Is Nothing Then
            If objDealer.Area1.ID.ToString = String.Empty Then
                ddlArea1.SelectedValue = "0"
            Else
                ddlArea1.SelectedValue = CType(objDealer.Area1.ID, Integer)
            End If
        End If
        ddlArea1.Enabled = m_bChangeDealer_Privilege
        ddlArea1.BackColor = m_DealerColor

        BindDdlArea2()
        If Not objDealer.Area2 Is Nothing Then
            If objDealer.Area2.ID.ToString = String.Empty Then
                ddlArea2.SelectedValue = "0"
            Else
                ddlArea2.SelectedValue = CType(objDealer.Area2.ID, Integer)
            End If
        End If
        ddlArea2.Enabled = m_bChangeDealer_Privilege
        ddlArea2.BackColor = m_DealerColor

        txtLegalStatus.Text = objDealer.LegalStatus
        txtLegalStatus.BackColor = m_DealerColor
        txtLegalStatus.Enabled = m_bChangeDealer_Privilege
        lblSearchDealer.Visible = m_bChangeDealer_Privilege

        txtPostCode.Text = objDealer.ZipCode
        txtLegalStatus.Text = objDealer.LegalStatus
        txtLegalStatus.BackColor = m_DealerColor
        txtPostCode.Enabled = m_bChangeDealer_Privilege
        txtPostCode.BackColor = m_DealerColor

        ' info main dealer
        txtMainDealer.Enabled = m_bChangeDealer_Privilege
        txtMainDealer.BackColor = m_DealerColor
        lblMainDealer.Visible = m_bChangeDealer_Privilege
        If Not IsNothing(objDealer.MainDealer) Then
            txtMainDealer.Text = objDealer.MainDealer.DealerCode
        End If

        ddlStatus.SelectedValue = objDealer.Status
        ddlStatus.Enabled = m_bChangeDealer_Privilege
        ddlStatus.BackColor = m_DealerColor

        cbSalesUnit.Checked = IIf(objDealer.SalesUnitFlag = 1, True, False)
        cbSalesUnit.Enabled = m_bChangeDealer_Privilege

        cbSpareParts.Checked = IIf(objDealer.SparepartFlag = 1, True, False)
        cbSpareParts.Enabled = m_bChangeDealer_Privilege

        cbService.Checked = IIf(objDealer.ServiceFlag = 1, True, False)
        cbService.Enabled = m_bChangeDealer_Privilege

        Dim funcX As New DealerOperationAreaBussinessFacade(Me.User)
        Dim arrDealerOpr As List(Of DealerOperationAreaBussiness) = funcX.RetrievebyDealerID(objDealer.ID)
        Dim strControlName() As String = {"chkSU", "chkSvc", "chkSPart"}
        For idx As Integer = 1 To strControlName.Length
            Dim strName As String = strControlName(idx - 1)
            For Each cbItem As ListItem In CType(Me.Page.FindControl(strName), CheckBoxList).Items
                Dim objDealerOpr As DealerOperationAreaBussiness
                Dim listDealerOpr As List(Of DealerOperationAreaBussiness) = _
                     arrDealerOpr.Where(Function(x) x.AreaBusiness = idx - 1 And x.DealerOperation = CInt(cbItem.Value)).ToList()
                If listDealerOpr.Count > 0 Then
                    cbItem.Selected = True
                Else
                    cbItem.Selected = False
                End If

            Next
        Next


        '===== daerah dealer profile yang diisi o/ dealer

        txtWeb.Text = objDealer.Website
        txtWeb.Enabled = m_bChangeCI_Dealer_Privilege
        txtWeb.BackColor = m_CI_DealerColor


        txtEmailAdd.Text = objDealer.Email
        txtEmailAdd.Enabled = m_bChangeCI_Dealer_Privilege
        txtEmailAdd.BackColor = m_CI_DealerColor

        chbxFreePPh.Checked = Not (CBool(objDealer.FreePPh22Indicator))
        txtValidFrom.Text = IIf(objDealer.FreePPh22From < New DateTime(1900, 1, 1), Format(New DateTime(1900, 1, 1), "MMyyyy"), Format(objDealer.FreePPh22From, "MMyyyy"))
        txtValidTo.Text = IIf(objDealer.FreePPh22To < New DateTime(1900, 1, 1), Format(New DateTime(1900, 1, 1), "MMyyyy"), Format(objDealer.FreePPh22To, "MMyyyy"))
        chbxFreePPh.Enabled = m_bChangeFreePPh22_Privilege
        txtValidFrom.Enabled = m_bChangeFreePPh22_Privilege
        txtValidTo.Enabled = m_bChangeFreePPh22_Privilege

        Dim delimStr As String = "-"
        Dim delimeter As Char() = delimStr.ToCharArray
        'Dim dummyParam As String() = objDealer.Phone.Split(delimeter)
        txtfullNoTelepon.Text = objDealer.Phone
        txtfullFax.Text = objDealer.Fax
        'If dummyParam.Length > 0 Then
        '    If Not dummyParam(0) = String.Empty Then
        '        txtTelpArea.Text = dummyParam(0)
        '    Else
        '        txtTelpArea.Text = ""
        '    End If
        'End If
        'txtTelpArea.Enabled = m_bChangeCI_Dealer_Privilege
        'txtTelpArea.BackColor = m_CI_DealerColor

        'If dummyParam.Length > 1 Then
        '    If Not dummyParam(1) = String.Empty Then
        '        txtTelpNo.Text = dummyParam(1)
        '    Else
        '        txtTelpNo.Text = ""
        '    End If
        'End If
        'txtTelpNo.Enabled = m_bChangeCI_Dealer_Privilege
        'txtTelpNo.BackColor = m_CI_DealerColor

        'dummyParam = objDealer.Fax.Split(delimeter)
        'If dummyParam.Length > 0 Then
        '    If Not dummyParam(0) = String.Empty Then
        '        txtFaxArea.Text = dummyParam(0)
        '    Else
        '        txtFaxArea.Text = ""
        '    End If
        'End If
        'txtFaxArea.Enabled = m_bChangeCI_Dealer_Privilege
        'txtFaxArea.BackColor = m_CI_DealerColor

        'If dummyParam.Length > 1 Then
        '    If Not dummyParam(1) = String.Empty Then
        '        txtFaxNo.Text = dummyParam(1)
        '    Else
        '        txtFaxNo.Text = ""
        '    End If
        'End If
        'txtFaxNo.Enabled = m_bChangeCI_Dealer_Privilege
        'txtFaxNo.BackColor = m_CI_DealerColor

        If Not objDealer.LastUpdateBy Is Nothing Then
            LblLastChange.Text = UserInfo.Convert(objDealer.LastUpdateBy)
        End If

        If Not objDealer.LastUpdateTime = Nothing Then
            If LblLastChange.Text = String.Empty Then
                LblLastChange.Text = "  -------   :  " + Format(objDealer.LastUpdateTime, "dd/MM/yyyy")
            Else
                LblLastChange.Text = LblLastChange.Text + "  :  " + Format(objDealer.LastUpdateTime, "dd/MM/yyyy")
            End If

        End If

        ShowDealerCategory(dealerID)
        ShowDealerProfile(dealerID)
        ShowBusinessArea(dealerID)

    End Sub

    Private Sub ShowDealerProfile(ByVal _dealerID As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DealerProfile), "Dealer.ID", MatchType.Exact, _dealerID))

        Dim arrDealerProfile As ArrayList = New DealerProfileFacade(User).Retrieve(criterias)

        If arrDealerProfile.Count > 0 Then
            For Each itemDealerProfile As DealerProfile In arrDealerProfile
                If itemDealerProfile.ProfileHeader.ID = 32 Then
                    txtNameBM.Text = itemDealerProfile.ProfileValue
                ElseIf itemDealerProfile.ProfileHeader.ID = 33 Then
                    txtHPBM.Text = itemDealerProfile.ProfileValue
                ElseIf itemDealerProfile.ProfileHeader.ID = 26 Then
                    txtEmailBM.Text = itemDealerProfile.ProfileValue
                ElseIf itemDealerProfile.ProfileHeader.ID = 46 Then
                    txtPICRegion.Text = itemDealerProfile.ProfileValue
                ElseIf itemDealerProfile.ProfileHeader.ID = 47 Then
                    txtPICArea.Text = itemDealerProfile.ProfileValue
                ElseIf itemDealerProfile.ProfileHeader.ID = 48 Then
                    txtPICSubArea.Text = itemDealerProfile.ProfileValue
                End If
            Next
        End If

    End Sub

    Private Sub ShowDealerCategory(ByVal _dealerID As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DealerCategory), "Dealer.ID", MatchType.Exact, _dealerID))

        Dim arrDealerCategory As ArrayList = New DealerCategoryFacade(User).Retrieve(criterias)

        For Each itemList As ListItem In chlVehicleCategory.Items
            itemList.Selected = False
        Next

        For Each itemDealerCategory As DealerCategory In arrDealerCategory
            For Each itemList As ListItem In chlVehicleCategory.Items
                If (itemDealerCategory.Category.ID.ToString() = itemList.Value) Then
                    itemList.Selected = True
                End If
            Next
        Next
    End Sub


    Private Sub ShowBusinessArea(ByVal dealerID As Integer)
        RetrieveBusinessArea(dealerID)
        Dim arrBA As ArrayList = CType(Session("sessBusinessArea"), ArrayList)

        For Each businessArea As BusinessArea In arrBA
            Dim objID As String = (CInt(businessArea.Kind) + 1).ToString() + businessArea.Position.ToString()

            Dim txtContactPerson As TextBox = Me.Page.FindControl("txtContactPerson" + objID)
            Dim txtEmail As TextBox = Me.Page.FindControl("txtEmail" + objID)
            Dim txtHP As TextBox = Me.Page.FindControl("txtHP" + objID)
            Dim lblLastUpdate As Label = Me.Page.FindControl("lblLastUpdate" + objID)

            If Not IsNothing(txtContactPerson) Then
                txtContactPerson.Text = businessArea.ContactPerson
            End If

            If Not IsNothing(txtEmail) Then
                txtEmail.Text = businessArea.Email
            End If

            If Not IsNothing(txtHP) Then
                txtHP.Text = businessArea.Phone
            End If

            If Not IsNothing(lblLastUpdate) Then
                lblLastUpdate.Text = Format(businessArea.LastUpdateTime, "dd/MM/yyyy")
            End If


            'If businessArea.Kind = CType(EnumDealerTransKind.DealerTransKind.SalesUnit, Integer) Then
            '    txtContactPerson11.Text = businessArea.ContactPerson
            '    txtEmail11.Text = businessArea.Email
            '    txtHP11.Text = businessArea.Phone

            '    If Not IsNothing(businessArea.LastUpdateTime) Then
            '        lblLastUpdate11.Text = Format(businessArea.LastUpdateTime, "dd/MM/yyyy")

            '    End If
            '    businessArea.RowStatus = DBRowStatus.Deleted
            '    pnlSales.Enabled = True
            'End If
            'If businessArea.Kind = CType(EnumDealerTransKind.DealerTransKind.ServiceUnit, Integer) Then
            '    txtContactPerson21.Text = businessArea.ContactPerson
            '    txtEmail21.Text = businessArea.Email
            '    txtHP21.Text = businessArea.Phone

            '    If Not IsNothing(businessArea.LastUpdateTime) Then
            '        lblLastUpdate21.Text = Format(businessArea.LastUpdateTime, "dd/MM/yyyy")

            '    End If
            '    businessArea.RowStatus = DBRowStatus.Deleted
            '    pnlService.Enabled = True
            'End If
            'If businessArea.Kind = CType(EnumDealerTransKind.DealerTransKind.SparePartUnit, Integer) Then
            '    txtContactPerson31.Text = businessArea.ContactPerson
            '    txtEmail31.Text = businessArea.Email
            '    txtHP31.Text = businessArea.Phone

            '    If Not IsNothing(businessArea.LastUpdateTime) Then
            '        lblLastUpdate31.Text = Format(businessArea.LastUpdateTime, "dd/MM/yyyy")

            '    End If
            '    businessArea.RowStatus = DBRowStatus.Deleted
            '    pnlSpareparts.Enabled = True
            'End If
        Next

        'tambahan privilege 
        'disable privilagenya by request mas Benny 2019/03/26
        'cbSalesUnit.Enabled = m_bChangeFU_Dealer_Privilege
        'cbService.Enabled = m_bChangeSV_Dealer_Privilege
        'cbSpareParts.Enabled = m_bChangeSP_Dealer_Privilege

        '-- changes by request Mas Benny - 2019/03/26
        'pnlSales.Enabled = m_bChangeFU_Dealer_Privilege
        If cbSalesUnit.Checked = True AndAlso m_bChangeFU_Dealer_Privilege = True Then
            pnlSales.Enabled = True
        Else
            pnlSales.Enabled = False
        End If

        'pnlService.Enabled = m_bChangeSV_Dealer_Privilege
        If cbService.Checked = True AndAlso m_bChangeSV_Dealer_Privilege = True Then
            pnlService.Enabled = True
        Else
            pnlService.Enabled = False
        End If

        'pnlSpareparts.Enabled = m_bChangeSP_Dealer_Privilege
        If cbSpareParts.Checked = True AndAlso m_bChangeSP_Dealer_Privilege = True Then
            pnlSpareparts.Enabled = True
        Else
            pnlSpareparts.Enabled = False
        End If
        '--- end changes


        txtContactPerson11.Enabled = m_bChangeFU_Dealer_Privilege
        txtEmail11.Enabled = m_bChangeFU_Dealer_Privilege
        txtHP11.Enabled = m_bChangeFU_Dealer_Privilege

        txtContactPerson11.BackColor = m_FU_DealerColor
        txtEmail11.BackColor = m_FU_DealerColor
        txtHP11.BackColor = m_FU_DealerColor


        txtContactPerson21.Enabled = m_bChangeSV_Dealer_Privilege
        txtEmail21.Enabled = m_bChangeSV_Dealer_Privilege
        txtHP21.Enabled = m_bChangeSV_Dealer_Privilege

        txtContactPerson21.BackColor = m_SV_DealerColor
        txtEmail21.BackColor = m_SV_DealerColor
        txtHP21.BackColor = m_SV_DealerColor

        txtContactPerson31.Enabled = m_bChangeSP_Dealer_Privilege
        txtEmail31.Enabled = m_bChangeSP_Dealer_Privilege
        txtHP31.Enabled = m_bChangeSP_Dealer_Privilege

        txtContactPerson31.BackColor = m_SV_DealerColor
        txtEmail31.BackColor = m_SV_DealerColor
        txtHP31.BackColor = m_SV_DealerColor


    End Sub

    Private Function RetrieveBusinessArea(ByVal dealerID As Integer) As ArrayList
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(BusinessArea), "Dealer.ID", MatchType.Exact, dealerID))
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BusinessArea), "Dealer.ID", MatchType.Exact, dealerID))
        Dim arrBusinessArea As ArrayList = New BusinessAreaFacade(User).Retrieve(criterias)
        sessHelper.SetSession("sessBusinessArea", arrBusinessArea)
    End Function

    Private Sub GetNewBusinessAreaObject()

        Dim arrBusinessArea As New ArrayList '= CType(Session("sessBusinessArea"), ArrayList)

        If cbSalesUnit.Checked Then
            Dim objBusinessArea As BusinessArea = New BusinessArea
            objBusinessArea.ContactPerson = txtContactPerson11.Text
            objBusinessArea.Email = txtEmail11.Text
            objBusinessArea.Phone = txtHP11.Text
            objBusinessArea.Kind = CType(EnumDealerTransKind.DealerTransKind.SalesUnit, Integer)
            arrBusinessArea.Add(objBusinessArea)

        End If
        If cbService.Checked Then
            Dim objBusinessArea As BusinessArea = New BusinessArea
            objBusinessArea.ContactPerson = txtContactPerson21.Text
            objBusinessArea.Email = txtEmail21.Text
            objBusinessArea.Phone = txtHP21.Text
            objBusinessArea.Kind = CType(EnumDealerTransKind.DealerTransKind.ServiceUnit, Integer)
            arrBusinessArea.Add(objBusinessArea)

        End If
        If cbSpareParts.Checked Then
            Dim objBusinessArea As BusinessArea = New BusinessArea
            objBusinessArea.ContactPerson = txtContactPerson31.Text
            objBusinessArea.Email = txtEmail31.Text
            objBusinessArea.Phone = txtHP31.Text
            objBusinessArea.Kind = CType(EnumDealerTransKind.DealerTransKind.SparePartUnit, Integer)
            arrBusinessArea.Add(objBusinessArea)
        End If

        sessHelper.SetSession("sessBusinessArea", arrBusinessArea)
    End Sub

    Private Function GetBussinessArea(ByVal arrBusinessArea As ArrayList, ByVal kind As Integer, ByVal IsChecked As Boolean) As BusinessArea
        For Each objBussinessArea As BusinessArea In arrBusinessArea
            If objBussinessArea.Kind = kind Then
                If IsChecked Then
                    objBussinessArea.RowStatus = 1
                Else
                    objBussinessArea.RowStatus = -1
                End If
                Return objBussinessArea
            End If
        Next
        Return New BusinessArea
    End Function

    Private Sub SetBusinessAreaObject()
        '--Update exist Bussiness Area
        Dim arrBusinessArea As ArrayList = CType(Session("sessBusinessArea"), ArrayList)
        Dim arrBA As ArrayList = New ArrayList

        If cbSalesUnit.Checked Then
            Dim objBusinessArea As BusinessArea = GetBussinessArea(arrBusinessArea, CType(EnumDealerTransKind.DealerTransKind.SalesUnit, Integer), cbSalesUnit.Checked)
            objBusinessArea.ContactPerson = txtContactPerson11.Text
            objBusinessArea.Email = txtEmail11.Text
            objBusinessArea.Phone = txtHP11.Text
            objBusinessArea.Kind = CType(EnumDealerTransKind.DealerTransKind.SalesUnit, Integer)
            arrBA.Add(objBusinessArea)
        Else
            Dim objBusinessArea As BusinessArea = GetBussinessArea(arrBusinessArea, CType(EnumDealerTransKind.DealerTransKind.SalesUnit, Integer), False)
            objBusinessArea.Kind = CType(EnumDealerTransKind.DealerTransKind.SalesUnit, Integer)
            arrBA.Add(objBusinessArea)
        End If
        If cbService.Checked Then
            Dim objBusinessArea As BusinessArea = GetBussinessArea(arrBusinessArea, CType(EnumDealerTransKind.DealerTransKind.ServiceUnit, Integer), cbService.Checked)
            objBusinessArea.ContactPerson = txtContactPerson21.Text
            objBusinessArea.Email = txtEmail21.Text
            objBusinessArea.Phone = txtHP21.Text
            objBusinessArea.Kind = CType(EnumDealerTransKind.DealerTransKind.ServiceUnit, Integer)
            arrBA.Add(objBusinessArea)
        Else
            Dim objBusinessArea As BusinessArea = GetBussinessArea(arrBusinessArea, CType(EnumDealerTransKind.DealerTransKind.ServiceUnit, Integer), False)
            objBusinessArea.Kind = CType(EnumDealerTransKind.DealerTransKind.ServiceUnit, Integer)
            arrBA.Add(objBusinessArea)
        End If

        If cbSpareParts.Checked Then
            Dim objBusinessArea As BusinessArea = GetBussinessArea(arrBusinessArea, CType(EnumDealerTransKind.DealerTransKind.SparePartUnit, Integer), cbSpareParts.Checked)
            objBusinessArea.ContactPerson = txtContactPerson31.Text
            objBusinessArea.Email = txtEmail31.Text
            objBusinessArea.Phone = txtHP31.Text
            objBusinessArea.Kind = CType(EnumDealerTransKind.DealerTransKind.SparePartUnit, Integer)
            arrBA.Add(objBusinessArea)
        Else
            Dim objBusinessArea As BusinessArea = GetBussinessArea(arrBusinessArea, CType(EnumDealerTransKind.DealerTransKind.SparePartUnit, Integer), False)
            objBusinessArea.Kind = CType(EnumDealerTransKind.DealerTransKind.SparePartUnit, Integer)
            arrBA.Add(objBusinessArea)
        End If
        sessHelper.SetSession("sessBusinessArea", arrBA)
    End Sub

    Private Sub PopulateBusinessAreaToObject()
        If CType(ViewState("vsAction"), String) = "insert" Then
            GetNewBusinessAreaObject()
        Else
            SetBusinessAreaObject()
        End If

    End Sub

    Private Sub cbSalesUnit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSalesUnit.CheckedChanged
        PanelBussinesAreaCheck()
    End Sub

    Private Sub PanelBussinesAreaCheck()
        If cbSalesUnit.Checked Then
            pnlSales.Enabled = True
            txtContactPerson11.Enabled = True
            txtHP11.Enabled = True
            txtEmail11.Enabled = True
            lblLastUpdate11.Enabled = True
        Else
            pnlSales.Enabled = False

            txtContactPerson11.Text = ""
            txtHP11.Text = ""
            txtEmail11.Text = ""
            lblLastUpdate11.Text = ""
            txtPICArea.Text = ""
            txtPICRegion.Text = ""
            txtPICSubArea.Text = ""
            txtContactPerson11.Enabled = False
            txtHP11.Enabled = False
            txtEmail11.Enabled = False
            lblLastUpdate11.Enabled = False
        End If

        If cbService.Checked Then
            pnlService.Enabled = True
            txtContactPerson21.Enabled = True
            txtHP21.Enabled = True
            txtEmail21.Enabled = True
            lblLastUpdate21.Enabled = True
        Else
            pnlService.Enabled = False
            txtContactPerson21.Text = ""
            txtHP21.Text = ""
            txtEmail21.Text = ""
            lblLastUpdate21.Text = ""

            txtContactPerson21.Enabled = False
            txtHP21.Enabled = False
            txtEmail21.Enabled = False
            lblLastUpdate21.Enabled = False

        End If

        If cbSpareParts.Checked Then
            pnlSpareparts.Enabled = True
            txtContactPerson31.Enabled = True
            txtHP31.Enabled = True
            txtEmail31.Enabled = True
            lblLastUpdate31.Enabled = True
        Else
            pnlSpareparts.Enabled = False
            txtContactPerson31.Text = ""
            txtHP31.Text = ""
            txtEmail31.Text = ""
            lblLastUpdate31.Text = ""

            txtContactPerson31.Enabled = False
            txtHP31.Enabled = False
            txtEmail31.Enabled = False
            lblLastUpdate31.Enabled = False
        End If

    End Sub
    Private Sub cbService_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbService.CheckedChanged
        PanelBussinesAreaCheck()
    End Sub

    Private Sub cbSpareParts_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSpareParts.CheckedChanged
        PanelBussinesAreaCheck()
    End Sub
    Private Function CheckMandatoryField() As Short

        If txtDealerCode.Text = String.Empty Then
            Return 1
        Else
            Dim bCon As Boolean = True
            If CType(ViewState("vsAction"), String) = "insert" Then
                Dim nRowCount As Integer = New DealerFacade(User).ValidateCode(txtDealerCode.Text)
                If nRowCount > 0 Then 'Code sudah ada
                    Return 1
                End If
            End If

            If ddlTitle.SelectedValue = "-1" Then
                Return 2
            End If
            If ddlProvince.SelectedValue = "0" Then
                Return 3
            End If
            If ddlCity.SelectedValue = "0" Then
                Return 4
            End If
            'Periksa Area Bisnis
            If Not (cbSalesUnit.Checked) And Not (cbService.Checked) And Not (cbSpareParts.Checked) Then
                Return 5
            End If

            If txtSearch1.Text.Length > 20 Then
                Return 6
            End If
            If txtSearch2.Text.Length <> 4 AndAlso (ViewState("vsAction") = "insert") Then
                Return 7
            End If

            If txtSearch2.Text.Length <> 4 AndAlso (ViewState("vsAction") = "edit" AndAlso ddlStatus.SelectedValue = EnumDealerStatus.DealerStatus.Aktive) Then
                Return 7
            End If

            If (txtSearch2.Text.Length <> 4 AndAlso txtSearch2.Text.Length <> 6 AndAlso ViewState("vsAction") = "edit") Then
                Return 8
            End If


        End If
        Return 0
    End Function
    Private Function IsUnhack() As Boolean
        If txtAddress.Text.IndexOf("<") >= 0 Or txtAddress.Text.IndexOf(">") >= 0 Or txtAddress.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtContactPerson11.Text.IndexOf("<") >= 0 Or txtContactPerson11.Text.IndexOf(">") >= 0 Or txtContactPerson11.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtContactPerson21.Text.IndexOf("<") >= 0 Or txtContactPerson21.Text.IndexOf(">") >= 0 Or txtContactPerson21.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtContactPerson31.Text.IndexOf("<") >= 0 Or txtContactPerson31.Text.IndexOf(">") >= 0 Or txtContactPerson31.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtDealerCode.Text.IndexOf("<") >= 0 Or txtDealerCode.Text.IndexOf(">") >= 0 Or txtDealerCode.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtEmail11.Text.IndexOf("<") >= 0 Or txtEmail11.Text.IndexOf(">") >= 0 Or txtEmail11.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtEmail21.Text.IndexOf("<") >= 0 Or txtEmail21.Text.IndexOf(">") >= 0 Or txtEmail21.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtEmail31.Text.IndexOf("<") >= 0 Or txtEmail31.Text.IndexOf(">") >= 0 Or txtEmail31.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        If txtEmailAdd.Text.IndexOf("<") >= 0 Or txtEmailAdd.Text.IndexOf(">") >= 0 Or txtEmailAdd.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        'If txtFaxArea.Text.IndexOf("<") >= 0 Or txtFaxArea.Text.IndexOf(">") >= 0 Or txtFaxArea.Text.IndexOf("'") >= 0 Then
        '    Return False
        'End If

        'If txtFaxNo.Text.IndexOf("<") >= 0 Or txtFaxNo.Text.IndexOf(">") >= 0 Or txtFaxNo.Text.IndexOf("'") >= 0 Then
        '    Return False
        'End If

        If txtHP11.Text.IndexOf("<") >= 0 Or txtHP11.Text.IndexOf(">") >= 0 Or txtHP11.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtHP21.Text.IndexOf("<") >= 0 Or txtHP21.Text.IndexOf(">") >= 0 Or txtHP21.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtHP31.Text.IndexOf("<") >= 0 Or txtHP31.Text.IndexOf(">") >= 0 Or txtHP31.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtName.Text.IndexOf("<") >= 0 Or txtName.Text.IndexOf(">") >= 0 Or txtName.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtNickNameDigital.Text.IndexOf("<") >= 0 Or txtNickNameDigital.Text.IndexOf(">") >= 0 Or txtNickNameDigital.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtNickNameEcomm.Text.IndexOf("<") >= 0 Or txtNickNameEcomm.Text.IndexOf(">") >= 0 Or txtNickNameEcomm.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtLongitude.Text.IndexOf("<") >= 0 Or txtLongitude.Text.IndexOf(">") >= 0 Or txtLongitude.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtLatitude.Text.IndexOf("<") >= 0 Or txtLatitude.Text.IndexOf(">") >= 0 Or txtLatitude.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtPostCode.Text.IndexOf("<") >= 0 Or txtPostCode.Text.IndexOf(">") >= 0 Or txtPostCode.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        If txtLegalStatus.Text.IndexOf("<") >= 0 Or txtLegalStatus.Text.IndexOf(">") >= 0 Or txtLegalStatus.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtSearch1.Text.IndexOf("<") >= 0 Or txtSearch1.Text.IndexOf(">") >= 0 Or txtSearch1.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        If txtSearch2.Text.IndexOf("<") >= 0 Or txtSearch2.Text.IndexOf(">") >= 0 Or txtSearch2.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        If txtSPADate.Text.IndexOf("<") >= 0 Or txtSPADate.Text.IndexOf(">") >= 0 Or txtSPADate.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        If txtSPANumber.Text.IndexOf("<") >= 0 Or txtSPANumber.Text.IndexOf(">") >= 0 Or txtSPANumber.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        'If txtTelpArea.Text.IndexOf("<") >= 0 Or txtTelpArea.Text.IndexOf(">") >= 0 Or txtTelpArea.Text.IndexOf("'") >= 0 Then
        '    Return False
        'End If
        'If txtTelpNo.Text.IndexOf("<") >= 0 Or txtTelpNo.Text.IndexOf(">") >= 0 Or txtTelpNo.Text.IndexOf("'") >= 0 Then
        '    Return False
        'End If
        If txtWeb.Text.IndexOf("<") >= 0 Or txtWeb.Text.IndexOf(">") >= 0 Or txtWeb.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        Return True
    End Function

    Private Function IsLegalStatusAvailable(ByVal code As String) As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, code))
        Dim objDealerFacade As DealerFacade = New DealerFacade(User)
        Dim list As ArrayList = objDealerFacade.Retrieve(criterias)
        If Not list Is Nothing Then
            If list.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Request.QueryString("isupdate") = "1" Then
            Dim func As New DealerFacade(Me.User)
            Dim objDealer As Dealer = func.Retrieve(CInt(Request.QueryString("dealerid")))
            objDealer.SalesUnitFlag = IIf(cbSalesUnit.Checked, 1, 0)
            objDealer.SparepartFlag = IIf(cbSpareParts.Checked, 1, 0)
            objDealer.ServiceFlag = IIf(cbService.Checked, 1, 0)
            objDealer.MainArea = New MainAreaFacade(User).Retrieve(CType(ddlMainArea.SelectedValue, Integer))
            objDealer.Area1 = New Area1Facade(User).Retrieve(CType(ddlArea1.SelectedValue, Integer))
            objDealer.Area2 = New Area2Facade(User).Retrieve(CType(ddlArea2.SelectedValue, Integer))
            func.Update(objDealer)

            Dim funcX As New DealerOperationAreaBussinessFacade(Me.User)
            Dim arrDealerOpr As List(Of DealerOperationAreaBussiness) = funcX.RetrievebyDealerID(objDealer.ID)
            Dim strControlName() As String = {"chkSU", "chkSvc", "chkSPart"}
            For idx As Integer = 1 To strControlName.Length
                Dim strName As String = strControlName(idx - 1)
                For Each cbItem As ListItem In CType(Me.Page.FindControl(strName), CheckBoxList).Items

                    Dim objDealerOpr As DealerOperationAreaBussiness
                    Dim listDealerOpr As List(Of DealerOperationAreaBussiness) = _
                         arrDealerOpr.Where(Function(x) x.AreaBusiness = idx And x.DealerOperation = CInt(cbItem.Value)).ToList()
                    If listDealerOpr.Count > 0 Then
                        objDealerOpr = listDealerOpr(0)
                        If Not cbItem.Selected Then
                            funcX.Delete(objDealerOpr)
                        End If
                    Else
                        If cbItem.Selected Then
                            objDealerOpr = New DealerOperationAreaBussiness
                            'objDealerOpr.DealerID = objDealer.ID
                            objDealerOpr.Dealer = objDealer
                            objDealerOpr.DealerOperation = CInt(cbItem.Value)
                            objDealerOpr.AreaBusiness = idx
                            funcX.Insert(objDealerOpr)
                        End If
                    End If

                Next
            Next
            MessageBox.Show(SR.SaveSuccess)
        Else
            If Not Page.IsValid Then
                MessageBox.Show(" Kode dealer, Nama Dealer, Term Cari 1/2, Alamat, Propinsi dan kota tidak boleh kosong" & "\n" & _
                "Silahkan isi kode dealer induk (tidak boleh kosong)" & "\n" & _
                "Kode Pos, No. Telpon, HP dan Fax harus angka " & "\n" & _
                "Tanggal SPA diisi dengan format 'ddMMyyyy',  misal '28022005' " & "\n" & _
                "Field-field email diisi dengan format email misal budi@ktb.com ")
                Exit Sub
            End If
            If Me.ddlGroup.SelectedIndex < 1 Then
                MessageBox.Show("Dealer Group Harus Dipilih")
                Exit Sub
            End If
            If Me.txtfullNoTelepon.Text.Trim = String.Empty Then
                MessageBox.Show("No Telepon Harus Diisi")
                Exit Sub
            End If

            If chbxFreePPh.Checked Then
                Try
                    ValidFromDate = New DateTime(CInt(txtValidFrom.Text.Substring(2, 4)), CInt(txtValidFrom.Text.Substring(0, 2)), 1)
                    ValidToDate = New DateTime(CInt(txtValidTo.Text.Substring(2, 4)), CInt(txtValidTo.Text.Substring(0, 2)), DateTime.DaysInMonth(CInt(txtValidTo.Text.Substring(2, 4)), CInt(txtValidTo.Text.Substring(0, 2))))
                Catch ex As Exception
                    MessageBox.Show("Tanggal Tidak Valid")
                    Exit Sub
                End Try
                If (ValidFromDate > ValidToDate) Then
                    MessageBox.Show("Tanggal Valid Dari lebih besar dari Tanggal Valid Sampai")
                    Exit Sub
                End If
            End If


            If Not IsUnhack() Then
                MessageBox.Show("< dan > bukan karakter valid")
                Return
            End If
            If CType(ViewState("vsAction"), String) = "edit" Then
                If Not IsLegalStatusAvailable(txtLegalStatus.Text.Trim) Then
                    MessageBox.Show("Legal Status tidak terdaftar")
                    Return
                End If
            End If

            Dim strErrTermInsert As String = ""
            Dim strErrTermUpdate As String = ""
            Dim nStatus As Integer = CheckMandatoryField()
            Select Case nStatus
                Case 0
                    Dim bCheckInsert As Boolean = True
                    PopulateDealerToObject()
                    Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
                    Dim arlBusinessArea As ArrayList = CType(Session("sessBusinessArea"), ArrayList)
                    Dim nResult As Integer

                    Dim arlVehicleCategory As ArrayList = New ArrayList
                    For Each items As ListItem In chlVehicleCategory.Items
                        If items.Selected Then
                            arlVehicleCategory.Add(CType(items.Value, Integer))
                        End If
                    Next
                    If arlVehicleCategory.Count < 1 Then
                        MessageBox.Show("Silahkan pilih kategori kendaraan")
                        Return
                    End If

                    'set Dealer Profile (Branch Manager, Email, No HP)
                    'set PIC Sales unit (SLS_PIC_REGION,SLS_PIC_AREA,SLS_PIC_SUBAREA)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, 14))
                    criterias.opOr(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, 15))
                    Dim arlProfileHeaderToGroup As ArrayList = New ProfileHeaderToGroupFacade(User).Retrieve(criterias)

                    Dim arlDealerProfile As ArrayList = New ArrayList
                    If arlProfileHeaderToGroup.Count > 0 Then
                        For Each objProfileHeaderToGroup As ProfileHeaderToGroup In arlProfileHeaderToGroup
                            Dim objDealerProfile As New DealerProfile
                            objDealerProfile.ProfileHeader = objProfileHeaderToGroup.ProfileHeader
                            objDealerProfile.Dealer = objDealer
                            objDealerProfile.ProfileGroup = objProfileHeaderToGroup.ProfileGroup
                            If objProfileHeaderToGroup.ProfileHeader.ID = 32 Then
                                objDealerProfile.ProfileValue = txtNameBM.Text
                            ElseIf objProfileHeaderToGroup.ProfileHeader.ID = 26 Then
                                objDealerProfile.ProfileValue = txtEmailBM.Text
                            ElseIf objProfileHeaderToGroup.ProfileHeader.ID = 33 Then
                                objDealerProfile.ProfileValue = txtHPBM.Text
                            ElseIf objProfileHeaderToGroup.ProfileHeader.ID = 46 Then
                                objDealerProfile.ProfileValue = txtPICRegion.Text
                            ElseIf objProfileHeaderToGroup.ProfileHeader.ID = 47 Then
                                objDealerProfile.ProfileValue = txtPICArea.Text
                            ElseIf objProfileHeaderToGroup.ProfileHeader.ID = 48 Then
                                objDealerProfile.ProfileValue = txtPICSubArea.Text
                            End If
                            arlDealerProfile.Add(objDealerProfile)
                        Next
                    End If

                    If CType(ViewState("vsAction"), String) = "insert" Then
                        CheckExistingSearchTerm(bCheckInsert, strErrTermInsert, "SearchTerm1", txtSearch1.Text, objDealer)
                        CheckExistingSearchTerm(bCheckInsert, strErrTermInsert, "SearchTerm2", txtSearch2.Text, objDealer)
                        strErrTermInsert = ErrMessageForSearchTerm(strErrTermInsert)
                        If bCheckInsert Then
                            Try
                                nResult = New DealerFacade(User).InsertDealer(objDealer, arlBusinessArea, arlVehicleCategory, arlDealerProfile)
                                'update MainDealerID
                                If nResult = 0 AndAlso (txtDealerCode.Text.Trim = txtMainDealer.Text.Trim) Then
                                    Dim objMainDealer As Dealer = New DealerFacade(User).Retrieve(txtMainDealer.Text.Trim)
                                    If Not IsNothing(objDealer) Then
                                        objDealer.LegalStatus = objMainDealer.DealerCode
                                        objDealer.MainDealer = objMainDealer
                                        Dim nUpdate As Integer = New DealerFacade(User).UpdateDealer(objDealer, arlBusinessArea, arlVehicleCategory, arlDealerProfile)
                                    End If
                                End If
                                'end MainDealerID
                                CreateSAPDirectory(objDealer.SearchTerm2)
                                'Create default value for transaction
                                'Factoring
                                Dim arlTC As New ArrayList
                                Dim oTC As New TransactionControl
                                oTC = New TransactionControl
                                oTC.Kind = EnumDealerTransType.DealerTransKind.Factoring
                                oTC.Dealer = objDealer
                                oTC.DateControl = 25
                                oTC.Status = 0 'gak aktif
                                'If nStatus = EnumDealerStatus.DealerStatus.Aktive Then
                                '    oTC.UpdateTransHistory = False
                                'End If
                                oTC.RowStatus = 0
                                oTC.LastUpdateBy = "" 'User.Identity.Name
                                arlTC.Add(oTC)
                                Dim nTCOk As Integer = New DealerFacade(User).UpdateTransactionControl(arlTC)

                            Catch ex As Exception
                                nResult = -1
                            End Try
                            If nResult = -1 Then
                                MessageBox.Show(SR.SaveFail)
                            Else
                                MessageBox.Show(SR.SaveSuccess)
                                ClearData()
                            End If
                        End If
                    ElseIf CType(ViewState("vsAction"), String) = "edit" Then
                        Dim bCheckUpdate As Boolean = True
                        CheckExistingSearchTerm(bCheckUpdate, strErrTermUpdate, "SearchTerm1", txtSearch1.Text, objDealer)
                        CheckExistingSearchTerm(bCheckUpdate, strErrTermUpdate, "SearchTerm2", txtSearch2.Text, objDealer)
                        strErrTermUpdate = ErrMessageForSearchTerm(strErrTermUpdate)
                        If bCheckUpdate Then
                            Try
                                nResult = New DealerFacade(User).UpdateDealer(objDealer, arlBusinessArea, arlVehicleCategory, arlDealerProfile)
                                CreateSAPDirectory(objDealer.SearchTerm2)
                            Catch ex As Exception
                                nResult = -1
                            End Try

                            If nResult = -1 Then
                                MessageBox.Show(SR.UpdateFail)
                            Else
                                'ClearData()
                                'Page.RegisterClientScriptBlock("BackUrl", "<script language=javascript>alert('Update Data Dealer Sukses'); window.history.go(" & (intPostBack * -1) - 2 & ");</script>")
                                Response.Redirect("../General/FrmListDealerMantenance.aspx?DealerID=" + CType(objDealer.ID, String))
                            End If
                        End If
                    End If
                Case 1
                    MessageBox.Show(SR.DataIsExist("Kode Dealer"))
                Case 2
                    MessageBox.Show(SR.DataNotChooseYet("Tipe Organisasi"))
                Case 3
                    MessageBox.Show(SR.GridIsEmpty("Nama Propinsi"))
                Case 4
                    MessageBox.Show(SR.GridIsEmpty("Nama Kota"))
                Case 5
                    MessageBox.Show(SR.DataNotChooseYet("Area Bisnis"))
                Case 6
                    MessageBox.Show("Term Cari 1 tidak boleh melebihi 20 karakter")
                Case 7
                    MessageBox.Show("Term Cari 2 harus 4 karakter")
                Case 8
                    MessageBox.Show("Term Cari 2 harus 4 atau 6 karakter")
            End Select
        End If

    End Sub

    Private Sub CreateSAPDirectory(ByVal SearchTerm2 As String)
        Dim sFolder1 As String = KTB.DNet.Lib.WebConfig.GetValue("DNetServerFolder") & SearchTerm2
        Dim sFolder2 As String = KTB.DNet.Lib.WebConfig.GetValue("DNetServerFolder") & SearchTerm2 & "\history"
        Dim oDI As IO.DirectoryInfo


        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _sapServer)
        Dim succes As Boolean = False
        Try
            succes = imp.Start
            If succes Then

                oDI = New IO.DirectoryInfo(sFolder1)
                If Not oDI.Exists Then
                    oDI.Create()
                End If

                oDI = New IO.DirectoryInfo(sFolder2)
                If Not oDI.Exists Then
                    oDI.Create()
                End If

                imp.StopImpersonate()
                imp = Nothing
            Else
                MessageBox.Show("Gagal Login ke SAP Server. Gagal create folder history di SAP")
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal create folder history di SAP")
        End Try
    End Sub

    Private Sub CheckExistingSearchTerm(ByRef bcheck As Boolean, ByRef strError As String, ByVal strColName As String, ByVal strColVal As String, ByVal ObjDealer As Dealer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Dealer), strColName, MatchType.Exact, strColVal))
        If CType(ViewState("vsAction"), String) = "edit" Then
            criterias.opAnd(New Criteria(GetType(Dealer), "ID", MatchType.No, ObjDealer.ID))
        End If
        Dim arrDealer As ArrayList = New DealerFacade(User).Retrieve(criterias)
        If arrDealer.Count > 0 Then
            bcheck = False
            If strColName = "SearchTerm1" Then
                strError = strError + "1"
            ElseIf strColName = "SearchTerm2" Then
                strError = strError + "2"
            End If
        End If
    End Sub

    Private Function ErrMessageForSearchTerm(ByVal strErr As String) As String
        If Len(strErr) > 0 Then
            If Len(strErr) = 1 Then
                If strErr = "1" Then
                    MessageBox.Show(SR.DataDuplicate("Term Cari 1", "Term Cari 1", "Dealer"))
                ElseIf strErr = "2" Then
                    MessageBox.Show(SR.DataDuplicate("Term Cari 2", "Term Cari 2", "Dealer"))
                End If
            ElseIf Len(strErr) = 2 Then
                MessageBox.Show(SR.DataDuplicate("Term Cari 1", "Term Cari 1", "Dealer") & "\n" & SR.DataDuplicate("Term Cari 2", "Term Cari 2", "Dealer"))
            End If
            strErr = ""
        End If
        Return strErr
    End Function

    Private Sub ClearData()

        txtDealerCode.Text = ""
        txtName.Text = ""
        txtNickNameDigital.Text = ""
        txtNickNameEcomm.Text = ""
        txtLongitude.Text = ""
        txtLatitude.Text = ""
        'ddlTitle.SelectedValue = "1"
        ddlTitle.SelectedIndex = 0
        txtSearch1.Text = ""
        txtSearch2.Text = ""
        ddlGroup.SelectedValue = "0"
        txtAddress.Text = ""
        ddlCity.SelectedValue = "0"
        ddlCity.Enabled = False

        txtSPANumber.Text = ""
        txtSPADate.Text = ""

        ddlProvince.SelectedValue = "0"
        ddlMainArea.SelectedValue = "0"
        ddlArea1.SelectedValue = "0"
        ddlArea2.SelectedValue = "0"
        txtWeb.Text = ""
        txtPostCode.Text = ""
        txtLegalStatus.Text = ""
        txtMainDealer.Text = ""
        ddlStatus.SelectedIndex = 0

        txtEmailAdd.Text = ""
        chbxFreePPh.Checked = False
        txtValidFrom.Text = String.Empty
        txtValidTo.Text = String.Empty
        'txtTelpArea.Text = ""
        'txtTelpNo.Text = ""
        'txtFaxArea.Text = ""
        'txtFaxNo.Text = ""
        txtfullFax.Text = String.Empty
        txtfullNoTelepon.Text = String.Empty
        LblLastChange.Text = ""

        txtNoPersetujuan.Text = ""
        icTglPersetujuan.Value = DateTime.Now.Date


        cbSalesUnit.Checked = False
        cbSpareParts.Checked = False
        cbService.Checked = False

        txtContactPerson11.Text = ""
        txtHP11.Text = ""
        txtEmail11.Text = ""
        lblLastUpdate11.Text = ""
        txtPICArea.Text = ""
        txtPICRegion.Text = ""
        txtPICSubArea.Text = ""
        txtNameBM.Text = ""
        txtEmailBM.Text = ""
        txtHPBM.Text = ""
        txtContactPerson21.Text = ""
        txtHP21.Text = ""
        txtEmail21.Text = ""
        lblLastUpdate21.Text = ""

        txtContactPerson31.Text = ""
        txtHP31.Text = ""
        txtEmail31.Text = ""
        lblLastUpdate31.Text = ""


    End Sub
    Private Sub ddlArea1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlArea1.SelectedIndexChanged
        BindDdlArea2()
    End Sub

    Private Sub ddlProvince_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlProvince.SelectedIndexChanged
        Dim nTotRow As Integer = 0
        Dim nPageNumber As Integer = 1
        Dim nPageSize As Integer = 50
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A")) 'A = Aktif; X = Tidak Aktif
        criterias.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, CType(ddlProvince.SelectedValue, Integer)))
        ddlCity.Enabled = True
        ddlCity.DataSource = New CityFacade(User).RetrieveActiveList(criterias, nPageNumber, nPageSize, nTotRow, "CityName", Sort.SortDirection.ASC)
        ddlCity.DataTextField = "CityName"
        ddlCity.DataValueField = "ID"
        ddlCity.DataBind()
        ddlCity.Items.Insert(0, New ListItem("", "0"))
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ClearData()
        ViewState("vsAction") = "insert"
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("./FrmListDealerMantenance.aspx")
    End Sub

    Private Sub ddlMainArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMainArea.SelectedIndexChanged
        BindDdlArea1()
        BindDdlArea2()
    End Sub
End Class

