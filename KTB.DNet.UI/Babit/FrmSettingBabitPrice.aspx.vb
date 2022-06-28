#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports Excel
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Parser

#End Region

Public Class FrmSettingBabitPrice
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"

    Dim objDealer As New Dealer
    Dim objLoginUser As UserInfo
    Dim objBabitMasterPrice As BabitMasterPrice
    Dim arrBabitMasterPrice As ArrayList = New ArrayList
    Private sesHelper As New SessionHelper
    Private intBabitMasterPriceID As Integer = 0
    Private intItemIndex As Integer = 0
    Private blnEditPriv As Boolean = False
    Private blnSavePriv As Boolean = False
    Private blnDeletePriv As Boolean = False
    Private blnDisplayPriv As Boolean = False
    Private blnUploadPriv As Boolean = False

    Private varSession As String = "FrmSettingBabitPrice.SessionUploadHargaBabit"
    Private varSessionIsExist As String = "FrmSettingBabitPrice.SessionUploadHargaBabitExist"
    Private varSessionNotExist As String = "FrmSettingBabitPrice.SessionUploadHargaBabitNotExis"

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property
#End Region

#Region "Custom Methods"
    Private Sub Upload()
        If (Not DataFile.PostedFile Is Nothing) AndAlso (DataFile.PostedFile.ContentLength > 0) Then
            Dim fileExt As String = Path.GetExtension(DataFile.PostedFile.FileName)
            If Not (fileExt.ToLower() = ".xls" OrElse fileExt.ToLower() = ".xlsx") Then
                MessageBox.Show("Hanya Menerima File Excell")
                Return
            End If
            dgBabitMasterPriceList.DataSource = New ArrayList
            dgBabitMasterPriceList.DataBind()

            Me.btnSaveUpload.Enabled = True

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate

            Try
                Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.ToString("yyyMMdd_HHmmss_") & SrcFile

                imp = New SAPImpersonate(_user, _password, _webServer)

                If imp.Start() Then
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, targetFile)

                    'Dim parser As IParser = New PriceParser  '-- Declare parser Price
                    ''-- Parse data file and store result into list
                    'Dim arList As ArrayList = CType(parser.ParseNoTransaction(targetFile, "User"), ArrayList)

                    Dim i As Integer = 0
                    Dim objReader As IExcelDataReader = Nothing
                    Dim ArrUpload As New ArrayList
                    Dim ArrUploadOK As New ArrayList
                    Dim isOK As Boolean = True

                    Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)

                        If fileExt.ToLower.Contains("xlsx") Then
                            objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                        Else
                            objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                        End If

                        If (Not IsNothing(objReader)) Then
                            While objReader.Read()
                                Dim isNotValidUpload As Boolean = False

                                If i >= 1 Then
                                    Dim ObjBabitMasterPrice As New BabitMasterPrice

                                    Dim StrModelName As String = ""
                                    Dim objSubCategoryVehicle As SubCategoryVehicle = New SubCategoryVehicle

                                    If IsNothing(objReader.GetString(1)) Then Exit While

                                    Try
                                        Try
                                            StrModelName = objReader.GetString(1).Trim()
                                        Catch exx As Exception
                                            Throw New Exception("Nama Model Tidak Valid")
                                        End Try

                                        If StrModelName.Trim <> "" Then
                                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                            criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Name", MatchType.Exact, StrModelName))
                                            Dim arlSubCategoryVehicle As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(criterias)
                                            If arlSubCategoryVehicle.Count > 0 Then
                                                objSubCategoryVehicle = CType(arlSubCategoryVehicle(0), SubCategoryVehicle)
                                            Else
                                                Throw New Exception("Model tidak terdaftar di Master Kategori Kendaraan")
                                            End If
                                        End If

                                    Catch ex As Exception
                                        ObjBabitMasterPrice.ErrorMessage += ex.Message & "; "
                                        isNotValidUpload = True
                                    Finally
                                        ObjBabitMasterPrice.SubCategoryVehicle = objSubCategoryVehicle
                                    End Try

                                    'UnitPrice
                                    Dim UnitPrice As Double = 0
                                    Try
                                        Dim strUnitPrice As String = objReader.GetString(2).Trim()
                                        Try
                                            UnitPrice = CDbl(strUnitPrice)
                                        Catch
                                            Throw New Exception("Format Harga/Unit salah")
                                        End Try

                                    Catch ex As Exception
                                        ObjBabitMasterPrice.ErrorMessage += ex.Message & "; "
                                        isNotValidUpload = True
                                    Finally
                                        ObjBabitMasterPrice.UnitPrice = UnitPrice
                                    End Try

                                    'ValidFrom
                                    Dim ValidFrom As DateTime = New DateTime(1973, 1, 1)
                                    Try
                                        Dim StrDate As String = objReader.GetString(3).Trim()

                                        Try
                                            ValidFrom = New DateTime(CInt(StrDate.Substring(0, 4)), CInt(StrDate.Substring(4, 2)), CInt(StrDate.Substring(6, 2)))
                                        Catch
                                            Throw New Exception("Periode Awal tidak valid")
                                        End Try
                                        If (Year(ValidFrom) <= Date.Today.Year - 20) Then
                                            Throw New Exception("Periode Awal salah")
                                        End If
                                        If ValidFrom < Date.Today Then
                                            Throw New Exception("Periode Awal tidak boleh kurang dari hari ini")
                                        End If

                                    Catch ex As Exception
                                        ObjBabitMasterPrice.ErrorMessage += ex.Message & "; "
                                        isNotValidUpload = True
                                    Finally
                                        ObjBabitMasterPrice.ValidFrom = ValidFrom
                                    End Try

                                    'ValidTo
                                    Dim ValidTo As DateTime = New DateTime(1973, 1, 1)
                                    Try
                                        Dim StrDate As String = objReader.GetString(4).Trim()

                                        Try
                                            ValidTo = New DateTime(CInt(StrDate.Substring(0, 4)), CInt(StrDate.Substring(4, 2)), CInt(StrDate.Substring(6, 2)))
                                        Catch
                                            Throw New Exception("Periode Akhir tidak valid")
                                        End Try
                                        If (Year(ValidTo) <= Date.Today.Year - 20) Then
                                            Throw New Exception("Periode Akhir salah")
                                        End If
                                        If ValidTo < ObjBabitMasterPrice.ValidFrom Then
                                            Throw New Exception("Periode Akhir tidak boleh kurang dari Periode Awal")
                                        End If

                                    Catch ex As Exception
                                        ObjBabitMasterPrice.ErrorMessage += ex.Message & "; "
                                        isNotValidUpload = True
                                    Finally
                                        ObjBabitMasterPrice.ValidTo = ValidTo
                                    End Try

                                    'SpecialCategoryFlag
                                    Dim intSpecialCategoryFlag As Short = 0
                                    Try
                                        Dim strSpecialCategoryFlag As String = objReader.GetString(5).Trim()

                                        If strSpecialCategoryFlag.ToUpper = "YES" Then
                                            intSpecialCategoryFlag = 1
                                        End If
                                        If strSpecialCategoryFlag.ToUpper = "NO" Then
                                            intSpecialCategoryFlag = 0
                                        End If
                                        If strSpecialCategoryFlag.ToUpper = "" Then
                                            intSpecialCategoryFlag = 0
                                        End If

                                    Catch ex As Exception
                                        ObjBabitMasterPrice.ErrorMessage += ex.Message & "; "
                                        isNotValidUpload = True
                                    Finally
                                        ObjBabitMasterPrice.SpecialCategoryFlag = intSpecialCategoryFlag
                                    End Try

                                    ObjBabitMasterPrice.Status = 1

                                    Try
                                        If Not IsNothing(objSubCategoryVehicle) Then
                                            If objSubCategoryVehicle.ID > 0 Then
                                                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                                criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "SubCategoryVehicle.ID", MatchType.Exact, objSubCategoryVehicle.ID))
                                                criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "ValidFrom", MatchType.Exact, ValidFrom))
                                                criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "ValidTo", MatchType.Exact, ValidTo))
                                                criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "Status", MatchType.Exact, 1))

                                                Dim arlBabitMasterPrice As ArrayList = New BabitMasterPriceFacade(User).Retrieve(criterias)
                                                If Not IsNothing(arlBabitMasterPrice) AndAlso arlBabitMasterPrice.Count > 0 Then
                                                    Throw New Exception("Model: " & ObjBabitMasterPrice.SubCategoryVehicle.Name & " di Periode ini sudah pernah di input")
                                                End If
                                            End If
                                        End If

                                    Catch ex As Exception
                                        ObjBabitMasterPrice.ErrorMessage += ex.Message & "; "
                                    Finally
                                        ObjBabitMasterPrice.ValidTo = ValidTo
                                    End Try

                                    If Not IsNothing(ObjBabitMasterPrice.ErrorMessage) AndAlso ObjBabitMasterPrice.ErrorMessage <> "" Then
                                        If isNotValidUpload = True Then
                                            isOK = False
                                        End If
                                    Else
                                        ObjBabitMasterPrice.ErrorMessage = ""
                                    End If

                                    ArrUpload.Add(ObjBabitMasterPrice)
                                End If

                                i = i + 1

                            End While
                        End If
                    End Using

                    dgBabitMasterPriceList.DataSource = ArrUpload
                    dgBabitMasterPriceList.DataBind()
                    dgBabitMasterPriceList.Visible = True
                    sesHelper.SetSession(Me.varSession, ArrUpload)
                    If isOK = True Then
                        sesHelper.SetSession("isOK", True)
                        btnSave.Enabled = True
                    Else
                        sesHelper.SetSession("isOK", False)
                        btnSave.Enabled = False
                    End If

                    btnSearch.Visible = False
                    dgBabitMasterPriceList.Columns(dgBabitMasterPriceList.Columns.Count - 1).Visible = False
                    dgBabitMasterPriceList.Columns(dgBabitMasterPriceList.Columns.Count - 2).Visible = True

                    ViewState("upload") = True
                End If

            Catch ex As Exception
                MessageBox.Show("Fail To Process")
            Finally

                imp.StopImpersonate()
                imp = Nothing
            End Try

        Else
            MessageBox.Show("Berkas Upload tidak boleh Kosong")
        End If

    End Sub

    Private Sub SaveData(ByVal _objarray As ArrayList)
        Dim IsDataOK As Boolean = True

        Try
            If CType(sesHelper.GetSession("isOK"), Boolean) = False Then
                Throw New Exception("Masih ada data bermasalah")
            End If

            Dim objBabitMasterPriceFac As BabitMasterPriceFacade
            Dim result As Integer = 0
            objBabitMasterPriceFac = New BabitMasterPriceFacade(User)

            For Each objBabitMasterPrice As BabitMasterPrice In _objarray
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "SubCategoryVehicle.ID", MatchType.Exact, objBabitMasterPrice.SubCategoryVehicle.ID))
                criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "ValidFrom", MatchType.Exact, objBabitMasterPrice.ValidFrom))
                criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "ValidTo", MatchType.Exact, objBabitMasterPrice.ValidTo))
                criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "Status", MatchType.Exact, 1))
                Dim _arrList As ArrayList = New BabitMasterPriceFacade(User).Retrieve(criterias)
                If Not IsNothing(_arrList) AndAlso _arrList.Count > 0 Then
                    Dim objDummy As BabitMasterPrice = CType(_arrList(0), BabitMasterPrice)
                    objDummy.SubCategoryVehicle = objBabitMasterPrice.SubCategoryVehicle
                    objDummy.ValidFrom = objBabitMasterPrice.ValidFrom
                    objDummy.ValidTo = objBabitMasterPrice.ValidTo
                    objDummy.UnitPrice = objBabitMasterPrice.UnitPrice
                    objDummy.Status = objBabitMasterPrice.Status
                    objDummy.SpecialCategoryFlag = objBabitMasterPrice.SpecialCategoryFlag
                    result = objBabitMasterPriceFac.Update(objDummy)
                Else
                    result = objBabitMasterPriceFac.Insert(objBabitMasterPrice)
                End If
            Next
            Dim ArrUpload As New ArrayList
            sesHelper.SetSession(varSession, ArrUpload)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Function BindVehicleSubCategoryToDDL(ByVal ddlSubCategory As DropDownList, ByVal intDdlCategoryID As Integer)
        ddlSubCategory.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.Exact, ""))  '-- Type still active
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.Exact, intDdlCategoryID))
        Dim arrList As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(criterias)
        With ddlSubCategory.Items
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each obj As SubCategoryVehicle In arrList
                .Add(New ListItem(obj.Name, obj.ID))
            Next
        End With
        ddlSubCategory.SelectedIndex = 0
    End Function

    Private Sub disableform(isEnb As Boolean)
        Me.ddlCategory.Enabled = isEnb
        Me.ddlSubCategory.Enabled = isEnb
        Me.icPeriodeStart.Enabled = isEnb
        Me.icPeriodeEnd.Enabled = isEnb
        Me.txtUnitPrice.Enabled = isEnb
        Me.ddlStatus.Enabled = isEnb
        Me.ddlSpecialCategoryFlag.Enabled = isEnb
        Me.ddlSpecialFlag.Enabled = isEnb

        Me.btnSave.Enabled = blnSavePriv
    End Sub

    Sub BindddlStatus()
        With ddlStatus
            .Items.Clear()
            .Items.Add(New ListItem("Aktif", 1))
            .Items.Add(New ListItem("Tidak Aktif", 0))
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Piih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Sub BindddlSpecialCategoryFlag()
        With ddlSpecialCategoryFlag
            .Items.Clear()
            .Items.Add(New ListItem("Ya", 1))
            .Items.Add(New ListItem("Tidak", 0))
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Piih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Sub BindddlSpecialFlag()
        With ddlSpecialFlag
            .Items.Clear()
            .Items.Add(New ListItem("Ya", 1))
            .Items.Add(New ListItem("Tidak", 0))
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Piih ", -1))
            .SelectedIndex = 2
        End With
    End Sub

    Private Sub BindDDLCategory()
        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, "MMC"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
        BindVehicleSubCategoryToDDL(ddlSubCategory, ddlCategory.SelectedValue)
    End Sub

    Private Sub BindGridList(ByVal index As Integer, Optional ByVal sortColoum As String = Nothing, Optional ByVal sortType As Sort.SortDirection = Sort.SortDirection.ASC)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlSubCategory.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "SubCategoryVehicle.ID", MatchType.Exact, ddlSubCategory.SelectedValue))
        End If
        If ddlStatus.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If
        If ddlSpecialCategoryFlag.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "SpecialCategoryFlag", MatchType.Exact, ddlSpecialCategoryFlag.SelectedValue))
        End If
        If ddlSpecialFlag.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "SpecialFlag", MatchType.Exact, ddlSpecialFlag.SelectedValue))
        End If

        If chkConfirmPeriod.Checked Then
            '-- Periode Konfirmasi
            Dim periodStart As New DateTime(CInt(icPeriodeStart.Value.Year), CInt(icPeriodeStart.Value.Month), CInt(icPeriodeStart.Value.Day), 0, 0, 0)
            Dim periodEnd As New DateTime(CInt(icPeriodeEnd.Value.Year), CInt(icPeriodeEnd.Value.Month), CInt(icPeriodeEnd.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "ValidFrom", MatchType.GreaterOrEqual, Format(periodStart, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "ValidTo", MatchType.LesserOrEqual, Format(periodEnd, "yyyy-MM-dd HH:mm:ss")))
        End If

        sortColoum = ViewState("currentSortColumn")
        sortType = ViewState("currentSortDirection")
        _arrList = New BabitMasterPriceFacade(User).RetrieveActiveList(index + 1, dgBabitMasterPriceList.PageSize, totalRow, sortColoum, sortType, criterias)

        Dim arForDisplay As New ArrayList
        arForDisplay.Add(ddlCategory.SelectedValue)
        arForDisplay.Add(ddlSubCategory.SelectedValue)
        arForDisplay.Add(icPeriodeStart.Value)
        arForDisplay.Add(icPeriodeEnd.Value)
        arForDisplay.Add(ddlStatus.SelectedValue)
        arForDisplay.Add(ddlSpecialCategoryFlag.SelectedValue)
        arForDisplay.Add(ddlSpecialFlag.SelectedValue)
        arForDisplay.Add(txtUnitPrice.Text)
        sesHelper.SetSession("SessionFrmSettingBabitPrice", arForDisplay)

        dgBabitMasterPriceList.VirtualItemCount = totalRow
        dgBabitMasterPriceList.DataSource = _arrList
        dgBabitMasterPriceList.DataBind()
    End Sub

    Private Sub DeleteBabitMasterPrice(ByVal intBabitMasterPriceID As Integer)
        Dim objBabitMasterPriceFacade As BabitMasterPriceFacade = New BabitMasterPriceFacade(User)

        Dim objBabitMasterPrice As BabitMasterPrice = objBabitMasterPriceFacade.Retrieve(intBabitMasterPriceID)
        objBabitMasterPrice.RowStatus = CType(DBRowStatus.Deleted, Short)

        Dim _result As Integer = 0
        _result = New BabitMasterPriceFacade(User).Update(objBabitMasterPrice)
        If _result > 0 Then
            MessageBox.Show("Delete Data Sukses")
        End If
        ClearAll()
        BindGridList(dgBabitMasterPriceList.CurrentPageIndex)
    End Sub

    Private Sub LoadDataBabitMasterPrice(intBabitMasterPriceID As Integer)
        Dim objBabitMasterPrice As BabitMasterPrice = New BabitMasterPriceFacade(User).Retrieve(intBabitMasterPriceID)
        If Not IsNothing(objBabitMasterPrice) Then
            Me.icPeriodeStart.Value = objBabitMasterPrice.ValidFrom
            Me.icPeriodeEnd.Value = objBabitMasterPrice.ValidTo
            Me.txtUnitPrice.Text = Format(objBabitMasterPrice.UnitPrice, "###,###,###.00")
            Me.ddlStatus.SelectedValue = objBabitMasterPrice.Status.ToString
            Me.ddlSpecialCategoryFlag.SelectedValue = objBabitMasterPrice.SpecialCategoryFlag.ToString
            Me.ddlSpecialFlag.SelectedValue = objBabitMasterPrice.SpecialFlag.ToString
            Me.ddlCategory.SelectedValue = objBabitMasterPrice.SubCategoryVehicle.Category.ID
            ddlCategory_SelectedIndexChanged(Nothing, Nothing)
            Me.ddlSubCategory.SelectedValue = objBabitMasterPrice.SubCategoryVehicle.ID
            sesHelper.SetSession("sessBabitMasterPrice", objBabitMasterPrice)
        End If
    End Sub

    Private Sub ClearAll()
        hdnBabitMasterPriceID.Value = ""
        ddlCategory.SelectedValue = "-1"
        ddlSubCategory.SelectedValue = "-1"
        icPeriodeStart.Value = Date.Now
        icPeriodeEnd.Value = Date.Now
        txtUnitPrice.Text = "0"
        ddlStatus.SelectedIndex = 0
        ddlSpecialCategoryFlag.SelectedIndex = 0
        ddlSpecialFlag.SelectedIndex = 2
        divNavigationButton.Visible = True
        btnSearch.Visible = True
        btnBatal.Visible = blnSavePriv
        btnSave.Visible = blnSavePriv
        btnUpload.Visible = blnUploadPriv
        btnSaveUpload.Visible = False
        sesHelper.SetSession("sessBabitMasterPrice", New BabitMasterPrice)
        dgBabitMasterPriceList.Columns(dgBabitMasterPriceList.Columns.Count - 2).Visible = False
        dgBabitMasterPriceList.Columns(dgBabitMasterPriceList.Columns.Count - 3).Visible = True
        dgBabitMasterPriceList.Columns(dgBabitMasterPriceList.Columns.Count - 1).Visible = True
        ViewState("upload") = False
        sesHelper.SetSession("isOK", True)
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If (ddlSubCategory.SelectedValue = "-1") Then
            sb.Append("Kategori Harus Diisi\n")
        End If

        If (ddlSubCategory.SelectedValue = "-1") Then
            sb.Append("Sub Kategori Harus Diisi\n")
        End If

        If (icPeriodeStart.Value > icPeriodeEnd.Value) Then
            sb.Append("Periode awal harus lebih kecil atau sama dengan periode akhir\n")
        End If

        If (txtUnitPrice.Text.Trim = String.Empty OrElse txtUnitPrice.Text.Trim = "0") Then
            sb.Append("Unit Price Harus Diisi\n")
        End If
        If (ddlStatus.SelectedValue = "-1") Then
            sb.Append("Status Harus Diisi\n")
        End If
        If (Me.ddlSpecialCategoryFlag.SelectedIndex = 0) Then
            sb.Append("Babit Kategori Spesial Harus Diisi\n")
        End If
        If (Me.ddlSpecialFlag.SelectedIndex = 0) Then
            sb.Append("Babit Spesial Harus Diisi\n")
        End If

        Return sb.ToString()
    End Function

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Setting_Harga_Babit_Display_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=MASTER - SETTING HARGA BABIT")
        End If

        blnDisplayPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Setting_Harga_Babit_Display_Privilege)
        blnEditPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Setting_Harga_Babit_Edit_Privilege)
        blnDeletePriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Setting_Harga_Babit_Delete_Privilege)
        blnSavePriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Setting_Harga_Babit_Simpan_Privilege)
        blnUploadPriv = SecurityProvider.Authorize(Context.User, SR.BABIT_Master_Setting_Harga_Babit_Upload_Privilege)
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        objDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        objLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim intId As Integer = 0
        InitiateAuthorization()

        If (Not IsPostBack) Then
            BindDDLCategory()
            BindddlStatus()
            BindddlSpecialCategoryFlag()
            BindddlSpecialFlag()
            ClearAll()

            ViewState("currentSortColumn") = "ID"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC

            BindGridList(dgBabitMasterPriceList.CurrentPageIndex)

            If Not IsNothing(Request.QueryString("Mode")) AndAlso CType(Request.QueryString("Mode"), String).Trim <> "" Then
                If Not IsNothing(Request.QueryString("BabitMasterPriceID")) Then
                    hdnBabitMasterPriceID.Value = Request.QueryString("BabitMasterPriceID")
                    LoadDataBabitMasterPrice(hdnBabitMasterPriceID.Value)
                End If

                btnSaveUpload.Visible = False
                btnSave.Visible = False
                btnBatal.Visible = False
                btnSearch.Visible = True
                btnUpload.Enabled = False
                btnSearch.Text = "Batal"
                If Request.QueryString("Mode") = "View" Then
                    disableform(False)
                ElseIf Request.QueryString("Mode") = "Edit" Then
                    disableform(True)
                    btnSave.Visible = blnSavePriv
                    btnSave.Enabled = True
                End If
            End If
        Else
            Dim ArrUpload As ArrayList = New ArrayList
            If (Request.Form("hdnIsExsistModel")) = "1" Then
                ArrUpload = CType(sesHelper.GetSession(varSession), ArrayList)
            Else
                ArrUpload = CType(sesHelper.GetSession(varSessionNotExist), ArrayList)
            End If
            If Not IsNothing(ArrUpload) AndAlso ArrUpload.Count > 0 Then
                SaveData(ArrUpload)
                MessageBox.Show("Simpan Upload Data Berhasil !")
                btnBatal_Click(Nothing, Nothing)
            End If
            hdnIsExsistModel.Value = "-1"
        End If
    End Sub

    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        BindVehicleSubCategoryToDDL(ddlSubCategory, ddlCategory.SelectedValue)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String

        If ViewState("upload") = True Then
            btnSaveUpload_Click(Nothing, Nothing)
        Else
            str = ValidateData()
            If (str.Length > 0) Then
                MessageBox.Show(str)
                Exit Sub
            End If

            If ddlSubCategory.SelectedValue <> "-1" Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "SubCategoryVehicle.ID", MatchType.Exact, ddlSubCategory.SelectedValue))
                criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "ValidFrom", MatchType.Exact, icPeriodeStart.Value))
                criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "ValidTo", MatchType.Exact, icPeriodeEnd.Value))
                criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "Status", MatchType.Exact, 1))
                Dim arlBabitMasterPrice As ArrayList = New BabitMasterPriceFacade(User).Retrieve(criterias)
                If Not IsNothing(arlBabitMasterPrice) AndAlso arlBabitMasterPrice.Count > 0 Then
                    For Each objBabitMasterPrice As BabitMasterPrice In arlBabitMasterPrice
                        If hdnBabitMasterPriceID.Value.ToString <> objBabitMasterPrice.ID.ToString Then
                            MessageBox.Show("Model: " & ddlSubCategory.SelectedItem.Text & " di Periode tersebut sudah pernah di input")
                            Exit Sub
                            Exit For
                        End If
                    Next
                End If
            End If

            Dim _BabitMasterPrice As BabitMasterPrice
            If Request.QueryString("Mode") <> "Edit" Then
                _BabitMasterPrice = New BabitMasterPrice
            Else
                _BabitMasterPrice = CType(sesHelper.GetSession("sessBabitMasterPrice"), BabitMasterPrice)
            End If

            _BabitMasterPrice.SubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(CType(ddlSubCategory.SelectedValue, Short))
            _BabitMasterPrice.ValidFrom = Me.icPeriodeStart.Value

            _BabitMasterPrice.ValidTo = Me.icPeriodeEnd.Value

            _BabitMasterPrice.UnitPrice = Me.txtUnitPrice.Text

            _BabitMasterPrice.Status = ddlStatus.SelectedValue   '-- 1 = Status Aktif, 0 = Status tidak aktif

            _BabitMasterPrice.SpecialCategoryFlag = ddlSpecialCategoryFlag.SelectedValue   '-- 1 = Special, 0 = Non Special
            _BabitMasterPrice.SpecialFlag = ddlSpecialFlag.SelectedValue   '-- 1 = Special, 0 = Non Special

            Dim _result As Integer = 0
            If Request.QueryString("Mode") = "Edit" Then
                _result = New BabitMasterPriceFacade(User).Update(_BabitMasterPrice)
            Else
                _result = New BabitMasterPriceFacade(User).Insert(_BabitMasterPrice)
            End If

            If _result > 0 Then
                ClearAll()
                MessageBox.Show("Simpan Data Berhasil !")
                Server.Transfer("~/Babit/FrmSettingBabitPrice.aspx?BabitMasterPriceID=" & _result)
            Else
                MessageBox.Show("Simpan Data Gagal")
            End If
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Me.ClearAll()
        BindGridList(dgBabitMasterPriceList.CurrentPageIndex)
    End Sub

    Private Sub dgBabitMasterPriceList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBabitMasterPriceList.ItemCommand
        Select Case e.CommandName
            Case "View"
                Response.Redirect("~/Babit/FrmSettingBabitPrice.aspx?Mode=View&BabitMasterPriceID=" & CInt(e.CommandArgument))
            Case "Edit"
                Response.Redirect("~/Babit/FrmSettingBabitPrice.aspx?Mode=Edit&BabitMasterPriceID=" & CInt(e.CommandArgument))
            Case "Delete"
                DeleteBabitMasterPrice(CInt(e.CommandArgument))
        End Select
    End Sub

    Private Sub dgBabitMasterPriceList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBabitMasterPriceList.ItemDataBound

        If Not IsNothing(e.Item.DataItem) Then
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim objDomain As BabitMasterPrice = CType(e.Item.DataItem, BabitMasterPrice)

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (e.Item.ItemIndex + 1 + (dgBabitMasterPriceList.CurrentPageIndex * dgBabitMasterPriceList.PageSize)).ToString()

                Dim imgActif As HtmlImage = CType(e.Item.FindControl("imgActif"), HtmlImage)
                Dim imgNonActif As HtmlImage = CType(e.Item.FindControl("imgNonActif"), HtmlImage)
                If objDomain.Status = 0 Then
                    imgActif.Visible = False
                    imgNonActif.Visible = True
                Else
                    imgActif.Visible = True
                    imgNonActif.Visible = False
                End If

                Dim chkSpecialCategoryFlag As CheckBox = CType(e.Item.FindControl("chkSpecialCategoryFlag"), CheckBox)
                If objDomain.SpecialCategoryFlag = 0 Then
                    chkSpecialCategoryFlag.Checked = False
                    chkSpecialCategoryFlag.BackColor = Color.Green
                Else
                    chkSpecialCategoryFlag.Checked = True
                    chkSpecialCategoryFlag.BackColor = Color.Blue
                End If
                'Dim chkSpecialFlag As CheckBox = CType(e.Item.FindControl("chkSpecialFlag"), CheckBox)
                'If objDomain.SpecialFlag = 0 Then
                '    chkSpecialFlag.Checked = False
                '    chkSpecialFlag.BackColor = Color.Green
                'Else
                '    chkSpecialFlag.Checked = True
                '    chkSpecialFlag.BackColor = Color.Blue
                'End If

                Dim lblStatusUpload As Label = CType(e.Item.FindControl("lblStatusUpload"), Label)
                If Not IsNothing(objDomain.ErrorMessage) Then
                    If objDomain.ErrorMessage.Trim = "" Then
                        lblStatusUpload.Text = "Upload Valid"
                        lblStatusUpload.ForeColor = Color.Blue
                    Else
                        lblStatusUpload.Text = objDomain.ErrorMessage.Trim
                        lblStatusUpload.ForeColor = Color.Red
                    End If
                End If

                Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)
                Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
                Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)

                lnkbtnView.Visible = blnDisplayPriv
                lnkbtnEdit.Visible = blnEditPriv
                lnkbtnDelete.Visible = blnDeletePriv
            End If
        End If
    End Sub

    Private Sub dgBabitMasterPriceList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgBabitMasterPriceList.PageIndexChanged
        dgBabitMasterPriceList.CurrentPageIndex = e.NewPageIndex
        BindGridList(dgBabitMasterPriceList.CurrentPageIndex)
    End Sub

    Private Sub dgBabitMasterPriceList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgBabitMasterPriceList.SortCommand
        Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)
            Case Sort.SortDirection.ASC
                ViewState("currentSortDirection") = Sort.SortDirection.DESC

            Case Sort.SortDirection.DESC
                ViewState("currentSortDirection") = Sort.SortDirection.ASC
        End Select

        ViewState("currentSortColumn") = e.SortExpression

        dgBabitMasterPriceList.SelectedIndex = -1
        dgBabitMasterPriceList.CurrentPageIndex = 0
        BindGridList(dgBabitMasterPriceList.CurrentPageIndex, e.SortExpression, ViewState("currentSortDirection"))
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If btnSearch.Text = "Batal" Then
            Response.Redirect("~/Babit/FrmSettingBabitPrice.aspx")
            Exit Sub
        End If
        dgBabitMasterPriceList.CurrentPageIndex = 0
        BindGridList(dgBabitMasterPriceList.CurrentPageIndex)
    End Sub

    Protected Sub LnkDownloadTemplate_Click(sender As Object, e As EventArgs) Handles LnkDownloadTemplate.Click
        Dim strName As String = "Template_Setting_Harga_Babit.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Babit\" & strName)
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If Not Page.IsValid Then  '-- Postback validation
            Exit Sub
        End If
        Upload()
    End Sub

    Private Sub btnSaveUpload_Click(sender As Object, e As EventArgs) Handles btnSaveUpload.Click
        Dim ArrUpload As New ArrayList
        Dim ArrUploadIsExist As New ArrayList
        Dim ArrUploadNotExist As New ArrayList
        Dim strModelName As String = String.Empty
        ArrUpload = CType(sesHelper.GetSession(varSession), ArrayList)

        For Each objBabitMasterPrice As BabitMasterPrice In ArrUpload
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "SubCategoryVehicle.ID", MatchType.Exact, objBabitMasterPrice.SubCategoryVehicle.ID))
            criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "ValidFrom", MatchType.Exact, objBabitMasterPrice.ValidFrom))
            criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "ValidTo", MatchType.Exact, objBabitMasterPrice.ValidTo))
            criterias.opAnd(New Criteria(GetType(BabitMasterPrice), "Status", MatchType.Exact, 1))
            Dim arlBabitMasterPrice As ArrayList = New BabitMasterPriceFacade(User).Retrieve(criterias)
            If Not IsNothing(arlBabitMasterPrice) AndAlso arlBabitMasterPrice.Count > 0 Then
                ArrUploadIsExist.Add(objBabitMasterPrice)
                If strModelName = "" Then
                    strModelName = "\n- " & objBabitMasterPrice.SubCategoryVehicle.Name & "||" & objBabitMasterPrice.ValidFrom.ToString & " s.d " & objBabitMasterPrice.ValidTo.ToString
                Else
                    strModelName += "\n- " & objBabitMasterPrice.SubCategoryVehicle.Name & "||" & objBabitMasterPrice.ValidFrom.ToString & " s.d " & objBabitMasterPrice.ValidTo.ToString
                End If
            Else
                ArrUploadNotExist.Add(objBabitMasterPrice)
            End If
        Next
        sesHelper.SetSession(varSessionIsExist, ArrUploadIsExist)
        sesHelper.SetSession(varSessionNotExist, ArrUploadNotExist)

        If Not IsNothing(ArrUploadIsExist) AndAlso ArrUploadIsExist.Count > 0 Then
            MessageBox.Confirm("Model: " & strModelName & " sudah pernah di input, Apakah mau melanjutkan simpan data ini ?", "hdnIsExsistModel")
            Exit Sub
        Else
            If Not IsNothing(ArrUpload) AndAlso ArrUpload.Count > 0 Then
                SaveData(ArrUpload)
                MessageBox.Show("Simpan Upload Data Berhasil !")
                btnBatal_Click(Nothing, Nothing)
            Else
                MessageBox.Show("Data upload masih kosong")
            End If
        End If
    End Sub

#End Region

End Class
