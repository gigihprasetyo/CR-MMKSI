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
'Modified by AAR-04/08/2008 Request by Rina A
Imports System.Globalization.CultureInfo
Imports GlobalExtensions

Public Class FrmDownLoadEHT
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents pnlDealer As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlCriteria As System.Web.UI.WebControls.Panel
    Protected WithEvents lblKodeKelas As System.Web.UI.WebControls.Label
    Protected WithEvents lblClassCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaKelas As System.Web.UI.WebControls.Label
    Protected WithEvents lblClassName As System.Web.UI.WebControls.Label
    Protected WithEvents lblMulai As System.Web.UI.WebControls.Label
    Protected WithEvents lblStart As System.Web.UI.WebControls.Label
    Protected WithEvents lblSelesai As System.Web.UI.WebControls.Label
    Protected WithEvents lblFinish As System.Web.UI.WebControls.Label
    Protected WithEvents dtgClassRegistration As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private _sessHelper As SessionHelper = New SessionHelper


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.EnableViewState = False
        initialPage()
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("Data Status Siswa.xls").Append("""").ToString()

        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
    End Sub
    Private Sub dtgClassRegistration_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegistration.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            BoundRowItems(e)
            CType(e.Item.FindControl("lblNo"), Label).Text = e.CreateNumberPage
        End If
    End Sub

    Private Sub BoundRowItems(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objTrClassRegistration As TrClassRegistration = CType(CType(dtgClassRegistration.DataSource, ArrayList)(e.Item.ItemIndex), TrClassRegistration)
        If Not IsNothing(objTrClassRegistration) Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                Dim isSubmitted As Boolean = IIf(objTrClassRegistration.TrClass.SubmitStatus = 1, True, False)
                'tampilkan Rata-rata
                
                If objTrClassRegistration.Avarage >= 0 Then
                    'Modified by AAR-04/08/2008 Request by Rina A
                    'CType(e.Item.FindControl("lblAverage"), Label).Text = objTrClassRegistration.Avarage.ToString("#.##")
                    If isSubmitted Then
                        CType(e.Item.FindControl("lblAverage"), Label).Text = objTrClassRegistration.Avarage.ToString("#.##", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                    Else
                        CType(e.Item.FindControl("lblAverage"), Label).Text = ""
                    End If

                Else
                    If CType(e.Item.FindControl("lblAverage"), Label).Text = "" Then
                        CType(e.Item.FindControl("lblAverage"), Label).Text = "-"
                    End If
                End If

                'Ranking
                If objTrClassRegistration.Rank <> Nothing Then
                    If isSubmitted Then
                        CType(e.Item.FindControl("lblRank"), Label).Text = objTrClassRegistration.Rank.ToString("###")
                    Else
                        CType(e.Item.FindControl("lblRank"), Label).Text = ""
                    End If
                End If
                'status kelulusan
                If objTrClassRegistration.Status <> "" Then
                    If objTrClassRegistration.TrClass.SubmitStatus = 1 Then
                        Select Case objTrClassRegistration.Status
                            Case EnumTrClassRegistration.DataStatusType.Pass
                                CType(e.Item.FindControl("lblStatus"), Label).Text = "Lulus"
                            Case EnumTrClassRegistration.DataStatusType.Fail
                                CType(e.Item.FindControl("lblStatus"), Label).Text = "Tidak Lulus"
                        End Select
                    Else
                        CType(e.Item.FindControl("lblStatus"), Label).Text = ""
                    End If
                End If
                If CType(e.Item.FindControl("lblInitial"), Label).Text = "" Then
                    'Modified by AAR-04/08/2008 Request by Rina A
                    'CType(e.Item.FindControl("lblInitial"), Label).Text = objTrClassRegistration.InitialTest.ToString("#.##")
                    CType(e.Item.FindControl("lblInitial"), Label).Text = objTrClassRegistration.InitialTest.ToString("#.##", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                End If

                If CType(e.Item.FindControl("lblFinal"), Label).Text = "" Then
                    'Modified by AAR-04/08/2008 Request by Rina A
                    'CType(e.Item.FindControl("lblFinal"), Label).Text = objTrClassRegistration.FinalTest.ToString("#.##")
                    CType(e.Item.FindControl("lblFinal"), Label).Text = objTrClassRegistration.FinalTest.ToString("#.##", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                End If
                'periksa dan tandai jika ada perubahan data nilai
                DetectAverageChanges(objTrClassRegistration, e, isSubmitted)
                'untuk versi terbaru nilai test1, test2 ... ditampilkan
                DisplayTest(e, objTrClassRegistration)
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
                        'Modified by AAR-04/08/2008 Request by Rina A
                        'CType(e.Item.FindControl("lblTest1"), Label).Text = CType(objTrClassRegistration.Test1, String)
                        CType(e.Item.FindControl("lblTest1"), Label).Text = objTrClassRegistration.Test1.ToString("#.##", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "02" Then
                        objTrClassRegistration.Test2 = obj.NumTestResult
                        'Modified by AAR-04/08/2008 Request by Rina A
                        'CType(e.Item.FindControl("lblTest2"), Label).Text = CType(objTrClassRegistration.Test2, String)
                        CType(e.Item.FindControl("lblTest2"), Label).Text = objTrClassRegistration.Test2.ToString("#.##", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "03" Then
                        objTrClassRegistration.Test3 = obj.NumTestResult
                        'Modified by AAR-04/08/2008 Request by Rina A
                        'CType(e.Item.FindControl("lblTest3"), Label).Text = CType(objTrClassRegistration.Test3, String)
                        CType(e.Item.FindControl("lblTest3"), Label).Text = objTrClassRegistration.Test3.ToString("#.##", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "04" Then
                        objTrClassRegistration.Test4 = obj.NumTestResult
                        'Modified by AAR-04/08/2008 Request by Rina A
                        'CType(e.Item.FindControl("lblTest4"), Label).Text = CType(objTrClassRegistration.Test4, String)
                        CType(e.Item.FindControl("lblTest4"), Label).Text = objTrClassRegistration.Test4.ToString("#.##", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "05" Then
                        objTrClassRegistration.Test5 = obj.NumTestResult
                        'Modified by AAR-04/08/2008 Request by Rina A
                        'CType(e.Item.FindControl("lblTest5"), Label).Text = CType(objTrClassRegistration.Test5, String)
                        CType(e.Item.FindControl("lblTest5"), Label).Text = objTrClassRegistration.Test5.ToString("#.##", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "06" Then
                        objTrClassRegistration.Test6 = obj.NumTestResult
                        'Modified by AAR-04/08/2008 Request by Rina A
                        'CType(e.Item.FindControl("lblTest6"), Label).Text = CType(objTrClassRegistration.Test6, String)
                        CType(e.Item.FindControl("lblTest6"), Label).Text = objTrClassRegistration.Test6.ToString("#.##", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                    End If
                    If Right(obj.TrCourseEvaluation.EvaluationCode, 2) = "07" Then
                        objTrClassRegistration.Test7 = obj.NumTestResult
                        'Modified by AAR-04/08/2008 Request by Rina A
                        'CType(e.Item.FindControl("lblTest7"), Label).Text = CType(objTrClassRegistration.Test7, String)
                        CType(e.Item.FindControl("lblTest7"), Label).Text = objTrClassRegistration.Test7.ToString("#.##", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub DetectAverageChanges(ByVal objTrClassRegistration As TrClassRegistration, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, ByVal isSubmitted As Boolean)

        Dim critCourseEval As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCourseEval.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, objTrClassRegistration.TrClass.TrCourse.ID))
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
                If isSubmitted Then
                    If objTrClassRegistration.Avarage <> (Total / nJumlah) Or bCheck Then
                        If objTrClassRegistration.Avarage <> (Total / nJumlah) Then
                            'CType(e.Item.FindControl("lblAverage"), Label).Text = objTrClassRegistration.Avarage.ToString("#.##")
                            CType(e.Item.FindControl("lblAverage"), Label).Text = objTrClassRegistration.Avarage.ToString("#.##", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                            objTrClassRegistration.Avarage = (Total / nJumlah)
                        End If
                        CType(e.Item.FindControl("lblAverage"), Label).ForeColor = Red
                        CType(e.Item.FindControl("lblAverage"), Label).ToolTip = "Ada perubahan data, nilai rata-rata perlu dihitung ulang"
                        ViewState("NeedReCalculate") = True
                    End If
                Else
                    CType(e.Item.FindControl("lblAverage"), Label).Text = ""
                End If
            End If
        End If

    End Sub

    Private Sub initialPage()
        If Not _sessHelper.GetSession("sessForDownLoad_dtgClassRegistration") Is Nothing Then
            dtgClassRegistration.DataSource = _sessHelper.GetSession("sessForDownLoad_dtgClassRegistration")
            dtgClassRegistration.DataBind()
        End If

        If Not _sessHelper.GetSession("sessForDownLoad_ClassCode") Is Nothing Then
            lblClassCode.Text = _sessHelper.GetSession("sessForDownLoad_ClassCode")
        End If

        If Not _sessHelper.GetSession("sessForDownLoad_ClassName") Is Nothing Then
            lblClassName.Text = _sessHelper.GetSession("sessForDownLoad_ClassName")
        End If

        If Not _sessHelper.GetSession("sessForDownLoad_Start") Is Nothing Then
            lblStart.Text = _sessHelper.GetSession("sessForDownLoad_Start")
        End If

        If Not _sessHelper.GetSession("sessForDownLoad_Finish") Is Nothing Then
            lblFinish.Text = _sessHelper.GetSession("sessForDownLoad_Finish")
        End If

    End Sub

End Class
