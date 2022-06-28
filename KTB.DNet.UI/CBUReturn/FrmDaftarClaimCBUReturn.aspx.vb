Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessValidation

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Collections.Generic
Imports OfficeOpenXml
Imports System.Linq
Imports GlobalExtensions


Public Class FrmDaftarClaimCBUReturn
    Inherits System.Web.UI.Page
    Private sessHelper As New SessionHelper
    Private oDealer As Dealer
    Private SessionCriteriaCBU = "FrmDaftarClaimCBUReturn.CriteriaList"
    Private SessionWSMData = "FrmDaftarClaimCBUReturn.WSMData"
    Private arlCheckedItemColl As ArrayList = New ArrayList
    Dim KTBID As String = "000002"
    Private Const confBlockFakturDesc As String = "ChassisMasterClaim.BlockFakturDesc"
    Dim ChassisMFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
    Dim objAppConfFacade As AppConfigFacade = New AppConfigFacade(User)

    Private m_bLihatPrivilege As Boolean = False
    Private m_bUbahVCDPrivilage As Boolean = False
    Private m_bUbahRetailPrivilage As Boolean = False
    Private ChassisList As New ArrayList

    Private templateDir As String
    Private Const CBUReturnEmailToRSD As String = "CBUReturnEmailToRSD"
    Private Const CBUReturnEmailToWSD As String = "CBUReturnEmailToWSD"
    Private Const CBUReturnEmailToVCD As String = "CBUReturnEmailToVCD"
    Private Const EmailBCC As String = "EmailBCC"

    Private Sub CheckPrivilege()
        m_bLihatPrivilege = SecurityProvider.Authorize(Context.User, SR.CBUReturn_DaftarClaim_View_Privilage)
        m_bUbahVCDPrivilage = SecurityProvider.Authorize(Context.User, SR.CBUReturn_VCD_Ubah_Privilage)
        m_bUbahRetailPrivilage = SecurityProvider.Authorize(Context.User, SR.CBUReturn_Retail_Ubah_Privilage)
        If Not m_bLihatPrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Klaim Pengembalian Kendaraan - Daftar Claim")
        End If
    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        templateDir = Server.MapPath("~/DataFile/EmailTemplate/")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        CheckPrivilege()

        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("IsBack")) Then
                ViewState("Back") = True
            End If
            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirection") = Sort.SortDirection.DESC
            rdoDealerAlokasi.Checked = True
            If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                'ddlKonfirmasi.Visible = False
                'btnProcess.Visible = False
                '    DealerControl()
                'Else
                '    'lnkBtnPopUpDealer.Visible = True
                '    lnkBtnPopUpDealer2.Visible = True
                '    'lnkBtnPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
                '    lblDealerName.Visible = False
                '    'lblKodeDealer.Visible = False
                btnProcessReturn.Visible = False
                ReTransfer.Visible = False
            Else
                ddlKonfirmasi.Visible = m_bUbahVCDPrivilage
                btnProcess.Visible = m_bUbahVCDPrivilage
                ReTransfer.Visible = m_bUbahRetailPrivilage
            End If
            lnkBtnPopUpDealer1.Attributes("onclick") = "ShowPopUpDealer1()"
            lnkBtnPopUpDealer2.Attributes("onclick") = "ShowPopUpDealer2()"
            BindDDL()
            BindGrid(0, True)
            icPeriodeStart.Value = Date.Now.AddDays(-90)

            sessHelper.SetSession(SessionWSMData, New ArrayList)
        End If

        Dim sHH As New SessionHelper
        If Request.Form("hdnValActive") = "1" Then
            Dim showPwd As Boolean = True
            If Not IsNothing(sHH.GetSession("NotShowPassword")) Then
                showPwd = Not CType(sHH.GetSession("NotShowPassword"), Boolean)
            End If

            If Me.txtPass.Text = String.Empty And showPwd Then
                RegisterStartupScript("OpenWindow", "<script>showPPPassword();</script>")
                Exit Sub
            Else
                sHH.SetSession("Confirm", True)
            End If
            sHH.SetSession("NotShowPassword", False)
            btnProcessReturn_Click(Nothing, Nothing)
            hdnValActive.Value = "-1"
        ElseIf Request.Form("hdnValActive") = "0" Then
            hdnValActive.Value = "-1"
        End If

        cbDate.Visible = False
        cbDate.Checked = True
        txtDealerName.Attributes("ReadOnly") = True
        txtDealerAlokasi.Attributes("ReadOnly") = True

    End Sub

    Private Sub DealerControl()
        'txtKodeDealer.Visible = False
        txtDealerName.Visible = False
        'lblKodeDealer.Text = oDealer.DealerCode
    End Sub


    Private Sub BindDDL()
        ddlKonfirmasi.Items.Clear()
        ddlKonfirmasi.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            ddlKonfirmasi.Items.Add(New ListItem("Batal Validasi", EnumCBUReturn.StatusClaim.Baru))
            ddlKonfirmasi.Items.Add(New ListItem("Validasi", EnumCBUReturn.StatusClaim.Validasi))
        Else
            ddlKonfirmasi.Items.Add(New ListItem("Konfirmasi", EnumCBUReturn.StatusClaim.Konfirmasi))
            'Request Mas BEN 6112020
            'ddlKonfirmasi.Items.Add(New ListItem("Revisi", EnumCBUReturn.StatusClaim.Revisi))
            'ddlKonfirmasi.Items.Add(New ListItem("Tolak", EnumCBUReturn.StatusClaim.Tolak))
            ddlKonfirmasi.Items.Add(New ListItem("Send To SAP", EnumCBUReturn.StatusClaim.Proses))
        End If

        ddlResponClaim.Items.Clear()
        Dim responClaimData As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("ChassisMasterClaim.RespondClaim")
        ddlResponClaim.DataSource = responClaimData
        ddlResponClaim.DataTextField = "ValueDesc"
        ddlResponClaim.DataValueField = "ValueId"
        ddlResponClaim.DataBind()
        ddlResponClaim.Items.Insert(0, New ListItem("Silahkan Pilih", 0))

        ddlStatusClaim.Items.Clear()
        Dim statusClaimData As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("ChassisMasterClaim.StatusClaim")
        ddlStatusClaim.DataSource = statusClaimData
        ddlStatusClaim.DataTextField = "ValueDesc"
        ddlStatusClaim.DataValueField = "ValueId"
        ddlStatusClaim.DataBind()
        ddlStatusClaim.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))

        ddlStatusProsesReturn.Items.Clear()
        Dim statusProsesReturnData As ArrayList = New StandardCodeFacade(User).RetrieveByCategory("ChassisMasterClaim.StatusProsesRetur")
        ddlStatusProsesReturn.DataSource = (From ObjES As StandardCode In statusProsesReturnData
                                            Where ObjES.ValueId <> 0
                                            Order By ObjES.ValueId
                                            Select ObjES).ToList()
        ddlStatusProsesReturn.DataTextField = "ValueDesc"
        ddlStatusProsesReturn.DataValueField = "ValueId"
        ddlStatusProsesReturn.DataBind()
        ddlStatusProsesReturn.Items.Insert(0, New ListItem("Silahkan Pilih", 0))


        ddlKonfirmasi.SelectedIndex = 0
        ddlResponClaim.SelectedIndex = 0
        ddlStatusClaim.SelectedIndex = 0
        ddlStatusProsesReturn.SelectedIndex = 0
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer, Optional firstTime As Boolean = False)
        Dim total As Integer = 0
        Dim crit As CriteriaComposite
        If Not IsNothing(ViewState("Back")) Then
            'crit = sessHelper.GetSession("criteriadownload")
            ReadCriteria()
            crit = SearchCriteria()
            If firstTime Then
                'cari priv

                'bkin crit
            End If
            ViewState.Remove("Back")
        Else
            crit = SearchCriteria()
        End If

        If Not IsNothing(CType(sessHelper.GetSession("FrmDaftarClaimCBUReturn"), ArrayList)) Then
            sessHelper.SetSession("_PgIdxBefore", CType(sessHelper.GetSession("_PgIdxNext"), Integer))
            sessHelper.SetSession("_PgIdxNext", pageIndex)

            arlCheckedItemColl = GetCheckedItem()
            Dim currentPage As String = CType(sessHelper.GetSession("_PgIdxBefore"), String)
            sessHelper.SetSession("CMCHProcess" + currentPage, arlCheckedItemColl)

            Dim arlUnCheckedItemColl As ArrayList = GetUnCheckedItem()
            currentPage = CType(sessHelper.GetSession("_PgIdxBefore"), String)
            sessHelper.SetSession("CMCHProcess2" + currentPage, arlUnCheckedItemColl)
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(ChassisMasterClaimHeader), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        Dim listSource As ArrayList = New ChassisMasterClaimHeaderFacade(User).Retrieve(crit, sortColl)
        'Dim listSource As ArrayList = New ChassisMasterClaimHeaderFacade(User).RetrieveActiveList(crit, pageIndex + 1, dgCBUList.PageSize, _
        'total, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If listSource.Count <> 0 Then
            CommonFunction.SortListControl(listSource, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(listSource, pageIndex, dgCBUList.PageSize)
            sessHelper.SetSession("FrmDaftarClaimCBUReturn", PagedList)
            dgCBUList.CurrentPageIndex = pageIndex
            dgCBUList.DataSource = PagedList
            dgCBUList.VirtualItemCount = listSource.Count
            dgCBUList.DataBind()
        Else
            dgCBUList.DataSource = New ArrayList
            dgCBUList.VirtualItemCount = 0
            dgCBUList.CurrentPageIndex = 0
            dgCBUList.DataBind()
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("txtNoClaim", txtNoClaim.Text)
        'crit.Add("txtKodeDealer", txtKodeDealer.Text)
        crit.Add("txtDealerName", txtDealerName.Text)
        crit.Add("calClaimStart", icPeriodeStart.Value)
        crit.Add("calClaimEnd", icPeriodeEnd.Value)
        crit.Add("ddlStatusClaim", ddlStatusClaim.SelectedValue)
        crit.Add("txtNoChassis", txtNoChassis.Text)
        crit.Add("ddlResponClaim", ddlResponClaim.SelectedValue)
        crit.Add("ddlStatusProsesReturn", ddlStatusProsesReturn.SelectedValue)
        crit.Add("PeriodeStart", icPeriodeStart.Value)
        crit.Add("PeriodeEnd", icPeriodeEnd.Value)

        crit.Add("PageIndex", dgCBUList.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sessHelper.SetSession(SessionCriteriaCBU, crit)  '-- Store in session
    End Sub

    Private Sub ReadCriteria()
        Dim crit As Hashtable
        crit = CType(sessHelper.GetSession(SessionCriteriaCBU), Hashtable)
        If Not crit Is Nothing Then
            txtNoClaim.Text = CStr(crit.Item("txtNoClaim"))
            'txtKodeDealer.Text = CStr(crit.Item("txtKodeDealer"))
            txtDealerName.Text = CStr(crit.Item("txtDealerName"))
            icPeriodeStart.Value = CStr(crit.Item("calClaimStart"))
            icPeriodeEnd.Value = CStr(crit.Item("calClaimEnd"))
            ddlStatusClaim.SelectedValue = CStr(crit.Item("ddlStatusClaim"))
            txtNoChassis.Text = CStr(crit.Item("txtNoChassis"))
            ddlResponClaim.SelectedValue = CStr(crit.Item("ddlResponClaim"))
            ddlStatusProsesReturn.SelectedValue = CStr(crit.Item("ddlStatusProsesReturn"))
            icPeriodeStart.Value = CStr(crit.Item("PeriodeStart"))
            icPeriodeEnd.Value = CStr(crit.Item("PeriodeEnd"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgCBUList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Function SearchCriteria() As CriteriaComposite
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtNoClaim.Text.Trim.Length > 0 Then
            criteria.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "ClaimNumber", MatchType.[Partial], txtNoClaim.Text.Trim))
        End If

        If rdoDealerClaim.Checked Then
            If txtDealerName.Text.Trim.Length > 0 Then
                criteria.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerName.Text.Replace(";", "','").Trim.ToString() & "')"))
            Else
                If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                    criteria.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "Dealer.DealerGroup.DealerGroupCode", MatchType.Exact, oDealer.DealerGroup.DealerGroupCode))
                End If
            End If
        End If

        If rdoDealerAlokasi.Checked Then
            If txtDealerAlokasi.Text.Trim.Length > 0 Then
                criteria.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "ChassisMaster.Dealer.DealerCode", MatchType.InSet, "('" & txtDealerAlokasi.Text.Replace(";", "','").Trim.ToString() & "')"))
            Else
                If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                    criteria.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "ChassisMaster.Dealer.DealerGroup.DealerGroupCode", MatchType.Exact, oDealer.DealerGroup.DealerGroupCode))
                End If
            End If
        End If

        If ddlStatusClaim.SelectedIndex <> 0 Then
            criteria.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "StatusID", MatchType.Exact, ddlStatusClaim.SelectedValue))
        End If
        If txtNoChassis.Text.Trim.Length > 0 Then
            criteria.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "ChassisMaster.ChassisNumber", MatchType.[Partial], txtNoChassis.Text.Trim))
        End If
        If ddlResponClaim.SelectedIndex <> 0 Then
            criteria.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "ResponClaim", MatchType.Exact, ddlResponClaim.SelectedValue))
        End If
        If ddlStatusProsesReturn.SelectedIndex <> 0 Then
            criteria.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "StatusProcessRetur", MatchType.Exact, ddlStatusProsesReturn.SelectedValue))
        End If

        If cbDate.Checked Then
            Dim tglFrom As New Date(icPeriodeStart.Value.Year, icPeriodeStart.Value.Month, icPeriodeStart.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icPeriodeEnd.Value.Year, icPeriodeEnd.Value.Month, icPeriodeEnd.Value.Day, 23, 59, 59)

            criteria.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "CreatedTime", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "(", True)
            criteria.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "CreatedTime", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), ")", False)
        End If
        sessHelper.SetSession("criteriadownload", criteria)
        Return criteria
    End Function

    Private Function GetCheckedItemAllPages() As ArrayList
        Dim arlCheckedItemAllPages As ArrayList = New ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession("FrmDaftarClaimCBUReturn"), ArrayList)
        Dim intPageCount As Integer = 0
        If Not IsNothing(ViewState.Item("GridPageCount")) Then
            intPageCount = CType(ViewState.Item("GridPageCount"), Integer)
        End If
        If Not IsNothing(Request.QueryString("GridPageCount")) AndAlso Not (Request.QueryString("GridPageCount") = "") Then
            intPageCount = CType(Request.QueryString("GridPageCount"), Integer)
            ViewState.Add("GridPageCount", intPageCount)
        End If
        Dim nGridCount As Integer = intPageCount - 1
        For idx As Integer = 0 To nGridCount
            Dim currentPage As String = CType(idx, String)
            Dim arrGrid2 As ArrayList = CType(sessHelper.GetSession("CMCHProcess" + currentPage), ArrayList)
            If Not IsNothing(arrGrid2) Then
                For i As Integer = 0 To arrGrid2.Count - 1
                    arlCheckedItemAllPages.Add(arrGrid2(i))
                Next i
            End If
        Next
        Return arlCheckedItemAllPages

    End Function

    Private Function GetCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession("FrmDaftarClaimCBUReturn"), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgCBUList.Items
            nIndeks = dtgItem.ItemIndex
            Dim objCM As ChassisMasterClaimHeader = CType(arrGrid(nIndeks), ChassisMasterClaimHeader)
            If CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                arlCheckedItem.Add(objCM)
            End If
        Next

        Return arlCheckedItem
    End Function

    Private Function GetUnCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession("FrmDaftarClaimCBUReturn"), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgCBUList.Items
            nIndeks = dtgItem.ItemIndex
            Dim objCM As ChassisMasterClaimHeader = CType(arrGrid(nIndeks), ChassisMasterClaimHeader)
            If Not CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                arlCheckedItem.Add(objCM)
            End If
        Next

        Return arlCheckedItem
    End Function

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If cbDate.Checked Then
            Dim diff As Integer = DateDiff(DateInterval.Day, icPeriodeStart.Value, icPeriodeEnd.Value)
            If diff > 90 Then
                MessageBox.Show("Periode pencarian maksimal 90 hari")
            End If
        End If
        BindGrid(0)
        StoreCriteria()
        If dgCBUList.Items.Count = 0 Then
            MessageBox.Show("Data list tidak ditemukan")
        End If
    End Sub

    Protected Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Dim arlBH As New ArrayList
        Dim arlCM As New ArrayList
        Dim ok As Boolean = True
        If ddlKonfirmasi.SelectedIndex = 0 Then
            MessageBox.Show("Pilih status terlebih dahulu")
            Exit Sub
        End If

        For Each dgItem As DataGridItem In dgCBUList.Items
            If CType(dgItem.FindControl("chkSelect"), CheckBox).Checked Then
                Dim objCMCH As ChassisMasterClaimHeader = New ChassisMasterClaimHeaderFacade(User).Retrieve(CType(dgItem.Cells(0).Text, Integer))
                If objCMCH.CreatedBy.Contains(KTBID) AndAlso ddlKonfirmasi.SelectedValue = EnumCBUReturn.StatusClaim.Revisi Then
                    MessageBox.Show("Tidak dapat memproses Claim " & objCMCH.ClaimNumber)
                    arlCM = New ArrayList
                    Exit Sub
                End If

                If objCMCH.IsDMSClaim AndAlso oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                    MessageBox.Show("Claim " & objCMCH.ClaimNumber & " hanya dapat diproses di DMS")
                    Exit Sub
                End If

                Dim result As String = String.Empty
                If ddlKonfirmasi.SelectedValue = EnumCBUReturn.StatusClaim.Proses Then
                    If String.IsNullorEmpty(objCMCH.ChassisNumberReplacement) Then
                        MessageBox.Show(String.Format("Chassis Pengganti masih kosong, mohon untuk diisi terlebihi dahulu untuk claim {0}", objCMCH.ClaimNumber))
                        Exit Sub
                    ElseIf Not CBUReturnValidation.IsValidChassisReplacement(objCMCH, result) Then
                        MessageBox.Show(result)
                        Exit Sub
                    End If
                End If

                

                'If objCMCH.StatusStockDMS = EnumCBUReturn.StatusStockDMS.Not_Available Then
                '    MessageBox.Show("Konfirmasi tidak dapat dilakukan, Status Stok DMS harus Available")
                '    Exit Sub
                'End If

                Dim validasi As String = String.Empty
                If Not CheckClaimType(objCMCH.ChassisMasterClaimDetails) Then
                    MessageBox.Show("Tipe Claim belum ditambahkan")
                    Exit Sub
                    'ElseIf objCMCH.DocumentUploads.Count = 0 Then
                    '    MessageBox.Show("Belum ada Dokumen lampiran")
                    '    Exit Sub
                ElseIf ddlKonfirmasi.SelectedValue = EnumCBUReturn.StatusClaim.Baru AndAlso objCMCH.StatusID <> EnumCBUReturn.StatusClaim.Validasi Then
                    ok = False
                ElseIf ddlKonfirmasi.SelectedValue = EnumCBUReturn.StatusClaim.Validasi AndAlso objCMCH.StatusID <> EnumCBUReturn.StatusClaim.Baru Then
                    ok = False
                ElseIf ddlKonfirmasi.SelectedValue = EnumCBUReturn.StatusClaim.Konfirmasi AndAlso objCMCH.StatusID <> EnumCBUReturn.StatusClaim.Validasi Then
                    ok = False
                ElseIf ddlKonfirmasi.SelectedValue = EnumCBUReturn.StatusClaim.Revisi AndAlso objCMCH.StatusID <> EnumCBUReturn.StatusClaim.Konfirmasi Then
                    ok = False
                ElseIf ddlKonfirmasi.SelectedValue = EnumCBUReturn.StatusClaim.Tolak AndAlso objCMCH.StatusID <> EnumCBUReturn.StatusClaim.Konfirmasi Then
                    ok = False
                ElseIf ddlKonfirmasi.SelectedValue = EnumCBUReturn.StatusClaim.Proses AndAlso objCMCH.ResponClaim = 0 Then
                    MessageBox.Show("Respond Claim belum di Pilih")
                    Exit Sub
                ElseIf ddlKonfirmasi.SelectedValue = EnumCBUReturn.StatusClaim.Proses AndAlso (objCMCH.StatusID <> EnumCBUReturn.StatusClaim.Konfirmasi OrElse objCMCH.ResponClaim <> EnumCBUReturn.RespondClaim.Ganti_Unit) Then
                    MessageBox.Show("Send TO SAP hanya untuk Respond Claim Ganti Unit dan status Konfirmasi")
                    Exit Sub
                End If
                arlCM.Add(objCMCH.ChassisMaster)
            End If
        Next

        If arlCM.Count = 0 Then
            MessageBox.Show("Claim yang akan di proses belum dipilih")
            Exit Sub
        End If

        If ok Then
            For Each dgItem As DataGridItem In dgCBUList.Items
                If CType(dgItem.FindControl("chkSelect"), CheckBox).Checked Then
                    Dim objPOH As ChassisMasterClaimHeader = New ChassisMasterClaimHeaderFacade(User).Retrieve(CType(dgItem.Cells(0).Text, Integer))

                    Dim objStatusChangeHistory As StatusChangeHistory = New StatusChangeHistory
                    objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
                    objStatusChangeHistory.DocumentRegNumber = objPOH.ClaimNumber
                    objStatusChangeHistory.OldStatus = objPOH.StatusID

                    If ddlKonfirmasi.SelectedValue <> EnumCBUReturn.StatusClaim.Proses Then
                        Dim errMsg As String = ""
                        If Not CBUReturnValidation.IsValidClaim(objPOH, objPOH.ChassisMasterClaimDetails.Cast(Of ChassisMasterClaimDetail).ToList, objPOH.DocumentUploads.Cast(Of DocumentUpload).ToList, errMsg, CType(ddlKonfirmasi.SelectedValue, Short), True) Then
                            MessageBox.Show(errMsg)
                            Exit Sub
                        End If

                        UpdateIsSendQueue(objPOH)
                        objPOH.StatusID = CType(ddlKonfirmasi.SelectedValue, Short)
                    Else
                        objPOH.StatusID = EnumCBUReturn.StatusClaim.Proses
                    End If
                    objStatusChangeHistory.NewStatus = objPOH.StatusID
                    Dim result = New StatusChangeHistoryFacade(User).Insert(objStatusChangeHistory)
                    arlBH.Add(objPOH)
                End If
            Next
        Else
            Select Case ddlKonfirmasi.SelectedValue
                Case EnumCBUReturn.StatusClaim.Baru 'Baru: 
                    MessageBox.Show("Status Batal Validasi hanya untuk claim berstatus Validasi")
                Case EnumCBUReturn.StatusClaim.Validasi 'Baru: 
                    MessageBox.Show("Status Validasi hanya untuk claim berstatus Baru")
                Case EnumCBUReturn.StatusClaim.Konfirmasi 'Konfirmasi: 
                    MessageBox.Show("Status Konfirmasi hanya untuk claim berstatus Validasi")
                Case EnumCBUReturn.StatusClaim.Revisi 'Revisi Claim:
                    MessageBox.Show("Status Revisi Claim hanya untuk claim berstatus Konfirmasi ")
                Case EnumCBUReturn.StatusClaim.Tolak 'Tolak Claim:
                    MessageBox.Show("Status Tolak Claim hanya untuk claim berstatus Konfirmasi")
                Case EnumCBUReturn.StatusClaim.Proses 'Send to SAP:
                    MessageBox.Show("Send to SAP hanya untuk claim berstatus Konfirmasi")
                Case Else       'Silahkan Pilih
                    MessageBox.Show("Pilih status terlebih dahulu")
            End Select
            Exit Sub
        End If

        If arlBH.Count > 0 Then
            Dim sb As StringBuilder = New StringBuilder
            Dim intGagal As Short = 0
            For Each cmch As ChassisMasterClaimHeader In arlBH
                If ddlKonfirmasi.SelectedValue <> EnumCBUReturn.StatusClaim.Proses Then
                    cmch.StatusID = ddlKonfirmasi.SelectedValue
                    Dim _return As Integer = New ChassisMasterClaimHeaderFacade(User).Update(cmch)
                    If _return = 0 Then
                        intGagal += 1
                        sb.Append("- Update Status untuk Nomor Claim : " & cmch.ClaimNumber & " = Gagal\n")
                    Else
                        sb.Append("- Update Status untuk Nomor Claim : " & cmch.ClaimNumber & " = Sukses\n")
                        If cmch.StatusID = EnumCBUReturn.StatusClaim.Validasi Then
                            Dim objEmailQueue As New ChassisMasterClaimEmailQueue
                            objEmailQueue.ClaimNumber = cmch.ClaimNumber
                            objEmailQueue.StatusClaim = cmch.StatusID
                            objEmailQueue.StatusReturnProcess = cmch.StatusProcessRetur
                            Dim rets = New ChassisMasterClaimEmailQueueFacade(User).Insert(objEmailQueue)
                        End If
                    End If
                Else

                    If Me.txtPass.Text = String.Empty Then
                        RegisterStartupScript("OpenWindow", "<script>showPPPassword2();</script>")
                        Exit Sub
                    End If

                    Dim result = 0
                    UpdateIsSendQueue(cmch)
                    result = SendReturn(cmch)
                    If result < 1 Then
                        intGagal += 1
                        sb.Append("Gagal memproses claim " & cmch.ClaimNumber & "\n")
                    End If
                End If
            Next
            If (sb.ToString().Length > 0) AndAlso intGagal > 0 Then
                MessageBox.Show(sb.ToString())
            Else
                MessageBox.Show("Update Status Sukses")
            End If
        End If

        txtUser.Text = ""
        txtPass.Text = ""
        hdnValActive.Value = "-1"

        BindGrid(ViewState("NewPageIndex"))
    End Sub

    Private Sub UpdateIsSendQueue(ByVal header As ChassisMasterClaimHeader, Optional ByVal All As Boolean = False)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimEmailQueue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "ClaimNumber", MatchType.Exact, header.ClaimNumber))
        If Not All Then
            crit.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "StatusClaim", MatchType.Exact, header.StatusID))
            crit.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "StatusReturnProcess", MatchType.Exact, header.StatusProcessRetur))
            crit.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "IsSend", MatchType.InSet, "(0, 1)"))
        End If
        Dim arlQueue As ArrayList = New ChassisMasterClaimEmailQueueFacade(User).Retrieve(crit)
        If arlQueue.Count > 0 Then
            For Each item As ChassisMasterClaimEmailQueue In arlQueue
                item.IsSend = 2
                Dim result = New ChassisMasterClaimEmailQueueFacade(User).Update(item)
            Next
        End If
    End Sub

    Private Sub dgCBUList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgCBUList.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                Dim objHeader As ChassisMasterClaimHeader = New ChassisMasterClaimHeaderFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
                'If objHeader.CreatedBy.Contains(KTBID) Then
                '    Response.Redirect("FrmInputClaimCBUReturn.aspx?ID=" & e.Item.Cells(0).Text & "&mode=Edit&FromUpload=true")
                'Else
                Response.Redirect("FrmInputClaimCBUReturn.aspx?ID=" & e.Item.Cells(0).Text & "&mode=Edit")
                'End If
        End Select
    End Sub

    Private Sub dgCBUList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgCBUList.ItemDataBound
        If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            e.Item.Cells(11).Visible = False
        End If
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As ChassisMasterClaimHeader = CType(e.Item.DataItem, ChassisMasterClaimHeader)
            Dim chkSelect As CheckBox = CType(e.Item.FindControl("chkSelect"), CheckBox)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblTglClaim As Label = CType(e.Item.FindControl("lblTglClaim"), Label)
            Dim lblNoClaim As Label = CType(e.Item.FindControl("lblNoClaim"), Label)
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Dim lblStatusClaim As Label = CType(e.Item.FindControl("lblStatusClaim"), Label)
            Dim lblResponClaim As Label = CType(e.Item.FindControl("lblResponClaim"), Label)
            Dim lblChassisPengganti As Label = CType(e.Item.FindControl("lblChassisPengganti"), Label)
            Dim lblNoChassis As Label = CType(e.Item.FindControl("lblNoChassis"), Label)
            Dim lblModel As Label = CType(e.Item.FindControl("lblModel"), Label)
            Dim lblStatusProsesReturn As Label = CType(e.Item.FindControl("lblStatusProsesReturn"), Label)
            Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lnkbtnDetail"), LinkButton)
            Dim lnkbtnStatusClaim As LinkButton = CType(e.Item.FindControl("lnkbtnStatusClaim"), LinkButton)
            Dim lnkbtnStatusReturn As LinkButton = CType(e.Item.FindControl("lnkbtnStatusReturn"), LinkButton)

            If oDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
                lnkbtnStatusReturn.Visible = False
            End If
            lnkbtnStatusClaim.Attributes("OnClick") = "showPopUp('../PopUp/PopUpStatusClaimChangeCBUReturn.aspx?Id=" & oData.ID & " ','',500,760,'');"
            lnkbtnStatusReturn.Attributes("OnClick") = "showPopUp('../PopUp/PopUpStatusProcssReturnCBUReturn.aspx?Id=" & oData.ID & " ','',500,760,'');"
            chkSelect.Checked = False
            Dim currentPage As String = CType(sessHelper.GetSession("_PgIdxNext"), String)
            Dim arrGridDF As ArrayList = CType(sessHelper.GetSession("CMCHProcess" + currentPage), ArrayList)
            If IsNothing(arrGridDF) Then arrGridDF = New ArrayList
            For Each oDF As ChassisMasterClaimHeader In arrGridDF
                If oData.ID = oDF.ID Then
                    chkSelect.Checked = True
                    Exit For
                End If
            Next
            lblNo.Text = (dgCBUList.PageSize * dgCBUList.CurrentPageIndex) + e.Item.ItemIndex + 1
            lblTglClaim.Text = oData.ClaimDate
            lblNoClaim.Text = oData.ClaimNumber
            lblNoChassis.Text = oData.ChassisMaster.ChassisNumber
            lblDealer.Text = oData.Dealer.SearchTerm1
            Dim stdStatusClaim As ArrayList = CType(New StandardCodeFacade(User).RetrieveByValueId(oData.StatusID, "ChassisMasterClaim.StatusClaim"), ArrayList)
            If stdStatusClaim.Count > 0 Then
                lblStatusClaim.Text = CType(stdStatusClaim(0), StandardCode).ValueDesc
            End If
            Dim stdResponClaim As ArrayList = CType(New StandardCodeFacade(User).RetrieveByValueId(oData.ResponClaim, "ChassisMasterClaim.RespondClaim"), ArrayList)
            If stdResponClaim.Count > 0 Then
                lblResponClaim.Text = CType(stdResponClaim(0), StandardCode).ValueDesc
            End If
            lblChassisPengganti.Text = oData.ChassisNumberReplacement
            If Not IsNothing(oData.ChassisMaster) Then
                If Not IsNothing(oData.ChassisMaster.VechileColor) Then
                    If Not IsNothing(oData.ChassisMaster.VechileColor.VechileType) Then
                        If Not IsNothing(oData.ChassisMaster.VechileColor.VechileType.VechileModel) Then
                            lblModel.Text = oData.ChassisMaster.VechileColor.VechileType.VechileModel.VechileModelCode
                        Else
                            lblModel.Text = ""
                        End If
                    Else
                        lblModel.Text = ""
                    End If
                Else
                    lblModel.Text = ""
                End If
            Else
                lblModel.Text = ""
            End If
            If oData.StatusProcessRetur > 0 Then
                lblStatusProsesReturn.Text = CType(oData.StatusProcessRetur, EnumCBUReturn.StatusProsesRetur).ToString.Replace("_", " ")
            Else
                lblStatusProsesReturn.Text = ""
            End If
        End If
    End Sub

    Protected Sub dgCBUList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgCBUList.PageIndexChanged
        dgCBUList.CurrentPageIndex = e.NewPageIndex
        ViewState("NewPageIndex") = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

