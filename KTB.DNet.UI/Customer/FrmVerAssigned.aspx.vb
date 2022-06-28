Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text

Public Class FrmVerAssigned
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lbl1 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipePengajuan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoPengajuan As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgPengajuan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents icTglPengajuanFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglPengajuanUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dtgRef As System.Web.UI.WebControls.DataGrid
    Protected WithEvents pnlRef As System.Web.UI.WebControls.Panel
    Protected WithEvents lblTipePengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDiajukanOleh As System.Web.UI.WebControls.Label
    Protected WithEvents lblName1Reff As System.Web.UI.WebControls.Label
    Protected WithEvents lblGedung1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamat1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKelurahan1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKecamatan1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodePos1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPropinsi1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblName1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblGedung2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamat2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKelurahan2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKecamatan2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodePos2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPropinsi2 As System.Web.UI.WebControls.Label
    Protected WithEvents pnlCompare As System.Web.UI.WebControls.Panel
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents pnlSearch As System.Web.UI.WebControls.Panel
    Protected WithEvents btnSend As System.Web.UI.WebControls.Button
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPerorangan0 As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPerusahaan0 As System.Web.UI.WebControls.Panel
    Protected WithEvents PnlBUMN0 As System.Web.UI.WebControls.Panel
    Protected WithEvents PnlLainnya0 As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlTambahan0 As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPerorangan1 As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlPerusahaan1 As System.Web.UI.WebControls.Panel
    Protected WithEvents PnlBUMN1 As System.Web.UI.WebControls.Panel
    Protected WithEvents PnlLainnya1 As System.Web.UI.WebControls.Panel
    Protected WithEvents pnlTambahan1 As System.Web.UI.WebControls.Panel
    Protected WithEvents Panel2 As System.Web.UI.WebControls.Panel
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblKodePelanggan As System.Web.UI.WebControls.Label
    Protected WithEvents btnBlock As System.Web.UI.WebControls.Button
    Protected WithEvents btnResend As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeKonsumenVer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealerVer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNamaKonsumenVer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKotaVer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAlamatVer As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCariVer As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchDealerVer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama2Reff As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblName2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblName2Reff As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCostumerType As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custome Variable Declaration"
    Private arlCustomerRequest As ArrayList
    Private arlCustomer As ArrayList
    Private sesshelper As New SessionHelper
    Private oLoginUser As New UserInfo

#End Region

