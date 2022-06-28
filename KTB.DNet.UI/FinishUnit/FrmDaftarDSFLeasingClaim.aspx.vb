#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Globalization
Imports System.Linq

#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Benefit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessValidation.Helpers
#End Region

Public Class FrmDaftarDSFLeasingClaim
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgDSFClaim As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAgreementNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents icUploadDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icUploadDateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label

    Protected WithEvents arrayCheck As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnUpdateStatus As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus2 As System.Web.UI.WebControls.DropDownList

    Protected WithEvents lblDelerSession As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button


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
    Private objDomain As DSFLeasingClaim = New DSFLeasingClaim
    Private objDomainFacade As DSFLeasingClaimFacade = New DSFLeasingClaimFacade(User)
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)

#Region "Private Property"
    Dim deletePriv As Boolean = False
    Dim historyPriv As Boolean = False
    Dim uploadPriv As Boolean = False

    Private objDealer As Dealer
    Private objSessionHelper As New SessionHelper
    Private inputeventparticipant_privillage As Boolean
    Private Vieweventparticipant_privillage As Boolean
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.Sales_Campaign_Daftar_DSF_Leasing_Claim_Lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - Daftar DSF Leasing Claim")
        Else
            deletePriv = SecurityProvider.Authorize(Context.User, SR.Sales_Campaign_Daftar_DSF_Leasing_Claim_Delete_Privilege)
            historyPriv = SecurityProvider.Authorize(Context.User, SR.Sales_Campaign_Daftar_DSF_Leasing_Claim_History_Privilege)
            uploadPriv = SecurityProvider.Authorize(Context.User, SR.Sales_Campaign_Daftar_DSF_Leasing_Claim_Upload_Doc_Privilege)
        End If
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
        objDealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        InitiateAuthorization()
        'uploadPriv = True
        If Not IsPostBack Then
            InitializeForm()
            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"

            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirection") = Sort.SortDirection.DESC
            ViewState("wrn") = "no"

            BindDDL()
            BindDDLPeriod()
            BindDdlStatus()
            RetrieveDealer()

            ddlPeriodeMonth.SelectedValue = Now.Month
            ddlPeriodeYear.SelectedValue = Now.Year

            dgDSFClaim.CurrentPageIndex = 0
            GetSessionCriteria()
            GetValueFromDB(dgDSFClaim.CurrentPageIndex)

            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER OrElse objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                trLeasingName.Visible = False
            Else
                trLeasingName.Visible = True
                lblDealerName.Text = objDealer.DealerName
            End If
            If objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                lblUpdateStatus.Visible = False
                ddlStatus2.Visible = False
                btnUpdateStatus.Visible = False
            End If
        Else
            If CType(ViewState("wrn"), String) = "yes" Then
                If (Request.Form("hdnValNew") = "1") Then
                    ContinueUpdateStatus()
                Else
                    InitializeFormUpdateStatus()
                End If
            End If
        End If
    End Sub

    Private Sub RetrieveDealer()
        If Not objDealer Is Nothing Then
            If Not objDealer.DealerGroup Is Nothing Then 'dealer side
                lblDelerSession.Visible = True
                lblPopUpDealer.Visible = Not lblDelerSession.Visible
                txtKodeDealer.Attributes("style") = "display:none"
                lblDelerSession.Text = objDealer.DealerCode & " / " & objDealer.DealerName
                txtKodeDealer.Text = objDealer.DealerCode
            Else
                lblDelerSession.Visible = False
                lblPopUpDealer.Visible = Not lblDelerSession.Visible
                txtKodeDealer.Attributes("style") = "display:"
            End If
        End If
    End Sub

    Private Sub BindDdlStatus()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumDSFClaim.ClaimStatus"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)

        ddlStatus.Items.Clear()
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Piih ", -1))
        For Each obj As StandardCode In arrDDL
            If obj.ValueId <> EnumStatusDSFLeasingClaim.Status.Batal_Validasi AndAlso obj.ValueId <> EnumStatusDSFLeasingClaim.Status.Batal_Konfirmasi Then
                ddlStatus.Items.Add(New ListItem(obj.ValueDesc, obj.ValueId))
            End If
        Next
        ddlStatus.SelectedIndex = 0

        '0 = Baru
        '1 = Validasi
        '2 = Konfirmasi
        '3 = Setuju by Dealer
        '4 = Selesai
        '5 = Tolak by Dealer
        '6 = BatalValidasi
        '7 = BatalKonfirmasi
        '8 = SetujuByDSF
        '9 = RejectByDSF
        '10 = TolakByMKS

        ddlStatus2.Items.Clear()
        ddlStatus2.Items.Insert(0, New ListItem("Silahkan Piih ", -1))
        For Each obj As StandardCode In arrDDL
            If objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                'If obj.ValueId = 1 OrElse obj.ValueId = 6 OrElse obj.ValueId = 8 OrElse obj.ValueId = 9 Then
                '    ddlStatus2.Items.Add(New ListItem(obj.ValueDesc, obj.ValueId))
                'End If
            ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If obj.ValueId = EnumStatusDSFLeasingClaim.Status.Setuju_by_Dealer OrElse obj.ValueId = EnumStatusDSFLeasingClaim.Status.Tolak_by_Dealer Then
                    ddlStatus2.Items.Add(New ListItem(obj.ValueDesc, obj.ValueId))
                End If
            ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If obj.ValueId = EnumStatusDSFLeasingClaim.Status.Konfirmasi OrElse obj.ValueId = EnumStatusDSFLeasingClaim.Status.Selesai OrElse obj.ValueId = EnumStatusDSFLeasingClaim.Status.Tolak_by_MKS Then
                    ddlStatus2.Items.Add(New ListItem(obj.ValueDesc, obj.ValueId))
                End If
            End If
        Next
        ddlStatus2.SelectedIndex = 0
    End Sub

    Private Sub InitializeForm()
        sessHelper.RemoveSession("FrmDaftarDSFLeasingClaim_GridSession")
    End Sub

    Private Sub InitializeFormUpdateStatus()
        sessHelper.RemoveSession("checkCounter")
        sessHelper.RemoveSession("checkDeductedCounter")
        sessHelper.RemoveSession("arrUpdateStatus")
        sessHelper.RemoveSession("sb")
        sessHelper.RemoveSession("sb2")
        sessHelper.RemoveSession("arrBenefitClaimDeducted")

        hdnStatusOld.Value = "-1"
        hdnValNew.Value = "-2"
        ViewState("wrn") = "no"
    End Sub

    Private Sub SetSessionCriteria()
        Dim arrSessCrit As ArrayList = New ArrayList
        arrSessCrit.Add(txtKodeDealer.Text.Trim) '0
        arrSessCrit.Add(lblDealerName.Text) '1
        arrSessCrit.Add(txtRegNumber.Text) '2
        arrSessCrit.Add(txtChassisNumber.Text) '3
        arrSessCrit.Add(txtAgreementNo.Text) '4
        arrSessCrit.Add(chkTglUpload.Checked) '5
        arrSessCrit.Add(icUploadDate.Value) '6
        arrSessCrit.Add(icUploadDateTo.Value) '7
        arrSessCrit.Add(chkValidateTime.Checked) '8
        arrSessCrit.Add(icValidateTimeFrom.Value) '9
        arrSessCrit.Add(icValidateTimeTo.Value) '10
        arrSessCrit.Add(ddlStatus.SelectedValue) '11
        arrSessCrit.Add(ddlKategori.SelectedValue) '12
        arrSessCrit.Add(ddlTipe.SelectedValue) '13
        arrSessCrit.Add(CType(ViewState("currSortColumn"), String)) '14
        arrSessCrit.Add(CType(ViewState("currSortDirection"), Sort.SortDirection)) '15
        sessHelper.SetSession("SessionSearchDSFClaim", arrSessCrit)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim arrSessCrit As ArrayList = sessHelper.GetSession("SessionSearchDSFClaim")
        If Not arrSessCrit Is Nothing Then
            txtKodeDealer.Text = arrSessCrit.Item(0)
            lblDealerName.Text = arrSessCrit.Item(1)
            txtRegNumber.Text = arrSessCrit.Item(2)
            txtChassisNumber.Text = arrSessCrit.Item(3)
            txtAgreementNo.Text = arrSessCrit.Item(4)
            chkTglUpload.Checked = arrSessCrit.Item(5)
            icUploadDate.Value = arrSessCrit.Item(6)
            icUploadDateTo.Value = arrSessCrit.Item(7)
            chkValidateTime.Checked = arrSessCrit.Item(8)
            icValidateTimeFrom.Value = arrSessCrit.Item(9)
            icValidateTimeTo.Value = arrSessCrit.Item(10)
            ddlStatus.SelectedValue = arrSessCrit.Item(11)
            ddlKategori.SelectedValue = arrSessCrit.Item(12)
            ddlTipe.SelectedValue = arrSessCrit.Item(13)
            ViewState("currSortColumn") = arrSessCrit.Item(14)
            ViewState("currSortDirection") = arrSessCrit.Item(15)
            Return True
        End If
        Return False
    End Function

    Private Sub GetValueFromDB(ByVal index As Integer)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DSFLeasingClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim objDealer As Dealer = Session("DEALER")

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Dim strKodeDealerIn As String = "('" & objDealer.DealerCode & "')"
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            ''2 = Konfirmasi
            ''3 = Setuju
            ''5 = Tolak
            ''7 = BatalKonfirmasi
            'Dim strStatusIn As String = "(2,3,5,7)"
            'criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "Status", MatchType.InSet, strStatusIn))

        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.KTB OrElse objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
            If txtKodeDealer.Text.Trim() <> "" Then
                Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
                criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            End If
        End If

        '-- Category
        If ddlKategori.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "ChassisMaster.Category.ID", MatchType.Exact, ddlKategori.SelectedValue))
        End If

        If ddlKategori.SelectedIndex > 0 And ddlTipe.SelectedIndex > 0 Then
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlTipe.SelectedValue
            Dim strSql2 As String = "select distinct a.ID from VechileColor a join VechileType b on a.VechileTypeID = b.ID and b.RowStatus = 0 "
            strSql2 += " join VechileModel c on b.ModelID = c.ID and c.RowStatus = 0 where a.RowStatus = 0 and c.ID in (" & strSql & ")"
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "ChassisMaster.VechileColor.ID", MatchType.InSet, "(" & strSql2 & ")"))
        End If

        If txtRegNumber.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "RegNumber", MatchType.Exact, txtRegNumber.Text.Trim))
        End If
        If txtChassisNumber.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "ChassisMaster.ChassisNumber", MatchType.Partial, txtChassisNumber.Text.Trim))
        End If
        If txtAgreementNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "AgreementNo", MatchType.Exact, txtAgreementNo.Text.Trim))
        End If
        If ddlStatus.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Short)))
        End If
        If chkTglUpload.Checked = True Then
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "CreatedTime", MatchType.GreaterOrEqual, icUploadDate.Value))
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "CreatedTime", MatchType.LesserOrEqual, icUploadDateTo.Value.AddDays(1)))
        End If
        If chkValidateTime.Checked = True Then
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "ChassisMaster.EndCustomer.ValidateTime", MatchType.GreaterOrEqual, icValidateTimeFrom.Value))
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "ChassisMaster.EndCustomer.ValidateTime", MatchType.LesserOrEqual, icValidateTimeTo.Value.AddDays(1)))
        End If
        If cbDate.Checked Then
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "CollectionPeriodMonth", MatchType.Exact, ddlPeriodeMonth.SelectedValue))
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "CollectionPeriodYear", MatchType.Exact, ddlPeriodeYear.SelectedValue))
        End If
        If cbDate.Checked Then
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "CollectionPeriodMonth", MatchType.Exact, ddlPeriodeMonth.SelectedValue))
            criterias.opAnd(New Criteria(GetType(DSFLeasingClaim), "CollectionPeriodYear", MatchType.Exact, ddlPeriodeYear.SelectedValue))
        End If

        _arrList = objDomainFacade.RetrieveActiveList(index + 1, dgDSFClaim.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection), criterias)

        SetSessionCriteria()
        sessHelper.SetSession("FrmDaftarDSFLeasingClaim_GridSession", _arrList)

        hdnAllID.Value = ""

        dgDSFClaim.DataSource = _arrList
        dgDSFClaim.VirtualItemCount = totalRow
        dgDSFClaim.DataBind()

        ' To Download
        Dim _arrListToDownload As ArrayList = New ArrayList
        _arrListToDownload = objDomainFacade.Retrieve(criterias)
        sessHelper.SetSession("FrmDaftarDSFLeasingClaim_Download", _arrListToDownload)
        lblTotalRecords.Text = "Jumlah Record Data : " & _arrListToDownload.Count.ToString()

    End Sub

    Private Sub dgDSFClaim_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDSFClaim.ItemCommand
        Dim arlData As ArrayList = New ArrayList
        SetSessionCriteria()
        If Not IsNothing(sessHelper.GetSession("FrmDaftarDSFLeasingClaim_GridSession")) Then
            arlData = CType(sessHelper.GetSession("FrmDaftarDSFLeasingClaim_GridSession"), ArrayList)
        End If

        Dim strCommand As String = String.Empty
        Dim lblPath As TextBox = FindControl("lblPath")
        strCommand = lblPath.Text

        Select Case e.CommandName
            Case "Delete"
                Dim objDSFLeasingClaim As DSFLeasingClaim = New DSFLeasingClaimFacade(User).Retrieve(CInt(e.Item.Cells(1).Text))
                If objDSFLeasingClaim.Status <> EnumStatusDSFLeasingClaim.Status.Baru Then
                    MessageBox.Show("Hapus data hanya bisa dilakukan untuk statusnya Baru")
                    Exit Sub
                End If

                objDSFLeasingClaim.RowStatus = CType(DBRowStatus.Deleted, Short)
                Dim result As Integer = New DSFLeasingClaimFacade(User).Update(objDSFLeasingClaim)
                If result > -1 Then
                    arlData.RemoveAt(e.Item.ItemIndex)
                    MessageBox.Show("Hapus data berhasil")
                Else
                    MessageBox.Show("Hapus data gagal")
                End If

            Case "lnkbtnDownloadDealer"
                Dim lnkbtnDownload As LinkButton = e.Item.FindControl("lnkbtnDownloadDealer")
                Dim fileInfox As New FileInfo(strCommand)
                Dim fileExist As Boolean = CheckFileExist(fileInfox)
                If fileExist Then
                    Try
                        Response.Redirect("../Download.aspx?file=" & strCommand)
                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail(lnkbtnDownload.Text))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfox.Name))
                End If

            Case "lnkbtnDownloadDSF"
                Dim lnkbtnDownload As LinkButton = e.Item.FindControl("lnkbtnDownloadDSF")
                Dim fileInfox As New FileInfo(strCommand)
                Dim fileExist As Boolean = CheckFileExist(fileInfox)
                If fileExist Then
                    Try
                        Response.Redirect("../Download.aspx?file=" & strCommand)
                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail(lnkbtnDownload.Text))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfox.Name))
                End If

            Case "chkItemChecked"
                Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
                If chkItemChecked.Checked = True Then
                    If hdnAllID.Value.Trim = "" Then
                        hdnAllID.Value = e.Item.Cells(1).Text
                    Else
                        hdnAllID.Value += ";" & e.Item.Cells(1).Text
                    End If
                End If

        End Select
        GetValueFromDB(dgDSFClaim.CurrentPageIndex)
    End Sub

    Private Function CheckFileExist(ByVal fileinfo As FileInfo) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                Return fileinfo.Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            imp.StopImpersonate()
            imp = Nothing
        End Try
    End Function

    Private Sub OnCheckedChangedEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim intUnCheck As Integer = 0
        Dim intDSFLeasingClaimID As Integer = 0
        Dim chkItemChecked As CheckBox = CType(sender, CheckBox)
        If chkItemChecked.Checked = True Then
            For Each item As DataGridItem In dgDSFClaim.Items
                intDSFLeasingClaimID = Convert.ToInt32(item.Cells(1).Text)
                Dim chckbox As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                If chckbox.Visible = True Then
                    If chckbox.Checked = False Then
                        intUnCheck += 1
                    End If
                    If chckbox.ClientID = chkItemChecked.ClientID Then
                        If hdnAllID.Value.Trim = "" Then
                            hdnAllID.Value = intDSFLeasingClaimID
                        Else
                            hdnAllID.Value += ";" & intDSFLeasingClaimID
                        End If
                    End If
                End If
            Next

            If intUnCheck = 0 Then
                CheckAllItemFlag(True)
            End If
        Else
            If hdnAllID.Value.Trim <> "" Then
                For Each item As DataGridItem In dgDSFClaim.Items
                    intDSFLeasingClaimID = Convert.ToInt32(item.Cells(1).Text)
                    Dim chckbox As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                    If chckbox.ClientID = chkItemChecked.ClientID Then
                        Exit For
                    End If
                Next

                Dim strAllID As String = String.Empty
                Dim arrID As String() = hdnAllID.Value.Trim.Split(";")
                For Each strID As String In arrID
                    If intDSFLeasingClaimID.ToString.Trim <> strID.Trim Then
                        If strAllID.Trim = "" Then
                            strAllID = strID.ToString
                        Else
                            strAllID += ";" & strID.ToString
                        End If
                    End If
                Next
                hdnAllID.Value = strAllID
                CheckAllItemFlag(False)
            End If
        End If
    End Sub

    Private Sub CheckAllItemFlag(ByVal chk As Boolean)
        Dim e1 As Control
        For Each e1 In dgDSFClaim.Controls
            For Each ct As Control In e1.Controls
                If TypeOf ct Is System.Web.UI.WebControls.DataGridItem Then
                    Dim di As System.Web.UI.WebControls.DataGridItem = CType(ct, System.Web.UI.WebControls.DataGridItem)
                    If di.ItemType = ListItemType.Header Then
                        Dim chkAllItems As CheckBox = CType(di.FindControl("chkAllItems"), CheckBox)
                        If Not IsNothing(chkAllItems) Then
                            chkAllItems.Checked = chk
                            Exit For
                        End If
                    End If
                End If
            Next
        Next
    End Sub

    Private Sub btnChkAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChkAll.Click
        Dim intChkAll As Integer = 0
        Dim intChkAllChecked As Integer = 0
        Dim intDSFLeasingClaimID As Integer = 0
        Dim strAllID As String = String.Empty
        For Each item As DataGridItem In dgDSFClaim.Items
            intDSFLeasingClaimID = Convert.ToInt32(item.Cells(1).Text)
            Dim chckbox As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim isExist As Boolean = False
            If chckbox.Visible = True Then
                intChkAll += 1
                If chckbox.Checked = True Then
                    intChkAllChecked += 1
                    isExist = False
                    strAllID = String.Empty
                    Dim arrID As String() = hdnAllID.Value.Trim.Split(";")
                    For Each strID As String In arrID
                        If intDSFLeasingClaimID.ToString.Trim = strID.Trim Then
                            isExist = True
                            Exit For
                        End If
                    Next
                    If isExist = False Then
                        If hdnAllID.Value.Trim = "" Then
                            hdnAllID.Value = strAllID
                        Else
                            hdnAllID.Value += ";" & strAllID
                        End If
                    End If
                Else
                    strAllID = String.Empty
                    Dim arrID As String() = hdnAllID.Value.Trim.Split(";")
                    For Each strID As String In arrID
                        If intDSFLeasingClaimID.ToString.Trim <> strID.Trim Then
                            If strAllID.Trim = "" Then
                                strAllID = strID.ToString
                            Else
                                strAllID += ";" & strID.ToString
                            End If
                        End If
                    Next
                    hdnAllID.Value = strAllID
                End If
            End If
        Next
    End Sub

    Private Sub dgDSFClaim_ItemCreated(sender As Object, e As DataGridItemEventArgs) Handles dgDSFClaim.ItemCreated
        Dim lit As ListItemType
        Dim cb As CheckBox
        Dim lb As HiddenField

        lit = e.Item.ItemType
        If lit = ListItemType.Item Or lit = ListItemType.AlternatingItem Then
            cb = CType(e.Item.Cells(0).FindControl("chkItemChecked"), CheckBox)
            cb.Text = e.Item.Cells(1).Text
            AddHandler cb.CheckedChanged, New EventHandler(AddressOf OnCheckedChangedEvent)
        End If
    End Sub

    Private Sub dgDSFClaim_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDSFClaim.ItemDataBound
        GenerateToGrid(e)
    End Sub

    Private Sub dgDSFClaim_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDSFClaim.PageIndexChanged
        dgDSFClaim.CurrentPageIndex = e.NewPageIndex
        GetValueFromDB(dgDSFClaim.CurrentPageIndex)
    End Sub

    Private Sub dgDSFClaim_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDSFClaim.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.DESC
        End If

        dgDSFClaim.SelectedIndex = -1
        dgDSFClaim.CurrentPageIndex = 0
        GetValueFromDB(dgDSFClaim.CurrentPageIndex)
    End Sub

    Private Sub BindDDL()
        ''--DropDownList Kategori
        Try
            Dim arrayListCategory As ArrayList = New CategoryFacade(User).RetrieveActiveList()
            Dim blankItem As New ListItem("Silahkan Pilih", 0)

            ddlKategori.Items.Clear()
            ddlKategori.Items.Add(blankItem)
            For Each item As Category In arrayListCategory
                Dim listItem As New ListItem(item.CategoryCode, item.ID)
                listItem.Selected = False
                ddlKategori.Items.Add(listItem)
            Next
            If ddlKategori.Items.Count > 0 Then
                ddlKategori.SelectedIndex = 0
            Else
                ddlKategori.ClearSelection()
            End If
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlKategori, silahkan kirim error ini ke dnet admin")
        End Try
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlTipe, ddlKategori.SelectedItem.Text)

    End Sub

    Private Sub ddlKategori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlKategori.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlTipe, ddlKategori.SelectedItem.Text)
    End Sub

    Private Sub BindDDLPeriod()
        ddlPeriodeMonth.Items.Clear()
        ddlPeriodeMonth.Items.Add(New ListItem("Pilih Bulan", 0))
        For Each item As ListItem In LookUp.ArrayMonth
            ddlPeriodeMonth.Items.Add(item)
        Next

        ddlPeriodeYear.Items.Clear()
        ddlPeriodeYear.Items.Add(New ListItem("Pilih Tahun", 0))
        With ddlPeriodeYear.Items
            Dim _date As Integer = Date.Now.Year - 1
            For i As Integer = 0 To 4 Step 1
                .Add(New ListItem(_date, _date))
                _date = _date + 1
            Next
        End With
    End Sub

    Private Sub GenerateToGrid(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If (e.Item.ItemType = ListItemType.Header) Then
            Dim chkAllItems As CheckBox = CType(e.Item.FindControl("chkAllItems"), CheckBox)
            If Not IsNothing(chkAllItems) Then
                chkAllItems.Attributes.Add("onclick", "CheckAll('chkItemChecked',document.forms[0]." & chkAllItems.ClientID & ".checked)")
            End If
        End If

        If Not IsNothing(e.Item.DataItem) Then
            Dim objDSF As DSFLeasingClaim = CType(e.Item.DataItem, DSFLeasingClaim)
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                If Not objDSF Is Nothing Then
                    Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                    lblNo.Text = (e.Item.ItemIndex + 1 + (dgDSFClaim.CurrentPageIndex * dgDSFClaim.PageSize)).ToString

                    Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                    lblDealerCode.Text = objDSF.Dealer.DealerCode

                    Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
                    lblDealerName.Text = objDSF.Dealer.SearchTerm1

                    Dim lblAgreementNo As Label = CType(e.Item.FindControl("lblAgreementNo"), Label)
                    lblAgreementNo.Text = objDSF.AgreementNo

                    Dim lblRegNumber As Label = CType(e.Item.FindControl("lblRegNumber"), Label)
                    lblRegNumber.Text = objDSF.RegNumber

                    Dim lblChassisNumber As Label = CType(e.Item.FindControl("lblChassisNumber"), Label)
                    lblChassisNumber.Text = objDSF.ChassisMaster.ChassisNumber

                    Dim lblEngineNumber As Label = CType(e.Item.FindControl("lblEngineNumber"), Label)
                    lblEngineNumber.Text = objDSF.ChassisMaster.EngineNumber

                    Dim lblCustomerName As Label = CType(e.Item.FindControl("lblCustomerName"), Label)
                    lblCustomerName.Text = objDSF.CustomerName

                    Dim lblATPMSubsidy As Label = CType(e.Item.FindControl("lblATPMSubsidy"), Label)
                    lblATPMSubsidy.Text = objDSF.ATPMSubsidy.ToString("#,##0")

                    Dim lblSKDNumber As Label = CType(e.Item.FindControl("lblSKDNumber"), Label)
                    lblSKDNumber.Text = objDSF.SKDNumber

                    Dim lblSKDApprovalDate As Label = CType(e.Item.FindControl("lblSKDApprovalDate"), Label)
                    lblSKDApprovalDate.Text = objDSF.SKDApprovalDate.ToString("dd/MM/yyyy")

                    Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                    lblStatus.Text = CommonFunction.GetEnumDescription(objDSF.Status, "EnumDSFClaim.ClaimStatus")

                    Dim strMode As String = "New"
                    Dim criterias2 As New CriteriaComposite(New Criteria(GetType(DSFLeasingClaimDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias2.opAnd(New Criteria(GetType(DSFLeasingClaimDocument), "DSFLeasingClaim.ID", MatchType.Exact, objDSF.ID))
                    Dim arrDSFLeasingClaimDocument As ArrayList = New DSFLeasingClaimDocumentFacade(User).Retrieve(criterias2)
                    If Not IsNothing(arrDSFLeasingClaimDocument) AndAlso arrDSFLeasingClaimDocument.Count > 0 Then
                        strMode = "Edit"
                    End If

                    CType(e.Item.FindControl("lnkbtnUpload"), LinkButton).Attributes.Add("onclick", "showPopUp('../General/../PopUp/PopUpDSFClaimDokumenUpload.aspx?DSFLeasingClaimID=" & objDSF.ID & "&Mode=" & strMode & "', '', 400, 700, isSuccesUpload);")

                    Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)
                    lnkbtnDelete.Visible = False
                    If objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                        If objDSF.Status = 0 Then
                            lnkbtnDelete.Visible = deletePriv
                        Else
                            lnkbtnDelete.Visible = False
                        End If
                    End If

                    Dim lnkbtnUpload As LinkButton = CType(e.Item.FindControl("lnkbtnUpload"), LinkButton)
                    lnkbtnUpload.Visible = False
                    If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        Select Case objDSF.Status
                            ''Only status (2)Konfirmasi, (3)Setuju, (5)Tolak
                            'Case EnumStatusDSFLeasingClaim.Status.Konfirmasi, EnumStatusDSFLeasingClaim.Status.Setuju_by_Dealer, EnumStatusDSFLeasingClaim.Status.Tolak_by_Dealer
                            Case EnumStatusDSFLeasingClaim.Status.Validasi
                                lnkbtnUpload.Visible = uploadPriv
                            Case Else
                                lnkbtnUpload.Visible = False
                        End Select
                    End If

                    Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
                    Dim arrID As String() = hdnAllID.Value.Trim.Split(";")
                    For Each strID As String In arrID
                        If objDSF.ID.ToString = strID Then
                            chkItemChecked.Checked = True
                            OnCheckedChangedEvent(chkItemChecked, e)
                            Exit For
                        End If
                    Next

                    '0 = Baru
                    '1 = Validasi
                    '2 = Konfirmasi
                    '3 = Setuju by Dealer
                    '4 = Selesai
                    '5 = Tolak by Dealer
                    '6 = Batal Validasi
                    '7 = Batal Konfirmasi
                    '8 = SetujuByDSF
                    '9 = RejectByDSF
                    '10 = TolakByMKS

                    chkItemChecked.Visible = False
                    If objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                        'Select Case objDSF.Status
                        '    Case 0, 1, 5
                        '        chkItemChecked.Visible = True
                        '    Case Else
                        '        chkItemChecked.Visible = False
                        'End Select
                    ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                        Select Case objDSF.Status
                            'Case EnumStatusDSFLeasingClaim.Status.Validasi, EnumStatusDSFLeasingClaim.Status.Reject_by_DSF
                            Case EnumStatusDSFLeasingClaim.Status.Validasi
                                chkItemChecked.Visible = True
                            Case Else
                                chkItemChecked.Visible = False
                        End Select
                    ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                        Select Case objDSF.Status
                            Case EnumStatusDSFLeasingClaim.Status.Konfirmasi, EnumStatusDSFLeasingClaim.Status.Tolak_by_Dealer
                                chkItemChecked.Visible = True
                            Case Else
                                chkItemChecked.Visible = False
                        End Select
                    End If

                    Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("DSFFileDirectory")
                    Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
                    Dim DestFullFilePath As String = fileInfo1.Directory.FullName '--& "\" & DestFile '-- Destination file

                    Dim lnkbtnDownloadDealer As LinkButton = CType(e.Item.FindControl("lnkbtnDownloadDealer"), LinkButton)
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(DSFLeasingClaimDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(DSFLeasingClaimDocument), "DSFLeasingClaim.ID", MatchType.Exact, objDSF.ID))
                    criterias.opAnd(New Criteria(GetType(DSFLeasingClaimDocument), "SourceData", MatchType.Exact, 0))
                    Dim arrList As ArrayList = New DSFLeasingClaimDocumentFacade(User).Retrieve(criterias)
                    If arrList.Count > 0 Then
                        Dim _evidence As New DSFLeasingClaimDocument
                        lnkbtnDownloadDealer.Text = String.Empty

                        For Each _evidence In arrList
                            Dim dataFile As String = DestFullFilePath & "\" & _evidence.Path
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lnkbtnDownloadDealer.Text = lnkbtnDownloadDealer.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage(this);"" onmouseover=""ShowEvidenceImage(this);"" title='" & _evidence.FileName & "'> <br>"
                            Else
                                lnkbtnDownloadDealer.Text = lnkbtnDownloadDealer.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);"" title='" & _evidence.FileName & "'> <br>"
                            End If
                            lnkbtnDownloadDealer.Visible = True
                        Next
                    End If

                    Dim lnkbtnDownloadDSF As LinkButton = CType(e.Item.FindControl("lnkbtnDownloadDSF"), LinkButton)
                    Dim criterias3 As New CriteriaComposite(New Criteria(GetType(DSFLeasingClaimDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias3.opAnd(New Criteria(GetType(DSFLeasingClaimDocument), "DSFLeasingClaim.ID", MatchType.Exact, objDSF.ID))
                    criterias3.opAnd(New Criteria(GetType(DSFLeasingClaimDocument), "SourceData", MatchType.Exact, 1))
                    Dim arrList2 As ArrayList = New DSFLeasingClaimDocumentFacade(User).Retrieve(criterias3)
                    If arrList2.Count > 0 Then
                        Dim _evidence As New DSFLeasingClaimDocument
                        lnkbtnDownloadDSF.Text = String.Empty

                        For Each _evidence In arrList2
                            Dim dataFile As String = DestFullFilePath & "\" & _evidence.Path
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lnkbtnDownloadDSF.Text = lnkbtnDownloadDSF.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage(this);"" onmouseover=""ShowEvidenceImage(this);"" title='" & _evidence.FileName & "'> <br>"
                            Else
                                lnkbtnDownloadDSF.Text = lnkbtnDownloadDSF.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);"" title='" & _evidence.FileName & "'> <br>"
                            End If
                            lnkbtnDownloadDSF.Visible = True
                        Next
                    End If

                    ' button History Status
                    Dim lblHistoryStatus As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
                    lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistoryDSF.aspx?DocType=" & LookUp.DocumentType.SalesCampaign_ClaimStatus & "&DocNumber=" & objDSF.RegNumber, "", 370, 400, "DealerSelection")
                    lblHistoryStatus.Visible = historyPriv

                    Dim lblPeriode As Label = CType(e.Item.FindControl("lblPeriode"), Label)
                    If objDSF.CollectionPeriodMonth > 0 AndAlso objDSF.CollectionPeriodYear > 0 Then
                        lblPeriode.Text = enumMonthGet.GetName(objDSF.CollectionPeriodMonth) & " - " & objDSF.CollectionPeriodYear.ToString()
                    Else
                        lblPeriode.Text = ""
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        Dim dtStart As DateTime = icUploadDate.Value
        Dim dtEnd As DateTime = icUploadDateTo.Value
        If dtStart <= dtEnd Then
            If DateDiff(DateInterval.Day, dtStart, dtEnd) > 65 Then
                MessageBox.Show("Periode tanggal tidak boleh lebih besar dari 65 hari")
            Else
                dgDSFClaim.CurrentPageIndex = 0
                GetValueFromDB(dgDSFClaim.CurrentPageIndex)
            End If
        Else
            MessageBox.Show("Tanggal mulai harus lebih kecil atau sama dengan tanggal akhir")
        End If
    End Sub

    Private Function CriteriaForHowManyAlreadyReg(ByVal strRegNumber As String) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, 15))
        criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, strRegNumber))
        criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "NewStatus", MatchType.Exact, CInt(EnumStatusDSFLeasingClaim.Status.Tolak_by_Dealer)))

        Return criterias
    End Function

    Private Function AggreateForCheckRecord() As Aggregate
        Dim aggregates As New Aggregate(GetType(StatusChangeHistory), "id", AggregateType.Count)
        Return aggregates
    End Function

    Private Function IsTolakByDealerStatusMoreThanTwice(ByVal obj As DSFLeasingClaim) As Boolean
        Dim iTotalReg As Integer = New HelperFacade(User, GetType(StatusChangeHistory)).RecordCount(CriteriaForHowManyAlreadyReg(obj.RegNumber), AggreateForCheckRecord())

        If iTotalReg >= 2 Then
            Return True
        End If

        Return False
    End Function

    Private Sub btnUpdateStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateStatus.Click
        Dim strhdnAlasanBatalAll As String = String.Empty
        Dim intStatus As Integer = -1
        Dim sb As StringBuilder = New StringBuilder
        Dim sb2 As StringBuilder = New StringBuilder

        If dgDSFClaim.Items.Count = 0 Then
            MessageBox.Show("Daftar klaim masih kosong")
            Exit Sub
        End If

        Dim checkChecked As Boolean = False
        For i As Integer = 0 To dgDSFClaim.Items.Count - 1
            If CType(dgDSFClaim.Items(i).FindControl("chkItemChecked"), CheckBox).Checked Then
                checkChecked = True
            End If
        Next
        If checkChecked = False Then
            MessageBox.Show("Check list data minimal satu")
            Exit Sub
        End If

        If ddlStatus2.SelectedIndex = 0 Then
            MessageBox.Show("Pilih daftar status terlebih dahulu")
            Exit Sub
        End If

        Dim objDSFLeasingClaim As New DSFLeasingClaim
        Dim objDSFLeasingClaimFacade As DSFLeasingClaimFacade = New DSFLeasingClaimFacade(User)
        Dim intDSFLeasingClaimID As Integer = 0
        Dim arrNewStatus As New ArrayList
        Dim nResult As Integer
        Dim checkCounter As Integer = 0
        Dim checkDeductedCounter As Integer = 0
        Dim arrBenefitClaimDeducted As New ArrayList
        Dim arrBenefitClaimDetail As New ArrayList

        ' Status Report :
        '0 = Baru
        '1 = Validasi
        '2 = Konfirmasi
        '3 = Setuju by Dealer
        '4 = Selesai
        '5 = Tolak by Dealer
        '6 = BatalValidasi
        '7 = BatalKonfirmasi
        '8 = SetujuByDSF
        '9 = RejectByDSF
        '10 = TolakByMKS

        If dgDSFClaim.Items.Count > 0 Then
            sb2.Append("Dengan klik OK, Dealer menyatakan setuju bahwa nilai double klaim untuk nomor klaim Dealer berikut akan mengurangi total nilai klaim Dealer selanjutnya.\n")

            ''For Each item As DataGridItem In dgDSFClaim.Items
            ''Dim chckbox As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            ''If chckbox.Checked Then

            Dim arrID As String() = hdnAllID.Value.Trim.Split(";")
            For Each strID As String In arrID
                intDSFLeasingClaimID = Convert.ToInt32(strID.Trim)
                objDSFLeasingClaim = objDSFLeasingClaimFacade.Retrieve(intDSFLeasingClaimID)

                intStatus = -1
                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    'If ddlStatus2.SelectedValue = 2 Then    '2 = status Konfirmasi
                    '    If objDSFLeasingClaim.Status = 3 Then  'status Setuju
                    '        sb.Append("- Update status [Konfirmasi] gagal untuk Nomor Reg: " & objDSFLeasingClaim.RegNumber & " karena statusnya [Setuju]\n")
                    '        Continue For
                    '    End If
                    'End If
                    'If ddlStatus2.SelectedValue = 7 Then    '7 = status BatalKonfirmasi
                    '    If objDSFLeasingClaim.Status = 1 Then  'status Validasi
                    '        sb.Append("- Update status [Batal Konfirmasi] gagal untuk Nomor Reg: " & objDSFLeasingClaim.RegNumber & " karena statusnya [Validasi]\n")
                    '        Continue For
                    '    End If
                    '    If objDSFLeasingClaim.Status = 3 Then  'status Setuju
                    '        sb.Append("- Update status [Batal Konfirmasi] gagal untuk Nomor Reg: " & objDSFLeasingClaim.RegNumber & " karena statusnya [Setuju]\n")
                    '        Continue For
                    '    End If
                    'End If
                    If ddlStatus2.SelectedValue = EnumStatusDSFLeasingClaim.Status.Selesai Then    'status Selesai
                        If objDSFLeasingClaim.Status <> EnumStatusDSFLeasingClaim.Status.Konfirmasi Then  '2 = status Konfirmasi
                            sb.Append("- Update status [Selesai] gagal untuk Nomor Reg: " & objDSFLeasingClaim.RegNumber & " karena statusnya bukan [Konfirmasi]\n")
                            Continue For
                        End If
                    End If
                    If ddlStatus2.SelectedValue = EnumStatusDSFLeasingClaim.Status.Tolak_by_MKS Then    'status TolakByMKS
                        If objDSFLeasingClaim.Status <> EnumStatusDSFLeasingClaim.Status.Reject_by_DSF Then  '9 = status RejectByDSF
                            sb.Append("- Update status [Tolak By MKS] gagal untuk Nomor Reg: " & objDSFLeasingClaim.RegNumber & " karena statusnya bukan [Reject By DSF]\n")
                            Continue For
                        End If
                    End If
                    If ddlStatus2.SelectedValue = EnumStatusDSFLeasingClaim.Status.Konfirmasi Then    'status Konfirmasi 
                        If objDSFLeasingClaim.Status <> EnumStatusDSFLeasingClaim.Status.Reject_by_DSF Then  '9 = status RejectByDSF
                            If objDSFLeasingClaim.Status = EnumStatusDSFLeasingClaim.Status.Tolak_by_Dealer Then
                                ''Remark request by Desy K
                                'If IsTolakByDealerStatusMoreThanTwice(objDSFLeasingClaim) = False Then
                                '    Dim strWarning As String = "Klaim tidak dapat di ubah status menjadi [" & ddlStatus2.SelectedItem.Text & "], DSF belum melakukan pengajuan banding untuk No Reg: "
                                '    If sb.ToString.IndexOf(strWarning) > 0 Then
                                '        sb.Replace(strWarning, strWarning & objDSFLeasingClaim.RegNumber & ", ")
                                '    Else
                                '        sb.Append("- Klaim tidak dapat di ubah status menjadi [" & ddlStatus2.SelectedItem.Text & "], DSF belum melakukan pengajuan banding untuk No Reg: " & objDSFLeasingClaim.RegNumber & "\n")
                                '    End If
                                '    Continue For
                                'End If
                            Else
                                sb.Append("- Update status [Konfirmasi] gagal untuk Nomor Reg: " & objDSFLeasingClaim.RegNumber & " karena statusnya bukan [Tolak By Dealer]\n")
                                Continue For
                            End If
                        End If
                    End If
                End If

                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    '5 = status Tolak by dealer
                    If ddlStatus2.SelectedValue = EnumStatusDSFLeasingClaim.Status.Tolak_by_Dealer Then
                        If objDSFLeasingClaim.DSFLeasingClaimDocuments.Count = 0 Then
                            sb.Append("- Harap isi dahulu dokumen pendukung untuk Nomor Reg: " & objDSFLeasingClaim.RegNumber & "\n")
                            Continue For
                        End If
                        If objDSFLeasingClaim.RemarkByDealer = "" Then
                            sb.Append("- Harap di isi alasan batal untuk Nomor Reg: " & objDSFLeasingClaim.RegNumber & "\n")
                            Continue For
                        End If
                        If objDSFLeasingClaim.RemarkByDealer = "" Then
                            sb.Append("- Harap di isi alasan batal untuk Nomor Reg: " & objDSFLeasingClaim.RegNumber & "\n")
                            Continue For
                        End If
                    End If
                End If
                'End If

                ''diRemark karna update statusnya dari Sistem DSF saja
                'If objDealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                'If ddlStatus2.SelectedValue = 6 Then    '6 = status Batal Validasi
                '    If objDSFLeasingClaim.Status = 0 Then  '0 = status Baru
                '        sb.Append("- Update status [Batal Validasi] gagal untuk Nomor Reg: " & objDSFLeasingClaim.RegNumber & " karena statusnya [Baru]\n")
                '        Continue For
                '    End If
                'End If
                'If objDSFLeasingClaim.Status <> 5 Then   '5 = Tolak by Dealer   
                '    If ddlStatus2.SelectedValue = 8 Then   '8 = status SetujuByDSF
                '        sb.Append("- Update status [Setuju By DSF] gagal untuk Nomor Reg: " & objDSFLeasingClaim.RegNumber & " karena statusnya bukan [Tolak by Dealer]\n")
                '        Continue For
                '    End If
                '    If ddlStatus2.SelectedValue = 9 Then   '9 = status RejectByDSF
                '        sb.Append("- Update status [Reject By DSF] gagal untuk Nomor Reg: " & objDSFLeasingClaim.RegNumber & " karena statusnya bukan [Tolak by Dealer]\n")
                '        Continue For
                '    End If
                'End If
                'End If

                ''Remark request by Desy K
                'If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                '    If objDSFLeasingClaim.Status = EnumStatusDSFLeasingClaim.Status.Reject_by_DSF OrElse
                '        objDSFLeasingClaim.Status = EnumStatusDSFLeasingClaim.Status.Tolak_by_Dealer Then
                '        If ddlStatus2.SelectedValue = EnumStatusDSFLeasingClaim.Status.Konfirmasi OrElse _
                '            ddlStatus2.SelectedValue = EnumStatusDSFLeasingClaim.Status.Tolak_by_MKS Then
                '            If IsTolakByDealerStatusMoreThanTwice(objDSFLeasingClaim) = False Then
                '                Dim strWarning As String = "Klaim tidak dapat di ubah status menjadi [" & ddlStatus2.SelectedItem.Text & "], DSF belum melakukan pengajuan banding untuk No Reg: "
                '                If sb.ToString.IndexOf(strWarning) > 0 Then
                '                    sb.Replace(strWarning, strWarning & objDSFLeasingClaim.RegNumber & ", ")
                '                Else
                '                    sb.Append("- Klaim tidak dapat di ubah status menjadi [" & ddlStatus2.SelectedItem.Text & "], DSF belum melakukan pengajuan banding untuk No Reg: " & objDSFLeasingClaim.RegNumber & "\n")
                '                End If
                '                Continue For
                '            End If
                '        End If
                '    End If
                'End If

                'status Setuju & Konfirmasi
                If ddlStatus2.SelectedValue = EnumStatusDSFLeasingClaim.Status.Setuju_by_Dealer OrElse _
                    ddlStatus2.SelectedValue = EnumStatusDSFLeasingClaim.Status.Konfirmasi Then
                    Dim strClaimRegNo As String = String.Empty
                    Dim dblAmount As Double = 0
                    Dim intStatusClaim As Integer = -1

                    Dim strValConfig As String = String.Empty
                    Dim objAppConfigFacade As AppConfigFacade = New AppConfigFacade(User)
                    Dim objAppConfig As AppConfig = objAppConfigFacade.Retrieve("SalesCampaignDeductedBenefitType")
                    If Not IsNothing(objAppConfig) Then
                        strValConfig = objAppConfig.Value
                    End If

                    Dim objBenefitClaimDeducted As New BenefitClaimDeducted
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "ChassisMaster.ID", MatchType.Exact, objDSFLeasingClaim.ChassisMaster.ID))
                    criterias.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitClaimHeader.BenefitType.ID", MatchType.InSet, "(" & strValConfig.ToString().Replace(";", ",") & ")"))
                    Dim arrBenefitClaimDetails As ArrayList = New BenefitClaimDetailsFacade(User).Retrieve(criterias)
                    If Not IsNothing(arrBenefitClaimDetails) AndAlso arrBenefitClaimDetails.Count > 0 Then
                        Dim objBenefitClaimDetails As BenefitClaimDetails = CType(arrBenefitClaimDetails(0), BenefitClaimDetails)
                        strClaimRegNo = objBenefitClaimDetails.BenefitClaimHeader.ClaimRegNo
                        Dim strChassisNumber As String = objBenefitClaimDetails.ChassisMaster.ChassisNumber
                        dblAmount = objBenefitClaimDetails.BenefitMasterDetail.Amount
                        intStatusClaim = objBenefitClaimDetails.BenefitClaimHeader.Status

                        Select Case intStatusClaim
                            Case 2, 4, 5    '--- (2)Konfirmasi, (4)Proses, (5)Selesai
                                Dim AmountPPHandPPN As Decimal = CalculateAmountPPHandPPN(objBenefitClaimDetails.BenefitClaimHeader, dblAmount)

                                objBenefitClaimDeducted.DSFLeasingClaim = objDSFLeasingClaim
                                objBenefitClaimDeducted.BenefitClaimHeader = objBenefitClaimDetails.BenefitClaimHeader
                                objBenefitClaimDeducted.DeductedAmount = AmountPPHandPPN
                                objBenefitClaimDeducted.RemainAmount = AmountPPHandPPN
                                arrBenefitClaimDeducted.Add(objBenefitClaimDeducted)

                                sb2.Append("- Double Claim untuk : " & strClaimRegNo & " No. Rangka " & strChassisNumber & ", maka kwitansi cashback selanjutnya akan berkurang senilai " & AmountPPHandPPN.ToString("#,##0") & "\n")
                                'sb2.Append("- Nomor klaim " & strClaimRegNo & " senilai " & AmountPPHandPPN.ToString("#,##0") & "\n")
                                checkDeductedCounter = checkDeductedCounter + 1

                            Case 0, 1
                                objBenefitClaimDetails.DetailStatus = 2       '-- (3)status Tolak
                                arrBenefitClaimDetail.Add(objBenefitClaimDetails)
                            Case Else
                        End Select
                    End If
                End If

                Select Case ddlStatus2.SelectedValue
                    Case EnumStatusDSFLeasingClaim.Status.Batal_Validasi     '-- Batal Validasi
                        intStatus = EnumStatusDSFLeasingClaim.Status.Baru   '--Baru
                    Case EnumStatusDSFLeasingClaim.Status.Batal_Konfirmasi      '-- Batal Konfirmasi
                        intStatus = EnumStatusDSFLeasingClaim.Status.Validasi   '--Validasi
                    Case EnumStatusDSFLeasingClaim.Status.Setuju_by_Dealer     '-- Setuju By Dealer
                        intStatus = EnumStatusDSFLeasingClaim.Status.Konfirmasi   '--Konfirmasi
                    Case Else
                        intStatus = ddlStatus2.SelectedValue
                End Select

                hdnStatusOld.Value = objDSFLeasingClaim.Status

                objDSFLeasingClaim.Status = intStatus
                arrNewStatus.Add(objDSFLeasingClaim)

                checkCounter = checkCounter + 1
                'End If
            Next
        End If

        sessHelper.SetSession("arrBenefitClaimDetail", arrBenefitClaimDetail)
        sessHelper.SetSession("arrBenefitClaimDeducted", arrBenefitClaimDeducted)
        sessHelper.SetSession("checkCounter", checkCounter)
        sessHelper.SetSession("checkDeductedCounter", checkDeductedCounter)
        sessHelper.SetSession("arrNewStatus", arrNewStatus)
        sessHelper.SetSession("sb", sb)
        sessHelper.SetSession("sb2", sb2)

        If arrBenefitClaimDeducted.Count = 0 Then
            arrBenefitClaimDeducted = Nothing
        End If
        If arrBenefitClaimDetail.Count = 0 Then
            arrBenefitClaimDetail = Nothing
        End If

        ContinueUpdateStatus()
    End Sub

    Private Sub ContinueUpdateStatus()
        Dim checkCounter As Integer = CType(sessHelper.GetSession("checkCounter"), Integer)
        Dim checkDeductedCounter As Integer = CType(sessHelper.GetSession("checkDeductedCounter"), Integer)
        Dim arrNewStatus As ArrayList = CType(sessHelper.GetSession("arrNewStatus"), ArrayList)
        Dim sb As StringBuilder = CType(sessHelper.GetSession("sb"), StringBuilder)
        Dim sb2 As StringBuilder = CType(sessHelper.GetSession("sb2"), StringBuilder)
        Dim arrBenefitClaimDeducted2 As New ArrayList
        Dim arrBenefitClaimDetail As ArrayList = CType(sessHelper.GetSession("arrBenefitClaimDetail"), ArrayList)

        Dim _result As Integer = 0
        If (checkCounter > 0) Then
            If checkDeductedCounter > 0 Then
                If (hdnValNew.Value = "-2") Then
                    If (sb2.ToString().Length > 0) Then
                        ViewState("wrn") = "yes"
                        MessageBox.Confirm(sb2.ToString(), "hdnValNew")
                        Return
                    End If
                ElseIf (hdnValNew.Value = "1") Then
                    If Not IsNothing(sessHelper.GetSession("arrBenefitClaimDeducted")) Then
                        arrBenefitClaimDeducted2 = CType(sessHelper.GetSession("arrBenefitClaimDeducted"), ArrayList)
                    End If
                End If
            End If

            If (sb.ToString().Length > 0) Then
                MessageBox.Show(sb.ToString())
            End If

            _result = New DSFLeasingClaimFacade(User).UpdateStatusTransaction(arrNewStatus, arrBenefitClaimDeducted2, arrBenefitClaimDetail)
            If _result <> -1 Then
                RecordStatusChangeHistory(arrNewStatus, ddlStatus2.SelectedValue)
                MessageBox.Show("Proses update status [" & ddlStatus2.SelectedItem.Text & "] sukses")
                hdnAllID.Value = ""
            Else
                MessageBox.Show("Proses update status gagal")
            End If
        Else
            If (sb.ToString().Length > 0) Then
                MessageBox.Show(sb.ToString())
            Else
                MessageBox.Show("Silahkan pilih item daftar")
            End If
        End If

        ddlStatus2.ClearSelection()
        ddlStatus2.SelectedIndex = 0
        InitializeFormUpdateStatus()
        GetValueFromDB(dgDSFClaim.CurrentPageIndex)
    End Sub

    Private Sub RecordStatusChangeHistory(ByVal arrList As ArrayList, ByVal newStatus As Integer)
        For Each item As DSFLeasingClaim In arrList
            If Not IsStatusExist(item, newStatus) Then
                Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.SalesCampaign_ClaimStatus), item.RegNumber, hdnStatusOld.Value, newStatus)
            End If
        Next
    End Sub

    Private Function IsStatusExist(ByVal _dSFLeasingClaim As DSFLeasingClaim, ByVal newStatus As Integer) As Boolean
        Try
            Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, CInt(LookUp.DocumentType.SalesCampaign_ClaimStatus)))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, _dSFLeasingClaim.RegNumber))
            criterias.opAnd(New Criteria(GetType(StatusChangeHistory), "NewStatus", MatchType.Exact, newStatus))
            Dim arlHistStatus As ArrayList = objStatusChangeHistoryFacade.Retrieve(criterias)
            If Not IsNothing(arlHistStatus) AndAlso arlHistStatus.Count > 0 Then
                If newStatus = 5 Then 'status Tolak by Dealer
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CalculateAmountPPHandPPN(objBenefitClaimHeader As BenefitClaimHeader, ByVal Amount As Decimal) As Decimal
        Dim total As Decimal = Amount
        Dim temptotal As Decimal = 0

        Dim cutOffDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        For Each el As BenefitClaimReceipt In objBenefitClaimHeader.BenefitClaimReceipts
            cutOffDate = el.ReceiptDate
        Next

        If cutOffDate.Year < 1900 Then
            cutOffDate = objBenefitClaimHeader.ClaimDate
        End If

        Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(cutOffDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_1").ValueId)
        Dim ppn As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(cutOffDate, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPN").ValueId)

        temptotal = total

        Dim nilaiPph As Decimal = 0
        If Not IsNothing(total) OrElse CInt(total) > 0 Then
            If objBenefitClaimHeader.BenefitType.LeasingBox = 1 Then
                'nilaiPph = Math.Round(((temptotal / (1 - 0.15)) - temptotal))
                'total = total + nilaiPph
                'total = Math.Round(total + (0.1 * (nilaiPph + temptotal)))

                nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.Total, pph, total:=total)
                total = CalcHelper.DPPCalculation(pph, total)
                total += CalcHelper.PPNCalculation(CalcSourceTypeEnum.DPP, ppn, dpp:=total)
            Else
                'nilaiPph = Math.Round(((temptotal / (1 - 0.15)) - temptotal))
                'nilaiPph = Math.Round(0.15 * temptotal)
                'total = Math.Round(total + (0.1 * temptotal))

                nilaiPph = CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, dpp:=total)
                total += CalcHelper.PPNCalculation(CalcSourceTypeEnum.Total, ppn, total:=total)
            End If
        End If
        'If Not IsNothing(total) OrElse CInt(total) > 0 Then
        '    total = Math.Round(total + (0.1 * (nilaiPph + temptotal)))
        'End If

        Return total
    End Function

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim arlData As ArrayList = New ArrayList
        If Not IsNothing(sessHelper.GetSession("FrmDaftarDSFLeasingClaim_Download")) Then
            arlData = CType(sessHelper.GetSession("FrmDaftarDSFLeasingClaim_Download"), ArrayList)
        Else
            MessageBox.Show("Tidak ada data yang akan di download")
            Exit Sub
        End If
        If arlData.Count = 0 Then
            MessageBox.Show("Tidak ada data yang akan di download")
            Exit Sub
        End If

        Dim sFileName As String = "DSFLeasingClaim" & "_" & DateTime.Now.ToString("yyyyMMddhhmmss")
        Dim DSFLeasingClaim As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(DSFLeasingClaim)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                Dim fs As FileStream = New FileStream(DSFLeasingClaim, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)

                WriteDownloadDSFData(sw, arlData)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show(ex.Message) '"DownloadData data gagal")
        End Try

    End Sub

    Private Sub WriteDownloadDSFData(ByRef sw As StreamWriter, ByVal arlData As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder  '-- SPPO line in file

        If arlData.Count > 0 Then
            '-- Write column header
            itemLine.Remove(0, itemLine.Length)     '-- Empty line
            itemLine.Append("No." & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Status" & tab)
            itemLine.Append("Nomor Registrasi" & tab)
            itemLine.Append("Nomor Rangka" & tab)
            itemLine.Append("Nomor Mesin" & tab)
            itemLine.Append("Tanggal Klaim" & tab)
            itemLine.Append("Nomor Agreement" & tab)
            itemLine.Append("Nomor SKD" & tab)
            itemLine.Append("Nama Konsumen" & tab)
            itemLine.Append("Unit" & tab)
            itemLine.Append("Object Lease" & tab)
            itemLine.Append("Tahun Produksi" & tab)
            itemLine.Append("ATPM Subsidi" & tab)
            itemLine.Append("Nama Supplier" & tab)
            itemLine.Append("Nama Program" & tab)
            itemLine.Append("Total Down Payment" & tab)
            itemLine.Append("Total Amount Lease" & tab)
            itemLine.Append("Period Lease" & tab)
            itemLine.Append("% Interest Lease" & tab)
            itemLine.Append("Insurance" & tab)
            itemLine.Append("Type Insurance" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim no As Integer = 0
            For Each item As DSFLeasingClaim In arlData
                itemLine.Remove(0, itemLine.Length)  '-- Empty line
                itemLine.Append((no + 1).ToString() & tab)
                itemLine.Append(item.Dealer.DealerCode & tab)
                itemLine.Append(item.Dealer.SearchTerm1.ToString().Replace(",", ".") & tab)
                itemLine.Append(CommonFunction.GetEnumDescription(item.Status, "EnumDSFClaim.ClaimStatus") & tab)
                itemLine.Append(item.RegNumber & tab)
                itemLine.Append(item.ChassisMaster.ChassisNumber & tab)
                itemLine.Append(item.ChassisMaster.EngineNumber & tab)
                itemLine.Append(item.ClaimDate.ToString("dd/MM/yyyy") & tab)
                itemLine.Append(item.AgreementNo & tab)
                itemLine.Append(item.SKDNumber & tab)
                itemLine.Append(item.CustomerName & tab)
                itemLine.Append(item.Unit & tab)
                itemLine.Append(item.ObjectLease & tab)
                itemLine.Append(item.ChassisMaster.ProductionYear & tab)
                itemLine.Append(item.ATPMSubsidy.ToString("#,##0") & tab)
                itemLine.Append(item.SupplierName & tab)
                itemLine.Append(item.ProgramName & tab)
                itemLine.Append(item.TotalDP.ToString("#,##0") & tab)
                itemLine.Append(item.TotalAmountLease.ToString("#,##0") & tab)
                itemLine.Append(item.PeriodLease & tab)
                itemLine.Append(item.InterestLease & tab)
                itemLine.Append(item.Insurance & tab)
                itemLine.Append(item.TypeInsurance & tab)

                sw.WriteLine(itemLine.ToString())
                no = no + 1
            Next

        End If
    End Sub

    Private Sub btnSuccessUpload_Click(sender As Object, e As EventArgs) Handles btnSuccessUpload.Click
        Dim lngDSFLeasingClaimID As String = hdnDSFLeasingClaimID.Value
        Dim strAlasanBatal As String = hdnAlasanBatal.Value
        Dim strAlasanBatalAll As String = String.Empty
        Dim strhdnAlasanBatalAll As String = String.Empty

        strhdnAlasanBatalAll = sessHelper.GetSession("FrmDaftarDSFLeasingClaim.sessAlasanBatalAll")
        strhdnAlasanBatalAll = IIf(IsNothing(strhdnAlasanBatalAll), "", strhdnAlasanBatalAll)

        If strhdnAlasanBatalAll <> "" Then
            Dim strSplit1() As String = strhdnAlasanBatalAll.Split(";")
            If strSplit1.Count > 0 Then
                Dim strSplit2() As String
                For Each str As String In strSplit1
                    strSplit2 = str.Split("|")
                    If strSplit2(0) = lngDSFLeasingClaimID Then
                        If strAlasanBatalAll = "" Then
                            strAlasanBatalAll = lngDSFLeasingClaimID & "|" & strAlasanBatal
                        Else
                            strAlasanBatalAll += ";" & lngDSFLeasingClaimID & "|" & strAlasanBatal
                        End If
                    Else
                        If strAlasanBatalAll = "" Then
                            strAlasanBatalAll = strSplit2(0) & "|" & strSplit2(1)
                        Else
                            strAlasanBatalAll += ";" & strSplit2(0) & "|" & strSplit2(1)
                        End If
                    End If
                Next
                sessHelper.SetSession("FrmDaftarDSFLeasingClaim.sessAlasanBatalAll", strAlasanBatalAll)

            Else

            End If
        Else
            sessHelper.SetSession("FrmDaftarDSFLeasingClaim.sessAlasanBatalAll", lngDSFLeasingClaimID & "|" & strAlasanBatal)
        End If

        hdnDSFLeasingClaimID.Value = ""
        hdnAlasanBatal.Value = ""
        GetValueFromDB(dgDSFClaim.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        txtKodeDealer.Text = ""
        lblDealerName.Text = ""
        txtRegNumber.Text = ""
        txtChassisNumber.Text = ""
        txtAgreementNo.Text = ""
        chkTglUpload.Checked = False
        icUploadDate.Value = DateTime.Now
        icUploadDateTo.Value = DateTime.Now
        chkValidateTime.Checked = False
        icValidateTimeFrom.Value = DateTime.Now
        icValidateTimeTo.Value = DateTime.Now
        ddlStatus.SelectedIndex = 0
        hdnAllID.Value = ""
        cbDate.Checked = False
        ddlPeriodeMonth.SelectedValue = Now.Month
        ddlPeriodeYear.SelectedValue = Now.Year
        ddlKategori.SelectedIndex = 0
        ddlTipe.SelectedIndex = 0

        SetSessionCriteria()
        dgDSFClaim.CurrentPageIndex = 0
        GetValueFromDB(dgDSFClaim.CurrentPageIndex)
    End Sub

End Class