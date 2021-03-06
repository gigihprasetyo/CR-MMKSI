#Region ".NET Base Class Namespace"
Imports System.Collections.Specialized
#End Region

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

Public Class FrmViewTrClassRegistration1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgClassRegistration As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPopUpClass As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents pnlDealerSearch As System.Web.UI.WebControls.Panel
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerSearchCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblHeader As System.Web.UI.WebControls.Label
    Protected WithEvents btnProsesCetak As System.Web.UI.WebControls.Button
    Protected WithEvents txtNoReg As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTraineeName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnUpdate As System.Web.UI.WebControls.Button
    Protected WithEvents txtClassCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents icStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents cbDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeKategori As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchKodeKategori As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Private objSessionHelper As New SessionHelper

#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
            RestoreLastState()
        End If
        AssignAttribute()
    End Sub

#Region "Private Method"

    Private Sub ActivateUserPrivilege(ByVal IsKTB As Boolean)
        Dim bUpdateRegistrationStatus As Boolean = False
        If IsKTB Then
            If Not SecurityProvider.Authorize(Context.User, SR.TrainingViewStatusPendaftaranKTB_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Lihat Pendaftaran")
            End If
            bUpdateRegistrationStatus = SecurityProvider.Authorize(Context.User, SR.TrainingUbahStatusPendaftaranKTB_Privilege)
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.TrainingViewStatusPendaftaranDealer_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Lihat Pendaftaran")
            End If
        End If
        btnUpdate.Visible = bUpdateRegistrationStatus
        ddlStatus.Visible = bUpdateRegistrationStatus
    End Sub

    Private Sub InitiatePage()
        'BindDdlStatus()
        BindDdlYear()
        BindDdlStatus()
        ViewState("CurrentSortColumn") = "RegistrationDate"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        'Dim coll As NameValueCollection = Page.Request.QueryString
        'If coll.Count > 0 Then
        '    GetQueryString(coll)
        '    BindDataGrid(0)
        'End If
        If Not Session.Item("arlQueryColl") Is Nothing Then
            Dim arlQueryColl As ArrayList = CType(Session.Item("arlQueryColl"), ArrayList)
            If arlQueryColl.Count = 6 Then
                GetSessionData(arlQueryColl)
                BindDataGrid(0)
            End If
        End If

        If Not Session.Item("DEALER") Is Nothing Then
            Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
            If objDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                SetDealerInfo(objDealer, True)
                lblHeader.Text = "TRAINING - List Pendaftaran"
            Else
                SetDealerInfo(objDealer, False)
                lblHeader.Text = "TRAINING - Status Pendaftaran"
            End If
        End If


    End Sub

    Private Sub BindDdlYear()
        Dim thisYear As Integer = Now.Year
        'Modified by Ikhsan, 07 Agustus 2008
        'Requested by Rina as Part of CR
        'To add option "Silahkan Pilih" for ddlYear
        '--------------------------------------------------------
        ddlYear.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        '--------------------------------------------------------
        For year As Integer = thisYear - 3 To thisYear + 3
            Dim item As ListItem = New ListItem(CStr(year), CStr(year))
            ddlYear.Items.Add(item)
        Next
        'Modified by Ikhsan, 07 Agustus 2008
        'Requested by Rina as Part of CR
        'To add option "Silahkan Pilih" for ddlYear
        '--------------------------------------------------------
        ddlYear.SelectedIndex = -1
        '--------------------------------------------------------
    End Sub

    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        Dim obj As New EnumTrClassRegistration
        Dim arlStatusReg As ArrayList = obj.RetrieveStatus()
        For Each en As EnumClassReg In arlStatusReg
            If en.ValueType = obj.DataStatusType.Cancel Or en.ValueType = obj.DataStatusType.Reject Then
                Dim lItem As ListItem = New ListItem(en.NameType, en.ValueType.ToString())
                ddlStatus.Items.Add(lItem)
            End If
        Next

        'Dim statusColl As Array = System.Enum.GetValues(GetType(TrClassRegistration.EnumClassRegStatus))
        'For i As Integer = 0 To statusColl.Length - 1
        '    ddlStatus.Items.Add(New ListItem( _
        '    System.Enum.GetName(GetType(TrClassRegistration.EnumClassRegStatus), statusColl(i)), _
        '    CType(statusColl(i), String)))
        'Next
        'pilihan kosong dihilangkan untuk menghindari save data kosong dan error saat menampilkannya
        'ddlStatus.Items.Insert(0, New ListItem("", ""))
    End Sub

    Private Sub SetDealerInfo(ByVal objDealer As Dealer, ByVal IsKTB As Boolean)
        'check privilege first, before set control to form
        ActivateUserPrivilege(IsKTB)
        dtgClassRegistration.Columns(1).Visible = IsKTB
        pnlDealerSearch.Visible = IsKTB
        'btnProsesCetak.Visible = Not IsKTB
        lblDealerCode.Text = objDealer.DealerCode & " / " & objDealer.SearchTerm1
        lblDealerName.Text = objDealer.DealerName
    End Sub

    'Private Sub BindDdlStatus()
    '    ddlStatus.Items.Clear()
    '    Dim statusColl As Array = System.Enum.GetValues(GetType(TrClassRegistration.EnumClassRegStatus))
    '    For i As Integer = 0 To statusColl.Length - 1
    '        ddlStatus.Items.Add(New ListItem( _
    '        System.Enum.GetName(GetType(TrClassRegistration.EnumClassRegStatus), statusColl(i)), _
    '        CType(statusColl(i), String)))
    '    Next
    '    ddlStatus.Items.Insert(0, New ListItem("", ""))
    'End Sub

    'Private Sub GetQueryString(ByVal QueryStringColl As NameValueCollection)
    '    'querystring collection consist of 0:classcode 1:status 2:regdatefrom 3:regdateto
    '    If QueryStringColl(0) = "all" Then
    '        txtClassCode.Text = ""
    '    Else
    '        txtClassCode.Text = QueryStringColl(0)
    '    End If
    '    If QueryStringColl(1) = "all" Then
    '        ddlStatus.SelectedValue = ""
    '    Else
    '        ddlStatus.SelectedValue = QueryStringColl(1)
    '    End If
    '    calRegDateFrom.Value = CType(QueryStringColl(2), DateTime)
    '    calRegDateTo.Value = CType(QueryStringColl(3), DateTime)
    'End Sub

    Private Sub GetSessionData(ByVal arlQueryColl As ArrayList)
        If (CType(arlQueryColl(0), QueryStringCollection)).ParamValue = "all" Then
            txtDealerSearchCode.Text = ""
        Else
            txtDealerSearchCode.Text = (CType(arlQueryColl(0), QueryStringCollection)).ParamValue
        End If
        If (CType(arlQueryColl(1), QueryStringCollection)).ParamValue = "all" Then
            txtClassCode.Text = ""
        Else
            txtClassCode.Text = (CType(arlQueryColl(1), QueryStringCollection)).ParamValue
        End If

        If (CType(arlQueryColl(2), QueryStringCollection)).ParamValue = "all" Then
            txtNoReg.Text = ""
        Else
            txtNoReg.Text = (CType(arlQueryColl(2), QueryStringCollection)).ParamValue
        End If

        If (CType(arlQueryColl(3), QueryStringCollection)).ParamValue = "all" Then
            txtTraineeName.Text = ""
        Else
            txtTraineeName.Text = (CType(arlQueryColl(3), QueryStringCollection)).ParamValue
        End If

        'If (CType(arlQueryColl(1), QueryStringCollection)).ParamValue = "all" Then
        '    ddlStatus.SelectedValue = ""
        'Else
        '    ddlStatus.SelectedValue = (CType(arlQueryColl(1), QueryStringCollection)).ParamValue
        'End If
        Dim item As ListItem = ddlYear.Items.FindByValue(CStr(CType(arlQueryColl(4), QueryStringCollection).ParamValue))
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item)
    End Sub

    Private Sub AssignAttribute()
        CType(Page.FindControl("lblPopUpDealer"), Label).Attributes.Add("onclick", GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection"))
        'CType(Page.FindControl("lblPopUpClass"), Label).Attributes.Add("onclick", GeneralScript.GetPopUpEventReference("../PopUp/PopUpClassSelection.aspx", "", 500, 760, "classSelection"))
        lblPopUpClass.Attributes("onclick") = "ShowPPClassSelection()"
        lblSearchKodeKategori.Attributes("onclick") = "ShowCategoryManySelection()"
    End Sub

    Private Function ConvertKodeDealer(ByVal sKodeDealerColl As String)
        Dim sKodeDealerTemp() As String = sKodeDealerColl.Split(New Char() {";"})
        Dim sKodeDealer As String = ""
        For i As Integer = 0 To sKodeDealerTemp.Length - 1
            sKodeDealer = sKodeDealer & "'" & sKodeDealerTemp(i).Trim() & "'"

            If Not (i = sKodeDealerTemp.Length - 1) Then
                sKodeDealer = sKodeDealer & ","
            End If
        Next
        sKodeDealer = "(" & sKodeDealer & ")"
        Return sKodeDealer
    End Function

    Private Function CreateDefaultCriteria() As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), _
            "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        ' Modified by Ikhsan, 20081212
        ' Requested by Rina as Part of CR
        ' Retrieve only active Trainee
        ' Start -----
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), _
                "TrTrainee.Status", MatchType.Exact, "1"))
        ' End -----

        Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        If Not objDealer Is Nothing Then
            If objDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                If txtDealerSearchCode.Text.Trim() <> "" Then
                    'criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.Dealer.DealerCode", MatchType.Exact, txtDealerSearchCode.Text.Trim()))
                    'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtDealerSearchCode.Text.Trim())))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtDealerSearchCode.Text.Trim())))
                End If
            Else
                'criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.Dealer.ID", MatchType.Exact, objDealer.ID))
                criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Dealer.ID", MatchType.Exact, objDealer.ID))
            End If
        End If

        If txtKodeKategori.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), _
                "TrClass.TrCourse.CourseCode", MatchType.InSet, "('" & txtKodeKategori.Text.Trim().Replace(";", "','") & "')"))
        End If

        If txtClassCode.Text.Trim() <> "" Then
            'Dim listClassCode As String = GetListClassCode(txtCourseCode.Text)
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), _
                "TrClass.ClassCode", MatchType.Exact, txtClassCode.Text.Trim()))
        End If

        If txtNoReg.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), _
                "TrTrainee.ID", MatchType.Exact, CInt(txtNoReg.Text)))
        End If

        If txtTraineeName.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), _
                "TrTrainee.Name", MatchType.[Partial], txtTraineeName.Text.Trim()))
        End If

        'Modified by Ikhsan, 07 Agustus 2008
        'Requested by Rina, as Part OF CR
        'To add action of Criteria for "Silahkan Pilih" in ddlYear
        '-----------------------------------------------------------------
        If ddlYear.SelectedValue <> -1 Then
            '-----------------------------------------------------------------
            'Original Block
            Dim startDate As DateTime = New DateTime(CInt(ddlYear.SelectedValue), 1, 1, 0, 0, 0)
            Dim endDate As DateTime = New DateTime(CInt(ddlYear.SelectedValue), 12, DateTime.DaysInMonth(CInt(ddlYear.SelectedValue), 12), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), _
                "RegistrationDate", MatchType.GreaterOrEqual, startDate))
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), _
                "RegistrationDate", MatchType.LesserOrEqual, endDate))
            '-----------------------------------------------------------------
        End If
        '-----------------------------------------------------------------



        If cbDate.Checked = True Then
            Dim tglMulai As DateTime = New Date(icStart.Value.Year, icStart.Value.Month, icStart.Value.Day, 0, 0, 0)
            Dim tglAkhir As DateTime = New Date(icEnd.Value.Year, icEnd.Value.Month, icEnd.Value.Day, 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.StartDate", MatchType.GreaterOrEqual, tglMulai))
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.StartDate", MatchType.LesserOrEqual, tglAkhir))
        End If

        objSessionHelper.SetSession("classRegCriteria", criterias)

        Return criterias
    End Function

    'Private Function GetListClassCode(ByVal courseCode As String) As String
    '    Dim course As TrCourse = New TrCourseFacade(User).Retrieve(courseCode)
    '    Dim classes As ArrayList = course.TrClasss

    '    If classes.Count = 0 Then
    '        Return "(0)"
    '    End If

    '    Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
    '    For idx As Integer = 0 To classes.Count - 1
    '        If idx = 0 Then
    '            sb.Append("(")
    '        Else
    '            sb.Append(",")
    '        End If
    '        sb.Append(CType(classes(idx), TrClass).ID)
    '        If idx = classes.Count - 1 Then
    '            sb.Append(")")
    '        End If
    '    Next
    '    Return sb.ToString
    'End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            Dim arlClassReg As ArrayList = New TrClassRegistrationFacade(User).RetrieveByCriteria( _
                CreateDefaultCriteria(), _
                indexPage + 1, dtgClassRegistration.PageSize, totalRow, _
                CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            If arlClassReg.Count > 0 Then
                dtgClassRegistration.DataSource = arlClassReg
                dtgClassRegistration.VirtualItemCount = totalRow
                dtgClassRegistration.DataBind()
            Else
                MessageBox.Show(SR.DataNotFound("Data"))
                dtgClassRegistration.DataSource = Nothing
                dtgClassRegistration.VirtualItemCount = totalRow
                dtgClassRegistration.DataBind()
            End If
            btnProsesCetak.Enabled = arlClassReg.Count > 0

        End If
    End Sub

    'Private Function GenerateQueryString(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs, _
    'ByVal IsUpdate As Boolean) As String
    '    'notify that "act" value in param querystring is 0 for view and 1 for edit/update
    '    Dim strClassCode As String = "all"
    '    Dim strStatus As String = "all"
    '    Dim strAct As String = "0"
    '    If txtClassCode.Text.Trim() <> "" Then
    '        strClassCode = txtClassCode.Text.Trim()
    '    End If
    '    If ddlStatus.SelectedValue <> "" Then
    '        strStatus = ddlStatus.SelectedValue
    '    End If
    '    If IsUpdate Then
    '        strAct = "1"
    '    End If

    '    Return "classcode=" & strClassCode & _
    '    "&status=" & strStatus & _
    '    "&regfrom=" & calRegDateFrom.Value.ToShortDateString & _
    '    "&regto=" & calRegDateTo.Value.ToShortDateString & _
    '    "&act=" & strAct & _
    '    "&regid=" & e.Item.Cells(0).Text.Trim()
    'End Function

    Private Function SetQueryStringColl(ByVal Name As String, _
        ByVal Value As String) As QueryStringCollection
        Dim objQueryColl As New QueryStringCollection
        objQueryColl.ParamName = Name
        objQueryColl.ParamValue = Value
        Return objQueryColl
    End Function

    Private Sub GenerateSessionState(ByVal IsUpdate As Boolean, _
        ByVal ClassRegID As Integer)
        Dim strDealerCode As String = "all"
        Dim strClassCode As String = "all"
        Dim strNoReg As String = "all"
        Dim strTraineeName As String = "all"
        Dim strAct As String = "0"

        If txtDealerSearchCode.Text.Trim() <> "" Then
            strDealerCode = txtDealerSearchCode.Text.Trim()
        End If
        If txtClassCode.Text.Trim() <> "" Then
            strClassCode = txtClassCode.Text.Trim()
        End If
        If txtNoReg.Text.Trim() <> "" Then
            strNoReg = txtNoReg.Text.Trim()
        End If
        If txtTraineeName.Text.Trim() <> "" Then
            strTraineeName = txtTraineeName.Text.Trim()
        End If

        'If ddlStatus.SelectedValue <> "" Then
        '    strStatus = ddlStatus.SelectedValue
        'End If
        If IsUpdate Then
            strAct = "1"
        End If
        Dim arlQueryColl As New ArrayList
        arlQueryColl.Add(CType(SetQueryStringColl("dealercode", strDealerCode), QueryStringCollection))
        arlQueryColl.Add(CType(SetQueryStringColl("classcode", strClassCode), QueryStringCollection))
        arlQueryColl.Add(CType(SetQueryStringColl("noreg", strNoReg), QueryStringCollection))
        arlQueryColl.Add(CType(SetQueryStringColl("traineename", strTraineeName), QueryStringCollection))
        'arlQueryColl.Add(CType(SetQueryStringColl("status", strStatus), QueryStringCollection))
        arlQueryColl.Add(CType(SetQueryStringColl("regyear", ddlYear.SelectedValue), QueryStringCollection))
        arlQueryColl.Add(CType(SetQueryStringColl("action", strAct), QueryStringCollection))
        objSessionHelper.SetSession("arlQueryColl", arlQueryColl)

        Dim objClassRegistration As TrClassRegistration = GetClassRegistration(ClassRegID)
        objSessionHelper.SetSession("objClassRegistration", objClassRegistration)
    End Sub

    Private Function GetClassRegistration(ByVal ClassRegID As Integer) As TrClassRegistration
        Return New TrClassRegistrationFacade(User).Retrieve(ClassRegID)
    End Function

    Private Function IsUpdatingValid(ByVal arlTCR As ArrayList, ByVal IsReject As Boolean) As Boolean
        Dim Sql As String = ""
        Dim oTCRFac As TrClassRegistrationFacade = New TrClassRegistrationFacade(User)
        Dim arlTemp As New ArrayList
        Dim crtTCR As CriteriaComposite

        'Start  :Check whether the data has already been scored or not 
        For Each oTCR As TrClassRegistration In arlTCR
            Sql = "select count(*) from TrCertificateLine tcl where tcl.RowStatus=0 and tcl.RegistrationID=" & oTCR.ID
            crtTCR = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtTCR.opAnd(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, "(" & Sql & ")"))
            arlTemp = oTCRFac.Retrieve(crtTCR)
            If arlTemp.Count = 0 Then
                MessageBox.Show("Perubahan status " & IIf(IsReject, "Tolak", "Batal") & " gagal, data sudah diinput nilai")
                Return False
            End If
        Next
        Return True
        'False  :Check whether the data has already been scored or not 
    End Function


    Private Sub UpdateStatus(ByVal Tolak As Boolean)
        Dim objTrClassRegistration As TrClassRegistration
        Dim objFacade As TrClassRegistrationFacade = New TrClassRegistrationFacade(User)
        Dim cbItem As CheckBox
        Dim result As ArrayList = New ArrayList
        For Each dtgItem As DataGridItem In dtgClassRegistration.Items
            If dtgItem.ItemType = ListItemType.AlternatingItem Or dtgItem.ItemType = ListItemType.Item Then
                cbItem = CType(dtgItem.FindControl("cbItem"), CheckBox)
                If cbItem.Checked Then
                    objTrClassRegistration = GetClassRegistration(CInt(dtgItem.Cells(0).Text))
                    If Not IsNothing(objTrClassRegistration) And objTrClassRegistration.ID > 0 Then
                        result.Add(objTrClassRegistration)
                    End If
                End If
            End If
        Next
        If Not IsUpdatingValid(result, Tolak) Then Exit Sub
        If Tolak Then
            Dim totalDealer As Integer = CountingDealer(result)
            If totalDealer = 0 Then
                MessageBox.Show("Belum ada data yang dipilih")
                Return
            End If
            If totalDealer > 1 Then
                MessageBox.Show("Proses Tolak hanya bisa terhadap siswa-siswa dari dealer yang sama")
                Return
            End If
            objSessionHelper.SetSession("arlRegistration", result)
            SaveCurrentState()
            Response.Redirect("./FrmTrClassRegistrationConfirm.aspx")
        Else
            For Each objTrClassRegistration In result
                Try
                    objFacade.DeleteFromDB(objTrClassRegistration)
                Catch
                End Try
            Next
            MessageBox.Show("Proses Ubah Status Selesai")
            dtgClassRegistration.CurrentPageIndex = 0
            BindDataGrid(0)
        End If
    End Sub

    Private Function CountingDealer(ByVal objTrClassRegistrations As ArrayList) As Integer
        If IsNothing(objTrClassRegistrations) Or objTrClassRegistrations.Count = 0 Then
            Return 0
        End If
        'Dim LastDealerID As Integer = CType(objTrClassRegistrations(0), TrClassRegistration).TrTrainee.Dealer.ID
        Dim LastDealerID As Integer = CType(objTrClassRegistrations(0), TrClassRegistration).Dealer.ID
        Dim totalDealer As Integer = 1
        For idx As Integer = 1 To objTrClassRegistrations.Count - 1
            'If CType(objTrClassRegistrations(idx), TrClassRegistration).TrTrainee.Dealer.ID <> LastDealerID Then
            If CType(objTrClassRegistrations(idx), TrClassRegistration).Dealer.ID <> LastDealerID Then
                totalDealer += 1
                Exit For
            End If
        Next
        Return totalDealer
    End Function

    Private Sub SaveCurrentState()
        objSessionHelper.SetSession("StateDealerCode", txtDealerSearchCode.Text)
        objSessionHelper.SetSession("StateClassCode", txtClassCode.Text)
        objSessionHelper.SetSession("StateRegistrationNumber", txtNoReg.Text)
        objSessionHelper.SetSession("StateTraineeName", txtTraineeName.Text)
        objSessionHelper.SetSession("StatePeriod", ddlYear.SelectedIndex)
    End Sub

    Private Sub RestoreLastState()
        Dim isRestore As Boolean = False
        If Not IsNothing(objSessionHelper.GetSession("StateDealerCode")) Then
            txtDealerSearchCode.Text = objSessionHelper.GetSession("StateDealerCode")
            objSessionHelper.RemoveSession("StateDealerCode")
            isRestore = True
        End If
        If Not IsNothing(objSessionHelper.GetSession("StateClassCode")) Then
            txtClassCode.Text = objSessionHelper.GetSession("StateClassCode")
            objSessionHelper.RemoveSession("StateClassCode")
            isRestore = True
        End If
        If Not IsNothing(objSessionHelper.GetSession("StateRegistrationNumber")) Then
            txtNoReg.Text = objSessionHelper.GetSession("StateRegistrationNumber")
            objSessionHelper.RemoveSession("StateRegistrationNumber")
            isRestore = True
        End If
        If Not IsNothing(objSessionHelper.GetSession("StateTraineeName")) Then
            txtTraineeName.Text = objSessionHelper.GetSession("StateTraineeName")
            objSessionHelper.RemoveSession("StateTraineeName")
            isRestore = True
        End If
        If Not IsNothing(objSessionHelper.GetSession("StatePeriod")) Then
            ddlYear.SelectedIndex = objSessionHelper.GetSession("StatePeriod")
            objSessionHelper.RemoveSession("StatePeriod")
            isRestore = True
        End If
        If isRestore Then
            dtgClassRegistration.CurrentPageIndex = 0
            BindDataGrid(dtgClassRegistration.CurrentPageIndex)
        End If
    End Sub

