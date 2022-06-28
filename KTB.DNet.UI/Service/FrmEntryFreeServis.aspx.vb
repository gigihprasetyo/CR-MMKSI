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
Imports KTB.DNet.BusinessValidation
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade
Imports System.Text.RegularExpressions
Imports System.Collections
Imports System.Linq
Imports System.Collections.Generic

#End Region

Public Class FrmEntryFreeServis
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents dgFreeServisEntry As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnRelease As System.Web.UI.WebControls.Button
    Protected WithEvents btnRilis As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblFSKind As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator4 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator3 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents lblKM As System.Web.UI.WebControls.Label
    Protected WithEvents LblTglServis As System.Web.UI.WebControls.Label
    Protected WithEvents lblTgl As System.Web.UI.WebControls.Label
    Protected WithEvents txtChassisMaster As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEngineNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKM As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTglServis As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTglJual As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerBranchCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBranchName As TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnBatal As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblVisitType As Label
    Protected WithEvents txtWONumber As TextBox

    Protected WithEvents ddlKind As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlVisitType As System.Web.UI.WebControls.DropDownList

    Protected WithEvents lbtnChassisLoad As Button
    Protected WithEvents lblMaxFileSize As System.Web.UI.WebControls.Label
    Protected WithEvents lblSupportedFormat As System.Web.UI.WebControls.Label
    Protected WithEvents iFSEvidence As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents lblUploadedFile As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents hdnMinSize As System.Web.UI.WebControls.HiddenField

    Private _sessHelper As SessionHelper = New SessionHelper
    Private m_bFormPrivilege As Boolean = False
    Private m_bFreeServiceDataUpdate_Privilege As Boolean = False
    Private m_bFreeServiceDataDelete_Privilege As Boolean = False
    Private m_bFreeServiceDataRelease_Privilege As Boolean = False
    Private m_bFreeServiceDataSave_Privilege As Boolean = False

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
    Dim rilisItem As FreeService
    Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)

    Private Sub BindVisitType()
        With ddlVisitType.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", ""))
            .Add(New ListItem("Walk In", "WI"))
            .Add(New ListItem("Booking", "BO"))
        End With
    End Sub
