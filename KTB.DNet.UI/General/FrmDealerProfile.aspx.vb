Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.Drawing.Color
Imports System.Drawing
Imports System.Drawing.Imaging
Imports KTB.DNet.BusinessFacade.Service
Imports System.IO
Imports System.Configuration

#Region "Custom NameSpace"
Imports KTB.DNet.BusinessFacade.Profile
#End Region


Public Class FrmDealerProfile
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents PnlManajemen As System.Web.UI.WebControls.Panel
    Protected WithEvents lblAddress As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblPhone As System.Web.UI.WebControls.Label
    Protected WithEvents lblFax As System.Web.UI.WebControls.Label
    Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
    Protected WithEvents lblArea As System.Web.UI.WebControls.Label
    Protected WithEvents lblGroup As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents btnUpdate As System.Web.UI.WebControls.Button
    Protected WithEvents txtShowroomFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtStuctureFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtSalesForceFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtClassification As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHeldYear As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTelephone As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusAdd As System.Web.UI.WebControls.Label
    Protected WithEvents lblUploadShowroom As System.Web.UI.WebControls.Label
    Protected WithEvents dgDealerProfilePhoto As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblUploadSalesForce As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblClasification As System.Web.UI.WebControls.Label
    Protected WithEvents lblHeldYear As System.Web.UI.WebControls.Label
    Protected WithEvents lblStrukturOrg As System.Web.UI.WebControls.Label

    ' base on profilGroup table
    Private intProfileGroup_Dealer As Integer = 18

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

        Dim isDisableProfile As Boolean = False
        objUserInfo = sessHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            isDisableProfile = False
        ElseIf objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            isDisableProfile = True
        End If

        If Not IsNothing(Request.QueryString("DealerCode")) Then
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(Request.QueryString("DealerCode"))
            RenderProfilePanel(objDealer, New ProfileGroupFacade(User).Retrieve("dealer_prf"), EnumProfileType.ProfileType.DEALER, PnlManajemen, isDisableProfile)
        Else
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("dealer_prf"), EnumProfileType.ProfileType.DEALER, PnlManajemen, isDisableProfile)
        End If
        'If Request.QueryString("DealerCode") = "" Then
        '    RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve("dealer_prf"), EnumProfileType.ProfileType.DEALER, PnlManajemen)
        'Else
        '    Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CType(Request.QueryString("DealerCode"), String))
        '    RenderProfilePanel(objDealer, New ProfileGroupFacade(User).Retrieve("dealer_prf"), EnumProfileType.ProfileType.DEALER, PnlManajemen)
        'End If

    End Sub

#End Region

#Region "PrivateVariables"
    Private _DealerProfilePhotoFacade As New DealerProfilePhotoFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper
    Private intMaxDataGrid As Integer = 10
    Private intCurrDataGrid As Integer
    Private objDealer As Dealer
    Private objUserInfo As UserInfo
#End Region

