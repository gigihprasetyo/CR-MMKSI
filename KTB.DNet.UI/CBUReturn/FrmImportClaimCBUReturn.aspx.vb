#Region ".NET Base Class Namespace Imports"

Imports System.IO
Imports System.Text
Imports System.Linq
Imports System.Collections
Imports System.Collections.Generic

#End Region

#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper

Imports ICSharpCode.SharpZipLib.Core
Imports ICSharpCode.SharpZipLib.Zip
Imports KTB.DNet.BusinessFacade.PO

#End Region

Public Class FrmImportClaimCBUReturn
    Inherits System.Web.UI.Page
    Private ReadOnly varUpload As String = KTB.DNet.Lib.WebConfig.GetValue("CBUReturnDirectory")
    Private _sesshelper As SessionHelper = New SessionHelper
    Dim oDealer As Dealer

    Private m_bLihatPrivilege As Boolean = False
    Private m_bInputPrivilege As Boolean = False

    Private Sub CheckPrivilege()
        m_bLihatPrivilege = SecurityProvider.Authorize(Context.User, SR.CBUReturn_UploadClaim_View_Privilage)
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.CBUReturn_UploadClaim_Input_Privilage)

        If Not m_bLihatPrivilege And Not oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Klaim Pengembalian Kendaraan - Import Claim")
        End If
    End Sub

    Private Sub DisabledAllInput()
        DataFile.Disabled = True
        btnUpload.Enabled = False
        lnkDownload.Enabled = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsPostBack Then
        oDealer = CType(_sesshelper.GetSession("DEALER"), Dealer)
        CheckPrivilege()

        ViewState("currSortColumn") = "LastUpdateTIme"
        ViewState("currSortDirection") = Sort.SortDirection.DESC
        BindGrid(0)
        'End If


        If Not m_bInputPrivilege Then
            DisabledAllInput()
        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim mess As String = String.Empty
        If NotValidated(mess) Then
            MessageBox.Show(mess)
            Exit Sub
        End If

        'targetDir = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        'targetDir = Server.MapPath("~/DataTemp/")
        'ProcessData()
        ReadExcelData()
        BindGrid(0)
    End Sub

    Protected Function NotValidated(ByRef message As String) As Boolean

        Return message.Length
    End Function

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "CreatedBy", MatchType.[Partial], "000002"))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(ChassisMasterClaimHeader), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))
        Dim listSource As ArrayList = New ChassisMasterClaimHeaderFacade(User).Retrieve(crit, sortColl)
        If listSource.Count <> 0 Then
            CommonFunction.SortListControl(listSource, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(listSource, pageIndex, dgCBUList.PageSize)
            _sesshelper.SetSession("FrmImportClaimCBUReturn", PagedList)
            dgCBUList.CurrentPageIndex = pageIndex
            dgCBUList.DataSource = PagedList
            dgCBUList.VirtualItemCount = listSource.Count
            dgCBUList.DataBind()
        Else
            dgCBUList.DataSource = New ArrayList
            dgCBUList.VirtualItemCount = 0
            dgCBUList.CurrentPageIndex = 0
            dgCBUList.DataBind()
        End If
    End Sub

    Private Sub ReadExcelData()
        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)   '-- Source file name
            Dim DestFile As String = Server.MapPath("") & "\..\DataFile\" & SrcFile  '-- Destination file
            Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile  '-- Temporary file

            'Todo session
            Session.Add("DestFile", DestFile)  '-- Store Destination file path into session
            'Todo session
            Session.Add("TempFile", TempFile)  '-- Store Temporary file path into session

            '-- Impersonation to manipulate file in server
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Try
                If imp.Start() Then

                    '-- Copy data file from client to server temporary folder
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, TempFile)

                    imp.StopImpersonate()
                    imp = Nothing

                    '-- Declare & instantiate parser
                    Dim parser As IExcelParser = New UploadCBUReturnExcelParser()

                    '-- Parse data file and store result into arraylist
                    Dim _arlHash As ArrayList = CType(parser.ParseExcelNoTransaction(TempFile, "[UploadClaim$]", oDealer.ID), ArrayList)
                    Dim ArrDuplicate As New ArrayList

                    If CheckExcelDuplicate(_arlHash, ArrDuplicate) Then
                        If ArrDuplicate.Count > 0 Then
                            Dim mess As String = ""
                            For Each dup As String In ArrDuplicate
                                mess = mess & dup & "\n"
                            Next
                            MessageBox.Show(mess)
                            Exit Sub
                        End If
                    End If

                    If CheckDBDuplicate(_arlHash, ArrDuplicate) Then
                        If ArrDuplicate.Count > 0 Then
                            Dim mess As String = ""
                            For Each dup As String In ArrDuplicate
                                mess = mess & dup & "\n"
                            Next
                            MessageBox.Show(mess)
                            Exit Sub
                        End If
                    End If

                    Dim nRes As ArrayList = New ArrayList
                    Dim ErrMess As String = String.Empty
                    If _arlHash.Count > 0 Then
                        For Each objHash As Hashtable In _arlHash
                            If Not IsNothing(objHash.Item("ChassisNumber")) Then
                                If objHash.Item("ChassisNumber").ToString.Trim.Length = 0 Then
                                    Continue For
                                End If
                                Dim ch As ChassisMaster = New ChassisMasterFacade(User).Retrieve(objHash.Item("ChassisNumber").ToString())
                                If ch.ID = 0 Then
                                    MessageBox.Show("Chassis Master " & objHash.Item("ChassisNumber").ToString() & " tidak ditemukan")
                                    Exit Sub
                                End If

                                If ThisChassisDMSDealer(ch) Then
                                    InsertToDMSReminder(ch)
                                    Continue For
                                End If

                            Else
                                MessageBox.Show("Chassis Master Harus diisi")
                                Exit Sub
                            End If

                            If Not ValidateDataHash(objHash, ErrMess) Then
                                MessageBox.Show(ErrMess)
                                Exit Sub
                            End If
                            'From Parser
                            'hashColl.Add("ReporterIssue", objReader.GetString(0))
                            'hashColl.Add("ChassisMasterLogisticCompany", objReader.GetString(1))
                            'hashColl.Add("DateOccur", objReader.GetString(2))
                            'hashColl.Add("PlaceOccur", objReader.GetString(3))
                            'hashColl.Add("ChassisNumber", objReader.GetString(4))
                            'hashColl.Add("DONumber", objReader.GetString(5))
                            'hashColl.Add("ClaimType", objReader.GetString(6).Split("_")(0))
                            'hashColl.Add("ClaimPoint", objReader.GetString(7))

                            Dim objHeader As ChassisMasterClaimHeader = New ChassisMasterClaimHeader
                            'objHeader.ClaimNumber = GenerateCode(DateTime.Now.Year, DateTime.Now.Month)
                            objHeader.ReporterIssue = objHash.Item("ReporterIssue")
                            objHeader.PODestination = New PODestinationFacade(User).Retrieve(1)
                            objHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Konfirmasi, Short)
                            objHeader.ClaimDate = Date.Now
                            objHeader.Dealer = oDealer
                            If Not IsNothing(objHash.Item("ChassisMasterLogisticCompany")) Then
                                objHeader.ChassisMasterLogisticCompany = New ChassisMasterLogisticCompanyFacade(User).Retrieve(objHash.Item("ChassisMasterLogisticCompany").ToString())
                            End If
                            Try
                                objHeader.DateOccur = CDate(objHash.Item("DateOccur").ToString)
                            Catch ex As Exception
                                objHeader.DateOccur = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                            End Try
                            objHeader.PlaceOccur = objHash.Item("PlaceOccur")
                            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crit.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, objHash.Item("ChassisNumber")))
                            If Not IsNothing(objHash.Item("DONumber")) Then
                                crit.opAnd(New Criteria(GetType(ChassisMaster), "DONumber", MatchType.Exact, objHash.Item("DONumber")))
                            End If
                            Dim arlCM As ArrayList = New ChassisMasterFacade(User).Retrieve(crit)
                            If arlCM.Count > 0 Then
                                objHeader.ChassisMaster = CType(arlCM(0), ChassisMaster)
                            End If
                            objHeader.ResponClaim = 0
                            objHeader.Nominal = 0
                            objHeader.EngineNumberReplacement = ""
                            objHeader.TransferDate = CDate("1753-01-01")
                            objHeader.CompletionDate = CDate("1753-01-01")
                            objHeader.RepairEstimationDate = CDate("1753-01-01")

                            Dim objDetail As ChassisMasterClaimDetail = New ChassisMasterClaimDetail
                            If Not IsNothing(objHash.Item("ClaimType")) Then
                                objDetail.ClaimType = objHash.Item("ClaimType").ToString().Split("_")(0)
                            End If
                            objDetail.ClaimPoint = objHash.Item("ClaimPoint")
                            Dim nResult As Integer = New ChassisMasterClaimDetailFacade(User).InsertByUpload(objHeader, objDetail)
                        Next
                    Else
                        MessageBox.Show("Tidak ada data")
                        Exit Sub
                    End If

                    If ErrMess.Length > 0 Then
                        MessageBox.Show(ErrMess)
                        Exit Sub
                    End If

                    If nRes.Count = 0 Then
                        MessageBox.Show(SR.SaveSuccess)
                    Else
                        MessageBox.Show(SR.SaveFail)
                    End If
                End If

            Catch Exc As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If
    End Sub

    Private Function ThisChassisDMSDealer(ByVal objCM As ChassisMaster) As Boolean
        Dim objDealerSystem As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objCM.Dealer.DealerCode)
        If objDealerSystem.GoLiveDate.Year = 1753 Then
            Return False
        End If
        Return True
    End Function

    Private Sub InsertToDMSReminder(ByVal objCM As ChassisMaster)
        Dim objCHDMS As New ChassisMasterClaimDMSReminder
        objCHDMS.ChassisMaster = objCM
        objCHDMS.IsSent = 0
        Dim result = New ChassisMasterClaimDMSReminderFacade(User).Insert(objCHDMS)
    End Sub

    Private Function ValidateDataHash(ByVal objHash As Hashtable, ByRef errMes As String) As Boolean
        'If Not IsNothing(objHash.Item("DateOccur")) Then
        '    If Not objHash.Item("DateOccur").ToString().Contains("-") Then
        '        errMes = "Format Tanggal kejadian salah"
        '    Else
        '        If Not objHash.Item("DateOccur").ToString().Trim.Split("-")(0).Length = 4 Then
        '            errMes = "Format Tanggal kejadian salah"
        '            Return False
        '        ElseIf Not objHash.Item("DateOccur").ToString().Trim.Split("-")(1).Length = 2 Then
        '            errMes = "Format Tanggal kejadian salah"
        '            Return False
        '        ElseIf Not objHash.Item("DateOccur").ToString().Trim.Split("-")(2).Length = 2 Then
        '            errMes = "Format Tanggal kejadian salah"
        '            Return False
        '        End If
        '    End If
        'End If
        Return True
    End Function

    Private Function CheckExcelDuplicate(objReader As ArrayList, ByRef DuplicateRow As ArrayList) As Boolean
        Dim listData As New List(Of String)()
        For Each _hash As Hashtable In objReader
            listData.Add(_hash.Item("ChassisNumber").ToString)
        Next
        Dim CH As New ArrayList
        Dim myList As String() = listData.ToArray
        If myList.Distinct().Count() <> myList.Count() Then
            myList.Distinct().ToList().ForEach(Sub(digit)
                                                   If myList.Count(Function(s) s = digit) > 1 Then
                                                       CH.Add("Excel " & myList.Count(Function(s) s = digit) & " Duplicate pada Chassis Number " & digit)
                                                   End If
                                               End Sub)
            DuplicateRow = CH
            Return True
        End If
        Return False
    End Function

    Private Function CheckDBDuplicate(objReader As ArrayList, ByRef DuplicateRow As ArrayList) As Boolean
        Dim listData As New List(Of String)()
        Dim arlCLDuplicate As New ArrayList
        For Each _hash As Hashtable In objReader
            'objReader.GetString(4)
            Dim claimNunmber As String = String.Empty
            If IsDuplicateDataClaim(_hash.Item("ChassisNumber").ToString, claimNunmber) Then
                arlCLDuplicate.Add("Data Chassis Number " & _hash.Item("ChassisNumber").ToString & " duplicate dengan Claim Number " & claimNunmber)
            End If
        Next
        DuplicateRow = arlCLDuplicate
        If arlCLDuplicate.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    Private Function IsDuplicateDataClaim(ByVal chassisNumber As String, ByRef CLNumber As String) As Boolean
        Dim _arrData As ArrayList = New ArrayList

        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber))
        crit.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "StatusID", MatchType.NotInSet, String.Format("{0}", CInt(EnumCBUReturn.StatusClaim.Tolak))))

        _arrData = New ChassisMasterClaimHeaderFacade(User).Retrieve(crit)
        If _arrData.Count > 0 Then
            CLNumber = CType(_arrData(0), ChassisMasterClaimHeader).ClaimNumber
            Return True
        End If
        Return False
    End Function

    Private Function GenerateCode(ByVal PeriodYear As String, ByVal PeriodMonth As String) As String
        Dim _ret = ""

        Dim noBuilder As New StringBuilder
        Dim RunningNumber As Integer = 0
        noBuilder.Append(String.Format("CLM/{0}/{1}", PeriodMonth, PeriodYear.ToString()))

        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimHeader), "ClaimNumber", MatchType.StartsWith, noBuilder.ToString))
        Dim arrl As ArrayList = New ChassisMasterClaimHeaderFacade(User).Retrieve(crit)

        If arrl.Count > 0 Then
            Dim objCMClaimH As ChassisMasterClaimHeader = CommonFunction.SortListControl(arrl, "ClaimNumber", Sort.SortDirection.DESC)(0)
            Dim repNumber As String = objCMClaimH.ClaimNumber
            RunningNumber = (CInt(Right(repNumber, 3)) + 1)
            repNumber = RunningNumber.ToString("d3")
            _ret = String.Format("{0}/{1}", noBuilder.ToString, repNumber)
        Else
            _ret = String.Format("{0}/{1}", noBuilder.ToString, CInt(1).ToString("d3"))
        End If

        Return _ret
    End Function

