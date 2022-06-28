Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SPAF
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text

Public Class FrmUploadSPAF
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ltrTitle As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrCompanyName As System.Web.UI.WebControls.Label
    Protected WithEvents fuUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents dgUploadData As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents lblUploadInfo As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Private ObjDealer As Dealer
    Private isSPAF As Boolean
    Private bIsError As Boolean = False
    Private sHelper As New SessionHelper
    Private dt As DateTime = DateTime.Now
    Private Suffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
#End Region

#Region "Custome Methods"

    Private Sub CheckDuplicateChassisNumber(ByRef NewList As ArrayList)
        Dim nIndex As Integer
        Dim nIterate As Integer = 1
        For Each item As SPAFDoc In NewList
            If Not IsNothing(item.ChassisMaster) Then
                For nIndex = nIterate To NewList.Count - 1
                    Dim item2 As SPAFDoc
                    item2 = NewList(nIndex)
                    If Not IsNothing(item2.ChassisMaster) Then
                        Dim sChassisNumber2 = item2.ChassisMaster.ChassisNumber
                        Dim sChassisNumber1 = item.ChassisMaster.ChassisNumber

                        If (sChassisNumber1 = sChassisNumber2) And (item.ReffLetter.Trim.ToLower = item2.ReffLetter.Trim.ToLower) Then
                            If item2.ErrorMessage = "" Then
                                item2.ErrorMessage = "Data Sudah Ada (Data Duplikat) dg Record " + CType(nIterate, String) & ". "
                            Else
                                item2.ErrorMessage = item2.ErrorMessage + "<br/> Data Sudah Ada (Data Duplikat) dg Record " + CType(nIterate, String) & ". "
                            End If
                        End If
                    End If
                Next
            End If
            nIterate = nIterate + 1
        Next
    End Sub

    Private Sub AddDetail(ByRef arrL As ArrayList)
        Dim nIndex As Integer
        Dim nIterate As Integer = 1
        Dim pph As Decimal = (New PPhFacade(User)).RetrievePPh
        For Each item As SPAFDoc In arrL
            item.PPhPercent = pph
            item.CreatedBy = CType(User.Identity.Name, String)
            item.Status = EnumSPAFSubsidy.SPAFDocStatus.Baru
            item.DocType = IIf(isSPAF, EnumSPAFSubsidy.DocumentType.SPAF, EnumSPAFSubsidy.DocumentType.Subsidi)

            Try
                If (item.ChassisMaster.ID > 0) Then

                    item.OrderDealer = item.ChassisMaster.Dealer.DealerCode
                    Dim arrlCM As New ArrayList
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "VechileType.ID", MatchType.Exact, item.ChassisMaster.VechileColor.VechileType.ID))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "ValidFrom", MatchType.LesserOrEqual, item.DateLetter))

                    Dim sortColl As SortCollection = New SortCollection
                    sortColl.Add(New Search.Sort(GetType(ConditionMaster), "ValidFrom", Sort.SortDirection.DESC))

                    arrlCM = New SPAF.ConditionMasterFacade(User).Retrieve(criterias, sortColl)

                    'Get Retail Price From Database

                    If arrlCM.Count = 0 Then
                        item.RetailPrice = 0
                        item.SPAF = 0
                        item.Subsidi = 0
                        item.ErrorMessage = item.ErrorMessage & "Harga Tidak Valid. "

                    ElseIf item.RetailPrice <> CType(arrlCM(0), ConditionMaster).RetailPrice Then
                        Dim _retailPrice = CType(arrlCM(0), ConditionMaster).RetailPrice
                        If item.RetailPrice = CType(-99999, Decimal) Then
                            item.RetailPrice = CType(arrlCM(0), ConditionMaster).RetailPrice
                            item.SPAF = IIf(isSPAF, (CType(arrlCM(0), ConditionMaster).SPAF / 100) * CType(arrlCM(0), ConditionMaster).RetailPrice, 0)
                            item.Subsidi = IIf(isSPAF, 0, CType(arrlCM(0), ConditionMaster).Subsidi)

                        Else
                            item.RetailPrice = CType(arrlCM(0), ConditionMaster).RetailPrice
                            item.SPAF = IIf(isSPAF, (CType(arrlCM(0), ConditionMaster).SPAF / 100) * CType(arrlCM(0), ConditionMaster).RetailPrice, 0)
                            item.ErrorMessage = item.ErrorMessage & "Harga Tidak Valid. "
                        End If
                    Else
                        item.RetailPrice = CType(arrlCM(0), ConditionMaster).RetailPrice
                        item.SPAF = IIf(isSPAF, (CType(arrlCM(0), ConditionMaster).SPAF / 100) * CType(arrlCM(0), ConditionMaster).RetailPrice, 0)
                        item.Subsidi = IIf(isSPAF, 0, CType(arrlCM(0), ConditionMaster).Subsidi)
                    End If

                    '---cek chassis dengan database sesuai tipe dokumen dan statusnya <> (ditolak,dihapus)
                    '-- kalo data ditemukan --> error message data sudah ada 
                    Dim _arrTmp As ArrayList = New ArrayList
                    Dim _status As String = String.Empty
                    _status = CInt(EnumSPAFSubsidy.SPAFDocStatus.Deleted).ToString & "," & CInt(EnumSPAFSubsidy.SPAFDocStatus.Ditolak).ToString

                    '---cek No kontrak di file teks
                    'Dim _refx As String = item.ReffLetter
                    'For j As Integer = nIterate To arrL.Count - 1
                    '    Dim _refy As String = CType(arrL(j), SPAFDoc).ReffLetter
                    '    If _refx = _refy Then
                    '        If CType(arrL(j), SPAFDoc).ErrorMessage = "" Then
                    '            CType(arrL(j), SPAFDoc).ErrorMessage = "No Kontrak Sudah Ada / Duplikat dg Record " & +CType(nIterate, String) & ". <br/>"
                    '        Else
                    '            CType(arrL(j), SPAFDoc).ErrorMessage = CType(arrL(j), SPAFDoc).ErrorMessage & "No Kontrak Sudah Ada / Duplikat dg Record " & +CType(nIterate, String) & ". <br/>"
                    '        End If
                    '    End If
                    'Next

                    '--cek no kontrak di database
                    Dim criteriaKontrak As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteriaKontrak.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "ReffLetter", MatchType.Exact, item.ReffLetter))
                    criteriaKontrak.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "Status", MatchType.NotInSet, _status))

                    If isSPAF Then
                        criteriaKontrak.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", CInt(EnumSPAFSubsidy.DocumentType.SPAF)))
                    Else
                        criteriaKontrak.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", CInt(EnumSPAFSubsidy.DocumentType.Subsidi)))
                    End If

                    'Modify by Ags on 25 Maret 2010 for DonSet's Request
                    '_arrTmp = New SPAF.SPAFFacade(User).Retrieve(criteriaKontrak)

                    'If _arrTmp.Count > 0 Then
                    '    item.ErrorMessage = item.ErrorMessage & "No Kontrak Sudah Ada Di Database. "
                    'End If

                    For nIndex = nIterate To arrL.Count - 1
                        Dim item2 As SPAFDoc
                        item2 = arrL(nIndex)
                        If Not IsNothing(item2.ChassisMaster) Then
                            Dim sChassisNumber2 = item2.ChassisMaster.ChassisNumber
                            Dim sChassisNumber1 = item.ChassisMaster.ChassisNumber

                            If (sChassisNumber1 = sChassisNumber2) Then
                                If item2.ErrorMessage = "" Then
                                    item2.ErrorMessage = "No Rangka Sudah Ada / Duplikat dg Record " + CType(nIterate, String) & ". "
                                Else
                                    item2.ErrorMessage = item2.ErrorMessage + "No Rangka Sudah Ada / Duplikat dg Record " + CType(nIterate, String) & ". "
                                End If
                            End If
                        End If
                    Next

                    '---cek chassis dengan database sesuai tipe dokumen dan statusnya <> (ditolak,dihapus)
                    '-- kalo data ditemukan --> error message data sudah ada 
                    Dim criteriaforChassis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "ChassisMaster.ID", MatchType.Exact, item.ChassisMaster.ID))
                    criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "Status", MatchType.NotInSet, _status))

                    If isSPAF Then
                        criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", CInt(EnumSPAFSubsidy.DocumentType.SPAF)))
                    Else
                        criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", CInt(EnumSPAFSubsidy.DocumentType.Subsidi)))
                    End If

                    _arrTmp = New SPAF.SPAFFacade(User).Retrieve(criteriaforChassis)

                    If _arrTmp.Count > 0 Then

                        item.ErrorMessage = StrIfChasisNumExist(_arrTmp) '"No Rangka Sudah Ada Di Database. <br/>" '---data sudah ada di database                   
                    End If
                End If
            Catch ex As Exception
                'do nothing to skip error
            End Try

            nIterate = nIterate + 1
        Next
    End Sub

    Private Sub AddUserCreatorAndStatus(ByRef arrL As ArrayList)
        For Each item As SPAFDoc In arrL
            item.CreatedBy = CType(User.Identity.Name, String)
            item.Status = EnumSPAFSubsidy.SPAFDocStatus.Baru

        Next
    End Sub

    Private Sub AddDocType(ByRef arrL As ArrayList)
        For Each item As SPAFDoc In arrL
            item.DocType = IIf(isSPAF, EnumSPAFSubsidy.DocumentType.SPAF, EnumSPAFSubsidy.DocumentType.Subsidi)
        Next
    End Sub

    Private Function GetValueConditionMaster(ByRef arrL As ArrayList) As ConditionMaster
        Dim dt1 As New Date
        Dim dt2 As New Date
        Dim dttmp As New Date
        Dim i As Integer = 0
        Dim CM As New ConditionMaster
        For idx As Integer = 0 To arrL.Count - 1
            Dim item As SPAFDoc = arrL(idx)
            'Next
            'For Each item As SPAFDoc In arrL

            If (Not item.ChassisMaster Is Nothing) Then
                Dim arrlCM As New ArrayList
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "VechileType", MatchType.Exact, item.ChassisMaster.VechileColor.VechileType.ID))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ConditionMaster), "ValidFrom", MatchType.LesserOrEqual, item.DateLetter))

                Dim sortColl As SortCollection = New SortCollection
                sortColl.Add(New Search.Sort(GetType(ConditionMaster), "ValidFrom", Sort.SortDirection.DESC))

                arrlCM = New SPAF.ConditionMasterFacade(User).Retrieve(criterias, sortColl)

                If arrlCM.Count = 0 Then
                    item.RetailPrice = 0
                    item.SPAF = 0
                    item.Subsidi = 0
                    item.ErrorMessage = item.ErrorMessage & "Harga Tidak Valid. <br/>"
                Else
                    item.RetailPrice = CType(arrlCM(0), ConditionMaster).RetailPrice
                    item.SPAF = IIf(isSPAF, (CType(arrlCM(0), ConditionMaster).SPAF / 100) * CType(arrlCM(0), ConditionMaster).RetailPrice, 0)
                    item.Subsidi = IIf(isSPAF, 0, CType(arrlCM(0), ConditionMaster).Subsidi)
                End If
            End If
            arrL(idx) = item
        Next
    End Function

    Private Sub CopyToWSM()
        If (Not fuUpload.PostedFile Is Nothing) And (fuUpload.PostedFile.ContentLength > 0) And _
        ((fuUpload.PostedFile.ContentType.ToLower() = "text/plain") Or (fuUpload.PostedFile.ContentType.ToLower() = "text/csv") _
        Or (fuUpload.PostedFile.ContentType.ToLower() = "application/octet-stream") Or (fuUpload.PostedFile.ContentType.ToLower() = "application/vnd.ms-excel")) Then
            Dim Extension As String = Path.GetExtension(fuUpload.PostedFile.FileName)

            If Extension.ToUpper = ".TXT" Then
                'cek maxFileSize first
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                If fuUpload.PostedFile.ContentLength > maxFileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    Exit Sub
                End If

                Dim SrcFile As String = Path.GetFileName(fuUpload.PostedFile.FileName)
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN1") & KTB.DNet.Lib.WebConfig.GetValue("SPAFFolderInWSM") & KTB.DNet.Lib.WebConfig.GetValue("SPAFInPrefix") & SrcFile

                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _SAPServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer")
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _SAPServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        fuUpload.PostedFile.SaveAs(DestFile)
                        fuUpload.PostedFile.SaveAs(DestFile & ".wts")
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    'Throw ex
                    If ex.Source = "mscorlib" AndAlso (" " & ex.Message).IndexOf("because it is being used by another process") > 0 Then
                        MessageBox.Show("File sedang diupload, silahkan tunggu beberapa saat")
                    Else
                        MessageBox.Show("Silahkan tunggu beberapa saat")
                    End If
                End Try
            Else
                MessageBox.Show("Jenis file tidak sesuai")
            End If
        Else
            MessageBox.Show("Pilih file yang akan di-upload.")
        End If
    End Sub

    Private Sub BindUpload()
        If (Not fuUpload.PostedFile Is Nothing) And (fuUpload.PostedFile.ContentLength > 0) And _
        ((fuUpload.PostedFile.ContentType.ToLower() = "text/plain") Or (fuUpload.PostedFile.ContentType.ToLower() = "text/csv") _
        Or (fuUpload.PostedFile.ContentType.ToLower() = "application/octet-stream") Or (fuUpload.PostedFile.ContentType.ToLower() = "application/vnd.ms-excel")) Then
            Dim Extension As String = Path.GetExtension(fuUpload.PostedFile.FileName)

            If Extension.ToUpper = ".TXT" Then
                'cek maxFileSize first
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                If fuUpload.PostedFile.ContentLength > maxFileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    Exit Sub
                End If

                Dim SrcFile As String = Path.GetFileName(fuUpload.PostedFile.FileName)
                Dim DestFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile & System.Guid.NewGuid().ToString()
                Try
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(fuUpload.PostedFile.InputStream, DestFile)

                    Dim parser As IParser = New SPAFParser
                    Dim arList As ArrayList = New ArrayList
                    arList = CType(parser.ParseNoTransaction(DestFile, "User"), ArrayList)
                    'AddDocType(arList)
                    'AddUserCreatorAndStatus(arList)
                    'GetValueConditionMaster(arList)
                    'CheckDuplicateChassisNumber(arList)

                    'Start  :dna:temporary remove AddDetail process to optimize process time : 20100518
                    'AddDetail(arList)
                    'End    :dna:temporary remove AddDetail process to optimize process time : 20100518

                    'Todo session
                    'Session("DataUpload") = arList
                    sHelper.SetSession("DataUpload", arList)
                    dgUploadData.DataSource = arList
                    dgUploadData.DataBind()
                    'ViewState("Process") = "View"
                    objUpload.DeleteFile(DestFile)
                    'File.Delete(DestFile)

                Catch Exc As Exception
                    Dim objUpload As New UploadToWebServer
                    objUpload.DeleteFile(DestFile)
                    MessageBox.Show(SR.UploadFail(SrcFile))
                End Try
            Else
                MessageBox.Show("Jenis file tidak sesuai")
            End If
        Else
            MessageBox.Show("Pilih file yang akan di-upload.")
        End If
    End Sub
    Private Sub checkFileExistenceToDownload()
        Dim finfo As FileInfo = New FileInfo(Me.Server.MapPath("") & "\..\DataTemp\ConditionMaster" & Suffix & ".txt")
        If finfo.Exists Then
            finfo.Delete()
        End If
    End Sub
    Private Sub saveToTextFile(ByVal str As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Try
            'Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\SPAFUpload" & Suffix & ".txt", FileMode.Append, FileAccess.Write)
            Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\SPAFUpload" & Suffix & ".csv", FileMode.Append, FileAccess.Write)

            Dim objStreamWriter As New StreamWriter(objFileStream)
            objStreamWriter.WriteLine(str)
            objStreamWriter.Close()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub DownloadPage()

        Response.Redirect("FrmDownloadErrorSPAF.aspx")
        'Response.Redirect("TestDownload.aspx")
        'Response.ContentType = "application/x-download"
        'Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("ErrorSPAF" & DateTime.Now.ToString() & ".xls").Append("""").ToString()
        'Response.AddHeader("Content-Disposition", _
        '    New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
    End Sub

    Private Sub Download()
        Dim textBuilt As New StringBuilder
        Dim delimiter As String = ";"
        checkFileExistenceToDownload()
        Dim objData As ArrayList = CType(sHelper.GetSession("DataUpload"), ArrayList)
        If objData Is Nothing OrElse objData.Count = 0 Then
            MessageBox.Show("Tidak ada data yg bisa di download")
            Return
        End If
        For Each spDoc As SPAFDoc In objData
            textBuilt.Append(spDoc.SPAFFType)
            textBuilt.Append(delimiter)
            Try
                textBuilt.Append(spDoc.Dealer.DealerCode)
            Catch ex As Exception
                textBuilt.Append("-")
            End Try
            textBuilt.Append(delimiter)
            textBuilt.Append(spDoc.ReffLetter)
            textBuilt.Append(delimiter)
            textBuilt.Append(spDoc.DateLetter.ToString("dd/MM/yyyy"))
            textBuilt.Append(delimiter)
            textBuilt.Append(spDoc.CustomerName)
            textBuilt.Append(delimiter)
            If Not IsNothing(spDoc.ChassisMaster) Then
                textBuilt.Append(spDoc.ChassisMaster.ChassisNumber)
            Else
                textBuilt.Append("-")
            End If
            textBuilt.Append(delimiter)
            textBuilt.Append(spDoc.DealerLeasing)
            textBuilt.Append(delimiter)
            textBuilt.Append(CType(spDoc.SellingType, EnumSellingType.SellingType).ToString)
            textBuilt.Append(delimiter)
            textBuilt.Append(spDoc.ErrorMessage)
            Try
                saveToTextFile(textBuilt.ToString)
                textBuilt = textBuilt.Remove(0, textBuilt.Length)
            Catch ex As Exception
                MessageBox.Show("Persiapan Proses Download gagal")
                Return
            End Try
        Next
        'Response.Redirect("../downloadlocal.aspx?file=DataTemp\SPAFUpload" & Suffix & ".txt")
        Response.Redirect("../downloadlocal.aspx?file=DataTemp\SPAFUpload" & Suffix & ".csv")

    End Sub
#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Page.Server.ScriptTimeout = 500
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ObjDealer = CType(sHelper.GetSession("DEALER"), Dealer)
        isSPAF = IIf(Convert.ToBoolean(Request.QueryString("isSPAF")) = True, True, False)
        If isSPAF = True Then
            InitiateSPAFAuthorization()
        Else
            InitiateSubsidiAuthorization()
        End If
        If Not IsNothing(Me.sHelper.GetSession("FrmUploadSPAF.UploadedFile")) AndAlso Me.fuUpload.PostedFile.FileName.Trim = String.Empty Then
            Me.fuUpload = CType(Me.sHelper.GetSession("FrmUploadSPAF.UploadedFile"), HtmlInputFile)
        End If
        If Not IsPostBack Then
            Me.sHelper.SetSession("FrmUploadSPAF.UploadedFile", Nothing)
            ViewState("CurrentSortColumn") = "id"
            ViewState("CurrentSortDirect") = "ASC"
            'ViewState("Process") = Nothing
            btnSimpan.Enabled = False
            ltrCompanyName.Text = ObjDealer.DealerCode + " / " + ObjDealer.DealerName
            ltrTitle.Text = IIf(isSPAF, "SPAF", "Subsidi")
            dgUploadData.Columns(8).Visible = IIf(isSPAF, True, False)
            dgUploadData.Columns(10).Visible = IIf(isSPAF, False, True)
        End If
    End Sub

    
    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Me.btnSimpan.Enabled = True
        Dim startTime As DateTime = DateTime.Now

        Response.Write("Start Processing at : " & Format(startTime, "HH:mm:ss"))
        If fuUpload.PostedFile.FileName = String.Empty Then
            MessageBox.Show("Pilih File Terlebih Dahulu")
            Return
        End If
        bIsError = False
        'Todo session
        'Session("isError") = bIsError
        sHelper.SetSession("isError", bIsError)
        isSPAF = IIf(Convert.ToBoolean(Request.QueryString("isSPAF")) = True, True, False)

        'Start  :by:dna;for:donset;to:optimize upload process;on:20100525
        Dim objSDUH As SPAFDoc_UploadHeader = Nothing
        lblUploadInfo.Text = ""
        sHelper.SetSession("FrmUploadSPAF.UploadedFile", Me.fuUpload)
        If IsFileAlreadyUploaded(objSDUH) Then
            Dim arList As New ArrayList
            If objSDUH.Status = enumSPAFUpload.SPAFUpload.Initialization Then
                lblUploadInfo.Text = "Uploading data...(" & objSDUH.SPAFDoc_UploadDetails.Count & " data are already uploaded)"
                arList = New ArrayList
                'AddDetail(arList)
                sHelper.SetSession("DataUpload", arList)
                dgUploadData.DataSource = arList
                dgUploadData.DataBind()
            Else
                lblUploadInfo.Text = "Number Of Data : " & objSDUH.NumberOfData & IIf(objSDUH.NumberOfError > 0, ", Error Uploaded:" & objSDUH.NumberOfError, "")
                arList = objSDUH.SPAFDocs
                'AddDetail(arList)
                sHelper.SetSession("DataUpload", arList)
                dgUploadData.DataSource = arList
                dgUploadData.DataBind()
            End If
        Else
            lblUploadInfo.Text = "Processing Upload..."
            CopyToWSM()
        End If
        Dim EndTime As DateTime = DateTime.Now

        Response.Write(", End Processing at : " & Format(EndTime, "HH:mm:ss"))
        Response.Write(", Total Processing Time :" & DateDiff(DateInterval.Second, EndTime, startTime) & " second")

        Exit Sub
        'End    :by:dna;for:donset;to:optimize upload process;on:20100525

        'BindUpload()
        'bIsError = CType(sHelper.GetSession("isError"), Boolean)
        'If Path.GetFileName(fuUpload.PostedFile.FileName).ToString.Trim <> String.Empty Then
        '    btnSimpan.Text = "Simpan"
        '    btnSimpan.Enabled = True
        'Else
        '    btnSimpan.Enabled = False
        'End If

        'Dim EndTime As DateTime = DateTime.Now

        'Response.Write(", End Processing at : " & Format(EndTime, "HH:mm:ss"))
        'Response.Write(", Total Processing Time :" & DateDiff(DateInterval.Second, EndTime, startTime) & " second")


    End Sub

    Private Sub dgUploadData_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUploadData.ItemDataBound
        'If ViewState("Process") = "View" Then
        '    Return
        'End If
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
            Dim oSPAF As SPAFDoc = CType(e.Item.DataItem, SPAFDoc)

            Dim lblMessage As Label = CType(e.Item.FindControl("lblMessage"), Label)
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim lblNoChassis As Label = CType(e.Item.FindControl("lblNoChassis"), Label)
            Dim oDealer As New Dealer
            oDealer = New General.DealerFacade(User).GetDealer(oSPAF.OrderDealer)
            lblDealerName.Text = oDealer.DealerName
            If Not IsNothing(oSPAF.ChassisMaster) AndAlso oSPAF.ChassisMaster.ID > 0 Then
                lblNoChassis.Text = oSPAF.ChassisMaster.ChassisNumber
            Else
                lblNoChassis.Text = ""
            End If
            If isSPAF Then
                If oSPAF.SPAFFType.ToUpper <> "SPAF" Then
                    oSPAF.ErrorMessage = "Kode Transaksi Tidak Sama(Tipe Dok Bukan SPAF). " & oSPAF.ErrorMessage
                ElseIf oSPAF.SPAFFType.ToUpper = String.Empty Then
                    oSPAF.ErrorMessage = "Kode Transaksi Tidak Sama(Tipe Dok Bukan SPAF)." & oSPAF.ErrorMessage
                End If
            Else
                If oSPAF.SPAFFType.ToUpper <> "SUBSIDI" Then
                    oSPAF.ErrorMessage = "Kode Transaksi Tidak Sama(Tipe Dok Bukan SUBSIDI)." & oSPAF.ErrorMessage
                ElseIf oSPAF.SPAFFType.ToUpper = String.Empty Then
                    oSPAF.ErrorMessage = "Kode Transaksi Tidak Sama(Tipe Dok Bukan SUBSIDI)." & oSPAF.ErrorMessage
                End If
            End If
            If Not oSPAF.ErrorMessage <> "" Then
                lblMessage.Text = "OK"
            Else
                lblMessage.Text = oSPAF.ErrorMessage
                bIsError = True
            End If
        End If
        'Todo session
        'Session("isError") = bIsError
        sHelper.SetSession("isError", bIsError)
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        '--modif by ronny to handle user thats open more than 1 page
        Dim _arrTmp As ArrayList = New ArrayList
        Dim _status As String = String.Empty
        Dim _arrData As ArrayList
        Dim _arrValid As New ArrayList
        Dim _arrNotValid As New ArrayList
        _arrData = CType(sHelper.GetSession("DataUpload"), ArrayList)
        If btnSimpan.Text = "Simpan" Then
            For Each item As SPAFDoc In _arrData
                If item.ErrorMessage Is Nothing OrElse item.ErrorMessage.Length = 0 Then
                    _status = CInt(EnumSPAFSubsidy.SPAFDocStatus.Deleted).ToString & "," & CInt(EnumSPAFSubsidy.SPAFDocStatus.Ditolak).ToString
                    Dim criteriaforChassis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "ChassisMaster.ID", MatchType.Exact, item.ChassisMaster.ID))
                    criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "Status", MatchType.NotInSet, _status))

                    If isSPAF Then
                        criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", CInt(EnumSPAFSubsidy.DocumentType.SPAF)))
                    Else
                        criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", CInt(EnumSPAFSubsidy.DocumentType.Subsidi)))
                    End If

                    _arrTmp = New SPAF.SPAFFacade(User).Retrieve(criteriaforChassis)

                    If _arrTmp.Count > 0 Then
                        MessageBox.Show("Data Sudah Ada") '---data sudah ada di database
                        Return
                    End If
                    _arrValid.Add(item)
                Else
                    _arrNotValid.Add(item)
                End If
            Next

            '--end modif
            If (New SPAF.SPAFFacade(User).InsertTransactionSPAFAndSubsidiFromTextFile(_arrValid) = 1) Then
                If _arrNotValid.Count <> _arrData.Count Then
                    MessageBox.Show(SR.UploadSucces(IIf(isSPAF, "SPAF", "Subsidi")))
                Else
                    MessageBox.Show("Tidak ada data yang disimpan")
                End If
                sHelper.SetSession("DataUpload", _arrNotValid)
                If _arrNotValid.Count > 0 Then
                    btnSimpan.Text = "Download"
                End If
                'start  :CR:send email after saving to DB:dna:20091216
                Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
                Dim ObjEmail As DNetMail = New DNetMail(smtp)
                Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
                Dim emailTo As String = KTB.DNet.Lib.WebConfig.GetValue("SPAFUploadEmailTo")
                Dim emailCC As String = KTB.DNet.Lib.WebConfig.GetValue("SPAFUploadEmailCC")
                Dim objUI As UserInfo = New UserInfo

                Dim _intOrgId As Integer = CType(User.Identity.Name.Substring(0, 6), Integer)
                Dim _strUserName As String = User.Identity.Name.Substring(6, User.Identity.Name.Length - 6)
                Dim _userInfoFacade As New KTB.DNet.BusinessFacade.UserManagement.UserInfoFacade(User)
                Dim crtParam As CriteriaComposite
                crtParam = New CriteriaComposite(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, _strUserName))
                crtParam = New CriteriaComposite(New Criteria(GetType(UserInfo), "OrganizationID", MatchType.Exact, _intOrgId))
                objUI = CType(_userInfoFacade.Retrieve(crtParam)(0), UserInfo)

                Dim strMessage As String = ""
                If Not (objUI Is Nothing) Then
                    emailCC &= IIf(emailCC.Trim = "", "", ";") & objUI.Email
                End If
                strMessage &= "SPAF Upload Report" & "<br/>"
                strMessage &= "Success" & ":" & FormatNumber(_arrValid.Count, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " data <br/>"
                strMessage &= "Failed " & ":" & FormatNumber(_arrNotValid.Count, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " data <br/>"
                'ObjEmail.sendMail(emailTo, "", emailFrom, "KTB-DNET", Mail.MailFormat.Html, contentEmail)

                ObjEmail.sendMail(emailTo, emailCC, emailFrom, "[MMKSI-DNet] - SPAF Upload Report", Mail.MailFormat.Html, strMessage)
                'End    :CR:send email after saving to DB:dna:20091216

                'change by ags open remarks Quote on 29/12/2009 to fullfill Rina's req
                dgUploadData.DataSource = _arrNotValid
                dgUploadData.DataBind()
                'End Change

                'delete temp data, saved in SPAFDoc_UploadHedaer and SPAFDoc_UploadDetail
                Dim objSDUH As SPAFDoc_UploadHeader = Nothing
                
                sHelper.SetSession("FrmUploadSPAF.UploadedFile", Me.fuUpload)
                If IsFileAlreadyUploaded(objSDUH) Then
                    Dim oSDUHFac As SPAFDoc_UploadHeaderFacade = New SPAFDoc_UploadHeaderFacade(User)
                    Dim oSDUDFac As SPAFDoc_UploadDetailFacade = New SPAFDoc_UploadDetailFacade(User)
                    For Each oSDUD As SPAFDoc_UploadDetail In objSDUH.SPAFDoc_UploadDetails
                        oSDUDFac.DeleteFromDB(oSDUD)
                    Next
                    oSDUHFac.DeleteFromDB(objSDUH)
                End If
            Else
                MessageBox.Show(SR.UploadFail(IIf(isSPAF, "SPAF", "Subsidi")))
            End If
        Else
            'Download()
            Me.btnSimpan.Enabled = False
            DownloadPage()
        End If
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateSPAFAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.SPAFSaveUpload_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SPAF - Upload SPAF")
        End If
    End Sub

    Private Sub InitiateSubsidiAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.SPAFSaveSubsidiUpload_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SUBSIDI - Upload Subsidi")
        End If
    End Sub
#End Region

    Private Sub dgUploadData_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgUploadData.SortCommand
        If IsNothing(sHelper.GetSession("DataUpload")) Then
            Return
        End If
        If dgUploadData.Enabled Then
            If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
                Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                    Case Sort.SortDirection.ASC
                        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                    Case Sort.SortDirection.DESC
                        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
                End Select
            Else
                ViewState("CurrentSortColumn") = e.SortExpression
                ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End If

            If Not IsNothing(sHelper.GetSession("DataUpload")) Then
                dgUploadData.DataSource = CommonFunction.SortArraylist(CType(sHelper.GetSession("DataUpload"), ArrayList), _
                    GetType(SPAFDoc), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
                dgUploadData.DataBind()

            Else
                Return
            End If
        End If
    End Sub

#Region "Check ChasisNumber"
    Private Function StrIfChasisNumExist(ByVal _arrTmp As ArrayList) As String
        Dim _strMessage As New StringBuilder
        'GetDealerCode
        Dim _spafDocTemp As New SPAFDoc
        _spafDocTemp = CType(_arrTmp(0), SPAFDoc)

        _strMessage.Append("Nomor rangka sudah disimpan oleh ")
        _strMessage.Append(_spafDocTemp.Dealer.DealerCode)
        _strMessage.Append("-")
        _strMessage.Append(_spafDocTemp.CreatedBy.Substring(6, _spafDocTemp.CreatedBy.Length - 6))
        _strMessage.Append(", tgl ")
        _strMessage.Append(_spafDocTemp.CreatedTime.Day)
        _strMessage.Append(" ")
        _strMessage.Append(_spafDocTemp.CreatedTime.Month)
        _strMessage.Append(" ")
        _strMessage.Append(_spafDocTemp.CreatedTime.Year)
        _strMessage.Append(" ")
        _strMessage.Append(_spafDocTemp.CreatedTime.Hour)
        _strMessage.Append(".")
        _strMessage.Append(_spafDocTemp.CreatedTime.Minute)
        _strMessage.Append(" WIB.")
        'Nomor rangka sudah disimpan oleh ABC-userX, tgl 22 Dec 2009 15.50 WIB.
        Return _strMessage.ToString()
    End Function

    'Private Sub SetMsgIfChasisNumExist(ByVal ChassisMasterID As Integer, ByRef _SPAFDoc As SPAFDoc)
    '    Dim _arrTmp As ArrayList = New ArrayList
    '    Dim _status As String = String.Empty

    '    _status = CInt(EnumSPAFSubsidy.SPAFDocStatus.Deleted).ToString & "," & CInt(EnumSPAFSubsidy.SPAFDocStatus.Ditolak).ToString
    '    Dim criteriaforChassis As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "ChassisMaster.ID", MatchType.Exact, ChassisMasterID))
    '    criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "Status", MatchType.NotInSet, _status))
    '    criteriaforChassis.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPAFDoc), "DocType", CInt(EnumSPAFSubsidy.DocumentType.SPAF)))

    '    _arrTmp = New SPAF.SPAFFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriaforChassis)

    '    If _arrTmp.Count > 0 Then
    '        _SPAFDoc.ErrorMessage = _SPAFDoc.ErrorMessage & StrIfChasisNumExist(_arrTmp)
    '    End If
    'End Sub
#End Region
    'Private Sub sortArray()
    '    Dim arlToSort As ArrayList = New ArrayList
    '    arlToSort = CType(Session("DataUpload"), ArrayList)
    '    arlToSort = CommonFunction.SortArraylist(arlToSort, GetType(SPAFDoc), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))

    '    dgUploadData.DataSource = arlToSort
    '    dgUploadData.DataBind()

    'End Sub

    Private Function IsFileAlreadyUploaded(ByRef pObjSDUH As SPAFDoc_UploadHeader) As Boolean
        Dim oSDUHFac As SPAFDoc_UploadHeaderFacade = New SPAFDoc_UploadHeaderFacade(User)
        Dim cSDUH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPAFDoc_UploadHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlSDUH As New ArrayList
        Dim finfo As FileInfo = New FileInfo(Me.fuUpload.PostedFile.FileName)
        Dim oDealer As Dealer = Me.sHelper.GetSession("DEALER")
        
        cSDUH.opAnd(New Criteria(GetType(SPAFDoc_UploadHeader), "Dealer.ID", MatchType.Exact, oDealer.ID))
        cSDUH.opAnd(New Criteria(GetType(SPAFDoc_UploadHeader), "Filename", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("SPAFInPrefix") & finfo.Name))
        arlSDUH = oSDUHFac.Retrieve(cSDUH)
        If arlSDUH.Count > 0 Then
            pObjSDUH = CType(arlSDUH(0), SPAFDoc_UploadHeader)
            Return True
        Else
            pObjSDUH = Nothing
            Return False
        End If
    End Function

End Class
