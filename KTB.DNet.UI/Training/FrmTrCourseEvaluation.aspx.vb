Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security


Public Class FrmTrCourseEvaluation
    Inherits System.Web.UI.Page
    Private m_bFormPrivilege As Boolean = False

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgCourseEvaluation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCourseEvaluation As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlCourse As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rbTest As System.Web.UI.WebControls.RadioButton
    Protected WithEvents rbInitialTest As System.Web.UI.WebControls.RadioButton
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rbFinalTest As System.Web.UI.WebControls.RadioButton
    Protected WithEvents ChkTampil As System.Web.UI.WebControls.CheckBox
    Protected WithEvents lblEvalCode As System.Web.UI.WebControls.Label
    Protected WithEvents RegularExpressionValidator2 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents ddlJenisTest As System.Web.UI.WebControls.DropDownList
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Private _sessHelper As SessionHelper = New SessionHelper

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDdlType()
            BindDdlCourse()
            BindDatagrid(0)
        End If
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.TrainingUpdateEvaluasi_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.TrainingViewEvaluasi_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=TRAINING - Jenis Evaluasi")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "EvaluationCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ChkTampil.Checked = True
    End Sub
    Private Sub BindDdlCourse()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arrylst As ArrayList = New TrCourseFacade(User).RetrieveActiveList(criterias, "CourseCode", Sort.SortDirection.ASC)
        For i As Integer = 0 To arrylst.Count - 1
            ddlCourse.Items.Insert(i, New ListItem(arrylst(i).CourseCode + "  :  " + arrylst(i).CourseName, arrylst(i).ID))
        Next
        ddlCourse.Items.Insert(0, New ListItem("", ""))
    End Sub
    Private Sub BindDdlType()
        Dim al As ArrayList = New EnumTrEvaluationType().Retrieve
        For i As Integer = 0 To al.Count - 1
            ddlType.Items.Insert(i, New ListItem(al(i).NameType, al(i).ValueType))
        Next
        ddlType.Items.Insert(0, New ListItem("", ""))
    End Sub

    Private Sub ClearData()
        lblEvalCode.Text = String.Empty
        'ddlCourse.SelectedValue = ""
        txtCourseEvaluation.Text = String.Empty
        ddlType.SelectedValue = ""
        txtDescription.Text = String.Empty


        txtCourseEvaluation.ReadOnly = False
        txtDescription.ReadOnly = False
        ddlCourse.Enabled = True
        ddlType.Enabled = True
        ddlJenisTest.Enabled = True
        ddlJenisTest.Items.Clear()

        dtgCourseEvaluation.SelectedIndex = -1

        btnSimpan.Enabled = True

        ViewState.Add("vsProcess", "Insert")

    End Sub

    Private Sub ClearDataAfterSave()
        lblEvalCode.Text = String.Empty

        txtCourseEvaluation.Text = String.Empty
        'ddlType.SelectedValue = ""
        txtDescription.Text = String.Empty
        txtCourseEvaluation.ReadOnly = False
        txtDescription.ReadOnly = False
        ddlCourse.Enabled = True
        ddlType.Enabled = True
        ddlJenisTest.Enabled = True

        ddlJenisTest.SelectedValue = ""
        dtgCourseEvaluation.SelectedIndex = -1
        btnSimpan.Enabled = True

        ViewState.Add("vsProcess", "Insert")

    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If ChkTampil.Checked Then
                If ddlCourse.SelectedValue <> "" Then
                    criterias.opAnd(New Criteria(GetType(TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, ddlCourse.SelectedValue))
                End If
            End If
            dtgCourseEvaluation.DataSource = New TrCourseEvaluationFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgCourseEvaluation.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgCourseEvaluation.VirtualItemCount = totalRow
            dtgCourseEvaluation.DataBind()
        End If
    End Sub

    Private Function IsUnhack() As Boolean
        If txtCourseEvaluation.Text.IndexOf("<") >= 0 Or txtCourseEvaluation.Text.IndexOf(">") >= 0 Or txtCourseEvaluation.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtDescription.Text.IndexOf("<") >= 0 Or txtDescription.Text.IndexOf(">") >= 0 Or txtDescription.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        Return True

    End Function
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        If Not IsUnhack() Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return
        End If

        Dim objTrCourseEval As TrCourseEvaluation = New TrCourseEvaluation
        Dim objTrCourseEvalFacade As TrCourseEvaluationFacade = New TrCourseEvaluationFacade(User)
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not lblEvalCode.Text = String.Empty Then
                If objTrCourseEvalFacade.ValidateCode(lblEvalCode.Text) <= 0 Then
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(TrCourse), "ID", MatchType.Exact, ddlCourse.SelectedValue))
                    Dim CourseColl As ArrayList = New TrCourseFacade(User).Retrieve(criterias)
                    If CourseColl.Count > 0 Then
                        objTrCourseEval.TrCourse = CourseColl(0)
                    End If
                    objTrCourseEval.EvaluationCode = lblEvalCode.Text
                    objTrCourseEval.Name = txtCourseEvaluation.Text
                    objTrCourseEval.Type = ddlType.SelectedValue
                    objTrCourseEval.Description = txtDescription.Text
                    nResult = New TrCourseEvaluationFacade(User).Insert(objTrCourseEval)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearDataAfterSave()
                    End If
                Else
                    If ddlJenisTest.SelectedValue = "" Then
                        MessageBox.Show(SR.DataIsExist("Jenis Test"))
                    End If
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Jenis Test"))
            End If
        Else
            nResult = UpdateTrCourseEval()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearDataAfterSave()
            End If
        End If
        dtgCourseEvaluation.CurrentPageIndex = 0
        BindDatagrid(dtgCourseEvaluation.CurrentPageIndex)

    End Sub
    Private Function UpdateTrCourseEval() As Integer
        Dim nResult As Integer = -1
        Dim objTrCourseEvalFacade As TrCourseEvaluationFacade = New TrCourseEvaluationFacade(User)

        Dim objTrCourseEval As TrCourseEvaluation = CType(Session.Item("vsTrCourseEval"), TrCourseEvaluation)
        objTrCourseEval.TrCourse = New TrCourseFacade(User).Retrieve(CType(ddlCourse.SelectedValue, Integer))
        objTrCourseEval.Name = txtCourseEvaluation.Text
        objTrCourseEval.Type = ddlType.SelectedValue
        objTrCourseEval.Description = txtDescription.Text
        nResult = New TrCourseEvaluationFacade(User).Update(objTrCourseEval)

        Return nResult
    End Function

    Private Sub ViewCourseEval(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objTrCourseEval As TrCourseEvaluation = New TrCourseEvaluationFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsTrCourseEval", objTrCourseEval)
        If IsNothing(objTrCourseEval.TrCourse.ID) Then
            ddlCourse.SelectedValue = ""
        Else
            ddlCourse.SelectedValue = CType(objTrCourseEval.TrCourse.ID, String)
        End If

        lblEvalCode.Text = objTrCourseEval.EvaluationCode

        txtCourseEvaluation.Text = objTrCourseEval.Name
        ddlType.SelectedValue = objTrCourseEval.Type

        '        ddlJenisTest.Items.Clear()
        '       ddlJenisTest.Items.Insert(0, New ListItem(StrJenisTestName(objTrCourseEval), Right(objTrCourseEval.EvaluationCode, 3)))
        '      ddlJenisTest.Items.Insert(0, New ListItem("", ""))
        Dim sender As System.Object
        Dim e As System.Web.UI.WebControls.DataGridItemEventArgs
        ddlType_SelectedIndexChanged(sender, e)
        ddlJenisTest.SelectedValue = Right(objTrCourseEval.EvaluationCode, 3)
        ddlJenisTest.Enabled = False
        txtDescription.Text = objTrCourseEval.Description
        Me.btnSimpan.Enabled = EditStatus
    End Sub
    Private Sub dtgCourseEvaluation_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCourseEvaluation.ItemDataBound
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            BoundRowItems(e)
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCourseEvaluation.CurrentPageIndex * dtgCourseEvaluation.PageSize)
        End If
        'privilege
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
        End If
        If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
        End If
    End Sub

    Private Sub BoundRowItems(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim objTrCourseEval As TrCourseEvaluation = CType(CType(dtgCourseEvaluation.DataSource, ArrayList)(e.Item.ItemIndex), TrCourseEvaluation)
        If Not IsNothing(objTrCourseEval) Then
            Dim objTrCourse As TrCourse = New TrCourseFacade(User).Retrieve(objTrCourseEval.TrCourse.ID)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblCode"), Label).Text = objTrCourseEval.EvaluationCode
                CType(e.Item.FindControl("lblCategory"), Label).Text = objTrCourse.CourseCode & " - " & objTrCourse.CourseName
                Dim lblJenisTest As Label = CType(e.Item.FindControl("lblJenisTest"), Label)
                lblJenisTest.Text = StrJenisTestName(objTrCourseEval)
            End If
        End If
    End Sub

    Private Function JenisTest(ByVal str1 As String, ByVal str2 As String) As String
        Select Case str1
            Case "A"
                Return "Test-" + str2
            Case "B"
                Return "Sikap-" + str2
            Case "C"
                Return "Prestasi-" + str2
            Case Else
                Return ""
        End Select
    End Function
    
    Private Function StrJenisTestName(ByVal objTrCourseEval As TrCourseEvaluation) As String
        Select Case Right(objTrCourseEval.EvaluationCode, 2)
            Case "00"
                Return "Initial Test"
            Case "99"
                Return "Final Test"
            Case "01"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "1")
            Case "02"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "2")
            Case "03"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "3")
            Case "04"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "4")
            Case "05"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "5")
            Case "06"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "6")
            Case "07"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "7")
            Case "08"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "8")
            Case "09"
                Return JenisTest(Left(Right(objTrCourseEval.EvaluationCode, 3), 1), "9")

        End Select
    End Function


    Private Sub dtgCourseEvaluation_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgCourseEvaluation.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")

            ddlCourse.Enabled = False
            txtCourseEvaluation.ReadOnly = True
            ddlType.Enabled = False
            txtDescription.ReadOnly = True
            ViewCourseEval(e.Item.Cells(0).Text, False)
          
        ElseIf e.CommandName = "Edit" Then

            ViewCourseEval(e.Item.Cells(0).Text, True)
            dtgCourseEvaluation.SelectedIndex = e.Item.ItemIndex

            ddlCourse.Enabled = False
            ViewState.Add("vsProcess", "Edit")
            txtCourseEvaluation.ReadOnly = False
            ddlType.Enabled = False
            txtDescription.ReadOnly = False

        ElseIf e.CommandName = "Delete" Then
            Try
                Dim nResult = DeleteTrCourseEval(e.Item.Cells(0).Text)
                If nResult = 2 Then
                    MessageBox.Show(SR.CannotDelete)
                ElseIf nResult = -1 Then
                    MessageBox.Show(SR.DeleteFail)
                Else
                    MessageBox.Show(SR.DeleteSucces)
                End If
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearDataAfterSave()
            BindDatagrid(dtgCourseEvaluation.CurrentPageIndex)

        End If
    End Sub
    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
           ByVal TrCourseEvaluationID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "TrCourseEvaluation", MatchType.Exact, TrCourseEvaluationID))
        Return criterias
    End Function
    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function DeleteTrCourseEval(ByVal nID As Integer) As Integer
        Dim nResult As Integer = -1
        If New HelperFacade(User, GetType(TrCertificateLine)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(TrCertificateLine), nID), _
            CreateAggreateForCheckRecord(GetType(TrCertificateLine))) Then
            nResult = 2
        Else
            Dim objTrCourseEval As TrCourseEvaluation = New TrCourseEvaluationFacade(User).Retrieve(nID)
            nResult = New TrCourseEvaluationFacade(User).DeleteFromDB(objTrCourseEval)
            dtgCourseEvaluation.CurrentPageIndex = 0
            BindDatagrid(dtgCourseEvaluation.CurrentPageIndex)
        End If
        Return nResult
    End Function

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgCourseEvaluation_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCourseEvaluation.PageIndexChanged
        dtgCourseEvaluation.SelectedIndex = -1
        dtgCourseEvaluation.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgCourseEvaluation.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgCourseEvaluation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCourseEvaluation.SortCommand
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

        dtgCourseEvaluation.SelectedIndex = -1
        dtgCourseEvaluation.CurrentPageIndex = 0
        BindDatagrid(dtgCourseEvaluation.CurrentPageIndex)

    End Sub

    Private Sub ddlCourse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCourse.SelectedIndexChanged
        ddlType.SelectedValue = ""
        lblEvalCode.Text = ""
        ddlType_SelectedIndexChanged(sender, e)
        dtgCourseEvaluation.CurrentPageIndex = 0
        BindDatagrid(0)
    End Sub

    Private Sub RefreshAfterMassage()
        lblEvalCode.Text = ""
        'ddlType.SelectedValue = ""
        'ddlCourse.SelectedValue = ""
        ddlJenisTest.SelectedValue = ""

    End Sub

    Private Sub rbInitialTest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbInitialTest.CheckedChanged
        If ddlCourse.SelectedValue <> "" Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrCourse), "ID", MatchType.Exact, CType(ddlCourse.SelectedValue, Integer)))
            Dim CourseColl As ArrayList = New TrCourseFacade(User).Retrieve(criterias)
            Dim objTrCourse As TrCourse

            If CourseColl.Count > 0 Then
                objTrCourse = CourseColl(0)
                lblEvalCode.Text = objTrCourse.CourseCode
                If ddlType.SelectedValue = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) Then
                    Dim critCourseEval As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critCourseEval.opAnd(New Criteria(GetType(TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, ddlCourse.SelectedValue))
                    critCourseEval.opAnd(New Criteria(GetType(TrCourseEvaluation), "Type", MatchType.Exact, ddlType.SelectedValue))
                    Dim CourseEvalColl As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(critCourseEval)

                    If CourseEvalColl.Count > 0 Then
                        Dim bSearch As Boolean = False
                        For Each objCourseEval As TrCourseEvaluation In CourseEvalColl
                            If Right(objCourseEval.EvaluationCode, 2) = "00" Then
                                bSearch = True
                                Exit For
                            End If
                        Next
                        If bSearch Then
                            RefreshAfterMassage()
                            MessageBox.Show("Jenis Test Initial tersebut sudah ada ")
                        Else
                            lblEvalCode.Text = lblEvalCode.Text & "-" & "T00"
                        End If
                    Else
                        lblEvalCode.Text = lblEvalCode.Text & "-" & "T00"
                    End If
                Else
                    lblEvalCode.Text = ""
                End If

            Else
                lblEvalCode.Text = ""
            End If
        End If
    End Sub

    Private Sub rbFinalTest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbFinalTest.CheckedChanged
        If ddlCourse.SelectedValue <> "" Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrCourse), "ID", MatchType.Exact, CType(ddlCourse.SelectedValue, Integer)))
            Dim CourseColl As ArrayList = New TrCourseFacade(User).Retrieve(criterias)
            Dim objTrCourse As TrCourse

            If CourseColl.Count > 0 Then
                objTrCourse = CourseColl(0)
                lblEvalCode.Text = objTrCourse.CourseCode
                If ddlType.SelectedValue = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) Then
                    Dim critCourseEval As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critCourseEval.opAnd(New Criteria(GetType(TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, ddlCourse.SelectedValue))
                    critCourseEval.opAnd(New Criteria(GetType(TrCourseEvaluation), "Type", MatchType.Exact, ddlType.SelectedValue))
                    Dim CourseEvalColl As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(critCourseEval)

                    If CourseEvalColl.Count > 0 Then
                        Dim bSearch As Boolean = False
                        For Each objCourseEval As TrCourseEvaluation In CourseEvalColl
                            If Right(objCourseEval.EvaluationCode, 2) = "99" Then
                                bSearch = True
                                Exit For
                            End If
                        Next
                        If bSearch Then
                            RefreshAfterMassage()
                            MessageBox.Show("Jenis Test Final tersebut sudah ada ")
                        Else
                            lblEvalCode.Text = lblEvalCode.Text & "-" & "T99"
                        End If
                    Else
                        lblEvalCode.Text = lblEvalCode.Text & "-" & "T99"
                    End If
                Else
                    lblEvalCode.Text = ""
                End If

            Else
                lblEvalCode.Text = ""
            End If
        End If

    End Sub

    Private Sub rbTest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTest.CheckedChanged
        If ddlCourse.SelectedValue <> "" Then
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(TrCourse), "ID", MatchType.Exact, CType(ddlCourse.SelectedValue, Integer)))
            Dim CourseColl As ArrayList = New TrCourseFacade(User).Retrieve(crit)
            Dim objTrCourse As TrCourse
            If CourseColl.Count > 0 Then
                objTrCourse = CourseColl(0)
                lblEvalCode.Text = objTrCourse.CourseCode
                WriteEvaluationCodeLabel(ddlType.SelectedValue)
            Else
                lblEvalCode.Text = ""
            End If
        End If
    End Sub

    Private Sub ddlType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlType.SelectedIndexChanged
        If ddlCourse.SelectedValue = "" Then
            MessageBox.Show("Kategori Pelatihan belum dipilih !")
        Else
            ddlJenisTest.Items.Clear()
            If ddlType.SelectedValue = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) Then
                ddlJenisTest.Items.Insert(0, New ListItem("Initial Test", "A00"))
                For i As Integer = 1 To 9
                    Dim str As String = "Test" + i.ToString
                    Dim val As String = "A0" + i.ToString
                    ddlJenisTest.Items.Insert(i, New ListItem(str, val))
                Next
                ddlJenisTest.Items.Insert(10, New ListItem("Final Test", "A99"))

            ElseIf ddlType.SelectedValue = CType(EnumTrEvaluationType.TrEvaluationType.Sikap, String) Then
                For i As Integer = 1 To 10
                    Dim str As String = "Sikap-" + i.ToString
                    Dim val As String = "B0" + i.ToString
                    ddlJenisTest.Items.Insert(i - 1, New ListItem(str, val))
                Next

            ElseIf ddlType.SelectedValue = CType(EnumTrEvaluationType.TrEvaluationType.Prestasi, String) Then
                For i As Integer = 1 To 9
                    Dim str As String = "Prestasi-" + i.ToString
                    Dim val As String = "C0" + i.ToString
                    ddlJenisTest.Items.Insert(i - 1, New ListItem(str, val))
                Next

            End If
            ddlJenisTest.Items.Insert(0, New ListItem("", ""))
        End If
    End Sub

    Private Sub WriteEvaluationCodeLabel(ByVal sType As String)
        Dim arrTestNo As ArrayList = New ArrayList
        Dim strType As String
        Select Case sType
            Case "0"
                strType = "A"
            Case "1"
                strType = "B"
            Case "2"
                strType = "C"
        End Select

        Dim bFound01 As Boolean = False
        Dim bFound02 As Boolean = False
        Dim bFound03 As Boolean = False
        Dim bFound04 As Boolean = False
        Dim bFound05 As Boolean = False
        Dim bFound06 As Boolean = False
        Dim bFound07 As Boolean = False
        Dim bFound08 As Boolean = False
        Dim bFound09 As Boolean = False
        Dim bFound10 As Boolean = False
        Dim bFound11 As Boolean = False

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, ddlCourse.SelectedValue))
        criterias.opAnd(New Criteria(GetType(TrCourseEvaluation), "Type", MatchType.Exact, ddlType.SelectedValue))
        Dim CourseEvalColl As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(criterias)

        If CourseEvalColl.Count > 0 Then
            For Each objTrCourseEval As TrCourseEvaluation In CourseEvalColl
                If Not (Right(objTrCourseEval.EvaluationCode, 2) = "00") And Not (Right(objTrCourseEval.EvaluationCode, 2) = "99") Then
                    If Right(objTrCourseEval.EvaluationCode, 2) = "01" Then
                        bFound01 = True
                    ElseIf Right(objTrCourseEval.EvaluationCode, 2) = "02" Then
                        bFound02 = True
                    ElseIf Right(objTrCourseEval.EvaluationCode, 2) = "03" Then
                        bFound03 = True
                    ElseIf Right(objTrCourseEval.EvaluationCode, 2) = "04" Then
                        bFound04 = True
                    ElseIf Right(objTrCourseEval.EvaluationCode, 2) = "05" Then
                        bFound05 = True
                    ElseIf Right(objTrCourseEval.EvaluationCode, 2) = "06" Then
                        bFound06 = True
                    ElseIf Right(objTrCourseEval.EvaluationCode, 2) = "07" Then
                        bFound07 = True
                    ElseIf Right(objTrCourseEval.EvaluationCode, 2) = "08" Then
                        bFound08 = True
                    ElseIf Right(objTrCourseEval.EvaluationCode, 2) = "09" Then
                        bFound09 = True
                    ElseIf Right(objTrCourseEval.EvaluationCode, 2) = "10" Then
                        bFound10 = True
                    ElseIf Right(objTrCourseEval.EvaluationCode, 2) = "11" Then
                        bFound11 = True
                    End If
                End If
            Next

            If Not bFound01 Then
                lblEvalCode.Text = lblEvalCode.Text & "-" & strType & "01"
            ElseIf Not bFound02 Then
                lblEvalCode.Text = lblEvalCode.Text & "-" & strType & "02"
            ElseIf Not bFound03 Then
                lblEvalCode.Text = lblEvalCode.Text & "-" & strType & "03"
            ElseIf Not bFound04 Then
                lblEvalCode.Text = lblEvalCode.Text & "-" & strType & "04"
            ElseIf Not bFound05 Then
                lblEvalCode.Text = lblEvalCode.Text & "-" & strType & "05"
            ElseIf Not bFound06 Then
                lblEvalCode.Text = lblEvalCode.Text & "-" & strType & "06"
            ElseIf Not bFound07 Then
                lblEvalCode.Text = lblEvalCode.Text & "-" & strType & "07"
                'ElseIf Not bFound08 Then
                '    lblEvalCode.Text = lblEvalCode.Text & "-" & strType & "08"
                'ElseIf Not bFound09 Then
                '    lblEvalCode.Text = lblEvalCode.Text & "-" & strType & "09"
                'ElseIf Not bFound10 Then
                '    lblEvalCode.Text = lblEvalCode.Text & "-" & strType & "10"
                'ElseIf Not bFound11 Then
                RefreshAfterMassage()
                MessageBox.Show("Jumlah test sejenis sudah maksimum (7)")
            End If
        Else
            If CourseEvalColl.Count = 0 Then
                lblEvalCode.Text = lblEvalCode.Text & "-" & strType & "01"
            End If
        End If



    End Sub

    Private Sub ddlJenisTest_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlJenisTest.SelectedIndexChanged
        If ddlType.SelectedValue = "" Then
            MessageBox.Show("Jenis Test belum dipilih")
        Else
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrCourse), "ID", MatchType.Exact, CType(ddlCourse.SelectedValue, Integer)))
            Dim CourseColl As ArrayList = New TrCourseFacade(User).Retrieve(criterias)
            If CourseColl.Count > 0 Then
                Dim critCourseEval As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critCourseEval.opAnd(New Criteria(GetType(TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, ddlCourse.SelectedValue))
                critCourseEval.opAnd(New Criteria(GetType(TrCourseEvaluation), "EvaluationCode", MatchType.Exact, CourseColl(0).CourseCode + "-" + ddlJenisTest.SelectedValue))
                Dim CourseEvalColl As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(critCourseEval)
                If CourseEvalColl.Count > 0 Then
                    RefreshAfterMassage()
                    MessageBox.Show("Jenis test tersebut sudah ada ")
                    lblEvalCode.Text = ""
                Else
                    lblEvalCode.Text = CourseColl(0).CourseCode + "-" + ddlJenisTest.SelectedValue
                End If
            End If
        End If

    End Sub

    Private Sub ChkTampil_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTampil.CheckedChanged
        ddlType_SelectedIndexChanged(sender, e)
        dtgCourseEvaluation.CurrentPageIndex = 0
        BindDatagrid(0)
    End Sub
End Class
