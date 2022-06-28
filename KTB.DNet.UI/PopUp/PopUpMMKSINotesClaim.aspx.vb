Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Benefit

Public Class PopUpMMKSINotesClaim
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblClaimRegNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtNotesMMKSI As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button

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

    Dim BCH As BenefitClaimHeader = New BenefitClaimHeader

#End Region

#Region "Custom Method"
    Sub DisplayKTBNote(ByVal ID As Integer)
        BCH = New BenefitClaimHeaderFacade(User).Retrieve(ID)
        lblClaimRegNo.Text = BCH.ClaimRegNo
        txtNotesMMKSI.Text = BCH.MMKSINotes
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If (Not IsPostBack) Then
            DisplayKTBNote(Convert.ToInt32(Request.QueryString("ID")))
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim BCHUpdate As BenefitClaimHeader = New BenefitClaimHeader
        BCHUpdate = New BenefitClaimHeaderFacade(User).Retrieve(Convert.ToInt32(Request.QueryString("ID")))
        BCHUpdate.MMKSINotes = Server.HtmlEncode(txtNotesMMKSI.Text.Trim())
        If (New BenefitClaimHeaderFacade(User).Update(BCHUpdate) <> -1) Then
            'MessageBox.Show(SR.UpdateSucces)

            RegisterStartupScript("CloseWindow", "<script languange=javascript>alert('Simpan Data Berhasil');window.returnValue='" + txtNotesMMKSI.Text + "';window.close()</script>")

        Else
            MessageBox.Show(SR.UpdateFail)
        End If
    End Sub
#End Region

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub
End Class
