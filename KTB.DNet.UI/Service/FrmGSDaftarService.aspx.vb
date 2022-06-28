#Region " Customer Name Space "

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
Imports System.Linq
Imports System.Collections
Imports System.Collections.Generic
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports OfficeOpenXml
#End Region

Public Class FrmGSDaftarService
    Inherits System.Web.UI.Page



#Region "Private variable"

    Private _sessHelper As SessionHelper = New SessionHelper
    Private inputPriv As Boolean
    Private viewPriv As Boolean
#End Region


#Region "Cek Privilege"

    Private Sub InitiateAuthorization()
        If CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_Input_Privilage) Then
                inputPriv = False
            Else
                inputPriv = SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_Input_Privilage)
                viewPriv = True
            End If
            If Not SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_View_Privilage) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Service")
            Else
                viewPriv = SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_View_Privilage)
                inputPriv = SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_Input_Privilage)
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_View_Privilage) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Service")
            Else
                viewPriv = SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_View_Privilage)
                inputPriv = SecurityProvider.Authorize(Context.User, SR.GlobalServiceReminder_Input_Privilage)
            End If
        End If

    End Sub

    Dim _userInfo As UserInfo
    Dim _dealerSystem As DealerSystems

#End Region


#Region "CUSTOM SUB/FUNC"

    Private Sub initSvcReminderList()
        bindDgSvcReminder(0)
    End Sub

    Private Sub bindDgSvcReminder(ByVal indexPage As Integer)
        btnSearch.Enabled = False
        Dim totalRow As Integer = 0
        Dim searchCrit As CriteriaComposite = CType(_sessHelper.GetSession("SEARCH_CRIT"), CriteriaComposite)
        Dim loginUserInfo As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim isKTB As Boolean = IIf(loginUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)
        Dim criteria As CriteriaComposite
        Dim marginDay As DateTime = DateTime.Today.AddDays(44)

        If Not IsNothing(searchCrit) Then
            criteria = searchCrit
        Else
            criteria = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If Not isKTB Then
                criteria.opAnd(New Criteria(GetType(ServiceReminder), "Status", MatchType.Lesser, 3))
            End If
        End If

        If Not isKTB Then
            'criteria.opAnd(New Criteria(GetType(ServiceReminder), "MaxFUDealerDate", MatchType.GreaterOrEqual, DateTime.Today))
            'criteria.opAnd(New Criteria(GetType(ServiceReminder), "MaxFUDealerDate", MatchType.LesserOrEqual, marginDay))
            'criteria.opAnd(New Criteria(GetType(ServiceReminder), "Dealer", MatchType.Exact, loginUserInfo.Dealer.ID))
            Dim strSql As String = String.Format("select distinct a.ID from ServiceReminder a join ServiceReminderFollowUp b on b.ServiceReminderID = a.ID where " &
                                                 "a.MaxFUDealerDate >= '" & DateTime.Today.ToString("yyyy-MM-dd") & "' and " &
                                                 "a.MaxFUDealerDate <= '" & marginDay.ToString("yyyy-MM-dd") & "' and " &
                                                 "a.DealerID = " & loginUserInfo.Dealer.ID)
            criteria.opAnd(New Criteria(GetType(ServiceReminder), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If


        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(ServiceReminder), "PMKind.KindCode", Sort.SortDirection.ASC))
        sortColl.Add(New Sort(GetType(ServiceReminder), "ServiceReminderDate", Sort.SortDirection.ASC))

        If (Not IsNothing(searchCrit) AndAlso isKTB) OrElse Not isKTB Then
            Dim arrSvcReminder As ArrayList = New ServiceReminderFacade(User).Retrieve(criteria, sortColl)

            If (Not IsNothing(searchCrit) AndAlso isKTB) AndAlso arrSvcReminder.Count > 500 Then
                MessageBox.Show("Silahkan melakukan pencarian berdasarkan range tanggal")
                btnSearch.Enabled = True
                Exit Sub
            End If
            _sessHelper.SetSession("ARRSVCREMINDER", arrSvcReminder)

            dgSvcReminder.CurrentPageIndex = indexPage
            dgSvcReminder.DataSource = ArrayListPager.DoPage(arrSvcReminder, indexPage, dgSvcReminder.PageSize)
            dgSvcReminder.VirtualItemCount = arrSvcReminder.Count
            dgSvcReminder.DataBind()

            lblTotalGridData.Text = arrSvcReminder.Count
        ElseIf isKTB Then
            'Dim arrSvcReminder As ArrayList = New ServiceReminderFacade(User).RetrieveActiveList(criteria, indexPage + 1, dgSvcReminder.PageSize, _
            '                                                                                        totalRow, "LastUpdateTime", Sort.SortDirection.ASC)
            '_sessHelper.SetSession("ARRSVCREMINDER", arrSvcReminder)

            'dgSvcReminder.CurrentPageIndex = indexPage
            ''dgSvcReminder.DataSource = ArrayListPager.DoPage(arrSvcReminder, indexPage, dgSvcReminder.PageSize)
            'dgSvcReminder.VirtualItemCount = arrSvcReminder.Count
            'dgSvcReminder.DataBind()
        End If

        'Dim arrSvcReminder As ArrayList = New ServiceReminderFacade(User).Retrieve(criteria, sortColl)
        btnSearch.Enabled = True
    End Sub

    Private Sub bindPage(ByVal pageIndex As Integer)
        Dim arrSvcReminder As ArrayList = CType(_sessHelper.GetSession("ARRSVCREMINDER"), ArrayList)

        If Not IsNothing(arrSvcReminder) AndAlso arrSvcReminder.Count <> 0 Then
            Try
                '-- Sort first
                SortListControl(arrSvcReminder, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Catch ex As Exception
            End Try
            '-- Then paging
            Dim pagedList As ArrayList = ArrayListPager.DoPage(arrSvcReminder, pageIndex, dgSvcReminder.PageSize)
            _sessHelper.SetSession("ARRSVCREMINDER", arrSvcReminder)
            dgSvcReminder.DataSource = pagedList
            dgSvcReminder.VirtualItemCount = arrSvcReminder.Count
            dgSvcReminder.CurrentPageIndex = pageIndex
            dgSvcReminder.DataBind()
        Else
            '-- Display datagrid header only
            dgSvcReminder.DataSource = New ArrayList
            dgSvcReminder.VirtualItemCount = 0
            dgSvcReminder.CurrentPageIndex = 0
            dgSvcReminder.DataBind()
        End If

    End Sub

    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                                ByVal SortDirection As Integer)

        'Dim list As List(Of ServiceReminder)

        'For Each s As ServiceReminder In pCompletelist
        '    list.Add(s)
        'Next


        If SortColumn.Trim <> "" Then
            Dim isASC As Boolean = (SortDirection = Sort.SortDirection.DESC)  '-- Is sorted ascending?
            Dim objListComparer As IComparer = New ListComparer(isASC, SortColumn)
            pCompletelist.Sort(objListComparer)
        End If

    End Sub

    Private Function getDealerID(ByVal dealerCode As String) As String
        Dim arrDealer As ArrayList = New ArrayList
        Dim result As String = "("
        Dim codes As String = "("
        Dim tempCode As String() = dealerCode.Split(";")
        For Each tc As String In tempCode
            codes += "'" + tc + "',"
        Next
        codes = codes.Substring(0, codes.Length - 1)
        codes += ")"
        Try
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, codes))
            arrDealer = New DealerFacade(User).RetrieveByCriteria(criteria)
        Catch ex As Exception
            Return ""
        End Try

        If Not IsNothing(arrDealer) AndAlso arrDealer.Count > 0 Then
            Dim temp As String = ""
            For i As Integer = 0 To arrDealer.Count - 1
                If i <> (arrDealer.Count - 1) Then
                    temp = CType(arrDealer(i), Dealer).ID
                    temp += ","
                Else
                    temp = CType(arrDealer(i), Dealer).ID
                End If

                result += temp
            Next

            result += ")"
        Else
            result = ""
        End If

        Return result
    End Function

    Private Function getDealerBranchID(ByVal dealerBranchCode As String) As String
        Dim arrDealerBranch As ArrayList = New ArrayList
        Dim result As String = "("
        Dim codes As String = "("
        Dim tempCode As String() = dealerBranchCode.Split(";")
        For Each tc As String In tempCode
            codes += "'" + tc + "',"
        Next
        codes = codes.Substring(0, codes.Length - 1)
        codes += ")"
        Try
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(DealerBranch), "DealerBranchCode", MatchType.InSet, codes))
            arrDealerBranch = New DealerBranchFacade(User).Retrieve(criteria)
        Catch ex As Exception
            Return ""
        End Try

        If Not IsNothing(arrDealerBranch) AndAlso arrDealerBranch.Count > 0 Then
            Dim temp As String = ""
            For i As Integer = 0 To arrDealerBranch.Count - 1
                If i <> (arrDealerBranch.Count - 1) Then
                    temp = CType(arrDealerBranch(i), DealerBranch).ID
                    temp += ","
                Else
                    temp = CType(arrDealerBranch(i), DealerBranch).ID
                End If

                result += temp
            Next

            result += ")"
        Else
            result = ""
        End If

        Return result
    End Function

    Private Sub binDdlStatus()
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "GlobalServiceReminder.Status"))
        Dim arrStatus As ArrayList = New StandardCodeFacade(User).Retrieve(criteria)

        _userInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim isKTB As Boolean = IIf(_userInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)

        Dim statusListItemFirst As New ListItem("silahkan Pilih", -1)
        ddlStatus.Items.Add(statusListItemFirst)

        For Each s As StandardCode In arrStatus
            Dim statusListItem As New ListItem(s.ValueDesc, s.ValueId)
            ddlStatus.Items.Add(statusListItem)

            If Not isKTB AndAlso s.ValueId = 3 Then
                Exit For
            End If
        Next
    End Sub

    Private Sub bindlboxStatus()
        Dim arrStatus As ArrayList = New ArrayList
        Dim tempStdCode As StandardCode = New StandardCode
        tempStdCode.ValueId = -1
        tempStdCode.ValueDesc = "Semua"
        arrStatus.Add(tempStdCode)
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "GlobalServiceReminder.Status"))
        Dim tempArrStatus = New StandardCodeFacade(User).Retrieve(criteria)

        _userInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim isKTB As Boolean = IIf(_userInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)

        For Each s As StandardCode In tempArrStatus
            arrStatus.Add(s)

            If Not isKTB AndAlso s.ValueId = 3 Then
                Exit For
            End If
        Next

        lboxStatus.DataSource = arrStatus
        lboxStatus.DataTextField = "ValueDesc"
        lboxStatus.DataValueField = "ValueID"
        lboxStatus.DataBind()
    End Sub

    Private Sub bindDdlCategory()
        Dim arrCategory As ArrayList = _sessHelper.GetSession("ARRCATEGORY")

        If IsNothing(arrCategory) OrElse arrCategory.Count = 0 Then
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "GlobalServiceReminder.Category"))
            criteria.opAnd(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            arrCategory = New StandardCodeFacade(User).Retrieve(criteria)
            _sessHelper.SetSession("ARRCATEGORY", arrCategory)
        End If

        ddlCategory.Items.Add(New ListItem("Silahkan pilih", -1))

        For Each c As StandardCode In arrCategory
            ddlCategory.Items.Add(New ListItem(c.ValueDesc, c.ValueId))
        Next
    End Sub

    Private Sub BindDdlJenisService()
        ddlJnsService.Items.Clear()

        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "GSR.WorkOrderCategory"))

        Dim results As ArrayList = New StandardCodeFacade(User).Retrieve(crit)

        With ddlJnsService.Items
            For Each obj As StandardCode In results
                .Add(New ListItem(obj.ValueDesc, obj.ValueCode))
            Next
        End With

        ddlJnsService.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Function getSearchCritByStatus() As String
        Dim result As String = ""
        For Each item As Integer In lboxStatus.GetSelectedIndices()
            Dim list As ListItem = lboxStatus.Items(item)
            If list.Value = -1 Then
                Return ""
            End If

            If list.Value = 3 AndAlso Not IsKTB() Then
                result += "3, 4, 5, 6,"
            Else
                result += Convert.ToString(list.Value) + ","
            End If
        Next

        Return result.Substring(0, result.Length - 1)

    End Function

    Private Sub getSearchCritBySvcCategory(ByVal val As Integer, ByRef criteria As CriteriaComposite)
        If val = 1 Then
            Dim startDate As DateTime = DateTime.Now.AddDays(38)

            criteria.opAnd(New Criteria(GetType(ServiceReminder), "MaxFUDealerDate", MatchType.GreaterOrEqual, startDate))
            'criteria.opAnd(New Criteria(GetType(ServiceReminder), "Status", MatchType.No, 3))
        ElseIf val = 2 Then
            Dim startDate As DateTime = DateTime.Now.AddDays(31)
            Dim endDate As DateTime = DateTime.Now.AddDays(37)

            criteria.opAnd(New Criteria(GetType(ServiceReminder), "MaxFUDealerDate", MatchType.GreaterOrEqual, startDate))
            criteria.opAnd(New Criteria(GetType(ServiceReminder), "MaxFUDealerDate", MatchType.LesserOrEqual, endDate))
            'criteria.opAnd(New Criteria(GetType(ServiceReminder), "Status", MatchType.No, 3))

        ElseIf val = 3 Then
            Dim startDate As DateTime = DateTime.Now
            Dim endDate As DateTime = DateTime.Now.AddDays(30)

            criteria.opAnd(New Criteria(GetType(ServiceReminder), "MaxFUDealerDate", MatchType.GreaterOrEqual, startDate))
            criteria.opAnd(New Criteria(GetType(ServiceReminder), "MaxFUDealerDate", MatchType.LesserOrEqual, endDate))
            'criteria.opAnd(New Criteria(GetType(ServiceReminder), "Status", MatchType.No, 3))
        End If
    End Sub

    Private Function getTransactionType(ByVal val As Byte) As String
        Dim arrTransactionType As ArrayList = _sessHelper.GetSession("ENUMTRANSACTION")

        If IsNothing(arrTransactionType) Then
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "GlobalServiceReminder.TransactionType"))
            arrTransactionType = CType(New StandardCodeFacade(User).Retrieve(criteria), ArrayList)
            _sessHelper.SetSession("ENUMTRANSACTION", arrTransactionType)
        End If

        For Each t As StandardCode In arrTransactionType
            If t.ValueId = val Then
                Return t.ValueDesc
            End If
        Next

        Return ""
    End Function

    Private Function getStatus(ByVal val As Integer)
        Dim arrStatus As ArrayList = _sessHelper.GetSession("ENUMSTATUS")

        _userInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim isKTB As Boolean = IIf(_userInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)

        If IsNothing(arrStatus) Then
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "GlobalServiceReminder.Status"))
            arrStatus = CType(New StandardCodeFacade(User).Retrieve(criteria), ArrayList)
            _sessHelper.SetSession("ENUMSTATUS", arrStatus)
        End If

        If Not isKTB AndAlso val > 2 Then
            Return "Complete"
        End If

        For Each t As StandardCode In arrStatus
            If t.ValueId = val Then
                Return t.ValueDesc
            End If
        Next

        Return ""
    End Function

    Private Sub setDefaultField()
        icStartReminder.Value = DateTime.Now.AddDays(-30)
        If Not IsKTB() Then
            dgSvcReminder.Attributes.Add("Style", "Width:100%")
            dgSvcReminder.Columns(13).Visible = False
            dgSvcReminder.Columns(14).Visible = False
            dgSvcReminder.Columns(15).Visible = False
        End If
    End Sub

    Private Sub SetDownload(ByVal crit As CriteriaComposite)
        Dim arrData As New DataSet
        ' mengambil data yang dibutuhkan
        arrData = New ServiceReminderFacade(User).RetrieveServiceReminderDownloadSP(crit)

        If arrData.Tables.Count > 0 And arrData.Tables(0).Rows.Count > 0 Then
            'DoDownload(arrData)
            Dim strFileNm As String = "Service reminder "
            CreateExcel1(strFileNm, arrData.Tables(0))
        Else
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim oDFac As New DealerFacade(User)
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Dim sgFac As New ServiceReminderFacade(User)
        Dim gradeDictionary As Dictionary(Of String, String) = GetDictionaryGrade()
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "No. Rangka"
            ws.Cells("C3").Value = "Tgl Batas Reminderr"
            ws.Cells("D3").Value = "Nama Konsumen"
            ws.Cells("E3").Value = "Telepon"
            ws.Cells("F3").Value = "Tipe Kendaraan"
            ws.Cells("G3").Value = "Kategori"
            ws.Cells("H3").Value = "Dealer Code"
            ws.Cells("I3").Value = "Dealer Name"   'added by Benny 20190401
            ws.Cells("J3").Value = "Dealer Branch"
            ws.Cells("K3").Value = "Status"
            ws.Cells("L3").Value = "Last Follow Up"
            ws.Cells("M3").Value = "Actual Dealer Service"
            ws.Cells("N3").Value = "Actual Service Date"
            ws.Cells("O3").Value = "Actual KM"

            For i As Integer = 0 To Data.Count - 1
                Dim item As ServiceReminder = Data(i)
                ws.Cells(i + 4, 1).Value = i + 1
                ws.Cells(i + 4, 2).Value = item.ChassisNumber
                If item.ServiceReminderDate.Year = 1753 Then
                    ws.Cells(i + 4, 3).Value = ""
                Else
                    ws.Cells(i + 4, 3).Value = Format(item.ServiceReminderDate, "yyyy/MM/dd")
                End If
                ws.Cells(i + 4, 4).Value = item.CustomerName
                ws.Cells(i + 4, 5).Value = item.CustomerPhoneNumber
                ws.Cells(i + 4, 6).Value = item.VehicleType
                ws.Cells(i + 4, 7).Value = item.Category
                If Not IsNothing(item.Dealer) Then
                    ws.Cells(i + 4, 8).Value = item.Dealer.DealerCode
                    ws.Cells(i + 4, 9).Value = item.Dealer.DealerName
                End If
                If Not IsNothing(item.DealerBranch) Then
                    ws.Cells(i + 4, 10).Value = item.DealerBranch.Dealer.DealerName
                End If
                ws.Cells(i + 4, 11).Value = getStatus(item.Status)
                If item.ServiceReminderDate.Year = 1753 Then
                    ws.Cells(i + 4, 12).Value = ""
                Else
                    ws.Cells(i + 4, 12).Value = Format(item.LastUpdateTime, "yyyy/MM/dd")
                End If
                If Not IsNothing(item.ActualServiceDealer) Then
                    ws.Cells(i + 4, 13).Value = item.ActualServiceDealer.DealerName
                End If
                If item.ServiceActualDate.Year = 1753 Then
                    ws.Cells(i + 4, 14).Value = ""
                Else
                    ws.Cells(i + 4, 14).Value = Format(item.ServiceActualDate, "yyyy/MM/dd")
                End If
                ws.Cells(i + 4, 15).Value = item.ActualKM
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using
    End Sub

    Private Sub CreateExcel1(ByVal FileName As String, ByVal Data As DataTable)
        Dim oD As Dealer
        Dim oDFac As New DealerFacade(User)
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Dim sgFac As New ServiceReminderFacade(User)
        Dim gradeDictionary As Dictionary(Of String, String) = GetDictionaryGrade()
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)
            Dim colLength As Integer = Data.Columns.Count

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            Dim colCharCounter As Integer = 66  '65 is decimal for A (ASCII Code)
            For Each col As DataColumn In Data.Columns
                Dim colChar As String = Convert.ToChar(colCharCounter) & "3"
                ws.Cells(colChar).Value = col.ColumnName
                colCharCounter = colCharCounter + 1
            Next

            For i As Integer = 0 To Data.Rows.Count - 1
                ws.Cells(i + 4, 1).Value = i + 1
                For j As Integer = 0 To colLength - 1
                    ws.Cells(i + 4, j + 2).Value = CStr(Data.Rows(i)(j))
                Next
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using
    End Sub

    Private Function GetDictionaryGrade() As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "Category", MatchType.Exact, "SalesmanGrade"))

        Dim arlGrade As ArrayList = New StandardCodeFacade(User).Retrieve(criterias)

        If arlGrade.Count > 0 Then

            For Each grade As StandardCode In arlGrade
                result.Add(grade.ValueId.ToString(), grade.ValueDesc)
            Next

        End If

        Return result
    End Function

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                File.WriteAllBytes(Server.MapPath("~/DataTemp/" & fileName), fileBytes)
                imp.StopImpersonate()
            End If

        Catch ex As Exception
            Exit Sub

        End Try

        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)
    End Sub