#Region "Privilege"
    Private Sub CheckPrivDealerOnly()
        If Not SecurityProvider.Authorize(context.User, SR.ProfileListCreate_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Dealer - Create Profile")
        End If
    End Sub

    dim bCekKTBEditPriv as Boolean = SecurityProvider.Authorize(context.User, SR.ProfileListEdit_Privilege) 
#End Region

#Region "Custom Method"

    Private Sub RenderProfilePanel(ByVal objDealer As Dealer, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel, ByVal isReadonly As Boolean)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(isReadonly)
        If Not objDealer Is Nothing Then
            objRenderPanel.GeneratePanel(objDealer.ID, objPanel, objGroup, profileType, User)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User)
        End If
    End Sub

    Private Sub RenderProfilePanel(ByVal objDealer As Dealer, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel)
        Dim isReadOnly As Boolean = False
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(isReadOnly)

        If Not objDealer Is Nothing Then
            objRenderPanel.GeneratePanel(objDealer.ID, objPanel, objGroup, profileType, User, True)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User, True)
        End If
    End Sub


    Private Sub CekStatusDealer()
        'get dealer id
        Dim dealerCode As String = txtDealerCode.Text
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, dealerCode.Trim))
        Dim arrDealer As ArrayList = New DealerFacade(User).Retrieve(criterias)
        Dim objdealer As Dealer = arrDealer(0)

        'get dealer additional
        Dim arrDealerAdd As ArrayList = New DealerAdditionalFacade(User).RetrieveByDealerID(objdealer.ID)
        If arrDealerAdd.Count > 0 Then
            Dim objDealerAdd As DealerAdditional = arrDealerAdd(0)
            viewstate.Add("DealerStatus", "Update")
            sessHelper.SetSession("objDealerAdd", objDealerAdd)
        Else
            viewstate.Add("DealerStatus", "Insert")
        End If
    End Sub

    ' saving on table dealerAdditional
    Private Sub UploadFile()
        If viewstate("DealerStatus") = "Update" Then
            Dim objDealerAdd As DealerAdditional = sessHelper.GetSession("objDealerAdd")

            objDealerAdd.Classification = txtClassification.Text
            If IsNumeric(txtHeldYear.Text.Trim) And (objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                objDealerAdd.HeldYear = CType(txtHeldYear.Text.Trim, Integer)
            End If
            'insert showroom audit first
            If txtShowroomFile.Value <> "" OrElse txtShowroomFile.Value <> Nothing Then
                Dim SrcFile As String = Path.GetFileName(txtShowroomFile.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("UploadDealerProfile") & "\" & txtDealerCode.Text.Trim & "\Audit\" & SrcFile   '-- Destination file
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        txtShowroomFile.PostedFile.SaveAs(DestFile)
                        objDealerAdd.ShowroomFile = "\" & txtDealerCode.Text.Trim & "\Audit\" & SrcFile
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            End If

            'insert organisasi
            If txtStuctureFile.Value <> "" OrElse txtStuctureFile.Value <> Nothing Then
                Dim SrcFile As String = Path.GetFileName(txtStuctureFile.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("UploadDealerProfile") & "\" & txtDealerCode.Text.Trim & "\Organisasi\" & SrcFile   '-- Destination file
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        txtStuctureFile.PostedFile.SaveAs(DestFile)
                        objDealerAdd.StuctureFile = "\" & txtDealerCode.Text.Trim & "\Organisasi\" & SrcFile
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            End If

            'insert SalesFoce
            If txtSalesForceFile.Value <> "" OrElse txtSalesForceFile.Value <> Nothing Then
                Dim SrcFile As String = Path.GetFileName(txtSalesForceFile.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("UploadDealerProfile") & "\" & txtDealerCode.Text.Trim & "\SalesForce\" & SrcFile   '-- Destination file
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        txtSalesForceFile.PostedFile.SaveAs(DestFile)
                        objDealerAdd.SalesForceFile = "\" & txtDealerCode.Text.Trim & "\SalesForce\" & SrcFile
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            End If

            'setelah sukses lakukan update ke db DealerAdditional
            Try
                Dim iresult As Integer = New DealerAdditionalFacade(User).Update(objDealerAdd)
            Catch ex As Exception
                MessageBox.Show("Gagal melakukan update database")
            End Try
        Else
            'doing insert process
            Dim objDealerAdditional As New DealerAdditional
            Dim dealerCode As String = txtDealerCode.Text
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, dealerCode.Trim))
            Dim arrDealer As ArrayList = New DealerFacade(User).Retrieve(criterias)
            Dim objdealer As Dealer = arrDealer(0)

            objDealerAdditional.Dealer = objdealer

            objDealerAdditional.Classification = txtClassification.Text
            objDealerAdditional.HeldYear = CType(txtHeldYear.Text, Integer)

            'insert showroom audit first
            If txtShowroomFile.Value <> "" OrElse txtShowroomFile.Value <> Nothing Then
                Dim SrcFile As String = Path.GetFileName(txtShowroomFile.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("UploadDealerProfile") & "\" & txtDealerCode.Text.Trim & "\Audit\" & SrcFile   '-- Destination file
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        txtShowroomFile.PostedFile.SaveAs(DestFile)
                        objDealerAdditional.ShowroomFile = "\" & txtDealerCode.Text.Trim & "\Audit\" & SrcFile
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            End If

            'insert organisasi
            If txtStuctureFile.Value <> "" OrElse txtStuctureFile.Value <> Nothing Then
                Dim SrcFile As String = Path.GetFileName(txtStuctureFile.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("UploadDealerProfile") & "\" & txtDealerCode.Text.Trim & "\Organisasi\" & SrcFile   '-- Destination file
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        txtStuctureFile.PostedFile.SaveAs(DestFile)
                        objDealerAdditional.StuctureFile = "\" & txtDealerCode.Text.Trim & "\Organisasi\" & SrcFile
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            End If

            'insert SalesFoce
            If txtSalesForceFile.Value <> "" OrElse txtSalesForceFile.Value <> Nothing Then
                Dim SrcFile As String = Path.GetFileName(txtSalesForceFile.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("UploadDealerProfile") & "\" & txtDealerCode.Text.Trim & "\SalesForce\" & SrcFile   '-- Destination file
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        txtSalesForceFile.PostedFile.SaveAs(DestFile)
                        objDealerAdditional.SalesForceFile = "\" & txtDealerCode.Text.Trim & "\SalesForce\" & SrcFile
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            End If

            'setelah sukses lakukan insert ke db DealerAdditional
            Try
                Dim iresult As Integer = New DealerAdditionalFacade(User).Insert(objDealerAdditional)
            Catch ex As Exception
                MessageBox.Show("Gagal melakukan insert database")
            End Try
        End If



    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsNothing(sessHelper.GetSession("LOGINUSERINFO")) Then
            Dim objUserInfo As UserInfo = New UserInfo
            objUserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            If objUserInfo.Dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                CheckPrivDealerOnly()
            End If
            btnSave.Visible = bCekKTBEditPriv
        End If
        If Not IsPostBack Then
            If Not IsNothing(sessHelper.GetSession("LOGINUSERINFO")) Then
                If Not IsNothing(Request.QueryString("Save")) Then
                    MessageBox.Show(SR.UpdateSucces)
                End If

                If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    ' case Dealer
                    SetControl(True)
                    If Not IsNothing(Request.QueryString("DealerCode")) Then
                        ViewData(Request.QueryString("DealerCode"))
                    Else
                        ViewData(objUserInfo.Dealer.DealerCode)
                    End If
                    'ControlLock(True)
                ElseIf objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    ' case KTB
                    SetControl(False)

                    BindControlsAttribute()
                    If Not IsNothing(Request.QueryString("DealerCode")) Then
                        ViewData(Request.QueryString("DealerCode"))
                    Else
                        ViewData(objUserInfo.Dealer.DealerCode)
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub SetControl(ByVal blnVisible As Boolean)
        txtDealerCode.ReadOnly = True
        lblPopUpDealer.Visible = False ' blnVisible
        lblUploadShowroom.Visible = Not blnVisible
        txtShowroomFile.Visible = Not blnVisible
        txtClassification.Visible = Not blnVisible
        lblClasification.Visible = blnVisible
        txtStuctureFile.Visible = blnVisible
        lblStrukturOrg.Visible = blnVisible
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        Else
            ' case KTB
            dgDealerProfilePhoto.Columns(3).Visible = False ' menu Edit visible
            dgDealerProfilePhoto.ShowFooter = False
        End If
    End Sub

    Private Sub BindControlsAttribute()
        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
    End Sub

    Private Sub ViewData(ByVal _dealerCode As String)
        If _dealerCode <> String.Empty Then
            'Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CType(Request.QueryString("DealerCode"), String))
            'Dim objDealerAdditional As DealerAdditional = New DealerAdditionalFacade(User).Retrieve(CType(Request.QueryString("DealerCode"), String))
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(_dealerCode)
            Dim objDealerAdditional As DealerAdditional = New DealerAdditionalFacade(User).Retrieve(_dealerCode)

            If Not objDealer Is Nothing Then
                txtDealerCode.Text = objDealer.DealerCode
                lblDealerName.Text = objDealer.DealerName
                lblAddress.Text = objDealer.Address
                lblCity.Text = objDealer.City.CityName
                lblGroup.Text = objDealer.DealerGroup.GroupName
                lblTelephone.Text = objDealer.Phone
                lblFax.Text = objDealer.Fax
                lblEmail.Text = objDealer.Email
                lblStatus.Text = CType(objDealer.Status, EnumDealerStatus.DealerStatus).ToString
                If Not IsNothing(objDealer.Area1) Then
                    lblArea.Text = objDealer.Area1.AreaCode
                Else
                    lblArea.Text = ""
                End If
                SetStatusAdd(CType(objDealer.SalesUnitFlag, Byte) + CType(objDealer.ServiceFlag, Byte) + CType(objDealer.SparepartFlag, Byte))
            End If

            If Not objDealerAdditional Is Nothing Then
                txtHeldYear.Text = IIf(objDealerAdditional.HeldYear.ToString = "0", "", objDealerAdditional.HeldYear.ToString)
                lblHeldYear.Text = IIf(objDealerAdditional.HeldYear.ToString = "0", "", objDealerAdditional.HeldYear.ToString)
                If (objDealerAdditional.HeldYear > 0) And (objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    txtHeldYear.Visible = False
                    lblHeldYear.Visible = True
                Else
                    If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        txtHeldYear.Visible = True
                        lblHeldYear.Visible = False
                    Else
                        ' KTB hanya bisa view saja untuk tahun
                        txtHeldYear.Visible = False
                        lblHeldYear.Visible = True
                    End If
                End If
                txtClassification.Text = objDealerAdditional.Classification
                lblClasification.Text = objDealerAdditional.Classification
            End If

            ' check data profile
            ' if exist      -> mode ubah
            ' if nothing    -> mode simpan
            Dim criterias As New CriteriaComposite(New Criteria(GetType(DealerProfile), "Dealer.ID", MatchType.Exact, objDealer.ID))
            Dim arrDealerProfile As ArrayList = New DealerProfileFacade(User).Retrieve(criterias)

            btnUpdate.Visible = False
            'If arrDealerProfile.Count > 0 Then
            '    btnSave.Enabled = False
            '    btnUpdate.Enabled = True
            'Else
            '    btnSave.Enabled = True
            '    btnUpdate.Enabled = False
            'End If

            ' bindgrid if dealercode have value
            BindDataGrid(0)

        End If
        'If Request.QueryString("DealerCode") <> "" Then


        'End If
    End Sub

    '28-Sep-2007    Deddy H     Menambahkan status addtional secara auto base on data flagnya
    Private Sub SetStatusAdd(ByVal bytVal As Byte)
        lblStatusAdd.Text = CType(bytVal, String) & "S"
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (txtHeldYear.Text = "") And (objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            MessageBox.Show("Tahun berdiri tidak boleh kosong")
            Return
        End If
        If (objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            If (CType(txtHeldYear.Text, Integer) <= 0) Then
                MessageBox.Show("Tahun berdiri harus lebih besar dari 0 ")
                Return
            End If
        End If

        Update()
    End Sub

    Private Sub Insert()
        If txtDealerCode.Text <> "" Then
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
            Dim objRenderPanel As RenderingProfile = New RenderingProfile
            ' From profileGroup database, 18 = Dealer-Manajemen
            Dim al As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("dealer_prf"), CType(EnumProfileType.ProfileType.DEALER, Short), User)
            Dim nResult As Integer = -1
            nResult = New DealerFacade(User).Insert(objDealer, al)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                CekStatusDealer()
                UploadFile()
                MessageBox.Show(SR.SaveSuccess)
                Response.Redirect("FrmDealerProfile.aspx?DealerCode=" + Trim(txtDealerCode.Text))
            End If
        Else
            MessageBox.Show("Silakan pilih Kode Dealer terlebih dahulu")
        End If
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        'If IsNumeric(txtHeldYear.Text.Trim) Then
        '    Update()
        'Else
        '    MessageBox.Show("Tahun berdiri tidak valid")
        'End If
    End Sub

    Private Sub Update()
        If txtDealerCode.Text <> "" Then
            Dim objRenderPanel As RenderingProfile = New RenderingProfile
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
            Dim al As ArrayList = objRenderPanel.RetrieveProfileValue(Me, New ProfileGroupFacade(User).Retrieve("dealer_prf"), CType(EnumProfileType.ProfileType.DEALER, Short), User)
            Dim nResult = New DealerFacade(User).Update(objDealer, al, New ProfileGroupFacade(User).Retrieve("dealer_prf"))
            If nResult = -1 Then
                MessageBox.Show("Record Gagal Diupdate")
            Else
                CekStatusDealer()
                UploadFile()
                If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    Response.Redirect("FrmDealerProfile.aspx?Save=True")
                Else
                    MessageBox.Show(SR.UpdateSucces)
                End If

            End If
        Else
            MessageBox.Show("Silakan pilih Kode Dealer terlebih dahulu")
        End If

    End Sub


    Private Sub dgDealerProfilePhoto_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDealerProfilePhoto.SortCommand
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
        dgDealerProfilePhoto.SelectedIndex = -1
        dgDealerProfilePhoto.CurrentPageIndex = 0
        BindDataGrid(dgDealerProfilePhoto.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(DealerProfilePhoto), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtDealerCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(DealerProfilePhoto), "Dealer.DealerCode", MatchType.Exact, Trim(txtDealerCode.Text)))
        End If

        arrList = _DealerProfilePhotoFacade.RetrieveByCriteria(criterias, idxPage + 1, dgDealerProfilePhoto.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgDealerProfilePhoto.DataSource = arrList
        dgDealerProfilePhoto.VirtualItemCount = totalRow
        dgDealerProfilePhoto.DataBind()
        intCurrDataGrid = arrList.Count
    End Sub

    Private Sub dgDealerProfilePhoto_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDealerProfilePhoto.PageIndexChanged
        dgDealerProfilePhoto.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgDealerProfilePhoto.CurrentPageIndex)
    End Sub

    Private Sub dgDealerProfilePhoto_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDealerProfilePhoto.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objDealerProfilePhoto As DealerProfilePhoto = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgDealerProfilePhoto.CurrentPageIndex * dgDealerProfilePhoto.PageSize)

            ' untuk bagian item / alternate item
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                ' mengisi value
                Dim lbtnDeleteNew As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnDeleteNew.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                lbtnDeleteNew.CommandArgument = objDealerProfilePhoto.ID

                Dim lblInitialFileNameNew As Label = CType(e.Item.FindControl("lblInitialFileName"), Label)
                lblInitialFileNameNew.Text = objDealerProfilePhoto.InitialFileName

            End If

            ' untuk bagian edit item
            If e.Item.ItemType = ListItemType.EditItem Then
                Dim lbtnSaveNew As LinkButton = CType(e.Item.FindControl("lbtnSave"), LinkButton)
                lbtnSaveNew.CommandArgument = objDealerProfilePhoto.ID

                Dim txtEditInitialFileNameNew As HtmlInputFile = CType(e.Item.FindControl("txtEditInitialFileName"), HtmlInputFile)
                'txtEditInitialFileNameNew.Value = objDealerProfilePhoto.InitialFileName


            End If

            ' untuk bagian footer
            If e.Item.ItemType = ListItemType.Footer Then
                Dim txtAddInitialFileNameNew As HtmlInputFile = CType(e.Item.FindControl("txtAddInitialFileName"), HtmlInputFile)
                txtAddInitialFileNameNew.Value = ""
            End If

        End If
    End Sub

    Private Sub dgDealerProfilePhoto_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDealerProfilePhoto.ItemCommand
        Dim objPostedData As HttpPostedFile
        Dim FileName As String      ' get only file name
        Dim imageFile As Byte()

        ' take data from related component
        Dim facade As DealerProfilePhotoFacade = New DealerProfilePhotoFacade(User)

        If e.CommandName = "Delete" Then
            Dim objDealerProfilePhoto As DealerProfilePhoto = facade.Retrieve(CInt(e.CommandArgument))
            Dim result As Integer = facade.DeleteFromDB(objDealerProfilePhoto)
            BindDataGrid(0)
        End If

        If e.CommandName = "Add" Then
            Dim txtAddInitialFileNameNew As HtmlInputFile = CType(e.Item.FindControl("txtAddInitialFileName"), HtmlInputFile)

            ' check validation
            If dgDealerProfilePhoto.Items.Count >= intMaxDataGrid Then
                MessageBox.Show("Data File Image, telah melewati kapasitas, max hanya " & intMaxDataGrid & " file saja")
                Return
            End If

            If txtDealerCode.Text = "" Then
                MessageBox.Show("DealerCode harus dipilih dahulu !")
                Return
            End If

            If txtAddInitialFileNameNew.Value = "" Then
                MessageBox.Show("File image harus dipilih dahulu !")
                Return
            End If


            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(Trim(txtDealerCode.Text))
            Dim objDealerProfilePhoto As DealerProfilePhoto = New DealerProfilePhoto

            objPostedData = txtAddInitialFileNameNew.PostedFile

            objDealerProfilePhoto.Dealer = objDealer
            FileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)
            objDealerProfilePhoto.InitialFileName = FileName

            ' penambahan image menggunakan method conversi
            If (txtAddInitialFileNameNew.PostedFile.FileName <> String.Empty) Then
                imageFile = UploadFile(txtAddInitialFileNameNew)
                If Not imageFile Is Nothing Then
                    objDealerProfilePhoto.Image = imageFile
                Else
                    Return
                End If
            End If

            Dim result As Integer = facade.Insert(objDealerProfilePhoto)

            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
            BindDataGrid(0)
        End If

        If e.CommandName = "Edit" Then
            dgDealerProfilePhoto.ShowFooter = False
            dgDealerProfilePhoto.EditItemIndex = e.Item.ItemIndex
            BindDataGrid(0)
        End If

        If e.CommandName = "Cancel" Then
            dgDealerProfilePhoto.ShowFooter = True
            dgDealerProfilePhoto.EditItemIndex = -1
            BindDataGrid(0)
        End If

        ' update data yg sdh ada
        If e.CommandName = "Save" Then
            Dim objDealerProfilePhoto As DealerProfilePhoto
            objDealerProfilePhoto = New DealerProfilePhotoFacade(User).Retrieve(CInt(e.CommandArgument))

            Dim txtEditInitialFileNameNew As HtmlInputFile = CType(e.Item.FindControl("txtEditInitialFileName"), HtmlInputFile)

            ' check validation
            If txtEditInitialFileNameNew.Value = "" Then
                MessageBox.Show("File image harus dipilih dahulu !")
                Return
            End If

            objPostedData = txtEditInitialFileNameNew.PostedFile
            FileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)
            objDealerProfilePhoto.InitialFileName = FileName

            ' penambahan image menggunakan method conversi
            If (txtEditInitialFileNameNew.PostedFile.FileName <> String.Empty) Then
                imageFile = UploadFile(txtEditInitialFileNameNew)
                If Not imageFile Is Nothing Then
                    objDealerProfilePhoto.Image = imageFile
                Else
                    Return
                End If
            End If

            Dim result As Integer = facade.Update(objDealerProfilePhoto)

            If result = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
            End If
            dgDealerProfilePhoto.ShowFooter = True
            dgDealerProfilePhoto.EditItemIndex = -1
            BindDataGrid(0)
        End If
    End Sub

    Private Function UploadFile(ByVal photoSrc As HtmlInputFile) As Byte()
        Dim nResult() As Byte

        Try
            If IsValidPhoto(photoSrc.PostedFile) Then
                Dim inStream As System.IO.Stream = photoSrc.PostedFile.InputStream()
                Dim ByteRead(DealerProfilePhoto.MAX_PHOTO_SIZE) As Byte
                Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, DealerProfilePhoto.MAX_PHOTO_SIZE)
                If ReadCount = 0 Then
                    Throw New InvalidConstraintException(SR.DataNotFound("Photo"))
                End If
                ReDim nResult(ReadCount)
                Array.Copy(ByteRead, nResult, ReadCount)
            Else
                'Throw New DataException("Bukan file gambar atau Ukuran file > " & _
                '                        CType(DealerProfilePhoto.MAX_PHOTO_SIZE / 1024, String) & "KB")
                MessageBox.Show("Bukan file gambar atau Ukuran file > " & _
                                        CType(DealerProfilePhoto.MAX_PHOTO_SIZE / 1024, String) & "KB")
            End If
        Catch
            '    Throw
        End Try

        Return nResult
    End Function

    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(DealerProfilePhoto.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= DealerProfilePhoto.MAX_PHOTO_SIZE)
        Return (containImage AndAlso sizeValid)
    End Function

    Private Function ControlLock(ByVal bval As Boolean)
        txtDealerCode.ReadOnly = bval
        txtClassification.ReadOnly = bval
        txtHeldYear.ReadOnly = bval

        '--manajemen

        txtSalesForceFile.Disabled = bval
        txtShowroomFile.Disabled = bval
        txtStuctureFile.Disabled = bval

    End Function

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmDealerList.aspx?isback=1")
    End Sub



End Class