#Region "download excel"
    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrData As New DataTable
        Dim crits As CriteriaComposite
        If dgCBUList.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelper.GetSession("criteriadownload")) Then
            crits = CType(sessHelper.GetSession("criteriadownload"), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        'arrData = New ChassisMasterClaimHeaderFacade(User).Retrieve(crits)
        arrData = New ChassisMasterClaimHeaderFacade(User).GetDownLoadExcel(crits.ToString())
        If arrData.Rows.Count > 0 Then
            CreateExcel("DaftarClaim", arrData)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As DataTable)
        Using pck As New ExcelPackage()
            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            'Create Header Column
            ws.Cells("A1").ValueBold(FileName)
            Dim rowIndex As Integer = 3
            Dim ColumnIndex As Integer = 1
            Dim lastColumn As Integer = 0
            ws.Cells(rowIndex, ColumnIndex).ValueBold("No")
            ws.Cells(rowIndex, ColumnIndex).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
            ws.Cells(rowIndex, ColumnIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)

            For Each dColumn As DataColumn In Data.Columns
                ColumnIndex += 1
                ws.Cells(rowIndex, ColumnIndex).ValueBold(dColumn.ColumnName)
                ws.Cells(rowIndex, ColumnIndex).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
                ws.Cells(rowIndex, ColumnIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)
            Next
            lastColumn = ColumnIndex

            'Create Data
            Dim noUrutan As Integer = 1
            For Each dRow As DataRow In Data.Rows
                rowIndex += 1
                ColumnIndex = 1
                ws.Cells(rowIndex, ColumnIndex).SetValue(noUrutan.ToString())
                For Each dColumn As DataColumn In Data.Columns
                    ColumnIndex += 1
                    ws.Cells(rowIndex, ColumnIndex).SetValue(dRow(dColumn.ColumnName).ToString())
                Next
                noUrutan += 1
            Next
            ws.Cells(3, 2, rowIndex, lastColumn).AutoFilter = True
            For colIdx As Integer = 1 To Data.Columns.Count + 1
                ws.Column(colIdx).AutoFit()
            Next
            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using
    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Nomor Claim"
            ws.Cells("C3").Value = "Kode Dealer"
            ws.Cells("D3").Value = "Nama Dealer"
            ws.Cells("E3").Value = "Periode Claim"
            ws.Cells("F3").Value = "Status Claim"
            ws.Cells("G3").Value = "Nomor Chassis"
            ws.Cells("H3").Value = "Status Proses Retur"
            ws.Cells("I3").Value = "Model"
            Dim standardCodeStatusClaimList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("ChassisMasterClaim.StatusClaim").Cast(Of  _
                                                StandardCode).ToList()
            Dim standardCodeStatusProsesReturList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("ChassisMasterClaim.StatusProsesRetur").Cast(Of  _
                                                StandardCode).ToList()

            Dim idx As Integer = 0
            For i As Integer = 0 To Data.Count - 1
                Dim item As ChassisMasterClaimHeader = Data(i)
                Dim arrBDA As ChassisMasterClaimHeader = New ChassisMasterClaimHeaderFacade(User).Retrieve(item.ID)

                ws.Cells(idx + 4, 1).Value = idx + 1
                ws.Cells(idx + 4, 2).Value = item.ClaimNumber
                ws.Cells(idx + 4, 3).Value = item.Dealer.DealerCode
                ws.Cells(idx + 4, 4).Value = item.Dealer.DealerName
                ws.Cells(idx + 4, 5).Value = item.CreatedTime
                Try
                    ws.Cells(idx + 4, 6).Value = standardCodeStatusClaimList.FirstOrDefault(Function(x) x.ValueId = item.StatusID).ValueDesc
                Catch
                End Try
                ws.Cells(idx + 4, 7).Value = item.ChassisNumberReplacement
                Try
                    ws.Cells(idx + 4, 8).Value = standardCodeStatusProsesReturList.FirstOrDefault(Function(x) x.ValueId = item.StatusProcessRetur).ValueDesc
                Catch
                End Try
                ws.Cells(idx + 4, 9).Value = item.ChassisMaster.VechileColor.VechileType.VechileModel.VechileModelCode
                ws.Column(5).Style.Numberformat.Format = "DD/MM/YY"
                idx = idx + 1
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using

    End Sub

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        'Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName)
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub
#End Region

    Protected Sub hdnDealerClaim_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealerClaim.ValueChanged
        Dim data As String() = hdnDealerClaim.Value.Trim.Split(";")
        txtDealerName.Text = hdnDealerClaim.Value
    End Sub

    Protected Sub hdnDealerAlokasi_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealerAlokasi.ValueChanged
        Dim data As String() = hdnDealerAlokasi.Value.Trim.Split(";")
        txtDealerAlokasi.Text = hdnDealerAlokasi.Value
    End Sub

    Protected Sub btnProcessReturn_Click(sender As Object, e As EventArgs) Handles btnProcessReturn.Click
        Dim arlBH As New ArrayList
        Dim ok As Boolean = True
        Dim StatRturn As Integer = -1
        For Each dgItem As DataGridItem In dgCBUList.Items
            If CType(dgItem.FindControl("chkSelect"), CheckBox).Checked Then
                Dim objCMCH As ChassisMasterClaimHeader = New ChassisMasterClaimHeaderFacade(User).Retrieve(CType(dgItem.Cells(0).Text, Integer))
                If StatRturn = -1 Then
                    StatRturn = objCMCH.StatusProcessRetur
                Else
                    If StatRturn <> objCMCH.StatusProcessRetur Then
                        MessageBox.Show("Hanya dapat memproses Claim dengan Status Proses Return yang sama")
                        Continue For
                    End If
                End If

                If Not CheckClaimType(objCMCH.ChassisMasterClaimDetails) Then
                    MessageBox.Show("Tipe Claim belum ditambahkan")
                    Exit Sub
                ElseIf objCMCH.DocumentUploads.Count = 0 Then
                    MessageBox.Show("Belum ada Dokumen lampiran")
                    Exit Sub
                ElseIf objCMCH.StatusID <> EnumCBUReturn.StatusClaim.Proses Then
                    MessageBox.Show("Tidak dapat memproses Claim " & objCMCH.ClaimNumber & "\nHanya dapat memproses Claim yang berstatus Proses")
                    arlBH = New ArrayList
                    Exit Sub
                ElseIf objCMCH.StatusProcessRetur <> StatRturn Then
                    MessageBox.Show("Tidak dapat memproses Claim " & objCMCH.ClaimNumber & "\nHanya dapat memproses Claim dengan Status Proses Return yang sama")
                    arlBH = New ArrayList
                    Exit Sub
                ElseIf objCMCH.StatusProcessRetur = 0 Then
                    MessageBox.Show("Tidak dapat memproses Claim " & objCMCH.ClaimNumber & "\nHanya dapat memproses Claim dengan Status Proses Return sudah terisi")
                    arlBH = New ArrayList
                    Exit Sub
                ElseIf objCMCH.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Cancel_Billing And objCMCH.StatusStockDMS = EnumCBUReturn.StatusStockDMS.Not_Available Then
                    MessageBox.Show("Claim Number " & objCMCH.ClaimNumber & " : " & CBUReturnValidation.STATUS_STOCK_DMS_NOT_VALID)
                    Exit Sub
                End If

                arlBH.Add(objCMCH)
            End If
        Next


        If Not ProcessPriv(StatRturn) Then
            MessageBox.Show("Anda tidak berhak mengubah Proses Return berstatus " & CType(StatRturn, EnumCBUReturn.StatusProsesRetur).ToString)
            Exit Sub
        End If

        If arlBH.Count = 0 Then
            MessageBox.Show("Claim yang akan di proses belum dipilih")
            Exit Sub
        Else
            Dim sHH As New SessionHelper
            Dim confirm As Boolean = False
            If Not IsNothing(sHH.GetSession("Confirm")) Then
                confirm = CType(sHH.GetSession("Confirm"), Boolean)
            End If
            If StatRturn <> EnumCBUReturn.StatusProsesRetur.Faktur_sudah_di_print AndAlso StatRturn <> EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                If (hdnValActive.Value = "-1" OrElse txtPass.Text.Trim.Length = 0) Then
                    If StatRturn = EnumCBUReturn.StatusProsesRetur.Faktur_sudah_di_print Then
                        MessageBox.Confirm("Apakah Faktur sudah dikembalikan kepada MMKSI?", "hdnValActive")
                    Else
                        MessageBox.Confirm("Semua data yang terpilih akan diproses?", "hdnValActive")
                    End If
                    sHH.SetSession("NotShowPassword", False)
                    Return
                End If
            ElseIf (StatRturn = EnumCBUReturn.StatusProsesRetur.Faktur_sudah_di_print) And Not confirm Then
                MessageBox.Confirm("Apakah Faktur sudah dikembalikan kepada MMKSI?", "hdnValActive")
                sHH.SetSession("NotShowPassword", True)
                sHH.SetSession("Confirm", False)
                Return
            End If
            sHH.SetSession("Confirm", False)

            Dim msg As String = String.Empty
            msg = SendReturn(arlBH.Cast(Of ChassisMasterClaimHeader).ToList())
            'For Each cmch As ChassisMasterClaimHeader In arlBH
            '    Dim result = 0
            '    UpdateIsSendQueue(cmch)
            '    result = SendReturn(cmch)
            '    If result < 1 Then
            '        msg = "Gagal memproses claim " & cmch.ClaimNumber & "\n"
            '    End If
            'Next
            If StatRturn = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                TransferWSM()
            End If

            If msg.Length > 0 Then
                MessageBox.Show(msg)
            Else
                MessageBox.Show("Proses Berhasil")
            End If
        End If

        txtUser.Text = ""
        txtPass.Text = ""
        hdnValActive.Value = "-1"

        BindGrid(ViewState("NewPageIndex"))
    End Sub

    Private Function ProcessPriv(ByVal StatusReturn As Integer) As Boolean
        Select Case StatusReturn
            Case 1, 2, 6
                Return SecurityProvider.Authorize(Context.User, SR.CBUReturn_Retail_Ubah_Privilage)
            Case 3, 4, 5
                Return SecurityProvider.Authorize(Context.User, SR.CBUReturn_Wholesales_Ubah_Privilage)
        End Select
        Return True
    End Function

    Protected Function SendReturn(ByVal listChassisMasterClaim As List(Of ChassisMasterClaimHeader)) As String
        Dim objCBUReturnSendSAP As CBUReturnSendSAP = New CBUReturnSendSAP
        Dim objChassisMaster As ChassisMaster = New ChassisMaster
        Dim statusProsesRetur As Integer = 0
        Dim CMClaimFacade As ChassisMasterClaimHeaderFacade = New ChassisMasterClaimHeaderFacade(User)

        Dim objChassisMasters As ArrayList = New ArrayList
        Dim objEmailQueues As ArrayList = New ArrayList
        Dim arlWSMData As ArrayList = CType(sessHelper.GetSession(SessionWSMData), ArrayList)
        'Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID
        Dim isFromCancelled As Boolean = False
        Dim emailTo As String = GetEmail("RSD")
        Dim emailBcc As String = GetEmail("EmailBCC")
        Dim endCustomerID As Integer = 0
        Dim msg As String = String.Empty

        objCBUReturnSendSAP.SAPConn = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionStringEmpty") 'For test using "SAPConnectionString"
        objCBUReturnSendSAP.Username = txtUser.Text
        objCBUReturnSendSAP.Password = txtPass.Text


        For Each objChassisMasterClaimHeader As ChassisMasterClaimHeader In listChassisMasterClaim
            Dim objStatusChanges As ArrayList = New ArrayList
            Dim objStatusChangeHistory As StatusChangeHistory


            UpdateIsSendQueue(objChassisMasterClaimHeader)
            statusProsesRetur = objChassisMasterClaimHeader.StatusProcessRetur
            Dim isSend As Boolean = False
            If objChassisMasterClaimHeader.ResponClaim = EnumCBUReturn.RespondClaim.Ganti_Unit Then
                objStatusChangeHistory = New StatusChangeHistory
                objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ReturStatus)
                objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusProcessRetur

                If objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Faktur_sudah_di_print Then
                    objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Cancel_Faktur
                ElseIf objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                    objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
                    objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusID
                    objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusClaim.Selesai, Short)
                    objStatusChanges.Add(objStatusChangeHistory)

                    objChassisMasterClaimHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Selesai, Short)
                Else
                    isSend = True
                    objCBUReturnSendSAP.ChassisClaimHeaders.Add(objChassisMasterClaimHeader)
                    objCBUReturnSendSAP.CurrentStatusRetur = statusProsesRetur
                End If

                If Not isSend Then
                    Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID
                    objChassisMasters = AfterReturnSAPProcess(objChassisMasterClaimHeader, objCBUReturnSendSAP, objChassisMasters, statusProsesRetur, isFromCancelled)

                    If objStatusChangeHistory.OldStatus <> EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                        objStatusChangeHistory.NewStatus = objChassisMasterClaimHeader.StatusProcessRetur
                        objStatusChanges.Add(objStatusChangeHistory)
                    End If

                    If objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Selesai Then
                        objStatusChangeHistory = New StatusChangeHistory
                        objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
                        objStatusChangeHistory.OldStatus = statusClaimInSession
                        objStatusChangeHistory.NewStatus = objChassisMasterClaimHeader.StatusID
                        objStatusChanges.Add(objStatusChangeHistory)
                    End If

                    If objChassisMasterClaimHeader.StatusProcessRetur <> 0 And objChassisMasterClaimHeader.StatusProcessRetur <> EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                        'Send Email to queue
                        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimEmailQueue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crit.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "ClaimNumber", MatchType.Exact, objChassisMasterClaimHeader.ClaimNumber))
                        crit.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "IsSend", MatchType.Exact, 0))
                        objEmailQueues = New ChassisMasterClaimEmailQueueFacade(User).Retrieve(crit)

                        Dim objEmailQueue As New ChassisMasterClaimEmailQueue
                        objEmailQueue.ClaimNumber = objChassisMasterClaimHeader.ClaimNumber
                        objEmailQueue.StatusClaim = objChassisMasterClaimHeader.StatusID
                        objEmailQueue.StatusReturnProcess = objChassisMasterClaimHeader.StatusProcessRetur
                        objEmailQueues.Add(objEmailQueue)
                    End If

                    If objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                        arlWSMData.Add(objChassisMasterClaimHeader)
                        sessHelper.SetSession(SessionWSMData, arlWSMData)

                        Dim chassisM As ChassisMaster = ChassisMFacade.Retrieve(objChassisMasterClaimHeader.ChassisNumberReplacement)
                        endCustomerID = chassisM.EndCustomerID

                        objStatusChangeHistory = New StatusChangeHistory
                        objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ReturStatus)
                        objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusProcessRetur
                        objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti, Short)
                        objStatusChanges.Add(objStatusChangeHistory)
                    End If

                    If objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Selesai Then
                        UpdateIsSendQueue(objChassisMasterClaimHeader, True)
                    End If

                    CMClaimFacade.Update(objChassisMasterClaimHeader, objStatusChanges, objChassisMasters, objEmailQueues)

                    Dim statusReturList As String = String.Format("{0},{1},{2}", _
                        CInt(EnumCBUReturn.StatusProsesRetur.Reverse_DO), CInt(EnumCBUReturn.StatusProsesRetur.Sales_Replacement), CInt(EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti))
                    If statusReturList.Contains(statusProsesRetur) And endCustomerID <> 0 Then
                        SendEmail(objChassisMasterClaimHeader, emailTo, emailBcc, isFromCancelled, msg)
                    End If
                End If
            End If
        Next

        If objCBUReturnSendSAP.ChassisClaimHeaders.Count > 0 Then
            If Not CBUReturnValidation.IsValidRetur(objCBUReturnSendSAP) Or objCBUReturnSendSAP.Message <> "" Then
                msg += objCBUReturnSendSAP.Message
            End If
        Else
            Return String.Empty
        End If
        SaveToWSLog(objCBUReturnSendSAP.GetBodyResponse)

        Dim listSuccess As New List(Of ChassisMasterClaimHeader)
        For Each result As CBUReturnSAPResponse In objCBUReturnSendSAP.SapResponses.Where(Function(x) x.Status = "S")
            objChassisMasters = New ArrayList()
            Dim objChassisMasterClaimHeader As ChassisMasterClaimHeader = objCBUReturnSendSAP.ChassisClaimHeaders.Cast(Of ChassisMasterClaimHeader).FirstOrDefault(Function(y) _
                                                                       y.ChassisMaster.ChassisNumber = result.ChassisLama)
            Dim objStatusChanges As New ArrayList
            Dim objStatusChangeHistory As StatusChangeHistory
            isFromCancelled = False
            endCustomerID = objChassisMasterClaimHeader.ChassisMaster.EndCustomerID

            Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID
            Dim chassisM As ChassisMaster = ChassisMFacade.Retrieve(result.ChassisPengganti)
            If chassisM.ID <> 0 Then
                If chassisM.EndCustomerID <> 0 Then
                    msg += String.Format("Data Chassis Pengganti : {0} sudah ada \n", chassisM.ChassisNumber)
                    Exit For
                Else
                    chassisM.RowStatus = -1
                    objChassisMasters.Add(chassisM)
                End If
            End If

            objStatusChangeHistory = New StatusChangeHistory
            objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ReturStatus)
            objStatusChangeHistory.OldStatus = objCBUReturnSendSAP.CurrentStatusRetur

            objChassisMasters = AfterReturnSAPProcess(objChassisMasterClaimHeader, objCBUReturnSendSAP, objChassisMasters, statusProsesRetur, isFromCancelled)

            If objStatusChangeHistory.OldStatus <> EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                objStatusChangeHistory.NewStatus = objChassisMasterClaimHeader.StatusProcessRetur
                objStatusChanges.Add(objStatusChangeHistory)
            End If

            If objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Selesai Then
                objStatusChangeHistory = New StatusChangeHistory
                objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
                objStatusChangeHistory.OldStatus = statusClaimInSession
                objStatusChangeHistory.NewStatus = objChassisMasterClaimHeader.StatusID
                objStatusChanges.Add(objStatusChangeHistory)
            End If

            If objChassisMasterClaimHeader.StatusProcessRetur <> 0 And objChassisMasterClaimHeader.StatusProcessRetur <> EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                'Send Email to queue
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimEmailQueue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "ClaimNumber", MatchType.Exact, objChassisMasterClaimHeader.ClaimNumber))
                crit.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "IsSend", MatchType.Exact, 0))
                objEmailQueues = New ChassisMasterClaimEmailQueueFacade(User).Retrieve(crit)

                Dim objEmailQueue As New ChassisMasterClaimEmailQueue
                objEmailQueue.ClaimNumber = objChassisMasterClaimHeader.ClaimNumber
                objEmailQueue.StatusClaim = objChassisMasterClaimHeader.StatusID
                objEmailQueue.StatusReturnProcess = objChassisMasterClaimHeader.StatusProcessRetur
                objEmailQueues.Add(objEmailQueue)
            End If

            If objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                arlWSMData.Add(objChassisMasterClaimHeader)
                sessHelper.SetSession(SessionWSMData, arlWSMData)
            End If

            If objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Selesai Then
                UpdateIsSendQueue(objChassisMasterClaimHeader, True)
            End If

            Dim funcH As New ChassisMasterClaimHeaderFacade(Me.User)
            funcH.Update(objChassisMasterClaimHeader, objStatusChanges, objChassisMasters, objEmailQueues)

            Dim statusReturList As String = String.Format("{0},{1},{2}", _
                CInt(EnumCBUReturn.StatusProsesRetur.Reverse_DO), CInt(EnumCBUReturn.StatusProsesRetur.Sales_Replacement), CInt(EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti))
            If statusReturList.Contains(statusProsesRetur) And endCustomerID <> 0 Then
                SendEmail(objChassisMasterClaimHeader, emailTo, emailBcc, isFromCancelled, msg)
            End If
        Next

        Return msg
    End Function

    Protected Function SendReturn(ByVal objChassisMasterClaimHeader As ChassisMasterClaimHeader) As Integer
        Dim result As Integer = 0
        Dim objCBUReturnSendSAP As CBUReturnSendSAP = New CBUReturnSendSAP
        Dim objChassisMaster As ChassisMaster = New ChassisMaster
        Dim statusProsesRetur As Integer = 0
        Dim CMClaimFacade As ChassisMasterClaimHeaderFacade = New ChassisMasterClaimHeaderFacade(User)
        Dim objStatusChanges As ArrayList = New ArrayList
        Dim objStatusChangeHistory As StatusChangeHistory
        Dim objChassisMasters As ArrayList = New ArrayList
        Dim objEmailQueues As ArrayList = New ArrayList
        Dim arlWSMData As ArrayList = CType(sessHelper.GetSession(SessionWSMData), ArrayList)
        Dim statusClaimInSession As Integer = objChassisMasterClaimHeader.StatusID
        Dim isFromCancelled As Boolean = False
        statusProsesRetur = objChassisMasterClaimHeader.StatusProcessRetur

        If objChassisMasterClaimHeader.ResponClaim = EnumCBUReturn.RespondClaim.Ganti_Unit Then

            objStatusChangeHistory = New StatusChangeHistory
            objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ReturStatus)
            objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusProcessRetur

            If objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Faktur_sudah_di_print Then
                objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Cancel_Faktur
            ElseIf objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
                objStatusChangeHistory.OldStatus = objChassisMasterClaimHeader.StatusID
                objStatusChangeHistory.NewStatus = CType(EnumCBUReturn.StatusClaim.Selesai, Short)
                objStatusChanges.Add(objStatusChangeHistory)

                objChassisMasterClaimHeader.StatusID = CType(EnumCBUReturn.StatusClaim.Selesai, Short)
            Else
                objCBUReturnSendSAP.SAPConn = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionStringEmpty") 'For test using "SAPConnectionString"
                objCBUReturnSendSAP.Username = txtUser.Text
                objCBUReturnSendSAP.Password = txtPass.Text
                objCBUReturnSendSAP.CurrentStatusRetur = statusProsesRetur
                objCBUReturnSendSAP.ChassisClaimHeaders.Add(objChassisMasterClaimHeader)

                If Not CBUReturnValidation.IsValidRetur(objCBUReturnSendSAP) Or objCBUReturnSendSAP.Message <> "" Then
                    SaveToWSLog(objCBUReturnSendSAP.GetBodyResponse)
                    MessageBox.Show(objCBUReturnSendSAP.Message)
                    Exit Function
                End If
                SaveToWSLog(objCBUReturnSendSAP.GetBodyResponse)

                If statusProsesRetur = 0 Then
                    Dim chassisM As ChassisMaster = ChassisMFacade.Retrieve(objCBUReturnSendSAP.SapResponses(0).ChassisPengganti)
                    'Sementara di lepas saat Pre UAT
                    'If chassisM.ID <> 0 Then
                    '    If chassisM.EndCustomerID <> 0 Then
                    '        MessageBox.Show(String.Format("Data Chassis Pengganti : {0} sudah ada", chassisM.ChassisNumber))
                    '        Exit Function
                    '    Else
                    '        chassisM.RowStatus = -1
                    '        objChassisMasters.Add(chassisM)
                    '    End If
                    'End If
                End If

                objChassisMasterClaimHeader = CType(objCBUReturnSendSAP.ChassisClaimHeaders(0), ChassisMasterClaimHeader)
            End If

            'objStatusChangeHistory.NewStatus = objChassisMasterClaimHeader.StatusProcessRetur
            'objStatusChanges.Add(objStatusChangeHistory)

            objChassisMasters = AfterReturnSAPProcess(objChassisMasterClaimHeader, objCBUReturnSendSAP, objChassisMasters, statusProsesRetur, isFromCancelled)

            If objStatusChangeHistory.OldStatus <> EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                objStatusChangeHistory.NewStatus = objChassisMasterClaimHeader.StatusProcessRetur
                objStatusChanges.Add(objStatusChangeHistory)
            End If

            If objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Selesai Then
                objStatusChangeHistory = New StatusChangeHistory
                objStatusChangeHistory.DocumentType = CInt(LookUp.DocumentType.CBUReturn_ClaimStatus)
                objStatusChangeHistory.OldStatus = statusClaimInSession
                objStatusChangeHistory.NewStatus = objChassisMasterClaimHeader.StatusID
                objStatusChanges.Add(objStatusChangeHistory)
            End If

            If objChassisMasterClaimHeader.StatusProcessRetur <> 0 And objChassisMasterClaimHeader.StatusProcessRetur <> EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
                'Send Email to queue
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimEmailQueue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "ClaimNumber", MatchType.Exact, objChassisMasterClaimHeader.ClaimNumber))
                crit.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "IsSend", MatchType.Exact, 0))
                objEmailQueues = New ChassisMasterClaimEmailQueueFacade(User).Retrieve(crit)

                Dim objEmailQueue As New ChassisMasterClaimEmailQueue
                objEmailQueue.ClaimNumber = objChassisMasterClaimHeader.ClaimNumber
                objEmailQueue.StatusClaim = objChassisMasterClaimHeader.StatusID
                objEmailQueue.StatusReturnProcess = objChassisMasterClaimHeader.StatusProcessRetur
                objEmailQueues.Add(objEmailQueue)
            End If
        Else
            objChassisMasters = Nothing
        End If

        'If objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Faktur_sudah_di_print Then
        '    objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Cancel_Faktur
        'End If

        If objChassisMasterClaimHeader.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
            arlWSMData.Add(objChassisMasterClaimHeader)
            sessHelper.SetSession(SessionWSMData, arlWSMData)
        End If

        If objChassisMasterClaimHeader.StatusID = EnumCBUReturn.StatusClaim.Selesai Then
            UpdateIsSendQueue(objChassisMasterClaimHeader, True)
        End If

        Return CMClaimFacade.Update(objChassisMasterClaimHeader, objStatusChanges, objChassisMasters, objEmailQueues)
    End Function

    Private Function CheckClaimType(ByVal arrDetail As ArrayList) As Boolean
        For Each item As ChassisMasterClaimDetail In arrDetail
            If item.ClaimType = 0 Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Function AfterReturnSAPProcess(ByRef objClaim As ChassisMasterClaimHeader, ByVal objCBUReturnSendSAP As CBUReturnSendSAP, ByVal objList As ArrayList, ByVal currentStatus As Integer, ByRef isFromCancelled As Boolean) As ArrayList
        Dim chassisM As ChassisMaster = ChassisMFacade.Retrieve(objClaim.ChassisMaster.ChassisNumber)
        currentStatus = IIf(currentStatus = 0, objClaim.StatusProcessRetur, currentStatus)

        Select Case currentStatus
            Case EnumCBUReturn.StatusProsesRetur.Faktur_sudah_di_print, EnumCBUReturn.StatusProsesRetur.Cancel_Billing
                Dim objAppConfFacade As AppConfigFacade = New AppConfigFacade(User)
                Dim objAppConf As AppConfig = objAppConfFacade.Retrieve(confBlockFakturDesc)
                chassisM.PendingDesc = String.Format("Block_Faktur_{0}", objAppConf.Value)
                objList.Add(chassisM)
            Case EnumCBUReturn.StatusProsesRetur.Reverse_DO, EnumCBUReturn.StatusProsesRetur.Sales_Replacement

                'Set inactive chassis claimed
                chassisM.RowStatus = -1
                chassisM.EndCustomer = Nothing
                objList.Add(chassisM)

                If objClaim.ChassisMaster.EndCustomerID <> 0 Then '
                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(StatusChangeHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, objClaim.ClaimNumber))
                    crit.opAnd(New Criteria(GetType(StatusChangeHistory), "DocumentType", MatchType.Exact, CType(LookUp.DocumentType.CBUReturn_ReturStatus, Integer)))
                    crit.opAnd(New Criteria(GetType(StatusChangeHistory), "NewStatus", MatchType.Exact, CType(EnumCBUReturn.StatusProsesRetur.Cancel_Faktur, Integer)))

                    Dim datas As ArrayList = New StatusChangeHistoryFacade(User).Retrieve(crit)
                    If datas.Count > 0 Then
                        objClaim.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti
                        isFromCancelled = True
                    Else
                        objClaim.StatusID = EnumCBUReturn.StatusClaim.Selesai
                    End If
                Else
                    objClaim.StatusID = EnumCBUReturn.StatusClaim.Selesai
                End If

                'Swap chassis claimed to chassis replacement
                Dim chassisOld As String = objClaim.ChassisMaster.ChassisNumber
                Dim responses As CBUReturnSAPResponse = objCBUReturnSendSAP.SapResponses.FirstOrDefault(Function(x) x.ChassisLama = chassisOld)
                chassisM = ChassisMFacade.Retrieve(objClaim.ChassisMaster.ChassisNumber)
                chassisM.ID = 0
                chassisM.ChassisNumber = objClaim.ChassisNumberReplacement
                chassisM.EngineNumber = responses.EngineNumber
                chassisM.SerialNumber = responses.SerialNumber
                chassisM.DODate = responses.DoDate
                chassisM.PendingDesc = ""
                chassisM.GIDate = CDate("1900-01-01")
                chassisM.ParkingDays = 0
                chassisM.ParkingAmount = 0

                If currentStatus = EnumCBUReturn.StatusProsesRetur.Sales_Replacement Then
                    'chassisM.SONumber = responses.SOReplacement
                    chassisM.DONumber = responses.DOReplacement

                    Dim statusFakturList As String = String.Format("{0},{1},{2}", _
                         CInt(EnumChassisMaster.FakturStatus.Konfirmasi), CInt(EnumChassisMaster.FakturStatus.Proses), CInt(EnumChassisMaster.FakturStatus.Selesai))
                    If statusFakturList.Contains(objClaim.ChassisMaster.FakturStatus) Then
                        chassisM.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi
                    End If
                End If

                objList.Add(chassisM)
        End Select
        Return objList
    End Function

    Protected Sub ReTransfer_Click(sender As Object, e As EventArgs) Handles ReTransfer.Click
        Dim arlWSMData As New ArrayList
        For Each item As DataGridItem In dgCBUList.Items
            If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                Dim id As Integer = item.Cells(0).Text
                Dim objChassisMasterClaimHeader As ChassisMasterClaimHeader = New ChassisMasterClaimHeaderFacade(User).Retrieve(id)  '-- Retrieve this invoice based on ID
                Dim objChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(objChassisMasterClaimHeader.ChassisNumberReplacement)
                If objChassisMaster.FakturStatus = EnumChassisMaster.FakturStatus.Proses Then
                    arlWSMData.Add(objChassisMasterClaimHeader)
                End If
            End If
        Next
        sessHelper.SetSession(SessionWSMData, arlWSMData)

        ReTransferWSM()
        ForceFinishClaim()
    End Sub

    Private Sub ForceFinishClaim()
        Dim arlData As ArrayList = sessHelper.GetSession(SessionWSMData)
        For Each item As ChassisMasterClaimHeader In arlData
            item.StatusID = EnumCBUReturn.StatusClaim.Selesai
            Dim result As Integer = New ChassisMasterClaimHeaderFacade(User).Update(item)
        Next
    End Sub

    Private Sub TransferWSM()
        TransferChasisMasterProfile(0)
        'TransferChasisMasterProfile(3) 'debug
        Transfer()
    End Sub

    Private Sub ReTransferWSM()
        TransferChasisMasterProfile(1)
        'TransferChasisMasterProfile(3) 'debug
        Transfer(True)
    End Sub

    Private Sub TransferChasisMasterProfile(ByVal mode As Integer)
        Dim filename = String.Format("{0}{1}{2}", "csprof", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\FK\" & filename  '-- Destination file
        TransferChassisProfile(DestFile, mode)
    End Sub

    Private Sub Transfer(Optional ByVal ReTransfer As Boolean = False)
        ChassisList.Clear()

        Dim arlWSMData As ArrayList = CType(sessHelper.GetSession(SessionWSMData), ArrayList)
        If ReTransfer Then
            For Each item As DataGridItem In dgCBUList.Items
                If CType(item.FindControl("chkSelect"), CheckBox).Checked Then
                    Dim id As Integer = item.Cells(0).Text
                    Dim CMHeader As ChassisMasterClaimHeader = New ChassisMasterClaimHeaderFacade(User).Retrieve(id)  '-- Retrieve this invoice based on ID
                    Dim Invoice As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CMHeader.ChassisNumberReplacement)
                    If Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Proses Then
                        ChassisList.Add(CMHeader)  '-- Add to list of invoices
                    End If
                    'ChassisList.Add(CMHeader) 'debug
                End If
            Next
        Else
            For Each CMHeader As ChassisMasterClaimHeader In arlWSMData
                Dim Invoice As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CMHeader.ChassisNumberReplacement)
                If Invoice.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi Then
                    ChassisList.Add(CMHeader)
                End If
                'ChassisList.Add(CMHeader) 'debug
            Next
        End If

        If ChassisList.Count = 0 Then
            MessageBox.Show("Tidak ada faktur yang dipilih atau faktur tidak bisa di-transfer")
            Exit Sub
        End If
        Dim dt As DateTime = DateTime.Now
        Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & _
                                CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & _
                                CType(dt.Second, String) & CType(dt.Millisecond, String)
        Dim InvoiceData As String = Server.MapPath("") & "\..\DataTemp\fkopen" & sSuffix & ".txt"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Try
            If imp.Start() Then
                Dim finfo As FileInfo = New FileInfo(InvoiceData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If
                Dim fs As FileStream = New FileStream(InvoiceData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteInvoiceData(sw)
                sw.Close()
                fs.Close()
                Dim DestFolder As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\FK"
                If Not IO.Directory.Exists(DestFolder) Then
                    IO.Directory.CreateDirectory(DestFolder)
                End If
                Dim DestFile As String = DestFolder & "\fkopen" & sSuffix & ".txt"
                Dim finfo2 As FileInfo = New FileInfo(InvoiceData)
                finfo2.CopyTo(DestFile, True)
                imp.StopImpersonate()
                imp = Nothing
            End If
            InProcessStatus()  '-- Change invoice status from 'Konfirmasi' to 'Proses'
            MessageBox.Show("Transfer data berhasil")
        Catch ex As Exception
            MessageBox.Show("Transfer data gagal")
        End Try

    End Sub

    Private Function TransferChassisProfile(ByVal DestFile As String, ByVal mode As Integer) As Boolean
        Dim listFaktur As ArrayList = PopulateInvoice(mode)
        If listFaktur.Count = 0 Then
            Return False
        End If
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If finfo.Exists Then
                    finfo.Delete()
                End If
                Dim fs As FileStream = New FileStream(DestFile, FileMode.CreateNew)
                sw = New StreamWriter(fs)
                DnLoadInvoiceDataChasisProfile(sw, mode)
                sw.Close()
                fs.Close()
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        End Try
        Return success
    End Function

    Private Sub WriteInvoiceData(ByRef sw As StreamWriter)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file
        For Each CMHeader As ChassisMasterClaimHeader In ChassisList
            Dim objInvoice As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CMHeader.ChassisNumberReplacement)
            InvoiceLine.Remove(0, InvoiceLine.Length)  '-- Empty Invoice line
            InvoiceLine.Append(objInvoice.ChassisNumber.Replace(tab, " ") & tab) '-- Chassis number
            If Not IsNothing(objInvoice.EndCustomer) Then
                InvoiceLine.Append(Format(objInvoice.EndCustomer.FakturDate, "ddMMyyyy") & tab)  '-- Faktur date
                Dim objRefChassisMaster As ChassisMaster
                objRefChassisMaster = New ChassisMasterFacade(User).Retrieve(objInvoice.EndCustomer.RefChassisNumberID)
                If objRefChassisMaster Is Nothing Then
                    InvoiceLine.Append(tab)  '-- Empty column
                Else
                    InvoiceLine.Append(objRefChassisMaster.ChassisNumber.Replace(tab, " ") & tab)  '-- Ref chassis number
                End If
                InvoiceLine.Append(" " & tab)   '-- Code

                InvoiceLine.Append(objInvoice.EndCustomer.Customer.Code.Replace(tab, " ") & tab)   '-- Code
                If UCase(objInvoice.EndCustomer.AreaViolationFlag) = "X" Then
                    Dim objAreaVioPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
                    Dim objAreaVioPatMeth As PaymentMethod = objAreaVioPayMethFacade.Retrieve(objInvoice.EndCustomer.AreaViolationPaymentMethodID)
                    InvoiceLine.Append(objAreaVioPatMeth.Code.Replace(tab, " ") & tab)   '-- Wilayah TOP
                    InvoiceLine.Append(Format(objInvoice.EndCustomer.AreaViolationyAmount, "0") & tab)  '-- Wilayah amount
                    InvoiceLine.Append(objInvoice.EndCustomer.AreaViolationBankName.Replace(tab, " ") & tab)  '-- Wilayah bank name
                    InvoiceLine.Append(objInvoice.EndCustomer.AreaViolationGyroNumber.Replace(tab, " ") & tab)  '-- Wilayah giro#
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                End If
                If UCase(objInvoice.EndCustomer.PenaltyFlag) = "X" Then
                    Dim objPenaltyPayMethFacade As PaymentMethodFacade = New PaymentMethodFacade(User)
                    Dim objPenaltyPatMeth As PaymentMethod = objPenaltyPayMethFacade.Retrieve(objInvoice.EndCustomer.PenaltyPaymentMethodID)
                    InvoiceLine.Append(objPenaltyPatMeth.Code.Replace(tab, " ") & tab)  '-- Disc TOP
                    InvoiceLine.Append(Format(objInvoice.EndCustomer.PenaltyAmount, "0") & tab)  '-- Disc amount
                    InvoiceLine.Append(objInvoice.EndCustomer.PenaltyBankName.Replace(tab, " ") & tab)  '-- Disc bank name
                    InvoiceLine.Append(objInvoice.EndCustomer.PenaltyGyroNumber.Replace(tab, " ") & tab)  '-- Disc giro#
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                    InvoiceLine.Append(tab)  '-- Empty column
                End If

                If UCase(objInvoice.EndCustomer.ReferenceLetterFlag) = "X" Then
                    InvoiceLine.Append(objInvoice.EndCustomer.ReferenceLetter.Replace(tab, " ") & tab)  '-- Letter
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                End If
                InvoiceLine.Append(IIf(objInvoice.EndCustomer.SaveBy <> "", UserInfo.Convert(objInvoice.EndCustomer.SaveBy.Replace(tab, " ")), "") & tab)  '-- Dibuat oleh
                InvoiceLine.Append(Format(objInvoice.EndCustomer.SaveTime, "ddMMyyyy") & tab)  '-- Tgl dibuat
                InvoiceLine.Append(IIf(objInvoice.EndCustomer.ValidateBy <> "", UserInfo.Convert(objInvoice.EndCustomer.ValidateBy.Replace(tab, " ")), "") & tab)  '-- Divalidasi oleh
                InvoiceLine.Append(Format(objInvoice.EndCustomer.ValidateTime, "ddMMyyyy") & tab)
                InvoiceLine.Append(objInvoice.EndCustomer.Customer.PrintRegion.Replace(tab, " ") & tab)

                'Start  :Add MCP Flag;by:dna;on:20110623;for:rina;
                If Not IsNothing(objInvoice.EndCustomer.Customer.MyCustomerRequest) AndAlso objInvoice.EndCustomer.Customer.MyCustomerRequest.ID > 0 Then
                    If objInvoice.EndCustomer.Customer.MyCustomerRequest.MCPStatus = EnumMCPStatus.MCPStatus.NotVerifiedMCP Then
                        InvoiceLine.Append("X" & tab)
                    Else
                        InvoiceLine.Append("" & tab)
                    End If
                Else
                    InvoiceLine.Append("" & tab)
                End If

                If Not IsNothing(objInvoice.EndCustomer.MCPHeader) Then
                    InvoiceLine.Append(objInvoice.EndCustomer.MCPHeader.ReferenceNumber & tab)
                    InvoiceLine.Append(objInvoice.EndCustomer.MCPHeader.LetterDate.ToString("ddMMyyyy") & tab)
                Else
                    InvoiceLine.Append("" & tab)
                    InvoiceLine.Append("" & tab)
                End If

                '' AdddSpkNumber
                If Not IsNothing(objInvoice.EndCustomer) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader) Then
                    InvoiceLine.Append(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SPKNumber & tab)
                    InvoiceLine.Append(objInvoice.EndCustomer.SPKFaktur.SPKHeader.CreatedTime.ToString("ddMMyyyy") & tab)
                Else
                    InvoiceLine.Append("" & tab)
                    InvoiceLine.Append("" & tab)
                End If

                If Not IsNothing(objInvoice.FleetFaktur) AndAlso Not IsNothing(objInvoice.FleetFaktur.FleetRequest) Then
                    InvoiceLine.Append(objInvoice.FleetFaktur.FleetRequest.NoRegRequest & tab)
                Else
                    InvoiceLine.Append("" & tab)
                End If


                If Not IsNothing(objInvoice.EndCustomer) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader) AndAlso Not IsNothing(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch) Then
                    InvoiceLine.Append(objInvoice.EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.DealerBranchCode & tab)
                Else
                    InvoiceLine.Append("" & tab)
                End If

                If objInvoice.EndCustomer.IsTemporary = CType(EnumEndCustomer.TemporaryFaktur.Temporary, Short) Then
                    InvoiceLine.Append("X" & tab) '-- if it is temporary faktur
                Else
                    InvoiceLine.Append(tab)  '-- Empty column
                End If
                InvoiceLine.Append(CMHeader.ChassisMaster.ChassisNumber & tab)
            Else
            End If
            sw.WriteLine(InvoiceLine.ToString())  '-- Write Invoice line
        Next
    End Sub

    Private Function PopulateInvoice(ByVal mode As Integer) As ArrayList
        Dim oExArgs As New System.Collections.ArrayList

        Dim arlWSMData As ArrayList = CType(sessHelper.GetSession(SessionWSMData), ArrayList)
        For Each _chsMasterClaimHeader As ChassisMasterClaimHeader In arlWSMData
            Dim _chsMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(_chsMasterClaimHeader.ChassisNumberReplacement)
            If mode = 0 Then
                If _chsMaster.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi Then
                    oExArgs.Add(_chsMasterClaimHeader)
                End If
            End If
            If mode = 1 Then
                If _chsMaster.FakturStatus = EnumChassisMaster.FakturStatus.Proses Then
                    oExArgs.Add(_chsMasterClaimHeader)
                End If
            End If
            If mode = 3 Then
                oExArgs.Add(_chsMasterClaimHeader)
            End If
        Next

        Return oExArgs
    End Function

    Private Sub DnLoadInvoiceDataChasisProfile(ByRef sw As StreamWriter, ByVal mode As String)
        Dim tab As Char  '-- Separator character <Tab>
        Dim InvoiceLine As StringBuilder = New StringBuilder  '-- Invoice line in text file
        Dim InvoiceResList As ArrayList = PopulateInvoice(mode)
        Dim temp As String = String.Empty
        tab = Chr(9)
        For Each _chsMasterClaimHeader As ChassisMasterClaimHeader In InvoiceResList
            For Each objChassisMasterProfile As ChassisMasterProfile In _chsMasterClaimHeader.ChassisMaster.ChassisMasterProfiles
                If objChassisMasterProfile.ProfileValue.Trim <> "" Then
                    InvoiceLine.Append(_chsMasterClaimHeader.ChassisNumberReplacement + tab)
                    InvoiceLine.Append(objChassisMasterProfile.ProfileHeader.Code + tab)
                    temp = objChassisMasterProfile.ProfileValue.Trim
                    InvoiceLine.Append(temp.Trim)
                    InvoiceLine.Append(vbNewLine)
                    temp = String.Empty
                End If
            Next
        Next
        sw.WriteLine(InvoiceLine.ToString())
    End Sub

    Private Sub InProcessStatus()
        '-- Change invoice status from 'Konfirmasi' to 'Proses'

        Dim ConfirmList As New ArrayList  '-- List of confirmed invoices

        '-- Iterate invoices selected
        For Each _chsMasterClaimHeader As ChassisMasterClaimHeader In ChassisList
            Dim item As ChassisMaster = New ChassisMasterFacade(User).Retrieve(_chsMasterClaimHeader.ChassisNumberReplacement)

            '-- Only invoices with status 'Konfirmasi' and with End Customer defined
            If item.FakturStatus = EnumChassisMaster.FakturStatus.Konfirmasi Then
                If Not IsNothing(item.EndCustomer) Then

                    item.FakturStatus = EnumChassisMaster.FakturStatus.Proses  '-- Change invoice status
                    item.EndCustomer.DownloadBy = UserInfo.Convert(User.Identity.Name)  '-- Set its downloader
                    item.EndCustomer.DownloadTime = Date.Now  '-- Set its download date

                    ConfirmList.Add(item)
                End If
            End If
        Next

        '-- If there exists at least a confirmed invoice selected then do update transaction
        Dim ChassisFac As New ChassisMasterFacade(User)
        ChassisFac.UpdateTransaction(ConfirmList)  '-- Update list of confirmed invoices

    End Sub

    Private Function SendEmail(ByVal objClaim As ChassisMasterClaimHeader, ByVal emailTo As String, ByVal emailBcc As String, ByVal isFromCancelled As Boolean, ByRef msg As String) As Integer
        Dim retValue As Integer = 0 '0 = gagal ; 1 = sukses

        Dim strSmtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim objEmail As DNetMail = New DNetMail(strSmtp)
        Dim strFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        Dim strSubject As String = "[MMKSI-DNet] CBUReturn - Notifikasi Unit Replacement"
        Dim strTo As String = emailTo
        Dim strCC As String = ""
        Dim strBcc As String = emailBcc
        If strTo.Trim = String.Empty Then
            msg += "Email Penerima tidak ada. \n"
            Exit Function
        End If

        Dim strBody As String = GetTemplateEmail(objClaim, isFromCancelled)
        Try
            objEmail.sendMail(strTo, strCC, strBcc, strFrom, strSubject, Mail.MailFormat.Html, strBody)
            retValue = 1
        Catch ex As Exception
            msg += String.Concat(ex.Message.Replace("""", ""), " \n")
            retValue = 0
        End Try
        Return retValue
    End Function

    Public Function GetTemplateEmail(ByVal objClaim As ChassisMasterClaimHeader, ByVal isFromCanceled As Boolean) As String
        Dim body As String = ""
        Dim statusFakturList As String = String.Format("{0},{1},{2}", _
                         CInt(EnumChassisMaster.FakturStatus.Validasi), CInt(EnumChassisMaster.FakturStatus.Konfirmasi), CInt(EnumChassisMaster.FakturStatus.Proses))

        If isFromCanceled Then
            body = ReadFileTemplate("CBUReturnTemplateA.htm") _
                    .Replace("[ChassisNumberDefect]", objClaim.ChassisMaster.ChassisNumber) _
                    .Replace("[ClaimNumber]", objClaim.ClaimNumber)
        ElseIf statusFakturList.Contains(objClaim.ChassisMaster.FakturStatus) Then
            body = ReadFileTemplate("CBUReturnTemplateB.htm") _
                    .Replace("[ChassisNumberDefect]", objClaim.ChassisMaster.ChassisNumber) _
                    .Replace("[FakturStatus]", objClaim.ChassisMaster.FakturStatus) _
                    .Replace("[ChassisNumberReplacement]", objClaim.ChassisNumberReplacement)
        ElseIf objClaim.StatusProcessRetur = EnumCBUReturn.StatusProsesRetur.Proses_Faktur_Chassis_Pengganti Then
            body = ReadFileTemplate("CBUReturnTemplateD.htm") _
                    .Replace("[ChassisNumberReplacement]", objClaim.ChassisNumberReplacement)
        Else
            body = ReadFileTemplate("CBUReturnTemplateC.htm") _
                    .Replace("[ChassisNumberDefect]", objClaim.ChassisMaster.ChassisNumber) _
                    .Replace("[FakturStatus]", objClaim.ChassisMaster.FakturStatus) _
                    .Replace("[ChassisNumberReplacement]", objClaim.ChassisNumberReplacement)

        End If
        Return body
    End Function

    Public Function GetEmail(ByVal category As String) As String
        Dim email As String = ""
        Dim objAppConf As AppConfig

        Select Case category
            Case "RSD"
                objAppConf = objAppConfFacade.Retrieve(CBUReturnEmailToRSD)
                email = objAppConf.Value
            Case "WSD"
                objAppConf = objAppConfFacade.Retrieve(CBUReturnEmailToWSD)
                email = objAppConf.Value
            Case "VCD"
                objAppConf = objAppConfFacade.Retrieve(CBUReturnEmailToVCD)
                email = objAppConf.Value
            Case "EmailBCC"
                objAppConf = objAppConfFacade.Retrieve(EmailBCC)
                email = objAppConf.Value
        End Select

        Return email
    End Function

    Public Function ReadFileTemplate(ByVal filename As String) As String
        Dim fileReader As String
        fileReader = File.ReadAllText(Path.Combine(templateDir, filename))
        Return fileReader
    End Function

    Private Sub SaveToWSLog(ByVal listBody As List(Of String))
        Dim func As New WsLogFacade(Me.User)
        For Each iBody As String In listBody
            Try
                Dim wLog As New WsLog
                wLog.Body = iBody
                wLog.Source = "Internal"
                wLog.Status = "True"
                wLog.Message = "Success"
                func.Insert(wLog)
            Catch
            End Try
        Next
    End Sub
End Class