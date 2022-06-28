Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports System.IO
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.Claim
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security

Public Class PopUpClaimStatusChanges
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgEntryClaimEdit As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents ltrClaimNo As System.Web.UI.WebControls.Literal
    Protected WithEvents ltrDealerCode As System.Web.UI.WebControls.Literal

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Dim criterias As CriteriaComposite
    Dim sHelper As New SessionHelper
    Dim arrList As ArrayList = New ArrayList
#End Region

#Region "Custom Method"

    Private Sub CreateCriteria()
        criterias = New CriteriaComposite(New Criteria(GetType(ClaimStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ClaimStatusHistory), "ClaimHeader.ID", MatchType.Exact, CInt(viewstate("ClaimHeaderID"))))
    End Sub

    Private Sub BindToInfo()
        Dim CHID As Integer = CInt(viewstate("ClaimHeaderID"))
        Dim objClaimHeader As ClaimHeader = New ClaimHeaderFacade(User).Retrieve(CHID)
        ltrClaimNo.Text = objClaimHeader.ClaimNo
        ltrDealerCode.Text = objClaimHeader.Dealer.DealerCode
    End Sub

#End Region

    Private Function CekPrivLihatHistory() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.HistoryStatusView_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "CreatedTime"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
            Dim claimHeaderID As Integer = CInt(Request.QueryString("ClaimHeaderID"))
            Dim statusKTB As Integer = CInt(Request.QueryString("StatusKTB"))
            viewstate.Add("ClaimHeaderID", claimHeaderID)
            viewstate.Add("StatusKTB", statusKTB)
            viewstate.Add("SortCol", "")
            BindTogrid(0)
            BindToInfo()
        End If
    End Sub

    Private Sub BindTogrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(ClaimStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ClaimStatusHistory), "ClaimHeader.ID", MatchType.Exact, CInt(Request.QueryString("ClaimHeaderID"))))
        ' criterias.opAnd(New Criteria(GetType(ClaimStatusHistory), "ClaimHeader.StatusKTB", MatchType.Exact, CInt(Request.QueryString("StatusKTB"))))

        arrList = New ClaimStatusHistoryFacade(User).RetrieveActiveList(criterias, idxPage + 1, dtgEntryClaimEdit.PageSize, totalRow, ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))

        dtgEntryClaimEdit.CurrentPageIndex = idxPage
        dtgEntryClaimEdit.DataSource = arrList
        dtgEntryClaimEdit.VirtualItemCount = totalRow
        dtgEntryClaimEdit.DataBind()


    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Write("<script language='javascript'>window.close();</script>")
    End Sub

    Private Sub dtgEntryClaimEdit_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEntryClaimEdit.ItemDataBound
        If e.Item.ItemIndex <> -1 Then

            'set nomor
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1

            Dim lblOldStatus As Label = CType(e.Item.FindControl("lblOldStatus"), Label)
            Dim lblOldProgress As Label = CType(e.Item.FindControl("lblOldProgress"), Label)
            Dim lblNewStatus As Label = CType(e.Item.FindControl("lblNewStatus"), Label)
            Dim lblNewProgress As Label = CType(e.Item.FindControl("lblNewProgress"), Label)
            Dim lblProcessBy As Label = CType(e.Item.FindControl("lblProcessBy"), Label)

            Dim objhistory As ClaimStatusHistory = arrList(e.Item.ItemIndex)

            'get the new status and progress
            'get the new Status
            Select Case objhistory.NewStatus
                Case EnumClaimProgress.ClaimProgressKTB.Baru
                    lblNewStatus.Text = EnumClaimProgress.ClaimProgressKTB.Baru.ToString
                Case EnumClaimProgress.ClaimProgressKTB.Diproses
                    lblNewStatus.Text = EnumClaimProgress.ClaimProgressKTB.Diproses.ToString
                Case EnumClaimProgress.ClaimProgressKTB.Selesai
                    lblNewStatus.Text = EnumClaimProgress.ClaimProgressKTB.Selesai.ToString
            End Select

            'get the new Progress
            Dim newProgressID As Integer = CInt(objhistory.Progress)
            Dim objClaimProgress As ClaimProgress = New ClaimProgressFacade(User).Retrieve(newProgressID)
            If objClaimProgress Is Nothing Then
                lblNewProgress.Text = ""
            Else
                lblNewProgress.Text = objClaimProgress.Progress
            End If

            'get the oldstatus and old progress
            Select Case objhistory.Status
                Case EnumClaimProgress.ClaimProgressKTB.Baru
                    lblOldStatus.Text = EnumClaimProgress.ClaimProgressKTB.Baru.ToString
                Case EnumClaimProgress.ClaimProgressKTB.Diproses
                    lblOldStatus.Text = EnumClaimProgress.ClaimProgressKTB.Diproses.ToString
                Case EnumClaimProgress.ClaimProgressKTB.Selesai
                    lblOldStatus.Text = EnumClaimProgress.ClaimProgressKTB.Selesai.ToString
            End Select

            'get the old progress
            Dim oldProgID As Integer = objhistory.OldProgress
            Dim objOldClaimProgress As ClaimProgress = New ClaimProgressFacade(User).Retrieve(oldProgID)
            If objOldClaimProgress Is Nothing Then
                lblOldProgress.Text = ""
            Else
                lblOldProgress.Text = objOldClaimProgress.Progress
            End If


            'Dim dealerID As Integer = CInt(lblProcessBy.Text.Substring(0, 6))
            'Dim userID As String = lblProcessBy.Text.Substring(7)
            Dim objUserInfo As UserInfo
            Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(UserInfo), "UserName", MatchType.Exact, lblProcessBy.Text))

            Dim arlUser As ArrayList = New UserInfoFacade(User).Retrieve(criteria)
            If arlUser.Count > 0 Then
                objUserInfo = New UserInfoFacade(User).Retrieve(criteria)(0)

                Dim processConstruct = objUserInfo.Dealer.DealerCode & "-" & objUserInfo.UserName
                lblProcessBy.Text = processConstruct

            End If

            If Not CekPrivLihatHistory() Then
                dtgEntryClaimEdit.Columns(8).Visible = False
            End If
        End If
    End Sub

    Private Sub dtgEntryClaimEdit_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEntryClaimEdit.EditCommand
        Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
        Dim idx As Integer = e.Item.ItemIndex
        dtgEntryClaimEdit.EditItemIndex = idx
        dtgEntryClaimEdit.ShowFooter = False
        BindTogrid(0)
    End Sub

    Private Sub dtgEntryClaimEdit_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEntryClaimEdit.CancelCommand
        dtgEntryClaimEdit.EditItemIndex = -1
        dtgEntryClaimEdit.ShowFooter = True
        BindTogrid(0)
    End Sub

    Private Sub dtgEntryClaimEdit_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEntryClaimEdit.UpdateCommand
        Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
        Dim lblOldProgress As Label = CType(e.Item.FindControl("lblOldProgress"), Label)
        Dim lblNewProgress As Label = CType(e.Item.FindControl("lblNewProgress"), Label)
        Dim objClaimStatusHistory As New ClaimStatusHistory
        Dim CHID As Integer = CInt(viewstate("ClaimHeaderID"))
        Dim objClaimHeader As ClaimHeader = New ClaimHeaderFacade(User).Retrieve(CHID)

        'objClaimStatusHistory.ID = CInt(lblID.Text)
        objClaimStatusHistory = New ClaimStatusHistoryFacade(User).Retrieve(CInt(lblID.Text))
        objClaimStatusHistory.Keterangan = CType(e.Item.FindControl("txtKeterangan"), TextBox).Text
        'objClaimStatusHistory.ClaimHeader = objClaimHeader
        'objClaimStatusHistory.OldProgress = lblOldProgress.Text
        'objClaimStatusHistory.NewProgress = lblNewProgress.Text

        Dim iresult As Integer = New ClaimStatusHistoryFacade(User).Update(objClaimStatusHistory)
        If iresult <> -1 Then
            MessageBox.Show("Update berhasil")
            dtgEntryClaimEdit.EditItemIndex = -1
            dtgEntryClaimEdit.ShowFooter = True
            BindTogrid(0)
        Else
            MessageBox.Show("Gagal melakukan update")
        End If
    End Sub
End Class