#End Region
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++
#Region "Custom Method"

    Private Sub DeleteFreeServis(ByVal nID As Integer)
        Dim objFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
        Dim objFreeServis As FreeService = New FreeServiceFacade(User).Retrieve(nID)
        objFreeServiceFacade.DeleteFromDB(objFreeServis)
        If Not objFreeServis.FilePath = String.Empty Then
            deleteSavedAttachment(objFreeServis.FilePath)
        End If
        BindDatagrid(dgFreeServisEntry.CurrentPageIndex)
    End Sub

    Private Function getMatch(ByVal ChassisNumber As String, ByVal EngineNumber As String) As Boolean
        Dim _match As Boolean = False

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strChassisNumber = txtChassisMaster.Text.Trim()
        Dim strEngineNumber = txtEngineNumber.Text.Trim()
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, strChassisNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, strEngineNumber))
        Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
        If ChassisColl.Count > 0 Then
            _match = True
        End If

        Return _match
    End Function

    Private Sub ViewFreeService(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objFreeService As FreeService = New FreeServiceFacade(User).Retrieve(nID)
        txtDealerBranchCode.Text = String.Empty
        txtBranchName.Text = String.Empty
        'Todo session
        Session.Add("vsFreeService", objFreeService)
        If Not IsNothing(objFreeService.DealerBranch) Then
            txtDealerBranchCode.Text = objFreeService.DealerBranch.DealerBranchCode
            txtBranchName.Text = objFreeService.DealerBranch.Name
        End If
        txtChassisMaster.Text = objFreeService.ChassisMaster.ChassisNumber
        txtKM.Text = objFreeService.MileAge
        txtEngineNumber.Text = objFreeService.ChassisMaster.EngineNumber
        ddlKind.SelectedValue = objFreeService.FSKind.KindCode
        ddlVisitType.SelectedValue = objFreeService.VisitType
        txtWONumber.Text = objFreeService.WorkOrderNumber
        If IsNothing(objFreeService.ServiceDate) Or objFreeService.ServiceDate = "1/1/1900" Then
            txtTglServis.Text = ""
        Else
            txtTglServis.Text = Format(objFreeService.ServiceDate, "ddMMyyyy")
        End If

        If IsNothing(objFreeService.SoldDate) Or objFreeService.SoldDate = "1/1/1900" Then
            txtTglJual.Text = ""
        Else
            txtTglJual.Text = Format(objFreeService.SoldDate, "ddMMyyyy")
        End If

        lblUploadedFile.Text = objFreeService.FileName

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

    Private Sub InsertobjFreeService(ByVal objFreeService As FreeService)
        Dim objFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
        Dim nResult As Integer = -1
        Dim bCheck As Boolean = True
        Dim ObjChassisMasterCheck As ChassisMaster
        Dim ObjServiceDealer As Dealer
        Dim ObjSoldDealer As Dealer
        Dim ObjDealerBranch As DealerBranch


        If Not txtChassisMaster.Text = String.Empty Then
            'added by anh 20130703
            Dim cFSKind As Char() = txtChassisMaster.Text.ToCharArray()
            Dim iCnt As Integer = 0
            For Each _c As Char In cFSKind
                If IsNumeric(_c) Then
                    iCnt = iCnt + 1
                Else
                    Exit For
                End If
            Next
            Dim fsKindCode As String = String.Empty
            '   fsKindCode = txtChassisMaster.Text.Substring(0, iCnt)
            fsKindCode = ddlKind.SelectedValue.ToString()
            'end added by anh 20130703

            If bCheck Then
                'validasi km ke 1 
                If txtKM.Text.Trim() = "" Then
                    MessageBox.Show("Jarak tempuh tidak boleh kosong !")
                    bCheck = False
                    Return
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
                    Return
                End If
            End If

            If bCheck Then
                If ddlVisitType.SelectedIndex = 0 Then
                    MessageBox.Show("Silahkan Pilih tipe visit")
                    bCheck = False
                End If
            End If

            '---check is fleet available, add by wdi 20161006
            Dim bolIsFleetExists As Boolean = False
            'Dim critFleetFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'critFleetFaktur.opAnd(New Criteria(GetType(FleetFaktur), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
            'Dim arrFleetFaktur As ArrayList = New FleetFakturFacade(User).Retrieve(critFleetFaktur)

            'Dim objFleetFaktur As FleetFaktur
            'If arrFleetFaktur.Count > 0 Then
            '    objFleetFaktur = arrFleetFaktur(0)
            'End If

            'If Not IsNothing(objFleetFaktur) Then
            '    objFreeService.FleetRequest = objFleetFaktur.FleetRequest
            '    bolIsFleetExists = True
            'End If

            'Centralize Check Fleet
            If Not IsNothing(objFreeService.FleetRequest) Then
                bolIsFleetExists = True
            End If
            '---end check fleet

            'If bCheck Then
            '    'validasi km ke 4 dan validasi jenis FS (FSKInd)

            '    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    'critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "KindCode", MatchType.Exact, txtChassisMaster.Text.Substring(0, 1).ToString()))
            '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "KindCode", MatchType.Exact, fsKindCode))

            '    Dim FSColl As ArrayList = New FSKindFacade(User).Retrieve(critFS)
            '    If FSColl.Count > 0 Then
            '        objFreeService.FSKind = FSColl(0)
            '        If CType(txtKM.Text, Integer) > objFreeService.FSKind.KM AndAlso Not bolIsFleetExists Then
            '            MessageBox.Show("Jarak tempuh melebihi batas maksimum paket " + objFreeService.FSKind.KM.ToString)
            '            bCheck = False
            '            Return
            '        Else
            '            'disini baru lolos validasi
            '            ''Penyamaan dengan upload
            '            'Dim MinValue As Integer = 0
            '            'Dim crtFS As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '            'Dim srtFS As New SortCollection
            '            'Dim arlFS As New ArrayList
            '            'crtFS.opAnd(New Criteria(GetType(FSKind), "KM", MatchType.Lesser, objFreeService.FSKind.KM))
            '            'srtFS.Add(New Sort(GetType(FSKind), "KM", Sort.SortDirection.DESC))
            '            'arlFS = New FSKindFacade(User).Retrieve(crtFS, srtFS)
            '            'If arlFS.Count > 0 Then
            '            '    MinValue = CType(arlFS(0), FSKind).KM + 1
            '            'End If
            '            'If CType(txtKM.Text, Integer) > MinValue Then
            '            '    MessageBox.Show("Jarak tempuh tidak sesuai dengan batas jenis free service ")
            '            '    bCheck = False
            '            'End If
            '            ''end of Penyamaan dengan upload
            'End If
            '    Else
            '        MessageBox.Show("Kode Jenis Free Servis tidak terdaftar !")
            '    bCheck = False
            '        Return
            '    End If
            'End If
            Dim mspExCheckStat As Boolean = False

            'validasi KM dan Valid Date untuk extended
            'If bCheck Then
            '    Dim ValidKM As Integer = 0
            '    Dim ValidDate As Date
            '    Dim critFSExt As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    critFSExt.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKindCode))
            '    critFSExt.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSType", MatchType.InSet, "(7,8,9)"))

            '    Dim arlFSExt As ArrayList = New FSKindOnVechileTypeFacade(User).Retrieve(critFSExt)
            '    If arlFSExt.Count > 0 Then
            '        Dim critFSSts As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        critFSSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))

            '        Dim arlFSSts As ArrayList = New MSPExRegistrationFacade(User).Retrieve(critFSSts)
            '        If arlFSSts.Count > 0 Then
            '            Dim lastMSPEx As MSPExRegistration = CType(arlFSSts(arlFSSts.Count - 1), MSPExRegistration)
            '            ValidKM = lastMSPEx.ValidKMTo
            '            ValidDate = lastMSPEx.ValidDateTo
            '            If CType(txtKM.Text, Integer) > ValidKM Then
            '                MessageBox.Show("Jarak tempuh dan Tanggal Service melebihi batas maksimum paket " + ValidKM.ToString)
            '                bCheck = False
            '                Return
            '            End If
            '            If ToDate(txtTglServis.Text.Trim) > ValidDate Then
            '                'validate msp ex
            '                If lastMSPEx.Status = 4 Then
            '                    If ValidDate >= DateTime.Now Then
            'Dim svcCount As Integer = New MSPExRegistrationFacade(User).RetrieveCountFS(txtChassisMaster.Text, lastMSPEx.ID)
            '                        Dim maxPM As Integer = GetMaxPM(lastMSPEx.MSPExMaster.MSPExType).Count
            '                        If svcCount < maxPM Then
            '                            mspExCheckStat = True
            '                        Else
            '                            mspExCheckStat = False
            '                        End If
            '                    End If
            '                End If
            '                If Not mspExCheckStat Then
            '                    MessageBox.Show("Tanggal Service melebihi batas maksimum paket " + ValidDate.ToShortDateString)
            '                    bCheck = False
            '                    Return
            '                End If
            '            End If
            '        End If
            '    End If
            'End If

            'validasi tidak boleh ambil kode free service yang telah digunakan
            'Dim MSPExValidStatus As Boolean = False
            'If bCheck Then
            '    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "FSKind.KindCode", MatchType.Exact, fsKindCode))
            '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))

            '    Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
            '    If arlFS.Count > 0 Then
            '        Dim critFSSts As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        critFSSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
            '        critFSSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "Status", MatchType.Exact, 4))
            '        Dim arlFSSts As ArrayList = New MSPExRegistrationFacade(User).Retrieve(critFSSts)
            '        If arlFSSts.Count > 0 Then
            '            Dim lastMSPEx As MSPExRegistration = CType(arlFSSts(arlFSSts.Count - 1), MSPExRegistration) 'ambil registrasi MPS terakhir
            '            If lastMSPEx.ValidDateTo >= DateTime.Now Then
            '                Dim FSHistoryAfterLastMSPCounter As Integer = 0
            '                Dim isUsedFSKind As Boolean = False
            '                For Each f As FreeService In arlFS
            '                    If f.CreatedTime >= lastMSPEx.CreatedTime Then
            '                        FSHistoryAfterLastMSPCounter += 1
            '                        If fsKindCode = f.FSKind.KindCode Then
            '                            isUsedFSKind = True
            '                        End If
            '                    End If
            '                Next
            '                If FSHistoryAfterLastMSPCounter < GetMaxPM(lastMSPEx.MSPExMaster.MSPExType).Count And Not isUsedFSKind Then
            '                    MSPExValidStatus = True
            '                End If
            '            End If
            '        End If

            '        If Not MSPExValidStatus Then
            '            MessageBox.Show("Kode Jenis Free Servis telah digunakan, harap pilih kode jenis free servis lain")
            '            bCheck = False
            '            Return
            '        End If
            '    End If
            'End If

            'validasi fs extended pada MSPExregistration
            'to-be centralize FreeServiceValidation line 145
            'If bCheck Then
            '    Dim critFSExt As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    critFSExt.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKindCode))
            '    Dim arlFS As ArrayList = New FSKindOnVechileTypeFacade(User).Retrieve(critFSExt)
            '    If arlFS.Count > 0 Then
            '        If CType(arlFS(0), FSKindOnVechileType).FSType = "7" OrElse CType(arlFS(0), FSKindOnVechileType).FSType = "8" OrElse CType(arlFS(0), FSKindOnVechileType).FSType = "9" Then
            '            Dim critFSSts As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '            critFSSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
            '            Dim arlFSSts As ArrayList = New MSPExRegistrationFacade(User).Retrieve(critFSSts)
            '            If arlFSSts.Count <= 0 Then
            '                MessageBox.Show("Kendaraan tidak mendapatkan Extended Service") 'ADA
            '                bCheck = False
            '                Return
            '            Else
            '                Dim StatusMSPEx As String
            '                StatusMSPEx = CType(arlFSSts(0), MSPExRegistration).Status
            '                'check status MSPExRegist sudah selesai?
            '                If StatusMSPEx <> CType(EnumMSPEx.MSPExStatus.Selesai, Short) Then
            '                    MessageBox.Show("Data tidak dapat disimpan karena Status MSPExRegistration belum selesai ") 'ADA
            '                    bCheck = False
            '                    Return
            '                End If

            '                'validasi FSKind Extended harus sesuai dg yg teregist
            '                Dim CodeMSPEx1 As String
            '                Dim CodeMSPEx2 As String

            '                Dim MspId As Integer = CType(arlFSSts(0), MSPExRegistration).MSPExMaster.ID
            '                Dim mSPEx As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '                mSPEx.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExMaster), "ID", MatchType.Exact, MspId))

            '                Dim armSPEx As ArrayList = New MSPExMasterFacade(User).Retrieve(mSPEx)
            '                If armSPEx.Count > 0 Then
            '                    Dim MspIdEx As Integer = CType(armSPEx(0), MSPExMaster).MSPExType.ID
            '                    Dim mSPExType As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '                    mSPExType.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExType), "ID", MatchType.Exact, MspIdEx))
            '                    Dim armSPExType As ArrayList = New MSPExTypeFacade(User).Retrieve(mSPExType)
            '                    If armSPExType.Count > 0 Then
            '                        CodeMSPEx1 = CType(armSPExType(0), MSPExType).Code
            '                        CodeMSPEx2 = CType(arlFS(0), FSKindOnVechileType).FSType
            '                        If CodeMSPEx1 = "2XPM" And CodeMSPEx2 <> "7" Then
            '                            MessageBox.Show("Kendaraan tidak berhak menggunakan Kode Jenis Free Servis " + CType(arlFS(0), FSKindOnVechileType).FSKind.KindDescription)
            '                            bCheck = False
            '                            Return
            '                        End If
            '                        If CodeMSPEx1 = "4XPM" And CodeMSPEx2 <> "8" Then
            '                            MessageBox.Show("Kendaraan tidak berhak menggunakan Kode Jenis Free Servis " + CType(arlFS(0), FSKindOnVechileType).FSKind.KindDescription)
            '                            bCheck = False
            '                            Return
            '                        End If
            '                        If CodeMSPEx1 = "6XPM" And CodeMSPEx2 <> "9" Then
            '                            MessageBox.Show("Kendaraan tidak berhak menggunakan Kode Jenis Free Servis " + CType(arlFS(0), FSKindOnVechileType).FSKind.KindDescription)
            '                            bCheck = False
            '                            Return
            '                        End If
            '                    End If
            '                End If
            '            End If
            '        End If
            '    End If
            'End If

            'If bCheck Then
            '    Dim critFSSts As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    critFSSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))

            '    Dim StatusMSPEx As String
            '    Dim ValidDate As Date
            '    Dim arlFSSts As ArrayList = New MSPExRegistrationFacade(User).Retrieve(critFSSts)
            '    If arlFSSts.Count > 0 Then
            '        StatusMSPEx = CType(arlFSSts(0), MSPExRegistration).Status
            '        If StatusMSPEx <> CType(EnumMSPEx.MSPExStatus.Selesai, Short) Then
            '            MessageBox.Show("Data tidak dapat disimpan karena Status MSPExRegistration belum selesai ")
            '            bCheck = False
            '            Return
            '        End If
            '    End If
            'End If

            'validasi FSKind Extended validate date masih aktiv
            'to-be Centralize FreeServiceValidation line 141
            'If bCheck Then
            '    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "FSKind.KindCode", MatchType.Exact, fsKindCode))
            '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))

            '    Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
            '    If arlFS.Count > 0 Then
            '        Dim ValidDate As Date
            '        Dim StatusMSPEx As String
            '        Dim critFSts As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        critFSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))

            '        Dim arlFSts As ArrayList = New MSPExRegistrationFacade(User).Retrieve(critFSts)
            '        If arlFSts.Count > 0 Then
            '            StatusMSPEx = CType(arlFSts(0), MSPExRegistration).Status
            '            If StatusMSPEx = CType(EnumMSPEx.MSPExStatus.Selesai, Short) Then
            '                ValidDate = CType(arlFSts(0), MSPExRegistration).ValidDateTo
            '                If ValidDate >= Now.ToShortDateString Then
            '                    MessageBox.Show("Kind Code sudah pernah diajukan dalam satu paket ESP aktif yang sama ")
            '                    bCheck = False
            '                    Return
            '                End If
            '            End If
            '        End If
            '    End If
            'End If
            '------------end bugfix tidak bisa inpout double claim msp---------------'

            'validasi FSKind Extended harus sesuai dg yg teregist
            'If bCheck Then
            '    Dim CodeMSPEx1 As String
            '    Dim CodeMSPEx2 As String
            '    Dim CodeMSPEx3 As String
            '    Dim critFSts As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    critFSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))

            '    Dim arlFSts As ArrayList = New MSPExRegistrationFacade(User).Retrieve(critFSts)
            '    If arlFSts.Count > 0 Then
            '        Dim critFSExt As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        critFSExt.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKindCode))
            '        Dim arlFSExt As ArrayList = New FSKindOnVechileTypeFacade(User).Retrieve(critFSExt)

            '        Dim MspId As Integer = CType(arlFSts(0), MSPExRegistration).MSPExMaster.ID
            '        Dim mSPEx As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        mSPEx.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExMaster), "ID", MatchType.Exact, MspId))
            '        Dim armSPEx As ArrayList = New MSPExMasterFacade(User).Retrieve(mSPEx)

            '        If armSPEx.Count > 0 And arlFSExt.Count > 0 Then
            '            Dim MspIdEx As Integer = CType(armSPEx(0), MSPExMaster).MSPExType.ID
            '            Dim mSPExType As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '            mSPExType.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExType), "ID", MatchType.Exact, MspIdEx))
            '            Dim armSPExType As ArrayList = New MSPExTypeFacade(User).Retrieve(mSPExType)
            '            If armSPExType.Count > 0 Then
            '                CodeMSPEx1 = CType(armSPExType(0), MSPExType).Code
            '                CodeMSPEx2 = CType(arlFSExt(0), FSKindOnVechileType).FSType
            '                If CodeMSPEx1 = "2XPM" And CodeMSPEx2 <> "7" Then
            '                    MessageBox.Show("Kendaraan tidak berhak menggunakan Kode Jenis Free Servis " + CType(arlFSExt(0), FSKindOnVechileType).FSKind.KindDescription)
            '                    bCheck = False
            '                    Return
            '                End If
            '                If CodeMSPEx1 = "4XPM" And CodeMSPEx2 <> "8" Then
            '                    MessageBox.Show("Kendaraan tidak berhak menggunakan Kode Jenis Free Servis " + CType(arlFSExt(0), FSKindOnVechileType).FSKind.KindDescription)
            '                    bCheck = False
            '                    Return
            '                End If
            '                If CodeMSPEx1 = "6XPM" And CodeMSPEx2 <> "9" Then
            '                    MessageBox.Show("Kendaraan tidak berhak menggunakan Kode Jenis Free Servis " + CType(arlFSExt(0), FSKindOnVechileType).FSKind.KindDescription)
            '                    bCheck = False
            '                    Return
            '                End If
            '            End If

            '        End If
            '    End If
            'End If

            'validasi tdk boleh ambil kode fs yang angka depannya telah digunakan (ex. telah save 7A, maka tdk boleh ambil 7D)
            'If bCheck Then
            '    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))

            '    Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
            '    For Each oFS As FreeService In arlFS
            '        If Regex.Replace(oFS.FSKind.KindCode, "[^0-9]", "") = Regex.Replace(fsKindCode, "[^0-9]", "") Then
            '            If Not MSPExValidStatus Then
            '                MessageBox.Show("Anda tidak bisa memilih kode free service " + fsKindCode + " karena telah menggunakan kode free service " + oFS.FSKind.KindCode)
            '                bCheck = False
            '                Return
            '            End If
            '        End If
            '    Next
            'End If

            'validasi durasi
            'If bCheck Then
            '    Dim oDatePembanding As Date
            '    Dim oChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(txtChassisMaster.Text)

            '    If Not IsNothing(oChassisMaster) Then
            '        Dim critCMPKT As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        critCMPKT.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterPKT), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))

            '        Dim arlCMPKT As New ArrayList
            '        arlCMPKT = New ChassisMasterPKTFacade(User).Retrieve(critCMPKT)
            '        Dim isSoldDealer As Boolean = False
            '        Dim DealerLogin As Dealer = CType(Session("Dealer"), Dealer)
            '        If Not IsNothing(DealerLogin) Then
            '            isSoldDealer = (oChassisMaster.Dealer.ID = DealerLogin.ID)
            '        End If

            '        If 1 = 1 Then
            '            Dim YearDurationVal As Integer = 2019
            '            Dim MonthDurationVal As Integer = 9
            '            Dim critAppConf As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '            critAppConf.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "Name", MatchType.InSet, "(" + "'YearFSDurationValidation','MonthFSDurationValidation'" + ")"))
            '            Dim srtAppConf As New SortCollection
            '            srtAppConf.Add(New Sort(GetType(AppConfig), "ID", Sort.SortDirection.ASC))

            '            Dim arlAppConf As ArrayList = New AppConfigFacade(User).Retrieve(critAppConf, srtAppConf)
            '            If arlAppConf.Count > 0 Then
            '                MonthDurationVal = arlAppConf(0).Value
            '                YearDurationVal = arlAppConf(1).Value
            '            End If
            '            Dim oCMPKT As ChassisMasterPKT
            '            If isSoldDealer Then
            '                If arlCMPKT.Count = 0 Then
            '                    MessageBox.Show("Nomor Chassis belum memiliki tanggal PKT")
            '                    bCheck = False
            '                    Return
            '                End If

            '            End If

            '            If arlCMPKT.Count = 0 Then
            'If Not IsNothing(oChassisMaster.EndCustomer) AndAlso oChassisMaster.EndCustomer.FakturDate.Year > 1900 Then
            '    oDatePembanding = oChassisMaster.EndCustomer.FakturDate
            'ElseIf Not IsNothing(oChassisMaster.EndCustomer) AndAlso oChassisMaster.EndCustomer.OpenFakturDate.Year > 1900 Then
            '    oDatePembanding = oChassisMaster.EndCustomer.OpenFakturDate
            'Else
            '    MessageBox.Show("Nomor Chassis belum Open Faktur")
            '    bCheck = False
            '    Return
            'End If
            '            Else
            '                oCMPKT = CType(arlCMPKT(0), ChassisMasterPKT)
            '                If (oCMPKT.PKTDate.Year = YearDurationVal AndAlso oCMPKT.PKTDate.Month < MonthDurationVal) OrElse oCMPKT.PKTDate.Year < YearDurationVal Then
            '                    oDatePembanding = oChassisMaster.EndCustomer.FakturDate
            '                Else
            '                    oDatePembanding = oCMPKT.PKTDate
            '                End If
            '            End If

            '            Dim critFSKindOnVT As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '            critFSKindOnVT.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKindCode))
            '            critFSKindOnVT.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "VechileType.ID", MatchType.Exact, oChassisMaster.VechileColor.VechileType.ID))

            '            Dim arlFSKindOnVT As ArrayList = New FSKindOnVechileTypeFacade(User).Retrieve(critFSKindOnVT)

            '            If arlFSKindOnVT.Count > 0 Then
            '                Dim oFSKindOnVT As FSKindOnVechileType = CType(arlFSKindOnVT(0), FSKindOnVechileType)
            '                Dim ts As TimeSpan = ToDate(txtTglServis.Text.Trim).Subtract(oDatePembanding) 'DateTime.Now.Subtract(oDatePembanding)

            '                Dim DayDifference As Integer = Convert.ToInt32(ts.Days)
            '                Dim DefaultExtraDate As Integer = 0

            '                Dim critAppConf2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '                critAppConf2.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "Name", MatchType.Exact, "ExtraDurationDateFS"))
            '                Dim arlAppConf2 As ArrayList = New AppConfigFacade(User).Retrieve(critAppConf2)
            '                If arlAppConf2.Count > 0 Then
            '                    DefaultExtraDate = CType(arlAppConf(0).Value, Integer)
            '                End If
            '                If DayDifference > (oFSKindOnVT.Duration + DefaultExtraDate) Then
            '                    MessageBox.Show("Tanggal service melebihi tanggal yang seharusnya")
            '                    bCheck = False
            '                    Return
            '                End If
            '            End If
            '        End If
            '    Else
            '        MessageBox.Show("Nomor Chassis tidak ditemukan")
            '        bCheck = False
            '        Return
            '    End If
            'End If

            'start validasi jika memiliki msp dan bukan dari promo maka harus melakukan pembayaran dahulu
            'to-be Centralize FreeServiceValidation line 148
            'If bCheck Then
            '    Dim oChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(txtChassisMaster.Text)
            '    If Not IsNothing(oChassisMaster) Then
            '        Dim critMSPRegHistory As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPRegistrationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        critMSPRegHistory.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistrationHistory), "Status", MatchType.Exact, CType(EnumStatusMSP.Status.Selesai, Integer)))
            '        critMSPRegHistory.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistrationHistory), "BenefitMasterHeaderID", MatchType.Exact, 0)) 'BenefitMasterHeaderID 0 = paid, !0 = promo
            '        critMSPRegHistory.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistrationHistory), "MSPRegistration.ChassisMaster.ID", MatchType.Exact, oChassisMaster.ID))
            '        critMSPRegHistory.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistrationHistory), "MSPRegistration.Dealer.ID", MatchType.Exact, oChassisMaster.Dealer.ID))
            '        Dim srtMSPRegHistory As New SortCollection
            '        srtMSPRegHistory.Add(New Sort(GetType(MSPRegistrationHistory), "ID", Sort.SortDirection.DESC))

            '        Dim arlMSPRegHistory As ArrayList = New MSPRegistrationHistoryFacade(User).Retrieve(critMSPRegHistory, srtMSPRegHistory)
            '        If arlMSPRegHistory.Count > 0 Then
            '            Dim oMSPRegHistory As MSPRegistrationHistory = New MSPRegistrationHistory
            '            oMSPRegHistory = CType(arlMSPRegHistory(0), MSPRegistrationHistory)

            '            Dim critMSPTransPaymentD As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPTransferPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '            critMSPTransPaymentD.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPTransferPaymentDetail), "MSPTransferPayment.Status", MatchType.Exact, CType(EnumStatusMSP.Status.Selesai, Integer)))
            '            critMSPTransPaymentD.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPTransferPaymentDetail), "MSPRegistrationHistory.ID", MatchType.Exact, oMSPRegHistory.ID))

            '            Dim arlMSPTransPaymentD As ArrayList = New MSPTransferPaymentDetailFacade(User).Retrieve(critMSPTransPaymentD)
            '            If arlMSPTransPaymentD.Count = 0 Then
            '                MessageBox.Show("Mohon melakukan pembayaran MSP terlebih dahulu")
            '                bCheck = False
            '                Return
            '            End If
            '        End If

            '    Else
            '        MessageBox.Show("Nomor Chassis tidak ditemukan")
            '        bCheck = False
            '        Return
            '    End If
            'End If
            'end validasi jika memiliki msp dan bukan dari promo maka harus melakukan pembayaran dahulu

            'validasi tgl servis
            'If bCheck Then
            '    If txtTglServis.Text.Trim <> "" Then
            '        If Len(txtTglServis.Text.Trim) = 8 Then
            '            If IsValidDate(txtTglServis.Text.Trim) Then
            '                If ToDate(txtTglServis.Text.Trim) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
            '                    ToDate(txtTglServis.Text.Trim) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
            '                    MessageBox.Show("Format tanggal service salah")
            '                    bCheck = False
            '                Else
            '                    objFreeService.ServiceDate = ToDate(txtTglServis.Text.Trim)
            '                End If
            '            Else
            '                MessageBox.Show("Format tgl. service salah")
            '                bCheck = False
            '            End If
            '        Else
            '            bCheck = False
            '            MessageBox.Show("Format tgl. service salah")
            '        End If

            '        'validasi tgl servis tdk boleh sama
            '        Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
            '        critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.Exact, ToDate(txtTglServis.Text.Trim)))
            '        Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
            '        If arlFS.Count > 0 Then
            '            MessageBox.Show("Chassis " + txtChassisMaster.Text + " Anda sudah mengajukan service pada tanggal yang sama!!")
            '            bCheck = False
            '        End If

            '    Else
            '        bCheck = False
            '        MessageBox.Show("Tgl. service kosong")
            '    End If
            'End If

            'If bCheck Then
            '    Dim objDealerSold As Dealer = New Dealer
            '    Dim objDealerFS As Dealer = New Dealer
            '    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    'Dim strChassisNumber = txtChassisMaster.Text.Substring(1, Len(Trim(txtChassisMaster.Text)) - 1)
            '    ' Dim strChassisNumber = txtChassisMaster.Text.Substring(Len(fsKindCode), Len(Trim(txtChassisMaster.Text)) - Len(fsKindCode))
            '    Dim strChassisNumber = txtChassisMaster.Text.Trim()
            '    Dim strEngineNumber = txtEngineNumber.Text.Trim()
            '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, strChassisNumber))
            '    Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
            '    If ChassisColl.Count > 0 Then
            '        objFreeService.ChassisMaster = ChassisColl(0)

            '        Dim critDealerSold As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        critDealerSold.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, objFreeService.ChassisMaster.Dealer.ID))
            '        Dim DealerCollSold As ArrayList = New DealerFacade(User).Retrieve(critDealerSold)
            '        If DealerCollSold.Count > 0 Then
            '            objDealerSold = DealerCollSold(0)
            '        End If

            '        Dim critDealerFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
            '        critDealerFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strDealerCode))
            '        Dim DealerCollFS As ArrayList = New DealerFacade(User).Retrieve(critDealerFS)
            '        If DealerCollFS.Count > 0 Then
            '            objDealerFS = DealerCollFS(0)
            '        End If
            '    Else
            '        MessageBox.Show("Chassis tidak terdaftar")
            '        Return
            '        bCheck = False
            '    End If

            '    'validasi tgl penjualan
            '    If txtTglJual.Text.Trim <> "" Then
            '        If Len(txtTglJual.Text.Trim) = 8 Then
            '            If IsValidDate(txtTglJual.Text.Trim) Then
            '                If ToDate(txtTglServis.Text.Trim) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
            '                   ToDate(txtTglServis.Text.Trim) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
            '                    MessageBox.Show("Format tanggal jual salah")
            '                    Return
            '                    bCheck = False
            '                Else
            '                    objFreeService.SoldDate = ToDate(txtTglJual.Text.Trim)
            '                End If
            '            Else
            '                MessageBox.Show("Format tgl. penjualan salah ")
            '                bCheck = False
            '                Return
            '            End If
            '        Else
            '            bCheck = False
            '            MessageBox.Show("Format tgl. penjualan salah")
            '            Return
            '        End If
            '    Else
            '        'Jika dealer FS dan Dealer Penjualan sama maka tanggal jual harus ada
            '        If Not IsNothing(objDealerSold) And Not IsNothing(objDealerFS) Then
            '            If objDealerSold.ID = objDealerFS.ID Then
            '                MessageBox.Show("Dealer FS sama dengan Dealer Penjualan, tanggal penjualan tidak boleh kosong !")
            '                bCheck = False
            '                Return
            '            Else
            '                objFreeService.SoldDate = New Date(1900, 1, 1)
            '            End If
            '        End If
            '    End If

            '    If Not IsNothing(objDealerSold) And Not IsNothing(objDealerFS) Then
            '        If objDealerSold.ID = objDealerFS.ID Then
            '            Dim critIsPDI As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '            critIsPDI.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Selesai, String)))
            '            critIsPDI.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.ID", MatchType.Exact, objFreeService.ChassisMaster.ID))
            '            Dim ArrIsPDI As ArrayList = New PDIFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(critIsPDI)
            '            If ArrIsPDI.Count = 0 Then 'sudah PDI jadi boleh insert
            '                MessageBox.Show("No. Rangka belum PDI ")
            '                bCheck = False
            '                Return
            '            End If
            '        End If
            '    End If

            'End If


            'checking tgl free service sama tanggal jual
            'If bCheck Then
            '    If Not txtTglJual.Text.Trim = "" Then
            '        If ToDate(txtTglJual.Text.Trim) > ToDate(txtTglServis.Text.Trim) Then
            '            MessageBox.Show("Tanggal Penjualan melebihi Tanggal Service")
            '            bCheck = False
            '        End If
            '    End If
            'End If

            'If bCheck Then
            '    If Not txtTglServis.Text.Trim = "" Then
            '        If Not ToDate(txtTglServis.Text.Trim) <= Now Then
            '            MessageBox.Show("Tanggal Service melebihi hari ini")
            '            bCheck = False
            '        End If
            '    End If
            'End If

            'If bCheck Then
            '    'Dim ServiceStart As DateTime = New date ToDate(txtTglServis.Text.Trim).Month
            '    'Dim ServiceEnd As DateTime = ToDate(txtTglServis.Text.Trim).Year
            '    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.GreaterOrEqual, ToDate(txtTglServis.Text.Trim).AddDays(-30)))
            '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.Lesser, ToDate(txtTglServis.Text.Trim)))
            '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
            '    Dim srt As New SortCollection()
            '    srt.Add(New Sort(GetType(FreeService), "ServiceDate", Sort.SortDirection.DESC))
            '    Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS, srt)
            '    If arlFS.Count > 0 Then
            '        'Cek apakah chassis termasuk dalam pengecualian 2x FS dalam  30 hari (PC = 30, LVC = 14) dalam hal ini data retrieve sudah difilter data 30 hari ke belakang dari tanggal pengajuan FS
            '        Dim oFSExisting As FreeService = CType(arlFS(0), FreeService)
            '        Dim cat = oFSExisting.ChassisMaster.VechileColor.VechileType.Category.ID

            '        If cat = 1 Then  'jika kategorinya PC, maka tidak valid
            '            MessageBox.Show("Chassis " + txtChassisMaster.Text + " Kategori PC. Tanggal Service kurang dari 30 Hari dari tanggal service terakhir.")
            '            bCheck = False
            '        ElseIf cat = 2 Then     'jika kategori LVC, maka cek apakah pengajuan lebih dari 14 hari dari pengajuan sebelumnya
            '            Dim d As DateTime = ToDate(txtTglServis.Text.Trim)
            '            If oFSExisting.ServiceDate.AddDays(14) > d Then
            '                MessageBox.Show("Chassis " + txtChassisMaster.Text + " Kategori LCV. Tanggal Service kurang dari 14 Hari dari tanggal service terakhir.")
            '                bCheck = False
            '            Else
            '                bCheck = True
            '            End If
            '        End If
            '    End If
            'End If

            'If bCheck Then
            '    'Dim ServiceStart As DateTime = New date ToDate(txtTglServis.Text.Trim).Month
            '    'Dim ServiceEnd As DateTime = ToDate(txtTglServis.Text.Trim).Year
            '    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.GreaterOrEqual, New DateTime(ToDate(txtTglServis.Text.Trim).Year, ToDate(txtTglServis.Text.Trim).Month, 1)))
            '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.Lesser, New DateTime(ToDate(txtTglServis.Text.Trim).Year, ToDate(txtTglServis.Text.Trim).Month + 1, 1)))
            '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
            '    Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
            '    If arlFS.Count > 0 Then
            '        MessageBox.Show("Chassis " + txtChassisMaster.Text + "Anda sudah mengajukan service 1x pada bulan ini!!")
            '        bCheck = False
            '    End If
            'End If

            If bCheck Then
                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'Dim strChassisNumber = txtChassisMaster.Text.Substring(1, Len(Trim(txtChassisMaster.Text)) - 1)
                '    Dim strChassisNumber = txtChassisMaster.Text.Substring(Len(fsKindCode), Len(Trim(txtChassisMaster.Text)) - Len(fsKindCode))
                Dim strChassisNumber = txtChassisMaster.Text.Trim()
                Dim strEngineNumber = txtEngineNumber.Text.Trim()
                criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, strChassisNumber))
                Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias2)
                If ChassisColl.Count > 0 Then
                    objFreeService.ChassisMaster = CType(ChassisColl(0), ChassisMaster)

                    If Not IsNothing(objFreeService.FleetRequest) Then
                        MessageBox.Show("kendaraan tidak berhak mendaftarkan Free Service")
                        Return
                    End If
                    'If Not ValidateFSKindOnVehicleType(objFreeService) AndAlso Not bolIsFleetExists Then
                    '    MessageBox.Show("kendaraan tidak berhak mendaftarkan Free Service")
                    '    Return
                    'End If

                    ObjChassisMasterCheck = CType(ChassisColl(0), ChassisMaster)
                    ObjSoldDealer = ObjChassisMasterCheck.Dealer

                    If (txtDealerBranchCode.Text.Trim() <> String.Empty) Then
                        Dim criterias4 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias4.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, txtDealerBranchCode.Text))
                        Dim DealerBranchColl As ArrayList = New DealerBranchFacade(User).Retrieve(criterias4)

                        If DealerBranchColl.Count > 0 Then
                            ObjDealerBranch = CType(DealerBranchColl(0), DealerBranch)
                            objFreeService.DealerBranch = ObjDealerBranch
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

                        'If Not IsExistCodeForInsert(objFreeService.ChassisMaster.ID, objFreeService.FSKind.ID) Or MSPExValidStatus Then
                        objFreeService.Dealer = ObjServiceDealer
                        objFreeService.MileAge = CType(txtKM.Text, Integer)
                        objFreeService.Status = EnumFSStatus.FSStatus.Baru
                        objFreeService.VisitType = ddlVisitType.SelectedValue
                        objFreeService.WorkOrderNumber = txtWONumber.Text.Trim()
                        'If Len(fsKindCode) = 1 Then

                        'If ddlKind.SelectedValue.ToString() = "1" OrElse ddlKind.SelectedValue.ToString() = "2" Then
                        nResult = objFreeServiceFacade.Insert(objFreeService)
                        'Else
                        '    If objFreeServiceFacade.IsAllowFreeService(objFreeService) OrElse bolIsFleetExists Then
                        '        nResult = objFreeServiceFacade.Insert(objFreeService)
                        '    Else
                        '        'remark by anh, belum masa naik 2011-12-08
                        '        If IsChassisAllowed(objFreeService) OrElse bolIsFleetExists Then
                        '            nResult = objFreeServiceFacade.Insert(objFreeService)
                        '        Else
                        '            MessageBox.Show("Simpan gagal, Chassis " & objFreeService.ChassisMaster.ChassisNumber & " tidak mendapat kupon Free Service")
                        '            nResult = -1
                        '        End If
                        '    End If
                        'End If
                        'Else
                        '    If objFreeServiceFacade.IsAllowFreeService(objFreeService) OrElse bolIsFleetExists Then
                        '        nResult = objFreeServiceFacade.Insert(objFreeService)
                        '    Else
                        '        'remark by anh, belum masa naik 2011-12-08
                        '        If IsChassisAllowed(objFreeService) OrElse bolIsFleetExists Then
                        '            nResult = objFreeServiceFacade.Insert(objFreeService)
                        '        Else
                        '            MessageBox.Show("Simpan gagal, Chassis " & objFreeService.ChassisMaster.ChassisNumber & " tidak mendapat kupon Free Service")
                        '            nResult = -1
                        '        End If
                        '    End If
                        'End If

                        'If nResult <> -1 Then
                        '    Dim oMSPClaim As MSPClaim = New MSPClaim
                        '    Dim oMSPRegistration As MSPRegistration = New MSPRegistration
                        '    Dim oMSPRegistrationHistory As MSPRegistrationHistory = New MSPRegistrationHistory
                        '    Dim oPMHeader As PMHeader = New PMHeader

                        '    Dim critMSPReg As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        '    critMSPReg.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, objFreeService.ChassisMaster.ChassisNumber))
                        '    critMSPReg.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistration), "Dealer.ID", MatchType.Exact, objFreeService.Dealer.ID))

                        '    Dim arlMSPReg As ArrayList = New MSPRegistrationFacade(User).Retrieve(critMSPReg)
                        '    If arlMSPReg.Count > 0 Then
                        '        oMSPRegistration = CType(arlMSPReg(0), MSPRegistration)

                        '        Dim critMSPRegHistory As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPRegistrationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        '        critMSPRegHistory.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPRegistrationHistory), "MSPRegistration.ID", MatchType.Exact, objFreeService.ChassisMaster.ChassisNumber))

                        '        Dim arlMSPRegHistory As ArrayList = New MSPRegistrationFacade(User).Retrieve(critMSPRegHistory)
                        '        If arlMSPRegHistory.Count > 0 Then
                        '            oMSPRegistrationHistory = CType(arlMSPRegHistory(0), MSPRegistrationHistory)
                        '        End If
                        '    End If

                        '    Dim critPMHeader As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        '    critPMHeader.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, objFreeService.ChassisMaster.ChassisNumber))
                        '    critPMHeader.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "Dealer.ID", MatchType.Exact, objFreeService.Dealer.ID))

                        '    oMSPClaim.MSPRegistrationHistory = oMSPRegistrationHistory

                        'End If

                        If nResult = -1 Then
                            MessageBox.Show("Simpan Gagal")
                        Else
                            MessageBox.Show("Input dan simpan sukses.\nSilakan melakukan rilis pada hari yang sama dengan tanggal penginputan data Free Service.")
                            Session.Add("vsFreeService", objFreeService)
                            ClearDataAfterSaving()
                            Dim strScript As String
                            strScript = "<script>document.all.txtChassisMaster.focus();</script>"
                            Page.RegisterStartupScript("", strScript)
                        End If
                        'Else
                        '    MessageBox.Show("No. Rangka dengan jenis servis tersebut sudah ada ")
                        'End If
                    Else
                        MessageBox.Show("Kode Dealer tidak terdaftar ")
                    End If
                Else
                    MessageBox.Show("No. Rangka tidak terdaftar !")
                End If
            Else
                MessageBox.Show("Kode FS Kind dan No Rangka Tidak boleh kosong")
            End If
        End If

    End Sub

    Private Sub commitAttachment(ByVal attachment As HttpPostedFile, ByRef oFS As FreeService)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim targetDIrectory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        Dim FSEvidenceDir As String = KTB.DNet.Lib.WebConfig.GetValue("FSEvidenceDirectory")
        Dim ID As String = Guid.NewGuid().ToString()
        Dim fileName As String = oFS.ChassisMaster.ChassisNumber + "-" + ID.Substring(ID.Length - 10) + Path.GetExtension(attachment.FileName)

        FSEvidenceDir = FSEvidenceDir + DateTime.Now.Year.ToString() + "\" + DateTime.Now.ToString("MM") + "\" + fileName

        Dim TargetFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(attachment) Then
                    TargetFInfo = New FileInfo(targetDIrectory + FSEvidenceDir)

                    If Not TargetFInfo.Directory.Exists Then
                        Directory.CreateDirectory(TargetFInfo.DirectoryName)
                    End If

                    attachment.SaveAs(targetDIrectory + FSEvidenceDir)

                    oFS.FileName = attachment.FileName
                    oFS.FilePath = FSEvidenceDir
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            imp.StopImpersonate()
            imp = Nothing
            Throw ex
        End Try

    End Sub

    Private Sub deleteSavedAttachment(ByVal oFS As FreeService)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim targetDIrectory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        Dim TargetFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                TargetFInfo = New FileInfo(targetDIrectory + oFS.FilePath)
                If TargetFInfo.Exists Then
                    TargetFInfo.Delete()
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            imp.StopImpersonate()
            imp = Nothing
            Throw ex
        End Try
    End Sub

    Private Sub deleteSavedAttachment(ByVal filePath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim targetDIrectory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        Dim TargetFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                TargetFInfo = New FileInfo(targetDIrectory + filePath)
                If TargetFInfo.Exists Then
                    TargetFInfo.Delete()
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            imp.StopImpersonate()
            imp = Nothing
            Throw ex
        End Try
    End Sub


    'added by anh 20111206 'sementara sebelum dibikin parameterise
    Public Function IsChassisAllowed(ByVal objFreeService As FreeService) As Boolean
        Dim objType() As String = {"MMBJNKB40AD026535", "MMBJNKB40AD033824", "MMBJNKB40AD042965", "MMBJNKB40AD043166", "MMBJNKB40AD020735", "MMBJNKB40AD030855", "MMBJNKB40AD030954", "MMBJNKB40AD038483", "MMBJNKB40AD038487", "MMBJNKB40AD038974"}
        Dim isAllowInsert As Boolean = False
        For i As Integer = 0 To objType.Length - 1
            If objFreeService.ChassisMaster.ChassisNumber = objType(i) Then
                isAllowInsert = True
                Exit For
            End If
        Next
        Return isAllowInsert
    End Function
    'end 

    Public Function IsAllowPKT(ByVal objFreeService As FreeService) As Boolean
        Dim vReturn As Boolean = False
        Dim ObjIsByPass As Boolean = False
        Dim ObjFsChassisCampaign As FSChassisCampaign = New FSChassisCampaign

        Dim criteriasFsChassisCampaign As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasFsChassisCampaign.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "ChassisMaster.ChassisNumber", MatchType.Exact, objFreeService.ChassisMaster.ChassisNumber))
        criteriasFsChassisCampaign.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "FSKind.ID", MatchType.Exact, objFreeService.FSKind.ID))

        Dim arrFsChassisCampaign As ArrayList = New FSChassisCampaignFacade(User).Retrieve(criteriasFsChassisCampaign)
        If Not IsNothing(arrFsChassisCampaign) AndAlso arrFsChassisCampaign.Count > 0 Then
            ObjIsByPass = True
            vReturn = True
        End If

        'If ObjIsByPass = False Then
        '    Dim critFSKindOnVT As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    critFSKindOnVT.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSKind.ID", MatchType.Exact, objFreeService.FSKind.ID))
        '    critFSKindOnVT.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "VechileType.ID", MatchType.Exact, objFreeService.ChassisMaster.VechileColor.VechileType.ID))
        '    Dim arlFSKindOnVT As ArrayList = New FSKindOnVechileTypeFacade(User).Retrieve(critFSKindOnVT)
        '    If Not IsNothing(arlFSKindOnVT) AndAlso arlFSKindOnVT.Count > 0 Then
        '        ObjIsByPass = True
        '        vReturn = True
        '    End If
        'End If

        If ObjIsByPass = False Then
            Dim arlFSCampaign As ArrayList = New ArrayList
            arlFSCampaign = GetFSCampaign()
            If arlFSCampaign.Count > 0 Then
                For Each objFSCampaign As FSCampaign In arlFSCampaign
                    'Dealer checking
                    Dim bDealer As Boolean = True
                    If objFSCampaign.DealerChecked = True Then
                        bDealer = False
                        For Each objFSCampaignDealer As FSCampaignDealer In objFSCampaign.FSCampaignDealers
                            If objFSCampaignDealer.DealerCode = objFreeService.Dealer.DealerCode Then
                                bDealer = True
                            End If
                        Next
                    End If

                    Dim bPKT As Boolean = True
                    If objFSCampaign.PKTDateChecked = True Then
                        bPKT = False
                        If objFSCampaign.PKTDateFrom <= objFreeService.SoldDate _
                           And objFSCampaign.PKTDateTo >= objFreeService.SoldDate Then
                            bPKT = True
                        End If
                    End If
                    'Combine value above
                    'If bDealer And bFSKind And bVehicle And bFaktur Then
                    If bDealer And bPKT Then
                        vReturn = True
                        Exit For
                    End If
                Next
            End If
        End If

        If vReturn = False Then
            MessageBox.Show("Chassis " & objFreeService.ChassisMaster.ChassisNumber & " dan FS Kind " & objFreeService.FSKind.KindCode & " tidak mendapat Free Service")
        End If

        Return vReturn
    End Function

    Public Function IsAllowFSCampaign(ByVal objFreeService As FreeService) As Boolean
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
                        If objFSCampaignDealer.DealerCode = objFreeService.Dealer.DealerCode Then
                            bDealer = True
                        End If
                    Next
                End If

                'FSKind checking
                Dim bFSKind As Boolean = True
                If objFSCampaign.FSTypeChecked = True Then
                    bFSKind = False
                    For Each objFSCampaignKind As FSCampaignKind In objFSCampaign.FSCampaignKinds
                        If objFSCampaignKind.FSKind.KindCode = objFreeService.FSKind.KindCode Then
                            bFSKind = True
                        End If
                    Next
                End If

                'VehicleType checking
                Dim bVehicle As Boolean = True
                If objFSCampaign.VehicleTypeChecked = True Then
                    bVehicle = False
                    For Each objFSCampaignVehicle As FSCampaignVehicle In objFSCampaign.FSCampaignVehicles
                        If objFSCampaignVehicle.VechileType.ID = objFreeService.ChassisMaster.VechileColor.VechileType.ID Then
                            bVehicle = True
                        End If
                    Next
                End If

                'Faktur Validation checking
                Dim bFaktur As Boolean = True
                If objFSCampaign.FakturDateChecked = True Then
                    bFaktur = False
                    If objFSCampaign.DateFrom <= objFreeService.ChassisMaster.EndCustomer.ValidateTime _
                       And objFSCampaign.DateTo >= objFreeService.ChassisMaster.EndCustomer.ValidateTime Then
                        bFaktur = True
                    End If
                End If

                'Dim bPKT As Boolean = True
                'If objFSCampaign.PKTDateChecked = True Then
                '    bPKT = False
                '    If objFSCampaign.PKTDateFrom <= objFreeService.SoldDate _
                '       And objFSCampaign.PKTDateTo >= objFreeService.SoldDate Then
                '        bPKT = True
                '    End If
                'End If
                'Combine value above
                If bDealer And bFSKind And bVehicle And bFaktur Then
                    vReturn = True
                    Exit For
                End If
            Next
        End If
        If vReturn = False Then
            MessageBox.Show("Chassis " & objFreeService.ChassisMaster.ChassisNumber & " dan FS Kind " & objFreeService.FSKind.KindCode & " tidak mendapat Free Service")
        End If

        Return vReturn
    End Function

    Public Function GetFSCampaign() As ArrayList
        Dim arlFSCampaign As ArrayList = New ArrayList
        Dim objFSCampaignFacade As FSCampaignFacade = New FSCampaignFacade(User)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(FSCampaign), "Status", MatchType.Exact, 0))
        'criterias.opAnd(New Criteria(GetType(FSCampaign), "DateFrom", MatchType.LesserOrEqual, Format(Date.Now, "yyyy-MM-dd 00:00:00")))
        'criterias.opAnd(New Criteria(GetType(FSCampaign), "DateTo", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy-MM-dd 00:00:00")))

        criterias.opAnd(New Criteria(GetType(FSCampaign), "PKTDateFrom", MatchType.LesserOrEqual, Format(ToDate(txtTglJual.Text.Trim), "yyyy-MM-dd 00:00:00")))
        criterias.opAnd(New Criteria(GetType(FSCampaign), "PKTDateTo", MatchType.GreaterOrEqual, Format(ToDate(txtTglJual.Text.Trim), "yyyy-MM-dd 00:00:00")))
        arlFSCampaign = objFSCampaignFacade.Retrieve(criterias)

        Return arlFSCampaign
    End Function

    Public Function IsFE7orFE8(ByVal IsAllowInsert As Boolean, ByRef objFreeService As FreeService) As Boolean
        'Start  :CR;by:dna;for:rina;on:20100615;remark:allow for below condition
        If IsAllowInsert = False Then
            Dim dtFacturValidation As Date = DateSerial(objFreeService.ChassisMaster.EndCustomer.ValidateTime.Year, objFreeService.ChassisMaster.EndCustomer.ValidateTime.Month, objFreeService.ChassisMaster.EndCustomer.ValidateTime.Day)
            If (objFreeService.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE7" _
            OrElse objFreeService.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE8") _
            AndAlso (objFreeService.FSKind.KindCode = "3" OrElse objFreeService.FSKind.KindCode = "4" OrElse objFreeService.FSKind.KindCode = "5" OrElse objFreeService.FSKind.KindCode = "6" OrElse objFreeService.FSKind.KindCode = "7") _
            AndAlso (dtFacturValidation > DateSerial(2010, 4, 1).AddDays(-1) And dtFacturValidation < DateSerial(2010, 10, 1)) Then
                IsAllowInsert = True
            End If
        End If
        Return IsAllowInsert
    End Function

    Public Function CheckToAllowStradaTriton(ByVal IsAllowInsert As Boolean, ByRef objFreeService As FreeService) As Boolean
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
            FSType = objFreeService.ChassisMaster.VechileColor.VechileType.VechileTypeCode.Trim.ToUpper
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
            nFSKind = CType(objFreeService.FSKind.KindCode, Integer)
        Catch ex As Exception
            nFSKind = 0
        End Try
        Try
            dtValidate = objFreeService.ChassisMaster.EndCustomer.ValidateTime
        Catch ex As Exception
            dtValidate = Date.MinValue
        End Try
        Try
            oVT = objFreeService.ChassisMaster.VechileColor.VechileType
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

    Public Function ChassisException(ByVal IsAllowInsert As Boolean, ByRef objFreeService As FreeService) As Boolean
        'Start  :CR;by:dna;for:Rina;On:20100616;Remark:allow for specified CM
        If IsAllowInsert = True _
        AndAlso (objFreeService.FSKind.KindCode = "3" _
        OrElse objFreeService.FSKind.KindCode = "4" _
        OrElse objFreeService.FSKind.KindCode = "5" _
        OrElse objFreeService.FSKind.KindCode = "6" _
        OrElse objFreeService.FSKind.KindCode = "7") Then
            Dim sForbiddenCMs() As String = {"MHMFE71P1AK018514", "MHMFE73P2AK014642", "MHMFE73P2AK014643", "MHMFE73P2AK014715", "MHMFE73P2AK014760"}
            For i As Integer = 0 To sForbiddenCMs.Length - 1
                If objFreeService.ChassisMaster.ChassisNumber.Trim.ToUpper = sForbiddenCMs(i).Trim.ToUpper Then
                    IsAllowInsert = False
                    MessageBox.Show("Simpan gagal, Chassis " & objFreeService.ChassisMaster.ChassisNumber & " tidak mendapat kupon Free Service")
                    Exit For
                End If
            Next
        End If
        Return IsAllowInsert
        'End    :CR;by:dna;for:Rina;On:20100616;Remark:allow for specified CM
    End Function

    Public Function IsFE75orFESHD(ByVal IsAllowInsert As Boolean, ByRef objFreeService As FreeService) As Boolean
        'Start  :CR;by:anh;for:Rina;On:20100616;Remark:allow for specified material description

        If IsAllowInsert = False Then
            If (objFreeService.FSKind.KindCode = "3" Or objFreeService.FSKind.KindCode = "4" Or objFreeService.FSKind.KindCode = "5") Then
                If (objFreeService.ChassisMaster.VechileColor.MaterialDescription.Substring(0, 4) = "FE75" _
                Or objFreeService.ChassisMaster.VechileColor.MaterialDescription.Substring(0, 4) = "FESH") Then
                    If objFreeService.ChassisMaster.EndCustomer Is Nothing Then
                        MessageBox.Show("Simpan gagal, Chassis " & objFreeService.ChassisMaster.ChassisNumber & " No faktur tidak ada")
                    Else
                        Dim dtFacturValidation As Date = DateSerial(objFreeService.ChassisMaster.EndCustomer.ValidateTime.Year, objFreeService.ChassisMaster.EndCustomer.ValidateTime.Month, objFreeService.ChassisMaster.EndCustomer.ValidateTime.Day)
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

    Private Sub UpdateObjFreeService(ByVal objFreeService As FreeService)
        Dim objFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
        Dim nResult As Integer = -1
        Dim bCheck As Boolean = True
        Dim isEditEvidence As Boolean = CBool(Session("ISEDITEVIDENCE"))
        If Not txtChassisMaster.Text = String.Empty Then
            'added by anh 20130703
            Dim cFSKind As Char() = txtChassisMaster.Text.ToCharArray()
            Dim iCnt As Integer = 0
            For Each _c As Char In cFSKind
                If IsNumeric(_c) Then
                    iCnt = iCnt + 1
                Else
                    Exit For
                End If
            Next
            Dim fsKindCode As String = String.Empty
            fsKindCode = ddlKind.SelectedValue.ToString() 'txtChassisMaster.Text.Substring(0, iCnt)
            'end added by anh 20130703

            'add validasi tgl PKT
            'If bCheck Then
            '    If txtTglServis.Text.Trim = "" Then
            '        MessageBox.Show("Tanggal Service tidak boleh kosong")
            '        bCheck = False
            '        Return
            '    End If
            'End If

            'If bCheck Then
            '    If txtTglJual.Text.Trim = "" Then
            '        MessageBox.Show("Tanggal Jual/PKT tidak boleh kosong")
            '        bCheck = False
            '        Return
            '    End If
            'End If

            'validasi km
            If bCheck Then
                If txtKM.Text.Trim() = "" OrElse CType(txtKM.Text, Integer) <= 0 Then
                    MessageBox.Show("Jarak tempuh tidak boleh kosong dan harus > 0 km !")
                    bCheck = False
                    Return
                    'Else
                    '    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    '    'critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "KindCode", MatchType.Exact, txtChassisMaster.Text.Trim.Substring(0, 1).ToString()))
                    '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "KindCode", MatchType.Exact, fsKindCode))
                    '    Dim FSColl As ArrayList = New FSKindFacade(User).Retrieve(critFS)
                    '    If FSColl.Count > 0 Then
                    '        objFreeService.FSKind = FSColl(0)
                    '        If CType(txtKM.Text.Trim, Integer) > objFreeService.FSKind.KM Then
                    '            MessageBox.Show("Jarak tempuh melampaui batas jenis free service ")
                    '            bCheck = False
                    '            Return
                    '        Else
                    '            'disini baru lolos validasi
                    '            ''Penyamaan dengan upload
                    '            'Dim MinValue As Integer = 0
                    '            'Dim crtFS As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    '            'Dim srtFS As New SortCollection
                    '            'Dim arlFS As New ArrayList
                    '            'crtFS.opAnd(New Criteria(GetType(FSKind), "KM", MatchType.Lesser, objFreeService.FSKind.KM))
                    '            'srtFS.Add(New Sort(GetType(FSKind), "KM", Sort.SortDirection.DESC))
                    '            'arlFS = New FSKindFacade(User).Retrieve(crtFS, srtFS)
                    '            'If arlFS.Count > 0 Then
                    '            '    MinValue = CType(arlFS(0), FSKind).KM + 1
                    '            'End If
                    '            'If CType(txtKM.Text, Integer) < MinValue Then
                    '            '    MessageBox.Show("Jarak tempuh tidak sesuai dengan batas jenis free service ")
                    '            '    bCheck = False
                    '            ' End If
                    '            'end of Penyamaan dengan upload
                    'End If
                    '    Else
                    '        MessageBox.Show("Kode Jenis Free Servis tidak terdaftar !")
                    '    bCheck = False
                    '        Return
                    ' End If
                    'end of Penyamaan dengan upload
                    'End If
                    'Else
                    '    MessageBox.Show("Kode Jenis Free Servis tidak terdaftar !")
                    '    bCheck = False
                    '   Return
                    'End If
                End If
            End If

            'validasi tgl servis
            'If bCheck Then
            '    If txtTglServis.Text.Trim <> "" Then
            '        If Len(txtTglServis.Text.Trim) = 8 Then
            '            If IsValidDate(txtTglServis.Text.Trim) Then
            '
            '                If ToDate(txtTglServis.Text.Trim) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
            '                    ToDate(txtTglServis.Text.Trim) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
            '                    MessageBox.Show("Format tanggal service salah")
            '                    bCheck = False
            '                    Return
            '                Else
            '                    objFreeService.ServiceDate = ToDate(txtTglServis.Text.Trim)
            '                End If
            '            Else
            '                MessageBox.Show("Format tgl. service salah")
            '                bCheck = False
            '                Return
            '            End If
            '        Else
            '            bCheck = False
            '            MessageBox.Show("Format tgl. service salah")
            '        End If
            '    Else
            '        bCheck = False
            '        MessageBox.Show("Tgl. service kosong")
            '        Return
            '    End If
            'End If

            'If bCheck Then
            '    If txtTglJual.Text.Trim <> "" Then
            '        If Len(txtTglJual.Text.Trim) = 8 Then
            '            If IsValidDate(txtTglJual.Text.Trim) Then
            '
            '                If ToDate(txtTglJual.Text.Trim) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
            '                    ToDate(txtTglJual.Text.Trim) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
            '                    MessageBox.Show("Format tanggal penjualan/PKT salah")
            '                    bCheck = False
            '                    Return
            '                Else
            '                    objFreeService.SoldDate = ToDate(txtTglJual.Text.Trim)
            '                End If
            '            Else
            '                MessageBox.Show("Format tgl. penjualan/PKT salah")
            '                bCheck = False
            '                Return
            '            End If
            '        Else
            '            bCheck = False
            '            MessageBox.Show("Format tgl. penjualan/PKT salah")
            '        End If
            '    Else
            '        bCheck = False
            '        MessageBox.Show("Tgl. penjualan/PKT kosong")
            '        Return
            '    End If
            'End If

            'validasi KM dan Valid Date untuk extended
            'If bCheck Then
            '    Dim ValidKM As Integer = 0
            '    Dim ValidDate As Date
            '    Dim critFSExt As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    critFSExt.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKindCode))
            '    critFSExt.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSType", MatchType.InSet, "(7,8,9)"))

            '    Dim arlFSExt As ArrayList = New FSKindOnVechileTypeFacade(User).Retrieve(critFSExt)
            '    If arlFSExt.Count > 0 Then
            '        Dim critFSSts As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        critFSSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
            '        'critFSSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "Status", MatchType.No, "4"))

            '        Dim sortColl As SortCollection = New SortCollection
            '        sortColl.Add(New Sort(GetType(MSPExRegistration), "ID", Sort.SortDirection.ASC))
            '        Dim arlFSSts As ArrayList = New MSPExRegistrationFacade(User).Retrieve(critFSSts, sortColl)
            '        If arlFSSts.Count > 0 Then
            '            Dim lastMSPEx As MSPExRegistration = CType(arlFSSts(arlFSSts.Count - 1), MSPExRegistration)
            '            ValidKM = lastMSPEx.ValidKMTo
            '            ValidDate = lastMSPEx.ValidDateTo
            '            If CType(txtKM.Text, Integer) > ValidKM Then
            '                MessageBox.Show("Jarak tempuh dan Tanggal Service melebihi batas maksimum paket " + ValidKM.ToString)
            '                bCheck = False
            '                Return
            '            End If
            'If ToDate(txtTglServis.Text.Trim) > ValidDate Then
            '    MessageBox.Show("Tanggal Service melebihi batas maksimum paket " + ValidDate.ToShortDateString)
            '    bCheck = False
            '    Return
            'End If

            'If ToDate(txtTglServis.Text.Trim) > ValidDate Then
            '    'validate msp ex
            '    Dim mspExCheckStat As Boolean = False
            '    If lastMSPEx.Status = 4 Then
            '        If ValidDate >= DateTime.Now Then
            '            Dim svcCount As Integer = New MSPExRegistrationFacade(User).RetrieveCountFS(lastMSPEx.MSPExMaster.MSPExType.ID, lastMSPEx.ChassisMaster.ID, lastMSPEx.CreatedTime, lastMSPEx.ValidDateTo)
            '            'Dim svcCount As Integer = New MSPExRegistrationFacade(User).RetrieveCountFS(txtChassisMaster.Text, lastMSPEx.ID)
            '            Dim maxPM As Integer = GetMaxPM(lastMSPEx.MSPExMaster.MSPExType).Count
            '            If svcCount < maxPM Then
            '                mspExCheckStat = True
            '            Else
            '                mspExCheckStat = False
            '            End If
            '        End If
            '    End If
            '    If Not mspExCheckStat Then
            '        MessageBox.Show("Tanggal Service melebihi batas maksimum paket " + ValidDate.ToShortDateString)
            '        bCheck = False
            '        Return
            '    End If
            'End If

            'If ValidDate < Now.ToShortDateString Then
            '    MessageBox.Show("Tanggal valid Service belum expired, berlaku sampai dengan tanggal " + ValidDate)
            '    bCheck = False
            '    Return
            'End If
        End If

        'End If
        'End If

        'validasi tidak boleh ambil kode free service yang telah digunakan
        'Dim MSPExValidStatus As Boolean = False
        'If CType(ViewState("vsProcess"), String) <> "Insert" Then
        '    MSPExValidStatus = True
        'End If
        'If bCheck Then
        '    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ID", MatchType.No, objFreeService.ID))
        '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "FSKind.KindCode", MatchType.Exact, fsKindCode))
        '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
        '
        'Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
        'If arlFS.Count > 0 Then
        '    Dim critFSSts As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    critFSSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
        '    critFSSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "Status", MatchType.Exact, 4))

        '    Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
        '    If arlFS.Count > 0 Then
        '        Dim critFSSts As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        critFSSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
        '        critFSSts.opAnd(New Criteria(GetType(KTB.DNet.Domain.MSPExRegistration), "Status", MatchType.Exact, 4))
        '        Dim arlFSSts As ArrayList = New MSPExRegistrationFacade(User).Retrieve(critFSSts)
        '        If arlFSSts.Count > 0 Then
        '            Dim lastMSPEx As MSPExRegistration = CType(arlFSSts(arlFSSts.Count - 1), MSPExRegistration)
        '            If lastMSPEx.ValidDateTo >= DateTime.Now Then
        '                Dim FSHistoryAfterLastMSPCounter As Integer = 0
        '                For Each f As FreeService In arlFS
        '                    If f.CreatedTime >= lastMSPEx.CreatedTime Then
        '                        FSHistoryAfterLastMSPCounter += 1
        '                    End If
        '                Next
        '                If FSHistoryAfterLastMSPCounter < GetMaxPM(lastMSPEx.MSPExMaster.MSPExType).Count Then
        '                    MSPExValidStatus = True
        '                End If
        '            End If
        '        End If

        '        If Not MSPExValidStatus Then
        '            MessageBox.Show("Kode Jenis Free Servis telah digunakan, harap pilih kode jenis free servis lain")
        '            bCheck = False
        '            Return
        '        End If
        '    End If
        'End If

        'validasi tdk boleh ambil kode fs yang angka depannya telah digunakan (ex. telah save 7A, maka tdk boleh ambil 7D)
        'If bCheck Then
        '    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ID", MatchType.No, objFreeService.ID))
        '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))

        '    Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
        '    For Each oFS As FreeService In arlFS
        '        If Regex.Replace(oFS.FSKind.KindCode, "[^0-9]", "") = Regex.Replace(fsKindCode, "[^0-9]", "") Then
        '            If Not MSPExValidStatus Then
        '                MessageBox.Show("Anda tidak bisa memilih kode free service " + fsKindCode + " karena telah menggunakan kode free service " + oFS.FSKind.KindCode)
        '                bCheck = False
        '                Return
        '            End If
        '        End If
        '    Next
        'End If

        'validasi durasi
        'If bCheck Then
        '    Dim oDatePembanding As Date
        '    Dim oChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(txtChassisMaster.Text)

        '    If Not IsNothing(oChassisMaster) Then
        '        Dim critCMPKT As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        critCMPKT.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterPKT), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))

        '        Dim arlCMPKT As New ArrayList
        '        arlCMPKT = New ChassisMasterPKTFacade(User).Retrieve(critCMPKT)
        '        Dim isSoldDealer As Boolean
        '        isSoldDealer = (oChassisMaster.Dealer.ID = CType(Session("Dealer"), Dealer).ID)

        '        If 1 = 1 Then
        '            Dim YearDurationVal As Integer = 2019
        '            Dim MonthDurationVal As Integer = 9
        '            Dim critAppConf As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            critAppConf.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "Name", MatchType.InSet, "(" + "'YearFSDurationValidation','MonthFSDurationValidation'" + ")"))
        '            Dim srtAppConf As New SortCollection
        '            srtAppConf.Add(New Sort(GetType(AppConfig), "ID", Sort.SortDirection.ASC))

        '            Dim arlAppConf As ArrayList = New AppConfigFacade(User).Retrieve(critAppConf, srtAppConf)
        '            If arlAppConf.Count > 0 Then
        '                MonthDurationVal = arlAppConf(0).Value
        '                YearDurationVal = arlAppConf(1).Value
        '            End If
        '            Dim oCMPKT As ChassisMasterPKT
        '            If isSoldDealer Then
        '                If arlCMPKT.Count = 0 Then
        '                    MessageBox.Show("Nomor Chassis belum memiliki tanggal PKT")
        '                    bCheck = False
        '                    Return
        '                End If

        '            End If

        'If arlCMPKT.Count = 0 Then
        '    MessageBox.Show("Nomor Chassis belum memiliki tanggal PKT")
        '    bCheck = False
        '    Return
        'remarks by irfan 18022022
        'If Not IsNothing(oChassisMaster.EndCustomer) AndAlso oChassisMaster.EndCustomer.FakturDate.Year > 1900 Then
        '    oDatePembanding = oChassisMaster.EndCustomer.FakturDate
        'ElseIf Not IsNothing(oChassisMaster.EndCustomer) AndAlso oChassisMaster.EndCustomer.OpenFakturDate.Year > 1900 Then
        '    oDatePembanding = oChassisMaster.EndCustomer.OpenFakturDate
        'Else
        '    MessageBox.Show("Nomor Chassis belum Open Faktur")
        '    bCheck = False
        '    Return
        'End If
        'Else
        '    oCMPKT = CType(arlCMPKT(0), ChassisMasterPKT)
        '    If oCMPKT.PKTDate <> ToDate(txtTglJual.Text.Trim) Then
        '        MessageBox.Show("Tanggal PKT tidak sama dengan tanggal input")
        '        bCheck = False
        '        Return
        'If (oCMPKT.PKTDate.Year = YearDurationVal AndAlso oCMPKT.PKTDate.Month < MonthDurationVal) OrElse oCMPKT.PKTDate.Year < YearDurationVal Then
        '    oDatePembanding = oChassisMaster.EndCustomer.FakturDate
        '                Else
        '                    oDatePembanding = oCMPKT.PKTDate
        '                End If
        '            End If

        '            Dim critFSKindOnVT As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            critFSKindOnVT.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSKind.KindCode", MatchType.Exact, fsKindCode))
        '            critFSKindOnVT.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "VechileType.ID", MatchType.Exact, oChassisMaster.VechileColor.VechileType.ID))

        '            Dim arlFSKindOnVT As ArrayList = New FSKindOnVechileTypeFacade(User).Retrieve(critFSKindOnVT)

        '            If arlFSKindOnVT.Count > 0 Then
        '                Dim oFSKindOnVT As FSKindOnVechileType = CType(arlFSKindOnVT(0), FSKindOnVechileType)
        '                Dim ts As TimeSpan = DateTime.Now.Subtract(oDatePembanding)

        '                Dim DayDifference As Integer = Convert.ToInt32(ts.Days)
        '                If DayDifference > oFSKindOnVT.Duration Then
        '                    MessageBox.Show("Tanggal service melebihi tanggal yang seharusnya")
        '                    bCheck = False
        '                    Return
        '                End If
        '            End If
        '        End If
        '    Else
        '        MessageBox.Show("Nomor Chassis tidak ditemukan")
        '        bCheck = False
        '        Return
        '    End If
        'End If

        'validasi tgl penjualan
        'If txtTglJual.Text.Trim <> "" Then
        '    If IsValidDate(txtTglJual.Text.Trim) Then
        '        If IsValidDate(txtTglJual.Text.Trim) Then
        '            If ToDate(txtTglJual.Text.Trim) <= System.Data.SqlTypes.SqlDateTime.MinValue.Value Or _
        '               ToDate(txtTglJual.Text.Trim) >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
        '                MessageBox.Show("Format tanggal jual salah")
        '                bCheck = False
        '            Else
        '                objFreeService.SoldDate = ToDate(txtTglJual.Text.Trim)
        '            End If
        '        Else
        '            MessageBox.Show("Format tgl. penjualan salah ")
        '            bCheck = False
        '        End If
        '    Else
        '        MessageBox.Show("Format tgl. penjualan salah ")
        '        bCheck = False
        '    End If
        'Else
        '    'Jika dealer FS dan Dealer Penjualan sama maka tanggal jual harus ada
        '    Dim objDealerSold As Dealer = New Dealer
        '    Dim objDealerFS As Dealer = New Dealer

        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    'Dim strChassisNumber = txtChassisMaster.Text.Trim.Substring(1, Len(Trim(txtChassisMaster.Text)) - 1)
        '    'Dim strChassisNumber = txtChassisMaster.Text.Trim.Substring(Len(fsKindCode), Len(Trim(txtChassisMaster.Text)) - Len(fsKindCode))
        '    Dim strChassisNumber = txtChassisMaster.Text.Trim()
        '    Dim strEngineNumber = txtEngineNumber.Text.Trim()
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, strChassisNumber))
        '    Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
        '    If ChassisColl.Count > 0 Then
        '        objFreeService.ChassisMaster = ChassisColl(0)

        '        Dim critDealerSold As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        critDealerSold.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.Exact, objFreeService.ChassisMaster.Dealer.ID))
        '        Dim DealerCollSold As ArrayList = New DealerFacade(User).Retrieve(critDealerSold)
        '        If DealerCollSold.Count > 0 Then
        '            objDealerSold = DealerCollSold(0)
        '        End If

        '        Dim critDealerFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
        '        critDealerFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strDealerCode))
        '        Dim DealerCollFS As ArrayList = New DealerFacade(User).Retrieve(critDealerFS)
        '        If DealerCollFS.Count > 0 Then
        '            objDealerFS = DealerCollFS(0)
        '        End If

        '        If Not IsNothing(objDealerSold) And Not IsNothing(objDealerFS) Then
        '            If objDealerSold.ID = objDealerFS.ID Then
        '                MessageBox.Show("Dealer FS sama dengan Dealer Penjualan, tanggal penjualan tidak boleh kosong !")
        '                bCheck = False
        '            Else
        '                objFreeService.SoldDate = New Date(1900, 1, 1)
        '            End If
        '        End If
        '    End If
        'End If

        'If bCheck Then
        '    If Not txtTglJual.Text.Trim = "" Then
        '        If ToDate(txtTglJual.Text.Trim) > ToDate(txtTglServis.Text.Trim) Then
        '            MessageBox.Show("Tanggal Penjualan melebihi Tanggal Service")
        '            bCheck = False
        '        End If
        '    End If
        'End If

        'If bCheck Then
        '    If Not ToDate(txtTglServis.Text) <= Now Then
        '        MessageBox.Show("Tanggal Service melebihi hari ini")
        '        bCheck = False
        '    End If
        'End If

        'If bCheck Then
        '    'Dim ServiceStart As DateTime = New date ToDate(txtTglServis.Text.Trim).Month
        '    'Dim ServiceEnd As DateTime = ToDate(txtTglServis.Text.Trim).Year
        '    Dim critFS As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.GreaterOrEqual, ToDate(txtTglServis.Text.Trim).AddDays(-30)))
        '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ServiceDate", MatchType.Lesser, ToDate(txtTglServis.Text.Trim)))
        '    critFS.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
        '    Dim srt As New SortCollection()
        '    srt.Add(New Sort(GetType(FreeService), "ServiceDate", Sort.SortDirection.DESC))
        '    Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS, srt)
        '    If arlFS.Count > 0 Then
        '        'Cek apakah chassis termasuk dalam pengecualian 2x FS dalam  30 hari (PC = 30, LVC = 14) dalam hal ini data retrieve sudah difilter data 30 hari ke belakang dari tanggal pengajuan FS
        '        Dim oFSExisting As New FreeService
        '        If arlFS.Count = 1 Then
        '            oFSExisting = CType(arlFS(0), FreeService)
        '        Else
        '            oFSExisting = CType(arlFS(1), FreeService)       'ambil data ke 2, karena data paling atas adalah data yang diedit
        '        End If
        '        Dim cat = oFSExisting.ChassisMaster.VechileColor.VechileType.Category.ID

        '        If cat = 1 Then  'jika kategorinya PC, maka tidak valid
        '            MessageBox.Show("Chassis " + txtChassisMaster.Text + " Kategori PC. Tanggal Service kurang dari 30 Hari dari tanggal service terakhir.")
        '            bCheck = False
        '        ElseIf cat = 2 Then     'jika kategori LVC, maka cek apakah pengajuan lebih dari 14 hari dari pengajuan sebelumnya
        '            Dim d As DateTime = ToDate(txtTglServis.Text.Trim)
        '            If oFSExisting.ServiceDate.AddDays(14) > d Then
        '                MessageBox.Show("Chassis " + txtChassisMaster.Text + " Kategori LCV. Tanggal Service kurang dari 14 Hari dari tanggal service terakhir.")
        '                bCheck = False
        '            Else
        '                bCheck = True
        '            End If
        '        End If
        '    End If
        'End If

        If bCheck Then
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'Dim strChassisNumber = txtChassisMaster.Text.Trim.Substring(1, Len(Trim(txtChassisMaster.Text)) - 1)
            'Dim strChassisNumber = txtChassisMaster.Text.Trim.Substring(Len(fsKindCode), Len(Trim(txtChassisMaster.Text)) - Len(fsKindCode))
            Dim strChassisNumber = txtChassisMaster.Text.Trim()
            Dim strEngineNumber = txtEngineNumber.Text.Trim()
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, strChassisNumber))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ID", MatchType.No, objFreeService.ChassisMaster.ID))
            Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias2)
            If ChassisColl.Count <= 0 Then
                Dim criterias3 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
                criterias3.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strDealerCode))
                Dim DealerColl As ArrayList = New DealerFacade(User).Retrieve(criterias3)
                If DealerColl.Count > 0 Then
                    'Dim criterias4 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'criterias4.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, txtDealerBranchCode.Text))
                    'Dim DealerBranchColl As ArrayList = New DealerBranchFacade(User).Retrieve(criterias4)
                    'If DealerBranchColl.Count > 0 Then
                    If Not IsExistCodeForUpdate(objFreeService.ID, objFreeService.ChassisMaster.ID, objFreeService.FSKind.ID) Then
                        'Jika ok isi dulu yang masih diperlukan
                        objFreeService.MileAge = CType(txtKM.Text.Trim, Integer)
                        objFreeService.Status = EnumFSStatus.FSStatus.Baru
                        objFreeService.WorkOrderNumber = txtWONumber.Text.Trim()
                        'Remark    :CR;by:anh;for:rina;on:20100827;remark:udah di berlaku untuksemua dealer
                        'If ValidateLBUMBengkulu(objFreeService, True) = False Then
                        '    MessageBox.Show(SR.SaveFail)
                        '    Exit Sub
                        'End If
                        'end remarks by anh

                        'lalu update
                        'Dim objType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}
                        ''End    :CR - Temporary allowing TU00 :Rina
                        'Dim IsAllowToUpdate As Boolean = True
                        'For i As Integer = 0 To objType.Length - 1
                        '    If objFreeService.ChassisMaster.VechileColor.VechileType.VechileTypeCode = objType(i) And (objFreeService.FSKind.KindCode = "3" Or objFreeService.FSKind.KindCode = "4" Or objFreeService.FSKind.KindCode = "5") Then
                        '        IsAllowToUpdate = False
                        '        Exit For
                        '    End If
                        'Next

                        'IsAllowToUpdate = Me.ChassisException(IsAllowToUpdate, objFreeService)
                        'IsAllowToUpdate = Me.IsFE7orFE8(IsAllowToUpdate, objFreeService)
                        'IsAllowToUpdate = Me.CheckToAllowStradaTriton(IsAllowToUpdate, objFreeService)
                        'IsAllowToUpdate = Me.IsFE75orFESHD(IsAllowToUpdate, objFreeService)
                        Dim oldFiles As String = objFreeService.FilePath
                        If isEditEvidence Then
                            Dim objAttachment As HttpPostedFile = CType(Session("FSEVIDENCE"), HttpPostedFile)
                            commitAttachment(objAttachment, objFreeService)
                        End If

                        'If Not IsAllowPKT(objFreeService) Then
                        '    Exit Sub
                        'End If
                        '
                        'If Len(fsKindCode) = 1 Then
                        'If txtChassisMaster.Text.Substring(0, 1).ToString() = "1" Or txtChassisMaster.Text.Substring(0, 1).ToString() = "2" Then
                        '    If ddlKind.SelectedValue.ToString() = "1" OrElse ddlKind.SelectedValue.ToString() = "2" Then
                        '        nResult = objFreeServiceFacade.Update(objFreeService)
                        '        If isEditEvidence And nResult > 0 Then
                        '            deleteSavedAttachment(oldFiles)
                        '        End If
                        '    Else
                        '        If objFreeServiceFacade.IsAllowFreeService(objFreeService) Then
                        '            nResult = objFreeServiceFacade.Update(objFreeService)
                        '            If isEditEvidence And nResult > 0 Then
                        '                deleteSavedAttachment(oldFiles)
                        '            End If
                        '        Else
                        '            deleteSavedAttachment(objFreeService)
                        '            MessageBox.Show("Update gagal, Chassis " & objFreeService.ChassisMaster.ChassisNumber & " tidak mendapat kupon Free Service")
                        '            nResult = -1
                        '        End If
                        '    End If
                        'Else
                        '    If objFreeServiceFacade.IsAllowFreeService(objFreeService) Then
                        '        nResult = objFreeServiceFacade.Update(objFreeService)
                        '        If isEditEvidence And nResult > 0 Then
                        '            deleteSavedAttachment(oldFiles)
                        '        End If
                        '    Else
                        '        deleteSavedAttachment(objFreeService)
                        '        MessageBox.Show("Update gagal, Chassis " & objFreeService.ChassisMaster.ChassisNumber & " tidak mendapat kupon Free Service")
                        '        nResult = -1
                        '    End If
                        'End If


                        If nResult = -1 Then
                            MessageBox.Show("Update Gagal")
                        Else
                            MessageBox.Show("Update Sukses")
                            ClearDataAfterSaving()
                            Dim strScript As String
                            strScript = "<script>document.all.txtChassisMaster.focus();</script>"
                            Page.RegisterStartupScript("", strScript)
                        End If
                    Else
                        MessageBox.Show("No. Rangka dengan jenis servis tersebut sudah ada")
                    End If
                    'Else
                    '    MessageBox.Show("Kode Cabang Dealer tidak terdaftar ")
                    'End If
                Else
                    MessageBox.Show("Kode Dealer tidak terdaftar !")
                End If
            Else
                MessageBox.Show("No. Rangka sudah ada !")
            End If
        End If
        'Else
        '    MessageBox.Show("Kode FS Kind dan No Rangka Tidak boleh kosong")
        'End If

        Session("ISEDITEVIDENCE") = Nothing
        Session("FSEVIDENCE") = Nothing

    End Sub

    Private Function ValidateLBUMBengkulu(ByVal oFS As FreeService, ByVal Filter1Status As Boolean) As Boolean
        Dim oD As Dealer = CType(Session.Item("DEALER"), Dealer)
        If oFS.FSKind.KindCode = "1" Or oFS.FSKind.KindCode = "2" Then
            Return True
        End If

        If (oFS.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE7" _
        Or oFS.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE8") _
        Then
            If (oFS.FSKind.KindCode = "3" OrElse oFS.FSKind.KindCode = "4" OrElse oFS.FSKind.KindCode = "5" OrElse oFS.FSKind.KindCode = "6" OrElse oFS.FSKind.KindCode = "7") _
            And oD.DealerCode = "100016" _
            AndAlso (oFS.ChassisMaster.EndCustomer.OpenFakturDate >= DateSerial(2010, 2, 1) _
            And oFS.ChassisMaster.EndCustomer.OpenFakturDate <= DateSerial(2010, 6, 30)) _
            Then
                Return True
            Else
                Return False
            End If
        Else
            Return Filter1Status
        End If

        'If (oFS.FSKind.KindCode = "3" OrElse oFS.FSKind.KindCode = "4" OrElse oFS.FSKind.KindCode = "5" OrElse oFS.FSKind.KindCode = "6") _
        'AndAlso (oFS.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE7" _
        'Or oFS.ChassisMaster.VechileColor.VechileType.Description.Substring(0, 3).ToUpper = "FE8") _
        'Then
        '    If oD.DealerCode = "100016" _
        '    AndAlso (oFS.ChassisMaster.EndCustomer.OpenFakturDate >= DateSerial(2010, 2, 1) _
        '    And oFS.ChassisMaster.EndCustomer.OpenFakturDate <= DateSerial(2010, 6, 30)) _
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
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Status", MatchType.Exact, CType(EnumFSStatus.FSStatus.Baru, String)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Dealer.ID", MatchType.Exact, CType(TmpObjDealer.ID, Integer)))
            Dim branchCode As String = txtDealerBranchCode.Text.Trim()
            If Not String.IsNullorEmpty(branchCode) Then
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "DealerBranch.DealerBranchCode", MatchType.Exact, branchCode))
                Dim ObjDealerBranch As DealerBranch
                Dim criterias4 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias4.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, txtDealerBranchCode.Text))
                Dim DealerBranchColl As ArrayList = New DealerBranchFacade(User).Retrieve(criterias4)
                If DealerBranchColl.Count > 0 Then
                    ObjDealerBranch = CType(DealerBranchColl(0), DealerBranch)
                    txtBranchName.Text = ObjDealerBranch.Name
                End If
            End If
            'dgFreeServisEntry.DataSource = New FreeServiceFacade(User).Retrieve(criterias)
            ' ery puts some changes
            _sessHelper.SetSession("SortViewFS", criterias)
            dgFreeServisEntry.DataSource = New FreeServiceFacade(User).RetrieveActiveList(criterias, indexPage + 1, dgFreeServisEntry.PageSize, totRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgFreeServisEntry.VirtualItemCount = totRow

            Dim al As ArrayList = dgFreeServisEntry.DataSource
            _sessHelper.SetSession("SessArrFreeService", dgFreeServisEntry.DataSource)
            If al.Count > 0 Then
                btnRelease.Enabled = True
                btnRilis.Disabled = False
            Else
                btnRelease.Enabled = False
                btnRilis.Disabled = True
            End If
            dgFreeServisEntry.DataBind()
        End If

    End Sub

    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dgFreeServisEntry.DataSource = New FreeServiceFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("sortViewFS"), CriteriaComposite), indexPage + 1, dgFreeServisEntry.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgFreeServisEntry.VirtualItemCount = totalRow
            dgFreeServisEntry.DataBind()
        End If

    End Sub

    Private Sub InitiatePage()
        'SetControlPrivilege()
        ViewState("currentSortColumn") = "ID"
        ViewState("currentSortDirection") = Sort.SortDirection.DESC

        ScriptManager.RegisterStartupScript(Me, Page.GetType, "CallFunction", "firstFocus();", True)

        txtChassisMaster.Attributes.Add("onkeydown", "enter(document.all.txtKM)")
        txtKM.Attributes.Add("onkeydown", "enter(document.all.txtTglServis)")
        txtTglServis.Attributes.Add("onkeydown", "enter(document.all.txtTglJual)")
        txtTglJual.Attributes.Add("onkeydown", "enter(document.all.btnSimpan)")
        btnSimpan.Attributes.Add("onkeydown", "enter(document.all.txtChassisMaster)")

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim ObjfsKind As ArrayList = New ArrayList
        ObjfsKind = New FSKindFacade(User).Retrieve(criterias)

        Dim dtFs As New DataTable()
        dtFs.Columns.Add("KindCode", GetType(String))
        dtFs.Columns.Add("KindDescription", GetType(String))
        dtFs.Rows.Add("-1", "Silahkan Pilih")
        For Each ObjKind As FSKind In ObjfsKind
            dtFs.Rows.Add(ObjKind.KindCode, ObjKind.KindCode.PadLeft(3) + " - " + ObjKind.KindDescription)
        Next

        Me.ddlKind.DataSource = dtFs
        Me.ddlKind.DataTextField = "KindDescription"
        Me.ddlKind.DataValueField = "KindCode"
        Me.ddlKind.DataBind()
        BindVisitType()

        Dim oCfg As New AppConfigFacade(User)


        lblMaxFileSize.Text = oCfg.Retrieve("FreeService.MaxFileSize").Value
        lblSupportedFormat.Text = oCfg.Retrieve("FreeService.SupportedFile").Value
        Label8.Text = IIf(CInt(lblMaxFileSize.Text) > 1000, String.Format("{0} MB, File yang didukung : ", (CInt(lblMaxFileSize.Text) / 1024).ToString()), String.Format("{0} KB, File yang didukung : ", (CInt(lblMaxFileSize.Text)).ToString()))
    End Sub

    Private Sub ClearDataAfterSaving()
        txtChassisMaster.Text = String.Empty
        txtEngineNumber.Text = String.Empty
        txtKM.Text = String.Empty
        txtWONumber.Text = String.Empty
        txtDealerBranchCode.Text = String.Empty
        txtBranchName.Text = String.Empty

        txtTglJual.Text = String.Empty
        txtTglServis.Text = String.Empty
        txtChassisMaster.ReadOnly = False
        txtEngineNumber.ReadOnly = False
        txtKM.ReadOnly = False
        txtTglServis.ReadOnly = False
        txtTglJual.ReadOnly = False
        txtWONumber.ReadOnly = False
        ddlVisitType.SelectedIndex = 0
        ddlKind.Enabled = True
        ddlKind.SelectedValue = "-1"
        btnSave.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        Session("FSEVIDENCE") = Nothing
        lblUploadedFile.Text = String.Empty

    End Sub

    Private Sub ClearData()
        txtChassisMaster.Text = String.Empty
        txtEngineNumber.Text = String.Empty
        txtKM.Text = String.Empty
        txtTglServis.Text = String.Empty
        txtTglJual.Text = String.Empty
        ddlVisitType.SelectedIndex = 0
        txtWONumber.Text = String.Empty

        txtChassisMaster.ReadOnly = False
        txtEngineNumber.ReadOnly = False
        txtKM.ReadOnly = False
        txtTglServis.ReadOnly = False
        txtTglJual.ReadOnly = False
        txtWONumber.ReadOnly = False
        ddlKind.Enabled = True
        btnSave.Enabled = True
        lblUploadedFile.Text = String.Empty
        ViewState.Add("vsProcess", "Insert")
        Try
            ddlKind.SelectedValue = "-1"

        Catch ex As Exception

        End Try

    End Sub

    Private Function IsExistCodeForInsert(ByVal ChassisID As Integer, ByVal FSKindID As Integer) As Boolean
        Return False ' Centralize
        Dim objFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
        'Periksa agar tidak ada key ganda 
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, ChassisID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "FSKind.ID", MatchType.Exact, FSKindID))
        Dim TestExist As ArrayList = New FreeServiceFacade(User).Retrieve(criterias)

        If TestExist.Count > 0 Then
            'Dim critFSExt As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'critFSExt.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSKind.ID", MatchType.Exact, FSKindID))
            'critFSExt.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSType", MatchType.InSet, "(7,8,9)"))

            'Dim arlFSExt As ArrayList = New FSKindOnVechileTypeFacade(User).Retrieve(critFSExt)
            'If arlFSExt.Count = 0 Then
            '    Return True
            'Else
            '    Return False
            'End If
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsExistCodeForUpdate(ByVal FSID As Integer, ByVal ChassisID As Integer, ByVal FSKindID As Integer) As Boolean
        Dim objFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
        'Periksa agar tidak ada key ganda 
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, ChassisID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "FSKind.ID", MatchType.Exact, FSKindID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Status", MatchType.Exact, EnumFSStatus.FSStatus.Baru))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "ID", MatchType.No, FSID))
        Dim TestExist As ArrayList = New FreeServiceFacade(User).Retrieve(criterias)
        If TestExist.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function GetCheckedFSItem() As ArrayList
        'dgFreeServisEntry.DataSource = CType(Session("SessArrFreeService"), ArrayList)
        Dim arlCheckedFSItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgFreeServisEntry.Items
            nIndeks = dtgItem.ItemIndex
            Dim objFS As FreeService = CType(CType(dgFreeServisEntry.DataSource, ArrayList)(nIndeks), FreeService)
            If CType(dtgItem.Cells(2).FindControl("cbSelect"), CheckBox).Checked Then
                If Not IsNothing(objFS) Then
                    If objFS.Status = CType(EnumFSStatus.FSStatus.Baru, String) Then
                        '  objFS.Status = CType(EnumFSStatus.FSStatus.Proses, String)
                        arlCheckedFSItem.Add(objFS)
                    End If
                End If
            End If
        Next
        Return arlCheckedFSItem
    End Function

    Private Function GetMaxPM(ByVal oMSPExType As MSPExType) As ArrayList
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExMappingtoFSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(MSPExMappingtoFSKind), "MSPExType.ID", MatchType.Exact, oMSPExType.ID))
        Return New MSPExMappingtoFSKindFacade(User).Retrieve(crit)
    End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()

        If Not IsPostBack Then
            If Not IsNothing(Session("DEALER")) Then
                If Not IsDownloaded() Then
                    Dim strMessage As String = String.Empty
                    strMessage = GetMonthlyFaultDescription()
                    Dim strMessageHeader As String = "Anda belum melakukan download atau kirim dokumen Kwitansi FS Campaign/FS Letter/FS Campaign Letter/Kwitansi Free Labour/Free Labour Letter/Free Maintenance Letter/Kwitansi Free Maintenance pada periode berikut : "
                    Server.Transfer("../FrmAccessDenied.aspx?isEncode=1&mess=" & Server.UrlEncode(strMessageHeader) & "&messDescription=" & Server.UrlEncode(strMessage) & "")
                End If


                InitiatePage()
                Dim ObjDealer As Dealer = CType(Session.Item("DEALER"), Dealer)

                If 1 = 1 Then
                    Dim oCfg As New AppConfigFacade(User)

                    Dim MinSize As Integer = 0
                    MinSize = oCfg.Retrieve("MinFileSize").Value
                    hdnMinSize.Value = MinSize.ToString()
                End If
                ''CR Tutup Menu
                '' by ali
                '' 2014 - 09 -30

                If (DateTime.Now >= New DateTime(2014, 10, 10) AndAlso DateTime.Now <= New DateTime(2014, 10, 11).AddMinutes(-1) AndAlso ObjDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    Dim MSgClose As String = IIf(Not IsNothing(KTB.DNet.Lib.WebConfig.GetValue("CloseMessage")), KTB.DNet.Lib.WebConfig.GetValue("CloseMessage"), "Module ini sedang di tutup, sampai dengan 10 Oktober 2014")
                    Server.Transfer("../ClossingMessage.htm")
                End If

                ''END CR Tutup Menu
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


        'FreeServiceDataUpdate_Privilege
        m_bFreeServiceDataUpdate_Privilege = SecurityProvider.Authorize(Context.User, SR.FreeServiceDataUpdate_Privilege)

        'FreeServiceDataDelete_Privilege
        m_bFreeServiceDataDelete_Privilege = SecurityProvider.Authorize(Context.User, SR.FreeServiceDataDelete_Privilege)

        'FreeServiceDataSave_Privilege
        m_bFreeServiceDataSave_Privilege = SecurityProvider.Authorize(Context.User, SR.FreeServiceDataSave_Privilege)

        If SecurityProvider.Authorize(Context.User, SR.FreeServiceDataSave_Privilege) Or m_bFreeServiceDataUpdate_Privilege Then
            btnSimpan.Visible = True
            btnBatal.Visible = True

        Else
            btnSimpan.Visible = False
            btnBatal.Visible = False
            btnUpload.Enabled = False
        End If

        'FreeServiceDataRelease_Privilege
        m_bFreeServiceDataRelease_Privilege = SecurityProvider.Authorize(Context.User, SR.FreeServiceDataRelease_Privilege)
        btnRilis.Visible = m_bFreeServiceDataRelease_Privilege

    End Sub
    Private Function IsDownloaded() As Boolean
        Dim _return As Boolean = True
        'Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        'Dim ArlMonthly As ArrayList = New ArrayList
        'Try
        '    Dim paramDate As DateTime = New DateTime(1900, 1, 1)
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "ProductCategoryID", MatchType.Exact, "1"))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "DealerCode", MatchType.Exact, objDealer.DealerCode))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(2, 4, 5, 12, 13, 14, 15)"))

        '    'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Exact, "2017"), "((", True)
        '    'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "4"), ")", False)

        '    'criterias.opOr(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Greater, "2017"), "(", True)
        '    'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "1"), "))", False)

        '    'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "CreatedTime", MatchType.GreaterOrEqual, Date.Now.AddMonths(-1)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.GreaterOrEqual, New DateTime(2017, 4, 1)))


        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.IsNull, True), "(", True)
        'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
        'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "(", True)
        'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), ")", False)
        'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
        'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
        'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.IsNull, True), ")", False)


        '    Dim dtn As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-2)


        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.LesserOrEqual, dtn))


        '    Dim sortColl As SortCollection = New SortCollection
        '    sortColl.Add(New Sort(GetType(V_MonthlyReport), "Period", Sort.SortDirection.DESC))


        '    ArlMonthly = New V_MonthlyReportFacade(User).Retrieve(criterias, sortColl)
        '    If Not IsNothing(ArlMonthly) AndAlso ArlMonthly.Count > 0 Then
        '        Dim vM As New V_MonthlyReport
        '        vM = CType(ArlMonthly(0), V_MonthlyReport)

        '        If 1 = 1 OrElse (vM.Period.Year = dtn.Year AndAlso dtn.Month = vM.Period.Month) Then
        '            _return = False
        '        Else
        '            Return True
        '        End If



        '        _return = False
        '    Else
        '        _return = True
        '    End If
        'Catch ex As Exception
        '    _return = False
        'End Try
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
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(2, 4, 5, 12, 13, 14, 15)"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(2, 4, 5, 12, 13, 14, 15, 23, 24)"))

            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Exact, "2017"), "((", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "4"), ")", False)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "4"))

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "PeriodeYear", MatchType.GreaterOrEqual, "2017"))
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Greater, "2017"), "(", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "1"), ")", False)

            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "CreatedTime", MatchType.GreaterOrEqual, Date.Now.AddMonths(-1)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.GreaterOrEqual, New DateTime(2017, 4, 1)))


            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.IsNull, True), "(", True)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "(", True)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), ")", False)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.IsNull, True), ")", False)


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

    Private Sub dgFreeServisEntry_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgFreeServisEntry.PageIndexChanged
        dgFreeServisEntry.SelectedIndex = -1
        dgFreeServisEntry.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dgFreeServisEntry.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dgFreeServisEntry_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgFreeServisEntry.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As FreeService = CType(e.Item.DataItem, FreeService)
            rilisItem = RowValue
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgFreeServisEntry.CurrentPageIndex * dgFreeServisEntry.PageSize) 'getDataGridItemIndex()
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                e.Item.FindControl("lbtnDelete").Visible = m_bFreeServiceDataDelete_Privilege
                Dim strMsg As String = "Yakin Data dengan nomor rangka " & CType(e.Item.FindControl("lblChassisNo"), Label).Text & "  ini akan dihapus? "
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & strMsg & "');")
            End If

            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                e.Item.FindControl("lbtnEdit").Visible = m_bFreeServiceDataUpdate_Privilege
            End If

            If Not e.Item.FindControl("btnDownload") Is Nothing Then
                If RowValue.FileName = String.Empty Then
                    e.Item.FindControl("btnDownload").Visible = False
                End If
            End If

            'If Not e.Item.FindControl("cbSelect") Is Nothing Then
            '    e.Item.FindControl("cbSelect").Visible = m_bFreeServiceDataRelease_Privilege
            'End If



            Dim lbltglJual As Label = CType(e.Item.FindControl("tglPenjualan"), Label)
            If Not IsNothing(RowValue.SoldDate) Then
                If RowValue.SoldDate = "1/1/1900" Then
                    lbltglJual.Text = ""
                End If
            End If

        End If
    End Sub

    Private Sub dgFreeServisEntry_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgFreeServisEntry.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtChassisMaster.ReadOnly = True
            txtKM.ReadOnly = True
            txtTglServis.ReadOnly = True
            txtTglJual.ReadOnly = True
            txtEngineNumber.ReadOnly = True
            ddlKind.Enabled = False
            ViewFreeService(e.Item.Cells(0).Text, False)

        ElseIf e.CommandName = "Edit" Then

            ViewState.Add("vsProcess", "Edit")
            ViewFreeService(e.Item.Cells(0).Text, True)
            dgFreeServisEntry.SelectedIndex = e.Item.ItemIndex
            txtChassisMaster.Enabled = True
            txtKM.Enabled = True
            txtTglServis.Enabled = True
            txtTglJual.Enabled = True
            txtChassisMaster.ReadOnly = True
            txtEngineNumber.ReadOnly = True
            txtKM.ReadOnly = False
            txtTglServis.ReadOnly = False
            txtTglJual.ReadOnly = False
            ddlKind.Enabled = False

        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteFreeServis(e.Item.Cells(0).Text)
                MessageBox.Show("Hapus Sukses !")
            Catch ex As Exception
                MessageBox.Show("Gagal Menghapus !")
            End Try
            dgFreeServisEntry.SelectedIndex = -1
            ClearData()

        ElseIf e.CommandName = "Download" Then
            Dim oFS As FreeService = CType(Session("SessArrFreeService"), ArrayList)(e.Item.ItemIndex)
            Response.Redirect("../Download.aspx?file=" & oFS.FilePath & "&name=" & Path.GetFileNameWithoutExtension(oFS.FileName))
        End If
    End Sub

    Private Function alreadyPM() As Boolean
        Dim _rets As Boolean = True
        Dim _fsType As Integer

        Integer.TryParse(checkFSLabor().FSType, _fsType)

        If _fsType = 2 Then
            Dim arlPMHeader As ArrayList
            Dim criteriasPMHeader As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasPMHeader.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
            arlPMHeader = New PMHeaderFacade(User).Retrieve(criteriasPMHeader)
            If arlPMHeader.Count > 0 Then
                _rets = True
                'Cek PMHeader yang Labor
            Else
                _rets = False
                'cek ada apa ngga di pm header kalo ada bisa insert
            End If
        End If

        Return _rets
    End Function

    Private Function checkFSLabor() As FSKind
        Dim arlistFSKind As ArrayList
        Dim criteriasFSKind As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasFSKind.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "KindCode", MatchType.Exact, ddlKind.SelectedValue))
        Return CType(New FSKindFacade(User).Retrieve(criteriasFSKind)(0), FSKind)
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
        Dim objFreeService As FreeService = buildFreeServiceObject()

        'If ddlKind.SelectedValue.ToLower() = "-1" Then
        '    MessageBox.Show("Silahkan mengisi FsKind")
        '    Return
        'End If

        Dim strChassisNumber = txtChassisMaster.Text.Trim()
        Dim strEngineNumber = txtEngineNumber.Text.Trim()
        'If Not getMatch(strChassisNumber, strEngineNumber) Then
        '    MessageBox.Show("Nomor Mesin tidak sesuai")
        '    Return


        ''If checkFSLabor.FSType = 2 Then
        'If ddlVisitType.SelectedIndex = 0 Then
        '    MessageBox.Show("Silahkan Pilih Tipe Visit")
        '    Return
        'End If

        Dim evidence As HttpPostedFile = CType(Session("FSEVIDENCE"), HttpPostedFile)


        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If IsNothing(evidence) Or lblUploadedFile.Text = String.Empty Then
                MessageBox.Show("Silahkan masukan evidence WO yang telah dilengkapi tanda tangan konsumen dan petugas Dealer untuk pengajuan FS Claim ke APM")
                Return
            End If
        Else
            Dim objUpdateFreeService As FreeService = CType(Session.Item("vsFreeService"), FreeService)
            If objUpdateFreeService.FileName = String.Empty Then
                If IsNothing(evidence) Or lblUploadedFile.Text = String.Empty Then
                    MessageBox.Show("Silahkan masukan evidence WO yang telah dilengkapi tanda tangan konsumen dan petugas Dealer untuk pengajuan FS Claim ke APM")
                    Return
                End If
            Else
                If Not IsNothing(evidence) And Not lblUploadedFile.Text = String.Empty Then
                    Session("ISEDITEVIDENCE") = True
                End If
            End If
        End If

        'If Not alreadyPM() Then
        '    MessageBox.Show("Data Simpan Gagal \nNomor rangka belum pernah melakukan servis PM")
        '    Return
        'End If 

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, strChassisNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
        Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
        If ChassisColl.Count > 0 Then
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                If m_bFreeServiceDataSave_Privilege Then
                    InsertobjFreeService(objFreeService)

                    dgFreeServisEntry.SelectedIndex = -1
                Else
                    MessageBox.Show("Anda tidak punya hak untuk menginsert data baru !")
                End If
            Else
                If m_bFreeServiceDataUpdate_Privilege Then
                    Dim objUpdateFreeService As FreeService = CType(Session.Item("vsFreeService"), FreeService)
                    objUpdateFreeService.VisitType = ddlVisitType.SelectedValue
                    UpdateObjFreeService(objUpdateFreeService)
                    dgFreeServisEntry.SelectedIndex = -1
                Else
                    MessageBox.Show("Anda tidak punya hak untuk mengupdate data lama !")
                End If
            End If
        Else
            MessageBox.Show("Chassis tidak terdaftar di " + companyCode)
        End If

        BindDatagrid(dgFreeServisEntry.CurrentPageIndex)
        Dim strScript As String
        strScript = "<script>document.all.txtChassisMaster.focus();</script>"
        Page.RegisterStartupScript("", strScript)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ClearData()
        txtDealerBranchCode.Text = String.Empty
        txtBranchName.Text = String.Empty
        dgFreeServisEntry.SelectedIndex = -1
        Dim strScript As String
        strScript = "<script>document.all.txtChassisMaster.focus();</script>"
        Page.RegisterStartupScript("", strScript)
    End Sub

    Private Function IsValidToExecute(ByVal oFS As FreeService) As Boolean
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

    Private Function GetProductCategoryCode(ByVal aFreeServices As ArrayList) As String
        Dim product As String = ""

        For Each oFreeService As FreeService In aFreeServices
            If product = "" Then
                product = oFreeService.ChassisMaster.Category.ProductCategory.Code
            Else
                If product <> oFreeService.ChassisMaster.Category.ProductCategory.Code Then
                    Return ""
                End If
            End If
        Next
        Return product
    End Function

    Private Function IsChassisAndKMEquals(ByVal parPmKindCode As String, Optional ByVal parID As Integer = 0) As Boolean
        '---cek Chassis yang sama dengan KM yang sama
        Dim StrFunction As String = String.Format("(SELECT ID FROM dbo.fn_PmHeaderChecking('{0}','{1}',{2}))", txtChassisMaster.Text.Trim(), parPmKindCode, parID.ToString())
        Dim checkRuleChassis_No As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        checkRuleChassis_No.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
        checkRuleChassis_No.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ID", MatchType.InSet, StrFunction))
        Dim arlRuleChassis As ArrayList = New PMHeaderFacade(User).Retrieve(checkRuleChassis_No)
        If arlRuleChassis.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsChassisAndPMDateEquals(ByVal ChassisNumber As String, ByVal tglServis As DateTime) As Boolean
        '---cek chasiss dgn tanggal PM
        Dim checkRule1 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim checkRuleChassis_Date As CriteriaComposite = checkRule1
        checkRuleChassis_Date.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, ChassisNumber.Trim))
        checkRuleChassis_Date.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ServiceDate", MatchType.Exact, tglServis))
        Dim arlCek As ArrayList = New PMHeaderFacade(User).Retrieve(checkRuleChassis_Date)
        If arlCek.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function IsExistCodeForInsert(ByVal ChassisID As Integer, ByVal DealerID As Integer, ByVal _serviceDate As Date) As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PMHeader), "ChassisMaster.ID", MatchType.Exact, ChassisID))
        'remark by anh, req by rna 20100805
        'criterias.opAnd(New Criteria(GetType(PMHeader), "Dealer.ID", MatchType.Exact, DealerID))
        'end remark
        criterias.opAnd(New Criteria(GetType(PMHeader), "ServiceDate", MatchType.LesserOrEqual, New Date(_serviceDate.Year, _serviceDate.Month, _serviceDate.Day, 23, 59, 59)))
        criterias.opAnd(New Criteria(GetType(PMHeader), "ServiceDate", MatchType.GreaterOrEqual, New Date(_serviceDate.Year, _serviceDate.Month, _serviceDate.Day, 0, 0, 0)))
        Dim TestExist As ArrayList = New PMHeaderFacade(User).Retrieve(criterias)
        If TestExist.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function insertPMHeader() As Integer

        Dim objPMHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
        Dim _res As Integer = -1
        dgFreeServisEntry.DataSource = CType(Session("SessArrFreeService"), ArrayList)
        Dim ReleasedItems As ArrayList = New ArrayList
        ReleasedItems = GetCheckedFSItem()


        Session("vsPMHeader") = Nothing
        Dim arrvsPMHeader As New ArrayList
        For Each item As FreeService In ReleasedItems
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "VechileType.ID", MatchType.Exact, CType(item.ChassisMaster.VechileColor.VechileType.ID, Integer)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSKind.ID", MatchType.Exact, CType(item.FSKind.ID, Integer)))
            Dim arrayCM As ArrayList = New FSKindOnVechileTypeFacade(User).Retrieve(criterias)
            Dim objPMHeader As PMHeader = New PMHeader
            If arrayCM.Count > 0 Then

                'objPMHeader.EntryType = New EnumFSKind().TypeByIndex(Integer.Parse(CType(arrayCM(0), FSKindOnVechileType).FSType))
                If Not IsNothing(CType(arrayCM(0), FSKindOnVechileType).FSType) AndAlso Not String.IsNullorEmpty(CType(arrayCM(0), FSKindOnVechileType).FSType) Then
                    objPMHeader.EntryType = CType(CInt(CType(arrayCM(0), FSKindOnVechileType).FSType), EnumFSKind.FSType).ToString()
                Else
                    objPMHeader.EntryType = "Free Service"
                End If

            Else
                objPMHeader.EntryType = "Free Service"
            End If

            'If item.FSKind.FSType = 2 Then
            'Dim objPMHeader As PMHeader = New PMHeader
            objPMHeader.Dealer = item.Dealer
            objPMHeader.ChassisMaster = item.ChassisMaster
            objPMHeader.PMKind = New PMKindFacade(User).RetrievePMKind(item.MileAge)
            objPMHeader.StandKM = item.MileAge
            objPMHeader.ServiceDate = item.ServiceDate
            objPMHeader.ReleaseDate = Today
            objPMHeader.PMStatus = EnumPMStatus.PMStatus.Proses
            'objPMHeader.EntryType = "Labor"
            'objPMHeader.VisitType = "WI"
            objPMHeader.VisitType = item.VisitType
            objPMHeader.Remarks = ""
            If Not IsExistCodeForInsert(objPMHeader.ChassisMaster.ID, objPMHeader.Dealer.ID, objPMHeader.ServiceDate) Then
                If IsChassisAndKMEquals(objPMHeader.PMKindCode) = True Then
                    Continue For
                    'MessageBox.Show("Nomor Rangka dan KM sudah pernah disimpan")
                    ''MessageBox.Show("Data PM chassis sudah terdaftar")
                    'Exit Function
                End If
                If IsChassisAndPMDateEquals(objPMHeader.ChassisMaster.ChassisNumber, objPMHeader.ServiceDate) = True Then
                    'MessageBox.Show("Nomor Rangka dan Tanggal PM sudah ada")
                    'Exit Function
                    Continue For
                End If
                If PMHeaderIsExist(item.ChassisMaster.ID, objPMHeader.PMKind.ID) Then
                    'Session.Add("vsPMHeader", objPMHeader)
                    'ClearDataAfterSaving()
                    Dim strScript As String
                    'strScript = "<script>document.all.txtChassisMaster.focus();</script>"
                    'Page.RegisterStartupScript("", strScript)
                    Continue For
                End If
                _res = New PMHeaderFacade(User).Insert(objPMHeader)
                If _res = -1 Then
                    'MessageBox.Show("Simpan Gagal")
                Else
                    arrvsPMHeader.Add(objPMHeader)
                    'MessageBox.Show("Simpan Sukses")
                    'Todo session

                    'ClearDataAfterSaving()
                    'Dim strScript As String
                    'strScript = "<script>document.all.txtChassisMaster.focus();</script>"
                    'Page.RegisterStartupScript("", strScript)
                End If
                'Else
                '    MessageBox.Show("Nomor rangka dengan dealer pada tanggal service yang dientry sudah ada")
            End If

            'End If

        Next
        ' Session.Add("vsPMHeader", arrvsPMHeader)

        If _res > 0 Then
            Dim objPMColl As ArrayList = New ArrayList
            Dim aPMA As New ArrayList
            Dim aPMB As New ArrayList
            Dim arrMSP As New ArrayList

            Dim CheckedPMItemColl As ArrayList = New ArrayList
            CheckedPMItemColl = arrvsPMHeader 'GetCheckedPMItem(arrvsPMHeader)

            For Each ObjPMHeader As PMHeader In CheckedPMItemColl
                ObjPMHeader.ReleaseDate = Today
                ObjPMHeader.PMStatus = EnumPMStatus.PMStatus.Proses
                objPMColl.Add(ObjPMHeader)
                If 1 = 1 Then '(ObjPMHeader.ChassisMaster.Category.ProductCategory.Code.ToLower() = "mmc") Then
                    'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122
                    ' memisahkan PM MSP atau normal PM
                    If 1 = 0 AndAlso ObjPMHeader.Remarks <> String.Empty And ObjPMHeader.IsValidMSP = True Then
                        arrMSP.Add(ObjPMHeader)
                    Else
                        aPMA.Add(ObjPMHeader)
                    End If
                    'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122
                Else
                    aPMB.Add(ObjPMHeader)
                End If
            Next


            If (aPMA.Count > 0) Then
                TransferToSAP(aPMA, "Normal", "mmc")
            End If
            'If (aPMB.Count > 0) Then
            '    TransferToSAP(aPMB, "Normal", "mftbc")
            'End If

            'If (arrMSP.Count > 0) Then
            '    TransferToSAP(arrMSP, "msp", "mmc")
            'End If


        End If
        Return _res
    End Function

    Private Sub btnRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        'bookmark
        'Dim _res As Integer = insertPMHeader()
        'If _res = -1 Then
        '    MessageBox.Show("Simpan Data Gagal")
        '    Exit Sub
        'End If

        Dim bcheck As Boolean = False
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        'Dim imp As SAPImpersonate = New SAPImpersonate("sap", "7Karakter", "172.17.31.21")
        'Dim imp As SAPImpersonate = New SAPImpersonate("sap", "7Karakter", "172.17.104.68")
        Dim success As Boolean = False
        Dim sTimestamp As String = sSuffix
        Dim FSFileName As String = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\FS\" & "FSData" & sTimestamp & ".fsd"
        Dim FSFileNameLocal As String = Server.MapPath("") & "\..\DataTemp\FSData" & sTimestamp & ".fsd"
        dgFreeServisEntry.DataSource = CType(Session("SessArrFreeService"), ArrayList)
        For Each dtgItem As DataGridItem In dgFreeServisEntry.Items
            If CType(dtgItem.Cells(0).FindControl("cbSelect"), CheckBox).Checked Then
                bcheck = True
                Exit For
            End If
        Next

        Try
            success = imp.Start
            'test
            '' success = True
            If bcheck And success Then
                Dim CheckedFSItemColl As ArrayList = New ArrayList
                Dim arlTransferedToSAP As New ArrayList
                CheckedFSItemColl = GetCheckedFSItem()

                ''Commented Out by ali
                ''Comented Date : 2014-09-02
                'Dim Product As String = GetProductCategoryCode(CheckedFSItemColl)
                'If Product = "" Then
                '    MessageBox.Show("Produk yang akan dirilis harus sama")
                '    Exit Sub
                'End If
                ''End CommentedOut

                ''LOC 2014-09-02
                ''BY ALi 
                ''Splitting
                Dim aFSAs As New ArrayList
                Dim aFSBs As New ArrayList
                'Dim strChassisNumber As String

                For Each oFreeService As FreeService In CheckedFSItemColl
                    Dim oFreeServiceValidation As FreeServiceValidation = New FreeServiceValidation()
                    Dim ObjDealer As Dealer = CType(Session.Item("DEALER"), Dealer)

                    Dim listValidResult As List(Of ValidResult) = oFreeServiceValidation.ValidateFreeServiceCentralizeRelease(oFreeService, ObjDealer.DealerCode)
                    If listValidResult.Count > 0 Then
                        Dim erMes As StringBuilder = New StringBuilder
                        Dim i As Integer = 1
                        For Each item As ValidResult In listValidResult
                            erMes.Append(i & ". " & item.Message.ToString & "\n")
                            i += 1
                        Next
                        MessageBox.Show(erMes.ToString())
                        Exit Sub
                    End If

                    If oFreeService.ChassisMaster.Category.ProductCategory.Code.ToLower() = "mmc" Then
                        aFSAs.Add(oFreeService)
                    Else
                        aFSBs.Add(oFreeService)
                    End If

                    'CR GSR
                    'GSRRilisFS(oFreeService.ID)
                    Dim nResults As Integer = InsertModel(oFreeService)

                Next

                'FSFileName = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\FS\" & "FSData" & sTimestamp & "_" & Product.ToLower() & ".fsd"
                'FSFileNameLocal = Server.MapPath("") & "\..\DataTemp\FSData" & sTimestamp & "_" & Product.ToLower() & ".fsd"



                Dim nSavedData As Integer = 0


                If aFSAs.Count > 0 Then
                    FSFileName = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\FS\mmc\" & "FSData" & Me.GetSuffix() & "_" & CType(aFSAs(0), FreeService).ChassisMaster.Category.ProductCategory.Code.ToLower() & ".fsd"
                    FSFileNameLocal = Server.MapPath("") & "\..\DataTemp\FSData" & Me.GetSuffix() & "_" & CType(aFSAs(0), FreeService).ChassisMaster.Category.ProductCategory.Code.ToLower() & ".fsd"
                    nSavedData = AppendText(aFSAs, FSFileNameLocal, FSFileName, arlTransferedToSAP)
                End If
                If aFSBs.Count > 0 Then
                    FSFileName = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\FS\MFTBC\" & "FSData" & Me.GetSuffix() & "_" & CType(aFSBs(0), FreeService).ChassisMaster.Category.ProductCategory.Code.ToLower() & ".fsd"
                    FSFileNameLocal = Server.MapPath("") & "\..\DataTemp\FSData" & Me.GetSuffix() & "_" & CType(aFSBs(0), FreeService).ChassisMaster.Category.ProductCategory.Code.ToLower() & ".fsd"

                    nSavedData = AppendText(aFSBs, FSFileNameLocal, FSFileName, arlTransferedToSAP)
                End If

                ''END of LOC 2014-09-02

                If nSavedData < 1 Then
                    Dim sIndicator As String = ""
                    sIndicator = IIf(nSavedData = -1, ".", IIf(nSavedData = -1, ",", ""))
                    MessageBox.Show("Rilis data gagal" & sIndicator)
                    Exit Sub
                End If

                'sekarang updatenya
                Dim objFreeServisColl As ArrayList = New ArrayList
                If arlTransferedToSAP.Count > 0 Then
                    For Each ObjFreeServise As FreeService In arlTransferedToSAP
                        ObjFreeServise.ReleaseBy = User.Identity.Name
                        ObjFreeServise.ReleaseDate = Today
                        ObjFreeServise.Status = CType(EnumFSStatus.FSStatus.Proses, String)
                        objFreeServisColl.Add(ObjFreeServise)
                    Next
                    Dim nResult = New FreeServiceFacade(User).UpdateFSCollection(objFreeServisColl)
                    If nResult = 0 Then
                        MessageBox.Show("Input dan Rilis Sukses\nPengajuan telah terkirim ke MMKSI")
                    Else
                        MessageBox.Show("Input dan Rilis Gagal")
                    End If
                End If
            Else
                MessageBox.Show("Record Free Service belum dipilih !")
            End If
            BindDatagrid(dgFreeServisEntry.CurrentPageIndex)
            'Dim strScript As String
            'strScript = "<script>document.all.txtChassisMaster.focus();</script>"
            'Page.RegisterStartupScript("", strScript)
        Catch ex As Exception
            MessageBox.Show("Update Rilis gagal !")
        End Try
    End Sub

    Private Function InsertModel(ByVal oFS As FreeService) As Integer
        Dim objGSRStaging As GSRStaging = New GSRStaging


        Dim nResult As Integer
        objGSRStaging.RilisID = oFS.ID
        objGSRStaging.Tipe = "FS"
        objGSRStaging.Remark = "Ready To Process"
        objGSRStaging.Status = 0
        objGSRStaging.RowStatus = 0
        objGSRStaging.CreatedBy = ""
        objGSRStaging.CreatedTime = Date.Now
        objGSRStaging.LastUpdatedBy = ""
        objGSRStaging.LastUpdatedTime = Date.Now
        nResult = New GSRStagingFacade(User).Insert(objGSRStaging)
        'If nResult = -1 Then
        '    MessageBox.Show(SR.SaveFail)
        'Else
        '    MessageBox.Show(SR.SaveSuccess)
        'End If
        Return nResult
    End Function

    Private Sub GSRRilisFS(ByVal ID As Integer)
        Dim objServiceReminder As ServiceReminder = New ServiceReminder
        'objService = New ServiceStandardTimeFacade(User).Calculate(txtKodeDealer.Text.Trim(), "", ddlJenisKegiatan2.SelectedValue, ICPeriodFrom.Value)
        Dim RESULT As Integer = 0
        RESULT = New ServiceReminderFacade(User).GSRRilisFS(ID)

    End Sub

    Public Function IsTypeAllowed(ByVal oFS As FreeService) As Boolean
        Dim i As Integer
        Dim _restrictedType() As String = {"EN10", "EF10", "ET10", "EU10", "UD00", "TC00", "UC00", "EV10", "EW10", "WD00", "EC00", "HW00", "HL00", "ML00", "FL00", "MH00", "MF00", "FN00", "TN00", "DK00", "DL00", "DH00", "HH00", "MD0T", "TZ00", "TY00", "TX00", "TW00", "TV00", "TS00", "TQ00", "TP00", "TM00", "TR00", "PA00", "PC00", "PD00", "PN00", "PT00"}

        For i = 0 To _restrictedType.Length - 1
            If oFS.ChassisMaster.VechileColor.VechileType.VechileTypeCode.Trim.ToLower = _restrictedType(i).Trim.ToLower Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Function AppendTextPM(FSFileNameLocalPM As String, FSFileNamePM As String, arlTransferedToSAP As ArrayList) As Integer
        Dim strText As New StringBuilder
        Dim objAl As New ArrayList
        Dim nData As Integer = 0

        Try
            nData = 0
            If arlTransferedToSAP.Count > 0 Then

                strText = New StringBuilder
                For Each objFS As FreeService In arlTransferedToSAP
                    Dim isAllowInsert As Boolean = True
                    If isAllowInsert Then
                        'Dim objDealer As Dealer = New Dealer
                        Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
                        'Dim pmCrit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'pmCrit.opAnd(New Criteria(GetType(PMHeader), "ChassisMaster.ID", MatchType.Exact, objFS.ChassisMaster.ID))
                        'Dim pmHead As PMHeader = New PMHeaderFacade(User).Retrieve(pmCrit)(0)
                        Dim objPMKind As PMKind = New PMKindFacade(User).RetrievePMKind(objFS.MileAge)

                        strText.Append(objFS.ChassisMaster.ChassisNumber & Chr(9) & objFS.ServiceDate.ToString("ddMMyyyy") & Chr(9) & objFS.MileAge & Chr(9) & objDealer.DealerCode & Chr(9) & "PM" & objPMKind.KindCode & Chr(9) & objFS.ReleaseDate.ToString("ddMMyyyy") & Chr(9) & objFS.VisitType.ToString() & Chr(9) & Chr(13) & Chr(10))
                        'strText.Append(vbNewLine)
                        'arlTransferedToSAP.Add(objFS)
                        nData += 1
                    End If
                Next
                If nData > 0 Then
                    If Not Me.SaveToSAP(FSFileNameLocalPM, FSFileNamePM, strText) Then
                        nData = -2
                    End If
                End If
            End If
        Catch ex As Exception
            nData = -1 ' -1 means error occured
        End Try

        Return nData
    End Function

    Private Function AppendText(ByVal ArrCheckedFSItem As ArrayList, ByVal FileNameLocal As String, ByVal filename As String, ByRef arlTransferedToSAP As ArrayList) As Integer ' Number of data sent to SAP
        Dim strText As New StringBuilder
        Dim objAl As New ArrayList
        Dim nData As Integer = 0

        Try
            nData = 0
            If ArrCheckedFSItem.Count > 0 Then
                Dim LMSPExFl As List(Of MSPExMappingtoFSKind) = New MSPExMappingtoFSKindFacade(User).RetrieveActiveList().Cast(Of MSPExMappingtoFSKind).ToList()
                Dim MSPExFl As Integer = 0
                strText = New StringBuilder
                For Each objFS As FreeService In ArrCheckedFSItem
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
                        Dim objChassisMaster As ChassisMaster = New ChassisMaster
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ID", MatchType.Exact, objFS.ChassisMaster.ID))
                        arlistChassis = New ChassisMasterFacade(User).Retrieve(criterias)
                        If arlistChassis.Count > 0 Then
                            objChassisMaster = CType(arlistChassis(0), ChassisMaster)
                            strText.Append(objChassisMaster.ChassisNumber)
                        End If
                        strText.Append(",")

                        Dim arlistFSKind As ArrayList
                        Dim objFSKind As FSKind = New FSKind
                        Dim criteriasFSKind As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasFSKind.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKind), "ID", MatchType.Exact, objFS.FSKind.ID))
                        arlistFSKind = New FSKindFacade(User).Retrieve(criteriasFSKind)
                        If arlistFSKind.Count > 0 Then
                            objFSKind = CType(arlistFSKind(0), FSKind)
                            MSPExFl = LMSPExFl.Where(Function(x) x.FSKind.KindCode = objFSKind.KindCode).Count
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
                        If MSPExFl > 0 Then
                            strText.Append(",")
                            strText.Append(CommonFunction.FSGetMSPRegNumber(objFS))
                        End If
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
#Region "PM"

    Private Function GetCheckedPMItem(ByVal dgEntryPM As ArrayList) As ArrayList
        ' dgEntryPM.DataSource = CType(Session("SessArrPMHeader"), ArrayList)
        Dim arlCheckedPMItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each objPM As PMHeader In dgEntryPM
            Dim _tempObjPMHeader As PMHeader

            If 1 = 1 Then ' If CType(dtgItem.Cells(2).FindControl("cbSelect"), CheckBox).Checked Then
                If Not IsNothing(objPM) Then
                    If objPM.PMStatus = CType(EnumPMStatus.PMStatus.Baru, String) Then
                        Dim _MSPHelper As New MSPHelper
                        'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180201
                        Dim _strMSPStatus As String = ""
                        _strMSPStatus = _MSPHelper.CheckStatusMSP(objPM.ChassisMaster.ChassisNumber, objPM.PMKind.ID, _tempObjPMHeader, objPM.StandKM, objPM.ServiceDate)
                        If _strMSPStatus = String.Empty AndAlso Not IsNothing(_tempObjPMHeader) Then
                            objPM.Remarks = If(Not IsNothing(_tempObjPMHeader), _tempObjPMHeader.Remarks, String.Empty)
                            objPM.MSPRegistrationHistoryID = If(Not IsNothing(_tempObjPMHeader), _tempObjPMHeader.MSPRegistrationHistoryID, 0)
                            objPM.IsValidMSP = _tempObjPMHeader.IsValidMSP
                        Else
                            objPM.IsValidMSP = False
                        End If
                        'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180201

                        arlCheckedPMItem.Add(objPM)
                    End If
                End If
            End If
        Next
        Return arlCheckedPMItem
    End Function


    Private Sub TransferToSAP(ByVal objPMColl As ArrayList, ByVal type As String, Optional ByVal parCategory As String = "")
        Dim NewArl As ArrayList = New ArrayList
        Dim sb As StringBuilder = New StringBuilder
        Dim Product As String = parCategory 'Me.GetProductCategoryCode(objPMColl)
        Dim filename = String.Format("{0}{1}{2}{3}", "statusPM", Date.Now.ToString("ddMMyyyyHHmmss"), "_" & parCategory, ".txt") ' "_" & Product.ToLower()
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder").ToString & "\Service\PM\" & Product & "\" & filename    '-- Destination file to local"
        Dim HistoryFolderSAP As String = String.Empty
        'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122
        If type = "msp" Then
            filename = "XCLMSP" & Date.Now.ToString("ddMMyyyyHHmmss") & "_mmc.txt"
            DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder").ToString & "\Service\MSP\Claim\" & filename
            HistoryFolderSAP = KTB.DNet.Lib.WebConfig.GetValue("SAPServerFolder").ToString & "\Service\MSP\Claim\History"
        End If
        'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122

        For Each item As PMHeader In objPMColl
            item.PMStatus = EnumPMStatus.PMStatus.Proses
            NewArl.Add(item)
            Dim Str As String = String.Empty
            For Each item2 As PMDetail In item.PMDetails
                Str += item2.ReplecementPartMaster.Code & "-"
            Next
            'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122
            If type = "msp" Then
                Dim objMSPClaim As New MSPClaim
                objMSPClaim.Dealer = item.Dealer
                objMSPClaim.ClaimDate = item.ServiceDate
                objMSPClaim.PMHeader = item
                objMSPClaim.Status = EnumStatusMSP.Status.Selesai
                objMSPClaim.MSPRegistrationHistory = New MSPRegistrationHistory(ID:=item.MSPRegistrationHistoryID)

                Dim intRes As Integer = New MSPClaimFacade(User).Insert(objMSPClaim)
                If intRes > 0 Then
                    Dim newObjMSPClaim As MSPClaim = New MSPClaimFacade(User).Retrieve(intRes)
                    sb.Append(item.ChassisMaster.ChassisNumber & Chr(9) & item.ServiceDate.ToString("ddMMyyyy") & Chr(9) & item.StandKM & Chr(9) & item.Dealer.DealerCode & Chr(9) & "PM" & item.PMKind.KindCode & Chr(9) & item.ReleaseDate.ToString("ddMMyyyy") & Chr(9) & item.VisitType.ToString() & Chr(9) & newObjMSPClaim.ClaimNumber & Chr(9) & Chr(13) & Chr(10))
                End If

            Else
                sb.Append(item.ChassisMaster.ChassisNumber & Chr(9) & item.ServiceDate.ToString("ddMMyyyy") & Chr(9) & item.StandKM & Chr(9) & item.Dealer.DealerCode & Chr(9) & "PM" & item.PMKindCode & Chr(9) & item.ReleaseDate.ToString("ddMMyyyy") & Chr(9) & item.VisitType.ToString() & Chr(9) & Chr(13) & Chr(10))
            End If
            'end  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122

        Next

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        success = imp.Start()

        If success Then
            Try
                Dim DestFileInfo As New FileInfo(DestFile)
                If Not DestFileInfo.Directory.Exists Then
                    Directory.CreateDirectory(DestFileInfo.DirectoryName)
                End If

                If HistoryFolderSAP <> String.Empty Then
                    Dim directoryHistory As New DirectoryInfo(HistoryFolderSAP)
                    If Not directoryHistory.Exists Then
                        Directory.CreateDirectory(HistoryFolderSAP)
                    End If
                End If

                Dim objFileStream As New FileStream(DestFile, FileMode.Append, FileAccess.Write)
                Dim objStreamWriter As New StreamWriter(objFileStream)
                objStreamWriter.WriteLine(sb)
                objStreamWriter.Close()
            Catch ex As Exception
                'MessageBox.Show("Gagal kirim file ke SAP.")
            End Try
        Else
            'MessageBox.Show("Gagal akses file ke SAP.")
        End If



    End Sub

