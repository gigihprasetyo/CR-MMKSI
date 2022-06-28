Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports KTB.DNet.Security
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Collections.Generic
Imports System.Linq
Imports SpireDoc = Spire.Doc
Imports SpireDocument = Spire.Doc.Document
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports B = DocumentFormat.OpenXml.Wordprocessing

Public Class FrmPKTCertificateMaster
    Inherits System.Web.UI.Page

    Private sessHelper As SessionHelper = New SessionHelper
    Dim objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private m_bInputPrivilege As Boolean = False
    Dim crit As CriteriaComposite
    Dim targetDir As String = String.Empty

    Dim pKTTemplateFacade As PKTTemplateFacade = New PKTTemplateFacade(User)
    Dim vehicleModelFacade As VechileModelFacade = New VechileModelFacade(User)
    Dim categoryFacade As CategoryFacade = New CategoryFacade(User)
    Dim validExt As String = ".docx"
    Private maxFileSize As Integer = 2048000

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        targetDir = KTB.DNet.Lib.WebConfig.GetValue("SAN")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            ResetControl()
            RefreshGrid()
        End If
    End Sub

    Protected Sub dgPKTCertMaster_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPKTCertMaster.ItemCommand
        Dim hdFilename As HiddenField = CType(e.Item.FindControl("hdFilename"), HiddenField)
        Select Case e.CommandName
            Case "download"
                Response.Redirect("../download.aspx?file=" & hdFilename.Value)
        End Select
    End Sub

    Protected Sub dgPKTCertMaster_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgPKTCertMaster.PageIndexChanged
        RefreshGrid(e.NewPageIndex)
    End Sub

    Protected Sub dgPKTCertMaster_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgPKTCertMaster.SortCommand
        If e.SortExpression = ViewState.Item("SortCol") Then
            If ViewState.Item("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState.Item("SortCol") = e.SortExpression
        dgPKTCertMaster.SelectedIndex = -1
        dgPKTCertMaster.CurrentPageIndex = 0
        RefreshGrid(0)
    End Sub

    Protected Sub dgPKTCertMaster_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPKTCertMaster.ItemDataBound
        Dim RowValue As PKTTemplate = CType(e.Item.DataItem, PKTTemplate)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgPKTCertMaster.PageSize * dgPKTCertMaster.CurrentPageIndex)
        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Dim ext As String = String.Empty
        Dim msgError As String = String.Empty
        Dim obj As PKTTemplate
        If IsNothing(CType(sessHelper.GetSession("PKTTemplate"), PKTTemplate)) Then
            obj = New PKTTemplate
        Else
            obj = CType(sessHelper.GetSession("PKTTemplate"), PKTTemplate)
        End If

        If fuUpload.PostedFile.ContentLength <> 0 Then
            ext = Path.GetExtension(fuUpload.PostedFile.FileName)
            If Not validExt.Equals(ext) Then
                msgError = String.Format("Upload File Gagal. Gunakan format file {0}", validExt)
            ElseIf fuUpload.PostedFile.ContentLength > maxFileSize Then
                msgError = "Upload File Gagal. Maksimum per file 2 MB"
            Else
                obj.FileName = UploadTemp(fuUpload.PostedFile.FileName, fuUpload.PostedFile.InputStream)
                obj.AttachmentData = fuUpload.PostedFile

                If (Not CheckFormatTemplate(obj.FileName)) Then
                    msgError = "Fomat template tidak sesuai. Mohon menggunakan file contoh template yang telah disediakan."
                Else
                    sessHelper.SetSession("PKTTemplate", obj)
                    MessageBox.Show("Upload berhasil.")
                    Return
                End If
            End If
        Else
            msgError = "Tidak ada data yang di upload."
        End If

        If Not String.IsNullorEmpty(msgError) Then
            sessHelper.SetSession("PKTTemplate", Nothing)
            MessageBox.Show(msgError)
        End If
    End Sub

    Protected Sub btnPreview_Click(sender As Object, e As EventArgs)
        If IsNothing(sessHelper.GetSession("PKTTemplate")) Then
            MessageBox.Show("Tidak ada data yang di upload.")
        Else
            Dim pKTTemplate As PKTTemplate = CType(sessHelper.GetSession("PKTTemplate"), PKTTemplate)
            DownloadPdfFile(pKTTemplate.FileName)
        End If
    End Sub

    Protected Sub ddlKategori_SelectedIndexChanged(sender As Object, e As EventArgs)
        CommonFunction.BindVehicleModelIndDescriptionToDDL(ddlModel, ddlKategori.SelectedItem.Text)
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs)
        Dim message As String = String.Empty
        If Not Validate(message) Then
            MessageBox.Show(message)
            Exit Sub
        End If

        Dim pKTTemplate As PKTTemplate = CType(sessHelper.GetSession("PKTTemplate"), PKTTemplate)
        Dim isExist As Boolean = IsDataExist(pKTTemplate)

        PopulateData(pKTTemplate)

        If Not UploadFile(pKTTemplate) Then
            Exit Sub
        End If

        Dim result As Integer = 0
        If isExist Then
            result = pKTTemplateFacade.Update(pKTTemplate)
        Else
            Dim objDomains As ArrayList = New ArrayList
            If ccTglBerlaku.Value.Date = DateTime.Now.Date Then
                Dim oldData As PKTTemplate = GetOldData()
                If Not IsNothing(oldData) Then
                    objDomains.Add(oldData)
                End If
            End If

            objDomains.Add(pKTTemplate)
            result = pKTTemplateFacade.Insert(objDomains)
        End If

        If result > 0 Then
            MessageBox.Show("Simpan berhasil.")
            ResetControl()
            RefreshGrid()
        Else
            MessageBox.Show("Simpan gagal.")
        End If
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs)
        ResetControl()
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs)
        RefreshGrid()
    End Sub

    Protected Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs)
        Dim fileDir As String = "DataFile\PKTTemplate\PKT_Template.docx"
        Response.Redirect(String.Format("../downloadlocal.aspx?file={0}", fileDir))
    End Sub
