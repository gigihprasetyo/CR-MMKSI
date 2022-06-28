Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports System.Drawing.Color
Imports System.Web.UI.WebControls.BorderStyle
Imports KTB.DNet.Security
Imports System.Collections.Generic
Imports System.Linq
Imports OfficeOpenXml
Imports GlobalExtensions
Imports System.IO

Public Class FrmTrCertificateLine3
    Inherits System.Web.UI.Page
    Private _sessHelper As SessionHelper = New SessionHelper
    Private helpers As New TrainingHelpers(Me.Page)

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtClassCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtClassName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeKategori As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpClass As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpCourse As System.Web.UI.WebControls.Label
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents btnRefresh As System.Web.UI.WebControls.Button
    Protected WithEvents ddlJenis As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnInsert As System.Web.UI.WebControls.Button
    Protected WithEvents dtgClassRegistration As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgClassRegistration2 As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dtgClassRegistration3 As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents ddlTahun As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlFiscalYear As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents ddlCourse As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnHitung As System.Web.UI.WebControls.Button

    Protected WithEvents dgNumEval As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSaveNumEval As System.Web.UI.WebControls.Button
    Protected WithEvents btnUpdateNumEval As System.Web.UI.WebControls.Button
    Protected WithEvents btnStatus As System.Web.UI.WebControls.Button
    Protected WithEvents hdnJenisNilai As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hdnCourseID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hdnAreaID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hdnClassCode As System.Web.UI.WebControls.HiddenField
    Protected WithEvents trTemplate As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trUpload As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents linkTemplate As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents fileUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents trAction As System.Web.UI.HtmlControls.HtmlTableRow

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private CellDisabled As List(Of String) = New List(Of String)
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub UbahTitikKeKoma(ByRef TxtTest1 As TextBox, ByRef TxtTest2 As TextBox, _
        ByRef TxtTest3 As TextBox, ByRef TxtTest4 As TextBox, ByRef TxtTest5 As TextBox, _
        ByRef TxtTest6 As TextBox, ByRef TxtTest7 As TextBox, ByRef TxtTest8 As TextBox, _
        ByRef TxtTest9 As TextBox, ByRef TxtTestAwal As TextBox, ByRef TxtTestAkhir As TextBox)

        If Not TxtTest1 Is Nothing Then
            TxtTest1.Text = TxtTest1.Text.Replace(".", ",")
        End If
        If Not TxtTest2 Is Nothing Then
            TxtTest2.Text = TxtTest2.Text.Replace(".", ",")
        End If
        If Not TxtTest3 Is Nothing Then
            TxtTest3.Text = TxtTest3.Text.Replace(".", ",")
        End If
        If Not TxtTest4 Is Nothing Then
            TxtTest4.Text = TxtTest4.Text.Replace(".", ",")
        End If

        If Not TxtTest5 Is Nothing Then
            TxtTest5.Text = TxtTest5.Text.Replace(".", ",")
        End If

        If Not TxtTest6 Is Nothing Then
            TxtTest6.Text = TxtTest6.Text.Replace(".", ",")
        End If

        If Not TxtTest7 Is Nothing Then
            TxtTest7.Text = TxtTest7.Text.Replace(".", ",")
        End If

        If Not TxtTest8 Is Nothing Then
            TxtTest8.Text = TxtTest8.Text.Replace(".", ",")
        End If

        If Not TxtTest9 Is Nothing Then
            TxtTest9.Text = TxtTest9.Text.Replace(".", ",")
        End If

        If Not TxtTestAwal Is Nothing Then
            TxtTestAwal.Text = TxtTestAwal.Text.Replace(".", ",")
        End If

        If Not TxtTestAkhir Is Nothing Then
            TxtTestAkhir.Text = TxtTestAkhir.Text.Replace(".", ",")
        End If

    End Sub

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.PageNoCache()
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDdlJenis()
            BindDDLTahunFiskal(ddlFiscalYear)
            _sessHelper.SetSession("SessIsSimpan", False)
            If Not IsNothing(Request.QueryString("QS")) Then
                BackFromSession()

            End If
            Select Case AreaId
                Case "1"
                    lblPageTitle.Text = "Training Sales - Input Data Nilai"
                    lblPopUpCourse.Attributes("onclick") = "ShowPPCourseSelection('sales');"
                    hdnAreaID.Value = "1"
                Case "2"
                    lblPageTitle.Text = "Training After Sales - Input Data Nilai"
                    lblPopUpCourse.Attributes("onclick") = "ShowPPCourseSelection('ass');"
                    hdnAreaID.Value = "2"
                Case "3"
                    lblPageTitle.Text = "Training Customer Satisfaction - Input Data Nilai"
                    lblPopUpCourse.Attributes("onclick") = "ShowPPCourseSelection('cs');"
                    hdnAreaID.Value = "3"
            End Select
        End If
    End Sub
    'Private Sub AssignAttributeControl()
    '    Dim lblPopUp As Label = CType(Page.FindControl("lblPopUpDealer"), Label)
    '    lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection")
    'End Sub

    Private Sub BackFromSession()
        Dim dummyParam As String() = CType(Request.QueryString("QS"), String).Trim().Split(";")
        If dummyParam.Length > 0 Then
            If Not dummyParam(0) = String.Empty Then
                txtKodeKategori.Text = dummyParam(0).ToString
            End If
        End If
        If dummyParam.Length > 0 Then
            If Not dummyParam(1) = String.Empty Then
                txtKodeKategori.Text = CType(dummyParam(1), String)
            End If
        End If
        If dummyParam.Length > 1 Then
            If Not dummyParam(2) = String.Empty Then
                txtClassCode.Text = CType(dummyParam(2), String)
            End If
        End If
        If dummyParam.Length > 2 Then
            If Not dummyParam(3) = String.Empty Then
                ddlJenis.SelectedValue = CType(dummyParam(3), String)
            End If
        End If
        btnRefresh_Click(Nothing, Nothing)
    End Sub

    Private Sub BindDdlJenis()
        Dim al As ArrayList = New EnumTrEvaluationType().Retrieve
        For i As Integer = 0 To al.Count - 1
            ddlJenis.Items.Insert(i, New ListItem(al(i).NameType, al(i).ValueType))
        Next
        ddlJenis.Items.Insert(0, New ListItem("Silahkan Pilih Jenis Evaluasi", -1))
    End Sub

    Private Sub ActivateUserPrivilege()
        helpers.CheckPrivilegeTransaction("tr5" + AreaId.PrivilegeTrainingType)
        trAction.Visible = helpers.IsEdit

        CType(Page.FindControl("btnInsert"), Button).Attributes.Add("onclick", "return ResetChangeData();")
        CType(Page.FindControl("btnHitung"), Button).Attributes.Add("onclick", "return CheckChangedData();")
    End Sub

    Private Sub InitiatePage()
        AssignAttributeControl()
        ViewState("CurrentSortColumn") = "Type"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub AssignAttributeControl()
        Dim lblPopUpClass As Label = CType(Page.FindControl("lblPopUpClass"), Label)
        'Dim strYearAndCourse As String = ddlTahun.SelectedValue + ";" + ddlCourse.SelectedValue
        'lblPopUpClass.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpClassSelection.aspx?Year=" + strYearAndCourse, "", 500, 760, "ClassSelection")
        lblPopUpClass.Attributes.Add("onclick", " return ShowPopupCourseEvalSelection()")
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        _sessHelper.SetSession("SessIsUpdateCatatan", False)
        trTemplate.Visible = False
        trUpload.Visible = False

        If txtClassCode.Text = "" Then
            MessageBox.Show("Silahkan isi kode kelas !")
            Return
        ElseIf ddlJenis.SelectedValue = -1 Then
            MessageBox.Show("Silahkan pilih jenis evaluasi !")
            Return
        End If

        Dim bTestNull As Boolean = False
        Dim nMessage As Integer = 0
        Dim TrClassRegColl As ArrayList
        Dim TrClassColl As ArrayList
        Dim ObjTrClass As TrClass
        Dim TrCertificateLineColl As ArrayList = New ArrayList

        If txtClassCode.Text.Trim = "" Then
            bTestNull = True
            nMessage = 1
        End If


        Select Case ddlJenis.SelectedValue
            Case 0
                dtgClassRegistration.Visible = True
                dtgClassRegistration2.Visible = False
                dtgClassRegistration3.Visible = False
            Case 1
                dtgClassRegistration.Visible = False
                dtgClassRegistration2.Visible = True
                dtgClassRegistration3.Visible = False
            Case 2
                dtgClassRegistration.Visible = False
                dtgClassRegistration2.Visible = False
                dtgClassRegistration3.Visible = True
        End Select

        If Not bTestNull Then



            Dim critClass As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassCode", MatchType.Exact, txtClassCode.Text.Trim().ToUpper()))

            If Not String.IsNullorEmpty(txtKodeKategori.Text) Then
                critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.CourseCode", MatchType.Exact, txtKodeKategori.Text))
            End If

            If Not String.IsNullorEmpty(ddlFiscalYear.SelectedValue) Then
                critClass.opAnd(New Criteria(GetType(TrClass), "FiscalYear", MatchType.Exact, ddlFiscalYear.SelectedValue))
            End If

            If AreaId.NotNullorEmpty Then
                critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.JobPositionCategory.AreaID", MatchType.Exact, AreaId))
            End If

            'If ddlTahun.SelectedIndex >= 0 Then
            '    Dim startDate As Date = New DateTime(ddlTahun.SelectedItem.ToString(), 12, Date.DaysInMonth(ddlTahun.SelectedItem.ToString(), 12), 23, 59, 59)
            '    Dim finishDate As Date = New DateTime(ddlTahun.SelectedItem.ToString(), 1, 1, 0, 0, 0)
            '    critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "StartDate", MatchType.LesserOrEqual, startDate))
            '    critClass.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "FinishDate", MatchType.GreaterOrEqual, finishDate))
            'End If


            TrClassColl = New TrClassFacade(User).Retrieve(critClass)
            If TrClassColl.Count > 0 Then
                ObjTrClass = CType(TrClassColl(0), TrClass)

                _sessHelper.SetSession("SessTrClass", ObjTrClass)
                If ObjTrClass.StartDate > Today And AreaId <> 3 Then
                    MessageBox.Show("Kelas belum dimulai, tidak dapat memasukan atau melihat nilai !")
                    Select Case ddlJenis.SelectedValue
                        Case 0
                            dtgClassRegistration.DataSource = Nothing
                            dtgClassRegistration.DataBind()
                        Case 1
                            dtgClassRegistration2.DataSource = Nothing
                            dtgClassRegistration2.DataBind()
                        Case 2
                            dtgClassRegistration3.DataSource = Nothing
                            dtgClassRegistration3.DataBind()
                    End Select
                    Exit Sub
                ElseIf ObjTrClass.FinishDate >= Today And AreaId = 3 Then
                    MessageBox.Show("Kelas belum selesai, tidak dapat memasukan atau melihat nilai !")
                    Select Case ddlJenis.SelectedValue
                        Case 0
                            dtgClassRegistration.DataSource = Nothing
                            dtgClassRegistration.DataBind()
                        Case 1
                            dtgClassRegistration2.DataSource = Nothing
                            dtgClassRegistration2.DataBind()
                        Case 2
                            dtgClassRegistration3.DataSource = Nothing
                            dtgClassRegistration3.DataBind()
                    End Select
                    Exit Sub
                Else
                    Dim critClassReg As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critClassReg.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ClassCode", MatchType.Exact, txtClassCode.Text.Trim))

                    If AreaId = 3 Then
                        critClassReg.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Status", MatchType.InSet, "(1,2,0)"))
                    End If

                    'critClassReg.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Register, String)))
                    TrClassRegColl = New TrClassRegistrationFacade(User).Retrieve(critClassReg)
                    If TrClassRegColl.Count <= 0 Then
                        MessageBox.Show("Peserta pada kelas tersebut tidak ditemukan !")
                        Select Case ddlJenis.SelectedValue
                            Case 0
                                dtgClassRegistration.DataSource = Nothing
                                dtgClassRegistration.DataBind()
                            Case 1
                                dtgClassRegistration2.DataSource = Nothing
                                dtgClassRegistration2.DataBind()
                            Case 2
                                dtgClassRegistration3.DataSource = Nothing
                                dtgClassRegistration3.DataBind()
                        End Select
                        Exit Sub
                    Else

                        'add by ags
                        GetCourseEvaluation(ObjTrClass)

                        _sessHelper.SetSession("SessClassRegColl", TrClassRegColl)
                        Select Case ddlJenis.SelectedValue
                            Case 0
                                Dim critCourseEval As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, ObjTrClass.TrCourse.ID))
                                critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "Type", MatchType.Exact, CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)))

                                'Ambil courseevaluation yang dipilih pada trnumclassevaluation
                                critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "ID", MatchType.InSet, ("(select CourseEvaluationID from trclassnumevaluation where ClassID=" & ObjTrClass.ID & ")")))

                                Dim TrCourseEval As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(critCourseEval)
                                If TrCourseEval.Count >= 0 Then
                                    Dim jmlTest As Integer = TrCourseEval.Count
                                    _sessHelper.SetSession("JmlTest", jmlTest)
                                    _sessHelper.SetSession("CourseEvaluationCollectionAngka", TrCourseEval)
                                    _sessHelper.SetSession("JenisEvaluasi", "Angka")
                                End If
                                hdnCourseID.Value = ObjTrClass.TrCourse.ID
                                hdnJenisNilai.Value = ddlJenis.SelectedValue
                                hdnClassCode.Value = txtClassCode.Text
                                dtgClassRegistration.DataSource = TrClassRegColl
                                dtgClassRegistration.DataBind()
                            Case 1
                                hdnClassCode.Value = txtClassCode.Text
                                Dim critCourseEval As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, ObjTrClass.TrCourse.ID))
                                critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "Type", MatchType.Exact, CType(EnumTrEvaluationType.TrEvaluationType.Sikap, String)))

                                'Ambil courseevaluation yang dipilih pada trnumclassevaluation
                                critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "ID", MatchType.InSet, ("(select CourseEvaluationID from trclassnumevaluation where ClassID=" & ObjTrClass.ID & ")")))

                                Dim TrCourseEval As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(critCourseEval)
                                If TrCourseEval.Count >= 0 Then
                                    _sessHelper.SetSession("JmlTest", TrCourseEval.Count)
                                    _sessHelper.SetSession("CourseEvaluationCollectionSikap", TrCourseEval)
                                    _sessHelper.SetSession("JenisEvaluasi", "Sikap")
                                End If
                                hdnCourseID.Value = ObjTrClass.TrCourse.ID
                                hdnJenisNilai.Value = ddlJenis.SelectedValue
                                dtgClassRegistration2.DataSource = TrClassRegColl
                                dtgClassRegistration2.DataBind()
                            Case 2
                                Dim critCourseEval As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, ObjTrClass.TrCourse.ID))
                                critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "Type", MatchType.Exact, CType(EnumTrEvaluationType.TrEvaluationType.Prestasi, String)))
                                Dim TrCourseEval As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(critCourseEval)
                                If TrCourseEval.Count >= 0 Then
                                    _sessHelper.SetSession("JmlTest", TrCourseEval.Count)
                                    _sessHelper.SetSession("CourseEvaluationCollectionPrestasi", TrCourseEval)
                                    _sessHelper.SetSession("JenisEvaluasi", "Prestasi")
                                End If
                                hdnCourseID.Value = ObjTrClass.TrCourse.ID
                                hdnJenisNilai.Value = ddlJenis.SelectedValue
                                dtgClassRegistration3.DataSource = TrClassRegColl
                                dtgClassRegistration3.DataBind()
                        End Select


                        If ObjTrClass.SubmitStatus = 1 Then
                            btnInsert.Enabled = False
                            btnHitung.Enabled = False
                            btnStatus.Enabled = False
                        Else
                            trUpload.Visible = True
                            btnInsert.Enabled = True
                            btnHitung.Enabled = True
                            btnStatus.Enabled = True
                        End If
                    End If
                    trTemplate.Visible = True
                End If

            Else
                MessageBox.Show("Kelas tidak ada")
                Select Case ddlJenis.SelectedValue
                    Case 0
                        dtgClassRegistration.DataSource = Nothing
                        dtgClassRegistration.DataBind()
                    Case 1
                        dtgClassRegistration2.DataSource = Nothing
                        dtgClassRegistration2.DataBind()
                    Case 2
                        dtgClassRegistration3.DataSource = Nothing
                        dtgClassRegistration3.DataBind()
                End Select
                Exit Sub
            End If

        End If
        _sessHelper.SetSession("SessIsSimpan", False)
    End Sub

    Private Function GetCertificateLine(ByVal ObjTrClass As TrClass, ByVal ObjTrClassReg As TrClassRegistration) As ArrayList
        If Not IsNothing(ObjTrClass) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.TrClass.ID", MatchType.Exact, ObjTrClass.ID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.ID", MatchType.Exact, ObjTrClassReg.ID))
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

    Private Sub dtgClassRegistration_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegistration.ItemDataBound, dtgClassRegistration2.ItemDataBound, dtgClassRegistration3.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, CInt(hdnCourseID.Value)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "Type", MatchType.Exact, hdnJenisNilai.Value))
            Dim listEva As List(Of TrCourseEvaluation) = New TrCourseEvaluationFacade(User).Retrieve(criterias).Cast(Of TrCourseEvaluation).ToList()

            For Each itemcell As TableCell In e.Item.Cells
                If itemcell.Text.IndexOf("Awal") > -1 Or itemcell.Text.IndexOf("Akhir") > -1 Then
                    Continue For
                End If

                If itemcell.Text.StartsWith("Test") Or itemcell.Text.StartsWith("Nilai") Then
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

        If e.IsRowItems Then
            Dim rowValue As TrClassRegistration = e.DataItem(Of TrClassRegistration)()
            For Each mNumber As String In CellDisabled
                Dim dataParent As String = e.Item.Parent.ClientID
                If dataParent.IndexOf("dtgClassRegistration2") > -1 Then
                    e.Item.Cells(4 + CInt(mNumber)).Visible = False
                Else
                    e.Item.Cells(5 + CInt(mNumber)).Visible = False
                End If
            Next
            Dim lblSalesmanCode As Label = e.FindLabel("lblSalesmanCode")
            Dim lblTrId As Label = e.FindLabel("lblTrId")

            If AreaId.NotNullorEmpty Then
                If AreaId.Equals("1") Or AreaId.Equals("3") Then
                    lblSalesmanCode.Text = rowValue.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) _
                                           x.JobPositionAreaID = CInt(AreaId)).SalesmanHeader.SalesmanCode
                    lblTrId.Text = rowValue.TrTrainee.ID
                    lblTrId.NonVisible()
                Else
                    lblTrId.Text = rowValue.TrTrainee.ID
                    lblSalesmanCode.NonVisible()
                    If rowValue.Dealer.IsDealer Then
                        If helpers.isDueDateClass(rowValue.ID) Then
                            e.Item.BackColor = Color.LightSalmon
                        End If
                    End If
                End If
            End If

            BoundRowItems(e)
            CType(e.Item.FindControl("lblNo"), Label).Text = e.CreateNumberPage
            Dim objClassReg As TrClassRegistration = New TrClassRegistration
            Dim strID As String
            Select Case ddlJenis.SelectedValue
                Case 0
                    objClassReg = CType(CType(dtgClassRegistration.DataSource, ArrayList)(e.Item.ItemIndex), TrClassRegistration)
                    strID = objClassReg.ID.ToString
                Case 1
                    objClassReg = CType(CType(dtgClassRegistration2.DataSource, ArrayList)(e.Item.ItemIndex), TrClassRegistration)
                    strID = objClassReg.ID.ToString
                Case 2
                    objClassReg = CType(CType(dtgClassRegistration3.DataSource, ArrayList)(e.Item.ItemIndex), TrClassRegistration)
                    strID = objClassReg.ID.ToString
            End Select
            Dim lbtnCatatan As Label = CType(e.Item.FindControl("lbtnCatatan"), Label)
            If AreaId.IsNullorEmpty Then
                lbtnCatatan.Attributes.Add("onclick", GeneralScript.GetPopUpEventReference("../PopUp/PopUpInputCatatan.aspx?id=" + strID, "", 250, 560, "''"))
            Else
                lbtnCatatan.Attributes.Add("onclick", GeneralScript.GetPopUpEventReference("../PopUp/PopUpInputCatatan.aspx?id=" + strID + "&area=" + AreaId, "", 250, 560, "''"))
            End If

            FillTestResult(e, objClassReg)

            Dim _chkRow As CheckBox = CType(e.Item.FindControl("chkPass"), CheckBox)
            If (Not _chkRow Is Nothing) Then
                _chkRow.Enabled = helpers.IsEdit
                _chkRow.Checked = IIf(objClassReg.Status = 1, True, False)
                If Not objClassReg.IsBilliing Then
                    _chkRow.Enabled = False
                End If

            End If

            If objClassReg.TrClass.ClassType = CType(EnumTrClass.EnumClassType.E_LEARNING, Short) Then
                Me.DisabledTextBoxWithPrefix("Txt")
            End If
            If Not helpers.IsEdit Then
                e.Item.DisabledTextBoxWithPrefix("txt")
                lbtnCatatan.Visible = False
            End If

        End If
    End Sub

    Private Sub FillTestResult(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal objClassReg As TrClassRegistration)
        If Not IsNothing(objClassReg) Then
            Dim TrCertificateLineColl As ArrayList = New ArrayList
            TrCertificateLineColl = GetCertificateLine(CType(_sessHelper.GetSession("SessTrClass"), TrClass), objClassReg)
            _sessHelper.SetSession("sessCertificateLineColl", TrCertificateLineColl)


            If Not IsNothing(TrCertificateLineColl) Then
                If TrCertificateLineColl.Count > 0 Then
                    For Each objCertificateLine As TrCertificateLine In TrCertificateLineColl
                        Select Case ddlJenis.SelectedValue
                            Case 0
                                For Each objTrCourseEval As TrCourseEvaluation In CType(_sessHelper.GetSession("CourseEvaluationCollectionAngka"), ArrayList)
                                    If objCertificateLine.TrCourseEvaluation.EvaluationCode = objTrCourseEval.EvaluationCode Then
                                        FillTextItemWithNumericTestResult(e, objCertificateLine)
                                    End If
                                Next
                            Case 1
                                For Each objTrCourseEval As TrCourseEvaluation In CType(_sessHelper.GetSession("CourseEvaluationCollectionSikap"), ArrayList)
                                    If objCertificateLine.TrCourseEvaluation.EvaluationCode = objTrCourseEval.EvaluationCode Then
                                        FillTextItemWithCharTestResult(e, objCertificateLine)
                                    End If
                                Next

                            Case 2
                                For Each objTrCourseEval As TrCourseEvaluation In CType(_sessHelper.GetSession("CourseEvaluationCollectionPrestasi"), ArrayList)
                                    If objCertificateLine.TrCourseEvaluation.EvaluationCode = objTrCourseEval.EvaluationCode Then
                                        FillTextItemWithCharTestResult(e, objCertificateLine)
                                    End If
                                Next
                        End Select
                    Next
                End If
            End If
        End If
    End Sub
    Private Sub FillControlWithNumValue(ByVal ControlName As String, _
               ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal objCertificateLine As TrCertificateLine)
        If objCertificateLine.NumTestResult >= 0 Then
            CType(e.Item.FindControl(ControlName), TextBox).Text = CType(objCertificateLine.NumTestResult, String)
            CType(e.Item.FindControl(ControlName), TextBox).BackColor = MistyRose
            CType(e.Item.FindControl(ControlName), TextBox).Enabled = True
        End If
    End Sub

    Private Sub FillTextItemWithNumericTestResult(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal objCertificateLine As TrCertificateLine)
        Select Case Right(objCertificateLine.TrCourseEvaluation.EvaluationCode, 2)
            Case "00"
                FillControlWithNumValue("TxtTestAwal", e, objCertificateLine)

            Case "01"
                FillControlWithNumValue("TxtTest1", e, objCertificateLine)
            Case "02"
                FillControlWithNumValue("TxtTest2", e, objCertificateLine)
            Case "03"
                FillControlWithNumValue("TxtTest3", e, objCertificateLine)
            Case "04"
                FillControlWithNumValue("TxtTest4", e, objCertificateLine)
            Case "05"
                FillControlWithNumValue("TxtTest5", e, objCertificateLine)
            Case "06"
                FillControlWithNumValue("TxtTest6", e, objCertificateLine)
            Case "07"
                FillControlWithNumValue("TxtTest7", e, objCertificateLine)
            Case "08"
                FillControlWithNumValue("TxtTest8", e, objCertificateLine)
            Case "09"
                FillControlWithNumValue("TxtTest9", e, objCertificateLine)
            Case "99"
                FillControlWithNumValue("TxtTestAkhir", e, objCertificateLine)
        End Select
    End Sub
    Private Sub FillControlWithCharValue(ByVal ControlName As String, _
            ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal objCertificateLine As TrCertificateLine)
        CType(e.Item.FindControl(ControlName), TextBox).Text = objCertificateLine.CharTestResult
        CType(e.Item.FindControl(ControlName), TextBox).BackColor = MistyRose
        'CType(e.Item.FindControl(ControlName), TextBox).ReadOnly = True
    End Sub

    Private Sub FillTextItemWithCharTestResult(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal objCertificateLine As TrCertificateLine)
        Select Case Right(objCertificateLine.TrCourseEvaluation.EvaluationCode, 2)
            Case "01"
                FillControlWithCharValue("TxtTest1", e, objCertificateLine)
            Case "02"
                FillControlWithCharValue("TxtTest2", e, objCertificateLine)
            Case "03"
                FillControlWithCharValue("TxtTest3", e, objCertificateLine)
            Case "04"
                FillControlWithCharValue("TxtTest4", e, objCertificateLine)
            Case "05"
                FillControlWithCharValue("TxtTest5", e, objCertificateLine)
            Case "06"
                FillControlWithCharValue("TxtTest6", e, objCertificateLine)
            Case "07"
                FillControlWithCharValue("TxtTest7", e, objCertificateLine)
            Case "08"
                FillControlWithCharValue("TxtTest8", e, objCertificateLine)
            Case "09"
                FillControlWithCharValue("TxtTest9", e, objCertificateLine)
        End Select

    End Sub

    Private Sub SetPropertyControl(ByVal ControlName As String, _
        ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal bWriteToolTip As Boolean)
        CType(e.Item.FindControl(ControlName), TextBox).Enabled = False
        CType(e.Item.FindControl(ControlName), TextBox).BackColor = LightGray
        If bWriteToolTip Then
            CType(e.Item.FindControl(ControlName), TextBox).ToolTip = "Jenis evaluasi belum dibuat !"
        End If
    End Sub

    Private Sub InitialTextItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal KindOfDtg As String)
        SetPropertyControl("TxtTest1", e, False)
        SetPropertyControl("TxtTest2", e, False)
        SetPropertyControl("TxtTest3", e, False)
        SetPropertyControl("TxtTest4", e, False)
        SetPropertyControl("TxtTest5", e, False)
        SetPropertyControl("TxtTest6", e, False)
        SetPropertyControl("TxtTest7", e, False)
        SetPropertyControl("TxtTest8", e, False)
        SetPropertyControl("TxtTest9", e, False)
        If KindOfDtg = "dtgClassRegistration" Then
            SetPropertyControl("TxtTestAwal", e, True)
            SetPropertyControl("TxtTestAkhir", e, True)
        End If

    End Sub

    Private Sub SetPropertyControl2(ByVal ControlName As String, _
            ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        CType(e.Item.FindControl(ControlName), TextBox).Enabled = True
        CType(e.Item.FindControl(ControlName), TextBox).BackColor = White
    End Sub

    Private Sub PrepareAndSettingTextItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal Iter As Integer, ByVal CourseEvaluationColl As ArrayList)

        If CourseEvaluationColl.Count > 0 Then
            For Each objCourseEval As TrCourseEvaluation In CourseEvaluationColl
                Select Case Right(objCourseEval.EvaluationCode, 2)
                    Case "01"
                        SetPropertyControl2("TxtTest1", e)
                    Case "02"
                        SetPropertyControl2("TxtTest2", e)
                    Case "03"
                        SetPropertyControl2("TxtTest3", e)
                    Case "04"
                        SetPropertyControl2("TxtTest4", e)
                    Case "05"
                        SetPropertyControl2("TxtTest5", e)
                    Case "06"
                        SetPropertyControl2("TxtTest6", e)
                    Case "07"
                        SetPropertyControl2("TxtTest7", e)
                    Case "08"
                        SetPropertyControl2("TxtTest8", e)
                    Case "09"
                        SetPropertyControl2("TxtTest9", e)
                End Select

            Next
        End If
    End Sub
    Private Sub BoundRowItems(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim jmlTest As Integer = CType(_sessHelper.GetSession("JmlTest"), Integer)
        Dim CourseEvaluationColl As ArrayList = New ArrayList
        Dim KindOfDtg As String = ""
        Select Case ddlJenis.SelectedValue
            Case 0
                CourseEvaluationColl = _sessHelper.GetSession("CourseEvaluationCollectionAngka")
                KindOfDtg = "dtgClassRegistration"
            Case 1
                CourseEvaluationColl = _sessHelper.GetSession("CourseEvaluationCollectionSikap")
                KindOfDtg = "dtgClassRegistration2"
            Case 2
                CourseEvaluationColl = _sessHelper.GetSession("CourseEvaluationCollectionPrestasi")
                KindOfDtg = "dtgClassRegistration3"
        End Select

        If CType(_sessHelper.GetSession("JenisEvaluasi"), String) = "Angka" Then
            For Each item As TrCourseEvaluation In CourseEvaluationColl
                If Right(item.EvaluationCode, 2) = "00" Then
                    jmlTest -= 1
                ElseIf Right(item.EvaluationCode, 2) = "99" Then
                    jmlTest -= 1
                End If
            Next
        End If
        InitialTextItem(e, KindOfDtg)
        PrepareAndSettingTextItem(e, jmlTest, CourseEvaluationColl)
        For Each item As TrCourseEvaluation In CourseEvaluationColl
            If KindOfDtg = "0" Then
                CType(e.Item.FindControl("TxtTestAwal"), TextBox).Attributes.Add("onchange", "ChangedData()")
                CType(e.Item.FindControl("TxtTestAkhir"), TextBox).Attributes.Add("onchange", "ChangedData()")
                CType(e.Item.FindControl("TxtTestAwal"), TextBox).Attributes.Add("onblur", "numericOnlyWithComaBlur(" & CType(e.Item.FindControl("TxtTestAwal"), TextBox).ClientID & ")")
                CType(e.Item.FindControl("TxtTestAkhir"), TextBox).Attributes.Add("onblur", "numericOnlyWithComaBlur(" & CType(e.Item.FindControl("TxtTestAkhir"), TextBox).ClientID & ")")

            End If
            If ddlJenis.SelectedValue.Equals("0") Then
                If Right(item.EvaluationCode, 2) = "00" Then
                    CType(e.Item.FindControl("TxtTestAwal"), TextBox).Enabled = True
                    CType(e.Item.FindControl("TxtTestAwal"), TextBox).BackColor = White
                    CType(e.Item.FindControl("TxtTestAwal"), TextBox).ToolTip = "Diisi dengan angka skala [0,100]"
                ElseIf Right(item.EvaluationCode, 2) = "99" Then
                    CType(e.Item.FindControl("TxtTestAkhir"), TextBox).Enabled = True
                    CType(e.Item.FindControl("TxtTestAkhir"), TextBox).BackColor = White
                    CType(e.Item.FindControl("TxtTestAkhir"), TextBox).ToolTip = "Diisi dengan angka skala [0,100]"
                End If
            End If


            CType(e.Item.FindControl("TxtTest1"), TextBox).Attributes.Add("onchange", "ChangedData()")
            CType(e.Item.FindControl("TxtTest2"), TextBox).Attributes.Add("onchange", "ChangedData()")
            CType(e.Item.FindControl("TxtTest3"), TextBox).Attributes.Add("onchange", "ChangedData()")
            CType(e.Item.FindControl("TxtTest4"), TextBox).Attributes.Add("onchange", "ChangedData()")
            CType(e.Item.FindControl("TxtTest5"), TextBox).Attributes.Add("onchange", "ChangedData()")
            CType(e.Item.FindControl("TxtTest6"), TextBox).Attributes.Add("onchange", "ChangedData()")
            CType(e.Item.FindControl("TxtTest7"), TextBox).Attributes.Add("onchange", "ChangedData()")
            CType(e.Item.FindControl("TxtTest8"), TextBox).Attributes.Add("onchange", "ChangedData()")
            CType(e.Item.FindControl("TxtTest9"), TextBox).Attributes.Add("onchange", "ChangedData()")
            If KindOfDtg = "0" Then
                CType(e.Item.FindControl("TxtTest1"), TextBox).Attributes.Add("onblur", "numericOnlyWithComaBlur(" & CType(e.Item.FindControl("TxtTest1"), TextBox).ClientID & ")")
                CType(e.Item.FindControl("TxtTest2"), TextBox).Attributes.Add("onblur", "numericOnlyWithComaBlur(" & CType(e.Item.FindControl("TxtTest2"), TextBox).ClientID & ")")
                CType(e.Item.FindControl("TxtTest3"), TextBox).Attributes.Add("onblur", "numericOnlyWithComaBlur(" & CType(e.Item.FindControl("TxtTest3"), TextBox).ClientID & ")")
                CType(e.Item.FindControl("TxtTest4"), TextBox).Attributes.Add("onblur", "numericOnlyWithComaBlur(" & CType(e.Item.FindControl("TxtTest4"), TextBox).ClientID & ")")
                CType(e.Item.FindControl("TxtTest5"), TextBox).Attributes.Add("onblur", "numericOnlyWithComaBlur(" & CType(e.Item.FindControl("TxtTest5"), TextBox).ClientID & ")")
                CType(e.Item.FindControl("TxtTest6"), TextBox).Attributes.Add("onblur", "numericOnlyWithComaBlur(" & CType(e.Item.FindControl("TxtTest6"), TextBox).ClientID & ")")
                CType(e.Item.FindControl("TxtTest7"), TextBox).Attributes.Add("onblur", "numericOnlyWithComaBlur(" & CType(e.Item.FindControl("TxtTest7"), TextBox).ClientID & ")")
                CType(e.Item.FindControl("TxtTest8"), TextBox).Attributes.Add("onblur", "numericOnlyWithComaBlur(" & CType(e.Item.FindControl("TxtTest8"), TextBox).ClientID & ")")
                CType(e.Item.FindControl("TxtTest9"), TextBox).Attributes.Add("onblur", "numericOnlyWithComaBlur(" & CType(e.Item.FindControl("TxtTest9"), TextBox).ClientID & ")")

            End If
        Next
    End Sub
    Private Sub dtgCourseEvaluation_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dtgClassRegistration.SelectedIndex = -1
        dtgClassRegistration.CurrentPageIndex = e.NewPageIndex
        'BindDatagrid(dtgClassRegistration.CurrentPageIndex)
    End Sub
    Private Sub dtgCourseEvaluation_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs)
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
    End Sub


    Private Function ValidateNilaiSikapAndPrestasi(ByVal txtstr As String, ByRef strMsg As String, ByRef nCheck As Integer, ByRef bValid As Boolean, ByVal FlagJenisEvaluasi As String) As Boolean
        'ada permintaan terakhir untuk melepas validasi 
        '29/01/06

        'If Len(txtstr) <> 0 Then
        '    If Len(txtstr) <> 2 Then
        '        strMsg = " X"
        '        If FlagJenisEvaluasi = "Sikap" Then nCheck = 3 Else nCheck = 4
        '        bValid = False
        '        Return False
        '    Else
        '        Dim str1 = txtstr.Substring(0, 1)
        '        Dim str2 = txtstr.Substring(1, 1)
        '        If ((str1 = "A" Or str1 = "B" Or str1 = "C" Or str1 = "D") And _
        '            (str2 = "1" Or str2 = "2" Or str2 = "3" Or str2 = "4" Or str2 = "5")) Then
        '            Return True
        '        Else
        '            strMsg = " X"
        '            If FlagJenisEvaluasi = "Sikap" Then nCheck = 3 Else nCheck = 4
        '            bValid = False
        '            Return False
        '        End If
        '    End If
        'End If
        'Return False

        'Permintaan terbaru untuk memasang validasi non numerik
        For Each character As Char In txtstr
            If IsNumeric(character) Then
                strMsg = " X"
                If FlagJenisEvaluasi = "Sikap" Then nCheck = 3 Else nCheck = 4
                bValid = False
                Return False
                Exit For
            End If
        Next

        Return True

    End Function
    Private Function ValidateNumericValue(ByVal strtxt As String, ByRef strMsg As String, ByRef nCheck As Integer, ByRef bValid As Boolean) As Boolean
        If strtxt <> "" Then
            'Modified by Ikhsan Aminuddin, 29 Juli 2008
            'Requested by Rina
            'Tambah validasi untuk format nilai decimal
            If IsNumeric(strtxt) Then
                'If Decimal.Parse( Parse(  (strtxt) Then
                If CType(strtxt, Decimal) > 100 Or CType(strtxt, Decimal) < 0 Then
                    strMsg = " X"
                    nCheck = 1
                    bValid = False
                    Return False
                Else
                    strMsg = ""
                End If
                'Else
                '    strMsg = " X"
                '    nCheck = 2
                '    bValid = False
                '    Return False
                'End If
            Else
                strMsg = " X"
                nCheck = 2
                bValid = False
                Return False
            End If
        Else
            Return False
        End If
        Return True
    End Function

    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsert.Click
        Dim bValid As Boolean = True
        Dim bCheck As Boolean = True
        Dim nCheck As Integer = 0
        Dim bSuccessInsert As Boolean = False
        Dim bSuccessUpdate As Boolean = False
        Dim nInsertFault As Integer = 0
        Dim nUpdateFault As Integer = 0
        Dim nIndeks As Integer
        Dim strNoReg As String = ""
        Dim strTraineeName As String = ""
        Dim nRowInsert As Integer = 0
        Dim nRowUpdate As Integer = 0
        Dim FlagJenisEvaluasi As String = ""

        Dim CourseEvaluationColl As ArrayList = New ArrayList
        Dim CertificateLineColl As ArrayList = New ArrayList
        Dim ClassRegColl As ArrayList = New ArrayList
        Dim CertificateLineCollForInsert As ArrayList = New ArrayList
        Dim CertificateLineCollForUpdate As ArrayList = New ArrayList
        Dim objCertificateLineForUpdate = New TrCertificateLine

        Dim TxtTest1 As TextBox = New TextBox
        Dim TxtTest2 As TextBox = New TextBox
        Dim TxtTest3 As TextBox = New TextBox
        Dim TxtTest4 As TextBox = New TextBox
        Dim TxtTest5 As TextBox = New TextBox
        Dim TxtTest6 As TextBox = New TextBox
        Dim TxtTest7 As TextBox = New TextBox
        Dim TxtTest8 As TextBox = New TextBox
        Dim TxtTest9 As TextBox = New TextBox
        Dim TxtTestAwal As TextBox = New TextBox
        Dim TxtTestAkhir As TextBox = New TextBox

        Dim TxtMsg1 As Label = New Label
        Dim TxtMsg2 As Label = New Label
        Dim TxtMsg3 As Label = New Label
        Dim TxtMsg4 As Label = New Label
        Dim TxtMsg5 As Label = New Label
        Dim TxtMsg6 As Label = New Label
        Dim TxtMsg7 As Label = New Label
        Dim TxtMsg8 As Label = New Label
        Dim TxtMsg9 As Label = New Label
        Dim TxtMsgAwal As Label = New Label
        Dim TxtMsgAkhir As Label = New Label

        Select Case ddlJenis.SelectedValue
            Case 0
                CourseEvaluationColl = _sessHelper.GetSession("CourseEvaluationCollectionAngka")
                FlagJenisEvaluasi = "Angka"
            Case 1
                CourseEvaluationColl = _sessHelper.GetSession("CourseEvaluationCollectionSikap")
                FlagJenisEvaluasi = "Sikap"
            Case 2
                CourseEvaluationColl = _sessHelper.GetSession("CourseEvaluationCollectionPrestasi")
                FlagJenisEvaluasi = "Prestasi"
        End Select
        If CourseEvaluationColl Is Nothing Then CourseEvaluationColl = New ArrayList

        ClassRegColl = CType(_sessHelper.GetSession("SessClassRegColl"), ArrayList)
        If ClassRegColl Is Nothing Then ClassRegColl = New ArrayList

        For iterate As Integer = 0 To ClassRegColl.Count - 1
            Dim objClassReg As TrClassRegistration = CType(ClassRegColl(iterate), TrClassRegistration)
            Dim dtgItem As DataGridItem

            If FlagJenisEvaluasi = "Angka" Then
                dtgItem = dtgClassRegistration.Items(iterate)
            ElseIf FlagJenisEvaluasi = "Sikap" Then
                dtgItem = dtgClassRegistration2.Items(iterate)
            ElseIf FlagJenisEvaluasi = "Prestasi" Then
                dtgItem = dtgClassRegistration3.Items(iterate)
            End If

            TxtTest1 = CType(dtgItem.FindControl("TxtTest1"), TextBox)
            TxtTest2 = CType(dtgItem.FindControl("TxtTest2"), TextBox)
            TxtTest3 = CType(dtgItem.FindControl("TxtTest3"), TextBox)
            TxtTest4 = CType(dtgItem.FindControl("TxtTest4"), TextBox)
            TxtTest5 = CType(dtgItem.FindControl("TxtTest5"), TextBox)
            TxtTest6 = CType(dtgItem.FindControl("TxtTest6"), TextBox)
            TxtTest7 = CType(dtgItem.FindControl("TxtTest7"), TextBox)
            TxtTest8 = CType(dtgItem.FindControl("TxtTest8"), TextBox)
            TxtTest9 = CType(dtgItem.FindControl("TxtTest9"), TextBox)
            TxtTestAwal = CType(dtgItem.FindControl("TxtTestAwal"), TextBox)
            TxtTestAkhir = CType(dtgItem.FindControl("TxtTestAkhir"), TextBox)

            UbahTitikKeKoma(TxtTest1, TxtTest2, TxtTest3, TxtTest4, TxtTest5, TxtTest6, _
                TxtTest7, TxtTest8, TxtTest9, TxtTestAwal, TxtTestAkhir)

            TxtMsg1 = CType(dtgItem.FindControl("lblMsgTest1"), Label)
            TxtMsg2 = CType(dtgItem.FindControl("lblMsgTest2"), Label)
            TxtMsg3 = CType(dtgItem.FindControl("lblMsgTest3"), Label)
            TxtMsg4 = CType(dtgItem.FindControl("lblMsgTest4"), Label)
            TxtMsg5 = CType(dtgItem.FindControl("lblMsgTest5"), Label)
            TxtMsg6 = CType(dtgItem.FindControl("lblMsgTest6"), Label)
            TxtMsg7 = CType(dtgItem.FindControl("lblMsgTest7"), Label)
            TxtMsg8 = CType(dtgItem.FindControl("lblMsgTest8"), Label)
            TxtMsg9 = CType(dtgItem.FindControl("lblMsgTest9"), Label)
            TxtMsgAwal = CType(dtgItem.FindControl("lblMsgTestAwal"), Label)
            TxtMsgAkhir = CType(dtgItem.FindControl("lblMsgTestAkhir"), Label)

            'CertificateLineColl = CType(_sessHelper.GetSession("sessCertificateLineColl"), ArrayList)
            'hanya session yang terakhir, jadi terpaksa rietrieve lagi
            CertificateLineColl = GetCertificateLine(CType(_sessHelper.GetSession("SessTrClass"), TrClass), objClassReg)
            If CertificateLineColl Is Nothing Then CertificateLineColl = New ArrayList

            If FlagJenisEvaluasi = "Angka" Then
                GetNumericArrayForInsertOrUpdate(CourseEvaluationColl, CertificateLineColl, objClassReg, CertificateLineCollForInsert, _
                    CertificateLineCollForUpdate, objCertificateLineForUpdate, nCheck, bValid, _
                    TxtTestAwal.Text, TxtMsgAwal.Text, _
                    TxtTest1.Text, TxtMsg1.Text, _
                    TxtTest2.Text, TxtMsg2.Text, _
                    TxtTest3.Text, TxtMsg3.Text, _
                    TxtTest4.Text, TxtMsg4.Text, _
                    TxtTest5.Text, TxtMsg5.Text, _
                    TxtTest6.Text, TxtMsg6.Text, _
                    TxtTest7.Text, TxtMsg7.Text, _
                    TxtTest8.Text, TxtMsg8.Text, _
                    TxtTest9.Text, TxtMsg9.Text, _
                    TxtTestAkhir.Text, TxtMsgAkhir.Text)
            Else
                GetCharacterArrayForInsertOrUpdate(CourseEvaluationColl, CertificateLineColl, objClassReg, CertificateLineCollForInsert, _
                   CertificateLineCollForUpdate, objCertificateLineForUpdate, nCheck, bValid, _
                   TxtTest1.Text, TxtMsg1.Text, _
                   TxtTest2.Text, TxtMsg2.Text, _
                   TxtTest3.Text, TxtMsg3.Text, _
                   TxtTest4.Text, TxtMsg4.Text, _
                   TxtTest5.Text, TxtMsg5.Text, _
                   TxtTest6.Text, TxtMsg6.Text, _
                   TxtTest7.Text, TxtMsg7.Text, _
                   TxtTest8.Text, TxtMsg8.Text, _
                   TxtTest9.Text, TxtMsg9.Text, _
                   FlagJenisEvaluasi)
            End If


            If bValid Then
                nRowInsert += CertificateLineCollForInsert.Count
                nRowUpdate += CertificateLineCollForUpdate.Count
                If CertificateLineCollForInsert.Count > 0 Then
                    Dim nTransResult = New TrCertificateLineFacade(User).InsertCertificateLinePerClassReg(CertificateLineCollForInsert, objClassReg)
                    If nTransResult = 0 Then
                        CertificateLineCollForInsert.Clear()
                        bSuccessInsert = True
                    Else
                        bValid = False
                        bSuccessInsert = False
                        nCheck = 5
                        nInsertFault += 1
                        strNoReg = objClassReg.ID.ToString
                        strTraineeName = objClassReg.TrTrainee.Name
                        Exit For
                    End If
                End If
                If CertificateLineCollForUpdate.Count > 0 Then
                    Dim nTransResult = New TrCertificateLineFacade(User).UpdateCertificateLinePerClassReg(CertificateLineCollForUpdate, objClassReg)
                    If nTransResult = 0 Then
                        CertificateLineCollForUpdate.Clear()
                        bSuccessUpdate = True
                    Else
                        bValid = False
                        bSuccessUpdate = False
                        nCheck = 6
                        nUpdateFault += 1
                        strNoReg = objClassReg.ID.ToString
                        strTraineeName = objClassReg.TrTrainee.Name
                        Exit For
                    End If
                End If
            End If
        Next

        If bValid Then
            If _sessHelper.GetSession("SessIsUpdateCatatan") Then
                If bSuccessInsert And bSuccessUpdate And nInsertFault = 0 And nUpdateFault = 0 Then
                    MessageBox.Show("Insert / Update nilai dan catatan sukses")
                ElseIf bSuccessInsert And nInsertFault = 0 And nRowInsert > 0 And nRowUpdate = 0 Then
                    MessageBox.Show("Insert nilai dan catatan sukses")
                ElseIf bSuccessUpdate And nUpdateFault = 0 And nRowUpdate > 0 And nRowInsert = 0 Then
                    MessageBox.Show("Update nilai dan catatan sukses")
                ElseIf nRowInsert = 0 And nRowUpdate = 0 Then
                    MessageBox.Show("Update Catatan Sukses")
                End If
            Else
                If bSuccessInsert And bSuccessUpdate And nInsertFault = 0 And nUpdateFault = 0 Then
                    MessageBox.Show("Insert dan Update nilai sukses")
                ElseIf bSuccessInsert And nInsertFault = 0 And nRowInsert > 0 And nRowUpdate = 0 Then
                    MessageBox.Show("Insert Nilai Sukses")
                ElseIf bSuccessUpdate And nUpdateFault = 0 And nRowUpdate > 0 And nRowInsert = 0 Then
                    MessageBox.Show("Update Nilai Sukses")
                ElseIf nRowInsert = 0 And nRowUpdate = 0 Then
                    MessageBox.Show("Tidak ada data yang disimpan / diupdate")
                End If
            End If
            _sessHelper.SetSession("SessIsUpdateCatatan", False)
            _sessHelper.SetSession("SessIsSimpan", True)

            btnRefresh_Click(sender, e)
        Else
            If nCheck = 1 Then
                MessageBox.Show("Nilai Angka diluar range [0,100]")
            ElseIf nCheck = 2 Then
                MessageBox.Show("Nilai Angka harus numerik")
            ElseIf nCheck = 3 Then
                MessageBox.Show("Format Nilai Sikap salah")
            ElseIf nCheck = 4 Then
                MessageBox.Show("Format Nilai Prestasi salah")
            ElseIf nCheck = 5 Then
                MessageBox.Show("Insert dengan no. reg = " + strNoReg + " Nama = " + strTraineeName + " gagal dilakukan")
            ElseIf nCheck = 6 Then
                MessageBox.Show("Update dengan no. reg = " + strNoReg + " Nama = " + strTraineeName + " gagal dilakukan")
            End If
        End If

        'CalculateAndRank()

    End Sub
    Private Sub FillCertificateLineCollForInsert(ByRef objCertificateLineForInsert As TrCertificateLine, ByRef CertificateLineCollForInsert As ArrayList, ByVal ControlName As String)
        If (objCertificateLineForInsert.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)) And (objCertificateLineForInsert.NumTestResult >= 0D) Then
            If ValidateCertificateLine(objCertificateLineForInsert.TrCourseEvaluation, objCertificateLineForInsert.TrClassRegistration) Then
                objCertificateLineForInsert.RowStatus = 0
                objCertificateLineForInsert.NumTestResult = CType(ControlName, Decimal)
                CertificateLineCollForInsert.Add(objCertificateLineForInsert)
            End If
        ElseIf (objCertificateLineForInsert.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Sikap, String)) Or _
                (objCertificateLineForInsert.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Prestasi, String)) Then

            If ValidateCertificateLine(objCertificateLineForInsert.TrCourseEvaluation, objCertificateLineForInsert.TrClassRegistration) Then
                objCertificateLineForInsert.RowStatus = 0
                objCertificateLineForInsert.CharTestResult = ControlName
                CertificateLineCollForInsert.Add(objCertificateLineForInsert)
            End If
        End If
    End Sub

    Private Sub FillCertificateLineCollForUpdate(ByRef objCertificateLineForUpdate As TrCertificateLine, ByRef CertificateLineCollForUpdate As ArrayList, ByVal ControlName As String)
        If (objCertificateLineForUpdate.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)) And (objCertificateLineForUpdate.NumTestResult >= 0D) Then
            If Not ValidateCertificateLine(objCertificateLineForUpdate.TrCourseEvaluation, objCertificateLineForUpdate.TrClassRegistration) Then
                If objCertificateLineForUpdate.NumTestResult <> CType(ControlName, Decimal) Then
                    objCertificateLineForUpdate.RowStatus = 0
                    objCertificateLineForUpdate.NumTestResult = CType(ControlName, Decimal)
                    CertificateLineCollForUpdate.Add(objCertificateLineForUpdate)
                End If
            End If
        ElseIf (objCertificateLineForUpdate.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Sikap, String)) Or _
                (objCertificateLineForUpdate.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Prestasi, String)) Then
            If Not ValidateCertificateLine(objCertificateLineForUpdate.TrCourseEvaluation, objCertificateLineForUpdate.TrClassRegistration) Then
                If objCertificateLineForUpdate.CharTestResult <> ControlName Then
                    objCertificateLineForUpdate.RowStatus = 0
                    objCertificateLineForUpdate.CharTestResult = ControlName
                    CertificateLineCollForUpdate.Add(objCertificateLineForUpdate)
                End If
            End If
        End If
    End Sub

    Private Function ValidateCertificateLine(ByVal ObjCourseEval As TrCourseEvaluation, ByVal ObjTrClassReg As TrClassRegistration) As Boolean
        If Not IsNothing(ObjCourseEval) Then
            If Not ObjTrClassReg Is Nothing Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrCourseEvaluation.ID", MatchType.Exact, ObjCourseEval.ID))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCertificateLine), "TrClassRegistration.ID", MatchType.Exact, ObjTrClassReg.ID))
                Dim ArryListCertificateLine As ArrayList = New TrCertificateLineFacade(User).Retrieve(criterias)
                If ArryListCertificateLine.Count > 0 Then
                    Return False
                Else
                    Return True
                End If
            End If
        End If
    End Function

    Private Sub GetNumericArrayForInsertOrUpdate( _
    ByVal CourseEvaluationColl As ArrayList, _
    ByVal CertificateLineColl As ArrayList, _
    ByVal objClassReg As TrClassRegistration, _
    ByRef CertificateLineCollForInsert As ArrayList, _
    ByRef CertificateLineCollForUpdate As ArrayList, _
    ByRef objCertificateLineForUpdate As TrCertificateLine, _
    ByRef nCheck As Integer, _
    ByRef bValid As Boolean, _
    ByVal txt0 As String, ByRef msg0 As String, _
    ByVal txt1 As String, ByRef msg1 As String, _
    ByVal txt2 As String, ByRef msg2 As String, _
    ByVal txt3 As String, ByRef msg3 As String, _
    ByVal txt4 As String, ByRef msg4 As String, _
    ByVal txt5 As String, ByRef msg5 As String, _
    ByVal txt6 As String, ByRef msg6 As String, _
    ByVal txt7 As String, ByRef msg7 As String, _
    ByVal txt8 As String, ByRef msg8 As String, _
    ByVal txt9 As String, ByRef msg9 As String, _
    ByVal txt99 As String, ByRef msg99 As String)

        For Each objTrCourseEval As TrCourseEvaluation In CourseEvaluationColl
            Dim Found As Boolean = False
            For i As Integer = 0 To CertificateLineColl.Count - 1
                If CertificateLineColl(i).TrCourseEvaluation.EvaluationCode = objTrCourseEval.EvaluationCode And _
                   CertificateLineColl(i).TrClassRegistration.ID = objClassReg.ID Then
                    Found = True
                    objCertificateLineForUpdate = CertificateLineColl(i)
                    Exit For
                End If
            Next
            If Found Then 'update
                If (objCertificateLineForUpdate.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)) Then
                    Select Case Right(objCertificateLineForUpdate.TrCourseEvaluation.EvaluationCode, 2)
                        Case "00"
                            If Not ValidateNumericValue(txt0, msg0, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt0)
                            End If
                        Case "01"
                            If Not ValidateNumericValue(txt1, msg1, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt1)
                            End If
                        Case "02"
                            If Not ValidateNumericValue(txt2, msg2, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt2)
                            End If
                        Case "03"
                            If Not ValidateNumericValue(txt3, msg3, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt3)
                            End If
                        Case "04"
                            If Not ValidateNumericValue(txt4, msg4, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt4)
                            End If
                        Case "05"
                            If Not ValidateNumericValue(txt5, msg5, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt5)
                            End If
                        Case "06"
                            If Not ValidateNumericValue(txt6, msg6, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt6)
                            End If
                        Case "07"
                            If Not ValidateNumericValue(txt7, msg7, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt7)
                            End If
                        Case "08"
                            If Not ValidateNumericValue(txt8, msg8, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt8)
                            End If
                        Case "09"
                            If Not ValidateNumericValue(txt9, msg9, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt9)
                            End If
                        Case "99"
                            If Not ValidateNumericValue(txt99, msg99, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt99)
                            End If
                    End Select
                End If
            Else 'insert

                Dim objCertificateLineForInsert = New TrCertificateLine
                objCertificateLineForInsert.TrClassRegistration = objClassReg
                objCertificateLineForInsert.TrCourseEvaluation = objTrCourseEval
                If (objCertificateLineForInsert.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)) Then
                    Select Case Right(objCertificateLineForInsert.TrCourseEvaluation.EvaluationCode, 2)
                        Case "00"
                            If Not ValidateNumericValue(txt0, msg0, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                Else
                                    'do not exit and insert
                                End If
                            Else
                                FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt0)
                            End If
                        Case "01"
                            If Not ValidateNumericValue(txt1, msg1, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt1)
                            End If
                        Case "02"
                            If Not ValidateNumericValue(txt2, msg2, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt2)
                            End If
                        Case "03"
                            If Not ValidateNumericValue(txt3, msg3, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt3)
                            End If
                        Case "04"
                            If Not ValidateNumericValue(txt4, msg4, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt4)
                            End If
                        Case "05"
                            If Not ValidateNumericValue(txt5, msg5, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt5)
                            End If
                        Case "06"
                            If Not ValidateNumericValue(txt6, msg6, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt6)
                            End If

                        Case "07"
                            If Not ValidateNumericValue(txt7, msg7, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt7)
                            End If
                        Case "08"
                            If Not ValidateNumericValue(txt8, msg8, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt8)
                            End If
                        Case "09"
                            If Not ValidateNumericValue(txt9, msg9, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt9)
                            End If
                        Case "99"
                            If Not ValidateNumericValue(txt99, msg99, nCheck, bValid) Then
                                If Not bValid Then
                                    Exit For
                                End If
                            Else
                                FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt99)
                            End If
                    End Select
                End If
            End If
            If Not bValid Then Exit For
        Next
    End Sub


    Private Sub GetCharacterArrayForInsertOrUpdate( _
    ByVal CourseEvaluationColl As ArrayList, _
    ByVal CertificateLineColl As ArrayList, _
    ByVal objClassReg As TrClassRegistration, _
    ByRef CertificateLineCollForInsert As ArrayList, _
    ByRef CertificateLineCollForUpdate As ArrayList, _
    ByRef objCertificateLineForUpdate As TrCertificateLine, _
    ByRef nCheck As Integer, _
    ByRef bValid As Boolean, _
    ByVal txt1 As String, ByRef msg1 As String, _
    ByVal txt2 As String, ByRef msg2 As String, _
    ByVal txt3 As String, ByRef msg3 As String, _
    ByVal txt4 As String, ByRef msg4 As String, _
    ByVal txt5 As String, ByRef msg5 As String, _
    ByVal txt6 As String, ByRef msg6 As String, _
    ByVal txt7 As String, ByRef msg7 As String, _
    ByVal txt8 As String, ByRef msg8 As String, _
     ByVal txt9 As String, ByRef msg9 As String, _
    ByVal FlagJenisEvaluasi As String)

        For Each objTrCourseEval As TrCourseEvaluation In CourseEvaluationColl
            Dim Found As Boolean = False
            For i As Integer = 0 To CertificateLineColl.Count - 1
                If CertificateLineColl(i).TrCourseEvaluation.EvaluationCode = objTrCourseEval.EvaluationCode And _
                   CertificateLineColl(i).TrClassRegistration.ID = objClassReg.ID Then
                    Found = True
                    objCertificateLineForUpdate = CertificateLineColl(i)
                    Exit For
                End If
            Next
            If Found Then 'update
                'If (objCertificateLineForUpdate.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)) Then
                Select Case Right(objCertificateLineForUpdate.TrCourseEvaluation.EvaluationCode, 2)
                    Case "01"
                        If Not ValidateNilaiSikapAndPrestasi(txt1, msg1, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt1)
                        End If
                    Case "02"
                        If Not ValidateNilaiSikapAndPrestasi(txt2, msg2, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt2)
                        End If
                    Case "03"
                        If Not ValidateNilaiSikapAndPrestasi(txt3, msg3, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt3)
                        End If
                    Case "04"
                        If Not ValidateNilaiSikapAndPrestasi(txt4, msg4, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt4)
                        End If
                    Case "05"
                        If Not ValidateNilaiSikapAndPrestasi(txt5, msg5, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt5)
                        End If
                    Case "06"
                        If Not ValidateNilaiSikapAndPrestasi(txt6, msg6, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt6)
                        End If
                    Case "07"
                        If Not ValidateNilaiSikapAndPrestasi(txt7, msg7, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt7)
                        End If
                    Case "08"
                        If Not ValidateNilaiSikapAndPrestasi(txt8, msg8, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt8)
                        End If
                    Case "09"
                        If Not ValidateNilaiSikapAndPrestasi(txt9, msg9, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForUpdate(objCertificateLineForUpdate, CertificateLineCollForUpdate, txt9)
                        End If
                End Select
                'End If
            Else 'insert

                Dim objCertificateLineForInsert = New TrCertificateLine
                objCertificateLineForInsert.TrClassRegistration = objClassReg
                objCertificateLineForInsert.TrCourseEvaluation = objTrCourseEval
                'If (objCertificateLineForInsert.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)) Then
                Select Case Right(objCertificateLineForInsert.TrCourseEvaluation.EvaluationCode, 2)
                    Case "01"
                        If Not ValidateNilaiSikapAndPrestasi(txt1, msg1, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt1)
                        End If
                    Case "02"
                        If Not ValidateNilaiSikapAndPrestasi(txt2, msg2, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt2)
                        End If
                    Case "03"
                        If Not ValidateNilaiSikapAndPrestasi(txt3, msg3, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt3)
                        End If
                    Case "04"
                        If Not ValidateNilaiSikapAndPrestasi(txt4, msg4, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt4)
                        End If
                    Case "05"
                        If Not ValidateNilaiSikapAndPrestasi(txt5, msg5, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt5)
                        End If
                    Case "06"
                        If Not ValidateNilaiSikapAndPrestasi(txt6, msg6, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt6)
                        End If

                    Case "07"
                        If Not ValidateNilaiSikapAndPrestasi(txt7, msg7, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt7)
                        End If
                    Case "08"
                        If Not ValidateNilaiSikapAndPrestasi(txt8, msg8, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt8)
                        End If
                    Case "09"
                        If Not ValidateNilaiSikapAndPrestasi(txt9, msg9, nCheck, bValid, FlagJenisEvaluasi) Then
                            If Not bValid Then
                                Exit For
                            End If
                        Else
                            FillCertificateLineCollForInsert(objCertificateLineForInsert, CertificateLineCollForInsert, txt9)
                        End If
                End Select
                'End If
            End If
            If Not bValid Then Exit For
        Next
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlJenis.SelectedIndexChanged
        If dtgClassRegistration.Visible Then
            dtgClassRegistration.DataSource = Nothing
            dtgClassRegistration.DataBind()
        ElseIf dtgClassRegistration2.Visible Then
            dtgClassRegistration2.DataSource = Nothing
            dtgClassRegistration2.DataBind()
        ElseIf dtgClassRegistration3.Visible Then
            dtgClassRegistration3.DataSource = Nothing
            dtgClassRegistration3.DataBind()
        End If

        btnSaveNumEval.Visible = False
        btnUpdateNumEval.Visible = False
        trTemplate.Visible = False
        trUpload.Visible = False

        dgNumEval.DataSource = Nothing
        dgNumEval.DataBind()
        dgNumEval.Visible = False

        ' Form the script to be registered at client side.
        SetFocus(btnRefresh)



    End Sub

    
    Private Sub BindClass(ByVal IsYearInclude As Boolean)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.CourseCode", MatchType.Exact, txtKodeKategori.Text))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "FiscalYear", MatchType.GreaterOrEqual, ddlFiscalYear.SelectedValue))

        Dim arrTest As ArrayList = New TrClassFacade(User).RetrieveActiveList(criterias, "ClassCode", Sort.SortDirection.ASC)
        If arrTest.Count <= 0 Then
            MessageBox.Show("Tidak ada kelas untuk kategori " & txtKodeKategori.Text & " pada tahun fiskal " & ddlFiscalYear.SelectedValue)

        End If
    End Sub

    
    Private Sub btnHitung_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHitung.Click
        'If Not _sessHelper.GetSession("SessIsSimpan") Is Nothing Then
        'If _sessHelper.GetSession("SessIsSimpan") Then
        Dim strTahun As String = ddlFiscalYear.SelectedValue.ToString
        Dim strCourse As String = txtKodeKategori.Text
        Dim strClassCode As String = txtClassCode.Text.Trim().ToUpper()
        Dim strJenisTest As String = ddlJenis.SelectedValue.ToString
        Dim QS As String = strTahun + ";" + strCourse + ";" + strClassCode + ";" + strJenisTest

        If AreaId.IsNullorEmpty Then
            Response.Redirect("../Training/FrmCourseEvaluationList.aspx?QS=" + QS)
        Else
            Response.Redirect("../Training/FrmCourseEvaluationList.aspx?QS=" + QS + "&area=" + AreaId)
        End If

    End Sub



#Region "DataGrid Handler"
    Private Sub BindDataCourseEvaluation(ByVal _CourseId As Integer)
        Dim _trCourseEvaluationFacade As New TrCourseEvaluationFacade(User)
        Dim _arrayCourseEvaluation As New ArrayList
        Dim crtCourseEvalParam As CriteriaComposite

        Dim srtParam As SortCollection = New SortCollection

        crtCourseEvalParam = New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'crtCourseEvalParam.opAnd(New Criteria(GetType(TrCourseEvaluation), "Type", MatchType.Exact, 0), "(", True)
        crtCourseEvalParam.opAnd(New Criteria(GetType(TrCourseEvaluation), "TrCourse", MatchType.Exact, _CourseId))
        crtCourseEvalParam.opAnd(New Criteria(GetType(TrCourseEvaluation), "Type", MatchType.Exact, ddlJenis.SelectedValue))
        srtParam.Add(New Sort(GetType(TrCourseEvaluation), "Type", Sort.SortDirection.ASC))
        srtParam.Add(New Sort(GetType(TrCourseEvaluation), "EvaluationCode", Sort.SortDirection.ASC))

        _arrayCourseEvaluation = _trCourseEvaluationFacade.Retrieve(crtCourseEvalParam, srtParam)
        If (_arrayCourseEvaluation.Count > 0) Then
            dgNumEval.Visible = True
            btnSaveNumEval.Visible = True
            btnUpdateNumEval.Visible = True
            btnSaveNumEval.Enabled = False
        End If
        dgNumEval.DataSource = _arrayCourseEvaluation
        dgNumEval.DataBind()


    End Sub


    Private Sub dgNumEval_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgNumEval.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            'Check By CourseEvaluationId and By Class Id
            Dim _chkRow As CheckBox = CType(e.Item.FindControl("chkNumEval"), CheckBox)
            Dim _lblSpecialName As TextBox = CType(e.Item.FindControl("txtSpecialName"), TextBox)

            If (Not _chkRow Is Nothing) Then
                _chkRow.Enabled = False
                _chkRow.BackColor = Color.Gray
                _chkRow.Checked = GetCheckNumEval(CType(e.Item.Cells(0).Text(), Integer))

                If (Not _lblSpecialName Is Nothing) Then
                    Dim sHelper As New SessionHelper
                    If (_chkRow.Checked) Then
                        _lblSpecialName.Text = sHelper.GetSession("SpecialName")
                        sHelper.RemoveSession("SpecialName")

                    End If
                End If
            End If

            Dim _lblType As Label = CType(e.Item.FindControl("LblType"), Label)
            If (Not _lblType Is Nothing) Then
                If (e.Item.Cells(1).Text = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)) Then
                    _lblType.Text = EnumTrEvaluationType.TrEvaluationType.Angka.ToString()
                ElseIf (e.Item.Cells(1).Text = CType(EnumTrEvaluationType.TrEvaluationType.Sikap, String)) Then
                    _lblType.Text = EnumTrEvaluationType.TrEvaluationType.Sikap.ToString()
                Else
                    _lblType.Text = EnumTrEvaluationType.TrEvaluationType.Prestasi.ToString()
                End If
            End If


        End If


    End Sub

