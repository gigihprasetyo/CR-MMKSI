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

Public Class FrmWSCParameterHeader
    Inherits System.Web.UI.Page

#Region "Private Variable"
    Private sessHelper As SessionHelper = New SessionHelper
    Private m_bFormPrivilege As Boolean = False
    Private m_bCreatePrivilege As Boolean = False
    Private m_bUpdatePrivilege As Boolean = False
    Private m_bActivatePrivilege As Boolean = False

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
        End If
    End Sub

    Private Sub SetControlPrivilege()
        If m_bCreatePrivilege OrElse m_bUpdatePrivilege Then
            btnSimpan.Visible = True
            btnBatal.Visible = True
        ElseIf m_bFormPrivilege OrElse m_bActivatePrivilege Then
            btnSimpan.Visible = False
            btnBatal.Visible = False
        End If
        btnCari.Visible = True
    End Sub
#End Region

#Region "Custom Method"

    Private Sub BindClaimType()
        With ddlClaimType.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", "No"))
            .Add(New ListItem("Z2", "Z2"))
            .Add(New ListItem("Z4", "Z4"))
            .Add(New ListItem("Z6", "Z6"))
            .Add(New ListItem("ZA", "ZA"))
            .Add(New ListItem("ZB", "ZB"))
        End With
    End Sub

    Private Sub BindStatus()
        With ddlStatus.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            .Add(New ListItem("Aktif", 0))
            .Add(New ListItem("Tidak Aktif", 1))
        End With
    End Sub

    Private Sub BindVehicleType()
        BindVehicleSubCategoryToDDL(lboxCategory)
    End Sub

    Public Sub BindVehicleSubCategoryToDDL(ByRef ddlSubCategory As ListBox)
        ddlSubCategory.Items.Clear()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        '-- SubCategoryVehicle criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.No, "X"))  '-- Status still active
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ProductCategory.Code", MatchType.Exact, companyCode))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(SubCategoryVehicle), "Name", Sort.SortDirection.ASC))  '-- Sort by Vechile type code

        '-- Bind ddlSubCategory dropdownlist
        ddlSubCategory.DataSource = New SubCategoryVehicleFacade(User).Retrieve(criterias, sortColl)
        ddlSubCategory.DataTextField = "Name"
        ddlSubCategory.DataValueField = "ID"
        ddlSubCategory.DataBind()
        'ddlSubCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item
    End Sub

    Function GetSubOfExcCVList() As ArrayList
        Dim arlRsl As New ArrayList
        For Each item As EnumWSCParamVehicleType.WSCParamVehicleTypeProp In New EnumWSCParamVehicleType().RetrieveWSCParamVehicleType()
            arlRsl.Add(New ListItem(item.Name, item.Val))
        Next
        Return arlRsl
    End Function

    Public Function GetSubOfExclCVString(ByVal val As Short) As String
        Return New EnumWSCParamVehicleType().RetrieveWSCParamVehicleType(val)
    End Function

    'Public Sub BindVehicleSubCategoryToDDL(ByRef ddlSubCategory As ListBox)
    '    Dim arlItem As ArrayList
    '    arlItem = GetSubOfExcCVList()

    '    ddlSubCategory.Items.Clear()
    '    If Not IsNothing(arlItem) Then
    '        For Each li As ListItem In arlItem
    '            ddlSubCategory.Items.Add(li)
    '        Next
    '    End If
    'End Sub

    Private Function GetSubOfExcCVSQLValue(ByVal Val As Short) As String

        Dim strRsl As String = ""
        'If Val = New EnumWSCParamVehicleType().StradaTriton Then strRsl = "STRADA%;TRITON%" ' "CR"
        If Val = New EnumWSCParamVehicleType().StradaTriton Then strRsl = "STRADA%;TRITON%;CR%" ' "CR"
        If Val = New EnumWSCParamVehicleType().Lancer_Grandis_Maven Then strRsl = "%LANCER%;GRANDIS%;MAVEN%" ' "MAVEN;GRANDIS%"
        'If Val = New EnumWSCParamVehicleType().Pajero_PajeroSport Then strRsl = "PAJERO%"
        If Val = New EnumWSCParamVehicleType().Pajero_PajeroSport Then strRsl = "%PAJERO%;QX%"
        If Val = New EnumWSCParamVehicleType().OutlanderSport Then strRsl = "%OUTLANDER%"
        If Val = New EnumWSCParamVehicleType().Mirage Then strRsl = "%MIRAGE%"
        If Val = New EnumWSCParamVehicleType().Delica Then strRsl = "%Delica%"
        If Val = New EnumWSCParamVehicleType().XPander Then strRsl = "%XPander%"
        'If Val = New EnumWSCParamVehicleType().T120ss Then strRsl = "T120SS%"
        If Val = New EnumWSCParamVehicleType().T120ss Then strRsl = "%T120SS%"
        If Val = New EnumWSCParamVehicleType().L300 Then strRsl = "L300%" '"Colt L300%"

        Return strRsl
    End Function
