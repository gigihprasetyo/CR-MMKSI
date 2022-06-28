Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.Linq
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade.PK
Imports OfficeOpenXml


Public Class FrmDaftarDiscountProposal    
    Inherits System.Web.UI.Page

    Protected WithEvents dgDetail As System.Web.UI.WebControls.DataGrid

#Region "Variable Declaration"
    Private _sesCriteria As String = "FrmDaftarDiscountProposal.DaftarDPCriterias"
    Private sessHelper As New SessionHelper
    Private _objDealer As Dealer
    Private Property objDealer() As Dealer
        Get
            Return _objDealer
        End Get
        Set(ByVal value As Dealer)
            _objDealer = value
        End Set
    End Property
    Private Property SesDealer() As Dealer
        Get
            Return CType(sessHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sessHelper.SetSession("DEALER", Value)
        End Set
    End Property
    Private Shared _arrayListStatusDP As List(Of ListItem)
    Private Shared ReadOnly Property ArrayStatusDP() As List(Of ListItem)
        Get
            If (_arrayListStatusDP Is Nothing) Then
                _arrayListStatusDP = New List(Of ListItem)
                Dim obj As New StandardCode
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumDiscountProposal.Status"))
                Dim sortColl As SortCollection = New SortCollection
                sortColl.Add(New Sort(GetType(StandardCode), "Sequence", Sort.SortDirection.ASC))
                Dim arlStatus As ArrayList = obj.DoLoadArray(GetType(StandardCode).ToString, crit, sortColl)

                If Not IsNothing(arlStatus) AndAlso arlStatus.Count > 0 Then
                    For Each objSC As StandardCode In arlStatus
                        _arrayListStatusDP.Add(New ListItem(objSC.ValueDesc, objSC.ValueId))
                    Next
                End If
            End If
            Return _arrayListStatusDP
        End Get
    End Property
    Private Shared _arrayListFleetCategory As List(Of ListItem)
    Private Shared ReadOnly Property ArrayFleetCustomer() As List(Of ListItem)
        Get
            If (_arrayListFleetCategory Is Nothing) Then
                _arrayListFleetCategory = New List(Of ListItem)
                Dim obj As New StandardCode
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumDiscountProposal.FleetCategory"))
                Dim arlStatus As ArrayList = obj.DoLoadArray(GetType(StandardCode).ToString, crit)

                If Not IsNothing(arlStatus) AndAlso arlStatus.Count > 0 Then
                    For Each objSC As StandardCode In arlStatus
                        _arrayListFleetCategory.Add(New ListItem(objSC.ValueDesc, objSC.ValueId))
                    Next
                End If
            End If
            Return _arrayListFleetCategory
        End Get
    End Property
    Private Shared _dummyData As List(Of Object)
    Private Shared ReadOnly Property DummyData
        Get
            _dummyData = New List(Of Object)
            Dim arrListCustCategory As ArrayList = New ArrayList
            Dim arrListCustName As ArrayList = New ArrayList
            arrListCustCategory.Add("Baru")
            arrListCustCategory.Add("Reguler")
            arrListCustName.Add("PT. Randu Dasamarca")
            arrListCustName.Add("PT. Wicaksana Abadi")

            Dim rnd = New Random()

            For i As Integer = 0 To 19
                Dim randomStatus = ArrayStatusDP(rnd.Next(0, ArrayStatusDP.Count))
                Dim randomCustCategory = arrListCustCategory(rnd.Next(0, arrListCustCategory.Count))
                Dim randomCustName = arrListCustName(rnd.Next(0, arrListCustName.Count))
                _dummyData.Add(
                    New With {
                        Key .ID = i + 1,
                            .No = i + 1,
                            .Status = randomStatus.ToString(),
                            .Dealer = String.Format("Dealer{0}", i + 1),
                            .Term = "Term",
                            .TglPengajuanDiskon = Date.Now.ToString("dd MMM yyyy"),
                            .NoSPL = String.Format("SPL-{0}", i + 1),
                            .NoAplikasi = String.Format("Nomor Aplikasi Dealer {0}", i + 1),
                            .CustomerCategory = randomCustCategory,
                            .CustomerName = randomCustName,
                            .TotalPengajuanDealer = rnd.Next(5000000, 20000000),
                            .TotalAppoved = rnd.Next(5000000, 20000000)
                        }
                )
            Next


            Return _dummyData
        End Get
    End Property
    Enum DPMode
        View
        Edit
    End Enum
#End Region

#Region "Function"
    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub BindDDL()
        Dim listItem As List(Of ListItem) = New List(Of ListItem)
        Dim collDealer As Integer() = {1, 2}
        Dim collMMKSI As Integer() = {3, 4, 8}

        BindToddlCategory()

        If Not IsNothing(objDealer) Then
            If Not IsNothing(Me.objDealer.DealerGroup) Then 'AS DEALER
                listItem = (From i In ArrayStatusDP Where collDealer.Contains(i.Value)).ToList()
            Else
                listItem = (From i In ArrayStatusDP Where collMMKSI.Contains(i.Value)).ToList()
            End If
        Else
            listItem = (From i In ArrayStatusDP Where collMMKSI.Contains(i.Value)).ToList()
        End If

        Try
            lboxStatus.Items.Clear()
            For Each item As ListItem In ArrayStatusDP
                item.Selected = False
                lboxStatus.Items.Add(item)
            Next
            lboxStatus.ClearSelection()
        Catch ex As Exception
            MessageBox.Show("Error Binding lboxStatus, silahkan kirim error ini ke dnet admin")
        End Try

        Try
            ddlStatus.Items.Clear()
            For Each item As ListItem In listItem
                item.Selected = False
                ddlStatus.Items.Add(item)
            Next
            ddlStatus.ClearSelection()
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlStatus, silahkan kirim error ini ke dnet admin")
        End Try
    End Sub
    Private Sub BindToddlCategory()
        Dim arrayListCategory As ArrayList = New PKHeaderFacade(User).RetrieveListCategory

        ddlCategory.Items.Clear()
        If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
            Dim listitemBlank As New ListItem("Pilih", -1)
            ddlCategory.Items.Add(listitemBlank)
        End If

        For Each item As Category In arrayListCategory
            Dim listItem As New ListItem(item.CategoryCode, item.ID)
            If item.CategoryCode = "PC" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) Then
                    ddlCategory.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "LCV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) Then
                    ddlCategory.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "CV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) Then
                    ddlCategory.Items.Add(listItem)
                End If
            End If
        Next
    End Sub
    Private Sub DoCari()
        dgMain.CurrentPageIndex = 0
        ViewState("idxPage") = dgMain.CurrentPageIndex
        BindDataGrid(dgMain.CurrentPageIndex, SearchCriteria(), "", Sort.SortDirection.ASC)
    End Sub
    Private Sub DoTransferToGroupware()
        Dim checkChecked As Boolean = False
        Dim arrCheckedHeader As New ArrayList
        For i As Integer = 0 To dgMain.Items.Count - 1
            If CType(dgMain.Items(i).FindControl("cbxDetail"), CheckBox).Checked Then
                checkChecked = True
                Dim _id As Integer = CType((CType(dgMain.Items(i).FindControl("lblIdDP"), Label)).Text, Integer)
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(DiscountProposalHeader), "ID", MatchType.Exact, _id))
                crit.opAnd(New Criteria(GetType(DiscountProposalHeader), "Status", MatchType.InSet, "(3,9)"))
                Dim arrl As ArrayList = New DiscountProposalHeaderFacade(User).Retrieve(crit)
                Dim oData As New DiscountProposalHeader
                If Not IsNothing(arrl) AndAlso arrl.Count > 0 Then
                    oData = CType(arrl(0), DiscountProposalHeader)
                End If
                Dim arrDPDA As ArrayList = New DiscountProposalDetailApprovalFacade(User).RetrieveByDiscountProposalHeader(oData.ID)
                If Not IsNothing(arrDPDA) AndAlso arrDPDA.Count > 0 Then
                    arrCheckedHeader.Add(oData)
                End If
            End If
        Next
        If checkChecked = False Then
            MessageBox.Show("Check list data minimal satu")
            Return
        End If

        If arrCheckedHeader.Count > 0 Then
            Dim sb As StringBuilder = New StringBuilder
            sb.Append("Data yang akan di transfer ke Groupware:\n")
            For Each obj As DiscountProposalHeader In arrCheckedHeader
                sb.Append("- " & obj.ProposalRegNo & "\n")
            Next
            If (sb.ToString().Length > 0) Then
                MessageBox.Show(sb.ToString())
            End If

            Dim _return As Integer = New DiscountProposalHeaderFacade(User).UpdateTransactionToGroupware(arrCheckedHeader)
            If _return = 1 Then
                MessageBox.Show("Data berhasil ditransfer ke Groupware")
                BindDataGrid(0, SearchCriteria(), "", Sort.SortDirection.ASC)
            Else
                MessageBox.Show("Data gagal ditransfer ke Groupware")
            End If
        Else
            MessageBox.Show("Data harus berstatus Konfirmasi atau Revisi dan mempunyai data Proposed Discount.\nData gagal ditransfer ke Groupware")
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "DaftarDiscounProposal_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim fileTemp As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim directoryTemp As String = Server.MapPath("") & "\..\DataTemp\"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        imp.Start()
        Try

            Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directoryTemp)

            If Not directoryInfo.Exists Then
                directoryInfo.Create()
            End If

            Dim finfo As FileInfo = New FileInfo(fileTemp)
            If finfo.Exists Then
                finfo.Delete()
            End If

            Dim fs As FileStream = New FileStream(fileTemp, FileMode.Create)
            Dim sw As StreamWriter = New StreamWriter(fs)

            WriteDiscProposalData(sw, data)

            sw.Close()
            fs.Close()

            imp.StopImpersonate()
            imp = Nothing

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub
    Private Sub DoProses()
        Dim checkChecked As Boolean = False
        Dim arrDPToProcess As ArrayList = New ArrayList()
        Dim arrDPOldStatus As ArrayList = New ArrayList()
        Dim result As Integer = -1
        Dim newStatus As Integer = CType(ddlStatus.SelectedValue, Integer)

        For i As Integer = 0 To dgMain.Items.Count - 1
            If CType(dgMain.Items(i).FindControl("cbxDetail"), CheckBox).Checked Then
                checkChecked = True
                Dim _id As Integer = CType((CType(dgMain.Items(i).FindControl("lblIdDP"), Label)).Text, Integer)
                Dim objDPNew As DiscountProposalHeader = New DiscountProposalHeaderFacade(User).Retrieve(_id)
                Dim objDPOld As DiscountProposalHeader = New DiscountProposalHeader
                objDPOld.ID = objDPNew.ID
                objDPOld.ProposalRegNo = objDPNew.ProposalRegNo
                objDPOld.Status = objDPNew.Status
                arrDPOldStatus.Add(objDPOld)

                Select Case newStatus
                    Case 2, 4
                        objDPNew.Status = 0
                    Case Else
                        objDPNew.Status = newStatus
                End Select
                arrDPToProcess.Add(objDPNew)
            End If
        Next

        If checkChecked = False Then
            MessageBox.Show("Check list data minimal satu")
            Return
        End If

        Dim list As List(Of DiscountProposalHeader) = arrDPOldStatus.Cast(Of DiscountProposalHeader)().ToList()
        Dim listStatus As List(Of Int16) = list.Select(Function(i) i.Status).GroupBy(Function(g) g).Select(Function(x) x.Key).ToList()
        If listStatus.Count = 0 Then
            MessageBox.Show("Tidak ada Status yang Valid")
            Return
        End If

        Dim _statusValidasi As String = String.Empty
        Dim strResult As String = String.Empty

        If newStatus = 1 Then           '--- Status Validasi
            _statusValidasi = "0"       ' Status Baru
        ElseIf newStatus = 2 Then           '--- Status Batal Validasi
            _statusValidasi = "1"       ' Status Validasi
        ElseIf newStatus = 3 Then           '--- Status Konfirmasi
            _statusValidasi = "1"       ' Status Validasi
        ElseIf newStatus = 4 Then           '--- Status Batal_Konfirmasi
            _statusValidasi = "3"       ' Status Konfirmasi
        ElseIf newStatus = 5 Then           '--- Status Proses
            _statusValidasi = "3"       ' Status Konfirmasi
        ElseIf newStatus = 8 Then           '--- Status Tidak Setuju
            _statusValidasi = "3;9"         'Status Konfirmasi dan Revisi
        End If

        Dim isSuccessUpdate As Boolean = False
        strResult = CekStatusValidation(arrDPOldStatus, arrDPToProcess, _statusValidasi, newStatus.ToString, isSuccessUpdate)
        If strResult <> "" Then
            MessageBox.Show(strResult)
        End If
        If strResult = "" OrElse isSuccessUpdate = False Then
            Return
        End If

        result = New DiscountProposalHeaderFacade(User).UpdateStatus(arrDPToProcess, arrDPOldStatus)
        If result = -1 Then
            MessageBox.Show("Update Status gagal")
            Return
        End If
        MessageBox.Show("Update Status berhasil")
        BindDataGrid(0, SearchCriteria(), "", Sort.SortDirection.ASC)
    End Sub

    Function CekStatusValidation(ByRef arrDPOldStatus As ArrayList, ByRef arrDPToProcess As ArrayList, ByVal _statusValidasi As String, ByVal _statusNew As String, ByRef isSuccessUpdate As Boolean) As String
        Dim strResult As String = String.Empty
        Dim strProposalRegNoValid As String = String.Empty
        Dim strProposalRegNoNotValid As String = String.Empty
        arrDPOldStatus = _
                    New System.Collections.ArrayList(
                        (From obj As DiscountProposalHeader In arrDPOldStatus.OfType(Of DiscountProposalHeader)()
                            Order By obj.ID, obj.Status
                            Select obj).ToList())

        arrDPToProcess = _
                    New System.Collections.ArrayList(
                        (From obj As DiscountProposalHeader In arrDPToProcess.OfType(Of DiscountProposalHeader)()
                            Order By obj.ID, obj.Status
                            Select obj).ToList())

        For Each oDiscountProposalHeader As DiscountProposalHeader In arrDPOldStatus
            If InStr(_statusValidasi.Replace(";", ""), oDiscountProposalHeader.Status) = 0 Then
                If strProposalRegNoNotValid = "" Then
                    strProposalRegNoNotValid = oDiscountProposalHeader.ProposalRegNo
                Else
                    strProposalRegNoNotValid += ", " & oDiscountProposalHeader.ProposalRegNo
                End If
            Else
                If strProposalRegNoValid = "" Then
                    strProposalRegNoValid = oDiscountProposalHeader.ProposalRegNo
                Else
                    strProposalRegNoValid += ", " & oDiscountProposalHeader.ProposalRegNo
                End If
            End If
        Next
        For j As Integer = arrDPOldStatus.Count - 1 To 0 Step -1
            Dim obj As DiscountProposalHeader = CType(arrDPOldStatus(j), DiscountProposalHeader)
            If strProposalRegNoNotValid.Trim <> "" Then
                For Each regNo As String In strProposalRegNoNotValid.Split(",")
                    If obj.ProposalRegNo.Trim() = regNo.Trim() Then
                        arrDPOldStatus.Remove(obj)
                        arrDPToProcess.RemoveAt(j)
                        Exit For
                    End If
                Next
            End If
        Next

        Dim strStatusName As String = String.Empty
        Dim strWarningStatus As String = String.Empty
        For Each strStatusID As String In _statusValidasi.Split(";")
            strStatusName = CType(New StandardCodeFacade(User).RetrieveByValueId(strStatusID, "EnumDiscountProposal.Status")(0), StandardCode).ValueDesc
            If strWarningStatus = "" Then
                strWarningStatus = "[" & strStatusName & "]"
            Else
                strWarningStatus += " atau [" & strStatusName & "]"
            End If
        Next

        Dim strMessageValid As String = String.Empty
        Dim strMessageNotValid As String = String.Empty
        If strProposalRegNoNotValid.Trim <> "" Then
            strMessageNotValid = "No Reg : " & strProposalRegNoNotValid & " statusnya bukan " & strWarningStatus
        End If
        If strProposalRegNoValid.Trim <> "" Then
            strStatusName = CType(New StandardCodeFacade(User).RetrieveByValueId(_statusNew, "EnumDiscountProposal.Status")(0), StandardCode).ValueDesc
            strMessageValid = "No Reg : " & strProposalRegNoValid & " selanjutnya akan di update ke Status " & strStatusName
        End If
        If strProposalRegNoValid.Trim <> "" AndAlso strProposalRegNoNotValid.Trim <> "" Then
            strResult = strMessageValid & "\n" & strMessageNotValid
            isSuccessUpdate = True
        ElseIf strProposalRegNoValid.Trim = "" AndAlso strProposalRegNoNotValid.Trim <> "" Then
            strResult = strMessageNotValid
            isSuccessUpdate = False
        ElseIf strProposalRegNoValid.Trim <> "" AndAlso strProposalRegNoNotValid.Trim = "" Then
            strResult = strMessageValid
            isSuccessUpdate = True
        End If

        Return strResult
    End Function

    Private Sub WriteDiscProposalData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim sbHeader As StringBuilder = New StringBuilder
        Dim sbDetail As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            sbHeader.Append("No" & tab)
            sbHeader.Append("Status" & tab)
            sbHeader.Append("Tanggal Pengajuan Dealer" & tab)
            sbHeader.Append("No.Aplikasi Dealer" & tab)
            sbHeader.Append("No.Reg Aplikasi" & tab)
            sbHeader.Append("Dealer" & tab)
            sbHeader.Append("Term1" & tab)
            sbHeader.Append("Nama Customer" & tab)
            sbHeader.Append("Model" & tab)
            sbHeader.Append("Tipe" & tab)
            sbHeader.Append("Warna" & tab)
            sbHeader.Append("Assy Year" & tab)
            sbHeader.Append("Unit" & tab)
            sbHeader.Append("Permohonan Diskon" & tab)
            sbHeader.Append("Diskon Disetujui" & tab)
            sw.WriteLine(sbHeader.ToString())

            Dim _no As Integer = 1
            For Each itemDPH As DiscountProposalHeader In data
                Dim al As ArrayList = New DiscountProposalHeaderFacade(User).RetrieveDownloadDataLine(itemDPH.ID)
                Dim objListItemStatus As ListItem = (From i As ListItem In ArrayStatusDP Where CType(i.Value, Short) = itemDPH.Status).FirstOrDefault()
                For Each dr As DataRow In al
                    sbDetail.Clear()
                    sbDetail.Append(_no & tab) 'No
                    sbDetail.Append(objListItemStatus.Text & tab)
                    sbDetail.Append(CType(dr.Item(1), Date).ToString("MM/dd/yyyy") & tab)
                    sbDetail.Append(dr.Item(2).ToString() & tab)
                    sbDetail.Append(dr.Item(3).ToString() & tab)
                    sbDetail.Append(dr.Item(4).ToString() & tab)
                    sbDetail.Append(dr.Item(5).ToString() & tab)
                    sbDetail.Append(dr.Item(6).ToString() & tab)
                    sbDetail.Append(dr.Item(7).ToString() & tab)
                    sbDetail.Append(dr.Item(8).ToString() & tab)
                    sbDetail.Append(dr.Item(9).ToString() & tab)
                    sbDetail.Append(dr.Item(10).ToString() & tab)
                    sbDetail.Append(dr.Item(11).ToString() & tab)
                    sbDetail.Append(If(dr.Item(12).ToString <> "", CType(dr.Item(12), Decimal).ToString("#,##0.00"), "") & tab)
                    If IsDBNull(dr.Item(13)) Then
                        sbDetail.Append(CType(0, Decimal).ToString("#,##0.00") & tab)
                    Else
                        sbDetail.Append(CType(dr.Item(13), Decimal).ToString("#,##0.00") & tab)
                    End If

                    sw.WriteLine(sbDetail.ToString())
                    _no += 1
                Next
            Next
        End If
    End Sub
    'Private Sub WriteDiscProposalData2(_excel As ExcelPackage, ByVal data As ArrayList)
    '    Dim tab As Char = Chr(9)
    '    Dim sbHeader As StringBuilder = New StringBuilder
    '    Dim sbDetail As StringBuilder = New StringBuilder
    '    Dim arrList As ArrayList = New ArrayList

    '    Dim dt As DataTable = New DataTable
    '    dt.Columns.Add("No")
    '    dt.Columns.Add("Status")
    '    dt.Columns.Add("Tgl Pengajuan Dealer")
    '    dt.Columns.Add("No Aplikasi Dealer")
    '    dt.Columns.Add("No Reg Aplikasi")
    '    dt.Columns.Add("Kode Dealer")
    '    dt.Columns.Add("Term 1")
    '    dt.Columns.Add("Nama Customer")
    '    dt.Columns.Add("Model")
    '    dt.Columns.Add("Tipe")
    '    dt.Columns.Add("Warna")
    '    dt.Columns.Add("Assy Year")
    '    dt.Columns.Add("Unit")
    '    dt.Columns.Add("Permohonan Diskon")
    '    dt.Columns.Add("Total Disetujui")

    '    If Not IsNothing(Data) Then

    '        Dim _no As Integer = 1
    '        For Each itemDPH As DiscountProposalHeader In Data
    '            Dim _dt As DataTable = New DiscountProposalHeaderFacade(User).RetrieveDownloadDataLine(itemDPH.ID)

    '            For Each dr As DataRow In _dt.Rows
    '                dt.ImportRow(dr)
    '            Next
    '        Next
    '    End If
    '    'Dim _excel As ExcelPackage = New ExcelPackage(finfo)
    '    '_excel.Load(fs)
    '    Dim _ws As ExcelWorksheet = _excel.Workbook.Worksheets.Add("Sheet1")
    '    _ws.Cells("A1").LoadFromDataTable(dt, True)
    '    _ws.Cells(_ws.Dimension.Address).AutoFitColumns()
    '    'Dim ms As MemoryStream = New MemoryStream(_excel.GetAsByteArray())
    '    'Dim sw As StreamWriter = New StreamWriter(ms)

    '    '_excel.Save()
    'End Sub
    Private Sub BindDataGrid(ByVal index As Integer, ByVal criteria As CriteriaComposite, Optional ByVal sortColumn As String = Nothing, Optional ByVal sortType As Sort.SortDirection = Sort.SortDirection.ASC)
        Dim _arrList As ArrayList = New ArrayList
        'Dim _arrList As List(Of Object) = New List(Of Object)
        Dim PagedList As ArrayList = New ArrayList
        Dim _totalRow As Integer = 0

        PagedList = New DiscountProposalHeaderFacade(User).RetrieveByCriteria(criteria, index + 1, dgMain.PageSize, _totalRow, ViewState("currentSortColumn"), ViewState("currentSortDirection"))
        If Not IsNothing(PagedList) Then
            _arrList = PagedList
        End If

        If dgMain.CurrentPageIndex = 0 Then
            dgMain.CurrentPageIndex = index
        End If

        dgMain.VirtualItemCount = _totalRow
        dgMain.DataSource = _arrList
        dgMain.DataBind()
    End Sub
    Private Sub DoDeleteDP(ByVal idDP As Integer)
        Dim objDiscountProposalHeader As DiscountProposalHeader = New DiscountProposalHeaderFacade(User).Retrieve(idDP)
        Dim fn As DiscountProposalHeaderFacade = New DiscountProposalHeaderFacade(User)
        objDiscountProposalHeader.RowStatus = 1
        Try
            fn.Delete(objDiscountProposalHeader)
            BindDataGrid(0, SearchCriteria(), "", Sort.SortDirection.ASC)
            MessageBox.Show(SR.DeleteSucces)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try
    End Sub
    Private Sub DoChangeStatus()

    End Sub
    Private Function SearchCriteria()
        Dim objSessionHelper As New SessionHelper
        Dim objDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim critDPDealerCode As String = IIf(IsNothing(txtKodeDealer.Text), "", txtKodeDealer.Text.Trim)
        Dim critDPNo As String = IIf(IsNothing(txtDPNo.Text), "", txtDPNo.Text.Trim)
        Dim critPropRegNo As String = IIf(IsNothing(txtPropRegNo.Text), "", txtPropRegNo.Text.Trim)
        Dim critCustomerName As String = IIf(IsNothing(txtCustomerName.Text), "", txtCustomerName.Text.Trim)
        Dim critFromDateDP As Date = IIf(cbDateClaim.Checked, icDateClaim.Value, Nothing)
        Dim critToDateDP As Date = IIf(cbDateClaim.Checked, icDateClaimTo.Value, Nothing)
        'Dim critStatus As Integer = If(lboxStatus.SelectedValue = "", -1, CType(lboxStatus.SelectedValue, Integer))
        Dim critStatus As Integer() = lboxStatus.GetSelectedIndices()
        Dim selectedCategory As Integer = CType(If(ddlCategory.SelectedValue = "", -1, ddlCategory.SelectedValue), Integer)
        Dim selectedSubCategory As Integer = If(ddlSubCategory.SelectedValue = "", -1, CType(ddlSubCategory.SelectedValue, Integer))

        If Not selectedCategory = -1 Then
            Dim strSql As String = "select a.ID  from DiscountProposalHeader as a " _
                                       & "join DiscountProposalDetail b on b.DiscountProposalHeaderID = a.ID and b.RowStatus = 0 " _
                                       & "join DiscountProposalDetailPrice c on c.DiscountProposalDetailID = b.ID and c.DiscountProposalHeaderID = a.ID and c.RowStatus = 0 " _
                                       & "join dealer e on e.ID = a.DealerID and e.RowStatus = 0 " _
                                       & "join VechileColorIsActiveOnPK f0 on f0.ID = b.VechileColorIsActiveOnPKID and f0.rowstatus = 0 " _
                                       & "join VechileColor f on f.ID = f0.VehicleColorID and f.rowstatus = 0 " _
                                       & "join VechileTypeGeneral g on g.ID = f0.VechileTypeGeneralID and g.rowstatus = 0 " _
                                       & "join vechileType h0 on h0.ID = f.VechileTypeID and h0.rowstatus = 0 " _
                                       & "join subCategoryVehicle j on j.ID = b.SubCategoryVehicleID and j.rowstatus = 0  " _
                                       & "join fleetcustomerDetail k on k.ID = a.fleetCustomerDetailID and k.rowstatus = 0 " _
                                       & "join category l on l.ID = j.categoryID and l.ID = h0.categoryID and l.rowstatus = 0 "
            If Not selectedSubCategory = -1 Then
                strSql = strSql + "" _
                        & String.Format("where l.ID={0}", selectedCategory) _
                        & String.Format("and j.ID={0}", selectedSubCategory)
                criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
            Else
                strSql = strSql + String.Format("where l.ID={0}", selectedCategory)
                criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
            End If
        End If

        If Not IsNothing(objDealer) Then
            If Not objDealer.DealerGroup Is Nothing Then
                criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
            Else
                If Not critDPDealerCode = "" Then
                    Dim value As String = ""
                    For Each item As String In critDPDealerCode.Replace(" ", "").Split(";")
                        If Not item Is Nothing And Not item = "" Then
                            value = value + "'" + item + "',"
                        End If
                    Next
                    value = value.Remove(value.Length - 1, 1)
                    Dim strSql As String = ""
                    strSql += "  select a.ID  from Dealer as a"
                    strSql += "  where a.RowStatus = 0"
                    strSql += String.Format(" and a.DealerCode in ({0}) ", value)
                    criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "Dealer.ID", MatchType.InSet, "(" & strSql & ")"))
                End If
            End If
        End If

        If Not critDPNo.Trim = "" Then
            criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "DealerProposalNo", MatchType.Partial, critDPNo.Trim))
        End If
        If Not critPropRegNo.Trim = "" Then
            criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "ProposalRegNo", MatchType.Partial, critPropRegNo.Trim))
        End If

        If Not critCustomerName.Trim = "" Then
            Dim strSql As String = ""
            strSql += "  select a.ID  from FleetCustomerDetail a join FleetCustomerHeader b on a.FleetCustomerHeaderID = b.ID and b.RowStatus = 0"
            strSql += "  where a.RowStatus = 0"
            strSql += String.Format(" and b.FleetCustomerName like '%{0}%' ", critCustomerName.Trim)
            criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "FleetCustomerDetail.ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        If cbDateClaim.Checked Then
            criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "SubmitDate", MatchType.GreaterOrEqual, critFromDateDP))
            criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "SubmitDate", MatchType.LesserOrEqual, critToDateDP))
        End If

        Dim sStatusCollection As String = ""
        If critStatus.Length > 1 Then
            sStatusCollection = "(" + String.Join(",", critStatus) + ")"
            criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "Status", MatchType.InSet, sStatusCollection))
        Else
            If critStatus.Length = 1 Then
                criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "Status", MatchType.Exact, critStatus(0)))
            End If
        End If

        SaveCriteria()
        Return criterias
    End Function

    Private Sub RestoreCriteria()
        If Not IsNothing(sessHelper.GetSession(_sesCriteria)) Then
            Dim ObjCriteria As New ArrayList
            ObjCriteria = CType(sessHelper.GetSession(_sesCriteria), ArrayList)

            ddlCategory.SelectedValue = ObjCriteria(0).ToString()
            ddlSubCategory.SelectedValue = ObjCriteria(1).ToString()
            lboxStatus.SelectedValue = ObjCriteria(2).ToString()
            txtKodeDealer.Text = ObjCriteria(3).ToString()
            txtDPNo.Text = ObjCriteria(4).ToString()
            txtPropRegNo.Text = ObjCriteria(5).ToString()
            txtCustomerName.Text = ObjCriteria(6).ToString()
            cbDateClaim.Checked = ObjCriteria(7).ToString()
            icDateClaim.Value = ObjCriteria(8).ToString()
            icDateClaimTo.Value = ObjCriteria(9).ToString()

            ViewState("idxPage") = CInt(ObjCriteria(10))
            ViewState("CurrentSortColumn") = ObjCriteria(11)
            ViewState("currentSortDirection") = ObjCriteria(12)
        End If
    End Sub

    Private Sub SaveCriteria()
        Dim ObjCriteria As New ArrayList
        ObjCriteria.Add(ddlCategory.SelectedValue.Trim())
        ObjCriteria.Add(ddlSubCategory.SelectedValue.Trim())
        ObjCriteria.Add(lboxStatus.SelectedValue.Trim())
        ObjCriteria.Add(txtKodeDealer.Text.Trim())
        ObjCriteria.Add(txtDPNo.Text.Trim())
        ObjCriteria.Add(txtPropRegNo.Text.Trim())
        ObjCriteria.Add(txtCustomerName.Text.Trim())
        ObjCriteria.Add(cbDateClaim.Checked)
        ObjCriteria.Add(icDateClaim.Value)
        ObjCriteria.Add(icDateClaimTo.Value)

        ObjCriteria.Add(ViewState("idxPage"))
        ObjCriteria.Add(ViewState("currentSortColumn"))
        ObjCriteria.Add(ViewState("currentSortDirection"))
        sessHelper.SetSession(_sesCriteria, ObjCriteria)
    End Sub

    Private Sub RetrieveDealer()
        Dim objSessionHelper As New SessionHelper
        Me.objDealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        Dim objDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        If Not objDealer Is Nothing Then
            If Not objDealer.DealerGroup Is Nothing Then
                lblDealerSession.Visible = True
                lblPopUpDealer.Visible = Not lblDealerSession.Visible
                txtKodeDealer.Attributes("style") = "display:none"
                lblDealerSession.Text = objDealer.DealerCode & " / " & objDealer.DealerName
                txtKodeDealer.Text = objDealer.DealerCode
                btnTransfer.Visible = False
                btnProses.Style("Width") = "150px"
            End If
        End If
    End Sub
    Private Function EnabledDisabledCheckBox(ByVal objDomain As DiscountProposalHeader)
        Dim objSessionHelper As New SessionHelper
        Me.objDealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        If Not IsNothing(Me.objDealer) Then
            If Not IsNothing(Me.objDealer.DealerGroup) Then 'AS DEALER
                Select Case objDomain.Status
                    Case 0, 1
                        Return True
                    Case 3, 5, 6, 7, 8
                        Return False
                End Select
            Else
                Select Case objDomain.Status
                    Case 1, 3
                        Return True
                    Case 0, 5, 6, 7, 8
                        Return False
                End Select
            End If
        Else
            Select Case objDomain.Status
                Case 1, 3
                    Return True
                Case 0, 5, 6, 7, 8
                    Return False
            End Select
        End If
    End Function
    Private Function EnabledDisabledButtonDelete(ByVal objDomain As DiscountProposalHeader)
        Select Case objDomain.Status
            Case 0 'Baru
                If IsLoginAsDealer() Then
                    Return True
                Else
                    Return False
                End If
            Case Else
                Return False
        End Select
    End Function
    Private Function EnabledDisabledButtonEdit(ByVal objDomain As DiscountProposalHeader)
        Select Case objDomain.Status
            Case 0 'Baru
                If IsLoginAsDealer() Then
                    Return True
                Else
                    Return False
                End If
            Case 3 'Konfirmasi
                If IsLoginAsDealer() Then
                    Return False
                Else
                    Return True
                End If
            Case 9 'Revisi
                If IsLoginAsDealer() Then
                    Return False
                Else
                    Return True
                End If
            Case Else
                Return False
        End Select
    End Function
    Private Function GridDetailDataSource(ByRef strProposalRegNo As String, ByVal selectedCategory As Integer, ByVal selectedSubCategory As Integer) As List(Of Vm_DetailGrid)
        Dim ar As ArrayList
        Dim result As List(Of Vm_DetailGrid) = New List(Of Vm_DetailGrid)
        ar = New DiscountProposalDetailFacade(User).RetrieveByHeaderID(strProposalRegNo, selectedCategory, selectedSubCategory)
        For Each dr As DataRow In ar
            result.Add(New Vm_DetailGrid With {
                       .Model = dr("Model").ToString(),
                       .Type = dr("Tipe").ToString(),
                       .Warna = dr("Warna").ToString(),
                       .AssyYear = dr("AssyYear").ToString(),
                       .Unit = dr("Unit").ToString(),
                       .PermohonanDiskon = If(IsDBNull(dr("PermohonanDiskon")), 0, dr("PermohonanDiskon").ToString()),
                       .DiskonDisetujui = If(IsDBNull(dr("DiskonDisetujui")), 0, dr("DiskonDisetujui").ToString())
            })
        Next
        Return result
    End Function
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
            If Not SecurityProvider.Authorize(Context.User, SR.DP_DaftarDP_Lihat_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=DISCOUNT PROPOSAL - Daftar Discount Proposal")
            End If
        Else
            ' case from dealer
            If Not SecurityProvider.Authorize(Context.User, SR.DP_DaftarDP_Lihat_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=DISCOUNT PROPOSAL - Daftar Discount Proposal")
            End If
        End If
    End Sub

#End Region

#Region "EventHandler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            ViewState("currentSortColumn") = "ID"
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
            ViewState("idxPage") = 0

            RetrieveDealer()
            BindDDL()
            RestoreCriteria()
            Dim Page As Integer = 0
            If Not IsNothing(ViewState("idxPage")) Then
                Page = ViewState("idxPage")
            End If
            BindDataGrid(Page, SearchCriteria(), ViewState("currentSortColumn"), ViewState("currentSortDirection"))
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        DoCari()
    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        DoProses()
    End Sub

    Protected Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        DoTransferToGroupware()
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim _arrList As ArrayList = New ArrayList
        Dim _chekced As Boolean = False
        For i As Integer = 0 To dgMain.Items.Count - 1
            If CType(dgMain.Items(i).FindControl("cbxDetail"), CheckBox).Checked Then
                _chekced = True
                Dim _id As Integer = CType((CType(dgMain.Items(i).FindControl("lblIdDP"), Label)).Text, Integer)
                Dim objDPH As DiscountProposalHeader = New DiscountProposalHeaderFacade(User).Retrieve(_id)
                _arrList.Add(objDPH)
            End If
        Next
        If Not _chekced Then
            _arrList = New DiscountProposalHeaderFacade(User).Retrieve(SearchCriteria())
        End If
        DoDownload(_arrList)
    End Sub

    Protected Sub dgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgMain.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then

            Dim objDomain As DiscountProposalHeader = CType(e.Item.DataItem, DiscountProposalHeader)

            Dim lblIdDP As Label = CType(e.Item.FindControl("lblIdDP"), Label)
            Dim lblNoGridRow As Label = CType(e.Item.FindControl("lblNoGridRow"), Label)
            Dim lblStatusDP As Label = CType(e.Item.FindControl("lblStatusDP"), Label)
            Dim lblDealerDP As Label = CType(e.Item.FindControl("lblDealerDP"), Label)
            Dim lblTermDP As Label = CType(e.Item.FindControl("lblTermDP"), Label)
            Dim lblRequestDateDP As Label = CType(e.Item.FindControl("lblRequestDateDP"), Label)
            Dim lblNoSPLDP As Label = CType(e.Item.FindControl("lblNoSPLDP"), Label)
            Dim lblNoAppDealerDP As Label = CType(e.Item.FindControl("lblNoAppDealerDP"), Label)
            Dim lblCustCategoryDP As Label = CType(e.Item.FindControl("lblCustCategoryDP"), Label)
            Dim lblCustName As Label = CType(e.Item.FindControl("lblCustName"), Label)
            Dim lblTotalRequestAmountDP As Label = CType(e.Item.FindControl("lblTotalRequestAmountDP"), Label)
            Dim lblTotalApprovedDP As Label = CType(e.Item.FindControl("lblTotalApprovedDP"), Label)
            Dim objListItemStatus As ListItem = (From i As ListItem In ArrayStatusDP Where CType(i.Value, Short) = objDomain.Status).FirstOrDefault()
            Dim objListItemFleetCategory As ListItem = (From i As ListItem In ArrayFleetCustomer Where CType(i.Value, Short) = objDomain.FleetCategory).FirstOrDefault()
            Dim totalDiscountRequest As Decimal = (From i As DiscountProposalDetailPrice In objDomain.DiscountProposalDetailPrices Select i.DiscountRequest).Sum()

            Dim chkBox As CheckBox = CType(e.Item.FindControl("cbxDetail"), CheckBox)
            chkBox.Enabled = True 'EnabledDisabledCheckBox(objDomain)

            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            lnkbtnEdit.Visible = EnabledDisabledButtonEdit(objDomain)

            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
            lnkbtnDelete.Visible = EnabledDisabledButtonDelete(objDomain)

            Dim strMode As String = "New"
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), "DiscountProposalHeader.ID", MatchType.Exact, objDomain.ID))
            Dim arrDiscountProposalDetailDocument As ArrayList = New DiscountProposalDetailDocumentFacade(User).Retrieve(criterias2)
            If Not IsNothing(arrDiscountProposalDetailDocument) AndAlso arrDiscountProposalDetailDocument.Count > 0 Then
                strMode = "Edit"
            End If
            Dim lnkbtnUpload As LinkButton = CType(e.Item.FindControl("lnkbtnUpload"), LinkButton)
            If Not IsLoginAsDealer() Then
                lnkbtnUpload.Visible = False
            End If
            lnkbtnUpload.Attributes.Add("onclick", "showPopUp('../General/../PopUp/PopUpDPDocumentUpload.aspx?DiscountProposalHeaderID=" & objDomain.ID & "&Mode=" & strMode & "', '', 450, 550, isSuccesUpload);")

            lblIdDP.Text = objDomain.ID.ToString()
            lblNoGridRow.Text = (e.Item.ItemIndex + 1 + (dgMain.CurrentPageIndex * dgMain.PageSize)).ToString
            lblStatusDP.Text = objListItemStatus.Text
            lblDealerDP.Text = objDomain.Dealer.DealerCode
            lblTermDP.Text = objDomain.Dealer.SearchTerm1
            lblRequestDateDP.Text = objDomain.SubmitDate.ToString("dd/MM/yyyy")
            lblNoSPLDP.Text = If(Not IsNothing(objDomain.ProposalRegNo), objDomain.ProposalRegNo, "-")
            lblNoAppDealerDP.Text = objDomain.DealerProposalNo
            lblCustCategoryDP.Text = If(Not IsNothing(objListItemFleetCategory), objListItemFleetCategory.Text, "")
            lblCustName.Text = If(Not IsNothing(objDomain.FleetCustomerDetail), objDomain.FleetCustomerDetail.FleetCustomerHeader.FleetCustomerName, "")
            lblTotalRequestAmountDP.Text = totalDiscountRequest.ToString("#,##0.00")
            lblTotalApprovedDP.Text = "0"

            Dim lnkbtnStatusHistory As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
            lnkbtnStatusHistory.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistoryDiscProposal.aspx?DocType=17&DocNumber=" & objDomain.ProposalRegNo, "", 370, 400, "DealerSelection")

            Dim selectedCategory As Integer = CType(If(ddlCategory.SelectedValue = "", -1, ddlCategory.SelectedValue), Integer)
            Dim selectedSubCategory As Integer = If(ddlSubCategory.SelectedValue = "", -1, CType(ddlSubCategory.SelectedValue, Integer))

            Dim dsDetail As List(Of Vm_DetailGrid) = GridDetailDataSource(objDomain.ProposalRegNo, selectedCategory, selectedSubCategory)
            dgDetail = CType(e.Item.FindControl("dtgDetail"), DataGrid)
            AddHandler dgDetail.ItemDataBound, New System.Web.UI.WebControls.DataGridItemEventHandler(AddressOf dtgDetail_ItemDataBound)
            dgDetail.DataSource = dsDetail
            dgDetail.DataBind()

        End If
    End Sub

    Private Sub dtgDetail_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgDetail.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As Vm_DetailGrid = CType(e.Item.DataItem, Vm_DetailGrid)
            'Dim objDiscountProposalDetail As DiscountProposalDetail = CType(e.Item.DataItem, DiscountProposalDetail)

            Dim lblModel As Label = CType(e.Item.FindControl("lblModel"), Label)
            Dim lblType As Label = CType(e.Item.FindControl("lblType"), Label)
            Dim lblWarna As Label = CType(e.Item.FindControl("lblWarna"), Label)
            Dim lblAssyYear As Label = CType(e.Item.FindControl("lblAssyYear"), Label)
            Dim lblUnit As Label = CType(e.Item.FindControl("lblUnit"), Label)
            Dim lblPermohonanDiskon As Label = CType(e.Item.FindControl("lblPermohonanDiskon"), Label)
            Dim lblDiskonDisetujui As Label = CType(e.Item.FindControl("lblDiskonDisetujui"), Label)

            lblModel.Text = RowValue.Model
            lblType.Text = RowValue.Type
            lblWarna.Text = RowValue.Warna
            lblAssyYear.Text = RowValue.AssyYear
            lblUnit.Text = RowValue.Unit
            lblPermohonanDiskon.Text = Format(CDbl(RowValue.PermohonanDiskon), "#,##0")
            lblDiskonDisetujui.Text = Format(CDbl(RowValue.DiskonDisetujui), "#,##0")

            'lblModel.Text = objDiscountProposalDetail.SubCategoryVehicle.Name
            'lblType.Text = objDiscountProposalDetail.VechileColor.VechileType.Description
            'lblWarna.Text = objDiscountProposalDetail.VechileColor.ColorIndName
            'lblAssyYear.Text = objDiscountProposalDetail.AssyYear
            'lblUnit.Text = objDiscountProposalDetail.ProposeQty

            'Dim list As List(Of DiscountProposalDetailPrice) = objDiscountProposalDetail.DiscountProposalDetailPrices.Cast(Of DiscountProposalDetailPrice)().ToList()
            'lblPermohonanDiskon.Text = list.Where(Function(i) i.DiscountProposalDetail.ID = objDiscountProposalDetail.ID).FirstOrDefault().DealPriceEstimation

            'lblClaimRegNoDealer.Text = RowValue.ClaimRegNo
            'lblDescription.Text = RowValue.Description
            'lblAmountDeducted.Text = RowValue.AmountDeducted.ToString("#,##0")
            'lblCreatedTime.Text = RowValue.TransactionDate.ToString("dd/MM/yyyy")
        End If
    End Sub

    Protected Sub dgMain_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgMain.ItemCommand
        Select Case e.CommandName
            Case "View"
                Response.Redirect("~/DiscountProposal/FrmInputDiscountProposal.aspx?Mode=View&DiscountProposalHeaderID=" & CInt(e.CommandArgument))
            Case "Edit"

                Response.Redirect("~/DiscountProposal/FrmInputDiscountProposal.aspx?Mode=Edit&DiscountProposalHeaderID=" & CInt(e.CommandArgument))
            Case "Delete"
                DoDeleteDP(CInt(e.CommandArgument))
        End Select
    End Sub

    Protected Sub dgMain_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgMain.PageIndexChanged
        dgMain.CurrentPageIndex = e.NewPageIndex
        ViewState("idxPage") = dgMain.CurrentPageIndex
        BindDataGrid(dgMain.CurrentPageIndex, SearchCriteria())
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub

