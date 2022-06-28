#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.AfterSales
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.WebCC
Imports MMKSI.DNetUpload.Utility
Imports System.Collections.Generic
Imports OfficeOpenXml
Imports GlobalExtensions
Imports KTB.DNet.BusinessValidation
Imports System.Linq
Imports System.Security.Principal


#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Reflection

#End Region

Public Class PopUpUploadMaintainGeneralRepair
    Inherits System.Web.UI.Page
#Region " Private fields "
    Private sessHelper As SessionHelper = New SessionHelper
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private m_bInputPrivilege As Boolean = False
    Private stdFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Private vechileModelFacade As VechileModelFacade = New VechileModelFacade(User)
    Private fSKindFacade As FSKindFacade = New FSKindFacade(User)
    Private pMKindFacade As PMKindFacade = New PMKindFacade(User)
    Private recallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)
    Private serviceBookingFacade As ServiceBookingFacade = New ServiceBookingFacade(User)
    Private freeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
    Private recallServiceFacade As RecallServiceFacade = New RecallServiceFacade(User)
    Private pmHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
    Private vechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
    Private chassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
    Private dealerFacade As DealerFacade = New DealerFacade(User)
    Private stallMasterFacade As StallMasterFacade = New StallMasterFacade(User)
    Private assistServiceTypeFacade As AssistServiceTypeFacade = New AssistServiceTypeFacade(User)
    Dim isDealerDMS As Boolean = False
    Private isDealerPiloting As Boolean = False
    Private _arlServStdTime As ArrayList = New ArrayList
    Private arrSST As ArrayList = New ArrayList
    Private objSrvST As New ServiceStandardTime
