Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.VehicleData
Imports KTB.DNet.BusinessFacade.UserManagement
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.Utility

Public Class PopUPItemNoSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents dtgItemSelection As System.Web.UI.WebControls.DataGrid

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
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            displaydata()
        End If
    End Sub

    Private Sub displaydata()
        Dim arlToDisplay As ArrayList = New ArrayList
        Dim critItem As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VDHItem), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        arlToDisplay = New VDHItemFacade(User).Retrieve(critItem)
        If arlToDisplay.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
        dtgItemSelection.DataSource = arlToDisplay
        dtgItemSelection.DataBind()
        '    criterias.opAnd(New Criteria(GetType(AuditParameter), "IsRilis", MatchType.Exact, 1)
    End Sub

End Class
