#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
#End Region

Public Class FrmTrPreRequire
    Inherits System.Web.UI.Page
    Private m_bFormPrivilege As Boolean = False
    Protected WithEvents txtPreRequireCodeNoPass As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPreRequireCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPreRequireCodeNoPass As System.Web.UI.WebControls.Label
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Private _sessHelper As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtDesc As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgPreRequire As System.Web.UI.WebControls.DataGrid
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents Requiredfieldvalidator8 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblPopUpCourse1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtCourseCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblPopUpCourse2 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private sHPreRequire As SessionHelper = New SessionHelper
    Private _trCourse As ArrayList
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "TrCourse.CourseCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrPreRequire As ArrayList = _sessHelper.GetSession("PreRequireCollection")

        If Not IsNothing(arrPreRequire) Then
            If dtgPreRequire.CurrentPageIndex = 0 Then
                SortListControl(arrPreRequire, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Integer))
            End If

            Dim arlCurrentColl As ArrayList = GetCurrentColl(arrPreRequire, indexPage)

            dtgPreRequire.DataSource = arlCurrentColl
            dtgPreRequire.DataBind()
        Else
            'RetriveDataAndSaveToCache()
        End If
    End Sub

    Private Sub SortListControl(ByRef ListControl As ArrayList, ByVal SortColumn As String, ByVal SortDirection As Integer)
        Dim IsAsc As Boolean = True
        If SortDirection = Sort.SortDirection.ASC Then
            IsAsc = True
        ElseIf SortDirection = Sort.SortDirection.DESC Then
            IsAsc = False
        End If
        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        ListControl.Sort(objListComparer)
    End Sub

    Private Function GetCurrentColl(ByVal PreRequireColl As ArrayList, ByVal CurrentPage As Integer) As ArrayList
        dtgPreRequire.VirtualItemCount = PreRequireColl.Count
        Dim arlCurrentColl As ArrayList = New ArrayList
        Dim nStart As Integer = (dtgPreRequire.PageSize * CurrentPage)
        Dim i As Integer
        For i = 0 To (dtgPreRequire.VirtualItemCount - (dtgPreRequire.PageSize * CurrentPage) - 1)
            arlCurrentColl.Add(PreRequireColl(nStart + i))
        Next
        Return arlCurrentColl
    End Function
    Private Sub ClearData()
        Me.txtDesc.ReadOnly = False
        Me.txtCourseCode.ReadOnly = False
        Me.lblPopUpCourse1.Enabled = True
        Me.txtPreRequireCode.ReadOnly = False
        Me.lblPopUpCourse2.Enabled = True
        Me.txtCourseCode.Text = String.Empty
        Me.txtPreRequireCode.Text = String.Empty
        Me.txtPreRequireCodeNoPass.Text = String.Empty
        Me.txtDesc.Text = String.Empty
        If dtgPreRequire.Items.Count > 0 Then
            dtgPreRequire.SelectedIndex = -1
        End If
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Function GetCourseInfo(ByVal CourseCode As String) As TrCourse
        Dim objCourseFacade As TrCourseFacade = New TrCourseFacade(User)
        Return objCourseFacade.Retrieve(CourseCode)
    End Function

    Private Function IsExistGroup(ByVal _courseid As Integer, ByVal _prerequireid As Integer) As Boolean
        Dim objPreRequireFacade As TrPreRequireFacade = New TrPreRequireFacade(User)
        If objPreRequireFacade.ValidateCode(_courseid, _prerequireid) > 0 Then
            Return True
        End If
        Return False
    End Function

    Private Function IsExistGroup(ByVal _courseid As Integer, ByVal _prerequireid As Integer, ByVal _reqType As Short) As Boolean
        Dim objPreRequireFacade As TrPreRequireFacade = New TrPreRequireFacade(User)
        If objPreRequireFacade.ValidateCode(_courseid, _prerequireid, _reqType) > 0 Then
            Return True
        End If
        Return False
    End Function

    Private Sub InsertPreRequire()
        Dim objTrPreRequire As TrPreRequire = New TrPreRequire
        Dim objCourseId As TrCourse
        Dim objPreReqCourseId As TrCourse
        Dim iLulus As Integer

        objCourseId = GetCourseInfo(txtCourseCode.Text.Trim())
        'Insert data syarat lulus
        Dim strMsgExist As String = String.Empty
        Dim strMsgFailed As String = String.Empty
        If txtPreRequireCode.Text.Trim() <> "" Then
            Dim arrLulus As String() = txtPreRequireCode.Text.Trim().Split(";".ToCharArray())
            For Each strlulus As String In arrLulus
                objPreReqCourseId = GetCourseInfo(strlulus)
                If objCourseId.ID > 0 And objPreReqCourseId.ID > 0 Then
                    If Not IsExistGroup(objCourseId.ID, objPreReqCourseId.ID, EnumCourseRequireType.RequireType.SyaratLulus) Then
                        iLulus = 0
                        objTrPreRequire.TrCourse = objCourseId
                        objTrPreRequire.PreRequireCourseID = objPreReqCourseId.ID
                        objTrPreRequire.RequireType = EnumCourseRequireType.RequireType.SyaratLulus
                        objTrPreRequire.Description = Me.txtDesc.Text.Trim()
                        iLulus = New TrPreRequireFacade(User).Insert(objTrPreRequire)
                    Else
                        strMsgExist += strlulus + ", "
                    End If
                    If iLulus = -1 Then
                        strMsgFailed += strlulus + ", "
                    End If
                End If
            Next
        End If

        'Insert data syarat blm lulus
        If txtPreRequireCodeNoPass.Text.Trim() <> "" Then
            Dim arrBlmLulus As String() = txtPreRequireCodeNoPass.Text.Trim().Split(";".ToCharArray())
            For Each strblmlulus As String In arrBlmLulus
                objPreReqCourseId = GetCourseInfo(strblmlulus)
                If objCourseId.ID > 0 And objPreReqCourseId.ID > 0 Then
                    If Not IsExistGroup(objCourseId.ID, objPreReqCourseId.ID, EnumCourseRequireType.RequireType.SyaratBelumLulus) Then
                        iLulus = 0
                        objTrPreRequire.TrCourse = objCourseId
                        objTrPreRequire.PreRequireCourseID = objPreReqCourseId.ID
                        objTrPreRequire.RequireType = EnumCourseRequireType.RequireType.SyaratBelumLulus
                        objTrPreRequire.Description = Me.txtDesc.Text.Trim()
                        iLulus = New TrPreRequireFacade(User).Insert(objTrPreRequire)
                    Else
                        strMsgExist += strblmlulus + ", "
                    End If
                    If iLulus = -1 Then
                        strMsgFailed += strblmlulus + ", "
                    End If
                End If
            Next
        End If


        Dim strMsgErr As String = String.Empty
        If strMsgExist <> String.Empty Then
            strMsgErr = "Syarat Lulus " + strMsgExist + " sudah ada"
        End If

        If strMsgFailed <> String.Empty Then
            strMsgErr += "Syarat Belum Lulus " + strMsgFailed + " sudah ada"
        End If

        If strMsgErr <> String.Empty Then
            MessageBox.Show(strMsgErr)
        Else
            MessageBox.Show(SR.SaveSuccess)
            ClearData()
        End If
        RetriveDataAndSaveToCache()

        dtgPreRequire.CurrentPageIndex = 0
        BindDataGrid(dtgPreRequire.CurrentPageIndex)

    End Sub

    Private Function InsertGroup() As Integer
        Dim objTrPreRequire As TrPreRequire = New TrPreRequire
        Dim objCourseId As TrCourse
        Dim objPreReqCourseId As TrCourse

        objCourseId = GetCourseInfo(txtCourseCode.Text.Trim())
        objPreReqCourseId = GetCourseInfo(txtPreRequireCode.Text.Trim())

        If objCourseId.ID > 0 And objPreReqCourseId.ID > 0 Then
            If Not IsExistGroup(objCourseId.ID, objPreReqCourseId.ID) Then
                objTrPreRequire.TrCourse = objCourseId
                objTrPreRequire.PreRequireCourseID = objPreReqCourseId.ID
                objTrPreRequire.Description = Me.txtDesc.Text.Trim()
                Return New TrPreRequireFacade(User).Insert(objTrPreRequire)
            Else
                Return -2
            End If
        End If
        Return -1
    End Function

    Private Sub UpdatePreRequire()

        Dim objTrPreRequire As TrPreRequire = CType(sHPreRequire.GetSession("vsTrPreRequire"), TrPreRequire)
        If Not IsNothing(objTrPreRequire) Then

            'Dim objTrPreRequire As TrPreRequire = New TrPreRequire
            'objTrPreRequire = New TrPreRequire
            Dim objCourseId As TrCourse
            Dim objPreReqCourseId As TrCourse
            Dim iLulus As Integer
            Dim iUpdate As Integer = 0
            objCourseId = GetCourseInfo(txtCourseCode.Text.Trim())
            'Insert data syarat lulus
            Dim strMsgExist As String = String.Empty
            Dim strMsgFailed As String = String.Empty
            If txtPreRequireCode.Text.Trim() <> "" Then
                Dim arrLulus As String() = txtPreRequireCode.Text.Trim().Split(";".ToCharArray())
                For Each strlulus As String In arrLulus
                    objPreReqCourseId = GetCourseInfo(strlulus)
                    If objCourseId.ID > 0 And objPreReqCourseId.ID > 0 Then
                        If Not IsExistGroup(objCourseId.ID, objPreReqCourseId.ID, EnumCourseRequireType.RequireType.SyaratLulus) Then
                            If iUpdate > 0 Then
                                iLulus = 0
                                objTrPreRequire.TrCourse = objCourseId
                                objTrPreRequire.PreRequireCourseID = objPreReqCourseId.ID
                                objTrPreRequire.RequireType = EnumCourseRequireType.RequireType.SyaratLulus
                                objTrPreRequire.Description = Me.txtDesc.Text.Trim()
                                iLulus = New TrPreRequireFacade(User).Insert(objTrPreRequire)
                            Else
                                'objTrPreRequire.TrCourse = objCourseId
                                objTrPreRequire.PreRequireCourseID = objPreReqCourseId.ID
                                objTrPreRequire.RequireType = EnumCourseRequireType.RequireType.SyaratLulus
                                objTrPreRequire.Description = Me.txtDesc.Text.Trim()
                                iLulus = New TrPreRequireFacade(User).Update(objTrPreRequire)
                                iUpdate = iUpdate + 1
                            End If
                        Else
                            '    'objTrPreRequire.TrCourse = objCourseId
                            '    objTrPreRequire.PreRequireCourseID = objPreReqCourseId.ID
                            '    objTrPreRequire.RequireType = EnumCourseRequireType.RequireType.SyaratLulus
                            '    objTrPreRequire.Description = Me.txtDesc.Text.Trim()
                            '    iLulus = New TrPreRequireFacade(User).Update(objTrPreRequire)
                            strMsgExist += strlulus + ", "
                        End If
                        If iLulus = -1 Then
                            strMsgFailed += strlulus + ", "
                        End If
                    End If
                Next
            End If

            'Insert data syarat blm lulus
            If txtPreRequireCodeNoPass.Text.Trim() <> "" Then
                Dim arrBlmLulus As String() = txtPreRequireCodeNoPass.Text.Trim().Split(";".ToCharArray())
                For Each strblmlulus As String In arrBlmLulus
                    objPreReqCourseId = GetCourseInfo(strblmlulus)
                    If objCourseId.ID > 0 And objPreReqCourseId.ID > 0 Then
                        If Not IsExistGroup(objCourseId.ID, objPreReqCourseId.ID, EnumCourseRequireType.RequireType.SyaratBelumLulus) Then
                            If iUpdate > 0 Then
                                iLulus = 0
                                objTrPreRequire.TrCourse = objCourseId
                                objTrPreRequire.PreRequireCourseID = objPreReqCourseId.ID
                                objTrPreRequire.RequireType = EnumCourseRequireType.RequireType.SyaratBelumLulus
                                objTrPreRequire.Description = Me.txtDesc.Text.Trim()
                                iLulus = New TrPreRequireFacade(User).Insert(objTrPreRequire)
                            Else
                                objTrPreRequire.PreRequireCourseID = objPreReqCourseId.ID
                                objTrPreRequire.RequireType = EnumCourseRequireType.RequireType.SyaratBelumLulus
                                objTrPreRequire.Description = Me.txtDesc.Text.Trim()
                                iLulus = New TrPreRequireFacade(User).Update(objTrPreRequire)
                                iUpdate = iUpdate + 1
                            End If
                        Else
                            'objTrPreRequire.PreRequireCourseID = objPreReqCourseId.ID
                            'objTrPreRequire.RequireType = EnumCourseRequireType.RequireType.SyaratBelumLulus
                            'objTrPreRequire.Description = Me.txtDesc.Text.Trim()
                            'iLulus = New TrPreRequireFacade(User).Update(objTrPreRequire)
                            strMsgExist += strblmlulus + ", "
                        End If
                        If iLulus = -1 Then
                            strMsgFailed += strblmlulus + ", "
                        End If
                    End If
                Next
            End If


            Dim strMsgErr As String = String.Empty
            If strMsgFailed <> String.Empty Then
                strMsgErr += "Syarat " + strMsgFailed + " gagal disimpan"
            End If

            If strMsgExist <> String.Empty Then
                strMsgErr += "Syarat " + strMsgExist + " sudah ada"
            End If

            If strMsgErr <> String.Empty Then
                MessageBox.Show(strMsgErr)
            Else
                MessageBox.Show(SR.SaveSuccess)
                ClearData()
            End If
            RetriveDataAndSaveToCache()

            dtgPreRequire.CurrentPageIndex = 0
            BindDataGrid(dtgPreRequire.CurrentPageIndex)
        End If


    End Sub

    Private Function UpdateGroup() As Integer
        Dim objTrPreRequire As TrPreRequire = CType(sHPreRequire.GetSession("vsTrPreRequire"), TrPreRequire)
        If Not IsNothing(objTrPreRequire) Then
            'Dim objTrCourse As TrCourse = New TrCourseFacade(User).Retrieve(CType(ddlCourseId.SelectedValue, Integer))
            'Dim objPreReqCourse As TrCourse = GetCourseInfo(txtPreRequireCode.Text.Trim())
            'Dim objPreReqCourse As TrCourse = GetCourseInfo(objTrPreRequire.PreRequireCourseCode.Trim())
            Dim objPreReqCourse As TrCourse = GetCourseInfo(txtPreRequireCode.Text.Trim)
            'If Not IsNothing(objTrCourse) Then
            'check validation code if user change prerequireid in update mode
            If objTrPreRequire.PreRequireCourseID = objPreReqCourse.ID Then
                'objTrPreRequire.TrCourse = objTrCourse
                objTrPreRequire.PreRequireCourseID = objPreReqCourse.ID
                objTrPreRequire.Description = Me.txtDesc.Text
                Return New TrPreRequireFacade(User).Update(objTrPreRequire)
            Else
                If Not IsExistGroup(objTrPreRequire.TrCourse.ID, objPreReqCourse.ID) Then
                    'objTrPreRequire.TrCourse = objTrCourse
                    objTrPreRequire.PreRequireCourseID = objPreReqCourse.ID
                    objTrPreRequire.Description = Me.txtDesc.Text
                    Return New TrPreRequireFacade(User).Update(objTrPreRequire)
                Else
                    Return -2
                End If
            End If
            'End If
        End If
        Return -1
    End Function

    Private Sub DeletePreRequire(ByVal nID As Integer)
        Dim objTrPreRequire As TrPreRequire = New TrPreRequireFacade(User).Retrieve(nID)
        Dim facade As TrPreRequireFacade = New TrPreRequireFacade(User)
        facade.DeleteFromDB(objTrPreRequire)
        RetriveDataAndSaveToCache()
        dtgPreRequire.CurrentPageIndex = 0
        BindDataGrid(dtgPreRequire.CurrentPageIndex)
        ClearData()
    End Sub

    Private Function GetPreRequire(ByVal nID As Integer) As TrPreRequire
        Dim arlPreRequire As ArrayList = CType(Session.Item("PreRequireCollection"), ArrayList)
        If arlPreRequire.Count > 0 Then
            For Each objPreRequire As TrPreRequire In arlPreRequire
                If objPreRequire.ID = nID Then
                    Return objPreRequire
                End If
            Next
        End If
        Return New TrPreRequire
    End Function

    Private Sub ViewGroup(ByVal nID As Integer, ByVal EditStatus As Boolean)

        Me.txtCourseCode.Text = ""
        Me.txtPreRequireCode.Text = ""
        Me.txtPreRequireCodeNoPass.Text = ""
        Me.txtDesc.Text = ""

        Dim objTrPreRequire As TrPreRequire = GetPreRequire(nID)
        sHPreRequire.SetSession("vsTrPreRequire", objTrPreRequire)
        If Not IsNothing(objTrPreRequire) Then
            If Not IsNothing(objTrPreRequire.TrCourse) Then
                Me.txtCourseCode.Text = objTrPreRequire.TrCourse.CourseCode
            End If

            If Not (IsNothing(objTrPreRequire.PreRequireCourseID) Or objTrPreRequire.PreRequireCourseID = 0) Then
                If objTrPreRequire.RequireType = EnumCourseRequireType.RequireType.SyaratBelumLulus Then
                    Me.txtPreRequireCodeNoPass.Text = objTrPreRequire.PreRequireCourseCode
                Else
                    Me.txtPreRequireCode.Text = objTrPreRequire.PreRequireCourseCode
                End If
            End If
            Me.txtDesc.Text = objTrPreRequire.Description
        End If
        
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub dtgPreRequire_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPreRequire.PageIndexChanged
        dtgPreRequire.SelectedIndex = -1
        dtgPreRequire.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgPreRequire.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            'RetriveDataAndSaveToCache()
            Dim nResult As Integer = RetriveDataAndSaveToCache()
            If nResult > -1 Then
                dtgPreRequire.CurrentPageIndex = 0
                BindDataGrid(dtgPreRequire.CurrentPageIndex)
            Else
                If nResult = -2 Then
                    MessageBox.Show(SR.DataNotFound("Prasyarat"))
                End If
            End If

        End If
        assignAttributeControl()
    End Sub

    Private Function RetriveDataAndSaveToCache() As Integer
        'get all data from course to fill prerequirecode base on coursecode
        _trCourse = New TrCourseFacade(User).RetrieveList()
        sHPreRequire.SetSession("objTRCourse", _trCourse)

        _sessHelper.RemoveSession("PreRequireCollection")
        'get prerequire data
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite
        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrPreRequire), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtCourseCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrPreRequire), "TrCourse.CourseCode", MatchType.Exact, txtCourseCode.Text.Trim))
        End If

        If txtPreRequireCode.Text <> String.Empty Then
            Dim strPreRequireCode As String = txtPreRequireCode.Text.Trim.Replace(";", "','")
            Dim strCourseID As String = GetCourseID(strPreRequireCode)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrPreRequire), "PreRequireCourseID", MatchType.InSet, "(" & strCourseID & ")"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrPreRequire), "RequireType", MatchType.Exact, CType(EnumCourseRequireType.RequireType.SyaratLulus, Integer)))
        End If

        If txtPreRequireCodeNoPass.Text <> String.Empty Then
            Dim strPreRequireCodeNoPass As String = txtPreRequireCodeNoPass.Text.Trim.Replace(";", "','")
            Dim strCourseID As String = GetCourseID(strPreRequireCodeNoPass)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrPreRequire), "PreRequireCourseID", MatchType.InSet, "(" & strCourseID & ")"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrPreRequire), "RequireType", MatchType.Exact, CType(EnumCourseRequireType.RequireType.SyaratBelumLulus, Integer)))
        End If

        Dim arrPreRequire = New TrPreRequireFacade(User).RetrieveActiveList(criterias, dtgPreRequire.CurrentPageIndex + 1, dtgPreRequire.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
        If Not IsNothing(arrPreRequire) Then
            For Each objPreRequire As TrPreRequire In arrPreRequire
                objPreRequire.PreRequireCourseCode = GetPreRequireCode(objPreRequire.PreRequireCourseID)
            Next
        Else
            Return -2
        End If
        _sessHelper.SetSession("PreRequireCollection", arrPreRequire)

        Return 0
    End Function


    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.TrainingUbahPrasyarat_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.TrainingViewPrasyarat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=TRAINING - Prasyarat")
        End If

    End Sub

    Private Function IsUnhack() As Boolean
        If txtCourseCode.Text.IndexOf("<") >= 0 Or txtCourseCode.Text.IndexOf(">") >= 0 Or txtCourseCode.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtPreRequireCode.Text.IndexOf("<") >= 0 Or txtPreRequireCode.Text.IndexOf(">") >= 0 Or txtPreRequireCode.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtDesc.Text.IndexOf("<") >= 0 Or txtDesc.Text.IndexOf(">") >= 0 Or txtDesc.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        Return True
    End Function

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        'Ditambah validasi u/ Mozilla
        If Not Page.IsValid Then
            Return
        End If

        If Not IsUnhack() Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return
        End If

        If txtCourseCode.Text.Trim() = txtPreRequireCode.Text.Trim() Then
            MessageBox.Show("Kode Kategori dan Kode Prasyarat lulus tidak boleh sama")
            Return
        End If

        If txtCourseCode.Text.Trim() = txtPreRequireCodeNoPass.Text.Trim() Then
            MessageBox.Show("Kode Kategori dan Kode Prasyarat belum lulus tidak boleh sama")
            Return
        End If

        If txtPreRequireCode.Text.Trim() = "" And txtPreRequireCodeNoPass.Text.Trim() = "" Then
            MessageBox.Show("Isi Prasyarat lulus  atau belum lulus")
            Return
        End If

        Dim arrLulus As String()
        If txtPreRequireCode.Text.Trim() <> "" Then
            arrLulus = txtPreRequireCode.Text.Trim().Split(";".ToCharArray())
            Dim strLulusMsg As String = String.Empty
            For Each strlulus As String In arrLulus
                If Not IsCourseValid(strlulus) Then
                    'MessageBox.Show(SR.DataNotFound("Kode Kategori Lulus"))
                    'Return
                    strLulusMsg += strLulusMsg + ","
                End If
            Next
            If strLulusMsg <> String.Empty Then
                MessageBox.Show("Kode Kategori Lulus " + strLulusMsg + " tidak ada.")
                Return
            End If

        End If

        Dim arrBlmLulus As String()
        If txtPreRequireCodeNoPass.Text.Trim() <> "" Then
            arrBlmLulus = txtPreRequireCodeNoPass.Text.Trim().Split(";".ToCharArray())
            Dim strBlmLulusMsg As String = String.Empty
            For Each strblmlulus As String In arrBlmLulus
                If Not IsCourseValid(strblmlulus) Then
                    strBlmLulusMsg += strBlmLulusMsg + ","
                End If
            Next
            If strBlmLulusMsg <> String.Empty Then
                MessageBox.Show("Kode Kategori Belum Lulus " + strBlmLulusMsg + " tidak ada.")
                Return
            End If

        End If

        If Not IsNothing(arrLulus) And Not IsNothing(arrBlmLulus) Then
            If arrLulus.Length > 0 And arrBlmLulus.Length > 0 Then
                For Each strLulus As String In arrLulus
                    For Each strBlmLulus As String In arrBlmLulus
                        If strLulus = strBlmLulus Then
                            MessageBox.Show("Kode Kategori Prasyarat Lulus tidak boleh sama dengan Prasyarat Belum Lulus.(" + strLulus + ")")
                            Return
                        End If
                    Next
                Next
            End If
        End If

        'If Not IsCourseValid(txtPreRequireCode.Text) Then
        '    MessageBox.Show(SR.DataNotFound("Kode Prasyarat"))
        '    Return
        'End If

        If txtCourseCode.Text.Trim() = "M1" Then
            MessageBox.Show("Kode Kategori M1 adalah syarat pertama, tidak perlu diisi")
            Return
        End If

        Dim objTrPreRequire As TrPreRequire = New TrPreRequire
        Dim objTrPreRequireFacade As TrPreRequireFacade = New TrPreRequireFacade(User)
        Dim nResult = -1

        If txtCourseCode.Text.Trim() <> "" And (txtPreRequireCode.Text.Trim() <> "" Or txtPreRequireCodeNoPass.Text.Trim() <> "") Then
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                InsertPreRequire()
                'Dim intInsertResult As Integer = InsertGroup()
                'If intInsertResult = -1 Then
                '    MessageBox.Show(SR.SaveFail)
                'Else
                '    If intInsertResult = -2 Then
                '        MessageBox.Show("PreRequire sudah ada")
                '    Else
                '        MessageBox.Show(SR.SaveSuccess)
                '        ClearData()
                '        RetriveDataAndSaveToCache()
                '        dtgPreRequire.CurrentPageIndex = 0
                '        BindDataGrid(dtgPreRequire.CurrentPageIndex)
                '    End If
                'End If
            Else
                UpdatePreRequire()
                'Dim intUpdateResult As Integer = UpdateGroup()
                'If intUpdateResult = -1 Then
                '    MessageBox.Show(SR.UpdateFail)
                'Else
                '    If intUpdateResult = -2 Then
                '        MessageBox.Show(SR.DataIsExist("PreRequire"))
                '    Else
                '        MessageBox.Show(SR.UpdateSucces)
                '        ClearData()
                '        RetriveDataAndSaveToCache()
                '        dtgPreRequire.CurrentPageIndex = 0
                '        BindDataGrid(dtgPreRequire.CurrentPageIndex)
                '    End If
                'End If
            End If
        Else
            MessageBox.Show(SR.DataNotChooseYet("Kategori atau Prasyarat"))
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        dtgPreRequire.SelectedIndex = -1
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        RetriveDataAndSaveToCache()
        'Dim totalRow As Integer = 0
        'Dim criterias As CriteriaComposite
        'criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrPreRequire), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'If txtCourseCode.Text <> "" Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrPreRequire), "TrCourse.CourseCode", MatchType.Exact, txtCourseCode.Text.Trim))
        'End If

        'If txtPreRequireCode.Text <> String.Empty Then
        '    Dim strPreRequireCode As String = txtPreRequireCode.Text.Trim.Replace(";", "','")
        '    Dim strCourseID As String = GetCourseID(strPreRequireCode)
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrPreRequire), "PreRequireCourseID", MatchType.InSet, "(" & strCourseID & ")"))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrPreRequire), "RequireType", MatchType.Exact, CType(EnumCourseRequireType.RequireType.SyaratLulus, Integer)))
        'End If

        'If txtPreRequireCodeNoPass.Text <> String.Empty Then
        '    Dim strPreRequireCodeNoPass As String = txtPreRequireCodeNoPass.Text.Trim.Replace(";", "','")
        '    Dim strCourseID As String = GetCourseID(strPreRequireCodeNoPass)
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrPreRequire), "PreRequireCourseID", MatchType.InSet, "(" & strCourseID & ")"))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrPreRequire), "RequireType", MatchType.Exact, CType(EnumCourseRequireType.RequireType.SyaratBelumLulus, Integer)))
        'End If

        'Dim arrPreRequire = New TrPreRequireFacade(User).RetrieveActiveList(criterias, dtgPreRequire.CurrentPageIndex + 1, dtgPreRequire.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
        'If Not IsNothing(arrPreRequire) Then
        '    For Each objPreRequire As TrPreRequire In arrPreRequire
        '        objPreRequire.PreRequireCourseCode = GetPreRequireCode(objPreRequire.PreRequireCourseID)
        '    Next
        'Else
        'End If
        '_sessHelper.SetSession("PreRequireCollection", arrPreRequire)

        dtgPreRequire.CurrentPageIndex = 0
        BindDataGrid(dtgPreRequire.CurrentPageIndex)

    End Sub

    Private Function GetCourseID(ByVal strCourceCode As String)
        Dim strReturn As String
        Try
            Dim criterias As CriteriaComposite
            criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "CourseCode", MatchType.InSet, "('" & strCourceCode & "')"))
            Dim arrCourse As ArrayList = New TrCourseFacade(User).Retrieve(criterias)
            If arrCourse.Count > 0 Then
                For Each _course As TrCourse In arrCourse
                    strReturn = _course.ID.ToString & "," & strReturn
                Next
                strReturn = strReturn.Substring(0, strReturn.Length - 1)
            End If
        Catch ex As Exception
            strReturn = String.Empty
        End Try
        Return strReturn
    End Function

    Private Sub assignAttributeControl()
        lblPopUpCourse1.Attributes("onclick") = "ShowPPCourseSelection1();"
        lblPopUpCourse2.Attributes("onclick") = "ShowPPCourseSelection2();"
        lblPreRequireCodeNoPass.Attributes("onclick") = "ShowPPCourseSelection3();"
    End Sub

    Private Sub SetControl(ByVal IsReadOnly As Boolean)
        txtCourseCode.ReadOnly = IsReadOnly
        lblPopUpCourse1.Enabled = Not IsReadOnly
        txtPreRequireCode.ReadOnly = IsReadOnly
        txtPreRequireCodeNoPass.ReadOnly = IsReadOnly
        lblPopUpCourse2.Enabled = Not IsReadOnly
        txtDesc.ReadOnly = IsReadOnly
    End Sub

    Private Sub dtgPreRequire_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPreRequire.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewGroup(e.Item.Cells(0).Text, False)
            SetControl(True)
            dtgPreRequire.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewGroup(e.Item.Cells(0).Text, True)
            SetControl(False)
            'override: coursecode always readonly in edit mode
            txtCourseCode.ReadOnly = True
            lblPopUpCourse1.Enabled = False
            dtgPreRequire.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            DeletePreRequire(e.Item.Cells(0).Text)
        End If
    End Sub

    Private Function GetPreRequireCode(ByVal PreRequireID As Integer) As String
        _trCourse = sHPreRequire.GetSession("objTRCourse")
        For x As Integer = 0 To _trCourse.Count - 1
            Dim _objTRCourse As TrCourse = CType(_trCourse(x), TrCourse)

            If PreRequireID = _objTRCourse.ID Then
                Return _objTRCourse.CourseCode
            End If
        Next
        Return ""
    End Function

    Private Sub dtgPreRequire_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPreRequire.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As TrPreRequire = CType(e.Item.DataItem, TrPreRequire)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or _
                        e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgPreRequire.CurrentPageIndex * dtgPreRequire.PageSize)
                CType(e.Item.FindControl("lbType"), Label).Text = EnumCourseRequireType.GetStringValue(RowValue.RequireType)
            End If

            If Not e.Item.FindControl("btnHapus") Is Nothing Then
                CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            End If

            'privilege
            If Not e.Item.FindControl("btnHapus") Is Nothing Then
                CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = m_bFormPrivilege
            End If
            If Not e.Item.FindControl("btnUbah") Is Nothing Then
                CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = m_bFormPrivilege
            End If
            'End If
        End If

    End Sub


    Private Sub dtgPreRequire_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPreRequire.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Integer)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select

        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        dtgPreRequire.SelectedIndex = -1
        dtgPreRequire.CurrentPageIndex = 0
        BindDataGrid(dtgPreRequire.CurrentPageIndex)
        ClearData()
    End Sub

    Private Function IsCourseValid(ByVal courseCode As String) As Boolean
        Return New TrCourseFacade(User).ValidateCode(courseCode) > 0
    End Function

#End Region

    
End Class