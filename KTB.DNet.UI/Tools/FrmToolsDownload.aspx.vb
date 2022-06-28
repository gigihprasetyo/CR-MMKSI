#Region "DNet Namespace Imports"
Imports KTB.DNet.BusinessFacade.Tools
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmToolsDownload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents txtSql As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblCondition As System.Web.UI.WebControls.Label
    Protected WithEvents ddlReport As System.Web.UI.WebControls.DropDownList
    Protected WithEvents pnlInfo As System.Web.UI.WebControls.Panel

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private oBCPQueryFacade As BCPQueryFacade = New BCPQueryFacade(User)
    Private sessHelper As New SessionHelper
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Authorization()
        If Not IsPostBack Then
            Initialization()
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        If ddlReport.SelectedIndex <> 0 Then
            If IsAuthorized() Then
                Dim objBPCQuery As BCPQuery = oBCPQueryFacade.Retrieve(CType(ddlReport.SelectedValue, Integer))
                Dim objReportList As ArrayList = oBCPQueryFacade.RetrieveFromSP(objBPCQuery.SQLBase, "", "", objBPCQuery.FlName)
                If objReportList.Count > 0 Then
                    Dim objReport As BCPQuery = CType(objReportList(0), BCPQuery)
                    If objReport.FlName <> String.Empty Then
                        DownloadFile(objReport.FlName)
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub ddlReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlReport.SelectedIndexChanged
        If ddlReport.SelectedIndex <> 0 Then
            Dim objBPCQuery As BCPQuery = oBCPQueryFacade.Retrieve(CType(ddlReport.SelectedValue, Integer))
            If objBPCQuery.SQLParam <> String.Empty Then
                pnlInfo.Visible = True
            Else
                pnlInfo.Visible = False
            End If
        Else
            pnlInfo.Visible = False
        End If
    End Sub

#Region "Custom"

    Private Sub Authorization()
        'If Not SecurityProvider.Authorize(Context.User, SR.Cc_akses_download_report_privilege) Then
        '    Response.Redirect("../frmAccessDenied.aspx?modulName=Customer Satisfaction")
        'End If
        'btnSave.Enabled = SecurityProvider.Authorize(Context.User, SR.cc_akses_download_report_sales_privilege) Or SecurityProvider.Authorize(Context.User, SR.cc_akses_download_report_aftersales_privilege)
    End Sub

    Private Function IsAuthorized() As Boolean
        Dim oLoginUser As UserInfo
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        'KTB or Dealer Report
        Dim objQuery As BCPQuery = New BCPQueryFacade(User).Retrieve(CInt(ddlReport.SelectedValue))
        If oLoginUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If objQuery.IsKTB = 0 Then
                MessageBox.Show("Anda tidak berhak untuk download report ini.")
                Return False
                Exit Function
            End If
        Else
            If objQuery.IsDealer = 0 Then
                MessageBox.Show("Anda tidak berhak untuk download report ini.")
                Return False
                Exit Function
            End If
        End If


        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserRole), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCol.opAnd(New Criteria(GetType(UserRole), "UserInfo.ID", MatchType.Exact, oLoginUser.ID))

        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(UserRole), "ID", Sort.SortDirection.ASC))
        Dim arlUserRole As ArrayList = New KTB.DNet.BusinessFacade.UserManagement.UserRoleFacade(User).Retrieve(critCol, sortCol)
        If arlUserRole.Count > 0 Then
            Dim strID As String = ""
            For Each item As UserRole In arlUserRole
                If strID <> "" Then
                    strID &= item.ID & "," & strID
                Else
                    strID &= item.ID
                End If
            Next
            If strID.Length > 0 Then

                Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BCPRoles), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crits.opAnd(New Criteria(GetType(BCPRoles), "UserRole.ID", MatchType.InSet, "(" & strID.ToString & ")"))
                crits.opAnd(New Criteria(GetType(BCPRoles), "BCPQuery.ID", MatchType.Exact, CInt(ddlReport.SelectedValue)))
                Dim arlBCPRoles As ArrayList = New KTB.DNet.BusinessFacade.Tools.BCPRolesFacade(User).Retrieve(crits)
                If arlBCPRoles.Count > 0 Then
                    Return True
                Else
                    MessageBox.Show("Anda tidak berhak untuk download report. Hubungi Adminsitrator")
                    Return False
                End If
            Else
                MessageBox.Show("Anda tidak berhak untuk download report. Hubungi Administrator")
                Return False
            End If
        Else
            MessageBox.Show("Anda tidak berhak untuk download report. Hubungi Administrator")
            Return False
        End If

    End Function

    Private Sub Initialization()
        GetReportList()
        pnlInfo.Visible = False
    End Sub

    Private Sub GetReportList()
        ddlReport.Items.Clear()
        ddlReport.Items.Clear()

        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BCPQuery), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(BCPQuery), "ID", Sort.SortDirection.ASC))
        Dim objReportList As ArrayList = New BCPQueryFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each objReport As BCPQuery In objReportList
            li = New ListItem(objReport.Title, objReport.ID.ToString)
            ddlReport.Items.Add(li)
        Next

        li = New ListItem("Silahkan pilih", "0")
        ddlReport.Items.Insert(0, li)

    End Sub

    Private Sub DownloadFile(ByVal fileName As String)

        Dim path As System.IO.Path
        Dim fullpath As String = ConfigurationSettings.AppSettings.Item("DL_Folder")

        fullpath = fullpath & fileName
        Dim name = path.GetFileName(fullpath)
        Dim ext = path.GetExtension(fullpath)
        Dim type As String = ""
        Dim _user As String = ConfigurationSettings.AppSettings.Get("DL_User")
        Dim _password As String = ConfigurationSettings.AppSettings.Get("DL_Password")
        Dim _webServer As String = ConfigurationSettings.AppSettings.Get("DL_Server")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        If imp.Start() Then
            Dim FileCheck As FileInfo = New FileInfo(fullpath)
            If FileCheck.Exists = True Then
                'Select Case ext
                '    'Case ".htm", ".html"
                '    '    type = "text/HTML"
                '    'Case ".txt"
                '    '    type = "text/plain"
                '    'Case ".doc", ".rtf"
                '    '    type = "Application/msword"
                'Case ".csv", ".xls"
                '        type = "Application/x-msexcel"
                '        'Case ".exe"
                '        'MessageBox.Show("File bermasalah tidak dapat di download")
                '        'Exit Sub
                '    Case Else
                '        type = "application/x-download"
                'End Select
                'Response.AppendHeader("content-disposition", "attachment; filename=" + name)
                'If type <> "" Then
                '    Response.ContentType = type
                'End If

                'Response.WriteFile(fullpath)
                'Response.End()
                Try
                    Response.Redirect("../Download.aspx?file=" & fullpath)
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(fileName.ToString))
                End Try
            Else
                MessageBox.Show("File tidak tersedia")
            End If
        Else
            MessageBox.Show("File tidak dapat diakses")
        End If
    End Sub

#End Region

End Class
