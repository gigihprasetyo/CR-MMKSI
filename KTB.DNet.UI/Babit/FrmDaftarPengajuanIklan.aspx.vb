#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.BabitSalesComm
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Web.UI.WebControls
Imports KTB.DNet.UI.Helper
Imports System.IO
#End Region
Public Class FrmDaftarPengajuanIklan
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnRilis As System.Web.UI.WebControls.Button
    Protected WithEvents dgDaftarPengajuanIklan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNamaIklanKTB As System.Web.UI.WebControls.TextBox
    Protected WithEvents LinkButton1 As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private _sessHelper As New SessionHelper
    Private objDealer As Dealer

    Dim dirName As String = KTB.DNet.Lib.WebConfig.GetValue("AlertManagement")
    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
    Dim success As Boolean = False
    Dim objUser As UserInfo

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanIklanListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Pengajuan Iklan")
        End If
    End Sub

    Private Function blnCekIklanListDownloadPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanIklanListDownloadItem_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function blnCekIklanListListRealease() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanIklanListRealease_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objUser = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        objDealer = CType(_sessHelper.GetSession("Dealer"), Dealer)
        InitiateAuthorization()
        If Not IsPostBack() Then
            If (Not objDealer Is Nothing) Then
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    txtDealerCode.Text = objDealer.DealerCode
                    txtDealerCode.Enabled = False
                    lblSearchDealer.Visible = False
                    btnRilis.Enabled = False
                Else
                    txtDealerCode.Enabled = True
                    lblSearchDealer.Visible = True
                End If
            End If

            ViewState("CurrentSortColumn") = "NamaIklanKTB"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            BindDDL()
            'CreateCriteria()
            'BindResult(0)
            'ReadCriteria()
            If Not blnCekIklanListListRealease() Then
                btnRilis.Enabled = False
                dgDaftarPengajuanIklan.Columns(7).Visible = False 'untuk tick persetujuan KTB
            End If
        End If
        lblSearchDealer.Attributes("onclick") = "ShowDealerSelection();"
    End Sub

    'Private Sub BindData(ByVal indexPage As Integer)
    '    Dim totalRow As Integer = 0
    '    Dim arrList As New ArrayList
    '    Dim criterias As New CriteriaComposite(New Criteria(GetType(PengajuanDesignIklan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

    '    ' menyelesaikan bug 1311, perlu ditambahkan kriterianya     Deddy H
    '    If ddlStatus.SelectedValue <> "invalid" Then
    '        criterias.opAnd(New Criteria(GetType(PengajuanDesignIklan), "Status", MatchType.Exact, ddlStatus.SelectedValue))
    '    End If

    '    If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
    '        If (txtDealerCode.Text.Trim <> String.Empty) Then
    '            criterias.opAnd(New Criteria(GetType(PengajuanDesignIklan), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
    '        End If
    '    Else
    '        If (txtDealerCode.Text.Trim <> String.Empty) Then
    '            If New DataOwner().IsdealerExistInGroup(txtDealerCode.Text.Trim, objDealer) Then
    '                criterias.opAnd(New Criteria(GetType(PengajuanDesignIklan), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
    '            End If
    '        Else
    '            Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
    '            criterias.opAnd(New Criteria(GetType(PengajuanDesignIklan), "Dealer.DealerCode", MatchType.InSet, strCrit))
    '        End If
    '    End If

    '    'arrList = New PengajuanDesignIklanFacade(User).RetrieveActiveList(criterias, indexPage + 1, dgDaftarPengajuanIklan.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
    '    arrList = New PengajuanDesignIklanFacade(User).RetrieveActiveList(criterias, indexPage + 1, dgDaftarPengajuanIklan.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
    '    dgDaftarPengajuanIklan.DataSource = arrList
    '    dgDaftarPengajuanIklan.VirtualItemCount = totalRow

    '    success = imp.Start
    '    dgDaftarPengajuanIklan.DataBind()
    '    If success Then
    '        imp.StopImpersonate()
    '    End If

    '    _sessHelper.SetSession("DATA", arrList)
    'End Sub
    Private Sub BindDDL()
        Dim a As New EnumBabit
        For Each item As BabitItem In a.PengajuanDesignIklanStatusList
            Dim _temp As New ListItem(item.BabitValue, item.BabitCode)
            ddlStatus.Items.Add(_temp)
        Next
        Dim _statInvalid As New ListItem("Silakan Pilih", "invalid")
        _statInvalid.Selected = True
        ddlStatus.Items.Insert(0, _statInvalid)
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SaveCriteria()
        CreateCriteria()
        BindResult(0)
    End Sub
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("DealerCode", txtDealerCode.Text)
        crits.Add("NamaIklanKTB", txtNamaIklanKTB.Text)
        crits.Add("Status", ddlStatus.SelectedValue.ToString)
        _sessHelper.SetSession("PengajuanIklan", crits)
    End Sub
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(_sessHelper.GetSession("PengajuanIklan"), Hashtable)

        If Not IsNothing(crits) Then
            txtDealerCode.Text = CStr(crits.Item("DealerCode"))
            txtNamaIklanKTB.Text = CStr(crits.Item("NamaIklanKTB"))
            ddlStatus.SelectedValue = CStr(crits.Item("Status"))
        End If
    End Sub
    Private Sub CreateCriteria()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PengajuanDesignIklan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtNamaIklanKTB.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(PengajuanDesignIklan), "NamaIklanKTB", MatchType.Exact, txtNamaIklanKTB.Text.Trim))
        End If
        If ddlStatus.SelectedValue <> "invalid" Then
            criterias.opAnd(New Criteria(GetType(PengajuanDesignIklan), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PengajuanDesignIklan), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
            End If
        Else
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                If New DataOwner().IsdealerExistInGroup(txtDealerCode.Text.Trim, objDealer) Then
                    criterias.opAnd(New Criteria(GetType(PengajuanDesignIklan), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
                Else
                    'mode = 0
                End If
            Else
                Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
                criterias.opAnd(New Criteria(GetType(PengajuanDesignIklan), "Dealer.DealerCode", MatchType.InSet, strCrit))
            End If
        End If
        _sessHelper.SetSession("SortViewVC", criterias)
    End Sub
    Private Sub BindResult(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        'Dim mode As Integer = 1
        Dim arrList As ArrayList = New PengajuanDesignIklanFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("SortViewVC"), CriteriaComposite), indexPage + 1, dgDaftarPengajuanIklan.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If indexPage >= 0 Then
            dgDaftarPengajuanIklan.CurrentPageIndex = indexPage
            dgDaftarPengajuanIklan.DataSource = arrList
            dgDaftarPengajuanIklan.VirtualItemCount = totalRow
            success = imp.Start
            dgDaftarPengajuanIklan.DataBind()
            If success Then
                imp.StopImpersonate()
            End If
            If (IsNothing(arrList) Or arrList.Count <= 0) Then
                MessageBox.Show("Data tidak ditemukan")
            End If
            'Else
            'dgDaftarPengajuanIklan.DataSource = Nothing
            'dgDaftarPengajuanIklan.DataBind()
            'MessageBox.Show("Kode dealer tidak valid.")
        End If
        _sessHelper.SetSession("DATA", arrList)
    End Sub
    Private Sub dgDaftarPengajuanIklan_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDaftarPengajuanIklan.PageIndexChanged
        dgDaftarPengajuanIklan.CurrentPageIndex = e.NewPageIndex
        'If CType(_sessHelper.GetSession("BINDSTATUS"), String) = "CARI" Then
        BindResult(dgDaftarPengajuanIklan.CurrentPageIndex)
        'Else
        '    BindData(dgDaftarPengajuanIklan.CurrentPageIndex)
        'End If
    End Sub

    Private Sub ClearData()
        If objDealer.Title <> EnumDealerTittle.DealerTittle.DEALER Then
            txtDealerCode.Text = String.Empty
        End If
        txtNamaIklanKTB.Text = String.Empty
        ddlStatus.SelectedValue = "invalid"
        dgDaftarPengajuanIklan.DataSource = Nothing
        dgDaftarPengajuanIklan.DataBind()
    End Sub
    Private Sub dgDaftarPengajuanIklan_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDaftarPengajuanIklan.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgDaftarPengajuanIklan.SelectedIndex = -1
        dgDaftarPengajuanIklan.CurrentPageIndex = 0
        bindGridSorting(dgDaftarPengajuanIklan.CurrentPageIndex)
    End Sub

    Private Sub dgDaftarPengajuanIklan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDaftarPengajuanIklan.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgDaftarPengajuanIklan.CurrentPageIndex * dgDaftarPengajuanIklan.PageSize)

            Dim obj As PengajuanDesignIklan = CType(e.Item.DataItem, PengajuanDesignIklan)
            Dim lblCatatanKTB As LinkButton = CType(e.Item.FindControl("lblCatatanKTB"), LinkButton)
            lblCatatanKTB.Attributes("onclick") = "showPopUp('../General/../PopUp/PopUpCatatanKTB.aspx?ID= " & Integer.Parse(e.Item.Cells(0).Text) & "','',210,300,'');"

            Dim lbtnDownloadDealer As LinkButton = e.Item.FindControl("lbtnDownloadDealer")
            lbtnDownloadDealer.CommandArgument = KTB.DNet.Lib.WebConfig.GetValue("IklanBABIT") & "\" & obj.UploadeIklan

            Dim lbtnDownloadKTB As LinkButton = e.Item.FindControl("lbtnDownloadKTB")
            Dim objSampleIklan As SampleIklan = New SampleIklanFacade(User).Retrieve(obj.NamaIklanKTB)
            If objSampleIklan.ID = 0 Then
                lbtnDownloadKTB.Visible = False
            Else
                lbtnDownloadKTB.CommandArgument = objSampleIklan.UploadedIklan
            End If

            Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                lbtnEdit.Visible = False
            Else
                If obj.Status = CType(EnumBabit.PengajuanDesignIklanStatus.Baru, Integer) Then
                    lbtnEdit.Visible = True
                Else
                    lbtnEdit.Visible = False
                End If
            End If
            Dim objEnumBabit As EnumBabit
            Dim lblKeterangan As Label = CType(e.Item.FindControl("lblKeterangan"), Label)
            lblKeterangan.Text = obj.Description
            If obj.Status = objEnumBabit.PengajuanDesignIklanStatus.Rilis Then
                lblKeterangan.Text = " "
            End If

            Dim imgIklanDealer As Image = CType(e.Item.FindControl("imgIklanDealer"), Image)
            Dim cbSelect As CheckBox = e.Item.FindControl("cbSelect")
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                cbSelect.Enabled = False
            End If

            If obj.Status = EnumBabit.PengajuanDesignIklanStatus.Rilis Then
                cbSelect.Checked = True
            Else
                cbSelect.Checked = False
            End If

            'If e.Item.ItemIndex = 0 Then
            '    If Directory.Exists(KTB.DNet.Lib.WebConfig.GetValue("EULAPATH") & "DataTemp\" & objUser.UserName & objUser.Dealer.DealerCode & "\") Then
            '        Directory.Delete(KTB.DNet.Lib.WebConfig.GetValue("EULAPATH") & "DataTemp\" & objUser.UserName & objUser.Dealer.DealerCode & "\", True)
            '    End If
            '    Directory.CreateDirectory(KTB.DNet.Lib.WebConfig.GetValue("EULAPATH") & "DataTemp\" & objUser.UserName & objUser.Dealer.DealerCode & "\")
            'End If

            'Dim SourceFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("IklanBABIT") & "\" & obj.UploadeIklan '-- Destination file
            'Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("EULAPATH") & "DataTemp\" & objUser.UserName & objUser.Dealer.DealerCode & "\" & obj.ID.ToString & New FileInfo(obj.UploadeIklan).Name '-- Destination file

            'If File.Exists(SourceFile) Then
            '    File.Copy(SourceFile, DestFile, True)
            '    imgIklanDealer.ImageUrl = "../Datatemp/" & objUser.UserName & objUser.Dealer.DealerCode & "\" & obj.ID.ToString & New FileInfo(obj.UploadeIklan).Name
            'End If
            Dim lbtnDescription As LinkButton = e.Item.FindControl("lbtnDescription")
            Dim strFile As String = String.Format("id={0}&dummy={1}&type=1", obj.ID, DateTime.Now.ToString("ddMMyyyyhhmmss"))
            lbtnDescription.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpPicture.aspx?" & strFile & "', '', 500, 500, KTBNote);return false;")

            Dim lbtnIklanKTB As LinkButton = e.Item.FindControl("lbtnIklanKTB")
            Dim strFile2 As String = String.Format("id={0}&dummy={1}&type=2", obj.ID, DateTime.Now.ToString("ddMMyyyyhhmmss"))
            lbtnIklanKTB.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpPicture.aspx?" & strFile2 & "', '', 500, 500, KTBNote);return false;")

            'Dim strFile2 As String = String.Format("?UploadIklan={0}&dummy={1}", obj.NamaIklanKTB, DateTime.Now.ToString("ddMMyyyyhhmmss"))
            'lbtnIklanKTB.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpPicture.aspx" & strFile & "', '', 500, 500, KTBNote);return false;")

            imgIklanDealer.ImageUrl = String.Format("~/Event/EventImageHandler.aspx?file={0}", lbtnDownloadDealer.CommandArgument)
            imgIklanDealer.Visible = True

            ' add security
            If Not blnCekIklanListDownloadPrivilege() Then
                lbtnDownloadKTB.Visible = False
                lbtnDownloadDealer.Visible = False
                lblCatatanKTB.Visible = False
            End If
        End If



    End Sub
    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dgDaftarPengajuanIklan.DataSource = New PengajuanDesignIklanFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("sortViewVC"), CriteriaComposite), indexPage + 1, dgDaftarPengajuanIklan.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgDaftarPengajuanIklan.VirtualItemCount = totalRow
            success = imp.Start
            dgDaftarPengajuanIklan.DataBind()
            If success Then
                imp.StopImpersonate()
            End If
        End If

    End Sub
    Private Sub dgDaftarPengajuanIklan_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDaftarPengajuanIklan.ItemCommand
        If e.CommandName = "DownloadKTB" Or e.CommandName = "DownloadDealer" Then
            Response.Redirect("../Download.aspx?file=" & e.CommandArgument)
        ElseIf e.CommandName = "edit" Then
            Response.Redirect("../Babit/FrmPengajuanDesignIklan.aspx?id=" & e.CommandArgument)
        End If
    End Sub

    Private Sub btnRilis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRilis.Click
        Dim bcheck As Boolean = False
        Dim success As Boolean = False

        dgDaftarPengajuanIklan.DataSource = CType(_sessHelper.GetSession("DATA"), ArrayList)
        For Each dtgItem As DataGridItem In dgDaftarPengajuanIklan.Items
            If CType(dtgItem.Cells(0).FindControl("cbSelect"), CheckBox).Checked Then
                bcheck = True
                Exit For
            End If
        Next
        Try
            If bcheck Then
                Dim CheckedItemColl As ArrayList = New ArrayList
                CheckedItemColl = GetCheckedPMItem()

                Dim objColl As ArrayList = New ArrayList
                Dim strErrMsg As String = String.Empty
                If CheckedItemColl.Count > 0 Then
                    For Each objIklan As PengajuanDesignIklan In CheckedItemColl
                        objIklan.Status = EnumBabit.BabitAllocationStatus.Rilis

                        objColl.Add(objIklan)
                    Next
                    Dim objFacade As New PengajuanDesignIklanFacade(User)
                    objFacade.UpdateTransactionCollection(objColl)
                    MessageBox.Show("Update Rilis Sukses")
                End If
            Else
                MessageBox.Show("Record Design belum dipilih !")
            End If
            BindResult(dgDaftarPengajuanIklan.CurrentPageIndex)
            'Dim strScript As String
            'strScript = "<script>document.all.txtChassisMaster.focus();</script>"
            'Page.RegisterStartupScript("", strScript)
        Catch ex As Exception
            MessageBox.Show("Update Rilis gagal !")
        End Try
    End Sub
    Private Function GetCheckedPMItem() As ArrayList
        dgDaftarPengajuanIklan.DataSource = CType(_sessHelper.GetSession("DATA"), ArrayList)
        Dim arlCheckedItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgDaftarPengajuanIklan.Items
            nIndeks = dtgItem.ItemIndex
            Dim obj As PengajuanDesignIklan = CType(CType(dgDaftarPengajuanIklan.DataSource, ArrayList)(nIndeks), PengajuanDesignIklan)
            If CType(dtgItem.Cells(2).FindControl("cbSelect"), CheckBox).Checked Then
                If Not IsNothing(obj) Then
                    If obj.Status = CType(EnumBabit.BabitAllocationStatus.Baru, String) Then
                        obj.Status = CType(EnumBabit.BabitAllocationStatus.Rilis, String)
                        arlCheckedItem.Add(obj)
                    ElseIf obj.Status = EnumBabit.BabitAllocationStatus.Rilis Then
                        MessageBox.Show("Nama Iklan MMKSI " + obj.NamaIklanKTB + " statusnya sudah rilis")
                        Exit For
                    End If
                End If
            End If
        Next
        Return arlCheckedItem
    End Function

    Private Sub dgDaftarPengajuanIklan_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgDaftarPengajuanIklan.DataBinding
    End Sub

    Private Sub LinkButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("../Download.aspx?file=" & "\\172.17.104.203\ZDNet\Repository\BSI-Net\BABIT\Iklan\021107094046\Winter.jpg")
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub
End Class
