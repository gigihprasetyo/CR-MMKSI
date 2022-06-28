Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.enumMode
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#Region "Custom Namespace Imports"
Imports PdfSharp
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports PdfSharp.Drawing
Imports SpireDoc = Spire.Doc
Imports Document = Spire.Doc.Document
Imports SpireDoc.Documents
Imports OfficeOpenXml
Imports Spire.Doc.Fields
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Net
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Data
Imports System.Linq
Imports System.Security
Imports System.Collections.Generic
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region


Public Class FrmSPLDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents hdnSPLID As System.Web.UI.WebControls.HiddenField
    Protected WithEvents lblSPLNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSPLNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblCustName As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtCustName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDesc As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblValidFrom As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents txtValidFrom As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtValidTo As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkFreeInterest As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtComment As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblComment As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitikComment As System.Web.UI.WebControls.Label
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDibuatOleh As System.Web.UI.WebControls.Label
    Protected WithEvents lblDibuatPada As System.Web.UI.WebControls.Label
    Protected WithEvents lblDiubahOleh As System.Web.UI.WebControls.Label
    Protected WithEvents lblDiubahPada As System.Web.UI.WebControls.Label
    Protected WithEvents tr1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents tr2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents tr3 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents tr4 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents dgProfileGroup As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgSPDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave2 As System.Web.UI.WebControls.Button
    Protected WithEvents lbtnPrevMonth As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnNextMonth As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblCurrentPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents PopUpDealer As System.Web.UI.WebControls.Image
    Protected WithEvents txtNumOfInstallment As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMaxTOPDay As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkIsAutoApprovedDealer As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkFinalApproval As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnRetrieveDetailDiscount As System.Web.UI.WebControls.Button

    Protected WithEvents txtLampiranPerhitunganDiskon As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblLampiranPerhitunganDiskon As System.Web.UI.WebControls.Label
    Protected WithEvents btnUploadLampiranPerhitunganDiskon As System.Web.UI.WebControls.Button
    Protected WithEvents FULampiranPerhitunganDiskon As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lbtnDeleteLampiranPerhitunganDiskon As System.Web.UI.WebControls.LinkButton

    Protected WithEvents txtAttachment As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAttachment As System.Web.UI.WebControls.Label
    Protected WithEvents btnUploadAttachment As System.Web.UI.WebControls.Button
    Protected WithEvents FUAttachment As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lbtnDeleteAttachment As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkAppointmentLetter As System.Web.UI.WebControls.LinkButton

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
    Private _SPLFacade As New SPLFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private sessHelper As New SessionHelper
    Private ObjSPL As New SPL
    Private ObjSPLDetail As New SPLDetail
    Private ObjSPLDetailtoSPL As New SPLDetailtoSPL
    Private arlSPLDetailList As ArrayList
    Private arlSPLDetailtoSPLList As ArrayList
    Private Mode As enumMode
    Private IndexList As Integer
    Private blnCommandEdit As Boolean = False
    Private _AttachmentData As System.Web.HttpPostedFile

    Private MAX_FILE_SIZE As Integer = 5120000
    Dim strTargetDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")
    Dim strTempDirectory As String = Server.MapPath("") + "\..\DataTemp\"

#End Region

