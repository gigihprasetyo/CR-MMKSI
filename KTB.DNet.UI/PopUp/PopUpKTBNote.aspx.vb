Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade

Public Class PopUpKTBNote
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKTBNote As System.Web.UI.WebControls.TextBox
    Protected WithEvents ltrKTBNote As System.Web.UI.WebControls.Literal
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblNote As System.Web.UI.WebControls.Label
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

    Dim CH As ClaimHeader = New ClaimHeader

#End Region

#Region "Custom Method"

    Sub Mode(ByVal IsKTB As Integer)
        If (IsKTB = EnumDealerTittle.DealerTittle.KTB) Then
            ltrKTBNote.Visible = False
            txtKTBNote.Visible = True
            btnSave.Visible = True
        ElseIf IsKTB = EnumDealerTittle.DealerTittle.DEALER Then
            ltrKTBNote.Visible = True
            txtKTBNote.Visible = False
            btnSave.Visible = False
        End If
    End Sub

    Sub DisplayKTBNote(ByVal ID As Integer, ByVal isKTB As Integer)
        CH = New Claim.ClaimHeaderFacade(User).Retrieve(ID)
        ltrKTBNote.Text = CH.KTBNote
        txtKTBNote.Text = CH.KTBNote
        Mode(isKTB)
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If (Not IsPostBack) Then
            Dim shelper As SessionHelper = New SessionHelper
            Dim objUser As UserInfo = CType(shelper.GetSession("LOGINUSERINFO"), UserInfo)
            If objUser.Dealer.Title.Trim <> "1" Then
                btnSave.Enabled = False
                btnSave.Visible = False
                btnCancel.Text = "Tutup"
            End If

            DisplayKTBNote(Convert.ToInt32(Request.QueryString("ID")), Request.QueryString("isKTB"))
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim CHUpdate As ClaimHeader = New ClaimHeader
        CHUpdate = New Claim.ClaimHeaderFacade(User).Retrieve(Convert.ToInt32(Request.QueryString("ID")))
        CHUpdate.KTBNote = Server.HtmlEncode(txtKTBNote.Text.Trim())
        If (New Claim.ClaimHeaderFacade(User).Update(CHUpdate) <> -1) Then
            MessageBox.Show(SR.UpdateSucces)
        Else
            MessageBox.Show(SR.UpdateFail)
        End If
    End Sub
#End Region

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub
End Class
