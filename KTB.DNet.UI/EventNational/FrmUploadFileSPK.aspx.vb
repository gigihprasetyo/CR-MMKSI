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
Imports OfficeOpenXml
Imports System.Linq
Imports System.Linq.Enumerable
Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class FrmUploadFileSPK
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    'Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    'Protected WithEvents lblMessage As System.Web.UI.WebControls.Label
    'Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    'Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    'Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    'Protected WithEvents txtSalesmanID As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtSalesmanName As System.Web.UI.WebControls.TextBox
    'Protected WithEvents ddlFilter As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents lblSearchSalesman As System.Web.UI.WebControls.Label
    'Protected WithEvents lbtnSearchSalesman As System.Web.UI.WebControls.LinkButton
    'Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    ''Protected WithEvents lblDateFrom As System.Web.UI.WebControls.Label
    ''Protected WithEvents lblDateUntil As System.Web.UI.WebControls.Label
    'Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    'Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    'Protected WithEvents dgNationalEventSPK As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents txtSalesmanCode As System.Web.UI.HtmlControls.HtmlInputHidden
    'Protected WithEvents txtPeriod As System.Web.UI.HtmlControls.HtmlInputHidden
    ''Protected WithEvents lblTotalRow As System.Web.UI.WebControls.Label
    'Protected WithEvents btnNoSales As System.Web.UI.WebControls.Button
    'Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    'Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    'Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    'Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
    'Protected WithEvents LnkDownloadTemplate As System.Web.UI.WebControls.LinkButton


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
    Private _arlNationalEventSPK As ArrayList = New ArrayList
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper
#End Region

