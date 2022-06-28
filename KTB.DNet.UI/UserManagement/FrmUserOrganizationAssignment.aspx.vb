#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility

#End Region


Public Class FrmUserOrganizationAssignment
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblID As System.Web.UI.WebControls.Label
    Protected WithEvents lblLogin As System.Web.UI.WebControls.Label
    Protected WithEvents lblName1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblName2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPosition As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents txtSearchDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgUserOrgAssgnment As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private sesHelper As SessionHelper = New SessionHelper
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            ViewState("CurrentSortTable") = GetType(UserOrgAssignment)
            ViewState("CurrentSortColumn") = ""
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

            ViewState("FindIDVehicleCode") = "-1"
            ViewState("FindLaborCode") = ""
            ViewState("FindWorkCode") = ""

            If BindHeader() Then
                BindDG(0)
                ViewState.Add("vsProcess", "Insert")
                Me.lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection('" & CStr(ViewState("DealerGroupID")) & "','" & CType(GetFromSession("DEALER"), Dealer).DealerCode & "');"
            End If
        End If
    End Sub

    Private Function BindHeader() As Boolean
        Dim objUserInfo As UserInfo
        Dim objUserInfoFac As UserInfoFacade = New UserInfoFacade(User)
        objUserInfo = objUserInfoFac.Retrieve(CInt(Request.QueryString("ID")))
        If Not objUserInfo Is Nothing Then
            Me.lblDealerCode.Text = objUserInfo.Dealer.DealerCode
            Me.lblDealerName.Text = objUserInfo.Dealer.DealerName
            Me.lblID.Text = objUserInfo.ID
            Me.lblLogin.Text = objUserInfo.UserName
            Me.lblName1.Text = objUserInfo.FirstName
            Me.lblName2.Text = objUserInfo.LastName

            'Me.lblPosition.Text = objUserInfo.JobPositionOld
            If Not objUserInfo.JobPosition Is Nothing Then
                Me.lblPosition.Text = objUserInfo.JobPosition.Description
            End If

            Dim objDealerGroup As DealerGroup
            objDealerGroup = objUserInfo.Dealer.DealerGroup
            If objDealerGroup Is Nothing Then
                ViewState("DealerGroupID") = -1
                Me.btnSave.Enabled = False
                Me.btnBatal.Enabled = False
                Me.lblSearchDealer.Enabled = False
                Me.txtSearchDealer.Enabled = False
                Me.lblSearchDealer.Attributes.Add("enabled", "0")
            Else
                ViewState("DealerGroupID") = objUserInfo.Dealer.DealerGroup.ID
                Me.btnSave.Enabled = True
                Me.btnBatal.Enabled = True
                Me.lblSearchDealer.Enabled = True
                Me.txtSearchDealer.Enabled = True
                Me.lblSearchDealer.Attributes.Add("enabled", "1")
            End If
            Return True
        Else
            Me.txtSearchDealer.Visible = False
            Me.lblSearchDealer.Visible = False
            Me.btnBatal.Enabled = False
            Me.btnSave.Enabled = False
            MessageBox.Show(SR.ViewFail)
            Return False
        End If
    End Function

    Private Function CreateSearchCriteria() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(UserOrgAssignment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(UserOrgAssignment), "UserInfo.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(UserOrgAssignment), "Dealer.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(UserOrgAssignment), "UserInfo.ID", MatchType.Exact, CInt(Me.lblID.Text)))

        Return criterias
    End Function

    Private Sub BindDG(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgUserOrgAssgnment.DataSource = New UserOrgAssignmentFacade(User).RetrieveActiveList(CreateSearchCriteria(), _
                indexPage + 1, Me.dtgUserOrgAssgnment.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortTable"), System.Type), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgUserOrgAssgnment.VirtualItemCount = totalRow
            dtgUserOrgAssgnment.DataBind()
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Me.lblSearchDealer.Visible = True
        Me.txtSearchDealer.ReadOnly = False
        txtSearchDealer.Text = String.Empty
        btnSave.Enabled = True

        ViewState.Add("vsProcess", "Insert")
        BindDG(dtgUserOrgAssgnment.CurrentPageIndex)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not Page.IsValid Then
            MessageBox.Show("Dealer tidak boleh kosong")
            Exit Sub
        End If
        Dim msg As String = ""
        Me.lblSearchDealer.Enabled = True
        Me.txtSearchDealer.Enabled = True

        If msg = "" Then
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                msg = InsertUserOrgAssign()
            Else
                msg = UpdateUserOrgAssign()
            End If
        End If
        MessageBox.Show(msg)
    End Sub

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Private Function UpdateUserOrgAssign() As String
        Dim objUserOrgAssgnFac As UserOrgAssignmentFacade = New UserOrgAssignmentFacade(User)
        Dim objUserOrgAssgn As UserOrgAssignment = CType(GetFromSession("vsUserOrgAssgn"), UserOrgAssignment)
        Dim ListUserOrgAssgn As ArrayList
        Dim nResult As Integer
        Dim Msg As String = ""

        If Me.txtSearchDealer.Text.Trim <> Me.lblDealerCode.Text Then
            Dim objDealerFacade As DealerFacade = New DealerFacade(User)
            Dim objDealer As Dealer
            objDealer = objDealerFacade.Retrieve(Me.txtSearchDealer.Text.Trim, CInt(ViewState("DealerGroupID")))
            If (Not objDealer Is Nothing) AndAlso (objDealer.ID <> 0) Then
                If Not IsExist(objDealer) Then
                    objUserOrgAssgn.Dealer = objDealer
                    If objUserOrgAssgnFac.Update(objUserOrgAssgn) > 0 Then
                        Msg = SR.UpdateSucces
                    Else
                        Msg = SR.UpdateFail
                    End If
                Else
                    Msg = "Kode Dealer [" & Me.txtSearchDealer.Text.Trim & "] sudah terdaftar"
                End If
            Else
                Msg = "Kode Dealer [" & Me.txtSearchDealer.Text.Trim & "] tidak ditemukan"
            End If
        Else
            Msg = "Kode dealer [" & Me.txtSearchDealer.Text.Trim & "] sama dengan kode dealer user"
        End If

        Me.txtSearchDealer.Text = ""
        Me.dtgUserOrgAssgnment.CurrentPageIndex = 0
        BindDG(Me.dtgUserOrgAssgnment.CurrentPageIndex)
        dtgUserOrgAssgnment.SelectedIndex = -1
        Return Msg
    End Function

    Private Function InsertUserOrgAssign() As String
        Dim objUserOrgAssgnFac As UserOrgAssignmentFacade = New UserOrgAssignmentFacade(User)
        Dim ListUserOrgAssgn As ArrayList = New ArrayList
        Dim nResult As Integer
        Dim Msg As String = ""
        Dim FailMsg As String = "Kode dealer berikut TIDAK BERHASIL disimpan :\n"
        Dim IsAnyFailedUserOrgAssign As Boolean = False

        If Me.txtSearchDealer.Text.Trim <> "" Then
            Dim listDealer() As String
            Dim objDealerFacade As DealerFacade = New DealerFacade(User)
            listDealer = Me.txtSearchDealer.Text.Trim.Split(";")

            For Each dealerCode As String In listDealer
                If dealerCode = Me.lblDealerCode.Text Then
                    IsAnyFailedUserOrgAssign = True
                    FailMsg = FailMsg & "- Kode dealer [" & dealerCode & "] sama dengan kode dealer user\n"
                Else
                    Dim objDealer As Dealer
                    objDealer = objDealerFacade.Retrieve(dealerCode, CInt(ViewState("DealerGroupID")))
                    If (Not objDealer Is Nothing) AndAlso (objDealer.ID <> 0) Then
                        If Not IsExist(objDealer) Then
                            Dim objUserOrgAssgn As UserOrgAssignment = New UserOrgAssignment
                            Dim objUserInfo As UserInfo
                            objUserInfo = New UserInfoFacade(User).Retrieve(CInt(Me.lblID.Text))
                            If Not IsNothing(objUserInfo) Then
                                objUserOrgAssgn.UserInfo = objUserInfo
                                objUserOrgAssgn.Dealer = objDealer
                                ListUserOrgAssgn.Add(objUserOrgAssgn)
                            Else
                                Msg = "User tidak ditemukan \n"
                                Return Msg
                            End If
                        Else
                            IsAnyFailedUserOrgAssign = True
                            FailMsg = FailMsg & "- Kode Dealer [" & dealerCode & "] sudah terdaftar\n"
                        End If
                    Else
                        IsAnyFailedUserOrgAssign = True
                        FailMsg = FailMsg & "- Kode Dealer [" & dealerCode & "] tidak ditemukan\n"
                    End If
                End If

            Next
        End If

        If ListUserOrgAssgn.Count > 0 Then
            Try
                objUserOrgAssgnFac.Insert(ListUserOrgAssgn)
                If IsAnyFailedUserOrgAssign Then
                    Msg = ConstructSaveSuccessMsg(ListUserOrgAssgn) & "\n\n" & FailMsg
                Else
                    Msg = ConstructSaveSuccessMsg(ListUserOrgAssgn)
                End If
                Me.txtSearchDealer.Text = ""
                ViewState("CurrentSortTable") = GetType(UserOrgAssignment)
                ViewState("CurrentSortColumn") = "ID"
                ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
                Me.dtgUserOrgAssgnment.CurrentPageIndex = 0
                BindDG(Me.dtgUserOrgAssgnment.CurrentPageIndex)
                dtgUserOrgAssgnment.SelectedIndex = -1
            Catch ex As Exception
                Msg = SR.SaveFail
            End Try
        Else
            If IsAnyFailedUserOrgAssign Then
                Msg = FailMsg
            End If
        End If
        Return Msg
    End Function

    Private Function ConstructSaveSuccessMsg(ByVal list As ArrayList) As String
        Dim msg As String = "Kode dealer berikut BERHASIL disimpan :\n"
        For Each item As UserOrgAssignment In list
            msg = msg & "- Kode Dealer [" & item.Dealer.DealerCode & "] berhasil disimpan\n"
        Next
        Return msg
    End Function

    Private Function IsExist(ByVal objDealer As Dealer) As Boolean
        Dim objUserOrgAssgnFac As UserOrgAssignmentFacade = New UserOrgAssignmentFacade(User)
        Return objUserOrgAssgnFac.IsExist(CInt(Me.lblID.Text), objDealer)
    End Function

    Private Sub dtgUserOrgAssgnment_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgUserOrgAssgnment.ItemCommand
        Select Case e.CommandName
            Case "View"
                ViewState.Add("vsProcess", "View")
                If ViewLabor(e.Item.Cells(4).Text, False) Then
                    Me.txtSearchDealer.ReadOnly = True
                    Me.lblSearchDealer.Visible = False
                    dtgUserOrgAssgnment.SelectedIndex = e.Item.ItemIndex
                Else
                    MessageBox.Show(SR.ViewFail)
                End If
            Case "Edit"
                ViewState.Add("vsProcess", "Edit")
                If ViewLabor(e.Item.Cells(4).Text, True) Then
                    Me.txtSearchDealer.ReadOnly = False
                    Me.lblSearchDealer.Visible = True
                    dtgUserOrgAssgnment.SelectedIndex = e.Item.ItemIndex
                Else
                    MessageBox.Show(SR.ViewFail)
                End If
            Case "Delete"
                If DeleteUserOrgAssgn(e.Item.Cells(4).Text) Then
                    txtSearchDealer.Text = String.Empty
                    btnSave.Enabled = True
                    ViewState.Add("vsProcess", "Insert")
                    MessageBox.Show(SR.DeleteSucces)
                Else
                    MessageBox.Show(SR.DeleteFail)
                End If
        End Select
    End Sub

    Private Function DeleteUserOrgAssgn(ByVal nID As Integer) As Boolean
        Dim ObjUserOrgAssignmentFacade As UserOrgAssignmentFacade = New UserOrgAssignmentFacade(User)
        Dim objUserOrgAssgn As UserOrgAssignment = ObjUserOrgAssignmentFacade.Retrieve(nID)
        If Not IsNothing(objUserOrgAssgn) Then
            Try
                ObjUserOrgAssignmentFacade.DeleteFromDB(objUserOrgAssgn)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
                Return False
            End Try
            Me.dtgUserOrgAssgnment.CurrentPageIndex = 0
            BindDG(Me.dtgUserOrgAssgnment.CurrentPageIndex)
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ViewLabor(ByVal nID As Integer, ByVal EditStatus As Boolean) As Boolean
        BindDG(dtgUserOrgAssgnment.CurrentPageIndex)
        Dim objUserOrgAssgn As UserOrgAssignment = New UserOrgAssignmentFacade(User).Retrieve(nID)
        If Not IsNothing(objUserOrgAssgn) Then
            sesHelper.SetSession("vsUserOrgAssgn", objUserOrgAssgn)
            txtSearchDealer.Text = objUserOrgAssgn.Dealer.DealerCode
            Me.btnSave.Enabled = EditStatus
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ViewState("CurrentSortTable") = GetType(UserOrgAssignment)
        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        Me.dtgUserOrgAssgnment.CurrentPageIndex = 0
        BindDG(dtgUserOrgAssgnment.CurrentPageIndex)
    End Sub

    Private Sub dtgUserOrgAssgnment_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgUserOrgAssgnment.PageIndexChanged
        Me.dtgUserOrgAssgnment.CurrentPageIndex = e.NewPageIndex
        BindDG(Me.dtgUserOrgAssgnment.CurrentPageIndex)
    End Sub

    Private Sub dtgUserOrgAssgnment_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgUserOrgAssgnment.SortCommand
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
            Select Case CType(ViewState("CurrentSortColumn"), String)
                Case "VechileTypeCode"
                    ViewState("CurrentSortTable") = GetType(VechileType)
                Case "LaborCode", "WorkCode"
                    ViewState("CurrentSortTable") = GetType(LaborMaster)
            End Select
        End If

        Me.dtgUserOrgAssgnment.CurrentPageIndex = 0
        BindDG(Me.dtgUserOrgAssgnment.CurrentPageIndex)
    End Sub

    Private Sub dtgUserOrgAssgnment_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUserOrgAssgnment.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim obj As UserOrgAssignment = CType(e.Item.DataItem, UserOrgAssignment)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = 1 + e.Item.ItemIndex + (Me.dtgUserOrgAssgnment.PageSize * Me.dtgUserOrgAssgnment.CurrentPageIndex)
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            End If
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmUserList.aspx?Reread=True")
    End Sub
End Class
