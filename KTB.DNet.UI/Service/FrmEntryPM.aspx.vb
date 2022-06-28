#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports System.Drawing.Color
Imports System.Web.UI.WebControls
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.Text
Imports System.IO
Imports System.Collections.Generic
#End Region
Public Class FrmEntryPM
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents LblTglServis As System.Web.UI.WebControls.Label
    Protected WithEvents txtChassisMaster As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEngineNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlVisitType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtWONo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKM As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTglServis As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnRelease As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnBatal As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtPenggatianPart As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPMKind As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPMKind As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPenggatianPart As System.Web.UI.WebControls.Label
    Protected WithEvents dgEntryPM As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPopUpDealerBranch As Label
    Protected WithEvents txtDealerBranchCode As TextBox
    Protected WithEvents txtBranchName As TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variables"
    Dim _sessHelper As SessionHelper = New SessionHelper
    Private m_bPMUpdate_Privilege As Boolean = False
    Private m_bPMRelease_Privilege As Boolean = False
    Private m_bPMSave_Privilege As Boolean = False
    Private _strMSPStatus As String = String.Empty
    Private _tempObjPMHeader As PMHeader
    Private _objPMKindTemp As PMKind
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.PMDataView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PERIODICAL MAINTENANCE - Data PM")
        End If
    End Sub

    Dim bCekPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.PMDataSave_Privilege)
    Dim bCekGridPriv As Boolean = SecurityProvider.Authorize(Context.User, SR.PMDataEdit_Privilege)

    Private Sub CekBtnPriv()
        btnSimpan.Disabled = Not bCekPriv
        btnBatal.Disabled = Not bCekPriv
        btnBatal.Disabled = Not bCekGridPriv
        If bCekPriv Then
            btnSimpan.Visible = True
            btnBatal.Visible = True
        Else
            btnSimpan.Visible = False
            btnBatal.Visible = False
        End If

        btnRelease.Enabled = SecurityProvider.Authorize(Context.User, SR.PMDataRilis_Privilege)
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'ActivateUserPrivilege()
        InitiateAuthorization()
        CekBtnPriv()
        If Not IsPostBack Then
            If Not IsNothing(Session("DEALER")) Then
                InitiatePage()
                Dim ObjDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
                ''CR Tutup Menu
                '' by ali
                '' 2014 - 09 -30

                If (DateTime.Now >= New DateTime(2014, 10, 1) AndAlso DateTime.Now <= New DateTime(2014, 10, 11).AddMinutes(-1) AndAlso ObjDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                    Dim MSgClose As String = IIf(Not IsNothing(KTB.DNet.Lib.WebConfig.GetValue("CloseMessage")), KTB.DNet.Lib.WebConfig.GetValue("CloseMessage"), "Module ini sedang di tutup, sampai dengan 10 Oktober 2014")
                    Server.Transfer("../ClossingMessage.htm")
                End If
                ''END CR Tutup Menu
                _sessHelper.SetSession("sessDealer", ObjDealer)
                lblDealerCode.Text = ObjDealer.DealerCode + " / " + ObjDealer.SearchTerm1
                lblDealerName.Text = ObjDealer.DealerName
                _sessHelper.SetSession("sessDealerLogin", ObjDealer.DealerCode)

                ViewState.Add("vsProcess", "Insert")

                BindDatagrid(0)
                BindDDLPMKind()
            Else
                'Response.Redirect("../SessionExpired.htm")
            End If
            lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
            txtDealerBranchCode.Attributes.Add("ReadOnly", "ReadOnly")
            txtBranchName.Attributes.Add("ReadOnly", "ReadOnly")
        End If
        lblPenggatianPart.Attributes("onclick") = "ShowReplacementPart();"
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.FreeServiceDataView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Periodical Inspection - Maintenance Entry ")
        End If

        'FreeServiceDataUpdate_Privilege
        m_bPMUpdate_Privilege = SecurityProvider.Authorize(Context.User, SR.FreeServiceDataUpdate_Privilege)

        'FreeServiceDataSave_Privilege
        m_bPMSave_Privilege = SecurityProvider.Authorize(Context.User, SR.FreeServiceDataSave_Privilege)

        If SecurityProvider.Authorize(Context.User, SR.FreeServiceDataSave_Privilege) Or m_bPMUpdate_Privilege Then
            btnSimpan.Visible = True
            btnBatal.Visible = True
        Else
            btnSimpan.Visible = False
            btnBatal.Visible = False
        End If

        'FreeServiceDataRelease_Privilege
        m_bPMRelease_Privilege = SecurityProvider.Authorize(Context.User, SR.FreeServiceDataRelease_Privilege)
    End Sub

    Private Sub InitiatePage()
        'SetControlPrivilege()
        ViewState("currentSortColumn") = "ID"
        ViewState("currentSortDirection") = Sort.SortDirection.ASC
        txtChassisMaster.Attributes.Add("onkeydown", "enter(document.all.txtEngineNo)")
        txtEngineNo.Attributes.Add("onkeydown", "enter(document.all.txtWONo)")
        txtWONo.Attributes.Add("onkeydown", "enter(document.all.txtKM)")
        txtKM.Attributes.Add("onkeydown", "enter(document.all.txtTglServis)")
        txtTglServis.Attributes.Add("onkeydown", "enter(document.all.btnSimpan)")
        btnSimpan.Attributes.Add("onkeydown", "enter(document.all.txtChassisMaster)")
    End Sub
    Private Sub TransferToSAP(ByVal objPMColl As ArrayList, ByVal type As String, Optional ByVal parCategory As String = "")
        Dim NewArl As ArrayList = New ArrayList
        Dim sb As StringBuilder = New StringBuilder
        Dim Product As String = Me.GetProductCategoryCode(objPMColl)
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
                objMSPClaim.Status = EnumStatusMSP.Status.Proses
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

            'If (Str <> String.Empty) Then
            '    sb.Append(item.ChassisMaster.ChassisNumber & Chr(9) & item.ServiceDate.ToString("ddMMyyyy") & Chr(9) & item.StandKM & Chr(9) & item.Dealer.DealerCode & Chr(9) & "PM" & item.PMKindCode & Chr(9) & item.ReleaseDate.ToString("ddMMyyyy") & Chr(9) & item.VisitType.ToString() & Chr(9) & Chr(13) & Chr(10))
            '        'sb.Append(item.ChassisMaster.ChassisNumber & Chr(9) & item.ServiceDate.ToString("dd/MM/yyyy") & Chr(9) & item.StandKM & Chr(9) & item.ReleaseDate.ToString("dd/MM/yyyy") & Chr(9) & Str.Remove(Str.Length - 1, 1) & Chr(13) & Chr(10))
            'Else
            '    sb.Append(item.ChassisMaster.ChassisNumber & Chr(9) & item.ServiceDate.ToString("ddMMyyyy") & Chr(9) & item.StandKM & Chr(9) & item.Dealer.DealerCode & Chr(9) & "PM" & item.PMKindCode & Chr(9) & item.ReleaseDate.ToString("ddMMyyyy") & Chr(9) & item.VisitType.ToString() & Chr(9) & Chr(13) & Chr(10))
            '    ' sb.Append(item.ChassisMaster.ChassisNumber & Chr(9) & item.ServiceDate.ToString("dd/MM/yyyy") & Chr(9) & item.StandKM & Chr(9) & item.ReleaseDate.ToString("dd/MM/yyyy") & Chr(9) & Chr(13) & Chr(10))
            'End If

            'If (Str <> String.Empty) Then
            '    sb.Append(item.ChassisMaster.ChassisNumber & ";" & item.ServiceDate.ToString("dd/MM/yyyy") & ";" & item.StandKM & ";" & item.ReleaseDate.ToString("dd/MM/yyyy") & ";" & Str.Remove(Str.Length - 1, 1) & Chr(13) & Chr(10))
            'Else
            '    sb.Append(item.ChassisMaster.ChassisNumber & ";" & item.ServiceDate.ToString("dd/MM/yyyy") & ";" & item.StandKM & ";" & item.ReleaseDate.ToString("dd/MM/yyyy") & ";" & Chr(13) & Chr(10))
            'End If
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
                imp.StopImpersonate()
                imp = Nothing
            Catch ex As Exception
                MessageBox.Show("Gagal kirim file ke SAP.")
            End Try
        Else
            MessageBox.Show("Gagal akses file ke SAP.")
        End If

        BindDatagrid(0)

    End Sub

    Private Function GetProductCategoryCode(ByVal aPMIs As ArrayList) As String
        Dim product As String = ""

        For Each oPMI As PMHeader In aPMIs
            If product = "" Then
                product = oPMI.ChassisMaster.Category.ProductCategory.Code
            Else
                If product <> oPMI.ChassisMaster.Category.ProductCategory.Code Then
                    Return ""
                End If
            End If
        Next
        Return product
    End Function

    Private Sub btnRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelease.Click
        Dim bcheck As Boolean = False
        Dim success As Boolean = False

        dgEntryPM.DataSource = CType(Session("SessArrPMHeader"), ArrayList)
        For Each dtgItem As DataGridItem In dgEntryPM.Items
            If CType(dtgItem.FindControl("cbSelect"), CheckBox).Checked Then ' If CType(dtgItem.Cells(0).FindControl("cbSelect"), CheckBox).Checked Then
                bcheck = True
                Exit For
            End If
        Next
        Try
            If bcheck Then
                Dim CheckedPMItemColl As ArrayList = New ArrayList
                CheckedPMItemColl = GetCheckedPMItem()

                'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180201
                ' _strMSPStatus pada saat release di isi pada fungsi GetCheckedPMItem()
                'If _strMSPStatus <> String.Empty And _strMSPStatus <> "PM" Then
                '    MessageBox.Show(_strMSPStatus)
                '    Exit Sub
                'End If
                'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180201

                ''Commented Out by ali
                ''Comented Date : 2014-09-02
                'If Me.GetProductCategoryCode(CheckedPMItemColl) = "" Then
                '    MessageBox.Show("Produk yang dirilis harus sama")
                '    Exit Sub
                'End If
                ''End CommentedOut

                Dim objPMColl As ArrayList = New ArrayList
                Dim aPMA As New ArrayList
                Dim aPMB As New ArrayList
                Dim arrMSP As New ArrayList

                If CheckedPMItemColl.Count > 0 Then
                    For Each ObjPMHeader As PMHeader In CheckedPMItemColl
                        ObjPMHeader.ReleaseDate = Today
                        ObjPMHeader.PMStatus = EnumPMStatus.PMStatus.Proses
                        objPMColl.Add(ObjPMHeader)
                        If (ObjPMHeader.ChassisMaster.Category.ProductCategory.Code.ToLower() = "mmc") Then
                            'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122
                            ' memisahkan PM MSP atau normal PM
                            If ObjPMHeader.Remarks <> String.Empty And ObjPMHeader.IsValidMSP = True Then
                                arrMSP.Add(ObjPMHeader)
                            Else
                                aPMA.Add(ObjPMHeader)
                            End If
                            'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180122
                        Else
                            aPMB.Add(ObjPMHeader)
                        End If
                        'CR GSR
                        'GSRRilisPM(ObjPMHeader.ID)
                        Dim nResults As Integer = InsertModel(ObjPMHeader)

                    Next
                    Dim nResult = New PMHeaderFacade(User).UpdatePMHeaderCollection(objPMColl)
                    If nResult = 0 Then
                        ''Commented Out by ali
                        ''Comented Date : 2014-09-02
                        'TransferToSAP(objPMColl)
                        ''End CommentedOut

                        ''LOC 2014-09-02
                        ''BY ALi 
                        ''Splitting
                        If (aPMA.Count > 0) Then
                            TransferToSAP(aPMA, "Normal", "mmc")
                        End If
                        If (aPMB.Count > 0) Then
                            TransferToSAP(aPMB, "Normal", "mftbc")
                        End If
                        ''ENd 2014-09-02

                        'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20171206
                        'If (arrMSP.Count > 0) Then
                        '    TransferToSAP(arrMSP, "msp", "mmc")
                        'End If
                        'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20171206

                        MessageBox.Show("Update Rilis Sukses")
                    Else
                        MessageBox.Show("Update Rilis gagal")
                    End If
                End If
            Else
                MessageBox.Show("Record PM Header belum dipilih !")
            End If
            BindDatagrid(dgEntryPM.CurrentPageIndex)
            Dim strScript As String
            strScript = "<script>document.all.txtChassisMaster.focus();</script>"
            Page.RegisterStartupScript("", strScript)
        Catch ex As Exception
            MessageBox.Show("Gagal Kirim")
        End Try
    End Sub
    Private Function InsertModel(ByVal oPM As PMHeader) As Integer
        Dim objGSRStaging As GSRStaging = New GSRStaging


        Dim nResult As Integer
        objGSRStaging.RilisID = oPM.ID
        objGSRStaging.Tipe = "PM"
        objGSRStaging.Remark = "Ready To Process"
        nResult = New GSRStagingFacade(User).Insert(objGSRStaging)
        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
        End If
        Return nResult
    End Function
    Private Sub GSRRilisPM(ByVal ID As Integer)
        Dim objServiceReminder As ServiceReminder = New ServiceReminder
        'objService = New ServiceStandardTimeFacade(User).Calculate(txtKodeDealer.Text.Trim(), "", ddlJenisKegiatan2.SelectedValue, ICPeriodFrom.Value)
        Dim RESULT As Integer = 0
        RESULT = New ServiceReminderFacade(User).GSRRilisPM(ID)

    End Sub

    Private Function IsChassisAndPMDateEquals() As Boolean
        '---cek chasiss dgn tanggal PM
        Dim checkRule1 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim checkRuleChassis_Date As CriteriaComposite = checkRule1
        checkRuleChassis_Date.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
        checkRuleChassis_Date.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ServiceDate", MatchType.Exact, ToDate(txtTglServis.Text.Trim)))
        Dim arlCek As ArrayList = New PMHeaderFacade(User).Retrieve(checkRuleChassis_Date)
        If arlCek.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function IsChassisAndKMEquals(ByVal StandKM As Integer, ByVal PMHeaderID As Integer) As Boolean
        '---cek Chassis yang sama dengan KM yang sama
        'Dim StrFunction As String = String.Format("(SELECT ID FROM dbo.fn_PmHeaderChecking('{0}','{1}',{2}))", txtChassisMaster.Text.Trim(), parPmKindCode, parID.ToString())
        Dim checkRuleChassis_No As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If PMHeaderID <> 0 Then
            checkRuleChassis_No.opAnd(New Criteria(GetType(KTB.DNET.Domain.PMHeader), "ID", MatchType.No, PMHeaderID))
        End If

        checkRuleChassis_No.opAnd(New Criteria(GetType(KTB.DNET.Domain.PMHeader), "StandKM", MatchType.GreaterOrEqual, StandKM))
        checkRuleChassis_No.opAnd(New Criteria(GetType(KTB.DNET.Domain.PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
        'checkRuleChassis_No.opAnd(New Criteria(GetType(KTB.DNet.Domain.PMHeader), "ID", MatchType.InSet, StrFunction))
        Dim arlRuleChassis As ArrayList = New PMHeaderFacade(User).Retrieve(checkRuleChassis_No)
        If arlRuleChassis.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsPMKindAlreadExist(ByVal parPmKindCode As String, Optional ByVal parID As Integer = 0) As Boolean
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

        'Dim objPMHeader As PMHeader = arlRuleChassis(0)
        'If txtChassisMaster.Text.Trim = objPMHeader.ChassisMaster.ChassisNumber And CInt(txtKM.Text) = objPMHeader.StandKM Then
        '   Return True
        'Else
        '   Return False
        'End If
    End Function

    Private Function IsKMLessThanPMMaster() As Boolean
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlCek As ArrayList = New PMKindFacade(User).RetrieveList(crits, "KM", Sort.SortDirection.ASC)
        Dim objPMKind As PMKind = arlCek(0)
        If txtKM.Text < objPMKind.KM Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function getRangePMKInd(ByVal desc As String) As String
        Dim _temp As String() = desc.Split(" ")
        Dim range As String = String.Empty
        For Each item As String In _temp
            If IsNumeric(item) Then
                range += item + "-"
            End If
        Next
        If range.Length > 0 Then
            range = range.Substring(0, range.Length - 1)
        End If
        Return range
    End Function

    'Private Function CheckStatusMSP(ByVal chassisNumber As String, ByVal PMKindID As Integer, ByRef objPMheader As PMHeader, ByVal inputKM As Integer, ByVal serviceDate As DateTime) As String
    '    ' untuk cek status MSP based on chassisnumber dan PMKindID yang terregister sebagai MSP
    '    Dim str As String = String.Empty
    '    Dim dtSet As DataSet = New PMHeaderFacade(User).RetrieveSp("EXEC sp_MSP_GetMSPStatus " & chassisNumber & "," & PMKindID)
    '    If dtSet.Tables.Count > 0 Then
    '        Dim dtTbl As DataTable = dtSet.Tables(0)
    '        Dim strMSPStatus As String = String.Empty
    '        If dtTbl.Rows.Count > 0 Then
    '            Dim _objMSPRegHistory As MSPRegistrationHistory = New MSPRegistrationHistoryFacade(User).Retrieve(CInt(dtTbl.Rows(0)("MSPRegHistoryID")))
    '            If dtTbl.Rows(0)("MSPStatus") = "Need Payment" Then
    '                Dim objPMKIndVal As PMKind = New PMKindFacade(User).Retrieve(PMKindID)
    '                Dim crtMspPM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPDurationPMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                crtMspPM.opAnd(New Criteria(GetType(MSPDurationPMKind), "Duration", MatchType.Exact, _objMSPRegHistory.MSPMaster.Duration))
    '                crtMspPM.opAnd(New Criteria(GetType(MSPDurationPMKind), "PMKindCode", MatchType.Exact, objPMKIndVal.KindCode))
    '                Dim arrMspPm As ArrayList = New MSPDurationPMKindFacade(User).Retrieve(crtMspPM)
    '                If arrMspPm.Count > 0 Then
    '                    str = "No Rangka " & chassisNumber & " belum melakukan pembayaran MSP."
    '                Else
    '                    str = "PM"
    '                End If
    '            Else
    '                If dtTbl.Rows(0)("MSPStatus") <> "MSP EXPIRED" Then
    '                    ' validasi jika masih ada upgrade registrasi dengan status belum selesai
    '                    If Not IsNothing(_objMSPRegHistory) Then
    '                        For Each item As MSPRegistrationHistory In _objMSPRegHistory.MSPRegistration.MSPRegistrationHistorys
    '                            If (item.Status <> EnumStatusMSP.Status.Selesai And item.RequestType = EnumStatusMSP.StatusType.Upgrade) Then
    '                                str = "Status Upgade belum selesai pada registrasi MSP dengan No Rangka " & chassisNumber & "."
    '                            End If
    '                        Next

    '                        If str = String.Empty Then
    '                            Dim regDate As DateTime = CDate(dtTbl.Rows(0)("RegistrationDate"))
    '                            If serviceDate < regDate Then
    '                                str = "Tanggal Service harus lebih dari tanggal pendaftaran MSP(" & CDate(dtTbl.Rows(0)("RegistrationDate")).ToString("yyyy/MM/dd") & ")."
    '                            Else
    '                                ' validasi PMKind input tidak boleh lebih dari MSP PMKind
    '                                ' get last PMKind from history Claim by MSP Registration History
    '                                'Dim objTempPMHeader As PMHeader
    '                                'Dim objTempDurationPMKind As MSPDurationPMKind

    '                                'Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                                'crt.opAnd(New Criteria(GetType(MSPClaim), "MSPRegistrationHistory.ID", MatchType.Exact, CInt(dtTbl.Rows(0)("MSPRegHistoryID"))))
    '                                'Dim sortCols As SortCollection = New SortCollection
    '                                'sortCols.Add(New Sort(GetType(MSPClaim), "ID", Sort.SortDirection.DESC))
    '                                'Dim arr As ArrayList = New MSPClaimFacade(User).Retrieve(crt, sortCols)
    '                                'If arr.Count > 0 Then
    '                                '    objTempPMHeader = CType(arr(0), MSPClaim).PMHeader
    '                                'End If

    '                                '' get MSPDurationPMKind 
    '                                'crt = New CriteriaComposite(New Criteria(GetType(MSPDurationPMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                                'crt.opAnd(New Criteria(GetType(MSPDurationPMKind), "Duration", MatchType.Exact, _objMSPRegHistory.MSPMaster.Duration))
    '                                'If Not IsNothing(objTempPMHeader) Then
    '                                '    crt.opAnd(New Criteria(GetType(MSPDurationPMKind), "PMKindCode", MatchType.Greater, objTempPMHeader.PMKindCode))
    '                                'End If
    '                                'sortCols = New SortCollection
    '                                'sortCols.Add(New Sort(GetType(MSPDurationPMKind), "PMKindCode", Sort.SortDirection.ASC))
    '                                'arr = New MSPDurationPMKindFacade(User).Retrieve(crt, sortCols)
    '                                'If arr.Count > 0 Then
    '                                '    objTempDurationPMKind = CType(arr(0), MSPDurationPMKind)
    '                                '    Dim objPMKind As PMKind = New PMKindFacade(User).Retrieve(objTempDurationPMKind.PMKindCode)

    '                                '    If inputKM > objPMKind.KM Then
    '                                '        str = "KM yang diinput tidak boleh lebih dari " & objPMKind.KM
    '                                '    End If
    '                                'End If
    '                            End If
    '                        End If
    '                    End If
    '                End If

    '                If str = String.Empty And dtTbl.Rows(0)("MSPStatus") <> "PM" Then
    '                    objPMheader = New PMHeader
    '                    objPMheader.Remarks = dtTbl.Rows(0)("MSPStatus")
    '                    objPMheader.MSPRegistrationHistoryID = CInt(dtTbl.Rows(0)("MSPRegHistoryID"))
    '                    If dtTbl.Rows(0)("MSPStatus") = "MSP EXPIRED" Then
    '                        objPMheader.IsValidMSP = False
    '                    End If
    '                End If
    '            End If
    '        Else
    '            str = "PM"
    '        End If
    '    Else
    '        str = "PM"
    '    End If
    '    Return str
    'End Function

    Private Sub btnSimpan_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.ServerClick
        Dim objPMHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
        Dim objPMKind As PMKindFacade = New PMKindFacade(User)
        Dim objPMHeader As PMHeader = New PMHeader

        If objPMKind.IsPMKindFound(CInt(txtKM.Text.Trim)) = False Then
            MessageBox.Show("Range jarak tempuh tidak valid")
            Return
        Else
            'Dim _objPMKind As PMKind = New PMKindFacade(User).RetrievePMKind(txtKM.Text)
            'If ddlPMKind.Visible Then
            If (ddlPMKind.SelectedIndex = 0) Then
                MessageBox.Show("Jenis PM harus dipilih.")
                Return
            End If
            Dim _objPMKind As PMKind = New PMKindFacade(User).Retrieve(CInt(ddlPMKind.SelectedValue))
            If CInt(txtKM.Text) > _objPMKind.KM Then
                MessageBox.Show("Jarak Tempuh melebihi batas jenis PM.")
                Return
            End If
            'End If
            _objPMKindTemp = _objPMKind
            'Validate MSP
            'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180201
            Dim _MSPHelper As New MSPHelper
            _strMSPStatus = _MSPHelper.CheckStatusMSP(txtChassisMaster.Text, _objPMKind.ID, _tempObjPMHeader, If(String.IsNullorEmpty(txtKM.Text), 0, CInt(txtKM.Text)), ToDate(txtTglServis.Text.Trim))
            'If _strMSPStatus <> String.Empty And _strMSPStatus <> "PM" Then
            '    MessageBox.Show(_strMSPStatus)
            '    Exit Sub
            'End If
            'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180201

        End If
        Try
            'If ToDate(txtTglServis.Text.Trim).ToShortDateString > Date.Today.ToShortDateString Then
            If ToDate(txtTglServis.Text.Trim) > Date.Today Then
                MessageBox.Show("Tanggal PM Service tidak boleh lebih besar dari tanggal sekarang")
                Return
            End If
        Catch ex As Exception
            MessageBox.Show("Format tanggal tidak sesuai")
            Return
        End Try

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim strChassisNumber = txtChassisMaster.Text.Trim()
        Dim strEngineNo = txtEngineNo.Text.Trim()
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, strChassisNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, strEngineNo))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
        Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
        If ChassisColl.Count > 0 Then
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                'If m_bFreeServiceDataSave_Privilege Then
                '---cek dulu rule-nya

                InsertobjPMHeader()
                dgEntryPM.SelectedIndex = -1
                'Else
                'MessageBox.Show("Anda tidak punya hak untuk menginsert data baru !")
                'End If
            Else
                'If m_bFreeServiceDataUpdate_Privilege Then
                Dim objUpdatePMHeader As PMHeader = CType(Session.Item("vsPMHeader"), PMHeader)
                UpdateObjPMHeader(objUpdatePMHeader)
                dgEntryPM.SelectedIndex = -1
                ' Else
                'MessageBox.Show("Anda tidak punya hak untuk mengupdate data lama !")
            End If
        Else
            MessageBox.Show("Chassis tidak terdaftar di " + companyCode + " atau chassis tidak sesuai dengan no mesin")
        End If

        'End If
        BindDatagrid(dgEntryPM.CurrentPageIndex)
        Dim strScript As String
        strScript = "<script>document.all.txtChassisMaster.focus();</script>"
        Page.RegisterStartupScript("", strScript)
    End Sub

    Private Sub InsertobjPMHeader()
        Dim objPMHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
        Dim nResult As Integer = -1
        Dim objPMDealer As New Dealer
        Dim objPMDealerBranch As New DealerBranch
        Dim objPMHeader As PMHeader = New PMHeader

        Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
        objPMDealer = New DealerFacade(User).Retrieve(strDealerCode)

        If Not String.IsNullOrEmpty(txtDealerBranchCode.Text.Trim) Then
            objPMDealerBranch = New DealerBranchFacade(User).Retrieve(txtDealerBranchCode.Text.Trim)
            objPMHeader.DealerBranch = objPMDealerBranch
        End If

        objPMHeader.Dealer = objPMDealer

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, txtEngineNo.Text))
        Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)

        If ChassisColl.Count > 0 Then
            objPMHeader.ChassisMaster = ChassisColl(0)
            objPMHeader.WorkOrderNumber = txtWONo.Text
            objPMHeader.StandKM = Integer.Parse(txtKM.Text)
            objPMHeader.ServiceDate = ToDate(txtTglServis.Text.Trim)
            objPMHeader.PMStatus = EnumPMStatus.PMStatus.Baru
            objPMHeader.EntryType = "Single"
            objPMHeader.VisitType = ddlVisitType.SelectedValue
            objPMHeader.Remarks = "" 'If(Not IsNothing(_tempObjPMHeader), _tempObjPMHeader.Remarks, String.Empty)
            objPMHeader.PMKind = _objPMKindTemp

            Dim objKindCode As String = objPMHeader.PMKindCode
            Dim _arrPMDetails As New ArrayList
            'Dim _temp() As String = txtPenggatianPart.Text.Trim.Split(";")
            'Dim sBuilder As String = String.Empty
            'Dim objRepPartMaster As ReplecementPartMaster
            'For Each _item As String In _temp
            '    Dim _objPMDetails As PMDetail = New PMDetail
            '    objRepPartMaster = New ReplecementPartMasterFacade(User).Retrieve(_item)
            '    If Not objRepPartMaster Is Nothing Then
            '        If objRepPartMaster.ID > 0 Then
            '            _objPMDetails.ReplecementPartMaster = objRepPartMaster
            '            objPMHeader.PMDetails.Add(_objPMDetails)
            '        Else
            '            sBuilder += _item & "-"
            '        End If
            '    Else
            '        sBuilder += _item & "-"
            '    End If

            'Next
            'sBuilder = sBuilder.Trim("-")
            'If sBuilder.Length > 0 Then
            '    MessageBox.Show("Part : " & sBuilder & " tidak valid")
            '    Return
            'End If
            If Not IsExistCodeForInsert(objPMHeader.ChassisMaster.ID, objPMHeader.Dealer.ID, objPMHeader.ServiceDate) Then
                'mod by ery
                'Jarak tempuh yang lebih kecil dari semua master Jenis PM tidak boleh 
                'If IsKMLessThanPMMaster() = False Then
                '    MessageBox.Show("Jarak tempuh lebih kecil dari Master Jenis PM")
                '    Exit Sub
                'End If

                'Chassis yang sama dengan KM yang sama tidak boleh 
                If IsChassisAndKMEquals(objPMHeader.StandKM, objPMHeader.ID) = True Then
                    MessageBox.Show("KM kurang dari atau sama dengan data yang pernah disimpan")
                    'MessageBox.Show("Data PM chassis sudah terdaftar")
                    Exit Sub
                End If

                If IsPMKindAlreadExist(objKindCode) = True Then
                    MessageBox.Show("PM Kind sama dengan data yang pernah disimpan")
                    'MessageBox.Show("Data PM chassis sudah terdaftar")
                    Exit Sub
                End If

                'Chassis yang sana dengan Tanggal PM yang sama tidak boleh 
                If IsChassisAndPMDateEquals() = True Then
                    MessageBox.Show("Nomor Rangka dan Tanggal PM sudah ada")
                    Exit Sub
                End If

                nResult = objPMHeaderFacade.Insert(objPMHeader)
                If nResult = -1 Then
                    MessageBox.Show("Simpan Gagal")
                Else
                    MessageBox.Show("Simpan Sukses")
                    'Todo session
                    Session.Add("vsPMHeader", objPMHeader)
                    ClearDataAfterSaving()
                    Dim strScript As String
                    strScript = "<script>document.all.txtChassisMaster.focus();</script>"
                    Page.RegisterStartupScript("", strScript)
                End If
            Else
                MessageBox.Show("Nomor rangka dengan dealer pada tanggal service yang dientry sudah ada")
                Return
            End If
        End If
    End Sub

    Private Sub UpdateObjPMHeader(ByVal objUpdatePMHeader As PMHeader)
        Dim objPMHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
        Dim nResult As Integer = -1
        Dim bCheck As Boolean = True
        Dim ObjChassisMasterCheck As ChassisMaster
        Dim objPMDealerBranch As New DealerBranch
        Dim objPMDealer As Dealer

        Dim strDealerCode As String = CType(_sessHelper.GetSession("sessDealerLogin"), String)
        objPMDealer = New DealerFacade(User).Retrieve(strDealerCode)
        objUpdatePMHeader.Dealer = objPMDealer

        If Not String.IsNullOrEmpty(txtDealerBranchCode.Text.Trim) Then
            objPMDealerBranch = New DealerBranchFacade(User).Retrieve(txtDealerBranchCode.Text.Trim)
            objUpdatePMHeader.DealerBranch = objPMDealerBranch
        End If

        Dim _OLDpart As New ArrayList
        For Each _ItemPMDetail As PMDetail In objUpdatePMHeader.PMDetails
            _OLDpart.Add(_ItemPMDetail)
        Next

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, txtChassisMaster.Text))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EngineNumber", MatchType.Exact, txtEngineNo.Text))
        Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)

        If ChassisColl.Count > 0 Then
            objUpdatePMHeader.ChassisMaster = ChassisColl(0)
            objUpdatePMHeader.VisitType = ddlVisitType.SelectedValue
            objUpdatePMHeader.WorkOrderNumber = txtWONo.Text
            objUpdatePMHeader.StandKM = Integer.Parse(txtKM.Text)
            objUpdatePMHeader.ServiceDate = ToDate(txtTglServis.Text.Trim)
            objUpdatePMHeader.PMStatus = EnumPMStatus.PMStatus.Baru
            objUpdatePMHeader.Remarks = If(Not IsNothing(_tempObjPMHeader), _tempObjPMHeader.Remarks, String.Empty)
            objUpdatePMHeader.PMKind = _objPMKindTemp

            Dim _NEWpart As New ArrayList
            'Dim _temp() As String = txtPenggatianPart.Text.Trim.Split(";")
            'Dim objRepPartMaster As ReplecementPartMaster

            'Dim sBuilder As String = String.Empty
            'For Each _item As String In _temp
            '    objRepPartMaster = New ReplecementPartMasterFacade(User).Retrieve(_item)
            '    If Not objRepPartMaster Is Nothing Then
            '        If objRepPartMaster.ID > 0 Then
            '            _NEWpart.Add(objRepPartMaster)
            '        Else
            '            sBuilder += _item & "-"
            '        End If
            '    Else
            '        sBuilder += _item & "-"
            '    End If

            'Next
            'sBuilder = sBuilder.Trim("-")
            'If sBuilder.Length > 0 Then
            '    MessageBox.Show("Part : " & sBuilder & " tidak valid")
            '    Return
            'End If

            If Not IsExistCodeForUpdate(objUpdatePMHeader.ID, objUpdatePMHeader.ChassisMaster.ID, objUpdatePMHeader.Dealer.ID, objUpdatePMHeader.ServiceDate) Then
                'Chassis yang sama dengan KM yang sama tidak boleh 
                If IsChassisAndKMEquals(objUpdatePMHeader.StandKM, objUpdatePMHeader.ID) = True Then
                    MessageBox.Show("KM kurang dari atau sama dengan data yang pernah disimpan")
                    'MessageBox.Show("Data PM chassis sudah terdaftar")
                    Exit Sub
                End If

                nResult = objPMHeaderFacade.UpdateTransaction(objUpdatePMHeader, _OLDpart, _NEWpart)
                If nResult = -1 Then
                    MessageBox.Show("Update Gagal")
                Else
                    MessageBox.Show("Update Sukses")
                    ClearData()
                    Dim strScript As String
                    strScript = "<script>document.all.txtChassisMaster.focus();</script>"
                    Page.RegisterStartupScript("", strScript)
                End If
            Else
                MessageBox.Show("Nomor rangka dengan dealer pada tanggal service yang dientry sudah ada")
                Return
            End If
        End If
    End Sub

    Private Sub DeletePMHeader(ByVal ID As Integer)
        Dim objPMHeaderFacade As New PMHeaderFacade(User)
        Dim objPMHeader As PMHeader = objPMHeaderFacade.Retrieve(ID)
        objPMHeaderFacade.Delete(objPMHeader)
        MessageBox.Show("Item PM Header sudah dihapus")
        BindDatagrid(dgEntryPM.CurrentPageIndex)
        ClearDataAfterSaving()
    End Sub

    Private Sub BindDatagrid(ByVal indexPage)
        Dim totRow As Integer = 0
        If Not IsNothing(CType(Session.Item("sessDealer"), Dealer)) Then
            Dim TmpObjDealer As Dealer = CType(Session.Item("sessDealer"), Dealer)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PMHeader), "PMStatus", MatchType.Exact, CType(EnumPMStatus.PMStatus.Baru, String)))
            criterias.opAnd(New Criteria(GetType(PMHeader), "Dealer.ID", MatchType.Exact, CType(TmpObjDealer.ID, Integer)))

            _sessHelper.SetSession("SortViewPM", criterias)
            dgEntryPM.DataSource = New PMHeaderFacade(User).RetrieveActiveList(criterias, indexPage + 1, dgEntryPM.PageSize, totRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgEntryPM.VirtualItemCount = totRow

            Dim al As ArrayList = dgEntryPM.DataSource
            _sessHelper.SetSession("SessArrPMHeader", dgEntryPM.DataSource)
            If al.Count > 0 Then
                btnRelease.Enabled = SecurityProvider.Authorize(Context.User, SR.PMDataRilis_Privilege)
            Else
                btnRelease.Enabled = False

            End If
            dgEntryPM.DataBind()
        End If
    End Sub

    Private Sub bindGridSorting(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dgEntryPM.DataSource = New PMHeaderFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("sortViewPM"), CriteriaComposite), indexPage + 1, dgEntryPM.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
            dgEntryPM.VirtualItemCount = totalRow
            dgEntryPM.DataBind()
        End If

    End Sub

    Private Function IsValidDate(ByVal strdate As String) As Boolean
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
        Try
            Return CType(strdate.Substring(0, 2).ToString & "-" & strdate.Substring(2, 2) & "-" & strdate.Substring(4, 4), Date)
            'Return CType(strdate.Substring(0, 2).ToString & "/" & strdate.Substring(2, 2) & "/" & strdate.Substring(4, 4), Date)
        Catch ex As Exception
            Throw New Exception("Format Tanggal tidak valid")
        End Try
    End Function

    Private Sub ActivateControlEdit(ByVal ID As Integer)
        Dim objPMHeader As PMHeader = New PMHeaderFacade(User).Retrieve(ID)
        txtChassisMaster.Text = objPMHeader.ChassisMaster.ChassisNumber
        txtEngineNo.Text = objPMHeader.ChassisMaster.EngineNumber
        If (Not IsNothing(objPMHeader.DealerBranch)) Then
            txtDealerBranchCode.Text = objPMHeader.DealerBranch.DealerBranchCode
            txtBranchName.Text = objPMHeader.DealerBranch.Name
        Else
            txtDealerBranchCode.Text = String.Empty
            txtBranchName.Text = String.Empty
        End If
        txtWONo.Text = objPMHeader.WorkOrderNumber
        txtKM.Text = objPMHeader.StandKM.ToString
        txtTglServis.Text = Format(objPMHeader.ServiceDate, "ddMMyyyy")
        ddlVisitType.SelectedValue = objPMHeader.VisitType
        Dim _temp As String = String.Empty

        If objPMHeader.PMDetails.Count > 0 Then
            For Each item As PMDetail In objPMHeader.PMDetails
                _temp = _temp + item.ReplecementPartMaster.Code + ";"
            Next
            txtPenggatianPart.Text = _temp.Substring(0, _temp.Length - 1)
        End If

        txtEngineNo_TextChanged(Me, EventArgs.Empty)
        If Not IsNothing(ddlPMKind) Then
            ddlPMKind.SelectedValue = objPMHeader.PMKind.ID
            ddlPMKind.Enabled = False
        End If
        'Todo session
        Session.Add("vsPMHeader", objPMHeader)

    End Sub

    Private Sub ClearDataAfterSaving()
        txtChassisMaster.Text = String.Empty
        txtEngineNo.Text = String.Empty
        txtDealerBranchCode.Text = String.Empty
        txtWONo.Text = String.Empty
        txtKM.Text = String.Empty

        txtTglServis.Text = String.Empty
        txtChassisMaster.ReadOnly = False
        txtEngineNo.ReadOnly = False
        txtWONo.ReadOnly = False
        txtKM.ReadOnly = False
        txtTglServis.ReadOnly = False
        txtPenggatianPart.Text = String.Empty
        lblPMKind.Visible = False
        ddlPMKind.SelectedIndex = 0
        ddlPMKind.Enabled = True
        'ddlPMKind.Items.Clear()
        'ddlPMKind.Visible = False

        'btnSimpan.Disabled = False
        ViewState.Add("vsProcess", "Insert")

    End Sub

    Private Sub ClearData()
        txtChassisMaster.Text = String.Empty
        txtEngineNo.Text = String.Empty
        txtDealerBranchCode.Text = String.Empty
        txtWONo.Text = String.Empty
        txtKM.Text = String.Empty
        txtTglServis.Text = String.Empty

        txtChassisMaster.ReadOnly = False
        txtEngineNo.ReadOnly = False
        txtWONo.ReadOnly = False
        txtKM.ReadOnly = False
        txtTglServis.ReadOnly = False
        txtPenggatianPart.Text = String.Empty

        lblPMKind.Visible = False
        'ddlPMKind.Items.Clear()
        'ddlPMKind.Visible = False
        ddlPMKind.SelectedIndex = 0
        ddlPMKind.Enabled = True

        btnSimpan.Disabled = Not bCekPriv
        ViewState.Add("vsProcess", "Insert")

    End Sub

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

    Private Function IsExistCodeForUpdate(ByVal PMHeaderID As Integer, ByVal ChassisID As Integer, ByVal DealerID As Integer, ByVal _serviceDate As Date) As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PMHeader), "ChassisMaster.ID", MatchType.Exact, ChassisID))
        'remark by anh, req by rna 20100805
        'criterias.opAnd(New Criteria(GetType(PMHeader), "Dealer.ID", MatchType.Exact, DealerID))
        'end remark
        criterias.opAnd(New Criteria(GetType(PMHeader), "ServiceDate", MatchType.LesserOrEqual, New Date(_serviceDate.Year, _serviceDate.Month, _serviceDate.Day, 23, 59, 59)))
        criterias.opAnd(New Criteria(GetType(PMHeader), "ServiceDate", MatchType.GreaterOrEqual, New Date(_serviceDate.Year, _serviceDate.Month, _serviceDate.Day, 0, 0, 0)))
        criterias.opAnd(New Criteria(GetType(PMHeader), "ID", MatchType.No, PMHeaderID))
        Dim TestExist As ArrayList = New PMHeaderFacade(User).Retrieve(criterias)
        If TestExist.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function GetCheckedPMItem() As ArrayList
        ' dgEntryPM.DataSource = CType(Session("SessArrPMHeader"), ArrayList)
        Dim arlCheckedPMItem As ArrayList = New ArrayList
        Dim nIndeks As Integer
        For Each dtgItem As DataGridItem In dgEntryPM.Items
            nIndeks = dtgItem.ItemIndex
            Dim objPM As PMHeader = CType(CType(dgEntryPM.DataSource, ArrayList)(nIndeks), PMHeader)
            If CType(dtgItem.FindControl("cbSelect"), CheckBox).Checked Then ' If CType(dtgItem.Cells(2).FindControl("cbSelect"), CheckBox).Checked Then
                If Not IsNothing(objPM) Then
                    If objPM.PMStatus = CType(EnumPMStatus.PMStatus.Baru, String) Then
                        Dim _MSPHelper As New MSPHelper
                        'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180201
                        '_strMSPStatus = _MSPHelper.CheckStatusMSP(objPM.ChassisMaster.ChassisNumber, objPM.PMKind.ID, _tempObjPMHeader, objPM.StandKM, objPM.ServiceDate)
                        'If _strMSPStatus = String.Empty AndAlso Not IsNothing(_tempObjPMHeader) Then
                        '    objPM.Remarks = If(Not IsNothing(_tempObjPMHeader), _tempObjPMHeader.Remarks, String.Empty)
                        '    objPM.MSPRegistrationHistoryID = If(Not IsNothing(_tempObjPMHeader), _tempObjPMHeader.MSPRegistrationHistoryID, 0)
                        '    objPM.IsValidMSP = _tempObjPMHeader.IsValidMSP
                        'Else
                        '    objPM.IsValidMSP = False
                        'End If
                        'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180201

                        arlCheckedPMItem.Add(objPM)
                    End If
                End If
            End If
        Next
        Return arlCheckedPMItem
    End Function

    Private Sub dgEntryPM_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgEntryPM.PageIndexChanged
        dgEntryPM.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dgEntryPM.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dgEntryPM_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEntryPM.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objPMHeader As PMHeader = e.Item.DataItem
            Dim objPMKind As PMKind = New PMKindFacade(User).RetrievePMKind(objPMHeader.StandKM)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgEntryPM.CurrentPageIndex * dgEntryPM.PageSize) 'getDataGridItemIndex()

            Dim lblVisitTypeGr As Label = CType(e.Item.FindControl("lblVisitTypeGrid"), Label)

            If lblVisitTypeGr.Text = "WI" Then
                lblVisitTypeGr.Text = "Walk In"
            ElseIf lblVisitTypeGr.Text = "BO" Then
                lblVisitTypeGr.Text = "Booking"
            Else
                lblVisitTypeGr.Text = ""
            End If

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                e.Item.FindControl("lbtnDelete").Visible = bCekGridPriv
                Dim strMsg As String = "Yakin Data dengan nomor rangka " & CType(e.Item.FindControl("lblChassisNo"), Label).Text & "  ini akan dihapus? "
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & strMsg & "');")
            End If

            If Not e.Item.FindControl("lbtnPart") Is Nothing Then
                Dim lblPart As Label = CType(e.Item.FindControl("lbtnPart"), Label)
                lblPart.Attributes("onclick") = "showPopUp('../General/../PopUp/PopReplecementPart.aspx?ID=" & objPMHeader.ID & "');"
            End If
            If Not e.Item.FindControl("lblPMKind") Is Nothing Then
                Dim lblPMKind As Label = CType(e.Item.FindControl("lblPMKind"), Label)
                lblPMKind.Text = objPMHeader.PMKind.KindCode
            End If
            If Not e.Item.FindControl("lblWONo") Is Nothing Then
                Dim lblWONo As Label = CType(e.Item.FindControl("lblWONo"), Label)
                lblWONo.Text = objPMHeader.WorkOrderNumber
            End If

            If Not IsNothing(objPMHeader.DealerBranch) Then
                Dim lblDealerBranch As Label = CType(e.Item.FindControl("lblDealerBranch"), Label)
                lblDealerBranch.Text = objPMHeader.DealerBranch.DealerBranchCode
            End If

            'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180115
            If Not e.Item.FindControl("lblMSPDescription") Is Nothing Then
                Dim lblMSPDescription As Label = CType(e.Item.FindControl("lblMSPDescription"), Label)
                lblMSPDescription.Text = objPMHeader.Remarks
            End If
            'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20180115
            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                lbtnEdit.Visible = bCekGridPriv
            End If
        End If

    End Sub

    Private Sub dgEntryPM_ItemCommand1(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPM.ItemCommand
        Select Case (e.CommandName)
            Case "Edit"
                ViewState.Add("vsProcess", "Update")
                ActivateControlEdit(Integer.Parse(e.Item.Cells(0).Text))
            Case "Delete"
                DeletePMHeader(Integer.Parse(e.Item.Cells(0).Text))
        End Select
    End Sub

    Private Sub dgEntryPM_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgEntryPM.SortCommand
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

        dgEntryPM.SelectedIndex = -1
        dgEntryPM.CurrentPageIndex = 0
        bindGridSorting(dgEntryPM.CurrentPageIndex)
    End Sub

    Private Sub btnBatal_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.ServerClick
        ClearData()
        dgEntryPM.SelectedIndex = -1
        Dim strScript As String
        strScript = "<script>document.all.txtChassisMaster.focus();</script>"
        Page.RegisterStartupScript("", strScript)
    End Sub

    Private Sub txtEngineNo_TextChanged(sender As Object, e As EventArgs) Handles txtEngineNo.TextChanged
        'GetDdlPMKind()
    End Sub

    Private Sub txtChassisMaster_TextChanged(sender As Object, e As EventArgs) Handles txtChassisMaster.TextChanged
        'GetDdlPMKind()
    End Sub

    'Private Function GetDdlPMKind()
    '    Dim str As String = String.Empty
    '    'ddlPMKind.Items.Clear()
    '    'ddlPMKind.Visible = False
    '    'lblPMKind.Visible = False

    '    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    crt.opAnd(New Criteria(GetType(ChassisMaster), "EngineNumber", MatchType.Exact, txtEngineNo.Text))
    '    Dim arr As ArrayList = New ChassisMasterFacade(User).Retrieve(crt)
    '    If arr.Count > 0 Then
    '        Dim objChassisMaster As ChassisMaster = CType(arr(0), ChassisMaster)
    '        If objChassisMaster.ChassisNumber <> txtChassisMaster.Text Then
    '            str = "No.Rangka tidak sesuai dengan No.Mesin."
    '            MessageBox.Show(str)
    '        Else
    '            crt = New CriteriaComposite(New Criteria(GetType(MSPRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '            crt.opAnd(New Criteria(GetType(MSPRegistration), "ChassisMaster.ID", MatchType.Exact, objChassisMaster.ID))
    '            arr = New MSPRegistrationFacade(User).Retrieve(crt)
    '            If arr.Count > 0 Then
    '                Dim objMSPReg As MSPRegistration = CType(arr(0), MSPRegistration)
    '                Dim strSQL As String = "(SELECT ID FROM MSPRegistrationHistory WHERE (([Status] >= 1 AND [Status] <> 2 AND IsDownloadCertificate = 1) OR [Status] = 6))"
    '                crt = New CriteriaComposite(New Criteria(GetType(MSPRegistrationHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                crt.opAnd(New Criteria(GetType(MSPRegistrationHistory), "ID", MatchType.InSet, strSQL))
    '                arr = New MSPRegistrationHistoryFacade(User).Retrieve(crt)
    '                'If arr.Count > 0 Then
    '                '    ' bind dropdownlist ddlPMKind
    '                '    lblPMKind.Visible = True
    '                '    ddlPMKind.Visible = True

    '                '    Dim dict As New Dictionary(Of String, String)
    '                '    Dim dtSet As DataSet = New PMHeaderFacade(User).RetrieveSp("EXEC sp_MSP_GetPMKindMSP " & objMSPReg.ID)
    '                '    If dtSet.Tables.Count > 0 Then
    '                '        Dim dtTbl As DataTable = dtSet.Tables(0)
    '                '        If dtTbl.Rows.Count > 0 Then
    '                '            Dim i As Integer = 1
    '                '            For Each item As DataRow In dtTbl.Rows
    '                '                Dim value As String = String.Empty
    '                '                value = item("KindCode").ToString

    '                '                If Not (dict.ContainsKey(item("ID").ToString)) Then
    '                '                    If Not String.IsNullOrEmpty(item("MSPDurationPMKind").ToString) Then
    '                '                        value = item("KindCode").ToString & " - Claim Ke-" & i.ToString
    '                '                        i += 1
    '                '                    End If

    '                '                    dict.Add(item("ID").ToString, value)
    '                '                End If
    '                '            Next
    '                '        End If
    '                '    End If

    '                '    ddlPMKind.DataSource = dict
    '                '    ddlPMKind.DataValueField = "Key"
    '                '    ddlPMKind.DataTextField = "Value"
    '                '    ddlPMKind.DataBind()
    '                '    ddlPMKind.Items.Insert(0, New ListItem("Silahkan Pilih", 0))
    '                '    ddlPMKind.SelectedIndex = 0
    '                'End If
    '            End If
    '        End If
    '    Else
    '        str = "No.Mesin tidak terdaftar."
    '        MessageBox.Show(str)
    '    End If
    'End Function

    Private Sub BindDDLPMKind()
        ddlPMKind.Items.Clear()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim oData As ArrayList = New PMKindFacade(User).RetrieveList(criterias, "KindCode", Sort.SortDirection.ASC)
        With ddlPMKind.Items
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each BP As PMKind In oData
                .Add(New ListItem(BP.KindCode + " - " + BP.KindDescription, BP.ID))
            Next
        End With
    End Sub
End Class
