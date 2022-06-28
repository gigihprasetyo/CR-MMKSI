Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports System.Drawing.Color
Imports System.Web.UI.WebControls.BorderStyle
Imports KTB.DNet.Security


Public Class FrmTrCertificateLine2
    Inherits System.Web.UI.Page
    'Private m_bFormPrivilege As Boolean = False

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents txtClassCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtClassName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpClass As System.Web.UI.WebControls.Label
    Protected WithEvents txtRegNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtTrainee As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpTrainee As System.Web.UI.WebControls.Label
    Protected WithEvents btnRefresh As System.Web.UI.WebControls.Button
    Protected WithEvents dtgCertificateLine As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnInsert As System.Web.UI.WebControls.Button
    Private _sessHelper As SessionHelper = New SessionHelper

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.TrainingInputDataNilai_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=TRAINING - Input Data Nilai")
        End If
    End Sub

    Private Sub InitiatePage()
        AssignAttributeControl()
        ClearData()
        ViewState("CurrentSortColumn") = "Type"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub AssignAttributeControl()
        Dim lblPopUpClass As Label = CType(Page.FindControl("lblPopUpClass"), Label)
        lblPopUpClass.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpClassSelection.aspx", "", 500, 760, "ClassSelection")
        Dim lblPopUpTrainee As Label = CType(Page.FindControl("lblPopUpTrainee"), Label)
        lblPopUpTrainee.Attributes.Add("onclick", " return ShowPopupTraineeSelection()")
    End Sub

    Private Sub ClearData()
        txtClassCode.Text = String.Empty
        txtClassName.Text = String.Empty
        txtRegNo.Text = String.Empty
        txtTrainee.Text = String.Empty

        lblPopUpClass.Enabled = True
        lblPopUpTrainee.Enabled = True

        lblPopUpClass.ToolTip = "Klik Popup"
        lblPopUpTrainee.ToolTip = "Klik Popup"

        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)

        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If txtClassCode.Text <> "" Then
                Dim critClass As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassCode", MatchType.Exact, Trim(txtClassCode.Text)))
                Dim arrClass As ArrayList = New TrClassFacade(User).Retrieve(critClass)
                If arrClass.Count > 0 Then
                    Dim objClass As TrClass = arrClass(0)
                    If Not IsNothing(objClass) Then
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, objClass.TrCourse.ID))

                        'Ambil courseevaluation yang dipilih pada trnumclassevaluation
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "Type", MatchType.Exact, 2), "(", True)
                        criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "Type", MatchType.No, 2), "(", True)
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "ID", MatchType.InSet, ("(select CourseEvaluationID from trclassnumevaluation where ClassID=" & objClass.ID & ")")), "))", False)
                    End If
                End If
            End If


            dtgCertificateLine.DataSource = New TrCourseEvaluationFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgCertificateLine.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            _sessHelper.SetSession("sessArrTrCourseEvaluation", dtgCertificateLine.DataSource)
            dtgCertificateLine.VirtualItemCount = totalRow
            ViewState("EmptyItem") = "0"
            dtgCertificateLine.DataBind()
            If ViewState("EmptyItem") = "1" Then
                btnInsert.Enabled = True
            Else
                btnInsert.Enabled = False
            End If
            If dtgCertificateLine.Items.Count > 0 Then
                btnInsert.Enabled = True
            Else
                btnInsert.Enabled = False
            End If

        End If
    End Sub

    Private Sub dtgCertificateLine_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCertificateLine.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            If BoundRowItems(e) Then ViewState("EmptyItem") = "1"
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCertificateLine.CurrentPageIndex * dtgCertificateLine.PageSize)
        End If

        Dim _trClassFacade As New Training.TrClassFacade(User)
        Dim _trClass As New TrClass

        _trClass = _trClassFacade.Retrieve(Me.txtClassCode.Text.Trim())

        Dim _labelEvaluationName As Label = CType(e.Item.FindControl("LabelEvaluationName"), Label)
        If (Not _labelEvaluationName Is Nothing) Then
            _labelEvaluationName.Text = KTB.DNet.Utility.CommonFunction.GetNamaKhususEvalTraining(_trClass.ID.ToString(), CType(e.Item.Cells(0).Text(), Integer))
        End If

    End Sub

    Private Function BoundRowItems(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) As Boolean
        Dim bReturn As Boolean = True
        Dim objTrCourseEvaluation As TrCourseEvaluation = CType(CType(dtgCertificateLine.DataSource, ArrayList)(e.Item.ItemIndex), TrCourseEvaluation)
        Dim ArrTrCertificateLine As ArrayList = New ArrayList

        If Not IsNothing(CType(_sessHelper.GetSession("sessCertificateLineColl"), ArrayList)) Then
            ArrTrCertificateLine = CType(_sessHelper.GetSession("sessCertificateLineColl"), ArrayList)
        Else
            ArrTrCertificateLine = GetCertificateLine()
        End If


        If Not IsNothing(objTrCourseEvaluation) Then


            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                Dim lblJenisTest As Label = CType(e.Item.FindControl("lblJenisTest"), Label)
                lblJenisTest.Text = StrJenisTestName(objTrCourseEvaluation)
                If objTrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) Then
                    CType(e.Item.FindControl("txtAngka"), TextBox).Enabled = True
                    CType(e.Item.FindControl("txtSikap"), TextBox).Enabled = False
                    CType(e.Item.FindControl("txtPrestasi"), TextBox).Enabled = False
                    CType(e.Item.FindControl("txtSikap"), TextBox).BackColor = LightGray
                    CType(e.Item.FindControl("txtPrestasi"), TextBox).BackColor = LightGray
                ElseIf objTrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Sikap, String) Then
                    CType(e.Item.FindControl("txtAngka"), TextBox).Enabled = False
                    CType(e.Item.FindControl("txtSikap"), TextBox).Enabled = True
                    CType(e.Item.FindControl("txtPrestasi"), TextBox).Enabled = False
                    CType(e.Item.FindControl("txtAngka"), TextBox).BackColor = LightGray
                    CType(e.Item.FindControl("txtPrestasi"), TextBox).BackColor = LightGray
                ElseIf objTrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Prestasi, String) Then
                    CType(e.Item.FindControl("txtAngka"), TextBox).Enabled = False
                    CType(e.Item.FindControl("txtSikap"), TextBox).Enabled = False
                    CType(e.Item.FindControl("txtPrestasi"), TextBox).Enabled = True
                    CType(e.Item.FindControl("txtAngka"), TextBox).BackColor = LightGray
                    CType(e.Item.FindControl("txtSikap"), TextBox).BackColor = LightGray
                End If

                If Trim(txtRegNo.Text) <> "" Then
                    If Not IsNothing(ArrTrCertificateLine) Then
                        If ArrTrCertificateLine.Count > 0 Then
                            For Each objCertificateLine As TrCertificateLine In ArrTrCertificateLine
                                If objCertificateLine.TrCourseEvaluation.EvaluationCode = objTrCourseEvaluation.EvaluationCode And objCertificateLine.TrClassRegistration.ID = Trim(txtRegNo.Text) Then

                                    If objCertificateLine.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Angka Then
                                        CType(e.Item.FindControl("txtAngka"), TextBox).Text = CType(objCertificateLine.NumTestResult, String)
                                        'CType(e.Item.FindControl("txtAngka"), TextBox).ReadOnly = True
                                        CType(e.Item.FindControl("txtAngka"), TextBox).BackColor = MistyRose
                                    ElseIf objCertificateLine.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Sikap Then
                                        CType(e.Item.FindControl("txtSikap"), TextBox).Text = CType(objCertificateLine.CharTestResult, String)
                                        'CType(e.Item.FindControl("txtSikap"), TextBox).ReadOnly = True
                                        CType(e.Item.FindControl("txtSikap"), TextBox).BackColor = MistyRose
                                    ElseIf objCertificateLine.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Prestasi Then
                                        CType(e.Item.FindControl("txtPrestasi"), TextBox).Text = CType(objCertificateLine.CharTestResult, String)
                                        'CType(e.Item.FindControl("txtPrestasi"), TextBox).ReadOnly = True
                                        CType(e.Item.FindControl("txtPrestasi"), TextBox).BackColor = MistyRose
                                    End If

                                    If (CType(e.Item.FindControl("txtAngka"), TextBox).Text = "") Or _
                                        (CType(e.Item.FindControl("txtSikap"), TextBox).Text = "") Or _
                                        (CType(e.Item.FindControl("txtPrestasi"), TextBox).Text = "") Then
                                        bReturn = False
                                    End If

                                End If
                            Next
                        End If

                    End If
                End If

            End If
        End If
        'End If
        Return bReturn
    End Function

    Private Sub dtgCourseEvaluation_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dtgCertificateLine.SelectedIndex = -1
        dtgCertificateLine.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgCertificateLine.CurrentPageIndex)
    End Sub
    Private Sub dtgCourseEvaluation_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCertificateLine.SortCommand
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
        BindDatagrid(dtgCertificateLine.CurrentPageIndex)
    End Sub


    Private Function GetTrClassObject() As TrClass
        Dim bCheck As Boolean = False
        Dim objClass As TrClass = New TrClass
        If txtClassCode.Text <> "" Then
            Dim critClass As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassCode", MatchType.Exact, Trim(txtClassCode.Text)))
            Dim arrClass As ArrayList = New TrClassFacade(User).Retrieve(critClass)
            If arrClass.Count > 0 Then
                objClass = arrClass(0)
                If Not IsNothing(objClass) Then
                    bCheck = True
                End If
            End If
        End If
        If bCheck Then Return objClass Else Return Nothing
    End Function

    Private Function GetCertificateLine() As ArrayList
        Dim ObjTrClass As TrClass = New TrClass
        ObjTrClass = GetTrClassObject()
        If Not IsNothing(ObjTrClass) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.TrClass.ID", MatchType.Exact, ObjTrClass.ID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.ID", MatchType.Exact, Trim(txtRegNo.Text)))
            Dim ArryListCertificateLine As ArrayList = New TrCertificateLineFacade(User).Retrieve(criterias)
            If ArryListCertificateLine.Count > 0 Then
                Return ArryListCertificateLine
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
    End Function

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        dtgCertificateLine.CurrentPageIndex = 0
        Dim bTestNull As Boolean = False
        Dim nMessage As Integer = 0

        If Trim(txtRegNo.Text) = "&nbsp" Then
            txtRegNo.Text = String.Empty
        End If

        If txtClassCode.Text.Trim = "" Then
            bTestNull = True
            nMessage = 1

        ElseIf Trim(txtRegNo.Text) = "" Then
            bTestNull = True
            nMessage = 2
        End If

        If Not bTestNull Then
            Dim TrCertificateLineColl As ArrayList = New ArrayList

            If Not IsNumeric(txtRegNo.Text) Then
                MessageBox.Show("No. Registrasi harus angka !")
                txtTrainee.Text = String.Empty
                Exit Sub
            End If

            Dim critClass As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassCode", MatchType.Exact, txtClassCode.Text.Trim))
            Dim TrClassColl As ArrayList = New TrClassFacade(User).Retrieve(critClass)
            If TrClassColl.Count > 0 Then
                Dim objTrClass As TrClass = CType(TrClassColl(0), TrClass)
                If objTrClass.StartDate > Today Then
                    MessageBox.Show("Kelas belum dimulai, tidak dapat memasukan atau melihat nilai !")
                    dtgCertificateLine.DataSource = Nothing
                    dtgCertificateLine.DataBind()
                    Exit Sub
                End If
            Else
                MessageBox.Show("Kelas tidak ada")
                dtgCertificateLine.DataSource = Nothing
                dtgCertificateLine.DataBind()
                Exit Sub
            End If

            TrCertificateLineColl = GetCertificateLine()

            If Not (TrCertificateLineColl) Is Nothing Then
                _sessHelper.SetSession("sessCertificateLineColl", TrCertificateLineColl)
                BindDatagrid(0)
            Else
                'belum punya nilai, periksa apakah terdaftar ?
                'If IsNumeric(txtRegNo.Text) Then
                Dim crit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "ID", MatchType.Exact, Trim(txtRegNo.Text)))
                Dim chekClassRegExist As ArrayList = New TrClassRegistrationFacade(User).Retrieve(crit)
                If chekClassRegExist.Count > 0 Then
                    BindDatagrid(0)
                Else
                    MessageBox.Show("Nomor Registrasi tidak terdaftar")
                    dtgCertificateLine.DataSource = Nothing
                    dtgCertificateLine.DataBind()
                End If
                'Else
                'MessageBox.Show("No. Registrasi harus angka !")
                'txtTrainee.Text = String.Empty
                'End If
            End If
        Else
            If nMessage = 1 Then
                MessageBox.Show("Kode kelas masih kosong")
                dtgCertificateLine.DataSource = Nothing
                dtgCertificateLine.DataBind()
            ElseIf nMessage = 2 Then
                MessageBox.Show("Nomor Registrasi masih kosong atau peserta tidak memiliki no registrasi")
                dtgCertificateLine.DataSource = Nothing
                dtgCertificateLine.DataBind()
            End If

        End If
    End Sub

    Private Sub btnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Dim bcheck As Boolean = True
        Dim nCheck As Integer = 0
        Dim nSuccessInsert As Integer = 0
        Dim nSuccessUpdate As Integer = 0
        Dim bSuccess As Boolean = True
        Dim nRowInsert As Integer = 0
        Dim nRowUpdate As Integer = 0
        Dim ObjCertificateLineForInsert As TrCertificateLine
        Dim ObjCertificateLineForUpdate As TrCertificateLine
        Dim ArrayCertificateLineForInsert As ArrayList = New ArrayList
        Dim ArrayCertificateLineForUpdate As ArrayList = New ArrayList
        Dim ArrTrCourseEvalTmp As ArrayList = CType(Session.Item("sessArrTrCourseEvaluation"), ArrayList)
        Dim nIndeks As Integer


        'tentukan kelas training dulu
        Dim ObjTrClassRegistration As TrClassRegistration
        Dim critTrClassReg As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critTrClassReg.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "ID", MatchType.Exact, Trim(txtRegNo.Text)))
        Dim arrTrClassReg As ArrayList = New TrClassRegistrationFacade(User).Retrieve(critTrClassReg)
        If arrTrClassReg.Count > 0 Then
            ObjTrClassRegistration = CType(arrTrClassReg(0), TrClassRegistration)
        Else
            bcheck = False
            nCheck = 1
        End If

        If bcheck Then
            'ambil jenis-jenis ujiannya
            For Each dtgItem As DataGridItem In dtgCertificateLine.Items
                Dim TxtAngka As TextBox = CType(dtgItem.FindControl("txtAngka"), TextBox)
                nIndeks = dtgItem.ItemIndex + (dtgCertificateLine.CurrentPageIndex * dtgCertificateLine.PageSize)
                Dim objTrCourseEval As TrCourseEvaluation = CType(ArrTrCourseEvalTmp(nIndeks), TrCourseEvaluation)
                Dim objTrCertificateLine As TrCertificateLine = New TrCertificateLineFacade(User).RetrieveTrCertificateLine2(objTrCourseEval.ID, Trim(txtRegNo.Text))
                If IsNothing(objTrCertificateLine) Then
                    'insert

                    ObjCertificateLineForInsert = New TrCertificateLine
                    ObjCertificateLineForInsert.TrCourseEvaluation = objTrCourseEval
                    ObjCertificateLineForInsert.TrClassRegistration = ObjTrClassRegistration

                    If (ObjCertificateLineForInsert.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Angka) And (TxtAngka.Text <> "") Or _
                       (ObjCertificateLineForInsert.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Sikap) And (CType(dtgItem.FindControl("txtSikap"), TextBox).Text <> "") Or _
                       (ObjCertificateLineForInsert.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Prestasi) And (CType(dtgItem.FindControl("txtPrestasi"), TextBox).Text <> "") Then

                        If ObjCertificateLineForInsert.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Angka Then

                            If IsNumeric(TxtAngka.Text) Then
                                If CType(TxtAngka.Text, Decimal) > 100 Or CType(TxtAngka.Text, Decimal) < 0 Then
                                    CType(dtgItem.FindControl("lblMsg"), Label).Text = " X"
                                    nCheck = 2
                                    bcheck = False
                                Else
                                    CType(dtgItem.FindControl("lblMsg"), Label).Text = ""
                                    If ObjCertificateLineForInsert.NumTestResult <> CType(TxtAngka.Text, Decimal) Then
                                        ObjCertificateLineForInsert.RowStatus = 0
                                        ObjCertificateLineForInsert.NumTestResult = CType(TxtAngka.Text, Decimal)
                                        ArrayCertificateLineForInsert.Add(ObjCertificateLineForInsert)
                                    End If

                                End If
                            Else
                                CType(dtgItem.FindControl("lblMsg"), Label).Text = " X"
                                nCheck = 3
                                bcheck = False

                            End If

                            'If CType(TxtAngka.Text, Decimal) > 100 Or CType(TxtAngka.Text, Decimal) < 0 Then
                            '    bcheck = False
                            '    CType(dtgItem.FindControl("lblMsg"), Label).Text = " X"
                            '    nCheck = 2
                            '    Exit For
                            'Else
                            '    CType(dtgItem.FindControl("lblMsg"), Label).Text = ""
                            '    objTrCertificateLine.NumTestResult = CType(TxtAngka.Text, Decimal)
                            'End If



                        ElseIf ObjCertificateLineForInsert.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Sikap Then
                            If ValidateNilaiSikapAndPrestasi(dtgItem, "txtSikap") Then
                                CType(dtgItem.FindControl("lblIndikator"), Label).Text = ""
                                If ObjCertificateLineForInsert.CharTestResult <> CType(CType(dtgItem.FindControl("txtSikap"), TextBox).Text, String) Then
                                    ObjCertificateLineForInsert.RowStatus = 0
                                    ObjCertificateLineForInsert.CharTestResult = CType(CType(dtgItem.FindControl("txtSikap"), TextBox).Text, String)
                                    ArrayCertificateLineForInsert.Add(ObjCertificateLineForInsert)
                                End If
                            Else
                                bcheck = False
                                CType(dtgItem.FindControl("lblIndikator"), Label).Text = " X"
                                nCheck = 4
                                Exit For
                            End If

                        ElseIf ObjCertificateLineForInsert.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Prestasi Then
                            If ValidateNilaiSikapAndPrestasi(dtgItem, "txtPrestasi") Then
                                CType(dtgItem.FindControl("lblIndikator2"), Label).Text = ""
                                If ObjCertificateLineForInsert.CharTestResult <> CType(CType(dtgItem.FindControl("txtPrestasi"), TextBox).Text, String) Then
                                    ObjCertificateLineForInsert.RowStatus = 0
                                    ObjCertificateLineForInsert.CharTestResult = CType(CType(dtgItem.FindControl("txtPrestasi"), TextBox).Text, String)
                                    ArrayCertificateLineForInsert.Add(ObjCertificateLineForInsert)
                                End If

                            Else
                                bcheck = False
                                CType(dtgItem.FindControl("lblIndikator2"), Label).Text = " X"
                                nCheck = 5
                                Exit For
                            End If
                        End If

                        'If (ObjCertificateLineForInsert.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Angka) And (ObjCertificateLineForInsert.NumTestResult >= 0D) Or _
                        '                   (ObjCertificateLineForInsert.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Sikap) And (ObjCertificateLineForInsert.CharTestResult <> "") Or _
                        '                   (ObjCertificateLineForInsert.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Prestasi) And (ObjCertificateLineForInsert.CharTestResult <> "") Then
                        '    ObjCertificateLineForInsert.RowStatus = 0
                        '    ArrayCertificateLineForInsert.Add(ObjCertificateLineForInsert)
                        'End If
                    End If
                Else

                    'update
                    ObjCertificateLineForUpdate = objTrCertificateLine
                    'ObjCertificateLineForUpdate.TrCourseEvaluation = objTrCourseEval
                    'ObjCertificateLineForUpdate.TrClassRegistration = ObjTrClassRegistration

                    If (ObjCertificateLineForUpdate.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Angka) And (TxtAngka.Text <> "") Or _
                      (ObjCertificateLineForUpdate.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Sikap) And (CType(dtgItem.FindControl("txtSikap"), TextBox).Text <> "") Or _
                      (ObjCertificateLineForUpdate.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Prestasi) And (CType(dtgItem.FindControl("txtPrestasi"), TextBox).Text <> "") Then

                        If ObjCertificateLineForUpdate.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Angka Then

                            If IsNumeric(TxtAngka.Text) Then
                                If CType(TxtAngka.Text, Decimal) > 100 Or CType(TxtAngka.Text, Decimal) < 0 Then
                                    CType(dtgItem.FindControl("lblMsg"), Label).Text = " X"
                                    nCheck = 2
                                    bcheck = False
                                Else
                                    CType(dtgItem.FindControl("lblMsg"), Label).Text = ""
                                    If ObjCertificateLineForUpdate.NumTestResult <> CType(TxtAngka.Text, Decimal) Then
                                        ObjCertificateLineForUpdate.RowStatus = 0
                                        If TxtAngka.Text = "" Then
                                            ObjCertificateLineForUpdate.NumTestResult = 0
                                        Else
                                            ObjCertificateLineForUpdate.NumTestResult = CType(TxtAngka.Text, Decimal)
                                        End If
                                        ArrayCertificateLineForUpdate.Add(ObjCertificateLineForUpdate)
                                    End If

                                End If
                            Else
                                CType(dtgItem.FindControl("lblMsg"), Label).Text = " X"
                                nCheck = 3
                                bcheck = False

                            End If

                        ElseIf ObjCertificateLineForUpdate.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Sikap Then
                            If ValidateNilaiSikapAndPrestasi(dtgItem, "txtSikap") Then
                                CType(dtgItem.FindControl("lblIndikator"), Label).Text = ""
                                If ObjCertificateLineForUpdate.CharTestResult <> CType(CType(dtgItem.FindControl("txtSikap"), TextBox).Text, String) Then
                                    ObjCertificateLineForUpdate.RowStatus = 0
                                    ObjCertificateLineForUpdate.CharTestResult = CType(CType(dtgItem.FindControl("txtSikap"), TextBox).Text, String)
                                    ArrayCertificateLineForUpdate.Add(ObjCertificateLineForUpdate)
                                End If

                            Else
                                bcheck = False
                                CType(dtgItem.FindControl("lblIndikator"), Label).Text = " X"
                                nCheck = 4
                                Exit For
                            End If

                        ElseIf ObjCertificateLineForUpdate.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Prestasi Then
                            If ValidateNilaiSikapAndPrestasi(dtgItem, "txtPrestasi") Then
                                CType(dtgItem.FindControl("lblIndikator2"), Label).Text = ""
                                If ObjCertificateLineForUpdate.CharTestResult <> CType(CType(dtgItem.FindControl("txtPrestasi"), TextBox).Text, String) Then
                                    ObjCertificateLineForUpdate.RowStatus = 0
                                    ObjCertificateLineForUpdate.CharTestResult = CType(CType(dtgItem.FindControl("txtPrestasi"), TextBox).Text, String)
                                    ArrayCertificateLineForUpdate.Add(ObjCertificateLineForUpdate)
                                End If
                            Else
                                bcheck = False
                                CType(dtgItem.FindControl("lblIndikator2"), Label).Text = " X"
                                nCheck = 5
                                Exit For
                            End If
                        End If

                        'If (objTrCertificateLine.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Angka) And (objTrCertificateLine.NumTestResult >= 0D) Or _
                        '                   (objTrCertificateLine.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Sikap) And (objTrCertificateLine.CharTestResult <> "") Or _
                        '                   (objTrCertificateLine.TrCourseEvaluation.Type = EnumTrEvaluationType.TrEvaluationType.Prestasi) And (objTrCertificateLine.CharTestResult <> "") Then
                        '    objTrCertificateLine.RowStatus = 0
                        '    ArrayCertificateLineForUpdate.Add(objTrCertificateLine)
                        'End If
                    End If
                End If
            Next
        End If


        If bcheck Then
            If ArrayCertificateLineForInsert.Count > 0 Then
                Dim nTransResult = New TrCertificateLineFacade(User).InsertCertificateLinePerClassReg(ArrayCertificateLineForInsert, ObjTrClassRegistration)
                If nTransResult = 0 Then
                    nSuccessInsert = 1
                    nRowInsert += ArrayCertificateLineForInsert.Count
                Else
                    bSuccess = False
                    nSuccessInsert = -1
                End If
            Else
                nRowInsert = 0
            End If

            If ArrayCertificateLineForUpdate.Count > 0 Then
                Dim nTransResult = New TrCertificateLineFacade(User).UpdateCertificateLinePerClassReg(ArrayCertificateLineForUpdate, ObjTrClassRegistration)
                If nTransResult = 0 Then
                    nSuccessUpdate = 1
                    nRowInsert += ArrayCertificateLineForUpdate.Count
                Else
                    bSuccess = False
                    nSuccessUpdate = -1
                End If
            Else
                nRowUpdate = 0
            End If

            If bSuccess Then
                If nSuccessInsert = 1 And nSuccessUpdate = 1 Then
                    MessageBox.Show("Insert dan Update data nilai sukses")
                ElseIf nSuccessInsert = 1 And nSuccessUpdate = 0 Then
                    MessageBox.Show("Insert data nilai sukses")
                ElseIf nSuccessUpdate = 1 And nSuccessInsert = 0 Then
                    MessageBox.Show("Update data nilai sukses")
                ElseIf nRowInsert = 0 And nRowUpdate = 0 Then
                    MessageBox.Show("Tidak ada data yang disimpan")
                End If
                btnRefresh_Click(sender, e)
            Else
                If nSuccessInsert = -1 And nSuccessUpdate = -1 Then
                    MessageBox.Show("Insert dan Update data nilai gagal")
                ElseIf nSuccessInsert = -1 And nSuccessUpdate = 0 Then
                    MessageBox.Show("Insert data nilai gagal")
                ElseIf nSuccessUpdate = -1 And nSuccessInsert = 0 Then
                    MessageBox.Show("Update data nilai sukses")
                End If
            End If
        Else
            If nCheck = 1 Then
                MessageBox.Show("Tidak Teregistrasi atau sudah lulus")
            ElseIf nCheck = 2 Then
                MessageBox.Show("Nilai Angka diluar range [0,100]")
            ElseIf nCheck = 3 Then
                MessageBox.Show("Format angka harus numerik")
            ElseIf nCheck = 4 Then
                MessageBox.Show("Format Nilai Sikap salah")
            ElseIf nCheck = 5 Then
                MessageBox.Show("Format Nilai Prestasi salah")
            End If
        End If





        'If bValid Then
        '    nRowInsert += CertificateLineCollForInsert.Count
        '    nRowUpdate += CertificateLineCollForUpdate.Count
        '    If CertificateLineCollForInsert.Count > 0 Then
        '        Dim nTransResult = New TrCertificateLineFacade(User).InsertCertificateLinePerClassReg(CertificateLineCollForInsert, objClassReg)
        '        If nTransResult = 0 Then
        '            CertificateLineCollForInsert.Clear()
        '            bSuccessInsert = True
        '        Else
        '            bValid = False
        '            bSuccessInsert = False
        '            nCheck = 5
        '            nInsertFault += 1
        '            strNoReg = objClassReg.ID.ToString
        '            strTraineeName = objClassReg.TrTrainee.Name
        '            Exit For
        '        End If
        '    End If
        '    If CertificateLineCollForUpdate.Count > 0 Then
        '        Dim nTransResult = New TrCertificateLineFacade(User).UpdateCertificateLinePerClassReg(CertificateLineCollForUpdate, objClassReg)
        '        If nTransResult = 0 Then
        '            CertificateLineCollForUpdate.Clear()
        '            bSuccessUpdate = True
        '        Else
        '            bValid = False
        '            bSuccessUpdate = False
        '            nCheck = 6
        '            nUpdateFault += 1
        '            strNoReg = objClassReg.ID.ToString
        '            strTraineeName = objClassReg.TrTrainee.Name
        '            Exit For
        '        End If
        '    End If
        'End If
        'Next

        'If bValid Then
        '    If bSuccessInsert And bSuccessUpdate And nInsertFault = 0 And nUpdateFault = 0 Then
        '        MessageBox.Show("Insert dan Update Sukses")
        '    ElseIf bSuccessInsert And nInsertFault = 0 And nRowInsert > 0 And nRowUpdate = 0 Then
        '        MessageBox.Show("Insert Sukses")
        '    ElseIf bSuccessUpdate And nUpdateFault = 0 And nRowUpdate > 0 And nRowInsert = 0 Then
        '        MessageBox.Show("Update Sukses")
        '    ElseIf nRowInsert = 0 And nRowUpdate = 0 Then
        '        MessageBox.Show("Tidak ada data yang disimpan atau diupdate")
        '    End If
        '    btnRefresh_Click(sender, e)
        'Else
        '    If nCheck = 1 Then
        '        MessageBox.Show("Nilai Angka diluar range [0,100]")
        '    ElseIf nCheck = 2 Then
        '        MessageBox.Show("Nilai Angka harus numerik")
        '    ElseIf nCheck = 3 Then
        '        MessageBox.Show("Format Nilai Sikap salah")
        '    ElseIf nCheck = 4 Then
        '        MessageBox.Show("Format Nilai Prestasi salah")
        '    ElseIf nCheck = 5 Then
        '        MessageBox.Show("Insert dengan no. reg = " + strNoReg + " Nama = " + strTraineeName + " gagal dilakukan")
        '    ElseIf nCheck = 6 Then
        '        MessageBox.Show("Update dengan no. reg = " + strNoReg + " Nama = " + strTraineeName + " gagal dilakukan")
        '    End If
        'End If

    End Sub


    Private Function ValidateNilaiSikapAndPrestasi(ByVal dtgItem As DataGridItem, ByVal txtstr As String) As Boolean
        'If Len(CType(dtgItem.FindControl(txtstr), TextBox).Text) <> 2 Then
        '    Return False
        'Else
        '    Dim str1 = CType(dtgItem.FindControl(txtstr), TextBox).Text.Substring(0, 1)
        '    Dim str2 = CType(dtgItem.FindControl(txtstr), TextBox).Text.Substring(1, 1)
        '    If ((str1 = "A" Or str1 = "B" Or str1 = "C" Or str1 = "D") And _
        '        (str2 = "1" Or str2 = "2" Or str2 = "3" Or str2 = "4" Or str2 = "5")) Then
        '        Return True
        '    End If
        'End If
        'Return False

        'ada permintaan terakhir untuk melepas validasi 
        '29/01/06
        Return True
    End Function

    Private Function JenisTest(ByVal str1 As String, ByVal str2 As String) As String
        Select Case str1
            Case "A"
                Return "Test-" + str2
            Case "B"
                Return "Sikap-" + str2
            Case "C"
                Return "Prestasi-1" + str2
            Case Else
                Return ""
        End Select
    End Function

    Private Function StrJenisTestName(ByVal objTrCourseEval As TrCourseEvaluation) As String
        Select Case Right(objTrCourseEval.EvaluationCode, 2)
            Case "00"
                Return "Initial Test"
            Case "99"
                Return "Final Test"
            Case "01"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "1")
            Case "02"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "2")
            Case "03"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "3")
            Case "04"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "4")
            Case "05"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "5")
            Case "06"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "6")
            Case "07"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "7")
            Case "08"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "8")
        End Select
    End Function


End Class