#End Region

#Region "Event Handler"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ActivateParameterPrivilege()
        If Not IsPostBack Then
            Initialize()
            BindClaimType()
            BindVehicleType()
            BindStatus()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub Initialize()
        ClearData()
        SetControlPrivilege()
        ViewState("currSortColumn") = "ID"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()
        lboxCategory.Items.Clear()
        lboxVehicleType.Items.Clear()
        ddlClaimType.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0

        txtDescription.Text = String.Empty
        BindVehicleType()
        BindLbVehicleType(-1)
        txtDescription.Text = String.Empty
        ViewState("vsProcess") = "Insert"
        lboxCategory.Enabled = True
        lblCategory.Text = ""
        lblVehicleType.Text = ""
    End Sub

    Public Function CriteriaSearch(ByVal val As String) As CriteriaComposite

        Dim criterias As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If val <> -1 Then
            'Dim Sql As String = ""
            'Dim sVals As String = GetSubOfExcCVSQLValue(val)

            'Sql &= " select distinct(ID) from VechileType "
            'Sql &= " where RowStatus = 0 "
            'Dim i As Integer
            'For i = 0 To sVals.Split(";").Length - 1
            '    If i = 0 Then
            '        Sql &= " and (Description like '" & sVals.Split(";")(i) & "' "
            '        If sVals.Split(";").Length = 1 Then Sql &= ")"
            '    ElseIf i = sVals.Split(";").Length - 1 Then
            '        Sql &= " or Description like '" & sVals.Split(";")(i) & "') "
            '    Else
            '        Sql &= " or Description like '" & sVals.Split(";")(i) & "'"
            '    End If
            'Next
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.VechileType), "ID", MatchType.InSet, "(" & Sql & ")"))

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & val
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))

        End If
        Return criterias
    End Function

    Private Sub BindLbVehicleType(ByVal val As String)
        If val > -1 Then
            Dim li As ListItem
            Dim sortCol As SortCollection = New SortCollection
            sortCol.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
            Dim objVehicleType As ArrayList = New VechileTypeFacade(User).Retrieve(CriteriaSearch(val), sortCol)
            For Each oneVehicleType As VechileType In objVehicleType
                li = New ListItem(oneVehicleType.VechileTypeCode, oneVehicleType.ID.ToString)
                lboxVehicleType.Items.Add(li)
            Next
        End If
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim data As ArrayList = New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite
        Try
            If ViewState("vsProcess") = "Search" Then
                criterias = New CriteriaComposite(New Criteria(GetType(WSCParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If ddlClaimType.SelectedValue <> "No" Then
                    criterias.opAnd(New Criteria(GetType(WSCParameterHeader), "ClaimType", MatchType.Exact, ddlClaimType.SelectedItem.Text))
                End If
                Dim lboxVTypeCount As Integer = (From item In lboxVehicleType.Items Where item.Selected Select item).Count()
                If lboxVTypeCount > 0 Then
                    Dim idVType As String = String.Empty
                    For Each Vt As Integer In lboxVehicleType.GetSelectedIndices()
                        Dim li As ListItem = lboxVehicleType.Items(Vt)
                        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteria.opAnd(New Criteria(GetType(WSCParameterVehicle), "VechileType.ID", MatchType.Exact, li.Value))
                        Dim arlVType As ArrayList = New WSCParameterVehicleFacade(User).Retrieve(criteria)
                        For Each item As WSCParameterVehicle In arlVType
                            If idVType.Trim.Length > 0 Then
                                idVType += ",'" + item.WSCParameterHeader.ID.ToString + "'"
                            Else
                                idVType = "'" + item.WSCParameterHeader.ID.ToString + "'"
                            End If
                        Next
                    Next
                    criterias.opAnd(New Criteria(GetType(WSCParameterHeader), "ID", MatchType.InSet, "(" + idVType + ")"))
                End If
                If txtDescription.Text.Trim.Length > 0 Then
                    criterias.opAnd(New Criteria(GetType(WSCParameterHeader), "Description", MatchType.Partial, txtDescription.Text))
                End If
                If ddlStatus.SelectedValue > -1 Then
                    criterias.opAnd(New Criteria(GetType(WSCParameterHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))
                End If

            Else
                criterias = New CriteriaComposite(New Criteria(GetType(WSCParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            End If
            data = New WSCParameterHeaderFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgWscParam.PageSize, totalRow)
            sessHelper.SetSession("GridSess", data)
            dtgWscParam.CurrentPageIndex = indexPage
            dtgWscParam.DataSource = data
            dtgWscParam.VirtualItemCount = totalRow
            dtgWscParam.DataBind()
        Catch ex As Exception
            dtgWscParam.DataSource = data
            dtgWscParam.DataBind()
        End Try

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        'Dim asd As Integer = New DeskripsiWorkPositionFacade(User).ValidateCode("1PC")
        'Exit Sub
        Try
            Dim valReturn = validateFunc()
            If valReturn = 0 Then
                SaveData(ViewState("vsProcess"))
                ClearData()
                dtgWscParam.CurrentPageIndex = 0
                dtgWscParam.SelectedIndex = -1
                BindDataGrid(dtgWscParam.CurrentPageIndex)
            End If
        Catch
            MessageBox.Show(SR.SaveFail)
        End Try

    End Sub

    Private Sub SaveData(ByVal vsProses As String)
        Dim objWSCParamHeader As WSCParameterHeader = New WSCParameterHeader
        Dim objWSCParamVehicle As WSCParameterVehicle = New WSCParameterVehicle
        Dim objWSCParamHeaderFacade As WSCParameterHeaderFacade = New WSCParameterHeaderFacade(User)
        Dim objWSCParamVehicleFacade As WSCParameterVehicleFacade = New WSCParameterVehicleFacade(User)
        Dim nResult As Integer = -1

        objWSCParamHeader.ClaimType = ddlClaimType.SelectedItem.Text
        objWSCParamHeader.Description = txtDescription.Text
        objWSCParamHeader.Status = ddlStatus.SelectedValue


        If CType(ViewState("vsProcess"), String) = "Edit" Then
            Dim oWSC As WSCParameterHeader = CType(sessHelper.GetSession("WSCParamHeaderEdit"), WSCParameterHeader)
            objWSCParamHeader.ID = oWSC.ID
            nResult = objWSCParamHeaderFacade.Update(objWSCParamHeader)
        Else
            If txtDescription.Text.Trim.Length = 0 Then
                MessageBox.Show("Deskripsi parameter masih kosong")
                nResult = -1
            ElseIf txtDescription.Text.Trim.Length <> 0 Then
                Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(WSCParameterHeader), "Description", MatchType.Exact, txtDescription.Text))
                Dim arlParamHeaderDesc As ArrayList = New WSCParameterHeaderFacade(User).Retrieve(criteria)
                If arlParamHeaderDesc.Count > 0 Then
                    MessageBox.Show("Deskripsi sudah pernah ada")
                    nResult = -1
                Else
                    nResult = objWSCParamHeaderFacade.Insert(objWSCParamHeader)
                End If
            End If
        End If

        If nResult > 0 Then
            Dim vTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
            Dim arrSelectedLbox As ArrayList = New ArrayList
            Dim notInSet As String = String.Empty
            objWSCParamVehicle.WSCParameterHeader = objWSCParamHeaderFacade.Retrieve(nResult)
            For Each Vt As Integer In lboxVehicleType.GetSelectedIndices()
                Dim SelVtID As Integer = CType(lboxVehicleType.Items(Vt).Value, Integer)
                arrSelectedLbox.Add(SelVtID)
                If notInSet.Trim.Length > 0 Then
                    notInSet += "," + SelVtID.ToString + ""
                Else
                    notInSet += "" + SelVtID.ToString + ""
                End If
            Next

            If vsProses = "Edit" Then
                nResult = CType(sessHelper.GetSession("WSCParamHeaderEdit"), WSCParameterHeader).ID
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.ID", MatchType.Exact, nResult))
                Dim arrWscParVe As ArrayList = objWSCParamVehicleFacade.Retrieve(crit)
                objWSCParamVehicle.WSCParameterHeader = objWSCParamHeaderFacade.Retrieve(nResult)
                For Each item As WSCParameterVehicle In arrWscParVe
                    objWSCParamVehicleFacade.Delete(item)
                Next
                For Each Vt As Integer In arrSelectedLbox
                    Dim exist As Integer = objWSCParamVehicleFacade.ExistVehicleType(nResult.ToString, Vt.ToString, False)
                    Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.ID", MatchType.Exact, nResult))
                    crits.opAnd(New Criteria(GetType(WSCParameterVehicle), "VechileType.ID", MatchType.Exact, Vt))
                    objWSCParamVehicle.VechileType = vTypeFacade.Retrieve(Vt)
                    If exist > 0 Then
                        objWSCParamVehicle = objWSCParamVehicleFacade.Retrieve(crits)(0)
                        objWSCParamVehicle.RowStatus = CType(DBRowStatus.Active, Short)
                        objWSCParamVehicleFacade.Update(objWSCParamVehicle)
                    Else
                        objWSCParamVehicleFacade.Insert(objWSCParamVehicle)
                    End If
                Next
            Else
                For Each Vt As Integer In arrSelectedLbox
                    objWSCParamVehicle.VechileType = vTypeFacade.Retrieve(Vt)
                    nResult = objWSCParamVehicleFacade.Insert(objWSCParamVehicle)
                Next
            End If
        End If

        If nResult <= 0 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
        End If

    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Page_Load(sender, e)
        ClearData()

    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        ViewState("vsProcess") = "Search"
        dtgWscParam.CurrentPageIndex = 0
        BindDataGrid(dtgWscParam.CurrentPageIndex)
    End Sub

    Private Sub dtgWscParam_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgWscParam.ItemCommand
        If e.CommandName = "Page" OrElse e.CommandName = "Sort" Then
            Exit Sub
        End If

        Dim RowValue As WSCParameterHeader
        Dim arrGrid As ArrayList = CType(sessHelper.GetSession("GridSess"), ArrayList)
        RowValue = CType(arrGrid(e.Item.ItemIndex), WSCParameterHeader)  '-- Current record

        If e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.ID", MatchType.Exact, RowValue.ID))
            Dim arrVehicles As ArrayList = New WSCParameterVehicleFacade(User).Retrieve(criteria)
            ddlClaimType.SelectedValue = RowValue.ClaimType
            ddlStatus.SelectedValue = RowValue.Status

            txtDescription.Text = RowValue.Description
            getCategoryFromType(RowValue.ID)
            sessHelper.SetSession("WSCParamHeaderEdit", RowValue)

            BindDataGrid(0)  '-- Bind page-1

        ElseIf e.CommandName = "Active" Then
            ActivateParameter(e.Item.Cells(0).Text)
            BindDataGrid(0)  '-- Bind page-1

        ElseIf e.CommandName = "Inactive" Then
            InActivateParameter(e.Item.Cells(0).Text)
            BindDataGrid(0)  '-- Bind page-1

        ElseIf e.CommandName = "Delete" Then
            DeleteCampaign(e.Item.Cells(0).Text)
            BindDataGrid(0)  '-- Bind page-1

        ElseIf e.CommandName = "View" Then
            sessHelper.SetSession("backURL", "./FrmWSCParameterHeader.aspx")
            sessHelper.SetSession("vsWSCParameterID", e.Item.Cells(0).Text)
            If Not IsNothing(sessHelper.GetSession("vsWSCParameterID")) Then
                Response.Redirect("FrmWSCParameterDetail.aspx?Mode=View")
            End If
        ElseIf e.CommandName = "Detail" Then
            sessHelper.SetSession("backURL", "./FrmWSCParameterHeader.aspx")
            sessHelper.SetSession("vsWSCParameterID", e.Item.Cells(0).Text)
            If Not IsNothing(sessHelper.GetSession("vsWSCParameterID")) Then
                Response.Redirect("FrmWSCParameterDetail.aspx?Mode=Detail")
            End If
        End If
    End Sub

    Private Sub ActivateParameter(ByVal nID As Integer)
        '-- Activate Parameter
        Dim oWscParamHeader As WSCParameterHeader = New WSCParameterHeaderFacade(User).Retrieve(nID)
        oWscParamHeader.Status = 0  '-- Parameter Aktif
        Dim nResult = New WSCParameterHeaderFacade(User).Update(oWscParamHeader)
    End Sub

    Private Sub InActivateParameter(ByVal nID As Integer)
        '-- Inactivate Parameter
        Dim oWscParamHeader As WSCParameterHeader = New WSCParameterHeaderFacade(User).Retrieve(nID)
        oWscParamHeader.Status = 1  '-- Parameter Tidak Aktif
        Dim nResult = New WSCParameterHeaderFacade(User).Update(oWscParamHeader)
    End Sub

    Private Sub DeleteCampaign(ByVal nID As Integer)
        '-- Inactivate Parameter
        Dim oWscParamHeader As WSCParameterHeader = New WSCParameterHeaderFacade(User).Retrieve(nID)
        oWscParamHeader.RowStatus = CType(DBRowStatus.Deleted, Short)  '-- Parameter Tidak Aktif
        Dim nResult = New WSCParameterHeaderFacade(User).Delete(oWscParamHeader)

    End Sub

    Private Sub dtgWscParam_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgWscParam.ItemDataBound
        If e.Item.ItemIndex <> -1 Then

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim lbtnActive As LinkButton = CType(e.Item.FindControl("lbtnActive"), LinkButton)
            Dim lbtnInactive As LinkButton = CType(e.Item.FindControl("lbtnInactive"), LinkButton)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim lblVehicleType As Label = CType(e.Item.FindControl("lblVehicleType"), Label)
            Dim RowValue As WSCParameterHeader = CType(e.Item.DataItem, WSCParameterHeader)  '-- Current record


            'If Not IsNothing(sessHelper.GetSession("WSCParamHeaderEdit")) Then
            '    RowValue = CType(sessHelper.GetSession("WSCParamHeaderEdit"), WSCParameterHeader)
            '    sessHelper.RemoveSession("WSCParamHeaderEdit")
            'Else
            '    RowValue = CType(e.Item.DataItem, WSCParameterHeader)  '-- Current record
            'End If

            If RowValue.Status = 0 Then
                If sessHelper.GetSession("ActivateParameter") Then
                    lbtnInactive.Visible = True
                Else
                    lbtnInactive.Visible = False
                End If
                lbtnActive.Visible = False

            ElseIf RowValue.Status = 1 Then
                lbtnInactive.Visible = False
                If sessHelper.GetSession("ActivateParameter") Then
                    lbtnActive.Visible = True
                Else
                    lbtnActive.Visible = False
                End If
            End If

            If sessHelper.GetSession("UpdateParameter") Then
                lbtnEdit.Visible = True
                lbtnDelete.Visible = True
            Else
                lbtnEdit.Visible = False
                lbtnDelete.Visible = False
            End If

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1 + (dtgWscParam.CurrentPageIndex * dtgWscParam.PageSize), String)
            End If

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim strVehicle As String = String.Empty
                Dim oWscParamVehicle As WSCParameterVehicleFacade = New WSCParameterVehicleFacade(User)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.ID", MatchType.Exact, RowValue.ID))

                Dim arlWPV As ArrayList = oWscParamVehicle.Retrieve(criterias)

                For Each code As WSCParameterVehicle In arlWPV
                    Dim vechileTypeCode As String
                    If IsNothing(code.VechileType) Then
                        vechileTypeCode = "000"
                    Else
                        vechileTypeCode = code.VechileType.VechileTypeCode
                    End If
                    If strVehicle.Length = 0 Then
                        strVehicle += vechileTypeCode
                    Else
                        strVehicle += ";" + vechileTypeCode
                    End If
                Next

                If strVehicle.Length > 30 Then
                    CType(e.Item.FindControl("lblVehicleType"), Label).Text = strVehicle.Substring(0, 30) & ".."
                Else
                    CType(e.Item.FindControl("lblVehicleType"), Label).Text = strVehicle
                End If
            End If

            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                '-- Confirm activation
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).ToolTip = "Edit"
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Attributes.Add("OnClick", "return confirm('Ubah record ini?');")
            End If
            If Not e.Item.FindControl("lbtnActive") Is Nothing Then
                '-- Confirm activation
                CType(e.Item.FindControl("lbtnActive"), LinkButton).ToolTip = "Aktifkan"
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Aktifkan record ini?');")
            End If
            If Not e.Item.FindControl("lbtnInactive") Is Nothing Then
                '-- Confirm inactivation
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).ToolTip = "Non Aktifkan"
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Attributes.Add("OnClick", "return confirm('Non-Aktifkan record ini?');")
            End If
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                '-- Confirm deletion
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).ToolTip = "Hapus"
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Hapus record ini?');")
            End If

        End If
    End Sub

    Private Sub dtgWscParam_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgWscParam.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        dtgWscParam.SelectedIndex = -1
        dtgWscParam.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub

    Private Sub dtgWscParam_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgWscParam.PageIndexChanged
        BindDataGrid(e.NewPageIndex)
    End Sub

    Private Sub lboxVehicleType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lboxVehicleType.SelectedIndexChanged
        Dim val As String
        lblVehicleType.Text = ""
        Dim arr As ArrayList = New ArrayList
        For Each Vt As Integer In lboxVehicleType.GetSelectedIndices()
            arr.Add(Vt)
            If lblVehicleType.Text.Trim = "" Then
                lblVehicleType.Text = lboxVehicleType.Items(Vt).Text
                val = lblVehicleType.Text
            Else
                lblVehicleType.Text += ";" & lboxVehicleType.Items(Vt).Text
            End If
        Next
        ScrollToLastIndex(val)
    End Sub

    Private Sub ScrollToLastIndex(val As String)
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "setFocus('" & val & "');", True)
    End Sub

    Protected Sub btnSerchCode_Click(sender As Object, e As EventArgs) Handles btnSerchCode.Click
        SearchVehicleType()
    End Sub

    Private Sub SearchVehicleType()
        lblCategory.Text = ""
        lblVehicleType.Text = ""
        Dim arr As ArrayList = New ArrayList
        For Each Vt As Integer In lboxCategory.GetSelectedIndices()
            'arr.Add(Vt)
            arr.Add(lboxCategory.Items(Vt).Value.ToString())
            If lblCategory.Text.Trim = "" Then
                'lblCategory.Text = GetSubOfExclCVString(Vt)
                lblCategory.Text = lboxCategory.Items(Vt).Text
            Else
                lblCategory.Text += ";" & lboxCategory.Items(Vt).Text
            End If
        Next
        lboxVehicleType.Items.Clear()
        For Each i As Integer In arr
            BindLbVehicleType(i)
        Next
    End Sub

    Private Function validateFunc() As Integer
        Dim valid As Integer = 0

        If valid = 0 AndAlso ddlClaimType.SelectedValue = "No" Then
            MessageBox.Show("Tipe claim masih kosong")
            valid = -1
        End If

        Dim lboxVTypeCount As Integer = 0
        Try
            lboxVTypeCount = (From item In lboxVehicleType.Items Where item.Selected Select item).Count()
        Catch
            MessageBox.Show("Tipe kendaraan masih kosong")
            valid = -1
        End Try

        If valid = 0 AndAlso lboxVTypeCount = 0 Then
            MessageBox.Show("Kode dari tipe kendaraan masih kosong")
            valid = -1
        End If

        If valid = 0 AndAlso ddlStatus.SelectedValue = -1 Then
            MessageBox.Show("Status masih kosong")
            valid = -1
        End If

        Return valid
    End Function

    Private Sub getCategoryFromType(ByVal ParamHeadID As Integer)
        BindVehicleType()
        Dim arlParamVehicle As ArrayList = New ArrayList
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.ID", MatchType.Exact, ParamHeadID))
        arlParamVehicle = New WSCParameterVehicleFacade(User).Retrieve(criteria)
        For Each item As WSCParameterVehicle In arlParamVehicle
            If Not IsNothing(item.VechileType) Then
                'selectCategoryListBox(item.VechileType.Description.Split(" "))
                selectCategoryListBox2(item.VechileType.VechileModel.ID)
            End If
        Next

        SearchVehicleType()
        selectVehicleTypeListBox(ParamHeadID)
        lboxVehicleType_SelectedIndexChanged(New Object, New System.EventArgs)
    End Sub

    Private Sub selectCategoryListBox(ByVal p1 As String())
        For Each item As String In p1
            If item.ToUpper = "TRITON".ToUpper Then
                lboxCategory.Items(New EnumWSCParamVehicleType().StradaTriton).Selected = True
            ElseIf item.ToUpper = "LANCER".ToUpper OrElse item.ToUpper = "GRANDIS".ToUpper OrElse item.ToUpper = "MAVEN".ToUpper Then
                lboxCategory.Items(New EnumWSCParamVehicleType().Lancer_Grandis_Maven).Selected = True
            ElseIf item.ToUpper = "PAJERO".ToUpper Then
                lboxCategory.Items(New EnumWSCParamVehicleType().Pajero_PajeroSport).Selected = True
            ElseIf item.ToUpper = "OUTLANDER".ToUpper Then
                lboxCategory.Items(New EnumWSCParamVehicleType().OutlanderSport).Selected = True
            ElseIf item.ToUpper = "MIRAGE".ToUpper Then
                lboxCategory.Items(New EnumWSCParamVehicleType().Mirage).Selected = True
            ElseIf item.ToUpper = "DELICA".ToUpper Then
                lboxCategory.Items(New EnumWSCParamVehicleType().Delica).Selected = True
            ElseIf item.ToUpper = "XPANDER".ToUpper OrElse item.ToUpper = "EXPANDER".ToUpper Then
                lboxCategory.Items(New EnumWSCParamVehicleType().XPander).Selected = True
            ElseIf item.ToUpper = "T120SS".ToUpper Then
                lboxCategory.Items(New EnumWSCParamVehicleType().T120ss).Selected = True
            ElseIf item.ToUpper = "L300".ToUpper Then
                lboxCategory.Items(New EnumWSCParamVehicleType().L300).Selected = True
            End If
        Next
    End Sub

    Private Sub selectCategoryListBox2(ByVal modelID As Integer)
        If lboxCategory.GetSelectedIndices().Count = lboxCategory.Items.Count Then Exit Sub

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, modelID))
        Dim arlSubCategoryVehicleToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
        For Each item As SubCategoryVehicleToModel In arlSubCategoryVehicleToModel
            Dim iSubCategoryVehicleID As Integer = item.SubCategoryVehicle.ID

            Dim index As Integer
            For index = 0 To lboxCategory.Items.Count - 1
                Dim li As String = lboxCategory.Items(index).Value
                If iSubCategoryVehicleID = li Then
                    lboxCategory.Items(index).Selected = True
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub selectVehicleTypeListBox(ByVal ParamHeadID As Integer)
        Dim index As Integer
        For index = 0 To lboxVehicleType.Items.Count - 1
            Dim li As String = lboxVehicleType.Items(index).Value

            Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.ID", MatchType.Exact, ParamHeadID))
            criteria.opAnd(New Criteria(GetType(WSCParameterVehicle), "VechileType.ID", MatchType.Exact, li))
            Dim arlParamVehicle As ArrayList = New WSCParameterVehicleFacade(User).Retrieve(criteria)
            For Each item As WSCParameterVehicle In arlParamVehicle
                If li = item.VechileType.ID.ToString Then
                    lboxVehicleType.Items(index).Selected = True
                End If
            Next
        Next
    End Sub
#End Region

End Class