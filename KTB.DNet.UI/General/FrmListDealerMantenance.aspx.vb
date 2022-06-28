Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports System
Imports System.Drawing.Color
Imports System.Text
Imports KTB.DNet.Security
Imports OfficeOpenXml
Imports System.IO
Imports Excel

Public Class FrmListDealerMantenance
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgDealerList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cbSalesUnit As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlstatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlstatuspublish As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbService As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbSparePart As System.Web.UI.WebControls.CheckBox
    Protected WithEvents LnkDownloadTemplate As System.Web.UI.WebControls.LinkButton
    Protected WithEvents fileUploadExcel As System.Web.UI.WebControls.FileUpload
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Private _sessHelper As SessionHelper = New SessionHelper
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        InitializeComponent()
    End Sub


    'CODEGEN: This method call is required by the Web Form Designer
    'Do not modify it using the code editor.

#End Region

    Private objDealer As Dealer
    Private m_bAdminAssigHakAccessOrganization_Privilege As Boolean = False
    Private m_bFormPrivilege As Boolean = False

    Private Sub BindDatagrid(ByVal indexPage)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) And Not IsNothing(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite)) Then
            dtgDealerList.DataSource = New DealerFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite), _
              indexPage + 1, dtgDealerList.PageSize, totalRow, _
              CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgDealerList.VirtualItemCount = totalRow
            dtgDealerList.DataBind()
        End If
    End Sub
    Private Sub BindDdlStatus()
        Dim listStatus As New EnumDealerStatus
        Dim al As ArrayList = listStatus.RetrieveStatus
        For Each item As EnumDealer In al
            ddlstatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        Next
        ddlstatus.Items.Insert(0, New ListItem("Pilih Status", ""))
    End Sub
    Private Sub BindDdlStatusPublish()
        Dim listStatus As New EnumDealerStatus
        Dim al As ArrayList = listStatus.RetrieveStatusPublish
        For Each item As EnumDealer In al
            ddlstatuspublish.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        Next
        ddlstatuspublish.Items.Insert(0, New ListItem("Silahkan Pilih", ""))
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDdlStatus()
            BindDdlStatusPublish()
            InitiatePage()
            'kalau dipanggil dari form dealer maintenance setelah proses update
            If Not (Request.QueryString("DealerID")) Is Nothing Then
                Dim nID As Integer = CType(Request.QueryString("DealerID"), Integer)
                If Not IsNothing(nID) Then
                    MessageBox.Show("Update Sukses !")
                    Dim ObjDealer As Dealer = New DealerFacade(User).Retrieve(nID)
                    If Not ObjDealer Is Nothing Then
                        Dim arrObjDealer As ArrayList = New ArrayList
                        arrObjDealer.Add(ObjDealer)
                        dtgDealerList.DataSource = arrObjDealer
                        dtgDealerList.DataBind()
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub ActivateUserPrivilege()

        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AdminUpdateOrganization_Privilege)
        m_bAdminAssigHakAccessOrganization_Privilege = SecurityProvider.Authorize(Context.User, SR.AdminAssigHakAccessOrganization_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.AdminViewOrganization_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Daftar Organisasi")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        AssignAttributeControl()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub ClearData()
        cbSalesUnit.Checked = False
        cbService.Checked = False
        cbSparePart.Checked = False
    End Sub
    Private Sub AssignAttributeControl()
        Dim lblPopUp As Label = CType(Page.FindControl("lblPopUpDealer"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection")
    End Sub
    Private Function ConvertKodeDealer(ByVal sKodeDealerColl As String)
        Dim sKodeDealerTemp() As String = sKodeDealerColl.Split(New Char() {";"})
        Dim sKodeDealer As String = ""
        For i As Integer = 0 To sKodeDealerTemp.Length - 1
            sKodeDealer = sKodeDealer & "'" & sKodeDealerTemp(i).Trim() & "'"

            If Not (i = sKodeDealerTemp.Length - 1) Then
                sKodeDealer = sKodeDealer & ","
            End If
        Next
        sKodeDealer = "(" & sKodeDealer & ")"
        Return sKodeDealer
    End Function

    Private Sub CreateCriteriaSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If
        If Not (txtDealerName.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerName", MatchType.[Partial], txtDealerName.Text.Trim()))
        End If
        If Not ddlstatus.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, ddlstatus.SelectedValue))
        End If
        If Not ddlstatuspublish.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Publish", MatchType.Exact, ddlstatuspublish.SelectedValue))
        End If
        If cbSalesUnit.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SalesUnitFlag", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If
        If cbService.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ServiceFlag", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If
        If cbSparePart.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SparepartFlag", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "OrganizationBranchType", MatchType.InSet, "(0,1,5,6)"))

        _sessHelper.SetSession("Criteria", criterias)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CreateCriteriaSearch()
        dtgDealerList.CurrentPageIndex = 0
        BindDatagrid(dtgDealerList.CurrentPageIndex)
    End Sub

    Private Sub dtgDealerList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            BoundRowItems(e)
        End If
    End Sub

    Private Function CreateAggreateForCountRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function GetUserInfoCriteria(ByVal nDealerID As Integer, ByVal cUserStatus As Byte) As ICriteria
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "Dealer.ID", MatchType.Exact, nDealerID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "UserStatus", MatchType.Exact, cUserStatus))
        Return criterias
    End Function

    Private Sub BoundRowItems(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim objDealer As Dealer = CType(CType(dtgDealerList.DataSource, ArrayList)(e.Item.ItemIndex), Dealer)
        CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgDealerList.CurrentPageIndex * dtgDealerList.PageSize)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        If lblStatus.Text = "Tidak Aktif" Then
            lblStatus.ForeColor = Red
            e.Item.BackColor = Color.LightSalmon
        End If

        Dim lbtnActive As LinkButton = CType(e.Item.FindControl("lbtnActive"), LinkButton)
        Dim lbtnNonActive As LinkButton = CType(e.Item.FindControl("lbtnNonActive"), LinkButton)
        If objDealer.Publish = True Then
            lbtnNonActive.Visible = False
        Else
            lbtnActive.Visible = False
        End If

        'privilege
        'ActivateUserPrivilege()
        If Not CType(e.Item.FindControl("lbtnEdit"), LinkButton) Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
        End If


        If Not CType(e.Item.FindControl("lbtnHakAkses"), LinkButton) Is Nothing Then
            CType(e.Item.FindControl("lbtnHakAkses"), LinkButton).Visible = m_bAdminAssigHakAccessOrganization_Privilege
        End If

        If Not CType(e.Item.FindControl("lbtnHakAkses"), LinkButton) Is Nothing Then
            'CType(e.Item.FindControl("lbtnHakAkses"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not CType(e.Item.FindControl("lblUserActive"), Label) Is Nothing Then
            CType(e.Item.FindControl("lblUserActive"), Label).Text = CType(New DealerFacade(User).RecordCount(CreateAggreateForCountRecord(GetType(UserInfo)), GetUserInfoCriteria(objDealer.ID, EnumUserStatus.UserStatus.Aktif)), String)
        End If

    End Sub


    Private Sub dtgDealerList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDealerList.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "edit"
                Response.Redirect("../General/FrmDealerMaintenance.aspx?dealerid=" + e.Item.Cells(0).Text.Trim() + "&user=mks&isupdate=1")
            Case "view"
                Response.Redirect("../General/FrmDealerMaintenance.aspx?dealerid=" + e.Item.Cells(0).Text.Trim() + "&user=mks&isupdate=0")
            Case "hakakses"
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CInt(e.Item.Cells(0).Text.Trim()))
                _sessHelper.SetSession("SessObjDealer", objDealer)
                Response.Redirect("../UserManagement/FrmAssignAccessibility.aspx")
            Case "activate"
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CInt(e.Item.Cells(0).Text.Trim()))
                Dim history As New StatusChangeHistory
                Dim _result As Integer = 0
                history.DocumentRegNumber = objDealer.DealerCode
                history.OldStatus = IIf(objDealer.Publish = True, 1, 0)
                objDealer.Publish = True
                history.NewStatus = IIf(objDealer.Publish = True, 1, 0)
                _result = New DealerFacade(User).Update(objDealer)
                _sessHelper.SetSession("SessObjDealer", objDealer)
                history.DocumentType = New KTB.DNet.BusinessFacade.StandardCodeFacade(User).GetByCategoryValueCode("LookUp.DocumentType", "Dealer_Publish").ValueId
                _result = New StatusChangeHistoryFacade(User).Insert(history)
                btnSearch_Click(Nothing, Nothing)
            Case "deactivate"
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CInt(e.Item.Cells(0).Text.Trim()))
                Dim history As New StatusChangeHistory
                Dim _result As Integer = 0
                history.DocumentRegNumber = objDealer.DealerCode
                history.OldStatus = IIf(objDealer.Publish = True, 1, 0)
                objDealer.Publish = False
                history.NewStatus = IIf(objDealer.Publish = True, 1, 0)
                _result = New DealerFacade(User).Update(objDealer)
                _sessHelper.SetSession("SessObjDealer", objDealer)
                history.DocumentType = New KTB.DNet.BusinessFacade.StandardCodeFacade(User).GetByCategoryValueCode("LookUp.DocumentType", "Dealer_Publish").ValueId
                _result = New StatusChangeHistoryFacade(User).Insert(history)
                btnSearch_Click(Nothing, Nothing)
        End Select

        'If e.CommandName = "view" Then
        '    'Dim objDealer As Dealer = CType(CType(Session("sessDealer"), ArrayList)(e.Item.ItemIndex), Dealer)
        '    Response.Redirect("../General/FrmDealerMaintenance.aspx?dealerid=" + e.Item.Cells(0).Text.Trim() + "&user=mks")
        'End If
        'If e.CommandName = "HakAkses" Then
        '    Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CInt(e.Item.Cells(0).Text.Trim()))
        '    _sessHelper.SetSession("SessObjDealer", objDealer)
        '    Response.Redirect("../UserManagement/FrmAssignAccessibility.aspx")
        'End If
    End Sub
    Private Sub dtgDealerList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerList.PageIndexChanged
        dtgDealerList.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgDealerList.CurrentPageIndex)
    End Sub

    Private Sub dtgDealerList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerList.SortCommand
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

        dtgDealerList.CurrentPageIndex = 0
        BindDatagrid(dtgDealerList.CurrentPageIndex)
    End Sub

    Protected Sub LnkDownloadTemplate_Click(sender As Object, e As EventArgs) Handles LnkDownloadTemplate.Click
        Try
            Dim wb As FileInfo = New FileInfo(Server.MapPath("~/DataFile/Template/Template_Upload_Status_Dealer.xlsx"))
            Using package As ExcelPackage = New ExcelPackage(wb)
                Dim qr As String = ""
                Dim tempdata As DataTable

                Dim wsDealer As ExcelWorksheet = package.Workbook.Worksheets("Sheet2")

                Dim tempCell As String = ""
                Dim arrDealer As ArrayList = New ArrayList
                Dim currentRow As Long = 0

                'Load Data Tanggal SPK
                Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If Not IsNothing(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite)) Then
                    arrDealer = New DealerFacade(User).Retrieve(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite))
                Else
                    arrDealer = New DealerFacade(User).Retrieve(criteria)
                End If

                currentRow = 3
                For Each objDealer As Dealer In arrDealer
                    wsDealer.Cells(currentRow, 1).Value = currentRow - 2
                    wsDealer.Cells(currentRow, 2).Value = objDealer.DealerCode
                    wsDealer.Cells(currentRow, 3).Value = objDealer.DealerName
                    If Not IsNothing(objDealer.DealerGroup) Then
                        wsDealer.Cells(currentRow, 4).Value = objDealer.DealerGroup.GroupName
                    End If
                    wsDealer.Cells(currentRow, 5).Value = IIf(objDealer.Status.Trim = "1", "Aktif", "Tidak Aktif")
                    wsDealer.Cells(currentRow, 6).Value = IIf(objDealer.Publish = True, "Publish", "Not Publish")
                    wsDealer.Cells(currentRow, 27).Value = objDealer.ID

                    currentRow += 1
                Next


                Response.Clear()
                Response.AddHeader("content-disposition", "attachment;filename=TemplateUploadStatusDealer.xlsx")
                Response.Charset = ""
                Me.EnableViewState = False
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.BinaryWrite(package.GetAsByteArray())
                Response.End()

            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim sb As StringBuilder = New StringBuilder
        Dim retValue As Integer = 0
        Dim totalUpdate As Integer = 0
        If fileUploadExcel.PostedFile.FileName.Length > 0 Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            sapImp.Start()
            Try
                If fileUploadExcel.PostedFile.ContentLength <> fileUploadExcel.PostedFile.InputStream.Length Then
                    'MessageBox.Show(SR.InvalidData(inFileLocation.PostedFile.FileName))
                    retValue = 0
                    Throw New Exception("File Tidak Sama")
                End If

                Dim datetimenow As String = Now.ToString("yyyy_MM_dd_H_mm_ss")

                Dim ext As String = System.IO.Path.GetExtension(fileUploadExcel.PostedFile.FileName)
                If Not CheckExt(ext.Substring(1)) Then
                    retValue = 0
                    Throw New Exception("Salah Extention")
                End If

                Dim SrcFile As String = Path.GetFileName(fileUploadExcel.PostedFile.FileName)   '-- Source file name
                SrcFile = New Date().Now.ToString("yyyyMMddhhmmss") & SrcFile
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile  '-- Temporary file

                fileUploadExcel.PostedFile.SaveAs(targetFile)

                Dim objReader As IExcelDataReader = Nothing
                Dim list As ArrayList = New ArrayList
                Dim checkSalah As Boolean = False
                Dim checkKosong As Boolean = False
                Dim arrDealer As New ArrayList
                Dim _result As Integer = 0

                Dim i As Integer = 0

                Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)

                    '   objReader = ExcelReaderFactory.CreateBinaryReader(stream)

                    If ext.ToLower.Contains("xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    Else
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    End If

                    If (Not IsNothing(objReader)) Then

                        While objReader.Read()
                            Dim oDealerFac As New DealerFacade(User)
                            Dim objDealer As New Dealer

                            If Not i > 1 Then
                                i += 1
                                Continue While
                            End If

                            If Not IsNothing(objReader.GetString(1)) Then
                                Try
                                    objDealer = oDealerFac.Retrieve(CInt(objReader.GetString(26).Trim()))
                                    If Not objDealer.ID > 0 Then
                                        sb.Append("Dealer " & objReader.GetString(1).Trim() & " Tidak Ditemukan;")
                                        checkSalah = True
                                    Else
                                        Dim history As New StatusChangeHistory
                                        history.DocumentRegNumber = objDealer.DealerCode
                                        history.OldStatus = IIf(objDealer.Publish = True, 1, 0)
                                        objDealer.Publish = IIf(objReader.GetString(5).ToLower.Trim = "publish", True, False)
                                        history.NewStatus = IIf(objDealer.Publish = True, 1, 0)
                                        history.DocumentType = New KTB.DNet.BusinessFacade.StandardCodeFacade(User).GetByCategoryValueCode("LookUp.DocumentType", "Dealer_Publish").ValueId
                                        _result = New DealerFacade(User).Update(objDealer)
                                        _result = New StatusChangeHistoryFacade(User).Insert(history)
                                        arrDealer.Add(objDealer)
                                        totalUpdate += 1
                                    End If

                                Catch ex As Exception
                                    sb.Append("Dealer " & objReader.GetString(1).Trim() & " Tidak Valid;")
                                    checkSalah = True
                                End Try
                            Else
                                Continue While
                            End If

                            i += 1
                        End While

                    End If

                End Using

                If i < 2 Then
                    checkKosong = True
                End If

                If checkSalah = True Then
                    MessageBox.Show(sb.ToString)
                    Return
                End If
                If checkKosong = True Then
                    MessageBox.Show("Data Excel tidak boleh ada yang kosong.")
                    Return
                End If

                retValue = 1
            Catch ex As Exception
                retValue = 0
            Finally
                sapImp.StopImpersonate()
                sapImp = Nothing
            End Try
        End If

        If retValue = 1 Then
            MessageBox.Show(totalUpdate.ToString & " Data Berhasil Diupdate.")
            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub

    Private Function CheckExt(ByVal ext As String) As Boolean
        Dim retValue As Boolean = False
        If ext.ToUpper() = "XLS" Then
            retValue = True
        ElseIf ext.ToUpper() = "XLSX" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function
End Class
