#Region "DNet"
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

#Region "System"
Imports System.Text
Imports System.IO
Imports System.Linq
#End Region

Public Class FrmWSCParameterDetail
    Inherits System.Web.UI.Page

#Region "Private Variable"
    Private sessHelper As SessionHelper = New SessionHelper
    Private m_bFormPrivilege As Boolean = False
    Private m_bCreatePrivilege As Boolean = False
    Private m_bUpdatePrivilege As Boolean = False
    Private m_bActivatePrivilege As Boolean = False
    Private backURL As String = String.Empty
    Private wscHeaderID As Integer = 0
    Private Mode As String = String.Empty
#End Region

#Region "Check Privilege"
    Private Sub ActivateParameterPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.WSCParameterView_Privilege)
        m_bCreatePrivilege = SecurityProvider.Authorize(Context.User, SR.WSCParameterCreate_Privilege)
        m_bUpdatePrivilege = SecurityProvider.Authorize(Context.User, SR.WSCParameterUpdate_Privilege)
        m_bActivatePrivilege = SecurityProvider.Authorize(Context.User, SR.WSCParameterActivate_Privilege)

        If Not m_bFormPrivilege _
            AndAlso Not m_bCreatePrivilege _
            AndAlso Not m_bActivatePrivilege _
            AndAlso Not m_bUpdatePrivilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Waranty Service Claim - Input Parameter WSC")
        End If

        If m_bCreatePrivilege Then
            btnSimpan.Visible = True
            btnBatal.Visible = True
        Else
            btnSimpan.Visible = False
            btnBatal.Visible = False
        End If

        If m_bActivatePrivilege Then
            sessHelper.SetSession("ActivateParameter", True)
        Else
            sessHelper.SetSession("ActivateParameter", False)
        End If

        If m_bUpdatePrivilege Then
            sessHelper.SetSession("UpdateParameter", True)
        Else
            sessHelper.SetSession("UpdateParameter", False)
            btnSimpan.Visible = False
        End If
    End Sub

    Private Sub SetControlPrivilege()
        If Mode = "View" Then
            btnSimpan.Enabled = False
            dtgWscParam.Enabled = False
            dtgWscParam.ShowFooter = False

            dtgWscCondition.Enabled = False
            dtgWscCondition.ShowFooter = False
        ElseIf Mode = "Detail" Then
            btnSimpan.Enabled = True
            dtgWscParam.Enabled = True
            dtgWscParam.ShowFooter = True

            dtgWscCondition.Enabled = True
            dtgWscCondition.ShowFooter = True
        End If
        btnBatal.Visible = True
        If m_bCreatePrivilege OrElse m_bUpdatePrivilege Then
            btnSimpan.Visible = True
        ElseIf m_bFormPrivilege OrElse m_bActivatePrivilege Then
            btnSimpan.Visible = False
        End If
        If Not m_bUpdatePrivilege Then
            btnSimpan.Visible = False
        End If
    End Sub
#End Region

