Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports System.Drawing.Color
Imports System.Web.UI.WebControls.BorderStyle
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports System.Collections.Generic
Imports System.Linq
Imports OfficeOpenXml
Imports GlobalExtensions
Imports System.IO

Public Class FrmCourseEvaluationList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents dtgClassRegistration As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblClassCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblClassName As System.Web.UI.WebControls.Label
    Protected WithEvents lblStart As System.Web.UI.WebControls.Label
    Protected WithEvents lblFinish As System.Web.UI.WebControls.Label
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    'Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlClass As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    'Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeKelas As System.Web.UI.WebControls.Label
    Protected WithEvents lblMulai As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaKelas As System.Web.UI.WebControls.Label
    Protected WithEvents lblSelesai As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeOrganisasi As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaOrganisasi As System.Web.UI.WebControls.Label
    Protected WithEvents lblCerConfig As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerSelection As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNamaPenandatangan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJabatanPenandatangan As System.Web.UI.WebControls.TextBox
    Protected WithEvents pnlDealer As System.Web.UI.WebControls.Panel
    Protected WithEvents btnCetak As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents pnlCriteria As System.Web.UI.WebControls.Panel
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgClassRegistration As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDownLoad As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents txtNoReg As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlFiscalYear As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents txtYear As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeKategori As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchKodeKategori As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeKelas As System.Web.UI.WebControls.TextBox
    Protected WithEvents hdnArea As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hdnCerID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents lblPopUpClass As System.Web.UI.WebControls.Label
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Private _sessHelper As SessionHelper = New SessionHelper


    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents trSertifikat As System.Web.UI.HtmlControls.HtmlTableRow

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private objDealer As Dealer
    Private CellDisabled As List(Of String) = New List(Of String)
    Private ssobjClass As TrClass = New TrClass
    Private helpers As New TrainingHelpers(Me.Page)

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        helpers.CheckPrivilegeTransaction("tr6" + AreaId.PrivilegeTrainingType)
        If Not IsPostBack Then
            helpers.CheckDueDateTagihan(AreaId)
            InitiatePage()
            ActivateUserPrivilege()
            BindDDLTahunFiskal(ddlFiscalYear)
            If Not IsNothing(Session("DEALER")) Then
                btnCancel.Enabled = False
                Dim ObjDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
                Dim StrDealerCode As String = ObjDealer.DealerCode
                lblKodeOrganisasi.Text = ObjDealer.DealerCode + " / " + ObjDealer.SearchTerm1
                lblNamaOrganisasi.Text = ObjDealer.DealerName

                'pasang privilege untuk ktb atau dealer
                If ObjDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                    If helpers.IsEdit Then
                        txtDealerSelection.Text = String.Empty
                        txtDealerSelection.Enabled = True
                        lblPopUpDealer.Visible = True
                        pnlDealer.Visible = True
                    End If
                Else
                    If helpers.IsView Then
                        btnSave.Visible = False
                        txtDealerSelection.Text = ObjDealer.DealerCode
                        txtDealerSelection.Enabled = False
                        lblPopUpDealer.Visible = False
                        pnlDealer.Visible = False
                        btnSubmit.Visible = False
                    End If
                End If

                _sessHelper.SetSession("sessDealer", ObjDealer)
                _sessHelper.SetSession("sessDealerLogin", ObjDealer.DealerCode)
                If AreaId.NotNullorEmpty Then
                    Select Case AreaId
                        Case "1"
                            lblSearchKodeKategori.Attributes("onclick") = "ShowPPCourseSelection('sales')"
                            lblPageTitle.Text = "Training Sales - Evaluasi Hasil Training"
                        Case "2"
                            lblSearchKodeKategori.Attributes("onclick") = "ShowPPCourseSelection('ass')"
                            lblPageTitle.Text = "Training After Sales - Evaluasi Hasil Training"
                        Case "3"
                            lblSearchKodeKategori.Attributes("onclick") = "ShowPPCourseSelection('cs')"
                            lblPageTitle.Text = "Training Customer Satisfaction - Evaluasi Hasil Training"
                    End Select
                    hdnArea.Value = AreaId
                Else
                    lblPageTitle.Text = "Training - Evaluasi Hasil Training"
                End If

                If Request.QueryString("QS") Is Nothing And Request.QueryString("Rank") Is Nothing Then
                    btnBack.Visible = False
                End If

                'kalo datangnya dari FrmDisplayPersonalEvaluation.aspx => button back 
                If Not Request.QueryString("form") Is Nothing Then
                    If CType(Request.QueryString("form"), String).Trim() = "FrmDisplayPersonalEvaluation.aspx" Then
                        If Not _sessHelper.GetSession("sessClassRegistration") Is Nothing Then
                            txtKodeKategori.Enabled = True
                            txtKodeKategori.Text = CType(_sessHelper.GetSession("Sess_ddlCategory"), String)
                            lblSearchKodeKategori.Visible = True
                            txtKodeKelas.Enabled = True
                            txtKodeKelas.Text = _sessHelper.GetSession("Sess_ddlClass")
                            lblPopUpClass.Visible = True

                            RestorePreviousState()
                            Dim arlClass As New ArrayList
                            arlClass = CType(_sessHelper.GetSession("sessClassRegistration"), ArrayList)
                            dtgClassRegistration.DataSource = arlClass
                            dtgClassRegistration.CurrentPageIndex = 0
                            If Not IsNothing(_sessHelper.GetSession("sessClassRegistration")) And arlClass.Count > 0 Then
                                btnCetak.Disabled = False 'true

                                Dim objClass As TrClass = CType(arlClass(0), TrClassRegistration).TrClass
                                If objClass.SubmitStatus = 1 Then
                                    btnSubmit.Enabled = False
                                    btnSave.Enabled = False
                                Else
                                    btnSubmit.Enabled = True
                                    btnSave.Enabled = True
                                End If

                                btnDownLoad.Enabled = True
                            End If
                            dtgClassRegistration.DataBind()
                        End If
                    Else
                        If Not _sessHelper.GetSession("sessClassRegistration") Is Nothing Then
                            txtKodeKategori.Enabled = False
                            lblSearchKodeKategori.Visible = False
                            txtKodeKelas.Enabled = False
                            lblPopUpClass.Visible = False

                            RestorePreviousState()
                            dtgClassRegistration.DataSource = CType(_sessHelper.GetSession("sessClassRegistration"), ArrayList)
                            dtgClassRegistration.CurrentPageIndex = 0
                            If Not IsNothing(dtgClassRegistration.DataSource) And CType(dtgClassRegistration.DataSource, ArrayList).Count > 0 Then
                                btnCetak.Disabled = False 'true
                                btnSave.Visible = False
                                btnSubmit.Visible = False
                                btnDownLoad.Enabled = True
                            End If
                            dtgClassRegistration.DataBind()
                            btnCari.Enabled = False
                            btnBack.Enabled = True
                        End If
                    End If

                    'kalo datangnya dari FrmCertificateLaine3.aspx 
                ElseIf Not Request.QueryString("QS") Is Nothing Then
                    Dim dummyParam As String() = CType(Request.QueryString("QS"), String).Trim().Split(";")
                    If dummyParam.Length > 0 Then
                        If Not dummyParam(0) = String.Empty Then
                            ddlFiscalYear.Text = dummyParam(0).ToString
                            ddlFiscalYear.Enabled = False
                        End If
                    End If
                    If dummyParam.Length > 1 Then
                        If Not dummyParam(1) = String.Empty Then
                            txtKodeKategori.Text = CType(dummyParam(1), String)
                        End If
                    End If

                    If dummyParam.Length > 2 Then

                        txtKodeKelas.Text = CType(dummyParam(2), String)
                        ssobjClass = New TrClassFacade(User).Retrieve(txtKodeKelas.Text)

                        lblClassCode.Text = ssobjClass.ClassCode
                        lblClassName.Text = ssobjClass.ClassName
                        lblStart.Text = ssobjClass.StartDate
                        lblFinish.Text = ssobjClass.FinishDate

                        txtDealerSelection.Enabled = False
                        lblPopUpDealer.Enabled = False
                        ddlFiscalYear.Enabled = False
                        txtNoReg.Enabled = False

                        txtKodeKategori.Enabled = False
                        lblSearchKodeKategori.Visible = False
                        txtKodeKelas.Enabled = False
                        lblPopUpClass.Visible = False

                        'Calculation()
                        btnSave_Click(sender, e)
                    End If

                    'kalo datangnya dari FrmDetailTrTrainee.aspx 
                ElseIf Not Request.QueryString("Rank") Is Nothing Then
                    'LoadByDetailTrainee()
                    Dim dummyParam As String() = CType(Request.QueryString("Rank"), String).Trim().Split(";")
                    If dummyParam.Length > 0 Then
                        Dim oDealer As Dealer = New DealerFacade(User).Retrieve(CType(dummyParam(0), Integer))
                        If Not oDealer Is Nothing Then
                            lblKodeOrganisasi.Text = oDealer.DealerCode + " / " + oDealer.SearchTerm1
                            lblNamaOrganisasi.Text = oDealer.DealerName
                            txtDealerSelection.Text = oDealer.DealerCode
                        End If
                    End If
                    If dummyParam.Length > 1 Then
                        If Not dummyParam(1) = String.Empty Then
                            ddlFiscalYear.SelectedValue = dummyParam(1).ToString
                        End If
                    End If
                    If dummyParam.Length > 2 Then
                        If Not dummyParam(2) = String.Empty Then
                            Dim oTrCourse As TrCourse = New TrCourseFacade(User).Retrieve(CType(dummyParam(2), Integer))
                            If Not oTrCourse Is Nothing Then
                                txtKodeKategori.Text = oTrCourse.CourseCode
                            Else
                                txtKodeKategori.Text = ""
                            End If
                        End If
                    End If

                    If dummyParam.Length > 3 Then

                        Dim oTrClass As TrClass = New TrClassFacade(User).Retrieve(CType(dummyParam(3), Integer))
                        If Not oTrClass Is Nothing Then
                            txtKodeKelas.Text = oTrClass.ClassCode
                            lblClassCode.Text = oTrClass.ClassCode
                            lblClassName.Text = oTrClass.ClassName
                            lblStart.Text = oTrClass.StartDate
                            lblFinish.Text = oTrClass.FinishDate
                        Else
                            txtKodeKelas.Text = ""
                        End If

                    End If
                    If dummyParam.Length > 4 Then
                        txtNoReg.Text = CType(dummyParam(4), String)
                    End If

                    txtDealerSelection.Enabled = False
                    lblPopUpDealer.Enabled = False
                    ddlFiscalYear.Enabled = False
                    txtKodeKategori.Enabled = False
                    lblSearchKodeKategori.Visible = False
                    txtKodeKelas.Enabled = False
                    lblPopUpClass.Visible = False

                    btnCari.Enabled = False
                    btnDownLoad.Enabled = False
                    btnBack.Enabled = True
                    btnSave.Visible = False
                    btnSubmit.Visible = False
                    dtgClassRegistration.CurrentPageIndex = 0
                    Rebind()
                End If
            End If
        End If

    End Sub

    Private Sub BindDDLTahunFiskal(ByVal ddl As DropDownList)
        Dim GetTahun As Integer = DateTime.Now.Year
        ddl.ClearSelection()
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("Semua", "-1"))
        'Before
        For x As Integer = 10 To 0 Step -1
            Dim value1 As String = (GetTahun - x).ToString()
            Dim value2 As String = (GetTahun - x - 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value2, value1)
            ddl.Items.Add(New ListItem(value, value))
        Next
        'After
        For x As Integer = 0 To 10
            Dim value1 As String = (GetTahun + x).ToString()
            Dim value2 As String = (GetTahun + x + 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value1, value2)
            ddl.Items.Add(New ListItem(value, value))
        Next
        ddl.SelectedValue = String.Format("{0}/{1}", GetTahun.ToString(), (GetTahun + 1).ToString())
    End Sub

    Private Sub ActivateUserPrivilege()
        btnSave.Visible = helpers.IsEdit
        btnSubmit.Visible = helpers.IsEdit
        btnCancel.Visible = helpers.IsEdit
    End Sub

    Private Sub AssignAttributeControl()

        Dim lblPopUp As Label = CType(Page.FindControl("lblPopUpDealer"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection")
        lblPopUpClass.Attributes("onclick") = String.Format("ShowPPClassSelectionMany('{0}')", AreaId)
        lblCerConfig.Attributes("onclick") = "ShowPPCertificate()"
    End Sub


    Private Sub InitiatePage()
        AssignAttributeControl()
        ViewState("CurrentSortColumn") = "Rank"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        btnCari.Enabled = True 'False
        btnCetak.Disabled = True 'False
        btnSave.Enabled = False
        btnSubmit.Enabled = False
        btnDownLoad.Enabled = False
        btnBack.Enabled = True
        lblKodeKelas.Visible = False
        lblNamaKelas.Visible = False
        lblMulai.Visible = False
        lblSelesai.Visible = False
        trSertifikat.Visible = False


    End Sub

    Private Function ConvertKodeDealer(ByVal sKodeDealerColl As String)
        Dim sKodeDealerTemp() As String = sKodeDealerColl.Split(New Char() {";"})
        Dim sKodeDealer As String = ""
        For i As Integer = 0 To sKodeDealerTemp.Length - 1
            sKodeDealer = sKodeDealer & "'" & sKodeDealerTemp(i).Trim() & "'"

            If Not (i = sKodeDealerTemp.Length - 1) Then
                sKodeDealer = sKodeDealer & ","
            End If
        Next
        sKodeDealer = "(" & sKodeDealer & ")"
        Return sKodeDealer
    End Function

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click

        dtgClassRegistration.CurrentPageIndex = 0
        Rebind()
    End Sub

    'Private Sub Rebind()
    '    Dim bRank As Boolean = True
    '    Dim bIsCertificateLineFound As Boolean = False
    '    If ddlClass.SelectedValue <> "0" Then
    '        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '        If (ddlClass.Items.Count > 0) AndAlso (CType(ddlClass.SelectedValue, Integer) > 0) Then
    '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ID", MatchType.Exact, CType(ddlClass.SelectedValue, Short)))
    '        Else
    '            Dim startDate As Date
    '            Dim finishDate As Date

    '            If txtYear.Text.Length = 4 Then
    '                startDate = New DateTime(txtYear.Text.ToString(), 12, Date.DaysInMonth(txtYear.Text.ToString(), 12), 23, 59, 59)
    '                finishDate = New DateTime(txtYear.Text.ToString(), 1, 1, 0, 0, 0)

    '                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.StartDate", MatchType.LesserOrEqual, startDate))
    '                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.FinishDate", MatchType.GreaterOrEqual, finishDate))
    '            End If
    '        End If

    '        If Not Request.QueryString("Rank") Is Nothing Then
    '            Dim dummyParam As String() = CType(Request.QueryString("Rank"), String).Trim().Split(";")
    '            If dummyParam.Length > 0 Then
    '                If Not dummyParam(0) = String.Empty Then
    '                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.Dealer.ID", MatchType.Exact, CType(dummyParam(0), Integer)))
    '                End If
    '                If Not dummyParam(4) = String.Empty Then
    '                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.ID", MatchType.Exact, CType(dummyParam(4), Integer)))
    '                End If
    '            End If
    '        Else
    '            objDealer = Session("DEALER")
    '            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
    '                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
    '            End If
    '            If Not (txtDealerSelection.Text.Trim() = "") Then
    '                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtDealerSelection.Text.Trim())))
    '            End If
    '            If Not (txtNoReg.Text.Trim() = "") Then
    '                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.ID", MatchType.Exact, txtNoReg.Text.Trim))
    '            End If
    '        End If

    '        Dim arryList As ArrayList = New ArrayList


    '        Dim sortCol As SortCollection = New SortCollection
    '        sortCol.Add(New Sort(GetType(TrClassRegistration), CType(ViewState("CurrentSortColumn"), String), _
    '                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
    '        arryList = New TrClassRegistrationFacade(User).Retrieve(criterias, sortCol)
    '        _sessHelper.SetSession("sessCriteria", criterias)
    '        _sessHelper.SetSession("sessForDownLoad_dtgClassRegistration", arryList)
    '        If ssobjClass.ID > 0 Then
    '            _sessHelper.SetSession("sessForDownLoad_ClassCode", ssobjClass.ClassCode)
    '            _sessHelper.SetSession("sessForDownLoad_ClassName", ssobjClass.ClassName)
    '            _sessHelper.SetSession("sessForDownLoad_Start", ssobjClass.StartDate)
    '            _sessHelper.SetSession("sessForDownLoad_Finish", ssobjClass.FinishDate)


    '        Else
    '            _sessHelper.SetSession("sessForDownLoad_ClassCode", lblClassCode.Text)
    '            _sessHelper.SetSession("sessForDownLoad_ClassName", lblClassName.Text)
    '            _sessHelper.SetSession("sessForDownLoad_Start", lblStart.Text)
    '            _sessHelper.SetSession("sessForDownLoad_Finish", lblFinish.Text)


    '        End If
    '        'blok ini memeriksa apakah sudah ada sebuah nilai, kalo belum ada sama sekali tidak dapat tampil maupun hitung
    '        If arryList.Count > 0 Then
    '            For Each objClassReg As TrClassRegistration In arryList
    '                Dim critCertificateLine As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                critCertificateLine.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.ID", MatchType.Exact, objClassReg.ID))
    '                Dim CertificateLineCol As ArrayList = New TrCertificateLineFacade(User).Retrieve(critCertificateLine)
    '                If CertificateLineCol.Count > 0 Then
    '                    'ada 
    '                    bIsCertificateLineFound = True
    '                    Exit For
    '                End If
    '            Next
    '        Else
    '            MessageBox.Show("Tidak ada siswa yang terdaftar ")
    '            Return

    '        End If

    '        If bIsCertificateLineFound Then
    '            If arryList.Count > 0 Then
    '                dtgClassRegistration.DataSource = arryList
    '                ViewState("NeedReCalculate") = False
    '                lblKodeKelas.Visible = True
    '                lblNamaKelas.Visible = True
    '                lblMulai.Visible = True
    '                lblSelesai.Visible = True
    '                dtgClassRegistration.DataBind()
    '                btnCetak.Disabled = False 'True
    '                btnSave.Enabled = True
    '                btnDownLoad.Enabled = True
    '            Else
    '                MessageBox.Show("Data tidak ditemukan ")
    '                dtgClassRegistration.DataSource = Nothing
    '                dtgClassRegistration.DataBind()
    '                btnCetak.Disabled = True 'False
    '                btnSave.Enabled = False
    '                btnDownLoad.Enabled = False
    '            End If
    '            _sessHelper.SetSession("Sess_ddlCategory", ddlCategory.SelectedValue)
    '            _sessHelper.SetSession("Sess_ddlClass", ddlClass.SelectedValue)
    '            _sessHelper.SetSession("sessClassRegistration", arryList)

    '        Else
    '            MessageBox.Show("Belum ada nilai yang dimasukan ")
    '            dtgClassRegistration.DataSource = Nothing
    '            dtgClassRegistration.DataBind()
    '            btnCetak.Disabled = True 'False
    '            btnSave.Enabled = False
    '            btnDownLoad.Enabled = False
    '        End If
    '    Else
    '        MessageBox.Show("Kelas belum dipilih ")
    '    End If
    'End Sub

    Private Sub Rebind()
        Dim bRank As Boolean = True
        Dim bIsCertificateLineFound As Boolean = False
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If AreaId = 3 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Status", MatchType.InSet, "(1,2)"))
        End If

        If Not String.IsNullorEmpty(ddlFiscalYear.SelectedValue) Then

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.FiscalYear", MatchType.Exact, ddlFiscalYear.SelectedValue))
        End If

        If txtKodeKelas.IsNotEmpty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ClassCode", MatchType.Exact, txtKodeKelas.Text))

        Else
            MessageBox.Show("Tentukan Kelas terlebih dahulu")
            Exit Sub
        End If

        If Not Request.QueryString("Rank") Is Nothing Then
            Dim dummyParam As String() = CType(Request.QueryString("Rank"), String).Trim().Split(";")
            If dummyParam.Length > 0 Then
                If Not dummyParam(0) = String.Empty Then
                    'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.Dealer.ID", MatchType.Exact, CType(dummyParam(0), Integer)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Dealer.ID", MatchType.Exact, CType(dummyParam(0), Integer)))
                End If
                If Not dummyParam(4) = String.Empty Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.ID", MatchType.Exact, CType(dummyParam(4), Integer)))
                End If
            End If
        Else
            objDealer = Session("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            End If
            If Not (txtDealerSelection.Text.Trim() = "") Then
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtDealerSelection.Text.Trim())))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtDealerSelection.Text.Trim())))
            End If
            If txtNoReg.IsNotEmpty Then
                If AreaId.IsNullorEmpty Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.ID", MatchType.Exact, txtNoReg.Text.Trim))
                Else
                    If AreaId.Equals("1") Or AreaId.Equals("3") Then
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.TrTraineeSalesmanHeader.SalesmanHeader.SalesmanCode", MatchType.Exact, txtNoReg.Text.Trim))
                    Else
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.ID", MatchType.Exact, txtNoReg.Text.Trim))
                    End If
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.TrTraineeSalesmanHeader.JobPositionAreaID", MatchType.Exact, AreaId))

                End If
            End If
        End If

        Dim arryList As ArrayList = New ArrayList
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(TrClassRegistration), CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
        arryList = New TrClassRegistrationFacade(User).Retrieve(criterias, sortCol)
        _sessHelper.SetSession("sessCriteria", criterias)
        _sessHelper.SetSession("sessForDownLoad_dtgClassRegistration", arryList)
        If ssobjClass.ID > 0 Then
            _sessHelper.SetSession("sessForDownLoad_ClassCode", ssobjClass.ClassCode)
            _sessHelper.SetSession("sessForDownLoad_ClassName", ssobjClass.ClassName)
            _sessHelper.SetSession("sessForDownLoad_Start", ssobjClass.StartDate)
            _sessHelper.SetSession("sessForDownLoad_Finish", ssobjClass.FinishDate)


        Else
            _sessHelper.SetSession("sessForDownLoad_ClassCode", lblClassCode.Text)
            _sessHelper.SetSession("sessForDownLoad_ClassName", lblClassName.Text)
            _sessHelper.SetSession("sessForDownLoad_Start", lblStart.Text)
            _sessHelper.SetSession("sessForDownLoad_Finish", lblFinish.Text)


        End If
        'blok ini memeriksa apakah sudah ada sebuah nilai, kalo belum ada sama sekali tidak dapat tampil maupun hitung
        If arryList.Count > 0 Then
            For Each objClassReg As TrClassRegistration In arryList
                Dim critCertificateLine As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critCertificateLine.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.ID", MatchType.Exact, objClassReg.ID))
                Dim CertificateLineCol As ArrayList = New TrCertificateLineFacade(User).Retrieve(critCertificateLine)
                If CertificateLineCol.Count > 0 Then
                    'ada 
                    bIsCertificateLineFound = True
                    Exit For
                End If
            Next
        Else
            MessageBox.Show("Tidak ada siswa yang terdaftar ")
            Return

        End If

        If bIsCertificateLineFound Then
            If arryList.Count > 0 Then
                dtgClassRegistration.DataSource = arryList
                ViewState("NeedReCalculate") = False
                lblKodeKelas.Visible = True
                lblNamaKelas.Visible = True
                lblMulai.Visible = True
                lblSelesai.Visible = True
                dtgClassRegistration.DataBind()
                btnCetak.Disabled = False 'True

                Dim oClassReg As TrClassRegistration = CType(arryList(0), TrClassRegistration)

                If oClassReg.TrClass.SubmitStatus = 1 Then
                    btnSubmit.Enabled = False
                    btnSave.Enabled = False
                    btnCancel.Enabled = True
                Else
                    btnSubmit.Enabled = True
                    btnSave.Enabled = True
                    btnCancel.Enabled = False
                End If

                btnDownLoad.Enabled = True
            Else
                MessageBox.Show("Data tidak ditemukan ")
                dtgClassRegistration.DataSource = Nothing
                dtgClassRegistration.DataBind()
                btnCetak.Disabled = True 'False
                btnSave.Enabled = False
                btnSubmit.Enabled = False
                btnDownLoad.Enabled = False
            End If
            '_sessHelper.SetSession("Sess_ddlCategory", ddlCategory.SelectedValue)
            '_sessHelper.SetSession("Sess_ddlClass", ddlClass.SelectedValue)
            _sessHelper.SetSession("Sess_ddlCategory", txtKodeKategori.Text)
            _sessHelper.SetSession("Sess_ddlClass", txtKodeKelas.Text)
            _sessHelper.SetSession("sessClassRegistration", arryList)

        Else
            MessageBox.Show("Belum ada nilai yang dimasukan ")
            dtgClassRegistration.DataSource = Nothing
            dtgClassRegistration.DataBind()
            btnCetak.Disabled = True 'False
            btnSave.Enabled = False
            btnSubmit.Enabled = False
            btnDownLoad.Enabled = False
            Return
        End If

        Dim dataKelas As TrClass = New TrClassFacade(User).Retrieve(txtKodeKelas.Text)
        Dim dataSertifikat As New TrCertificateConfig
        Dim critsCfg As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCertificateConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critsCfg.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateConfig), "Status", MatchType.Exact, 1))

        Dim arrSertifikat As ArrayList = New TrCertificateConfigFacade(User).Retrieve(critsCfg)
        If arrSertifikat.IsItems Then
            dataSertifikat = CType(arrSertifikat(0), TrCertificateConfig)
            txtNamaPenandatangan.Text = dataSertifikat.NamaTTD
            txtJabatanPenandatangan.Text = dataSertifikat.JabatanTTD
            hdnCerID.Value = dataSertifikat.ID.ToString()
        End If

        If Not dataKelas.ID.Equals(0) Then
            lblClassCode.Text = dataKelas.ClassCode
            lblClassName.Text = dataKelas.ClassName
            lblFinish.Text = dataKelas.FinishDate.DateToString
            lblStart.Text = dataKelas.StartDate.DateToString
            If AreaId.Equals("2") Then
                trSertifikat.Visible = True
                txtNamaPenandatangan.Disabled()
                txtJabatanPenandatangan.Disabled()
                If dataKelas.SubmitStatus = 1 Then
                    lblCerConfig.Visible = False
                    If Not IsNothing(dataKelas.TrCertificateConfig) Then
                        txtNamaPenandatangan.Text = dataKelas.TrCertificateConfig.NamaTTD
                        txtJabatanPenandatangan.Text = dataKelas.TrCertificateConfig.JabatanTTD
                        hdnCerID.Value = dataKelas.TrCertificateConfig.ID.ToString()
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub Calculation()
        Dim bIsCertificateLineFound As Boolean = False
        Dim WorkingClassRegColl As ArrayList = New ArrayList
        WorkingClassRegColl = CalcutateAverage()
        WorkingClassRegColl = RankClassRegistration(WorkingClassRegColl)

        If WorkingClassRegColl.Count > 0 Then
            For Each objClassReg As TrClassRegistration In WorkingClassRegColl
                Dim critCertificateLine As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critCertificateLine.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.ID", MatchType.Exact, objClassReg.ID))
                Dim CertificateLineCol As ArrayList = New TrCertificateLineFacade(User).Retrieve(critCertificateLine)
                If CertificateLineCol.Count > 0 Then
                    'ada 
                    bIsCertificateLineFound = True
                    Exit For
                End If
            Next
        Else
            MessageBox.Show("Tidak ada siswa yang terdaftar ")
            Return
        End If

        If bIsCertificateLineFound Then
            If WorkingClassRegColl.Count > 0 Then
                dtgClassRegistration.DataSource = WorkingClassRegColl
                ViewState("NeedReCalculate") = True
                lblKodeKelas.Visible = True
                lblNamaKelas.Visible = True
                lblMulai.Visible = True
                lblSelesai.Visible = True
                dtgClassRegistration.DataBind()
                btnCetak.Disabled = False 'True

                Dim oClassReg As TrClassRegistration = CType(WorkingClassRegColl(0), TrClassRegistration)
                If oClassReg.TrClass.SubmitStatus = 1 Then
                    btnSubmit.Enabled = False
                    btnSave.Enabled = False
                Else
                    btnSubmit.Enabled = True
                    btnSave.Enabled = True
                End If

                btnDownLoad.Enabled = True
            Else
                MessageBox.Show("Data tidak ditemukan ")
                dtgClassRegistration.DataSource = Nothing
                dtgClassRegistration.DataBind()
                btnCetak.Disabled = False 'False
                btnSave.Enabled = True
                btnSubmit.Enabled = False
                btnDownLoad.Enabled = False
            End If
            '_sessHelper.SetSession("Sess_ddlCategory", ddlCategory.SelectedValue)
            '_sessHelper.SetSession("Sess_ddlClass", ddlClass.SelectedValue)
            _sessHelper.SetSession("Sess_ddlCategory", txtKodeKategori.Text)
            _sessHelper.SetSession("Sess_ddlClass", txtKodeKelas.Text)
            _sessHelper.SetSession("sessClassRegistration", WorkingClassRegColl)

        Else
            MessageBox.Show("Belum ada nilai yang dimasukan ")
            dtgClassRegistration.DataSource = Nothing
            dtgClassRegistration.DataBind()
            btnCetak.Disabled = False 'False
            btnSave.Enabled = False
            btnSubmit.Enabled = False
            btnDownLoad.Enabled = False
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not IsPageValid() Then
            Return
        End If
        Dim WorkingClassRegColl As ArrayList = New ArrayList
        WorkingClassRegColl = CalcutateAverage()
        WorkingClassRegColl = RankClassRegistration(WorkingClassRegColl)
        Dim nResult As Integer = UpdateCollectionTrClassRegistration(WorkingClassRegColl)
        If nResult = 0 Then
            'MessageBox.Show("Perhitungan rangking dan update ke database sukses !")
            'ViewState("CurrentSortColumn") = "Rank"
            'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Rebind()
            'BindDatagrid(0)
        Else
            MessageBox.Show(SR.UpdateFail)
        End If


        If ssobjClass.ID > 0 Then
            _sessHelper.SetSession("sessForDownLoad_ClassCode", ssobjClass.ClassCode)
            _sessHelper.SetSession("sessForDownLoad_ClassName", ssobjClass.ClassName)
            _sessHelper.SetSession("sessForDownLoad_Start", ssobjClass.StartDate)
            _sessHelper.SetSession("sessForDownLoad_Finish", ssobjClass.FinishDate)
        Else
            _sessHelper.SetSession("sessForDownLoad_ClassCode", lblClassCode.Text)
            _sessHelper.SetSession("sessForDownLoad_ClassName", lblClassName.Text)
            _sessHelper.SetSession("sessForDownLoad_Start", lblStart.Text)
            _sessHelper.SetSession("sessForDownLoad_Finish", lblFinish.Text)
        End If
        _sessHelper.SetSession("sessForDownLoad_dtgClassRegistration", WorkingClassRegColl)

    End Sub

    Private Function IsPageValid() As Boolean
        'If ddlClass.SelectedValue = String.Empty Then
        '    Return False
        'End If
        Return True
    End Function

    'Private Function CalcutateAverage() As ArrayList
    '    If ddlClass.SelectedValue <> "0" Then
    '        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ID", MatchType.Exact, CType(ddlClass.SelectedValue, Short)))
    '        Dim totalRow As Integer = 0
    '        Dim indexPage As Integer = 0
    '        Dim arryList As ArrayList = New ArrayList
    '        Dim PassingScore As Decimal = 70 'inisial 

    '        'ambil semuanya siswa aktif dalam kelas ybs
    '        arryList = New TrClassRegistrationFacade(User).Retrieve(criterias)
    '        If arryList.Count > 0 Then
    '            PassingScore = CType(arryList(0), TrClassRegistration).TrClass.TrCourse.PassingScore
    '            'masing-masing itung average nya
    '            For Each objTrClassRegistration As TrClassRegistration In arryList
    '                'ambil CourseEval-nya dulu untuk menentukan jumlah test
    '                Dim critCourseEval As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, objTrClassRegistration.TrClass.TrCourse.ID))
    '                critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "Type", MatchType.Exact, CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)))
    '                'Ambil courseevaluation yang dipilih pada trnumclassevaluation
    '                critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "ID", MatchType.InSet, ("(select CourseEvaluationID from trclassnumevaluation where ClassID=" & objTrClassRegistration.TrClass.ID & ")")))

    '                Dim TrCourseEvalColl As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(critCourseEval)
    '                If TrCourseEvalColl.Count > 0 Then
    '                    _sessHelper.SetSession("JumlahTest", TrCourseEvalColl.Count - 2)
    '                End If

    '                'ambil masing-masing nilai test dengan tipe angka dari tiap peserta
    '                Dim critCertificateline As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                critCertificateline.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.ID", MatchType.Exact, objTrClassRegistration.ID))
    '                critCertificateline.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrCourseEvaluation.Type", MatchType.Exact, CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)))
    '                Dim TrCertificateLineColl As ArrayList = New TrCertificateLineFacade(User).Retrieve(critCertificateline)
    '                If TrCertificateLineColl.Count > 0 Then
    '                    For Each obj As TrCertificateLine In TrCertificateLineColl
    '                        If Right(obj.TrCourseEvaluation.EvaluationCode, 2) <> "99" Or Right(obj.TrCourseEvaluation.EvaluationCode, 2) <> "00" Then
    '                            If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "01" Then
    '                                objTrClassRegistration.Test1 = obj.NumTestResult
    '                            End If
    '                            If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "02" Then
    '                                objTrClassRegistration.Test2 = obj.NumTestResult
    '                            End If
    '                            If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "03" Then
    '                                objTrClassRegistration.Test3 = obj.NumTestResult
    '                            End If
    '                            If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "04" Then
    '                                objTrClassRegistration.Test4 = obj.NumTestResult
    '                            End If
    '                            If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "05" Then
    '                                objTrClassRegistration.Test5 = obj.NumTestResult
    '                            End If
    '                            If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "06" Then
    '                                objTrClassRegistration.Test6 = obj.NumTestResult
    '                            End If
    '                            If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "07" Then
    '                                objTrClassRegistration.Test7 = obj.NumTestResult
    '                            End If
    '                            If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "08" Then
    '                                objTrClassRegistration.Test8 = obj.NumTestResult
    '                            End If
    '                            If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "09" Then
    '                                objTrClassRegistration.Test9 = obj.NumTestResult
    '                            End If
    '                            If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "10" Then
    '                                objTrClassRegistration.Test10 = obj.NumTestResult
    '                            End If
    '                        End If
    '                    Next
    '                End If

    '                If TrCourseEvalColl.Count > 0 Then
    '                    Dim Total As Decimal = 0
    '                    Dim nJumlah As Integer = 0

    '                    For j As Integer = 0 To TrCourseEvalColl.Count - 1 'iterasi untuk hitung total
    '                        Dim objTrCourseEval As TrCourseEvaluation = CType(TrCourseEvalColl(j), TrCourseEvaluation)

    '                        If TrCertificateLineColl.Count > 0 Then
    '                            Dim bCertificateLineFound As Boolean = False
    '                            Dim objTrCertificateLine As TrCertificateLine
    '                            For i As Integer = 0 To TrCertificateLineColl.Count - 1
    '                                If TrCertificateLineColl(i).TrCourseEvaluation.EvaluationCode = objTrCourseEval.EvaluationCode Then
    '                                    bCertificateLineFound = True
    '                                    objTrCertificateLine = TrCertificateLineColl(i)
    '                                    Exit For
    '                                End If
    '                            Next

    '                            If bCertificateLineFound Then
    '                                'If objTrCourseEval.ID = objTrCertificateLine.TrCourseEvaluation.ID Then
    '                                ' yang jenis ujiannya angka saja (non sikap) dan bukan test awal
    '                                If (objTrCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) And _
    '                                   (Right(objTrCertificateLine.TrCourseEvaluation.EvaluationCode, 2) <> "00")) Then
    '                                    Total += objTrCertificateLine.NumTestResult
    '                                    If Right(objTrCertificateLine.TrCourseEvaluation.EvaluationCode, 2) = "99" Then
    '                                        objTrClassRegistration.FinalTest = objTrCertificateLine.NumTestResult
    '                                    End If
    '                                ElseIf objTrCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) And _
    '                                       Right(objTrCertificateLine.TrCourseEvaluation.EvaluationCode, 2) = "00" Then
    '                                    'ingat, test awal tidak diikutkan
    '                                    objTrClassRegistration.InitialTest = objTrCertificateLine.NumTestResult
    '                                End If
    '                                'End If
    '                            End If
    '                        End If

    '                        If objTrCourseEval.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) And _
    '                           Right(objTrCourseEval.EvaluationCode, 2) <> "00" Then
    '                            nJumlah += 1
    '                        End If
    '                    Next
    '                    'TODO: M1
    '                    If nJumlah > 0 Then
    '                        objTrClassRegistration.Avarage = Decimal.Round((Total / nJumlah), 3)
    '                        If (objTrClassRegistration.TrClass.TrCourse.CourseCode.IndexOf("M1") >= 0 OrElse objTrClassRegistration.TrClass.TrCourse.CourseCode.IndexOf("M2") >= 0 OrElse objTrClassRegistration.TrClass.TrCourse.CourseCode.IndexOf("M3") >= 0) Then
    '                            If (objTrClassRegistration.FinalTest >= PassingScore AndAlso objTrClassRegistration.Avarage >= PassingScore) Then
    '                                objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Pass
    '                            Else
    '                                objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Fail
    '                            End If
    '                        Else

    '                            If objTrClassRegistration.Avarage >= PassingScore Then
    '                                objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Pass
    '                            Else
    '                                objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Fail
    '                            End If
    '                        End If

    '                    End If

    '                End If
    '                objTrClassRegistration.MarkLoaded()
    '            Next

    '        End If
    '        _sessHelper.SetSession("Sess_ddlCategory", ddlCategory.SelectedValue)
    '        _sessHelper.SetSession("Sess_ddlClass", ddlClass.SelectedValue)
    '        _sessHelper.SetSession("sessClassRegistrationAverage", arryList)
    '        Return arryList
    '    Else
    '        MessageBox.Show("Kelas belum dipilih ")
    '    End If
    'End Function

    Private Function CalcutateAverage() As ArrayList
        If txtKodeKelas.Text <> "" Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ClassCode", MatchType.Exact, txtKodeKelas.Text.Trim))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Status", MatchType.NotInSet, "'3','4','5'"))
            Dim totalRow As Integer = 0
            Dim indexPage As Integer = 0
            Dim arryList As ArrayList = New ArrayList
            Dim PassingScore As Decimal = 70 'inisial 
            'ambil semuanya siswa aktif dalam kelas ybs
            arryList = New TrClassRegistrationFacade(User).Retrieve(criterias)
            If arryList.Count > 0 Then
                PassingScore = CType(arryList(0), TrClassRegistration).TrClass.TrCourse.PassingScore
                'masing-masing itung average nya
                For Each objTrClassRegistration As TrClassRegistration In arryList
                    'ambil CourseEval-nya dulu untuk menentukan jumlah test
                    Dim critCourseEval As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, objTrClassRegistration.TrClass.TrCourse.ID))
                    critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "Type", MatchType.Exact, CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)))
                    'Ambil courseevaluation yang dipilih pada trnumclassevaluation
                    critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "ID", MatchType.InSet, ("(select CourseEvaluationID from trclassnumevaluation where ClassID=" & objTrClassRegistration.TrClass.ID & ")")))

                    Dim TrCourseEvalColl As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(critCourseEval)
                    If TrCourseEvalColl.Count > 0 Then
                        _sessHelper.SetSession("JumlahTest", TrCourseEvalColl.Count - 2)
                    End If

                    'ambil masing-masing nilai test dengan tipe angka dari tiap peserta
                    Dim critCertificateline As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critCertificateline.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.ID", MatchType.Exact, objTrClassRegistration.ID))
                    critCertificateline.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrCourseEvaluation.Type", MatchType.Exact, CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)))
                    Dim TrCertificateLineColl As ArrayList = New TrCertificateLineFacade(User).Retrieve(critCertificateline)
                    If TrCertificateLineColl.Count > 0 Then
                        For Each obj As TrCertificateLine In TrCertificateLineColl
                            If Right(obj.TrCourseEvaluation.EvaluationCode, 2) <> "99" Or Right(obj.TrCourseEvaluation.EvaluationCode, 2) <> "00" Then
                                If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "01" Then
                                    objTrClassRegistration.Test1 = obj.NumTestResult
                                End If
                                If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "02" Then
                                    objTrClassRegistration.Test2 = obj.NumTestResult
                                End If
                                If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "03" Then
                                    objTrClassRegistration.Test3 = obj.NumTestResult
                                End If
                                If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "04" Then
                                    objTrClassRegistration.Test4 = obj.NumTestResult
                                End If
                                If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "05" Then
                                    objTrClassRegistration.Test5 = obj.NumTestResult
                                End If
                                If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "06" Then
                                    objTrClassRegistration.Test6 = obj.NumTestResult
                                End If
                                If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "07" Then
                                    objTrClassRegistration.Test7 = obj.NumTestResult
                                End If
                                If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "08" Then
                                    objTrClassRegistration.Test8 = obj.NumTestResult
                                End If
                                If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "09" Then
                                    objTrClassRegistration.Test9 = obj.NumTestResult
                                End If
                                If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "10" Then
                                    objTrClassRegistration.Test10 = obj.NumTestResult
                                End If
                            End If
                        Next
                    End If

                    If TrCourseEvalColl.Count > 0 Then
                        Dim Total As Decimal = 0
                        Dim nJumlah As Integer = 0

                        For j As Integer = 0 To TrCourseEvalColl.Count - 1 'iterasi untuk hitung total
                            Dim objTrCourseEval As TrCourseEvaluation = CType(TrCourseEvalColl(j), TrCourseEvaluation)

                            If TrCertificateLineColl.Count > 0 Then
                                Dim bCertificateLineFound As Boolean = False
                                Dim objTrCertificateLine As TrCertificateLine
                                For i As Integer = 0 To TrCertificateLineColl.Count - 1
                                    If TrCertificateLineColl(i).TrCourseEvaluation.EvaluationCode = objTrCourseEval.EvaluationCode Then
                                        bCertificateLineFound = True
                                        objTrCertificateLine = TrCertificateLineColl(i)
                                        Exit For
                                    End If
                                Next

                                If bCertificateLineFound Then
                                    'If objTrCourseEval.ID = objTrCertificateLine.TrCourseEvaluation.ID Then
                                    ' yang jenis ujiannya angka saja (non sikap) dan bukan test awal
                                    If (objTrCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) And _
                                       (Right(objTrCertificateLine.TrCourseEvaluation.EvaluationCode, 2) <> "00")) Then
                                        Total += objTrCertificateLine.NumTestResult
                                        If Right(objTrCertificateLine.TrCourseEvaluation.EvaluationCode, 2) = "99" Then
                                            objTrClassRegistration.FinalTest = objTrCertificateLine.NumTestResult
                                        End If
                                    ElseIf objTrCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) And _
                                           Right(objTrCertificateLine.TrCourseEvaluation.EvaluationCode, 2) = "00" Then
                                        'ingat, test awal tidak diikutkan
                                        objTrClassRegistration.InitialTest = objTrCertificateLine.NumTestResult
                                    End If
                                    'End If
                                End If
                            End If

                            If objTrCourseEval.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) And _
                               Right(objTrCourseEval.EvaluationCode, 2) <> "00" Then
                                nJumlah += 1
                            End If
                        Next
                        'TODO: M1
                        If nJumlah > 0 And objTrClassRegistration.IsBilliing Then
                            objTrClassRegistration.Avarage = Decimal.Round((Total / nJumlah), 3)
                            If (objTrClassRegistration.TrClass.TrCourse.CourseCode.IndexOf("M1") >= 0 OrElse objTrClassRegistration.TrClass.TrCourse.CourseCode.IndexOf("M2") >= 0 OrElse objTrClassRegistration.TrClass.TrCourse.CourseCode.IndexOf("M3") >= 0) Then
                                If Not objTrClassRegistration.IsManualCheck Then
                                    If (objTrClassRegistration.FinalTest >= PassingScore AndAlso objTrClassRegistration.Avarage >= PassingScore) Then
                                        objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Pass
                                    Else
                                        objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Fail
                                    End If
                                Else
                                    objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Pass
                                End If
                            Else
                                If Not objTrClassRegistration.IsManualCheck Then
                                    If objTrClassRegistration.Avarage >= PassingScore Then
                                        objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Pass
                                    Else
                                        objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Fail
                                    End If
                                Else
                                    objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Pass

                                End If
                            End If

                        End If

                    End If
                    objTrClassRegistration.MarkLoaded()
                Next

            End If
            '_sessHelper.SetSession("Sess_ddlCategory", ddlCategory.SelectedValue)
            '_sessHelper.SetSession("Sess_ddlClass", ddlClass.SelectedValue)
            _sessHelper.SetSession("Sess_ddlCategory", txtKodeKategori.Text)
            _sessHelper.SetSession("Sess_ddlClass", txtKodeKelas.Text)
            _sessHelper.SetSession("sessClassRegistrationAverage", arryList)
            Return arryList
        Else
            MessageBox.Show("Kelas belum dipilih ")
        End If
    End Function


    Private Sub ChangePosition(ByVal j As Integer, ByVal k As Integer, ByRef arrClassReg As ArrayList)
        Dim objClassRegTmp As TrClassRegistration
        'objClassRegTmp = arrClassReg(j)
        'arrClassReg(j) = arrClassReg(k)
        'arrClassReg(k) = objClassRegTmp

        objClassRegTmp = arrClassReg(k)
        arrClassReg(k) = arrClassReg(j)
        arrClassReg(j) = objClassRegTmp

    End Sub

    Private Sub ChangePositionCouseOfID(ByVal j As Integer, ByVal k As Integer, ByRef arrClassReg As ArrayList)
        If arrClassReg(j).ID > arrClassReg(k).ID Then
            ChangePosition(j, k, arrClassReg)
        End If
    End Sub

    Private Sub ChangePositionLevel7(ByVal j As Integer, ByVal k As Integer, ByRef arrClassReg As ArrayList)
        If arrClassReg(j).Test7 < arrClassReg(k).Test7 Then
            ChangePosition(j, k, arrClassReg)
        ElseIf arrClassReg(j).Test7 = arrClassReg(k).Test7 Then
            ChangePositionCouseOfID(j, k, arrClassReg)
        End If
    End Sub
    Private Sub ChangePositionLevel6(ByVal JmlTest As Integer, ByVal j As Integer, ByVal k As Integer, ByRef arrClassReg As ArrayList)
        If JmlTest = 6 Then
            If arrClassReg(j).Test6 < arrClassReg(k).Test6 Then
                ChangePosition(j, k, arrClassReg)
            ElseIf arrClassReg(j).Test6 = arrClassReg(k).Test6 Then
                ChangePositionCouseOfID(j, k, arrClassReg)
            End If
        Else
            If arrClassReg(j).Test6 < arrClassReg(k).Test6 Then
                ChangePosition(j, k, arrClassReg)
            ElseIf arrClassReg(j).Test6 = arrClassReg(k).Test6 Then
                ChangePositionLevel7(j, k, arrClassReg)
            End If
        End If
    End Sub

    Private Sub ChangePositionLevel5(ByVal JmlTest As Integer, ByVal j As Integer, ByVal k As Integer, ByRef arrClassReg As ArrayList)
        If JmlTest = 5 Then
            If arrClassReg(j).Test5 < arrClassReg(k).Test5 Then
                ChangePosition(j, k, arrClassReg)
            ElseIf arrClassReg(j).Test5 = arrClassReg(k).Test5 Then
                ChangePositionCouseOfID(j, k, arrClassReg)
            End If
        Else
            If arrClassReg(j).Test5 < arrClassReg(k).Test5 Then
                ChangePosition(j, k, arrClassReg)
            ElseIf arrClassReg(j).Test5 = arrClassReg(k).Test5 Then
                ChangePositionLevel6(JmlTest, j, k, arrClassReg)
            End If
        End If
    End Sub

    Private Sub ChangePositionLevel4(ByVal JmlTest As Integer, ByVal j As Integer, ByVal k As Integer, ByRef arrClassReg As ArrayList)
        If JmlTest = 4 Then
            If arrClassReg(j).Test4 < arrClassReg(k).Test4 Then
                ChangePosition(j, k, arrClassReg)
            ElseIf arrClassReg(j).Test4 = arrClassReg(k).Test4 Then
                ChangePositionCouseOfID(j, k, arrClassReg)
            End If
        Else
            If arrClassReg(j).Test4 < arrClassReg(k).Test4 Then
                ChangePosition(j, k, arrClassReg)
            ElseIf arrClassReg(j).Test4 = arrClassReg(k).Test4 Then
                ChangePositionLevel5(JmlTest, j, k, arrClassReg)
            End If
        End If
    End Sub
    Private Sub ChangePositionLevel3(ByVal JmlTest As Integer, ByVal j As Integer, ByVal k As Integer, ByRef arrClassReg As ArrayList)
        If JmlTest = 3 Then
            If arrClassReg(j).Test3 < arrClassReg(k).Test3 Then
                ChangePosition(j, k, arrClassReg)
            ElseIf arrClassReg(j).Test3 = arrClassReg(k).Test3 Then
                ChangePositionCouseOfID(j, k, arrClassReg)
            End If
        Else
            If arrClassReg(j).Test3 < arrClassReg(k).Test3 Then
                ChangePosition(j, k, arrClassReg)
            ElseIf arrClassReg(j).Test3 = arrClassReg(k).Test3 Then
                ChangePositionLevel4(JmlTest, j, k, arrClassReg)
            End If
        End If
    End Sub
    Private Sub ChangePositionLevel2(ByVal JmlTest As Integer, ByVal j As Integer, ByVal k As Integer, ByRef arrClassReg As ArrayList)
        If JmlTest = 2 Then
            If arrClassReg(j).Test2 < arrClassReg(k).Test2 Then
                ChangePosition(j, k, arrClassReg)
            ElseIf arrClassReg(j).Test2 = arrClassReg(k).Test2 Then
                ChangePositionCouseOfID(j, k, arrClassReg)
            End If
        Else
            If arrClassReg(j).Test2 < arrClassReg(k).Test2 Then
                ChangePosition(j, k, arrClassReg)
            ElseIf arrClassReg(j).Test2 = arrClassReg(k).Test2 Then
                ChangePositionLevel3(JmlTest, j, k, arrClassReg)
            End If
        End If
    End Sub

    Private Sub ChangePositionLevel1(ByVal JmlTest As Integer, ByVal j As Integer, ByVal k As Integer, ByRef arrClassReg As ArrayList)
        If JmlTest = 1 Then
            If arrClassReg(j).Test1 < arrClassReg(k).Test1 Then
                ChangePosition(j, k, arrClassReg)
            ElseIf arrClassReg(j).Test1 = arrClassReg(k).Test1 Then
                ChangePositionCouseOfID(j, k, arrClassReg)
            End If
        Else
            If arrClassReg(j).Test1 < arrClassReg(k).Test1 Then
                ChangePosition(j, k, arrClassReg)
            ElseIf arrClassReg(j).Test1 = arrClassReg(k).Test1 Then
                ChangePositionLevel2(JmlTest, j, k, arrClassReg)
            End If
        End If
    End Sub

    Private Function RankClassRegistration(ByVal ArrTrClassReg As ArrayList) As ArrayList
        Dim arrClassReg As ArrayList = ArrTrClassReg 'CType(_sessHelper.GetSession("sessClassRegistrationAverage"), ArrayList)
        Dim objClassRegTmp As TrClassRegistration
        Dim JmlTest As Integer = CType(_sessHelper.GetSession("JumlahTest"), Integer)

        'lakukan sorting ranking, jika average sama, lihat initial test, test 1, test 2 ...test N dan terakhir id
        If Not IsNothing(arrClassReg) Then
            For j As Integer = 0 To arrClassReg.Count - 1
                For k As Integer = j + 1 To arrClassReg.Count - 1
                    If arrClassReg(j).Avarage = arrClassReg(k).Avarage Then
                        If arrClassReg(j).InitialTest = arrClassReg(k).InitialTest Then
                            ChangePositionLevel1(JmlTest, j, k, arrClassReg)
                        ElseIf arrClassReg(j).InitialTest < arrClassReg(k).InitialTest Then
                            ChangePosition(j, k, arrClassReg)
                        End If
                    Else
                        If arrClassReg(j).Avarage < arrClassReg(k).Avarage Then
                            ChangePosition(j, k, arrClassReg)
                            'objClassRegTmp = arrClassReg(j)
                            'arrClassReg(j) = arrClassReg(k)
                            'arrClassReg(k) = objClassRegTmp
                        End If
                    End If
                Next
            Next

            Dim i As Integer = 1
            For Each objClassReg As TrClassRegistration In arrClassReg
                objClassReg.Rank = i
                i += 1
            Next
            Return arrClassReg
        Else
            Return Nothing
        End If
    End Function

    Private Sub dtgClassRegistration_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgClassRegistration.ItemCommand
        If e.CommandName = "View" Then
            'If ViewState("NeedReCalculate") Then
            'MessageBox.Show("Rata-rata dan ranking perlu dihitung ulang")
            'Else
            If Not IsNothing(Session("sessClassRegistration")) Then
                Dim ArrListPointing As ArrayList = CType(_sessHelper.GetSession("sessClassRegistration"), ArrayList)
                Dim objClassReg As TrClassRegistration = New TrClassRegistration
                objClassReg = ArrListPointing(e.Item.ItemIndex)
                PrepareSessionBeforeMoving(objClassReg)
                Dim areas As String = String.Empty
                If AreaId.NotNullorEmpty Then
                    areas = "&area=" + AreaId
                End If
                If Not IsNothing(Session("SessObjClassReg")) Then
                    If Not Request.QueryString("Rank") Is Nothing Then
                        Response.Redirect("../Training/FrmDisplayPersonalEvaluation.aspx?Rank=True" + areas)
                    Else
                        Response.Redirect("../Training/FrmDisplayPersonalEvaluation.aspx?ids=2" + areas)
                    End If

                Else
                    'Response.Redirect("../login.aspx#expired")
                End If
            Else
                'Response.Redirect("../login.aspx#expired")
            End If
            'End If

        End If
    End Sub

    Private Sub PrepareSessionBeforeMoving(ByVal objClassReg As TrClassRegistration)
        _sessHelper.SetSession("SessObjClassReg", objClassReg)
        _sessHelper.SetSession("txtDealerSelection", txtDealerSelection)
        _sessHelper.SetSession("ddlFiscalYear", ddlFiscalYear.SelectedValue)
        _sessHelper.SetSession("ddlCategory", txtKodeKategori.Text)
        _sessHelper.SetSession("ddlClass", txtKodeKelas.Text)
        _sessHelper.SetSession("NoReg", txtNoReg.Text)
        _sessHelper.SetSession("lblClassCode", lblClassCode)
        _sessHelper.SetSession("lblClassName", lblClassName)
        _sessHelper.SetSession("lblStart", lblStart)
        _sessHelper.SetSession("lblFinish", lblFinish)
    End Sub

    Private Sub RestorePreviousState()
        Dim objTxtDealerSelection As TextBox = _sessHelper.GetSession("txtDealerSelection")
        txtDealerSelection.Text = objTxtDealerSelection.Text
        txtDealerSelection.Visible = objTxtDealerSelection.Visible

        ddlFiscalYear.SelectedValue = _sessHelper.GetSession("ddlFiscalYear")
        txtKodeKategori.Text = _sessHelper.GetSession("ddlCategory")
        txtKodeKelas.Text = _sessHelper.GetSession("ddlClass")
        txtNoReg.Text = _sessHelper.GetSession("NoReg")

        Dim objLblClassCode As Label = _sessHelper.GetSession("lblClassCode")
        lblClassCode.Text = objLblClassCode.Text
        lblClassCode.Visible = objLblClassCode.Visible

        Dim objlblClassName As Label = _sessHelper.GetSession("lblClassName")
        lblClassName.Text = objlblClassName.Text
        lblClassName.Visible = objlblClassName.Visible

        Dim objLblStart As Label = _sessHelper.GetSession("lblStart")
        lblStart.Text = objLblStart.Text
        lblStart.Visible = objLblStart.Visible

        Dim objLblFinish As Label = _sessHelper.GetSession("lblFinish")
        lblFinish.Text = objLblFinish.Text
        lblFinish.Visible = objLblFinish.Visible


        lblKodeKelas.Visible = True
        lblNamaKelas.Visible = True
        lblMulai.Visible = True
        lblSelesai.Visible = True
        btnCari.Enabled = True
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ClassCode", MatchType.Exact, txtKodeKelas.Text))
            Dim arryList As ArrayList = New TrClassRegistrationFacade(User).RetrieveActiveList(indexPage + 1, dtgClassRegistration.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection), criterias)
            dtgClassRegistration.DataSource = arryList
            _sessHelper.SetSession("sessClassRegistration", arryList)
            If arryList.Count > 0 Then
                dtgClassRegistration.VirtualItemCount = totalRow
                ViewState("NeedReCalculate") = False
                dtgClassRegistration.DataBind()
                btnCetak.Disabled = False 'True

                Dim oClassReg As TrClassRegistration = CType(arryList(0), TrClassRegistration)
                If oClassReg.TrClass.SubmitStatus = 1 Then
                    btnSubmit.Enabled = False
                    btnSave.Enabled = False
                Else
                    btnSubmit.Enabled = True
                    btnSave.Enabled = True
                End If

                btnDownLoad.Enabled = True
            Else
                btnCetak.Disabled = True 'False
                btnSave.Enabled = False
                btnSubmit.Enabled = False
                btnDownLoad.Enabled = False
            End If
        End If
    End Sub
    Private Sub dtgClassRegistration_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegistration.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim dataKelas As TrClass = New TrClassFacade(User).Retrieve(txtKodeKelas.Text)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, dataKelas.TrCourse.ID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "Type", MatchType.Exact, "0"))
            Dim listEva As List(Of TrCourseEvaluation) = New TrCourseEvaluationFacade(User).Retrieve(criterias).Cast(Of TrCourseEvaluation).ToList()

            For Each itemcell As TableCell In e.Item.Cells
                If itemcell.Text.IndexOf("Awal") > -1 Or itemcell.Text.IndexOf("Akhir") > -1 Then
                    Continue For
                End If

                If itemcell.Text.StartsWith("Test") Then
                    Dim dataEva As TrCourseEvaluation = listEva.FirstOrDefault(Function(x) CInt(Right(x.EvaluationCode, 2)) = CInt(Right(itemcell.Text, 1)))
                    If dataEva IsNot Nothing Then
                        itemcell.Text = dataEva.Name
                    Else
                        CellDisabled.Add(Right(itemcell.Text, 1))
                        itemcell.Visible = False
                    End If
                    'Ridwan Code
                End If
            Next
        End If
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            For Each mNumber As String In CellDisabled
                e.Item.Cells(6 + CInt(mNumber)).Visible = False
            Next
            BoundRowItems(e)
            CType(e.Item.FindControl("lblNo"), Label).Text = e.CreateNumberPage
        End If
    End Sub
    Private Sub DetectAverageChanges(ByVal objTrClassRegistration As TrClassRegistration, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim critCourseEval As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, objTrClassRegistration.TrClass.TrCourse.ID))

        'Ambil courseevaluation yang dipilih pada trnumclassevaluation
        critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "ID", MatchType.InSet, ("(select CourseEvaluationID from trclassnumevaluation where ClassID=" & objTrClassRegistration.TrClass.ID & ")")))

        Dim TrCourseEvalColl As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(critCourseEval)

        Dim critCertificateline As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCertificateline.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.ID", MatchType.Exact, objTrClassRegistration.ID))
        Dim TrCertificateLineColl As ArrayList = New TrCertificateLineFacade(User).Retrieve(critCertificateline)


        If TrCourseEvalColl.Count > 0 Then
            Dim Total As Decimal = 0
            Dim nJumlah As Integer = 0
            Dim bCheck As Boolean = False

            For j As Integer = 0 To TrCourseEvalColl.Count - 1 'iterasi untuk hitung total
                Dim objTrCourseEval As TrCourseEvaluation = CType(TrCourseEvalColl(j), TrCourseEvaluation)
                'For k As Integer = 0 To TrCertificateLineColl.Count - 1
                Dim bCertificateLineFound As Boolean = False
                Dim objTrCertificateLine As TrCertificateLine
                For i As Integer = 0 To TrCertificateLineColl.Count - 1
                    If TrCertificateLineColl(i).TrCourseEvaluation.EvaluationCode = objTrCourseEval.EvaluationCode Then
                        bCertificateLineFound = True
                        objTrCertificateLine = TrCertificateLineColl(i)
                        Exit For
                    End If
                Next

                If bCertificateLineFound Then
                    If Not objTrCertificateLine Is Nothing Then

                        If objTrCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) _
                        And Right(objTrCertificateLine.TrCourseEvaluation.EvaluationCode, 2) <> "00" Then
                            Total += objTrCertificateLine.NumTestResult
                        End If
                        If objTrCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) _
                        And Right(objTrCertificateLine.TrCourseEvaluation.EvaluationCode, 2) = "00" Then

                            If objTrCertificateLine.TrClassRegistration.InitialTest <> objTrCertificateLine.NumTestResult Then
                                CType(e.Item.FindControl("lblInitial"), Label).Text = CType(objTrCertificateLine.NumTestResult, String)
                                CType(e.Item.FindControl("lblInitial"), Label).ForeColor = Red
                                objTrCertificateLine.TrClassRegistration.InitialTest = objTrCertificateLine.NumTestResult
                                bCheck = True
                            End If
                        End If

                        If objTrCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) And _
                        Right(objTrCertificateLine.TrCourseEvaluation.EvaluationCode, 2) = "99" Then
                            If objTrCertificateLine.TrClassRegistration.FinalTest <> objTrCertificateLine.NumTestResult Then
                                CType(e.Item.FindControl("lblFinal"), Label).Text = CType(objTrCertificateLine.NumTestResult, String)
                                CType(e.Item.FindControl("lblFinal"), Label).ForeColor = Red
                                objTrCertificateLine.TrClassRegistration.InitialTest = objTrCertificateLine.NumTestResult
                                bCheck = True
                            End If
                        End If

                    End If
                End If

                If objTrCourseEval.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) And Right(objTrCourseEval.EvaluationCode, 2) <> "00" Then
                    nJumlah += 1
                End If

            Next
            If nJumlah > 0 Then
                Dim rata As Double = (Total / nJumlah)
                If objTrClassRegistration.Avarage <> CDbl(FormatNumber(rata, 3, TriState.UseDefault, TriState.UseDefault, TriState.True)) Or bCheck Then
                    If objTrClassRegistration.Avarage <> (Total / nJumlah) Then
                        CType(e.Item.FindControl("lblAverage"), Label).Text = objTrClassRegistration.Avarage.ToString("#.##")
                        objTrClassRegistration.Avarage = (Total / nJumlah)
                    End If
                    CType(e.Item.FindControl("lblAverage"), Label).ForeColor = Red
                    CType(e.Item.FindControl("lblAverage"), Label).ToolTip = "Ada perubahan data, nilai rata-rata perlu dihitung ulang"
                    ViewState("NeedReCalculate") = True
                End If

            End If
        End If

    End Sub

    Private Sub BoundRowItems(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objTrClassRegistration As TrClassRegistration = CType(CType(dtgClassRegistration.DataSource, ArrayList)(e.Item.ItemIndex), TrClassRegistration)
        If Not IsNothing(objTrClassRegistration) Then
            If e.IsRowItems Then
                Dim lblRegCode As Label = e.FindLabel("lblRegCode")
                Dim lblSalesmanCode As Label = e.FindLabel("lblSalesmanCode")
                If AreaId.NotNullorEmpty Then
                    If AreaId.Equals("1") Or AreaId.Equals("3") Then
                        lblSalesmanCode.Text = objTrClassRegistration.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) _
                                               x.JobPositionAreaID = CInt(AreaId)).SalesmanHeader.SalesmanCode
                        lblRegCode.Text = objTrClassRegistration.TrTrainee.ID
                        lblRegCode.NonVisible()
                    Else
                        lblRegCode.Text = objTrClassRegistration.TrTrainee.ID
                        lblSalesmanCode.NonVisible()
                    End If
                End If

                'Ranking
                If objTrClassRegistration.Rank <> Nothing Then
                    CType(e.Item.FindControl("lblRank"), Label).Text = objTrClassRegistration.Rank.ToString("###")
                End If
                'status kelulusan
                If objTrClassRegistration.Status <> "" Then
                    Select Case objTrClassRegistration.Status
                        Case EnumTrClassRegistration.DataStatusType.Pass
                            CType(e.Item.FindControl("lblStatus"), Label).Text = "Lulus"
                        Case EnumTrClassRegistration.DataStatusType.Fail
                            CType(e.Item.FindControl("lblStatus"), Label).Text = "Tidak Lulus"
                    End Select
                    ''If objTrClassRegistration.Status = CStr(EnumTrClassRegistration.DataStatusType.Reject) Then
                    ''    CType(e.Item.FindControl("lblStatus"), Label).Text = "Tolak"
                    ''ElseIf objTrClassRegistration.Status = CStr(EnumTrClassRegistration.DataStatusType.Register) Then
                    ''    CType(e.Item.FindControl("lblStatus"), Label).Text = "Daftar"
                    ''ElseIf objTrClassRegistration.Status = CStr(EnumTrClassRegistration.DataStatusType.Fail) Then
                    ''    CType(e.Item.FindControl("lblStatus"), Label).Text = "Tidak Lulus"
                    ''ElseIf objTrClassRegistration.Status = CStr(EnumTrClassRegistration.DataStatusType.Pass) Then
                    ''    CType(e.Item.FindControl("lblStatus"), Label).Text = "Lulus"
                    ''End If
                End If
                If CType(e.Item.FindControl("lblInitial"), Label).Text = "" Then
                    CType(e.Item.FindControl("lblInitial"), Label).Text = objTrClassRegistration.InitialTest.ToString("#.##")
                    If objTrClassRegistration.InitialTest = 0 Then
                        CType(e.Item.FindControl("lblInitial"), Label).Text = 0
                    End If
                End If

                If CType(e.Item.FindControl("lblFinal"), Label).Text = "" Then
                    CType(e.Item.FindControl("lblFinal"), Label).Text = objTrClassRegistration.FinalTest.ToString("#.##")
                    If objTrClassRegistration.FinalTest = 0 Then
                        CType(e.Item.FindControl("lblFinal"), Label).Text = 0
                    End If
                End If
                'periksa dan tandai jika ada perubahan data nilai
                DetectAverageChanges(objTrClassRegistration, e)
                'untuk versi terbaru nilai test1, test2 ... ditampilkan

                'tampilkan Rata-rata
                If objTrClassRegistration.Avarage >= 0 Then
                    CType(e.Item.FindControl("lblAverage"), Label).Text = objTrClassRegistration.Avarage.ToString("#.##")
                Else
                    If CType(e.Item.FindControl("lblAverage"), Label).Text = "" Then
                        CType(e.Item.FindControl("lblAverage"), Label).Text = "-"
                    End If
                End If
                DisplayTest(e, objTrClassRegistration)
                If Not objDealer Is Nothing Then
                    If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        If objTrClassRegistration.TrClass.SubmitStatus = EnumTrClassSubmitStatus.TrClassSubmitStatus.Submited Then
                            dtgClassRegistration.Columns(18).Visible = False
                            dtgClassRegistration.Columns(19).Visible = False
                            dtgClassRegistration.Columns(20).Visible = False
                            dtgClassRegistration.Columns(21).Visible = False
                        Else

                            dtgClassRegistration.Columns(18).Visible = True
                            dtgClassRegistration.Columns(19).Visible = True
                            dtgClassRegistration.Columns(20).Visible = True
                            dtgClassRegistration.Columns(21).Visible = True
                        End If
                    End If
                End If

            End If
        End If

    End Sub

    Private Sub DisplayTest(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal objTrClassRegistration As TrClassRegistration)

        Dim critCertificateline As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCertificateline.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.ID", MatchType.Exact, objTrClassRegistration.ID))
        critCertificateline.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrCourseEvaluation.Type", MatchType.Exact, CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)))
        Dim TrCertificateLineColl As ArrayList = New TrCertificateLineFacade(User).Retrieve(critCertificateline)
        If TrCertificateLineColl.Count > 0 Then
            For Each obj As TrCertificateLine In TrCertificateLineColl
                If Right(obj.TrCourseEvaluation.EvaluationCode, 2) <> "99" Or Right(obj.TrCourseEvaluation.EvaluationCode, 2) <> "00" Then
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "01" Then
                        objTrClassRegistration.Test1 = obj.NumTestResult
                        CType(e.Item.FindControl("lblTest1"), Label).Text = CType(objTrClassRegistration.Test1, String)
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "02" Then
                        objTrClassRegistration.Test2 = obj.NumTestResult
                        CType(e.Item.FindControl("lblTest2"), Label).Text = CType(objTrClassRegistration.Test2, String)
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "03" Then
                        objTrClassRegistration.Test3 = obj.NumTestResult
                        CType(e.Item.FindControl("lblTest3"), Label).Text = CType(objTrClassRegistration.Test3, String)
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "04" Then
                        objTrClassRegistration.Test4 = obj.NumTestResult
                        CType(e.Item.FindControl("lblTest4"), Label).Text = CType(objTrClassRegistration.Test4, String)
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "05" Then
                        objTrClassRegistration.Test5 = obj.NumTestResult
                        CType(e.Item.FindControl("lblTest5"), Label).Text = CType(objTrClassRegistration.Test5, String)
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "06" Then
                        objTrClassRegistration.Test6 = obj.NumTestResult
                        CType(e.Item.FindControl("lblTest6"), Label).Text = CType(objTrClassRegistration.Test6, String)
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "07" Then
                        objTrClassRegistration.Test7 = obj.NumTestResult
                        CType(e.Item.FindControl("lblTest7"), Label).Text = CType(objTrClassRegistration.Test7, String)
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "08" Then
                        objTrClassRegistration.Test8 = obj.NumTestResult
                        CType(e.Item.FindControl("lblTest8"), Label).Text = CType(objTrClassRegistration.Test8, String)
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "09" Then
                        objTrClassRegistration.Test9 = obj.NumTestResult
                        CType(e.Item.FindControl("lblTest9"), Label).Text = CType(objTrClassRegistration.Test9, String)
                    End If
                End If
            Next
        End If
    End Sub
    Private Function UpdateCollectionTrClassRegistration(ByVal arrClassReg As ArrayList) As Integer
        Try
            Return New TrClassRegistrationFacade(User).UpdateTrClassRegistrationCollection(arrClassReg)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Private Sub dtgCourseEvaluation_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClassRegistration.PageIndexChanged
        dtgClassRegistration.SelectedIndex = -1
        dtgClassRegistration.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgClassRegistration.CurrentPageIndex)
    End Sub

    Private Sub dtgCourseEvaluation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClassRegistration.SortCommand
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
        dtgClassRegistration.SelectedIndex = -1
        dtgClassRegistration.CurrentPageIndex = 0
        'BindDatagrid(dtgClassRegistration.CurrentPageIndex)
        Rebind()
    End Sub

    Private Sub btnDownLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownLoad.Click
        'Response.Redirect("./FrmDownLoadEHT.aspx")
        Using excelPackage As New ExcelPackage()
            Dim wsData As ExcelWorksheet = excelPackage.Workbook.Worksheets.Add("Evaluasi Hasil Training")
            Dim rowIdx As Integer = 1
            wsData.Cells("A" & rowIdx.ToString()).ValueBold("Evaluasi Hasil Training")
            rowIdx += 1
            rowIdx += 1

            wsData.Cells("B" & rowIdx.ToString()).ValueBold("Kode Kelas ")
            wsData.Cells("C" & rowIdx.ToString()).ValueBold(lblClassCode.Text)
            wsData.Cells("E" & rowIdx.ToString()).ValueBold("Nama Kelas ")
            wsData.Cells("F" & rowIdx.ToString()).ValueBold(lblClassName.Text)
            rowIdx += 1
            wsData.Cells("B" & rowIdx.ToString()).ValueBold("Mulai ")
            wsData.Cells("C" & rowIdx.ToString()).ValueBold(lblStart.Text)
            wsData.Cells("E" & rowIdx.ToString()).ValueBold("Selesai ")
            wsData.Cells("F" & rowIdx.ToString()).ValueBold(lblFinish.Text)
            rowIdx += 1

            Dim listDisabled As New List(Of String)
            For Each dtItem As DataGridItem In dtgClassRegistration.Items
                If dtItem.IsRowItems Then
                    For idx As Integer = 1 To 9
                        Dim lblInput As Label = dtItem.FindLabel("lblTest" + idx.ToString)
                        If Not lblInput.Visible Then
                            listDisabled.Add("Test " + idx.ToString)
                        End If
                    Next
                    Exit For
                End If
            Next
            rowIdx += 1
            Dim columnIdx As Integer = 1
            Dim dataKelas As TrClass = New TrClassFacade(User).Retrieve(lblClassCode.Text)

            For Each dtColumn As DataGridColumn In dtgClassRegistration.Columns
                If dtColumn.Visible And listDisabled.IndexOf(dtColumn.HeaderText) <= -1 And dtColumn.HeaderText.NotNullorEmpty Then
                    If dtColumn.HeaderText.StartsWith("Test") Then
                        Dim evaluationCode As String = dataKelas.TrCourse.CourseCode.ToUpper() + "-A" + CInt(dtColumn.HeaderText.Replace("Test ", "")).GenerateIncrement(2)
                        Dim crit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, dataKelas.TrCourse.ID))
                        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "EvaluationCode", MatchType.Exact, evaluationCode))
                        Dim dataEva As TrCourseEvaluation = CType(New TrCourseEvaluationFacade(User).Retrieve(crit)(0), TrCourseEvaluation)
                        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue(dataEva.Name)
                    Else
                        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue(dtColumn.HeaderText)
                    End If
                    columnIdx += 1
                End If
            Next
            rowIdx += 1
            For Each dtItem As DataGridItem In dtgClassRegistration.Items
                If dtItem.IsRowItems Then
                    Dim clmIdx As Integer = 1
                    Dim lblNo As Label = dtItem.FindLabel("lblNo")
                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblNo.Text, Style.ExcelHorizontalAlignment.Center)
                    clmIdx += 1

                    Dim lblSalesmanCode As Label = dtItem.FindLabel("lblSalesmanCode")
                    Dim lblRegCode As Label = dtItem.FindLabel("lblRegCode")

                    If AreaId.IsNullorEmpty Then
                        wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblRegCode.Text, Style.ExcelHorizontalAlignment.Left)
                    Else
                        If AreaId.Equals("1") Or AreaId.Equals("3") Then
                            wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblSalesmanCode.Text, Style.ExcelHorizontalAlignment.Left)
                        Else
                            wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblRegCode.Text, Style.ExcelHorizontalAlignment.Left)
                        End If
                    End If
                    clmIdx += 1

                    Dim lblTraineeName As Label = dtItem.FindLabel("lblTraineeName")
                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblTraineeName.Text, Style.ExcelHorizontalAlignment.Left)
                    clmIdx += 1

                    Dim lblDealerName As Label = dtItem.FindLabel("lblDealerName")
                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblDealerName.Text, Style.ExcelHorizontalAlignment.Left)
                    clmIdx += 1

                    Dim lblCity As Label = dtItem.FindLabel("lblCity")
                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblCity.Text, Style.ExcelHorizontalAlignment.Left)
                    clmIdx += 1

                    Dim lblInitial As Label = dtItem.FindLabel("lblInitial")
                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblInitial.Text, Style.ExcelHorizontalAlignment.Center)
                    clmIdx += 1

                    For idx As Integer = 1 To 9
                        Dim lblInput As Label = dtItem.FindLabel("lblTest" + idx.ToString)
                        If lblInput.Visible Then
                            wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblInput.Text, Style.ExcelHorizontalAlignment.Center)
                            clmIdx += 1
                        End If
                    Next

                    Dim lblFinal As Label = dtItem.FindLabel("lblFinal")
                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblFinal.Text, Style.ExcelHorizontalAlignment.Center)
                    clmIdx += 1

                    Dim lblAverage As Label = dtItem.FindLabel("lblAverage")
                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblAverage.Text, Style.ExcelHorizontalAlignment.Center)
                    clmIdx += 1

                    Dim lblRank As Label = dtItem.FindLabel("lblRank")
                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblRank.Text, Style.ExcelHorizontalAlignment.Center)
                    clmIdx += 1

                    Dim lblStatus As Label = dtItem.FindLabel("lblStatus")
                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblStatus.Text, Style.ExcelHorizontalAlignment.Center)
                    clmIdx += 1
                    rowIdx += 1
                End If
            Next
            For colIdx As Integer = 2 To columnIdx - 1
                wsData.Column(colIdx).AutoFit()
            Next

            Dim fileBytes = excelPackage.GetAsByteArray()
            Dim fileName As String = String.Format("EvaluasiHasilTraining_{0}.xls", dataKelas.ClassCode)

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Dim success As Boolean = False

            Try
                success = imp.Start()
                If success Then
                    File.WriteAllBytes(Server.MapPath("~/DataTemp/" & fileName), fileBytes)
                    imp.StopImpersonate()
                End If

            Catch ex As Exception
                Exit Sub

            End Try

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)
        End Using
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not Request.QueryString("QS") Is Nothing Then
            If AreaId.IsNullorEmpty Then
                Response.Redirect("../Training/FrmTrCertificateLine3.aspx?QS=" & Request.QueryString("QS"))
            Else
                Response.Redirect("../Training/FrmTrCertificateLine3.aspx?QS=" & Request.QueryString("QS") & "&area=" & AreaId)
            End If
        ElseIf Not Request.QueryString("Rank") Is Nothing Then
            Select Case AreaId
                Case "1"
                    Response.Redirect("../Training/FrmDataStatusSiswa.aspx?category=sales")
                Case "2"
                    Response.Redirect("../Training/FrmDataStatusSiswa.aspx?category=ass")
                Case "3"
                    Response.Redirect("../Training/FrmDataStatusSiswa.aspx?category=cs")
                Case Else
                    Response.Redirect("../Training/FrmTrTrainee1.aspx?Menu=5")
            End Select
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            ssobjClass = New TrClassFacade(User).Retrieve(txtKodeKelas.Text)
            If ssobjClass.ID > 0 Then
                ssobjClass.SubmitStatus = 1
                ssobjClass.TrCertificateConfig = New TrCertificateConfig(CInt(hdnCerID.Value))
                Dim nResult As Integer = New TrClassFacade(User).Update(ssobjClass)
                If nResult <> -1 Then
                    If ssobjClass.TrCourse.TrTraineeLevel IsNot Nothing Then
                        Dim FuncTCR As TrClassRegistrationFacade = New TrClassRegistrationFacade(User)
                        Dim dataSertifikat As New TrCertificateConfig

                        Dim crit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ID", MatchType.Exact, ssobjClass.ID))
                        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Pass, String)))


                        Dim critsCfg As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCertificateConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critsCfg.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateConfig), "Status", MatchType.Exact, 1))

                        If hdnCerID.Value.IsNullorEmpty Then
                            Dim arrSertifikat As ArrayList = New TrCertificateConfigFacade(User).Retrieve(critsCfg)
                            If arrSertifikat.IsItems Then
                                dataSertifikat = CType(arrSertifikat(0), TrCertificateConfig)
                            End If
                        Else
                            dataSertifikat = New TrCertificateConfig(CInt(hdnCerID.Value))
                        End If

                        Dim dataLulus As ArrayList = FuncTCR.Retrieve(crit)
                        Dim dataCourse As TrCourse = ssobjClass.TrCourse

                        For Each siswaLulus As TrClassRegistration In dataLulus
                            Dim funcH As New TrTraineeLevelHeaderFacade(User)
                            Dim funcD As New TrTraineeLevelDetailFacade(User)
                            Dim funcCR As New TrClassRegistrationFacade(User)
                            Dim funcC As New TrCourseFacade(User)
                            Dim IsSaveLevel As Boolean = True

                            If siswaLulus.CertificateNo.IsNullorEmpty Then
                                siswaLulus.CertificateNo = "generate"
                                funcCR.Update(siswaLulus)
                            End If



                            Dim crits As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelHeader), "TrTrainee.ID", MatchType.Exact, siswaLulus.TrTrainee.ID))
                            crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelHeader), "TrCourseCategory.ID", MatchType.Exact, dataCourse.Category.ID))

                            Dim arrDataLevel As ArrayList = funcH.Retrieve(crits)
                            If arrDataLevel.Count > 0 Then
                                Dim dataLevel As TrTraineeLevelHeader = CType(arrDataLevel(0), TrTraineeLevelHeader)
                                If dataCourse.TrTraineeLevel.Sequence <= dataLevel.TrTraineeLevel.Sequence Then
                                    Continue For
                                End If
                            End If

                            Dim critTCR As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critTCR.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.ID", MatchType.Exact, siswaLulus.TrTrainee.ID))
                            critTCR.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Pass, String)))

                            Dim allDataLulus As List(Of TrClassRegistration) = FuncTCR.Retrieve(critTCR).Cast(Of TrClassRegistration).ToList()

                            Dim critTrCr As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critTrCr.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "TrTraineeLevel.ID", MatchType.Exact, dataCourse.TrTraineeLevel.ID))
                            critTrCr.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "Category.ID", MatchType.Exact, dataCourse.Category.ID))

                            Dim courseLevel As ArrayList = funcC.Retrieve(critTrCr)
                            Dim listLulus As New List(Of String)
                            For Each itemCourse As TrCourse In courseLevel
                                If allDataLulus.Where(Function(x) x.TrClass.TrCourse.ID.Equals(itemCourse.ID)).Count = 0 Then
                                    IsSaveLevel = False
                                    Exit For
                                Else
                                    listLulus.Add(allDataLulus.FirstOrDefault(Function(x) x.TrClass.TrCourse.ID.Equals(itemCourse.ID)).ID.ToString())
                                End If
                            Next

                            If IsSaveLevel Then
                                Dim lvlDetail As TrTraineeLevelDetail = New TrTraineeLevelDetail
                                lvlDetail.TrTrainee = siswaLulus.TrTrainee
                                lvlDetail.TrCourseCategory = dataCourse.Category
                                lvlDetail.NamaSiswa = siswaLulus.TrTrainee.Name
                                lvlDetail.TrCertificateConfig = dataSertifikat
                                lvlDetail.TrTraineeLevel = dataCourse.TrTraineeLevel
                                lvlDetail.TanggalLulus = siswaLulus.TrClass.FinishDate
                                lvlDetail.TrClassRegistration = String.Join(",", listLulus.ToArray())
                                lvlDetail.Status = 1
                                lvlDetail.CertificateNumber = String.Empty
                                lvlDetail.ClassID = siswaLulus.TrClass.ID
                                funcD.Insert(lvlDetail)

                                If arrDataLevel.Count > 0 Then
                                    Dim dataLevel As TrTraineeLevelHeader = CType(arrDataLevel(0), TrTraineeLevelHeader)
                                    dataLevel.TrTraineeLevel = dataCourse.TrTraineeLevel
                                    dataLevel.TanggalLulus = siswaLulus.TrClass.FinishDate
                                    dataLevel.Status = 1

                                    funcH.Update(dataLevel)
                                Else
                                    Dim lvlHeader As TrTraineeLevelHeader = New TrTraineeLevelHeader
                                    lvlHeader.TrTrainee = siswaLulus.TrTrainee
                                    lvlHeader.TrTraineeLevel = dataCourse.TrTraineeLevel
                                    lvlHeader.TrCourseCategory = dataCourse.Category
                                    lvlHeader.TanggalLulus = siswaLulus.TrClass.FinishDate
                                    lvlHeader.Status = 1

                                    funcH.Insert(lvlHeader)
                                End If
                            End If

                        Next

                    End If

                    MessageBox.Show(SR.UpdateSucces)
                    btnSubmit.Enabled = False
                    btnSave.Enabled = False
                    btnCancel.Enabled = True
                Else
                    MessageBox.Show(SR.UpdateFail)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try

    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            ssobjClass = New TrClassFacade(User).Retrieve(txtKodeKelas.Text)
            If ssobjClass.ID > 0 Then
                ssobjClass.SubmitStatus = 0
                Dim nResult As Integer = New TrClassFacade(User).Update(ssobjClass)
                If nResult <> -1 Then
                    If ssobjClass.TrCourse.TrTraineeLevel IsNot Nothing Then
                        Dim FuncTCR As TrClassRegistrationFacade = New TrClassRegistrationFacade(User)
                        Dim funcH As New TrTraineeLevelHeaderFacade(User)
                        Dim funcD As New TrTraineeLevelDetailFacade(User)

                        Dim crit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ID", MatchType.Exact, ssobjClass.ID))
                        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Pass, String)))
                        Dim dataLulus As ArrayList = FuncTCR.Retrieve(crit)
                        Dim dataCourse As TrCourse = ssobjClass.TrCourse

                        For Each siswaLulus As TrClassRegistration In dataLulus
                            Dim crits As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelDetail), "TrTrainee.ID", MatchType.Exact, siswaLulus.TrTrainee.ID))
                            crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelDetail), "TrCourseCategory.ID", MatchType.Exact, dataCourse.Category.ID))
                            crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelDetail), "TrClassRegistration", MatchType.Partial, siswaLulus.ID))

                            Dim arrLeveldetail As ArrayList = funcD.Retrieve(crits)
                            If arrLeveldetail.IsItems Then
                                Dim levelDetail As TrTraineeLevelDetail = CType(arrLeveldetail(0), TrTraineeLevelDetail)
                                levelDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                                funcD.Update(levelDetail)

                                Dim critsH As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                critsH.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelHeader), "TrTrainee.ID", MatchType.Exact, siswaLulus.TrTrainee.ID))
                                critsH.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelHeader), "TrCourseCategory.ID", MatchType.Exact, dataCourse.Category.ID))
                                critsH.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelHeader), "TanggalLulus", MatchType.Exact, levelDetail.TanggalLulus))
                                critsH.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelHeader), "TrTraineeLevel.ID", MatchType.Exact, levelDetail.TrTraineeLevel.ID))

                                Dim arrLevelheader As ArrayList = funcH.Retrieve(critsH)
                                If arrLevelheader.IsItems Then
                                    Dim levelHeader As TrTraineeLevelHeader = CType(arrLevelheader(0), TrTraineeLevelHeader)
                                    If levelHeader.CreatedTime.DateDay = levelDetail.CreatedTime.DateDay Then
                                        levelHeader.RowStatus = CType(DBRowStatus.Deleted, Short)
                                        funcH.Update(levelHeader)
                                    End If
                                End If
                            End If
                        Next

                    End If
                    If AreaId.Equals("2") Then
                        lblCerConfig.Visible = True
                    End If

                    MessageBox.Show(SR.UpdateSucces)
                    btnSubmit.Enabled = True
                    btnSave.Enabled = True
                    btnCancel.Enabled = False
                Else
                    MessageBox.Show(SR.UpdateFail)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
    End Sub
End Class
