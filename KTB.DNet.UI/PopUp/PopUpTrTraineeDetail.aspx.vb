#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
#End Region

Public Class PopUpTrTraineeDetail
    Inherits System.Web.UI.Page

    Private ReadOnly Property TrTraineeID As String
        Get
            Return Request.QueryString("trtraineeid")
        End Get
    End Property
    Dim sHTrainee As SessionHelper = New SessionHelper

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            lblPopUpDealer.Attributes.Add("onClick", "ShowPPDealerSelection();")
            lblPopupDealerBranch.Attributes.Add("onClick", "ShowPPDealerBranchSelection();")
            lblSearchJobPos.Attributes("onclick") = "ShowJobPosSelection()"
            InitiatePage()
            BindDdlGender()
            If Not String.IsNullOrEmpty(TrTraineeID) Then
                ViewState.Add("vsProcess", "Edit")
                ViewTrainee(CInt(TrTraineeID), True)
                Me.txtName.Disabled()
                Me.txtDealerCode.Disabled()
                Me.txtDealerBranchCode.Disabled()
                Me.txtJobPosition.Disabled()
                Me.icBirthDate.Enabled = False
                Me.ICStartWork.Enabled = False
                Me.txtEducationLevel.Disabled()
                Me.photoSrc.Disabled = True
                Me.ddlShirtSize.Enabled = False
                Me.ddlStatus.Enabled = False
                Me.ddlGender.Enabled = False
                Me.lblPopUpDealer.Enabled = False
                cbDeletePhoto.Visible = False
                cbDeletePhoto.Checked = False

                btnSimpan.Visible = True
            End If

        End If
        txtJobPosition.Attributes.Add("readonly", "readonly")
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
        Next

        ddlStatus.SelectedValue = 1

        Dim ShirtSize As ArrayList = objEnumTrTrainee.RetrieveSize()
        For Each es As EnumShirtData In ShirtSize
            lItem = New ListItem(es.NameTitle, es.ValTitle.ToString)
            ddlShirtSize.Items.Add(lItem)
        Next

        Dim li As ListItem = New ListItem(String.Empty, String.Empty)
    End Sub

    Private Sub ClearData()
        sHTrainee.RemoveSession("objTrainee")
        Me.txtName.ReadOnly = False
        Me.txtDealerCode.ReadOnly = False
        Me.ICStartWork.Enabled = True
        'Me.txtJobPosition.ReadOnly = True 'dna False
        Me.txtEducationLevel.ReadOnly = False
        Me.photoSrc.Disabled = False
        Me.ddlShirtSize.Enabled = True
        Me.ddlStatus.Enabled = True
        Me.lblPopUpDealer.Enabled = True

        Me.txtName.Text = String.Empty
        Me.txtDealerCode.Text = String.Empty
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
        cbDeletePhoto.Visible = False
    End Sub


    Private Sub BindDdlGender()
        CommonFunction.BindFromEnum("SalesmanGender", ddlGender, User, False, "NameStatus", "ValStatus")
        ddlGender.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
        ddlGender.SelectedIndex = 0
    End Sub

    Private Sub ViewTrainee(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objTrainee As TrTrainee = New TrTraineeFacade(User).Retrieve(nID)
        sHTrainee.SetSession("objTrainee", objTrainee)
        FillFormFromObject(objTrainee)
        Me.btnSimpan.Enabled = EditStatus
    End Sub


    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
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

        If IsUnhack() Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return
        End If

        Try
            UpdateTrainee()
            
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


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

        If txtKTP.IsEmpty Then
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
            If Not String.IsNullorEmpty(Request.QueryString("isreload")) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "closePage", "FuncUpdated('" + Request.QueryString("trtraineeid") + ";" + objTrainee.NoKTP + "')", True)
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

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

    Private Function GetRegOfTrainee(ByVal objTrainee As TrTrainee) As ArrayList
        Dim objRegFacade As New TrClassRegistrationFacade(User)
        Return objRegFacade.Retrieve(RegOfTraineeCriteria(objTrainee.ID))
    End Function

    Private Function IsTraineeExist(ByVal objFacade As TrTraineeFacade, ByVal objDomain As TrTrainee) As Boolean
        Return objFacade.ValidateTrainee(objDomain) > 0
    End Function

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

            nResult = objTraineeFacade.Update(objTrainee)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
                ClearData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

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

    Private Sub FillingObjectFromForm(ByVal objTrainee As TrTrainee)
        objTrainee.Name = Me.txtName.Text

        objTrainee.BirthDate = Me.icBirthDate.Value
        objTrainee.Gender = Me.ddlGender.SelectedValue
        objTrainee.Dealer = New DealerFacade(User).Retrieve(Me.txtDealerCode.Text)
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
        Me.photoView.ImageUrl = "../WebResources/GetPhoto.aspx?id=" + objTrainee.ID.ToString
    End Sub

    Private Function IsFieldValidationUpdated(ByVal objDomain As TrTrainee) As Boolean
        Dim objDomainComparer As TrTrainee = New TrTraineeFacade(User).Retrieve(objDomain.ID)

        If IsNothing(objDomainComparer) Or objDomainComparer.ID = 0 Then
            Throw New Exception(SR.DataNotFound("Siswa"))
        End If

        Return objDomain.Name <> objDomainComparer.Name _
        Or objDomain.StartWorkingDate <> objDomainComparer.StartWorkingDate _
        Or objDomain.Dealer.ID <> objDomain.Dealer.ID

    End Function

    Private Function RegOfTraineeCriteria(ByVal TraineeID As Integer) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, TraineeID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Register, String)))
        Return criterias
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

End Class