#Region "Custom Method"
    Private Sub BindParameter(ByVal ddlItem As DropDownList, Optional ByVal selected As Integer = -1)
        ddlItem.Items.Clear()
        Dim enumParameter As ArrayList = New EnumWSCParamParameter().RetrieveWSCParameterLI()
        For Each item As ListItem In enumParameter
            If Not IsNothing(ddlItem) Then
                ddlItem.Items.Add(New ListItem(item.Text, item.Value))
            End If
        Next
    End Sub

    Private Sub BindOperator(ByVal ddlItem As DropDownList)
        ddlItem.Items.Clear()
        Dim enumParameter As ArrayList = New EnumWSCParamParameter().RetrieveWSCOperatorLI()
        Dim dgSource As ArrayList = New ArrayList
        For Each item As ListItem In enumParameter
            If Not IsNothing(ddlItem) Then
                ddlItem.Items.Add(New ListItem(item.Text, item.Value))
            End If
        Next
    End Sub

    Private Sub BindParameterMaster(ByVal ddlItem As DropDownList, ByVal paramMstItemLn As Integer)
        ddlItem.Items.Clear()
        ddlItem.Items.Add(New ListItem("SILAHKAN PILIH", -1))
        For i As Integer = 1 To paramMstItemLn
            ddlItem.Items.Add(New ListItem(i, i))
        Next
    End Sub

    Private Sub BindConditionFunction(ByVal ddlItem As DropDownList)
        ddlItem.Items.Clear()
        ddlItem.Items.Add(New ListItem("SILAHKAN PILIH", -1))
        ddlItem.Items.Add(New ListItem("AND", "0"))
        ddlItem.Items.Add(New ListItem("OR", "1"))
    End Sub

    Private Sub BindKondisiDataInput(ByVal ddlItem As DropDownList)
        Dim arrCondition As ArrayList = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)
        ddlItem.Items.Add(New ListItem("SILAHKAN PILIH", -1))
        For i As Integer = 0 To arrCondition.Count - 1
            Dim item As WSCParameterCondition = arrCondition(i)
            ddlItem.Items.Add(New ListItem(i + 1, i))
        Next
    End Sub

    Private Sub BindDataGrid(ByVal p1 As Integer, ByVal index As Integer)
        Dim totalRow As Integer = 0

        Dim arlConditionGrid As ArrayList = New ArrayList
        Dim criteria1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterCondition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria1.opAnd(New Criteria(GetType(WSCParameterCondition), "WSCParameterHeader.ID", MatchType.Exact, p1))
        arlConditionGrid = New WSCParameterConditionFacade(User).RetrieveByCriteria(criteria1, index + 1, dtgWscCondition.PageSize, totalRow)
        If p1 = -1 AndAlso index = -1 Then
            arlConditionGrid = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)
        End If

        sessHelper.SetSession("ConditionGrid", CType(arlConditionGrid, ArrayList))
        dtgWscCondition.DataSource = arlConditionGrid
        dtgWscCondition.DataBind()

        Dim arlDGrid As ArrayList = New ArrayList
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(WSCParameterDetail), "WSCParameterHeader.ID", MatchType.Exact, p1))
        arlDGrid = New WSCParameterDetailFacade(User).RetrieveByCriteria(criteria, index + 1, dtgWscParam.PageSize, totalRow)
        If p1 = -1 AndAlso index = -1 Then
            arlDGrid = CType(sessHelper.GetSession("ParamGrid"), ArrayList)
        End If

        sessHelper.SetSession("ParamGrid", CType(arlDGrid, ArrayList))
        dtgWscParam.DataSource = arlDGrid
        dtgWscParam.DataBind()

    End Sub

    Private Sub Initialize()
        sessHelper.SetSession("ParamGrid", New ArrayList)
        If Mode <> "" AndAlso wscHeaderID <> 0 Then
            Dim arlWSCVehicle As ArrayList = New ArrayList
            Dim oWSCHeader As WSCParameterHeader = New WSCParameterHeaderFacade(User).Retrieve(wscHeaderID)
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.ID", MatchType.Exact, wscHeaderID))
            arlWSCVehicle = New WSCParameterVehicleFacade(User).Retrieve(criteria)

            lblClaimType.Text = oWSCHeader.ClaimType.ToString
            lblDescription.Text = oWSCHeader.Description.ToString
            lblStatus.Text = New EnumWSCParamStatus().RetrieveStatusMode(oWSCHeader.Status).DescStatus
            getCategoryFromType(wscHeaderID)

            For Each oWscVehicle As WSCParameterVehicle In arlWSCVehicle
                lboxVehicleType.Items.Add(oWscVehicle.VechileType.VechileTypeCode)
            Next
        End If
        BindDataGrid(wscHeaderID, 0)
        SetControlPrivilege()
        ViewState("currSortColumn") = "ID"
        ViewState("currSortDirection") = Sort.SortDirection.ASC



    End Sub

    Private Sub SessionInit()
        If Not IsNothing(Request.QueryString("Mode").ToString) Then
            Mode = Request.QueryString("Mode").ToString
        End If
        If Not IsNothing(sessHelper.GetSession("vsWSCParameterID")) Then
            wscHeaderID = sessHelper.GetSession("vsWSCParameterID")
        End If
    End Sub

    Private Sub getCategoryFromType(ByVal ParamHeadID As Integer)
        Dim arlParamVehicle As ArrayList = New ArrayList
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.ID", MatchType.Exact, ParamHeadID))
        arlParamVehicle = New WSCParameterVehicleFacade(User).Retrieve(criteria)
        For Each item As WSCParameterVehicle In arlParamVehicle
            'insertCategoryListBox(item.VechileType.Description.Split(" "))
            If Not IsNothing(item.VechileType) Then
                insertCategoryListBox2(item.VechileType.VechileModel.ID)
            End If
        Next
    End Sub

    Private Sub insertCategoryListBox2(ByVal modelID As Integer)
        Dim strValueID As String = String.Empty
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, modelID))
        Dim arlSubCategoryVehicleToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
        For Each item As SubCategoryVehicleToModel In arlSubCategoryVehicleToModel
            Dim iSubCategoryVehicleID As Integer = item.SubCategoryVehicle.ID
            If strValueID = String.Empty Then
                strValueID = iSubCategoryVehicleID
            Else
                strValueID += "," & iSubCategoryVehicleID
            End If
        Next
        If strValueID <> String.Empty Then
            BindVehicleSubCategoryToDDL(lboxCategory, strValueID)
        End If
    End Sub

    Public Sub BindVehicleSubCategoryToDDL(ByRef lboxCategory As ListBox, strValueID As String)
        lboxCategory.Items.Clear()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        '-- SubCategoryVehicle criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.No, "X"))  '-- Status still active
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "ID", MatchType.InSet, "(" + strValueID + ")"))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(SubCategoryVehicle), "Name", Sort.SortDirection.ASC))  '-- Sort by Vechile type code

        '-- Bind lboxCategory dropdownlist
        lboxCategory.DataSource = New SubCategoryVehicleFacade(User).Retrieve(criterias, sortColl)
        lboxCategory.DataTextField = "Name"
        lboxCategory.DataValueField = "ID"
        lboxCategory.DataBind()
        'lboxCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item
    End Sub

    Private Sub insertCategoryListBox(ByVal p1 As String())
        For Each item As String In p1
            If item.ToUpper = "TRITON".ToUpper _
                OrElse item.ToUpper = "LANCER".ToUpper _
                OrElse item.ToUpper = "GRANDIS".ToUpper _
                OrElse item.ToUpper = "MAVEN".ToUpper _
                OrElse item.ToUpper = "PAJERO".ToUpper _
                OrElse item.ToUpper = "OUTLANDER".ToUpper _
                OrElse item.ToUpper = "MIRAGE".ToUpper _
                OrElse item.ToUpper = "DELICA".ToUpper _
                OrElse item.ToUpper = "XPANDER".ToUpper _
                OrElse item.ToUpper = "EXPANDER".ToUpper _
                OrElse item.ToUpper = "T120SS".ToUpper _
                OrElse item.ToUpper = "L300".ToUpper Then
                Dim li As ListItem = New ListItem(item.ToUpper)
                If Not lboxCategory.Items.Contains(li) Then
                    lboxCategory.Items.Add(li)
                End If
            End If
        Next
    End Sub

    Private Sub insertSession(ByVal Prmtr As String, ByVal Oprt As String, ByVal Val As String, Optional ByVal ReasonCode As String = "", Optional ByVal KondDtInpt As Integer = -1)
        Dim arlWSCParamDetail As ArrayList = CType(sessHelper.GetSession("ParamGrid"), ArrayList)
        Dim oWSCParamDetail As WSCParameterDetail = New WSCParameterDetail
        oWSCParamDetail.Kind = Prmtr
        oWSCParamDetail.Operators = Oprt
        oWSCParamDetail.Value = Val
        oWSCParamDetail.ReasonCode = ReasonCode.Trim
        oWSCParamDetail.ConditionIndex = KondDtInpt
        arlWSCParamDetail.Add(oWSCParamDetail)
        sessHelper.SetSession("ParamGrid", arlWSCParamDetail)
    End Sub

    Private Sub insertConditionSession(ByVal Prmtr As String, ByVal Oprt As String, ByVal Val As String, ByVal Funct As String, ByVal ParamMst As String)
        'Add to grid for displaying data
        Dim arlWSCParamCondition As ArrayList = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)
        Dim oWSCParamCondition As WSCParameterCondition = New WSCParameterCondition
        oWSCParamCondition.Kind = Prmtr
        oWSCParamCondition.Operators = Oprt
        oWSCParamCondition.Value = Val
        oWSCParamCondition.Functions = Funct
        oWSCParamCondition.DataStatus = 1
        arlWSCParamCondition.Add(oWSCParamCondition)
        If ParamMst = "" Then
            oWSCParamCondition.WSCParameterConditionIndex = Nothing
            oWSCParamCondition.WSCParameterConditionID = Nothing
        Else
            oWSCParamCondition.WSCParameterConditionIndex = ParamMst
            oWSCParamCondition.WSCParameterConditionID = ParamMst
        End If

        sessHelper.SetSession("ConditionGrid", arlWSCParamCondition)

        'Add to temp session for insert
        Dim arlWSCParamConditionToInsert As ArrayList = CType(sessHelper.GetSession("NewCondition"), ArrayList)
        If IsNothing(arlWSCParamConditionToInsert) Then
            arlWSCParamConditionToInsert = New ArrayList
            arlWSCParamConditionToInsert.Add(oWSCParamCondition)
        Else
            arlWSCParamConditionToInsert.Add(oWSCParamCondition)
        End If

        sessHelper.SetSession("NewCondition", arlWSCParamConditionToInsert)
    End Sub

    Private Function simpanCondition() As Integer
        Dim nResult = -1
        Dim oWSCCondition As WSCParameterCondition
        Dim oWSCConditionF As New WSCParameterConditionFacade(User)
        Dim arrParamConditionToInsert As ArrayList = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)
        If IsNothing(arrParamConditionToInsert) OrElse arrParamConditionToInsert.Count < 1 Then
            nResult = 0
        Else
            Try
                For Each item As WSCParameterCondition In arrParamConditionToInsert
                    If item.DataStatus = 1 Then
                        oWSCCondition = New WSCParameterCondition
                        Dim oWSCHeader As New WSCParameterHeader
                        oWSCHeader.ID = wscHeaderID
                        oWSCCondition.WSCParameterHeader = oWSCHeader
                        oWSCCondition.Kind = item.Kind
                        oWSCCondition.Operators = item.Operators
                        oWSCCondition.Value = item.Value
                        oWSCCondition.WSCParameterConditionID = item.WSCParameterConditionID
                        item.ID = oWSCConditionF.Insert(oWSCCondition)
                        item.WSCParameterHeader = oWSCHeader
                    End If
                Next

                For Each item As WSCParameterCondition In arrParamConditionToInsert
                    If item.DataStatus > 0 Then
                        If item.WSCParameterConditionIndex > 0 Then
                            oWSCCondition = New WSCParameterCondition
                            oWSCCondition = item
                            Dim temp As WSCParameterCondition = arrParamConditionToInsert(item.WSCParameterConditionIndex - 1)
                            oWSCCondition.WSCParameterConditionID = temp.ID
                            nResult = oWSCConditionF.Update(oWSCCondition)
                        End If
                    End If
                Next

                nResult = 1
                sessHelper.SetSession("ConditionGrid", arrParamConditionToInsert)
            Catch ex As Exception
                nResult = -1
            End Try
        End If

        Return nResult

    End Function

    Private Function setWSCParameterConditionID() As Integer
        Dim nResult = -1
        Dim oWSCCondition As WSCParameterCondition
        Dim oWSCConditionF As New WSCParameterConditionFacade(User)
        Dim arrParamConditionToInsert As ArrayList = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)
        If IsNothing(arrParamConditionToInsert) OrElse arrParamConditionToInsert.Count < 1 Then
            nResult = 0
        Else
            Try
                For Each item As WSCParameterCondition In arrParamConditionToInsert
                    If item.DataStatus > 0 Then
                        oWSCCondition = New WSCParameterCondition
                        oWSCCondition = item
                        Dim temp As WSCParameterCondition = arrParamConditionToInsert(item.WSCParameterConditionID - 1)
                        oWSCCondition.WSCParameterConditionID = temp.ID
                        nResult = oWSCConditionF.Update(oWSCCondition)
                    End If
                Next

                sessHelper.SetSession("ConditionGrid", arrParamConditionToInsert)
            Catch ex As Exception
                nResult = -1
            End Try
        End If

        Return nResult
    End Function

    Private Sub deleteConditionSession(ByVal index As Integer)
        Dim itemToDel As WSCParameterCondition
        Dim arrCondition As ArrayList = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)
        Dim unsavedArrCondition As ArrayList = CType(sessHelper.GetSession("NewCondition"), ArrayList)
        Dim arrWSCParamDetail As ArrayList = CType(sessHelper.GetSession("ParamGrid"), ArrayList)
        itemToDel = arrCondition(index)

        'validate unsaved condition
        If itemToDel.DataStatus = 1 Then
            'validate that is is not refered by parameterCondition
            If Not IsNothing(unsavedArrCondition) Then
                If (From u As WSCParameterCondition In unsavedArrCondition Where u.WSCParameterConditionID = index + 1 Select u).Count > 0 Then
                    MessageBox.Show("Condition masih digunakan!")
                    Exit Sub
                End If
            End If

            'validate that it is not refered by parameterDetail
            If (From d1 As WSCParameterDetail In arrWSCParamDetail Where d1.ConditionIndex = index + 1 Select d1).Count > 0 Then
                MessageBox.Show("Condition masih digunakan!")
                Exit Sub
            End If

            'delete from unsaved Array Condition
            unsavedArrCondition.Remove(itemToDel)
        Else
            'validate saved condition
            If (From c As WSCParameterCondition In arrCondition Where c.WSCParameterConditionID = itemToDel.ID Select c).Count > 0 Then
                MessageBox.Show("Condition masih digunakan!")
                Exit Sub
            End If

            'validate that it is not refered by parameterDetail
            If (From d1 As WSCParameterDetail In (From d As WSCParameterDetail In arrWSCParamDetail Where Not IsNothing(d.WSCParameterCondition) Select d)
               Where d1.WSCParameterCondition.ID = itemToDel.ID Select d1).Count > 0 Then

                MessageBox.Show("Condition masih digunakan!")
                Exit Sub
            End If

            'validate that is not refered by new parameterDetail
            If (From d1 As WSCParameterDetail In arrWSCParamDetail Where d1.ConditionIndex = index Select d1).Count > 0 Then

                MessageBox.Show("Condition masih digunakan!")
                Exit Sub
            End If
        End If

        'Remove from grid
        arrCondition.RemoveAt(index)

        'Add deleteed record to session
        Dim arrToDel As ArrayList = CType(sessHelper.GetSession("DeletedCondition"), ArrayList)
        If IsNothing(arrToDel) Then
            arrToDel = New ArrayList
            arrToDel.Add(itemToDel)
        Else
            arrToDel.Add(itemToDel)
        End If

        sessHelper.SetSession("DeletedCondition", arrToDel)

        'Update GridView
        sessHelper.SetSession("ConditionGrid", arrCondition)
    End Sub

    Private Function getConditionIndexForParam(ByVal cond As WSCParameterCondition) As Integer
        Dim arrWscCondition As ArrayList = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)
        If cond.ID = 0 Then
            Return cond.WSCParameterConditionID
        Else
            Dim index As Integer = 1
            For Each item As WSCParameterCondition In arrWscCondition
                If item.ID = cond.ID Then
                    Exit For
                End If

                index += 1
            Next

            Return index
        End If
    End Function

    Private Function getConditionIndex(ByVal cond As WSCParameterCondition) As Integer
        Dim arrWSCCondition As ArrayList = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)
        If cond.ID = 0 Then
            Return cond.WSCParameterConditionID

        ElseIf (From c As WSCParameterCondition In arrWSCCondition Where c.ID = cond.WSCParameterConditionID Select c).Count = 0 Then
            Return cond.WSCParameterConditionID

        Else
            Dim index As Integer = 1
            For Each item As WSCParameterCondition In arrWSCCondition
                If item.ID = cond.WSCParameterConditionID Then
                    Exit For
                End If

                index += 1
            Next

            Return index
        End If
    End Function

    Private Sub bindConditionAndValidation()
        Dim arrCondition As ArrayList = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)
        Dim arrParamDetail As ArrayList = CType(sessHelper.GetSession("ParamGrid"), ArrayList)
        Dim temp As WSCParameterDetail

        If Not IsNothing(arrParamDetail) Then
            For Each paramDetail As WSCParameterDetail In arrParamDetail
                If paramDetail.ConditionIndex > -1 Then
                    Dim itemCondition As WSCParameterCondition = arrCondition(paramDetail.ConditionIndex)
                    paramDetail.WSCParameterCondition = itemCondition
                ElseIf paramDetail.ConditionIndex = -2 Then
                    paramDetail.WSCParameterCondition = Nothing
                End If
            Next
        End If

        sessHelper.SetSession("ParamGrid", arrParamDetail)
    End Sub

    Private Sub saveUpdateCondtion(ByVal cond As WSCParameterCondition)
        Dim arrCondToUpdate As ArrayList = CType(sessHelper.GetSession("ConditionUpdateList"), ArrayList)
        cond.DataStatus = 2
        If IsNothing(arrCondToUpdate) Then
            arrCondToUpdate = New ArrayList
            arrCondToUpdate.Add(cond)
        Else
            arrCondToUpdate.Add(cond)
        End If

        sessHelper.SetSession("ConditionUpdateList", arrCondToUpdate)

        Dim arrCondition As ArrayList = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)
        For Each item As WSCParameterCondition In arrCondition
            If item.ID = cond.ID Then
                item = cond
            End If
        Next

        sessHelper.SetSession("ConditionGrid", arrCondition)
    End Sub

    Private Sub saveUpdateParam(ByVal param As WSCParameterDetail)
        Dim arrParamToUpdate As ArrayList = CType(sessHelper.GetSession("ParamUpdateList"), ArrayList)
        If IsNothing(arrParamToUpdate) Then
            arrParamToUpdate = New ArrayList
            arrParamToUpdate.Add(param)
        Else
            arrParamToUpdate.Add(param)
        End If

        sessHelper.SetSession("ParamUpdateList", arrParamToUpdate)

        Dim arrParam As ArrayList = CType(sessHelper.GetSession("ParamGrid"), ArrayList)
        For Each item As WSCParameterDetail In arrParam
            If item.ID = param.ID Then
                item = param
            End If
        Next

        sessHelper.SetSession("ParamGrid", arrParam)
    End Sub

    Private Sub setInputField(ByVal e As Object, ByVal param As Integer, ByVal value As String, ByVal grid As String, ByVal mode As String)
        If mode = "edit" Then
            Dim txtValueEdit As TextBox
            Dim txtValueEditNum As TextBox
            Dim lblSearchChassisEdit As Label
            Dim calendar As KTB.DNet.WebCC.IntiCalendar
            If grid = "condition" Then
                txtValueEdit = CType(e.Item.FindControl("txtValueEditC"), TextBox)
                txtValueEditNum = CType(e.Item.FindControl("txtValueEditNumC"), TextBox)
                lblSearchChassisEdit = CType(e.Item.FindControl("lblSearchChassisEditC"), Label)
                calendar = CType(e.Item.FindControl("icValueEditC"), KTB.DNet.WebCC.IntiCalendar)
            ElseIf grid = "param" Then
                txtValueEdit = CType(e.Item.FindControl("txtValueEditP"), TextBox)
                txtValueEditNum = CType(e.Item.FindControl("txtValueEditNumP"), TextBox)
                lblSearchChassisEdit = CType(e.Item.FindControl("lblSearchChassisEditP"), Label)
                calendar = CType(e.Item.FindControl("icValueEditP"), KTB.DNet.WebCC.IntiCalendar)
            End If

            txtValueEdit.Visible = False
            txtValueEditNum.Visible = False
            lblSearchChassisEdit.Visible = False
            calendar.Visible = False
            If param = 0 Or param = 5 Or param = 9 Or param = 10 Or param = 11 Or param = 12 Or param = 13 Or param = 15 Or param = 19 Then
                txtValueEdit.Visible = True
                lblSearchChassisEdit.Visible = True
                txtValueEdit.Text = value
            ElseIf param = 1 Or param = 2 Or param = 3 Or param = 18 Then
                calendar.Visible = True
                If value <> "" Then
                    calendar.Value = value
                End If
            ElseIf param = 4 Or param = 6 Or param = 7 Or param = 8 Or param = 16 Or param = 17 Or param = 20 Then
                txtValueEditNum.Visible = True
                txtValueEditNum.Text = value
            End If
        ElseIf mode = "add" Then
            Dim txtValue As TextBox
            Dim txtValueNum As TextBox
            Dim lblSearchChassis As Label
            Dim calendar As KTB.DNet.WebCC.IntiCalendar
            If grid = "condition" Then
                txtValue = CType(e.Item.FindControl("txtValueFooter0"), TextBox)
                txtValueNum = CType(e.Item.FindControl("txtValueFooterNum0"), TextBox)
                lblSearchChassis = CType(e.Item.FindControl("lblSearchChassisFooter0"), Label)
                calendar = CType(e.Item.FindControl("icValueFooter0"), KTB.DNet.WebCC.IntiCalendar)
            ElseIf grid = "param" Then
                txtValue = CType(e.Item.FindControl("txtValueFooter"), TextBox)
                txtValueNum = CType(e.Item.FindControl("txtValueFooterNum"), TextBox)
                lblSearchChassis = CType(e.Item.FindControl("lblSearchChassisFooter"), Label)
                calendar = CType(e.Item.FindControl("icValueFooter"), KTB.DNet.WebCC.IntiCalendar)
            End If

            txtValue.Visible = False
            txtValueNum.Visible = False
            lblSearchChassis.Visible = False
            calendar.Visible = False
            If param = 0 Or param = 5 Or param = 9 Or param = 10 Or param = 11 Or param = 12 Or param = 13 Or param = 15 Or param = 19 Then
                txtValue.Visible = True
                lblSearchChassis.Visible = True
                txtValue.Text = value
            ElseIf param = 1 Or param = 2 Or param = 3 Or param = 18 Then
                calendar.Visible = True
                If value <> "" Then
                    calendar.Value = value
                End If
            ElseIf param = 4 Or param = 6 Or param = 7 Or param = 8 Or param = 16 Or param = 17 Or param = 20 Then
                txtValueNum.Visible = True
                txtValueNum.Text = value
            End If
        End If
    End Sub

    Private Sub showFooterInput(ByVal dg As DataGrid, ByVal param As Integer, ByVal value As String, ByVal grid As String)
        Dim txtValue As TextBox
        Dim txtValueNum As TextBox
        Dim lblSearchChassis As Label
        Dim calendar As KTB.DNet.WebCC.IntiCalendar
        If grid = "condition" Then
            For Each item As DataGridItem In dg.Items
                If item.ItemType = ListItemType.Footer Then
                    txtValue = CType(item.FindControl("txtValueFooter0"), TextBox)
                    txtValueNum = CType(item.FindControl("txtValueFooterNum0"), TextBox)
                    lblSearchChassis = CType(item.FindControl("lblSearchChassisFooter0"), Label)
                    calendar = CType(item.FindControl("icValueFooter0"), KTB.DNet.WebCC.IntiCalendar)
                End If
            Next
        ElseIf grid = "param" Then
        End If

        txtValue.Visible = False
        txtValueNum.Visible = False
        lblSearchChassis.Visible = False
        calendar.Visible = False
        If param = 0 Or param = 5 Or param = 9 Or param = 10 Or param = 11 Or param = 12 Or param = 13 Or param = 15 Or param = 19 Then
            txtValue.Visible = True
            lblSearchChassis.Visible = True
            txtValue.Text = value
        ElseIf param = 1 Or param = 2 Or param = 3 Or param = 18 Then
            calendar.Visible = True
            If value <> "" Then
                calendar.Value = value
            End If
        ElseIf param = 4 Or param = 6 Or param = 7 Or param = 8 Or param = 16 Or param = 17 Or param = 20 Then
            txtValueNum.Visible = True
            txtValueNum.Text = value
        End If
    End Sub

    Private Function getConditionValue(ByVal index As Integer) As Integer
        Dim arrCond As ArrayList = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)
        Dim cond As WSCParameterCondition = arrCond(index)

        Return cond.Kind
    End Function

    Private Function validateInputValue(ByVal value As String) As Boolean
        If IsNothing(value) OrElse value.IsNullorEmpty() OrElse String.IsNullOrWhiteSpace(value) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function validateParamAndMasterParam(ByVal domainParam As String, ByVal referedParam As String) As Boolean
        'If getConditionValue(referedParam) = 5 Then
        '    If domainParam <> 9 Then
        '        MessageBox.Show("Kondisi data input tidak valid! \nKode posisi hanya bisa dirujuk oleh Kode kerja.")
        '        Return False
        '    End If
        'ElseIf getConditionValue(referedParam) = 16 Then
        '    If domainParam <> 5 OrElse domainParam <> 9 OrElse domainParam <> 15 Then
        '        MessageBox.Show("Kondisi data input tidak valid! \nAmount hanya bisa dirujuk oleh Kode posisi, Kode kerja, dan Part.")
        '        Exit Function
        '    End If
        'ElseIf domainParam = 17 Then
        '    If getConditionValue(referedParam) = 15 Then
        '        MessageBox.Show("Kondisi data input tidak valid? \nJumlah hanya dapat merujuk pada Part.")
        '    End If
        'End If
        Return True
    End Function

    Private Function valueValidation(ByVal value As String, ByVal kind As Integer) As Boolean
        Dim arrVal As String() = value.Split(";")
        If kind = 5 Then
            For Each val As String In arrVal
                Dim criteriaas As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaas.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "KodePosition", MatchType.Exact, val))
                Dim posCodeFacade As DeskripsiPositionCodeFacade = New DeskripsiPositionCodeFacade(User)
                Dim dataMaster As ArrayList = posCodeFacade.Retrieve(criteriaas)
                If IsNothing(dataMaster) OrElse dataMaster.Count = 0 Then
                    MessageBox.Show("Kode Posisi " & val & " tidak terdaftar.")
                    sessHelper.SetSession("falseParamValue", kind)
                    Return False
                End If
            Next
        ElseIf kind = 9 Then
            For Each val As String In arrVal
                Dim criteriaas As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaas.opAnd(New Criteria(GetType(DeskripsiKodeKerja), "KodeKerja", MatchType.Exact, val))
                Dim wrkCodeFacade As DeskripsiWorkPositionFacade = New DeskripsiWorkPositionFacade(User)
                Dim dataMaster As ArrayList = wrkCodeFacade.Retrieve(criteriaas)
                If IsNothing(dataMaster) OrElse dataMaster.Count = 0 Then
                    MessageBox.Show("Kode Kerja " & val & " tidak terdaftar.")
                    sessHelper.SetSession("falseParamValue", kind)
                    Return False
                End If
            Next
        ElseIf kind = 15 Then
            For Each val As String In arrVal
                Dim criteriaas As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaas.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, val))
                Dim spPartFacade As SparePartMasterFacade = New SparePartMasterFacade(User)
                Dim dataMaster As ArrayList = spPartFacade.Retrieve(criteriaas)
                If IsNothing(dataMaster) OrElse dataMaster.Count = 0 Then
                    sessHelper.SetSession("falseParamValue", kind)
                    MessageBox.Show("Part " & val & " tidak terdaftar.")
                    Return False
                End If
            Next
        End If

        sessHelper.RemoveSession("falseParamValue")
        Return True
    End Function
