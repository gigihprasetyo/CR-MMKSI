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

Public Class FrmDownloadTrClassCs
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

    Dim sHdownLoad As SessionHelper = New SessionHelper
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        Response.ContentType = "application/x-download"

        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append("Daftar Kelas.xls").ToString)

        Dim arlResult As ArrayList = CType(Session("SessListTrClassCS"), ArrayList)
        dtgDwnload.DataSource = arlResult
        dtgDwnload.DataBind()


        dtgDwnload.RenderControl(writer)

    End Sub
    Public Function hitungSisa(ByVal id As Integer) As Integer
        Dim objTrClassRegisFacade As TrClassRegistrationFacade = New TrClassRegistrationFacade(User)
        Dim crit As CriteriaComposite

        crit = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, id))
        crit.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Register, Short)), "(", True)
        crit.opOr(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Pass, Short)))
        crit.opOr(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Fail, Short)), ")", False)

        Dim arlResult As New ArrayList
        arlResult = objTrClassRegisFacade.Retrieve(crit)
        Return arlResult.Count
    End Function
    Private Sub dtgDwnload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDwnload.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As TrClass = CType(e.Item.DataItem, TrClass)

            '--get rest of class capacity
            Dim RetHit As Integer = hitungSisa(RowValue.ID)
            Dim Selisih As Integer = CInt(RowValue.Capacity) - RetHit
            Dim lblSelisih As Label = CType(e.Item.FindControl("lblSelisih"), Label)
            lblSelisih.Text = Selisih.ToString().Trim
            '-- get number
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgDwnload.CurrentPageIndex * dtgDwnload.PageSize)

            '-- get city
            Dim lblKategori As Label = CType(e.Item.FindControl("lblKategori"), Label)
            If Not IsNothing(RowValue.TrCourse) Then
                lblKategori.Text = RowValue.TrCourse.CourseCode
            End If


        End If
    End Sub

#End Region
    

End Class