#Region "PrivateCustomMethods"
    Private Function IsMonthYearValid() As String
        Dim retValue As String = ""
        Try
            Dim dd1 As Date = GetDateFromMonthYear(txtValidFrom.Text.Trim(), 1)
            Dim dd2 As Date = GetDateFromMonthYear(txtValidTo.Text.Trim(), 2)
            'If dd1 > dd2 Then
            '    retValue = "Valid sampai tidak boleh lebih besar dari valid dari"
            'Else
            '    retValue = ""
            'End If

            If dd1 > dd2 Then
                retValue = "Valid dari tidak boleh lebih besar dari valid sampai"
            Else
                retValue = ""
            End If

        Catch ex As Exception
            retValue = "Format bulan dan tahun di 'Valid Dari/Valid Sampai' salah"
        End Try
        Return retValue
    End Function
    Private Function IsExistSPLNumber(ByVal SPLNumber As String) As Boolean
        Dim isExist As Boolean = True
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPL), "SPLNumber", MatchType.Exact, SPLNumber))

        Dim arrList As ArrayList = _SPLFacade.Retrieve(criterias)
        If arrList.Count > 0 Then
            isExist = True
        Else
            isExist = False
        End If

        Return isExist
    End Function
    Private Function CheckExt(ByVal ext As String) As Boolean
        Dim retValue As Boolean = False
        If ext.ToUpper() = "PDF" Or ext.ToUpper() = "XLS" Or ext.ToUpper() = "XLSX" Or ext.ToUpper() = "DOC" Or ext.ToUpper() = "DOCX" Or ext.ToUpper() = "ZIP" Or ext.ToUpper() = "RAR" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function

    Private Function UploadFile(ByRef ObjSPL As SPL, ByRef _message As String) As Integer
        Dim retValue As Integer = 0
        _AttachmentData = CType(sessHelper.GetSession("FrmSPLDetail.Attachment"), System.Web.HttpPostedFile)
        If IsNothing(_AttachmentData) Then
            _AttachmentData = FUAttachment.PostedFile
        End If

        If _AttachmentData.FileName.Length > 0 Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            sapImp.Start()
            Try
                If _AttachmentData.ContentLength <> _AttachmentData.InputStream.Length Then
                    'MessageBox.Show(SR.InvalidData(_AttachmentData.FileName))
                    retValue = 0
                    Throw New Exception("File Tidak Sama")
                End If

                Dim directory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("SPLAttachment")
                Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directory)
                If Not directoryInfo.Exists Then
                    directoryInfo.Create()
                End If

                Dim ext As String = System.IO.Path.GetExtension(_AttachmentData.FileName)
                If Not CheckExt(ext.Substring(1)) Then
                    retValue = 0
                    Throw New Exception("Harap upload tipe file sesuai yang ditentukan (PDF/XLS/XLSX/DOC/DOCX/ZIP/RAR)")
                End If

                Dim filename As String = txtSPLNumber.Text.Replace("/", "_") & ext
                Dim targetFile As String = New System.Text.StringBuilder(directory). _
                    Append("\").Append(filename).ToString

                Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
                If fInfo.Exists Then
                    fInfo.Delete()
                End If

                _AttachmentData.SaveAs(targetFile)
                Dim trgInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
                If Not trgInfo.Exists Then
                    retValue = 0
                End If
                Dim strFileSave As String = KTB.DNet.Lib.WebConfig.GetValue("SPLAttachment") & "\" & filename
                ObjSPL.Attachment = strFileSave
                retValue = 1

            Catch ex As Exception
                _message = ex.Message
                retValue = 0
            Finally
                sapImp.StopImpersonate()
            End Try
            Return retValue
        Else
            retValue = 1
            Return retValue
        End If
    End Function
    Private Function GetDateFromMonthYear(ByVal MonthYear As String, ByVal type As Integer) As Date
        If MonthYear.Length = 5 Then
            MonthYear = "0" + MonthYear
        End If
        Dim month As Integer = CInt(MonthYear.Substring(0, 2))
        Dim year As Integer = CInt(MonthYear.Substring(2, 4))
        Dim retDate As DateTime
        If type = 1 Then
            retDate = New Date(year, month, 1)
        Else
            retDate = New Date(year, month, DateTime.DaysInMonth(year, month))
        End If
        Return retDate
    End Function
    Private Sub GetEnableControl(ByVal isEnabled As Boolean)
        If Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
            txtSPLNumber.Attributes.Add("readonly", "readonly")
            'txtSPLNumber.ReadOnly = True
        Else
            If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
                txtSPLNumber.Attributes.Add("readonly", "readonly")
            Else
                txtSPLNumber.Attributes.Remove("readonly")
            End If
            ' txtSPLNumber.ReadOnly = Not isEnabled
        End If
        If isEnabled Then
            txtDealerName.Attributes.Remove("readonly")
            txtCustName.Attributes.Remove("readonly")
            txtDescription.Attributes.Remove("readonly")
            txtValidFrom.Attributes.Remove("readonly")
            txtValidTo.Attributes.Remove("readonly")
        Else
            txtDealerName.Attributes.Add("readonly", "readonly")
            txtCustName.Attributes.Add("readonly", "readonly")
            txtDescription.Attributes.Add("readonly", "readonly")
            txtValidFrom.Attributes.Add("readonly", "readonly")
            txtValidTo.Attributes.Add("readonly", "readonly")
        End If
        'txtDealerName.ReadOnly = Not isEnabled
        'txtCustName.ReadOnly = Not isEnabled
        'txtDescription.ReadOnly = Not isEnabled
        'txtValidFrom.ReadOnly = Not isEnabled
        'txtValidTo.ReadOnly = Not isEnabled
        ddlStatus.Enabled = isEnabled
        btnSave.Enabled = isEnabled

        lblComment.Visible = False
        lblTitikComment.Visible = False
        txtComment.Visible = False
        Dim idSPL As Integer = CInt(sessHelper.GetSession("IDSPLHeader"))
        Dim ObjSPL As SPL = _SPLFacade.Retrieve(idSPL)
        If Not IsNothing(ObjSPL) AndAlso ObjSPL.ID > 0 Then
            Select Case ObjSPL.ApprovalStatus
                Case 5, 6, 7
                    lblComment.Visible = True
                    lblTitikComment.Visible = True
                    txtComment.Visible = True
                Case Else
                    lblComment.Visible = False
                    lblTitikComment.Visible = False
                    txtComment.Visible = False
            End Select
        End If

        txtLampiranPerhitunganDiskon.Enabled = isEnabled
        txtLampiranPerhitunganDiskon.Visible = isEnabled
        lblLampiranPerhitunganDiskon.Visible = Not isEnabled
        FULampiranPerhitunganDiskon.Visible = isEnabled
        If txtLampiranPerhitunganDiskon.Text = "" Then
            lbtnDeleteLampiranPerhitunganDiskon.Visible = False
        Else
            lbtnDeleteLampiranPerhitunganDiskon.Visible = isEnabled
        End If
        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            txtLampiranPerhitunganDiskon.Visible = False
            FULampiranPerhitunganDiskon.Visible = False
            lbtnDeleteLampiranPerhitunganDiskon.Visible = False
            lblLampiranPerhitunganDiskon.Visible = True
        End If

        txtAttachment.Enabled = isEnabled
        txtAttachment.Visible = isEnabled
        FUAttachment.Visible = isEnabled
        lblAttachment.Visible = Not isEnabled
        If txtAttachment.Text = "" Then
            lbtnDeleteAttachment.Visible = False
        Else
            lbtnDeleteAttachment.Visible = isEnabled
        End If
        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            txtAttachment.Visible = False
            FUAttachment.Visible = False
            lbtnDeleteAttachment.Visible = False
            lblAttachment.Visible = True
        End If

        chkIsAutoApprovedDealer.Enabled = isEnabled
        chkFinalApproval.Enabled = isEnabled
    End Sub

    Private Sub GetValueToDatabase(ByRef ObjSPL As SPL)
        ObjSPL.SPLNumber = txtSPLNumber.Text
        ObjSPL.DealerName = txtDealerName.Text
        ObjSPL.CustomerName = txtCustName.Text
        ObjSPL.Description = txtDescription.Text
        ObjSPL.Comment = txtComment.Text
        ObjSPL.ValidFrom = GetDateFromMonthYear(txtValidFrom.Text, 1)
        ObjSPL.ValidTo = GetDateFromMonthYear(txtValidTo.Text, 2)
        ObjSPL.Status = CType(EnumStatusSPL.StatusSPL.Aktif, Short)
        ObjSPL.NumOfInstallment = CType(Me.txtNumOfInstallment.Text, Integer)
        ObjSPL.MaxTOPDay = CType(Me.txtMaxTOPDay.Text, Integer)
        ObjSPL.IsAutoApprovedDealer = IIf(chkIsAutoApprovedDealer.Checked = True, 1, 0)
        ObjSPL.FinalApproval = IIf(chkFinalApproval.Checked = True, 1, 0)
        _AttachmentData = CType(sessHelper.GetSession("FrmSPLDetail.Attachment"), System.Web.HttpPostedFile)
        If IsNothing(_AttachmentData) Then
            If txtAttachment.Text.Trim = "" Then
                ObjSPL.Attachment = ""
            End If
        End If

        ObjSPL.IsFromDP = 0
    End Sub

    Private Sub GetValueFromDataBase(ByVal idSPL As Integer)
        Dim ObjSPL As SPL = _SPLFacade.Retrieve(idSPL)
        hdnSPLID.Value = ObjSPL.ID

        txtSPLNumber.Text = ObjSPL.SPLNumber
        Dim _splDealer As ArrayList = _SPLFacade.RetrieveDealerID(idSPL)
        sessHelper.SetSession("OLDSPLDealer", _splDealer)
        Dim _tempDealer As String = ""
        If Not _splDealer Is Nothing Then
            For Each item As SPLDealer In _splDealer
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(item.Dealer.ID)
                _tempDealer += objDealer.DealerCode + ";"
            Next
            'txtDealerName.Text = _tempDealer.Remove(_tempDealer.LastIndexOf(";"), 1)
            txtDealerName.Text = _tempDealer
        End If
        txtCustName.Text = ObjSPL.CustomerName
        txtDescription.Text = ObjSPL.Description
        txtComment.Text = ObjSPL.Comment
        Select Case ObjSPL.ApprovalStatus
            Case 5, 6, 7
                lblComment.Visible = True
                lblTitikComment.Visible = True
                txtComment.Visible = True
            Case Else
                lblComment.Visible = False
                lblTitikComment.Visible = False
                txtComment.Visible = False
        End Select

        txtValidFrom.Text = ReturnMonth2Digit(Convert.ToString(ObjSPL.ValidFrom.Month)) & Convert.ToString(ObjSPL.ValidFrom.Year)
        txtValidTo.Text = ReturnMonth2Digit(Convert.ToString(ObjSPL.ValidTo.Month)) & Convert.ToString(ObjSPL.ValidTo.Year)

        ddlStatus.SelectedValue = ObjSPL.Status
        lblDibuatOleh.Text = UserInfo.Convert(ObjSPL.CreatedBy)
        lblDibuatPada.Text = ObjSPL.CreatedTime
        lblDiubahOleh.Text = UserInfo.Convert(ObjSPL.LastUpdateBy)
        lblDiubahPada.Text = ObjSPL.LastUpdateTime

        Me.txtNumOfInstallment.Text = ObjSPL.NumOfInstallment.ToString()
        Me.txtMaxTOPDay.Text = ObjSPL.MaxTOPDay.ToString()

        chkIsAutoApprovedDealer.Checked = IIf(ObjSPL.IsAutoApprovedDealer = 0, False, True)
        chkFinalApproval.Checked = IIf(ObjSPL.FinalApproval = 0, False, True)

        Dim arlSPLDetail As ArrayList
        Dim crit As New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(SPLDetail), "SPL.ID", MatchType.Exact, ObjSPL.ID))
        arlSPLDetail = New SPLDetailFacade(User).Retrieve(crit)
        sessHelper.SetSession("SPLDETAILLIST", arlSPLDetail)

        'Dim arlSPLDetailtoSPLALL As ArrayList = New SPLDetailtoSPLFacade(User).RetrieveSPLDetailtoSPLFromSP(ObjSPL.ID)

        Dim crit2 As New CriteriaComposite(New Criteria(GetType(SPLDetailtoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit2.opAnd(New Criteria(GetType(SPLDetailtoSPL), "SPLDetail.SPL.ID", MatchType.Exact, ObjSPL.ID))
        Dim arlSPLDetailtoSPLALL As ArrayList = New SPLDetailtoSPLFacade(User).Retrieve(crit2)
        'For Each oSPLDetail As SPLDetail In arlSPLDetail
        '    Dim arlSPLDetailtoSPL As ArrayList = oSPLDetail.SPLDetailtoSPLs
        '    If Not IsNothing(arlSPLDetailtoSPL) AndAlso arlSPLDetailtoSPL.Count > 0 Then
        '        arlSPLDetailtoSPLALL.AddRange(arlSPLDetailtoSPL)
        '    End If
        'Next
        sessHelper.SetSession("SPLDETAILTOSPLLIST", arlSPLDetailtoSPLALL)

        Dim arlSPLDtlDocument As ArrayList
        Dim crit1 As New CriteriaComposite(New Criteria(GetType(SPLDetailDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit1.opAnd(New Criteria(GetType(SPLDetailDocument), "FileType", MatchType.Exact, 1)) '--- File Doc by Attactment
        crit1.opAnd(New Criteria(GetType(SPLDetailDocument), "SPL.ID", MatchType.Exact, ObjSPL.ID))
        arlSPLDtlDocument = New SPLDetailDocumentFacade(User).Retrieve(crit1)
        sessHelper.SetSession("SPLDETAILDOCUMENTLIST", arlSPLDtlDocument)
        For Each objSPLDoc As SPLDetailDocument In arlSPLDtlDocument
            If objSPLDoc.FileType = 1 Then   '---ValueCode = Surat Pernyataan
                txtLampiranPerhitunganDiskon.Text = objSPLDoc.Path
                lblLampiranPerhitunganDiskon.Text = objSPLDoc.FileName
                Exit For
            End If
        Next

        txtAttachment.Text = ObjSPL.Attachment
        lblAttachment.Text = ObjSPL.Attachment
    End Sub

    Private Function ReturnMonth2Digit(ByVal mm As String) As String
        If mm.Length < 2 Then
            mm = "0" & mm
        End If
        Return mm
    End Function
    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = EnumStatusSPL.RetrieveStatus
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataBind()
    End Sub
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ENHPKDaftarAplikasiLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Detail SPL")
        End If

        _create = SecurityProvider.Authorize(Context.User, SR.ENHPKDaftarAplikasiBuat_Privilege)
        _edit = SecurityProvider.Authorize(Context.User, SR.ENHPKDaftarAplikasiUbah_Privilege)

        btnSave.Visible = _create And _edit
    End Sub
    Private Sub RemoveALLSession()
        sessHelper.RemoveSession("OLDSPLDealer")
        sessHelper.RemoveSession("OLDSPLDETAILLIST")
        sessHelper.RemoveSession("OLDSPLDETAILTOSPLLIST")
        sessHelper.RemoveSession("SPLDETAILLIST")
        sessHelper.RemoveSession("SPLDETAILTOSPLLIST")
        sessHelper.RemoveSession("SPLDETAILDOCUMENTLIST")
        sessHelper.RemoveSession("DELETESPLDETAILLIST")
        sessHelper.RemoveSession("DELETESPLDETAILTOSPLLIST")
        sessHelper.RemoveSession("DELETESPLDETAILDOCUMENTLIST")
        sessHelper.RemoveSession("STATUSMONTH")
        sessHelper.RemoveSession("STATUSMAXMONTH")
        sessHelper.RemoveSession("STATUSMINMONTH")
        sessHelper.RemoveSession("FrmSPLDetail.Attachment")
        sessHelper.RemoveSession("IDSPLHeader")
    End Sub
    Private Sub Initialize()
        BindDdlStatus()
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnBack.Visible = True
        FULampiranPerhitunganDiskon.Attributes("onchange") = "UploadFile1(this)"
        FUAttachment.Attributes("onchange") = "UploadFile2(this)"

        'If Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then
        '    txtSPLNumber.Text = ""
        '    txtDealerName.Text = ""
        '    txtCustName.Text = ""
        '    txtDescription.Text = ""
        '    txtValidFrom.Text = ""
        '    txtValidTo.Text = ""
        '    lblAttachment.Text = ""
        '    GetEnableControl(True)
        '    tr1.Visible = False
        '    ' tr2.Visible = False
        '    tr3.Visible = False
        '    'tr4.Visible = False
        '    ddlStatus.SelectedValue = 1
        '    ddlStatus.Enabled = False
        '    lbtnNextMonth.Visible = False
        '    lbtnPrevMonth.Visible = False
        '    dgSPDetail.ShowFooter = False
        '    btnBack.Visible = False
        'End If

        If Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
            Dim idSPL As Integer = CInt(sessHelper.GetSession("IDSPLHeader"))
            GetValueFromDataBase(idSPL)
            ddlStatus.Enabled = True
            GetEnableControl(True)
            BindDetail(idSPL)
            If ddlStatus.SelectedValue = 1 Then
                dgSPDetail.ShowFooter = False
                lbtnNextMonth.Visible = False
                lbtnPrevMonth.Visible = False
            Else
                dgSPDetail.ShowFooter = True
                lbtnNextMonth.Visible = True
                lbtnPrevMonth.Visible = True
            End If
        ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            Dim idSPL As Integer = CInt(sessHelper.GetSession("IDSPLHeader"))
            GetValueFromDataBase(idSPL)
            BindDetail(idSPL)
            GetEnableControl(False)
            dgSPDetail.ShowFooter = False
        Else
            txtSPLNumber.Text = ""
            txtDealerName.Text = ""
            txtCustName.Text = ""
            txtDescription.Text = ""
            txtComment.Text = ""
            txtValidFrom.Text = ""
            txtValidTo.Text = ""
            lblAttachment.Text = ""
            GetEnableControl(True)
            tr1.Visible = False
            ' tr2.Visible = False
            tr3.Visible = False
            'tr4.Visible = False
            ddlStatus.SelectedValue = 1
            ddlStatus.Enabled = False
            lbtnNextMonth.Visible = False
            lbtnPrevMonth.Visible = False
            dgSPDetail.ShowFooter = False
            btnBack.Visible = False
            sessHelper.SetSession("Status", "Insert")
        End If
    End Sub

    Private Function GetTOPInstallment(ByVal DealerID As Integer, ByVal SPLD As SPLDetail, ByVal NumInstallMent As Integer) As Integer
        Dim Val As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.VechileType.ID", MatchType.Exact, SPLD.VechileType.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, DealerID))
        Dim DTPrice As DateTime = New DateTime(SPLD.PeriodYear, SPLD.PeriodMonth, 1)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "ValidFrom", MatchType.LesserOrEqual, Format(DTPrice, "yyyy-MM-dd")))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))
        Dim objPriceArrayList As ArrayList = New ArrayList
        objPriceArrayList = New PriceFacade(User).Retrieve(criterias, sortColl)

        If Not IsNothing(objPriceArrayList) AndAlso objPriceArrayList.Count > 0 Then
            Dim ObjPrice As Price = CType(objPriceArrayList(0), Price)
            Dim MainInstallment As Decimal = ObjPrice.BasePrice
            MainInstallment = Me.RoundUp(MainInstallment / CDec(NumInstallMent), -3)
            Dim Dt As DateTime = DateTime.Now.Date
            Dim vBalance As Decimal = ObjPrice.BasePrice
            Dim TINterest As Decimal = 0

            'Jika tak punya interest harfcode to 1.5
            ' requested bby jeng ike 20161114
            'confirmed by sugeng via phone
            If ObjPrice.Interest = 0 Then
                ObjPrice.Interest = 1.5
            End If
            Dim Tdate As Integer = 0
            Dim TDay As Date = Dt

            For idM As Integer = 1 To NumInstallMent
                Dim VINterest As Decimal = 0
                VINterest = vBalance * ObjPrice.Interest * 1.0 / 100.0
                vBalance = vBalance - MainInstallment
                If idM > 1 Then
                    Dim DifDate As TimeSpan
                    Dim IDif As Integer = 0
                    TINterest = TINterest + VINterest
                    TDay = TDay.AddMonths(1)

                    DifDate = TDay.Subtract(Dt)
                    IDif = DifDate.Days
                    Tdate = Tdate + IDif

                    Dt = Dt.AddMonths(1)
                End If
            Next

            Dim DayT As Integer = Math.Round(((365 * TINterest) / (ObjPrice.BasePrice * ObjPrice.Interest * (12.0 / 100.0))), 0)
            Val = DayT
        Else
            Throw New Exception("No Data Price")
        End If
        Return Val
    End Function

    Private Function RoundDown(ByVal number As Double, ByVal decimalPlaces As Integer) As Double

        Return Math.Floor(number * Math.Pow(10, decimalPlaces)) / Math.Pow(10, decimalPlaces)
    End Function

    Private Function RoundUp(ByVal number As Double, ByVal decimalPlaces As Integer) As Double

        Return Math.Ceiling(number * Math.Pow(10, decimalPlaces)) / Math.Pow(10, decimalPlaces)
    End Function

    ''Penambahan logic copy data untuk perpanjang periode, hanya untuk proses update, bukan insert
    Private Function copyDetail(ByVal objSPL As SPL, ByVal arlSPLDetails As ArrayList, ByVal arlSPLDetailtoSPLs As ArrayList, ByVal lastValidTo As DateTime) As ArrayList
        Dim _arlSPLDetails As New ArrayList
        Dim _arlSPLDetailtoSPLs As New ArrayList
        Dim arrNewSPLDetail As ArrayList = New ArrayList()
        Dim arrNewSPLDetailToSPL As ArrayList = New ArrayList()
        Dim seqLoop As Integer = 0

        Dim startMonth As DateTime = lastValidTo
        Dim endMonth As DateTime = objSPL.ValidTo
        Dim periodeGap As Integer = Math.Abs((startMonth.Month - endMonth.Month) + 12 * (startMonth.Year - endMonth.Year))

        _arlSPLDetails = New System.Collections.ArrayList(
                                    (From obj As SPLDetail In arlSPLDetails.OfType(Of SPLDetail)()
                                        Where obj.SisaQty > 0 _
                                        And obj.PeriodMonth = lastValidTo.Month _
                                        And obj.PeriodYear = lastValidTo.Year _
                                        Order By obj.PeriodYear, obj.PeriodMonth, obj.ModelID, obj.VechileTypeID
                                        Select obj).ToList())

        _arlSPLDetailtoSPLs = New System.Collections.ArrayList(
                                        (From obj As SPLDetailtoSPL In arlSPLDetailtoSPLs.OfType(Of SPLDetailtoSPL)()
                                            Where obj.PeriodMonth = lastValidTo.Month _
                                            And obj.PeriodYear = lastValidTo.Year _
                                            Order By obj.PeriodYear, obj.PeriodMonth, obj.ModelID, obj.VechileTypeID
                                            Select obj).ToList())

        If periodeGap > 0 And _arlSPLDetails.Count > 0 Then
            For i As Integer = 0 To _arlSPLDetails.Count - 1
                Dim tempSPLDetail As SPLDetail = CType(_arlSPLDetails(i), SPLDetail)
                Dim SPLDetailID As Integer = tempSPLDetail.ID
                If tempSPLDetail.SisaQty > 0 Then
                    If lastValidTo.Month = tempSPLDetail.PeriodMonth And lastValidTo.Year = tempSPLDetail.PeriodYear Then
                        tempSPLDetail.SPL = objSPL
                        Dim tempArrSPLDetail As ArrayList = generateSPLDetailCopy(tempSPLDetail, periodeGap, lastValidTo)
                        arrNewSPLDetail.AddRange(tempArrSPLDetail)

                        If Not IsNothing(_arlSPLDetailtoSPLs) AndAlso _arlSPLDetailtoSPLs.Count > 0 Then
                            For j As Integer = seqLoop To _arlSPLDetailtoSPLs.Count - 1
                                Dim oSPLDetailtoSPL As SPLDetailtoSPL = CType(_arlSPLDetailtoSPLs(j), SPLDetailtoSPL)
                                Dim oSPLDetailtoSPL2 As SPLDetailtoSPL
                                If Not IsNothing(oSPLDetailtoSPL.SPLDetail) Then
                                    If SPLDetailID = oSPLDetailtoSPL.SPLDetail.ID Then
                                        arrNewSPLDetailToSPL.AddRange(generateSPLDetailToSPLCopy(tempArrSPLDetail, oSPLDetailtoSPL))
                                        Try
                                            oSPLDetailtoSPL2 = CType(_arlSPLDetailtoSPLs(j + 1), SPLDetailtoSPL)
                                            If Not IsNothing(oSPLDetailtoSPL2) Then
                                                If tempSPLDetail.PeriodYear <> oSPLDetailtoSPL2.PeriodYear OrElse _
                                                    tempSPLDetail.PeriodMonth <> oSPLDetailtoSPL2.PeriodMonth OrElse _
                                                    tempSPLDetail.ModelID <> oSPLDetailtoSPL2.ModelID OrElse _
                                                    tempSPLDetail.VechileType.ID <> oSPLDetailtoSPL2.VechileTypeID Then
                                                    seqLoop = j + 1
                                                    Exit For
                                                End If
                                            End If
                                        Catch
                                        End Try
                                        seqLoop = j + 1
                                    End If
                                Else
                                    If tempSPLDetail.PeriodYear = oSPLDetailtoSPL.PeriodYear _
                                        And tempSPLDetail.PeriodMonth = oSPLDetailtoSPL.PeriodMonth _
                                        And tempSPLDetail.ModelID = oSPLDetailtoSPL.ModelID _
                                        And tempSPLDetail.VechileType.ID = CInt(oSPLDetailtoSPL.VechileTypeID) Then
                                        arrNewSPLDetailToSPL.AddRange(generateSPLDetailToSPLCopy(tempArrSPLDetail, oSPLDetailtoSPL))

                                        Try
                                            oSPLDetailtoSPL2 = CType(_arlSPLDetailtoSPLs(j + 1), SPLDetailtoSPL)
                                            If Not IsNothing(oSPLDetailtoSPL2) Then
                                                If tempSPLDetail.PeriodYear <> oSPLDetailtoSPL2.PeriodYear OrElse _
                                                    tempSPLDetail.PeriodMonth <> oSPLDetailtoSPL2.PeriodMonth OrElse _
                                                    tempSPLDetail.ModelID <> oSPLDetailtoSPL2.ModelID OrElse _
                                                    tempSPLDetail.VechileType.ID <> oSPLDetailtoSPL2.VechileTypeID Then
                                                    seqLoop = j + 1
                                                    Exit For
                                                End If
                                            End If
                                        Catch
                                        End Try
                                        seqLoop = j + 1
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
            Next

            seqLoop = 0
            Dim additionalSPLDetail As ArrayList = New ArrayList()
            additionalSPLDetail = getUnavailableSPLDetail(objSPL, arlSPLDetails, lastValidTo, periodeGap)
            If additionalSPLDetail.Count > 0 Then
                additionalSPLDetail = New System.Collections.ArrayList(
                                            (From obj As SPLDetail In additionalSPLDetail.OfType(Of SPLDetail)()
                                                Order By obj.PeriodYear, obj.PeriodMonth, obj.ModelID, obj.VechileTypeID
                                                Select obj).ToList())

                _arlSPLDetailtoSPLs = New System.Collections.ArrayList(
                                                (From obj As SPLDetailtoSPL In arlSPLDetailtoSPLs.OfType(Of SPLDetailtoSPL)()
                                                    Order By obj.PeriodYear, obj.PeriodMonth, obj.ModelID, obj.VechileTypeID
                                                    Select obj).ToList())

                For i As Integer = 0 To additionalSPLDetail.Count - 1
                    Dim tempUnavailable As SPLDetail = CType(additionalSPLDetail(i), SPLDetail)
                    Dim SPLDetailID As Integer = tempUnavailable.ID
                    Dim additionalSPLDetailRsult As ArrayList = generateSPLDetailCopy(tempUnavailable, periodeGap, lastValidTo)
                    arrNewSPLDetail.AddRange(additionalSPLDetailRsult)

                    If Not IsNothing(_arlSPLDetailtoSPLs) AndAlso _arlSPLDetailtoSPLs.Count > 0 Then
                        For j As Integer = seqLoop To _arlSPLDetailtoSPLs.Count - 1
                            Dim oSPLDetailtoSPL As SPLDetailtoSPL = CType(_arlSPLDetailtoSPLs(j), SPLDetailtoSPL)
                            Dim oSPLDetailtoSPL2 As SPLDetailtoSPL
                            If Not IsNothing(oSPLDetailtoSPL.SPLDetail) Then
                                If SPLDetailID = oSPLDetailtoSPL.SPLDetail.ID Then
                                    arrNewSPLDetailToSPL.AddRange(generateSPLDetailToSPLCopy(additionalSPLDetailRsult, oSPLDetailtoSPL))
                                    Try
                                        oSPLDetailtoSPL2 = CType(_arlSPLDetailtoSPLs(j + 1), SPLDetailtoSPL)
                                        If Not IsNothing(oSPLDetailtoSPL2) Then
                                            If tempUnavailable.PeriodYear <> oSPLDetailtoSPL2.PeriodYear OrElse _
                                                tempUnavailable.PeriodMonth <> oSPLDetailtoSPL2.PeriodMonth OrElse _
                                                tempUnavailable.ModelID <> oSPLDetailtoSPL2.ModelID OrElse _
                                                tempUnavailable.VechileType.ID <> oSPLDetailtoSPL2.VechileTypeID Then
                                                seqLoop = j + 1
                                                Exit For
                                            End If
                                        End If
                                    Catch
                                    End Try
                                    seqLoop = j + 1
                                End If
                            Else
                                If tempUnavailable.PeriodYear = oSPLDetailtoSPL.PeriodYear _
                                    And tempUnavailable.PeriodMonth = oSPLDetailtoSPL.PeriodMonth _
                                    And tempUnavailable.ModelID = oSPLDetailtoSPL.ModelID _
                                    And tempUnavailable.VechileType.ID = CInt(oSPLDetailtoSPL.VechileTypeID) Then
                                    arrNewSPLDetailToSPL.AddRange(generateSPLDetailToSPLCopy(additionalSPLDetailRsult, oSPLDetailtoSPL))
                                    Try
                                        oSPLDetailtoSPL2 = CType(_arlSPLDetailtoSPLs(j + 1), SPLDetailtoSPL)
                                        If Not IsNothing(oSPLDetailtoSPL2) Then
                                            If tempUnavailable.PeriodYear <> oSPLDetailtoSPL2.PeriodYear OrElse _
                                                tempUnavailable.PeriodMonth <> oSPLDetailtoSPL2.PeriodMonth OrElse _
                                                tempUnavailable.ModelID <> oSPLDetailtoSPL2.ModelID OrElse _
                                                tempUnavailable.VechileType.ID <> oSPLDetailtoSPL2.VechileTypeID Then
                                                seqLoop = j + 1
                                                Exit For
                                            End If
                                        End If
                                    Catch
                                    End Try
                                    seqLoop = j + 1
                                End If
                            End If
                        Next
                    End If
                Next
            End If
        End If

        removeIdentic(objSPL, arrNewSPLDetailToSPL, lastValidTo)

        Dim ret As ArrayList = New ArrayList()
        ret.Add(arrNewSPLDetail)
        ret.Add(arrNewSPLDetailToSPL)

        Return ret
    End Function

    Private Function generateSPLDetailCopy(ByVal objSPLDetail As SPLDetail, ByVal periodeGap As Integer, ByVal lastValidTo As DateTime) As ArrayList
        Dim arrReturnSPLDetail As ArrayList = New ArrayList()
        Dim isNextYear As Boolean = False
        Dim periodYear As Integer = objSPLDetail.PeriodYear
        For i As Integer = 1 To periodeGap
            Dim tempSPL As SPLDetail = New SPLDetail()
            'Dim month As Integer = objSPLDetail.PeriodMonth + i
            Dim month As Integer = lastValidTo.Month + i
            If month > 12 Then
                Dim tempMonth As Integer = month Mod 12
                If tempMonth = 0 Then
                    tempSPL.PeriodMonth = 12
                Else
                    tempSPL.PeriodMonth = tempMonth
                End If

                If tempMonth = 1 Then
                    isNextYear = True
                End If
            Else
                tempSPL.PeriodMonth = month
            End If

            If month = 0 Then
                tempSPL.PeriodMonth = 0
            End If

            If isNextYear Then
                isNextYear = False
                periodYear = periodYear + 1
            End If

            tempSPL.PeriodYear = periodYear
            tempSPL.SPL = objSPLDetail.SPL
            tempSPL.VechileType = objSPLDetail.VechileType
            tempSPL.Quantity = objSPLDetail.SisaQty
            tempSPL.PriceRefDate = objSPLDetail.PriceRefDate
            tempSPL.Discount = objSPLDetail.Discount
            tempSPL.Surcharge = objSPLDetail.Surcharge
            tempSPL.MaxTopDate = objSPLDetail.MaxTopDate
            tempSPL.MaxTopDay = objSPLDetail.MaxTopDay
            tempSPL.MaxTopIndicator = objSPLDetail.MaxTopIndicator
            tempSPL.FreeIntIndicator = objSPLDetail.FreeIntIndicator
            tempSPL.CreditCeiling = objSPLDetail.CreditCeiling
            tempSPL.DeliveryDate = objSPLDetail.DeliveryDate.AddMonths(i)
            tempSPL.NumberRow = objSPLDetail.NumberRow
            tempSPL.ModelID = objSPLDetail.ModelID
            tempSPL.IsUpdated = False
            arrReturnSPLDetail.Add(tempSPL)
        Next

        Return arrReturnSPLDetail
    End Function

    Private Function generateSPLDetailToSPLCopy(ByVal arrSPLDetail As ArrayList, ByVal objSPLDetailToSPL As SPLDetailtoSPL) As ArrayList
        Dim arrReturnSPLDetailToSPL As ArrayList = New ArrayList()
        Dim tempPSL As SPL = ObjSPLDetail.SPL
        Dim isNextYear As Boolean = False

        For i As Integer = 0 To arrSPLDetail.Count - 1
            Dim tempSPLDetailToSPL As SPLDetailtoSPL = New SPLDetailtoSPL()
            Dim objSPLDetail As SPLDetail = CType(arrSPLDetail(i), SPLDetail)
            tempSPLDetailToSPL.ID = 0
            tempSPLDetailToSPL.SPLDetail = objSPLDetail
            tempSPLDetailToSPL.PeriodMonth = objSPLDetail.PeriodMonth
            tempSPLDetailToSPL.PeriodYear = objSPLDetail.PeriodYear
            tempSPLDetailToSPL.Discount = objSPLDetailToSPL.Discount
            tempSPLDetailToSPL.SPLDetailReference = objSPLDetailToSPL.SPLDetailReference
            tempSPLDetailToSPL.DiscountMaster = objSPLDetailToSPL.DiscountMaster
            tempSPLDetailToSPL.ModelID = objSPLDetailToSPL.ModelID
            tempSPLDetailToSPL.TotalDiscount = objSPLDetailToSPL.TotalDiscount
            tempSPLDetailToSPL.LabelTotal = objSPLDetailToSPL.LabelTotal
            tempSPLDetailToSPL.VechileTypeID = objSPLDetailToSPL.VechileTypeID
            tempSPLDetailToSPL.NumberRow = objSPLDetailToSPL.NumberRow
            arrReturnSPLDetailToSPL.Add(tempSPLDetailToSPL)
        Next

        Return arrReturnSPLDetailToSPL
    End Function

    Private Function getDeletedSPLDetail(ByVal arlSPLDetails As ArrayList, ByVal arlSPLDetailtoSPLs As ArrayList, ByVal newValidTo As DateTime) As ArrayList
        Dim arrSPLDetailDelete As ArrayList = New ArrayList()
        Dim arrSPLDetailToSPLDelete As ArrayList = New ArrayList()

        For i As Integer = 0 To arlSPLDetails.Count - 1
            Dim tempSPLDetail As SPLDetail = CType(arlSPLDetails(i), SPLDetail)
            Dim SPLDetailID As Integer = tempSPLDetail.ID
            Dim lastDay As Integer = Date.DaysInMonth(tempSPLDetail.PeriodYear, tempSPLDetail.PeriodMonth)
            Dim strDate As String = lastDay & "/" & tempSPLDetail.PeriodMonth.ToString("D2") & "/" & tempSPLDetail.PeriodYear
            Dim splDate As DateTime = Date.ParseExact(strDate, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
            If splDate > newValidTo Then
                arrSPLDetailDelete.Add(tempSPLDetail)
                If Not IsNothing(arlSPLDetailtoSPLs) AndAlso arlSPLDetailtoSPLs.Count > 0 Then
                    For j As Integer = 0 To arlSPLDetailtoSPLs.Count - 1
                        Dim oSPLDetailtoSPL As SPLDetailtoSPL = CType(arlSPLDetailtoSPLs(j), SPLDetailtoSPL)
                        If SPLDetailID = oSPLDetailtoSPL.SPLDetail.ID Then
                            arrSPLDetailToSPLDelete.Add(oSPLDetailtoSPL)
                        End If
                    Next
                End If
            End If
        Next

        Dim ret As ArrayList = New ArrayList()
        ret.Add(arrSPLDetailDelete)
        ret.Add(arrSPLDetailToSPLDelete)

        Return ret
    End Function

    Private Function getCurrentSPLDetail(ByVal arrSPLDetail As ArrayList, ByVal lastValidTo As DateTime)
        Dim arrReturn As ArrayList = New ArrayList()

        For Each item As SPLDetail In arrSPLDetail
            Dim strDate As String = item.PeriodMonth.ToString("D2") & "/" & item.PeriodYear

            If strDate = lastValidTo.ToString("MM/yyyy") Then
                arrReturn.Add(item)
            End If
        Next
        Return arrReturn
    End Function

    Private Function getSPLDetailByPeriode(ByVal arrSPLDetail As ArrayList, ByVal periode As DateTime) As ArrayList
        Dim arrReturn As ArrayList = New ArrayList()

        For item As Integer = 0 To arrSPLDetail.Count - 1
            Dim temp As SPLDetail = CType(arrSPLDetail(item), SPLDetail)
            If periode.ToString("MM/yyyy") = temp.PeriodMonth.ToString("D2") & "/" & temp.PeriodYear And temp.SisaQty > 0 Then
                arrReturn.Add(temp)
            End If
        Next

        Return arrReturn
    End Function

    Private Function getUnavailableSPLDetail(ByVal objSPL As SPL, ByVal arrSPLDetail As ArrayList, ByVal lastValidTo As DateTime, ByVal periodeGap As Integer) As ArrayList
        Dim unavailableSPLDetail As ArrayList = New ArrayList()
        Dim arrCurrentSPLDetail As ArrayList = getCurrentSPLDetail(arrSPLDetail, lastValidTo)
        Dim arrAdditionalSPLDetail As ArrayList = New ArrayList()
        Dim startMonth As DateTime = objSPL.ValidFrom
        Dim endMonth As DateTime = lastValidTo
        Dim gap As Integer = Math.Abs((startMonth.Month - endMonth.Month) + 12 * (startMonth.Year - endMonth.Year))
        For i As Integer = 0 To gap - 1
            Dim periode As DateTime = startMonth.AddMonths(i)
            Dim pastArrSPLDetail As ArrayList = getSPLDetailByPeriode(arrSPLDetail, periode)
            If pastArrSPLDetail.Count > 0 Then
                For j As Integer = 0 To pastArrSPLDetail.Count - 1
                    Dim isAvailable As Boolean = False
                    Dim pastSPLDetail As SPLDetail = CType(pastArrSPLDetail(j), SPLDetail)
                    For Each currentSPLDetail As SPLDetail In arrCurrentSPLDetail
                        If currentSPLDetail.VechileType.ID = pastSPLDetail.VechileType.ID Then
                            isAvailable = True
                        End If
                    Next
                    If Not isAvailable Then
                        Dim indexToRemove As Integer = 0
                        Dim isRemove As Boolean = False
                        For Each item As SPLDetail In unavailableSPLDetail
                            If item.VechileType.ID = pastSPLDetail.VechileType.ID Then
                                isRemove = True
                                Exit For
                            End If
                            indexToRemove = indexToRemove + 1
                        Next

                        If isRemove Then
                            unavailableSPLDetail.RemoveAt(indexToRemove)
                        End If

                        unavailableSPLDetail.Add(pastSPLDetail)
                    End If
                Next
            End If
        Next

        Return unavailableSPLDetail
    End Function

    Private Function getNewSPLDetail(ByVal arrSPLDetail As ArrayList, ByVal splDetailToSPL As SPLDetailtoSPL) As SPLDetail
        For Each item As SPLDetail In arrSPLDetail
            If item.ModelID = splDetailToSPL.ModelID And item.VechileType.ID = splDetailToSPL.VechileTypeID _
                And item.PeriodMonth = splDetailToSPL.PeriodMonth And item.PeriodYear - splDetailToSPL.PeriodYear And item.ID = 0 Then
                Return item
            End If
        Next
    End Function

    Private Function removeIdentic(ByVal objSPL As SPL, ByRef arrSPLDetailToSPL As ArrayList, ByVal lastValidTo As DateTime)
        Dim startMonth As DateTime = lastValidTo
        Dim endMonth As DateTime = objSPL.ValidTo
        Dim currentMonth As DateTime = startMonth

        While currentMonth.ToString("MMyyyy") <> endMonth.AddMonths(1).ToString("MMyyyy")
            Dim index As New ArrayList()
            For i As Integer = 0 To arrSPLDetailToSPL.Count - 1
                Dim obj As SPLDetailtoSPL = CType(arrSPLDetailToSPL(i), SPLDetailtoSPL)
                If obj.PeriodMonth = currentMonth.Month And obj.PeriodYear = currentMonth.Year And isDouble(obj, arrSPLDetailToSPL) Then
                    arrSPLDetailToSPL.RemoveAt(i)
                    Exit For
                    'Dim y As Integer = i
                    'index.Add(y)
                End If
            Next

            'If index.Count > 1 Then
            '    For i As Integer = 0 To index.Count - 2
            '        arrSPLDetailToSPL.RemoveAt(index(i))
            '    Next
            'End If
            currentMonth = currentMonth.AddMonths(1)
        End While
    End Function
#End Region

#Region "extend & duplicate function"

    Private Sub Copy(ByVal obj As Object, ByVal startDate As DateTime, ByVal endDate As DateTime)
    End Sub

    Private Function isDouble(ByVal obj1 As SPLDetailtoSPL, ByVal arrObj As ArrayList) As Boolean
        Dim count As Integer = 0
        For Each item As SPLDetailtoSPL In arrObj
            If obj1.ModelID = item.ModelID _
                And obj1.VechileTypeID = item.VechileTypeID _
                And item.DiscountMaster.ID = obj1.DiscountMaster.ID _
                And obj1.PeriodYear = item.PeriodYear _
                And obj1.PeriodMonth = item.PeriodMonth Then
                Dim strSPLID As String = ""
                Dim strSPLID2 As String = ""
                If Not IsNothing(item.SPLDetailReference) Then
                    If Not IsNothing(item.SPLDetailReference.SPL) Then
                        strSPLID = item.SPLDetailReference.ID.ToString
                    End If
                End If
                If Not IsNothing(obj1.SPLDetailReference) Then
                    If Not IsNothing(obj1.SPLDetailReference.SPL) Then
                        strSPLID2 = obj1.SPLDetailReference.ID.ToString
                    End If
                End If
                If strSPLID = strSPLID2 Then
                    count += 1
                    If count = 2 Then
                        Return True
                    End If
                End If
            End If
        Next
        Return False
    End Function

    Private Function extend(ByVal objSpl As SPL, ByVal arrSplDetail As ArrayList, ByVal arrSplDetailToSpl As ArrayList, ByVal lastValidTo As DateTime) As ArrayList

    End Function

#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            txtMaxTOPDay.Attributes.Add("readonly", "readonly")
            Initialize()
        End If
    End Sub
    Function GenerateNumberRow(ByVal Tobj As String, ByRef objArray As ArrayList)
        If Not IsNothing(objArray) AndAlso objArray.Count > 0 Then
            Dim i% = 0
            If Tobj.ToUpper = "SPLDETAILTOSPL" Then
                For Each obj As SPLDetailtoSPL In objArray
                    obj.NumberRow = i + 1
                    i += 1
                Next
            End If
            i = 0
        End If
    End Function
    Private Sub BindDetail(ByVal IDSPL As Integer)
        'Tampilkan berdasarkan periodmonth
        Dim oSPLDetailtoSPL As New SPLDetailtoSPL
        Dim arlSPLDetaillistTemp As New ArrayList
        'Dim arlSPLDetailToSPLlistTemp As New ArrayList

        Dim objSPL As SPL = New SPLFacade(User).Retrieve(IDSPL)
        Dim _sessArlSPLDetail As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
        Dim _sessArlSPLDetailToSPL As ArrayList = CType(sessHelper.GetSession("SPLDETAILTOSPLLIST"), ArrayList)

        _sessArlSPLDetailToSPL = New System.Collections.ArrayList(
                            (From obj As SPLDetailtoSPL In _sessArlSPLDetailToSPL.OfType(Of SPLDetailtoSPL)()
                                Order By obj.PeriodYear, obj.PeriodMonth, obj.ModelID, obj.VechileTypeID
                                Select obj).ToList())
        GenerateNumberRow("SPLDETAILTOSPL", _sessArlSPLDetailToSPL)

        _sessArlSPLDetail = New System.Collections.ArrayList(
                            (From obj As SPLDetail In _sessArlSPLDetail.OfType(Of SPLDetail)()
                                Order By obj.PeriodYear, obj.PeriodMonth, obj.ModelID, obj.VechileTypeID
                                Select obj).ToList())
        GenerateNumberRow("SPLDETAIL", _sessArlSPLDetail)

        If sessHelper.GetSession("STATUSMONTH") Is Nothing Then
            sessHelper.SetSession("STATUSMONTH", objSPL.ValidFrom)
            sessHelper.SetSession("STATUSMAXMONTH", objSPL.ValidTo)
            sessHelper.SetSession("STATUSMINMONTH", objSPL.ValidFrom)
            lblCurrentPeriode.Text = CType(objSPL.ValidFrom.Month - 1, enumMonth.Month).ToString.Replace("_", " ") + " " + objSPL.ValidFrom.Year.ToString()
        Else
            lblCurrentPeriode.Text = CType(CType(sessHelper.GetSession("STATUSMONTH"), Date).Month - 1, enumMonth.Month).ToString.Replace("_", " ") + " " + CType(sessHelper.GetSession("STATUSMONTH"), Date).Year.ToString()
        End If

        'For Each _sPLDetail As SPLDetail In _sessArlSPLDetail
        '    If Not IsNothing(_sPLDetail) Then
        '        If _sPLDetail.PeriodMonth = CType(sessHelper.GetSession("STATUSMONTH"), Date).Month And _sPLDetail.PeriodYear = CType(sessHelper.GetSession("STATUSMONTH"), Date).Year Then
        '            _sPLDetail.Discount = GetTotalPriceByVechileTypeID(_sPLDetail.ModelID, _sPLDetail.VechileTypeID, _sPLDetail.PeriodMonth, _sPLDetail.PeriodYear, _sessArlSPLDetailToSPL)
        '            arlSPLDetaillistTemp.Add(_sPLDetail)

        '            For Each objSPLDetailtoSPL As SPLDetailtoSPL In _sessArlSPLDetailToSPL
        '                If objSPLDetailtoSPL.VechileTypeID = _sPLDetail.VechileTypeID AndAlso objSPLDetailtoSPL.ModelID = _sPLDetail.ModelID AndAlso _
        '                    _sPLDetail.PeriodMonth = objSPLDetailtoSPL.PeriodMonth AndAlso _sPLDetail.PeriodYear = objSPLDetailtoSPL.PeriodYear Then
        '                    arlSPLDetailToSPLlistTemp.Add(objSPLDetailtoSPL)
        '                End If
        '            Next
        '        End If
        '    End If
        'Next

        Dim arlSPLDetailToSPLWithoutTotal As ArrayList = New ArrayList
        arlSPLDetailToSPLWithoutTotal = New System.Collections.ArrayList(
                                        (From obj As SPLDetailtoSPL In _sessArlSPLDetailToSPL.OfType(Of SPLDetailtoSPL)()
                                            Where obj.LabelTotal <> "TOTAL :" _
                                                And obj.PeriodMonth = CType(sessHelper.GetSession("STATUSMONTH"), Date).Month _
                                                And obj.PeriodYear = CType(sessHelper.GetSession("STATUSMONTH"), Date).Year _
                                            Order By obj.ModelID, obj.VechileTypeID
                                            Select obj).ToList())

        Dim strModelID As String = ""
        Dim strVechileTypeID As String = ""
        Dim strPeriodMonth As String = ""
        Dim strPeriodYear As String = ""

        Dim arlSPLDetailToSPLWithTotal As ArrayList = New ArrayList
        For i As Integer = 0 To arlSPLDetailToSPLWithoutTotal.Count - 1
            Dim oSPLDtoSPL As SPLDetailtoSPL = CType(arlSPLDetailToSPLWithoutTotal(i), SPLDetailtoSPL)
            If i = 0 Then
                strModelID = oSPLDtoSPL.ModelID
                strVechileTypeID = oSPLDtoSPL.VechileTypeID
                strPeriodMonth = oSPLDtoSPL.PeriodMonth
                strPeriodYear = oSPLDtoSPL.PeriodYear
            End If
            If strVechileTypeID <> oSPLDtoSPL.VechileTypeID OrElse strModelID <> oSPLDtoSPL.ModelID _
                OrElse strPeriodMonth <> oSPLDtoSPL.PeriodMonth OrElse strPeriodYear <> oSPLDtoSPL.PeriodYear Then
                oSPLDetailtoSPL = New SPLDetailtoSPL
                oSPLDetailtoSPL.SPLDetail = New SPLDetail
                oSPLDetailtoSPL.ModelID = strModelID
                oSPLDetailtoSPL.VechileTypeID = strVechileTypeID
                oSPLDetailtoSPL.PeriodMonth = strPeriodMonth
                oSPLDetailtoSPL.PeriodYear = strPeriodYear
                oSPLDetailtoSPL.LabelTotal = "TOTAL :"
                oSPLDetailtoSPL.TotalDiscount = GetTotalPriceByVechileTypeID(strModelID, strVechileTypeID, strPeriodMonth, strPeriodYear, arlSPLDetailToSPLWithoutTotal)
                arlSPLDetailToSPLWithTotal.Add(oSPLDetailtoSPL)
                strModelID = oSPLDtoSPL.ModelID
                strVechileTypeID = oSPLDtoSPL.VechileTypeID
                strPeriodMonth = oSPLDtoSPL.PeriodMonth
                strPeriodYear = oSPLDtoSPL.PeriodYear
            End If

            arlSPLDetailToSPLWithTotal.Add(oSPLDtoSPL)
            If i = arlSPLDetailToSPLWithoutTotal.Count - 1 Then
                oSPLDetailtoSPL = New SPLDetailtoSPL
                oSPLDetailtoSPL.SPLDetail = New SPLDetail
                oSPLDetailtoSPL.ModelID = oSPLDtoSPL.ModelID
                oSPLDetailtoSPL.VechileTypeID = oSPLDtoSPL.VechileTypeID
                oSPLDetailtoSPL.PeriodMonth = oSPLDtoSPL.PeriodMonth
                oSPLDetailtoSPL.PeriodYear = oSPLDtoSPL.PeriodYear
                oSPLDetailtoSPL.LabelTotal = "TOTAL :"
                oSPLDetailtoSPL.TotalDiscount = GetTotalPriceByVechileTypeID(strModelID, strVechileTypeID, strPeriodMonth, strPeriodYear, arlSPLDetailToSPLWithoutTotal)
                arlSPLDetailToSPLWithTotal.Add(oSPLDetailtoSPL)
            End If
        Next
        Dim dataList2 As ArrayList = New System.Collections.ArrayList(
                        (From obj As SPLDetailtoSPL In arlSPLDetailToSPLWithTotal.OfType(Of SPLDetailtoSPL)()
                            Order By obj.ModelID, obj.VechileTypeID
                            Select obj).ToList())

        GenerateNumberRow("SPLDETAILTOSPL", dataList2)

        sessHelper.SetSession("SPLDETAILLIST", _sessArlSPLDetail)
        sessHelper.SetSession("SPLDETAILTOSPLLIST", _sessArlSPLDetailToSPL)
        sessHelper.SetSession("SPLVIEW", dataList2)
        dgSPDetail.DataSource = dataList2
        dgSPDetail.DataBind()
    End Sub

    Private Sub UpdateDiscountSPLDetail(ByRef _oSPLDetail As SPLDetail, ByVal _arlSPLDetailtoSPLs As ArrayList)
        If Not IsNothing(_oSPLDetail) Then
            _oSPLDetail.Discount = GetTotalPriceByVechileTypeID(_oSPLDetail.ModelID, _oSPLDetail.VechileTypeID, _oSPLDetail.PeriodMonth, _oSPLDetail.PeriodYear, _arlSPLDetailtoSPLs)
        End If
    End Sub

    Private Function GetTotalPriceByVechileTypeID(ByVal strModelID As String, ByVal strVechileTypeID As String, ByVal strPeriodMonth As String, ByVal strPeriodYear As String, ByVal _sPLDetailToSPLlist As ArrayList) As Double
        Dim dblSumTotalDiscount As Double = 0
        If IsNothing(_sPLDetailToSPLlist) Then _sPLDetailToSPLlist = New ArrayList
        dblSumTotalDiscount = (From item As SPLDetailtoSPL In _sPLDetailToSPLlist
                                Where item.VechileTypeID = strVechileTypeID And item.ModelID = strModelID _
                                And item.PeriodMonth = strPeriodMonth And item.PeriodYear = strPeriodYear _
                                And item.LabelTotal <> "TOTAL :"
                                Select (item.Discount)).Sum()
        Return dblSumTotalDiscount
    End Function

    Private Function IsValidInstallment() As Boolean
        Dim nNum As Integer
        Dim IsValid As Boolean

        IsValid = False
        Try
            nNum = CType(Me.txtNumOfInstallment.Text, Integer)
            If nNum >= 1 Then
                IsValid = True
            End If
        Catch ex As Exception

        End Try
        If IsValid = False Then
            MessageBox.Show("Jumlah Installment Tidak Valid!")
            Return IsValid
        End If

        IsValid = False
        Try
            nNum = CType(Me.txtMaxTOPDay.Text, Integer)
            If nNum >= 1 Then
                IsValid = True
            End If
        Catch ex As Exception

        End Try
        If IsValid = False Then
            MessageBox.Show("Jumlah Hari TOP  Tidak Valid!")
        End If

    End Function

    Private Function UpdatePKTOPDay(ByVal SPLNumber As String, ByVal NumOfInstalment As Integer, ByVal MaxTOPDay As Integer) As Boolean
        Dim cPK As CriteriaComposite
        Dim oPKFac As New PKHeaderFacade(User)
        Dim aPKs As ArrayList
        Dim aPKChecks As ArrayList
        Dim Sql As String = ""
        Dim sPKIDs As String
        Dim oSPLinDB As SPL
        Dim oSPLFac As New SPLFacade(User)

        oSPLinDB = oSPLFac.Retrieve(SPLNumber)
        If Not IsNothing(oSPLinDB) AndAlso oSPLinDB.ID > 0 Then 'UPDATE
            If oSPLinDB.NumOfInstallment <> NumOfInstalment Then

                cPK = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cPK.opAnd(New Criteria(GetType(PKHeader), "SPLNumber", MatchType.Exact, SPLNumber))
                aPKs = oPKFac.Retrieve(cPK)
                If aPKs.Count > 0 Then
                    sPKIDs = ""
                    For i As Integer = 0 To aPKs.Count - 1
                        sPKIDs &= CType(aPKs(i), PKHeader).ID.ToString()
                        If i <> aPKs.Count - 1 Then
                            sPKIDs &= ","
                        End If
                    Next

                    Sql = " select count(1) n " & _
                        "   from POHeader poh with (nolock) " & _
                        "       join ContractHeader ch with (nolock) on ch.ID=poh.ContractHeaderID " & _
                        "       join PKHeader pkh with (nolock) on pkh.PKNumber=ch.PKNumber " & _
                        "   where 1=1 " & _
                        "       and poh.RowStatus=0 and ch.RowStatus=0 and pkh.RowStatus=0 " & _
                        "       and poh.Status in (0,2,4,6,8) " & _
                        "       and pkh.ID in (" & sPKIDs & ")"
                    Sql = "(" & Sql & ")"
                    cPK = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, Sql))
                    cPK.opAnd(New Criteria(GetType(PKHeader), "ID", MatchType.Exact, CType(aPKs(0), PKHeader).ID))
                    aPKChecks = oPKFac.Retrieve(cPK)
                    If aPKChecks.Count = 0 Then
                        MessageBox.Show("Jumlah Cicilan Tidak Bisa Dirubah. Sudah Ada PO Untuk Nomor SPL " & SPLNumber)
                        Return False
                    Else
                        'Update PK
                        For Each oPKH As PKHeader In aPKs
                            oPKH.MaxTopDay = MaxTOPDay
                            oPKH.MaxTopIndicator = 1
                            oPKH.ID = oPKFac.Update(oPKH)
                        Next
                    End If
                End If
            End If

            Return True
        Else
            MessageBox.Show("Data SPL " & SPLNumber & " Tidak Ada!")
            Return False
        End If
    End Function

    Function RemoveRowTotalDiscountOnGrid(ByVal arlSPLDetailToSPL As ArrayList) As ArrayList
        For i As Integer = 0 To arlSPLDetailToSPL.Count - 1
            Dim ObjSPLDetailtoSPL As SPLDetailtoSPL = CType(arlSPLDetailToSPL(i), SPLDetailtoSPL)
            If Not IsNothing(ObjSPLDetailtoSPL.LabelTotal) Then
                If ObjSPLDetailtoSPL.LabelTotal.ToLower = "total :" Then
                    arlSPLDetailToSPL.RemoveAt(i)
                    i -= 1
                End If
            End If
            If i = arlSPLDetailToSPL.Count - 1 Then
                Exit For
            End If
        Next
        Return arlSPLDetailToSPL
    End Function

    Private Sub lbtnDeleteLampiranPerhitunganDiskon_Click(sender As Object, e As EventArgs) Handles lbtnDeleteLampiranPerhitunganDiskon.Click
        Dim i% = 0
        Dim isExistDeleteFile As Boolean = False
        Dim oSPLDtlDocument As SPLDetailDocument
        Dim arlSPLDtlDocument As ArrayList = CType(sessHelper.GetSession("SPLDETAILDOCUMENTLIST"), ArrayList)
        If Not IsNothing(arlSPLDtlDocument) AndAlso arlSPLDtlDocument.Count = 0 Then Exit Sub
        If IsNothing(arlSPLDtlDocument) Then arlSPLDtlDocument = New ArrayList
        For Each objSPLDoc As SPLDetailDocument In arlSPLDtlDocument
            If objSPLDoc.FileType = 1 Then
                oSPLDtlDocument = objSPLDoc
                isExistDeleteFile = True
                Exit For
            End If
            i += 1
        Next
        If Not IsNothing(oSPLDtlDocument) AndAlso oSPLDtlDocument.ID > 0 Then
            Dim arrDelete As ArrayList = CType(sessHelper.GetSession("DELETESPLDETAILDOCUMENTLIST"), ArrayList)
            If IsNothing(arrDelete) Then arrDelete = New ArrayList
            arrDelete.Add(oSPLDtlDocument)
            sessHelper.SetSession("DELETESPLDETAILDOCUMENTLIST", arrDelete)
        End If
        If isExistDeleteFile Then
            arlSPLDtlDocument.RemoveAt(i)
            txtLampiranPerhitunganDiskon.Text = ""
        End If
        sessHelper.SetSession("SPLDETAILDOCUMENTLIST", arlSPLDtlDocument)
    End Sub

    Private Sub btnUploadLampiranPerhitunganDiskon_Click(sender As Object, e As EventArgs) Handles btnUploadLampiranPerhitunganDiskon.Click
        Dim arlSPLDtlDocument As ArrayList
        Dim objPostedData As HttpPostedFile
        Dim sFileName As String
        Dim objSPLDetailDocument As SPLDetailDocument = New SPLDetailDocument()

        If IsNothing(FULampiranPerhitunganDiskon) OrElse FULampiranPerhitunganDiskon.Value = String.Empty Then
            MessageBox.Show("Lampiran masih kosong")
            Return
        End If
        Dim _filename As String = System.IO.Path.GetFileName(FULampiranPerhitunganDiskon.PostedFile.FileName)
        If _filename.Trim().Length <= 0 Then
            MessageBox.Show("Upload file belum diisi\n")
            Return
        End If
        If _filename.Trim().Length > 0 Then
            If FULampiranPerhitunganDiskon.PostedFile.ContentLength > MAX_FILE_SIZE Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                Return
            End If
        End If
        Dim ext As String = System.IO.Path.GetExtension(FULampiranPerhitunganDiskon.PostedFile.FileName)
        If Not (ext.ToUpper() = ".PDF") Then
            MessageBox.Show("Hanya menerima file format (*.PDF)")
            Return
        End If

        lbtnDeleteLampiranPerhitunganDiskon_Click(Nothing, Nothing)

        If Not IsNothing(FULampiranPerhitunganDiskon) OrElse FULampiranPerhitunganDiskon.Value <> String.Empty Then
            objPostedData = FULampiranPerhitunganDiskon.PostedFile
        Else
            objPostedData = Nothing
        End If

        If Not (IsNothing(objPostedData)) Then
            sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

            If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                Return
            End If

            Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
            Dim strSPLPathConfig As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")
            Dim strSPLPathFile As String = "DiscountProposal\Non Fleet\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
            Dim strDestFile As String = strSPLPathConfig & strSPLPathFile '--Destination File                       

            Dim idSPL As Integer = CInt(sessHelper.GetSession("IDSPLHeader"))
            Dim ObjSPL As SPL = _SPLFacade.Retrieve(idSPL)
            If IsNothing(ObjSPL) Then ObjSPL = New SPL

            With objSPLDetailDocument
                .SPL = ObjSPL
                .AttachmentData = objPostedData
                .FileType = 1  '---> Lampiran SPL
                .FileName = sFileName
                .Path = strSPLPathFile
            End With
            txtLampiranPerhitunganDiskon.Text = objPostedData.FileName
            If txtLampiranPerhitunganDiskon.Text.Trim <> "" Then
                lbtnDeleteLampiranPerhitunganDiskon.Visible = True
            Else
                lbtnDeleteLampiranPerhitunganDiskon.Visible = False
            End If

            UploadAttachment(objSPLDetailDocument, strTempDirectory)

            arlSPLDtlDocument = CType(sessHelper.GetSession("SPLDETAILDOCUMENTLIST"), ArrayList)
            arlSPLDtlDocument.Add(objSPLDetailDocument)
        Else
            MessageBox.Show(SR.DataNotFound("Attachment File"))
        End If
    End Sub

    Sub UploadAttachment(ByVal ObjAttachment As SPLDetailDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(ObjAttachment.AttachmentData) Then
                    If Not IsNothing(ObjAttachment.Path) Then
                        finfo = New FileInfo(TargetPath + ObjAttachment.Path)

                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.Path)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lbtnDeleteAttachment_Click(sender As Object, e As EventArgs) Handles lbtnDeleteAttachment.Click
        txtAttachment.Text = ""
        lblAttachment.Text = ""
        If txtAttachment.Text.Trim <> "" Then
            lbtnDeleteAttachment.Visible = True
        Else
            lbtnDeleteAttachment.Visible = False
        End If
        sessHelper.SetSession("FrmSPLDetail.Attachment", Nothing)
    End Sub

    Private Sub btnUploadAttachment_Click(sender As Object, e As EventArgs) Handles btnUploadAttachment.Click
        Dim objPostedData As HttpPostedFile
        Dim sFileName As String

        If IsNothing(FUAttachment) OrElse FUAttachment.Value = String.Empty Then
            MessageBox.Show("Lampiran masih kosong")
            Return
        End If
        Dim _filename As String = System.IO.Path.GetFileName(FUAttachment.PostedFile.FileName)
        If _filename.Trim().Length <= 0 Then
            MessageBox.Show("Upload file belum diisi\n")
            Return
        End If
        If _filename.Trim().Length > 0 Then
            If FUAttachment.PostedFile.ContentLength > MAX_FILE_SIZE Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                Return
            End If
        End If
        Dim ext As String = System.IO.Path.GetExtension(FUAttachment.PostedFile.FileName)
        If Not (ext.ToUpper() = ".DOC") AndAlso _
            Not (ext.ToUpper() = ".DOCX") AndAlso _
            Not (ext.ToUpper() = ".XLS") AndAlso _
            Not (ext.ToUpper() = ".XLSX") AndAlso _
            Not (ext.ToUpper() = ".PDF") AndAlso _
            Not (ext.ToUpper() = ".ZIP") AndAlso _
            Not (ext.ToUpper() = ".RAR") Then
            MessageBox.Show("Hanya menerima file format (*.DOC, *.PDF, *.XLS, *.ZIP, *.RAR)")
            Return
        End If

        lbtnDeleteAttachment_Click(Nothing, Nothing)
        If Not IsNothing(FUAttachment) OrElse FUAttachment.Value <> String.Empty Then
            objPostedData = FUAttachment.PostedFile
        Else
            objPostedData = Nothing
        End If

        If Not (IsNothing(objPostedData)) Then
            sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

            If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                Return
            End If

            txtAttachment.Text = objPostedData.FileName
            lblAttachment.Text = objPostedData.FileName
            If txtAttachment.Text.Trim <> "" Then
                lbtnDeleteAttachment.Visible = True
            Else
                lbtnDeleteAttachment.Visible = False
            End If
            sessHelper.SetSession("FrmSPLDetail.Attachment", objPostedData)
        Else
            MessageBox.Show(SR.DataNotFound("Attachment File"))
        End If
    End Sub

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Private Function ValidateSaveData() As String
        Dim sb As StringBuilder = New StringBuilder

        If (txtSPLNumber.Text.Trim = String.Empty) Then
            sb.Append("- Nomor Aplikasi Harus Diisi\n")
        End If
        If (txtDescription.Text.Trim = String.Empty) Then
            sb.Append("- Deskripsi Harus Diisi\n")
        End If
        If (txtDealerName.Text.Trim = String.Empty) Then
            sb.Append("- Nama Dealer Harus Diisi\n")
        End If
        If (txtCustName.Text.Trim = String.Empty) Then
            sb.Append("- Nama Customer Harus Diisi\n")
        End If
        If txtValidFrom.Text.Trim = "" Then
            sb.Append("- Periode Tebus Dari Harus Diisi\n")
        End If
        If txtValidTo.Text.Trim = "" Then
            sb.Append("- Periode Tebus Sampai Harus Diisi\n")
        End If

        Return sb.ToString()
    End Function

    Private Sub btnSave2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave2.Click
        Dim _message As String = ""
        Dim sValid As String = IsMonthYearValid()
        Dim arlSPLDetail As New ArrayList
        Dim arlSPLDetailToSPL As New ArrayList
        Dim i As Integer
        If sValid.Trim().Length > 0 Then
            MessageBox.Show(sValid)
            Return
        End If
        If ValidateDealers(txtDealerName.Text.Trim) Then
            Dim Str As String = ValidateSaveData()
            If (Str.Length > 0) Then
                MessageBox.Show(Str)
                Exit Sub
            End If

            arlSPLDetail = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
            If IsNothing(arlSPLDetail) Then arlSPLDetail = New ArrayList
            arlSPLDetailToSPL = CType(sessHelper.GetSession("SPLDETAILTOSPLLIST"), ArrayList)
            If IsNothing(arlSPLDetailToSPL) Then arlSPLDetailToSPL = New ArrayList

            'If arlSPLDetail Then

            If Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then
                If IsExistSPLNumber(txtSPLNumber.Text.Trim()) Then
                    MessageBox.Show(SR.DataIsExist("SPLNumber"))
                Else
                    'Remove row Total grid 
                    arlSPLDetailToSPL = RemoveRowTotalDiscountOnGrid(arlSPLDetailToSPL)

                    Dim arlSPLDealers As New ArrayList
                    GetValueToDatabase(ObjSPL)
                    i = 0
                    For Each item As String In txtDealerName.Text.Split(";")
                        If i < txtDealerName.Text.Split(";").Length - 1 Or i = 0 Then
                            Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(item)
                            Dim objSPLDealer As SPLDealer = New SPLDealer
                            objSPLDealer.SPL = New SPL
                            objSPLDealer.Dealer = objDealerTmp
                            arlSPLDealers.Add(objSPLDealer)
                        End If
                        i = i + 1
                    Next

                    Dim arlSPLDtlDocument As ArrayList = CType(sessHelper.GetSession("SPLDETAILDOCUMENTLIST"), ArrayList)
                    If IsNothing(arlSPLDtlDocument) Then arlSPLDtlDocument = New ArrayList

                    If UploadFile(ObjSPL, _message) = 1 Then
                        Dim _result As Integer = _SPLFacade.InsertSPL(ObjSPL, arlSPLDetail, arlSPLDetailToSPL, arlSPLDealers, arlSPLDtlDocument)
                        If _result = -1 Then
                            MessageBox.Show(SR.SaveFail)
                        Else
                            CommitAttachment(arlSPLDtlDocument)

                            sessHelper.SetSession("SPLDETAILTOSPLLIST", arlSPLDetailToSPL)
                            Dim strMessageGeneratePDF As String = ""
                            If _result > 0 Then
                                Dim strFileName As String, strDestFile As String
                                Dim objSPLDetailDocument As SPLDetailDocument
                                arlSPLDtlDocument = New ArrayList
                                strFileName = ""
                                strDestFile = ""
                                Dim strResult As String = GeneratePDFtoGW(_result, strFileName, strDestFile)
                                If strResult <> "" Then
                                    strMessageGeneratePDF = "\n- [Gagal Generate File PDF]\n"
                                    strMessageGeneratePDF += "- " & strResult
                                Else
                                    objSPLDetailDocument = New SPLDetailDocument
                                    With objSPLDetailDocument
                                        .SPL = ObjSPL
                                        .FileType = 0  '---> Generate File PDF to Groupware
                                        .FileName = strFileName
                                        .Path = strDestFile
                                    End With
                                    arlSPLDtlDocument.Add(objSPLDetailDocument)
                                End If

                                _result = New SPLFacade(User).InsertTransactionGenerateFiletoGW(ObjSPL, arlSPLDtlDocument)
                            End If

                            RemoveALLSession()
                            sessHelper.SetSession("Status", "Update")
                            sessHelper.SetSession("IDSPLHeader", ObjSPL.ID)
                            Dim strJs As String = String.Empty
                            strJs = "alert('Simpan Data Berhasil" & strMessageGeneratePDF & "');"
                            strJs += "window.location = '../FinishUnit/FrmSPLDetail.aspx';"
                            System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
                        End If
                    Else
                        MessageBox.Show("File gagal disimpan di Server.\n" & _message)
                    End If
                End If

            ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
                Dim idSPL As Integer = CInt(sessHelper.GetSession("IDSPLHeader"))
                Dim ObjSPL As SPL = _SPLFacade.Retrieve(idSPL)
                If ddlStatus.SelectedValue = EnumStatusSPL.StatusSPL.Tidak_Aktif Then
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(PKHeader), "SPLNumber", MatchType.Exact, ObjSPL.SPLNumber))
                    Dim x As Integer = New PKHeaderFacade(User).Retrieve(criterias).Count
                    If x > 0 Then
                        MessageBox.Show("Aplikasi sudah di gunakan di Pesanan Kendaraan")
                        Return
                    End If
                End If

                Dim _oldSPLDetail As ArrayList = CType(sessHelper.GetSession("OLDSPLDETAILTOSPLLIST"), ArrayList)
                Dim _oldSPLDealer As ArrayList = CType(sessHelper.GetSession("OLDSPLDealer"), ArrayList)

                ''remark by ali
                'If Not _oldSPLDetail Is Nothing Then
                '    Dim _delDetail As Integer = _SPLFacade.DeleteOLDSPLDetail(_oldSPLDetail)
                'End If

                'If Not _oldSPLDealer Is Nothing Then
                '    Dim _delDealer As Integer = _SPLFacade.DeleteOLDDealer(_oldSPLDealer)
                'End If
                ''remark by ali

                Dim arlDelSPLDetail As ArrayList = CType(sessHelper.GetSession("DELETESPLDETAILLIST"), ArrayList)
                If IsNothing(arlDelSPLDetail) Then arlDelSPLDetail = New ArrayList
                Dim arlDelSPLDetailToSPL As ArrayList = CType(sessHelper.GetSession("DELETESPLDETAILTOSPLLIST"), ArrayList)
                If IsNothing(arlDelSPLDetailToSPL) Then arlDelSPLDetailToSPL = New ArrayList

                Dim _splDealers As New ArrayList
                i = 0
                If CInt(txtNumOfInstallment.Text) > 1 AndAlso arlSPLDetailToSPL.Count = 0 Then
                    MessageBox.Show("Minimum Installment  1 jenis kendaraan")
                    Return
                End If

                If CInt(txtNumOfInstallment.Text) = 1 Then
                    txtMaxTOPDay.Text = 0
                End If 

                Dim TempDealer As Dealer
                For Each item As String In txtDealerName.Text.Split(";")
                    If i < txtDealerName.Text.Split(";").Length - 1 Then
                        Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(item)
                        Dim objSPLDealer As SPLDealer = New SPLDealer
                        objSPLDealer.SPL = ObjSPL
                        objSPLDealer.Dealer = objDealerTmp
                        _splDealers.Add(objSPLDealer)
                        If IsNothing(TempDealer) OrElse TempDealer.ID <= 0 Then
                            TempDealer = objDealerTmp
                        End If
                    End If
                    i = i + 1
                Next

                Try
                    If CInt(txtNumOfInstallment.Text) > 1 Then
                        txtMaxTOPDay.Text = GetTOPInstallment(TempDealer.ID, arlSPLDetail(0), CInt(txtNumOfInstallment.Text))
                    End If
                Catch ex As Exception
                    MessageBox.Show("Harga Tidak memiliki Interest")
                    Return
                End Try

                ''Remove row Total grid 
                'arlSPLDetailToSPL = RemoveRowTotalDiscountOnGrid(arlSPLDetailToSPL)
                arlSPLDetailToSPL = New System.Collections.ArrayList(
                                                (From obj As SPLDetailtoSPL In arlSPLDetailToSPL.OfType(Of SPLDetailtoSPL)()
                                                    Where obj.LabelTotal.ToLower <> "total :"
                                                    Order By obj.ModelID, obj.VechileTypeID, obj.PeriodMonth, obj.PeriodYear
                                                    Select obj).ToList())

                Dim lastValidTo As DateTime = ObjSPL.ValidTo
                GetValueToDatabase(ObjSPL)

                If ddlStatus.SelectedValue = EnumStatusSPL.StatusSPL.Tidak_Aktif Then
                    ObjSPL.Status = EnumStatusSPL.StatusSPL.Tidak_Aktif
                End If

                Dim arlSPLDtlDocument As ArrayList = CType(sessHelper.GetSession("SPLDETAILDOCUMENTLIST"), ArrayList)
                Dim arlDelSPLDtlDocument As ArrayList = CType(sessHelper.GetSession("DELETESPLDETAILDOCUMENTLIST"), ArrayList)
                If IsNothing(arlSPLDtlDocument) Then arlSPLDtlDocument = New ArrayList
                If IsNothing(arlDelSPLDtlDocument) Then arlDelSPLDtlDocument = New ArrayList

                If UploadFile(ObjSPL, _message) = 1 Then
                    'perubahan pada validTo
                    If lastValidTo < ObjSPL.ValidTo Then
                        Dim arrDetailWithSisaQty As ArrayList = setSisaQty(arlSPLDetail)
                        Dim copyResult As ArrayList = copyDetail(ObjSPL, arrDetailWithSisaQty, arlSPLDetailToSPL, lastValidTo)
                        arlSPLDetail.AddRange(copyResult(0))
                        arlSPLDetailToSPL.AddRange(copyResult(1))
                    ElseIf lastValidTo > ObjSPL.ValidTo Then
                        Dim deleteResult As ArrayList = getDeletedSPLDetail(arlSPLDetail, arlSPLDetailToSPL, ObjSPL.ValidTo)
                        arlDelSPLDetail.AddRange(deleteResult(0))
                        arlDelSPLDetailToSPL.AddRange(deleteResult(1))
                    End If

                    Dim _result As Integer = _SPLFacade.UpdateSPL(ObjSPL, arlSPLDetail, arlDelSPLDetail, arlSPLDetailToSPL, arlDelSPLDetailToSPL, _splDealers, _oldSPLDealer, arlSPLDtlDocument, arlDelSPLDtlDocument)
                    If _result = -1 Then
                        MessageBox.Show(SR.UpdateFail)
                    Else
                        CommitAttachment(arlSPLDtlDocument)

                        'SetSessionSPLDetailFromDB(ObjSPL)
                        sessHelper.SetSession("IDSPLHeader", ObjSPL.ID)
                        sessHelper.SetSession("SPLDETAILTOSPLLIST", arlSPLDetailToSPL)

                        Dim strMessageGeneratePDF As String = ""
                        If _result > 0 Then
                            Dim strFileName As String, strDestFile As String
                            Dim objSPLDetailDocument As SPLDetailDocument
                            arlSPLDtlDocument = New ArrayList
                            strFileName = ""
                            strDestFile = ""
                            Dim strResult As String = GeneratePDFtoGW(_result, strFileName, strDestFile)
                            If strResult <> String.Empty Then
                                strMessageGeneratePDF = " - [Gagal Generate File PDF]\n"
                                strMessageGeneratePDF += " - " & strResult
                            Else
                                objSPLDetailDocument = New SPLDetailDocument
                                With objSPLDetailDocument
                                    .SPL = ObjSPL
                                    .FileType = 0  '---> Generate File PDF to Groupware
                                    .FileName = strFileName
                                    .Path = strDestFile
                                End With
                                arlSPLDtlDocument.Add(objSPLDetailDocument)
                            End If

                            _result = New SPLFacade(User).InsertTransactionGenerateFiletoGW(ObjSPL, arlSPLDtlDocument)
                        End If

                        RemoveALLSession()
                        sessHelper.SetSession("Status", "Update")
                        'sessHelper.SetSession("STATUSMONTH", ObjSPL.ValidFrom)
                        sessHelper.SetSession("IDSPLHeader", ObjSPL.ID)

                        Dim strJs As String = String.Empty
                        strJs = "alert('Simpan Data Berhasil" & strMessageGeneratePDF & "');"
                        strJs += "window.location = '../FinishUnit/FrmSPLDetail.aspx';"
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
                    End If
                Else
                    MessageBox.Show("File gagal disimpan di Server.\n" & _message)
                End If
            End If
        End If
    End Sub

    Private Sub lnkAppointmentLetter_Click(sender As Object, e As EventArgs) Handles lnkAppointmentLetter.Click
        Dim strFileName As String, strDestFile As String
        If Not IsNothing(sessHelper.GetSession("IDSPLHeader")) Then
            If CInt(sessHelper.GetSession("IDSPLHeader")) <> 0 Then
                If GeneratePDFtoGW(CInt(sessHelper.GetSession("IDSPLHeader")), strFileName, strDestFile) Then
                    MessageBox.Show("Sukses Generate File PDF")
                Else
                    MessageBox.Show("Gagal Generate File PDF")
                End If
            End If
        End If
    End Sub

    Private Sub CommitAttachment(ByVal AttachmentCollection As ArrayList)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim TargetFInfo As FileInfo
        Dim TempFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(AttachmentCollection) Then
                    For Each obj As SPLDetailDocument In AttachmentCollection
                        If Not IsNothing(obj.AttachmentData) Then
                            TargetFInfo = New FileInfo(strTargetDirectory + obj.Path)
                            TempFInfo = New FileInfo(strTempDirectory + obj.Path)

                            If TempFInfo.Exists Then
                                If Not TargetFInfo.Directory.Exists Then
                                    Directory.CreateDirectory(TargetFInfo.DirectoryName)
                                End If
                                TempFInfo.MoveTo(TargetFInfo.FullName)
                            End If
                            obj.AttachmentData.SaveAs(strTargetDirectory + obj.Path)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GeneratePDFtoGW(ByVal id As Integer, ByRef strFileName As String, ByRef strDestFile As String) As String
        Dim result As String = String.Empty
        Try
            Dim data As SPL = New SPLFacade(User).Retrieve(id)
            Dim newLine As String = Environment.NewLine

            Dim filePath As String = Server.MapPath("~\DataFile\Template\DP\Non_Fleet_Discount_Template.docx")
            Dim directoryTemp As String = Server.MapPath("~\DataFile\Template\DP\")
            Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directoryTemp)
            If Not directoryInfo.Exists Then
                directoryInfo.Create()
            End If
            Dim finfo As FileInfo = New FileInfo(filePath)
            If Not finfo.Exists Then
                result = "File template non fleet diskon tidak ditemukan"
                Return result
            End If

            Dim filebytes As Byte() = File.ReadAllBytes(filePath)

            Using Stream As MemoryStream = New MemoryStream()
                Stream.Write(filebytes, 0, filebytes.Length)
                Using doc As WordprocessingDocument = WordprocessingDocument.Open(Stream, True)
                    Dim body As DocumentFormat.OpenXml.Wordprocessing.Body = doc.MainDocumentPart.Document.Body
                    Dim tables As List(Of DocumentFormat.OpenXml.Wordprocessing.Table) = body.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Table)().ToList()
                    For Each table As DocumentFormat.OpenXml.Wordprocessing.Table In tables
                        Dim rows As List(Of DocumentFormat.OpenXml.Wordprocessing.TableRow) = table.Elements(Of DocumentFormat.OpenXml.Wordprocessing.TableRow)().ToList()
                        For Each row As DocumentFormat.OpenXml.Wordprocessing.TableRow In rows
                            For Each cell As DocumentFormat.OpenXml.Wordprocessing.TableCell In row.Descendants(Of DocumentFormat.OpenXml.Wordprocessing.TableCell)().Where(Function(x) x.InnerText.Contains("_"))
                                Dim texts As List(Of DocumentFormat.OpenXml.Wordprocessing.Text) = cell.SelectMany(Function(p) p.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Run)()).SelectMany(Function(r) r.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Text)()).ToList()
                                Dim word As DocumentFormat.OpenXml.Wordprocessing.Text = New DocumentFormat.OpenXml.Wordprocessing.Text
                                Dim wordGabungan As String = ""
                                Dim i% = 0
                                For Each word2 As DocumentFormat.OpenXml.Wordprocessing.Text In texts
                                    wordGabungan += word2.InnerText
                                    If texts.Count > 1 Then
                                        If i = 0 Then
                                            word2.Text = ""
                                        End If
                                    End If
                                    word = word2
                                    i += 1
                                Next
                                word.Text = wordGabungan
                                If word.Text.Contains("_") Then
                                    Select Case word.Text.ToLower
                                        Case "submit_date"
                                            word.Text = data.CreatedTime.ToString("dd MMMM yyyy")
                                        Case "app_regno"
                                            word.Text = data.SPLNumber
                                        Case "dealer_code"
                                            word.Text = data.DealerName
                                        Case "disc_description"
                                            word.Text = data.Description
                                        Case "customer_name"
                                            word.Text = data.CustomerName
                                        Case "mmksi_comment"
                                            word.Text = data.Comment
                                    End Select
                                End If
                            Next
                        Next
                    Next
                End Using
                Dim bytes As Byte() = Stream.ToArray()

                Dim tempPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + "DataTemp\SPL\" + Guid.NewGuid().ToString().Substring(0, 5) + ".docx"
                UploadDocXFile(bytes, tempPath)
                CreateTableinWord(tempPath, strFileName, strDestFile)
            End Using

            result = String.Empty
        Catch ex As Exception
            result = ex.Message
        End Try
        Return result

    End Function

    Private Sub CreateTableinWord(ByVal tempPath As String, ByRef strFileName As String, ByRef strDestFile As String)
        'Load Document
        Dim doc As New Document()
        Dim filePath As String = tempPath
        doc.LoadFromFile(filePath)

        CreateTableProposedDiscount(doc, 0)

        ''Save and Launch
        'tempPath2 = KTB.DNet.Lib.WebConfig.GetValue("SAN") + "DataTemp\SPL\" + Guid.NewGuid().ToString().Substring(0, 5) + ".docx"
        'doc.SaveToFile(tempPath2, Spire.Doc.FileFormat.Docx)
        DownloadPdfFile(doc, strFileName, strDestFile)
    End Sub

    Private Sub DownloadPdfFile(ByVal doc As Document, ByRef strFileName As String, ByRef strDestFile As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim success As Boolean = False
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            success = imp.Start()
            If success Then
                strFileName = "SPL_" & hdnSPLID.Value & "_" & Now.Year.ToString & Now.Month.ToString("d2") & Now.Day.ToString("d2") & "_" & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & ".pdf"
                strDestFile = "DiscountProposal\Non Fleet\" & strFileName
                'Dim document As Document = New Document()
                'document.LoadFromFile(tempPath)
                doc.SaveToFile(strTargetDirectory & strDestFile, Spire.Doc.FileFormat.PDF)

                imp.StopImpersonate()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GenerateArraySPLDetailToSPLWithTotal()
        Dim intSPLID As Integer = sessHelper.GetSession("IDSPLHeader")
        Dim _arlSPLDetailToSPL As ArrayList = New SPLDetailtoSPLFacade(User).RetrieveFromSP(intSPLID)
        If _arlSPLDetailToSPL.Count > 0 Then
            sessHelper.SetSession("SPLDETAILTOSPLLIST", _arlSPLDetailToSPL)
        End If
    End Function

    Function CreateTableProposedDiscount(ByRef doc As Document, ByVal j As Integer)
        Dim section As SpireDoc.Section = doc.Sections(j)
        Dim selection As SpireDoc.Documents.TextSelection = doc.FindString("gridData_Proposed_Discount", True, True)
        Dim range As TextRange = selection.GetAsOneRange()
        Dim paragraph As SpireDoc.Documents.Paragraph = range.OwnerParagraph
        Dim body As SpireDoc.Body = paragraph.OwnerTextBody
        Dim index As Integer = body.ChildObjects.IndexOf(paragraph)
        Dim table As SpireDoc.Table = section.AddTable(True)

        Dim strLabelTotalDiscount As String = "TOTAL :"

        'Create Header and Data
        Dim Header() As String = {"No", "Model", "Tipe", "Unit", "Discount Type", "Application No.", "Detail Discount", "Price Reff", "TOP", "Interest", "Delivery Time"}

        GenerateArraySPLDetailToSPLWithTotal()

        Dim arlSPLDtltoSPL As ArrayList = CType(sessHelper.GetSession("SPLDETAILTOSPLLIST"), ArrayList)
        If IsNothing(arlSPLDtltoSPL) Then arlSPLDtltoSPL = New ArrayList()

        Dim arlSPLDetail As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
        If IsNothing(arlSPLDetail) Then arlSPLDetail = New ArrayList

        table.ResetCells(arlSPLDtltoSPL.Count + 1, Header.Length)
        table.TableFormat.WrapTextAround = True
        table.TableFormat.Positioning.HorizPositionAbs = SpireDoc.HorizontalPosition.Outside
        table.TableFormat.Positioning.VertRelationTo = SpireDoc.VerticalRelation.Margin
        table.TableFormat.Positioning.VertPosition = 10
        Dim width As SpireDoc.PreferredWidth = New SpireDoc.PreferredWidth(SpireDoc.WidthType.Percentage, 100)
        table.PreferredWidth = width

        'Header Row
        Dim FRow As SpireDoc.TableRow = table.Rows(0)
        FRow.IsHeader = True
        'Row Height
        FRow.Height = 20
        'Header Format
        FRow.RowFormat.BackColor = Color.PaleTurquoise
        For i As Integer = 0 To Header.Length - 1
            'Cell Alignment
            Dim p As SpireDoc.Documents.Paragraph = FRow.Cells(i).AddParagraph()
            FRow.Cells(i).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
            p.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Center
            'Data Format
            Dim TR As TextRange = p.AppendText(Header(i))
            TR.CharacterFormat.FontName = "MMC Office"
            TR.CharacterFormat.FontSize = 10
            TR.CharacterFormat.TextColor = Color.Teal
            TR.CharacterFormat.Bold = True
            If i = 0 Then
                FRow.Cells(i).Width = 15
            End If
        Next i

        'Data Row
        Dim oSPLDetailtoSPL As SPLDetailtoSPL
        Dim strVechileType As String = ""
        Dim strPeriodMonth As String = ""
        Dim strPeriodYear As String = ""
        Dim q As Integer = 0
        For r As Integer = 0 To arlSPLDtltoSPL.Count - 1
            oSPLDetailtoSPL = CType(arlSPLDtltoSPL(r), SPLDetailtoSPL)
            Dim DataRow As SpireDoc.TableRow = table.Rows(r + 1)

            'Row Height
            DataRow.Height = 20
            Dim cols(Header.Length) As String

            Dim oSPLDetail As SPLDetail
            If Not IsNothing(oSPLDetailtoSPL.SPLDetail) Then
                oSPLDetail = oSPLDetailtoSPL.SPLDetail
            Else
                oSPLDetail = New SPLDetail
            End If

            Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
            Dim strLabelTotal As String = If(Not IsNothing(oSPLDetailtoSPL.LabelTotal), oSPLDetailtoSPL.LabelTotal, "")
            If strLabelTotal.Trim.ToLower <> strLabelTotalDiscount.ToLower Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, oSPLDetail.VechileType.VechileModel.ID))
                Dim arrSCVToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
                If Not IsNothing(arrSCVToModel) AndAlso arrSCVToModel.Count > 0 Then
                    objSubCategoryVehicleToModel = CType(arrSCVToModel(0), SubCategoryVehicleToModel)
                End If

                If strVechileType.ToLower.Trim <> oSPLDetail.VechileType.VechileTypeCode.ToLower.Trim OrElse _
                   strPeriodMonth.ToLower.Trim <> oSPLDetail.PeriodMonth.ToString.ToLower.Trim OrElse _
                   strPeriodYear.ToLower.Trim <> oSPLDetail.PeriodYear.ToString.ToLower.Trim Then
                    strVechileType = oSPLDetail.VechileType.VechileTypeCode
                    strPeriodMonth = oSPLDetail.PeriodMonth
                    strPeriodYear = oSPLDetail.PeriodYear
                    q += 1
                End If

                cols(0) = q
                cols(1) = objSubCategoryVehicleToModel.SubCategoryVehicle.Name
                cols(2) = oSPLDetail.VechileType.VechileTypeCode & " (" & oSPLDetail.VechileType.Description & ")"
                cols(3) = oSPLDetail.Quantity
                cols(4) = oSPLDetailtoSPL.DiscountMaster.Category
                cols(5) = If(Not IsNothing(oSPLDetailtoSPL.SPLDetailReference), If(Not IsNothing(oSPLDetailtoSPL.SPLDetailReference.SPL), oSPLDetailtoSPL.SPLDetailReference.SPL.SPLNumber, ""), "")
                cols(6) = Format(oSPLDetailtoSPL.Discount, "#,##0")
                cols(7) = If(oSPLDetail.PriceRefDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value, "Update Price", oSPLDetail.PriceRefDate.ToString("MMyyyy"))
                cols(8) = oSPLDetail.MaxTopDay
                cols(9) = If(SPLEnum.GetStringValue(oSPLDetail.FreeIntIndicator) <> "", SPLEnum.GetStringValue(oSPLDetail.FreeIntIndicator), "")
                cols(10) = enumMonthGet.GetName(oSPLDetail.DeliveryDate.Month) & " " & oSPLDetail.DeliveryDate.Year.ToString("d4")
            Else
                cols(0) = strLabelTotalDiscount & " "
                cols(1) = ""
                cols(2) = ""
                cols(3) = ""
                cols(4) = ""
                cols(5) = ""
                cols(6) = Format(oSPLDetailtoSPL.TotalDiscount, "#,##0")
                cols(7) = ""
                cols(8) = ""
                cols(9) = ""
                cols(10) = ""
            End If

            'C Represents Column.
            For c As Integer = 0 To Header.Length - 1
                'Cell Alignment
                DataRow.Cells(c).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
                'Fill Data in Rows
                Dim p2 As SpireDoc.Documents.Paragraph = DataRow.Cells(c).AddParagraph()
                Dim TR2 As TextRange = p2.AppendText(cols(c))
                'Format Cells
                p2.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Center
                TR2.CharacterFormat.FontName = "MMC Office"
                TR2.CharacterFormat.FontSize = 10
                TR2.CharacterFormat.TextColor = Color.Black
                If cols(0).Trim.ToLower = strLabelTotalDiscount.Trim.ToLower Then
                    TR2.CharacterFormat.Bold = True
                    p2.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Right
                    DataRow.Cells(c).CellFormat.BackColor = Color.PaleTurquoise
                    DataRow.Cells(6).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
                End If
            Next c
        Next r

        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim ObjSPLDetailtoSPL2 As SPLDetailtoSPL
        For r As Integer = 0 To arlSPLDtltoSPL.Count - 1
            ObjSPLDetailtoSPL = CType(arlSPLDtltoSPL(r), SPLDetailtoSPL)
            If r > 0 Then
                ObjSPLDetailtoSPL2 = CType(arlSPLDtltoSPL(r - 1), SPLDetailtoSPL)
                Dim strLabelTotalLoop As String = String.Empty
                If Not IsNothing(ObjSPLDetailtoSPL.LabelTotal) Then
                    strLabelTotalLoop = ObjSPLDetailtoSPL.LabelTotal.ToLower
                End If
                If ObjSPLDetailtoSPL.VechileTypeID = ObjSPLDetailtoSPL2.VechileTypeID AndAlso _
                   ObjSPLDetailtoSPL.ModelID = ObjSPLDetailtoSPL2.ModelID AndAlso _
                   ObjSPLDetailtoSPL.PeriodMonth = ObjSPLDetailtoSPL2.PeriodMonth AndAlso _
                   ObjSPLDetailtoSPL.PeriodYear = ObjSPLDetailtoSPL2.PeriodYear AndAlso _
                   strLabelTotalLoop.Trim.ToLower <> strLabelTotalDiscount.Trim.ToLower Then

                    If x = 0 Then
                        y = r
                        x = 1
                    Else
                        x += 1
                    End If
                End If
                If strLabelTotalLoop.Trim.ToLower = strLabelTotalDiscount.Trim.ToLower Then
                    table.ApplyHorizontalMerge(r + 1, 0, 5)
                    table.ApplyHorizontalMerge(r + 1, 7, 10)

                    If x > 0 Then
                        table.ApplyVerticalMerge(0, y, y + x)
                        table.ApplyVerticalMerge(1, y, y + x)
                        table.ApplyVerticalMerge(2, y, y + x)
                        table.ApplyVerticalMerge(3, y, y + x)
                        table.ApplyVerticalMerge(7, y, y + x)
                        table.ApplyVerticalMerge(8, y, y + x)
                        table.ApplyVerticalMerge(9, y, y + x)
                        table.ApplyVerticalMerge(10, y, y + x)
                    End If
                    x = 0
                End If
            End If
        Next

        body.ChildObjects.Remove(paragraph)
        body.ChildObjects.Insert(index, table)
    End Function

    'Private Sub SetSessionSPLDetailFromDB(ByVal _objSPL As SPL)        
    '    Dim crit As New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    crit.opAnd(New Criteria(GetType(SPLDetail), "SPL.ID", MatchType.Exact, _objSPL.ID))
    '    Dim sortColl As SortCollection = New SortCollection
    '    sortColl.Add(New Sort(GetType(SPLDetail), "PeriodYear", Sort.SortDirection.ASC))
    '    sortColl.Add(New Sort(GetType(SPLDetail), "PeriodMonth", Sort.SortDirection.ASC))
    '    sortColl.Add(New Sort(GetType(SPLDetail), "VechileType.ID", Sort.SortDirection.ASC))
    '    Dim arlSPLDetail As ArrayList = New SPLDetailFacade(User).Retrieve(crit, sortColl)
    '    sessHelper.SetSession("SPLDETAILLIST", arlSPLDetail)

    '    Dim crit2 As New CriteriaComposite(New Criteria(GetType(SPLDetailtoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    crit2.opAnd(New Criteria(GetType(SPLDetailtoSPL), "SPLDetail.SPL.ID", MatchType.Exact, _objSPL.ID))
    '    Dim sortColl2 As SortCollection = New SortCollection
    '    sortColl2.Add(New Sort(GetType(SPLDetailtoSPL), "SPLDetail.PeriodYear", Sort.SortDirection.ASC))
    '    sortColl2.Add(New Sort(GetType(SPLDetailtoSPL), "SPLDetail.PeriodMonth", Sort.SortDirection.ASC))
    '    sortColl2.Add(New Sort(GetType(SPLDetailtoSPL), "SPLDetail.VechileType.ID", Sort.SortDirection.ASC))
    '    Dim arlSPLDetailtoSPL As ArrayList = New SPLDetailtoSPLFacade(User).Retrieve(crit2, sortColl2)
    '    sessHelper.SetSession("SPLDETAILTOSPLLIST", arlSPLDetailtoSPL)
    'End Sub

    Private Sub UploadDocXFile(ByVal bytes As Byte(), ByVal tempPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim success As Boolean = False
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            success = imp.Start()
            If imp.Start() Then
                If Not System.IO.Directory.Exists(tempPath) Then
                    System.IO.Directory.CreateDirectory(Path.GetDirectoryName(tempPath))
                End If

                If System.IO.File.Exists(tempPath) Then
                    System.IO.File.Delete(Path.GetDirectoryName(tempPath))
                End If

                Try
                    Dim wFile As System.IO.FileStream
                    wFile = New FileStream(tempPath, FileMode.Append)
                    wFile.Write(bytes, 0, bytes.Length)
                    wFile.Close()
                Catch ex As IOException
                    MsgBox(ex.ToString)
                End Try
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        RemoveALLSession()
        Response.Redirect("FrmSPLHeader.aspx")
    End Sub
    Private Sub SetDgSPLDetailItemFooter(ByVal e As DataGridItemEventArgs)
        Dim ddlFooterModelKendaraan As DropDownList = CType(e.Item.FindControl("ddlFooterModelKendaraan"), DropDownList)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.Exact, ""))  '-- Type still active
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.InSet, "(1,2)"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(SubCategoryVehicle), "ID", Sort.SortDirection.DESC))  '-- Sort by Vechile type code
        '-- Bind ddlSubCategory dropdownlist
        Dim arrDDLModel As ArrayList = New ArrayList
        arrDDLModel = New SubCategoryVehicleFacade(User).Retrieve(criterias, sortColl)
        With ddlFooterModelKendaraan
            .Items.Clear()
            .DataSource = arrDDLModel
            .DataTextField = "Name"
            .DataValueField = "ID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", ""))
            .SelectedIndex = 0
        End With

        Dim lblFooterTipeKendaraan As Label = CType(e.Item.FindControl("lblFooterTipeKendaraan"), Label)
        lblFooterTipeKendaraan.Attributes("onclick") = "ShowPPKodeModelSelection('" & ddlFooterModelKendaraan.SelectedValue & "');"

        Dim li As New ListItem
        Dim ddlFooterInterest As DropDownList = CType(e.Item.FindControl("ddlFooterInterest"), DropDownList)
        For Each items As SPLInterest In SPLEnum.RetrieveEnumInterest
            li = New ListItem(items.Desc, items.Code)
            ddlFooterInterest.Items.Add(li)
        Next
        Dim txtFooterTipeKendaraan As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtFooterTipeKendaraan"), System.Web.UI.WebControls.TextBox)
        'txtFooterTipeKendaraan.Attributes.Add("readonly", "readonly")

        Dim lblSearchFooterApplicationNo As Label = CType(e.Item.FindControl("lblSearchFooterApplicationNo"), Label)
        Dim lblSearchFooterPriceReff As Label = CType(e.Item.FindControl("lblSearchFooterPriceRefDate"), Label)
        lblSearchFooterApplicationNo.Attributes("onclick") = "showPopupSearchFooterApplicationNo(this);"
        lblSearchFooterPriceReff.Attributes("onclick") = "showPopupSearchFooterPriceReff(this);"

        Dim ddlFooterDiscountType As DropDownList = CType(e.Item.FindControl("ddlFooterDiscountType"), DropDownList)
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(DiscountMaster), "Status", MatchType.Exact, "1"))  '-- Type still active
        Dim sortColl2 As SortCollection = New SortCollection
        sortColl2.Add(New Sort(GetType(DiscountMaster), "ID", Sort.SortDirection.DESC))  '-- Sort by Vechile type code
        Dim arrDDLDiscType As ArrayList = New ArrayList
        arrDDLDiscType = New DiscountMasterFacade(User).Retrieve(criterias2, sortColl2)
        CommonFunction.SortListControl(arrDDLDiscType, "Category", Sort.SortDirection.ASC)
        With ddlFooterDiscountType
            .Items.Clear()
            .DataSource = arrDDLDiscType
            .DataTextField = "Category"
            .DataValueField = "ID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", ""))
        End With

        Dim txtFooterDeliveryDate As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtFooterDeliveryDate"), System.Web.UI.WebControls.TextBox)
        txtFooterDeliveryDate.Text = CInt(Right(CType(sessHelper.GetSession("STATUSMONTH"), Date).Month.ToString("d2"), 2) & CType(sessHelper.GetSession("STATUSMONTH"), Date).Year)
        txtFooterDeliveryDate.Enabled = False
        Dim txtFooterDeliveryTime As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtFooterDeliveryTime"), System.Web.UI.WebControls.TextBox)
        txtFooterDeliveryTime.Text = Right(CType(sessHelper.GetSession("STATUSMONTH"), Date).Month.ToString("d2"), 2) & CType(sessHelper.GetSession("STATUSMONTH"), Date).Year
        txtFooterDeliveryTime.Enabled = False
    End Sub

    Private Sub SetDgSPLDetailItemEdit(ByVal e As DataGridItemEventArgs, ByVal arrSPLDetailtoSPLView As ArrayList)
        If arrSPLDetailtoSPLView Is Nothing Then
            arrSPLDetailtoSPLView = New ArrayList
        End If
        Dim intItemIndexx As Integer = 0

        Dim arlSPLDetailList As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
        If IsNothing(arlSPLDetailList) Then arlSPLDetailList = New ArrayList

        Dim oSPLDetailtoSPL As SPLDetailtoSPL = CType(arrSPLDetailtoSPLView(e.Item.ItemIndex), SPLDetailtoSPL)

        Dim strLabelTotalDisc As String = If(IsNothing(oSPLDetailtoSPL.LabelTotal), "", oSPLDetailtoSPL.LabelTotal)
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        lblNo = CType(e.Item.FindControl("lblNo"), Label)

        If e.Item.ItemIndex = 0 Then
            ViewState("ItemIndexx") = e.Item.ItemIndex
            intItemIndexx = CType(ViewState("ItemIndexx"), Integer)
            lblNo.Text = intItemIndexx + 1 + (dgSPDetail.CurrentPageIndex * dgSPDetail.PageSize)
            ViewState("ItemIndexx") = lblNo.Text
        Else
            If strLabelTotalDisc.Trim.ToLower = "total :" Then
                ViewState("ItemIndexx") = 0
            Else
                intItemIndexx = CType(ViewState("ItemIndexx"), Integer)
                lblNo.Text = intItemIndexx + 1 + (dgSPDetail.CurrentPageIndex * dgSPDetail.PageSize)
                ViewState("ItemIndexx") = lblNo.Text
            End If
        End If

        Dim i% = 0
        Dim oSPLDetail As SPLDetail
        If arlSPLDetailList.Count > 0 Then
            For Each _objSPLDetail As SPLDetail In arlSPLDetailList
                If _objSPLDetail.VechileType.ID = oSPLDetailtoSPL.VechileTypeID AndAlso _objSPLDetail.ModelID = oSPLDetailtoSPL.ModelID AndAlso _
                        _objSPLDetail.PeriodMonth = oSPLDetailtoSPL.PeriodMonth AndAlso _objSPLDetail.PeriodYear = oSPLDetailtoSPL.PeriodYear Then
                    oSPLDetail = _objSPLDetail
                    Exit For
                End If
                i += 1
            Next
        End If

        Dim ddlEditModelKendaraan As DropDownList = CType(e.Item.FindControl("ddlEditModelKendaraan"), DropDownList)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.Exact, ""))  '-- Type still active
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.InSet, "(1,2)"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(SubCategoryVehicle), "ID", Sort.SortDirection.DESC))  '-- Sort by Vechile type code
        '-- Bind ddlSubCategory dropdownlist
        Dim arrDDLModel As ArrayList = New ArrayList
        arrDDLModel = New SubCategoryVehicleFacade(User).Retrieve(criterias, sortColl)
        With ddlEditModelKendaraan
            .Items.Clear()
            .DataSource = arrDDLModel
            .DataTextField = "Name"
            .DataValueField = "ID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", ""))

            Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, oSPLDetail.VechileType.VechileModel.ID))
            Dim arrSCVToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias2)
            If Not IsNothing(arrSCVToModel) AndAlso arrSCVToModel.Count > 0 Then
                objSubCategoryVehicleToModel = CType(arrSCVToModel(0), SubCategoryVehicleToModel)
            End If
            .SelectedValue = objSubCategoryVehicleToModel.SubCategoryVehicle.ID
        End With
        ddlFooterModelKendaraan_SelectedIndexChanged(ddlEditModelKendaraan, Nothing)

        Dim txtEditTipeKendaraan As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditTipeKendaraan"), System.Web.UI.WebControls.TextBox)

        Dim ObjVechileType As VechileType = New VechileTypeFacade(User).Retrieve(oSPLDetail.VechileType.ID)
        If IsNothing(ObjVechileType) Then ObjVechileType = New VechileType
        txtEditTipeKendaraan.Text = ObjVechileType.VechileTypeCode
        txtFooterTipeKendaraan_TextChanged(txtEditTipeKendaraan, Nothing)

        'txtEditTipeKendaraan.ReadOnly = True
        txtEditTipeKendaraan.Attributes.Add("readonly", "readonly")

        Dim li As ListItem
        Dim ddlEditInterest As DropDownList = CType(e.Item.FindControl("ddlEditInterest"), DropDownList)
        For Each items As SPLInterest In SPLEnum.RetrieveEnumInterest
            li = New ListItem(items.Desc, items.Code)
            ddlEditInterest.Items.Add(li)
        Next
        ddlEditInterest.SelectedValue = oSPLDetail.FreeIntIndicator

        Dim txtEditUnit As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditUnit"), System.Web.UI.WebControls.TextBox)
        txtEditUnit.Text = oSPLDetail.Quantity

        Dim txtEditDeliveryDate As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditDeliveryDate"), System.Web.UI.WebControls.TextBox)
        txtEditDeliveryDate.Text = ReturnMonth2Digit(Convert.ToString(oSPLDetail.DeliveryDate.Month)) & Convert.ToString(oSPLDetail.DeliveryDate.Year)

        Dim lblSearchEditApplicationNo As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("lblSearchEditApplicationNo"), System.Web.UI.WebControls.Label)
        Dim lblSearchEditPriceReff As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("lblSearchEditPriceRefDate"), System.Web.UI.WebControls.Label)
        lblSearchEditApplicationNo.Attributes("onclick") = "showPopupSearchFooterApplicationNo(this);"
        lblSearchEditPriceReff.Attributes("onclick") = "showPopupSearchFooterPriceReff(this);"

        Dim ddlEditDiscountType As DropDownList = CType(e.Item.FindControl("ddlEditDiscountType"), DropDownList)
        Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias3.opAnd(New Criteria(GetType(DiscountMaster), "Status", MatchType.Exact, "1"))  '-- Type still active
        Dim sortColl3 As SortCollection = New SortCollection
        sortColl3.Add(New Sort(GetType(DiscountMaster), "ID", Sort.SortDirection.DESC))  '-- Sort by Vechile type code
        '-- Bind ddlEDiscountType dropdownlist
        Dim arrDDLDiscType As ArrayList = New ArrayList
        arrDDLDiscType = New DiscountMasterFacade(User).Retrieve(criterias3, sortColl3)
        CommonFunction.SortListControl(arrDDLDiscType, "Category", Sort.SortDirection.ASC)
        With ddlEditDiscountType
            .Items.Clear()
            .DataSource = arrDDLDiscType
            .DataTextField = "Category"
            .DataValueField = "ID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", ""))
            .SelectedValue = oSPLDetailtoSPL.DiscountMaster.ID
        End With

        Dim hdnEditSPLID As HiddenField = CType(e.Item.FindControl("hdnEditSPLID"), HiddenField)
        Dim txtEditApplicationNo As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditApplicationNo"), System.Web.UI.WebControls.TextBox)
        Dim txtEditDetailDiscount As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditDetailDiscount"), System.Web.UI.WebControls.TextBox)
        Dim txtEditPriceRefDate As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditPriceRefDate"), System.Web.UI.WebControls.TextBox)
        Dim txtEditTOP As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditTOP"), System.Web.UI.WebControls.TextBox)
        Dim txtEditDeliveryTime As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditDeliveryTime"), System.Web.UI.WebControls.TextBox)

        hdnEditSPLID.Value = If(Not IsNothing(oSPLDetailtoSPL.SPLDetailReference), If(Not IsNothing(oSPLDetailtoSPL.SPLDetailReference.SPL), oSPLDetailtoSPL.SPLDetailReference.SPL.ID, ""), "")
        txtEditApplicationNo.Text = If(Not IsNothing(oSPLDetailtoSPL.SPLDetailReference), If(Not IsNothing(oSPLDetailtoSPL.SPLDetailReference.SPL), oSPLDetailtoSPL.SPLDetailReference.SPL.SPLNumber, ""), "")
        txtFooterApplicationNo_TextChanged(txtEditApplicationNo, Nothing)
        txtEditDetailDiscount.Text = Format(oSPLDetailtoSPL.Discount, "#,##0")
        txtEditPriceRefDate.Text = If(oSPLDetail.PriceRefDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value, "Update Price", oSPLDetail.PriceRefDate.ToString("MMyyyy"))
        txtEditTOP.Text = oSPLDetail.MaxTopDay
        txtEditDeliveryTime.Text = oSPLDetail.DeliveryDate.Month.ToString("d2") & oSPLDetail.DeliveryDate.Year.ToString("d4")
        txtEditDeliveryTime.Enabled = False

        Dim lblEditTipeKendaraan As Label = CType(e.Item.FindControl("lblEditTipeKendaraan"), Label)
        ddlEditModelKendaraan.Enabled = False
        txtEditTipeKendaraan.Enabled = False
        lblEditTipeKendaraan.Visible = False
    End Sub

    Public Sub txtFooterApplicationNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txtFooterApplicationNo As System.Web.UI.WebControls.TextBox = sender
        Dim gridItem As DataGridItem = txtFooterApplicationNo.Parent.Parent
        Dim txtFooterDetailDiscount As System.Web.UI.WebControls.TextBox
        Dim hdnFooterSPLID As HiddenField
        Dim txtFooterTipeKendaraan As System.Web.UI.WebControls.TextBox
        Dim ddlDiscountType As DropDownList
        If gridItem.DataSetIndex > -1 Then
            txtFooterDetailDiscount = gridItem.FindControl("txtEditDetailDiscount")
            hdnFooterSPLID = gridItem.FindControl("hdnEditSPLID")
            txtFooterTipeKendaraan = gridItem.FindControl("txtEditTipeKendaraan")
            ddlDiscountType = gridItem.FindControl("ddlEditDiscountType")
        Else
            txtFooterDetailDiscount = gridItem.FindControl("txtFooterDetailDiscount")
            hdnFooterSPLID = gridItem.FindControl("hdnFooterSPLID")
            txtFooterTipeKendaraan = gridItem.FindControl("txtFooterTipeKendaraan")
            ddlDiscountType = gridItem.FindControl("ddlFooterDiscountType")
        End If
        If txtFooterApplicationNo.Text.Trim = "" Then
            txtFooterDetailDiscount.Enabled = True
            txtFooterDetailDiscount.Text = 0
            hdnFooterSPLID.Value = ""
            txtFooterDetailDiscount.Focus()
        Else
            txtFooterDetailDiscount.Text = "0"
            txtFooterDetailDiscount.Enabled = False
            If txtFooterTipeKendaraan.Text.Trim <> "" Then

                Dim disc As String = getDiscountFromApplicationNo(txtFooterApplicationNo.Text, txtFooterTipeKendaraan.Text, ddlDiscountType.SelectedValue)

                Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtFooterTipeKendaraan.Text)
                Dim objSPL As SPL = New SPLFacade(User).Retrieve(txtFooterApplicationNo.Text)
                If Not IsNothing(objSPL) AndAlso Not IsNothing(objVechileType) Then
                    hdnFooterSPLID.Value = objSPL.ID
                    For Each objSPLDtl As SPLDetail In objSPL.SPLDetails
                        If objSPLDtl.VechileType.ID = objVechileType.ID Then
                            'txtFooterDetailDiscount.Text = Format(objSPLDtl.Discount, "#,##0")
                            txtFooterDetailDiscount.Text = disc
                            Exit For
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Function getDiscountFromApplicationNo(ByVal appNo As String, ByVal vhType As String, ByVal discType As Integer) As String
        'enhance by Ridwan, BA : Desy
        Dim objSPL As SPL = New SPLFacade(User).Retrieve(appNo)
        Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(vhType)
        If Not IsNothing(objSPL) AndAlso Not IsNothing(objVechileType) Then
            Dim critSPLDetail As New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, 0))
            critSPLDetail.opAnd(New Criteria(GetType(SPLDetail), "SPL.ID", MatchType.Exact, objSPL.ID))
            critSPLDetail.opAnd(New Criteria(GetType(SPLDetail), "PeriodMonth", MatchType.Exact, CType(sessHelper.GetSession("STATUSMONTH"), Date).Month))
            critSPLDetail.opAnd(New Criteria(GetType(SPLDetail), "PeriodYear", MatchType.Exact, CType(sessHelper.GetSession("STATUSMONTH"), Date).Year))
            critSPLDetail.opAnd(New Criteria(GetType(SPLDetail), "VechileType.ID", MatchType.Exact, objVechileType.ID))
            Dim tempArrSPLDet As ArrayList = New SPLDetailFacade(User).Retrieve(critSPLDetail)
            If Not IsNothing(tempArrSPLDet) AndAlso tempArrSPLDet.Count > 0 Then
                Dim objSPLDetail As SPLDetail = CType(tempArrSPLDet(0), SPLDetail)

                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetailtoSPL), "RowStatus", MatchType.Exact, 0))
                crit.opAnd(New Criteria(GetType(SPLDetailtoSPL), "SPLDetail.ID", MatchType.Exact, objSPLDetail.ID))
                crit.opAnd(New Criteria(GetType(SPLDetailtoSPL), "DiscountMaster.ID", MatchType.Exact, discType))
                crit.opAnd(New Criteria(GetType(SPLDetailtoSPL), "SPLDetail.ID", MatchType.Exact, objSPLDetail.ID))
                Dim arrSPLDetToSPL As ArrayList = New SPLDetailtoSPLFacade(User).Retrieve(crit)

                If Not IsNothing(arrSPLDetToSPL) AndAlso arrSPLDetToSPL.Count > 0 Then
                    Dim objSPLDetailToSPL As SPLDetailtoSPL = CType(arrSPLDetToSPL(0), SPLDetailtoSPL)
                    Return Format(objSPLDetailToSPL.Discount, "#,##0")
                End If
            End If
        End If
        Return "0"
    End Function

    Protected Sub hdnFooterSPLID_ValueChanged(sender As Object, e As EventArgs)
        Dim hdnFooterSPLID As HiddenField = sender
        Dim gridItem As DataGridItem = hdnFooterSPLID.Parent.Parent
        Dim txtFooterDetailDiscount As System.Web.UI.WebControls.TextBox
        Dim txtFooterApplicationNo As System.Web.UI.WebControls.TextBox
        Dim txtFooterTipeKendaraan As System.Web.UI.WebControls.TextBox
        Dim ddlDiscountType As DropDownList
        If gridItem.DataSetIndex > -1 Then
            txtFooterDetailDiscount = gridItem.FindControl("txtEditDetailDiscount")
            txtFooterApplicationNo = gridItem.FindControl("txtEditApplicationNo")
            txtFooterTipeKendaraan = gridItem.FindControl("txtEditTipeKendaraan")
            ddlDiscountType = gridItem.FindControl("ddlEditDiscountType")
        Else
            txtFooterDetailDiscount = gridItem.FindControl("txtFooterDetailDiscount")
            txtFooterApplicationNo = gridItem.FindControl("txtFooterApplicationNo")
            txtFooterTipeKendaraan = gridItem.FindControl("txtFooterTipeKendaraan")
            ddlDiscountType = gridItem.FindControl("ddlFooterDiscountType")
        End If
        If hdnFooterSPLID.Value.Trim = "" Then
            txtFooterDetailDiscount.Enabled = True
            txtFooterDetailDiscount.Text = 0
            txtFooterDetailDiscount.Focus()
        Else
            txtFooterDetailDiscount.Text = "0"
            txtFooterDetailDiscount.Enabled = False
            If txtFooterTipeKendaraan.Text.Trim <> "" Then
                Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtFooterTipeKendaraan.Text)
                Dim objSPL As SPL = New SPLFacade(User).Retrieve(txtFooterApplicationNo.Text)
                If Not IsNothing(objSPL) AndAlso Not IsNothing(objVechileType) Then
                    hdnFooterSPLID.Value = objSPL.ID
                    For Each objSPLDtl As SPLDetail In objSPL.SPLDetails
                        If objSPLDtl.VechileType.ID = objVechileType.ID Then
                            'txtFooterDetailDiscount.Text = Format(objSPLDtl.Discount, "#,##0")
                            txtFooterDetailDiscount.Text = getDiscountFromApplicationNo(txtFooterApplicationNo.Text, txtFooterTipeKendaraan.Text, ddlDiscountType.SelectedValue)
                            Exit For
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Public Sub txtFooterTipeKendaraan_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txtFooterTipeKendaraan As System.Web.UI.WebControls.TextBox = sender
        Dim gridItem As DataGridItem = txtFooterTipeKendaraan.Parent.Parent
        Dim ddlFooterModelKendaraan As System.Web.UI.WebControls.DropDownList
        Dim ddlFooterDiscountType As DropDownList
        Dim hdnFooterSPLID As HiddenField
        Dim txtFooterUnit As System.Web.UI.WebControls.TextBox
        Dim txtFooterPriceRefDate As System.Web.UI.WebControls.TextBox
        Dim txtFooterTOP As System.Web.UI.WebControls.TextBox
        Dim ddlFooterInterest As System.Web.UI.WebControls.DropDownList
        Dim txtFooterDeliveryTime As System.Web.UI.WebControls.TextBox
        Dim txtFooterApplicationNo As System.Web.UI.WebControls.TextBox
        Dim txtFooterDetailDiscount As System.Web.UI.WebControls.TextBox
        Dim lblSearchFooterPriceRefDate As Label

        If gridItem.DataSetIndex > -1 Then
            ddlFooterModelKendaraan = gridItem.FindControl("ddlEditModelKendaraan")
            hdnFooterSPLID = gridItem.FindControl("hdnEditSPLID")
            ddlFooterDiscountType = gridItem.FindControl("ddlEditDiscountType")
            txtFooterUnit = gridItem.FindControl("txtEditUnit")
            txtFooterPriceRefDate = gridItem.FindControl("txtEditPriceRefDate")
            txtFooterTOP = gridItem.FindControl("txtEditTOP")
            ddlFooterInterest = gridItem.FindControl("ddlEditInterest")
            txtFooterDeliveryTime = gridItem.FindControl("txtEditDeliveryTime")
            txtFooterApplicationNo = gridItem.FindControl("txtEditApplicationNo")
            txtFooterDetailDiscount = gridItem.FindControl("txtEditDetailDiscount")
            lblSearchFooterPriceRefDate = gridItem.FindControl("lblSearchEditPriceRefDate")
        Else
            ddlFooterModelKendaraan = gridItem.FindControl("ddlFooterModelKendaraan")
            hdnFooterSPLID = gridItem.FindControl("hdnFooterSPLID")
            ddlFooterDiscountType = gridItem.FindControl("ddlFooterDiscountType")
            txtFooterUnit = gridItem.FindControl("txtFooterUnit")
            txtFooterPriceRefDate = gridItem.FindControl("txtFooterPriceRefDate")
            txtFooterTOP = gridItem.FindControl("txtFooterTOP")
            ddlFooterInterest = gridItem.FindControl("ddlFooterInterest")
            txtFooterDeliveryTime = gridItem.FindControl("txtFooterDeliveryTime")
            txtFooterApplicationNo = gridItem.FindControl("txtFooterApplicationNo")
            txtFooterDetailDiscount = gridItem.FindControl("txtFooterDetailDiscount")
            lblSearchFooterPriceRefDate = gridItem.FindControl("lblSearchFooterPriceRefDate")
        End If

        Dim arlSPLDetaillist As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
        If arlSPLDetaillist Is Nothing Then arlSPLDetaillist = New ArrayList
        Dim arlSPLDetailtoSPLlist As ArrayList = CType(sessHelper.GetSession("SPLDETAILTOSPLLIST"), ArrayList)
        If arlSPLDetailtoSPLlist Is Nothing Then arlSPLDetailtoSPLlist = New ArrayList

        Dim blnIsExist As Boolean = False
        Dim _period As Date = CType(sessHelper.GetSession("STATUSMONTH"), Date)
        Dim objVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtFooterTipeKendaraan.Text.Trim)
        If Not arlSPLDetailtoSPLlist Is Nothing Then
            For Each objSPLDetailtoSPL As SPLDetailtoSPL In arlSPLDetailtoSPLlist
                If (objSPLDetailtoSPL.ModelID.ToString() = ddlFooterModelKendaraan.SelectedValue And objSPLDetailtoSPL.VechileTypeID = objVechileType.ID) Then
                    If objSPLDetailtoSPL.PeriodMonth = _period.Month And objSPLDetailtoSPL.PeriodYear = _period.Year Then
                        If objSPLDetailtoSPL.NumberRow <> (gridItem.ItemIndex + 1) Then
                            If blnCommandEdit = False Then
                                hdnFooterSPLID.Value = If(Not IsNothing(objSPLDetailtoSPL.SPLDetailReference), If(Not IsNothing(objSPLDetailtoSPL.SPLDetailReference.SPL), objSPLDetailtoSPL.SPLDetailReference.SPL.ID, ""), "")
                                'ddlFooterDiscountType.SelectedValue = objSPLDetailtoSPL.DiscountMaster.ID
                                'txtFooterApplicationNo.Text = If(Not IsNothing(objSPLDetailtoSPL.SPLDetailReference), If(Not IsNothing(objSPLDetailtoSPL.SPLDetailReference.SPL), objSPLDetailtoSPL.SPLDetailReference.SPL.SPLNumber, ""), "")
                                'txtFooterDetailDiscount.Text = Format(objSPLDetailtoSPL.Discount, "#,##0")
                                blnIsExist = True
                                Exit For
                            End If
                        End If
                    End If
                End If
            Next
        End If
        If blnIsExist = False Then
            hdnFooterSPLID.Value = ""
            ddlFooterDiscountType.SelectedIndex = 0
            txtFooterApplicationNo.Text = ""
            txtFooterDetailDiscount.Text = "0"
        End If
        txtFooterApplicationNo_TextChanged(txtFooterApplicationNo, Nothing)

        If blnIsExist = True Then
            If Not arlSPLDetaillist Is Nothing AndAlso arlSPLDetaillist.Count > 0 Then
                For Each oSPLDetail As SPLDetail In arlSPLDetaillist
                    If (oSPLDetail.ModelID.ToString() = ddlFooterModelKendaraan.SelectedValue And oSPLDetail.VechileType.VechileTypeCode.ToString = txtFooterTipeKendaraan.Text.Trim _
                        And oSPLDetail.PeriodMonth = _period.Month And oSPLDetail.PeriodYear = _period.Year) Then
                        txtFooterUnit.Text = oSPLDetail.Quantity
                        txtFooterPriceRefDate.Text = If(oSPLDetail.PriceRefDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value, "Update Price", oSPLDetail.PriceRefDate.ToString("MMyyyy"))
                        txtFooterTOP.Text = oSPLDetail.MaxTopDay
                        ddlFooterInterest.SelectedValue = oSPLDetail.FreeIntIndicator
                        txtFooterDeliveryTime.Text = oSPLDetail.DeliveryDate.Month.ToString("d2") & oSPLDetail.DeliveryDate.Year.ToString("d4")
                        txtFooterUnit.Enabled = False
                        txtFooterPriceRefDate.Enabled = False
                        txtFooterTOP.Enabled = False
                        ddlFooterInterest.Enabled = False
                        txtFooterDeliveryTime.Enabled = False
                        lblSearchFooterPriceRefDate.Visible = False
                        blnIsExist = True
                        Exit For
                    End If
                Next
            End If
        Else
            txtFooterUnit.Enabled = True
            txtFooterPriceRefDate.Enabled = True
            txtFooterTOP.Enabled = True
            ddlFooterInterest.Enabled = True
            txtFooterDeliveryTime.Enabled = False
            lblSearchFooterPriceRefDate.Visible = True
        End If
    End Sub

    Public Sub ddlFooterModelKendaraan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlFooterModelKendaraan As DropDownList = sender
        Dim gridItem As DataGridItem = ddlFooterModelKendaraan.Parent.Parent
        Dim txtFooterTipeKendaraan As System.Web.UI.WebControls.TextBox
        Dim lblFooterTipeKendaraan As System.Web.UI.WebControls.Label
        If gridItem.DataSetIndex > -1 Then
            txtFooterTipeKendaraan = gridItem.FindControl("txtEditTipeKendaraan")
            lblFooterTipeKendaraan = gridItem.FindControl("lblEditTipeKendaraan")
        Else
            txtFooterTipeKendaraan = gridItem.FindControl("txtFooterTipeKendaraan")
            lblFooterTipeKendaraan = gridItem.FindControl("lblFooterTipeKendaraan")
        End If
        txtFooterTipeKendaraan.Text = ""
        txtFooterTipeKendaraan_TextChanged(txtFooterTipeKendaraan, Nothing)
        lblFooterTipeKendaraan.Attributes("onclick") = "ShowPPKodeModelSelection('" & ddlFooterModelKendaraan.SelectedValue & "');"
    End Sub

    Private Sub btnRetrieveDetailDiscount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRetrieveDetailDiscount.Click
    End Sub

    Sub dgSPDetail_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles dgSPDetail.ItemCommand
        Select Case (e.CommandName)
            Case "Delete"
                Dim lShouldReturn As Boolean
                DeleteCommand(e, lShouldReturn)
                If lShouldReturn Then
                    Return
                End If
            Case "Add"
                AddCommand(e)
            Case "Edit"
                dgSPDetail_EditCommand(e)
            Case "Update"
                dgSPDetail_Update(e)
            Case "Cancel"
                dgSPDetail_CancelCommand(e)
        End Select
    End Sub

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs, ByRef shouldReturn As Boolean)
        Dim arlSPLDetail As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
        If IsNothing(arlSPLDetail) Then arlSPLDetail = New ArrayList

        Dim arlSPLDetailToSPLALL As ArrayList = CType(sessHelper.GetSession("SPLDETAILTOSPLLIST"), ArrayList)
        If IsNothing(arlSPLDetailToSPLALL) Then arlSPLDetailToSPLALL = New ArrayList
        Dim arlSPLDetailToSPLView As ArrayList = CType(sessHelper.GetSession("SPLVIEW"), ArrayList)
        If IsNothing(arlSPLDetailToSPLView) Then arlSPLDetailToSPLView = New ArrayList

        Dim oSPLDetailtoSPL As SPLDetailtoSPL = CType(arlSPLDetailToSPLView(e.Item.ItemIndex), SPLDetailtoSPL)
        IndexList = GetIndexSPLDETAILLIST(arlSPLDetailToSPLALL, oSPLDetailtoSPL)
        If oSPLDetailtoSPL.ID > 0 Then
            Dim arrDelete As ArrayList = CType(sessHelper.GetSession("DELETESPLDETAILTOSPLLIST"), ArrayList)
            If IsNothing(arrDelete) Then arrDelete = New ArrayList
            arrDelete.Add(oSPLDetailtoSPL)
            sessHelper.SetSession("DELETESPLDETAILTOSPLLIST", arrDelete)
        End If
        If Not arlSPLDetailToSPLALL Is Nothing Then
            arlSPLDetailToSPLALL.RemoveAt(IndexList)
        End If
        If Not arlSPLDetailToSPLView Is Nothing Then
            arlSPLDetailToSPLView.RemoveAt(e.Item.ItemIndex)
        End If

        sessHelper.SetSession("SPLDETAILTOSPLLIST", arlSPLDetailToSPLALL)

        'Hapus SPLDetailnya jika SPLDetailtoSPLnya kosong
        Dim arlSPLDetailToSPLPerType As ArrayList = New ArrayList
        arlSPLDetailToSPLPerType = New System.Collections.ArrayList(
                                                (From obj As SPLDetailtoSPL In arlSPLDetailToSPLALL.OfType(Of SPLDetailtoSPL)()
                                                    Where obj.VechileTypeID = oSPLDetailtoSPL.VechileTypeID _
                                                    And obj.ModelID = oSPLDetailtoSPL.ModelID _
                                                    And obj.PeriodMonth = oSPLDetailtoSPL.PeriodMonth _
                                                    And obj.PeriodYear = oSPLDetailtoSPL.PeriodYear
                                                    Select obj).ToList())
        Dim i As Integer = 0
        Dim oSPLDetail As New SPLDetail
        For i = 0 To arlSPLDetail.Count - 1
            Dim _objSPLDetail As SPLDetail = CType(arlSPLDetail(i), SPLDetail)
            If _objSPLDetail.VechileTypeID = oSPLDetailtoSPL.VechileTypeID _
                AndAlso _objSPLDetail.ModelID = oSPLDetailtoSPL.ModelID _
                AndAlso _objSPLDetail.PeriodMonth = oSPLDetailtoSPL.PeriodMonth _
                AndAlso _objSPLDetail.PeriodYear = oSPLDetailtoSPL.PeriodYear Then
                oSPLDetail = _objSPLDetail
                Exit For
            End If
        Next
        Call UpdateDiscountSPLDetail(oSPLDetail, arlSPLDetailToSPLView)

        Try
            If arlSPLDetailToSPLPerType.Count = 0 Then
                If oSPLDetail.ID > 0 Then
                    Dim arrDelete As ArrayList = CType(sessHelper.GetSession("DELETESPLDETAILLIST"), ArrayList)
                    If IsNothing(arrDelete) Then arrDelete = New ArrayList
                    arrDelete.Add(oSPLDetail)
                    sessHelper.SetSession("DELETESPLDETAILLIST", arrDelete)
                End If
                If arlSPLDetail.Count > 0 Then
                    arlSPLDetail.RemoveAt(i)
                End If
            End If
            sessHelper.SetSession("SPLDETAILLIST", arlSPLDetail)
        Catch
        End Try

        BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
    End Sub

    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Exit Sub
        End If

        Dim arlSPLDetailToSPL As ArrayList = CType(sessHelper.GetSession("SPLDETAILTOSPLLIST"), ArrayList)
        If IsNothing(arlSPLDetailToSPL) Then arlSPLDetailToSPL = New ArrayList
        Dim arlSPLDetail As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
        If IsNothing(arlSPLDetail) Then arlSPLDetail = New ArrayList

        Dim ddlFooterModelKendaraan As DropDownList = CType(e.Item.FindControl("ddlFooterModelKendaraan"), DropDownList)
        Dim txtFooterTipeKendaraan As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtFooterTipeKendaraan"), System.Web.UI.WebControls.TextBox)
        Dim txtFooterUnit As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtFooterUnit"), System.Web.UI.WebControls.TextBox)
        Dim ddlFooterDiscountType As DropDownList = CType(e.Item.FindControl("ddlFooterDiscountType"), DropDownList)
        Dim txtFooterApplicationNo As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtFooterApplicationNo"), System.Web.UI.WebControls.TextBox)
        Dim txtFooterDetailDiscount As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtFooterDetailDiscount"), System.Web.UI.WebControls.TextBox)
        Dim txtFooterPriceRefDate As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtFooterPriceRefDate"), System.Web.UI.WebControls.TextBox)
        Dim txtFooterTOP As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtFooterTOP"), System.Web.UI.WebControls.TextBox)
        Dim txtFooterDeliveryTime As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtFooterDeliveryTime"), System.Web.UI.WebControls.TextBox)
        Dim hdnFooterSPLID As HiddenField = CType(e.Item.FindControl("hdnFooterSPLID"), HiddenField)
        Dim ddlFooterInterest As DropDownList = CType(e.Item.FindControl("ddlFooterInterest"), DropDownList)
        Dim oSPLDetail As SPLDetail

        Dim dtePriceRefDate As Date
        If ValidateData(ddlFooterModelKendaraan, txtFooterTipeKendaraan, txtFooterUnit, ddlFooterDiscountType, txtFooterApplicationNo, txtFooterDetailDiscount, _
                        txtFooterPriceRefDate, txtFooterTOP, txtFooterDeliveryTime, dtePriceRefDate) Then

            If txtFooterApplicationNo.Text.Trim <> "" Then
                Dim criterias0 As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias0.opAnd(New Criteria(GetType(SPL), "ApprovalStatus", MatchType.Exact, 5))
                'criterias0.opAnd(New Criteria(GetType(SPL), "DealerName", MatchType.[Partial], txtDealerCode.Text))

                Dim strSQL As String = String.Empty
                If ddlFooterDiscountType.SelectedValue <> "" Then
                    strSQL = "Select distinct a.ID from SPL a join SPLDetail b on a.ID = b.SPLID and b.RowStatus = 0 "
                    strSQL += "join SPLDetailtoSPL c on b.ID = c.SPLDetailID and c.RowStatus = 0 "
                    strSQL += "where a.RowStatus = 0 And c.DiscountMasterID = " & ddlFooterDiscountType.SelectedValue
                    criterias0.opAnd(New Criteria(GetType(SPL), "ID", MatchType.InSet, "(" & strSQL & ")"))
                End If
                If txtFooterTipeKendaraan.Text.Trim <> "" Then
                    strSQL = "Select distinct a.ID From SPL a join SPLDetail b on a.ID = b.SPLID join VechileType c on b.VehicleTypeID = c.ID "
                    strSQL += "Where a.RowStatus = 0 And b.RowStatus = 0 And c.VechileTypeCode = '" & txtFooterTipeKendaraan.Text & "'"
                    criterias0.opAnd(New Criteria(GetType(SPL), "ID", MatchType.InSet, "(" & strSQL & ")"))
                End If
                criterias0.opAnd(New Criteria(GetType(SPL), "SPLNumber", MatchType.[Partial], txtFooterApplicationNo.Text))
                Dim arrSPL As ArrayList = New SPLFacade(User).Retrieve(criterias0)
                If IsNothing(arrSPL) OrElse (Not IsNothing(arrSPL) AndAlso arrSPL.Count = 0) Then
                    MessageBox.Show("Application No. tidak ada di data Pencarian Aplikasi")
                    txtFooterApplicationNo.Focus()
                    Exit Sub
                End If
            End If

            Dim strLabelTotal As String = ""
            Dim _arlSPLDetailtoSPLs As ArrayList = New System.Collections.ArrayList(
                                                    (From obj As SPLDetailtoSPL In arlSPLDetailToSPL.OfType(Of SPLDetailtoSPL)()
                                                        Where obj.PeriodYear = CType(sessHelper.GetSession("STATUSMONTH"), Date).Year _
                                                        And obj.PeriodMonth = CType(sessHelper.GetSession("STATUSMONTH"), Date).Month _
                                                        Select obj).ToList())

            For Each objSPLDetailToSPL As SPLDetailtoSPL In _arlSPLDetailtoSPLs
                strLabelTotal = ""
                If Not IsNothing(objSPLDetailToSPL.LabelTotal) Then
                    strLabelTotal = objSPLDetailToSPL.LabelTotal
                End If
                If strLabelTotal.ToLower <> "total :" Then
                    oSPLDetail = New SPLDetail
                    For Each objSPLDetail As SPLDetail In arlSPLDetail
                        If objSPLDetail.VechileTypeID = objSPLDetailToSPL.VechileTypeID _
                            AndAlso objSPLDetail.ModelID = objSPLDetailToSPL.ModelID _
                            AndAlso objSPLDetail.PeriodMonth = objSPLDetailToSPL.PeriodMonth _
                            AndAlso objSPLDetail.PeriodYear = objSPLDetailToSPL.PeriodYear Then
                            oSPLDetail = objSPLDetail
                            Exit For
                        End If
                    Next

                    Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, oSPLDetail.VechileType.VechileModel.ID))
                    Dim arrSCVToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
                    If Not IsNothing(arrSCVToModel) AndAlso arrSCVToModel.Count > 0 Then
                        objSubCategoryVehicleToModel = CType(arrSCVToModel(0), SubCategoryVehicleToModel)
                    End If

                    Dim strSPLID As String = String.Empty
                    Dim ObjSPLDetailReference As SPLDetail
                    If objSubCategoryVehicleToModel.SubCategoryVehicle.ID.ToString() = ddlFooterModelKendaraan.SelectedValue Then
                        If oSPLDetail.VechileType.ID = New VechileTypeFacade(User).Retrieve(txtFooterTipeKendaraan.Text).ID Then
                            If objSPLDetailToSPL.DiscountMaster.ID.ToString() = ddlFooterDiscountType.SelectedValue Then
                                If Not IsNothing(objSPLDetailToSPL.SPLDetailReference) Then
                                    ObjSPLDetailReference = objSPLDetailToSPL.SPLDetailReference
                                    If Not IsNothing(ObjSPLDetailReference.SPL) Then
                                        strSPLID = ObjSPLDetailReference.SPL.ID.ToString
                                    End If
                                End If
                                If strSPLID = hdnFooterSPLID.Value Then
                                    MessageBox.Show("Model, Tipe, Discount Type dan Application Number sudah pernah di input.")
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            'If Not ValidateDuplication(ddlFooterModelKendaraan.SelectedValue, txtFooterTipeKendaraan.Text.Trim, CType(sessHelper.GetSession("STATUSMONTH"), Date), "add", e.Item.ItemIndex) Then
            '    Return
            'End If

            oSPLDetail = New SPLDetail
            Dim oSPLDetailtoSPL As New SPLDetailtoSPL
            With oSPLDetailtoSPL
                .NumberRow = arlSPLDetailToSPL.Count + 1

                Dim objSPLDetailReference As SPLDetail
                If hdnFooterSPLID.Value <> "" Then
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SPLDetail), "SPL.ID", MatchType.Exact, hdnFooterSPLID.Value))
                    criterias.opAnd(New Criteria(GetType(SPLDetail), "VechileType.VechileTypeCode", MatchType.Exact, txtFooterTipeKendaraan.Text.Trim))
                    Dim arrSPLDtl As ArrayList = New SPLDetailFacade(User).Retrieve(criterias)
                    If Not IsNothing(arrSPLDtl) AndAlso arrSPLDtl.Count > 0 Then
                        objSPLDetailReference = CType(arrSPLDtl(0), SPLDetail)
                    End If
                End If
                .SPLDetailReference = objSPLDetailReference
                .DiscountMaster = New DiscountMasterFacade(User).Retrieve(CInt(ddlFooterDiscountType.SelectedValue))
                .Discount = txtFooterDetailDiscount.Text
                .VechileTypeID = New VechileTypeFacade(User).Retrieve(txtFooterTipeKendaraan.Text.Trim).ID
                .ModelID = ddlFooterModelKendaraan.SelectedValue
                .PeriodMonth = CType(sessHelper.GetSession("STATUSMONTH"), Date).Month
                .PeriodYear = CType(sessHelper.GetSession("STATUSMONTH"), Date).Year

                Dim objSPL As SPL
                If Not IsNothing(sessHelper.GetSession("IDSPLHeader")) Then
                    objSPL = New SPLFacade(User).Retrieve(CInt(sessHelper.GetSession("IDSPLHeader")))
                End If
                oSPLDetail.SPL = objSPL
                Dim oVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtFooterTipeKendaraan.Text.Trim)
                oSPLDetail.VechileType = oVechileType
                oSPLDetail.Quantity = txtFooterUnit.Text
                oSPLDetail.PriceRefDate = dtePriceRefDate
                oSPLDetail.MaxTopDay = txtFooterTOP.Text
                oSPLDetail.FreeIntIndicator = ddlFooterInterest.SelectedValue
                oSPLDetail.DeliveryDate = CDate("01" & "/" & Left(txtFooterDeliveryTime.Text, 2) & "/" & Mid(txtFooterDeliveryTime.Text, 3, 4))
                oSPLDetail.VechileTypeID = oVechileType.ID
                oSPLDetail.ModelID = ddlFooterModelKendaraan.SelectedValue
                oSPLDetail.PeriodMonth = CType(sessHelper.GetSession("STATUSMONTH"), Date).Month
                oSPLDetail.PeriodYear = CType(sessHelper.GetSession("STATUSMONTH"), Date).Year
            End With

            If txtFooterTOP.Text > 0 Then
                oSPLDetail.MaxTopIndicator = 1
            Else
                oSPLDetail.MaxTopIndicator = 0
            End If

            Dim IsExistTipe As Boolean = False
            For Each oSPLDtl As SPLDetail In arlSPLDetail  
                If oSPLDtl.ModelID = oSPLDetail.ModelID AndAlso oSPLDtl.VechileTypeID = oSPLDetail.VechileTypeID AndAlso _
                    oSPLDtl.PeriodMonth = oSPLDetail.PeriodMonth AndAlso oSPLDtl.PeriodYear = oSPLDetail.PeriodYear Then
                    oSPLDetail = oSPLDtl
                    IsExistTipe = True
                    Exit For
                End If
            Next
            If IsExistTipe = False Then
                arlSPLDetail.Add(oSPLDetail)
            End If
            arlSPLDetailToSPL.Add(oSPLDetailtoSPL)
            _arlSPLDetailtoSPLs.Add(oSPLDetailtoSPL)

            'Dim arlSPLDetailToSPLCompare As New ArrayList
            'Dim _oSPLDetail As New SPLDetail
            'For Each oSPLDtl As SPLDetail In arlSPLDetail
            '    For Each oSPLDtltoSPLx As SPLDetailtoSPL In oSPLDtl.SPLDetailtoSPLs
            '        For Each oSPLDtltoSPL As SPLDetailtoSPL In arlSPLDetailToSPL
            '            Dim intoSPLDtltoSPLxSPLDetailReferenceID As Integer = 0
            '            Dim intoSPLDtltoSPLSPLDetailReferenceID As Integer = 0
            '            If Not IsNothing(oSPLDtltoSPLx.SPLDetailReference) Then intoSPLDtltoSPLxSPLDetailReferenceID = oSPLDtltoSPLx.SPLDetailReference.ID
            '            If Not IsNothing(oSPLDtltoSPL.SPLDetailReference) Then intoSPLDtltoSPLSPLDetailReferenceID = oSPLDtltoSPL.SPLDetailReference.ID
            '            If oSPLDtltoSPLx.ModelID = oSPLDtltoSPL.ModelID AndAlso oSPLDtltoSPLx.VechileTypeID = oSPLDtltoSPL.VechileTypeID AndAlso _
            '                oSPLDtltoSPLx.PeriodMonth = oSPLDtltoSPL.PeriodMonth AndAlso oSPLDtltoSPLx.PeriodYear = oSPLDtltoSPL.PeriodYear AndAlso _
            '                oSPLDtltoSPLx.DiscountMaster.ID = oSPLDtltoSPL.DiscountMaster.ID AndAlso intoSPLDtltoSPLxSPLDetailReferenceID = intoSPLDtltoSPLSPLDetailReferenceID AndAlso _
            '                oSPLDtltoSPLx.Discount = oSPLDtltoSPL.Discount Then
            '                arlSPLDetailToSPLCompare.Add(oSPLDtltoSPL)
            '                Exit For
            '            End If
            '        Next
            '    Next
            'Next

            'Dim arlSPLDetailToSPLNew As New ArrayList
            'For Each oSPLDtltoSPLx As SPLDetailtoSPL In arlSPLDetailToSPL
            '    Dim isExistVal As Boolean = False
            '    For Each oSPLDtltoSPL As SPLDetailtoSPL In arlSPLDetailToSPLCompare
            '        Dim intoSPLDtltoSPLxSPLDetailReferenceID As Integer = 0
            '        Dim intoSPLDtltoSPLSPLDetailReferenceID As Integer = 0
            '        If Not IsNothing(oSPLDtltoSPLx.SPLDetailReference) Then intoSPLDtltoSPLxSPLDetailReferenceID = oSPLDtltoSPLx.SPLDetailReference.ID
            '        If Not IsNothing(oSPLDtltoSPL.SPLDetailReference) Then intoSPLDtltoSPLSPLDetailReferenceID = oSPLDtltoSPL.SPLDetailReference.ID

            '        If oSPLDtltoSPLx.ModelID = oSPLDtltoSPL.ModelID AndAlso oSPLDtltoSPLx.VechileTypeID = oSPLDtltoSPL.VechileTypeID AndAlso _
            '            oSPLDtltoSPLx.PeriodMonth = oSPLDtltoSPL.PeriodMonth AndAlso oSPLDtltoSPLx.PeriodYear = oSPLDtltoSPL.PeriodYear AndAlso _
            '            oSPLDtltoSPLx.DiscountMaster.ID = oSPLDtltoSPL.DiscountMaster.ID AndAlso intoSPLDtltoSPLxSPLDetailReferenceID = intoSPLDtltoSPLSPLDetailReferenceID AndAlso _
            '            oSPLDtltoSPLx.Discount = oSPLDtltoSPL.Discount Then
            '            isExistVal = True
            '            Exit For
            '        End If
            '    Next
            '    If isExistVal = False Then
            '        arlSPLDetailToSPLNew.Add(oSPLDtltoSPLx)
            '    End If
            'Next

            'For Each oSPLDtl As SPLDetail In arlSPLDetail
            '    For i As Integer = 0 To oSPLDtl.SPLDetailtoSPLs.Count - 1
            '        oSPLDtl.SPLDetailtoSPLs.Remove(i)
            '    Next
            'Next

            'For Each oSPLDtl As SPLDetail In arlSPLDetail
            '    For Each oSPLDtltoSPLNew As SPLDetailtoSPL In arlSPLDetailToSPLNew
            '        If oSPLDtl.ModelID = oSPLDtltoSPLNew.ModelID AndAlso oSPLDtl.VechileTypeID = oSPLDtltoSPLNew.VechileTypeID AndAlso _
            '            oSPLDtl.PeriodMonth = oSPLDtltoSPLNew.PeriodMonth AndAlso oSPLDtl.PeriodYear = oSPLDtltoSPLNew.PeriodYear Then
            '            oSPLDtl.SPLDetailtoSPLs.Add(oSPLDtltoSPLNew)
            '        End If
            '    Next
            'Next

            Call UpdateDiscountSPLDetail(oSPLDetail, _arlSPLDetailtoSPLs)
            sessHelper.SetSession("SPLDETAILLIST", arlSPLDetail)
            sessHelper.SetSession("SPLDETAILTOSPLLIST", arlSPLDetailToSPL)

            BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
        End If
    End Sub

    Private Function ValidateData(ddlModelKendaraan As DropDownList, txtTipeKendaraan As System.Web.UI.WebControls.TextBox, txtUnit As System.Web.UI.WebControls.TextBox, _
                                  ddlDiscountType As DropDownList, txtApplicationNo As System.Web.UI.WebControls.TextBox, txtDetailDiscount As System.Web.UI.WebControls.TextBox, _
                                  txtPriceRefDate As System.Web.UI.WebControls.TextBox, txtTOP As System.Web.UI.WebControls.TextBox, txtDeliveryTime As System.Web.UI.WebControls.TextBox _
                                  , ByRef dtePriceRefDate As Date) As Boolean

        Dim objVecType As VechileType = New VechileTypeFacade(User).Retrieve(txtTipeKendaraan.Text.Trim)
        If objVecType.ID = 0 Then
            MessageBox.Show("Kode Tipe Kendaraan tidak valid")
            Return False
        End If
        If ddlModelKendaraan.SelectedIndex = 0 Then
            MessageBox.Show("Model Kendaraan harus dipilih.")
            Return False
        End If
        If txtTipeKendaraan.Text.Trim = "" Then
            MessageBox.Show("Tipe Kendaraan harus dipilih.")
            Return False
        End If
        If txtUnit.Text.Trim = String.Empty OrElse txtUnit.Text.Trim = "0" Then
            MessageBox.Show("Qty Unit harus diisi atau harus lebih dari 0")
            Return False
        End If
        If ddlDiscountType.SelectedIndex = 0 Then
            MessageBox.Show("Discount Type harus dipilih.")
            Return False
        End If
        'Dim strDiscountTypeCode As String = String.Empty
        'Dim objDiscountMaster As DiscountMaster = New DiscountMasterFacade(User).Retrieve(CInt(BlankToZerro(ddlDiscountType.SelectedValue)))
        'If Not IsNothing(objDiscountMaster) Then strDiscountTypeCode = objDiscountMaster.Code
        'If strDiscountTypeCode <> "D01" Then   '--> Fleet/Government Discount
        '    If txtApplicationNo.Text = String.Empty Then
        '        MessageBox.Show("Application No harus diisi.")
        '        Return False
        '    End If
        'End If
        If txtDetailDiscount.Enabled = True Then
            If txtDetailDiscount.Text.Trim = String.Empty OrElse txtDetailDiscount.Text.Trim = "0" Then
                MessageBox.Show("Detail Discount harus diisi atau harus lebih dari 0")
                Return False
            End If
        End If
        If txtTOP.Text.Trim = String.Empty Then
            txtTOP.Text = 0
        End If
        If txtDeliveryTime.Text.Trim = "" Then
            MessageBox.Show("Delivery Time harus dipilih.")
            Return False
        End If
        If txtPriceRefDate.Text = String.Empty Then
            MessageBox.Show("Price Reff Date harus diisi.")
            Return False
        Else
            If txtPriceRefDate.Text.Trim <> "Update Price" Then
                If Len(txtPriceRefDate.Text.Trim) < 6 Then
                    MessageBox.Show("Format bulan tahun salah")
                    Return False
                End If
                If IsNumeric(txtPriceRefDate.Text.Trim) Then
                    If Len(txtPriceRefDate.Text.Trim) = 6 Then
                        If Mid(txtPriceRefDate.Text.Trim, 3, 2) <> "" Then
                            If CInt(Mid(txtPriceRefDate.Text.Trim, 3, 2)) < 20 Then
                                MessageBox.Show("Format bulan tahun salah")
                                Return False
                            End If
                        End If
                        If CInt(Left(txtPriceRefDate.Text, 2)) < 1 OrElse CInt(Left(txtPriceRefDate.Text, 2)) > 12 Then
                            MessageBox.Show("Format bulan salah")
                            Return False
                        End If
                        dtePriceRefDate = CDate("01" & "/" & Left(txtPriceRefDate.Text, 2) & "/" & Mid(txtPriceRefDate.Text, 3, 4))
                    Else
                        MessageBox.Show("Format bulan tahun salah")
                        Return False
                    End If
                Else
                    MessageBox.Show("Format bulan tahun salah")
                    Return False
                End If
            Else
                dtePriceRefDate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
            End If
        End If

        If Len(txtDeliveryTime.Text.Trim) < 6 Then
            MessageBox.Show("Format bulan tahun salah")
            Return False
        End If
        If IsNumeric(txtDeliveryTime.Text.Trim) Then
            If Len(txtDeliveryTime.Text.Trim) = 6 Then
                If Mid(txtDeliveryTime.Text.Trim, 3, 2) < 20 Then
                    MessageBox.Show("Format bulan tahun salah")
                    Return False
                End If
                If CInt(Left(txtDeliveryTime.Text, 2)) < 1 OrElse CInt(Left(txtDeliveryTime.Text, 2)) > 12 Then
                    MessageBox.Show("Format bulan salah")
                    Return False
                End If
            Else
                MessageBox.Show("Format bulan tahun salah")
                Return False
            End If
        Else
            MessageBox.Show("Format bulan tahun salah")
            Return False
        End If

        Return True
    End Function

    Private Function ValidateDate(ByVal periods As String) As Boolean
        Dim bcheck As Boolean = True
        Try
            Dim dd1 As Date = GetDateFromMonthYear(periods.Trim(), 1)
        Catch ex As Exception
            bcheck = False
        End Try
        Return bcheck
    End Function
    Private Function ValidateDealers(ByVal _dealers As String) As Boolean
        Dim bcheck As Boolean = True
        Dim i As Integer
        Dim items() As String = _dealers.Split(";")
        For i = 0 To items.Length - 2
            Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(items(i))
            If objDealerTmp.ID = 0 Then
                MessageBox.Show("Dealer " + items(i) + "tidak valid")
                bcheck = False
                Exit For
            End If

        Next
        If ValidateDealerDuplication(_dealers) <> String.Empty Then
            MessageBox.Show("Duplikasi Dealer " + ValidateDealerDuplication(_dealers))
            bcheck = False
        End If
        Return bcheck
    End Function
    Private Function ValidateDealerDuplication(ByVal _dealers As String) As String
        Dim bcheck As Boolean = True
        Dim _dealerDuplicate As String = String.Empty
        Dim i As Integer
        Dim j As Integer
        Dim list() As String = _dealers.Split(";")
        For i = 0 To list.Length - 2
            For j = i + 1 To list.Length - 1
                If list(i) = list(j) Then
                    bcheck = False
                    Exit For
                End If
            Next
            If bcheck = False Then
                _dealerDuplicate = list(i)
                Exit For
            End If
        Next
        Return _dealerDuplicate
    End Function
    Private Function ValidateDuplication(ByVal modelID As String, ByVal kodeType As String, ByVal _period As Date, ByVal Mode As String, ByVal Rowindex As Integer) As Boolean
        Dim bcheck As Boolean = True
        Dim arlSPLDetaillist As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
        If arlSPLDetaillist Is Nothing Then arlSPLDetaillist = New ArrayList

        If (Mode = "Add") Then
            If Not arlSPLDetaillist Is Nothing Then
                For Each item As SPLDetail In arlSPLDetaillist
                    If (item.ModelID = modelID And item.VechileType.VechileTypeCode.ToString = kodeType And item.PeriodMonth = _period.Month And item.PeriodYear = _period.Year) Then
                        MessageBox.Show("Error : Duplikasi Kode Tipe")
                        bcheck = False
                    End If

                Next
            End If
        Else
            Dim i As Integer = 0
            For Each item As SPLDetail In arlSPLDetaillist
                If (item.ModelID = modelID And item.VechileType.VechileTypeCode.ToString = kodeType And item.PeriodMonth = _period.Month And item.PeriodYear = _period.Year) Then
                    If i <> Rowindex Then
                        MessageBox.Show("Error : Duplikasi Kode Tipe")
                        bcheck = False
                    End If
                End If
                i = i + 1
            Next
        End If
        Return bcheck
    End Function

    Sub dgSPDetail_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dgSPDetail.ItemDataBound
        Dim _arlSPLDetailtoSPLView As ArrayList = CType(sessHelper.GetSession("SPLVIEW"), ArrayList)

        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                SetDgSPLDetailItemView(e, _arlSPLDetailtoSPLView)
                Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
                lbtnDelete.Visible = False
                Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")
                lbtnEdit.Visible = False
            End If
        Else
            If e.Item.ItemType = ListItemType.Footer Then
                SetDgSPLDetailItemFooter(e)
            ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                SetDgSPLDetailItemEdit(e, _arlSPLDetailtoSPLView)
            ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                SetDgSPLDetailItemView(e, _arlSPLDetailtoSPLView)
            End If
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Anda yakin data ini ingin dihapus?');")
            End If
        End If

    End Sub
    Private Sub SetDgSPLDetailItemView(ByVal e As DataGridItemEventArgs, ByVal arrSPLDetailtoSPLView As ArrayList)
        If Not arrSPLDetailtoSPLView Is Nothing Then
            Dim intItemIndexx As Integer = 0
            Dim _objSPLTmp As New SPL
            Dim _objSPLDetailtoSPL As New SPLDetailtoSPL
            _objSPLTmp = New SPLFacade(User).Retrieve(CInt(sessHelper.GetSession("IDSPLHeader")))

            arlSPLDetailList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
            If IsNothing(arlSPLDetailList) Then arlSPLDetailList = New ArrayList

            _objSPLDetailtoSPL = CType(arrSPLDetailtoSPLView(e.Item.ItemIndex), SPLDetailtoSPL)

            Dim strLabelTotalDisc As String = If(IsNothing(_objSPLDetailtoSPL.LabelTotal), "", _objSPLDetailtoSPL.LabelTotal)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo = CType(e.Item.FindControl("lblNo"), Label)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndexx") = e.Item.ItemIndex
                intItemIndexx = CType(ViewState("ItemIndexx"), Integer)
                lblNo.Text = intItemIndexx + 1 + (dgSPDetail.CurrentPageIndex * dgSPDetail.PageSize)
                ViewState("ItemIndexx") = lblNo.Text
            Else
                If strLabelTotalDisc.Trim.ToLower = "total :" Then
                    ViewState("ItemIndexx") = 0
                Else
                    intItemIndexx = CType(ViewState("ItemIndexx"), Integer)
                    lblNo.Text = intItemIndexx + 1 + (dgSPDetail.CurrentPageIndex * dgSPDetail.PageSize)
                    ViewState("ItemIndexx") = lblNo.Text
                End If
            End If

            Dim arlSPLDetail As ArrayList
            Dim i% = 0
            Dim _objSPLDetail As New SPLDetail
            If arlSPLDetailList.Count > 0 Then
                For Each objSPLDetail As SPLDetail In arlSPLDetailList
                    If objSPLDetail.VechileType.ID = _objSPLDetailtoSPL.VechileTypeID AndAlso objSPLDetail.ModelID = _objSPLDetailtoSPL.ModelID AndAlso _
                        objSPLDetail.PeriodMonth = _objSPLDetailtoSPL.PeriodMonth AndAlso objSPLDetail.PeriodYear = _objSPLDetailtoSPL.PeriodYear Then
                        _objSPLDetail = objSPLDetail
                        Exit For
                    End If
                    i += 1
                Next
            End If

            Dim _lblModelKendaraan As Label = CType(e.Item.FindControl("lblModelKendaraan"), Label)
            Dim _lblNamaTipe As Label = CType(e.Item.FindControl("lblNamaType"), Label)
            Dim _lblViewInterest As Label = CType(e.Item.FindControl("lblViewInterest"), Label)
            Dim _lblPeriodMonth As Label = CType(e.Item.FindControl("lblPeriodMonth"), Label)
            Dim _lblPeriodYear As Label = CType(e.Item.FindControl("lblPeriodYear"), Label)
            Dim lblDeliveryTime As Label = CType(e.Item.FindControl("lblDeliveryTime"), Label)
            Dim lblSisaUnit As Label = CType(e.Item.FindControl("lblSisaUnit"), Label)
            Dim lblViewPK As Label = CType(e.Item.FindControl("lblViewPK"), Label)

            Dim lblViewUnit As Label = CType(e.Item.FindControl("lblViewUnit"), Label)
            Dim lblViewDiscountType As Label = CType(e.Item.FindControl("lblViewDiscountType"), Label)
            Dim lblViewApplicationNo As Label = CType(e.Item.FindControl("lblViewApplicationNo"), Label)
            Dim lblViewDetailDiscount As Label = CType(e.Item.FindControl("lblViewDetailDiscount"), Label)
            Dim lblViewPriceReff As Label = CType(e.Item.FindControl("lblViewPriceRefDate"), Label)
            Dim lblViewTOP As Label = CType(e.Item.FindControl("lblViewTOP"), Label)
            Dim lblViewDeliveryTime As Label = CType(e.Item.FindControl("lblViewDeliveryTime"), Label)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            Dim strLabelTotal As String = ""
            If Not IsNothing(_objSPLDetailtoSPL.LabelTotal) Then
                strLabelTotal = _objSPLDetailtoSPL.LabelTotal
            End If
            If strLabelTotal.ToLower <> "total :" Then
                _objSPLDetail.SPL = _objSPLTmp
                Dim ObjVechileType As VechileType = New VechileTypeFacade(User).Retrieve(_objSPLDetail.VechileType.ID)
                If IsNothing(ObjVechileType) Then ObjVechileType = New VechileType

                Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, ObjVechileType.VechileModel.ID))
                Dim arrSCVToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
                If Not IsNothing(arrSCVToModel) AndAlso arrSCVToModel.Count > 0 Then
                    objSubCategoryVehicleToModel = CType(arrSCVToModel(0), SubCategoryVehicleToModel)
                End If

                _lblModelKendaraan.Text = objSubCategoryVehicleToModel.SubCategoryVehicle.Name
                _lblNamaTipe.Text = ObjVechileType.Description
                _lblViewInterest.Text = CType(_objSPLDetail.FreeIntIndicator, SPLEnum.Interest).ToString.Replace("_", " ")

                _lblPeriodMonth.Text = _objSPLDetail.PeriodMonth.ToString
                _lblPeriodYear.Text = _objSPLDetail.PeriodYear.ToString
                lblDeliveryTime.Text = Format(_objSPLDetail.DeliveryDate, "dd/MM/yyyy")

                lblViewPK.Attributes("onclick") = "showPopUp('../FinishUnit/FrmPKHeaderSPL.aspx?_splnumber=" & _objSPLTmp.SPLNumber & "&_kodetipe=" & _objSPLDetail.VechileType.VechileTypeCode & "&_periodemonth=" & _objSPLDetail.PeriodMonth & "&_periodeyear=" & _objSPLDetail.PeriodYear & "','',400,500,'');"

                lblSisaUnit.Text = (_objSPLDetail.Quantity - SPLFunction.GetResponseQtyPKDetail(_objSPLDetail)).ToString

                lblViewUnit.Text = _objSPLDetail.Quantity
                lblViewDiscountType.Text = _objSPLDetailtoSPL.DiscountMaster.Category
                lblViewApplicationNo.Text = If(Not IsNothing(_objSPLDetailtoSPL.SPLDetailReference), If(Not IsNothing(_objSPLDetailtoSPL.SPLDetailReference.SPL), _objSPLDetailtoSPL.SPLDetailReference.SPL.SPLNumber, ""), "")
                lblViewDetailDiscount.Text = Format(_objSPLDetailtoSPL.Discount, "#,##0")
                lblViewPriceReff.Text = If(_objSPLDetail.PriceRefDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value, "Update Price", _objSPLDetail.PriceRefDate.ToString("MMyyyy"))
                lblViewTOP.Text = _objSPLDetail.MaxTopDay
                lblViewDeliveryTime.Text = enumMonthGet.GetName(_objSPLDetail.DeliveryDate.Month) & " " & _objSPLDetail.DeliveryDate.Year.ToString("d4")
            Else
                e.Item.Cells(2).BackColor = Color.SkyBlue
                e.Item.Cells(3).BackColor = Color.SkyBlue
                e.Item.Cells(4).BackColor = Color.SkyBlue
                e.Item.Cells(5).BackColor = Color.SkyBlue
                e.Item.Cells(6).BackColor = Color.SkyBlue
                e.Item.Cells(7).BackColor = Color.SkyBlue
                e.Item.Cells(8).BackColor = Color.SkyBlue
                e.Item.Cells(9).BackColor = Color.SkyBlue
                e.Item.Cells(10).BackColor = Color.SkyBlue
                e.Item.Cells(11).BackColor = Color.SkyBlue
                e.Item.Cells(12).BackColor = Color.SkyBlue
                e.Item.Cells(13).BackColor = Color.SkyBlue
                e.Item.Cells(14).BackColor = Color.SkyBlue
                lblViewApplicationNo.Font.Bold = True
                lblViewDetailDiscount.Font.Bold = True

                _lblNamaTipe.Text = ""
                _lblModelKendaraan.Text = ""
                lblViewUnit.Text = ""
                lblViewDiscountType.Text = ""
                lblViewApplicationNo.Text = _objSPLDetailtoSPL.LabelTotal
                lblViewDetailDiscount.Text = Format(_objSPLDetailtoSPL.TotalDiscount, "#,##0")
                lblViewPriceReff.Text = ""
                lblViewTOP.Text = ""
                _lblViewInterest.Text = ""
                lblViewDeliveryTime.Text = ""
                e.Item.Cells(0).Text = ""
                lbtnEdit.Attributes("style") = "display:none"
                lbtnDelete.Attributes("style") = "display:none"
                lblViewPK.Attributes("style") = "display:none"
            End If
        End If
    End Sub

    Private Sub dgSPDetail_EditCommand(ByVal e As DataGridCommandEventArgs)
        blnCommandEdit = True
        dgSPDetail.ShowFooter = False
        dgSPDetail.EditItemIndex = CInt(e.Item.ItemIndex)
        BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
    End Sub
    Private Sub dgSPDetail_CancelCommand(ByVal E As DataGridCommandEventArgs)
        dgSPDetail.EditItemIndex = -1
        BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
        dgSPDetail.ShowFooter = True
    End Sub
    Private Sub dgSPDetail_Update(ByVal e As DataGridCommandEventArgs)
        UpdateCommand(e)
    End Sub
    Private Function GetIndexSPLDETAILLIST(ByVal _list As ArrayList, ByVal _obj As SPLDetailtoSPL) As Integer
        Dim i As Integer = 0
        Dim intSPLDetailRefID1 As Integer = 0
        Dim intSPLDetailRefID2 As Integer = 0
        For Each item As SPLDetailtoSPL In _list
            intSPLDetailRefID1 = 0
            intSPLDetailRefID2 = 0
            If Not IsNothing(item.SPLDetailReference) Then
                intSPLDetailRefID1 = item.SPLDetailReference.ID
            End If
            If Not IsNothing(_obj.SPLDetailReference) Then
                intSPLDetailRefID2 = _obj.SPLDetailReference.ID
            End If
            If item.VechileTypeID = _obj.VechileTypeID And item.ModelID = _obj.ModelID And item.PeriodMonth = _obj.PeriodMonth _
                And item.PeriodYear = _obj.PeriodYear And intSPLDetailRefID1 = intSPLDetailRefID2 And item.DiscountMaster.ID = _obj.DiscountMaster.ID Then
                Exit For
            End If
            i = i + 1
        Next
        Return i
    End Function
    Private Sub UpdateCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Exit Sub
        End If
        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            dgSPDetail.EditItemIndex = -1
            BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
        Else
            Dim arlSPLDetailToSPL As ArrayList = CType(sessHelper.GetSession("SPLDETAILTOSPLLIST"), ArrayList)
            If IsNothing(arlSPLDetailToSPL) Then arlSPLDetailToSPL = New ArrayList
            Dim arlSPLDetail As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
            If IsNothing(arlSPLDetail) Then arlSPLDetail = New ArrayList

            Dim ddlEditModelKendaraan As DropDownList = CType(e.Item.FindControl("ddlEditModelKendaraan"), DropDownList)
            Dim txtEditTipeKendaraan As System.Web.UI.WebControls.TextBox = e.Item.FindControl("txtEditTipeKendaraan")
            Dim txtEditUnit As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditUnit"), System.Web.UI.WebControls.TextBox)
            Dim ddlEditDiscountType As DropDownList = CType(e.Item.FindControl("ddlEditDiscountType"), DropDownList)
            Dim txtEditApplicationNo As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditApplicationNo"), System.Web.UI.WebControls.TextBox)
            Dim txtEditDetailDiscount As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditDetailDiscount"), System.Web.UI.WebControls.TextBox)
            Dim txtEditPriceRefDate As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditPriceRefDate"), System.Web.UI.WebControls.TextBox)
            Dim txtEditTOP As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditTOP"), System.Web.UI.WebControls.TextBox)
            Dim txtEditDeliveryTime As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtEditDeliveryTime"), System.Web.UI.WebControls.TextBox)
            Dim hdnEditSPLID As HiddenField = CType(e.Item.FindControl("hdnEditSPLID"), HiddenField)
            Dim ddlEditInterest As DropDownList = CType(e.Item.FindControl("ddlEditInterest"), DropDownList)
            Dim oSPLDetail As New SPLDetail

            Dim dtePriceRefDate As Date
            If ValidateData(ddlEditModelKendaraan, txtEditTipeKendaraan, txtEditUnit, ddlEditDiscountType, txtEditApplicationNo, txtEditDetailDiscount, _
                            txtEditPriceRefDate, txtEditTOP, txtEditDeliveryTime, dtePriceRefDate) Then

                If txtEditApplicationNo.Text.Trim <> "" Then
                    Dim criterias0 As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias0.opAnd(New Criteria(GetType(SPL), "ApprovalStatus", MatchType.Exact, 5))
                    'criterias0.opAnd(New Criteria(GetType(SPL), "DealerName", MatchType.[Partial], txtDealerCode.Text))
                    Dim strSQL As String = String.Empty
                    If ddlEditDiscountType.SelectedValue <> "" Then
                        strSQL = "Select distinct a.ID from SPL a join SPLDetail b on a.ID = b.SPLID and b.RowStatus = 0 "
                        strSQL += "join SPLDetailtoSPL c on b.ID = c.SPLDetailID and c.RowStatus = 0 "
                        strSQL += "where a.RowStatus = 0 And c.DiscountMasterID = " & ddlEditDiscountType.SelectedValue
                        criterias0.opAnd(New Criteria(GetType(SPL), "ID", MatchType.InSet, "(" & strSQL & ")"))
                    End If
                    If txtEditTipeKendaraan.Text.Trim <> "" Then
                        strSQL = "Select distinct a.ID From SPL a join SPLDetail b on a.ID = b.SPLID join VechileType c on b.VehicleTypeID = c.ID "
                        strSQL += "Where a.RowStatus = 0 And b.RowStatus = 0 And c.VechileTypeCode = '" & txtEditTipeKendaraan.Text & "'"
                        criterias0.opAnd(New Criteria(GetType(SPL), "ID", MatchType.InSet, "(" & strSQL & ")"))
                    End If
                    criterias0.opAnd(New Criteria(GetType(SPL), "SPLNumber", MatchType.[Partial], txtEditApplicationNo.Text))
                    Dim arrSPL As ArrayList = New SPLFacade(User).Retrieve(criterias0)
                    If IsNothing(arrSPL) OrElse (Not IsNothing(arrSPL) AndAlso arrSPL.Count = 0) Then
                        MessageBox.Show("Application No. tidak ada di data Pencarian Aplikasi")
                        txtEditApplicationNo.Focus()
                        Exit Sub
                    End If
                End If

                ObjSPLDetailtoSPL = New SPLDetailtoSPL
                Dim arlSPLDetailToSPLView As ArrayList = CType(sessHelper.GetSession("SPLVIEW"), ArrayList)
                ObjSPLDetailtoSPL = CType(arlSPLDetailToSPLView(e.Item.ItemIndex), SPLDetailtoSPL)
                IndexList = GetIndexSPLDETAILLIST(arlSPLDetailToSPL, ObjSPLDetailtoSPL)

                For Each objSPLDetail As SPLDetail In arlSPLDetail
                    If objSPLDetail.VechileTypeID = ObjSPLDetailtoSPL.VechileTypeID AndAlso objSPLDetail.ModelID = ObjSPLDetailtoSPL.ModelID AndAlso _
                        objSPLDetail.PeriodMonth = ObjSPLDetailtoSPL.PeriodMonth AndAlso objSPLDetail.PeriodYear = ObjSPLDetailtoSPL.PeriodYear Then
                        oSPLDetail = objSPLDetail
                        Exit For
                    End If
                Next

                Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, oSPLDetail.VechileType.VechileModel.ID))
                Dim arrSCVToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
                If Not IsNothing(arrSCVToModel) AndAlso arrSCVToModel.Count > 0 Then
                    objSubCategoryVehicleToModel = CType(arrSCVToModel(0), SubCategoryVehicleToModel)
                End If

                Dim strSPLID As String = String.Empty
                Dim ObjSPLDetailReference As SPLDetail
                Try
                    Dim oVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtEditTipeKendaraan.Text)
                    If IsNothing(oVechileType) Then oVechileType = New VechileType
                    If objSubCategoryVehicleToModel.SubCategoryVehicle.ID.ToString() = ddlEditModelKendaraan.SelectedValue Then
                        If oSPLDetail.VechileType.ID = oVechileType.ID Then
                            If ObjSPLDetailtoSPL.DiscountMaster.ID.ToString() = ddlEditDiscountType.SelectedValue Then
                                If Not IsNothing(ObjSPLDetailtoSPL.SPLDetailReference) Then
                                    ObjSPLDetailReference = ObjSPLDetailtoSPL.SPLDetailReference
                                    If Not IsNothing(ObjSPLDetailReference.SPL) Then
                                        strSPLID = ObjSPLDetailReference.SPL.ID.ToString
                                    End If
                                End If
                                If strSPLID = hdnEditSPLID.Value Then
                                    If ObjSPLDetailtoSPL.NumberRow <> (e.Item.ItemIndex + 1) Then
                                        MessageBox.Show("Model, Tipe, Discount Type dan Application Number sudah pernah di input.")
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If
                    End If
                Catch
                End Try

                With ObjSPLDetailtoSPL
                    ObjSPLDetailReference = New SPLDetail
                    If hdnEditSPLID.Value <> "" Then
                        Dim criterias0 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias0.opAnd(New Criteria(GetType(SPLDetail), "SPL.ID", MatchType.Exact, hdnEditSPLID.Value))
                        criterias0.opAnd(New Criteria(GetType(SPLDetail), "VechileType.VechileTypeCode", MatchType.Exact, txtEditTipeKendaraan.Text.Trim))
                        Dim arrSPLDtl As ArrayList = New SPLDetailFacade(User).Retrieve(criterias0)
                        If Not IsNothing(arrSPLDtl) AndAlso arrSPLDtl.Count > 0 Then
                            ObjSPLDetailReference = CType(arrSPLDtl(0), SPLDetail)
                        End If
                    End If
                    .SPLDetailReference = ObjSPLDetailReference
                    .DiscountMaster = New DiscountMasterFacade(User).Retrieve(CInt(ddlEditDiscountType.SelectedValue))
                    .Discount = txtEditDetailDiscount.Text
                    .DiscountMaster = New DiscountMasterFacade(User).Retrieve(CInt(ddlEditDiscountType.SelectedValue))
                    .Discount = txtEditDetailDiscount.Text

                    Dim oVechileType As VechileType = New VechileTypeFacade(User).Retrieve(txtEditTipeKendaraan.Text.Trim)
                    .VechileTypeID = oVechileType.ID
                    .ModelID = ddlEditModelKendaraan.SelectedValue
                    .PeriodMonth = CType(sessHelper.GetSession("STATUSMONTH"), Date).Month
                    .PeriodYear = CType(sessHelper.GetSession("STATUSMONTH"), Date).Year

                    Dim objSPL As SPL
                    If Not IsNothing(sessHelper.GetSession("IDSPLHeader")) Then
                        objSPL = New SPLFacade(User).Retrieve(CInt(sessHelper.GetSession("IDSPLHeader")))
                    End If
                    oSPLDetail.SPL = objSPL
                    oSPLDetail.VechileType = oVechileType
                    oSPLDetail.Quantity = txtEditUnit.Text
                    oSPLDetail.PriceRefDate = dtePriceRefDate
                    oSPLDetail.MaxTopDay = txtEditTOP.Text
                    oSPLDetail.FreeIntIndicator = ddlEditInterest.SelectedValue
                    oSPLDetail.DeliveryDate = CDate("01" & "/" & Left(txtEditDeliveryTime.Text, 2) & "/" & Mid(txtEditDeliveryTime.Text, 3, 4))
                    oSPLDetail.VechileTypeID = oVechileType.ID
                    oSPLDetail.ModelID = ddlEditModelKendaraan.SelectedValue
                    oSPLDetail.PeriodMonth = CType(sessHelper.GetSession("STATUSMONTH"), Date).Month
                    oSPLDetail.PeriodYear = CType(sessHelper.GetSession("STATUSMONTH"), Date).Year

                    If txtEditTOP.Text > 0 Then
                        oSPLDetail.MaxTopIndicator = 1
                    Else
                        oSPLDetail.MaxTopIndicator = 0
                    End If

                    .SPLDetail = oSPLDetail
                End With

                If Not arlSPLDetailToSPLView Is Nothing Then
                    arlSPLDetailToSPL.RemoveAt(IndexList)
                    arlSPLDetailToSPL.Insert(IndexList, ObjSPLDetailtoSPL)
                End If
                Call UpdateDiscountSPLDetail(oSPLDetail, arlSPLDetailToSPLView)

                dgSPDetail.EditItemIndex = -1
                BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
                dgSPDetail.ShowFooter = True
            End If
        End If
    End Sub

    Private Function GetIndexSPLDETAILTOSPLLIST(ByVal _list As ArrayList, ByVal _obj As SPLDetailtoSPL) As Integer
        Dim i As Integer = 0
        For Each item As SPLDetailtoSPL In _list
            If item.SPLDetail.VechileType.VechileTypeCode = _obj.SPLDetail.VechileType.VechileTypeCode And _
                item.SPLDetail.PeriodMonth = _obj.SPLDetail.PeriodMonth And item.SPLDetail.PeriodYear = _obj.SPLDetail.PeriodYear Then
                Exit For
            End If
            i = i + 1
        Next
        Return i
    End Function
    Sub lbtnPrevMonth_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lbtnPrevMonth.Click
        Dim _prevDate As Date
        If Not sessHelper.GetSession("STATUSMONTH") Is Nothing Then
            Dim _sessDate As Date = CType(sessHelper.GetSession("STATUSMONTH"), Date).Date
            Dim _sessMinDate As Date = CType(sessHelper.GetSession("STATUSMINMONTH"), Date).Date
            If _sessDate.Month = 1 Then
                _prevDate = GetDateFromMonthYear((12).ToString + (_sessDate.Year - 1).ToString, 1)
            Else
                _prevDate = GetDateFromMonthYear((_sessDate.Month - 1).ToString + _sessDate.Year.ToString, 1)
            End If

            If DateDiff(DateInterval.Day, _sessMinDate, _prevDate, Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1) >= 0 Then
                sessHelper.SetSession("STATUSMONTH", _prevDate)
                lblCurrentPeriode.Text = CType(_prevDate.Month - 1, enumMonth.Month).ToString.Replace("_", " ") + " " + _prevDate.Year.ToString
                BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
            Else
                MessageBox.Show("Periode Detil Melampaui Periode Aplikasi")
            End If
        End If
    End Sub
    Sub lbtnNextMonth_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lbtnNextMonth.Click
        Dim _nextDate As Date
        If Not sessHelper.GetSession("STATUSMONTH") Is Nothing Then
            Dim _sessDate As Date = CType(sessHelper.GetSession("STATUSMONTH"), Date).Date
            Dim _sessMaxDate As Date = CType(sessHelper.GetSession("STATUSMAXMONTH"), Date).Date
            If _sessDate.Month = 12 Then
                _nextDate = GetDateFromMonthYear((1).ToString + (_sessDate.Year + 1).ToString, 1)
            Else
                _nextDate = GetDateFromMonthYear((_sessDate.Month + 1).ToString + _sessDate.Year.ToString, 1)
            End If
            If DateDiff(DateInterval.Day, _nextDate, _sessMaxDate, Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1) >= 0 Then
                sessHelper.SetSession("STATUSMONTH", _nextDate)
                lblCurrentPeriode.Text = CType(_nextDate.Month - 1, enumMonth.Month).ToString.Replace("_", " ") + " " + _nextDate.Year.ToString
                BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
            Else
                MessageBox.Show("Periode Detil Melampaui Periode Aplikasi")
            End If
        End If
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
        If ddlStatus.SelectedValue = 1 Then
            lbtnNextMonth.Visible = False
            lbtnPrevMonth.Visible = False
            dgSPDetail.ShowFooter = False
        Else
            lbtnNextMonth.Visible = True
            lbtnPrevMonth.Visible = True
            dgSPDetail.ShowFooter = True
            BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
        End If
    End Sub

    ''copy data untuk perpanjangan periode CR Discount Proposal
    Private Function setSisaQty(ByVal arrObj As ArrayList) As ArrayList
        Dim arrReturn As ArrayList = New ArrayList()
        For Each item As SPLDetail In arrObj
            item.SisaQty = item.Quantity - SPLFunction.GetResponseQtyPKDetail(item)
            arrReturn.Add(item)
        Next

        Return arrReturn
    End Function

    Private Function BlankToZerro(ByVal _valueProperty As String) As Double
        If Len(_valueProperty.Trim) > 0 Then
            _valueProperty = Replace(Replace(_valueProperty.Trim, ".", ""), ",", "")
            If _valueProperty.Trim = "" OrElse _valueProperty.Trim <= 0 Then
                Return 0
            Else
                Return Format(CDbl(_valueProperty), "#,##0")
            End If
        Else
            Return 0
        End If
    End Function


#End Region

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        RegisterStartupScript("OpenWindow", "<script>KonfirmasiSimpan();</script>")
    End Sub
End Class
