Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

Public Class FrmJaminanList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblColon4 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtBerlakuPada As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgSPLHeader As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusJaminan As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


#Region "Private Variables"
    Private sHelper As SessionHelper = New SessionHelper
    Private arlJ As ArrayList
    Private oJFac As JaminanFacade = New JaminanFacade(User)

#End Region

#Region "Custom Methods"

    Private Sub Initialization()
        viewstate.Add("OrderField", "ID")
        viewstate.Add("OrderDirection", Sort.SortDirection.ASC)
        sHelper.SetSession("FrmJaminanList.arlJaminan", New ArrayList)
        BindDdlStatus()
    End Sub

    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = EnumStatusSPL.RetrieveStatus
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataBind()
    End Sub

    Private Sub BindDtg(ByVal PageIndex As Integer)
        Dim TotRow As Integer = 0
        arlJ = oJFac.RetrieveActiveList(GetCriterias, PageIndex, dgSPLHeader.PageSize, TotRow, viewstate.Item("OrderField"), viewstate.Item("OrderDirection"))
        sHelper.SetSession("FrmJaminanList.arlJaminan", arlJ)
        dgSPLHeader.CurrentPageIndex = PageIndex
        dgSPLHeader.DataSource = arlJ
        dgSPLHeader.VirtualItemCount = TotRow
        dgSPLHeader.DataBind()
    End Sub

    Private Function GetCriterias() As CriteriaComposite
        Dim crtJ As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Jaminan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtDealerName.Text.Trim <> "" Then
            Dim sDealers = txtDealerName.Text.Trim.Replace(";", ",")
            crtJ.opAnd(New Criteria(GetType(Jaminan), "DealerCode", MatchType.[Partial], sDealers))
        End If
        If txtBerlakuPada.Text.Trim <> "" Then
            If txtBerlakuPada.Text.Trim.Length = 6 Then
                Dim str As String = txtBerlakuPada.Text.Trim
                Try
                    Dim dt As Date = DateSerial(str.Substring(2, 4), str.Substring(0, 2), 1)
                    crtJ.opAnd(New Criteria(GetType(Jaminan), "ValidFrom", MatchType.LesserOrEqual, dt))
                    crtJ.opAnd(New Criteria(GetType(Jaminan), "ValidFrom", MatchType.GreaterOrEqual, dt))
                Catch ex As Exception
                    MessageBox.Show("Format Periode salah")
                    Exit Function
                End Try
            End If
        End If
        If ddlStatus.SelectedValue <> -1 Then
            crtJ.opAnd(New Criteria(GetType(Jaminan), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Integer)))
        End If
        Return crtJ
    End Function

    Private Function DeleteJaminan(ByVal oJ As Jaminan) As Boolean
        Return IIf(oJFac.Delete(oJ) = -1, False, True)
    End Function

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.jaminan_lihat_display_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Jaminan")
        End If
    End Sub
#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        If Not IsPostBack Then
            Initialization()
            BindDtg(0)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDtg(0)
    End Sub

    Private Sub dgSPLHeader_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSPLHeader.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim lblPeriode As Label = e.Item.FindControl("lblPeriode")
            Dim lbtnDownload As LinkButton = e.Item.FindControl("lbtnDownload")
            Dim oJ As Jaminan = CType(sHelper.GetSession("FrmJaminanList.arlJaminan"), ArrayList)(e.Item.ItemIndex)
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")
            Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
            
            lblNo.Text = dgSPLHeader.CurrentPageIndex * dgSPLHeader.PageSize + e.Item.ItemIndex + 1
            lblPeriode.Text = Format(oJ.ValidFrom, "MMyyyy") & "-" & Format(oJ.ValidTo, "MMyyyy")
            If oJ.Attachment.Trim.Length > 4 Then
                lbtnDownload.CommandArgument = oJ.Attachment
                lbtnDownload.Visible = True
            Else
                lbtnDownload.CommandArgument = ""
                lbtnDownload.Visible = False
            End If
            If SecurityProvider.Authorize(Context.User, SR.jaminan_edit_privilege) Then
                lbtnEdit.Visible = True
                lbtnDelete.Visible = True
            Else
                lbtnEdit.Visible = False
                lbtnDelete.Visible = False
            End If

            If SecurityProvider.Authorize(Context.User, SR.jaminan_download_privilege) Then
                lbtnDownload.Visible = True
            Else
                lbtnDownload.Visible = False
            End If
        End If
    End Sub

    Private Sub dgSPLHeader_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSPLHeader.ItemCommand
        Dim oJ As Jaminan = CType(sHelper.GetSession("FrmJaminanList.arlJaminan"), ArrayList)(e.Item.ItemIndex)
        Select Case e.CommandName.ToUpper
            Case "View".ToUpper
                Response.Redirect("FrmJaminan.aspx?Mode=View&ID=" & oJ.ID)
            Case "Edit".ToUpper
                Response.Redirect("FrmJaminan.aspx?Mode=Edit&ID=" & oJ.ID)
            Case "Delete".ToUpper
                If DeleteJaminan(oJ) Then
                    BindDtg(dgSPLHeader.CurrentPageIndex)
                Else
                    MessageBox.Show("Data gagal dihapus")
                End If
            Case "Download".ToUpper
                Dim file As String = e.CommandArgument
                Dim fInfo As New System.IO.FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & file)
                Try
                    Response.Redirect("../Download.aspx?file=" & file)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(file))
                End Try
        End Select
    End Sub

    Private Sub dgSPLHeader_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSPLHeader.SortCommand
        If CType(viewstate.Item("OrderField"), String).Trim.ToUpper = e.SortExpression.Trim.ToUpper Then
            If viewstate.Item("OrderDirection") = Sort.SortDirection.ASC Then
                viewstate.Item("OrderDirection") = Sort.SortDirection.DESC
            Else
                viewstate.Item("OrderDirection") = Sort.SortDirection.ASC
            End If
        Else
            viewstate.Item("OrderField") = e.SortExpression
            viewstate.Item("OrderDirection") = Sort.SortDirection.ASC
        End If
        BindDtg(dgSPLHeader.CurrentPageIndex)
    End Sub

    Private Sub dgSPLHeader_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSPLHeader.PageIndexChanged
        BindDtg(e.NewPageIndex)
    End Sub

#End Region
End Class
