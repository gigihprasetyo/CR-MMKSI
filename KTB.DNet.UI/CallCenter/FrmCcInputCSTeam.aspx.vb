Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Math
Imports System.IO
Imports System.Text
Imports GlobalExtensions
Imports PdfSharp
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports PdfSharp.Drawing
#Region "Custom NameSpace"
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessValidation
Imports System.Text.RegularExpressions
Imports KTB.DNet.BusinessFacade.CallCenter
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports System.Linq
Imports KTB.DNet.BusinessFacade.UserManagement

#End Region

Public Class FrmInputCSTeam
    Inherits System.Web.UI.Page

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page, "INPUT CS EMPLOYEE")

#Region "PrivateVariables"
    Private _SalesmanHeaderFacade As New SalesmanHeaderFacade(User)
    Private _salesmanHeader As SalesmanHeader
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper
    Dim nIdTemp As Integer = -100

    'Dim arrHistoryToInsert As New ArrayList
    'Dim arrHistoryToDelete As New ArrayList
    Dim arrHistory As New ArrayList
    Dim arrArea As New ArrayList
    Dim arrPartShop As New ArrayList
    Dim arrExp As New ArrayList
    Dim arrTraining As New ArrayList
    Dim arrAccomp As New ArrayList
    Dim arrTarget As New ArrayList
    Dim blnSaved As Boolean
    Dim strCurrProfile As String
    Dim strIdSManCode As String
    Dim strIdBManCode As String
    Dim intIdLevelBlank As Integer
    Private objDealerTmp As Dealer
    Private sessImageByte As String = "SESSION_IMAGE_BYTE"
    Private sessKtpPath As String = "SESSION_KTP_PATH"
    'Private isRegistered As Boolean

#End Region

