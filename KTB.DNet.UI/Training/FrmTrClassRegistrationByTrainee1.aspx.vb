#Region ".NET Base Class Namespace"
Imports System.Text
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

Public Class FrmTrClassRegistrationByTrainee1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgClassAllocation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTraineeName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnDaftar As System.Web.UI.WebControls.Button
    Protected WithEvents lblNote As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label

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
    Private counterHistory As Integer
    Private objSessionHelper As New SessionHelper
    Private Const TYPE_REF_CODE As String = "TR"
    Private Const BCC_REF_CODE As String = "BCCODE"
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
        End If
    End Sub

#Region "Private Method"
    Private Function GetBasicCourseCode() As String
        Return CType(New ReferenceFacade(User).RetrieveActiveList(TYPE_REF_CODE, BCC_REF_CODE), Reference).Description
    End Function

    Private Function GetBasicCourseID(ByVal CourseCode As String, _
        ByVal objTrainee As TrTrainee) As Integer
        Dim objCourse As TrCourse = CType(New TrCourseFacade(User).Retrieve(CourseCode), TrCourse)
        If Not objCourse Is Nothing Then
            Return objCourse.ID
        End If
        Return 0
    End Function

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "TrClass.StartDate"
        ViewState("CurrentSortDirect") = "ASC" 'Sort.SortDirection.ASC
        Dim CourseInset As String = String.Empty
        Dim ClassAlreadyReg As ArrayList = New ArrayList

        If Not IsNothing(Session.Item("veTrainee")) Then
            Dim objTrainee As TrTrainee = CType(Session.Item("veTrainee"), TrTrainee)
            If Not IsNothing(objTrainee) Then
                FillHeaderForm(objTrainee)
                LoadClass(objTrainee.ID)
                'ProcessDataForDetailForm(CourseInset, ClassAlreadyReg, objTrainee)
                'FillDetailForm(CourseInset, ClassAlreadyReg, objTrainee)
            End If
        Else
            Response.Redirect("../Training/FrmTrTrainee1.aspx")
        End If
    End Sub

    Private Sub LoadClass(ByVal traineeID As Integer)
        Dim arlClassAllocation As ArrayList = New TrClassAllocationFacade(User).RetrieveTrClassAllocationByTraineeID(traineeID)

        objSessionHelper.SetSession("arlClassAlloc", arlClassAllocation)

        BindDataGrid(dtgClassAllocation.CurrentPageIndex)
    End Sub

    Private Function GenerateCourseInSetForGetClass(ByVal CourseStringColl As ArrayList) As String
        Dim courseidColl As String = String.Empty
        If CourseStringColl.Count > 0 Then
            For x As Integer = 0 To CourseStringColl.Count - 1
                courseidColl = courseidColl & "'" & CType(CourseStringColl(x), String) & "'"
                If x <> CourseStringColl.Count - 1 Then
                    courseidColl = courseidColl & ","
                End If
            Next
        Else
            courseidColl = "'" & courseidColl & "'"
        End If
        Return courseidColl
    End Function

    Private Function FilterCourseAlreadyRegIntoCourseInsetColl(ByVal CourseInsetColl As ArrayList, _
    ByVal ClassAlreadyRegColl As ArrayList) As ArrayList
        Dim arlTemp As ArrayList = New ArrayList
        For Each CourseString As String In CourseInsetColl
            Dim isNotYetRegBefore As Boolean = True
            For Each objClassReg As TrClassRegistration In ClassAlreadyRegColl
                'if course that trainee already register or pass before don't add it
                If CInt(CourseString) = objClassReg.TrClass.TrCourse.ID And _
                    objClassReg.Status <> CType(EnumTrClassRegistration.DataStatusType.Fail, Short).ToString() Then
                    isNotYetRegBefore = False
                    Exit For
                End If
            Next
            If isNotYetRegBefore Then
                arlTemp.Add(CourseString)
            End If
        Next
        Return arlTemp
    End Function

    Private Sub ProcessDataForDetailForm(ByRef CourseInset As String, _
        ByRef ClassAlreadyRegByTrainee As ArrayList, ByVal objTrainee As TrTrainee)
        'all checking and filtering in this function is "course level"
        Try
            'arlCourseInSetColl uses to collect all course that allowed to reg by trainee
            Dim arlCourseInSetColl As ArrayList = New ArrayList
            'intBasicCourseID uses to get basic course id, this value get from table references
            'the default is M1
            Dim intBasicCourseID As Integer = _
                GetBasicCourseID(GetBasicCourseCode(), objTrainee)
            'arlClassReg uses to get all classes that has been registered before
            'it means not only with status register but all status (register,pass,fail)
            ClassAlreadyRegByTrainee = GetClassRegCollNEw(objTrainee.ID)
            Dim strPreRequireInSet As String = String.Empty

            If ClassAlreadyRegByTrainee.Count > 0 Then
                'convert courses from classregistration to mode:inset string for criteriacomposite
                strPreRequireInSet = GenerateCourseInSetForGetPreRequire(ClassAlreadyRegByTrainee)
                'get collection of course from prerequire by prequirecourseid = collection of course
                Dim arlPreRequire As ArrayList = GetPreRequireColl(strPreRequireInSet)
                If arlPreRequire.Count > 0 Then
                    'filter arlprerequire to clean up prerequire data (see more detail exp. in function)
                    Dim arlFilteredPreRequire As ArrayList = FilterPreRequireInSet(ClassAlreadyRegByTrainee, arlPreRequire)
                    'add courses from prerequire into arlCourseInSetColl
                    AddPreReqCollIntoCourseInsetColl(arlCourseInSetColl, arlFilteredPreRequire)
                End If
            Else
                If intBasicCourseID > 0 Then
                    'add basic courses into arlCourseInSetColl
                    AddBasicClassIntoCourseInsetColl(arlCourseInSetColl, intBasicCourseID)
                End If
            End If
            'add courses without pre require condition into arlCourseInSetColl
            AddCourseWOPreRequireIntoCourseInsetColl(arlCourseInSetColl, _
                GetAllActiveCourse(), intBasicCourseID, objTrainee)

            'remove all courses that has been registered by trainee before
            Dim arlFilteredCourseInsetColl As ArrayList = FilterCourseAlreadyRegIntoCourseInsetColl( _
                arlCourseInSetColl, ClassAlreadyRegByTrainee)

            'generate courses into mode:inset string for criteriacomposite
            CourseInset = "(" & GenerateCourseInSetForGetClass(arlFilteredCourseInsetColl) & ")"

        Catch ex As Exception
            MessageBox.Show(SR.ViewFail)
        End Try
    End Sub

    Private Function CriteriaGetAllActiveCourse() As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourse), _
            "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrCourse), _
            "Status", MatchType.Exact, CType(EnumTrDataStatus.DataStatusType.Active, Short).ToString()))
        Return criterias
    End Function

    Private Function GetAllActiveCourse() As ArrayList
        Return New TrCourseFacade(User).Retrieve(CriteriaGetAllActiveCourse)
    End Function

    Private Sub AddCourseWOPreRequireIntoCourseInsetColl(ByRef CourseInsetColl As ArrayList, _
        ByVal AllCourseColl As ArrayList, ByVal BasicCourseID As Integer, _
        ByVal objTrainee As TrTrainee)
        If AllCourseColl.Count > 0 Then
            For x As Integer = 0 To AllCourseColl.Count - 1
                Dim objCourse As TrCourse = CType(AllCourseColl(x), TrCourse)
                'get into collection that course didn't have arraylist of trprerequire
                'and not a basic course id
                If objCourse.TrPreRequires.Count = 0 Then
                    CourseInsetColl.Add(objCourse.ID.ToString())
                End If
            Next
        End If
    End Sub

    Private Function FilterPreRequireInSet(ByVal arlClassReg As ArrayList, _
           ByVal arlPreRequire As ArrayList) As ArrayList
        'first filter, check if there's several prerequirecourse in one course
        Dim arlResultFilter1 As ArrayList = FilterPreRequire1(arlClassReg, arlPreRequire)
        'second filter, remove prerequire object from trainee class course has been registered before
        Dim arlResultFilter2 As ArrayList = FilterPreRequire2(arlClassReg, arlResultFilter1)
        Return arlResultFilter2
    End Function

    Private Function CriteriaFilterPreRequire1(ByVal CourseID As Integer) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrPreRequire), _
            "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrPreRequire), _
            "TrCourse.ID", MatchType.Exact, CourseID))
        Return criterias
    End Function

    Private Function FilterPreRequire1(ByVal arlClassReg As ArrayList, _
        ByVal arlPreRequire As ArrayList) As ArrayList
        Dim arlTemp As New ArrayList
        For x As Integer = 0 To arlPreRequire.Count - 1
            Dim objPreRequire As TrPreRequire = CType(arlPreRequire(x), TrPreRequire)
            Dim arlMultiplePreRequire As ArrayList = New TrPreRequireFacade(User).Retrieve( _
                CriteriaFilterPreRequire1(objPreRequire.TrCourse.ID))
            'for course with multiple prerequire, must check first, all class registration course already in it or not
            If arlMultiplePreRequire.Count > 1 Then
                If CheckCourseCanAdd(arlMultiplePreRequire, arlClassReg) Then
                    arlTemp.Add(objPreRequire)
                End If
            Else
                'for course with one prerequire just add in
                arlTemp.Add(objPreRequire)
            End If
        Next
        Return arlTemp
    End Function

    Private Function CheckCourseCanAdd(ByVal arlMultiplePreRequireCourse As ArrayList, _
        ByVal arlClassReg As ArrayList) As Boolean

        Dim strMulti As String = GetStringTrPreRequireID(arlMultiplePreRequireCourse)
        Dim strClassReg As String = GetStringClassRegID(arlClassReg)

        Dim intOk As Integer = 0
        For x As Integer = 0 To arlMultiplePreRequireCourse.Count - 1
            Dim objMultiplePreRequire As TrPreRequire = CType(arlMultiplePreRequireCourse(x), TrPreRequire)
            For y As Integer = 0 To arlClassReg.Count - 1
                Dim objClassReg As TrClassRegistration = CType(arlClassReg(y), TrClassRegistration)
                If objMultiplePreRequire.PreRequireCourseID = objClassReg.TrClass.TrCourse.ID Then
                    intOk = intOk + 1
                End If
            Next
        Next
        If intOk = arlMultiplePreRequireCourse.Count Then
            Return True
        End If
        Return False
    End Function

    Private Function GetStringTrPreRequireID(ByVal arr As ArrayList) As String
        Dim str As String = ""
        For x As Integer = 0 To arr.Count - 1
            Dim obj As TrPreRequire = CType(arr(x), TrPreRequire)
            If x = 0 Then
                str = obj.ID.ToString
            Else
                str = str + "," + obj.ID.ToString
            End If
        Next
        Return str
    End Function

    Private Function GetStringClassRegID(ByVal arr As ArrayList) As String
        Dim str As String = ""
        For x As Integer = 0 To arr.Count - 1
            Dim obj As TrClassRegistration = CType(arr(x), TrClassRegistration)
            If x = 0 Then
                str = obj.ID.ToString
            Else
                str = str + "," + obj.ID.ToString
            End If
        Next
        Return str
    End Function

    Private Function FilterPreRequire2(ByVal arlClassReg As ArrayList, _
        ByVal arlResultFilter1 As ArrayList) As ArrayList
        Dim arlTemp As New ArrayList
        For x As Integer = 0 To arlResultFilter1.Count - 1
            Dim objPreRequire As TrPreRequire = CType(arlResultFilter1(x), TrPreRequire)
            Dim isAlreadyRegBefore As Boolean = True
            For y As Integer = 0 To arlClassReg.Count - 1
                Dim objClassReg As TrClassRegistration = CType(arlClassReg(y), TrClassRegistration)
                If objPreRequire.TrCourse.ID = objClassReg.TrClass.TrCourse.ID Then
                    isAlreadyRegBefore = False
                    Exit For
                End If
            Next
            If isAlreadyRegBefore Then
                arlTemp.Add(objPreRequire)
            End If
        Next
        Return arlTemp
    End Function

    Private Function FilterRequireWorkDate(ByVal ClassAllocationColl As ArrayList, _
        ByVal objTrainee As TrTrainee) As ArrayList
        Dim arlTemp As New ArrayList
        For Each objAllocation As TrClassAllocation In ClassAllocationColl
            If objAllocation.TrClass.TrCourse.RequireWorkDate Then
                If IsWorkMoreThanAYear(objTrainee, objAllocation.TrClass) Then
                    arlTemp.Add(objAllocation)
                End If
            Else
                arlTemp.Add(objAllocation)
            End If
        Next
        Return arlTemp
    End Function

    Private Function FilterClassAlreadyReg(ByVal ResultFilterWorkDate As ArrayList, _
        ByVal ClassAlreadyReg As ArrayList)
        Dim arlTemp As New ArrayList

        For Each objFilteredAllocation As TrClassAllocation In ResultFilterWorkDate
            Dim isNotYetRegBefore As Boolean = True
            For Each objClassAlreadyReg As TrClassRegistration In ClassAlreadyReg
                If objFilteredAllocation.TrClass.ID = objClassAlreadyReg.TrClass.ID And _
                    isNotYetRegBefore = False Then
                    Exit For
                End If
            Next
            If isNotYetRegBefore Then
                arlTemp.Add(objFilteredAllocation)
            End If
        Next

        Return arlTemp
    End Function

    Private Function GetTrainee(ByVal TraineeID As Integer) As TrTrainee
        Dim objTraineeFacade As New TrTraineeFacade(User)
        Return objTraineeFacade.Retrieve(TraineeID)
    End Function

    Private Sub FillHeaderForm(ByVal objTrainee As TrTrainee)
        lblTraineeName.Text = objTrainee.Name
        lblDealerCode.Text = objTrainee.Dealer.DealerCode & " / " & objTrainee.Dealer.SearchTerm1
        lblDealerName.Text = objTrainee.Dealer.DealerName
    End Sub

    Private Function CreateGetClassRegCriteria(ByVal TraineeID As Integer) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), _
            "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), _
            "TrTrainee.ID", MatchType.Exact, TraineeID))
        Return criterias
    End Function


    Private Function CreateGetClassRegCriteriaNew(ByVal TraineeID As Integer) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), _
            "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), _
            "TrTrainee.ID", MatchType.Exact, TraineeID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), _
                  "Status", MatchType.Exact, CInt(EnumTrClassRegistration.DataStatusType.Pass)))

        Return criterias
    End Function


    Private Function GetClassRegColl(ByVal TraineeID As Integer) As ArrayList
        Dim objClassRegFacade As New TrClassRegistrationFacade(User)
        Return objClassRegFacade.Retrieve(CreateGetClassRegCriteria(TraineeID))
    End Function

    Private Function GetClassRegCollNEw(ByVal TraineeID As Integer) As ArrayList
        Dim objClassRegFacade As New TrClassRegistrationFacade(User)
        Return objClassRegFacade.Retrieve(CreateGetClassRegCriteriaNew(TraineeID))
    End Function

    Private Function GenerateCourseInSetForGetPreRequire(ByVal ClassRegColl As ArrayList) As String
        Dim courseidColl As String = String.Empty
        If ClassRegColl.Count > 0 Then
            For x As Integer = 0 To ClassRegColl.Count - 1
                Dim objClassReg As TrClassRegistration = CType(ClassRegColl(x), TrClassRegistration)
                courseidColl = courseidColl & "'" & CType(objClassReg.TrClass.TrCourse.ID, String) & "'"
                If x <> ClassRegColl.Count - 1 Then
                    courseidColl = courseidColl & ","
                End If
            Next
            courseidColl = "(" & courseidColl & ")"
        Else
            courseidColl = "('" & courseidColl & "')"
        End If
        Return courseidColl
    End Function

    Private Function CreateGetPreRequireCriteria(ByVal preRequireCourseColl As String) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrPreRequire), _
            "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrPreRequire), _
            "PreRequireCourseID", MatchType.InSet, preRequireCourseColl))
        Return criterias
    End Function

    Private Function GetPreRequireColl(ByVal classregCourseColl As String) As ArrayList
        Dim objPreRequireFacade As New TrPreRequireFacade(User)
        Return objPreRequireFacade.Retrieve(CreateGetPreRequireCriteria(classregCourseColl))
    End Function

    Private Sub AddPreReqCollIntoCourseInsetColl(ByRef CourseInsetColl As ArrayList, _
        ByVal PreRequireColl As ArrayList)
        Dim courseidColl As String = ""
        If PreRequireColl.Count > 0 Then
            For x As Integer = 0 To PreRequireColl.Count - 1
                Dim objPreRequire As TrPreRequire = CType(PreRequireColl(x), TrPreRequire)
                CourseInsetColl.Add(objPreRequire.TrCourse.ID.ToString())
            Next
        End If
    End Sub

    Private Sub AddBasicClassIntoCourseInsetColl(ByRef CourseInsetColl As ArrayList, _
    ByVal BasicCourseID As Integer)
        If BasicCourseID <> 0 Then
            CourseInsetColl.Add(BasicCourseID.ToString())
        End If
    End Sub

    Private Function CreateGetClassAllocationCriteria(ByVal CourseInset As String) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), _
            "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.TrCourse.ID", MatchType.InSet, CourseInset))

        'get only category and class with status active
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.Status", MatchType.Exact, CType(EnumTrDataStatus.DataStatusType.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.TrCourse.Status", MatchType.Exact, CType(EnumTrDataStatus.DataStatusType.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.InSet, "(select id from TrClass where year(StartDate) in (Year(getdate()),Year(getdate()) + 1))"))

        '##Modified on May 16, 2006: DealerID get from veTrainee session
        'Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        'If Not IsNothing(objDealer) Then
        '    criterias.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer", MatchType.Exact, objTrainee.Dealer.ID))
        'End If
        Dim objTrainee As TrTrainee = CType(Session.Item("veTrainee"), TrTrainee)
        If Not IsNothing(objTrainee) Then
            criterias.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer", MatchType.Exact, objTrainee.Dealer.ID))
        End If

        Return criterias
    End Function

    Private Sub FillDetailForm(ByVal CourseInsetColl As String, _
        ByVal ClassAlreadyRegByTrainee As ArrayList, ByVal objTrainee As TrTrainee)
        'adding: all checking and filtering in this function is "class level"
        Dim totalRow As Integer = 0
        If CourseInsetColl <> String.Empty Then
            Try
                Dim arlClassAllocation As ArrayList = New TrClassAllocationFacade(User).Retrieve( _
                    CreateGetClassAllocationCriteria(CourseInsetColl))

                'filter working date all classes that has been queries before,
                'must filter in here because working date check vs class - startdate 
                Dim arlResultFilter1 As ArrayList = _
                    FilterRequireWorkDate(arlClassAllocation, objTrainee)

                'filter trainee can't register if they already reg before, 
                'just in case user change the period of class then want to reg again, 
                'meanwhile this trainee already get pass or fail status
                Dim arlResultFilter2 As ArrayList
                If ClassAlreadyRegByTrainee.Count > 0 Then
                    arlResultFilter2 = _
                        FilterClassAlreadyReg(arlResultFilter1, ClassAlreadyRegByTrainee)
                    objSessionHelper.SetSession("arlClassAlloc", arlResultFilter2)
                Else
                    objSessionHelper.SetSession("arlClassAlloc", arlResultFilter1)
                End If

                dtgClassAllocation.CurrentPageIndex = 0

                SortListControl(arlClassAllocation, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), String))
                objSessionHelper.SetSession("arlClassAlloc", arlClassAllocation)

                BindDataGrid(dtgClassAllocation.CurrentPageIndex)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                               ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = True
        If SortDirection = Sort.SortDirection.ASC Then
            IsAsc = True
        ElseIf SortDirection = Sort.SortDirection.DESC Then
            IsAsc = False
        End If

        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        pCompletelist.Sort(objListComparer)

    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim arlClassAllocation As ArrayList = CType(Session.Item("arlClassAlloc"), ArrayList)
        SortListControl(arlClassAllocation, CType(ViewState("CurrentSortColumn"), String), _
                 CType(ViewState("CurrentSortDirect"), String))
        objSessionHelper.SetSession("arlClassAlloc", arlClassAllocation)
        If Not arlClassAllocation Is Nothing And arlClassAllocation.Count > 0 Then
            lblNote.Text = "Silakan pilih kelas yang bisa diikuti:"
            lblNote.ForeColor = Color.Black
            btnDaftar.Enabled = True

            dtgClassAllocation.SelectedIndex = -1
            Dim arlClassAllocations As ArrayList = GetCurrentColl(arlClassAllocation, indexPage)
            objSessionHelper.SetSession("arlClassToRegister", arlClassAllocations)
            dtgClassAllocation.DataSource = arlClassAllocations 'GetCurrentColl(arlClassAllocation, indexPage)
            dtgClassAllocation.DataBind()
        Else
            lblNote.Text = "Maaf, tidak tersedia kelas untuk siswa atau dealer yg bersangkutan"
            lblNote.ForeColor = Color.Red
            btnDaftar.Enabled = False
        End If
    End Sub

    Private Function GenerateStatusInSet() As String
        Dim inSet As String = "(" & _
        "'" & CType(EnumTrClassRegistration.DataStatusType.Register, Short).ToString() & "'," & _
        "'" & CType(EnumTrClassRegistration.DataStatusType.Pass, Short).ToString() & "'" & _
        ")"
        Return inSet
    End Function

    Private Function CriteriaForHowManyAlreadyReg(ByVal ClassID As Integer, ByVal DealerID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, ClassID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Dealer.ID", MatchType.Exact, DealerID))
        'retreive only data with status daftar, if user already set status and lulus, for gagal not counted
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.InSet, GenerateStatusInSet()))
        Return criterias
    End Function

    Private Function AggreateForCheckRecord() As Aggregate
        Dim aggregates As New Aggregate(GetType(TrClassRegistration), "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function IsClassAllocationNotFull(ByVal objAllocation As TrClassAllocation) As Boolean
        Dim iTotalRegTrainee As Integer = New HelperFacade(User, GetType(TrClassRegistration)).RecordCount( _
        CriteriaForHowManyAlreadyReg(objAllocation.TrClass.ID, objAllocation.Dealer.ID), _
        AggreateForCheckRecord())

        If iTotalRegTrainee < objAllocation.Allocated Then
            Return True
        End If
        Return False
    End Function

    Private Function IsWorkMoreThanAYear(ByVal objTrainee As TrTrainee, ByVal objClass As TrClass) As Boolean
        Dim deviation As Long = DateDiff(DateInterval.Day, objTrainee.StartWorkingDate.Date, objClass.StartDate)
        If deviation >= 365 Then
            Return True
        End If
        Return False
    End Function

#Region "Data Processing Method"

#End Region

#Region "UI Processing Method"

    Private Sub SortListControl(ByRef ListControl As ArrayList, ByVal SortColumn As String, _
    ByVal SortDirection As String)
        Dim IsAsc As Boolean = True
        If SortDirection = "ASC" Then
            IsAsc = True
        ElseIf SortDirection = "DESC" Then
            IsAsc = False
        End If
        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        ListControl.Sort(objListComparer)
    End Sub

    Private Function GetCurrentColl(ByVal ClassColl As ArrayList, ByVal CurrentPage As Integer) As ArrayList
        SortListControl(ClassColl, CType(ViewState("CurrentSortColumn"), String), _
                 CType(ViewState("CurrentSortDirect"), String))

        dtgClassAllocation.VirtualItemCount = ClassColl.Count
        Dim arlCurrentColl As ArrayList = New ArrayList
        Dim nStart As Integer = (dtgClassAllocation.PageSize * CurrentPage)
        Dim i As Integer
        'For i = 0 To (dtgClassAllocation.VirtualItemCount - (dtgClassAllocation.PageSize * CurrentPage) - 1)
        '    arlCurrentColl.Add(ClassColl(nStart + i))
        'Next

        For i = nStart To nStart + (dtgClassAllocation.PageSize - 1)
            If ClassColl.Count >= i + 1 Then
                arlCurrentColl.Add(ClassColl(i))
            End If
        Next

        Return arlCurrentColl
    End Function

    Private sRbText As String = ""
    Public Sub OnlyOneChecked(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim rb As RadioButton = CType(sender, RadioButton)
        sRbText = rb.ClientID
        For Each i As DataGridItem In dtgClassAllocation.Items
            rb = CType(i.FindControl("rbSelect"), RadioButton)
            rb.Checked = False
            If (sRbText = rb.ClientID) Then
                rb.Checked = True
            End If
        Next
    End Sub
#End Region

#End Region

#Region "Event Handler"
    Private Sub dtgClassAllocation_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClassAllocation.PageIndexChanged
        dtgClassAllocation.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgClassAllocation.CurrentPageIndex)
    End Sub


    Private Sub dtgClassAllocation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClassAllocation.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), String)
                Case "ASC"
                    ViewState("CurrentSortDirect") = "DESC" 'Sort.SortDirection.DESC
                Case "DESC"
                    ViewState("CurrentSortDirect") = "ASC" 'Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = "ASC" 'Sort.SortDirection.ASC
        End If

        dtgClassAllocation.CurrentPageIndex = 0
        BindDataGrid(dtgClassAllocation.CurrentPageIndex)
    End Sub

    Private Sub dtgClassAllocation_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassAllocation.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgClassAllocation.CurrentPageIndex * dtgClassAllocation.PageSize)
            Dim arlAllocation As ArrayList = CType(Session.Item("arlClassToRegister"), ArrayList)
            If arlAllocation.Count > 0 Then
                Dim objAllocation As TrClassAllocation = CType(arlAllocation(e.Item.ItemIndex), TrClassAllocation)
                CType(e.Item.FindControl("lblAllocationRemaining"), Label).Text = _
                    (objAllocation.Allocated - AllocationRemaining(objAllocation)).ToString()
            End If
        End If
    End Sub

    Private Function AllocationRemaining(ByVal objAllocation As TrClassAllocation) As Integer
        Return New HelperFacade(User, GetType(TrClassRegistration)).RecordCount( _
        CriteriaForHowManyAlreadyReg(objAllocation.TrClass.ID, objAllocation.Dealer.ID), _
        AggreateForCheckRecord())
    End Function


    Private Sub btnDaftar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDaftar.Click
        Dim arlClassAlloc As ArrayList = CType(Session.Item("arlClassAlloc"), ArrayList)
        Dim objClassAlloc As TrClassAllocation

        For Each dgItem As DataGridItem In dtgClassAllocation.Items
            If CType(dgItem.FindControl("rbSelect"), RadioButton).Checked Then
                objClassAlloc = CType(arlClassAlloc( _
                    dgItem.ItemIndex + (dtgClassAllocation.CurrentPageIndex * dtgClassAllocation.PageSize)), TrClassAllocation)
                Exit For
            End If
        Next

        If Not objClassAlloc Is Nothing Then

            If objClassAlloc.TrClass.TrCourse.RequireWorkDate Then
                Dim objTrainee As TrTrainee = CType(Session.Item("veTrainee"), TrTrainee)
                If Not PassCondition1(objTrainee, objClassAlloc.TrClass) Then
                    MessageBox.Show("Tidak Memenuhi Syarat Masa Kerja Minimum")
                    Exit Sub
                End If
            End If

            If objClassAlloc.TrClass.StartDate >= Today Then
                If IsClassAllocationNotFull(objClassAlloc) Then
                    objSessionHelper.SetSession("objClassAlloc", objClassAlloc)
                    Response.Redirect("../Training/FrmTrClassRegistrationByTrainee2.aspx")
                Else
                    MessageBox.Show("Kapasitas Kelas Sudah Penuh")
                End If
            Else
                MessageBox.Show("Tidak bisa daftar karena kelas sudah mulai")
            End If

        Else
            MessageBox.Show("Pilih Kelas Terlebih Dahulu")
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../Training/FrmTrTrainee1.aspx")
    End Sub
#End Region

#Region "Cek Requirement For Class Registration"
    Private Function PassCondition1(ByVal objTrainee As TrTrainee, _
        ByVal objClass As TrClass) As Boolean
        Dim deviation As Long = DateDiff(DateInterval.Day, objTrainee.StartWorkingDate.Date, objClass.StartDate)
        If deviation >= 365 Then
            Return True
        End If
        Return False
    End Function
#End Region

End Class
