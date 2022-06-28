#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Drawing.Color
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessValidation
Imports System.Text
Imports KTB.DNet.Security
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
#End Region

Public Class FrmEntryPDI
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblKM As System.Web.UI.WebControls.Label
    Protected WithEvents LblTglServis As System.Web.UI.WebControls.Label
    Protected WithEvents txtDisclaimer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtChassisMaster As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEngineNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTglPDI As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents btnRelease As System.Web.UI.WebControls.Button

    Protected WithEvents txtDealerBranchCode As TextBox
    Protected WithEvents lblDealerBranch As Label
    Protected WithEvents lblPopUpDealerBranch As Label
    Protected WithEvents txtBranchName As TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents dgPDIEntry As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlPDIKind As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnBatal As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnRilis As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Private _sessHelper As SessionHelper = New SessionHelper
    Private m_bPDIDataRelease_Privilege As Boolean = False
    Private m_bPDIDataUpdate_Privilege As Boolean = False
    Private m_bPDIDataSave_Privilege As Boolean = False
    Private ObjDealer As Dealer

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        ObjDealer = CType(Session.Item("DEALER"), Dealer)
    End Sub

#End Region

#Region "Custom Variable Declaration"
    'Dim total, dealerName As String
    'Dim totRow As Integer
    Private isDealerPiloting As Boolean = False
    Dim disclaimer As String = New AppConfigFacade(User).Retrieve("PDI-Disclaimer").Value

    '-- Generate timestamp untuk nama file FSData[timestamp].txt
    Dim dt As DateTime = DateTime.Now
    Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
#End Region

