Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessValidation.Helpers

Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports System.Linq
Imports System.Collections.Generic


Public Class FrmBenefitClaimList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgTable As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKodeName As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnTambah As System.Web.UI.WebControls.Button

    Protected WithEvents btnProses As System.Web.UI.WebControls.Button

    Protected WithEvents txtNoClaim As System.Web.UI.WebControls.TextBox
    Protected WithEvents icDateBayar As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icDateClaim As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icDateClaimTo As KTB.DNet.WebCC.IntiCalendar

    Protected WithEvents ddlStatusUpload As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatusTransfer As System.Web.UI.WebControls.DropDownList

    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList

    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList


    Protected WithEvents ddlPilihanClaim As System.Web.UI.WebControls.DropDownList

    Protected WithEvents ddlStatus2 As System.Web.UI.WebControls.DropDownList


    Protected WithEvents ddlLeasing As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAccuered As System.Web.UI.WebControls.DropDownList

    Protected WithEvents arrayCheck As System.Web.UI.WebControls.HiddenField

    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label

    Protected WithEvents btnSAP As System.Web.UI.WebControls.Button

    Protected WithEvents txtIdDetailMasterShow As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIdDetailMaster As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnRefClaim As System.Web.UI.WebControls.Button

    Protected WithEvents panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents panel2 As System.Web.UI.WebControls.Panel

    Protected WithEvents dgGridDetil As System.Web.UI.WebControls.DataGrid

    Protected WithEvents lblDelerSession As System.Web.UI.WebControls.Label

    Protected WithEvents cbDateClaim As System.Web.UI.WebControls.CheckBox

    Protected WithEvents pnlbtnsimpan As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlProses As System.Web.UI.WebControls.Panel


    Protected WithEvents txtNoRangka As System.Web.UI.WebControls.TextBox

    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button

    Protected WithEvents cbDateBayar As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icDateBayarFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icDateBayarTo As KTB.DNet.WebCC.IntiCalendar

    Protected WithEvents PanelJV As System.Web.UI.WebControls.Panel
    Protected WithEvents lblJvKet As System.Web.UI.WebControls.Label
    Protected WithEvents dgJV As System.Web.UI.WebControls.DataGrid

    Protected WithEvents btnAddJv As System.Web.UI.WebControls.Button

    Protected WithEvents btnFilterCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtNoRegBenefit As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoSurat As System.Web.UI.WebControls.TextBox


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private sessHelper As New SessionHelper
    Dim dataCount As Integer = 0
    'Private objAlertFacade As AlertMasterFacade = New AlertMasterFacade(User)
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

#Region "Private Property"
    Private Property SesType() As EnumAlertManagement.AlertManagementType
        Get
            Return CType(sessHelper.GetSession("ListAlertMasterType"), EnumAlertManagement.AlertManagementType)
        End Get
        Set(ByVal Value As EnumAlertManagement.AlertManagementType)
            sessHelper.SetSession("ListAlertMasterType", Value)
        End Set
    End Property

    Private Viewdaftarclaim_privillage As Boolean
    Private prosesdaftarclaimdealer_privillage As Boolean
    Private prosesdaftarclaimktb_privillage As Boolean
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        'If Not SecurityProvider.Authorize(Context.User, SR.AlertManagementListView_Privilege) Then
        'If Not SecurityProvider.Authorize(Context.User, SR.AlertManagementListView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=ALERT MANAGEMENT - Alert Managemen List")
        'End If


        Viewdaftarclaim_privillage = False
        prosesdaftarclaimdealer_privillage = False
        prosesdaftarclaimktb_privillage = False

        Viewdaftarclaim_privillage = SecurityProvider.Authorize(Context.User, SR.Viewdaftarclaim_privillage)
        prosesdaftarclaimdealer_privillage = SecurityProvider.Authorize(Context.User, SR.prosesdaftarclaimdealer_privillage)
        prosesdaftarclaimktb_privillage = SecurityProvider.Authorize(Context.User, SR.prosesdaftarclaimktb_privillage)

        If Not Viewdaftarclaim_privillage Then

            Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - Daftar Claim")

        End If

        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            If Not prosesdaftarclaimdealer_privillage Then
                btnProses.Visible = False
            End If
            btnSAP.Visible = False
        ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            If Not prosesdaftarclaimktb_privillage Then
                btnProses.Visible = False
                btnSAP.Visible = False
                'btnStatus.Visible = False
            End If
        End If

    End Sub

    'Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.AlertManagementListView_Privilege)
