#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmWSCEvidanceListBB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorWSC As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategori As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalKirim As System.Web.UI.WebControls.Label
    Protected WithEvents ddlstatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlkategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtnomorwsc As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEvidence As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlTglKirim As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents txtDownload As System.Web.UI.WebControls.TextBox
    Protected WithEvents ICStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlEvidenceType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPath As System.Web.UI.WebControls.TextBox
    Protected WithEvents divPath As System.Web.UI.HtmlControls.HtmlGenericControl
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
    Private arlListWsc As ArrayList
    Private arlListWscDownload As ArrayList
    Private ObjWSCEvidance As WSCEvidenceBB
    Private ObjWSCEvidanceFacade As New WSCEvidenceBBFacade(User)
    Private sessionHelper As New sessionHelper
    Private objDealer As Dealer
#End Region

#Region "Custom Method"

    Private Sub BindGrid()
        BindToDataGrid(dtgEvidence.CurrentPageIndex)
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "Status"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Sub dtgEvidence_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEvidence.PageIndexChanged
        dtgEvidence.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Private Sub BindToDropDownList()
        Dim listitemBlank As listItem = New listItem("Silahkan Pilih", -1)

        '--DropDownList Kategori
        Dim arrayListDealer As ArrayList = New PKHeaderFacade(User).RetrieveListCategory
        ddlkategori.Items.Add(listitemBlank)
        For Each item As Category In arrayListDealer
            Dim listItem As New listItem(item.CategoryCode, item.ID)
            ddlkategori.Items.Add(listItem)
        Next

        '--DropDownList Status
        ddlstatus.Items.Add(listitemBlank)
        For Each item As listItem In enumStatusWSC.ArrayListStatus
            ddlstatus.Items.Add(item)
        Next

        '--DropDownlist Tanggal Kirim
        'For Each item As listItem In LookUp.ArraylistMonth(True, 6, 0, DateTime.Now)
        '    ddlTglKirim.Items.Add(item)
        'Next
    End Sub

    Private Sub BindToDataGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '--Select Criterias From DropDownList 
        If ddlkategori.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "WSCHeaderBB.ChassisMasterBB.Category.ID", MatchType.Exact, ddlkategori.SelectedValue))
        If ddlstatus.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "Status", MatchType.Exact, ddlstatus.SelectedValue))

        '--Select Criterias From TextBox
        If txtnomorwsc.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "WSCHeaderBB.ClaimNumber", MatchType.Exact, txtnomorwsc.Text))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "WSCHeaderBB.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "WSCHeaderBB.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))

        'Dim tgl As DateTime = System.Convert.ToDateTime(ddlTglKirim.SelectedValue)
        'Dim tglAwal As New DateTime(tgl.Year, tgl.Month, 1, 0, 0, 0)
        'Dim tglAkhir As New DateTime(tgl.Year, tgl.Month, DateTime.DaysInMonth(tgl.Year, tgl.Month), 23, 59, 59)
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "CreatedTime", MatchType.GreaterOrEqual, tglAwal))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "CreatedTime", MatchType.LesserOrEqual, tglAkhir))

        'Modify jadi range date

        Dim tglAwal As Date = New Date(ICStart.Value.Year, ICStart.Value.Month, ICStart.Value.Day, 0, 0, 0)
        Dim tglAkhir As Date = New Date(ICEnd.Value.Year, ICEnd.Value.Month, ICEnd.Value.Day, 23, 59, 59)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "CreatedTime", MatchType.GreaterOrEqual, tglAwal))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "CreatedTime", MatchType.LesserOrEqual, tglAkhir))


        '-- Evidence Type
        If ddlEvidenceType.SelectedValue <> "Pilih" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "EvidenceType", MatchType.Exact, ddlEvidenceType.SelectedValue))
        End If

        arlListWsc = New WSCEvidenceBBFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgEvidence.PageSize, _
        total, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If arlListWsc.Count = 0 Then
            btnDownload.Enabled = False
        Else
            btnDownload.Enabled = True
        End If

        dtgEvidence.DataSource = arlListWsc
        sessionHelper.SetSession("WSC", arlListWsc)
        dtgEvidence.VirtualItemCount = total
        dtgEvidence.DataBind()
    End Sub

    Private Sub delete(ByVal nId As Integer)
        Dim fileInfo As New fileInfo(Server.MapPath(""))
        Dim FilePath As String = fileInfo.Directory.FullName & "\" & dtgEvidence.Items(nId).Cells(12).Text
        fileInfo = New fileInfo(FilePath)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        If (fileInfo.Exists) Then
            Try
                success = imp.Start()
                If success Then
                    fileInfo.Delete()
                    imp.StopImpersonate()
                    imp = Nothing
                End If
            Catch ex As Exception
                Throw ex
            End Try
        Else

            'MessageBox.Show(SR.FileNotFound(fileInfo.Name))
        End If
        Dim objwschapus As WSCEvidenceBB = New WSCEvidenceBBFacade(User).Retrieve(CInt(dtgEvidence.Items(nId).Cells(0).Text))
        Dim nResult = New WSCEvidenceBBFacade(User).DeleteFromDB(objwschapus)
        dtgEvidence.CurrentPageIndex = 0
        BindToDataGrid(dtgEvidence.CurrentPageIndex)
    End Sub

    Private Sub RetriveDownload()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '--Select Criterias From DropDownList 
        If ddlkategori.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "WSCHeaderBB.ChassisMasterBB.Category.ID", MatchType.Exact, ddlkategori.SelectedValue))
        If ddlstatus.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "Status", MatchType.Exact, ddlstatus.SelectedValue))

        '--Select Criterias From TextBox
        If txtnomorwsc.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "WSCHeaderBB.ClaimNumber", MatchType.Exact, txtnomorwsc.Text))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "WSCHeaderBB.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "WSCHeaderBB.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))

        'Dim tgl As DateTime = System.Convert.ToDateTime(ddlTglKirim.SelectedValue)
        'Dim tglAwal As New DateTime(tgl.Year, tgl.Month, 1, 0, 0, 0)
        'Dim tglAkhir As New DateTime(tgl.Year, tgl.Month, DateTime.DaysInMonth(tgl.Year, tgl.Month), 23, 59, 59)
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "CreatedTime", MatchType.GreaterOrEqual, tglAwal))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "CreatedTime", MatchType.LesserOrEqual, tglAkhir))
        'Dim tglAwal As Date = ICStart.Value
        'Dim tglAkhir As Date = ICEnd.Value
        Dim tglAwal As Date = New Date(ICStart.Value.Year, ICStart.Value.Month, ICStart.Value.Day, 0, 0, 0)
        Dim tglAkhir As Date = New Date(ICEnd.Value.Year, ICEnd.Value.Month, ICEnd.Value.Day, 23, 59, 59)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "CreatedTime", MatchType.GreaterOrEqual, tglAwal))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCEvidenceBB), "CreatedTime", MatchType.LesserOrEqual, tglAkhir))


        arlListWscDownload = New WSCEvidenceBBFacade(User).Retrieve(criterias)
        sessionHelper.SetSession("arlListWsc", arlListWscDownload)

    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        If Not IsPostBack Then

            BindToDropDownList()
            BindWSCEvidenceType()
            InitiatePage()
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnDownload.Attributes.Add("onclick", "return confirm('Yakin Akan Melakukan Proses Download ??');")
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.WSBuktiListView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Bukti WSC")
        End If
        btnDownload.Visible = SecurityProvider.Authorize(Context.User, SR.WSBuktiListDownload_Privilege)
    End Sub
    Private Sub BindWSCEvidenceType()
        Dim _EnumWSCEvidenceType As New EnumWSCEvidenceType
        Dim _arrTmp As New ArrayList
        _arrTmp = _EnumWSCEvidenceType.WSCEvidenceTypeList

        ddlEvidenceType.DataSource = _arrTmp
        ddlEvidenceType.DataTextField = "WSCEvidenceTypeId"
        ddlEvidenceType.DataValueField = "WSCEvidenceTypeValue"
        ddlEvidenceType.DataBind()
        ddlEvidenceType.Items.Insert(0, "Pilih")
    End Sub

    Private Sub dtgEvidence_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEvidence.SortCommand
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

        dtgEvidence.SelectedIndex = -1
        dtgEvidence.CurrentPageIndex = 0
        BindToDataGrid(dtgEvidence.CurrentPageIndex)

    End Sub

    Sub dtgEvidence_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dtgEvidence.ItemDataBound
        If Not (arlListWsc Is Nothing) Then
            If Not (arlListWsc.Count = 0 Or e.Item.ItemIndex = -1) Then
                ObjWSCEvidance = CType(arlListWsc(e.Item.ItemIndex), WSCEvidenceBB)
                e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgEvidence.PageSize * dtgEvidence.CurrentPageIndex)
                e.Item.Cells(2).Text = CType(ObjWSCEvidance.Status, enumStatusWSC.Status).ToString
                e.Item.Cells(3).Text = ObjWSCEvidance.WSCHeaderBB.Dealer.DealerCode & " / " & ObjWSCEvidance.WSCHeaderBB.Dealer.SearchTerm1
                e.Item.Cells(4).Text = ObjWSCEvidance.WSCHeaderBB.Dealer.DealerName
                e.Item.Cells(5).Text = ObjWSCEvidance.WSCHeaderBB.ClaimNumber
                e.Item.Cells(6).Text = ObjWSCEvidance.WSCHeaderBB.ChassisMasterBB.ChassisNumber
                e.Item.Cells(7).Text = Format(ObjWSCEvidance.CreatedTime, "dd/MM/yyyy")
                If ObjWSCEvidance.CreatedBy <> String.Empty Then
                    If ObjWSCEvidance.CreatedBy.Length >= 6 Then
                        e.Item.Cells(8).Text = ObjWSCEvidance.CreatedBy.Substring(6)
                    End If
                End If

                Dim lbKwitansi As LinkButton = CType(e.Item.FindControl("lnkKwitansi"), LinkButton)
                Dim lbSurat As LinkButton = CType(e.Item.FindControl("lnkSurat"), LinkButton)
                Dim lbTeknikal As LinkButton = CType(e.Item.FindControl("lnkTeknikal"), LinkButton)
                lbKwitansi.Visible = False
                lbSurat.Visible = False
                lbTeknikal.Visible = False

                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceBBFileDirectory")
                Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
                Dim DestFullFilePath As String = fileInfo1.Directory.FullName '--& "\" & DestFile '-- Destination file

                lbKwitansi.Text = String.Empty
                lbSurat.Text = String.Empty
                lbTeknikal.Text = String.Empty

                Dim dataFile As String = DestFullFilePath & "\" & ObjWSCEvidance.PathFile
                Dim fileInfox As New FileInfo(dataFile)
                Dim fileExist As Boolean = True 'CheckFileExist(fileInfox)
                If fileExist Then
                    Select Case ObjWSCEvidance.EvidenceType
                        Case EnumWSCEvidenceType.WSCEvidenceType.KWITANSI_WSC
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lbKwitansi.Text = lbKwitansi.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0"" onmouseout =""HideEvidenceImage(this);""  onmouseover=""ShowEvidenceImage(this);"" > <br>"
                            Else
                                lbKwitansi.Text = lbKwitansi.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);""> <br>"
                            End If
                            lbKwitansi.Visible = True
                        Case EnumWSCEvidenceType.WSCEvidenceType.SURAT_WSC
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lbSurat.Text = lbSurat.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage(this);"" onmouseover=""ShowEvidenceImage(this);"" > <br>"
                            Else
                                lbSurat.Text = lbSurat.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);""> <br>"
                            End If
                            lbSurat.Visible = True
                        Case EnumWSCEvidenceType.WSCEvidenceType.TEKNIKAL_WSC
                            If dataFile.Substring(dataFile.Length - 4, 4).ToLower = ".jpg" Then
                                lbTeknikal.Text = lbTeknikal.Text & "<img src=""../images/download.gif"" lowsrc='" & dataFile & "' border=""0""   onmouseout=""HideEvidenceImage(this);"" onmouseover=""ShowEvidenceImage(this);"" >  <br>"
                            Else
                                lbTeknikal.Text = lbTeknikal.Text & "<img src=""../images/download.gif"" border=""0"" lowsrc='" & dataFile & "' onmouseover=""SetPath(this);"">  <br>"
                            End If
                            lbTeknikal.Visible = True
                    End Select
                End If

                'Dim linkbtnlihat As LinkButton = e.Item.FindControl("lbnLihat")
                Dim linkbtnHapus As LinkButton = e.Item.FindControl("lbnHapus")
                'linkbtnlihat.Text = "<img src=""../images/download.gif"" border=""0"" alt=""Download"">"
                linkbtnHapus.Text = "<img src=""../images/trash.gif"" border=""0"" alt=""Hapus"">"
                linkbtnHapus.Visible = SecurityProvider.Authorize(Context.User, SR.WSBuktiListDelete_Privilege)
                If e.Item.Cells(2).Text = enumStatusWSC.Status.Selesai.ToString Then
                    linkbtnHapus.Visible = False
                Else
                    linkbtnHapus.Visible = True
                End If

                Dim _lblEvidence As Label = CType(e.Item.FindControl("lblEvidence"), Label)
                Dim tempDesc As String = ObjWSCEvidance.Description.Trim.Replace("'", " ")
                tempDesc.Replace("""", " ")
                tempDesc.Replace(";", " ")
                tempDesc.Replace(":", " ")
                tempDesc.Replace("!", " ")
                tempDesc.Replace("@", " ")
                tempDesc.Replace("$", " ")
                tempDesc.Replace("^", " ")
                tempDesc.Replace("&", " ")
                tempDesc.Replace("*", " ")
                Dim clearString As String = KTB.DNet.UI.Helper.DNetEncryption.ClearEnterCharacter(tempDesc)
                If ObjWSCEvidance.Description <> String.Empty Then
                    _lblEvidence.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Service/frmPopupEvidence.aspx?ket=" & clearString, "", 350, 350, "DealerSelection")
                Else
                    _lblEvidence.Visible = False
                End If

                If Not e.Item.FindControl("lbnHapus") Is Nothing Then
                    CType(e.Item.FindControl("lbnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                End If
            End If
        End If
    End Sub

    Private Function CheckFileExist(ByVal fileinfo As FileInfo) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Try
            success = imp.Start()
            If success Then
                Return fileinfo.Exists
            End If
        Catch ex As Exception
            Return False
        Finally
            imp.StopImpersonate()
            imp = Nothing
        End Try

    End Function

    Sub dtgEvidence_ItemCommand(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) Handles dtgEvidence.ItemCommand
        Select Case (e.CommandName)
            Case "lnkKwitansi" '---"Lihat"
                Dim linkButton As linkButton = e.Item.FindControl("lnkKwitansi")

                'Dim fileInfo As New fileInfo(Server.MapPath("") & "\..\" & e.Item.Cells(11).Text)
                Dim fileInfox As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & e.Item.Cells(13).Text)
                Dim fileExist As Boolean = CheckFileExist(fileInfox)
                If fileExist Then

                    Try
                        'Response.Redirect("../Download.aspx?file=" & e.Item.Cells(11).Text)
                        Response.Redirect("../Download.aspx?file=" & e.Item.Cells(13).Text)
                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail(linkButton.Text))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfox.Name))
                End If
            Case "lnkSurat"
                Dim linkButton As LinkButton = e.Item.FindControl("lnkSurat")

                Dim fileInfox As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & e.Item.Cells(13).Text)
                Dim fileExist As Boolean = CheckFileExist(fileInfox)
                If fileExist Then
                    Try
                        Response.Redirect("../Download.aspx?file=" & e.Item.Cells(13).Text)
                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail(linkButton.Text))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfox.Name))
                End If
            Case "lnkTeknikal"
                Dim linkButton As LinkButton = e.Item.FindControl("lnkTeknikal")
                Dim fileInfox As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN") & e.Item.Cells(13).Text)
                Dim fileExist As Boolean = CheckFileExist(fileInfox)
                If fileExist Then

                    Try
                        Response.Redirect("../Download.aspx?file=" & e.Item.Cells(13).Text)
                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail(linkButton.Text))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfox.Name))
                End If
            Case "Hapus"
                Try
                    delete(e.Item.ItemIndex)
                    MessageBox.Show(SR.DeleteSucces())
                Catch ex As Exception
                    MessageBox.Show(SR.DeleteFail())
                End Try
        End Select
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgEvidence.CurrentPageIndex = 0
        BindToDataGrid(dtgEvidence.CurrentPageIndex)
    End Sub

    Private Sub ChangeStatus()
        arlListWscDownload = sessionHelper.GetSession("arlListWsc")
        For Each item As WSCEvidenceBB In arlListWscDownload
            item.Status = enumStatusWSC.Status.Selesai
            ObjWSCEvidanceFacade.Update(item)
        Next
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim _fileHelper As New FileHelper
        Dim fileInfo1 As New FileInfo(Server.MapPath(""))
        Try
            RetriveDownload()
            ChangeStatus()
            Dim str As FileInfo = _fileHelper.TransferWSCBBtoSAP(arlListWscDownload, fileInfo1)
            txtDownload.Text = KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceBBFileDirectory").ToString & "\" & str.Name
            BindToDataGrid(dtgEvidence.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show("Gagal Download File.")
        End Try
    End Sub

#End Region

End Class