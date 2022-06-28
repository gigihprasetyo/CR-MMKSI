Imports System.IO
Imports System.Text
Imports OfficeOpenXml
Imports System.Collections.Generic
Imports System.Linq
Imports System.Security.Principal
Imports System.Web.UI.Page
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports GlobalExtensions

Public Class FrmTrCourseEvaluationNew
    Inherits System.Web.UI.Page

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
            lblTitle.Text = "Training Sales - Jenis Evaluasi"
            IDArea.Value = "sales"
        ElseIf areaid.Equals("2") Then
            lblTitle.Text = "Training After Sales - Jenis Evaluasi"
            IDArea.Value = "ass"
        Else
            lblTitle.Text = "Training Customer Satisfaction - Jenis Evaluasi"
            IDArea.Value = "cs"
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        helpers.CheckPrivilege("priv3" + PrivArea)
        If Not Page.IsPostBack Then
            TitleDescription(AreaId)
            tdRef.Visible = False
            DefaultGrid()
            ViewState("CurrentSortColumn1") = "EvaluationCode"
            ViewState("CurrentSortDirect1") = Sort.SortDirection.ASC
            ViewState("CurrentSortColumn2") = "EvaluationCode"
            ViewState("CurrentSortDirect2") = Sort.SortDirection.ASC
            IDProccess.Value = "begin"

            trReferensi.Visible = helpers.IsEdit
            BtEdit.Visible = helpers.IsEdit
        End If
    End Sub

    Protected Sub Menu1_MenuItemClick(sender As Object, e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiTabs.ActiveViewIndex = Int32.Parse(e.Item.Value)
        For i As Integer = 0 To Menu1.Items.Count - 1
            If i = e.Item.Value Then
                Menu1.Items(i).ImageUrl = "../images/aktif.gif"
            Else
                Menu1.Items(i).ImageUrl = String.Empty
            End If
        Next
        Select Case IDProccess.Value
            Case "edit"
                If String.IsNullorEmpty(txtCourseCode.Text) Or (String.IsNullorEmpty(txtCodeRef.Text) And chkItemChecked.Checked) Then
                    DefaultGrid()
                    Exit Sub
                End If

                Dim objTrCourse As New TrCourse
                If chkItemChecked.Checked Then
                    objTrCourse = New TrCourseFacade(User).Retrieve(txtCodeRef.Text)
                Else
                    objTrCourse = New TrCourseFacade(User).Retrieve(txtCourseCode.Text)
                End If

                If objTrCourse.ID.Equals(0) Then
                    DefaultGrid()
                    Exit Sub
                Else
                    Dim arrArea As List(Of JobPositionToCategory) = New List(Of JobPositionToCategory)
                    arrArea = New JobPositionToCategoryFacade(User).RetrieveActiveList().Cast(Of  _
                            JobPositionToCategory).Where(Function(x) _
                            x.JobPositionCategory.JobPositionCategoryArea.ID = Integer.Parse(AreaId) And x.CategoryID = _
                            objTrCourse.JobPositionCategory.ID).ToList()
                    If arrArea.Count.Equals(0) Then
                        DefaultGrid()
                        Exit Sub
                    End If
                End If
                GetDataEvaluation(objTrCourse)
            Case "lihat"
                If String.IsNullorEmpty(txtCourseCode.Text) Then
                    DefaultGrid()
                    Exit Sub
                End If

                Dim objTrCourse As TrCourse = New TrCourseFacade(User).Retrieve(txtCourseCode.Text)
                If objTrCourse.ID.Equals(0) Then
                    DefaultGrid()
                    Exit Sub
                Else
                    Dim arrArea As List(Of JobPositionToCategory) = New List(Of JobPositionToCategory)
                    arrArea = New JobPositionToCategoryFacade(User).RetrieveActiveList().Cast(Of  _
                            JobPositionToCategory).Where(Function(x) _
                            x.JobPositionCategory.JobPositionCategoryArea.ID = Integer.Parse(AreaId) And x.CategoryID = _
                            objTrCourse.JobPositionCategory.ID).ToList()
                    If arrArea.Count.Equals(0) Then
                        DefaultGrid()
                        Exit Sub
                    End If
                End If
                GetDataEvaluation(objTrCourse)
            Case "refEdit"
                If String.IsNullorEmpty(txtCourseCode.Text) Or String.IsNullorEmpty(txtCodeRef.Text) Then
                    DefaultGrid()
                    Exit Sub
                End If

                Dim objTrCourse As TrCourse = New TrCourseFacade(User).Retrieve(txtCodeRef.Text)
                If objTrCourse.ID.Equals(0) Then
                    DefaultGrid()
                    Exit Sub
                Else
                    Dim arrArea As List(Of JobPositionToCategory) = New List(Of JobPositionToCategory)
                    arrArea = New JobPositionToCategoryFacade(User).RetrieveActiveList().Cast(Of  _
                            JobPositionToCategory).Where(Function(x) _
                            x.JobPositionCategory.JobPositionCategoryArea.ID = Integer.Parse(AreaId) And x.CategoryID = _
                            objTrCourse.JobPositionCategory.ID).ToList()
                    If arrArea.Count.Equals(0) Then
                        DefaultGrid()
                        Exit Sub
                    End If
                End If
                GetDataEvaluation(objTrCourse)
        End Select

    End Sub

    Private Sub DefaultGrid()
        btnSimpan.Visible = False
        btnBatal.Visible = False
        LinkAdd1.Visible = False
        LinkAdd2.Visible = False
        dtgNilaiAngka.DataSource = New ArrayList()
        dtgNilaiAngka.DataBind()
        dtgNilaiSikap.DataSource = New ArrayList()
        dtgNilaiSikap.DataBind()
    End Sub

    Protected Sub BtnLihat_Click(sender As Object, e As EventArgs) Handles BtnLihat.Click
        If String.IsNullorEmpty(txtCourseCode.Text) Then
            MessageBox.Show("Masukan Kode kategori")
            Exit Sub
        End If

        Dim objTrCourse As TrCourse = New TrCourseFacade(User).Retrieve(txtCourseCode.Text)
        If objTrCourse.ID.Equals(0) Then
            MessageBox.Show("Kode kategori tidak ditemukan")
            Exit Sub
        Else
            Dim arrArea As List(Of JobPositionToCategory) = New List(Of JobPositionToCategory)
            arrArea = New JobPositionToCategoryFacade(User).RetrieveActiveList().Cast(Of  _
                    JobPositionToCategory).Where(Function(x) _
                    x.JobPositionCategory.JobPositionCategoryArea.ID = Integer.Parse(AreaId) And x.CategoryID = _
                    objTrCourse.JobPositionCategory.ID).ToList()
            If arrArea.Count.Equals(0) Then
                MessageBox.Show("Kode kategori tidak ditemukan")
                Exit Sub
            End If

        End If

        IDProccess.Value = "lihat"
        GetDataEvaluation(objTrCourse)
        ModeEdit(False)
    End Sub

    Protected Sub BtEdit_Click(sender As Object, e As EventArgs) Handles BtEdit.Click
        If String.IsNullorEmpty(txtCourseCode.Text) Then
            MessageBox.Show("Masukan Kode kategori")
            Exit Sub
        End If

        Dim objTrCourse As TrCourse = New TrCourseFacade(User).Retrieve(txtCourseCode.Text)
        If objTrCourse.ID.Equals(0) Then
            MessageBox.Show("Kode kategori tidak ditemukan")
            Exit Sub
        Else
            Dim arrArea As List(Of JobPositionToCategory) = New List(Of JobPositionToCategory)
            arrArea = New JobPositionToCategoryFacade(User).RetrieveActiveList().Cast(Of  _
                    JobPositionToCategory).Where(Function(x) _
                    x.JobPositionCategory.JobPositionCategoryArea.ID = Integer.Parse(AreaId) And x.CategoryID = _
                    objTrCourse.JobPositionCategory.ID).ToList()
            If arrArea.Count.Equals(0) Then
                MessageBox.Show("Kode kategori tidak ditemukan")
                Exit Sub
            End If
        End If
        IDCourse.Value = objTrCourse.ID
        IDProccess.Value = "edit"
        GetDataEvaluation(objTrCourse)
        ModeEdit(True)
    End Sub

    Private Sub ModeEdit(ByVal IsActive As Boolean)
        LinkAdd1.Visible = IsActive
        LinkAdd2.Visible = IsActive
        btnSimpan.Visible = IsActive
        btnBatal.Visible = IsActive
    End Sub

    Private Sub GetDataEvaluation(ByVal objTrCourse As TrCourse)
        If MultiTabs.ActiveViewIndex = 0 Then
            BindDataAngka(objTrCourse)
        Else
            BindDataSikap(objTrCourse)
        End If
    End Sub

    Private Sub GetDataEvaluationMenu(ByVal objTrCourse As TrCourse)
        If MultiTabs.ActiveViewIndex = 0 Then
            BindDataAngka(objTrCourse, CType(helpers.GetSession("idxpage1"), Integer))
        Else
            BindDataSikap(objTrCourse, CType(helpers.GetSession("idxpage2"), Integer))
        End If
    End Sub

    Private Sub BindDataSikap(ByVal objTrCourse As TrCourse, Optional ByVal idxPage As Integer = 0)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, objTrCourse.ID))
        criterias.opAnd(New Criteria(GetType(TrCourseEvaluation), "Type", MatchType.Exact, 1))
        Dim dataEva As ArrayList = New TrCourseEvaluationFacade(User).RetrieveActiveList(idxPage + 1, dtgNilaiSikap.PageSize, totalRow, _
             CType(ViewState("CurrentSortColumn2"), String), CType(ViewState("CurrentSortDirect2"), Sort.SortDirection), _
             criterias)
        dtgNilaiSikap.DataSource = dataEva
        dtgNilaiSikap.DataBind()
        dtgNilaiSikap.VirtualItemCount = totalRow
        helpers.SetSession("idxpage2", dtgNilaiSikap.CurrentPageIndex)
    End Sub

    Private Sub BindDataAngka(ByVal objTrCourse As TrCourse, Optional ByVal idxPage As Integer = 0)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, objTrCourse.ID))
        criterias.opAnd(New Criteria(GetType(TrCourseEvaluation), "Type", MatchType.Exact, 0))
        Dim dataEva As ArrayList = New TrCourseEvaluationFacade(User).RetrieveActiveList(idxPage + 1, dtgNilaiAngka.PageSize, totalRow, _
             CType(ViewState("CurrentSortColumn1"), String), CType(ViewState("CurrentSortDirect1"), Sort.SortDirection), _
             criterias)
        dtgNilaiAngka.DataSource = dataEva
        dtgNilaiAngka.DataBind()
        dtgNilaiAngka.VirtualItemCount = totalRow
        helpers.SetSession("idxpage1", dtgNilaiAngka.CurrentPageIndex)
    End Sub

    Protected Sub LinkAdd1_Click(sender As Object, e As EventArgs) Handles LinkAdd1.Click
        Dim listDatas As List(Of TrCourseEvaluation) = GetListNilaiAngkaFromGrid()
        listDatas.Add(New TrCourseEvaluation())
        dtgNilaiAngka.DataSource = listDatas
        dtgNilaiAngka.DataBind()
    End Sub

    Protected Sub LinkAdd2_Click(sender As Object, e As EventArgs) Handles LinkAdd2.Click
        Dim listDatas As List(Of TrCourseEvaluation) = GetListNilaiSikapFromGrid()
        listDatas.Add(New TrCourseEvaluation())
        dtgNilaiSikap.DataSource = listDatas
        dtgNilaiSikap.DataBind()
    End Sub

    Private Function GetListNilaiSikapFromGrid() As IEnumerable(Of TrCourseEvaluation)
        Dim listDatas As List(Of TrCourseEvaluation) = New List(Of TrCourseEvaluation)
        Dim trCourse As TrCourse = New TrCourseFacade(User).Retrieve(Integer.Parse(IDCourse.Value))

        For Each dataItem As DataGridItem In dtgNilaiSikap.Items
            Dim objEva As TrCourseEvaluation = New TrCourseEvaluation()
            Dim txtName As TextBox = CType(dataItem.FindControl("NamaEvaluasi2"), TextBox)
            Dim txtDesc As TextBox = CType(dataItem.FindControl("Description2"), TextBox)
            Dim lblID As Label = CType(dataItem.FindControl("lblIDEva2"), Label)
            Dim lblPK As Label = CType(dataItem.FindControl("lblID2"), Label)

            objEva.EvaluationCode = lblID.Text
            objEva.Name = txtName.Text
            objEva.Description = txtDesc.Text
            objEva.TrCourse = trCourse
            objEva.Type = "0"

            If Not String.IsNullorEmpty(lblPK.Text) Then
                objEva.ID = Integer.Parse(lblPK.Text)
            End If
            listDatas.Add(objEva)
        Next
        Return listDatas
    End Function

    Private Function GetListNilaiAngkaFromGrid() As IEnumerable(Of TrCourseEvaluation)
        Dim listDatas As List(Of TrCourseEvaluation) = New List(Of TrCourseEvaluation)
        Dim trCourse As TrCourse = New TrCourseFacade(User).Retrieve(Integer.Parse(IDCourse.Value))

        For Each dataItem As DataGridItem In dtgNilaiAngka.Items
            Dim objEva As TrCourseEvaluation = New TrCourseEvaluation()
            Dim txtName As TextBox = CType(dataItem.FindControl("NamaEvaluasi1"), TextBox)
            Dim txtDesc As TextBox = CType(dataItem.FindControl("Description1"), TextBox)
            Dim lblID As Label = CType(dataItem.FindControl("lblIDEva1"), Label)
            Dim lblPK As Label = CType(dataItem.FindControl("lblID1"), Label)

            objEva.EvaluationCode = lblID.Text
            objEva.Name = txtName.Text
            objEva.Description = txtDesc.Text
            objEva.TrCourse = trCourse
            objEva.Type = "0"

            If Not String.IsNullorEmpty(lblPK.Text) Then
                objEva.ID = Integer.Parse(lblPK.Text)
            End If
            listDatas.Add(objEva)
        Next
        Return listDatas
    End Function

    Private Sub dtgNilaiAngka_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgNilaiAngka.ItemCommand
        If e.CommandName.Equals("Delete") Then
            If ConfirmDelete.Value.Equals("yes") Then
                Dim datas As List(Of TrCourseEvaluation) = GetListNilaiAngkaFromGrid()
                datas.RemoveAt(e.Item.ItemIndex)
                ConfirmDelete.Value = String.Empty
                dtgNilaiAngka.DataSource = datas
                dtgNilaiAngka.DataBind()
            End If
        End If
    End Sub

    Private Sub dtgNilaiAngka_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgNilaiAngka.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objTrEva As TrCourseEvaluation = CType(e.Item.DataItem, TrCourseEvaluation)
            Dim txtName As TextBox = CType(e.Item.FindControl("NamaEvaluasi1"), TextBox)
            Dim txtDesc As TextBox = CType(e.Item.FindControl("Description1"), TextBox)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lblPK As Label = CType(e.Item.FindControl("lblID1"), Label)
            Dim lblID As Label = CType(e.Item.FindControl("lblIDEva1"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo1"), Label)
            txtDesc.ReadOnly = False
            txtName.ReadOnly = False

            lblPK.Text = objTrEva.ID
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgNilaiAngka.CurrentPageIndex * dtgNilaiAngka.PageSize)
            txtName.Text = objTrEva.Name
            txtDesc.Text = objTrEva.Description
            If IDProccess.Value.Equals("refEdit") Then
                lblID.Text = objTrEva.EvaluationCode.Replace(txtCodeRef.Text.ToUpper().Trim + "-", txtCourseCode.Text.ToUpper().Trim + "-")
            Else
                lblID.Text = objTrEva.EvaluationCode
            End If

            If IDProccess.Value.Equals("lihat") Then
                txtDesc.Disabled()
                txtName.Disabled()
                lbtnDelete.Visible = False
            End If
            If objTrEva.EvaluationCode.Contains("A00") Or objTrEva.EvaluationCode.Contains("A99") Then
                lbtnDelete.Visible = False
            End If
        End If
        If IDProccess.Value.Equals("edit") Or IDProccess.Value.Equals("refEdit") Then
            e.Item.Cells(0).Visible = False
        Else
            e.Item.Cells(5).Visible = False
        End If
        e.Item.Cells(1).Visible = False
    End Sub

    Private Sub dtgNilaiSikap_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgNilaiSikap.ItemCommand
        If e.CommandName.Equals("Delete") Then
            If ConfirmDelete.Value.Equals("yes") Then
                Dim datas As List(Of TrCourseEvaluation) = GetListNilaiSikapFromGrid()
                datas.RemoveAt(e.Item.ItemIndex)
                ConfirmDelete.Value = String.Empty
                dtgNilaiSikap.DataSource = datas
                dtgNilaiSikap.DataBind()
            End If

        End If
    End Sub

    Private Sub dtgNilaiSikap_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgNilaiSikap.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objTrEva As TrCourseEvaluation = CType(e.Item.DataItem, TrCourseEvaluation)
            Dim txtName As TextBox = CType(e.Item.FindControl("NamaEvaluasi2"), TextBox)
            Dim txtDesc As TextBox = CType(e.Item.FindControl("Description2"), TextBox)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lblPK As Label = CType(e.Item.FindControl("lblID2"), Label)
            Dim lblID As Label = CType(e.Item.FindControl("lblIDEva2"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo2"), Label)
            txtDesc.ReadOnly = False
            txtName.ReadOnly = False

            lblPK.Text = objTrEva.ID
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgNilaiSikap.CurrentPageIndex * dtgNilaiSikap.PageSize)
            lblID.Text = objTrEva.EvaluationCode
            txtName.Text = objTrEva.Name
            txtDesc.Text = objTrEva.Description
            If IDProccess.Value.Equals("refEdit") Then
                lblID.Text = objTrEva.EvaluationCode.Replace(txtCodeRef.Text.ToUpper().Trim + "-", txtCourseCode.Text.ToUpper().Trim + "-")
            Else
                lblID.Text = objTrEva.EvaluationCode
            End If
            If IDProccess.Value.Equals("lihat") Then
                txtDesc.Disabled()
                txtName.Disabled()
                lbtnDelete.Visible = False
            End If
            If objTrEva.EvaluationCode.Contains("A00") Or objTrEva.EvaluationCode.Contains("A99") Then
                lbtnDelete.Visible = False
            End If
        End If
        If IDProccess.Value.Equals("edit") Or IDProccess.Value.Equals("refEdit") Then
            e.Item.Cells(0).Visible = False
        Else
            e.Item.Cells(5).Visible = False
        End If
        e.Item.Cells(1).Visible = False
    End Sub

    Private Sub dtgNilaiAngka_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgNilaiAngka.PageIndexChanged
        If String.IsNullorEmpty(txtCourseCode.Text) Then
            DefaultGrid()
            Exit Sub
        End If

        Dim objTrCourse As TrCourse = New TrCourseFacade(User).Retrieve(txtCourseCode.Text)
        If objTrCourse.ID.Equals(0) Then
            DefaultGrid()
            Exit Sub
        End If
        dtgNilaiAngka.CurrentPageIndex = e.NewPageIndex
        BindDataAngka(objTrCourse, dtgNilaiAngka.CurrentPageIndex)
    End Sub

    Private Sub dtgNilaiSikap_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgNilaiSikap.PageIndexChanged
        If String.IsNullorEmpty(txtCourseCode.Text) Then
            DefaultGrid()
            Exit Sub
        End If

        Dim objTrCourse As TrCourse = New TrCourseFacade(User).Retrieve(txtCourseCode.Text)
        If objTrCourse.ID.Equals(0) Then
            DefaultGrid()
            Exit Sub
        End If
        dtgNilaiSikap.CurrentPageIndex = e.NewPageIndex
        BindDataSikap(objTrCourse, dtgNilaiSikap.CurrentPageIndex)
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim listDatas As List(Of TrCourseEvaluation) = New List(Of TrCourseEvaluation)
        Dim trCourse As TrCourse = New TrCourse()
        If IDProccess.Value.Equals("refEdit") Then
            trCourse = New TrCourseFacade(User).Retrieve(txtCourseCode.Text)
        Else
            trCourse = New TrCourseFacade(User).Retrieve(Integer.Parse(IDCourse.Value))
        End If

        Dim isDataValid As Boolean = True
        If MultiTabs.ActiveViewIndex = 0 Then
            For Each dataItem As DataGridItem In dtgNilaiAngka.Items
                Dim objEva As TrCourseEvaluation = New TrCourseEvaluation()
                Dim txtName As TextBox = CType(dataItem.FindControl("NamaEvaluasi1"), TextBox)
                Dim txtDesc As TextBox = CType(dataItem.FindControl("Description1"), TextBox)
                Dim lblID As Label = CType(dataItem.FindControl("lblIDEva1"), Label)
                Dim lblPK As Label = CType(dataItem.FindControl("lblID1"), Label)

                objEva.EvaluationCode = lblID.Text
                objEva.Name = txtName.Text
                objEva.Description = txtDesc.Text
                objEva.TrCourse = trCourse
                objEva.Type = "0"

                If String.IsNullorEmpty(objEva.Name) Then
                    isDataValid = False
                    txtName.BorderColor = Color.OrangeRed
                Else
                    txtName.BorderColor = Color.Empty
                End If

                If String.IsNullorEmpty(objEva.EvaluationCode) Then
                    objEva.EvaluationCode = "A"
                End If

                If Not String.IsNullorEmpty(lblPK.Text) Then
                    objEva.ID = Integer.Parse(lblPK.Text)
                End If

                listDatas.Add(objEva)
            Next
        Else
            For Each dataItem As DataGridItem In dtgNilaiSikap.Items
                Dim objEva As TrCourseEvaluation = New TrCourseEvaluation()
                Dim txtName As TextBox = CType(dataItem.FindControl("NamaEvaluasi2"), TextBox)
                Dim txtDesc As TextBox = CType(dataItem.FindControl("Description2"), TextBox)
                Dim lblID As Label = CType(dataItem.FindControl("lblIDEva2"), Label)
                Dim lblPK As Label = CType(dataItem.FindControl("lblID2"), Label)

                objEva.EvaluationCode = lblID.Text
                objEva.Name = txtName.Text
                objEva.Description = txtDesc.Text
                objEva.TrCourse = trCourse
                objEva.Type = "1"

                If String.IsNullorEmpty(objEva.Name) Then
                    isDataValid = False
                    txtName.BorderColor = Color.OrangeRed
                Else
                    txtName.BorderColor = Color.Empty
                End If

                If String.IsNullorEmpty(objEva.EvaluationCode) Then
                    objEva.EvaluationCode = "B"
                End If

                If Not String.IsNullorEmpty(lblPK.Text) Then
                    objEva.ID = Integer.Parse(lblPK.Text)
                End If

                listDatas.Add(objEva)
            Next
        End If
        If Not isDataValid Then
            MessageBox.Show("Harap periksa kembali data anda")
            Exit Sub
        End If
        Dim func As TrCourseEvaluationFacade = New TrCourseEvaluationFacade(User)
        Dim vValueA As String = trCourse.CourseCode.ToUpper() + "-A"
        Dim vValueB As String = trCourse.CourseCode.ToUpper() + "-B"

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, trCourse.ID))
        criterias.opAnd(New Criteria(GetType(TrCourseEvaluation), "Type", MatchType.Exact, MultiTabs.ActiveViewIndex))
        Dim dataExist As List(Of TrCourseEvaluation) = func.Retrieve(criterias).Cast(Of TrCourseEvaluation).ToList()
        For Each dataEva As TrCourseEvaluation In dataExist
            'Cek data apakah sudah dihapus di Grid
            If listDatas.Where(Function(x) x.EvaluationCode.Equals(dataEva.EvaluationCode)).Count.Equals(0) Then
                dataEva.RowStatus = CType(DBRowStatus.Deleted, Short)
                func.Update(dataEva)
            End If
        Next

        For Each itemEva As TrCourseEvaluation In listDatas
            If IDProccess.Value.Equals("edit") Then
                If itemEva.EvaluationCode.Equals("A") Then
                    Dim nValueA As Integer = GetMaxCount(trCourse, "0")
                    itemEva.EvaluationCode = vValueA + nValueA.GenerateIncrement(2)

                    func.Insert(itemEva)
                ElseIf itemEva.EvaluationCode.Equals("B") Then
                    Dim nValueB As Integer = GetMaxCount(trCourse, "1")
                    itemEva.EvaluationCode = vValueB + nValueB.GenerateIncrement(2)

                    func.Insert(itemEva)
                Else
                    func.Update(itemEva)
                End If
            Else
                If itemEva.EvaluationCode.Length > 2 Then
                    For Each dataEva As TrCourseEvaluation In dataExist
                        dataEva.RowStatus = CType(DBRowStatus.Deleted, Short)
                        func.Update(dataEva)
                    Next
                    func.Insert(itemEva)
                Else
                    If itemEva.EvaluationCode.Equals("A") Then
                        Dim nValueA As Integer = GetMaxCount(trCourse, "0")
                        nValueA = nValueA + 1
                        If nValueA >= 10 Then
                            itemEva.EvaluationCode = vValueA + nValueA.ToString
                        Else
                            itemEva.EvaluationCode = vValueA + "0" + nValueA.ToString
                        End If
                        func.Insert(itemEva)
                    ElseIf itemEva.EvaluationCode.Equals("B") Then
                        Dim nValueB As Integer = GetMaxCount(trCourse, "1")
                        nValueB = nValueB + 1
                        If nValueB >= 10 Then
                            itemEva.EvaluationCode = vValueB + nValueB.ToString
                        Else
                            itemEva.EvaluationCode = vValueB + "0" + nValueB.ToString
                        End If
                        func.Insert(itemEva)
                    End If
                End If
            End If
        Next
        MessageBox.Show("Simpan Data Berhasil")
        IDProccess.Value = "lihat"
        GetDataEvaluation(trCourse)
        ModeEdit(False)

    End Sub

    Private Function GetMaxCount(ByVal objTrCourse As TrCourse, ByVal type As String) As Integer
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrCourseEvaluation), "Type", MatchType.Exact, type))
        criterias.opAnd(New Criteria(GetType(TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, objTrCourse.ID))
        Dim arrEva As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(criterias)
        Dim vValue As Integer = 0
        Dim splitType As Char = "A"
        If type.Equals("1") Then
            splitType = "B"
        End If

        For Each item As TrCourseEvaluation In arrEva
            Dim splitValue() As String = item.EvaluationCode.Split(splitType)
            Dim nValue As Integer = Integer.Parse(splitValue(splitValue.Length - 1))
            If nValue = 0 Or nValue = 99 Then
                Continue For
            End If

            If nValue > vValue Then
                vValue = nValue
            End If
        Next
        Return vValue + 1
    End Function

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        txtCourseCode.Clear()
        txtCodeRef.Clear()
        chkItemChecked.Checked = False
        tdRef.Visible = False
        IDCourse.Value = String.Empty
        DefaultGrid()
        txtCourseCode.Focus()
    End Sub

    Private Sub chkItemChecked_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemChecked.CheckedChanged
        If chkItemChecked.Checked Then
            tdRef.Visible = True
            BtnLihat.Enabled = False
            BtEdit.Enabled = False
        Else
            txtCodeRef.Clear()
            BtnLihat.Enabled = True
            BtEdit.Enabled = True
            tdRef.Visible = False
        End If
        DefaultGrid()

    End Sub

    Protected Sub btncheck_Click(sender As Object, e As EventArgs) Handles btncheck.Click
        If String.IsNullorEmpty(txtCourseCode.Text) Then
            MessageBox.Show("Masukan Kode kategori")
            Exit Sub
        End If

        Dim objTrCourse As TrCourse = New TrCourseFacade(User).Retrieve(txtCodeRef.Text)
        If objTrCourse.ID.Equals(0) Then
            MessageBox.Show("Reference Kode kategori tidak ditemukan")
            Exit Sub
        Else
            Dim arrArea As List(Of JobPositionToCategory) = New List(Of JobPositionToCategory)
            arrArea = New JobPositionToCategoryFacade(User).RetrieveActiveList().Cast(Of  _
                    JobPositionToCategory).Where(Function(x) _
                    x.JobPositionCategory.JobPositionCategoryArea.ID = Integer.Parse(AreaId) And x.CategoryID = _
                    objTrCourse.JobPositionCategory.ID).ToList()
            If arrArea.Count.Equals(0) Then
                MessageBox.Show("Reference Kode kategori tidak ditemukan")
                Exit Sub
            End If
        End If
        IDCourse.Value = objTrCourse.ID
        IDProccess.Value = "refEdit"
        GetDataEvaluation(objTrCourse)
        ModeEdit(True)
    End Sub
End Class