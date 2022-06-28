#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Drawing.Color
Imports System.Linq
Imports System.Text
#End Region

Public Class FrmEntryPMViaText
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblFileName As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dfChassis As System.Web.UI.HtmlControls.HtmlInputFile

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents dgPMHeaderUpload As System.Web.UI.WebControls.DataGrid
    Private bIsError As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.PMUploadView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PERIODICAL MAINTENANCE - Upload Data PM")
        End If
    End Sub

    Dim bCekPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.PMUploadSave_Privilege)
    Private Sub CekBtnPriv()
        btnSave.Enabled = bCekPriv
    End Sub
#End Region

#Region "Custom Method"

    Protected Sub DownloadSampleButton_Click(sender As Object, e As EventArgs)
        Dim sb As StringBuilder = New StringBuilder
        Dim sample1 As String = "Kode dealer;chassismaster;tanggal PM;Jarak Tempuh;nomor mesin;tipe visit;jenis pm;kode cabang dealer;WO number"
        Dim sample2 As String = "100XXX;MK2NCWHXNJXXXX;04022012;9392;4X91DXXX;WI;3;100XXX;WO-00001"
        Dim sample3 As String = "100XXX;MK2NCXHANJXXXXX8;04022012;9392;4A9XDG6XXX;WI;3;;"
        sb.Append(sample1 & vbCrLf & sample2 & vbCrLf & sample3)
        sb.Append("" & vbCrLf)
        Dim text As String = sb.ToString
        Response.Clear()
        Response.ClearHeaders()
        Response.AddHeader("Content-Length", text.Length.ToString)
        Response.ContentType = "text/plain"
        Response.AppendHeader("content-disposition", "attachment;filename=""PMUploadSample.txt""")
        Response.Write(text)
        Response.End()
    End Sub

    Private Sub BindUpload()
        Dim ObjDealer As Dealer = CType(Session.Item("DEALER"), Dealer)

        dgPMHeaderUpload.CurrentPageIndex = 0
        ViewState.Add("dtgPageIndex", 0)
        Dim objUpload As New UploadToWebServer
        If (Not dfChassis.PostedFile Is Nothing) And (dfChassis.PostedFile.ContentLength > 0) And _
        ((dfChassis.PostedFile.ContentType.ToLower() = "text/plain") Or (dfChassis.PostedFile.ContentType.ToLower() = "text/csv") _
        Or (dfChassis.PostedFile.ContentType.ToLower() = "application/octet-stream") Or (dfChassis.PostedFile.ContentType.ToLower() = "application/vnd.ms-excel")) Then
            Dim Extension As String = Path.GetExtension(dfChassis.PostedFile.FileName)

            If Extension.ToUpper = ".TXT" Then

                Dim SrcFile As String = Path.GetFileName(dfChassis.PostedFile.FileName)
                Dim DestFile As String = Server.MapPath("..") & "\..\DataTemp\" & SrcFile
                Try

                    ''---------- Pake ini Error -----------
                    'dfChassis.PostedFile.SaveAs(DestFile) '-- Copy source file to destination file

                    ''---------- Pake UploadToWebServer saja -----------

                    objUpload.Upload(dfChassis.PostedFile.InputStream, DestFile)

                    Dim parser As IParser = New PMParser
                    'remarks by anh 20120402
                    'Dim arList As ArrayList = CType(parser.ParseNoTransaction(DestFile, "User"), ArrayList)
                    'add by anh 20120402 ' 
                    Dim arList As ArrayList = CType(parser.ParseNoTransaction(DestFile, ObjDealer.DealerCode), ArrayList)

                    'periksa tidak boleh ada chassis# dan kind# yang sama di tabel freeservice
                    Dim NewList As ArrayList = New ArrayList
                    'check status MSP

                    'Try
                    '    CheckStatusMSP(arList)
                    'Catch ex As Exception
                    '    Dim cumi = 0
                    'End Try

                    NewList = CheckExistingInPMHeader(arList)
                    AddUserCreator(NewList)
                    AddReleaseDate(NewList)
                    AddEntryType(NewList)
                    'periksa kalo ada record rangkap (chassis# dan kind# yang sama) di arraylist
                    CheckDoubleRows(NewList)

                    'periksa kalau ada Chassis yang sama dan jarak tempuh yang sama
                    CheckSameChassisAndKM(NewList)

                    'periksa kalau ada chassis number yang sama dan tanggal pm yang sama
                    CheckSameChassisAndPM(NewList)

                    'Added by Ikhsan, 20 June 2008
                    'melakukan validasi terhadap jenis PMKind yang telah terdaftar di DB dengan standKM yang dimasukkan
                    '---------------------------------
                    CheckPMKindAvailability(NewList)
                    '---------------------------------

                    CheckChassisCategory(NewList)

                    _sessHelper.SetSession("sessPMHeader", NewList)
                    dgPMHeaderUpload.DataSource = NewList
                    dgPMHeaderUpload.DataBind()

                Catch Exc As Exception
                    MessageBox.Show(SR.UploadFail(SrcFile))
                Finally
                    objUpload.DeleteFile(DestFile)
                End Try
            Else
                MessageBox.Show("Jenis file tidak sesuai")
            End If
        Else
            MessageBox.Show("Pilih file yang akan di-upload.")
        End If

    End Sub

    Private Function CheckChassisCategory(ByVal arList As ArrayList) As ArrayList
        Dim TmpList As ArrayList = New ArrayList
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        For Each objPMHeader As PMHeader In arList
            If Not IsNothing(objPMHeader.ChassisMaster) Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, objPMHeader.ChassisMaster.ChassisNumber))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, objPMHeader.ChassisMaster.EngineNumber))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
                Dim ChassisMasterCollection As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
                If ChassisMasterCollection.Count = 0 Then
                    objPMHeader.ErrorMessage = "Chassis tidak terdaftar di " + companyCode + " atau chassis tidak sesuai dengan no mesin"
                    TmpList.Add(objPMHeader)
                Else
                    TmpList.Add(objPMHeader)
                End If
            Else
                TmpList.Add(objPMHeader)
            End If
        Next
        Return TmpList
    End Function

    Private Sub AddUserCreator(ByRef arrL As ArrayList)
        For Each objPMHeader As PMHeader In arrL
            objPMHeader.CreatedBy = CType(User.Identity.Name, String)
        Next
    End Sub
    Private Sub AddReleaseDate(ByRef arrL As ArrayList)
        Dim value As DateTime
        value = DateTime.Now
        For Each objPMHeader As PMHeader In arrL
            objPMHeader.ReleaseDate = value
        Next
    End Sub
    Private Sub AddEntryType(ByRef arrL As ArrayList)
        For Each objPMHeader As PMHeader In arrL
            objPMHeader.EntryType = "Upload"
        Next
    End Sub
    Private Sub CheckPMKindAvailability(ByRef NewList As ArrayList)
        Dim objPMKind As PMKindFacade = New PMKindFacade(User)
        For Each objPMHeader As PMHeader In NewList
            If objPMKind.IsPMKindFound(CInt(objPMHeader.StandKM)) = False Then
                If objPMHeader.ErrorMessage = "" Then
                    objPMHeader.ErrorMessage = "Range jarak tempuh tidak valid"
                Else
                    objPMHeader.ErrorMessage = objPMHeader.ErrorMessage + ";<br> Range jarak tempuh tidak valid"
                End If
                'objPMHeader.ErrorMessage = "Range jarak tempuh tidak valid"
                'MessageBox.Show("Range jarak tempuh tidak valid")
                'Return
            End If
        Next

    End Sub


    Private Sub CheckDoubleRows(ByRef NewList As ArrayList)
        Dim nIndex As Integer
        Dim nIterate As Integer = 1
        For Each objPMHeader As PMHeader In NewList
            If Not IsNothing(objPMHeader.ChassisMaster) And Not IsNothing(objPMHeader.Dealer) And Not IsNothing(objPMHeader.ServiceDate) Then
                For nIndex = nIterate To NewList.Count - 1
                    Dim objPMHeader2 As PMHeader
                    objPMHeader2 = NewList(nIndex)
                    If Not IsNothing(objPMHeader2.ChassisMaster) And Not IsNothing(objPMHeader2.Dealer) And Not IsNothing(objPMHeader2.ServiceDate) Then
                        Dim sChassisNumber2 = objPMHeader2.ChassisMaster.ChassisNumber
                        Dim sChassisNumber1 = objPMHeader.ChassisMaster.ChassisNumber

                        Dim sKindCode1 = objPMHeader.PMKindCode
                        Dim sKindCode2 = objPMHeader2.PMKindCode

                        Dim sServiceDate1 = objPMHeader.ServiceDate
                        Dim sServiceDate2 = objPMHeader2.ServiceDate

                        If sChassisNumber1 = sChassisNumber2 And (sKindCode1 = sKindCode2 OrElse sServiceDate1 = sServiceDate2) Then
                            ' If sChassisNumber1 = sChassisNumber2 And sKindCode1 = sKindCode2 And sServiceDate1 = sServiceDate2 Then
                            If objPMHeader2.ErrorMessage = "" Then
                                objPMHeader2.ErrorMessage = "Data PM Ganda dg Record " + CType(nIterate, String)
                            Else
                                objPMHeader2.ErrorMessage = objPMHeader2.ErrorMessage + ";<br> Data PM Ganda dg Record " + CType(nIterate, String)
                            End If
                        End If
                    End If
                Next
            End If
            nIterate = nIterate + 1
        Next
    End Sub

    'add by ery
    Private Sub CheckSameChassisAndKM(ByVal NewList As ArrayList)
        Dim nIndex As Integer
        Dim nIterate As Integer = 1
        For Each objPMHeader As PMHeader In NewList
            If Not IsNothing(objPMHeader.ChassisMaster) And Not IsNothing(objPMHeader.Dealer) Then
                For nIndex = nIterate To NewList.Count - 1
                    Dim objPMHeader2 As PMHeader
                    objPMHeader2 = NewList(nIndex)
                    If Not IsNothing(objPMHeader2.ChassisMaster) And Not IsNothing(objPMHeader2.Dealer) Then
                        Dim sChassisNumber2 = objPMHeader2.ChassisMaster.ChassisNumber
                        Dim sChassisNumber1 = objPMHeader.ChassisMaster.ChassisNumber

                        Dim sKM1 = objPMHeader.StandKM
                        Dim sKM2 = objPMHeader2.StandKM

                        If sChassisNumber1 = sChassisNumber2 And sKM1 = sKM2 Then
                            If objPMHeader2.ErrorMessage = "" Then
                                objPMHeader2.ErrorMessage = "Data Chassis Number dan Stand KM Ganda dg Record " + CType(nIterate, String)
                            Else
                                objPMHeader2.ErrorMessage = objPMHeader2.ErrorMessage + ";<br> Data Chassis Number dan Stand KM dg Record " + CType(nIterate, String)
                            End If
                        End If
                    End If
                Next
            End If
            nIterate = nIterate + 1
        Next
    End Sub

    'add by ery
    Private Sub CheckSameChassisAndPM(ByVal NewList As ArrayList)
        Dim nIndex As Integer
        Dim nIterate As Integer = 1
        For Each objPMHeader As PMHeader In NewList
            If Not IsNothing(objPMHeader.ChassisMaster) And Not IsNothing(objPMHeader.Dealer) Then
                For nIndex = nIterate To NewList.Count - 1
                    Dim objPMHeader2 As PMHeader
                    objPMHeader2 = NewList(nIndex)
                    If Not IsNothing(objPMHeader2.ChassisMaster) And Not IsNothing(objPMHeader2.Dealer) Then
                        Dim sChassisNumber2 = objPMHeader2.ChassisMaster.ChassisNumber
                        Dim sChassisNumber1 = objPMHeader.ChassisMaster.ChassisNumber

                        Dim sPM1 = objPMHeader.ServiceDate
                        Dim spM2 = objPMHeader2.ServiceDate

                        If sChassisNumber1 = sChassisNumber2 And sPM1 = spM2 Then
                            If objPMHeader2.ErrorMessage = "" Then
                                objPMHeader2.ErrorMessage = "Data Chassis Number dan Tanggal PM Ganda dg Record " + CType(nIterate, String)
                            Else
                                objPMHeader2.ErrorMessage = objPMHeader2.ErrorMessage + ";<br> Data Chassis Number dan Tanggal PM Ganda dg Record " + CType(nIterate, String)
                            End If
                        End If
                    End If
                Next
            End If
            nIterate = nIterate + 1
        Next
    End Sub

    Private Sub CheckStatusMSP(ByVal NewList As ArrayList)
        If NewList.Count > 0 Then
            Dim _MSPHelper As New MSPHelper
            For i As Integer = 0 To NewList.Count - 1
                Dim _objPMHeader As PMHeader = CType(NewList(i), PMHeader)
                If Not IsNothing(_objPMHeader.ErrorMessage) AndAlso _objPMHeader.ErrorMessage.ToString() <> "" Then
                    Continue For
                End If
                'jika PMKind IsNothing maka diset sebagain PM biasa
                If IsNothing(_objPMHeader.PMKind) Then
                    _objPMHeader.PMKind = New PMKind(ID:=_objPMHeader.PMKindID)

                    If Not IsNothing(_objPMHeader.ChassisMaster) And Not IsNothing(_objPMHeader.Dealer) Then
                        Dim _newObjPMHeader As PMHeader
                        Dim _objPMKind As PMKind = New PMKind(ID:=_objPMHeader.PMKindID)
                        Dim str As String = String.Empty
                        str = _MSPHelper.CheckStatusMSPIFPMKIND0(_objPMHeader.ChassisMaster.ChassisNumber, _objPMHeader.PMKind.ID, _newObjPMHeader, _objPMHeader.StandKM, _objPMHeader.ServiceDate)




                        If str <> String.Empty And str <> "PM" Then
                            If _objPMHeader.ErrorMessage = String.Empty Then
                                _objPMHeader.ErrorMessage = str
                            Else
                                _objPMHeader.ErrorMessage += ";<br>" & str
                            End If
                        End If
                    End If


                Else
                    If Not IsNothing(_objPMHeader.ChassisMaster) And Not IsNothing(_objPMHeader.Dealer) Then
                        Dim _newObjPMHeader As PMHeader
                        Dim _objPMKind As PMKind = New PMKindFacade(User).Retrieve(_objPMHeader.PMKindCode)
                        Dim str As String = String.Empty
                        str = _MSPHelper.CheckStatusMSP(_objPMHeader.ChassisMaster.ChassisNumber, _objPMHeader.PMKind.ID, _newObjPMHeader, _objPMHeader.StandKM, _objPMHeader.ServiceDate)
                        If Not IsNothing(_newObjPMHeader) Then
                            _objPMHeader.MSPRegistrationHistoryID = _newObjPMHeader.MSPRegistrationHistoryID
                            _objPMHeader.Remarks = _newObjPMHeader.Remarks
                            _objPMHeader.IsValidMSP = _newObjPMHeader.IsValidMSP
                        End If

                        If (str = "PM") Then
                            _objPMHeader.PMKind = New PMKind(ID:=_objPMHeader.PMKindID)
                        End If

                        If str <> String.Empty And str <> "PM" Then
                            If _objPMHeader.ErrorMessage = String.Empty Then
                                _objPMHeader.ErrorMessage = str
                            Else
                                _objPMHeader.ErrorMessage += ";<br>" & str
                            End If
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    'Private Function CheckExistingInPMHeader(ByVal arList As ArrayList) As ArrayList
    '    Dim TmpList As ArrayList = New ArrayList
    '    For Each objPMHeader As PMHeader In arList
    '        If Not IsNothing(objPMHeader.ChassisMaster) And Not IsNothing(objPMHeader.Dealer) Then
    '            Dim StrFunction As String = String.Format("(SELECT ID FROM dbo.fn_PmHeaderChecking('{0}','{1}',{2}))", objPMHeader.ChassisMaster.ChassisNumber, objPMHeader.PMKindCode, 0)

    '            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.ID", MatchType.Exact, objPMHeader.ChassisMaster.ID))
    '            ' criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "Dealer.ID", MatchType.Exact, objPMHeader.Dealer.ID))
    '            'criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ServiceDate", MatchType.Exact, objPMHeader.ServiceDate))

    '            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ID", MatchType.InSet, StrFunction))
    '            Dim PMHeaderCollection As ArrayList = New PMHeaderFacade(User).Retrieve(criterias2)



    '            If PMHeaderCollection.Count > 0 Then
    '                If objPMHeader.ErrorMessage = "" Then
    '                    objPMHeader.ErrorMessage = "Data PM Sudah Terdaftar"
    '                Else
    '                    objPMHeader.ErrorMessage = objPMHeader.ErrorMessage + ";<br> Data PM Sudah Terdaftar"
    '                End If
    '                TmpList.Add(objPMHeader)
    '            Else
    '                TmpList.Add(objPMHeader)
    '            End If

    '        Else
    '            TmpList.Add(objPMHeader)
    '        End If
    '    Next
    '    Return TmpList
    'End Function

    Private Sub BinddgPMHeaderUpload(ByVal pageIndeks As Integer)
        Dim totalRow As Integer = 0
        dgPMHeaderUpload.DataSource = CType(_sessHelper.GetSession("sessPMHeader"), ArrayList)
        dgPMHeaderUpload.VirtualItemCount = totalRow
        dgPMHeaderUpload.DataBind()
    End Sub

    Private Function IsExistCode(ByVal ChassisID As Integer, ByVal DealerID As Integer) As Boolean
        Dim objPMHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
        'Periksa agar tidak ada key ganda 
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.ID", MatchType.Exact, ChassisID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "Dealer.ID", MatchType.Exact, DealerID))
        Dim TestExist As ArrayList = objPMHeaderFacade.Retrieve(criterias)
        If TestExist.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'ActivateUserPrivilege()
        InitiateAuthorization()
        If Not IsPostBack Then
            If Not IsNothing(Session("DEALER")) Then
                Dim ObjDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
                ''CR Tutup Menu
                '' by ali
                '' 2014 - 09 -30
                If (DateTime.Now >= New DateTime(2014, 10, 1) AndAlso DateTime.Now <= New DateTime(2014, 10, 11).AddMinutes(-1) AndAlso ObjDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    Dim MSgClose As String = IIf(Not IsNothing(KTB.DNet.Lib.WebConfig.GetValue("CloseMessage")), KTB.DNet.Lib.WebConfig.GetValue("CloseMessage"), "Module ini sedang di tutup, sampai dengan 10 Oktober 2014")
                    Server.Transfer("../ClossingMessage.htm")
                End If
                ''END CR Tutup Menu

                lblDealerCode.Text = ObjDealer.DealerCode + " / " + ObjDealer.SearchTerm1
                lblDealerName.Text = ObjDealer.DealerName
                _sessHelper.SetSession("sessDealer", ObjDealer)
                _sessHelper.SetSession("sessDealerLogin", ObjDealer.DealerCode)
                _sessHelper.SetSession("sessError", False)
            Else
                'Response.Redirect("../SessionExpired.htm")
            End If
        End If
    End Sub
    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.FreeServiceUploadView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FREE SERVICE - Upload Data Free Service")
        End If

        'FreeServiceUploadSave_Privilege  
        btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.FreeServiceUploadSave_Privilege)
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        bIsError = False
        BindUpload()
        bIsError = CType(_sessHelper.GetSession("sessError"), Boolean)

        If Not bIsError And Path.GetFileName(dfChassis.PostedFile.FileName).ToString.Trim <> String.Empty Then
            If Not _sessHelper.GetSession("sessPMHeader") Is Nothing Then
                btnSave.Enabled = bCekPriv
            Else
                btnSave.Enabled = False
            End If
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Function CheckExistingInPMHeader(ByVal arList As ArrayList) As ArrayList
        Dim TmpList As ArrayList = New ArrayList
        For Each objPMHeader As PMHeader In arList
            'If Not IsNothing(objPMHeader.ErrorMessage) AndAlso objPMHeader.ErrorMessage.ToString() <> "" Then
            '    Continue For
            'End If

            If IsNothing(objPMHeader.PMKind) Then
                'objPMHeader.PMKind = New PMKind(ID:=objPMHeader.PMKindID)
            End If


            If Not IsNothing(objPMHeader.ChassisMaster) And Not IsNothing(objPMHeader.Dealer) Then
                If IsNothing(objPMHeader.PMKind) Then
                    If objPMHeader.ErrorMessage = String.Empty Then
                        objPMHeader.ErrorMessage = "Jenis PM tidak valid"
                    Else
                        objPMHeader.ErrorMessage = objPMHeader.ErrorMessage + ";<br> Jenis PM tidak valid"
                    End If
                    TmpList.Add(objPMHeader)
                Else
                    Dim StrFunction As String = String.Format("(SELECT ID FROM dbo.fn_PmHeaderChecking('{0}','{1}',{2}))", objPMHeader.ChassisMaster.ChassisNumber, objPMHeader.PMKind.KindCode, 0)

                    Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.ID", MatchType.Exact, objPMHeader.ChassisMaster.ID))
                    'criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "Dealer.ID", MatchType.Exact, objPMHeader.Dealer.ID))
                    'criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ServiceDate", MatchType.Exact, objPMHeader.ServiceDate))
                    criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ID", MatchType.InSet, StrFunction))
                    Dim PMHeaderCollectionByCode As ArrayList = New PMHeaderFacade(User).Retrieve(criterias2)

                    Dim checkRuleChassis_No As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    checkRuleChassis_No.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "StandKM", MatchType.GreaterOrEqual, objPMHeader.StandKM))
                    checkRuleChassis_No.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.ID", MatchType.Exact, objPMHeader.ChassisMaster.ID))
                    'checkRuleChassis_No.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ID", MatchType.InSet, StrFunction))
                    Dim PMHeaderCollectionByKM As ArrayList = New PMHeaderFacade(User).Retrieve(checkRuleChassis_No)

                    If PMHeaderCollectionByCode.Count > 0 Then
                        If objPMHeader.ErrorMessage = "" Then
                            objPMHeader.ErrorMessage = "Jenis PM sama dengan atau kurang dari data yang sudah terdaftar"
                        Else
                            objPMHeader.ErrorMessage = objPMHeader.ErrorMessage + ";<br> Jenis PM sama dengan atau kurang dari data yang sudah terdaftar"
                        End If
                    End If

                    If PMHeaderCollectionByKM.Count > 0 Then
                        If objPMHeader.ErrorMessage = "" Then
                            objPMHeader.ErrorMessage = "KM kurang dari atau sama dengan data yang pernah disimpan"
                        Else
                            objPMHeader.ErrorMessage = objPMHeader.ErrorMessage + ";<br> KM kurang dari atau sama dengan data yang pernah disimpan"
                        End If
                    End If
                    TmpList.Add(objPMHeader)
                End If
            Else
                TmpList.Add(objPMHeader)
            End If
        Next
        Return TmpList
    End Function

    Private Sub dgPMHeaderUpload_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPMHeaderUpload.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As PMHeader = CType(e.Item.DataItem, PMHeader)

            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgPMHeaderUpload.CurrentPageIndex * dgPMHeaderUpload.PageSize)

                Dim lblTglPM As Label = CType(e.Item.FindControl("lblTglPM"), Label)
                If RowValue.ServiceDate = New DateTime(1900, 1, 1) Or RowValue.ServiceDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString() Then
                    lblTglPM.Text = ""
                Else
                    If RowValue.ServiceDate < New Date(1900, 1, 1) Then
                        lblTglPM.Text = ""
                    Else
                        lblTglPM.Text = RowValue.ServiceDate
                    End If
                End If

                Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblPopUpDetail"), Label)
                Dim lblMessage As Label = CType(e.Item.FindControl("lblMessage"), Label)
                If Not RowValue.ErrorMessage <> "" Then
                    lblMessage.Text = "OK"
                    lblMessage.BackColor = GreenYellow
                    lblPopUp.Visible = True
                    'lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Service/FrmPopUpPMDetail.aspx?id=0&dc=" & RowValue.Dealer.DealerCode & "&cn=" & RowValue.ChassisMaster.ChassisNumber & "&dt=" & RowValue.ServiceDate.ToString, "", 310, 500, "ShowPopUp")
                    lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Service/FrmPopUpPMDetail.aspx?id=0&index=" & e.Item.ItemIndex, "", 310, 500, "ShowPopUp")
                Else
                    lblMessage.BackColor = LightSalmon
                    bIsError = True
                    lblPopUp.Visible = False
                End If

                Dim lblVisitTypeGr As Label = CType(e.Item.FindControl("lblVisitType"), Label)

                If lblVisitTypeGr.Text = "WI" Then
                    lblVisitTypeGr.Text = "Walk In"
                ElseIf lblVisitTypeGr.Text = "BO" Then
                    lblVisitTypeGr.Text = "Booking"
                Else
                    lblVisitTypeGr.Text = ""
                End If

                Dim lblPMKind As Label = CType(e.Item.FindControl("lblPMKind"), Label)
                If Not IsNothing(lblPMKind) Then
                    lblPMKind.Text = If(Not IsNothing(RowValue.PMKind), RowValue.PMKind.KindCode, String.Empty)
                End If

                Dim lblDealerBranch As Label = CType(e.Item.FindControl("lblDealerBranch"), Label)
                If Not IsNothing(RowValue.DealerBranch) Then
                    lblDealerBranch.Text = RowValue.DealerBranch.DealerBranchCode
                Else
                    lblDealerBranch.Text = RowValue.DealerBranchCodeMsg
                End If

            End If
            _sessHelper.SetSession("sessError", bIsError)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim nResult As Integer
            Dim objPMHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
            Dim bCheckSuccess As Integer
            Dim arList As ArrayList = CType(_sessHelper.GetSession("sessPMHeader"), ArrayList)
            Dim validDatalist As ArrayList = New ArrayList

            Dim checkingDataResults = CheckExistingInPMHeader(arList)
            For Each itemData As PMHeader In checkingDataResults
                Dim statusData = itemData.ErrorMessage
                If statusData = "" Then
                    validDatalist.Add(itemData)
                End If
            Next

            bCheckSuccess = objPMHeaderFacade.Insert(validDatalist)
            If bCheckSuccess > 0 Then
                MessageBox.Show("Proses simpan berhasil!")
            Else
                MessageBox.Show("Proses simpan gagal!")
            End If
            btnSave.Enabled = False

            'bCheckSuccess = objPMHeaderFacade.Insert(validArrList)

            'If bCheckSuccess > 0 Then
            '    MessageBox.Show("Proses simpan berhasil!")
            'Else
            '    MessageBox.Show("Proses simpan gagal!")
            'End If
            'btnSave.Enabled = False
        Catch ex As Exception
            MessageBox.Show("Proses simpan gagal!")
        End Try
    End Sub
#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class