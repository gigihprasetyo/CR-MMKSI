#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports System.Drawing.Color
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmEntryFreeServisBB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents dgFreeServisBBEntry As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnRelease As System.Web.UI.WebControls.Button
    Protected WithEvents btnRilis As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents lblKM As System.Web.UI.WebControls.Label
    Protected WithEvents LblTglServis As System.Web.UI.WebControls.Label
    Protected WithEvents lblTgl As System.Web.UI.WebControls.Label
    Protected WithEvents txtChassisMasterBB As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEngineNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKM As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTglServis As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTglJual As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnBatal As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblVisitType As Label
    Protected WithEvents txtWONumber As TextBox
    Protected WithEvents ddlVisitType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDealerBranchCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBranchName As TextBox
    Private _sessHelper As SessionHelper = New SessionHelper
    Private m_bFormPrivilege As Boolean = False
    Private m_bFreeServiceBBDataUpdate_Privilege As Boolean = False
    Private m_bFreeServiceBBDataRelease_Privilege As Boolean = False
    Protected WithEvents dgFreeServisBBBBEntry As System.Web.UI.WebControls.DataGrid
    Private m_bFreeServiceBBDataSave_Privilege As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub


#End Region

#Region "Custom Variable Declaration"
    'Dim total, dealerName As String
    'Dim totRow As Integer


    '-- Generate timestamp untuk nama file FSData[timestamp].txt
    Dim dt As DateTime = DateTime.Now
    Dim rilis As Boolean = False
    Dim rilisItem As FreeServiceBB
    Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)

    Private Sub BindVisitType()
        With ddlVisitType.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            .Add(New ListItem("Walk In", "WI"))
            .Add(New ListItem("Booking", "BO"))
        End With
    End Sub
#End Region

