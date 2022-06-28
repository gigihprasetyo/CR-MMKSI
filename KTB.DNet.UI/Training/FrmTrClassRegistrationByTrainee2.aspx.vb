#Region ".NET Base Class Namespace"
Imports System.Collections
Imports System.Collections.Specialized
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
#End Region

Public Class FrmTrClassRegistrationByTrainee2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTraineeName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents lblRegSuccessNote As System.Web.UI.WebControls.Label
    Protected WithEvents dtgClassRegSuccess As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblProcessNote As System.Web.UI.WebControls.Label
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblRegFailNote As System.Web.UI.WebControls.Label
    Protected WithEvents dtgClassRegFail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblRegPassRegisterNote As System.Web.UI.WebControls.Label
    Protected WithEvents dtgClassRegPassRegister As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Private objSessionHelper As New SessionHelper
    Private Const TYPE_REF_CODE As String = "TR"
    Private Const BCC_REF_CODE As String = "BCCODE"
#End Region

#Region "Private Method"
    Private Function GetBasicCourseCode() As String
        Return CType(New ReferenceFacade(User).RetrieveActiveList(TYPE_REF_CODE, BCC_REF_CODE), Reference).Description
    End Function

    Private Sub InitiatePage()
        Dim objClassAlloc As TrClassAllocation = CType(Session.Item("objClassAlloc"), TrClassAllocation)
        Dim objTrainee As TrTrainee = CType(Session.Item("veTrainee"), TrTrainee)

        If Not IsNothing(objClassAlloc) And Not IsNothing(objClassAlloc) Then
            'fill header
            FillHeaderData(objTrainee)
            'process data: check schedule bentrok or not
            'and separate arraylist between success and fail data
            ProcessDetailData(objClassAlloc, objTrainee)
            'display data
            FillDetailData()
        Else
            Response.Redirect("../Training/FrmTrTrainee1.aspx")
        End If
    End Sub

    Private Sub FillHeaderData(ByVal objTrainee As TrTrainee)
        lblTraineeName.Text = objTrainee.Name
        lblDealerCode.Text = objTrainee.Dealer.DealerCode & " / " & objTrainee.Dealer.SearchTerm1
        lblDealerName.Text = objTrainee.Dealer.DealerName
    End Sub

    Private Sub ProcessDetailData(ByVal objAllocation As TrClassAllocation, _
    ByVal objTrainee As TrTrainee)

        lblRegPassRegisterNote.Text = ""

        Dim arlClassRegSuccess As New ArrayList
        Dim arlClassRegFail As New ArrayList
        Dim arlClassRegPassRegister As New ArrayList

        arlClassRegPassRegister = _
                   GetRegisteredClassAlreadyPassOrRegisterStatusOnOneOfCourse( _
                       objTrainee.ID, objAllocation.TrClass.TrCourse.ID)
        If arlClassRegPassRegister.Count > 0 Then
            lblRegPassRegisterNote.Text = "Maaf, siswa ini tidak dapat didaftarkan di kelas dengan kategori " + objAllocation.TrClass.TrCourse.CourseCode + ", karena sudah terdaftar dikelas lain atau sudah lulus."
            lblRegSuccessNote.Visible = False
            lblRegFailNote.Visible = False
            lblProcessNote.Text = "Klik Ubah untuk kembali ke layar sebelumnya"
            Exit Sub
        End If

        Dim arlClassAlreadyRegColl As ArrayList = GetClassAlreadyReg( _
            objTrainee.ID, objAllocation.TrClass)

        Dim objClassToReg As TrClass = objAllocation.TrClass
        Dim objTrClassRegistration As TrClassRegistration

        If arlClassAlreadyRegColl.Count > 0 Then
            'check bentrok, please notify that arlClassRegFail filling byref
            'return value of CheckClassBentrok, 1 no one class clash, -1 there's class clash, 
            '0 this class already registered before so let be it
            Dim intResult As Integer = CheckClassBentrok(arlClassAlreadyRegColl, _
            objAllocation.TrClass, arlClassRegFail)
            If intResult = 1 Then
                ' check, is it this class with this category course and
                ' this student already pass or currently register in other class
                arlClassRegPassRegister = _
                    GetRegisteredClassAlreadyPassOrRegisterStatusOnOneOfCourse( _
                        objTrainee.ID, objClassToReg.TrCourse.ID)
                If arlClassRegPassRegister.Count = 0 Then
                    objTrClassRegistration = CreateNewClassReg(objTrainee, objClassToReg)
                    arlClassRegSuccess.Add(objTrClassRegistration)
                Else
                    lblRegPassRegisterNote.Text = "Maaf, siswa ini tidak dapat didaftarkan di kelas dengan kategori " + objClassToReg.TrCourse.CourseCode + ", karena sudah terdaftar dikelas lain atau sudah lulus."
                End If
            End If
            ''check bentrok, please notify that arlClassRegFail filling byref
            'If Not IsClassBentrok(arlClassAlreadyRegColl, objAllocation.TrClass, arlClassRegFail) Then
            '    objTrClassRegistration = CreateNewClassReg(objTrainee, objClassToReg)
            '    arlClassRegSuccess.Add(objTrClassRegistration)
            'End If
        Else
            'just get it in, this is fundamental course
            If objClassToReg.TrCourse.CourseCode = GetBasicCourseCode() Then
                objTrClassRegistration = CreateNewClassReg(objTrainee, objClassToReg)
                arlClassRegSuccess.Add(objTrClassRegistration)
            Else
                objTrClassRegistration = CreateNewClassReg(objTrainee, objClassToReg)
                arlClassRegSuccess.Add(objTrClassRegistration)
            End If
        End If
        objSessionHelper.SetSession("arlClassRegSuccess", arlClassRegSuccess)
        objSessionHelper.SetSession("arlClassRegFail", arlClassRegFail)
        objSessionHelper.SetSession("arlClassRegPassRegister", arlClassRegPassRegister)
    End Sub

    Private Function GetRegisteredClassAlreadyPassOrRegisterStatusOnOneOfCourse(ByVal TraineeID As Integer, ByVal CourseID As Integer) As ArrayList
        Dim retval As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", _
            MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", _
            MatchType.Exact, TraineeID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.TrCourse.ID", _
            MatchType.Exact, CourseID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", _
            MatchType.InSet, "(" & CType(EnumTrClassRegistration.DataStatusType.Pass, Integer).ToString() + ", " + _
            CType(EnumTrClassRegistration.DataStatusType.Register, Integer).ToString() + ")"))

        Dim s As String = criterias.ToString()
        Dim objFacade As New TrClassRegistrationFacade(User)
        retval = objFacade.Retrieve(criterias)

        Return retval
    End Function

    Private Function CreateNewClassReg(ByVal TraineeToReg As TrTrainee, _
    ByVal ClassToReg As TrClass) As TrClassRegistration

        Dim objTrClassRegistration As New TrClassRegistration
        objTrClassRegistration.TrTrainee = TraineeToReg
        objTrClassRegistration.Dealer = TraineeToReg.Dealer
        objTrClassRegistration.TrClass = ClassToReg
        objTrClassRegistration.Status = CType(EnumTrClassRegistration.DataStatusType.Register, Short)
        objTrClassRegistration.RegistrationDate = DateTime.Today
        objTrClassRegistration.MarkLoaded()

        Return objTrClassRegistration
    End Function

    Private Function CheckClassBentrok(ByVal ClassAlreadyRegColl As ArrayList, _
    ByVal ClassToReg As TrClass, ByRef arlClassRegFail As ArrayList) As Integer
        Dim intResult As Integer = -1
        Dim totCounterError As Integer = 0

        For c As Integer = 0 To ClassAlreadyRegColl.Count - 1
            Dim objClassAlreadyReg As TrClassRegistration = _
                CType(ClassAlreadyRegColl(c), TrClassRegistration)

            Dim subCounterError As Integer = 0

            Dim oldStartDate As DateTime = objClassAlreadyReg.TrClass.StartDate
            Dim oldFinishDate As DateTime = objClassAlreadyReg.TrClass.FinishDate
            Dim newStartDate As DateTime = ClassToReg.StartDate
            Dim newFinishDate As DateTime = ClassToReg.FinishDate

            If objClassAlreadyReg.TrClass.ID = ClassToReg.ID Then
                Return 0 'there's class already or had been registered before
            End If

            If oldStartDate <= newStartDate And newStartDate <= oldFinishDate Then
                subCounterError = subCounterError + 1
            End If

            If oldStartDate <= newFinishDate And newFinishDate <= oldFinishDate Then
                subCounterError = subCounterError + 1
            End If

            If newStartDate <= oldStartDate And newFinishDate >= oldFinishDate Then
                subCounterError = subCounterError + 1
            End If

            If newStartDate >= oldStartDate And newFinishDate <= oldFinishDate Then
                subCounterError = subCounterError + 1
            End If

            If subCounterError > 0 Then
                totCounterError = totCounterError + 1
                arlClassRegFail.Add(objClassAlreadyReg)
            End If
        Next

        If totCounterError > 0 Then
            intResult = -1 'there's class clash
        Else
            intResult = 1
        End If

        Return intResult
    End Function

    'Private Function IsClassBentrok(ByVal ClassAlreadyRegColl As ArrayList, _
    '    ByVal ClassToReg As TrClass, ByRef arlClassRegFail As ArrayList) As Boolean

    '    Dim totCounterError As Integer = 0

    '    For c As Integer = 0 To ClassAlreadyRegColl.Count - 1
    '        Dim objClassAlreadyReg As TrClassRegistration = _
    '            CType(ClassAlreadyRegColl(c), TrClassRegistration)

    '        Dim subCounterError As Integer = 0

    '        Dim oldStartDate As DateTime = objClassAlreadyReg.TrClass.StartDate
    '        Dim oldFinishDate As DateTime = objClassAlreadyReg.TrClass.FinishDate
    '        Dim newStartDate As DateTime = ClassToReg.StartDate
    '        Dim newFinishDate As DateTime = ClassToReg.FinishDate

    '        If oldStartDate < newStartDate And newStartDate < oldFinishDate Then
    '            subCounterError = subCounterError + 1
    '        End If

    '        If oldStartDate < newFinishDate And newFinishDate < oldFinishDate Then
    '            subCounterError = subCounterError + 1
    '        End If

    '        If subCounterError > 0 Then
    '            totCounterError = totCounterError + 1
    '            arlClassRegFail.Add(objClassAlreadyReg)
    '        End If
    '    Next

    '    If totCounterError > 0 Then
    '        Return True
    '    End If

    '    Return False
    'End Function

    Private Function CreateGetClassAlreadyRegCriteria(ByVal TraineeID As Integer, _
    ByVal SearchingDate As DateTime) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee", MatchType.Exact, TraineeID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.StartDate", MatchType.GreaterOrEqual, SearchingDate))
        Return criterias
    End Function

    Private Const SearchingDistanceMonth As Integer = 1

    Private Function GetClassAlreadyReg(ByVal TraineeID As Integer, _
        ByVal ClassToReg As TrClass) As ArrayList

        'Dim dtStartDateClassToReg As DateTime = ClassToReg.StartDate
        Dim dtSearchingDate As DateTime = ClassToReg.StartDate.AddMonths(-1).AddDays(-1)

        Return New TrClassRegistrationFacade(User).Retrieve( _
            CreateGetClassAlreadyRegCriteria(TraineeID, dtSearchingDate))
    End Function

    Private Sub FillDetailData()
        Dim arlClassRegSuccess As ArrayList = CType(Session.Item("arlClassRegSuccess"), ArrayList)
        Dim arlClassRegFail As ArrayList = CType(Session.Item("arlClassRegFail"), ArrayList)
        Dim arlClassRegPassRegister As ArrayList = CType(Session.Item("arlClassRegPassRegister"), ArrayList)

        dtgClassRegSuccess.DataSource = arlClassRegSuccess
        dtgClassRegSuccess.DataBind()

        dtgClassRegFail.DataSource = arlClassRegFail
        dtgClassRegFail.DataBind()

        dtgClassRegPassRegister.DataSource = arlClassRegPassRegister
        dtgClassRegPassRegister.DataBind()

        If Not arlClassRegSuccess Is Nothing Then
            If arlClassRegSuccess.Count > 0 Then
                SetVisibleDataGrid(dtgClassRegSuccess, lblRegSuccessNote, True)
            Else
                SetVisibleDataGrid(dtgClassRegSuccess, lblRegSuccessNote, False)
            End If
        End If
       
        If Not arlClassRegFail Is Nothing Then
            If arlClassRegFail.Count > 0 Then
                SetVisibleDataGrid(dtgClassRegFail, lblRegFailNote, True)
            Else
                SetVisibleDataGrid(dtgClassRegFail, lblRegFailNote, False)
            End If
        End If

        If Not arlClassRegPassRegister Is Nothing Then
            If arlClassRegPassRegister.Count > 0 Then
                SetVisibleDataGrid(dtgClassRegPassRegister, lblRegPassRegisterNote, True)
            Else
                SetVisibleDataGrid(dtgClassRegPassRegister, lblRegPassRegisterNote, False)
            End If
        End If

        If Not arlClassRegSuccess Is Nothing And Not arlClassRegFail Is Nothing And Not arlClassRegPassRegister Is Nothing Then

            If arlClassRegSuccess.Count = 0 And arlClassRegFail.Count = 0 And arlClassRegPassRegister.Count = 0 Then
                lblErrorMessage.Text = "Tidak ada perubahan data, karena siswa sudah terdaftar pada kelas tersebut"
                btnSubmit.Enabled = False
            Else
                If arlClassRegSuccess.Count = 0 Then
                    btnSubmit.Enabled = False
                Else
                    btnSubmit.Enabled = True
                End If

            End If
        Else
            btnSubmit.Enabled = False
        End If
        
    End Sub

    Private Sub SetVisibleDataGrid(ByVal ControlGrid As DataGrid, ByVal ControlNote As Label, _
        ByVal IsVisible As Boolean)
        ControlGrid.Visible = IsVisible
        ControlNote.Visible = IsVisible
        lblErrorMessage.Text = ""
    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
        End If
    End Sub

    Private Sub dtgClassRegSuccess_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegSuccess.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            CType(e.Item.FindControl("lblNo1"), Label).Text = CType(e.Item.ItemIndex + 1, String)
        End If

    End Sub

    Private Sub dtgClassRegFail_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegFail.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            CType(e.Item.FindControl("lblNo2"), Label).Text = CType(e.Item.ItemIndex + 1, String)
        End If
    End Sub

    Private Sub dtgClassRegPassRegister_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegPassRegister.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            CType(e.Item.FindControl("lblNo3"), Label).Text = CType(e.Item.ItemIndex + 1, String)
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim arlClassRegSuccess As ArrayList = CType(Session.Item("arlClassRegSuccess"), ArrayList)
        Dim arlClassRegCancel As New ArrayList

        Dim objRegistrationFacade As New TrClassRegistrationFacade(User)
        If objRegistrationFacade.Insert(arlClassRegSuccess, arlClassRegCancel) > 0 Then
            Page.RegisterStartupScript("", "<script language=JavaScript> RedirectAfterSave(); </script>")
            'MessageBox.Show(SR.SaveSuccess)
            'btnSubmit.Enabled = False
            'Response.Redirect("../Training/FrmTrTrainee1.aspx")
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../Training/FrmTrClassRegistrationByTrainee1.aspx")
    End Sub
#End Region

End Class
