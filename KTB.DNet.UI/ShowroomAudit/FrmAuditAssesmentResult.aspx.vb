Imports System.IO

#Region "Custom Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.ShowroomAudit
Imports KTB.DNet.Utility
#End Region

Public Class FrmAuditAssesmentResult
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblAuditorName As System.Web.UI.WebControls.Label
    Protected WithEvents dtgPhotoList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents lblAuditorType As System.Web.UI.WebControls.Label
    Protected WithEvents lblAuditScheduleStartDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblAuditScheduleSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents lblAuditScheduleEndDate As System.Web.UI.WebControls.Label
    Protected WithEvents hypDownloadAssesmentItem As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hypDownloadJuklakFile As System.Web.UI.WebControls.HyperLink
    Protected WithEvents btnRilis As System.Web.UI.WebControls.Button
    Protected WithEvents fileUploadAssesmentResult As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblAssesmentResultFile As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblUploadFoto As System.Web.UI.WebControls.Label
    Protected WithEvents lblFotoPerbaikan As System.Web.UI.WebControls.Label
    Protected WithEvents dtgFotoPerbaikan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents pnlDealerSection As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlKTBSection As System.Web.UI.WebControls.Panel
    Protected WithEvents lblUploadHasilPenilaian As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Const SES_objAuditScheduleDealer As String = "SES_objAuditScheduleDealer"
#End Region

#Region "Deklarasi"
    Dim sHelper As New SessionHelper
#End Region

