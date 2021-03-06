Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports System.IO
Imports System.Text

Public Class FrmPartShopList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents txtPartShopCode As System.Web.UI.WebControls.TextBox
    'Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblUser As System.Web.UI.WebControls.Label
    'Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnRequestID As System.Web.UI.WebControls.Button
    Protected WithEvents btnRegister As System.Web.UI.WebControls.Button
    Protected WithEvents ddlMonth1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlYear2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlMonth2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblFileName As System.Web.UI.WebControls.Label
    Protected WithEvents dgPartShop As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPartShopID As System.Web.UI.WebControls.Label
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents valName As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtAddress As System.Web.UI.WebControls.TextBox
    Protected WithEvents valAddress As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlProvince As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSeparator As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCity As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCityPart As System.Web.UI.WebControls.DropDownList
    Protected WithEvents valProvince As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents valCity As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtPhone As System.Web.UI.WebControls.TextBox
    Protected WithEvents valPhone As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents lblSprPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents lblSprPeriode2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtFax As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblJumlah As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnDeActivated As System.Web.UI.WebControls.Button
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private sessHelper As New SessionHelper
    Private arlPartShop As New ArrayList
    Private objDealer As Dealer
    Dim strFileNm As String
    Dim strFileNmHeader As String
#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Lihat_part_shop_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Partshop - Daftar Partshop")
        End If
    End Sub
    Dim Priv_KTB_Generate As Boolean = SecurityProvider.Authorize(Context.User, SR.Generate_part_shop_privilege)
    Dim Priv_KTB_Delete As Boolean = SecurityProvider.Authorize(Context.User, SR.Delete_part_shop_privilege)
    Dim Priv_KTB_DeActivated As Boolean = SecurityProvider.Authorize(Context.User, SR.Nonaktif_part_shop_privilege)
    Dim Priv_Dealer_Request As Boolean = SecurityProvider.Authorize(Context.User, SR.Input_part_shop_privilege)
    Dim Priv_All_Download As Boolean = SecurityProvider.Authorize(Context.User, SR.Download_part_shop_privilege)
    Dim Priv_All_ViewEdit As Boolean = SecurityProvider.Authorize(Context.User, SR.Edit_part_shop_privilege)
    '
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))
        If Not IsPostBack Then

            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtDealerCode.Text = objDealer.DealerCode
                txtDealerCode.Enabled = False
                lblSearchDealer.Visible = False
            End If
            Select Case Request.QueryString("mode")
                Case "list"
                    lblPageTitle.Text = "PARTSHOP - Daftar Part Shop"
                    VisibilityControl(True)
                    BindDdlPeriode()
                Case "entry"
                    lblPageTitle.Text = "PARTSHOP - Entry Part Shop"
                    VisibilityControl(False)
                Case "Registration"
                    lblPageTitle.Text = "PARTSHOP - Registrasi Part Shop"
                    VisibilityControl(False)

            End Select
            btnSave.Visible = False
            BindDDlProvince()
            BindDDlStatus()
            'BindDDlCity()
            InitiateControl(False)
            sessHelper.SetSession("CurrentSortColumn", "PartShopCode")
            sessHelper.SetSession("CurrentSortDirect", Sort.SortDirection.ASC)
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        BindDataGrid()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavePartShop()
    End Sub

    Private Sub btnRequestID_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRequestID.Click
        Dim iChecked As Integer = 0
        Dim arlPartShop As New ArrayList

        For Each item As DataGridItem In dgPartShop.Items
            Dim chkItem As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim status As Label = CType(item.FindControl("lblStatus"), Label)
            If (chkItem.Checked) And (status.Text = "Baru") Then
                iChecked += 1
                Dim id As Integer = item.Cells(0).Text
                If id > 0 Then
                    Dim objPartShop As PartShop = New PartShopFacade(User).Retrieve(id)
                    If Not objPartShop Is Nothing Then
                        objPartShop.Status = 1 'Request Registered
                        Dim nResult As Integer = New PartShopFacade(User).Update(objPartShop)
                        arlPartShop.Add(objPartShop)
                    End If
                End If
            End If
        Next
        If arlPartShop.Count > 0 Then
            SendEmail(arlPartShop)
            BindDataGridPartShop(dgPartShop.CurrentPageIndex)
        End If

        If iChecked = 0 Then
            MessageBox.Show("Tidak ada data part shop baru yang dipilih")
        End If
    End Sub

    Private Sub btnRegister_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegister.Click
        Dim iChecked As Integer = 0
        Dim arlPartShop As New ArrayList

        For Each item As DataGridItem In dgPartShop.Items
            Dim iStatus As Integer = CType(item.Cells(1).Text, Integer)
            Dim chkItem As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim status As Label = CType(item.FindControl("lblStatus"), Label)
            If (chkItem.Checked) And iStatus = EnumPartShopStatus.PartShopStatus.Request_ID Then
                iChecked += 1
                Dim id As Integer = item.Cells(0).Text
                If id > 0 Then
                    Dim objPartShop As PartShop = New PartShopFacade(User).Retrieve(id)

                    Dim criterias As New CriteriaComposite(New Criteria(GetType(PartShop), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(PartShop), "PartShopCode", MatchType.No, String.Empty))

                    ' only update the citypart if viewstate is city and citypart is null 
                    If IsNothing(objPartShop.CityPart) Then
                        criterias.opAnd(New Criteria(GetType(PartShop), "City.ID", MatchType.Exact, objPartShop.City.ID))
                        Dim objCityPart As New CityPart
                        objCityPart = New CityPartFacade(User).Retrieve(objPartShop.City.ID, False)
                        If objCityPart Is Nothing Then
                            MessageBox.Show("Data CityPart Belum Tersedia Silahkan Melakukan Update Pada CityPart")
                            Return
                        End If
                        objPartShop.CityPart = objCityPart
                    Else
                        criterias.opAnd(New Criteria(GetType(PartShop), "CityPart.ID", MatchType.Exact, objPartShop.CityPart.ID))
                    End If


                    'Dim StrCrit As String = String.Empty
                    'StrCrit = CInt(EnumPartShopStatus.PartShopStatus.Tidak_Aktif).ToString()
                    'StrCrit = StrCrit & ", " & CInt(EnumPartShopStatus.PartShopStatus.Request_ID).ToString()

                    'criterias.opAnd(New Criteria(GetType(PartShop), "Status", MatchType.NotInSet, StrCrit))
                    Dim sortColl As SortCollection = New SortCollection
                    sortColl.Add(New Sort(GetType(PartShop), "PartShopCode", Sort.SortDirection.DESC))
                    Dim arlPartShop2 As ArrayList = New PartShopFacade(User).Retrieve(criterias, sortColl)

                    If Not IsNothing(arlPartShop2) AndAlso arlPartShop2.Count > 1 Then
                        Dim oPartshop As PartShop = CType(arlPartShop2(0), PartShop)
                        If Not oPartshop Is Nothing Then
                            Dim code As String = oPartshop.PartShopCode
                            If code.Length = 8 AndAlso code.Substring(4, 4) = "9999" Then
                                MessageBox.Show("Jumlah partshop pada kota " & oPartshop.CityPart.CityName & " sudah mencapai kuota. Hubungi MMKSI")
                                btnCari_Click(Me, Nothing)
                                Return
                            End If
                        End If

                    End If

                    If Not objPartShop Is Nothing Then
                        objPartShop.Status = EnumPartShopStatus.PartShopStatus.Aktif 'Request Registered
                        Dim nResult As Integer = New PartShopFacade(User).Update(objPartShop)
                        arlPartShop.Add(objPartShop)
                    End If
                End If
            End If
        Next
        If arlPartShop.Count > 0 Then
            BindDataGridPartShop(dgPartShop.CurrentPageIndex)
            MessageBox.Show(SR.UpdateSucces)
        End If

        If iChecked = 0 Then
            MessageBox.Show("Tidak ada data part shop dengan status Request ID yang dipilih")
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        DownloadPartshop()
    End Sub

    Private Sub ddlProvince_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProvince.SelectedIndexChanged
        If ddlProvince.SelectedValue <> "" Then
            CommonFunction.BindActiveCity(ddlCity, User, True, ddlProvince.SelectedValue, False)
        Else
            ddlCity.Items.Clear()
        End If
        ddlCityPart.Items.Clear()
    End Sub

    Protected Sub ddlCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCity.SelectedIndexChanged
        If ddlProvince.SelectedValue <> "" AndAlso ddlCity.SelectedValue <> "" Then
            CommonFunction.BindCityPart(ddlCityPart, User, True, ddlCity.SelectedValue, False)
        Else
            ddlCityPart.Items.Clear()
        End If
    End Sub

    Private Sub dgPartShop_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartShop.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objPartShop As PartShop = e.Item.DataItem
            Dim lblDealerPartshop As Label = e.Item.FindControl("lblDealerPartshop")
            Dim lblProvince As Label = e.Item.FindControl("lblPropinsi")
            Dim lblCity As Label = e.Item.FindControl("lblKota")
            Dim lblCityPart As Label = e.Item.FindControl("lblKotaPart")
            Dim lblStatus As Label = e.Item.FindControl("lblStatus")
            Dim lbtnActive As LinkButton = e.Item.FindControl("lbtnActive")
            Dim lbtnInActive As LinkButton = e.Item.FindControl("lbtnInActive")
            Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")
            Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
            objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            e.Item.Cells(2).Text = e.Item.ItemIndex + 1 + (dgPartShop.CurrentPageIndex * dgPartShop.PageSize)

            lbtnActive.Visible = False
            lbtnInActive.Visible = False
            lbtnEdit.Visible = False
            lbtnDelete.Visible = False


            If Not objPartShop Is Nothing Then
                Dim EnumStatus As EnumPartShopStatus.PartShopStatus = objPartShop.Status
                If objPartShop.City Is Nothing And objPartShop.CityPart Is Nothing Then
                    e.Item.BackColor = Color.Yellow
                    'MessageBox.Show("Data CityPart & City Belum Tersedia untuk Nama PartShop  : " + objPartShop.Name)
                    Dim chkItemChecked As CheckBox = e.Item.FindControl("chkItemChecked")
                    chkItemChecked.Visible = False
                    lbtnDelete.Visible = True
                    Return
                End If


                If Not IsNothing(objPartShop.CityPart) Then
                    lblCityPart.Text = objPartShop.CityPart.CityName
                Else
                    lblCityPart.Text = String.Empty
                End If

                If Not IsNothing(objPartShop.City) Then
                    lblProvince.Text = objPartShop.City.Province.ProvinceName
                    lblCity.Text = objPartShop.City.CityName
                Else
                    lblProvince.Text = String.Empty
                    lblCity.Text = String.Empty
                End If

                If Not objPartShop.Dealer Is Nothing Then
                    lblDealerPartshop.Text = objPartShop.Dealer.DealerCode
                End If
                lblStatus.Text = EnumStatus.ToString.Replace("_", " ")
                'If Not objPartShop.Dealer Is Nothing Then
                '    If objPartShop.Dealer.ID = objDealer.ID Or objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                lbtnEdit.Visible = Priv_All_ViewEdit 'True
                '    End If
                'End If

                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    If objPartShop.Status = EnumPartShopStatus.PartShopStatus.Aktif Then
                        lbtnInActive.Visible = Priv_KTB_DeActivated 'True
                    ElseIf objPartShop.Status = EnumPartShopStatus.PartShopStatus.Tidak_Aktif Then
                        lbtnActive.Visible = Priv_KTB_DeActivated 'True
                    End If
                    lbtnDelete.Visible = Priv_KTB_Delete 'True
                End If
            End If
            'If Request.QueryString("mode") = "list" Then
            '    e.Item.Cells(3).Visible = False
            '    'e.Item.Cells(12).Visible = False
        End If
    End Sub

    Private Sub dgPartShop_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPartShop.ItemCommand
        Dim id As Integer
        If (e.CommandName = "edit") Then
            id = e.Item.Cells(0).Text
            BindItemPartShop(id)
        ElseIf e.CommandName = "delete" Then
            id = e.Item.Cells(0).Text
            DeletePartShop(id)
        ElseIf e.CommandName = "activate" Then
            id = e.Item.Cells(0).Text
            ActivationPartShop(EnumPartShopStatus.PartShopStatus.Aktif, id)
        ElseIf e.CommandName = "inactivate" Then
            id = e.Item.Cells(0).Text
            ActivationPartShop(EnumPartShopStatus.PartShopStatus.Tidak_Aktif, id)
        End If

    End Sub

    Private Sub dgPartShop_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPartShop.SortCommand
        If sessHelper.GetSession("CurrentSortColumn") = e.SortExpression Then
            Select Case CType(sessHelper.GetSession("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    sessHelper.SetSession("CurrentSortDirect", Sort.SortDirection.DESC)
                Case Sort.SortDirection.DESC
                    sessHelper.SetSession("CurrentSortDirect", Sort.SortDirection.ASC)
            End Select
        Else
            sessHelper.SetSession("CurrentSortColumn", e.SortExpression)
            sessHelper.SetSession("CurrentSortDirect", Sort.SortDirection.ASC)

        End If
        BindDataGrid()
    End Sub

    Private Sub dgPartShop_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPartShop.PageIndexChanged
        dgPartShop.CurrentPageIndex = e.NewPageIndex
        BindDataGridPartShop(dgPartShop.CurrentPageIndex)
    End Sub

    Private Sub btnDeActivated_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeActivated.Click
        Dim iChecked As Integer = 0
        Dim arlPartShop As New ArrayList

        For Each item As DataGridItem In dgPartShop.Items
            Dim chkItem As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim status As Label = CType(item.FindControl("lblStatus"), Label)
            If (chkItem.Checked) And (status.Text = "Aktif") Then
                iChecked += 1
                Dim id As Integer = item.Cells(0).Text
                If id > 0 Then
                    Dim objPartShop As PartShop = New PartShopFacade(User).Retrieve(id)
                    If Not objPartShop Is Nothing Then
                        arlPartShop.Add(objPartShop)
                    End If
                End If
            End If
        Next
        If arlPartShop.Count > 0 Then
            SendEmail(arlPartShop, "Pengajuan Non Aktif Data Partshop")
            MessageBox.Show("Pengajuan Non Aktif Partshop berhasil.")
            BindDataGridPartShop(dgPartShop.CurrentPageIndex)
        End If

        If iChecked = 0 Then
            MessageBox.Show("Tidak ada data part shop status aktif yang dipilih")
        End If
    End Sub
#End Region

#Region " Custom"

    Private Sub InitiateControl(Optional ByVal isPostBack As Boolean = True)
        lblPartShopID.Text = String.Empty
        txtPartShopCode.Text = String.Empty
        txtName.Text = String.Empty
        txtAddress.Text = String.Empty
        txtPhone.Text = String.Empty
        txtFax.Text = String.Empty
        ddlProvince.SelectedIndex = -1
        ddlCity.SelectedIndex = -1
        ddlCityPart.SelectedIndex = -1
        ddlStatus.Enabled = True
        If Not isPostBack Then
            ddlStatus.SelectedIndex = -1
        End If

        sessHelper.SetSession("objPartShop", Nothing)

        btnRegister.Enabled = Priv_KTB_Generate
        'btnRequestID.Enabled = Priv_Dealer_Request
        btnDownload.Enabled = Priv_All_Download
        btnDeActivated.Enabled = Priv_Dealer_Request
    End Sub

    Private Sub VisibilityControl(ByVal val As Boolean)
        lblPeriode.Visible = val
        lblSprPeriode.Visible = val
        lblSprPeriode2.Visible = val
        ddlMonth1.Visible = val
        ddlMonth2.Visible = val
        ddlYear1.Visible = val
        ddlYear2.Visible = val

        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            btnRegister.Visible = False
            btnRequestID.Visible = True
            txtPartShopCode.Visible = False
            lblPartShopID.Visible = True
            btnDeActivated.Visible = True
        Else
            btnRegister.Visible = True
            btnDeActivated.Visible = False
            btnRequestID.Visible = False
            txtPartShopCode.Visible = True
            lblPartShopID.Visible = False
        End If
    End Sub

    Private Sub BindDdlPeriode()
        Try
            ddlMonth1.Items.Clear()
            ddlMonth1.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayMonth()
                item.Selected = False
                ddlMonth1.Items.Add(item)
            Next
            ddlMonth1.ClearSelection()
            'ddlMonth1.SelectedIndex = 0 ' CType(Format(DateTime.Now, "MM").ToString, Integer)
            Dim iMonth As Integer = CType(Format(DateTime.Now, "MM").ToString, Integer)
            If (iMonth - 3) < 0 Then
                ddlMonth1.SelectedValue = iMonth + 10
            ElseIf (iMonth - 3) = 0 Then
                ddlMonth1.SelectedIndex = 1
            Else
                ddlMonth1.SelectedValue = iMonth - 2
            End If
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlSPKMonth1, silahkan kirim error ini ke dnet admin")
        End Try

        Try
            ddlYear1.Items.Clear()
            ddlYear1.Items.Insert(0, New ListItem("Pilih", -1))
            For Each item As ListItem In LookUp.ArrayYear(True, 2, 8, Date.Now.Year.ToString)
                item.Selected = False
                ddlYear1.Items.Add(item)
            Next
            ddlYear1.ClearSelection()
            'ddlYear1.SelectedIndex = 0
            Dim iMonth As Integer = CType(Format(DateTime.Now, "MM").ToString, Integer)
            Dim iYear As Integer = CType(Format(DateTime.Now, "yyyy").ToString, Integer)
            If (iMonth - 3) < 0 Then
                ddlYear1.SelectedValue = (iYear - 1).ToString
            Else
                ddlYear1.SelectedValue = iYear
            End If
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlYear1, silahkan kirim error ini ke dnet admin")
        End Try

        Try
            ddlMonth2.Items.Clear()
            ddlMonth2.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArrayMonth()
                item.Selected = False
                ddlMonth2.Items.Add(item)
            Next
            ddlMonth2.ClearSelection()
            'ddlMonth2.SelectedIndex = 0 ' CType(Format(DateTime.Now, "MM").ToString, Integer)
            ddlMonth2.SelectedValue = CType(Format(DateTime.Now, "MM").ToString, Integer)
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlMonth2, silahkan kirim error ini ke dnet admin")
        End Try

        Try
            ddlYear2.Items.Clear()
            ddlYear2.Items.Insert(0, New ListItem("Pilih", -1))
            For Each item As ListItem In LookUp.ArrayYear(True, 2, 8, Date.Now.Year.ToString)
                item.Selected = False
                ddlYear2.Items.Add(item)
            Next
            ddlYear2.ClearSelection()
            'ddlYear2.SelectedIndex = 0
            ddlYear2.SelectedValue = CType(Format(DateTime.Now, "yyyy").ToString, String)
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlYear2, silahkan kirim error ini ke dnet admin")
        End Try
    End Sub

    Private Sub BindDDlProvince()
        CommonFunction.BindProvince(ddlProvince, User, True, False)
    End Sub

    Private Sub BindDDlCity()
        CommonFunction.BindActiveCity(ddlCity, User, True, ddlProvince.SelectedValue, False)
    End Sub

    Private Sub BindDDlCityPart()
        CommonFunction.BindCityPart(ddlCityPart, User, True, ddlCity.SelectedValue, False)
    End Sub

    Private Sub BindDDlStatus()
        CommonFunction.BindFromEnum("PartShopStatus", ddlStatus, User, True, "NameStatus", "ValStatus")
    End Sub

    Private Sub BindDataGrid()
        dgPartShop.CurrentPageIndex = 0
        BindDataGridPartShop(dgPartShop.CurrentPageIndex)
    End Sub

    Private Sub BindDataGridPartShop(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PartShop), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'If (txtDealerCode.Text.Trim <> String.Empty) Then
        '    criterias.opAnd(New Criteria(GetType(PartShop), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
        'End If
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        'If Not objDealer Is Nothing And objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
        '    'Dim dealerGroupID As Integer = objDealer.DealerGroup.ID
        '    'Dim strSql = "(select PartShopCode from PartShop where Substring(PartShopCode,0,3) in (select SUBSTRING(SearchTerm2,0,3) from Dealer where DealerGroupID= " & dealerGroupID & "))"
        '    'criterias.opAnd(New Criteria(GetType(PartShop), "PartShopCode", MatchType.InSet, strSql))
        '    criterias.opAnd(New Criteria(GetType(PartShop), "DealerID", MatchType.InSet, "(select ID from Dealer where DealerGroupID=" & objDealer.DealerGroup.ID.ToString & ")"))
        'End If
        If (txtName.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(PartShop), "Name", MatchType.[Partial], txtName.Text.Trim))
        Else
            'If Not objDealer Is Nothing And objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            '    criterias.opAnd(New Criteria(GetType(PartShop), "Dealer.ID", MatchType.InSet, "(select ID from Dealer where DealerGroupID=" & objDealer.DealerGroup.ID.ToString & ")"))
            'End If
        End If
        If (txtPartShopCode.Text <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(PartShop), "PartShopCode", MatchType.[Partial], txtPartShopCode.Text.Trim))
        End If
        If (txtAddress.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(PartShop), "Address", MatchType.[Partial], txtAddress.Text.Trim))
        End If
        If (txtPhone.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(PartShop), "Phone", MatchType.[Partial], txtPhone.Text.Trim))
        End If
        If (txtFax.Text.Trim <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(PartShop), "Fax", MatchType.[Partial], txtFax.Text.Trim))
        End If

        If (ddlCityPart.SelectedValue <> "") Then
            criterias.opAnd(New Criteria(GetType(PartShop), "CityPart.ID", MatchType.Exact, ddlCityPart.SelectedValue))
        ElseIf (ddlCity.SelectedValue <> "") Then
            criterias.opAnd(New Criteria(GetType(PartShop), "City.ID", MatchType.Exact, ddlCity.SelectedValue))
        ElseIf (ddlProvince.SelectedValue <> "") Then
            criterias.opAnd(New Criteria(GetType(PartShop), "City.Province.ID", MatchType.Exact, ddlProvince.SelectedValue))
        End If

        If (ddlStatus.SelectedValue <> -1) Then
            criterias.opAnd(New Criteria(GetType(PartShop), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If
        If Request.QueryString("mode") = "list" Then
            If ddlMonth1.SelectedIndex <> 0 And ddlYear1.SelectedIndex <> 0 Then
                Dim startDate As DateTime = New DateTime(CType(ddlYear1.SelectedValue, Integer), CType(ddlMonth1.SelectedValue, Integer), 1, 0, 0, 0)
                criterias.opAnd(New Criteria(GetType(PartShop), "CreatedTime", MatchType.GreaterOrEqual, startDate))
                If ddlMonth2.SelectedIndex <> 0 And ddlYear2.SelectedIndex <> 0 Then
                    Dim endDate As DateTime = New DateTime(CType(ddlYear2.SelectedValue, Integer), CType(ddlMonth2.SelectedValue, Integer), Date.DaysInMonth(ddlYear2.SelectedValue, ddlMonth2.SelectedValue), 23, 59, 59)
                    criterias.opAnd(New Criteria(GetType(PartShop), "CreatedTime", MatchType.LesserOrEqual, endDate))
                End If
            End If
        End If

        arlPartShop = New PartShopFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgPartShop.PageSize, totalRow, _
        sessHelper.GetSession("CurrentSortColumn"), sessHelper.GetSession("CurrentSortDirect"))
        sessHelper.SetSession("criteriadownload", criterias)
        sessHelper.SetSession("PartshopList", arlPartShop)
        If idxPage <= 0 Then
            idxPage = 0
        ElseIf idxPage >= dgPartShop.PageCount Then
            idxPage = dgPartShop.PageCount - 1
        End If

        dgPartShop.CurrentPageIndex = idxPage
        dgPartShop.DataSource = arlPartShop
        dgPartShop.VirtualItemCount = totalRow
        dgPartShop.DataBind()

        Me.lblJumlah.Text = FormatNumber(totalRow, 0, TriState.False, TriState.True, TriState.True)
    End Sub

    Private Sub BindItemPartShop(ByVal id As Integer)
        Dim objPartShop As PartShop = New PartShopFacade(User).Retrieve(id)
        If Not objPartShop Is Nothing Then
            If Not IsNothing(objPartShop.CityPart) Then
                ddlProvince.SelectedValue = objPartShop.CityPart.City.Province.ID
                ddlProvince_SelectedIndexChanged(Nothing, Nothing)
                If (objPartShop.CityPart.City.Status = "A") Then
                    ddlCity.SelectedValue = objPartShop.CityPart.City.ID
                    ddlCity_SelectedIndexChanged(Nothing, Nothing)
                    ddlCityPart.SelectedValue = objPartShop.CityPart.ID
                Else
                    MessageBox.Show("Kota " + objPartShop.CityPart.City.CityName + " tidak aktif")
                End If
            ElseIf Not IsNothing(objPartShop.City) Then
                ddlProvince.SelectedValue = objPartShop.City.Province.ID
                ddlProvince_SelectedIndexChanged(Nothing, Nothing)
                If (objPartShop.City.Status = "A") Then
                    ddlCity.SelectedValue = objPartShop.City.ID
                    ddlCity_SelectedIndexChanged(Nothing, Nothing)
                Else
                    MessageBox.Show("Kota " + objPartShop.City.CityName + " tidak aktif")
                End If
            End If

            txtPartShopCode.Text = objPartShop.PartShopCode
            lblPartShopID.Text = objPartShop.PartShopCode
            txtName.Text = objPartShop.Name
            txtAddress.Text = objPartShop.Address
            txtPhone.Text = objPartShop.Phone
            txtFax.Text = objPartShop.Fax

            ddlStatus.SelectedValue = objPartShop.Status
            ddlStatus.Enabled = False
            sessHelper.SetSession("objPartShop", objPartShop)
            btnSave.Visible = True
            btnSave.Enabled = True
        End If
    End Sub

    Private Sub SavePartShop()
        If IsDataValid() Then
            Dim objPartShop As PartShop
            Dim objPartShopOld As PartShop
            Dim objPartShopNew As PartShop
            Dim objPartShopFacade As PartShopFacade = New PartShopFacade(User)

            Dim objCity As City = New City
            Dim objCityFacade As CityFacade = New CityFacade(User)

            Dim objCityPart As CityPart = New CityPart
            Dim objCityPartFacade As CityPartFacade = New CityPartFacade(User)
            Dim nResult As Integer = -1
            Dim isNameUpdate As Boolean = False
            Dim isAddressUpdate As Boolean = False

            Try
                Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
                objCityPart = New CityPartFacade(User).Retrieve(CInt(ddlCityPart.SelectedValue))
                objCity = New CityFacade(User).Retrieve(CInt(ddlCity.SelectedValue))
                If Not sessHelper.GetSession("objPartShop") Is Nothing Then
                    objPartShop = CType(sessHelper.GetSession("objPartShop"), PartShop)

                    If Not (objPartShop.ID = Nothing) Then
                        objPartShopOld = objPartShop
                    End If
                Else
                    objPartShopOld = New PartShop
                End If

                objPartShopNew = New PartShop
                'objPartShopNew = objPartShop
                With objPartShopNew
                    .ID = objPartShop.ID
                    .PartShopCode = txtPartShopCode.Text.Trim
                    .Dealer = objDealer
                    .Name = txtName.Text.Trim
                    .Address = txtAddress.Text.Trim
                    .Phone = txtPhone.Text.Trim
                    .Fax = txtFax.Text.Trim
                    .City = objCity
                    .CityPart = objCityPart
                    .Status = objPartShopOld.Status
                    '.Status = ddlStatus.SelectedValue
                End With
                If objPartShopOld.Name.Trim.ToUpper <> txtName.Text.Trim.ToUpper Then
                    isNameUpdate = True
                End If
                If objPartShopOld.Address.Trim.ToUpper <> txtAddress.Text.Trim.ToUpper Then
                    isAddressUpdate = True
                End If

                If objPartShopOld.CityPart Is Nothing OrElse objPartShop.CityPart Is Nothing Then

                    If objPartShopOld.City.ID <> objCity.ID Then
                        MessageBox.Show("Provinsi dan Kota tidak dapat diubah, jika berubah non aktifkan dahulu kemudian daftarkan kembali")
                        btnSave.Enabled = False
                        Exit Sub
                    End If

                Else
                    ' issue redmine #36403, 
                    ' because when status Active and user update data city compared with citypart, can't update data
                    If objPartShopOld.CityPart.City.ID <> objCityPart.City.ID Then
                        MessageBox.Show("Provinsi dan Kota tidak dapat diubah, jika berubah non aktifkan dahulu kemudian daftarkan kembali")
                        btnSave.Enabled = False
                        Exit Sub
                    End If
                End If

                If Not (objPartShopNew.ID = Nothing) Then
                    nResult = objPartShopFacade.Update(objPartShopNew)
                    If isNameUpdate Or isAddressUpdate Then
                        SendEmail(objPartShopOld, objPartShopNew)
                    End If
                    'Else
                    'nResult = objPartShopFacade.Insert(objPartShopNew)
                End If

            Catch ex As Exception
                nResult = -1
            End Try
            If nResult <> -1 Then
                MessageBox.Show(SR.UpdateSucces)
                InitiateControl()
                BindDataGridPartShop(dgPartShop.CurrentPageIndex)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        End If
    End Sub

    Private Sub DeletePartShop(ByVal id As Integer)
        Dim objPartShop As PartShop = New PartShop
        Dim objPartShopFacade As PartShopFacade = New PartShopFacade(User)
        objPartShop = objPartShopFacade.Retrieve(id)
        If Not objPartShop Is Nothing Then
            Try
                objPartShopFacade.Delete(objPartShop)
                MessageBox.Show(SR.DeleteSucces)
                InitiateControl()
                BindDataGridPartShop(dgPartShop.CurrentPageIndex)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        End If
    End Sub

    Private Sub ActivationPartShop(ByVal status As EnumPartShopStatus.PartShopStatus, ByVal id As Integer)
        Dim nResult As Integer = -1
        Dim objPartShop As PartShop = New PartShop
        Dim objPartShopFacade As PartShopFacade = New PartShopFacade(User)
        objPartShop = objPartShopFacade.Retrieve(id)
        If Not objPartShop Is Nothing Then
            Try
                objPartShop.Status = status
                nResult = objPartShopFacade.Update(objPartShop)
                If nResult <> -1 Then
                    MessageBox.Show(SR.UpdateSucces)
                    InitiateControl()
                    BindDataGridPartShop(dgPartShop.CurrentPageIndex)
                Else
                    MessageBox.Show(SR.UpdateFail)
                End If
            Catch ex As Exception
                MessageBox.Show(SR.UpdateFail)
            End Try
        End If
    End Sub

    Private Sub DownloadPartshop()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgPartShop.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        If Not IsNothing(sessHelper.GetSession("criteriadownload")) Then
            crits = CType(sessHelper.GetSession("criteriadownload"), CriteriaComposite)
        End If
        ' mengambil data yang dibutuhkan
        arrData = New KTB.DNet.BusinessFacade.Salesman.PartShopFacade(User).RetrieveByCriteria(crits)
        If arrData.Count > 0 Then
            DoDownload(arrData)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        strFileNm = "Daftar Partshop"
        strFileNmHeader = "Daftar Partshop"

        sFileName = strFileNm & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        Dim ListData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        Dim _user As String
        _user = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String
        _password = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String
        _webServer = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            'If imp.Start() Then

            Dim finfo As FileInfo = New FileInfo(ListData)
            If finfo.Exists Then
                finfo.Delete()
            End If

            Dim fs As FileStream = New FileStream(ListData, FileMode.CreateNew)
            Dim sw As StreamWriter = New StreamWriter(fs)
            If Not IsNothing(objDealer) Then
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    WriteListDataDealer(sw, data)
                Else
                    WriteListDataKTB(sw, data)
                End If
            Else
                WriteListDataDealer(sw, data)
            End If
            sw.Close()
            fs.Close()
            '    imp.StopImpersonate()
            '    imp = Nothing
            'End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")
        Catch ex As Exception
            Dim dummy As Integer = 0
            MessageBox.Show("Download data gagal. " & ex.Message)
        End Try
    End Sub

    Private Sub WriteListDataDealer(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)

        Dim itemLine As StringBuilder = New StringBuilder
        Dim objPartShop As New PartShopFacade(User)
        Try
            If Not IsNothing(data) Then
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(strFileNmHeader)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(" " & tab & tab)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("No" & tab)
                'itemLine.Append("Dealer Yang Mengajukan" & tab)
                itemLine.Append("Kode Partshop" & tab)
                itemLine.Append("Kode Old Partshop" & tab)
                itemLine.Append("Nama Partshop" & tab)
                itemLine.Append("Alamat" & tab)
                itemLine.Append("Propinsi" & tab)
                itemLine.Append("Kota" & tab)
                itemLine.Append("Kota Part" & tab)
                itemLine.Append("Telp" & tab)
                itemLine.Append("Fax" & tab)
                itemLine.Append("Status" & tab)
                sw.WriteLine(itemLine.ToString())

                Dim i As Integer = 1
                For Each item As PartShop In data
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    'If Not item.Dealer Is Nothing Then
                    '    itemLine.Append(item.Dealer.DealerCode & tab)
                    'Else
                    '    itemLine.Append("" & tab)
                    'End If
                    itemLine.Append(item.PartShopCode & tab)
                    itemLine.Append(item.OldPartShopCode & tab)
                    itemLine.Append(item.Name & tab)
                    itemLine.Append(item.Address & tab)

                    If Not IsNothing(item.City) Then
                        If Not IsNothing(item.City.Province) Then
                            itemLine.Append(item.City.Province.ProvinceName & tab)
                        Else
                            itemLine.Append(String.Empty & tab)
                        End If
                        itemLine.Append(item.City.CityName & tab)
                    Else
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                    End If

                    If Not IsNothing(item.CityPart) Then
                        itemLine.Append(item.CityPart.CityName & tab)
                    Else
                        itemLine.Append(String.Empty & tab)
                    End If
                    itemLine.Append("'" & item.Phone & tab)
                    itemLine.Append(item.Fax & tab)
                    Dim strStatus As String
                    itemLine.Append(EnumPartShopStatus.GetStringValue(item.Status) & tab)
                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Download gagal.")
        End Try

    End Sub

    Private Sub WriteListDataKTB(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)

        Dim itemLine As StringBuilder = New StringBuilder
        Dim objPartShop As New PartShopFacade(User)
        Try
            If Not IsNothing(data) Then
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(strFileNmHeader)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(" " & tab & tab)
                sw.WriteLine(itemLine)
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append("No" & tab)
                itemLine.Append("Dealer Yang Mendaftarkan" & tab)
                itemLine.Append("Kode Partshop" & tab)
                itemLine.Append("Kode Old Partshop" & tab)
                itemLine.Append("Nama Partshop" & tab)
                itemLine.Append("Alamat" & tab)
                itemLine.Append("Propinsi" & tab)
                itemLine.Append("Kota" & tab)
                itemLine.Append("Kota Part" & tab)
                itemLine.Append("Telp" & tab)
                itemLine.Append("Fax" & tab)
                itemLine.Append("Status" & tab)
                sw.WriteLine(itemLine.ToString())

                Dim i As Integer = 1
                For Each item As PartShop In data
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    If Not item.Dealer Is Nothing Then
                        itemLine.Append(item.Dealer.DealerCode & tab)
                    Else
                        itemLine.Append("" & tab)
                    End If
                    itemLine.Append(item.PartShopCode & tab)
                    itemLine.Append(item.OldPartShopCode & tab)
                    itemLine.Append(item.Name & tab)
                    itemLine.Append(item.Address & tab)

                    If Not IsNothing(item.City) Then
                        If Not IsNothing(item.City.Province) Then
                            itemLine.Append(item.City.Province.ProvinceName & tab)
                        Else
                            itemLine.Append(String.Empty & tab)
                        End If
                        itemLine.Append(item.City.CityName & tab)
                    Else
                        itemLine.Append(String.Empty & tab)
                        itemLine.Append(String.Empty & tab)
                    End If

                    If Not IsNothing(item.CityPart) Then
                        itemLine.Append(item.CityPart.CityName & tab)
                    Else
                        itemLine.Append(String.Empty & tab)
                    End If
                    itemLine.Append("'" & item.Phone & tab)
                    itemLine.Append(item.Fax & tab)
                    Dim strStatus As String
                    itemLine.Append(EnumPartShopStatus.GetStringValue(item.Status) & tab)
                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Download gagal.")
        End Try

    End Sub

    Private Function IsDataValid() As Boolean
        txtPhone.Text = ValidatePhone(txtPhone.Text)
        If Not ValidateLengthPhone(txtPhone.Text) Then Return False

        Return True
    End Function

    Private Function ValidatePhone(ByRef strPhone As String) As String
        strPhone = strPhone.Trim
        If strPhone.Length > 0 Then
            If Left(strPhone, 1) = "-" OrElse Left(strPhone, 1) = "/" Then
                strPhone = strPhone.Substring(1, (strPhone.Length - 1))
            ElseIf Right(strPhone, 1) = "-" OrElse Right(strPhone, 1) = "/" OrElse Right(strPhone, 1) = "+" Then
                strPhone = strPhone.Substring(0, (strPhone.Length - 1))
            End If
        End If
        Return strPhone
    End Function

    Private Function ValidateLengthPhone(ByRef strPhone As String)
        Dim strPhone2 As String = String.Empty
        strPhone = strPhone.Trim

        If strPhone.Length > 0 Then
            Dim arrPhone As String() = strPhone.Split("/".ToCharArray, StringSplitOptions.RemoveEmptyEntries)
            For Each _phone As String In arrPhone
                _phone = _phone.Trim
                _phone = _phone.Replace("+", "").Replace("-", "").Replace(" ", "")
                strPhone2 = _phone
                Exit For
            Next
            If strPhone2.Trim.Length < 8 Then
                MessageBox.Show("Telephone minimal harus 8 digit")
                Return False
            End If
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub SendEmail(ByVal objPartShopOld As PartShop, ByVal objPartShopNew As PartShop)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        Dim emailTo As String = KTB.DNet.Lib.WebConfig.GetValue("EmailSPAdmin")
        Dim valueEmail As String = GenerateEmail(objPartShopOld, objPartShopNew)

        ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet] Parts - Update Part Shop", Mail.MailFormat.Html, valueEmail)
    End Sub

    Private Sub SendEmail(ByVal arlPartShop As ArrayList, Optional ByVal subject As String = "")
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetValue("EmailFrom")
        Dim emailTo As String = KTB.DNet.Lib.WebConfig.GetValue("EmailSPAdmin")

        If subject <> "" Then
            Dim valueEmail As String = GenerateEmail(arlPartShop, subject)
            ObjEmail.sendMail(emailTo, "", emailFrom, subject, Mail.MailFormat.Html, valueEmail)
        Else
            Dim valueEmail As String = GenerateEmail(arlPartShop)
            ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet] Parts - Request Part Shop Code ", Mail.MailFormat.Html, valueEmail)
        End If

    End Sub

    Private Function GenerateEmail(ByVal arlPartShop As ArrayList, Optional ByVal subject As String = "") As String

        Dim sb As StringBuilder = New StringBuilder("")
        sb.Append("<FONT face=Arial size=1>")
        sb.Append("<table width=700>")
        sb.Append("<tr>")
        If subject <> "" Then
            sb.Append("<td colspan=5 align=center><b>" & subject & "</b></td>")
        Else
            sb.Append("<td colspan=5 align=center><b>Request Part Shop Code</b></td>")
        End If

        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50>")
        sb.Append("Dengan hormat,&nbsp;")
        If subject <> "" Then
            sb.Append("<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Berikut daftar Part Shop untuk dinon-aktifkan :")
        Else
            sb.Append("<br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Berikut daftar Part Shop untuk dibuatkan kode :")
        End If
        sb.Append("</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=10></td>")
        sb.Append("</tr>")

        sb.Append("<tr>")
        sb.Append("<td height=50 colspan=6 align=center><hr><b>Daftar Part Shop</b></td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("<table border=1 width=700 cellpadding=0>")
        sb.Append("<tr>")
        sb.Append("<td width=30>No</td>")
        sb.Append("<td width=100>Dealer Yang Mendaftarkan</td>")
        sb.Append("<td width=295>Nama</td>")
        sb.Append("<td width=295>Alamat</td>")
        sb.Append("<td width=100>Telephone/Pc</td>")
        sb.Append("<td width=100>Fax/Pc</td>")
        sb.Append("<td width=125>Propinsi</td>")
        sb.Append("<td width=100>Kota</td>")
        sb.Append("</tr>")

        Dim counter As Integer = 0

        For Each itemDetail As PartShop In arlPartShop
            counter += 1
            sb.Append("<tr>")
            sb.Append("<td>" & counter.ToString & "</td>")
            If Not itemDetail.Dealer Is Nothing Then
                sb.Append("<td>" & itemDetail.Dealer.DealerCode & " / " & itemDetail.Dealer.SearchTerm2 & "</td>")
            Else
                sb.Append("<td>&nbsp;</td>")
            End If
            sb.Append("<td>" & itemDetail.Name & "</td>")
            sb.Append("<td>" & itemDetail.Address & "</td>")
            sb.Append("<td>" & itemDetail.Phone & "</td>")
            sb.Append("<td>" & itemDetail.Fax & "</td>")
            If Not IsNothing(itemDetail.CityPart) Then
                sb.Append("<td>" & itemDetail.CityPart.Province.ProvinceName & "</td>")
                sb.Append("<td>" & itemDetail.CityPart.CityName & "</td>")
            ElseIf Not IsNothing(itemDetail.City) Then
                sb.Append("<td>" & itemDetail.City.Province.ProvinceName & "</td>")
                sb.Append("<td>" & itemDetail.City.CityName & "</td>")
            Else
                sb.Append("<td></td>")
                sb.Append("<td></td>")
            End If
            sb.Append("<td>" & itemDetail.CityPart.Province.ProvinceName & "</td>")
            sb.Append("<td>" & itemDetail.CityPart.CityName & "</td>")
            sb.Append("</tr>")
        Next
        sb.Append("</table>")

        sb.Append("<table width=700>")

        sb.Append("<tr>")
        sb.Append("<td></td>")
        If subject <> "" Then
            sb.Append("<td colspan=4><font color='blue'>- Mohon di non-aktifkan untuk daftar part shop diatas</font></td>")
        Else
            sb.Append("<td colspan=4><font color='blue'>- Mohon untuk dibuatkan kode untuk daftar part shop diatas</font></td>")
        End If

        sb.Append("<td colspan=4>https://d-net.mitsubishi-motors.co.id/default_parts.aspx?screenid=9963</td>")
        sb.Append("</tr>")
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If Not objDealer Is Nothing Then
            sb.Append("<tr>")
            sb.Append("<td colspan=8>" & " Diajukan oleh dealer :" & objDealer.DealerCode & " - " & objDealer.DealerName & "</td>")
            sb.Append("</tr>")
        End If
        sb.Append("</table>")
        sb.Append("</FONT>")

        Return sb.ToString

    End Function

    Private Function GenerateEmail(ByVal objPartShopOld As PartShop, ByVal objPartShopNew As PartShop)
        Dim sb As StringBuilder = New StringBuilder("")
        sb.Append("<HTML>")
        sb.Append("<Body>")
        sb.Append("<FONT face=Arial size=1>")
        sb.Append("<table width=700>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 align=center><b>Update Data Part Shop</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50>")
        sb.Append("Dengan hormat,&nbsp;")
        sb.Append("<br><br>Berikut perubahan data Part Shop :<b> " & objPartShopOld.PartShopCode & " </b>")
        sb.Append("</td>")
        sb.Append("</tr>")

        sb.Append("</table>")
        sb.Append("<table border=1 width=700 cellpadding=0>")
        sb.Append("<tr>")
        sb.Append("<td height=50 colspan=3 align=center><b>Data</b></td>")
        sb.Append("<td height=50 align=center><b>Lama</b></td>")
        sb.Append("<td height=50 align=center><b>Baru</b></td>")
        sb.Append("</tr>")
        'sb.Append("<tr>")
        'sb.Append("<td height=50 colspan=3 align=left><b>Part Shop Code</b></td>")
        'sb.Append("<td height=50 align=left><b>" & objPartShopOld.PartShopCode & "</b></td>")
        'sb.Append("<td height=50 align=left><b>" & objPartShopNew.PartShopCode & "</b></td>")
        'sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td height=50 colspan=3 align=left><b>Nama</b></td>")
        sb.Append("<td height=50 align=left><b>" & objPartShopOld.Name & "</b></td>")
        sb.Append("<td height=50 align=left><b>" & objPartShopNew.Name & "</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td height=50 colspan=3 align=left><b>Alamat</b></td>")
        sb.Append("<td height=50 align=left><b>" & objPartShopOld.Address & "</b></td>")
        sb.Append("<td height=50 align=left><b>" & objPartShopNew.Address & "</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td height=50 colspan=3 align=left><b>Kota</b></td>")
        sb.Append("<td height=50 align=left><b>" & objPartShopOld.CityPart.CityName & "</b></td>")
        sb.Append("<td height=50 align=left><b>" & objPartShopNew.CityPart.CityName & "</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td height=50 colspan=3 align=left><b>Propinsi</b></td>")
        sb.Append("<td height=50 align=left><b>" & objPartShopOld.CityPart.Province.ProvinceName & "</b></td>")
        sb.Append("<td height=50 align=left><b>" & objPartShopNew.CityPart.Province.ProvinceName & "</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td height=50 colspan=3 align=left><b>Telp</b></td>")
        sb.Append("<td height=50 align=left><b>" & objPartShopOld.Phone & "</b></td>")
        sb.Append("<td height=50 align=left><b>" & objPartShopNew.Phone & "</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td height=50 colspan=3 align=left><b>Fax</b></td>")
        sb.Append("<td height=50 align=left><b>" & objPartShopOld.Fax & "</b></td>")
        sb.Append("<td height=50 align=left><b>" & objPartShopNew.Fax & "</b></td>")
        sb.Append("</tr>")
        sb.Append("</table>")

        sb.Append("<table width=700>")

        sb.Append("<tr>")
        sb.Append("<td><font color='blue'>Informasi perubahan data Part Shop, silahkan akses pada link berikut : <a href = https://d-net.mitsubishi-motors.co.id/default_parts.aspx?screenid=9963>https://d-net.mitsubishi-motors.co.id</a></font></td>")
        sb.Append("</tr>")
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If Not objDealer Is Nothing Then
            sb.Append("<tr>")
            sb.Append("<td>" & " Diajukan oleh dealer :" & objDealer.DealerCode & " - " & objDealer.DealerName & "</td>")
            sb.Append("</tr>")
        End If
        sb.Append("</table>")
        sb.Append("</FONT>")
        sb.Append("</Body>")
        sb.Append("</HTML>")


        Return sb.ToString

    End Function
#End Region


End Class
