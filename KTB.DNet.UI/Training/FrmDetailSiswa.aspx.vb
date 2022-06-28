Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports A = DocumentFormat.OpenXml.Drawing
Imports DW = DocumentFormat.OpenXml.Drawing.Wordprocessing
Imports PIC = DocumentFormat.OpenXml.Drawing.Pictures
Imports System.IO
Imports System.Text
Imports SpireDoc = Spire.Doc
Imports SpireDocument = Spire.Doc.Document


Public Class FrmDetailSiswa
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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

    Dim helpers As TrainingHelpers = New TrainingHelpers(Me.Page)
    Dim objTrainee As TrTrainee
    Dim sessDealer As Dealer
    Dim gridRowNo As Integer = 0
    Dim bDownloadCertificate As Boolean = False

    Private ReadOnly Property Category As String
        Get
            Try
                Return Request.QueryString("category")
            Catch
                Return String.Empty
            End Try
        End Get
    End Property
    Private ReadOnly Property AreaID As String
        Get
            Try
                Select Case Category
                    Case "sales"
                        Return "1"
                    Case "ass"
                        Return "2"
                    Case "cs"
                        Return "3"
                    Case Else
                        Return String.Empty
                End Select

            Catch
                Return String.Empty
            End Try
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        helpers.CheckPrivilegeTransaction("tr4B")
        sessDealer = helpers.GetSession("DEALER")
        lblDealerCode.Value = sessDealer.DealerCode
        objTrainee = helpers.GetSession("veTrainee")
        lblSearchJobPos.Attributes("onclick") = "ShowJobPosSelection()"
        lblPopUpDealerBranch.Attributes.Add("onClick", "ShowPPDealerBranchSelection();")

        If SecurityProvider.Authorize(Context.User, SR.TrDownloadCertificate_Privilege) Then
            bDownloadCertificate = True
        End If

        If IsNothing(objTrainee) Then
            Response.Redirect("../Login.aspx#Expired")
        End If
        If Not IsPostBack Then
            InitiatePage()
            hdnCategory.Value = Request.QueryString("category")
            FillFormFromObject(objTrainee)
            BindDataToPage()
        End If
        txtJobPosition.Attributes.Add("readonly", "readonly")
        ActivateUserPrivilege()
        SetFieldMandatory()
        If Not Me.Category.IsNullorEmpty Then
            btnRegister.NonVisible()
            If Category.Equals("cs") Or Category.Equals("sales") Then
                btnCetak.NonVisible()
                btnSimpan.NonVisible()
                trSize.NonVisible()
                helpers.ModeReadOnly()
            End If
        End If
    End Sub

    Protected Sub SetFieldMandatory()
        RequiredFieldValidator8.Enabled = IsEmailAndKtpMandatory()
        RequiredFieldValidator9.Enabled = IsEmailAndKtpMandatory()
    End Sub

    Private Function IsEmailAndKtpMandatory() As Boolean
        Dim objUser As UserInfo = CType(helpers.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser.Dealer.Title = "0" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ActivateUserPrivilege()
        btnRegister.Visible = helpers.IsEdit
        btnSimpan.Visible = helpers.IsEdit

      
    End Sub

    Private Sub InitiatePage()
        helpers.RemoveSession("dtRepeater")

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

        ViewState("CurrentSortColumn") = "TrClass.TrCourse.CourseCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        Select Case Category
            Case "sales"
                lblPageTitle.Text = "Training Sales - Detail Siswa"
            Case "ass"
                lblPageTitle.Text = "Training After Sales - Detail Siswa"
            Case "cs"
                lblPageTitle.Text = "Training Customer Satisfaction - Detail Siswa"
        End Select

        ViewState("CurrentSortColumnCert") = "ID"
        ViewState("CurrentSortDirectCert") = Sort.SortDirection.ASC

    End Sub

    Private Sub BindDataToPage()
        LoadRepeater()

        If Me.AreaID = "2" Then
            BindDgCertificate(0)
        Else
            divCertificate.Visible = False
        End If


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
        Select Case Category
            Case "sales"
                Dim dataSiswa As TrTraineeSalesmanHeader = objTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) x.JobPositionAreaID = 1)
                Me.txtJobPosition.Text = dataSiswa.JobPosition
                Me.lblStatus.Text = New EnumTrTrainee().StatusByIndex(dataSiswa.Status)
                Me.ddlStatus.Visible = False

            Case "ass"
                Me.txtJobPosition.Text = objTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) x.JobPositionAreaID = 2).JobPosition
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

            Case "cs"
                Dim dataSiswa As TrTraineeSalesmanHeader = objTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) x.JobPositionAreaID = 3)
                Me.txtJobPosition.Text = dataSiswa.JobPosition
                Me.ddlStatus.Visible = False
                Me.lblStatus.Text = New EnumTrTrainee().StatusByIndex(dataSiswa.Status)

        End Select

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
        Me.txtEducationLevel.Text = objTrainee.EducationLevel
        Me.photoSrc.Attributes("value") = String.Empty
        Dim itemShirt As ListItem = Me.ddlShirtSize.Items.FindByText(objTrainee.ShirtSize)
        Me.ddlShirtSize.SelectedIndex = Me.ddlShirtSize.Items.IndexOf(itemShirt)

        Me.photoView.ImageUrl = "../WebResources/GetPhoto.aspx?id=" + objTrainee.ID.ToString

        btnRegister.Enabled = False
        If objTrainee.Status = CStr(EnumTrTrainee.TrTraineeStatus.Active) Then
            btnRegister.Enabled = True
        End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect(String.Format("FrmDataStatusSiswa.aspx?category={0}", hdnCategory.Value))
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
                    Dim func As New TrTraineeSalesmanHeaderFacade(User)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), _
                                                         "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "TrTrainee.ID", MatchType.Exact, objTrainee.ID))
                    criterias.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "JobPositionAreaID", MatchType.Exact, 2))
                    Dim datas As TrTraineeSalesmanHeader = CType(func.Retrieve(criterias)(0), TrTraineeSalesmanHeader)
                    datas.JobPosition = objTrainee.JobPosition
                    datas.Status = CInt(objTrainee.Status)
                    func.Update(datas)

                    helpers.SetSession("veTrainee", objTrainee)
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

        'Dim ds As DataTable = helpers.GetSession("dtRepeater")
        Dim ds As List(Of TrClassRegistration) = helpers.GetSession("dtRepeater")

        If IsNothing(ds) Then
            Select Case Category.ToLower()
                Case "sales"
                    ds = objTrainee.TrClassRegistrations.Cast(Of TrClassRegistration).Where(Function(x) _
                         x.TrClass.TrCourse.JobPositionCategory.AreaID = 1 And x.Status = "1").ToList()
                Case "ass"
                    ds = objTrainee.TrClassRegistrations.Cast(Of TrClassRegistration).Where(Function(x) _
                         x.TrClass.TrCourse.JobPositionCategory.AreaID = 2 And x.Status = "1").ToList()
                Case "cs"
                    ds = objTrainee.TrClassRegistrations.Cast(Of TrClassRegistration).Where(Function(x) _
                         x.TrClass.TrCourse.JobPositionCategory.AreaID = 3 And x.Status = "1").ToList()
            End Select

            'CleanUpUngraduatedClass(ds)
            helpers.SetSession("dtRepeater", ds)
        End If

        Dim arr As New ArrayList
        If Not IsNothing(ds) Then
            Dim isAsc As Boolean = ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Dim iCmp As IComparer = New ListComparer(isAsc, ViewState("CurrentSortColumn"))
            arr.AddRange(ds)
            arr.Sort(iCmp)
        End If

        dtgCourseClass.DataSource = arr
        dtgCourseClass.DataBind()

    End Sub

    Private Sub BindDgCertificate(ByVal indexPage As Integer)

        Dim totalRow As Integer = 0
        gridRowNo = dgCertificate.PageSize * indexPage
        dgCertificate.DataSource = New TrTraineeLevelDetailFacade(User).RetrieveActiveList(CriteriaSearchCertificate(), indexPage + 1, dgCertificate.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumnCert"), String), _
            CType(ViewState("CurrentSortDirectCert"), Sort.SortDirection))
        dgCertificate.VirtualItemCount = totalRow
        dgCertificate.CurrentPageIndex = indexPage
        dgCertificate.DataBind()
    End Sub

    Private Function CriteriaSearchCertificate() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrTraineeLevelDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrTraineeLevelDetail), "TrTrainee.ID", MatchType.Exact, CInt(objTrainee.ID)))
        Return criterias
    End Function

    Private Sub CleanUpUngraduatedClass(ByVal classes As List(Of TrClassRegistration))
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

    Private Sub dtgCourseClass_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgCourseClass.ItemCommand
        If e.CommandName.Equals("unduh") Then
            Try
                Dim hdnFilePath As HiddenField = CType(e.Item.FindControl("hdnPath"), HiddenField)
                helpers.DownloadFile(hdnFilePath.Value)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf e.CommandName.Equals("downloadCertificate") Then
            Dim hdnId As HiddenField = CType(e.Item.FindControl("hdnId"), HiddenField)
            DownloadCertificatePerClass(hdnId.Value)
        End If

    End Sub

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
                Dim hdnPath As HiddenField = CType(e.Item.FindControl("hdnPath"), HiddenField)
                Dim hdnId As HiddenField = CType(e.Item.FindControl("hdnId"), HiddenField)
                Dim btnDownload As LinkButton = CType(e.Item.FindControl("btnDownload"), LinkButton)
                Dim btnDownloadCertificate As LinkButton = CType(e.Item.FindControl("btnDownloadCertificate"), LinkButton)

                btnDownloadCertificate.Visible = bDownloadCertificate

                If IsNothing(RowValue.CertificateNo) Then
                    btnDownloadCertificate.Visible = False
                ElseIf RowValue.CertificateNo = String.Empty Then
                    btnDownloadCertificate.Visible = False
                End If



                If Not IsNothing(RowValue.TrClass) And Not IsNothing(hlClass) Then
                    Dim actionValue As String = "popUpClassInformation('" + RowValue.TrClass.ClassCode + "');"
                    hlClass.Text = RowValue.TrClass.ClassCode
                    hlClass.NavigateUrl = "javascript:" + actionValue
                    hdnId.Value = RowValue.ID

                    hlRank.Text = RowValue.Rank.ToString
                    If RowValue.Rank > 0 Then
                        Dim iDealer As String = RowValue.Dealer.ID.ToString
                        Dim iYear As String = RowValue.TrClass.StartDate.Year.ToString
                        Dim iCategory As String = RowValue.TrClass.TrCourse.ID.ToString
                        Dim iClass As String = RowValue.TrClass.ID.ToString
                        Dim iNoReg As String = RowValue.TrTrainee.ID.ToString

                        Dim qryRank As String = iDealer + ";" + iYear + ";" + iCategory + ";" + iClass + ";" + iNoReg
                        hlRank.NavigateUrl = "FrmCourseEvaluationList.aspx?Rank=" + qryRank + "&area=" + AreaID
                    Else
                        hlRank.Enabled = False
                    End If
                    hdnPath.Value = RowValue.TrClass.FilePath
                    If String.IsNullorEmpty(hdnPath.Value) Then
                        btnDownload.Visible = False
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

    Private Sub dgCertificate_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgCertificate.ItemCommand
        Try
            If e.CommandName.ToLower() = "unduh" Then
                Dim id As Integer = CInt(e.Item.Cells(0).Text)
                DownloadCertificate(id)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DownloadCertificatePerClass(ByVal id As Integer)
        Try

            Dim data As TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve(id)


            Dim filePath As String = Server.MapPath("") & "\..\TemplateFile\Template_Sertifikat_Class.docx"
            Dim tempPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + "DataTemp\" + Guid.NewGuid().ToString().Substring(0, 5) + ".docx"
            Dim filebytes As Byte() = File.ReadAllBytes(filePath)


            Using Stream As MemoryStream = New MemoryStream()
                Stream.Write(filebytes, 0, filebytes.Length)
                Using doc As WordprocessingDocument = WordprocessingDocument.Open(Stream, True)
                    Dim body As DocumentFormat.OpenXml.Wordprocessing.Body = doc.MainDocumentPart.Document.Body
                    Dim paragraph As List(Of DocumentFormat.OpenXml.Wordprocessing.Paragraph) = body.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Paragraph)().ToList()

                    Dim texts As List(Of DocumentFormat.OpenXml.Wordprocessing.Text) = paragraph.SelectMany(Function(p) p.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Run)()).SelectMany(Function(r) r.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Text)()).ToList()

                    '-----buka saat certificate config sudah ada---------
                    'Dim mainPart As MainDocumentPart = doc.MainDocumentPart
                    'Dim imageTtdPath As String = "\" & (KTB.DNet.Lib.WebConfig.GetValue("SAN") + data.TrCertificateConfig.PathTTD).Replace("\\", "\")
                    'Dim imageBytes As Byte() = GetImageTtdByte(imageTtdPath)

                    'Dim listPicture As List(Of DocumentFormat.OpenXml.Wordprocessing.Drawing) = doc.MainDocumentPart.Document.Body.Descendants(Of DocumentFormat.OpenXml.Wordprocessing.Drawing).ToList()
                    'Dim picBox As DocumentFormat.OpenXml.Wordprocessing.Drawing = listPicture(1) 'INI PURE HARDCODE , KALAU TEMPLATE DIRUBAH INDEX HARUS DIUBAH JUGA

                    'Dim embed As String = Nothing

                    'If picBox IsNot Nothing Then
                    '    Dim blip As DocumentFormat.OpenXml.Drawing.Blip = picBox.Descendants(Of DocumentFormat.OpenXml.Drawing.Blip)().FirstOrDefault()
                    '    If blip IsNot Nothing Then embed = blip.Embed
                    'End If

                    'If embed IsNot Nothing Then
                    '    Dim idpp As IdPartPair = doc.MainDocumentPart.Parts.Where(Function(pa) pa.RelationshipId = embed).FirstOrDefault()

                    '    If idpp IsNot Nothing Then
                    '        Dim ip As ImagePart = CType(idpp.OpenXmlPart, ImagePart)

                    '        Using fileImageStream As FileStream = File.Open(imageTtdPath, FileMode.Open)
                    '            ip.FeedData(fileImageStream)
                    '        End Using

                    '    End If
                    'End If

                    For Each word As DocumentFormat.OpenXml.Wordprocessing.Text In texts

                        If word.Text.Contains("_") Then
                            Select Case word.Text
                                Case "nomor_sertifikat"
                                    word.Text = data.CertificateNo
                                Case "nama_trainee"
                                    word.Text = data.TrTrainee.Name
                                Case "nomor_reg"
                                    word.Text = data.TrTrainee.ID
                                Case "nama_dealer"
                                    word.Text = data.TrTrainee.Dealer.DealerName
                                Case "kota_dealer"
                                    word.Text = data.TrTrainee.Dealer.City.CityName
                                Case "$deskripsi_kategori$"
                                    word.Text = "After Sales"
                                Case "kategori_kursus"
                                    word.Text = data.TrClass.TrCourse.Category.Description
                                Case "course_1"
                                    word.Text = data.TrClass.ClassName
                                Case "tanggal_lulus"
                                    word.Text = data.TrClass.FinishDate.ToString("MMMM dd yyyy")
                                    'Case "nama_dep_head"
                                    '    word.Text = data.TrCertificateConfig.NamaTTD
                                    'Case "jabatan_dep_head"
                                    '    word.Text = data.TrCertificateConfig.JabatanTTD
                            End Select
                        End If
                    Next
                End Using
                Dim bytes As Byte() = Stream.ToArray()

                UploadDocXFile(bytes, tempPath)
                DownloadPdfFile(tempPath)
            End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DownloadCertificate(ByVal id As Integer)
        Try

            Dim data As TrTraineeLevelDetail = New TrTraineeLevelDetailFacade(User).Retrieve(id)


            Dim filePath As String = Server.MapPath("") & "\..\TemplateFile\Template_Sertifikat.docx"
            Dim tempPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + "DataTemp\" + Guid.NewGuid().ToString().Substring(0, 5) + ".docx"
            Dim filebytes As Byte() = File.ReadAllBytes(filePath)


            Using Stream As MemoryStream = New MemoryStream()
                Stream.Write(filebytes, 0, filebytes.Length)
                Using doc As WordprocessingDocument = WordprocessingDocument.Open(Stream, True)
                    Dim body As DocumentFormat.OpenXml.Wordprocessing.Body = doc.MainDocumentPart.Document.Body
                    Dim paragraph As List(Of DocumentFormat.OpenXml.Wordprocessing.Paragraph) = body.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Paragraph)().ToList()

                    Dim texts As List(Of DocumentFormat.OpenXml.Wordprocessing.Text) = paragraph.SelectMany(Function(p) p.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Run)()).SelectMany(Function(r) r.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Text)()).ToList()

                    Dim mainPart As MainDocumentPart = doc.MainDocumentPart
                    Dim imageTtdPath As String = "\" & (KTB.DNet.Lib.WebConfig.GetValue("SAN") + data.TrCertificateConfig.PathTTD).Replace("\\", "\")
                    Dim imageBytes As Byte() = GetImageTtdByte(imageTtdPath)

                    Dim listPicture As List(Of DocumentFormat.OpenXml.Wordprocessing.Drawing) = doc.MainDocumentPart.Document.Body.Descendants(Of DocumentFormat.OpenXml.Wordprocessing.Drawing).ToList()
                    Dim picBox As DocumentFormat.OpenXml.Wordprocessing.Drawing = listPicture(1) 'INI PURE HARDCODE , KALAU TEMPLATE DIRUBAH INDEX HARUS DIUBAH JUGA

                    Dim embed As String = Nothing

                    If picBox IsNot Nothing Then
                        Dim blip As DocumentFormat.OpenXml.Drawing.Blip = picBox.Descendants(Of DocumentFormat.OpenXml.Drawing.Blip)().FirstOrDefault()
                        If blip IsNot Nothing Then embed = blip.Embed
                    End If

                    If embed IsNot Nothing Then
                        Dim idpp As IdPartPair = doc.MainDocumentPart.Parts.Where(Function(pa) pa.RelationshipId = embed).FirstOrDefault()

                        If idpp IsNot Nothing Then
                            Dim ip As ImagePart = CType(idpp.OpenXmlPart, ImagePart)

                            Using fileImageStream As FileStream = File.Open(imageTtdPath, FileMode.Open)
                                ip.FeedData(fileImageStream)
                            End Using

                        End If
                    End If

                    For Each word As DocumentFormat.OpenXml.Wordprocessing.Text In texts

                        If word.Text.Contains("_") Then
                            Select Case word.Text
                                Case "nomor_sertifikat"
                                    word.Text = data.CertificateNumber
                                Case "nama_trainee"
                                    word.Text = data.NamaSiswa
                                Case "nomor_reg"
                                    word.Text = data.TrTrainee.ID
                                Case "nama_dealer"
                                    word.Text = data.TrTrainee.Dealer.DealerName
                                Case "kota_dealer"
                                    word.Text = data.TrTrainee.Dealer.City.CityName
                                Case "$deskripsi_kategori$"
                                    word.Text = "After Sales"
                                Case "kategori_kursus"
                                    word.Text = data.TrCourseCategory.Description
                                Case "course_1"
                                    Try
                                        Dim classregis As TrClassRegistration = DirectCast(data.ListOfTrClassRegistration(0), TrClassRegistration)
                                        word.Text = "- " & classregis.TrClass.TrCourse.CourseCode & " - " & classregis.TrClass.TrCourse.CourseName
                                    Catch ex As Exception
                                        word.Text = ""
                                    End Try
                                Case "course_2"
                                    Try
                                        Dim classregis As TrClassRegistration = DirectCast(data.ListOfTrClassRegistration(1), TrClassRegistration)
                                        word.Text = "- " & classregis.TrClass.TrCourse.CourseCode & " - " & classregis.TrClass.TrCourse.CourseName
                                    Catch ex As Exception
                                        word.Text = ""
                                    End Try
                                Case "course_3"
                                    Try
                                        Dim classregis As TrClassRegistration = DirectCast(data.ListOfTrClassRegistration(2), TrClassRegistration)
                                        word.Text = "- " & classregis.TrClass.TrCourse.CourseCode & " - " & classregis.TrClass.TrCourse.CourseName
                                    Catch ex As Exception
                                        word.Text = ""
                                    End Try
                                Case "course_4"
                                    Try
                                        Dim classregis As TrClassRegistration = DirectCast(data.ListOfTrClassRegistration(3), TrClassRegistration)
                                        word.Text = "- " & classregis.TrClass.TrCourse.CourseCode & " - " & classregis.TrClass.TrCourse.CourseName
                                    Catch ex As Exception
                                        word.Text = ""
                                    End Try
                                Case "course_5"
                                    Try
                                        Dim classregis As TrClassRegistration = DirectCast(data.ListOfTrClassRegistration(4), TrClassRegistration)
                                        word.Text = "- " & classregis.TrClass.TrCourse.CourseCode & " - " & classregis.TrClass.TrCourse.CourseName
                                    Catch ex As Exception
                                        word.Text = ""
                                    End Try
                                Case "level_description"
                                    word.Text = data.TrTraineeLevel.Description.ToUpper()
                                Case "tanggal_lulus"
                                    word.Text = data.TanggalLulus.ToString("MMMM dd yyyy")
                                Case "nama_dep_head"
                                    word.Text = data.TrCertificateConfig.NamaTTD
                                Case "jabatan_dep_head"
                                    word.Text = data.TrCertificateConfig.JabatanTTD
                            End Select
                        End If
                    Next
                End Using
                Dim bytes As Byte() = Stream.ToArray()

                UploadDocXFile(bytes, tempPath)
                DownloadPdfFile(tempPath)
            End Using

        Catch ex As Exception

        End Try

    End Sub

    Private Function GetImageTtdByte(ByVal path As String) As Byte()
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim success As Boolean = False
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim bit As Byte()

        Try
            success = imp.Start()
            If imp.Start() Then
                bit = File.ReadAllBytes(path)
                imp.StopImpersonate()
                imp = Nothing
            End If
            Return bit
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Sub UploadDocXFile(ByVal bytes As Byte(), ByVal tempPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim success As Boolean = False
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            success = imp.Start()


            If imp.Start() Then

                If Not IO.Directory.Exists(tempPath) Then
                    IO.Directory.CreateDirectory(Path.GetDirectoryName(tempPath))
                End If

                If IO.File.Exists(tempPath) Then
                    IO.File.Delete(Path.GetDirectoryName(tempPath))
                End If


                Try
                    Dim wFile As System.IO.FileStream
                    wFile = New FileStream(tempPath, FileMode.Append)
                    wFile.Write(bytes, 0, bytes.Length)
                    wFile.Close()
                Catch ex As IOException
                    MsgBox(ex.ToString)
                End Try

                imp.StopImpersonate()
                imp = Nothing
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub DownloadPdfFile(ByVal tempPath As String)

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim success As Boolean = False
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            success = imp.Start()
            If imp.Start() Then

                Dim pdfDoc As New SpireDoc.Document
                pdfDoc.LoadFromFile(tempPath)
                pdfDoc.SaveToFile("Sertifikat.pdf", Spire.Doc.FileFormat.PDF, Response, Spire.Doc.HttpContentType.Attachment)
                imp.StopImpersonate()
                imp = Nothing
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub dgCertificate_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgCertificate.ItemDataBound
        Try
            If Not e.Item.DataItem Is Nothing Then
                Dim data As TrTraineeLevelDetail = CType(e.Item.DataItem, TrTraineeLevelDetail)

                gridRowNo += 1

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lblCourseCategory As Label = CType(e.Item.FindControl("lblCourseCategory"), Label)
                Dim lblTraineeLevel As Label = CType(e.Item.FindControl("lblTraineeLevel"), Label)
                Dim lblTanggalLulus As Label = CType(e.Item.FindControl("lblTanggalLulus"), Label)
                Dim btnDownload As LinkButton = CType(e.Item.FindControl("btnDownload"), LinkButton)

                lblNo.Text = gridRowNo
                lblCourseCategory.Text = data.TrCourseCategory.Code & " - " & data.TrCourseCategory.Description
                lblTraineeLevel.Text = data.TrTraineeLevel.Description
                lblTanggalLulus.Text = data.TanggalLulus
                btnDownload.Visible = bDownloadCertificate

                If IsNothing(data.CertificateNumber) Then
                    btnDownload.Visible = False
                ElseIf data.CertificateNumber = String.Empty Then
                    btnDownload.Visible = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
