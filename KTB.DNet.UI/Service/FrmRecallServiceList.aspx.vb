#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Service
Imports System.IO
Imports System.Text

#End Region


Public Class FrmRecallServiceList
    Inherits System.Web.UI.Page

#Region "Variable"
    Private _sessHelper As SessionHelper = New SessionHelper
    Private bPrivilegeChangesCity As Boolean
    Private arlRecallCategory As ArrayList
    Private sessCriterias As String = "FrmRecallServiceList.sessCriterias"
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
            txtDealerCode.Text = oD.DealerCode
            txtDealerCode.ReadOnly = True
            Dim dtgLast As Integer = dtgRecallService.Columns.Count - 1
            dtgRecallService.Columns(dtgLast).Visible = False
        Else

        End If


    End Sub

    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)

        If txtDealerCode.Text.Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(RecallService), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        End If
        If txtRecallRegNo.Text().Trim().Length > 0 Then
            criterias.opAnd(New Criteria(GetType(RecallService), "RecallChassisMaster.RecallCategory.RecallRegNo", MatchType.InSet, "('" & txtRecallRegNo.Text.Replace(";", "','") & "')"))
        End If

        If chkServiceDate.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "ServiceDate", MatchType.GreaterOrEqual, icStartDate.Value))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "ServiceDate", MatchType.LesserOrEqual, icEndDate.Value.AddDays(1)))
        End If


        If chkChassisBB.Checked Then
            If txtNorangka.Text.Trim().Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "ChassisMasterBB", MatchType.InSet, "(select id from chassismasterbb where chassisnumber in ( '" & txtNorangka.Text.Replace(";", "','") & "'))"))
            Else
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "ChassisMasterBB", MatchType.InSet, "(select id from chassismasterbb) "))
            End If
        Else
            If txtNorangka.Text.Trim().Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "ChassisMaster", MatchType.InSet, "(select id from chassismaster  where chassisnumber in ( '" & txtNorangka.Text.Replace(";", "','") & "'))"))
            Else
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "ChassisMaster", MatchType.InSet, "(select id from chassismaster )"))
            End If
        End If


        If chkInputDate.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "CreatedTime", MatchType.GreaterOrEqual, ccStart.Value))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.RecallService), "CreatedTime", MatchType.LesserOrEqual, ccEnd.Value.AddDays(1)))
        End If

    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim TotalRow As Integer = 0
        If indexPage >= 0 Then
            arlRecallCategory = New RecallServiceFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession(Me.sessCriterias), CriteriaComposite), indexPage + 1, dtgRecallService.PageSize, TotalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgRecallService.DataSource = arlRecallCategory
            dtgRecallService.VirtualItemCount = TotalRow
            dtgRecallService.DataBind()

            If dtgRecallService.Items.Count > 0 Then
                btnDownload.Visible = True
            Else
                btnDownload.Visible = False
            End If
        End If
    End Sub

    Private Sub ViewRecallCategory(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objRecallCategory As RecallCategory = New RecallCategoryFacade(User).Retrieve(nID)
        If Not objRecallCategory Is Nothing Then

        Else
            MessageBox.Show(SR.ViewFail)
        End If
    End Sub

    Private Sub DeleteRecallCategory(ByVal nID As Integer)
        Try
            Dim objRecallCategory As RecallService = New RecallServiceFacade(User).Retrieve(nID)
            'objCity.RowStatus = DBRowStatus.Deleted
            If Not objRecallCategory Is Nothing Then

                
                Dim objRecallCategoryFacade As RecallServiceFacade = New RecallServiceFacade(User)
                objRecallCategoryFacade.Delete(objRecallCategory)
                ClearData()
                dtgRecallService.CurrentPageIndex = 0
                BindDatagrid(dtgRecallService.CurrentPageIndex)
            Else
                MessageBox.Show(SR.DeleteFail)
            End If


        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try



    End Sub

    Private Function UpdateRecallCategory() As Integer

    End Function

    Private Sub ClearData()
        dtgRecallService.SelectedIndex = -1


    End Sub

    Private Sub DownloadRecall(ByRef sw As StreamWriter)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim RecallLine As StringBuilder = New StringBuilder
        Dim DataList As ArrayList = New ArrayList()
        DataList = New RecallServiceFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession(Me.sessCriterias), CriteriaComposite), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If DataList Is Nothing Then
            DataList = New ArrayList
        End If
        RecallLine.Remove(0, RecallLine.Length)  '-- Empty  line
        RecallLine.Append("Kode Dealer" & tab)
        RecallLine.Append("Nama Dealer" & tab)
        RecallLine.Append("No Rangka" & tab)
        RecallLine.Append("Field Fix Reg No" & tab)
        RecallLine.Append("Deskripsi" & tab)
        RecallLine.Append("Jarak Tempuh (KM)" & tab)
        RecallLine.Append("Tanggal service" & tab)
        sw.WriteLine(RecallLine.ToString())  '-- Write  line
        For Each objHeader As RecallService In DataList
            RecallLine.Remove(0, RecallLine.Length)  '-- Empty  line
            RecallLine.Append(objHeader.Dealer.DealerCode & tab)
            RecallLine.Append(objHeader.Dealer.DealerName.Replace(vbTab, " ") & tab)
            If chkChassisBB.Checked = False Then
                RecallLine.Append(objHeader.ChassisMaster.ChassisNumber & tab)
            Else
                RecallLine.Append(objHeader.ChassisMasterBB.ChassisNumber & tab)
            End If
            RecallLine.Append(objHeader.RecallChassisMaster.RecallCategory.RecallRegNo & tab)
            RecallLine.Append(objHeader.RecallChassisMaster.RecallCategory.Description & tab)
            RecallLine.Append(Format(objHeader.MileAge, "#,##0") & tab)
            RecallLine.Append(Format(objHeader.ServiceDate, "dd/MM/yyyy") & tab)
            sw.WriteLine(RecallLine.ToString())  '-- Write  line
        Next

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()

        If Not IsPostBack Then
            ClearData()
            ViewState("CurrentSortColumn") = "RecallRegNo"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblRecallRegNo.Attributes("onclick") = "ShowPPRecallCategorySelection();"
            lblNoRangka.Attributes("onclick") = "ShowPPNoRangkaSelection();"
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.RecallService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        CreateCriteria(criterias)
        _sessHelper.SetSession(Me.sessCriterias, criterias)
        dtgRecallService.CurrentPageIndex = 0
        BindDatagrid(dtgRecallService.CurrentPageIndex)

    End Sub


    Private Sub dtgRecallService_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgRecallService.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            dtgRecallService.SelectedIndex = e.Item.ItemIndex
            ViewRecallCategory(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewRecallCategory(e.Item.Cells(0).Text, True)
            btnSearch.Enabled = False
            dtgRecallService.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Try
                DeleteRecallCategory(e.Item.Cells(0).Text)

            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try


        End If
    End Sub

    Private Sub dtgRecallService_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgRecallService.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then

            e.Item.DataItem.GetType().ToString()
            Dim objRS As RecallService = CType(e.Item.DataItem, RecallService)

            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgRecallService.CurrentPageIndex * dtgRecallService.PageSize)
                Dim lblinoRangka As Label = CType(e.Item.FindControl("lblinoRangka"), Label)



                If Not IsNothing(objRS.ChassisMaster) Then
                    lblinoRangka.Text = objRS.ChassisMaster.ChassisNumber
                Else
                    lblinoRangka.Text = objRS.ChassisMasterBB.ChassisNumber
                End If

            End If


        End If

    End Sub

    Private Sub dtgRecallService_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgRecallService.PageIndexChanged

        dtgRecallService.SelectedIndex = -1
        dtgRecallService.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgRecallService.CurrentPageIndex)
        ClearData()

    End Sub

    Private Sub dtgRecallService_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgRecallService.SortCommand
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

        dtgRecallService.SelectedIndex = -1
        dtgRecallService.CurrentPageIndex = 0
        BindDatagrid(dtgRecallService.CurrentPageIndex)
        ClearData()
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        If dtgRecallService.Items.Count <= 0 Then
            MessageBox.Show("Data Kosong")
            Return
        End If

        '-- Download data in datagrid to text file
        '-- Generate random number [0..9999]
        Dim sSuffix As String = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond
        '-- Temp file must be a randomly named text file!
        Dim strPath As String = Server.MapPath("") & "\..\DataTemp\FieldFixCampaignList_" & sSuffix & ".xls"
        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Try
            If imp.Start() Then
                Dim finfo As FileInfo = New FileInfo(strPath)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If
                '-- Create file stream
                Dim fs As FileStream = New FileStream(strPath, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)
                '-- Write data to temp file
                DownloadRecall(sw)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
            '-- Download data to client!
            Response.Redirect("../downloadLocal.aspx?file=DataTemp\FieldFixCampaignList_" & sSuffix & ".xls")
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub
End Class
