Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Event
#End Region

Public Class FrmEventParameter
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents upFileMaterialName As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents upFileDirectionName As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents upFileProposalName As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents lblEventNo As System.Web.UI.WebControls.Label
    Protected WithEvents ddlPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlStartMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlEndMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgEventParameter As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents txtEventNo As System.Web.UI.WebControls.TextBox

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
    Private arlEventMaster As ArrayList
    Dim objEventMaster As EventMaster = New EventMaster
    Dim objEventMasterFacade As EventMasterFacade = New EventMasterFacade(User)
    Private sHelper As SessionHelper = New SessionHelper
#End Region

#Region "Custom Method"
    Private Sub BindYear()
        Dim i As Integer
        Dim yearMax As Integer = Year(Date.Now) + 2
        Dim yearMin As Integer = Year(Date.Now) - 1
        For i = yearMin To yearMax
            ddlPeriod.Items.Add(i.ToString)
        Next
        Dim objDealer As Dealer = Session.Item("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Dim li As ListItem = New ListItem("Silahkan Pilih", -1)
            ddlPeriod.Items.Insert(0, li)
        End If
        ' ddlPeriod.SelectedIndex = -1
    End Sub
    Private Sub BindMonth()
        CommonFunction.BindFromEnum("Month", ddlStartMonth, User, False, "NameStatus", "ValStatus")
        CommonFunction.BindFromEnum("Month", ddlEndMonth, User, False, "NameStatus", "ValStatus")
        Dim objDealer As Dealer = Session.Item("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Dim liStartMonth As ListItem = New ListItem("Silahkan Pilih", -1)
            ddlStartMonth.Items.Insert(0, liStartMonth)
            Dim liEndMonth As ListItem = New ListItem("Silahkan Pilih", -1)
            ddlEndMonth.Items.Insert(0, liEndMonth)
        End If
    End Sub
    Private Sub ClearData()
        BindYear()
        BindMonth()
        lblEventNo.Text = ""
        dtgEventParameter.SelectedIndex = -1
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim objDealer As Dealer = Session.Item("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                arlEventMaster = New EventMasterFacade(User).RetrieveActiveList(indexPage + 1, dtgEventParameter.PageSize, totalRow, sHelper.GetSession("SortColEventParameter"), sHelper.GetSession("SortDirectionEventParameter"))
            Else
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If txtEventNo.Text.Trim <> "" Then
                    criterias.opAnd(New Criteria(GetType(EventMaster), "EventNo", MatchType.Exact, txtEventNo.Text.Trim))
                End If
                If ddlPeriod.SelectedValue <> -1 Then
                    criterias.opAnd(New Criteria(GetType(EventMaster), "Period", MatchType.Exact, ddlPeriod.SelectedValue))
                End If
                If ddlEndMonth.SelectedValue <> -1 Then
                    criterias.opAnd(New Criteria(GetType(EventMaster), "EndMonth", MatchType.Exact, ddlEndMonth.SelectedValue))
                End If
                If ddlStartMonth.SelectedValue <> -1 Then
                    criterias.opAnd(New Criteria(GetType(EventMaster), "StartMonth", MatchType.Exact, ddlStartMonth.SelectedValue))
                End If
                arlEventMaster = New EventMasterFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgEventParameter.PageSize, totalRow, sHelper.GetSession("SortColEventParameter"), sHelper.GetSession("SortDirectionEventParameter"))
            End If
            dtgEventParameter.DataSource = arlEventMaster
            dtgEventParameter.VirtualItemCount = totalRow
            dtgEventParameter.DataBind()
        End If
    End Sub
    Private Function FillEventMaster(ByVal mode As String) As EventMaster

        objEventMaster.Period = ddlPeriod.SelectedItem.Text
        objEventMaster.StartMonth = CType(ddlStartMonth.SelectedValue, Short)
        objEventMaster.EndMonth = CType(ddlEndMonth.SelectedValue, Short)

        If upFileMaterialName.Value <> "" OrElse upFileMaterialName.Value <> Nothing Then
            Dim finfo As New FileInfo(upFileMaterialName.PostedFile.FileName)
            objEventMaster.FileMaterialName = finfo.Name
            UploadFile(upFileMaterialName, "FileMaterial")
        Else
            If (mode <> "update") Then
                objEventMaster.FileMaterialName = ""
            End If
        End If

        If upFileDirectionName.Value <> "" OrElse upFileDirectionName.Value <> Nothing Then
            Dim finfo As New FileInfo(upFileDirectionName.PostedFile.FileName)
            objEventMaster.FileDirectionName = finfo.Name
            UploadFile(upFileDirectionName, "FileDirection")
        Else
            If (mode <> "update") Then
                objEventMaster.FileDirectionName = ""
            End If
        End If

        If upFileProposalName.Value <> "" OrElse upFileProposalName.Value <> Nothing Then
            Dim finfo As New FileInfo(upFileProposalName.PostedFile.FileName)
            objEventMaster.FileProposalName = finfo.Name
            UploadFile(upFileProposalName, "FileProposal")
        Else
            If (mode <> "update") Then
                objEventMaster.FileProposalName = ""
            End If
        End If

        Return objEventMaster
    End Function
    Private Sub UploadFile(ByVal uploadFileControl As HtmlInputFile, ByVal fileType As String)
        If uploadFileControl.Value <> "" OrElse uploadFileControl.Value <> Nothing Then
            Dim SrcFile As String = Path.GetFileName(uploadFileControl.PostedFile.FileName)  '-- Source file name
            Dim DestFile As String
            Select Case (fileType)
                Case "FileMaterial"
                    DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("Event") & "\Material File\" & "\" & SrcFile  '-- Destination file
                Case "FileDirection"
                    DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("Event") & "\Direction File\" & "\" & SrcFile  '-- Destination file
                Case "FileProposal"
                    DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("Event") & "\Proposal File\" & "\" & SrcFile  '-- Destination file
            End Select

            'cek max fileSize first
            Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
            If uploadFileControl.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            Else
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        uploadFileControl.PostedFile.SaveAs(DestFile)
                        imp.StopImpersonate()
                        imp = Nothing
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            End If
        End If
    End Sub
    Private Sub DeleteEventParameter(ByVal nID As Integer)
        Dim crit, crit2 As CriteriaComposite
        Dim objEventMaster As EventMaster = New EventMasterFacade(User).Retrieve(nID)
        Dim objEventMasterFacade As EventMasterFacade = New EventMasterFacade(User)

        crit = New CriteriaComposite(New Criteria(GetType(EventInfo), "EventMaster.ID", MatchType.Exact, objEventMaster.ID))
        crit2 = New CriteriaComposite(New Criteria(GetType(EventDocument), "EventMaster.ID", MatchType.Exact, objEventMaster.ID))

        Dim arlEventInfo As ArrayList = New EventInfoFacade(User).Retrieve(crit)
        Dim arlEventDocument As ArrayList = New EventDocumentFacade(User).Retrieve(crit2)

        Dim sign = 1

        If ((arlEventInfo.Count > 0) Or (arlEventDocument.Count > 0)) Then
            sign = 0
            MessageBox.Show("Data tidak dapat dihapus karena digunakan pada Event Info atau Event Document")
        End If

        If (sign <> 0) Then
            objEventMasterFacade.DeleteFromDB(objEventMaster)
        End If
        BindDataGrid(dtgEventParameter.CurrentPageIndex)
    End Sub
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            ClearData()
            sHelper.SetSession("SortColEventParameter", "EventNo")
            sHelper.SetSession("SortDirectionEventParameter", Sort.SortDirection.ASC)
            Dim objDealer As Dealer = Session.Item("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                btnSimpan.Visible = True
                btnBatal.Visible = True
                btnCari.Visible = False
                lblEventNo.Visible = True
                txtEventNo.Visible = False
                upFileDirectionName.Visible = True
                upFileMaterialName.Visible = True
                upFileProposalName.Visible = True
            Else
                upFileDirectionName.Visible = False
                upFileMaterialName.Visible = False
                upFileProposalName.Visible = False
                btnSimpan.Visible = False
                btnBatal.Visible = False
                btnCari.Visible = True
                lblEventNo.Visible = False
                txtEventNo.Visible = True
                dtgEventParameter.Columns(dtgEventParameter.Columns.Count - 1).Visible = False
            End If
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim Idedit As Integer
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            Idedit = 0
        ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
            Idedit = CType(sHelper.GetSession("objedit"), EventMaster).ID
        End If

        Dim nResult As Integer = -1

        If CType(ViewState("vsProcess"), String) = "Insert" Then
            objEventMaster = FillEventMaster("insert")

            If New EventMasterFacade(User).ValidateCode(objEventMaster.Period, objEventMaster.StartMonth, objEventMaster.EndMonth) > 0 Then
                DuplicatePeriodMessage(objEventMaster.StartMonth, objEventMaster.EndMonth, objEventMaster.Period)
            Else
                nResult = New EventMasterFacade(User).Insert(objEventMaster)
                alertMessage(nResult)
            End If

        ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
            objEventMaster = CType(sHelper.GetSession("objedit"), EventMaster)
            objEventMaster = FillEventMaster("update")
            If New EventMasterFacade(User).ValidateCode(objEventMaster.Period, objEventMaster.StartMonth, objEventMaster.EndMonth, objEventMaster.ID) > 0 Then
                DuplicatePeriodMessage(objEventMaster.StartMonth, objEventMaster.EndMonth, objEventMaster.Period)
            Else
                nResult = New EventMasterFacade(User).Update(objEventMaster)
                alertMessage(nResult)
            End If
        End If

        BindDataGrid(dtgEventParameter.CurrentPageIndex)
        ClearData()

    End Sub
    Private Sub alertMessage(ByVal nResult As Integer)
        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            If (CType(ViewState("vsProcess"), String) = "Insert") Then
                Dim _obj As EventMaster = New EventMasterFacade(User).Retrieve(nResult)
                MessageBox.Show(SR.SaveSuccess & " : No Event = " & _obj.EventNo.ToString)
            Else
                MessageBox.Show(SR.UpdateSucces)
            End If
        End If
    End Sub
    Private Sub DuplicatePeriodMessage(ByVal StartMonth As Integer, ByVal EndMonth As Integer, ByVal Period As Integer)
        If (StartMonth <> EndMonth) Then
            MessageBox.Show("Periode " & enumMonthGet.GetName(StartMonth - 1) & " - " & enumMonthGet.GetName(EndMonth - 1) & Period & " sudah pernah digunakan, ganti dengan periode lainnya!")
        Else
            MessageBox.Show("Periode " & enumMonthGet.GetName(StartMonth - 1) & Period & " sudah pernah digunakan, ganti dengan periode lainnya!")
        End If
    End Sub

    Private Sub dtgEventParameter_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEventParameter.SortCommand
        If e.SortExpression = sHelper.GetSession("SortColEventParameter") Then
            If sHelper.GetSession("SortDirectionEventParameter") = Sort.SortDirection.ASC Then
                sHelper.SetSession("SortDirectionEventParameter", Sort.SortDirection.DESC)
            Else
                sHelper.SetSession("SortDirectionEventParameter", Sort.SortDirection.ASC)
            End If
        End If
        sHelper.SetSession("SortColEventParameter", e.SortExpression)
        dtgEventParameter.SelectedIndex = -1
        dtgEventParameter.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub

    Private Sub dtgEventParameter_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEventParameter.PageIndexChanged
        dtgEventParameter.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgEventParameter.CurrentPageIndex)
    End Sub

    Private Sub dtgEventParameter_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEventParameter.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (arlEventMaster Is Nothing) Then
                objEventMaster = arlEventMaster(e.Item.ItemIndex)

                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 + (dtgEventParameter.CurrentPageIndex * dtgEventParameter.PageSize)

                Dim lblFileMaterialName As Label = CType(e.Item.FindControl("lblFileMaterialName"), Label)
                lblFileMaterialName.Text = objEventMaster.FileMaterialName

                Dim lblFileProposalName As Label = CType(e.Item.FindControl("lblFileProposalName"), Label)
                lblFileProposalName.Text = objEventMaster.FileProposalName

                '---modify by ronny ---> add icon download file petunjuk pelaksanaan
                Dim lblFileDirectionName As Label = CType(e.Item.FindControl("lblFileDirectionName"), Label)
                lblFileDirectionName.Text = objEventMaster.FileDirectionName

                Dim lbtnFileDirectionName As LinkButton = CType(e.Item.FindControl("lbtnFileDirectionName"), LinkButton)
                If (objEventMaster.FileDirectionName <> "") Then
                    lbtnFileDirectionName.CommandArgument = objEventMaster.FileDirectionName
                Else
                    lbtnFileDirectionName.Visible = False
                End If

                '---end modify

                Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                _lbtnEdit.CommandArgument = objEventMaster.ID

                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                lbtnDelete.CommandArgument = objEventMaster.ID

                Dim lblStartMonth As Label = CType(e.Item.FindControl("lblStartMonth"), Label)
                lblStartMonth.Text = enumMonthGet.GetName(objEventMaster.StartMonth)

                Dim lblEndMonth As Label = CType(e.Item.FindControl("lblEndMonth"), Label)
                lblEndMonth.Text = enumMonthGet.GetName(objEventMaster.EndMonth)

                Dim lbtnPopUp As LinkButton = CType(e.Item.FindControl("lbtnPopUp"), LinkButton)
                If (objEventMaster.FileMaterialName <> "") Then
                    lbtnPopUp.CommandArgument = objEventMaster.FileMaterialName
                Else
                    lbtnPopUp.Visible = False
                End If

                Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
                If (objEventMaster.FileProposalName <> "") Then
                    lbtnView.CommandArgument = objEventMaster.FileProposalName
                Else
                    lbtnView.Visible = False
                End If
            End If
        End If
    End Sub

    Private Sub dtgEventParameter_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEventParameter.ItemCommand
        If e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            dtgEventParameter.SelectedIndex = e.Item.ItemIndex
            Dim objEventMaster As EventMaster = New EventMasterFacade(User).Retrieve(CInt(e.CommandArgument))
            sHelper.SetSession("objedit", objEventMaster)
            lblEventNo.Text = objEventMaster.EventNo
            ddlPeriod.SelectedItem.Text = objEventMaster.Period
            ddlStartMonth.SelectedValue = objEventMaster.StartMonth
            ddlEndMonth.SelectedValue = objEventMaster.EndMonth
            'dtgEventParameter.Columns(dtgEventParameter.Columns.Count-1).Visible=False 
        ElseIf (e.CommandName = "Download") Then
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("Event") & "\Material File\" & "\" & e.CommandArgument
            Response.Redirect("../Download.aspx?file=" & PathFile)
        ElseIf (e.CommandName = "Download2") Then
            Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("Event") & "\Proposal File\" & "\" & e.CommandArgument
            Response.Redirect("../Download.aspx?file=" & PathFile)
        ElseIf e.CommandName = "Delete" Then
            DeleteEventParameter(e.CommandArgument)
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        lblEventNo.Text = ""
        'dtgEventParameter.Columns(dtgEventParameter.Columns.Count - 1).Visible = True

    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgEventParameter.CurrentPageIndex = 0
        BindDataGrid(0)
    End Sub
End Class
