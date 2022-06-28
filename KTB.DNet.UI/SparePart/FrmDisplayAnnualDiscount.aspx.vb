#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.Dnet.BusinessFacade.SparePart
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
#End Region

Public Class FrmDisplayAnnualDiscount
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnRetrive As System.Web.UI.WebControls.Button
    Protected WithEvents txtValidateTo As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlValidateFrom As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgDaftarAnnual As System.Web.UI.WebControls.DataGrid
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents opClient As System.Web.UI.HtmlControls.HtmlTableRow

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
    Private objDealer As Dealer
    Private arlAnnual As ArrayList
    Private ObjFileAnnualDiscount As FileUploadAnnualDiscount
    Private PROGRAM_ANNUAL_DISCOUNT As String = KTB.DNet.Lib.WebConfig.GetValue("PROGRAM_ANNUAL_DISCOUNT_FILe_NAME")
    Private PETUNJUK_PELAKSANAAN As String = KTB.DNet.Lib.WebConfig.GetValue("PETUNJUK_PELAKSANAAN_FILe_NAME")
    Private HASIL_ANNUAL_DISCOUNT As String = KTB.DNet.Lib.WebConfig.GetValue("HASIL_ANNUAL_DISCOUNT_FILe_NAME")
#End Region

#Region "Custom Method"
    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ProgramName"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub dgDaftarAnnual_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDaftarAnnual.SortCommand
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

        dgDaftarAnnual.SelectedIndex = -1
        dgDaftarAnnual.CurrentPageIndex = 0
        BindGrid(dgDaftarAnnual.CurrentPageIndex)

    End Sub

    Private Sub dgDaftarAnnual_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDaftarAnnual.PageIndexChanged
        dgDaftarAnnual.CurrentPageIndex = e.NewPageIndex
        BindGrid(dgDaftarAnnual.CurrentPageIndex)
    End Sub

    Private Sub BindGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FileUploadAnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        arlAnnual = New FileUploadAnnualDiscountFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dgDaftarAnnual.PageSize, _
               total, CType(ViewState("CurrentSortColumn"), String), _
               CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgDaftarAnnual.DataSource = CheckFileByDealer(arlAnnual)
        dgDaftarAnnual.VirtualItemCount = total
        dgDaftarAnnual.DataBind()
        arlAnnual = New FileUploadAnnualDiscountFacade(User).Retrieve(criterias)
        'dgDaftarAnnual.DataBind()
    End Sub

    Private Function CheckFileByDealer(ByVal _annualDiscount As ArrayList) As ArrayList
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim _arrFileUploadFilter As New ArrayList
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim strDealerGroup As String = String.Empty

        Try
            success = imp.Start
            If success Then
                If objDealer.DealerGroup Is Nothing Then
                    strDealerGroup = "SingleGroup"
                Else
                    strDealerGroup = objDealer.DealerGroup.GroupName
                End If

                'Dim DestFile1 As String = Server.MapPath("") & "\..\DataFile\AnnualDiscount\" & strDealerGroup & "\" & objDealer.DealerCode & "\"  '-- Destination file
                Dim DestFile1 As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\..\DataFile\AnnualDiscount\" & strDealerGroup & "\" & objDealer.DealerCode & "\" '-- Destination file

                Dim finfo1 As New FileInfo(DestFile1)
                Dim fileInfo1() As FileInfo
                If finfo1.Directory.Exists Then
                    Try
                        fileInfo1 = finfo1.Directory.GetFiles
                    Catch ex As Exception

                    End Try
                End If

                'Dim DestFile2 As String = Server.MapPath("") & "\..\DataFile\AnnualDiscount\" & strDealerGroup & "\" '-- Destination file
                Dim DestFile2 As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\..\DataFile\AnnualDiscount\" & strDealerGroup & "\" '-- Destination file
                Dim finfo2 As New FileInfo(DestFile2)
                Dim fileInfo2() As FileInfo
                If finfo2.Directory.Exists Then
                    Try
                        fileInfo2 = finfo2.Directory.GetFiles
                    Catch ex As Exception

                    End Try
                End If

                'For Each item As FileInfo In fileInfo1
                For Each item1 As FileUploadAnnualDiscount In _annualDiscount
                    If item1.Tipe = 0 Then
                        _arrFileUploadFilter.Add(item1)
                    ElseIf item1.Tipe = 2 Then
                        If Not fileInfo2 Is Nothing Then '(fileInfo2.Length <> 0)
                            For Each item As FileInfo In fileInfo2
                                If item.Name.ToUpper = item1.FileName.ToUpper Then
                                    _arrFileUploadFilter.Add(item1)
                                End If
                            Next
                        End If
                    Else
                        If Not fileInfo1 Is Nothing Then '(fileInfo1.Length <> 0) OrElse
                            For Each item As FileInfo In fileInfo1
                                If item.Name.ToUpper = item1.FileName.ToUpper Then
                                    _arrFileUploadFilter.Add(item1)
                                End If
                            Next
                        End If
                    End If
                Next
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
        End Try
        Return _arrFileUploadFilter
    End Function
    Private Function CheckFileExist(ByVal finfo As FileInfo) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Try
            If imp.Start() Then
                If finfo.Exists Then
                    Return True
                Else
                    Return False
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub Download(ByVal NamaFile As String, ByVal tipe As Int16)
        objDealer = CType(Session("DEALER"), Dealer)
        Dim strDealerGroup As String = String.Empty
        Dim isExist As Boolean = False
        If objDealer.DealerGroup Is Nothing Then
            strDealerGroup = "SingleGroup"
        Else
            strDealerGroup = objDealer.DealerGroup.GroupName
        End If

        If tipe = 0 Then
            'Dim fileInfo As New FileInfo(Server.MapPath("") & "\..\DataFile\AnnualDiscount\General" & "\" & NamaFile.ToString)
            Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\..\DataFile\AnnualDiscount\General" & "\" & NamaFile.ToString)
            isExist = CheckFileExist(fileInfo)
            If (isExist) Then
                Try
                    Response.Redirect("../Download.aspx?file=..\DataFile\AnnualDiscount\General" & "\" & NamaFile.ToString)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(NamaFile.ToString))
                End Try
            Else
                MessageBox.Show(SR.FileNotFound(fileInfo.Name))
            End If
        ElseIf tipe = 2 Then
            'Dim fileInfo As New FileInfo(Server.MapPath("") & "\..\DataFile\AnnualDiscount\" & strDealerGroup & "\" & NamaFile.ToString)
            Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\..\DataFile\AnnualDiscount\" & strDealerGroup & "\" & NamaFile.ToString)
            isExist = CheckFileExist(fileInfo)
            If (isExist) Then
                Try
                    Response.Redirect("../Download.aspx?file=..\DataFile\AnnualDiscount\" & strDealerGroup & "\" & NamaFile.ToString)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(NamaFile.ToString))
                End Try
            Else
                MessageBox.Show(SR.FileNotFound(fileInfo.Name))
            End If
        Else
            'Dim fileInfo As New FileInfo(Server.MapPath("") & "\..\DataFile\AnnualDiscount\" & strDealerGroup & "\" & objDealer.DealerCode & "\" & NamaFile.ToString)
            Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & "\..\DataFile\AnnualDiscount\" & strDealerGroup & "\" & objDealer.DealerCode & "\" & NamaFile.ToString)
            isExist = CheckFileExist(fileInfo)
            If (isExist) Then
                Try
                    Response.Redirect("../Download.aspx?file=..\DataFile\AnnualDiscount\" & strDealerGroup & "\" & objDealer.DealerCode & "\" & NamaFile.ToString)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(NamaFile.ToString))
                End Try
            Else
                MessageBox.Show(SR.FileNotFound(fileInfo.Name))
            End If
        End If
    End Sub

    Private Sub BindToDropDownList()
        Dim obj As AnnualDiscount
        Dim year As Integer = DateTime.Now.Year
        Dim ArrayListForDDl As ArrayList
        Dim objAnnualDiscountFacade As New AnnualDiscountFacade(User)
        ArrayListForDDl = objAnnualDiscountFacade.RetrieveDistinct

        For Each item As AnnualDiscount In ArrayListForDDl
            ddlValidateFrom.Items.Add(New ListItem(Format(item.ValidateDateFrom, "dd/MM/yyyy").ToString, Format(item.ValidateDateTo, "dd/MM/yyyy").ToString))
        Next
        ddlValidateFrom.DataBind()
        Dim selecteditem As String
        For Each item1 As ListItem In ddlValidateFrom.Items
            Dim strTanggal As String() = item1.Text.ToString.Split("/")
            Dim tgl As New DateTime(CInt(strTanggal(2)), CInt(strTanggal(1)), CInt(strTanggal(0)))
            If tgl >= DateTime.Now Then
                selecteditem = item1.Value
                Exit For
            End If
        Next

        ddlValidateFrom.SelectedValue = selecteditem
        ddlValidateFrom_SelectedIndexChanged(Nothing, Nothing)
        'ddlValidateFrom.DataSource = ArrayListForDDl
        'ddlValidateFrom.DataTextField = "ValidateDateFrom"
        'ddlValidateFrom.DataValueField = "ValidateDateTo"
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'Dim sortColl As SortCollection = New SortCollection                 '-- Sorting in DropDownList Periode
        'If (Not IsNothing("ValidateDateFrom")) Then
        'sortColl.Add(New Sort(GetType(AnnualDiscount), "ValidateDateFrom", Sort.SortDirection.ASC))
        'Else
        '    sortColl = Nothing
        'End If
        'ArrayListForDDl = New AnnualDiscountFacade(User).Retrieve(criterias, sortColl)

        'Dim CurrentDate As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)
        'For Each item As AnnualDiscount In ArrayListForDDl                  '-- Bind ArrayList to DropDownList
        'If item.ValidateDateFrom.Year = year Or item.ValidateDateFrom.Year = year - 1 Or item.ValidateDateFrom.Year = year + 1 Then
        'Dim listItem As New listItem(Format(item.ValidateDateFrom, "dd-MM-yyyy"), item.ID)
        'ddlValidateFrom.Items.Add(listItem)
        'End If
        'Next

        'Dim _id As Integer
        'For Each item As listItem In ddlValidateFrom.Items
        '    Dim str As String() = item.ToString.Split("-")
        '    Dim tgl As New DateTime(CInt(str(2)), CInt(str(1)), CInt(str(0)), 0, 0, 0)
        '    If tgl >= CurrentDate Then
        '        _id = item.Value
        '        Exit For
        '    End If

        'Next
        'If _id > 0 Then
        '    ddlValidateFrom.SelectedValue = _id
        '    obj = New AnnualDiscountFacade(User).Retrieve(_id)
        '    txtValidateTo.Text = Format(obj.ValidateDateTo, "dd-MM-yyyy")
        '    viewstate("ValidateTo") = obj.ValidateDateTo
        '    Dim strFrom As String() = ddlValidateFrom.SelectedItem.ToString.Split("-")
        '    Dim tglFrom As New DateTime(CInt(strFrom(2)), CInt(strFrom(1)), CInt(strFrom(0)))
        '    viewstate("ValidateFrom") = tglFrom
        'End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        CheckUserPrivilege()
        If Not IsPostBack Then

            BindToDropDownList()
            BindGrid(0)
            InitiatePage()
        End If
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewAnnualDiscountFileList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar File Annual Discount")
        End If
        dgDaftarAnnual.Columns(6).Visible = SecurityProvider.Authorize(Context.User, SR.DownloadAnnualDiscountFileList_Privilege)
        '--exclude  this privilege from Asra (BA)
        'btnRetrive.Visible = SecurityProvider.Authorize(Context.User, SR.SearchAnnualDiscountFileList_Privilege)
        If btnRetrive.Visible = False Then
            opClient.Visible = False
        End If
    End Sub

    Private Sub btnRetrive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRetrive.Click
        If Not Page.IsValid Then
            Return
        End If
        Response.Redirect("../SparePart/FrmListAnnualDiscount.aspx?From=" & ddlValidateFrom.SelectedItem.ToString & "&To=" & ddlValidateFrom.SelectedValue.ToString)
    End Sub

    Private Sub ddlValidateFrom_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlValidateFrom.SelectedIndexChanged
        'Dim arl As ArrayList
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.AnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.AnnualDiscount), "ID", MatchType.Exact, ddlValidateFrom.SelectedValue))
        'arl = New AnnualDiscountFacade(User).Retrieve(criterias)

        ''If ddlValidateFrom.SelectedValue <> -1 Then   '--Check Value Of DropDownList
        'If Not (arl) Is Nothing Then
        '    Dim str As String() = ddlValidateFrom.SelectedItem.ToString.Split("-")
        '    Dim tglFrom As New DateTime(CInt(str(2)), CInt(str(1)), CInt(str(0)))
        '    viewstate("ValidateFrom") = tglFrom
        '    txtValidateTo.Text = Format(arl(0).ValidateDateTo, "dd-MM-yyyy")
        '    viewstate("ValidateTo") = arl(0).ValidateDateTo
        'End If
        ''Else
        ''txtValidateTo.Text = String.Empty   '--Clear view State for Use In Button Retrive
        ''viewstate("ValidateTo") = ""
        ''viewstate("ValidateFrom") = ""
        ''End If
        txtValidateTo.Text = ddlValidateFrom.SelectedValue

    End Sub

    Private Sub dgDaftarAnnual_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDaftarAnnual.ItemCommand
        If e.CommandName = "Download" Then
            Download(e.Item.Cells(3).Text, e.Item.Cells(5).Text)
        End If
    End Sub

    Private Sub dgDaftarAnnual_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDaftarAnnual.ItemDataBound
        Dim ArlFileannualDiscount As ArrayList = CheckFileByDealer(arlAnnual)

        If e.Item.ItemIndex <> -1 AndAlso ArlFileannualDiscount.Count > 0 Then
            ObjFileAnnualDiscount = ArlFileannualDiscount(e.Item.ItemIndex)
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarAnnual.CurrentPageIndex * dgDaftarAnnual.PageSize)

            Dim _Programname As Label = CType(e.Item.FindControl("lblProgramName"), Label)
            If ObjFileAnnualDiscount.ProgramName.Length > 8 Then
                Dim _upLoaded As String = Right(ObjFileAnnualDiscount.ProgramName, 8)
                If _upLoaded = "Uploaded" Then
                    Dim _Result As String = Left(ObjFileAnnualDiscount.ProgramName, ObjFileAnnualDiscount.ProgramName.Length - 9)
                    _Programname.Text = _Result
                Else
                    _Programname.Text = ObjFileAnnualDiscount.ProgramName
                End If
            Else
                _Programname.Text = ObjFileAnnualDiscount.ProgramName
            End If
        End If
    End Sub
#End Region

End Class