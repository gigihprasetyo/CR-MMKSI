Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.IO

Public Class FrmBabitProposalUploadRealization
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents phJS As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents lblNoPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblProvince As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoPerjanjian As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents phBottomScript As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents pnlScript As System.Web.UI.WebControls.Panel
    Protected WithEvents hdnSelectedVehicleType As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnSelectedVehicleTypeIndex As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnCancel As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents UploadFileRealization As System.Web.UI.HtmlControls.HtmlInputFile

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variables Declaration"
    Dim LoginDealer As New Dealer
    Private oBabitProposal As New BabitProposal
    Private sesHelper As New SessionHelper
#End Region



#Region "Custom Methods"
    Sub LoadData()
        oBabitProposal = CType(Session("BabitProposalRealization"), BabitProposal)
        lblNoPengajuan.Text = oBabitProposal.NoPengajuan
        lblDealerCode.Text = oBabitProposal.Dealer.DealerCode
        lblDealerName.Text = oBabitProposal.Dealer.DealerName
        lblCity.Text = oBabitProposal.Dealer.City.CityName
        lblProvince.Text = oBabitProposal.Dealer.Province.ProvinceName
        lblNoPerjanjian.Text = oBabitProposal.BabitAllocation.NoPerjanjian
        lblPeriode.Text = MonthName(oBabitProposal.BabitAllocation.Babit.StartPeriod, False) & " - " & MonthName(oBabitProposal.BabitAllocation.Babit.EndPeriod, False) & " " & oBabitProposal.BabitAllocation.Babit.BabitYear
    End Sub

    Private Function SaveFile(ByVal _filename As String) As Boolean
        Dim nResult As Boolean = False
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("RealisasiBabit") & "\" & _filename      '-- Destination file
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo
        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(DestFile)

                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                If finfo.Exists Then
                    finfo.Delete()
                End If
                UploadFileRealization.PostedFile.SaveAs(DestFile)

                '============= save upload data to database =========================
                oBabitProposal = CType(Session("BabitProposalRealization"), BabitProposal)
                oBabitProposal.BabitRealizationFile = _filename

                Dim iResult As Integer
                iResult = New BabitSalesComm.BabitProposalFacade(User).UpdateBabitProposal(oBabitProposal)
                '======================================================================

                If iResult > 0 Then
                    nResult = True
                Else
                    nResult = False
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            nResult = False
            Throw ex
        End Try
        Return nResult
    End Function
#End Region

#Region "Event Handlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        LoginDealer = CType(Session("DEALER"), Dealer)
        oBabitProposal = New BabitSalesComm.BabitProposalFacade(User).Retrieve(CInt(Request.QueryString("id")))
        sesHelper.SetSession("BabitProposalRealization", oBabitProposal)
        If (Not IsPostBack) Then
            LoadData()
        End If
    End Sub
    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        If UploadFileRealization.PostedFile Is Nothing OrElse UploadFileRealization.Value = String.Empty Then
            MessageBox.Show("Silakan pilih file yg akan di upload.")
            Return
        Else
            If UploadFileRealization.PostedFile.ContentLength > CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize")) Then
                MessageBox.Show("Ukuran file tidak boleh melebihi 500kb")
            Else
                Dim filename As String
                filename = "BabitRealization_" & oBabitProposal.NoPengajuan.Replace("/", "-") & "_" & System.IO.Path.GetFileName(UploadFileRealization.PostedFile.FileName)
                If SaveFile(filename) Then
                    MessageBox.Show("Proses upload berhasil.")
                    Response.Write("<script language=""javascript"">window.close();</script>")
                Else
                    MessageBox.Show("Proses upload file gagal.")
                End If
            End If
        End If
    End Sub

#End Region

End Class

