#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports Excel
#End Region

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


Public Class frmMonthlyDocument
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblJenisDokumen As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlert As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents OpClient As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents ddlJenisDokumen As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPeriode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPeriodeYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPeriodeTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPeriodeYearTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNomorFaktur As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoAccounting As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBillingNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgMonthlyDocument As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtDownload As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStatusDownload As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatusDownload As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents SCDealer As System.Web.UI.WebControls.HiddenField
    Protected WithEvents JDoc As System.Web.UI.WebControls.HiddenField
    Protected WithEvents NoFaktur As System.Web.UI.WebControls.HiddenField
    Protected WithEvents AccountingNo As System.Web.UI.WebControls.HiddenField
    Protected WithEvents BillingNo As System.Web.UI.WebControls.HiddenField
    Protected WithEvents Month As System.Web.UI.WebControls.HiddenField
    Protected WithEvents Year As System.Web.UI.WebControls.HiddenField
    Protected WithEvents MonthTo As System.Web.UI.WebControls.HiddenField
    Protected WithEvents YearTo As System.Web.UI.WebControls.HiddenField
    Protected WithEvents PCategory As System.Web.UI.WebControls.HiddenField
    Protected WithEvents Downloads As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblLoading As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

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
            Dim arlListTemp As ArrayList = MonthlyDocumentType.RetrieveDocumentType()
            For Each item As MonthlyDocumentTypeListItem In arlListTemp
                If SecurityProvider.Authorize(Context.User, SR.DocServiceIndepB_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Deposit_B_Interest Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DocServiceKudepB_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Deposit_B_Kwitansi Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DocServicePMLett_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Periodical_Maintenance_Letter Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_wscsta_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Warranty_ESP_Status_List Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lwsc01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Warranty_ESP_Letter Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lfsc01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Service_Campaign_Letter Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lfs001_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Service_Regular_Letter Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lpdi01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.PDI_Letter Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_kfsc01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Service_Campaign Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_kwsc01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_ESP_Warranty Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_Depb01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Deposit_B_Report Then
                        arlList.Add(item)
                    End If
                End If
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_lpdi02_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kend_Belum_PDI_List Then
                        arlList.Add(item)
                    End If
                End If

                'farid additional 20190828 ---------------------------------------------------------------------------------
                If SecurityProvider.Authorize(Context.User, SR.Free_service_regular_status_list_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_service_regular_status_list Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.Free_service_campaign_status_list_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_service_campaign_status_list Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.Free_maintenance_status_list_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_maintenance_status_list Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.Free_labor_status_list_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_labor_status_list Then
                        arlList.Add(item)
                    End If
                End If
                'farid additional 20190828 ---------------------------------------------------------------------------------


                'Tambahan CR Standard
                If SecurityProvider.Authorize(Context.User, SR.DokumenService_fll01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_ESP_Labour_Letter Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.DokumenService_kfl01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_ESP_Free_Labour Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.dokumen_service_lfm01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Maintenance_Letter Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.dokumen_service_kfm01_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Maintenance Then
                        arlList.Add(item)
                    End If
                End If

                If SecurityProvider.Authorize(Context.User, SR.Kwitansi_Warranty_Spare_Part_Accessories_Privilege) Then
                    If item.ValStatus = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Warranty_Spare_Part_Accessories Then
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
        Next

        For Each item As ListItem In LookUp.ArraylistMonthCalendar()
            ddlPeriodeTo.Items.Add(item)
        Next
        '--DropDownList PeriodeDokumen
        Dim yearDiff As Integer
        yearDiff = DateTime.Now.Year - 2006
        For Each item As ListItem In LookUp.ArraylistYear(True, yearDiff, 0, DateTime.Now.Year)
            ddlPeriodeYear.Items.Add(item)
        Next
        For Each item As ListItem In LookUp.ArraylistYear(True, yearDiff, 0, DateTime.Now.Year)
            ddlPeriodeYearTo.Items.Add(item)
        Next

    End Sub

#End Region

#Region "Event Hendlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        Dim dtcriteria As String = CType(Request.QueryString("Criterias"), String)
        'Dim crit As CriteriaComposite = sessionHelper.GetSession("CRITERIASfrMonthly")
        If Not IsPostBack Then
            BindDropdownList()
            RetrieveMaster()
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
            InitiatePage()
            'GetInformation()
            If Not String.IsNullorEmpty(dtcriteria) Then
                LoadData()
                'SearchMontlyDocument()
            End If
        Else
            SearchMontlyDocument()
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Function GetProductCategoryID() As Short
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
    End Function

    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.DokumenServiceView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Dokumen Service")
        End If
    End Sub

    'Private Sub GetInformation()
    '    ArlSearchCriteria = sessionHelper.GetSession("SearchCriteria")
    '    If (Not ArlSearchCriteria Is Nothing) AndAlso ArlSearchCriteria.Count <> 0 Then
    '        ddlJenisDokumen.SelectedValue = ArlSearchCriteria(0)
    '        ddlPeriode.SelectedValue = ArlSearchCriteria(1)
    '        txtKodeDealer.Text = ArlSearchCriteria(2)
    '        dtgMonthlyDocument.CurrentPageIndex = 0
    '        BindToDataGrid(dtgMonthlyDocument.CurrentPageIndex)
    '    End If
    'End Sub

    'Private Sub SaveInformation()
    '    ArlSearchCriteria = New ArrayList
    '    ArlSearchCriteria.Add(ddlJenisDokumen.SelectedValue)
    '    ArlSearchCriteria.Add(ddlPeriode.SelectedValue)
    '    ArlSearchCriteria.Add(txtKodeDealer.Text)
    '    sessionHelper.SetSession("SearchCriteria", ArlSearchCriteria)
    'End Sub

    Private Sub BindDropdownList()
        ddlStatusDownload.DataSource = New EnumDNET().RetrieveStatusDaftarDokumenService
        ddlStatusDownload.DataTextField = "NameType"
        ddlStatusDownload.DataValueField = "ValType"
        ddlStatusDownload.DataBind()
    End Sub

    Private Sub BindToDataGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If ddlJenisDokumen.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.Exact, ddlJenisDokumen.SelectedValue))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If Not String.IsNullOrEmpty(txtNomorFaktur.Text) Then
            Dim strQuery As String = "(select MonthlyDocumentID from MonthlyDocumentToFakturEvidance (nolock) where fakturnumber like '%" & txtNomorFaktur.Text.Trim & "%')"
            criterias.opAnd(New Criteria(GetType(MonthlyDocument), "id", MatchType.InSet, strQuery))
        End If

        If Not String.IsNullorEmpty(txtNoAccounting.Text) Then
            Dim strQuery As String = "(select id from MonthlyDocument (nolock) where AccountingNo like '%" & txtNoAccounting.Text.Trim & "%')"
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "id", MatchType.InSet, strQuery))
        End If

        If Not String.IsNullorEmpty(txtBillingNo.Text) Then
            Dim strQuery As String = "(select id from MonthlyDocument (nolock) where BillingNo like '%" & txtBillingNo.Text.Trim & "%')"
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "id", MatchType.InSet, strQuery))
        End If


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
        'If Me.ddlCategory.SelectedIndex > 0 Then
        '    Dim oC As Category = New CategoryFacade(User).Retrieve(CType(Me.ddlCategory.SelectedValue, Integer))
        '    Dim fileLike As String = "_" & oC.ProductCategory.Code.ToLower() & "."
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "FileName", MatchType.Partial, fileLike))
        'End If
        Dim PCID As Integer = Me.GetProductCategoryID()
        If PCID > 0 Then
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.InSet, "(0,2,10)"), "(", True)
            '0:          DEPB01, 10:         INDEPB, 2:          KFSC01
            Dim oPC As ProductCategory = New ProductCategoryFacade(User).Retrieve(PCID)
            Dim fileLike As String = "_" & oPC.Code.ToLower() & "."
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "FileName", MatchType.Partial, fileLike), ")", False)
            ''LOC 2014-09-04
            '' by ali
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "FileName", MatchType.Partial, fileLike))
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

    Private downloadPrivilege As Boolean
    Private deletePrivilege As Boolean
    Sub dtgMonthlyDocument_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dtgMonthlyDocument.ItemDataBound
        If Not (ArlMonthly) Is Nothing Then
            If (ArlMonthly.Count > 0 And e.Item.ItemIndex <> -1) Then
                If e.Item.ItemIndex = 0 Then
                    downloadPrivilege = SecurityProvider.Authorize(Context.User, SR.DokumenServiceDownload_Privilege)
                    deletePrivilege = SecurityProvider.Authorize(Context.User, SR.ENHRevisedServiceDocument_Privilege)
                End If
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

                Dim lblNomorFaktur As Label = CType(e.Item.FindControl("lblNomorFaktur"), Label)
                Dim lblTglTanggalRencanaTransfer As Label = CType(e.Item.FindControl("lblTglTanggalRencanaTransfer"), Label)
                Dim lblTglUpload As Label = CType(e.Item.FindControl("lblTglUpload"), Label)
                If objMonthEvi.ID > 0 Then
                    lblTglUpload.Text = objMonthEvi.UploadDate.ToString("dd/MM/yyyy")
                    lblNomorFaktur.Text = objMonthEvi.FakturNumber
                    lblTglTanggalRencanaTransfer.Text = objMonthEvi.PlanningTransferDate.ToString("dd/MM/yyyy")
                End If

                Dim lblTglTransfer As Label = CType(e.Item.FindControl("lblTglTransfer"), Label)
                If objMonthy.ActualTransferDate = "1753-01-01" Then
                    lblTglTransfer.Text = ""
                Else
                    lblTglTransfer.Text = Format(objMonthy.ActualTransferDate, "dd/MM/yyyy")
                End If

                If objMonthy.TransferDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) And objMonthy.TransferDate.Year <> 1900 Then
                    e.Item.Cells(12).Text = objMonthy.TransferDate.ToString("dd/MM/yyyy")
                Else
                    e.Item.Cells(12).Text = ""
                End If

                Dim lbtnUpload As LinkButton = CType(e.Item.FindControl("lbtnUpload"), LinkButton)
                Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)

                Dim lbtnViewDetail As LinkButton = CType(e.Item.FindControl("lbtnViewDetail"), LinkButton)
                lbtnViewDetail.Visible = False

                lbtnView.Visible = False
                lbtnUpload.Visible = True
                'lbtnUpload.Attributes("OnClick") = "on_create_form_clicked(this);"
                'lbtnView.Attributes("OnClick") = "on_create_form_clicked_view(this);"
                Dim cri As New CriteriaComposite(New Criteria(GetType(MonthlyDocumentToFakturEvidance), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cri.opAnd(New Criteria(GetType(MonthlyDocumentToFakturEvidance), "MonthlyDocumentID", MatchType.Exact, objMonthy.id))
                Dim arr As ArrayList = New MonthlyDocumentToFakturEvidanceFacade(User).Retrieve(cri)

                If arr.Count > 0 Then
                    lbtnUpload.Visible = False
                    lbtnView.Visible = True
                    lbtnViewDetail.Visible = True
                End If

                Dim lbtnDownload As LinkButton = e.Item.FindControl("lbtnDownload")
                lbtnDownload.Text = "<img src=""../images/download.gif"" border=""0"" alt=" & e.Item.Cells(6).Text & ">"
                lbtnDownload.Attributes.Add("OnClick", "return confirm('Anda Yakin Mau Download?');")
                lbtnDownload.Visible = downloadPrivilege

                Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
                lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data Mau Dihapus?');")
                lbtnDelete.Visible = deletePrivilege


                Dim lbtnDeleteDetail As LinkButton = e.Item.FindControl("lbtnDeleteDetail")
                lbtnDeleteDetail.Attributes.Add("OnClick", "return confirm('Yakin Data Mau Dihapus?');")
                lbtnDeleteDetail.Visible = False
                objDealer = Session("DEALER")
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    Dim dtDocEvi As MonthlyDocumentToFakturEvidance = New MonthlyDocumentToFakturEvidanceFacade(User).RetrieveByMDId(objMonthy.id)
                    If dtDocEvi.ID > 0 Then
                        If objMonthy.TransferDate <> "1753-01-01" AndAlso dtDocEvi.FakturNumber <> "" Then
                            lbtnDeleteDetail.Visible = True
                        End If
                    End If
                End If

                Dim trumekuhi() As Integer = {1, 2, 4, 5, 6, 7, 12, 13, 14, 15, 23, 24}

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
                        e.Item.Cells(12).Text = ""
                    ElseIf (objMonthy.TransferDate.Year <> 1753 Or objMonthy.TransferDate.Year <> 1900) And
                        (objMonthy.LastDownloadDate.Year = 1753 Or objMonthy.LastDownloadDate.Year = 1900) Then
                        lblDownload.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Dokumen belum didownload"">"
                        e.Item.Cells(8).Text = ""
                    ElseIf (objMonthy.TransferDate.Year = 1753 Or objMonthy.TransferDate.Year = 1900) And
                        (objMonthy.LastDownloadDate.Year <> 1753 Or objMonthy.LastDownloadDate.Year <> 1900) Then
                        lblDownload.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Tanggal Transfer belum ada"">"
                        e.Item.Cells(12).Text = ""
                    Else
                        ' ini dibuat algoritma seperti ini karena permintaan user berubah2
                        If String.IsNullorEmpty(lblNomorFaktur.Text) Then
                            lblDownload.Text = "<img src=""../images/yellow.gif"" border=""0"" alt=""Tanggal Transfer belum ada"">"
                        Else
                            lblDownload.Text = "<img src=""../images/green.gif"" border=""0"" alt="""">"
                        End If
                        lblDownload.Text = "<img src=""../images/green.gif"" border=""0"" alt="""">"
                    End If
                End If

                If lastmekuhi = False Then
                    If (objMonthy.LastDownloadDate.Year = 1753 Or objMonthy.LastDownloadDate.Year = 1900) Then
                        lblDownload.Text = "<img src=""../images/red.gif"" border=""0"" alt=""Dokumen belum didownload"">"
                        e.Item.Cells(8).Text = ""
                        e.Item.Cells(12).Text = ""

                    Else
                        lblDownload.Text = "<img src=""../images/green.gif"" border=""0"" alt="""">"
                    End If
                End If

                'If (objMonthy.Kind = 1 And objMonthy.Kind = 2 And objMonthy.Kind = 4 And objMonthy.Kind = 5 And objMonthy.Kind = 6 And objMonthy.Kind = 7 And objMonthy.Kind = 12 And objMonthy.Kind = 13 And objMonthy.Kind = 14 And objMonthy.Kind = 15) And (objMonthy.LastDownloadDate.Year <> 1753 And objMonthy.TransferDate.Year <> 1753 And objMonthy.TransferDate.Year <> 1900) Then
                '    lblDownload.Text = "<img src=""../images/green.gif"" border=""0"" alt=""Tanggal Transfer Belum Ada"">"
                '    e.Item.Cells(9).Text = UserInfo.Convert(objMonthy.LastDownloadBy)
                'ElseIf objMonthy.LastDownloadDate.Year <> 1753 Then
                '    lblDownload.Text = "<img src=""../images/red.gif"" border=""0"" alt=""Belum Didownload"">"
                '    e.Item.Cells(9).Text = UserInfo.Convert(objMonthy.LastDownloadBy)
                'Else
                '    lblDownload.Text = "<img src=""../images/red.gif"" border=""0"" alt=""Sudah Didownload"">"
                'End If
            End If
        End If
    End Sub

    Sub dtgMonthlyDocument_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMonthlyDocument.ItemCommand
        Dim objMonthlyDocumentFacade As New MonthlyDocumentFacade(User)
        Dim criterias As CriteriaComposite = sessionHelper.GetSession("CRITERIASfrMonthly")
        If e.CommandName = "Download" Then
            'SaveInformation()
            Dim LblFullName As Label = e.Item.FindControl("lblFullName")
            Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAPFolder").ToString & "\" & LblFullName.Text)

            'Dim fileInfo1 As New fileInfo(Server.MapPath(""))
            Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))

            Dim destFilePath As String = fileInfo1.Directory.FullName & "\DataFile\" & LblFullName.Text
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            'Dim _webServer As String = "172.17.104.90"
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim success As Boolean = False
            success = imp.Start()
            If success Then
                If (fileInfo.Exists) Then
                    Try

                        Try
                            Dim destinationFileInfo As New FileInfo(destFilePath)
                            If Not destinationFileInfo.Directory.Exists Then
                                destinationFileInfo.Directory.Create()
                            End If
                            fileInfo.CopyTo(destFilePath, True)

                        Catch ex As Exception

                        End Try

                        Dim newFileInfo As FileInfo = New FileInfo(destFilePath)
                        Dim exist As Boolean = newFileInfo.Exists
                        imp.StopImpersonate()
                        imp = Nothing

                        ArlMonthly = sessionHelper.GetSession("MonthlyDocument")
                        objMonthy = ArlMonthly(e.Item.ItemIndex)
                        objMonthy.LastDownloadBy = User.Identity.Name
                        objMonthy.LastDownloadDate = DateTime.Now
                        objMonthlyDocumentFacade.Update(objMonthy)
                        txtDownload.Text = "DataFile\" & LblFullName.Text
                        ArlMonthly.RemoveAt(e.Item.ItemIndex)
                        ArlMonthly.Insert(e.Item.ItemIndex, objMonthy)


                        sessionHelper.SetSession("MonthlyDocument", ArlMonthly)
                        dtgMonthlyDocument.DataSource = ArlMonthly
                        dtgMonthlyDocument.DataBind()

                        Dim lblDownload As Label = e.Item.FindControl("lblDownload")
                        If objMonthy.LastDownloadDate.Year <> 1 And objMonthy.TransferDate.Year <> 1 Then
                            lblDownload.Text = "<img src=""../images/green.gif"" border=""0"" alt=""Sudah Didownload"">"
                            e.Item.Cells(9).Text = UserInfo.Convert(objMonthy.LastDownloadBy)
                        End If


                        'ViewState(V_Load) = 1

                        'Server.Transfer("../Download.aspx?file=" & txtDownload.Text)
                        ''Response.Redirect("../Download.aspx?file=" & txtDownload.Text)
                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail(LblFullName.Text))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                End If
            Else
                MessageBox.Show("Gagal download file.")
            End If
        ElseIf e.CommandName = "Delete" Then
            ArlMonthly = sessionHelper.GetSession("MonthlyDocument")
            objMonthy = ArlMonthly(e.Item.ItemIndex)
            Dim objFileInfo As New FileInfo(Server.MapPath("") & "\..\Datafile\" & objMonthy.FileName)
            If objFileInfo.Exists Then
                objFileInfo.Delete()
            End If
            objMonthlyDocumentFacade.DeleteFromDB(objMonthy)
            SearchMontlyDocument()
            'ElseIf e.CommandName = "Upload" Then
            '    sessionHelper.SetSession("IDMonth", CType(e.Item.Cells(0).Text, Integer))
        ElseIf e.CommandName = "DeleteDetail" Then
            ArlMonthly = sessionHelper.GetSession("MonthlyDocument")
            objMonthy = ArlMonthly(e.Item.ItemIndex)
            UpdateDocEvidence(objMonthy.id, objMonthy)
            sessionHelper.SetSession("IDUploadEvidance", Nothing)
            SearchMontlyDocument()
        ElseIf e.CommandName = "Upload" Then
            ArlMonthly = sessionHelper.GetSession("MonthlyDocument")
            objMonthy = ArlMonthly(e.Item.ItemIndex)
            Server.Transfer("FrmUploadFaktureEvidance.aspx?mode=input&IDmonth=" & objMonthy.id.ToString & "&DealerCode=" & objMonthy.Dealer.DealerCode & "&JDoc=" & ddlJenisDokumen.SelectedValue _
                            & "&NoFaktur=" & txtNomorFaktur.Text & "&Month=" & ddlPeriode.SelectedValue & "&Year=" & ddlPeriodeYear.SelectedValue & "&PCategory=" & ddlProductCategory.SelectedValue _
                            & "&Download=" & ddlStatusDownload.SelectedValue & "&SCDealer=" & txtKodeDealer.Text & "&MonthTo=" & ddlPeriodeTo.SelectedValue & "&YearTo=" & ddlPeriodeYearTo.SelectedValue _
                            & "&BillingNo=" & txtBillingNo.Text & "&AccountingNo=" & txtNoAccounting.Text, True)
        ElseIf e.CommandName = "View" Then
            ArlMonthly = sessionHelper.GetSession("MonthlyDocument")
            objMonthy = ArlMonthly(e.Item.ItemIndex)
            Server.Transfer("FrmUploadFaktureEvidance.aspx?mode=view&IDmonth=" & objMonthy.id.ToString & "&DealerCode=" & objMonthy.Dealer.DealerCode & "&JDoc=" & ddlJenisDokumen.SelectedValue _
                            & "&NoFaktur=" & txtNomorFaktur.Text & "&Month=" & ddlPeriode.SelectedValue & "&Year=" & ddlPeriodeYear.SelectedValue & "&PCategory=" & ddlProductCategory.SelectedValue _
                            & "&Download=" & ddlStatusDownload.SelectedValue & "&SCDealer=" & txtKodeDealer.Text & "&MonthTo=" & ddlPeriodeTo.SelectedValue & "&YearTo=" & ddlPeriodeYearTo.SelectedValue _
                            & "&BillingNo=" & txtBillingNo.Text & "&AccountingNo=" & txtNoAccounting.Text, True)
        ElseIf e.CommandName = "ViewDetail" Then
            ArlMonthly = sessionHelper.GetSession("MonthlyDocument")
            objMonthy = ArlMonthly(e.Item.ItemIndex)
            Server.Transfer("FrmMonthlyDocumentActualTransfer.aspx?mode=view&IDmonth=" + objMonthy.id.ToString, True)
        End If
    End Sub

    Private Sub UpdateDocEvidence(ByVal ID As Integer, ByVal objDocMonth As Object)
        Dim retun As Integer
         Dim objMonthDoc As New MonthlyDocument
        Dim objmonevi As New MonthlyDocumentToFakturEvidance
        Dim arra As MonthlyDocumentToFakturEvidance = New MonthlyDocumentToFakturEvidanceFacade(User).RetrieveByMDId(ID)
        If arra.ID > 0 Then
            Dim objDocEvidanceFacade As New MonthlyDocumentToFakturEvidanceFacade(User)
            objDocEvidanceFacade.DeleteFromDB(arra)

            Dim montDocFacade As New MonthlyDocumentFacade(User)
            objMonthDoc = objDocMonth
            objMonthDoc.AccountNumberBank = ""
            objMonthDoc.NameofBank = ""
            montDocFacade.Update(objMonthDoc)
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If CType(ddlPeriodeYear.SelectedValue, Integer) = CType(ddlPeriodeYearTo.SelectedValue, Integer) Then
            If GetMonthValue(ddlPeriode.SelectedValue) > GetMonthValue(ddlPeriodeTo.SelectedValue) Then
                MessageBox.Show("Pencarian Bulan Awal tidak boleh lebih besar dari Bulan Akhir")
            End If
        ElseIf CType(ddlPeriodeYear.SelectedValue, Integer) > CType(ddlPeriodeYearTo.SelectedValue, Integer) Then
            MessageBox.Show("Pencarian Tahun Awal tidak boleh lebih besar dari Tahun Akhir")
        End If
        SearchMontlyDocument()
    End Sub

    Private Sub SearchMontlyDocument()
        dtgMonthlyDocument.CurrentPageIndex = 0
        BindToDataGrid(dtgMonthlyDocument.CurrentPageIndex)
    End Sub

#End Region

    Private Sub dtgMonthlyDocument_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgMonthlyDocument.PageIndexChanged
        dtgMonthlyDocument.CurrentPageIndex = e.NewPageIndex
        BindToDataGrid(dtgMonthlyDocument.CurrentPageIndex)
        'BindGrid()
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "PeriodeMonth"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub dtgMonthlyDocument_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMonthlyDocument.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        dtgMonthlyDocument.SelectedIndex = -1
        dtgMonthlyDocument.CurrentPageIndex = 0
        BindToDataGrid(dtgMonthlyDocument.CurrentPageIndex)

    End Sub

    Private Sub LoadData()
        SCDealer.Value = CType(Request.QueryString("SCDealer"), String)
        JDoc.Value = CType(Request.QueryString("JDoc"), String)
        NoFaktur.Value = CType(Request.QueryString("NoFaktur"), String)
        AccountingNo.Value = CType(Request.QueryString("AccountingNo"), String)
        BillingNo.Value = CType(Request.QueryString("BillingNo"), String)
        Month.Value = CType(Request.QueryString("Month"), String)
        Year.Value = CType(Request.QueryString("Year"), String)
        MonthTo.Value = CType(Request.QueryString("MonthTo"), String)
        YearTo.Value = CType(Request.QueryString("YearTo"), String)
        PCategory.Value = CType(Request.QueryString("PCategory"), String)
        Downloads.Value = CType(Request.QueryString("Download"), String)

        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If SCDealer.Value <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.DealerCode", MatchType.InSet, SCDealer.Value))
                txtKodeDealer.Text = SCDealer.Value
            Else
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            End If

        End If

        If JDoc.Value <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.Exact, JDoc.Value))
            ddlJenisDokumen.SelectedValue = JDoc.Value
        End If


        If Not String.IsNullorEmpty(NoFaktur.Value) Then
            Dim strQuery As String = "(select MonthlyDocumentID from MonthlyDocumentToFakturEvidance (nolock) where fakturnumber = '" & NoFaktur.Value & "')"
            criterias.opAnd(New Criteria(GetType(MonthlyDocument), "id", MatchType.InSet, strQuery))
            txtNomorFaktur.Text = NoFaktur.Value
        End If

        If Not String.IsNullorEmpty(AccountingNo.Value) Then
            Dim strQuery As String = "(select id from MonthlyDocument (nolock) where AccountingNo = '" & AccountingNo.Value & "')"
            criterias.opAnd(New Criteria(GetType(MonthlyDocument), "id", MatchType.InSet, strQuery))
            txtNoAccounting.Text = AccountingNo.Value
        End If

        If Not String.IsNullorEmpty(BillingNo.Value) Then
            Dim strQuery As String = "(select id from MonthlyDocument (nolock) where BillingNo = '" & BillingNo.Value & "')"
            criterias.opAnd(New Criteria(GetType(MonthlyDocument), "id", MatchType.InSet, strQuery))
            txtBillingNo.Text = BillingNo.Value
        End If

        If Month.Value <> "" Then
            ddlPeriode.SelectedIndex = GetMonthValue(Month.Value) - 1
        End If

        If Year.Value <> "" Then
            ddlPeriodeYear.SelectedValue = CType(Year.Value, Integer)
        End If

        If MonthTo.Value <> "" Then
            ddlPeriodeTo.SelectedIndex = GetMonthValue(MonthTo.Value) - 1
        End If

        If YearTo.Value <> "" Then
            ddlPeriodeYearTo.SelectedValue = CType(YearTo.Value, Integer)
        End If

        If SCDealer.Value <> String.Empty Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.DealerCode", MatchType.InSet, "('" & SCDealer.Value.Replace(";", "','") & "')"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "ProductCategory.ID", MatchType.Exact, "1"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "PeriodeMonth", MatchType.GreaterOrEqual, GetMonthValue(Month.Value)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "PeriodeYear", MatchType.GreaterOrEqual, CType(Year.Value, Integer)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "PeriodeMonth", MatchType.LesserOrEqual, GetMonthValue(MonthTo.Value)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "PeriodeYear", MatchType.LesserOrEqual, CType(YearTo.Value, Integer)))

        If Downloads.Value = 1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "LastDownloadBy", MatchType.Exact, String.Empty))
            ddlStatusDownload.SelectedValue = Downloads.Value
        ElseIf Downloads.Value = 2 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "LastDownloadBy", MatchType.No, String.Empty))
            ddlStatusDownload.SelectedValue = Downloads.Value
        End If
        Dim PCID As Integer = PCategory.Value
        If PCID > 0 Then
            Dim oPC As ProductCategory = New ProductCategoryFacade(User).Retrieve(PCID)
            Dim fileLike As String = "_" & oPC.Code.ToLower() & "."
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "ProductCategory.ID", MatchType.Exact, PCID))
            ddlProductCategory.SelectedValue = PCID
        End If

        ArlMonthly = New MonthlyDocumentFacade(User).RetrieveActiveList(criterias, 1, dtgMonthlyDocument.PageSize, _
              total, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dtgMonthlyDocument.VirtualItemCount = total
        dtgMonthlyDocument.DataSource = ArlMonthly
        sessionHelper.SetSession("MonthlyDocument", ArlMonthly)
        sessionHelper.SetSession("CRITERIASfrMonthly", criterias)
        dtgMonthlyDocument.DataBind()

        Dim strlabel As Label = CType(FindControl("lblAlert"), Label)
        strlabel.Text = String.Empty
        If ddlStatusDownload.SelectedValue <> 2 Then
            If ArlMonthly.Count > 0 Then
                SetAlert(ArlMonthly)
            End If
        End If






        'Dim criterias As CriteriaComposite = sessionHelper.GetSession("CRITERIASfrMonthly")

        'Dim totalRow As Integer = 0
        ''-- Retrieve recordset
        'Dim arrMD As ArrayList = New MonthlyDocumentFacade(User).RetrieveActiveList(criterias, 1, dtgMonthlyDocument.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        'If arrMD.Count = 0 Then
        '    dtgMonthlyDocument.DataSource = New ArrayList
        'Else
        '    dtgMonthlyDocument.DataSource = arrMD
        'End If

        'dtgMonthlyDocument.VirtualItemCount = totalRow
        'dtgMonthlyDocument.DataBind()

    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        lblLoading.Text = "Proses download. Mohon tunggu sebentar..."
        btnDownload.Enabled = False
        SetDownload()
        lblLoading.Text = ""
        btnDownload.Enabled = True
    End Sub

    Private Sub SetDownload()
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
        sFileName = "BuktiFaktur_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Dim BuktiFakturData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then
                Dim finfo As FileInfo = New FileInfo(BuktiFakturData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(BuktiFakturData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteBuktiFaktur(sw, data)
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

    Private Sub WriteBuktiFaktur(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Bukti Faktur")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Dealer Code" & tab)
            itemLine.Append("No Accounting" & tab)
            itemLine.Append("No Billing" & tab)
            itemLine.Append("No Faktur Pajak" & tab)
            itemLine.Append("Deskripsi Pembayaran" & tab)
            itemLine.Append("Tanggal Rencana Transfer" & tab)
            itemLine.Append("Rekening Transfer" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As MonthlyDocument In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                If Not IsNothing(item.Dealer) Then
                    itemLine.Append(item.Dealer.DealerCode & tab)
                Else
                    itemLine.Append("" & tab)
                End If
                itemLine.Append("'" + item.AccountingNo & tab)
                itemLine.Append(item.BillingNo & tab)

                Dim objMonthEvi As New MonthlyDocumentToFakturEvidance
                Dim crit As New CriteriaComposite(New Criteria(GetType(MonthlyDocumentToFakturEvidance), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(MonthlyDocumentToFakturEvidance), "MonthlyDocumentID", MatchType.Exact, item.id))
                Dim arra As ArrayList = (New MonthlyDocumentToFakturEvidanceFacade(User).Retrieve(crit))
                If arra.Count > 0 Then
                    objMonthEvi = CType(arra.Item(0), MonthlyDocumentToFakturEvidance)

                    itemLine.Append(objMonthEvi.FakturNumber & tab)
                    itemLine.Append(objMonthEvi.PaymentDescription & tab)
                    itemLine.Append(objMonthEvi.PlanningTransferDate.ToString("dd/MM/yyyy") & tab)
                Else
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab)
                End If
                If item.AccountNumberBank <> "" Then
                    itemLine.Append("'" + item.AccountNumberBank & tab)
                Else
                    itemLine.Append("" & tab)
                End If


                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub

End Class
