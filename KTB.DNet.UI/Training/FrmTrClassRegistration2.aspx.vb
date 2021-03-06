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

Public Class FrmTrClassRegistration2
    Inherits System.Web.UI.Page
    Private _sessHelper As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgTrainee As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblClassCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblClassName As System.Web.UI.WebControls.Label
    Protected WithEvents lblStartDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblLocation As System.Web.UI.WebControls.Label
    Protected WithEvents lblFinishDate As System.Web.UI.WebControls.Label
    Protected WithEvents btnDaftar As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblAllocatedReg As System.Web.UI.WebControls.Label
    Protected WithEvents lblAllocatedTot As System.Web.UI.WebControls.Label
    Protected WithEvents txtItemCheckColl As System.Web.UI.HtmlControls.HtmlInputHidden

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
    Private m_intAllocatedReg As Integer
    Private QString As String
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
            BindDataGrid()
        End If
    End Sub

#Region "Private Method"

    Private Function GetBasicCourseCode() As String
        Return CType(New ReferenceFacade(User).RetrieveActiveList(TYPE_REF_CODE, BCC_REF_CODE), Reference).Description
    End Function

    Private Sub InitiatePage()
        Dim coll As NameValueCollection = Page.Request.QueryString
        If coll.Count > 0 Then
            'get querystring from page registration1
            Dim AllocationID As Integer = 0
            GetQueryString(coll, AllocationID)
            'get object trclassallocation
            Dim objAllocation As TrClassAllocation = GetTrainingData(AllocationID)
            'and fill it to header
            FillHeaderData(objAllocation)
        Else
            Response.Redirect("../Training/FrmTrClassRegistration1.aspx")
        End If

        'ViewState("CurrentSortColumn") = "ID"
        'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub FillHeaderData(ByVal objAllocation As TrClassAllocation)
        lblClassCode.Text = objAllocation.TrClass.ClassCode
        lblClassName.Text = objAllocation.TrClass.ClassName
        lblStartDate.Text = objAllocation.TrClass.StartDate.ToShortDateString
        lblFinishDate.Text = objAllocation.TrClass.FinishDate.ToShortDateString
        lblLocation.Text = objAllocation.TrClass.Location
        lblAllocatedTot.Text = objAllocation.Allocated
    End Sub

    Private Sub GetQueryString(ByVal QueryStringColl As NameValueCollection, _
        ByRef AllocationID As Integer)
        'querystring collection consist of 0:year 1:month 2:cat 3:allocid
        '0-2 > we use it for query again in registration1 if user press Kembali button 
        '3 > use it to get trclassallocation object to fill header data

        Dim queryHistory As String = QueryStringColl.GetKey(0) & "=" & QueryStringColl(0) & _
            "&" & QueryStringColl.GetKey(1) & "=" & QueryStringColl(1) & _
            "&" & QueryStringColl.GetKey(2) & "=" & QueryStringColl(2)
        ViewState.Add("queryHistory", queryHistory)
        ViewState.Add("allocid", QueryStringColl(3))
        AllocationID = CInt(QueryStringColl(3))
    End Sub

    Private Function GetTrainingData(ByVal AllocationID As Integer) As TrClassAllocation
        Dim objAllocation As TrClassAllocation = New TrClassAllocationFacade(User).Retrieve(AllocationID)
        If Not IsNothing(objAllocation) Then
            objSessionHelper.SetSession("objAllocation", objAllocation)
        End If
        Return objAllocation
    End Function

    Private Function GetCriteriaTrainee() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", _
            MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        If Not IsNothing(objDealer) Then
            criterias.opAnd(New Criteria(GetType(TrTrainee), "Dealer", MatchType.Exact, objDealer.ID))
        End If
        criterias.opAnd(New Criteria(GetType(TrTrainee), "Status", MatchType.Exact, CType(EnumTrTrainee.TrTraineeStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'criterias.opAnd(New Criteria(GetType(TrTrainee), "Dealer", MatchType.Exact, 43))

        Return criterias
    End Function

    Private Function FilterData3(ByVal arlTrainee As ArrayList, _
        ByVal objAllocation As TrClassAllocation) As ArrayList
        Dim arlTemp As ArrayList = New ArrayList

        For Each objTrainee As TrTrainee In arlTrainee

        Next

        Return arlTemp
    End Function

    Private Sub BindDataGrid()
        'get all trainee in dealer
        Dim arlTrainee As ArrayList = New TrTraineeFacade(User).Retrieve(GetCriteriaTrainee())
        Dim objAllocation As TrClassAllocation = CType(Session.Item("objAllocation"), TrClassAllocation)

        If arlTrainee.Count > 0 Then
            'filter all trainee before displayed
            'user already get this class or has been "lulus" for this course
            Dim arlTraineeFiltered1 As ArrayList = FilterData1(arlTrainee, objAllocation)

            'by condition (see detail in function for more explaination)
            Dim arlTraineeFiltered2 As ArrayList = FilterData2(arlTraineeFiltered1, objAllocation)

            'modifikasi : tulis dulu jml pendaftarnya untuk keperluan pencegahan jumlah 
            'pendaftar yang melebihi alokasi
            lblAllocatedReg.Text = UpdateStatus(arlTraineeFiltered2).ToString

            objSessionHelper.SetSession("arlTrainee", arlTraineeFiltered2)
            dtgTrainee.DataSource = arlTraineeFiltered2
            dtgTrainee.DataBind()

            If arlTraineeFiltered2.Count > 0 Then
                btnDaftar.Enabled = True
            Else
                btnDaftar.Enabled = False
            End If
        End If

        'if total trainee in class reached max capacity, disabled button "daftar"
        'If lblAllocatedReg.Text.Trim() = lblAllocatedTot.Text.Trim() Then
        '    btnDaftar.Enabled = False
        'Else
        '    If CInt(lblAllocatedReg.Text.Trim()) = dtgTrainee.Items.Count Then
        '        btnDaftar.Enabled = False
        '    Else
        '        btnDaftar.Enabled = True
        '    End If
        'End If

    End Sub

    Private Function CreateCriteriaForCheckRegStatus(ByVal TraineeID As Integer, _
        ByVal ClassID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, TraineeID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, ClassID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Register, Short)))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord() As Aggregate
        Dim aggregates As New Aggregate(GetType(TrClassRegistration), "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function CheckRegStatus(ByVal TraineeID As Integer) As Boolean
        Dim objAllocation As TrClassAllocation = CType(Session.Item("objAllocation"), TrClassAllocation)

        Return New HelperFacade(User, GetType(TrClassRegistration)).IsRecordExist( _
        CreateCriteriaForCheckRegStatus(TraineeID, objAllocation.TrClass.ID), _
        CreateAggreateForCheckRecord())
    End Function

    Private Function CreateCriteriaForCheckPreRequisite(ByVal TraineeID As Integer, _
        ByVal CourseID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, TraineeID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.TrCourse.ID", MatchType.Exact, CourseID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Pass, Short)))

        Return criterias
    End Function

    Private Function CheckPreRequisite(ByVal TraineeID As Integer, _
        ByVal CourseID As Integer) As Boolean
        Dim objAllocation As TrClassAllocation = CType(Session.Item("objAllocation"), TrClassAllocation)

        Return New HelperFacade(User, GetType(TrClassRegistration)).IsRecordExist( _
        CreateCriteriaForCheckPreRequisite(TraineeID, CourseID), _
        CreateAggreateForCheckRecord())
    End Function

    Private Function CheckPassPreRequisiteClass(ByVal TraineeID As Integer, _
        ByVal CoursePreRequireColl As ArrayList) As Boolean
        Dim counterPassClass As Integer = 0
        For r As Integer = 0 To CoursePreRequireColl.Count - 1
            Dim objTrPreRequire As TrPreRequire = CType(CoursePreRequireColl(r), TrPreRequire)

            If CheckPreRequisite(TraineeID, objTrPreRequire.PreRequireCourseID) Then
                counterPassClass = counterPassClass + 1
            End If
        Next
        If counterPassClass = CoursePreRequireColl.Count Then
            Return True
        End If
        Return False
    End Function

    Private Function CheckNoPassPreRequisiteClass(ByVal TraineeID As Integer, _
        ByVal CoursePreRequireColl As ArrayList) As Boolean
        Dim counterPassClass As Integer = 0
        For r As Integer = 0 To CoursePreRequireColl.Count - 1
            Dim objTrPreRequire As TrPreRequire = CType(CoursePreRequireColl(r), TrPreRequire)

            If CheckPreRequisite(TraineeID, objTrPreRequire.PreRequireCourseID) Then
                counterPassClass = counterPassClass + 1
            End If
        Next
        If counterPassClass > 0 Then
            Return False
        End If
        Return True
    End Function

    Private Function UpdateStatus(ByRef arlTrainee As ArrayList) As Integer
        Dim count As Integer = 0
        If arlTrainee.Count > 0 Then
            For t As Integer = 0 To arlTrainee.Count - 1
                Dim objTrainee As TrTrainee = arlTrainee(t)
                If CheckRegStatus(objTrainee.ID) Then
                    objTrainee.IsTraineeRegistered = True
                    count += 1
                Else
                    objTrainee.IsTraineeRegistered = False
                End If
            Next
        End If
        Return count
    End Function

    Private Function IsAllowedTrainee(ByRef strTraineeList As String) As Boolean
        Dim IsAllowed As Boolean = True
        Dim IsFirst As Boolean = True

        For Each dtgItem As DataGridItem In dtgTrainee.Items
            Dim chk As CheckBox = CType(dtgItem.FindControl("chkItemChecked"), CheckBox)
            Dim txtID As TextBox = CType(dtgItem.FindControl("txtID"), TextBox)
            Dim objIHMFac As TrInhouseMemberFacade = New TrInhouseMemberFacade(User)
            Dim arlIHM As ArrayList = New ArrayList
            Dim crtIHM As CriteriaComposite

            If chk.Checked Then
                crtIHM = New CriteriaComposite(New Criteria(GetType(TrInhouseMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crtIHM.opAnd(New Criteria(GetType(TrInhouseMember), "TraineeID", MatchType.Exact, CType(txtID.Text, Integer)))
                'crtIHM.opAnd(New Criteria(GetType(TrInhouseMember), "Result", MatchType.Lesser, "(" & CType(ConfigurationSettings.AppSettings.Item("Training.MinResultToRegister"), Decimal) & ")"))
                arlIHM = objIHMFac.Retrieve(crtIHM)
                For Each objIHM As TrInhouseMember In arlIHM
                    If objIHM.Result < CType(ConfigurationSettings.AppSettings.Item("Training.MinResultToRegister"), Decimal) Then
                        'strTraineeList = strTraineeList & IIf(strTraineeList.Trim = "", "", ", ") & objIHM.TrTrainee.Name
                        strTraineeList = strTraineeList & IIf(IsFirst, "", ", ") & objIHM.TrTrainee.Name
                        IsFirst = False
                        IsAllowed = False
                        GoTo NextTrainee
                    End If
                Next
            End If
NextTrainee:
        Next
        Return IsAllowed
    End Function

#End Region

#Region "Event Handler"

    Private Sub dtgTrainee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTrainee.ItemDataBound
        Dim arlTrainee As ArrayList = CType(Session.Item("arlTrainee"), ArrayList)
        'If e.Item.ItemType = ListItemType.Header Then
        '    m_intAllocatedReg = 0
        'End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objTrainee As TrTrainee = arlTrainee(e.Item.ItemIndex)
            Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
            chkItemChecked.Attributes.Add("onclick", "CheckEnability();")
            If (objTrainee.IsTraineeRegistered) Then
                e.Item.BackColor = Color.LightSalmon
                chkItemChecked.Checked = True
                'chkItemChecked.Enabled = False
                'm_intAllocatedReg = m_intAllocatedReg + 1
            Else
                e.Item.BackColor = Color.White
                chkItemChecked.Checked = False
                'chkItemChecked.Enabled = True
            End If

            'Ditambahkan baris ini untuk mencegah jumlah pendaftar yang melebihi alokasi
            'If CType(lblAllocatedReg.Text, Integer) >= CType(lblAllocatedTot.Text, Integer) Then
            '    chkItemChecked.Enabled = False
            'End If
            CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1, String)
        End If

        'lblAllocatedReg.Text = m_intAllocatedReg
    End Sub

    'Private Function GenerateStatusInSet() As String
    '    Dim inSet As String = "(" & _
    '    "'" & CType(EnumTrClassRegistration.DataStatusType.Register, Short).ToString() & "'," & _
    '    "'" & CType(EnumTrClassRegistration.DataStatusType.Pass, Short).ToString() & "'" & _
    '    ")"
    '    Return inSet
    'End Function

    'Private Function CreateCriteriaForCheckCourseExist(ByVal TraineeID As Integer, _
    '    ByVal CourseID As Integer) As CriteriaComposite
    '    Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", _
    '    MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, TraineeID))
    '    criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.TrCourse.ID", MatchType.Exact, CourseID))
    '    criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.InSet, GenerateStatusInSet()))
    '    Return criterias
    'End Function

    'Private Function CheckAlreadyLulus(ByVal objTrainee As TrTrainee, _
    '    ByVal objCourse As TrCourse)
    '    Return New HelperFacade(User, GetType(TrClassRegistration)).IsRecordExist( _
    '    CreateCriteriaForCheckCourseExist(objTrainee.ID, objCourse.ID), _
    '    CreateAggreateForCheckRecord())
    'End Function

    Private Function CreateCriteriaForCheckCourseExist(ByVal TraineeID As Integer, _
        ByVal CourseID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, TraineeID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.TrCourse.ID", MatchType.Exact, CourseID))
        'criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.InSet, GenerateStatusInSet()))
        Return criterias
    End Function

    Private Function CheckCourseExist(ByVal objTrainee As TrTrainee, _
        ByVal objClass As TrClass) As Boolean

        Dim arlRegistration As ArrayList = New TrClassRegistrationFacade(User).Retrieve( _
            CreateCriteriaForCheckCourseExist(objTrainee.ID, objClass.TrCourse.ID))
        Dim bCourseExist As Boolean = False

        'if trainee has been registered before, rule of checking with status:
        '1.pass = return true (course exist)
        '2.fail = return false (course not exist, so trainee can be register again)
        '3.register = if the same class id return false (course not exist, so user can see this trainee)
        '             else return true (course exist, maybe trainee has been registered in other class)
        If arlRegistration.Count > 0 Then
            For Each objRegistration As TrClassRegistration In arlRegistration
                If objRegistration.Status = EnumTrClassRegistration.DataStatusType.Pass Then
                    bCourseExist = True
                ElseIf objRegistration.Status = EnumTrClassRegistration.DataStatusType.Register Then
                    If objRegistration.TrClass.ID <> objClass.ID Then
                        bCourseExist = True
                    End If
                    'note bugs no1795
                ElseIf objRegistration.Status = EnumTrClassRegistration.DataStatusType.Fail Then
                    If objRegistration.TrClass.ID = objClass.ID Then
                        bCourseExist = True
                    End If
                End If
            Next
        End If
        Return bCourseExist
    End Function

    Private Function FilterData1(ByVal arlTrainee As ArrayList, _
        ByVal objAllocation As TrClassAllocation) As ArrayList
        Dim arlTemp As New ArrayList
        For x As Integer = 0 To arlTrainee.Count - 1
            Dim objTrainee As TrTrainee = CType(arlTrainee(x), TrTrainee)
            If Not CheckCourseExist(objTrainee, objAllocation.TrClass) Then
                arlTemp.Add(objTrainee)
            End If
        Next
        Return arlTemp
    End Function

    Private Function FilterData2(ByVal arlTrainee As ArrayList, _
        ByVal objAllocation As TrClassAllocation) As ArrayList
        Dim arlTemp As New ArrayList

        For t As Integer = 0 To arlTrainee.Count - 1
            Dim objTrainee As TrTrainee = CType(arlTrainee(t), TrTrainee)
            Dim FlagReqWorkDate As Boolean = False
            Dim FlagPreRequired1 As Boolean = False
            Dim FlagPreRequired2 As Boolean = False
            Dim FlagIsAktif As Boolean = False

            If objAllocation.TrClass.TrCourse.RequireWorkDate Then
                'condition1: userworkingdate must more than 1 year
                If PassCondition1(objTrainee, objAllocation.TrClass) Then
                    FlagReqWorkDate = True
                End If
            End If

            If objAllocation.TrClass.TrCourse.ID <> 5 Then 'M1 - Basic
                'condition2: user must pass prerequisites class
                If PassCondition2(objTrainee, objAllocation.TrClass.TrCourse) Then
                    FlagPreRequired1 = True
                End If

                'condition3: user must not pass prerequisites class
                If PassCondition3(objTrainee, objAllocation.TrClass.TrCourse) Then
                    FlagPreRequired2 = True
                End If

                If objAllocation.TrClass.TrCourse.RequireWorkDate Then
                    If FlagReqWorkDate And FlagPreRequired1 And FlagPreRequired2 Then
                        arlTemp.Add(objTrainee)
                    End If
                Else
                    If FlagPreRequired1 And FlagPreRequired2 Then
                        arlTemp.Add(objTrainee)
                    End If
                End If
            Else
                If objAllocation.TrClass.TrCourse.RequireWorkDate Then
                    If FlagReqWorkDate Then
                        arlTemp.Add(objTrainee)
                    End If
                Else
                    arlTemp.Add(objTrainee)
                End If
            End If



        Next
        Return arlTemp
    End Function

    Private Function PassCondition1(ByVal objTrainee As TrTrainee, _
        ByVal objClass As TrClass) As Boolean
        Dim deviation As Long = DateDiff(DateInterval.Day, objTrainee.StartWorkingDate.Date, objClass.StartDate)
        If deviation >= 365 Then
            Return True
        End If
        Return False
    End Function

    'Private Function MinimumStartWorkingDate(ByVal StartWorkingDate As DateTime) As DateTime
    '    Return New DateTime(StartWorkingDate.Year - 1, StartWorkingDate.Month, StartWorkingDate.Day)
    'End Function

    Private Function GetCriteriaPreRequisiteClass(ByVal CourseID As Integer, ByVal requireType As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrPreRequire), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrPreRequire), "TrCourse", MatchType.Exact, CourseID))
        criterias.opAnd(New Criteria(GetType(TrPreRequire), "RequireType", MatchType.Exact, requireType))
        Return criterias
    End Function

    Private Function PassCondition2(ByVal objTrainee As TrTrainee, _
        ByVal objCourse As TrCourse) As Boolean
        Dim arlPreRequisiteColl As ArrayList = New TrPreRequireFacade(User).Retrieve( _
            GetCriteriaPreRequisiteClass(objCourse.ID, EnumCourseRequireType.RequireType.SyaratLulus))

        If arlPreRequisiteColl.Count > 0 Then
            Return CheckPassPreRequisiteClass(objTrainee.ID, arlPreRequisiteColl)
        Else
            'If objCourse.RequireWorkDate Then
            '    If objCourse.CourseCode = GetBasicCourseCode() Then
            '        Return True
            '    Else
            '        Return True
            '    End If
            'Else
            '    Return True
            'End If
            Return True
        End If

        Return False
    End Function

    Private Function PassCondition3(ByVal objTrainee As TrTrainee, _
        ByVal objCourse As TrCourse) As Boolean
        Dim arlPreRequisiteColl As ArrayList = New TrPreRequireFacade(User).Retrieve( _
            GetCriteriaPreRequisiteClass(objCourse.ID, EnumCourseRequireType.RequireType.SyaratBelumLulus))

        If arlPreRequisiteColl.Count > 0 Then
            Return CheckNoPassPreRequisiteClass(objTrainee.ID, arlPreRequisiteColl)
        Else
            Return True
        End If
    End Function

    'Private Function PassCondition3(ByVal objTrainee As TrTrainee)
    '    If objTrainee.Status = CType(EnumTrTrainee.TrTraineeStatus.Active, String) And _
    '       objTrainee.RowStatus = CType(DBRowStatus.Active, Short) Then
    '        Return True
    '    End If
    '    Return False
    'End Function

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Dim querystring As String = CType(ViewState.Item("queryHistory"), String)
        Response.Redirect("../Training/FrmTrClassRegistration1.aspx?" & querystring)
    End Sub

    Private Function IsThereCheckedItem() As Boolean
        'count checked item except trainee already register before
        Dim itemChecked As Integer = 0

        For Each dgItem As DataGridItem In dtgTrainee.Items
            'add counter if item checked and status not terdaftar (belum terdaftar)
            If CType(dgItem.Cells(0).FindControl("chkItemChecked"), CheckBox).Checked And _
                CType(dgItem.Cells(0).FindControl("chkItemChecked"), CheckBox).Enabled = True Then
                itemChecked = itemChecked + 1
            End If
        Next

        If itemChecked > 0 Then
            Return True
        End If
        Return False
    End Function

    Private Function GenerateTraineeIDColl(ByVal ItemCheck As String) As String
        'remove ";" at the end of string
        Dim arrItemCheck As String() = (ItemCheck.Substring(0, ItemCheck.Length - 1)).Split(";")
        Dim arlTrainee As ArrayList = CType(Session.Item("arlTrainee"), ArrayList)
        Dim TraineeIDColl As String = ""

        For x As Integer = 0 To arlTrainee.Count - 1
            Dim objTrainee As TrTrainee = CType(arlTrainee(x), TrTrainee)
            'get only trainee not registered yet
            'If Not objTrainee.IsTraineeRegistered Then
            For y As Integer = 0 To arrItemCheck.Length - 1
                'check index must plus one because arrItemCheck contains 
                'index of datagrid which start with 1


                'karena dimodifikasi dengan disisipkan spasi kosong (" ")
                'maka jadi harus diperiksa dulu apakah arrItemCheck(y) berupa numerik
                If IsNumeric(arrItemCheck(y)) Then
                    'If x + 1 = arrItemCheck(y) Then
                    If x + 1 = CType(arrItemCheck(y), Integer) Then
                        TraineeIDColl = TraineeIDColl & objTrainee.ID.ToString() & ";"
                        Exit For
                    End If
                End If

            Next
            'End If
        Next

        If TraineeIDColl <> "" Then
            'remove ";" at the end of string
            TraineeIDColl = TraineeIDColl.Substring(0, TraineeIDColl.Length - 1)
        End If

        Return TraineeIDColl
    End Function

    Private Function GetTraineeIDColl(ByVal ItemCheck As String) As String
        'remove ";" at the end of string
        Dim arlTrainee As ArrayList = CType(Session.Item("arlTrainee"), ArrayList)
        Dim TraineeIDColl As String = ""
        Dim intFlag As Integer
        Dim nCheckCount As Integer = 0

        'mencegah ItemCheck.Length - 1 = -1 
        'Dim arrItemCheck As String() = (ItemCheck.Substring(0, ItemCheck.Length - 1)).Split(";")
        Dim arrItemCheck As String()

        If ItemCheck.Length > 0 Then
            arrItemCheck = (ItemCheck.Substring(0, ItemCheck.Length - 1)).Split(";")
            For x As Integer = 0 To arlTrainee.Count - 1
                Dim objTrainee As TrTrainee = CType(arlTrainee(x), TrTrainee)
                'get only trainee not registered yet
                'If Not objTrainee.IsTraineeRegistered Then
                intFlag = 1
                For y As Integer = 0 To arrItemCheck.Length - 1
                    'check index must plus one because arrItemCheck contains 
                    'index of datagrid which start with 1

                    'karena dimodifikasi dengan disisipkan spasi kosong (" ")
                    'maka jadi harus diperiksa dulu apakah arrItemCheck(y) berupa numerik
                    If IsNumeric(arrItemCheck(y)) Then
                        'If x + 1 = arrItemCheck(y) Then
                        If x + 1 = CType(arrItemCheck(y), Integer) Then
                            TraineeIDColl = TraineeIDColl & objTrainee.ID.ToString() & "c;"
                            intFlag = -1
                            Exit For
                        End If
                    End If
                Next
                If intFlag > 0 Then
                    TraineeIDColl = TraineeIDColl & objTrainee.ID.ToString() & "u;"
                Else
                    nCheckCount += 1

                End If
                'End If
            Next
        End If

        _sessHelper.SetSession("sess_nCheckCount", nCheckCount)

        If TraineeIDColl <> "" Then
            'remove ";" at the end of string
            TraineeIDColl = TraineeIDColl.Substring(0, TraineeIDColl.Length - 1)
        End If


        Return TraineeIDColl
    End Function

    Private Function IsAnyTraineeDuplicate() As Boolean

        Return False

        Dim DealerID As Integer
        Dim ClassID As Integer
        Dim TraineeID As Integer
        Dim TraineeName As String

        DealerID = CType(Session.Item("DEALER"), Dealer).ID
        ClassID = New TrClassFacade(User).Retrieve(lblClassCode.Text).ID
        For Each dgItem As DataGridItem In dtgTrainee.Items
            Dim chk As CheckBox = CType(dgItem.FindControl("chkItemChecked"), CheckBox)
            Dim objReg As TrClassRegistration

            If chk.Checked Then
                Dim txt As TextBox = CType(dgItem.FindControl("txtName"), TextBox)
                'TraineeID = CInt(txt.Text)
                TraineeName = txt.Text
                Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, 0))
                'crits.opAnd(New Criteria(GetType(TrClassRegistration), "TraineeID", MatchType.Exact, TraineeID))
                crits.opAnd(New Criteria(GetType(TrClassRegistration), "trClass.ID", MatchType.Exact, ClassID))
                crits.opAnd(New Criteria(GetType(TrClassRegistration), "Trainee.Name", MatchType.Exact, TraineeName))
                crits.opAnd(New Criteria(GetType(TrClassRegistration), "Trainee.Dealer.ID", MatchType.No, DealerID))
                Dim arlTCR As ArrayList = New TrClassRegistrationFacade(User).Retrieve(crits)
                If arlTCR.Count > 0 Then
                    Dim objTCR As New TrClassRegistration
                    objTCR = arlTCR.Item(0)

                    Dim txtName As TextBox = CType(dgItem.FindControl("txtName"), TextBox)
                    Dim objDealer As New Dealer
                    'objDealer = objTCR.TrTrainee.Dealer
                    objDealer = objTCR.Dealer
                    MessageBox.Show("Trainee " & TraineeName & " sudah terdaftar atas dealer " & objDealer.DealerCode & vbCrLf & "Silahkan membatalkan pendaftaran tersebut terlebih dahulu")
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Private Sub btnDaftar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDaftar.Click
        'If IsThereCheckedItem() Then
        '    Dim querystring As String = _
        '        CType(ViewState.Item("queryHistory"), String) & _
        '        "&allocid=" & CType(ViewState.Item("allocid"), String) & _
        '        "&tcoll=" & GenerateTraineeIDColl(txtItemCheckColl.Value.Trim())
        '    Response.Redirect("../Training/FrmTrClassRegistration3.aspx?" & querystring)
        'Else
        '    MessageBox.Show("Pilih Data Siswa Terlebih Dahulu")
        'End If

        'Check whether the trainee has score less than 60 in Training Inhouse (in table TrInhouseMember)
        'Start  :Never run this logic (not implemented yet
        Dim strTraineeList As String
        If 1 = 2 AndAlso Not IsAllowedTrainee(strTraineeList) Then
            MessageBox.Show("" & strTraineeList & " tidak bisa didaftarkan karena nilainya tidak memenuhi syarat(data di Training Inhouse)")
            Exit Sub
        End If
        'End    :Never run this logic (not implemented yet


        Dim querystring As String = _
            CType(ViewState.Item("queryHistory"), String) & _
            "&allocid=" & CType(ViewState.Item("allocid"), String) & _
            "&tcoll=" & GetTraineeIDColl(txtItemCheckColl.Value.Trim())

        If Not _sessHelper.GetSession("sess_nCheckCount") Is Nothing Then
            If CInt(lblAllocatedTot.Text) >= CType(_sessHelper.GetSession("sess_nCheckCount"), Integer) Then
                'If IsAnyTraineeDuplicate() Then
                '    Exit Sub
                'End If
                Response.Redirect("../Training/FrmTrClassRegistration3.aspx?" & querystring)
            Else
                MessageBox.Show("Jumlah yang di daftarkan melampaui alokasi")
            End If
        End If

    End Sub

#End Region

End Class
