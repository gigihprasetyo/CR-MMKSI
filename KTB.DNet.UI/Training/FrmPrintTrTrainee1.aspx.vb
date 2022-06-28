Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports System.Text
Public Class FrmPrintTrTrainee1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgTrainee As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnCetak As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sHTrainee As SessionHelper = New SessionHelper

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            'InitiatePage()
            BindDataGrid()
        End If
    End Sub

    'Private Sub InitiatePage()
    '    Response.Write( _
    '                    New System.Text.StringBuilder("<script language='javascript'>"). _
    '                        Append("function downloadTrainee()"). _
    '                        Append("{	window.open('./FrmDownloadTrTrainee1.aspx','_blank','fullscreen=no,menubar=yes,status=yes,titlebar=yes,toolbar=no,height=480,width=640,resizable=yes');	}"). _
    '                        Append("</script>").ToString _
    '                    )
    'End Sub

    Private Sub BindDataGrid()
        Dim critCol As ICriteria = sHTrainee.GetSession("CritOfTrainee")
        Dim sortCol As ICollection = sHTrainee.GetSession("SortOfTrainee")
        Dim cat As String = sHTrainee.GetSession("classCategory")
        Dim cat2 As String = sHTrainee.GetSession("classCategory2")

        If IsNothing(critCol) Or IsNothing(sortCol) Then
            Response.Redirect("./FrmTrTrainee1.aspx")
        End If

        'Dim data As ArrayList = sHTrainee.GetSession("objTrTrainee")
        'Dim data As ArrayList
        Dim objTraineeList As ArrayList
        'If IsNothing(data) Then
        objTraineeList = New TrTraineeFacade(User).Retrieve(critCol, sortCol)
        If cat <> String.Empty Then
            For Each item As String In cat.Trim.Split(";")
                objTraineeList = FilterList(objTraineeList, item.Trim.ToUpper)
            Next
        End If
        If cat2 <> String.Empty Then
            For Each item As String In cat2.Trim.Split(";")
                objTraineeList = FilterListNoPass(objTraineeList, item.Trim.ToUpper)
            Next
        End If
        'objTraineeList = data
        sHTrainee.SetSession("objTrTrainee", objTraineeList)
        'Else
        'objTraineeList = Data
        'End If

        dtgTrainee.DataSource = objTraineeList
        dtgTrainee.DataBind()

    End Sub

    Private Function FilterListNoPass(ByVal orgList As ArrayList, ByVal filter As String) As ArrayList
        Dim newList As ArrayList = New ArrayList
        Dim mStepGeneralList As ArrayList = New ArrayList
        mStepGeneralList = GetCourseMstepGeneral()
        For Each item As TrTrainee In orgList
            Dim passCourse As ArrayList = New ArrayList
            For Each classReg As TrClassRegistration In item.TrClassRegistrations
                If classReg.Status = CStr(EnumTrClassRegistration.DataStatusType.Pass) Then
                    passCourse.Add(GetCourse(classReg))
                End If
            Next

            Dim isMstepGeneralNoPass As Boolean = False
            Dim mStepGeneralNoPassList As ArrayList = GetNotPassCourseByCategory(passCourse, mStepGeneralList)
            For Each strCat As String In mStepGeneralNoPassList
                If strCat = filter Then
                    isMstepGeneralNoPass = True
                    Exit For
                End If
            Next
            If isMstepGeneralNoPass Then
                newList.Add(item)
            End If
        Next
        Return newList
    End Function

    Private Function GetCourseMstepGeneral() As ArrayList
        Dim trCFacade As TrCourseFacade = New TrCourseFacade(User)
        Dim objCritCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objCritCol.opAnd(New Criteria(GetType(TrCourse), "Category", MatchType.InSet, "(1,2)"))

        Dim list As ArrayList = trCFacade.Retrieve(objCritCol)
        Return list
    End Function

    Private Function FilterList(ByVal orgList As ArrayList, ByVal filter As String) As ArrayList
        Dim newList As ArrayList = New ArrayList
        For Each item As TrTrainee In orgList
            For Each classReg As TrClassRegistration In item.TrClassRegistrations
                If classReg.Status = CStr(EnumTrClassRegistration.DataStatusType.Pass) Then
                    If filter = GetCourse(classReg) Then
                        newList.Add(item)
                        Exit For
                    End If
                End If
            Next
        Next
        Return newList
    End Function




    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("./FrmTrTrainee1.aspx")
    End Sub

    Private Function GetPassDateByCategory(ByVal passCourses As ArrayList, ByVal Category As String) As String
        Dim jrCourses As String() = KTB.DNet.Lib.WebConfig.GetValue(Category).Split(",")
        Dim passDate As DateTime = New DateTime(1900, 1, 1, 0, 0, 0)
        Dim isPassList As ArrayList = New ArrayList
        Dim isPass As Boolean = False
        For Each item As String In jrCourses
            Dim tempPass As Boolean = False
            For Each classReg As TrClassRegistration In passCourses
                If item = classReg.TrClass.TrCourse.CourseCode Then
                    If passDate < classReg.TrClass.FinishDate Then
                        passDate = classReg.TrClass.FinishDate
                        tempPass = True
                        Exit For
                    End If
                End If
            Next
            'isPassList.Add(tempPass)
            If tempPass Then
                isPass = True
            End If
        Next

        'For Each isPassed As Boolean In isPassList
        '    isPass = True
        '    If isPassed = False Then
        '        isPass = False
        '        Exit For
        '    End If
        'Next

        If isPass Then
            Return passDate.Day & "/" & passDate.Month & "/" & passDate.Year
        Else
            Return String.Empty
        End If
    End Function

    Private Function GetPassDateByCategoryX(ByVal passCourses As ArrayList, ByVal Category As String) As String
        Dim jrCourses As String() = KTB.DNet.Lib.WebConfig.GetValue(Category).Split(",")
        Dim passDate As DateTime = New DateTime(1900, 1, 1, 0, 0, 0)
        Dim isPassList As ArrayList = New ArrayList
        Dim isPass As Boolean = False
        For Each item As String In jrCourses
            Dim tempPass As Boolean = False
            For Each classReg As TrClassRegistration In passCourses
                If item = classReg.TrClass.TrCourse.CourseCode Then
                    If passDate < classReg.TrClass.FinishDate Then
                        passDate = classReg.TrClass.FinishDate
                    End If
                    tempPass = True
                    Exit For
                End If
            Next
            isPassList.Add(tempPass)
        Next

        For Each isPassed As Boolean In isPassList
            isPass = True
            If isPassed = False Then
                isPass = False
                Exit For
            End If
        Next

        If isPass Then
            Return passDate.Day & "/" & passDate.Month & "/" & passDate.Year
        Else
            Return String.Empty
        End If
    End Function

    Private Sub dtgTrainee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTrainee.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgTrainee.CurrentPageIndex * dtgTrainee.PageSize)
            Dim rowValue As TrTrainee = CType(e.Item.DataItem, TrTrainee)

            Dim lblCourses As Label = CType(e.Item.FindControl("lblCourses"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblMSTEP As Label = CType(e.Item.FindControl("lblMSTEP"), Label)
            Dim lblGeneral As Label = CType(e.Item.FindControl("lblGeneral"), Label)
            Dim lblJr As Label = CType(e.Item.FindControl("lblJr"), Label)
            Dim lblSr As Label = CType(e.Item.FindControl("lblSr"), Label)
            Dim lblMr As Label = CType(e.Item.FindControl("lblMr"), Label)
            Dim lblSrE As Label = CType(e.Item.FindControl("lblSrE"), Label)
            Dim lblSrD As Label = CType(e.Item.FindControl("lblSrD"), Label)

            Dim passClassCourse As ArrayList = New ArrayList
            If Not IsNothing(rowValue.TrClassRegistrations) Then
                Dim courses As System.Text.StringBuilder = New System.Text.StringBuilder
                Dim sList As ArrayList = New ArrayList
                Dim passCourse As ArrayList = New ArrayList
                For Each classReg As TrClassRegistration In rowValue.TrClassRegistrations
                    If classReg.Status = CStr(EnumTrClassRegistration.DataStatusType.Pass) Then
                        sList.Add(GetCourse(classReg))
                        passCourse.Add(GetCourse(classReg))
                        passClassCourse.Add(classReg)
                    End If
                Next
                sList.Sort()

                For Each strClassReg As String In sList
                    courses.Append(strClassReg)

                    If Not (strClassReg Is sList(sList.Count - 1)) Then
                        courses.Append(",")
                    End If
                Next

                lblCourses.Text = courses.ToString
                lblDealerCode.Text = rowValue.Dealer.DealerCode

                Dim mStepList As ArrayList = GetNotPassCourseByCategory(passCourse, GetCourseByCategory(EnumTrClass.EnumTrClassCategory.MSTEP))
                Dim mCategory As StringBuilder = New StringBuilder
                For Each strCat As String In mStepList
                    mCategory.Append(strCat)
                    If Not (strCat Is mStepList(mStepList.Count - 1)) Then
                        mCategory.Append(", ")
                    End If
                Next
                lblMSTEP.Text = mCategory.ToString

                lblJr.Text = GetPassDateByCategory(passClassCourse, "JUNIOR")
                lblSr.Text = GetPassDateByCategory(passClassCourse, "SENIOR")
                lblMr.Text = GetPassDateByCategory(passClassCourse, "MASTER")
                lblSrE.Text = GetPassDateByCategory(passClassCourse, "SPECIALIS_EGFS")
                lblSrD.Text = GetPassDateByCategory(passClassCourse, "SPECIALIS_DTEC")

                mStepList = GetNotPassCourseByCategory(passCourse, GetCourseByCategory(EnumTrClass.EnumTrClassCategory.GENERAL_WAJIB))
                mCategory = New StringBuilder
                For Each strCat As String In mStepList
                    mCategory.Append(strCat)
                    If Not (strCat Is mStepList(mStepList.Count - 1)) Then
                        mCategory.Append(", ")
                    End If
                Next
                lblGeneral.Text = mCategory.ToString
            End If

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

            If Not IsNothing(rowValue.Status) Then
                Dim enumTr As EnumTrTrainee = New EnumTrTrainee
                lblStatus.Text = enumTr.StatusByIndex(Integer.Parse(rowValue.Status))
            End If


        End If
    End Sub

    Private Function GetCourseByCategory(ByVal cat As EnumTrClass.EnumTrClassCategory) As ArrayList
        Dim trCFacade As TrCourseFacade = New TrCourseFacade(User)
        Dim objCritCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objCritCol.opAnd(New Criteria(GetType(TrCourse), "Category", MatchType.Exact, CType(cat, Integer)))
        Dim list As ArrayList = trCFacade.Retrieve(objCritCol)
        Return list
    End Function

    Private Function GetNotPassCourseByCategory(ByVal listPassCourse As ArrayList, ByVal listCourseByCategory As ArrayList) As ArrayList
        Dim result As ArrayList = New ArrayList
        Dim found As Boolean
        For Each item As TrCourse In listCourseByCategory
            found = False
            For Each courseCode As String In listPassCourse
                If item.CourseCode = courseCode Then
                    found = True
                    Exit For
                End If
            Next
            If Not found Then
                result.Add(item.CourseCode)
            End If
        Next
        Return result
    End Function

    Private Function GetCourse(ByVal nTrClassRegistration As TrClassRegistration) As String
        Return nTrClassRegistration.TrClass.TrCourse.CourseCode
    End Function


    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Response.Redirect("./FrmDownloadTrTrainee1.aspx")
    End Sub


End Class
