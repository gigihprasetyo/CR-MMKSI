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
Imports KTB.DNet.WebCC
Imports MMKSI.DNetUpload.Utility
Imports System.Collections.Generic
Imports OfficeOpenXml
Imports GlobalExtensions
Imports KTB.DNet.BusinessValidation
Imports System.Linq


#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Reflection

#End Region

Public Class PopUpUploadAllocationRealTimeService
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

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
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
                    Dim parser As UploadAllocationRealtimeService = New UploadAllocationRealtimeService(objDealer, DataFile.PostedFile.ContentType.ToString)

                    '-- Parse data file and store result into arraylist
                    'Dim arlCustomer As ArrayList = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1$]", "User"), ArrayList)
                    Dim arlCustomer As ArrayList = CType(parser.ParseExcelNoTransaction(TempFile, "[Sheet1$A1:S152]", "User"), ArrayList)
                    If arlCustomer.Count > 0 Then
                        arlCustomer = SortArraylist(arlCustomer, GetType(AllocationRealTimeService), "ErrorMessage", Sort.SortDirection.DESC)

                        'arlCustomer.Sort()
                        'arlCustomer = arlCustomer.OrderByDescending(Function(x) x.isPrimary).ThenBy(Function(x) x.id).ToList()
                        'Dim tt As List(Of extra) = allExtras.ToList()
                        If parser.IsAllDataValid Then
                            btnSimpan.Enabled = True
                        Else
                            btnSimpan.Enabled = False
                        End If
                    Else
                        If Not IsNothing(parser.GetErrorMessage()) Then
                            MessageBox.Show(parser.GetErrorMessage())
                        Else
                            MessageBox.Show("Tidak ada data yang diupload dari excel.")
                        End If
                        'MessageBox.Show(parser.GetErrorMessage())

                        btnSimpan.Enabled = False
                    End If

                    sessHelper.SetSession("AllocationRealTimeService", arlCustomer)
                    'objSrvST = CType(sessHelper.GetSession("ServiceStandardTime"), ServiceStandardTime) 'sessHelper.SetSession("ServiceStandardTime", arlCustomer)
                    dtgServiceStandardTime.Visible = True
                    dtgServiceStandardTime.DataSource = Nothing  '-- Reset datagrid first
                    dtgServiceStandardTime.CurrentPageIndex = 0
                    BindUploadServiceStandardTime()
                End If

            Catch Exc As Exception
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
        Session.Remove("AllocationRealTimeService")
        btnSimpan.Enabled = False
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs)
        'Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        _arlServStdTime = sessHelper.GetSession("AllocationRealTimeService")
        Dim _arlSST As ArrayList = sessHelper.GetSession("AllocationRealTimeService")
        Dim objSST As AllocationRealTimeService = New AllocationRealTimeService
        Dim _nResult As Integer = 0
        Dim test As Integer = 0
        Dim strAssist As String = ""
        'objSST = CType(_arlSST(0), ServiceStandardTime)
        If Not IsNothing(_arlSST) Then
            If _arlSST.Count > 0 Then
                Dim _errMsg As StringBuilder = New StringBuilder
                Dim oFacade As New AllocationRealTimeServiceFacade(User)
                Dim iSuccess As Integer = 0
                For Each _sst As AllocationRealTimeService In _arlSST
                    test = test + 1
                    If _sst.ErrorMessage = String.Empty Then

                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AllocationRealTimeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AllocationRealTimeService), "Dealer.DealerCode", MatchType.Exact, _sst.Dealer.DealerCode))

                        Dim arlSST As ArrayList = oFacade.Retrieve(criterias)
                        If arlSST.Count > 0 Then
                            objSST = CType(arlSST(0), AllocationRealTimeService)
                            _sst.ID = objSST.ID
                            _nResult = oFacade.Update(_sst)

                            'Return nResult
                        Else
                            '_sst.Dealer = objDealer
                            '_sst.VechileModel = _sst.VechileType.VechileModel
                            '_sst.AssistServiceTypeCode = _sst.AssistServiceTypeCode + test.ToString()
                            _nResult = oFacade.Insert(_sst)
                            'If _nResult = -1 Then
                            '    MessageBox.Show(SR.SaveFail)
                            'Else
                            '    MessageBox.Show(SR.SaveSuccess)
                            'End If
                        End If
                    Else
                        MessageBox.Show(test.ToString())
                    End If
                Next

                If _nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                    RegisterStartupScript("Close", "<script>onSuccess();</script>")
                    'ViewState.Add("vsProcess", "Add")
                End If

            End If


            Page_Load(Nothing, EventArgs.Empty)
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
    Private Sub dtgServiceStandardTime_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServiceStandardTime.ItemDataBound
        Dim objARS As VWI_AllocationRealTimeService
        If Not e.Item.DataItem Is Nothing Then
            Try
                'Dim objContact As CcContact = e.Item.DataItem

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lbCurrentStall As Label = CType(e.Item.FindControl("lbCurrentStall"), Label)
                Dim lbKodeDealer As Label = CType(e.Item.FindControl("lbKodeDealer"), Label)

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VWI_AllocationRealTimeService), "DealerCode", MatchType.Exact, lbKodeDealer.Text))

                Dim AllocationRealTimeService As List(Of VWI_AllocationRealTimeService) = New KTB.DNet.BusinessFacade.Service.VWI_AllocationRealTimeServiceFacade(User).RetrieveByCriteria(criterias).Cast(Of VWI_AllocationRealTimeService).ToList
                Dim arrVM = New ArrayList
                For Each objs As VWI_AllocationRealTimeService In AllocationRealTimeService

                    arrVM.add(objs)
                Next

                If arrVM.Count > 0 Then
                    If Not IsNothing(arrVM) AndAlso arrVM.Count > 0 Then
                        objARS = CType(arrVM(0), VWI_AllocationRealTimeService)
                        lbCurrentStall.Text = objARS.CurrentStall
                    End If
                Else
                    lbCurrentStall.Text = 0
                End If
                lblNo.Text = (dtgServiceStandardTime.CurrentPageIndex * dtgServiceStandardTime.PageSize + e.Item.ItemIndex + 1).ToString
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub
#End Region
    