#End Region


#Region "EVENT HANDLER"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _userInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim isKTB As Boolean = IIf(_userInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)
        InitiateAuthorization()

        If Not IsPostBack Then
            ViewState("currSortColumn") = ""
            ViewState("currSortDirection") = Sort.SortDirection.DESC

            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(DealerSystems), "Dealer", MatchType.Exact, _userInfo.Dealer.ID))
            _dealerSystem = CType(New DealerSystemsFacade(User).Retrieve(criteria)(0), DealerSystems)

            _sessHelper.SetSession("DEALERSYSTEM", _dealerSystem)

            binDdlStatus()
            bindDdlCategory()
            BindDdlJenisService()
            bindlboxStatus()
            setDefaultField()
            initSvcReminderList()
        End If

        If Not isKTB Then
            lblDealerCode.Text = _userInfo.Dealer.DealerCode & " / " & _userInfo.Dealer.SearchTerm1
            txtDealerCode.Visible = False
            txtDealerCode.Enabled = False
            lblPopUpDealer.Enabled = False
            lblPopUpDealer.Visible = False
        Else
            lblDealerCode.Visible = False
        End If
    End Sub

    Protected Sub dgSvcReminder_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgSvcReminder.PageIndexChanged
        bindPage(e.NewPageIndex)
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim searchFlag As Boolean = False

        _userInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim isKTB As Boolean = IIf(_userInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)

        If txtDealerCode.Text <> "" AndAlso isKTB Then
            Dim arrDealerID As String = getDealerID(txtDealerCode.Text)

            If arrDealerID <> "" Then
                criteria.opAnd(New Criteria(GetType(ServiceReminder), "Dealer", MatchType.InSet, arrDealerID))
            Else
                MessageBox.Show("Kode dealer tidak valid")
                Exit Sub
            End If
        End If

        If txtDealerBranchCode.Text <> "" Then
            Dim arrDealerBranchID As String = getDealerBranchID(txtDealerBranchCode.Text)

            If arrDealerBranchID <> "" Then
                criteria.opAnd(New Criteria(GetType(ServiceReminder), "DealerBranch", MatchType.InSet, arrDealerBranchID))
            Else
                MessageBox.Show("Kode dealer tidak valid")
                Exit Sub
            End If
        End If

        If txtEngineNo.Text <> "" Then
            criteria.opAnd(New Criteria(GetType(ServiceReminder), "EngineNumber", MatchType.Partial, txtEngineNo.Text.Trim))
            searchFlag = True
        End If

        If txtChassisNo.Text <> "" Then
            criteria.opAnd(New Criteria(GetType(ServiceReminder), "ChassisNumber", MatchType.Partial, txtChassisNo.Text.Trim))
        End If

        If txtConsumenName.Text <> "" Then
            criteria.opAnd(New Criteria(GetType(ServiceReminder), "CustomerName", MatchType.Partial, txtConsumenName.Text))
            searchFlag = True
        End If

        If ddlStatus.SelectedValue <> -1 Then
            If Not isKTB Then
                If ddlStatus.SelectedValue = 3 Then
                    criteria.opAnd(New Criteria(GetType(ServiceReminder), "Status", MatchType.InSet, "(3,4,5,6)"))
                Else
                    criteria.opAnd(New Criteria(GetType(ServiceReminder), "Status", MatchType.Exact, ddlStatus.SelectedValue))
                End If
            Else
                criteria.opAnd(New Criteria(GetType(ServiceReminder), "Status", MatchType.Exact, ddlStatus.SelectedValue))
            End If
        End If

        If ddlJnsService.SelectedIndex <> 0 Then
            Dim kindCode As String = ddlJnsService.SelectedValue
            If String.IsNullorEmpty(kindCode) Then
                Dim stdCode As List(Of StandardCode) = New StandardCodeFacade(User).RetrieveByCategory("GSR.WorkOrderCategory").Cast(Of StandardCode).ToList
                criteria.opAnd(New Criteria(GetType(ServiceReminder), "PMKind.KindCode", MatchType.NotInSet, String.Join(",", _
                    stdCode.Where(Function(x) "1,2".Contains(x.ValueId.ToString)).Select(Function(s) New With {.ValueCode = String.Format("'{0}'", s.ValueCode)}).Select(Function(s) s.ValueCode))))
            Else
                criteria.opAnd(New Criteria(GetType(ServiceReminder), "PMKind.KindCode", MatchType.Exact, kindCode))
            End If
        End If

        If lboxStatus.GetSelectedIndices().Count > 0 Then
            Dim crit As String = getSearchCritByStatus()
            If crit <> "" Then
                criteria.opAnd(New Criteria(GetType(ServiceReminder), "Status", MatchType.InSet, "(" + crit + ")"))
            End If
        End If

        If chReminderDate.Checked Then
            If (icEndReminder.Value - icStartReminder.Value).TotalDays <= 30 Then
                Dim startDate As DateTime = New DateTime(icStartReminder.Value.Year, icStartReminder.Value.Month, icStartReminder.Value.Day, 0, 0, 0)
                Dim endDate As DateTime = New DateTime(icEndReminder.Value.Year, icEndReminder.Value.Month, icEndReminder.Value.Day, 23, 59, 59)
                criteria.opAnd(New Criteria(GetType(ServiceReminder), "ServiceReminderDate", MatchType.GreaterOrEqual, startDate))
                criteria.opAnd(New Criteria(GetType(ServiceReminder), "ServiceReminderDate", MatchType.LesserOrEqual, endDate))
            Else
                MessageBox.Show("Batas tanggal pencarian tidak boleh lebih dari 30 hari")
                Exit Sub
            End If
        End If

        If ddlCategory.SelectedValue <> -1 Then
            getSearchCritBySvcCategory(ddlCategory.SelectedValue, criteria)
        End If

        _sessHelper.SetSession("SEARCH_CRIT", criteria)
        btnSearch.Enabled = False
        bindDgSvcReminder(0)
    End Sub

    Protected Sub dgSvcReminder_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgSvcReminder.ItemDataBound
        Dim lblNo As Label
        Dim lblChassisNo As Label
        Dim lblReminderDate As Label
        Dim lblConsumenName As Label
        Dim lblPhone As Label
        Dim lblVehicleType As Label
        'Dim lblCategory As Label
        Dim lblKindService As Label
        Dim lblDealerCode As Label
        Dim lblDealerName As Label
        Dim lblDealerBranch As Label
        Dim lblStatus As Label
        Dim lblLastFollowUp As Label
        Dim lbtnEdit As LinkButton
        Dim lblActDealerService As Label
        Dim lblActServiceDate As Label
        Dim lblActualKMGrd As Label

        Dim index = e.Item.ItemIndex
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        'Dim dealerSystem As DealerSystems = _sessHelper.GetSession("DEALERSYSETM")
        Dim arrSvcReminder As ArrayList = CType(_sessHelper.GetSession("ARRSVCREMINDER"), ArrayList)
        Dim svcReminder As ServiceReminder = New ServiceReminder

        If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem Then
            lblNo = CType(e.Item.FindControl("lblNo"), Label)
            lblChassisNo = CType(e.Item.FindControl("lblChassisNo"), Label)
            lblReminderDate = CType(e.Item.FindControl("lblReminderDate"), Label)
            lblConsumenName = CType(e.Item.FindControl("lblConsumenName"), Label)
            lblPhone = CType(e.Item.FindControl("lblPhone"), Label)
            lblVehicleType = CType(e.Item.FindControl("lblVehicleType"), Label)
            lblDealerCode = CType(e.Item.FindControl("lblDealerCode"), Label)
            lblDealerName = CType(e.Item.FindControl("lblDealerName"), Label)
            lblDealerBranch = CType(e.Item.FindControl("lblDealerBranch"), Label)
            'lblCategory = CType(e.Item.FindControl("lblCategory"), Label)
            lblKindService = CType(e.Item.FindControl("lblKindService"), Label)
            lblStatus = CType(e.Item.FindControl("lblStatus"), Label)
            lblLastFollowUp = CType(e.Item.FindControl("lblLastFollowUp"), Label)
            lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)

            Dim tempIndex As Integer = e.Item.ItemIndex + (dgSvcReminder.CurrentPageIndex * dgSvcReminder.PageSize)

            svcReminder = arrSvcReminder(tempIndex)

            lblNo.Text = tempIndex + 1
            lblChassisNo.Text = svcReminder.ChassisNumber
            lblReminderDate.Text = svcReminder.ServiceReminderDate
            lblConsumenName.Text = svcReminder.CustomerName
            lblPhone.Text = svcReminder.CustomerPhoneNumber
            lblVehicleType.Text = svcReminder.VehicleType
            'lblCategory.Text = getTransactionType(svcReminder.TransactionType)
            lblKindService.Text = svcReminder.PMKind.KindDescription
            lblDealerCode.Text = svcReminder.Dealer.DealerCode
            lblDealerName.Text = svcReminder.Dealer.DealerName
            lblStatus.Text = getStatus(svcReminder.Status)
            lblLastFollowUp.Text = svcReminder.LastUpdateTime.Date

            If Not IsNothing(svcReminder.DealerBranch) Then
                lblDealerBranch.Text = svcReminder.DealerBranch.Name
            End If

            Dim marginDays As Double = (DateTime.Today - svcReminder.MaxFUDealerDate).TotalDays
            Dim boolStatus As Boolean = True
            If getStatus(svcReminder.Status) <> "Complete" Then
                If marginDays <= -31 AndAlso marginDays >= -37 Then
                    e.Item.BackColor = Color.Yellow
                ElseIf marginDays <= -0 AndAlso marginDays >= -30 Then
                    e.Item.BackColor = Color.LightSalmon
                End If
            Else
                boolStatus = False

            End If

            If Not IsNothing(_dealerSystem) Then
                If _dealerSystem.SystemID > 1 Then
                    lbtnEdit.Visible = False
                End If
            End If


            If IsKTB() Then
                lbtnEdit.Visible = False
                lblActDealerService = CType(e.Item.FindControl("lblActDealerService"), Label)
                lblActServiceDate = CType(e.Item.FindControl("lblActServiceDate"), Label)
                lblActualKMGrd = CType(e.Item.FindControl("lblActualKMGrd"), Label)

                lblActualKMGrd.Text = svcReminder.ActualKM
                If Not IsNothing(svcReminder.ActualServiceDealer) Then
                    lblActDealerService.Text = svcReminder.ActualServiceDealer.DealerName
                End If
                If Not IsNothing(svcReminder.ActualServiceDealerBranch) Then
                    lblActDealerService.Text = svcReminder.ActualServiceDealerBranch.Name
                End If

                If svcReminder.ServiceActualDate.Year <> 1753 Then
                    lblActServiceDate.Text = svcReminder.ServiceActualDate
                End If
            Else
                If boolStatus = True Then
                    lbtnEdit.Visible = inputPriv
                Else
                    lbtnEdit.Visible = False
                End If
            End If
        End If
    End Sub

    Protected Sub dgSvcReminder_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgSvcReminder.ItemCommand
        If e.CommandName = "Edit" Then
            _sessHelper.SetSession("backURL", "/FrmGSDaftarService.aspx")
            _sessHelper.SetSession("SVCREMINDERID", e.Item.Cells(0).Text)
            _sessHelper.SetSession("SVCMODE", "EDIT")
            If Not IsNothing(_sessHelper.GetSession("SVCREMINDERID")) Then
                Response.Redirect("FrmGSServiceFollowUp.aspx")
            End If
        ElseIf e.CommandName = "View" Then
            _sessHelper.SetSession("backURL", "/FrmGSDaftarService.aspx")
            _sessHelper.SetSession("SVCREMINDERID", e.Item.Cells(0).Text)
            _sessHelper.SetSession("SVCMODE", "VIEW")
            If Not IsNothing(_sessHelper.GetSession("SVCREMINDERID")) Then
                Response.Redirect("FrmGSServiceFollowUp.aspx")
            End If
        End If
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        _sessHelper.RemoveSession("SEARCH_CRIT")
        txtDealerCode.Text = ""
        txtEngineNo.Text = ""
        txtChassisNo.Text = ""
        txtConsumenName.Text = ""
        ddlStatus.SelectedValue = -1
        icStartReminder.Value = DateTime.Today.AddDays(-30)
        icEndReminder.Value = DateTime.Today
        lboxStatus.ClearSelection()
        bindDgSvcReminder(0)
    End Sub

    Protected Sub dgSvcReminder_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgSvcReminder.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        BindPage(0)
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        btnSearch_Click(sender, e)
        Dim searchCrit As CriteriaComposite = CType(_sessHelper.GetSession("SEARCH_CRIT"), CriteriaComposite)
        Dim loginUserInfo As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim isKTB As Boolean = IIf(loginUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB, True, False)
        Dim criteria As CriteriaComposite
        Dim marginDay As DateTime = DateTime.Today.AddDays(44)

        If Not IsNothing(searchCrit) Then
            criteria = searchCrit
        Else
            criteria = New CriteriaComposite(New Criteria(GetType(ServiceReminder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If Not isKTB Then
                criteria.opAnd(New Criteria(GetType(ServiceReminder), "Status", MatchType.Lesser, 3))
            End If
        End If

        If Not isKTB Then
            'criteria.opAnd(New Criteria(GetType(ServiceReminder), "MaxFUDealerDate", MatchType.GreaterOrEqual, DateTime.Today))
            'criteria.opAnd(New Criteria(GetType(ServiceReminder), "MaxFUDealerDate", MatchType.LesserOrEqual, marginDay))
            'criteria.opAnd(New Criteria(GetType(ServiceReminder), "Dealer", MatchType.Exact, loginUserInfo.Dealer.ID))
            Dim strSql As String = String.Format("select distinct a.ID from ServiceReminder a join ServiceReminderFollowUp b on b.ServiceReminderID = a.ID where " &
                                                 "a.MaxFUDealerDate >= '" & DateTime.Today.ToString("yyyy-MM-dd") & "' and " &
                                                 "a.MaxFUDealerDate <= '" & marginDay.ToString("yyyy-MM-dd") & "' and " &
                                                 "a.DealerID = " & loginUserInfo.Dealer.ID)
            criteria.opAnd(New Criteria(GetType(ServiceReminder), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        SetDownload(criteria)
    End Sub

#End Region

End Class