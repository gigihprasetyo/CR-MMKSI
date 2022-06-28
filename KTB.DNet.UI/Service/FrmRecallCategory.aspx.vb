#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Service

#End Region


Public Class FrmRecallCategory
    Inherits System.Web.UI.Page

#Region "Variable"
    Private _sessHelper As SessionHelper = New SessionHelper
    Private bPrivilegeChangesCity As Boolean
    Private arlRecallCategory As ArrayList
    Private sessCriterias As String = "FrmRecallCategory.sessCriterias"
    Private _EditTable As Boolean = False


#End Region

#Region "Custom Method"
    Private Sub InitiateAuthorization()
        Dim oD As Dealer = CType(Session("DEALER"), Dealer)

        If Not SecurityProvider.Authorize(Context.User, SR.Recall_ListCategory_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - lihat Daftar Kategori Field Fix Campaign")
        End If

        If oD.Title <> EnumDealerTittle.DealerTittle.KTB Then
            _EditTable = False
        Else
            _EditTable = SecurityProvider.Authorize(Context.User, SR.Recall_InputCategory_Privilege)
        End If

        btnSimpan.Visible = _EditTable
    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)

        If txtDescription.Text.Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "Description", MatchType.Partial, txtDescription.Text().Trim()))
        End If
        If txtRecallRegNo.Text().Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RecallRegNo", MatchType.Partial, txtRecallRegNo.Text().Trim()))
        End If
        'If Me.ddlStatus.SelectedValue().ToString().Trim().Length > 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "Status", MatchType.Partial, ddlStatus.SelectedValue().ToString()))
        'End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "ValidStartDate", MatchType.GreaterOrEqual, icStartDate.Value))
        If txtBuletin.Text().Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "BuletinDescription", MatchType.Partial, txtBuletin.Text().Trim()))
        End If
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim TotalRow As Integer = 0
        If indexPage >= 0 Then
            arlRecallCategory = New RecallCategoryFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession(Me.sessCriterias), CriteriaComposite), indexPage + 1, dtgRecallCategory.PageSize, TotalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgRecallCategory.DataSource = arlRecallCategory
            dtgRecallCategory.VirtualItemCount = TotalRow
            dtgRecallCategory.DataBind()
        End If
    End Sub

    Private Sub ViewRecallCategory(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objRecallCategory As RecallCategory = New RecallCategoryFacade(User).Retrieve(nID)
        If Not objRecallCategory Is Nothing Then
            _sessHelper.SetSession("vsRecallCategory", objRecallCategory)
            'ViewState.Add("vsCity", objCity)
            txtDescription.Text = objRecallCategory.Description.ToString()
            txtRecallRegNo.Text = objRecallCategory.RecallRegNo.ToString()
            icStartDate.Value = objRecallCategory.ValidStartDate
            txtBuletin.Text = objRecallCategory.BuletinDescription
            txtRecallRegNo.ReadOnly = True
            txtRecallRegNo.Enabled = False
            lblREgCallNo.Text = objRecallCategory.RecallRegNo
            Me.btnSimpan.Enabled = EditStatus
        Else
            MessageBox.Show(SR.ViewFail)
        End If
    End Sub

    Private Sub DeleteRecallCategory(ByVal nID As Integer)
        Try
            Dim objRecallCategory As RecallCategory = New RecallCategoryFacade(User).Retrieve(nID)
            'objCity.RowStatus = DBRowStatus.Deleted
            If Not objRecallCategory Is Nothing Then

                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallChassisMaster), "RecallCategory.ID", MatchType.Exact, nID))

                Dim objFRCM As RecallCategoryFacade = New RecallCategoryFacade(User)
                Dim arr As ArrayList = New ArrayList()
                arr = New RecallChassisMasterFacade(User).Retrieve(criterias)

                If Not IsNothing(arr) AndAlso arr.Count > 0 Then
                    MessageBox.Show(SR.DeleteFail & ", Data sudah terpakai")
                    Return
                End If
                Dim objRecallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)
                objRecallCategoryFacade.Delete(objRecallCategory)
                ClearData()
                dtgRecallCategory.CurrentPageIndex = 0
                BindDatagrid(dtgRecallCategory.CurrentPageIndex)
            Else
                MessageBox.Show(SR.DeleteFail)
            End If


        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try



    End Sub

    Private Function UpdateRecallCategory() As Integer
        Dim objRecallCategory As RecallCategory = CType(Session.Item("vsRecallCategory"), RecallCategory)
        objRecallCategory.Description = txtDescription.Text.Trim()
        objRecallCategory.ValidStartDate = icStartDate.Value
        objRecallCategory.BuletinDescription = txtBuletin.Text

        Dim objRecallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)

        Try
            Return objRecallCategoryFacade.Update(objRecallCategory)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Private Sub ClearData()
        txtDescription.Text() = String.Empty
        txtRecallRegNo.Text() = String.Empty
        txtRecallRegNo.Enabled = True
        txtRecallRegNo.ReadOnly = False
        txtBuletin.Text = String.Empty
        icStartDate.Value = New DateTime(DateTime.Now.Year - 1, 1, 1)
        btnSimpan.Enabled = True
        dtgRecallCategory.SelectedIndex = -1
        btnSearch.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        lblREgCallNo.Text = "[AUTONUMBER]"

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()

        If Not IsPostBack Then
            icStartDate.Value = New DateTime(DateTime.Now.Year - 1, 1, 1)
            ClearData()
            ViewState("CurrentSortColumn") = "RecallRegNo"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            Search()
        End If
    End Sub

    'Add by Reza
    Private Sub Search()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        CreateCriteria(criterias)
        _sessHelper.SetSession(Me.sessCriterias, criterias)
        dtgRecallCategory.CurrentPageIndex = 0
        BindDatagrid(dtgRecallCategory.CurrentPageIndex)
        btnSimpan.Enabled = True
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'Edit by Reza
        Search()

        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '
        'CreateCriteria(criterias)
        '_sessHelper.SetSession(Me.sessCriterias, criterias)
        'dtgRecallCategory.CurrentPageIndex = 0
        'BindDatagrid(dtgRecallCategory.CurrentPageIndex)
        'btnSimpan.Enabled = True
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click

        If txtDescription.Text.Trim() = "" Then
            txtDescription.Focus()
            MessageBox.Show("Input Deskripsi")
            Return
        End If

        Select Case CType(ViewState("vsProcess"), String)
            Case "Insert"
                Try
                    Dim objRC As New RecallCategory
                    objRC.Description = txtDescription.Text.Trim()
                    objRC.ValidStartDate = icStartDate.Value
                    objRC.BuletinDescription = txtBuletin.Text
                    Dim i As Integer = New RecallCategoryFacade(User).Insert(objRC)
                    ClearData()
                    MessageBox.Show(SR.SaveSuccess)
                    btnSearch_Click(Me, Nothing)
                Catch ex As Exception
                    MessageBox.Show(SR.SaveFail)
                End Try
            Case "Edit"

                Try
                    UpdateRecallCategory()
                    ClearData()
                    MessageBox.Show(SR.SaveSuccess)
                    btnSearch_Click(Me, Nothing)
                Catch ex As Exception
                    MessageBox.Show(SR.SaveFail)
                End Try
        End Select


    End Sub

    Private Sub dtgRecallCategory_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgRecallCategory.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            dtgRecallCategory.SelectedIndex = e.Item.ItemIndex
            ViewRecallCategory(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewRecallCategory(e.Item.Cells(0).Text, True)
            btnSearch.Enabled = False
            dtgRecallCategory.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteRecallCategory(e.Item.Cells(0).Text)

            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        ElseIf e.CommandName = "AssignPositionCode" Then
            Dim strId As String = e.Item.Cells(5).Text
            Server.Transfer("./FrmRecallCategoryDetail.aspx?strId=" & strId & "&actionMode=First")
        End If
    End Sub

    Private Sub dtgRecallCategory_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgRecallCategory.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then

            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")

            Dim RowValue As RecallCategory = CType(e.Item.DataItem, RecallCategory)



            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgRecallCategory.CurrentPageIndex * dtgRecallCategory.PageSize)
                CType(e.Item.FindControl("lbAssignPositionCode"), LinkButton).Visible = SecurityProvider.Authorize(Context.User, SR.Recall_AssignLaborCode_Privilege)
            End If



            Dim oD As Dealer = CType(Session("DEALER"), Dealer)
            If oD.Title <> EnumDealerTittle.DealerTittle.KTB Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False

                Dim TotalRecall As Integer = 0
                Dim TOtalService As Integer = 0

                Try
                    Dim strTRecall As String = " SELECT	  [b].[ChassisNumber]  FROM	  [dbo].[ChassisMaster] b INNER JOIN dbo.[RecallChassisMaster] c ON c.[ChassisNo] = b.[ChassisNumber] 	 WHERE	  b.[RowStatus] = {0}  AND c.[RowStatus] =  {1}  AND b.[SoldDealerID] =  {2}    AND c.[RecallCategoryID] =  {3}  "
                    strTRecall = String.Format(strTRecall, CType(DBRowStatus.Active, Short).ToString(), CType(DBRowStatus.Active, Short).ToString(), oD.ID, RowValue.ID)

                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RecallCategory.ID", RowValue.ID))
                    criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ChassisNo", MatchType.InSet, "( " & strTRecall & " )"))
                    Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.RecallChassisMaster), "ID", AggregateType.Count)
                    Dim objFRCM As RecallChassisMasterFacade = New RecallChassisMasterFacade(User)

                    TotalRecall = objFRCM.RetrieveScalar(agg, criterias)

                    Dim strTRService As String = " SELECT	  [b].[ChassisNumber]  FROM	  [dbo].[ChassisMaster] b INNER JOIN dbo.[RecallChassisMaster] c ON c.[ChassisNo] = b.[ChassisNumber]  INNER JOIN dbo.[RecallService] d ON d.[ChassisMasterID] = b.[ID]  WHERE	  b.[RowStatus] = {0}  AND c.[RowStatus] =  {1} and d.[RowStatus] = {2}  AND b.[SoldDealerID] =  {3}    AND c.[RecallCategoryID] =  {4}  "
                    strTRService = String.Format(strTRService, CType(DBRowStatus.Active, Short).ToString(), CType(DBRowStatus.Active, Short).ToString(), CType(DBRowStatus.Active, Short).ToString(), oD.ID, RowValue.ID)

                    Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'criterias2.opAnd(New Criteria(GetType(RecallService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias2.opAnd(New Criteria(GetType(RecallService), "RecallChassisMaster.RecallCategory.ID", RowValue.ID))
                    criterias2.opAnd(New Criteria(GetType(RecallService), "ChassisMaster.Dealer.ID", MatchType.InSet, "( " & oD.ID & " )"))
                    Dim agg2 As Aggregate = New Aggregate(GetType(RecallService), "ID", AggregateType.Count)

                    Dim objFRS As RecallServiceFacade = New RecallServiceFacade(User)

                    TOtalService = objFRS.RetrieveScalar(agg2, criterias2)


                    If TotalRecall > 0 Then
                        CType(e.Item.FindControl("lblTotalService"), Label).Text = TOtalService.ToString("#,##0")
                        CType(e.Item.FindControl("lblTotalRecall"), Label).Text = TotalRecall.ToString("#,##0")
                        CType(e.Item.FindControl("lblPersentase"), Label).Text = (CDbl(TOtalService) / CDbl(TotalRecall) * 100.0).ToString("#,##0.##")
                    Else
                        CType(e.Item.FindControl("lblTotalService"), Label).Text = "0"
                        CType(e.Item.FindControl("lblTotalRecall"), Label).Text = "0"
                        CType(e.Item.FindControl("lblPersentase"), Label).Text = "0"
                    End If
                Catch ex As Exception

                End Try
            Else

                Try
                    Dim objVRC As VwRecallCategory = New VwRecallCategoryFacade(User).Retrieve(RowValue.ID)

                    If Not IsNothing(objVRC) Then
                        CType(e.Item.FindControl("lblTotalService"), Label).Text = objVRC.TotalService.ToString("#,##0")
                        CType(e.Item.FindControl("lblTotalRecall"), Label).Text = objVRC.TotalRecall.ToString("#,##0")
                        CType(e.Item.FindControl("lblPersentase"), Label).Text = (objVRC.Persentase * 100.0).ToString("#,##0.##")
                    End If
                Catch ex As Exception

                End Try
            End If


            If Not _EditTable Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
            End If



        End If

    End Sub

    Private Sub dtgRecallCategory_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgRecallCategory.PageIndexChanged

        dtgRecallCategory.SelectedIndex = -1
        dtgRecallCategory.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgRecallCategory.CurrentPageIndex)
        ClearData()

    End Sub

    Private Sub dtgRecallCategory_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgRecallCategory.SortCommand
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

        dtgRecallCategory.SelectedIndex = -1
        dtgRecallCategory.CurrentPageIndex = 0
        BindDatagrid(dtgRecallCategory.CurrentPageIndex)
        ClearData()
    End Sub
End Class
