#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.BabitSalesComm
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Web.UI.WebControls
#End Region
Public Class PopUpCatatanKTB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents txtNote As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private designID As Integer
    Private obj As New PengajuanDesignIklan
    Private objFacade As New PengajuanDesignIklanFacade(User)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            designID = Request.QueryString("ID")
            obj = objFacade.Retrieve(designID)
            If obj.Note <> String.Empty Then
                txtNote.Text = obj.Note
            End If
            btnSimpan.Enabled = SecurityProvider.Authorize(Context.User, SR.BuatCatatanKTBDaftarStatus_Privilege)
            Dim objDealer As Dealer = Session.Item("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                btnSimpan.Visible = True
            Else
                btnSimpan.Visible = False
            End If
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If txtNote.Text.Trim <> String.Empty Then
            designID = Request.QueryString("ID")
            obj = objFacade.Retrieve(designID)
            obj.Note = txtNote.Text
            objFacade.Update(obj)
            MessageBox.Show("Catatan MMKSI sudah diupdate")
        End If
    End Sub
End Class
