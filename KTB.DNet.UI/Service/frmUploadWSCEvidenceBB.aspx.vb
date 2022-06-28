#Region "Custom Namespace Imports"
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Configuration

#End Region

Public Class frmUploadWSCEvidenceBBBB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorWSC As System.Web.UI.WebControls.Label
    Protected WithEvents lblLampiranBukti As System.Web.UI.WebControls.Label
    Protected WithEvents lblKeterangan As System.Web.UI.WebControls.Label
    Protected WithEvents txtKeterangan As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents DataFile1 As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtNomorWSC As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnClear As System.Web.UI.WebControls.Button
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTerm1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlEvidenceType As System.Web.UI.WebControls.DropDownList

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
    Private objWSC As WSCHeaderBB
    Private sessionHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub CopyFileToServer()
        If (Not DataFile1.PostedFile.FileName = String.Empty) Then
            If CopyFile(DataFile1) Then
                Label1.Text = "Sukses"
            Else
                Label1.Text = "Gagal (Maks. 3-MBytes)"
            End If
        Else
            Label1.Text = ""
        End If

        'If (Not DataFile2.PostedFile.FileName = String.Empty) Then
        '    If CopyFile(DataFile2) Then
        '        Label3.Text = "Sukses"
        '    Else
        '        Label3.Text = "Gagal"
        '    End If
        'Else
        '    Label3.Text = ""
        'End If

        'If (Not DataFile3.PostedFile.FileName = String.Empty) Then
        '    If CopyFile(DataFile3) Then
        '        Label5.Text = "Sukses"
        '    Else
        '        Label5.Text = "Gagal"
        '    End If
        'Else
        '    Label5.Text = ""
        'End If

    End Sub

    Private Sub RetrieveWSC()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.WSCHeaderBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCHeaderBB), "Dealer.DealerCode", MatchType.Exact, lblDealerCode.Text))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCHeaderBB), "ClaimNumber", MatchType.Exact, txtNomorWSC.Text))
        Dim arlWSC As ArrayList = New WSCHeaderBBFacade(User).Retrieve(criterias)
        If arlWSC.Count > 0 Then
            objWSC = arlWSC(0)
            'Modified (Remark) by Anh, August 18, 2010
            'Request by Rina
            'Validation per row on server
            'Start -----------------------
            'If (objWSC.PQR.Trim() <> String.Empty) Then
            'Dim oPQRHeader As PQRHeader = New PQRHeader
            'oPQRHeader = New PQRHeaderFacade(User).Retrieve(objWSC.PQR)
            'If (oPQRHeader.PQRNo.Trim() <> String.Empty) Then
            'CopyFileToServer()
            'Else
            '    MessageBox.Show("Nomor PQR Tidak Ditemukan")
            'End If
            'Else
            '    CopyFileToServer()
            'End If
            CopyFileToServer()
            'End--------------------------
        Else
            MessageBox.Show("Nomor WSC Tidak Ditemukan")
        End If

    End Sub

    Private Function CopyFile(ByVal DataFile As HtmlInputFile) As Boolean
        If (Not DataFile1.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) And (DataFile.PostedFile.ContentLength < 3000000) Then
            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName) '-- Source file name
            Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceBBFileDirectory") & "\" & objWSC.Dealer.DealerCode & "\" & objWSC.ClaimNumber & "\" & "SS" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)  '-- Destination file

            'Dim fileInfo1 As New FileInfo(Server.MapPath(""))
            Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))

            Dim DestFullFilePath As String = fileInfo1.Directory.FullName & "\" & DestFile '-- Destination file
            Dim fileInfoDestination As New FileInfo(DestFullFilePath)
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim success As Boolean = False
            Dim helper As FileHelper = New FileHelper
            Try
                success = imp.Start()
                If success Then
                    If Not fileInfoDestination.Exists Then
                        fileInfoDestination.Directory.Create()
                    End If
                    DataFile.PostedFile.SaveAs(DestFullFilePath)
                    'CopytoAnOtherWebServer(fileInfoDestination, objWSC.Dealer.DealerCode, objWSC.ClaimNumber)
                    imp.StopImpersonate()
                    imp = Nothing
                    SaveToDatabase(DestFile)
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End If
        Return False
    End Function

    Private Sub CopytoAnOtherWebServer(ByVal finfo As FileInfo, ByVal dealerCode As String, ByVal ClaimNumber As String)
        Dim helper As FileHelper = New FileHelper
        Dim otherFolder As String = dealerCode & "\" & ClaimNumber
        helper.TransferToListWebServer(finfo, otherFolder, True, "WSCDirectory")

    End Sub

    Private Sub SaveToDatabase(ByVal DestFile As String)
        'Dim SrcFile As String = Path.GetFullPath(DataFile.PostedFile.FileName)
        Dim objWSCEvidenceBB As New WSCEvidenceBB
        objWSCEvidenceBB.EvidenceType = ddlEvidenceType.SelectedValue
        objWSCEvidenceBB.Description = txtKeterangan.Text
        objWSCEvidenceBB.PathFile = DestFile
        objWSCEvidenceBB.UploadDate = DateTime.Now.Day
        objWSCEvidenceBB.UploadMonth = DateTime.Now.Month
        objWSCEvidenceBB.UploadYear = DateTime.Now.Year
        objWSCEvidenceBB.WSCHeaderBB = objWSC
        Dim objWSCEvidenceBBFacade As New WSCEvidenceBBFacade(User)
        objWSCEvidenceBBFacade.Insert(objWSCEvidenceBB)
    End Sub

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        If Not IsPostBack Then

            Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
            lblDealerCode.Text = objDealer.DealerCode
            lblSearchTerm1.Text = objDealer.SearchTerm1

            BindWSCEvidenceType()
        End If
    End Sub

    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.WSBuktiListView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Upload Bukti WSC")
        End If
        If Not IsDownloaded() Then
            Server.Transfer("../FrmAccessDenied.aspx?mess=Anda belum melakukan download Kwitansi WSC  (Menu Daftar Dokumen Service)")
        End If
        btnUpload.Visible = SecurityProvider.Authorize(Context.User, SR.WSCBuktiUploadSave_Privilege)
        btnClear.Visible = SecurityProvider.Authorize(Context.User, SR.WSCBuktiUploadSave_Privilege)
    End Sub

    Private Function IsDownloaded() As Boolean
        Dim _return As Boolean = False
        Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        Dim ArlMonthly As ArrayList = New ArrayList
        Try
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.Exact, 1))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "CreatedTime", MatchType.GreaterOrEqual, Date.Now.AddMonths(-1)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "LastDownloadBy", MatchType.Exact, String.Empty))

            ArlMonthly = New MonthlyDocumentFacade(User).Retrieve(criterias)
            If Not IsNothing(ArlMonthly) AndAlso ArlMonthly.Count > 0 Then
                _return = False
            Else
                _return = True
            End If
        Catch ex As Exception
            _return = False
        End Try
        Return _return
    End Function

    Private Sub BindWSCEvidenceType()
        Dim _enumWSCEvidenceType As New EnumWSCEvidenceType
        Dim _arrTmp As New ArrayList
        _arrTmp = _enumWSCEvidenceType.WSCEvidenceTypeList

        ddlEvidenceType.DataSource = _arrTmp
        ddlEvidenceType.DataTextField = "WSCEvidenceTypeId"
        ddlEvidenceType.DataValueField = "WSCEvidenceTypeValue"
        ddlEvidenceType.DataBind()
        ddlEvidenceType.Items.Insert(0, "Pilih")
    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If (Not (DataFile1.PostedFile.FileName = String.Empty)) Then
            If ddlEvidenceType.SelectedIndex = 0 Then
                MessageBox.Show("Tipe Bukti harus dipilih")
            Else
                RetrieveWSC()
            End If
        Else
            MessageBox.Show("Lampiran Bukti harus diisi")
        End If

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtNomorWSC.Text = String.Empty
        txtKeterangan.Text = String.Empty
        DataFile1 = New HtmlInputFile
        'DataFile2 = New HtmlInputFile
        'DataFile3 = New HtmlInputFile
    End Sub

#End Region

End Class