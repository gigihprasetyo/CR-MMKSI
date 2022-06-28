Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.Domain
Imports KTB.DNet.Security

Public Class FrmCcCSPerformanceClasification
    Inherits System.Web.UI.Page

    Private _sessHelper As SessionHelper = New SessionHelper
    Dim IsAllowToEdit As Boolean
    Dim IsAllowToRead As Boolean
    Dim objCSPParameter As CcCSPerformanceSubParameter


#Region "handler"

    Private Sub dtgCSPParameter_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgCSPParameter.SortCommand
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
        dtgCSPParameter.SelectedIndex = -1
        BindDataGrid(dtgCSPParameter.CurrentPageIndex)
    End Sub

    'Private Sub ddlReferral_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReferral.SelectedIndexChanged
    '    If ddlReferral.SelectedValue <> "" Then
    '        Dim objCSPParameter As CcCSPerformanceSubParameter
    '        objCSPParameter = New CcCSPerformanceSubParameterFacade(User).Retrieve(CType(ddlReferral.SelectedValue, Integer))

    '        txtParameterName.Text = objCSPParameter.Name
    '        txtParameterWeight.Text = objCSPParameter.Weight
    '        'txtSequence.Text = objCSPParameter.Sequence
    '        lblKodeParameter.Text = objCSPParameter.CcCSPerformanceMaster.Code
    '        'ddlTipe.SelectedValue = objCSPParameter.Type
    '        ViewState("CSPParameter") = "Insert"

    '    End If

    'End Sub

    Protected Sub btnBaru_Click(sender As Object, e As EventArgs) Handles btnBaru.Click
        ClearData()
    End Sub


    Private Sub dtgCSPParameter_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgCSPParameter.ItemDataBound

        If e.Item.ItemIndex <> -1 Then
            Dim model As CcCSPerformanceClasification = CType(e.Item.DataItem, CcCSPerformanceClasification)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            Dim lblKodeParameter As Label = CType(e.Item.FindControl("lblKodeParameter"), Label)
            Dim isEdit As Boolean = GetCSPMaster(model.CcCSPerformanceMasterID)
            Dim lbtnGotoClasification As LinkButton = CType(e.Item.FindControl("lbtnGotoClasification"), LinkButton)

            lblKodeParameter.Text = model.CcCSPerformanceSubParameter.Name
            lbtnEdit.Visible = isEdit
            lbtnDelete.Visible = isEdit
            lbtnGotoClasification.Visible = False

        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCSPParameter.CurrentPageIndex * dtgCSPParameter.PageSize)
        End If
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Server.Transfer("FrmCcCSPerformanceSubParameter.aspx?")
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click

        Dim model As New CcCSPerformanceMaster
        Dim nResult As Integer

        If Not isEmptyText() Then
            Select Case CType(ViewState("CSPParameter"), String)
                Case "Insert"
                    nResult = InsertData()
                Case "Update"
                    nResult = UpdateData()
            End Select

            If nResult > 0 Then
                MessageBox.Show(SR.SaveSuccess)
                ClearData()
                DisplayDataGrid()
            Else
                MessageBox.Show(SR.SaveFail)
            End If

        Else

            If Not (Page.IsValid) Then
                MessageBox.Show("Masih Ada yang Belum Lengkap")
            End If

        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ActivateUserPrivilege()
        ddlReferral.Visible = False
        If Not IsPostBack Then
            Dim Id As Integer

            ClearData()
            'BindDDLTipe()

            ' ini untuk handle tombol gotosubparameter di form parameter
            Id = CInt(Request.QueryString("SubID"))
            If CInt(Request.QueryString("SubID")) > 0 Or Request.QueryString("SubID") IsNot Nothing Then
                objCSPParameter = New CcCSPerformanceSubParameter
                objCSPParameter = New CcCSPerformanceSubParameterFacade(User).Retrieve(Id)
                lblKodeSubParameter.Text = objCSPParameter.Name
                ViewState("SubPar") = objCSPParameter.Code
                ViewState("SubParID") = CInt(Request.QueryString("SubID"))
            End If


            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            DisplayDataGrid()
            'BindRefferal()

        End If


    End Sub


    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        If isEmptyText() Then
            DisplayDataGrid()
        Else
            MessageBox.Show("Data Masih Belum Lengkap")
        End If

    End Sub

    Private Sub dtgCSPParameter_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgCSPParameter.ItemCommand
        If e.CommandName = "View" Then
            ViewDataCSPParameter(Val(e.Item.Cells(0).Text), False)
            SetTextboxIsEnable(False)
        ElseIf e.CommandName = "Delete" Then
            DeletecccsperformanceSubparameter(Val(e.Item.Cells(0).Text))
        ElseIf e.CommandName = "Edit" Then
            ViewState("CSPParameter") = "Update"
            ViewState("StateEdit") = Val(e.Item.Cells(0).Text)
            ViewDataCSPParameter(Val(e.Item.Cells(0).Text), True)
            SetTextboxIsEnable(True)
            'ElseIf e.CommandName = "GotoSubParameter" Then
            '    Dim strCSPParameterID As String = e.Item.Cells(0).Text
            '    Dim objCSPSubParameter As CcCSPerformanceSubParameter = New CcCSPerformanceSubParameterFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
            '    _sessHelper.SetSession("CSPSubPar", objCSPSubParameter)
            '    Server.Transfer("FrmCcCSPerformanceClasification.aspx?SubID=" & strCSPParameterID)
        End If
    End Sub

