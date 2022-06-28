#Region "Custom Namespace Imports"
Imports System.Text

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Utility
Imports Ktb.DNet.BusinessFacade.General

#End Region


Public Class FrmPartIncidentalPrintDocument
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblStringBuilder As System.Web.UI.WebControls.Label
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnPrinter As System.Web.UI.WebControls.Button

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
        'Put user code to initialize the page here
        If Not IsPostBack Then
            If Not IsNothing(Session("stringBulder")) Then
                lblStringBuilder.Text = Session("stringBulder")
            End If
        End If
        btnPrinter.Attributes.Add("onClick", "window.print()")
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Dim id As Integer = Request.QueryString("ID")
        Response.Redirect("../SparePart/FrmListPartIncidentalKTB.aspx?ID=" & id)
    End Sub

    Private Sub btnPrinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrinter.Click
        Dim id As Integer = Request.QueryString("ID")
        If id > 0 Then           '--Change Status From Baru To Sedang Proses
            Dim objPartIncidentalHeader As New PartIncidentalHeader
            Dim FacadePartListHeader As New PartIncidentalHeaderFacade(User)
            Dim status As New PartIncidentalStatus.PartIncidentalKTBStatusEnum
            objPartIncidentalHeader = FacadePartListHeader.Retrieve(id)
            objPartIncidentalHeader.KTBStatus = status.Sedang_Proses
            FacadePartListHeader.Update(objPartIncidentalHeader)
        Else
            MessageBox.Show("Tidak Dapat Mencetak Data")
        End If
    End Sub
End Class
