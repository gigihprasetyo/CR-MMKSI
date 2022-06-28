Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
Imports OfficeOpenXml
Imports System.Threading.Tasks
Imports System.IO

Public Class FrmDataSiswa
    Inherits System.Web.UI.Page
    Dim sHTrainee As SessionHelper = New SessionHelper
    Dim helpers As New TrainingHelpers(Me.Page)

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private bPrivilegeChangeTrainee As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Private ReadOnly Property TrTraineeID As String
        Get
            Return Request.QueryString("trtraineeid")
        End Get
    End Property

    Private ReadOnly Property Category As String
        Get
            Try
                Return Request.QueryString("area")
            Catch
                Return String.Empty
            End Try
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            lblPopUpDealer.Attributes.Add("onClick", "ShowPPDealerSelection();")
            lblPopupDealerBranch.Attributes.Add("onClick", "ShowPPDealerBranchSelection();")
            lblSearchJobPos.Attributes("onclick") = "ShowJobPosSelection()"
            InitiatePage()

            SetControlPrivilege()
            If String.IsNullorEmpty(TrTraineeID) Then
                SettingPage()
                CreateCriteria()
                BindDataGrid(0)
            Else
                trSearch.Visible = False
                trGrid.Visible = False
                ViewState.Add("vsProcess", "Edit")
                ViewTrainee(CInt(TrTraineeID), True)
                Me.txtName.ReadOnly = False
                'Me.txtDealerCode.ReadOnly = False
                Me.ICStartWork.Enabled = True
                Me.txtEducationLevel.ReadOnly = False
                Me.photoSrc.Disabled = False
                Me.ddlShirtSize.Enabled = True
                Me.ddlStatus.Enabled = True
                Me.lblPopUpDealer.Enabled = True
                cbDeletePhoto.Visible = True
                cbDeletePhoto.Checked = False
            End If

            SetFieldMandatory()
        End If
        txtJobPosition.Attributes.Add("readonly", "readonly")
        AssignControlAttribute()
    End Sub

    Protected Sub SetFieldMandatory()
        RequiredFieldValidator8.Enabled = IsEmailAndKtpMandatory()
        RequiredFieldValidator9.Enabled = IsEmailAndKtpMandatory()
    End Sub

    Private Sub SettingPage()
        ddlStatus.ClearSelection()
        ddlStatus.Enabled = False
        If Me.IsKTB Then
            ddlStatus.Enabled = True
        End If
        ddlStatus.SelectedValue = "0"
    End Sub

    Private Sub AssignControlAttribute()
        btnSimpan.Attributes.Add("onclick", New CommonFunction().PreventDoubleClick(btnSimpan))
    End Sub

    Private Sub ActivateUserPrivilege()
        Select Case AreaId
            Case "1"
                lblPageTitle.Text = "Training Sales - Siswa"
            Case "2"
                lblPageTitle.Text = "Training After Sales - Siswa"
            Case "3"
                lblPageTitle.Text = "Training Customer Satisfaction - Siswa"
        End Select
        helpers = New TrainingHelpers(Me.Page, lblPageTitle.Text)
        helpers.CheckPrivilegeTransaction("tr3" + AreaId.PrivilegeTrainingType)
        bPrivilegeChangeTrainee = helpers.IsEdit
    End Sub
    Private Sub SetControlPrivilege()
        btnSimpan.Visible = bPrivilegeChangeTrainee
        btnBatal.Visible = bPrivilegeChangeTrainee

    End Sub
    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "Name"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        Dim objEnumTrTrainee = New EnumTrTrainee
        Dim TraineeStatus As ArrayList = objEnumTrTrainee.RetrieveStatus()
        Dim lItem As ListItem
        For Each ts As EnumTrTraineeData In TraineeStatus
            lItem = New ListItem(ts.NameTitle, ts.ValTitle.ToString)
            ddlStatus.Items.Add(lItem)
            ddlSearchStatus.Items.Add(lItem)
        Next
        ddlStatus.SelectedValue = "0"

        lItem = New ListItem("Silahkan pilih", "")
        ddlSearchStatus.Items.Insert(0, lItem)
        ddlSearchStatus.SelectedValue = "1"

        Dim ShirtSize As ArrayList = objEnumTrTrainee.RetrieveSize()
        For Each es As EnumShirtData In ShirtSize
            lItem = New ListItem(es.NameTitle, es.ValTitle.ToString)
            ddlShirtSize.Items.Add(lItem)
        Next

        Dim li As ListItem = New ListItem(String.Empty, String.Empty)

        If AreaId.Equals("1") Or AreaId.Equals("3") Then
            trDetail.NonVisible()
            div1.Style.Add("HEIGHT", "360px")
        End If

        BindDdlGender()
        BindDdlRegion()

        If Me.IsDealer Then
            txtDealerCode.Text = Me.GetDealer.DealerCode
            Me.hdnDealerCode.Value = Me.GetDealer.DealerCode
            txtDealerCode.Disabled()
            lblPopUpDealer.Visible = False
            txtSearchDealerCode.Text = Me.GetDealer.DealerCode
            txtSearchDealerCode.Disabled()
            ddlRegion.Visible = False
            'ddlRegion.SelectedValue = Me.GetDealer.Area2.ID.ToString()
            'ddlRegion.Enabled = False
        End If

    End Sub

    Private Sub CreateCriteria()
        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If AreaId.Equals("2") Then
            If txtSearchDealerCode.Text <> String.Empty Then
                critCol.opAnd(New Criteria(GetType(TrTrainee), "Dealer.DealerCode", MatchType.Exact, txtSearchDealerCode.Text))
            End If
            If txtDealerBranchCode.Text.Trim <> String.Empty Then
                critCol.opAnd(New Criteria(GetType(TrTrainee), "DealerBranch.DealerBranchCode", MatchType.Exact, txtSearchDealerBranchCode.Text))
            End If
            If txtSearchNoReg.Text <> String.Empty Then
                critCol.opAnd(New Criteria(GetType(TrTrainee), "ID", MatchType.Exact, txtSearchNoReg.Text))
            End If
            If txtJobPosition.Text.Length <> 0 Then
                critCol.opAnd(New Criteria(GetType(TrTrainee), "JobPosition", MatchType.[Partial], txtJobPosition.Text.Trim()))
            End If
            If ddlSearchStatus.SelectedIndex <> 0 Then
                critCol.opAnd(New Criteria(GetType(TrTrainee), "Status", MatchType.Exact, ddlSearchStatus.SelectedValue.ToString))
            End If
            If Not ddlRegion.SelectedValue = "-1" Then
                critCol.opAnd(New Criteria(GetType(TrTrainee), "Dealer.Area2.ID", MatchType.Exact, CInt(ddlRegion.SelectedValue)))
            End If

            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(JobPosition), "Category", MatchType.InSet, "(0,2,3)"))
            Dim arrJobposition As ArrayList = New JobPositionFacade(Me.User).Retrieve(crits)
            If arrJobposition.Count > 0 Then
                Dim strJob As String = String.Empty
                For Each iJob As JobPosition In arrJobposition
                    strJob += "'" + iJob.Code.ToString() + "', "
                Next
                If strJob.Length > 2 Then
                    strJob = strJob.Remove(strJob.Length - 2, 2)
                End If

                critCol.opAnd(New Criteria(GetType(TrTrainee), "JobPosition", MatchType.InSet, String.Format("({0})", strJob)))
            End If

        Else
            If txtSearchDealerCode.Text <> String.Empty Then
                critCol.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.SalesmanHeader.Dealer.DealerCode", MatchType.Exact, txtSearchDealerCode.Text))
            End If
            If txtDealerBranchCode.Text.Trim <> String.Empty Then
                critCol.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.SalesmanHeader.DealerBranch.DealerBranchCode", MatchType.Exact, txtSearchDealerBranchCode.Text))
            End If
            If Not ddlRegion.SelectedValue = "-1" Then
                critCol.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.SalesmanHeader.Dealer.Area2.ID", MatchType.Exact, CInt(ddlRegion.SelectedValue)))
            End If
            If txtSearchNoReg.Text <> String.Empty Then
                critCol.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.SalesmanHeader.SalesmanCode", MatchType.Exact, txtSearchNoReg.Text))
            End If
            If txtJobPosition.Text.Length <> 0 Then
                critCol.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.JobPosition", MatchType.[Partial], txtJobPosition.Text.Trim()))
            End If
            If ddlSearchStatus.SelectedIndex <> 0 Then
                critCol.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.Status", MatchType.Exact, ddlSearchStatus.SelectedValue.ToString))
            End If
            critCol.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.JobPositionAreaID", MatchType.Exact, AreaId))
        End If

        If txtSearchTraineeName.Text <> String.Empty Then
            critCol.opAnd(New Criteria(GetType(TrTrainee), "Name", MatchType.[Partial], txtSearchTraineeName.Text))
        End If



        helpers.SetSession("criteria", critCol)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            Try
                Dim critCol As CriteriaComposite = helpers.GetSession("criteria")

                dtgTrainee.DataSource = New TrTraineeFacade(User).RetrieveActiveList(critCol, _
                    indexPage + 1, dtgTrainee.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
                dtgTrainee.VirtualItemCount = totalRow
                dtgTrainee.DataBind()



                If totalRow > 0 Then
                    btnDownload.Enabled = True
                Else
                    btnDownload.Enabled = False
                End If

            Catch
                MessageBox.Show("Harap periksa kategori pencarian anda")
            End Try
        End If
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer, ByVal defaultStatus As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            Try
                Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If txtSearchDealerCode.Text <> String.Empty Then
                    critCol.opAnd(New Criteria(GetType(TrTrainee), "Dealer.DealerCode", MatchType.Exact, txtSearchDealerCode.Text))
                End If
                'If txtSearchNoReg.Text <> String.Empty Then

                '    critCol.opAnd(New Criteria(GetType(TrTrainee), "ID", MatchType.Exact, CInt(txtSearchNoReg.Text)))
                'End If
                If txtSearchNoReg.IsNotEmpty Then
                    critCol.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.SalesmanHeader.SalesmanCode", MatchType.Exact, txtSearchNoReg.Text))
                End If

                If txtSearchTraineeName.Text <> String.Empty Then
                    critCol.opAnd(New Criteria(GetType(TrTrainee), "Name", MatchType.Exact, txtSearchTraineeName.Text))
                End If
                critCol.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.JobPositionAreaID", MatchType.Exact, AreaId))
                critCol.opAnd(New Criteria(GetType(TrTrainee), "Status", MatchType.Exact, EnumTrTrainee.TrTraineeStatus.Active))


                dtgTrainee.DataSource = New TrTraineeFacade(User).RetrieveActiveList(critCol, _
                    indexPage + 1, dtgTrainee.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
                dtgTrainee.VirtualItemCount = totalRow
                dtgTrainee.DataBind()
            Catch
                MessageBox.Show("Harap periksa kategori pencarian anda")
            End Try
        End If
    End Sub

    Private Sub BindDdlGender()
        CommonFunction.BindFromEnum("SalesmanGender", ddlGender, User, False, "NameStatus", "ValStatus")
        ddlGender.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
        ddlGender.SelectedIndex = 0
    End Sub

    Private Sub BindDdlRegion()
        ddlRegion.Items.Clear()
        Dim arlRegion As ArrayList = New Area2Facade(User).RetrieveActiveList()
        Dim objRegion As New Area2
        ddlRegion.Items.Add(New ListItem("Semua Region", "-1"))
        For i As Integer = 0 To arlRegion.Count - 1
            objRegion = CType(arlRegion(i), Area2)
            ddlRegion.Items.Add(New ListItem(objRegion.AreaCode, objRegion.ID))
        Next i
    End Sub

    Private Sub ClearData()
        sHTrainee.RemoveSession("objTrainee")

        Me.txtName.ReadOnly = False
        'Me.txtDealerCode.ReadOnly = False
        Me.ICStartWork.Enabled = True
        'Me.txtJobPosition.ReadOnly = True 'dna False
        Me.txtEducationLevel.ReadOnly = False
        Me.photoSrc.Disabled = False
        Me.ddlShirtSize.Enabled = True
        Me.ddlStatus.Enabled = False
        If Me.IsKTB Then
            Me.ddlStatus.Enabled = True
        End If

        Me.ddlStatus.ClearSelection()
        Me.ddlStatus.SelectedValue = "0"
        Me.lblPopUpDealer.Enabled = True

        Me.txtName.Text = String.Empty
        Me.txtDealerCode.Text = String.Empty
        Me.txtDealerCode.Enabled = True
        Me.ICStartWork.Value = Today
        Me.txtJobPosition.Text = String.Empty
        Me.txtEducationLevel.Text = String.Empty
        Me.photoSrc.Attributes("value") = String.Empty
        Me.ddlShirtSize.SelectedIndex = 0
        Me.ddlStatus.SelectedIndex = 0
        Me.photoView.ImageUrl = "../WebResources/GetPhoto.aspx"
        Me.txtKTP.Text = ""
        Me.txtDealerBranchCode.Text = ""
        Me.txtEmail.Text = ""
        btnSimpan.Enabled = True
        dtgTrainee.SelectedIndex = -1
        ViewState.Add("vsProcess", "Insert")
        cbDeletePhoto.Visible = False
    End Sub

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

        If Not IsUnhack() Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return
        End If

        Try
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                InsertTrainee()
            ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
                UpdateTrainee()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        dtgTrainee.CurrentPageIndex = 0
        BindDataGrid(dtgTrainee.CurrentPageIndex)
    End Sub

    Private Function IsEmailAndKtpMandatory() As Boolean
        Dim objUser As UserInfo = CType(sHTrainee.GetSession("LOGINUSERINFO"), UserInfo)
        If objUser.Dealer.Title = "0" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub InsertTrainee()
        Dim objTrainee As TrTrainee = New TrTrainee
        Dim objTraineeFacade As TrTraineeFacade = New TrTraineeFacade(User)
        Dim objTrSalesmanFacade As TrTraineeSalesmanHeaderFacade = New TrTraineeSalesmanHeaderFacade(User)
        Dim nResult = -1
        Dim imageFile As Byte()

        Try
            imageFile = UploadFile()

            If Not IsDealerExist(Me.txtDealerCode.Text) Then
                Throw New DataException(SR.DataNotFound("Kode Dealer"))
            End If

            If Not ValidateDealerBranchIfExist(txtDealerBranchCode.Text) Then
                Throw New DataException(SR.DataNotFound("Kode Cabang Dealer"))
            End If

            FillingObjectFromForm(objTrainee)
            objTrainee.Photo = imageFile

            Dim errMsg As String
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(TrTrainee), "NoKTP", MatchType.Exact, objTrainee.NoKTP))

            Dim arrTrtrainee As ArrayList = objTraineeFacade.Retrieve(crit)
            Dim objTr As New TrTrainee
            Dim isUpdate As Boolean = False
            If arrTrtrainee.Count > 0 Then
                objTr = arrTrtrainee(0)

                If objTr.ListTrTraineeSalesmanHeader.Count > 0 Then
                    Dim objTth As TrTraineeSalesmanHeader = objTr.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) x.JobPositionAreaID = Category.ToLower())
                    If Not objTth Is Nothing Then
                        If (objTth.Status = CType(EnumTrTrainee.TrTraineeStatus.Active, String) Or objTth.Status = CType(EnumTrTrainee.TrTraineeStatus.Unapproved, String)) And Category.ToLower() = objTth.JobPositionAreaID Then
                            errMsg = String.Format("KTP dengan no {0} sudah terdaftar di dealer {1} dengan nomor registrasi {2}. Harap non aktifkan terlebih dahulu.", objTrainee.NoKTP, _
                                     objTr.Dealer.DealerCode, objTr.ID)
                            MessageBox.Show(errMsg)
                            Return
                        End If
                    End If
                End If
                isUpdate = True
                objTrainee.ID = objTr.ID
                nResult = objTraineeFacade.Update(objTrainee)
            Else
                nResult = objTraineeFacade.Insert(objTrainee)
            End If

            'nResult = objTraineeFacade.Insert(objTrainee)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else

                objTrainee.ID = nResult
                Dim dtTrSlsmn As TrTraineeSalesmanHeader = New TrTraineeSalesmanHeader()
                dtTrSlsmn.TrTrainee = objTrainee
                Select Case Category.ToLower()
                    Case "1"
                        dtTrSlsmn.JobPositionAreaID = 1
                    Case "2"
                        dtTrSlsmn.JobPositionAreaID = 2
                    Case "3"
                        dtTrSlsmn.JobPositionAreaID = 3
                End Select
                dtTrSlsmn.JobPosition = objTrainee.JobPosition
                dtTrSlsmn.SalesmanHeader = Nothing
                dtTrSlsmn.Status = 0
                objTrSalesmanFacade.Insert(dtTrSlsmn)


                MessageBox.Show(SR.SaveSuccess)
                ClearData()
            End If
        Catch ex As Exception
            Throw ex
        End Try

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
        Or objDomain.Dealer.ID <> objDomainComparer.Dealer.ID

        'If objDomain.Name = objDomainComparer.Name _
        '    Or objDomain.StartWorkingDate = objDomainComparer.StartWorkingDate _
        '    Or objDomain.Dealer.ID = objDomain.Dealer.ID Then
        '    Return True
        'Else
        '    Return False
        'End If
    End Function

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
        Dim objTrainee As TrTrainee = CType(sHTrainee.GetSession("objTrainee"), TrTrainee)

        Dim arlRegClassColl As New ArrayList

        Try

            'If objTrainee.Status.Trim().ToUpper() <> ddlStatus.SelectedValue.Trim().ToUpper() Then
            '    If ddlStatus.SelectedValue = CType(EnumTrTrainee.TrTraineeStatus.Deactive, String) Or _
            '        ddlStatus.SelectedValue = CType(EnumTrTrainee.TrTraineeStatus.Unapproved, String) Then
            arlRegClassColl = GetRegOfTrainee(objTrainee)
            '    End If
            'End If

            If arlRegClassColl.Count = 0 Then
                SaveUpdate(objTrainee)
            Else
                Dim arlClass As New ArrayList
                For Each tcr As TrClassRegistration In arlRegClassColl
                    If tcr.TrClass.FinishDate >= New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day) Then
                        arlClass.Add(tcr)
                    End If
                Next
                If arlClass.Count > 0 Then
                    'MessageBox.Show("Data siswa - Status tidak bisa dirubah, karena siswa sudah terdaftar di kelas : \n" & _
                    '                    BuildStringOfRegClassColl(arlRegClassColl) & "")
                    Dim newDealer As Dealer = New DealerFacade(User).Retrieve(Me.txtDealerCode.Text)
                    If (objTrainee.Dealer.ID <> newDealer.ID) Then
                        MessageBox.Show("Data siswa - Status tidak bisa dirubah, karena siswa sudah terdaftar di kelas : \n" & _
                                                                BuildStringOfRegClassColl(arlClass) & "")
                    Else
                        SaveUpdate(objTrainee)
                    End If
                Else
                    SaveUpdate(objTrainee)
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Async Function SendEmail(ByVal objTrtraineeBefore As TrTrainee, ByVal objTrtraineeAfter As TrTrainee, ByVal objUser As UserInfo) As Task ' bStatus = New (true) or Update(false) Employee
        Dim appConfigFacade As New AppConfigFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFromDealerChange")
        Dim emailTo As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_TO_DEALER_CHANGE).Value

        Try
            Await Task.Run(Sub()
                               Dim valueEmail As String
                               Dim subject As String = "[MMKSI-DNet] Call Center - Update Employee"
                               Dim Dir As String = Server.MapPath("") & "\..\DataFile\EmailTemplate\Perubahan_Dealer_Employee.htm"
                               Try
                                   Dim sContents() As String = _
                                { _
                                   objTrtraineeBefore.Dealer.DealerCode, _
                                   objTrtraineeBefore.Dealer.SearchTerm1, _
                                   objTrtraineeAfter.Dealer.DealerCode, _
                                   objTrtraineeAfter.Dealer.SearchTerm1, _
                                   objTrtraineeAfter.ID, _
                                   objTrtraineeAfter.Name, _
                                   objTrtraineeAfter.RefJobPosition.Description, _
                                   objUser.Dealer.DealerCode, _
                                   objUser.UserName _
                               }

                                   Me.SendEmailFromTemplate(Dir, emailTo, String.Empty, subject, sContents)
                               Catch
                               End Try

                           End Sub)
        Catch ex As Exception
            Throw New Exception("Failed Sending Email : " & ex.Message)
        End Try
    End Function

    Private Sub SendEmailFromTemplate(ByVal EmailFile As String, ByVal sTo As String, ByVal sCC As String, ByVal sSubject As String, ByVal sMessage() As String)
        Dim appConfigFacade As New AppConfigFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailAdmin As String = String.Empty
        Dim emailFrom As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_FROM_GENERAL).Value
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(EmailFile)
        Dim szEmailFormat As String = sr.ReadToEnd()
        sr.Close()
        Dim szEmailContent As String = String.Format(szEmailFormat, sMessage)
        If Not IsNothing(sCC) AndAlso sCC.Trim() <> "" AndAlso sCC.EndsWith(";") = False AndAlso Not IsNothing(emailAdmin) AndAlso emailAdmin <> "" Then
            emailAdmin = ";" & emailAdmin
        End If

        ObjEmail.sendMail(sTo, sCC, emailAdmin, emailFrom, sSubject, Mail.MailFormat.Html, szEmailContent)
    End Sub

    Private Sub SaveUpdate(ByVal objTrainee As TrTrainee)
        Dim objTraineeFacade As TrTraineeFacade = New TrTraineeFacade(User)
        Dim nResult = -1
        Dim imageFile As Byte()

        imageFile = Nothing
        If Not cbDeletePhoto.Checked Then
            imageFile = UploadFile()
        End If

        If Not IsDealerExist(Me.txtDealerCode.Text) Then
            Throw New DataException(SR.DataNotFound("Kode Dealer"))
        End If

        If Not ValidateDealerBranchIfExist(txtDealerBranchCode.Text) Then
            Throw New DataException(SR.DataNotFound("Kode Cabang Dealer"))
        End If

        Try
            FillingObjectFromForm(objTrainee)
            If photoSrc.Value <> String.Empty Or cbDeletePhoto.Checked Then
                objTrainee.Photo = imageFile
            End If

            If IsFieldValidationUpdated(objTrainee) And IsTraineeExist(objTraineeFacade, objTrainee) Then
                Throw New Exception(SR.DataIsExist("Siswa"))
            End If

            Dim isSendEmail As Boolean = False
            Dim objTrExist As TrTrainee = objTraineeFacade.Retrieve(objTrainee.ID)
            If objTrExist.Dealer.DealerCode <> objTrainee.Dealer.DealerCode Then
                Dim funcDS As New DealerSystemsFacade(Me.User)
                Dim dealerBefore_DS As DealerSystems = funcDS.RetrieveByDealerCode(objTrExist.Dealer.DealerCode)
                Dim dealerAfter_DS As DealerSystems = funcDS.RetrieveByDealerCode(objTrainee.Dealer.DealerCode)

                If dealerBefore_DS.SystemID <> 1 Or dealerAfter_DS.SystemID <> 1 Then
                    isSendEmail = True
                End If
            End If

            nResult = objTraineeFacade.Update(objTrainee, Category.ToLower())
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                Dim sessHelper As New SessionHelper
                Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
                If isSendEmail Then
                    SendEmail(objTrExist, objTrainee, objUser)
                End If
                MessageBox.Show(SR.SaveSuccess)
                ClearData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub dtgTrainee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTrainee.ItemDataBound
        If Not AreaId.Equals("2") Then
            e.Item.Cells(10).Visible = False
        End If
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", KTB.DNet.Utility.CommonFunction.PreventDoubleEvent("return confirm('Yakin Data ini akan dihapus?');"))
        End If

        If Not e.Item.FindControl("btnLihat") Is Nothing Then
            CType(e.Item.FindControl("btnLihat"), LinkButton).Attributes.Add("OnClick", KTB.DNet.Utility.CommonFunction.PreventDoubleEvent(String.Empty))
        End If

        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            CType(e.Item.FindControl("btnUbah"), LinkButton).Attributes.Add("OnClick", KTB.DNet.Utility.CommonFunction.PreventDoubleEvent(String.Empty))
        End If

        If Not e.Item.FindControl("btnAktif") Is Nothing Then
            CType(e.Item.FindControl("btnAktif"), LinkButton).Attributes.Add("OnClick", KTB.DNet.Utility.CommonFunction.PreventDoubleEvent(String.Empty))
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgTrainee.CurrentPageIndex * dtgTrainee.PageSize)
        End If

        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As TrTrainee = CType(e.Item.DataItem, TrTrainee)

            Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            Dim lblGender As Label = CType(e.Item.FindControl("lblGender"), Label)
            Dim lblPosition As Label = CType(e.Item.FindControl("lblPosition"), Label)
            Dim lblSalesmanCode As Label = e.FindLabel("lblSalesmanCode")
            Dim lblTrID As Label = e.FindLabel("lblTrID")
            Dim btnAktif As LinkButton = CType(e.Item.FindControl("btnAktif"), LinkButton)
            If RowValue.Status = CStr(EnumTrTrainee.TrTraineeStatus.Active) Then
                btnAktif.Text = "<img src='../images/in-aktif.gif' border='0' alt='Non-Aktif'>"
            Else
                btnAktif.Text = "<img src='../images/aktif.gif' border='0' alt='Aktif'>"
            End If
            Dim datas As New List(Of TrTraineeSalesmanHeader)
            Dim data As New TrTraineeSalesmanHeader
            If RowValue.ListTrTraineeSalesmanHeader.IsItems Then
                If Not AreaId.Equals("2") Then

                    datas = RowValue.ListTrTraineeSalesmanHeader
                    data = datas.FirstOrDefault(Function(x) x.JobPositionAreaID = CInt(AreaId))

                    If Not IsNothing(data.SalesmanHeader.Dealer) Then
                        lblKodeDealer.Text = data.SalesmanHeader.Dealer.DealerCode
                        If Not IsNothing(RowValue.Gender) Or RowValue.Gender > 0 Then
                            lblGender.Text = EnumGender.GetStringGender(CType(RowValue.Gender, Integer))
                        Else
                            lblGender.Text = String.Empty
                        End If
                    End If

                    Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

                    If Not IsNothing(RowValue.Status) Then
                        Try
                            lblStatus.Text = New EnumTrTrainee().StatusByIndex(Integer.Parse(data.Status))
                        Catch
                            lblStatus.Text = String.Empty
                        End Try
                    End If

                    lblSalesmanCode.Text = data.SalesmanHeader.SalesmanCode
                    lblPosition.Text = data.RefJobPosition.Description
                    lblTrID.NonVisible()
                Else
                    If Not IsNothing(RowValue.Dealer) Then
                        lblKodeDealer.Text = RowValue.Dealer.DealerCode
                        If Not IsNothing(RowValue.Gender) Or RowValue.Gender > 0 Then
                            lblGender.Text = EnumGender.GetStringGender(CType(RowValue.Gender, Integer))
                        Else
                            lblGender.Text = String.Empty
                        End If
                    End If

                    Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

                    If Not IsNothing(RowValue.Status) Then
                        Try
                            lblStatus.Text = New EnumTrTrainee().StatusByIndex(Integer.Parse(RowValue.Status))
                        Catch
                            lblStatus.Text = String.Empty
                        End Try
                    End If
                    If Not e.Item.FindControl("btnAktif") Is Nothing Then
                        CType(e.Item.FindControl("btnAktif"), LinkButton).Visible = False
                        If RowValue.Status = CStr(EnumTrTrainee.TrTraineeStatus.Unapproved) Or RowValue.Status = CStr(EnumTrTrainee.TrTraineeStatus.Deactive) Then
                            If IsKTB() And (RowValue.RefJobPosition.Category = 2 Or RowValue.RefJobPosition.Category = 3) Then
                                CType(e.Item.FindControl("btnAktif"), LinkButton).Visible = helpers.IsEdit
                            End If
                        End If
                    End If

                    lblTrID.Text = RowValue.ID.ToString
                    lblPosition.Text = RowValue.JobPosition
                    lblSalesmanCode.NonVisible()
                End If

            Else
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                If Not IsNothing(RowValue.Status) Then
                    Try
                        lblStatus.Text = New EnumTrTrainee().StatusByIndex(Integer.Parse(RowValue.Status))
                    Catch
                        lblStatus.Text = String.Empty
                    End Try
                End If

                If Not IsNothing(RowValue.Dealer) Then
                    lblKodeDealer.Text = RowValue.Dealer.DealerCode
                    If Not IsNothing(RowValue.Gender) Or RowValue.Gender > 0 Then
                        lblGender.Text = EnumGender.GetStringGender(CType(RowValue.Gender, Integer))
                    Else
                        lblGender.Text = String.Empty
                    End If
                End If

                If Not e.Item.FindControl("btnAktif") Is Nothing Then
                    CType(e.Item.FindControl("btnAktif"), LinkButton).Visible = False
                    If RowValue.Status = CStr(EnumTrTrainee.TrTraineeStatus.Unapproved) Or RowValue.Status = CStr(EnumTrTrainee.TrTraineeStatus.Deactive) Then
                        If IsKTB() And (RowValue.RefJobPosition.Category = 2 Or RowValue.RefJobPosition.Category = 3) Then
                            CType(e.Item.FindControl("btnAktif"), LinkButton).Visible = helpers.IsEdit
                        End If
                    End If
                End If
                lblTrID.Text = RowValue.ID.ToString
                lblPosition.Text = RowValue.JobPosition
                lblSalesmanCode.NonVisible()
            End If

            If Not e.Item.FindControl("btnUbah") Is Nothing Then
                If AreaId.Equals("2") Then
                    CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = bPrivilegeChangeTrainee
                End If
            End If
        End If



        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = False
            If IsKTB() Then
                CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = bPrivilegeChangeTrainee
            End If
        End If

        If AreaId.Equals("1") Or AreaId.Equals("3") Then
            If Not e.Item.FindControl("btnUbah") Is Nothing Then
                CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = False
            End If

            If Not e.Item.FindControl("btnHapus") Is Nothing Then
                CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = False
            End If

            If Not e.Item.FindControl("btnAktif") Is Nothing Then
                CType(e.Item.FindControl("btnAktif"), LinkButton).Visible = False
            End If
        End If

    End Sub

    Private Sub dtgTrainee_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgTrainee.ItemCommand
        If (e.CommandName = "View") Then
            Select Case AreaId
                Case "1"
                    Dim lblSalesmanCode As Label = e.Item.FindControl("lblSalesmanCode")
                    Dim dataSales As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(lblSalesmanCode.Text)
                    Response.Redirect("../Salesman/FrmSalesmanHeader.aspx?ID=" + dataSales.ID.ToString() + "&Mode=unit&area=" + AreaId)
                Case "2"
                    ViewState.Add("vsProcess", "View")
                    ViewTrainee(e.Item.Cells(1).Text, False)
                    dtgTrainee.SelectedIndex = -1
                    Me.txtName.ReadOnly = True
                    'Me.txtDealerCode.ReadOnly = True
                    Me.ICStartWork.Enabled = False
                    Me.txtEducationLevel.ReadOnly = True
                    Me.photoSrc.Disabled = True
                    Me.ddlShirtSize.Enabled = False
                    Me.ddlStatus.Enabled = False
                    Me.lblPopUpDealer.Enabled = False
                Case "3"
                    Dim lblSalesmanCode As Label = e.Item.FindControl("lblSalesmanCode")
                    Dim dataSales As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(lblSalesmanCode.Text)
                    Response.Redirect("../CallCenter/FrmCcInputCSTeam.aspx?ID=" + dataSales.ID.ToString + "&view=true&Mode=CS")
            End Select

        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            dtgTrainee.SelectedIndex = e.Item.ItemIndex
            Me.txtName.ReadOnly = False
            'Me.txtDealerCode.ReadOnly = False
            txtDealerCode.Enabled = False
            Me.ICStartWork.Enabled = True
            'Me.txtJobPosition.ReadOnly = True 'dnaFalse
            Me.txtEducationLevel.ReadOnly = False
            Me.photoSrc.Disabled = False
            Me.ddlShirtSize.Enabled = True
            Me.ddlStatus.Enabled = False
            If Me.IsKTB And CType(e.Item.Cells(8).FindControl("lblStatus"), Label).Text <> "Belum disetujui" Then
                Me.ddlStatus.Enabled = True
            End If
            Me.lblPopUpDealer.Enabled = True
            cbDeletePhoto.Visible = True
            cbDeletePhoto.Checked = False
            ViewTrainee(e.Item.Cells(1).Text, True)
        ElseIf e.CommandName = "Delete" Then
            DeleteTrainee(e.Item.Cells(1).Text)
        ElseIf e.CommandName = "Active" Then
            'ActivateTrainee(e.Item.Cells(1).Text, _
            '    CType(e.Item.Cells(8).FindControl("btnAktif"), LinkButton).Text = "<img src='../images/aktif.gif' border='0' alt='Aktif'>")
            ActivatedTrainee(e.Item.Cells(1).Text, _
                CType(e.Item.Cells(8).FindControl("lblStatus"), Label).Text <> "Aktif")

        End If
    End Sub

    Private Sub ActivateTrainee(ByVal nID As Integer, ByVal active As Boolean)
        Dim objTraineeFacade As TrTraineeFacade = New TrTraineeFacade(User)
        Dim objTrainee As TrTrainee = objTraineeFacade.Retrieve(nID)
        Dim arlRegClassColl As New ArrayList
        Dim IsErrorFound As Boolean = False

        If Not IsNothing(objTrainee) And (objTrainee.ID <> 0) Then
            Try
                If active Then
                    objTrainee.Status = CStr(EnumTrTrainee.TrTraineeStatus.Active)
                Else
                    arlRegClassColl = GetRegOfTrainee(objTrainee)
                    If arlRegClassColl.Count > 0 Then
                        MessageBox.Show("Data siswa - Status tidak bisa dirubah, karena siswa sudah terdaftar di kelas : \n" & _
                            BuildStringOfRegClassColl(arlRegClassColl) & "")
                        IsErrorFound = True
                    Else
                        objTrainee.Status = CStr(EnumTrTrainee.TrTraineeStatus.Deactive)
                    End If
                End If
                If Not IsErrorFound Then
                    objTraineeFacade.Update(objTrainee)

                    dtgTrainee.CurrentPageIndex = 0
                    dtgTrainee.SelectedIndex = -1
                    BindDataGrid(0)
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ActivatedTrainee(ByVal nID As Integer, ByVal active As Boolean)
        Dim objTraineeFacade As TrTraineeFacade = New TrTraineeFacade(User)
        Dim objTTHFacade As New TrTraineeSalesmanHeaderFacade(User)
        Dim objTrainee As TrTrainee = objTraineeFacade.Retrieve(nID)
        Dim arlRegClassColl As New ArrayList
        Dim IsErrorFound As Boolean = False

        If String.IsNullorEmpty(objTrainee.NoKTP.Trim) Then
            MessageBox.Show("No KTP kosong")
            Return
        End If

        Dim errMsg As String
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(TrTrainee), "NoKTP", MatchType.Exact, objTrainee.NoKTP))
        crit.opAnd(New Criteria(GetType(TrTrainee), "ID", MatchType.No, objTrainee.ID))

        Dim arrTrtrainee As ArrayList = objTraineeFacade.Retrieve(crit)

        Dim isUpdate As Boolean = False
        If arrTrtrainee.Count > 0 Then
            Dim objExistingTrainee As New TrTrainee
            objExistingTrainee = arrTrtrainee(0)
            Dim existingTTH As TrTraineeSalesmanHeader = objExistingTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) x.JobPositionAreaID = Category.ToLower())

            If Not existingTTH Is Nothing Then
                If existingTTH.Status = CType(EnumTrTrainee.TrTraineeStatus.Active, Short) Or existingTTH.Status = CType(EnumTrTrainee.TrTraineeStatus.Unapproved, Short) Then
                    errMsg = String.Format("KTP dengan no {0} sudah terdaftar di dealer {1} dengan nomor registrasi {2}. Harap non aktifkan terlebih dahulu.", objTrainee.NoKTP, _
                       objExistingTrainee.Dealer.DealerCode, objExistingTrainee.ID)
                    MessageBox.Show(errMsg)
                    Return
                End If

                existingTTH.Status = CType(EnumTrTrainee.TrTraineeStatus.Active, Short)
                existingTTH.JobPosition = objTrainee.JobPosition
                objTTHFacade.Update(existingTTH)
            Else
                Dim newTTH As New TrTraineeSalesmanHeader
                newTTH.Status = CShort(EnumTrTrainee.TrTraineeStatus.Active)
                newTTH.TrTrainee = objExistingTrainee
                newTTH.SalesmanHeader = objTrainee.SalesmanHeader
                newTTH.JobPositionAreaID = Category.ToLower()
                newTTH.JobPosition = objTrainee.JobPosition
                objTTHFacade.Insert(newTTH)

            End If
            ReplaceExistingDataWithNewData(objExistingTrainee, objTrainee)
            objTraineeFacade.Update(objExistingTrainee)

            objTrainee.RowStatus = CShort(DBRowStatus.Deleted)
            objTraineeFacade.Update(objTrainee)

            dtgTrainee.CurrentPageIndex = 0
            dtgTrainee.SelectedIndex = -1
            BindDataGrid(0)
            Exit Sub

        End If

        If Not IsNothing(objTrainee) And (objTrainee.ID <> 0) Then
            Try
                If active Then
                    objTrainee.Status = CStr(EnumTrTrainee.TrTraineeStatus.Active)

                    Dim criterias As New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), "RowStatus", _
                                    MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "TrTrainee.ID", MatchType.Exact, objTrainee.ID))
                    criterias.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "JobPositionAreaID", MatchType.Exact, AreaId))

                    Dim arrTrainee As ArrayList = objTTHFacade.Retrieve(criterias)
                    If arrTrainee.Count > 0 Then
                        Dim dataTrainee As TrTraineeSalesmanHeader = CType(arrTrainee(0), TrTraineeSalesmanHeader)
                        dataTrainee.Status = 1
                        objTTHFacade.Update(dataTrainee)
                    Else
                        Dim dtTrSlsmn As TrTraineeSalesmanHeader = New TrTraineeSalesmanHeader()
                        dtTrSlsmn.TrTrainee = objTrainee
                        Select Case Category.ToLower()
                            Case "1"
                                dtTrSlsmn.JobPositionAreaID = 1
                            Case "2"
                                dtTrSlsmn.JobPositionAreaID = 2
                            Case "3"
                                dtTrSlsmn.JobPositionAreaID = 3
                        End Select
                        dtTrSlsmn.JobPosition = objTrainee.JobPosition
                        dtTrSlsmn.SalesmanHeader = Nothing
                        dtTrSlsmn.Status = 1
                        objTTHFacade.Insert(dtTrSlsmn)
                    End If

                Else
                    arlRegClassColl = GetRegOfTrainee(objTrainee)
                    ' Modified by Ikhsan 20081218
                    ' Requested by Rina as a bug report
                    ' Produce message box to warn user to cancel the registered class first before deactivated the user
                    ' Start -----
                    If arlRegClassColl.Count > 0 Then
                        MessageBox.Show("Data siswa - Status tidak bisa dirubah, karena siswa sudah terdaftar di kelas : \n" & _
                            BuildStringOfRegClassColl(arlRegClassColl) & "")
                        IsErrorFound = True
                    Else
                        objTrainee.Status = CStr(EnumTrTrainee.TrTraineeStatus.Deactive)
                    End If
                    ' End -------
                End If
                If Not IsErrorFound Then
                    objTraineeFacade.Update(objTrainee)

                    dtgTrainee.CurrentPageIndex = 0
                    dtgTrainee.SelectedIndex = -1
                    BindDataGrid(0)
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ViewTrainee(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objTrainee As TrTrainee = New TrTraineeFacade(User).Retrieve(nID)
        sHTrainee.SetSession("objTrainee", objTrainee)
        FillFormFromObject(objTrainee)
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Function IsDealerExist(ByVal DealerCode As String) As Boolean
        Dim objDealerFacade As DealerFacade = New DealerFacade(User)

        Try
            If objDealerFacade.ValidateCode(DealerCode) = 0 Then
                Return False
            End If
        Catch
            Throw
        End Try

        Return True
    End Function

    Private Function ValidateDealerBranchIfExist(ByVal DealerBranchCode As String) As Boolean
        If txtDealerBranchCode.Text.Trim <> String.Empty Then
            Dim dealerBranch As DealerBranch = New DealerBranchFacade(User).Retrieve(DealerBranchCode)
            If IsNothing(dealerBranch) Then
                Return False
            ElseIf (dealerBranch.Dealer.DealerCode <> txtDealerCode.Text.Trim) Then
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function

    Private Sub FillingObjectFromForm(ByRef objTrainee As TrTrainee)
        objTrainee.Name = Me.txtName.Text

        objTrainee.BirthDate = Me.icBirthDate.Value
        objTrainee.Gender = Me.ddlGender.SelectedValue
        objTrainee.Dealer = New DealerFacade(User).Retrieve(Me.hdnDealerCode.Value)
        If (txtDealerBranchCode.Text.Trim <> String.Empty) Then
            objTrainee.DealerBranch = New DealerBranchFacade(User).Retrieve(txtDealerBranchCode.Text)
        End If
        objTrainee.StartWorkingDate = ICStartWork.Value
        objTrainee.JobPosition = txtJobPosition.Text
        objTrainee.EducationLevel = txtEducationLevel.Text
        objTrainee.ShirtSize = ddlShirtSize.SelectedItem.Text
        objTrainee.Status = ddlStatus.SelectedValue.ToString
        objTrainee.Email = txtEmail.Text.Trim
        objTrainee.NoKTP = txtKTP.Text.Trim
    End Sub

    Private Sub FillFormFromObject(ByVal objTrainee As TrTrainee)
        Me.txtName.Text = objTrainee.Name
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
        Me.txtDealerCode.Text = objTrainee.Dealer.DealerCode
        hdnDealerCode.Value = objTrainee.Dealer.DealerCode
        If Not IsNothing(objTrainee.DealerBranch) Then
            Me.txtDealerBranchCode.Text = objTrainee.DealerBranch.DealerBranchCode
        End If
        Me.ICStartWork.Value = objTrainee.StartWorkingDate
        Me.txtJobPosition.Text = objTrainee.JobPosition
        Me.txtEducationLevel.Text = objTrainee.EducationLevel
        Me.photoSrc.Attributes("value") = String.Empty
        Dim itemShirt As ListItem = Me.ddlShirtSize.Items.FindByText(objTrainee.ShirtSize)
        Me.ddlShirtSize.SelectedIndex = Me.ddlShirtSize.Items.IndexOf(itemShirt)
        Me.txtEmail.Text = objTrainee.Email
        Me.txtKTP.Text = objTrainee.NoKTP
        Dim itemStatus As ListItem = Me.ddlStatus.Items.FindByValue(objTrainee.Status)
        Me.ddlStatus.SelectedIndex = Me.ddlStatus.Items.IndexOf(itemStatus)
        'Response.Redirect("../WebResources/GetPhoto.aspx?id=" + objTrainee.ID.ToString)
        Me.photoView.ImageUrl = "../WebResources/GetPhoto.aspx?id=" + objTrainee.ID.ToString
    End Sub

    'Hard Delete, directly affected to physical data
    Private Sub DeleteTrainee(ByVal nID As Integer)
        If New HelperFacade(User, GetType(TrClassRegistration)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(TrClassRegistration), nID), _
            CreateAggreateForCheckRecord(GetType(TrClassRegistration))) Then

            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objTraineeFacade As TrTraineeFacade = New TrTraineeFacade(User)
            Dim objTrainee As TrTrainee = objTraineeFacade.Retrieve(nID)
            'objDealerGroup.RowStatus = DBRowStatus.Deleted
            'Dim nResult = New DealerGroupFacade(User).Update(objDealerGroup)

            Try
                If objTraineeFacade.DeleteFromDB(objTrainee) < 0 Then
                    MessageBox.Show(SR.DeleteFail)
                Else
                    MessageBox.Show(SR.DeleteSucces)
                End If
            Catch
                MessageBox.Show(SR.DeleteFail)
            End Try

            ClearData()
            dtgTrainee.SelectedIndex = -1
            dtgTrainee.CurrentPageIndex = 0
            BindDataGrid(dtgTrainee.CurrentPageIndex)
        End If
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        InitiatePage()
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
        ClearData()
    End Sub


    Private Sub dtgTrainee_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgTrainee.PageIndexChanged
        dtgTrainee.SelectedIndex = -1
        dtgTrainee.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgTrainee.CurrentPageIndex)
        ClearData()
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
    ByVal TraineeID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "TrTrainee", MatchType.Exact, TraineeID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

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
                    Throw New DataException("Foto harus file gambar dan maksimum 20 KB")
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
        If txtName.Text = String.Empty Then
            Return False
        End If

        If txtDealerCode.Text = String.Empty Then
            Return False
        End If

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

        'If ddlShirtSize.SelectedIndex = 0 Then
        '    Return False
        'End If
        Dim liStatus As ListItem
        liStatus = ddlStatus.Items.FindByValue(ddlStatus.SelectedValue)
        If IsNothing(liStatus) Then
            Return False
        End If

        Return True
    End Function

    Private Function IsUnhack() As Boolean
        'If txtName.Text.IndexOf("<") >= 0 Or txtName.Text.IndexOf(">") >= 0 Or txtName.Text.IndexOf("'") >= 0 Then
        '    Return False
        'End If

        If txtName.Text.IndexOf("<") >= 0 Or txtName.Text.IndexOf(">") >= 0 Then
            Return False
        End If

        If txtDealerCode.Text.IndexOf("<") >= 0 Or txtDealerCode.Text.IndexOf(">") >= 0 Or txtDealerCode.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtEducationLevel.Text.IndexOf("<") >= 0 Or txtEducationLevel.Text.IndexOf(">") >= 0 Or txtEducationLevel.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtJobPosition.Text.IndexOf("<") >= 0 Or txtJobPosition.Text.IndexOf(">") >= 0 Or txtJobPosition.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        Return True
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ClearData()
        dtgTrainee.CurrentPageIndex = 0
        CreateCriteria()
        BindDataGrid(dtgTrainee.CurrentPageIndex)
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Using excelPackage As New ExcelPackage()
            Dim wsData As ExcelWorksheet = excelPackage.Workbook.Worksheets.Add("Data Siswa")
            Dim rowIdx As Integer = 1
            Dim noUrut As Integer = 1

            CreateHeaderColumn(wsData, rowIdx)
            rowIdx += 1
            Dim criterias As ICriteria = helpers.GetSession("criteria")
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(CType(ViewState("CurrentSortColumn"), String))) And (Not IsNothing(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))) Then
                sortColl.Add(New Sort(GetType(TrTrainee), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
            Else
                sortColl = Nothing
            End If

            Dim dataDownload As ArrayList = New TrTraineeFacade(Me.User).Retrieve(criterias, sortColl)
            For Each rowValue As TrTrainee In dataDownload
                Dim status As String = String.Empty
                Dim id As String = rowValue.ID.ToString
                Dim gender As String = String.Empty
                Dim posisi As String = String.Empty
                Dim kodedealer As String = String.Empty
                Dim level As String = String.Empty
                Dim clmidx As Integer = 1

                If rowValue.ListTrTraineeSalesmanHeader.IsItems Then
                    If Not AreaId.Equals("2") Then
                        Dim datas As List(Of TrTraineeSalesmanHeader) = rowValue.ListTrTraineeSalesmanHeader
                        Dim data As TrTraineeSalesmanHeader = datas.FirstOrDefault(Function(x) x.JobPositionAreaID = CInt(AreaId))

                        If Not IsNothing(data.SalesmanHeader.Dealer) Then
                            kodedealer = data.SalesmanHeader.Dealer.DealerCode
                            If Not IsNothing(rowValue.Gender) Or rowValue.Gender > 0 Then
                                gender = EnumGender.GetStringGender(CType(rowValue.Gender, Integer))
                            End If
                        End If


                        If Not IsNothing(rowValue.Status) Then
                            Try
                                status = New EnumTrTrainee().StatusByIndex(Integer.Parse(data.Status))
                            Catch
                            End Try
                        End If

                        id = data.SalesmanHeader.SalesmanCode
                        posisi = data.RefJobPosition.Description
                    Else
                        If Not IsNothing(rowValue.Dealer) Then
                            kodedealer = rowValue.Dealer.DealerCode
                            If Not IsNothing(rowValue.Gender) Or rowValue.Gender > 0 Then
                                gender = EnumGender.GetStringGender(CType(rowValue.Gender, Integer))
                            Else
                                gender = String.Empty
                            End If
                        End If

                        If Not IsNothing(rowValue.Status) Then
                            Try
                                status = New EnumTrTrainee().StatusByIndex(Integer.Parse(rowValue.Status))
                            Catch
                            End Try
                        End If
                        posisi = rowValue.RefJobPosition.Description

                    End If
                End If

                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(noUrut.ToString(), Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(id, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.Name, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(kodedealer, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.BirthDate.DateToString, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(gender, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.StartWorkingDate.DateToString, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(posisi, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.EducationLevel, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                If AreaId.Equals("2") Then
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.ShirtSize, Style.ExcelHorizontalAlignment.Center)
                    clmidx += 1
                End If
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.Email, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.NoKTP, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(status, Style.ExcelHorizontalAlignment.Center)
                rowIdx += 1
                noUrut += 1

            Next
            rowIdx += 1
            rowIdx += 1
            For colIdx As Integer = 2 To 12
                wsData.Column(colIdx).AutoFit()
            Next

            Dim fileBytes = excelPackage.GetAsByteArray()
            Dim fileName As String = String.Format("DaftarSiswaKelas.xls")

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Dim success As Boolean = False

            Try
                success = imp.Start()
                If success Then
                    File.WriteAllBytes(Server.MapPath("~/DataTemp/" & fileName), fileBytes)
                    imp.StopImpersonate()
                End If

            Catch ex As Exception
                Exit Sub

            End Try

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)
        End Using
    End Sub

    Private Sub CreateHeaderColumn(ByVal wsData As ExcelWorksheet, ByVal rowIdx As Integer)
        Dim columnIdx As Integer = 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("No")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("No. Reg")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Nama Siswa")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Kode Dealer")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Tanggal Lahir")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Gender")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Mulai Bekerja")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Posisi Pekerjaan")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Level Pendidikan")
        columnIdx += 1

        If AreaId.Equals("2") Then
            wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Ukuran Baju")
            columnIdx += 1
        End If
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Email")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("No KTP")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Status")
        columnIdx += 1

    End Sub

    Private Sub ReplaceExistingDataWithNewData(ByRef objExistingTrainee As TrTrainee, ByVal objTrainee As TrTrainee)
        objExistingTrainee.Name = objTrainee.Name
        objExistingTrainee.BirthDate = objTrainee.BirthDate
        objExistingTrainee.Email = objTrainee.Email
        objExistingTrainee.StartWorkingDate = objTrainee.StartWorkingDate
        objExistingTrainee.JobPosition = objTrainee.JobPosition
        objExistingTrainee.SalesmanHeader = objTrainee.SalesmanHeader
        objExistingTrainee.EducationLevel = objTrainee.EducationLevel
        objExistingTrainee.Photo = objTrainee.Photo
        objExistingTrainee.ShirtSize = objTrainee.ShirtSize
        objExistingTrainee.Gender = objTrainee.Gender
        objExistingTrainee.Dealer = objTrainee.Dealer
        objExistingTrainee.DealerBranch = objTrainee.DealerBranch
        objExistingTrainee.Status = 1

    End Sub

End Class
