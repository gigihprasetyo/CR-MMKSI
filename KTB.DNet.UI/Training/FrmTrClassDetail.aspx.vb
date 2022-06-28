#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
#End Region

Public Class FrmTrClassDetail
    Inherits System.Web.UI.Page
    Dim sHClass As SessionHelper = New SessionHelper
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtPengajar3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents RangeValidator1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtPengajar2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLokasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNamaKelas As System.Web.UI.WebControls.TextBox
    Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents dgNumEval As System.Web.UI.WebControls.DataGrid
    Dim objClass As TrClass

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeKelas As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtPengajar1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKeterangan As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtKapasitas As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeKategori As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpCourse As System.Web.UI.WebControls.Label
    Protected WithEvents Requiredfieldvalidator8 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ICTanggalMulai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents ICTanggalSelesai As KTB.DNet.WebCC.IntiCalendar

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'clearData()
        getDataClass()
        If Not IsPostBack Then
            BindddlClass()
            BindDdlCategory()
            LoadData()
        End If
        lblPopUpCourse.Attributes("onclick") = "ShowPPCourseSelection();"
    End Sub
    Private Sub BindDdlCategory()
        ddlCategory.Items.Clear()
        ddlCategory.DataSource = EnumTrClass.RetrieveEnumTrClass()
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataTextField = "Name"
        ddlCategory.DataBind()
    End Sub
    Public Sub clearData()
        txtKodeKategori.Text = String.Empty
        txtKapasitas.Text = String.Empty
        txtKeterangan.Text = String.Empty
        txtKodeKategori.Text = String.Empty
        txtKodeKelas.Text = String.Empty
        txtLokasi.Text = String.Empty
        txtNamaKelas.Text = String.Empty
        txtPengajar1.Text = String.Empty
        txtPengajar2.Text = String.Empty
        txtPengajar3.Text = String.Empty

    End Sub
    Private Sub BindddlClass()
        ddlStatus.Items.Clear()
        Dim obj As New EnumTrDataStatus
        Dim arlStatusReg As ArrayList = obj.Retrieve()
        For Each en As EnumDataStatus In arlStatusReg
            Dim lItem As ListItem = New ListItem(en.NameType, en.ValueType.ToString())
            ddlStatus.Items.Add(lItem)
        Next
    End Sub
    Public Sub getDataClass()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not sHClass.GetSession("editID") Is Nothing Then
            objClass = New TrClassFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(sHClass.GetSession("editID"))
            RequiredFieldValidator1.Visible = False
            btnBatal.Visible = True
            btnSimpan.Visible = True

        End If

        If Not sHClass.GetSession("viewID") Is Nothing Then
            objClass = New TrClassFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(sHClass.GetSession("viewID"))
            RequiredFieldValidator1.Visible = False
        End If

        If (Not objClass Is Nothing) Then
            sHClass.SetSession("SESSION_CLASSID", objClass.ID)
        End If

    End Sub
    Public Sub visibleLblTxt()
        'visibleClassLbl()
        hideText()
    End Sub
    Public Sub visibleClassLbl()
        txtKodeKelas.Visible = False
        txtNamaKelas.Visible = False
        txtLokasi.Visible = False
        txtKapasitas.Visible = False
        txtPengajar1.Visible = False
        txtPengajar2.Visible = False
        txtPengajar3.Visible = False
        txtKodeKategori.Visible = False
        txtKeterangan.Visible = False
        ddlStatus.Visible = False
        'ddlCategory.Visible = False
        ICTanggalMulai.Visible = False
        ICTanggalSelesai.Visible = False
        btnBatal.Visible = False
        btnSimpan.Visible = False
        lblPopUpCourse.Visible = False
    End Sub
    Public Sub hideText()
        txtKodeKelas.ReadOnly = True
        txtNamaKelas.ReadOnly = True
        txtLokasi.ReadOnly = True
        txtKapasitas.ReadOnly = True
        txtPengajar1.ReadOnly = True
        txtPengajar2.ReadOnly = True
        txtPengajar3.ReadOnly = True
        txtKodeKategori.ReadOnly = True
        txtKeterangan.ReadOnly = True
        'ddlStatus.ReadOnly = True
        'ICTanggalMulai.Controls.IsReadOnly = True
        'ICTanggalSelesai.Controls.IsReadOnly = True
        btnBatal.Visible = False
        btnSimpan.Visible = False
        lblPopUpCourse.Visible = False
    End Sub
    Private Sub LoadData()
        viewEditClass()
    End Sub
    Public Sub viewEditClass()
        If Not IsNothing(objClass) Then
            txtKodeKelas.Text = objClass.ClassCode.Trim()
            txtNamaKelas.Text = objClass.ClassName.Trim()
            txtLokasi.Text = objClass.Location.Trim()
            txtKapasitas.Text = objClass.Capacity.ToString()
            txtPengajar1.Text = objClass.Trainer1.Trim()
            txtPengajar2.Text = objClass.Trainer2.Trim()
            txtPengajar3.Text = objClass.Trainer3.Trim()
            txtKodeKategori.Text = objClass.TrCourse.CourseCode.Trim()
            txtKeterangan.Text = objClass.Description.Trim()
            ICTanggalMulai.Value = objClass.StartDate.ToString("dd/MM/yyyy").Trim()
            ICTanggalSelesai.Value = objClass.FinishDate.ToString("dd/MM/yyyy").Trim()
            If Not sHClass.GetSession("viewID") Is Nothing Then
                hideText()
                ddlStatus.Visible = False
                'ddlCategory.Visible = False
                lblStatus.Visible = True
                'lblCategory.Visible = True
                btnBatal.Visible = False
                btnSimpan.Visible = False
                lblPopUpCourse.Visible = False
                Dim obj As New EnumTrDataStatus
                If objClass.Status.ToString() = "0" Then
                    lblStatus.Text = obj.StatusByIndex(CInt(objClass.Status))
                Else
                    lblStatus.Text = obj.StatusByIndex(CInt(objClass.Status))
                End If

                If objClass.Category >= 0 Then
                    lblCategory.Text = EnumTrClass.RetrieveTrClassCategory(objClass.Category)
                End If
            Else
                ddlStatus.Visible = True
                lblStatus.Visible = False
                btnBatal.Visible = True
                btnSimpan.Visible = True
                lblPopUpCourse.Visible = True
                If objClass.Status.ToString() = "0" Then
                    ddlStatus.SelectedValue = CType(EnumTrDataStatus.DataStatusType.Deactive, Short)
                Else
                    ddlStatus.SelectedValue = CType(EnumTrDataStatus.DataStatusType.Active, Short)
                End If

                If objClass.Category >= 0 Then
                    ddlCategory.SelectedValue = CType(objClass.Category, Short)
                End If

            End If

        End If
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        If Not IsUnhack() Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return
        End If
        If ICTanggalSelesai.Value.Subtract(ICTanggalMulai.Value).Days < 0 Then
            MessageBox.Show("Tanggal mulai tidak boleh lebih besar daripada tanggal selesai")
            Return
        End If
        save()
    End Sub
    Private Sub save()
        Dim objTrClassFacade As New TrClassFacade(User)
        Dim nResult = -1

        If sHClass.GetSession("editID") Is Nothing Then
            If objTrClassFacade.ValidateCode(Me.txtKodeKelas.Text.Trim) = 0 Then
                insertTrClass(objClass, nResult)
                sHClass.SetSession("SESSION_CLASSID", nResult)
                
            Else
                MessageBox.Show(SR.DataIsExist("Kelas"))
            End If
        Else

            updateTrClass(nResult)
        End If

        If Not nResult = -1 Then
            sHClass.SetSession("backRes", 1)
        End If

        'InsertDataClassNumEval()

    End Sub
    Private Function IsTraineeRegistered(ByVal traineeID As Integer, ByVal objClass As TrClass, ByVal objOrgClass As TrClass) As Boolean
        For Each item As TrClassRegistration In objClass.TrClassRegistrations
            If objClass.ID = objOrgClass.ID Then
                Return False
            End If
            If item.TrTrainee.ID = traineeID Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function IsRangeDateOverlap(ByVal objTrClass As TrClass) As String
        Dim _result As String = String.Empty
        Dim sDate As Date = New Date(objTrClass.StartDate.Year, objTrClass.StartDate.Month, objTrClass.StartDate.Day, 0, 0, 0)
        Dim eDate As Date = New Date(objTrClass.FinishDate.Year, objTrClass.FinishDate.Month, objTrClass.FinishDate.Day, 23, 59, 59)
        Dim trClassFacade As trClassFacade = New trClassFacade(User)
        Dim trClassRegCollection As ArrayList = objTrClass.TrClassRegistrations
        Dim ClassCollection As ArrayList = trClassFacade.RetrieveActiveList()
        If ClassCollection.Count > 0 Then
            For Each item As TrClass In ClassCollection
                If (item.FinishDate >= sDate And item.FinishDate <= eDate) Or (item.StartDate >= sDate And item.StartDate <= eDate) Or (item.StartDate >= sDate And item.FinishDate <= eDate) Or (item.StartDate <= sDate And item.FinishDate >= eDate) Then
                    For Each objClassReg As TrClassRegistration In trClassRegCollection
                        If IsTraineeRegistered(objClassReg.TrTrainee.ID, item, objTrClass) = True Then
                            _result += objClassReg.TrTrainee.Name & " terdaftar di " & item.ClassCode & ","
                        End If
                    Next
                End If
            Next
        End If
        If _result = String.Empty Then
            Return _result
        Else
            Return _result.Remove(_result.Length - 1, 1)
        End If
    End Function
    Private Sub updateTrClass(ByRef nResult As Integer)
        Dim objTrCourse As New TrCourse
        txtKodeKelas.ReadOnly = True
        With objClass
            .ClassCode = txtKodeKelas.Text.Trim()
            .ClassName = txtNamaKelas.Text.Trim()

            objTrCourse = getCourseID(txtKodeKategori.Text.Trim())
            If Not IsNothing(objTrCourse) Then
                .TrCourse = objTrCourse
            Else
                MessageBox.Show(SR.DataNotFound("Kode Kategory"))
                Exit Sub
            End If
            .Status = ddlStatus.SelectedIndex
            .Category = ddlCategory.SelectedValue
            .Location = txtLokasi.Text.Trim()
            .Trainer1 = txtPengajar1.Text.Trim()
            .Trainer2 = txtPengajar2.Text.Trim()
            .Trainer3 = txtPengajar3.Text.Trim()
            .StartDate = ICTanggalMulai.Value.ToString.Trim()
            .FinishDate = ICTanggalSelesai.Value.ToString.Trim()
            .Capacity = txtKapasitas.Text.Trim()
            .Description = txtKeterangan.Text.Trim()
        End With
        Dim collClass As String = IsRangeDateOverlap(objClass)
        If collClass = String.Empty Then
            nResult = New TrClassFacade(User).Update(objClass)
        Else
            MessageBox.Show(collClass)
            Return
        End If

        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
            txtKodeKelas.Visible = True
            RequiredFieldValidator1.Visible = True
            
        End If

    End Sub
   
    Private Sub insertTrClass(ByVal objTrClass As TrClass, ByRef nResult As Integer)
        objTrClass = New TrClass
        Dim objTrCourse As New TrCourse
        Me.txtKodeKelas.Enabled = True
        If ICTanggalMulai.Value < Today Then
            MessageBox.Show("Tanggal mulai tidak boleh lebih kecil dari tanggal hari ini")
            Return
        ElseIf ICTanggalSelesai.Value < Today Then
            MessageBox.Show("Tanggal selesai tidak boleh lebih kecil dari tanggal hari ini")
            Return
        End If
        With objTrClass
            .ClassCode = txtKodeKelas.Text.Trim()
            .ClassName = txtNamaKelas.Text.Trim()
            objTrCourse = getCourseID(txtKodeKategori.Text.Trim())

            If Not IsNothing(objTrCourse) Then
                .TrCourse = objTrCourse
            Else
                MessageBox.Show(SR.DataNotFound("Kode Kategory"))
                Exit Sub
            End If
            .Status = ddlStatus.SelectedIndex
            .Category = ddlCategory.SelectedValue
            .Location = txtLokasi.Text.Trim()
            .Trainer1 = txtPengajar1.Text.Trim()
            .Trainer2 = txtPengajar2.Text.Trim()
            .Trainer3 = txtPengajar3.Text.Trim()
            .StartDate = ICTanggalMulai.Value.ToString.Trim()
            .FinishDate = ICTanggalSelesai.Value.ToString.Trim()
            .Capacity = txtKapasitas.Text.Trim()
            .Description = txtKeterangan.Text.Trim()
        End With

        nResult = New TrClassFacade(User).InsertTransaction(objTrClass)
        sHClass.SetSession("SESSION_CLASSID", nResult)
        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            Try
                InsertDataClassNumEval()
                MessageBox.Show(SR.SaveSuccess)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

    End Sub
    Private Function IsUnhack() As Boolean

        If txtKapasitas.Text.IndexOf("<") >= 0 Or txtKapasitas.Text.IndexOf(">") >= 0 Or txtKapasitas.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtKeterangan.Text.IndexOf("<") >= 0 Or txtKeterangan.Text.IndexOf(">") >= 0 Or txtKeterangan.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtKodeKategori.Text.IndexOf("<") >= 0 Or txtKodeKategori.Text.IndexOf(">") >= 0 Or txtKodeKategori.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtKodeKelas.Text.IndexOf("<") >= 0 Or txtKodeKelas.Text.IndexOf(">") >= 0 Or txtKodeKelas.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtLokasi.Text.IndexOf("<") >= 0 Or txtLokasi.Text.IndexOf(">") >= 0 Or txtLokasi.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtNamaKelas.Text.IndexOf("<") >= 0 Or txtNamaKelas.Text.IndexOf(">") >= 0 Or txtNamaKelas.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtPengajar1.Text.IndexOf("<") >= 0 Or txtPengajar1.Text.IndexOf(">") >= 0 Or txtPengajar1.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtPengajar2.Text.IndexOf("<") >= 0 Or txtPengajar2.Text.IndexOf(">") >= 0 Or txtPengajar2.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        If txtPengajar3.Text.IndexOf("<") >= 0 Or txtPengajar3.Text.IndexOf(">") >= 0 Or txtPengajar3.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        Return True
    End Function
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        If Not sHClass.GetSession("editID") Is Nothing Then
            viewEditClass()
        End If
        If sHClass.GetSession("clear") = 1 And (sHClass.GetSession("editID") Is Nothing) Then
            txtKapasitas.Text = String.Empty
            txtKeterangan.Text = String.Empty
            txtKodeKategori.Text = String.Empty
            txtKodeKelas.Text = String.Empty
            txtLokasi.Text = String.Empty
            txtNamaKelas.Text = String.Empty
            txtPengajar1.Text = String.Empty
            txtPengajar2.Text = String.Empty
            txtPengajar3.Text = String.Empty
            ICTanggalMulai.Value = Today
            ICTanggalSelesai.Value = Today
            'sHClass.RemoveSession("clear")
        End If
       
    End Sub
    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        sHClass.RemoveSession("viewID")
        sHClass.RemoveSession("editID")
        sHClass.RemoveSession("SESSION_CLASSID")
        sHClass.SetSession("backRes", 1)
        Response.Redirect("FrmTrClass.aspx")
    End Sub

    Private Function getCourseID(ByVal courseCode As String) As TrCourse
        Dim objTrCourseAl As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourse), "CourseCode", MatchType.Exact, courseCode))

        objTrCourseAl = New TrCourseFacade(User).Retrieve(criterias)

        If Not objTrCourseAl.Count <= 0 Then
            Return CType(objTrCourseAl.Item(0), TrCourse)
        Else
            Return Nothing
        End If
    End Function

