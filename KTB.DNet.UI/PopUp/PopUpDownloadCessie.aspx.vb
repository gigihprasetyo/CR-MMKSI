#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
#End Region

Public Class PopUpDownloadCessie
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid

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
        If (Not Page.IsPostBack) Then
            ''If Not Session("CessieSession") Is Nothing Then
            ''    BindToDataGrid()
            ''End If
            If Not (Request.QueryString("IdCessie") Is Nothing) Then
                Dim idCessie As Integer = Integer.Parse(Request.QueryString("IdCessie"))
                BindToDataGrid(idCessie)
            End If
        End If
    End Sub

    Sub BindToDataGrid(ByVal intIdCessie As Integer)
        Dim _lstCessie As New ArrayList

        If Not (GetCessie(intIdCessie) Is Nothing) Then
            _lstCessie.Add(GetCessie(intIdCessie))
        End If
        Me.dtgMain.DataSource = _lstCessie
        Me.dtgMain.DataBind()

    End Sub

    Function GetCessie(ByVal _idCessie As Integer) As Cessie
        Dim _retVal As New Cessie
        Dim _facade As New CessieFacade(User)
        _retVal = _facade.Retrieve(_idCessie)
        Return _retVal
    End Function

End Class
