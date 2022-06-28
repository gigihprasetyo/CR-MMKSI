#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

Public Class frmEntryPartIncidental
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblNomorPermintaan As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPermintaanValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents IntiCalendar1 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblNomorPolisi As System.Web.UI.WebControls.Label
    Protected WithEvents lblWO As System.Web.UI.WebControls.Label
    Protected WithEvents txtNomorPolisi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWO As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPIC As System.Web.UI.WebControls.Label
    Protected WithEvents txtPIC As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents lblNoSurat As System.Web.UI.WebControls.Label
    Protected WithEvents txtNomorSurat As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalInput As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents dtgPartIncidental As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTelp As System.Web.UI.WebControls.Label
    Protected WithEvents txtTelp As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblNoRangka As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoRangka As System.Web.UI.WebControls.TextBox
    Protected WithEvents lnkbtnCheckChassis As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblVehicleType As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunProduksi As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents lblPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents rblStatus As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents txtStatusLain As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtYearProduction As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator6 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sessionHelper As New SessionHelper
    Private objDealer As Dealer
    Private objPartIncidentalHeader As PartIncidentalHeader
    Private objPartIncidentalDetail As PartIncidentalDetail
    Private Mode As enumMode.Mode
    Private PartIncidentalHeaderID As String
    Private strNA As String = "N/A"

    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.ViewPartIncidentalEntry_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Pengajuan Permintaan Khusus")
        End If

        btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.SavePartIncidentalEntry_Privilege)
        btnDelete.Visible = SecurityProvider.Authorize(Context.User, SR.DeletePartIncidentalEntry_Privilege)

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            PartIncidentalHeaderID = Request.QueryString("id")
            RetrieveMaster()
            SetMode()
            If Not objDealer Is Nothing Then
                lblKodeDealerValue.Text = objDealer.DealerCode
                lblNamaDealerValue.Text = objDealer.DealerName & " / " & objDealer.SearchTerm2
            End If
            BindDataToPage()
            ActivateUserPrivilege()
        End If
        btnDelete.Attributes.Add("OnClick", "return confirm('Yakin Permintaan Khusus ini akan dihapus?');")
        'btnKembali.Attributes.Add("OnClick", "window.history.go(-1)")
    End Sub

    Private Sub BindDetailToGrid()
        objDealer = sessionHelper.GetSession("DEALER")
        objPartIncidentalHeader = sessionHelper.GetSession("Part")
        If (objPartIncidentalHeader.KTBStatus <> CType(PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru, Short) OrElse ((Not (objPartIncidentalHeader.Dealer Is Nothing)) AndAlso (objPartIncidentalHeader.Dealer.ID <> objDealer.ID))) Then
            dtgPartIncidental.Columns(5).Visible = False
            dtgPartIncidental.Columns(6).Visible = False
            dtgPartIncidental.Columns(7).Visible = False
        End If
        dtgPartIncidental.DataSource = objPartIncidentalHeader.PartIncidentalDetails
        dtgPartIncidental.DataBind()
    End Sub

    Private Sub RetrieveMaster()
        rblStatus.DataSource = PartIncidentalStatus.RetrievePartIncidentalStatus()
        rblStatus.DataTextField = "NameStatus"
        rblStatus.DataValueField = "ValStatus"
        rblStatus.DataBind()
        rblStatus.SelectedIndex = 0


    End Sub

    Sub dtgPartIncidental_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs)
        objPartIncidentalHeader = sessionHelper.GetSession("Part")
        Dim objPartIncidentalDetailFacade As PartIncidentalDetailFacade
        Select Case (e.CommandName)
            Case "Delete"
                Dim lbl1 As Label = e.Item.Cells(0).FindControl("lblNo")
                Mode = ViewState("Mode")
                If (Mode = enumMode.Mode.EditMode) Then
                    If (CType(objPartIncidentalHeader.KTBStatus, Short) = CType(PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru, Short)) Then
                        If (objPartIncidentalHeader.PartIncidentalDetails.Count <> 1) Then
                            objPartIncidentalDetailFacade = New PartIncidentalDetailFacade(User)
                            objPartIncidentalDetailFacade.Delete(objPartIncidentalHeader.PartIncidentalDetails.Item(CType(lbl1.Text, Integer) - 1))
                        Else
                            MessageBox.Show("Permintaan Khusus Harus memiliki minimal 1 Detail")
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("Status Permintaan Bukan Baru")
                        Exit Sub
                    End If
                End If
                objPartIncidentalHeader.PartIncidentalDetails.Remove(objPartIncidentalHeader.PartIncidentalDetails.Item(CType(lbl1.Text, Integer) - 1))
                sessionHelper.SetSession("Part", objPartIncidentalHeader)
                BindDataToPage()
                Mode = ViewState("Mode")
                If objPartIncidentalHeader.PartIncidentalDetails.Count = 0 And Mode = enumMode.Mode.NewItemMode Then
                    SetButtonNewMode()
                End If
                dtgPartIncidental.ShowFooter = True
            Case "Add"
                If Not Page.IsValid Then
                    Return
                End If
                Dim txt1 As TextBox = e.Item.FindControl("txtFooterNomorBarang")
                Dim txt2 As TextBox = e.Item.FindControl("txtFooterUnit")
                Dim txt3 As TextBox = e.Item.FindControl("txtFooterChassis")
                Dim txt4 As TextBox = e.Item.FindControl("txtFooterPerakitan")

                'Modified By Ikhsan, 20081119
                'Requested by Yurike as Part of CR
                'To make sure that only validated chassis number as input data
                If txtNoRangka.ForeColor.ToString = Color.Black.ToString Then
                    If (ValidateItem(txt1.Text, txt2.Text, txt3.Text, txt4.Text) AndAlso ValidateDuplication(txt1.Text.ToUpper, "Add", -1)) Then
                        objPartIncidentalDetail = New KTB.DNet.Domain.PartIncidentalDetail
                        objPartIncidentalDetail.SparePartMaster = New SparePartMasterFacade(User).Retrieve(txt1.Text)
                        objPartIncidentalDetail.Quantity = txt2.Text
                        'remark by heru CR
                        objPartIncidentalDetail.ChassisNumber = CStr(txt3.Text)
                        objPartIncidentalDetail.AssemblyYear = CStr(txt4.Text)
                        Mode = ViewState("Mode")
                        If (Mode = enumMode.Mode.EditMode) Then
                            objPartIncidentalDetailFacade = New PartIncidentalDetailFacade(User)
                            objPartIncidentalDetail.PartIncidentalHeader = objPartIncidentalHeader
                            objPartIncidentalDetailFacade.Insert(objPartIncidentalDetail)
                        End If
                    Else
                        Exit Sub
                    End If
                    objPartIncidentalHeader.PartIncidentalDetails.Add(objPartIncidentalDetail)
                    BindDataToPage()
                    SetButtonEditMode()
                Else
                    lblError.Text = "Nomor Rangka Tidak Valid"
                    Return
                End If

        End Select
    End Sub

    Sub dtgPartIncidental_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        dtgPartIncidental.EditItemIndex = CInt(e.Item.ItemIndex)
        dtgPartIncidental.ShowFooter = False
        BindDetailToGrid()
    End Sub

    Sub dtgPartIncidental_Cancel(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        dtgPartIncidental.EditItemIndex = -1
        BindDetailToGrid()
        dtgPartIncidental.ShowFooter = True
    End Sub

    Sub dtgPartIncidental_Update(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        objPartIncidentalHeader = sessionHelper.GetSession("Part")
        Dim txt1 As TextBox = E.Item.FindControl("txtEditNomorBarang")
        Dim txt2 As TextBox = E.Item.FindControl("txtEditUnit")
        Dim txt3 As TextBox = E.Item.FindControl("txtEditChassis")
        Dim txt4 As TextBox = E.Item.FindControl("txtEditPerakitan")
        If (ValidateItem(txt1.Text, txt2.Text, txt3.Text, txt4.Text) AndAlso ValidateDuplication(txt1.Text.ToUpper, "Edit", E.Item.ItemIndex)) Then
            objPartIncidentalDetail = objPartIncidentalHeader.PartIncidentalDetails(E.Item.ItemIndex)
            objPartIncidentalDetail.SparePartMaster = New SparePartMasterFacade(User).Retrieve(txt1.Text)
            objPartIncidentalDetail.Quantity = txt2.Text
            'remark by heru cr
            objPartIncidentalDetail.ChassisNumber = txt3.Text
            objPartIncidentalDetail.AssemblyYear = txt4.Text
            sessionHelper.SetSession("Part", objPartIncidentalHeader)
            dtgPartIncidental.EditItemIndex = -1
            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.EditMode) Then
                Dim objPartIncidentalDetailFacade As New PartIncidentalDetailFacade(User)
                objPartIncidentalDetail.PartIncidentalHeader = objPartIncidentalHeader
                objPartIncidentalDetailFacade.Update(objPartIncidentalDetail)
            End If
            dtgPartIncidental.EditItemIndex = -1
            dtgPartIncidental.ShowFooter = True
            BindDetailToGrid()
        End If
    End Sub

    Sub dtgPartIncidental_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs)
        objPartIncidentalHeader = sessionHelper.GetSession("Part")
        If E.Item.ItemType = ListItemType.Footer Then
            SetDtgPartIncidentalItemFooter(E)
        ElseIf E.Item.ItemType = ListItemType.EditItem OrElse E.Item.ItemType = ListItemType.SelectedItem Then
            SetDtgPartIncidentalItemEdit(E)
        End If
        If Not (objPartIncidentalHeader.PartIncidentalDetails.Count = 0 Or E.Item.ItemIndex = -1) Then
            objPartIncidentalDetail = objPartIncidentalHeader.PartIncidentalDetails(E.Item.ItemIndex)
            E.Item.Cells(2).Text = objPartIncidentalDetail.SparePartMaster.PartName
            E.Item.Cells(3).Text = objPartIncidentalDetail.SparePartMaster.ModelCode
            Dim lbtnHapus As LinkButton = CType(E.Item.FindControl("lbtnDelete"), LinkButton)
            lbtnHapus.Attributes("onclick") = "return confirm('Yakin akan hapus record ini?');"
        End If
    End Sub

    Private Sub SetDtgPartIncidentalItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblFooterNomorBarang As Label = CType(e.Item.FindControl("lblFooterNomorBarang"), Label)
        lblFooterNomorBarang.Attributes("onclick") = "ShowPPKodeBarangSelection();"
    End Sub

    Private Sub SetDtgPartIncidentalItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblEditNomorBarang As Label = CType(e.Item.FindControl("lblEditNomorBarang"), Label)
        lblEditNomorBarang.Attributes("onclick") = "ShowPPKodeBarangSelection();"
    End Sub

    Private Sub rblStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rblStatus.SelectedIndex = 2 Then
            txtStatusLain.ReadOnly = False
            RequiredFieldValidator1.Enabled = True
        Else
            txtStatusLain.Text = String.Empty
            txtStatusLain.ReadOnly = True
            RequiredFieldValidator1.Enabled = False
        End If
    End Sub

    Private Sub SetButtonNewMode()
        btnSimpan.Enabled = True
        btnDelete.Enabled = True
        dtgPartIncidental.ShowFooter = True
        IntiCalendar1.Enabled = False
        txtNomorPolisi.ReadOnly = False
        txtNomorSurat.ReadOnly = True
        txtWO.ReadOnly = False
        rblStatus.Enabled = False
        rblStatus.Visible = False
        lblPengajuan.Visible = True
        txtStatusLain.Visible = False
        txtPIC.ReadOnly = False

        Dim totalStatus = rblStatus.Items.Count
        For i As Integer = 1 To totalStatus - 1
            rblStatus.Items.RemoveAt(rblStatus.Items.Count - 1)
        Next
    End Sub

    Private Sub SetButtonEditMode()
        objPartIncidentalHeader = sessionHelper.GetSession("Part")
        Dim objDealerSession As Dealer = sessionHelper.GetSession("DEALER")
        If (objPartIncidentalHeader.KTBStatus <> PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru) OrElse ((Not (objPartIncidentalHeader.Dealer Is Nothing)) AndAlso (objPartIncidentalHeader.Dealer.ID <> objDealerSession.ID)) Then
            btnSimpan.Enabled = False
            'btnBaru.Enabled = False
            btnDelete.Enabled = False
            'btnValidasi.Enabled = False
            dtgPartIncidental.ShowFooter = False
            IntiCalendar1.Enabled = False
            txtNomorPolisi.ReadOnly = True
            txtWO.ReadOnly = True
            txtNomorSurat.ReadOnly = False
            rblStatus.Enabled = False
            txtPIC.ReadOnly = True
        Else
            'If objPartIncidentalHeader.ID <> 0 Then
            '    btnBaru.Enabled = True
            '    btnValidasi.Enabled = True
            'End If
        End If
    End Sub

    Private Sub BindDataToPage()
        If IsNothing(sessionHelper.GetSession("Part")) Then
            objPartIncidentalHeader = New KTB.DNet.Domain.PartIncidentalHeader
            objPartIncidentalHeader.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru
            sessionHelper.SetSession("Part", objPartIncidentalHeader)
            ClearAllFields()
        Else
            objPartIncidentalHeader = sessionHelper.GetSession("Part")
            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.EditMode) Then
                BindHeaderToForm()
            End If
        End If
        BindDetailToGrid()
    End Sub

    Private Function GenerateRequestNumber() As String
        Dim returnValue As String
        objDealer = sessionHelper.GetSession("DEALER")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "RequestNumber", MatchType.StartsWith, objDealer.SearchTerm2))
        Dim sPIH As New SortCollection
        sPIH.Add(New Sort(GetType(PartIncidentalHeader), "RequestNumber", Sort.SortDirection.ASC))
        Dim arlPartIncidentalHeader As ArrayList = New PartIncidentalHeaderFacade(User).Retrieve(criterias, sPIH)
        If arlPartIncidentalHeader.Count > 0 Then
            returnValue = objDealer.SearchTerm2 & "-" & (CInt(Strings.Right(CType(arlPartIncidentalHeader(arlPartIncidentalHeader.Count - 1), PartIncidentalHeader).RequestNumber.ToString(), 7)) + 1).ToString.PadLeft(7, "0")
        Else
            returnValue = objDealer.SearchTerm2 & "-0000001"
        End If

        Return returnValue
    End Function

    Private Sub ClearAllFields()
        lblNomorPermintaanValue.Text = String.Empty
        txtNomorPolisi.Text = String.Empty
        txtWO.Text = String.Empty
        txtPIC.Text = String.Empty
        txtStatusLain.Text = String.Empty
        txtStatusLain.ReadOnly = True
        rblStatus.SelectedIndex = 0
        RequiredFieldValidator1.Enabled = False
        txtNomorSurat.Text = "Otomatis"
    End Sub

    Private Sub BindHeaderToForm()
        objPartIncidentalHeader = sessionHelper.GetSession("Part")
        lblKodeDealerValue.Text = objPartIncidentalHeader.Dealer.DealerCode
        lblNamaDealerValue.Text = objPartIncidentalHeader.Dealer.DealerName & " / " & objPartIncidentalHeader.Dealer.SearchTerm2
        lblNomorPermintaanValue.Text = objPartIncidentalHeader.RequestNumber
        IntiCalendar1.Value = objPartIncidentalHeader.IncidentalDate
        txtNomorPolisi.Text = objPartIncidentalHeader.PoliceNumber
        txtWO.Text = objPartIncidentalHeader.WorkOrder
        txtPIC.Text = objPartIncidentalHeader.PIC
        txtNomorSurat.Text = objPartIncidentalHeader.DealerMailNumber
        If objPartIncidentalHeader.Status.ToString() = "Pemesanan" Then
            rblStatus.SelectedIndex = 0
        ElseIf objPartIncidentalHeader.Status.ToString() = "Estimasi" Then
            rblStatus.SelectedIndex = 1
        Else
            rblStatus.SelectedIndex = 2
            txtStatusLain.Text = objPartIncidentalHeader.Status
            txtStatusLain.ReadOnly = False

        End If
    End Sub

    Private Function GenerateID(ByVal id As Integer) As String
        If id < 10 Then
            Return "000000" & id.ToString
        ElseIf id < 100 Then
            Return "00000" & id.ToString
        ElseIf id < 1000 Then
            Return "0000" & id.ToString
        ElseIf id < 10000 Then
            Return "000" & id.ToString
        ElseIf id < 100000 Then
            Return "00" & id.ToString
        ElseIf id < 1000000 Then
            Return "0" & id.ToString
        Else
            Return id.ToString
        End If
    End Function
    Private Sub BindDataToObject()
        objPartIncidentalHeader.Dealer = sessionHelper.GetSession("DEALER")
        objPartIncidentalHeader.IncidentalDate = IntiCalendar1.Value
        objPartIncidentalHeader.PIC = txtPIC.Text
        objPartIncidentalHeader.Phone = txtTelp.Text
        objPartIncidentalHeader.PoliceNumber = txtNomorPolisi.Text

        objPartIncidentalHeader.ChassisNumber = txtNoRangka.Text
        objPartIncidentalHeader.VehicleType = lblVehicleType.Text
        'objPartIncidentalHeader.AssemblyYear = lblTahunProduksi.Text

        If txtYearProduction.Text = strNA Then
            objPartIncidentalHeader.AssemblyYear = 1980
        Else
            objPartIncidentalHeader.AssemblyYear = txtYearProduction.Text
        End If

        objPartIncidentalHeader.WorkOrder = txtWO.Text
        'objPartIncidentalHeader.DealerMailNumber = txtNomorSurat.Text

        For Each item As PartIncidentalDetail In objPartIncidentalHeader.PartIncidentalDetails
            item.ChassisNumber = objPartIncidentalHeader.ChassisNumber
            'item.AssemblyYear = lblTahunProduksi.Text
            If txtYearProduction.Text = strNA Then
                item.AssemblyYear = 1980
            Else
                item.AssemblyYear = txtYearProduction.Text
            End If
        Next

        If objPartIncidentalHeader.RequestNumber = String.Empty Then
            objPartIncidentalHeader.RequestNumber = GenerateRequestNumber()
        End If
        If rblStatus.SelectedIndex = 2 Then
            objPartIncidentalHeader.Status = txtStatusLain.Text
        Else
            objPartIncidentalHeader.Status = rblStatus.SelectedItem.ToString
        End If

    End Sub

    Private Sub SaveToDatabase()
        Dim int As Integer = New PartIncidentalHeaderFacade(User).Insert(objPartIncidentalHeader)
        If int > 0 Then
            RefreshData(int)
        End If
    End Sub

    Private Sub RefreshData(ByVal id As Integer)
        objPartIncidentalHeader = New PartIncidentalHeaderFacade(User).Retrieve(id)
        If Not IsNothing(objPartIncidentalHeader) Then
            sessionHelper.SetSession("Part", objPartIncidentalHeader)
            BindHeaderToForm()
            BindDetailToGrid()
            MessageBox.Show("Data Berhasil Disimpan")
        Else
            MessageBox.Show("Proses Simpan Gagal")
        End If
    End Sub

    Private Function ValidateItem(ByVal kodeBarang As String, ByVal Unit As String, ByVal chassisNum As String, ByVal assemblyYear As String) As Boolean
        'If (kodeBarang = String.Empty Or Unit = String.Empty Or chassisNum = String.Empty Or assemblyYear = String.Empty) Then
        If (kodeBarang = String.Empty Or Unit = String.Empty) Then
            lblError.Text = "Error : Nomor Barang dan Jumlah Tidak boleh Kosong"
            Return False
        Else
            Dim ObjSparePartMaster As SparePartMaster = New SparePartMasterFacade(User).Retrieve(kodeBarang.Trim)
            If (ObjSparePartMaster.ID = 0) Then
                lblError.Text = "Error : SparePart Tidak Ditemukan"
                Return False
            ElseIf ObjSparePartMaster.TypeCode = "I" Or ObjSparePartMaster.TypeCode = "E" Or ObjSparePartMaster.TypeCode = "A" Then
                lblError.Text = "Error : Sparepart dengan stop mark I, E, A tidak bisa dipesan"
                Return False
            End If

        End If
        Return True
    End Function

    Private Function ValidateDuplication(ByVal kodeBarang As String, ByVal Mode As String, ByVal Rowindex As Integer) As Boolean
        Try
            If (Mode = "Add") Then
                For Each item As PartIncidentalDetail In objPartIncidentalHeader.PartIncidentalDetails
                    If (item.SparePartMaster.PartNumber = kodeBarang.Trim) Then
                        lblError.Text = "Error : Duplikasi Kode Barang"
                        Return False
                    End If
                Next
            Else
                Dim i As Integer = 0
                For Each item As PartIncidentalDetail In objPartIncidentalHeader.PartIncidentalDetails
                    If (item.SparePartMaster.PartNumber = kodeBarang.Trim) Then
                        If i <> Rowindex Then
                            lblError.Text = "Error : Duplikasi Kode Barang"
                            Return False
                        End If
                    End If
                    i = i + 1
                Next
            End If
            Return True
        Catch ex As Exception
            lblError.Text = "Error : Part Incedental tidak ditemukan."
            Return False
        End Try

    End Function

    Private Sub SearchPartIncidentalHeaderAndDetail()
        objPartIncidentalHeader = New PartIncidentalHeaderFacade(User).Retrieve(CInt(PartIncidentalHeaderID))
        sessionHelper.SetSession("Part", objPartIncidentalHeader)
        objDealer = objPartIncidentalHeader.Dealer
    End Sub

    Private Sub SetMode()
        If PartIncidentalHeaderID = String.Empty Then
            btnKembali.Visible = False
            Mode = enumMode.Mode.NewItemMode
            SetButtonNewMode()
            ViewState("Mode") = Mode
            sessionHelper.RemoveSession("Part")
            objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        Else
            btnKembali.Visible = True
            SearchPartIncidentalHeaderAndDetail()
            Mode = enumMode.Mode.EditMode
            SetButtonEditMode()
            ViewState("Mode") = Mode
        End If
    End Sub

    Private Function NoSuratIsValid() As Boolean
        Dim i As Integer = New PartIncidentalHeaderFacade(User).ValidateNoSurat(txtNomorSurat.Text)
        If i > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        'If NoSuratIsValid() Then
        If IntiCalendar1.Value.Date >= DateTime.Now.Date Then
            objPartIncidentalHeader = sessionHelper.GetSession("Part")
            dtgPartIncidental.EditItemIndex = -1
            dtgPartIncidental.ShowFooter = True

            If Not (objPartIncidentalHeader.PartIncidentalDetails.Count = 0) Then
                Mode = ViewState("Mode")
                RefreshChassisInfo()
                BindDataToObject()
                If Mode = enumMode.Mode.NewItemMode Then
                    SaveToDatabase()
                    Mode = enumMode.Mode.EditMode
                    ViewState("Mode") = Mode
                    SetButtonEditMode()
                Else
                    Dim objPartIncidentalHeaderFacade As New PartIncidentalHeaderFacade(User)
                    objPartIncidentalHeaderFacade.Update(objPartIncidentalHeader)
                    RefreshData(objPartIncidentalHeader.ID)
                End If
            Else
                MessageBox.Show("Belum ada Detail")
            End If
        Else
            MessageBox.Show("Tanggal Input Tidak Valid")
        End If
        'Else
        '    MessageBox.Show("Duplikasi Nomor Surat")
        'End If

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            objPartIncidentalHeader = sessionHelper.GetSession("Part")
            If Not (objPartIncidentalHeader Is Nothing) Then
                If (objPartIncidentalHeader.ID <> 0) Then
                    Dim objPartIncidentailHeaderFacade As New PartIncidentalHeaderFacade(User)
                    objPartIncidentailHeaderFacade.Delete(objPartIncidentalHeader)
                End If
                sessionHelper.RemoveSession("Part")
                BindDataToPage()
                Mode = enumMode.Mode.NewItemMode
                SetButtonNewMode()
                ViewState("Mode") = Mode
            End If
        Catch ex As Exception
            MessageBox.Show("Proses Hapus Gagal")
        End Try

    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        If Not sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessionHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If
    End Sub

    Private Sub lnkbtnCheckChassis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnCheckChassis.Click
        RefreshChassisInfo()
        'RefreshGrid()
    End Sub

    Private Sub RefreshChassisInfo()
        Dim oChassisFacade As New ChassisMasterFacade(User)
        Dim objChassisMaster As ChassisMaster

        If oChassisFacade.IsExist(txtNoRangka.Text) Then
            txtNoRangka.ForeColor = Color.Black
            objChassisMaster = oChassisFacade.Retrieve(txtNoRangka.Text)
            lblTahunProduksi.Visible = False
            lblTahunProduksi.Text = objChassisMaster.ProductionYear
            txtYearProduction.Enabled = False
            txtYearProduction.Text = objChassisMaster.ProductionYear
            lblVehicleType.Text = objChassisMaster.VechileColor.VechileType.Description
            'LoadChassisInfo(CType(oChassisFacade.Retrieve(txtNoChasis.Text), ChassisMaster))
        Else
            ' Modified by Ikhsan, 20081120
            ' Requested by Yurike
            ' To add validation for chassismaster, if the chassismaster is not exists then
            ' another validation for the data's length, then check the Vehicle type code
            ' Start ----------------------------------------------------------------------
            If txtNoRangka.Text.ToString.Length >= 15 Then

                txtNoRangka.ForeColor = Color.Black
                lblTahunProduksi.Visible = False
                lblTahunProduksi.Text = strNA
                txtYearProduction.Enabled = False
                txtYearProduction.Text = strNA
                lblVehicleType.Text = txtNoRangka.Text.ToString.Substring(3, 4)

                'If CheckVehicleType(txtNoRangka.Text.ToString.Substring(3, 4)) Then
                '    txtNoRangka.ForeColor = Color.Black
                '    lblTahunProduksi.Visible = False
                '    lblTahunProduksi.Text = "1980"
                '    txtYearProduction.Enabled = False
                '    txtYearProduction.Text = "1980"
                '    lblVehicleType.Text = txtNoRangka.Text.ToString.Substring(3, 4)
                'Else
                '    txtNoRangka.ForeColor = Color.Red
                '    'txtYearProduction.Text = ""
                '    txtYearProduction.Enabled = True
                '    lblTahunProduksi.Visible = False
                '    lblTahunProduksi.Text = ""
                '    lblVehicleType.Text = ""
                '    txtYearProduction.Text = ""
                '    'ClearChassisInfo()
                '    'MessageBox.Show("No Rangka tidak terdaftar.")
                'End If
            Else
                txtNoRangka.ForeColor = Color.Red
                txtYearProduction.Text = strNA
                txtYearProduction.Enabled = False
                lblTahunProduksi.Visible = False
                lblTahunProduksi.Text = strNA
                lblVehicleType.Text = ""
                'ClearChassisInfo()
                'MessageBox.Show("No Rangka tidak terdaftar.")
            End If
            ' End  ----------------------------------------------------------------------
        End If
    End Sub

    Private Function CheckVehicleType(ByVal VehicleType As String) As Boolean

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "VechileTypeCode", MatchType.Exact, VehicleType))

        Dim ObjVehicleType As VechileType = New VechileTypeFacade(User).RetrieveByCriteria(criterias)

        If ObjVehicleType.ID >= 1 Then
            Return True
        Else
            Return False
        End If

    End Function
End Class
