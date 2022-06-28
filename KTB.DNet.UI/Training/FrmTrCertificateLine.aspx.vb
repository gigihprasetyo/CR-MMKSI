Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports System.Drawing.Color
Imports System.Web.UI.WebControls.BorderStyle
Imports KTB.DNet.Security


Public Class FrmTrCertificateLine
    Inherits System.Web.UI.Page
    Private m_bFormPrivilege As Boolean = False
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtNilaiAngka As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNilaiSikap As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgCertificateLine As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtTrainee As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpClass As System.Web.UI.WebControls.Label
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents txtRegNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpTrainee As System.Web.UI.WebControls.Label
    Protected WithEvents txtClassName As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnEvaluasi As System.Web.UI.WebControls.Button
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents txtClassCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJenisEval As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEvalCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpEval As System.Web.UI.WebControls.Label
    Protected WithEvents btnClear1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnClear2 As System.Web.UI.WebControls.Button
    Protected WithEvents btnClear3 As System.Web.UI.WebControls.Button
    Private _sessHelper As SessionHelper = New SessionHelper

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            'BindDatagrid(0)
        End If
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.TrainingUbahDataNilai_Privilege)
        'If Not SecurityProvider.Authorize(Context.User, SR.TrainingUbahDataNilai_Privilege) Then
        'Server.Transfer("../FrmAccessDenied.aspx?modulName=TRAINING - Jenis Evaluasi")
        'End If
    End Sub

    Private Sub InitiatePage()
        AssignAttributeControl()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "TrClassRegistration.TrClass.ClassName"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub AssignAttributeControl()
        Dim lblPopUpClass As Label = CType(Page.FindControl("lblPopUpClass"), Label)
        lblPopUpClass.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpClassSelection.aspx", "", 500, 760, "ClassSelection")
        Dim lblPopUpTrainee As Label = CType(Page.FindControl("lblPopUpTrainee"), Label)
        lblPopUpTrainee.Attributes.Add("onclick", " return ShowPopupTraineeSelection()")
        Dim lblPopUpEval As Label = CType(Page.FindControl("lblPopUpEval"), Label)
        lblPopUpEval.Attributes.Add("onclick", " return ShowPopupCourseEvalSelection()")
    End Sub

    Private Sub ClearData()
        txtClassCode.Text = String.Empty
        txtClassName.Text = String.Empty
        txtRegNo.Text = String.Empty
        txtTrainee.Text = String.Empty
        txtNilaiAngka.Text = String.Empty
        txtNilaiSikap.Text = String.Empty
        txtEvalCode.Text = String.Empty
        txtJenisEval.Text = String.Empty

        txtNilaiAngka.Enabled = True
        txtNilaiSikap.Enabled = True
        txtNilaiAngka.ReadOnly = False
        txtNilaiSikap.ReadOnly = False
        txtNilaiAngka.BackColor = White
        txtNilaiSikap.BackColor = White


        lblPopUpClass.Enabled = True
        lblPopUpTrainee.Enabled = True
        lblPopUpEval.Enabled = True

        lblPopUpClass.ToolTip = "Klik Popup"
        lblPopUpTrainee.ToolTip = "Klik Popup"
        lblPopUpEval.ToolTip = "Klik Popup"

        txtClassCode.Enabled = True
        txtRegNo.Enabled = True
        txtEvalCode.Enabled = True
        txtClassCode.ReadOnly = False
        txtRegNo.ReadOnly = False
        txtEvalCode.ReadOnly = False

        txtNilaiAngka.Enabled = False
        txtNilaiSikap.Enabled = False
        txtNilaiAngka.Text = String.Empty
        txtNilaiSikap.Text = String.Empty
        txtNilaiAngka.BackColor = LightGray
        txtNilaiSikap.BackColor = LightGray

        'btnSimpan.Enabled = True
        btnClear1.Enabled = True
        btnClear2.Enabled = True
        btnClear3.Enabled = True
        btnEvaluasi.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub ClearDataAfterSave()

        txtNilaiAngka.Text = String.Empty
        txtNilaiSikap.Text = String.Empty

        txtNilaiAngka.Enabled = True
        txtNilaiSikap.Enabled = True
        txtNilaiAngka.ReadOnly = False
        txtNilaiSikap.ReadOnly = False
        txtNilaiAngka.BackColor = White
        txtNilaiSikap.BackColor = White

        lblPopUpClass.Enabled = True
        lblPopUpTrainee.Enabled = True
        lblPopUpEval.Enabled = True

        lblPopUpClass.ToolTip = "Klik Popup"
        lblPopUpTrainee.ToolTip = "Klik Popup"
        lblPopUpEval.ToolTip = "Klik Popup"


        txtClassCode.Enabled = True
        txtRegNo.Enabled = True
        txtEvalCode.Enabled = True
        txtClassCode.ReadOnly = False
        txtRegNo.ReadOnly = False
        txtEvalCode.ReadOnly = False

        txtNilaiAngka.Enabled = False
        txtNilaiSikap.Enabled = False
        txtNilaiAngka.Text = String.Empty
        txtNilaiSikap.Text = String.Empty
        txtNilaiAngka.BackColor = LightGray
        txtNilaiSikap.BackColor = LightGray

        btnSimpan.Enabled = False
        btnClear1.Enabled = True
        btnClear2.Enabled = True
        btnClear3.Enabled = True
        btnEvaluasi.Enabled = True

        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = _sessHelper.GetSession("sessCriteria")
        If (indexPage >= 0) Then
            If Not criterias Is Nothing Then
                dtgCertificateLine.DataSource = New TrCertificateLineFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgCertificateLine.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                         CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            Else
                dtgCertificateLine.DataSource = New TrCertificateLineFacade(User).RetrieveActiveList(indexPage + 1, dtgCertificateLine.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
         CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            End If
            dtgCertificateLine.VirtualItemCount = totalRow
            dtgCertificateLine.DataBind()
        End If
    End Sub


    Private Sub dtgCertificateLine_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgCertificateLine.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtClassCode.ReadOnly = True
            txtRegNo.ReadOnly = True
            txtEvalCode.ReadOnly = True

            txtTrainee.ReadOnly = True
            txtNilaiAngka.ReadOnly = True
            txtNilaiSikap.ReadOnly = True

            lblPopUpClass.Enabled = False
            lblPopUpTrainee.Enabled = False
            lblPopUpEval.Enabled = False
            lblPopUpClass.ToolTip = "Pada mode view, popup tidak aktif"
            lblPopUpTrainee.ToolTip = "Pada mode view, popup tidak aktif"
            lblPopUpEval.ToolTip = "Pada mode view, popup tidak aktif"

            btnSimpan.Enabled = False
            btnClear1.Enabled = False
            btnClear2.Enabled = False
            btnClear3.Enabled = False
            btnEvaluasi.Enabled = False


            ViewCertificateLine(e.Item.Cells(0).Text, False)

        ElseIf e.CommandName = "Edit" Then

            txtClassCode.ReadOnly = True
            txtRegNo.ReadOnly = True
            txtTrainee.ReadOnly = True
            txtEvalCode.ReadOnly = True

            txtNilaiAngka.ReadOnly = False
            txtNilaiSikap.ReadOnly = False

            lblPopUpClass.Enabled = False
            lblPopUpTrainee.Enabled = False
            lblPopUpEval.Enabled = False
            lblPopUpClass.ToolTip = "Pada mode edit, popup tidak aktif"
            lblPopUpTrainee.ToolTip = "Pada mode edit, popup tidak aktif"
            lblPopUpEval.ToolTip = "Pada mode edit, popup tidak aktif"
            btnSimpan.Enabled = True
            btnClear1.Enabled = False
            btnClear2.Enabled = False
            btnClear3.Enabled = False
            btnEvaluasi.Enabled = False

            ViewState.Add("vsProcess", "Edit")
            ViewCertificateLine(e.Item.Cells(0).Text, True)
            dtgCertificateLine.SelectedIndex = e.Item.ItemIndex

        ElseIf e.CommandName = "Delete" Then
            Try
                Dim nResult = DeleteTrCertificateLine(e.Item.Cells(0).Text)
                If nResult = 2 Then
                    MessageBox.Show(SR.CannotDelete)
                ElseIf nResult = -1 Then
                    MessageBox.Show(SR.DeleteFail)
                Else
                    MessageBox.Show(SR.DeleteSucces)
                End If
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            txtEvalCode.Text = String.Empty
            txtJenisEval.Text = String.Empty
            btnEvaluasi_Click(source, e)
            'ClearData()
            'BindDatagrid(dtgCertificateLine.CurrentPageIndex)
        End If
    End Sub

    Private Function DeleteTrCertificateLine(ByVal nID As Integer) As Integer
        Dim nResult As Integer = -1
        Dim objTrCertificateLine As TrCertificateLine = New TrCertificateLineFacade(User).Retrieve(nID)
        nResult = New TrCertificateLineFacade(User).DeleteFromDB(objTrCertificateLine)
        dtgCertificateLine.CurrentPageIndex = 0
        BindDatagrid(dtgCertificateLine.CurrentPageIndex)
        Return nResult
    End Function

    Private Sub ViewCertificateLine(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objTrCertificateLine As TrCertificateLine = New TrCertificateLineFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsTrCertificateLine", objTrCertificateLine)

        If Not IsNothing(objTrCertificateLine.TrClassRegistration.TrClass) Then
            txtClassCode.Text = objTrCertificateLine.TrClassRegistration.TrClass.ClassCode
            txtClassName.Text = objTrCertificateLine.TrClassRegistration.TrClass.ClassName
        Else
            txtClassCode.Text = String.Empty
            txtClassName.Text = String.Empty
        End If

        If Not IsNothing(objTrCertificateLine.TrClassRegistration.TrTrainee) Then
            txtRegNo.Text = objTrCertificateLine.TrClassRegistration.TrTrainee.ID
        Else
            txtRegNo.Text = String.Empty
        End If

        If Not IsNothing(objTrCertificateLine.TrClassRegistration.TrTrainee) Then
            txtTrainee.Text = objTrCertificateLine.TrClassRegistration.TrTrainee.Name
        Else
            txtRegNo.Text = String.Empty
        End If

        If Not IsNothing(objTrCertificateLine.TrCourseEvaluation.EvaluationCode) Then
            txtEvalCode.Text = objTrCertificateLine.TrCourseEvaluation.EvaluationCode
        Else
            txtEvalCode.Text = String.Empty
        End If

        If Not IsNothing(objTrCertificateLine.TrCourseEvaluation.Name) Then
            txtJenisEval.Text = objTrCertificateLine.TrCourseEvaluation.Name
        Else
            txtJenisEval.Text = String.Empty
        End If



        SetEnableTextBox(objTrCertificateLine.TrCourseEvaluation.ID, objTrCertificateLine)
        Me.btnSimpan.Enabled = EditStatus
    End Sub
    Private Sub SetEnableTextBox(ByVal nID As Integer, ByVal objTrCertificateLine As TrCertificateLine)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "ID", MatchType.Exact, nID))
        Dim ArryList As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(criterias)
        If ArryList.Count > 0 Then
            Dim ObjTrCourseEvaluation As TrCourseEvaluation = ArryList(0)
            If ObjTrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Angka Then
                txtNilaiAngka.Enabled = True
                txtNilaiSikap.Enabled = False
                txtNilaiSikap.Text = String.Empty
                txtNilaiAngka.Text = CType(objTrCertificateLine.NumTestResult, String)
                txtNilaiAngka.BackColor = White
                txtNilaiSikap.BackColor = LightGray
            Else
                txtNilaiAngka.Enabled = False
                txtNilaiSikap.Enabled = True
                txtNilaiAngka.Text = String.Empty
                txtNilaiSikap.Text = objTrCertificateLine.CharTestResult
                txtNilaiAngka.BackColor = LightGray
                txtNilaiSikap.BackColor = White
            End If
        Else
            txtNilaiAngka.Enabled = False
            txtNilaiSikap.Enabled = False
            txtNilaiAngka.Text = String.Empty
            txtNilaiSikap.Text = String.Empty
            txtNilaiAngka.BackColor = LightGray
            txtNilaiSikap.BackColor = LightGray
        End If


    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        dtgCertificateLine.SelectedIndex = -1
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim bCheck As Boolean = True
        Dim nCheck As Integer = 0

        If txtNilaiAngka.Enabled And txtNilaiAngka.Text = String.Empty Then
            bCheck = False
            nCheck = 1
        ElseIf txtNilaiSikap.Enabled And txtNilaiSikap.Text = String.Empty Then
            bCheck = False
            nCheck = 2
        End If

        If txtNilaiAngka.Enabled And (Not IsNumeric(txtNilaiAngka.Text.Trim)) Then
            bCheck = False
            nCheck = 5
        End If

        If bCheck Then
            If txtNilaiAngka.Text <> String.Empty Then
                If txtNilaiAngka.Enabled And (CType(txtNilaiAngka.Text, Decimal) > 100 Or CType(txtNilaiAngka.Text, Decimal)) < 0 Then
                    bCheck = False
                    nCheck = 3
                End If
            End If
        End If

        If bCheck Then
            If txtNilaiSikap.Text <> String.Empty Then
                If txtNilaiSikap.Enabled And Not (ValidateNilaiSikap(txtNilaiSikap.Text)) Then
                    bCheck = False
                    nCheck = 4
                End If
            End If
        End If

        If bCheck Then
            'Dim objTrCertificateLine As TrCertificateLine = New TrCertificateLine
            'Dim objTrCertificateLineFacade As TrCertificateLineFacade = New TrCertificateLineFacade(User)
            Dim nResult As Integer = -1

            If CType(ViewState("vsProcess"), String) = "Insert" Then
                'insert 
                ' == sesuai dengan permintaan, form ini dimodifikasi agar hanya dapat update saja
                'InsertTrCertificateLine()
            Else
                'update
                nResult = UpdateTrCertificateLine()
                If nResult = -1 Then
                    MessageBox.Show(SR.UpdateFail)
                Else
                    MessageBox.Show(SR.UpdateSucces)
                    ClearDataAfterSave()
                End If
            End If
            dtgCertificateLine.CurrentPageIndex = 0
            dtgCertificateLine.SelectedIndex = -1
            'txtEvalCode.Text = String.Empty
            'txtJenisEval.Text = String.Empty
            Dim criterias As ICriteria
            Dim totalrow As Integer
            If Not IsNothing(Session("sessCriteria")) Then
                criterias = CType(_sessHelper.GetSession("sessCriteria"), ICriteria)
                dtgCertificateLine.DataSource = New TrCertificateLineFacade(User).RetrieveActiveList(criterias, 1, dtgCertificateLine.PageSize, totalrow, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
                dtgCertificateLine.VirtualItemCount = totalrow
                dtgCertificateLine.DataBind()
            Else
                btnEvaluasi_Click(sender, e)
            End If

        Else
            If nCheck = 1 Then
                MessageBox.Show("Nilai Angka Belum Diisi")
            ElseIf nCheck = 2 Then
                MessageBox.Show("Nilai Sikap Belum Diisi")
            ElseIf nCheck = 3 Then
                MessageBox.Show("Nilai Angka diluar range [0,100]")
            ElseIf nCheck = 4 Then
                MessageBox.Show("Format Nilai Sikap tidak sesuai")
            ElseIf nCheck = 5 Then
                MessageBox.Show("Format Angka Tidak Sesuai")
            End If

        End If

    End Sub

    Private Function ValidateNilaiSikap(ByVal strNilaiSikap As String) As Boolean
        Dim str1 = txtNilaiSikap.Text.Substring(0, 1)
        If (str1 = "A" Or str1 = "B" Or str1 = "C" Or str1 = "D" Or str1 = "E") Then
            Return True
        End If
        Return False
    End Function

    Private Sub btnEvaluasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEvaluasi.Click

        Dim bCheck As Boolean = True
        dtgCertificateLine.CurrentPageIndex = 0

        Dim critClass As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtClassCode.Text <> "" Then
            critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassCode", MatchType.Exact, txtClassCode.Text))
        Else
            txtClassName.Text = String.Empty
        End If

        Dim TrClassColl As ArrayList = New TrClassFacade(User).Retrieve(critClass)
        Dim ObjTrClass As TrClass = New TrClass
        If TrClassColl.Count > 0 Then
            ObjTrClass = CType(TrClassColl(0), TrClass)
        Else
            MessageBox.Show("Kode kelas tidak terdaftar !")
            txtClassName.Text = String.Empty
            bCheck = False
        End If

        If bCheck Then

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If Not IsNothing(ObjTrClass) Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, ObjTrClass.TrCourse.ID))
            End If


            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(TrCourseEvaluation), "Name", Sort.SortDirection.ASC))
            Dim ArryList As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(criterias, sortColl)
            If ArryList.Count > 0 Then
                txtNilaiSikap.Enabled = False
                txtNilaiAngka.Text = String.Empty
                txtNilaiSikap.Text = String.Empty
                txtNilaiAngka.BackColor = LightGray
                txtNilaiSikap.BackColor = LightGray
            End If
        End If

        If bCheck Then
            Dim critClassReg As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If txtRegNo.Text <> "" Then
                Dim i As Integer
                'For i = 0 To Len(txtRegNo.Text.Trim) - 1
                If IsNumeric(txtRegNo.Text) Then
                    critClassReg.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.ID", MatchType.Exact, txtRegNo.Text))
                    'critClassReg.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Register, String)))
                    Dim ArrClassReg As ArrayList = New TrClassRegistrationFacade(User).Retrieve(critClassReg)
                    If ArrClassReg.Count <= 0 Then
                        MessageBox.Show("No. Reg tidak terdaftar atau siswa ybs telah lulus!")
                        txtTrainee.Text = String.Empty
                        bCheck = False
                    End If
                Else
                    MessageBox.Show("No. Registrasi harus angka !")
                    txtTrainee.Text = String.Empty
                    bCheck = False
                End If
                'Next
            Else
                txtTrainee.Text = String.Empty
            End If
        End If

        If bCheck Then
            If txtEvalCode.Text <> "" Then
                Dim CritCourseEval As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                CritCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "EvaluationCode", MatchType.Exact, txtEvalCode.Text))
                Dim ArrCourseEval As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(CritCourseEval)
                If ArrCourseEval.Count <= 0 Then
                    MessageBox.Show("Jenis Evaluasi tidak terdaftar !")
                    txtJenisEval.Text = String.Empty
                End If
            Else
                txtJenisEval.Text = String.Empty
            End If
        End If

        If bCheck Then
            Dim totalRow As Integer = 0
            Dim critTrCertificateLine As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If txtClassCode.Text <> "" Then
                critTrCertificateLine.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.TrClass.ClassCode", MatchType.Exact, txtClassCode.Text))
            End If
            If txtRegNo.Text <> "" Then
                critTrCertificateLine.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.TrTrainee.ID", MatchType.Exact, txtRegNo.Text))
            End If
            If txtEvalCode.Text <> "" Then
                critTrCertificateLine.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrCourseEvaluation.EvaluationCode", MatchType.Exact, txtEvalCode.Text))
            End If
            dtgCertificateLine.DataSource = New TrCertificateLineFacade(User).RetrieveActiveList(critTrCertificateLine, 1, dtgCertificateLine.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            _sessHelper.SetSession("sessCriteria", critTrCertificateLine)
            dtgCertificateLine.VirtualItemCount = totalRow
            dtgCertificateLine.DataBind()
        End If

    End Sub

    Private Sub dtgCertificateLine_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCertificateLine.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            BoundRowItems(e)
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCertificateLine.CurrentPageIndex * dtgCertificateLine.PageSize)
        End If

        'privilege
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
        End If
        If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
        End If
    End Sub

    Private Sub BoundRowItems(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objTrCertificateLine As TrCertificateLine = CType(CType(dtgCertificateLine.DataSource, ArrayList)(e.Item.ItemIndex), TrCertificateLine)
        If Not IsNothing(objTrCertificateLine) Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                If objTrCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Sikap, String) Or _
                   objTrCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Prestasi, String) Then
                    CType(e.Item.FindControl("lblNilaiAngka"), Label).Text = "N/A"
                End If
                If objTrCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) Then
                    CType(e.Item.FindControl("lblNilaiSikap"), Label).Text = "N/A"
                End If
            End If
        End If
    End Sub

    Private Sub dtgCourseEvaluation_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCertificateLine.PageIndexChanged
        dtgCertificateLine.SelectedIndex = -1
        dtgCertificateLine.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgCertificateLine.CurrentPageIndex)
        ' ClearData()
    End Sub

    Private Sub dtgCourseEvaluation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCertificateLine.SortCommand
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
        dtgCertificateLine.SelectedIndex = -1
        dtgCertificateLine.CurrentPageIndex = 0
        If txtClassCode.Text <> "" And txtRegNo.Text <> "" Then
            btnEvaluasi_Click(source, e)
        Else
            BindDatagrid(dtgCertificateLine.CurrentPageIndex)
        End If

    End Sub

    Private Function UpdateTrCertificateLine() As Integer
        Dim objTrCertificateLine As TrCertificateLine = CType(Session.Item("vsTrCertificateLine"), TrCertificateLine)
        Dim objTrClassReg As TrClassRegistration
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "ID", MatchType.Exact, txtRegNo.Text.Trim))
        Dim ArryList As ArrayList = New TrClassRegistrationFacade(User).Retrieve(criterias)
        If ArryList.Count > 0 Then
            objTrCertificateLine.TrClassRegistration = CType(ArryList(0), TrClassRegistration)
            objTrClassReg = CType(ArryList(0), TrClassRegistration)
        End If

        Dim CritTRCourseEval As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        CritTRCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "EvaluationCode", MatchType.Exact, Trim(txtEvalCode.Text)))

        Dim ObjTRCourseEvalColl As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(CritTRCourseEval)
        If ObjTRCourseEvalColl.Count > 0 Then
            objTrCertificateLine.TrCourseEvaluation = CType(ObjTRCourseEvalColl(0), TrCourseEvaluation)
            If objTrCertificateLine.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) Then
                If txtNilaiAngka.Text <> "" Then
                    objTrCertificateLine.NumTestResult = CType(txtNilaiAngka.Text, Decimal)
                    objTrCertificateLine.CharTestResult = String.Empty
                End If
            Else
                objTrCertificateLine.NumTestResult = CType(Nothing, Decimal)
                objTrCertificateLine.CharTestResult = CType(txtNilaiSikap.Text, String)
            End If
        End If
        objTrCertificateLine.LastUpdateBy = User.Identity.Name
        Dim nResult = New TrCertificateLineFacade(User).UpdateCertificateLineClassReg(objTrCertificateLine, objTrClassReg)
        Return nResult
    End Function

    Private Sub InsertTrCertificateLine()
        Dim objTrCertificateLine As TrCertificateLine = New TrCertificateLine
        Dim objTrCertificateLineFacade As TrCertificateLineFacade = New TrCertificateLineFacade(User)
        Dim nResult As Integer = -1
        If Not txtClassCode.Text = String.Empty And Not txtRegNo.Text = String.Empty Then

            Dim CritTRCourseEval As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'CritTRCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "ID", MatchType.Exact, CType(ddlEvaluation.SelectedValue, Integer)))
            Dim ObjTRCourseEvalColl As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(CritTRCourseEval)
            Dim ObjTrCourseEval As TrCourseEvaluation = New TrCourseEvaluation
            If ObjTRCourseEvalColl.Count > 0 Then
                ObjTrCourseEval = CType(ObjTRCourseEvalColl(0), TrCourseEvaluation)
            End If


            If objTrCertificateLineFacade.ValidateCode(txtRegNo.Text, ObjTrCourseEval.ID) <= 0 Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "ID", MatchType.Exact, txtRegNo.Text.Trim))
                Dim ArryList As ArrayList = New TrClassRegistrationFacade(User).Retrieve(criterias)
                If ArryList.Count > 0 Then
                    objTrCertificateLine.TrClassRegistration = CType(ArryList(0), TrClassRegistration)
                End If


                Dim CritTRCourseEval2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'CritTRCourseEval2.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "ID", MatchType.Exact, CType(ddlEvaluation.SelectedValue, Integer)))
                Dim ObjTRCourseEvalColl2 As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(CritTRCourseEval2)
                If ObjTRCourseEvalColl2.Count > 0 Then
                    objTrCertificateLine.TrCourseEvaluation = CType(ObjTRCourseEvalColl(0), TrCourseEvaluation)
                    If objTrCertificateLine.TrCourseEvaluation.Type = "1" Then

                        objTrCertificateLine.NumTestResult = CType(txtNilaiAngka.Text, Decimal)
                        objTrCertificateLine.CharTestResult = String.Empty
                    Else
                        objTrCertificateLine.NumTestResult = CType(Nothing, Decimal)
                        objTrCertificateLine.CharTestResult = CType(txtNilaiSikap.Text, String)
                    End If
                End If
                objTrCertificateLine.CreatedBy = User.Identity.Name

                nResult = New TrCertificateLineFacade(User).Insert(objTrCertificateLine)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                    ClearDataAfterSave()
                End If
            Else
                MessageBox.Show(SR.DataIsExist("Data Nilai Untuk Ujian dan No. Registrasi Tersebut"))
            End If
        Else
            MessageBox.Show(SR.GridIsEmpty("Kode Kelas dan No. Registrasi"))
        End If

    End Sub

    Private Sub btnClear3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear3.Click
        txtEvalCode.Text = String.Empty
        txtJenisEval.Text = String.Empty
    End Sub

    Private Sub btnClear2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear2.Click
        txtRegNo.Text = String.Empty
        txtTrainee.Text = String.Empty
        txtEvalCode.Text = String.Empty
        txtJenisEval.Text = String.Empty
    End Sub

    Private Sub btnClear1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear1.Click
        txtClassCode.Text = String.Empty
        txtClassName.Text = String.Empty
        txtRegNo.Text = String.Empty
        txtTrainee.Text = String.Empty
        txtEvalCode.Text = String.Empty
        txtJenisEval.Text = String.Empty
    End Sub

    Private Sub dtgCertificateLine_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgCertificateLine.SelectedIndexChanged

    End Sub
End Class
