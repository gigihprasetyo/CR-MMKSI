#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.Text
Imports KTB.DNet.SAP
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit

Imports System.Collections.Generic
Imports System.Linq
Imports System.IO

#End Region

Public Class FrmInputBabitIklanReport
    Inherits System.Web.UI.Page

#Region "Global Var"
    Dim objBabitReportHeader As BabitReportHeader
    Private objDealer As Dealer
    Private sessionHelper As New SessionHelper
    Dim objLoginUser As UserInfo
    Private TargetDirectory As String
    Private TempDirectory As String
    Private sessionIklan As String = "FrmInputBabitIklanReport.sessionIklan"
    Private sessionFiles As String = "FrmInputBabitIklanReport.sessionFiles"
    Private sessionBabitHeaderID As String = "FrmInputBabitIklanReport.oBHID"
    Private sessionDealer As String = "FrmInputBabitIklanReport.DEALER"
    Private MAX_FILE_SIZE As Integer = 5120000
    Private Mode As String = "New"
#End Region

    Private Property SesDealer() As Dealer
        Get
            Return CType(sessionHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sessionHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
            btnBack.Visible = True
        End If
        If Mode = "New" Then  ' Login as Dealer
            objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        Else
            objDealer = CType(sessionHelper.GetSession(sessionDealer), Dealer)
        End If
        objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        objLoginUser = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"
    End Sub

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Mode = "New" Then
            If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Input_Iklan_Laporan_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT LAPORAN BABIT IKLAN")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Laporan_Display_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT LAPORAN BABIT IKLAN")
            End If
        End If
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
            lblDealerCodeName.Text = objDealer.DealerCode & " - " & objDealer.DealerName
            lblPopUpRegNumber.Attributes("onclick") = "ShowPopUpTO()"
            sessionHelper.SetSession(sessionIklan, New ArrayList)
            sessionHelper.SetSession(sessionFiles, New ArrayList)
            sessionHelper.SetSession(sessionBabitHeaderID, 0)
            BindMonth()
            BindYear()
            BindDgFiles()
            BindDgMedia()
            If Not IsNothing(Request.QueryString("Mode")) Then
                LoadData()
            End If
        End If
        disableAll()
    End Sub

    Private Sub LoadData()
        Mode = Request.QueryString("Mode")
        If Mode <> "New" Then
            objBabitReportHeader = New BabitReportHeaderFacade(User).Retrieve(CInt(Request.QueryString("BabitReportHeaderID")))
            objDealer = objBabitReportHeader.Dealer
            sessionHelper.SetSession(sessionDealer, objDealer)

            hdnNoReg.Value = objBabitReportHeader.BabitHeader.BabitRegNumber
            hdnNoReg_ValueChanged(Nothing, Nothing)

            Dim crit As New CriteriaComposite(New Criteria(GetType(BabitReportDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(BabitReportDocument), "BabitReportHeader.ID", MatchType.Exact, objBabitReportHeader.ID))
            Dim arlDoc As ArrayList = New BabitReportDocumentFacade(User).Retrieve(crit)
            sessionHelper.SetSession(sessionFiles, IIf(IsNothing(arlDoc), New ArrayList, arlDoc))

            BindDgFiles()
            lblPopUpRegNumber.Visible = False
            txtNoReg.Enabled = False

            If Mode = "Detail" Then
                dgUploadFile.ShowFooter = False
                dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = False

                btnSave.Enabled = False
            Else
                btnSave.Enabled = True
            End If
            btnBack.Visible = True
        End If
    End Sub

    Private Sub disableAll()
        txtNoReg.Enabled = False
        txtTemporaryOutlet.Enabled = False
        txtNomorSurat.Enabled = False
        ddlMonth.Enabled = False
        ddlYear.Enabled = False
    End Sub

    Private Sub clearAll()
        txtNoReg.Text = ""
        txtTemporaryOutlet.Text = ""
        txtNomorSurat.Text = ""
        ddlMonth.SelectedIndex = 0
        ddlYear.SelectedIndex = 0
        sessionHelper.SetSession(sessionIklan, New ArrayList)
        sessionHelper.SetSession(sessionFiles, New ArrayList)
    End Sub

    Private Sub BindDgMedia()
        dgIklan.DataSource = CType(sessionHelper.GetSession(sessionIklan), ArrayList)
        dgIklan.DataBind()
    End Sub

    Private Sub BindDgFiles()
        dgUploadFile.DataSource = CType(sessionHelper.GetSession(sessionFiles), ArrayList)
        dgUploadFile.DataBind()
    End Sub

    Private Sub BindMonth()
        Dim al As ArrayList = enumMonthGet.RetriveMonth()
        ddlMonth.DataSource = enumMonthGet.RetriveMonth()
        ddlMonth.DataValueField = "ValStatus"
        ddlMonth.DataTextField = "NameStatus"
        ddlMonth.DataBind()

        ddlMonth.SelectedValue = DateTime.Now.Month - 1
    End Sub

    Private Sub BindYear()
        Dim a As Integer
        Dim now As DateTime = DateTime.Now
        For a = -1 To 1
            ddlYear.Items.Insert(0, New ListItem(now.AddYears(a).Year, now.AddYears(a).Year))
        Next
        ddlYear.SelectedValue = now.Year
    End Sub

    Protected Sub hdnNoReg_ValueChanged(sender As Object, e As EventArgs) Handles hdnNoReg.ValueChanged
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(BabitHeader), "BabitRegNumber", MatchType.Exact, hdnNoReg.Value))
        Dim arrBH As ArrayList = New BabitHeaderFacade(User).Retrieve(criteria)
        If arrBH.Count > 0 Then
            Dim oBH As BabitHeader = arrBH(0)
            txtNoReg.Text = hdnNoReg.Value
            lblDealerCodeName.Text = oBH.Dealer.DealerCode & " - " & oBH.Dealer.DealerName
            If Not IsNothing(oBH.DealerBranch) Then
                txtTemporaryOutlet.Text = oBH.DealerBranch.DealerBranchCode
                lblNamaCabang.Text = oBH.DealerBranch.Name
            End If
            lblArea.Text = oBH.Dealer.Area2.Description
            txtNomorSurat.Text = oBH.BabitDealerNumber
            ddlMonth.SelectedValue = oBH.PeriodStart.Month
            ddlYear.SelectedValue = oBH.PeriodEnd.Year

            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(BabitIklanDetail), "BabitHeader.ID", MatchType.Exact, oBH.ID))
            Dim arrIklanDetail As ArrayList = New BabitIklanDetailFacade(User).Retrieve(crit)
            sessionHelper.SetSession(sessionIklan, arrIklanDetail)
            sessionHelper.SetSession(sessionBabitHeaderID, oBH.ID)
        End If
        BindDgMedia()
    End Sub

    Protected Sub dgUploadFile_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgUploadFile.ItemCommand
        Dim _arrDataUploadFile As ArrayList = CType(sessionHelper.GetSession(sessionFiles), ArrayList)
        If IsNothing(_arrDataUploadFile) Then _arrDataUploadFile = New ArrayList

        Select Case e.CommandName
            Case "add"
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("UploadFile"), HtmlInputFile)
                Dim txtKeterangan As TextBox = CType(e.Item.FindControl("txtKeterangan"), TextBox)
                Dim objPostedData As HttpPostedFile
                Dim objBabitDocument As BabitReportDocument = New BabitReportDocument()
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
                If Not (ext.ToUpper() = ".JPG" OrElse ext.ToUpper() = ".JPEG" OrElse ext.ToUpper() = ".PDF") Then
                    MessageBox.Show("Hanya menerima file format (PDF/JPG/JPEG)")
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
                        BindDgFiles()
                        Return
                    End If

                    Dim strDealerCode As String = lblDealerCodeName.Text

                    If Not FileIsExist(sFileName, _arrDataUploadFile) Then
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        Dim strBabitPathConfig As String = KTB.DNet.Lib.WebConfig.GetValue("BabitFileDirectory")
                        Dim strBabitPathFile As String = "\BABIT\" & objDealer.DealerCode & "\IklanReport\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                        Dim strDestFile As String = strBabitPathConfig & strBabitPathFile '--Destination File                       

                        objBabitDocument.BabitReportHeader = New BabitReportHeader()
                        objBabitDocument.AttachmentData = objPostedData
                        objBabitDocument.FileName = sFileName
                        objBabitDocument.Path = strDestFile
                        objBabitDocument.FileDescription = IIf(txtKeterangan.Text.Trim = String.Empty, "Dokumen Laporan Babit Iklan", txtKeterangan.Text.Trim)

                        UploadAttachment(objBabitDocument, TempDirectory)

                        _arrDataUploadFile.Add(objBabitDocument)
                        sessionHelper.SetSession(sessionFiles, _arrDataUploadFile)
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If

            Case "Delete" 'Delete this datagrid item 
                Dim oBabitDocument As BabitReportDocument = CType(_arrDataUploadFile(e.Item.ItemIndex), BabitReportDocument)
                If oBabitDocument.ID > 0 Then
                    Dim arrDelete As ArrayList = CType(sessionHelper.GetSession("sessDelDataUploadFile"), ArrayList)
                    If IsNothing(arrDelete) Then arrDelete = New ArrayList
                    arrDelete.Add(oBabitDocument)
                    sessionHelper.SetSession("sessDelDataUploadFile", arrDelete)
                End If

                RemoveBabitAttachment(CType(_arrDataUploadFile(e.Item.ItemIndex), BabitReportDocument), TempDirectory)
                _arrDataUploadFile.RemoveAt(e.Item.ItemIndex)

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)
        End Select

        BindDgFiles()
    End Sub

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each obj As BabitReportDocument In AttachmentCollection
                If Not IsNothing(obj.AttachmentData) Then
                    If Path.GetFileName(obj.AttachmentData.FileName) = FileName Then
                        bResult = True
                        Exit For
                    End If
                End If
            Next
        End If
        Return bResult
    End Function

    Private Sub UploadAttachment(ByRef ObjAttachment As BabitReportDocument, ByVal TargetPath As String)
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

    Private Sub RemoveBabitAttachment(ByVal ObjAttachment As BabitReportDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(TargetPath + ObjAttachment.Path)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

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
                For Each obj As BabitReportDocument In AttachmentCollection
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

    Private Function Validate() As Boolean
        If CType(sessionHelper.GetSession(sessionBabitHeaderID), Integer) <= 0 Then
            MessageBox.Show("Nomor Registrasi harus diisi")
            Return False
        End If
        If dgUploadFile.Items.Count < 1 Then
            MessageBox.Show("Lampiran harus di isi")
            Return False
        End If

        Return True
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not Validate() Then
            Exit Sub
        End If

        Dim _return As Integer = 0
        Dim oBHR As BabitReportHeader = New BabitReportHeader()
        Dim arrBDR As ArrayList = CType(sessionHelper.GetSession(sessionFiles), ArrayList)

        Dim arrDocDelete As ArrayList = CType(sessionHelper.GetSession("sessDelDataUploadFile"), ArrayList)
        arrDocDelete = IIf(Not IsNothing(arrDocDelete), arrDocDelete, New ArrayList)

        If Mode = "Edit" Then
            oBHR = New BabitReportHeaderFacade(User).Retrieve(CInt(Request.QueryString("BabitReportHeaderID")))
        End If

        oBHR.Dealer = objDealer
        Dim oBHID As Integer = CType(sessionHelper.GetSession(sessionBabitHeaderID), Integer)
        Dim oBH As BabitHeader = New BabitHeaderFacade(User).Retrieve(oBHID)
        oBHR.BabitHeader = oBH
        'oBHR.PeriodStart = CType(ddlMonth.SelectedValue & "/1/" & ddlYear.SelectedValue, Date)
        oBHR.PeriodStart = CDate(1 & "/" & ddlMonth.SelectedValue & "/" & ddlYear.SelectedValue).ToString("yyyy/MM/dd 00:00:00")
        oBHR.PeriodEnd = CDate(Date.DaysInMonth(ddlYear.SelectedValue, ddlMonth.SelectedValue) & "/" & ddlMonth.SelectedValue & "/" & ddlYear.SelectedValue).ToString("yyyy/MM/dd 00:00:00")

        If Mode = "Edit" Then
            _return = New BabitReportDocumentFacade(User).UpdateTransaction(oBHR, arrBDR, arrDocDelete)
        ElseIf Mode = "New" Then
            'oBHR.ID = New BabitReportHeaderFacade(User).Insert(oBHR)
            _return = New BabitReportDocumentFacade(User).InsertTransaction(oBHR, arrBDR)
        End If

        Dim strJs As String = String.Empty
        If _return > 0 Then
            Dim debug = _return
            CommitAttachment(arrBDR)
            If Mode = "Edit" Then
                RemoveBabitIklanReportAttachment(arrDocDelete, TargetDirectory)
            End If
            clearAll()
            ClearTempData()

            If Mode = "Edit" Then
                strJs = "alert('Update Data Berhasil');"
            ElseIf Mode = "New" Then
                strJs = "alert('Simpan Data Berhasil');"
            End If
            strJs += "window.location = '../Babit/FrmBabitReportEventList.aspx'"
        Else
            If Mode = "Edit" Then
                strJs = "alert('Update Data Gagal');"
            ElseIf Mode = "New" Then
                strJs = "alert('Simpan Data Gagal');"
            End If
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)

    End Sub

    Private Sub ClearTempData()
        'Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        'Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        'Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        'Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        'Dim success As Boolean = False

        'Try
        '    success = imp.Start()
        '    If success Then
        '        Dim dir As New DirectoryInfo(TempDirectory)
        '        dir.Delete(True)
        '    End If
        'Catch ex As Exception
        '    'Throw ex
        'End Try
    End Sub

    Private Sub RemoveBabitIklanReportAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As BabitReportDocument In AttachmentCollection
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

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/Babit/FrmBabitReportEventList.aspx")
    End Sub

    Private Sub btnNoRegChange_Click(sender As Object, e As EventArgs) Handles btnNoRegChange.Click
        hdnNoReg_ValueChanged(Nothing, Nothing)
    End Sub
End Class