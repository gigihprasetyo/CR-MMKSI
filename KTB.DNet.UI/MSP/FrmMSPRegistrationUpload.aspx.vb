Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade
Imports Excel
Imports System.IO
Imports System.Text
Imports System.Reflection
Imports System.Collections.Generic
Imports KTB.DNet.UI.Helper
Imports System.Linq
Imports System.Net.Mail

Public Class FrmMSPRegistrationUpload
    Inherits System.Web.UI.Page

    Private sessHelper As New SessionHelper
    Private objCity As New City
    Private facCity As New CityFacade(User)
    Private objDealer As New Dealer
    Private facDealer As New DealerFacade(User)
    Private objMSPReg As New MSPRegistration
    Private objMSPRegHistory As New MSPRegistrationHistory
    Private objMSPCustomer As New MSPCustomer
    Private inputupload_privillage As Boolean
    Private strSessUploadExcel As String = "ExcelSession"
    Private strSessMSPCustomer As String = "strSessMSPCustomer"
    Private strSessNewArrayRemarks As String = "strSessNewArrayRemarks"
    Private strSessNewArrayBenefitType As String = "strSessNewArrayBenefitType"
    Private strSessNewArrayMSPCustomer As String = "strSessNewArrayMSPCustomer"
    Private _input As Boolean = False
    Private _edit As Boolean = False
    Private _view As Boolean = False
    Private arr As New ArrayList
    Private arrMSPCustomer As New ArrayList
    Private crt As CriteriaComposite

    Private Sub CheckPrivilege()
        _view = SecurityProvider.Authorize(Context.User, SR.MSPMaster_upload_mks_Privilege)
        If Not _view Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MSP - Upload Registrasi MSP")
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPrivilege()
        If Not IsPostBack Then
            If arr.Count > 0 Then
                BindDataGrid()
            End If
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

    Private Function ValidateData(ByVal objExcelReader As IExcelDataReader) As String
       
        Dim str = String.Empty
        If IsNothing(objExcelReader.Item(1)) Then
            str += Environment.NewLine + "Kode dealer tidak boleh kosong."
        End If

        If IsNothing(objExcelReader.Item(2)) Then
            str += Environment.NewLine + "Nama tidak boleh kosong."
        ElseIf objExcelReader.Item(2).ToString.Length > 50 Then
            str += Environment.NewLine + "Nama tidak boleh lebih dari 50 karakter."
        End If

        If Not IsNothing(objExcelReader.Item(4)) Then
            If objExcelReader.Item(4).ToString().Trim().Length > 0 AndAlso objExcelReader.Item(4).ToString.Length <> 16 Then
                str += Environment.NewLine + "No KTP harus sama dengan 16 karakter."
            End If
        End If
        

        If Not IsNothing(objExcelReader.Item(6)) Then
            If objExcelReader.Item(6).ToString.Length > 100 Then
                str += Environment.NewLine + "Alamat tidak boleh lebih dari 100 karakter."
            End If
        End If
         

        If Not IsNothing(objExcelReader.Item(9)) Then
            If objExcelReader.Item(9).ToString.Length > 15 Then
                str += Environment.NewLine + "No Telepon tidak boleh lebih dari 15 karakter."
            End If
        End If
       

        If Not IsNothing(objExcelReader.Item(10)) AndAlso objExcelReader.Item(10).ToString().Trim() <> "" Then
            Dim bool As Boolean = New KTB.DNet.UI.Helper.MSPHelper().EmailAddressCheck(objExcelReader.Item(10))
            If bool = False Then
                str += Environment.NewLine & "Alamat email tidak valid."
            ElseIf objExcelReader.Item(10).ToString.Length > 50 Then
                str += Environment.NewLine + "Email tidak boleh lebih dari 50 karakter."
            End If
        End If

        If IsNothing(objExcelReader.Item(12)) Then
            str += Environment.NewLine + "No Rangka tidak boleh kosong."
        End If

        Return str
    End Function

    Private Function ValidateObject(ByVal strObject As String, ByVal strCode As String) As String
        Dim crt As CriteriaComposite
        Dim count As Integer = 0
        Select Case strObject.ToLower()
            Case "chassisnumber"
                crt = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, strCode))
                If New ChassisMasterFacade(User).Retrieve(crt).Count = 0 Then
                    Return "No Rangka tidak valid."
                End If
            Case "city"
                crt = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, strCode))
                If facCity.Retrieve(crt).Count = 0 Then
                    Return "Nama Kota tidak boleh kosong."
                End If
            Case "dealer"
                crt = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, strCode))
                If facDealer.Retrieve(crt).Count = 0 Then
                    Return "Kode dealer " + strCode + " tidak valid."
                End If

        End Select
        Return "-1"
    End Function

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
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
                Dim directory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "MSP"
                Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directory)

                If Not directoryInfo.Exists Then
                    directoryInfo.Create()
                End If

                Dim ext As String = System.IO.Path.GetExtension(UploadFile.PostedFile.FileName)
                If Not CheckExt(ext.Substring(1)) Then
                    Throw New Exception("Salah Extention")
                End If

                Dim targetFile As String = New System.Text.StringBuilder(directory). _
                   Append("\").Append(datetimenow + "_" + _
                                      Path.GetFileName(UploadFile.PostedFile.FileName)).ToString
                UploadFile.PostedFile.SaveAs(targetFile)

                Dim objExcelReader As IExcelDataReader = Nothing
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

                        While objExcelReader.Read()
                            ' get value from excel and insert to fleet customer object
                            If i >= 2 Then
                                Dim strRemarks As String = String.Empty
                                Dim strValidate As String = String.Empty
                                objMSPReg = New MSPRegistration
                                objMSPCustomer = New MSPCustomer
                                objMSPRegHistory = New MSPRegistrationHistory
                                objMSPRegHistory.Status = CInt(EnumStatusMSP.Status.Baru)
                                objMSPRegHistory.RequestType = CInt(EnumStatusMSP.StatusType.Baru).ToString
                                ' validate inputted data
                                strRemarks = ValidateData(objExcelReader)

                                strValidate = ValidateObject("dealer", If(IsNothing(objExcelReader.Item(1)), String.Empty, objExcelReader.Item(1).ToString))
                                If strValidate <> "-1" Then
                                    strRemarks += Environment.NewLine & strValidate
                                Else
                                    objMSPReg.Dealer = New DealerFacade(User).Retrieve(If(IsNothing(objExcelReader.Item(1)), String.Empty, objExcelReader.Item(1).ToString))
                                End If

                                objMSPCustomer.Name1 = If(IsNothing(objExcelReader.Item(2)), String.Empty, objExcelReader.Item(2).ToString)
                                objMSPReg.OldMSPCode = If(IsNothing(objExcelReader.Item(3)), String.Empty, objExcelReader.Item(3).ToString)
                                objMSPCustomer.KTPNo = If(IsNothing(objExcelReader.Item(4)), String.Empty, objExcelReader.Item(4).ToString)
                                objMSPCustomer.Age = If(IsNothing(objExcelReader.Item(5)), 0, CInt(objExcelReader.Item(5)))
                                objMSPCustomer.Alamat = If(IsNothing(objExcelReader.Item(6)), String.Empty, objExcelReader.Item(6).ToString)
                                objMSPCustomer.PreArea = If(IsNothing(objExcelReader.Item(7)), String.Empty, objExcelReader.Item(7).ToString)

                                strValidate = ValidateObject("city", If(IsNothing(objExcelReader.Item(8)), String.Empty, objExcelReader.Item(8).ToString))
                                If strValidate = "-1" Then
                                    crt = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                    crt.opAnd(New Criteria(GetType(City), "CityName", MatchType.Exact, objExcelReader.Item(8).ToString))
                                    Dim arrList As ArrayList = facCity.Retrieve(crt)
                                    objMSPCustomer.City = CType(arrList(0), City)
                                    objMSPCustomer.Province = objMSPCustomer.City.Province
                                Else
                                    'strRemarks += Environment.NewLine & strValidate
                                End If

                                objMSPCustomer.PhoneNo = If(IsNothing(objExcelReader.Item(9)), String.Empty, objExcelReader.Item(9).ToString)
                                objMSPCustomer.Email = If(IsNothing(objExcelReader.Item(10)), String.Empty, objExcelReader.Item(10).ToString)
                                ' Tgl Join MSP
                                Dim errRegDate As Boolean = False
                                If Not IsNothing(objExcelReader.Item(17)) Then
                                    Try
                                        objMSPRegHistory.RegistrationDate = New MSPHelper().GetDateShort(objExcelReader.Item(17).ToString)
                                        errRegDate = True
                                        If objMSPRegHistory.RegistrationDate > Now Then
                                            strRemarks += Environment.NewLine & "Tgl Join MSP tidak boleh lebih dari tgl upload."
                                        End If
                                    Catch ex As Exception
                                        strRemarks += Environment.NewLine & "Tgl Join MSP format salah."
                                    End Try
                                Else
                                    strRemarks += Environment.NewLine & "Tgl Join MSP tidak boleh kosong."
                                End If

                                strValidate = ValidateObject("chassisnumber", If(IsNothing(objExcelReader.Item(11)), String.Empty, objExcelReader.Item(11).ToString))
                                If strValidate = "-1" Then
                                    ' validate chassis number 
                                    If errRegDate Then
                                        Dim strValidateChassis As String = New MSPHelper().ValidateChassisNumber(objExcelReader.Item(11).ToString, objMSPRegHistory.RegistrationDate)
                                        If strValidateChassis = String.Empty Then
                                            crt = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                            crt.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, objExcelReader.Item(11).ToString))
                                            Dim arrList As ArrayList = New ChassisMasterFacade(User).Retrieve(crt)
                                            objMSPReg.ChassisMaster = CType(arrList(0), ChassisMaster)


                                            Dim dtSet As DataSet = New MSPRegistrationFacade(User).RetrieveSp("EXEC sp_MSP_GetMSPMasterList '" + objExcelReader.Item(11).ToString + "','" & objMSPRegHistory.RegistrationDate.ToString("yyyy-MM-dd") & "'")
                                            If dtSet.Tables.Count > 0 Then
                                                Dim dtTbl As DataTable = dtSet.Tables(0)
                                                If dtTbl.Rows.Count > 0 Then
                                                    Dim strMSPMasterID As String = String.Empty
                                                    For Each row As DataRow In dtTbl.Rows
                                                        strMSPMasterID += "," & row("MSPMasterID").ToString
                                                    Next

                                                    crt = New CriteriaComposite(New Criteria(GetType(MSPMaster), "ID", MatchType.InSet, "(" + strMSPMasterID.Substring(1, strMSPMasterID.Length - 1) + ")"))
                                                    crt.opAnd(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                                    crt.opAnd(New Criteria(GetType(MSPMaster), "MSPType.Description", MatchType.Exact, If(IsNothing(objExcelReader.Item(12)), String.Empty, objExcelReader.Item(12).ToString)))
                                                    Dim arrMSPMaster As ArrayList = New MSPMasterFacade(User).Retrieve(crt)
                                                    If arrMSPMaster.Count = 0 Then
                                                        strRemarks += Environment.NewLine & "Tipe MSP belum terdaftar pada No Rangka ini."
                                                        strRemarks += Environment.NewLine & "Durasi belum terdaftar pada Tipe MSP dan No Rangka yang diinput."
                                                        strRemarks += Environment.NewLine & "KM belum terdaftar pada Tipe MSP dan No Rangka yang diinput."
                                                    Else
                                                        ' validate duration
                                                        crt.opAnd(New Criteria(GetType(MSPMaster), "Duration", MatchType.Exact, If(IsNothing(objExcelReader.Item(13)), String.Empty, objExcelReader.Item(13).ToString)))
                                                        arrMSPMaster = New MSPMasterFacade(User).Retrieve(crt)

                                                        If arrMSPMaster.Count > 0 Then
                                                            ' validate expired based on duration selected
                                                            Dim validUntil As Date = objMSPReg.ChassisMaster.EndCustomer.OpenFakturDate.AddYears(CType(arrMSPMaster(0), MSPMaster).Duration)
                                                            If Now.AddYears(1) >= validUntil Then
                                                                strRemarks += Environment.NewLine & "Nomor rangka ini sudah melebihi batas pendaftaran pada durasi " & CType(arrMSPMaster(0), MSPMaster).Duration.ToString & " tahun."
                                                            End If
                                                            ' validate MSPKm
                                                            crt.opAnd(New Criteria(GetType(MSPMaster), "MSPKm", MatchType.Exact, If(IsNothing(objExcelReader.Item(14)), String.Empty, objExcelReader.Item(14).ToString)))
                                                            arrMSPMaster = New MSPMasterFacade(User).Retrieve(crt)
                                                            If arrMSPMaster.Count > 0 Then
                                                                objMSPRegHistory.MSPMaster = CType(arrMSPMaster(0), MSPMaster)
                                                            Else
                                                                strRemarks += Environment.NewLine & "KM belum terdaftar pada Tipe MSP dan No Rangka yang diinput."
                                                            End If
                                                        Else
                                                            strRemarks += Environment.NewLine & "Durasi belum terdaftar pada Tipe MSP dan No Rangka yang diinput."
                                                        End If
                                                    End If
                                                End If
                                            End If

                                    Else
                                            strRemarks += Environment.NewLine & strValidateChassis.Substring(2, strValidateChassis.Length - 2)
                                    End If
                                Else
                                    strRemarks += Environment.NewLine & "Tidak ada MSP terdaftar pada tgl " & objMSPRegHistory.RegistrationDate.ToString("yyyy/MM/dd") & "."
                                End If

                            Else
                                strRemarks += Environment.NewLine & strValidate
                                End If
                            ' promo
                            If Not IsNothing(objExcelReader.Item(15)) Then
                                If objExcelReader.Item(15).ToString.ToLower = "promo" Then
                                    If IsNothing(objExcelReader.Item(16)) Then
                                        objMSPRegHistory.BenefitMasterHeaderID = -1
                                        strRemarks += Environment.NewLine & "Tipe Benefit tidak boleh kosong."
                                    Else
                                        Dim dtSet As DataSet = New MSPRegistrationFacade(User).RetrieveSp("EXEC sp_MSP_CheckBenefit '" & If(Not IsNothing(objExcelReader.Item(11)), objExcelReader.Item(11).ToString, String.Empty) & "','" & If(Not IsNothing(objExcelReader.Item(1)), objExcelReader.Item(1).ToString, String.Empty) & "','" & objExcelReader.Item(16).ToString & "'")
                                        If dtSet.Tables.Count > 0 Then
                                            Dim dtTbl As DataTable = dtSet.Tables(0)
                                            If dtTbl.Rows.Count > 0 Then
                                                If Not IsNothing(dtTbl.Rows(0)("BenefitMasterHeaderID")) Then
                                                    objMSPRegHistory.BenefitMasterHeaderID = CInt(dtTbl.Rows(0)("BenefitMasterHeaderID"))
                                                Else
                                                    objMSPRegHistory.BenefitMasterHeaderID = -1
                                                End If
                                            Else
                                                strRemarks += Environment.NewLine & "Tipe Benefit tidak terdaftar dalam Sales Campaign"
                                                objMSPRegHistory.BenefitMasterHeaderID = -1
                                            End If

                                        End If
                                        End If
                                    Else
                                        If objExcelReader.Item(15).ToString.ToLower = "paid" Then
                                            If Not IsNothing(objExcelReader.Item(16)) Then
                                                strRemarks += Environment.NewLine & "Pengajuan Paid tidak boleh ada Benefit"
                                            End If
                                        End If

                                    End If
                            Else
                                objMSPRegHistory.BenefitMasterHeaderID = -2
                                strRemarks += Environment.NewLine & "Tipe pengajuan tidak boleh kosong."
                            End If

                            ' Dijual Oleh
                            If Not IsNothing(objExcelReader.Item(18)) Then
                                objMSPRegHistory.SoldBy = objExcelReader.Item(18).ToString
                            Else
                                strRemarks += Environment.NewLine & "Dijual oleh tidak boleh kosong."
                            End If

                            ' remarks & benefit type as custom function untuk menyimpan  temporary remarks error dan benefit type
                            objMSPReg.Remarks = strRemarks
                            objMSPReg.BenefitType = If(IsNothing(objExcelReader.Item(16)), String.Empty, objExcelReader.Item(16).ToString)

                            objMSPReg.MSPCustomer = objMSPCustomer
                            objMSPReg.MSPRegistrationHistorys.Clear()
                            objMSPReg.MSPRegistrationHistorys.Add(objMSPRegHistory)

                            arr.Add(objMSPReg)
                            arrMSPCustomer.Add(objMSPCustomer)
                            End If

                    i = i + 1
                        End While

                        sessHelper.SetSession(strSessUploadExcel, arr)
                        sessHelper.SetSession(strSessMSPCustomer, arrMSPCustomer)
                       
                        BindDataGrid()
                    Else
                        ' show message if excel file isn't have value
                        MessageBox.Show("Data yang anda input kosong.")
                    End If

                    sapImp.StopImpersonate()
                    sapImp = Nothing
                End Using
            Catch ex As Exception
                MessageBox.Show("Gagal upload file.")
            End Try
        Else
            MessageBox.Show("Excel file is required.")
        End If
    End Sub

    Private Sub BindDataGrid()
        dtgMSPRegUpload.DataSource = CType(sessHelper.GetSession(strSessUploadExcel), ArrayList)
        dtgMSPRegUpload.DataBind()
        Dim col As DataGridColumn = dtgMSPRegUpload.Columns(dtgMSPRegUpload.Columns.Count - 1)
        col.ItemStyle.Width = Unit.Pixel(100)

        btnSave.Visible = True
    End Sub

    Private Sub dtgMSPRegUpload_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgMSPRegUpload.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "edit"
                dtgMSPRegUpload.EditItemIndex = CInt(e.Item.ItemIndex)
                BindDataGrid()
                btnSave.Visible = False
                ddlFilter.Enabled = False
            Case "delete"
                Dim arrList As ArrayList = sessHelper.GetSession(strSessUploadExcel)
                Dim arrlistMspCustomer As ArrayList = sessHelper.GetSession(strSessMSPCustomer)

                arrList.RemoveAt(CInt(e.Item.ItemIndex))
                arrlistMspCustomer.RemoveAt(CInt(e.Item.ItemIndex))

                sessHelper.SetSession(strSessUploadExcel, arrList)
                sessHelper.SetSession(strSessMSPCustomer, arrlistMspCustomer)
                BindDataGrid()
            Case "update"
                UpdateCommand(e)
                ddlFilter.Enabled = True
            Case "cancel"
                dtgMSPRegUpload.EditItemIndex = -1
                BindDataGrid()
                ddlFilter.Enabled = True
        End Select
    End Sub

    Private Sub UpdateCommand(ByVal e As DataGridCommandEventArgs)
        Dim arrListDt As ArrayList = sessHelper.GetSession(strSessUploadExcel)
        Dim arrLIstMSPCustomer As ArrayList = sessHelper.GetSession(strSessMSPCustomer)
        Dim strValidate As String = String.Empty

        Dim txtDealer As TextBox = CType(e.Item.FindControl("txtDealer"), TextBox)
        Dim txtName As TextBox = CType(e.Item.FindControl("txtName"), TextBox)
        Dim txtKTP As TextBox = CType(e.Item.FindControl("txtKTP"), TextBox)
        Dim txtOldMSP As TextBox = CType(e.Item.FindControl("txtOldMSP"), TextBox)
        Dim txtAddress As TextBox = CType(e.Item.FindControl("txtAddress"), TextBox)
        Dim ddlProvince As DropDownList = CType(e.Item.FindControl("ddlProvince"), DropDownList)
        Dim ddlCity As DropDownList = CType(e.Item.FindControl("ddlCity"), DropDownList)
        Dim ddlPreArea As DropDownList = CType(e.Item.FindControl("ddlPreArea"), DropDownList)
        Dim txtPhoneNo As TextBox = CType(e.Item.FindControl("txtPhoneNo"), TextBox)
        Dim txtEmail As TextBox = CType(e.Item.FindControl("txtEmail"), TextBox)
        Dim txtChassisNumber As TextBox = CType(e.Item.FindControl("txtChassisNumber"), TextBox)
        Dim ddlMSPType As DropDownList = CType(e.Item.FindControl("ddlMSPType"), DropDownList)
        Dim ddlDuration As DropDownList = CType(e.Item.FindControl("ddlDuration"), DropDownList)
        Dim ddlRequestType As DropDownList = CType(e.Item.FindControl("ddlRequestType"), DropDownList)
        Dim txtBenefitType As TextBox = CType(e.Item.FindControl("txtBenefitType"), TextBox)
        Dim txtRequestDate As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("txtRequestDate"), KTB.DNet.WebCC.IntiCalendar)
        Dim ddlSoldBy As DropDownList = CType(e.Item.FindControl("ddlSoldBy"), DropDownList)

        strValidate = ValidateUpdateCommand(e)
        objMSPReg = arrListDt(e.Item.ItemIndex)
        objMSPCustomer = New MSPCustomer
        objMSPRegHistory = New MSPRegistrationHistory
        Dim arrUpdate As New ArrayList
        crt = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crt.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, txtDealer.Text))
        arrUpdate = New DealerFacade(User).Retrieve(crt)
        If arrUpdate.Count > 0 Then
            objMSPReg.Dealer = CType(arrUpdate(0), Dealer)
        Else
            strValidate += Environment.NewLine + "Kode dealer tidak tidak terdaftar."
        End If

        objMSPRegHistory.RequestType = CInt(EnumStatusMSP.StatusType.Baru).ToString
        objMSPCustomer.Name1 = txtName.Text
        objMSPCustomer.KTPNo = txtKTP.Text
        objMSPReg.OldMSPCode = txtOldMSP.Text
        objMSPCustomer.Alamat = txtAddress.Text

        If ddlProvince.SelectedIndex <> 0 Then
            objMSPCustomer.Province = New Province(ID:=CInt(ddlProvince.SelectedValue))
        End If

        If ddlCity.SelectedIndex <> 0 Then
            objMSPCustomer.City = New City(ID:=CInt(ddlCity.SelectedValue))
        End If

        If ddlPreArea.SelectedIndex <> 0 Then
            objMSPCustomer.PreArea = ddlPreArea.SelectedValue
        End If

        objMSPCustomer.PhoneNo = txtPhoneNo.Text
        objMSPCustomer.Email = txtEmail.Text

        If txtChassisNumber.Text <> String.Empty Then
            Dim strTempChassis As String = New MSPHelper().ValidateChassisNumber(txtChassisNumber.Text)
            If strTempChassis <> String.Empty Then
                strValidate += strTempChassis
            Else
                objMSPReg.ChassisMaster = New ChassisMasterFacade(User).Retrieve(txtChassisNumber.Text)
            End If
        End If

        If ddlMSPType.Items.Count > 0 Then
            If ddlMSPType.SelectedIndex <> 0 Then

                If ddlDuration.Items.Count > 0 Then
                    If ddlDuration.SelectedIndex <> 0 Then
                        objMSPRegHistory.MSPMaster = New MSPMaster(ID:=CInt(ddlDuration.SelectedValue))
                    Else
                        strValidate += Environment.NewLine + "Durasi belum dipilih."
                    End If
                Else
                    strValidate += Environment.NewLine + "Durasi tidak valid."
                End If

            Else
                strValidate += Environment.NewLine + "Tipe MSP belum dipilih."
            End If
        Else
            strValidate += Environment.NewLine + "Tipe MSP tidak valid."
        End If

        If ddlRequestType.SelectedIndex <> 0 Then
            If ddlRequestType.SelectedValue.ToString.ToLower = "promo" Then
                If txtBenefitType.Text = String.Empty Then
                    strValidate += Environment.NewLine & "Tipe Benefit tidak boleh kosong."
                Else
                    Dim dtSet As DataSet = New MSPRegistrationFacade(User).RetrieveSp("EXEC sp_MSP_CheckBenefit '" & txtChassisNumber.Text & "','" & txtDealer.Text & "','" & txtBenefitType.Text & "'")
                    If dtSet.Tables.Count > 0 Then
                        Dim dtTbl As DataTable = dtSet.Tables(0)
                        If dtTbl.Rows.Count > 0 Then
                            If Not IsNothing(dtTbl.Rows(0)("BenefitMasterHeaderID")) Then
                                objMSPRegHistory.BenefitMasterHeaderID = CInt(dtTbl.Rows(0)("BenefitMasterHeaderID"))
                            Else
                                objMSPRegHistory.BenefitMasterHeaderID = -1
                            End If
                        Else
                            strValidate += Environment.NewLine & "Tipe Benefit tidak terdaftar dalam Sales Campaign"
                            objMSPRegHistory.BenefitMasterHeaderID = -1
                        End If
                       
                    End If
                End If
            Else
                If txtBenefitType.Text <> String.Empty Then
                    strValidate += Environment.NewLine & "Tipe Benefit tidak boleh Terisi."
                End If

            End If
        Else
            strValidate += Environment.NewLine & "Tipe pengajuan tidak boleh kosong."
        End If

        If txtRequestDate.Value = Date.MinValue Then
            strValidate += Environment.NewLine & "Tipe pengajuan tidak boleh kosong."
        Else
            objMSPRegHistory.RegistrationDate = CDate(txtRequestDate.Value)
        End If

        If ddlSoldBy.SelectedIndex <> 0 Then
            objMSPRegHistory.SoldBy = ddlSoldBy.SelectedValue
        Else
            strValidate += Environment.NewLine & "Dijual oleh tidak boleh kosong."
        End If

        objMSPReg.BenefitType = txtBenefitType.Text
        objMSPReg.Remarks = strValidate

        objMSPReg.MSPCustomer = objMSPCustomer
        objMSPReg.MSPRegistrationHistorys.Clear()
        objMSPReg.MSPRegistrationHistorys.Add(objMSPRegHistory)

        arrListDt(e.Item.ItemIndex) = objMSPReg
        arrLIstMSPCustomer(e.Item.ItemIndex) = objMSPCustomer

        sessHelper.SetSession(strSessUploadExcel, arrListDt)
        sessHelper.SetSession(strSessMSPCustomer, arrLIstMSPCustomer)

        dtgMSPRegUpload.EditItemIndex = -1
        BindDataGrid()

    End Sub

    Private Function ValidateUpdateCommand(ByVal e As DataGridCommandEventArgs) As String
        Dim Str As String = String.Empty

        Dim txtDealer As TextBox = CType(e.Item.FindControl("txtDealer"), TextBox)
        Dim txtName As TextBox = CType(e.Item.FindControl("txtName"), TextBox)
        Dim txtKTP As TextBox = CType(e.Item.FindControl("txtKTP"), TextBox)
        Dim txtAddress As TextBox = CType(e.Item.FindControl("txtAddress"), TextBox)
        Dim txtPhoneNo As TextBox = CType(e.Item.FindControl("txtPhoneNo"), TextBox)
        Dim txtChassisNumber As TextBox = CType(e.Item.FindControl("txtChassisNumber"), TextBox)
        Dim ddlMSPType As DropDownList = CType(e.Item.FindControl("ddlMSPType"), DropDownList)
        Dim ddlDuration As DropDownList = CType(e.Item.FindControl("ddlDuration"), DropDownList)
        Dim ddlRequestType As DropDownList = CType(e.Item.FindControl("ddlRequestType"), DropDownList)
        Dim txtBenefitType As TextBox = CType(e.Item.FindControl("txtBenefitType"), TextBox)
        Dim txtRequestDate As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("txtRequestDate"), KTB.DNet.WebCC.IntiCalendar)
        Dim ddlSoldBy As DropDownList = CType(e.Item.FindControl("ddlSoldBy"), DropDownList)

        If String.IsNullOrEmpty(txtDealer.Text) Then
            Str += Environment.NewLine + "Kode dealer tidak boleh kosong."
        End If

        If String.IsNullOrEmpty(txtName.Text) Then
            Str += Environment.NewLine + "Nama tidak boleh kosong."
        End If

        If (txtKTP.Text.Length <> 16) AndAlso txtKTP.Text.Length > 0 Then
            Str += Environment.NewLine + "No KTP harus sama dengan 16 karakter."
        End If

        'If String.IsNullOrEmpty(txtAddress.Text) Then
        '    Str += Environment.NewLine + "Alamat tidak boleh kosong."
        'End If

        'If String.IsNullOrEmpty(txtPhoneNo.Text) Then
        '    Str += Environment.NewLine + "No tlp tidak boleh kosong."
        'End If

        If String.IsNullOrEmpty(txtChassisNumber.Text) Then
            Str += Environment.NewLine + "No Rangka tidak boleh kosong."
        End If

        Return Str
    End Function

    Private Sub dtgMSPRegUpload_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMSPRegUpload.ItemDataBound
        arrMSPCustomer = sessHelper.GetSession(strSessMSPCustomer)
        Dim arrNewMSPCustomer As ArrayList = sessHelper.GetSession(strSessNewArrayMSPCustomer)

        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim rowValue As MSPRegistration = CType(e.Item.DataItem, MSPRegistration)
            objMSPCustomer = If(Not IsNothing(arrNewMSPCustomer), CType(arrNewMSPCustomer(e.Item.ItemIndex), MSPCustomer), CType(arrMSPCustomer(e.Item.ItemIndex), MSPCustomer))
            objMSPRegHistory = CType(rowValue.MSPRegistrationHistorys(0), MSPRegistrationHistory)

            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Then
                'lblDealer
                Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
                If Not IsNothing(lblDealer) Then
                    lblDealer.Text = If(Not IsNothing(rowValue.Dealer), rowValue.Dealer.DealerCode, "")
                End If

                'lblName
                Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)
                If Not IsNothing(lblName) Then
                    lblName.Text = objMSPCustomer.Name1
                End If

                'lblKTP
                Dim lblKTP As Label = CType(e.Item.FindControl("lblKTP"), Label)
                If Not IsNothing(lblKTP) Then
                    lblKTP.Text = objMSPCustomer.KTPNo
                End If

                'lblOldMSP
                Dim lblOldMSP As Label = CType(e.Item.FindControl("lblOldMSP"), Label)
                If Not IsNothing(lblOldMSP) Then
                    lblOldMSP.Text = rowValue.OldMSPCode
                End If

                'lblAddress
                Dim lblAddress As Label = CType(e.Item.FindControl("lblAddress"), Label)
                If Not IsNothing(lblAddress) Then
                    lblAddress.Text = objMSPCustomer.Alamat
                End If

                'lblProvince
                Dim lblProvince As Label = CType(e.Item.FindControl("lblProvince"), Label)
                If Not IsNothing(lblProvince) Then
                    lblProvince.Text = If(Not IsNothing(objMSPCustomer.City), objMSPCustomer.City.Province.ProvinceName, "")
                End If

                'lblCity
                Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
                If Not IsNothing(lblCity) Then
                    lblCity.Text = If(Not IsNothing(objMSPCustomer.City), objMSPCustomer.City.CityName, "")
                End If

                'lblPreArea
                Dim lblPreArea As Label = CType(e.Item.FindControl("lblPreArea"), Label)
                If Not IsNothing(lblPreArea) Then
                    lblPreArea.Text = objMSPCustomer.PreArea
                End If

                'lblPhoneNo
                Dim lblPhoneNo As Label = CType(e.Item.FindControl("lblPhoneNo"), Label)
                If Not IsNothing(lblPhoneNo) Then
                    lblPhoneNo.Text = objMSPCustomer.PhoneNo
                End If

                'lblEmail
                Dim lblEmail As Label = CType(e.Item.FindControl("lblEmail"), Label)
                If Not IsNothing(lblEmail) Then
                    lblEmail.Text = objMSPCustomer.Email
                End If

                'lblChassisNumber
                Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
                If Not IsNothing(lblChassisNumber) Then
                    lblChassisNumber.Text = If(Not IsNothing(rowValue.ChassisMaster), rowValue.ChassisMaster.ChassisNumber, "")
                End If

                'lblPKTDate
                Dim lblPKTDate As Label = CType(e.Item.FindControl("lblPKTDate"), Label)
                If Not IsNothing(lblPKTDate) Then
                    lblPKTDate.Text = If(Not IsNothing(rowValue.ChassisMaster) AndAlso Not IsNothing(rowValue.ChassisMaster.EndCustomer), rowValue.ChassisMaster.EndCustomer.OpenFakturDate.ToString("yyyy-MM-dd"), "")
                End If

                'lblMSPType
                Dim lblMSPType As Label = CType(e.Item.FindControl("lblMSPType"), Label)
                If Not IsNothing(lblMSPType) Then
                    lblMSPType.Text = If(Not IsNothing(objMSPRegHistory.MSPMaster), objMSPRegHistory.MSPMaster.MSPType.Description, "")
                End If

                'lblDuration
                Dim lblDuration As Label = CType(e.Item.FindControl("lblDuration"), Label)
                If Not IsNothing(lblDuration) Then
                    lblDuration.Text = If(Not IsNothing(objMSPRegHistory.MSPMaster), objMSPRegHistory.MSPMaster.Duration.ToString & " Thn - " + Convert.ToDouble(objMSPRegHistory.MSPMaster.MSPKm).ToString("C"), "")
                End If

                'lblRequestType
                Dim lblRequestType As Label = CType(e.Item.FindControl("lblRequestType"), Label)
                If Not IsNothing(lblRequestType) Then
                    lblRequestType.Text = If(objMSPRegHistory.BenefitMasterHeaderID = 0, "Paid", If(objMSPRegHistory.BenefitMasterHeaderID = -2, "", "Promo"))
                End If

                'lblBenefitType
                Dim lblBenefitType As Label = CType(e.Item.FindControl("lblBenefitType"), Label)
                If Not IsNothing(lblBenefitType) Then
                    lblBenefitType.Text = rowValue.BenefitType
                    'If Not IsNothing(newArrBenefitType) Then
                    '    lblBenefitType.Text = newArrBenefitType(e.Item.ItemIndex)
                    'Else
                    '    lblBenefitType.Text = arrBenefitType(e.Item.ItemIndex)
                    'End If
                End If

                'lblRequestDate
                Dim lblRequestDate As Label = CType(e.Item.FindControl("lblRequestDate"), Label)
                If Not IsNothing(lblRequestDate) Then
                    lblRequestDate.Text = objMSPRegHistory.RegistrationDate.ToString("yyyy-MM-dd")
                End If

                'lblSoldBy
                Dim lblSoldBy As Label = CType(e.Item.FindControl("lblSoldBy"), Label)
                If Not IsNothing(lblSoldBy) Then
                    lblSoldBy.Text = objMSPRegHistory.SoldBy
                End If

                'lblDescription
                Dim lblDescription As Label = CType(e.Item.FindControl("lblDescription"), Label)
                If Not IsNothing(lblDescription) Then
                    lblDescription.Text = rowValue.Remarks
                End If
            Else
                'txtDealer
                Dim txtDealer As TextBox = CType(e.Item.FindControl("txtDealer"), TextBox)
                If Not IsNothing(txtDealer) Then
                    txtDealer.Text = If(Not IsNothing(rowValue.Dealer), rowValue.Dealer.DealerCode, "")
                End If

                'txtName
                Dim txtName As TextBox = CType(e.Item.FindControl("txtName"), TextBox)
                If Not IsNothing(txtName) Then
                    txtName.Text = objMSPCustomer.Name1
                End If

                'txtKTP
                Dim txtKTP As TextBox = CType(e.Item.FindControl("txtKTP"), TextBox)
                If Not IsNothing(txtKTP) Then
                    txtKTP.Text = objMSPCustomer.KTPNo
                End If

                'txtOldMSP
                Dim txtOldMSP As TextBox = CType(e.Item.FindControl("txtOldMSP"), TextBox)
                If Not IsNothing(txtOldMSP) Then
                    txtOldMSP.Text = rowValue.OldMSPCode
                End If

                'txtAddress
                Dim txtAddress As TextBox = CType(e.Item.FindControl("txtAddress"), TextBox)
                If Not IsNothing(txtAddress) Then
                    txtAddress.Text = objMSPCustomer.Alamat
                End If

                'ddlProvince
                Dim ddlProvince As DropDownList = CType(e.Item.FindControl("ddlProvince"), DropDownList)
                ddlProvince.Items.Clear()
                crt = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                ddlProvince.DataSource = New ProvinceFacade(User).RetrieveActiveList(crt, "ProvinceName", Sort.SortDirection.ASC)
                ddlProvince.DataTextField = "ProvinceName"
                ddlProvince.DataValueField = "ID"
                ddlProvince.DataBind()
                ddlProvince.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
                If Not IsNothing(objMSPCustomer.City) Then
                    ddlProvince.SelectedValue = objMSPCustomer.City.Province.ID
                Else
                    ddlProvince.SelectedIndex = 0
                End If

                ' ddlcity
                Dim ddlCity As DropDownList = CType(e.Item.FindControl("ddlCity"), DropDownList)
                ddlCity.Items.Clear()
                crt = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

                ddlCity.DataSource = New CityFacade(User).RetrieveActiveList(crt, "CityName", Sort.SortDirection.ASC)
                ddlCity.DataTextField = "CityName".ToUpper
                ddlCity.DataValueField = "ID"
                ddlCity.DataBind()
                ddlCity.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
                If Not IsNothing(objMSPCustomer.City) Then
                    ddlCity.SelectedValue = objMSPCustomer.City.ID
                Else
                    ddlCity.SelectedIndex = 0
                End If

                'ddlPreArea
                Dim ddlPreArea As DropDownList = CType(e.Item.FindControl("ddlPreArea"), DropDownList)
                If Not IsNothing(ddlPreArea) Then
                    ddlPreArea.SelectedValue = objMSPCustomer.PreArea
                End If

                'txtPhoneNo
                Dim txtPhoneNo As TextBox = CType(e.Item.FindControl("txtPhoneNo"), TextBox)
                If Not IsNothing(txtPhoneNo) Then
                    txtPhoneNo.Text = objMSPCustomer.PhoneNo
                End If

                'txtEmail
                Dim txtEmail As TextBox = CType(e.Item.FindControl("txtEmail"), TextBox)
                If Not IsNothing(txtEmail) Then
                    txtEmail.Text = objMSPCustomer.Email
                End If

                'txtChassisNumber
                Dim txtChassisNumber As TextBox = CType(e.Item.FindControl("txtChassisNumber"), TextBox)
                If Not IsNothing(txtChassisNumber) Then
                    txtChassisNumber.Text = If(Not IsNothing(rowValue.ChassisMaster), rowValue.ChassisMaster.ChassisNumber, "")
                End If

                'lblPKTDateEdit
                Dim lblPKTDateEdit As Label = CType(e.Item.FindControl("lblPKTDateEdit"), Label)
                If Not IsNothing(lblPKTDateEdit) Then
                    lblPKTDateEdit.Text = If(Not IsNothing(rowValue.ChassisMaster), rowValue.ChassisMaster.EndCustomer.OpenFakturDate.ToString("yyyy-MM-dd"), "")
                End If

                'ddlMSPType
                Dim ddlMSPType As DropDownList = CType(e.Item.FindControl("ddlMSPType"), DropDownList)
                If Not IsNothing(ddlMSPType) Then
                    If Not IsNothing(objMSPRegHistory.MSPMaster) Then
                        ' set dropdown Type MSP
                        crt = New CriteriaComposite(New Criteria(GetType(MSPMaster), "VehicleType.ID", MatchType.Exact, If(Not IsNothing(objMSPRegHistory.MSPMaster), objMSPRegHistory.MSPMaster.VehicleType.ID, "0")))
                        crt.opAnd(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Dim arrMspMaster As ArrayList = New MSPMasterFacade(User).Retrieve(crt)

                        Dim newArrObjMSPMaster = From a As MSPMaster In arrMspMaster
                                                 Group By a.MSPType.ID, a.MSPType.Description Into Group
                                            Select ID, Description

                        ddlMSPType.Items.Clear()
                        ddlMSPType.DataSource = newArrObjMSPMaster
                        ddlMSPType.DataTextField = "Description"
                        ddlMSPType.DataValueField = "ID"
                        ddlMSPType.DataBind()
                        ddlMSPType.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
                        If Not IsNothing(objMSPRegHistory.MSPMaster) Then
                            ddlMSPType.SelectedValue = objMSPRegHistory.MSPMaster.MSPType.ID
                        Else
                            ddlMSPType.SelectedIndex = 0
                        End If
                    End If

                End If

                'ddlDuration
                Dim ddlDuration As DropDownList = CType(e.Item.FindControl("ddlDuration"), DropDownList)
                If Not IsNothing(ddlDuration) Then
                    If Not IsNothing(objMSPRegHistory.MSPMaster) Then
                        crt = New CriteriaComposite(New Criteria(GetType(MSPMaster), "VehicleType.ID", MatchType.Exact, If(Not IsNothing(objMSPRegHistory.MSPMaster), objMSPRegHistory.MSPMaster.VehicleType.ID, "0")))
                        crt.opAnd(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Dim sorts As SortCollection = New SortCollection
                        sorts.Add(New Sort(GetType(MSPMaster), "Duration", Sort.SortDirection.ASC))
                        sorts.Add(New Sort(GetType(MSPMaster), "Amount", Sort.SortDirection.ASC))
                        Dim arrMSPDuration As ArrayList = New MSPMasterFacade(User).Retrieve(crt, sorts)
                        Dim newArrDuration = From a As MSPMaster In arrMSPDuration
                                                  Select New With {.ID = a.ID, .DurationAmount = a.Duration.ToString + " Thn - " + String.Format("{0:#,##0}", Convert.ToDouble(a.MSPKm)) + " KM"}
                        ddlDuration.Items.Clear()
                        ddlDuration.DataSource = newArrDuration
                        ddlDuration.DataTextField = "DurationAmount"
                        ddlDuration.DataValueField = "ID"
                        ddlDuration.DataBind()
                        ddlDuration.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
                        If Not IsNothing(objMSPRegHistory.MSPMaster) Then
                            ddlDuration.SelectedValue = objMSPRegHistory.MSPMaster.ID
                        Else
                            ddlDuration.SelectedIndex = 0
                        End If
                    End If
                End If

                'ddlRequestType
                Dim ddlRequestType As DropDownList = CType(e.Item.FindControl("ddlRequestType"), DropDownList)
                If Not IsNothing(ddlRequestType) Then
                    ddlRequestType.SelectedValue = If(objMSPRegHistory.BenefitMasterHeaderID = 0, "Paid", "Promo")
                End If

                'txtBenefitType
                Dim txtBenefitType As TextBox = CType(e.Item.FindControl("txtBenefitType"), TextBox)
                If Not IsNothing(txtBenefitType) Then
                    txtBenefitType.Text = rowValue.BenefitType
                    'If Not IsNothing(newArrBenefitType) Then
                    '    txtBenefitType.Text = newArrBenefitType(e.Item.ItemIndex)
                    'Else
                    '    txtBenefitType.Text = arrBenefitType(e.Item.ItemIndex)
                    'End If
                End If

                'txtRequestDate
                Dim txtRequestDate As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("txtRequestDate"), KTB.DNet.WebCC.IntiCalendar)
                If Not IsNothing(txtRequestDate) Then
                    txtRequestDate.Value = objMSPRegHistory.RegistrationDate.ToString("yyyy-MM-dd")
                End If

                'ddlSoldBy
                Dim ddlSoldBy As DropDownList = CType(e.Item.FindControl("ddlSoldBy"), DropDownList)
                If Not IsNothing(ddlSoldBy) Then
                    ddlSoldBy.SelectedValue = objMSPRegHistory.SoldBy.ToUpper
                End If

                'lblDescriptionEdit
                Dim lblDescriptionEdit As Label = CType(e.Item.FindControl("lblDescriptionEdit"), Label)
                If Not IsNothing(lblDescriptionEdit) Then
                    lblDescriptionEdit.Text = rowValue.Remarks
                    'If Not IsNothing(newArrRemarks) Then
                    '    lblDescriptionEdit.Text = newArrRemarks(e.Item.ItemIndex)
                    'Else
                    '    lblDescriptionEdit.Text = arrRemarks(e.Item.ItemIndex)
                    'End If
                End If
                End If
        End If

    End Sub

    Protected Sub ddlProvince_SelectedIndexChanged(sender As Object, e As EventArgs)
        For Each item As DataGridItem In dtgMSPRegUpload.Items
            If dtgMSPRegUpload.EditItemIndex = item.ItemIndex Then
                Dim ddlProvince As DropDownList = CType(item.FindControl("ddlProvince"), DropDownList)
                Dim ddlCity As DropDownList = CType(item.FindControl("ddlCity"), DropDownList)

                ddlCity.Items.Clear()
                If ddlProvince.SelectedIndex <> 0 Then
                    crt = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crt.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, ddlProvince.SelectedValue))
                    crt.opAnd(New Criteria(GetType(City), "Status", MatchType.Exact, "A"))

                    ddlCity.DataSource = New CityFacade(User).RetrieveActiveList(crt, "CityName", Sort.SortDirection.ASC)
                    ddlCity.DataTextField = "CityName".ToUpper
                    ddlCity.DataValueField = "ID"
                    ddlCity.DataBind()
                End If
                ddlCity.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
                ddlCity.SelectedIndex = 0
            End If
        Next
    End Sub


    Protected Sub txtChassisNumber_TextChanged(sender As Object, e As EventArgs)
        For Each item As DataGridItem In dtgMSPRegUpload.Items
            If dtgMSPRegUpload.EditItemIndex = item.ItemIndex Then
                Dim txtChassisNumber As TextBox = CType(item.FindControl("txtChassisNumber"), TextBox)
                Dim ddlMSPType As DropDownList = CType(item.FindControl("ddlMSPType"), DropDownList)
                Dim lblPKTDateEdit As Label = CType(item.FindControl("lblPKTDateEdit"), Label)
                Dim txtRequestDate As KTB.DNet.WebCC.IntiCalendar = CType(item.FindControl("txtRequestDate"), KTB.DNet.WebCC.IntiCalendar)
                Dim strMSPMasterID As String = String.Empty

                Dim strValidateChassis As String = New MSPHelper().ValidateChassisNumber(txtChassisNumber.Text, CDate(Format(txtRequestDate.Value, "yyyy-MM-dd")))
                If strValidateChassis = String.Empty Then
                    Dim dtSet As DataSet = New MSPRegistrationFacade(User).RetrieveSp("EXEC sp_MSP_GetMSPMasterList '" + txtChassisNumber.Text + "','" & CDate(Format(txtRequestDate.Value, "yyyy-MM-dd")).ToString("yyyy-MM-dd") & "'")
                    Dim dtTbl As DataTable = dtSet.Tables(0)
                    If dtTbl.Rows.Count > 0 Then

                        If (Convert.ToDateTime(dtTbl.Rows(0)("PKTDate")) = CType("1753-01-01 00:00:00.000", DateTime)) Then
                            MessageBox.Show("Chassis Number belum memiliki tanggal Open Faktur.")
                            txtChassisNumber.Text = String.Empty
                            Return
                        End If

                        For Each row As DataRow In dtTbl.Rows
                            strMSPMasterID += "," & row("MSPMasterID").ToString
                        Next
                        ' set tanggal PKT
                        lblPKTDateEdit.Text = Convert.ToDateTime(dtTbl.Rows(0)("PKTDate")).ToString("dd/MM/yyyy")
                        ' set dropdown Type MSP
                        crt = New CriteriaComposite(New Criteria(GetType(MSPMaster), "ID", MatchType.InSet, "(" + strMSPMasterID.Substring(1, strMSPMasterID.Length - 1) + ")"))
                        crt.opAnd(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        arr = New MSPMasterFacade(User).Retrieve(crt)

                        Dim newArrObjMSPMaster = From a As MSPMaster In arr
                                                 Group By a.MSPType.ID, a.MSPType.Description Into Group
                                            Select ID, Description

                        ddlMSPType.Items.Clear()
                        ddlMSPType.DataSource = newArrObjMSPMaster
                        ddlMSPType.DataTextField = "Description"
                        ddlMSPType.DataValueField = "ID"
                        ddlMSPType.DataBind()
                        ddlMSPType.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
                        ddlMSPType.SelectedIndex = 0
                        ddlMSPType_SelectedIndexChanged(Me, System.EventArgs.Empty)
                    Else
                      
                        lblPKTDateEdit.Text = String.Empty
                        txtChassisNumber.Text = String.Empty
                        ddlMSPType.Items.Clear()
                        ddlMSPType_SelectedIndexChanged(Me, System.EventArgs.Empty)
                        MessageBox.Show("Tidak ada MSP Master yang terhubung dengan Chassis Number " + txtChassisNumber.Text)
                    End If
                Else
                    MessageBox.Show(strValidateChassis.Substring(2, strValidateChassis.Length - 2))
                End If
            End If
        Next
    End Sub

    Protected Sub ddlMSPType_SelectedIndexChanged(sender As Object, e As EventArgs)
        For Each item As DataGridItem In dtgMSPRegUpload.Items
            If dtgMSPRegUpload.EditItemIndex = item.ItemIndex Then
                Dim ddlMSPType As DropDownList = CType(item.FindControl("ddlMSPType"), DropDownList)
                Dim ddlDuration As DropDownList = CType(item.FindControl("ddlDuration"), DropDownList)
                Dim txtChassisNumber As TextBox = CType(item.FindControl("txtChassisNumber"), TextBox)
                Dim strMSPMasterID As String = String.Empty

                ddlDuration.Items.Clear()
                If ddlMSPType.SelectedIndex > 0 Then
                    Dim dtSet As DataSet = New MSPRegistrationFacade(User).RetrieveSp("EXEC sp_MSP_GetMSPMasterList '" + txtChassisNumber.Text + "'")
                    Dim dtTbl As DataTable = dtSet.Tables(0)
                    If dtTbl.Rows.Count > 0 Then
                        For Each row As DataRow In dtTbl.Rows
                            strMSPMasterID += "," & row("MSPMasterID").ToString
                        Next

                        crt = New CriteriaComposite(New Criteria(GetType(MSPMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crt.opAnd(New Criteria(GetType(MSPMaster), "MSPType", MatchType.Exact, ddlMSPType.SelectedValue))
                        crt.opAnd(New Criteria(GetType(MSPMaster), "ID", MatchType.InSet, "(" + strMSPMasterID.Substring(1, strMSPMasterID.Length - 1) + ")"))
                        Dim sorts As SortCollection = New SortCollection
                        sorts.Add(New Sort(GetType(MSPMaster), "Duration", Sort.SortDirection.ASC))
                        sorts.Add(New Sort(GetType(MSPMaster), "Amount", Sort.SortDirection.ASC))
                        arr = New MSPMasterFacade(User).Retrieve(crt, sorts)
                        Dim newArr = From a As MSPMaster In arr
                                                  Select New With {.ID = a.ID, .DurationAmount = a.Duration.ToString + " Thn - " + String.Format("{0:#,##0}", Convert.ToDouble(a.MSPKm)) + " KM"}
                        ddlDuration.Items.Clear()
                        ddlDuration.DataSource = newArr
                        ddlDuration.DataTextField = "DurationAmount"
                        ddlDuration.DataValueField = "ID"
                        ddlDuration.DataBind()
                        ddlDuration.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
                        ddlDuration.SelectedIndex = 0
                    End If
                End If
            End If
        Next
    End Sub

    Protected Sub ddlRequestType_SelectedIndexChanged(sender As Object, e As EventArgs)
        For Each item As DataGridItem In dtgMSPRegUpload.Items
            If dtgMSPRegUpload.EditItemIndex = item.ItemIndex Then
                Dim ddlRequestType As DropDownList = CType(item.FindControl("ddlRequestType"), DropDownList)
                Dim txtBenefitType As TextBox = CType(item.FindControl("txtBenefitType"), TextBox)

                txtBenefitType.Enabled = False
                If ddlRequestType.SelectedValue.ToLower = "promo" Then
                    txtBenefitType.Enabled = True
                End If
            End If
        Next
    End Sub


    Protected Sub ddlFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilter.SelectedIndexChanged
        arr = sessHelper.GetSession(strSessUploadExcel)
        arrMSPCustomer = sessHelper.GetSession(strSessMSPCustomer)
        Dim newArray As New ArrayList
        Dim newArrayMSPCustomer As New ArrayList

        If ddlFilter.SelectedIndex = 0 Then
            sessHelper.RemoveSession(strSessNewArrayMSPCustomer)
            BindDataGrid()
            btnSave.Visible = False

        ElseIf ddlFilter.SelectedValue.ToLower = "valid" Then
            Dim x As Integer = 0
            For Each _objMSReg As MSPRegistration In arr
                If _objMSReg.Remarks = String.Empty Then
                    newArray.Add(arr(x))
                    newArrayMSPCustomer.Add(arrMSPCustomer(x))
                    x += 1
                End If
            Next
            sessHelper.SetSession(strSessNewArrayMSPCustomer, newArrayMSPCustomer)
            dtgMSPRegUpload.DataSource = newArray
            dtgMSPRegUpload.DataBind()
            Dim col As DataGridColumn = dtgMSPRegUpload.Columns(dtgMSPRegUpload.Columns.Count - 1)
            col.ItemStyle.Width = Unit.Pixel(100)
            btnSave.Visible = True
        ElseIf ddlFilter.SelectedValue.ToLower = "tidakvalid" Then
            Dim x As Integer = 0
            For Each _objMSReg As MSPRegistration In arr
                If _objMSReg.Remarks <> String.Empty Then
                    newArray.Add(arr(x))
                    newArrayMSPCustomer.Add(arrMSPCustomer(x))
                    x += 1
                End If
            Next
            sessHelper.SetSession(strSessNewArrayMSPCustomer, newArrayMSPCustomer)
            dtgMSPRegUpload.DataSource = newArray
            dtgMSPRegUpload.DataBind()
            Dim col As DataGridColumn = dtgMSPRegUpload.Columns(dtgMSPRegUpload.Columns.Count - 1)
            col.ItemStyle.Width = Unit.Pixel(100)
            btnSave.Visible = False
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim arrToSap As New ArrayList
        arr = sessHelper.GetSession(strSessUploadExcel)
        arrMSPCustomer = sessHelper.GetSession(strSessMSPCustomer)
        Dim strSave As String = String.Empty
        If arr.Count > 0 Then

            Dim No As Integer = 1
            For i As Integer = 0 To arr.Count - 1
                objMSPReg = CType(arr(i), MSPRegistration)
                objMSPCustomer = CType(arrMSPCustomer(i), MSPCustomer)

                If objMSPReg.Remarks = String.Empty Then
                    Dim newObjMSPRegHistory As MSPRegistrationHistory = objMSPReg.MSPRegistrationHistorys(0)
                    newObjMSPRegHistory.Sequence = 1
                    newObjMSPRegHistory.SelisihAmount = newObjMSPRegHistory.MSPMaster.Amount
                    newObjMSPRegHistory.Status = CInt(EnumStatusMSP.Status.Konfirmasi)
                    ''New Checking
                    Dim CheckM As New ArrayList
                    Dim crt = New CriteriaComposite(New Criteria(GetType(MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crt.opAnd(New Criteria(GetType(MSPRegistration), "ChassisMaster.ID", MatchType.Exact, objMSPReg.ChassisMaster.ID))
                    CheckM = New MSPRegistrationFacade(User).Retrieve(crt)
                    If Not IsNothing(CheckM) AndAlso CheckM.Count > 0 Then
                        strSave += "\n" & "Data pada baris ke-" & No.ToString & ", (" & objMSPReg.ChassisMaster.ChassisNumber & ") Sudah Ada."
                        No += 1
                        Continue For
                    End If
                    ''


                    Dim intIdMSPReg As Integer = New MSPRegistrationFacade(User).Insert(objMSPReg, newObjMSPRegHistory, objMSPCustomer)
                    If intIdMSPReg = -1 Then
                        strSave += "\n" & "Data pada baris ke-" & No.ToString & " gagal tersimpan."
                    Else
                        strSave += "\n" & "Data pada baris ke-" & No.ToString & " berhasil tersimpan."
                    End If
                Else
                    strSave += "\n" & "Data pada baris ke-" & No.ToString & " gagal tersimpan."
                End If

                No += 1
            Next

            dtgMSPRegUpload.Columns(dtgMSPRegUpload.Columns.Count - 1).Visible = False
            btnSave.Visible = False
            MessageBox.Show(strSave.Substring(2, strSave.Length - 2))

        Else
            MessageBox.Show("Belum ada data yang diupload.")
        End If
    End Sub

    Protected Sub lbtnUploadTemplate_Click(sender As Object, e As EventArgs) Handles lbtnUploadTemplate.Click
        Response.Redirect("../downloadlocal.aspx?file=MSP\TemplateMSPRegistrationUpload.xlsx")
    End Sub

    Protected Sub btnMigrasi_Click(sender As Object, e As EventArgs) Handles btnMigrasi.Click
        arr = sessHelper.GetSession(strSessUploadExcel)
        arrMSPCustomer = sessHelper.GetSession(strSessMSPCustomer)
        Dim strSave As String = String.Empty
        If arr.Count > 0 Then
            Dim No As Integer = 1
            For i As Integer = 0 To arr.Count - 1
                objMSPReg = CType(arr(i), MSPRegistration)
                objMSPCustomer = CType(arrMSPCustomer(i), MSPCustomer)

                ' set status menjadi baru untuk migrasi data
                objMSPRegHistory = objMSPReg.MSPRegistrationHistorys(0)
                objMSPRegHistory.Status = EnumStatusMSP.Status.Baru
                objMSPReg.MSPRegistrationHistorys.Clear()
                objMSPReg.MSPRegistrationHistorys.Add(objMSPRegHistory)

                If objMSPReg.Remarks = String.Empty Then
                    crt = New CriteriaComposite(New Criteria(GetType(MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    If New MSPRegistrationFacade(User).Insert(objMSPReg, objMSPReg.MSPRegistrationHistorys(0), objMSPCustomer) = -1 Then
                        strSave += "\n" & "Data pada baris ke-" & No.ToString & " gagal tersimpan."
                    End If
                    No += 1
                End If
            Next

            If strSave <> String.Empty Then
                MessageBox.Show(strSave.Substring(2, strSave.Length - 2))
            Else
                ' jika berhasil simpan, sembunyikan kolom aksi dan button save
                dtgMSPRegUpload.Columns(dtgMSPRegUpload.Columns.Count - 1).Visible = False
                btnSave.Visible = False
                MessageBox.Show("Data berhasil tersimpan.")
            End If

        Else
            MessageBox.Show("Belum ada data yang diupload.")
        End If
    End Sub
End Class