#Region "PrivateCustomMethods"

    Private Function uploadFile(objSalHea As SalesmanHeader, ByRef errmsg As String) As String

        Dim strComplaintNumber As String

        '-- Source file name



        If IsNothing(photoSrc.PostedFile) Then
            errmsg = "Tidak Ada Foto Yang di Upload"
            Return ""
        End If
        Dim SrcFile As String = Path.GetFileName(photoSrc.PostedFile.FileName)


        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("CSTeam") & objSalHea.SalesmanCode & "\" & SrcFile      '-- Destination file

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim maxFinfo As Long = KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize")
        Dim success As Boolean = False
        Dim finfo As New FileInfo(DestFile)
        Try
            If finfo.Length > maxFinfo Then
                errmsg = "Ukuran File Maximal 1 MB"
                Return ""
            End If

            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                photoSrc.PostedFile.SaveAs(DestFile)
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
        errmsg = ""
        Return DestFile
    End Function


    Private Function msgErrorEmailValidation(strEmail As String) As String
        Dim objRenderPanel As RenderingProfile = New RenderingProfile
        Dim errorMessage As String
        Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)


        Dim isValidEmailAddress As Boolean = False
        isValidEmailAddress = ValidateEmail(strEmail)

        If (strEmail = "") Then
            errorMessage = "Email harus diisi!"
        Else
            If (isValidEmailAddress = False) Then
                errorMessage = "Format email salah!"
            End If
        End If

        Return errorMessage
    End Function

    Public Shared Function ValidateEmail(ByVal emailAddress As String) As Boolean
        Dim emailAddressCheck As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            emailAddressCheck = True
        Else
            emailAddressCheck = False
        End If

        Return emailAddressCheck
    End Function

    Private Sub SetSetting()
        lblPageTitle.Text = "CS EMPLOYEE"

        lblKodeSalesman.Text = "Kode CS Employee"

        pnlSuperior.Visible = False
        ddlIndicator.Enabled = False
        txtMode.Value = ddlIndicator.SelectedValue
        Dim lblKtp As Label
        If Request.QueryString("edit") = "true" Then
            lblKtp = Me.Panel1.FindControl("LABEL29_12")
        Else
            lblKtp = Me.Panel1.FindControl("LABEL57_12")
        End If
        If Not lblKtp Is Nothing Then
            lblKtp.Text = "NO KTP"
        End If

    End Sub

    Private Sub SetProfile()
        strCurrProfile = "Sales_DBS_CS"
    End Sub

    Private Sub BindDropDown()
        Dim iMenu As Integer

        Dim CommFunction As New CommonFunction

        CommFunction.BindEnumDetailToDDL(ddlStatus, "EMP_STT")
        CommFunction.BindEnumDetailToDDL(ddlGender, "JK")
        CommFunction.BindEnumDetailToDDL(ddlIndicator, "SI")

        CommFunction.BindEnumDetailToDDL(ddlMarriedStatus, "STAKWIN")
        CommFunction.BindEnumDetailToDDL(ddlJobPositionDesc, "EMP_POS_CS")
        CommFunction.BindEnumDetailToDDL(ddlSalesmanLevel, "EMP_LEVEL")

        CommFunction.BindProvince(ddlPropinsi, User, True, False)

        'ddlGender.SelectedValue = 0
        ddlMarriedStatus.SelectedIndex = 0
        ddlIndicator.SelectedIndex = 5

        BindDdlKategoriTim()

    End Sub

    Private Function msgErrorSuperiorAndKTP() As String
        If txtSuperior.Text.Trim <> "" Then
            Dim objSalesmanHeaderFacade As New SalesmanHeaderFacade(User)
            Dim objSuperior As SalesmanHeader = objSalesmanHeaderFacade.RetrieveByCode(txtSuperior.Text.Trim)
            If objSuperior.ID = 0 Then
                Return "Data atasan dengan kode " & txtSuperior.Text.Trim & " tidak ada."
            End If

            'Salesman and salesman counter treated as a same level
            Dim CurrentPosition As Integer
            CurrentPosition = CInt(ddlJobPositionDesc.SelectedValue)
            If CurrentPosition = 2 Then CurrentPosition = 3

            If CurrentPosition + 2 < objSuperior.JobPosition.ID Or CurrentPosition >= objSuperior.JobPosition.ID Then
                Return "Posisi atasan maksimal 2 tingkat diatas salesman ybs."
            End If
        End If



        'KTP data should get from profile
        Dim objRenderPanel As RenderingProfile = New RenderingProfile

        Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
        Dim strKTP As String = objRenderPanel.RetrieveValueByProfileHeaderID(Me, objProfileGroup, CType(EnumProfileType.ProfileType.SALESMAN, Short), User, 29)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
        criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, strKTP.ToString.Trim))
        If CType(ViewState("vsProcess"), String) <> "Insert" Then
            criterias.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.No, Request.QueryString("ID")))
        End If

        Dim arrKTPExist As ArrayList = New SalesmanProfileFacade(User).Retrieve(criterias)

        If arrKTPExist.Count > 0 Then
            Dim objSalesmanProfile As SalesmanProfile = arrKTPExist(0)
            Dim objSalesmanExist As SalesmanHeader = objSalesmanProfile.SalesmanHeader
            If objSalesmanExist.RowStatus = CType(DBRowStatus.Active, Short) Then
                Return "Data KTP yang anda masukkan sudah ada atas nama " & objSalesmanExist.SalesmanCode & " " & objSalesmanExist.Name
            End If

        End If


        Return ""

    End Function



    Private Function MsgErrorDateRangeHistory(ByVal DtIn As Date, ByVal DtOut As Date) As String

        Dim arrHistory As ArrayList
        If CType(Request.QueryString("id"), Integer) < 0 Or Request.QueryString("id") = String.Empty Then
            arrHistory = sessHelper.GetSession("arrHistoryToInsert")
            'dtgHistory.DataBind()

        Else

            Dim totalRow As Integer = 0

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanDealerHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanDealerHistory), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))

            arrHistory = New SalesmanDealerHistoryFacade(User).RetrieveByCriteria(criterias)
        End If

        For Each item As SalesmanDealerHistory In arrHistory
            If (DtIn < item.DateOut And DtIn > item.DateIn) Or _
                (DtOut <= item.DateOut And DtOut >= item.DateIn) Or _
                (DtIn < item.DateIn And DtOut > item.DateOut) Then
                Return "Tanggal overlap dengan history pada dealer " & item.Dealer.DealerCode & " ( " & item.DateIn.ToString("dd/MM/yyyy") & " - " & item.DateOut.ToString("dd/MM/yyyy") & " )"
            End If
        Next

        Return ""

    End Function

    Private Sub BindDgTarget(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanSalesTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanSalesTarget), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))


    End Sub

    Private Sub BindDgPrestasi(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAccomplish), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanAccomplish), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))



    End Sub

    Private Sub BindDgExperience(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanExperience), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanExperience), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))



    End Sub


    Private Function CheckValidation(Optional ByVal isValidation As Boolean = True) As Boolean
        Dim blnValid As Boolean = True


        If txtName.Text = "" Then
            blnValid = False
            MessageBox.Show("Silakan input nama Salesman terlebih dahulu")
            Return (blnValid)
        End If

        If txtPlaceOfBirth.Text = "" Then
            blnValid = False
            MessageBox.Show("Silakan input tempat lahir Salesman terlebih dahulu")
            Return (blnValid)
        End If

        'If txtAlamat.Text = "" Then
        '    blnValid = False
        '    MessageBox.Show("Silakan input alamat Salesman terlebih dahulu")
        '    Return (blnValid)
        'End If

        If ddlKota.SelectedValue = "" Then
            blnValid = False
            MessageBox.Show("Silakan pilih kota Salesman terlebih dahulu")
            Return (blnValid)
        Else
            Dim arrCity As ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, ddlKota.SelectedItem.Text))
            arrCity = New CityFacade(User).Retrieve(criterias)

            If Not IsNothing(arrCity) Then
                If arrCity.Count <= 0 Then
                    blnValid = False
                    MessageBox.Show("Silakan masukan data Kota yg valid, gunakan list")
                    Return (blnValid)
                End If
            End If
        End If

        If ddlMarriedStatus.SelectedValue = "" Then
            blnValid = False
            MessageBox.Show("Silakan pilih Status Pernikahan Salesman terlebih dahulu")
            Return (blnValid)
        End If

        If ddlJobPositionDesc.SelectedValue = "" Then
            blnValid = False
            MessageBox.Show("Silakan dipilih posisi Salesman terlebih dahulu")
            Return (blnValid)

        End If

        If isValidation Then
            If (ICDateOfBirth.Value = ICStartWork.Value) Then
                blnValid = False
                MessageBox.Show("Tanggal Lahir tidak boleh sama dengan Tanggal Masuk")
                Return (blnValid)
            End If

            If (ICStartWork.Value < ICDateOfBirth.Value) Then
                blnValid = False
                MessageBox.Show("Tanggal Masuk tidak boleh lebih kecil dari Tanggal Lahir")
                Return (blnValid)
            End If
        End If

        

        'If (ddlSalesmanLevel.SelectedValue = "") And (ddlSalesmanLevel.Enabled) Then
        '    blnValid = False
        '    MessageBox.Show("Salesman level belum dipilih")
        '    Return (blnValid)
        'End If

        Return blnValid

    End Function

    Private Sub UpperControl(ByVal blnIsUpper As Boolean)
        CommonFunction.UpperTextControl(txtName, blnIsUpper)
        CommonFunction.UpperTextControl(txtPlaceOfBirth, blnIsUpper)
        CommonFunction.UpperTextControl(txtAlamat, blnIsUpper)
    End Sub

    ' keperluan untuk set variable from session
    Private Sub SetVarFromSession()
        If Not IsNothing(sessHelper.GetSession("strIdSManCode")) Then
            strIdSManCode = CType(sessHelper.GetSession("strIdSManCode"), String)
        End If
        If Not IsNothing(sessHelper.GetSession("strIdBManCode")) Then
            strIdBManCode = CType(sessHelper.GetSession("strIdBManCode"), String)
        End If
        If Not IsNothing(sessHelper.GetSession("intIdLevelBlank")) Then
            intIdLevelBlank = CType(sessHelper.GetSession("intIdLevelBlank"), Integer)
        End If
    End Sub

    ' keperluan untuk membuat salesman level bayangan
    Private Sub GetIdLevelBlank()
        Dim arrSalesmanLevel As ArrayList
        Dim crits As New CriteriaComposite(New Criteria(GetType(SalesmanLevel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Deleted, Short)))
        crits.opAnd(New Criteria(GetType(SalesmanLevel), "Description", MatchType.Exact, ""))
        arrSalesmanLevel = New SalesmanHeaderFacade(User).RetrieveSalesmanLevel(crits)

        For Each item As SalesmanLevel In arrSalesmanLevel
            sessHelper.SetSession("intIdLevelBlank", item.ID)
            Exit For
        Next
    End Sub

    ' penambahan untuk initialize data
    Private Sub ClearData()
        lblSalesmanCode.Text = ""
        txtName.Text = ""
        txtPlaceOfBirth.Text = ""
        txtJobPosition.Value = ""
        txtAlamat.Text = ""

        ddlGender.SelectedIndex = -1
        ddlSalesmanLevel.SelectedIndex = 1
        ddlMarriedStatus.SelectedIndex = -1
        ddlPropinsi.SelectedIndex = -1
        ddlKota.SelectedIndex = -1

        ICDateOfBirth.Value = Date.Now
        ICStartWork.Value = Date.Now
        ICEndWork.Value = Date.Now

        'photoView.Visible = False
        blnSaved = False

        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub ProcessSave(ByRef objSalHea As SalesmanHeader, Optional ByVal isValidation As Boolean = True)
        Dim objDealer As Dealer = Session.Item("DEALER")
        Dim bStatus As Boolean = True
        SetVarFromSession()

        If (CheckValidation(isValidation) = True) Then
            UpperControl(True)
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                Insert(objSalHea)
                bStatus = True
            Else
                Update()
                bStatus = False
                objSalHea = CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader)
            End If


            If objSalHea IsNot Nothing Then
                Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
                'Await Task.Run(Sub()
                Try
                    SendEmail(objSalHea, objUser, bStatus)
                Catch ex As Exception
                    MessageBox.Show("Gagal kirim email")
                End Try

                '              End Sub)

            End If

        End If
    End Sub

    Private Sub Insert(ByRef objSalHea As SalesmanHeader)
        Dim objSalesHeaderFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Try

            'cek apakah KTP Sudah Terdaftar
            If Not IsKTPExist() Then

                Dim idReturn As Integer = InsertSalesmanHeader(objSalHea)

                If idReturn > 0 Then

                    objSalesHeaderFacade.DoEffectUserGroupMember(objSalHea)
                    objSalHea.ID = idReturn

                    If objSalHea.JobPosition.Code <> "CSO" Then
                        CreateProfileKategoriTeam(objSalHea)
                    End If

                    'photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & idReturn.ToString & "&type=" & "SalesmanHeader"
                    sessHelper.SetSession("IsInsertSuccess", 1)
                    Response.Redirect("FrmCcInputCSTeam.aspx?Mode=" & Request.QueryString("mode") & "&ID=" + idReturn.ToString + "&edit=true", False)
                    MessageBox.Show(SR.SaveSuccess)

                Else
                    MessageBox.Show(SR.SaveFail)
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
    End Sub

    Private Sub Update()
        Dim objSalesHeaderFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Try
            If Not IsKTPExistUpdate() Then
                Dim idReturn As Integer = UpdateSalesmanHeader()
                If idReturn <> -1 Then
                    Dim objSalesHeader As SalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader)
                    If Not IsNothing(objSalesHeader) Then



                    End If

                    Dim errmsg As String = ""
                    'If String.IsNullOrEmpty(uploadFile(objSalesHeader, errmsg)) And errmsg <> "" Then
                    '    MessageBox.Show(errmsg)
                    'Else
                    '    objPath.Path = uploadFile(objSalesHeader, errmsg)
                    'End If

                    'objPath.ReferenceTable = "SalesmanHeader"
                    'objPath.ForeigKeyID = idReturn

                    'Dim arlPath As New ArrayList
                    'idReturn = New GeneralPathImageFacade(User).Insert(objPath)

                    If objSalesHeader.JobPosition.Code <> "CSO" Then

                        Dim existingProfile As SalesmanProfile = GetExistingProfileKategoriTim(objSalesHeader)
                        If existingProfile Is Nothing Then
                            CreateProfileKategoriTeam(objSalesHeader)
                        Else
                            existingProfile.ProfileValue = ddlKategoriTim.SelectedItem.Text
                            Dim iResult As Integer = New SalesmanProfileFacade(User).Update(existingProfile)
                        End If


                    End If

                    objSalesHeaderFacade.DoEffectUserGroupMember(objSalesHeader)
                    MessageBox.Show(SR.UpdateSucces)
                    Dim objDealer As Dealer = Session.Item("DEALER")
                    If objSalesHeader.SalesmanCode = String.Empty Then
                        If Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                            btnGenerateCode.Visible = False
                            btnGenerateCode.Enabled = False
                        End If

                    End If
                Else
                    MessageBox.Show(SR.UpdateFail)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
    End Sub

    Private Function InsertSalesmanHeader(ByRef objSalHea As SalesmanHeader) As Integer

        Dim nResult As Integer = -1

        Dim objSalesmanHeaderFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Dim objSalesmanAdditionalInfo As New SalesmanAdditionalInfo

        objSalHea = New SalesmanHeader

        Try
            With objSalHea
                .Dealer = New DealerFacade(User).Retrieve(lblDealerCode.Text)
                .SalesmanCode = lblSalesmanCode.Text
                .Name = txtName.Text
                .PlaceOfBirth = txtPlaceOfBirth.Text
                .DateOfBirth = ICDateOfBirth.Value
                .HireDate = ICStartWork.Value
                .SalesIndicator = CType(ddlIndicator.SelectedValue, Byte)
                .Gender = ddlGender.SelectedValue
                .MarriedStatus = ddlMarriedStatus.SelectedValue
                .Address = txtAlamat.Text
                .City = ddlKota.SelectedItem.Text
                .Status = EnumSalesmanStatus.SalesmanStatus.Baru
                .RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Belum_Register
                .IsRequestID = EnumSalesmanIsRequest.SalesmanIsRequest.Belum_Request
                If ddlSalesmanLevel.SelectedValue <> "" Then
                    .SalesmanLevel = New SalesmanLevelFacade(User).Retrieve(CInt(ddlSalesmanLevel.SelectedValue))

                End If

                .JobPosition = New JobPositionFacade(User).Retrieve(CInt(ddlJobPositionDesc.SelectedValue))


                If txtSuperior.Text = "" Then
                    .LeaderId = 0
                Else
                    Dim objSuperior As SalesmanHeader = objSalesmanHeaderFacade.RetrieveByCode(txtSuperior.Text.Trim)
                    .LeaderId = objSuperior.ID
                End If

                If Not IsNothing(sessHelper.GetSession(sessImageByte)) Then
                    .Image = CType(sessHelper.GetSession(sessImageByte), Byte())
                End If

                '' penambahan image menggunakan method conversi
                'Dim filePic As HttpPostedFile = CType(sessHelper.GetSession("IMAGEPATH"), HttpPostedFile)
                'If Not IsNothing(filePic) Then
                '    If (filePic.FileName <> String.Empty) Then 'If (photoSrc.PostedFile.FileName <> String.Empty) Then
                '        Dim imageFile As Byte()
                '        imageFile = uploadFile()
                '        .Image = imageFile
                '        lblRemoveImage.Visible = True
                '        lblRemoveImage.Enabled = True
                '    Else
                '        lblRemoveImage.Visible = False
                '        lblRemoveImage.Enabled = False
                '    End If
                'End If
            End With

            Dim objRenderPanel As RenderingProfile = New RenderingProfile
            ' sebelum melakukan update, pastikan data ProfileHeaderToGroup sdh ada / belum
            Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
            If Not IsNothing(objProfileGroup) Then
                Dim arrTmp As ArrayList
                Dim criterias As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, objProfileGroup.ID))
                arrTmp = New ProfileHeaderToGroupFacade(User).RetrieveByCriteria(criterias)

                'To make sure that if edit operation doesn't pass dealerhistory
                If Not (CType(Request.QueryString("id"), Integer) < 0 Or Request.QueryString("id") = String.Empty) Then
                    sessHelper.SetSession("arrHistoryToInsert", New ArrayList)

                End If

                If Not IsNothing(arrTmp) Then
                    If arrTmp.Count > 0 Then
                        Dim al As ArrayList = objRenderPanel.RetrieveProfileValue(Me, objProfileGroup, CType(EnumProfileType.ProfileType.CS, Short), User)
                        nResult = New SalesmanHeaderFacade(User).Insert(objSalHea, al)
                    Else
                        nResult = New SalesmanHeaderFacade(User).InsertTransactionCS(objSalHea, sessHelper.GetSession("arrHistoryToInsert"))
                    End If
                Else
                    nResult = New SalesmanHeaderFacade(User).InsertTransactionCS(objSalHea, sessHelper.GetSession("arrHistoryToInsert"))
                End If

                sessHelper.SetSession("objSalesmanHeader", objSalHea)

                objSalesmanAdditionalInfo.SalesmanHeader = objSalHea

                If objSalHea.JobPosition.Code = "CSO" Then
                    objSalesmanAdditionalInfo.CSOHireDate = ICStartWork.Value
                End If

                If Not IsNothing(sessHelper.GetSession(sessKtpPath)) Then

                    objSalesmanAdditionalInfo.KtpImagePath = sessHelper.GetSession(sessKtpPath).ToString()
                    objSalesmanAdditionalInfo.SalesmanCategoryLevel = New SalesmanCategoryLevelFacade(User).Retrieve(1) 'HARDCODE
                End If

                If uplAppointmentLetter.PostedFile.ContentLength > 0 Then
                    Dim pdfExtension() As String = {".pdf"}
                    Dim filePath As String = UploadAndGetFileName(uplAppointmentLetter, pdfExtension)
                    hdnAppointmentLetterPath.Value = filePath
                    objSalesmanAdditionalInfo.AppointmentLetterPath = filePath
                End If

                Dim oResult As Integer = New SalesmanAdditionalInfoFacade(User).Insert(objSalesmanAdditionalInfo)


            End If
            Return nResult
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function UpdateSalesmanHeader() As Integer
        Dim nresult As Integer = -1
        Try
            Dim objSalesmanHeader As New SalesmanHeader
            Dim objJPosition As New JobPosition
            Dim objJobPosition As New JobPosition

            If CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader) Is Nothing Then
                objSalesmanHeader = CType(sessHelper.GetSession("CSTeam"), SalesmanHeader)
            Else
                objSalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader)
            End If

            Dim objRenderPanel As RenderingProfile = New RenderingProfile
            Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
            Dim al As ArrayList = objRenderPanel.RetrieveProfileValue(Me, objProfileGroup, CType(EnumProfileType.ProfileType.CS, Short), User)

            Dim oldJobPosition As JobPosition = objSalesmanHeader.JobPosition

            With objSalesmanHeader
                .Dealer = New DealerFacade(User).Retrieve(lblDealerCode.Text)
                '.SalesmanCode = lblSalesmanCode.Text
                .Name = txtName.Text
                .PlaceOfBirth = txtPlaceOfBirth.Text
                .DateOfBirth = ICDateOfBirth.Value
                .HireDate = ICStartWork.Value
                .SalesIndicator = CType(ddlIndicator.SelectedValue, Byte)
                .SalesmanArea = Nothing
                .JobPosition = New JobPositionFacade(User).Retrieve(CType(ddlJobPositionDesc.SelectedValue, Integer))
                If ddlSalesmanLevel.SelectedValue <> "" Then
                    .SalesmanLevel = New SalesmanLevelFacade(User).Retrieve(CInt(ddlSalesmanLevel.SelectedValue))
                End If

                .LeaderId = 0
                .JobPositionId_Leader = Nothing
                .Gender = ddlGender.SelectedValue
                .MarriedStatus = ddlMarriedStatus.SelectedValue
                .Address = txtAlamat.Text
                .City = ddlKota.SelectedItem.Text
                .RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register

                If Not IsNothing(Request.QueryString("SalesResign")) Then
                    .RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Belum_Register
                    '.ResignDate = ICEndWork.Value
                    '.ResignReason = String.Empty
                End If

                '.Status = Nothing
                '.SalesmanLevel = Nothing

                Dim blnRemovePic As Boolean
                If Not IsNothing(sessHelper.GetSession("RemovePic")) Then
                    blnRemovePic = sessHelper.GetSession("RemovePic")
                    If blnRemovePic = True Then
                        .Image = Nothing
                    End If
                End If

                ' penambahan image menggunakan method conversi
                Dim filePic As HttpPostedFile = CType(sessHelper.GetSession("IMAGEPATH"), HttpPostedFile)
                If Not IsNothing(filePic) Then
                    If (filePic.FileName <> String.Empty) Then 'If (photoSrc.PostedFile.FileName <> String.Empty) Then
                        Dim imageFile As Byte()
                        imageFile = uploadFile()
                        .Image = imageFile
                        lblRemoveImage.Visible = True
                        lblRemoveImage.Enabled = True
                    Else
                        lblRemoveImage.Visible = False
                        lblRemoveImage.Enabled = False
                    End If
                End If
            End With

            If CType(ViewState("vsProcess"), String) = "Konfirmasi" Then

                objSalesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Konfirmasi, String)
                objSalesmanHeader.RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Belum_Register
                If objSalesmanHeader.SalesIndicator = CType(EnumSalesmanUnit.SalesmanUnit.CS, Short) Then
                    nresult = New SalesmanHeaderFacade(User).UpdateCSTeamOnly(objSalesmanHeader, al, objProfileGroup)
                Else
                    nresult = New SalesmanHeaderFacade(User).Insert(objSalesmanHeader)
                End If
            Else
                nresult = New SalesmanHeaderFacade(User).UpdateCSTeamOnly(objSalesmanHeader, al, objProfileGroup)
            End If

            Dim objSalesmanAdditionalInfo As SalesmanAdditionalInfo = GetSalesmanAdditionalInfoBySalesmanHeaderId(objSalesmanHeader.ID)

            If uplAppointmentLetter.PostedFile.ContentLength > 0 Then
                Dim pdfExtension() As String = {".pdf"}
                Dim filePath As String = UploadAndGetFileName(uplAppointmentLetter, pdfExtension)
                hdnAppointmentLetterPath.Value = filePath
                objSalesmanAdditionalInfo.AppointmentLetterPath = filePath
            End If

            If Not sessHelper.GetSession(sessKtpPath) Is Nothing Then
                Dim addFacade As New SalesmanAdditionalInfoFacade(User)
                objSalesmanAdditionalInfo.KtpImagePath = sessHelper.GetSession(sessKtpPath)

                If objSalesmanHeader.JobPosition.Code = "CSO" And oldJobPosition.Code <> "CSO" Then
                    objSalesmanAdditionalInfo.CSOHireDate = ICStartWork.Value
                End If

                If objSalesmanAdditionalInfo.ID <> 0 Then
                    addFacade.Update(objSalesmanAdditionalInfo)
                Else
                    addFacade.Insert(objSalesmanAdditionalInfo)
                End If

            End If

            sessHelper.SetSession("vsSalesmanHeader", objSalesmanHeader)

            photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & Request.QueryString("id") & "&type=" & "SalesmanHeader"
            Return nresult
        Catch ex As Exception
            Return nresult
        End Try
    End Function

    Private Function GetSalesmanLevelByYears(ByVal iYears As Integer) As Integer
        Dim iReturn As Integer = 0


        Select Case iYears
            Case Is < 5
                iReturn = EnumSalesmanPartLevel.Level.Junior
            Case Is < 10
                iReturn = EnumSalesmanPartLevel.Level.Senior
            Case Is >= 10
                iReturn = EnumSalesmanPartLevel.Level.Top
        End Select

        Return iReturn
    End Function

    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "Name"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Insert")
        Dim objJobPosition As New JobPosition

        objJobPosition = New JobPositionFacade(User).Retrieve(KTB.DNet.Lib.WebConfig.GetValue("SManCode"))
        If Not objJobPosition Is Nothing Then
            sessHelper.SetSession("strIdSManCode", objJobPosition.ID.ToString)
            strIdSManCode = objJobPosition.ID.ToString
        End If

        'objJobPosition = New JobPositionFacade(User).Retrieve(KTB.DNet.Lib.WebConfig.GetValue("BManCode"))
        'If Not objJobPosition Is Nothing Then
        '    sessHelper.SetSession("strIdBManCode", objJobPosition.ID.ToString)
        '    strIdBManCode = objJobPosition.ID.ToString
        'End If

        GetIdLevelBlank()
    End Sub

    ' penambahan untuk delete data
    Private Sub Delete(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(nID)
        Dim facade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Dim iReturn As Integer = -1
        'iReturn = facade.DeleteTransaction(objSalesmanArea)
        iReturn = facade.DeleteFromDB(objSalesmanHeader)
        If iReturn <= 0 Then
            MessageBox.Show("Record Gagal Dihapus")
        End If

    End Sub

    ' penambahan untuk view data
    Private Sub View(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objSalesmanHeaderFacade As New SalesmanHeaderFacade(User)
        Dim objSalesmanHeader As SalesmanHeader = objSalesmanHeaderFacade.Retrieve(nID)
        Dim objDealer As Dealer

        If Not IsNothing(Request.QueryString("SalesResign")) Then
            objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Else
            objDealer = objSalesmanHeader.Dealer
        End If

        Dim objSalesmanAdditionalInfo As SalesmanAdditionalInfo = GetSalesmanAdditionalInfoBySalesmanHeaderId(nID)

        If objSalesmanAdditionalInfo.ID <> 0 Then
            If objSalesmanAdditionalInfo.KtpImagePath <> String.Empty Then
                sessHelper.SetSession(sessKtpPath, objSalesmanAdditionalInfo.KtpImagePath)
            End If

            hdnAppointmentLetterPath.Value = objSalesmanAdditionalInfo.AppointmentLetterPath

        End If

        If objSalesmanHeader.SalesmanCode = String.Empty Then
            If Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                btnGenerateCode.Enabled = Not EditStatus
            End If
        Else
            btnGenerateCode.Enabled = Not EditStatus
            btnGenerateCode.Visible = Not EditStatus
        End If

        lblDealerCode.Text = objDealer.DealerCode
        lblDealerName.Text = objDealer.DealerName

        'If EditStatus = True And Not (IsNothing(objSalesmanHeader.Image)) Then
        '    lblRemoveImage.Visible = True
        '    lblRemoveImage.Enabled = True
        'Else
        '    lblRemoveImage.Visible = False
        '    lblRemoveImage.Enabled = False
        'End If

        If Not IsNothing(objSalesmanHeader.Image) Then
            sessHelper.SetSession(sessImageByte, objSalesmanHeader.Image)
        End If

        If Not IsNothing(objSalesmanAdditionalInfo.KtpImagePath) Then
            sessHelper.SetSession(sessKtpPath, objSalesmanAdditionalInfo.KtpImagePath)
        End If


        sessHelper.SetSession("vsSalesmanHeader", objSalesmanHeader)
        With objSalesmanHeader
            'Dim objDealer As Dealer = New DealerFacade(User).Retrieve(.Dealer.ID)
            'If Not IsNothing(Request.QueryString("SalesResign")) Then
            lblSalesmanCode.Text = .SalesmanCode
            'Else
            'lblSalesmanCode.Text = ""
            'End If

            lblName.Text = .Name
            lblPlaceOfBirth.Text = .PlaceOfBirth
            lblDateOfBirth.Text = .DateOfBirth
            lblStartWork.Text = .HireDate

            Dim objSalProf As SalesmanProfile
            Dim arlSalProf As New ArrayList
            Dim criSalProf As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criSalProf.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, .ID))
            criSalProf.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.InSet, "(29,33)"))

            arlSalProf = New SalesmanProfileFacade(User).Retrieve(criSalProf)
            objSalProf = CType(arlSalProf.Item(0), SalesmanProfile)

            txtName.Text = .Name
            txtPlaceOfBirth.Text = .PlaceOfBirth
            ICDateOfBirth.Value = .DateOfBirth
            ICStartWork.Value = .HireDate
            ddlIndicator.SelectedValue = .SalesIndicator
            lblIndicator.Text = ddlIndicator.SelectedItem.Text
            ddlGender.SelectedValue = .Gender
            lblGender.Text = ddlGender.SelectedItem.Text
            ddlMarriedStatus.SelectedValue = .MarriedStatus
            ddlJobPositionDesc.SelectedValue = .JobPosition.ID
            lblMarriedStatus.Text = ddlMarriedStatus.SelectedItem.Text
            lblAlamat.Text = .Address
            lblKota.Text = .City



            If .LeaderId > 0 Then
                Dim objSuperior As SalesmanHeader = objSalesmanHeaderFacade.Retrieve(.LeaderId)
                txtSuperior.Text = objSuperior.SalesmanCode
                txtSuperiorName.Text = objSuperior.Name
            End If

            Dim objSalesmanHeaderLead As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(.LeaderId)

            txtAlamat.Text = .Address

            ' mengambil data propinsi yg bersangkutan
            Dim objCity As City
            Dim objCityFacade As CityFacade = New CityFacade(User)
            If .City.Trim <> "" Then
                objCity = objCityFacade.Retrieve(.City, 1)
                ddlPropinsi.SelectedValue = objCity.Province.ID
                ddlPropinsi_SelectedIndexChanged(Me, System.EventArgs.Empty)
                lblPropinsi.Text = ddlPropinsi.SelectedItem.Text
                ddlKota.SelectedValue = objCity.ID
            Else
                ddlPropinsi.SelectedIndex = 0
                ddlKota.SelectedIndex = 0
            End If

            ddlStatus.SelectedValue = .Status.ToString
            lblStatus.Text = ddlStatus.SelectedItem.Text





            If IsNothing(Request.QueryString("SalesResign")) Then
                ICEndWork.Value = .ResignDate
                If .ResignDate.Year < 1900 Then
                    lblEndWork.Text = "-"
                Else
                    lblEndWork.Text = .ResignDate
                End If
            Else
                ICEndWork.Value = Date.Now
                lblEndWork.Text = String.Empty
            End If

            If .JobPosition.Code <> "CSO" Then
                rowKategoriTim.Visible = True
            End If

            Dim salesmanProfileKategoriTim As SalesmanProfile = GetExistingProfileKategoriTim(objSalesmanHeader)
            If Not salesmanProfileKategoriTim Is Nothing Then
                Dim selectedTim As ListItem = ddlKategoriTim.Items.FindByText(salesmanProfileKategoriTim.ProfileValue)
                If Not selectedTim Is Nothing Then
                    ddlKategoriTim.ClearSelection()
                    selectedTim.Selected = True
                End If
            End If

        End With

        Me.btnSimpan.Enabled = EditStatus
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            EnablingControl(EditStatus)
        Else
            EnablingControl(False)

            'CR Part Employee By jon, assigne to mr ali
            ddlJobPositionDesc.Enabled = True
            ddlSalesmanLevel.Enabled = True

            ddlStatus.Enabled = EditStatus
            btnBatal.Enabled = False
            RequiredFieldValidator1.Enabled = False
            'RequiredFieldValidator2.Enabled = False
            'RequiredFieldValidator3.Enabled = False
            'Requiredfieldvalidator8.Enabled = False

        End If

    End Sub

    Private Sub EnablingControl(ByVal isEnabled As Boolean)

        'txtName.Enabled = isEnabled
        txtAlamat.Enabled = isEnabled
        txtPlaceOfBirth.Enabled = isEnabled
        'ICDateOfBirth.Enabled = isEnabled
        ddlGender.Enabled = isEnabled
        ddlMarriedStatus.Enabled = isEnabled
        photoSrc.Disabled = isEnabled
        ddlPropinsi.Enabled = isEnabled
        ddlKota.Enabled = isEnabled
        ddlJobPositionDesc.Enabled = isEnabled
        ddlSalesmanLevel.Enabled = isEnabled
        'ddlGrade.Enabled = isEnabled
        'txtSalary.Enabled = isEnabled

        ddlStatus.Enabled = False 'isEnabled
        ICStartWork.Enabled = isEnabled
        ICDateOfBirth.Enabled = isEnabled
        ddlSalesmanLevel.Enabled = isEnabled

        ValAtasan.Enabled = isEnabled

        ICEndWork.Enabled = False



        'dtgTraining.Columns(4).Visible = isEnabled  ' kolom aksi
        'dtgTraining.ShowFooter = isEnabled
    End Sub

    Private Sub InsertImage()
        Dim oPhisingGuardImage As SalesmanHeader = New SalesmanHeader
        If photoSrc.PostedFile.FileName = String.Empty Then
            MessageBox.Show("Tidak ada file gambar")
            Return  '-- No photo defined
        End If

        '-- Split into array of strings. The last element is the file name
        Dim sFileNames() As String = photoSrc.PostedFile.FileName.Split("\")
        Dim sFileName As String = sFileNames(sFileNames.Length - 1)

        Try
            Dim imageFile As Byte()
            imageFile = uploadFile()

            oPhisingGuardImage.Image = imageFile
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub VisibleControl(ByVal blnVisible As Boolean, Optional ByVal isNewEmployee As Boolean = False)
        txtName.Visible = blnVisible
        txtPlaceOfBirth.Visible = blnVisible
        ICDateOfBirth.Visible = blnVisible
        ddlGender.Visible = blnVisible
        ddlMarriedStatus.Visible = blnVisible
        txtAlamat.Visible = blnVisible
        ddlPropinsi.Visible = blnVisible
        ddlKota.Visible = blnVisible
        'ddlSalesmanLevel.Visible = blnVisible
        ddlJobPositionDesc.Visible = blnVisible
        ddlKategoriTim.Visible = blnVisible
        'ddlGrade.Visible = blnVisible

        ICStartWork.Visible = blnVisible
        ICEndWork.Visible = blnVisible
        ddlStatus.Visible = blnVisible
        ddlIndicator.Visible = blnVisible
        'txtSalary.Visible = blnVisible
        '---
        lblName.Visible = Not blnVisible
        lblPlaceOfBirth.Visible = Not blnVisible
        lblDateOfBirth.Visible = Not blnVisible
        lblGender.Visible = Not blnVisible
        lblMarriedStatus.Visible = Not blnVisible
        lblAlamat.Visible = Not blnVisible
        lblPropinsi.Visible = Not blnVisible
        lblKota.Visible = Not blnVisible
        lblSalesmanLevel.Visible = Not blnVisible
        lblJobPositionDesc.Visible = Not blnVisible

        'lblSalary.Visible = Not blnVisible

        lblStartWork.Visible = Not blnVisible
        lblEndWork.Visible = Not blnVisible
        lblStatus.Visible = Not blnVisible
        lblIndicator.Visible = Not blnVisible
        btnGenerateCode.Visible = blnVisible
        ' yang status konfirmasi hanya bs simpan, tp change status
        If Not IsNothing(Request.QueryString("Konfirmasi")) Then
            If blnVisible = True Then
                ddlStatus.SelectedValue = EnumSalesmanStatus.SalesmanStatus.Konfirmasi
                ddlStatus.Enabled = False
            End If
        Else
            If ddlStatus.SelectedValue = CType(EnumSalesmanStatus.SalesmanStatus.Konfirmasi, String) Then
                ddlStatus.Enabled = False
            End If
        End If
    End Sub

    Private Function UploadFile() As Byte()
        Dim nResult() As Byte

        Try
            ' Dim filePic As HttpPostedFile = CType(sessHelper.GetSession("IMAGEPATH"), HttpPostedFile)
            If IsValidPhoto(photoSrc) Then
                Dim inStream As System.IO.Stream = photoSrc.PostedFile.InputStream()
                Dim ByteRead(SalesmanHeader.MAX_PHOTO_SIZE) As Byte
                Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, SalesmanHeader.MAX_PHOTO_SIZE)
                If ReadCount = 0 Then
                    Throw New InvalidConstraintException(SR.DataNotFound("Photo"))
                End If
                ReDim nResult(ReadCount)
                Array.Copy(ByteRead, nResult, ReadCount)
                sessHelper.SetSession(sessImageByte, nResult)
            Else
                sessHelper.SetSession(sessImageByte, Nothing)
                'Throw New DataException("Bukan file gambar atau Ukuran file > " & _
                '                        CType(SalesmanHeader.MAX_PHOTO_SIZE / 1024, String) & "KB")
                MessageBox.Show("Bukan file gambar atau Ukuran file > " & _
                                        CType(SalesmanHeader.MAX_PHOTO_SIZE / 1024, String) & "KB")

            End If
        Catch
            'Throw
        End Try


        Return nResult
    End Function

    Private Function UploadKtp() As String
        Dim nResult As String

        Try
            ' Dim filePic As HttpPostedFile = CType(sessHelper.GetSession("IMAGEPATH"), HttpPostedFile)
            If IsValidPhoto(ktpSrc) Then
                Dim extension As String = Path.GetExtension(ktpSrc.PostedFile.FileName)  '-- Source file name
                Dim KtpPath As String = KTB.DNet.Lib.WebConfig.GetValue("TraineeKTPPath") & "\" & Guid.NewGuid().ToString().Substring(0, 5) & extension      '-- Destination file
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KtpPath
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        ktpSrc.PostedFile.SaveAs(DestFile)
                        nResult = KtpPath
                        sessHelper.SetSession(sessKtpPath, KtpPath)
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            Else
                sessHelper.SetSession(sessKtpPath, Nothing)
                'Throw New DataException("Bukan file gambar atau Ukuran file > " & _
                '                        CType(SalesmanHeader.MAX_PHOTO_SIZE / 1024, String) & "KB")
                MessageBox.Show("Bukan file gambar atau Ukuran file > " & _
                                        CType(SalesmanHeader.MAX_PHOTO_SIZE / 1024, String) & "KB")

            End If
        Catch
            'Throw
        End Try


        Return nResult
    End Function

    ' untuk keperluan penyimpanan photo
    Private Function IsValidPhoto(ByVal file As HtmlInputFile) As Boolean
        Dim containImage As Boolean = (file.PostedFile.ContentType.ToUpper.IndexOf(SalesmanHeader.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.PostedFile.ContentLength <= SalesmanHeader.MAX_PHOTO_SIZE)
        Return (containImage AndAlso sizeValid)
    End Function

    Private Sub SetSession()
        sessHelper.SetSession("SortColHistory", "DateIn")
        sessHelper.SetSession("SortDirectionHistory", Sort.SortDirection.DESC)

        sessHelper.SetSession("SortColArea", "SalesmanArea.AreaCode")
        sessHelper.SetSession("SortDirectionArea", Sort.SortDirection.ASC)
        sessHelper.SetSession("SortColExperience", "YearExperience")
        sessHelper.SetSession("SortDirectionExperience", Sort.SortDirection.ASC)
        sessHelper.SetSession("SortColPrestasi", "")
        sessHelper.SetSession("SortDirectionPrestasi", Sort.SortDirection.ASC)
        sessHelper.SetSession("SortColTarget", "")
        sessHelper.SetSession("SortDirectionTarget", Sort.SortDirection.ASC)
        sessHelper.SetSession("SortColTraining", "")
        sessHelper.SetSession("SortDirectionTraining", Sort.SortDirection.ASC)
        sessHelper.SetSession("RemovePic", False)
    End Sub

    Private Sub SendEmailFromTemplate(ByVal EmailFile As String, ByVal sTo As String, ByVal sCC As String, ByVal sSubject As String, ByVal sMessage() As String)
        Dim appConfigFacade As New AppConfigFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailAdmin As String = String.Empty
        Dim emailFrom As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_FROM_CS).Value
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(EmailFile)
        Dim szEmailFormat As String = sr.ReadToEnd()
        sr.Close()
        Dim szEmailContent As String = String.Format(szEmailFormat, sMessage)
        If Not IsNothing(sCC) AndAlso sCC.Trim() <> "" AndAlso sCC.EndsWith(";") = False AndAlso Not IsNothing(emailAdmin) AndAlso emailAdmin <> "" Then
            emailAdmin = ";" & emailAdmin
        End If

        ObjEmail.sendMail(sTo, sCC, emailAdmin, emailFrom, sSubject, Mail.MailFormat.Html, szEmailContent)
    End Sub

    Private Async Function SendEmail(ByVal objSalesmanHeader As SalesmanHeader, ByVal objUser As UserInfo, ByVal bStatus As Boolean) As Task ' bStatus = New (true) or Update(false) Employee
        Dim appConfigFacade As New AppConfigFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_FROM_CS).Value
        Dim emailTo As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_TO_CS).Value

        Dim UrlPartEmpGenerate As String = KTB.DNet.Lib.WebConfig.GetValue("UrlCSEmpGenerate")
        Dim urlPartEmpList As String = KTB.DNet.Lib.WebConfig.GetValue("UrlCSEmpList")

        Try
            Await Task.Run(Sub()
                               Dim valueEmail As String
                               If bStatus Then

                                   Dim subject As String = "[MMKSI-DNet] Call Center - Request Employee CS Code "
                                   Dim Dir As String = Server.MapPath("") & "\..\DataFile\EmailTemplate\Penambahan_Employee_CS.htm"
                                   Try
                                       Dim sContents() As String = { _
                                       objSalesmanHeader.Dealer.DealerCode, _
                                       objSalesmanHeader.Dealer.SearchTerm1, _
                                       objSalesmanHeader.Name, _
                                       objSalesmanHeader.JobPosition.Description, _
                                       objSalesmanHeader.HireDate.ToString("dd MMM yyyy"), _
                                       objUser.Dealer.DealerCode, _
                                       objUser.UserName}

                                       Me.SendEmailFromTemplate(Dir, emailTo, String.Empty, subject, sContents)
                                   Catch
                                   End Try
                                   'valueEmail = GenerateEmailNewEmployee(objSalesmanHeader, objUser, UrlPartEmpGenerate)
                                   'Dim GetStringTask As String
                                   'ObjEmail.sendMail(emailTo, "", emailFrom, , Mail.MailFormat.Html, valueEmail)

                               Else
                                   Dim subject As String = "[MMKSI-DNet] Call Center - Update Employee CS Code "
                                   Dim Dir As String = Server.MapPath("") & "\..\DataFile\EmailTemplate\Perubahan_Employee_CS.htm"
                                   Try
                                       Dim sContents() As String = { _
                                       objSalesmanHeader.Dealer.DealerCode, _
                                       objSalesmanHeader.Dealer.SearchTerm1, _
                                       objSalesmanHeader.SalesmanCode, _
                                       objSalesmanHeader.Name, _
                                       objSalesmanHeader.JobPosition.Description, _
                                       objUser.Dealer.DealerCode, _
                                       objUser.UserName}

                                       Me.SendEmailFromTemplate(Dir, emailTo, String.Empty, subject, sContents)
                                   Catch
                                   End Try
                                   'valueEmail = GenerateEmailUpdateEmployee(objSalesmanHeader, objUser, urlPartEmpList)
                                   'ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet] Call Center - Update Employee CS Code ", Mail.MailFormat.Html, valueEmail)
                               End If
                           End Sub)
        Catch ex As Exception
            Throw New Exception("Failed Sending Email : " & ex.Message)
        End Try
    End Function

    Private Function GenerateEmailNewEmployee(ByVal objSalesmanHeader As SalesmanHeader, objuser As UserInfo, ByVal urlRequest As String) As String

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder("")
        sb.Append("<FONT face=Arial size=1>")
        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 align=center><b>Request CS Employee Code</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50>")
        sb.Append("Dengan hormat,&nbsp;")
        sb.Append("<br><br>Berikut data CS Employee baru :")
        sb.Append("</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=10></td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")
        sb.Append("<tr>")
        sb.Append("<td width='35%'>Dealer</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.Dealer.DealerCode & " / " & objSalesmanHeader.Dealer.SearchTerm1 & "</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width='35%'>Nama</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.Name & "</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width='35%'>Job Posisi</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.JobPosition.Description & "</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width='35%'>Tanggal Masuk</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.HireDate.ToString("dd MMM yyyy") & "</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width='35%'>Diajukan oleh</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objuser.Dealer.DealerCode & " - " & objuser.UserName & "</b></td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>Untuk registrasi data CS Employee diatas, dapat diakses pada aplikasi D-NET</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("</FONT>")

        Return sb.ToString

    End Function

    Private Function GenerateEmailUpdateEmployee(ByVal objSalesmanHeader As SalesmanHeader, objuser As UserInfo, ByVal urlPartEmpList As String) As String
        Dim commFuntion As New CommonFunction
        Try
            'Dim oSalesPartHistory As SalesmanPartHistory = CType(sessHelper.GetSession("SALESHISTORY"), SalesmanPartHistory)
            Dim sb As StringBuilder = New StringBuilder(String.Empty)
            sb.Append("<FONT face=Arial size=1>")
            sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
            sb.Append("<tr>")
            sb.Append("<td colspan=3 align=center><b>Perubahan Data Jabatan CS Employee </b></td>")
            sb.Append("</tr>")
            sb.Append("<tr>")
            sb.Append("<td colspan=3 height=50></td>")
            sb.Append("</tr>")
            sb.Append("<tr>")
            sb.Append("<td colspan=3 height=50>")
            sb.Append("Dengan hormat,&nbsp;")
            sb.Append("<br><br>Berikut data CS Employee yang berubah  :")
            sb.Append("</td>")
            sb.Append("</tr>")
            sb.Append("<tr>")
            sb.Append("<td colspan=3 height=10></td>")
            sb.Append("</tr>")
            sb.Append("<tr>") 'Dealer
            sb.Append("<td height=10 width='25%'>Nama Dealer</td>")
            sb.Append("<td height=10 width='5%'>:</td>")
            sb.Append("<td height=10 width='75%'>" & objSalesmanHeader.Dealer.DealerCode & " / " & objSalesmanHeader.Dealer.SearchTerm1 & "</td>")
            sb.Append("</tr>")
            sb.Append("<tr>") 'Nama
            sb.Append("<td height=10 width='25%'>Nama Employee</td>")
            sb.Append("<td height=10 width='5%'>:</td>")
            sb.Append("<td height=10 width='75%'>" & objSalesmanHeader.SalesmanCode & " / " & objSalesmanHeader.Name & "</td>")
            sb.Append("</tr>")

            sb.Append("<tr>") 'Posisi
            sb.Append("<td height=10 width='25%'>Job Posisi</td>")
            sb.Append("<td height=10 width='5%'>:</td>")
            sb.Append("<td height=10 width='75%'>" & objSalesmanHeader.JobPosition.Description & "</td>")
            sb.Append("</tr>")

            sb.Append("<tr>") 'diajukan
            sb.Append("<td height=10 width='25%'>Diajukan oleh</td>")
            sb.Append("<td height=10 width='5%'>:</td>")
            sb.Append("<td height=10 width='75%'>" & objuser.Dealer.DealerCode & " - " & objuser.UserName & "</td>")
            sb.Append("</tr>")

            sb.Append("<tr>")
            sb.Append("<td colspan=5 height=10></td>")
            sb.Append("</tr>")

            sb.Append("</table>")
            sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")

            sb.Append("<tr>")
            sb.Append("<td width='100%'>Untuk menyetujui atau menolak perubahan data CS Employee diatas, dapat diakses pada aplikasi D-NET</td>")
            sb.Append("</tr>")

            sb.Append("<tr>")
            sb.Append("<td >&nbsp;</td>")
            sb.Append("</tr>")

            sb.Append("<tr>")
            sb.Append("</tr>")
            sb.Append("</table>")
            sb.Append("</FONT>")

            Return sb.ToString
        Catch ex As Exception
            Return String.Empty
        End Try


    End Function

    Private Function IsKTPExist() As Boolean
        Dim ktp As String = ""
        Dim objExist As SalesmanHeader
        Dim bolReturn As Boolean = False


        Dim objRenderPanel As RenderingProfile = New RenderingProfile
        ' sebelum melakukan update, pastikan data ProfileHeaderToGroup sdh ada / belum
        Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)

        'Dim arrTmp As ArrayList
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, objProfileGroup.ID))
        'arrTmp = New ProfileHeaderToGroupFacade(User).RetrieveByCriteria(criterias)

        Dim al As ArrayList = objRenderPanel.RetrieveProfileValue(Me, objProfileGroup, CType(EnumProfileType.ProfileType.CS, Short), User)

        For Each row As SalesmanProfile In al
            If row.ProfileHeader.ID = 29 Then
                If row.ProfileValue.Length <> 16 Then
                    MessageBox.Show("Nomor KTP Tidak Valid")
                    Return True
                End If
                ktp = row.ProfileValue
            ElseIf row.ProfileHeader.ID = 26 Then
                ' Validate email address
                Dim emailAddressValidationMessage As String
                emailAddressValidationMessage = msgErrorEmailValidation(row.ProfileValue)
                If (emailAddressValidationMessage <> "") Then
                    MessageBox.Show(emailAddressValidationMessage)
                    Return True
                End If
            ElseIf row.ProfileHeader.Code = "NO_HP" Then
                If Left(row.ProfileValue, 2) <> "08" Or row.ProfileValue.Length < 9 Or row.ProfileValue.Length > 15 Then
                    MessageBox.Show("Format Nomor HP Tidak Valid")
                    Return True
                End If
            ElseIf row.ProfileHeader.Code = "USER_DNET" Then
                If row.ProfileValue.Trim() <> String.Empty Then
                    Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
                    Dim critUserProfile As New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critUserProfile.opAnd(New Criteria(GetType(UserInfo), "OrganizationID", MatchType.Exact, objDealer.ID))
                    critUserProfile.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, row.ProfileValue.Trim()))

                    Dim arlResult As ArrayList = New UserInfoFacade(User).Retrieve(critUserProfile)
                    If arlResult.Count = 0 Then
                        MessageBox.Show("User DNET tidak terdaftar")
                        Return True
                    End If
                End If
            End If

        Next

        Dim critKTP As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
        critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, ktp))

        Dim sortCollKTP As SortCollection = New SortCollection
        sortCollKTP.Add(New Sort(GetType(SalesmanProfile), "CreatedTime", Sort.SortDirection.DESC))

        Dim ktpValue As String

        Dim arrKTP As ArrayList = New SalesmanProfileFacade(User).Retrieve(critKTP, sortCollKTP)
        If arrKTP.Count > 0 Then
            ktp = ktp.Trim.Replace(".", "")
            For Each sp As SalesmanProfile In arrKTP
                ktpValue = sp.ProfileValue.Trim.Replace(".", "")
                If (ktpValue = ktp) Then
                    objExist = sp.SalesmanHeader
                    Exit For
                End If
            Next
        End If

        If (objExist IsNot Nothing) Then
            If objExist.SalesIndicator = 4 Then
                If objExist.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String) Then
                    Dim obJsalprov As SalesmanProfile

                    sessHelper.SetSession("CSTeam", objExist)
                    MessageBox.Show("Nomor KTP yang anda masukkan telah terdaftar di dealer " & objExist.Dealer.DealerName & " )\n Silakan gunakan fitur referensi menggunakan kode CS " & objExist.SalesmanCode)
                    ' MessageBox.Confirm("Nomor KTP atas nama " & objExist.Name & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & ") - " & objExist.SalesmanCode & " di dealer " & objExist.Dealer.DealerName & " )\n Apakah anda akan tetap menginput data?", "hdnVal")
                Else
                    MessageBox.Show("Nomor KTP atas nama  " & objExist.Name & " - " & objExist.SalesmanCode & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & "), \n masih aktif terdaftar di dealer " & objExist.Dealer.DealerName)
                End If
                Return True
            End If

        End If

        Return bolReturn
    End Function

    Private Function IsKTPExistUpdate() As Boolean
        Dim ktp As String = ""
        Dim objExist As SalesmanHeader
        Dim bolReturn As Boolean = False


        Dim objRenderPanel As RenderingProfile = New RenderingProfile
        ' sebelum melakukan update, pastikan data ProfileHeaderToGroup sdh ada / belum
        Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)

        'Dim arrTmp As ArrayList
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, objProfileGroup.ID))
        'arrTmp = New ProfileHeaderToGroupFacade(User).RetrieveByCriteria(criterias)

        Dim al As ArrayList = objRenderPanel.RetrieveProfileValue(Me, objProfileGroup, CType(EnumProfileType.ProfileType.CS, Short), User)

        For Each row As SalesmanProfile In al
            If row.ProfileHeader.ID = 29 Then
                If row.ProfileValue.Length <> 16 Then
                    MessageBox.Show("Nomor KTP Tidak Valid")
                    Return True
                End If
                ktp = row.ProfileValue
            ElseIf row.ProfileHeader.ID = 26 Then
                ' Validate email address
                Dim emailAddressValidationMessage As String
                emailAddressValidationMessage = msgErrorEmailValidation(row.ProfileValue)
                If (emailAddressValidationMessage <> "") Then
                    MessageBox.Show(emailAddressValidationMessage)
                    Return True
                End If
            ElseIf row.ProfileHeader.Code = "NO_HP" Then
                If Left(row.ProfileValue, 2) <> "08" Or row.ProfileValue.Length < 9 Or row.ProfileValue.Length > 15 Then
                    MessageBox.Show("Format Nomor HP Tidak Valid")
                    Return True
                End If
            ElseIf row.ProfileHeader.Code = "USER_DNET" Then
                If row.ProfileValue.Trim() <> String.Empty Then
                    Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
                    Dim critUserProfile As New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critUserProfile.opAnd(New Criteria(GetType(UserInfo), "OrganizationID", MatchType.Exact, objDealer.ID))
                    critUserProfile.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, row.ProfileValue.Trim()))

                    Dim arlResult As ArrayList = New UserInfoFacade(User).Retrieve(critUserProfile)
                    If arlResult.Count = 0 Then
                        MessageBox.Show("User DNET tidak terdaftar")
                        Return True
                    End If
                End If
            End If

        Next

        Dim critKTP As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
        critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, ktp))

        Dim sortCollKTP As SortCollection = New SortCollection
        sortCollKTP.Add(New Sort(GetType(SalesmanProfile), "CreatedTime", Sort.SortDirection.DESC))

        Dim ktpValue As String

        Dim arrKTP As ArrayList = New SalesmanProfileFacade(User).Retrieve(critKTP, sortCollKTP)
        If arrKTP.Count > 0 Then
            ktp = ktp.Trim.Replace(".", "")
            For Each sp As SalesmanProfile In arrKTP
                ktpValue = sp.ProfileValue.Trim.Replace(".", "")
                If (ktpValue = ktp) Then
                    objExist = sp.SalesmanHeader
                    Exit For
                End If
            Next
        End If



        Return bolReturn
    End Function

    Private Sub RenderProfilePanel(ByVal objSalesmanHeader As SalesmanHeader, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel)
        Dim isReadOnly As Boolean

        If Request.QueryString("ID") = String.Empty Or Request.QueryString("ID") = "" Then
            isReadOnly = False
        Else
            If Request.QueryString("edit") = String.Empty Or Request.QueryString("edit") = "" Then
                isReadOnly = True
            Else
                Dim objDealer As Dealer
                objDealer = Session.Item("DEALER")
                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    isReadOnly = True
                Else
                    isReadOnly = False
                End If
            End If
        End If

        Dim objRenderPanel As RenderingProfile = New RenderingProfile(isReadOnly)

        If Not objSalesmanHeader Is Nothing Then
            objRenderPanel.GeneratePanel(objSalesmanHeader.ID, objPanel, objGroup, profileType, User, isReadOnly, False)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User, isReadOnly, False)
        End If
    End Sub

    'Private Sub BindGrvTraining()
    '    Dim objSalTrai As New SalesmanTraining
    '    Dim arlSalTri As New ArrayList
    '    Dim strID As Integer = 0
    '    Dim strSalID As Integer = 0
    '    Dim criSal As New CriteriaComposite(New Criteria(GetType(SalesmanTraining), "ID", MatchType.Exact, strID))
    '    criSal.opAnd(New Criteria(GetType(SalesmanTraining), "RowStatus", MatchType.Exact, CShort(DBRowStatus.Active)))

    '    With arlSalTri
    '        .Add(1)
    '        .Add("test")
    '        .Add("test")
    '    End With

    '    grvSalesmanTraining.DataSource = arlSalTri
    '    grvSalesmanTraining.DataBind()
    'End Sub

    Private Sub BindTrainingCRUD(ByVal idxPage As Integer, ByVal objSalTrain As SalesmanTraining, ByVal method As String)
        'Dim totalRow As Integer = 0
        'Dim dtbl As DataTable

        'If ViewState("dtblTraining") IsNot Nothing Then
        '    dtbl = ViewState("dtblTraining")
        'Else
        '    dtbl = New DataTable
        '    dtbl.Columns.Add("ID", GetType(Integer))
        '    dtbl.Columns.Add("TrainingModule", GetType(String))
        '    dtbl.Columns.Add("TrainingPlaceAndDate", GetType(String))
        '    dtbl.Columns.Add("TrainingProvider", GetType(String))
        '    dtbl.Columns.Add("Metode", GetType(String))

        'End If


        'dtbl.Rows.Add(objSalTrain.ID, objSalTrain.TrainingModule, objSalTrain.TrainingPlaceAndDate, objSalTrain.TrainingProvider, method)

        'ViewState("dtblTraining") = dtbl


        'Select Case method
        '    Case "Add"

        '        For Each row As DataRow In dtbl.Rows
        '            objSalTrain = New SalesmanTraining
        '            objSalTrain.TrainingProvider = row.Item("TrainingProvider")
        '            objSalTrain.TrainingModule = row.Item("TrainingModule")
        '            objSalTrain.TrainingPlaceAndDate = row.Item("TrainingPlaceAndDate")
        '            objSalTrain.ID = row.Item("ID")
        '            arrTraining.Add(objSalTrain)
        '        Next



        '        dtgTraining.CurrentPageIndex = idxPage
        '        dtgTraining.DataSource = arrTraining
        '        dtgTraining.VirtualItemCount = totalRow
        '        dtgTraining.DataBind()

        '    Case "Edit"
        '        For Each row As DataRow In dtbl.Rows
        '            objSalTrain = New SalesmanTraining
        '            objSalTrain.TrainingProvider = row.Item("TrainingProvider")
        '            objSalTrain.TrainingModule = row.Item("TrainingModule")
        '            objSalTrain.TrainingPlaceAndDate = row.Item("TrainingPlaceAndDate")
        '            objSalTrain.ID = row.Item("ID")
        '            arrTraining.Add(objSalTrain)
        '        Next

        '        dtgTraining.CurrentPageIndex = idxPage
        '        dtgTraining.DataSource = arrTraining
        '        dtgTraining.VirtualItemCount = totalRow
        '        dtgTraining.DataBind()

        '    Case "Delete"
        '        For Each row As DataRow In dtbl.Rows
        '            objSalTrain = New SalesmanTraining
        '            objSalTrain.TrainingProvider = row.Item("TrainingProvider")
        '            objSalTrain.TrainingModule = row.Item("TrainingModule")
        '            objSalTrain.TrainingPlaceAndDate = row.Item("TrainingPlaceAndDate")
        '            objSalTrain.ID = row.Item("ID")
        '            arrTraining.Add(objSalTrain)
        '        Next

        '        dtgTraining.CurrentPageIndex = idxPage
        '        dtgTraining.DataSource = arrTraining
        '        dtgTraining.VirtualItemCount = totalRow
        '        dtgTraining.DataBind()
        'End Select
    End Sub

    Private Sub BindTraining(ByVal idxPage As Integer)
        'Dim totalRow As Integer = 0
        'Dim dtbl As DataTable

        'Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanTraining), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(SalesmanTraining), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))

        'arrTraining = New SalesmanTrainingFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dtgTraining.PageSize, totalRow, _
        ' sessHelper.GetSession("SortColTraining"), sessHelper.GetSession("SortDirectionTraining"))

        'dtbl = New DataTable
        'dtbl.Columns.Add("No", GetType(Integer))
        'dtbl.Columns.Add("TrainingModule", GetType(String))
        'dtbl.Columns.Add("TrainingPlaceAndDate", GetType(String))
        'dtbl.Columns.Add("TrainingProvider", GetType(String))
        'Dim x As Integer = 1
        'For Each row As SalesmanTraining In arrTraining
        '    dtbl.Rows.Add(x, row.TrainingModule, row.TrainingPlaceAndDate, row.TrainingProvider)
        '    x = x + 1
        'Next

        'dtbl = CType(ViewState("dtblTraining"), DataTable)

        'dtgTraining.CurrentPageIndex = idxPage

        'If arrTraining.Count = 0 Then
        '    dtgTraining.DataSource = arrTraining
        'Else
        '    dtgTraining.DataSource = dtbl
        'End If

        'dtgTraining.VirtualItemCount = totalRow
        'dtgTraining.DataBind()

    End Sub

    Private Sub BindDgTraining(ByVal idxPage As Integer)
        'Dim totalRow As Integer = 0

        'Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanTraining), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(SalesmanTraining), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))

        'arrTraining = New SalesmanTrainingFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dtgTraining.PageSize, totalRow, _
        ' sessHelper.GetSession("SortColTraining"), sessHelper.GetSession("SortDirectionTraining"))

        'dtgTraining.CurrentPageIndex = idxPage
        'dtgTraining.DataSource = arrTraining
        'dtgTraining.VirtualItemCount = totalRow
        'dtgTraining.DataBind()

    End Sub

    Private Sub CreateCriTraining(ByVal criterias As CriteriaComposite)
        criterias.opAnd(New Criteria(GetType(SalesmanTraining), "SalesmanHeader.ID", MatchType.Exact, CInt(Request.QueryString("id"))))
    End Sub