#Region "CourseEvaluationHandler"

    Private Function GetCourseEvaluation() As ArrayList
        Dim objTrCourse As New TrCourse
        objTrCourse = getCourseID(txtKodeKategori.Text.Trim())
        If (Not objTrCourse Is Nothing) Then
            Return BindDataCourseEvaluation(objTrCourse.ID)
        Else
            Return Nothing
        End If
    End Function

    Private Function BindDataCourseEvaluation(ByVal _CourseId As Integer) As ArrayList
        Dim _trCourseEvaluationFacade As New TrCourseEvaluationFacade(User)
        Dim _arrayCourseEvaluation As New ArrayList
        Dim crtCourseEvalParam As CriteriaComposite

        Dim srtParam As SortCollection = New SortCollection

        crtCourseEvalParam = New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "TrCourse", MatchType.Exact, _CourseId))
        'crtCourseEvalParam.opAnd(New Criteria(GetType(TrCourseEvaluation), "Type", MatchType.Exact, 0), "(", True)
        'crtCourseEvalParam.opOr(New Criteria(GetType(TrCourseEvaluation), "Type", MatchType.Exact, 1), ")", False)

        srtParam.Add(New Sort(GetType(TrCourseEvaluation), "Type", Sort.SortDirection.ASC))
        srtParam.Add(New Sort(GetType(TrCourseEvaluation), "EvaluationCode", Sort.SortDirection.ASC))

        _arrayCourseEvaluation = _trCourseEvaluationFacade.Retrieve(crtCourseEvalParam, srtParam)

        Return _arrayCourseEvaluation
    End Function

    Sub InsertDataClassNumEval()

        'Delete All First
        DeleteAllClassNumEval()

        Dim _trClassNumEvalFacade As TrClassNumEvaluationFacade = New TrClassNumEvaluationFacade(User)
        Dim _trCourseEvalFacade As TrCourseEvaluationFacade = New TrCourseEvaluationFacade(User)
        Dim _trClassFacade As TrClassFacade = New TrClassFacade(User)

        Dim _trClassNumEval As TrClassNumEvaluation = New TrClassNumEvaluation
        Dim _trCourseEvaluation As TrCourseEvaluation = New TrCourseEvaluation
        Dim _trClass As TrClass = _trClassFacade.Retrieve(CType(sHClass.GetSession("SESSION_CLASSID"), Integer))

        Dim _arrayCourseEvaluation As New ArrayList
        _arrayCourseEvaluation = GetCourseEvaluation()

        If Not (_arrayCourseEvaluation Is Nothing) Then
            If (_arrayCourseEvaluation.Count > 0) Then

                For i As Integer = 0 To _arrayCourseEvaluation.Count - 1
                    'Insert Data
                    With _trClassNumEval
                        _trCourseEvaluation = CType(_arrayCourseEvaluation(i), TrCourseEvaluation)
                        .TrCourseEvaluation = _trCourseEvaluation
                        .TrClass = _trClass
                        .SpecialName = KTB.DNet.Utility.CommonFunction.GetNamaKhususEvalTraining(_trClass.ID.ToString(), _trCourseEvaluation.ID)
                    End With
                    _trClassNumEvalFacade.Insert(_trClassNumEval)

                    'Change Here To Insert New Data For Sikap
                Next
            End If
        Else
            Throw New ApplicationException("Kategori Training Tidak Ada")
        End If
        
        
    End Sub

    Private Sub DeleteAllClassNumEval()
        Dim objRetVal As New TrClassNumEvaluation
        Dim _trClassNumEvalFacade As TrClassNumEvaluationFacade = New TrClassNumEvaluationFacade(User)
        Dim _lst As New ArrayList
        Dim crtParam As CriteriaComposite
        Dim _classId As Integer = Val(sHClass.GetSession("SESSION_CLASSID"))

        crtParam = New CriteriaComposite(New Criteria(GetType(TrClassNumEvaluation), "TrClass.ID", MatchType.Exact, _classId))
        _lst = _trClassNumEvalFacade.Retrieve(crtParam)

        For Each item As TrClassNumEvaluation In _lst
            _trClassNumEvalFacade.DeleteFromDB(item)
        Next

    End Sub

#End Region

End Class

