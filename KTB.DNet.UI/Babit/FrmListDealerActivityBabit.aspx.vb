Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BabitSalesComm
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security


Public Class FrmListDealerActivityBabit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealers As System.Web.UI.WebControls.Label
    Protected WithEvents txtBabitActivity As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStartPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlEndPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgActivityPlanning As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlTahun As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlTahunTo As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Dim sHelper As New SessionHelper
    Private objDealer As Dealer
    Dim criterias As CriteriaComposite
    Dim intMode As Integer
#End Region

#Region "Custom Method"

    Private Sub BindToGrid(ByVal indexpage As Integer)
        Dim totalrow As Integer
        Dim intMode As Integer
        'If indexpage >= 0 Then
        If (IsNothing(sHelper.GetSession("criteriaBabit"))) Then
            'dtgActivityPlanning.DataSource = Nothing
            'dtgActivityPlanning.DataBind()
            'MessageBox.Show("Kode dealer tidak valid.")
            intMode = CreateCriteria()
            Dim arlActivity As ArrayList = New DealerActivityPlanningFacade(User).RetrieveActiveList(CType(sHelper.GetSession("criteriaBabit"), CriteriaComposite), indexpage + 1, dtgActivityPlanning.PageSize, totalrow, viewstate("SortColBABIT"), viewstate("SortDirectionBABIT"))

            dtgActivityPlanning.VirtualItemCount = totalrow
            If arlActivity.Count > 0 Then

                If (ddlStartPeriod.SelectedValue <> "-1" And ddlTahun.SelectedValue <> "-1" And ddlEndPeriod.SelectedValue <> "-1" And ddlTahunTo.SelectedValue <> "-1") Then
                    Dim dtmFrom As DateTime = New DateTime(CInt(ddlTahun.SelectedValue), CInt(ddlStartPeriod.SelectedValue), 1)
                    Dim dtmTo As DateTime = New DateTime(CInt(ddlTahunTo.SelectedValue), CInt(ddlEndPeriod.SelectedValue), 1)
                    Dim n As Integer = 0
                    Dim arl As New ArrayList
                    For Each obj As DealerActivityPlanning In arlActivity
                        Dim dtmFrom2 As DateTime = New DateTime(obj.PeriodYear, obj.StartPeriodMonth, 1)
                        Dim dtmTo2 As DateTime = New DateTime(obj.PeriodYearEnd, obj.EndPeriodMonth, 2)
                        If (dtmFrom2 <= dtmFrom) And (dtmTo2 >= dtmTo) Then
                            arl.Add(obj)
                        Else
                            n += 1
                        End If
                    Next
                    If (n >= arlActivity.Count) Then
                        dtgActivityPlanning.DataSource = New ArrayList
                        dtgActivityPlanning.DataBind()
                        MessageBox.Show("Data tidak ditemukan")
                        Return
                    End If
                    If IsNothing(arl) Then
                        dtgActivityPlanning.DataSource = New ArrayList
                        dtgActivityPlanning.DataBind()
                        MessageBox.Show("Data tidak ditemukan")
                        Return
                    End If
                End If
                dtgActivityPlanning.DataSource = arlActivity
            Else
                dtgActivityPlanning.DataSource = New ArrayList
                dtgActivityPlanning.DataBind()
                MessageBox.Show("Data tidak ditemukan")
                Return
            End If

            If indexpage = 0 Then
                dtgActivityPlanning.CurrentPageIndex = 0
            End If

            dtgActivityPlanning.DataBind()
        Else
            Dim arlActivity As ArrayList = New DealerActivityPlanningFacade(User).RetrieveActiveList(CType(sHelper.GetSession("criteriaBabit"), CriteriaComposite), indexpage + 1, dtgActivityPlanning.PageSize, totalrow, viewstate("SortColBABIT"), viewstate("SortDirectionBABIT"))

            dtgActivityPlanning.VirtualItemCount = totalrow
            If arlActivity.Count > 0 Then
                dtgActivityPlanning.DataSource = arlActivity
            Else
                dtgActivityPlanning.DataSource = New ArrayList
                MessageBox.Show("Data tidak ditemukan")
            End If

            If indexpage = 0 Then
                dtgActivityPlanning.CurrentPageIndex = 0
            End If
            dtgActivityPlanning.DataBind()
        End If

        'End If
    End Sub
    Public Function CreateCriteria() As Integer
        Dim ht As New Hashtable

        'objDealer = Session("DEALER")
        Dim mode As Integer = 1
        If IsNothing(sHelper.GetSession("criteriaBabit")) Then
            criterias = New CriteriaComposite(New Criteria(GetType(DealerActivityPlanning), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            'If txtDealerCode.Text <> String.Empty Then
            '    criterias.opAnd(New Criteria(GetType(DealerActivityPlanning), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
            'End If

            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If (txtDealerCode.Text.Trim <> String.Empty) Then
                    criterias.opAnd(New Criteria(GetType(DealerActivityPlanning), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
                    ht.Add("DealerTitle", txtDealerCode.Text)
                End If
            Else
                If (txtDealerCode.Text.Trim <> String.Empty) Then
                    If New DataOwner().IsdealerExistInGroup(txtDealerCode.Text.Trim, objDealer) Then
                        criterias.opAnd(New Criteria(GetType(DealerActivityPlanning), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
                        ht.Add("DealerTitle", txtDealerCode.Text)
                    Else
                        ht.Add("DealerTitle", "")
                        Return 0
                    End If
                Else
                    Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
                    criterias.opAnd(New Criteria(GetType(DealerActivityPlanning), "Dealer.DealerCode", MatchType.InSet, strCrit))
                    ht.Add("DealerTitle", strCrit)
                End If
            End If


            If txtBabitActivity.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(DealerActivityPlanning), "BabitActivitiy", MatchType.[Partial], txtBabitActivity.Text))
                ht.Add("BabitActivitiy", txtBabitActivity.Text)
            Else
                ht.Add("BabitActivitiy", "")
            End If

            If (ddlStartPeriod.SelectedValue <> "-1") Then
                criterias.opAnd(New Criteria(GetType(DealerActivityPlanning), "StartPeriodMonth", MatchType.LesserOrEqual, ddlStartPeriod.SelectedValue))
                ht.Add("StartPeriod", ddlStartPeriod.SelectedValue)
            Else
                ht.Add("StartPeriod", 0)
            End If

            'If (ddlEndPeriod.SelectedValue <> "-1") Then
            '    criterias.opAnd(New Criteria(GetType(DealerActivityPlanning), "EndPeriodMonth", MatchType.GreaterOrEqual, ddlEndPeriod.SelectedValue))
            '    ht.Add("EndPeriod", ddlEndPeriod.SelectedValue)
            'Else
            '    ht.Add("EndPeriod", 0)
            'End If

            If (ddlTahun.SelectedValue <> "-1") Then
                criterias.opAnd(New Criteria(GetType(DealerActivityPlanning), "PeriodYear", MatchType.LesserOrEqual, ddlTahun.SelectedValue))
                ht.Add("PeriodYear", ddlTahun.SelectedValue)
            Else
                ht.Add("PeriodYear", 0)
            End If

            If (ddlTahunTo.SelectedValue <> "-1") Then
                criterias.opAnd(New Criteria(GetType(DealerActivityPlanning), "PeriodYearEnd", MatchType.GreaterOrEqual, ddlTahunTo.SelectedValue))
                ht.Add("PeriodYearEnd", ddlTahun.SelectedValue)
            Else
                ht.Add("PeriodYearEnd", 0)
            End If

            If ddlEndPeriod.SelectedValue <> "-1" Then
                If (ddlStartPeriod.SelectedValue = "-1") Or (ddlTahun.SelectedValue = "-1") Or (ddlTahunTo.SelectedValue = "-1") Then
                    criterias.opAnd(New Criteria(GetType(DealerActivityPlanning), "EndPeriodMonth", MatchType.GreaterOrEqual, ddlEndPeriod.SelectedValue))
                End If
            End If


        End If

        sHelper.SetSession("criteriaBabit", criterias)
        sHelper.SetSession("CriteriasBack", criterias)
        sHelper.SetSession("SaveHTCriteria", ht)
        Return mode
    End Function

    Private Sub SetBackCriteria()
        Dim ht As Hashtable = CType(sHelper.GetSession("SaveHTCriteria"), Hashtable)
        txtDealerCode.Text = ht.Item("DealerTitle")
        txtBabitActivity.Text = ht.Item("BabitActivitiy")
        If ht.Item("StartPeriod") <> 0 Then
            ddlStartPeriod.SelectedValue = ht.Item("StartPeriod")
        Else
            ddlStartPeriod.SelectedIndex = 0
        End If
        If ht.Item("EndPeriod") <> 0 Then
            ddlEndPeriod.SelectedValue = ht.Item("EndPeriod")
        Else
            ddlEndPeriod.SelectedIndex = 0
        End If

        If ht.Item("PeriodYear") <> 0 Then
            ddlTahun.SelectedValue = ht.Item("PeriodYear")
        Else
            ddlTahun.SelectedIndex = 0
        End If

        If ht.Item("PeriodYearEnd") <> 0 Then
            ddlTahunTo.SelectedValue = ht.Item("PeriodYearEnd")
        Else
            ddlTahunTo.SelectedIndex = 0
        End If

    End Sub

    Private Function GetMonthName(ByVal month As Integer) As String
        Select Case month
            Case 1
                Return "Jan"
            Case 2
                Return "Feb"
            Case 3
                Return "Mar"
            Case 4
                Return "Apr"
            Case 5
                Return "Mei"
            Case 6
                Return "Jun"
            Case 7
                Return "Jul"
            Case 8
                Return "Ags"
            Case 9
                Return "Sep"
            Case 10
                Return "Okt"
            Case 11
                Return "Nov"
            Case 12
                Return "Des"
        End Select
    End Function

    Private Sub BindMonth()
        CommonFunction.BindFromEnum("Month", ddlStartPeriod, User, False, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("Month", ddlEndPeriod, User, False, "NameStatus", "ValStatus")

        ddlStartPeriod.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlEndPeriod.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))

    End Sub

    Private Sub BindTahun()
        ddlTahun.Items.Clear()
        ddlTahun.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahun.Items.Add(New ListItem(i.ToString, i))
        Next

        ddlTahunTo.Items.Clear()
        ddlTahunTo.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For i As Integer = DateTime.Today.Year - 5 To DateTime.Today.Year + 1
            ddlTahunTo.Items.Add(New ListItem(i.ToString, i))
        Next
    End Sub

    Private Sub BindAfterEdit(ByVal indexPage As Integer, ByVal criteriasE As CriteriaComposite)
        Dim totalrow As Integer

        If indexPage >= 0 Then

            Dim arlActivity As ArrayList = New DealerActivityPlanningFacade(User).RetrieveActiveList(criteriasE, indexPage + 1, dtgActivityPlanning.PageSize, totalrow, viewstate("SortColBABIT"), viewstate("SortDirectionBABIT"))
            dtgActivityPlanning.VirtualItemCount = totalrow
            If arlActivity.Count > 0 Then
                dtgActivityPlanning.DataSource = arlActivity
            Else
                dtgActivityPlanning.DataSource = New ArrayList
                MessageBox.Show("Data tidak ditemukan")
            End If

            dtgActivityPlanning.DataBind()
        End If
    End Sub
#End Region
#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.RencanaAkctivitasListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Babit-Daftar Rencana Aktivitas")
        End If
    End Sub

    Private Function blnCekListEditPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.RencanaAkctivitasListEdit_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function blnCekListDeletePrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.RencanaAkctivitasListDelete_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objDealer = CType(sHelper.GetSession("Dealer"), Dealer)
        InitiateAuthorization()
        If Not IsPostBack Then

            If Not objDealer Is Nothing Then
                If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    txtDealerCode.Text = objDealer.DealerCode
                    txtDealerCode.Enabled = False
                    lblDealers.Visible = False
                Else
                    txtDealerCode.Enabled = True
                    lblDealers.Visible = True
                End If
            End If

            Viewstate("SortColBABIT") = "ActivityPlanning"
            viewstate("SortDirectionBABIT") = Sort.SortDirection.ASC
            BindMonth()
            BindTahun()
            lblDealers.Attributes("onclick") = "ShowPPDealerSelection()"

            If Not sHelper.GetSession("IEditMode") = 0 Then
                Dim criteriasE As CriteriaComposite = sHelper.GetSession("CriteriasBack")
                SetBackCriteria()
                BindAfterEdit(0, criteriasE)
            End If
        End If
    End Sub

    Private Sub dtgActivityPlanning_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgActivityPlanning.PageIndexChanged
        'Dim htCek As Hashtable = sHelper.GetSession("SaveHTCriteria")
        'If txtDealerCode.Text <> htCek.Item("DealerTitle") Then
        '    txtDealerCode.Text = htCek.Item("DealerTitle")
        '    dtgActivityPlanning.CurrentPageIndex = e.NewPageIndex
        '    BindToGrid(dtgActivityPlanning.CurrentPageIndex)
        'End If
        dtgActivityPlanning.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgActivityPlanning.CurrentPageIndex)
    End Sub

    Private Sub dtgActivityPlanning_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgActivityPlanning.SortCommand
        If e.SortExpression = viewstate("SortColBABIT") Then
            If viewstate("SortDirectionBABIT") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirectionBABIT", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirectionBABIT", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColBABIT", e.SortExpression)
        BindToGrid(0)
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgActivityPlanning.CurrentPageIndex = 0
        'If (CInt(ddlStartPeriod.SelectedValue) <> -1 And CInt(ddlEndPeriod.SelectedValue) <> -1) Then

        '    If CInt(ddlStartPeriod.SelectedValue) > CInt(ddlEndPeriod.SelectedValue) Then
        '        MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
        '        Exit Sub
        '    End If

        'End If
        If (CInt(ddlTahun.SelectedValue) <> -1 And CInt(ddlTahunTo.SelectedValue) <> -1) Then
            If (CInt(ddlTahun.SelectedValue) = CInt(ddlTahunTo.SelectedValue)) Then
                If (CInt(ddlStartPeriod.SelectedValue) <> -1 And CInt(ddlEndPeriod.SelectedValue) <> -1) Then
                    If CInt(ddlStartPeriod.SelectedValue) > CInt(ddlEndPeriod.SelectedValue) Then
                        MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
                        Exit Sub
                    End If
                End If
            ElseIf (CInt(ddlTahun.SelectedValue) > CInt(ddlTahunTo.SelectedValue)) Then
                MessageBox.Show("Periode awal harus lebih kecil dari akhir periode")
                Exit Sub
            End If
        End If

        intMode = CreateCriteria()
        BindToGrid(dtgActivityPlanning.CurrentPageIndex)
    End Sub

    Private Sub dtgActivityPlanning_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgActivityPlanning.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            'set number
            Dim lblNO As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNO.Text = (e.Item.ItemIndex + 1 + (dtgActivityPlanning.CurrentPageIndex * dtgActivityPlanning.PageSize)).ToString

            'get month name
            Dim lblStartPeriode As Label = CType(e.Item.FindControl("lblStartPeriode"), Label)
            Dim strStartTempperiod As String = GetMonthName(lblStartPeriode.Text)
            lblStartPeriode.Text = strStartTempperiod

            Dim lblEndPeriode As Label = CType(e.Item.FindControl("lblEndPeriode"), Label)
            Dim strEndTempperiod As String = GetMonthName(lblEndPeriode.Text)
            lblEndPeriode.Text = strEndTempperiod

            'get filename
            Dim lbtnActivityPlan As LinkButton = CType(e.Item.FindControl("lbtnActivityPlan"), LinkButton)
            Dim strFile As String = lbtnActivityPlan.CommandArgument.ToString
            lbtnActivityPlan.Text = strFile.Substring(strFile.LastIndexOf("\") + 1)

            ' add security
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            If Not blnCekListEditPrivilege() Then
                lbtnEdit.Visible = False
            End If
            If Not blnCekListDeletePrivilege() Then
                lbtnDelete.Visible = False
            End If
        End If
    End Sub

    Private Sub dtgActivityPlanning_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgActivityPlanning.ItemCommand
        If e.CommandName = "Download" Then
            Response.Redirect("../Download.aspx?file=" & e.CommandArgument)
        ElseIf e.CommandName = "Edit" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim objDealerActivity As DealerActivityPlanning = New DealerActivityPlanningFacade(User).Retrieve(CInt(lblID.Text))
            sHelper.SetSession("IEditMode", 1)
            sHelper.SetSession("objDealerActivity", objDealerActivity)
            Response.Redirect("FrmActivityPlanBabit.aspx")
        ElseIf e.CommandName = "Delete" Then
            sHelper.SetSession("IEditMode", 0)
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim objDealerActivity As DealerActivityPlanning = New DealerActivityPlanningFacade(User).Retrieve(CInt(lblID.Text))

            Try
                Dim dapFacade As New DealerActivityPlanningFacade(User)
                dapFacade.Delete(objDealerActivity)
                BindToGrid(0)
            Catch ex As Exception
                MessageBox.Show("Gagal melakukan hapus data")
            End Try
        End If
    End Sub
End Class