#End Region

#Region "custom Method"

    'Private Sub BindDDLTipe()
    '    CommonFunction.BindEnumDetailToDDL(ddlTipe, "CSP_SUBPAR_TYPE")
    'End Sub

    'Private Sub BindRefferal()
    '    Dim arrList As New ArrayList
    '    Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "CcCSPerformanceMaster.Code", MatchType.Exact, lblKodeSubParameter.Text))
    '    Dim dataResult = New CcCSPerformanceSubParameterFacade(User).Retrieve(criterias)

    '    ddlReferral.DataSource = dataResult
    '    ddlReferral.DataTextField = "Code"
    '    ddlReferral.DataValueField = "ID"
    '    ddlReferral.DataBind()

    '    ddlReferral.Items.Insert(0, New ListItem("", ""))
    'End Sub


    Private Sub ClearData()

        txtParameterWeight.Text = 0
        txtParameterWeight.Enabled = True

        txtParameterName.Text = ""
        txtParameterName.Enabled = True

        'lblKodeParameter.Text = ""

        'txtSequence.Text = ""
        'txtSequence.Enabled = True

        'ddlTipe.Enabled = True

        ddlReferral.Enabled = True
        btnSimpan.Enabled = True
        ViewState("CSPParameter") = "Insert"
    End Sub

    Private Sub DeletecccsperformanceSubparameter(ByVal Id As Integer)

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Dim objCSPSubParCode As String
            objCSPSubParCode = CType(ViewState("SubPar"), String)

            Dim model As CcCSPerformanceClasification = New CcCSPerformanceClasificationFacade(User).Retrieve(Id)
            Dim facade As CcCSPerformanceClasificationFacade = New CcCSPerformanceClasificationFacade(User)
            Dim nResult As Integer

            model.RowStatus = -1
            model.CcCSPerformanceSubParameterID = New CcCSPerformanceSubParameterFacade(User).Retrieve(objCSPSubParCode)
            model.Code = lblKodeKlasifikasi.Text
            model.Weight = CInt(txtParameterWeight.Text)
            model.Name = txtParameterName.Text
            nResult = facade.Update(model)

            If nResult > 0 Then
                MessageBox.Show(SR.DeleteSucces)
            Else
                MessageBox.Show(SR.DeleteFail)
            End If

            DisplayDataGrid()
        End If



    End Sub

    Private Function UpdateData() As Integer
        Dim model As New CcCSPerformanceClasification
        Dim Id As Integer = ViewState("StateEdit")
        Dim nresult As Integer = 0
        Dim objCSPSubParCode As String
        objCSPSubParCode = CType(ViewState("SubPar"), String)
        Dim objCSPSubParID As String
        objCSPSubParID = CType(ViewState("SubParID"), Integer)

        model.Name = txtParameterName.Text
        model.Weight = txtParameterWeight.Text
        'model.Sequence = txtSequence.Text
        'model.Type = CType(ddlTipe.SelectedValue, Short)
        'model.CcCSPerformanceSubParameterID = New CcCSPerformanceSubParameterFacade(User).Retrieve(objCSPSubParCode)
        model.CcCSPerformanceSubParameterID = objCSPSubParID
        model.ID = CType(ViewState("StateEdit"), Integer)

        Try
            nresult = New CcCSPerformanceClasificationFacade(User).Update(model)
            ViewState("CurrentSortColumn") = "LastUpdateTime"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        Catch ex As Exception
            MessageBox.Show("Error Has Been Occur : " & ex.ToString)
        End Try
        Return nresult


    End Function

    Private Function ViewDataCSPParameter(ByVal id As Integer, ByVal isEnable As Boolean)
        Dim objModel As CcCSPerformanceClasification = New CcCSPerformanceClasificationFacade(User).Retrieve(id)

        If Not objModel Is Nothing Then
            'lblKodeParameter.Text = objModel.CcCSPerformancesubParameter.Code
            lblKodeKlasifikasi.Text = objModel.Code
            txtParameterName.Text = objModel.Name
            'txtSequence.Text = objModel.Sequence
            txtParameterWeight.Text = objModel.Weight
        End If

        btnSimpan.Enabled = isEnable
    End Function

    Private Sub SetTextboxIsEnable(ByVal isEnable As Boolean)

        txtParameterName.Enabled = isEnable
        txtParameterWeight.Enabled = isEnable
        'txtSequence.Enabled = isEnable
    End Sub


  
    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.CSP_Input_Master_Privilage) Then
            If Not SecurityProvider.Authorize(Context.User, SR.CSP_View_Master_Privilage) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=CS Performance Master Parameter")
            End If
            IsAllowToRead = True
            IsAllowToEdit = False
        Else
            IsAllowToEdit = True
        End If
    End Sub

    Private Sub DisplayDataGrid()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceClasification), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        _sessHelper.SetSession("CSPClasification", criterias)
        dtgCSPParameter.CurrentPageIndex = 0
        BindDataGrid(dtgCSPParameter.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim arrList As ArrayList
        Dim totalRow As Integer = 0

        If indexPage >= 0 Then
            arrList = New CcCSPerformanceClasificationFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("CSPClasification"), CriteriaComposite),
                                                                                  indexPage + 1, dtgCSPParameter.PageSize, totalRow, _
                                                                                   CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgCSPParameter.VirtualItemCount = totalRow
            dtgCSPParameter.CurrentPageIndex = indexPage
            dtgCSPParameter.DataSource = arrList
            dtgCSPParameter.DataBind()

        End If
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        Dim objCSPSubParCode As String
        Dim objCSPSubParID As Integer
        objCSPSubParCode = CType(ViewState("SubPar"), String)
        objCSPSubParID = CType(ViewState("SubParID"), Integer)
        criterias.opAnd(New Criteria(GetType(CcCSPerformanceClasification), "CcCSPerformanceSubParameter.ID", MatchType.Exact, objCSPSubParID))
        criterias.opAnd(New Criteria(GetType(CcCSPerformanceClasification), "CcCSPerformanceSubParameter.Code", MatchType.Exact, objCSPSubParCode))

        If Not String.IsNullOrEmpty(txtParameterName.Text) Then
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceClasification), "Name", MatchType.Partial, txtParameterName.Text))
        End If

        If CInt(txtParameterWeight.Text) > 0 Then
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceClasification), "Weight", MatchType.Exact, CDec(Val(txtParameterWeight.Text))))
        End If

        'If Not String.IsNullOrEmpty(txtSequence.Text) Then
        '    criterias.opAnd(New Criteria(GetType(CcCSPerformanceClasification), "Sequence", MatchType.Exact, Val(txtParameterName.Text)))
        'End If

        'If ddlTipe.SelectedIndex > 0 Then
        '    criterias.opAnd(New Criteria(GetType(CcCSPerformanceSubParameter), "Type", MatchType.Exact, CType(ddlTipe.SelectedValue, Short)))
        'End If
    End Sub

    Private Function GetCSPMaster(ByVal CSPMasterID As String) As Boolean
        Dim id As Integer
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceResultHeader), "CcCSPerformanceMasterID", MatchType.Exact, CSPMasterID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceResultHeader), "RowStatus", MatchType.Exact, 0))

        Dim idResult As ArrayList = New CcCSPerformanceResultHeaderFacade(User).Retrieve(criterias)

        If idResult.Count > 0 Then
            Return False
        End If
        Return True
    End Function

    Private Function ReceiveIDFromDAMaster() As String
        Dim Id As String
        Id = _sessHelper.GetSession("SEND_FORMCODE")
        Return Id
    End Function

    Private Function InsertData() As Integer
        Dim objModel As New CcCSPerformanceClasification
        Dim nresult As Integer = 0
        Dim objCSPSubParCode As String
        objCSPSubParCode = CType(ViewState("SubPar"), String)
        Dim objCSPSubParID As String
        objCSPSubParID = CType(ViewState("SubParID"), Integer)


        'If Not CheckCategoryCodeIsAlreadyExist(txtCategoryCode.Text) Then
        'objModel.CcCSPerformanceSubParameterID = New CcCSPerformanceSubParameterFacade(User).Retrieve(objCSPSubParCode)
        objModel.CcCSPerformanceSubParameterID = objCSPSubParID
        objModel.Name = txtParameterName.Text
        objModel.Weight = CDec(txtParameterWeight.Text)

        Try
            nresult = New CcCSPerformanceClasificationFacade(User).Insert(objModel)
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        Catch ex As Exception
            MessageBox.Show("Error Has Been Occur : " & ex.ToString)
        End Try
        Return nresult
    End Function

    Private Function isEmptyText() As Boolean
        Dim isEmpty As Boolean = False

        If String.IsNullOrEmpty(txtParameterName.Text) Then
            Return True
        End If


        'If String.IsNullOrEmpty(txtSequence.Text) Then
        '    Return True
        'End If

        If String.IsNullOrEmpty(txtParameterWeight.Text) Then
            Return True
        End If

        'If ddlTipe.SelectedIndex = 0 Then
        '    Return True
        'End If

        Return isEmpty
    End Function

#End Region


End Class