#Region "ON HOLD"
    'Private Sub ProcessData()
    '    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
    '    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
    '    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
    '    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
    '    'Dim targetDir = KTB.DNet.Lib.WebConfig.GetValue("SAN")
    '    Dim targetDir = Server.MapPath("~/DataTemp/")
    '    Dim dummyName = Guid.NewGuid.ToString().Substring(0, 5)
    '    If (Not Me.DataFile.PostedFile Is Nothing) And (Me.DataFile.PostedFile.ContentLength > 0) Then
    '        Try
    '            If Me.DataFile.PostedFile.ContentLength > 10240000 Then
    '                MessageBox.Show("Hanya menerima file format Zip (Ukuran maksimum 10Mb)")
    '                Return
    '            End If
    '            Dim ext As String = System.IO.Path.GetExtension(Me.DataFile.PostedFile.FileName)
    '            If Not ext.ToUpper() = ".ZIP" Then
    '                MessageBox.Show("Hanya menerima file format Zip (Ukuran maksimum 10Mb)")
    '                Return
    '            End If

    '            If imp.Start() Then
    '                Dim NewFileLocation As String = targetDir & varUpload ' & objPresentation.UniqueName & ".zip"

    '                If Not IO.Directory.Exists(NewFileLocation) Then
    '                    IO.Directory.CreateDirectory(Path.GetDirectoryName(NewFileLocation))
    '                End If

    '                NewFileLocation = NewFileLocation & dummyName & ".zip"
    '                Dim objUpload As New UploadToWebServer
    '                objUpload.Upload(Me.DataFile.PostedFile.InputStream, NewFileLocation)

    '                If IO.File.Exists(NewFileLocation) Then
    '                    Dim _pathSlider As String = targetDir & varUpload
    '                    _pathSlider = _pathSlider & dummyName
    '                    If Not IO.Directory.Exists(_pathSlider) Then
    '                        Directory.CreateDirectory(_pathSlider)
    '                    End If

    '                    Try
    '                        ExtractZipFile(NewFileLocation, "", _pathSlider)
    '                        ReadExtractedZipFile(_pathSlider)
    '                    Catch ex As Exception
    '                        imp.StopImpersonate()
    '                        imp = Nothing
    '                        MessageBox.Show(SR.UploadFail("Gagal Extract"))
    '                        Return
    '                    End Try

    '                    Dim CFiles As Integer = New DirectoryInfo(_pathSlider).GetFiles("*.PNG").Length
    '                    If CFiles = 0 Then
    '                        imp.StopImpersonate()
    '                        imp = Nothing
    '                        MessageBox.Show(SR.UploadFail("Gambar PNG Tidak Ada"))
    '                        Return
    '                    End If
    '                End If

    '                imp.StopImpersonate()
    '                imp = Nothing

    '                Dim returnValID As Integer = 1
    '                If returnValID = -1 Then
    '                    MessageBox.Show(SR.UploadFail(Me.DataFile.Value.Replace("\", "\\")))
    '                Else
    '                    MessageBox.Show(SR.UploadSucces(Me.DataFile.Value.Replace("\", "\\")))
    '                End If
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show(SR.UploadFail(Me.DataFile.Value.Replace("\", "\\")))
    '        End Try
    '    Else
    '        MessageBox.Show(SR.FileNotSelected)
    '    End If
    'End Sub

    'Public Sub ExtractZipFile(archiveFilenameIn As String, password As String, outFolder As String)
    '    Dim zf As ZipFile = Nothing
    '    Try
    '        Dim fs As FileStream = File.OpenRead(archiveFilenameIn)
    '        zf = New ZipFile(fs)
    '        If Not [String].IsNullorEmpty(password) Then    ' AES encrypted entries are handled automatically
    '            zf.Password = password
    '        End If
    '        For Each zipEntry As ZipEntry In zf
    '            If Not zipEntry.IsFile Then     ' Ignore directories
    '                Continue For
    '            End If
    '            Dim entryFileName As [String] = Path.GetFileName(zipEntry.Name)
    '            ' to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
    '            ' Optionally match entrynames against a selection list here to skip as desired.
    '            ' The unpacked length is available in the zipEntry.Size property.

    '            Dim buffer As Byte() = New Byte(4095) {}    ' 4K is optimum
    '            Dim zipStream As Stream = zf.GetInputStream(zipEntry)

    '            ' Manipulate the output filename here as desired.
    '            Dim fullZipToPath As [String] = Path.Combine(outFolder, entryFileName)
    '            Dim directoryName As String = Path.GetDirectoryName(fullZipToPath)
    '            If directoryName.Length > 0 Then
    '                Directory.CreateDirectory(directoryName)
    '            End If

    '            ' Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
    '            ' of the file, but does not waste memory.
    '            ' The "Using" will close the stream even if an exception occurs.
    '            Using streamWriter As FileStream = File.Create(fullZipToPath)
    '                StreamUtils.Copy(zipStream, streamWriter, buffer)
    '            End Using
    '        Next
    '    Finally
    '        If zf IsNot Nothing Then
    '            zf.IsStreamOwner = True     ' Makes close also shut the underlying stream
    '            ' Ensure we release resources
    '            zf.Close()
    '            File.Delete(archiveFilenameIn)
    '        End If
    '    End Try
    'End Sub

    'Public Sub ReadExtractedZipFile(ByVal _pathSlider As String)
    '    If Not IO.Directory.Exists(_pathSlider) Then
    '        Directory.CreateDirectory(_pathSlider)
    '    End If

    '    Dim files As String() = Directory.GetFiles(_pathSlider)
    '    Dim debug = ""
    'End Sub
#End Region

    Protected Sub dgCBUList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgCBUList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As ChassisMasterClaimHeader = CType(e.Item.DataItem, ChassisMasterClaimHeader)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblTglClaim As Label = CType(e.Item.FindControl("lblTglClaim"), Label)
            Dim lblNoClaim As Label = CType(e.Item.FindControl("lblNoClaim"), Label)
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Dim lblStatusClaim As Label = CType(e.Item.FindControl("lblStatusClaim"), Label)
            Dim lblResponClaim As Label = CType(e.Item.FindControl("lblResponClaim"), Label)
            Dim lblNoChassis As Label = CType(e.Item.FindControl("lblNoChassis"), Label)
            Dim lblNoDO As Label = CType(e.Item.FindControl("lblNoDO"), Label)
            Dim lblFileLampiran As Label = CType(e.Item.FindControl("lblFileLampiran"), Label)
            Dim lnkUploadLampiran As LinkButton = CType(e.Item.FindControl("lnkUploadLampiran"), LinkButton)


            lblNo.Text = (dgCBUList.PageSize * dgCBUList.CurrentPageIndex) + e.Item.ItemIndex + 1
            lblTglClaim.Text = oData.ClaimDate
            lblNoClaim.Text = oData.ClaimNumber
            If Not IsNothing(oData.ChassisMaster) Then
                lblNoChassis.Text = oData.ChassisMaster.ChassisNumber
                lblNoDO.Text = oData.ChassisMaster.DONumber
            Else
                lblNoDO.Text = ""
                lblNoChassis.Text = ""
            End If
            If Not IsNothing(oData.Dealer) Then
                lblDealer.Text = oData.Dealer.SearchTerm1
            Else
                lblDealer.Text = ""
            End If
            Dim stdStatusClaim As ArrayList = CType(New StandardCodeFacade(User).RetrieveByValueId(oData.StatusID, "ChassisMasterClaim.StatusClaim"), ArrayList)
            If stdStatusClaim.Count > 0 Then
                lblStatusClaim.Text = CType(stdStatusClaim(0), StandardCode).ValueDesc
            End If
            Dim stdResponClaim As ArrayList = CType(New StandardCodeFacade(User).RetrieveByValueId(oData.ResponClaim, "ChassisMasterClaim.RespondClaim"), ArrayList)
            If stdResponClaim.Count > 0 Then
                lblResponClaim.Text = CType(stdResponClaim(0), StandardCode).ValueDesc
            End If

            Dim critDoc As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DocumentUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critDoc.opAnd(New Criteria(GetType(DocumentUpload), "Type", MatchType.Exact, "1"))
            critDoc.opAnd(New Criteria(GetType(DocumentUpload), "DocRegNumber", MatchType.Exact, oData.ClaimNumber))
            Dim arlDoc As ArrayList = New DocumentUploadFacade(User).Retrieve(critDoc)
            If arlDoc.Count > 0 Then
                lblFileLampiran.Text = "Sudah ada"
            Else
                lblFileLampiran.Text = "Belum ada"
            End If
            If Not m_bInputPrivilege Then
                lnkUploadLampiran.Visible = False
            End If
            lnkUploadLampiran.Attributes("onclick") = "ShowPPUploadDoc(" & oData.ID & ")"
        End If
    End Sub

    Protected Sub dgCBUList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgCBUList.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                Response.Redirect("FrmInputClaimCBUReturn.aspx?ID=" & e.Item.Cells(0).Text & "&mode=Edit&FromUpload=true")
        End Select
    End Sub

    Protected Sub lnkDownload_Click(sender As Object, e As EventArgs) Handles lnkDownload.Click
        Dim strName As String = "TemplateUploadCBURetur.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Template\" & strName)
    End Sub

    Protected Sub dgCBUList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgCBUList.PageIndexChanged
        dgCBUList.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub
End Class