#Region "Custom Method"

    Private Sub DeleteFreeServisBB(ByVal nID As Integer)
        Dim objFreeServiceBBFacade As FreeServiceBBFacade = New FreeServiceBBFacade(User)
        Dim objFreeServisBB As FreeServiceBB = New FreeServiceBBFacade(User).Retrieve(nID)
        objFreeServiceBBFacade.DeleteFromDB(objFreeServisBB)
        BindDatagrid(dgFreeServisBBEntry.CurrentPageIndex)
        Dim strScript As String
        strScript = "<script>document.all.txtChassisMasterBB.focus();</script>"
        Page.RegisterStartupScript("", strScript)
    End Sub

    Private Sub ViewFreeServiceBB(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objFreeServiceBB As FreeServiceBB = New FreeServiceBBFacade(User).Retrieve(nID)
        txtDealerBranchCode.Text = String.Empty
        txtBranchName.Text = String.Empty
        'Todo session
        Session.Add("vsFreeServiceBB", objFreeServiceBB)
        If Not IsNothing(objFreeServiceBB.DealerBranch) Then
            txtDealerBranchCode.Text = objFreeServiceBB.DealerBranch.DealerBranchCode
            txtBranchName.Text = objFreeServiceBB.DealerBranch.Name
        End If
        txtEngineNumber.Text = objFreeServiceBB.ChassisMasterBB.EngineNumber
        txtChassisMasterBB.Text = objFreeServiceBB.FSKind.KindCode + objFreeServiceBB.ChassisMasterBB.ChassisNumber
        txtKM.Text = objFreeServiceBB.MileAge

        If (Not String.IsNullOrEmpty(objFreeServiceBB.WorkOrderNumber)) Then
            txtWONumber.Text = objFreeServiceBB.WorkOrderNumber
        End If

        If (Not String.IsNullOrEmpty(objFreeServiceBB.VisitType)) Then
            ddlVisitType.SelectedValue = objFreeServiceBB.VisitType
        End If

        If IsNothing(objFreeServiceBB.ServiceDate) Or objFreeServiceBB.ServiceDate = "1/1/1900" Then
            txtTglServis.Text = ""
        Else
            txtTglServis.Text = Format(objFreeServiceBB.ServiceDate, "ddMMyyyy")
        End If

        If IsNothing(objFreeServiceBB.SoldDate) Or objFreeServiceBB.SoldDate = "1/1/1900" Then
            'txtTglJual.Text = ""
        Else
            txtTglJual.Text = Format(objFreeServiceBB.SoldDate, "ddMMyyyy")
        End If

        Me.btnSave.Enabled = EditStatus
    End Sub

    Private Function IsValidDate(ByVal strdate As String) As Boolean
        'Dim strtgl As String = strdate.Substring(2, 2).ToString & "-" & strdate.Substring(0, 2) & "-" & strdate.Substring(4, 4)
        'yang jadi dipakai adalah setting tanggal indonesia
        If Not strdate.Trim = "" Then
            Dim strtgl As String = strdate.Substring(0, 2).ToString & "-" & strdate.Substring(2, 2) & "-" & strdate.Substring(4, 4)
            If IsDate(strtgl) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Private Function ToDate(ByVal strdate As String) As Date
        'Return CType(strdate.Substring(2, 2).ToString & "-" & strdate.Substring(0, 2) & "-" & strdate.Substring(4, 4), Date)
        'yang jadi dipakai adalah setting tanggal indonesia
        'Dim dt As New Date(CInt(strdate.Substring(4, 4)), CInt(strdate.Substring(2, 2)), CInt(strdate.Substring(0, 2)))

        Return CType(strdate.Substring(0, 2).ToString & "-" & strdate.Substring(2, 2) & "-" & strdate.Substring(4, 4), Date)
        'Return dt
    End Function

    Private Sub InsertobjFreeServiceBB(ByVal objFreeServiceBB As FreeServiceBB)
        Dim objFreeServiceBBFacade As FreeServiceBBFacade = New FreeServiceBBFacade(User)
        Dim nResult As Integer = -1
        Dim bCheck As Boolean = True
        Dim ObjChassisMasterBBCheck As ChassisMasterBB
        Dim ObjServiceDealer As Dealer
        Dim ObjSoldDealer As Dealer

        If Not txtChassisMasterBB.Text = String.Empty Then

            If bCheck Then
                'validasi km ke 1 
                If txtKM.Text = "" Then
                    MessageBox.Show("Jarak tempuh tidak boleh kosong !")
                    bCheck = False
                End If
            End If


            If bCheck Then
                'validasi km ke 2
                For i As Integer = 0 To Len(txtKM.Text) - 1
                    If Not IsNumeric(txtKM.Text.Trim.Chars(i)) Then
                        bCheck = False
                        MessageBox.Show("Jarak tempuh harus numerik !")
                        Exit For
                    End If
                Next
            End If

            If bCheck Then
                'validasi km ke 3
                If CType(txtKM.Text, Integer) <= 0 Then
                    MessageBox.Show("Jarak harus > 0 km !")
                    bCheck = False
                End If
            End If

            If bCheck Then
                If ddlVisitType.SelectedIndex = 0 Then
                    MessageBox.Show("Silahkan Pilih tipe visit")
                    bCheck = False
                End If
            End If

            If bCheck Then
                'validasi km ke 4 dan validasi jenis FS (FSKInd)
                Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "KindCode", MatchType.Exact, txtChassisMasterBB.Text.Substring(0, 1).ToString()))

                Dim FSColl As ArrayList = New FSKindFacade(User).Retrieve(critFS)
                If FSColl.Count > 0 Then
                    objFreeServiceBB.FSKind = FSColl(0)
                    If CType(txtKM.Text, Integer) > objFreeServiceBB.FSKind.KM Then
                        MessageBox.Show("Jarak tempuh melampaui batas jenis free service ")
                        bCheck = False
                    Else
                        'disini baru lolos validasi
                        Dim MinValue As Integer = 0
                        Dim crtFS As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Dim srtFS As New SortCollection
                        Dim arlFS As New ArrayList
                        crtFS.opAnd(New Criteria(GetType(FSKind), "KM", MatchType.Lesser, objFreeServiceBB.FSKind.KM))
                        srtFS.Add(New Sort(GetType(FSKind), "KM", Sort.SortDirection.DESC))
                        arlFS = New FSKindFacade(User).Retrieve(crtFS, srtFS)
                        If arlFS.Count > 0 Then
                            MinValue = CType(arlFS(0), FSKind).KM + 1
                        End If
                        If CType(txtKM.Text, Integer) < MinValue Then
                            MessageBox.Show("Jarak tempuh tidak sesuai dengan batas jenis free service ")
                            bCheck = False
                        End If
                    End If
                Else
                    MessageBox.Show("Kode Jenis Free Servis tidak terdaftar !")
                    bCheck = False
                End If
            End If


            'validasi tgl servis
            If bCheck Then
                If txtTglServis.Text.Trim <> "" Then
                    If Len(txtTglServis.Text.Trim) = 8 Then
                        If IsValidDate(txtTglServis.Text.Trim) Then
                            If ToDate(txtTglServis.Text.Trim) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
                                ToDate(txtTglServis.Text.Trim) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
                                MessageBox.Show("Format tanggal service salah")
                                bCheck = False
                            Else
                                objFreeServiceBB.ServiceDate = ToDate(txtTglServis.Text.Trim)
                            End If
                        Else
                            MessageBox.Show("Format tgl. service salah")
                            bCheck = False
                        End If
                    Else
                        bCheck = False
                        MessageBox.Show("Format tgl. service salah")
                    End If
                Else
                    bCheck = False
                    MessageBox.Show("Tgl. service kosong")
                End If
            End If

            If bCheck Then
                Dim objDealerSold As Dealer = New Dealer
                Dim objDealerFS As Dealer = New Dealer

                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim strChassisNumber = txtChassisMasterBB.Text.Substring(1, Len(Trim(txtChassisMasterBB.Text)) - 1)
                Dim strEngineNumber = txtEngineNumber.Text.Trim()
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, strChassisNumber))
                Dim ChassisColl As ArrayList = New ChassisMasterBBFacade(User).Retrieve(criterias)
                If ChassisColl.Count > 0 Then
                    objFreeServiceBB.ChassisMasterBB = ChassisColl(0)

                    Dim critDealerSold As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critDealerSold.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, objFreeServiceBB.ChassisMasterBB.Dealer.ID))
                    Dim DealerCollSold As ArrayList = New DealerFacade(User).Retrieve(critDealerSold)
                    If DealerCollSold.Count > 0 Then
                        objDealerSold = DealerCollSold(0)
                    End If

                    Dim critDealerFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
                    critDealerFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strDealerCode))
                    Dim DealerCollFS As ArrayList = New DealerFacade(User).Retrieve(critDealerFS)
                    If DealerCollFS.Count > 0 Then
                        objDealerFS = DealerCollFS(0)
                    End If
                Else
                    MessageBox.Show("Chassis tidak terdaftar")
                    Return
                    bCheck = False
                End If

                'validasi tgl penjualan
                objFreeServiceBB.SoldDate = ToDate(txtTglJual.Text.Trim)
                'If txtTglJual.Text.Trim <> "" Then
                '    If Len(txtTglJual.Text.Trim) = 8 Then
                '        If IsValidDate(txtTglJual.Text.Trim) Then
                '            If ToDate(txtTglServis.Text.Trim) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
                '               ToDate(txtTglServis.Text.Trim) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
                '                MessageBox.Show("Format tanggal jual salah")
                '                Return
                '                bCheck = False
                '            Else
                '                objFreeServiceBB.SoldDate = ToDate(txtTglJual.Text.Trim)
                '            End If


                '        Else
                '            MessageBox.Show("Format tgl. penjualan salah ")
                '            bCheck = False
                '            Return
                '        End If
                '    Else
                '        bCheck = False
                '        MessageBox.Show("Format tgl. penjualan salah")
                '        Return
                '    End If
                If IsValidDate(txtTglJual.Text.Trim) Then
                    If ToDate(txtTglServis.Text.Trim) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
                       ToDate(txtTglServis.Text.Trim) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
                        MessageBox.Show("Format tanggal jual salah")
                        Return
                        bCheck = False
                    Else
                        objFreeServiceBB.SoldDate = ToDate(txtTglJual.Text.Trim)
                    End If

                End If
                'Else
                '    'Jika dealer FS dan Dealer Penjualan sama maka tanggal jual harus ada
                '    If Not IsNothing(objDealerSold) And Not IsNothing(objDealerFS) Then
                '        If objDealerSold.ID = objDealerFS.ID Then
                '            MessageBox.Show("Dealer FS sama dengan Dealer Penjualan, tanggal penjualan tidak boleh kosong !")
                '            bCheck = False
                '            Return
                '        Else
                '            objFreeServiceBB.SoldDate = New Date(1900, 1, 1)
                '        End If
                '    End If
                'End If
                'Start  :by:dna;on:20111017;for:angga;remark:remove this validator for FS Special
                'If Not IsNothing(objDealerSold) And Not IsNothing(objDealerFS) Then
                '    If objDealerSold.ID = objDealerFS.ID Then
                '        Dim critIsPDI As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                '        critIsPDI.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Selesai, String)))
                '        critIsPDI.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMasterBB.ID", MatchType.Exact, objFreeServiceBB.chassismasterBB.ID))
                '        Dim ArrIsPDI As ArrayList = New PDIFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(critIsPDI)
                '        If ArrIsPDI.Count = 0 Then 'sudah PDI jadi boleh insert
                '            MessageBox.Show("No. Rangka belum PDI ")
                '            bCheck = False
                '            Return
                '        End If
                '    End If
                'End If
                'End    :by:dna;on:20111017;for:angga;remark:remove this validator for FS Special
            End If


            'checking tgl free service sama tanggal jual
            If bCheck Then
                If Not txtTglJual.Text.Trim = "" Then
                    If ToDate(txtTglJual.Text.Trim) > ToDate(txtTglServis.Text.Trim) Then
                        MessageBox.Show("Tanggal Penjualan melebihi Tanggal Service")
                        bCheck = False
                    End If
                End If
            End If

            If bCheck Then
                If Not txtTglServis.Text.Trim = "" Then
                    If Not ToDate(txtTglServis.Text.Trim) <= Now Then
                        MessageBox.Show("Tanggal Service melebihi hari ini")
                        bCheck = False
                    End If
                End If
            End If

            If bCheck Then
                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim strChassisNumber = txtChassisMasterBB.Text.Substring(1, Len(Trim(txtChassisMasterBB.Text)) - 1)
                Dim strEngineNumber = txtEngineNumber.Text.Trim()
                Dim ObjDealerBranch As DealerBranch
                criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, strChassisNumber))
                Dim ChassisColl As ArrayList = New ChassisMasterBBFacade(User).Retrieve(criterias2)
                If ChassisColl.Count > 0 Then
                    objFreeServiceBB.ChassisMasterBB = CType(ChassisColl(0), ChassisMasterBB)

                    If Not ValidateFSKindOnVehicleType(objFreeServiceBB) Then
                        MessageBox.Show("Jenis FS tidak Terdaftar")
                        Return
                    End If

                    ObjChassisMasterBBCheck = CType(ChassisColl(0), ChassisMasterBB)
                    ObjSoldDealer = ObjChassisMasterBBCheck.Dealer

                    If (txtDealerBranchCode.Text.Trim() <> String.Empty) Then
                        Dim criterias4 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias4.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, txtDealerBranchCode.Text))
                        Dim DealerBranchColl As ArrayList = New DealerBranchFacade(User).Retrieve(criterias4)

                        If DealerBranchColl.Count > 0 Then
                            ObjDealerBranch = CType(DealerBranchColl(0), DealerBranch)
                            objFreeServiceBB.DealerBranch = ObjDealerBranch
                        Else
                            MessageBox.Show("Kode Cabang Dealer tidak terdaftar ")
                        End If
                    End If

                    Dim criterias3 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
                    criterias3.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strDealerCode))
                    Dim DealerColl As ArrayList = New DealerFacade(User).Retrieve(criterias3)
                    If DealerColl.Count > 0 Then
                        ObjServiceDealer = CType(DealerColl(0), Dealer)


                        If Not IsExistCodeForInsert(objFreeServiceBB.ChassisMasterBB.ID, objFreeServiceBB.FSKind.ID) Then
                            objFreeServiceBB.Dealer = ObjServiceDealer
                            objFreeServiceBB.MileAge = CType(txtKM.Text, Integer)
                            objFreeServiceBB.Status = EnumFSStatus.FSStatus.Baru
                            objFreeServiceBB.VisitType = ddlVisitType.SelectedValue
                            objFreeServiceBB.WorkOrderNumber = txtWONumber.Text.Trim()
                            'Dim objType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}
                            ''End    :CR - Temporary allowing TU00 :Rina
                            'Dim isAllowInsert As Boolean = True
                            'For i As Integer = 0 To objType.Length - 1
                            '    If objFreeServiceBB.ChassisMasterBB.VechileColor.VechileType.VechileTypeCode = objType(i) And (objFreeServiceBB.FSKind.KindCode = "3" Or objFreeServiceBB.FSKind.KindCode = "4" Or objFreeServiceBB.FSKind.KindCode = "5") Then
                            '        isAllowInsert = False
                            '        MessageBox.Show("Simpan gagal, Chassis " & objFreeServiceBB.ChassisMasterBB.ChassisNumber & " tidak mendapat kupon Free Service")
                            '        Exit For
                            '    End If
                            'Next
                            'isAllowInsert = Me.ChassisException(isAllowInsert, objFreeServiceBB)

                            'isAllowInsert = Me.IsFE7orFE8(isAllowInsert, objFreeServiceBB)
                            'isAllowInsert = Me.CheckToAllowStradaTriton(isAllowInsert, objFreeServiceBB)
                            'isAllowInsert = Me.IsFE75orFESHD(isAllowInsert, objFreeServiceBB)

                            If txtChassisMasterBB.Text.Substring(0, 1).ToString() = "1" Or txtChassisMasterBB.Text.Substring(0, 1).ToString() = "2" Then
                                nResult = objFreeServiceBBFacade.Insert(objFreeServiceBB)
                            Else
                                nResult = objFreeServiceBBFacade.Insert(objFreeServiceBB)

                                'If objFreeServiceBBFacade.IsAllowFreeServiceBB(objFreeServiceBB) Then
                                '    nResult = objFreeServiceBBFacade.Insert(objFreeServiceBB)
                                'Else
                                '    MessageBox.Show("Simpan gagal, Chassis " & objFreeServiceBB.ChassisMasterBB.ChassisNumber & " tidak mendapat kupon Free Service")
                                '    nResult = -1
                                'End If
                            End If

                            If nResult = -1 Then
                                'MessageBox.Show("Simpan Gagal")
                            Else
                                MessageBox.Show("Simpan Sukses")
                                Session.Add("vsFreeServiceBB", objFreeServiceBB)
                                ClearDataAfterSaving()
                                Dim strScript As String
                                strScript = "<script>document.all.txtChassisMasterBB.focus();</script>"
                                Page.RegisterStartupScript("", strScript)
                            End If
                        Else
                            MessageBox.Show("No. Rangka dengan jenis servis tersebut sudah ada ")
                        End If
                    Else
                        MessageBox.Show("Kode Dealer tidak terdaftar ")
                    End If
                Else
                    MessageBox.Show("No. Rangka tidak terdaftar !")
                End If
            End If
        Else
            MessageBox.Show("Kode FS Kind dan No Rangka Tidak boleh kosong")
        End If
    End Sub


    Public Function IsAllowFSCampaign(ByVal objFreeServiceBB As FreeServiceBB) As Boolean
        Dim vReturn As Boolean = False
        Dim arlFSCampaign As ArrayList = New ArrayList
        arlFSCampaign = GetFSCampaign()
        If arlFSCampaign.Count > 0 Then

            For Each objFSCampaign As FSCampaign In arlFSCampaign
                'Dealer checking
                Dim bDealer As Boolean = True
                If objFSCampaign.DealerChecked = True Then
                    bDealer = False
                    For Each objFSCampaignDealer As FSCampaignDealer In objFSCampaign.FSCampaignDealers
                        If objFSCampaignDealer.DealerCode = objFreeServiceBB.Dealer.DealerCode Then
                            bDealer = True
                        End If
                    Next
                End If

                'FSKind checking
                Dim bFSKind As Boolean = True
                If objFSCampaign.FSTypeChecked = True Then
                    bFSKind = False
                    For Each objFSCampaignKind As FSCampaignKind In objFSCampaign.FSCampaignKinds
                        If objFSCampaignKind.FSKind.KindCode = objFreeServiceBB.FSKind.KindCode Then
                            bFSKind = True
                        End If
                    Next
                End If

                'VehicleType checking
                Dim bVehicle As Boolean = True
                If objFSCampaign.VehicleTypeChecked = True Then
                    bVehicle = False
                    For Each objFSCampaignVehicle As FSCampaignVehicle In objFSCampaign.FSCampaignVehicles
                        If objFSCampaignVehicle.VechileType.ID = objFreeServiceBB.ChassisMasterBB.VechileColor.VechileType.ID Then
                            bVehicle = True
                        End If
                    Next
                End If

                'Faktur Validation checking
                Dim bFaktur As Boolean = True
                If objFSCampaign.FakturDateChecked = True Then
                    bFaktur = False
                    If objFSCampaign.DateFrom <= objFreeServiceBB.ChassisMasterBB.EndCustomer.ValidateTime _
                       And objFSCampaign.DateTo >= objFreeServiceBB.ChassisMasterBB.EndCustomer.ValidateTime Then
                        bFaktur = True
                    End If
                End If

                'Combine value above
                If bDealer And bFSKind And bVehicle And bFaktur Then
                    vReturn = True
                    Exit For
                End If
            Next
        End If
        If vReturn = False Then
            MessageBox.Show("Chassis " & objFreeServiceBB.ChassisMasterBB.ChassisNumber & " dan FS Kind " & objFreeServiceBB.FSKind.KindCode & " tidak mendapat Free Service")
        End If

        Return vReturn
    End Function

    Public Function GetFSCampaign() As ArrayList
        Dim arlFSCampaign As ArrayList = New ArrayList
        Dim objFSCampaignFacade As FSCampaignFacade = New FSCampaignFacade(User)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(FSCampaign), "Status", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(FSCampaign), "DateFrom", MatchType.LesserOrEqual, Format(Date.Now, "yyyy-MM-dd 00:00:00")))
        criterias.opAnd(New Criteria(GetType(FSCampaign), "DateTo", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy-MM-dd 00:00:00")))
        arlFSCampaign = objFSCampaignFacade.Retrieve(criterias)

        Return arlFSCampaign
    End Function

    Public Function IsFE7orFE8(ByVal IsAllowInsert As Boolean, ByRef objFreeServiceBB As FreeServiceBB) As Boolean
        'Start  :CR;by:dna;for:rina;on:20100615;remark:allow for below condition
        If IsAllowInsert = False Then
            Dim dtFacturValidation As Date = DateSerial(objFreeServiceBB.ChassisMasterBB.EndCustomer.ValidateTime.Year, objFreeServiceBB.ChassisMasterBB.EndCustomer.ValidateTime.Month, objFreeServiceBB.ChassisMasterBB.EndCustomer.ValidateTime.Day)
            If (objFreeServiceBB.ChassisMasterBB.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE7" _
            OrElse objFreeServiceBB.ChassisMasterBB.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE8") _
            AndAlso (objFreeServiceBB.FSKind.KindCode = "3" OrElse objFreeServiceBB.FSKind.KindCode = "4" OrElse objFreeServiceBB.FSKind.KindCode = "5" OrElse objFreeServiceBB.FSKind.KindCode = "6" OrElse objFreeServiceBB.FSKind.KindCode = "7") _
            AndAlso (dtFacturValidation > DateSerial(2010, 4, 1).AddDays(-1) And dtFacturValidation < DateSerial(2010, 10, 1)) Then
                IsAllowInsert = True
            End If
        End If
        Return IsAllowInsert
    End Function

    Public Function CheckToAllowStradaTriton(ByVal IsAllowInsert As Boolean, ByRef objFreeServiceBB As FreeServiceBB) As Boolean
        'Start  :CR;by:dna;for:Rina;On:20100702;Remark:allow for Strada Triton
        Dim nFSKind As Integer
        Dim dtValidate As Date
        Dim dtStart As Date = DateSerial(2010, 4, 1)
        Dim dtEnd As Date = DateSerial(2010, 12, 31)
        Dim oVT As VechileType = Nothing
        Dim objType() As String = {"VA01", "VW01", "VJ01", "VT01", "VK01", "VS01", "VP01", "RD01", "RE01", "RF01", "RG01", "RH01", "RI01"}
        Dim FSType As String
        Dim IsValidFSType As Boolean = False
        Dim i As Integer

        Try
            FSType = objFreeServiceBB.ChassisMasterBB.VechileColor.VechileType.VechileTypeCode.Trim.ToUpper
        Catch ex As Exception
            FSType = ""
        End Try
        For i = 0 To objType.Length - 1
            If FSType = objType(i).Trim.ToUpper Then
                IsValidFSType = True
                Exit For
            End If
        Next

        Try
            nFSKind = CType(objFreeServiceBB.FSKind.KindCode, Integer)
        Catch ex As Exception
            nFSKind = 0
        End Try
        Try
            dtValidate = objFreeServiceBB.ChassisMasterBB.EndCustomer.ValidateTime
        Catch ex As Exception
            dtValidate = Date.MinValue
        End Try
        Try
            oVT = objFreeServiceBB.ChassisMasterBB.VechileColor.VechileType
        Catch ex As Exception
            oVT = Nothing
        End Try

        If IsValidFSType _
        AndAlso nFSKind >= 3 AndAlso nFSKind <= 9 _
        AndAlso dtValidate >= dtStart AndAlso dtValidate <= dtEnd _
        AndAlso Not IsNothing(oVT) _
        AndAlso 1 = 1 Then 'AndAlso oVT.Description.Trim.ToUpper.StartsWith("STRADA TRITON") _
            IsAllowInsert = True
        End If
        Return IsAllowInsert
        'End    :CR;by:dna;for:Rina;On:20100702;Remark:allow for Strada Triton
    End Function

    Public Function ChassisException(ByVal IsAllowInsert As Boolean, ByRef objFreeServiceBB As FreeServiceBB) As Boolean
        'Start  :CR;by:dna;for:Rina;On:20100616;Remark:allow for specified CM
        If IsAllowInsert = True _
        AndAlso (objFreeServiceBB.FSKind.KindCode = "3" _
        OrElse objFreeServiceBB.FSKind.KindCode = "4" _
        OrElse objFreeServiceBB.FSKind.KindCode = "5" _
        OrElse objFreeServiceBB.FSKind.KindCode = "6" _
        OrElse objFreeServiceBB.FSKind.KindCode = "7") Then
            Dim sForbiddenCMs() As String = {"MHMFE71P1AK018514", "MHMFE73P2AK014642", "MHMFE73P2AK014643", "MHMFE73P2AK014715", "MHMFE73P2AK014760"}
            For i As Integer = 0 To sForbiddenCMs.Length - 1
                If objFreeServiceBB.ChassisMasterBB.ChassisNumber.Trim.ToUpper = sForbiddenCMs(i).Trim.ToUpper Then
                    IsAllowInsert = False
                    MessageBox.Show("Simpan gagal, Chassis " & objFreeServiceBB.ChassisMasterBB.ChassisNumber & " tidak mendapat kupon Free Service")
                    Exit For
                End If
            Next
        End If
        Return IsAllowInsert
        'End    :CR;by:dna;for:Rina;On:20100616;Remark:allow for specified CM
    End Function

    Public Function IsFE75orFESHD(ByVal IsAllowInsert As Boolean, ByRef objFreeServiceBB As FreeServiceBB) As Boolean
        'Start  :CR;by:anh;for:Rina;On:20100616;Remark:allow for specified material description

        If IsAllowInsert = False Then
            If (objFreeServiceBB.FSKind.KindCode = "3" Or objFreeServiceBB.FSKind.KindCode = "4" Or objFreeServiceBB.FSKind.KindCode = "5") Then
                If (objFreeServiceBB.ChassisMasterBB.VechileColor.MaterialDescription.Substring(0, 4) = "FE75" _
                Or objFreeServiceBB.ChassisMasterBB.VechileColor.MaterialDescription.Substring(0, 4) = "FESH") Then
                    If objFreeServiceBB.ChassisMasterBB.EndCustomer Is Nothing Then
                        MessageBox.Show("Simpan gagal, Chassis " & objFreeServiceBB.ChassisMasterBB.ChassisNumber & " No faktur tidak ada")
                    Else
                        Dim dtFacturValidation As Date = DateSerial(objFreeServiceBB.ChassisMasterBB.EndCustomer.ValidateTime.Year, objFreeServiceBB.ChassisMasterBB.EndCustomer.ValidateTime.Month, objFreeServiceBB.ChassisMasterBB.EndCustomer.ValidateTime.Day)
                        If (dtFacturValidation > DateSerial(2009, 8, 1) And dtFacturValidation < DateSerial(2009, 11, 1).AddDays(-1)) Then
                            IsAllowInsert = True
                        End If
                    End If
                End If
            End If
        End If
        Return IsAllowInsert
        'End    :CR;by:anh;for:Rina;On:20100616;Remark:allow for specified material description
    End Function

    Private Sub UpdateObjFreeServiceBB(ByVal objFreeServiceBB As FreeServiceBB)
        Dim objFreeServiceBBFacade As FreeServiceBBFacade = New FreeServiceBBFacade(User)
        Dim nResult As Integer = -1
        Dim bCheck As Boolean = True
        If Not txtChassisMasterBB.Text = String.Empty Then

            'validasi km
            If bCheck Then
                If txtKM.Text = "" Or CType(txtKM.Text, Integer) <= 0 Then
                    MessageBox.Show("Jarak tempuh tidak boleh kosong dan harus > 0 km !")
                    bCheck = False
                Else
                    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "KindCode", MatchType.Exact, txtChassisMasterBB.Text.Trim.Substring(0, 1).ToString()))
                    Dim FSColl As ArrayList = New FSKindFacade(User).Retrieve(critFS)
                    If FSColl.Count > 0 Then
                        objFreeServiceBB.FSKind = FSColl(0)
                        If CType(txtKM.Text.Trim, Integer) > objFreeServiceBB.FSKind.KM Then
                            MessageBox.Show("Jarak tempuh melampaui batas jenis free service ")
                            bCheck = False
                        Else
                            'disini baru lolos validasi
                            Dim MinValue As Integer = 0
                            Dim crtFS As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            Dim srtFS As New SortCollection
                            Dim arlFS As New ArrayList
                            crtFS.opAnd(New Criteria(GetType(FSKind), "KM", MatchType.Lesser, objFreeServiceBB.FSKind.KM))
                            srtFS.Add(New Sort(GetType(FSKind), "KM", Sort.SortDirection.DESC))
                            arlFS = New FSKindFacade(User).Retrieve(crtFS, srtFS)
                            If arlFS.Count > 0 Then
                                MinValue = CType(arlFS(0), FSKind).KM + 1
                            End If
                            If CType(txtKM.Text, Integer) < MinValue Then
                                MessageBox.Show("Jarak tempuh tidak sesuai dengan batas jenis free service ")
                                bCheck = False
                            End If
                        End If
                    Else
                        MessageBox.Show("Kode Jenis Free Servis tidak terdaftar !")
                        bCheck = False
                    End If
                End If
            End If

            'validasi tgl servis
            If bCheck Then
                If txtTglServis.Text.Trim <> "" Then
                    If Len(txtTglServis.Text.Trim) = 8 Then
                        If IsValidDate(txtTglServis.Text.Trim) Then

                            If ToDate(txtTglServis.Text.Trim) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
                                ToDate(txtTglServis.Text.Trim) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
                                MessageBox.Show("Format tanggal service salah")
                                bCheck = False
                            Else
                                objFreeServiceBB.ServiceDate = ToDate(txtTglServis.Text.Trim)
                            End If
                        Else
                            MessageBox.Show("Format tgl. service salah")
                            bCheck = False
                        End If
                    Else
                        bCheck = False
                        MessageBox.Show("Format tgl. service salah")
                    End If
                Else
                    bCheck = False
                    MessageBox.Show("Tgl. service kosong")
                End If
            End If


            'validasi tgl penjualan
            If txtTglJual.Text.Trim <> "" Then
                If IsValidDate(txtTglJual.Text.Trim) Then
                    If IsValidDate(txtTglJual.Text.Trim) Then
                        If ToDate(txtTglJual.Text.Trim) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
                           ToDate(txtTglJual.Text.Trim) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
                            MessageBox.Show("Format tanggal jual salah")
                            bCheck = False
                        Else
                            objFreeServiceBB.SoldDate = ToDate(txtTglJual.Text.Trim)
                        End If
                    Else
                        MessageBox.Show("Format tgl. penjualan salah ")
                        bCheck = False
                    End If
                Else
                    MessageBox.Show("Format tgl. penjualan salah ")
                    bCheck = False
                End If
            Else
                'Jika dealer FS dan Dealer Penjualan sama maka tanggal jual harus ada
                Dim objDealerSold As Dealer = New Dealer
                Dim objDealerFS As Dealer = New Dealer

                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim strChassisNumber = txtChassisMasterBB.Text.Trim.Substring(1, Len(Trim(txtChassisMasterBB.Text)) - 1)
                Dim strEngineNumber = txtEngineNumber.Text.Trim()
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, strChassisNumber))
                Dim ChassisColl As ArrayList = New ChassisMasterBBFacade(User).Retrieve(criterias)
                If ChassisColl.Count > 0 Then
                    objFreeServiceBB.ChassisMasterBB = ChassisColl(0)

                    Dim critDealerSold As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critDealerSold.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, objFreeServiceBB.ChassisMasterBB.Dealer.ID))
                    Dim DealerCollSold As ArrayList = New DealerFacade(User).Retrieve(critDealerSold)
                    If DealerCollSold.Count > 0 Then
                        objDealerSold = DealerCollSold(0)
                    End If

                    Dim critDealerFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
                    critDealerFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strDealerCode))
                    Dim DealerCollFS As ArrayList = New DealerFacade(User).Retrieve(critDealerFS)
                    If DealerCollFS.Count > 0 Then
                        objDealerFS = DealerCollFS(0)
                    End If

                    If Not IsNothing(objDealerSold) And Not IsNothing(objDealerFS) Then
                        If objDealerSold.ID = objDealerFS.ID Then
                            MessageBox.Show("Dealer FS sama dengan Dealer Penjualan, tanggal penjualan tidak boleh kosong !")
                            bCheck = False
                        Else
                            objFreeServiceBB.SoldDate = New Date(1900, 1, 1)
                        End If
                    End If
                End If
            End If

            If bCheck Then
                If Not txtTglJual.Text.Trim = "" Then
                    If ToDate(txtTglJual.Text.Trim) > ToDate(txtTglServis.Text.Trim) Then
                        MessageBox.Show("Tanggal Penjualan melebihi Tanggal Service")
                        bCheck = False
                    End If
                End If
            End If

            If bCheck Then
                If Not ToDate(txtTglServis.Text) <= Now Then
                    MessageBox.Show("Tanggal Service melebihi hari ini")
                    bCheck = False
                End If
            End If


            If bCheck Then
                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim strChassisNumber = txtChassisMasterBB.Text.Trim.Substring(1, Len(Trim(txtChassisMasterBB.Text)) - 1)
                Dim strEngineNumber = txtEngineNumber.Text.Trim()
                criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, strChassisNumber))
                criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ID", MatchType.No, objFreeServiceBB.ChassisMasterBB.ID))
                Dim ChassisColl As ArrayList = New ChassisMasterBBFacade(User).Retrieve(criterias2)
                If ChassisColl.Count <= 0 Then
                    Dim criterias3 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
                    criterias3.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strDealerCode))
                    Dim DealerColl As ArrayList = New DealerFacade(User).Retrieve(criterias3)
                    If DealerColl.Count > 0 Then
                        Dim criterias4 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias4.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, txtDealerBranchCode.Text))
                        Dim DealerBranchColl As ArrayList = New DealerBranchFacade(User).Retrieve(criterias4)

                        If DealerBranchColl.Count > 0 Then
                            If Not IsExistCodeForUpdate(objFreeServiceBB.ID, objFreeServiceBB.ChassisMasterBB.ID, objFreeServiceBB.FSKind.ID) Then
                                'Jika ok isi dulu yang masih diperlukan
                                objFreeServiceBB.MileAge = CType(txtKM.Text.Trim, Integer)
                                objFreeServiceBB.Status = EnumFSStatus.FSStatus.Baru
                                objFreeServiceBB.VisitType = ddlVisitType.SelectedValue
                                objFreeServiceBB.WorkOrderNumber = txtWONumber.Text.Trim()
                                'Remark    :CR;by:anh;for:rina;on:20100827;remark:udah di berlaku untuksemua dealer
                                'If ValidateLBUMBengkulu(objFreeServiceBB, True) = False Then
                                '    MessageBox.Show(SR.SaveFail)
                                '    Exit Sub
                                'End If
                                'end remarks by anh

                                'lalu update
                                'Dim objType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}
                                ''End    :CR - Temporary allowing TU00 :Rina
                                'Dim IsAllowToUpdate As Boolean = True
                                'For i As Integer = 0 To objType.Length - 1
                                '    If objFreeServiceBB.ChassisMasterBB.VechileColor.VechileType.VechileTypeCode = objType(i) And (objFreeServiceBB.FSKind.KindCode = "3" Or objFreeServiceBB.FSKind.KindCode = "4" Or objFreeServiceBB.FSKind.KindCode = "5") Then
                                '        IsAllowToUpdate = False
                                '        Exit For
                                '    End If
                                'Next

                                'IsAllowToUpdate = Me.ChassisException(IsAllowToUpdate, objFreeServiceBB)
                                'IsAllowToUpdate = Me.IsFE7orFE8(IsAllowToUpdate, objFreeServiceBB)
                                'IsAllowToUpdate = Me.CheckToAllowStradaTriton(IsAllowToUpdate, objFreeServiceBB)
                                'IsAllowToUpdate = Me.IsFE75orFESHD(IsAllowToUpdate, objFreeServiceBB)
                                If txtChassisMasterBB.Text.Substring(0, 1).ToString() = "1" Or txtChassisMasterBB.Text.Substring(0, 1).ToString() = "2" Then
                                    nResult = objFreeServiceBBFacade.Update(objFreeServiceBB)
                                Else
                                    nResult = objFreeServiceBBFacade.Update(objFreeServiceBB)
                                    'If objFreeServiceBBFacade.IsAllowFreeServiceBB(objFreeServiceBB) Then
                                    '    nResult = objFreeServiceBBFacade.Update(objFreeServiceBB)
                                    'Else
                                    '    MessageBox.Show("Update gagal, Chassis " & objFreeServiceBB.chassismasterBB.ChassisNumber & " tidak mendapat kupon Free Service")
                                    '    nResult = -1
                                    'End If
                                End If

                                If nResult = -1 Then
                                    MessageBox.Show("Update Gagal")
                                Else
                                    MessageBox.Show("Update Sukses")
                                    ClearData()
                                    Dim strScript As String
                                    strScript = "<script>document.all.txtChassisMasterBB.focus();</script>"
                                    Page.RegisterStartupScript("", strScript)
                                End If
                            Else
                                MessageBox.Show("No. Rangka dengan jenis servis tersebut sudah ada")
                            End If
                        Else
                            MessageBox.Show("Kode Cabang Dealer tidak terdaftar ")
                        End If
                    Else
                        MessageBox.Show("Kode Dealer tidak terdaftar !")
                    End If 
                Else
                    MessageBox.Show("No. Rangka sudah ada !")
                End If
            End If
        Else
            MessageBox.Show("Kode FS Kind dan No Rangka Tidak boleh kosong")
        End If

    End Sub

    Private Function ValidateLBUMBengkulu(ByVal oFS As FreeServiceBB, ByVal Filter1Status As Boolean) As Boolean
        Dim oD As Dealer = CType(Session.Item("DEALER"), Dealer)
        If oFS.FSKind.KindCode = "1" Or oFS.FSKind.KindCode = "2" Then
            Return True
        End If

        If (oFS.ChassisMasterBB.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE7" _
        Or oFS.ChassisMasterBB.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE8") _
        Then
            If (oFS.FSKind.KindCode = "3" OrElse oFS.FSKind.KindCode = "4" OrElse oFS.FSKind.KindCode = "5" OrElse oFS.FSKind.KindCode = "6" OrElse oFS.FSKind.KindCode = "7") _
            And oD.DealerCode = "100016" _
            AndAlso (oFS.ChassisMasterBB.EndCustomer.OpenFakturDate >= DateSerial(2010, 2, 1) _
            And oFS.ChassisMasterBB.EndCustomer.OpenFakturDate <= DateSerial(2010, 6, 30)) _
            Then
                Return True
            Else
                Return False
            End If
        Else
            Return Filter1Status
        End If

        'If (oFS.FSKind.KindCode = "3" OrElse oFS.FSKind.KindCode = "4" OrElse oFS.FSKind.KindCode = "5" OrElse oFS.FSKind.KindCode = "6") _
        'AndAlso (oFS.ChassisMasterBB.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE7" _
        'Or oFS.ChassisMasterBB.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE8") _
        'Then
        '    If oD.DealerCode = "100016" _
        '    AndAlso (oFS.ChassisMasterBB.EndCustomer.OpenFakturDate >= DateSerial(2010, 2, 1) _
        '    And oFS.ChassisMasterBB.EndCustomer.OpenFakturDate <= DateSerial(2010, 6, 30)) _
        '    Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'Else
        '    Return False
        'End If
    End Function

    Private Sub BindDatagrid(ByVal indexPage)
        Dim totRow As Integer = 0
        If Not IsNothing(CType(Session.Item("sessDealer"), Dealer)) Then
            Dim TmpObjDealer As Dealer = CType(Session.Item("sessDealer"), Dealer)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Status", MatchType.Exact, CType(EnumFSStatus.FSStatus.Baru, String)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Dealer.ID", MatchType.Exact, CType(TmpObjDealer.ID, Integer)))

            Dim branchCode As String = txtDealerBranchCode.Text.Trim()
            If Not String.IsNullOrEmpty(branchCode) Then
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "DealerBranch.DealerBranchCode", MatchType.Exact, branchCode))
                Dim ObjDealerBranch As DealerBranch
                Dim criterias4 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias4.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, txtDealerBranchCode.Text))
                Dim DealerBranchColl As ArrayList = New DealerBranchFacade(User).Retrieve(criterias4)
                If DealerBranchColl.Count > 0 Then
                    ObjDealerBranch = CType(DealerBranchColl(0), DealerBranch)
                    txtBranchName.Text = ObjDealerBranch.Name
                End If

            End If

            'dgFreeServisBBEntry.DataSource = New FreeServiceBBFacade(User).Retrieve(criterias)
            ' ery puts some changes

            _sessHelper.SetSession("SortViewFS", criterias)
            dgFreeServisBBEntry.DataSource = New FreeServiceBBFacade(User).RetrieveActiveList(criterias, indexPage + 1, dgFreeServisBBEntry.PageSize, totRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgFreeServisBBEntry.VirtualItemCount = totRow

            Dim al As ArrayList = dgFreeServisBBEntry.DataSource
            _sessHelper.SetSession("SessArrFreeServiceBB", dgFreeServisBBEntry.DataSource)
            If al.Count > 0 Then
                btnRelease.Enabled = True
                btnRilis.Disabled = False
            Else
                btnRelease.Enabled = False
                btnRilis.Disabled = True
            End If
            dgFreeServisBBEntry.DataBind()
        End If

    End Sub

    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dgFreeServisBBEntry.DataSource = New FreeServiceBBFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("sortViewFS"), CriteriaComposite), indexPage + 1, dgFreeServisBBEntry.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgFreeServisBBEntry.VirtualItemCount = totalRow
            dgFreeServisBBEntry.DataBind()
        End If

    End Sub

    Private Sub InitiatePage()
        'SetControlPrivilege()
        ViewState("currentSortColumn") = "ID"
        ViewState("currentSortDirection") = Sort.SortDirection.DESC
        txtChassisMasterBB.Attributes.Add("onkeydown", "enter(document.all.txtKM)")
        txtKM.Attributes.Add("onkeydown", "enter(document.all.txtTglServis)")
        txtTglServis.Attributes.Add("onkeydown", "enter(document.all.txtTglJual)")
        txtTglJual.Attributes.Add("onkeydown", "enter(document.all.btnSimpan)")
        btnSimpan.Attributes.Add("onkeydown", "enter(document.all.txtChassisMasterBB)")
        BindVisitType()
    End Sub

    Private Sub ClearDataAfterSaving()
        txtChassisMasterBB.Text = String.Empty
        txtKM.Text = String.Empty
        txtWONumber.Text = String.Empty
        txtDealerBranchCode.Text = String.Empty
        txtEngineNumber.Text = String.Empty
        txtBranchName.Text = String.Empty
        txtTglJual.Text = String.Empty
        txtTglServis.Text = String.Empty
        txtDealerBranchCode.Text = String.Empty
        txtBranchName.Text = String.Empty
        ddlVisitType.SelectedIndex = 0

        txtChassisMasterBB.ReadOnly = False
        txtEngineNumber.ReadOnly = False
        txtKM.ReadOnly = False
        txtWONumber.ReadOnly = False
        txtTglServis.ReadOnly = False
        txtTglJual.ReadOnly = False
        lblPopUpDealerBranch.Visible = True
        txtDealerBranchCode.ReadOnly = False
        ddlVisitType.Enabled = True

        btnSave.Enabled = True
        ViewState.Add("vsProcess", "Insert")

    End Sub

    Private Sub ClearData()
        txtChassisMasterBB.Text = String.Empty
        txtKM.Text = String.Empty
        txtTglServis.Text = String.Empty
        txtWONumber.Text = String.Empty
        txtEngineNumber.Text = String.Empty
        txtTglJual.Text = String.Empty
        txtDealerBranchCode.Text = String.Empty
        txtBranchName.Text = String.Empty
        ddlVisitType.SelectedIndex = 0

        txtChassisMasterBB.ReadOnly = False
        txtKM.ReadOnly = False
        txtTglServis.ReadOnly = False
        txtWONumber.ReadOnly = False
        txtEngineNumber.ReadOnly = False
        txtTglJual.ReadOnly = False
        lblPopUpDealerBranch.Visible = True
        txtDealerBranchCode.ReadOnly = False
        ddlVisitType.Enabled = True

        btnSave.Enabled = True
        ViewState.Add("vsProcess", "Insert")

    End Sub

    Private Function IsExistCodeForInsert(ByVal ChassisID As Integer, ByVal FSKindID As Integer) As Boolean
        Dim objFreeServiceBBFacade As FreeServiceBBFacade = New FreeServiceBBFacade(User)
        'Periksa agar tidak ada key ganda 
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "ChassisMasterBB.ID", MatchType.Exact, ChassisID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "FSKind.ID", MatchType.Exact, FSKindID))
        Dim TestExist As ArrayList = New FreeServiceBBFacade(User).Retrieve(criterias)
        If TestExist.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsExistCodeForUpdate(ByVal FSID As Integer, ByVal ChassisID As Integer, ByVal FSKindID As Integer) As Boolean
        Dim objFreeServiceBBFacade As FreeServiceBBFacade = New FreeServiceBBFacade(User)
        'Periksa agar tidak ada key ganda 
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "ChassisMasterBB.ID", MatchType.Exact, ChassisID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "FSKind.ID", MatchType.Exact, FSKindID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Status", MatchType.Exact, EnumFSStatus.FSStatus.Baru))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "ID", MatchType.No, FSID))
        Dim TestExist As ArrayList = New FreeServiceBBFacade(User).Retrieve(criterias)
        If TestExist.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function GetCheckedFSItem() As ArrayList
        dgFreeServisBBEntry.DataSource = CType(Session("SessArrFreeServiceBB"), ArrayList)
        Dim arlCheckedFSItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgFreeServisBBEntry.Items
            nIndeks = dtgItem.ItemIndex
            Dim objFS As FreeServiceBB = CType(CType(dgFreeServisBBEntry.DataSource, ArrayList)(nIndeks), FreeServiceBB)
            If CType(dtgItem.Cells(2).FindControl("cbSelect"), CheckBox).Checked Then
                If Not IsNothing(objFS) Then
                    If objFS.Status = CType(EnumFSStatus.FSStatus.Baru, String) Then
                        objFS.Status = CType(EnumFSStatus.FSStatus.Proses, String)
                        arlCheckedFSItem.Add(objFS)
                    End If
                End If
            End If
        Next
        Return arlCheckedFSItem
    End Function

    Private Function IsValidCMForFSSpecial(ByVal ChassisNumber As String) As Boolean
        Dim oCMBBFac As New ChassisMasterBBFacade(User)
        Dim aCMBBs As ArrayList
        Dim cCMBB As New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sColors As String = KTB.DNet.Lib.WebConfig.GetValue("FSSpecialColor")

        sColors = "'" & sColors.Replace(";", "','") & "'"
        cCMBB.opAnd(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, ChassisNumber))
        'remarks bt anh 20140602 req by anq
        'cCMBB.opAnd(New Criteria(GetType(ChassisMasterBB), "VechileColor.Status", MatchType.Exact, "X"))
        'cCMBB.opAnd(New Criteria(GetType(ChassisMasterBB), "VechileColor.ColorCode", MatchType.InSet, "(" & sColors & ")"))
        'anker
        aCMBBs = oCMBBFac.Retrieve(cCMBB)
        If aCMBBs.Count < 1 Then
            MessageBox.Show("Chassis " & ChassisNumber & " tidak boleh Free Service Special")
            Return False
        End If
        Return True
    End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then

            If Not IsDownloaded() Then
                Dim strMessage As String = String.Empty
                strMessage = GetMonthlyFaultDescription()
                Dim strMessageHeader As String = "Anda belum melakukan download atau kirim dokumen Kwitansi FS Campaign/FS Letter/FS Campaign Letter/Kwitansi Free Labour/Free Labour Letter/Free Maintenance Letter/Kwitansi Free Maintenance pada periode berikut : "
                Server.Transfer("../FrmAccessDenied.aspx?isEncode=1&mess=" & Server.UrlEncode(strMessageHeader) & "&messDescription=" & Server.UrlEncode(strMessage) & "")

            End If
            If Not IsNothing(Session("DEALER")) Then
                Me.txtTglJual.Text = KTB.DNet.Lib.WebConfig.GetValue("BackboneSoldDate")
                InitiatePage()
                Dim ObjDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
                _sessHelper.SetSession("sessDealer", ObjDealer)
                lblDealerCode.Text = ObjDealer.DealerCode + " / " + ObjDealer.SearchTerm1
                lblDealerName.Text = ObjDealer.DealerName
                lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
                _sessHelper.SetSession("sessDealerLogin", ObjDealer.DealerCode)

                ViewState.Add("vsProcess", "Insert")

                BindDatagrid(0)
            Else
                'Response.Redirect("../SessionExpired.htm")
            End If
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.FreeServiceDataView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FREE SERVICE - Data Free Service")
        End If

        'If Not IsDownloaded() Then
        '    Server.Transfer("../FrmAccessDenied.aspx?mess=Anda belum melakukan download Kwitansi FS atau FS Letter (Menu Daftar Dokumen Service)")
        'End If



        'FreeServiceBBDataUpdate_Privilege
        m_bFreeServiceBBDataUpdate_Privilege = SecurityProvider.Authorize(Context.User, SR.FreeServiceDataUpdate_Privilege)

        'FreeServiceBBDataSave_Privilege
        m_bFreeServiceBBDataSave_Privilege = SecurityProvider.Authorize(Context.User, SR.FreeServiceDataSave_Privilege)

        If SecurityProvider.Authorize(Context.User, SR.FreeServiceDataSave_Privilege) Or m_bFreeServiceBBDataUpdate_Privilege Then
            btnSimpan.Visible = True
            btnBatal.Visible = True
        Else
            btnSimpan.Visible = False
            btnBatal.Visible = False
        End If

        'FreeServiceBBDataRelease_Privilege
        m_bFreeServiceBBDataRelease_Privilege = SecurityProvider.Authorize(Context.User, SR.FreeServiceDataRelease_Privilege)
        btnRilis.Visible = m_bFreeServiceBBDataRelease_Privilege

    End Sub

    Private Function IsDownloaded() As Boolean
        Dim _return As Boolean = False
        Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim ArlMonthly As ArrayList = New ArrayList
        Try
            Dim paramDate As DateTime = New DateTime(1900, 1, 1)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "ProductCategoryID", MatchType.Exact, "1"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "DealerCode", MatchType.Exact, objDealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(2, 4, 5, 12, 13, 14, 15)"))

            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Exact, "2017"), "((", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "4"), ")", False)

            'criterias.opOr(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Greater, "2017"), "(", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "1"), "))", False)

            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "CreatedTime", MatchType.GreaterOrEqual, Date.Now.AddMonths(-1)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.GreaterOrEqual, New DateTime(2017, 4, 1)))


            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.IsNull, True), "(", True)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.IsNull, True), ")", False)


            Dim dtn As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-2)


            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.LesserOrEqual, dtn))


            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(V_MonthlyReport), "Period", Sort.SortDirection.DESC))


            ArlMonthly = New V_MonthlyReportFacade(User).Retrieve(criterias, sortColl)
            If Not IsNothing(ArlMonthly) AndAlso ArlMonthly.Count > 0 Then
                Dim vM As New V_MonthlyReport
                vM = CType(ArlMonthly(0), V_MonthlyReport)

                If 1 = 1 OrElse (vM.Period.Year = dtn.Year AndAlso dtn.Month = vM.Period.Month) Then
                    _return = False
                Else
                    Return True
                End If



                _return = False
            Else
                _return = True
            End If
        Catch ex As Exception
            _return = False
        End Try
        Return _return
    End Function

    Private Function GetMonthlyFaultDescription() As String
        Dim _return As Boolean = False
        Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim ArlMonthly As ArrayList = New ArrayList
        Dim strMessage As String = String.Empty
        Try
            Dim paramDate As DateTime = New DateTime(1900, 1, 1)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "ProductCategoryID", MatchType.Exact, "1"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "DealerCode", MatchType.Exact, objDealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(2, 4, 5, 12, 13, 14, 15)"))

            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Exact, "2017"), "((", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "4"), ")", False)

            'criterias.opOr(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Greater, "2017"), "(", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "1"), "))", False)

            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "CreatedTime", MatchType.GreaterOrEqual, Date.Now.AddMonths(-1)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.GreaterOrEqual, New DateTime(2017, 4, 1)))


            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.IsNull, True), "(", True)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.IsNull, True), ")", False)


            Dim dtn As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-2)


            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.LesserOrEqual, dtn))


            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(V_MonthlyReport), "Period", Sort.SortDirection.DESC))

            ArlMonthly = New V_MonthlyReportFacade(User).Retrieve(criterias, sortColl)

            If Not IsNothing(ArlMonthly) AndAlso ArlMonthly.Count > 0 Then
                Dim itr As Integer = 0
                Dim currentYear As Integer = 0
                Dim currentMonth As Integer = 0
                strMessage = "||"
                For Each item As V_MonthlyReport In ArlMonthly
                    If (itr = 0) Then
                        currentYear = item.PeriodeYear
                        strMessage = "Year Periode : " & currentYear & "|Month : "
                    End If

                    If (item.PeriodeYear = currentYear) Then
                        If (currentMonth <> item.PeriodeMonth) Then
                            strMessage = strMessage & item.PeriodeMonth & ", "
                            currentMonth = item.PeriodeMonth
                        End If
                    Else
                        currentYear = item.PeriodeYear
                        strMessage = strMessage.Substring(0, strMessage.Length - 2) & "||Year Periode : " & currentYear & "|Month : " & item.PeriodeMonth & ", "
                        currentMonth = item.PeriodeMonth
                    End If

                    itr = itr + 1
                Next

                strMessage = strMessage.Substring(0, strMessage.Length - 2)

                'Dim lengthOfMessage As Integer = strMessage.Length
                'Dim lengthOfLastWord As Integer = 5
                'Dim lastWord1 As String = strMessage.Substring(lengthOfMessage - lengthOfLastWord, lengthOfLastWord)
                'Dim lastWord2 As String = strMessage.Substring(lengthOfMessage - (lengthOfLastWord + 1), (lengthOfLastWord + 1))
                'If (lastWord1 = "Month" Or lastWord2 = "Month ") Then

                'End If
                Return strMessage
            End If
        Catch ex As Exception
            strMessage = ex.Message
        End Try
        Return strMessage
    End Function


    Private Sub dgFreeServisBBEntry_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgFreeServisBBEntry.PageIndexChanged
        dgFreeServisBBEntry.SelectedIndex = -1
        dgFreeServisBBEntry.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dgFreeServisBBEntry.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dgFreeServisBBEntry_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgFreeServisBBEntry.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As FreeServiceBB = CType(e.Item.DataItem, FreeServiceBB)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgFreeServisBBEntry.CurrentPageIndex * dgFreeServisBBEntry.PageSize) 'getDataGridItemIndex()
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                e.Item.FindControl("lbtnDelete").Visible = m_bFreeServiceBBDataUpdate_Privilege
                Dim strMsg As String = "Yakin Data dengan nomor rangka " & CType(e.Item.FindControl("lblChassisNo"), Label).Text & "  ini akan dihapus? "
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & strMsg & "');")
            End If

            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                e.Item.FindControl("lbtnEdit").Visible = m_bFreeServiceBBDataUpdate_Privilege
            End If

            'If Not e.Item.FindControl("cbSelect") Is Nothing Then
            '    e.Item.FindControl("cbSelect").Visible = m_bFreeServiceBBDataRelease_Privilege
            'End If



            Dim lbltglJual As Label = CType(e.Item.FindControl("tglPenjualan"), Label)
            If Not IsNothing(RowValue.SoldDate) Then
                If RowValue.SoldDate = "1/1/1900" Then
                    lbltglJual.Text = ""
                End If
            End If

        End If
    End Sub

    Private Sub dgFreeServisBBEntry_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgFreeServisBBEntry.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtChassisMasterBB.ReadOnly = True
            txtKM.ReadOnly = True
            txtTglServis.ReadOnly = True
            txtTglJual.ReadOnly = True
            txtEngineNumber.ReadOnly = True
            txtWONumber.ReadOnly = True
            lblPopUpDealerBranch.Visible = False
            txtDealerBranchCode.ReadOnly = True
            ddlVisitType.Enabled = False
            'txtTglJual.ReadOnly = True
            ViewFreeServiceBB(e.Item.Cells(0).Text, False)

        ElseIf e.CommandName = "Edit" Then

            ViewState.Add("vsProcess", "Edit")
            ViewFreeServiceBB(e.Item.Cells(0).Text, True)
            dgFreeServisBBEntry.SelectedIndex = e.Item.ItemIndex
            txtChassisMasterBB.Enabled = True
            txtKM.Enabled = True
            txtTglServis.Enabled = True

            txtChassisMasterBB.ReadOnly = True
            txtKM.ReadOnly = False
            txtTglServis.ReadOnly = False
            txtTglJual.ReadOnly = False
            txtEngineNumber.ReadOnly = True
            txtWONumber.ReadOnly = False
            txtDealerBranchCode.ReadOnly = True
            lblPopUpDealerBranch.Visible = False

        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteFreeServisBB(e.Item.Cells(0).Text)
                MessageBox.Show("Hapus Sukses !")
            Catch ex As Exception
                MessageBox.Show("Gagal Menghapus !")
            End Try
            dgFreeServisBBEntry.SelectedIndex = -1
            ClearData()
        End If
    End Sub

    Private Function getMatch(ByVal ChassisNumber As String, ByVal EngineNumber As String) As Boolean
        Dim _match As Boolean = False

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strChassisNumber = txtChassisMasterBB.Text.Substring(1, Len(Trim(txtChassisMasterBB.Text)) - 1)
        Dim strEngineNumber = txtEngineNumber.Text.Trim()
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, strChassisNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "EngineNumber", MatchType.Exact, strEngineNumber))
        Dim ChassisColl As ArrayList = New ChassisMasterBBFacade(User).Retrieve(criterias)
        If ChassisColl.Count > 0 Then
            _match = True
        End If

        Return _match
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objFreeServiceBBFacade As FreeServiceBBFacade = New FreeServiceBBFacade(User)
        Dim objFreeServiceBB As FreeServiceBB = New FreeServiceBB
        Dim strChassisNumber = txtChassisMasterBB.Text.Substring(1, Len(Trim(txtChassisMasterBB.Text)) - 1)
        Dim strEngineNumber = txtEngineNumber.Text.Trim()
        If Not getMatch(strChassisNumber, strEngineNumber) Then
            MessageBox.Show("Nomor Mesin tidak sesuai")
            Return
        End If

        If ddlVisitType.SelectedIndex = 0 Then
            MessageBox.Show("Silahkan Pilih Tipe Visit")
            Return
        End If

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Dim strChassisNumber = txtChassisMasterBB.Text.Substring(1, Len(Trim(txtChassisMasterBB.Text)) - 1)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ChassisNumber", MatchType.Exact, strChassisNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
        Dim ChassisColl As ArrayList = New ChassisMasterBBFacade(User).Retrieve(criterias)
        If ChassisColl.Count > 0 Then
            If IsValidCMForFSSpecial(strChassisNumber) = False Then Exit Sub

            If CType(ViewState("vsProcess"), String) = "Insert" Then
                If m_bFreeServiceBBDataSave_Privilege Then
                    InsertobjFreeServiceBB(objFreeServiceBB)
                    dgFreeServisBBEntry.SelectedIndex = -1
                Else
                    MessageBox.Show("Anda tidak punya hak untuk menginsert data baru !")
                End If
            Else
                If m_bFreeServiceBBDataUpdate_Privilege Then
                    Dim objUpdateFreeServiceBB As FreeServiceBB = CType(Session.Item("vsFreeServiceBB"), FreeServiceBB)
                    UpdateObjFreeServiceBB(objUpdateFreeServiceBB)
                    dgFreeServisBBEntry.SelectedIndex = -1
                Else
                    MessageBox.Show("Anda tidak punya hak untuk mengupdate data lama !")
                End If
            End If
        Else
            MessageBox.Show("Chassis tidak terdaftar di " + companyCode)
        End If
        BindDatagrid(dgFreeServisBBEntry.CurrentPageIndex)
        Dim strScript As String
        strScript = "<script>document.all.txtChassisMasterBB.focus();</script>"
        Page.RegisterStartupScript("", strScript)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearData()
        txtDealerBranchCode.Text = String.Empty
        txtBranchName.Text = String.Empty
        dgFreeServisBBEntry.SelectedIndex = -1
        Dim strScript As String
        strScript = "<script>document.all.txtChassisMasterBB.focus();</script>"
        Page.RegisterStartupScript("", strScript)
    End Sub

    Private Function IsValidToExecute(ByVal oFS As FreeServiceBB) As Boolean
        Dim IsValid As Boolean = True
        Dim IsStatusA As Boolean = False
        Dim IsStatusB As Boolean = False
        Dim oD As Dealer = CType(Session.Item("DEALER"), Dealer)

        IsStatusA = IsTypeAllowed(oFS)
        If oD.DealerCode = "100016" AndAlso IsStatusA = False Then
            If ValidateLBUMBengkulu(oFS, IsStatusA) = False Then
                IsValid = False
            Else
                IsValid = True
            End If
        ElseIf oD.DealerCode = "100016" AndAlso IsStatusA = True Then
            If ValidateLBUMBengkulu(oFS, IsStatusA) = False Then
                IsValid = False
            Else
                IsValid = True
            End If
        End If
        Return IsValid
    End Function

    Private Sub btnRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        Dim bcheck As Boolean = False
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        'Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer) Just TEST
        Dim imp As SAPImpersonate = New SAPImpersonate("sap", "7Karakter", "172.17.31.21")
        'Dim imp As SAPImpersonate = New SAPImpersonate("sap", "7Karakter", "172.17.104.68")
        Dim success As Boolean = False
        Dim sTimestamp As String = sSuffix
        Dim FSFileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\FS\" & "FSData" & sTimestamp & ".fsd"
        Dim FSFileNameLocal As String = Server.MapPath("") & "\..\DataTemp\FSData" & sTimestamp & ".fsd"
        dgFreeServisBBEntry.DataSource = CType(Session("SessArrFreeServiceBB"), ArrayList)
        For Each dtgItem As DataGridItem In dgFreeServisBBEntry.Items
            If CType(dtgItem.Cells(0).FindControl("cbSelect"), CheckBox).Checked Then
                bcheck = True
                Exit For
            End If
        Next

        Try
            success = imp.Start
            'test
            success = True
            If bcheck And success Then
                Dim CheckedFSItemColl As ArrayList = New ArrayList
                Dim arlTransferedToSAP As New ArrayList
                CheckedFSItemColl = GetCheckedFSItem()

                Dim nSavedData As Integer = AppendText(CheckedFSItemColl, FSFileNameLocal, FSFileName, arlTransferedToSAP)
                If nSavedData < 1 Then
                    Dim sIndicator As String = ""
                    sIndicator = IIf(nSavedData = -1, ".", IIf(nSavedData = -1, ",", ""))
                    MessageBox.Show("Rilis data gagal" & sIndicator)
                    Exit Sub
                End If
                'sekarang updatenya
                Dim objFreeServisBBColl As ArrayList = New ArrayList
                If arlTransferedToSAP.Count > 0 Then
                    For Each ObjFreeServisBBe As FreeServiceBB In arlTransferedToSAP
                        ObjFreeServisBBe.ReleaseBy = User.Identity.Name
                        ObjFreeServisBBe.ReleaseDate = Today
                        ObjFreeServisBBe.Status = CType(EnumFSStatus.FSStatus.Proses, String)
                        objFreeServisBBColl.Add(ObjFreeServisBBe)
                    Next
                    Dim nResult = New FreeServiceBBFacade(User).UpdateFSCollection(objFreeServisBBColl)
                    If nResult = 0 Then
                        MessageBox.Show("Update Rilis Sukses")

                    Else
                        MessageBox.Show("Update Rilis gagal")
                    End If
                End If
            Else
                MessageBox.Show("Record Free Service belum dipilih !")
            End If
            imp.StopImpersonate()
            imp = Nothing
            BindDatagrid(dgFreeServisBBEntry.CurrentPageIndex)
            Dim strScript As String
            strScript = "<script>document.all.txtChassisMasterBB.focus();</script>"
            Page.RegisterStartupScript("", strScript)
        Catch ex As Exception
            MessageBox.Show("Update Rilis gagal !")
        End Try
    End Sub

    Public Function IsTypeAllowed(ByVal oFS As FreeServiceBB) As Boolean
        Dim i As Integer
        Dim _restrictedType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}

        For i = 0 To _restrictedType.Length - 1
            If oFS.ChassisMasterBB.VechileColor.VechileType.VechileTypeCode.Trim.ToLower = _restrictedType(i).Trim.ToLower Then
                Return False
            End If
        Next
        Return True
    End Function


    Private Function AppendText(ByVal ArrCheckedFSItem As ArrayList, ByVal FileNameLocal As String, ByVal filename As String, ByRef arlTransferedToSAP As ArrayList) As Integer ' Number of data sent to SAP
        Dim strText As New StringBuilder
        Dim objAl As New ArrayList
        Dim nData As Integer = 0

        Try
            nData = 0
            If ArrCheckedFSItem.Count > 0 Then
                strText = New StringBuilder
                For Each objFS As FreeServiceBB In ArrCheckedFSItem
                    Dim isAllowInsert As Boolean = True
                    If isAllowInsert Then
                        Dim arlistDealer As ArrayList
                        Dim objDealer As Dealer = New Dealer
                        Dim criteriasDealer As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasDealer.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, objFS.Dealer.ID))
                        arlistDealer = New DealerFacade(User).Retrieve(criteriasDealer)

                        If arlistDealer.Count > 0 Then
                            objDealer = CType(arlistDealer(0), Dealer)
                            strText.Append(objDealer.DealerCode)
                        End If
                        strText.Append(",")

                        Dim arlistChassis As ArrayList
                        Dim objChassisMasterBB As ChassisMasterBB = New ChassisMasterBB
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterBB), "ID", MatchType.Exact, objFS.ChassisMasterBB.ID))
                        arlistChassis = New ChassisMasterBBFacade(User).Retrieve(criterias)
                        If arlistChassis.Count > 0 Then
                            objChassisMasterBB = CType(arlistChassis(0), ChassisMasterBB)
                            strText.Append(objChassisMasterBB.ChassisNumber)
                        End If
                        strText.Append(",")

                        Dim arlistFSKind As ArrayList
                        Dim objFSKind As FSKind = New FSKind
                        Dim criteriasFSKind As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasFSKind.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "ID", MatchType.Exact, objFS.FSKind.ID))
                        arlistFSKind = New FSKindFacade(User).Retrieve(criteriasFSKind)
                        If arlistFSKind.Count > 0 Then
                            objFSKind = CType(arlistFSKind(0), FSKind)
                            strText.Append(objFSKind.KindCode)
                        End If
                        strText.Append(",")
                        Dim tgl As Date = objFS.ServiceDate.Date '.ToShortDateString
                        Dim tahun As String = tgl.Year.ToString
                        Dim bulan As String = tgl.Month.ToString
                        Dim tanggal As String = tgl.Day.ToString
                        If Len(bulan) = 1 Then bulan = "0" & bulan
                        If Len(tanggal) = 1 Then tanggal = "0" & tanggal
                        strText.Append(tanggal & bulan & tahun)

                        strText.Append(",")
                        If objFS.SoldDate < New Date(1901, 1, 1) Then
                            strText.Append(",")
                        Else
                            Dim tgl2 As Date = objFS.SoldDate '.ToShortDateString
                            Dim tahun2 As String = tgl2.Year.ToString
                            Dim bulan2 As String = tgl2.Month.ToString
                            Dim tanggal2 As String = tgl2.Day.ToString
                            If Len(bulan2) = 1 Then bulan2 = "0" & bulan2
                            If Len(tanggal2) = 1 Then tanggal2 = "0" & tanggal2
                            strText.Append(tanggal2 & bulan2 & tahun2)
                            strText.Append(",")
                        End If
                        strText.Append(objFS.MileAge.ToString)
                        strText.Append(",")
                        strText.Append(UserInfo.Convert(objFS.CreatedBy))
                        strText.Append(",")
                        Dim tglRelease As Date = Now.Date
                        Dim tahunRelease As String = tglRelease.Year.ToString
                        Dim bulanRelease As String = tglRelease.Month.ToString
                        Dim tanggalRelease As String = tglRelease.Day.ToString
                        If Len(bulanRelease) = 1 Then bulanRelease = "0" & bulanRelease
                        If Len(tanggalRelease) = 1 Then tanggalRelease = "0" & tanggalRelease
                        strText.Append(tanggalRelease & bulanRelease & tahunRelease)
                        strText.Append(vbNewLine)

                        arlTransferedToSAP.Add(objFS)
                        nData += 1
                    End If
                Next
                If nData > 0 Then
                    If Not Me.SaveToSAP(FileNameLocal, filename, strText) Then
                        nData = -2
                    End If
                End If
            End If
        Catch ex As Exception
            nData = -1 ' -1 means error occured
        End Try

        Return nData
    End Function

    Private Function SaveToSAP(ByVal DestFileLocal As String, ByVal DestFile As String, ByRef sb As StringBuilder) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim fInfoLocal As New FileInfo(DestFile)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                'Local
                If Not fInfoLocal.Directory.Exists Then Directory.CreateDirectory(fInfoLocal.DirectoryName)
                If fInfoLocal.Exists() Then fInfoLocal.Delete()
                Dim fs As FileStream = New FileStream(DestFileLocal, FileMode.CreateNew)
                sw = New StreamWriter(fs)
                sw.Write(sb.ToString)
                sw.Close()
                fs.Close()

                'Server
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If finfo.Exists Then
                    finfo.Delete()
                End If
                System.IO.File.Copy(DestFileLocal, DestFile)
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            success = False
            sw.Close()
        End Try
        Return success
    End Function

    Public Sub CopyFileToSAPServer(ByVal _fileName As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
        Dim _sapServerFolder As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder") & "\Service\FS\"
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
        If Not finfo.Directory.Exists Then
            finfo.Directory.Create()
        End If
    End Sub

    Private Function WriteFileToLocalHost(ByVal str As String, ByVal fileName As String) As Boolean
        Try
            Dim objFileStream As New FileStream(fileName, FileMode.Append, FileAccess.Write)
            Dim objStreamWriter As New StreamWriter(objFileStream)
            objStreamWriter.WriteLine(str)
            objStreamWriter.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub txtDealerBranchCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDealerBranchCode.TextChanged
        BindDatagrid(0)
    End Sub

#End Region

    Private Sub dgFreeServisBBEntry_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgFreeServisBBEntry.SortCommand

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

        dgFreeServisBBEntry.SelectedIndex = -1
        dgFreeServisBBEntry.CurrentPageIndex = 0
        bindGridSorting(dgFreeServisBBEntry.CurrentPageIndex)
    End Sub

    Private Sub btnSimpan_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.ServerClick
        btnSave_Click(sender, e)
    End Sub

    Private Sub btnBatal_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.ServerClick
        btnCancel_Click(sender, e)
    End Sub

    Private Sub btnRilis_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRilis.ServerClick
        btnRelease_Click(sender, e)
    End Sub

    Private Function ValidateFSKindOnVehicleType(ByVal objFreeServiceBB As FreeServiceBB) As Boolean
        Try
            Dim VechileTypeID As Integer = objFreeServiceBB.ChassisMasterBB.VechileColor.VechileType.ID
            Dim FSKindID As Integer = objFreeServiceBB.FSKind.ID
            Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, FSKindID))
            critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, VechileTypeID))

            Return New FSKindOnVechileTypeFacade(User).Retrieve(critComp).Count > 0
        Catch
            Return False
        End Try
    End Function

End Class