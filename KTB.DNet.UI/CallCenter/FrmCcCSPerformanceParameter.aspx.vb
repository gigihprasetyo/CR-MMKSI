Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.Domain
Imports KTB.DNet.Security

Public Class FrmCcCSPerformanceParameter
    Inherits System.Web.UI.Page

    Private _sessHelper As SessionHelper = New SessionHelper
    Dim IsAllowToEdit As Boolean
    Dim IsAllowToRead As Boolean
    Dim objCSPMaster As CcCSPerformanceMaster



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
            Dim model As CcCSPerformanceParameter = CType(e.Item.DataItem, CcCSPerformanceParameter)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            Dim lblKodeMaster As Label = CType(e.Item.FindControl("lblKodeMaster"), Label)
            Dim isEdit As Boolean = GetCSPMaster(model.CcCSPerformanceMasterID)

            Dim objCSPMaster As New CcCSPerformanceMaster
            objCSPMaster = New CcCSPerformanceMasterFacade(User).Retrieve(model.CcCSPerformanceMasterID)


            lblKodeMaster.Text = objCSPMaster.Description
            lbtnEdit.Visible = isEdit
            lbtnDelete.Visible = isEdit

            'Dim lblReferensi As Label = CType(e.Item.FindControl("lblReferensi"), Label)
            'lblReferensi.Text = CommonFunction.GetEnumDescription(model.FunctionName, "CSP_PAR_FUNCTION")

            Dim lblGvCustomerCategory As Label = CType(e.Item.FindControl("lblGvCustomerCategory"), Label)
            Dim lblGvStatus As Label = CType(e.Item.FindControl("lblGvStatus"), Label)

            If model.CcCustomerCategoryID <> 0 Then
                lblGvCustomerCategory.Text = model.CcCustomerCategory.Description
            End If


            lblGvStatus.Text = CcCSPerformanceParameter.GetStatusDescription(model.Status)

            If objCSPMaster.Status = 0 Then
                lbtnDelete.Visible = False
                lbtnEdit.Visible = False
            End If


        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCSPParameter.CurrentPageIndex * dtgCSPParameter.PageSize)
        End If
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Server.Transfer("FrmCcCSPerformanceMaster.aspx")
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click

        Dim model As New CcCSPerformanceMaster
        Dim nResult As Integer
        Dim errMsg As String = ""

        If Not isEmptyText(errMsg) Then
            Select Case CType(ViewState("CSPParameter"), String)
                Case "Insert"
                    nResult = InsertData()
                Case "Update"
                    nResult = UpdateData()
            End Select

            If nResult > 0 Then
                MessageBox.Show(SR.SaveSuccess)
                ClearData()
                BindDataGrid(0)
            Else
                MessageBox.Show(SR.SaveFail)
            End If

        Else
            'If Not (Page.IsValid) Then
            'MessageBox.Show(errMsg)
            lblErrMsg.Text = errMsg
            'End If
            ''If Val(txtParameterWeight.Text) = 0 Then
            ''    MessageBox.Show("Bobot Harus Lebih besar dari 0")
            ''End If
            'If ddlRefrensi.SelectedIndex = 0 Then
            '    MessageBox.Show("Referensi Harus Dipilih")
            'End If
        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ActivateUserPrivilege()

        If Not IsPostBack Then
            Dim Id As Integer


            BindDdlCustomerCategory()
            BindDdlStatus()
            ClearData()
            If CType(_sessHelper.GetSession("CSPParameterID"), CcCSPerformanceParameter) IsNot Nothing Then
                ' ini untuk handle tombol kembali di form clasification
                Dim objCSPParameter As CcCSPerformanceParameter
                objCSPParameter = CType(_sessHelper.GetSession("CSPParameterID"), CcCSPerformanceParameter)
                objCSPMaster = New CcCSPerformanceMaster
                objCSPMaster = New CcCSPerformanceMasterFacade(User).Retrieve(objCSPParameter.CcCSPerformanceMasterID)
                lblKodeMaster.Text = objCSPMaster.Description
                ViewState("Master") = objCSPMaster.Code
            Else
                Id = CInt(Request.QueryString("MasID"))
                If CInt(Request.QueryString("MasID")) > 0 Or Request.QueryString("MasID") IsNot Nothing Then
                    objCSPMaster = New CcCSPerformanceMaster
                    objCSPMaster = New CcCSPerformanceMasterFacade(User).Retrieve(CInt(Request.QueryString("MasID")))
                    lblKodeMaster.Text = objCSPMaster.Description
                    ViewState("Master") = objCSPMaster.Code

                    If objCSPMaster.Status = 0 Then
                        SetTextboxIsEnable(False)
                        btnSimpan.Visible = False
                        btnBaru.Visible = False
                    End If
                End If
            End If

            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

            DisplayDataGrid()

        End If


    End Sub

    Private Sub BindDdlCustomerCategory()
        ddlCustomerCategory.Items.Clear()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(CcCustomerCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim dataResult As ArrayList = New CcCustomerCategoryFacade(User).Retrieve(criterias)

        For Each Category As CcCustomerCategory In dataResult
            ddlCustomerCategory.Items.Add(New ListItem(Category.Description, Category.ID))
        Next

        ddlCustomerCategory.Items.Insert(0, New ListItem("Silakan Pilih", ""))

    End Sub

    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()

        ddlStatus.Items.Add(New ListItem("Silakan Pilih", ""))
        ddlStatus.Items.Add(New ListItem(CcCSPerformanceParameter.GetStatusDescription(CInt(CcCSPerformanceParameter.EnumStatus.ACTIVE)), CShort(CcCSPerformanceParameter.EnumStatus.ACTIVE)))
        ddlStatus.Items.Add(New ListItem(CcCSPerformanceParameter.GetStatusDescription(CInt(CcCSPerformanceParameter.EnumStatus.NON_ACTIVE)), CShort(CcCSPerformanceParameter.EnumStatus.NON_ACTIVE)))
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        'Dim strErrMsg As String = ""
        'If Not isEmptyText(strErrMsg) Then
        'Else
        '    lblErrMsg.Text = strErrMsg
        'End If
        If btnCari.Text = "Cari" Then
            DisplayDataGrid()
        Else
            dtgCSPParameter.SelectedIndex = -1
            ClearData()
            ddlCustomerCategory.Enabled = True
            ddlStatus.Enabled = True
            btnCari.Text = "Cari"
        End If


    End Sub

    Private Sub dtgCSPParameter_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgCSPParameter.ItemCommand
        If e.CommandName = "View" Then
            ViewDataCSPParameter(Val(e.Item.Cells(0).Text), False)
            SetTextboxIsEnable(False)
            btnCari.Text = "Batal"
            dtgCSPParameter.SelectedIndex = e.Item.ItemIndex

        ElseIf e.CommandName = "Delete" Then
            DeleteCcCSPerformanceParameter(Val(e.Item.Cells(0).Text))
        ElseIf e.CommandName = "Edit" Then
            ViewState("CSPParameter") = "Update"
            ViewState("StateEdit") = Val(e.Item.Cells(0).Text)
            ViewDataCSPParameter(Val(e.Item.Cells(0).Text), True)
            SetTextboxIsEnable(True)
            btnCari.Text = "Batal"
            dtgCSPParameter.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "GotoSubParameter" Then
            Dim strCSPParameterID As String = e.Item.Cells(0).Text
            Dim objCSPParameter As CcCSPerformanceParameter = New CcCSPerformanceParameterFacade(User).Retrieve(CInt(strCSPParameterID))
            _sessHelper.SetSession("CSPParameterID", objCSPParameter)
            Server.Transfer("FrmCcCSPerformanceSubParameter.aspx?ParID=" & strCSPParameterID)
        End If
    End Sub

#End Region

#Region "custom Method"

   
   

    Private Sub ClearData()

        txtParameterWeight.Text = 0
        txtParameterWeight.Enabled = True

        txtParameterName.Text = ""
        txtParameterName.Enabled = True

        lblKodeParameter.Text = ""

        txtSequence.Text = ""
        txtSequence.Enabled = True


        btnSimpan.Enabled = True
        ViewState("CSPParameter") = "Insert"


        ddlCustomerCategory.ClearSelection()
        ddlStatus.ClearSelection()
    End Sub

    Private Sub DeleteCcCSPerformanceParameter(ByVal Id As Integer)

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Dim model As CcCSPerformanceParameter = New CcCSPerformanceParameterFacade(User).Retrieve(Id)
            Dim facade As CcCSPerformanceParameterFacade = New CcCSPerformanceParameterFacade(User)
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
        Dim model As New CcCSPerformanceParameter
        Dim Id As Integer = ViewState("StateEdit")
        Dim nresult As Integer = 0
        Dim objCSPMasterCode As String
        objCSPMasterCode = CType(ViewState("Master"), String)

        model.Name = txtParameterName.Text
        model.Weight = txtParameterWeight.Text
        model.Sequence = txtSequence.Text
        model.CcCSPerformanceMasterID = New CcCSPerformanceMasterFacade(User).Retrieve(objCSPMasterCode)
        model.ID = CType(ViewState("StateEdit"), Integer)
        model.FunctionName = 0
        model.Status = ddlStatus.SelectedValue
        model.CcCustomerCategoryID = ddlCustomerCategory.SelectedValue

        Try
            nresult = New CcCSPerformanceParameterFacade(User).Update(model)
            ViewState("CurrentSortColumn") = "LastUpdateTime"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        Catch ex As Exception
            MessageBox.Show("Error Has Been Occur : " & ex.ToString)
        End Try
        Return nresult


    End Function

    Private Function ViewDataCSPParameter(ByVal id As Integer, ByVal isEnable As Boolean)
        Dim objModel As CcCSPerformanceParameter = New CcCSPerformanceParameterFacade(User).Retrieve(id)

        If Not objModel Is Nothing Then
            lblKodeMaster.Text = objModel.CcCSPerformanceMaster.Description
            lblKodeParameter.Text = objModel.Code
            txtParameterName.Text = objModel.Name
            txtSequence.Text = objModel.Sequence
            txtParameterWeight.Text = objModel.Weight

            ddlCustomerCategory.ClearSelection()
            Dim selectedCustomerCategory As ListItem = ddlCustomerCategory.Items.FindByValue(objModel.CcCustomerCategoryID)
            If Not selectedCustomerCategory Is Nothing Then
                selectedCustomerCategory.Selected = True
            End If

            ddlStatus.ClearSelection()
            Dim selectedStatus As ListItem = ddlStatus.Items.FindByValue(objModel.Status)
            If Not selectedStatus Is Nothing Then
                selectedStatus.Selected = True
            End If

        End If

        btnSimpan.Enabled = isEnable
    End Function

    Private Sub SetTextboxIsEnable(ByVal isEnable As Boolean)

        txtParameterName.Enabled = isEnable
        txtParameterWeight.Enabled = isEnable
        txtSequence.Enabled = isEnable
        ddlCustomerCategory.Enabled = isEnable
        ddlStatus.Enabled = isEnable

    End Sub

    Private Sub ActivateUserPrivilege()

        IsAllowToRead = False
        IsAllowToEdit = False

        If Not SecurityProvider.Authorize(Context.User, SR.CSP_Input_Parameter_Privilage) Then
            If Not SecurityProvider.Authorize(Context.User, SR.CSP_View_Parameter_Privilage) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName= CS Performance Parameter")
            Else
                IsAllowToEdit = True
            End If
        Else
            IsAllowToRead = True
            IsAllowToEdit = True
        End If
    End Sub

    Private Sub DisplayDataGrid()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        _sessHelper.SetSession("CSPParameter", criterias)
        dtgCSPParameter.CurrentPageIndex = 0
        BindDataGrid(dtgCSPParameter.CurrentPageIndex)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim arrList As ArrayList
        Dim totalRow As Integer = 0

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(CcCSPerformanceParameter), "CcCustomerCategoryID", Sort.SortDirection.ASC))
        sortColl.Add(New Sort(GetType(CcCSPerformanceParameter), "Sequence", Sort.SortDirection.ASC))

        If indexPage >= 0 Then
            arrList = New CcCSPerformanceParameterFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("CSPParameter"), CriteriaComposite), sortColl, indexPage + 1, dtgCSPParameter.PageSize, totalRow)

            'arrList = New CcCSPerformanceParameterFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("CSPParameter"), CriteriaComposite),
            '                                                                      indexPage + 1, dtgCSPParameter.PageSize, totalRow, _
            '                                                                       CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgCSPParameter.VirtualItemCount = totalRow
            dtgCSPParameter.CurrentPageIndex = indexPage
            dtgCSPParameter.DataSource = arrList
            dtgCSPParameter.DataBind()

        End If
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        Dim objCSPMasterCode As String
        objCSPMasterCode = CType(ViewState("Master"), String)

        criterias.opAnd(New Criteria(GetType(CcCSPerformanceParameter), "CcCSPerformanceMaster.Code", MatchType.Exact, objCSPMasterCode))

        If Not String.IsNullOrEmpty(txtParameterName.Text) Then
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceParameter), "Name", MatchType.Partial, txtParameterName.Text))
        End If

        If Not String.IsNullOrEmpty(txtSequence.Text) Then
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceParameter), "Sequence", MatchType.Exact, Val(txtParameterName.Text)))
        End If

        If CDec(txtParameterWeight.Text) > 0 Then
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceParameter), "Weight", MatchType.Exact, CDec(Val(txtParameterWeight.Text))))
        End If

        If ddlCustomerCategory.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceParameter), "CcCustomerCategoryID", MatchType.Exact, CInt(ddlCustomerCategory.SelectedValue)))
        End If

        If ddlStatus.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceParameter), "Status", MatchType.Exact, CShort(ddlStatus.SelectedValue)))
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
        Dim objModel As New CcCSPerformanceParameter
        Dim nresult As Integer = 0
        Dim objCSPMasterCode As String
        objCSPMasterCode = CType(ViewState("Master"), String)


        'If Not CheckCategoryCodeIsAlreadyExist(txtCategoryCode.Text) Then
        objModel.CcCSPerformanceMasterID = New CcCSPerformanceMasterFacade(User).Retrieve(objCSPMasterCode)
        objModel.Name = txtParameterName.Text
        objModel.Weight = CDec(txtParameterWeight.Text)
        objModel.Sequence = txtSequence.Text
        objModel.ParentID = 0
        objModel.level = 0
        objModel.FunctionName = 0
        objModel.Status = ddlStatus.SelectedValue
        objModel.CcCustomerCategoryID = ddlCustomerCategory.SelectedValue
        Try
            nresult = New CcCSPerformanceParameterFacade(User).Insert(objModel)
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        Catch ex As Exception
            MessageBox.Show("Error Has Been Occur : " & ex.ToString)
        End Try
        Return nresult
    End Function

    Private Function isEmptyText(ByRef errMsg As String) As Boolean
        Dim isEmpty As Boolean = False

        If String.IsNullOrEmpty(txtParameterName.Text) Then
            errMsg = errMsg + "** Nama Parameter Harus Diisi <br>"
            isEmpty = True
        End If


        If String.IsNullOrEmpty(txtSequence.Text) Then
            errMsg = errMsg + "** Nomor Urut Harus Diisi <br>"
            isEmpty = True
        End If

        If String.IsNullOrEmpty(txtParameterWeight.Text) Then
            errMsg = errMsg + "** Bobot Harus Diisi <br>"
            isEmpty = True
            'Else
            '    If CDec(txtParameterWeight.Text) = 0 Then
            '        errMsg = errMsg + "** Bobot Harus Diisi <br>"
            '        isEmpty = True
            'End If
        End If

        If ddlCustomerCategory.SelectedValue = "" Then
            errMsg = errMsg + "** Tipe Customer Harus Dipilih <br>"
            isEmpty = True
        End If

        If ddlStatus.SelectedValue = "" Then
            errMsg = errMsg + "** Status Harus Dipilih <br>"
            isEmpty = True
        End If

        Return isEmpty
    End Function

#End Region


End Class