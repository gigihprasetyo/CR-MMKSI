Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports KTB.DNet.Security

Public Class FrmManualDoc
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtmanualName As System.Web.UI.WebControls.TextBox
    Protected WithEvents fileUploadManual As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtSequence As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents taDesc As System.Web.UI.HtmlControls.HtmlTextArea

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Internal Enum"
    Public Enum TipeUser
        All = 0
        KTB = 1
        Dealer = 2
    End Enum

    Private Enum TipeInsert
        Baru = 1
        Ubah = 2
    End Enum

    Public Function RetrieveTipeUser() As ArrayList
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        
        Dim al As New ArrayList
        Dim dt As EnumTipeUser
        dt = New EnumTipeUser(0, "Semua")
        al.Add(dt)
        If companyCode = "MFTBC" Then
            dt = New EnumTipeUser(1, "KTB")
        Else
            dt = New EnumTipeUser(1, "MKS")
        End If
        al.Add(dt)
        dt = New EnumTipeUser(2, "Dealer")
        al.Add(dt)

        Return al
    End Function

    Public Class EnumTipeUser
        Private _val As Integer
        Private _nama As String

        Public Sub New(ByVal val As Integer, ByVal nama As String)
            _val = val
            _nama = nama
        End Sub

        Public Property ValStatus() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Public Property NamaUser() As String
            Get
                Return _nama
            End Get
            Set(ByVal Value As String)
                _nama = Value
            End Set
        End Property
    End Class
#End Region

#Region "Deklarasi"
    Dim sHelper As New SessionHelper
#End Region

