#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security
Imports System.Text
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmFreeServisViaText
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents dgFreeServisUpload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgFreeServisView As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dfChassis As System.Web.UI.HtmlControls.HtmlInputFile

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblFileName As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    'Protected WithEvents lnkTemplate As System.Web.UI.WebControls.LinkButton
    Private bIsError As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Method"

    Protected Sub DownloadSampleButton_Click(sender As Object, e As EventArgs)
        Dim strName As String = "Template-UploadFreeServis.txt"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\FS\" & strName)
        'Dim sb As StringBuilder = New StringBuilder
        'Dim sample1 As String = "100001,MHMFE349E4R062877,4D34-432469,1,04122016,04122016,1000,300016, WI"
        'Dim sample2 As String = "100001,MHMFE349E4R062823,4D34-432865,1,04122018,04122018,3000,300014, BO"
        'sb.Append(sample1 & vbCrLf & sample2)
        'sb.Append("" & vbCrLf)
        'Dim text As String = sb.ToString
        'Response.Clear()
        'Response.ClearHeaders()
        'Response.AddHeader("Content-Length", text.Length.ToString)
        'Response.ContentType = "text/plain"
        'Response.AppendHeader("content-disposition", "attachment;filename=""FSUploadSample.txt""")
        'Response.Write(text)
        'Response.End()
    End Sub

    Private Sub BindUpload()
        dgFreeServisUpload.CurrentPageIndex = 0
        ViewState.Add("dtgPageIndex", 0)
        Dim strError As String = ""

        If (Not dfChassis.PostedFile Is Nothing) And (dfChassis.PostedFile.ContentLength > 0) And _
        ((dfChassis.PostedFile.ContentType.ToLower() = "text/plain") Or (dfChassis.PostedFile.ContentType.ToLower() = "text/csv") _
        Or (dfChassis.PostedFile.ContentType.ToLower() = "application/octet-stream") Or (dfChassis.PostedFile.ContentType.ToLower() = "application/vnd.ms-excel")) Then
            Dim Extension As String = Path.GetExtension(dfChassis.PostedFile.FileName)

            If Extension.ToUpper = ".CSV" Or Extension.ToUpper = ".TXT" Then '--> req by rna 20100823

                Dim SrcFile As String = Format(Date.Now, "yyyyMMddHHmmss") & Path.GetFileName(dfChassis.PostedFile.FileName)
                Dim DestFile As String = Server.MapPath("") & "\..\DataFile\" & SrcFile
                Try
                    strError = "1"
                    ''---------- Pake ini Error -----------
                    'dfChassis.PostedFile.SaveAs(DestFile) '-- Copy source file to destination file

                    ''---------- Pake UploadToWebServer saja -----------
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(dfChassis.PostedFile.InputStream, DestFile)
                    strError = "2"
                    Dim parser As IParser = New FreeServisParser
                    Dim arList As ArrayList = CType(parser.ParseNoTransaction(DestFile, "User"), ArrayList)

                    'periksa tidak boleh ada chassis# dan kind# yang sama di tabel freeservice
                    Dim NewList As ArrayList = New ArrayList
                    NewList = arList
                    'remark by anh, validation already exist in parser
                    'NewList = CheckExistingInFreeService(arList)
                    'end remark

                    AddUserCreator(NewList)

                    strError = "3"
                    'periksa kalo ada record rangkap (chassis# dan kind# yang sama) di arraylist
                    CheckDoubleRows(NewList)

                    'periksa kalo ada record rangkap (chassis# yang sama dlm satu bulan yang sama) di arraylist
                    CheckChassisDouble(NewList)
                    CheckChassisDoubleDataBase(NewList)

                    'remarks by anh dipindah ke parser
                    'If ValidateLBUMBengkulu(NewList) = False Then
                    '    MessageBox.Show("Kendaraan tidak valid")
                    '    _sessHelper.SetSession("sessError", True)
                    'End If
                    'end remarks

                    CheckChassisCategory(NewList)

                    strError = "4"
                    _sessHelper.SetSession("sessFreeService", NewList)
                    dgFreeServisUpload.DataSource = NewList
                    dgFreeServisUpload.DataBind()

                Catch Ex As Exception
                    MessageBox.Show(Ex.Message + " " + SR.UploadFail(SrcFile))
                    'MessageBox.Show(strError)
                End Try
            Else
                MessageBox.Show("Jenis file tidak sesuai (file yang diterima plain/text)")
            End If
        Else
            MessageBox.Show("Pilih file yang akan di-upload.")
        End If
    End Sub

    Private Function CheckChassisCategory(ByVal arList As ArrayList) As ArrayList
        Dim TmpList As ArrayList = New ArrayList
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        For Each objFS As FreeService In arList
            If Not IsNothing(objFS.ChassisMaster) Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, objFS.ChassisMaster.ChassisNumber))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
                Dim ChassisMasterCollection As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
                If ChassisMasterCollection.Count = 0 Then
                    objFS.ErrorMessage = "Chassis tidak terdaftar di " + companyCode
                    TmpList.Add(objFS)
                Else
                    TmpList.Add(objFS)
                End If
            Else
                TmpList.Add(objFS)
            End If
        Next
        Return TmpList
    End Function

    Private Sub CheckChassisDoubleDataBase(ByRef NewList As ArrayList)
        Dim nIndex As Integer
        Dim nIterate As Integer = 0
        For Each objFreeService As FreeService In NewList
            If Not IsNothing(objFreeService.ChassisMaster) Then
                'For nIndex = nIterate To NewList.Count - 1
                'Next
                'Dim objFreeService2 As FreeService
                'objFreeService2 = NewList(nIndex)
                Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, objFreeService.ChassisMaster.ChassisNumber))
                critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.Exact, objFreeService.ServiceDate))
                Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
                If Not IsNothing(arlFS) AndAlso arlFS.Count > 0 Then
                    If objFreeService.ErrorMessage = "" Then
                        objFreeService.ErrorMessage = "Data Chasis sudah mengajukan service pada tanggal yang sama "
                    Else
                        objFreeService.ErrorMessage = objFreeService.ErrorMessage + ";<br> Data Chasis sudah mengajukan service pada tanggal yang sama "
                    End If
                End If
            End If
            nIterate = nIterate + 1
        Next

    End Sub

    Private Function ValidateLBUMBengkulu(ByVal arlFS As ArrayList) As Boolean
        Dim oD As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim nOK As Integer = 0

        For Each oFS As FreeService In arlFS
            If oFS.FSKind.KindCode = "6" _
                    AndAlso (oFS.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE7" _
                    Or oFS.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE8") _
                    Then
                If oD.DealerCode = "100016" _
                AndAlso (oFS.ChassisMaster.EndCustomer.OpenFakturDate >= DateSerial(2010, 2, 1) _
                And oFS.ChassisMaster.EndCustomer.OpenFakturDate <= DateSerial(2010, 6, 30)) _
                Then
                    nOK += 1
                Else
                    Return False
                End If
            Else
                nOK += 1
            End If
        Next

        If nOK = arlFS.Count Then
            Return True
        Else
            Return False
        End If

        'If oD.DealerCode <> "100016" AndAlso oFS.FSKind.KindCode = "6" _
        'AndAlso (oFS.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE7" _
        '    Or oFS.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE8") _
        'AndAlso (oFS.ChassisMaster.EndCustomer.OpenFakturDate >= DateSerial(2010, 2, 1) _
        '    And oFS.ChassisMaster.EndCustomer.OpenFakturDate <= DateSerial(2010, 6, 30)) _
        'Then
        '    Return True
        'Else
        '    Return False
        'End If
    End Function

    Private Sub AddUserCreator(ByRef arrL As ArrayList)
        For Each objFreeService As FreeService In arrL
            objFreeService.CreatedBy = CType(User.Identity.Name, String)
        Next
    End Sub

    Private Sub CheckChassisDouble(ByRef NewList As ArrayList)
        Dim nIndex As Integer
        Dim nIterate As Integer = 1
        For Each objFreeService As FreeService In NewList
            If Not IsNothing(objFreeService.ChassisMaster) And Not IsNothing(objFreeService.FSKind) Then
                For nIndex = nIterate To NewList.Count - 1
                    Dim objFreeService2 As FreeService
                    objFreeService2 = NewList(nIndex)
                    'compare dg Record
                    If Not IsNothing(objFreeService2.ChassisMaster) Then
                        Dim sChassisNumber2 = objFreeService2.ChassisMaster.ChassisNumber
                        Dim sChassisNumber1 = objFreeService.ChassisMaster.ChassisNumber

                        Dim sMonthService1 = objFreeService.ServiceDate.Month
                        Dim sMonthService2 = objFreeService2.ServiceDate.Month

                        If sChassisNumber1 = sChassisNumber2 And sMonthService1 = sMonthService2 Then
                            If objFreeService2.ErrorMessage = "" Then
                                objFreeService2.ErrorMessage = "Data Chasis Ganda dibulan yang sama dg Record " + CType(nIterate, String)
                            Else
                                objFreeService2.ErrorMessage = objFreeService2.ErrorMessage + ";<br> Data Chasis Ganda dibulan yang sama dg Record " + CType(nIterate, String)
                            End If
                        End If
                    End If

                    'compare dg db    
                    'Dim arFS As New ArrayList
                    'Dim oFSFac As FreeServiceFacade = New FreeServiceFacade(User)
                    'Dim cFS As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'Dim Sql As String = ""
                    'Sql &= " select distinct(FS.ID) from FreeService FS, ChassisMaster CM"
                    'Sql &= " where 1=1 and Month(FS.ServiceDate) =" & objFreeService.ServiceDate.Month
                    'Sql &= " and Year(FS.ServiceDate) =" & objFreeService.ServiceDate.Year
                    'Sql &= " and CM.ChassisNumber = '" & objFreeService.ChassisMaster.ChassisNumber & "'"
                    'cFS.opAnd(New Criteria(GetType(FreeService), "ID", MatchType.InSet, "(" & Sql & ")"))
                    'arFS = oFSFac.Retrieve(cFS)
                    'If arFS.Count > 0 Then
                    '    If objFreeService2.ErrorMessage = "" Then
                    '        objFreeService2.ErrorMessage = "Chasis No sudah pernah servis dibulan yang sama"
                    '    Else
                    '        objFreeService2.ErrorMessage = objFreeService2.ErrorMessage + ";<br> Chasis No sudah pernah servis dibulan yang sama "
                    '    End If
                    'End If

                Next

            End If
            nIterate = nIterate + 1
        Next

    End Sub

    Private Sub CheckDoubleRows(ByRef NewList As ArrayList)
        Dim nIndex As Integer
        Dim nIterate As Integer = 1
        For Each objFreeService As FreeService In NewList
            If Not IsNothing(objFreeService.ChassisMaster) And Not IsNothing(objFreeService.FSKind) Then
                For nIndex = nIterate To NewList.Count - 1
                    Dim objFreeService2 As FreeService
                    objFreeService2 = NewList(nIndex)
                    If Not IsNothing(objFreeService2.ChassisMaster) And Not IsNothing(objFreeService2.FSKind) Then
                        Dim sChassisNumber2 = objFreeService2.ChassisMaster.ChassisNumber
                        Dim sChassisNumber1 = objFreeService.ChassisMaster.ChassisNumber

                        Dim sKindCode1 = objFreeService.FSKind.KindCode
                        Dim sKindCode2 = objFreeService2.FSKind.KindCode

                        If sChassisNumber1 = sChassisNumber2 And sKindCode1 = sKindCode2 Then
                            If objFreeService2.ErrorMessage = "" Then
                                objFreeService2.ErrorMessage = "Data FS Ganda dg Record " + CType(nIterate, String)
                            Else
                                objFreeService2.ErrorMessage = objFreeService2.ErrorMessage + ";<br> Data FS Ganda dg Record " + CType(nIterate, String)
                            End If
                        End If
                    End If
                Next
            End If
            nIterate = nIterate + 1
        Next

    End Sub

    Private Function CheckExistingInFreeService(ByVal arList As ArrayList) As ArrayList
        Dim TmpList As ArrayList = New ArrayList
        For Each objFreeService As FreeService In arList
            If Not IsNothing(objFreeService.ChassisMaster) And Not IsNothing(objFreeService.FSKind) Then
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, objFreeService.ChassisMaster.ChassisNumber))
                Dim ChassisMasterCollection As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)

                If ChassisMasterCollection.Count > 0 Then
                    Dim objChassisMaster As ChassisMaster = New ChassisMaster
                    objChassisMaster = ChassisMasterCollection(0)
                    Dim criterias1 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "KindCode", MatchType.Exact, objFreeService.FSKind.KindCode))
                    Dim FSKindCollection As ArrayList = New FSKindFacade(User).Retrieve(criterias1)

                    If FSKindCollection.Count > 0 Then
                        Dim objFSKind As FSKind = New FSKind
                        objFSKind = FSKindCollection(0)
                        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, objChassisMaster.ID))
                        criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "FSKind.ID", MatchType.Exact, objFSKind.ID))
                        Dim FreeServiceCollection As ArrayList = New FreeServiceFacade(User).Retrieve(criterias2)

                        If FreeServiceCollection.Count > 0 Then
                            If objFreeService.ErrorMessage = "" Then
                                objFreeService.ErrorMessage = "Data FS Sudah Terdaftar"
                            Else
                                objFreeService.ErrorMessage = objFreeService.ErrorMessage + ";<br> Data FS Sudah Terdaftar"
                            End If
                            TmpList.Add(objFreeService)
                        Else
                            TmpList.Add(objFreeService)
                        End If

                    Else
                        TmpList.Add(objFreeService)
                    End If

                    'Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, objFreeService.ChassisMaster.ChassisNumber))
                    'critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.Exact, objFreeService.ServiceDate))
                    'Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
                    'If Not IsNothing(arlFS) AndAlso arlFS.Count > 0 Then
                    '    If objFreeService.ErrorMessage = "" Then
                    '        objFreeService.ErrorMessage = "Data Chasis sudah mengajukan service pada tanggal yang sama "
                    '    Else
                    '        objFreeService.ErrorMessage = objFreeService.ErrorMessage + ";<br> Data Chasis sudah mengajukan service pada tanggal yang sama "
                    '    End If
                    '    TmpList.Add(objFreeService)
                    'Else
                    '    TmpList.Add(objFreeService)
                    'End If
                Else
                    TmpList.Add(objFreeService)
                End If
            Else
                TmpList.Add(objFreeService)
                objFreeService.ErrorMessage = "Nomor Chassis tidak terdaftar"
            End If
        Next
        Return TmpList

    End Function

    Private Sub BinddgFreeServisUpload(ByVal pageIndeks As Integer)
        Dim totalRow As Integer = 0
        dgFreeServisUpload.DataSource = CType(_sessHelper.GetSession("sessFreeService"), ArrayList)
        dgFreeServisUpload.VirtualItemCount = totalRow
        dgFreeServisUpload.DataBind()
    End Sub

    Private Function IsExistCode(ByVal ChassisID As Integer, ByVal FSKindID As Integer) As Boolean
        Dim objFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
        'Periksa agar tidak ada key ganda 
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, ChassisID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "FSKind.ID", MatchType.Exact, FSKindID))
        Dim TestExist As ArrayList = New FreeServiceFacade(User).Retrieve(criterias)
        If TestExist.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function InsertFreeService(ByVal objFreeService As FreeService) As Integer
        Dim objFreeServisFacade As FreeServiceFacade = New FreeServiceFacade(User)
        objFreeService.ChassisMaster = New ChassisMasterFacade(User).Retrieve(objFreeService.ChassisMaster.ID)
        objFreeService.FSKind = New FSKindFacade(User).Retrieve(objFreeService.FSKind.ID)
        objFreeService.Dealer = New DealerFacade(User).Retrieve(objFreeService.Dealer.ID)


        '---check is fleet available, add by wdi 20161006
        Dim critFleetFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critFleetFaktur.opAnd(New Criteria(GetType(FleetFaktur), "ChassisMaster.ID", MatchType.Exact, objFreeService.ChassisMaster.ID))
        Dim arrFleetFaktur As ArrayList = New FleetFakturFacade(User).Retrieve(critFleetFaktur)

        Dim objFleetFaktur As FleetFaktur
        If arrFleetFaktur.Count > 0 Then
            objFleetFaktur = arrFleetFaktur(0)
        End If

        If Not IsNothing(objFleetFaktur) Then
            objFreeService.FleetRequest = objFleetFaktur.FleetRequest
        End If
        '---end check fleet

        Return objFreeServisFacade.Insert(objFreeService)
    End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then

            If Not IsDownloaded() Then
                Dim strMessage As String = String.Empty
                strMessage = GetMonthlyFaultDescription()
                Dim strMessageHeader As String = "Anda belum melakukan download atau kirim dokumen Kwitansi FS Campaign/FS Letter/FS Campaign Letter/Kwitansi Free Labour/Free Labour Letter/Free Maintenance Letter/Kwitansi Free Maintenance pada periode berikut : "
                Server.Transfer("../FrmAccessDenied.aspx?isEncode=1&mess=" & Server.UrlEncode(strMessageHeader) & "&messDescription=" & Server.UrlEncode(strMessage) & "")
            End If

            If Not IsNothing(Session("DEALER")) Then
                Dim ObjDealer As Dealer = CType(Session.Item("DEALER"), Dealer)

                ''CR Tutup Menu
                '' by ali
                '' 2014 - 09 -30

                If (DateTime.Now >= New DateTime(2014, 10, 10) AndAlso DateTime.Now <= New DateTime(2014, 10, 11).AddMinutes(-1) AndAlso ObjDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    Dim MSgClose As String = IIf(Not IsNothing(KTB.DNet.Lib.WebConfig.GetValue("CloseMessage")), KTB.DNet.Lib.WebConfig.GetValue("CloseMessage"), "Module ini sedang di tutup, sampai dengan 10 Oktober 2014")
                    Server.Transfer("../ClossingMessage.htm")
                End If
                ''END CR Tutup Menu
                lblDealerCode.Text = ObjDealer.DealerCode + " / " + ObjDealer.SearchTerm1
                lblDealerName.Text = ObjDealer.DealerName
                _sessHelper.SetSession("sessDealer", ObjDealer)
                _sessHelper.SetSession("sessDealerLogin", ObjDealer.DealerCode)
            Else
                'Response.Redirect("../SessionExpired.htm")
            End If
        End If
    End Sub
    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.FreeServiceUploadView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FREE SERVICE - Upload Data Free Service")
        End If

        'If Not IsDownloaded() Then
        '    Server.Transfer("../FrmAccessDenied.aspx?mess=Anda belum melakukan download Kwitansi FS atau FS Letter (Menu Daftar Dokumen Service)")
        'End If



        'FreeServiceUploadSave_Privilege  
        btnSave.Visible = SecurityProvider.Authorize(Context.User, SR.FreeServiceUploadSave_Privilege)
    End Sub

    Private Function IsDownloaded() As Boolean
        Dim _return As Boolean = False
        Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim ArlMonthly As ArrayList = New ArrayList
        Try
            Dim paramDate As DateTime = New DateTime(1900, 1, 1)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "ProductCategoryID", MatchType.Exact, "1"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "DealerCode", MatchType.Exact, objDealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(2, 4, 5, 12, 13, 14, 15)"))

            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Exact, "2017"), "((", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "4"), ")", False)

            'criterias.opOr(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Greater, "2017"), "(", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "1"), "))", False)

            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "CreatedTime", MatchType.GreaterOrEqual, Date.Now.AddMonths(-1)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.GreaterOrEqual, New DateTime(2017, 4, 1)))


            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.IsNull, True), "(", True)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.IsNull, True), ")", False)


            Dim dtn As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-2)


            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.LesserOrEqual, dtn))


            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(V_MonthlyReport), "Period", Sort.SortDirection.DESC))


            ArlMonthly = New V_MonthlyReportFacade(User).Retrieve(criterias, sortColl)
            If Not IsNothing(ArlMonthly) AndAlso ArlMonthly.Count > 0 Then
                Dim vM As New V_MonthlyReport
                vM = CType(ArlMonthly(0), V_MonthlyReport)

                If 1 = 1 OrElse (vM.Period.Year = dtn.Year AndAlso dtn.Month = vM.Period.Month) Then
                    _return = False
                Else
                    Return True
                End If



                _return = False
            Else
                _return = True
            End If
        Catch ex As Exception
            _return = False
        End Try
        Return _return
    End Function

    Private Function GetMonthlyFaultDescription() As String
        Dim _return As Boolean = False
        Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim ArlMonthly As ArrayList = New ArrayList
        Dim strMessage As String = String.Empty
        Try
            Dim paramDate As DateTime = New DateTime(1900, 1, 1)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "ProductCategoryID", MatchType.Exact, "1"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "DealerCode", MatchType.Exact, objDealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(2, 4, 5, 12, 13, 14, 15)"))

            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Exact, "2017"), "((", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "4"), ")", False)

            'criterias.opOr(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Greater, "2017"), "(", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "1"), "))", False)

            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "CreatedTime", MatchType.GreaterOrEqual, Date.Now.AddMonths(-1)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.GreaterOrEqual, New DateTime(2017, 4, 1)))


            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.IsNull, True), "(", True)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.IsNull, True), ")", False)


            Dim dtn As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-2)


            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.LesserOrEqual, dtn))


            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(V_MonthlyReport), "Period", Sort.SortDirection.DESC))

            ArlMonthly = New V_MonthlyReportFacade(User).Retrieve(criterias, sortColl)

            If Not IsNothing(ArlMonthly) AndAlso ArlMonthly.Count > 0 Then
                Dim itr As Integer = 0
                Dim currentYear As Integer = 0
                Dim currentMonth As Integer = 0
                strMessage = "||"
                For Each item As V_MonthlyReport In ArlMonthly
                    If (itr = 0) Then
                        currentYear = item.PeriodeYear
                        strMessage = "Year Periode : " & currentYear & "|Month : "
                    End If

                    If (item.PeriodeYear = currentYear) Then
                        If (currentMonth <> item.PeriodeMonth) Then
                            strMessage = strMessage & item.PeriodeMonth & ", "
                            currentMonth = item.PeriodeMonth
                        End If
                    Else
                        currentYear = item.PeriodeYear
                        strMessage = strMessage.Substring(0, strMessage.Length - 2) & "||Year Periode : " & currentYear & "|Month : " & item.PeriodeMonth & ", "
                        currentMonth = item.PeriodeMonth
                    End If

                    itr = itr + 1
                Next

                strMessage = strMessage.Substring(0, strMessage.Length - 2)

                'Dim lengthOfMessage As Integer = strMessage.Length
                'Dim lengthOfLastWord As Integer = 5
                'Dim lastWord1 As String = strMessage.Substring(lengthOfMessage - lengthOfLastWord, lengthOfLastWord)
                'Dim lastWord2 As String = strMessage.Substring(lengthOfMessage - (lengthOfLastWord + 1), (lengthOfLastWord + 1))
                'If (lastWord1 = "Month" Or lastWord2 = "Month ") Then

                'End If
                Return strMessage
            End If
        Catch ex As Exception
            strMessage = ex.Message
        End Try
        Return strMessage
    End Function

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        bIsError = False
        BindUpload()
        bIsError = CType(_sessHelper.GetSession("sessError"), Boolean)
        If Not bIsError And Path.GetFileName(dfChassis.PostedFile.FileName).ToString.Trim <> String.Empty Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub dgFreeServisUpload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgFreeServisUpload.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As FreeService = CType(e.Item.DataItem, FreeService)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                Dim lblFSKind As Label = CType(e.Item.FindControl("lblFSKind"), Label)
                If Not IsNothing(RowValue.FSKind) Then
                    lblFSKind.Text = RowValue.FSKind.KindDescription
                End If

                If RowValue.FSKindMsg <> "" And lblFSKind.Text = "" Then
                    lblFSKind.Text = RowValue.FSKindMsg
                End If

                Dim lblDealerBranch As Label = CType(e.Item.FindControl("lblDealerBranch"), Label)
                If Not IsNothing(RowValue.DealerBranch) Then
                    lblDealerBranch.Text = RowValue.DealerBranch.DealerBranchCode
                Else
                    lblDealerBranch.Text = RowValue.DealerBranchCodeMsg
                End If

                Dim lblChassisNo As Label = CType(e.Item.FindControl("lblChassisNo"), Label)
                If Not IsNothing(RowValue.ChassisMaster) Then
                    lblChassisNo.Text = RowValue.ChassisMaster.ChassisNumber
                Else
                    lblChassisNo.Text = RowValue.ChassisNumberMsg
                End If

                Dim lblTglFS As Label = CType(e.Item.FindControl("lblTglFS"), Label)
                'If RowValue.FSDateMsg <> "" Then
                '    lblTglFS.Text = RowValue.FSDateMsg
                'End If

                If RowValue.FSDateMsg = CDate("1/1/1900").ToString() Or _
                    RowValue.FSDateMsg = System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString() Or _
                    RowValue.FSDateMsg <> "" Then
                    lblTglFS.Text = ""
                Else
                    If RowValue.ServiceDate < New Date(1900, 1, 1) Then
                        lblTglFS.Text = ""
                    Else
                        lblTglFS.Text = RowValue.ServiceDate
                    End If

                End If

                Dim lblTglJual As Label = CType(e.Item.FindControl("lblTglJual"), Label)
                'If RowValue.SoldDateMsg <> "" Then
                '    lblTglJual.Text = RowValue.SoldDateMsg
                'End If

                If RowValue.SoldDateMsg = CDate("1/1/1900").ToString() Or _
                    RowValue.SoldDateMsg = System.Data.SqlTypes.SqlDateTime.MinValue.Value.ToString() Or _
                    RowValue.SoldDateMsg <> "" Then
                    lblTglJual.Text = ""
                Else
                    If RowValue.SoldDate < New Date(1900, 1, 1) Then
                        lblTglJual.Text = ""
                    Else
                        lblTglJual.Text = RowValue.SoldDate
                    End If

                End If

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgFreeServisUpload.CurrentPageIndex * dgFreeServisUpload.PageSize)

                Dim lblMessage As Label = CType(e.Item.FindControl("lblMessage"), Label)

                If Not RowValue.ErrorMessage <> "" Then
                    lblMessage.Text = "OK"
                    lblMessage.BackColor = GreenYellow
                Else
                    lblMessage.BackColor = LightSalmon
                    bIsError = True
                End If

                Dim lblMileAge As Label = CType(e.Item.FindControl("lblMileAge"), Label)
                If lblMileAge.Text = "" Then
                    lblMileAge.Text = RowValue.MileAgeMsg
                End If

                Dim lblTipeVisit As Label = CType(e.Item.FindControl("lblTipeVisit"), Label)
                If lblTipeVisit.Text = "" Then
                    lblTipeVisit.Text = RowValue.VisitType
                End If

            End If
            _sessHelper.SetSession("sessError", bIsError)
        End If
    End Sub

    Private Sub dgFreeServisUpload_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgFreeServisUpload.PageIndexChanged
        ViewState.Remove("dtgPageIndex")
        dgFreeServisUpload.CurrentPageIndex = e.NewPageIndex
        ViewState.Add("dtgPageIndex", e.NewPageIndex)
        BinddgFreeServisUpload(e.NewPageIndex + 1)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim bCheckSuccess As Boolean = True
        Dim arList As ArrayList = CType(_sessHelper.GetSession("sessFreeService"), ArrayList)
        For Each objFreeService As FreeService In arList
            If Not IsExistCode(objFreeService.ChassisMaster.ID, objFreeService.FSKind.ID) Then
                'Start remark by anh ; dipindah semua ke parser
                'Dim oFEFS As New FrmEntryFreeServis
                'Dim IsAllowedToInsert As Boolean = oFEFS.ChassisException(True, objFreeService)
                ''Start  :CR;by:dna;for:rina;on:20100615;remark:allow for below condition
                'If Not IsAllowedToInsert Then
                '    Dim dtFacturValidation As Date = DateSerial(objFreeService.ChassisMaster.EndCustomer.ValidateTime.Year, objFreeService.ChassisMaster.EndCustomer.ValidateTime.Month, objFreeService.ChassisMaster.EndCustomer.ValidateTime.Day)
                '    If (objFreeService.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE7" _
                '    OrElse objFreeService.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE8") _
                '    AndAlso (objFreeService.FSKind.KindCode = "3" OrElse objFreeService.FSKind.KindCode = "4" OrElse objFreeService.FSKind.KindCode = "5" OrElse objFreeService.FSKind.KindCode = "6" OrElse objFreeService.FSKind.KindCode = "7") _
                '    AndAlso (dtFacturValidation > DateSerial(2010, 4, 1).AddDays(-1) And dtFacturValidation < DateSerial(2010, 10, 1)) Then
                '        IsAllowedToInsert = True
                '    End If
                'End If
                ''End    :CR;by:dna;for:rina;on:20100615;remark:allow for below condition

                'IsAllowedToInsert = oFEFS.CheckToAllowStradaTriton(IsAllowedToInsert, objFreeService)

                'If Not IsAllowedToInsert Then Exit For '  Sub
                'end remark by anh
                Dim nResult As Integer = InsertFreeService(objFreeService)
                If nResult = -1 Then
                    bCheckSuccess = False
                End If
            End If
        Next
        If bCheckSuccess Then
            MessageBox.Show("Semua record berhasil disimpan !")
            Server.Transfer("FrmEntryFreeServis.aspx")
        Else
            MessageBox.Show("Ada record yang gagal disimpan !")
        End If
        btnSave.Enabled = False
    End Sub

    'Protected Sub lnkTemplate_Click(sender As Object, e As EventArgs) Handles lnkTemplate.Click
    '    Dim strName As String = "Template-UploadFreeServis.txt"
    '    Response.Redirect("../downloadlocal.aspx?file=DataFile\FS\" & strName)
    'End Sub

#End Region
End Class