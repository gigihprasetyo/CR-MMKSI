Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.enumMode
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class PopUpReferensiHarga
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rbtnUpdatePrice As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents rbtnMonthYear As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents txtMonthYear As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnPilih As System.Web.UI.WebControls.Button

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
        Dim strPriceReff As String = String.Empty

        If Not IsNothing(Request.QueryString("priceReff")) Then
            strPriceReff = Request.QueryString("priceReff")
        End If
        If Not Page.IsPostBack Then
            If strPriceReff = "Update Price" Then
                rbtnUpdatePrice.Checked = True
            ElseIf strPriceReff <> "" AndAlso strPriceReff <> "Update Price" Then
                rbtnMonthYear.Checked = True
                txtMonthYear.Text = strPriceReff
            Else
                rbtnUpdatePrice.Checked = False
                rbtnMonthYear.Checked = False
                txtMonthYear.Text = ""
            End If
        End If
    End Sub

    Private Sub btnPilih_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPilih.Click
        If ValidateData() Then
            Dim selectedPriceReff As String = ""
            If rbtnMonthYear.Checked Then
                selectedPriceReff = txtMonthYear.Text.Trim
            ElseIf (rbtnUpdatePrice.Checked) Then
                selectedPriceReff = "Update Price"
            Else
                selectedPriceReff = ""
            End If
            Dim strScriptJS As String = String.Empty
            strScriptJS = "if ('" & selectedPriceReff & "' != '') {"
            strScriptJS += "if (navigator.appName != 'Microsoft Internet Explorer') {"
            strScriptJS += "window.close();"
            strScriptJS += "window.opener.dialogWin.returnFunc('" & selectedPriceReff & "');"
            strScriptJS += "}"
            strScriptJS += "else {"
            strScriptJS += "window.returnValue = '" & selectedPriceReff & "';"
            strScriptJS += "window.close();"
            strScriptJS += "}"
            strScriptJS += "}"
            strScriptJS += "else {"
            strScriptJS += "alert('Silahkan pilih Referensi Harga dahulu');"
            strScriptJS += "}"
            Response.Write("<script language='javascript'>" & strScriptJS & "</script>")
        End If
    End Sub

    Private Function ValidateData() As Boolean
        Dim bcheck As Boolean = True
        Try
            If rbtnMonthYear.Checked Then
                If Len(txtMonthYear.Text.Trim) < 6 Then
                    bcheck = False
                    MessageBox.Show("Format bulan tahun salah")
                    Exit Function
                End If
                If Mid(txtMonthYear.Text.Trim, 3, 2) < 20 Then
                    bcheck = False
                    MessageBox.Show("Format bulan tahun salah")
                    Exit Function
                End If
                If CInt(Left(txtMonthYear.Text, 2)) < 1 OrElse CInt(Left(txtMonthYear.Text, 2)) > 12 Then
                    bcheck = False
                    MessageBox.Show("Format bulan salah")
                    Exit Function
                End If
            End If
        Catch ex As Exception
            bcheck = False
            MessageBox.Show("Format bulan tahun salah")
        End Try
        Return bcheck
    End Function
End Class
