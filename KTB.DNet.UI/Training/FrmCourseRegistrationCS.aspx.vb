#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
#End Region

Public Class FrmCourseRegistrationCS
    Inherits System.Web.UI.Page

    Private gridRowNo As Integer = 0
    Private sessFiscalYear As String = "sessCSRegisFY"
    Private sessBulan As String = "sessCSRegisMonth"
    Private sessCategory As String = "sessCSCategory"
    Dim sessHelper As New SessionHelper()
    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page, "INPUT PENDAFTARAN KELAS CS")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.view, SR.TrainingCsViewClassRegistration_Privilege)
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditClassRegistration_Privilege)
        helpers.Privilage()
        If Not Page.IsPostBack Then
            InitForm()
            ReadCriteriaFromSession()
        End If
    End Sub

    Private Sub InitForm()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        BindDDLTahunFiskal()
        BindDDLBulan()
        txtkodeKategori.Text = String.Empty
        BindDataGrid(0)
    End Sub

    Private Sub ReadCriteriaFromSession()
        If Not IsNothing(sessHelper.GetSession(sessFiscalYear)) Then
            ddlTahunFiscal.ClearSelection()
            ddlTahunFiscal.Items.FindByValue(sessHelper.GetSession(sessFiscalYear)).Selected = True
        End If

        If Not IsNothing(sessHelper.GetSession(sessBulan)) Then
            ddlBulan.ClearSelection()
            ddlBulan.Items.FindByValue(sessHelper.GetSession(sessBulan)).Selected = True
        End If

        If Not IsNothing(sessHelper.GetSession(sessCategory)) Then
            txtkodeKategori.Text = sessHelper.GetSession(sessCategory)
        End If
    End Sub

    Private Sub BindDDLTahunFiskal()
        Dim GetTahun As Integer = DateTime.Now.Year
        ddlTahunFiscal.ClearSelection()
        ddlTahunFiscal.Items.Clear()
        'Before
        For x As Integer = 4 To 0 Step -1
            Dim value1 As String = (GetTahun - x).ToString()
            Dim value2 As String = (GetTahun - x - 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value2, value1)
            ddlTahunFiscal.Items.Add(New ListItem(value, value))
        Next
        'After
        For x As Integer = 0 To 4
            Dim value1 As String = (GetTahun + x).ToString()
            Dim value2 As String = (GetTahun + x + 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value1, value2)
            ddlTahunFiscal.Items.Add(New ListItem(value, value))
        Next
        ddlTahunFiscal.SelectedValue = String.Format("{0}/{1}", GetTahun.ToString(), (GetTahun + 1).ToString())
    End Sub

    Private Sub BindDDLBulan()
        ddlBulan.ClearSelection()
        ddlBulan.Items.Clear()
        ddlBulan.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlBulan.Items.Add(New ListItem("Januari", "1"))
        ddlBulan.Items.Add(New ListItem("Febuari", "2"))
        ddlBulan.Items.Add(New ListItem("Maret", "3"))
        ddlBulan.Items.Add(New ListItem("April", "4"))
        ddlBulan.Items.Add(New ListItem("Mei", "5"))
        ddlBulan.Items.Add(New ListItem("Juni", "6"))
        ddlBulan.Items.Add(New ListItem("Juli", "7"))
        ddlBulan.Items.Add(New ListItem("Agustus", "8"))
        ddlBulan.Items.Add(New ListItem("September", "9"))
        ddlBulan.Items.Add(New ListItem("Oktober", "10"))
        ddlBulan.Items.Add(New ListItem("November", "11"))
        ddlBulan.Items.Add(New ListItem("Desember", "12"))
        ddlBulan.SelectedValue = "-1"
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        gridRowNo = dtgClass.PageSize * indexPage
        Dim arlResult As ArrayList = New TrClassFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, dtgClass.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        sessHelper.SetSession("sessListCourseRegistrationCS", arlResult)

        dtgClass.DataSource = arlResult
        dtgClass.VirtualItemCount = totalRow
        dtgClass.CurrentPageIndex = indexPage
        dtgClass.DataBind()
    End Sub

    Private Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClass), "Status", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(TrClass), "TrCourse.JobPositionCategory.ID", MatchType.Exact, 4))
        criterias.opAnd(New Criteria(GetType(TrClass), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))
        'criterias.opAnd(New Criteria(GetType(TrClass), "StartDate", MatchType.GreaterOrEqual, DateTime.Now.AddDays(-1)))

        If txtkodeKategori.IsNotEmpty Then
            Dim courseCodeInSet As String = txtkodeKategori.Text.GenerateInSet()
            criterias.opAnd(New Criteria(GetType(TrClass), "TrCourse.CourseCode", MatchType.InSet, String.Format("({0})", courseCodeInSet)))
        End If

        If ddlBulan.IsSelected Then
            Dim nBulan As Integer = CInt(ddlBulan.SelectedValue)
            Dim tglMulai As DateTime = DateTime.MinValue
            Dim tglSelesai As DateTime = DateTime.MinValue
            Dim tahun1 As Integer = CInt(ddlTahunFiscal.SelectedValue.Split("/")(0))
            Dim tahun2 As Integer = CInt(ddlTahunFiscal.SelectedValue.Split("/")(1))

            If nBulan > 0 And nBulan < 4 Then
                tglMulai = New DateTime(tahun2, nBulan, 1)
                tglSelesai = New DateTime(tahun2, nBulan + 1, 1)
            ElseIf nBulan > 3 And nBulan < 12 Then
                tglMulai = New DateTime(tahun1, nBulan, 1)
                tglSelesai = New DateTime(tahun1, nBulan + 1, 1)
            Else
                tglMulai = New DateTime(tahun1, nBulan, 1)
                tglSelesai = New DateTime(tahun2, 1, 1)
            End If
            criterias.opAnd(New Criteria(GetType(TrClass), "StartDate", MatchType.GreaterOrEqual, tglMulai))
            criterias.opAnd(New Criteria(GetType(TrClass), "FinishDate", MatchType.Lesser, tglSelesai))
        End If

        SaveCriteriaToSession()

        Return criterias
    End Function

    Private Sub SaveCriteriaToSession()
        sessHelper.SetSession(sessFiscalYear, ddlTahunFiscal.SelectedValue)
        sessHelper.SetSession(sessBulan, ddlBulan.SelectedValue)
        sessHelper.SetSession(sessCategory, txtkodeKategori.Text)
    End Sub

    Private Sub dtgClass_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgClass.ItemCommand
        If e.CommandName.Equals("RegisterToClass") Then
            Dim id As Integer = CInt(e.Item.Cells(0).Text)
            Dim pageTarget As String = String.Format("FrmCourseRegistrationInputCS.aspx?classId={0}&readonly=0", _
                                          id)
            Response.Redirect(pageTarget)
        ElseIf e.CommandName.Equals("Detail") Then
            Dim id As Integer = CInt(e.Item.Cells(0).Text)
            Dim pageTarget As String = String.Format("FrmCourseRegistrationInputCS.aspx?classId={0}&readonly=1", _
                                          id)
            Response.Redirect(pageTarget)
        End If
    End Sub

    Private Sub dtgClass_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgClass.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            gridRowNo += 1

            Dim data As TrClass = CType(e.Item.DataItem, TrClass)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblCourseCategory As Label = CType(e.Item.FindControl("lblCourseCategory"), Label)
            Dim lblTanggalMulai As Label = CType(e.Item.FindControl("lblTanggalMulai"), Label)
            Dim lblTanggalSelesai As Label = CType(e.Item.FindControl("lblTanggalSelesai"), Label)
            Dim lblSiswaTerdaftar As Label = CType(e.Item.FindControl("lblSiswaTerdaftar"), Label)
            Dim lblSiswaTerundang As Label = CType(e.Item.FindControl("lblSiswaTerundang"), Label)
            Dim btnDaftar As LinkButton = CType(e.Item.FindControl("btnDaftar"), LinkButton)
            Dim hKodeKelas As HyperLink = CType(e.Item.FindControl("hKodeKelas"), HyperLink)

            lblNo.Text = gridRowNo
            lblCourseCategory.Text = data.TrCourse.CourseCode
            lblTanggalMulai.Text = data.StartDate
            lblTanggalSelesai.Text = data.FinishDate
            Dim jmlSiswaTerdaftar As Integer = GetSiswaTerdaftarOnClass(data.ID)
            lblSiswaTerdaftar.Text = jmlSiswaTerdaftar
            Dim jmlSiswaTerundang As Integer = GetSiswaTerundangOnClass(data.ID)
            lblSiswaTerundang.Text = jmlSiswaTerundang

            If data.StartDate.DateDay > Me.DateNow Then
                btnDaftar.Visible = True
            Else
                btnDaftar.Visible = False
            End If

            Dim actionValue As String = "popUpClassInformation('" + data.ClassCode + "');"
            hKodeKelas.NavigateUrl = "javascript:" + actionValue

            If Not helpers.IsEdit Then
                btnDaftar.Visible = False
            End If

        End If

    End Sub

    Private Function GetSiswaTerdaftarOnClass(ByVal classId As Integer) As Integer
        Dim result As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, classId))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CInt(EnumTrClassRegistration.DataStatusType.Register)))
        'criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.Status", MatchType.Exact, CInt(EnumTrTrainee.TrTraineeStatus.Active)))

        result = New TrClassRegistrationFacade(User).Retrieve(criterias).Count
     
        Return result
    End Function

    Private Function GetSiswaTerundangOnClass(ByVal classId As Integer) As Integer
        Dim result As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, classId))
        'criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CInt(EnumTrClassRegistration.DataStatusType.Invite)))
        'criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.Status", MatchType.Exact, CInt(EnumTrTrainee.TrTraineeStatus.Active)))

        result = New TrClassRegistrationFacade(User).Retrieve(criterias).Count

        Return result
    End Function

    Private Sub dtgClass_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgClass.PageIndexChanged
        Try
            BindDataGrid(e.NewPageIndex)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtgClass_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgClass.SortCommand
        Try
            If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
                Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                    Case Sort.SortDirection.ASC
                        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                    Case Sort.SortDirection.DESC
                        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
                End Select
            Else
                ViewState("CurrentSortColumn") = e.SortExpression
                ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End If

            dtgClass.CurrentPageIndex = 0
            BindDataGrid(dtgClass.CurrentPageIndex)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Try
            BindDataGrid(0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Response.Redirect("FrmDownloadCourseRegistrationCS.aspx")
    End Sub
   

End Class