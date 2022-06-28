#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

Public Class FrmInputPenjualanAcc
    Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents txtNamaCustomer As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoSurat As System.Web.UI.WebControls.Label
    Protected WithEvents txtTelp As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNomorLaporan As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblWO As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPermintaan As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblTanggalInput As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRangka As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoRangka As System.Web.UI.WebControls.TextBox
    Protected WithEvents lnkbtnCheckChassis As System.Web.UI.WebControls.LinkButton
    Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents dtgPartIncidental As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblReportNum As System.Web.UI.WebControls.Label
    Protected WithEvents txtRefNum As System.Web.UI.WebControls.TextBox
    Protected WithEvents calTanggal As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtEnumAccessories As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtCommentx As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtComment As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchKonfirmasi As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"
    Private _sessHelper As New SessionHelper
    Private _vstID As String = "_vstID"
    Private _sessAS As String = "FrmInputPenjualanAcc._sessAS"
    Private _sessASDs As String = "FrmInputPenjualanAcc._sessASDs"
    Private _sessCriterias As String = "FrmLaporanPenjualanAcc._sessCriterias"
#End Region

#Region "Methods"

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.Input_penjualan_accessories_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PENJUALAN ACCESSORIES - Input Penjualan")
        End If
    End Sub
    Private Sub Initialization()
        If Not IsNothing(Request.Item("ID")) Then
            Me.ViewState.Add(Me._vstID, Request.Item("ID"))
        Else
            Me.ViewState.Add(Me._vstID, 0)
        End If
        InitLibrary()
        BindData()
    End Sub

    Private Sub InitLibrary()
        Dim aACs As ArrayList
        Dim sAC As New SortCollection
        Dim cAC As New CriteriaComposite(New Criteria(GetType(AccessoriesCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim oDealer As Dealer = Session.Item("DEALER")
        Dim ID As Integer = CType(Me.ViewState.Item(Me._vstID), Integer)

        sAC.Add(New Sort(GetType(AccessoriesCategory), "Name", Sort.SortDirection.ASC))
        aACs = New AccessoriesCategoryFacade(User).Retrieve(cAC, sAC)
        Me.ddlKategori.Items.Clear()
        For Each oAC As AccessoriesCategory In aACs
            Me.ddlKategori.Items.Add(New ListItem(oAC.Name, oAC.ID))
        Next
        Me.txtEnumAccessories.Text = CType(EnumMaterialType.MaterialType.Accessories, Integer).ToString
        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 1).Visible = False
            Me.btnSimpan.Enabled = False
        Else
            Me.dtgMain.Columns(Me.dtgMain.Columns.Count - 1).Visible = True
            Me.btnSimpan.Enabled = True
        End If

        If ID > 0 AndAlso IsNothing(Me._sessHelper.GetSession(Me._sessCriterias)) = False Then
            Me.btnKembali.Visible = True
        Else
            Me.btnKembali.Visible = False
        End If
        lblSearchKonfirmasi.Attributes.Add("OnClick", "ShowPPKonfirmasi();")
    End Sub

    Private Sub BindData()
        Dim oASFac As New AccessoriesSaleFacade(User)
        Dim oAS As AccessoriesSale
        Dim ID As Integer = CType(Me.ViewState.Item(Me._vstID), Integer)
        Dim aASs As ArrayList

        oAS = oASFac.Retrieve(ID)
        If Not IsNothing(oAS) AndAlso oAS.ID > 0 Then
            Me._sessHelper.SetSession(Me._sessASDs, oAS.AccessoriesSaleDetails)

            Me.lblKodeDealerValue.Text = oAS.Dealer.DealerCode
            Me.lblNamaDealerValue.Text = oAS.Dealer.DealerName
            Me.ddlKategori.SelectedValue = oAS.AccessoriesCategoryID
            Me.lblReportNum.Text = oAS.ReportNumber
            Me.txtRefNum.Text = oAS.RefNumber
            Me.calTanggal.Value = oAS.SoldDate
            Me.txtNoRangka.Text = oAS.ChassisMaster.ChassisNumber

            Me.txtNamaCustomer.Text = oAS.CustomerName
            Me.txtTelp.Text = oAS.CustomerPhone
            Me.txtComment.Text = oAS.Comment
        Else
            oAS = New AccessoriesSale
            Me._sessHelper.SetSession(Me._sessASDs, New ArrayList)
            Dim oD As Dealer = Session.Item("DEALER")
            Me.lblKodeDealerValue.Text = oD.DealerCode
            Me.lblNamaDealerValue.Text = oD.DealerName
            'Me.ddlKategori.SelectedValue = oAS.AccessoriesCategoryID
            Me.lblReportNum.Text = String.Empty
            Me.txtRefNum.Text = String.Empty
            Me.calTanggal.Value = Now
            Me.txtNoRangka.Text = String.Empty

            Me.txtNamaCustomer.Text = String.Empty
            Me.txtTelp.Text = String.Empty
            Me.txtComment.Text = String.Empty
        End If
        Me._sessHelper.SetSession(Me._sessAS, oAS)
        Me.BindDetail()
    End Sub

    Private Sub BindDetail()
        Me.dtgMain.DataSource = Me._sessHelper.GetSession(Me._sessASDs)
        Me.dtgMain.DataBind()
    End Sub

    Private Function IsDataValid() As Boolean
        Dim oCM As ChassisMaster
        Dim oSPM As SparePartMaster
        Dim oSPMFac As New SparePartMasterFacade(User)

        If Me.txtRefNum.Text.Trim = String.Empty Then
            MessageBox.Show("Nomor Referensi Belum Diisi")
            Return False
        End If
        If Me.txtNamaCustomer.Text.Trim = String.Empty Then
            MessageBox.Show("Nama Customer Masih Kosong")
            Return False
        End If
        If Me.ddlKategori.SelectedIndex < 0 Then
            MessageBox.Show("Silahkan Pilih Kategori Input")
            Return False
        End If
        oCM = New ChassisMasterFacade(User).Retrieve(Me.txtNoRangka.Text)
        If IsNothing(oCM) OrElse oCM.ID < 1 Then
            MessageBox.Show("Nomor Rangka " & Me.txtNoRangka.Text & " Tidak Valid")
            Return False
        Else
            If IsNothing(oCM.EndCustomer) OrElse oCM.EndCustomer.ID < 1 Then
                MessageBox.Show("Nomor Rangka " & Me.txtNoRangka.Text & " Tidak Valid")
                Return False
            End If
            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            If oCM.Category.ProductCategory.Code <> companyCode Then
                MessageBox.Show("Nomor Rangka " & Me.txtNoRangka.Text & " bukan produk " & companyCode)
                Return False
            End If
        End If

        If CType(Me._sessHelper.GetSession(Me._sessASDs), ArrayList).Count < 1 Then
            MessageBox.Show("Simpan Gagal, Barang Belum Diisi")
            Return False
        End If

        For Each di As DataGridItem In Me.dtgMain.Items
            Dim txtNomorBarang As TextBox = di.FindControl("txtNomorBarang")
            Dim txtUnit As TextBox = di.FindControl("txtUnit")
            Dim Jumlah As Integer

            oSPM = oSPMFac.Retrieve(txtNomorBarang.Text)
            If IsNothing(oSPM) OrElse oSPM.ID < 1 Then
                MessageBox.Show("Part " & txtNomorBarang.Text & " Tidak Valid")
                Return False
            End If
            Try
                Jumlah = CType(txtUnit.Text, Integer)
            Catch ex As Exception
                Jumlah = 0
            End Try
            If Jumlah <= 0 Then
                MessageBox.Show("Jumlah Part " & txtNomorBarang.Text & " Tidak Valid")
                Return False
            End If
        Next

        '---Already Inputted
        Dim cAS As New CriteriaComposite(New Criteria(GetType(AccessoriesSale), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aASs As ArrayList
        Dim ID As Integer = Me.ViewState.Item(Me._vstID)


        cAS.opAnd(New Criteria(GetType(AccessoriesSale), "RefNumber", MatchType.Exact, Me.txtRefNum.Text.Trim))
        cAS.opAnd(New Criteria(GetType(AccessoriesSale), "ChassisMaster.ID", MatchType.Exact, oCM.ID))
        cAS.opAnd(New Criteria(GetType(AccessoriesSale), "ID", MatchType.No, ID))
        aASs = New AccessoriesSaleFacade(User).Retrieve(cAS)
        If aASs.Count > 0 Then
            MessageBox.Show("Simpan Gagal. Penjualan Dg No. Referensi " & Me.txtRefNum.Text.Trim & " Dan No. Rangka " & Me.txtNoRangka.Text.Trim & " Sudah Ada")
            Return False
        End If

        Return True
    End Function

    Private Sub SaveData()
        Dim oAS As AccessoriesSale = Me._sessHelper.GetSession(Me._sessAS)
        Dim oASFac As New AccessoriesSaleFacade(User)
        Dim aASDs As ArrayList = Me._sessHelper.GetSession(Me._sessASDs)
        Dim oAC As AccessoriesCategory
        If IsNothing(oAS) OrElse oAS.ID < 1 Then
            oAS = New AccessoriesSale
        End If
        oAS.Dealer = New DealerFacade(User).Retrieve(Me.lblKodeDealerValue.Text)
        oAS.AccessoriesCategory = New AccessoriesCategoryFacade(User).Retrieve(CType(Me.ddlKategori.SelectedValue, Integer))
        oAS.ReportNumber = Me.lblReportNum.Text
        oAS.RefNumber = Me.txtRefNum.Text
        oAS.SoldDate = Me.calTanggal.Value

        oAS.ChassisMaster = New ChassisMasterFacade(User).Retrieve(Me.txtNoRangka.Text)

        oAS.CustomerName = Me.txtNamaCustomer.Text.Trim
        oAS.CustomerPhone = Me.txtTelp.Text.Trim
        oAS.Comment = Me.txtComment.Text.Trim

        Dim oASD As AccessoriesSaleDetail
        Dim oSPM As SparePartMaster
        Dim oSPMFac As New SparePartMasterFacade(User)
        For Each di As DataGridItem In Me.dtgMain.Items
            Dim txtNomorBarang As TextBox = di.FindControl("txtNomorBarang")
            Dim txtUnit As TextBox = di.FindControl("txtUnit")

            oSPM = oSPMFac.Retrieve(txtNomorBarang.Text)
            If IsNothing(oSPM) OrElse oSPM.ID < 1 Then
                MessageBox.Show("Nomor Barang " & txtNomorBarang.Text & " Tidak Valid")
                Exit Sub
            Else
                oASD = aASDs(di.ItemIndex)
                oASD.SparePartMaster = oSPM
                Try
                    oASD.Jumlah = CType(txtUnit.Text, Integer)
                Catch ex As Exception
                    oASD.Jumlah = 0
                    MessageBox.Show("Jumlah " & oSPM.PartCode & " Tidak Boleh 0")
                    Exit Sub
                End Try
                aASDs(di.ItemIndex) = oASD
            End If
        Next
        Me._sessHelper.SetSession(Me._sessASDs, aASDs)
        If oAS.ID < 1 Then
            oAS.ID = oASFac.Insert(oAS, aASDs)
        Else
            oAS.ID = oASFac.Update(oAS, aASDs)
        End If
        If oAS.ID > 0 Then
            Me.ViewState.Item(Me._vstID) = oAS.ID
            Me._sessHelper.SetSession(Me._sessAS, oAS)
            Me.BindData()
            MessageBox.Show(SR.SaveSuccess())
        Else
            MessageBox.Show(SR.SaveFail())
        End If
    End Sub
#End Region

#Region "Events"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CheckUserPrivilege()
        If IsPostBack = False Then
            Initialization()
        End If
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim txtNomorBarang As TextBox = e.Item.FindControl("txtNomorBarang")
            Dim btnEditSparePart As Button = e.Item.FindControl("btnEditSparePart")
            Dim lblEditNomorBarang As Label = e.Item.FindControl("lblEditNomorBarang")
            Dim lblNamaPart As Label = e.Item.FindControl("lblNamaPart")
            Dim lblModel As Label = e.Item.FindControl("lblModel")

            lblEditNomorBarang.Attributes.Add("OnClick", "ShowPPKodeBarangSelection('" & btnEditSparePart.ClientID & "','" & txtNomorBarang.ClientID & "','" & lblNamaPart.ClientID & "','" & lblModel.ClientID & "')")
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Dim lblFooterNomorBarang As Label = e.Item.FindControl("lblFooterNomorBarang")
            Dim btnFooterSparePart As Button = e.Item.FindControl("btnFooterSparePart")
            Dim txtFooterNomorBarang As TextBox = e.Item.FindControl("txtFooterNomorBarang")
            Dim lblFooterNamaPart As Label = e.Item.FindControl("lblFooterNamaPart")
            Dim lblFooterModel As Label = e.Item.FindControl("lblFooterModel")

            lblFooterNomorBarang.Attributes.Add("OnClick", "ShowPPKodeBarangSelection('" & btnFooterSparePart.ClientID & "','" & txtFooterNomorBarang.ClientID & "','" & lblFooterNamaPart.ClientID & "','" & lblFooterModel.ClientID & "')")

        End If
    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.CommandName.Trim.ToUpper = "Delete".ToUpper Then
                Dim txtNomorBarang As TextBox = e.Item.FindControl("txtNomorBarang")
                Dim txtUnit As TextBox = e.Item.FindControl("txtUnit")
                Dim aASDs As ArrayList = Me._sessHelper.GetSession(Me._sessASDs)

                aasds.RemoveAt(e.Item.ItemIndex)
                Me._sessHelper.SetSession(Me._sessASDs, aasds)
                Me.BindDetail()
            ElseIf e.CommandName.Trim.ToUpper = "UpdateSparePart".ToUpper Then
                Dim txtNomorBarang As TextBox = e.Item.FindControl("txtNomorBarang")
                Dim lblNamaPart As Label = e.Item.FindControl("lblNamaPart")
                Dim lblModel As Label = e.Item.FindControl("lblModel")
                Dim oSPM As SparePartMaster = New SparePartMasterFacade(User).Retrieve(txtnomorbarang.Text)

                If Not IsNothing(ospm) AndAlso ospm.ID > 0 Then
                    lblNamaPart.Text = ospm.PartName
                    lblModel.Text = ospm.ModelCode
                Else
                    lblNamaPart.Text = String.Empty
                    lblModel.Text = String.Empty
                    MessageBox.Show(txtnomorbarang.Text & " Tidak Terdaftar")
                End If
            End If
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Dim txtFooterNomorBarang As TextBox = e.Item.FindControl("txtFooterNomorBarang")
            Dim lblFooterNamaPart As Label = e.Item.FindControl("lblFooterNamaPart")
            Dim lblFooterModel As Label = e.Item.FindControl("lblFooterModel")
            Dim txtFooterUnit As TextBox = e.Item.FindControl("txtFooterUnit")
            Dim oSPM As SparePartMaster = New SparePartMasterFacade(User).Retrieve(txtFooterNomorBarang.Text)

            If IsNothing(oSPM) OrElse oSPM.ID < 1 Then
                lblFooterNamaPart.Text = String.Empty
                lblFooterModel.Text = String.Empty
                MessageBox.Show("Part " & txtFooterNomorBarang.Text & " Tidak Valid")
                Exit Sub
            Else
                lblFooterNamaPart.Text = ospm.PartName
                lblFooterModel.Text = ospm.ModelCode
            End If


            If e.CommandName.Trim.ToUpper = "Add".ToUpper Then
                Dim aASDs As ArrayList = Me._sessHelper.GetSession(Me._sessASDs)
                Dim oASD As New AccessoriesSaleDetail

                If IsNothing(oSPM) OrElse oSPM.ID < 1 Then
                    MessageBox.Show("Part " & txtFooterNomorBarang.Text & " Tidak Valid")
                    Exit Sub
                Else
                    lblFooterNamaPart.Text = ospm.PartName
                    lblFooterModel.Text = ospm.ModelCode
                End If

                oASD.AccessoriesSale = Me._sessHelper.GetSession(Me._sessAS)
                oASD.SparePartMaster = oSPM
                Try
                    oASD.Jumlah = CType(txtFooterUnit.Text, Integer)
                Catch ex As Exception
                    oASD.Jumlah = 0
                End Try
                aASDs.Add(oASD)
                Me._sessHelper.SetSession(Me._sessASDs, aASDs)
                Me.BindDetail()
            ElseIf e.CommandName.Trim.ToUpper = "UpdateSparePart".ToUpper Then
            End If

        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If IsDataValid() = False Then Exit Sub
        SaveData()
    End Sub

    Private Sub btnKembali_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmLaporanPenjualanAcc.aspx?IsRedirected=1")
    End Sub

#End Region
End Class
