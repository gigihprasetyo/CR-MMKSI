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

Public Class FrmTrClassRegistration3
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgClassRegSuccess As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblClassName As System.Web.UI.WebControls.Label
    Protected WithEvents lblClassCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblStartDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblLocation As System.Web.UI.WebControls.Label
    Protected WithEvents lblFinishDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents lblRegSuccessNote As System.Web.UI.WebControls.Label
    Protected WithEvents lblRegFailNote As System.Web.UI.WebControls.Label
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents lblAllocatedTot As System.Web.UI.WebControls.Label
    Protected WithEvents dtgClassRegFail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnUbahAction As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dtgClassRegCancel As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblClassRegCancel As System.Web.UI.WebControls.Label
    Protected WithEvents txtISM2 As System.Web.UI.WebControls.TextBox

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
    Private Const SearchingDistanceMonth As Integer = 1
    Private QString As String
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            'txtISM2.Text = ""
            InitiatePage()
        End If
    End Sub

#Region "Private Method"
    Private Sub InitiatePage()
        Dim coll As NameValueCollection = Page.Request.QueryString
        If coll.Count > 0 Then
            'get querystring from page registration1
            Dim AllocationID As Integer = 0
            Dim TraineeIDColl As String
            GetQueryString(coll, AllocationID, TraineeIDColl)
            'get object trclassallocation
            Dim objAllocation As TrClassAllocation = GetTrainingData(AllocationID)
            'and fill it to header
            FillHeaderData(objAllocation)
            'process data: check schedule bentrok or not
            'and separate arraylist between success and fail data
            ProcessDetailData(objAllocation, TraineeIDColl)
            'display data
            FillDetailData()
        End If

    End Sub

    Private Sub GetQueryString(ByVal QueryStringColl As NameValueCollection, _
    ByRef AllocationID As Integer, ByRef TraineeIDColl As String)
        'querystring collection consist of 0:year 1:month 2:category 3:allocid
        '0-2 > we use it for query again in registration1 if user press Kembali button 
        '3 > use it to get trclassallocation object to fill header data
        '4 > collection of trainee id
        Dim queryHistory As String = QueryStringColl.GetKey(0) & "=" & QueryStringColl(0) & _
            "&" & QueryStringColl.GetKey(1) & "=" & QueryStringColl(1) & _
            "&" & QueryStringColl.GetKey(2) & "=" & QueryStringColl(2) & _
            "&" & QueryStringColl.GetKey(3) & "=" & QueryStringColl(3)
        ViewState.Add("queryHistory", queryHistory)
        AllocationID = CInt(QueryStringColl(3))
        TraineeIDColl = QueryStringColl(4)
    End Sub

    Private Function GetTrainingData(ByVal AllocationID As Integer) As TrClassAllocation
        Dim objAllocation As TrClassAllocation = New TrClassAllocationFacade(User).Retrieve(AllocationID)
        If Not IsNothing(objAllocation) Then
            objSessionHelper.SetSession("objAllocation", objAllocation)
        End If
        Return objAllocation
    End Function

    Private Sub FillHeaderData(ByVal objAllocation As TrClassAllocation)
        lblClassCode.Text = objAllocation.TrClass.ClassCode
        lblClassName.Text = objAllocation.TrClass.ClassName
        lblStartDate.Text = objAllocation.TrClass.StartDate.ToString("dd/MM/yyyy")
        lblFinishDate.Text = objAllocation.TrClass.FinishDate.ToString("dd/MM/yyyy")
        lblLocation.Text = objAllocation.TrClass.Location
        lblAllocatedTot.Text = objAllocation.Allocated
    End Sub

    Private Sub ProcessDetailData(ByVal objAllocation As TrClassAllocation, _
        ByVal TraineeIDStringColl As String)

        Dim arlClassRegSuccess As New ArrayList
        Dim arlClassRegFail As New ArrayList
        Dim arlClassRegCancel As New ArrayList
        'ex: "11c;4u;5c" -> traineeid 11-checked, 4-unchecked, 5-checked
        Dim TraineeID() As String = TraineeIDStringColl.Split(";")
        For i As Integer = 0 To TraineeID.Length - 1
            'mencegah TraineeID(i).Length - 1 = -1 
            If TraineeID(i).Length > 0 Then
                Dim strOp As String = TraineeID(i).Substring(TraineeID(i).Length - 1, 1)
                Dim strTraineeID As String = TraineeID(i).Substring(0, TraineeID(i).Length - 1)

                If strOp = "c" Then
                    Dim arlClassAlreadyRegColl As ArrayList = GetClassAlreadyReg( _
                        CInt(strTraineeID), objAllocation.TrClass)
                    Dim objClassToReg As TrClass = objAllocation.TrClass
                    Dim objTrainee As TrTrainee = GetTrainee(CInt(strTraineeID))
                    Dim objTrClassRegistration As TrClassRegistration
                    If arlClassAlreadyRegColl.Count > 0 Then
                        'check bentrok, please notify that arlClassRegFail filling byref
                        'return value of CheckClassBentrok, 1 no one class clash, -1 there's class clash, 
                        '0 this class already registered before so let be it
                        Dim intResult As Integer = CheckClassBentrok(arlClassAlreadyRegColl, _
                        objAllocation.TrClass, arlClassRegFail)
                        If intResult = 1 Then
                            objTrClassRegistration = CreateNewClassReg(objTrainee, objClassToReg)
                            arlClassRegSuccess.Add(objTrClassRegistration)
                        End If
                    Else
                        'just get it in
                        objTrClassRegistration = CreateNewClassReg(objTrainee, objClassToReg)
                        arlClassRegSuccess.Add(objTrClassRegistration)
                    End If
                ElseIf strOp = "u" Then
                    'check if class has been reg before and user unchecked so it means user want to cancel this reg
                    Dim objTrClassRegCancel As TrClassRegistration = _
                        CheckClassHasRegBefore(strTraineeID, objAllocation.TrClass)
                    If Not objTrClassRegCancel Is Nothing Then
                        arlClassRegCancel.Add(objTrClassRegCancel)
                    End If
                End If
            End If
        Next



        objSessionHelper.SetSession("arlClassRegSuccess", arlClassRegSuccess)
        objSessionHelper.SetSession("arlClassRegFail", arlClassRegFail)
        objSessionHelper.SetSession("arlClassRegCancel", arlClassRegCancel)
    End Sub

    Private Function GetTrainee(ByVal TraineeID As Integer) As TrTrainee
        Return CType(New TrTraineeFacade(User).Retrieve(TraineeID), TrTrainee)
    End Function

    Private Function CreateNewClassReg(ByVal TraineeToReg As TrTrainee, _
        ByVal ClassToReg As TrClass) As TrClassRegistration

        Dim objTrClassRegistration As New TrClassRegistration
        objTrClassRegistration.TrTrainee = TraineeToReg
        objTrClassRegistration.TrClass = ClassToReg
        objTrClassRegistration.RegistrationDate = DateTime.Today
        objTrClassRegistration.Status = CType(EnumTrClassRegistration.DataStatusType.Register, Short)
        'add by anh 20120207 
        objTrClassRegistration.Dealer = TraineeToReg.Dealer
        'end
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

    Private Function ClassAlreadyRegCriteria(ByVal TraineeID As Integer, _
        ByVal LowerSearchDate As DateTime, ByVal UpperSearchDate As DateTime) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, TraineeID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.StartDate", MatchType.GreaterOrEqual, LowerSearchDate))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.StartDate", MatchType.LesserOrEqual, UpperSearchDate))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Register, Short)))
        Return criterias
    End Function

    Private Function CheckClassHasRegBeforeCriteria(ByVal TraineeID As Integer, _
        ByVal ClassID As Integer) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, TraineeID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, ClassID))
        Return criterias
    End Function

    'Private Function MaxDay(ByVal Month As Short, ByVal year As Integer) As Short
    '    Select Case Month()
    '        Case 1, 3, 5, 7, 8, 10, 12
    '            Return 31
    '        Case 4, 6, 9, 11
    '            Return 30
    '        Case 2
    '            If year() Mod 4 = 0 Then
    '                Return 29
    '            Else
    '                Return 28
    '            End If
    '    End Select

    'End Function

    Private Function CheckClassHasRegBefore(ByVal TraineeID As Integer, _
        ByVal ClassToReg As TrClass) As TrClassRegistration
        Dim arlClassHasRegBefore As ArrayList = New TrClassRegistrationFacade(User).Retrieve( _
            CheckClassHasRegBeforeCriteria(TraineeID, ClassToReg.ID))
        If arlClassHasRegBefore.Count > 0 Then
            Return CType(arlClassHasRegBefore(0), TrClassRegistration)
        End If
        Return Nothing
    End Function


    Private Function GetClassAlreadyReg(ByVal TraineeID As Integer, _
        ByVal ClassToReg As TrClass) As ArrayList

        Dim dtStartDateClassToReg As DateTime = ClassToReg.StartDate
        Dim ts As New TimeSpan(60, 0, 0, 0)
        Dim dtLowerSearchDate As DateTime = dtStartDateClassToReg.Subtract(ts)
        Dim dtUpperSearchDate As DateTime = dtStartDateClassToReg.Add(ts)

        'Dim dtSearchingDate As New DateTime(dtStartDateClassToReg.Year, _
        '        dtStartDateClassToReg.Month - SearchingDistanceMonth, _
        'dtStartDateClassToReg.Day - 1)

        'Modifikasi untuk keperluan bug fixed  
        'Terjadi dtSearchingDate.month = 0 jika dtStartDateClassToReg.Month = 1 

        'Dim Year As Integer = dtStartDateClassToReg.Year
        'Dim Month As Short = dtStartDateClassToReg.Month
        'Dim Day As Short = dtStartDateClassToReg.Day
        'Dim YearDiff As Integer = dtStartDateClassToReg.Year - 0
        'Dim MonthDiff As Short = dtStartDateClassToReg.Month - SearchingDistanceMonth
        'Dim DayDiff As Short = dtStartDateClassToReg.Day - 1
        'Dim bFlagNullDiff As Boolean = False
        'Dim dtSearchingDate As DateTime

        'If DayDiff = 0 Then
        '    If MonthDiff = 0 Then
        '        DayDiff = 31
        '        MonthDiff = 12
        '        YearDiff = dtStartDateClassToReg.Year - 1
        '    Else
        '        DayDiff = MaxDay(dtStartDateClassToReg.Month - 1, dtStartDateClassToReg.Year)
        '        MonthDiff = dtStartDateClassToReg.Month - 1
        '    End If
        '    bFlagNullDiff = True
        'End If

        'If MonthDiff = 0 Then
        '    MonthDiff = 12
        '    YearDiff = dtStartDateClassToReg.Year - 1
        '    bFlagNullDiff = True
        'End If

        ''If DayDiff = 0 Then
        ''    DayDiff = 1
        ''    bFlagNullDiff = True
        ''End If

        ''If MonthDiff = 0 Then
        ''    MonthDiff = 1
        ''    bFlagNullDiff = True
        ''End If

        'If Not bFlagNullDiff Then
        '    dtSearchingDate = New DateTime(dtStartDateClassToReg.Year, _
        '        dtStartDateClassToReg.Month - SearchingDistanceMonth, _
        '        dtStartDateClassToReg.Day - 1)
        'Else
        '    dtSearchingDate = New DateTime(YearDiff, MonthDiff, DayDiff)
        'End If

        Return New TrClassRegistrationFacade(User).Retrieve( _
            ClassAlreadyRegCriteria(TraineeID, dtLowerSearchDate, dtUpperSearchDate))
    End Function

    Private Sub FillDetailData()
        Dim arlClassRegSuccess As ArrayList = CType(Session.Item("arlClassRegSuccess"), ArrayList)
        Dim arlClassRegFail As ArrayList = CType(Session.Item("arlClassRegFail"), ArrayList)
        Dim arlClassRegCancel As ArrayList = CType(Session.Item("arlClassRegCancel"), ArrayList)

        dtgClassRegSuccess.DataSource = arlClassRegSuccess
        dtgClassRegSuccess.DataBind()

        dtgClassRegFail.DataSource = arlClassRegFail
        dtgClassRegFail.DataBind()

        dtgClassRegCancel.DataSource = arlClassRegCancel
        dtgClassRegCancel.DataBind()

        If arlClassRegSuccess.Count > 0 Then
            SetVisibleDataGrid(dtgClassRegSuccess, lblRegSuccessNote, True)
        Else
            SetVisibleDataGrid(dtgClassRegSuccess, lblRegSuccessNote, False)
        End If

        If arlClassRegFail.Count > 0 Then
            SetVisibleDataGrid(dtgClassRegFail, lblRegFailNote, True)
        Else
            SetVisibleDataGrid(dtgClassRegFail, lblRegFailNote, False)
        End If

        If arlClassRegCancel.Count > 0 Then
            SetVisibleDataGrid(dtgClassRegCancel, lblClassRegCancel, True)
        Else
            SetVisibleDataGrid(dtgClassRegCancel, lblClassRegCancel, False)
        End If

        If arlClassRegSuccess.Count = 0 And arlClassRegCancel.Count = 0 And arlClassRegFail.Count = 0 Then
            lblErrorMessage.Text = "Tidak ada perubahan data"
            btnSubmit.Enabled = False
        Else
            If arlClassRegSuccess.Count = 0 And arlClassRegCancel.Count = 0 Then
                btnSubmit.Enabled = False
            Else
                btnSubmit.Enabled = True
            End If
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
    Private Sub dtgClassRegSuccess_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegSuccess.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1, String)
        End If

    End Sub

    Private Sub dtgClassRegFail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegFail.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1, String)
        End If
    End Sub

    Private Sub dtgClassRegCancel_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegCancel.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1, String)
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim arlClassRegSuccess As ArrayList = CType(Session.Item("arlClassRegSuccess"), ArrayList)
        Dim arlClassRegCancel As ArrayList = CType(Session.Item("arlClassRegCancel"), ArrayList)

        Dim objRegistrationFacade As New TrClassRegistrationFacade(User)
        If objRegistrationFacade.Insert(arlClassRegSuccess, arlClassRegCancel) > 0 Then
            Page.RegisterStartupScript("", "<script language=JavaScript> RedirectAfterSave(); </script>")
            'MessageBox.Show(SR.SaveSuccess)
            'btnSubmit.Enabled = False
            'Response.Redirect("../Training/FrmTrClassRegistration1.aspx")
            'belum masa naik anh 20120216
            'If lblClassCode.Text.StartsWith("M2") Then
            '    txtISM2.Text = "1"
            'Else
            '    txtISM2.Text = "0"
            'End If

        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Sub btnUbahAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUbahAction.Click
        'Modifikasi : tidak menggunakan javascript:history.go(-1) tetapi pakai Redirect
        'untuk mengatasi masalah checkbox yang selalu menjadi enable lagi ketika pakai history.go(-1)
        If Not IsNothing(Request.QueryString("year")) Then
            QString = CType(Request.QueryString("year"), String).Trim() & "-" & _
                        CType(Request.QueryString("month"), String).Trim() & "-" & _
                        CType(Request.QueryString("cat"), String).Trim() & "-" & _
                        CType(Request.QueryString("allocid"), String).Trim()
        End If

        Dim delimStr As String = "-"
        Dim delimeter As Char() = delimStr.ToCharArray
        Dim dummyParam As String() = QString.Split(delimeter)
        Dim strYear As String = dummyParam(0)
        Dim strMonth As String = dummyParam(1)
        Dim strCat As String = dummyParam(2)
        Dim allocid As String = dummyParam(3)
        Dim strUrl As String = "../Training/FrmTrClassRegistration2.aspx?" & _
                        "year=" & strYear & "&month=" & strMonth & "&cat=" & strCat & _
                        "&allocid=" & allocid
        Response.Redirect(strUrl)
    End Sub

    Private Sub btnBack_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.ServerClick
        btnUbahAction_Click(sender, e)
    End Sub
#End Region
End Class
