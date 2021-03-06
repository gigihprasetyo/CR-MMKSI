
#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text
#End Region

Public Class FrmSalesmanTrainingListof
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgTraining As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtTrainingTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlKodeTraining As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlJenisTraining As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icTglCreate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglCreate2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents chkPeriode As System.Web.UI.WebControls.CheckBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variable"
    Private _downloadCmd As Boolean = False
    Private _listParticipantCmd As Boolean = False
    Dim sessHelper As SessionHelper = New SessionHelper
    Dim objSalesmanTrainingTypeFacade As New SalesmanTrainingTypeFacade(User)
#End Region

#Region "Custom Method"

    Private Sub bindgrid(ByVal idxPage As Integer)

        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanMasterTraining), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlKodeTraining.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanMasterTraining), "TrainingCode", MatchType.Exact, ddlKodeTraining.SelectedItem.Text))
        End If

        If txtTrainingTitle.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanMasterTraining), "TrainingTitle", MatchType.[Partial], txtTrainingTitle.Text))
        End If

        If ddlJenisTraining.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanMasterTraining), "SalesmanTrainingType.ID", MatchType.Exact, ddlJenisTraining.SelectedItem.Value))
        End If

        If chkPeriode.Checked = True Then
            If CType(icTglCreate.Value, Date) <= CType(icTglCreate2.Value, Date) Then
                Dim TanggalAwal As New DateTime(CInt(icTglCreate.Value.Year), CInt(icTglCreate.Value.Month), CInt(icTglCreate.Value.Day), 0, 0, 0)
                Dim TanggalAkhir As New DateTime(CInt(icTglCreate2.Value.Year), CInt(icTglCreate2.Value.Month), CInt(icTglCreate2.Value.Day), 23, 59, 59)
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanMasterTraining), "StartingDate", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SalesmanMasterTraining), "EndDate", MatchType.LesserOrEqual, Format(TanggalAkhir, "yyyy-MM-dd HH:mm:ss")))
            Else
                MessageBox.Show(SR.InvalidRangeDate)
            End If
        End If

        arrList = New SalesmanMasterTrainingFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgTraining.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgTraining.CurrentPageIndex = idxPage
        dgTraining.DataSource = arrList
        dgTraining.VirtualItemCount = totalRow
        dgTraining.DataBind()

    End Sub

    Private Sub BindDropDownLists()
        CommonFunction.BindSalesmanTrainingCode(ddlKodeTraining, Me.User, True)      
        bindArrayListToDropDownList(ddlJenisTraining, objSalesmanTrainingTypeFacade.RetrieveList(), "TrainingType")
    End Sub

    Private Sub bindArrayListToDropDownList(ByRef objDropDownList As DropDownList, ByVal objArrayList As ArrayList, ByVal DataTextField As String)
        objDropDownList.Items.Add(New ListItem("Silahkan Pilih", String.Empty))
        For Each obj As SalesmanTrainingType In objArrayList
            objDropDownList.Items.Add(New ListItem(obj.TrainingType, obj.ID))
        Next
    End Sub

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not IsNothing(sessHelper.GetSession("LOGINUSERINFO")) Then
            Dim objUserInfo As UserInfo = New UserInfo
            objUserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                CheckPrivilege()
            End If
        End If

        _downloadCmd = CheckDownloadPrivilege()
        _listParticipantCmd = CheckListParticipantPrivilege()

        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "TrainingCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            BindDropDownLists()          
            bindgrid(0)
        End If

    End Sub

    Private Sub dgTraining_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTraining.ItemDataBound

        If Not e.Item.DataItem Is Nothing Then
           
            Dim objSalesmanMasterTraining As SalesmanMasterTraining = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgTraining.CurrentPageIndex * dgTraining.PageSize)
            CType(e.Item.FindControl("lblTrainingType"), Label).Text = objSalesmanMasterTraining.SalesmanTrainingType.TrainingType
            CType(e.Item.FindControl("lblPeriod"), Label).Text = objSalesmanMasterTraining.StartingDate.ToString("dd-MM-yyyy") & " s/d " & objSalesmanMasterTraining.EndDate.ToString("dd-MM-yyyy")

            Dim lbtnAnnouncement As LinkButton = CType(e.Item.FindControl("lbtnAnnouncement"), LinkButton)
            If objSalesmanMasterTraining.AnnouncementFileName <> "" Then
                lbtnAnnouncement.CommandArgument = objSalesmanMasterTraining.AnnouncementFileName
                lbtnAnnouncement.Visible = _downloadCmd
            Else
                lbtnAnnouncement.Visible = False
            End If

            Dim lbtnMaterial As LinkButton = CType(e.Item.FindControl("lbtnMaterial"), LinkButton)
            If objSalesmanMasterTraining.MaterialFileName <> "" Then
                lbtnMaterial.CommandArgument = objSalesmanMasterTraining.MaterialFileName
                lbtnMaterial.Visible = _downloadCmd
            Else
                lbtnMaterial.Visible = False
            End If

            Dim lbtnParticipant As LinkButton = CType(e.Item.FindControl("lbtnParticipant"), LinkButton)
            lbtnParticipant.CommandArgument = objSalesmanMasterTraining.ID
            lbtnParticipant.Attributes("onclick") = "showPopUp('../PopUp/PopupSalesTrainingParticipant.aspx?ID=" & objSalesmanMasterTraining.ID & "&date=" & Date.Now & "','',600,600);"
            lbtnParticipant.Visible = _listParticipantCmd

            Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
            lbtnDownload.CommandName = objSalesmanMasterTraining.ID
            lbtnDownload.ToolTip = "Download List Peserta"
            lbtnDownload.Visible = _listParticipantCmd

            Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            'Rina Request 290208
            If lbtnParticipant.Visible = True Then                
                If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                    lbtnParticipant.Visible = False
                End If
            End If

            If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                lbtnDownload.Visible = False
            End If
        End If

    End Sub

    Private Sub dgTraining_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTraining.ItemCommand

        If e.CommandName = "DownloadMaterial" Or e.CommandName = "DownloadAnnouncement" Then
            Dim PathFile As String = e.CommandArgument
            Response.Redirect("../Download.aspx?file=" & PathFile)
        ElseIf (e.CommandName <> String.Empty AndAlso Not e.CommandName.Contains("Sort")) Then
            SetDownload(CInt(e.CommandName))
        End If
    End Sub

    Private Sub dgTraining_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgTraining.PageIndexChanged
        dgTraining.CurrentPageIndex = e.NewPageIndex
        bindgrid(dgTraining.CurrentPageIndex)
    End Sub

    Private Sub dgTraining_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgTraining.SortCommand
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
        dgTraining.SelectedIndex = -1
        dgTraining.CurrentPageIndex = 0
        bindgrid(dgTraining.CurrentPageIndex)
    End Sub

    Private Sub SetDownload(ByVal ID As Integer)
        Dim arrList As ArrayList

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanTrainingParticipant), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanMasterTraining.ID", MatchType.Exact, ID))
        arrList = New SalesmanTrainingParticipantFacade(User).Retrieve(criterias)

        If arrList.Count > 0 Then
            DoDownload(arrList)
        Else
            MessageBox.Show("Tidak ada data yang di download")
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "TrainingParticipant" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Dim TrainingParticipantData As String = Server.MapPath("").Replace("\Salesman", "") & "\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(TrainingParticipantData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(TrainingParticipantData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteTrainingParticipantData(sw, data)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
            Exit Sub
        End Try

        'Response.Write("<script language='javascript'>window.open('../downloadlocal.aspx?file=" & "DataTemp/" & sFileName & ".xls" & "');</script>")

        Response.Redirect("../downloadlocal.aspx?file=" & "DataTemp\" & sFileName & ".xls", False)
    End Sub

    Private Sub WriteTrainingParticipantData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Training Participant")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            Dim obj As New SalesmanTrainingParticipant
            obj = data(0)

            itemLine.Append("Kode Training : " & tab)
            itemLine.Append(obj.SalesmanMasterTraining.TrainingCode & tab)
            sw.WriteLine(itemLine.ToString())
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("Nama Training : " & tab)
            itemLine.Append(obj.SalesmanMasterTraining.TrainingTitle & tab)
            sw.WriteLine(itemLine.ToString())
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("Periode : " & tab)
            itemLine.Append(obj.SalesmanMasterTraining.StartingDate.ToString("dd/MM/yyyy") & "s/d" & obj.SalesmanMasterTraining.EndDate.ToString("dd/MM/yyyy") & tab)
            sw.WriteLine(itemLine.ToString())
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Salesman ID" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Kota Dealer" & tab)
            itemLine.Append("Nama" & tab)
            itemLine.Append("Status" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As SalesmanTrainingParticipant In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.SalesmanHeader.SalesmanCode & tab)
                itemLine.Append(item.SalesmanHeader.Dealer.DealerName & tab)
                itemLine.Append(item.SalesmanHeader.Dealer.City.CityName & tab)
                itemLine.Append(item.SalesmanHeader.Name & tab)                
                If item.IsConfirm = 1 Then
                    itemLine.Append("Confirm" & tab)
                Else
                    itemLine.Append("Tidak Confirm" & tab)
                End If
                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub


#End Region

#Region "Privilege"

    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.Pelat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pelatihan Tenaga Penjual - Daftar Pelatihan")
        End If
    End Sub

    Private Function CheckDownloadPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PelatihanListView_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CheckListParticipantPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PesertaPelatihanListView_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region


    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        bindgrid(0)
    End Sub
End Class