#End Region

#Region "DataHandler"

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

    Private Sub GetCourseEvaluation(ByVal classData As TrClass)
        'Dim objTrCourse As New TrCourse
        'objTrCourse = getCourseID(ddlCourse.SelectedValue)
        'If (Not objTrCourse Is Nothing) Then
        '    BindDataCourseEvaluation(objTrCourse.ID)
        'End If
        Dim dataTraining As TrCourse = New TrCourseFacade(User).Retrieve(classData.TrCourse.ID)
        BindDataCourseEvaluation(dataTraining.ID)
    End Sub
    Private Function GetCheckNumEval(ByVal _courseEvalId As Integer, ByVal _classId As Integer) As Boolean
        Dim retBol As Boolean = False

        'Check If CourseEvaluation Is Exist on CourseNumEvaluation
        Dim _trCourseNumEvalFacade As New TrClassNumEvaluationFacade(User)

        Dim _lst As New ArrayList
        Dim crtParam As CriteriaComposite

        crtParam = New CriteriaComposite(New Criteria(GetType(TrClassNumEvaluation), "TrCourseEvaluation", MatchType.Exact, _courseEvalId))
        crtParam.opAnd(New Criteria(GetType(TrClassNumEvaluation), "TrClass", MatchType.Exact, _classId))

        _lst = _trCourseNumEvalFacade.Retrieve(crtParam)
        If _lst.Count > 0 Then
            retBol = True
            Dim sHelper As New SessionHelper
            Dim _strSpecialName As String = String.Empty
            _strSpecialName = CType(_lst(0), TrClassNumEvaluation).SpecialName()
            sHelper.SetSession("SpecialName", _strSpecialName)
        End If


        Return retBol
    End Function
    Private Function GetCheckNumEval(ByVal _courseEvalId As Integer) As Boolean
        Dim retBol As Boolean = False

        Dim _classId As TrClass = CType(_sessHelper.GetSession("SessTrClass"), TrClass)
        'Check If CourseEvaluation Is Exist on CourseNumEvaluation
        Dim _trCourseNumEvalFacade As New TrClassNumEvaluationFacade(User)

        Dim _lst As New ArrayList
        Dim crtParam As CriteriaComposite

        crtParam = New CriteriaComposite(New Criteria(GetType(TrClassNumEvaluation), "TrCourseEvaluation", MatchType.Exact, _courseEvalId))
        crtParam.opAnd(New Criteria(GetType(TrClassNumEvaluation), "TrClass", MatchType.Exact, _classId.ID))

        _lst = _trCourseNumEvalFacade.Retrieve(crtParam)


        If _lst.Count > 0 Then
            retBol = True
            Dim sHelper As New SessionHelper
            Dim _strSpecialName As String = String.Empty
            _strSpecialName = CType(_lst(0), TrClassNumEvaluation).SpecialName()
            sHelper.SetSession("SpecialName", _strSpecialName)
        End If


        Return retBol
    End Function

    Private Function GetCheckPassValue(ByVal _courseEvalId As Integer) As Boolean
        Dim retBol As Boolean = False

        Dim _classId As TrClass = CType(_sessHelper.GetSession("SessTrClass"), TrClass)
        'Check If CourseEvaluation Is Exist on CourseNumEvaluation
        Dim _trCourseNumEvalFacade As New TrClassNumEvaluationFacade(User)

        Dim _lst As New ArrayList
        Dim crtParam As CriteriaComposite

        crtParam = New CriteriaComposite(New Criteria(GetType(TrClassNumEvaluation), "TrCourseEvaluation", MatchType.Exact, _courseEvalId))
        crtParam.opAnd(New Criteria(GetType(TrClassNumEvaluation), "TrClass", MatchType.Exact, _classId.ID))

        _lst = _trCourseNumEvalFacade.Retrieve(crtParam)


        If _lst.Count > 0 Then
            retBol = True
            Dim sHelper As New SessionHelper
            Dim _strSpecialName As String = String.Empty
            _strSpecialName = CType(_lst(0), TrClassNumEvaluation).SpecialName()
            sHelper.SetSession("SpecialName", _strSpecialName)
        End If


        Return retBol
    End Function

    Sub InsertDataClassNumEval()

        'Delete All First
        'DeleteAllClassNumEval()


        Dim _trClassNumEvalFacade As TrClassNumEvaluationFacade = New TrClassNumEvaluationFacade(User)
        Dim _trCourseEvalFacade As TrCourseEvaluationFacade = New TrCourseEvaluationFacade(User)
        'Dim _trClassFacade As TrClassFacade = New TrClassFacade(User)

        Dim _trClassNumEval As TrClassNumEvaluation = New TrClassNumEvaluation
        Dim _trCourseEvaluation As TrCourseEvaluation = New TrCourseEvaluation
        'Dim _trClass As TrClass = _trClassFacade.Retrieve(CType(_sessHelper.GetSession("SessTrClass"), Integer))
        Dim _trClass As TrClass = CType(_sessHelper.GetSession("SessTrClass"), TrClass)

        For i As Integer = 0 To dgNumEval.Items.Count - 1
            Dim _checkBox As CheckBox = CType(dgNumEval.Items(i).FindControl("chkNumEval"), CheckBox)
            Dim _txtSpecialName As TextBox = CType(dgNumEval.Items(i).FindControl("txtSpecialName"), TextBox)

            If _checkBox.Checked Then
                'Insert Data
                With _trClassNumEval
                    _trCourseEvaluation = _trCourseEvalFacade.Retrieve(CType(dgNumEval.Items(i).Cells(0).Text, Integer))
                    .TrCourseEvaluation = _trCourseEvaluation
                    .TrClass = _trClass
                    .SpecialName = _txtSpecialName.Text
                End With
                Try
                    _trClassNumEvalFacade.Insert(_trClassNumEval)
                    'MessageBox.Show("Simpan Data Sukses")
                    btnSaveNumEval.Enabled = False
                Catch ex As Exception
                    'MessageBox.Show("Simpan Data Gagal")
                End Try

            End If

        Next
    End Sub

    Private Sub DeleteAllClassNumEval()
        Dim objRetVal As New TrClassNumEvaluation
        Dim _trClassNumEvalFacade As TrClassNumEvaluationFacade = New TrClassNumEvaluationFacade(User)
        Dim _lst As New ArrayList
        Dim crtParam As CriteriaComposite
        Dim _classId As TrClass = CType(_sessHelper.GetSession("SessTrClass"), TrClass)

        crtParam = New CriteriaComposite(New Criteria(GetType(TrClassNumEvaluation), "TrClass.ID", MatchType.Exact, _classId.ID))
        _lst = _trClassNumEvalFacade.Retrieve(crtParam)

        For Each item As TrClassNumEvaluation In _lst
            _trClassNumEvalFacade.DeleteFromDB(item)
        Next

    End Sub


    Sub InsertDataClassNumEval(ByVal _strType As String)

        'Delete All First
        DeleteAllClassNumEval(_strType)


        Dim _trClassNumEvalFacade As TrClassNumEvaluationFacade = New TrClassNumEvaluationFacade(User)
        Dim _trCourseEvalFacade As TrCourseEvaluationFacade = New TrCourseEvaluationFacade(User)
        'Dim _trClassFacade As TrClassFacade = New TrClassFacade(User)

        Dim _trClassNumEval As TrClassNumEvaluation = New TrClassNumEvaluation
        Dim _trCourseEvaluation As TrCourseEvaluation = New TrCourseEvaluation
        'Dim _trClass As TrClass = _trClassFacade.Retrieve(CType(_sessHelper.GetSession("SessTrClass"), Integer))
        Dim _trClass As TrClass = CType(_sessHelper.GetSession("SessTrClass"), TrClass)

        For i As Integer = 0 To dgNumEval.Items.Count - 1
            Dim _checkBox As CheckBox = CType(dgNumEval.Items(i).FindControl("chkNumEval"), CheckBox)
            Dim _txtSpecialName As TextBox = CType(dgNumEval.Items(i).FindControl("txtSpecialName"), TextBox)

            If _checkBox.Checked Then
                'Insert Data
                With _trClassNumEval
                    _trCourseEvaluation = _trCourseEvalFacade.Retrieve(CType(dgNumEval.Items(i).Cells(0).Text, Integer))
                    .TrCourseEvaluation = _trCourseEvaluation
                    .TrClass = _trClass
                    .SpecialName = _txtSpecialName.Text
                End With
                Try
                    _trClassNumEvalFacade.Insert(_trClassNumEval)
                    'MessageBox.Show("Simpan Data Sukses")
                    btnSaveNumEval.Enabled = False
                Catch ex As Exception
                    'MessageBox.Show("Simpan Data Gagal")
                End Try

            End If

        Next
    End Sub


    Private Sub DeleteAllClassNumEval(ByVal _strType As String)
        Dim objRetVal As New TrClassNumEvaluation
        Dim _trClassNumEvalFacade As TrClassNumEvaluationFacade = New TrClassNumEvaluationFacade(User)
        Dim _lst As New ArrayList
        Dim crtParam As CriteriaComposite
        Dim _classId As TrClass = CType(_sessHelper.GetSession("SessTrClass"), TrClass)

        crtParam = New CriteriaComposite(New Criteria(GetType(TrClassNumEvaluation), "TrClass.ID", MatchType.Exact, _classId.ID))
        crtParam.opAnd(New Criteria(GetType(TrClassNumEvaluation), "TrCourseEvaluation.Type", MatchType.Exact, _strType))

        _lst = _trClassNumEvalFacade.Retrieve(crtParam)

        For Each item As TrClassNumEvaluation In _lst
            _trClassNumEvalFacade.DeleteFromDB(item)
        Next

    End Sub

