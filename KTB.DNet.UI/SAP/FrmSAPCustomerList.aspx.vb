Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.SAP

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.IO
Imports System.Text



Public Class FrmSAPCustomerList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents dgSAPCustomer As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtSalesmanID As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents icPaymentDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPaymentDateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hdnID As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnName As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SAPCustomerFacade As New SAPCustomerFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper

#End Region

#Region "PrivateCustomMethods"

    

   
#End Region

#Region "EventHandlers"

   
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'txtSalesmanID.Text = 'hdnID.Value

        'remark by ery
        'If icPaymentDateFrom.Value = New DateTime Or icPaymentDateTo.Value.ToString = String.Empty Then
        '    MessageBox.Show("Tanggal harus diisi")
        '    Exit Sub
        'End If

        If txtSalesmanID.Text <> "" Then
            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
            If objSalesmanHeader.ID = 0 Then
                MessageBox.Show("No Salesman yang anda masukkan tidak ada")
                Exit Sub
            End If
            lblNamaSalesman.Text = objSalesmanHeader.Name
        End If
        CreateCriteria()
        'lblNamaSalesman.Text = hdnName.Value
        dgSAPCustomer.CurrentPageIndex = 0
        BindDataGrid(0)
        'If txtPeriod.Value <> "" Then
        '    ' mengembalikan value hidden field ke label ybs
        '    Dim strTmp As String() = txtPeriod.Value.Split(";")
        '    lblDateFrom.Text = strTmp(0)
        '    lblDateUntil.Text = strTmp(1)
        'End If
    End Sub

    Private Sub BindDropDown()
        '   CommonFunction.BindFromEnum("SAPCustomerStatus", ddlStatus, User, True, "NameStatus", "ValStatus")
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = New EnumDNET().RetrieveSAPCustomerList()
        ddlStatus.DataTextField = "NameType"
        ddlStatus.DataValueField = "ValType"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
    End Sub

    Private Function BindDataSalesman(ByVal ddlSalesmanHeader As DropDownList)
        ddlSalesmanHeader.Items.Clear()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SAPRegister), "IsCancelled", MatchType.Exact, 0))
        '  criterias.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.SAPNumber", MatchType.Exact, txtSapNo.Text.Trim))
        Dim arrSAPRegister As ArrayList = New SAPRegisterFacade(User).Retrieve(criterias)
        Dim strSalesmanHeaderId As String
        strSalesmanHeaderId = ""
        If Not IsNothing(arrSAPRegister) Then
            If arrSAPRegister.Count > 0 Then
                For Each item As SAPRegister In arrSAPRegister
                    If strSalesmanHeaderId = "" Then
                        strSalesmanHeaderId = item.SalesmanHeader.ID
                    Else
                        strSalesmanHeaderId = strSalesmanHeaderId & ";" & item.SalesmanHeader.ID
                    End If
                Next
            End If
        End If

        ' mengambil salesman yg register pd periode bersangkutan
        Dim crits As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If strSalesmanHeaderId <> "" Then
            'Todo Inset
            crits.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.InSet, CommonFunction.GetStrValue(strSalesmanHeaderId, ";", ",")))
        End If
        crits.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)))
        Dim arrSalesmanHeader As ArrayList = New SalesmanHeaderFacade(User).Retrieve(crits)

        'Dim list As New ListItem("Silakan Pilih", "")
        'list.Selected = True
        'ddlSalesmanLevel.Items.Add(list)

        For Each item As SalesmanHeader In arrSalesmanHeader
            Dim _list As New ListItem(item.Name, item.SalesmanCode)
            ddlSalesmanHeader.Items.Add(_list)
        Next
        ddlSalesmanHeader.DataBind()
    End Function

    Private Sub dgSAPCustomer_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSAPCustomer.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSAPCustomer As SAPCustomer = e.Item.DataItem
            'e.Item.Cells(1).Text = 
            ' untuk bagian item / alternate item
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                CType(e.Item.FindControl("lblNomor"), Label).Text = e.Item.ItemIndex + 1 + (dgSAPCustomer.CurrentPageIndex * dgSAPCustomer.PageSize)

                ' mengisi value
                'Dim lblDealerCodeNew As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                'lblDealerCodeNew.Text = objSAPCustomer.SalesmanHeader.Dealer.DealerCode

                Dim lblNameNew As Label = CType(e.Item.FindControl("lblCustomerName"), Label)
                lblNameNew.Text = objSAPCustomer.CustomerName

                Dim lblCodeNew As Label = CType(e.Item.FindControl("lblCustomerCode"), Label)
                lblCodeNew.Text = objSAPCustomer.CustomerCode


                Dim lblStatusNew As Label = CType(e.Item.FindControl("lblStatus"), Label)
                lblStatusNew.Text = CType(objSAPCustomer.Status, EnumSAPCustomerStatus.SAPCustomerStatus).ToString.Replace("_", " ")

                Dim lblQty As Label = CType(e.Item.FindControl("lblQty"), Label)
                lblQty.Text = objSAPCustomer.Qty.ToString()
                'lblSalesmanCodeNew.Text = objSAPCustomer.SAPRegister.SalesmanHeader.SalesmanCode

                Dim lblVechileTypeCode As Label = CType(e.Item.FindControl("lblVechileTypeCode"), Label)
                lblVechileTypeCode.Text = objSAPCustomer.VechileType.VechileTypeCode

                Dim lblProspectDateNew As Label = CType(e.Item.FindControl("lblProspectDate"), Label)
                lblProspectDateNew.Text = objSAPCustomer.ProspectDate.ToString("dd/MM/yyyy")

            End If

        End If
    End Sub
    Private Sub dgSAPCustomer_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSAPCustomer.ItemCommand
        If e.CommandName = "Delete" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim objSAPCustomer As SAPCustomer = New SAPCustomerFacade(User).Retrieve(CInt(lblID.Text))
            If (New SAPCustomerFacade(User).DeleteStatus(objSAPCustomer) <> -1) Then
                BindDataGrid(0)
                MessageBox.Show("Data berhasil dihapus")
            Else
                MessageBox.Show("Data gagal dihapus")
            End If
        End If
    End Sub

    Private Sub dgSAPCustomer_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSAPCustomer.PageIndexChanged
        dgSAPCustomer.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSAPCustomer.CurrentPageIndex)
    End Sub

    Private Sub dgSAPCustomer_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSAPCustomer.SortCommand
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
        dgSAPCustomer.SelectedIndex = -1
        BindDataGrid(dgSAPCustomer.CurrentPageIndex)
    End Sub
