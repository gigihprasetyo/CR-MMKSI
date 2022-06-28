Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Security
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility

Public Class FrmProfileGroupDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents dtgProfileGroupDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblKode As System.Web.UI.WebControls.Label
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents dtgAlreadyAssign As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Var"
    Private listProfileHeader As ArrayList
    Private _ProfileHeaderToGroupFacade As New ProfileHeaderToGroupFacade(User)
    Private ssHelper As SessionHelper = New SessionHelper
#End Region

#Region "Custom Method"

    Public Sub InitiatePage()
        Dim strID As String = Request.QueryString("qwqewqwqewwkiopqreopqropr")
        ViewState("vsSortColumn") = "Code"
        ViewState("vsSortDirect") = Sort.SortDirection.ASC
        If IsNumeric(strID) Then
            Dim id As Integer = CInt(strID)
            Dim objGroup As ProfileGroup = GetProfileGroup(id)
            lblKode.Text = objGroup.Code
            lblDescription.Text = objGroup.Description
            Dim ssHelper As SessionHelper = New SessionHelper
            ssHelper.SetSession("PROFILEGROUP", objGroup)
        End If
    End Sub

    Private Sub BindDataAlreadyAssign(ByVal indexpage As Integer)
        Dim totalRow As Integer
        Dim idProfGroup As Integer = CInt(ssHelper.GetSession("IDProfileGroup"))
        If indexpage >= 0 Then
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, idProfGroup))
            Dim arlPH2Group As ArrayList = New ProfileHeaderToGroupFacade(User).RetrieveByCriteria(crit, indexpage + 1, dtgAlreadyAssign.PageSize, totalRow, ViewState("vsSortColumnAA"), ViewState("vsSortDirectAA"))

            dtgAlreadyAssign.VirtualItemCount = totalRow

            If arlPH2Group.Count > 0 Then
                dtgAlreadyAssign.DataSource = arlPH2Group
            Else
                dtgAlreadyAssign.DataSource = New ArrayList
            End If

            If indexpage = 0 Then
                dtgAlreadyAssign.CurrentPageIndex = 0
            End If
        End If
        
        dtgAlreadyAssign.DataBind()
    End Sub

    'get the profile header
    Private Sub BindToGrid()
        Dim objGroup As ProfileGroup = ssHelper.GetSession("PROFILEGROUP")

        Dim objPHFacade As ProfileHeaderFacade = New ProfileHeaderFacade(User)
        Dim arlNeedAssign As ArrayList = objPHFacade.RetrieveActiveList

        'get profile header
        Dim arlAlreadyAssign As New ArrayList
        For Each items As DataGridItem In dtgAlreadyAssign.Items
            Dim lblKodeAA As Label = CType(items.FindControl("lblKodeAA"), Label)
            Dim objPH As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(lblKodeAA.Text.Trim)
            arlAlreadyAssign.Add(objPH)
        Next

        'cek the data
        For Each item As ProfileHeader In arlAlreadyAssign
            For Each item2 As ProfileHeader In arlNeedAssign
                If item2.Code = item.Code Then
                    arlNeedAssign.Remove(item2)
                    Exit For
                End If
            Next
        Next

        If arlNeedAssign.Count > 0 Then
            dtgProfileGroupDetail.DataSource = arlNeedAssign
        Else
            dtgProfileGroupDetail.DataSource = New ArrayList
        End If

        dtgProfileGroupDetail.DataBind()
    End Sub

    Public Sub BindDataGrid()
        Dim objGroup As ProfileGroup = ssHelper.GetSession("PROFILEGROUP")
        Dim objProfileGroupDetails As ArrayList = objGroup.ProfileHeaderToGroups
        Dim listProfileGroupDetails As ArrayList = GetListProfileGroup()
        SortListControl(listProfileGroupDetails, CType(ViewState("vsSortColumn"), String), CType(ViewState("vsSortDirect"), Integer))
        Dim PagedList As ArrayList = ArrayListPager.DoPage(listProfileGroupDetails, 0, dtgProfileGroupDetail.PageSize)
        listProfileHeader = New ArrayList

        For Each item As ProfileHeaderToGroup In objGroup.ProfileHeaderToGroups
            listProfileHeader.Add(item.ProfileHeader.ID)
        Next

        ' take data ProfileHeaderToGroup from objGroup
        Dim criterias As CriteriaComposite
        criterias = New CriteriaComposite(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "ProfileGroup.ID", MatchType.Exact, objGroup.ID))

        Dim totalRow As Integer = 0
        Dim arrlist As ArrayList = New ArrayList
        arrlist = _ProfileHeaderToGroupFacade.RetrieveByCriteria(criterias, 1, dtgProfileGroupDetail.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))


        ssHelper = New SessionHelper
        ssHelper.SetSession("PROFILEBYGROUP", listProfileHeader)
        ssHelper.SetSession("sessProfileHeaderToGroup", arrlist)

        dtgProfileGroupDetail.DataSource = listProfileGroupDetails
        dtgProfileGroupDetail.DataBind()

    End Sub

    Private Function GetListProfileGroup() As ArrayList
        Dim objFacade As ProfileHeaderFacade = New ProfileHeaderFacade(User)
        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        If (Not IsNothing(ViewState("vsSortColumn"))) And (Not IsNothing(ViewState("vsSortDirect"))) Then
            sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(ProfileHeader), ViewState("vsSortColumn"), ViewState("vsSortDirect")))
        Else
            sortColl = Nothing
        End If
        Dim crit As CriteriaComposite
        crit = New CriteriaComposite(New Criteria(GetType(ProfileHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Return objFacade.Retrieve(crit, sortColl)
    End Function

    Private Function GetProfileGroup(ByVal id As Integer) As ProfileGroup
        Dim objFacade As ProfileGroupFacade = New ProfileGroupFacade(User)
        Return objFacade.Retrieve(id)
    End Function

    Private Function GetPriorityValue(ByVal intProfileHeaderID As Integer) As Integer
        Dim arrList As ArrayList = CType(ssHelper.GetSession("sessProfileHeaderToGroup"), ArrayList)
        Dim intValue As Integer = 0
        For Each item As ProfileHeaderToGroup In arrList
            If item.ProfileHeader.ID = intProfileHeaderID Then
                intValue = item.Priority
            End If
        Next
        Return intValue
    End Function

    Private Function InsertNewData(ByVal objProfileGroup As ProfileGroup) As Integer
        Dim listAlreadyAssignProfileToGroup As ArrayList = objProfileGroup.ProfileHeaderToGroups
        Dim objAssignProfileToGroup As ProfileHeaderToGroup
        Dim listAssignProfileToGroup As ArrayList = New ArrayList

        'get the checked data
        Dim arrVal As ArrayList = New ArrayList
        For Each val As DataGridItem In dtgProfileGroupDetail.Items
            Dim cbItem As CheckBox = CType(val.FindControl("cbItem"), CheckBox)
            If cbItem.Checked Then
                Dim lblID_Val As Label = CType(val.FindControl("lblID"), Label)
                Dim txtPriority_Val As TextBox = CType(val.FindControl("txtPriority"), TextBox)
                Dim objProfileHeaderToGroupTmp As ProfileHeaderToGroup = New ProfileHeaderToGroup
                objProfileHeaderToGroupTmp.ID = CType(lblID_Val.Text, Integer)

                'cek the blank priority
                If txtPriority_Val.Text <> "" Then
                    objProfileHeaderToGroupTmp.Priority = CType(txtPriority_Val.Text, Integer)
                Else
                    MessageBox.Show("Priority tidak boleh kosong")
                    Return -1
                End If
                arrVal.Add(objProfileHeaderToGroupTmp)
            End If
        Next

        If arrVal.Count > 0 Then
            ''cek blank priority
            'For Each item As DataGridItem In dtgProfileGroupDetail.Items
            '    Dim cbItem As CheckBox = CType(item.FindControl("cbItem"), CheckBox)
            '    If cbItem.Checked Then
            '        Dim lblID As Label = CType(item.FindControl("lblID"), Label)
            '        Dim txtPriority As TextBox = CType(item.FindControl("txtPriority"), TextBox)
            '        For Each check As ProfileHeaderToGroup In arrVal
            '            If CType(lblID.Text, Integer) <> check.ID Then
            '                If CType(txtPriority.Text, Integer) = "" Then
            '                    MessageBox.Show("Priority harus diisi")
            '                    Return -1
            '                End If
            '            End If
            '        Next
            '    End If
            'Next

            ' check if any duplicate priority inside dtgProfileGroupDetail
            For Each item As DataGridItem In dtgProfileGroupDetail.Items
                Dim cbItem As CheckBox = CType(item.FindControl("cbItem"), CheckBox)
                If cbItem.Checked Then
                    Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                    Dim txtPriority As TextBox = CType(item.FindControl("txtPriority"), TextBox)
                    For Each check As ProfileHeaderToGroup In arrVal
                        If CType(lblID.Text, Integer) <> check.ID Then
                            If CType(txtPriority.Text, Integer) = check.Priority Then
                                MessageBox.Show("Priority tdk boleh duplikat")
                                Return -1
                            End If
                        End If
                    Next
                End If
            Next

            For Each item As DataGridItem In dtgProfileGroupDetail.Items
                Dim cbItem As CheckBox = CType(item.FindControl("cbItem"), CheckBox)
                If cbItem.Checked Then
                    Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                    If Not IsNothing(lblID) Then
                        Dim objProfileHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(CType(lblID.Text, Integer))
                        objAssignProfileToGroup = New ProfileHeaderToGroup
                        objAssignProfileToGroup.ProfileHeader = objProfileHeader
                        objAssignProfileToGroup.ProfileGroup = objProfileGroup

                        Dim txtPriority As TextBox = CType(item.FindControl("txtPriority"), TextBox)
                        If Not IsNothing(txtPriority) Then
                            objAssignProfileToGroup.Priority = CType(txtPriority.Text, Integer)
                        Else
                            objAssignProfileToGroup.Priority = 0
                        End If
                        listAssignProfileToGroup.Add(objAssignProfileToGroup)
                    End If
                End If
            Next

            'cek already assign profilegroup with profile2group that will be assigned
            For Each itemOri As ProfileHeaderToGroup In listAlreadyAssignProfileToGroup
                For Each itemChecked As ProfileHeaderToGroup In listAssignProfileToGroup
                    If itemChecked.Priority = itemOri.Priority Then
                        MessageBox.Show("Priority tdk boleh duplikat")
                        Return -1
                    End If
                Next
            Next

            Return New ProfileHeaderToGroupFacade(User).Insert(listAssignProfileToGroup)
        Else
            MessageBox.Show("Tidak ada data yang hendak disimpan")
            Return -1
        End If
    End Function

    Private Function InsertData(ByVal objProfileGroup As ProfileGroup) As Integer
        Dim listAlreadyAssignProfileToGroup As ArrayList = objProfileGroup.ProfileHeaderToGroups
        Dim objAssignProfileToGroup As ProfileHeaderToGroup
        Dim objGroupProfileFacade As ProfileHeaderToGroupFacade = New ProfileHeaderToGroupFacade(User)
        Dim listAssignProfileToGroup As ArrayList = New ArrayList

        'Dim objAssignProfileToGroup As ProfileHeaderToGroup
        'Dim objGroupProfileFacade As ProfileHeaderToGroupFacade = New ProfileHeaderToGroupFacade(User)

        'new profile header input
        ' check if any duplicate priority
        Dim arrVal As ArrayList = New ArrayList
        For Each val As DataGridItem In dtgProfileGroupDetail.Items
            Dim cbItem As CheckBox = CType(val.FindControl("cbItem"), CheckBox)
            If cbItem.Checked Then
                Dim lblID_Val As Label = CType(val.FindControl("lblID"), Label)
                Dim txtPriority_Val As TextBox = CType(val.FindControl("txtPriority"), TextBox)
                Dim objProfileHeaderToGroupTmp As ProfileHeaderToGroup = New ProfileHeaderToGroup
                'objProfileHeaderToGroupTmp.ID = CType(lblID_Val.Text, Integer)
                objProfileHeaderToGroupTmp.Priority = CType(txtPriority_Val.Text, Integer)
                arrVal.Add(objProfileHeaderToGroupTmp)
            End If
        Next

        If arrVal.Count > 0 Then
            For Each item As DataGridItem In dtgProfileGroupDetail.Items
                Dim cbItem As CheckBox = CType(item.FindControl("cbItem"), CheckBox)
                If cbItem.Checked Then
                    Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                    Dim txtPriority As TextBox = CType(item.FindControl("txtPriority"), TextBox)
                    For Each check As ProfileHeaderToGroup In arrVal
                        'If CType(lblID.Text, Integer) <> check.ID Then
                        If CType(txtPriority.Text, Integer) = check.Priority Then
                            MessageBox.Show("Priority tdk boleh duplikat")
                            Return -1
                        End If
                        'End If
                    Next
                End If
            Next

            For Each item As DataGridItem In dtgProfileGroupDetail.Items
                Dim cbItem As CheckBox = CType(item.FindControl("cbItem"), CheckBox)
                If cbItem.Checked Then
                    Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                    If Not IsNothing(lblID) Then
                        Dim objProfileHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(CType(lblID.Text, Integer))
                        objAssignProfileToGroup = New ProfileHeaderToGroup
                        objAssignProfileToGroup.ProfileHeader = objProfileHeader
                        objAssignProfileToGroup.ProfileGroup = objProfileGroup

                        Dim txtPriority As TextBox = CType(item.FindControl("txtPriority"), TextBox)
                        If Not IsNothing(txtPriority) Then
                            objAssignProfileToGroup.Priority = CType(txtPriority.Text, Integer)
                        Else
                            objAssignProfileToGroup.Priority = 0
                        End If
                        listAssignProfileToGroup.Add(objAssignProfileToGroup)
                    End If
                End If
            Next


        End If

        'chek new data with old data
        'cek the value first, if already exist, out, else add
        'cek the priority, if have same seq, then out
        Dim arlAddedProfileHeader2Group As New ArrayList
        If listAssignProfileToGroup.Count > 0 Then
            For Each item As ProfileHeaderToGroup In listAlreadyAssignProfileToGroup
                For Each item2 As ProfileHeaderToGroup In listAssignProfileToGroup
                    If item2.RowStatus <> 4 Then
                        If item2.ProfileHeader.Code = item.Profileheader.Code AndAlso item2.Priority <> item.Priority Then
                            MessageBox.Show("Kode: " & item2.ProfileHeader.Code & " sudah pernah di-assign")
                            listAssignProfileToGroup.Remove(item2)
                            Exit For
                        ElseIf item2.ProfileHeader.Code = item.Profileheader.Code AndAlso item2.Priority = item.Priority Then
                            MessageBox.Show("Kode: " & item2.ProfileHeader.Code & " dan priority: " & item2.Priority & " sudah pernah digunakan")
                            listAssignProfileToGroup.Remove(item2)
                            Exit For
                        ElseIf item2.ProfileHeader.Code <> item.Profileheader.Code AndAlso item2.Priority = item.Priority Then
                            MessageBox.Show("No Sequence sudah dipakai sebelumnya, gunakan no sequence yang lain")
                            Exit For
                        Else
                            arlAddedProfileHeader2Group.Add(item2)
                            item2.RowStatus = 4 'sudah ditambahkan, hanya sebagai flag saja
                        End If
                    End If
                Next
            Next
        End If


    End Function

    Private Function ListingCheckedData() As ArrayList
        Dim listCheckProfile As ArrayList = New ArrayList
        For Each item As DataGridItem In dtgProfileGroupDetail.Items
            If item.ItemType = ListItemType.Item Or item.ItemType = ListItemType.AlternatingItem Then
                Dim cbItem As CheckBox = CType(item.FindControl("cbItem"), CheckBox)
                If cbItem.Checked Then
                    'Dim txtPriorityNew As TextBox = CType(item.FindControl("txtPriority"), TextBox)


                    Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                    Dim id As Integer = CInt(lblID.Text)
                    Dim profHeader As ProfileHeader = New ProfileHeaderFacade(User).Retrieve(id)
                    listCheckProfile.Add(profHeader)
                End If
            End If
        Next
        Return listCheckProfile
    End Function
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' ActivateUserPrivilege()
        If Not IsPostBack Then
            ViewState("vsSortColumnAA") = "Priority"
            ViewState("vsSortDirectAA") = Sort.SortDirection.ASC
            InitiatePage()
            BindDataAlreadyAssign(0)
            'BindDataGrid()
            BindToGrid()
        End If
    End Sub


    Private Sub ActivateUserPrivilege()
        'If Not IsNothing(sessDealer) Then
        '    If sessDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '        If Not SecurityProvider.Authorize(Context.User, SR.AdminCreateNewRoleKTB_Privilege) Then
        '            Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Role Baru")
        '        End If
        '        Return
        '    ElseIf sessDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '        If Not SecurityProvider.Authorize(Context.User, SR.AdminCreateNewRoleDealer_Privilege) Then
        '            Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Role Baru")
        '        End If
        '        Return
        '    End If
        'End If
        'Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Role Baru")
    End Sub


    Private Sub SortListControl(ByRef pCompletelist As ArrayList, ByVal SortColumn As String, _
                              ByVal SortDirection As Integer)
        '-- Sort arraylist

        If SortColumn.Trim <> "" Then
            Dim isASC As Boolean = (SortDirection = Sort.SortDirection.ASC)  '-- Is sorted ascending?
            Dim objListComparer As IComparer = New ListComparer(isASC, SortColumn)
            pCompletelist.Sort(objListComparer)
        End If

    End Sub



    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Page.IsValid Then
            Return
        End If
        If isPageValid() Then
            Try
                Dim objGroup As ProfileGroup = New ProfileGroupFacade(User).Retrieve(lblKode.Text)
                If Not objGroup Is Nothing Then
                    Dim nID As Integer = InsertNewData(objGroup)
                    If nID <> -1 Then
                        'add by ery
                        BindDataAlreadyAssign(0)
                        BindToGrid()

                        MessageBox.Show("Simpan Data Berhasil")
                    ElseIf nID = 1 Then
                        MessageBox.Show("Simpan data gagal")
                    End If
                Else
                    MessageBox.Show("Data tidak valid")
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            MessageBox.Show("Data tidak valid")
        End If
    End Sub

    Private Function isPageValid() As Boolean
        Return True
    End Function


    Private Sub dtgProfileGroupDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgProfileGroupDetail.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgProfileGroupDetail.CurrentPageIndex * dtgProfileGroupDetail.PageSize)
            'Dim cbItem As CheckBox = e.Item.FindControl("cbItem")
            'Dim txtPriority As TextBox = e.Item.FindControl("txtPriority")
            'Dim idStr As String = CType(e.Item.FindControl("lblID"), Label).Text
            'Dim id As Integer = CInt(idStr)

            'Dim objListProfileByGroup As ArrayList = CType(ssHelper.GetSession("PROFILEBYGROUP"), ArrayList)

            'Dim arrList As ArrayList = CType(ssHelper.GetSession("sessProfileHeaderToGroup"), ArrayList)

            'cbItem.Checked = False
            'If Not IsNothing(objListProfileByGroup) Then
            '    If objListProfileByGroup.Contains(id) Then
            '        cbItem.Checked = True
            '        txtPriority.Text = GetPriorityValue(id)
            '    End If
            'End If
        End If
    End Sub



    Private Sub dtgProfileGroupDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgProfileGroupDetail.SortCommand
        If CType(ViewState("vsSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("vsSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("vsSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("vsSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("vsSortColumn") = e.SortExpression
            ViewState("vsSortDirect") = Sort.SortDirection.ASC
        End If
        dtgProfileGroupDetail.CurrentPageIndex = 0
        BindDataGrid()
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect("frmProfileGroup.aspx")
    End Sub

    Private Sub dtgAlreadyAssign_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgAlreadyAssign.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNoAA As Label = CType(e.Item.FindControl("lblNoAA"), Label)
            lblNoAA.Text = e.Item.ItemIndex + 1
        End If
    End Sub

    Private Sub dtgAlreadyAssign_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgAlreadyAssign.ItemCommand
        If e.CommandName = "Delete" Then
            Dim lblIDAA As Label = CType(e.Item.FindControl("lblIDAA"), Label)
            Dim objPH2Group As ProfileHeaderToGroup = New ProfileHeaderToGroupFacade(User).Retrieve(CInt(lblIDAA.Text))
            If (New ProfileHeaderToGroupFacade(User).DeleteDB(objPH2Group) <> -1) Then
                BindDataAlreadyAssign(dtgAlreadyAssign.CurrentPageIndex)
                BindToGrid()
            Else
                MessageBox.Show("Hapus data gagal!")
            End If
        End If
    End Sub

    Private Sub dtgAlreadyAssign_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgAlreadyAssign.PageIndexChanged
        dtgAlreadyAssign.CurrentPageIndex = e.NewPageIndex
        BindDataAlreadyAssign(dtgAlreadyAssign.CurrentPageIndex)
    End Sub

    Private Sub dtgAlreadyAssign_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgAlreadyAssign.SortCommand
        If CType(ViewState("vsSortColumnAA"), String) = e.SortExpression Then
            Select Case CType(ViewState("vsSortDirectAA"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("vsSortDirectAA") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("vsSortDirectAA") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("vsSortColumnAA") = e.SortExpression
            ViewState("vsSortDirectAA") = Sort.SortDirection.ASC
        End If
        BindDataAlreadyAssign(dtgAlreadyAssign.CurrentPageIndex)
    End Sub
End Class
