#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region



Public Class PopUpMaxPO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnCancel As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblPattern As System.Web.UI.WebControls.Label
    Protected WithEvents lblCalculation As System.Web.UI.WebControls.Label
    Protected WithEvents lblResult As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private variables"
    Dim sHelper As SessionHelper = New SessionHelper
#End Region
#Region "Custom Methods"

    Private Sub Initialization()
        Dim strUrl As String = CType(sHelper.GetSession("MaxPOUrl"), String)
        Dim str() As String

        str = strUrl.Split("&")
        lblTitle.Text = "Max PO " & (str(0).Split("="))(1) ' CType(Request.Item("PaymentType"), String)
        lblPattern.Text = (str(1).Split("="))(1) ' CType(Request.Item("sPattern"), String)
        lblCalculation.Text = (str(2).Split("="))(1) '  CType(Request.Item("sCalculation"), String)
        lblResult.Text = (str(3).Split("="))(1) ' CType(Request.Item("sResult"), String)
    End Sub

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            Initialization()
        End If
    End Sub

#End Region

End Class