#End Region

#Region "Event Handler"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ActivateParameterPrivilege()
        SessionInit()
        If Not IsPostBack Then
            Initialize()
        End If

        'Dim txtValue As TextBox
        'Dim txtValueNum As TextBox
        'Dim lblSearchChassis As Label
        'Dim calendar As KTB.DNet.WebCC.IntiCalendar
        'For Each item As DataGridItem In dtgWscCondition.Items
        '    If item.ItemType = ListItemType.Footer Then
        '        txtValue = CType(item.FindControl("txtValueFooter0"), TextBox)
        '        txtValueNum = CType(item.FindControl("txtValueFooterNum0"), TextBox)
        '        lblSearchChassis = CType(item.FindControl("lblSearchChassisFooter0"), Label)
        '        calendar = CType(item.FindControl("icValueFooter0"), KTB.DNet.WebCC.IntiCalendar)
        '    End If
        'Next
        'txtValue.Visible = False
        'txtValueNum.Visible = False
        'lblSearchChassis.Visible = False
        'calendar.Visible = False
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Response.Redirect(sessHelper.GetSession("backURL").ToString)
    End Sub

    Protected Sub dtgWscParam_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgWscParam.ItemDataBound
        Dim ddlParameter As Label
        Dim ddlOperator As Label
        Dim lblValue As Label
        Dim lblReasonCode As Label
        Dim lblKondisiDataInput As Label
        Dim ddlParameterF As DropDownList
        Dim ddlOperatorF As DropDownList
        Dim ddlKondisiDataInput As DropDownList
        Dim txtValueF As TextBox
        Dim txtReasonCodeF As TextBox
        Dim index = e.Item.ItemIndex
        Dim oWSCParamDetail As WSCParameterDetail = New WSCParameterDetail
        Dim arlWSCParamDetail As ArrayList = CType(sessHelper.GetSession("ParamGrid"), ArrayList)
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If ItemType = ListItemType.Item OrElse ItemType = ListItemType.AlternatingItem Then
            ddlParameter = CType(e.Item.FindControl("lblParameter"), Label)
            ddlOperator = CType(e.Item.FindControl("lblOperator"), Label)
            lblValue = CType(e.Item.FindControl("lblValue"), Label)
            lblReasonCode = CType(e.Item.FindControl("lblReasonCode"), Label)
            lblKondisiDataInput = CType(e.Item.FindControl("lblKondisiDataInput"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            oWSCParamDetail = arlWSCParamDetail(index)
            ddlParameter.Text = New EnumWSCParamParameter().RetrieveWSCParameter(oWSCParamDetail.Kind)
            ddlOperator.Text = New EnumWSCParamParameter().RetrieveWSCParamOperator(oWSCParamDetail.Operators)
            lblValue.Text = oWSCParamDetail.Value
            lblReasonCode.Text = oWSCParamDetail.ReasonCode
            If oWSCParamDetail.ConditionIndex = -2 Then
                lblKondisiDataInput.Text = ""
            ElseIf oWSCParamDetail.ConditionIndex > -1 Then
                lblKondisiDataInput.Text = oWSCParamDetail.ConditionIndex + 1
            ElseIf Not IsNothing(oWSCParamDetail.WSCParameterCondition) Then
                lblKondisiDataInput.Text = getConditionIndexForParam(oWSCParamDetail.WSCParameterCondition)
            End If
            lblNo.Text = index + 1

        ElseIf ItemType = ListItemType.Footer Then
            ddlParameterF = CType(e.Item.FindControl("ddlParameterFooter"), DropDownList)
            ddlOperatorF = CType(e.Item.FindControl("ddlOperatorFooter"), DropDownList)
            ddlKondisiDataInput = CType(e.Item.FindControl("ddlKondisiDataInput"), DropDownList)
            txtValueF = CType(e.Item.FindControl("txtValueFooter"), TextBox)
            txtReasonCodeF = CType(e.Item.FindControl("txtReasonCodeFooter"), TextBox)
            BindParameter(ddlParameterF)
            BindOperator(ddlOperatorF)
            BindKondisiDataInput(ddlKondisiDataInput)
            ddlParameterF.ClearSelection()
            ddlOperatorF.ClearSelection()
            txtValueF.Text = ""
            txtReasonCodeF.Text = ""

        ElseIf ItemType = ListItemType.EditItem Then
            ddlParameterF = CType(e.Item.FindControl("ddlParameterEditP"), DropDownList)
            ddlOperatorF = CType(e.Item.FindControl("ddlOperatorEditP"), DropDownList)
            ddlKondisiDataInput = CType(e.Item.FindControl("ddlKondisiDataInputEditP"), DropDownList)
            txtReasonCodeF = CType(e.Item.FindControl("txtReasonCodeEditP"), TextBox)
            Dim paramToEdit As WSCParameterDetail = CType(sessHelper.GetSession("ParamEdit"), WSCParameterDetail)

            BindParameter(ddlParameterF)
            BindOperator(ddlOperatorF)
            BindKondisiDataInput(ddlKondisiDataInput)

            ddlParameterF.ClearSelection()
            ddlParameterF.SelectedValue = paramToEdit.Kind
            ddlParameterF.Enabled = False
            ddlOperatorF.SelectedValue = paramToEdit.Operators
            setInputField(e, paramToEdit.Kind, paramToEdit.Value, "param", "edit")
            ddlKondisiDataInput.SelectedValue = paramToEdit.ConditionIndex
            txtReasonCodeF.Text = paramToEdit.ReasonCode
        End If
    End Sub

    Protected Sub dtgWscParam_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgWscParam.ItemCommand
        Dim _arParamGrid As ArrayList = CType(sessHelper.GetSession("ParamGrid"), ArrayList)

        Dim ddlParameterF As DropDownList
        Dim ddlOperatorF As DropDownList
        Dim ddlKondisiDataInputF As DropDownList
        Dim txtValueF As TextBox
        Dim txtReasonCodeF As TextBox
        Dim strValue As String
        Dim strKondisiDataInput As Integer

        Select Case e.CommandName

            Case "Add" 'Insert New item to datagrid

                ddlOperatorF = CType(e.Item.FindControl("ddlOperatorFooter"), DropDownList)
                ddlParameterF = CType(e.Item.FindControl("ddlParameterFooter"), DropDownList)
                ddlKondisiDataInputF = CType(e.Item.FindControl("ddlKondisiDataInput"), DropDownList)
                txtReasonCodeF = CType(e.Item.FindControl("txtReasonCodeFooter"), TextBox)

                If ddlParameterF.SelectedValue = 4 OrElse ddlParameterF.SelectedValue = 6 OrElse ddlParameterF.SelectedValue = 7 OrElse ddlParameterF.SelectedValue = 8 OrElse
                   ddlParameterF.SelectedValue = 16 OrElse ddlParameterF.SelectedValue = 17 OrElse ddlParameterF.SelectedValue = 20 Then
                    txtValueF = CType(e.Item.FindControl("txtValueFooterNum"), TextBox)
                    strValue = txtValueF.Text
                ElseIf ddlParameterF.SelectedValue = 1 OrElse ddlParameterF.SelectedValue = 2 OrElse ddlParameterF.SelectedValue = 3 OrElse ddlParameterF.SelectedValue = 18 Then
                    strValue = CType(e.Item.FindControl("icValueFooter"), KTB.DNet.WebCC.IntiCalendar).Value
                Else
                    txtValueF = CType(e.Item.FindControl("txtValueFooter"), TextBox)
                    strValue = txtValueF.Text
                End If

                If ddlKondisiDataInputF.SelectedValue > 0 Then
                    If Not validateParamAndMasterParam(ddlParameterF.SelectedValue, ddlKondisiDataInputF.SelectedValue) Then
                        Exit Sub
                    End If
                    strKondisiDataInput = ddlKondisiDataInputF.SelectedValue
                Else
                    strKondisiDataInput = -1
                End If

                'add Value Validation
                If Not valueValidation(strValue, ddlParameterF.SelectedValue) Then
                    setInputField(e, ddlParameterF.SelectedValue, strValue, "param", "add")
                    Exit Sub
                End If

                'Add Part Validation
                If ddlParameterF.SelectedValue = 15 Then

                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aDatas As ArrayList

                    ' Terdiri dari OR Tidak Terdiri dari
                    If (ddlOperatorF.SelectedValue = 9 Or ddlOperatorF.SelectedValue = 10) Then

                        If (strValue.Trim() <> String.Empty) Then
                            criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.InSet, "('" & strValue.Trim().Replace(";", "','") & "')"))
                            criterias.opAnd(New Criteria(GetType(SparePartMaster), "ProductCategory.Code", MatchType.InSet, "('MMC','ALL')"))
                        End If

                        aDatas = New SparePartMasterFacade(User).Retrieve(CType(criterias, CriteriaComposite))

                        If aDatas.Count = 0 Then
                            MessageBox.Show("Kode Part tidak terdaftar.")
                            Exit Sub
                        End If

                    End If

                End If

                If ddlOperatorF.SelectedIndex > 0 AndAlso ddlParameterF.SelectedIndex > 0 AndAlso strValue.Trim.Length > 0 Then
                    If ddlParameterF.SelectedIndex = 6 Then
                        If lblClaimType.Text.Trim.ToUpper = "Z2" Then
                            If Not Char.IsLetter(txtValueF.Text) OrElse txtValueF.Text = "XEE999" Then
                                insertSession(ddlParameterF.SelectedValue, ddlOperatorF.SelectedValue, strValue, txtReasonCodeF.Text, strKondisiDataInput)
                            Else
                                MessageBox.Show("Untuk Type Claim Z2, \n Kode Posisi hanya boleh yang berawalan Angka")
                            End If
                        ElseIf lblClaimType.Text.Trim.ToUpper = "Z4" Then
                            If Char.IsLetter(txtValueF.Text) Then
                                insertSession(ddlParameterF.SelectedValue, ddlOperatorF.SelectedValue, strValue, txtReasonCodeF.Text, strKondisiDataInput)
                            Else
                                MessageBox.Show("Untuk Type Claim Z4, \n Kode Posisi hanya boleh yang berawalan Huruf")
                            End If
                        Else
                            insertSession(ddlParameterF.SelectedValue, ddlOperatorF.SelectedValue, strValue, txtReasonCodeF.Text, strKondisiDataInput)
                        End If
                    Else
                        insertSession(ddlParameterF.SelectedValue, ddlOperatorF.SelectedValue, strValue, txtReasonCodeF.Text, strKondisiDataInput)
                    End If
                ElseIf ddlOperatorF.SelectedIndex = 0 AndAlso ddlParameterF.SelectedIndex = 0 AndAlso strValue.Trim.Length = 0 Then
                    MessageBox.Show("Data masih kosong")
                ElseIf ddlOperatorF.SelectedIndex = 0 Then
                    MessageBox.Show("Silahkan pilih Operator terlebih dahulu")
                ElseIf ddlParameterF.SelectedIndex = 0 Then
                    MessageBox.Show("Silahkan pilih Parameter terlebih dahulu")
                ElseIf strValue.Trim.Length = 0 Then
                    MessageBox.Show("Silahkan isi value terlebih dahulu")
                End If


            Case "Delete" 'Delete this datagrid item
                sessHelper.RemoveSession("ParamGrid")
                sessHelper.SetSession("ParamGrid", New ArrayList)
                For index As Integer = 0 To _arParamGrid.Count - 1
                    Select Case e.Item.ItemType
                        Case ListItemType.Item, ListItemType.AlternatingItem
                            Dim ddlParameter As Label = CType(e.Item.FindControl("lblParameter"), Label)
                            Dim ddlOperator As Label = CType(e.Item.FindControl("lblOperator"), Label)
                            Dim lblValue As Label = CType(e.Item.FindControl("lblValue"), Label)
                            Dim lblReasonCode As Label = CType(e.Item.FindControl("lblReasonCode"), Label)
                            Dim parameter As Integer
                            Dim op As Integer
                            For Each item As ListItem In New EnumWSCParamParameter().RetrieveWSCParameterLI
                                If item.Text.ToUpper = ddlParameter.Text.ToUpper Then
                                    parameter = CType(item.Value, Integer)
                                End If
                            Next
                            For Each item As ListItem In New EnumWSCParamParameter().RetrieveWSCOperatorLI
                                If item.Text.ToUpper = ddlOperator.Text.ToUpper Then
                                    op = CType(item.Value, Integer)
                                End If
                            Next
                            insertSession(parameter, op, lblValue.Text, lblReasonCode.Text)
                        Case ListItemType.Footer
                            ddlOperatorF = CType(e.Item.FindControl("ddlOperatorFooter"), DropDownList)
                            ddlParameterF = CType(e.Item.FindControl("ddlParameterFooter"), DropDownList)
                            txtReasonCodeF = CType(e.Item.FindControl("txtReasonCodeFooter"), TextBox)
                            '(selectedParameterValue == "4" || selectedParameterValue == "6" || selectedParameterValue == "7" || selectedParameterValue == "8") 
                            If ddlParameterF.SelectedValue = 4 OrElse ddlParameterF.SelectedValue = 6 OrElse ddlParameterF.SelectedValue = 7 OrElse ddlParameterF.SelectedValue = 8 OrElse ddlParameterF.SelectedValue = 20 Then
                                txtValueF = CType(e.Item.FindControl("txtValueFooterNum"), TextBox)
                            Else
                                txtValueF = CType(e.Item.FindControl("txtValueFooter"), TextBox)
                            End If
                            insertSession(ddlParameterF.SelectedValue, ddlOperatorF.SelectedValue, txtValueF.Text, txtReasonCodeF.Text)
                    End Select
                Next
                _arParamGrid.RemoveAt(e.Item.ItemIndex)
                sessHelper.SetSession("ParamGrid", CType(_arParamGrid, ArrayList))

            Case "Edit"
                Dim paramGrid As DataGrid = Me.Page.FindControl("dtgWscParam")
                paramGrid.ShowFooter = False
                paramGrid.EditItemIndex = CInt(e.Item.ItemIndex)
                Dim paramToEdit As WSCParameterDetail = CType(sessHelper.GetSession("ParamGrid"), ArrayList)(e.Item.ItemIndex)
                sessHelper.SetSession("ParamEdit", paramToEdit)
                sessHelper.SetSession("ParamEditRow", e)

            Case "Update"
                Dim paramToEdit As WSCParameterDetail = CType(sessHelper.GetSession("ParamEdit"), WSCParameterDetail)
                ddlParameterF = CType(e.Item.FindControl("ddlParameterEditP"), DropDownList)
                ddlOperatorF = CType(e.Item.FindControl("ddlOperatorEditP"), DropDownList)
                ddlKondisiDataInputF = CType(e.Item.FindControl("ddlKondisiDataInputEditP"), DropDownList)
                txtReasonCodeF = CType(e.Item.FindControl("txtReasonCodeEditP"), TextBox)

                If ddlKondisiDataInputF.SelectedValue > -1 Then
                    If Not validateParamAndMasterParam(ddlParameterF.SelectedValue, ddlKondisiDataInputF.SelectedValue) Then
                        Exit Sub
                    End If
                    paramToEdit.ConditionIndex = ddlKondisiDataInputF.SelectedValue
                    paramToEdit.WSCParameterCondition = Nothing
                Else
                    paramToEdit.ConditionIndex = -2
                    paramToEdit.WSCParameterCondition = Nothing
                End If

                If paramToEdit.Kind = 0 Or paramToEdit.Kind = 5 Or paramToEdit.Kind = 9 Or paramToEdit.Kind = 10 Or paramToEdit.Kind = 11 Or
                    paramToEdit.Kind = 12 Or paramToEdit.Kind = 13 Or paramToEdit.Kind = 15 Then
                    Dim val As TextBox = CType(e.Item.FindControl("txtValueEditP"), TextBox)
                    If validateInputValue(val.Text) Then
                        paramToEdit.Value = val.Text
                    Else
                        MessageBox.Show("Silahkan isi value terlebih dahulu")
                        Exit Sub
                    End If
                ElseIf paramToEdit.Kind = 1 Or paramToEdit.Kind = 2 Or paramToEdit.Kind = 3 Then
                    Dim calendar As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icValueEditP"), KTB.DNet.WebCC.IntiCalendar)
                    If validateInputValue(calendar.Value) Then
                        paramToEdit.Value = calendar.Value
                    Else
                        MessageBox.Show("Silahkan isi value terlebih dahulu")
                        Exit Sub
                    End If
                ElseIf paramToEdit.Kind = 4 Or paramToEdit.Kind = 6 Or paramToEdit.Kind = 7 Or paramToEdit.Kind = 8 Or paramToEdit.Kind = 16 Or paramToEdit.Kind = 17 Or paramToEdit.Kind = 20 Then
                    Dim txtValueEditNum As TextBox = CType(e.Item.FindControl("txtValueEditNumP"), TextBox)
                    If validateInputValue(txtValueEditNum.Text) Then
                        paramToEdit.Value = txtValueEditNum.Text
                    Else
                        MessageBox.Show("Silahkan isi value terlebih dahulu")
                        Exit Sub
                    End If
                End If

                paramToEdit.Operators = ddlOperatorF.SelectedValue
                paramToEdit.ReasonCode = txtReasonCodeF.Text

                'add Value Validation
                If Not valueValidation(paramToEdit.Value, paramToEdit.Kind) Then
                    Exit Sub
                End If

                saveUpdateParam(paramToEdit)
                Dim paramGrid As DataGrid = Me.Page.FindControl("dtgWscParam")
                paramGrid.ShowFooter = True
                paramGrid.EditItemIndex = -1

                BindDataGrid(-1, -1)

            Case "Cancel"
                sessHelper.RemoveSession("ParamEdit")
                Dim condGrid As DataGrid = Me.Page.FindControl("dtgWscParam")
                condGrid.ShowFooter = True
                condGrid.EditItemIndex = -1
                BindDataGrid(-1, -1)
        End Select

