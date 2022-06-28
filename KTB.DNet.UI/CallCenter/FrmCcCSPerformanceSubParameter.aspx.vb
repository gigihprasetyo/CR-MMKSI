Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports System.Collections.Generic
Imports System.Linq

Public Class FrmCcCSPerformanceSubParameter
    Inherits System.Web.UI.Page

    Private _sessHelper As SessionHelper = New SessionHelper
    Dim IsAllowToEdit As Boolean
    Dim IsAllowToRead As Boolean
    Dim objCSPParameter As CcCSPerformanceParameter

    Private Const SESS_LIST_ATTRIBUTE As String = "SessListSubParameterAttribute"

    Private Shared _listCcPeriod As ArrayList
    Private ReadOnly Property ListCcPeriod As ArrayList
        Get
            If _listCcPeriod Is Nothing Then
                _listCcPeriod = New ArrayList
                Dim objCSPParameter As CcCSPerformanceParameter
                objCSPParameter = CType(_sessHelper.GetSession("CSPParameterID"), CcCSPerformanceParameter)

                Dim criterias As New CriteriaComposite(New Criteria(GetType(CcPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(CcPeriod), "ID", MatchType.GreaterOrEqual, objCSPParameter.CcCSPerformanceMaster.CcPeriodIDFrom))
                criterias.opAnd(New Criteria(GetType(CcPeriod), "ID", MatchType.LesserOrEqual, objCSPParameter.CcCSPerformanceMaster.CcPeriodIDTo))
                _listCcPeriod = New CcPeriodFacade(User).Retrieve(criterias)

            End If

            Return _listCcPeriod

        End Get
    End Property



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



    Protected Sub btnBaru_Click(sender As Object, e As EventArgs) Handles btnBaru.Click
        ClearData()
    End Sub


    Private Sub dtgCSPParameter_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgCSPParameter.ItemDataBound

        If e.Item.ItemIndex <> -1 Then
            Dim model As CcCSPerformanceSubParameter = CType(e.Item.DataItem, CcCSPerformanceSubParameter)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)

            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            Dim lblKodeParameter As Label = CType(e.Item.FindControl("lblKodeParameter"), Label)
            Dim isEdit As Boolean = GetCSPMaster(model.CcCSPerformanceParameter.CcCSPerformanceMasterID)

            lbtnEdit.Visible = isEdit
            lbtnDelete.Visible = isEdit


            Dim lblTipe As Label = CType(e.Item.FindControl("lblTipe"), Label)
            Dim lblprosedur As Label = CType(e.Item.FindControl("lblprosedur"), Label)

            lblTipe.Text = CommonFunction.GetEnumDescription(model.Type, "CSP_SUBPAR_TYPE")
            lblprosedur.Text = model.FunctionName
            lblKodeParameter.Text = model.CcCSPerformanceParameter.Name


            lbtnView.Visible = True
            If CType(_sessHelper.GetSession("CSPParameterID"), CcCSPerformanceParameter) IsNot Nothing Then

                Dim objCSPParameter As CcCSPerformanceParameter
                objCSPParameter = CType(_sessHelper.GetSession("CSPParameterID"), CcCSPerformanceParameter)
                If objCSPParameter.FunctionName <> 0 Then
                    lbtnEdit.Visible = False
                    lbtnDelete.Visible = False
                    lbtnView.Visible = False
                End If
            End If

            If model.CcCSPerformanceMaster.Status = 0 Then
                lbtnDelete.Visible = False
                lbtnEdit.Visible = False
            End If

            Dim dtgAttribute As DataGrid = CType(e.Item.FindControl("dtgAttribute"), DataGrid)
            AddHandler dtgAttribute.ItemDataBound, New System.Web.UI.WebControls.DataGridItemEventHandler(AddressOf dtgAttribute_ItemDataBound)

            dtgAttribute.DataSource = model.ListOfAttribute
            dtgAttribute.DataBind()

        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCSPParameter.CurrentPageIndex * dtgCSPParameter.PageSize)
        End If
    End Sub

    Private Sub dtgAttribute_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As CcCSPerformanceSubParameterAttribute = CType(e.Item.DataItem, CcCSPerformanceSubParameterAttribute)
            Dim lblTipeKendaraan As Label = CType(e.Item.FindControl("lblTipeKendaraan"), Label)
            Dim lblDeskripsi As Label = CType(e.Item.FindControl("lblDeskripsi"), Label)
            Dim lblFaktor As Label = CType(e.Item.FindControl("lblFaktor"), Label)
            Dim lblMinimumScore As Label = CType(e.Item.FindControl("lblMinimumScore"), Label)
            Dim lblPeriodFrom As Label = CType(e.Item.FindControl("lblPeriodFrom"), Label)
            Dim lblPeriodTo As Label = CType(e.Item.FindControl("lblPeriodTo"), Label)

            lblFaktor.Text = RowValue.CcAttribute.CcFactor.Description
            lblDeskripsi.Text = RowValue.CcAttribute.Description
            lblTipeKendaraan.Text = RowValue.CcAttribute.CcFactor.CcVehicleCategory.Description
            lblMinimumScore.Text = RowValue.MinimumScore

            If Not RowValue.CcPeriodFrom Is Nothing Then
                lblPeriodFrom.Text = RowValue.CcPeriodFrom.YearMonth
            End If

            If Not RowValue.CcPeriodTo Is Nothing Then
                lblPeriodTo.Text = RowValue.CcPeriodTo.YearMonth
            End If




        End If
    End Sub


    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Server.Transfer("FrmCcCSPerformanceParameter.aspx?")
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click

        Dim model As New CcCSPerformanceMaster
        Dim nResult As Integer
        Dim errMsg As String = ""

        If Not isEmptyText(errMsg) Then
            Select Case CType(ViewState("CSPSubParameter"), String)
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

            'If Not (Page.IsValid) Then
            lblErrMsg.Text = errMsg
            'End If
        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ActivateUserPrivilege()
        If Not IsPostBack Then
            Dim Id As Integer


            BindDDLTipe()

            If CType(_sessHelper.GetSession("CSPSubPar"), CcCSPerformanceSubParameter) IsNot Nothing Then
                ' ini untuk handle tombol kembali di form clasification
                Dim objCSPSubParameter As CcCSPerformanceSubParameter
                objCSPSubParameter = CType(_sessHelper.GetSession("CSPSubPar"), CcCSPerformanceSubParameter)
                objCSPParameter = New CcCSPerformanceParameter
                objCSPParameter = New CcCSPerformanceParameterFacade(User).Retrieve(objCSPSubParameter.CcCSPerformanceParameter.ID)
                lblKodeParameter.Text = objCSPParameter.Name
                ViewState("Parameter") = objCSPParameter.Code
                If objCSPParameter.FunctionName = 1 Then
                    ViewState("StateReferenceParameter") = False
                End If
            Else
                ' ini untuk handle tombol gotosubparameter di form parameter
                Id = CInt(Request.QueryString("ParID"))
                If CInt(Request.QueryString("ParID")) > 0 Or Request.QueryString("ParID") IsNot Nothing Then
                    objCSPParameter = New CcCSPerformanceParameter
                    objCSPParameter = New CcCSPerformanceParameterFacade(User).Retrieve(Id)
                    lblKodeParameter.Text = objCSPParameter.Name
                    ViewState("Parameter") = objCSPParameter.Code
                    If objCSPParameter.FunctionName = 1 Then
                        ViewState("StateReferenceParameter") = False
                    End If
                    lblShowAttribute.Attributes("onclick") = "ShowAttribute(" & objCSPParameter.CcCustomerCategoryID & ");"

                    If objCSPParameter.CcCSPerformanceMaster.Status = 0 Then
                        SetTextboxIsEnable(False)
                        btnBaru.Visible = False
                        btnSimpan.Visible = False
                    End If

                End If
            End If
            ClearData()

            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            DisplayDataGrid()


        End If

        'jika dari daeler ranking maka tidak bisa save


    End Sub


    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        'Dim strErrMsg As String = ""
        'If Not isEmptyText(strErrMsg) Then
        '    DisplayDataGrid()
        'Else
        '    lblErrMsg.Text = strErrMsg
        'End If
        If btnCari.Text = "Cari" Then
            DisplayDataGrid()
        Else
            ClearData()
            dtgCSPParameter.SelectedIndex = -1
            ddlTipe.Enabled = True
            btnCari.Text = "Cari"
        End If

    End Sub

    Private Sub dtgCSPParameter_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgCSPParameter.ItemCommand
        If e.CommandName = "View" Then
            Session(SESS_LIST_ATTRIBUTE) = Nothing
            hdnAttributeID.Value = String.Empty
            SetTextboxIsEnable(False)
            ViewDataCSPParameter(Val(e.Item.Cells(1).Text), False)
            dtgCSPParameter.SelectedIndex = e.Item.ItemIndex
            btnCari.Text = "Batal"
        ElseIf e.CommandName = "Delete" Then
            Session(SESS_LIST_ATTRIBUTE) = Nothing
            hdnAttributeID.Value = String.Empty
            DeletecccsperformanceSubparameter(Val(e.Item.Cells(1).Text))
        ElseIf e.CommandName = "Edit" Then
            Session(SESS_LIST_ATTRIBUTE) = Nothing
            hdnAttributeID.Value = String.Empty
            ViewState("CSPSubParameter") = "Update"
            ViewState("StateEditSub") = Val(e.Item.Cells(1).Text)
            SetTextboxIsEnable(True)
            ViewDataCSPParameter(Val(e.Item.Cells(1).Text), True)
            dtgCSPParameter.SelectedIndex = e.Item.ItemIndex
            btnCari.Text = "Batal"
        ElseIf e.CommandName = "GoToAttribute" Then
            Dim strCSPParameterID As String = e.Item.Cells(1).Text
            Dim objCSPSubParameter As CcCSPerformanceSubParameter = New CcCSPerformanceSubParameterFacade(User).Retrieve(CInt(e.Item.Cells(1).Text))
            _sessHelper.SetSession("CSPSubPar", objCSPSubParameter)
            Server.Transfer("FrmCcCSPerformanceSubParameterAttribute.aspx?SubID=" & strCSPParameterID)
        End If
    End Sub

