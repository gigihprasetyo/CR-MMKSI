#Region "Custom Namespace Import"
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports KTB.DNet.Utility
Imports System.Web.UI.WebControls
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
#End Region

Public Class SalesmanCreateAnnouncement
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents icTglCreate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglCreate2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnRelease As System.Web.UI.WebControls.Button
    Protected WithEvents fileUploadAnnouncementFileName As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents FileUploadMaterialFileName As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtAnnouncementContent As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSalesmanTrainingType As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTrainingTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTrainingCode As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Dim objSalesmanMasterTraining As SalesmanMasterTraining = New SalesmanMasterTraining
    Dim objSalesmanMasterTrainingFacade As SalesmanMasterTrainingFacade = New SalesmanMasterTrainingFacade(User)
    Dim objSalesmanTrainingTypeFacade As New SalesmanTrainingTypeFacade(User)
#End Region

#Region "Custom Method"
    Private Function InsertTrainingMaster() As Boolean

        Dim nResult As Integer

        objSalesmanMasterTraining = objSalesmanMasterTrainingFacade.Retrieve(CType(ddlTrainingCode.SelectedValue, Integer))
        objSalesmanMasterTraining.AnnouncementContent = txtAnnouncementContent.Text

        If fileUploadAnnouncementFileName.Value <> "" OrElse fileUploadAnnouncementFileName.Value <> Nothing Then
            Dim finfo As New FileInfo(fileUploadAnnouncementFileName.PostedFile.FileName)
            objSalesmanMasterTraining.AnnouncementFileName = KTB.DNet.Lib.WebConfig.GetValue("SalesTrainingDownload") & "\Pengumuman Training\" & ddlTrainingCode.SelectedItem.Text & "\" & finfo.Name
            UploadAnnouncementFile()
        Else
            objSalesmanMasterTraining.AnnouncementFileName = ""
        End If

        If FileUploadMaterialFileName.Value <> "" OrElse FileUploadMaterialFileName.Value <> Nothing Then
            Dim finfo2 As New FileInfo(FileUploadMaterialFileName.PostedFile.FileName)
            objSalesmanMasterTraining.MaterialFileName = KTB.DNet.Lib.WebConfig.GetValue("SalesTrainingDownload") & "\Material Training\" & ddlTrainingCode.SelectedItem.Text & "\" & finfo2.Name
            UploadMaterialFile()
        Else
            objSalesmanMasterTraining.MaterialFileName = ""
        End If

        nResult = New SalesmanMasterTrainingFacade(User).Update(objSalesmanMasterTraining)
        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
            Return False
        Else
            MessageBox.Show(SR.SaveSuccess)
            Return True
        End If
    End Function
    Private Sub ViewData()
        Dim arrList As ArrayList
        arrList = objSalesmanMasterTrainingFacade.Retrieve(ddlTrainingCode.SelectedItem.Text)

        If arrList.Count > 0 Then
            objSalesmanMasterTraining = arrList(0)
            txtTrainingTitle.Text = IIf((objSalesmanMasterTraining.TrainingTitle <> ""), objSalesmanMasterTraining.TrainingTitle, "")
            icTglCreate.Value = objSalesmanMasterTraining.StartingDate.Date
            icTglCreate2.Value = objSalesmanMasterTraining.EndDate.Date
            txtSalesmanTrainingType.Text = objSalesmanMasterTraining.SalesmanTrainingType.TrainingType
            txtAnnouncementContent.Text = objSalesmanMasterTraining.AnnouncementContent

            txtTrainingTitle.Enabled = False
            icTglCreate.Enabled = False
            icTglCreate2.Enabled = False
            txtSalesmanTrainingType.Enabled = False
            If (objSalesmanMasterTraining.IsReleased = 0) Then
                If (objSalesmanMasterTraining.AnnouncementContent <> "") Then
                    btnRelease.Enabled = True
                Else
                    btnRelease.Enabled = False
                End If
            Else
                btnRelease.Enabled = False
            End If
        End If
    End Sub
    Private Sub UploadAnnouncementFile()
        If fileUploadAnnouncementFileName.Value <> "" OrElse fileUploadAnnouncementFileName.Value <> Nothing Then
            'cek maxFileSize first
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

            If fileUploadAnnouncementFileName.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            End If

            Dim SrcFile As String = Path.GetFileName(fileUploadAnnouncementFileName.PostedFile.FileName)  '-- Source file name
            Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("SalesTrainingDownload") & "\Pengumuman Training\" & ddlTrainingCode.SelectedItem.Text & "\" & SrcFile   '-- Destination file
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
                    fileUploadAnnouncementFileName.PostedFile.SaveAs(DestFile)
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub
    Private Sub UploadMaterialFile()
        If FileUploadMaterialFileName.Value <> "" OrElse FileUploadMaterialFileName.Value <> Nothing Then
            'cek maxFileSize first
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

            If FileUploadMaterialFileName.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            End If

            Dim SrcFile As String = Path.GetFileName(FileUploadMaterialFileName.PostedFile.FileName)  '-- Source file name
            Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("SalesTrainingDownload") & "\Material Training\" & ddlTrainingCode.SelectedItem.Text & "\" & SrcFile   '-- Destination file
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
                    FileUploadMaterialFileName.PostedFile.SaveAs(DestFile)
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            CommonFunction.BindSalesmanTrainingCode(ddlTrainingCode, Me.User, False)
            ddlTrainingCode.SelectedIndex = 0
            If ddlTrainingCode.SelectedIndex >= 0 Then
                ViewData()
            End If
        End If
        If CheckCreatePrivilege() Then
            btnSimpan.Visible = True
            btnRelease.Visible = True
            fileUploadAnnouncementFileName.Visible = True
            FileUploadMaterialFileName.Visible = True
        Else
            btnSimpan.Visible = False
            btnRelease.Visible = False
            fileUploadAnnouncementFileName.Visible = False
            FileUploadMaterialFileName.Visible = False
        End If
    End Sub

    Private Function ValidateEntry() As Boolean
        If txtAnnouncementContent.Text.Trim = "" Then
            Return False
        End If
        'Modified by AAR-07/08/2008 Request by Rina Anggraini
        'If fileUploadAnnouncementFileName.Value = "" Then
        '   Return False
        'End If
        'If FileUploadMaterialFileName.Value = "" Then
        '   Return False
        'End If
        If fileUploadAnnouncementFileName.Value = "" And FileUploadMaterialFileName.Value = "" Then
            Return False
        End If
        'End of Modified by AAR-07/08/2008 Request by Rina Anggraini

        Return True
    End Function

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If ValidateEntry() Then
            InsertTrainingMaster()
            ViewData()
        Else
            MessageBox.Show("Data tidak lengkap.")
        End If
    End Sub
    Private Sub ddlTrainingCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTrainingCode.SelectedIndexChanged
        ViewData()
    End Sub
    Private Sub btnRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        Dim nResult As Integer
        objSalesmanMasterTraining = objSalesmanMasterTrainingFacade.Retrieve(CType(ddlTrainingCode.SelectedValue, Integer))
        objSalesmanMasterTraining.IsReleased = 1

        nResult = New SalesmanMasterTrainingFacade(User).Update(objSalesmanMasterTraining)

        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            ViewData()
            MessageBox.Show("Rilis pengumuman berhasil")
        End If
    End Sub

#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.AnnouncementCreateView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pelatihan Tenaga Penjual - Buat Pengumuman")
        End If
    End Sub

    Private Function CheckCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.AnnouncementCreateDataCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

End Class