#End Region

#Region "EventHandlers"


    'Private Sub lbtnAddTrain_Click(sender As Object, e As EventArgs) Handles lbtnAddTrain.Click
    '    bindGrv(0)
    'End Sub

    Private Sub FrmInputCSTeam_Init(sender As Object, e As EventArgs) Handles Me.Init
        SetProfile()
        If Request.QueryString("ID") = String.Empty Then
            RenderProfilePanel(Nothing, New ProfileGroupFacade(User).Retrieve(strCurrProfile), EnumProfileType.ProfileType.CS, Panel1)
        Else
            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("ID")))
            RenderProfilePanel(objSalesmanHeader, New ProfileGroupFacade(User).Retrieve(strCurrProfile), EnumProfileType.ProfileType.CS, Panel1)
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditTeam_Privilege)
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.view, SR.TrainingCsViewTeam_Privilege)
        helpers.Privilage()
        'SmartNavigation = True
        If Val(sessHelper.GetSession("IsInsertSuccess")) = 1 Then
            IsInsertSuccess.Value = "1"
            sessHelper.SetSession("IsInsertSuccess", "")
        Else
            IsInsertSuccess.Value = ""
            sessHelper.SetSession("IsInsertSuccess", "")
        End If

        If Not IsPostBack Then
            BindDropDown()
            SetSession()
            Initialize()
            objDealerTmp = sessHelper.GetSession("DEALER")
            If CType(Request.QueryString("id"), Integer) < 0 Or Request.QueryString("id") = String.Empty Then
                _salesmanHeader = New SalesmanHeader
                ViewState("vsProcess") = "Insert"
                btnGenerateCode.Visible = False
                btnRequestID.Visible = False
                lblRemoveImage.Visible = False
                ddlStatus.SelectedValue = 0
                ddlStatus.Enabled = False
                btnBatal.Visible = True
                Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
                lblDealerCode.Text = objuser.Dealer.DealerCode
                lblDealerName.Text = objuser.Dealer.DealerName

                trDownloadAppointmentLetter.Visible = False
                trUploadAppointmentLetter.Visible = True

                If blnSaved Then
                    MessageBox.Show(SR.SaveSuccess & ", Silakan melengkapi data lainnya")
                End If
            Else

                If Request.QueryString("edit") <> String.Empty Then
                    If Request.QueryString("edit") = "true" Then
                        ViewState("vsProcess") = "Edit"
                        View(CInt(Request.QueryString("id")), True)
                        VisibleControl(True)
                        BindTraining(0)

                        trDownloadAppointmentLetter.Visible = True
                        trUploadAppointmentLetter.Visible = True
                    Else
                        ViewState("vsProcess") = "Insert"
                    End If
                Else
                    If Request.QueryString("view") = "true" Then
                        ViewState("vsProcess") = "View"
                        View(CInt(Request.QueryString("id")), True)
                        VisibleControl(False)
                        btnSimpan.Enabled = False
                        trDownloadAppointmentLetter.Visible = True
                        trUploadAppointmentLetter.Visible = False
                        'If Me.GetDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
                        '    Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
                        '    If objSalesmanHeader.IsRequestID = EnumSalesmanIsRequest.SalesmanIsRequest.Sudah_Request And _
                        '       objSalesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Baru, String) Then
                        '        btnGenerateCode.Visible = True
                        '        btnGenerateCode.Enabled = True
                        '    End If
                        'End If
                    End If
                End If

                If Request.QueryString("Konfirmasi") <> String.Empty Then
                    ViewState("vsProcess") = "Konfirmasi"
                    View(CInt(Request.QueryString("id")), True)
                    'BindDgTraining(0)
                End If

                btnBatal.Visible = True
                sessHelper.SetSession("HeaderID", Request.QueryString("ID"))
                _salesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("ID")))
                If _salesmanHeader.IsRequestID = EnumSalesmanIsRequest.SalesmanIsRequest.Sudah_Request Then
                    txtName.Enabled = False
                    ICDateOfBirth.Enabled = False
                    btnRequestID.NonVisible()
                    DisableKTPField()
                    If Me.GetDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
                        If _salesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Baru, String) Then
                            btnGenerateCode.Enabled = True
                            btnGenerateCode.Visible = True
                        Else
                            btnGenerateCode.Visible = False
                        End If
                    Else
                        btnGenerateCode.Visible = False
                    End If
                Else
                    If Me.GetDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
                        btnRequestID.NonVisible()
                    End If
                    btnGenerateCode.Visible = False
                End If
            End If
        Else
            'If Request.QueryString("Konfirmasi") <> String.Empty Then
            '    ViewState("vsProcess") = "Konfirmasi"
            '    View(CInt(Request.QueryString("id")), True)
            '    BindDgTraining(0)
            'End If
        End If

        ProcessPhoto()
        ProcessKTP()

        SetSetting()

        If Val(hdnVal.Value) = 1 Then
            ViewState("vsProcess") = "Edit"
            hdnVal.Value = "0"
            'Dim salcode As String = CType(sessHelper.GetSession("CSTeam"), SalesmanHeader).SalesmanCode
            'Dim cri As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'cri.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.Exact, salcode))

            'Dim oblsalhe As SalesmanHeader = CType((New SalesmanHeaderFacade(User).Retrieve(cri)).Item(0), SalesmanHeader)
            Dim oblsalhe As SalesmanHeader = CType(sessHelper.GetSession("CSTeam"), SalesmanHeader)
            ViewState("vsProcess") = "Konfirmasi"
            ProcessSave(oblsalhe, False)
        End If

        lbtnRefSalesman.Attributes("onclick") = "ShowSalesmanResign();"
    End Sub

    Private Sub lnkReloadSalesman_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkReloadSalesman.Click
        If txtRefSalesman.Text = "" Then
            MessageBox.Show("Pilih user yang telah resign terlebih dahulu")
        Else
            'isRegistered = True
            Dim objSalesmanHeaderReload As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtRefSalesman.Text)
            If Not IsNothing(objSalesmanHeaderReload) Then
                If Request.QueryString("SalesResign") = "" Then
                    Response.Redirect("FrmCcInputCSTeam.aspx?ID=" + objSalesmanHeaderReload.ID.ToString + "&edit=false" + "&Mode=CS&Konfirmasi=True&SalesResign=" + txtRefSalesman.Text)
                End If
            Else
                MessageBox.Show("User Reference tidak valid, silakan gunakan pop up")
            End If
        End If
    End Sub

    Private Sub ddlPropinsi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPropinsi.SelectedIndexChanged
        If ddlPropinsi.SelectedValue <> "" Then
            CommonFunction.BindCity(ddlKota, User, True, ddlPropinsi.SelectedValue, False)
        Else
            ddlKota.Items.Clear()
        End If
    End Sub

    Private Sub btnGenerateCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateCode.Click
        Dim objSalesmanHeader As SalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader)
        Dim salesmanFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)

        objSalesmanHeader.RegisterStatus = CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)
        objSalesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Aktif, String)
        objSalesmanHeader.SalesmanCode = "CS_Employee"

        Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

        If vr.IsValid = False Then
            MessageBox.Show(vr.Message)
            Exit Sub
        End If

        Dim nResult As Integer = salesmanFacade.Update(objSalesmanHeader)

        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
            btnGenerateCode.Enabled = True
        Else
            '(objSalesmanHeader)
            MessageBox.Show(SR.SaveSuccess)
            lblSalesmanCode.Text = salesmanFacade.Retrieve(objSalesmanHeader.ID).SalesmanCode
            btnGenerateCode.Enabled = False
        End If

    End Sub

    Private Sub btnKembali_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmCcCSTeamList.aspx?Mode=CS")
    End Sub
    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click

        Dim queryString As String = String.Empty
        For Each key As String In Request.QueryString.AllKeys
            queryString += key & "=" & Request.QueryString(key) & "&"
        Next

        If queryString = String.Empty Then
            ClearData()
        Else
            queryString = queryString.Remove(queryString.Length - 1)
            Response.Redirect("FrmCcInputCSTeam.aspx?" & queryString)
        End If


    End Sub
    Private Sub lblRemoveImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblRemoveImage.Click
        'sessHelper.SetSession("RemovePic", True)
        'MessageBox.Show("Tekan button Simpan, untuk eksekusi remove image")
        sessHelper.SetSession(sessImageByte, Nothing)
        photoView.ImageUrl = String.Empty
        lblRemoveImage.Visible = False
        photoSrc.Disabled = False
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        'Validation

        If Not Page.IsValid Then
            MessageBox.Show("Data Belum Lengkap")
            Return
        End If

        If ddlJobPositionDesc.SelectedIndex = 0 Then
            MessageBox.Show("Posisi Harus Dipilih")
            Return
        End If

        If ICStartWork.Value > DateTime.Now Then
            MessageBox.Show("Tanggal Masuk Tidak boleh lebih besar dari Hari ini")
            Return
        End If

        If ddlGender.SelectedIndex = 0 Then
            MessageBox.Show("Jenis Kelamin Harus Dipilih ")
            Return
        End If

        If ddlMarriedStatus.SelectedIndex = 0 Then
            MessageBox.Show("Status Perkawinan Harus Dipilih")
            Return
        End If

        If hdnAppointmentLetterPath.Value = String.Empty Then
            If uplAppointmentLetter.PostedFile.FileName = String.Empty Then
                MessageBox.Show("Harap Upload File Surat Pengangkatan")
                Return
            End If
        End If

        If uplAppointmentLetter.PostedFile.ContentLength > 512000 Then
            MessageBox.Show("Ukuran File Surat Pengangkatan Maximal 500Kb")
            Return
        End If

        Dim ktp As String = String.Empty

        ktp = GetKTPFromUi()
        Dim objDealer As Dealer = Session.Item("DEALER")
        Dim CommFuntion As New CommonFunction

        'ini untuk edit
        If CType(ViewState("vsProcess"), String) <> "Insert" AndAlso objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            If CType(ViewState("vsProcess"), String) <> "Konfirmasi" Then

                Dim objExist As New SalesmanHeader
                Dim ObjSalesmanHeaderID As Integer = Session.Item("HeaderID")

                If ddlJobPositionDesc.SelectedValue = "40" Then
                    Dim lastCSO As SalesmanHeader = GetLastCSO(ObjSalesmanHeaderID)
                    If Not lastCSO Is Nothing Then
                        If lastCSO.Status <> 3 Then
                            MessageBox.Show("Tidak dapat menginput CSO, karena dealer masih memiliki CSO")
                            Return
                        Else
                            Dim critKTPSales As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
                            critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, ktp))
                            critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.SalesIndicator", MatchType.Exact, 4)) 'jika dibutuhkan aktifkan kembali

                            Dim arrSalesmanFromKTP As ArrayList = New SalesmanProfileFacade(User).Retrieve(critKTPSales)

                            If arrSalesmanFromKTP.Count > 0 Then
                                Dim existingSalesmanHeaderByKTP As SalesmanHeader = CType(arrSalesmanFromKTP(0), SalesmanProfile).SalesmanHeader
                                Dim existingSalesmanHeaderById As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(ObjSalesmanHeaderID)
                                If existingSalesmanHeaderByKTP.ID = existingSalesmanHeaderById.ID And existingSalesmanHeaderByKTP.Dealer.ID <> existingSalesmanHeaderById.Dealer.ID And existingSalesmanHeaderByKTP.Dealer.DealerGroup.ID = existingSalesmanHeaderById.Dealer.DealerGroup.ID Then
                                    Dim prof As SalesmanAdditionalInfo = GetSalesmanAdditionalInfoBySalesmanHeaderId(ObjSalesmanHeaderID)
                                    ICStartWork.Value = prof.CSOHireDate
                                Else
                                    Dim lastCSOHireDate As DateTime = GetLastCSOHireDate(lastCSO.ID)
                                    If lastCSOHireDate > ICStartWork.Value Then
                                        MessageBox.Show("Tidak dapat menginput CSO, karena tanggal masuk kurang dari tanggal resign CSO sebelumnya")
                                        Return
                                    End If
                                End If

                            Else
                                Dim lastCSOHireDate As DateTime = GetLastCSOHireDate(lastCSO.ID)
                                If lastCSOHireDate > ICStartWork.Value Then
                                    MessageBox.Show("Tidak dapat menginput CSO, karena tanggal masuk kurang dari tanggal resign CSO sebelumnya")
                                    Return
                                End If
                            End If
                        End If
                    End If
                End If

                Dim critKTP As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, ktp))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.No, ObjSalesmanHeaderID))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.Status", MatchType.No, 3))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.SalesIndicator", MatchType.Exact, 4)) 'jika dibutuhkan aktifkan kembali

                Dim sortCollKTP As SortCollection = New SortCollection
                sortCollKTP.Add(New Sort(GetType(SalesmanProfile), "CreatedTime", Sort.SortDirection.DESC))

                Dim arrKTP As ArrayList = New SalesmanProfileFacade(User).Retrieve(critKTP, sortCollKTP)

                If arrKTP.Count > 0 Then
                    ktp = ktp.Trim.Replace(".", "")
                    For Each sp As SalesmanProfile In arrKTP
                        Dim ktpValue As String = sp.ProfileValue.Trim.Replace(".", "")
                        If (ktpValue = ktp) Then
                            objExist = sp.SalesmanHeader
                            Exit For
                        End If
                    Next
                End If

                If (objExist.ID > 0) Then
                    If objExist.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String) Then
                        'sessHelper.SetSession("SALESREFERENCE", objExist)
                        objExist.HireDate = ICStartWork.Value
                        sessHelper.SetSession("CSTeam", objExist)
                        MessageBox.Confirm("Nomor KTP atas nama " & objExist.Name & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & ") - " &
                                           objExist.SalesmanCode & " di dealer " & objExist.Dealer.DealerName & " )\n Apakah anda akan tetap menginput data?", "hdnVal")
                    Else
                        MessageBox.Show("Nomor KTP atas nama  " & objExist.Name & " - " & objExist.SalesmanCode & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") &
                                        "), \n masih aktif terdaftar di dealer " & objExist.Dealer.DealerName)
                        Return
                    End If

                End If
            End If
        Else
            If CType(ViewState("vsProcess"), String) <> "Konfirmasi" Then
                Dim objExist As New SalesmanHeader
                Dim ObjSalesmanHeaderID As Integer = Session.Item("HeaderID")


                If ddlJobPositionDesc.SelectedValue = "40" Then
                    Dim lastCSO As SalesmanHeader = GetLastCSO()
                    If Not lastCSO Is Nothing Then
                        If lastCSO.Status <> 3 Then
                            MessageBox.Show("Tidak dapat menginput CSO, karena dealer masih memiliki CSO")
                            Return
                        Else
                            Dim critKTPSales As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
                            critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, ktp))
                            critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.SalesIndicator", MatchType.Exact, 4)) 'jika dibutuhkan aktifkan kembali

                            Dim arrSalesmanFromKTP As ArrayList = New SalesmanProfileFacade(User).Retrieve(critKTPSales)
                            If arrSalesmanFromKTP.Count > 0 Then
                                Dim existingSalesmanHeaderByKTP As SalesmanHeader = CType(arrSalesmanFromKTP(0), SalesmanProfile).SalesmanHeader
                                If existingSalesmanHeaderByKTP.Dealer.ID <> objDealer.ID And existingSalesmanHeaderByKTP.Dealer.DealerGroup.ID = objDealer.DealerGroup.ID Then
                                    Dim prof As SalesmanAdditionalInfo = GetSalesmanAdditionalInfoBySalesmanHeaderId(existingSalesmanHeaderByKTP.ID)
                                    If ICStartWork.Value < existingSalesmanHeaderByKTP.HireDate Then
                                        MessageBox.Show("Tidak dapat menginput CSO, karena tanggal masuk harus lebih besar sama dengan dari tanggal masuk sebelumnya")
                                        Return
                                    End If
                                Else
                                    Dim lastCSOHireDate As DateTime = GetLastCSOHireDate(lastCSO.ID)
                                    If lastCSOHireDate > ICStartWork.Value Then
                                        MessageBox.Show("Tidak dapat menginput CSO, karena tanggal masuk kurang dari tanggal resign CSO sebelumnya")
                                        Return
                                    End If
                                End If

                            Else
                                Dim lastCSOHireDate As DateTime = GetLastCSOHireDate(lastCSO.ID)
                                If lastCSOHireDate > ICStartWork.Value Then
                                    MessageBox.Show("Tidak dapat menginput CSO, karena tanggal masuk kurang dari tanggal resign CSO sebelumnya")
                                    Return
                                End If
                            End If
                        End If
                    End If
                End If


                Dim critKTP As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, ktp))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.No, ObjSalesmanHeaderID))
                critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.SalesIndicator", MatchType.Exact, 4))

                Dim sortCollKTP As SortCollection = New SortCollection
                sortCollKTP.Add(New Sort(GetType(SalesmanProfile), "CreatedTime", Sort.SortDirection.DESC))

                Dim arrKTP As ArrayList = New SalesmanProfileFacade(User).Retrieve(critKTP, sortCollKTP)
                If arrKTP.Count > 0 Then
                    ktp = ktp.Trim.Replace(".", "")
                    For Each sp As SalesmanProfile In arrKTP
                        Dim ktpValue As String = sp.ProfileValue.Trim.Replace(".", "")
                        If (ktpValue = ktp) Then
                            objExist = sp.SalesmanHeader
                            Exit For
                        End If
                    Next
                End If

                If (objExist.ID > 0) Then
                    If objExist.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String) Then
                        'sessHelper.SetSession("SALESREFERENCE", objExist)
                        sessHelper.SetSession("CSTeam", objExist)
                        MessageBox.Confirm("Apakah Nomor KTP atas nama " & objExist.Name & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & ") - " & objExist.SalesmanCode & " Tetap Menginput Data?", "hdnVal")
                    Else
                        MessageBox.Show("Nomor KTP atas nama  " & objExist.Name & " - " & objExist.SalesmanCode & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & "), \n masih aktif terdaftar di dealer " & objExist.Dealer.DealerName)
                        Return
                    End If

                End If

            End If
        End If


        Dim objSalHea As SalesmanHeader
        If CType(ViewState("vsProcess"), String) = "Insert" Then

            'If ddlJobPositionDesc.SelectedValue = "40" Then
            '    Dim lastCSO As SalesmanHeader = GetLastCSO()
            '    If Not lastCSO Is Nothing Then
            '        If lastCSO.Status <> 3 Then
            '            MessageBox.Show("Tidak dapat menginput CSO, karena dealer masih memiliki CSO")
            '            Return
            '        Else
            '            Dim lastCSOHireDate As DateTime = GetLastCSOHireDate(lastCSO.ID)
            '            If lastCSOHireDate > ICStartWork.Value Then
            '                MessageBox.Show("Tidak dapat menginput CSO, karena tanggal masuk kurang dari tanggal resign CSO sebelumnya")
            '                Return
            '            End If
            '        End If
            '    End If
            'End If

            If IsNothing(sessHelper.GetSession(sessImageByte)) Then
                MessageBox.Show("Silahkan MengUpload Foto Terlebih Dahulu")
                Return
            End If

            'If photoSrc.Value = "" And Not String.IsNullorEmpty(lblSalesmanCode.Text) Then
            '    MessageBox.Show("Silahkan MengUpload Foto Terlebih Dahulu")
            '    Return
            'End If




            If ddlJobPositionDesc.SelectedValue = "40" Then
                Dim lastCSO As SalesmanHeader = GetLastCSO()
                If Not lastCSO Is Nothing Then
                    If lastCSO.Status <> 3 Then
                        MessageBox.Show("Tidak dapat menginput CSO, karena dealer masih memiliki CSO")
                        Return
                    Else
                        Dim critKTPSales As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
                        critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, ktp))
                        critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.SalesIndicator", MatchType.Exact, 4)) 'jika dibutuhkan aktifkan kembali

                        Dim arrSalesmanFromKTP As ArrayList = New SalesmanProfileFacade(User).Retrieve(critKTPSales)
                        If arrSalesmanFromKTP.Count > 0 Then
                            Dim existingSalesmanHeaderByKTP As SalesmanHeader = CType(arrSalesmanFromKTP(0), SalesmanProfile).SalesmanHeader
                            If existingSalesmanHeaderByKTP.Dealer.ID <> objDealer.ID And existingSalesmanHeaderByKTP.Dealer.DealerGroup.ID = objDealer.DealerGroup.ID Then
                                Dim prof As SalesmanAdditionalInfo = GetSalesmanAdditionalInfoBySalesmanHeaderId(existingSalesmanHeaderByKTP.ID)
                                ICStartWork.Value = prof.CSOHireDate
                            Else
                                Dim lastCSOHireDate As DateTime = GetLastCSOHireDate(lastCSO.ID)
                                If lastCSOHireDate > ICStartWork.Value Then
                                    MessageBox.Show("Tidak dapat menginput CSO, karena tanggal masuk kurang dari tanggal resign CSO sebelumnya")
                                    Return
                                End If
                            End If

                        Else
                            Dim lastCSOHireDate As DateTime = GetLastCSOHireDate(lastCSO.ID)
                            If lastCSOHireDate > ICStartWork.Value Then
                                MessageBox.Show("Tidak dapat menginput CSO, karena tanggal masuk kurang dari tanggal resign CSO sebelumnya")
                                Return
                            End If
                        End If
                    End If
                End If
            End If

            Dim objRenderPanel As RenderingProfile = New RenderingProfile
            Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
            Dim valid As Boolean = False
            Dim objExist As New SalesmanHeader

            Dim critKTP As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
            critKTP.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, ktp))


            Dim sortCollKTP As SortCollection = New SortCollection
            sortCollKTP.Add(New Sort(GetType(SalesmanProfile), "CreatedTime", Sort.SortDirection.DESC))

            Dim arrKTP As ArrayList = New SalesmanProfileFacade(User).Retrieve(critKTP, sortCollKTP)


            If arrKTP.Count > 0 Then
                ktp = ktp.Trim.Replace(".", "")
                For Each sp As SalesmanProfile In arrKTP
                    Dim ktpValue As String = sp.ProfileValue.Trim.Replace(".", "")
                    If (ktpValue = ktp) Then
                        objExist = sp.SalesmanHeader
                        Exit For
                    End If
                Next
            End If

            If (objExist.ID > 0 And objExist.SalesIndicator = 4) Then
                If objExist.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String) Then
                    'sessHelper.SetSession("SALESREFERENCE", objExist)
                    objExist.HireDate = objSalHea.HireDate
                    objExist.Dealer = objSalHea.Dealer
                    sessHelper.SetSession("CSTeam", objExist)
                    MessageBox.Confirm("Nomor KTP atas nama " & objExist.Name & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & ") - " & objExist.SalesmanCode & " di dealer " & objExist.Dealer.DealerName & " )\n Apakah anda akan tetap menginput data?", "hdnVal")
                Else
                    MessageBox.Show("Nomor KTP atas nama  " & objExist.Name & " - " & objExist.SalesmanCode & " (" & objExist.DateOfBirth.ToString("dd/MM/yyyy") & "), \n masih aktif terdaftar di dealer " & objExist.Dealer.DealerName)
                    Return
                End If
            Else


                ProcessSave(objSalHea)
                'objSalHea = New SalesmanHeader
                'objSalHea = CType(sessHelper.GetSession("objSalesmanHeader"), SalesmanHeader)


            End If
        ElseIf CType(ViewState("vsProcess"), String) = "Konfirmasi" Then

            If ddlJobPositionDesc.SelectedValue = "40" Then
                Dim lastCSO As SalesmanHeader = GetLastCSO()
                If Not lastCSO Is Nothing Then
                    If lastCSO.Status <> 3 Then
                        MessageBox.Show("Tidak dapat menginput CSO, karena dealer masih memiliki CSO")
                        Return
                    Else
                        Dim critKTPSales As New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29))
                        critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileValue", MatchType.Exact, ktp))
                        critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        critKTPSales.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.SalesIndicator", MatchType.Exact, 4)) 'jika dibutuhkan aktifkan kembali

                        Dim arrSalesmanFromKTP As ArrayList = New SalesmanProfileFacade(User).Retrieve(critKTPSales)
                        If arrSalesmanFromKTP.Count > 0 Then
                            Dim existingSalesmanHeaderByKTP As SalesmanHeader = CType(arrSalesmanFromKTP(0), SalesmanProfile).SalesmanHeader
                            If existingSalesmanHeaderByKTP.Dealer.ID <> objDealer.ID And existingSalesmanHeaderByKTP.Dealer.DealerGroup.ID = objDealer.DealerGroup.ID Then
                                Dim prof As SalesmanAdditionalInfo = GetSalesmanAdditionalInfoBySalesmanHeaderId(existingSalesmanHeaderByKTP.ID)
                                ICStartWork.Value = prof.CSOHireDate
                            Else
                                Dim lastCSOHireDate As DateTime = GetLastCSOHireDate(lastCSO.ID)
                                If lastCSOHireDate > ICStartWork.Value Then
                                    MessageBox.Show("Tidak dapat menginput CSO, karena tanggal masuk kurang dari tanggal resign CSO sebelumnya")
                                    Return
                                End If
                            End If

                        Else
                            Dim lastCSOHireDate As DateTime = GetLastCSOHireDate(lastCSO.ID)
                            If lastCSOHireDate > ICStartWork.Value Then
                                MessageBox.Show("Tidak dapat menginput CSO, karena tanggal masuk kurang dari tanggal resign CSO sebelumnya")
                                Return
                            End If
                        End If
                    End If
                End If
            End If

            ProcessSave(objSalHea)
        Else
            ProcessSave(objSalHea)
        End If
    End Sub


