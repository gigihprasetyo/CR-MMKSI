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

Imports IExcelDataReader = Excel.IExcelDataReader
Imports ExcelReaderFactory = Excel.ExcelReaderFactory

Public Class FrmUploadScoreCSPerformance
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
            btnSimpan.Enabled = False

        End If
    End Sub


    Private Sub DownloadTemplate(ByVal fileName)
        Dim strName As String = fileName
        Response.Redirect("../downloadlocal.aspx?file=" & strName)
    End Sub

    Protected Sub btnLinkDownloadTemplate_Click(sender As Object, e As EventArgs) Handles btnLinkDownloadTemplate.Click
        Dim fileName As String
        fileName = "Template-SubParameter.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\CS\" & fileName)
        '    DownloadTemplate(fileName)
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim sourceFile As String
        Dim targetFile As String

        If isheaderComplete() Then

            sourceFile = Path.GetFileName(fileUploadBudget.PostedFile.FileName)
            targetFile = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.Year.ToString() & "\" & DateTime.Now.Month.ToString() & "\" & DateTime.Now.ToString("ddMMyyyHHmmss")

            ReadExcelFile(sourceFile, targetFile)

            Dim arrList = sessHelper.GetSession("CSPerformanceScoreUploadData")
            BindDataGrid(arrList)
        Else
            MessageBox.Show("Silahkan Melengkapi Data Header (*)")
        End If

    End Sub

    Private Function isheaderComplete() As Boolean


        'If DdlJenisKendaraan.SelectedIndex = 0 Then
        '    Return False
        'End If

        If ddlPeriode.SelectedIndex = 0 Then
            Return False
        End If

        If ddlPelayanan.SelectedIndex = 0 Then
            Return False
        End If

        If ddlSubParameter.SelectedIndex = 0 Then
            Return False
        End If

        Return True

    End Function

    Private Sub ReadExcelFile(ByVal sourceFile As String, ByVal targetFile As String)
        Dim objUpload As New UploadToWebServer
        Dim objReader As IExcelDataReader = Nothing
        Dim ext As String = System.IO.Path.GetExtension(fileUploadBudget.PostedFile.FileName)
        Dim retVal As Integer = 0

        objUpload.Upload(fileUploadBudget.PostedFile.InputStream, targetFile)
        Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)
            Dim is2007 As Integer = 0

            If ext.ToLower.Contains("xlsx") Then
                objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                objReader.IsFirstRowAsColumnNames = False
                is2007 = 0
            Else
                objReader = ExcelReaderFactory.CreateBinaryReader(stream)
            End If

            Dim i As Integer = 0
            If (Not IsNothing(objReader)) Then
                Dim arr As ArrayList = New ArrayList
                arr.Add(objReader.Read())
                While objReader.Read()
                    If i >= 0 Then

                        Dim objCcCSPerformanceSubParameterScore As CcCSPerformanceSubParameterScore = New CcCSPerformanceSubParameterScore

                        If objReader.GetString(0).Trim = "" Then
                            objCcCSPerformanceSubParameterScore.StatusUpload = "Kode Dealer tidak valid"
                            isEnable = False
                        ElseIf Not isDealerValid(objReader.GetString(0).Trim) Then
                            objCcCSPerformanceSubParameterScore.StatusUpload = "Kode Dealer tidak valid"
                            isEnable = False
                        ElseIf Not IsNumeric(objReader.GetString(1).Trim) Then
                            objCcCSPerformanceSubParameterScore.StatusUpload = "Score tidak valid"
                            isEnable = False
                        ElseIf Not isScoreValid(ddlSubParameter.SelectedValue, objReader.GetString(1).Trim) Then
                            objCcCSPerformanceSubParameterScore.StatusUpload = "Score tidak valid"
                            isEnable = False
                        Else
                            objCcCSPerformanceSubParameterScore.StatusUpload = CheckStatusDataUpload(objCcCSPerformanceSubParameterScore, New DealerFacade(User).GetDealer(objReader.GetString(0)).ID)
                        End If

                        Dim objDealer As Dealer = New DealerFacade(User).GetDealer(objReader.GetString(0))

                        objCcCSPerformanceSubParameterScore.DealerID = objDealer.ID
                        objCcCSPerformanceSubParameterScore.CcPeriodID = ddlPeriode.SelectedValue
                        objCcCSPerformanceSubParameterScore.CcCustomerCategoryID = ddlPelayanan.SelectedValue
                        'objCcCSPerformanceSubParameterScore.CcVehicleCategoryID = DdlJenisKendaraan.SelectedValue
                        objCcCSPerformanceSubParameterScore.CcCSPerformanceSubParameterCode = ddlSubParameter.SelectedValue

                        If IsNumeric(objReader.GetString(1)) Then
                            objCcCSPerformanceSubParameterScore.ParameterScore = objReader.GetString(1)
                        Else
                            objCcCSPerformanceSubParameterScore.ParameterScore = 0
                            retVal += 1
                        End If
                        objCcCSPerformanceSubParameterScore.SubFunction = txtSubFunction.Text

                        objCcCSPerformanceSubParameterScore.RowStatus = 0
                        objCcCSPerformanceSubParameterScore.CreatedBy = ""
                        objCcCSPerformanceSubParameterScore.CreatedTime = DateTime.Now
                        objCcCSPerformanceSubParameterScore.LastUpdateBy = User.Identity.Name
                        objCcCSPerformanceSubParameterScore.LastUpdateTime = DateTime.Now

                        arrList.Add(objCcCSPerformanceSubParameterScore)
                    End If

                    i = i + 1
                End While

                If Not retVal >= 1 Then
                    If arrList.Count > 0 Then btnSimpan.Enabled = isEnable
                    sessHelper.SetSession("CSPerformanceScoreUploadData", arrList)
                Else
                    MessageBox.Show("Mohon koreksi kembali data score")
                End If
            End If
        End Using
    End Sub

    Private Function IsNumeric(ByVal value As String) As Boolean
        Dim result As Integer
        Return Decimal.TryParse(value, result)
    End Function


    Private Sub ReloadDataAfterAction(ByVal btnSimpan As Button, ByVal dataArray As ArrayList)
        Dim data As ArrayList

        Dim countStatus = CheckStatusUploadAfterEdit(dataArray)
        If countStatus >= 1 Then
            btnSimpan.Enabled = False
        Else
            btnSimpan.Enabled = True
        End If
    End Sub

    Private Function CheckStatusUploadAfterEdit(ByVal dataarray As ArrayList) As Integer
        Dim arrayData As New ArrayList

        For Each sourceModel As CcCSPerformanceSubParameterScore In (From a As CcCSPerformanceSubParameterScore In dataarray Where a.StatusUpload = "Kode Dealer tidak valid" Or a.StatusUpload = "Score tidak valid")
            Dim model As New CcCSPerformanceSubParameterScore
            model.StatusUpload = sourceModel.StatusUpload
            arrayData.Add(model)
        Next

        Return arrayData.Count
    End Function

    Private Function isDataExcelDouble(ByVal data As ArrayList, ByVal obj As CcCSPerformanceSubParameterScore) As Boolean
        Dim isDouble As Boolean = False
        For Each m As CcCSPerformanceSubParameterScore In data
            If obj.DealerID = m.DealerID And obj.ParameterScore = m.ParameterScore Then
                isDouble = True
            End If
        Next
        Return isDouble
    End Function


    Private Sub BindDataGrid(ByVal arrListData As ArrayList)
        dtgUploadCSPerformanceScore.DataSource = arrListData
        dtgUploadCSPerformanceScore.DataBind()
    End Sub

    Private Function CheckStatusDataUpload(ByRef objDomain As CcCSPerformanceSubParameterScore, ByVal dealerId As Integer) As String
        Dim status As String
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "RowStatus", MatchType.Exact, CType(0, Short)))

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "DealerID", MatchType.Exact, (dealerId)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcPeriodID", MatchType.Exact, ddlPeriode.SelectedValue))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcCustomerCategoryID", MatchType.Exact, ddlPelayanan.SelectedValue))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcVehicleCategoryID", MatchType.Exact, DdlJenisKendaraan.SelectedValue))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameterScore), "CcCSPerformanceSubParameterCode", MatchType.Exact, ddlSubParameter.SelectedValue))

        Dim queryResult As ArrayList = New CcCSPerformanceSubParameterScoreFacade(User).Retrieve(criterias)

        If queryResult.Count > 0 Then
            status = "Update"
            objDomain.ID = CType(queryResult(0), CcCSPerformanceSubParameterScore).ID
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


                Dim objDealer As Dealer = New DealerFacade(User).GetDealer(objDomain.DealerID)

                If Not IsNothing(objDealer) Then

                    Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
                    lblKodeDealer.Text = objDealer.DealerCode

                    Dim lblNamaDealer As Label = CType(e.Item.FindControl("lblNamaDealer"), Label)
                    lblNamaDealer.Text = objDealer.DealerName

                End If

                If Not ViewState("EditUpload") = "EditUpload" Then
                    Dim lblScore As Label = CType(e.Item.FindControl("lblScore"), Label)
                    lblScore.Text = objDomain.ParameterScore.ToString("#,##0.00")
                End If

                Dim lblKeterangan As Label = CType(e.Item.FindControl("lblKeterangan"), Label)
                lblKeterangan.Text = objDomain.StatusUpload

            End If
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgUploadCSPerformanceScore.CurrentPageIndex * dtgUploadCSPerformanceScore.PageSize)
        End If
    End Sub

    Private Function CollectionData() As IList
        Dim sessionData = sessHelper.GetSession("CSPerformanceScoreUploadData")
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
        ElseIf Not isScoreValid(ddlSubParameter.SelectedValue, GetValueEdit("txtEditScore", e)) Then
            ObjModel.StatusUpload = "Score tidak valid"
            isEnable = False
        Else
            ObjModel.StatusUpload = CheckStatusDataUpload(ObjModel, ObjModel.DealerID)
        End If

        btnSimpan.Enabled = isEnable
        ObjModel.ParameterScore = GetValueEdit("txtEditScore", e)
    End Sub

    Private Function GetValueEdit(ByVal ControlName As String, ByVal e As DataGridCommandEventArgs) As Decimal
        Dim val As Decimal = 0
        Try
            Try
                val = CDec(CType(e.Item.FindControl(ControlName), TextBox).Text)
                Return val
            Catch ex As Exception
                CType(e.Item.FindControl(ControlName), TextBox).Text = 0
            End Try
        Catch exx As Exception

        End Try
        Return 0
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
        Dim arrListData = CType(sessHelper.GetSession("CSPerformanceScoreUploadData"), ArrayList)
        If e.CommandName = "Edit" Then
            ViewState.Add("EditUpload", "EditUpload")
            dtgUploadCSPerformanceScore.EditItemIndex = e.Item.ItemIndex
            'dtgUploadCSPerformanceScore.ShowFooter = False

            BindDataGrid(arrListData)

        ElseIf e.CommandName = "Delete" Then
            arrListData.RemoveAt(e.Item.ItemIndex)
            BindDataGrid(arrListData)
            ReloadDataAfterAction(btnSimpan, arrListData)

        ElseIf e.CommandName = "Cancel" Then
            dtgUploadCSPerformanceScore.EditItemIndex = -1
            'dtgUploadCSPerformanceScore.ShowFooter = True
            BindDataGrid(arrListData)
        ElseIf e.CommandName = "Save" Then
            ViewState.Add("EditUpload", "SaveChange")
            Dim objModel = CType(arrListData(e.Item.ItemIndex), CcCSPerformanceSubParameterScore)

            EditDataScore(objModel, e)
            arrListData(e.Item.ItemIndex) = objModel


            dtgUploadCSPerformanceScore.EditItemIndex = -1
            'dtgUploadCSPerformanceScore.ShowFooter = True

            'ReloadDataAfterAction(btnSimpan, arrListData)
            BindDataGrid(arrListData)
            ReloadDataAfterAction(btnSimpan, arrListData)

            sessHelper.SetSession("CSPerformanceScoreUploadData", arrListData)
        End If
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim listData = CollectionData()
        Dim retVal As Integer = 0
        Dim retvalVal As Integer
        Dim strMsg As String


        For Each item As CcCSPerformanceSubParameterScore In listData
            'Dim objModel As New CcCSPerformanceSubParameterScore

            'objModel.DealerID = item.DealerID
            'objModel.CcPeriodID = item.CcPeriodID
            'objModel.CcCustomerCategoryID = item.CcCustomerCategoryID
            'objModel.CcVehicleCategoryID = item.CcVehicleCategoryID
            'objModel.CcCSPerformanceSubParameterCode = item.CcCSPerformanceSubParameterCode
            'objModel.ParameterScore = item.ParameterScore
            'objModel.RowStatus = item.RowStatus
            'objModel.CreatedBy = item.CreatedBy
            'objModel.CreatedTime = item.CreatedTime
            'objModel.LastUpdateBy = item.LastUpdateBy
            'objModel.LastUpdateTime = item.LastUpdateTime
            'objModel.StatusUpload = item.StatusUpload
            retVal = ProcessSave(item)
            strMsg = "Dealer Code : " & item.DealerID & ", Score = " & item.ParameterScore
            If retVal = -1 Then Exit For
        Next

        If retVal <> -1 Then
            MessageBox.Show("Data Sukses Tersimpan!")
            ClearDataGridUpload()
        Else
            MessageBox.Show(SR.SaveFail & "\n" & strMsg)
        End If

