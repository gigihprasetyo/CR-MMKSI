#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmWSCUploadBB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents dtgWSC As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents infWSCData As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label

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
    Private _iSeqNum As Integer  '-- Sequence number
    Private _Dealer As Dealer    '-- Dealer object
    Private _sesHelper As SessionHelper = New SessionHelper
    Private ListWSC As ArrayList
    Dim dt As DateTime = DateTime.Now
    Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
#End Region

#Region "Custom Method"

    Private Sub DealerInit()
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(52)
        _sesHelper.SetSession("DEALER", objDealer)
    End Sub

    Private Sub BindHeader()
        checkDealer()
        Me.lblDealer.Text = CType(Session("DEALER"), Dealer).DealerCode & " / " & CType(Session("DEALER"), Dealer).SearchTerm1
        Me.lblDealerName.Text = CType(Session("DEALER"), Dealer).DealerName
    End Sub

    Private Sub checkDealer()
        'If Session("DEALER") Is Nothing Then
        '    Response.Redirect("../SessionExpired.htm")
        'End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.WSCUploadView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=WSC - WSC Upload")
        End If
        'If Not IsDownloaded() Then
        '    Server.Transfer("../FrmAccessDenied.aspx?mess=Anda belum melakukan download Kwitansi WSC (Menu Daftar Dokumen Service)")
        'End If
        
        Me.btnSave.Visible = SecurityProvider.Authorize(context.User, SR.WSCUploadSave_Privilege)
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
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(1,6,7)"))

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
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(1,6,7)"))

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then

            If Not IsDownloaded() Then
                Dim strMessage As String = String.Empty
                strMessage = GetMonthlyFaultDescription()
                Dim strMessageHeader As String = "Anda belum melakukan download atau kirim dokumen Warranty Letter/Warranty Status List/Kwitansi Warranty (Menu Daftar Dokumen Service) "
                Server.Transfer("../FrmAccessDenied.aspx?isEncode=1&mess=" & Server.UrlEncode(strMessageHeader) & "&messDescription=" & Server.UrlEncode(strMessage) & "")
            End If

            'DealerInit()
            ViewState("CurrentSortColumn") = ""
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            ViewState.Add("IsErrorInData", True)
            BindHeader()
            Me.dtgWSC.DataSource = New ArrayList
            Me.dtgWSC.DataBind()
        End If
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click

        'checkDealer()
        _sesHelper.RemoveSession("WSCHeaderBBS")
        Dim sErrMsg As String = ""
        If Not (Me.infWSCData.Value Is Nothing Or Me.infWSCData.Value.Trim = "") Then
            If (Not infWSCData.PostedFile Is Nothing) And (infWSCData.PostedFile.ContentLength > 0) Then
                If infWSCData.PostedFile.ContentType = "text/plain" Then
                    'cek maxFileSize first
                    Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                    If infWSCData.PostedFile.ContentLength > maxFileSize Then
                        MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                        Exit Sub
                    End If

                    Dim SrcFile As String = Path.GetFileName(Me.infWSCData.PostedFile.FileName)
                    Dim fileName As String = Path.GetFileNameWithoutExtension(Me.infWSCData.PostedFile.FileName)
                    Dim fileExt As String = Path.GetExtension(Me.infWSCData.PostedFile.FileName)
                    Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\" & fileName & sSuffix & fileExt

                    '-- Impersonation to manipulate file in server
                    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                    Dim objWSCBBParser As WSCBBParser
                    Dim CompleteListWSC As ArrayList

                    Try
                        If imp.Start() Then
                            Dim objUpload As New UploadToWebServer
                            objUpload.Upload(Me.infWSCData.PostedFile.InputStream, TempFile)

                            imp.StopImpersonate()
                            imp = Nothing

                            objWSCBBParser = New WSCBBParser
                            objWSCBBParser.Dealer = CType(Session("DEALER"), Dealer)

                            CompleteListWSC = CType(objWSCBBParser.ParsingNoTrasaction(TempFile, User.Identity.Name), ArrayList)

                        End If
                    Catch ex As Exception

                    End Try

                    If (Not CompleteListWSC Is Nothing) AndAlso CompleteListWSC.Count > 0 Then
                        ListWSC = ArrayListPager.DoPage(CompleteListWSC, 0, dtgWSC.PageSize)
                        _sesHelper.SetSession("WSCHeaderBBS", CompleteListWSC)
                        Me.dtgWSC.DataSource = ListWSC
                        dtgWSC.VirtualItemCount = CompleteListWSC.Count
                        dtgWSC.CurrentPageIndex = 0
                        Me.dtgWSC.DataBind()
                    Else
                        Me.dtgWSC.DataSource = New ArrayList
                        Me.dtgWSC.DataBind()
                    End If

                    viewstate.Item("IsErrorInData") = objWSCBBParser.IsErrorExist()
                    If viewstate.Item("IsErrorInData") Then
                        Me.btnSave.Enabled = False
                    Else
                        Me.btnSave.Enabled = True
                    End If
                Else
                    sErrMsg = "File yang Anda pilih bukan file text"
                End If
            Else
                sErrMsg = "File yang Anda pilih tidak ditemukan"
            End If
        Else
            sErrMsg = "Anda belum memilih file"
        End If
        If sErrMsg <> "" Then
            MessageBox.Show(sErrMsg)
            Me.dtgWSC.DataSource = New ArrayList
            Me.dtgWSC.CurrentPageIndex = 0
            Me.dtgWSC.VirtualItemCount = 0
            Me.dtgWSC.DataBind()
        End If

    End Sub

    Private Sub dtgWSC_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgWSC.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgWSC.PageSize * dtgWSC.CurrentPageIndex)).ToString
            'If Not CType(ListWSC.Item(e.Item.ItemIndex), WSCHeaderBB).ChassisMasterBB Is Nothing Then
            '    e.Item.Cells(4).Text = CType(ListWSC.Item(e.Item.ItemIndex), WSCHeaderBB).ChassisMasterBB.ChassisNumber
            'Else
            '    e.Item.Cells(4).Text = ""
            'End If
            If Not IsNothing(e.Item.AccessKey) Then

            End If
            Dim it As DataGridItem
            it = e.Item
            Dim objWSCHeaderBB As New WSCHeaderBB
            objWSCHeaderBB = CType(e.Item.DataItem, WSCHeaderBB)

            If CType(ViewState.Item("IsErrorInData"), Boolean) = True Then
                Me.btnSave.Enabled = False
            Else
                Me.btnSave.Enabled = True
            End If
            'If objWSCHeader.ErrorMessage <> "" Then
            '    btnSave.Enabled = False
            'Else
            '    btnSave.Enabled = True
            'End If

        End If
    End Sub

    Private Function isKodeTypeExist(ByVal kode As String, ByVal tipe As String, ByVal WSCDetailBBList As ArrayList, ByVal WorkCode As String) As Integer
        Dim found As Integer = 0
        For Each item As WSCDetailBB In WSCDetailBBList
            If tipe.Substring(0, 1).Trim.ToUpper = "P" Then
                If (kode.ToUpper.Trim = item.Code.ToUpper.Trim) And (tipe.ToUpper.Trim = item.Type.ToUpper.Trim) Then
                    found += 1
                End If
            Else
                If tipe.Substring(0, 1).Trim.ToUpper = "L" Then
                    'Dim lcode As String = String.Empty
                    'If Not item.LaborMaster Is Nothing Then
                    '    lcode = item.LaborMaster.LaborCode
                    'End If
                    If (kode.ToUpper.Trim = item.Code.ToUpper.Trim) And (tipe.ToUpper.Trim = item.Type.ToUpper.Trim) And (WorkCode.Trim.ToUpper = item.WorkQty.Trim.ToUpper) Then
                        found += 1
                    End If
                End If
            End If
        Next
        Return found
    End Function


    Private Function ValidateWSC(ByVal wscList As ArrayList) As Boolean
        For Each item As WSCHeaderBB In wscList
            Dim count As Integer = 0
            For Each detil As WSCDetailBB In item.WSCDetailBBs
                'Dim lCode As String = String.Empty
                'If Not detil.LaborMaster Is Nothing Then
                '    lCode = detil.LaborMaster.LaborCode
                'End If
                count += isKodeTypeExist(detil.Code, detil.Type, item.WSCDetailBBs, detil.WorkQty)
            Next
            If count > item.WSCDetailBBs.Count Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim ListWSCHeaderBB As ArrayList
        ListWSCHeaderBB = CType(Session("WSCHeaderBBS"), ArrayList)
        Dim ObjWSCHeaderBBFacade As WSCHeaderBBFacade
        ObjWSCHeaderBBFacade = New WSCHeaderBBFacade(User)
        If ValidateWSC(ListWSCHeaderBB) Then
            If ObjWSCHeaderBBFacade.InsertWSC(ListWSCHeaderBB) = 0 Then
                MessageBox.Show("WSC berhasil ditambahkan")
                Me.btnSave.Enabled = False
            End If
        Else
            MessageBox.Show("Terdapat data duplikat, silahkan di cek lagi.")
        End If

    End Sub

