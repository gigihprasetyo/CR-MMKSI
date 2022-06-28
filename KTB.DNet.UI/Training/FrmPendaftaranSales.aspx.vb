#Region ".NET Base Class Namespace"
Imports System.Collections.Specialized
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports GlobalExtensions
#End Region

Public Class FrmPendaftaranSales
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    'Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    'Protected WithEvents dtgClassAllocation As System.Web.UI.WebControls.DataGrid
    'Protected WithEvents lblPopUpCourse As System.Web.UI.WebControls.Label
    'Protected WithEvents txtCourseCode As System.Web.UI.WebControls.TextBox
    'Protected WithEvents lblInformation As System.Web.UI.WebControls.Label

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
    Private bPrivilegePendaftaran As Boolean = False
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not Page.IsPostBack Then
            InitiatePage()
        End If
        assignAttributeControl()
    End Sub


    Private Sub assignAttributeControl()
        lblPopUpCourse.Attributes("onclick") = "ShowPPCourseSelection();"
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not Session.Item("DEALER") Is Nothing Then
            Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
            If SecurityProvider.Authorize(Context.User, SR.TrainingPendaftaran_Privilege) _
                And PassTransactionControl(objDealer) Then
                bPrivilegePendaftaran = True
            End If
            If Not bPrivilegePendaftaran Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Pendaftaran")
            End If
        End If
    End Sub

    Private Function PassTransactionControl(ByVal objDealer As Dealer) As Boolean
        Dim bPassTC As Boolean = True

        Dim objTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl( _
            objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.DaftarTraining).ToString())

        If Not objTC Is Nothing Then
            If objTC.Status = 0 Then
                bPassTC = False
            End If
        End If

        Return bPassTC
    End Function

    Private Sub InitiatePage()
        BindDdlYear()

        ViewState("CurrentSortColumn") = "TrClass.StartDate"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        Dim coll As NameValueCollection = Page.Request.QueryString
        If coll.Count > 0 Then
            GetQueryString(coll)
            dtgClassAllocation.CurrentPageIndex = 0
            BindDataGrid(dtgClassAllocation.CurrentPageIndex)
            InitInfoAllocation()
        End If
    End Sub

    Private Sub GetQueryString(ByVal QueryStringColl As NameValueCollection)
        'querystring collection consist of 0:year 1:month 2:category 3:allocid
        '0-2 > we use it for query again in registration1 if user press Kembali button 
        If QueryStringColl(0) = "all" Then
            ddlYear.SelectedValue = ""
        Else
            ddlYear.SelectedValue = QueryStringColl(0)
        End If
        If QueryStringColl(1) = "all" Then
            ddlMonth.SelectedValue = ""
        Else
            ddlMonth.SelectedValue = QueryStringColl(1)
        End If
        If QueryStringColl(2) = "all" Then
            txtCourseCode.Text = ""
        Else
            txtCourseCode.Text = QueryStringColl(2)
        End If
    End Sub

    Private Sub BindDdlYear()
        Dim thisYear As Integer = DateTime.Now.Year() - 1
        For i As Integer = 0 To 4
            ddlYear.Items.Add(New ListItem(thisYear.ToString(), thisYear.ToString()))
            thisYear = thisYear + 1
        Next

        ddlYear.SelectedValue = DateTime.Now.Year.ToString()
    End Sub

    Private Sub AssignAttribute()

    End Sub

    Private Function GenerateDateForSearch()
        Dim dt As DateTime

        If (ddlYear.SelectedValue <> "") Then
            If (ddlMonth.SelectedValue <> "") Then
                dt = New DateTime(CInt(ddlYear.SelectedValue), CInt(ddlMonth.SelectedValue), 1, 0, 0, 0)
            Else
                dt = New DateTime(CInt(ddlYear.SelectedValue), 1, 1, 0, 0, 0)
            End If
        Else
            dt = New DateTime(1900, 1, 1)
        End If

        Return dt

    End Function

    Private Function MaxDay(ByVal Month As Short, ByVal year As Integer) As Short
        Select Case Month
            Case 1, 3, 5, 7, 8, 10, 12
                Return 31
            Case 4, 6, 9, 11
                Return 30
            Case 2
                If year Mod 4 = 0 Then
                    Return 29
                Else
                    Return 28
                End If
        End Select

    End Function

    Private Function GenerateCriteriaDate(ByVal IsLowerBound As Boolean)
        Dim dt As DateTime

        If (ddlYear.SelectedValue <> "") Then
            If (ddlMonth.SelectedValue <> "") Then
                If IsLowerBound Then
                    dt = New DateTime(CInt(ddlYear.SelectedValue), CInt(ddlMonth.SelectedValue), 1, 0, 0, 0)
                Else
                    dt = New DateTime(CInt(ddlYear.SelectedValue), CInt(ddlMonth.SelectedValue), MaxDay(CInt(ddlMonth.SelectedValue), CInt(ddlYear.SelectedValue)), 23, 59, 59)
                End If
            Else
                If IsLowerBound Then
                    dt = New DateTime(CInt(ddlYear.SelectedValue), 1, 1, 0, 0, 0)
                Else
                    dt = New DateTime(CInt(ddlYear.SelectedValue), 12, 31, 23, 59, 59)
                End If
            End If
        Else
            dt = New DateTime(1900, 1, 1)
        End If

        Return dt

    End Function


    Private Function CreateDefaultCriteria(ByRef nResult As Integer) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria( _
            GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim objDealer As Dealer = Me.GetDealer()
        If Not objDealer Is Nothing Then
            'If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            criterias.opAnd(New Criteria(GetType(TrClassAllocation), _
                "Dealer.ID", MatchType.Exact, objDealer.ID))
            'End If
        End If

        Dim dtCriteriaDate1 As DateTime = GenerateCriteriaDate(True)
        If dtCriteriaDate1 <> "1/1/1900" Then
            criterias.opAnd(New Criteria(GetType(TrClassAllocation), _
                "TrClass.StartDate", MatchType.GreaterOrEqual, dtCriteriaDate1))
        End If

        Dim dtCriteriaDate2 As DateTime = GenerateCriteriaDate(False)
        If dtCriteriaDate2 <> "1/1/1900" Then
            criterias.opAnd(New Criteria(GetType(TrClassAllocation), _
                "TrClass.StartDate", MatchType.LesserOrEqual, dtCriteriaDate2))
        End If

        If txtCourseCode.Text.Trim() <> "" Then
            Dim nCatFound As Integer = New TrCourseFacade(User).ValidateCode(txtCourseCode.Text.Trim())
            If nCatFound > 0 Then
                nResult = 1
                criterias.opAnd(New Criteria(GetType(TrClassAllocation), _
                    "TrClass.TrCourse.CourseCode", MatchType.Exact, txtCourseCode.Text.Trim()))
            End If
        Else
            nResult = 1
        End If
        'Ditambahkan untuk course sales
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), _
                    "TrClass.TrCourse.JobPositionCategory.ID", MatchType.Exact, 1))
        'Ditambahkan kriteria sbb. agar hanya kategori dan kelas yang aktif saja yang ditampilkan
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), _
            "TrClass.Status", MatchType.Exact, CType(EnumTrDataStatus.DataStatusType.Active, String)))
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), _
            "TrClass.TrCourse.Status", MatchType.Exact, CType(EnumTrDataStatus.DataStatusType.Active, String)))

        Return criterias
    End Function

    Private Function GenerateStatusInSet() As String
        Dim inSet As String = "(" & _
        "'" & CType(EnumTrClassRegistration.DataStatusType.Register, Short).ToString() & "'," & _
        "'" & CType(EnumTrClassRegistration.DataStatusType.Pass, Short).ToString() & "'," & _
        "'" & CType(EnumTrClassRegistration.DataStatusType.Fail, Short).ToString() & "'" & _
        ")"
        Return inSet
    End Function

    Private Function CriteriaForHowManyAlreadyReg(ByVal ClassID As Integer, ByVal DealerID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, ClassID))
        'criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.Dealer.ID", MatchType.Exact, DealerID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Dealer.ID", MatchType.Exact, DealerID))
        'retreive only data with status daftar, if user already set status and lulus, for gagal not counted
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.InSet, GenerateStatusInSet()))
        'add filter khusus untuk yg entry manual
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "EntryType", MatchType.Exact, 0))

        Return criterias
    End Function

    Private Function AggreateForCheckRecord() As Aggregate
        Dim aggregates As New Aggregate(GetType(TrClassRegistration), "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function AllocationRemaining(ByVal objAllocation As TrClassAllocation) As Integer
        Return New HelperFacade(User, GetType(TrClassRegistration)).RecordCount( _
        CriteriaForHowManyAlreadyReg(objAllocation.TrClass.ID, objAllocation.Dealer.ID), _
        AggreateForCheckRecord())
    End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim nResult As Integer = -1
        If (indexPage >= 0) Then
            Dim arlAllocation As ArrayList = New TrClassAllocationFacade(User).RetrieveByCriteria( _
                CreateDefaultCriteria(nResult), _
                indexPage + 1, dtgClassAllocation.PageSize, totalRow, _
                CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            If nResult > 0 Then
                objSessionHelper.SetSession("arlAllocation", arlAllocation)
                dtgClassAllocation.DataSource = arlAllocation
                dtgClassAllocation.VirtualItemCount = totalRow
                dtgClassAllocation.DataBind()
            Else
                MessageBox.Show(SR.DataNotFound("Kategori"))
            End If
        End If
    End Sub

    Private Sub InitInfoAllocation()
        Dim strInfo As String = ""
        Dim ObjDealer As New Dealer
        If Not IsNothing(objSessionHelper.GetSession("DEALER")) Then
            ObjDealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        End If '
        Dim crits As New CriteriaComposite(New Criteria(GetType(v_trClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(v_trClass), "DealerID", MatchType.Exact, ObjDealer.ID))
        crits.opAnd(New Criteria(GetType(v_trClass), "Allocated", MatchType.Greater, 0))
        crits.opAnd(New Criteria(GetType(v_trClass), "NumOfTrainee", MatchType.Lesser, "v_trClass.Allocated"))
        crits.opAnd(New Criteria(GetType(v_trClass), "AreaID", MatchType.Exact, 1))
        crits.opAnd(New Criteria(GetType(v_trClass), "StartDate", MatchType.GreaterOrEqual, Format(Now, "yyyy/MM/dd")))

        Dim arltrClass As New ArrayList
        
        Dim objv_trClassFac As New v_trClassFacade(User)
        arltrClass = objv_trClassFac.Retrieve(crits)

        strInfo = ""
        lblInformation.Text = ""
        If arltrClass.Count > 0 Then
            For Each objv_trclass As v_trClass In arltrClass
                Dim objTrClass As TrClass = New TrClassFacade(User).Retrieve(objv_trclass.ClassID)
                If objTrClass.Status = EnumTrDataStatus.DataStatusType.Active Then
                    Dim strUrlReg As String = "../Training/FrmPendaftaranSales2.aspx?" & _
                                        "year=" & Format(objv_trclass.StartDate, "yyyy") & "&month=all&cat=" & _
                                        "&allocid=" & objv_trclass.ID

                    Dim strUrlCancel As String = "FrmTrClassAllocationCancel.aspx?ID=" & objv_trclass.ID & "&Opener=FrmPendaftaranSales.aspx"

                    strInfo = strInfo & IIf(strInfo = "", "", "; ") & objv_trclass.ClassCode & "[" & "<a href='" & strUrlReg & "'>Register</a>|<a href='" & strUrlCancel & "'>Cancel Alloc.</a>]"
                End If
            Next
            If strInfo <> "" Then
                lblInformation.Text = "Mohon mendaftar untuk kelas yang sudah dialokasikan berikut : " & strInfo
                objSessionHelper.SetSession("AllocationInfo", lblInformation.Text)
            End If
        End If
    End Sub


#Region "Event Handler"
    Private Sub dtgClassAllocation_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClassAllocation.ItemDataBound
        If e.IsRowItems Then
            BindDataColumnText(e)
        End If
    End Sub

    Private Sub BindDataColumnText(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        CType(e.Item.FindControl("lblNo"), Label).Text = _
            e.Item.ItemIndex + 1 + (dtgClassAllocation.CurrentPageIndex * dtgClassAllocation.PageSize)

        Dim arlAllocation As ArrayList = CType(Session.Item("arlAllocation"), ArrayList)
        If arlAllocation.Count > 0 Then
            Dim objAllocation As TrClassAllocation = CType(arlAllocation(e.Item.ItemIndex), TrClassAllocation)
            CType(e.Item.FindControl("lblAllocationRemaining"), Label).Text = _
                (objAllocation.Allocated - AllocationRemaining(objAllocation)).ToString()

            Dim lnk As HyperLink = CType(e.Item.FindControl("lnkClass"), HyperLink)
            If Not lnk Is Nothing Then
                lnk.Text = objAllocation.TrClass.ClassCode
                lnk.NavigateUrl = "javascript:popUpClassInformation('" & objAllocation.TrClass.ClassCode & "');"
            End If
            If objAllocation.Allocated < 1 Then 'if objAllocation.Allocated - AllocationRemaining(objAllocation) < 1 then
                Dim lbtnDetail As LinkButton = e.Item.FindControl("lbtnDetail")
                If Not IsNothing(lbtnDetail) Then
                    lbtnDetail.Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub dtgClassAllocation_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClassAllocation.PageIndexChanged
        dtgClassAllocation.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgClassAllocation.CurrentPageIndex)
    End Sub

    Private Sub dtgClassAllocation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClassAllocation.SortCommand
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

        dtgClassAllocation.CurrentPageIndex = 0
        BindDataGrid(dtgClassAllocation.CurrentPageIndex)
    End Sub

    Private Sub dtgClassAllocation_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgClassAllocation.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "detail"
                Dim arlAllocation As ArrayList = CType(Session.Item("arlAllocation"), ArrayList)
                Dim objAllocation As TrClassAllocation = CType(arlAllocation(e.Item.ItemIndex), TrClassAllocation)
                Dim strYear As String = "all"
                Dim strMonth As String = "all"
                Dim strCat As String = "all"
                If ddlYear.SelectedValue <> "" Then
                    strYear = ddlYear.SelectedValue.Trim()
                End If
                If ddlMonth.IsSelected Then
                    strMonth = ddlMonth.SelectedValue.Trim()
                End If
                If txtCourseCode.IsNotEmpty Then
                    strCat = txtCourseCode.Text.Trim()
                End If
                Dim AlocRemaining As Label = CType(e.Item.FindControl("lblAllocationRemaining"), Label)

                If objAllocation.TrClass.StartDate >= Today Then
                    Dim strUrl As String = "../Training/FrmPendaftaranSales2.aspx?" & _
                        "year=" & strYear & "&month=" & strMonth & "&cat=" & strCat & _
                        "&allocid=" & objAllocation.ID
                    Response.Redirect(strUrl)
                Else
                    MessageBox.Show("Tidak bisa daftar karena kelas sudah mulai")
                End If
            Case "lihat"
                Dim strYear As String = "all"
                Dim strMonth As String = "all"
                Dim strCat As String = "all"
                If ddlYear.SelectedValue <> "" Then
                    strYear = ddlYear.SelectedValue.Trim()
                End If
                If ddlMonth.IsSelected Then
                    strMonth = ddlMonth.SelectedValue.Trim()
                End If
                If txtCourseCode.IsNotEmpty Then
                    strCat = txtCourseCode.Text.Trim()
                End If
                Dim dealer As Dealer = Me.GetDealer()
                Dim hpl As HyperLink = CType(e.Item.FindControl("lnkClass"), HyperLink)
                Dim url As String = "FrmPendaftaranSalesDetail.aspx?classcode={0}&dealercode={1}&year={2}&month={3}&cat={4}"

                Response.Redirect(String.Format(url, hpl.Text, dealer.DealerCode, strYear, strMonth, strCat))
        End Select

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'Modifikasi untuk bug fix berikut
        'Pd saat cari data dan menampilkan data dg 2 page lalu ke page 2 
        'kemudian cari dengan kriteria bulan yang lain keluar error message 'Invalid CurrentPageIndex value. It must be >= 0 and < the PageCount.' 

        dtgClassAllocation.CurrentPageIndex = 0
        BindDataGrid(dtgClassAllocation.CurrentPageIndex)

        InitInfoAllocation()
    End Sub
#End Region

End Class
