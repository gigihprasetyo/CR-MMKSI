Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports System.Linq
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.CallCenter

Public Class FrmCcCSPerformanceMaster
    Inherits System.Web.UI.Page

    Private _sessHelper As SessionHelper = New SessionHelper
    Dim IsAllowToEdit As Boolean
    Dim IsAllowToRead As Boolean


#Region "Custom Method"

    'Private Sub BindingDropdownStatus()
    '    Dim commFunction As New CommonFunction
    '    commFunction.BindEnumDetailToDDL(ddlstatus, "CSP_STATUS")
    'End Sub

    Private Sub ClearData()
        txtDescription.Text = ""
        lblCode.Text = ""
        'CommonFunction.BindEnumDetailToDDL(ddlstatus, "CSP_STATUS")
        BindDropdownReffFormId()
        ViewState("vsProcessCSPM") = "Insert"
        'ddlstatus.Enabled = True
        ddlRefFormCode.Enabled = True
        ddlPeriodeFrom.ClearSelection()
        ddlPeriodeTo.ClearSelection()
    End Sub

    Private Sub destroySession()
        If CType(_sessHelper.GetSession("CSPParameterID"), CcCSPerformanceParameter) IsNot Nothing Then
            _sessHelper.SetSession("CSPParameterID", Nothing)
        End If

        If CType(_sessHelper.GetSession("CSPSubPar"), CcCSPerformanceSubParameter) IsNot Nothing Then
            _sessHelper.SetSession("CSPSubPar", Nothing)
        End If

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

    Private Function GetDAFormID(ByVal DAFormCode As String) As Integer
        Dim id As Integer
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "Code", MatchType.Exact, DAFormCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "RowStatus", MatchType.Exact, 0))

        Dim idResult = New CcCSPerformanceMasterFacade(User).Retrieve(criterias)
        For Each model As CcCSPerformanceMaster In idResult
            id = model.ID
        Next
        Return id
    End Function

    Private Sub SetNonActiveAll(ByVal Id As Integer)
        Dim model As New CcCSPerformanceMaster
        Dim obj As CcCSPerformanceMaster = New CcCSPerformanceMasterFacade(User).Retrieve(Id)

        model.ID = Id
        model.Code = obj.Code
        model.Description = obj.Description
        model.Status = 1

        model.LastUpdateBy = obj.LastUpdateBy
        model.RowStatus = obj.RowStatus

        If Not obj Is Nothing Then
            Dim facade = New CcCSPerformanceMasterFacade(User).Update(model)
        Else
            MessageBox.Show(SR.ViewFail)
        End If

        DisplayDataGrid()
    End Sub

    Private Function UpdateCSPMaster() As Integer
        Dim model As New CcCSPerformanceMaster
        Dim updateResult As Integer = 0
        Dim id As Integer = ViewState("stateEdit")

        model.ID = id
        model.Code = ""
        model.Description = txtDescription.Text
        ' model.Status = 1
        If ddlRefFormCode.SelectedIndex = 0 Then
            model.ReferenceID = 0
        Else
            model.ReferenceID = CInt(ddlRefFormCode.SelectedValue)
        End If

        model.CcPeriodIDFrom = ddlPeriodeFrom.SelectedValue
        model.CcPeriodIDTo = ddlPeriodeTo.SelectedValue

        model.RowStatus = 0

        Try
            updateResult = New CcCSPerformanceMasterFacade(User).Update(model)

            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try
        Return updateResult
    End Function

    Private Sub DisplayDataSearch(ByVal indexPage As Integer)
        Dim arrList As ArrayList
        Dim totalRow As Integer = 0

        If indexPage >= 0 Then
            arrList = New CcCSPerformanceMasterFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("CSPMaster"), CriteriaComposite), indexPage + 1, dtgCSPMaster.PageSize, totalRow)
            dtgCSPMaster.VirtualItemCount = totalRow
            BindDataGrid(arrList)
        End If
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        _sessHelper.SetSession("criterias", criterias)

        arrList = New CcCSPerformanceMasterFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("criterias"), CriteriaComposite), idxPage + 1, dtgCSPMaster.PageSize, totalRow, _
                CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dtgCSPMaster.CurrentPageIndex = idxPage
        dtgCSPMaster.DataSource = arrList
        dtgCSPMaster.VirtualItemCount = totalRow
        dtgCSPMaster.DataBind()
        _sessHelper.SetSession("idxPage", dtgCSPMaster.CurrentPageIndex)

    End Sub

    Private Sub BindDataGrid(ByVal arrList As ArrayList)
        dtgCSPMaster.DataSource = arrList
        dtgCSPMaster.DataBind()
    End Sub

    Public Sub BindDropdownReffFormId()
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.CcCSPerformanceMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim dataResult = New CcCSPerformanceMasterFacade(User).Retrieve(criterias)

        ddlRefFormCode.DataSource = dataResult
        ddlRefFormCode.DataTextField = "Code"
        ddlRefFormCode.DataValueField = "ID"
        ddlRefFormCode.DataBind()

        ddlRefFormCode.Items.Insert(0, New ListItem("", ""))
    End Sub

    Private Sub SendIDToCategory(ByVal Id As String)
        _sessHelper.SetSession("SEND_FORMCODE", Id)
    End Sub

    Private Sub DisplayDataGrid()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        _sessHelper.SetSession("CSPMaster", criterias)
        dtgCSPMaster.CurrentPageIndex = 0
        DisplayDataSearch(dtgCSPMaster.CurrentPageIndex)
    End Sub

    Private Function ItemFormReffCode() As ArrayList
        Dim objModel As ObjFormReffCode
        Dim arrList As New ArrayList
        Dim ddlformReffCodeCount = ddlRefFormCode.Items.Count
        arrList.Add(ddlformReffCodeCount)
        Return arrList
    End Function

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        If Not String.IsNullOrEmpty(lblCode.Text) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "Code", MatchType.Partial, lblCode.Text))
        End If

        If Not String.IsNullOrEmpty(txtDescription.Text) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "Description", MatchType.Partial, txtDescription.Text))
        End If

        'If ddlstatus.SelectedIndex > 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "Status", MatchType.Exact, ddlstatus.SelectedValue))
        'End If

        If ddlPeriodeFrom.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "CcPeriodIDFrom", MatchType.Exact, ddlPeriodeFrom.SelectedValue))
        End If

        If ddlPeriodeTo.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "CcPeriodIDTo", MatchType.Exact, ddlPeriodeTo.SelectedValue))
        End If


    End Sub

    Private Function isFormCodeAlreadyExist(ByVal codeInput As String) As Boolean
        Dim isExist As Boolean = False

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "Code", MatchType.Exact, codeInput))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "RowStatus", MatchType.Exact, 0))

        Dim codeResult = New CcCSPerformanceMasterFacade(User).Retrieve(criterias)
        If codeResult.count > 1 Then
            isExist = True
        End If

        Return isExist
    End Function

    Private Function InsertCSPMaster() As Integer

        Dim saveResult As Integer
        Dim objModel As New CcCSPerformanceMaster
        Dim reffCodeResult As Integer
        Dim CSPMasterFacade As CcCSPerformanceMasterFacade = New CcCSPerformanceMasterFacade(User)


        objModel.Code = ""
        objModel.Description = txtDescription.Text
        objModel.Status = 1
        objModel.CreatedBy = User.Identity.Name
        objModel.RowStatus = 0
        objModel.CcPeriodIDFrom = ddlPeriodeFrom.SelectedValue
        objModel.CcPeriodIDTo = ddlPeriodeTo.SelectedValue

        Try

            If ddlRefFormCode.SelectedIndex = 0 Then
                objModel.ReferenceID = 0
            Else
                objModel.ReferenceID = CInt(ddlRefFormCode.SelectedValue)
            End If

            saveResult = CSPMasterFacade.Insert(objModel)
            If saveResult > 0 Then
                If ddlRefFormCode.SelectedIndex > 0 Then
                    CSPMasterFacade.InsertFromReff(saveResult, CType(ddlRefFormCode.SelectedValue, Integer))
                End If
                ViewState("CurrentSortColumn") = "ID"
                ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            End If

        Catch ex As Exception

            If saveResult <= 0 Then
                MessageBox.Show(SR.SaveFail)
            End If


        End Try

        Return saveResult
    End Function

    Private Function GetFormCodeByIndex(ByVal index As Integer) As String
        Dim strCode As String
        Dim abc As Integer
        Dim listData = CType(_sessHelper.GetSession("ddlFormCode"), ArrayList)

        strCode = listData.IndexOf(index)
        Return strCode
    End Function

    Private Function isEmptyText() As Boolean

        If String.IsNullOrEmpty(txtDescription.Text) Then
            Return True
        End If

        'If ddlstatus.SelectedIndex = 0 Then
        '    Return True
        'End If

        If ddlPeriodeFrom.SelectedValue = "-1" Or ddlPeriodeTo.SelectedValue = "-1" Then
            Return True
        End If

    End Function

    Private Sub SetActive(ByVal Id As Integer)
        Dim model As New CcCSPerformanceMaster
        Dim obj As CcCSPerformanceMaster = New CcCSPerformanceMasterFacade(User).Retrieve(Id)

        model.ID = Id
        model.Code = obj.Code
        model.Description = obj.Description
        model.Status = 0

        model.LastUpdateBy = obj.LastUpdateBy
        model.RowStatus = obj.RowStatus

        If Not obj Is Nothing Then
            Dim facade = New CcCSPerformanceMasterFacade(User).Update(model)
        Else
            MessageBox.Show(SR.ViewFail)
        End If

        DisplayDataGrid()
    End Sub

    Private Function SetNonActive(ByVal Id As Integer) As Integer
        Dim model As New CcCSPerformanceMaster
        Dim facade As Integer = 0

        Dim arlCSPMaster As ArrayList
        Dim criCSPMaster As New CriteriaComposite(New Criteria(GetType(CcCSPerformanceMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criCSPMaster.opAnd(New Criteria(GetType(CcCSPerformanceMaster), "Status", MatchType.Exact, CType(StatusActive.Aktif, Short)))
        criCSPMaster.opAnd(New Criteria(GetType(CcCSPerformanceMaster), "ID", MatchType.No, Id))

        arlCSPMaster = New CcCSPerformanceMasterFacade(User).Retrieve(criCSPMaster)

        If arlCSPMaster.Count > 0 Then

            Dim obj As CcCSPerformanceMaster = New CcCSPerformanceMasterFacade(User).Retrieve(Id)

            model.ID = Id
            model.Code = obj.Code
            model.Description = obj.Description
            model.Status = StatusActive.NonAktif

            model.LastUpdateBy = obj.LastUpdateBy
            model.RowStatus = obj.RowStatus

            facade = New CcCSPerformanceMasterFacade(User).Update(model)

            ViewState("CurrentSortColumn") = "LastUpdateTime"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            BindDataGrid(0)

            Return facade

        End If


        MessageBox.Show("Tidak Ada Formula Master yang aktif")
        Return facade

        DisplayDataGrid()
    End Function

    Public Sub ViewDataCSPMaster(ByVal id As Integer, ByVal isEnableBtn As Boolean)
        Dim obj As New CcCSPerformanceMaster
        obj = New CcCSPerformanceMasterFacade(User).Retrieve(id)

        lblCode.Text = obj.Code
        txtDescription.Text = obj.Description
        btnSimpan.Enabled = isEnableBtn
        ' ddlstatus.SelectedValue = obj.Status
        ViewState("stateEdit") = id
        If isEnableBtn Then
            ViewState("vsProcessCSPM") = "Update"
        Else
            ViewState("vsProcessCSPM") = "View"
        End If

        ddlPeriodeFrom.ClearSelection()
        ddlPeriodeTo.ClearSelection()
        ddlRefFormCode.ClearSelection()

        Dim selectedPeriodFrom As ListItem = ddlPeriodeFrom.Items.FindByValue(obj.CcPeriodIDFrom)
        If Not selectedPeriodFrom Is Nothing Then
            selectedPeriodFrom.Selected = True
        End If

        Dim selectedPeriodTo As ListItem = ddlPeriodeTo.Items.FindByValue(obj.CcPeriodIDTo)
        If Not selectedPeriodTo Is Nothing Then
            selectedPeriodTo.Selected = True
        End If

        Dim selectedRef As ListItem = ddlRefFormCode.Items.FindByValue(obj.ReferenceID)
        If Not selectedRef Is Nothing Then
            selectedRef.Selected = True
        End If

    End Sub

    Private Sub NonActiveAllDataActive()
        Dim obj As New CcCSPerformanceMaster
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "Status", MatchType.Exact, 0))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "RowStatus", MatchType.Exact, 0))

        Dim dataActiveResult = New CcCSPerformanceMasterFacade(User).Retrieve(criterias)
        For Each model As CcCSPerformanceMaster In dataActiveResult
            obj.ID = model.ID
            obj.Code = model.Code
            obj.Description = model.Description
            obj.Status = 1

            obj.RowStatus = model.RowStatus
            obj.LastUpdateBy = model.LastUpdateBy

            Dim facade = New CcCSPerformanceMasterFacade(User).Update(obj)
        Next
    End Sub


    Private Sub DeleteCSPMaster(ByVal Id As Integer)
        Dim model As New CcCSPerformanceMaster
        Dim CSPMasterFacade As New CcCSPerformanceMasterFacade(User)

        model.ID = Id

        CSPMasterFacade.Delete(model)

        ViewState("CurrentSortColumn") = "LastUpdateTime"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

        DisplayDataGrid()
    End Sub