#End Region
#Region "Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)

        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            'cek maxFileSize first
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
            'Start  :Donas,20140603:failed to upload by dealer (specific)
            'If 1 = 2 AndAlso DataFile.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
            '     DataFile.PostedFile.ContentType.ToString <> "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Then
            If DataFile.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
             DataFile.PostedFile.ContentType.ToString <> "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Then
                'DataFile.PostedFile.ContentType.ToString <> "application/octet-stream" Then

                'If Not (DataFile.PostedFile.ContentType.ToString = "application/vnd.ms-excel" Or _
                '            DataFile.PostedFile.ContentType.ToString = "application/octet-stream" Or _
                '            DataFile.PostedFile.ContentType.ToString = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" Or _
                '            DataFile.PostedFile.ContentType.ToString = "application/x-zip-compressed") Then
                MessageBox.Show("Extension file tidak sesuai. Ubah ke *.xlsx (excel 2007).")
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

            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)   '-- Source file name
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
                    Dim parser As UploadMaintainGeneralRepairService = New UploadMaintainGeneralRepairService(objDealer, DataFile.PostedFile.ContentType.ToString)

                    '-- Parse data file and store result into arraylist
                    'Dim arlCustomer As ArrayList = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1$]", "User"), ArrayList)
                    Dim sheets() As String = {"[Sheet1$A1:S152]", "[Sheet2$A1:S152]"}
                    Dim arlCustomer As ArrayList = CType(parser.ParsingExcelMultiSheet(TempFile, sheets, "User"), ArrayList)
                    If arlCustomer.Count > 0 Then
                        'Dim arlCustomerDetail As ArrayList = SortArraylist(arlCustomer, GetType(ServiceTemplateGRPartDetail), "ErrorMessage", Sort.SortDirection.DESC)
                        'arlCustomer = SortArraylist(arlCustomer, GetType(ServiceTemplateGRLabor), "ErrorMessage", Sort.SortDirection.DESC)

                        'arlCustomer.Sort()
                        'arlCustomer = arlCustomer.OrderByDescending(Function(x) x.isPrimary).ThenBy(Function(x) x.id).ToList()
                        'Dim tt As List(Of extra) = allExtras.ToList()
                        If parser.IsAllDataValid Then
                            btnSimpan.Enabled = True
                        Else
                            btnSimpan.Enabled = False
                        End If
                    Else
                        'If Not IsNothing(parser.GetErrorMessage()) Then
                        '    MessageBox.Show(parser.GetErrorMessage())
                        'Else
                        '    MessageBox.Show("Tidak ada data yang diupload dari excel.")
                        'End If
                        'MessageBox.Show(parser.GetErrorMessage())
                        MessageBox.Show("Tidak ada data yang diinsert/update")
                        btnSimpan.Enabled = False
                    End If

                    sessHelper.SetSession("MaintainGeneralRepair", arlCustomer)
                    'objSrvST = CType(sessHelper.GetSession("ServiceStandardTime"), ServiceStandardTime) 'sessHelper.SetSession("ServiceStandardTime", arlCustomer)
                    dtgServiceStandardTime.Visible = True
                    dtgServiceStandardTime.DataSource = Nothing  '-- Reset datagrid first
                    dtgServiceStandardTime.CurrentPageIndex = 0
                    dtgServiceStandardTimeDetail.Visible = True
                    dtgServiceStandardTimeDetail.DataSource = Nothing
                    dtgServiceStandardTimeDetail.CurrentPageIndex = 0
                    BindUploadServiceStandardTime()
                End If

            Catch Exc As Exception
                imp.StopImpersonate()
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If

    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs)
        'ResetControl()
        'ReadData()
        'dtgServiceStandardTime.CurrentPageIndex = 0
        'BindPage(dtgServiceStandardTime.CurrentPageIndex)
        dtgServiceStandardTime.DataSource = Nothing
        dtgServiceStandardTime.Visible = False
        dtgServiceStandardTimeDetail.DataSource = Nothing
        dtgServiceStandardTimeDetail.Visible = False
        Session.Remove("MaintainGeneralRepair")
        btnSimpan.Enabled = False
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs)
        'Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        '_arlServStdTime = sessHelper.GetSession("MaintainGeneralRepair")
        Dim _arlSST As ArrayList = sessHelper.GetSession("MaintainGeneralRepair")
        Dim _arrServiceTemplateGRLabor As ArrayList = New ArrayList
        Dim _arrServiceTemplateGRPartDetail As ArrayList = New ArrayList
        Dim objGRLabor As ServiceTemplateGRLabor = New ServiceTemplateGRLabor
        Dim objGRLaborPartDetail As ServiceTemplateGRPartDetail = New ServiceTemplateGRPartDetail
        Dim _nResult As Integer = 0
        Dim test As Integer = 0
        Dim strAssist As String = ""

        For i As Integer = 0 To _arlSST.Count - 1
            If _arlSST.Item(i).GetType() = GetType(ServiceTemplateGRLabor) Then
                _arrServiceTemplateGRLabor.Add(_arlSST.Item(i))
            Else
                _arrServiceTemplateGRPartDetail.Add(_arlSST.Item(i))
            End If
        Next
        'objSST = CType(_arlSST(0), ServiceStandardTime)
        If Not IsNothing(_arlSST) Then
            If _arrServiceTemplateGRLabor.Count > 0 Or _arrServiceTemplateGRPartDetail.Count > 0 Then
                Dim _errMsg As StringBuilder = New StringBuilder
                Dim oFacade As New ServiceTemplateGRLaborFacade(User)
                Dim oFacade2 As New ServiceTemplateGRPartDetailFacade(User)
                Dim iSuccess As Integer = 0
                For Each _sst As ServiceTemplateGRLabor In _arrServiceTemplateGRLabor
                    test = test + 1
                    If _sst.ErrorMessage = String.Empty Then

                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRLabor), "Variants", MatchType.Exact, _sst.Variants))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRLabor), "GRKindID", MatchType.Exact, _sst.GRKindID))

                        Dim arlSST As ArrayList = oFacade.Retrieve(criterias)
                        If arlSST.Count > 0 Then
                            objGRLabor = CType(arlSST(0), ServiceTemplateGRLabor)
                            _sst.ID = objGRLabor.ID
                            _nResult = oFacade.Update(_sst)
                        Else
                            _nResult = oFacade.Insert(_sst)
                        End If
                    Else
                        MessageBox.Show(test.ToString())
                    End If
                Next
                If _nResult = -1 Then
                    MessageBox.Show("Service Template GR Labor " & SR.SaveFail)
                ElseIf _nResult > 0 Then
                    MessageBox.Show("Service Template GR Labor " & SR.SaveSuccess)
                Else

                End If

                For Each _sst As ServiceTemplateGRPartDetail In _arrServiceTemplateGRPartDetail
                    test = test + 1
                    If _sst.ErrorMessage = String.Empty Then

                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRPartDetail), "ServiceTemplateGRLaborID", MatchType.Exact, _sst.ServiceTemplateGRLaborID))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRPartDetail), "SparePartMasterID", MatchType.Exact, _sst.SparePartMasterID))

                        Dim arlSST As ArrayList = oFacade2.Retrieve(criterias)
                        If arlSST.Count > 0 Then
                            objGRLaborPartDetail = CType(arlSST(0), ServiceTemplateGRPartDetail)
                            _sst.ID = objGRLaborPartDetail.ID
                            _nResult = oFacade2.Update(_sst)
                            
                        Else
                            _nResult = oFacade2.Insert(_sst)
                        End If
                    Else
                        MessageBox.Show(test.ToString())
                    End If
                Next
                If _nResult = -1 Then
                    MessageBox.Show("Service Template GR Labor Detail " & SR.SaveFail)
                ElseIf _nResult > 0 Then
                    'MessageBox.Show("Service Template GR Labor Detail " & SR.SaveSuccess)
                    RegisterStartupScript("Close", "<script>onSuccess();</script>")
                    'ViewState.Add("vsProcess", "Add")
                Else

                End If

            End If


            'Page_Load(Nothing, EventArgs.Empty)
            dtgServiceStandardTime.Visible = False
            btnSimpan.Enabled = False
        Else
            MessageBox.Show("Lakukan Upload Excel sebelum Simpan.")
        End If
    End Sub

    Private Sub dtgServiceStandardTime_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgServiceStandardTime.PageIndexChanged
        '-- Change datagrid page

        dtgServiceStandardTime.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)

    End Sub
    Private Sub dtgServiceStandardTimeDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgServiceStandardTimeDetail.PageIndexChanged
        '-- Change datagrid page

        dtgServiceStandardTimeDetail.CurrentPageIndex = e.NewPageIndex
        BindPageDetail(e.NewPageIndex)

    End Sub
    Private Sub dtgServiceStandardTime_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServiceStandardTime.ItemDataBound
        Dim objARS As GRKind
        If Not e.Item.DataItem Is Nothing Then
            Try
                'Dim objContact As CcContact = e.Item.DataItem

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lbJenisService As Label = CType(e.Item.FindControl("lbJenisService"), Label)
                Dim lbKindCode As Label = CType(e.Item.FindControl("lbKindCode"), Label)
                Dim lblGRKindID As Label = CType(e.Item.FindControl("lblGRKindID"), Label)
                Dim lbJasaService As Label = CType(e.Item.FindControl("lbJasaService"), Label)
                Dim lbDurasiService As Label = CType(e.Item.FindControl("lbDurasiService"), Label)
                Dim lbMulaiBerlaku As Label = CType(e.Item.FindControl("lbMulaiBerlaku"), Label)
                Dim dt As Date = Date.Parse(lbMulaiBerlaku.Text)
                lbMulaiBerlaku.Text = CType(dt, String)

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.GRKind), "ID", MatchType.Exact, lblGRKindID.Text))

                Dim arrGRKind As ArrayList = New GRKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                If arrGRKind.Count > 0 Then
                    Dim itemGRKind As GRKind = arrGRKind.Item(0)
                    lbJenisService.Text = itemGRKind.KindDescription
                    lbKindCode.Text = itemGRKind.KindCode
                Else
                    lbJenisService.Text = String.Empty
                    lbKindCode.Text = String.Empty
                End If

                If lbJasaService.Text = "0" Then
                    lbJasaService.Text = String.Empty
                End If
                If lbDurasiService.Text = "0" Then
                    lbDurasiService.Text = String.Empty
                End If
                If lbMulaiBerlaku.Text = "01/01/1753 00.00.00" Then
                    lbMulaiBerlaku.Text = String.Empty
                End If
                lblNo.Text = (dtgServiceStandardTime.CurrentPageIndex * dtgServiceStandardTime.PageSize + e.Item.ItemIndex + 1).ToString
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub dtgServiceStandardTimeDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServiceStandardTimeDetail.ItemDataBound
        Dim objARS As GRKind
        If Not e.Item.DataItem Is Nothing Then
            Try
                'Dim objContact As CcContact = e.Item.DataItem

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lbJenisServiceDetail As Label = CType(e.Item.FindControl("lbJenisServiceDetail"), Label)
                Dim lbKindCodeDetail As Label = CType(e.Item.FindControl("lbKindCodeDetail"), Label)
                Dim lblServiceTemplateGRId As Label = CType(e.Item.FindControl("lblServiceTemplateGRId"), Label)
                Dim lblSparepartMasterId As Label = CType(e.Item.FindControl("lblSparepartMasterId"), Label)
                Dim lbVariantDetail As Label = CType(e.Item.FindControl("lbVariantDetail"), Label)
                Dim lbNamaSaprepart As Label = CType(e.Item.FindControl("lbNamaSaprepart"), Label)
                Dim lbKodeSparepart As Label = CType(e.Item.FindControl("lbKodeSparepart"), Label)
                Dim lbHargaSatuan As Label = CType(e.Item.FindControl("lbHargaSatuan"), Label)
                Dim lbJumlahUnit As Label = CType(e.Item.FindControl("lbJumlahUnit"), Label)

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRLabor), "ID", MatchType.Exact, lblServiceTemplateGRId.Text))
                Dim arrServiceTemplateGRLabor As ArrayList = New ServiceTemplateGRLaborFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                If arrServiceTemplateGRLabor.Count > 0 Then
                    Dim item As ServiceTemplateGRLabor = arrServiceTemplateGRLabor.Item(0)
                    Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.GRKind), "ID", MatchType.Exact, item.GRKindID))
                    Dim arrGRKind As ArrayList = New GRKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias2)
                    If arrGRKind.Count > 0 Then
                        Dim itemGRKind As GRKind = arrGRKind.Item(0)
                        lbJenisServiceDetail.Text = itemGRKind.KindDescription
                        lbKindCodeDetail.Text = itemGRKind.KindCode
                    Else
                        lbJenisServiceDetail.Text = String.Empty
                        lbKindCodeDetail.Text = String.Empty
                    End If
                    lbVariantDetail.Text = item.Variants
                Else
                    lbVariantDetail.Text = String.Empty
                    lbJenisServiceDetail.Text = String.Empty
                    lbKindCodeDetail.Text = String.Empty
                End If

                criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartMaster), "ID", MatchType.Exact, lblSparepartMasterId.Text))
                Dim arrSparepartMaster As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                If arrSparepartMaster.Count > 0 Then
                    Dim spm As SparePartMaster = arrSparepartMaster.Item(0)
                    lbNamaSaprepart.Text = spm.PartName
                    lbKodeSparepart.Text = spm.PartNumber
                    lbHargaSatuan.Text = spm.RetalPrice
                Else
                    lbNamaSaprepart.Text = String.Empty
                    lbKodeSparepart.Text = String.Empty
                    lbHargaSatuan.Text = String.Empty
                End If
                lblNo.Text = (dtgServiceStandardTimeDetail.CurrentPageIndex * dtgServiceStandardTimeDetail.PageSize + e.Item.ItemIndex + 1).ToString
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
#End Region

