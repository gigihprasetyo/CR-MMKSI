#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#End Region



Public Class FrmSalesmanPartEntryResign
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents icResignDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtResignReason As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents dgSalesmanHeader As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblText As System.Web.UI.WebControls.Label
    Protected WithEvents lblsemicolon As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategory As System.Web.UI.WebControls.Label
    Protected WithEvents lblPosition As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSalesmanCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblShowSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlResign As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblResign As System.Web.UI.WebControls.Label
    Protected WithEvents Requiredfieldvalidator7 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SalesmanHeaderFacade As New SalesmanHeaderFacade(User)
    Private _viewPriv As Boolean
    Private _downloadPriv As Boolean
    Private sessHelper As New SessionHelper
    Private strDefDate As String = "1753/01/01"

#End Region

#Region "PrivateCustomMethods"
    ' penambahan untuk initialize data
    Private Sub ClearData()
        lblName.Text = String.Empty
        lblCategory.Text = String.Empty
        lblPosition.Text = String.Empty
        txtSalesmanCode.Text = String.Empty
        icResignDate.Value = Now
        txtResignReason.Text = String.Empty
        icResignDate.Enabled = False
        icResignDate.Value = Now
        'txtName.ReadOnly = False
        'txtPosition.ReadOnly = False
        btnSimpan.Enabled = True
        If dgSalesmanHeader.Items.Count > 0 Then
            dgSalesmanHeader.SelectedIndex = -1
        End If
        ViewState.Add("vsProcess", "Edit")
    End Sub
    ' untuk update data yg sdh ada sebelumnya
    Private Sub Update()
        If Not IsNothing(sessHelper.GetSession("vsSalesmanHeader")) Then
            Dim objSalesmanHeader As SalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader)
            objSalesmanHeader.ResignDate = icResignDate.Value
            objSalesmanHeader.ResignReason = txtResignReason.Text
            objSalesmanHeader.ResignType = ddlResign.SelectedValue
            objSalesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String)

            Dim nResult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
            If nResult < 0 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                UpdateSalesmanAdditionalInfo(objSalesmanHeader)
                'UpdateSalesmanPartHistory(objSalesmanHeader)
                'SaveSalesmanPartHistory(objSalesmanHeader, HistoryStatus.Resign)
                MessageBox.Show(SR.UpdateSucces)
                ViewState.Add("vsProcess", "Default")
                'SendEmail(objSalesmanHeader, True)
            End If
        Else
            MessageBox.Show(SR.UpdateFail)
        End If
    End Sub

    Private Sub Insert()
        If Not IsNothing(sessHelper.GetSession("vsSalesmanHeader")) Then
            Dim objSalesmanHeader As SalesmanHeader = CType(sessHelper.GetSession("vsSalesmanHeader"), SalesmanHeader)
            objSalesmanHeader.ResignDate = icResignDate.Value
            objSalesmanHeader.ResignReason = txtResignReason.Text
            objSalesmanHeader.ResignType = ddlResign.SelectedValue
            objSalesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String)

            Dim nResult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
            If nResult < 0 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                UpdateSalesmanAdditionalInfo(objSalesmanHeader)
                UpdateSalesmanPartHistory(objSalesmanHeader)
                SaveSalesmanPartHistory(objSalesmanHeader, HistoryStatus.Resign)
                MessageBox.Show(SR.UpdateSucces)
                ViewState.Add("vsProcess", "Default")
                SendEmail(objSalesmanHeader, True)
            End If
        Else
            MessageBox.Show(SR.UpdateFail)
        End If
    End Sub

    Private Sub UpdateSalesmanAdditionalInfo(ByVal objSalesmanHeader As SalesmanHeader)
        Try
            Dim oSAIFacade As SalesmanAdditionalInfoFacade = New SalesmanAdditionalInfoFacade(User)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", MatchType.Exact, objSalesmanHeader.ID))

            Dim arrList As ArrayList = New SalesmanAdditionalInfoFacade(User).Retrieve(criterias)
            For Each oSAI As SalesmanAdditionalInfo In arrList
                oSAI.RowStatus = DBRowStatus.Deleted
                oSAIFacade.Update(oSAI)
            Next
        Catch ex As Exception
            'MessageBox.Show("Update historikal gagal")
        End Try
    End Sub

    Private Sub UpdateSalesmanPartHistory(ByVal salesmanHeader As SalesmanHeader)
        Try
            Dim oSalesHistoryFacade As SalesmanPartHistoryFacade = New SalesmanPartHistoryFacade(User)

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanPartHistory), "SalesmanHeader.ID", MatchType.Exact, salesmanHeader.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanPartHistory), "Dealer.ID", MatchType.Exact, salesmanHeader.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanPartHistory), "Status", MatchType.Exact, 1)) ' --pengajuan

            Dim arrList As ArrayList = oSalesHistoryFacade.Retrieve(criterias)
            For Each oSPH As SalesmanPartHistory In arrList
                oSPH.Status = HistoryStatus.Dibatalkan
                oSalesHistoryFacade.Update(oSPH)
            Next
        Catch ex As Exception
            'MessageBox.Show("Update historikal gagal")
        End Try

    End Sub

    Private Sub SaveSalesmanPartHistory(ByVal salesmanHeader As SalesmanHeader, ByVal status As HistoryStatus)
        Dim oSalesHistoryFacade As SalesmanPartHistoryFacade = New SalesmanPartHistoryFacade(User)
        Try

            Dim oSalesHistory As SalesmanPartHistory

            oSalesHistory = New SalesmanPartHistory
            oSalesHistory.SalesmanCode = salesmanHeader.SalesmanCode
            oSalesHistory.ChangedDate = Date.Now
            oSalesHistory.Status = status
            oSalesHistory.Dealer = salesmanHeader.Dealer
            oSalesHistory.SalesmanHeader = salesmanHeader
            Dim arlSai As ArrayList = salesmanHeader.SalesmanAdditionalInfo
            If arlSai.Count > 0 Then
                Dim oSai As SalesmanAdditionalInfo = CType(arlSai(0), SalesmanAdditionalInfo)
                oSalesHistory.SalesmanCategoryLevel = oSai.SalesmanCategoryLevel
                oSalesHistory.SalesmanLevel = oSai.SalesmanLevel
            Else
                oSalesHistory.SalesmanCategoryLevel = Nothing
                oSalesHistory.SalesmanLevel = Nothing
            End If

            oSalesHistoryFacade.Insert(oSalesHistory)
        Catch ex As Exception
            'MessageBox.Show("Update historikal gagal")
        End Try

    End Sub

    Private Sub Initialize()
        ClearData()
        ViewState("CurrentSortColumn") = "SalesmanCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ViewState.Add("vsProcess", "Default")

    End Sub

    Private Sub SendEmail(ByVal objSalesmanHeader As SalesmanHeader, ByVal bStatus As Boolean) ' bStatus = New (true) or Update(false) Employee
        Dim appConfigFacade As New AppConfigFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailFrom As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_FROM_ASS).Value
        Dim emailTo As String = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_TO_SP_ADMIN).Value
        Dim urlPartEmpList As String = KTB.DNet.Lib.WebConfig.GetValue("UrlPartEmpList")

        Dim valueEmail As String
        If bStatus Then
            valueEmail = GenerateEmailEmployeeResign(objSalesmanHeader, urlPartEmpList)
            ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet] Parts - Employee Part Resign", Mail.MailFormat.Html, valueEmail)
        End If

    End Sub

    Private Function GenerateEmailEmployeeResign(ByVal objSalesmanHeader As SalesmanHeader, ByVal urlResign As String) As String

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder("")
        sb.Append("<FONT face=Arial size=1>")
        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 align=center><b>Part Employee Resign</b></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50></td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=50>")
        sb.Append("Dengan hormat,&nbsp;")
        sb.Append("<br><br>Berikut data Part Employee yang resign :")
        sb.Append("</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td colspan=5 height=10></td>")
        sb.Append("</tr>")
        sb.Append("</table>")

        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")

        sb.Append("<tr>")
        sb.Append("<td width='35%'>Dealer</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.Dealer.DealerCode & " / " & objSalesmanHeader.Dealer.SearchTerm2 & "</b></td>")
        sb.Append("</tr>")

        sb.Append("<tr>")
        sb.Append("<td width='35%'>Nama</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.Name & "</b></td>")
        sb.Append("</tr>")

        sb.Append("<tr>")
        sb.Append("<td width='35%'>Kategori</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName & "</b></td>")
        sb.Append("</tr>")

        sb.Append("<tr>")
        sb.Append("<td width='35%'>Posisi</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.PositionName & "</b></td>")
        sb.Append("</tr>")

        Dim EnumLevel As EnumSalesmanPartLevel.Level = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanLevel
        If EnumLevel <> 99 Then
            sb.Append("<tr>")
            sb.Append("<td width='35%'>Level</td>")
            sb.Append("<td width='5%'>:</td>")
            sb.Append("<td width='60%'><b>" & EnumLevel.ToString & "</b></td>")
            sb.Append("</tr>")
        End If

        sb.Append("<tr>")
        sb.Append("<td width='35%'>Tanggal Masuk</td>")
        sb.Append("<td width='5%'>:</td>")
        sb.Append("<td width='60%'><b>" & objSalesmanHeader.HireDate.ToString("dd MMM yyyy") & "</b></td>")
        sb.Append("</tr>")

        sb.Append("</table>")

        sb.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>Untuk data Employee Part diatas, dapat diakses pada link dibawah ini :</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'>&nbsp;</td>")
        sb.Append("</tr>")
        sb.Append("<tr>")
        sb.Append("<td width='100%'><font color='blue'><a href='" & urlResign & "'>MMKSI DNet Sparepart</a></font></td>")
        sb.Append("</tr>")
        sb.Append("</table>")
        sb.Append("</FONT>")

        Return sb.ToString

    End Function

    Private Sub BindControlsAttribute()
        'lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblShowSalesman.Attributes("onclick") = "ShowSalesmanSelection();"
    End Sub
    ' penambahan untuk delete data
    Private Sub Delete(ByVal nID As Integer)
        ' melakukan update, pembatalan resign
        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(nID)
        If Not objSalesmanHeader Is Nothing Then
            objSalesmanHeader.ResignDate = Date.Parse(strDefDate)
            objSalesmanHeader.ResignReason = String.Empty
            Dim nResult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
            If nResult < 0 Then
                MessageBox.Show("Status Pembatalan Resign Salesman gagal")
            Else
                MessageBox.Show("Status Pembatalan Resign Salesman telah dibatalkan")
            End If
        End If
        BindDataGrid(0)
    End Sub
    ' penambahan untuk view data
    Private Sub View(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(nID)
        sessHelper.SetSession("vsSalesmanHeader", objSalesmanHeader)
        txtSalesmanCode.Text = objSalesmanHeader.SalesmanCode
        lblName.Text = objSalesmanHeader.Name
        If objSalesmanHeader.SalesmanAdditionalInfo.Count > 0 Then
            lblCategory.Text = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName
            lblPosition.Text = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.PositionName
        Else
            lblCategory.Text = String.Empty
            lblPosition.Text = String.Empty
        End If
        'lblPosition.Text = objSalesmanHeader.JobPosition.Code
        icResignDate.Value = objSalesmanHeader.ResignDate
        txtResignReason.Text = objSalesmanHeader.ResignReason
        If Not IsNothing(objSalesmanHeader.ResignType) Then
            ddlResign.SelectedValue = objSalesmanHeader.ResignType
        End If
        icResignDate.Enabled = EditStatus
        Me.btnSimpan.Enabled = EditStatus
    End Sub
    Private Sub SetPageTitle()
        lblPageTitle.Text = "PART EMPLOYEE - Pengunduran Diri"
    End Sub
    Private Sub EnableControl()
        If Not IsNothing(Request.QueryString("From")) Then
            btnSimpan.Visible = False
            btnBatal.Visible = False
        Else
            btnSimpan.Visible = True
            btnBatal.Visible = True
        End If
        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            btnBack.Visible = False
        Else
            ' dari KTB, lihat saja dari view data rekap turn over
            btnBack.Visible = True
        End If

        If Not IsNothing(sessHelper.GetSession("mode")) Then
            If sessHelper.GetSession("mode") = "View" Then
                txtSalesmanCode.Visible = True
                lblText.Visible = True
                lblsemicolon.Visible = True
                lblKodeDealer.Visible = True

                Dim crits As Hashtable
                crits = CType(sessHelper.GetSession("TO"), Hashtable)
                If Not IsNothing(crits) Then
                    lblKodeDealer.Text = CType(crits.Item("DealerCode"), String)
                End If

                'lblName.Enabled = False
                'lblPosition.Enabled = False
                txtResignReason.Enabled = False
                icResignDate.Enabled = False
            End If
        Else
            lblText.Visible = False
            lblsemicolon.Visible = False
            lblKodeDealer.Visible = False
            txtSalesmanCode.Enabled = True
            lblName.Enabled = True
            lblPosition.Enabled = True
            txtResignReason.Enabled = True
            icResignDate.Enabled = True
        End If
    End Sub

    ' Untuk bind data yg bersangkutan - related
    Private Sub BindDropDownLists()
        Dim strDealer As String
        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            strDealer = objDealer.DealerCode
        Else
            ' untuk KTB bs akses semua data
            strDealer = ""
        End If

    End Sub

    Private Sub BindResignType()
        ddlResign.Items.Clear()
        ddlResign.Items.Add(New ListItem("Silahkan Pilih", 0))
        ddlResign.Items.Add(New ListItem("Resign Dari Dealer", 1))
        ddlResign.Items.Add(New ListItem("Mutasi Ke Departemen Lain", 2))
        ddlResign.SelectedIndex = 0
    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.Sparepart, Byte)))
        criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.Exact, CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, Byte)))

        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")

        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            ' ambil berdasarkan dealer yg login
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then

            'Bugs 100006
            Dim crits As Hashtable
            crits = CType(sessHelper.GetSession("TO"), Hashtable)
            If Not IsNothing(crits) Then

                Dim icStartDate, icEndDate As Date

                icStartDate = CType(crits.Item("CreatedTimeGreaterOrEqual"), Date)
                icEndDate = CType(crits.Item("CreatedTimeLesserOrEqual"), Date)

                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, CType(crits.Item("DealerCode"), String)))
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.GreaterOrEqual, icStartDate))
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "HireDate", MatchType.LesserOrEqual, icEndDate))
                criterias.opAnd(New Criteria(GetType(SalesmanHeader), "ResignDate", MatchType.No, Date.Parse(strDefDate)))
            End If
        End If

        arrList = _SalesmanHeaderFacade.RetrieveByCriteria(criterias, idxPage + 1, dgSalesmanHeader.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSalesmanHeader.DataSource = arrList
        dgSalesmanHeader.VirtualItemCount = totalRow
        dgSalesmanHeader.DataBind()

    End Sub

    Private Function newValidation(objSalesmanHeader As SalesmanHeader) As Boolean
        If ddlResign.SelectedIndex = 0 Then
            MessageBox.Show("Tipe resign harus diisi !")
            Return False
        End If
        If txtResignReason.Text.Trim = "" Then
            MessageBox.Show("Alasan keluar harus diisi !")
            Return False
        End If
        If objSalesmanHeader.HireDate > CDate(icResignDate.Value) Then
            MessageBox.Show("Tanggal keluar tidak boleh lebih kecil dari tanggal masuk")
            Return False
        End If
        Return True
    End Function
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CheckPrivilege()
        _viewPriv = checkViewDetailPriv()
        _downloadPriv = checkDownloadPriv()
        If Not IsPostBack Then
            Initialize()
            BindDropDownLists()
            BindResignType()
            BindDataGrid(0)
            SetPageTitle()
            EnableControl()
            BindControlsAttribute()
        End If
        txtSalesmanCode.Attributes.Add("readonly", "readonly")
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeader
        Dim objSalesmanHeaderFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
        Dim nResult As Integer = -1

        objSalesmanHeader = objSalesmanHeaderFacade.Retrieve(txtSalesmanCode.Text.Trim)

        If Not newValidation(objSalesmanHeader) Then
            Return
        End If

        sessHelper.SetSession("vsSalesmanHeader", objSalesmanHeader)

        If CType(ViewState("vsProcess"), String) = "Edit" Then
            If Not txtSalesmanCode.Text = String.Empty Then
                Update()
                btnSimpan.Enabled = False
            Else
                MessageBox.Show("Salesman harus teregister terlebih dahulu!")
                Exit Sub
            End If
        Else
            If Not txtSalesmanCode.Text = String.Empty Then
                Insert()
                btnSimpan.Enabled = False
            Else
                MessageBox.Show("Salesman harus teregister terlebih dahulu!")
                Exit Sub
            End If
        End If

        'Page_Load(sender, Nothing)
        ClearData()
        dgSalesmanHeader.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanHeader.CurrentPageIndex)
        ViewState.Add("vsProcess", "Default")
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        txtResignReason.ReadOnly = False
    End Sub
    Private Sub dgSalesmanHeader_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanHeader.SortCommand
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
        dgSalesmanHeader.SelectedIndex = -1
        dgSalesmanHeader.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanHeader.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanHeader_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanHeader.PageIndexChanged
        dgSalesmanHeader.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanHeader.CurrentPageIndex)

        Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(1)
        sessHelper.SetSession("SessionImage", objSalesmanHeader)
    End Sub
    Private Sub dgSalesmanHeader_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanHeader.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            View(e.Item.Cells(0).Text, False)
            icResignDate.Enabled = False
            txtResignReason.ReadOnly = True

        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            View(e.Item.Cells(0).Text, True)
            dgSalesmanHeader.SelectedIndex = e.Item.ItemIndex
            txtResignReason.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            Delete(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub
    Private Sub dgSalesmanHeader_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanHeader.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanHeader As SalesmanHeader = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSalesmanHeader.CurrentPageIndex * dgSalesmanHeader.PageSize)

            Dim lblPosisiNew As Label = CType(e.Item.FindControl("lblPosisi"), Label)
            If objSalesmanHeader.SalesmanAdditionalInfo.Count > 0 Then
                lblPosisiNew.Text = CType(objSalesmanHeader.SalesmanAdditionalInfo(0), SalesmanAdditionalInfo).SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName
            Else
                lblPosisiNew.Text = String.Empty
            End If

            Dim lblResignDateNew As Label = CType(e.Item.FindControl("lblResignDate"), Label)
            lblResignDateNew.Text = objSalesmanHeader.ResignDate.ToString("dd/MM/yyyy")

            Dim lblResignType As Label = CType(e.Item.FindControl("lblResignType"), Label)
            Select Case objSalesmanHeader.ResignType
                Case 0
                    lblResignType.Text = String.Empty
                Case 1
                    lblResignType.Text = "Resign Dari Dealer"
                Case 2
                    lblResignType.Text = "Mutasi Ke Departemen Lain"
            End Select

            ' yg aktif, dibuatkan disable labelnya - belum resign, related bug 695
            If objSalesmanHeader.ResignDate = Date.Parse(strDefDate) Then
                lblResignDateNew.Visible = False
            Else
                lblResignDateNew.Visible = True
            End If

            Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            If _viewPriv Then
                lbtnView.Visible = True
            Else
                lbtnView.Visible = False
            End If
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            If objSalesmanHeader.ResignDate < New Date(1900, 1, 1) Then
                lbtnEdit.Visible = False
            End If
        End If
        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dibatalkan status resignnya?');")
        End If

        If Not IsNothing(Request.QueryString("From")) Then
            e.Item.Cells(6).Visible = False
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../Salesman/FrmSalesmanTurnOver.aspx")
    End Sub

#End Region

#Region "Privilege"

    Private Sub CheckPrivilege()

        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            If Not SecurityProvider.Authorize(context.User, SR.Input_data_salesman_part_privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Tenaga Penjual - Buat Data Pengunduran Diri")
            End If

        End If



    End Sub

    Private Function checkViewDetailPriv() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.Input_data_salesman_part_privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function checkDownloadPriv() As Boolean
        If Not SecurityProvider.Authorize(Context.User, SR.Input_data_salesman_part_privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region "Internal Enum"
    Private Enum HistoryStatus
        Baru = 0
        Pengajuan = 1
        Disetujui = 2
        Ditolak = 3
        Dibatalkan = 4
        Resign = 5
    End Enum

#End Region

End Class

