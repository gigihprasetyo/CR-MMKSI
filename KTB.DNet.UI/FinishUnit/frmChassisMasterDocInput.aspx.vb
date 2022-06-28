#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports System.IO

#End Region

Public Class frmChassisMasterDocInput
    Inherits System.Web.UI.Page



#Region "variable"
    Private ReadOnly varUpload As String = "\OCRChasiss\" '\\172.17.31.121\MDNET_Repo\Repository\BSI-Net\DNet\SAP\OCRChasiss
    Private ReadOnly varSession As String = "sessfrmChassisMasterDocInput"
    Private ReadOnly varCrite As String = "CritfrmChassisMasterDocInput"
    Private sesHelper As New SessionHelper
    Dim arlUserGroup As New ArrayList
    Dim IsKTB As Boolean
    Dim IsAllowToEdit As Boolean
    Dim IsAllowToRead As Boolean
#End Region

#Region "Private Function"

    Private Sub loadDoc(ByVal ChassisID As Integer)
        Try
            Dim cm As Vw_ChassisMaster = New Vw_ChassisMasterFacade(User).Retrieve(ChassisID)

            lblDealerCode.Text = cm.Dealer.DealerCode
            lblDealerName.Text = cm.Dealer.DealerName

            lblChassisNumber.Text = cm.ChassisNumber
            lblEngineNumber.Text = cm.EngineNumber

            lblSPKNumber.Text = cm.EndCustomer.SPKFaktur.SPKHeader.SPKNumber
            lblEndCustomerName.Text = cm.EndCustomer.Name1

            If cm.ChassisMasterDocumentID = 0 Then
                ViewState("Mode") = "New"

            Else
                ViewState("Mode") = "Edit"
                lblAttachmentChassis.Text = cm.ChassisImagePath
                photoViewChassis.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=0&file=" & cm.ChassisImagePath & "&type=" & "Chassis"

                lblAttachmentEngine.Text = cm.EngineImagePath
                photoViewEngine.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=0&file=" & cm.EngineImagePath & "&type=" & "Chassis"
            End If

            Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)

            If objDealer.Title <> EnumDealerTittle.DealerTittle.DEALER Then
                btnSave.Visible = False
                htmButtonSave.Visible = False
            End If
        Catch ex As Exception
            InvalidData()
        End Try


    End Sub

    Private Sub InvalidData()

        btnSave.Visible = False
        htmButtonSave.Visible = False
        MessageBox.Show("Invalid Data")
    End Sub

    Private Function UploadFile() As Boolean

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim UniqueName As String = DateTime.Now.ToString("yyyyMMddHHmmss") & Guid.NewGuid().ToString().Substring(0, 3)

        Try
            If lblAttachmentChassis.Text = "" AndAlso (IsNothing(fileUploadChassis) OrElse (Me.fileUploadChassis.PostedFile.ContentLength = 0)) Then
                hdnProgress.Value = "0"
                MessageBox.Show("Silahkan Lampirkan Gambar No Rangka")
                Return False
            End If


            If lblAttachmentEngine.Text = "" AndAlso (IsNothing(fileUploadEngine) OrElse (Me.fileUploadEngine.PostedFile.ContentLength = 0)) Then
                hdnProgress.Value = "0"
                MessageBox.Show("Silahkan Lampirkan Gambar No MEsin")
                Return False
            End If

            Dim UploadChassis As Boolean = False
            Dim UploadEngine As Boolean = False
            ''Upload CHassis
            If (Not Me.fileUploadChassis.PostedFile Is Nothing) And (Me.fileUploadChassis.PostedFile.ContentLength > 0) Then

                If Me.fileUploadChassis.PostedFile.ContentLength > 216000 Then
                    hdnProgress.Value = "0"
                    MessageBox.Show(" (Ukuran maksimum 200 Kb)")
                    Return False
                End If

                Dim ext As String = System.IO.Path.GetExtension(Me.fileUploadChassis.PostedFile.FileName)
                If Not (ext.ToUpper() = ".JPG" OrElse ext.ToUpper() = ".JPEG" OrElse ext.ToUpper() = ".PNG" OrElse ext.ToUpper() = ".GIF" OrElse ext.ToUpper() = ".BMP") Then
                    hdnProgress.Value = "0"
                    MessageBox.Show("Hanya menerima file format Gambar")
                    Return False
                End If

                UploadChassis = True
            End If

            If (Not Me.fileUploadEngine.PostedFile Is Nothing) And (Me.fileUploadEngine.PostedFile.ContentLength > 0) Then

                If Me.fileUploadEngine.PostedFile.ContentLength > 216000 Then
                    hdnProgress.Value = "0"
                    MessageBox.Show(" (Ukuran maksimum 200 Kb)")
                    Return False
                End If

                Dim ext As String = System.IO.Path.GetExtension(Me.fileUploadEngine.PostedFile.FileName)
                If Not (ext.ToUpper() = ".JPG" OrElse ext.ToUpper() = ".JPEG" OrElse ext.ToUpper() = ".PNG" OrElse ext.ToUpper() = ".GIF" OrElse ext.ToUpper() = ".BMP") Then
                    hdnProgress.Value = "0"
                    MessageBox.Show("Hanya menerima file format Gambar")
                    Return False
                End If

                UploadEngine = True
            End If
            Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
            If UploadChassis OrElse UploadEngine Then

                If imp.Start() Then
                    If UploadChassis Then
                        Dim ext As String = System.IO.Path.GetExtension(Me.fileUploadChassis.PostedFile.FileName)


                        UniqueName = objDealer.DealerCode & "_" & DateTime.Now.ToString("yyyyMMddHHmmss_C") & Guid.NewGuid().ToString().Substring(0, 3)
                        Dim ChassisFileLocation As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload & DateTime.Now.Year.ToString() & "\" & UniqueName
                        Dim strCHassiFileName As String = Path.GetFileName(Me.fileUploadChassis.PostedFile.FileName)
                        If Not IO.Directory.Exists(ChassisFileLocation) Then
                            IO.Directory.CreateDirectory(Path.GetDirectoryName(ChassisFileLocation))
                        End If
                        ChassisFileLocation = ChassisFileLocation & ext

                        If IO.File.Exists(ChassisFileLocation) Then
                            IO.File.Delete(Path.GetDirectoryName(ChassisFileLocation))
                        End If

                        Dim objUpload As New UploadToWebServer
                        objUpload.Upload(Me.fileUploadChassis.PostedFile.InputStream, ChassisFileLocation)

                        lblAttachmentChassis.Text = DateTime.Now.Year.ToString() & "\" & UniqueName & ext
                        photoViewChassis.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=0&file=" & lblAttachmentChassis.Text & "&type=" & "Chassis"

                    End If

                    If UploadEngine Then
                        Dim ext As String = System.IO.Path.GetExtension(Me.fileUploadEngine.PostedFile.FileName)
                        UniqueName = DateTime.Now.ToString("yyyyMMddHHmmss_E") & Guid.NewGuid().ToString().Substring(0, 3)
                        Dim ChassisFileLocation As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload & DateTime.Now.Year.ToString() & "\" & UniqueName
                        Dim strCHassiFileName As String = Path.GetFileName(Me.fileUploadChassis.PostedFile.FileName)
                        If Not IO.Directory.Exists(ChassisFileLocation) Then
                            IO.Directory.CreateDirectory(Path.GetDirectoryName(ChassisFileLocation))
                        End If
                        ChassisFileLocation = ChassisFileLocation & ext

                        If IO.File.Exists(ChassisFileLocation) Then
                            IO.File.Delete(Path.GetDirectoryName(ChassisFileLocation))
                        End If

                        Dim objUpload As New UploadToWebServer
                        objUpload.Upload(Me.fileUploadEngine.PostedFile.InputStream, ChassisFileLocation)

                        lblAttachmentEngine.Text = DateTime.Now.Year.ToString() & "\" & UniqueName & ext
                        photoViewEngine.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=0&file=" & lblAttachmentEngine.Text & "&type=" & "Chassis"

                    End If


                    hdnProgress.Value = "0"


                Else
                    hdnProgress.Value = "0"
                    MessageBox.Show("File Gagal Upload")
                    Return False
                End If
                imp.StopImpersonate()
                imp = Nothing

            Else
                hdnProgress.Value = "0"
                MessageBox.Show("Tidak Ada File yang dipilih")
                Return False
            End If
        Catch ex As Exception
            hdnProgress.Value = "0"
            MessageBox.Show(SR.UploadFail(ex.Message.ToString()))
            Return False
        End Try

        Return True
    End Function


    Private Sub CheckPrivilege()
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        Dim lihat
        IsAllowToRead = SecurityProvider.Authorize(Context.User, SR.UploadGesek_Lihat_Privilege)
        IsAllowToEdit = SecurityProvider.Authorize(Context.User, SR.UploadGesek_Input_Privilege)

        If Not IsAllowToEdit AndAlso Not IsAllowToRead Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sales-FakturKendaraan-UploadRangka")
        End If

        If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            IsKTB = True
            btnSave.Visible = False
        Else
            btnSave.Visible = IsAllowToEdit
            
        End If
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckPrivilege()
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("ID")) Then
                ViewState("CHassisID") = CInt(Request.QueryString("ID"))
                Me.loadDoc(CInt(Request.QueryString("ID")))
            Else
                InvalidData()
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If UploadFile() Then
                Dim ChassisID As Integer = ViewState("CHassisID")
                Dim cm As Vw_ChassisMaster = New Vw_ChassisMasterFacade(User).Retrieve(ChassisID)
                Dim u As New ChassisMasterDocumentFacade(User)
                Dim i As Integer = 0
                If cm.ChassisMasterDocumentID = 0 Then 'Insert Data
                    Dim ch As New ChassisMasterDocument

                    ch.ChassisMasterID = ChassisID
                    ch.ChassisImagePath = lblAttachmentChassis.Text
                    ch.EngineImagePath = lblAttachmentEngine.Text
                    ch.Status = 0
                    ch.UploadDate = DateTime.Now.Date

                    i = u.Insert(ch)
                Else  'Update Data
                    Dim ch As New ChassisMasterDocument
                    ch = cm.ChassisMasterDocument
                    ch.ChassisImagePath = lblAttachmentChassis.Text
                    ch.EngineImagePath = lblAttachmentEngine.Text
                    i = u.Update(ch)
                End If
                If i > 0 Then
                    MessageBox.Show(SR.SaveSuccess)
                Else
                    MessageBox.Show(SR.SaveFail)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try

        hdnProgress.Value = "0"
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("frmChassisMasterDoc.aspx?Mode=1&isSelf=0", False)
    End Sub
End Class