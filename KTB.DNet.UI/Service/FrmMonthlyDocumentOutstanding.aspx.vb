
Imports System
Imports System.IO
Imports System.Text
Imports Excel
Imports System.Threading
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.Globalization
Imports System.Collections.Generic
Imports KTB.DNet.BusinessFacade

Public Class FrmMonthlyDocumentOutstanding
    Inherits System.Web.UI.Page

#Region "Custom Variable Declaration"

    Private ArlMonthly As ArrayList
    Private objMonthy As MonthlyDocument
    Private sessionHelper As New SessionHelper
    Private ArlSearchCriteria As ArrayList
    Private objDealer As Dealer

#End Region

#Region "Custom Method"

    Private Sub RetrieveMaster()
        '--DropDownList JenisDokumen
        'add privilege
        Dim arlList As New ArrayList
        If Not SecurityProvider.Authorize(Context.User, SR.DokumenServiceAll_Privilege) Then
            Me.ddlJenisDokumen.DataSource = New ArrayList
            Me.ddlJenisDokumen.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            Dim arlListTemp As ArrayList = MonthlyDocumentType.RetrieveDocumentOutstandingType()
            For Each item As MonthlyDocumentTypeListItem In arlListTemp
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_kwsc01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_ESP_Warranty Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.DokumenService_kfsc01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Service_Campaign Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lfs001_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Service_Regular_Letter Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lfsc01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Service_Campaign_Letter Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lwsc01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Warranty_ESP_Letter Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.DokumenService_wscsta_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Warranty_ESP_Status_List Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.Kwitansi_Free_Maintenance_and_Campaign_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Maintenance_and_Campaign Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.Free_Maintenance_and_campaign_letter_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Maintenance_and_campaign_letter Then
                        arlList.Add(item)
                    End If
                End If

                'If SecurityProvider.Authorize(Context.User, SR.DokumenService_fll01_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_ESP_Labour_Letter Then
                    arlList.Add(item)
                End If
                'End If

                'If SecurityProvider.Authorize(Context.User, SR.DokumenService_kfl01_Privilege) Then
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_ESP_Free_Labour Then
                    arlList.Add(item)
                End If
                'End If

            Next
            'Start  : by donas for Yurike/b Widya on 2014.09.09'
            Dim aList As New ArrayList
            For Each item As MonthlyDocumentTypeListItem In arlList
                If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kend_Belum_PDI_List Then
                Else
                    aList.Add(item)
                End If
            Next
            arlList = aList
            'End    : by donas for Yurike/b Widya on 2014.09.09'

            Me.ddlJenisDokumen.DataSource = arlList
            Me.ddlJenisDokumen.DataTextField = "NameStatus"
            Me.ddlJenisDokumen.DataValueField = "ValStatus"
            Me.ddlJenisDokumen.DataBind()
            Me.ddlJenisDokumen.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        End If

        If ddlJenisDokumen.Items.Count = 0 Then
            btnCari.Enabled = False
        End If

        '--DropDownList PeriodeDokumen
        For Each item As ListItem In LookUp.ArraylistMonthCalendar()
            ddlPeriode.Items.Add(item)
            ddlPeriodeTo.Items.Add(item)
        Next

        '--DropDownList PeriodeDokumen
        Dim yearDiff As Integer
        yearDiff = DateTime.Now.Year - 2006
        For Each item As ListItem In LookUp.ArraylistYear(True, yearDiff, 0, DateTime.Now.Year)
            ddlPeriodeYear.Items.Add(item)
            ddlPeriodeYearTo.Items.Add(item)
        Next

    End Sub

    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.DokumenServiceView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Dokumen Service")
        End If
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDropdownList()
            RetrieveMaster()
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
            InitiatePage()
        Else
            SearchMontlyDocument()
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub BindDropdownList()
        ddlStatusDownload.DataSource = New EnumDNET().RetrieveStatusDaftarDokumenService
        ddlStatusDownload.DataTextField = "NameType"
        ddlStatusDownload.DataValueField = "ValType"
        ddlStatusDownload.DataBind()
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "PeriodeMonth"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub SearchMontlyDocument()
        dtgMonthlyDocument.CurrentPageIndex = 0
        BindToDataGrid(dtgMonthlyDocument.CurrentPageIndex)
    End Sub

    Private Sub BindToDataGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim dtDef As Date = "1753-01-01"
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "TransferDate", MatchType.Exact, dtDef))
        Dim strQueryDate As String = "(select id from MonthlyDocument (nolock) where TransferDate='" & dtDef & "' OR TransferDate IS NULL)"
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "id", MatchType.InSet, strQueryDate))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If ddlJenisDokumen.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.Exact, ddlJenisDokumen.SelectedValue))
        Else
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.InSet, "(1,2,4,5,6,7,23,24)"))
        End If


        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        Dim dNoAcc As String = CStr(txtNoAccounting.Text.Trim)
        If Not String.IsNullorEmpty(dNoAcc) Then
            Dim strQuery As String = "(select id from MonthlyDocument (nolock) where AccountingNo like '%" & txtNoAccounting.Text.Trim & "%')"
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "id", MatchType.InSet, strQuery))
        End If

        If Not String.IsNullorEmpty(txtBillingNo.Text) Then
            Dim strQuery As String = "(select id from MonthlyDocument (nolock) where BillingNo like '%" & txtBillingNo.Text.Trim & "%')"
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "id", MatchType.InSet, strQuery))
        End If

        'If Not String.IsNullOrEmpty(txtNomorFaktur.Text) Then
        '    Dim strQuery As String = "(select MonthlyDocumentID from MonthlyDocumentToFakturEvidance (nolock) where fakturnumber = '" & txtNomorFaktur.Text.Trim & "')"
        '    criterias.opAnd(New Criteria(GetType(MonthlyDocument), "id", MatchType.InSet, strQuery))
        'End If


        If txtKodeDealer.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "ProductCategory.ID", MatchType.Exact, "1"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "PeriodeMonth", MatchType.GreaterOrEqual, GetMonthValue(ddlPeriode.SelectedValue)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "PeriodeYear", MatchType.GreaterOrEqual, CType(ddlPeriodeYear.SelectedValue, Integer)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "PeriodeMonth", MatchType.LesserOrEqual, GetMonthValue(ddlPeriodeTo.SelectedValue)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "PeriodeYear", MatchType.LesserOrEqual, CType(ddlPeriodeYearTo.SelectedValue, Integer)))
        If ddlStatusDownload.SelectedValue = 1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "LastDownloadBy", MatchType.Exact, String.Empty))
        ElseIf ddlStatusDownload.SelectedValue = 2 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "LastDownloadBy", MatchType.No, String.Empty))
        End If

        Dim PCID As Integer = Me.GetProductCategoryID()
        If PCID > 0 Then
            Dim oPC As ProductCategory = New ProductCategoryFacade(User).Retrieve(PCID)
            Dim fileLike As String = "_" & oPC.Code.ToLower() & "."
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "ProductCategory.ID", MatchType.Exact, PCID))
        End If

        ArlMonthly = New MonthlyDocumentFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgMonthlyDocument.PageSize, _
              total, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dtgMonthlyDocument.VirtualItemCount = total
        dtgMonthlyDocument.DataSource = ArlMonthly
        sessionHelper.SetSession("MonthlyDocument", ArlMonthly)
        sessionHelper.SetSession("CRITERIASfrMonthly", criterias)
        dtgMonthlyDocument.DataBind()

        Dim strlabel As Label = CType(FindControl("lblAlert"), Label)
        strlabel.Text = String.Empty
        'strlabel.Visible = False
        If ddlStatusDownload.SelectedValue <> 2 Then
            If ArlMonthly.Count > 0 Then
                SetAlert(ArlMonthly)
            End If
        End If

    End Sub

    Private Sub SetAlert(ByVal arl As ArrayList)
        Dim strAlert As String = "Silahkan download dokumen service "
        Dim strKwitansiWaranty As String = String.Empty
        Dim strKwitansiFS As String = String.Empty
        Dim strFSLetter As String = String.Empty
        Dim iFirst As Boolean = True
        For Each doc As MonthlyDocument In arl
            If doc.LastDownloadBy = String.Empty Then
                Dim dtNow As Date = New Date(Date.Now.Year, Now.Month, Now.Day, 0, 0, 0)
                If doc.CreatedTime < CommonFunction.AddNWorkingDay(dtNow, 3, True) Then
                    Select Case doc.Kind
                        Case 1
                            If strKwitansiWaranty = String.Empty Then
                                strKwitansiWaranty = "Kwitansi Waranty"
                                If iFirst Then
                                    strAlert = strAlert & strKwitansiWaranty
                                    iFirst = False
                                Else
                                    strAlert = strAlert & ", " & strKwitansiWaranty
                                End If
                            End If
                        Case 2
                            If strKwitansiFS = String.Empty Then
                                strKwitansiFS = "Kwitansi FS"
                                If iFirst Then
                                    strAlert = strAlert & strKwitansiFS
                                    iFirst = False
                                Else
                                    strAlert = strAlert & ", " & strKwitansiFS
                                End If
                            End If
                        Case 4
                            If strFSLetter = String.Empty Then
                                strFSLetter = "FS Letter"
                                If iFirst Then
                                    strAlert = strAlert & strFSLetter
                                    iFirst = False
                                Else
                                    strAlert = strAlert & ", " & strFSLetter
                                End If
                            End If
                    End Select
                End If
            End If
        Next
        If strKwitansiWaranty <> String.Empty _
            Or strKwitansiFS <> String.Empty _
            Or strFSLetter <> String.Empty Then

            Dim strlabel As Label = CType(FindControl("lblAlert"), Label)
            strlabel.Text = strAlert
            'strlabel.Visible = True
        End If
    End Sub

    Private Function GetMonthValue(ByVal strMonth As String) As Integer
        Dim currentMonth As Integer = 0
        Dim arrMonth(12) As String
        arrMonth(0) = "Januari"
        arrMonth(1) = "Februari"
        arrMonth(2) = "Maret"
        arrMonth(3) = "April"
        arrMonth(4) = "Mei"
        arrMonth(5) = "Juni"
        arrMonth(6) = "Juli"
        arrMonth(7) = "Agustus"
        arrMonth(8) = "September"
        arrMonth(9) = "Oktober"
        arrMonth(10) = "November"
        arrMonth(11) = "Desember"

        For Each month As String In arrMonth
            currentMonth = currentMonth + 1
            If (month = strMonth) Then
                Exit For
            End If
        Next

        Return currentMonth
    End Function

    Private Function GetProductCategoryID() As Short
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
    End Function


    Private Sub dtgMonthlyDocument_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgMonthlyDocument.ItemDataBound
        If Not (ArlMonthly) Is Nothing Then
            If (ArlMonthly.Count > 0 And e.Item.ItemIndex <> -1) Then
                objMonthy = ArlMonthly(e.Item.ItemIndex)
                Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                lblID.Text = objMonthy.id

                Dim lblNo As Label = e.Item.FindControl("lblNo")
                lblNo.Text = e.Item.ItemIndex + (dtgMonthlyDocument.PageSize * dtgMonthlyDocument.CurrentPageIndex) + 1

                Dim lblDownload As Label = e.Item.FindControl("lblDownload")
                If objMonthy.LastDownloadDate.Year <> 1 And objMonthy.TransferDate.Year <> 1 Then
                    lblDownload.Text = "<img src=""../images/green.gif"" border=""0"" alt=""Sudah Didownload"">"
                End If

                e.Item.Cells(2).Text = CType(objMonthy.Kind, MonthlyDocumentType.MonthlyDocumentTypeEnum).ToString
                Dim datemonth As New DateTime(CInt(objMonthy.PeriodeYear), CInt(objMonthy.PeriodeMonth), 1)
                e.Item.Cells(3).Text = Format(datemonth, "MMM yyyy")
                e.Item.Cells(4).Text = objMonthy.Dealer.DealerCode & " - " & objMonthy.Dealer.SearchTerm1
                Dim str As String() = objMonthy.FileName.Split("\")
                e.Item.Cells(5).Text = str(str.Length - 1)
                e.Item.Cells(6).Text = FormatNumber(CType(objMonthy.FileSize, Double) / 1024, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                If objMonthy.LastDownloadBy Is Nothing OrElse objMonthy.LastDownloadBy = String.Empty Then
                    e.Item.Cells(8).Text = ""
                Else
                    e.Item.Cells(8).Text = UserInfo.Convert(objMonthy.LastDownloadBy)
                End If

                If objMonthy.LastDownloadDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                    e.Item.Cells(9).Text = objMonthy.LastDownloadDate
                Else
                    e.Item.Cells(9).Text = ""
                End If

                Dim objMonthEvi As New MonthlyDocumentToFakturEvidance
                If CType(sessionHelper.GetSession("IDUploadEvidance"), MonthlyDocumentToFakturEvidance) IsNot Nothing AndAlso _
                    CType(sessionHelper.GetSession("IDUploadEvidance"), MonthlyDocumentToFakturEvidance).MonthlyDocumentID = objMonthy.id Then
                    objMonthEvi = CType(sessionHelper.GetSession("IDUploadEvidance"), MonthlyDocumentToFakturEvidance)

                Else
                    Dim crit As New CriteriaComposite(New Criteria(GetType(MonthlyDocumentToFakturEvidance), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(MonthlyDocumentToFakturEvidance), "MonthlyDocumentID", MatchType.Exact, objMonthy.id))

                    Dim arra As ArrayList = (New MonthlyDocumentToFakturEvidanceFacade(User).Retrieve(crit))
                    If arra.Count > 0 Then
                        objMonthEvi = CType(arra.Item(0), MonthlyDocumentToFakturEvidance)
                    End If

                End If


                Dim trumekuhi() As Integer = {1, 2, 4, 5, 6, 7, 23, 24}

                Dim lastmekuhi As Boolean = False

                Dim x As Integer = 0
                For Each row As Integer In trumekuhi
                    If objMonthy.Kind = row Then
                        lastmekuhi = True
                        Exit For
                    End If
                Next

                If lastmekuhi = True Then
                    If (objMonthy.LastDownloadDate.Year = 1753 Or objMonthy.LastDownloadDate.Year = 1900) And
                    (objMonthy.TransferDate.Year = 1753 Or objMonthy.TransferDate.Year = 1900) Then
                        lblDownload.Text = "<img src=""../images/red.gif"" border=""0"" alt=""Dokumen belum didownload dan Tanggal Transfer belum ada"">"
                        e.Item.Cells(8).Text = ""
                    ElseIf (objMonthy.TransferDate.Year <> 1753 Or objMonthy.TransferDate.Year <> 1900) And
                        (objMonthy.LastDownloadDate.Year = 1753 Or objMonthy.LastDownloadDate.Year = 1900) Then
                        lblDownload.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Dokumen belum didownload"">"
                        e.Item.Cells(8).Text = ""
                    ElseIf (objMonthy.TransferDate.Year = 1753 Or objMonthy.TransferDate.Year = 1900) And
                        (objMonthy.LastDownloadDate.Year <> 1753 Or objMonthy.LastDownloadDate.Year <> 1900) Then
                        lblDownload.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Tanggal Transfer belum ada"">"

                    Else
                        ' ini dibuat algoritma seperti ini karena permintaan user berubah2
                        'If String.IsNullorEmpty(lblNomorFaktur.Text) Then
                        '    lblDownload.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Tanggal Transfer belum ada"">"
                        'Else
                        '    lblDownload.Text = "<img src=""../images/green.gif"" border=""0"" alt="""">"
                        'End If
                        lblDownload.Text = "<img src=""../images/green.gif"" border=""0"" alt="""">"
                    End If
                End If

                If lastmekuhi = False Then
                    If (objMonthy.LastDownloadDate.Year = 1753 Or objMonthy.LastDownloadDate.Year = 1900) Then
                        lblDownload.Text = "<img src=""../images/red.gif"" border=""0"" alt=""Dokumen belum didownload"">"
                        e.Item.Cells(8).Text = ""
                    Else
                        lblDownload.Text = "<img src=""../images/green.gif"" border=""0"" alt="""">"
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        If CType(ddlPeriodeYear.SelectedValue, Integer) = CType(ddlPeriodeYearTo.SelectedValue, Integer) Then
            If GetMonthValue(ddlPeriode.SelectedValue) > GetMonthValue(ddlPeriodeTo.SelectedValue) Then
                MessageBox.Show("Pencarian Bulan Awal tidak boleh lebih besar dari Bulan Akhir")
            End If
        ElseIf CType(ddlPeriodeYear.SelectedValue, Integer) > CType(ddlPeriodeYearTo.SelectedValue, Integer) Then
            MessageBox.Show("Pencarian Tahun Awal tidak boleh lebih besar dari Tahun Akhir")
        End If
        SearchMontlyDocument()
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim arrData As ArrayList
        Dim criterias As CriteriaComposite = sessionHelper.GetSession("CRITERIASfrMonthly")
        If dtgMonthlyDocument.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If
        arrData = New MonthlyDocumentFacade(User).Retrieve(criterias)
        If arrData.Count > 0 Then
            DoDownload(arrData)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "OutstandingMonthlyDoc_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Dim OMDocData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then
                Dim finfo As FileInfo = New FileInfo(OMDocData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(OMDocData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteOMDocData(sw, data)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteOMDocData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Monthly Document Outstanding")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Jenis Dokumen" & tab)
            itemLine.Append("Periode" & tab)
            itemLine.Append("Dealer" & tab)
            itemLine.Append("Ukuran File" & tab)
            itemLine.Append("Tanggal Dibuat" & tab)
            itemLine.Append("Didownload Oleh " & tab)
            itemLine.Append("Tanggal Download " & tab)
            itemLine.Append("Nomor Accounting " & tab)
            itemLine.Append("No Billing " & tab)
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As MonthlyDocument In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(CType(item.Kind, MonthlyDocumentType.MonthlyDocumentTypeEnum).ToString & tab)
                itemLine.Append(item.PeriodeMonth & " - " & item.PeriodeYear & tab)
                itemLine.Append(item.Dealer.DealerCode & " - " & item.Dealer.SearchTerm1 & tab)
                itemLine.Append(item.FileSize & tab)
                itemLine.Append(item.CreatedTime & tab)
                itemLine.Append(UserInfo.Convert(item.LastDownloadBy) & tab)
                itemLine.Append(item.LastDownloadDate & tab)
                itemLine.Append(item.AccountingNo & tab)
                itemLine.Append(item.BillingNo & tab)

                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub


    Private Sub dtgMonthlyDocument_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgMonthlyDocument.PageIndexChanged
        dtgMonthlyDocument.CurrentPageIndex = e.NewPageIndex
        BindToDataGrid(dtgMonthlyDocument.CurrentPageIndex)
    End Sub
End Class