#End Region

    Private Class Vm_DetailGrid
        Private _model As String
        Private _type As String
        Private _warna As String
        Private _assyYear As String
        Private _unit As String
        Private _permohonanDiskon As String
        Private _diskonDisetujui As String

        Public Property Model() As String
            Get
                Return _model
            End Get
            Set(ByVal value As String)
                _model = value
            End Set
        End Property
        Public Property Type() As String
            Get
                Return _type
            End Get
            Set(ByVal value As String)
                _type = value
            End Set
        End Property
        Public Property Warna() As String
            Get
                Return _warna
            End Get
            Set(ByVal value As String)
                _warna = value
            End Set
        End Property
        Public Property AssyYear() As String
            Get
                Return _assyYear
            End Get
            Set(ByVal value As String)
                _assyYear = value
            End Set
        End Property
        Public Property Unit() As String
            Get
                Return _unit
            End Get
            Set(ByVal value As String)
                _unit = value
            End Set
        End Property
        Public Property PermohonanDiskon() As String
            Get
                Return _permohonanDiskon
            End Get
            Set(ByVal value As String)
                _permohonanDiskon = value
            End Set
        End Property
        Public Property DiskonDisetujui() As String
            Get
                Return _diskonDisetujui
            End Get
            Set(ByVal value As String)
                _diskonDisetujui = value
            End Set
        End Property

    End Class

End Class