#End Region

#Region "custom Method"

    Private Sub BindDDLTipe()
        CommonFunction.BindEnumDetailToDDL(ddlTipe, "CSP_SUBPAR_TYPE")
    End Sub



    Private Sub ClearData()

        txtParameterWeight.Text = 0
        txtParameterWeight.Enabled = True

        txtParameterName.Text = ""
        txtParameterName.Enabled = True

        lblKodeSubParameter.Text = ""

        txtSequence.Text = ""
        txtSequence.Enabled = True

        lblErrMsg.Text = ""

        BindDDLTipe()


        btnSimpan.Enabled = True
        ViewState("CSPSubParameter") = "Insert"



        If ViewState("StateReferenceParameter") IsNot Nothing Then
            btnSimpan.Enabled = CType(ViewState("StateReferenceParameter"), Boolean)
        End If

        lblShowAttribute.Visible = True
        txtProcedure.Enabled = True
        txtProcedure.Text = String.Empty

        Session(SESS_LIST_ATTRIBUTE) = Nothing
        hdnAttributeID.Value = String.Empty
        gvAttribute.DataSource = Nothing
        gvAttribute.DataBind()



    End Sub

    Private Sub DeletecccsperformanceSubparameter(ByVal Id As Integer)

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Dim model As CcCSPerformanceSubParameter = New CcCSPerformanceSubParameterFacade(User).Retrieve(Id)
            Dim facade As CcCSPerformanceSubParameterFacade = New CcCSPerformanceSubParameterFacade(User)
            Dim nResult As Integer

            model.RowStatus = -1
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
        Dim objCSPParCode As String
        objCSPParCode = CType(ViewState("Parameter"), String)

        Dim model As New CcCSPerformanceSubParameter
        Dim Id As Integer = ViewState("StateEditSub")
        Dim nresult As Integer = 0

        Dim objCSPParameter As CcCSPerformanceParameter
        objCSPParameter = CType(_sessHelper.GetSession("CSPParameterID"), CcCSPerformanceParameter)


        model.Name = txtParameterName.Text
        model.Weight = txtParameterWeight.Text
        model.Sequence = txtSequence.Text
        model.Type = CType(ddlTipe.SelectedValue, Short)
        model.CcCSPerformanceParameter = objCSPParameter
        model.ID = CType(ViewState("StateEditSub"), Integer)
        model.FunctionName = txtProcedure.Text
        model.ListOfAttribute = GetListSubParameterAttributeFromGridView()
        Try
            nresult = New CcCSPerformanceSubParameterFacade(User).Update(model)
            ViewState("CurrentSortColumn") = "LastUpdateTime"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        Catch ex As Exception
            MessageBox.Show("Error Has Been Occur : " & ex.ToString)
        End Try
        Return nresult


    End Function

    Private Function ViewDataCSPParameter(ByVal id As Integer, ByVal isEnable As Boolean)
        Dim objModel As CcCSPerformanceSubParameter = New CcCSPerformanceSubParameterFacade(User).Retrieve(id)

        If Not objModel Is Nothing Then
            lblKodeParameter.Text = objModel.CcCSPerformanceParameter.Name
            lblKodeSubParameter.Text = objModel.Code
            txtParameterName.Text = objModel.Name
            txtSequence.Text = objModel.Sequence
            txtParameterWeight.Text = objModel.Weight
            ddlTipe.SelectedValue = objModel.Type
            txtProcedure.Text = objModel.FunctionName

            Session(SESS_LIST_ATTRIBUTE) = GetListAttributeBySubParameter(objModel.ID)
            BindAttributeGrid()

        End If

        btnSimpan.Enabled = isEnable
    End Function

    Private Function GetListAttributeBySubParameter(ByVal subId) As List(Of CcCSPerformanceSubParameterAttribute)
        Dim result As New List(Of CcCSPerformanceSubParameterAttribute)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(CcCSPerformanceSubParameterAttribute), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(CcCSPerformanceSubParameterAttribute), "CcCSPerformanceSubParameter.ID", MatchType.Exact, subId))
        Dim arlData As ArrayList = New CcCSPerformanceSubParameterAttributeFacade(User).Retrieve(criterias)

        If arlData.Count > 0 Then
            result = arlData.Cast(Of CcCSPerformanceSubParameterAttribute).ToList()
        End If

        Return result
    End Function

    Private Function GetListSubParameterAttributeFromGridView() As ArrayList
        Dim result As New ArrayList

        For Each Row As GridViewRow In gvAttribute.Rows
            Dim item As New CcCSPerformanceSubParameterAttribute

            Dim lblID As Label = CType(Row.FindControl("lblID"), Label)
            Dim lblCcAttributeID As Label = CType(Row.FindControl("lblCcAttributeID"), Label)
            Dim lblDescription As Label = CType(Row.FindControl("lblDescription"), Label)
            Dim lbtnDelete As LinkButton = CType(Row.FindControl("lbtnDelete"), LinkButton)
            Dim ddlCcPeriodFrom As DropDownList = CType(Row.FindControl("ddlCcPeriodFrom"), DropDownList)
            Dim ddlCcPeriodTo As DropDownList = CType(Row.FindControl("ddlCcPeriodTo"), DropDownList)
            Dim txtMinimumScore As TextBox = CType(Row.FindControl("txtMinimumScore"), TextBox)

            item.ID = lblID.Text
            item.CcAttribute = New CcAttribute(CInt(lblCcAttributeID.Text))
            item.MinimumScore = Decimal.Parse(txtMinimumScore.Text)
            item.CcPeriodFrom = New CcPeriod(CInt(ddlCcPeriodFrom.SelectedValue))
            item.CcPeriodTo = New CcPeriod(CInt(ddlCcPeriodTo.SelectedValue))

            result.Add(item)


        Next

        'If Not Session(SESS_LIST_ATTRIBUTE) Is Nothing Then
        '    Dim listAttribute As List(Of CcAttribute) = CType(Session(SESS_LIST_ATTRIBUTE), List(Of CcAttribute))

        '    For Each att As CcAttribute In listAttribute
        '        Dim subParamAttribute As New CcCSPerformanceSubParameterAttribute
        '        subParamAttribute.CcAttribute = att
        '        result.Add(subParamAttribute)
        '    Next
        'End If

        Return result
    End Function

    Private Sub SetTextboxIsEnable(ByVal isEnable As Boolean)

        txtParameterName.Enabled = isEnable
        txtParameterWeight.Enabled = isEnable
        txtSequence.Enabled = isEnable
        ddlTipe.Enabled = isEnable
        txtProcedure.Enabled = isEnable
        lblShowAttribute.Visible = isEnable
    End Sub



    Private Sub ActivateUserPrivilege()

        IsAllowToRead = False
        IsAllowToEdit = False

        If Not SecurityProvider.Authorize(Context.User, SR.CSP_Input_SubParameter_Privilage) Then
            If Not SecurityProvider.Authorize(Context.User, SR.CSP_View_SubParameter_Privilage) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName= CS Performance Sub Parameter")
            Else
                IsAllowToEdit = True
            End If
        Else
            IsAllowToRead = True
            IsAllowToEdit = True
        End If
    End Sub

    Private Sub DisplayDataGrid()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        _sessHelper.SetSession("CSPSubParameter", criterias)
        dtgCSPParameter.CurrentPageIndex = 0
        BindDataGrid(dtgCSPParameter.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim arrList As ArrayList
        Dim totalRow As Integer = 0

        If indexPage >= 0 Then
            arrList = New CcCSPerformanceSubParameterFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("CSPSubParameter"), CriteriaComposite),
                                                                                  indexPage + 1, dtgCSPParameter.PageSize, totalRow, _
                                                                                   CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgCSPParameter.VirtualItemCount = totalRow
            dtgCSPParameter.CurrentPageIndex = indexPage
            dtgCSPParameter.DataSource = arrList
            dtgCSPParameter.DataBind()

        End If
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        Dim objCSPPar As CcCSPerformanceParameter
        objCSPPar = CType(_sessHelper.GetSession("CSPParameterID"), CcCSPerformanceParameter)

        criterias.opAnd(New Criteria(GetType(CcCSPerformanceSubParameter), "CcCSPerformanceParameter.ID", MatchType.Exact, objCSPPar.ID))

        If Not String.IsNullorEmpty(txtParameterName.Text) Then
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceSubParameter), "Name", MatchType.Partial, txtParameterName.Text))
        End If

        If Not String.IsNullorEmpty(txtSequence.Text) Then
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceSubParameter), "Sequence", MatchType.Exact, Val(txtSequence.Text)))
        End If

        If CDec(txtParameterWeight.Text) > 0 Then
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceSubParameter), "Weight", MatchType.Exact, CDec(Val(txtParameterWeight.Text))))
        End If

        If ddlTipe.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceSubParameter), "Type", MatchType.Exact, CType(ddlTipe.SelectedValue, Short)))
        End If

        If Not String.IsNullorEmpty(txtProcedure.Text) Then
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceSubParameter), "FunctionName", MatchType.Partial, txtProcedure.Text))
        End If
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
        Dim objModel As New CcCSPerformanceSubParameter
        Dim nresult As Integer = 0

        'If Not CheckCategoryCodeIsAlreadyExist(txtCategoryCode.Text) Then
        Dim objCSPParameter As CcCSPerformanceParameter
        objCSPParameter = CType(_sessHelper.GetSession("CSPParameterID"), CcCSPerformanceParameter)
        objModel.CcCSPerformanceParameter = objCSPParameter
        objModel.Name = txtParameterName.Text
        objModel.Weight = CDec(txtParameterWeight.Text)
        objModel.Sequence = txtSequence.Text
        objModel.Type = CType(ddlTipe.SelectedValue, Short)
        objModel.FunctionName = txtProcedure.Text
        objModel.ListOfAttribute = GetListSubParameterAttributeFromGridView()

        Try
            nresult = New CcCSPerformanceSubParameterFacade(User).Insert(objModel)
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        Catch ex As Exception
            MessageBox.Show("Error Has Been Occur : " & ex.ToString)
        End Try
        Return nresult
    End Function

    Private Function isEmptyText(ByRef errMsg As String) As Boolean
        Dim isEmpty As Boolean = False

        If String.IsNullorEmpty(txtParameterName.Text) Then
            errMsg = errMsg + "** Parameter Name Harus Diisi <br>"
            isEmpty = True
        End If

        'If CcCSPerformanceSubParameter.SubParameterMaxValue.Grade <> CType(ddlTipe.SelectedValue, Short) Then
        '    If String.IsNullorEmpty(txtBobotMaximal.Text) Then
        '        errMsg = errMsg + "** Jika Tipe Grade, Maka Bobot Maximal Harus Diisi <br>"
        '        isEmpty = True
        '    End If
        'End If

        If String.IsNullorEmpty(txtSequence.Text) Then
            errMsg = errMsg + "** Nomor Urut Harus Diisi <br>"
            isEmpty = True
        End If

        If String.IsNullorEmpty(txtParameterWeight.Text) Then
            errMsg = errMsg + "** Bobot Harus Diisi <br>"
            isEmpty = True
        Else
            If CDec(txtParameterWeight.Text) = 0 Then
                errMsg = errMsg + "** Bobot Harus Lebih dari 0 <br>"
                isEmpty = True
            End If
        End If

        If ddlTipe.SelectedIndex = 0 Then
            errMsg = errMsg + "** Tipe Harus Dipilih <br>"
            Return True
        End If



        Return isEmpty
    End Function

