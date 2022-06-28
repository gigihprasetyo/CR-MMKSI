#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports System.Collections.Generic
Imports System.Linq

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.UI.Helper
#End Region

Public Class FrmTrReplacement
    Inherits System.Web.UI.Page

    Dim gridColNo As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                InitForm()
            End If
        Catch ex As Exception
            MessageBox.Show("Error dalam load form")
        End Try
    End Sub

    Protected Sub InitForm()
        btnSimpan.Enabled = True
        btnCari.Enabled = True
        btnBatal.Enabled = True
        ViewState("CurrentSortColumn") = "TrCourse.CourseCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        txtCourseCode.Text = String.Empty
        txtDesc.Text = String.Empty
        txtReplacementCode.Text = String.Empty
        txtCourseCode.Enabled = True
        lblPopUpCourse.Visible = True
        BindDataGrid(0)
    End Sub

    Private Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrReplacementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtCourseCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(TrReplacementHeader), "TrCourse.CourseCode", MatchType.[Partial], txtCourseCode.Text))
        End If

        If txtDesc.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(TrReplacementHeader), "Description", MatchType.[Partial], txtDesc.Text))
        End If

        If txtReplacementCode.Text <> "" Then
            Dim strInset As String = "(SELECT ID FROM TrReplacementDetail detail"
            strInset &= " INNER JOIN TrCourse course ON detail.TrCourseID = course.ID"
            strInset &= " WHERE course.ID IN ('" + Replace(txtReplacementCode.Text, ";", "','") + "'))"
            criterias.opAnd(New Criteria(GetType(TrReplacementHeader), "ID", MatchType.InSet, strInset))
        End If




        Return criterias
    End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        gridColNo = 0
        dtgReplacement.DataSource = New TrReplacementHeaderFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, dtgReplacement.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgReplacement.VirtualItemCount = totalRow
        dtgReplacement.DataBind()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            Dim dataHeader As TrReplacementHeader = MappingFromUiToDataHeader()
            Dim arlDataDetail As List(Of TrReplacementDetail) = MappingFromUiToListDetail()

            Dim errMessage As String = GetErrorMessage(dataHeader, arlDataDetail)

            If errMessage <> String.Empty Then
                MessageBox.Show(errMessage)
                Exit Sub
            End If

            Dim headerFacade As TrReplacementHeaderFacade = New TrReplacementHeaderFacade(User)
            headerFacade.Save(dataHeader, arlDataDetail)

            MessageBox.Show("Data berhasil disimpan")
            InitForm()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function MappingFromUiToDataHeader() As TrReplacementHeader
        Dim data As TrReplacementHeader

        Dim headerFacade As TrReplacementHeaderFacade = New TrReplacementHeaderFacade(User)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrReplacementHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrReplacementHeader), "TrCourse.CourseCode", MatchType.Exact, txtCourseCode.Text.Trim()))

        Dim arlResult As ArrayList = headerFacade.Retrieve(criterias)

        If arlResult.Count > 0 Then
            data = arlResult(0)
        Else
            data = New TrReplacementHeader
            data.TrCourse = GetTrCourseByCode(txtCourseCode.Text.Trim())
        End If

        data.Description = txtDesc.Text.Trim()
        Return data
    End Function

    Private Function GetTrCourseByCode(ByVal courseCode As String) As TrCourse
        Try
            Dim course As New TrCourse
            Dim courseFacade As TrCourseFacade = New TrCourseFacade(User)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrCourse), "CourseCode", MatchType.Exact, courseCode))

            Dim arlCourse As ArrayList = courseFacade.Retrieve(criterias)

            If arlCourse.Count > 0 Then
                course = arlCourse(0)
            Else
                Throw New Exception("Course code " + courseCode + " tidak terdaftar dalam database")
            End If

            Return course
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Private Function MappingFromUiToListDetail() As List(Of TrReplacementDetail)
        Try
            Dim listDetail As New List(Of TrReplacementDetail)

            If Not String.IsNullOrEmpty(txtReplacementCode.Text) Then
                Dim listCourseCode() As String = txtReplacementCode.Text.Trim.Split(";")
                For Each code As String In listCourseCode
                    Dim courseData As TrCourse = GetTrCourseByCode(code)
                    Dim detailData As New TrReplacementDetail
                    detailData.TrCourse = courseData
                    listDetail.Add(detailData)
                Next
            End If

            Return listDetail
        Catch ex As Exception
            If ex.Message = String.Empty Then
                Throw New Exception("Gagal dalam mengambil detail course")
            End If
            Throw New Exception(ex.Message)
        End Try


    End Function

    Private Function GetErrorMessage(dataHeader As TrReplacementHeader, arlDataDetail As List(Of TrReplacementDetail)) As String
        Dim errMessage As String = String.Empty

        Dim indexFound As Integer = arlDataDetail.FindIndex(Function(x) x.TrCourse.ID = dataHeader.TrCourse.ID)
        If indexFound <> -1 Then
            errMessage &= "Kode kategori tidak boleh berada didalam list kode pengganti lulus"
        End If

        Return errMessage
    End Function

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Try
            BindDataGrid(0)
            btnSimpan.Enabled = True

            If dtgReplacement.Items.Count < 0 Then
                MessageBox.Show("Data tidak ditemukan")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dtgReplacement_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgReplacement.ItemCommand

        If (e.CommandName = "View") Then
            SendGridItemToField(e.Item.Cells(0).Text)
            btnSimpan.Enabled = False
        ElseIf (e.CommandName = "Edit") Then
            SendGridItemToField(e.Item.Cells(0).Text)
            btnSimpan.Enabled = True
            txtCourseCode.Enabled = False
            lblPopUpCourse.Visible = False
        ElseIf (e.CommandName = "Delete") Then
            DeleteHeaderData(e.Item.Cells(0).Text)
        End If
    End Sub

    Private Sub dtgReplacement_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgReplacement.ItemDataBound
        Try

            If Not e.Item.DataItem Is Nothing Then
                Dim data As TrReplacementHeader = CType(e.Item.DataItem, TrReplacementHeader)
                gridColNo += 1

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lblTrCourseCode As Label = CType(e.Item.FindControl("lblTrCourseCode"), Label)
                Dim lblCourseReplacement As Label = CType(e.Item.FindControl("lblCourseReplacement"), Label)
                Dim lblDescription As Label = CType(e.Item.FindControl("lblDescription"), Label)

                lblNo.Text = gridColNo
                lblTrCourseCode.Text = data.TrCourse.CourseCode
                lblCourseReplacement.Text = GetListReplacementCode(data.ID)
                lblDescription.Text = data.Description


            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function GetListReplacementCode(id As Integer) As String
        Try
            Dim listCode As String = String.Empty
            Dim detailFacade As TrReplacementDetailFacade = New TrReplacementDetailFacade(User)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(TrReplacementDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrReplacementDetail), "TrReplacementHeader.ID", MatchType.Exact, id))

            Dim arlReplacement As ArrayList = detailFacade.Retrieve(criterias)

            For Each detail As TrReplacementDetail In arlReplacement
                listCode += detail.TrCourse.CourseCode + ";"
            Next

            If listCode <> String.Empty Then
                listCode = listCode.Trim().Remove(listCode.Length - 1)
            End If

            Return listCode
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        InitForm()
    End Sub

    Private Sub dtgReplacement_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgReplacement.PageIndexChanged
        dtgReplacement.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgReplacement.CurrentPageIndex)
    End Sub

    Private Sub dtgReplacement_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgReplacement.SortCommand
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

        dtgReplacement.CurrentPageIndex = 0
        BindDataGrid(dtgReplacement.CurrentPageIndex)
    End Sub


    Private Sub SendGridItemToField(id As Integer)
        Dim headerFacade As TrReplacementHeaderFacade = New TrReplacementHeaderFacade(User)
        Dim data As TrReplacementHeader = headerFacade.Retrieve(id)

        txtCourseCode.Text = data.TrCourse.CourseCode
        txtReplacementCode.Text = GetListReplacementCode(id)
        txtDesc.Text = data.Description

    End Sub

    Private Sub DeleteHeaderData(id As Integer)
        Try
            Dim headerFacade As TrReplacementHeaderFacade = New TrReplacementHeaderFacade(User)
            Dim data As TrReplacementHeader = headerFacade.Retrieve(id)
            data.RowStatus = CType(DBRowStatus.Deleted, Short)
            headerFacade.Delete(data)
            MessageBox.Show("Data berhasil dihapus")
            InitForm()
        Catch ex As Exception

        End Try
    End Sub

End Class