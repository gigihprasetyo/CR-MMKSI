Imports KTB.DNet.Domain
Imports System.Net
Imports System.IO
Imports KTB.DNet.Utility

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Linq
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper

Imports IExcelDataReader = Excel.IExcelDataReader
Imports ExcelReaderFactory = Excel.ExcelReaderFactory

Public Class FrmCSPerformanceScoreList
    Inherits System.Web.UI.Page

    Private objCcCSPerformanceSubParameterScore As CcCSPerformanceSubParameterScore = New CcCSPerformanceSubParameterScore
    Dim arrList As ArrayList = New ArrayList
    Private sessHelper As New SessionHelper
    Dim IsAllowToEdit As Boolean
    Dim IsAllowToRead As Boolean
    Dim isEnable As Boolean = True

    Private Sub ActivateUserPrivilege()
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        IsAllowToEdit = True 'SecurityProvider.Authorize(Context.User, SR.Input_DiskonBudget_Privilege)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            If Not SecurityProvider.Authorize(Context.User, SR.CSP_Upload_Score_Privilage) Then

                Server.Transfer("../FrmAccessDenied.aspx?modulName=CS Performance Score")

                IsAllowToEdit = False
            Else
                IsAllowToEdit = True
            End If
        Else
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Discount Proposal - Upload Score Sub Parameter CS Performance")
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindToDDL()

        End If
    End Sub


    Private Sub DownloadTemplate(ByVal fileName)
        Dim strName As String = fileName
        Response.Redirect("../downloadlocal.aspx?file=" & strName)
    End Sub



    Private Function IsNumeric(ByVal value As String) As Boolean
        Dim result As Integer
        Return Decimal.TryParse(value, result)
    End Function



    Private Sub BindDataGrid(ByVal arrListData As ArrayList)
        dtgUploadCSPerformanceScore.DataSource = arrListData
        dtgUploadCSPerformanceScore.DataBind()
    End Sub



    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim arrListData As ArrayList = CType(sessHelper.GetSession("CSPerformanceUploadScoreData"), ArrayList)
        If arrListData.Count <> 0 Then
            'SortListControl(arrListData, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrListData, pageIndex, dtgUploadCSPerformanceScore.PageSize)
            dtgUploadCSPerformanceScore.DataSource = PagedList
            dtgUploadCSPerformanceScore.VirtualItemCount = arrListData.Count()
            dtgUploadCSPerformanceScore.DataBind()
        Else
            dtgUploadCSPerformanceScore.DataSource = New ArrayList
            dtgUploadCSPerformanceScore.VirtualItemCount = 0
            dtgUploadCSPerformanceScore.CurrentPageIndex = 0
            dtgUploadCSPerformanceScore.DataBind()
        End If
        If dtgUploadCSPerformanceScore.VirtualItemCount > 0 Then
            'lblJumRecord.Text = "Jumlah record : " & dtgUploadCSPerformanceScore.VirtualItemCount
        End If
    End Sub

    Private Function CheckStatusDataUpload(ByVal objModel As CcCSPerformanceSubParameterScore) As String
        Dim status As String
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "RowStatus", MatchType.Exact, CType(0, Short)))

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "DealerID", MatchType.Exact, (objModel.DealerID)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcPeriodID", MatchType.Exact, objModel.CcPeriodID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcCustomerCategoryID", MatchType.Exact, objModel.CcCustomerCategoryID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcVehicleCategoryID", MatchType.Exact, objModel.CcVehicleCategoryID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcCSPerformanceSubParameterCode", MatchType.Exact, objModel.CcCSPerformanceSubParameterCode))

        Dim queryResult As ArrayList = New CcCSPerformanceSubParameterScoreFacade(User).Retrieve(criterias)

        If queryResult.Count > 0 Then
            status = "Update"
        Else
            status = "OK"
        End If

        Return status
    End Function

    Private Sub dtgUploadCSPerformanceScore_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUploadCSPerformanceScore.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain As CcCSPerformanceSubParameterScore = CType(e.Item.DataItem, CcCSPerformanceSubParameterScore)
            If Not objDomain Is Nothing Then
                Dim sess = ViewState("EditUpload")


                'Dim objDealer As Dealer = New DealerFacade(User).GetDealer(objDomain.DealerID)
                Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
                lblKodeDealer.Text = objDomain.Dealer.DealerCode

                Dim lblNamaDealer As Label = CType(e.Item.FindControl("lblNamaDealer"), Label)
                lblNamaDealer.Text = objDomain.Dealer.DealerName

                Dim lblPeriode As Label = CType(e.Item.FindControl("lblPeriode"), Label)
                lblPeriode.Text = objDomain.CcPeriod.YearMonth

                Dim lblPelayanan As Label = CType(e.Item.FindControl("lblPelayanan"), Label)
                lblPelayanan.Text = objDomain.CcCustomerCategory.Description

                'Dim lblJenisKendaraan As Label = CType(e.Item.FindControl("lblJenisKendaraan"), Label)
                'lblJenisKendaraan.Text = objDomain.CcVehicleCategory.Description

                Dim lblSubParameter As Label = CType(e.Item.FindControl("lblSubParameter"), Label)
                lblSubParameter.Text = objDomain.CcCSPerformanceSubParameter.Name

                If Not ViewState("EditUpload") = "EditUpload" Then
                    Dim lblScore As Label = CType(e.Item.FindControl("lblScore"), Label)
                    lblScore.Text = objDomain.ParameterScore.ToString("#,##0.00")
                End If



            End If
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgUploadCSPerformanceScore.CurrentPageIndex * dtgUploadCSPerformanceScore.PageSize)
        End If
    End Sub

    Private Function CollectionData() As IList
        Dim sessionData = sessHelper.GetSession("CSPerformanceUploadScoreData")
        Return sessionData
    End Function


    Private Function ProcessSave(ByVal objModel As CcCSPerformanceSubParameterScore) As Integer
        Dim retVal As Integer

        If objModel.StatusUpload = "Update" Then
            retVal = New CcCSPerformanceSubParameterScoreFacade(User).Update(objModel)
        ElseIf objModel.StatusUpload = "OK" Then
            retVal = New CcCSPerformanceSubParameterScoreFacade(User).Insert(objModel)
        Else
            If CheckDataDiscountBudgetBeforeUpdateDouble(objModel.DealerID) >= 1 Then
                retVal = New CcCSPerformanceSubParameterScoreFacade(User).Update(objModel)
            End If
        End If

        Return retVal
    End Function

    Private Function CheckDataDiscountBudgetBeforeUpdateDouble(ByVal dealerId As Integer) As Integer
        Dim dataCount As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "RowStatus", MatchType.Exact, CType(0, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "DealerID", MatchType.Exact, (dealerId)))

        Dim countResult = New CcCSPerformanceSubParameterScoreFacade(User).Retrieve(criterias)

        dataCount = countResult.count
        Return dataCount
    End Function

    Private Sub EditDataScore(ByRef ObjModel As CcCSPerformanceSubParameterScore, ByVal e As DataGridCommandEventArgs)
        If Not IsNumeric(GetValueEdit("txtEditScore", e)) Then
            ObjModel.StatusUpload = "Score tidak valid"
            isEnable = False
       
        Else
            ObjModel.StatusUpload = "Update"
            ObjModel.ParameterScore = GetValueEdit("txtEditScore", e)
        End If

    End Sub

    Private Function GetValueEdit(ByVal ControlName As String, ByVal e As DataGridCommandEventArgs) As String
        Return CType(e.Item.FindControl(ControlName), TextBox).Text
    End Function

    Private Function GetValueEditString(ByVal ControlName As String, ByVal e As DataGridCommandEventArgs) As String
        Dim val As String
        Try
            val = CType(e.Item.FindControl(ControlName), DropDownList).Text
            Return val
        Catch ex As Exception
            CType(e.Item.FindControl(ControlName), DropDownList).Text = ""
        End Try
        Return ""
    End Function

    Private Sub isEditEnable(ByVal isEnable As Boolean, ByVal e As DataGridCommandEventArgs)
        'Dim linkEdit = CType(e.Item.FindControl(""), LinkButton).Visible = isEnable
        'Dim linkEdit = dtgUploadCSPerformanceScore.FindControl("lbtnEdit").Visible = isEnable

        CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = isEnable

    End Sub

    Private Sub dtgUploadCSPerformanceScore_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgUploadCSPerformanceScore.ItemCommand
        Dim arrListData = CType(sessHelper.GetSession("CSPerformanceUploadScoreData"), ArrayList)
        Dim PagedList As ArrayList = ArrayListPager.DoPage(arrListData, dtgUploadCSPerformanceScore.CurrentPageIndex, dtgUploadCSPerformanceScore.PageSize)
        Dim intRetVal As Integer
        Dim intActualItemIndex As Integer

        intActualItemIndex = (dtgUploadCSPerformanceScore.CurrentPageIndex * dtgUploadCSPerformanceScore.PageSize) + e.Item.ItemIndex


        If e.CommandName = "Edit" Then
            ViewState.Add("EditUpload", "EditUpload")
            dtgUploadCSPerformanceScore.EditItemIndex = e.Item.ItemIndex
            'dtgUploadCSPerformanceScore.ShowFooter = False

            BindPage(dtgUploadCSPerformanceScore.CurrentPageIndex)

        ElseIf e.CommandName = "Delete" Then
            Dim objModel As CcCSPerformanceSubParameterScore = CType(PagedList(e.Item.ItemIndex), CcCSPerformanceSubParameterScore)
            objModel.RowStatus = DBRowStatus.Deleted
            intRetVal = ProcessSave(objModel)

            If intRetVal <> -1 Then
                arrListData.RemoveAt(intActualItemIndex)
            End If

            BindPage(dtgUploadCSPerformanceScore.CurrentPageIndex)
            'ReloadDataAfterAction(btnSimpan, arrListData)

        ElseIf e.CommandName = "Cancel" Then
            dtgUploadCSPerformanceScore.EditItemIndex = -1
            'dtgUploadCSPerformanceScore.ShowFooter = True
            BindPage(dtgUploadCSPerformanceScore.CurrentPageIndex)
        ElseIf e.CommandName = "Save" Then
            ViewState.Add("EditUpload", "SaveChange")

            Dim objModel As CcCSPerformanceSubParameterScore = CType(PagedList(e.Item.ItemIndex), CcCSPerformanceSubParameterScore)

            EditDataScore(objModel, e)

            If objModel.StatusUpload = "Update" Or objModel.StatusUpload = "OK" Then
                intRetVal = ProcessSave(objModel)

                If intRetVal <> -1 Then
                    arrListData(intActualItemIndex) = objModel
                End If
                'Else
                '    MsgBox(objModel.StatusUpload)
            End If


            dtgUploadCSPerformanceScore.EditItemIndex = -1
            'dtgUploadCSPerformanceScore.ShowFooter = True

            'ReloadDataAfterAction(btnSimpan, arrListData)
            BindPage(dtgUploadCSPerformanceScore.CurrentPageIndex)

            sessHelper.SetSession("CSPerformanceUploadScoreData", arrListData)
        End If
    End Sub


    Private Function isDealerValid(ByVal strVal As String) As Boolean
        Dim isValid As Boolean = False

        Dim crit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strVal))

        Dim objDealerList As ArrayList = New DealerFacade(User).RetrieveByCriteria(crit)

        If objDealerList.Count > 0 Then
            isValid = True
        End If

        Return isValid
    End Function




    Private Sub ClearDataGridUpload()
        dtgUploadCSPerformanceScore.DataSource = ""
        dtgUploadCSPerformanceScore.DataBind()
        'btnSimpan.Enabled = False
    End Sub

    'Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
    '    ClearDataGridUpload()
    'End Sub


    Private Sub BindToDDL()

        ' this is for what..?

        'cRETIRIA CcPeriod
        Dim DtStart As DateTime = DateTime.Now.AddMonths(-20)
        Dim DtEnd As DateTime = DateTime.Now.AddMonths(-1)

        '------------------------------------------------------------------------------------------------------------------------------------------
        Dim criteriaCCperiod As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaCCperiod.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcPeriod), "YearMonth", MatchType.GreaterOrEqual, DtStart.ToString("yyyyMM")))
        criteriaCCperiod.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcPeriod), "YearMonth", MatchType.LesserOrEqual, DtEnd.ToString("yyyyMM")))

        ' artinya criteriacomposite untuk mengenerate dinamic query. dinamic querynya yaitu "select * form ccperiod where rowstatus = 0 and yearmonth >= dtstart and yearmonth <= DateTime.Now.ToString("yyyyMM")))"
        ' jadi bukan retrivebycriteria yang akan mengexecusi SP nya retrivenya
        'opand artinya adalah 'AND'
        Dim arrayListPeriode As ArrayList = New CcPeriodFacade(User).RetrieveByCriteria(criteriaCCperiod, "YearMonth", Sort.SortDirection.DESC)


        '------------------------------------------------------------------------------------------------------------------------------------------
        Dim criteriaVehicleCategory As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcVehicleCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criteriaVehicleCategory.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcVehicleCategory), "Code", MatchType.Exact, "CV"))
        Dim arrayListJenisKendaraan As ArrayList = New CcVehicleCategoryFacade(User).RetrieveByCriteria(criteriaVehicleCategory)


        '------------------------------------------------------------------------------------------------------------------------------------------
        Dim arrayPelayanan As ArrayList = New CcCustomerCategoryFacade(User).RetrieveActiveList


        '------------------------------------------------------------------------------------------------------------------------------------------
        Dim criteriaSubParameter As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaSubParameter.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "Type", MatchType.Exact, 1))
        criteriaSubParameter.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "CcCSPerformanceParameter.FunctionName", MatchType.Exact, 0))
        criteriaSubParameter.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "CcCSPerformanceMaster.Status", MatchType.Exact, 0))
        Dim arrayListSubParameter As ArrayList = New CcCSPerformanceSubParameterFacade(User).RetrieveByCriteria(criteriaSubParameter)


        ddlPelayanan.Items.Clear()
        ddlPeriode.Items.Clear()
        DdlJenisKendaraan.Items.Clear()
        ddlSubParameter.Items.Clear()

        Dim listitemBlank As New ListItem("Silahkan Pilih", "")
        ddlPelayanan.Items.Add(listitemBlank)
        ddlPeriode.Items.Add(listitemBlank)
        DdlJenisKendaraan.Items.Add(listitemBlank)
        ddlSubParameter.Items.Add(listitemBlank)


        For Each item As CcCustomerCategory In arrayPelayanan
            Dim listItem As New ListItem(item.Description, item.ID)
            ddlPelayanan.Items.Add(listItem)
        Next

        For Each item As CcPeriod In arrayListPeriode
            Dim listItem As New ListItem(item.YearMonth, item.ID)
            ddlPeriode.Items.Add(listItem)
        Next

        For Each item As CcVehicleCategory In arrayListJenisKendaraan
            Dim listItem As New ListItem(item.Description, item.ID)
            DdlJenisKendaraan.Items.Add(listItem)
        Next

        For Each item As CcCSPerformanceSubParameter In arrayListSubParameter
            Dim listItem As New ListItem(item.Name, item.Code)
            ddlSubParameter.Items.Add(listItem)
        Next

    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "RowStatus", MatchType.Exact, CType(0, Short)))

        If txtKodeDealer.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If


        If ddlPeriode.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcPeriodID", MatchType.Exact, ddlPeriode.SelectedValue))
        End If

        If ddlPelayanan.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcCustomerCategoryID", MatchType.Exact, ddlPelayanan.SelectedValue))
        End If

        'If DdlJenisKendaraan.SelectedIndex > 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcVehicleCategoryID", MatchType.Exact, DdlJenisKendaraan.SelectedValue))
        'End If

        If ddlMaster.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcCSPerformanceSubParameter.CcCSPerformanceMaster.ID", MatchType.Exact, ddlMaster.SelectedValue))
        End If

        If ddlParameter.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcCSPerformanceSubParameter.CcCSPerformanceParameter.ID", MatchType.Exact, ddlParameter.SelectedValue))
        End If

        If ddlSubParameter.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcCSPerformanceSubParameterCode", MatchType.Exact, ddlSubParameter.SelectedValue))
        End If

        Dim arrListData As ArrayList = New CcCSPerformanceSubParameterScoreFacade(User).Retrieve(criterias)

        sessHelper.SetSession("CSPerformanceUploadScoreData", arrListData)
        BindPage(dtgUploadCSPerformanceScore.CurrentPageIndex)
    End Sub

    Private Sub dtgUploadCSPerformanceScore_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgUploadCSPerformanceScore.PageIndexChanged
        dtgUploadCSPerformanceScore.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)
    End Sub



    Protected Sub ddlMaster_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ddlParameter.ClearSelection()
            ddlSubParameter.ClearSelection()
            ddlParameter.Items.Clear()
            ddlSubParameter.Items.Clear()

            If Not ddlMaster.SelectedValue = "" Or Not ddlMaster.SelectedValue = "0" Then
                Dim funcParameter As New CcCSPerformanceParameterFacade(Me.User)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                If Not String.IsNullorEmpty(ddlMaster.SelectedValue) Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceParameter), "CcCSPerformanceMaster.ID", MatchType.Exact, ddlMaster.SelectedValue))
                End If

                If Not String.IsNullorEmpty(ddlPelayanan.SelectedValue) Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceParameter), "CcCustomerCategory.ID", MatchType.Exact, ddlPelayanan.SelectedValue))
                End If

                Dim arrParameter As ArrayList = funcParameter.Retrieve(criterias)
                For Each iParameter As CcCSPerformanceParameter In arrParameter
                    ddlParameter.Items.Add(New ListItem(iParameter.Name, iParameter.ID.ToString))
                Next
                ddlParameter.Items.Insert(0, New ListItem("Silahkan pilih", "0"))
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Protected Sub ddlParameter_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ddlSubParameter.ClearSelection()
            ddlSubParameter.Items.Clear()

            If Not ddlParameter.SelectedValue = "" Or Not ddlParameter.SelectedValue = "0" Then
                Dim funcSubParameter As New CcCSPerformanceSubParameterFacade(Me.User)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "CcCSPerformanceParameter.ID", MatchType.Exact, ddlParameter.SelectedValue))

                Dim arrSubParameter As ArrayList = funcSubParameter.Retrieve(criterias)
                For Each iSubParameter As CcCSPerformanceSubParameter In arrSubParameter
                    ddlSubParameter.Items.Add(New ListItem(iSubParameter.Name, iSubParameter.ID.ToString))
                Next
                ddlSubParameter.Items.Insert(0, New ListItem("Silahkan pilih", "0"))
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ddlPelayanan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPelayanan.SelectedIndexChanged
        Try
            ddlParameter.ClearSelection()
            ddlSubParameter.ClearSelection()
            ddlParameter.Items.Clear()
            ddlSubParameter.Items.Clear()

            If Not ddlMaster.SelectedValue = "" Or Not ddlMaster.SelectedValue = "0" Then
                Dim funcParameter As New CcCSPerformanceParameterFacade(Me.User)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                If Not String.IsNullorEmpty(ddlMaster.SelectedValue) Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceParameter), "CcCSPerformanceMaster.ID", MatchType.Exact, ddlMaster.SelectedValue))
                End If

                If Not String.IsNullorEmpty(ddlPelayanan.SelectedValue) Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceParameter), "CcCustomerCategory.ID", MatchType.Exact, ddlPelayanan.SelectedValue))
                End If

                Dim arrParameter As ArrayList = funcParameter.Retrieve(criterias)
                For Each iParameter As CcCSPerformanceParameter In arrParameter
                    ddlParameter.Items.Add(New ListItem(iParameter.Name, iParameter.ID.ToString))
                Next
                ddlParameter.Items.Insert(0, New ListItem("Silahkan pilih", "0"))
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub ddlPeriode_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles ddlPeriode.SelectedIndexChanged
        Try
            ddlMaster.ClearSelection()
            ddlParameter.ClearSelection()
            ddlSubParameter.ClearSelection()
            ddlMaster.Items.Clear()
            ddlParameter.Items.Clear()
            ddlSubParameter.Items.Clear()

            If Not ddlPeriode.SelectedValue = "" Or Not ddlPeriode.SelectedValue = "0" Then
                Dim funcMaster As New CcCSPerformanceMasterFacade(Me.User)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "CcPeriodIDFrom", MatchType.LesserOrEqual, ddlPeriode.SelectedValue))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceMaster), "CcPeriodIDTo", MatchType.GreaterOrEqual, ddlPeriode.SelectedValue))

                Dim arrMaster As ArrayList = funcMaster.Retrieve(criterias)
                For Each iMaster As CcCSPerformanceMaster In arrMaster
                    ddlMaster.Items.Add(New ListItem(iMaster.Description, iMaster.ID.ToString))
                Next
                ddlMaster.Items.Insert(0, New ListItem("Silahkan pilih", "0"))
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class