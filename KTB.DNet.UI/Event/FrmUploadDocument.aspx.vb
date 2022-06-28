Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
Imports System.IO

Public Class FrmUploadDocument
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents txtEventNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents UploadFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchEvent As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label

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
        lblSearchDealer.Attributes("onclick") = "ShowDealerSelection();"
        lblSearchEvent.Attributes("onclick") = "ShowEventSelection();"
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If ValidateUpload() Then
            Dim objDoc As New EventDocument
            Dim objFacade As New EventDocumentFacade(User)
            objDoc.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
            objDoc.EventMaster = New EventMasterFacade(User).Retrieve(txtEventNumber.Text)
            Dim _filename As String = System.IO.Path.GetFileName(UploadFile.PostedFile.FileName)
            If SaveFile(_filename) Then
                objDoc.DocumentFile = _filename
                objDoc.DocumentCode = txtEventNumber.Text.Trim & "/" & txtDealerCode.Text.Trim & "/" & _filename
                Dim _nResult As Integer = 0
                Select Case CheckDocumentCode(objDoc.DocumentCode)
                    Case "Insert"
                        _nResult = objFacade.Insert(objDoc)
                    Case "Update"
                        _nResult = objFacade.Update(objDoc)
                End Select
                If _nResult > 0 Then
                    ClearData()
                    MessageBox.Show("Dokumen berhasil diupload")
                Else
                    MessageBox.Show("Dokumen gagal diupload")
                End If
            Else
                MessageBox.Show("File gagal diupload")
            End If
        End If
    End Sub
    Private Sub ClearData()
        txtEventNumber.Text = String.Empty
        txtDealerCode.Text = String.Empty
    End Sub
    Private Function SaveFile(ByVal _filename As String) As Boolean
        Dim nResult As Boolean = False
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("EventDir") & "\Dokumen\" & txtEventNumber.Text.Trim & "-" & txtDealerCode.Text.Trim & "\" & _filename      '-- Destination file
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

                UploadFile.PostedFile.SaveAs(DestFile)
                nResult = True
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            nResult = False
            Throw ex
        End Try
        Return nResult
    End Function
    Private Function ValidateUpload() As Boolean
        Dim bcheck As Boolean = True
        If txtDealerCode.Text = String.Empty Then
            MessageBox.Show("Kode dealer belum dipilih")
            bcheck = False
        End If

        If txtEventNumber.Text.Trim = String.Empty Then
            MessageBox.Show("Nomor event belum dipilih")
            bcheck = False
        End If

        If UploadFile.Value = String.Empty Then
            MessageBox.Show("Tidak ada file yg di pilih!")
            bcheck = False
        End If

        Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
        If UploadFile.PostedFile.ContentLength > maxFileSize Then
            MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
            bcheck = False
        End If

        Return bcheck
    End Function
    Private Function CheckDocumentCode(ByVal docCode As String) As String
        Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(EventDocument), "DocumentCode", MatchType.Exact, docCode))
        Dim _arr As ArrayList = New EventDocumentFacade(User).RetrieveByCriteria(criterias)
        If _arr.Count > 0 Then
            Return "Update"
        Else
            Return "Insert"
        End If
    End Function
End Class