#Region "Custom Method"
    Private Sub BindUploadServiceStandardTime()
        Dim totalRow As Integer = 0
        Dim totalRowDetail As Integer = 0
        _arlServStdTime = New ArrayList
        Dim _arrServiceTemplateGRLabor As ArrayList = New ArrayList
        Dim _arrServiceTemplateGRPartDetail As ArrayList = New ArrayList

        Try
            _arlServStdTime = sessHelper.GetSession("MaintainGeneralRepair")

            If Not IsNothing(_arlServStdTime) Then

                'objSrvST = CType(sessHelper.GetSession("ServiceStandardTime"), ServiceStandardTime) 'sessHelper.SetSession("ServiceStandardTime", arlCustomer)

                For i As Integer = 0 To _arlServStdTime.Count - 1
                    If _arlServStdTime.Item(i).GetType() = GetType(ServiceTemplateGRLabor) Then
                        _arrServiceTemplateGRLabor.Add(_arlServStdTime.Item(i))
                    Else
                        _arrServiceTemplateGRPartDetail.Add(_arlServStdTime.Item(i))
                    End If
                Next
                sessHelper.SetSession("MaintainGeneralRepair", _arlServStdTime)

                For Each _c As ServiceTemplateGRLabor In _arrServiceTemplateGRLabor
                    If _c.ErrorMessage <> String.Empty Then
                        btnSimpan.Enabled = False
                        Exit For
                    Else
                        For Each _cdetail As ServiceTemplateGRPartDetail In _arrServiceTemplateGRPartDetail
                            If _cdetail.ErrorMessage <> String.Empty Then
                                btnSimpan.Enabled = False
                                Exit For
                            End If
                        Next
                        'btnSimpan.Enabled = True
                    End If
                Next


                totalRow = _arrServiceTemplateGRLabor.Count
                dtgServiceStandardTime.DataSource = _arrServiceTemplateGRLabor
                dtgServiceStandardTime.VirtualItemCount = totalRow
                dtgServiceStandardTime.DataBind()

                totalRowDetail = _arrServiceTemplateGRPartDetail.Count
                dtgServiceStandardTimeDetail.DataSource = _arrServiceTemplateGRPartDetail
                dtgServiceStandardTimeDetail.VirtualItemCount = totalRowDetail
                dtgServiceStandardTimeDetail.DataBind()
                '           lblMessage.Text = "Jumlah data : " & _arlServStdTime.Count & " ( Valid : " & _arlValid.Count & " ; Tidak Valid : " & _arlInValid.Count & " )"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'lblMessage.Text = ""
        End Try
    End Sub
    Private Function SortArraylist(ByVal ArlToSort As ArrayList, ByVal ObjType As Type, ByVal SortColumn As String, ByVal SortDirection As Sort.SortDirection) As ArrayList

        Dim isDeepSort As Boolean = (SortColumn.IndexOf(".") <> -1)

        Dim i As Integer
        Dim x, y As Object
        Dim currentValue, prevValue As Object
        For i = 0 To ArlToSort.Count - 1
            If i >= 1 Then
                If isDeepSort Then 'Only for 2 level max
                    Dim Properties() As String = SortColumn.Split((".").ToCharArray())
                    Dim dummyType As Type = ObjType
                    Dim currentDummyObject As Object
                    Dim prevDummyObject As Object

                    For z As Integer = 0 To Properties.Length - 2
                        currentDummyObject = dummyType.GetProperty(Properties(z)).GetValue(ArlToSort(i), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                        prevDummyObject = dummyType.GetProperty(Properties(z)).GetValue(ArlToSort(i - 1), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)

                        dummyType = dummyType.GetProperty(Properties(z)).PropertyType
                    Next
                    Dim prop As PropertyInfo
                    prop = dummyType.GetProperty(Properties(Properties.Length - 1))

                    currentValue = dummyType.GetProperty(Properties(Properties.Length - 1)).GetValue(currentDummyObject, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                    prevValue = dummyType.GetProperty(Properties(Properties.Length - 1)).GetValue(prevDummyObject, BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                Else
                    currentValue = ObjType.GetProperty(SortColumn).GetValue(ArlToSort(i), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                    prevValue = ObjType.GetProperty(SortColumn).GetValue(ArlToSort(i - 1), BindingFlags.GetProperty, Type.DefaultBinder, Nothing, Nothing)
                End If

                If SortDirection = Sort.SortDirection.ASC Then
                    If currentValue < prevValue Then
                        x = ArlToSort(i)
                        y = ArlToSort(i - 1)
                        ArlToSort(i) = y
                        ArlToSort(i - 1) = x
                        i = 0
                    End If
                Else
                    If currentValue > prevValue Then
                        x = ArlToSort(i)
                        y = ArlToSort(i - 1)
                        ArlToSort(i) = y
                        ArlToSort(i - 1) = x
                        i = 0
                    End If
                End If
            End If
        Next

        Return ArlToSort

    End Function
    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim arrServiceStandard As ArrayList = CType(sessHelper.GetSession("MaintainGeneralRepair"), ArrayList)
        Dim _arrServiceTemplateGRLabor As ArrayList = New ArrayList

        For i As Integer = 0 To arrServiceStandard.Count - 1
            If arrServiceStandard.Item(i).GetType() = GetType(ServiceTemplateGRLabor) Then
                _arrServiceTemplateGRLabor.Add(arrServiceStandard.Item(i))
            End If
        Next

        If _arrServiceTemplateGRLabor.Count <> 0 Then
            ' SortListControl(arrStallMaster, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(_arrServiceTemplateGRLabor, pageIndex, dtgServiceStandardTime.PageSize)
            dtgServiceStandardTime.DataSource = PagedList
            dtgServiceStandardTime.VirtualItemCount = _arrServiceTemplateGRLabor.Count()
            dtgServiceStandardTime.DataBind()
        Else
            dtgServiceStandardTime.DataSource = New ArrayList
            dtgServiceStandardTime.VirtualItemCount = 0
            dtgServiceStandardTime.CurrentPageIndex = 0
            dtgServiceStandardTime.DataBind()
        End If
    End Sub

    Private Sub BindPageDetail(ByVal pageIndex As Integer)
        Dim arrServiceStandard As ArrayList = CType(sessHelper.GetSession("MaintainGeneralRepair"), ArrayList)
        Dim _arrServiceTemplateGRPartDetail As ArrayList = New ArrayList

        For i As Integer = 0 To arrServiceStandard.Count - 1
            If arrServiceStandard.Item(i).GetType() = GetType(ServiceTemplateGRPartDetail) Then
                _arrServiceTemplateGRPartDetail.Add(arrServiceStandard.Item(i))
            End If
        Next

        If _arrServiceTemplateGRPartDetail.Count <> 0 Then
            ' SortListControl(arrStallMaster, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(_arrServiceTemplateGRPartDetail, pageIndex, dtgServiceStandardTimeDetail.PageSize)
            dtgServiceStandardTimeDetail.DataSource = PagedList
            dtgServiceStandardTimeDetail.VirtualItemCount = _arrServiceTemplateGRPartDetail.Count()
            dtgServiceStandardTimeDetail.DataBind()
        Else
            dtgServiceStandardTimeDetail.DataSource = New ArrayList
            dtgServiceStandardTimeDetail.VirtualItemCount = 0
            dtgServiceStandardTimeDetail.CurrentPageIndex = 0
            dtgServiceStandardTimeDetail.DataBind()
        End If
    End Sub

#End Region



    Protected Sub dtgServiceStandardTime_SortCommand(source As Object, e As DataGridSortCommandEventArgs)
        'If e.SortExpression = ViewState.Item("PMSortColumn") Then
        '    If ViewState.Item("PMSortDirection") = Sort.SortDirection.ASC Then
        '        ViewState.Item("PMSortDirection") = Sort.SortDirection.DESC
        '    Else
        '        ViewState.Item("PMSortDirection") = Sort.SortDirection.ASC
        '    End If
        'End If
        'ViewState.Item("PMSortColumn") = e.SortExpression
        'dgPM.SelectedIndex = -1
        'dgPM.CurrentPageIndex = 0
        'RefreshGridPM(0)
    End Sub

    Protected Sub dtgServiceStandardTimeDetail_SortCommand(source As Object, e As DataGridSortCommandEventArgs)
        'If e.SortExpression = ViewState.Item("PMSortColumn") Then
        '    If ViewState.Item("PMSortDirection") = Sort.SortDirection.ASC Then
        '        ViewState.Item("PMSortDirection") = Sort.SortDirection.DESC
        '    Else
        '        ViewState.Item("PMSortDirection") = Sort.SortDirection.ASC
        '    End If
        'End If
        'ViewState.Item("PMSortColumn") = e.SortExpression
        'dgPM.SelectedIndex = -1
        'dgPM.CurrentPageIndex = 0
        'RefreshGridPM(0)
    End Sub
End Class