#Region "Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()


        If Not IsPostBack Then
            Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            'lblDealerCode.Text = objuser.Dealer.DealerCode
            'lblDealerName.Text = objuser.Dealer.DealerName
            If Request.QueryString("isBack") <> "1" Then
                Initialize()
                BindControlsAttribute()
                dgNationalEventSPK.ShowFooter = True
            Else
                Dim arlDataGet As ArrayList = sessHelper.GetSession("DataForSAPReturn")
                'txtSalesmanID.Text = arlDataGet(1)
                'txtSalesmanName.Text = arlDataGet(2)
                'lblDateFrom.Text = arlDataGet(3)
                'lblDateUntil.Text = arlDataGet(4)
                txtSalesmanCode.Value = arlDataGet(3)
                'txtPeriod.Value = arlDataGet(4)
                dgNationalEventSPK.CurrentPageIndex = 0
                BindControlsAttribute()
                dgNationalEventSPK.ShowFooter = True
            End If
        Else
            'Postback from jscript
            If Request("__EVENTARGUMENT") = "searchsalesman" Then
                btnSearch_Click(Me, System.EventArgs.Empty)
            End If
        End If

        lblPopUpTO.Attributes("onclick") = "ShowPopUpTO()"
        'txtSalesmanName.Attributes.Add("readonly", "readonly")
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        _arlNationalEventSPK = sessHelper.GetSession("NationalEventSPK")

        dgNationalEventSPK.CurrentPageIndex = 0
        BindUploadNationalEventSPK()
    End Sub

    Private Sub dgNationalEventSPK_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgNationalEventSPK.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSPKNationalEvent As SPKNationalEvent = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgNationalEventSPK.CurrentPageIndex * dgNationalEventSPK.PageSize)
            'Dim objNationalEvent As NationalEvent = sessHelper.GetSession("NationalEvent")
            'objSPKNationalEvent.NationalEvent = objNationalEvent
            ' untuk bagian item / alternate item
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                ' mengisi value

                'Dim lbtnDeleteNew As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                'lbtnDeleteNew.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                'lbtnDeleteNew.CommandArgument = objSAPCustomer.ID

                Dim lblEvent As Label = CType(e.Item.FindControl("lblEvent"), Label)
                If Not IsNothing(objSPKNationalEvent.NationalEvent) AndAlso objSPKNationalEvent.NationalEvent.ID > 0 Then
                    lblEvent.Text = objSPKNationalEvent.NationalEvent.RegNumber & " - " & objSPKNationalEvent.NationalEvent.NationalEventType.Name & " " & objSPKNationalEvent.NationalEvent.NationalEventCity.City.CityName
                Else
                    lblEvent.Text = ""
                End If

                Dim lblSPKNumber As Label = CType(e.Item.FindControl("lblSPKNumber"), Label)
                lblSPKNumber.Text = objSPKNationalEvent.SPKNumber

                Dim lblDealerSPKDate As Label = CType(e.Item.FindControl("lblDealerSPKDate"), Label)
                lblDealerSPKDate.Text = objSPKNationalEvent.DealerSPKDate

                Dim lblCustomerName As Label = CType(e.Item.FindControl("lblCustomerName"), Label)
                lblCustomerName.Text = objSPKNationalEvent.CustomerName

                Dim lblNamaSales As Label = CType(e.Item.FindControl("lblNamaSales"), Label)
                If Not IsNothing(objSPKNationalEvent.SalesmanHeader) AndAlso objSPKNationalEvent.SalesmanHeader.ID > 0 Then
                    lblNamaSales.Text = objSPKNationalEvent.SalesmanHeader.Name
                Else
                    lblNamaSales.Text = objSPKNationalEvent.SalesmanName
                End If

                Dim lblSalesmanCode As Label = CType(e.Item.FindControl("lblSalesmanCode"), Label)
                If Not IsNothing(objSPKNationalEvent.SalesmanHeader) AndAlso objSPKNationalEvent.SalesmanHeader.ID > 0 Then
                    lblSalesmanCode.Text = objSPKNationalEvent.SalesmanHeader.SalesmanCode
                Else
                    lblSalesmanCode.Text = objSPKNationalEvent.SalesmanCode
                End If

                Dim lblVechileTypeCode As Label = CType(e.Item.FindControl("lblVechileTypeCode"), Label)
                If Not IsNothing(objSPKNationalEvent.VechileColor) AndAlso objSPKNationalEvent.VechileColor.ID > 0 Then
                    lblVechileTypeCode.Text = objSPKNationalEvent.VechileColor.VechileType.VechileModel.SubCategoryVehicleToModel.SubCategoryVehicle.Name
                Else
                    lblVechileTypeCode.Text = objSPKNationalEvent.VehicleType
                End If

                Dim lblVechileTypeDesc As Label = CType(e.Item.FindControl("lblVechileTypeDesc"), Label)
                If Not IsNothing(objSPKNationalEvent.VechileColor) AndAlso objSPKNationalEvent.VechileColor.ID > 0 Then
                    lblVechileTypeDesc.Text = objSPKNationalEvent.VechileColor.VechileType.Description
                Else
                    lblVechileTypeDesc.Text = objSPKNationalEvent.VehicleName
                End If

                Dim lblVechileColor As Label = CType(e.Item.FindControl("lblVechileColor"), Label)
                If Not IsNothing(objSPKNationalEvent.VechileColor) AndAlso objSPKNationalEvent.VechileColor.ID > 0 Then
                    lblVechileColor.Text = objSPKNationalEvent.VechileColor.ColorIndName
                Else
                    lblVechileColor.Text = objSPKNationalEvent.VehicleColor
                End If

                Dim lblQty As Label = CType(e.Item.FindControl("lblQty"), Label)
                lblQty.Text = objSPKNationalEvent.Quantity

                Dim lblDownPayment As Label = CType(e.Item.FindControl("lblDownPayment"), Label)
                lblDownPayment.Text = objSPKNationalEvent.DownPayment.ToString("c")

                Dim lblPaymentType As Label = CType(e.Item.FindControl("lblPaymentType"), Label)
                If Not IsNothing(objSPKNationalEvent.PaymentType) AndAlso objSPKNationalEvent.PaymentType.ID > 0 Then
                    lblPaymentType.Text = IIf(objSPKNationalEvent.PaymentType.ID = 2, "Kredit", "Tunai")
                Else
                    lblPaymentType.Text = objSPKNationalEvent.PaymentTypeTemp
                End If

                Dim lblLeasing As Label = CType(e.Item.FindControl("lblLeasing"), Label)
                If Not IsNothing(objSPKNationalEvent.Leasing) AndAlso objSPKNationalEvent.Leasing.ID > 0 Then
                    Dim leasingText As String = objSPKNationalEvent.Leasing.LeasingGroupName
                    lblLeasing.Text = leasingText
                Else
                    If lblPaymentType.Text = "Kredit" Then
                        lblLeasing.Text = objSPKNationalEvent.LeasingTemp
                    Else
                        lblLeasing.Text = "Tunai"
                    End If
                End If

                Dim lblRemarks As Label = CType(e.Item.FindControl("lblRemarks"), Label)
                lblRemarks.Text = objSPKNationalEvent.Remarks

                Dim lblShift As Label = CType(e.Item.FindControl("lblShift"), Label)
                lblShift.Text = objSPKNationalEvent.Shift
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

    Private Sub dgNationalEventSPK_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgNationalEventSPK.ItemCommand
    End Sub

    Private Sub dgNationalEventSPK_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgNationalEventSPK.PageIndexChanged
        Dim arlCcContact As ArrayList = New ArrayList
        Dim arlNewCcContact As ArrayList = New ArrayList
        Try
            Select Case ddlFilter.SelectedIndex
                Case 0
                    arlCcContact = sessHelper.GetSession("NationalEventSPK")
                Case 1
                    arlCcContact = sessHelper.GetSession("NationalEventSPKValid")
                Case 2
                    arlCcContact = sessHelper.GetSession("NationalEventSPKInValid")
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If Not arlCcContact Is Nothing Then
            arlNewCcContact = KTB.DNet.BusinessFacade.Helper.ArrayListPager.DoPage(arlCcContact, e.NewPageIndex, dgNationalEventSPK.PageSize)
            Me.dgNationalEventSPK.DataSource = arlNewCcContact
            Me.dgNationalEventSPK.VirtualItemCount = arlCcContact.Count
            Me.dgNationalEventSPK.CurrentPageIndex = e.NewPageIndex
            Me.dgNationalEventSPK.DataBind()
        End If

    End Sub

    Private Sub dgNationalEventSPK_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgNationalEventSPK.SortCommand
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
        Dim arlCcContact As ArrayList = Session("NationalEventSPK")
        If Not arlCcContact Is Nothing Then
            SortListControl(arlCcContact, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            sessHelper.SetSession("NationalEventSPK", arlCcContact)
            BindUploadNationalEventSPK()
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
        If Not SecurityProvider.Authorize(Context.User, SR.EVENTNASIONAL_UploadFileSPK_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT NASIONAL - UPLOAD FILE SPK")
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
        'lblSearchSalesman.Attributes("onclick") = "ShowPopUpSAPRegisterSalesman();"
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
        'txtSalesmanID.Text = String.Empty
        'txtSalesmanName.Text = String.Empty
        'lblDateFrom.Text = String.Empty
        'lblDateUntil.Text = String.Empty
        txtSalesmanCode.Value = String.Empty
        'txtPeriod.Value = String.Empty

        'If dgNationalEventSPK.Items.Count > 0 Then
        '    dgNationalEventSPK.SelectedIndex = -1
        '    dgNationalEventSPK.DataSource = New ArrayList
        '    dgNationalEventSPK.DataBind()
        'End If
        dgNationalEventSPK.DataSource = Nothing
        dgNationalEventSPK.DataBind()
        'lblSearchSalesman.Visible = True
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

        dgNationalEventSPK.SelectedIndex = -1
        dgNationalEventSPK.DataSource = New ArrayList
        dgNationalEventSPK.DataBind()
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        ddlFilter.SelectedIndex = 0
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumNationalEventFileSize"))
            If 1 = 2 AndAlso DataFile.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
                        DataFile.PostedFile.ContentType.ToString <> "application/octet-stream" Then
                MessageBox.Show("Extension file tidak sesuai. Ubah ke Microsoft Excel Worksheet (*.xlsx).")
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
                    Dim parser As UploadNationalEventSpkParser = New UploadNationalEventSpkParser(objDealer, DataFile.PostedFile.ContentType.ToString)

                    Dim arlNationalEventSPK As ArrayList = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1]", "User"), ArrayList)
                    If arlNationalEventSPK.Count > 0 Then
                        If parser.IsAllDataValid Then
                            btnSave.Enabled = True
                        Else
                            btnSave.Enabled = False
                        End If
                    Else
                        MessageBox.Show(parser.GetErrorMessage())
                    End If

                    sessHelper.SetSession("NationalEventSPK", arlNationalEventSPK)

                    dgNationalEventSPK.DataSource = Nothing  '-- Reset datagrid first
                    dgNationalEventSPK.CurrentPageIndex = 0
                    BindUploadNationalEventSPK()
                End If

                btnSave.Enabled = True
                btnSearch.Enabled = True
                btnDownload.Enabled = True
            Catch Exc As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If
    End Sub

    Private Sub BindUploadNationalEventSPK()
        Dim totalRow As Integer = 0
        _arlNationalEventSPK = New ArrayList
        Dim _arlValid As ArrayList = New ArrayList
        Dim _arlInValid As ArrayList = New ArrayList

        Try
            _arlNationalEventSPK = sessHelper.GetSession("NationalEventSPK")

            If Not IsNothing(_arlNationalEventSPK) Then
                For Each _c As SPKNationalEvent In _arlNationalEventSPK
                    If _c.ErrorMessage = String.Empty Then
                        _arlValid.Add(_c)
                    End If
                Next
                sessHelper.SetSession("NationalEventSPKValid", _arlValid)

                For Each _c As SPKNationalEvent In _arlNationalEventSPK
                    If _c.ErrorMessage <> String.Empty Then
                        _arlInValid.Add(_c)
                    End If
                Next
                sessHelper.SetSession("NationalEventSPKInValid", _arlInValid)

                Select Case ddlFilter.SelectedIndex
                    Case 0
                        btnSave.Enabled = True
                        totalRow = _arlNationalEventSPK.Count
                        dgNationalEventSPK.DataSource = _arlNationalEventSPK
                        Dim iError As Integer = 0
                        For Each _c As SPKNationalEvent In _arlNationalEventSPK
                            If _c.ErrorMessage <> String.Empty Then
                                btnSave.Enabled = False
                                Exit For
                            End If
                        Next

                    Case 1
                        totalRow = _arlValid.Count
                        dgNationalEventSPK.DataSource = _arlValid
                        If totalRow > 0 Then
                            btnSave.Enabled = True
                        Else
                            btnSave.Enabled = False
                        End If
                    Case 2
                        totalRow = _arlInValid.Count
                        dgNationalEventSPK.DataSource = _arlInValid
                        btnSave.Enabled = False

                End Select
                dgNationalEventSPK.VirtualItemCount = totalRow
                dgNationalEventSPK.DataBind()
                lblMessage.Text = "Jumlah data : " & _arlNationalEventSPK.Count & " ( Valid : " & _arlValid.Count & " ; Tidak Valid : " & _arlInValid.Count & " )"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            lblMessage.Text = ""
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        _arlNationalEventSPK = New ArrayList
        _arlNationalEventSPK = sessHelper.GetSession("NationalEventSPK")
        If Not IsNothing(_arlNationalEventSPK) AndAlso (_arlNationalEventSPK.Count > 0) Then
            Dim _errMsg As StringBuilder = New StringBuilder
            Dim oFacade As New SPKNationalEventFacade(User)
            Dim iSuccess As Integer = 0
            Dim iFailed As Integer = 0
            For Each _NationalEventSPK As SPKNationalEvent In _arlNationalEventSPK
                If _NationalEventSPK.ErrorMessage = String.Empty Then
                    'Check existing
                    Dim isExist As Boolean = False
                    If Not isExist Then
                        'Save to SPKNationalEvent
                        Dim _nResult As Integer = 0
                        _nResult = oFacade.Insert(_NationalEventSPK)
                        iSuccess = iSuccess + 1
                        If _nResult > -2 Then
                            iFailed = iFailed + 1
                            '_errMsg.Append("Nama konsumen " & _NationalEventSPK.CustomerName & ", gagal disimpan \n")
                        End If
                    Else

                    End If
                Else
                    iFailed = iFailed + 1
                End If
            Next

            If iFailed > 0 Then
                If iSuccess > 0 Then
                    btnSave.Enabled = False
                Else
                    btnSave.Enabled = True
                End If

                MessageBox.Show("Data berhasil disimpan : " & iSuccess.ToString & "\n" & _
                    "Data gagal simpan " & (_arlNationalEventSPK.Count - iSuccess).ToString)
            Else
                btnSave.Enabled = False
                MessageBox.Show(SR.SaveSuccess)
            End If

            dgNationalEventSPK.DataSource = _arlNationalEventSPK
            dgNationalEventSPK.DataBind()
        Else
            MessageBox.Show("Tidak ada data konsumen yang di upload")
        End If
    End Sub

    Public Function RetrieveDataSet(ByVal Sql As String) As DataSet
        Dim cmd As SqlClient.SqlCommand = New SqlClient.SqlCommand
        Dim con As SqlClient.SqlConnection
        Dim adp As SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim db As Database

        Try
            con = db.GetConnection
            con.Open()
            cmd.Connection = con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = Sql
            cmd.CommandTimeout = 600 '120 'in seconds
            adp = New SqlClient.SqlDataAdapter(cmd)
            adp.Fill(ds)
        Catch ex As Exception
            Dim s As String = ex.Message
        Finally
            con.Close()
            con.Dispose()
        End Try
        Return ds
    End Function

    Protected Sub GenerateTemplate(ByVal param As String, Optional ByVal arlData As ArrayList = Nothing)
        Dim wb As FileInfo = New FileInfo(Server.MapPath("~/DataFile/Babit/Template_NationalEvent_SPK.xlsx"))
        Using package As ExcelPackage = New ExcelPackage(wb)
            Dim qr As String = ""
            Dim tempdata As DataTable

            Dim wsLeasing As ExcelWorksheet = package.Workbook.Worksheets("LeasingType")
            Dim wsTanggalSPK As ExcelWorksheet = package.Workbook.Worksheets("TanggalSPK")
            Dim wsSales As ExcelWorksheet = package.Workbook.Worksheets("Sales")
            Dim wsKendaraan As ExcelWorksheet = package.Workbook.Worksheets("Kendaraan")

            Dim tempCell As String = ""

            Dim currentRow As Long = 0
            If Not IsNothing(hdnEventID.Value) AndAlso Not hdnEventID.Value.ToString = "" Then
                'Load Data Tanggal SPK
                Dim critNationalEvent As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critNationalEvent.opAnd(New Criteria(GetType(NationalEvent), "ID", MatchType.Exact, hdnEventID.Value))
                Dim arrNationalEvent As ArrayList = New NationalEventFacade(User).Retrieve(critNationalEvent)
                Dim objNationalEvent As NationalEvent = arrNationalEvent(0)
                wsTanggalSPK.Cells(2, 3).Value = objNationalEvent.ID.ToString

                currentRow = 2
                For Each Day As DateTime In Enumerable.Range(0, (objNationalEvent.PeriodEnd - objNationalEvent.PeriodStart).Days + 1).Select(Function(i) objNationalEvent.PeriodStart.AddDays(i))
                    wsTanggalSPK.Cells(currentRow, 1).Value = Day.ToString("yyyyMMdd")
                    currentRow += 1
                Next Day

                'Load berdasarkan National Event Detail
                Dim critNationalEventDetail As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critNationalEventDetail.opAnd(New Criteria(GetType(NationalEventDetail), "NationalEvent.ID", MatchType.Exact, objNationalEvent.ID))
                Dim arrNationalEventDetail As ArrayList = New NationalEventDetailFacade(User).Retrieve(critNationalEventDetail)

                If Not IsNothing(arrNationalEventDetail) AndAlso arrNationalEventDetail.Count > 0 Then
                    Dim salesID As String = ""

                    For Each objNationalEventDetail As NationalEventDetail In arrNationalEventDetail
                        salesID = salesID & ";" & objNationalEventDetail.SalesmanID
                    Next

                    Dim critSalesman As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critSalesman.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.No, ""))
                    critSalesman.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.InSet, "(2,4)"))
                    critSalesman.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.InSet, "('" & salesID.Replace(";", "','") & "')"))
                    Dim arrSalesman As ArrayList = New SalesmanHeaderFacade(User).Retrieve(critSalesman)

                    currentRow = 2
                    For Each objSalesman As SalesmanHeader In arrSalesman
                        wsSales.Cells(currentRow, 1).Value = objSalesman.Name & " " & objSalesman.SalesmanCode
                        wsSales.Cells(currentRow, 2).Value = objSalesman.SalesmanCode
                        wsSales.Cells(currentRow, 3).Value = objSalesman.ID
                        wsSales.Cells(currentRow, 4).Value = objSalesman.Dealer.ID
                        wsSales.Cells(currentRow, 5).Value = objSalesman.Dealer.SearchTerm1

                        currentRow += 1
                    Next
                Else
                    MessageBox.Show("Belum ada Dealer terdaftar.")
                End If

                Dim queryKendaraan = "select d.ID " &
                                    "from subcategoryvehicle a " &
                                    "join subcategoryvehicletomodel b on b.SubCategoryVehicleID = a.id " &
                                    "join vechileType c on c.ModelID = b.VechileModelID " &
                                    "join vechileColor d on d.VechileTypeID = c.ID " &
                                    "Where(a.rowstatus = 0 And b.rowstatus = 0 And c.rowstatus = 0 And d.rowstatus = 0) " &
                                    "and c.status = 'A' and c.categoryID IN(1,2) and d.status = '' "

                Dim critWarnaKendaraan As New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critWarnaKendaraan.opAnd(New Criteria(GetType(VechileColor), "ID", MatchType.InSet, "(" & queryKendaraan & ")"))

                Dim arrWarnaKendaraan As ArrayList = New VechileColorFacade(User).Retrieve(critWarnaKendaraan)

                currentRow = 2
                For Each objWarnaKendaraan As VechileColor In arrWarnaKendaraan
                    wsKendaraan.Cells(currentRow, 1).Value = objWarnaKendaraan.VechileType.VechileModel.SubCategoryVehicleToModel.SubCategoryVehicle.Name & " " & objWarnaKendaraan.VechileType.Description & " " & objWarnaKendaraan.ColorIndName & " " & objWarnaKendaraan.ID
                    wsKendaraan.Cells(currentRow, 2).Value = objWarnaKendaraan.ID

                    currentRow += 1
                Next

                Dim critPaymentType As New CriteriaComposite(New Criteria(GetType(PaymentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim arrPaymentType As ArrayList = New PaymentTypeFacade(User).Retrieve(critPaymentType)
                currentRow = 2
                For Each objPaymentType As PaymentType In arrPaymentType
                    wsLeasing.Cells(currentRow, 1).Value = objPaymentType.Description
                    wsLeasing.Cells(currentRow, 2).Value = objPaymentType.ID

                    currentRow += 1
                Next

                Dim critLeasing As New CriteriaComposite(New Criteria(GetType(Leasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim arrLeasing As ArrayList = New LeasingFacade(User).Retrieve(critLeasing)

                wsLeasing.Cells(2, 4).Value = "Tunai"
                wsLeasing.Cells(2, 5).Value = "10000"
                wsLeasing.Cells(2, 8).Value = "Tunai"

                currentRow = 3
                For Each objLeasing As Leasing In arrLeasing
                    wsLeasing.Cells(currentRow, 4).Value = objLeasing.LeasingName
                    wsLeasing.Cells(currentRow, 5).Value = objLeasing.ID

                    currentRow += 1
                Next

                If param = "WithData" Then
                    Dim wsSPK As ExcelWorksheet = package.Workbook.Worksheets("SPK")

                    If arlData.Count > 0 Then
                        currentRow = 2
                        wsSPK.Cells(1, 21).Value = "Info"
                        For Each detail As SPKNationalEvent In arlData
                            wsSPK.Cells(currentRow, 1).Value = currentRow - 1
                            wsSPK.Cells(currentRow, 2).Value = detail.SPKNumber
                            wsSPK.Cells(currentRow, 3).Value = detail.DealerSPKDate.ToString("yyyyMMdd")
                            wsSPK.Cells(currentRow, 4).Value = detail.CustomerName

                            If Not IsNothing(detail.SalesmanHeader) Then
                                wsSPK.Cells(currentRow, 5).Value = detail.SalesmanHeader.Name & " " & detail.SalesmanHeader.SalesmanCode
                            Else
                                wsSPK.Cells(currentRow, 5).Value = detail.SalesmanName
                            End If

                            If Not IsNothing(detail.VechileColor) Then
                                wsSPK.Cells(currentRow, 10).Value = detail.VechileColor.VechileType.VechileModel.SubCategoryVehicleToModel.SubCategoryVehicle.Name & " " & detail.VechileColor.VechileType.Description & " " & detail.VechileColor.ColorIndName & " " & detail.VechileColor.ID
                            Else
                                wsSPK.Cells(currentRow, 10).Value = detail.VehicleColor
                            End If

                            wsSPK.Cells(currentRow, 12).Value = detail.AssyYear
                            wsSPK.Cells(currentRow, 13).Value = detail.Quantity
                            wsSPK.Cells(currentRow, 14).Value = detail.DownPayment

                            If Not IsNothing(detail.PaymentType) Then
                                wsSPK.Cells(currentRow, 15).Value = detail.PaymentType.Description
                            Else
                                wsSPK.Cells(currentRow, 15).Value = detail.PaymentTypeTemp
                            End If

                            If Not IsNothing(detail.Leasing) Then
                                wsSPK.Cells(currentRow, 16).Value = detail.Leasing.LeasingName
                            Else
                                wsSPK.Cells(currentRow, 16).Value = detail.LeasingTemp
                            End If

                            wsSPK.Cells(currentRow, 18).Value = detail.Remarks
                            wsSPK.Cells(currentRow, 19).Value = hdnEventID.Value
                            wsSPK.Cells(currentRow, 20).Value = detail.Shift

                            If Not String.IsNullorEmpty(detail.ErrorMessage) Then
                                wsSPK.Cells(currentRow, 21).Value = detail.ErrorMessage.Replace(vbCr, "").Replace(vbLf, "").Trim()
                            End If

                            currentRow += 1
                        Next
                    End If
                End If

                Response.Clear()
                Response.AddHeader("content-disposition", "attachment;filename=TemplateUploadNationalEventSPK.xlsx")
                Response.Charset = ""
                Me.EnableViewState = False
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.BinaryWrite(package.GetAsByteArray())
                Response.End()
            Else
                MessageBox.Show("Pilih Kode Event")
            End If
        End Using
    End Sub

    Protected Sub LnkDownloadTemplate_Click(sender As Object, e As EventArgs) Handles LnkDownloadTemplate.Click
        'Dim strName As String = KTB.DNet.Lib.WebConfig.GetValue("FILE_CUSTOMER_AWAL") '"CUSTOMER_AWAL.xls"
        'Response.Redirect("../downloadlocal.aspx?file=DataFile\Babit\Template_NationalEvent_SPK.xlsx")

        Try
            GenerateTemplate("TemplateOnly")
            
        Catch ex As Exception
            MessageBox.Show(ex.Message) '"DownloadData data gagal")

        End Try

    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim arlData As ArrayList = New ArrayList
        If ddlFilter.SelectedValue = "Semua" Then
            arlData = CType(sessHelper.GetSession("NationalEventSPK"), ArrayList)
        ElseIf ddlFilter.SelectedValue = "Valid" Then
            arlData = CType(sessHelper.GetSession("NationalEventSPKValid"), ArrayList)
        ElseIf ddlFilter.SelectedValue = "Tidak Valid" Then
            arlData = CType(sessHelper.GetSession("NationalEventSPKInValid"), ArrayList)
        End If

        If Not IsNothing(arlData) AndAlso (arlData.Count > 0) Then

            Dim sFileName As String = "NationalEventSPK" & "_" & DateTime.Now.ToString("yyyyMMddhhmmss")
            Dim NationalEventSPKData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

            '-- Impersonation to manipulate file in server
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Try
                If imp.Start() Then

                    Dim finfo As FileInfo = New FileInfo(NationalEventSPKData)
                    If finfo.Exists Then
                        finfo.Delete()  '-- Delete temp file if exists
                    End If

                    Dim fs As FileStream = New FileStream(NationalEventSPKData, FileMode.CreateNew)
                    Dim sw As StreamWriter = New StreamWriter(fs)

                    WriteData(sw, arlData)

                    sw.Close()
                    fs.Close()

                    imp.StopImpersonate()
                    imp = Nothing

                End If

                'Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

            Catch ex As Exception
                MessageBox.Show(ex.Message) '"DownloadData data gagal")
            End Try
        Else
            MessageBox.Show("Tidak ada data yang di download ")
        End If

    End Sub

    Private Sub WriteData(ByRef sw As StreamWriter, ByVal arlData As ArrayList)

        GenerateTemplate("WithData", arlData)

        'Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        'Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file

        'If arlData.Count > 0 Then
        '    '-- Write column header
        '    itemLine.Remove(0, itemLine.Length)     '-- Empty line
        '    'itemLine.Append("No." & tab)
        '    itemLine.Append("No" & tab)
        '    itemLine.Append("No SPK" & tab)
        '    itemLine.Append("Tanggal SPK (DDMMYYYY)" & tab)
        '    itemLine.Append("Nama Konsumen" & tab)
        '    itemLine.Append("Group Dealer" & tab)
        '    itemLine.Append("Nama Dealer" & tab)
        '    itemLine.Append("Kode Dealer" & tab)
        '    itemLine.Append("Kota" & tab)
        '    itemLine.Append("Nama Sales" & tab)
        '    itemLine.Append("Kode Sales" & tab)
        '    itemLine.Append("Tipe Kendaraan" & tab)
        '    itemLine.Append("Nama Kendaraan" & tab)
        '    itemLine.Append("Warna Kendaraan" & tab)
        '    itemLine.Append("Tahun Produksi (YYYY)" & tab)
        '    itemLine.Append("Quantity" & tab)
        '    itemLine.Append("Tanda Jadi" & tab)
        '    itemLine.Append("Type Pembiayaan" & tab)
        '    itemLine.Append("Lembaga Pembiayaan" & tab)
        '    itemLine.Append("Remark" & tab)
        '    itemLine.Append("Info" & tab)

        '    sw.WriteLine(itemLine.ToString())

        '    Dim no As Integer = 0
        '    For Each detail As SPKNationalEvent In arlData
        '        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        '        itemLine.Append((no + 1).ToString() & tab)
        '        itemLine.Append(detail.SPKNumber & tab)
        '        itemLine.Append(detail.DealerSPKDate.ToString("yyyyMMdd") & tab)
        '        itemLine.Append(detail.CustomerName & tab)
        '        If Not IsNothing(detail.Dealer) Then
        '            itemLine.Append(detail.Dealer.DealerGroup.GroupName & tab)
        '            itemLine.Append(detail.Dealer.DealerName & tab)
        '            itemLine.Append(detail.Dealer.DealerCode & tab)
        '            itemLine.Append(detail.Dealer.City.CityName & tab)
        '        Else
        '            itemLine.Append(detail.GroupDealer & tab)
        '            itemLine.Append(detail.DealerName & tab)
        '            itemLine.Append(detail.DealerCode & tab)
        '            itemLine.Append(detail.DealerCity & tab)
        '        End If

        '        If Not IsNothing(detail.SalesmanHeader) Then
        '            itemLine.Append(detail.SalesmanHeader.Name & tab)
        '            itemLine.Append(detail.SalesmanHeader.SalesmanCode & tab)
        '        Else
        '            itemLine.Append(detail.SalesmanName & tab)
        '            itemLine.Append(detail.SalesmanCode & tab)
        '        End If

        '        If Not IsNothing(detail.VechileColor) Then
        '            itemLine.Append(detail.VechileColor.VechileType.VechileModel.SubCategoryVehicleToModel.SubCategoryVehicle.Name & tab)
        '            itemLine.Append(detail.VechileColor.VechileType.Description & tab)
        '            itemLine.Append(detail.VechileColor.ColorIndName & tab)
        '        Else
        '            itemLine.Append(detail.VehicleType & tab)
        '            itemLine.Append(detail.VehicleName & tab)
        '            itemLine.Append(detail.VehicleColor & tab)
        '        End If
        '        itemLine.Append(detail.AssyYear & tab)
        '        itemLine.Append(detail.Quantity & tab)
        '        itemLine.Append(detail.DownPayment & tab)

        '        If Not IsNothing(detail.PaymentType) Then
        '            itemLine.Append(detail.PaymentType.Description & tab)
        '        Else
        '            itemLine.Append(detail.PaymentTypeTemp & tab)
        '        End If

        '        If Not IsNothing(detail.Leasing) Then
        '            itemLine.Append(detail.Leasing.LeasingName & tab)
        '        Else
        '            itemLine.Append(detail.LeasingTemp & tab)
        '        End If

        '        itemLine.Append(detail.Remarks & tab)

        '        If Not String.IsNullorEmpty(detail.ErrorMessage) Then
        '            itemLine.Append(detail.ErrorMessage.Replace(vbCr, "").Replace(vbLf, "").Trim() & tab)
        '        Else
        '            itemLine.Append("" & tab)
        '        End If
        '        sw.WriteLine(itemLine.ToString())
        '        no = no + 1
        '    Next
        'End If
    End Sub

    Protected Sub txtKodeEvent_TextChanged(sender As Object, e As EventArgs) Handles txtKodeEvent.TextChanged
        Dim objNationalEvent As NationalEvent = New NationalEventFacade(User).Retrieve(txtKodeEvent.Text)
        If Not IsNothing(objNationalEvent) AndAlso objNationalEvent.ID > 0 Then
            hdnEventID.Value = objNationalEvent.ID
            lblNamaEvent.Text = objNationalEvent.NationalEventType.Name & " " & objNationalEvent.NationalEventCity.City.CityName
            icPeriodeStart.Value = objNationalEvent.PeriodStart
            icPeriodeEnd.Value = objNationalEvent.PeriodEnd
            sessHelper.SetSession("NationalEvent", objNationalEvent)
            btnUpload.Enabled = True
        Else
            MessageBox.Show("Kode Event salah.")
        End If
    End Sub
End Class