#Region "Custome Method"

    Private Function Transfer(ByVal DestFile As String, ByVal Val As String, ByVal arl As ArrayList) As Boolean
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

                If (New Service.CustomerRequestFacade(User).UpdateTransaction(arl) <> -1) Then
                    sw = New StreamWriter(DestFile)
                    sw.Write(Val)
                    sw.Close()
                Else
                    success = False
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        End Try
        Return success
    End Function

    Private Function CountChecked() As Integer
        Dim itemChecked As Integer = 0
        For Each oDataGridItem As DataGridItem In dtgPengajuan.Items
            Dim chkExport As CheckBox = oDataGridItem.FindControl("chkItemChecked")
            If chkExport.Checked Then
                itemChecked += 1
            End If
        Next
        Return itemChecked
    End Function

    Private Function Download(ByVal DestFile As String, ByVal Val As String, ByVal arl As ArrayList) As Boolean

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

                If (New Service.CustomerRequestFacade(User).UpdateTransaction(arl) <> -1) Then
                    sw = New StreamWriter(DestFile)
                    sw.WriteLine(Val)
                    sw.Close()
                Else
                    success = False
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        End Try
        Return success
    End Function

    Sub BindTipePengajuan()
        ddlTipePengajuan.DataSource = New EnumTipePengajuanCustomerRequest().RetrieveType()
        ddlTipePengajuan.DataTextField = "NameTipe"
        ddlTipePengajuan.DataValueField = "ValTipe"
        ddlTipePengajuan.DataBind()
        ddlStatus.DataSource = New EnumStatusCustomerRequest().RetrieveStatusVerifikasi
        ddlStatus.DataTextField = "NameTipe"
        ddlStatus.DataValueField = "ValTipe"
        ddlStatus.DataBind()

        ddlCostumerType.DataSource = New EnumTipePelangganCustomerRequest().RetrieveType(True)
        ddlCostumerType.DataTextField = "NameTipe"
        ddlCostumerType.DataValueField = "ValTipe"
        ddlCostumerType.DataBind()
    End Sub

    Sub BindToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerRequest), "RequestType", MatchType.Exact, ddlTipePengajuan.SelectedValue))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerRequest), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        If ddlCostumerType.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerRequest), "Status1", MatchType.Exact, ddlCostumerType.SelectedValue))
        End If

        If icTglPengajuanFrom.Value <= icTglPengajuanUntil.Value Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerRequest), "RequestDate", MatchType.GreaterOrEqual, icTglPengajuanFrom.Value))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerRequest), "RequestDate", MatchType.LesserOrEqual, icTglPengajuanUntil.Value.AddDays(1)))
        Else
            MessageBox.Show("Tanggal pengajuan sampai harus lebih besar atau sama dengan tanggal pengajuan dari")
            Exit Sub
        End If

        If (txtNoPengajuan.Text.Trim() <> String.Empty) Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerRequest), "RequestNo", MatchType.Exact, txtNoPengajuan.Text.Trim()))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerRequest), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim.Replace(";", "','") & "')"))

        'If (txtDiajukanOleh.Text.Trim() <> String.Empty) Then
        '    Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "UserName", MatchType.InSet, txtDiajukanOleh.Text.Trim()))
        '    Dim arrayList As arrayList = New UserManagement.UserInfoFacade(User).Retrieve(criterias2)
        '    If (arrayList.Count > 0) Then
        '        For Each item As UserInfo In arrayList
        '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerRequest), "RequestUserID", MatchType.Exact, item.ID))
        '        Next
        '    End If
        'End If

        arlCustomerRequest = New Service.CustomerRequestFacade(User).RetrieveByCriteria(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If (arlCustomerRequest.Count > 0) Then
            dtgPengajuan.Visible = True
            dtgPengajuan.VirtualItemCount = total
            dtgPengajuan.DataSource = arlCustomerRequest
            dtgPengajuan.DataBind()
        Else
            dtgPengajuan.Visible = False
            MessageBox.Show(SR.DataNotFound("Daftar Customer Request"))
        End If

        If (ddlTipePengajuan.SelectedValue = EnumTipePengajuanCustomerRequest.TipePengajuanCustomerRequest.Baru) And arlCustomerRequest.Count > 0 Then
            btnSend.Visible = bCekBtnTransferPriv
            btnResend.Visible = bCekBtnReTransferPriv
        Else
            btnSend.Visible = False
            btnResend.Visible = False
        End If
    End Sub

    Sub BindToGridRef(ByVal currentPageIndex As Integer, ByVal isSearchDetailClick As Boolean)
        Dim total As Integer = 0
        Dim filter As String = KTB.DNet.Lib.WebConfig.GetValue("FilterVerificationCustomer")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim SearchName As String = CType(ViewState("Name"), String)
        If SearchName.Trim <> String.Empty Then
            SearchName = SearchName.Replace(",", " ")
            SearchName = SearchName.Replace(".", " ")
            Dim temp As Array = Array.CreateInstance(GetType(String), SearchName.Split(" ").Length)
            Dim nama As New ArrayList
            SearchName.Split(" ").CopyTo(temp, 0)
            Dim found As Boolean = False
            nama.Clear()
            For Each item As String In temp
                found = False
                If item.Trim <> String.Empty Then
                    For Each itemFilter As String In filter.Split(";")
                        If itemFilter.ToUpper = item.ToUpper Then
                            found = True
                        End If
                    Next
                    If Not found Then
                        nama.Add(item)
                    End If
                End If
            Next

            'If nama.Count > 1 Then
            '  For i As Integer = 0 To nama.Count - 1
            '    If i = 0 Then
            '      criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "Name1", MatchType.Partial, nama(i)), "(", True)
            '    ElseIf i = nama.Count - 1 Then
            '      criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.Customer), "Name1", MatchType.Partial, nama(i)), ")", False)
            '    Else
            '      criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.Customer), "Name1", MatchType.Partial, nama(i)))
            '    End If
            '  Next
            'Else
            '  criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "Name1", MatchType.Partial, nama(0)))
            'End If

            ' New Search Critreria Rule
            Dim _CompleteName As String = String.Empty
            For Each sItem As String In nama
                _CompleteName += sItem
            Next

            _CompleteName = _CompleteName.Replace(" ", "")
            _CompleteName = _CompleteName.Replace("'", "")
            _CompleteName = _CompleteName.Replace(Chr(34), "") ' Chr(34) = "

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "CompleteName", MatchType.StartsWith, _CompleteName))

        End If


        Dim SearchCityID As Integer = CType(ViewState("IDKota"), Integer)

        If SearchCityID <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "City.ID", MatchType.Exact, SearchCityID))
        End If


        If txtKodeKonsumenVer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "Code", MatchType.[Partial], txtKodeKonsumenVer.Text.Trim))
        End If
        If txtNamaKonsumenVer.Text <> String.Empty Then
            Dim VerName As String = txtNamaKonsumenVer.Text
            If VerName.Trim <> String.Empty Then
                VerName = VerName.Replace(",", " ")
                VerName = VerName.Replace(".", " ")
                Dim tempVername As Array = Array.CreateInstance(GetType(String), VerName.Split(" ").Length)
                Dim namaComplete As New ArrayList
                VerName.Split(" ").CopyTo(tempVername, 0)
                Dim founds As Boolean = False
                namaComplete.Clear()
                For Each items As String In tempVername
                    founds = False
                    If items.Trim <> String.Empty Then
                        For Each itemFilters As String In filter.Split(";")
                            If itemFilters.ToUpper = items.ToUpper Then
                                founds = True
                            End If
                        Next
                        If Not founds Then
                            namaComplete.Add(items)
                        End If
                    End If
                Next
                Dim _CompleteNames As String = String.Empty
                For Each sItems As String In namaComplete
                    _CompleteNames += sItems
                Next

                _CompleteNames = _CompleteNames.Replace(" ", "")
                _CompleteNames = _CompleteNames.Replace("'", "")
                _CompleteNames = _CompleteNames.Replace(Chr(34), "") ' Chr(34) = 
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "CompleteName", MatchType.[Partial], _CompleteNames))

            End If

        End If

        If txtAlamatVer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "Alamat", MatchType.[Partial], txtAlamatVer.Text.Trim))
        End If
        If txtKotaVer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "City.CityName", MatchType.[Partial], txtKotaVer.Text.Trim))
        End If


        If txtKodeDealerVer.Text <> String.Empty Then
            Dim CustDealerCrit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CustomerDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            CustDealerCrit.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerDealer), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealerVer.Text.Trim.Replace(";", "','") & "')"))

            Dim arr As ArrayList = New CustomerDealerFacade(User).Retrieve(CustDealerCrit)
            Dim DealerCustID As String = "0,"
            If arr.Count > 0 Then
                For Each item As CustomerDealer In arr
                    DealerCustID += item.Customer.ID.ToString + ","
                Next
            End If
            DealerCustID = DealerCustID.Substring(0, DealerCustID.Length - 1)
            If DealerCustID.Trim <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "ID", MatchType.InSet, "(" + DealerCustID + ")"))
            End If
        End If

        arlCustomer = New Service.CustomerFacade(User).RetrieveByCriteria(criterias, currentPageIndex + 1, dtgRef.PageSize, _
            total, CType(ViewState("CurrentSortColumnRef"), String), _
            CType(ViewState("CurrentSortDirectRef"), Sort.SortDirection))


        'UpdateStatus()

        dtgRef.VirtualItemCount = total
        dtgRef.DataSource = arlCustomer
        dtgRef.DataBind()

        If (arlCustomer.Count > 0) Then
            pnlRef.Visible = True
            dtgRef.Visible = True
        Else
            If isSearchDetailClick Then
                pnlRef.Visible = True
                dtgRef.Visible = True
            Else
                dtgRef.Visible = False
                pnlRef.Visible = False
            End If
            MessageBox.Show(SR.DataNotFound("Daftar Customer"))
        End If
    End Sub

    Private Sub ClearSearchDetailCustomerVerificaation()
        txtKodeKonsumenVer.Text = ""
        txtNamaKonsumenVer.Text = ""
        txtAlamatVer.Text = ""
        txtKodeDealerVer.Text = ""
        txtKotaVer.Text = ""
    End Sub

    Private Sub UpdateStatus()
        Dim result As Integer
        Dim _CR As CustomerRequest = CType(sesshelper.GetSession("CR"), CustomerRequest)
        If _CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi Then
            _CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Proses
            _CR.ProcessDate = DateTime.Today
            _CR.ProcessUserID = oLoginUser.ID

            result = New CustomerRequestFacade(User).Update(_CR)
        End If

    End Sub

    Sub FillCompareData()
        pnlSearch.Visible = False
        pnlCompare.Visible = True
        Dim oC As Customer = CType(sesshelper.GetSession("C"), Customer)
        Dim oCR As CustomerRequest = CType(sesshelper.GetSession("CR"), CustomerRequest)

        Dim RT As New EnumTipePengajuanCustomerRequest
        lblTipePengajuan.Text = RT.RetrieveName(oCR.RequestType)
        lblTglPengajuan.Text = oCR.RequestDate.ToString("dd/MM/yyyy")
        lblNoPengajuan.Text = oCR.RequestNo
        lblKodeDealer.Text = oCR.Dealer.DealerCode & " - " & oCR.Dealer.DealerName
        Dim oUserInfo As New UserInfo
        oUserInfo = New UserManagement.UserInfoFacade(User).Retrieve(Convert.ToInt32(oCR.RequestUserID))
        lblDiajukanOleh.Text = oUserInfo.UserName

        lblName1Reff.Text = oCR.Name1
        lblName2Reff.Text = oCR.Name2
        lblGedung1.Text = oCR.Name3
        lblAlamat1.Text = oCR.Alamat
        lblKelurahan1.Text = oCR.Kelurahan
        lblKecamatan1.Text = oCR.Kecamatan
        Dim ObjCity As New City
        ObjCity = New General.CityFacade(User).Retrieve(oCR.CityID)

        If oCR.PreArea = "blank" Then
            lblKota1.Text = ObjCity.CityName
        Else
            lblKota1.Text = oCR.PreArea & " " & ObjCity.CityName
        End If
        lblKodePos1.Text = oCR.PostalCode
        lblPropinsi1.Text = ObjCity.Province.ProvinceName
        lblRefCode.Text = oC.Code
        lblName1.Text = oC.Name1
        lblName2.Text = oC.Name2

        lblGedung2.Text = oC.Name3
        lblAlamat2.Text = oC.Alamat
        lblKelurahan2.Text = oC.Kelurahan
        lblKecamatan2.Text = oC.Kecamatan
        lblKota2.Text = oC.PreArea & " " & oC.City.CityName
        lblKodePos2.Text = oC.PostalCode
        lblPropinsi2.Text = oC.City.Province.ProvinceName
        lblKodePelanggan.Text = oC.Code
        pnlPerorangan0.Visible = False
        pnlPerusahaan0.Visible = False
        pnlTambahan0.Visible = True
        PnlBUMN0.Visible = False
        PnlLainnya0.Visible = False
        pnlPerorangan1.Visible = False
        pnlPerusahaan1.Visible = False
        pnlTambahan1.Visible = True
        PnlBUMN1.Visible = False
        PnlLainnya1.Visible = False

        If oCR.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
            PnlBUMN0.Visible = True
        End If
        If oCR.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya Then
            PnlLainnya0.Visible = True
        End If
        If oCR.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan Then
            pnlPerorangan0.Visible = True
        End If
        If oCR.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan Then
            pnlPerusahaan0.Visible = True
        End If
        RenderProfilePanelCR(oCR, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlPerorangan0)
        RenderProfilePanelCR(oCR, New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlPerusahaan0)
        RenderProfilePanelCR(oCR, New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlTambahan0)
        RenderProfilePanelCR(oCR, New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), EnumProfileType.ProfileType.CUSTOMERREQUEST, PnlBUMN0)
        RenderProfilePanelCR(oCR, New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), EnumProfileType.ProfileType.CUSTOMERREQUEST, PnlLainnya0)
        'RenderProfilePanelCR(oCR, New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnl1)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CustomerRequest), "CustomerCode", MatchType.[Partial], oC.Code))
        Dim list As ArrayList = New CustomerRequestFacade(User).Retrieve(criterias)
        Dim obj_CustomerRequest As CustomerRequest
        If list.Count > 0 Then
            obj_CustomerRequest = list(0)
            If obj_CustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah Then
                PnlBUMN1.Visible = True
            End If
            If obj_CustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya Then
                PnlLainnya1.Visible = True
            End If
            If obj_CustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan Then
                pnlPerorangan1.Visible = True
            End If
            If obj_CustomerRequest.Status1 = EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan Then
                pnlPerusahaan1.Visible = True
            End If
            RenderProfilePanelCR(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlPerorangan1)
            RenderProfilePanelCR(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlPerusahaan1)
            RenderProfilePanelCR(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlTambahan1)
            RenderProfilePanelCR(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), EnumProfileType.ProfileType.CUSTOMERREQUEST, PnlBUMN1)
            RenderProfilePanelCR(obj_CustomerRequest, New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), EnumProfileType.ProfileType.CUSTOMERREQUEST, PnlLainnya1)
        Else
            RenderProfilePanelCR(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_2"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlPerorangan1)
            RenderProfilePanelCR(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_3"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlPerusahaan1)
            RenderProfilePanelCR(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_1"), EnumProfileType.ProfileType.CUSTOMERREQUEST, pnlTambahan1)
            RenderProfilePanelCR(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_4"), EnumProfileType.ProfileType.CUSTOMERREQUEST, PnlBUMN1)
            RenderProfilePanelCR(Nothing, New ProfileGroupFacade(User).Retrieve("cust_dbs_5"), EnumProfileType.ProfileType.CUSTOMERREQUEST, PnlLainnya1)

        End If

        'RenderProfilePanelC(oC, New ProfileGroupFacade(User).Retrieve("CD-Customer-Tambahan"), EnumProfileType.ProfileType.CUSTOMER, pnl2)

        If (oCR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Selesai) Then
            btnSave.Visible = False
        Else
            If CheckCmdBtnSavePriv() Then
                btnSave.Visible = True
            Else
                btnSave.Visible = False
            End If
        End If
    End Sub

    Private Sub RenderProfilePanelCR(ByVal objCustomerRequest As CustomerRequest, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(True)

        If Not objCustomerRequest Is Nothing Then
            objRenderPanel.GeneratePanel(objCustomerRequest.ID, objPanel, objGroup, profileType, User)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User)
        End If
    End Sub

    Private Sub RenderProfilePanelC(ByVal objCustomer As Customer, ByVal objGroup As ProfileGroup, ByVal profileType As Short, ByVal objPanel As Panel)
        Dim objRenderPanel As RenderingProfile = New RenderingProfile(True)
        If Not objCustomer Is Nothing Then
            objRenderPanel.GeneratePanel(objCustomer.ID, objPanel, objGroup, profileType, User)
        Else
            objRenderPanel.GeneratePanel(0, objPanel, objGroup, profileType, User)
        End If
    End Sub

    Sub ClearCompareData()
        For Each control As Control In pnlCompare.Controls
            If (control.GetType().ToString().Equals("System.Web.UI.WebControls.Label")) Then
                CType(control, Label).Text = String.Empty
            End If
        Next
    End Sub

    Private Function NewRequest(ByVal c As Customer, ByVal cr As CustomerRequest) As Boolean
        Dim objUser As UserInfo = CType(sesshelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim retVal As Boolean = False
        cr.CustomerCode = c.Code
        cr.ProcessUserID = objUser.ID
        cr.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Selesai

        Dim CD As New CustomerDealer
        CD.Customer = c
        CD.Dealer = cr.Dealer

        If (New Service.CustomerRequestFacade(User).AssignedNewRequest(cr, CD) <> -1) Then
            retVal = True
            Return retVal
        End If
        Return retVal
    End Function

    Private Function RevisiRequest(ByVal c As Customer, ByVal cr As CustomerRequest) As Boolean
        'Dim retVal As Boolean = False
        'Dim objUser As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)


        'cr.ProcessUserID = objUser.ID
        'cr.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Selesai

        'Dim arlAdd As New ArrayList
        'Dim arlUpdate As New ArrayList

        'For Each item As CustomerRequestProfile In cr.CustomerRequestProfiles
        '    If (item.ProfileGroup.ID = 15) Then
        '        Dim arlCP As New ArrayList
        '        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerProfile), "ProfileHeader.ID", MatchType.Exact, item.ProfileHeader.ID))
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerProfile), "Customer.ID", MatchType.Exact, c.ID))
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CustomerProfile), "ProfileGroup.ID", MatchType.Exact, 16))

        '        arlCP = New Profile.CustomerProfileFacade(User).RetrieveByCriteria(criterias)
        '        If (arlCP.Count > 0) Then
        '            Dim CP As CustomerProfile = CType(arlCP(0), CustomerProfile)
        '            CP.ProfileValue = item.ProfileValue
        '            arlUpdate.Add(CP)
        '        Else
        '            Dim CP As New CustomerProfile
        '            CP.Customer = c
        '            CP.ProfileHeader = item.ProfileHeader
        '            CP.ProfileValue = item.ProfileValue
        '            CP.ProfileGroup = New ProfileGroupFacade(User).Retrieve("CD-Customer-Tambahan")
        '            arlAdd.Add(CP)
        '        End If
        '    End If
        'Next

        'If (New Service.CustomerRequestFacade(User).UpdateRevisiRequest(cr, arlAdd, arlUpdate) <> -1) Then
        '    retVal = True
        '    Return retVal
        'End If
        'Return retVal
    End Function

    Private Sub GetKTPAndPhone(ByVal oCR As CustomerRequest, ByRef NOKTP As String, ByRef NOTELP As String)
        NOKTP = ""
        NOTELP = ""
        For Each oCRP As CustomerRequestProfile In oCR.CustomerRequestProfiles
            If oCRP.ProfileHeader.Code.Trim.ToUpper = "NOKTP" Then
                NOKTP = oCRP.ProfileValue
            ElseIf oCRP.ProfileHeader.Code.Trim.ToUpper = "NOTELP" Then
                NOTELP = oCRP.ProfileValue
            End If
        Next
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        oLoginUser = CType(sesshelper.GetSession("LOGINUSERINFO"), UserInfo)
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblSearchDealerVer.Attributes("onclick") = "ShowPPDealerVerSelection();"

        If Not IsPostBack Then
            BindTipePengajuan()
            ViewState("CurrentSortColumn") = "CustomerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            btnSend.Attributes.Add("onclick", "return ValidateRecord();")
            btnResend.Attributes.Add("onclick", "return ValidateRecord();")

        End If

        btnBlock.Enabled = bCekBtnBlokPriv
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        pnlRef.Visible = False
        dtgPengajuan.CurrentPageIndex = 0
        BindToGrid(dtgPengajuan.CurrentPageIndex)
    End Sub

    Private Sub dtgPengajuan_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPengajuan.SortCommand
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
        BindToGrid(dtgPengajuan.CurrentPageIndex)
    End Sub

    Private Sub dtgPengajuan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPengajuan.ItemDataBound
        If (e.Item.ItemType = ListItemType.Header) Then
            Dim chkAllItems As CheckBox = CType(e.Item.FindControl("chkAllItems"), CheckBox)
            If (ddlTipePengajuan.SelectedValue = EnumTipePengajuanCustomerRequest.TipePengajuanCustomerRequest.Revisi) Then
                chkAllItems.Enabled = False
            Else
                chkAllItems.Attributes.Add("onclick", "CheckAll('chkItemChecked',document.forms[0]." & chkAllItems.ClientID & ".checked)")
            End If
        End If
        If (e.Item.ItemIndex >= 0) Then
            Dim item As CustomerRequest = CType(e.Item.DataItem, CustomerRequest)
            Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
            Dim lblProvince As Label = CType(e.Item.FindControl("lblProvince"), Label)
            Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)
            Dim lnkAttach As LinkButton = CType(e.Item.FindControl("lnkAttach"), LinkButton)
            Dim lblKTP As Label = e.Item.FindControl("lblKTP")

            Dim lblCustumerType As Label = e.Item.FindControl("lblCustumerType")
            If Not IsNothing(item.Status1) Then
                lblCustumerType.Text = New EnumTipePelangganCustomerRequest().RetrieveTipePelangganCustomerRequest(CInt(item.Status1))
            End If

            If item.Attachment = "" Then
                lnkAttach.Visible = False
            Else
                lnkAttach.ToolTip = item.Attachment
                lnkAttach.Visible = True
            End If

            Dim ObjCity As New City
            ObjCity = New General.CityFacade(User).Retrieve(item.CityID)
            lblCity.Text = ObjCity.CityName
            lblProvince.Text = ObjCity.Province.ProvinceName
            lblName.Text = item.Name1 & " " & item.Name2
            If (ddlTipePengajuan.SelectedValue = EnumTipePengajuanCustomerRequest.TipePengajuanCustomerRequest.Revisi) Then
                Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
                chkItemChecked.Enabled = False
            End If
            Dim sKTP As String, sPhone As String
            GetKTPAndPhone(item, sKTP, sPhone)
            lblKTP.Text = sKTP
            'Red Background if record have reference
            Dim filter As String = KTB.DNet.Lib.WebConfig.GetValue("FilterVerificationCustomer")

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Customer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "City.ID", MatchType.Exact, item.CityID))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "Name1", MatchType.Partial, item.Name1))
            Dim SearchName As String = item.Name1 + " " + item.Name2 'CType(ViewState("Name"), String)
            If SearchName.Trim <> String.Empty Then

                SearchName = SearchName.Replace(",", " ")
                SearchName = SearchName.Replace(".", " ")
                Dim temp As Array = Array.CreateInstance(GetType(String), SearchName.Split(" ").Length)
                Dim nama As New ArrayList
                SearchName.Split(" ").CopyTo(temp, 0)
                Dim found As Boolean = False
                nama.Clear()
                For Each itemx As String In temp
                    found = False
                    If itemx.Trim <> String.Empty Then
                        For Each itemFilter As String In filter.Split(";")
                            If itemFilter.ToUpper = itemx.ToUpper Then
                                found = True
                            End If
                        Next
                        If Not found Then
                            nama.Add(itemx)
                        End If
                    End If
                Next

                ' Old Search Critreria Rule
                'If nama.Count > 1 Then
                '  For i As Integer = 0 To nama.Count - 1
                '    If i = 0 Then
                '      criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "Name1", MatchType.Partial, nama(i)), "(", True)
                '    ElseIf i = nama.Count - 1 Then
                '      criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.Customer), "Name1", MatchType.Partial, nama(i)), ")", False)
                '    Else
                '      criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.Customer), "Name1", MatchType.Partial, nama(i)))
                '    End If
                '  Next
                'Else
                '  criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "Name1", MatchType.Partial, nama(0)))
                'End If

                ' New Search Critreria Rule
                Dim _CompleteName As String = String.Empty
                For Each sItem As String In nama
                    _CompleteName += sItem
                Next

                _CompleteName = _CompleteName.Replace(" ", "")
                _CompleteName = _CompleteName.Replace("'", "")
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Customer), "CompleteName", MatchType.StartsWith, _CompleteName))

            End If

            Dim arlCustomer As ArrayList = New Service.CustomerFacade(User).Retrieve(criterias)
            Dim chkhavereference As CheckBox = e.Item.FindControl("chkhavereference")
            If arlCustomer.Count > 0 Then
                e.Item.BackColor = System.Drawing.Color.PaleGreen
                chkhavereference.Checked = True
            Else
                chkhavereference.Checked = False
            End If


        End If
    End Sub

    Private Sub dtgRef_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgRef.SortCommand
        If CType(ViewState("CurrentSortColumnRef"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirectRef"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirectRef") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirectRef") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumnRef") = e.SortExpression
            ViewState("CurrentSortDirectRef") = Sort.SortDirection.ASC
        End If
        BindToGridRef(dtgRef.CurrentPageIndex, False)
    End Sub

    Private Sub dtgPengajuan_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPengajuan.ItemCommand
        If (e.CommandName = "detail") Then
            Dim objCR As CustomerRequest = New CustomerRequest
            objCR = New Service.CustomerRequestFacade(User).Retrieve(Convert.ToInt32(e.CommandArgument))
            'Todo session
            'Session("CR") = objCR
            sesshelper.SetSession("CR", objCR)
            ViewState("Name") = objCR.Name1 + " " + objCR.Name2
            ViewState("IDKota") = objCR.CityID
            ViewState("CurrentSortColumnRef") = "Code"
            ViewState("CurrentSortDirectRef") = Sort.SortDirection.ASC
            ClearSearchDetailCustomerVerificaation()
            dtgRef.CurrentPageIndex = 0
            BindToGridRef(dtgRef.CurrentPageIndex, False)
        ElseIf e.CommandName = "download" Then
            Dim objCR As CustomerRequest = New CustomerRequest
            objCR = New Service.CustomerRequestFacade(User).Retrieve(Convert.ToInt32(e.CommandArgument))

            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("CustomerRequestDir") & "\" & objCR.Attachment
            Response.Redirect("../Download.aspx?file=" & PathFile)
        End If
    End Sub

    Private Sub dtgRef_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgRef.ItemCommand
        If (e.CommandName = "detail") Then
            Dim objC As Customer = New Customer
            objC = New Service.CustomerFacade(User).Retrieve(Convert.ToInt32(e.CommandArgument))
            'Todo session
            'Session("C") = objC
            sesshelper.SetSession("C", objC)
            FillCompareData()
        ElseIf e.CommandName = "download" Then
            Dim objC As Customer = New Customer
            objC = New Service.CustomerFacade(User).Retrieve(Convert.ToInt32(e.CommandArgument))
            'Todo session
            'Session("C") = objC
            sesshelper.SetSession("C", objC)
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("CustomerRequestDir") & "\" & objC.Attachment
            Response.Redirect("../Download.aspx?file=" & PathFile)
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim oCR As CustomerRequest = CType(sesshelper.GetSession("CR"), CustomerRequest)
        Dim oC As Customer = CType(sesshelper.GetSession("C"), Customer)
        If (oCR.RequestType = EnumTipePengajuanCustomerRequest.TipePengajuanCustomerRequest.Baru) Then
            If (NewRequest(oC, oCR)) Then
                MessageBox.Show(SR.UpdateSucces)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        ElseIf (oCR.RequestType = EnumTipePengajuanCustomerRequest.TipePengajuanCustomerRequest.Revisi) Then
            If (RevisiRequest(oC, oCR)) Then
                MessageBox.Show(SR.UpdateSucces)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        End If
        FillCompareData()
    End Sub

    Private Sub Cancel()
        ClearCompareData()
        pnlSearch.Visible = True
        pnlCompare.Visible = False
        If (ddlTipePengajuan.SelectedValue = EnumTipePengajuanCustomerRequest.TipePengajuanCustomerRequest.Baru) Then
            btnSend.Visible = bCekBtnTransferPriv
            btnResend.Visible = bCekBtnReTransferPriv
        Else
            btnSend.Visible = False
            btnResend.Visible = False
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Cancel()
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim i As Integer = 0
        Dim arl As ArrayList = New ArrayList
        Dim objUser As UserInfo = CType(sesshelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim CR As CustomerRequest
        Dim success As Boolean = False

        Dim IsCheck As Boolean = False
        Dim sw As StreamWriter
        Dim sb As New StringBuilder
        Dim filename = String.Format("{0}{1}{2}", "cusreq", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\" & filename  '-- Destination file
        Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename
        Dim tmp As Integer = 0
        Dim NoKTP As String, NoTelp As String

        For Each item As DataGridItem In dtgPengajuan.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If (chk.Checked) Then
                CR = New CustomerRequest
                Dim CRFacade As Service.CustomerRequestFacade = New Service.CustomerRequestFacade(User)
                CR = CRFacade.Retrieve(Convert.ToInt32(dtgPengajuan.DataKeys().Item(i)))
                If CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi Then
                    IsCheck = True
                    CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Proses
                    CR.ProcessUserID = objUser.ID

                    Dim ObjCity As New City
                    ObjCity = New General.CityFacade(User).Retrieve(CR.CityID)
                    Dim preRegion As String
                    If CR.PrintRegion = "0" Then
                        preRegion = "X"
                    Else
                        preRegion = ""
                    End If

                    'handle sementara untuk prearea
                    If CR.PreArea.ToLower = "blank" Then
                        CR.PreArea = ""
                    End If

                    'Untuk preArea dan kota dipisahkan dengan spasi dan bukan dengan Delimiter chr(13) (Enter)
                    'Konfirmasi dari Heru
                    'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(13) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & Chr(10))
                    'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion )
                    GetKTPAndPhone(CR, NoKTP, NoTelp) 'CR:for:Rina;by:dna:on:20110323

                    'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP & Chr(9) & NoTelp)
                    If Not (NoKTP.Trim = "") Then
                        If arl.Count > 0 Then sb.Append(vbNewLine)
                        arl.Add(CR)
                        sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP)
                    End If
                    'If tmp < CountChecked() - 1 Then
                    '    sb.Append(vbNewLine)
                    'End If
                    tmp += 1
                End If
            End If
            i = i + 1
        Next

        If IsCheck Then
            If (sb.Length > 0) Then

                If Transfer(DestFile, sb.ToString(), arl) Then         '>> Code utk upload data lsg ke SAP Folder
                    'If Download(LocalDest, sb.ToString(), arl) Then         '>> Code utk download ke folder lokal
                    MessageBox.Show("Data berhasil di upload ke SAP")
                    'Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & filename)
                Else
                    MessageBox.Show("Download data gagal")
                End If
            End If
        Else
            MessageBox.Show("Daftar customer request belum dipilih atau status tidak valid")
        End If
    End Sub
    Private Sub btnResend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResend.Click
        Dim i As Integer = 0
        Dim arl As ArrayList = New ArrayList
        Dim objUser As UserInfo = CType(sesshelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim CR As CustomerRequest
        Dim success As Boolean = False

        Dim IsCheck As Boolean = False
        Dim sw As StreamWriter
        Dim sb As New StringBuilder
        Dim filename = String.Format("{0}{1}{2}", "cusreq", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\FinishUnit\" & filename  '-- Destination file
        Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename
        Dim _tmp As Integer = 0
        Dim NoKTP As String, NoTelp As String

        For Each item As DataGridItem In dtgPengajuan.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If (chk.Checked) Then
                CR = New CustomerRequest
                Dim CRFacade As Service.CustomerRequestFacade = New Service.CustomerRequestFacade(User)
                CR = CRFacade.Retrieve(Convert.ToInt32(dtgPengajuan.DataKeys().Item(i)))
                If CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Proses Then
                    IsCheck = True

                    Dim ObjCity As New City
                    ObjCity = New General.CityFacade(User).Retrieve(CR.CityID)
                    Dim preRegion As String
                    If CR.PrintRegion = "0" Then
                        preRegion = "X"
                    Else
                        preRegion = ""
                    End If

                    'handle sementara untuk prearea
                    If CR.PreArea.ToLower = "blank" Then
                        CR.PreArea = ""
                    End If

                    'Untuk preArea dan kota dipisahkan dengan spasi dan bukan dengan Delimiter chr(13) (Enter)
                    'Konfirmasi dari Heru
                    'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(13) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & Chr(10))
                    'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion)
                    GetKTPAndPhone(CR, NoKTP, NoTelp) 'CR:for:Rina;by:dna:on:20110323

                    'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP & Chr(9) & NoTelp)
                    If Not (NoKTP.Trim = "") Then
                        If arl.Count > 0 Then sb.Append(vbNewLine)
                        arl.Add(CR)
                        sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & "KTP" & Chr(9) & NoKTP)
                    End If
                    'If _tmp < CountChecked() - 1 Then
                    '    sb.Append(vbNewLine)
                    'End If
                    _tmp += 1
                End If
            End If
            i = i + 1
        Next

        If IsCheck Then
            If (sb.Length > 0) Then

                If Transfer(DestFile, sb.ToString(), arl) Then         '>> Code utk upload data lsg ke SAP Folder
                    'If Download(LocalDest, sb.ToString(), arl) Then         '>> Code utk download ke folder lokal
                    MessageBox.Show("Data berhasil di upload ke SAP")
                    'Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & filename)
                Else
                    MessageBox.Show("Download data gagal")
                End If
            End If
        Else
            MessageBox.Show("Daftar customer request belum dipilih atau status tidak valid")
        End If

    End Sub

    Private Sub dtgRef_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgRef.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim item As Customer = CType(e.Item.DataItem, Customer)
            'Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)
            ' lblName.Text = item.Name1 & " " & item.Name2
            Dim lnkAttach As LinkButton = CType(e.Item.FindControl("lnkAttach"), LinkButton)
            If item.Attachment = "" Then
                lnkAttach.Visible = False
            Else
                lnkAttach.ToolTip = item.Attachment
                lnkAttach.Visible = True
            End If
        End If
    End Sub

    Private Sub dtgRef_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgRef.PageIndexChanged
        dtgRef.CurrentPageIndex = e.NewPageIndex
        BindToGridRef(dtgRef.CurrentPageIndex, False)
    End Sub

    Private Sub btnBlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBlock.Click
        Dim oCR As CustomerRequest = CType(sesshelper.GetSession("CR"), CustomerRequest)
        oCR.Status = New EnumStatusCustomerRequest().TipePengajuanCustomerRequest.Block
        Dim i As Integer = New CustomerRequestFacade(User).Update(oCR)
        pnlRef.Visible = False
        dtgPengajuan.CurrentPageIndex = 0
        BindToGrid(dtgPengajuan.CurrentPageIndex)
        Cancel()
    End Sub

    Private Sub btnCariVer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCariVer.Click
        dtgRef.CurrentPageIndex = 0
        BindToGridRef(dtgRef.CurrentPageIndex, True)
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.CustomerVerificationView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Konsumen - Daftar Verifikasi")
        End If
    End Sub

    Dim bCekBtnTransferPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.CustomerVerificationSAPTransfer_Privilege)
    Dim bCekBtnReTransferPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.CustomerVerificationSAPReTransfer_Privilege)

    Private Function CheckCmdBtnSavePriv() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.CustomerVerificationSave_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Dim bCekBtnBlokPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.CustomerVerificationBlock_Privilege)
#End Region

End Class