#End Region

#Region "Custom Method"
    Private Sub CheckPrivilege()
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.PKTMasterTemplate_Input_Privilage)

        If Not m_bInputPrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Umum - Template Sertifikat PKT")
        End If
    End Sub

    Private Sub RefreshGrid(Optional indexPage As Integer = 0)
        Dim totalRow As Integer

        crit = New CriteriaComposite(New Criteria(GetType(PKTTemplate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ccTglBerlaku.Value.ToString <> "01/01/0001 0:00:00" Then
            Dim tglBerlaku As DateTime = New DateTime(ccTglBerlaku.Value.Year, ccTglBerlaku.Value.Month, ccTglBerlaku.Value.Day, 0, 0, 0)
            crit.opAnd(New Criteria(GetType(PKTTemplate), "ValidFrom", MatchType.GreaterOrEqual, tglBerlaku))
        End If

        If ddlKategori.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(PKTTemplate), "VechileModel.Category.ID", MatchType.Exact, CType(ddlKategori.SelectedValue, Short)))
        End If

        If ddlModel.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(PKTTemplate), "VechileModel.ID", MatchType.Exact, CType(ddlModel.SelectedValue, Short)))
        End If

        If ddlStatus.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(PKTTemplate), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Short)))
        End If

        Dim data As ArrayList = pKTTemplateFacade.RetrieveByCriteria(crit, indexPage + 1, dgPKTCertMaster.PageSize, totalRow, ViewState.Item("SortCol"), ViewState.Item("SortDirection"))

        If data.Count = 0 Then
            dgPKTCertMaster.CurrentPageIndex = 0
        Else
            dgPKTCertMaster.CurrentPageIndex = indexPage
        End If

        dgPKTCertMaster.DataSource = data
        dgPKTCertMaster.VirtualItemCount = totalRow
        dgPKTCertMaster.DataBind()
    End Sub

    Private Sub ResetControl()
        ccTglBerlaku.Value = Nothing
        InitiateDropdownList()
        ddlKategori_SelectedIndexChanged(Nothing, Nothing)
        ddlStatus.SelectedIndex = 0
        sessHelper.SetSession("PKTTemplate", Nothing)
    End Sub

    Private Sub InitiateDropdownList()
        ddlKategori.Items.Clear()
        crit = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.No, "CV"))
        Dim oData As ArrayList = categoryFacade.Retrieve(crit)
        With ddlKategori.Items
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each obj As Category In oData
                .Add(New ListItem(obj.CategoryCode.ToUpper, obj.ID))
            Next
        End With

        ddlModel.Items.Clear()
        ddlModel.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Function Validate(ByRef message As String) As Boolean
        If ccTglBerlaku.Value.ToString = "01/01/0001 0:00:00" Then
            message = "Tanggal Berlaku harus diisi."
        ElseIf ccTglBerlaku.Value < Date.Now.Date Then
            message = "Tanggal Berlaku minimal hari ini."
        ElseIf ddlModel.SelectedIndex = 0 Then
            message = "Model harus dipilih."
        ElseIf IsNothing(CType(sessHelper.GetSession("PKTTemplate"), PKTTemplate)) Then
            message = "Belum ada file yang diupload."
        End If

        Return String.IsNullorEmpty(message)
    End Function

    Private Function IsDataExist(ByRef PKTTemplate As PKTTemplate) As Boolean
        Dim isExist As Boolean = False
        Dim arrExist As ArrayList = New ArrayList
        crit = New CriteriaComposite(New Criteria(GetType(PKTTemplate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(PKTTemplate), "VechileModel.ID", MatchType.Exact, CShort(ddlModel.SelectedValue)))
        crit.opAnd(New Criteria(GetType(PKTTemplate), "ValidFrom", MatchType.Exact, ccTglBerlaku.Value))
        crit.opAnd(New Criteria(GetType(PKTTemplate), "Status", MatchType.Exact, 1))
        arrExist = PKTTemplateFacade.Retrieve(crit)
        If arrExist.Count > 0 Then
            Dim obj As PKTTemplate = CType(arrExist(0), PKTTemplate)
            PKTTemplate.ID = obj.ID
            PKTTemplate.CreatedBy = obj.CreatedBy
            isExist = True
        End If

        Return isExist
    End Function

    Private Function GetOldData() As PKTTemplate
        Dim PKTTemplate As PKTTemplate
        Dim arrExist As ArrayList = New ArrayList
        crit = New CriteriaComposite(New Criteria(GetType(PKTTemplate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(PKTTemplate), "VechileModel.ID", MatchType.Exact, CShort(ddlModel.SelectedValue)))
        crit.opAnd(New Criteria(GetType(PKTTemplate), "ValidFrom", MatchType.Lesser, DateTime.Now.Date))
        crit.opAnd(New Criteria(GetType(PKTTemplate), "Status", MatchType.Exact, 1))
        arrExist = PKTTemplateFacade.Retrieve(crit)
        If arrExist.Count > 0 Then
            PKTTemplate = CType(arrExist(0), PKTTemplate)
            PKTTemplate.Status = 0
        Else
            PKTTemplate = Nothing
        End If

        Return PKTTemplate
    End Function

    Private Sub PopulateData(ByVal PKTTemplate As PKTTemplate)
        PKTTemplate.VechileModel = vehicleModelFacade.Retrieve(CShort(ddlModel.SelectedValue))
        PKTTemplate.ValidFrom = ccTglBerlaku.Value
        PKTTemplate.Status = IIf(CDate(ccTglBerlaku.Value) > DateTime.Now.Date, 0, 1)
    End Sub

    Private Function UploadFile(ByVal obj As PKTTemplate) As Boolean
        Dim isValid As Boolean = True

        If Not obj.AttachmentData Is Nothing Then
            Dim ext As String = Path.GetExtension(obj.AttachmentData.FileName)
            obj.FileName = String.Format("PKT-Template\{0}\{1}\{2}_{3}{4}", obj.ValidFrom.ToString("yyyy"), obj.ValidFrom.ToString("MM"), obj.VechileModel.VechileModelCode, obj.ValidFrom.ToString("ddMMyyyy"), ext)
            isValid = CommitAttachment(obj)
        Else
            MessageBox.Show("Tidak ada data yang diupload. Mohon untuk melakukan upload terlebih dahulu.")
            isValid = False
        End If

        Return isValid
    End Function

    Private Function CommitAttachment(ByVal obj As PKTTemplate) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim TargetFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(obj.AttachmentData) Then
                    Dim fullpath As String = Path.Combine(targetDir, obj.FileName)
                    TargetFInfo = New FileInfo(fullpath)

                    If Not TargetFInfo.Directory.Exists Then
                        Directory.CreateDirectory(TargetFInfo.DirectoryName)
                    End If

                    obj.AttachmentData.SaveAs(fullpath)
                    obj.AttachmentData = Nothing
                    Return True
                End If
            End If
            imp.StopImpersonate()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return False
    End Function

    Private Function UploadTemp(ByVal dir As String, ByVal srcFile As Stream) As String
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate
        Try
            Dim ext As String = Path.GetExtension(dir)
            Dim dirTarget As String = Guid.NewGuid.ToString() & ext
            Dim targetFile As String = Server.MapPath("~/DataTemp/" & dirTarget)

            imp = New SAPImpersonate(_user, _password, _webServer)

            If imp.Start() Then
                Dim objUpload As New UploadToWebServer
                objUpload.Upload(srcFile, targetFile)
            End If

            imp.StopImpersonate()

            Return targetFile
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function

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
                pdfDoc.SaveToFile("Preview.pdf", Spire.Doc.FileFormat.PDF, Response, Spire.Doc.HttpContentType.Attachment)
            End If
            imp.StopImpersonate()
        Catch ex As Exception

        End Try
    End Sub

    Private Function CheckFormatTemplate(ByVal tempPath As String) As Boolean
        Dim totalParam As Integer = 0
        Dim template As Byte() = File.ReadAllBytes(tempPath)
        Using ms As New MemoryStream
            ms.Write(template, 0, template.Length)
            Using doc As WordprocessingDocument = WordprocessingDocument.Open(ms, True)
                Dim body As B.Body = doc.MainDocumentPart.Document.Body
                totalParam = body.Descendants(Of B.Drawing)().Count

                Dim words As List(Of B.Text) = body.Elements(Of B.Table)() _
                                                .SelectMany(Function(s) s.Elements(Of B.TableRow)()) _
                                                .SelectMany(Function(s) s.Elements(Of B.TableCell)()) _
                                                .SelectMany(Function(s) s.Elements(Of B.Paragraph)()) _
                                                .SelectMany(Function(s) s.Elements(Of B.Run)()) _
                                                .SelectMany(Function(s) s.Elements(Of B.Text)()).ToList()

                For Each word As B.Text In words
                    Select Case word.Text
                        Case "<", ">"
                            word.Text = ""
                        Case "<CustomerName>", "CustomerName"
                            word.Text = ""
                            totalParam += 1
                        Case "<CustAddr>", "CustAddr"
                            word.Text = ""
                            totalParam += 1
                        Case "<PhoneNumber>", "PhoneNumber"
                            word.Text = ""
                            totalParam += 1
                        Case "<ModelDesc>", "ModelDesc"
                            word.Text = ""
                            totalParam += 1
                        Case "<ChassisNumber>", "ChassisNumber"
                            word.Text = ""
                            totalParam += 1
                        Case "<EngineNumber>", "EngineNumber"
                            word.Text = ""
                            totalParam += 1
                        Case "<FakturDate>", "FakturDate"
                            word.Text = ""
                            totalParam += 1
                        Case "<PKTDate>", "PKTDate"
                            word.Text = ""
                            totalParam += 1
                    End Select
                Next

            End Using

        End Using

        Return totalParam = 12

    End Function
#End Region


End Class