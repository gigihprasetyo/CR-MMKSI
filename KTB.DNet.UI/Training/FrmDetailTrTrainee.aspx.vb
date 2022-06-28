Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI

Public Class FrmDetailTrTrainee
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtJobPosition As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKTP As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtEducationLevel As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerBranchCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlShirtSize As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents photoSrc As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents photoView As System.Web.UI.WebControls.Image
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents lblTraineeName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.HiddenField
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblStartDate As System.Web.UI.WebControls.Label
    Protected WithEvents btnRegister As System.Web.UI.WebControls.Button
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents dtgCourseClass As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cbDeletePhoto As System.Web.UI.WebControls.CheckBox
    Protected WithEvents messageValidationSummary As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents btnCetak As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchJobPos As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents ddlGender As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Requiredfieldvalidator7 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents icBirthDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents Requiredfieldvalidator8 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator9 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
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
    Private Const URL_REGISTER As String = "FrmTrClassRegistrationByTrainee1.aspx"
#End Region

    Dim sHTrainee As SessionHelper = New SessionHelper
    Dim objTrainee As TrTrainee
    Dim sessDealer As Dealer


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sessDealer = sHTrainee.GetSession("DEALER")
        lblDealerCode.Value = sessDealer.DealerCode
        objTrainee = sHTrainee.GetSession("veTrainee")
        lblSearchJobPos.Attributes("onclick") = "ShowJobPosSelection()"
        lblPopUpDealerBranch.Attributes.Add("onClick", "ShowPPDealerBranchSelection();")
        If IsNothing(objTrainee) Then
            Response.Redirect("../Login.aspx#Expired")
        End If
        If Not IsPostBack Then
            InitiatePage()
            FillFormFromObject(objTrainee)
            BindDataToPage()
        End If
        txtJobPosition.Attributes.Add("readonly", "readonly")
        ActivateUserPrivilege()
        SetFieldMandatory()
    End Sub

    Protected Sub SetFieldMandatory()
        Requiredfieldvalidator8.Enabled = IsEmailAndKtpMandatory()
        RequiredFieldValidator9.Enabled = IsEmailAndKtpMandatory()
    End Sub

    Private Function IsEmailAndKtpMandatory() As Boolean
        Dim objUser As UserInfo = CType(sHTrainee.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser.Dealer.Title = "0" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ActivateUserPrivilege()
        Dim bPrivilegeRegisterClass As Boolean
        bPrivilegeRegisterClass = SecurityProvider.Authorize(Context.User, SR.TrainingPendaftaran_Privilege)
        btnRegister.Visible = bPrivilegeRegisterClass

        If (sessDealer.Title = EnumDealerTittle.DealerTittle.DEALER And Not SecurityProvider.Authorize(Context.User, SR.TrainingViewDataSiswaDealer_Privilege)) Or _
            (sessDealer.Title = EnumDealerTittle.DealerTittle.KTB And Not SecurityProvider.Authorize(Context.User, SR.TrainingViewDataSiswaKTB_Privilege)) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Siswa")
        End If
    End Sub

    Private Sub InitiatePage()
        sHTrainee.RemoveSession("dtRepeater")

        Dim objEnumTrTrainee = New EnumTrTrainee

        Dim TraineeStatus As ArrayList = objEnumTrTrainee.RetrieveStatus()
        For Each ts As EnumTrTraineeData In TraineeStatus
            If Not (ts Is TraineeStatus.Item(0)) Then
                Dim lItem As ListItem = New ListItem(ts.NameTitle, ts.ValTitle.ToString)
                ddlStatus.Items.Add(lItem)
            End If
        Next

        Dim ShirtSize As ArrayList = objEnumTrTrainee.RetrieveSize()
        For Each es As EnumShirtData In ShirtSize
            Dim lItem As ListItem = New ListItem(es.NameTitle, es.ValTitle.ToString)
            ddlShirtSize.Items.Add(lItem)
        Next

        CommonFunction.BindFromEnum("SalesmanGender", ddlGender, User, False, "NameStatus", "ValStatus")
        ddlGender.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
        ddlGender.SelectedIndex = 0

        'Dim li As ListItem = New ListItem(String.Empty, String.Empty)
        'ddlStatus.Items.Insert(0, li)
        'ddlShirtSize.Items.Insert(0, li)

        ViewState("CurrentSortColumn") = "TrClass.TrCourse.CourseCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDataToPage()
        LoadRepeater()
    End Sub

    Private Sub FillingObjectFromForm(ByVal objTrainee As TrTrainee)
        objTrainee.NoKTP = Me.txtKTP.Text
        objTrainee.Email = Me.txtEmail.Text
        objTrainee.BirthDate = Me.icBirthDate.Value
        objTrainee.Gender = Me.ddlGender.SelectedValue
        objTrainee.JobPosition = txtJobPosition.Text
        objTrainee.EducationLevel = txtEducationLevel.Text
        objTrainee.ShirtSize = ddlShirtSize.SelectedItem.Text
        If (txtDealerBranchCode.Text.Trim <> String.Empty) Then
            objTrainee.DealerBranch = New DealerBranchFacade(User).Retrieve(txtDealerBranchCode.Text)
        End If
        If ddlStatus.Visible Then
            objTrainee.Status = ddlStatus.SelectedValue.ToString
        End If
    End Sub

    Private Sub FillFormFromObject(ByVal objTrainee As TrTrainee)
        If Not IsNothing(objTrainee.BirthDate) Then
            Me.icBirthDate.Value = objTrainee.BirthDate
        Else
            Me.icBirthDate.Value = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day)
        End If
        If Not IsNothing(objTrainee.Gender) And CType(objTrainee.Gender, Integer) > 0 Then
            Me.ddlGender.SelectedValue = CType(objTrainee.Gender, Integer)
        Else
            Me.ddlGender.SelectedIndex = 0
        End If
        Me.lblTraineeName.Text = objTrainee.Name
        Me.lblDealerName.Text = objTrainee.Dealer.DealerName
        If Not IsNothing(objTrainee.DealerBranch) Then
            Me.txtDealerBranchCode.Text = objTrainee.DealerBranch.DealerBranchCode
        End If
        Me.txtKTP.Text = objTrainee.NoKTP
        Me.txtEmail.Text = objTrainee.Email
        Me.lblCity.Text = objTrainee.Dealer.City.CityName
        Me.lblStartDate.Text = objTrainee.StartWorkingDate.ToShortDateString
        Me.txtJobPosition.Text = objTrainee.JobPosition
        Me.txtEducationLevel.Text = objTrainee.EducationLevel
        Me.photoSrc.Attributes("value") = String.Empty
        Dim itemShirt As ListItem = Me.ddlShirtSize.Items.FindByText(objTrainee.ShirtSize)
        Me.ddlShirtSize.SelectedIndex = Me.ddlShirtSize.Items.IndexOf(itemShirt)
        If Integer.Parse(objTrainee.Status) = EnumTrTrainee.TrTraineeStatus.Unapproved Then
            Me.ddlStatus.Visible = False
            Me.lblStatus.Visible = True
            Me.lblStatus.Text = New EnumTrTrainee().StatusByIndex(Integer.Parse(objTrainee.Status))
        Else
            Me.ddlStatus.Visible = True
            Me.lblStatus.Visible = False
            Dim itemStatus As ListItem = Me.ddlStatus.Items.FindByValue(objTrainee.Status)
            Me.ddlStatus.SelectedIndex = Me.ddlStatus.Items.IndexOf(itemStatus)
        End If
        Me.photoView.ImageUrl = "../WebResources/GetPhoto.aspx?id=" + objTrainee.ID.ToString

        btnRegister.Enabled = False
        If objTrainee.Status = CStr(EnumTrTrainee.TrTraineeStatus.Active) Then
            btnRegister.Enabled = True
        End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmTrTrainee1.aspx")
    End Sub

    Private Function RegOfTraineeCriteria(ByVal TraineeID As Integer) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, TraineeID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Register, String)))
        Return criterias
    End Function

    Private Function GetRegOfTrainee(ByVal objTrainee As TrTrainee) As ArrayList
        Dim objRegFacade As New TrClassRegistrationFacade(User)
        Return objRegFacade.Retrieve(RegOfTraineeCriteria(objTrainee.ID))
    End Function

    Private Function BuildStringOfRegClassColl(ByVal RegClassColl As ArrayList) As String
        Dim strClassColl As String = String.Empty
        For x As Integer = 0 To RegClassColl.Count - 1
            Dim objReg As TrClassRegistration = CType(RegClassColl(x), TrClassRegistration)
            If x <> RegClassColl.Count - 1 Then
                strClassColl = strClassColl & objReg.TrClass.ClassCode & ", "
            Else
                strClassColl = strClassColl & objReg.TrClass.ClassCode
            End If
        Next
        Return strClassColl
    End Function

    Private Sub UpdateTrainee()
        Dim nResult = -1
        Dim imageFile As Byte()
        Dim objTraineeFacade As TrTraineeFacade = New TrTraineeFacade(User)
        Dim arlRegClassColl As New ArrayList
        Try
            If objTrainee.Status.Trim().ToUpper() <> ddlStatus.SelectedValue.Trim().ToUpper() And _
                ddlStatus.SelectedValue = CType(EnumTrTrainee.TrTraineeStatus.Deactive, String) Then
                arlRegClassColl = GetRegOfTrainee(objTrainee)
            End If

            If arlRegClassColl.Count = 0 Then
                imageFile = Nothing
                If Not cbDeletePhoto.Checked Then
                    imageFile = UploadFile()
                End If

                FillingObjectFromForm(objTrainee)

                If photoSrc.Value <> String.Empty Or cbDeletePhoto.Checked Then
                    objTrainee.Photo = imageFile
                End If

                If IsFieldValidationUpdated(objTrainee) And IsTraineeExist(objTraineeFacade, objTrainee) Then
                    Throw New Exception(SR.DataIsExist("Siswa"))
                End If

                nResult = objTraineeFacade.Update(objTrainee)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    sHTrainee.SetSession("veTrainee", objTrainee)
                    MessageBox.Show(SR.SaveSuccess)
                    cbDeletePhoto.Checked = False
                End If
            Else
                MessageBox.Show("Data siswa - Status tidak bisa dirubah, karena siswa sudah terdaftar di kelas : \n" & _
                    BuildStringOfRegClassColl(arlRegClassColl) & "")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function UploadFile() As Byte()
        Dim nResult() As Byte

        If photoSrc.Value = "" Then
            Return Nothing
        End If

        If Not (photoSrc.PostedFile Is Nothing) Then
            Try
                If IsValidPhoto(photoSrc.PostedFile) Then
                    Dim inStream As System.IO.Stream = photoSrc.PostedFile.InputStream()
                    Dim ByteRead(TrTrainee.MAX_PHOTO_SIZE) As Byte
                    Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, TrTrainee.MAX_PHOTO_SIZE)
                    If ReadCount = 0 Then
                        Throw New InvalidConstraintException(SR.DataNotFound("Photo"))
                    End If
                    ReDim nResult(ReadCount)
                    Array.Copy(ByteRead, nResult, ReadCount)
                Else
                    Throw New DataException("Ukuran file foto > 20KB")
                End If
            Catch
                Throw
            End Try
        End If

        Return nResult
    End Function

    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(TrTrainee.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= TrTrainee.MAX_PHOTO_SIZE)
        Return (containImage AndAlso sizeValid)
    End Function

    Private Function IsFormValid() As Boolean
        If txtEducationLevel.Text = String.Empty Then
            Return False
        End If

        If txtJobPosition.Text = String.Empty Then
            Return False
        End If

        'If ddlShirtSize.SelectedIndex = 0 Then
        '    Return False
        'End If
        Dim liShirtSize As ListItem
        liShirtSize = ddlShirtSize.Items.FindByValue(ddlShirtSize.SelectedValue)
        If IsNothing(liShirtSize) Then
            Return False
        End If

        If ddlStatus.Visible Then
            'If ddlStatus.SelectedIndex = 0 Then
            '    Return False
            'End If
            Dim liStatus As ListItem
            liStatus = ddlStatus.Items.FindByValue(ddlStatus.SelectedValue)
            If IsNothing(liStatus) Then
                Return False
            End If
        End If

        Return True
    End Function

    Private Function IsUnhack() As Boolean
        If txtEducationLevel.Text.IndexOf("<") >= 0 Or txtEducationLevel.Text.IndexOf(">") >= 0 Or txtEducationLevel.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtJobPosition.Text.IndexOf("<") >= 0 Or txtJobPosition.Text.IndexOf(">") >= 0 Or txtJobPosition.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        Return True
    End Function

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not IsFormValid() Then
            MessageBox.Show("Data masih kosong")
            Return
        End If

        If CType(ddlGender.SelectedValue, Integer) < 1 Then
            MessageBox.Show("Pilih Jenis Kelamin")
            Return
        End If

        If (Date.Now.Year - Me.icBirthDate.Value.Year) < 17 Then
            MessageBox.Show("Umur siswa tidak memenuhi syarat/ Tanggal lahir di cek kembali")
            Return
        End If

        If (Me.icBirthDate.Value.Year) < 1930 Then
            MessageBox.Show("Tanggal lahir tolong di cek kembali")
            Return
        End If

        If Not ValidateDealerBranchIfExist(txtDealerBranchCode.Text) Then
            MessageBox.Show(SR.DataNotFound("Kode Cabang Dealer"))
            Return
        End If

        If Not IsUnhack() Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return
        End If

        Try
            UpdateTrainee()
        Catch
            MessageBox.Show(SR.SaveFail)
        End Try

        BindDataToPage()
    End Sub

    Private Function ValidateDealerBranchIfExist(ByVal DealerBranchCode As String) As Boolean
        If txtDealerBranchCode.Text.Trim <> String.Empty Then
            Dim dealerBranch As DealerBranch = New DealerBranchFacade(User).Retrieve(DealerBranchCode)
            If IsNothing(dealerBranch) Then
                Return False
            ElseIf (dealerBranch.Dealer.DealerCode <> sessDealer.DealerCode) Then
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function

    Private Sub LoadRepeater()

        'Dim ds As DataTable = sHTrainee.GetSession("dtRepeater")
        Dim ds As ArrayList = sHTrainee.GetSession("dtRepeater")

        If IsNothing(ds) Then

            ds = objTrainee.TrClassRegistrations
            'Dim ClassRegistrationList As ArrayList = objTrainee.TrClassRegistrations
            'Dim CourseList As SortedList = New SortedList

            'For Each objClassRegistration As TrClassRegistration In ClassRegistrationList
            '    If objClassRegistration.Status = CStr(TrClassRegistration.EnumClassRegStatus.Lulus) Then
            '        Dim Key As String = objClassRegistration.TrClass.TrCourse.CourseCode

            '        Dim idx As Integer = CourseList.IndexOfKey(Key)
            '        If idx < 0 Then
            '            Dim Values As String = objClassRegistration.TrClass.ClassCode
            '            CourseList.Add(Key, Values)
            '        Else
            '            Dim Values As String = CourseList.Item(Key)
            '            Values += ", "
            '            Values += objClassRegistration.TrClass.ClassCode
            '            CourseList.Item(Key) = Values
            '        End If
            '    End If
            'Next

            'ds = ConvertSortedListToDataTable(CourseList)
            CleanUpUngraduatedClass(ds)
            'sHTrainee.SetSession("dtRepeater", ds)
            sHTrainee.SetSession("dtRepeater", ds)
        End If

        'ds.DefaultView.Sort = ViewState("CurrentSortColumn") + " "
        'Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

        '    Case Sort.SortDirection.ASC
        '        ds.DefaultView.Sort += "ASC"

        '    Case Sort.SortDirection.DESC
        '        ds.DefaultView.Sort += "DESC"
        'End Select

        'dtgCourseClass.DataSource = ds.DefaultView

        'Sort
        If Not IsNothing(ds) Then
            Dim isAsc As Boolean = ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Dim iCmp As IComparer = New ListComparer(isAsc, ViewState("CurrentSortColumn"))
            ds.Sort(iCmp)
        End If

        dtgCourseClass.DataSource = ds
        dtgCourseClass.DataBind()

    End Sub

    Private Sub CleanUpUngraduatedClass(ByVal classes As ArrayList)
        If Not IsNothing(classes) Then
            For idx As Integer = classes.Count - 1 To 0 Step -1
                Dim item As TrClassRegistration = classes(idx)
                If item.Status <> EnumTrClassRegistration.DataStatusType.Pass Then
                    classes.Remove(item)
                End If
            Next
        End If
    End Sub

    Private Function ConvertSortedListToDataTable(ByVal slInput As SortedList) As DataTable
        Dim nResult As DataTable = New DataTable

        Dim dtCol As DataColumn = New DataColumn("Category", GetType(String))
        nResult.Columns.Add(dtCol)
        dtCol = New DataColumn("ClassList", GetType(String))
        nResult.Columns.Add(dtCol)

        Dim dtRow As DataRow
        For idx As Integer = 0 To slInput.Count - 1
            dtRow = nResult.NewRow()
            dtRow(0) = slInput.GetKey(idx)
            dtRow(1) = slInput.GetByIndex(idx)
            nResult.Rows.Add(dtRow)
        Next
        Return nResult
    End Function

    Private Sub dtgCourseClass_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCourseClass.SortCommand
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

        dtgCourseClass.SelectedIndex = -1
        dtgCourseClass.CurrentPageIndex = 0

        BindDataToPage()
    End Sub

    Private Sub dtgCourseClass_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCourseClass.PageIndexChanged
        dtgCourseClass.CurrentPageIndex = e.NewPageIndex
        BindDataToPage()
    End Sub

    Private Sub btnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Response.Redirect(URL_REGISTER)
    End Sub

    Private Sub dtgCourseClass_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCourseClass.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not e.Item.DataItem Is Nothing Then

                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgCourseClass.CurrentPageIndex * dtgCourseClass.PageSize)

                Dim RowValue As TrClassRegistration = CType(e.Item.DataItem, TrClassRegistration)



                Dim hlClass As HyperLink = CType(e.Item.FindControl("hlClass"), HyperLink)
                Dim hlRank As HyperLink = CType(e.Item.FindControl("hlRank"), HyperLink)

                If Not IsNothing(RowValue.TrClass) And Not IsNothing(hlClass) Then
                    Dim actionValue As String = "popUpClassInformation('" + RowValue.TrClass.ClassCode + "');"
                    hlClass.Text = RowValue.TrClass.ClassCode
                    hlClass.NavigateUrl = "javascript:" + actionValue

                    hlRank.Text = RowValue.Rank.ToString
                    If RowValue.Rank > 0 Then
                        Dim iDealer As String = RowValue.Dealer.ID.ToString
                        Dim iYear As String = RowValue.TrClass.StartDate.Year.ToString
                        Dim iCategory As String = RowValue.TrClass.TrCourse.ID.ToString
                        Dim iClass As String = RowValue.TrClass.ID.ToString
                        Dim iNoReg As String = RowValue.TrTrainee.ID.ToString

                        Dim qryRank As String = iDealer + ";" + iYear + ";" + iCategory + ";" + iClass + ";" + iNoReg
                        hlRank.NavigateUrl = "FrmCourseEvaluationList.aspx?Rank=" + qryRank
                    Else
                        hlRank.Enabled = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Function IsTraineeExist(ByVal objFacade As TrTraineeFacade, ByVal objDomain As TrTrainee) As Boolean
        Return objFacade.ValidateTrainee(objDomain) > 0
    End Function

    Private Function IsFieldValidationUpdated(ByVal objDomain As TrTrainee) As Boolean
        Dim objDomainComparer As TrTrainee = New TrTraineeFacade(User).Retrieve(objDomain.ID)
        If IsNothing(objDomainComparer) Or objDomainComparer.ID = 0 Then
            Throw New Exception(SR.DataNotFound("Siswa"))
        End If
        Return objDomain.Name <> objDomainComparer.Name _
        Or objDomain.StartWorkingDate <> objDomainComparer.StartWorkingDate _
        Or objDomain.Dealer.ID <> objDomain.Dealer.ID
    End Function

    Private Sub btnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCetak.Click

        Response.Redirect("./FrmPrintTrTrainee.aspx")
    End Sub
End Class