endParamItemCommand:
        BindDataGrid(-1, -1)
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim nResult = -1
        Try
            'Do delete
            Dim oWSCParamCondF As New WSCParameterConditionFacade(User)
            Dim arrToDel As ArrayList = CType(sessHelper.GetSession("DeletedCondition"), ArrayList)
            If Not IsNothing(arrToDel) Then
                For Each item As WSCParameterCondition In arrToDel
                    oWSCParamCondF.Delete(item)
                Next
                nResult = 1
            End If

            'Do update condition
            Dim arrCondUpdate As ArrayList = CType(sessHelper.GetSession("ConditionUpdateList"), ArrayList)
            If Not IsNothing(arrCondUpdate) Then
                For Each item As WSCParameterCondition In arrCondUpdate
                    nResult = oWSCParamCondF.Update(item)
                Next
            End If

            'Do save Conditions
            nResult = simpanCondition()

            'Do set WSCParameterConditionID
            'nResult = setWSCParameterConditionID()

            'Bind validation and condition
            bindConditionAndValidation()

        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
            Exit Sub
        End Try



        'Do save parameter validation
        Dim oWSCDetail As WSCParameterDetail
        Dim oWSCDetailF As New WSCParameterDetailFacade(User)
        Dim arrParamGrid As ArrayList = CType(sessHelper.GetSession("ParamGrid"), ArrayList)

        If arrParamGrid.Count > 0 Then
            reFlush(wscHeaderID)
            For Each item As WSCParameterDetail In arrParamGrid
                oWSCDetail = New WSCParameterDetail
                Dim oWSCHeader As New WSCParameterHeader
                oWSCHeader.ID = wscHeaderID
                oWSCDetail.WSCParameterHeader = oWSCHeader
                oWSCDetail.Kind = item.Kind
                oWSCDetail.Operators = item.Operators
                oWSCDetail.Value = item.Value
                oWSCDetail.ReasonCode = item.ReasonCode
                oWSCDetail.WSCParameterCondition = item.WSCParameterCondition
                nResult = oWSCDetailF.Insert(oWSCDetail)
            Next

        Else
            Try
                Dim deCount As Integer = oWSCDetailF.ScalarDetail(wscHeaderID)
                If deCount > 0 Then
                    reFlush(wscHeaderID)
                End If
            Catch
            End Try
        End If

        If nResult > -1 Then
            MessageBox.Show(SR.SaveSuccess)
            Server.Transfer("FrmWSCParameterHeader.aspx")
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Sub reFlush(ByVal HeaderID As String)
        Dim crit As New CriteriaComposite(New Criteria(GetType(WSCParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(WSCParameterDetail), "WSCParameterHeader.ID", MatchType.Exact, HeaderID))
        Dim arrWscPaDe As ArrayList = New WSCParameterDetailFacade(User).Retrieve(crit)
        For Each item As WSCParameterDetail In arrWscPaDe
            Dim oWscParamHeader As WSCParameterHeader = New WSCParameterHeaderFacade(User).Retrieve(CType(HeaderID, Integer))
            item.WSCParameterHeader = oWscParamHeader
            Dim del As New WSCParameterDetailFacade(User)
            del.Delete(item)
        Next
    End Sub

#End Region

    Protected Sub dtgWscCondition_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgWscCondition.ItemDataBound
        Dim ddlParameter As Label
        Dim ddlOperator As Label
        Dim lblValue As Label
        Dim lblParameterMaster As Label
        Dim lblFunction As Label
        Dim ddlParameterF As DropDownList
        Dim ddlOperatorF As DropDownList
        Dim ddlParameterMasterF As DropDownList
        Dim ddlConditionFunctionF As DropDownList
        Dim txtValueF As TextBox
        Dim index = e.Item.ItemIndex
        Dim oWSCParamCondition As WSCParameterCondition = New WSCParameterCondition
        Dim arlWSCParamDetail As ArrayList = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If ItemType = ListItemType.Item OrElse ItemType = ListItemType.AlternatingItem Then
            ddlParameter = CType(e.Item.FindControl("lblParameter0"), Label)
            ddlOperator = CType(e.Item.FindControl("lblOperator0"), Label)
            lblValue = CType(e.Item.FindControl("lblValue0"), Label)
            lblParameterMaster = CType(e.Item.FindControl("lblParameterMaster"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo0"), Label)
            lblFunction = CType(e.Item.FindControl("lblFunction"), Label)
            oWSCParamCondition = arlWSCParamDetail(index)
            ddlParameter.Text = New EnumWSCParamParameter().RetrieveWSCParameter(oWSCParamCondition.Kind)
            ddlOperator.Text = New EnumWSCParamParameter().RetrieveWSCParamOperator(oWSCParamCondition.Operators)
            lblValue.Text = oWSCParamCondition.Value
            If oWSCParamCondition.WSCParameterConditionID > 0 Then
                lblParameterMaster.Text = getConditionIndex(oWSCParamCondition)
            Else
                lblParameterMaster.Text = ""
            End If
            lblNo.Text = index + 1
            If oWSCParamCondition.Functions = 0 Then
                lblFunction.Text = "AND"
            ElseIf oWSCParamCondition.Functions = 1 Then
                lblFunction.Text = "OR"
            End If

        ElseIf ItemType = ListItemType.Footer Then
            ddlParameterF = CType(e.Item.FindControl("ddlParameterFooter0"), DropDownList)
            ddlOperatorF = CType(e.Item.FindControl("ddlOperatorFooter0"), DropDownList)
            ddlParameterMasterF = CType(e.Item.FindControl("ddlParameterMaster"), DropDownList)
            ddlConditionFunctionF = CType(e.Item.FindControl("ddlConditionFunction"), DropDownList)
            txtValueF = CType(e.Item.FindControl("txtValueFooter0"), TextBox)
            BindParameter(ddlParameterF)
            BindOperator(ddlOperatorF)
            BindParameterMaster(ddlParameterMasterF, arlWSCParamDetail.Count)
            BindConditionFunction(ddlConditionFunctionF)
            ddlParameterF.ClearSelection()
            ddlOperatorF.ClearSelection()
            ddlParameterMasterF.ClearSelection()
            ddlConditionFunctionF.ClearSelection()
            txtValueF.Text = ""

        ElseIf ItemType = ListItemType.EditItem Then
            ddlParameterF = CType(e.Item.FindControl("ddlParameterEditC"), DropDownList)
            ddlOperatorF = CType(e.Item.FindControl("ddlOperatorEditC"), DropDownList)
            ddlParameterMasterF = CType(e.Item.FindControl("ddlParameterMasterEditC"), DropDownList)
            ddlConditionFunctionF = CType(e.Item.FindControl("ddlConditionFunctionEditC"), DropDownList)

            Dim condToEdit As WSCParameterCondition = CType(sessHelper.GetSession("ConditionEdit"), WSCParameterCondition)

            BindParameter(ddlParameterF)
            BindOperator(ddlOperatorF)
            BindParameterMaster(ddlParameterMasterF, CType(sessHelper.GetSession("ConditionGrid"), ArrayList).Count)
            BindConditionFunction(ddlConditionFunctionF)

            ddlParameterF.ClearSelection()
            ddlParameterF.SelectedValue = condToEdit.Kind
            ddlParameterF.Enabled = False
            ddlOperatorF.SelectedValue = condToEdit.Operators
            ddlParameterMasterF.SelectedIndex = condToEdit.WSCParameterConditionIndex
            ddlConditionFunctionF.SelectedValue = condToEdit.Functions
            setInputField(e, condToEdit.Kind, condToEdit.Value, "condition", "edit")
        End If
    End Sub

    Protected Sub dtgWscCondition_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgWscCondition.ItemCommand
        Dim _arConditionGrid As ArrayList = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)

        Dim ddlParameterF As DropDownList
        Dim ddlOperatorF As DropDownList
        Dim ddlParameterMasterF As DropDownList
        Dim ddlConditionFunctionF As DropDownList
        Dim txtValueF As TextBox
        Dim strValue As String
        Dim strParameterMaster As String

        Select Case e.CommandName

            Case "Add" 'Insert New item to datagrid
                ddlParameterF = CType(e.Item.FindControl("ddlParameterFooter0"), DropDownList)
                ddlOperatorF = CType(e.Item.FindControl("ddlOperatorFooter0"), DropDownList)
                ddlParameterMasterF = CType(e.Item.FindControl("ddlParameterMaster"), DropDownList)
                ddlConditionFunctionF = CType(e.Item.FindControl("ddlConditionFunction"), DropDownList)
                txtValueF = CType(e.Item.FindControl("txtValueFooter0"), TextBox)

                If ddlParameterF.SelectedValue = 4 OrElse ddlParameterF.SelectedValue = 6 OrElse ddlParameterF.SelectedValue = 7 OrElse ddlParameterF.SelectedValue = 8 OrElse
                    ddlParameterF.SelectedValue = 16 OrElse ddlParameterF.SelectedValue = 17 OrElse ddlParameterF.SelectedValue = 20 Then
                    txtValueF = CType(e.Item.FindControl("txtValueFooterNum0"), TextBox)
                    strValue = txtValueF.Text
                ElseIf ddlParameterF.SelectedValue = 1 OrElse ddlParameterF.SelectedValue = 2 OrElse ddlParameterF.SelectedValue = 3 OrElse ddlParameterF.SelectedValue = 18 Then
                    strValue = CType(e.Item.FindControl("icValueFooter0"), KTB.DNet.WebCC.IntiCalendar).Value
                Else
                    txtValueF = CType(e.Item.FindControl("txtValueFooter0"), TextBox)
                    strValue = txtValueF.Text
                End If

                If ddlConditionFunctionF.SelectedValue < 0 Then
                    MessageBox.Show("Function tidak boleh kosong!")
                    Exit Sub
                End If

                If ddlParameterMasterF.SelectedValue > 0 Then
                    If Not validateParamAndMasterParam(ddlParameterF.SelectedValue, ddlParameterMasterF.SelectedValue) Then
                        Exit Sub
                    End If
                    strParameterMaster = ddlParameterMasterF.SelectedValue
                Else
                    strParameterMaster = ""
                End If

                'add Value Validation
                If Not valueValidation(strValue, ddlParameterF.SelectedValue) Then
                    Exit Sub
                End If

                'Add Part Validation
                If ddlParameterF.SelectedValue = 15 Then

                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aDatas As ArrayList

                    ' Terdiri dari OR Tidak Terdiri dari
                    If (ddlOperatorF.SelectedValue = 9 Or ddlOperatorF.SelectedValue = 10) Then

                        If (strValue.Trim() <> String.Empty) Then
                            criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.InSet, "('" & strValue.Trim().Replace(";", "','") & "')"))
                            criterias.opAnd(New Criteria(GetType(SparePartMaster), "ProductCategory.Code", MatchType.InSet, "('MMC','ALL')"))
                        End If

                        aDatas = New SparePartMasterFacade(User).Retrieve(CType(criterias, CriteriaComposite))

                        If aDatas.Count = 0 Then
                            MessageBox.Show("Kode Part tidak terdaftar.")
                            Exit Sub
                        End If
                    End If
                End If

                If ddlOperatorF.SelectedIndex > 0 AndAlso ddlParameterF.SelectedIndex > 0 AndAlso strValue.Trim.Length > 0 Then
                    If ddlParameterF.SelectedIndex = 6 Then
                        If lblClaimType.Text.Trim.ToUpper = "Z2" Then
                            If Not Char.IsLetter(txtValueF.Text) OrElse txtValueF.Text = "XEE999" Then
                                insertConditionSession(ddlParameterF.SelectedValue, ddlOperatorF.SelectedValue, strValue, ddlConditionFunctionF.SelectedValue, strParameterMaster)
                            Else
                                MessageBox.Show("Untuk Type Claim Z2, \n Kode Posisi hanya boleh yang berawalan Angka")
                            End If
                        ElseIf lblClaimType.Text.Trim.ToUpper = "Z4" Then
                            If Char.IsLetter(txtValueF.Text) Then
                                insertConditionSession(ddlParameterF.SelectedValue, ddlOperatorF.SelectedValue, strValue, ddlConditionFunctionF.SelectedValue, strParameterMaster)
                            Else
                                MessageBox.Show("Untuk Type Claim Z4, \n Kode Posisi hanya boleh yang berawalan Huruf")
                            End If
                        Else
                            insertConditionSession(ddlParameterF.SelectedValue, ddlOperatorF.SelectedValue, strValue, ddlConditionFunctionF.SelectedValue, strParameterMaster)
                        End If
                    Else
                        insertConditionSession(ddlParameterF.SelectedValue, ddlOperatorF.SelectedValue, strValue, ddlConditionFunctionF.SelectedValue, strParameterMaster)
                    End If
                ElseIf ddlOperatorF.SelectedIndex = 0 AndAlso ddlParameterF.SelectedIndex = 0 AndAlso strValue.Trim.Length = 0 Then
                    MessageBox.Show("Data masih kosong")
                ElseIf ddlOperatorF.SelectedIndex = 0 Then
                    MessageBox.Show("Silahkan pilih Operator terlebih dahulu")
                ElseIf ddlParameterF.SelectedIndex = 0 Then
                    MessageBox.Show("Silahkan pilih Parameter terlebih dahulu")
                ElseIf strValue.Trim.Length = 0 Then
                    MessageBox.Show("Silahkan isi value terlebih dahulu")
                End If

            Case "Edit"
                Dim condGrid As DataGrid = Me.Page.FindControl("dtgWscCondition")
                condGrid.ShowFooter = False
                condGrid.EditItemIndex = CInt(e.Item.ItemIndex)
                Dim condToEdit As WSCParameterCondition = CType(sessHelper.GetSession("ConditionGrid"), ArrayList)(e.Item.ItemIndex)
                sessHelper.SetSession("ConditionEdit", condToEdit)

            Case "Update"
                Dim condToEdit As WSCParameterCondition = CType(sessHelper.GetSession("ConditionEdit"), WSCParameterCondition)
                Dim value As String
                ddlParameterF = CType(e.Item.FindControl("ddlParameterEditC"), DropDownList)
                ddlOperatorF = CType(e.Item.FindControl("ddlOperatorEditC"), DropDownList)
                ddlParameterMasterF = CType(e.Item.FindControl("ddlParameterMasterEditC"), DropDownList)
                ddlConditionFunctionF = CType(e.Item.FindControl("ddlConditionFunctionEditC"), DropDownList)

                condToEdit.Operators = ddlOperatorF.SelectedValue
                If ddlParameterMasterF.SelectedValue > -1 Then
                    If Not validateParamAndMasterParam(ddlParameterF.SelectedValue, ddlParameterMasterF.SelectedValue) Then
                        Exit Sub
                    End If
                    condToEdit.WSCParameterConditionID = ddlParameterMasterF.SelectedValue
                    condToEdit.WSCParameterConditionIndex = ddlParameterMasterF.SelectedValue
                Else
                    condToEdit.WSCParameterConditionID = Nothing
                    condToEdit.WSCParameterConditionIndex = Nothing
                End If
                condToEdit.Functions = ddlConditionFunctionF.SelectedValue

                'get value
                value = condToEdit.Value
                If condToEdit.Kind = 0 Or condToEdit.Kind = 5 Or condToEdit.Kind = 9 Or condToEdit.Kind = 10 Or condToEdit.Kind = 11 Or
                    condToEdit.Kind = 12 Or condToEdit.Kind = 13 Or condToEdit.Kind = 15 Then
                    Dim val As TextBox = CType(e.Item.FindControl("txtValueEditC"), TextBox)
                    If validateInputValue(val.Text) Then
                        value = val.Text
                    Else
                        MessageBox.Show("Silahkan isi value terlebih dahulu")
                        Exit Sub
                    End If
                ElseIf condToEdit.Kind = 1 Or condToEdit.Kind = 2 Or condToEdit.Kind = 3 Then
                    Dim calendar As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icValueEditC"), KTB.DNet.WebCC.IntiCalendar)
                    If validateInputValue(calendar.Value) Then
                        value = calendar.Value
                    Else
                        MessageBox.Show("Silahkan isi value terlebih dahulu")
                        Exit Sub
                    End If
                ElseIf condToEdit.Kind = 4 Or condToEdit.Kind = 6 Or condToEdit.Kind = 7 Or condToEdit.Kind = 8 Or condToEdit.Kind = 16 Or condToEdit.Kind = 20 Then
                    Dim txtValueEditNum As TextBox = CType(e.Item.FindControl("txtValueEditNumC"), TextBox)
                    If validateInputValue(txtValueEditNum.Text) Then
                        value = txtValueEditNum.Text
                    Else
                        MessageBox.Show("Silahkan isi value terlebih dahulu")
                        Exit Sub
                    End If
                End If

                'add Value Validation
                If Not valueValidation(value, condToEdit.Kind) Then
                    Exit Sub
                End If

                condToEdit.Value = value

                saveUpdateCondtion(condToEdit)
                Dim condGrid As DataGrid = Me.Page.FindControl("dtgWscCondition")
                condGrid.ShowFooter = True
                condGrid.EditItemIndex = -1

                BindDataGrid(-1, -1)

            Case "Cancel"
                sessHelper.RemoveSession("ConditionEdit")
                Dim condGrid As DataGrid = Me.Page.FindControl("dtgWscCondition")
                condGrid.ShowFooter = True
                condGrid.EditItemIndex = -1
                BindDataGrid(-1, -1)

            Case "Delete" 'Delete this datagrid item

                deleteConditionSession(e.Item.ItemIndex)

        End Select

endConditionItemCmd:
        BindDataGrid(-1, -1)
    End Sub

End Class