#Region "Custom Method"
    Private Sub BindUploadServiceStandardTime()
        Dim totalRow As Integer = 0
        _arlServStdTime = New ArrayList
        Dim _arlValid As ArrayList = New ArrayList
        Dim _arlInValid As ArrayList = New ArrayList

        Try
            _arlServStdTime = sessHelper.GetSession("AllocationRealTimeService")

            If Not IsNothing(_arlServStdTime) Then

                'objSrvST = CType(sessHelper.GetSession("ServiceStandardTime"), ServiceStandardTime) 'sessHelper.SetSession("ServiceStandardTime", arlCustomer)


                sessHelper.SetSession("AllocationRealTimeService", _arlServStdTime)

                For Each _c As AllocationRealTimeService In _arlServStdTime
                    If _c.ErrorMessage <> String.Empty Then
                        btnSimpan.Enabled = False
                        Exit For
                    Else
                        btnSimpan.Enabled = True
                    End If
                Next


                totalRow = _arlServStdTime.Count
                dtgServiceStandardTime.DataSource = _arlServStdTime
                dtgServiceStandardTime.VirtualItemCount = totalRow
                dtgServiceStandardTime.DataBind()
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
        Dim arrServiceStandard As ArrayList = CType(sessHelper.GetSession("AllocationRealTimeService"), ArrayList)
        Dim aStatus As New ArrayList
        If arrServiceStandard.Count <> 0 Then
            ' SortListControl(arrStallMaster, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrServiceStandard, pageIndex, dtgServiceStandardTime.PageSize)
            dtgServiceStandardTime.DataSource = PagedList
            dtgServiceStandardTime.VirtualItemCount = arrServiceStandard.Count()
            dtgServiceStandardTime.DataBind()
        Else
            dtgServiceStandardTime.DataSource = New ArrayList
            dtgServiceStandardTime.VirtualItemCount = 0
            dtgServiceStandardTime.CurrentPageIndex = 0
            dtgServiceStandardTime.DataBind()
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
End Class