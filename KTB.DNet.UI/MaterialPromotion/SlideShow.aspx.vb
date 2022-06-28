Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General

Public Class SlideShow
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents pnlSlideShow As System.Web.UI.WebControls.Panel
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents Image2 As System.Web.UI.WebControls.Image

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
            GeneratePhoto()
        End If

    End Sub

    Private Sub GeneratePhoto()

        Dim DealerID As Integer = 5

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerProfilePhoto), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DealerProfilePhoto), "Dealer.ID", MatchType.Exact, DealerID))

        Dim arlImage As ArrayList = New DealerProfilePhotoFacade(User).Retrieve(criterias)
        Dim counter As Integer = 0
        For Each item As DealerProfilePhoto In arlImage
            counter += 1
            Dim objimg As System.Web.UI.WebControls.Image = New System.Web.UI.WebControls.Image
            objimg.ID = "MyImage" & counter.ToString
            objimg.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & item.ID & "&type=" & "DealerSlideShow"

            objimg.Height = Unit.Pixel(100)
            objimg.Width = Unit.Pixel(100)
            If counter > 1 Then
                objimg.Style.Add("display", "none")
            Else
                objimg.Style.Add("display", "blocked")
            End If


            pnlSlideShow.Controls.Add(objimg)
        Next

    End Sub

End Class
