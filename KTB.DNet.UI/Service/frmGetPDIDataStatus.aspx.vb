#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessValidation
Imports System.Collections.Generic
Imports System.Linq
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class frmGetPDIDataStatus
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents dgPDI As System.Web.UI.WebControls.DataGrid
    Dim dt As DateTime = DateTime.Now
    Dim Suffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
    'Dim suffix As String = New Random().Next(10000).ToString()
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents ICDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnRefresh As System.Web.UI.WebControls.Button
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealerBranch As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCategori As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategori2 As System.Web.UI.WebControls.Label
    Private _sessHelper As SessionHelper = New SessionHelper

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Method"
    Dim ArrDealer As New ArrayList
    Dim ArrDealerBranch As New ArrayList
    Dim crRelease As Date = CDate(New AppConfigFacade(User).Retrieve("PDIPKTReleaseDate").Value)

    Private Sub bindDgPDI()

        Dim objPDIAl As ArrayList = New SessionHelper().GetSession("PDIAl")

        If IsNothing(objPDIAl) Then

            Dim criterias As New CriteriaComposite(New Criteria(GetType(PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim objDealer As Dealer = CType(_sessHelper.GetSession("DEALER"), Dealer)

            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            End If
            'Permintaan baru untuk menambahkan kriteria sbb 
            If txtKodeDealer.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
            End If

            If txtKodeDealerBranch.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtKodeDealerBranch.Text.Replace(";", "','") & "')"))
            End If

            If Me.ddlCategory.SelectedIndex > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PDI), "ChassisMaster.Category.ID", MatchType.Exact, Me.ddlCategory.SelectedValue))
            End If

            Dim sorts As SortCollection = New SortCollection
            sorts.Add(New Sort(GetType(PDI), CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection)))

            If Not validateCriteria(criterias) Then
                Exit Sub
            End If

            objPDIAl = New PDIFacade(User).Retrieve(criterias, sorts)
            _sessHelper.SetSession("PDIAl", objPDIAl)
            For Each PDIItem As PDI In objPDIAl
                Dim isDealerAlreadyExist As Boolean = False
                Dim isDealerBranchExist As Boolean = False

                For Each item As String In ArrDealer
                    If item = PDIItem.Dealer.DealerCode Then
                        isDealerAlreadyExist = True
                    End If
                Next
                If Not isDealerAlreadyExist Then
                    ArrDealer.Add(PDIItem.Dealer.DealerCode)
                End If

                If Not IsNothing(PDIItem.DealerBranch) Then
                    For Each item As String In ArrDealerBranch
                        If item = PDIItem.DealerBranch.DealerBranchCode Then
                            isDealerBranchExist = True
                        End If
                    Next
                    If Not isDealerBranchExist Then
                        ArrDealerBranch.Add(PDIItem.DealerBranch.DealerBranchCode)
                    End If
                End If
               
            Next
            _sessHelper.SetSession("ArrDealer", ArrDealer)
        Else
            ArrDealer = _sessHelper.GetSession("ArrDealer")
        End If

        If Not objPDIAl.Count = 0 Then

            dgPDI.DataSource = objPDIAl
            dgPDI.DataBind()

            btnDownload.Enabled = True
        Else
            dgPDI.DataSource = Nothing
            dgPDI.DataBind()

            MessageBox.Show("Data Tidak Ditemukan")
            btnDownload.Enabled = False

        End If

    End Sub
    Private Function validateCriteria(ByRef criterias As CriteriaComposite) As Boolean

        'If txtKodeDealer.Text <> "" Then
        '    criterias.opAnd(New Criteria(GetType(PDI), "Dealer.DealerCode", MatchType.InSet, seperatePopUpReturn(txtKodeDealer.Text.Trim())))
        'End If

        If ICDari.Value.ToString <> "" And ICSampai.Value.ToString <> "" Then

            If ICSampai.Value.Subtract(ICDari.Value).Days < 0 Then
                MessageBox.Show("Kriteria tanggal Rilis tidak valid.")
                Return False
            End If

            If ICSampai.Value.Subtract(ICDari.Value).Days > 65 Then
                MessageBox.Show("Tanggal rilis tidak boleh melebihi 65 hari")
                Return False
            End If

            Dim tglAwal As DateTime = New DateTime(ICDari.Value.Year, ICDari.Value.Month, ICDari.Value.Day, 0, 0, 0)
            Dim tglAkhir As DateTime = New DateTime(ICSampai.Value.Year, ICSampai.Value.Month, ICSampai.Value.Day, 23, 59, 59)
             
            criterias.opAnd(New Criteria(GetType(PDI), "ReleaseDate", MatchType.GreaterOrEqual, tglAwal))
            criterias.opAnd(New Criteria(GetType(PDI), "ReleaseDate", MatchType.LesserOrEqual, tglAkhir))
        End If

        If ddlStatus.SelectedItem.Value.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(PDI), "PDIStatus", MatchType.Exact, ddlStatus.SelectedValue))
        End If
        Return True
    End Function

    Private Sub bindDdlStatus()

        Dim listTitle As New EnumFSStatus
        'Dim al As ArrayList = listTitle.RetrieveFSStatus
        Dim al2 As ArrayList = listTitle.RetrieveFSStatus
        'Dim i As Integer
        'For i = 0 To al.Count - 1
        'al2.Item(al.Count - 1 - i) = al.Item(i)
        'Next
        For Each item As EnumFS In al2
            If item.NameFSStatus <> "Rilis" And item.NameFSStatus <> "Baru" Then
                ddlStatus.Items.Add(New ListItem(item.NameFSStatus, item.ValFSStatus))
            End If
        Next
        ddlStatus.Items.Insert(0, New ListItem("Pilih Semua", ""))

    End Sub

    Private Sub bindItemDgPDI(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            Dim objPDI As PDI
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblPDIStatus As Label = CType(e.Item.FindControl("lblPDIStatus"), Label)
            Dim lbDownload As LinkButton = CType(e.Item.FindControl("lbDownload"), LinkButton)

            objPDI = CType(e.Item.DataItem, PDI)
            lblNo.Text = (e.Item.ItemIndex + 1) + (dgPDI.CurrentPageIndex * dgPDI.PageSize)

            

            If objPDI.PDIStatus = EnumFSStatus.FSStatus.Baru Then
                lblPDIStatus.Text = "Baru"
                'ElseIf objPDI.PDIStatus = EnumFSStatus.FSStatus.Rilis Then
                '    lblPDIStatus.Text = "Rilis"
            ElseIf objPDI.PDIStatus = EnumFSStatus.FSStatus.Proses Then
                lblPDIStatus.Text = "Proses"
            ElseIf objPDI.PDIStatus = EnumFSStatus.FSStatus.Selesai Then
                lblPDIStatus.Text = "Selesai"
            End If

            lbDownload.Visible = ((objPDI.PDIStatus = EnumFSStatus.FSStatus.Proses Or objPDI.PDIStatus = EnumFSStatus.FSStatus.Selesai) And objPDI.CreatedTime.Date >= crRelease)

        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Dim lblTotalDealer As Label = e.Item.FindControl("lblTotalDealerValue")
            lblTotalDealer.Text = ArrDealer.Count

            Dim lblTotalDealerBranch As Label = e.Item.FindControl("lblTotalDealerBranchValue")
            lblTotalDealerBranch.Text = ArrDealerBranch.Count

            Dim lblTotalUnit As Label = e.Item.FindControl("lblTotalUnitValue")
            Dim arrPDI As ArrayList = _sessHelper.GetSession("PDIAl")
            lblTotalUnit.Text = arrPDI.Count

        End If

    End Sub

    Private Sub download()
        Dim strText As StringBuilder
        Dim objAl As New ArrayList
        Dim Delimiter As String = Chr(9)
        checkFileExistenceToDownload()

        objAl = CType(Session.Item("PDIAl"), ArrayList)
        If IsNothing(objAl) Or objAl.Count = 0 Then
            MessageBox.Show(SR.DataNotFound("PDI"))
            Return
        End If

        For count As Integer = 0 To objAl.Count - 1

            Dim objPDI As PDI = CType(objAl.Item(count), PDI)
            strText = New StringBuilder

            strText.Append(objPDI.Dealer.DealerCode.ToString())
            strText.Append(Delimiter)
            If IsNothing(objPDI.DealerBranch) Then
                strText.Append(String.Empty)
            Else
                strText.Append(objPDI.DealerBranch.DealerBranchCode.ToString())
            End If
            strText.Append(Delimiter)
            strText.Append(objPDI.ChassisMaster.ChassisNumber.ToString().Trim())
            strText.Append(Delimiter)
            strText.Append(objPDI.ChassisMaster.SerialNumber.ToString().Trim())
            strText.Append(Delimiter)
            strText.Append(objPDI.ChassisMaster.EngineNumber.ToString().Trim())
            strText.Append(Delimiter)
            strText.Append(objPDI.ChassisMaster.VechileColor.VechileType.VechileTypeCode.ToString().Trim())
            strText.Append(Delimiter)
            strText.Append(objPDI.Kind.ToString().Trim())
            strText.Append(Delimiter)
            strText.Append(objPDI.PDIDate.ToString("dd/MM/yyyy").Trim())
            strText.Append(Delimiter)
            'strText.Append(objPDI.CreatedTime.ToString("dd/MM/yyyy").Trim())
            'changes based on CRF # SV / 9 on 27/04/2007
            strText.Append(objPDI.ReleaseDate.ToString("dd/MM/yyyy").Trim())
            strText.Append(Delimiter)
            strText.Append(objPDI.WorkOrderNumber.ToString())
            saveToTextFile(strText.ToString())
        Next

        Response.Redirect("../downloadlocal.aspx?file=DataTemp\StatusPDI" & Suffix & ".txt")
        MessageBox.Show("Data Telah Disimpan")
    End Sub

    Private Sub checkFileExistenceToDownload()

        Dim finfo As FileInfo = New FileInfo(Me.Server.MapPath("") & "\..\DataTemp\StatusPDI" & Suffix & ".txt")

        If finfo.Exists Then
            finfo.Delete()
        End If

    End Sub

    Private Sub saveToTextFile(ByVal str As String)

        Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\StatusPDI" & Suffix & ".txt", FileMode.Append, FileAccess.Write)
        'Dim objFileStream As New FileStream(Path.GetTempPath & "FS.txt", FileMode.Append, FileAccess.Write)
        Dim objStreamWriter As New StreamWriter(objFileStream)

        objStreamWriter.WriteLine(str)
        objStreamWriter.Close()

    End Sub

    Private Function seperatePopUpReturn(ByVal sDealerCodeCollumn As String)
        Dim sDealerCodeTemp() As String = sDealerCodeCollumn.Split(New Char() {";"})
        Dim sDealerCode As String = ""
        For i As Integer = 0 To sDealerCodeTemp.Length - 1
            sDealerCode = sDealerCode & "'" & sDealerCodeTemp(i).Trim() & "'"

            If Not (i = sDealerCodeTemp.Length - 1) Then
                sDealerCode = sDealerCode & ","
            End If
        Next
        sDealerCode = "(" & sDealerCode & ")"
        Return sDealerCode
    End Function

    'Private Sub assignAttributeControl()
    '    lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    'End Sub


#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            If Not IsNothing(Session("DEALER")) Then
                Dim ObjDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
                _sessHelper.SetSession("sessDealer", ObjDealer)

                _sessHelper.SetSession("sessDealerCodeLogin", ObjDealer.DealerCode)
                _sessHelper.SetSession("sessDealerIDLogin", ObjDealer.ID)

                ViewState("currentSortColumn") = "Dealer.DealerCode"
                ViewState("currentSortDirection") = Sort.SortDirection.ASC
                bindDdlCategory()
                bindDdlStatus()

                Dim isDealer As Boolean = IIf(CType(Session("LOGINUSERINFO"), UserInfo).Dealer.Title = EnumDealerTittle.DealerTittle.DEALER, False, True)

                lblCategori.Visible = isDealer
                lblCategori2.Visible = isDealer
                ddlCategory.Visible = isDealer


                For IC As Integer = 0 To dgPDI.Columns.Count - 1
                    If dgPDI.Columns(IC).HeaderText.ToLower() = "kategori" Then
                        dgPDI.Columns(IC).Visible = isDealer
                    End If

                Next
            Else
                'Response.Redirect("../SessionExpired.htm")
            End If
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblSearchDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
    End Sub

    Private Sub bindDdlCategory()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim aCs As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
        Me.ddlCategory.Items.Clear()
        Me.ddlCategory.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each oC As Category In aCs
            Me.ddlCategory.Items.Add(New ListItem(oC.CategoryCode, oC.ID))
        Next
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.PDIStatusView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PDI - Daftar Status PDI")
        End If

        'FreeServiceUploadSave_Privilege  
        btnDownload.Visible = SecurityProvider.Authorize(Context.User, SR.PDIStatusDownload_Privilege)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        dgPDI.CurrentPageIndex = 0
        _sessHelper.RemoveSession("PDIAl")

        bindDgPDI()
    End Sub

    Private Sub dgFreeService_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPDI.ItemDataBound
        bindItemDgPDI(e)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        download()
    End Sub

    Private Sub dgPDI_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPDI.SortCommand

        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.ASC
        End If

        dgPDI.SelectedIndex = -1
        dgPDI.CurrentPageIndex = 0
        _sessHelper.RemoveSession("PDIAl")
        bindDgPDI()

    End Sub

#End Region

    Private Sub dgPDI_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPDI.PageIndexChanged
        dgPDI.CurrentPageIndex = e.NewPageIndex
        bindDgPDI()
    End Sub

    Protected Sub dgPDI_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPDI.ItemCommand
        Dim hdID As HiddenField = CType(e.Item.FindControl("hdID"), HiddenField)

        Select Case e.CommandName
            Case "download"
                Dim _SANstring As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204

                Dim pdiFacade As New PDIFacade(User)
                Dim objPDI As PDI = pdiFacade.Retrieve(CInt(hdID.Value))
                Dim pdiValidation As PDIValidation = New PDIValidation(_SANstring, _user, _password, _webServer)
                Dim filename As String = objPDI.FileName
                Dim validationResults As List(Of ValidResult) = New List(Of ValidResult)
                Dim isUpdate As Boolean = False

                If pdiValidation.GenerateCertificate(objPDI, isUpdate, filename, Nothing, validationResults, False, True) Then
                    If isUpdate Then
                        objPDI.FileName = filename
                        pdiFacade.Update(objPDI)
                    End If
                End If

                If validationResults.Count = 0 Then
                    Response.Redirect("../download.aspx?file=" & filename)
                Else
                    MessageBox.Show(validationResults.FirstOrDefault().Message)
                End If
        End Select
    End Sub
End Class