#End Region

    Private Sub btnSaveNumEval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveNumEval.Click
        InsertDataClassNumEval(ddlJenis.SelectedValue)
        For i As Integer = 0 To dgNumEval.Items.Count - 1
            Dim _checkBox As CheckBox = CType(dgNumEval.Items(i).FindControl("chkNumEval"), CheckBox)
            Dim _txtSpecialName As TextBox = CType(dgNumEval.Items(i).FindControl("txtSpecialName"), TextBox)

            If _checkBox.Checked Then
                _checkBox.BackColor = Color.Gray
                _checkBox.Enabled = False
            End If

        Next
        Me.btnRefresh_Click(sender, e)
    End Sub

    Private Sub btnUpdateNumEval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateNumEval.Click
        For i As Integer = 0 To dgNumEval.Items.Count - 1
            Dim _checkBox As CheckBox = CType(dgNumEval.Items(i).FindControl("chkNumEval"), CheckBox)
            Dim _txtSpecialName As TextBox = CType(dgNumEval.Items(i).FindControl("txtSpecialName"), TextBox)

            If Not (_checkBox Is Nothing) Then
                'If _checkBox.Checked Then
                _checkBox.BackColor = Color.Black
                _checkBox.Enabled = True
                'End If
            End If


        Next

        btnSaveNumEval.Enabled = True
    End Sub

