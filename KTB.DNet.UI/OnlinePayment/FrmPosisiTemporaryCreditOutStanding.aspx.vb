#Region "Custom Imports"

Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security

Imports System.IO
Imports System.Text
#End Region

Public Class FrmPosisiTemporaryCreditOutStanding
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblCreditAcct As System.Web.UI.WebControls.Label
    Protected WithEvents txtCreditAcct As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchCreditAcct As System.Web.UI.WebControls.Label
    Protected WithEvents lblTempKind As System.Web.UI.WebControls.Label
    Protected WithEvents txtTempKind As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTglDueDate As System.Web.UI.WebControls.Label
    Protected WithEvents icTglDueDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgCreditOutstanding As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Varialbe"
    Private sessHelper As New SessionHelper
    Private oLoginDealer As Dealer
    Private oLoginUser As UserInfo
    Private SAPConString As String
    Private _poHeaderFacade As POHeaderFacade
    Private bDownloadPriv As Boolean
#End Region

#Region "Custom Method"
#Region "Check Privilage"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.CCPembayaranLaporanTemporaryOutStandingCreditView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Credit Control - Posisi Temporary Credit Outstanding")
        End If

        bDownloadPriv = SecurityProvider.Authorize(context.User, SR.CCPembayaranLaporanTemporaryOutStandingCreditDownload_Privilege)

        btnDownload.Visible = bDownloadPriv
    End Sub

#End Region

    Private Sub ClearForm()
        txtCreditAcct.Text = ""
        txtTempKind.Text = ""
        icTglDueDate.Value = DateTime.Today
    End Sub
    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtCreditAcct.Text)
        arrLastState.Add(txtTempKind.Text)
        arrLastState.Add(icTglDueDate.Value)
        arrLastState.Add(dtgCreditOutstanding.CurrentPageIndex)
        arrLastState.Add(CType(ViewState("currSortColumn"), String))
        arrLastState.Add(CType(ViewState("currSortDirection"), Sort.SortDirection))
        sessHelper.SetSession("CREDITOUTSTANDINGSESSIONLASTSTATE", arrLastState)
    End Sub
    Private Sub GetSessionCriteria()
        Dim arrLastState As ArrayList = sessHelper.GetSession("CREDITOUTSTANDINGSESSIONLASTSTATE")
        If Not arrLastState Is Nothing Then
            txtCreditAcct.Text = arrLastState.Item(0)
            txtTempKind.Text = arrLastState.Item(1)
            icTglDueDate.Value = arrLastState.Item(2)
            dtgCreditOutstanding.CurrentPageIndex = arrLastState.Item(3)
            ViewState("currSortColumn") = arrLastState.Item(4)
            ViewState("currSortDirection") = arrLastState.Item(5)
        Else
            ViewState("currSortColumn") = "Status"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            dtgCreditOutstanding.CurrentPageIndex = 0

        End If
    End Sub
#End Region

#Region "Control Event"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        SAPConString = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionString")
        InitiateAuthorization()

        If Not IsPostBack Then
            lblSearchCreditAcct.Attributes("onclick") = "ShowPPCreditAcctSelection();"
            ClearForm()
            GetSessionCriteria()
            'BindToGrid(dtgCreditOutstanding.CurrentPageIndex, True)
            If bDownloadPriv Then
                btnDownload.Visible = False
            Else
                btnDownload.Visible = bDownloadPriv
            End If
        End If
    End Sub

    Private Function isCreditAccountValid(ByVal sListLegalStatus As String) As Boolean

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "LegalStatus", MatchType.No, String.Empty))

        Dim DistinctArl As New ArrayList
        Dim arl As ArrayList = New DealerFacade(User).RetrieveActiveList(criterias, viewstate("SortColDealer"), viewstate("SortDirDealer"))

        For Each item As Dealer In arl
            If DistinctArl.Count = 0 Then
                DistinctArl.Add(item)
            Else
                Dim isNew As Boolean = True
                For Each dItem As Dealer In DistinctArl
                    If dItem.LegalStatus = item.LegalStatus Then
                        isNew = False
                        Exit For
                    End If
                Next

                If isNew Then
                    DistinctArl.Add(item)
                End If
            End If
        Next

        Dim isValid As Boolean
        For Each sCreditAcct As String In sListLegalStatus.Split(";")
            isValid = False
            For Each ValidDealer As Dealer In DistinctArl
                If ValidDealer.LegalStatus = sCreditAcct Then
                    isValid = True
                End If
            Next

            If Not isValid Then
                Exit For
            End If
        Next

        Return isValid


    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtCreditAcct.Text <> String.Empty Then
            If Not isCreditAccountValid(txtCreditAcct.Text) Then
                MessageBox.Show("Ada credit account yg tidak valid.")
                Return
            End If
        Else
            MessageBox.Show("Silakan masukan credit account.")
            Return
        End If

        dtgCreditOutstanding.CurrentPageIndex = 0
        BindToGrid(dtgCreditOutstanding.CurrentPageIndex, True)
    End Sub
    Private Sub dtgCreditOutstanding_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCreditOutstanding.PageIndexChanged
        dtgCreditOutstanding.CurrentPageIndex = e.NewPageIndex
        'BindToGrid(dtgCreditOutstanding.CurrentPageIndex, False)

        dtgCreditOutstanding.VirtualItemCount = CType(sessHelper.GetSession("ListCROutstanding"), ArrayList).Count
        dtgCreditOutstanding.DataSource = CType(sessHelper.GetSession("ListCROutstanding"), ArrayList)
        dtgCreditOutstanding.DataBind()

    End Sub