Failed:
        'MessageBox.Show(SR.SaveFail)
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



    Private Function isScoreValid(ByVal strSPCode As String, ByVal strScore As String) As Boolean
        Dim isValid As Boolean = False

        Dim crit As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "ID", MatchType.Exact, strSPCode))
        'crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcCSPerformanceSubParameter), "CcCSPerformanceMaster.Status", MatchType.Exact, 0))

        Dim objSPList As ArrayList = New CcCSPerformanceSubParameterFacade(User).RetrieveByCriteria(crit)

        If objSPList.Count > 0 Then
            'Dim objSP As CcCSPerformanceSubParameter = objSPList(0)
            'If objSP.MaxValue >= CType(strScore, Decimal) Then isValid = True
            Dim crits As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DRMasterGeneral), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.DRMasterGeneral), "GeneralName", MatchType.Exact, "Maximum Point"))
            crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.DRMasterGeneral), "CcCustomerCategoryID", MatchType.Exact, ddlPelayanan.SelectedValue))
            crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.DRMasterGeneral), "ProductCategory", MatchType.Exact, "MMC"))

            Dim arrMaxValue As ArrayList = New DRMasterGeneralFacade(Me.User).Retrieve(crits)
            If arrMaxValue.Count > 0 Then
                Dim objMaxValue As DRMasterGeneral = arrMaxValue(0)
                If Decimal.Parse(objMaxValue.GeneralValue) >= CType(strScore, Decimal) Then
                    isValid = True
                End If

            End If

        End If

        Return isValid
    End Function

    Private Sub ClearDataGridUpload()
        dtgUploadCSPerformanceScore.DataSource = ""
        dtgUploadCSPerformanceScore.DataBind()
        btnSimpan.Enabled = False
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        ddlPeriode.SelectedIndex = 0
        ddlPelayanan.SelectedIndex = 0
        DdlJenisKendaraan.SelectedIndex = 0
        ddlSubParameter.SelectedIndex = 0
        ClearDataGridUpload()
    End Sub


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

        'For Each item As CcCSPerformanceSubParameter In arrayListSubParameter
        '    Dim listItem As New ListItem(item.Name, item.Code)
        '    ddlSubParameter.Items.Add(listItem)
        'Next

    End Sub

    Protected Sub ddlPeriode_SelectedIndexChanged(sender As Object, e As EventArgs)
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
End Class