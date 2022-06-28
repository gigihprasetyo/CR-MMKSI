#Region "DNet Namespace Imports"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmUploadCustomer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlKategoriKonsumen As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icPeriodFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents valDealer As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnShow As System.Web.UI.WebControls.Button
    Protected WithEvents dgReports As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents fraDownload As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents fraDownload As System.Web.UI.HtmlControls.HtmlIframe
    Protected WithEvents hdnValSave As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblFilter As System.Web.UI.WebControls.Label
    Protected WithEvents lblFilterSep As System.Web.UI.WebControls.Label
    Protected WithEvents ddlFilter As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblASS As System.Web.UI.WebControls.Label
    Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
    Protected WithEvents LnkDownloadTemplate As System.Web.UI.WebControls.LinkButton

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private sessHelper As New SessionHelper
    Private _arlCcContacts As ArrayList = New ArrayList
#End Region

#Region "Event"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        lblMessage.Text = ""
        'InitiateAuthorization()
        If Not IsPostBack Then
            BindPeriod()
            BindDealer()
            BindKategoriKonsumen()
            dgReports.DataSource = New ArrayList
            dgReports.DataBind()
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub dgReports_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgReports.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Try
                Dim objContact As CcContact = e.Item.DataItem

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (dgReports.CurrentPageIndex * dgReports.PageSize + e.Item.ItemIndex + 1).ToString

                'Dim lblVehicleType As Label = CType(e.Item.FindControl("lblVehicleType"), Label)
                'lblVehicleType.Text = IIf(objContact.VehicleType.ToString = "1", "PC", "CV/LCV")

                Dim lblVehicleCategory As Label = CType(e.Item.FindControl("lblVehicleCategory"), Label)
                'lblVehicleCategory.Text = IIf(objContact.CcVehicleCategoryID.ToString = "1", "PC", "CV/LCV")
                Select Case objContact.CcVehicleCategoryID.ToString
                    Case "1"
                        lblVehicleCategory.Text = "PC"
                    Case "2"
                        lblVehicleCategory.Text = "LCV"
                    Case "3"
                        lblVehicleCategory.Text = "CV"
                    Case "4"
                        lblVehicleCategory.Text = "LCV/CV"
                End Select

                '
                Dim lblSexDesc As Label = CType(e.Item.FindControl("lblSexDesc"), Label)
                If objContact.Sex.Length > 0 Then
                    lblSexDesc.Text = IIf(objContact.Sex.ToString = "L", "Bpk", "Ibu")
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub dgReports_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgReports.PageIndexChanged
        Dim arlCcContact As ArrayList = New ArrayList
        Dim arlNewCcContact As ArrayList = New ArrayList
        Try
            Select Case ddlFilter.SelectedIndex
                Case 0
                    arlCcContact = sessHelper.GetSession("DataCustomerASS")
                Case 1
                    arlCcContact = sessHelper.GetSession("DataCustomerASSValid")
                Case 2
                    arlCcContact = sessHelper.GetSession("DataCustomerASSInValid")
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If Not arlCcContact Is Nothing Then
            arlNewCcContact = KTB.DNet.BusinessFacade.Helper.ArrayListPager.DoPage(arlCcContact, e.NewPageIndex, dgReports.PageSize)
            Me.dgReports.DataSource = arlNewCcContact
            Me.dgReports.VirtualItemCount = arlCcContact.Count
            Me.dgReports.CurrentPageIndex = e.NewPageIndex
            Me.dgReports.DataBind()
        End If
    End Sub

    Private Sub dgReports_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgReports.SortCommand
        If CType(viewstate("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    viewstate("CurrentSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    viewstate("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("CurrentSortColumn") = e.SortExpression
            viewstate("CurrentSortDirect") = Sort.SortDirection.DESC
        End If
        Dim arlCcContact As ArrayList = Session("DataCustomerASS")
        If Not arlCcContact Is Nothing Then
            SortListControl(arlCcContact, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            sessHelper.SetSession("DataCustomerASS", arlCcContact)
            BindUploadCustomerASS()
        End If
    End Sub

    Private Sub SortListControl(ByRef arlCcContact As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = True
        If SortDirection = Sort.SortDirection.ASC Then
            IsAsc = True
        ElseIf SortDirection = Sort.SortDirection.DESC Then
            IsAsc = False
        End If

        Dim objListComparer As IComparer = New KTB.DNet.BusinessFacade.Helper.ListComparer(IsAsc, SortColumn)
        arlCcContact.Sort(objListComparer)

    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        ddlFilter.SelectedIndex = 0
        Dim strPeriod As String = ddlYear.SelectedValue.ToString & IIf(ddlMonth.SelectedValue.ToString.Length = 1, "0" & ddlMonth.SelectedValue.ToString, ddlMonth.SelectedValue.ToString)
        Dim objCcPeriod As CcPeriod = New CcPeriodFacade(User).Retrieve(strPeriod)
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)

        Dim periodNow As String = DateTime.Now.ToString("yyyyMM")
        Dim func As New DRMasterGeneralFacade(Me.User)
        Dim objSetting As DRMasterGeneral = func.Retrieve("Cutt of Upload", 2)
        Dim maxDay As Integer = CInt(objSetting.GeneralValue)

        If CInt(objSetting.GeneralValue) = 0 Then
            maxDay = Date.DaysInMonth(CInt(strPeriod.Substring(0, 4)), CInt(strPeriod.Substring(4, 2)))
        End If

        Dim dtgMax As Date = New Date(CInt(strPeriod.Substring(0, 4)), CInt(strPeriod.Substring(4, 2)), maxDay)
        If dtgMax < New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) Then
            MessageBox.Show("Data yang diupload sudah melebihi Periode")
            Return
        End If


        If Not IsNothing(objCcPeriod) Then
            If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
                'cek maxFileSize first
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
                'Start  :Donas,20140603:failed to upload by dealer (specific)
                If 1 = 2 AndAlso DataFile.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
                            DataFile.PostedFile.ContentType.ToString <> "application/octet-stream" Then
                    'If Not (DataFile.PostedFile.ContentType.ToString = "application/vnd.ms-excel" Or _
                    '            DataFile.PostedFile.ContentType.ToString = "application/octet-stream" Or _
                    '            DataFile.PostedFile.ContentType.ToString = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Or _
                    '            DataFile.PostedFile.ContentType.ToString = "application/x-zip-compressed") Then
                    MessageBox.Show("Extension file tidak sesuai. Ubah ke *.xls (excel 2003).")
                    Exit Sub
                End If
                'Start  :Donas,20140603:failed to upload by dealer (specific)
                'remark by anh, interop dah pake 2007
                'Dim path As System.IO.Path
                'Dim ext As String = path.GetExtension(DataFile.PostedFile.FileName).Trim.ToLower

                'If ext <> ".xls" Then
                '    MessageBox.Show("Extension file " & DataFile.PostedFile.FileName & " tidak sesuai. Mohon convert file ke *.xls (excel 2003) terlebih dahulu.")
                '    Exit Sub
                'End If
                'End    : Donas,20140603:failed to upload by dealer (specific)
                If DataFile.PostedFile.ContentLength > maxFileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    Exit Sub
                End If

                Dim SrcFile As String = path.GetFileName(DataFile.PostedFile.FileName)   '-- Source file name
                SrcFile = New Date().Now.ToString("yyyyMMddhhmmss") & SrcFile
                'Dim DestFile As String = Server.MapPath("") & "\..\DataFile\" & SrcFile  '-- Destination file
                Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile  '-- Temporary file

                'Todo session
                'Session.Add("DestFile", DestFile)  '-- Store Destination file path into session
                'Todo session
                'Session.Add("TempFile", TempFile)  '-- Store Temporary file path into session


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
                        Dim parser As UploadCustomerCallCenter = New UploadCustomerCallCenter(objCcPeriod.YearMonth, objCcPeriod.ID, objDealer, DataFile.PostedFile.ContentType.ToString)

                        '-- Parse data file and store result into arraylist
                        'Dim arlCustomer As ArrayList = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1$]", "User"), ArrayList)
                        Dim arlCustomer As ArrayList = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1$A1:S152]", "User"), ArrayList)
                        If arlCustomer.Count > 0 Then
                            If parser.IsAllDataValid Then
                                btnSave.Enabled = True
                            Else
                                btnSave.Enabled = False
                            End If
                        Else
                            MessageBox.Show(parser.GetErrorMessage())
                        End If

                        sessHelper.SetSession("DataCustomerASS", arlCustomer)

                        dgReports.DataSource = Nothing  '-- Reset datagrid first
                        dgReports.CurrentPageIndex = 0
                        BindUploadCustomerASS()
                    End If

                Catch Exc As Exception
                    MessageBox.Show(SR.UploadFail(SrcFile))
                End Try
            Else
                MessageBox.Show(SR.FileNotSelected)
            End If
        Else
            MessageBox.Show("Periode survey tidak ada. Hubungi administrator")
        End If

    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        _arlCcContacts = New ArrayList
        _arlCcContacts = sessHelper.GetSession("DataCustomerASS")
        If _arlCcContacts.Count > 0 Then
            Dim _errMsg As StringBuilder = New StringBuilder
            Dim oFacade As New CcContactFacade(User)
            Dim iSuccess As Integer = 0
            For Each _contact As CcContact In _arlCcContacts
                If _contact.ErrorMessage = String.Empty Then
                    'Check existing
                    Dim isExist As Boolean = False
                    If _contact.HandphoneNo <> "" Then
                        Try
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "CcPeriodID", MatchType.Exact, _contact.CcPeriodID))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "CcCustomerCategoryID", MatchType.Exact, _contact.CcCustomerCategoryID))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "HandphoneNo", MatchType.Exact, _contact.HandphoneNo))
                            Dim arlContacts As ArrayList = oFacade.RetrieveByCriteria(criterias)
                            If arlContacts.Count > 0 Then
                                isExist = True
                                _errMsg.Append("no. handphone : " & _contact.HandphoneNo & ", sudah diupload \n")
                            End If
                        Catch ex As Exception

                        End Try

                    End If
                    If _contact.HomePhoneNo <> "" And isExist = False Then
                        Try
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "CcPeriodID", MatchType.Exact, _contact.CcPeriodID))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "CcCustomerCategoryID", MatchType.Exact, _contact.CcCustomerCategoryID))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "HomePhoneNo", MatchType.Exact, _contact.HomePhoneNo))
                            Dim arlContacts As ArrayList = oFacade.RetrieveByCriteria(criterias)
                            If arlContacts.Count > 0 Then
                                isExist = True
                                _errMsg.Append("no. rumah : " & _contact.HomePhoneAreaCode & _contact.HomePhoneNo & ", sudah diupload \n")
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                    If _contact.OfficePhoneNo <> "" And isExist = False Then
                        Try
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "CcPeriodID", MatchType.Exact, _contact.CcPeriodID))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "CcCustomerCategoryID", MatchType.Exact, _contact.CcCustomerCategoryID))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "OfficePhoneNo", MatchType.Exact, _contact.OfficePhoneNo))
                            Dim arlContacts As ArrayList = oFacade.RetrieveByCriteria(criterias)
                            If arlContacts.Count > 0 Then
                                isExist = True
                                _errMsg.Append("no. kantor : " & _contact.OfficePhoneAreaCode & _contact.OfficePhoneNo & ", sudah diupload \n")
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                    If Not isExist Then
                        'Save to CcContact
                        Dim _nResult As Integer = 0
                        _nResult = oFacade.Insert(_contact)
                        iSuccess = iSuccess + 1
                        If _nResult < 0 Then
                            _errMsg.Append("Nama konsumen" & _contact.ConsumerName & ", gagal disimpan \n")
                        End If
                    Else

                    End If

                End If
            Next

            If _errMsg.Length > 0 Then
                If iSuccess > 0 Then
                    btnSave.Enabled = False
                Else
                    btnSave.Enabled = True
                End If

                MessageBox.Show("Data berhasil disimpan : " & iSuccess.ToString & _
                    " dan " & (_arlCcContacts.Count - iSuccess).ToString & " data konsumen : " & _errMsg.ToString)
            Else
                btnSave.Enabled = False
                MessageBox.Show(SR.SaveSuccess)
            End If

            dgReports.DataSource = _arlCcContacts
            dgReports.DataBind()
        Else
            MessageBox.Show("Tidak ada data konsumen yang di upload")
        End If

    End Sub

    Private Sub ddlFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFilter.SelectedIndexChanged
        dgReports.CurrentPageIndex = 0
        BindUploadCustomerASS()
    End Sub

