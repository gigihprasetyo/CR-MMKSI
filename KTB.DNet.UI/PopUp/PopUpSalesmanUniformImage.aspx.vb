#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Utility
#End Region

Public Class PopUpSalesmanUniformImage
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents photoView As System.Web.UI.WebControls.Image

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
        If Not Page.IsPostBack Then
            BindImage()
        End If
    End Sub

    Private Sub BindImage()
        Dim arrSalesmanUniform As ArrayList
        Dim strSalesmanUniformId As String
        If Not IsNothing(Request.QueryString("Distribution")) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.SalesmanUnifDistributionCode", Request.QueryString("Distribution")))
            arrSalesmanUniform = New SalesmanUniformFacade(User).Retrieve(criterias)

            For Each item As SalesmanUniform In arrSalesmanUniform
                If strSalesmanUniformId = "" Then
                    strSalesmanUniformId = CType(item.ID, String)
                    Exit For
                End If
            Next

            Dim objSalesmanUniform As SalesmanUniform = New SalesmanUniformFacade(User).Retrieve(CType(strSalesmanUniformId, Integer))
            If Not IsNothing(objSalesmanUniform.Image) Then
                ' Show image
                photoView.Visible = True
                photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & CType(strSalesmanUniformId, Integer) & "&type=" & "SalesmanUniformBig"
            Else
                photoView.Visible = False
                MessageBox.Show("Tidak ada data Image")
            End If

        Else
            photoView.Visible = False
            MessageBox.Show("Tidak ada data Distribution yang dipilih")
        End If
    End Sub


End Class
