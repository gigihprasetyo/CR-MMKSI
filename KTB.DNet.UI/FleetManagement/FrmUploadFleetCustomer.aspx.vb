Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.DealerReport
Imports Excel
Imports System.IO
Imports System.Text
Imports System.Reflection
Imports System.Collections.Generic


Public Class FrmUploadFleetCustomer
    Inherits System.Web.UI.Page

    Private sessHelper As New SessionHelper
    Private objFleetCustomer As FleetCustomer = New FleetCustomer
    Private objFleetHasilSurveyHeader As FleetHasilSurveyHeader = New FleetHasilSurveyHeader
    Private objFleetHasilSurveyDetail As FleetHasilSurveyDetail = New FleetHasilSurveyDetail
    Private objFleetCustomerToDealer As FleetCustomerToDealer = New FleetCustomerToDealer
    Private objCustomerGroup As CustomerGroup = New CustomerGroup
    Private facFleetCustomer As FleetCustomerFacade = New FleetCustomerFacade(User)
    Private facFleetCustomerToDealer As FleetCustomerToDealerFacade = New FleetCustomerToDealerFacade(User)
    Private facCustomerGroup As CustomerGroupFacade = New CustomerGroupFacade(User)
    Private facBusinessSectorDetail As VWI_BusinessSectorFacade = New VWI_BusinessSectorFacade(User)
    Private facCity As CityFacade = New CityFacade(User)
    Private objDealer As Dealer = New Dealer
    Private facDealer As DealerFacade = New DealerFacade(User)
    Private inputupload_privillage As Boolean
    Private strSessUploadExcel As String = "ExcelSession"
    Private strSessFleetCustomerToDealer As String = "FleetCustomerToDealer"
    Private strSessFleetCustomer As String = "FleetCustomer"
    Private strSessFleetCustomerGrid As String = "FleetCustomerGrid"
    Private strSessDealerGrid As String = "DealerGrid"
    Private strSessRemarksGrid As String = "RemarksGrid"
    Private _input As Boolean = False
    Private _edit As Boolean = False
    Private _view As Boolean = False

    Private _listOfTipePelanggan As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("EnumTipePelanggan")
    Private _listOfTipePerusahaan As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("EnumTipePerusahaan")
    Private _listOfIdentityType As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("EnumIdentityType")
    Private _listOfPreArea As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("EnumPreArea")



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            BindDataGrid()
            pnlGrid.Visible = False
        End If
    End Sub

    Private Sub CheckPrivilege()
        _view = SecurityProvider.Authorize(Context.User, SR.FleetCustomer_List_Privilege)
        If Not _view Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FLEET MANAGEMENT - Upload Fleet Customer")
        End If

        _input = SecurityProvider.Authorize(Context.User, SR.FleetCustomer_Input_Privilege)
        btnSave.Visible = _input
        btnUpload.Visible = _input
    End Sub

    Private Sub BindDataGrid()
        Dim arrListSession As ArrayList = CType(sessHelper.GetSession(strSessUploadExcel), ArrayList)

        If Not arrListSession Is Nothing Then

            dtgUploadFleetCustomer.DataSource = arrListSession
            dtgUploadFleetCustomer.DataBind()
            Dim col As DataGridColumn = dtgUploadFleetCustomer.Columns(dtgUploadFleetCustomer.Columns.Count - 1)
            col.ItemStyle.Width = Unit.Pixel(100)
            pnlGrid.Visible = True
        End If

    End Sub

    Private Function CheckExt(ByVal ext As String) As Boolean
        Dim retValue As Boolean = False
        If ext.ToUpper() = "XLS" Then
            retValue = True
        ElseIf ext.ToUpper() = "XLSX" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If UploadFile.PostedFile.FileName.Length > 0 Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            sapImp.Start()

            Try
                If UploadFile.PostedFile.ContentLength <> UploadFile.PostedFile.InputStream.Length Then
                    Throw New Exception("File Tidak Sama")
                End If

                Dim datetimenow As String = Now.ToString("yyyy_MM_dd_H_mm_ss")
                Dim directory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "FleetCustomer"
                Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directory)

                If Not directoryInfo.Exists Then
                    directoryInfo.Create()
                End If

                Dim ext As String = System.IO.Path.GetExtension(UploadFile.PostedFile.FileName)
                If Not CheckExt(ext.Substring(1)) Then
                    Throw New Exception("Salah Extention")
                End If

                Dim targetFile As String = Path.Combine(directory, datetimenow + "_" + Path.GetFileName(UploadFile.PostedFile.FileName))

                UploadFile.PostedFile.SaveAs(targetFile)

                Dim objExcelReader As IExcelDataReader = Nothing
                Dim arrListFleetCustomer As ArrayList = New ArrayList
                Dim arrListDealer As ArrayList = New ArrayList
                Dim arrListRemarks As ArrayList = New ArrayList
                Dim totalUpload As Integer = 0
                Dim i As Integer = 0

                Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)
                    If ext.ToLower.Contains("xlsx") Then
                        objExcelReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    Else
                        objExcelReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    End If

                    If Not IsNothing(objExcelReader) Then
                        Dim temp As String = String.Empty
                        Dim crt As CriteriaComposite
                        While objExcelReader.Read()
                            ' get value from excel and insert to fleet customer object
                            If i >= 2 Then
                                Dim strRemarks As String = String.Empty
                                Dim strValidate As String = String.Empty
                                objFleetCustomer = New FleetCustomer
                                objFleetCustomer.ID = i

                                ' preparing fleet code
                                Dim strCodeType As String = String.Empty
                                Dim strCodeProfil As String = String.Empty
                                Dim strCodeGroup As String = String.Empty

                                ' validate inputted data
                                Dim strValidateData As String = ValidateData(objExcelReader)
                                If strValidateData <> String.Empty Then
                                    strRemarks = strValidateData
                                End If

                                If IsEmptyLine(objExcelReader) Then
                                    Exit While
                                End If

                                strValidate = ValidateObject("category", objExcelReader.Item(1), objFleetCustomer)
                                If strValidate <> "-1" Then
                                    If strRemarks = String.Empty Then
                                        strRemarks = strValidate
                                    Else
                                        strRemarks += Environment.NewLine & strValidate
                                    End If
                                End If

                                'If strValidate = "-1" Then
                                '    objFleetCustomer.CategoryIndex = New EnumTipePelangganCustomerRequest().GetIndex(categoryItem)
                                '    strCodeType = GetStr(objExcelReader.Item(2))
                                '    If objExcelReader.Item(2) = "Perusahaan" Then
                                '        strCodeType = "CP"
                                '    ElseIf GetStr(objExcelReader.Item(2)) = "BUMN/Pemerintah" Then
                                '        strCodeType = "GV"
                                '    Else
                                '        strCodeType = "[Tipe]"
                                '    End If
                                'Else
                                '    strCodeType = "[Type]"
                                '    If strRemarks = String.Empty Then
                                '        strRemarks = strValidate
                                '    Else
                                '        strRemarks += Environment.NewLine & strValidate
                                '    End If
                                'End If

                                If objFleetCustomer.CategoryIndex = 1 Then
                                    ' if perusahaan maka tipe perusahaan is mandatory
                                    strValidate = ValidateObject("type", objExcelReader.Item(2), objFleetCustomer)
                                    If strValidate = "-1" Then
                                        objFleetCustomer.ClassificationIndex = -1
                                    Else
                                        objFleetCustomer.ClassificationIndex = -1
                                        objFleetCustomer.TypeIndex = -1

                                        If strRemarks = String.Empty Then
                                            strRemarks = strValidate
                                        Else
                                            strRemarks += Environment.NewLine & strValidate
                                        End If
                                    End If

                                ElseIf objFleetCustomer.CategoryIndex = 2 Then
                                    ' if pemerintah/bumn maka classification is mandatory
                                    strValidate = ValidateObject("classification", objExcelReader.Item(3), objFleetCustomer)
                                    If strValidate = "-1" Then
                                        objFleetCustomer.TypeIndex = -1
                                    Else
                                        objFleetCustomer.ClassificationIndex = -1
                                        objFleetCustomer.TypeIndex = -1

                                        If strRemarks = String.Empty Then
                                            strRemarks = strValidate
                                        Else
                                            strRemarks += Environment.NewLine & strValidate
                                        End If
                                    End If
                                End If

                                objFleetCustomer.Name = GetStr(objExcelReader.Item(4))

                                strValidate = ValidateObject("customergroup", GetStr(objExcelReader.Item(5)), objFleetCustomer)
                                If strValidate <> "-1" Then
                                    If strRemarks = String.Empty Then
                                        strRemarks = strValidate
                                    Else
                                        strRemarks += Environment.NewLine & strValidate
                                    End If
                                End If


                                strValidate = ValidateObject("businessSectorDetail", objExcelReader.Item(6), objFleetCustomer)
                                If strValidate <> "-1" Then
                                    If strRemarks = String.Empty Then
                                        strRemarks = strValidate
                                    Else
                                        strRemarks += Environment.NewLine & strValidate
                                    End If
                                End If

                                'Dim counterCode As String = objExcelReader.Item(1).ToString().Trim()
                                'If String.IsNullOrEmpty(counterCode) Then
                                '    counterCode = "[CounterCode]"
                                'End If

                                'objFleetCustomer.Code = strCodeType + strCodeProfil + strCodeGroup + counterCode.PadLeft(3, "0")
                                objFleetCustomer.Alamat = GetStr(objExcelReader.Item(7))
                                objFleetCustomer.Gedung = GetStr(objExcelReader.Item(8))

                                strValidate = ValidateObject("city", GetStr(objExcelReader.Item(9)), objFleetCustomer)
                                If strValidate <> "-1" Then
                                    If strRemarks = String.Empty Then
                                        strRemarks = strValidate
                                    Else
                                        strRemarks += Environment.NewLine & strValidate
                                    End If
                                End If

                                objFleetCustomer.Kecamatan = GetStr(objExcelReader.Item(10)).ToString().Trim()
                                objFleetCustomer.Kelurahan = GetStr(objExcelReader.Item(11)).ToString().Trim()

                                strValidate = ValidateObject("prearea", GetStr(objExcelReader.Item(12)), objFleetCustomer)
                                If strValidate <> "-1" Then
                                    If strRemarks = String.Empty Then
                                        strRemarks = strValidate
                                    Else
                                        strRemarks += Environment.NewLine & strValidate
                                    End If
                                End If

                                objFleetCustomer.Email = GetStr(objExcelReader.Item(13)).ToString().Trim()
                                objFleetCustomer.PhoneNo = GetStr(objExcelReader.Item(14)).ToString().Trim()
                                objFleetCustomer.PostalCode = GetStr(objExcelReader.Item(15)).ToString().Trim()

                                strValidate = ValidateObject("identity", GetStr(objExcelReader.Item(16)), objFleetCustomer)
                                If strValidate <> "-1" Then
                                    If strRemarks = String.Empty Then
                                        strRemarks = strValidate
                                    Else
                                        strRemarks += Environment.NewLine & strValidate
                                    End If
                                End If

                                objFleetCustomer.IdentityNumber = GetStr(objExcelReader.Item(17)).ToString().Trim()

                                If Not String.IsNullOrEmpty(objExcelReader.Item(18)) Then
                                    Dim dealer() As String = GetStr(objExcelReader.Item(18)).ToString().Split(";")
                                    If dealer.Length > 0 Then
                                        For idealer As Integer = 0 To dealer.Length - 1
                                            strValidate = ValidateObject("dealer", dealer(idealer), objFleetCustomer)
                                            If strValidate <> "-1" Then
                                                If strRemarks = String.Empty Then
                                                    strRemarks = strValidate
                                                Else
                                                    strRemarks += Environment.NewLine & strValidate
                                                End If
                                            End If
                                        Next
                                    End If
                                End If

                                ' validate code
                                Dim strValidateCode = facFleetCustomer.ValidateCode(objFleetCustomer.Code)
                                If strValidateCode <> String.Empty Then
                                    If strRemarks = String.Empty Then
                                        strRemarks = strValidateCode
                                    Else
                                        strRemarks += Environment.NewLine & strValidateCode
                                    End If
                                End If
                                objFleetCustomer.ErrorMessage = strRemarks
                                arrListFleetCustomer.Add(objFleetCustomer)
                                arrListDealer.Add(GetStr(objExcelReader.Item(17)))
                                arrListRemarks.Add(strRemarks)
                            End If

                            i = i + 1
                        End While
                        sessHelper.SetSession(strSessDealerGrid, arrListDealer)
                        sessHelper.SetSession(strSessRemarksGrid, arrListRemarks)
                        sessHelper.SetSession(strSessUploadExcel, arrListFleetCustomer)

                        BindDataGrid()
                    Else
                        ' show message if excel file isn't have value
                        MessageBox.Show("Data yang anda input kosong.")
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Gagal upload file.")
            End Try
            sapImp.StopImpersonate()
            sapImp = Nothing
        Else
            MessageBox.Show("Excel file is required.")
        End If
    End Sub


    Private Function GetStr(ByVal obj As Object) As String

        Try
            If obj Is Nothing Then

                Return String.Empty
            End If
            Return obj.ToString().Trim()

        Catch ex As Exception

        End Try
        Return String.Empty
    End Function
    Private Function ValidateData(ByVal objExcelReader As IExcelDataReader) As String
        Dim str = String.Empty

        ' validate fleet code number
        'If String.IsNullOrEmpty(objExcelReader.Item(1)) Then
        '    If str = String.Empty Then
        '        str = "Kode fleet konsumen tidak boleh kosong."
        '    Else
        '        str += Environment.NewLine + "Kode fleet customer tidak boleh kosong."
        '    End If
        'End If

        ' validate nama fleet
        If String.IsNullOrEmpty(objExcelReader.Item(4)) Then
            If str = String.Empty Then
                str = "Nama fleet konsumen tidak boleh kosong."
            Else
                str += Environment.NewLine + "Nama fleet konsumne tidak boleh kosong."
            End If
        End If

        Return str
    End Function


    Private Function IsEmptyLine(ByVal objExcelReader As IExcelDataReader) As Boolean

        Try
            For index As Integer = 1 To 17
                If Not (String.IsNullOrEmpty(objExcelReader.Item(index)) OrElse objExcelReader.Item(index).ToString().Trim() = "") Then
                    Return False
                End If
            Next
            Return True

        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Private Function ValidateObject(ByVal strObject As String, ByVal strCode As String, ByRef fleetCustomer As FleetCustomer) As String
        Dim crt As CriteriaComposite
        Dim count As Integer = 0
        strCode = strCode.Trim()
        Select Case strObject.ToLower()
            Case "category"
                For Each tipePelanggan As StandardCode In _listOfTipePelanggan
                    If tipePelanggan.ValueDesc = strCode Then
                        fleetCustomer.CategoryIndex = tipePelanggan.ValueId
                        fleetCustomer.TipeCustomer = tipePelanggan.ValueId
                        count += 1
                    End If
                Next
                If count = 0 Then
                    Return "Kode kategori tidak valid."
                End If
            Case "type"
                For Each tipePerusahaan As StandardCode In _listOfTipePerusahaan
                    If tipePerusahaan.ValueDesc = strCode Then
                        fleetCustomer.TypeIndex = tipePerusahaan.ValueId
                        count += 1
                    End If
                Next
                If count = 0 Then
                    Return "Tipe perusahaan tidak valid."
                End If
            Case "classification"
                Dim classification As ArrayList = New EnumMCPClassification().Retrieve()
                For Each Str As enumMCPClass In classification
                    If Str.NameStatus = strCode Then
                        fleetCustomer.ClassificationIndex = Str.ValStatus
                        count += 1
                    End If
                Next
                If count = 0 Then
                    Return "Klasifikasi tidak boleh kosong."
                End If
            Case "customergroup"
                crt = New CriteriaComposite(New Criteria(GetType(CustomerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(CustomerGroup), "Code", MatchType.Exact, strCode))
                Dim lstCustGroup = facCustomerGroup.Retrieve(crt)
                If lstCustGroup.Count = 0 Then
                    Return "Kode grup konsumen tidak ditemukan"
                Else
                    objFleetCustomer.CustomerGroup = lstCustGroup(0)
                End If
            Case "businessSectorDetail"
                crt = New CriteriaComposite(New Criteria(GetType(VWI_BusinessSector), "BusinessName", MatchType.Exact, strCode))
                Dim lstSectorDetail As ArrayList = facBusinessSectorDetail.Retrieve(crt)
                If lstSectorDetail.Count = 0 Then
                    Return "Sektor Bisnis tidak valid."
                Else
                    objFleetCustomer.BusinessSectorDetailId = CType(lstSectorDetail(0), BusinessSectorDetail).ID
                End If
            Case "city"
                crt = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, strCode))
                Dim lstCity As ArrayList = facCity.Retrieve(crt)
                If lstCity.Count = 0 Then
                    Return "Nama Kota tidak valid."
                Else
                    objFleetCustomer.City = lstCity(0)
                    objFleetCustomer.ProvinceID = objFleetCustomer.City.Province.ID
                End If
            Case "dealer"
                crt = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, strCode.Trim()))
                Dim lstDealer As ArrayList = facDealer.Retrieve(crt)
                If lstDealer.Count = 0 Then
                    Return "Kode dealer " + strCode + " tidak terdaftar."
                End If
            Case "identity"
                For Each idType As StandardCode In _listOfIdentityType
                    If idType.ValueDesc = strCode Then
                        fleetCustomer.IdentityType = idType.ValueId
                        count += 1
                    End If
                Next
                If count = 0 Then
                    Return "Tipe identitas tidak valid."
                End If
            Case "prearea"
                For Each prearea As StandardCode In _listOfPreArea
                    If prearea.ValueDesc = strCode Then
                        fleetCustomer.PreArea = prearea.ValueId
                        count += 1
                    End If
                Next
                If count = 0 Then
                    Return "Pre Area tidak valid."
                End If

        End Select
        Return "-1"
    End Function

    Protected Sub UploadTemplate_Click(sender As Object, e As EventArgs) Handles UploadTemplate.Click
        Response.Redirect("../downloadlocal.aspx?file=FleetManagement\UploadFleetCustomerTemplate.xls")
    End Sub

    Protected Sub dtgUploadFleetCustomer_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgUploadFleetCustomer.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "edit"
                ' edit here 
                dtgUploadFleetCustomer.EditItemIndex = CInt(e.Item.ItemIndex)
                BindDataGrid()
                btnSave.Visible = False
            Case "delete"
                ' delete here
                Dim arrList As ArrayList = sessHelper.GetSession(strSessUploadExcel)
                arrList.RemoveAt(CInt(e.Item.ItemIndex))
                sessHelper.SetSession(strSessUploadExcel, arrList)
                BindDataGrid()

            Case "update"
                UpdateCommand(e)

            Case "cancel"
                dtgUploadFleetCustomer.EditItemIndex = -1
                BindDataGrid()

        End Select
    End Sub

    Private Sub UpdateCommand(ByVal e As DataGridCommandEventArgs)
        Dim arrListDt As ArrayList = sessHelper.GetSession(strSessUploadExcel)
        Dim arrListDealer As ArrayList = sessHelper.GetSession(strSessDealerGrid)
        Dim arrListRemarks As ArrayList = sessHelper.GetSession(strSessRemarksGrid)
        Dim strValidate As String = String.Empty

        Dim lblCode As Label = CType(e.Item.FindControl("lblCodeEdit"), Label)
        Dim txtCode As TextBox = CType(e.Item.FindControl("txtCode"), TextBox)
        Dim ddlCategory As DropDownList = CType(e.Item.FindControl("ddlCategory"), DropDownList)
        Dim ddlType As DropDownList = CType(e.Item.FindControl("ddlType"), DropDownList)
        Dim ddlClassification As DropDownList = CType(e.Item.FindControl("ddlClassification"), DropDownList)
        Dim txtName As TextBox = CType(e.Item.FindControl("_ctl1"), TextBox)
        Dim ddlCustomerGroup As DropDownList = CType(e.Item.FindControl("ddlCustomerGroup"), DropDownList)
        Dim ddlBusinessSectorDetail As DropDownList = CType(e.Item.FindControl("ddlBusinessSectorDetail"), DropDownList)
        Dim txtAlamat As TextBox = CType(e.Item.FindControl("_ctl2"), TextBox)
        Dim txtGedung As TextBox = CType(e.Item.FindControl("_ctl3"), TextBox)
        Dim ddlCity As DropDownList = CType(e.Item.FindControl("ddlCity"), DropDownList)
        Dim txtKecamatan As TextBox = CType(e.Item.FindControl("_ctl4"), TextBox)
        Dim txtKelurahan As TextBox = CType(e.Item.FindControl("_ctl5"), TextBox)
        Dim txtEmail As TextBox = CType(e.Item.FindControl("_ctl6"), TextBox)
        Dim txtTlp As TextBox = CType(e.Item.FindControl("_ctl7"), TextBox)
        Dim txtPostalCode As TextBox = CType(e.Item.FindControl("_ctl8"), TextBox)
        Dim txtNpwp As TextBox = CType(e.Item.FindControl("_ctl9"), TextBox)
        Dim txtDealers As TextBox = CType(e.Item.FindControl("txtDealers"), TextBox)

        strValidate = ValidateUpdate(e)
        objFleetCustomer = arrListDt(e.Item.ItemIndex)
        'objFleetCustomer.Code = lblCode.Text + txtCode.Text

        Dim facFleetCustomer As FleetCustomerFacade = New FleetCustomerFacade(User)
        Dim strValidateCode As String = facFleetCustomer.ValidateCode(objFleetCustomer.Code)
        If strValidate = String.Empty Then
            strValidate = strValidateCode
        Else
            strValidate += Environment.NewLine + strValidateCode
        End If

        If ddlCategory.SelectedIndex <> 0 Then
            objFleetCustomer.CategoryIndex = CInt(ddlCategory.SelectedItem.Value)
            objFleetCustomer.TipeCustomer = CInt(ddlCategory.SelectedItem.Value)
        Else
            objFleetCustomer.CategoryIndex = -1
            objFleetCustomer.TipeCustomer = -1
        End If

        If ddlType.SelectedIndex <> 0 Then
            objFleetCustomer.TypeIndex = CInt(ddlType.SelectedItem.Value)
        Else
            objFleetCustomer.TypeIndex = -1
        End If

        If ddlClassification.SelectedIndex <> 0 Then
            objFleetCustomer.ClassificationIndex = CInt(ddlClassification.SelectedItem.Value)
        Else
            objFleetCustomer.ClassificationIndex = -1
        End If

        If ddlCustomerGroup.SelectedIndex <> 0 Then
            Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crt.opAnd(New Criteria(GetType(CustomerGroup), "Code", MatchType.Exact, ddlCustomerGroup.SelectedItem.Value))
            Dim arrCustomerGrp As ArrayList = facCustomerGroup.Retrieve(crt)
            If (arrCustomerGrp.Count > 0) Then
                objCustomerGroup = arrCustomerGrp(0)
                objFleetCustomer.CustomerGroup = New CustomerGroup(ID:=objCustomerGroup.ID)
            End If

        End If

        If ddlBusinessSectorDetail.SelectedIndex <> 0 Then
            Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VWI_BusinessSector), "ID", MatchType.Exact, ddlBusinessSectorDetail.SelectedItem.Value))
            Dim arrBusinessSectorDetail As ArrayList = facBusinessSectorDetail.Retrieve(crt)
            If (arrBusinessSectorDetail.Count > 0) Then
                Dim objBusinessSectorDetail As VWI_BusinessSector = arrBusinessSectorDetail(0)
                objFleetCustomer.BusinessSectorDetailId = objBusinessSectorDetail.ID
            End If

        End If

        If ddlCity.SelectedValue <> 0 Then
            objFleetCustomer.City = New City(ID:=CInt(ddlCity.SelectedItem.Value))
        Else
            objFleetCustomer.City = New City(ID:=CInt(0))
        End If

        objFleetCustomer.Name = txtName.Text
        objFleetCustomer.Alamat = txtAlamat.Text
        objFleetCustomer.Gedung = txtGedung.Text
        objFleetCustomer.Gedung = txtGedung.Text
        objFleetCustomer.Kecamatan = txtKecamatan.Text
        objFleetCustomer.Kelurahan = txtKelurahan.Text
        objFleetCustomer.Email = txtEmail.Text
        objFleetCustomer.PhoneNo = txtTlp.Text
        objFleetCustomer.PostalCode = txtPostalCode.Text

        'TODO : set IdentityType and IdentityNumber
        'objFleetCustomer.NPWP = txtNpwp.Text

        arrListDt(e.Item.ItemIndex) = objFleetCustomer
        arrListDealer(e.Item.ItemIndex) = txtDealers.Text
        arrListRemarks(e.Item.ItemIndex) = strValidate

        sessHelper.SetSession(strSessDealerGrid, arrListDealer)
        sessHelper.SetSession(strSessRemarksGrid, arrListRemarks)
        sessHelper.SetSession(strSessUploadExcel, arrListDt)

        dtgUploadFleetCustomer.EditItemIndex = -1
        BindDataGrid()
        btnSave.Visible = True

    End Sub

    Private Function ValidateUpdate(ByVal e As DataGridCommandEventArgs) As String
        Dim strValidate As String = String.Empty

        Dim ddlCategory As DropDownList = CType(e.Item.FindControl("ddlCategory"), DropDownList)
        Dim ddlType As DropDownList = CType(e.Item.FindControl("ddlType"), DropDownList)
        Dim ddlClassification As DropDownList = CType(e.Item.FindControl("ddlClassification"), DropDownList)
        Dim txtDealers As TextBox = CType(e.Item.FindControl("txtDealers"), TextBox)
        Dim txtName As TextBox = CType(e.Item.FindControl("_ctl1"), TextBox)
        Dim ddlBusinessSectorDetail As DropDownList = CType(e.Item.FindControl("ddlBusinessSectorDetail"), DropDownList)
        Dim ddlCity As DropDownList = CType(e.Item.FindControl("ddlCity"), DropDownList)

        If String.IsNullOrEmpty(txtName.Text) Then
            If String.IsNullOrEmpty(strValidate) Then
                strValidate = "Nama fleet konsumen tidak boleh kosong."
            Else
                strValidate += Environment.NewLine + "Nama fleet konsumen tidak boleh kosong."
            End If
        End If

        If ddlBusinessSectorDetail.SelectedIndex = 0 Then
            If String.IsNullOrEmpty(strValidate) Then
                strValidate = "Profil bisnis konsumen tidak boleh kosong."
            Else
                strValidate += Environment.NewLine + "Profil bisnis konsumen tidak boleh kosong."
            End If
        End If

        If ddlCity.SelectedValue = 0 Then
            If String.IsNullOrEmpty(strValidate) Then
                strValidate = "Nama kota tidak boleh kosong."
            Else
                strValidate += Environment.NewLine + "Nama kota tidak boleh kosong."
            End If
        End If

        If ddlCategory.SelectedIndex = 0 Then
            If String.IsNullOrEmpty(strValidate) Then
                strValidate = "Kategori tidak boleh kosong."
            Else
                strValidate += Environment.NewLine + "Kategori tidak boleh kosong."
            End If
        Else
            If ddlCategory.SelectedValue = "1" Then
                ' cek tipe perusahaan tidak boleh kosong
                If ddlType.SelectedIndex = 0 Then
                    If String.IsNullOrEmpty(strValidate) Then
                        strValidate = "Tipe perusahaan tidak boleh kosong."
                    Else
                        strValidate += Environment.NewLine + "Tipe perusahaan tidak boleh kosong."
                    End If
                End If
            ElseIf ddlCategory.SelectedValue = "2" Then
                ' cek classfifikasi tidak boleh kosong
                If ddlClassification.SelectedIndex = 0 Then
                    If String.IsNullOrEmpty(strValidate) Then
                        strValidate = "Klasifikasi tidak boleh kosong."
                    Else
                        strValidate += Environment.NewLine + "Klasifikasi tidak boleh kosong."
                    End If
                End If
            End If
        End If

        If Not String.IsNullOrEmpty(txtDealers.Text) Then
            Dim splitTxt() As String = txtDealers.Text.Split(";")
            For i As Integer = 0 To splitTxt.Length - 1
                Dim arrList As ArrayList = New ArrayList
                Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, splitTxt(i)))
                arrList = facDealer.RetrieveByCriteria(crt)
                If arrList.Count = 0 Then
                    If String.IsNullOrEmpty(strValidate) Then
                        strValidate = "Kode dealer " + splitTxt(i) + " tidak terdaftar."
                    Else
                        strValidate += Environment.NewLine + "Kode dealer " + splitTxt(i) + " tidak terdaftar."
                    End If
                End If
            Next
        End If

        Return strValidate
    End Function

    Private Sub RemoveALLSession()
        sessHelper.RemoveSession(strSessFleetCustomerToDealer)
        sessHelper.RemoveSession(strSessFleetCustomer)
        sessHelper.RemoveSession(strSessDealerGrid)
        sessHelper.RemoveSession(strSessRemarksGrid)
        sessHelper.RemoveSession(strSessUploadExcel)
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim strValidate As String = String.Empty
        Dim strValidateGrid As String = String.Empty
        Dim arrListFleet As ArrayList = sessHelper.GetSession(strSessUploadExcel)
        Dim newArrListFleet As ArrayList = sessHelper.GetSession(strSessUploadExcel)
        Dim arrListDealer As ArrayList = CType(sessHelper.GetSession(strSessDealerGrid), ArrayList)
        Dim arrListRemarks As ArrayList = sessHelper.GetSession(strSessRemarksGrid)
        Dim newArrListRemarks As ArrayList = sessHelper.GetSession(strSessRemarksGrid)
        Dim a As Integer = 1
        Dim arrIndex As ArrayList
        Dim Succes As Integer, Total As Integer, Invalid As Integer

        If 1 = 0 Then


            For x As Integer = 0 To arrListRemarks.Count - 1
                If Not String.IsNullOrEmpty(arrListRemarks(x)) Then
                    Dim barisKe As Integer = x + 1
                    If strValidate = String.Empty Then
                        strValidate = "Data baris ke-" + (x + 1).ToString + " belum sesuai."
                    Else
                        strValidate += "\nData baris ke-" + (x + 1).ToString + " belum sesuai."
                    End If
                End If
            Next

            If Not IsNothing(arrListDealer(0)) Then
                For y As Integer = 0 To arrListDealer.Count - 1
                    If Not (String.IsNullOrEmpty(arrListDealer(y).ToString)) Then
                        Dim strDealers() As String = arrListDealer(y).ToString().Split(";")

                        If strDealers.Length > 0 Then
                            For j As Integer = 0 To strDealers.Length - 1
                                Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                crt.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, strDealers(j).ToString().Trim()))
                                Dim arrDealers As ArrayList = facDealer.Retrieve(crt)
                                If arrDealers.Count = 0 Then
                                    Dim barisKe As Integer = y + 1
                                    If strValidate = String.Empty Then
                                        strValidate = "Kode dealer " + strDealers(j) + " pada data baris ke-" + barisKe.ToString() + " tidak terdaftar."
                                    Else
                                        strValidate += "\nKode dealer " + strDealers(j) + " pada data baris ke-" + barisKe.ToString() + " tidak terdaftar."
                                    End If
                                End If
                            Next
                        End If
                    End If
                Next
            End If
        End If


        If 1 = 1 OrElse strValidate = String.Empty Then
            Dim idx As Integer = -1
            Dim arrListFleetCustomer As ArrayList = New ArrayList

            For Each fleetCustomer As FleetCustomer In arrListFleet
                idx = idx + 1
                Total = Total + 1
                If Not String.IsNullOrEmpty(fleetCustomer.ErrorMessage) Then
                    arrListFleetCustomer.Add(fleetCustomer)
                    Invalid = Invalid + 1
                    Continue For
                End If
                If IsNothing(fleetCustomer.CustomerGroup) Then
                    fleetCustomer.CustomerGroup = New CustomerGroup(ID:=1)
                End If
                Dim fleetCustomerID As Integer = facFleetCustomer.Insert(fleetCustomer)

                If fleetCustomerID <> -1 Then
                    Dim fleetcust As FleetCustomer = facFleetCustomer.Retrieve(fleetCustomerID)
                    fleetcust.Code = String.Format("F{0}", fleetCustomerID.ToString().PadLeft(5, "0"))
                    facFleetCustomer.Update(fleetcust)
                    fleetCustomer.Code = fleetcust.Code

                    If Not IsNothing(arrListDealer(idx)) Then
                        '  For i As Integer = 0 To arrListDealer.Count - 1
                        If Not (String.IsNullOrEmpty(arrListDealer(idx).ToString)) Then
                            Dim strDealers() As String = arrListDealer(idx).ToString().Split(";")

                            If strDealers.Length > 0 Then
                                For j As Integer = 0 To strDealers.Length - 1
                                    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    crt.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, strDealers(j).Trim()))
                                    Dim arrDealers As ArrayList = facDealer.Retrieve(crt)
                                    If arrDealers.Count > 0 Then
                                        objDealer = arrDealers(0)
                                        objFleetCustomerToDealer = New FleetCustomerToDealer
                                        objFleetCustomerToDealer.FleetCustomerID = fleetCustomerID
                                        objFleetCustomerToDealer.DealerID = objDealer.ID

                                        facFleetCustomerToDealer.Insert(objFleetCustomerToDealer)
                                    End If
                                Next
                            End If

                        End If

                        '  Next
                    End If

                    ' insert visitasi 
                    ' data visitasi is mandatory pada modul make new visit
                    'GetFleetHasilSurverHeader(fleetCustomerID)
                    '' insert fleet hasil surver header
                    'Dim facFleetHasilSurveyHeader As New FleetHasilSurveyHeaderFacade(User)
                    'Dim facFleetHasilSurveyDetail As New FleetHasilSurveyDetailFacade(User)
                    'Dim fleetHasilSurveyHeaderID = facFleetHasilSurveyHeader.Insert(objFleetHasilSurveyHeader)
                    'If (fleetHasilSurveyHeaderID <> -1) Then
                    '    Dim listFHSDetail = GetFleetHasilSurveyDetail(fleetHasilSurveyHeaderID)
                    '    ' insert list fleet hasil survey detail
                    '    For Each objDetail As FleetHasilSurveyDetail In listFHSDetail
                    '        If (facFleetHasilSurveyDetail.Insert(objDetail) = -1) Then

                    '        End If
                    '    Next
                    'End If

                Else
                    'MessageBox.Show("Data gagal tersimpan.")
                    'Return
                End If
                Succes = Succes + 1
                arrListFleetCustomer.Add(fleetCustomer)
            Next

            btnSave.Visible = False
            btnBack.Visible = True

            For Each item As DataGridItem In dtgUploadFleetCustomer.Items
                Dim lbtnEdit As LinkButton = CType(item.FindControl("lbtnEdit"), LinkButton)
                Dim lbtnDelete As LinkButton = CType(item.FindControl("lbtnDelete"), LinkButton)
                If Not IsNothing(lbtnEdit) Then
                    lbtnEdit.Visible = False
                End If

                If Not IsNothing(lbtnDelete) Then
                    lbtnDelete.Visible = False
                End If
            Next

            sessHelper.SetSession(strSessDealerGrid, arrListDealer)
            sessHelper.SetSession(strSessRemarksGrid, arrListRemarks)
            sessHelper.SetSession(strSessUploadExcel, arrListFleetCustomer)

            BindDataGrid()

            MessageBox.Show(String.Format("Data Tersimpan {0} dari {1} baris", Succes, Total))


        Else
            MessageBox.Show(strValidate)
        End If

    End Sub

    'Private Function GetFleetHasilSurverHeader(ByVal fleetCustomerID As Integer)
    '    objFleetHasilSurveyHeader = New FleetHasilSurveyHeader()
    '    objFleetHasilSurveyHeader.FleetCustomerID = fleetCustomerID

    'End Function

    'Private Function GetFleetHasilSurveyDetail(ByVal fleetHasilSurveyHeaderID As Integer) As List(Of FleetHasilSurveyDetail)

    '    Dim facCompetitor As New CompetitorFacade(User)
    '    Dim facVehicleClass As New VehicleClassFacade(User)
    '    Dim list As New List(Of FleetHasilSurveyDetail)
    '    Dim arrList As New ArrayList

    '    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Competitor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    crt.opOr(New Criteria(GetType(Competitor), "ID", MatchType.Exact, 1))
    '    Dim sort As New SortCollection
    '    sort.Add(New Sort(GetType(Competitor), "Sequence", Search.Sort.SortDirection.ASC))
    '    arrList = facCompetitor.Retrieve(crt, sort)

    '    If arrList.Count > 0 Then
    '        For Each itemCompetitor As Competitor In arrList
    '            crt = New CriteriaComposite(New Criteria(GetType(VehicleClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '            crt.opAnd(New Criteria(GetType(VehicleClass), "Status", MatchType.Exact, 0))
    '            Dim arrListVehicleClass As New ArrayList
    '            arrListVehicleClass = facVehicleClass.Retrieve(crt)
    '            If arrListVehicleClass.Count > 0 Then
    '                For Each itemVehicleClass As VehicleClass In arrListVehicleClass
    '                    objFleetHasilSurveyDetail = New FleetHasilSurveyDetail
    '                    objFleetHasilSurveyDetail.FleetHasilSurveyHeaderID = fleetHasilSurveyHeaderID
    '                    objFleetHasilSurveyDetail.CompetitorID = itemCompetitor.ID
    '                    objFleetHasilSurveyDetail.VehicleClassID = itemVehicleClass.ID
    '                    objFleetHasilSurveyDetail.Amount = 0
    '                    objFleetHasilSurveyDetail.SelisihHasilSurvey = 0

    '                    list.Add(objFleetHasilSurveyDetail)
    '                Next
    '            End If

    '        Next

    '    End If

    '    Return list
    'End Function

    Private Sub dtgUploadFleetCustomer_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgUploadFleetCustomer.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            Dim rowValue As FleetCustomer = CType(e.Item.DataItem, FleetCustomer)
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then
                Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
                If Not IsNothing(rowValue.CategoryIndex) Then
                    Dim tipePelanggan As StandardCode = New StandardCodeFacade(User).GetByCategoryValue("EnumTipePelanggan", rowValue.CategoryIndex.ToString())
                    lblCategory.Text = tipePelanggan.ValueDesc

                    ' If tipePelanggan == 1 (Perusahaan)
                    If tipePelanggan.ValueId = 1 Then
                        Dim lblTypePerusahaan As Label = CType(e.Item.FindControl("lblTypePerusahaan"), Label)
                        If rowValue.TypeIndex <> -1 Then
                            Dim tipe As StandardCode = New StandardCodeFacade(User).GetByCategoryValue("EnumTipePerusahaan", rowValue.TypeIndex.ToString())
                            lblTypePerusahaan.Text = tipe.ValueDesc

                        End If
                    ElseIf tipePelanggan.ValueId = 2 Then
                        Dim lblClassification As Label = CType(e.Item.FindControl("lblClassification"), Label)
                        If rowValue.ClassificationIndex <> -1 Then
                            Dim mcp As enumMCPClass = New EnumMCPClassification().Retrieve.Item(rowValue.ClassificationIndex)
                            lblClassification.Text = mcp.NameStatus
                        End If
                    End If
                End If

                Dim lblCode As Label = CType(e.Item.FindControl("lblCode"), Label)
                If Not IsNothing(rowValue.Code) Then
                    lblCode.Text = rowValue.Code
                End If

                Dim lblPreArea As Label = CType(e.Item.FindControl("lblPreArea"), Label)
                If Not IsNothing(rowValue.Code) Then
                    Dim preArea As StandardCode = New StandardCodeFacade(User).GetByCategoryValue("EnumPreArea", rowValue.PreArea.ToString())
                    lblPreArea.Text = preArea.ValueDesc
                End If

                Dim lblCustomerGroup As Label = CType(e.Item.FindControl("lblCustomerGroup"), Label)
                If Not IsNothing(rowValue.CustomerGroup) Then
                    lblCustomerGroup.Text = rowValue.CustomerGroup.Code
                End If

                Dim lblBusinessSectorDetail As Label = CType(e.Item.FindControl("lblBusinessSectorDetail"), Label)
                If rowValue.BusinessSectorDetailId <> 0 Then
                    lblBusinessSectorDetail.Text = facBusinessSectorDetail.Retrieve(rowValue.BusinessSectorDetailId).ID
                End If

                Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
                If Not IsNothing(rowValue.City) Then
                    lblCity.Text = rowValue.City.CityName
                End If

                Dim lblDealers As Label = CType(e.Item.FindControl("lblDealers"), Label)
                Dim dealers As ArrayList = sessHelper.GetSession(strSessDealerGrid)
                If Not IsNothing(dealers) Then
                    lblDealers.Text = dealers(e.Item.ItemIndex)
                End If

                Dim lblRemarks As Label = CType(e.Item.FindControl("lblRemarks"), Label)
                Dim remarks As ArrayList = sessHelper.GetSession(strSessRemarksGrid)
                'If Not IsNothing(remarks) Then
                '    lblRemarks.Text = remarks(e.Item.ItemIndex)
                'End If

                If Not String.IsNullOrEmpty(rowValue.ErrorMessage) Then
                    lblRemarks.Text = rowValue.ErrorMessage
                End If

                'CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
            Else
                ' txt code fleet customer
                'Dim hdnType As HiddenField = CType(e.Item.FindControl("hdnType"), HiddenField)
                'hdnType.Value = "[Tipe]"
                'Dim hdnProfile As HiddenField = CType(e.Item.FindControl("hdnProfile"), HiddenField)
                'hdnProfile.Value = "[Profil]"
                'Dim hdnGroup As HiddenField = CType(e.Item.FindControl("hdnGroup"), HiddenField)
                'hdnGroup.Value = facCustomerGroup.Retrieve(1).Code
                'Dim lblCode As Label = CType(e.Item.FindControl("lblCodeEdit"), Label)
                'Dim txtCode As TextBox = CType(e.Item.FindControl("txtCode"), TextBox)

                'If Not String.IsNullOrEmpty(rowValue.Code) Then
                '    lblCode.Text = rowValue.Code.Substring(0, rowValue.Code.Length - 3)
                '    txtCode.Text = rowValue.Code.Substring(rowValue.Code.Length - 3, 3)
                'End If

                ' ddl category
                Dim ddlCategory As DropDownList = CType(e.Item.FindControl("ddlCategory"), DropDownList)
                ddlCategory.Items.Clear()
                ddlCategory.DataSource = _listOfTipePelanggan
                ddlCategory.DataTextField = "ValueDesc"
                ddlCategory.DataValueField = "ValueId"
                ddlCategory.DataBind()
                ddlCategory.Items.Insert(0, New ListItem("Silakan Pilih", -1))
                If rowValue.CategoryIndex <> -1 Then
                    ddlCategory.SelectedValue = rowValue.CategoryIndex
                    'Dim typeCategory As EnumTipePelanggan = New EnumTipePelangganCustomerRequest().RetrieveTypeFleet().Item(rowValue.CategoryIndex)
                    'hdnType.Value = typeCategory.NameTipe
                    'If rowValue.CategoryIndex = 1 Then
                    '    hdnType.Value = "CP"
                    'ElseIf rowValue.CategoryIndex = 2 Then
                    '    hdnType.Value = "GP"
                    'Else
                    '    hdnType.Value = "[Tipe]"
                    'End If
                Else
                    ddlCategory.SelectedIndex = 0
                End If

                Dim ddlType As DropDownList = CType(e.Item.FindControl("ddlType"), DropDownList)
                ddlType.Items.Clear()
                ddlType.DataSource = _listOfTipePerusahaan
                ddlType.DataTextField = "ValueDesc"
                ddlType.DataValueField = "ValueId"
                ddlType.DataBind()
                ddlType.Items.Insert(0, New ListItem("Silakan Pilih", -1))
                If rowValue.TypeIndex <> -1 Then
                    ddlType.SelectedValue = rowValue.TypeIndex

                Else
                    ddlType.SelectedIndex = -1
                End If

                ' ddlclassification
                Dim ddlClassification As DropDownList = CType(e.Item.FindControl("ddlClassification"), DropDownList)
                ddlClassification.Items.Clear()
                ddlClassification.DataSource = New EnumMCPClassification().Retrieve()
                ddlClassification.DataTextField = "NameStatus"
                ddlClassification.DataValueField = "ValStatus"
                ddlClassification.DataBind()
                ddlClassification.Items.Insert(0, New ListItem("Silakan Pilih", -1))
                If rowValue.ClassificationIndex <> -1 Then
                    ddlClassification.SelectedValue = rowValue.ClassificationIndex
                Else
                    ddlClassification.SelectedIndex = 0
                End If

                If rowValue.CategoryIndex = 1 Then
                    ddlClassification.Enabled = False
                    ddlClassification.SelectedIndex = 0
                    ddlType.Enabled = True
                ElseIf rowValue.CategoryIndex = 2 Then
                    ddlType.Enabled = False
                    ddlType.SelectedIndex = 0
                    ddlClassification.Enabled = True
                End If

                ' ddlcustomergroup
                Dim ddlCustomerGroup As DropDownList = CType(e.Item.FindControl("ddlCustomerGroup"), DropDownList)
                ddlCustomerGroup.Items.Clear()
                Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                ddlCustomerGroup.DataSource = New CustomerGroupFacade(User).Retrieve(crt)
                ddlCustomerGroup.DataTextField = "Description"
                ddlCustomerGroup.DataValueField = "Code"
                ddlCustomerGroup.DataBind()
                ddlCustomerGroup.Items.Insert(0, New ListItem("Silakan Pilih", 0))
                If Not IsNothing(rowValue.CustomerGroup) Then
                    ddlCustomerGroup.SelectedValue = rowValue.CustomerGroup.ID
                    'hdnGroup.Value = rowValue.CustomerGroup.Code

                Else
                    ddlCustomerGroup.SelectedIndex = 0
                End If

                ' ddlBusinessSectorDetail
                Dim ddlBusinessSectorDetail As DropDownList = CType(e.Item.FindControl("ddlBusinessSectorDetail"), DropDownList)
                ddlBusinessSectorDetail.Items.Clear()
                ddlBusinessSectorDetail.DataSource = New VWI_BusinessSectorFacade(User).RetrieveList()
                ddlBusinessSectorDetail.DataTextField = "BusinessName"
                ddlBusinessSectorDetail.DataValueField = "ID"
                ddlBusinessSectorDetail.DataBind()
                ddlBusinessSectorDetail.Items.Insert(0, New ListItem("Silakan Pilih", 0))
                If rowValue.BusinessSectorDetailId <> 0 Then
                    Dim objBusinessSectorDetail As VWI_BusinessSector = facBusinessSectorDetail.Retrieve(rowValue.BusinessSectorDetailId)
                    'hdnProfile.Value = objBusinessSectorDetail.Code
                    ddlBusinessSectorDetail.SelectedValue = objBusinessSectorDetail.ID
                Else
                    ddlBusinessSectorDetail.SelectedIndex = 0
                End If

                ' ddlcity
                Dim ddlCity As DropDownList = CType(e.Item.FindControl("ddlCity"), DropDownList)
                ddlCity.Items.Clear()
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

                ddlCity.DataSource = New CityFacade(User).RetrieveActiveList(criterias, "CityName", Sort.SortDirection.ASC)
                ddlCity.DataTextField = "CityName".ToUpper
                ddlCity.DataValueField = "ID"
                ddlCity.DataBind()
                If Not IsNothing(rowValue.City) Then
                    ddlCity.SelectedValue = rowValue.City.ID
                Else
                    ddlCity.SelectedValue = 0
                End If

                Dim txtDealers As TextBox = CType(e.Item.FindControl("txtDealers"), TextBox)
                Dim dealers As ArrayList = sessHelper.GetSession(strSessDealerGrid)
                If Not IsNothing(dealers) Then
                    txtDealers.Text = dealers(e.Item.ItemIndex)
                End If

                Dim lblRemarks As Label = CType(e.Item.FindControl("lblRemarksEdit"), Label)
                Dim remarks As ArrayList = sessHelper.GetSession(strSessRemarksGrid)
                If Not IsNothing(remarks) Then
                    lblRemarks.Text = remarks(e.Item.ItemIndex)
                End If
            End If
        End If
    End Sub

    Protected Sub ddlGrid_SelectedIndexChanged(sender As Object, e As EventArgs)
        For Each item As DataGridItem In dtgUploadFleetCustomer.Items
            If dtgUploadFleetCustomer.EditItemIndex = item.ItemIndex Then
                'Dim hdnType As HiddenField = CType(item.FindControl("hdnType"), HiddenField)
                'Dim hdnProfile As HiddenField = CType(item.FindControl("hdnProfile"), HiddenField)
                'Dim hdnGroup As HiddenField = CType(item.FindControl("hdnGroup"), HiddenField)
                Dim lblCode As Label = CType(item.FindControl("lblCodeEdit"), Label)
                Dim ddlCategory As DropDownList = CType(item.FindControl("ddlCategory"), DropDownList)
                Dim ddlType As DropDownList = CType(item.FindControl("ddlType"), DropDownList)
                Dim ddlClassification As DropDownList = CType(item.FindControl("ddlClassification"), DropDownList)
                Dim ddlCustomerGroup As DropDownList = CType(item.FindControl("ddlCustomerGroup"), DropDownList)
                Dim ddlBusinessSectorDetail As DropDownList = CType(item.FindControl("ddlBusinessSectorDetail"), DropDownList)

                If ddlCategory.SelectedIndex <> 0 Then
                    If ddlCategory.SelectedItem.Value = "1" Then ' 1 as index of perusahaan
                        ddlType.Enabled = True
                        ddlClassification.Enabled = False
                        ddlClassification.SelectedIndex = 0

                        'hdnType.Value = "CP"
                    ElseIf ddlCategory.SelectedItem.Value = "2" Then
                        ddlClassification.Enabled = True
                        ddlType.Enabled = False
                        ddlType.SelectedIndex = 0

                        'hdnType.Value = "GV"
                    Else
                        ddlClassification.Enabled = False
                        ddlType.Enabled = False
                        ddlClassification.SelectedIndex = 0
                        ddlType.SelectedIndex = 0
                        'hdnType.Value = "[Tipe]"
                    End If
                End If

                'If ddlCustomerGroup.SelectedIndex <> 0 Then
                '    hdnGroup.Value = ddlCustomerGroup.SelectedItem.Value
                'End If

                'If ddlBusinessSectorDetail.SelectedIndex <> 0 Then
                '    hdnProfile.Value = ddlBusinessSectorDetail.SelectedItem.Value
                'End If

                'lblCode.Text = hdnType.Value + hdnProfile.Value + hdnGroup.Value
            End If
        Next
    End Sub
    Private Function ValidateStandardCode(ByVal comparedBy As String, ByVal value As String, ByVal listOfStandardCode As ArrayList, ByVal FieldName As String) As String
        Dim result As String
        value = value.Trim()
        For Each standardCode As StandardCode In listOfStandardCode
            Dim scVal As String = String.Empty
            Select Case comparedBy
                Case "ValueId"
                    scVal = standardCode.ValueId.ToString()
                Case "ValueDesc"
                    scVal = standardCode.ValueDesc
            End Select

            If value = scVal Then
                Return "-1"
            End If
        Next
        Return FieldName + " tidak valid."
    End Function
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RemoveALLSession()
        Response.Redirect("FrmUploadFleetCustomer.aspx")
    End Sub

End Class