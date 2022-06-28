Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade

Imports System.Text
Imports System.IO

Public Class PopUpDPDocumentUpload
    Inherits System.Web.UI.Page

    Private Mode As String = "New"
    Protected countChk As Integer = 0
    Private objDealer As Dealer
    Private arlDocument As ArrayList = New ArrayList
    Private sessHelper As New SessionHelper

    Const sessDiscountProposalHeader As String = "PopUpDPDocumentUpload.sessDataDP"
    Const sessDiscountProposalDtlDocument As String = "PopUpDPDocumentUpload.sessDataDoc"
    Const sessDeleteDiscountProposalDtlDocument As String = "PopUpDPDocumentUpload.sessDeleteDataDoc"

    Dim objDiscountProposalHeader As New DiscountProposalHeader
    Dim objLoginUser As UserInfo

    Private TempDirectory As String = String.Empty
    Private TargetDirectory As String = String.Empty
    Private MAX_FILE_SIZE As Integer = 5120000
    Private Property SesDealer() As Dealer
        Get
            Return CType(sessHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sessHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then
            Try
                sessHelper.RemoveSession(sessDiscountProposalHeader)
            Catch
            End Try
            Try
                sessHelper.RemoveSession(sessDiscountProposalDtlDocument)
            Catch
            End Try
            Try
                sessHelper.RemoveSession(sessDeleteDiscountProposalDtlDocument)
            Catch
            End Try

            ViewState.Add("SortColCurrent", "ID")
            ViewState.Add("SortDirCurrent", Sort.SortDirection.ASC)

            hdnMode.Value = Request.QueryString("Mode")
            objDealer = Session("DEALER")
            If Not IsNothing(Request.QueryString("DiscountProposalHeaderID")) Then
                hdnDiscountProposalHeaderID.Value = Request.QueryString("DiscountProposalHeaderID")
                LoadDataDP(hdnDiscountProposalHeaderID.Value)

                ''0 = Baru
                ''1 = Validasi
                ''2 = Konfirmasi
                ''3 = Setuju
                ''4 = Selesai
                ''5 = Tolak
                ''6 = BatalValidasi
                ''7 = BatalKonfirmasi
                'Select Case objDiscountProposalHeader.Status
                '    Case 3, 4, 5
                '        btnSave.Visible = False
                '        dgUploadFile.ShowFooter = False
                '        dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = False
                '    Case Else
                '        btnSave.Visible = True
                'End Select
            End If
        End If
    End Sub

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
        End If
        If Mode = "New" Then  ' Login as Dealer
            objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Else
            objDealer = CType(sessHelper.GetSession("PopUpDPDocumentUpload.DEALER"), Dealer)
            If IsNothing(objDealer) Then
                objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            End If
        End If
        objLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"
    End Sub

    Private Sub LoadDataDP(intDiscountProposalHeaderID As Integer)
        Dim arrDiscountProposalDetailDocument As ArrayList

        objDiscountProposalHeader = New DiscountProposalHeaderFacade(User).Retrieve(intDiscountProposalHeaderID)
        If Not IsNothing(objDiscountProposalHeader) AndAlso objDiscountProposalHeader.ID > 0 Then
            sessHelper.SetSession(sessDiscountProposalHeader, objDiscountProposalHeader)

            hdnDiscountProposalHeaderID.Value = objDiscountProposalHeader.ID
            lblRegNumber.Text = objDiscountProposalHeader.ProposalRegNo
            lblDealerProposalNo.Text = objDiscountProposalHeader.DealerProposalNo

            objDealer = objDiscountProposalHeader.Dealer
            sessHelper.SetSession("PopUpDPDocumentUpload.DEALER", objDealer)
            lblDealerCode.Text = objDealer.DealerCode & " / " & objDealer.DealerName

            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), "DiscountProposalHeader.ID", MatchType.Exact, intDiscountProposalHeaderID))
            criterias2.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), "FileType", MatchType.Exact, 2))
            arrDiscountProposalDetailDocument = New DiscountProposalDetailDocumentFacade(User).Retrieve(criterias2)
            sessHelper.SetSession(sessDiscountProposalDtlDocument, arrDiscountProposalDetailDocument)
            BindGridDPUploadFile()

            btnSave.Enabled = False
            dgUploadFile.ShowFooter = False
            dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = False

            If Mode = "New" OrElse Mode = "Edit" Then
                btnSave.Enabled = True
                dgUploadFile.ShowFooter = True
                dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = True
            End If
        End If
    End Sub

    Private Sub dgUploadFile_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgUploadFile.ItemCommand
        Dim _arrDataUploadFile As ArrayList = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
        If IsNothing(_arrDataUploadFile) Then _arrDataUploadFile = New ArrayList

        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("UploadFile"), HtmlInputFile)
                Dim objPostedData As HttpPostedFile
                Dim objDiscountProposalDetailDocument As DiscountProposalDetailDocument = New DiscountProposalDetailDocument()
                Dim sFileName As String

                '========= Validasi  =======================================================================
                If IsNothing(FileUpload) OrElse FileUpload.Value = String.Empty Then
                    MessageBox.Show("Lampiran masih kosong")
                    Return
                End If
                Dim _filename As String = System.IO.Path.GetFileName(FileUpload.PostedFile.FileName)
                If _filename.Trim().Length <= 0 Then
                    MessageBox.Show("Upload file belum diisi\n")
                    Return
                End If
                If _filename.Trim().Length > 0 Then
                    If FileUpload.PostedFile.ContentLength > MAX_FILE_SIZE Then
                        MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                        Return
                    End If
                End If
                Dim ext As String = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName)
                If Not (ext.ToUpper() = ".PDF" OrElse
                        ext.ToUpper() = ".DOC" OrElse
                        ext.ToUpper() = ".DOCX" OrElse
                        ext.ToUpper() = ".PNG" OrElse
                        ext.ToUpper() = ".JPG" OrElse
                        ext.ToUpper() = ".JPEG" OrElse
                        ext.ToUpper() = ".XLS" OrElse
                        ext.ToUpper() = ".XLSX") Then
                    MessageBox.Show("Hanya menerima file format (PNG/JPG/JPEG/PDF/XLS/DOC)")
                    Return
                End If

                If Not IsNothing(FileUpload) OrElse FileUpload.Value <> String.Empty Then
                    objPostedData = FileUpload.PostedFile
                Else
                    objPostedData = Nothing
                End If

                If Not (IsNothing(objPostedData)) Then
                    sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                    If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                        MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                        BindGridDPUploadFile()
                        Return
                    End If

                    If Not FileIsExist(sFileName, _arrDataUploadFile) Then
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        Dim strDPPathConfig As String = "DiscountProposal\Fleet\"
                        Dim strDPPathFile As String = objDealer.DealerCode & "\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                        Dim strDestFile As String = strDPPathConfig & strDPPathFile '--Destination File                       

                        With objDiscountProposalDetailDocument
                            .DiscountProposalHeader = New DiscountProposalHeader()
                            .FileType = 2   '---ValueCode = LampiranPOSPK
                            .AttachmentData = objPostedData
                            .FileName = sFileName
                            .Path = strDestFile
                        End With

                        UploadAttachment(objDiscountProposalDetailDocument, TempDirectory)

                        _arrDataUploadFile.Add(objDiscountProposalDetailDocument)
                        sessHelper.SetSession(sessDiscountProposalDtlDocument, _arrDataUploadFile)
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If

            Case "Delete" 'Delete this datagrid item 
                Dim oDiscountProposalDetailDocument As DiscountProposalDetailDocument = CType(_arrDataUploadFile(e.Item.ItemIndex), DiscountProposalDetailDocument)
                If oDiscountProposalDetailDocument.ID > 0 Then
                    Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlDocument), ArrayList)
                    If IsNothing(arrDelete) Then arrDelete = New ArrayList
                    arrDelete.Add(oDiscountProposalDetailDocument)
                    sessHelper.SetSession(sessDeleteDiscountProposalDtlDocument, arrDelete)
                End If
                _arrDataUploadFile.RemoveAt(e.Item.ItemIndex)
                sessHelper.SetSession(sessDiscountProposalDtlDocument, _arrDataUploadFile)

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)

        End Select

        BindGridDPUploadFile()
    End Sub

    Private Sub UploadAttachment(ByVal ObjAttachment As DiscountProposalDetailDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(ObjAttachment.Path) Then
                    finfo = New FileInfo(TargetPath + ObjAttachment.Path)

                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.Path)
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Sub BindGridDPUploadFile()
        arlDocument = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
        If IsNothing(arlDocument) Then arlDocument = New ArrayList()
        dgUploadFile.DataSource = arlDocument
        dgUploadFile.DataBind()
    End Sub

    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each obj As DiscountProposalDetailDocument In AttachmentCollection
                If Not IsNothing(obj.AttachmentData) Then
                    If Path.GetFileName(obj.AttachmentData.FileName).ToUpper.Trim = FileName.ToUpper.Trim Then
                        bResult = True
                        Exit For
                    End If
                Else
                    If obj.FileName.ToUpper.Trim = FileName.ToUpper.Trim Then
                        bResult = True
                        Exit For
                    End If
                End If
            Next
        End If
        Return bResult
    End Function

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Private Sub dgUploadFile_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUploadFile.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgUploadFile.CurrentPageIndex * dgUploadFile.PageSize)

            Dim arrUpload As ArrayList = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
            If Not IsNothing(arrUpload) AndAlso arrUpload.Count > 0 Then
                Dim objDiscountProposalDtlDocument As DiscountProposalDetailDocument = arrUpload(e.Item.ItemIndex)
                Dim lblFileName As Label = CType(e.Item.FindControl("lblFileName"), Label)
                lblFileName.Text = Path.GetFileName(objDiscountProposalDtlDocument.FileName)
            End If
        End If
    End Sub

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If Mode = "New" Then
            If (sessHelper.GetSession(sessDiscountProposalDtlDocument) Is Nothing) Then
                sb.Append("- Data Dokumen Discount Proposal belum ada\n")
            Else
                If CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList).Count = 0 Then
                    sb.Append("- Data Dokumen Discount Proposal belum ada\n")
                End If
            End If
        End If

        Return sb.ToString()
    End Function

    Private Sub CommitAttachment(ByVal AttachmentCollection As ArrayList)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim TargetFInfo As FileInfo
        Dim TempFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As DiscountProposalDetailDocument In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Then
                        TargetFInfo = New FileInfo(TargetDirectory + obj.Path)
                        TempFInfo = New FileInfo(TempDirectory + obj.Path)

                        If TempFInfo.Exists Then
                            If Not TargetFInfo.Directory.Exists Then
                                Directory.CreateDirectory(TargetFInfo.DirectoryName)
                            End If
                            TempFInfo.MoveTo(TargetFInfo.FullName)
                        End If
                        obj.AttachmentData.SaveAs(TargetDirectory + obj.Path)
                    End If
                Next

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RemoveDocumentAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As DiscountProposalDetailDocument In AttachmentCollection
                    finfo = New FileInfo(TargetPath + obj.Path)
                    If finfo.Exists Then
                        finfo.Delete()
                    End If
                Next
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If

        Dim _result As Integer = 0
        objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHeader), DiscountProposalHeader)
        arlDocument = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
        Dim arlDelDocument As New ArrayList

        If Mode = "Edit" Then
            arlDelDocument = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlDocument), ArrayList)
            If IsNothing(arlDelDocument) Then arlDelDocument = New ArrayList
            _result = New DiscountProposalDetailDocumentFacade(User).UpdateTransaction(objDiscountProposalHeader, arlDocument, arlDelDocument)
        Else
            _result = New DiscountProposalDetailDocumentFacade(User).InsertTransaction(objDiscountProposalHeader, arlDocument)
        End If

        If _result > 0 Then
            CommitAttachment(arlDocument)
            If Request.QueryString("Mode") = "Edit" Then
                If Not IsNothing(arlDelDocument) Then
                    RemoveDocumentAttachment(arlDelDocument, TargetDirectory)
                End If
            End If
            ClearAll()
            pnlRunCloseWindow.Visible = True
        Else
            MessageBox.Show("Simpan Dokumen Gagal")
        End If
    End Sub

    Private Sub ClearAll()
        hdnMode.Value = ""
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        pnlRunCloseWindow2.Visible = True
    End Sub

End Class