#End Region



    Private Sub btnTriggerAttribute_Click(sender As Object, e As EventArgs) Handles btnTriggerAttribute.Click
        Try
            BindAttributeGrid()
            hdnAttributeID.Value = String.Empty
        Catch ex As Exception
            MessageBox.Show("Gagal dalam memuat atribut : " & ex.Message)
        End Try
    End Sub

    Private Sub BindAttributeGrid()
        Dim listAttribute As List(Of CcCSPerformanceSubParameterAttribute)
        If Not Session(SESS_LIST_ATTRIBUTE) Is Nothing Then
            listAttribute = CType(Session(SESS_LIST_ATTRIBUTE), List(Of CcCSPerformanceSubParameterAttribute))
        Else
            listAttribute = New List(Of CcCSPerformanceSubParameterAttribute)
        End If

        If hdnAttributeID.Value <> String.Empty Then
            Dim listId() As String = hdnAttributeID.Value.Split(";")
            Dim facAttribute As CcAttributeFacade = New CcAttributeFacade(User)
            For Each id As Integer In listId
                If listAttribute.FirstOrDefault(Function(x) x.CcAttribute.ID = id) Is Nothing Then
                    Dim newData As New CcCSPerformanceSubParameterAttribute
                    newData.CcAttribute = facAttribute.Retrieve(CShort(id))
                    listAttribute.Add(newData)
                End If
            Next
        End If

        Session(SESS_LIST_ATTRIBUTE) = listAttribute
        gvAttribute.DataSource = listAttribute
        gvAttribute.DataBind()

    End Sub

    Private Sub gvAttribute_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAttribute.RowCommand
        If e.CommandName = "Delete" Then
            Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)
            Dim lblCcAttributeID As Label = CType(row.FindControl("lblCcAttributeID"), Label)
            Dim ccAttributeID As Integer = lblCcAttributeID.Text
            Dim listAttribute As List(Of CcCSPerformanceSubParameterAttribute) = CType(Session(SESS_LIST_ATTRIBUTE), List(Of CcCSPerformanceSubParameterAttribute))

            Dim itemToRemove As CcCSPerformanceSubParameterAttribute = listAttribute.FirstOrDefault(Function(x) x.CcAttribute.ID = ccAttributeID)
            listAttribute.Remove(itemToRemove)
            Session(SESS_LIST_ATTRIBUTE) = listAttribute
            gvAttribute.DataSource = listAttribute
            gvAttribute.DataBind()

        End If
    End Sub

    Private Sub gvAttribute_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAttribute.RowDataBound
        If Not e.Row.DataItem Is Nothing Then
            Dim data As CcCSPerformanceSubParameterAttribute = CType(e.Row.DataItem, CcCSPerformanceSubParameterAttribute)
            Dim lblID As Label = CType(e.Row.FindControl("lblID"), Label)
            Dim lblCcAttributeID As Label = CType(e.Row.FindControl("lblCcAttributeID"), Label)
            Dim lblDescription As Label = CType(e.Row.FindControl("lblDescription"), Label)
            Dim lbtnDelete As LinkButton = CType(e.Row.FindControl("lbtnDelete"), LinkButton)
            Dim ddlCcPeriodFrom As DropDownList = CType(e.Row.FindControl("ddlCcPeriodFrom"), DropDownList)
            Dim ddlCcPeriodTo As DropDownList = CType(e.Row.FindControl("ddlCcPeriodTo"), DropDownList)
            Dim txtMinimumScore As TextBox = CType(e.Row.FindControl("txtMinimumScore"), TextBox)
            lbtnDelete.Visible = lblShowAttribute.Visible
            txtMinimumScore.Enabled = lblShowAttribute.Visible
            ddlCcPeriodFrom.Enabled = lblShowAttribute.Visible
            ddlCcPeriodTo.Enabled = lblShowAttribute.Visible

            lblID.Text = data.ID
            lblCcAttributeID.Text = data.CcAttribute.ID
            txtMinimumScore.Text = data.MinimumScore
            lblDescription.Text = data.CcAttribute.Description
            ddlCcPeriodFrom.Items.Clear()
            For Each period As CcPeriod In Me.ListCcPeriod
                ddlCcPeriodFrom.Items.Add(New ListItem(period.YearMonth, period.ID))
            Next

            If Not data.CcPeriodFrom Is Nothing Then
                Dim selectedPeriodFrom As ListItem = ddlCcPeriodFrom.Items.FindByValue(data.CcPeriodFrom.ID)
                If Not selectedPeriodFrom Is Nothing Then
                    selectedPeriodFrom.Selected = True
                End If
            End If

            ddlCcPeriodTo.Items.Clear()
            For Each period As CcPeriod In Me.ListCcPeriod
                ddlCcPeriodTo.Items.Add(New ListItem(period.YearMonth, period.ID))
            Next

            If Not data.CcPeriodTo Is Nothing Then
                Dim selectedPeriodTo As ListItem = ddlCcPeriodTo.Items.FindByValue(data.CcPeriodTo.ID)
                If Not selectedPeriodTo Is Nothing Then
                    selectedPeriodTo.Selected = True
                End If
            End If


        End If

    End Sub

    Private Sub gvAttribute_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvAttribute.RowDeleting

    End Sub
End Class