#End Region


    Enum TransferStatus
        Ok = 1
        Belum = 0
    End Enum

    Enum AccueredMonth
        Januari = 1
        Februari = 2
        Maret = 3
        April = 4
        Mei = 5
        Juni = 6
        Juli = 7
        Agustus = 8
        September = 9
        Oktober = 10
        November = 11
        Desember = 12
    End Enum

    Enum Status

        Validasi = 1
        BatalValidasi = 0
    End Enum

    Enum StatusKtb

        Konfirmasi = 2
        Tolak = 3
        Proses = 4
        Selesai = 5
        BatalValidasi = 0
    End Enum

    Enum StatusUpload

        Ok = 1
        DoubleClaim = 2
        BedaLeasing = 3


    End Enum

    Private Sub EnumToListBox(EnumType As Type, TheListBox As ListControl)
        Dim Values As Array = System.Enum.GetValues(EnumType)
        For Each Value As Integer In Values
            Dim Display As String = [Enum].GetName(EnumType, Value)
            Dim Item As ListItem = New ListItem(Display, Value.ToString())
            TheListBox.Items.Add(Item)
        Next
    End Sub




    Private Sub RetrieveDealer()
        Dim objSessionHelper As New SessionHelper
        Dim objDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        If Not objDealer Is Nothing Then

            If Not objDealer.DealerGroup Is Nothing Then 'untuk yg dealer
                lblDelerSession.Visible = True
                lblPopUpDealer.Visible = Not lblDelerSession.Visible
                txtKodeName.Attributes("style") = "display:none"
                lblDelerSession.Text = objDealer.DealerCode & " / " & objDealer.DealerName
                txtKodeName.Text = objDealer.DealerCode
                ' btnSAP.Visible = lblPopUpDealer.Visible

                btnSAP.Visible = False

                pnlbtnsimpan.Visible = False
                ' pnlProses.Visible = pnlbtnsimpan.Visible
                ' btnTambah.Visible = True
                'EnumToListBox(GetType(Status), ddlStatus2)
                'ddlStatus2.Items.Add(New ListItem("Baru", "0")) 'name,id
                ddlStatus2.Items.Add(New ListItem("Validasi", "1")) 'name,id
                ddlStatus2.Items.Add(New ListItem("Batal Validasi", "0")) 'name,id
            Else
                lblDelerSession.Visible = False
                lblPopUpDealer.Visible = Not lblDelerSession.Visible
                txtKodeName.Attributes("style") = "display:"
                'btnSAP.Visible = lblPopUpDealer.Visible

                ' btnSAP.Visible = False

                ' pnlbtnsimpan.Visible = True
                pnlbtnsimpan.Visible = False

                ddlStatus2.Items.Add(New ListItem("Konfirmasi", "2")) 'name,id
                ddlStatus2.Items.Add(New ListItem("Tolak", "3")) 'name,id
                ddlStatus2.Items.Add(New ListItem("Proses", "4")) 'name,id
                ddlStatus2.Items.Add(New ListItem("Selesai", "5")) 'name,id
                ' ddlStatus2.Items.Add(New ListItem("Batal Validasi", "0")) 'name,id
                ddlStatus2.Items.Add(New ListItem("Batal Konfirmasi", "1")) 'name,id
                If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
                    ddlStatus2.Items.Add(New ListItem("Batal Selesai", "9")) 'name,id
                End If

            End If

        Else

        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        InitiateAuthorization()
        If Not IsPostBack Then
            RetrieveDealer()

            RemoveALLSession()

            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            ddlPilihanClaim.Attributes("onchange") = "showhideLeasing();"
            InitPilihanClaim()
            InitPilihanLeasing()
            InitKategori()
            ViewState("currentSortColumn") = "Name"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            'ViewState("currentSortDirection") = Sort.SortDirection.DESC


            EnumToListBox(GetType(AccueredMonth), ddlAccuered)

            ' EnumToListBox(GetType(AccueredMonth), ddlMonthJV)

            ddlStatusTransfer.Items.Add(New ListItem("", ""))
            EnumToListBox(GetType(TransferStatus), ddlStatusTransfer)
            ddlStatusTransfer.SelectedValue = ""

            ddlStatusUpload.Items.Add(New ListItem("", ""))
            EnumToListBox(GetType(StatusUpload), ddlStatusUpload)
            ddlStatusUpload.SelectedValue = ""

            ddlStatus.Items.Add(New ListItem("", ""))
            'EnumToListBox(GetType(Status), ddlStatus)
            'EnumToListBox(GetType(StatusKtb), ddlStatus)
            InitPilihanStatus()
            ddlStatus.SelectedValue = ""

            dgTable.CurrentPageIndex = 0

            If Not Request.QueryString("SessionForDisplayDetail") Is Nothing Then
                If Not sessHelper.GetSession("SessionForDisplay") Is Nothing Then
                    Dim list As New ArrayList
                    list = CType(sessHelper.GetSession("SessionForDisplay"), ArrayList)
                    txtKodeName.Text = list.Item(0).ToString
                    ddlPilihanClaim.SelectedValue = list.Item(1).ToString.Split("~")(0)
                    ddlLeasing.SelectedValue = list.Item(1).ToString.Split("~")(1)
                    txtIdDetailMaster.Value = list.Item(2).ToString.Split("~")(0)
                    txtIdDetailMasterShow.Text = list.Item(2).ToString.Split("~")(1)
                    cbDateClaim.Checked = False
                    If list.Item(3).ToString.Contains("True") Then
                        cbDateClaim.Checked = True
                    End If
                    icDateClaim.Value = list.Item(4)
                    icDateClaimTo.Value = list.Item(5)

                    txtNoClaim.Text = list.Item(6).ToString
                    txtNoRangka.Text = list.Item(7).ToString
                    ddlStatusTransfer.SelectedValue = list.Item(8).ToString.Split("~")(0)
                    ddlStatusUpload.SelectedValue = list.Item(8).ToString.Split("~")(1)
                    ddlKategori.SelectedValue = list.Item(8).ToString.Split("~")(2)
                    ddlStatus.SelectedValue = list.Item(8).ToString.Split("~")(3)
                    cbDateBayar.Checked = False
                    If list.Item(9).ToString.Contains("True") Then
                        cbDateBayar.Checked = True
                    End If
                    icDateBayarFrom.Value = list.Item(10)
                    icDateBayarTo.Value = list.Item(11)
                End If
            End If

            If Not IsNothing(sessHelper.GetSession("SearchCriterias")) Then
                Dim _CriteriaComposite As CriteriaComposite = CType(sessHelper.GetSession("SearchCriterias"), CriteriaComposite)
                BindDataGrid(dgTable.CurrentPageIndex, Nothing, Sort.SortDirection.ASC, _CriteriaComposite)
            Else
                BindDataGrid(dgTable.CurrentPageIndex)
            End If

        End If
        panel1.Attributes("style") = "display:;"
        panel2.Attributes("style") = "display:none;"
    End Sub

    Private Sub InitPilihanStatus()

        ddlStatus.Items.Add(New ListItem("Baru", "0")) 'name,id
        ddlStatus.Items.Add(New ListItem("Validasi", "1")) 'name,id
        ddlStatus.Items.Add(New ListItem("Konfirmasi", "2")) 'name,id
        ddlStatus.Items.Add(New ListItem("Tolak", "3")) 'name,id
        ddlStatus.Items.Add(New ListItem("Proses", "4")) 'name,id
        ddlStatus.Items.Add(New ListItem("Selesai", "5")) 'name,id


        'Enum Status

        '       Validasi = 1
        '       BatalValidasi = 0
        '   End Enum

        '   Enum StatusKtb

        '       Konfirmasi = 2
        '       Tolak = 3
        '       Proses = 4
        '       Selesai = 5
        '       BatalValidasi = 0
        '   End Enum
    End Sub

    Private Sub InitPilihanClaim()
        Dim facade As New BenefitTypeFacade(User)
        Dim arlfacade As ArrayList = facade.RetrieveActiveList()

        ddlPilihanClaim.Items.Clear()
        ddlPilihanClaim.Items.Add(New ListItem("", ""))



        For Each cat As BenefitType In arlfacade
            ddlPilihanClaim.Items.Add(New ListItem(cat.Name, cat.ID.ToString & ";" & cat.LeasingBox))
        Next

    End Sub

    Private Sub InitKategori()
        Dim facade As New CategoryFacade(User)
        Dim arlfacade As ArrayList = facade.RetrieveActiveList()

        ddlKategori.Items.Clear()
        ddlKategori.Items.Add(New ListItem("", ""))

        For Each cat As Category In arlfacade
            ddlKategori.Items.Add(New ListItem(cat.CategoryCode & " - " & cat.Description, cat.ID.ToString))
        Next

    End Sub

    Private Sub InitPilihanLeasing()
        Dim facade As New LeasingCompanyFacade(User)
        Dim arlfacade As ArrayList = facade.RetrieveActiveList()

        ddlLeasing.Items.Clear()
        ddlLeasing.Items.Add(New ListItem("", ""))

        'For Each cat As AlertCategory In arlAlertCategory
        '    ddlAlertCategory.Items.Add(New ListItem(cat.Description, cat.ID))
        'Next

        For Each cat As LeasingCompany In arlfacade
            ddlLeasing.Items.Add(New ListItem(cat.LeasingName, cat.ID.ToString))
        Next
        ddlLeasing.Style.Add("display", "none")
        'ddlAlertCategory.SelectedIndex = 0
        'RebindModulDropdownList()
    End Sub

    Private Sub RebindModulDropdownList()
        'Dim categoryId As Integer = CInt(ddlAlertCategory.SelectedValue)
        'Dim arlModul As ArrayList = New AlertModulFacade(User).RetrieveActiveListByCategoryID(categoryId)

        'ddlAlertModul.Items.Clear()
        'ddlAlertModul.Items.Add(New ListItem("Semua", 0))
        'For Each modul As AlertModul In arlModul
        '    ddlAlertModul.Items.Add(New ListItem(modul.Description, modul.ID))
        'Next
    End Sub

    Private Sub BindDataGrid(ByVal index As Integer, Optional ByVal sortColoum As String = Nothing, Optional ByVal sortType As Sort.SortDirection = Sort.SortDirection.ASC, Optional ByVal criteriaComposite As CriteriaComposite = Nothing)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))


        If lblDelerSession.Visible = False Then
            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "Status", MatchType.No, 0))
        End If

        If Not ddlStatus.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If cbDateClaim.Checked = True Then
            If CInt(icDateClaim.Value.ToString("yyyy")) > 1900 And CInt(icDateClaimTo.Value.ToString("yyyy")) > 1900 Then
                Dim tgl As New DateTime(CInt(icDateClaim.Value.Year), CInt(icDateClaim.Value.Month), CInt(icDateClaim.Value.Day), 0, 0, 0)
                Dim tgl1 As New DateTime(CInt(icDateClaimTo.Value.Year), CInt(icDateClaimTo.Value.Month), CInt(icDateClaimTo.Value.Day), 0, 0, 0)
                criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ClaimDate", MatchType.GreaterOrEqual, Format(tgl, "yyyy-MM-dd HH:mm:ss")))
                criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ClaimDate", MatchType.LesserOrEqual, Format(tgl1, "yyyy-MM-dd HH:mm:ss")))
            End If
        End If

        If cbDateBayar.Checked = True Then
            If CInt(icDateBayarFrom.Value.ToString("yyyy")) > 1900 And CInt(icDateBayarTo.Value.ToString("yyyy")) > 1900 Then
                Dim tgl As New DateTime(CInt(icDateBayarFrom.Value.Year), CInt(icDateBayarFrom.Value.Month), CInt(icDateBayarFrom.Value.Day), 0, 0, 0)
                Dim tgl1 As New DateTime(CInt(icDateBayarTo.Value.Year), CInt(icDateBayarTo.Value.Month), CInt(icDateBayarTo.Value.Day), 0, 0, 0)

                Dim strSql As String = ""
                strSql += " select BenefitClaimHeaderID from BenefitClaimJV"
                strSql += " where PaymentDate between  '" & Format(tgl, "yyyy-MM-dd HH:mm:ss") & "' and '" & Format(tgl1, "yyyy-MM-dd HH:mm:ss") & "'"
                strSql += " and isnull(PaymentDate,'') <> '' "
                criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ID", MatchType.InSet, "(" & strSql & ")"))

                '  criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "PlanningPaymentDate", MatchType.GreaterOrEqual, Format(tgl, "yyyy-MM-dd HH:mm:ss")))
                ' criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "PlanningPaymentDate", MatchType.LesserOrEqual, Format(tgl1, "yyyy-MM-dd HH:mm:ss")))
            End If
        End If


        If txtKodeName.Text <> "" Or ddlPilihanClaim.SelectedValue <> "" Or Not ddlStatusUpload.SelectedValue = "" Then

            Dim valuedddlPilihanClaim() As String = ddlPilihanClaim.SelectedValue.Replace(" ", "").Split(";")


            Dim condition As String = ""
            If txtKodeName.Text <> "" Then
                condition += " and d.DealerCode = '" & txtKodeName.Text & "'"
                condition += " and a.dealerid in (select id from dealer where dealercode in ('" & txtKodeName.Text.Trim & "'))"
            End If
            If ddlPilihanClaim.SelectedValue <> "" Then
                condition += "  and b.BenefitTypeID = '" & valuedddlPilihanClaim(0) & "' "
                If valuedddlPilihanClaim(1) = "1" And ddlLeasing.SelectedValue <> "" Then
                    condition += "  and e.LeasingCompanyID in (" & ddlLeasing.SelectedValue & ") "
                    condition += "  and a.LeasingCompanyID in (" & ddlLeasing.SelectedValue & ") "
                End If
            End If

            If ddlStatusUpload.SelectedValue <> "" Then
                condition += "  and  cc.statusupload = " & ddlStatusUpload.SelectedValue
            End If

            Dim strSql As String = ""
            strSql += "  select distinct a.id from BenefitClaimHeader a inner join"
            strSql += "  BenefitClaimDetails cc on a.ID = cc.BenefitClaimHeaderID and cc.RowStatus = 0 left join BenefitMasterDetail b"
            strSql += " on cc.BenefitMasterDetailID = b.ID "
            strSql += " inner join BenefitMasterDealer c on b.BenefitMasterHeaderID = c.BenefitMasterHeaderID "
            strSql += " inner join Dealer d on c.DealerID = d.ID"
            strSql += "   left join BenefitMasterLeasing e on b.ID = e.BenefitMasterDetailID  "
            strSql += " where 1=1 " & condition


            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        If txtNoClaim.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ClaimRegNo", MatchType.[Partial], txtNoClaim.Text.Trim))
        End If

        If txtNoRangka.Text <> "" Or ddlKategori.SelectedValue <> "" Then
            Dim strSql As String = ""
            strSql += "  select BenefitClaimHeaderID from BenefitClaimDetails a "
            strSql += "  inner join ChassisMaster b on a.ChassisMasterID = b.ID "
            strSql += "  where 1=1  "
            If txtNoRangka.Text <> "" Then
                strSql += "  and  b.ChassisNumber like '%" & txtNoRangka.Text & "%'"
            End If
            If ddlKategori.SelectedValue <> "" Then
                strSql += "  and  b.categoryid = " & ddlKategori.SelectedValue
            End If
            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        If txtIdDetailMaster.Value <> "" Then
            Dim strSql As String = ""
            strSql += "  select BenefitClaimHeaderID from BenefitClaimDetails a"
            strSql += " inner join BenefitMasterDetail b on a.benefitmasterdetailid = b.id"
            strSql += " where 1=1 "
            If txtIdDetailMaster.Value <> "" Then
                strSql += "  and  BenefitMasterHeaderID in (" & txtIdDetailMaster.Value & ")"
            End If

            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ID", MatchType.InSet, "(" & strSql & ")"))

        End If


        If Not ddlStatusTransfer.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "IsTransfer", MatchType.Exact, CShort(ddlStatusTransfer.SelectedValue)))
        End If




        'If Not sortColoum = Nothing Then
        '    Dim kondisi As String = criterias.ToString
        '    kondisi = kondisi & " order by " & sortColoum & " " & sortType.ToString
        '    Dim criteriasNew As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    Dim strSql As String = ""
        '    strSql += "  select BenefitClaimHeader.id  from BenefitClaimHeader"
        '    strSql += " inner join BenefitType "
        '    strSql += " on BenefitClaimHeader.BenefitTypeID = BenefitType.ID"
        '    strSql += " inner join Dealer"
        '    strSql += " on BenefitClaimHeader.DealerID = Dealer.ID"
        '    strSql += " left join LeasingCompany"
        '    strSql += " on BenefitClaimHeader.LeasingCompanyID = LeasingCompany.id "

        '    strSql += kondisi
        '    criteriasNew.opAnd(New Criteria(GetType(BenefitClaimHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
        '    _arrList = New BenefitClaimHeaderFacade(User).RetrieveByCriteriaPage(criteriasNew, index + 1, dgTable.PageSize, totalRow, Nothing, Nothing)
        'Else
        '    _arrList = New BenefitClaimHeaderFacade(User).RetrieveByCriteriaPage(criterias, index + 1, dgTable.PageSize, totalRow, sortColoum, sortType)
        'End If

        If Not IsNothing(criteriaComposite) Then
            criterias = criteriaComposite
        End If
        _arrList = New BenefitClaimHeaderFacade(User).RetrieveByCriteriaPage(criterias, index + 1, dgTable.PageSize, totalRow, sortColoum, sortType)
        sessHelper.SetSession("SearchCriterias", criterias)


        sessHelper.SetSession("ListSession", _arrList)

        Dim arForDisplay As New ArrayList
        arForDisplay.Add(txtKodeName.Text)
        arForDisplay.Add(ddlPilihanClaim.SelectedValue & "~" & ddlLeasing.SelectedValue)
        arForDisplay.Add(txtIdDetailMaster.Value & "~" & txtIdDetailMasterShow.Text)
        arForDisplay.Add(cbDateClaim.Checked)
        arForDisplay.Add(icDateClaim.Value)
        arForDisplay.Add(icDateClaimTo.Value)

        arForDisplay.Add(txtNoClaim.Text)
        arForDisplay.Add(txtNoRangka.Text)
        arForDisplay.Add(ddlStatusTransfer.SelectedValue & "~" & ddlStatusUpload.SelectedValue & "~" & ddlKategori.SelectedValue & "~" & ddlStatus.SelectedValue)
        arForDisplay.Add(cbDateBayar.Checked)
        arForDisplay.Add(icDateBayarFrom.Value)
        arForDisplay.Add(icDateBayarTo.Value)
        sessHelper.SetSession("SessionForDisplay", arForDisplay)


        PanelJV.Attributes("style") = "display:none;"

        Dim critBCR As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        For Each i As BenefitClaimHeader In _arrList
            critBCR.opAnd(New Criteria(GetType(BenefitClaimReceipt), "BenefitClaimHeader.ID", MatchType.Exact, i.ID))
            Dim bcr As BenefitClaimReceipt = New BenefitClaimReceiptFacade(User).Retrieve(critBCR).Cast(Of BenefitClaimReceipt)().SingleOrDefault()
            i.HasReceipt = If(Not IsNothing(bcr), True, False)
            i.TotalNilaiClaim = If(Not IsNothing(bcr), bcr.ReceiptAmountDeducted, 0)
            i.TotalPPh = If(Not IsNothing(bcr), bcr.PPHTotal, 0)
            i.TotalPPn = If(Not IsNothing(bcr), bcr.VATTotal, 0)
        Next

        dgTable.DataSource = _arrList
        dgTable.VirtualItemCount = totalRow
        dgTable.DataBind()

        PanelJV.Attributes("style") = "display:none;"

        If _arrList.Count < 1 Then
            MessageBox.Show("Data tidak ditemukan.")
        End If

    End Sub

    Private Sub RemoveALLSession()

        sessHelper.RemoveSession("ListSession")

    End Sub
    Private Sub dgTable_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTable.ItemCommand
        Dim show As String = ""

        Select Case e.CommandName
            Case "View"
                Response.Redirect("~/Benefit/FrmInputClaim.aspx?Mode=View&id=" & CInt(e.CommandArgument))
            Case "Edit"

                Response.Redirect("~/Benefit/FrmInputClaim.aspx?Mode=Edit&id=" & CInt(e.CommandArgument))
            Case "Delete"
                Response.Redirect("~/Benefit/FrmInputClaim.aspx?Mode=Delete&id=" & CInt(e.CommandArgument))
            Case "Print"
                Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
                If lnkbtnEdit.Visible = False Then
                    Response.Redirect("~/Benefit/FrmReceipt.aspx?id=" & CInt(e.CommandArgument) & "&justview=1&redirectFrom=FrmBenefitClaimList.aspx?SessionForDisplayDetail=1")
                Else
                    Response.Redirect("~/Benefit/FrmReceipt.aspx?id=" & CInt(e.CommandArgument) & "&amount=")
                End If
            Case "addJv"
                JVCommand(e, CInt(e.CommandArgument))

        End Select
    End Sub



    Private Function getmonth(ByVal i As Integer) As String
        Select Case i
            Case 1
                Return "Januari"
            Case 2
                Return "Februari"
            Case 3
                Return "Maret"
            Case 4
                Return "April"
            Case 5
                Return "Mei"
            Case 6
                Return "Juni"
            Case 7
                Return "Juli"
            Case 8
                Return "Agustus"
            Case 9
                Return "September"
            Case 10
                Return "Oktober"
            Case 11
                Return "November"
            Case 12
                Return "Desember"
        End Select
        Return ""
    End Function

    Private Sub dgTable_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTable.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain As BenefitClaimHeader = CType(e.Item.DataItem, BenefitClaimHeader)
            'If e.Item.ItemType = ListItemType.Item Then
            If Not objDomain Is Nothing Then

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)).ToString

                Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
                Dim temp As String = ""
                'Dim index As Integer = 0
                'For Each cat As BenefitMasterDealer In objDomain.BenefitMasterDetail.BenefitMasterHeader.BenefitMasterDealers
                '    temp += cat.Dealer.DealerCode & ";"
                '    If index >= 3 Then
                '        temp += "..... "
                '        Exit For
                '    End If
                '    index += 1
                'Next
                'lblKodeDealer.Text = temp
                lblKodeDealer.Text = objDomain.Dealer.DealerCode

                Dim lblNoClaimReg As Label = CType(e.Item.FindControl("lblNoClaimReg"), Label)
                lblNoClaimReg.Text = objDomain.ClaimRegNo

                Dim lblMMKSINotes As Label = CType(e.Item.FindControl("lblMMKSINotes"), Label)
                lblMMKSINotes.Text = objDomain.MMKSINotes

                Dim lblPilihanClaim As Label = CType(e.Item.FindControl("lblPilihanClaim"), Label)
                lblPilihanClaim.Text = objDomain.BenefitType.Name

                Dim lblLeasing As Label = CType(e.Item.FindControl("lblLeasing"), Label)
                temp = ""
                'For Each cat As BenefitMasterLeasing In objDomain.BenefitMasterDetail.BenefitMasterLeasings
                '    temp += cat.LeasingCompany.LeasingName & ";"
                'Next
                'lblLeasing.Text = temp
                If objDomain.LeasingCompany Is Nothing Then
                    lblLeasing.Text = ""
                Else
                    lblLeasing.Text = objDomain.LeasingCompany.LeasingName
                End If

                Dim lblTglClaim As Label = CType(e.Item.FindControl("lblTglClaim"), Label)
                lblTglClaim.Text = objDomain.ClaimDate.ToString("dd/MM/yyyy")

                Dim strPaymentDate As String = String.Empty
                Dim strActualPaymentDate As String = String.Empty
                Dim lblTglBayar As Label = CType(e.Item.FindControl("lblTglBayar"), Label)
                Dim lblTglAktualBayar As Label = CType(e.Item.FindControl("lblTglAktualBayar"), Label)
                For Each objJV As BenefitClaimJV In objDomain.BenefitClaimJVs
                    Dim strTipeAccount As String = objJV.TipeAccount
                    If strTipeAccount.Trim = "D" Then
                        strPaymentDate = String.Empty
                        strActualPaymentDate = String.Empty
                        If objJV.PaymentDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                            strPaymentDate = objJV.PaymentDate.ToString("dd/MM/yyyy")
                        End If
                        If objJV.ActualPaymentDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                            strActualPaymentDate = objJV.ActualPaymentDate.ToString("dd/MM/yyyy")
                        End If

                        lblTglBayar.Text = strPaymentDate
                        lblTglAktualBayar.Text = strActualPaymentDate
                    End If
                Next

                'If Not objDomain.PlanningPaymentDate = Nothing And CInt(objDomain.PlanningPaymentDate.ToString("yyyy")) > 2000 Then
                '    lblTglBayar.Text = objDomain.PlanningPaymentDate.ToString("dd/MM/yyyy")
                'Else
                '    lblTglBayar.Text = ""
                'End If


                'Dim lblAccrued As Label = CType(e.Item.FindControl("lblAccrued"), Label)
                'lblAccrued.Text = getmonth(objDomain.AccruedMonth)

                Dim lblSalesRef As Label = CType(e.Item.FindControl("lblSalesRef"), Label)

                'lblSalesRef.Text = objDomain.BenefitClaimDetailss.BenefitMasterHeader.BenefitRegNo
                For Each cat As BenefitClaimDetails In objDomain.BenefitClaimDetailss
                    'total += objDomain.BenefitMasterDetail.Amount
                    lblSalesRef.Text = cat.BenefitMasterDetail.BenefitMasterHeader.NomorSurat
                Next

                Dim cbAllGrid As CheckBox = CType(e.Item.FindControl("cbAllGrid"), CheckBox)

                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                If objDomain.Status = 0 Then
                    lblStatus.Text = "Baru" '[Enum].GetName(GetType(Status), Status.BatalValidasi)

                ElseIf objDomain.Status = 1 Then
                    lblStatus.Text = [Enum].GetName(GetType(Status), Status.Validasi) ' Status.Validasi
                ElseIf objDomain.Status = 2 Then
                    If lblDelerSession.Visible = True Then
                        cbAllGrid.Visible = False
                    End If
                    lblStatus.Text = [Enum].GetName(GetType(StatusKtb), StatusKtb.Konfirmasi) 'StatusKtb.Konfirmasi '"Konfirmasi"
                ElseIf objDomain.Status = 3 Then
                    'If lblDelerSession.Visible = True Then
                    cbAllGrid.Visible = False
                    'End If
                    lblStatus.Text = [Enum].GetName(GetType(StatusKtb), StatusKtb.Tolak) 'StatusKtb.Tolak '"Tolak"
                ElseIf objDomain.Status = 4 Then
                    If lblDelerSession.Visible = True Then
                        cbAllGrid.Visible = False
                    End If
                    lblStatus.Text = [Enum].GetName(GetType(StatusKtb), StatusKtb.Proses) ' StatusKtb.SetujuSebagian '"Setuju Sebagian"
                Else
                    If lblDelerSession.Visible = True Then
                        cbAllGrid.Visible = False
                    Else
                        ' btnSAP.Visible = True
                    End If
                    lblStatus.Text = [Enum].GetName(GetType(StatusKtb), StatusKtb.Selesai) ' StatusKtb.Setuju '"Setuju"   

                End If
                'lblStatus.Text = objDoma

                Dim lblJv As Label = CType(e.Item.FindControl("lblJv"), Label)
                lblJv.Text = objDomain.JVNumber

                Dim cutOffDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                Dim fakturPajakDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                For Each el As BenefitClaimReceipt In objDomain.BenefitClaimReceipts
                    cutOffDate = el.ReceiptDate
                    fakturPajakDate = el.FakturPajakDate
                Next

                If cutOffDate.Year < 1900 Then
                    cutOffDate = objDomain.ClaimDate
                End If

                Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(fakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
                Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(fakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

                'tyo
                Dim total As Decimal = 0, temptotal As Decimal = 0
                For Each cat As BenefitClaimDetails In objDomain.BenefitClaimDetailss
                    total += cat.BenefitMasterDetail.Amount
                Next

                Dim lblTotalNilaiClaim As Label = CType(e.Item.FindControl("lblTotalNilaiClaim"), Label)
                lblTotalNilaiClaim.Text = total.ToString("#,##0.00")

                temptotal = total

                Dim nilaiPph As Decimal = 0
                Dim nilaiVAT As Decimal = 0
                Dim lblPph As Label = CType(e.Item.FindControl("lblPph"), Label)
                If Not IsNothing(total) AndAlso CInt(total) > 0 Then
                    If objDomain.HasReceipt Then
                        nilaiPph = objDomain.TotalPPh
                        nilaiVAT = objDomain.TotalPPn
                    Else
                        If objDomain.BenefitType.LeasingBox = 1 Then
                            'nilaiPph = Math.Round(((temptotal / (1 - 0.15)) - temptotal))
                            'nilaiVAT = Math.Round((0.1 * (nilaiPph + temptotal)))
                            'total = total + nilaiPph

                            nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.Total, pph, total:=total)

                            total = CalcHelper.DPPCalculation(pph, total)
                            nilaiVAT = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=total)
                        Else
                            'nilaiPph = Math.Round(((temptotal / (1 - 0.15)) - temptotal))
                            'nilaiPph = Math.Round(0.15 * temptotal)
                            'nilaiVAT = 0 'Math.Round(0.1 * temptotal)

                            nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=total)
                            nilaiVAT = 0
                        End If
                    End If
                End If
                lblPph.Text = nilaiPph.ToString("#,##0.00")
                total = Math.Round(total + nilaiVAT)
                If objDomain.HasReceipt Then
                    total = CType(objDomain.BenefitClaimReceipts(0), BenefitClaimReceipt).ReceiptAmountDeducted
                End If

                Dim lblVat As Label = CType(e.Item.FindControl("lblVat"), Label)
                lblVat.Text = nilaiVAT.ToString("#,##0.00")

                Dim lblTotalNilai As Label = CType(e.Item.FindControl("lblTotalNilai"), Label)
                lblTotalNilai.Text = total.ToString("#,##0.00")

                Dim lblKuitansi As Label = CType(e.Item.FindControl("lblKuitansi"), Label)
                If Not objDomain.BenefitClaimReceipts Is Nothing Then
                    For Each item As BenefitClaimReceipt In objDomain.BenefitClaimReceipts
                        lblKuitansi.Text = item.ReceiptNo
                    Next
                End If

                Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
                Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
                Dim lnkbtnPrint As LinkButton = CType(e.Item.FindControl("lnkbtnPrint"), LinkButton)
                lnkbtnPrint.Visible = False
                If pnlbtnsimpan.Visible = False Then
                    '    lnkbtnEdit.Visible = True
                Else
                    '    lnkbtnEdit.Visible = False
                End If

                Dim lnkJV As LinkButton = CType(e.Item.FindControl("lnkJV"), LinkButton)
                lnkJV.Visible = False
                If lblDelerSession.Visible = False Then 'ktb side

                    lnkbtnDelete.Visible = False
                    lnkbtnPrint.Visible = lnkbtnDelete.Visible
                    lnkbtnEdit.Visible = lnkbtnDelete.Visible
                    If objDomain.JVNumber = "" Then
                        'lnkJV.Visible = True
                        If objDomain.BenefitType.ReceiptBox = 1 Or objDomain.BenefitType.LeasingBox = 1 Then
                            If objDomain.Status = 2 Or objDomain.Status = 4 Or objDomain.Status = 5 Then
                                'lnkJV.Visible = True
                            End If

                        End If
                    End If
                Else
                    'If objDomain.Status = 0 Or objDomain.Status = 1 Then
                    '    lnkbtnEdit.Visible = False
                    'End If
                    If cbAllGrid.Visible = False Then
                        lnkbtnDelete.Visible = False
                        lnkbtnEdit.Visible = lnkbtnDelete.Visible
                    End If

                    If objDomain.Status = 1 Then
                        lnkbtnDelete.Visible = False
                        lnkbtnEdit.Visible = lnkbtnDelete.Visible
                    End If

                    If objDomain.BenefitType.ReceiptBox = 1 Then
                        lnkbtnPrint.Visible = True
                    End If

                    'If objDomain.Status = 1 Or objDomain.Status = 0 Then

                    '    If objDomain.BenefitType.ReceiptBox = 1 Then
                    '        lnkbtnPrint.Visible = True
                    '    End If

                    'End If
                End If



                ' lnkbtnDelete.Visible = lnkbtnEdit.Visible
                ' lnkbtnPrint.Visible = lnkbtnEdit.Visible

                If prosesdaftarclaimktb_privillage = False Then
                    lnkJV.Visible = False
                End If

                If prosesdaftarclaimdealer_privillage = False Then
                    lnkbtnDelete.Visible = False
                    lnkbtnEdit.Visible = False
                End If

                dataCount += 1
                Dim _arrList As ArrayList = sessHelper.GetSession("ListSession")
                If dataCount = _arrList.Count Then
                    Dim strJs As String = "generateCheckBoxClick();"
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
                    dataCount = 0
                End If
            End If
        End If
    End Sub



    Private Sub dgTable_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgTable.PageIndexChanged
        dgTable.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgTable.CurrentPageIndex)
    End Sub

    Private Sub dgTable_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgTable.SortCommand
        'If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
        '    Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

        '        Case Sort.SortDirection.ASC
        '            ViewState("currentSortDirection") = Sort.SortDirection.DESC

        '        Case Sort.SortDirection.DESC
        '            ViewState("currentSortDirection") = Sort.SortDirection.ASC
        '    End Select
        'Else
        '    ViewState("currentSortColumn") = e.SortExpression
        '    ViewState("currentSortDirection") = Sort.SortDirection.DESC
        'End If


        Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

            Case Sort.SortDirection.ASC
                ViewState("currentSortDirection") = Sort.SortDirection.DESC

            Case Sort.SortDirection.DESC
                ViewState("currentSortDirection") = Sort.SortDirection.ASC
        End Select

        ViewState("currentSortColumn") = e.SortExpression



        dgTable.SelectedIndex = -1
        dgTable.CurrentPageIndex = 0
        BindDataGrid(dgTable.CurrentPageIndex, e.SortExpression, ViewState("currentSortDirection"))
    End Sub


    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dgTable.CurrentPageIndex = 0
        BindDataGrid(dgTable.CurrentPageIndex)
    End Sub

    Private Sub btnTambah_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTambah.Click
        Response.Redirect("~/Benefit/FrmInputClaim.aspx?Mode=Tambah")
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim list As New ArrayList
        Dim listcheck As New ArrayList

        For Each item As String In arrayCheck.Value.Replace(" ", "").Split(";")
            If Not item Is Nothing And Not item = "" Then
                listcheck.Add(item)
            End If
        Next

        If listcheck.Count < 1 Then
            MessageBox.Show("Check list data minimal satu")
            Return
        End If

        If Not sessHelper.GetSession("ListSession") Is Nothing Then
            list = CType(sessHelper.GetSession("ListSession"), ArrayList)
        End If





        'Dim n As Integer = New BenefitClaimHeaderFacade(User).UpdateStatus1(list, listcheck, ddlAccuered.SelectedValue, _
        '                                                              icDateBayar.Value, ddlStatusTransfer.SelectedValue)
        Dim n As Integer = New BenefitClaimHeaderFacade(User).UpdateStatus1(list, listcheck, ddlAccuered.SelectedValue, _
                                                                    icDateBayar.Value, Nothing)
        If n = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
            BindDataGrid(dgTable.CurrentPageIndex)
        End If

    End Sub



    'Private Function WriteBabitData(ByRef sw As StreamWriter)
    '    Dim list As ArrayList
    '    If Not sessHelper.GetSession("ListSession") Is Nothing Then
    '        list = CType(sessHelper.GetSession("ListSession"), ArrayList)
    '    End If

    '    Dim lines As New System.Text.StringBuilder
    '    Dim line As New System.Text.StringBuilder
    '    Dim checkDetailStatus As Boolean = False
    '    Dim separator As String = ";"

    '    Dim SelectedItemCount As Integer = 0
    '    For i As Integer = 0 To dgTable.Items.Count - 1
    '        If CType(dgTable.Items(i).FindControl("cbAllGrid"), CheckBox).Checked Then
    '            SelectedItemCount += 1
    '        End If
    '    Next

    '    Dim listAktif As String = ""
    '    Dim n As Integer = -1
    '    For i As Integer = 0 To dgTable.Items.Count - 1
    '        line.Clear()
    '        checkDetailStatus = False
    '        Dim objDomain As BenefitClaimHeader = CType(list(i), BenefitClaimHeader)
    '        If CType(dgTable.Items(i).FindControl("cbAllGrid"), CheckBox).Checked Then

    '            If objDomain.Status = 4 And (objDomain.BenefitType.ReceiptBox = 1 _
    '                Or objDomain.BenefitType.LeasingBox = 1) Then

    '                Dim countBenefitClaimReceipts As Integer = objDomain.BenefitClaimReceipts.Count
    '                If countBenefitClaimReceipts > 0 Then
    '                    If Not CType(objDomain.BenefitClaimReceipts.Item(0), BenefitClaimReceipt).FakturPajakNo = "" Then


    '                        'Dim lblTotalNilaiClaim As Label = CType(e.Item.FindControl("lblTotalNilaiClaim"), Label)
    '                        Dim total As Decimal = 0
    '                        Dim model As String = ""
    '                        For Each cat As BenefitClaimDetails In objDomain.BenefitClaimDetailss
    '                            'total += objDomain.BenefitMasterDetail.Amount
    '                            total += cat.BenefitMasterDetail.Amount
    '                            model = cat.ChassisMaster.VechileColor.VechileType.VechileModel.Description
    '                        Next
    '                        Dim nilaiPph As Decimal = 0
    '                        Dim nilaiPpn As Decimal = 0
    '                        If objDomain.BenefitType.LeasingBox = 1 Then
    '                            nilaiPph = Math.Round(((total / (1 - 0.15)) - total))
    '                        Else
    '                            nilaiPph = Math.Round(((total / (1 - 0.02)) - total))
    '                        End If

    '                        nilaiPpn = Math.Round((0.1 * (nilaiPph + total)))

    '                        Dim index As Integer = 0

    '                        Dim dpp As Decimal = 0
    '                        For Each item As BenefitClaimJV In objDomain.BenefitClaimJVs
    '                            line.Clear()
    '                            If Not item Is Nothing Then
    '                                If index = 0 Then
    '                                    dpp = item.Amount
    '                                    line.Append("H")
    '                                    line.Append(separator)
    '                                    line.Append("K")
    '                                    line.Append(separator)
    '                                    line.Append(objDomain.ClaimRegNo)
    '                                    line.Append(separator)
    '                                    If item.TipeAccount = "D" Then
    '                                        line.Append(objDomain.Dealer.DealerCode)
    '                                    ElseIf item.TipeAccount = "V" Then
    '                                        line.Append(objDomain.LeasingCompany.ID)
    '                                    End If
    '                                    line.Append(separator)
    '                                    line.Append(objDomain.ClaimDate.ToString("yyyyMMdd"))
    '                                    line.Append(separator)

    '                                    line.Append(nilaiPpn.ToString("##0"))
    '                                    line.Append(separator)
    '                                    line.Append(nilaiPph.ToString("##0"))
    '                                    line.Append(separator)

    '                                    line.Append(item.PaymentDate.ToString("yyyyMMdd"))
    '                                    line.Append(separator)
    '                                    line.Append(CType(objDomain.BenefitClaimReceipts.Item(0), BenefitClaimReceipt).FakturPajakNo)
    '                                    line.Append(separator)

    '                                    If objDomain.BenefitType.LeasingBox = 1 Then
    '                                        If Not item.Month = Nothing Then
    '                                            line.Append("Komisi Penjualan " & model _
    '                                                    & " Periode " & getmonth(item.Month))
    '                                        Else
    '                                            line.Append("Komisi Penjualan " & model)
    '                                        End If
    '                                    Else
    '                                        If Not item.Month = Nothing Then
    '                                            line.Append("Komisi Penjualan " & model _
    '                                                    & " Periode " & getmonth(item.Month))
    '                                        Else
    '                                            line.Append("Komisi Penjualan " & model)
    '                                        End If
    '                                    End If



    '                                    line.Append(vbNewLine)
    '                                    lines.Append(line)
    '                                Else
    '                                    line.Append("D")
    '                                    line.Append(separator)
    '                                    If item.TipeAccount = "E" Then
    '                                        line.Append("Expense")
    '                                        line.Append(separator)
    '                                        line.Append(" ")
    '                                    ElseIf item.TipeAccount = "V" Then
    '                                        line.Append("Vendor")
    '                                        line.Append(separator)
    '                                        line.Append(item.VendorID)
    '                                    ElseIf item.TipeAccount = "O" Then
    '                                        line.Append("Oth")
    '                                        line.Append(separator)
    '                                        line.Append(" ")
    '                                    End If
    '                                    line.Append(separator)
    '                                    line.Append(model & " " & getmonth(item.Month))
    '                                    line.Append(separator)
    '                                    If item.TipeAccount = "E" Or item.TipeAccount = "V" Then
    '                                        line.Append(item.Amount.ToString("##0"))
    '                                    ElseIf item.TipeAccount = "O" Then
    '                                        If objDomain.BenefitClaimJVs.Count = 3 Then
    '                                            line.Append((dpp - CType(objDomain.BenefitClaimJVs.Item(2), BenefitClaimJV).Amount).ToString("#,##0.00"))
    '                                        Else
    '                                            line.Append(dpp.ToString("##0"))
    '                                        End If

    '                                    End If
    '                                    line.Append(separator)
    '                                    line.Append(item.PaymentDate.ToString("yyyyMMdd"))
    '                                    line.Append(separator)
    '                                    line.Append(item.BusinessArea)
    '                                    line.Append(separator)
    '                                    line.Append(item.CostCenter)
    '                                    line.Append(vbNewLine)
    '                                    lines.Append(line)
    '                                End If


    '                                index = index + 1
    '                            End If
    '                        Next
    '                        If index > 0 Then
    '                            n = New BenefitClaimHeaderFacade(User).UpdateStatusTransfer(objDomain)
    '                        End If
    '                    End If
    '                End If

    '            End If
    '        End If
    '    Next

    '    sw.WriteLine(lines.ToString())

    'End Function

    Private Function WriteBabitData(ByRef lines As System.Text.StringBuilder)
        Dim list As ArrayList
        If Not sessHelper.GetSession("ListSession") Is Nothing Then
            list = CType(sessHelper.GetSession("ListSession"), ArrayList)
        End If

        'Dim lines As New System.Text.StringBuilder
        Dim line As New System.Text.StringBuilder
        Dim checkDetailStatus As Boolean = False
        Dim separator As String = ";"

        Dim SelectedItemCount As Integer = 0
        For i As Integer = 0 To dgTable.Items.Count - 1
            If CType(dgTable.Items(i).FindControl("cbAllGrid"), CheckBox).Checked Then
                SelectedItemCount += 1
            End If
        Next

        Dim listAktif As String = ""
        Dim n As Integer = -1
        For i As Integer = 0 To dgTable.Items.Count - 1
            line.Clear()
            checkDetailStatus = False
            Dim objDomain As BenefitClaimHeader = CType(list(i), BenefitClaimHeader)

            Dim cutOffDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
            Dim fakturPajakDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
            For Each el As BenefitClaimReceipt In objDomain.BenefitClaimReceipts
                cutOffDate = el.ReceiptDate
                fakturPajakDate = el.FakturPajakDate
            Next

            If cutOffDate.Year < 1900 Then
                cutOffDate = objDomain.ClaimDate
            End If

            Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(fakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
            Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(fakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

            If CType(dgTable.Items(i).FindControl("cbAllGrid"), CheckBox).Checked Then

                If objDomain.Status = 4 And (objDomain.BenefitType.ReceiptBox = 1 _
                    Or objDomain.BenefitType.LeasingBox = 1) Then

                    Dim countBenefitClaimReceipts As Integer = objDomain.BenefitClaimReceipts.Count
                    If countBenefitClaimReceipts > 0 Then
                        If Not CType(objDomain.BenefitClaimReceipts.Item(0), BenefitClaimReceipt).FakturPajakNo = "" Then

                            Dim kodedealer As String = objDomain.Dealer.DealerCode

                            Dim total As Decimal = 0
                            Dim model As String = ""
                            For Each cat As BenefitClaimDetails In objDomain.BenefitClaimDetailss
                                total += cat.BenefitMasterDetail.Amount
                                model = cat.ChassisMaster.VechileColor.VechileType.VechileModel.Description
                            Next
                            Dim nilaiPph As Decimal = 0
                            Dim nilaiPpn As Decimal = 0
                            If objDomain.BenefitType.LeasingBox = 1 Then
                                'nilaiPph = Math.Round(((total / (1 - 0.15)) - total))
                                'nilaiPpn = Math.Round((0.1 * (nilaiPph + total)))

                                nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.Total, pph, total:=total)
                                nilaiPpn = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=CalcHelper.DPPCalculation(pph, total))
                            Else
                                'nilaiPph = Math.Round(((total / (1 - 0.15)) - total))
                                'nilaiPph = Math.Round(0.15 * total)
                                'nilaiPpn = 0 'Math.Round(0.1 * total)

                                nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=total)
                                nilaiPpn = 0
                            End If



                            Dim index As Integer = 0

                            Dim dpp As Decimal = 0
                            Dim textDetil As String = ""
                            ' For Each item As BenefitClaimJV In objDomain.BenefitClaimJVs
                            Dim objBenefitClaimJVFacade As ArrayList = New BenefitClaimJVFacade(User).RetrieveList(objDomain.ID)
                            For Each item As BenefitClaimJV In objBenefitClaimJVFacade
                                line.Clear()
                                If Not item Is Nothing Then
                                    If index = 0 Then
                                        dpp = item.Amount
                                        line.Append("H")
                                        line.Append(separator)
                                        'line.Append("K")
                                        If item.TipeAccount = "D" Then
                                            line.Append("D")
                                            ' ElseIf item.TipeAccount = "V" Then
                                        Else
                                            line.Append("K")
                                        End If
                                        line.Append(separator)
                                        line.Append(objDomain.ClaimRegNo)
                                        line.Append(separator)
                                        If item.TipeAccount = "D" Then
                                            line.Append(objDomain.Dealer.DealerCode)
                                        ElseIf item.TipeAccount = "V" Then
                                            'line.Append(objDomain.LeasingCompany.ID)
                                            line.Append(objDomain.LeasingCompany.VendorID)
                                        End If
                                        line.Append(separator)
                                        Dim ObjReceipt As BenefitClaimReceipt = New BenefitClaimReceipt
                                        If objDomain.BenefitClaimReceipts.Count > 0 Then
                                            ObjReceipt = CType(objDomain.BenefitClaimReceipts(0), BenefitClaimReceipt)
                                        End If
                                        line.Append(ObjReceipt.ReceiptDate.ToString("yyyyMMdd"))
                                        ' line.Append(objDomain.ClaimDate.ToString("yyyyMMdd"))
                                        line.Append(separator)

                                        line.Append(nilaiPpn.ToString("##0"))
                                        line.Append(separator)
                                        line.Append(nilaiPph.ToString("##0"))
                                        line.Append(separator)

                                        line.Append(item.PaymentDate.ToString("yyyyMMdd"))
                                        line.Append(separator)
                                        line.Append(CType(objDomain.BenefitClaimReceipts.Item(0), BenefitClaimReceipt).FakturPajakNo)
                                        line.Append(separator)

                                        If objDomain.BenefitType.LeasingBox = 1 Then
                                            If Not item.Month = Nothing Then
                                                line.Append("Insentif Pembayaran " & model & " " & objDomain.BenefitClaimDetailss.Count().ToString() & " Unit" _
                                                        & " Periode " & getmonth(item.Month))
                                                textDetil = "Insentif Pembayaran"
                                            Else
                                                line.Append("Insentif Pembayaran " & model & " " & objDomain.BenefitClaimDetailss.Count().ToString() & " Unit")
                                                textDetil = "Insentif Pembayaran"
                                            End If
                                        Else
                                            If Not item.Month = Nothing Then
                                                line.Append("Komisi Penjualan " & model & " " & objDomain.BenefitClaimDetailss.Count().ToString() & " Unit" _
                                                        & " Periode " & getmonth(item.Month))
                                                textDetil = "Komisi Penjualan"
                                            Else
                                                line.Append("Komisi Penjualan " & model & " " & objDomain.BenefitClaimDetailss.Count().ToString() & " Unit")
                                                textDetil = "Komisi Penjualan"
                                            End If
                                        End If
                                        line.Append(separator)
                                        line.Append(CType(objDomain.BenefitClaimReceipts.Item(0), BenefitClaimReceipt).FakturPajakDate.ToString("yyyyMMdd"))


                                        line.Append(vbNewLine)
                                        lines.Append(line)
                                    Else
                                        line.Append("D")
                                        line.Append(separator)
                                        If item.TipeAccount = "E" Then
                                            line.Append("EXP")
                                            'line.Append(separator)
                                            ' line.Append("")
                                        ElseIf item.TipeAccount = "V" Then
                                            'line.Append("Vendor")
                                            'line.Append(separator)
                                            line.Append(item.VendorID)
                                        ElseIf item.TipeAccount = "O" Then
                                            line.Append("OTH")
                                            ' line.Append(separator)
                                            '  line.Append("")
                                        Else
                                            line.Append(kodedealer)
                                        End If
                                        line.Append(separator)
                                        line.Append(model & " " & getmonth(item.Month))
                                        ' line.Append(model)
                                        line.Append(separator)
                                        line.Append(item.Amount.ToString("##0"))
                                        'If item.TipeAccount = "E" Or item.TipeAccount = "V" Then
                                        '    line.Append(item.Amount.ToString("##0"))
                                        'ElseIf item.TipeAccount = "O" Then
                                        '    If objDomain.BenefitClaimJVs.Count = 3 Then
                                        '        line.Append((dpp - CType(objDomain.BenefitClaimJVs.Item(2), BenefitClaimJV).Amount).ToString("#,##0.00"))
                                        '    Else
                                        '        line.Append(dpp.ToString("##0"))
                                        '    End If

                                        'End If
                                        line.Append(separator)
                                        line.Append(item.PaymentDate.ToString("yyyyMMdd"))
                                        line.Append(separator)
                                        line.Append(item.BusinessArea)
                                        line.Append(separator)
                                        line.Append(item.CostCenter)
                                        line.Append(separator)
                                        line.Append(textDetil)
                                        line.Append(vbNewLine)
                                        lines.Append(line)
                                    End If


                                    index = index + 1
                                End If
                            Next
                            If index > 0 Then
                                n = New BenefitClaimHeaderFacade(User).UpdateStatusTransfer(objDomain)
                            End If
                        End If
                    End If

                End If
            End If
        Next

        ' sw.WriteLine(lines.ToString())

    End Function

    Private Sub DoDownload1()
        Dim lines As New System.Text.StringBuilder
        WriteBabitData(lines)

        If Not lines.ToString = "" Then



            ' Dim datetimenow As String = Now.ToString("yyyy_MM_dd_H_mm_ss")
            Dim datetimenow As String = Now.ToString("yyyyMMddHmmss")

            'Dim ClaimDataPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\campaign\" & datetimenow & "_Claim.txt"
            Dim ClaimDataPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\campaign\JVCMPGN" & datetimenow & ".txt"

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            'Dim _webServer As String = "172.17.104.204"
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            imp.Start()
            Try
                Dim dirInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(Path.GetDirectoryName(ClaimDataPath))

                If Not dirInfo.Exists Then
                    dirInfo.Create()
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(ClaimDataPath, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                sw.WriteLine(lines.ToString)
                ' WriteBabitData(sw)

                '-- Close stream & file
                sw.Close()
                fs.Close()

                '   imp.StopImpersonate()
                '  imp = Nothing
                MessageBox.Show("Data berhasil diupload ke SAP")
                PanelJV.Attributes("style") = "display:none;"

                ' End If

                '-- Download color data to client!
                'Response.Redirect("../downloadlocal.aspx?file=DataTemp\BabitPaymentData" & sSuffix & ".txt")
                imp.StopImpersonate()
                imp = Nothing
            Catch ex As Exception
                Dim errMess As String = ex.Message
            End Try
        Else
            MessageBox.Show("Tidak ada data diupload ke SAP. Cek faktur pajak, status proses dan merupakan cashback atau leasing")
            PanelJV.Attributes("style") = "display:none;"
        End If
    End Sub

    Private Sub btnSAP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSAP.Click
        Dim list As ArrayList
        If Not sessHelper.GetSession("ListSession") Is Nothing Then
            list = CType(sessHelper.GetSession("ListSession"), ArrayList)
        End If
        Dim checkChecked As Boolean = False
        Dim checkProses As Boolean = False
        For i As Integer = 0 To dgTable.Items.Count - 1
            Dim objDomain As BenefitClaimHeader = CType(list(i), BenefitClaimHeader)
            If CType(dgTable.Items(i).FindControl("cbAllGrid"), CheckBox).Checked Then
                checkChecked = True

                If objDomain.Status = 4 And (objDomain.BenefitType.ReceiptBox = 1 Or objDomain.BenefitType.LeasingBox = 1) Then
                    checkProses = True
                End If

            End If
        Next

        If checkChecked = False Then
            MessageBox.Show("Check list data minimal satu")
            Return
        End If

        If checkProses = False Then
            MessageBox.Show("Check list data yang berstatus Proses, cashbask atau leasing minimal satu")
            Return
        End If

        DoDownload1()
    End Sub

    Private Sub btnProses_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProses.Click
        Dim checkChecked As Boolean = False
        For i As Integer = 0 To dgTable.Items.Count - 1
            If CType(dgTable.Items(i).FindControl("cbAllGrid"), CheckBox).Checked Then
                checkChecked = True
            End If
        Next

        If checkChecked = False Then
            MessageBox.Show("Check list data minimal satu")
            Return
        End If

        Dim list As ArrayList
        Dim listcheck As New ArrayList

        For Each item As String In arrayCheck.Value.Replace(" ", "").Split(";")
            If Not item Is Nothing And Not item = "" Then
                listcheck.Add(item)
            End If
        Next

        If Not sessHelper.GetSession("ListSession") Is Nothing Then
            list = CType(sessHelper.GetSession("ListSession"), ArrayList)
        End If

        Dim arrBenefitClaimDeducted As New ArrayList, arrBenefitClaimDeductedHistory As New ArrayList
        If CShort(ddlStatus2.SelectedValue) = 3 Then   '--Status Tolak
            For i As Integer = 0 To dgTable.Items.Count - 1
                Dim objDomain As BenefitClaimHeader = CType(list(i), BenefitClaimHeader)
                If CType(dgTable.Items(i).FindControl("cbAllGrid"), CheckBox).Checked Then
                    GetDataBenefitClaimDeducted(objDomain, arrBenefitClaimDeducted, arrBenefitClaimDeductedHistory)
                End If
            Next
        End If

        Dim errExist As Integer = 0
        Dim n As Integer = New BenefitClaimHeaderFacade(User).UpdateStatus3(list, listcheck, ddlStatus2.SelectedValue, errExist, arrBenefitClaimDeducted, arrBenefitClaimDeductedHistory)

        If n <> -1 Then
            Dim msg As String = SR.SaveSuccess
            If (errExist = 1) Then
                If ddlStatus2.SelectedValue = 9 Then
                    msg = "'Batal selesai' hanya bisa diubah untuk status claim 'selesai'"
                End If
            End If
            MessageBox.Show(msg)
            BindDataGrid(dgTable.CurrentPageIndex)
        Else
            MessageBox.Show(SR.SaveFail)
        End If


    End Sub

    Private Sub GetDataBenefitClaimDeducted(ByVal objHeader As BenefitClaimHeader, ByRef arrBenefitClaimDeducted As ArrayList, ByRef arrBCDHAll As ArrayList)
        Dim tempAmount As Decimal = 0, tempamount1 As Decimal = 0

        Dim cutOffDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Dim fakturPajakDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        For Each el As BenefitClaimReceipt In objHeader.BenefitClaimReceipts
            cutOffDate = el.ReceiptDate
            fakturPajakDate = el.FakturPajakDate
        Next

        If cutOffDate.Year < 1900 Then
            cutOffDate = objHeader.ClaimDate
        End If

        Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(fakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(fakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        For Each el As BenefitClaimDetails In objHeader.BenefitClaimDetailss
            tempAmount = tempAmount + el.BenefitMasterDetail.Amount
        Next
        tempamount1 = tempAmount
        Dim total As Decimal = tempamount1
        If Not tempAmount = 0 Then
            Dim nilaiPph As Decimal = 0
            If objHeader.BenefitType.LeasingBox = 1 Then
                'nilaiPph = Math.Round(((tempamount1 / (1 - 0.14999999999999999)) - tempamount1))
                'total = total + nilaiPph
                'total = Math.Round(total + (0.1 * (nilaiPph + tempamount1)))

                nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.Total, pph, total:=total)
                total += CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=CalcHelper.DPPCalculation(pph, total))

            Else
                'nilaiPph = Math.Round(0.15 * tempamount1)
                'total = total + nilaiPph
                'total = Math.Round(total + (0.1 * tempamount1))

                nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=total)
                total += CalcHelper.PPNCalculation(CalcSourceTypeEnum.Total, ppn, total:=total)
            End If
        End If
        Dim dblAmountBCHeader As Double = total

        'Sampe sini ya

        Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)
        Dim objAppConfig As AppConfig = objAppConfigFacade.Retrieve("SalesCampaignDeductedBenefitType")
        If Not IsNothing(objAppConfig) Then
            Dim strVal() As String = objAppConfig.Value.ToString().Split(";")
            For Each strCode As String In strVal
                If strCode = objHeader.BenefitType.ID Then
                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDeductedHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(BenefitClaimDeductedHistory), "BenefitClaimHeader.ID", MatchType.Exact, objHeader.ID))
                    Dim arrBCDH As ArrayList = New BenefitClaimDeductedHistoryFacade(User).Retrieve(crit)
                    For Each objBCDH As BenefitClaimDeductedHistory In arrBCDH
                        Dim objBenefitClaimDeductedFacade As New BenefitClaimDeductedFacade(User)
                        Dim objDeduct As BenefitClaimDeducted = objBenefitClaimDeductedFacade.Retrieve(objBCDH.BenefitClaimDeducted.ID)
                        If Not IsNothing(objDeduct) Then
                            objDeduct.RemainAmount += IsDBNull(objBCDH.AmountDeducted, 0)
                            arrBenefitClaimDeducted.Add(objDeduct)
                        End If

                        objBCDH.RowStatus = -1
                        arrBCDHAll.Add(objBCDH)
                    Next

                    Exit For
                End If
            Next
        End If

    End Sub

    Private Sub dgGridDetil_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgGridDetil.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As BenefitMasterHeader = CType(e.Item.DataItem, BenefitMasterHeader)
            ' If e.Item.ItemType = ListItemType.Item Then

            If Not objDomain2 Is Nothing Then
                If Not e.Item.ItemType = ListItemType.EditItem Then
                    Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=checkbox name='rb' id='cbxDetail'>")
                    e.Item.Cells(0).Controls.Add(rdbChoice)

                    Dim lblIDoGridDetil As Label = CType(e.Item.FindControl("lblIDoGridDetil"), Label)
                    lblIDoGridDetil.Text = objDomain2.ID.ToString

                    Dim lblformula As Label = CType(e.Item.FindControl("lblformula"), Label)
                    lblformula.Text = objDomain2.Formula

                    Dim lblNoGridDetil As Label = CType(e.Item.FindControl("lblNoGridDetil"), Label)
                    lblNoGridDetil.Text = (e.Item.ItemIndex + 1 + (dgGridDetil.CurrentPageIndex * dgGridDetil.PageSize)).ToString

                    Dim lblnnosuratGridDetil As Label = CType(e.Item.FindControl("lblnnosuratGridDetil"), Label)
                    lblnnosuratGridDetil.Text = objDomain2.NomorSurat

                    Dim lblNoRegBenefitGridDetil As Label = CType(e.Item.FindControl("lblNoRegBenefitGridDetil"), Label)
                    lblNoRegBenefitGridDetil.Text = objDomain2.BenefitRegNo

                    Dim lbldeskripsiGridDetil As Label = CType(e.Item.FindControl("lbldeskripsiGridDetil"), Label)
                    lbldeskripsiGridDetil.Text = objDomain2.Remarks

                End If
            End If
        End If
    End Sub

    Private Sub btnRefClaim_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefClaim.Click


        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0


        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "RowStatus", MatchType.InSet, "(0,-1)"))




        Dim strSql As String = ""
        strSql += "  select distinct bmh.id from BenefitMasterHeader bmh"
        strSql += "  inner join BenefitMasterDetail bmd on bmh.ID = bmd.BenefitMasterHeaderID "
        strSql += "  left join BenefitMasterDealer bmde on bmh.ID = bmde.BenefitMasterHeaderID "
        strSql += "  left join BenefitMasterLeasing bml on bmd.ID = bml.BenefitMasterDetailID "
        strSql += "  left join Dealer c on bmde.DealerID = c.ID and c.RowStatus = 0 "
        strSql += "  where bmh.status = 0 and bmde.RowStatus=0  "

        Dim value As String = ""
        If txtKodeName.Text <> "" Then
            value = ""
            For Each item As String In txtKodeName.Text.Replace(" ", "").Split(";")
                If Not item Is Nothing And Not item = "" Then
                    value = value + "'" + item + "',"
                End If
            Next

            value = value + "'--'"
            strSql += " and c.DealerCode in (" & value & ")"

        End If

        If ddlPilihanClaim.SelectedValue <> "" Then
            Dim valuedddlPilihanClaim() As String = ddlPilihanClaim.SelectedValue.Replace(" ", "").Split(";")

            strSql += " and bmd.BenefitTypeID = " & valuedddlPilihanClaim(0)
            If valuedddlPilihanClaim(1) = "1" And ddlLeasing.SelectedValue <> "" Then
                strSql += " and bml.LeasingCompanyID = " & ddlLeasing.SelectedValue
            End If
        End If



        criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "ID", MatchType.InSet, "(" & strSql & ")"))

        _arrList = New BenefitMasterHeaderFacade(User).RetrieveByCriteria(criterias, dgGridDetil.CurrentPageIndex + 1, dgGridDetil.PageSize, totalRow)

        dgGridDetil.VirtualItemCount = totalRow
        sessHelper.SetSession("MasterDetailSession", _arrList)

        dgGridDetil.DataSource = _arrList
        dgGridDetil.DataBind()


        'If cbRefClaim.Checked = True Then
        panel2.Attributes("style") = "display:;"
        panel1.Attributes("style") = "display:none;"

    End Sub

    'Add by Deni Firdaus 29 Agustus 2017, Service Request : Filter Data 
    Private Sub FilterReffClaimBy(ByVal benefitNo As String, ByVal suratNo As String)

        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "RowStatus", MatchType.InSet, "(0,-1)"))

        Dim strSql As String = ""
        strSql += "  select distinct bmh.id from BenefitMasterHeader bmh"
        strSql += "  inner join BenefitMasterDetail bmd on bmh.ID = bmd.BenefitMasterHeaderID "
        strSql += "  left join BenefitMasterDealer bmde on bmh.ID = bmde.BenefitMasterHeaderID "
        strSql += "  left join BenefitMasterLeasing bml on bmd.ID = bml.BenefitMasterDetailID "
        strSql += "  left join Dealer c on bmde.DealerID = c.ID and c.RowStatus = 0 "
        strSql += "  where bmh.status = 0 and bmde.RowStatus=0 And "
        strSql += String.Format("bmh.BenefitRegNo like '%{0}%' and bmh.NomorSurat like '%{1}%'", benefitNo, suratNo)

        Dim value As String = ""
        If txtKodeName.Text <> "" Then
            value = ""
            For Each item As String In txtKodeName.Text.Replace(" ", "").Split(";")
                If Not item Is Nothing And Not item = "" Then
                    value = value + "'" + item + "',"
                End If
            Next

            value = value + "'--'"
            strSql += " and c.DealerCode in (" & value & ")"

        End If

        If ddlPilihanClaim.SelectedValue <> "" Then
            Dim valuedddlPilihanClaim() As String = ddlPilihanClaim.SelectedValue.Replace(" ", "").Split(";")

            strSql += " and bmd.BenefitTypeID = " & valuedddlPilihanClaim(0)
            If valuedddlPilihanClaim(1) = "1" And ddlLeasing.SelectedValue <> "" Then
                strSql += " and bml.LeasingCompanyID = " & ddlLeasing.SelectedValue
            End If
        End If

        criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "ID", MatchType.InSet, "(" & strSql & ")"))

        _arrList = New BenefitMasterHeaderFacade(User).RetrieveByCriteria(criterias, dgGridDetil.CurrentPageIndex + 1, dgGridDetil.PageSize, totalRow)

        dgGridDetil.VirtualItemCount = totalRow
        sessHelper.SetSession("MasterDetailSession", _arrList)

        dgGridDetil.DataSource = _arrList
        dgGridDetil.DataBind()

        panel2.Attributes("style") = "display:;"
        panel1.Attributes("style") = "display:none;"


    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "BenefitList_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

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

            Dim fs As FileStream = New FileStream(fileTemp, FileMode.CreateNew)
            Dim sw As StreamWriter = New StreamWriter(fs)

            WriteDepositAData(sw, data)



            sw.Close()
            fs.Close()
            imp.StopImpersonate()
            imp = Nothing

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteDepositAData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder
        Dim itemLineDetil As StringBuilder = New StringBuilder
        Dim itemLineDetilHeader As StringBuilder = New StringBuilder
        If Not IsNothing(data) Then
            itemLineDetilHeader.Remove(0, itemLine.Length)
            '   itemLineDetilHeader.Append(" " & tab)
            '  itemLineDetilHeader.Append(" " & tab)
            '  itemLineDetilHeader.Append("No" & tab)
            itemLineDetilHeader.Append("Nomor Rangka" & tab)
            itemLineDetilHeader.Append("Deskripsi Kendaraan" & tab)
            itemLineDetilHeader.Append("Nomor Mesin" & tab)
            itemLineDetilHeader.Append("Tanggal Faktur Open" & tab)
            itemLineDetilHeader.Append("Tanggal Validasi Faktur" & tab)
            itemLineDetilHeader.Append("Nama Customer" & tab)
            itemLineDetilHeader.Append("Alamat" & tab)
            itemLineDetilHeader.Append("Nilai Claim Nett" & tab)
            itemLineDetilHeader.Append("Max Claim" & tab)
            itemLineDetilHeader.Append("Durasi Claim" & tab)
            itemLineDetilHeader.Append("Status" & tab)
            itemLineDetilHeader.Append("Ket Dealer" & tab)
            itemLineDetilHeader.Append("Ket MMKSI" & tab)
            itemLineDetilHeader.Append("Leasing Company" & tab)
            itemLineDetilHeader.Append("No Surat Rekomendasi" & tab)




            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("BENEFIT LIST")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            '  itemLine.Append("No" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("No Claim Reg" & tab)
            itemLine.Append("Pilihan Claim" & tab)
            itemLine.Append("Leasing" & tab)
            itemLine.Append("Tanggal Claim" & tab)
            'itemLine.Append("Tanggal Pembayaran" & tab)
            'itemLine.Append("Accured Amount" & tab)
            itemLine.Append("No Surat" & tab)
            itemLine.Append("Status Claim" & tab)
            itemLine.Append("Nomor JV" & tab)
            itemLine.Append("Total Nilai Kuitansi" & tab)
            itemLine.Append("Total Nilai Claim Nett" & tab)
            itemLine.Append("Total Pph" & tab)
            itemLine.Append("Total Ppn" & tab)
            itemLine.Append("No Kuitansi" & tab)

            sw.WriteLine(itemLine.ToString() & itemLineDetilHeader.ToString)

            Dim i As Integer = 1
            Dim DealerCode As String = ""
            Dim j As Integer = 1

            Dim index As Integer = 0
            For Each item As BenefitClaimHeader In data

                '   If CType(dgTable.Items(index).FindControl("cbAllGrid"), CheckBox).Checked Then
                '     checkChecked = True


                itemLine.Remove(0, itemLine.Length)
                '  itemLine.Append(i & tab)
                itemLine.Append(item.Dealer.DealerCode & tab)
                itemLine.Append(item.Dealer.DealerName & tab)
                itemLine.Append(item.ClaimRegNo & tab)
                itemLine.Append(item.BenefitType.Name & tab)
                If item.LeasingCompany Is Nothing Then
                    itemLine.Append(" " & tab)
                Else
                    itemLine.Append(item.LeasingCompany.LeasingName & tab)
                End If

                If CInt(item.ClaimDate.ToString("yyyy")) > 1900 Then
                    itemLine.Append(item.ClaimDate.ToString("dd/MM/yyyy") & tab)
                Else
                    itemLine.Append(" " & tab)
                End If

                'If CInt(item.PlanningPaymentDate.ToString("yyyy")) > 1900 Then
                '    itemLine.Append(item.PlanningPaymentDate.ToString("dd/MM/yyyy") & tab)
                'Else
                '    itemLine.Append(" " & tab)
                'End If

                'itemLine.Append(getmonth(item.AccruedMonth) & tab)
                Dim nosurat As String = ""
                'Dim total As Decimal = 0, temptotal As Decimal = 0

                For Each cat As BenefitClaimDetails In item.BenefitClaimDetailss
                    'total += objDomain.BenefitMasterDetail.Amount
                    nosurat = cat.BenefitMasterDetail.BenefitMasterHeader.NomorSurat
                    'total += cat.BenefitMasterDetail.Amount
                Next
                'temptotal = total

                itemLine.Append(nosurat & tab)

                If item.Status = 0 Then
                    itemLine.Append("Baru" & tab)
                ElseIf item.Status = 1 Then
                    itemLine.Append([Enum].GetName(GetType(Status), Status.Validasi) & tab)
                ElseIf item.Status = 2 Then
                    itemLine.Append([Enum].GetName(GetType(StatusKtb), StatusKtb.Konfirmasi) & tab)
                ElseIf item.Status = 3 Then
                    itemLine.Append([Enum].GetName(GetType(StatusKtb), StatusKtb.Tolak) & tab)
                ElseIf item.Status = 4 Then
                    itemLine.Append([Enum].GetName(GetType(StatusKtb), StatusKtb.Proses) & tab)
                Else
                    itemLine.Append([Enum].GetName(GetType(StatusKtb), StatusKtb.Selesai) & tab)
                End If

                Dim nilaiPph As Decimal = 0
                Dim nilaiPpn As Decimal = 0
                Dim total As Decimal = 0, temptotal As Decimal = 0

                Dim cutOffDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                Dim fakturPajakDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                For Each el As BenefitClaimReceipt In item.BenefitClaimReceipts
                    cutOffDate = el.ReceiptDate
                    fakturPajakDate = el.FakturPajakDate
                Next

                If cutOffDate.Year < 1900 Then
                    cutOffDate = item.ClaimDate
                End If

                Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(fakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
                Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(fakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

                For Each cat As BenefitClaimDetails In item.BenefitClaimDetailss
                    total += cat.BenefitMasterDetail.Amount
                Next
                temptotal = total

                If Not IsNothing(temptotal) AndAlso CInt(temptotal) > 0 Then
                    If Not IsNothing(item.BenefitClaimReceipts) AndAlso item.BenefitClaimReceipts.Count > 0 Then
                        nilaiPph = CType(item.BenefitClaimReceipts(0), BenefitClaimReceipt).PPHTotal
                        nilaiPpn = CType(item.BenefitClaimReceipts(0), BenefitClaimReceipt).VATTotal
                    Else
                        If item.BenefitType.LeasingBox = 1 Then
                            'nilaiPph = Math.Round(((temptotal / (1 - 0.15)) - temptotal))
                            'nilaiPpn = Math.Round((0.1 * (nilaiPph + temptotal)))
                            'total = total + nilaiPph
                            'total = Math.Round(total + nilaiPpn)

                            nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.Total, pph, total:=total)

                            total = CalcHelper.DPPCalculation(pph, total)
                            nilaiPpn = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=total)
                            total += nilaiPpn
                        Else
                            'nilaiPph = Math.Round(0.15 * temptotal)
                            'nilaiPpn = 0 'Math.Round(0.1 * temptotal)
                            'total = Math.Round(total + nilaiPpn)

                            nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=total)
                            nilaiPpn = 0
                            total += nilaiPpn
                        End If
                    End If
                End If

                If Not IsNothing(item.BenefitClaimReceipts) AndAlso item.BenefitClaimReceipts.Count > 0 Then
                    total = CType(item.BenefitClaimReceipts(0), BenefitClaimReceipt).ReceiptAmountDeducted
                End If

                itemLine.Append(item.JVNumber & tab)
                itemLine.Append(total.ToString("#,##0.00") & tab)
                itemLine.Append(temptotal.ToString("#,##0.00") & tab)
                itemLine.Append(nilaiPph.ToString("#,##0.00") & tab)
                itemLine.Append(nilaiPpn.ToString("#,##0.00") & tab)

                Dim lblKuitansi As String = ""
                If Not item.BenefitClaimReceipts Is Nothing Then
                    For Each item1 As BenefitClaimReceipt In item.BenefitClaimReceipts
                        lblKuitansi = item1.ReceiptNo
                    Next
                End If

                itemLine.Append(lblKuitansi & tab)


                itemLineDetil.Clear()
                j = 1
                For Each cat As BenefitClaimDetails In item.BenefitClaimDetailss
                    'total += objDomain.BenefitMasterDetail.Amount
                    nosurat = cat.BenefitMasterDetail.BenefitMasterHeader.NomorSurat
                    total += cat.BenefitMasterDetail.Amount

                    itemLineDetil.Remove(0, itemLineDetil.Length)
                    ' itemLineDetil.Append(" " & tab)
                    'itemLineDetil.Append(" " & tab)
                    ' itemLineDetil.Append(j & tab)
                    itemLineDetil.Append(cat.ChassisMaster.ChassisNumber & tab)
                    itemLineDetil.Append(cat.ChassisMaster.VechileColor.MaterialDescription & tab)
                    itemLineDetil.Append(cat.ChassisMaster.EngineNumber & tab)
                    If Not cat.ChassisMaster.EndCustomer Is Nothing Then
                        If CInt(cat.ChassisMaster.EndCustomer.OpenFakturDate.ToString("yyyy")) > 1990 Then
                            itemLineDetil.Append(cat.ChassisMaster.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy") & tab)
                        Else
                            itemLineDetil.Append(" " & tab)
                        End If
                        If CInt(cat.ChassisMaster.EndCustomer.ValidateTime.ToString("yyyy")) > 1990 Then
                            itemLineDetil.Append(cat.ChassisMaster.EndCustomer.ValidateTime.ToString("dd/MM/yyyy") & tab)
                        Else
                            itemLineDetil.Append(" " & tab)
                        End If
                    Else
                        itemLineDetil.Append(" " & tab)
                        itemLineDetil.Append(" " & tab)
                    End If
                    itemLineDetil.Append(cat.ChassisMaster.EndCustNameText & tab)
                    itemLineDetil.Append(cat.ChassisMaster.AddressText & tab)

                    itemLineDetil.Append(cat.BenefitMasterDetail.Amount.ToString("#,##0.00") & tab)

                    itemLineDetil.Append(cat.BenefitMasterDetail.MaxClaim.ToString & tab)
                    If Not cat.ChassisMaster.EndCustomer Is Nothing Then
                        itemLineDetil.Append(DateTime.Now.Subtract(cat.ChassisMaster.EndCustomer.ValidateTime).Days.ToString & tab)
                    Else
                        itemLineDetil.Append(" " & tab)
                    End If

                    If cat.DetailStatus = 1 Then
                        itemLineDetil.Append("OK" & tab)
                    ElseIf cat.DetailStatus = 2 Then
                        itemLineDetil.Append("Tidak" & tab)
                    Else
                        itemLineDetil.Append(" " & tab)
                    End If

                    itemLineDetil.Append(cat.DescDealer & tab)
                    itemLineDetil.Append(cat.DescKtb & tab)

                    If cat.LeasingCompany Is Nothing Then
                        itemLineDetil.Append(" " & tab)
                    Else
                        itemLineDetil.Append(cat.LeasingCompany.LeasingName & tab)
                    End If

                    itemLineDetil.Append(cat.RecLetterRegNo & tab)

                    j = j + 1


                    sw.WriteLine(itemLine.ToString() & itemLineDetil.ToString)

                Next

                'sw.WriteLine(itemLine.ToString())


                'sw.WriteLine(itemLineDetilHeader.ToString())
                'sw.WriteLine(itemLineDetil.ToString())



                ' DealerCode = item.DealerCode.ToString
                i = i + 1
                '   End If
                index = index + 1
            Next
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        'sessHelper.SetSession("ListSession", _arrList)
        'Dim data As ArrayList = CType(sessHelper.GetSession("ListSession"), ArrayList)
        'If IsNothing(data) Then
        '    MessageBox.Show("Tidak ada data yang di download")
        'Else
        '    DoDownload(data)
        'End If

        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If lblDelerSession.Visible = False Then
            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "Status", MatchType.No, 0))
        End If


        If Not ddlStatus.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If


        If cbDateClaim.Checked = True Then
            If CInt(icDateClaim.Value.ToString("yyyy")) > 1900 And CInt(icDateClaimTo.Value.ToString("yyyy")) > 1900 Then
                Dim tgl As New DateTime(CInt(icDateClaim.Value.Year), CInt(icDateClaim.Value.Month), CInt(icDateClaim.Value.Day), 0, 0, 0)
                Dim tgl1 As New DateTime(CInt(icDateClaimTo.Value.Year), CInt(icDateClaimTo.Value.Month), CInt(icDateClaimTo.Value.Day), 0, 0, 0)
                criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ClaimDate", MatchType.GreaterOrEqual, Format(tgl, "yyyy-MM-dd HH:mm:ss")))
                criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ClaimDate", MatchType.LesserOrEqual, Format(tgl1, "yyyy-MM-dd HH:mm:ss")))
            End If
        End If

        'If cbDateBayar.Checked = True Then
        '    If CInt(icDateBayarFrom.Value.ToString("yyyy")) > 1900 And CInt(icDateBayarTo.Value.ToString("yyyy")) > 1900 Then
        '        Dim tgl As New DateTime(CInt(icDateBayarFrom.Value.Year), CInt(icDateBayarFrom.Value.Month), CInt(icDateBayarFrom.Value.Day), 0, 0, 0)
        '        Dim tgl1 As New DateTime(CInt(icDateBayarTo.Value.Year), CInt(icDateBayarTo.Value.Month), CInt(icDateBayarTo.Value.Day), 0, 0, 0)
        '        criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "PlanningPaymentDate", MatchType.GreaterOrEqual, Format(tgl, "yyyy-MM-dd HH:mm:ss")))
        '        criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "PlanningPaymentDate", MatchType.LesserOrEqual, Format(tgl1, "yyyy-MM-dd HH:mm:ss")))
        '    End If
        'End If

        If cbDateBayar.Checked = True Then
            If CInt(icDateBayarFrom.Value.ToString("yyyy")) > 1900 And CInt(icDateBayarTo.Value.ToString("yyyy")) > 1900 Then
                Dim tgl As New DateTime(CInt(icDateBayarFrom.Value.Year), CInt(icDateBayarFrom.Value.Month), CInt(icDateBayarFrom.Value.Day), 0, 0, 0)
                Dim tgl1 As New DateTime(CInt(icDateBayarTo.Value.Year), CInt(icDateBayarTo.Value.Month), CInt(icDateBayarTo.Value.Day), 0, 0, 0)

                Dim strSql As String = ""
                strSql += " select BenefitClaimHeaderID from BenefitClaimJV"
                strSql += " where PaymentDate between  '" & Format(tgl, "yyyy-MM-dd HH:mm:ss") & "' and '" & Format(tgl1, "yyyy-MM-dd HH:mm:ss") & "'"
                strSql += " and isnull(PaymentDate,'') <> '' "
                criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ID", MatchType.InSet, "(" & strSql & ")"))

                '  criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "PlanningPaymentDate", MatchType.GreaterOrEqual, Format(tgl, "yyyy-MM-dd HH:mm:ss")))
                ' criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "PlanningPaymentDate", MatchType.LesserOrEqual, Format(tgl1, "yyyy-MM-dd HH:mm:ss")))
            End If
        End If


        If txtKodeName.Text <> "" Or ddlPilihanClaim.SelectedValue <> "" Or Not ddlStatusUpload.SelectedValue = "" Then

            Dim valuedddlPilihanClaim() As String = ddlPilihanClaim.SelectedValue.Replace(" ", "").Split(";")


            Dim condition As String = ""
            If txtKodeName.Text <> "" Then
                condition += " and d.DealerCode = '" & txtKodeName.Text & "'"
                condition += " and a.dealerid in (select id from dealer where dealercode in ('" & txtKodeName.Text.Trim & "'))"
            End If
            If ddlPilihanClaim.SelectedValue <> "" Then
                condition += "  and b.BenefitTypeID = '" & valuedddlPilihanClaim(0) & "' "
                If valuedddlPilihanClaim(1) = "1" And ddlLeasing.SelectedValue <> "" Then
                    condition += "  and e.LeasingCompanyID in (" & ddlLeasing.SelectedValue & ") "
                    condition += "  and a.LeasingCompanyID in (" & ddlLeasing.SelectedValue & ") "
                End If
            End If

            If ddlStatusUpload.SelectedValue <> "" Then
                condition += "  and  cc.statusupload = " & ddlStatusUpload.SelectedValue
            End If

            Dim strSql As String = ""
            strSql += "  select distinct a.id from BenefitClaimHeader a inner join"
            strSql += "  BenefitClaimDetails cc on a.ID = cc.BenefitClaimHeaderID and cc.RowStatus = 0 left join BenefitMasterDetail b"
            strSql += " on cc.BenefitMasterDetailID = b.ID "
            strSql += " inner join BenefitMasterDealer c on b.BenefitMasterHeaderID = c.BenefitMasterHeaderID "
            strSql += " inner join Dealer d on c.DealerID = d.ID"
            strSql += "   left join BenefitMasterLeasing e on b.ID = e.BenefitMasterDetailID "
            strSql += " where 1=1 " & condition


            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        If txtNoClaim.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ClaimRegNo", MatchType.[Partial], txtNoClaim.Text.Trim))
        End If

        If txtNoRangka.Text <> "" Or ddlKategori.SelectedValue <> "" Then
            Dim strSql As String = ""
            strSql += "  select BenefitClaimHeaderID from BenefitClaimDetails a "
            strSql += "  inner join ChassisMaster b on a.ChassisMasterID = b.ID "
            strSql += "  where 1=1  "
            If txtNoRangka.Text <> "" Then
                strSql += "  and  b.ChassisNumber like '%" & txtNoRangka.Text & "%'"
            End If
            If ddlKategori.SelectedValue <> "" Then
                strSql += "  and  b.categoryid = " & ddlKategori.SelectedValue
            End If
            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If

        If txtIdDetailMaster.Value <> "" Then
            Dim strSql As String = ""
            strSql += "  select BenefitClaimHeaderID from BenefitClaimDetails a"
            strSql += " inner join BenefitMasterDetail b on a.benefitmasterdetailid = b.id"
            strSql += " where 1=1 "
            If txtIdDetailMaster.Value <> "" Then
                strSql += "  and  BenefitMasterHeaderID in (" & txtIdDetailMaster.Value & ")"
            End If

            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "ID", MatchType.InSet, "(" & strSql & ")"))

        End If


        If Not ddlStatusTransfer.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(BenefitClaimHeader), "IsTransfer", MatchType.Exact, CShort(ddlStatusTransfer.SelectedValue)))
        End If

        _arrList = New BenefitClaimHeaderFacade(User).Retrieve(criterias)
        DoDownload(_arrList)
    End Sub


    Private Sub JVCommand(ByVal e As DataGridCommandEventArgs, ByVal id As Integer)
        sessHelper.RemoveSession("ListSessionJV")
        sessHelper.RemoveSession("ListSessionJVClaimHeader")
        sessHelper.RemoveSession("ListSessionJVDPP")

        'Dim _arrListNew As New ArrayList
        'Dim criteriasNew As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Dim strSql1 As String = ""
        'strSql1 += " select id from BenefitClaimDetails where RowStatus = 0 and DetailStatus = 1"
        'strSql1 += " and BenefitClaimHeaderID = " & id
        'criteriasNew.opAnd(New Criteria(GetType(BenefitClaimDetails), "ID", MatchType.InSet, "(" & strSql1 & ")"))
        '_arrListNew = New BenefitClaimDetailsFacade(User).Retrieve(criteriasNew)




        Dim _arrList As New ArrayList

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimJV), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim strSql As String = ""
        strSql += "  select id from benefitclaimjv "
        strSql += " where benefitclaimheaderid = " & id

        criterias.opAnd(New Criteria(GetType(BenefitClaimJV), "ID", MatchType.InSet, "(" & strSql & ")"))

        _arrList = New BenefitClaimJVFacade(User).Retrieve(criterias)

        Dim objBenefitClaimHeaderTemp As BenefitClaimHeader = New BenefitClaimHeader
        Dim objBenefitClaimJV As BenefitClaimJV = New BenefitClaimJV



        Dim list As New ArrayList
        If Not sessHelper.GetSession("ListSession") Is Nothing Then

            list = CType(sessHelper.GetSession("ListSession"), ArrayList)

            For Each item As BenefitClaimHeader In list
                If item.ID = id Then
                    objBenefitClaimHeaderTemp = item
                    Exit For
                End If
            Next
        End If

        Dim total As Decimal = 0, temptotal As Decimal = 0
        Dim nilaiPph As Decimal = 0
        Dim nilaiPpn As Decimal = 0

        criterias = New CriteriaComposite(New Criteria(GetType(BenefitClaimReceipt), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BenefitClaimReceipt), "BenefitClaimHeader", MatchType.Exact, objBenefitClaimHeaderTemp.ID))
        Dim arrBenefitClaimReceipt As ArrayList = New BenefitClaimReceiptFacade(User).Retrieve(criterias)
        Dim objBenefitClaimReceipt As BenefitClaimReceipt
        If arrBenefitClaimReceipt.Count <> 0 Then
            objBenefitClaimReceipt = arrBenefitClaimReceipt(0)
        End If

        Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objBenefitClaimReceipt.FakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(objBenefitClaimReceipt.FakturPajakDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        If Not objBenefitClaimHeaderTemp Is Nothing Then

            For Each item As BenefitClaimDetails In objBenefitClaimHeaderTemp.BenefitClaimDetailss
                If item.DetailStatus = 1 Then
                    total += item.BenefitMasterDetail.Amount
                End If
            Next
            temptotal = total


            If objBenefitClaimHeaderTemp.BenefitType.LeasingBox = 1 Then
                'nilaiPph = Math.Round(((temptotal / (1 - 0.15)) - temptotal))
                'nilaiPpn = Math.Round((0.1 * (nilaiPph + temptotal)))
                Dim dpp As Decimal = CalcHelper.DPPCalculation(pph, total)
                nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.Total, pph, total:=total)
                nilaiPpn = CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=dpp)
            Else
                'nilaiPph = Math.Round(0.15 * temptotal)
                'nilaiPpn = 0 'Math.Round(0.1 * temptotal)
                nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=total)
                nilaiPpn = 0
            End If

            Dim tableket As String = ""
            tableket = "<table cellspacing='1' cellpadding='3' bordercolor='Gainsboro' border='0' style='background-color: rgb(205, 205, 205);border-color:Gainsboro;border-width:0px;width:100%;' rules='all'> "
            tableket += "<tr align='center' style='color:White;background-color:Blue;font-weight:bold;'>"
            tableket += "<td class='titleTableSales'>Claim Nett</td><td  class='titleTableSales'>PPh</td><td  class='titleTableSales'>PPn</td><td  class='titleTableSales'>DPP</td></tr>"
            tableket += " <tr> "
            tableket += " <td align='right'> " & total.ToString("#,##0.00") & " </td>"
            tableket += " <td align='right'> " & nilaiPph.ToString("#,##0.00") & " </td>"
            tableket += " <td align='right'> " & nilaiPpn.ToString("#,##0.00") & " </td>"
            tableket += " <td align='right'> " & (total + nilaiPph).ToString("#,##0.00") & " </td>"
            tableket += " </tr> "
            tableket += "    </table>"

            lblJvKet.Text = tableket

            sessHelper.SetSession("ListSessionJVDPP", (total + nilaiPph))

        End If

        If _arrList.Count < 1 Then
            'D = DEALER, V = VENDOR, E = EXPENSE, O = VENDOR OTHER

            objBenefitClaimJV.BenefitClaimHeader = objBenefitClaimHeaderTemp
            'If objBenefitClaimHeaderTemp.BenefitType.ReceiptBox = 1 Then
            '    objBenefitClaimJV.TipeAccount = "D"


            'ElseIf objBenefitClaimHeaderTemp.BenefitType.LeasingBox = 1 Then
            '    objBenefitClaimJV.TipeAccount = "V"
            '    objBenefitClaimJV.VendorID = objBenefitClaimHeaderTemp.LeasingCompany.VendorID
            '    ' objBenefitClaimJV.TipeAccount = objBenefitClaimHeaderTemp.LeasingCompany.VendorID
            'End If

            If objBenefitClaimHeaderTemp.BenefitType.LeasingBox = 1 Then


                objBenefitClaimJV.TipeAccount = "V"
                objBenefitClaimJV.VendorID = objBenefitClaimHeaderTemp.LeasingCompany.VendorID
            ElseIf objBenefitClaimHeaderTemp.BenefitType.ReceiptBox = 1 Then
                objBenefitClaimJV.TipeAccount = "D"

            End If

            objBenefitClaimJV.Amount = (total + nilaiPpn)
            _arrList.Add(objBenefitClaimJV)





        End If






        sessHelper.SetSession("ListSessionJV", _arrList)
        sessHelper.SetSession("ListSessionJVClaimHeader", objBenefitClaimHeaderTemp)

        dgJV.EditItemIndex = -1
        dgJV.ShowFooter = False

        dgJV.DataSource = _arrList
        dgJV.DataBind()


        panel1.Attributes("style") = "display:none;"
        panel2.Attributes("style") = "display:none;"

        PanelJV.Attributes("style") = "display:;"







    End Sub

    Private Sub dgJV_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgJV.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As BenefitClaimJV = CType(e.Item.DataItem, BenefitClaimJV)
            ' If e.Item.ItemType = ListItemType.Item Then
            If Not objDomain2 Is Nothing Then
                If Not e.Item.ItemType = ListItemType.EditItem Then

                    Dim lblAccount As Label = CType(e.Item.FindControl("lblAccount"), Label)
                    If Not objDomain2.BenefitClaimHeader Is Nothing Then
                        If objDomain2.TipeAccount = "D" Then
                            lblAccount.Text = objDomain2.BenefitClaimHeader.Dealer.DealerCode
                        ElseIf objDomain2.TipeAccount = "V" Then
                            ' lblAccount.Text = objDomain2.BenefitClaimHeader.LeasingCompany.LeasingName
                            lblAccount.Text = "VENDOR"
                        ElseIf objDomain2.TipeAccount = "E" Then
                            lblAccount.Text = "EXPENSE"
                        ElseIf objDomain2.TipeAccount = "O" Then
                            lblAccount.Text = "VENDOR OTHER"
                        End If

                    End If


                    Dim lblVendor As Label = CType(e.Item.FindControl("lblVendor"), Label)
                    lblVendor.Text = objDomain2.VendorID

                    Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
                    lblAmount.Text = objDomain2.Amount.ToString("#,##0.00")

                    Dim lblMonth As Label = CType(e.Item.FindControl("lblMonth"), Label)
                    lblMonth.Text = getmonth(objDomain2.Month)

                    Dim lblPembayaran As Label = CType(e.Item.FindControl("lblPembayaran"), Label)
                    If CInt(objDomain2.PaymentDate.ToString("yyyy")) > 1900 Then
                        lblPembayaran.Text = objDomain2.PaymentDate.ToString("dd/MM/yyyy")
                    End If


                    Dim lblBusinessArea As Label = CType(e.Item.FindControl("lblBusinessArea"), Label)
                    lblBusinessArea.Text = objDomain2.BusinessArea

                    Dim lblCostCenter As Label = CType(e.Item.FindControl("lblCostCenter"), Label)
                    lblCostCenter.Text = objDomain2.CostCenter

                    Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
                    Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
                    Dim lnkbtnAdd As LinkButton = CType(e.Item.FindControl("lnkbtnAdd"), LinkButton)
                    If e.Item.ItemIndex < 1 Then
                        lnkbtnEdit.Visible = True
                        lnkbtnDelete.Visible = Not lnkbtnEdit.Visible
                        lnkbtnAdd.Visible = lnkbtnEdit.Visible

                        lblCostCenter.Text = ""
                        lblBusinessArea.Visible = False
                        lblBusinessArea.Text = lblCostCenter.Text
                        lblBusinessArea.Visible = lblCostCenter.Visible

                    Else
                        lnkbtnAdd.Visible = False
                    End If
                Else
                    Dim lblAccountEditGrid As Label = CType(e.Item.FindControl("lblAccountEditGrid"), Label)
                    If Not objDomain2.BenefitClaimHeader Is Nothing Then
                        If objDomain2.TipeAccount = "D" Then
                            lblAccountEditGrid.Text = objDomain2.BenefitClaimHeader.Dealer.DealerCode
                        ElseIf objDomain2.TipeAccount = "V" Then
                            lblAccountEditGrid.Text = objDomain2.BenefitClaimHeader.LeasingCompany.LeasingName
                        ElseIf objDomain2.TipeAccount = "E" Then
                            lblAccountEditGrid.Text = "EXPENSE"
                        ElseIf objDomain2.TipeAccount = "O" Then
                            lblAccountEditGrid.Text = "VENDOR OTHER"
                        End If

                    End If


                    Dim txtBusinessAreaEditGrid As TextBox = CType(e.Item.FindControl("txtBusinessAreaEditGrid"), TextBox)
                    txtBusinessAreaEditGrid.Text = objDomain2.BusinessArea

                    Dim txtCostCenterEditGrid As TextBox = CType(e.Item.FindControl("txtCostCenterEditGrid"), TextBox)
                    txtCostCenterEditGrid.Text = objDomain2.CostCenter


                    Dim lblVendorEditGrid As Label = CType(e.Item.FindControl("lblVendorEditGrid"), Label)
                    Dim ddlVendorEditGrid As DropDownList = CType(e.Item.FindControl("ddlVendorEditGrid"), DropDownList)
                    Dim icPembayaranGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icPembayaranEditGrid"), KTB.DNet.WebCC.IntiCalendar)
                    If e.Item.ItemIndex < 1 Then 'yg baris 1
                        lblVendorEditGrid.Text = objDomain2.VendorID
                        lblVendorEditGrid.Visible = True
                        ddlVendorEditGrid.Visible = Not lblVendorEditGrid.Visible
                        txtBusinessAreaEditGrid.Text = ""
                        txtBusinessAreaEditGrid.Visible = False
                        txtCostCenterEditGrid.Text = txtBusinessAreaEditGrid.Text
                        txtCostCenterEditGrid.Visible = txtBusinessAreaEditGrid.Visible
                    Else
                        lblVendorEditGrid.Text = objDomain2.VendorID
                        InitPilihanLeasingJV(ddlVendorEditGrid)
                        ddlVendorEditGrid.SelectedValue = objDomain2.VendorID
                        If objDomain2.VendorID = "" Then
                            ddlVendorEditGrid.Attributes("style") = "display:none"
                        End If

                        lblVendorEditGrid.Visible = False
                        ddlVendorEditGrid.Visible = Not lblVendorEditGrid.Visible

                        icPembayaranGrid.Enabled = False
                    End If



                    Dim lblAmountEditGrid As Label = CType(e.Item.FindControl("lblAmountEditGrid"), Label)
                    lblAmountEditGrid.Text = objDomain2.Amount.ToString("#,##0.00")

                    Dim ddlAccueredEditGrid As DropDownList = CType(e.Item.FindControl("ddlAccueredEditGrid"), DropDownList)
                    ddlAccueredEditGrid.Items.Clear()
                    EnumToListBox(GetType(AccueredMonth), ddlAccueredEditGrid)
                    ddlAccueredEditGrid.SelectedValue = objDomain2.Month

                    If CInt(objDomain2.PaymentDate.ToString("yyyy")) > 1900 Then
                        icPembayaranGrid.Value = objDomain2.PaymentDate
                    End If



                End If
            End If
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            Dim ar As ArrayList = CType(sessHelper.GetSession("ListSessionJV"), ArrayList)
            Dim ddlTipeAccount As DropDownList = CType(e.Item.FindControl("ddlTipeAccountGrid"), DropDownList)
            Dim txtAmountGrid As TextBox = CType(e.Item.FindControl("txtAmountGrid"), TextBox)



            Dim objBenefitClaimJV As BenefitClaimJV = CType(ar.Item(0), BenefitClaimJV)
            ddlTipeAccount.Items.Clear()
            Dim ta As String = ""
            Dim totalamount As Double = 0
            Dim index As Integer = 0
            For Each cat As BenefitClaimJV In ar
                'cat.TipeAccount
                ta = ta & cat.TipeAccount
                If index = 0 Then
                    '  totalamount = cat.Amount
                    totalamount = CType(sessHelper.GetSession("ListSessionJVDPP"), Double)
                Else
                    totalamount = totalamount - cat.Amount
                End If

                index = index + 1
            Next
            If objBenefitClaimJV.TipeAccount = "D" Then
                If ta.Length = 1 Then
                    ddlTipeAccount.Items.Add(New ListItem("EXPENSE", "E"))
                    ddlTipeAccount.Items.Add(New ListItem("VENDOR OTHER", "O"))
                ElseIf ta.Length = 2 Then
                    If (ta = "DO") Then
                        ddlTipeAccount.Items.Add(New ListItem("EXPENSE", "E"))

                    ElseIf (ta = "DE") Then
                        ddlTipeAccount.Items.Add(New ListItem("VENDOR OTHER", "O"))
                    End If
                Else

                End If
            Else
                If ta.Length = 1 Then
                    ddlTipeAccount.Items.Add(New ListItem("EXPENSE", "E"))
                    ddlTipeAccount.Items.Add(New ListItem("VENDOR OTHER", "O"))
                    ddlTipeAccount.Items.Add(New ListItem("VENDOR", "V"))
                ElseIf ta.Length = 2 Then
                    If (ta = "VO") Then
                        ddlTipeAccount.Items.Add(New ListItem("EXPENSE", "E"))
                    ElseIf (ta = "VE") Then
                        ddlTipeAccount.Items.Add(New ListItem("VENDOR OTHER", "O"))
                    End If
                Else

                End If
                'If ta.Contains("E") = False Then
                '    ddlTipeAccount.Items.Add(New ListItem("EXPENSE", "E"))
                'End If
                'If ta.Contains("O") = False Then
                '    ddlTipeAccount.Items.Add(New ListItem("VENDOR OTHER", "O"))
                'End If
                '' Dim Count As Integer = (From c As Char In ta.ToCharArray() Where c.Equals("o"c) Select c).Count
                'If ta.Contains("V") = False Then
                '    ddlTipeAccount.Items.Add(New ListItem("VENDOR", "V"))
                'End If

            End If

            Dim ddlAccueredGrid As DropDownList = CType(e.Item.FindControl("ddlAccueredGrid"), DropDownList)
            EnumToListBox(GetType(AccueredMonth), ddlAccueredGrid)


            Dim ddlVendorGrid As DropDownList = CType(e.Item.FindControl("ddlVendorGrid"), DropDownList)
            InitPilihanLeasingJV(ddlVendorGrid)
            ddlVendorGrid.Attributes("style") = "display:none"


            txtAmountGrid.Text = totalamount.ToString("#,##0.00")
            'txtAmountGrid.Text = totalamount.ToString()
            If ddlTipeAccount.Items.Count = 1 Then
                If ddlTipeAccount.Items(0).Value = "O" Then
                    txtAmountGrid.Attributes("disabled") = "disabled"
                End If

            Else

            End If


            ' If e.Item.ItemIndex > 0 Then
            Dim icPembayaranGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icPembayaranGrid"), KTB.DNet.WebCC.IntiCalendar)
            icPembayaranGrid.Value = objBenefitClaimJV.PaymentDate
            icPembayaranGrid.Enabled = False
            'End If

            ddlTipeAccount.Attributes("onchange") = "showhideLeasingJV();"
            'ddlPilihanClaim.Attributes("onchange") = "showhideLeasing();"
        End If
    End Sub

    Private Sub InitPilihanLeasingJV(ByVal ddl As DropDownList)
        Dim facade As New LeasingCompanyFacade(User)
        Dim arlfacade As ArrayList = facade.RetrieveActiveList()
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("", ""))

        For Each cat As LeasingCompany In arlfacade
            ddl.Items.Add(New ListItem(cat.LeasingName, cat.VendorID))
        Next

    End Sub

    Private Sub btnAddJv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddJv.Click
        If Not sessHelper.GetSession("ListSessionJV") Is Nothing Then
            Dim ar As ArrayList = CType(sessHelper.GetSession("ListSessionJV"), ArrayList)

            Dim objBenefitClaimHeader As BenefitClaimHeader = CType(sessHelper.GetSession("ListSessionJVClaimHeader"), BenefitClaimHeader)

            If ar.Count > 1 Then
                Dim cekRow1 As BenefitClaimJV = CType(ar.Item(0), BenefitClaimJV)
                If CInt(cekRow1.PaymentDate.ToString("yyyy")) > 1900 Or cekRow1.Month > 0 Then
                    Dim objDomainFacade As BenefitClaimJVFacade = New BenefitClaimJVFacade(User)
                    Dim n As Integer = objDomainFacade.InsertUpdateJV(ar, objBenefitClaimHeader)

                    If n = -1 Then
                        MessageBox.Show(SR.SaveFail)

                    Else
                        ' RemoveALLSession()
                        MessageBox.Show(SR.SaveSuccess)

                    End If

                    sessHelper.SetSession("ListSessionJV", ar)

                    dgJV.DataSource = ar
                    dgJV.DataBind()
                Else
                    MessageBox.Show("Isi Month dan Tanggal pembayaran di data pertama atau baris pertama.")
                End If


            Else
                MessageBox.Show("Isi minimal 2 data.")
            End If



            panel1.Attributes("style") = "display:;"
            panel2.Attributes("style") = "display:none;"

            PanelJV.Attributes("style") = "display:none;"
        End If

    End Sub



    Private Sub dgJV_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgJV.ItemCommand
        Dim show As String = ""

        Select Case e.CommandName
            Case "Add"
                JVAddCommand(e)
                ' Response.Redirect("~/Benefit/FrmInputClaim.aspx?Mode=View&id=" & CInt(e.CommandArgument))
            Case "addJv"
                ' JVCommand(e, CInt(e.CommandArgument))
            Case "AddSave"
                JVAddSaveCommand(e)
            Case "Edit"
                JVEditCommand(e)
            Case "Save"
                UpdateCommand(e)
            Case "Delete"
                DeleteCommand(e)
        End Select
    End Sub

    Private Sub JVEditCommand(ByVal e As DataGridCommandEventArgs)

        dgJV.EditItemIndex = CInt(e.Item.ItemIndex)
        dgJV.ShowFooter = False

        Dim ar As ArrayList
        If Not sessHelper.GetSession("ListSessionJV") Is Nothing Then
            ar = CType(sessHelper.GetSession("ListSessionJV"), ArrayList)
        End If

        dgJV.DataSource = ar
        dgJV.DataBind()

        panel1.Attributes("style") = "display:none;"
        panel2.Attributes("style") = "display:none;"

        PanelJV.Attributes("style") = "display:;"


    End Sub

    Private Sub JVAddCommand(ByVal e As DataGridCommandEventArgs)

        If Not sessHelper.GetSession("ListSessionJV") Is Nothing Then
            Dim ar As ArrayList = CType(sessHelper.GetSession("ListSessionJV"), ArrayList)
            panel1.Attributes("style") = "display:none;"
            panel2.Attributes("style") = "display:none;"
            PanelJV.Attributes("style") = "display:;"
            'PanelJvInput.Visible = True



            Dim isfail As Boolean = True

            Dim objBenefitClaimJV As BenefitClaimJV = CType(ar.Item(0), BenefitClaimJV)
            If objBenefitClaimJV.Month = Nothing Or objBenefitClaimJV.PaymentDate = Nothing Then
                MessageBox.Show("Isi Month dan Tanggal Pembayaran di baris pertama")
            Else
                isfail = False
            End If

            If isfail = False Then
                If objBenefitClaimJV.Amount = 0 Then
                    isfail = True
                    MessageBox.Show("Amount baris pertama tidak boleh kosong")
                End If
            End If

            If isfail = False Then
                Dim ta As String = ""
                For Each cat As BenefitClaimJV In ar
                    'cat.TipeAccount
                    ta = ta & cat.TipeAccount
                Next
                If objBenefitClaimJV.TipeAccount = "D" Then
                    If ar.Count = 3 Then
                        isfail = True
                        MessageBox.Show("Tipe Account Dealer maksimal 3 (Expense atau Vendor Other)")

                    End If
                Else


                    If Not ta = "VV" Then
                        If ar.Count = 3 Then
                            isfail = True
                            MessageBox.Show("Tipe Account Vendor maksimal 3 (Expense atau Vendor Other atau Vendor)")

                        End If
                    Else
                        MessageBox.Show("Tipe Account Vendor maksimal 2")
                    End If

                End If

                If isfail = False Then
                    If ar.Count > 3 Then
                        isfail = True
                        MessageBox.Show("Maximal 3 data")
                    End If
                End If

            End If

            If isfail = False Then
                dgJV.ShowFooter = True
                sessHelper.SetSession("ListSessionJV", ar)
            End If
            dgJV.DataSource = ar
            dgJV.DataBind()
        End If


    End Sub


    Private Sub JVAddSaveCommand(ByVal e As DataGridCommandEventArgs)

        Dim ddlTipeAccountGrid As DropDownList = CType(e.Item.FindControl("ddlTipeAccountGrid"), DropDownList)
        Dim ddlAccueredGrid As DropDownList = CType(e.Item.FindControl("ddlAccueredGrid"), DropDownList)
        Dim ddlVendorGrid As DropDownList = CType(e.Item.FindControl("ddlVendorGrid"), DropDownList)
        Dim txtAmountGrid As TextBox = CType(e.Item.FindControl("txtAmountGrid"), TextBox)
        ' Dim icPembayaranGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icPembayaranGrid"), KTB.DNet.WebCC.IntiCalendar)
        Dim txtBusinessAreaGrid As TextBox = CType(e.Item.FindControl("txtBusinessAreaGrid"), TextBox)
        Dim txtCostCenterGrid As TextBox = CType(e.Item.FindControl("txtCostCenterGrid"), TextBox)

        Dim isFail As Boolean = False
        'If CInt(icPembayaranGrid.Value.ToString("yyyy")) < 1900 Then
        '    MessageBox.Show("Isi Tanggal Pembayaran")
        '    isFail = True
        'End If

        If isFail = False Then
            If Not e.Item.ItemIndex = 0 Then
                If ddlTipeAccountGrid.SelectedValue = "E" Then
                    If txtBusinessAreaGrid.Text = "" Or txtCostCenterGrid.Text = "" Then
                        MessageBox.Show("Isi Business Area dan Cost Center")
                        isFail = True
                    End If
                End If

            End If
        End If
        If isFail = False Then
            If ddlAccueredGrid.SelectedValue = "" Then
                MessageBox.Show("Isi Bulan Accured")
                isFail = True
            End If
        End If

        Dim ar As ArrayList = New ArrayList
        If Not sessHelper.GetSession("ListSessionJV") Is Nothing Then
            ar = CType(sessHelper.GetSession("ListSessionJV"), ArrayList)
        End If

        Dim cekRow1 As BenefitClaimJV = CType(ar.Item(0), BenefitClaimJV)

        Dim totalamount As Decimal = 0
        Dim index As Integer = 0
        Dim totalamountRow1 As Decimal = 0
        For Each cat As BenefitClaimJV In ar
            If index = 0 Then
                'CType(sessHelper.GetSession("ListSessionJVDPP"), Double)
                'totalamount = cat.Amount
                'totalamountRow1 = cat.Amount
                totalamount = CType(sessHelper.GetSession("ListSessionJVDPP"), Decimal)
                totalamountRow1 = CType(sessHelper.GetSession("ListSessionJVDPP"), Decimal)
            Else
                If Not cat.TipeAccount = "O" Then
                    totalamount = totalamount - cat.Amount
                End If
            End If
            index = index + 1
        Next

        Dim objBenefitClaimJV As BenefitClaimJV = New BenefitClaimJV
        If ddlTipeAccountGrid.SelectedValue = "V" Or ddlTipeAccountGrid.SelectedValue = "E" Then
            Dim textamount As String = txtAmountGrid.Text.Split(",")(0)
            textamount = textamount.Replace(".", "")
            'objBenefitClaimJV.Amount = txtAmountGrid.Text.Replace(".", "").Replace(",", ".")
            objBenefitClaimJV.Amount = textamount
            'If cekRow1.Amount < CDbl(txtAmountGrid.Text.Replace(".", "").Replace(",", ".")) Then
            ' If cekRow1.Amount > CDbl(txtAmountGrid.Text) Then
            If cekRow1.Amount < CDbl(textamount) Then
                MessageBox.Show("Amount tidak boleh melebih amount yang sudah ada")
                isFail = True
            End If
        ElseIf ddlTipeAccountGrid.SelectedValue = "O" Then
            objBenefitClaimJV.Amount = totalamount
            'If totalamount < CDbl(txtAmountGrid.Text.Replace(".", "").Replace(",", ".")) Then
            '    MessageBox.Show("Amount tidak boleh melebih amount yang sudah ada")
            '    isFail = True
            'End If
        End If



        If isFail = False Then
            ' Dim cekRow1 As BenefitClaimJV = CType(ar.Item(0), BenefitClaimJV)


            objBenefitClaimJV.PaymentDate = cekRow1.PaymentDate

            objBenefitClaimJV.TipeAccount = ddlTipeAccountGrid.SelectedValue
            objBenefitClaimJV.Month = ddlAccueredGrid.SelectedValue
            objBenefitClaimJV.VendorID = ddlVendorGrid.SelectedValue


            ' objBenefitClaimJV.PaymentDate = icPembayaranGrid.Value
            objBenefitClaimJV.BusinessArea = txtBusinessAreaGrid.Text
            objBenefitClaimJV.CostCenter = txtCostCenterGrid.Text

            If Not sessHelper.GetSession("ListSessionJVClaimHeader") Is Nothing Then
                objBenefitClaimJV.BenefitClaimHeader = CType(sessHelper.GetSession("ListSessionJVClaimHeader"), BenefitClaimHeader)
            End If

            ar.Add(objBenefitClaimJV)

            If ar.Count = 3 Then
                Dim cekRow2 As BenefitClaimJV = CType(ar.Item(1), BenefitClaimJV)
                Dim cekRow3 As BenefitClaimJV = CType(ar.Item(2), BenefitClaimJV)
                If cekRow2.TipeAccount = "O" Then
                    cekRow2.Amount = totalamount - cekRow3.Amount
                End If

                ar.RemoveAt(1)
                ar.Insert(1, cekRow2)
            End If



            sessHelper.SetSession("ListSessionJV", ar)
            dgJV.DataSource = ar
            dgJV.DataBind()
        End If

        panel1.Attributes("style") = "display:none;"
        panel2.Attributes("style") = "display:none;"

        PanelJV.Attributes("style") = "display:;"
        dgJV.ShowFooter = False
    End Sub



    Private Sub UpdateCommand(ByVal e As DataGridCommandEventArgs)

        Dim listAll As ArrayList = CType(sessHelper.GetSession("ListSessionJV"), ArrayList)
        Dim ObjBenefitClaimJV As BenefitClaimJV = New BenefitClaimJV
        ObjBenefitClaimJV = CType(listAll(e.Item.ItemIndex), BenefitClaimJV)

        Dim ddlAccueredEditGrid As DropDownList = CType(e.Item.FindControl("ddlAccueredEditGrid"), DropDownList)
        Dim ddlVendorEditGrid As DropDownList = CType(e.Item.FindControl("ddlVendorEditGrid"), DropDownList)
        Dim lblVendorEditGrid As Label = CType(e.Item.FindControl("lblVendorEditGrid"), Label)
        Dim icPembayaranGrid As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icPembayaranEditGrid"), KTB.DNet.WebCC.IntiCalendar)
        Dim txtBusinessAreaEditGrid As TextBox = CType(e.Item.FindControl("txtBusinessAreaEditGrid"), TextBox)
        Dim txtCostCenterEditGrid As TextBox = CType(e.Item.FindControl("txtCostCenterEditGrid"), TextBox)

        Dim isFail As Boolean = False
        If CInt(icPembayaranGrid.Value.ToString("yyyy")) < 1900 Then
            MessageBox.Show("Isi Tanggal Pembayaran")
            isFail = True
        End If

        If isFail = False Then
            If Not e.Item.ItemIndex = 0 Then
                If ObjBenefitClaimJV.TipeAccount = "E" Then
                    If txtBusinessAreaEditGrid.Text = "" Or txtCostCenterEditGrid.Text = "" Then
                        MessageBox.Show("Isi Business Area dan Cost Center")
                        isFail = True
                    End If
                End If
            End If
        End If
        If isFail = False Then
            If ddlAccueredEditGrid.SelectedValue = "" Then
                MessageBox.Show("Isi Bulan Accured")
                isFail = True
            End If
        End If



        If isFail = False Then
            If e.Item.ItemIndex = 0 Then
                ObjBenefitClaimJV.VendorID = lblVendorEditGrid.Text
            Else
                ObjBenefitClaimJV.VendorID = ddlVendorEditGrid.SelectedValue
                ObjBenefitClaimJV.BusinessArea = txtBusinessAreaEditGrid.Text
                ObjBenefitClaimJV.CostCenter = txtCostCenterEditGrid.Text
            End If
            ObjBenefitClaimJV.Month = ddlAccueredEditGrid.SelectedValue
            ObjBenefitClaimJV.PaymentDate = icPembayaranGrid.Value


            If Not listAll Is Nothing Then
                listAll.RemoveAt(e.Item.ItemIndex)
                listAll.Insert(e.Item.ItemIndex, ObjBenefitClaimJV)
            End If

            sessHelper.SetSession("ListSessionJV", listAll)

            dgJV.EditItemIndex = -1
            dgJV.DataSource = listAll
            dgJV.DataBind()
        End If

        panel1.Attributes("style") = "display:none;"
        panel2.Attributes("style") = "display:none;"

        PanelJV.Attributes("style") = "display:;"
        dgJV.ShowFooter = False

    End Sub

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs)
        Dim listAll As ArrayList = CType(sessHelper.GetSession("ListSessionJV"), ArrayList)
        Dim ObjBenefitClaimJV As BenefitClaimJV = New BenefitClaimJV
        ObjBenefitClaimJV = CType(listAll(e.Item.ItemIndex), BenefitClaimJV)

        listAll.Remove(ObjBenefitClaimJV)

        Dim totalamount As Decimal = 0
        Dim index As Integer = 0
        Dim totalamountRow1 As Decimal = 0
        For Each cat As BenefitClaimJV In listAll
            If index = 0 Then
                totalamount = CType(sessHelper.GetSession("ListSessionJVDPP"), Decimal) 'cat.Amount
                totalamountRow1 = CType(sessHelper.GetSession("ListSessionJVDPP"), Decimal) ' cat.Amount
            Else
                If Not cat.TipeAccount = "O" Then
                    totalamount = totalamount - cat.Amount
                End If
            End If
            index = index + 1
        Next


        If listAll.Count = 2 Then
            Dim cekRow2 As BenefitClaimJV = CType(listAll.Item(1), BenefitClaimJV)

            If cekRow2.TipeAccount = "O" Then
                cekRow2.Amount = totalamount
            End If

            listAll.RemoveAt(1)
            listAll.Insert(1, cekRow2)
        End If

        sessHelper.SetSession("ListSessionJV", listAll)

        dgJV.DataSource = listAll
        dgJV.DataBind()
        panel1.Attributes("style") = "display:none;"
        panel2.Attributes("style") = "display:none;"

        PanelJV.Attributes("style") = "display:;"
        dgJV.ShowFooter = False
    End Sub

    Protected Sub btnFilterCari_Click(sender As Object, e As EventArgs) Handles btnFilterCari.Click
        FilterReffClaimBy(txtNoRegBenefit.Text.Trim, txtNoSurat.Text.Trim)
    End Sub

    Private Function IsDBNull(ByVal obj As Object, ByVal replacement As Decimal) As Decimal
        If obj Is DBNull.Value Then
            Return replacement
        End If
        Return CType(obj, Decimal)
    End Function

    'Private Class CriteriaBenefitClaim
    '    Public Property Number() As Integer
    '        Get
    '            Return _count
    '        End Get
    '        Set(ByVal value As Integer)
    '            _count = value
    '        End Set
    '    End Property
    'End Class

End Class
