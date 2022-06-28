
Imports System.IO
Imports System.Text
Imports OfficeOpenXml
Imports System.Collections.Generic
Imports System.Linq
Imports System.Security.Principal
Imports System.Web.UI.Page
Imports KTB.DNET.Utility
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Domain
Imports KTB.DNET.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports GlobalExtensions

Public Class FrmTRCourseCategory
    Inherits System.Web.UI.Page


    'Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page, "priv1" + PrivArea)
    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page)

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Private ReadOnly Property PrivArea As String
        Get
            Select Case Request.QueryString("area")
                Case "1"
                    Return "A"
                Case "2"
                    Return "B"
                Case "3"
                    Return "C"
                Case Else
                    Return "B"
            End Select
        End Get
    End Property

    Private Sub TitleDescription(ByVal areaid As String)
        If areaid.Equals("1") Then
            lblTitle.Text = "Training Sales - Kursus Kategori"
        ElseIf areaid.Equals("2") Then
            lblTitle.Text = "Training After Sales - Kursus Kategori"
        Else
            lblTitle.Text = "Training Customer Satisfaction - Kursus Kategori"
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        helpers.CheckPrivilege("priv1" + PrivArea)
        If Not Page.IsPostBack Then
            TitleDescription(AreaId)
            helpers.ClearData(trInput, True)
            BindDropDownList()
            ViewState("CurrentSortColumn") = "Code"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            ReadCritriaSearch()
            If Not IsNothing(helpers.GetSession("idxpage")) Then
                BindDataGrid(helpers.GetSession("idxpage"))
            Else
                BindDataGrid()
            End If

            If Not helpers.IsEdit Then
                btnSimpan.Enabled = False
            End If
        End If

    End Sub

    Private Sub ddlTipeKategoriKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipeKategoriKursus.SelectedIndexChanged
        If ddlTipeKategoriKursus.SelectedValue.Equals(helpers.NotSelected) Or ddlTipeKategoriKursus.SelectedValue.Equals("0") Then
            trjobposition.Visible = False
            cbxAll.Visible = False
            cbxAll.Checked = False
            trLevel.Visible = False
            If ddlKategory.SelectedValue.Equals(helpers.NotSelected) Then
                listCheckJobPosition.ClearSelection()
            End If

            Exit Sub
        End If
        ddlKategory_SelectedIndexChanged(sender, e)
        trjobposition.Visible = True
        cbxAll.Visible = True

    End Sub

    Private Sub ddlKategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKategory.SelectedIndexChanged

        If ddlKategory.SelectedValue.Equals(helpers.NotSelected) Then
            ddlLevel.ClearSelection()
            ddlLevel.Items.Clear()
            listCheckJobPosition.ClearSelection()
            listCheckJobPosition.Items.Clear()
            trjobposition.Visible = False
            cbxAll.Visible = False
            cbxAll.Checked = False
            trLevel.Visible = False
            Exit Sub
        End If
        BindJobPosition(listCheckJobPosition, ddlKategory.SelectedValue)
        CommonFunction.BindSalesmanLevel(ddlLevel, Integer.Parse(ddlKategory.SelectedValue), User, False)

    End Sub

    Private Sub BindJobPosition(ByRef cbl As CheckBoxList, ByVal catgory As String)
        cbl.ClearSelection()
        cbl.Items.Clear()

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.JobPositionToCategory), "CategoryID", MatchType.Exact, CType(catgory, Short)))

        Dim arlJobPositionToCategory As ArrayList = New JobPositionToCategoryFacade(Me.Page.User).Retrieve(criterias)
        For Each JobPosition As JobPositionToCategory In arlJobPositionToCategory
            cbl.Items.Add(New ListItem(JobPosition.JobPosition.Description + String.Format(" ({0})", JobPosition.JobPosition.Code), JobPosition.JobPositionID))
        Next

        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(TrCourseCategory), "JobPositionCategory.ID", MatchType.Exact, CType(catgory, Short)))

        Dim dataCtgKursus As ArrayList = New TrCourseCategoryFacade(User).Retrieve(crit)
        If dataCtgKursus.IsItems Then
            Dim listId As List(Of String) = dataCtgKursus.Cast(Of TrCourseCategory).Select(Function(x) x.ID.ToString()).ToList()
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseCategoryToJobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseCategoryToJobPosition), "TrCourseCategory.ID", MatchType.InSet, listId.GenerateInSet()))
            'crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseCategoryToJobPosition), "JobPositionCategory.iD", MatchType.Exact, CType(catgory, Short)))

            Dim arrJobposition As ArrayList = New TrCourseCategoryToJobPositionFacade(User).Retrieve(crits)
            If arrJobposition.IsItems Then
                Dim listJobs As List(Of TrCourseCategoryToJobPosition) = arrJobposition.Cast(Of TrCourseCategoryToJobPosition).ToList()
                For Each li As ListItem In cbl.Items
                    If listJobs.Where(Function(x) x.JobPosition.ID = CInt(li.Value)).IsData Then
                        'cbl.Items.FindByValue(li.Value).Enabled = False
                    End If
                Next
            End If
        End If

    End Sub


    Private Sub BindDropDownList()
        helpers.BindDDLCategory(ddlKategory, AreaId)
        helpers.BindDDLStatus(DdlStatus)
        CommonFunction.BindDDLFromStandartCode("EnumTipeKategoriKhusus", ddlTipeKategoriKursus)

        If ddlKategory.Items.Count.Equals(2) Then
            ddlKategory.SelectedIndex = 1
            helpers.BindJobPosition(listCheckJobPosition, ddlKategory.SelectedValue)
            CommonFunction.BindSalesmanLevel(ddlLevel, Integer.Parse(ddlKategory.SelectedValue), User, False)
        End If

        'ddlTipeKategoriKursus.ClearSelection()
        'ddlTipeKategoriKursus.Items.FindByValue("0").Selected = True
    End Sub

    Private Sub BindDataGrid(Optional ByVal indexPage As Integer = 0)
        Dim totalRow As Integer = 0
        Try
            GridCourseCtg.DataSource = New TrCourseCategoryFacade(User).RetrieveActiveList(indexPage + 1, _
            GridCourseCtg.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection), CriteriaSearch())
            GridCourseCtg.VirtualItemCount = totalRow
            GridCourseCtg.DataBind()
            helpers.SetSession("idxpage", GridCourseCtg.CurrentPageIndex)
        Catch ex As Exception
            GridCourseCtg.DataSource = New List(Of TrCourseCategory)
            GridCourseCtg.CurrentPageIndex = 0
            GridCourseCtg.DataBind()
            MessageBox.Show(SR.DataNotFound("Kategori Kursus"))
            Exit Sub
        End Try
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourseCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If Not String.IsNullOrEmpty(txtKode.Text) Then
            criterias.opAnd(New Criteria(GetType(TrCourseCategory), "Code", MatchType.[Partial], txtKode.Text))
        End If

        If Not String.IsNullOrEmpty(txtDeskripsi.Text) Then
            criterias.opAnd(New Criteria(GetType(TrCourseCategory), "Description", MatchType.[Partial], txtDeskripsi.Text))
        End If

        If Not ddlKategory.SelectedValue.Equals(helpers.NotSelected) Then
            criterias.opAnd(New Criteria(GetType(TrCourseCategory), "JobPositionCategory.ID", MatchType.Exact, ddlKategory.SelectedValue))
        Else
            Dim kategoryIn As String = String.Empty
            For Each item As ListItem In ddlKategory.Items
                If Not item.Value.Equals(helpers.NotSelected) Then
                    kategoryIn = kategoryIn + item.Value + ", "
                End If
            Next
            If Not String.IsNullOrEmpty(kategoryIn) Then
                kategoryIn = kategoryIn.Remove(kategoryIn.Length - 2)
                kategoryIn = String.Format("({0})", kategoryIn)
                criterias.opAnd(New Criteria(GetType(TrCourseCategory), "JobPositionCategory.ID", MatchType.InSet, kategoryIn))
            End If
        End If

        If Not DdlStatus.SelectedValue.Equals(helpers.NotSelected) Then
            criterias.opAnd(New Criteria(GetType(TrCourseCategory), "Status", MatchType.Exact, DdlStatus.SelectedValue))
        End If

        If Not (ddlLevel.SelectedValue.Equals(helpers.NotSelected) Or String.IsNullOrEmpty(ddlLevel.SelectedValue)) Then
            criterias.opAnd(New Criteria(GetType(TrCourseCategory), "LevelID", MatchType.Exact, ddlLevel.SelectedValue))
        End If

        If Not ddlTipeKategoriKursus.SelectedValue.Equals(helpers.NotSelected) Then
            criterias.opAnd(New Criteria(GetType(TrCourseCategory), "IsMandatory", MatchType.Exact, ddlTipeKategoriKursus.SelectedValue))
        End If

        Return criterias
    End Function

    Private Sub SaveCriteriaSearch()
        helpers.AddCriteria("Code", txtKode.Text)
        helpers.AddCriteria("Description", txtDeskripsi.Text)
        helpers.AddCriteria("Status", DdlStatus.SelectedValue)
        helpers.AddCriteria("Category", ddlKategory.SelectedValue)
        helpers.AddCriteria("IsMandatory", ddlTipeKategoriKursus.SelectedValue)
        helpers.AddCriteria("Level", ddlLevel.SelectedValue)
        helpers.SaveCriteria()
    End Sub

    Private Sub ReadCritriaSearch()
        If Not helpers.IsNullCriteria Then
            txtKode.Text = helpers.GetStringCriteria("Code")
            txtDeskripsi.Text = helpers.GetStringCriteria("Description")
            DdlStatus.SelectedValue = helpers.GetStringCriteria("Status")
            ddlKategory.SelectedValue = helpers.GetStringCriteria("Category")
            ddlTipeKategoriKursus.SelectedValue = helpers.GetStringCriteria("IsMandatory")
            ddlLevel.SelectedValue = helpers.GetStringCriteria("Level")
        End If
    End Sub

    Private Sub RetrieveByCode(ByVal code As String)
        Dim data As TrCourseCategory = New TrCourseCategoryFacade(User).Retrieve(code)
        If data IsNot Nothing And data.ID <> 0 Then
            idvalue.Value = data.ID.ToString()
            BindDropDownList()
            CommonFunction.BindSalesmanLevel(ddlLevel, data.JobPositionCategory.ID, User, False)
            

            txtKode.Text = data.Code
            txtDeskripsi.Text = data.Description
            ddlKategory.ClearSelection()
            DdlStatus.ClearSelection()
            ddlTipeKategoriKursus.ClearSelection()
            ddlKategory.Items.FindByValue(data.JobPositionCategory.ID.ToString()).Selected = True
            DdlStatus.Items.FindByValue(data.Status.ToString()).Selected = True

            Dim vvAlue As String = "0"
            If data.IsMandatory Then
                vvAlue = "1"
            End If
            ddlTipeKategoriKursus.Items.FindByValue(vvAlue).Selected = True

            If data.SalesmanLevel IsNot Nothing Then
                ddlLevel.ClearSelection()
                ddlLevel.Items.FindByValue(data.SalesmanLevel.ID.ToString()).Selected = True
            End If

            If Not ddlKategory.SelectedValue.Equals(helpers.NotSelected) Then
                BindJobPosition(listCheckJobPosition, data.JobPositionCategory.ID.ToString())
            End If

            If data.IsMandatory Then
                trjobposition.Visible = True
                cbxAll.Visible = True

                For Each item As TrCourseCategoryToJobPosition In data.ListOfJobPosition
                    Dim liItem As ListItem = listCheckJobPosition.Items.FindByValue(item.JobPosition.ID.ToString())
                    liItem.Selected = True
                    liItem.Enabled = True
                Next
            End If

        End If
    End Sub

    Private Function GetCourseCategoryfromUI(Optional ByVal id As Integer = 0) As TrCourseCategory
        Dim rest As TrCourseCategory = New TrCourseCategory()
        
        If Not id.Equals(0) Then
            rest.ID = id
        End If
        rest.Status = CType(DdlStatus.SelectedValue, Short)
        rest.Code = txtKode.Text
        rest.Description = txtDeskripsi.Text
        rest.JobPositionCategory = New JobPositionCategoryFacade(User).Retrieve(Integer.Parse(ddlKategory.SelectedValue))
        rest.IsMandatory = CBool(ddlTipeKategoriKursus.SelectedValue)
        If Not ddlLevel.SelectedValue.Equals(helpers.NotSelected) Then
            rest.SalesmanLevel = New SalesmanLevelFacade(User).Retrieve(Integer.Parse(ddlLevel.SelectedValue))
        End If

        Return rest
    End Function

    Private Function GetJobPositionFromUI() As ArrayList
        Dim strIn As String = helpers.StringInFromListItem(listCheckJobPosition.Items)
        Dim arrJobPosition As ArrayList = New ArrayList()

        If Not String.IsNullOrEmpty(strIn) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(JobPosition), "ID", MatchType.InSet, strIn))
            arrJobPosition = New JobPositionFacade(User).Retrieve(criterias)
        End If

        Return arrJobPosition
    End Function

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        SaveCriteriaSearch()
        GridCourseCtg.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub

    Private Sub dgCourseCategory_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles GridCourseCtg.ItemCommand

        If (e.CommandName.Contains("View")) Then
            GridCourseCtg.SelectedIndex = -1
            Dim Code As String = e.Item.Cells(1).Text
            ModeLihat(Code)
        ElseIf e.CommandName.Contains("Edit") Then
            GridCourseCtg.SelectedIndex = e.Item.ItemIndex
            Dim Code As String = e.Item.Cells(1).Text
            ModeEdit(Code)
        ElseIf e.CommandName.Contains("Delete") Then
            Dim Code As String = e.Item.Cells(1).Text
            If IdDelete.Value.ToLower().Equals("yes") Then
                ModeDelete(Code)
            End If

        End If
    End Sub

    Private Sub ModeDelete(ByVal code As String)
        Dim data As TrCourseCategory = New TrCourseCategoryFacade(User).Retrieve(code)
        Dim func As TrCourseCategoryFacade = New TrCourseCategoryFacade(User)
        data.RowStatus = CType(DBRowStatus.Deleted, Short)
        func.Update(data)
        MessageBox.Show("Hapus data berhasil")
        IdDelete.Value = "No"
        BindDataGrid(GridCourseCtg.CurrentPageIndex)
    End Sub

    Private Sub ModeLihat(ByVal code As String)
        RetrieveByCode(code)
        helpers.ModeReadOnly(trInput)
        listCheckJobPosition.Enabled = False
        btnSimpan.Enabled = False
        btnBatal.Visible = True
        btnCari.Visible = True
        btnSimpan.Visible = True
    End Sub

    Private Sub ModeEdit(ByVal code As String)
        listCheckJobPosition.Enabled = True
        helpers.ClearData(trInput, True)
        btnSimpan.Enabled = True
        RetrieveByCode(code)
        txtKode.Disabled()
        ddlKategory.Enabled = False
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        helpers.ClearData(trInput, True)
        listCheckJobPosition.ClearSelection()
        listCheckJobPosition.Items.Clear()
        trjobposition.Visible = False
        cbxAll.Checked = False
        cbxAll.Visible = False
        trLevel.Visible = False
        listCheckJobPosition.Enabled = True
        idvalue.Value = String.Empty
        ddlKategory.Visible = True
        DdlStatus.Visible = True
        ddlTipeKategoriKursus.Visible = True
        BindDropDownList()
        GridCourseCtg.SelectedIndex = -1

        If helpers.IsEdit Then
            btnSimpan.Enabled = True
        End If
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        helpers.AddValidatation(txtKode, "Kode", "required")
        helpers.AddValidatation(txtDeskripsi, "Deskripsi", "required")
        helpers.AddValidatation(ddlKategory, "Kategori", "required")
        helpers.AddValidatation(ddlTipeKategoriKursus, "Tipe Kategori Kursus", "required")
        helpers.AddValidatation(DdlStatus, "Status", "required")
        Dim rest As ActionResult = helpers.CheckValidatiaon()
        Dim result As Integer = 0

        If rest.Status.Equals(EnumStatusActive.Fail) Then
            MessageBox.Show(rest.Message)
            Exit Sub
        End If

        Dim data As TrCourseCategory
        Dim dataJobs As ArrayList = New ArrayList()
        Dim UniqueId As Integer = 0

        If ddlTipeKategoriKursus.SelectedValue.Equals("1") Then
            dataJobs = GetJobPositionFromUI()
            If dataJobs.Count.Equals(0) Then
                MessageBox.Show("Posisi harus dipilih")
                Exit Sub
            End If
        End If

        If Not String.IsNullOrEmpty(idvalue.Value) Then
            UniqueId = Integer.Parse(idvalue.Value)

            If DdlStatus.SelectedValue = 0 And IsHaveActiveCourse(UniqueId) Then
                MessageBox.Show("Tidak bisa menonaktifkan kategori kursus karena memiliki training aktif")
                Exit Sub
            End If

        End If

        data = GetCourseCategoryfromUI(UniqueId)
        If CheckCode(data) Then
            MessageBox.Show("Kode sudah digunakan")
            Exit Sub
        End If

      

        If data.ID.Equals(0) Then
            result = New TrCourseCategoryFacade(User).Insert(data)
            data.ID = result
        Else
            result = New TrCourseCategoryFacade(User).Update(data)
            If Not data.IsMandatory Then
                Dim criteria As New CriteriaComposite(New Criteria(GetType(TrCourseCategoryToJobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(TrCourseCategoryToJobPosition), "TrCourseCategory.ID", MatchType.Exact, data.ID))

                Dim dataJobsExist As ArrayList = New TrCourseCategoryToJobPositionFacade(User).Retrieve(criteria)
                For Each dataJobExist As TrCourseCategoryToJobPosition In dataJobsExist
                    dataJobExist.RowStatus = CType(DBRowStatus.Deleted, Short)
                    Dim restA As Integer = New TrCourseCategoryToJobPositionFacade(User).Update(dataJobExist)
                Next
            End If
        End If

        If data.IsMandatory Then
            Dim dataJobNew As List(Of JobPosition) = dataJobs.Cast(Of JobPosition).ToList()

            Dim criteria As New CriteriaComposite(New Criteria(GetType(TrCourseCategoryToJobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(TrCourseCategoryToJobPosition), "TrCourseCategory.ID", MatchType.Exact, data.ID))

            Dim dataJobsExist As ArrayList = New TrCourseCategoryToJobPositionFacade(User).Retrieve(criteria)
            For Each jobExistItem As TrCourseCategoryToJobPosition In dataJobsExist
                If dataJobNew.Where(Function(x) x.ID = jobExistItem.JobPosition.ID).Count.Equals(0) Then
                    jobExistItem.RowStatus = CType(DBRowStatus.Deleted, Short)
                    Dim restA As TrCourseCategoryToJobPositionFacade = New TrCourseCategoryToJobPositionFacade(User)
                    Dim rst As Integer = restA.Update(jobExistItem)
                End If
            Next

            For Each itemJob As JobPosition In dataJobs
                Dim dataJobItem As TrCourseCategoryToJobPosition = New TrCourseCategoryToJobPosition()
                dataJobItem.JobPosition = itemJob
                dataJobItem.TrCourseCategory = data

                Dim restultJob As Integer = 0
                Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourseCategoryToJobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(TrCourseCategoryToJobPosition), "TrCourseCategory.ID", MatchType.Exact, data.ID))
                criterias.opAnd(New Criteria(GetType(TrCourseCategoryToJobPosition), "JobPosition.ID", MatchType.Exact, itemJob.ID))

                Dim dataItem As ArrayList = New TrCourseCategoryToJobPositionFacade(User).Retrieve(criterias)
                If dataItem.Count.Equals(0) Then
                    restultJob = New TrCourseCategoryToJobPositionFacade(User).Insert(dataJobItem)
                End If
            Next
        End If

        helpers.ClearData(trInput, True)
        If data.IsMandatory Then
            ddlLevel.ClearSelection()
            ddlLevel.Items.Clear()
            listCheckJobPosition.ClearSelection()
            listCheckJobPosition.Items.Clear()
            trjobposition.Visible = False
            cbxAll.Checked = False
            cbxAll.Visible = False
            trLevel.Visible = False
        End If

        MessageBox.Show("Data Berhasil disimpan")
        btnBatal_Click(Nothing, Nothing)
        '  BindDropDownList()
        BindDataGrid(0)
        'GridCourseCtg.SelectedIndex = -1
    End Sub

    Private Function CheckCode(ByVal ctgCourse As TrCourseCategory) As Boolean
        If Not ctgCourse.ID.Equals(0) Then
            Return False
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourseCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrCourseCategory), "Code", MatchType.Exact, ctgCourse.Code))
        criterias.opAnd(New Criteria(GetType(TrCourseCategory), "JobPositionCategory.JobPositionCategoryArea.ID", MatchType.Exact, AreaId))

        If New TrCourseCategoryFacade(User).Retrieve(criterias).Count > 0 Then
            Return True
        End If

        Return False
    End Function

    Private Sub GridCourseCtg_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles GridCourseCtg.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objTrCtg As TrCourseCategory = e.Item.DataItem
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblTypeKategori As Label = CType(e.Item.FindControl("lblTipeKategori"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblCtg As Label = CType(e.Item.FindControl("lblKategori"), Label)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)

            lblNo.Text = e.Item.ItemIndex + 1 + (GridCourseCtg.CurrentPageIndex * GridCourseCtg.PageSize)
            lblStatus.Text = DdlStatus.Items.FindByValue(objTrCtg.Status.ToString()).Text
            lblCtg.Text = objTrCtg.JobPositionCategory.Description
            Dim vvAlue As String = "0"
            If objTrCtg.IsMandatory Then
                vvAlue = "1"
            End If
            lblTypeKategori.Text = ddlTipeKategoriKursus.Items.FindByValue(vvAlue).Text

            lbtnDelete.Visible = False
            lbtnEdit.Visible = False
            If helpers.IsEdit Then
                lbtnEdit.Visible = True
                If IsHaveActiveCourse(objTrCtg.ID) = True Then
                    lbtnDelete.Visible = False
                Else
                    lbtnDelete.Visible = True
                End If
            End If

        End If
    End Sub

    Private Sub GridCourseCtg_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles GridCourseCtg.PageIndexChanged
        GridCourseCtg.SelectedIndex = -1
        GridCourseCtg.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(GridCourseCtg.CurrentPageIndex)
    End Sub


    Private Sub GridCourseCtg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridCourseCtg.SelectedIndexChanged

    End Sub
    ''' <summary>
    ''' Create By Moh Ridwan
    ''' Create Date 28-06-2019
    ''' Description : Meruba ackground cell pada row menjadi warna kuning
    ''' </summary>
    ''' <param name="ctr"></param>
    ''' <remarks></remarks>
    Private Sub TableCellColor(ByVal ctr As Control, ByVal color As Color)
        If TypeOf ctr Is TableCell Then
            Dim tblCell As TableCell = DirectCast(ctr, TableCell)
            tblCell.BackColor = color
        End If

        If ctr.HasControls Then
            For Each childclr As Control In ctr.Controls
                TableCellColor(childclr, color)
            Next
        End If
    End Sub

    Private Sub GridCourseCtg_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles GridCourseCtg.SortCommand
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

        GridCourseCtg.SelectedIndex = -1
        GridCourseCtg.CurrentPageIndex = 0
        BindDataGrid(GridCourseCtg.CurrentPageIndex)
    End Sub

    Protected Sub cbxAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbxAll.CheckedChanged
        listCheckJobPosition.ClearSelection()
        If cbxAll.Checked Then
            For Each item As ListItem In listCheckJobPosition.Items
                If item.Enabled = True Then
                    item.Selected = True
                End If
            Next
        End If
    End Sub

    Private Function IsHaveActiveCourse(ByVal TrCourseCategoryId As Integer) As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrCourse), "Category.ID", MatchType.Exact, TrCourseCategoryId))
        criterias.opAnd(New Criteria(GetType(TrCourse), "Status", MatchType.Exact, 1)) 'Active Training

        Dim arlResult As ArrayList = New TrCourseFacade(User).Retrieve(criterias)

        If arlResult.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

End Class