#Region "Calculate and Rank"
    Private Function CalcutateAverage() As ArrayList
        Dim _trClass As TrClass = CType(_sessHelper.GetSession("SessTrClass"), TrClass)

        If Not (_trClass Is Nothing) Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ID", MatchType.Exact, CType(_trClass.ID, Short)))
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
                        If nJumlah > 0 Then
                            objTrClassRegistration.Avarage = (Total / nJumlah)
                            If (objTrClassRegistration.TrClass.TrCourse.CourseCode.IndexOf("M1") >= 0 OrElse objTrClassRegistration.TrClass.TrCourse.CourseCode.IndexOf("M2") >= 0 OrElse objTrClassRegistration.TrClass.TrCourse.CourseCode.IndexOf("M3") >= 0) Then
                                If (objTrClassRegistration.FinalTest >= PassingScore AndAlso objTrClassRegistration.Avarage >= PassingScore) Then
                                    objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Pass
                                Else
                                    objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Fail
                                End If
                            Else

                                If objTrClassRegistration.Avarage >= PassingScore Then
                                    objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Pass
                                Else
                                    objTrClassRegistration.Status = EnumTrClassRegistration.DataStatusType.Fail
                                End If
                            End If

                        End If

                    End If
                    objTrClassRegistration.MarkLoaded()
                Next

            End If

            Return arryList
        Else
            MessageBox.Show("Kelas belum dipilih ")
        End If
    End Function

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

    Private Function UpdateCollectionTrClassRegistration(ByVal arrClassReg As ArrayList) As Integer
        Try
            Return New TrClassRegistrationFacade(User).UpdateTrClassRegistrationCollection(arrClassReg)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Sub CalculateAndRank()
        Dim WorkingClassRegColl As ArrayList = New ArrayList
        WorkingClassRegColl = CalcutateAverage()
        WorkingClassRegColl = RankClassRegistration(WorkingClassRegColl)
        Dim nResult As Integer = UpdateCollectionTrClassRegistration(WorkingClassRegColl)
        If nResult = 0 Then
            'MessageBox.Show("Perhitungan rangking dan update ke database sukses !")
            'ViewState("CurrentSortColumn") = "Rank"
            'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            'Rebind()
            'BindDatagrid(0)
        Else
            MessageBox.Show(SR.UpdateFail)
        End If
    End Sub