#End Region

#Region "Event Handler"
    Private Sub dtgClassRegistration_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassRegistration.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            Dim cbAll As HtmlInputCheckBox = CType(e.Item.FindControl("cbAll"), HtmlInputCheckBox)
            Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
            cbAll.Visible = False
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                cbAll.Visible = True
            End If
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            SetTextColumnNo(e)
            SetTextColumnStatus(e)
            Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
            SetVisibleColumnCommand(e, objDealer)

            Dim RowValue As TrClassRegistration = CType(e.Item.DataItem, TrClassRegistration)

            Dim cbItem As CheckBox = CType(e.Item.FindControl("cbItem"), CheckBox)
            cbItem.Attributes.Add("onclick", "EnableDelete('cbItem')")
            cbItem.Visible = False
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                cbItem.Visible = RowValue.Status = EnumTrClassRegistration.DataStatusType.Register
            End If

            Dim hlClass As HyperLink = CType(e.Item.FindControl("hlClass"), HyperLink)

            If Not IsNothing(RowValue.TrClass) Then
                Dim lblStartDate As Label = CType(e.Item.FindControl("lblStartDate"), Label)
                Dim lblEndDate As Label = CType(e.Item.FindControl("lblEndDate"), Label)
                lblStartDate.Text = RowValue.TrClass.StartDate.Day & "-" & RowValue.TrClass.StartDate.Month & "-" & RowValue.TrClass.StartDate.Year
                lblEndDate.Text = RowValue.TrClass.FinishDate.Day & "-" & RowValue.TrClass.FinishDate.Month & "-" & RowValue.TrClass.FinishDate.Year
            End If
            

            If Not IsNothing(RowValue.TrClass) And Not IsNothing(hlClass) Then
                Dim actionValue As String = "popUpClassInformation('" + RowValue.TrClass.ClassCode + "');"
                hlClass.Text = RowValue.TrClass.ClassCode
                hlClass.NavigateUrl = "javascript:" + actionValue
            End If
        End If
    End Sub

    Private Sub SetTextColumnNo(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgClassRegistration.CurrentPageIndex * dtgClassRegistration.PageSize)
    End Sub

    Private Sub SetTextColumnStatus(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        lblStatus.Text = New EnumTrClassRegistration().StatusByIndex( _
            CInt(CType(DataBinder.Eval(e.Item.DataItem, "Status"), String)))

        'Select Case CType(DataBinder.Eval(e.Item.DataItem, "Status"), String)
        '    Case "0"
        '        lblStatus.Text = GetEnumClassRegText(EnumTrClassRegistration.DataStatusType.Register)
        '    Case "1"
        '        lblStatus.Text = GetEnumClassRegText(EnumTrClassRegistration.DataStatusType.Pass)
        '    Case "2"
        '        lblStatus.Text = GetEnumClassRegText(EnumTrClassRegistration.DataStatusType.Fail)
        '    Case "3"
        '        lblStatus.Text = GetEnumClassRegText(EnumTrClassRegistration.DataStatusType.Reject)
        '    Case Else
        '        lblStatus.Text = ""
        'End Select
    End Sub

    Private Function GetEnumClassRegText(ByVal index As Integer) As String
        Dim obj As New EnumTrClassRegistration
        Return obj.StatusByIndex(index)
    End Function

    Private Sub SetVisibleColumnCommand(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs, _
        ByVal objDealer As Dealer)
        Dim lbtnLihat As LinkButton = CType(e.Item.FindControl("btnLihat"), LinkButton)
        'Dim lbtnUbah As LinkButton = CType(e.Item.FindControl("btnUbah"), LinkButton)

        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            lbtnLihat.Visible = True
            'lbtnUbah.Visible = True
        Else
            lbtnLihat.Visible = True
            'lbtnUbah.Visible = False
        End If
    End Sub

    Private Sub dtgClassRegistration_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgClassRegistration.ItemCommand
        If e.CommandName = "View" Then
            GenerateSessionState(False, CInt(e.Item.Cells(0).Text.Trim()))
            Response.Redirect("../Training/FrmViewTrClassRegistration2.aspx")
            'Response.Redirect("../Training/FrmViewTrClassRegistration2.aspx?" & GenerateQueryString(e, False))
        End If
        'If e.CommandName = "Edit" Then
        '    GenerateSessionState(True, CInt(e.Item.Cells(0).Text.Trim()))
        '    Response.Redirect("../Training/FrmViewTrClassRegistration2.aspx")
        '    'Response.Redirect("../Training/FrmViewTrClassRegistration2.aspx?" & GenerateQueryString(e, True))
        'End If
    End Sub

    Private Sub dtgClassRegistration_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClassRegistration.PageIndexChanged
        dtgClassRegistration.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgClassRegistration.CurrentPageIndex)
    End Sub

    Private Sub dtgClassRegistration_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClassRegistration.SortCommand
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

        dtgClassRegistration.CurrentPageIndex = 0
        BindDataGrid(dtgClassRegistration.CurrentPageIndex)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If txtNoReg.Text.Trim() <> String.Empty Then
                Dim i As Integer = CInt(txtNoReg.Text.Trim())
            End If
        Catch
            MessageBox.Show("Nomor Registrasi tidak boleh karakter.")
            Exit Sub
        End Try
        dtgClassRegistration.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub


    Private Sub btnProsesCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProsesCetak.Click
        If IsNothing(objSessionHelper.GetSession("classRegCriteria")) Then
            MessageBox.Show("Data tidak ditemukan")
            Return
        End If
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(TrClassRegistration), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        objSessionHelper.SetSession("classRegSort", sortCol)

        Response.Redirect("./FrmTrRegistrationStatus.aspx")

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        UpdateStatus(ddlStatus.SelectedItem.Text = "Tolak")
    End Sub

#End Region

End Class