#Region "Custom Method"
    Private Sub SaveData()
        If sHelper.GetSession("ViewModeMD") = "Edit" Then
            Dim objDomainMD As ManualDoc = sHelper.GetSession("DomainMD")
            If txtmanualName.Text = String.Empty Then
                MessageBox.Show("Nama Manual harus diisi!")
            Else
                Dim seqTemp As Integer = objDomainMD.Sequence
                'objDomainMD.Description = txtDescription.Text.Trim
                objDomainMD.Name = txtmanualName.Text.Trim 'added by anh, req by angga on 20140226
                objDomainMD.Description = taDesc.Value
                If txtSequence.Text <> "" Then
                    objDomainMD.Sequence = txtSequence.Text
                    objDomainMD.Type = ddlType.SelectedValue

                    If Not seqTemp = objDomainMD.Sequence Then
                        If Not CekSeqNo(objDomainMD) = False Then
                            MessageBox.Show("No. Sequence sudah pernah digunakan!")
                        Else
                            Dim facadeUpdate As New ManualDocFacade(User)
                            If Not fileUploadManual.Value = "" Then
                                Dim fileExt As String = Path.GetExtension(fileUploadManual.PostedFile.FileName)
                                If fileext = ".pdf" Then
                                    'update file
                                    UploadNewFile(fileUploadManual.Value, objDomainMD)

                                    Dim retVal As Integer = facadeUpdate.Update(objDomainMD)
                                    If retVal <> -1 Then
                                        MessageBox.Show("Update berhasil dilakukan")
                                        ClearControl()
                                    Else
                                        MessageBox.Show("Update tidak berhasil dilakukan")
                                    End If
                                Else
                                    MessageBox.Show("Hanya menerima file format PDF")
                                End If
                            Else
                                Dim retVal As Integer = facadeUpdate.Update(objDomainMD)
                                If retVal <> -1 Then
                                    MessageBox.Show("Update berhasil dilakukan")
                                    ClearControl()
                                Else
                                    MessageBox.Show("Update tidak berhasil dilakukan")
                                End If
                            End If
                        End If
                    Else
                        Dim facadeUpdate As New ManualDocFacade(User)
                        If Not fileUploadManual.Value = "" Then
                            Dim fileExt As String = Path.GetExtension(fileUploadManual.PostedFile.FileName)
                            If fileext = ".pdf" Then
                                'update file
                                UploadNewFile(fileUploadManual.Value, objDomainMD)

                                Dim retVal As Integer = facadeUpdate.Update(objDomainMD)
                                If retVal <> -1 Then
                                    MessageBox.Show("Update berhasil dilakukan")
                                    ClearControl()
                                Else
                                    MessageBox.Show("Update tidak berhasil dilakukan")
                                End If
                            Else
                                MessageBox.Show("Hanya menerima file format PDF")
                            End If
                        Else
                            Dim retVal As Integer = facadeUpdate.Update(objDomainMD)
                            If retVal <> -1 Then
                                MessageBox.Show("Update berhasil dilakukan")
                                ClearControl()
                            Else
                                MessageBox.Show("Update tidak berhasil dilakukan")
                            End If
                        End If
                        End If
                Else
                    MessageBox.Show("Nomor Sequence Harus diisi")
                End If
            End If
        Else
            'insert new Manual DOc
            If txtmanualName.Text = String.Empty Then
                MessageBox.Show("Nama Manual harus diisi!")
            Else
                If fileUploadManual.Value = "" Then
                    MessageBox.Show("Silahkan masukkan file yang akan diupload")
                Else
                    Dim fileExt As String = Path.GetExtension(fileUploadManual.PostedFile.FileName)
                    If fileExt = ".pdf" Then
                        Dim objDomain As New ManualDoc
                        objDomain.Name = txtmanualName.Text.Trim
                        'objDomain.Description = txtDescription.Text.Trim
                        objDomain.Description = taDesc.Value
                        If txtSequence.Text <> "" Then
                            objDomain.Sequence = txtSequence.Text
                            objDomain.Type = ddlType.SelectedValue

                            If Not CekManualExist(objDomain) = False Then
                                MessageBox.Show("Nama Manual sudah pernah digunakan sebelumnya!")
                            Else
                                If Not CekSeqNo(objDomain) = False Then
                                    MessageBox.Show("No. Sequence sudah pernah digunakan!")
                                Else
                                    Dim facadeInsert As New ManualDocFacade(User)
                                    Dim retVal As Integer = facadeInsert.Insert(objDomain)
                                    If retVal <> -1 Then
                                        UpdateDomain(retVal)
                                        ClearControl()
                                    Else
                                        MessageBox.Show("Insert tidak berhasil dilakukan")
                                    End If
                                End If
                            End If
                        Else
                            MessageBox.Show("Nomor Sequence Harus diisi")
                        End If
                    Else
                        MessageBox.Show("Hanya menerima format file PDF")
                    End If
                End If
            End If
        End If
    End Sub

    'khusus upload update
    Private Sub UploadNewFile(ByVal fileUpload As String, ByVal objdomain As ManualDoc)
        If fileUpload <> "" OrElse fileUpload <> String.Empty Then
            Dim SrcFile As String = Path.GetFileName(fileUploadManual.PostedFile.FileName)  '-- Source file name
            Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ManualDoc") & "\" & objdomain.ID & "\" & SrcFile     '-- Destination file
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
                    fileUploadManual.PostedFile.SaveAs(DestFile)
                    objdomain.FileName = KTB.DNet.Lib.WebConfig.GetValue("ManualDoc") & "\" & objdomain.ID & "\" & SrcFile
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

    Private Sub UpdateDomain(ByVal retVal As Integer)
        Dim objDomain As ManualDoc = New ManualDocFacade(User).Retrieve(retVal)
        If fileUploadManual.Value <> "" Then
            'upload new image
            Dim SrcFile As String = Path.GetFileName(fileUploadManual.PostedFile.FileName)  '-- Source file name
            Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ManualDoc") & "\" & objDomain.ID & "\" & SrcFile     '-- Destination file
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
                    fileUploadManual.PostedFile.SaveAs(DestFile)
                    objDomain.FileName = KTB.DNet.Lib.WebConfig.GetValue("ManualDoc") & "\" & objDomain.ID & "\" & SrcFile
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try

            If (New ManualDocFacade(User).Update(objDomain) <> -1) Then
                MessageBox.Show("Data berhasil disimpan")
            End If
        Else
            MessageBox.Show("Silahkan pilih file yang akan diupload")
        End If
    End Sub

    Private Function CekManualExist(ByVal objDomain As ManualDoc) As Boolean
        Dim facade As New ManualDocFacade(User)
        If facade.ValidateManualName(objDomain.Name) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function CekSeqNo(ByVal objDomain As ManualDoc) As Boolean
        Dim facade2 As New ManualDocFacade(User)
        If facade2.ValidateSeqNo(objDomain.Sequence) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ClearControl()
        txtmanualName.Text = ""
        txtSequence.Text = ""
        ddlType.SelectedIndex = 0
        taDesc.Value = ""
    End Sub

    Private Sub MapToControl(ByVal objDomain As ManualDoc)
        txtmanualName.Text = objDomain.Name
        'txtDescription.Text = objDomain.Description
        taDesc.Value = objDomain.Description
        txtSequence.Text = objDomain.Sequence
        ddlType.SelectedValue = objDomain.Type
    End Sub
#End Region

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ManualDocMaintain_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=BULETIN - Manual Doc")
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            InitiateAuthorization()
            ddlType.DataSource = RetrieveTipeUser()
            ddlType.DataTextField = "NamaUser"
            ddlType.DataValueField = "ValStatus"
            ddlType.DataBind()

            If sHelper.GetSession("ViewModeMD") = "Edit" Then
                Dim id As Integer = CInt(sHelper.GetSession("EditIDMD"))
                Dim objDomain As ManualDoc = New ManualDocFacade(User).Retrieve(id)
                MapToControl(objDomain)
                sHelper.SetSession("DomainMD", objDomain)
                txtmanualName.Enabled = True 'False 'edited by anh,req by angga on 20140226
                btnBack.Visible = True
            Else
                txtmanualName.Enabled = True
                btnBack.Visible = False
            End If
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        SaveData()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        sHelper.RemoveSession("ViewModeMD")
        Response.Redirect("FrmManualDocList.aspx")
    End Sub
End Class