#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        'Dim objDealer As Dealer = Session.Item("DEALER")
        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    If Request.QueryString("Mode") = "CS" Then
        '        If Not SecurityProvider.Authorize(Context.User, SR.CSO_Input_CS_Employee_privilege) Then
        '            Server.Transfer("../FrmAccessDenied.aspx?modulName=CS Employee - Buat CS Employee")
        '        End If
        '        'btnSimpan.Enabled = SecurityProvider.Authorize(Context.User, SR.Input_data_salesman_part_privilege)
        '    End If
        'Else
        '    If Not SecurityProvider.Authorize(Context.User, SR.CSO_Generate_CS_Employee_privilege) Then
        '        Server.Transfer("../FrmAccessDenied.aspx?modulName=CS Employee - Buat CS Employee")
        '    End If
        'End If

    End Sub

#End Region

#Region "Internal Enum"
    Private Enum HistoryStatus
        Baru = 0
        Pengajuan = 1
        Disetujui = 2
        Ditolak = 3
        Dibatalkan = 4
        Resign = 5
    End Enum

#End Region

    'Private Sub dtgTraining_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgTraining.SortCommand
    '    If e.SortExpression = sessHelper.GetSession("SortColTraining") Then
    '        If sessHelper.GetSession("SortDirectionTraining") = Sort.SortDirection.ASC Then
    '            sessHelper.SetSession("SortDirectionTraining", Sort.SortDirection.DESC)
    '        Else
    '            sessHelper.SetSession("SortDirectionTraining", Sort.SortDirection.ASC)
    '        End If
    '    End If
    '    sessHelper.SetSession("SortColTraining", e.SortExpression)
    '    dtgTraining.SelectedIndex = -1
    '    BindDgTraining(dtgTraining.CurrentPageIndex)
    'End Sub

    'Private Sub dtgTraining_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgTraining.PageIndexChanged
    '    dtgTraining.CurrentPageIndex = e.NewPageIndex
    '    BindDgTraining(dtgTraining.CurrentPageIndex)
    'End Sub

    'Private Sub dtgTraining_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgTraining.ItemCommand
    '    If e.CommandName = "deleteTrain" Then
    '        Dim facade As SalesmanTrainingFacade = New SalesmanTrainingFacade(User)
    '        Dim objSalesmanTraining As SalesmanTraining = facade.Retrieve(CInt(e.CommandArgument))
    '        Dim result As Integer = facade.DeleteFromDB(objSalesmanTraining)
    '        BindDgTraining(0)

    '    End If

    '    If e.CommandName = "addTrain" Then
    '        Dim facade As SalesmanTrainingFacade = New SalesmanTrainingFacade(User)
    '        Dim txtModulTrainingAdd As TextBox = e.Item.FindControl("txtModulTrainingAdd")
    '        Dim txtTempatTanggalAdd As TextBox = e.Item.FindControl("txtTempatTanggalAdd")
    '        Dim txtPenyelenggaraAdd As TextBox = e.Item.FindControl("txtPenyelenggaraAdd")

    '        If txtModulTrainingAdd.Text = "" Or txtTempatTanggalAdd.Text = "" Or txtPenyelenggaraAdd.Text = "" Then
    '            MessageBox.Show("Lengkapi Data Dulu !")
    '            Return
    '        End If

    '        Dim objSalesmanTraining As SalesmanTraining = New SalesmanTraining
    '        objSalesmanTraining.SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
    '        objSalesmanTraining.TrainingModule = txtModulTrainingAdd.Text
    '        objSalesmanTraining.TrainingPlaceAndDate = txtTempatTanggalAdd.Text
    '        objSalesmanTraining.TrainingProvider = txtPenyelenggaraAdd.Text

    '        Dim result As Integer = facade.Insert(objSalesmanTraining)

    '        If result = -1 Then
    '            MessageBox.Show(SR.SaveFail)
    '        Else
    '            MessageBox.Show(SR.SaveSuccess)
    '        End If
    '        BindDgTraining(0)


    '    End If

    '    If e.CommandName = "editTrain" Then
    '        dtgTraining.ShowFooter = False
    '        dtgTraining.EditItemIndex = e.Item.ItemIndex
    '        BindDgTraining(0)
    '    End If

    '    If e.CommandName = "cancelTrain" Then
    '        dtgTraining.ShowFooter = True
    '        dtgTraining.EditItemIndex = -1
    '        BindDgTraining(0)
    '    End If

    '    If e.CommandName = "saveTrain" Then
    '        Dim objSalesmanTraining As SalesmanTraining
    '        objSalesmanTraining = New SalesmanTrainingFacade(User).Retrieve(CInt(e.CommandArgument))

    '        Dim txtModulTrainingEdit As TextBox = e.Item.FindControl("txtModulTrainingEdit")
    '        Dim txtTempatTanggalEdit As TextBox = e.Item.FindControl("txtTempatTanggalEdit")
    '        Dim txtPenyelenggaraEdit As TextBox = e.Item.FindControl("txtPenyelenggaraEdit")
    '        objSalesmanTraining.TrainingModule = txtModulTrainingEdit.Text
    '        objSalesmanTraining.TrainingPlaceAndDate = txtTempatTanggalEdit.Text
    '        objSalesmanTraining.TrainingProvider = txtPenyelenggaraEdit.Text

    '        Dim facade As SalesmanTrainingFacade = New SalesmanTrainingFacade(User)
    '        Dim result As Integer = facade.Update(objSalesmanTraining)

    '        If result = -1 Then
    '            MessageBox.Show(SR.SaveFail)
    '        Else
    '            MessageBox.Show(SR.SaveSuccess)
    '        End If
    '        dtgTraining.ShowFooter = True
    '        dtgTraining.EditItemIndex = -1
    '        BindDgTraining(0)

    '    End If

    'End Sub
    'Private Sub dtgTraining_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTraining.ItemDataBound
    '    If e.Item.ItemIndex <> -1 Then
    '        If Not (arrTraining Is Nothing) Then
    '            Dim objSalesmanTraining As SalesmanTraining
    '            objSalesmanTraining = arrTraining(e.Item.ItemIndex)

    '            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgTraining.CurrentPageIndex * dtgTraining.PageSize)
    '            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '                Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDeleteTrain"), LinkButton)
    '                _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
    '                _lbtnDelete.CommandArgument = objSalesmanTraining.ID

    '            End If

    '            If e.Item.ItemType = ListItemType.EditItem Then
    '                Dim _lbtnSave As LinkButton = CType(e.Item.FindControl("lbtnSaveTrain"), LinkButton)
    '                _lbtnSave.CommandArgument = objSalesmanTraining.ID

    '            End If
    '        End If
    '    End If

    'End Sub


    'Private Sub dtgTraining_sItemCommand(source As Object, e As DataGridCommandEventArgs) 'Handles dtgTraining.ItemCommand
    '    Dim objSalTrain As New SalesmanTraining
    '    If e.CommandName = "deleteTrain" Then
    '        Dim facade As SalesmanTrainingFacade = New SalesmanTrainingFacade(User)
    '        Dim objSalesmanTraining As SalesmanTraining = facade.Retrieve(CInt(e.CommandArgument))
    '        BindTrainingCRUD(0, objSalesmanTraining, "Delete")
    '        'Dim result As Integer = facade.DeleteFromDB(objSalesmanTraining)
    '    End If

    '    If e.CommandName = "addTrain" Then
    '        Dim facade As SalesmanTrainingFacade = New SalesmanTrainingFacade(User)
    '        Dim txtModulTrainingAdd As TextBox = e.Item.FindControl("txtModulTrainingAdd")
    '        Dim txtTempatTanggalAdd As TextBox = e.Item.FindControl("txtTempatTanggalAdd")
    '        Dim txtPenyelenggaraAdd As TextBox = e.Item.FindControl("txtPenyelenggaraAdd")

    '        If txtModulTrainingAdd.Text = "" Or txtTempatTanggalAdd.Text = "" Or txtPenyelenggaraAdd.Text = "" Then
    '            MessageBox.Show("Lengkapi Data Dulu !")
    '            Return
    '        End If

    '        Dim objSalesmanTraining As SalesmanTraining = New SalesmanTraining
    '        objSalesmanTraining.SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
    '        objSalesmanTraining.TrainingModule = txtModulTrainingAdd.Text
    '        objSalesmanTraining.TrainingPlaceAndDate = txtTempatTanggalAdd.Text
    '        objSalesmanTraining.TrainingProvider = txtPenyelenggaraAdd.Text
    '        objSalesmanTraining.ID = -nIdTemp

    '        'Dim result As Integer = facade.Insert(objSalesmanTraining)

    '        'If result = -1 Then
    '        '    MessageBox.Show(SR.SaveFail)
    '        'Else
    '        '    MessageBox.Show(SR.SaveSuccess)
    '        'End If
    '        BindTrainingCRUD(0, objSalesmanTraining, "Add")


    '    End If

    '    If e.CommandName = "editTrain" Then
    '        Dim facade As SalesmanTrainingFacade = New SalesmanTrainingFacade(User)
    '        Dim objSalesmanTraining As SalesmanTraining = facade.Retrieve(CInt(e.CommandArgument))
    '        dtgTraining.ShowFooter = False
    '        dtgTraining.EditItemIndex = e.Item.ItemIndex
    '        'BindTrainingCRUD(0, objSalesmanTraining, "Edit")
    '    End If

    '    If e.CommandName = "cancelTrain" Then
    '        dtgTraining.ShowFooter = True
    '        dtgTraining.EditItemIndex = -1
    '        BindTraining(0)
    '    End If

    '    If e.CommandName = "saveTrain" Then
    '        Dim objSalesmanTraining As SalesmanTraining
    '        'objSalesmanTraining = New SalesmanTrainingFacade(User).Retrieve(CInt(e.CommandArgument))

    '        Dim txtModulTrainingEdit As TextBox = e.Item.FindControl("txtModulTrainingEdit")
    '        Dim txtTempatTanggalEdit As TextBox = e.Item.FindControl("txtTempatTanggalEdit")
    '        Dim txtPenyelenggaraEdit As TextBox = e.Item.FindControl("txtPenyelenggaraEdit")
    '        Dim lblID As Label = e.Item.FindControl("lblID")
    '        objSalTrain.TrainingModule = txtModulTrainingEdit.Text
    '        objSalTrain.TrainingPlaceAndDate = txtTempatTanggalEdit.Text
    '        objSalTrain.TrainingProvider = txtPenyelenggaraEdit.Text
    '        objSalTrain.ID = Val(lblID.Text)

    '        BindTrainingCRUD(0, objSalesmanTraining, "Edit")

    '        dtgTraining.ShowFooter = True
    '        dtgTraining.EditItemIndex = -1
    '        BindTraining(0)

    '    End If
    '    Dim nId As Integer
    '    nIdTemp = nIdTemp - 1
    'End Sub

    Private Function GetLastCSO(Optional ByVal salesmanHeaderId As Integer = 0) As SalesmanHeader
        Dim objDealer As Dealer = CType(Session("Dealer"), Dealer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.No, "3")) 'resign
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "JobPosition.ID", MatchType.Exact, "40")) 'CSO
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, "4"))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))

        Dim sortColLastUpdate As SortCollection = New SortCollection
        sortColLastUpdate.Add(New Sort(GetType(SalesmanHeader), "LastUpdateTime", Sort.SortDirection.DESC))

        If salesmanHeaderId <> 0 Then
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.No, salesmanHeaderId))
        End If

        Dim arlResult As ArrayList = New SalesmanHeaderFacade(User).Retrieve(criterias, sortColLastUpdate)

        If arlResult.Count > 0 Then
            Return CType(arlResult(0), SalesmanHeader)
        Else
            Return Nothing
        End If

    End Function

    Private Sub ProcessPhoto()

        If photoSrc.Value <> "" Then
            uploadFile()
        End If

        If Not IsNothing(sessHelper.GetSession(sessImageByte)) Then
            Dim imageByteFromSession As Byte() = CType(sessHelper.GetSession(sessImageByte), Byte())
            photoView.ImageUrl = "data:image;base64," + Convert.ToBase64String(imageByteFromSession)

            lblRemoveImage.Visible = True
            photoSrc.Disabled = True
        Else
            lblRemoveImage.Visible = False
        End If

    End Sub

    Private Sub ProcessKTP()

        If ktpSrc.Value <> "" Then
            UploadKtp()
        End If

        If Not IsNothing(sessHelper.GetSession(sessKtpPath)) Then
            DisplayKTPImage()
        Else
            lblRemoveKtp.Visible = False
        End If

    End Sub

    Private Sub DisplayKTPImage()
        Dim pathKtpFromSession As String = CType(sessHelper.GetSession(sessKtpPath), String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                ktpView.ImageUrl = KTB.DNet.Lib.WebConfig.GetValue("SAN") & pathKtpFromSession
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
        lblRemoveKtp.Visible = True
        ktpSrc.Disabled = True
    End Sub

    Private Sub DisableKTPField()
        Try
            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(Request.QueryString("ID")))
            Dim renderProfile As New RenderingProfile
            Dim profileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
            Dim arlProfile As ArrayList = renderProfile.GetListProfileHeader(objSalesmanHeader.ID, profileGroup, EnumProfileType.ProfileType.CS)

            If arlProfile.Count > 0 Then
                Dim listProfile As List(Of ProfileHeader) = arlProfile.Cast(Of ProfileHeader).ToList()
                Dim ktpProfile As ProfileHeader = listProfile.FirstOrDefault(Function(x) x.Code = "NOKTP")

                If Not IsNothing(ktpProfile) Then
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, profileGroup.ID))
                    criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileHeader.ID", MatchType.Exact, ktpProfile.ID))
                    Dim arlGroup As ArrayList = New ProfileHeaderToGroupFacade(User).Retrieve(criterias)

                    If arlGroup.Count > 0 Then
                        Dim profHeadToGrup As ProfileHeaderToGroup = CType(arlGroup(0), ProfileHeaderToGroup)

                        Dim textboxId As String = "TEXTBOX" & profHeadToGrup.ID & "_" & profileGroup.ID
                        Dim textBoxKtp As TextBox = CType(Panel1.FindControl(textboxId), TextBox)
                        textBoxKtp.Enabled = False
                    End If
                End If

            End If
        Catch ex As Exception
            Dim a As String = ex.Message
        End Try
    End Sub

    Protected Sub btnRequestID_Click(sender As Object, e As EventArgs) Handles btnRequestID.Click
        Dim func As New SalesmanHeaderFacade(User)
        Dim ObjSalesmanHeaderID As Integer = Session.Item("HeaderID")
        Try
            'btnSimpan_Click(sender, e)
            Dim objSalesmanHeader As SalesmanHeader = func.Retrieve(ObjSalesmanHeaderID)
            objSalesmanHeader.IsRequestID = EnumSalesmanIsRequest.SalesmanIsRequest.Sudah_Request

            Dim vr As ValidResult = New SalesmanHeaderValidation().ValidateKTPSalesmanHeader(objSalesmanHeader)

            If vr.IsValid = False Then
                MessageBox.Show(vr.Message)
                Exit Sub
            End If

            func.Update(objSalesmanHeader)
            txtName.Enabled = False
            ICDateOfBirth.Enabled = False
            btnRequestID.NonVisible()
            DisableKTPField()
            MessageBox.Show("Request ID berhasil")
        Catch ex As Exception
            MessageBox.Show("Request ID gagal. " + ex.Message)
        End Try
    End Sub

    Protected Sub cvGender_ServerValidate(source As Object, args As ServerValidateEventArgs)
        If ddlGender.SelectedIndex = 0 Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Protected Sub cvMarriedStatus_ServerValidate(source As Object, args As ServerValidateEventArgs)
        If ddlMarriedStatus.SelectedIndex = 0 Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Protected Sub cvPropinsi_ServerValidate(source As Object, args As ServerValidateEventArgs)
        If ddlPropinsi.SelectedIndex = 0 Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Protected Sub cvKota_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If ddlKota.SelectedIndex = 0 Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvJobPositionDesc_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If ddlJobPositionDesc.SelectedIndex = 0 Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub



    Protected Sub cvAlamat_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If txtAlamat.Text.Trim = String.Empty Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Protected Sub cvImage_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If IsNothing(sessHelper.GetSession(sessImageByte)) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Private Sub lblRemoveKtp_Click(sender As Object, e As EventArgs) Handles lblRemoveKtp.Click
        'sessHelper.SetSession("RemovePic", True)
        'MessageBox.Show("Tekan button Simpan, untuk eksekusi remove image")
        sessHelper.SetSession(sessKtpPath, Nothing)
        ktpView.ImageUrl = String.Empty
        lblRemoveKtp.Visible = False
        ktpSrc.Disabled = False
    End Sub

    Private Function GetSalesmanAdditionalInfoBySalesmanHeaderId(salesmanHeaderId As Integer) As SalesmanAdditionalInfo
        Dim result As New SalesmanAdditionalInfo
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", MatchType.Exact, salesmanHeaderId))

        Dim arlResult As ArrayList = New SalesmanAdditionalInfoFacade(User).Retrieve(criterias)
        If arlResult.Count > 0 Then
            result = CType(arlResult(0), SalesmanAdditionalInfo)
        Else
            result.SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(salesmanHeaderId)
            result.SalesmanCategoryLevel = New SalesmanCategoryLevelFacade(User).Retrieve(1)
        End If
        Return result
    End Function

    Protected Sub cvKtp_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Try
            If IsNothing(sessHelper.GetSession(sessKtpPath)) Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If
        Catch ex As Exception
            args.IsValid = False
        End Try
    End Sub

    Private Sub ddlJobPositionDesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJobPositionDesc.SelectedIndexChanged
        Try
            If ddlJobPositionDesc.SelectedItem.Text = "CSO" Then
                rowKategoriTim.Visible = False
            Else
                rowKategoriTim.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindDdlKategoriTim()
        ddlKategoriTim.Items.Clear()
        Dim objDealer As Dealer = Session.Item("DEALER")

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DealerCategory), "Dealer.ID", MatchType.Exact, objDealer.ID))
        Dim arlResult As ArrayList = New DealerCategoryFacade(User).Retrieve(criterias)

        For Each dealerCategory As DealerCategory In arlResult
            ddlKategoriTim.Items.Add(New ListItem(dealerCategory.Category.CategoryCode, dealerCategory.Category.ID))
        Next


    End Sub

    Private Sub CreateProfileKategoriTeam(objSalHea As SalesmanHeader)
        Dim salesmanProfileFacade As SalesmanProfileFacade = New SalesmanProfileFacade(User)
        Dim salesmanProfile As New SalesmanProfile
        salesmanProfile.ProfileHeader = New ProfileHeaderFacade(User).Retrieve(45)
        salesmanProfile.ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)
        salesmanProfile.ProfileValue = ddlKategoriTim.SelectedItem.Text
        salesmanProfile.SalesmanHeader = objSalHea
        salesmanProfileFacade.Insert(salesmanProfile)
    End Sub

    Private Function GetExistingProfileKategoriTim(objSalesHeader As SalesmanHeader) As SalesmanProfile

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, objSalesHeader.ID))
        criterias.opAnd(New Criteria(GetType(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 45))
        Dim arlResult As ArrayList = New SalesmanProfileFacade(User).Retrieve(criterias)

        If arlResult.Count > 0 Then
            Return CType(arlResult(0), SalesmanProfile)
        Else
            Return Nothing
        End If

    End Function

    Private Function GetLastCSOHireDate(SalesmanHeaderID As Integer) As DateTime
        Dim objDealer As Dealer = CType(Session("Dealer"), Dealer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDealerHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.No, "3")) 'resign
        criterias.opAnd(New Criteria(GetType(SalesmanDealerHistory), "SalesmanHeader.JobPosition.ID", MatchType.Exact, "40")) 'CSO
        criterias.opAnd(New Criteria(GetType(SalesmanDealerHistory), "SalesmanHeader.SalesIndicator", MatchType.Exact, "4"))
        criterias.opAnd(New Criteria(GetType(SalesmanDealerHistory), "Dealer.ID", MatchType.Exact, objDealer.ID))

        Dim sortColLastUpdate As SortCollection = New SortCollection
        sortColLastUpdate.Add(New Sort(GetType(SalesmanDealerHistory), "LastUpdateTime", Sort.SortDirection.DESC))


        Dim arlResult As ArrayList = New SalesmanDealerHistoryFacade(User).Retrieve(criterias, sortColLastUpdate)

        If arlResult.Count > 0 Then
            Return CType(arlResult(0), SalesmanDealerHistory).DateOut
        Else
            Return DateTime.MaxValue
        End If

    End Function

    Private Function UploadAndGetFileName(ByVal fileUpload As System.Web.UI.HtmlControls.HtmlInputFile, ByVal extension() As String) As String

        Dim fileInfo As FileInfo
        Dim fileName As String = String.Empty
        Dim fileSize As Long = 512000
        Dim errMessage As String = String.Empty
        'Dim listTrClass As List(Of TrClass) = New List(Of TrClass)
        'Dim listError As List(Of ErrorExcelUpload) = New List(Of ErrorExcelUpload)
        Dim resultUpload As String = helpers.UploadFile(fileUpload, "\SuratPengangkatan\", fileSize, extension, errMessage)
        Dim errArr() As String = resultUpload.Split("|")
        If errArr(0).ToLower().Equals("error") Then
            Throw New Exception(errArr(1))
        Else
            fileName = errArr(1)
        End If

        Return fileName

    End Function

    Private Sub lnkAppointmentLetter_Click(sender As Object, e As EventArgs) Handles lnkAppointmentLetter.Click
        Try
            If hdnAppointmentLetterPath.Value = String.Empty Then
                Throw New Exception("Surat Pengangkatan belum di Upload")
            End If

            Dim pdfDoc As New PdfDocument()
            pdfDoc.Options.FlateEncodeMode = PdfFlateEncodeMode.BestCompression
            pdfDoc.Options.UseFlateDecoderForJpegImages = PdfUseFlateDecoderForJpegImages.Automatic
            pdfDoc.Options.NoCompression = False
            pdfDoc.Options.CompressContentStreams = True

            Dim pages As PdfPages = GetPdfPages(hdnAppointmentLetterPath.Value)

            For Each Page As PdfPage In pages
                pdfDoc.AddPage(Page)
            Next

            Dim fileName As String = Guid.NewGuid().ToString().Substring(0, 5) & "_" & txtName.Text.Replace(" ", "_") & ".pdf"
            SaveFileToTempAndDownload(pdfDoc, fileName)


        Catch ex As Exception
            MessageBox.Show("Gagal dalam mendownload surat pengangkatan : " & ex.Message)
        End Try



    End Sub

    Private Function GetPdfPages(ByVal mainFilePath As String) As PdfPages
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Dim filePath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + mainFilePath

        Dim fileInfo As New FileInfo(filePath)
        Dim destFilePath As String = fileInfo.FullName
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                If (fileInfo.Exists) Then
                    Dim pdfDoc As PdfDocument = PdfReader.Open(filePath, PdfDocumentOpenMode.Import)
                    Return pdfDoc.Pages
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                End If
                imp.StopImpersonate()
            End If

        Catch ex As Exception
            MessageBox.Show(SR.DownloadFail(fileInfo.Name))

        End Try
    End Function

    Private Sub SaveFileToTempAndDownload(pdfDoc As PdfDocument, fileName As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                pdfDoc.Save(Server.MapPath("~/DataTemp/" & fileName))
                imp.StopImpersonate()
            End If

        Catch ex As Exception
            MessageBox.Show(SR.SaveFail(fileName))

        End Try

        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)

    End Sub

    Private Function GetKTPFromUi() As String
        Dim ktp As String = ""
        Dim objExist As SalesmanHeader
        Dim bolReturn As Boolean = False
        Dim objRenderPanel As RenderingProfile = New RenderingProfile
        Dim objProfileGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(strCurrProfile)

        Dim al As ArrayList = objRenderPanel.RetrieveProfileValue(Me, objProfileGroup, CType(EnumProfileType.ProfileType.CS, Short), User)

        For Each row As SalesmanProfile In al
            If row.ProfileHeader.ID = 29 Then
                ktp = row.ProfileValue
                Exit For
            End If
        Next
        Return ktp
    End Function

End Class