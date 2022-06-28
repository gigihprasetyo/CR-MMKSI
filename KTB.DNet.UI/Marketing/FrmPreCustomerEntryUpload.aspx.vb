Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text

Public Class FrmPreCustomerEntryUpload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSalesmanID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSalesmanName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlFilter As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lbtnSearchSalesman As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    'Protected WithEvents lblDateFrom As System.Web.UI.WebControls.Label
    'Protected WithEvents lblDateUntil As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dgSAPCustomer As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtSalesmanCode As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtPeriod As System.Web.UI.HtmlControls.HtmlInputHidden
    'Protected WithEvents lblTotalRow As System.Web.UI.WebControls.Label
    Protected WithEvents btnNoSales As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
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

#Region "PrivateVariables"
    Private _SAPCustomerFacade As New SAPCustomerFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _arlCustomerAwal As ArrayList = New ArrayList
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper
#End Region

#Region "Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()


        If Not IsPostBack Then
            Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            lblDealerCode.Text = objuser.Dealer.DealerCode
            lblDealerName.Text = objuser.Dealer.DealerName
            If Request.QueryString("isBack") <> "1" Then
                Initialize()
                BindControlsAttribute()
                dgSAPCustomer.ShowFooter = True
            Else
                Dim arlDataGet As ArrayList = sessHelper.GetSession("DataForSAPReturn")
                txtSalesmanID.Text = arlDataGet(1)
                txtSalesmanName.Text = arlDataGet(2)
                'lblDateFrom.Text = arlDataGet(3)
                'lblDateUntil.Text = arlDataGet(4)
                txtSalesmanCode.Value = arlDataGet(3)
                txtPeriod.Value = arlDataGet(4)
                dgSAPCustomer.CurrentPageIndex = 0
                BindControlsAttribute()
                dgSAPCustomer.ShowFooter = True
            End If
        Else
            'Postback from jscript
            If Request("__EVENTARGUMENT") = "searchsalesman" Then
                btnSearch_Click(Me, System.EventArgs.Empty)
            End If
        End If

        txtSalesmanID.Attributes.Add("readonly", "readonly")
        txtSalesmanName.Attributes.Add("readonly", "readonly")
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        _arlCustomerAwal = sessHelper.GetSession("CustomerAwal")

        dgSAPCustomer.CurrentPageIndex = 0
        BindUploadCustomerAwal()
    End Sub

    Private Sub dgSAPCustomer_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSAPCustomer.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSAPCustomer As SAPCustomer = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSAPCustomer.CurrentPageIndex * dgSAPCustomer.PageSize)
            ' untuk bagian item / alternate item
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                ' mengisi value

                'Dim lbtnDeleteNew As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                'lbtnDeleteNew.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                'lbtnDeleteNew.CommandArgument = objSAPCustomer.ID

                Dim lblNameNew As Label = CType(e.Item.FindControl("lblName"), Label)
                'lblNameNew.Text = objSAPCustomer.SAPRegister.SalesmanHeader.Name

                Dim lblSalesmanCode As Label = CType(e.Item.FindControl("lblSalesmanCode"), Label)
                If Not IsNothing(objSAPCustomer.SalesmanHeader) Then
                    lblSalesmanCode.Text = objSAPCustomer.SalesmanHeader.SalesmanCode
                Else
                    lblSalesmanCode.Text = ""
                End If

                Dim lblVechileTypeCodeNew As Label = CType(e.Item.FindControl("lblVechileTypeCode"), Label)
                If Not IsNothing(objSAPCustomer.VechileType) Then
                    lblVechileTypeCodeNew.Text = objSAPCustomer.VechileType.VechileTypeCode
                End If

                Dim lblCustomerNameNew As Label = CType(e.Item.FindControl("lblCustomerName"), Label)
                lblCustomerNameNew.Text = objSAPCustomer.CustomerName

                Dim lblCustomerAddressNew As Label = CType(e.Item.FindControl("lblCustomerAddress"), Label)
                lblCustomerAddressNew.Text = objSAPCustomer.CustomerAddress

                Dim lblStatusNew As Label = CType(e.Item.FindControl("lblStatus"), Label)
                lblStatusNew.Text = CType(objSAPCustomer.Status, EnumSAPCustomerStatus.SAPCustomerStatus).ToString.Replace("_", " ")

                Dim lblPhone As Label = CType(e.Item.FindControl("lblPhone"), Label)
                lblPhone.Text = objSAPCustomer.Phone

                Dim lblGender As Label = CType(e.Item.FindControl("lblGender"), Label)
                lblGender.Text = EnumGender.GetStringGender(objSAPCustomer.Sex)

                Dim lblAge As Label = CType(e.Item.FindControl("lblAge"), Label)
                lblAge.Text = EnumAgeSegment.GetStringAgeSegment(objSAPCustomer.AgeSegment)

                Dim lblType As Label = CType(e.Item.FindControl("lblType"), Label)
                lblType.Text = EnumInformationType.GetStringInformationType(objSAPCustomer.InformationType)

                Dim lblPurpose As Label = CType(e.Item.FindControl("lblPurpose"), Label)
                lblPurpose.Text = EnumCustomerPurpose.GetStringCustomerPurpose(objSAPCustomer.CustomerPurpose)

                Dim lblSource As Label = CType(e.Item.FindControl("lblSource"), Label)
                lblSource.Text = EnumInformationSource.GetStringInformationSource(objSAPCustomer.InformationSource)

                Dim lblEstimatedCloseDate As Label = CType(e.Item.FindControl("lblEstimatedCloseDate"), Label)
                lblEstimatedCloseDate.Text = objSAPCustomer.EstimatedCloseDate

                Dim lblBirthDate As Label = CType(e.Item.FindControl("lblBirthDate"), Label)
                lblBirthDate.Text = objSAPCustomer.BirthDate

                Dim lblNote As Label = CType(e.Item.FindControl("lblNote"), Label)
                lblNote.Text = objSAPCustomer.Note

                Dim lblStatusCode As Label = CType(e.Item.FindControl("lblStatusCode"), Label)
                Dim criteriaLeadStatus As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "LeadStatusCode"))
                criteriaLeadStatus.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, objSAPCustomer.StatusCode))
                Dim leadStatusCodeList As ArrayList = New StandardCodeFacade(User).RetrieveByCriteria(criteriaLeadStatus)
                If leadStatusCodeList.Count > 0 Then
                    Dim leadStatusCode As StandardCode = leadStatusCodeList(0)
                    lblStatusCode.Text = leadStatusCode.ValueDesc
                End If

                Dim lblStateCode As Label = CType(e.Item.FindControl("lblStateCode"), Label)
                Dim criteriaLeadState As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "LeadStateCode"))
                criteriaLeadState.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, objSAPCustomer.StateCode))
                Dim leadStateCodeList As ArrayList = New StandardCodeFacade(User).RetrieveByCriteria(criteriaLeadState)

                If leadStateCodeList.Count > 0 Then
                    Dim leadStateCode As StandardCode = leadStateCodeList(0)
                    lblStateCode.Text = leadStateCode.ValueDesc
                End If


                Dim lblCampaignName As Label = CType(e.Item.FindControl("lblCampaignName"), Label)
                lblCampaignName.Text = objSAPCustomer.CampaignName

                Dim lblBusinessSector As Label = CType(e.Item.FindControl("lblBusinessSector"), Label)
                If Not IsNothing(objSAPCustomer.BusinessSectorDetail) Then
                    Dim sectorView As VWI_BusinessSector = New VWI_BusinessSectorFacade(User).Retrieve(objSAPCustomer.BusinessSectorDetail.ID)
                    lblBusinessSector.Text = sectorView.BusinessName
                Else
                    lblBusinessSector.Text = String.Empty
                End If

            End If
        End If
    End Sub

    Private Sub BindGender(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumGenderOp.RetriveSalesGender(True)
        For Each item As EnumGenderOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindAgeSegment(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumAgeSegmentOp.RetriveAgeSegment(True)
        For Each item As EnumAgeSegmentOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindInformationType(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumInformationTypeOp.RetriveInformationType(True)
        For Each item As EnumInformationTypeOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next

    End Sub

    Private Sub BindInformationSource(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumInformationSourceOp.RetriveInformationSource(True)
        For Each item As EnumInformationSourceOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindCustomerPurpose(ByVal ddl As DropDownList)
        Dim arrList As ArrayList = EnumCustomerPurposeOp.RetriveCustomerPurpose(True)
        For Each item As EnumCustomerPurposeOp In arrList
            Dim listItem As New ListItem(item.NameStatus, item.ValStatus)
            listItem.Selected = False
            ddl.Items.Add(listItem)
        Next
    End Sub

    Private Sub AddRawData(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs, Optional ByVal IsCopy As Boolean = False)
    End Sub

    Private Sub dgSAPCustomer_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSAPCustomer.ItemCommand
    End Sub

    Private Sub dgSAPCustomer_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSAPCustomer.PageIndexChanged
        Dim arlCcContact As ArrayList = New ArrayList
        Dim arlNewCcContact As ArrayList = New ArrayList
        Try
            Select Case ddlFilter.SelectedIndex
                Case 0
                    arlCcContact = sessHelper.GetSession("CustomerAwal")
                Case 1
                    arlCcContact = sessHelper.GetSession("CustomerAwalValid")
                Case 2
                    arlCcContact = sessHelper.GetSession("CustomerAwalInValid")
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If Not arlCcContact Is Nothing Then
            arlNewCcContact = KTB.DNet.BusinessFacade.Helper.ArrayListPager.DoPage(arlCcContact, e.NewPageIndex, dgSAPCustomer.PageSize)
            Me.dgSAPCustomer.DataSource = arlNewCcContact
            Me.dgSAPCustomer.VirtualItemCount = arlCcContact.Count
            Me.dgSAPCustomer.CurrentPageIndex = e.NewPageIndex
            Me.dgSAPCustomer.DataBind()
        End If

    End Sub

    Private Sub dgSAPCustomer_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSAPCustomer.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        End If
        Dim arlCcContact As ArrayList = Session("CustomerAwal")
        If Not arlCcContact Is Nothing Then
            SortListControl(arlCcContact, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            sessHelper.SetSession("CustomerAwal", arlCcContact)
            BindUploadCustomerAwal()
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
#End Region

#Region "Custom"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.CustomerProspekUpload_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Marketing - Upload Prospektif Konsumen")
        End If
    End Sub

    Private Function CekProspekCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.CustomerProspekCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub BindControlsAttribute()
        lblSearchSalesman.Attributes("onclick") = "ShowPopUpSAPRegisterSalesman();"
    End Sub

    Private Sub Initialize()
        ClearData()
        'ViewState("CurrentSortColumn") = "AreaCode"
        'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        'ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub ClearData()
        'txtSalesmanID.Enabled = True
        'txtSalesmanName.Enabled = True
        txtSalesmanID.Text = String.Empty
        txtSalesmanName.Text = String.Empty
        'lblDateFrom.Text = String.Empty
        'lblDateUntil.Text = String.Empty
        txtSalesmanCode.Value = String.Empty
        txtPeriod.Value = String.Empty

        'If dgSAPCustomer.Items.Count > 0 Then
        '    dgSAPCustomer.SelectedIndex = -1
        '    dgSAPCustomer.DataSource = New ArrayList
        '    dgSAPCustomer.DataBind()
        'End If
        dgSAPCustomer.DataSource = Nothing
        dgSAPCustomer.DataBind()
        lblSearchSalesman.Visible = True
        lblMessage.Text = String.Empty
    End Sub

#End Region

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        btnSearch.Enabled = True
    End Sub

    Private Sub btnNoSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoSales.Click
        ClearData()

        'txtSalesmanID.Enabled = False
        'txtSalesmanName.Enabled = False

        dgSAPCustomer.SelectedIndex = -1
        dgSAPCustomer.DataSource = New ArrayList
        dgSAPCustomer.DataBind()
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        ddlFilter.SelectedIndex = 0
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
            If 1 = 2 AndAlso DataFile.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
                        DataFile.PostedFile.ContentType.ToString <> "application/octet-stream" Then
                MessageBox.Show("Extension file tidak sesuai. Ubah ke *.xls (excel 2003).")
                Exit Sub
            End If
            If DataFile.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            End If

            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)   '-- Source file name
            SrcFile = New Date().Now.ToString("yyyyMMddhhmmss") & SrcFile
            Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile  '-- Temporary file

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
                    Dim parser As UploadCustomerEntryParser = New UploadCustomerEntryParser(objDealer, DataFile.PostedFile.ContentType.ToString)

                    Dim arlCustomerAwal As ArrayList = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1]", "User"), ArrayList)
                    If arlCustomerAwal.Count > 0 Then
                        If parser.IsAllDataValid Then
                            btnSave.Enabled = True
                        Else
                            btnSave.Enabled = False
                        End If
                    Else
                        MessageBox.Show(parser.GetErrorMessage())
                    End If

                    sessHelper.SetSession("CustomerAwal", arlCustomerAwal)

                    dgSAPCustomer.DataSource = Nothing  '-- Reset datagrid first
                    dgSAPCustomer.CurrentPageIndex = 0
                    BindUploadCustomerAwal()
                End If

            Catch Exc As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If
    End Sub

    Private Sub BindUploadCustomerAwal()
        Dim totalRow As Integer = 0
        _arlCustomerAwal = New ArrayList
        Dim _arlValid As ArrayList = New ArrayList
        Dim _arlInValid As ArrayList = New ArrayList

        Try
            _arlCustomerAwal = sessHelper.GetSession("CustomerAwal")

            If Not IsNothing(_arlCustomerAwal) Then
                For Each _c As SAPCustomer In _arlCustomerAwal
                    If _c.ErrorMessage = String.Empty Then
                        _arlValid.Add(_c)
                    End If
                Next
                sessHelper.SetSession("CustomerAwalValid", _arlValid)

                For Each _c As SAPCustomer In _arlCustomerAwal
                    If _c.ErrorMessage <> String.Empty Then
                        _arlInValid.Add(_c)
                    End If
                Next
                sessHelper.SetSession("CustomerAwalInValid", _arlInValid)

                Select Case ddlFilter.SelectedIndex
                    Case 0
                        btnSave.Enabled = True
                        totalRow = _arlCustomerAwal.Count
                        dgSAPCustomer.DataSource = _arlCustomerAwal
                        Dim iError As Integer = 0
                        For Each _c As SAPCustomer In _arlCustomerAwal
                            If _c.ErrorMessage <> String.Empty Then
                                btnSave.Enabled = False
                                Exit For
                            End If
                        Next

                    Case 1
                        totalRow = _arlValid.Count
                        dgSAPCustomer.DataSource = _arlValid
                        If totalRow > 0 Then
                            btnSave.Enabled = True
                        Else
                            btnSave.Enabled = False
                        End If
                    Case 2
                        totalRow = _arlInValid.Count
                        dgSAPCustomer.DataSource = _arlInValid
                        btnSave.Enabled = False

                End Select
                dgSAPCustomer.VirtualItemCount = totalRow
                dgSAPCustomer.DataBind()
                lblMessage.Text = "Jumlah data : " & _arlCustomerAwal.Count & " ( Valid : " & _arlValid.Count & " ; Tidak Valid : " & _arlInValid.Count & " )"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            lblMessage.Text = ""
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        _arlCustomerAwal = New ArrayList
        _arlCustomerAwal = sessHelper.GetSession("CustomerAwal")
        If Not IsNothing(_arlCustomerAwal) AndAlso (_arlCustomerAwal.Count > 0) Then
            Dim _errMsg As StringBuilder = New StringBuilder
            Dim oFacade As New SAPCustomerFacade(User)
            Dim iSuccess As Integer = 0
            Dim iFailed As Integer = 0
            For Each _Customer As SAPCustomer In _arlCustomerAwal
                If _Customer.ErrorMessage = String.Empty Then
                    'Check existing
                    Dim isExist As Boolean = False
                    If Not isExist Then
                        'Save to SAPCustomer
                        Dim _nResult As Integer = 0
                        _nResult = oFacade.Insert(_Customer)
                        iSuccess = iSuccess + 1
                        If _nResult > -2 Then
                            iFailed = iFailed + 1
                            '_errMsg.Append("Nama konsumen " & _Customer.CustomerName & ", gagal disimpan \n")
                        End If
                    Else

                    End If

                End If
            Next

            If iFailed > 0 Then
                If iSuccess > 0 Then
                    btnSave.Enabled = False
                Else
                    btnSave.Enabled = True
                End If

                MessageBox.Show("Data berhasil disimpan : " & iSuccess.ToString & "\n" & _
                    "Data gagal simpan " & (_arlCustomerAwal.Count - iSuccess).ToString)
            Else
                btnSave.Enabled = False
                MessageBox.Show(SR.SaveSuccess)
            End If

            dgSAPCustomer.DataSource = _arlCustomerAwal
            dgSAPCustomer.DataBind()
        Else
            MessageBox.Show("Tidak ada data konsumen yang di upload")
        End If
    End Sub

    Protected Sub LnkDownloadTemplate_Click(sender As Object, e As EventArgs) Handles LnkDownloadTemplate.Click
        Dim strName As String = KTB.DNet.Lib.WebConfig.GetValue("FILE_CUSTOMER_AWAL") '"CUSTOMER_AWAL.xls"
        Response.Redirect("../downloadlocal.aspx?file=" & strName)
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim arlData As ArrayList = New ArrayList
        If ddlFilter.SelectedValue = "Semua" Then
            arlData = CType(sessHelper.GetSession("CustomerAwal"), ArrayList)
        ElseIf ddlFilter.SelectedValue = "Valid" Then
            arlData = CType(sessHelper.GetSession("CustomerAwalValid"), ArrayList)
        ElseIf ddlFilter.SelectedValue = "Tidak Valid" Then
            arlData = CType(sessHelper.GetSession("CustomerAwalInValid"), ArrayList)
        End If

        If Not IsNothing(arlData) AndAlso (arlData.Count > 0) Then

            Dim sFileName As String = "Sheet1" '"CustomerAwal" & "_" & DateTime.Now.ToString("yyyyMMddhhmmss")
            Dim CustomerAwalData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

            '-- Impersonation to manipulate file in server
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Try
                If imp.Start() Then

                    Dim finfo As FileInfo = New FileInfo(CustomerAwalData)
                    If finfo.Exists Then
                        finfo.Delete()  '-- Delete temp file if exists
                    End If

                    Dim fs As FileStream = New FileStream(CustomerAwalData, FileMode.CreateNew)
                    Dim sw As StreamWriter = New StreamWriter(fs)

                    WriteData(sw, arlData)

                    sw.Close()
                    fs.Close()

                    imp.StopImpersonate()
                    imp = Nothing

                End If

                Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

            Catch ex As Exception
                MessageBox.Show(ex.Message) '"DownloadData data gagal")
            End Try
        Else
            MessageBox.Show("Tidak ada data yang di download ")
        End If

    End Sub

    Private Sub WriteData(ByRef sw As StreamWriter, ByVal arlData As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file

        If arlData.Count > 0 Then
            '-- Write column header
            itemLine.Remove(0, itemLine.Length)     '-- Empty line
            'itemLine.Append("No." & tab)
            itemLine.Append("Tipe Konsumen" & tab)
            itemLine.Append("Kode Salesman" & tab)
            itemLine.Append("Nama Konsumen" & tab)
            itemLine.Append("Alamat" & tab)
            itemLine.Append("Email" & tab)
            itemLine.Append("HP" & tab)
            itemLine.Append("Jenis Kelamin" & tab)
            itemLine.Append("Usia" & tab)
            itemLine.Append("Tanggal Lahir(YYYYMMDD)" & tab)
            itemLine.Append("Status" & tab)
            itemLine.Append("Sumber Informasi" & tab)
            itemLine.Append("Sumber Lead" & tab)
            itemLine.Append("Tujuan Konsumen" & tab)
            itemLine.Append("Vehicle Type" & tab)
            itemLine.Append("Qty" & tab)
            itemLine.Append("Tanggal Prospek(YYYYMMDD)" & tab)
            itemLine.Append("Curr. Vehicle Brand" & tab)
            itemLine.Append("Curr. Vehicle Type" & tab)
            itemLine.Append("Tgl. Rencana Pembelian (YYYYMMDD)" & tab)
            itemLine.Append("Status Follow Up" & tab)
            itemLine.Append("Status Akhir" & tab)
            itemLine.Append("Alasan Batal" & tab)
            itemLine.Append("Dealer Event" & tab)
            itemLine.Append("Industri" & tab)
            itemLine.Append("Keterangan" & tab)
            itemLine.Append("Note" & tab)

            sw.WriteLine(itemLine.ToString())

            Dim no As Integer = 0
            For Each detail As SAPCustomer In arlData
                itemLine.Remove(0, itemLine.Length)  '-- Empty line
                'itemLine.Append((no + 1).ToString() & tab)
                itemLine.Append(EnumTipePelangganCustomerRequest.RetrieveTipePelangganCustomerRequest(detail.CustomerType) & tab)
                If Not IsNothing(detail.SalesmanHeader) Then
                    itemLine.Append(detail.SalesmanHeader.SalesmanCode & tab)
                Else
                    itemLine.Append("" & tab)
                End If
                itemLine.Append(detail.CustomerName & tab)
                itemLine.Append(detail.CustomerAddress & tab)
                itemLine.Append(detail.Email & tab)
                itemLine.Append(detail.Phone & tab)
                itemLine.Append(EnumGender.GetStringGender(detail.Sex) & tab)
                itemLine.Append(EnumAgeSegment.GetStringAgeSegment(detail.AgeSegment) & tab)
                itemLine.Append(detail.BirthDate.ToString("yyyyMMdd") & tab)
                itemLine.Append(CType(detail.Status, EnumSAPCustomerStatus.SAPCustomerStatus).ToString.Replace("_", " ") & tab)
                itemLine.Append(EnumInformationType.GetStringInformationType(detail.InformationType) & tab)
                itemLine.Append(EnumInformationSource.GetStringInformationSource(detail.InformationSource) & tab)
                itemLine.Append(EnumCustomerPurpose.GetStringCustomerPurpose(detail.CustomerPurpose) & tab)
                If Not IsNothing(detail.VechileType) Then
                    itemLine.Append(detail.VechileType.VechileTypeCode & tab)
                Else
                    itemLine.Append("" & tab)
                End If
                itemLine.Append(detail.Qty & tab)
                itemLine.Append(detail.ProspectDate.ToString("yyyyMMdd") & tab)
                itemLine.Append(detail.CurrVehicleBrand & tab)
                itemLine.Append(detail.CurrVehicleType & tab)
                itemLine.Append(detail.EstimatedCloseDate.ToString("yyyyMMdd") & tab)

                Dim criteriaLeadStatus As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "LeadStatus"))
                criteriaLeadStatus.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, detail.LeadStatus))
                Dim leadStatusList As ArrayList = New StandardCodeFacade(User).RetrieveByCriteria(criteriaLeadStatus)
                If leadStatusList.Count > 0 Then
                    Dim leadStatus As StandardCode = leadStatusList(0)
                    itemLine.Append(leadStatus.ValueDesc & tab)
                Else
                    itemLine.Append("" & tab)
                End If

                Dim criteriaLeadState As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "LeadStateCode"))
                criteriaLeadState.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, detail.StateCode))
                Dim leadStateCodeList As ArrayList = New StandardCodeFacade(User).RetrieveByCriteria(criteriaLeadState)
                If leadStateCodeList.Count > 0 Then
                    Dim leadStateCode As StandardCode = leadStateCodeList(0)
                    itemLine.Append(leadStateCode.ValueDesc & tab)
                Else
                    itemLine.Append("" & tab)
                End If

                Dim criteriaLeadStatusCode As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "LeadStatusCode"))
                criteriaLeadStatusCode.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, detail.StatusCode))
                Dim leadStatusCodeList As ArrayList = New StandardCodeFacade(User).RetrieveByCriteria(criteriaLeadStatusCode)
                If leadStatusCodeList.Count > 0 Then
                    Dim leadStatusCode As StandardCode = leadStatusCodeList(0)
                    itemLine.Append(leadStatusCode.ValueDesc & tab)
                Else
                    itemLine.Append("" & tab)
                End If

                itemLine.Append(detail.CampaignName & tab)
                If Not IsNothing(detail.BusinessSectorDetail) Then
                    Dim sectorView As VWI_BusinessSector = New VWI_BusinessSectorFacade(User).Retrieve(detail.BusinessSectorDetail.ID)
                    itemLine.Append(sectorView.BusinessName & tab)
                Else
                    itemLine.Append("" & tab)
                End If

                itemLine.Append(detail.Description & tab)
                itemLine.Append(detail.Note & tab)
                If Not String.IsNullOrEmpty(detail.ErrorMessage) Then
                    itemLine.Append(detail.ErrorMessage.Replace(vbCr, "").Replace(vbLf, "").Trim() & tab)
                Else
                    itemLine.Append("" & tab)
                End If

                sw.WriteLine(itemLine.ToString())
                no = no + 1
            Next
        End If
    End Sub
End Class
