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
Public Class FrmTOPFacility
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rbtnNone As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents txtMaxDay As System.Web.UI.WebControls.TextBox
    Protected WithEvents rbtnDay As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents rbtnDate As System.Web.UI.WebControls.RadioButton
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents icMaxDate As KTB.DNet.WebCC.IntiCalendar
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
        rbtnDate.Attributes("onclick") = "ValidateRadioButton(this)"
        rbtnDay.Attributes("onclick") = "ValidateRadioButton(this)"
        rbtnNone.Attributes("onclick") = "ValidateRadioButton(this)"

    End Sub

    Private Sub btnPilih_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPilih.Click
        If ValidateData() Then
            Dim selectedTOP As String = ""
            If rbtnDate.Checked Then
                selectedTOP = "0," + icMaxDate.Value.ToShortDateString + "," + txtMaxDay.Text.Trim
            ElseIf (rbtnDay.Checked) Then

                selectedTOP = "1," + icMaxDate.Value.ToShortDateString + "," + txtMaxDay.Text.Trim
            Else
                selectedTOP = "-1,,"
            End If
            Dim jsString As String = "<script language='javascript'> if (navigator.appName == 'Microsoft Internet Explorer') {window.returnValue='" + selectedTOP + "' ; } else {opener.dialogWin.returnFunc('" + selectedTOP + "');} window.close(); </script>"

            'Response.Write("<script language='javascript'>window.returnValue='" + selectedTOP + "';window.close()</script>")
            Response.Write(jsString)
        End If
    End Sub
    Private Function ValidateData() As Boolean
        Dim bcheck As Boolean = True
        Try
            If Integer.Parse(txtMaxDay.Text.Trim) < 1 Then
                bcheck = False
                MessageBox.Show("Pilihan TOP untuk Hari tidak boleh < 1 ")
            End If


            Dim objFTOP As TermOfPaymentFacade = New TermOfPaymentFacade(User)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TermOfPayment), "TermOfPaymentValue", MatchType.Exact, Integer.Parse(txtMaxDay.Text.Trim)))

            Dim agg As Aggregate = New Aggregate(GetType(TermOfPayment), "TermOfPaymentValue", AggregateType.Max)
            Dim JJ = objFTOP.RetrieveScalar(criterias, agg)
            Dim maxDay As Integer = 0
            If Not IsNothing(JJ) Then
                maxDay = CInt(JJ)
            End If

            If Integer.Parse(txtMaxDay.Text.Trim) > maxDay Then
                bcheck = False
                MessageBox.Show("Pilihan TOP " + txtMaxDay.Text + " hari tidak terdaftar   ")
            End If

        Catch ex As Exception
            bcheck = False
            MessageBox.Show("Pilihan TOP untuk Hari tidak boleh mengandung karakter selain angka")
        End Try
        Return bcheck
    End Function
End Class
