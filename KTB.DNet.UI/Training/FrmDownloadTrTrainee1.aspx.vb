#Region "Import Statement"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.UI.Helper
Imports System.IO
Imports System.Text
#End Region

Public Class FrmDownloadTrTrainee1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgTrainee As System.Web.UI.WebControls.DataGrid

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
        SetDownload()
    End Sub

    Private Sub BindDataGrid()
        Dim data As ArrayList = sHTrainee.GetSession("objTrTrainee")
        If IsNothing(data) Then
            Response.Write("<script>alert('Tidak ada data');window.close();</script>")
            Return
        End If

        dtgTrainee.DataSource = data
        dtgTrainee.DataBind()

    End Sub

    Private Sub dtgTrainee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTrainee.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim rowValue As TrTrainee = CType(e.Item.DataItem, TrTrainee)
            Dim lblMSTEP As Label = CType(e.Item.FindControl("lblMSTEP"), Label)
            Dim lblGeneral As Label = CType(e.Item.FindControl("lblGeneral"), Label)
            Dim lblJr As Label = CType(e.Item.FindControl("lblJr"), Label)
            Dim lblSr As Label = CType(e.Item.FindControl("lblSr"), Label)
            Dim lblMr As Label = CType(e.Item.FindControl("lblMr"), Label)
            Dim passClassCourse As ArrayList = New ArrayList

            Dim lblCourses As Label = CType(e.Item.FindControl("lblCourses"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
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

    Private Function GetNotPassCourse(ByVal trainee As TrTrainee, ByVal cat As EnumTrClass.EnumTrClassCategory) As String
        If Not trainee Is Nothing Then
            Dim sList As ArrayList = New ArrayList
            Dim courses As System.Text.StringBuilder = New System.Text.StringBuilder
            Dim passCourse As ArrayList = New ArrayList
            For Each classReg As TrClassRegistration In trainee.TrClassRegistrations
                If classReg.Status = CStr(EnumTrClassRegistration.DataStatusType.Pass) Then
                    sList.Add(GetCourse(classReg))
                    passCourse.Add(GetCourse(classReg))
                End If
            Next
            sList.Sort()
            Dim mStepList As ArrayList = GetNotPassCourseByCategory(passCourse, GetCourseByCategory(CInt(cat)))
            Dim mCategory As StringBuilder = New StringBuilder
            For Each strCat As String In mStepList
                mCategory.Append(strCat)
                If Not (strCat Is mStepList(mStepList.Count - 1)) Then
                    mCategory.Append(", ")
                End If
            Next
            Return mCategory.ToString
        End If
        Return " "
    End Function

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

    Private Sub SetDownload()
        'Response.ContentType = "application/x-download"
        'Dim uploadFilename As String = New System.Text.StringBuilder("Data Status Siswa.xls").ToString()

        'Response.AddHeader("Content-Disposition", _
        '    New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)

        'BindDataGrid()

        'Dim stream As System.IO.MemoryStream = New System.IO.MemoryStream
        'Dim writer As System.IO.StreamWriter = New System.IO.StreamWriter(stream)
        'Dim htmlWriter As Html32TextWriter = New Html32TextWriter(writer)
        'dtgTrainee.RenderControl(htmlWriter)
        'writer.Flush()

        'Response.Write(writer)

        'writer.Close()
        'stream.Close()
        download()
    End Sub

    Private Sub download()
        'Dim objSPEstimate As SparePartPOEstimate = New SparePartPOEstimateFacade(User).Retrieve(idSPPOEstimate)
        Dim data As ArrayList = sHTrainee.GetSession("objTrTrainee")
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "DataTrainee" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        '-- Temp file must be a randomly named file!
        Dim TraineeData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(TraineeData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(TraineeData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteTraineeData(sw, data)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteTraineeData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            '-- Write column header
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("TRAINING - Data Status Siswa")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No. Reg" & tab)
            itemLine.Append("Nama Siswa." & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Mulai Bekerja" & tab)
            itemLine.Append("Posisi" & tab)
            itemLine.Append("Pendidikan" & tab)
            itemLine.Append("Daftar Course" & tab)
            itemLine.Append("Belum Lulus MSTEP" & tab)
            itemLine.Append("Belum Lulus General" & tab)
            itemLine.Append("Tgl Lulus Junior" & tab)
            itemLine.Append("Tgl Lulus Senior" & tab)
            itemLine.Append("Tgl Lulus Master" & tab)
            itemLine.Append("Tgl Lulus Specialist E/G & F/S" & tab)
            itemLine.Append("Tgl Lulus Specialist D/T & E/C" & tab)
            itemLine.Append("Status" & tab)
            sw.WriteLine(itemLine.ToString())
            'lblJr.Text = 
            'lblSr.Text = GetPassDateByCategory(passClassCourse, "SENIOR")
            'lblMr.Text = GetPassDateByCategory(passClassCourse, "MASTER")
            For Each item As TrTrainee In data
                itemLine.Remove(0, itemLine.Length)  '-- Empty line
                itemLine.Append(item.ID & tab)
                itemLine.Append(item.Name & tab)
                itemLine.Append(item.Dealer.DealerCode & tab)
                itemLine.Append(item.StartWorkingDate & tab)
                itemLine.Append(item.JobPosition & tab)
                itemLine.Append(item.EducationLevel & tab)
                itemLine.Append(GetCourseList(item) & tab)
                itemLine.Append(GetNotPassCourse(item, EnumTrClass.EnumTrClassCategory.MSTEP) & tab)
                itemLine.Append(GetNotPassCourse(item, EnumTrClass.EnumTrClassCategory.GENERAL_WAJIB) & tab)

                Dim passClassCourse As ArrayList = GetPassCourseList(item)
                itemLine.Append(GetPassDateByCategory(passClassCourse, "SENIOR") & tab)
                itemLine.Append(GetPassDateByCategory(passClassCourse, "JUNIOR") & tab)
                itemLine.Append(GetPassDateByCategory(passClassCourse, "MASTER") & tab)
                itemLine.Append(GetPassDateByCategory(passClassCourse, "SPECIALIS_EGFS") & tab)
                itemLine.Append(GetPassDateByCategory(passClassCourse, "SPECIALIS_DTEC") & tab)

                itemLine.Append(GetRealStatus(item) & tab)
                sw.WriteLine(itemLine.ToString())
            Next
        End If
    End Sub


    Private Function GetPassCourseList(ByVal trainee As TrTrainee) As ArrayList
        Dim sList As ArrayList = New ArrayList
        If Not trainee Is Nothing Then
            Dim courses As System.Text.StringBuilder = New System.Text.StringBuilder
            For Each classReg As TrClassRegistration In trainee.TrClassRegistrations
                If classReg.Status = CStr(EnumTrClassRegistration.DataStatusType.Pass) Then
                    sList.Add(classReg)
                End If
            Next
        End If
        Return sList
    End Function

    Private Function GetCourseList(ByVal trainee As TrTrainee) As String
        If Not trainee Is Nothing Then
            Dim sList As ArrayList = New ArrayList
            Dim courses As System.Text.StringBuilder = New System.Text.StringBuilder
            For Each classReg As TrClassRegistration In trainee.TrClassRegistrations
                If classReg.Status = CStr(EnumTrClassRegistration.DataStatusType.Pass) Then
                    sList.Add(GetCourse(classReg))
                End If
            Next
            sList.Sort()
            For Each strClassReg As String In sList
                courses.Append(strClassReg)
                If Not (strClassReg Is sList(sList.Count - 1)) Then
                    courses.Append(",")
                End If
            Next
            Return courses.ToString
        End If
        Return " "
    End Function

    Private Function GetRealStatus(ByVal trainee As TrTrainee) As String
        If Not IsNothing(trainee.Status) Then
            Dim enumTr As EnumTrTrainee = New EnumTrTrainee
            Return enumTr.StatusByIndex(Integer.Parse(trainee.Status))
        Else
            Return " "
        End If
    End Function
End Class
