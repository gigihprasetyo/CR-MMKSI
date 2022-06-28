Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls

#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Claim
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.Drawing.Color

#End Region
Public Class FrmClaimEvidence
    Inherits System.Web.UI.Page

    Dim oDealer As Dealer
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEvidence As System.Web.UI.WebControls.DataGrid
    Dim sesHelper As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchClaim As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClaimNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents fileUpload As System.Web.UI.HtmlControls.HtmlInputFile

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            oDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
            lblDealerCode.Text = String.Format("{0} / {1}", oDealer.DealerCode.ToString(), oDealer.Province.ProvinceName)
            lblDealerName.Text = oDealer.DealerName
            lblSearchClaim.Attributes("onclick") = "ShowPPClaimSelectionOne();"
            btnUpload.Attributes.Add("onclick", New CommonFunction().PreventDoubleClick(btnUpload))
        End If
    End Sub

    Private Function IsInputValid() As String
        Dim sMsg As String = ""
        If Me.lblDealerCode.Text.Trim = String.Empty Then
            sMsg = sMsg & "Dealer Code TIdak Valid\n"
        End If
        If Me.txtClaimNo.Text.Trim = String.Empty Then
            sMsg = sMsg & "No Claim Wajib Diisi\n"
        End If
        If IsNothing(viewstate("id")) Then
            If Me.fileUpload.Value = String.Empty Then
                sMsg = sMsg & "File wajib dipilih\n"
            End If
        End If
        Return sMsg
    End Function

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click

        If txtClaimNo.Text.Trim = "" Then
            MessageBox.Show("Tentukan No Claim Dulu !")
        End If


        If fileUpload.Value <> "" OrElse fileUpload.Value <> Nothing Then
            'cek the maxfile first
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
            If fileUpload.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
            Else
                Dim SrcFile As String = Path.GetFileName(fileUpload.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ClaimEvidenceDir") & "\" & SrcFile  '-- Destination file
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        fileUpload.PostedFile.SaveAs(DestFile)
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            End If
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        'Dim obHeader As ClaimHeader
        'Dim objClaimheader As ClaimHeaderFacade = New ClaimHeaderFacade(User)

        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(ClaimHeader), "Dealer", MatchType.Exact, Code))

        'obHeader = objClaimheader.Retrieve()
        Try

        Catch ex As Exception

        End Try

    End Sub
End Class
