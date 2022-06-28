Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports System.Text
Public Class FrmTrTrainee1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgTrainee As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents btnBaru As System.Web.UI.WebControls.Button
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTerm1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblOrganizationCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrganizationName As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNomorReg As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPosisi As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchJobPos As System.Web.UI.WebControls.Label
    Protected WithEvents txtArea As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchRegion As System.Web.UI.WebControls.Label
    Protected WithEvents btnConfirmation As System.Web.UI.WebControls.Button
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents txtClassCategory2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClassCategory As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constanta URL"
    Private Const URL_DETAIL As String = "FrmDetailTrTrainee.aspx"
    Private Const URL_REGISTER As String = "FrmTrClassRegistrationByTrainee1.aspx"

#End Region

#Region "Private Variable"
    Dim sHTrainee As SessionHelper = New SessionHelper
    Dim sessDealer As Dealer
    Private bPrivilegeRegisterClass As Boolean = False
    Private bPrivilegeEditTrainee As Boolean = False
    Private bPrivilegeNew As Boolean = False
#End Region

#Region "Event Method"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim trUpdate As String = Request.QueryString("TraineeUpdate")
        If Not trUpdate Is Nothing Then
            If UpdateConfirmDate(trUpdate) = True And trUpdate = "Yes" Then
                SendConfirmationEmail()
            End If
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        sHTrainee.RemoveSession("veTrainee")
        sessDealer = CType(sHTrainee.GetSession("DEALER"), Dealer)
        ActivateUserPrivilege()
        lblPopUpDealer.Attributes.Add("onClick", "ShowPPDealerSelection();")
        lblSearchJobPos.Attributes("onclick") = "ShowJobPosSelectionMany()"
        lblSearchRegion.Attributes("onclick") = "ShowPPAreaSelection()"
        txtPosisi.Attributes.Add("readonly", "readonly")
        If Not IsPostBack Then
            InitiateMode()
            InitiatePage()
            If Not RestoreLastState() Then
                If sessDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    dtgTrainee.DataSource = New ArrayList
                    dtgTrainee.VirtualItemCount = 0
                    dtgTrainee.DataBind()
                Else
                    BindDataGrid(0)
                End If
            End If
        End If
    End Sub

    Private Sub dtgTrainee_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgTrainee.SortCommand
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

        dtgTrainee.SelectedIndex = -1
        dtgTrainee.CurrentPageIndex = 0
        BindDataGrid(dtgTrainee.CurrentPageIndex)
    End Sub

    Private Sub dtgTrainee_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgTrainee.PageIndexChanged
        dtgTrainee.SelectedIndex = -1
        dtgTrainee.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgTrainee.CurrentPageIndex)
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        Response.Redirect("FrmNewTrTrainee.aspx")
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        Try
            'If txtNomorReg.Text.Trim() <> String.Empty Then
            '    Dim i As Integer = CInt(txtNomorReg.Text.Trim())
            'End If
            dtgTrainee.CurrentPageIndex = 0
            BindDataGrid(0)
        Catch ex As Exception
            MessageBox.Show("Nomor Registrasi tidak boleh karakter.")
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If IsNothing(sHTrainee.GetSession("CritOfTrainee")) Then
            MessageBox.Show("Data tidak ditemukan")
            Return
        End If

        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(TrTrainee), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))

        sHTrainee.SetSession("SortOfTrainee", sortCol)

        Try
            SaveLastState()
        Catch
        End Try

        Response.Redirect("./FrmPrintTrTrainee1.aspx")
    End Sub
    'Add by ANH request from Rina 20100727
    Private Sub btnConfirmation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirmation.Click
        If UpdateConfirmDate("Yes") = True Then 'update data dealer for ConfirmTraineeDate
            SendConfirmationEmail() 'send confirmation email
            btnConfirmation.Enabled = False
        End If
    End Sub
    Private Sub SendConfirmationEmail()
        Dim appConfigFacade As New AppConfigFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim objUserInfo As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim contentEmail As String = GetEmailNotification()

        Dim emailTo As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_TO_TC_ADMIN).Value
        Dim emailFrom As String = objUserInfo.Email
        If emailFrom = "" Then
            emailFrom = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_FROM_GENERAL).Value
        End If
        ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet] Service - (Validitas data siswa Training)", Mail.MailFormat.Html, contentEmail)
    End Sub
    Private Function UpdateConfirmDate(ByVal strUpdate As String) As Boolean
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim objAlertConfirmationFacade As New KTB.DNet.BusinessFacade.AlertManagement.AlertConfirmationFacade(User)
        Dim arlAlertConfirmation As New ArrayList
        Dim bReturn As Boolean = False
        Dim idAlert As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("IdUpdateSiswaTraining"))

        arlAlertConfirmation = objAlertConfirmationFacade.RetrieveByAlertDealer(idAlert, objDealer.ID)
        If arlAlertConfirmation.Count > 0 Then
            For Each objAlertConfirmation As AlertConfirmation In arlAlertConfirmation
                Try
                    If strUpdate = "Yes" Then
                        objAlertConfirmation.ConfirmDate = Date.Now.AddMonths(3)
                    Else
                        objAlertConfirmation.ConfirmDate = Date.Now
                    End If
                    objAlertConfirmationFacade.Update(objAlertConfirmation)
                    bReturn = True
                Catch ex As Exception
                    bReturn = False
                End Try
            Next
        Else
            Dim objAC As New AlertConfirmation
            Dim objAlertMaster As New AlertMaster
            Dim objAlertMasterFacade As New KTB.DNet.BusinessFacade.AlertManagement.AlertMasterFacade(User)
            Try
                objAlertMaster = objAlertMasterFacade.Retrieve(idAlert)

                objAC.AlertMaster = objAlertMaster
                objAC.DealerID = objDealer.ID
                objAC.Status = 0
                objAC.RowStatus = 0
                If strUpdate = "Yes" Then
                    objAC.ConfirmDate = Date.Now.AddMonths(3)
                Else
                    objAC.ConfirmDate = Date.Now
                End If

                objAlertConfirmationFacade.Insert(objAC)
                bReturn = True
            Catch
                bReturn = False
            End Try
        End If
        Return bReturn
    End Function

    Private Function GetEmailNotification() As String
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim objUserInfo As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim contentEmail As String

        contentEmail = "<P>Kepada Yth,</P>"
        contentEmail += "<P>PT.MMKSI-TC</P>"
        contentEmail += "<P>Ditempat</P>"
        contentEmail += "<P>&nbsp;</P>"
        contentEmail += "<P>Data status siswa " & objDealer.DealerName & " - " & objDealer.City.CityName & ",</P>"
        contentEmail += "<P>adalah valid per tanggal " & Date.Now.ToString("dd-MM-yyyy") & ".</P>"
        contentEmail += "<P>&nbsp;</P>"
        contentEmail += "<P>Regards,</P>"
        contentEmail += "<P>" & objUserInfo.UserName & "</P>"
        contentEmail += "<P>" & objDealer.DealerCode & " - " & objDealer.DealerName & "</P>"
        contentEmail += "<P>" & objDealer.City.CityName & "</P>"

        Return contentEmail
    End Function

    Private Sub dtgTrainee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTrainee.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgTrainee.CurrentPageIndex * dtgTrainee.PageSize)
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As TrTrainee = CType(e.Item.DataItem, TrTrainee)

            Dim lblCourses As Label = CType(e.Item.FindControl("lblCourses"), Label)
            Dim lblMSTEP As Label = CType(e.Item.FindControl("lblMSTEP"), Label)
            Dim lblGeneral As Label = CType(e.Item.FindControl("lblGeneral"), Label)
            Dim lblJr As Label = CType(e.Item.FindControl("lblJr"), Label)
            Dim lblSr As Label = CType(e.Item.FindControl("lblSr"), Label)
            Dim lblSrE As Label = CType(e.Item.FindControl("lblSrE"), Label)
            Dim lblSrD As Label = CType(e.Item.FindControl("lblSrD"), Label)
            Dim lblMr As Label = CType(e.Item.FindControl("lblMr"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim btnLihat As LinkButton = CType(e.Item.FindControl("btnLihat"), LinkButton)
            Dim btnDaftar As LinkButton = CType(e.Item.FindControl("btnDaftar"), LinkButton)
            btnDaftar.Enabled = False

            If Not IsNothing(RowValue.TrClassRegistrations) Then
                Dim courses As System.Text.StringBuilder = New System.Text.StringBuilder
                Dim sList As ArrayList = New ArrayList
                Dim passCourse As ArrayList = New ArrayList
                Dim passClassCourse As ArrayList = New ArrayList
                For Each classReg As TrClassRegistration In RowValue.TrClassRegistrations
                    If classReg.Status = CStr(EnumTrClassRegistration.DataStatusType.Pass) Then
                        passCourse.Add(GetCourse(classReg))
                        passClassCourse.Add(classReg)
                        sList.Add(GetCourse(classReg))
                    End If
                Next
                sList.Sort()

                For Each strClassReg As String In sList
                    courses.Append(strClassReg)
                    If Not (strClassReg Is sList(sList.Count - 1)) Then
                        courses.Append(", ")
                    End If
                Next

                lblCourses.Text = courses.ToString
                Dim objArea As Area1 = RowValue.Dealer.Area1
                Dim areaName As String = String.Empty
                If Not objArea Is Nothing Then
                    areaName = objArea.Description
                End If
                lblDealerCode.Text = RowValue.Dealer.DealerCode & " - " & areaName

                Dim mStepList As ArrayList = GetNotPassCourseByCategory(passCourse, GetCourseByCategory(EnumTrClass.EnumTrClassCategory.MSTEP))
                Dim mCategory As StringBuilder = New StringBuilder
                For Each strCat As String In mStepList
                    mCategory.Append(strCat)
                    If Not (strCat Is mStepList(mStepList.Count - 1)) Then
                        mCategory.Append(", ")
                    End If
                Next
                lblMSTEP.Text = mCategory.ToString

                mStepList = GetNotPassCourseByCategory(passCourse, GetCourseByCategory(EnumTrClass.EnumTrClassCategory.GENERAL_WAJIB))
                mCategory = New StringBuilder
                For Each strCat As String In mStepList
                    mCategory.Append(strCat)
                    If Not (strCat Is mStepList(mStepList.Count - 1)) Then
                        mCategory.Append(", ")
                    End If
                Next
                lblGeneral.Text = mCategory.ToString
                lblJr.Text = GetPassDateByCategory(passClassCourse, "JUNIOR")
                lblSr.Text = GetPassDateByCategory(passClassCourse, "SENIOR")
                lblSrE.Text = GetPassDateByCategory(passClassCourse, "SPECIALIS_EGFS")
                lblSrD.Text = GetPassDateByCategory(passClassCourse, "SPECIALIS_DTEC")
                lblMr.Text = GetPassDateByCategory(passClassCourse, "MASTER")

            End If

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

            If Not IsNothing(RowValue.Status) Then
                Dim enumTr As EnumTrTrainee = New EnumTrTrainee
                lblStatus.Text = enumTr.StatusByIndex(Integer.Parse(RowValue.Status))
                If RowValue.Status = CStr(enumTr.TrTraineeStatus.Active) Then
                    btnDaftar.Enabled = True
                Else
                    btnDaftar.Text = "<img src='../images/in-aktif.gif' border='0' alt='Daftar'>"
                End If
            End If
            'Add by anh 20120501 'penambahan gender dan birthdate
            Dim lblGender As Label = CType(e.Item.FindControl("lblGender"), Label)
            If Not IsNothing(RowValue.Gender) Or RowValue.Gender > 0 Then
                lblGender.Text = EnumGender.GetStringGender(CType(RowValue.Gender, Integer))
            Else
                lblGender.Text = ""
            End If
            'end added by anh
            SetControlPrivilege(btnLihat, btnDaftar)

        End If
    End Sub

    Private Sub dtgTrainee_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgTrainee.ItemCommand
        If (e.CommandName = "RegisterToClass") Then
            Try
                SaveLastState()
            Catch
            End Try
            TraineeRedirect(URL_REGISTER, Integer.Parse(e.Item.Cells(1).Text))
        ElseIf e.CommandName = "Detail" Then
            Try
                SaveLastState()
            Catch
            End Try
            TraineeRedirect(URL_DETAIL, Integer.Parse(e.Item.Cells(1).Text))
        End If
    End Sub
#End Region

#Region "Custom Method"
    Private Sub InitiateMode()
        lblPopUpDealer.Visible = sessDealer.Title = EnumDealerTittle.DealerTittle.KTB
        ' btnCari.Visible = lblPopUpDealer.Visible
        txtDealerCode.ReadOnly = Not lblPopUpDealer.Visible
        If sessDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            txtDealerCode.Text = sessDealer.DealerCode
        Else
            btnBaru.Visible = False
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If (sessDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            If Not SecurityProvider.Authorize(Context.User, SR.TrainingViewDataSiswaDealer_Privilege) Then
                Unauthorize()
                Return
            End If

            bPrivilegeNew = SecurityProvider.Authorize(Context.User, SR.TrainingAddSiswa_Privilege)
            btnBaru.Visible = bPrivilegeNew

            'bPrivilegeRegisterClass = SecurityProvider.Authorize(Context.User, SR.TrainingDaftarDataSiswaDealer_Privilege)
            bPrivilegeEditTrainee = SecurityProvider.Authorize(Context.User, SR.TrainingUpdateSiswaDealer_Privilege)
            If SecurityProvider.Authorize(Context.User, SR.TrainingDaftarDataSiswaDealer_Privilege) _
            And PassTransactionControl(sessDealer) Then
                bPrivilegeRegisterClass = True
            End If
        ElseIf (sessDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            If Not SecurityProvider.Authorize(Context.User, SR.TrainingViewDataSiswaKTB_Privilege) Then
                Unauthorize()
                Return
            End If

            'bPrivilegeRegisterClass = SecurityProvider.Authorize(Context.User, SR.TrainingDaftarDataSiswaKTB_Privilege)
            bPrivilegeEditTrainee = SecurityProvider.Authorize(Context.User, SR.TrainingUpdateSiswa_Privilege)
            If SecurityProvider.Authorize(Context.User, SR.TrainingDaftarDataSiswaKTB_Privilege) _
                And PassTransactionControl(sessDealer) Then
                bPrivilegeRegisterClass = True
            End If
        Else
            Unauthorize()
        End If
    End Sub

    Private Sub Unauthorize()
        Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Data Status Siswa")
    End Sub

    Private Function PassTransactionControl(ByVal objDealer As Dealer) As Boolean
        Dim bPassTC As Boolean = True

        Dim objTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl( _
            objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.DaftarTraining).ToString())

        If Not objTC Is Nothing Then
            If objTC.Status = 0 Then
                bPassTC = False
            End If
        End If

        Return bPassTC
    End Function

    Private Sub InitiatePage()
        lblOrganizationCode.Text = sessDealer.DealerCode
        lblOrganizationName.Text = sessDealer.DealerName
        lblSearchTerm1.Text = sessDealer.SearchTerm1
        If Not IsNothing(sessDealer.City) Then
            lblCity.Text = sessDealer.City.CityName
        End If
        Dim enumTrainee As EnumTrTrainee = New EnumTrTrainee
        ddlStatus.DataSource = enumTrainee.RetrieveStatus
        ddlStatus.DataTextField = "NameTitle"
        ddlStatus.DataValueField = "ValTitle"
        ddlStatus.DataBind()
        ddlStatus.SelectedIndex = 1

        'Dim liblank As ListItem = New ListItem("Silahkan Pilih", -1)
        'Dim listClassReg As ArrayList = New TrClassFacade(User).RetrieveActiveList()
        'For Each item As TrClass In listClassReg
        '    Dim li As ListItem = New ListItem(item.TrCourse.CourseCode, item.ID)
        '    lbPass.Items.Add(li)
        'Next

        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Function GetCourseByCategory(ByVal cat As EnumTrClass.EnumTrClassCategory) As ArrayList
        Dim trCFacade As TrCourseFacade = New TrCourseFacade(User)
        Dim objCritCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objCritCol.opAnd(New Criteria(GetType(TrCourse), "Category", MatchType.Exact, CType(cat, Integer)))
        'objCritCol.opAnd(New Criteria(GetType(TrCourse), "Status", MatchType.Exact, EnumTrDataStatus.DataStatusType.Active))

        Dim list As ArrayList = trCFacade.Retrieve(objCritCol)
        Return list
    End Function

    Private Function GetCourseMstepGeneral() As ArrayList
        Dim trCFacade As TrCourseFacade = New TrCourseFacade(User)
        Dim objCritCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objCritCol.opAnd(New Criteria(GetType(TrCourse), "Category", MatchType.InSet, "(1,2)"))

        Dim list As ArrayList = trCFacade.Retrieve(objCritCol)
        Return list
    End Function


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

    Private Function GetNotPassCourseByCategory2(ByVal listPassCourse As ArrayList, ByVal listCourseByCategory As ArrayList) As ArrayList
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
            If found Then
                result.Add(item.CourseCode)
            End If
        Next
        Return result
    End Function

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                              ByVal SortDirection As Integer)
        '-- Sort arraylist

        If SortColumn.Trim <> "" Then
            Dim isASC As Boolean = (SortDirection = Sort.SortDirection.ASC)  '-- Is sorted ascending?
            Dim objListComparer As IComparer = New ListComparer(isASC, SortColumn)
            pCompletelist.Sort(objListComparer)
        End If

    End Sub

    Private Sub BindDataGridX(ByVal indexPage As Integer)
        btnPrint.Enabled = False
        Try
            If (indexPage >= 0) Then
                Dim totalRow As Integer
                dtgTrainee.DataSource = GetTraineeList(indexPage, totalRow)
                dtgTrainee.VirtualItemCount = totalRow
                dtgTrainee.DataBind()
                If CType(dtgTrainee.DataSource, ArrayList).Count = 0 Then
                    Throw New Exception(SR.DataNotFound("Data Siswa"))
                End If
                btnPrint.Enabled = True
                ViewState("currentPage") = dtgTrainee.CurrentPageIndex
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

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

    Private Sub BindDataGrid(ByVal pageIndex As Integer)
        Dim objTraineeList As ArrayList
        Dim tempList As ArrayList = New ArrayList
        Dim tempList2 As ArrayList = New ArrayList
        Dim orgList As ArrayList = GetTraineeList()

        If txtClassCategory.Text.Trim <> String.Empty Then
            For Each item As String In txtClassCategory.Text.Trim.Split(";")
                tempList = FilterList(orgList, item.Trim.ToUpper)
                orgList = tempList
            Next
        End If
        If txtClassCategory2.Text.Trim <> String.Empty Then
            For Each item As String In txtClassCategory2.Text.Trim.Split(";")
                tempList = FilterListNoPass(orgList, item.Trim.ToUpper)
                orgList = tempList
            Next
        End If

        objTraineeList = orgList

        ViewState("currentPage") = dtgTrainee.CurrentPageIndex
        If objTraineeList.Count <> 0 Then
            '-- Sort first
            SortListControl(objTraineeList, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            '-- Then paging
            Dim PagedList As ArrayList = ArrayListPager.DoPage(objTraineeList, pageIndex, dtgTrainee.PageSize)
            dtgTrainee.DataSource = PagedList
            dtgTrainee.VirtualItemCount = objTraineeList.Count()
            dtgTrainee.CurrentPageIndex = pageIndex
            dtgTrainee.DataBind()
            btnPrint.Enabled = True
            ViewState("currentPage") = dtgTrainee.CurrentPageIndex


        Else
            '-- Display datagrid header only
            dtgTrainee.DataSource = New ArrayList
            dtgTrainee.VirtualItemCount = 0
            dtgTrainee.CurrentPageIndex = 0
            dtgTrainee.DataBind()
        End If

    End Sub

    Private Function GetTraineeList() As ArrayList
        Dim objCritCol As CriteriaComposite = CreateCriteriaOfTrainees()
        Dim trList As ArrayList = New TrTraineeFacade(User).Retrieve(objCritCol)
        Return trList
    End Function

    Private Function GetTraineeList(ByVal indexPage As Integer, ByRef totalRow As Integer) As ArrayList
        Dim objCritCol As CriteriaComposite = CreateCriteriaOfTrainees()
        Dim trList As ArrayList = New TrTraineeFacade(User).RetrieveActiveList(objCritCol, indexPage + 1, dtgTrainee.PageSize, totalRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
        Return trList
    End Function

    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Function CreateCriteriaOfTrainees() As CriteriaComposite
        Dim objCritCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If sessDealer.Title = EnumDealerTittle.DealerTittle.KTB And txtDealerCode.Text <> String.Empty Then
            Dim dealers As String = New System.Text.StringBuilder("('").Append(txtDealerCode.Text).Append("')").ToString().Replace(";", "','")
            objCritCol.opAnd(New Criteria(GetType(TrTrainee), "Dealer.DealerCode", MatchType.InSet, dealers))
        ElseIf sessDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            objCritCol.opAnd(New Criteria(GetType(TrTrainee), "Dealer.DealerCode", MatchType.Exact, sessDealer.DealerCode))
        End If

        If ddlStatus.SelectedValue <> -1 Then
            objCritCol.opAnd(New Criteria(GetType(TrTrainee), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If txtNomorReg.Text.Trim <> String.Empty Then
            objCritCol.opAnd(New Criteria(GetType(TrTrainee), "ID", MatchType.Exact, txtNomorReg.Text.Trim))
        End If

        If txtNama.Text.Trim <> String.Empty Then
            Dim strName() As String = txtNama.Text.Split(",")
            For Each item As String In strName
                objCritCol.opAnd(New Criteria(GetType(TrTrainee), "Name", MatchType.[Partial], item))
            Next
        End If

        If txtPosisi.Text.Trim <> String.Empty Then
            objCritCol.opAnd(New Criteria(GetType(TrTrainee), "JobPosition", MatchType.InSet, "('" & txtPosisi.Text.Replace(";", "','") & "')"))
        End If


        If txtArea.Text <> String.Empty Then
            'objCritCol.opAnd(New Criteria(GetType(TrTrainee), "Dealer.Area2.AreaCode", MatchType.Partial, txtArea.Text.Trim))
            objCritCol.opAnd(New Criteria(GetType(TrTrainee), "Dealer.Area1.AreaCode", MatchType.InSet, "('" & txtArea.Text.Replace(";", "','") & "')"))

        End If

        sHTrainee.SetSession("CritOfTrainee", objCritCol)
        sHTrainee.SetSession("classCategory", txtClassCategory.Text.Trim)
        sHTrainee.SetSession("classCategory2", txtClassCategory2.Text.Trim)
        Return objCritCol
    End Function

    Private Sub SetControlPrivilege(ByVal editButton As Control, ByVal registerButton As Control)
        editButton.Visible = bPrivilegeEditTrainee
        registerButton.Visible = bPrivilegeRegisterClass
    End Sub

    Private Function GetCourse(ByVal nTrClassRegistration As TrClassRegistration) As String
        Return nTrClassRegistration.TrClass.TrCourse.CourseCode
    End Function

    Private Sub TraineeRedirect(ByVal url As String, ByVal nIdTrainee As Integer)
        Dim selectedTrainee As TrTrainee = New TrTraineeFacade(User).Retrieve(nIdTrainee)
        sHTrainee.SetSession("veTrainee", selectedTrainee)
        Response.Redirect(url)
    End Sub

    Private Sub SaveLastState()
        sHTrainee.SetSession("lastobjDealer", txtDealerCode.Text)
        sHTrainee.SetSession("currentPage", ViewState("currentPage"))
        sHTrainee.SetSession("classCategory", ViewState("currentPage"))
        sHTrainee.SetSession("status", ddlStatus.SelectedIndex)
        sHTrainee.SetSession("classCategory", txtClassCategory.Text.Trim)
        sHTrainee.SetSession("nomor_reg", txtNomorReg.Text.Trim)
        sHTrainee.SetSession("_name", txtNama.Text.Trim)
        sHTrainee.SetSession("posisi", txtPosisi.Text.Trim)
    End Sub

    Private Function RestoreLastState() As Boolean
        If Not IsNothing(sHTrainee.GetSession("lastobjDealer")) Then
            txtDealerCode.Text = CStr(sHTrainee.GetSession("lastobjDealer"))
            dtgTrainee.CurrentPageIndex = sHTrainee.GetSession("currentPage")
            txtClassCategory.Text = sHTrainee.GetSession("classCategory")
            txtClassCategory2.Text = sHTrainee.GetSession("classCategory2")
            ddlStatus.SelectedIndex = sHTrainee.GetSession("status")
            txtNomorReg.Text = sHTrainee.GetSession("nomor_reg")
            txtNama.Text = sHTrainee.GetSession("_name")
            txtPosisi.Text = sHTrainee.GetSession("posisi")
            BindDataGrid(dtgTrainee.CurrentPageIndex)
            sHTrainee.RemoveSession("lastobjDealer")
            sHTrainee.RemoveSession("currentPage")
            Return True
        End If
        Return False
    End Function
#End Region

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("../PopUp/PopUpArea2.aspx")
    End Sub


End Class
