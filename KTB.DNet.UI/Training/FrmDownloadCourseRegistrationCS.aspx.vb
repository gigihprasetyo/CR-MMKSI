#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
#End Region

Public Class FrmDownloadCourseRegistrationCS
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim sHdownLoad As SessionHelper = New SessionHelper
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'Response.ContentType = "application/x-download"

        'Response.AddHeader("Content-Disposition", _
        '    New System.Text.StringBuilder("attachment;filename=").Append("Daftar Kelas.xls").ToString)
        'dtgDwnload.DataSource() = New TrClassFacade(User).Retrieve(CType(sHdownLoad.GetSession("searchRes"), CriteriaComposite))
        'dtgDwnload.DataBind()
    End Sub
    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        Response.ContentType = "application/x-download"

        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append("Daftar Pendaftaran Kelas CS.xls").ToString)

        If Not sHdownLoad.GetSession("sessListCourseRegistrationCS") Is Nothing Then
            dtgDwnload.DataSource() = CType(sHdownLoad.GetSession("sessListCourseRegistrationCS"), ArrayList)
            dtgDwnload.DataBind()
        End If


        dtgDwnload.RenderControl(writer)

    End Sub

    Private Sub dtgDwnload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDwnload.ItemDataBound


        If e.Item.DataItem IsNot Nothing Then

            Dim data As TrClass = CType(e.Item.DataItem, TrClass)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblCourseCategory As Label = CType(e.Item.FindControl("lblCourseCategory"), Label)
            Dim lblTanggalMulai As Label = CType(e.Item.FindControl("lblTanggalMulai"), Label)
            Dim lblTanggalSelesai As Label = CType(e.Item.FindControl("lblTanggalSelesai"), Label)
            Dim lblSiswaTerdaftar As Label = CType(e.Item.FindControl("lblSiswaTerdaftar"), Label)
            Dim lblSiswaTerundang As Label = CType(e.Item.FindControl("lblSiswaTerundang"), Label)
            Dim btnDaftar As LinkButton = CType(e.Item.FindControl("btnDaftar"), LinkButton)
            Dim hKodeKelas As HyperLink = CType(e.Item.FindControl("hKodeKelas"), HyperLink)

            lblNo.Text = e.Item.ItemIndex + 1 + (dtgDwnload.CurrentPageIndex * dtgDwnload.PageSize)
            lblCourseCategory.Text = data.TrCourse.CourseCode
            lblTanggalMulai.Text = data.StartDate
            lblTanggalSelesai.Text = data.FinishDate
            Dim jmlSiswaTerdaftar As Integer = GetSiswaTerdaftarOnClass(data.ID)
            lblSiswaTerdaftar.Text = jmlSiswaTerdaftar
            Dim jmlSiswaTerundang As Integer = GetSiswaTerundangOnClass(data.ID)
            lblSiswaTerundang.Text = jmlSiswaTerundang



        End If

    End Sub

    Private Function GetSiswaTerdaftarOnClass(ByVal classId As Integer) As Integer
        Dim result As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, classId))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CInt(EnumTrClassRegistration.DataStatusType.Register)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.Status", MatchType.Exact, CInt(EnumTrTrainee.TrTraineeStatus.Active)))

        result = New TrClassRegistrationFacade(User).Retrieve(criterias).Count

        Return result
    End Function

    Private Function GetSiswaTerundangOnClass(ByVal classId As Integer) As Integer
        Dim result As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, classId))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CInt(EnumTrClassRegistration.DataStatusType.Invite)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.Status", MatchType.Exact, CInt(EnumTrTrainee.TrTraineeStatus.Active)))

        result = New TrClassRegistrationFacade(User).Retrieve(criterias).Count

        Return result
    End Function
End Class
