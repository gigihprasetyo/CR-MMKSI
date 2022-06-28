#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.Utility

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
#End Region

Public Class PopUpUniform
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKeterangan As System.Web.UI.WebControls.Label
    Protected WithEvents photoView As System.Web.UI.WebControls.Image
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents dtgUniform As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblUniformCode As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variable"
    Private unifCode As String
    Private arlUniform As ArrayList = New ArrayList
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        unifCode = Request.QueryString("UniformID")

        If Not IsPostBack Then
            cleardata()
            btnClose.Attributes.Add("Onclick", "window.close();")
            Bindata()
        End If

    End Sub

    Private Sub Cleardata()
        lblKeterangan.Text = ""
        lblUniformCode.Text = ""
        dtgUniform.DataSource = Nothing
        dtgUniform.DataBind()
    End Sub
    Private Sub Bindata()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUnifGuide), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanUnifGuide), "SalesmanUniform.ID", MatchType.Exact, unifCode))

        Dim objUniform As SalesmanUniform
        objUniform = New SalesmanUniformFacade(User).Retrieve(CInt(unifCode))
        lblUniformCode.Text = objUniform.SalesmanUniformCode
        lblKeterangan.Text = objUniform.Description
        photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & objUniform.ID & "&type=" & "SalesmanUniform"
        arlUniform = New SalesmanUnifGuideFacade(User).Retrieve(criterias)
        If arlUniform.Count > 0 Then
            dtgUniform.DataSource = arlUniform
            dtgUniform.DataBind()
        Else
            MessageBox.Show(SR.DataNotFound("Detail Ukuran Seragam"))
        End If
    End Sub

End Class
