Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Text
Imports System.Configuration
Imports System.IO
Imports SpireDoc = Spire.Doc
Imports SpireDocument = Spire.Doc.Document
Imports System.Collections.Generic
Imports System.Linq
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports B = DocumentFormat.OpenXml.Wordprocessing


Public Class FrmPDIMasterTemplate
    Inherits System.Web.UI.Page
    Private _view As Boolean = False
    Private _sessHelper As New SessionHelper
    Private _strSessSearch As String = "CRITERIAS"
    Private _arlPDITemplate As ArrayList = New ArrayList
    Dim crt As CriteriaComposite
    Dim arr As ArrayList
    Dim sorts As SortCollection
    Dim TempFile As String
    Dim SrcFile As String
    Dim pdiTemplateFacade As PDITemplateFacade = New PDITemplateFacade(User)
    Dim objPDI As PDITemplate = New PDITemplate
    Dim vechileModelFacade As VechileModelFacade = New VechileModelFacade(User)
    Private m_bInputPrivilege As Boolean = False
    Dim objDealer As New Dealer
    Private oLoginUser As New UserInfo

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            BindDropDown()
            ViewState("CurrentSortColumn") = "StartDate"
            ViewState("CurrentSortDirect") = KTB.DNet.Domain.Search.Sort.SortDirection.DESC
        End If
    End Sub

    Protected Sub ddlModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModel.SelectedIndexChanged
        ddlKategori.Items.Clear()
        If ddlModel.SelectedIndex <> 0 Then
            CommonFunction.BindVehicleModelIndDescriptionToDDL(ddlKategori, ddlModel.SelectedItem.Text)
            ddlKategori.SelectedIndex = 0
        End If
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        Dim strName As String = "PDI_Template.docx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\PDITemplate\" & strName)
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)

        If (ddlModel.SelectedIndex = 0) Or (ddlKategori.SelectedIndex = 0) Or (icPaymentDateFrom.Value = Nothing) Then
            MessageBox.Show("Model, dan Tanggal berlaku Mulai harus diisi.")
            Exit Sub
        End If

        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            Dim path As System.IO.Path
            Dim ext As String = path.GetExtension(DataFile.PostedFile.FileName).Trim.ToLower

            If ext <> ".docx" Then
                MessageBox.Show("Extension file " & DataFile.PostedFile.FileName & " tidak sesuai. Mohon convert file ke *.docx terlebih dahulu.")
                Exit Sub
            End If


            Dim s As String = ddlKategori.SelectedItem.Text
            Dim kb As String = "("
            Dim kt As String = ")"
            Dim i As Integer = s.IndexOf(kb)
            Dim j As Integer = s.IndexOf(kt)

            If i > -1 AndAlso j > -1 Then '-1 means the word was not found.
                Dim res As String = Strings.Mid(s, i + kb.Length + 1, j - i - kb.Length) 'Crop the text between
                SrcFile = res
            End If

            SrcFile = SrcFile & "_" & icPaymentDateFrom.Value.ToString("ddMMyyyy")
            TempFile = Server.MapPath("") & "\..\DataTemp\" & SrcFile & ".docx" '-- Temporary file

            '-- Copy data file from client to server temporary folder

            Dim objUpload As New UploadToWebServer
            objUpload.Upload(DataFile.PostedFile.InputStream, TempFile)
            '_sessHelper.SetSession("PDITemplate", objUpload)
            If Not CheckFormatTemplate(TempFile) Then
                MessageBox.Show("Fomat template tidak sesuai. Mohon menggunakan file contoh template yang telah disediakan.")
                Exit Sub
            End If

            MessageBox.Show("Berhasil Mengupload " & DataFile.PostedFile.FileName & " ke web server. Harap melakukan Preview sebelum Simpan.")
            btnPreview.Visible = True
            btnSimpan.Enabled = True
            ddlModel.Enabled = False
            ddlKategori.Enabled = False
            icPaymentDateFrom.Enabled = False
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If

    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs)
        If icPaymentDateFrom.Value < New Date().Now.Date Then
            MessageBox.Show("Tanggal berlaku tidak boleh kurang dari hari ini.")
            Exit Sub
        End If

        Dim s As String = ddlKategori.SelectedItem.Text
        Dim kb As String = "("
        Dim kt As String = ")"
        Dim i As Integer = s.IndexOf(kb)
        Dim j As Integer = s.IndexOf(kt)

        If i > -1 AndAlso j > -1 Then
            Dim res As String = Strings.Mid(s, i + kb.Length + 1, j - i - kb.Length) 'Crop the text between
            SrcFile = res
        End If

        SrcFile = SrcFile & "_" & icPaymentDateFrom.Value.ToString("ddMMyyyy")
        TempFile = Server.MapPath("") & "\..\DataTemp\" & SrcFile & ".docx"
        SaveFile(TempFile, SrcFile)

        Dim bCheckSuccess As Boolean = True

        Dim objPDIFacade As PDITemplateFacade = New PDITemplateFacade(User)

        Dim nResult As Integer

        Dim pdiTemplate As PDITemplate = CType(_sessHelper.GetSession("PDITemplate"), PDITemplate)
        Dim isExist As Boolean = IsDataExist(pdiTemplate)

        PopulateData(objPDI)

        Dim result As Integer = 0
        If isExist Then
            If objPDI.ValidFrom <> icPaymentDateFrom.Value Then
                If New Date().Now.Date = icPaymentDateFrom.Value Then
                    objPDI.Status = 0
                    result = pdiTemplateFacade.Update(objPDI)
                    objPDI.Status = 1
                Else
                    objPDI.Status = 0
                End If
                objPDI.ValidFrom = icPaymentDateFrom.Value
                objPDI.FileName = "PDITemplate\" & icPaymentDateFrom.Value.ToString("yyyy") & "\" & icPaymentDateFrom.Value.ToString("MM") & "\" & SrcFile & ".docx"
                result = pdiTemplateFacade.Insert(objPDI)
            ElseIf objPDI.ValidFrom = icPaymentDateFrom.Value Then
                result = pdiTemplateFacade.Update(objPDI)
            End If
        Else
            objPDI.Status = 1
            objPDI.ValidFrom = icPaymentDateFrom.Value
            If objPDI.ValidFrom > New Date().Now.Date Then
                objPDI.Status = 0
            End If
            objPDI.FileName = "PDITemplate\" & icPaymentDateFrom.Value.ToString("yyyy") & "\" & icPaymentDateFrom.Value.ToString("MM") & "\" & SrcFile & ".docx"
            objPDI.RowStatus = 0
            result = pdiTemplateFacade.Insert(objPDI)
        End If


        If bCheckSuccess Then
            MessageBox.Show("Semua record berhasil disimpan !")
            btnSimpan.Enabled = False
            btnPreview.Visible = False
            ddlModel.Enabled = True
            ddlKategori.Enabled = True
            icPaymentDateFrom.Enabled = True
        Else
            MessageBox.Show("Ada record yang gagal disimpan !")
        End If

    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs)
        _arlPDITemplate = _sessHelper.GetSession("sessPDI")

        dgPDITemplate.CurrentPageIndex = 0
        BindUploadPDITemplate()
    End Sub

    Protected Sub dgPDITemplate_ItemCommand(source As Object, e As DataGridCommandEventArgs)
        Dim hdFilename As HiddenField = CType(e.Item.FindControl("hdFilename"), HiddenField)
        Select Case e.CommandName
            Case "download"
                Response.Redirect("../download.aspx?file=" & hdFilename.Value)
        End Select
    End Sub

    Protected Sub dgPDITemplate_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        bindItemDgPDI(e)
    End Sub

    Protected Sub btnPreview_Click(sender As Object, e As EventArgs)
        Dim s As String = ddlKategori.SelectedItem.Text
        Dim kb As String = "("
        Dim kt As String = ")"
        Dim i As Integer = s.IndexOf(kb)
        Dim j As Integer = s.IndexOf(kt)

        If i > -1 AndAlso j > -1 Then '-1 means the word was not found.
            Dim res As String = Strings.Mid(s, i + kb.Length + 1, j - i - kb.Length) 'Crop the text between
            SrcFile = res
        End If

        SrcFile = SrcFile & "_" & icPaymentDateFrom.Value.ToString("ddMMyyyy")
        TempFile = Server.MapPath("") & "\..\DataTemp\" & SrcFile & ".docx"
        DownloadPdfFile(TempFile)
    End Sub

    Protected Sub dgPDITemplate_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs)
        BindUploadPDITemplate(e.NewPageIndex)
    End Sub

    Protected Sub dgPDITemplate_SortCommand(source As Object, e As DataGridSortCommandEventArgs)
        If e.SortExpression = ViewState.Item("SortCol") Then
            If ViewState.Item("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Item("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState.Item("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState.Item("SortCol") = e.SortExpression
        dgPDITemplate.SelectedIndex = -1
        dgPDITemplate.CurrentPageIndex = 0
        BindUploadPDITemplate(0)
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs)
        ddlKategori.SelectedIndex = -1
        ddlModel.SelectedIndex = -1
        ddlStatus.SelectedIndex = -1
        icPaymentDateFrom.Value = New Date().Now
        btnSimpan.Enabled = False
        btnPreview.Visible = False
        ddlModel.Enabled = True
        ddlKategori.Enabled = True
        icPaymentDateFrom.Enabled = True
    End Sub

#End Region

#Region "Custom Method"
    Private Sub CheckPrivilege()
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.PDIMasterTemplate_Input_Privilage)

        If Not m_bInputPrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - PDI - PDI Master Template")
        End If
    End Sub

    Private Sub BindDropDown()
        'dropdown category
        crt = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.No, "CV"))
        sorts = New SortCollection
        sorts.Add(New Sort(GetType(Category), "CategoryCode", Search.Sort.SortDirection.ASC))
        arr = New CategoryFacade(User).RetrieveByCriteria(crt, sorts)

        ddlModel.Items.Clear()
        ddlModel.DataSource = arr
        ddlModel.DataTextField = "CategoryCode".ToUpper
        ddlModel.DataValueField = "ID"
        ddlModel.DataBind()
        ddlModel.Items.Insert(0, New ListItem("Silakan Pilih", 0))
        ddlModel.SelectedIndex = 0
        ddlModel_SelectedIndexChanged(Me, System.EventArgs.Empty)

        ' dropdown status
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = New EnumMSPMasterStatus().Retrieve()
        ddlStatus.DataTextField = "NameTitle".ToUpper
        ddlStatus.DataValueField = "ValTitle"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silakan Pilih", -1))
        ddlStatus.SelectedIndex = 0

    End Sub

    Private Function InsertPDITemplate(ByVal objPDI As PDITemplate) As Integer
        Dim objPDIFacade As PDITemplateFacade = New PDITemplateFacade(User)
        Return objPDIFacade.Insert(objPDI)
    End Function

    Private Function SaveFile(ByVal tempfile As String, ByVal srcfile As String) As Integer
        Dim nResult As Integer = -1
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("PDITemplate") & icPaymentDateFrom.Value.ToString("yyyy") & "\" & icPaymentDateFrom.Value.ToString("MM") & "\"
        Dim _user As String = KTB.DNET.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNET.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNET.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfoDest As FileInfo
        Dim finfoSource As FileInfo

        Try
            success = imp.Start()
            If success Then
                finfoSource = New FileInfo(tempfile)
                finfoDest = New FileInfo(DestFile & srcfile & ".docx")

                If Not finfoDest.Directory.Exists Then
                    Directory.CreateDirectory(finfoDest.DirectoryName)
                End If

                If finfoDest.Exists Then
                    finfoDest.Delete()
                End If

                finfoSource.CopyTo(DestFile & SrcFile & ".docx")
                nResult = 1
                imp.StopImpersonate()
            End If
        Catch ex As Exception
            nResult = -1
            Throw ex
        End Try
        'Return nResult
    End Function

    Private Sub BindUploadPDITemplate(Optional indexPage As Integer = 0)
        Dim totalRow As Integer = 0
        _arlPDITemplate = New ArrayList
        Dim _arlValid As ArrayList = New ArrayList
        Dim _arlInValid As ArrayList = New ArrayList
        Dim intStatus As Integer = 0

        Try
            _arlPDITemplate = _sessHelper.GetSession("sessPDI")
            Dim criterias As New CriteriaComposite(New Criteria(GetType(PDITemplate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PDITemplate), "ValidFrom", MatchType.GreaterOrEqual, icPaymentDateFrom.Value))

            If ddlKategori.SelectedIndex > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDITemplate), "VechileModel.ID", MatchType.Exact, ddlKategori.SelectedValue))
            End If

            If ddlStatus.SelectedIndex > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDITemplate), "Status", MatchType.Exact, ddlStatus.SelectedValue))
            End If

            If ddlModel.SelectedValue > 0 Then
                criterias.opAnd(New Criteria(GetType(PDITemplate), "VechileModel.Category.ID", MatchType.Exact, ddlModel.SelectedValue))
            End If

            Dim objPDIAl = New PDITemplateFacade(User).Retrieve(criterias, sorts)
            _sessHelper.SetSession("PDITemplateAl", objPDIAl)
            Dim data As ArrayList = New PDITemplateFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgPDITemplate.PageSize, totalRow, ViewState.Item("SortCol"), ViewState.Item("SortDirection"))
            If data.Count = 0 Then
                dgPDITemplate.CurrentPageIndex = 0
            Else
                dgPDITemplate.CurrentPageIndex = indexPage
            End If

            dgPDITemplate.DataSource = data
            dgPDITemplate.VirtualItemCount = totalRow
            dgPDITemplate.DataBind()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            lblMessage.Text = ""
        End Try
    End Sub

    Private Sub bindItemDgPDI(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            Dim objPDI As PDITemplate
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblPDIStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblModel As Label = CType(e.Item.FindControl("lblModel"), Label)
            Dim lbDownload As LinkButton = CType(e.Item.FindControl("lbDownload"), LinkButton)

            objPDI = CType(e.Item.DataItem, PDITemplate)
            lblNo.Text = (e.Item.ItemIndex + 1) + (dgPDITemplate.CurrentPageIndex * dgPDITemplate.PageSize)

            If objPDI.Status = 1 Then
                lblPDIStatus.Text = "Aktif"
            ElseIf objPDI.Status = 0 Then
                lblPDIStatus.Text = "Tidak Aktif"
            End If

            lbDownload.Visible = Not String.IsNullorEmpty(objPDI.FileName)

        End If

    End Sub

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
                tempPath = tempPath.Replace(".docx", "")
                pdfDoc.SaveToFile(tempPath & ".pdf", Spire.Doc.FileFormat.PDF, Response, Spire.Doc.HttpContentType.Attachment)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function IsDataExist(ByRef pditemplate As PDITemplate) As Boolean
        Dim isExist As Boolean = False
        Dim arrExist As ArrayList = New ArrayList
        Dim crit As CriteriaComposite
        crit = New CriteriaComposite(New Criteria(GetType(PDITemplate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(PDITemplate), "VechileModel.ID", MatchType.Exact, ddlKategori.SelectedValue.ToString()))
        crit.opAnd(New Criteria(GetType(PDITemplate), "Status", MatchType.Exact, 1))

        Dim pdiTemplateFacade As PDITemplateFacade = New PDITemplateFacade(User)
        arrExist = pdiTemplateFacade.Retrieve(crit)
        If arrExist.Count > 0 Then
            Dim obj As PDITemplate = CType(arrExist(0), PDITemplate)
            objPDI.ID = obj.ID
            objPDI.ValidFrom = obj.ValidFrom
            objPDI.Status = 1
            isExist = True
        End If

        Return isExist
    End Function

    Private Sub PopulateData(ByVal pdiTemplate As PDITemplate)
        pdiTemplate.VechileModel = vechileModelFacade.RetrieveByID(ddlKategori.SelectedValue)
        pdiTemplate.Status = IIf(CDate(icPaymentDateFrom.Value) > DateTime.Now.Date, 0, 1)

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
                        Case "<ModelKendaraan>", "ModelKendaraan"
                            word.Text = ""
                            totalParam += 1
                        Case "<NomorRangka>", "NomorRangka"
                            word.Text = ""
                            totalParam += 1
                        Case "<TglPelaksanaan>", "TglPelaksanaan"
                            word.Text = ""
                            totalParam += 1
                        Case "<NamaDealer>", "NamaDealer"
                            word.Text = ""
                            totalParam += 1
                        Case "<WONumber>", "WONumber"
                            word.Text = ""
                            totalParam += 1
                    End Select
                Next

            End Using

        End Using

        Return totalParam = 5

    End Function

#End Region
End Class