#Region "Custom Method"

    Private Sub BindDdlPDIKind()
        Dim al As ArrayList = New EnumPDIKind().RetrievePDIKind
        For i As Integer = 0 To al.Count - 1
            If al(i).NamePDIKind <> "B" AndAlso al(i).NamePDIKind <> "C" _
                AndAlso al(i).NamePDIKind <> "D" Then  '---> add by rudi

                ddlPDIKind.Items.Add(New ListItem(al(i).NamePDIKind, al(i).NamePDIKind))
            End If

        Next
        ddlPDIKind.Items.Insert(0, New ListItem("", ""))
    End Sub

    Private Sub BindDatagrid(ByVal indexPage)
        Dim totRow As Integer = 0
        If Not IsNothing(CType(Session.Item("sessDealer"), Dealer)) Then
            Dim TmpObjDealer As Dealer = CType(Session.Item("sessDealer"), Dealer)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Baru, String)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "Dealer.ID", MatchType.Exact, CType(TmpObjDealer.ID, Integer)))

            _sessHelper.SetSession("SortViewPDI", criterias)
            dgPDIEntry.DataSource = New PDIFacade(User).RetrieveActiveList(criterias, indexPage + 1, dgPDIEntry.PageSize, totRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgPDIEntry.VirtualItemCount = totRow

            Dim al As ArrayList = dgPDIEntry.DataSource
            _sessHelper.SetSession("SessArrPDI", dgPDIEntry.DataSource)
            If al.Count > 0 Then
                btnRelease.Enabled = True
                btnRilis.Disabled = False
            Else
                btnRelease.Enabled = False
                btnRilis.Disabled = True

            End If
            dgPDIEntry.DataBind()
        End If
    End Sub

    Private Sub InitiatePage()
        ViewState("currentSortColumn") = "ID"
        ViewState("currentSortDirection") = Sort.SortDirection.DESC
    End Sub

    Private Sub DeletePDI(ByVal nID As Integer)
        Dim objPDIFacade As PDIFacade = New PDIFacade(User)
        Dim objPDI As PDI = New PDIFacade(User).Retrieve(nID)
        objPDIFacade.DeleteFromDB(objPDI)
        BindDatagrid(0)
    End Sub

    Private Sub ViewPDI(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objPDI As PDI = New PDIFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsObjPDI", objPDI)

        txtChassisMaster.Text = objPDI.ChassisMaster.ChassisNumber
        txtEngineNo.Text = objPDI.ChassisMaster.EngineNumber
        txtWONumber.Text = objPDI.WorkOrderNumber
        If Not IsNothing(objPDI.DealerBranch) Then
            txtDealerBranchCode.Text = objPDI.DealerBranch.DealerBranchCode
            txtBranchName.Text = objPDI.DealerBranch.Name
        End If

        ddlPDIKind.SelectedValue = objPDI.Kind

        If IsNothing(objPDI.PDIDate) Or objPDI.PDIDate = "1/1/1900" Then
            txtTglPDI.Text = ""
        Else
            txtTglPDI.Text = Format(objPDI.PDIDate, "ddMMyyyy")
        End If

        Me.btnSave.Enabled = EditStatus
    End Sub

    Private Sub ClearData()
        txtChassisMaster.Text() = String.Empty
        txtEngineNo.Text() = String.Empty
        txtTglPDI.Text() = String.Empty
        txtWONumber.Text() = String.Empty

        txtChassisMaster.Enabled = True
        txtChassisMaster.ReadOnly = False
        txtEngineNo.ReadOnly = False
        txtTglPDI.ReadOnly = False
        txtWONumber.ReadOnly = False

        ddlPDIKind.Enabled = False
        ddlPDIKind.SelectedValue = "A"

        txtDealerBranchCode.Text() = String.Empty
        txtBranchName.Text() = String.Empty
        dgPDIEntry.SelectedIndex = -1

        btnSave.Enabled = True
        ViewState.Add("vsProcess", "Insert")

    End Sub

    Private Sub ClearDataAfterSaving()
        txtChassisMaster.Text() = String.Empty
        txtEngineNo.Text() = String.Empty
        txtTglPDI.Text() = String.Empty
        txtWONumber.Text() = String.Empty

        txtChassisMaster.ReadOnly = False
        txtEngineNo.ReadOnly = False
        txtTglPDI.ReadOnly = False
        txtWONumber.ReadOnly = False

        ddlPDIKind.Enabled = True
        ddlPDIKind.SelectedValue = ""

        btnSave.Enabled = True
        ViewState.Add("vsProcess", "Insert")

    End Sub

    Private Sub InsertobjPDI(ByVal objPDI As PDI)
        Dim objPDIFacade As PDIFacade = New PDIFacade(User)
        Dim nResult As Integer = -1
        Dim bCheck As Boolean = True
        Dim objChasisMaster As ChassisMaster
        Dim objDealer As Dealer
        Dim objDealerBranch As DealerBranch

        If Not txtChassisMaster.Text = String.Empty Then

            'ambil dealer dulu
            If bCheck Then
                Dim critDealer As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
                critDealer.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strDealerCode))
                Dim DealerColl As ArrayList = New DealerFacade(User).Retrieve(critDealer)
                If DealerColl.Count > 0 Then
                    objDealer = CType(DealerColl(0), Dealer)
                Else
                    MessageBox.Show("Kode Dealer tidak terdaftar ")
                    bCheck = False
                End If
            End If

            'ambil dealer branch
            If (bCheck And (txtDealerBranchCode.Text.Trim() <> String.Empty)) Then
                objDealerBranch = New DealerBranchFacade(User).Retrieve(txtDealerBranchCode.Text)
                If IsNothing(objDealerBranch) Then
                    MessageBox.Show("Kode Cabang tidak terdaftar ")
                    bCheck = False
                End If
            End If

            'ambil chassis master
            If bCheck Then
                Dim critChassisMaster As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critChassisMaster.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, CType(txtChassisMaster.Text, String)))
                critChassisMaster.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, CType(txtEngineNo.Text, String)))
                Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(critChassisMaster)
                If ChassisColl.Count > 0 Then
                    objChasisMaster = CType(ChassisColl(0), ChassisMaster)
                Else
                    MessageBox.Show("No. Rangka tidak terdaftar ! atau No Mesin tidak sesuai")
                    bCheck = False
                End If
            End If

            'periksa Jenis PDI
            If bCheck Then
                If ddlPDIKind.SelectedValue = "" Then
                    MessageBox.Show("Jenis PDI masih kosong")
                    bCheck = False
                End If
            End If

            'validasi tgl PDI
            If bCheck Then
                If txtTglPDI.Text <> "" Then
                    If Len(txtTglPDI.Text) = 8 Then
                        If IsValidDate(txtTglPDI.Text) Then
                            objPDI.PDIDate = ToDate(txtTglPDI.Text)
                        Else
                            MessageBox.Show("Format tgl. PDI salah")
                            bCheck = False
                        End If
                    Else
                        bCheck = False
                        MessageBox.Show("Format tgl. PDI salah")
                    End If
                Else
                    bCheck = False
                    MessageBox.Show("Tgl. PDI kosong")
                End If
            End If

            'start take out request dari miyuki 14/01/2020
            'Validasi tglPDI & tglFaktur
            'If bCheck Then
            '    If Not IsNothing(objChasisMaster.EndCustomer) Then
            '        'Remark by yuki
            '        'If objChasisMaster.EndCustomer.FakturDate = "1900-01-01 00:00:00.000" Then
            '        '    MessageBox.Show("Tanggal Faktur kosong")
            '        '    bCheck = False
            '        'End If
            '        'If objPDI.PDIDate < objChasisMaster.EndCustomer.FakturDate Then
            '        '    MessageBox.Show("Tgl PDI < Tgl Faktur ")
            '        '    bCheck = False
            '        'End If

            '        'start take out request dari miyuki 14/01/2020
            '        'If objChasisMaster.EndCustomer.OpenFakturDate.Year <= 1900 Then
            '        '    MessageBox.Show("Tanggal Buka Faktur kosong")
            '        '    bCheck = False
            '        'End If
            '        'end take out request dari miyuki 14/01/2020
            '        If objPDI.PDIDate < objChasisMaster.EndCustomer.OpenFakturDate AndAlso objChasisMaster.EndCustomer.OpenFakturDate.Year > 1900 Then
            '            MessageBox.Show("Tgl PDI < Tgl Buka Faktur ")
            '            bCheck = False
            '        End If

            '        If objChasisMaster.FakturStatus <> "4" Then
            '            MessageBox.Show("Nomor chassis belum pengajuan konsumen")
            '            bCheck = False
            '        End If
            '    Else
            '        MessageBox.Show("Faktur tidak ditemukan")
            '        bCheck = False
            '    End If
            'End If
            'end take out request dari miyuki 14/01/2020

            'Validasi Retur
            If bCheck AndAlso Not IsNothing(objChasisMaster) AndAlso objChasisMaster.isValidToCreateFaktur() = False Then 'Add Filter on 20130104 by dna for angga
                MessageBox.Show("Chassis " & objChasisMaster.ChassisNumber & " Sedang diretur")
                bCheck = False
            End If
            '
            If bCheck Then
                If Not IsNothing(objDealer) And Not IsNothing(objChasisMaster) Then
                    'Modifikasi karena Change Request dari hasil UAT
                    'Valdasi Dealer yang berhak PDI

                    'Berdasarkan CRF Tanggal 23-02-06 permintaan validasi di atas di hilangkan kembali
                    'If objDealer.ID = objChasisMaster.Dealer.ID Then

                    objPDI.ChassisMaster = objChasisMaster
                    If Not IsExistCodeForInsert(objPDI.ChassisMaster.ID) Then

                        'Jika ok isi dulu yang masih diperlukan
                        objPDI.Dealer = objDealer
                        objPDI.Kind = ddlPDIKind.SelectedValue
                        objPDI.PDIStatus = EnumFSStatus.FSStatus.Baru
                        objPDI.WorkOrderNumber = txtWONumber.Text.Trim()
                        If Not IsNothing(objDealerBranch) Then
                            objPDI.DealerBranch = objDealerBranch
                        End If

                        'lalu insert
                        nResult = objPDIFacade.Insert(objPDI)
                        If nResult = -1 Then
                            MessageBox.Show("Simpan Gagal")
                        Else
                            MessageBox.Show("Simpan Sukses")
                            'Todo session
                            Session.Add("vsObjPDI", objPDI)
                            ClearDataAfterSaving()
                        End If
                    Else
                        MessageBox.Show("No. Rangka tersebut telah PDI ")
                    End If

                    'Else
                    '    MessageBox.Show("Dealer Tidak Berhak PDI ")
                    'End If


                End If
            End If
        Else
            MessageBox.Show("No Rangka Tidak boleh kosong !")
        End If

    End Sub

    Private Sub UpdateObjPDI(ByVal objPDI As PDI)
        Dim objPDIFacade As PDIFacade = New PDIFacade(User)
        Dim nResult As Integer = -1
        Dim bCheck As Boolean = True
        Dim objChasisMaster As ChassisMaster
        Dim objDealer As Dealer
        Dim objDealerBranch As DealerBranch

        If Not txtChassisMaster.Text = String.Empty Then

            'periksa dealer branch
            If (bCheck And (txtDealerBranchCode.Text.Trim() <> String.Empty)) Then
                objDealerBranch = New DealerBranchFacade(User).Retrieve(txtDealerBranchCode.Text)
                If IsNothing(objDealerBranch) Then
                    MessageBox.Show("Kode Cabang tidak terdaftar ")
                    bCheck = False
                End If
            End If

            'periksa Jenis PDI
            If bCheck Then
                If ddlPDIKind.SelectedValue = "" Then
                    MessageBox.Show("Jenis PDI masih kosong")
                    bCheck = False
                End If
            End If

            'validasi tgl PDI
            If bCheck Then
                If txtTglPDI.Text <> "" Then
                    If Len(txtTglPDI.Text) = 8 Then
                        If IsValidDate(txtTglPDI.Text) Then
                            objPDI.PDIDate = ToDate(txtTglPDI.Text)
                        Else
                            MessageBox.Show("Format tgl. PDI salah")
                            bCheck = False
                        End If
                    Else
                        bCheck = False
                        MessageBox.Show("Format tgl. PDI salah")
                    End If
                Else
                    bCheck = False
                    MessageBox.Show("Tgl. PDI kosong")
                End If
            End If

            'start take out request dari miyuki 14/01/2020
            'Validasi tglPDI & tglFaktur
            'If bCheck Then
            '    If Not IsNothing(objPDI.ChassisMaster.EndCustomer) Then
            '        'If objPDI.ChassisMaster.EndCustomer.FakturDate = "1900-01-01 00:00:00.000" Then
            '        '    MessageBox.Show("Tanggal Faktur kosong")
            '        '    bCheck = False
            '        'End If

            '        'If objPDI.PDIDate < objPDI.ChassisMaster.EndCustomer.FakturDate Then
            '        '    MessageBox.Show("Tgl PDI < Tgl Faktur ")
            '        '    bCheck = False
            '        'End If
            '        
            '        If objPDI.ChassisMaster.EndCustomer.OpenFakturDate.Year <= 1900 Then
            '            MessageBox.Show("Tanggal Buka Faktur kosong")
            '            bCheck = False
            '        End If
            '        
            '        If objPDI.PDIDate < objPDI.ChassisMaster.EndCustomer.OpenFakturDate AndAlso objPDI.ChassisMaster.EndCustomer.OpenFakturDate.Year > 1900 Then
            '            MessageBox.Show("Tgl PDI < Tgl Buka Faktur ")
            '            bCheck = False
            '        End If
            '        If objPDI.ChassisMaster.FakturStatus <> "4" Then
            '            MessageBox.Show("Nomor chassis belum pengajuan konsumen")
            '            bCheck = False
            '        End If
            '    Else
            '        MessageBox.Show("Faktur tidak ditemukan")
            '        bCheck = False
            '    End If
            'End If
            'end take out request dari miyuki 14/01/2020
            If bCheck Then
                'objPDI.ChassisMaster = objChasisMaster
                If Not IsExistCodeForUpdate(objPDI.ID, objPDI.ChassisMaster.ID) Then

                    'Jika ok isi dulu yang masih diperlukan
                    objPDI.Kind = ddlPDIKind.SelectedValue
                    objPDI.PDIStatus = EnumFSStatus.FSStatus.Baru
                    objPDI.DealerBranch = objDealerBranch
                    objPDI.WorkOrderNumber = txtWONumber.Text.Trim()

                    'lalu update
                    nResult = objPDIFacade.Update(objPDI)
                    If nResult = -1 Then
                        MessageBox.Show("Update Gagal")
                    Else
                        MessageBox.Show("Update Sukses")
                        ClearData()
                    End If
                Else
                    MessageBox.Show("No. Rangka tersebut telah PDI ")
                End If
            End If
        Else
            MessageBox.Show("No Rangka Tidak boleh kosong")
        End If

    End Sub

    Private Function IsValidDate(ByVal strdate As String) As Boolean
        'Dim strtgl As String = strdate.Substring(2, 2).ToString & "-" & strdate.Substring(0, 2) & "-" & strdate.Substring(4, 4)
        'yang jadi dipakai adalah setting tanggal indonesia
        Dim strtgl As String = strdate.Substring(0, 2).ToString & "-" & strdate.Substring(2, 2) & "-" & strdate.Substring(4, 4)
        If IsDate(strtgl) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ToDate(ByVal strdate As String) As Date
        'Return CType(strdate.Substring(2, 2).ToString & "-" & strdate.Substring(0, 2) & "-" & strdate.Substring(4, 4), Date)
        'yang jadi dipakai adalah setting tanggal indonesia
        Return CType(strdate.Substring(0, 2).ToString & "-" & strdate.Substring(2, 2) & "-" & strdate.Substring(4, 4), Date)
    End Function

    Private Function IsExistCodeForInsert(ByVal ChassisID As Integer) As Boolean
        Dim objPDIFacade As PDIFacade = New PDIFacade(User)
        'Periksa agar tidak ada key ganda 
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.ID", MatchType.Exact, ChassisID))
        Dim TestExist As ArrayList = New PDIFacade(User).Retrieve(criterias)
        If TestExist.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsExistCodeForUpdate(ByVal PDIId As Integer, ByVal ChassisID As Integer) As Boolean
        Dim objPDIFacade As PDIFacade = New PDIFacade(User)
        'Periksa agar tidak ada key ganda 
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.ID", MatchType.Exact, ChassisID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "PDIStatus", MatchType.Exact, EnumFSStatus.FSStatus.Baru))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ID", MatchType.No, PDIId))
        Dim TestExist As ArrayList = New PDIFacade(User).Retrieve(criterias)
        If TestExist.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function



    Private Function GetCheckedPDIItem(ByRef message As String) As ArrayList
        'COm by me dgPDIEntry.DataSource = CType(Session("SessArrPDI"), ArrayList)
        Dim objPDIFacade As PDIFacade = New PDIFacade(User)
        Dim arlCheckedPDIItem As ArrayList = New ArrayList
        Dim invalidChassis As List(Of String) = New List(Of String)
        Dim nIndeks As Integer
        Dim ds As DataSet
        isDealerPiloting = TCHelper.GetActiveTCResult(ObjDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingPDI))

        For Each dtgItem As DataGridItem In dgPDIEntry.Items
            nIndeks = dtgItem.ItemIndex
            Dim objPDI As PDI = CType(CType(dgPDIEntry.DataSource, ArrayList)(nIndeks), PDI)
            If CType(dtgItem.Cells(2).FindControl("cbSelect"), CheckBox).Checked Then
                If Not IsNothing(objPDI) Then
                    If objPDI.PDIStatus = CType(EnumFSStatus.FSStatus.Baru, String) And isDealerPiloting Then
                        ' objPDI.PDIStatus = CType(EnumFSStatus.FSStatus.Proses, String)
                        ds = objPDIFacade.RetrieveGetPDIInfoTemplate(objPDI.ChassisMaster.ID)
                        If ds.Tables(0).Rows.Count = 0 Then
                            invalidChassis.Add(objPDI.ChassisMaster.ChassisNumber)
                        Else
                            arlCheckedPDIItem.Add(objPDI)
                        End If
                    Else
                        arlCheckedPDIItem.Add(objPDI)
                    End If
                End If
            End If
        Next
        If invalidChassis.Count > 0 Then
            message = String.Format("Sertifikat PDI untuk Chassis Number [{0}] belum memiliki Master Data PDI Template. Mohon untuk melakukan input terlebih dahulu.", String.Join(",", invalidChassis))
        End If
        Return arlCheckedPDIItem
    End Function

    Private Sub bindGridSorting(ByVal indexPage As Integer)

        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then

            dgPDIEntry.DataSource = New PDIFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("SortViewPDI"), CriteriaComposite), indexPage + 1, dgPDIEntry.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgPDIEntry.VirtualItemCount = totalRow
            dgPDIEntry.DataBind()

        End If

    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            If Not IsNothing(Session("DEALER")) Then
                InitiatePage()

                ''CR Tutup Menu
                '' by ali
                '' 2014 - 09 -30
                If (DateTime.Now >= New DateTime(2014, 10, 1) AndAlso DateTime.Now <= New DateTime(2014, 10, 11).AddMinutes(-1) AndAlso ObjDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    Dim MSgClose As String = IIf(Not IsNothing(KTB.DNet.Lib.WebConfig.GetValue("CloseMessage")), KTB.DNet.Lib.WebConfig.GetValue("CloseMessage"), "Module ini sedang di tutup, sampai dengan 10 Oktober 2014")
                    Server.Transfer("../ClossingMessage.htm")
                End If
                ''END CR Tutup Menu
                Dim StrDealerCode As String = ObjDealer.DealerCode
                lblDealerCode.Text = ObjDealer.DealerCode + " / " + ObjDealer.SearchTerm1
                lblDealerName.Text = ObjDealer.DealerName
                _sessHelper.SetSession("sessDealer", ObjDealer)
                _sessHelper.SetSession("sessDealerLogin", ObjDealer.DealerCode)

                'isDealerPiloting = TCHelper.GetActiveTCResult(ObjDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingPDI))

                ViewState.Add("vsProcess", "Insert")
                BindDdlPDIKind()
                BindDatagrid(0)
                txtChassisMaster.Attributes.Add("onkeydown", "enter(document.all.ddlPDIKind)")
                ddlPDIKind.Attributes.Add("onkeydown", "enter(document.all.txtTglPDI)")
                txtTglPDI.Attributes.Add("onkeydown", "enter(document.all.btnSimpan)")
                btnSimpan.Attributes.Add("onkeydown", "enter(document.all.txtChassisMaster)")
                lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
                txtDealerBranchCode.Attributes.Add("ReadOnly", "ReadOnly")
                txtDisclaimer.Attributes.Add("ReadOnly", "ReadOnly")
                txtBranchName.Attributes.Add("ReadOnly", "ReadOnly")
                ddlPDIKind.SelectedValue = "A"
                ddlPDIKind.Enabled = False
                txtDisclaimer.Text = disclaimer
            Else
                'Response.Redirect("../SessionExpired.htm")
            End If
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.PDIDataView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PDI - Data PDI")
        End If

        'PDIDataUpdate_Privilege
        m_bPDIDataUpdate_Privilege = SecurityProvider.Authorize(Context.User, SR.PDIDataUpdate_Privilege)

        'PDIDataSave_Privilege
        m_bPDIDataSave_Privilege = SecurityProvider.Authorize(Context.User, SR.PDIDataSave_Privilege)

        'set control btnsave and btnbatal
        If m_bPDIDataSave_Privilege Or m_bPDIDataUpdate_Privilege Then
            btnSimpan.Visible = True
            btnBatal.Visible = True
        Else
            btnSimpan.Visible = False
            btnBatal.Visible = False
        End If
        'PDIDataRelease_Privilege
        m_bPDIDataRelease_Privilege = SecurityProvider.Authorize(Context.User, SR.PDIDataRelease_Privilege)
        btnRilis.Visible = m_bPDIDataRelease_Privilege
    End Sub


    Private Sub dgPDIEntry_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPDIEntry.PageIndexChanged
        dgPDIEntry.SelectedIndex = -1
        dgPDIEntry.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dgPDIEntry.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dgPDIEntry_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPDIEntry.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As PDI = CType(e.Item.DataItem, PDI)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgPDIEntry.CurrentPageIndex * dgPDIEntry.PageSize) 'getDataGridItemIndex()
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                e.Item.FindControl("lbtnDelete").Visible = m_bPDIDataUpdate_Privilege
                Dim strMsg As String = "Yakin Data dengan nomor rangka " & CType(e.Item.FindControl("lblChassisNo"), Label).Text & "  ini akan dihapus? "
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & strMsg & "');")
            End If
            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                e.Item.FindControl("lbtnEdit").Visible = m_bPDIDataUpdate_Privilege
            End If
            Dim lblDealerBranch As Label = CType(e.Item.FindControl("lblDealerBranch"), Label)
            If Not IsNothing(RowValue.DealerBranch) Then
                lblDealerBranch.Text = RowValue.DealerBranch.DealerBranchCode
            Else
                lblDealerBranch.Text = RowValue.DealerBranchCodeMsg
            End If
            'If Not e.Item.FindControl("cbSelect") Is Nothing Then
            '    e.Item.FindControl("cbSelect").Visible = m_bPDIDataRelease_Privilege
            'End If
        End If
    End Sub

    Private Sub dgPDIEntry_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPDIEntry.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtChassisMaster.ReadOnly = True
            ddlPDIKind.Enabled = False
            txtTglPDI.ReadOnly = True
            ViewPDI(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewPDI(e.Item.Cells(0).Text, True)
            dgPDIEntry.SelectedIndex = e.Item.ItemIndex
            txtChassisMaster.Enabled = False
            txtWONumber.Enabled = True
            ddlPDIKind.Enabled = False
            txtTglPDI.Enabled = True
            txtChassisMaster.ReadOnly = True
            txtTglPDI.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            Try
                DeletePDI(e.Item.Cells(0).Text)
                MessageBox.Show("Hapus Sukses !")
            Catch ex As Exception
                MessageBox.Show("Gagal Menghapus !")
            End Try
            dgPDIEntry.SelectedIndex = -1
            ClearData()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearData()
    End Sub

    Private Function GetProductCategoryCode(ByVal aPDIs As ArrayList) As String
        Dim product As String = ""

        Try
            For Each oPDI As PDI In aPDIs
                If product = "" Then
                    product = oPDI.ChassisMaster.Category.ProductCategory.Code
                Else
                    If product <> oPDI.ChassisMaster.Category.ProductCategory.Code Then
                        Return ""
                    End If
                End If
            Next
        Catch ex As Exception

        End Try

        Return product
    End Function

    Private Sub btnRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        Dim bcheck As Boolean = False
        Dim PDIFileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\PDI\" & "PDIData" & sSuffix & ".txt"
        'Dim PDIFileName As String = Server.MapPath("") & "\..\DataTemp\PDIData" & sSuffix & ".txt"
        dgPDIEntry.DataSource = CType(Session("SessArrPDI"), ArrayList)
        For Each dtgItem As DataGridItem In dgPDIEntry.Items
            If CType(dtgItem.Cells(0).FindControl("cbSelect"), CheckBox).Checked Then
                bcheck = True
                Exit For
            End If
        Next
        If bcheck Then
            Dim CheckedPDIItemColl As ArrayList = New ArrayList
            Dim message As String = String.Empty
            CheckedPDIItemColl = GetCheckedPDIItem(message)
            If Not String.IsNullorEmpty(message) Then
                MessageBox.Show(message)
                Exit Sub
            End If
            Dim IsReleased As Boolean = Me.ReleaseToSAP(CheckedPDIItemColl)

            If IsReleased Then
                GenerateDocument(CheckedPDIItemColl)
                BindDatagrid(dgPDIEntry.CurrentPageIndex)

                Dim strScript As String
                strScript = "<script>document.all.txtChassisMaster.focus();</script>"
                Page.RegisterStartupScript("", strScript)
            End If
        End If
    End Sub

    Private Function GetSuffix() As String
        Return DateTime.Now.ToString("yyyyMMddHHmmss")
    End Function

    Private Function ReleaseToSAP(ByVal aPDIs As ArrayList) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim aPDIAs As New ArrayList
        Dim aPDIBs As New ArrayList
        Dim PDIFileName As String
        Dim IsReleased As Boolean = False

        'Splitting
        For Each oPDI As PDI In aPDIs
            If oPDI.ChassisMaster.VechileColor.VechileType.Category.ProductCategory.Code.ToLower.Trim = "mmc" Then
                aPDIAs.Add(oPDI)
            Else
                aPDIBs.Add(oPDI)
            End If
        Next
        'Transfering to SAP
        Try
            success = imp.Start
            'success = True
            If aPDIs.Count > 0 AndAlso success Then
                If aPDIAs.Count > 0 Then
                    PDIFileName = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\PDI\" & "PDIData" & Me.GetSuffix() & "_" & CType(aPDIAs(0), PDI).ChassisMaster.VechileColor.VechileType.Category.ProductCategory.Code.ToLower() & ".txt"
                    AppendText(aPDIAs, PDIFileName)
                End If
                If aPDIBs.Count > 0 Then
                    PDIFileName = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\PDI\" & "PDIData" & Me.GetSuffix() & "_" & CType(aPDIBs(0), PDI).ChassisMaster.VechileColor.VechileType.Category.ProductCategory.Code.ToLower() & ".txt"
                    AppendText(aPDIBs, PDIFileName)
                End If
                Dim objPDIColl As ArrayList = New ArrayList
                For Each ObjPDI As PDI In aPDIs
                    ObjPDI.ReleaseBy = User.Identity.Name
                    ObjPDI.ReleaseDate = Today
                    ObjPDI.PDIStatus = CType(EnumFSStatus.FSStatus.Proses, String)
                    objPDIColl.Add(ObjPDI)
                Next
                Dim nResult = New PDIFacade(User).UpdatePDICollection(objPDIColl)
                If nResult = 0 Then
                    IsReleased = True
                    MessageBox.Show("Update Rilis Sukses")
                Else
                    MessageBox.Show("Update Rilis gagal")
                End If
            Else
                MessageBox.Show("Gagal Melakukan Proses Rilis..")
            End If
            imp.StopImpersonate()
            imp = Nothing
        Catch ex As Exception
            MessageBox.Show("Gagal Melakukan Proses Rilis.")
        End Try
        Return IsReleased
    End Function


    Private Sub AppendText(ByVal ArrCheckedPDIItem As ArrayList, ByVal filename As String)
        Dim strText As New StringBuilder
        Dim objAl As New ArrayList
        'Dim ObjFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
        checkFileExistenceToDownload(filename)
        If ArrCheckedPDIItem.Count > 0 Then
            For Each objPDI As PDI In ArrCheckedPDIItem
                strText = New StringBuilder
                Dim arlistDealer As ArrayList
                Dim objDealer As Dealer = New Dealer
                Dim criteriasDealer As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasDealer.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, objPDI.Dealer.ID))
                arlistDealer = New DealerFacade(User).Retrieve(criteriasDealer)
                If arlistDealer.Count > 0 Then
                    objDealer = CType(arlistDealer(0), Dealer)
                    strText.Append(objDealer.DealerCode)
                End If
                strText.Append(",")

                Dim arlistChassis As ArrayList
                Dim objChassisMaster As ChassisMaster = New ChassisMaster
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ID", MatchType.Exact, objPDI.ChassisMaster.ID))
                arlistChassis = New ChassisMasterFacade(User).Retrieve(criterias)
                If arlistChassis.Count > 0 Then
                    objChassisMaster = CType(arlistChassis(0), ChassisMaster)
                    strText.Append(objChassisMaster.ChassisNumber)
                End If
                strText.Append(",")

                strText.Append(objPDI.Kind.ToString)
                strText.Append(",")


                If objPDI.PDIDate.Date = "1/1/1900" Then
                    strText.Append("")
                Else
                    'Add by Heru 16022006
                    'Dim tgl As String = objPDI.PDIDate.Date.ToShortDateString
                    'Dim delimStr As String = "/"
                    'Dim delimeter As Char() = delimStr.ToCharArray
                    'Dim strtmp As String() = tgl.Split(delimeter)
                    'If Len(strtmp(0)) = 1 Then strtmp(0) = "0" & strtmp(0)
                    'If Len(strtmp(1)) = 1 Then strtmp(1) = "0" & strtmp(1)
                    'strText.Append(strtmp(1) & strtmp(0) & strtmp(2))
                    Dim PDIDate As Date = objPDI.PDIDate.Date
                    Dim _day As String = PDIDate.Day.ToString
                    Dim _month As String = PDIDate.Month.ToString
                    If Len(_day.Trim) = 1 Then _day = "0" & _day
                    If Len(_month.Trim) = 1 Then _month = "0" & _month

                    strText.Append(_day & _month & PDIDate.Year)
                End If
                strText.Append(",")

                Dim _days As String = Now.Day
                Dim _months As String = Now.Month
                If Len(_days.Trim) = 1 Then _days = "0" & _days
                If Len(_months.Trim) = 1 Then _months = "0" & _months
                strText.Append(_days & _months & Now.Year)
                WriteFileToLocalHost(strText.ToString(), filename)
            Next
        End If
    End Sub

    Public Sub CopyFileToSAPServer(ByVal _fileName As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
        Dim _sapServerFolder As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\Service\PDI\"
        Dim _file As KTB.DNet.Utility.TransferFile
        Try
            _file = New KTB.DNet.Utility.TransferFile(_user, _password, _sapServer)
            _file.Transfer(_fileName, _sapServerFolder)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub checkFileExistenceToDownload(ByVal Filename As String)
        Dim finfo As FileInfo = New FileInfo(Filename)
        If finfo.Exists Then
            finfo.Delete()
        End If
    End Sub

    Private Function WriteFileToLocalHost(ByVal str As String, ByVal fileName As String)
        Dim objFileStream As New FileStream(fileName, FileMode.Append, FileAccess.Write)
        Dim objStreamWriter As New StreamWriter(objFileStream)
        objStreamWriter.WriteLine(str)
        objStreamWriter.Close()
    End Function
#End Region

    Private Sub dgPDIEntry_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPDIEntry.SortCommand

        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgPDIEntry.SelectedIndex = -1
        dgPDIEntry.CurrentPageIndex = 0
        bindGridSorting(dgPDIEntry.CurrentPageIndex)
    End Sub


    Private Sub btnRilis_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRilis.ServerClick
        btnRelease_Click(sender, e)
    End Sub

    Private Sub btnSimpan_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.ServerClick
        btnSave_Click(sender, e)
    End Sub

    Private Sub btnBatal_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.ServerClick
        btnCancel_Click(sender, e)
    End Sub

    Private Function CompareDateWithCurrentDate(ByVal entryDate As Date) As Boolean
        Dim currentDate As Date = New Date(Now.Year, Now.Month, Now.Day, 0, 0, 0)
        currentDate = currentDate.AddDays(1)
        If entryDate >= currentDate Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function cekEngineNo() As Boolean
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strChassisNumber = txtChassisMaster.Text.Trim()
        Dim strEngineNo = txtEngineNo.Text.Trim()
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, strChassisNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, strEngineNo))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
        Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
        If ChassisColl.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objPDIFacade As PDIFacade = New PDIFacade(User)
        Dim objPDI As PDI = New PDI
        Dim bEmpty As Boolean = False
        Dim tgl As String = txtTglPDI.Text.Trim
        Dim entryDate As Date
        Dim ObjDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        isDealerPiloting = TCHelper.GetActiveTCResult(ObjDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingPDI))
        If (isDealerPiloting) Then
            If (txtWONumber.Text.Trim() = "") Then
                MessageBox.Show("Untuk Dealer Piloting WO Wajib diisi.")
                Return
            End If
        End If
        Try
            entryDate = New Date(tgl.Substring(4, 4), tgl.Substring(2, 2), tgl.Substring(0, 2), 0, 0, 0)
        Catch ex As Exception
            MessageBox.Show("Format Tanggal tidak sesuai.")
            Return
        End Try
        If CompareDateWithCurrentDate(entryDate) Then
            If ddlPDIKind.SelectedValue = "" Then
                MessageBox.Show("Jenis PDI masih kosong")
                bEmpty = True
            End If

            If Not bEmpty Then
                Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim strChassisNumber = txtChassisMaster.Text.Trim()
                Dim strEngineNo = txtEngineNo.Text.Trim()
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, strChassisNumber))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
                Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
                If ChassisColl.Count > 0 Then
                    'cek no mesin
                    If cekEngineNo() Then
                        If CType(ViewState("vsProcess"), String) = "Insert" Then
                            If m_bPDIDataSave_Privilege Then
                                InsertobjPDI(objPDI)
                                dgPDIEntry.SelectedIndex = -1
                            Else
                                MessageBox.Show("Anda tidak punya hak untuk menginsert data baru !")
                            End If

                        Else
                            Dim objUpdatePDI As PDI = CType(Session.Item("vsObjPDI"), PDI)
                            If m_bPDIDataUpdate_Privilege Then
                                UpdateObjPDI(objUpdatePDI)
                                dgPDIEntry.SelectedIndex = -1
                            Else
                                MessageBox.Show("Anda tidak punya hak untuk mengupdate data lama !")
                            End If
                        End If
                        BindDatagrid(dgPDIEntry.CurrentPageIndex)
                    Else
                        MessageBox.Show("No Mesin tidak sesuai")
                    End If
                Else
                    MessageBox.Show("Chassis tidak terdaftar di " + companyCode)
                End If
            End If
        Else
            MessageBox.Show("Tanggal PDI melebihi hari ini")
        End If
    End Sub

    Private Sub GenerateDocument(ByVal datas As ArrayList)
        Try
            Dim _SANstring As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204

            Dim pdiValidation As PDIValidation = New PDIValidation(_SANstring, _user, _password, _webServer)
            Dim filename As String = String.Empty
            Dim validationResults As List(Of ValidResult) = New List(Of ValidResult)

            For Each obj As PDI In datas
                pdiValidation.GenerateCertificate(obj, True, filename, Nothing, validationResults, True)
                obj.FileName = filename
            Next

            Dim nResult = New PDIFacade(User).UpdatePDICollection(datas)
        Catch ex As Exception

        End Try
    End Sub
End Class