#End Region



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

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim messages As String = String.Empty
        lblUploadedFile.Text = ""
        Session("FSEVIDENCE") = Nothing
        If Not validateFile(iFSEvidence.PostedFile, messages) Then
            MessageBox.Show(messages)
            Return
        End If

        Dim sizes As String() = {"B", "KB", "MB", "GB", "TB"}
        Dim len As Double = iFSEvidence.PostedFile.ContentLength
        Dim order As Integer = 0

        While len >= 1024 And order < sizes.Length - 1
            order += 1
            len = len / 1024
        End While


        Dim resultSize As String = String.Format("{0:0.##} {1}", len, sizes(order))
        lblUploadedFile.Text = iFSEvidence.PostedFile.FileName & "(" & resultSize & ")"
        Session("FSEVIDENCE") = iFSEvidence.PostedFile
        MessageBox.Show("Berkas Berhasil diupload")
        Return
    End Sub
#End Region

    Private Sub dgFreeServisEntry_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgFreeServisEntry.SortCommand

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

        dgFreeServisEntry.SelectedIndex = -1
        dgFreeServisEntry.CurrentPageIndex = 0
        bindGridSorting(dgFreeServisEntry.CurrentPageIndex)
    End Sub

    Private Sub btnSimpan_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.ServerClick
        Dim oFreeServiceValidation As FreeServiceValidation = New FreeServiceValidation()
        Dim ObjDealer As Dealer = CType(Session.Item("DEALER"), Dealer)

        Try
            'CentalizeValidateInputFS(string kindCode, string chassisNumber, string engineNumber, string mileage, string stringServiceDate, string stringSoldPKTDate, string visitType)
            Dim listValidResult As List(Of ValidResult) = oFreeServiceValidation.CentalizeValidateInputFS(ddlKind.SelectedValue, txtChassisMaster.Text.Trim, txtEngineNumber.Text.Trim, txtKM.Text.Trim, txtTglServis.Text.Trim, txtTglJual.Text.Trim, ddlVisitType.SelectedValue, txtWONumber.Text.Trim)
            If listValidResult.Count > 0 Then
                Dim erMes As StringBuilder = New StringBuilder
                Dim i As Integer = 1
                For Each item As ValidResult In listValidResult
                    erMes.Append(i & ". " & item.Message.ToString & "\n")
                    i += 1
                Next
                MessageBox.Show(erMes.ToString())
                Return
            End If

            listValidResult = oFreeServiceValidation.ValidateFreeServiceCentralize(buildFreeServiceObject(), ObjDealer.DealerCode)
            If listValidResult.Count > 0 Then
                Dim erMes As StringBuilder = New StringBuilder
                Dim i As Integer = 1
                For Each item As ValidResult In listValidResult
                    erMes.Append(i & ". " & item.Message.ToString & "\n")
                    i += 1
                Next
                MessageBox.Show(erMes.ToString())
                Return
            End If

            btnSave_Click(sender, e)

            'If MSPClaimExist() Then
            '    Exit Sub
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub

    Private Function buildFreeServiceObject() As FreeService
        Dim oFS As FreeService = New FreeService

        Dim oCM As ChassisMaster = New ChassisMasterFacade(User).RetrieveByChassisAndEngine(txtChassisMaster.Text, txtEngineNumber.Text)
        If oCM.ID = 0 Then
            oCM = New ChassisMaster
            oCM.ChassisNumber = txtChassisMaster.Text
            oCM.EngineNumber = txtEngineNumber.Text
            oFS.ChassisMaster = oCM
        End If

        Dim oFKind As FSKind = New FSKindFacade(User).Retrieve(ddlKind.SelectedValue)
        If oFKind.ID = 0 Then
            oFKind = New FSKind
            oFKind.KindCode = ddlKind.SelectedValue
        End If

        oFS.Status = EnumFSStatus.FSStatus.Baru
        oFS.ChassisMaster = oCM
        oFS.FSKind = oFKind
        If txtKM.Text.Trim <> "" Then
            oFS.MileAge = CInt(txtKM.Text)
        End If
        If txtTglServis.Text.Trim <> "" Then
            oFS.ServiceDate = ToDate(txtTglServis.Text)
        End If
        Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strDealerCode))
        Dim DealerColl As ArrayList = New DealerFacade(User).Retrieve(criterias3)
        If DealerColl.Count > 0 Then
            oFS.Dealer = CType(DealerColl(0), Dealer)
        End If
        If (txtDealerBranchCode.Text.Trim() <> String.Empty) Then
            Dim criterias4 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias4.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerBranch), "DealerBranchCode", MatchType.Exact, txtDealerBranchCode.Text))
            Dim DealerBranchColl As ArrayList = New DealerBranchFacade(User).Retrieve(criterias4)

            If DealerBranchColl.Count > 0 Then
                oFS.DealerBranch = CType(DealerBranchColl(0), DealerBranch)
            End If
        End If
        If txtTglJual.Text.Trim <> "" Then
            oFS.SoldDate = ToDate(txtTglJual.Text)
        Else
            oFS.SoldDate = New Date(1900, 1, 1)
        End If
        oFS.VisitType = ddlVisitType.SelectedValue
        oFS.WorkOrderNumber = txtWONumber.Text.Trim()

        Return oFS
    End Function

    Private Sub btnBatal_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.ServerClick
        btnCancel_Click(sender, e)
    End Sub

    Private Sub btnRilis_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRilis.ServerClick
        btnRelease_Click(sender, e)
    End Sub

    Private Function ValidateFSKindOnVehicleType(ByVal objFreeService As FreeService) As Boolean
        Try
            Dim VechileTypeID As Integer = objFreeService.ChassisMaster.VechileColor.VechileType.ID
            Dim FSKindID As Integer = objFreeService.FSKind.ID
            Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, FSKindID))
            critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, VechileTypeID))

            Return New FSKindOnVechileTypeFacade(User).Retrieve(critComp).Count > 0
        Catch
            Return False
        End Try
    End Function

    ''LOC 2014-09-02
    '' By Ali
    Private Function GetSuffix() As String
        Return DateTime.Now.ToString("yyyyMMddHHmmss")
    End Function
    ''End LOC

    Private Function PMHeaderIsExist(ByVal chassisMasterID As Integer, ByVal pmKindID As Integer) As Integer
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(PMHeader), "PMKind.ID", MatchType.Exact, pmKindID))
        criteria.opAnd(New Criteria(GetType(PMHeader), "ChassisMaster.ID", MatchType.Exact, chassisMasterID))
        Dim arrPMHeader As ArrayList = New PMHeaderFacade(User).Retrieve(criteria)
        If arrPMHeader.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    Private Function MSPClaimExist() As Boolean
        Dim kindValue As String = Regex.Replace(ddlKind.SelectedValue, "[^\d]", "")
        If kindValue.Length = 1 Then
            kindValue = "0" & kindValue
        End If
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(MSPClaim), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text.Trim))
        crit.opAnd(New Criteria(GetType(MSPClaim), "PMKind.KindCode", MatchType.Exact, kindValue))
        Dim arlMSP As ArrayList = New MSPClaimFacade(User).Retrieve(crit)
        If arlMSP.Count > 0 Then
            MessageBox.Show("Chassis ini sudah mengajukan MSP sebelumnya, silahkan hubungi MMKSI")
            Return True
        End If
        Return False
    End Function

    Private Function validateFile(ByVal file As HttpPostedFile, ByRef messages As String) As Boolean

        If file Is Nothing OrElse file.FileName = "" Then
            messages = "Silahkan Pilih berkas yang akan diUpload"
            Return False
        End If


        Dim oCfg As New AppConfigFacade(User)
        Dim maxFSize As Integer = oCfg.Retrieve("FreeService.MaxFileSize").Value
        Dim MinSize As Integer = oCfg.Retrieve("MinFileSize").Value
        If file.ContentLength > (maxFSize * 1024) Then

            messages = "Ukuran file melebihi batas maksimal ,1 MB!"
            Return False
        End If

        If MinSize > 0 AndAlso file.ContentLength < (MinSize * 1024) Then

            messages = "Ukuran dokumen terlalu kecil, Ukuran minimum ,50 kb!"
            Return False
        End If

        If Not getSupportedFileType().Contains(Path.GetExtension(file.FileName)) Then

            messages = "Format file tidak didukung!"
            Return False
        End If
        Return True
    End Function

    Private Function getSupportedFileType() As ArrayList
        Dim result As New ArrayList
        Dim oCfg As New AppConfigFacade(User)
        Dim strFType As String() = oCfg.Retrieve("FreeService.SupportedFile").Value.Split(",")
        For Each ft As String In strFType
            result.Add("." + ft.Trim)
        Next

        Return result
    End Function

    Protected Sub lbtnChassisLoad_Click(sender As Object, e As EventArgs) Handles lbtnChassisLoad.Click
        Try
            If txtChassisMaster.Text.Trim <> "" Then
                Dim critCMPKT As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critCMPKT.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMasterPKT), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))

                Dim arlCMPKT As New ArrayList
                arlCMPKT = New ChassisMasterPKTFacade(User).Retrieve(critCMPKT)
                If arlCMPKT.Count > 0 Then
                    txtTglJual.Text = CType(arlCMPKT(0), ChassisMasterPKT).PKTDate.ToString("ddMMyyyy")
                Else
                    Dim oChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(txtChassisMaster.Text)
                    If oChassisMaster.ID > 0 Then
                        If oChassisMaster.EndCustomer.FakturDate.Year = 1753 Then
                            If oChassisMaster.EndCustomer.OpenFakturDate.Year > 1753 Then
                                txtTglJual.Text = oChassisMaster.EndCustomer.OpenFakturDate.ToString("ddMMyyyy")
                            End If
                        Else
                            txtTglJual.Text = oChassisMaster.EndCustomer.FakturDate.ToString("ddMMyyyy")
                        End If
                    End If
                End If
            End If
        Catch
        End Try
    End Sub

End Class