#Region "Sessions"
    Private Property IDEdit() As String
        Get
            Dim objValue = sHelper.GetSession("IDEdit")
            If objValue Is Nothing Then
                objValue = String.Empty
            End If

            Return objValue
        End Get
        Set(ByVal Value As String)
            sHelper.SetSession("IDEdit", Value)
        End Set
    End Property
    Private Property SesPhotoList() As ArrayList
        Get
            Dim arlListPhoto As ArrayList = sHelper.GetSession("arlListPhoto")
            If arlListPhoto Is Nothing Then
                arlListPhoto = New ArrayList
                sHelper.SetSession("arlListPhoto", arlListPhoto)
            End If

            Return arlListPhoto
        End Get
        Set(ByVal Value As ArrayList)
            sHelper.SetSession("arlListPhoto", Value)
        End Set
    End Property

    Private Property SesAuditScheduleDealer() As AuditScheduleDealer
        Get
            Return CType(sHelper.GetSession(SES_objAuditScheduleDealer), AuditScheduleDealer)
        End Get
        Set(ByVal Value As AuditScheduleDealer)
            sHelper.SetSession(SES_objAuditScheduleDealer, Value)
        End Set
    End Property

    Private Property SesDealer() As Dealer
        Get
            Return CType(sHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sHelper.SetSession("DEALER", Value)
        End Set
    End Property
#End Region

#Region "Custom Methods"
    Dim maxPicSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(SalesmanHeader.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= maxPicSize)
        Return (containImage AndAlso sizeValid)
    End Function

    Private Function UploadFile(ByVal file As HttpPostedFile) As Byte()
        Dim nResult() As Byte

        Try
            If IsValidPhoto(file) Then
                Dim inStream As System.IO.Stream = file.InputStream()
                'Dim ByteRead(SalesmanHeader.MAX_PHOTO_SIZE) As Byte
                Dim ByteRead(maxPicSize) As Byte
                'Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, SalesmanHeader.MAX_PHOTO_SIZE)
                Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, maxPicSize)
                If ReadCount = 0 Then
                    Throw New InvalidConstraintException(SR.DataNotFound("Photo"))
                End If
                ReDim nResult(ReadCount)
                Array.Copy(ByteRead, nResult, ReadCount)
            Else
                'MessageBox.Show("Bukan file gambar atau Ukuran file > " & _
                '                        CType(SalesmanHeader.MAX_PHOTO_SIZE / 1024, String) & "KB")
                MessageBox.Show("Bukan file gambar atau Ukuran file > " & _
                                        CType(maxPicSize, String) & " bytes")
            End If
        Catch
            'Throw
        End Try

        Return nResult
    End Function

    Private Sub BindToControl(ByVal IDEdit As Integer)
        Dim objAuditScheduleDealer As AuditScheduleDealer = New AuditScheduleDealerFacade(User).Retrieve(IDEdit)
        SesAuditScheduleDealer = objAuditScheduleDealer

        lblPeriode.Text = objAuditScheduleDealer.AuditSchedule.AuditParameter.Period.ToString()
        lblDealerCode.Text = objAuditScheduleDealer.Dealer.ID.ToString() + " / " + objAuditScheduleDealer.Dealer.DealerName

        If Not objAuditScheduleDealer.AuditScheduleAuditor Is Nothing Then
            lblAuditScheduleStartDate.Text = objAuditScheduleDealer.AuditScheduleAuditor.StartDate.ToString("dd/MM/yyyy")
            lblAuditScheduleEndDate.Text = objAuditScheduleDealer.AuditScheduleAuditor.EndDate.ToString("dd/MM/yyyy")
        Else
            lblAuditScheduleStartDate.Visible = False
            lblAuditScheduleSeparator.Visible = False
            lblAuditScheduleEndDate.Visible = False
        End If

        'EnumDealerTittle.RetrieveTitleForPrivilege()
        'Dim a As ArrayList = new EnumDealerTittle().
        If Not objAuditScheduleDealer.AuditScheduleAuditor Is Nothing Then
            lblAuditorType.Text = CType(objAuditScheduleDealer.AuditScheduleAuditor.AuditorType, EnumDealerTittle.DealerTittle).ToString()
            lblAuditorName.Text = objAuditScheduleDealer.AuditScheduleAuditor.Auditor
        Else
            lblAuditorType.Text = String.Empty
            lblAuditorName.Text = String.Empty
        End If

        'bug 1674
        sHelper.SetSession("objAuditScheduleDealer", objAuditScheduleDealer)

        hypDownloadAssesmentItem.Text = System.IO.Path.GetFileName(objAuditScheduleDealer.AuditSchedule.AuditParameter.AssessmentItem)
        Dim filePath As String = String.Empty 'KTB.DNet.Lib.WebConfig.GetValue("Audit") & "\"
        filePath += objAuditScheduleDealer.AuditSchedule.AuditParameter.AssessmentItem
        hypDownloadAssesmentItem.NavigateUrl = "../download.aspx?file=" & filePath

        hypDownloadJuklakFile.Text = System.IO.Path.GetFileName(objAuditScheduleDealer.AuditSchedule.AuditParameter.JukLakFile)
        filePath = String.Empty 'KTB.DNet.Lib.WebConfig.GetValue("Audit") & "\"
        filePath += objAuditScheduleDealer.AuditSchedule.AuditParameter.JukLakFile
        hypDownloadJuklakFile.NavigateUrl = "../download.aspx?file=" & filePath

        lblAssesmentResultFile.Text = objAuditScheduleDealer.AssessmentFile

        'Bind Foto Awal datagrid
        If objAuditScheduleDealer.AuditSchedule.AuditParameter.AuditParameterPhotos Is Nothing Then
            Dim arl As ArrayList = New ArrayList
            dtgPhotoList.DataSource = arl
            SesPhotoList = arl
        Else
            Dim arl As ArrayList = objAuditScheduleDealer.AuditSchedule.AuditParameter.AuditParameterPhotos
            dtgPhotoList.DataSource = arl
            SesPhotoList = arl
        End If

        'Bind Foto Perbaikan datagrid        

        Dim arlReports As ArrayList = New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealerReport), "AuditScheduleDealerID", MatchType.Exact, sHelper.GetSession("IDEdit")))
        arlReports = New AuditScheduleDealerReportFacade(User).Retrieve(criterias)

        'bug 1674
        sHelper.SetSession("arlReports", arlReports)

        'If Not objAuditScheduleDealer.AuditScheduleDealerReports Is Nothing Then
        If arlReports.Count > 0 Then
            'dtgFotoPerbaikan.DataSource = objAuditScheduleDealer.AuditScheduleDealerReports
            dtgFotoPerbaikan.DataSource = arlReports
            dtgFotoPerbaikan.DataBind()
        Else
            dtgFotoPerbaikan.DataSource = New ArrayList
            dtgFotoPerbaikan.DataBind()
        End If

        Me.IDEdit = IDEdit
        dtgPhotoList.DataBind()
    End Sub

    Private Function IsAuditorIsCurrentLogonDealer() As Boolean
        If IsLoginAsDealer() Then
            Dim bResult As Boolean = False

            If Not SesAuditScheduleDealer.AuditScheduleAuditor Is Nothing Then
                If SesAuditScheduleDealer.AuditScheduleAuditor.Auditor.Trim().ToLower() = _
                    SesDealer.DealerCode.Trim().ToLower() Then
                    bResult = True
                End If
            End If
            Return bResult
        End If
    End Function

    Private Function IsLoginAsDealerButAuditorIsKTB() As Boolean
        If IsLoginAsDealer() Then
            Dim bResult As Boolean = False

            If Not SesAuditScheduleDealer.AuditScheduleAuditor Is Nothing Then
                If CType(SesAuditScheduleDealer.AuditScheduleAuditor.AuditorType, EnumDealerTittle.DealerTittle) = EnumDealerTittle.DealerTittle.KTB Then
                    bResult = True
                End If
            End If
            Return bResult
        End If
    End Function

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function
#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Dim IDEdit As String = Me.IDEdit
            If IDEdit <> "" Then
                Me.IDEdit = IDEdit

                BindToControl(CInt(IDEdit))
                If IsAuditorIsCurrentLogonDealer() Then
                    lblUploadFoto.Visible = True
                    dtgPhotoList.Visible = True
                    lblUploadFoto.Text = "Upload Foto Awal"
                    lblFotoPerbaikan.Text = "Upload Foto Perbaikan"
                    pnlDealerSection.Visible = True
                    lblUploadHasilPenilaian.Text = "Upload Form"
                ElseIf IsLoginAsDealerButAuditorIsKTB() Then
                    pnlKTBSection.Visible = False
                Else
                    lblUploadFoto.Visible = True
                    dtgPhotoList.Visible = True
                    pnlDealerSection.Visible = False
                    pnlKTBSection.Visible = True
                End If
            End If
        End If
    End Sub

    Private Function SaveFile(ByVal _filename As String, ByVal fileUploadControl As HtmlInputFile) As Boolean
        Dim nResult As Boolean = False
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + _
                                KTB.DNet.Lib.WebConfig.GetValue("AuditAssesmentResult") + _
                                "\" + Me.IDEdit + "\" + System.IO.Path.GetFileName(_filename)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        'cek maxFileSize first
        Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

        If fileUploadControl.PostedFile.ContentLength > maxFileSize Then
            MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
            Exit Function
        End If

        Dim finfo As FileInfo
        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(DestFile)

                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                If finfo.Exists Then
                    finfo.Delete()
                End If

                fileUploadControl.PostedFile.SaveAs(DestFile)
                nResult = True
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            nResult = False
            Throw ex
        End Try
        Return nResult
    End Function

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        '--cekHeader
        Dim nresult As Integer = 0
        Dim isEditPhoto As Boolean = False

        'If assesmentFilePath.Length <= 0 Then
        '    MessageBox.Show("File Upload Hasil Penilaian harus diisi.")
        '    Return
        'End If
        'For i As Integer = 0 To dtgPhotoList.Items.Count - 1
        '    Dim fileUploadControl As HtmlInputFile = dtgPhotoList.Items(i).FindControl("fileEditItemImage")
        '    Dim lblDesc As Label = dtgPhotoList.Items(i).FindControl("lblDesc")
        '    If Not fileUploadControl Is Nothing Then
        '        Dim imageFilePath As String = fileUploadControl.PostedFile.FileName.Trim()
        '        If imageFilePath.Length <= 0 Then
        '            MessageBox.Show("File Upload Foto " + lblDesc.Text + " harus diisi.")
        '            Return
        '        End If
        '    End If
        'Next


        If (IsAuditorIsCurrentLogonDealer()) Or (Not IsLoginAsDealerButAuditorIsKTB()) Then
            Dim assesmentFilePath As String = fileUploadAssesmentResult.PostedFile.FileName.Trim()
            'upload assesment file path
            If assesmentFilePath.Length > 0 Then
                If SaveFile(assesmentFilePath, fileUploadAssesmentResult) Then
                    SesAuditScheduleDealer.AssessmentFile = Path.GetFileName(assesmentFilePath)
                Else
                    Exit Sub
                End If
            End If
        End If


        If (IsAuditorIsCurrentLogonDealer()) Or (Not IsLoginAsDealerButAuditorIsKTB()) Then
            'upload all photo files        
            For i As Integer = 0 To dtgPhotoList.Items.Count - 1
                Dim fileUploadControl As HtmlInputFile = dtgPhotoList.Items(i).FindControl("fileEditItemImage")
                Dim lblDesc As Label = dtgPhotoList.Items(i).FindControl("lblDesc")
                If Not fileUploadControl Is Nothing Then
                    Dim imageFilePath As String = fileUploadControl.PostedFile.FileName.Trim()

                    If imageFilePath.Length > 0 Then
                        Dim objAuditfoto As AuditParameterPhoto
                        objAuditfoto = SesAuditScheduleDealer.AuditSchedule.AuditParameter.AuditParameterPhotos(i)
                        Dim imageFile As Byte()
                        imageFile = UploadFile(fileUploadControl.PostedFile)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealerReport), "AuditScheduleDealerID", MatchType.Exact, sHelper.GetSession("IDEdit")))
                        Dim arlreportx As ArrayList = New AuditScheduleDealerReportFacade(User).Retrieve(criterias)
                        Dim objAuditScheduleReport As AuditScheduleDealerReport
                        If arlreportx.Count > 0 Then
                            'objAuditScheduleReport = arlreportx(0)
                            objAuditScheduleReport = New AuditScheduleDealerReportFacade(User).Retrieve(CInt(CType(dtgPhotoList.Items(i).FindControl("IDFoto"), Label).Text))
                        Else
                            objAuditScheduleReport = New AuditScheduleDealerReport
                            objAuditfoto.AuditScheduleDealerReports.Add(objAuditScheduleReport)
                        End If
                        'If (objAuditfoto.AuditScheduleDealerReports.Count > 0) Then
                        '    objAuditScheduleReport = CType(objAuditfoto.AuditScheduleDealerReports(0), AuditScheduleDealerReport)
                        'Else
                        '    objAuditScheduleReport = New AuditScheduleDealerReport
                        '    objAuditfoto.AuditScheduleDealerReports.Add(objAuditScheduleReport)
                        'End If
                        objAuditScheduleReport.ItemImage = imageFile
                        objAuditScheduleReport.ItemDesc = imageFilePath.Substring(imageFilePath.LastIndexOf("\") + 1)
                        If arlreportx.Count > 0 Then
                            Dim result As Integer = New AuditScheduleDealerReportFacade(User).Update(objAuditScheduleReport)
                            isEditPhoto = True
                        End If
                    End If
                End If
            Next
        End If

        If IsAuditorIsCurrentLogonDealer() Or IsLoginAsDealerButAuditorIsKTB() Then
            For i As Integer = 0 To dtgFotoPerbaikan.Items.Count - 1
                Dim fileUploadControl As HtmlInputFile = dtgFotoPerbaikan.Items(i).FindControl("fileEditItemImage")
                Dim lblDesc As Label = dtgFotoPerbaikan.Items(i).FindControl("lblDesc")
                If Not fileUploadControl Is Nothing Then
                    Dim imageFilePath As String = fileUploadControl.PostedFile.FileName.Trim()

                    If imageFilePath.Length > 0 Then
                        Dim imageFile As Byte()
                        imageFile = UploadFile(fileUploadControl.PostedFile)
                        Dim objAuditScheduleReport As AuditScheduleDealerReport
                        objAuditScheduleReport = CType(SesAuditScheduleDealer.AuditScheduleDealerReports(i), AuditScheduleDealerReport)
                        objAuditScheduleReport.ItemImageReparation = imageFile
                        objAuditScheduleReport.ItemDesc = imageFilePath.Substring(imagefilepath.LastIndexOf("\") + 1)
                    End If
                End If
            Next
        End If


        Dim facade As New AuditScheduleDealerFacade(User)
        facade.UpdateAuditAssesmentResult(SesAuditScheduleDealer, isEditPhoto)

        If nresult <> -1 Then
            MessageBox.Show(SR.SaveSuccess)
            'bug 1471
            'Response.Redirect("FrmListAuditSchedule.aspx")
        Else
            MessageBox.Show(SR.SaveFail)
        End If

    End Sub

    Private Sub dtgPhotoList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPhotoList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ctlImg As System.Web.UI.WebControls.Image = e.Item.FindControl("imgEditItemImage")
            Dim objAuditScheduleDealer As AuditScheduleDealer = SesAuditScheduleDealer
            Dim objAuditParameterPhoto As AuditParameterPhoto = CType(objAuditScheduleDealer.AuditSchedule.AuditParameter.AuditParameterPhotos(e.Item.ItemIndex), AuditParameterPhoto)

            If (objAuditParameterPhoto.AuditScheduleDealerReports.Count > 0) Then
                'Dim objAuditReport As AuditScheduleDealerReport = CType(objAuditParameterPhoto.AuditScheduleDealerReports(0), AuditScheduleDealerReport)
                'Dim arlreport As ArrayList = New AuditScheduleDealerReportFacade(User)
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AuditScheduleDealerReport), "AuditScheduleDealerID", MatchType.Exact, sHelper.GetSession("IDEdit")))
                criterias.opAnd(New Criteria(GetType(AuditScheduleDealerReport), "AuditParameterPhotoID", MatchType.Exact, objAuditParameterPhoto.ID))
                Dim arlreportx As ArrayList = New AuditScheduleDealerReportFacade(User).Retrieve(criterias)
                If arlreportx.Count > 0 Then
                    Dim objAuditReport As AuditScheduleDealerReport = arlreportx(0)
                    CType(e.Item.FindControl("IDFoto"), Label).Text = objAuditReport.ID.ToString

                    If Not objAuditReport.ItemImage Is Nothing Then
                        If (Not ctlImg Is Nothing) Then
                            ctlImg.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & objAuditReport.ID.ToString & "&type=" & "AuditScheduleDealer"
                        End If
                    Else
                        If (Not ctlImg Is Nothing) Then
                            ctlImg.Visible = False
                        End If
                    End If

                    CType(e.Item.FindControl("IDFoto"), Label).Visible = False
                Else
                    If (Not ctlImg Is Nothing) Then
                        ctlImg.Visible = False
                    End If

                End If

            Else
                If (Not ctlImg Is Nothing) Then
                    ctlImg.Visible = False
                End If
            End If

            'bug 1674
            Dim oDealer As Dealer = sHelper.GetSession("DEALER")
            Dim objAuditSchedule As AuditScheduleDealer = CType(sHelper.GetSession("objAuditScheduleDealer"), AuditScheduleDealer)
            If Not IsNothing(objAuditSchedule) Then
                If Not IsNothing(objAuditScheduleDealer.AuditScheduleAuditor) Then
                    If oDealer.Title = EnumDealerTittle.DealerTittle.KTB And objAuditScheduleDealer.AuditScheduleAuditor.AuditorType = EnumDealerTittle.DealerTittle.DEALER Then
                        Dim htmlInput As HtmlInputFile = CType(e.Item.FindControl("fileEditItemImage"), HtmlInputFile)
                        htmlInput.Visible = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnRilis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRilis.Click
        Dim arlChecked As New ArrayList
        Dim objAuditScheduler As AuditScheduleDealer = SesAuditScheduleDealer
        arlChecked.Add(objAuditScheduler)

        If (New AuditScheduleDealerFacade(User).UpdateTransaction(arlChecked) <> -1) Then
            MessageBox.Show("Data berhasil dirilis")
        Else
            MessageBox.Show("Data gagal dirilis")
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmListAuditSchedule.aspx?isBack=1")
    End Sub

    Private Sub dtgFotoPerbaikan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFotoPerbaikan.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ctlImg As System.Web.UI.WebControls.Image = e.Item.FindControl("imgEditItemImage")
            Dim objAuditScheduleDealer As AuditScheduleDealer = SesAuditScheduleDealer

            If (objAuditScheduleDealer.AuditScheduleDealerReports.Count > 0) Then
                Dim objAuditReport As AuditScheduleDealerReport = CType(objAuditScheduleDealer.AuditScheduleDealerReports(e.Item.ItemIndex), AuditScheduleDealerReport)
                If Not objAuditReport.ItemImageReparation Is Nothing Then
                    If (Not ctlImg Is Nothing) Then
                        ctlImg.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & e.Item.ItemIndex.ToString & "&type=" & "FotoPerbaikanAuditScheduleDealer"
                    End If
                Else
                    If (Not ctlImg Is Nothing) Then
                        ctlImg.Visible = False
                    End If
                End If
            Else
                If (Not ctlImg Is Nothing) Then
                    ctlImg.Visible = False
                End If
            End If

            'bug 1674
            Dim oDealer As Dealer = sHelper.GetSession("DEALER")
            If (objAuditScheduleDealer.AuditScheduleDealerReports.Count > 0) Then
                If Not IsNothing(objAuditScheduleDealer.AuditScheduleAuditor) Then
                    If oDealer.Title = EnumDealerTittle.DealerTittle.KTB And objAuditScheduleDealer.AuditScheduleAuditor.AuditorType = EnumDealerTittle.DealerTittle.DEALER Then
                        Dim htmlInput As HtmlInputFile = CType(e.Item.FindControl("fileEditItemImage"), HtmlInputFile)
                        htmlInput.Visible = False
                    End If
                End If
            End If
        End If
    End Sub

End Class
