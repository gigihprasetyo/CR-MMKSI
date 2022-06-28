Imports KTB.DNet.Utility
Imports KTB.DNet.Domain

Public Class FrmFacilityTOP
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents rbtnDate As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents rbtnDay As System.Web.UI.WebControls.RadioButton
    Protected WithEvents txtMaxDay As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents rbtnNone As System.Web.UI.WebControls.RadioButton
    Protected WithEvents icMaxDate As KTB.DNet.WebCC.IntiCalendar

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

        rbtnDate.Attributes("onclick") = "ValidateRadioButton(this)"
        rbtnDay.Attributes("onclick") = "ValidateRadioButton(this)"
        rbtnNone.Attributes("onclick") = "ValidateRadioButton(this)"
    End Sub

    Private Sub btnPilih_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If rbtnDay.Checked Then
            Try
                Dim i As Integer = txtMaxDay.Text
                If i < 1 Then
                    MessageBox.Show("Jumlah hari di Fasilitas TOP harus lebih besar dari 0")
                    Return
                End If
            Catch ex As Exception
                MessageBox.Show("Input Jumlah hari di Fasilitas TOP tidak valid")
                Return
            End Try
        End If
    End Sub

    
End Class
