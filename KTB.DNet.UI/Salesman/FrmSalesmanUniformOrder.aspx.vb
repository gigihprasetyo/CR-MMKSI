#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.Utility.CommonFunction

Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman


#End Region


Public Class FrmSalesmanUniformOrder
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrderNo As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlUnifDist As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlJobPositionDesc As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblHargaNormal As System.Web.UI.WebControls.Label
    Protected WithEvents lblSeparator01 As System.Web.UI.WebControls.Label
    Protected WithEvents lblHargaNormalVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblHargaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblHargaDealerVal As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblGrandTotal As System.Web.UI.WebControls.Label
    Protected WithEvents dtgUniformOrder As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpanDealer As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpanKTB As System.Web.UI.WebControls.Button
    Protected WithEvents btnValidate As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnRecap As System.Web.UI.HtmlControls.HtmlInputButton

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
    Private SessHelper As New SessionHelper
    Private DealerId As Integer
    Private blnValidate As Boolean
    Private strSalesId As String
    Private _createPriv As Boolean = False
    Private _submitPriv As Boolean = False
    Private _recapKtbPriv As Boolean = False
#End Region

#Region "EventHandlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        _createPriv = CheckCreatePrivilege()
        _submitPriv = CheckSubmitPrivilege()
        _recapKtbPriv = checkRecapPrivilege()
        btnRecap.Visible = _recapKtbPriv
        If Not IsNothing(SessHelper.GetSession("LOGINUSERINFO")) Then
            If CType(SessHelper.GetSession("LOGINUSERINFO"), UserInfo).Dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                CheckPrivilege()
                btnValidate.Visible = _submitPriv
                'ElseIf CType(SessHelper.GetSession("LOGINUSERINFO"), UserInfo).Dealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                'recapKtbPriv = CheckKTBPrivilege() '    iif(CheckKTBPrivilege,btnRilis.visible
            End If
        End If
        If Not IsPostBack Then
            dtgUniformOrder.DataSource = New ArrayList
            dtgUniformOrder.DataBind()
            blnValidate = False
            strSalesId = ""
            ViewState("currSortColumn") = "SalesmanHeader.SalesmanCode"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindDDLUniformDistribution()
            CommonFunction.BindJobPosition(ddlJobPositionDesc, User, True, False)
            Dim objUserInfo As UserInfo = CType(SessHelper.GetSession("LOGINUSERINFO"), UserInfo)

            Dim isKTB As Boolean = (objUserInfo.Dealer.Title.Trim = "1")

            If isKTB Then
                lblTitle.Text = "Pengesahan Seragam MMKSI"
                btnSimpanDealer.Visible = False
                '----create privilege
                btnSimpanKTB.Visible = _createPriv
            Else
                ' Dealer
                lblTitle.Text = "Pemesanan Seragam"
                btnSimpanKTB.Visible = False
                ' Modified by Ikhsan, 20081124
                ' Requested by Rina as Part of CR
                ' Adding checkbox to save only selected items
                ' Start -----------------------------------
                If Request.QueryString("id") = String.Empty Then
                    If Not IsActiveTransControl() Then
                        Response.Redirect("../frmAccessDenied.aspx?modulName=Pesan Seragam (Transaction Control)")
                    End If
                    dtgUniformOrder.Columns(8).Visible = True
                Else
                    If Request.QueryString("mode").ToUpper = "EDIT" Then
                        dtgUniformOrder.Columns(8).Visible = True
                    Else
                        dtgUniformOrder.Columns(8).Visible = True
                    End If
                End If
                ' End  ------------------------------------
                '----create privilege
                btnSimpanDealer.Visible = _createPriv
            End If

            If Request.QueryString("id") <> String.Empty Then
                ' hanya view data
                Dim objHeader As SalesmanUniformOrderHeader = New SalesmanUniformOrderHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
                btnSearch.Visible = False
                lblOrderNo.Text = objHeader.OrderNumber
                ddlUnifDist.SelectedValue = objHeader.SalesmanUnifDistribution.ID.ToString
                ddlUnifDist.Enabled = False
                ddlUnifDist_SelectedIndexChanged(sender, e)
                ddlJobPositionDesc.Enabled = False
                If Request.QueryString("mode").ToUpper = "EDIT" Then
                    'btnValidate.Visible = False
                    'btnSimpanDealer.Visible = False
                    'btnSimpanKTB.Visible = False
                    'dtgUniformOrder.Columns(7).Visible = False ' template aksi diinvisible
                Else
                    btnValidate.Visible = False
                    btnSimpanDealer.Visible = False
                    btnSimpanKTB.Visible = False
                    dtgUniformOrder.Columns(7).Visible = False ' template aksi diinvisible
                End If

                bindToGrid(True)
                SessHelper.SetSession("arlDelete", New ArrayList)
            End If

        End If
        VisibleControl()
    End Sub

    Private Sub VisibleControl()
        ' kalau user dealer, btn kembali tdk ada
        Dim objDealer As Dealer = SessHelper.GetSession("DEALER")
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            If Request.QueryString("id") <> String.Empty Then
                btnBack.Visible = True
                btnBatal.Visible = False
            Else
                btnBack.Visible = False
                btnBatal.Visible = True
            End If
        Else
            'KTB
            btnBack.Visible = True
            btnBatal.Visible = False
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If ddlUnifDist.SelectedValue <> "-1" Then
            lblOrderNo.Text = GetOrderNo()

            bindToGrid(True)
            'If dtgUniformOrder.Items.Count > 0 Then
            '    blnValidate = False
            '    btnSimpanDealer.Enabled = True
            '    btnValidate.Enabled = True
            '    btnSimpanDealer.Visible = _createPriv
            '    btnValidate.Visible = _submitPriv
            'End If
        Else
            MessageBox.Show("Pilih Kode Pesanan Dulu")
        End If
    End Sub

    Private Sub dtgUniformOrder_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUniformOrder.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgUniformOrder.CurrentPageIndex * dtgUniformOrder.PageSize)

            Dim obj As SalesmanUniformOrderDetail = CType(SessHelper.GetSession("arlOrder")(e.Item.ItemIndex), SalesmanUniformOrderDetail)
            Dim objSalesmanUniformAssigned As SalesmanUniformAssigned = CType(SessHelper.GetSession("arlUniformAssigned")(e.Item.ItemIndex), SalesmanUniformAssigned)

            If e.Item.ItemType = ListItemType.EditItem Then
                Dim ddlUniformSize As DropDownList = e.Item.FindControl("ddlUniformSize")
                'ddlUniformSize.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))
                For Each _item As EnumUnifSize In New EnumUniformSize().RetrieveUniformSize
                    ddlUniformSize.Items.Add(New ListItem(_item.NameType, _item.ValType.ToString))
                Next
                'ddlUniformSize.SelectedIndex = -1
                ddlUniformSize.SelectedIndex = 0

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.ID", MatchType.Exact, obj.SalesmanHeader.ID))

                If Not IsNothing(objSalesmanUniformAssigned.UniformSize) <> 0 Then
                    ddlUniformSize.SelectedValue = objSalesmanUniformAssigned.UniformSize.ToString
                End If

            ElseIf e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
                Dim lblUkuran As Label = e.Item.FindControl("lblUkuran")
                ' Modified by Ikhsan, 20081124
                ' To fill the empty data with Silahkan Pilih
                ' If Not IsNothing(objSalesmanUniformAssigned.UniformSize) Then
                If Request.QueryString("ID") <> String.Empty Then
                    lblUkuran.Text = New EnumUniformSize().GetName(CInt(obj.UniformSize))
                Else
                    If objSalesmanUniformAssigned.UniformSize > 0 Then
                        lblUkuran.Text = New EnumUniformSize().GetName(CInt(objSalesmanUniformAssigned.UniformSize))
                    Else
                        lblUkuran.Text = "Silahkan Pilih"
                    End If
                End If

                'If objSalesmanUniformAssigned.UniformSize > 0 Then
                '    lblUkuran.Text = New EnumUniformSize().GetName(CInt(objSalesmanUniformAssigned.UniformSize))
                'Else
                '    lblUkuran.Text = "Silahkan Pilih"
                'End If

                '----create Privilege
                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                lbtnEdit.Visible = _createPriv

            End If

        End If
    End Sub

    Private Sub dtgUniformOrder_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgUniformOrder.ItemCommand
        If e.Item.ItemIndex < 0 Then
            Exit Sub
        End If

        Dim currentObjOrder As SalesmanUniformOrderDetail = CType((CType(SessHelper.GetSession("arlOrder"), ArrayList)(e.Item.ItemIndex)), SalesmanUniformOrderDetail)
        Dim currSalesmanUniformAssigned As SalesmanUniformAssigned = CType((CType(SessHelper.GetSession("arlUniformAssigned"), ArrayList)(e.Item.ItemIndex)), SalesmanUniformAssigned)


        Select Case e.CommandName
            Case "add"
                'Check ada record belum diisi
                For Each itemOrder As SalesmanUniformOrderDetail In CType(SessHelper.GetSession("arlOrder"), ArrayList)
                    If currentObjOrder.SalesmanHeader.ID = itemOrder.SalesmanHeader.ID And itemOrder.SalesmanUniform Is Nothing Then
                        MessageBox.Show("Lengkapi data seragam salesman ini sebelum menambah record")
                        Exit Sub
                    End If

                Next

                'Check jumlah record untuk salesman ybs
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.ID", MatchType.Exact, currentObjOrder.SalesmanHeader.ID))
                Dim arlSalesmanUniformAssigned As ArrayList = New SalesmanUniformAssignedFacade(User).Retrieve(criterias)

                Dim jumlahUniform As Integer = arlSalesmanUniformAssigned.Count
                Dim jumlahExistingRecord As Integer = 0
                For Each item As DataGridItem In dtgUniformOrder.Items
                    Dim lbtnAdd As LinkButton = item.FindControl("lbtnAdd")
                    If lbtnAdd.CommandArgument = e.CommandArgument Then
                        jumlahExistingRecord += 1
                    End If
                Next

                If jumlahUniform <= jumlahExistingRecord Then
                    MessageBox.Show("Jumlah seragam untuk salesman ini hanya ada " & jumlahUniform.ToString)
                    Exit Sub
                End If

                Dim objOrder As SalesmanUniformOrderDetail = New SalesmanUniformOrderDetail

                Dim lbtnAddCurrentRow As LinkButton = e.Item.FindControl("lbtnAdd")
                objOrder.SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(CInt(lbtnAddCurrentRow.CommandArgument))

                Dim arlToUpdate As ArrayList = SessHelper.GetSession("arlOrder")
                arlToUpdate.Add(objOrder)
                SessHelper.SetSession("arlOrder", arlToUpdate)
                bindToGrid(False)

            Case "delete"
                'Check jumlah salesman ybs
                Dim jumlahExistingRecord As Integer = 0
                For Each itemOrder As SalesmanUniformOrderDetail In CType(SessHelper.GetSession("arlOrder"), ArrayList)
                    If currentObjOrder.SalesmanHeader.ID = itemOrder.SalesmanHeader.ID Then
                        jumlahExistingRecord += 1
                    End If
                Next

                Dim arlToUpdate As ArrayList = SessHelper.GetSession("arlOrder")

                If jumlahExistingRecord <= 1 Then
                    currentObjOrder.SalesmanUniform = Nothing
                    currentObjOrder.UniformSize = 0
                    currentObjOrder.Qty = 0
                    arltoupdate(e.Item.ItemIndex) = currentObjOrder
                Else
                    Dim objdelete As SalesmanUniformOrderDetail = arlToUpdate(e.Item.ItemIndex)
                    If objdelete.ID > 0 Then
                        Dim arlDelete As ArrayList = SessHelper.GetSession("arlDelete")
                        arlDelete.Add(arlToUpdate(e.Item.ItemIndex))
                        SessHelper.SetSession("arlDelete", arlDelete)
                    End If
                    arlToUpdate.Removeat(e.Item.ItemIndex)
                End If
                SessHelper.SetSession("arlOrder", arlToUpdate)
                bindToGrid(False)


            Case "edit"
                SaveCheckedStatus()
                dtgUniformOrder.EditItemIndex = e.Item.ItemIndex
                bindToGrid(False)
                SetCheckedStatus()

            Case "cancel"
                SaveCheckedStatus()
                dtgUniformOrder.EditItemIndex = -1
                bindToGrid(False)
                SetCheckedStatus()

            Case "save"
                SaveCheckedStatus()
                Dim ddlUniformSize As DropDownList = e.Item.FindControl("ddlUniformSize")
                'If ddlUniformSize.SelectedValue = "-1" Then
                If ddlUniformSize.SelectedValue = "0" Then
                    MessageBox.Show("Silakan memilih Kode Uniform yang tersedia")
                    Return
                End If
                ' mengambil data uniform dari distribution code
                Dim obj As SalesmanUniform
                Dim arrTmp As ArrayList
                Dim strSalesmanUniformId As String

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.ID", MatchType.Exact, CType(ddlUnifDist.SelectedValue, Integer)))
                arrTmp = New SalesmanUniformFacade(User).Retrieve(criterias)
                If arrTmp.Count > 0 Then
                    For Each item As SalesmanUniform In arrTmp
                        If strSalesmanUniformId = "" Then
                            strSalesmanUniformId = CType(item.ID, Integer)
                            Exit For
                        End If
                    Next
                    currentObjOrder.SalesmanUniform = New SalesmanUniformFacade(User).Retrieve(CInt(strSalesmanUniformId))
                    currentObjOrder.UniformSize = CByte(ddlUniformSize.SelectedValue)
                    currentObjOrder.Qty = 1 'Val(txtQty.Text) ' slalu define, field digunakan untuk perhitungan harga -> qty x dealer price
                Else
                    currentObjOrder.SalesmanUniform = Nothing
                    currentObjOrder.UniformSize = 0
                    currentObjOrder.Qty = 0
                End If

                currSalesmanUniformAssigned.UniformSize = CByte(ddlUniformSize.SelectedValue)

                Dim arlToUpdate As ArrayList = SessHelper.GetSession("arlOrder")
                Dim arlUniformAssignedToUpdate As ArrayList = SessHelper.GetSession("arlUniformAssigned")

                arltoupdate(e.Item.ItemIndex) = currentObjOrder
                arlUniformAssignedToUpdate(e.Item.ItemIndex) = currSalesmanUniformAssigned

                SessHelper.SetSession("arlOrder", arlToUpdate)
                SessHelper.SetSession("arlUniformAssigned", arlUniformAssignedToUpdate)

                dtgUniformOrder.EditItemIndex = -1
                bindToGrid(False)
                SetCheckedStatus()
        End Select
    End Sub

    Private Sub SaveCheckedStatus()
        Dim str As String = ""

        For Each di As DataGridItem In dtgUniformOrder.Items
            Dim ChkBoxItem As CheckBox = di.FindControl("ChkBoxItem")
            str &= IIf(ChkBoxItem.Checked = True, "1", "0") & ";"
        Next
        SessHelper.SetSession("CheckedStatus", str)

    End Sub

    Private Sub SetCheckedStatus()
        Dim str() As String = CType(SessHelper.GetSession("CheckedStatus"), String).Split(";")
        For Each di As DataGridItem In dtgUniformOrder.Items
            Dim ChkBoxItem As CheckBox = di.FindControl("ChkBoxItem")
            ChkBoxItem.Checked = IIf(str(di.ItemIndex) = "1", True, False)
        Next
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpanDealer.Click
        If dtgUniformOrder.Items.Count < 1 Then
            MessageBox.Show("Data Pesanan tidak ada, silakan cari yang ada terlebih dahulu")
            Exit Sub
        End If

        'Start  :Check data validation
        If IsNothing(Request.QueryString("id")) Then
            For Each di As DataGridItem In dtgUniformOrder.Items
                Dim objSUA As SalesmanUniformAssigned = CType(CType(SessHelper.GetSession("arlUniformAssigned"), ArrayList)(di.ItemIndex), SalesmanUniformAssigned)
                Dim ChkBoxItem As CheckBox = di.FindControl("ChkBoxItem")
                If (IsNothing(objSUA.UniformSize) OrElse objSUA.UniformSize <= 0) And ChkBoxItem.Checked = True Then
                    MessageBox.Show("Data tidak bisa disimpan, ukuran belum dipilih")
                    Exit Sub
                ElseIf (Not IsNothing(objSUA.UniformSize) AndAlso objSUA.UniformSize > 0) And ChkBoxItem.Checked = False Then
                    MessageBox.Show("Data tidak bisa disimpan, belum ada data yang dipilih")
                    Exit Sub
                End If
            Next
        End If
        'End    :Check data validation

        Dim nresult As Integer
        ' mengupdate data yang ada didatagrid, update data SalesmanUniformAssigned
        If dtgUniformOrder.Items.Count > 0 Then
            For Each item As SalesmanUniformAssigned In CType(SessHelper.GetSession("arlUniformAssigned"), ArrayList)
                ' Modified by Ikhsan, 20081124
                ' Requested by Rina as Part of CR
                ' Only selected item will be updated
                ' Start ------------------------------------------------------------------
                Dim Counter As Integer = 0
                Dim chk As CheckBox = dtgUniformOrder.Items(Counter).FindControl("ChkBoxItem")

                If chk.Checked Then
                    If item.UniformSize.ToString <> "" Then
                        Dim objSalesmanUniformAssigned As SalesmanUniformAssigned = New SalesmanUniformAssignedFacade(User).Retrieve(item.ID)
                        objSalesmanUniformAssigned.UniformSize = item.UniformSize

                        nresult = New SalesmanUniformAssignedFacade(User).Update(item)
                    End If
                End If
                Counter += 1
                ' End --------------------------------------------------------------------
            Next
            If nresult = -1 Then
                MessageBox.Show("Data Uniform Assigned gagal diupdate")
            Else
                MessageBox.Show("Data Uniform Assigned berhasil diupdate")
            End If
        End If
    End Sub

    Private Sub dtgUniformOrder_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgUniformOrder.SortCommand

        If e.SortExpression = ViewState("currSortColumn") Then
            If ViewState("currSortDirection") = Sort.SortDirection.ASC Then
                ViewState("currSortDirection") = Sort.SortDirection.DESC
            Else
                ViewState("currSortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState("currSortColumn") = e.SortExpression
        dtgUniformOrder.SelectedIndex = -1
        dtgUniformOrder.CurrentPageIndex = 0
        bindToGrid(False)

    End Sub

    Private Sub btnSimpanKTB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpanKTB.Click
        Dim counter = 0
        For Each objOrder As SalesmanUniformOrderDetail In CType(SessHelper.GetSession("arlOrder"), ArrayList)
            If objOrder.ID > 0 Then
                Dim chk As CheckBox = dtgUniformOrder.Items(counter).FindControl("chk")
                If chk.Checked Then
                    objOrder.IsValidate = 1
                Else
                    objOrder.IsValidate = 0
                End If
            End If
            Dim result As Integer = New SalesmanUniformOrderDetailFacade(User).Update(objOrder)
            counter += 1
        Next
        MessageBox.Show(SR.SaveSuccess)
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        SessHelper.SetSession("ModeBack", True)
        Response.Redirect("FrmSalesmanUniformList.aspx?")
    End Sub

    Private Sub ddlUnifDist_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlUnifDist.SelectedIndexChanged
        ' mengambil data harga
        If ddlUnifDist.SelectedValue <> "-1" Then
            Dim obj As SalesmanUniform = New SalesmanUniformFacade(User).Retrieve(ddlUnifDist.SelectedValue)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.ID", MatchType.Exact, CType(ddlUnifDist.SelectedValue, Integer)))
            Dim arrSalesmanUniform As ArrayList
            arrSalesmanUniform = New SalesmanUniformFacade(User).Retrieve(criterias)
            If arrSalesmanUniform.Count > 0 Then
                For Each item As SalesmanUniform In arrSalesmanUniform
                    lblHargaNormalVal.Text = item.NormalPrice.ToString("#,##0")
                    lblHargaDealerVal.Text = item.DealerPrice.ToString("#,##0")
                    Exit For
                Next
            Else
                lblHargaNormalVal.Text = ""
                lblHargaDealerVal.Text = ""
                lblGrandTotal.Text = "0"
            End If
        Else
            lblHargaNormalVal.Text = ""
            lblHargaDealerVal.Text = ""
            lblGrandTotal.Text = "0"
        End If
    End Sub

    Private Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click

        If dtgUniformOrder.Items.Count < 1 Then
            MessageBox.Show("Data Pesanan tidak ada, silakan cari yang ada terlebih dahulu")
            Exit Sub
        End If

        'Start  :Check data validation
        If IsNothing(Request.QueryString("id")) Then
            For Each di As DataGridItem In dtgUniformOrder.Items
                Dim objSUA As SalesmanUniformAssigned = CType(CType(SessHelper.GetSession("arlUniformAssigned"), ArrayList)(di.ItemIndex), SalesmanUniformAssigned)
                Dim ChkBoxItem As CheckBox = di.FindControl("ChkBoxItem")
                If (IsNothing(objSUA.UniformSize) OrElse objSUA.UniformSize <= 0) And ChkBoxItem.Checked = True Then
                    MessageBox.Show("Data tidak bisa disimpan, ukuran belum dipilih")
                    Exit Sub
                ElseIf (Not IsNothing(objSUA.UniformSize) AndAlso objSUA.UniformSize > 0) And ChkBoxItem.Checked = False Then
                    MessageBox.Show("Data tidak bisa disimpan, belum ada data yang dipilih")
                    Exit Sub
                End If
            Next
        End If
        'End    :Check data validation

        'Melakukan penyimpanan ke SalesmanUniformOrderHeader & SalesmanUniformOrderDetail, generate order no
        Dim valid As Boolean = False
        'Start OldScript
        'For Each objSalesmanUniformAssigned As SalesmanUniformAssigned In CType(SessHelper.GetSession("arlUniformAssigned"), ArrayList)
        '    If (Not IsNothing(objSalesmanUniformAssigned.UniformSize) AndAlso objsalesmanuniformassigned.UniformSize > 0) And 1 = 1 Then
        '        valid = True
        '        Exit For
        '    End If
        'Next
        'End OldScript
        For Each di As DataGridItem In dtgUniformOrder.Items
            Dim objSUA As SalesmanUniformAssigned = CType(CType(SessHelper.GetSession("arlUniformAssigned"), ArrayList)(di.ItemIndex), SalesmanUniformAssigned)
            Dim ChkBoxItem As CheckBox = di.FindControl("ChkBoxItem")
            If (Not IsNothing(objSUA.UniformSize) AndAlso objSUA.UniformSize > 0) And ChkBoxItem.Checked = True Then
                valid = True
            End If
        Next

        If valid Then
            Dim objHeader As SalesmanUniformOrderHeader
            If Request.QueryString("id") = String.Empty Then
                objHeader = New SalesmanUniformOrderHeader
                Dim objuser As UserInfo = CType(SessHelper.GetSession("LOGINUSERINFO"), UserInfo)
                objHeader.Dealer = objuser.Dealer
                objHeader.OrderDate = Date.Now
                objHeader.SalesmanUnifDistribution = New SalesmanUnifDistributionFacade(User).Retrieve(CInt(ddlUnifDist.SelectedValue))
                objHeader.OrderNumber = "request"
                objHeader.InvoiceNo = ""
            Else
                objHeader = New SalesmanUniformOrderHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
            End If

            Dim arlSalesmanUniformAssignedToUpdate As ArrayList = New ArrayList
            Dim arlToInsert As ArrayList = New ArrayList
            strSalesId = ""
            Dim idx As Integer = 0
            For Each objSalesmanUniformAssigned As SalesmanUniformAssigned In CType(SessHelper.GetSession("arlUniformAssigned"), ArrayList)
                Dim ChkBoxItem As CheckBox = dtgUniformOrder.Items(idx).FindControl("ChkBoxItem")

                If (Not IsNothing(objSalesmanUniformAssigned.UniformSize) AndAlso objSalesmanUniformAssigned.UniformSize > 0) And ChkBoxItem.Checked = True Then
                    Dim objTemp As SalesmanUniformAssigned = New SalesmanUniformAssignedFacade(User).Retrieve(objSalesmanUniformAssigned.ID)

                    If Not IsNothing(objTemp) Then
                        'If strSalesId = "" Then
                        '    strSalesId = objTemp.SalesmanHeader.ID
                        'Else
                        '    strSalesId = strSalesId & ";" & objTemp.SalesmanHeader.ID
                        'End If
                        objtemp.UniformSize = objSalesmanUniformAssigned.UniformSize
                        objTemp.IsValidate = EnumSalesmanUniformAssigned.IsReleased.Released
                    End If
                    arlSalesmanUniformAssignedToUpdate.Add(objTemp)
                    Dim objOrder As SalesmanUniformOrderDetail = CType(SessHelper.GetSession("arlOrder"), ArrayList)(idx)
                    objOrder.SalesmanUniformOrderHeader = objHeader
                    objOrder.Qty = 1 ' untuk perhitungan harga => qty * dealer price
                    arlToInsert.Add(objOrder)
                Else
                    Dim objTemp As SalesmanUniformAssigned = New SalesmanUniformAssignedFacade(User).Retrieve(objSalesmanUniformAssigned.ID)
                    If Not IsNothing(objTemp) Then
                        'If strSalesId = "" Then
                        '    strSalesId = objTemp.SalesmanHeader.ID
                        'Else
                        '    strSalesId = strSalesId & ";" & objTemp.SalesmanHeader.ID
                        'End If
                        objTemp.IsValidate = EnumSalesmanUniformAssigned.IsReleased.Not_Released
                        objTemp.UniformSize = 0
                    End If
                    arlSalesmanUniformAssignedToUpdate.Add(objTemp)
                End If
                idx += 1
            Next

            Dim result As Integer

            Dim arlToDelete As ArrayList
            If Request.QueryString("id") = String.Empty Then
                arlToDelete = New ArrayList
            Else
                arlToDelete = SessHelper.GetSession("arlDelete")
            End If
            'delete * OrderDetail First
            If Not IsNothing(Request.QueryString("id")) AndAlso Request.QueryString("mode").ToUpper = "EDIT" Then
                Dim objSUODFac As SalesmanUniformOrderDetailFacade = New SalesmanUniformOrderDetailFacade(User)
                Dim crtSUOD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim arlSUOD As ArrayList

                crtSUOD.opAnd(New Criteria(GetType(SalesmanUniformOrderDetail), "SalesmanUniformOrderHeader.ID", MatchType.Exact, CType(Request.QueryString("id"), Integer)))
                arlSUOD = objSUODFac.Retrieve(crtSUOD)
                For Each oSUOD As SalesmanUniformOrderDetail In arlSUOD
                    objSUODFac.DeleteFromDB(oSUOD)
                Next
            End If
            result = New SalesmanUniformOrderHeaderFacade(User).InsertTransaction(objHeader, arlToInsert, arlToDelete, arlSalesmanUniformAssignedToUpdate)


            If result > 0 Then
                Dim NoOrder As String = New SalesmanUniformOrderHeaderFacade(User).Retrieve(result).OrderNumber
                lblOrderNo.Text = NoOrder
                blnValidate = True
                btnValidate.Enabled = False
                btnSimpanDealer.Enabled = False
                'bindToGrid(True)
                Page_Load(sender, e)
                MessageBox.Show("Data Pesanan No " & NoOrder & " Berhasil Disimpan")
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        Else
            MessageBox.Show("Isi Data Dulu...!")
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

#End Region

#Region "Custom Method"

    Private Sub BindDDLUniformDistribution()
        'Dim arlUnifDis As ArrayList
        ''27-Sep-2007    Deddy H     hanya menampilkan unifDistribution yang aktif saja
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifDistribution), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(SalesmanUnifDistribution), "IsActive", MatchType.Exact, CType(enumStatusSalesmanUnifDistribution.StatusSalesmanUnifDistribution.Aktif, Integer)))
        ''arlUnifDis = New SalesmanUnifDistributionFacade(User).RetrieveList
        'arlUnifDis = New SalesmanUnifDistributionFacade(User).Retrieve(criterias)

        'ddlUnifDist.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
        'For Each itemUnifDis As SalesmanUnifDistribution In arlUnifDis
        '    ddlUnifDist.Items.Add(New ListItem(itemUnifDis.SalesmanUnifDistributionCode, itemUnifDis.ID.ToString))
        'Next
        CommonFunction.BindSalesmanUnifDistributionCode(ddlUnifDist, Me.User, True)
    End Sub

    Private Sub bindToGrid(ByVal isfromDb As Boolean)
        Dim objUserInfo As UserInfo = CType(SessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim arlUniformAssigned As ArrayList = New ArrayList

        If blnValidate Then
            ' kalau sdh divalidate, data tdk bs dimodifikasi
            'dtgUniformOrder.Columns(7).Visible = False ' template aksi diinvisible
        Else
            If Not IsNothing(Request.QueryString("id")) Then
                If (Not IsNothing(Request.QueryString("mode")) AndAlso Request.QueryString("mode").ToUpper = "EDIT") Then
                    dtgUniformOrder.Columns(7).Visible = True
                Else
                    dtgUniformOrder.Columns(7).Visible = False

                End If
            Else
                dtgUniformOrder.Columns(7).Visible = True
            End If

        End If

        If isfromDb Then
            Dim arlUniformOrder As ArrayList = New ArrayList
            Dim strUniformID As String = ""
            Dim isKTB As Boolean = (objUserInfo.Dealer.Title.Trim = "1")

            If isKTB Then
                Dim objHeader As SalesmanUniformOrderHeader = New SalesmanUniformOrderHeaderFacade(User).Retrieve(CInt(Request.QueryString("id")))
                DealerId = objHeader.Dealer.ID
            Else
                DealerId = objUserInfo.Dealer.ID
            End If
            Dim objDistribution As SalesmanUnifDistribution = New SalesmanUnifDistributionFacade(User).Retrieve(CInt(ddlUnifDist.SelectedValue))
            For Each objUniform As SalesmanUniform In objDistribution.SalesmanUniforms
                strUniformID = strUniformID & objUniform.ID.ToString & ","
            Next

            If strUniformID = "" Then
                MessageBox.Show("Tidak ada data Seragam dalam Kode Pesanan ini")
                dtgUniformOrder.DataSource = Nothing
                dtgUniformOrder.DataBind()
                Return
            Else
                strUniformID = Left(strUniformID, strUniformID.Length - 1)
            End If

            If 1 = 1 Or Request.QueryString("id") = String.Empty Then '  OrElse (Request.QueryString("id") <> String.Empty And Request.QueryString("mode").ToUpper = "EDIT") Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanUniform.ID", MatchType.InSet, "(" & strUniformID & ")"))

                '07-Sep-2007    Deddy H     Filter dengan dealer ybs
                criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.Dealer.ID", MatchType.Exact, DealerId))

                If ddlJobPositionDesc.SelectedIndex > 0 Then
                    criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.JobPosition.ID", MatchType.Exact, ddlJobPositionDesc.SelectedValue.ToString))
                End If
                '28-Sep-2007    Deddy H     Ambil yang sdh dirilis dari pengguna seragam
                criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "IsReleased", MatchType.Exact, CType(EnumSalesmanUniformAssigned.IsReleased.Released, Byte)))

                If Not blnValidate Then
                    ' buat baru hanya yang belum divalidate yang bs diorder
                    'criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "IsValidate", MatchType.Exact, CType(EnumSalesmanUniformAssigned.IsValidate.Not_Validate, Byte)))
                Else
                    ' hanya mengambil data sales ybs
                    If strSalesId <> "" Then
                        'Todo Inset
                        criterias.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.ID", MatchType.InSet, CommonFunction.GetStrValue(strSalesId, ";", ",")))
                    End If
                End If

                arlUniformAssigned = New SalesmanUniformAssignedFacade(User).Retrieve(criterias)
                arlUniformAssigned = CommonFunction.SortArraylist(arlUniformAssigned, GetType(SalesmanUniformAssigned), "SalesmanHeader.ID", Sort.SortDirection.ASC)
                'arlUniformOrder = arlUniformAssigned
                If arlUniformAssigned.Count = 0 Then
                    MessageBox.Show("Tidak ada data Salesman dalam kode distribusi ini")
                    dtgUniformOrder.DataSource = New ArrayList
                    dtgUniformOrder.DataBind()
                    Return
                End If

                'Get one Salesman Each
                Dim tmpsalesmanid As Integer = 0
                For Each itemAssigned As SalesmanUniformAssigned In arlUniformAssigned
                    If 1 = 1 And tmpsalesmanid <> itemAssigned.SalesmanHeader.ID Then
                        Dim objOrder As SalesmanUniformOrderDetail = New SalesmanUniformOrderDetail
                        objOrder.SalesmanHeader = itemAssigned.SalesmanHeader
                        objOrder.SalesmanUniform = itemAssigned.SalesmanUniform
                        objOrder.UniformSize = itemAssigned.UniformSize

                        arlUniformOrder.Add(objOrder)
                        tmpsalesmanid = itemAssigned.SalesmanHeader.ID
                    End If
                Next
            End If
            SessHelper.SetSession("arlOrder", arlUniformOrder)
            SessHelper.SetSession("arlUniformAssigned", arlUniformAssigned)
        End If


        Dim arlToBind As ArrayList = SessHelper.GetSession("arlOrder")
        'arlToBind = CommonFunction.SortArraylist(arlToBind, GetType(SalesmanUniformOrderDetail), ViewState("currSortColumn"), ViewState("currSortDirection"))

        Dim totalRow As Integer = arlToBind.Count
        dtgUniformOrder.DataSource = arlToBind
        dtgUniformOrder.VirtualItemCount = totalRow
        dtgUniformOrder.DataBind()

        Dim Total As Decimal = 0
        Dim HargaDealer As Decimal
        If lblHargaDealerVal.Text = "" Then
            HargaDealer = 0
        Else
            HargaDealer = CType(lblHargaDealerVal.Text, Decimal)
        End If

        If dtgUniformOrder.Items.Count > 0 Then
            blnValidate = False
            btnSimpanDealer.Enabled = True
            btnValidate.Enabled = True
            btnSimpanDealer.Visible = _createPriv
            btnValidate.Visible = _submitPriv
        End If

        Dim idx As Integer = 0
        Dim oSUA As SalesmanUniformAssigned
        Dim IsValidated As Boolean = False
        Total = 0
        For i As Integer = 0 To arlUniformAssigned.Count - 1
            oSUA = CType(arlUniformAssigned(i), SalesmanUniformAssigned)
            If oSUA.IsValidate = EnumSalesmanUniformAssigned.IsReleased.Released Then
                Dim ChkBoxItem As CheckBox = dtgUniformOrder.Items(i).FindControl("ChkBoxItem")
                ChkBoxItem.Checked = True
                IsValidated = True
                Total += Val(HargaDealer)
            End If
        Next
        lblGrandTotal.Text = Format(Total, "#,##0")
        If IsValidated Then
            If Not IsNothing(Request.QueryString("mode")) AndAlso Request.QueryString("mode").ToUpper = "EDIT" Then
                dtgUniformOrder.Columns(7).Visible = True
                btnSimpanDealer.Visible = _createPriv
                btnValidate.Visible = _submitPriv
            Else
                dtgUniformOrder.Columns(7).Visible = False
                btnSimpanDealer.Enabled = False
                btnValidate.Enabled = False
                btnSimpanDealer.Visible = False
                btnValidate.Visible = False
                For Each di As DataGridItem In dtgUniformOrder.Items
                    Dim ChkBoxItem1 As CheckBox = di.FindControl("ChkBoxItem")
                    ChkBoxItem1.Enabled = False
                Next
            End If
        End If
    End Sub

    Private Function GetAssigned() As ArrayList
        Dim arlUniformOrder As New ArrayList
        Dim arlUniformAssigned As New ArrayList
        Dim strUniformID As String

        Dim objDistribution As SalesmanUnifDistribution = New SalesmanUnifDistributionFacade(User).Retrieve(CInt(ddlUnifDist.SelectedValue))
        For Each objUniform As SalesmanUniform In objDistribution.SalesmanUniforms
            strUniformID = strUniformID & objUniform.ID.ToString & ","
        Next

        If strUniformID = "" Then
            Return New ArrayList 'nothing 
        Else
            strUniformID = Left(strUniformID, strUniformID.Length - 1)
        End If


        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderDetail), "SalesmanUniformOrderHeader.ID", MatchType.Exact, Request.QueryString("id")))
        arlUniformOrder = New SalesmanUniformOrderDetailFacade(User).Retrieve(criterias)

        ' mengambil data salesman ybs
        Dim criteriaOrderDetail As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaOrderDetail.opAnd(New Criteria(GetType(SalesmanUniformOrderDetail), "SalesmanUniformOrderHeader.ID", MatchType.Exact, Request.QueryString("id")))
        arlUniformOrder = New SalesmanUniformOrderDetailFacade(User).Retrieve(criteriaOrderDetail)

        Dim strSalesmanHeaderId As String
        strSalesmanHeaderId = ""
        If arlUniformOrder.Count > 0 Then
            For Each item As SalesmanUniformOrderDetail In arlUniformOrder
                If strSalesmanHeaderId = "" Then
                    strSalesmanHeaderId = item.SalesmanHeader.ID.ToString
                Else
                    strSalesmanHeaderId = strSalesmanHeaderId & ";" & item.SalesmanHeader.ID.ToString
                End If
            Next
        End If

        ' mengambil data assigned
        Dim criteriaAssigned As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformAssigned), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaAssigned.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanUniform.ID", MatchType.InSet, "(" & strUniformID & ")"))
        If strSalesmanHeaderId <> "" Then
            'Todo Inset
            criteriaAssigned.opAnd(New Criteria(GetType(SalesmanUniformAssigned), "SalesmanHeader.ID", MatchType.InSet, CommonFunction.GetStrValue(strSalesmanHeaderId, ";", ",")))
        End If
        arlUniformAssigned = New SalesmanUniformAssignedFacade(User).Retrieve(criteriaAssigned)
        arlUniformAssigned = CommonFunction.SortArraylist(arlUniformAssigned, GetType(SalesmanUniformAssigned), "SalesmanHeader.ID", Sort.SortDirection.ASC)

        Return arlUniformAssigned

    End Function

    ' Digunakan untuk user dealer saja
    Private Sub ClearData()
        ddlUnifDist.SelectedIndex = -1
        ddlJobPositionDesc.SelectedIndex = -1

        lblOrderNo.Text = ""
        lblHargaNormalVal.Text = ""
        lblHargaDealerVal.Text = ""
        lblGrandTotal.Text = "0"

        If dtgUniformOrder.Items.Count > 0 Then
            dtgUniformOrder.DataSource = Nothing
            dtgUniformOrder.DataBind()
        End If
    End Sub

    Private Function GetOrderNo() As String
        Dim sRsl As String = ""
        Dim objSUOHFac As SalesmanUniformOrderHeaderFacade = New SalesmanUniformOrderHeaderFacade(User)
        Dim crtSUOH As CriteriaComposite
        Dim arlSUOH As ArrayList
        Dim objUI As UserInfo

        If ddlUnifDist.SelectedValue = "-1" Then
            Return sRsl
        End If
        If Not IsNothing(SessHelper.GetSession("LOGINUSERINFO")) Then
            objUI = CType(SessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Else
            Return sRsl
        End If

        crtSUOH = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtSUOH.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "Dealer.ID", MatchType.Exact, objUI.Dealer.ID))
        crtSUOH.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "SalesmanUnifDistribution.SalesmanUnifDistributionCode", MatchType.Exact, ddlUnifDist.SelectedItem.Text))
        arlSUOH = objSUOHFac.Retrieve(crtSUOH)
        If arlSUOH.Count > 0 Then
            sRsl = CType(arlSUOH(0), SalesmanUniformOrderHeader).OrderNumber
        End If
        Return sRsl
    End Function
#End Region

#Region "Privilege & Transaction Control"

    Private Function IsActiveTransControl() As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransactionControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TransactionControl), "Kind", MatchType.Exact, CInt(EnumDealerTransType.DealerTransKind.PesanSeragam)))
        criterias.opAnd(New Criteria(GetType(TransactionControl), "Dealer.ID", MatchType.Exact, CType(Session("Dealer"), Dealer).ID))

        Dim objTransControl As TransactionControl = New DealerFacade(User).RetrieveTransactionControlByCriteria(criterias)

        If IsNothing(objTransControl) Then
            Return True
        Else
            If objTransControl.Status = 1 Then
                Return True
            Else
                Return False
            End If
        End If

    End Function

    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.UniformListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Seragam Tenaga Penjual - Pesan Seragam")
        End If
    End Sub

    Private Function CheckCreatePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.UniformRequestCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckKTBPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.UniformApprovalViewRecap_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckSubmitPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.UniformRequestSubmit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function checkRecapPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.UniformApprovalViewRecap_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region


End Class