#End Region

#Region "Custom"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.BuatUploadPemesananOthers_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Spare Part - Upload Pemesanan")
        End If
    End Sub

    Private Sub BindPeriod()
        Try
            ddlMonth.Items.Clear()
            ddlMonth.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayMonth()
                item.Selected = False
                ddlMonth.Items.Add(item)
            Next
            ddlMonth.ClearSelection()
            ddlMonth.SelectedIndex = CType(Format(DateTime.Now, "MM").ToString, Integer)
        Catch ex As Exception
            MessageBox.Show("Error binding data bulan, silahkan kirim error ini ke dnet admin")
        End Try

        '--DropDownList Faktur Year
        Try
            ddlYear.Items.Clear()
            ddlYear.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayYear(True, 2, 8, Date.Now.Year.ToString)
                item.Selected = False
                ddlYear.Items.Add(item)
            Next
            ddlYear.ClearSelection()
            ddlYear.SelectedValue = Format(DateTime.Now, "yyyy").ToString
        Catch ex As Exception
            MessageBox.Show("Error binding data tahun, silahkan kirim error ini ke dnet admin")
        End Try

    End Sub

    Private Sub BindDealer()
        Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not IsNothing(objuser) Then
            lblDealer.Text = objuser.Dealer.DealerCode & " - " & objuser.Dealer.DealerName
        End If
    End Sub

    Private Sub BindKategoriKonsumen()

        ddlKategoriKonsumen.Items.Clear()

        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCustomerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCol.opAnd(New Criteria(GetType(CcCustomerCategory), "Code", MatchType.Exact, "ASS"))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(CcCustomerCategory), "ID", Sort.SortDirection.ASC))
        Dim objReportList As ArrayList = New CcCustomerCategoryFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each objReport As CcCustomerCategory In objReportList
            li = New ListItem(objReport.Description, objReport.ID.ToString)
            ddlKategoriKonsumen.Items.Add(li)
        Next
        'li = New ListItem("Silahkan pilih", "0")
        'ddlKategoriKonsumen.Items.Insert(0, li)

    End Sub

    Private Sub BindUploadCustomerASS()
        Dim totalRow As Integer = 0
        _arlCcContacts = New ArrayList
        Dim _arlValid As ArrayList = New ArrayList
        Dim _arlInValid As ArrayList = New ArrayList

        Try
            _arlCcContacts = sessHelper.GetSession("DataCustomerASS")

            For Each _c As CcContact In _arlCcContacts
                If _c.ErrorMessage = String.Empty Then
                    _arlValid.Add(_c)
                End If
            Next
            sessHelper.SetSession("DataCustomerASSValid", _arlValid)

            For Each _c As CcContact In _arlCcContacts
                If _c.ErrorMessage <> String.Empty Then
                    _arlInValid.Add(_c)
                End If
            Next
            sessHelper.SetSession("DataCustomerASSInValid", _arlInValid)

            If Not IsNothing(_arlCcContacts) Then
                Select Case ddlFilter.SelectedIndex
                    Case 0
                        btnSave.Enabled = True
                        totalRow = _arlCcContacts.Count
                        dgReports.DataSource = _arlCcContacts
                        Dim iError As Integer = 0
                        For Each _c As CcContact In _arlCcContacts
                            If _c.ErrorMessage <> String.Empty Then
                                btnSave.Enabled = False
                                Exit For
                            End If
                        Next

                    Case 1
                        'For Each _c As CcContact In _arlCcContacts
                        '    If _c.ErrorMessage = String.Empty Then
                        '        _arlValid.Add(_c)
                        '    End If
                        'Next
                        'sessHelper.SetSession("DataCustomerASSValid", _arlValid)
                        totalRow = _arlValid.Count
                        dgReports.DataSource = _arlValid
                        If totalRow > 0 Then
                            btnSave.Enabled = True
                        Else
                            btnSave.Enabled = False
                        End If
                    Case 2
                        'For Each _c As CcContact In _arlCcContacts
                        '    If _c.ErrorMessage <> String.Empty Then
                        '        _arlInValid.Add(_c)
                        '    End If
                        'Next
                        'sessHelper.SetSession("DataCustomerASSInValid", _arlInValid)
                        totalRow = _arlInValid.Count
                        dgReports.DataSource = _arlInValid
                        btnSave.Enabled = False

                End Select
                dgReports.VirtualItemCount = totalRow
                dgReports.DataBind()
                lblMessage.Text = "Jumlah data : " & _arlCcContacts.Count & " ( Valid : " & _arlValid.Count & " ; Tidak Valid : " & _arlInValid.Count & " )"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            lblMessage.Text = ""
        End Try

    End Sub
#End Region

    Protected Sub LnkDownloadTemplate_Click(sender As Object, e As EventArgs) Handles LnkDownloadTemplate.Click
        Dim strName As String = KTB.DNet.Lib.WebConfig.GetValue("FILE_DATA_ASS") '"DATA_PELANGGAN_ASS.xls"
        Response.Redirect("../downloadlocal.aspx?file=" & strName)
    End Sub
End Class