#End Region


    ' Code yg masih pending dan blm di beresin 
    Private Sub dtgCreditOutstanding_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCreditOutstanding.SortCommand
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

        Dim sortedArl As New ArrayList
        sortedArl = CommonFunction.SortArraylist(CType(sessHelper.GetSession("ListCROutstanding"), ArrayList), GetType(SAPCreditCeiling), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessHelper.SetSession("ListCROutstanding", sortedArl)

        dtgCreditOutstanding.VirtualItemCount = sortedArl.Count
        dtgCreditOutstanding.DataSource = sortedArl
        dtgCreditOutstanding.DataBind()
    End Sub
    Private Sub dtgCreditOutstanding_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCreditOutstanding.ItemDataBound
        If e.Item.ItemIndex >= 0 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1) + (dtgCreditOutstanding.CurrentPageIndex * dtgCreditOutstanding.PageSize)


        End If
    End Sub

    Sub BindToGrid(ByVal currentPageIndex As Integer, ByVal isSearch As Boolean)
        Dim total As Integer = 0
        Dim arl As New ArrayList
        _poHeaderFacade = New POHeaderFacade(User)
        arl = _poHeaderFacade.GetCreditOutstandingReportCeiling(txtCreditAcct.Text, icTglDueDate.Value, txtTempKind.Text, SAPConString, total)
        Dim newList As ArrayList = New ArrayList
        For Each item As SAPCreditCeiling In arl
            item.SelectionDate = icTglDueDate.Value
            newList.Add(item)
        Next
        sessHelper.SetSession("ListCROutstanding", newList)
        sessHelper.SetSession("ListCROutstandingDownload", newList)

        If (arl.Count > 0) Then
            dtgCreditOutstanding.VirtualItemCount = total
            dtgCreditOutstanding.DataSource = newList
            dtgCreditOutstanding.DataBind()

            If bDownloadPriv Then
                btnDownload.Visible = True
            Else
                btnDownload.Visible = bDownloadPriv
            End If
        Else
            dtgCreditOutstanding.DataSource = New ArrayList
            dtgCreditOutstanding.DataBind()
            If bDownloadPriv Then
                btnDownload.Visible = False
            Else
                btnDownload.Visible = bDownloadPriv
            End If
        End If
    End Sub
    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        DoDownload(CType(sessHelper.GetSession("ListCROutstandingDownload"), ArrayList))
    End Sub
    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "DaftarCreditOutstanding" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        '-- Temp file must be a randomly named file!
        Dim TraineeData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(TraineeData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(TraineeData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteTraineeData(sw, data)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub
    Private Sub WriteTraineeData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        'Dim sProfileConfig As String = KTB.DNet.Lib.WebConfig.GetValue("DownloadPQRFormatProfile")
        Dim header As String

        If Not IsNothing(data) Then
            '-- Write column header
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Daftar Credit Outstanding")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Credit Account" & tab)
            itemLine.Append("Temporary Kind" & tab)
            itemLine.Append("Project Name" & tab)
            itemLine.Append("No. SPL" & tab)
            itemLine.Append("Credit Ceiling" & tab)
            itemLine.Append("Amount Outstanding" & tab)
            itemLine.Append("Status" & tab)

            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As SAPCreditCeiling In data
                itemLine.Remove(0, itemLine.Length)  '-- Empty line
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.CreditAccount & tab)
                itemLine.Append(item.TemporaryKind & tab)
                itemLine.Append(item.ProjectName & tab)
                itemLine.Append(item.SPLNumber.ToString & tab)
                itemLine.Append(item.CeilingAmount.ToString & tab)
                itemLine.Append(item.TemporaryCreditExposure.ToString & tab)
                itemLine.Append(item.BlokedName & tab)
                sw.WriteLine(itemLine.ToString)
                i += 1
            Next
        End If
    End Sub
End Class
