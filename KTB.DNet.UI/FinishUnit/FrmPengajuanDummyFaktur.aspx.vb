#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports System.Linq
Imports System.Collections
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.Configuration
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO
#End Region

Public Class FrmPengajuanDummyFaktur
    Inherits System.Web.UI.Page

#Region "Private Variables"
    Private ssHelper As SessionHelper = New SessionHelper
    Private arlCheckedItemColl As ArrayList = New ArrayList
    Dim _arrDetailDel As New ArrayList

    Private _guid As String
    Public Property sessGuid() As String
        Get
            Return _guid
        End Get
        Set(ByVal value As String)
            _guid = value
        End Set
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hdnValsessGuid.Value = Guid.NewGuid.ToString

            CheckUserPrivilege()
            BindDDLKategory()
            BindDDLModel()
            BindDDLTipe()
            BindDDLWarna()
            InitData()
            lblCustomerCode.Attributes("onClick") = "ShowPPTujuanSelection();"
        End If
    End Sub


#Region "Custom Method"
    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Faktur_Kendaraan_Buat_Permohonan_Temp_FK_Simpan_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FAKTUR KENDARAAN - Buat Permohonan Temporary Faktur")
        End If
    End Sub

    Private Function IsTransBlocked() As Boolean
        Dim isDealerYana = False
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)

        'Dim crtDS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'crtDS.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerSystems), "Dealer.ID", MatchType.Exact, objDealer.ID))
        'Dim arrDealerSystem As ArrayList = New DealerSystemsFacade(User).Retrieve(crtDS)

        'If arrDealerSystem.Count > 0 Then
        '    Dim objDealerSystem As DealerSystems = arrDealerSystem(0)
        '    If objDealerSystem.SystemID = 2 Then
        '        isDealerYana = True
        '    End If
        'End If

        'If Not isDealerYana Then
        '    Return True
        'Else
        Dim nVal As Integer = New DealerFacade(User).ValidateBlockedTransactionControl(CType(Session("DEALER"), Dealer).ID, _
        CType(EnumDealerTransType.DealerTransKind.TemporaryFaktur, String))
        Return nVal > 0
        'End If

    End Function

    Private Sub InitData()
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(ssHelper.GetSession("LOGINUSERINFO"), UserInfo)

        ViewState.Add("SortColumn", "ChassisNumber")
        ViewState.Add("SortDirection", Sort.SortDirection.ASC)

        If Not objDealer Is Nothing Then
            lblKodeDealer.Text = objDealer.DealerCode
            lblNamaDealer.Text = objDealer.DealerName

            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If IsTransBlocked() Then
                    Server.Transfer("../FrmAccessDenied.aspx?mess=SAAT%20INI%20MODUL%20BUAT%20PERMOHONAN%20TEMPORARY%20FAKTUR%20DALAM%20STATUS%20TIDAK%20AKTIF.%20SILAHKAN%20MENGHUBUNGI%20TIM%20RETAIL%20SALES%20MMKSI")
                End If

                If Not Session("PrevPage") Is Nothing Then
                    If Not IsNothing(Request.QueryString("guid")) AndAlso CType(Request.QueryString("guid"), String) <> "" Then
                        hdnValsessGuid.Value = CType(Request.QueryString("guid"), String)
                        arlCheckedItemColl = CType(Session(CType(Request.QueryString("guid"), String) & "_" & "PrevDummyFaktur"), ArrayList)
                        setSession("DummyFaktur", arlCheckedItemColl)

                        txtCustomerCode.Text = CType(Session(CType(Request.QueryString("guid"), String) & "_" & "PrevCustomer"), Customer).Code
                        GetCustomerInfo(txtCustomerCode.Text)

                        arlCheckedItemColl = CType(Session(CType(Request.QueryString("guid"), String) & "_" & "sessPrevCheckedItemAllPages"), ArrayList)
                        setSession("sessCheckedItemAllPages", arlCheckedItemColl)

                        dtgPengajuanFaktur.DataSource = arlCheckedItemColl
                        dtgPengajuanFaktur.CurrentPageIndex = 0
                        dtgPengajuanFaktur.VirtualItemCount = arlCheckedItemColl.Count
                        dtgPengajuanFaktur.DataBind()

                        btnSimpan.Enabled = False
                        btnUpdateProfil.Enabled = True
                        btnCari.Enabled = False
                        lblCustomerCode.Enabled = False
                    End If

                    If Not getSession("UpdateProfile") Is Nothing Then
                        'If Not getSession("UpdateProfile") Is Nothing Then
                        btnSimpan.Enabled = False
                        If Not getSession("IsSucceedProfileFaktur") Is Nothing Then
                            'If Not getSession("IsSucceedProfileFaktur") Is Nothing Then
                            If getSession("IsSucceedProfileFaktur") = 0 Then
                                'If getSession("IsSucceedProfileFaktur") = 0 Then
                                'setSession(sessMODE, "update")
                                setSession("sessMODE", "update")
                                btnSimpan.Enabled = True
                            End If
                        End If
                    End If
                Else
                    setSession("DummyFaktur", arlCheckedItemColl)
                    Dim strCustomerRequestId As String
                    Dim CustomerRequestId As String
                    Dim CustomerCode As String
                    If Not Request.QueryString("qxctrvvyuotrpn") Is Nothing Then
                        strCustomerRequestId = Request.QueryString("qxctrvvyuotrpn")
                        CustomerRequestId = strCustomerRequestId.Split(";")(0)
                        CustomerCode = strCustomerRequestId.Split(";")(1)
                        txtCustomerCode.Text = CustomerCode
                        If Me.txtCustomerCode.Text.Trim <> String.Empty Then
                            GetCustomerInfo(txtCustomerCode.Text)
                        End If
                    End If
                    'setSession(sessMODE, "insert")
                    setSession("sessMODE", "insert")
                End If
            End If
        End If
    End Sub

    Private Function isValidCM() As Boolean
        Dim aCMs As ArrayList = CType(getSession("DummyFaktur"), ArrayList)
        Dim sError As String = String.Empty

        For Each oCM As v_RetrieveDummyFaktur In aCMs
            If oCM.isValidToCreateFaktur() = False Then
                sError &= IIf(sError = String.Empty, "", ", ") & oCM.ChassisNumber
            End If
        Next
        If sError <> String.Empty Then
            MessageBox.Show("Nomor Rangka " & sError & " Sudah Diretur")
        End If
        Return (sError = String.Empty)
    End Function

    Private Function IsCustomerAvailaibleForLoginDealer(ByVal objCust As Customer, ByVal loginDealer As Dealer) As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.ID", MatchType.Exact, objCust.ID))
        Dim objCustomerDealerFacade As CustomerDealerFacade = New CustomerDealerFacade(User)
        Dim objList As ArrayList = objCustomerDealerFacade.Retrieve(criterias)
        If Not IsNothing(objList) AndAlso objList.Count > 0 Then
            For Each item As CustomerDealer In objList
                If item.Dealer.DealerGroup.ID = loginDealer.DealerGroup.ID Then
                    If item.Customer.RowStatus <> DBRowStatus.Deleted Then
                        Return True
                    End If
                End If
            Next
        Else
            Return False
        End If
        Return False
    End Function

    Private Sub BindCustomer(ByVal objCust As Customer)
        If Len(objCust.Code) > 1 Then
            txtCustomerCode.Text = objCust.Code
            lblName.Text = objCust.Name1
            lblName2.Text = objCust.Name2
            lblGedung.Text = objCust.Name3
            lblAlamat.Text = objCust.Alamat
            lblKelurahan.Text = objCust.Kelurahan
            lblKecamatan.Text = objCust.Kecamatan
            lblKodePos.Text = objCust.PostalCode
            If Not IsNothing(objCust.City) Then
                lblPropinsi.Text = objCust.City.Province.ProvinceName
            End If

            If Not IsNothing(objCust.MyCustomerRequest) Then
                If Not IsNothing(objCust.MyCustomerRequest.GetCustomerRequestProfile("NOKTP")) Then
                    lblNoKTP.Text = objCust.MyCustomerRequest.GetCustomerRequestProfile("NOKTP").ProfileValue
                End If
            Else
                lblNoKTP.Text = String.Empty
            End If
            lblKodya.Text = objCust.City.CityName
            btnSimpan.Enabled = True
        Else
            txtCustomerCode.Text = String.Empty
            lblName.Text = String.Empty
            lblName2.Text = String.Empty
            lblGedung.Text = String.Empty
            lblAlamat.Text = String.Empty
            lblKelurahan.Text = String.Empty
            lblKecamatan.Text = String.Empty
            lblKodePos.Text = String.Empty
            lblPropinsi.Text = String.Empty
            lblNoKTP.Text = String.Empty
            lblKodya.Text = String.Empty
            btnSimpan.Enabled = False
        End If
    End Sub

    Private Sub BindDDLKategory()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim arrayListCategory As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
        Dim blankItem As New ListItem("Silahkan Pilih", "-1")
        ddlKategory.Items.Add(blankItem)
        For Each item As Category In arrayListCategory
            Dim listItem As New ListItem(item.CategoryCode, item.ID)
            listItem.Selected = False
            ddlKategory.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindDDLModel()
        ddlModel.Items.Clear()
        If ddlKategory.SelectedIndex <> "-1" Then
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlModel, ddlKategory.SelectedItem.Text)
            BindDDLTipe()
            ddlModel.SelectedIndex = 0
        Else
            ddlModel.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
        End If
    End Sub

    Private Sub BindDDLTipe()
        ddlTipe.Items.Clear()
        If ddlModel.SelectedValue <> "-1" Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Category.ID", MatchType.Exact, ddlKategory.SelectedValue))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Status", MatchType.Exact, "A"))
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlModel.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("VechileTypeCode")) Then
                sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            ddlTipe.DataSource = New VechileTypeFacade(User).Retrieve(criterias, sortColl)
            ddlTipe.DataTextField = "VechileTypeCode"
            ddlTipe.DataValueField = "ID"
            ddlTipe.DataBind()
        End If
        ddlTipe.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        BindDDLWarna()
    End Sub

    Private Sub BindDDLWarna()
        ddlWarna.Items.Clear()
        If ddlTipe.SelectedIndex <> 0 Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.ID", MatchType.Exact, ddlTipe.SelectedValue))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("ColorIndName")) Then
                sortColl.Add(New Sort(GetType(VechileColor), "ColorIndName", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            ddlWarna.DataSource = New VechileColorFacade(User).Retrieve(criterias, sortColl)
            ddlWarna.DataTextField = "ColorIndName"
            ddlWarna.DataValueField = "ID"
            ddlWarna.DataBind()
            For Each item As ListItem In ddlWarna.Items
                item.Text = item.Text.ToUpper
            Next
            'Else
            '    ddlWarna.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
        End If
        ddlWarna.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
        ddlWarna.SelectedIndex = 0
    End Sub

    Private Sub setViewState()
        Dim objCustomerFacade As CustomerFacade = New CustomerFacade(User)
        If hdnCustomerCode.Value <> "" Then
            Dim arrValue As String() = hdnCustomerCode.Value.Split(";")
            txtCustomerCode.Text = arrValue(0)
            lblName.Text = arrValue(1)
            lblGedung.Text = arrValue(2)
            lblAlamat.Text = arrValue(3)
            lblKelurahan.Text = arrValue(4)
            lblKecamatan.Text = arrValue(5)
            lblKodePos.Text = arrValue(6)
            lblKodya.Text = arrValue(7)
            lblPropinsi.Text = arrValue(8)
            lblName2.Text = arrValue(12)
            lblNoKTP.Text = arrValue(13)

            ViewState("txtCustomerCode") = txtCustomerCode.Text
            ViewState("lblName") = lblName.Text
            ViewState("lblAlamat") = lblAlamat.Text
            ViewState("lblGedung") = lblGedung.Text
            ViewState("lblKelurahan") = lblKelurahan.Text
            ViewState("lblKecamatan") = lblKecamatan.Text
            ViewState("lblKodePos") = lblKodePos.Text
            ViewState("lblKodya") = lblKodya.Text
            ViewState("lblPropinsi") = lblPropinsi.Text
            ViewState("lblName2") = lblName2.Text
            ViewState("lblNoKTP") = lblNoKTP.Text

            Dim objCust As Customer = objCustomerFacade.Retrieve(txtCustomerCode.Text)
            If Not IsNothing(objCust) AndAlso objCust.ID > 0 Then
                setSession("Customer", objCust)
            End If

        End If
    End Sub

    Private Sub loadViewState()
        txtCustomerCode.Text = ViewState("txtCustomerCode")
        lblName.Text = ViewState("lblName")
        lblAlamat.Text = ViewState("lblAlamat")
        lblGedung.Text = ViewState("lblGedung")
        lblKelurahan.Text = ViewState("lblKelurahan")
        lblKecamatan.Text = ViewState("lblKecamatan")
        lblKodePos.Text = ViewState("lblKodePos")
        lblKodya.Text = ViewState("lblKodya")
        lblPropinsi.Text = ViewState("lblPropinsi")
        lblName2.Text = ViewState("lblName2")
        lblNoKTP.Text = ViewState("lblNoKTP")
    End Sub

    Private Function GetCustomerInfo(ByVal code As String) As Boolean
        Dim bcheck As Boolean = True
        Dim objCustomerFacade As CustomerFacade = New CustomerFacade(User)
        Dim objCust As Customer = objCustomerFacade.Retrieve(code)
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        If objCust.ID > 0 Then
            If IsCustomerAvailaibleForLoginDealer(objCust, objDealer) Then
                'setSession(sessCustomer, objCust)
                setSession("Customer", objCust)
                bcheck = True
                BindCustomer(objCust)
                If objCust.CreatedTime < New Date(2011, 6, 1) Then
                    MessageBox.Show("Data konsumen tidak dapat digunakan, silakan ajukan ulang.")
                    bcheck = False
                End If
            Else
                MessageBox.Show("Customer tidak terdaftar di dealer anda.")
                bcheck = False
            End If
        Else
            MessageBox.Show("Customer tidak ditemukan")
            bcheck = False
        End If
        Return bcheck
    End Function

    Private Function GetCheckedItemAllPages() As ArrayList
        Dim arlCheckedItemAllPages As ArrayList = New ArrayList
        Dim arrGrid As ArrayList = CType(getSession("DummyFaktur"), ArrayList)
        Dim intPageCount As Integer = 0
        If Not IsNothing(ViewState.Item("GridPageCount")) Then
            intPageCount = CType(ViewState.Item("GridPageCount"), Integer)
        End If
        If Not IsNothing(Request.QueryString("GridPageCount")) AndAlso Not (Request.QueryString("GridPageCount") = "") Then
            intPageCount = CType(Request.QueryString("GridPageCount"), Integer)
            ViewState.Add("GridPageCount", intPageCount)
        End If

        If chkAllPages.Checked Then
            Dim objVRDFFac As v_RetrieveDummyFakturFacade = New v_RetrieveDummyFakturFacade(User)
            arlCheckedItemAllPages = objVRDFFac.Retrieve(GetCriteriaVDS())

            Dim nGridCount As Integer = intPageCount - 1
            For idx As Integer = 0 To nGridCount
                Dim currentPage As String = CType(idx, String)
                Dim arrGrid2 As ArrayList = CType(getSession("sessProcess2" + currentPage), ArrayList)
                If Not IsNothing(arrGrid2) Then
                    For i As Integer = 0 To arrGrid2.Count - 1
                        Dim model As v_RetrieveDummyFaktur = (CType(arrGrid2(i), v_RetrieveDummyFaktur))
                        Dim query As v_RetrieveDummyFaktur = (From obj As v_RetrieveDummyFaktur In arlCheckedItemAllPages
                                    Where obj.ID = model.ID
                                    Select obj).FirstOrDefault
                        If Not IsNothing(query) AndAlso query.ID > 0 Then
                            arlCheckedItemAllPages.Remove(query)
                        End If
                    Next i
                End If
            Next

            Dim nIndeks As Integer
            For Each dtgItem As DataGridItem In dtgPengajuanFaktur.Items
                nIndeks = dtgItem.ItemIndex
                Dim model As v_RetrieveDummyFaktur = CType(arrGrid(nIndeks), v_RetrieveDummyFaktur)
                Dim chkItem As CheckBox = CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox)
                If chkItem.Checked = False Then
                    arlCheckedItemAllPages.Remove(model)
                End If
            Next
        Else
            Dim nGridCount As Integer = intPageCount - 1
            For idx As Integer = 0 To nGridCount
                Dim currentPage As String = CType(idx, String)
                Dim arrGrid2 As ArrayList = CType(getSession("sessProcess" + currentPage), ArrayList)
                If Not IsNothing(arrGrid2) Then
                    For i As Integer = 0 To arrGrid2.Count - 1
                        arlCheckedItemAllPages.Add(arrGrid2(i))
                    Next i
                End If
            Next
        End If
        Return arlCheckedItemAllPages

    End Function

    Private Sub SetCheckedItemAllPages()
        Dim TotRow As Integer = 0
        Dim srtColumn As String = ""
        Dim srtDirection As Sort.SortDirection
        srtColumn = ViewState.Item("SortColumn")
        srtDirection = ViewState.Item("SortDirection")

        Dim objVRDFFac As v_RetrieveDummyFakturFacade = New v_RetrieveDummyFakturFacade(User)
        Dim arlPerPages As ArrayList = New ArrayList

        Dim arlCheckedItemAllPages As ArrayList = New ArrayList
        Dim nPageGridCount As Integer = dtgPengajuanFaktur.PageCount - 1
        For idx As Integer = 0 To nPageGridCount
            Dim currentPage As String = CType(idx, String)
            arlPerPages = objVRDFFac.RetrieveActiveList(GetCriteriaVDS(), idx + 1, dtgPengajuanFaktur.PageSize, TotRow, srtColumn, srtDirection)
            setSession("sessProcess" + currentPage, arlPerPages)
            setSession("sessProcess2" + currentPage, New ArrayList)
        Next

        '-- Check all checkbox at current page
        For Each dtgItem As DataGridItem In dtgPengajuanFaktur.Items
            Dim chkItem As CheckBox = CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox)
            chkItem.Checked = True
        Next

    End Sub

    Private Sub ClearCheckedItemAllPages()
        Dim arlPerPages As ArrayList = New ArrayList
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(v_RetrieveDummyFaktur), ViewState.Item("SortColumn"), ViewState.Item("SortDirection")))
        Dim iColl As ICollection = CType(sortCol, SortCollection)

        Dim objVRDFFac As v_RetrieveDummyFakturFacade = New v_RetrieveDummyFakturFacade(User)
        Dim arlVRDF As ArrayList = New ArrayList
        arlVRDF = objVRDFFac.Retrieve(GetCriteriaVDS(), iColl)

        Dim arlCheckedItemAllPages As ArrayList = New ArrayList
        Dim nPageGridCount As Integer = dtgPengajuanFaktur.PageCount - 1
        For idx As Integer = 0 To nPageGridCount
            Dim currentPage As String = CType(idx, String)
            setSession("sessProcess" + currentPage, arlPerPages)
            setSession("sessProcess2" + currentPage, New ArrayList)
        Next

        '-- UnCheck all checkbox at current page
        For Each dtgItem As DataGridItem In dtgPengajuanFaktur.Items
            Dim chkItem As CheckBox = CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox)
            chkItem.Checked = False
        Next
    End Sub

    Private Function GetCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(getSession("DummyFaktur"), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dtgPengajuanFaktur.Items
            nIndeks = dtgItem.ItemIndex
            Dim objCM As v_RetrieveDummyFaktur = CType(arrGrid(nIndeks), v_RetrieveDummyFaktur)
            If CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                arlCheckedItem.Add(objCM)
            End If
        Next

        Return arlCheckedItem
    End Function

    Private Function GetUnCheckedItem() As ArrayList
        Dim arrGrid As ArrayList = CType(getSession("DummyFaktur"), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dtgPengajuanFaktur.Items
            nIndeks = dtgItem.ItemIndex
            Dim objCM As v_RetrieveDummyFaktur = CType(arrGrid(nIndeks), v_RetrieveDummyFaktur)
            If Not CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                arlCheckedItem.Add(objCM)
            End If
        Next

        Return arlCheckedItem
    End Function

    Private Sub UpdatePendingDescAndLastUpdateProfile(ByVal chassisNumber As String)
        Dim lastUpdateProfile = "1753-01-01 00:00:00.000"
        Dim facade As New ChassisMasterFacade(User)
        Dim result = facade.ExecuteSPChassisMasterProfile(chassisNumber, "", Convert.ToDateTime(lastUpdateProfile))
    End Sub

    Private Function CopyCustomerDealerToDealerSPK(ByVal oCust As Customer) As Boolean
        Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
        If Not IsNothing(oCust) AndAlso oCust.ID > 1 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(CustomerDealer), "Customer.ID", MatchType.Exact, oCust.ID))
            Dim objCustomerDealerFacade As CustomerDealerFacade = New CustomerDealerFacade(User)
            Dim objList As ArrayList = objCustomerDealerFacade.Retrieve(criterias)
            If objList.Count > 0 Then
                Dim isExist As Boolean = False
                For Each item As CustomerDealer In objList
                    If item.Dealer.ID = objDealer.ID Then
                        isExist = True
                    End If
                Next
                If isExist = False Then
                    Dim newCustDealer As CustomerDealer = CType(objList(0), CustomerDealer)
                    newCustDealer.Dealer = objDealer
                    objCustomerDealerFacade.Insert(newCustDealer)
                End If
            End If
        End If
    End Function

    Private Sub SaveChassisMasterProfile(arlCheckedItemColl As ArrayList)
        Dim oCM As v_RetrieveDummyFaktur
        Dim oCMPFac As New ChassisMasterProfileFacade(User)
        Dim oCMP As ChassisMasterProfile
        Dim oPHFac As New ProfileHeaderFacade(User)
        Dim oPHJenis As ProfileHeader = oPHFac.Retrieve("CBU_JENISKEND")
        Dim oPHModel As ProfileHeader = oPHFac.Retrieve("CBU_MODELKEND")
        Dim oPG As ProfileGroup
        Dim oPGFac As New ProfileGroupFacade(User)
        Dim GroupCode As String = ""

        If Not IsNothing(oPHJenis) AndAlso oPHJenis.ID > 0 Then
            For i As Integer = 0 To arlCheckedItemColl.Count - 1
                oCM = CType(arlCheckedItemColl(i), v_RetrieveDummyFaktur)

                GroupCode = "cust_prf_" & oCM.Category.CategoryCode.ToLower
                oPG = oPGFac.Retrieve(GroupCode)

                '1. Proses by Jenis
                oCMP = GetCMProfile(oCM, oPG, oPHJenis)
                oCMP.ChassisMaster = oCM.ChassisMaster
                oCMP.ProfileGroup = oPG
                oCMP.ProfileHeader = oPHJenis

                Dim strVehicleKindGroupCode As String = String.Empty
                If Not IsNothing(oCM.ChassisMaster.VehicleKind) Then
                    If Not IsNothing(oCM.ChassisMaster.VehicleKind.VehicleKindGroup) Then
                        strVehicleKindGroupCode = oCM.ChassisMaster.VehicleKind.VehicleKindGroup.Code
                    End If
                End If
                oCMP.ProfileValue = strVehicleKindGroupCode
                If oCMP.ID < 1 Then
                    oCMPFac.Insert(oCMP)
                Else
                    oCMPFac.Update(oCMP)
                End If

                '2. Proses by Model
                oCMP = GetCMProfile(oCM, oPG, oPHModel)
                oCMP.ChassisMaster = oCM.ChassisMaster
                oCMP.ProfileGroup = oPG
                oCMP.ProfileHeader = oPHModel

                strVehicleKindGroupCode = String.Empty
                If Not IsNothing(oCM.ChassisMaster.VehicleKind) Then
                    strVehicleKindGroupCode = oCM.ChassisMaster.VehicleKind.Code
                End If
                oCMP.ProfileValue = strVehicleKindGroupCode
                If oCMP.ID < 1 Then
                    oCMPFac.Insert(oCMP)
                Else
                    oCMPFac.Update(oCMP)
                End If
            Next
        End If
    End Sub

    Private Function GetCMProfile(ByRef oCM As v_RetrieveDummyFaktur, ByRef oPG As ProfileGroup, ByRef oPH As ProfileHeader) As ChassisMasterProfile
        Dim oCMPFac As New ChassisMasterProfileFacade(User)
        Dim cCMP As New CriteriaComposite(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aCMP As ArrayList


        cCMP.opAnd(New Criteria(GetType(ChassisMasterProfile), "ChassisMaster.ID", MatchType.Exact, oCM.ID))
        cCMP.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileGroup.ID", MatchType.Exact, oPG.ID))
        cCMP.opAnd(New Criteria(GetType(ChassisMasterProfile), "ProfileHeader.ID", MatchType.Exact, oPH.ID))
        aCMP = oCMPFac.Retrieve(cCMP)
        If aCMP.Count > 0 Then
            Return CType(aCMP(0), ChassisMasterProfile)
        Else
            Return New ChassisMasterProfile
        End If
    End Function

#End Region

    Private Sub ddlKategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlKategory.SelectedIndexChanged
        setViewState()
        BindDDLModel()
        loadViewState()
    End Sub

    Private Sub ddlMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlModel.SelectedIndexChanged
        setViewState()
        BindDDLTipe()
        loadViewState()
    End Sub

    Private Sub ddlTipe_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlTipe.SelectedIndexChanged
        setViewState()
        BindDDLWarna()
        loadViewState()
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        On Error GoTo errCheck_error
        Dim nResult As Integer

        setViewState()
        loadViewState()
        BinddtgPengajuanFaktur(dtgPengajuanFaktur.CurrentPageIndex)

        If Me.isValidCM() = False Then
            Exit Sub
        End If

        Dim oCust As Customer
        If txtCustomerCode.Text.Trim = "" Then
            MessageBox.Show("Kode Konsumen harus diisi")
            Exit Sub
        End If

        If Not getSession("Customer") Is Nothing Then
            'If (Not Session("Customer") Is Nothing) Then
            oCust = CType(getSession("Customer"), Customer)
            'oCust = CType(Session("Customer"), Customer)
        Else
            Dim objCustomerFacade As CustomerFacade = New CustomerFacade(User)
            oCust = objCustomerFacade.Retrieve(txtCustomerCode.Text.Trim)
        End If

        If Not IsNothing(oCust) Then
            If oCust.CreatedTime < New Date(2011, 6, 1) Then
                MessageBox.Show("Data konsumen tidak dapat digunakan, silahkan ajukan ulang.")
                Exit Sub
            End If
        End If

        If dtgPengajuanFaktur.Items.Count <= 0 Then
            MessageBox.Show("Pastikan terisi satu atau lebih data detail rangka sebelum melanjutkan proses")
            Exit Sub
        End If

        If Not IsNothing(oCust) Then
            ViewState.Add("GridPageCount", dtgPengajuanFaktur.PageCount)
            arlCheckedItemColl = GetCheckedItemAllPages()
            setSession("sessCheckedItemAllPages", arlCheckedItemColl)

            arlCheckedItemColl = CType(getSession("sessCheckedItemAllPages"), ArrayList)
            If (arlCheckedItemColl.Count > 0) Then
                SaveChassisMasterProfile(arlCheckedItemColl)

                Dim isTmp As Short = 1 ' Temporary di set true
                For i As Integer = 0 To arlCheckedItemColl.Count - 1
                    Dim chassis As v_RetrieveDummyFaktur = arlCheckedItemColl(i)
                    If Not IsNothing(chassis.ChassisMaster.EndCustomer) Then
                        If chassis.ChassisMaster.EndCustomer.IsTemporary <> isTmp Then
                            chassis.ChassisMaster.EndCustomer.IsTemporary = isTmp
                            arlCheckedItemColl(i) = chassis
                        End If
                    End If
                Next

                Dim oCustRequest As CustomerRequest = New CustomerRequestFacade(User).RetrieveCodeDesc(oCust.Code)
                If Not IsNothing(oCustRequest) AndAlso oCustRequest.ID > 0 Then
                    Dim oCustRequestProfile As CustomerRequestProfile = oCustRequest.GetCustomerRequestProfile("NOKTP")
                    If Not IsNothing(oCustRequestProfile) AndAlso oCustRequestProfile.ID > 0 Then
                        If Not IsNothing(oCustRequestProfile.ProfileValue) Then
                            If oCustRequest.Status1 <> CType(EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah, Short) Then
                                If oCustRequestProfile.ProfileValue.Trim.Length <= 5 Then
                                    MessageBox.Show("Faktur tidak bisa disimpan karena konsumen tidak punya data KTP/TDP. Silakan ajukan ulang konsumen tsb.")
                                    Exit Sub
                                End If
                            End If
                        Else
                            If oCustRequest.Status1 <> CType(EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah, Short) Then
                                MessageBox.Show("Faktur tidak bisa disimpan karena konsumen tidak punya data KTP/TDP. Silakan ajukan ulang konsumen tsb.")
                                Exit Sub
                            End If
                        End If
                    Else
                        If oCustRequest.Status1 <> CType(EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah, Short) Then
                            MessageBox.Show("Faktur tidak bisa disimpan karena konsumen tidak punya data KTP/TDP. Silakan ajukan ulang konsumen tsb.")
                            Exit Sub
                        End If
                    End If
                Else
                End If

                nResult = New EndCustomerFacade(User).InsertTransactionPengajuanDummyFaktur(oCust, icFakturDate.Value, arlCheckedItemColl)
                If (nResult <> -1) Then
                    CopyCustomerDealerToDealerSPK(oCust)
                    For Each model As v_RetrieveDummyFaktur In arlCheckedItemColl
                        UpdatePendingDescAndLastUpdateProfile(model.ChassisNumber)
                    Next

                    BinddtgPengajuanFaktur(0)

                    MessageBox.Show("Pengajuan faktur sukses")
                    btnSimpan.Enabled = False
                    btnUpdateProfil.Enabled = True
                    Me.chkAllPages.Visible = False
                    Me.btnCari.Visible = False
                    lblCustomerCode.Visible = False
                    btnUpdateProfil_Click(Nothing, Nothing)
                Else
                    MessageBox.Show("Pengajuan faktur gagal")
                End If
            Else
                MessageBox.Show("Tidak ada data dalam list")
                Exit Sub
            End If
        Else
            MessageBox.Show("Data konsumen tidak ditemukan")
            Exit Sub
        End If

errClose:
        Exit Sub

errCheck_error:
        MessageBox.Show("Error Number : " & CType(Err.Number, String) & "\nDescription : " + Err.Description & "\nSession Guid : " & hdnValsessGuid.Value())
        Resume errClose
        Resume Next
    End Sub

    Private Sub btnUpdateProfil_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateProfil.Click
        Dim bcheck As Boolean = False
        Dim success As Boolean = False

        dtgPengajuanFaktur.DataSource = CType(getSession("DummyFaktur"), ArrayList)
        For Each dtgItem As DataGridItem In dtgPengajuanFaktur.Items
            If CType(dtgItem.Cells(2).FindControl("chkSelect"), CheckBox).Checked Then
                bcheck = True
                Exit For
            End If
        Next

        If bcheck Then
            'chkAllPages.Checked = True
            Dim CheckedItemColl As ArrayList = New ArrayList
            'CheckedItemColl = GetCheckedItem()
            CheckedItemColl = GetCheckedItemAllPages()
            If CheckedItemColl.Count > 0 Then

                Dim cat As String = ""
                Dim arlCategory As ArrayList = New ArrayList
                Dim CatTotal As Integer = 0

                For Each ObjCM As v_RetrieveDummyFaktur In CheckedItemColl
                    If arlCategory.IndexOf(ObjCM.Category.ID) < 0 Then
                        arlCategory.Add(ObjCM.Category.ID)
                    End If
                    'strID = strID + ObjCM.ID.ToString + "-"
                Next

                CatTotal = arlCategory.Count
                Dim strID As String = String.Empty
                Dim strID2 As String = String.Empty
                For Each i As Integer In arlCategory
                    For Each ObjCM As v_RetrieveDummyFaktur In CheckedItemColl
                        If i = ObjCM.Category.ID Then
                            If ObjCM.Category.CategoryCode = "PC" Then
                                strID = strID + ObjCM.ID.ToString + "-"
                            ElseIf ObjCM.Category.CategoryCode = "LCV" Then
                                strID2 = strID2 + ObjCM.ID.ToString + "-"
                            End If
                        End If
                    Next
                Next

                Dim FirstCategory As String = String.Empty
                Dim SecondCategory As String = String.Empty
                If String.IsNullOrEmpty(strID) Then
                    FirstCategory = strID2
                Else
                    FirstCategory = strID
                    SecondCategory = strID2
                End If
                ssHelper.SetSession("PREVPAGE", "FrmPengajuanDummyFaktur.aspx?guid=" & hdnValsessGuid.Value)
                setSession("UpdateProfile", True)

                ssHelper.SetSession(hdnValsessGuid.Value & "_" & "PrevDummyFaktur", getSession("DummyFaktur"))
                ssHelper.SetSession(hdnValsessGuid.Value & "_" & "PrevCustomer", getSession("Customer"))
                ssHelper.SetSession(hdnValsessGuid.Value & "_" & "sessPrevCheckedItemAllPages", getSession("sessCheckedItemAllPages"))

                Response.Redirect("FrmMasterProfiles.aspx?Cat=" & cat & "&adsfadfadfw32342412412412424=1&kjhadshkf9784323247832493ihdufiue=17&hjkadsdhhdhkjdafkhdafklhdfahdsajklsdfjdsjjkldsaf9874553983423=" & FirstCategory &
                                  "&guid=" & hdnValsessGuid.Value & "&SecondCategory=" & SecondCategory & "&GridPageCount=" & CType(ViewState.Item("GridPageCount"), Integer))

            End If
        Else
            MessageBox.Show("Pilih chassis yang ingin di update")
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        setViewState()
        setSession("_PgIdxBefore", 0)
        BinddtgPengajuanFaktur(0)
        loadViewState()
    End Sub

    Private Sub BinddtgPengajuanFaktur(ByVal PgIdx As Integer)
        Dim objVRDFFac As v_RetrieveDummyFakturFacade = New v_RetrieveDummyFakturFacade(User)
        Dim arlVRDF As ArrayList = New ArrayList
        Dim TotRow As Integer
        Dim srtColumn As String
        Dim srtDirection As Sort.SortDirection

        If CBTglCetakDO.Checked Then
            If icCetakDoStart.Value > icCetakDoSEnd.Value Then
                MessageBox.Show("Periode tanggal cetak salah")
                Exit Sub
            End If
        End If

        If Not IsNothing(CType(getSession("DummyFaktur"), ArrayList)) Then
            setSession("_PgIdxBefore", CType(getSession("_PgIdxNext"), Integer))
            setSession("_PgIdxNext", PgIdx)

            arlCheckedItemColl = GetCheckedItem()
            Dim currentPage As String = CType(getSession("_PgIdxBefore"), String)
            setSession("sessProcess" + currentPage, arlCheckedItemColl)

            Dim arlUnCheckedItemColl As ArrayList = GetUnCheckedItem()
            currentPage = CType(getSession("_PgIdxBefore"), String)
            setSession("sessProcess2" + currentPage, arlUnCheckedItemColl)
        End If

        srtColumn = ViewState.Item("SortColumn")
        srtDirection = ViewState.Item("SortDirection")

        'ssHelper.RemoveSession(sessGuid & "_" & "DummyFaktur")

        arlVRDF = objVRDFFac.RetrieveActiveList(GetCriteriaVDS(), PgIdx + 1, dtgPengajuanFaktur.PageSize, TotRow, srtColumn, srtDirection)
        setSession("DummyFaktur", arlVRDF)
        dtgPengajuanFaktur.DataSource = arlVRDF
        dtgPengajuanFaktur.CurrentPageIndex = PgIdx
        dtgPengajuanFaktur.VirtualItemCount = TotRow
        dtgPengajuanFaktur.DataBind()

        If TotRow > 0 Then
            Me.chkAllPages.Visible = True
        Else
            Me.chkAllPages.Visible = False
        End If
    End Sub

    Private Function GetCriteriaVDS() As CriteriaComposite
        Dim crt As CriteriaComposite

        crt = New CriteriaComposite(New Criteria(GetType(v_RetrieveDummyFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If lblKodeDealer.Text.Trim <> "" Then
            crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "DealerCode", MatchType.Exact, lblKodeDealer.Text))
        End If

        If CBTglCetakDO.Checked Then
            crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "DODate", MatchType.GreaterOrEqual, Format(icCetakDoStart.Value, "MM/dd/yyyy")))
            crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "DODate", MatchType.LesserOrEqual, Format(icCetakDoSEnd.Value, "MM/dd/yyyy")))
        End If

        If ddlKategory.SelectedValue <> -1 Then
            crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "Category.ID", MatchType.Exact, ddlKategory.SelectedValue))
        End If
        If ddlModel.SelectedValue <> -1 Then
            crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "VehicleModel.SubCategoryVehicleToModel.SubCategoryVehicle.ID", MatchType.Exact, ddlModel.SelectedValue))
        End If
        If ddlTipe.SelectedValue <> -1 Then
            crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "VehicleType.ID", MatchType.Exact, ddlTipe.SelectedValue))
        End If
        If ddlWarna.SelectedValue <> -1 Then
            crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "VehicleColor.ID", MatchType.Exact, ddlWarna.SelectedValue))
        End If
        If txtFilterChassisNumber.Text <> "" Then
            crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "ChassisNumber", MatchType.Exact, txtFilterChassisNumber.Text.Trim))
        End If

        If txtFilterChassisNumber.Text.Trim <> "" Then
            crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "ChassisNumber", MatchType.[Partial], txtFilterChassisNumber.Text.Trim))
        End If
        'start default parameter
        Dim AlreadySaled As Int16 = 0 '0 = blm terjual, 2 = terjual
        crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "AlreadySaled", MatchType.Exact, AlreadySaled))
        crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "FakturStatus", MatchType.Exact, CType(EnumChassisMaster.FakturStatus.Baru, Int16)), "(", True)
        crt.opOr(New Criteria(GetType(v_RetrieveDummyFaktur), "FakturStatus", MatchType.Exact, "-1"), ")", False) 'fakturstatus -1 = blm terbuat endcustomer
        crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "ValidateTime", MatchType.LesserOrEqual, New Date(1900, 1, 1)), "(", True)
        crt.opOr(New Criteria(GetType(v_RetrieveDummyFaktur), "ValidateTime", MatchType.IsNull, Nothing), ")", False)

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim CrtCategory As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CrtCategory.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, companyCode))

        Dim arrCategory As ArrayList = New CategoryFacade(User).Retrieve(CrtCategory)
        Dim CategoryString As String = String.Empty
        If arrCategory.Count > 0 Then
            For Each CatItem As Category In arrCategory
                CategoryString += CatItem.ID.ToString() + ","
            Next
        End If
        If CategoryString.Length > 0 Then
            CategoryString = CategoryString.Substring(0, CategoryString.Length - 1)
        End If
        crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "Category.ID", MatchType.InSet, "(" & CategoryString & ")"))
        'end default parameter

        arlCheckedItemColl = CType(getSession("sessCheckedItemAllPages"), ArrayList)
        If Not IsNothing(arlCheckedItemColl) AndAlso arlCheckedItemColl.Count > 0 Then
            Dim strID As String = String.Empty
            If arrCategory.Count > 0 Then
                For Each item As v_RetrieveDummyFaktur In arlCheckedItemColl
                    If strID.Trim = "" Then
                        strID = item.ID.ToString()
                    Else
                        strID += "," + item.ID.ToString()
                    End If
                Next
            End If
            crt.opAnd(New Criteria(GetType(v_RetrieveDummyFaktur), "ID", MatchType.InSet, "(" & strID & ")"))
        End If

        Return crt
    End Function

    Protected Sub dtgPengajuanFaktur_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgPengajuanFaktur.PageIndexChanged
        BinddtgPengajuanFaktur(e.NewPageIndex)
    End Sub

    Protected Sub dtgPengajuanFaktur_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgPengajuanFaktur.SortCommand

    End Sub

    Protected Sub dtgPengajuanFaktur_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgPengajuanFaktur.ItemDataBound
        If Not (e.Item.ItemIndex = -1) Then
            Dim lblKategori As Label = e.Item.FindControl("lblKategori")
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblRemarks As Label = e.Item.FindControl("lblRemarks")
            Dim lblNamaKonsumen As Label = e.Item.FindControl("lblNamaKonsumen")
            Dim chkSelect As CheckBox = e.Item.FindControl("chkSelect")

            Dim arlDummyFaktur As ArrayList = New ArrayList
            Dim objDummyFaktur As v_RetrieveDummyFaktur = New v_RetrieveDummyFaktur

            If Not IsNothing(Request.QueryString("guid")) AndAlso CType(Request.QueryString("guid"), String) <> "" Then
                arlCheckedItemColl = CType(Session(CType(Request.QueryString("guid"), String) & "_" & "PrevDummyFaktur"), ArrayList)
                setSession("DummyFaktur", arlCheckedItemColl)

                arlCheckedItemColl = CType(Session(CType(Request.QueryString("guid"), String) & "_" & "sessPrevCheckedItemAllPages"), ArrayList)
                setSession("sessCheckedItemAllPages", arlCheckedItemColl)

                arlDummyFaktur = CType(getSession("sessCheckedItemAllPages"), ArrayList)
            Else
                arlDummyFaktur = CType(getSession("DummyFaktur"), ArrayList)
            End If

            If Not IsNothing(arlDummyFaktur) Then
                'arlDummyFaktur = CType(getSession("DummyFaktur"), ArrayList)
                objDummyFaktur = arlDummyFaktur(e.Item.ItemIndex)
                Dim objCategory As Category = New CategoryFacade(User).Retrieve(objDummyFaktur.Category.ID)
                lblKategori.Text = objCategory.CategoryCode

                If Not IsNothing(objDummyFaktur.ChassisMaster.EndCustomerID) Then
                    Dim ObjEndCustomer As EndCustomer = New EndCustomerFacade(User).Retrieve(objDummyFaktur.ChassisMaster.EndCustomerID)
                    If Not IsNothing(ObjEndCustomer) Then
                        If Not IsNothing(ObjEndCustomer.Customer) Then
                            lblRemarks.Text = ObjEndCustomer.Customer.Code
                            lblRemarks.ForeColor = Color.Red
                            lblNamaKonsumen.Text = ObjEndCustomer.Customer.Name1
                            lblNamaKonsumen.ForeColor = Color.Red
                        End If
                    End If
                End If

                If chkAllPages.Checked Then
                    chkSelect.Checked = True
                    Dim currentPage As String = CType(getSession("_PgIdxNext"), String)
                    Dim arrGridDF As ArrayList = CType(getSession("sessProcess2" + currentPage), ArrayList)
                    If IsNothing(arrGridDF) Then arrGridDF = New ArrayList
                    For Each oDF As v_RetrieveDummyFaktur In arrGridDF
                        If objDummyFaktur.ID = oDF.ID Then
                            chkSelect.Checked = False
                            Exit For
                        End If
                    Next
                Else
                    chkSelect.Checked = False
                    Dim currentPage As String = CType(getSession("_PgIdxNext"), String)
                    Dim arrGridDF As ArrayList = CType(getSession("sessProcess" + currentPage), ArrayList)
                    If IsNothing(arrGridDF) Then arrGridDF = New ArrayList
                    For Each oDF As v_RetrieveDummyFaktur In arrGridDF
                        If objDummyFaktur.ID = oDF.ID Then
                            chkSelect.Checked = True
                            Exit For
                        End If
                    Next
                End If

                Dim arlCheckedItemCollAllPages As New ArrayList
                arlCheckedItemCollAllPages = CType(getSession("sessCheckedItemAllPages"), ArrayList)
                If Not IsNothing(arlCheckedItemCollAllPages) AndAlso arlCheckedItemCollAllPages.Count > 0 Then
                    For Each oDF As v_RetrieveDummyFaktur In arlCheckedItemCollAllPages
                        If objDummyFaktur.ID = oDF.ID Then
                            chkSelect.Checked = True
                            chkSelect.Enabled = False
                            Exit For
                        End If
                    Next
                End If

            End If

            lblNo.Text = (dtgPengajuanFaktur.PageSize * dtgPengajuanFaktur.CurrentPageIndex) + e.Item.ItemIndex + 1
        End If
    End Sub

    Private Sub setSession(ByVal sessionName As String, ByVal content As Object)
        Session(hdnValsessGuid.Value & "_" & sessionName) = Content
    End Sub

    Private Function getSession(ByVal sessionName As String) As Object
        Return Session(hdnValsessGuid.Value & "_" & sessionName)
    End Function

    Private Sub chkAllPages_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllPages.CheckedChanged
        If chkAllPages.Checked Then
            SetCheckedItemAllPages()
        Else
            ClearCheckedItemAllPages()
        End If
        BinddtgPengajuanFaktur(0)
    End Sub

End Class