#End Region

    Private Sub dtgWSC_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgWSC.PageIndexChanged
        Dim CompletelistWSC As ArrayList = _sesHelper.GetSession("WSCHeaderBBS")
        If Not CompletelistWSC Is Nothing Then
            ListWSC = ArrayListPager.DoPage(CompletelistWSC, e.NewPageIndex, dtgWSC.PageSize)
            Me.dtgWSC.DataSource = ListWSC
            Me.dtgWSC.VirtualItemCount = CompletelistWSC.Count
            Me.dtgWSC.CurrentPageIndex = e.NewPageIndex
            Me.dtgWSC.DataBind()
        End If
    End Sub

    Private Sub dtgWSC_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgWSC.SortCommand
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

        Dim CompletelistWSC As ArrayList = _sesHelper.GetSession("WSCHeaderBBS")
        If Not CompletelistWSC Is Nothing Then
            SortListControl(CompletelistWSC, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            ListWSC = ArrayListPager.DoPage(CompletelistWSC, 0, dtgWSC.PageSize)
            Me.dtgWSC.DataSource = ListWSC
            Me.dtgWSC.VirtualItemCount = CompletelistWSC.Count
            Me.dtgWSC.CurrentPageIndex = 0
            Me.dtgWSC.DataBind()
        End If
    End Sub

    Private Sub SortListControl(ByRef pCompletelistWSC As ArrayList, ByVal SortColumn As String, _
            ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = True
        If SortDirection = Sort.SortDirection.ASC Then
            IsAsc = True
        ElseIf SortDirection = Sort.SortDirection.DESC Then
            IsAsc = False
        End If

        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        pCompletelistWSC.Sort(objListComparer)

    End Sub
End Class