#End Region


#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.CustomerProspekListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SAP - Daftar Konsumen Prospek")
        End If
    End Sub

    Private Function CekListDeletePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.CustomerProspekListDelete_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CheckPrivilege()
        InitiateAuthorization()
        If Not IsPostBack Then
            'Dim objuser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            'lblDealerCode.Text = objuser.Dealer.DealerCode
            'lblDealerName.Text = objuser.Dealer.DealerName
            Initialize()
            BindControlsAttribute()
            BindDropDown()
            '  BindDataGrid(0)
            Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
            If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                lblSearchDealer.Visible = False
                txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
                txtKodeDealer.Enabled = False
                dgSAPCustomer.Columns(7).Visible = False
            Else
                dgSAPCustomer.Columns(7).Visible = True
            End If

            ' add security
            If Not CekListDeletePrivilege() Then
                dgSAPCustomer.Columns(7).Visible = False    'icon delete
            End If

        End If
    End Sub

    Private Sub BindControlsAttribute()
        'lblPopUpDealer.Attributes("onClick") = "showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);"
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblPopUpSalesman.Attributes("onclick") = "ShowPPSAP();"
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub
#Region "Need To Add"
    ' penambahan untuk initialize data
    Private Sub ClearData()
        'txtDealerCode.Text = String.Empty
        'txtName.Text = String.Empty
        'txtSalesmanCode.Text = String.Empty
        'txtSapNo.Text = String.Empty
        'lblDateFrom.Text = String.Empty
        'lblDateUntil.Text = String.Empty
        'txtPeriod.Value = String.Empty
        'ddlStatus.SelectedIndex = -1
        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblSearchDealer.Visible = False
            txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
            txtKodeDealer.Enabled = False
        Else
            lblSearchDealer.Visible = True
            txtKodeDealer.Text = String.Empty
            txtKodeDealer.Enabled = True
        End If
        txtSalesmanID.Text = String.Empty
        lblNamaSalesman.Text = String.Empty
        ddlStatus.SelectedIndex = 0
        'dgSAPCustomer.DataSource = New ArrayList
        'dgSAPCustomer.VirtualItemCount = 0
        'dgSAPCustomer.DataBind()

        'If dgSAPCustomer.Items.Count > 0 Then
        '    dgSAPCustomer.SelectedIndex = -1
        'End If

    End Sub



    Private Sub Initialize()
        ClearData()
        'ViewState("CurrentSortColumn") = "AreaCode"
        'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        'ViewState.Add("vsProcess", "Insert")
    End Sub

    ' ini perlu set security
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Daftar SPL")
        End If

        _create = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiBuat_Privilege)
        _edit = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiUbah_Privilege)
        _view = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiDetail_Privilege)

        'lbtnNew.Visible = _create
        btnSearch.Visible = _view

    End Sub

    Private Sub CreateCriteria()
        'remarked by anh 20140417 biar gak muncul di modul Marketing>Konsumen
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, 3))
        'end remarked 

        criterias.opAnd(New Criteria(GetType(SAPCustomer), "ProspectDate", _
                                        MatchType.GreaterOrEqual, icPaymentDateFrom.Value))
        criterias.opAnd(New Criteria(GetType(SAPCustomer), "ProspectDate", _
                                        MatchType.Lesser, icPaymentDateTo.Value.AddDays(1)))

        If txtKodeDealer.Text.Trim() <> "" Then
            Dim strKodeDealerIn As String = "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "SalesmanHeader.Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
            'criterias.opAnd(New Criteria(GetType(SAPCustomer), "Dealer.DealerCode", MatchType.InSet, strKodeDealerIn))
        End If

        If txtSalesmanID.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "SalesmanHeader.SalesmanCode", MatchType.[Partial], txtSalesmanID.Text.Trim()))
        End If

        If CInt(ddlStatus.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If
        sessHelper.SetSession("CRITERIAS", criterias)
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        arrList = _SAPCustomerFacade.RetrieveByCriteria(CType(sessHelper.GetSession("CRITERIAS"), CriteriaComposite), idxPage + 1, dgSAPCustomer.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        sessHelper.SetSession("ViewSAPCustomer", arrList)
        dgSAPCustomer.DataSource = arrList
        dgSAPCustomer.VirtualItemCount = totalRow
        dgSAPCustomer.DataBind()
    End Sub

    Private Function CheckValidation() As Boolean
        Dim blnValid As Boolean = True
        'If (txtAreaCode.Text = "") Then
        '    blnValid = False
        '    MessageBox.Show("Kode Area harus diinput terlebih dahulu")
        '    Return (blnValid)
        'End If

        Return blnValid
    End Function
#End Region


    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        SetDownload()
    End Sub

    Private Sub SetDownload()
        Dim arrList As ArrayList
        If Not sessHelper.GetSession("ViewSAPCustomer") Is Nothing Then
            arrList = sessHelper.GetSession("ViewSAPCustomer")
        End If
        If arrList.Count > 0 Then
            DoDownload(arrList)
        Else
            MessageBox.Show("Tidak ada data yang di download")
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "SAPCustomer" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Dim SAPCustomerData As String = Server.MapPath("").Replace("\SAP", "") & "\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SAPCustomerData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(SAPCustomerData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteSAPKonsumenData(sw, data)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
            Exit Sub
        End Try

        'Response.Write("<script language='javascript'>window.open('../downloadlocal.aspx?file=" & "DataTemp/" & sFileName & ".xls" & "');</script>")

        Response.Redirect("../downloadlocal.aspx?file=" & "DataTemp\" & sFileName & ".xls", False)
    End Sub


    Private Sub WriteSAPKonsumenData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("SAP - Daftar Konsumen Prospek")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            Dim obj As New SAPCustomer

            Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(txtSalesmanID.Text)
            If objSalesmanHeader.ID > 0 Then
                itemLine.Append("" & tab)
                itemLine.Append("Dealer : " & tab)
                itemLine.Append(objSalesmanHeader.Dealer.DealerCode & tab)
                sw.WriteLine(itemLine.ToString())
                itemLine.Remove(0, itemLine.Length)

                itemLine.Append("" & tab)
                itemLine.Append("Nama Dealer : " & tab)
                itemLine.Append(objSalesmanHeader.Dealer.DealerName & tab)
                sw.WriteLine(itemLine.ToString())
                itemLine.Remove(0, itemLine.Length)

                itemLine.Append("" & tab)
                itemLine.Append("Kode Salesman : " & tab)
                itemLine.Append(txtSalesmanID.Text & tab)
                sw.WriteLine(itemLine.ToString())
                itemLine.Remove(0, itemLine.Length)

                itemLine.Append("" & tab)
                itemLine.Append("Nama Salesman : " & tab)
                itemLine.Append(lblNamaSalesman.Text & tab)
                sw.WriteLine(itemLine.ToString())
                itemLine.Remove(0, itemLine.Length)

            End If

            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Nama Konsumen" & tab)
            itemLine.Append("Alamat" & tab)
            itemLine.Append("Status" & tab)
            itemLine.Append("Kuantitas" & tab)
            itemLine.Append("Kode Kendaraan" & tab)
            itemLine.Append("Tanggal Prospect" & tab)            
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As SAPCustomer In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.CustomerName & tab)
                itemLine.Append(item.CustomerAddress & tab)
                If item.Status = 1 Then
                    itemLine.Append("Hot Prospect" & tab)
                ElseIf item.Status = 2 Then
                    itemLine.Append("Prospect" & tab)
                End If

                itemLine.Append(item.Qty & tab)
                itemLine.Append(item.VechileType.VechileTypeCode & tab)
                itemLine.Append(item.ProspectDate & tab)
                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub

End Class