#End Region

#Region "Private Class"
    Private Class ObjFormReffCode
        Private value As Integer
        Private text As String

        Public Property objValue As Integer
            Get
                Return value
            End Get
            Set(ByVal value As Integer)
                value = value
            End Set
        End Property

        Public Property objText As String
            Get
                Return text
            End Get
            Set(ByVal value As String)
                text = value
            End Set
        End Property
    End Class

    Private Enum StatusActive
        Aktif = 0
        NonAktif = 1
    End Enum
#End Region



#Region "Handler"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ActivateUserPrivilege()
        destroySession()
        If Not IsPostBack Then
            ViewState("vsProcessCSPM") = "Insert"
            BindDropdownReffFormId()
            'BindingDropdownStatus()
            BindDdlPeriod()
            BindDataGrid(0)
            ViewState("CurrentSortColumn") = "ID,Status"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        End If
    End Sub

    Private Sub BindDdlPeriod()
        Dim DtStart As DateTime = DateTime.Now.AddMonths(-5)
        Dim DtEnd As DateTime = DateTime.Now.AddMonths(15)

        Dim criteriaCCperiod As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaCCperiod.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcPeriod), "YearMonth", MatchType.GreaterOrEqual, DtStart.ToString("yyyyMM")))
        criteriaCCperiod.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcPeriod), "YearMonth", MatchType.LesserOrEqual, DtEnd.ToString("yyyyMM")))

        Dim arrayListPeriode As ArrayList = New CcPeriodFacade(User).RetrieveByCriteria(criteriaCCperiod, "YearMonth", Sort.SortDirection.DESC)

        For Each item As CcPeriod In arrayListPeriode
            Dim listItem As New ListItem(item.YearMonth, item.ID)
            ddlPeriodeFrom.Items.Add(listItem)
            ddlPeriodeTo.Items.Add(listItem)
        Next
        Dim listitemBlank As New ListItem("Silahkan Pilih", "-1")
        ddlPeriodeFrom.Items.Insert(0, listitemBlank)
        ddlPeriodeTo.Items.Insert(0, listitemBlank)

    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim model As New CcCSPerformanceMaster
        Dim nResult As Integer

        If Not isEmptyText() Then
            Select Case CType(ViewState("vsProcessCSPM"), String)
                Case "Insert"
                    nResult = InsertCSPMaster()
                Case "Update"
                    nResult = UpdateCSPMaster()
            End Select

            If nResult > 0 Then
                MessageBox.Show(SR.SaveSuccess)
                ClearData()
                BindDataGrid(0)
            End If

        Else
            If Not (Page.IsValid) Then
                MessageBox.Show("Masih Ada yang Belum Lengkap")
            End If
            'If ddlstatus.SelectedIndex = 0 Then
            '    MessageBox.Show("Status Harus dipilih")
            'End If
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        DisplayDataGrid()
        'If IsPostBack Then
        '    Dim m = String.Format("{0} - - {1}", ddlRefFormCode.SelectedIndex, ddlRefFormCode.SelectedItem.Text)
        '    MessageBox.Show(m)
        'End If
    End Sub

    Private Sub dtgCSPMaster_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgCSPMaster.ItemCommand
        Dim listData = CType(_sessHelper.GetSession("CSPMASTER"), CriteriaComposite)
        If (e.CommandName = "Activate") Then
            If MsgBox("Yakin data ini akan di Aktifkan ?", MsgBoxStyle.Information, "Informasi").Ok Then
                NonActiveAllDataActive()
                SetActive(Val(e.Item.Cells(0).Text))
            End If
        ElseIf e.CommandName = "Deactivate" Then
            If MsgBox("Yakin data ini akan di Non-Aktifkan ?", MsgBoxStyle.Information, "Informasi").Ok Then
                SetNonActive(e.Item.Cells(0).Text)
            End If
        ElseIf e.CommandName = "Delete" Then
            If MsgBox("Yakin ingin menghapus data ini ?", MsgBoxStyle.YesNo, "Informasi") = MsgBoxResult.Yes Then
                Try
                    DeleteCSPMaster(Val(e.Item.Cells(0).Text))
                Catch ex As Exception
                    MessageBox.Show(SR.DeleteFail)
                End Try
            End If
        ElseIf e.CommandName = "Edit" Then
            btnSimpan.Enabled = True

            ViewDataCSPMaster(Val(e.Item.Cells(0).Text), True)
            dtgCSPMaster.SelectedIndex = e.Item.ItemIndex

            ddlRefFormCode.Enabled = False
            ' ddlstatus.Enabled = False

        ElseIf e.CommandName = "View" Then

            Dim id As Integer = Val(e.Item.Cells(0).Text)
            Dim code As String = Val(e.Item.Cells(0).Text)
            ViewDataCSPMaster(id, False)
            dtgCSPMaster.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "GoToParameter" Then
            Dim id As Integer = Val(e.Item.Cells(0).Text)
            SendIDToCategory(id)
            Response.Redirect("FrmCcCSPerformanceParameter.aspx?MasID=" & id.ToString())
            'Server.Transfer("FrmCcCSPerformanceParameter.aspx?MasID=" & id.ToString())
        ElseIf e.CommandName = "cluster" Then
            Dim id As Integer = Val(e.Item.Cells(0).Text)
            Response.Redirect("FrmCcCSPerformanceCluster.aspx?masterid=" & id.ToString() & "&IsEdit=1")
        End If

    End Sub

    Private Sub dtgCSPMaster_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dtgCSPMaster.ItemDataBound
        Dim a = e.Item.ItemIndex

        If e.Item.ItemIndex <> -1 Then
            Dim periodFacade As New CcPeriodFacade(User)
            Dim model As CcCSPerformanceMaster = CType(e.Item.DataItem, CcCSPerformanceMaster)

            'Dim lbActive As LinkButton = CType(e.Item.FindControl("linkButonActive"), LinkButton)
            'Dim lbNonActive As LinkButton = CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton)

            'If model.Status = 0 Then
            '    'lbActive.Visible = False
            '    lbActive.Visible = False
            'Else
            '    'lbNonActive.Visible = False
            '    lbNonActive.Visible = False
            'End If

            Dim lblPeriodFrom As Label = CType(e.Item.FindControl("lblPeriodFrom"), Label)
            Dim lblPeriodTo As Label = CType(e.Item.FindControl("lblPeriodTo"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)


            If model.Status = 0 Then
                lblStatus.Text = "Lock"
                lbtnEdit.Visible = False
            ElseIf model.Status = 1 Then
                lblStatus.Text = "Open"
                lbtnEdit.Visible = True
            Else
                lblStatus.Text = String.Empty
                lbtnEdit.Visible = False
            End If

            If model.CcPeriodIDFrom <> 0 Then
                lblPeriodFrom.Text = periodFacade.Retrieve(model.CcPeriodIDFrom).YearMonth
            End If

            If model.CcPeriodIDTo <> 0 Then
                lblPeriodTo.Text = periodFacade.Retrieve(model.CcPeriodIDTo).YearMonth
            End If



        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCSPMaster.CurrentPageIndex * dtgCSPMaster.PageSize)
        End If
    End Sub

    Protected Sub btnBaru_Click(sender As Object, e As EventArgs) Handles btnBaru.Click
        ClearData()
    End Sub

    Private Sub ddlRefFormCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRefFormCode.SelectedIndexChanged
        If ddlRefFormCode.SelectedValue <> "" Then
            Dim objCSPMaster As CcCSPerformanceMaster

            objCSPMaster = New CcCSPerformanceMasterFacade(User).Retrieve(CType(ddlRefFormCode.SelectedValue, Integer))

            txtDescription.Text = objCSPMaster.Description
            lblCode.Text = objCSPMaster.Code
            '  ddlstatus.SelectedValue = objCSPMaster.Status

            ViewState("vsProcessCSPM") = "Insert"

        End If


    End Sub

    Private Sub dtgCSPMaster_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgCSPMaster.SortCommand
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
        dtgCSPMaster.SelectedIndex = -1
        BindDataGrid(dtgCSPMaster.CurrentPageIndex)
    End Sub


#End Region


End Class