#End Region

#Region "ChangePosition"
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

#End Region

    Private Sub SetFocus(ByVal FocusControl As Control)
        Dim Script As New System.Text.StringBuilder
        Dim ClientID As String = FocusControl.ClientID
        With Script
            .Append("<script language='javascript'>")
            .Append("document.getElementById('")
            .Append(ClientID)
            .Append("').focus();")
            .Append("</script>")
        End With
        Me.RegisterStartupScript("setFocus", Script.ToString())
    End Sub

    Private Sub btnStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStatus.Click
        Dim ClassRegColl As ArrayList = New ArrayList
        Dim FlagJenisEvaluasi As String = ""

        ClassRegColl = CType(_sessHelper.GetSession("SessClassRegColl"), ArrayList)

        Select Case ddlJenis.SelectedValue
            Case 0
                FlagJenisEvaluasi = "Angka"
            Case 1
                FlagJenisEvaluasi = "Sikap"
            Case 2
                FlagJenisEvaluasi = "Prestasi"
        End Select

        Dim chkPass As CheckBox = New CheckBox

        If ClassRegColl.Count > 0 Then
            For iterate As Integer = 0 To ClassRegColl.Count - 1
                Dim objClassReg As TrClassRegistration = CType(ClassRegColl(iterate), TrClassRegistration)
                Dim dtgItem As DataGridItem

                If FlagJenisEvaluasi = "Angka" Then
                    dtgItem = dtgClassRegistration.Items(iterate)
                    chkPass = CType(dtgItem.FindControl("chkPass"), CheckBox)
                    Dim iCheck As Integer = IIf(chkPass.Checked, 1, 2)
                    objClassReg.Status = iCheck
                ElseIf FlagJenisEvaluasi = "Sikap" Then
                    dtgItem = dtgClassRegistration2.Items(iterate)
                ElseIf FlagJenisEvaluasi = "Prestasi" Then
                    dtgItem = dtgClassRegistration3.Items(iterate)
                End If

                Dim iRet As Integer = New TrClassRegistrationFacade(User).Update(objClassReg)

            Next
            MessageBox.Show(SR.UpdateSucces)
        End If
    End Sub

    Protected Sub linkTemplate_Click(sender As Object, e As EventArgs) Handles linkTemplate.Click
        Dim dataKelas As TrClass = New TrClassFacade(User).Retrieve(hdnClassCode.Value)
        Dim tipeNilai As String = String.Empty

        Using excelPackage As New ExcelPackage()
            Dim wsData As ExcelWorksheet = excelPackage.Workbook.Worksheets.Add("Input Data Nilai")
            Dim rowIdx As Integer = 1
            wsData.Cells("A" & rowIdx.ToString()).ValueBold("Input Data Nilai")
            rowIdx += 1
            rowIdx += 1
            wsData.Cells("B" & rowIdx.ToString()).ValueBold("Kode Kelas ")
            wsData.Cells("C" & rowIdx.ToString()).ValueBold(dataKelas.ClassCode)
            wsData.Cells("E" & rowIdx.ToString()).ValueBold("Nama Kelas ")
            wsData.Cells("F" & rowIdx.ToString()).ValueBold(dataKelas.ClassName)
            rowIdx += 1
            wsData.Cells("B" & rowIdx.ToString()).ValueBold("Mulai ")
            wsData.Cells("C" & rowIdx.ToString()).ValueBold(dataKelas.StartDate.DateToString())
            wsData.Cells("E" & rowIdx.ToString()).ValueBold("Selesai ")
            wsData.Cells("F" & rowIdx.ToString()).ValueBold(dataKelas.FinishDate.DateToString())
            rowIdx += 1

            Dim dtgInputNilai As DataGrid = Nothing
            Select Case hdnJenisNilai.Value
                Case "0"
                    tipeNilai = "Angka"
                    dtgInputNilai = dtgClassRegistration
                Case "1"
                    tipeNilai = "Sikap"
                    dtgInputNilai = dtgClassRegistration2
            End Select

            Dim listDisabled As New List(Of String)
            For Each dtItem As DataGridItem In dtgInputNilai.Items
                If dtItem.IsRowItems Then
                    For idx As Integer = 1 To 9
                        Dim txtTest As TextBox = dtItem.FindTextBox("txtTest" + idx.ToString)
                        If Not txtTest.Visible Then
                            listDisabled.Add("Test " + idx.ToString)
                            listDisabled.Add("Nilai Sikap " + idx.ToString)
                        End If
                    Next
                    Exit For
                End If
            Next
            rowIdx += 1
            Dim columnIdx As Integer = 1

            For Each dtColumn As DataGridColumn In dtgInputNilai.Columns
                If dtColumn.Visible And listDisabled.IndexOf(dtColumn.HeaderText) <= -1 And dtColumn.HeaderText.NotNullorEmpty Then
                    If dtColumn.HeaderText.StartsWith("Test") And dtColumn.HeaderText.IndexOf("A") = -1 Then
                        Dim evaluationCode As String = dataKelas.TrCourse.CourseCode.ToUpper() + "-A" + CInt(dtColumn.HeaderText.Replace("Test ", "")).GenerateIncrement(2)
                        Dim crit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, dataKelas.TrCourse.ID))
                        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "EvaluationCode", MatchType.Exact, evaluationCode))
                        Dim dataEva As TrCourseEvaluation = CType(New TrCourseEvaluationFacade(User).Retrieve(crit)(0), TrCourseEvaluation)
                        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue(dataEva.Name)
                    ElseIf dtColumn.HeaderText.StartsWith("Nilai") Then
                        Dim evaluationCode As String = dataKelas.TrCourse.CourseCode.ToUpper() + "-B" + CInt(dtColumn.HeaderText.Replace("Nilai Sikap ", "")).GenerateIncrement(2)
                        Dim crit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, dataKelas.TrCourse.ID))
                        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "EvaluationCode", MatchType.Exact, evaluationCode))
                        Dim dataEva As TrCourseEvaluation = CType(New TrCourseEvaluationFacade(User).Retrieve(crit)(0), TrCourseEvaluation)
                        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue(dataEva.Name)
                    Else
                        If tipeNilai.Equals("Angka") Then
                            wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue(dtColumn.HeaderText.Replace("Catatan", "Status Lulus"))
                        Else
                            If dtColumn.HeaderText.IndexOf("Catatan") = -1 Then
                                wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue(dtColumn.HeaderText)
                            End If
                        End If

                    End If
                    columnIdx += 1
                End If
            Next
            rowIdx += 1
            For Each dtItem As DataGridItem In dtgInputNilai.Items
                If dtItem.IsRowItems Then
                    

                    Dim clmIdx As Integer = 1
                    Dim lblNo As Label = dtItem.FindLabel("lblNo")
                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblNo.Text, Style.ExcelHorizontalAlignment.Center)
                    clmIdx += 1

                    Dim lblSalesmanCode As Label = dtItem.FindLabel("lblSalesmanCode")
                    Dim lblRegCode As Label = dtItem.FindLabel("lblTrId")

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

                    Dim lblDealerName As Label = dtItem.FindLabel("lblKodeOrg")
                    wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(lblDealerName.Text, Style.ExcelHorizontalAlignment.Left)
                    clmIdx += 1

                    If tipeNilai.Equals("Angka") Then
                        Dim TxtTestAwal As TextBox = dtItem.FindTextBox("TxtTestAwal")
                        wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(TxtTestAwal.Text, Style.ExcelHorizontalAlignment.Center)
                        clmIdx += 1
                    End If

                    For idx As Integer = 1 To 9
                        Dim txtInput As TextBox = dtItem.FindTextBox("txtTest" + idx.ToString)
                        If txtInput.Visible Then
                            wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(txtInput.Text, Style.ExcelHorizontalAlignment.Center)
                            clmIdx += 1
                        End If
                    Next

                    If tipeNilai.Equals("Angka") Then
                        Dim TxtTestAkhir As TextBox = dtItem.FindTextBox("TxtTestAkhir")
                        wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue(TxtTestAkhir.Text, Style.ExcelHorizontalAlignment.Center)
                        clmIdx += 1

                        Dim chkPass As CheckBox = dtItem.FindCheckBox("chkPass")
                        If chkPass.Checked Then
                            wsData.Cells(clmIdx.ExcelColumnName & rowIdx.ToString).SetValue("Ya", Style.ExcelHorizontalAlignment.Center)
                        End If
                        clmIdx += 1
                    End If
                    If dtItem.BackColor = OrangeRed Then
                        For nCol As Integer = 1 To clmIdx - 1
                            Dim nCells As ExcelRange = wsData.Cells(nCol.ExcelColumnName & rowIdx.ToString)
                            nCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
                            nCells.Style.Fill.BackgroundColor.SetColor(Color.OrangeRed)
                        Next
                    End If
                    rowIdx += 1
                End If
            Next
            For colIdx As Integer = 2 To columnIdx - 1
                wsData.Column(colIdx).AutoFit()
            Next

            Dim fileBytes = excelPackage.GetAsByteArray()

            Dim fileName As String = String.Format("InputDataNilai_{0}_{1}.xls", tipeNilai, dataKelas.ClassCode)

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

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim fileName As String = String.Empty
        Dim minColumn As Integer = 0
        Dim DicData As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))
        Dim arrExtension() As String = {".xls", ".xlsx"}
        Dim dtgInput As DataGrid = New DataGrid

        Dim resultUpload As String = helpers.UploadFile(fileUpload, KTB.DNet.Lib.WebConfig.GetValue("GlobalUpload"), _
                                                    KTB.DNet.Lib.WebConfig.GetValue("MaximumClassUploadSize"), arrExtension)
        Dim errArr() As String = resultUpload.Split("|")
        If errArr(0).Equals("Error") Then
            MessageBox.Show(errArr(1))
            Return
        Else
            fileName = KTB.DNet.Lib.WebConfig.GetValue("SAN") & errArr(1)
        End If
        Select Case hdnJenisNilai.Value
            Case "0"
                minColumn = 7
                dtgInput = dtgClassRegistration
            Case "1"
                minColumn = 5
                dtgInput = dtgClassRegistration2
        End Select
        Dim fileInfo As FileInfo = New FileInfo(fileName)
        Using excelPkg As New ExcelPackage(fileInfo)
            Using ws As ExcelWorksheet = excelPkg.Workbook.Worksheets.Item(1)
                Dim ColumnCount As Integer = ws.Dimension.End.Column
                Dim RowCount As Integer = ws.Dimension.End.Row
                If ColumnCount < 7 And RowCount < 7 Then
                    MessageBox.Show("Format Excel Tidak Sesuai. Silahkan download template di link yang telah disediakan")
                    Exit Sub
                End If
                For idx As Integer = 7 To RowCount
                    Dim regNo As String = ws.GetCellValue(idx, 2)
                    Dim dataNilai As New List(Of String)
                    For columnIdx As Integer = 5 To ColumnCount
                        dataNilai.Add(ws.GetCellValue(idx, columnIdx))
                    Next
                    DicData.Add(regNo, dataNilai)
                Next
            End Using
        End Using

        For Each dtgItem As DataGridItem In dtgInput.Items
            Dim NoReg As String = String.Empty
            If AreaId.Equals("1") Or AreaId.Equals("3") Then
                NoReg = dtgItem.FindLabel("lblSalesmanCode").Text
            Else
                NoReg = dtgItem.FindLabel("lblTrId").Text
            End If

            Dim dtUpload As KeyValuePair(Of String, List(Of String)) = DicData.First(Function(x) x.Key.Trim = NoReg.Trim)
            If Not IsNothing(dtUpload) Then
                Dim indexList As Integer = 0
                If hdnJenisNilai.Value.Equals("0") Then
                    Dim txNilai As TextBox = dtgItem.FindTextBox("TxtTestAwal")
                    txNilai.Text = dtUpload.Value(indexList)
                    indexList += 1
                End If
                For idx As Integer = 1 To 9
                    Dim txNilai As TextBox = dtgItem.FindTextBox("txtTest" + idx.ToString())
                    If txNilai.Visible Then
                        txNilai.Text = dtUpload.Value(indexList)
                        indexList += 1
                    End If
                Next
                If hdnJenisNilai.Value.Equals("0") Then
                    Dim txNilai As TextBox = dtgItem.FindTextBox("TxtTestAkhir")
                    txNilai.Text = dtUpload.Value(indexList)
                    indexList += 1

                    Dim cbxLulus As CheckBox = dtgItem.FindCheckBox("chkPass")
                    If dtUpload.Value(indexList).ToLower = "ya" Or dtUpload.Value(indexList).ToLower = "yes" Then
                        cbxLulus.Checked = True
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub chkPass_CheckedChanged(sender As Object, e As EventArgs)
        Dim func As New TrClassRegistrationFacade(Me.User)
        Dim item As DataGridItem = CType(CType(sender, Control).Parent.Parent, DataGridItem)
        Dim ClassRegColl As List(Of TrClassRegistration) = CType(_sessHelper.GetSession("SessClassRegColl"), ArrayList).Cast(Of TrClassRegistration).ToList()
        Dim chkPass As CheckBox = item.FindCheckBox("chkPass")
        Dim lblTrId As Label = item.FindLabel("lblTrId")
        Dim datas As TrClassRegistration = ClassRegColl.FirstOrDefault(Function(x) x.TrTrainee.ID = CInt(lblTrId.Text))
        If chkPass.Checked Then
            datas.IsManualCheck = True
            datas.IsManualBy = Me.User.Identity.Name
            datas.Status = CType(EnumTrClassRegistration.DataStatusType.Pass, String)
        Else
            datas.IsManualCheck = False
            datas.IsManualBy = Me.User.Identity.Name
            datas.Status = CType(EnumTrClassRegistration.DataStatusType.Register, String)
        End If
        func.Update(datas)
    End Sub
End Class
