Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SAP


Public Class FrmSAPSalesmanValidation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtSAPNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents txtSalesCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSalesName As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnProcess As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgSAPSalesmanValidation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlcategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchSAP As System.Web.UI.WebControls.Label
    Protected WithEvents lblDateFrom As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDateUntil As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Variable Declaration"
    Dim arlSAP As ArrayList = New ArrayList
    Private sHelper As New SessionHelper
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            CommonFunction.BindJobPosition(ddlcategori, User, True, False)
            viewstate.Add("currentSortCol", "ID")
            ViewState.Add("currrentSortDirection", Sort.SortDirection.ASC)

            Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")

            btnProcess.Attributes("onclick") = "return GetSelectedItem();"

            If objUserInfo.Dealer.Title <> 1 Then
                lblDealerCode.Text = objUserInfo.Dealer.DealerCode
                'txtDealerCode.Text = objUserInfo.Dealer.DealerCode
                'txtDealerCode.Enabled = False
            Else
                lblDealerCode.Text = objUserInfo.Dealer.DealerCode
                lblSearchSAP.Attributes("onclick") = "ShowPPSAP();"
                'txtDealerCode.Enabled = True
            End If

        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        bindData(0)
        If arlSAP.Count > 0 Then
            btnProcess.Enabled = True
        Else
            MessageBox.Show(SR.DataNotFound("SAP REGISTER"))
            btnProcess.Enabled = False
        End If
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim _arToProcess As ArrayList = New ArrayList
        Dim _objSAPRegister As SAPRegister = New SAPRegister
        Dim _objSAPPeriod As SAPPeriod = New SAPPeriod
        Dim nresult As Integer = 0
        Dim nSuccess As Integer = 0
        Dim ntotal As Integer = 0

        For Each item As DataGridItem In dtgSAPSalesmanValidation.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If (chk.Checked) Then
                Dim lblID As Label = CType(item.FindControl("lblID"), Label)


                _objSAPRegister = New SAPRegisterFacade(User).Retrieve(Convert.ToInt32(lblID.Text))
                _objSAPPeriod = New SAPPeriodFacade(User).Retrieve(CInt(_objSAPRegister.SAPPeriod.ID))
                If _objSAPPeriod.ID > 0 Then

                    Dim dtData As DateTime
                    If _objSAPPeriod.EndConfirmedDate > "1/1/1973" Then
                        dtData = CDate(_objSAPPeriod.EndConfirmedDate.ToString("dd/MM/yyyy") & " " & _objSAPPeriod.EndConfirmHour)
                        If dtData > Now() Then
                            _objSAPRegister.IsCancelled = 1
                            _arToProcess.Add(_objSAPRegister)
                            nSuccess = nSuccess + 1
                        End If
                    End If
                End If
                ntotal = ntotal + 1
            End If
        Next
        If _arToProcess.Count > 0 Then
            Try
                nresult = New SAPRegisterFacade(User).Update(_arToProcess)
                If nresult > 0 Then
                    MessageBox.Show(SR.UpdateSucces & " : " & nSuccess.ToString & " dari " & ntotal.ToString)
                Else
                    MessageBox.Show(SR.UpdateFail)
                End If
            Catch ex As Exception
                MessageBox.Show(SR.UpdateFail)
            End Try
        Else
            MessageBox.Show("Gagal Update --- Tanggal Konfirmasi Sudah Lewat / Tidak Valid")
        End If

    End Sub

    Private Sub dtgSAPSalesmanValidation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSAPSalesmanValidation.SortCommand
        If CType(ViewState("currentSortCol"), String) = e.SortExpression Then
            Select Case CType(ViewState("currrentSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currrentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currrentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortCol") = e.SortExpression
            ViewState("currrentSortDirection") = Sort.SortDirection.ASC
        End If
        bindData(dtgSAPSalesmanValidation.CurrentPageIndex)
    End Sub


#End Region

#Region "Custom Method"
    Private Sub bindData(ByVal curPageIndex As Integer)
        Dim _totalRow As Integer

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SAPRegister), "IsCancelled", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If (txtSAPNo.Text <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.SAPNumber", MatchType.[Partial], txtSAPNo.Text.Trim))
        End If

        If ddlcategori.SelectedValue <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.JobPosition", MatchType.Exact, ddlcategori.SelectedValue))
        End If

        If txtSalesCode.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.SalesmanCode", MatchType.[Partial], txtSalesCode.Text.Trim))
        End If

        If txtSalesName.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.Name", MatchType.[Partial], txtSalesName.Text.Trim))
        End If


        arlSAP = New SAPRegisterFacade(User).RetrieveActiveList(curPageIndex + 1, dtgSAPSalesmanValidation.PageSize, _
        _totalRow, CType(ViewState("currentSortCol"), String), CType(ViewState("currrentSortDirection"), String), criterias)

        dtgSAPSalesmanValidation.DataSource = arlSAP
        dtgSAPSalesmanValidation.DataBind()

    End Sub

#End Region




   
   
End Class
