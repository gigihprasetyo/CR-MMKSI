Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.Collections.Generic
Imports GlobalExtensions

Public Class FrmTrCourse
    Inherits System.Web.UI.Page
    Dim sHCourse As SessionHelper = New SessionHelper
    Protected WithEvents txtBtsLulus As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RangeValidator1 As System.Web.UI.WebControls.RangeValidator
    Protected WithEvents txtNotes As System.Web.UI.WebControls.TextBox
    Protected WithEvents txCodeKelas As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRequireWorkDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTipePembayaran As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
    Protected WithEvents Table2 As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents tablebaru As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents hdnCategory As System.Web.UI.WebControls.HiddenField
    Private m_bFormPrivilege As Boolean = False

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlLevel As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCourseCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCourseName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescCourse As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgCourse As System.Web.UI.WebControls.DataGrid
    Protected WithEvents chkRequireWorkDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkCodeKelas As System.Web.UI.WebControls.CheckBox
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page)

    Private ReadOnly Property AreaId As String
        Get
            If Request.QueryString("area") Is Nothing Then
                Return String.Empty
            End If
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
            lblTitle.Text = "Training Sales - Kategori Training"
        ElseIf areaid.Equals("2") Then
            lblTitle.Text = "Training After Sales - Kategori Training"
        Else
            lblTitle.Text = "Training Customer Satisfaction - Kategori Training"
        End If
        txtBtsLulus.Text = "70"
        If Not helpers.IsEdit Then
            btnSimpan.Enabled = False
        End If

    End Sub

    Private Sub BindDropdownList()
        helpers.BindDDLCategory(ddlKategory, AreaId)
        CommonFunction.BindDDLFromStandartCode("EnumTipePembayaran", ddlTipePembayaran)
        BindddlCourse()
        If ddlKategory.Items.Count.Equals(2) Then
            ddlKategory.SelectedIndex = 1
            BindDDLCategoryKursus(ddlKategory.SelectedValue)
            BindDDLLevel(ddlKategory.SelectedValue)
        Else
            BindDDLCategoryKursus()
            BindDDLLevel()
        End If

        txtBtsLulus.Text = "70"
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        helpers.CheckPrivilege("priv2" + PrivArea)
        If Not IsPostBack Then
            InitiatePage()
            TitleDescription(AreaId)
            BindDropdownList()
            ReadCritriaSearch()
            BindDataGrid(0)
        End If

        Try
            If ddlCategory.Items.Count > 1 Then
                For Each item As ListItem In ddlCategory.Items

                    If item.Value <> "-1" Then
                        Dim data As TrCourseCategory = New TrCourseCategoryFacade(User).Retrieve(CInt(item.Value))
                        If data.Status = 0 Then
                            item.Attributes.CssStyle.Add("background-color", "Gray")
                        End If
                    End If
                
                Next
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = helpers.IsEdit
        btnBatal.Visible = helpers.IsEdit
    End Sub

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "CourseCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindddlCourse()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem("Semua Status", "-1"))
        Dim obj As New EnumTrDataStatus
        Dim arlStatusReg As ArrayList = obj.Retrieve()
        For Each en As EnumDataStatus In arlStatusReg
            Dim lItem As ListItem = New ListItem(en.NameType, en.ValueType.ToString())
            ddlStatus.Items.Add(lItem)
        Next
        ddlStatus.ClearSelection()
        ddlStatus.SelectedIndex = 2
    End Sub

    Private Sub BindDdlCategory()
        ddlCategory.Items.Clear()
        ddlCategory.Items.Add(New ListItem("Semua Kategori", "-1"))
        Dim arrListEnumTrClass As ArrayList = EnumTrClass.RetrieveEnumTrClass()
        Dim objClassCat As New TrClassCategory
        For i As Integer = 0 To arrListEnumTrClass.Count - 1
            objClassCat = CType(arrListEnumTrClass(i), TrClassCategory)
            ddlCategory.Items.Add(New ListItem(objClassCat.Name, objClassCat.ID.ToString()))
        Next i
        ddlCategory.ClearSelection()
        ddlCategory.SelectedIndex = 0
    End Sub

    Private Sub ReadCritriaSearch()
        If Not helpers.IsNullCriteria Then
            txtCourseCode.Text = helpers.GetStringCriteria("CourseCode")
            txCodeKelas.Text = helpers.GetStringCriteria("ClassCode")
            chkRequireWorkDate.Checked = IIf(helpers.GetStringCriteria("isWorkDate") = "False", False, True)
            txtRequireWorkDate.Text = helpers.GetStringCriteria("WorkDate")
            txtCourseName.Text = helpers.GetStringCriteria("CourseName")
            ddlStatus.SelectedValue = helpers.GetStringCriteria("Status")
            ddlCategory.SelectedValue = helpers.GetStringCriteria("Category")
            ddlLevel.SelectedValue = helpers.GetStringCriteria("TrTraineeLevel.ID")
            ddlTipePembayaran.SelectedValue = helpers.GetStringCriteria("PaymentType")
            ddlKategory.SelectedValue = helpers.GetStringCriteria("JobPositionCategory.ID")
        End If
    End Sub

    Private Sub SaveCriteriaSearch()
        helpers.AddCriteria("CourseCode", txtCourseCode.Text)
        helpers.AddCriteria("ClassCode", txCodeKelas.Text)
        helpers.AddCriteria("isWorkDate", chkRequireWorkDate.Checked.ToString())
        helpers.AddCriteria("WorkDate", txtRequireWorkDate.Text)
        helpers.AddCriteria("CourseName", txtCourseName.Text)
        helpers.AddCriteria("Status", ddlStatus.SelectedValue)
        helpers.AddCriteria("Category", ddlCategory.SelectedValue)
        helpers.AddCriteria("TrTraineeLevel.ID", ddlLevel.SelectedValue)
        helpers.AddCriteria("PaymentType", ddlTipePembayaran.SelectedValue)
        helpers.AddCriteria("JobPositionCategory.ID", ddlKategory.SelectedValue)
        helpers.SaveCriteria()
    End Sub

    Private Function CreateCriteriaComposite() As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourse), _
        "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))


        If Not String.IsNullOrEmpty(txtCourseCode.Text.Trim()) Then
            criterias.opAnd(New Criteria(GetType(TrCourse), _
               "CourseCode", MatchType.[Partial], txtCourseCode.Text.Trim()))
        End If

        If Not String.IsNullOrEmpty(txCodeKelas.Text.Trim()) Then
            criterias.opAnd(New Criteria(GetType(TrCourse), _
               "ClassCode", MatchType.[Partial], txCodeKelas.Text.Trim()))
        End If

        If chkRequireWorkDate.Checked Then
            criterias.opAnd(New Criteria(GetType(TrCourse), _
               "RequireWorkDate", MatchType.Exact, "1"))
            If Not String.IsNullOrEmpty(txtRequireWorkDate.Text) Then
                criterias.opAnd(New Criteria(GetType(TrCourse), "WorkDate", MatchType.Exact, Integer.Parse(txtRequireWorkDate.Text)))
            End If
        End If

        If Not String.IsNullOrEmpty(txtCourseName.Text.Trim()) Then
            criterias.opAnd(New Criteria(GetType(TrCourse), _
                "CourseName", MatchType.[Partial], txtCourseName.Text.Trim()))
        End If

        If Not ddlStatus.SelectedValue.Equals(helpers.NotSelected) Then
            criterias.opAnd(New Criteria(GetType(TrCourse), _
                "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If Not ddlCategory.SelectedValue.Equals(helpers.NotSelected) Then
            criterias.opAnd(New Criteria(GetType(TrCourse), _
                "Category", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        If ddlLevel.IsSelected Then
            criterias.opAnd(New Criteria(GetType(TrCourse), _
                "TrTraineeLevel.ID", MatchType.Exact, CType(ddlLevel.SelectedValue, Integer)))
        End If

        If Not ddlTipePembayaran.SelectedValue.Equals(helpers.NotSelected) Then
            criterias.opAnd(New Criteria(GetType(TrCourse), _
                "PaymentType", MatchType.Exact, CType(ddlTipePembayaran.SelectedValue, Short)))
        End If

        If Not ddlKategory.SelectedValue.Equals(helpers.NotSelected) Then
            criterias.opAnd(New Criteria(GetType(TrCourse), _
                "JobPositionCategory.ID", MatchType.Exact, CType(ddlKategory.SelectedValue, Integer)))
        End If

        If Not ddlKategory.SelectedValue.Equals(helpers.NotSelected) Then
            criterias.opAnd(New Criteria(GetType(TrCourse), "JobPositionCategory.ID", MatchType.Exact, ddlKategory.SelectedValue))
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
                criterias.opAnd(New Criteria(GetType(TrCourse), "JobPositionCategory.ID", MatchType.InSet, kategoryIn))
            End If
        End If


        Return criterias
    End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            Try
                dtgCourse.DataSource = New TrCourseFacade(User).RetrieveActiveList(CreateCriteriaComposite, indexPage + 1, dtgCourse.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
             CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
                dtgCourse.VirtualItemCount = totalRow
                dtgCourse.DataBind()
            Catch
                dtgCourse.DataSource = New List(Of TrCourse)
                dtgCourse.CurrentPageIndex = 0
                dtgCourse.DataBind()
                MessageBox.Show(SR.DataNotFound("Kategori Training"))
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub ClearData()
        helpers.ClearData(Table2, True)
        txtRequireWorkDate.Enabled = False
        txCodeKelas.Enabled = False
        chkCodeKelas.Checked = False
        chkRequireWorkDate.Checked = False
        txtBtsLulus.Text = 70
        If dtgCourse.Items.Count > 0 Then
            dtgCourse.SelectedIndex = -1
        End If
        btnSimpan.Enabled = helpers.IsEdit
        btnSimpan.Visible = helpers.IsEdit
        ViewState.Add("vsProcess", "Insert")
        hdnCategory.Value = String.Empty
    End Sub

    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewState.Clear()
        Response.Redirect("../Default.aspx")
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        If chkRequireWorkDate.Checked Then
            If txtRequireWorkDate.Text = String.Empty Then
                MessageBox.Show("Persyaratan lama kerja harus diisi")
                Exit Sub
            End If
        End If

        If chkCodeKelas.Checked Then
            If txCodeKelas.Text = String.Empty Then
                MessageBox.Show("Kode kelas harus diisi")
                Exit Sub
            End If
        End If

        If ddlCategory.SelectedValue = "-1" Then
            MessageBox.Show("Kategori Kursus harus dipilih")
            Exit Sub
        End If

        If Not ddlTipePembayaran.IsSelected And AreaId.Equals("2") Then
            MessageBox.Show("Tipe pembayaran harus dipilih")
            Exit Sub
        End If


        If Not IsUnhack() Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return
        End If
        Me.txtBtsLulus.ReadOnly = False
        Dim objTrCourse As TrCourse = New TrCourse
        Dim objTrCourseFacade As TrCourseFacade = New TrCourseFacade(User)
        Dim nResult = -1
        Me.txtCourseCode.Enabled = True
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If objTrCourseFacade.ValidateCode(Me.txtCourseCode.Text) = 0 Then
                objTrCourse.PassingScore = Me.txtBtsLulus.Text
                objTrCourse.CourseCode = Me.txtCourseCode.Text
                objTrCourse.CourseName = Me.txtCourseName.Text
                objTrCourse.ClassCode = Me.txCodeKelas.Text.Trim
                objTrCourse.Description = Me.txtDescCourse.Text
                objTrCourse.RequireWorkDate = Me.chkRequireWorkDate.Checked
                objTrCourse.Status = Me.ddlStatus.SelectedValue
                objTrCourse.Notes = Me.txtNotes.Text.Trim
                If objTrCourse.RequireWorkDate Then
                    objTrCourse.WorkDate = Me.txtRequireWorkDate.Text
                End If
                If Not ddlKategory.SelectedValue.Equals(helpers.NotSelected) Then
                    objTrCourse.JobPositionCategory = New JobPositionCategoryFacade(User).Retrieve(Integer.Parse(ddlKategory.SelectedValue))
                End If
                If ddlTipePembayaran.IsSelected Then
                    objTrCourse.PaymentType = Me.ddlTipePembayaran.SelectedValue
                End If

                If ddlLevel.IsSelected Then
                    objTrCourse.TrTraineeLevel = New TrTraineeLevelFacade(User).Retrieve(CInt(ddlLevel.SelectedValue))
                End If
                If ddlCategory.IsSelected Then
                    objTrCourse.Category = New TrCourseCategoryFacade(User).Retrieve(Integer.Parse(Me.ddlCategory.SelectedValue))
                    If objTrCourse.Category.Status = 0 Then
                        MessageBox.Show("Kategori kursus yang dipilih tidak aktif")
                        Exit Sub
                    End If
                End If

                nResult = New TrCourseFacade(User).Insert(objTrCourse)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    objTrCourse = New TrCourseFacade(User).Retrieve(objTrCourse.CourseCode)
                    Dim listEva As List(Of TrCourseEvaluation) = New List(Of TrCourseEvaluation)
                    listEva.Add(New TrCourseEvaluation(objTrCourse, "0", objTrCourse.CourseCode.ToUpper() & "-A00", "Initial Test"))
                    listEva.Add(New TrCourseEvaluation(objTrCourse, "0", objTrCourse.CourseCode.ToUpper() & "-A99", "Final Test"))
                    'listEva.Add(New TrCourseEvaluation(objTrCourse, "1", objTrCourse.CourseCode.ToUpper() & "-B00", "Initial Test"))
                    'listEva.Add(New TrCourseEvaluation(objTrCourse, "1", objTrCourse.CourseCode.ToUpper() & "-B99", "Final Test"))
                    Dim funcEva As TrCourseEvaluationFacade = New TrCourseEvaluationFacade(User)
                    For Each data As TrCourseEvaluation In listEva
                        funcEva.Insert(data)
                    Next
                    MessageBox.Show(SR.SaveSuccess)
                End If
            Else
                MessageBox.Show(SR.DataIsExist("Course"))
            End If
        Else
            UpdateGroup()
            MessageBox.Show(SR.SaveSuccess)
        End If

        helpers.ClearData(Table2, True)
        BindDropdownList()
        dtgCourse.SelectedIndex = -1
        dtgCourse.CurrentPageIndex = 0
        BindDataGrid(dtgCourse.CurrentPageIndex)
    End Sub

    Private Function IsExistGroup(ByVal strGroupCode As String) As Boolean
        Dim objTrCourseFacade As TrCourseFacade = New TrCourseFacade(User)
        If objTrCourseFacade.ValidateCode(strGroupCode) > 0 Then
            MessageBox.Show(SR.DataIsExist("Kode Course"))
            Return True
        End If
        Return False
    End Function

    Private Function InsertGroup() As Integer
        Dim objTrCourse As TrCourse = New TrCourse
        Dim nResult As Integer = -1
        If Not IsExistGroup(txtCourseCode.Text) Then
            objTrCourse.CourseCode = Me.txtCourseCode.Text
            objTrCourse.CourseName = Me.txtCourseName.Text
            objTrCourse.Description = Me.txtDescCourse.Text
            objTrCourse.Status = Me.ddlStatus.SelectedValue
            objTrCourse.Category = New TrCourseCategory(Integer.Parse(Me.ddlCategory.SelectedValue))
            objTrCourse.RequireWorkDate = Me.chkRequireWorkDate.Checked
            nResult = New TrCourseFacade(User).Insert(objTrCourse)
        Else
        End If
        Return nResult
    End Function

    Private Sub dtgCourse_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCourse.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Try
                Dim objTr As TrCourse = e.Item.DataItem
                Dim lblCatg As Label = CType(e.Item.FindControl("lblCategory"), Label)
                Dim lblWorkDate As Label = CType(e.Item.FindControl("lblWorkDate"), Label)
                Dim lblLevel As Label = CType(e.Item.FindControl("lblLevel"), Label)

                'If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCourse.CurrentPageIndex * dtgCourse.PageSize)
                'End If


                lblCatg.Text = objTr.JobPositionCategory.Description
                lblWorkDate.Text = IIf(objTr.WorkDate.Equals(0), String.Empty, objTr.WorkDate.ToString())

                If e.Item.ItemIndex <> -1 Then
                    e.Item.Cells(9).Text = objTr.Category.Description
                End If

                If Not e.Item.FindControl("btnHapus") Is Nothing Then
                    CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                End If


                'privilege
                If Not e.Item.FindControl("btnHapus") Is Nothing Then
                    CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = helpers.IsEdit
                End If
                If Not e.Item.FindControl("btnUbah") Is Nothing Then
                    CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = helpers.IsEdit
                End If

                lblLevel.Text = objTr.TrTraineeLevel.Description

            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub UpdateGroup()
        Dim objTrCourse As TrCourse = CType(sHCourse.GetSession("vsTrCourse"), TrCourse)
        objTrCourse.PassingScore = Me.txtBtsLulus.Text
        objTrCourse.CourseCode = Me.txtCourseCode.Text
        objTrCourse.CourseName = Me.txtCourseName.Text
        objTrCourse.ClassCode = Me.txCodeKelas.Text.Trim
        objTrCourse.Description = Me.txtDescCourse.Text
        objTrCourse.RequireWorkDate = Me.chkRequireWorkDate.Checked
        objTrCourse.Status = Me.ddlStatus.SelectedValue
        objTrCourse.Notes = Me.txtNotes.Text.Trim
        If objTrCourse.RequireWorkDate Then
            objTrCourse.WorkDate = Me.txtRequireWorkDate.Text
        End If
        If Not ddlKategory.SelectedValue.Equals(helpers.NotSelected) Then
            objTrCourse.JobPositionCategory = New JobPositionCategoryFacade(User).Retrieve(Integer.Parse(ddlKategory.SelectedValue))
        End If
        If ddlTipePembayaran.IsSelected Then
            objTrCourse.PaymentType = Me.ddlTipePembayaran.SelectedValue
        End If

        If ddlLevel.IsSelected Then
            objTrCourse.TrTraineeLevel = New TrTraineeLevelFacade(User).Retrieve(CInt(ddlLevel.SelectedValue))
        Else
            objTrCourse.TrTraineeLevel = Nothing
        End If
        If ddlCategory.IsSelected Then
            objTrCourse.Category = New TrCourseCategoryFacade(User).Retrieve(Integer.Parse(Me.ddlCategory.SelectedValue))
        End If

        Dim nResult = New TrCourseFacade(User).Update(objTrCourse)
    End Sub

    Private Sub dtgCourse_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgCourse.ItemCommand
        Try
            If (e.CommandName = "View") Then
                ViewState.Add("vsProcess", "View")
                ViewGroup(e.Item.Cells(0).Text, False)
                Table2.DisabledTextBoxWithPrefix("txt")
                
                Me.ddlStatus.Enabled = False
                Me.ddlCategory.Enabled = False
                Me.ddlKategory.Enabled = False
                Me.ddlLevel.Enabled = False
                Me.ddlTipePembayaran.Enabled = False
                Me.chkRequireWorkDate.Enabled = False
                dtgCourse.SelectedIndex = -1
            ElseIf e.CommandName = "Edit" Then
                ViewState.Add("vsProcess", "Edit")
                helpers.ClearData(Table2, True)
                ViewGroup(e.Item.Cells(0).Text, True)
                dtgCourse.SelectedIndex = e.Item.ItemIndex
                Me.txtCourseCode.Disabled()
                Me.ddlKategory.Enabled = False
            ElseIf e.CommandName = "Delete" Then
                DeleteCourse(e.Item.Cells(0).Text)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
    ByVal TrCourseID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "TrCourse", MatchType.Exact, TrCourseID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteCourse(ByVal nID As Integer)
        'Modifikasi u/ bugfixed
        Dim isRecordPrasyarat As Boolean = New HelperFacade(User, GetType(TrPreRequire)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(TrPreRequire), nID), CreateAggreateForCheckRecord(GetType(TrPreRequire)))
        Dim isRecordClass As Boolean = New HelperFacade(User, GetType(TrClass)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(TrClass), nID), CreateAggreateForCheckRecord(GetType(TrClass)))
        'Dim isRecordEvaluation As Boolean = New HelperFacade(User, GetType(TrCourseEvaluation)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(TrCourseEvaluation), nID), CreateAggreateForCheckRecord(GetType(TrCourseEvaluation)))

        If isRecordPrasyarat Or isRecordClass Then
            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objTrCourse As TrCourse = New TrCourseFacade(User).Retrieve(nID)
            objTrCourse.RowStatus = DBRowStatus.Deleted
            'Dim nResult = New TrCourseFacade(User).Update(objTrCourse)
            Dim facade As TrCourseFacade = New TrCourseFacade(User)
            Try
                Dim nResult = New TrCourseFacade(User).Update(objTrCourse)
                MessageBox.Show(SR.DeleteSucces)
                dtgCourse.CurrentPageIndex = 0
                BindDataGrid(dtgCourse.CurrentPageIndex)
                ClearData()
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try

        End If
    End Sub

    Private Sub ViewGroup(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objTrCourse As TrCourse = New TrCourseFacade(User).Retrieve(nID)
        sHCourse.SetSession("vsTrCourse", objTrCourse)
        Me.txtCourseCode.Text = objTrCourse.CourseCode
        Me.txtCourseName.Text = objTrCourse.CourseName
        Me.txtDescCourse.Text = objTrCourse.Description
        Me.txtBtsLulus.Text = objTrCourse.PassingScore
        Me.txtRequireWorkDate.Text = objTrCourse.WorkDate.ToString()
        Me.txCodeKelas.Text = objTrCourse.ClassCode.ToString()

        Me.txtNotes.Text = objTrCourse.Notes
        'default is true
        If IsNothing(objTrCourse.RequireWorkDate) Then
            Me.chkRequireWorkDate.Checked = True
        Else
            Me.chkRequireWorkDate.Checked = objTrCourse.RequireWorkDate
        End If
        'default is tidak aktif like in ui binding data
        If IsNothing(objTrCourse.Status) Or objTrCourse.Status = "" Then
            Me.ddlStatus.SelectedValue = "0"
        Else
            Me.ddlStatus.ClearSelection()
            Me.ddlStatus.SelectedValue = objTrCourse.Status
        End If

        If objTrCourse.JobPositionCategory IsNot Nothing Then
            Me.ddlKategory.ClearSelection()
            Me.ddlKategory.Items.FindByValue(objTrCourse.JobPositionCategory.ID.ToString).Selected = True
            BindDDLCategoryKursus(objTrCourse.JobPositionCategory.ID.ToString())
            BindDDLLevel(objTrCourse.JobPositionCategory.ID.ToString())
        End If
        If Not objTrCourse.PaymentType = 0 Then
            Me.ddlTipePembayaran.ClearSelection()
            Me.ddlTipePembayaran.Items.FindByValue(objTrCourse.PaymentType.ToString).Selected = True
        End If


        If Not objTrCourse.Category.ID.Equals(0) Then
            Me.ddlCategory.ClearSelection()
            ' Me.ddlCategory.SelectedValue = objTrCourse.Category.ID
            ddlCategory.Items.FindByValue(objTrCourse.Category.ID).Selected = True
        End If

        If objTrCourse.TrTraineeLevel IsNot Nothing Then
            If Not objTrCourse.TrTraineeLevel.ID.Equals(0) Then
                Me.ddlLevel.ClearSelection()
                Me.ddlLevel.SelectedValue = objTrCourse.TrTraineeLevel.ID
            End If
        End If

        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgCourse_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCourse.SortCommand
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

        dtgCourse.SelectedIndex = -1
        dtgCourse.CurrentPageIndex = 0
        ReadCritriaSearch()
        BindDataGrid(dtgCourse.CurrentPageIndex)
        'ClearData()
    End Sub

    Private Sub dtgCourse_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCourse.PageIndexChanged
        dtgCourse.SelectedIndex = -1
        dtgCourse.CurrentPageIndex = e.NewPageIndex
        ReadCritriaSearch()
        BindDataGrid(dtgCourse.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub findClassCategory()
        dtgCourse.CurrentPageIndex = 0
        SaveCriteriaSearch()
        BindDataGrid(0)
    End Sub

    Private Function IsUnhack() As Boolean
        If txtCourseCode.Text.IndexOf("<") >= 0 Or txtCourseCode.Text.IndexOf(">") >= 0 Or txtCourseCode.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtCourseName.Text.IndexOf("<") >= 0 Or txtCourseName.Text.IndexOf(">") >= 0 Or txtCourseName.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtDescCourse.Text.IndexOf("<") >= 0 Or txtDescCourse.Text.IndexOf(">") >= 0 Or txtDescCourse.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtNotes.Text.IndexOf("<") >= 0 Or txtNotes.Text.IndexOf(">") >= 0 Or txtNotes.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        Return True
    End Function

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        findClassCategory()
    End Sub

    Protected Sub chkRequireWorkDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkRequireWorkDate.CheckedChanged
        If chkRequireWorkDate.Checked Then
            txtRequireWorkDate.Text = 0
            txtRequireWorkDate.Enabled = True
            txtRequireWorkDate.Focus()
        Else
            txtRequireWorkDate.Text = String.Empty
            txtRequireWorkDate.Enabled = False
        End If

    End Sub

    Public Sub BindDDLCategoryKursus(Optional ByVal category As String = "")
        If String.IsNullOrEmpty(category) Then
            ddlCategory.ClearSelection()
            ddlCategory.Items.Clear()
            ddlCategory.Items.Add(New ListItem("Semua Kategori", "-1"))
            ddlCategory.Items.FindByValue("-1").Selected = True
            Exit Sub
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrCourseCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrCourseCategory), "JobPositionCategory.ID", MatchType.Exact, category))
        Dim dataCtg As ArrayList = New TrCourseCategoryFacade(User).Retrieve(criterias)
        ddlCategory.ClearSelection()
        ddlCategory.Items.Clear()
        ddlCategory.Items.Add(New ListItem("Semua Kategori", "-1"))

        For Each data As TrCourseCategory In dataCtg
            Dim item As New ListItem(data.Description, data.ID)

            If data.Status = 0 Then
                item.Attributes.CssStyle.Add("background-color", "Gray")
            End If

            ddlCategory.Items.Add(item)
        Next
        ddlCategory.Items.FindByValue("-1").Selected = True
    End Sub

    Public Sub BindDDLLevel(Optional ByVal category As String = "")
        If String.IsNullOrEmpty(category) Then
            ddlLevel.ClearSelection()
            ddlLevel.Items.Clear()
            ddlLevel.Items.Add(New ListItem("Silahkan Pilih", "-1"))
            ddlLevel.Items.FindByValue("-1").Selected = True
            Exit Sub
        End If
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrTraineeLevel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrTraineeLevel), "JobPositionCategory.ID", MatchType.Exact, category))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(TrTraineeLevel), "Sequence", Sort.SortDirection.ASC))

        Dim dataCtg As ArrayList = New TrTraineeLevelFacade(User).Retrieve(criterias, sortColl)
        ddlLevel.ClearSelection()
        ddlLevel.Items.Clear()
        ddlLevel.Items.Add(New ListItem("Silahkan Pilih", "-1"))

        For Each data As TrTraineeLevel In dataCtg
            ddlLevel.Items.Add(New ListItem(data.Description, data.ID))
        Next
        ddlLevel.Items.FindByValue("-1").Selected = True
    End Sub

    Protected Sub ddlKategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKategory.SelectedIndexChanged
        If Not ddlKategory.SelectedValue.Equals(helpers.NotSelected) Then
            BindDDLCategoryKursus(ddlKategory.SelectedValue)
            BindDDLLevel(ddlKategory.SelectedValue)
            Exit Sub
        End If
        BindDDLCategoryKursus()
        BindDDLLevel()
    End Sub

    Private Sub chkCodeKelas_CheckedChanged(sender As Object, e As EventArgs) Handles chkCodeKelas.CheckedChanged
        If chkCodeKelas.Checked Then
            txCodeKelas.Enabled = True
        Else
            txCodeKelas.Text = String.Empty
            txCodeKelas.Enabled = False
        End If
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        hdnCategory.Value = ddlCategory.SelectedValue
    End Sub
End Class

