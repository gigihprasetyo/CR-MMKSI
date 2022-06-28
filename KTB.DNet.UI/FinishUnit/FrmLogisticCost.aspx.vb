Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.IO
Imports System.Text
Imports System.Linq
Imports System.Collections
Imports KTB.DNet.BusinessValidation.Helpers


Public Class FrmLogisticCost
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgLogisticFee As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtGenerate As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGenerate As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblGenerate As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ICDari As KTB.DNET.WebCC.IntiCalendar
    Protected WithEvents ICSampai As KTB.DNET.WebCC.IntiCalendar
    Protected WithEvents chkCheckAll As HiddenField
    Protected WithEvents lblTotalBiaya As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPph As System.Web.UI.WebControls.Label

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
    Dim sHelper As New SessionHelper
    Private arlLogisticFee As ArrayList = New ArrayList
    Private arlLogisticFeeFilter As ArrayList = New ArrayList
    Private objDealer As Dealer
    Private IsLockPeriod As Boolean
    Private LockPeriodFrom As DateTime
    Private LockPeriodTo As DateTime
    Private objStdCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
#End Region

#Region "Event"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        objDealer = sHelper.GetSession("DEALER")
        InitiateAuthorization()
        InitLockPeriod()
        If Not IsPostBack Then
            Initialize()
            ViewState("currentSortColumn") = "Dealer.DealerCode"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtKodeDealer.Attributes.Add("readonly", "readonly")
            End If

        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        LogisticFeeBind(0)
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click

        Dim objPFFacade As LogisticFeeFacade = New LogisticFeeFacade(User)
        Dim idPF As String = ""

        If txtGenerate.Text.Trim = "" Then
            MessageBox.Show("Nomor Bukti Potong tidak boleh kosong")
            Exit Sub
        End If



        Dim strCompanyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim ObjCatF As New ProductCategoryFacade(User)
        Dim isAll As Boolean = False
        Dim arlPF As New ArrayList
        Dim criterias As CriteriaComposite

        If chkCheckAll.Value.ToString().ToUpper() = "TRUE" AndAlso Not IsNothing(sHelper.GetSession("CritLogisticFee")) Then
            criterias = CType(sHelper.GetSession("CritLogisticFee"), CriteriaComposite)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.LogisticFee), "Status", MatchType.Exact, CInt(EnumLogisticFeeStatus.LogisticFeeStatus.Baru)))
        Else


            For Each item As DataGridItem In dtgLogisticFee.Items
                Dim chk As CheckBox = item.FindControl("cbCheck")
                If chk.Checked Then
                    Dim objLogisticFee As New LogisticFee()
                    Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                    Dim lblStatus As Label = CType(item.FindControl("lblStatus"), Label)
                    Dim lblDealerCode As Label = CType(item.FindControl("lblDealerCode"), Label)

                    If lblStatus.Text <> "Baru" Then
                        MessageBox.Show("Status harus baru")
                        Exit Sub
                    End If

                    idPF = idPF & lblID.Text.ToString & ","

                End If
            Next

            If idPF = "" Then
                MessageBox.Show("Pilih Data Terlebih Dulu")
                Exit Sub
            End If

            idPF = Left(idPF, idPF.Length - 1)
            criterias = New CriteriaComposite(New Criteria(GetType(LogisticFee), "ID", MatchType.InSet, "(" & idPF & ")"))

        End If

      
        If 1 = 0 Then
            MessageBox.Show("Pilih Data Terlebih Dulu")
            Exit Sub
        Else
            'Try

            arlPF = objPFFacade.Retrieve(criterias)
            If arlPF.Count > 0 Then
                Dim monthyear As String = CType(arlPF(0), LogisticFee).LogisticDN.BillingDate.Month.ToString() + CType(arlPF(0), LogisticFee).LogisticDN.BillingDate.Year.ToString()
                Dim creditAccount As String = CType(arlPF(0), LogisticFee).Dealer.CreditAccount
                Dim ObjMMC As Integer = 0
                Dim ObjMFTBC As Integer = 0
                If arlPF.Count > 0 Then
                    For Each pf As LogisticFee In arlPF

                        If (pf.LogisticDN.BillingDate.Month.ToString() + pf.LogisticDN.BillingDate.Year.ToString()) <> monthyear Then
                            MessageBox.Show("Data Logistic Fee harus dalam bulan yang sama")
                            Exit Sub
                        End If

                        If String.IsNullOrEmpty(pf.FakturPajakNo) Then
                            MessageBox.Show("No Faktur Pajak tidak boleh kosong")
                            Exit Sub
                        End If

                        If pf.Dealer.CreditAccount <> creditAccount Then
                            MessageBox.Show("Data Logistic Fee harus dalam satu Credit Account")
                            Exit Sub
                        End If
                    Next
                End If

                Dim objLogisticPPHHeaderFacade As LogisticPPHHeaderFacade = New LogisticPPHHeaderFacade(User)
                Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(DateTime.Now.Date, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_2").ValueId)
                If objLogisticPPHHeaderFacade.GenerateNoRegBuktiPotongPPh(arlPF, txtGenerate.Text.Trim, pph) = 1 Then
                    MessageBox.Show("Generate No. Bukti Potong PPh Dengan No " & txtGenerate.Text.Trim & " Berhasil")
                    txtGenerate.Text = ""
                    LogisticFeeBind(dtgLogisticFee.CurrentPageIndex)
                Else
                    MessageBox.Show("Generate No. Bukti Potong PPh Dengan No " & txtGenerate.Text.Trim & " Gagal !")
                End If

            Else
                MessageBox.Show("Tidak Ada Data Dengan Status Baru ")

            End If

            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            'End Try

        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim arlPF As New ArrayList
        arlPF = PopulateLogisticFee()
        If arlPF.Count = 0 Then
            arlPF = CType(sHelper.GetSession("arlLogisticFee"), ArrayList)
        End If
        DoDownload(arlPF)
    End Sub

    Private Sub dtgLogisticFee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgLogisticFee.ItemDataBound
        If Not (arlLogisticFee Is Nothing) Then
            If Not (arlLogisticFee.Count = 0 Or e.Item.ItemIndex = -1) Then
                Dim RowValue As LogisticFee = CType(e.Item.DataItem, LogisticFee)
                Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(DateTime.Now.Date, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_2").ValueId)

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (dtgLogisticFee.CurrentPageIndex * dtgLogisticFee.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                lblStatus.Text = EnumLogisticFeeStatus.GetStringValue(RowValue.Status)

                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                lblDealerCode.Text = RowValue.Dealer.DealerCode
                lblDealerCode.ToolTip = RowValue.Dealer.DealerName

                Dim lblCreditAccount As Label = CType(e.Item.FindControl("lblCreditAccount"), Label)
                lblCreditAccount.Text = RowValue.Dealer.CreditAccount

                Dim lblTglInvoice As Label = CType(e.Item.FindControl("lblTglInvoice"), Label)
                lblTglInvoice.Text = RowValue.LogisticDN.BillingDate

                Dim lblPPH As Label = CType(e.Item.FindControl("lblPPH"), Label)
                'lblPPH.Text = CType(RowValue.Amount * 0.02, Decimal).ToString("#,###")
                lblPPH.Text = CType(CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, RowValue.Amount), Decimal).ToString("#,###")

                Dim lblDebitCharge As Label = CType(e.Item.FindControl("lblDebitCharge"), Label)
                lblDebitCharge.Text = RowValue.LogisticDN.LogisticDCHeader.DebitChargeNo

                Dim lblDebitMemo As Label = CType(e.Item.FindControl("lblDebitMemo"), Label)
                lblDebitMemo.Text = RowValue.LogisticDN.DebitMemoNo

                Dim lblFakturPajak As Label = CType(e.Item.FindControl("lblFakturPajak"), Label)
                lblFakturPajak.Text = RowValue.FakturPajakNo

                Dim lnkDebitCharge As LinkButton = CType(e.Item.FindControl("lnkDebitCharge"), LinkButton)
                If Not IsNothing(RowValue.FileNameLogistic) And RowValue.FileNameLogistic.ToString <> "" Then
                    lnkDebitCharge.Text = "<img src=""../images/download.gif"" border=""0""/> "
                Else
                    lnkDebitCharge.Text = String.Empty
                End If

                Dim lnkDebitMemo As LinkButton = CType(e.Item.FindControl("lnkDebitMemo"), LinkButton)
                If Not IsNothing(RowValue.FileNameDebitMemo) And RowValue.FileNameDebitMemo.ToString <> "" Then
                    lnkDebitMemo.Text = "<img src=""../images/download.gif"" border=""0""/> "
                Else
                    lnkDebitMemo.Text = String.Empty
                End If

                'Dim lblBuktiPotong As Label = CType(e.Item.FindControl("lblBuktiPotong"), Label)
                'lblBuktiPotong.ToolTip = "Bukti Potong"
                'lblBuktiPotong.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpBuktiPemotonganPPH.aspx?id=" & RowValue.ID, "scrollbars=auto", 800, 800, "DealerSelection")

                Dim lbtnHistory As LinkButton = CType(e.Item.FindControl("lbtnHistory"), LinkButton)
                lbtnHistory.Attributes("OnClick") = "showPopUp('../PopUp/PopUpHistoryLogisticFee.aspx?Id=" & RowValue.ID & " ','',500,760,'');"
                lbtnHistory.ToolTip = "Lihat History"

                If IsLockPeriod Then
                    Dim cbCheck As CheckBox = e.Item.FindControl("cbCheck")
                    If Not IsNothing(cbCheck) AndAlso Not IsNothing(RowValue.LogisticDN) AndAlso Not IsNothing(RowValue.LogisticDN.SalesOrder) AndAlso Not IsNothing(RowValue.LogisticDN.SalesOrder.POHeader) Then
                        Dim Top As Date = RowValue.LogisticDN.BillingDate.AddDays(RowValue.LogisticDN.SalesOrder.POHeader.TermOfPayment.TermOfPaymentValue)

                        Top = CommonFunction.AddNWorkingDay(Top, 1, False)

                        If (RowValue.LogisticDN.BillingDate >= LockPeriodFrom AndAlso RowValue.LogisticDN.BillingDate <= LockPeriodTo) AndAlso (Top <= LockPeriodTo) Then
                            cbCheck.Enabled = False
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub dtgLogisticFee_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgLogisticFee.ItemCommand
        Select Case (e.CommandName)
            Case "lnkDebitCharge"
                Dim linkButton As LinkButton = e.Item.FindControl("lnkDebitCharge")

                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("LogisticDestFileDirectory")
                Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
                Dim DestFullFilePath As String = fileInfo1.Directory.FullName & "\" & DestFile
                Dim dataFile As String = DestFullFilePath & "\" & e.Item.Cells(12).Text
                Dim fileInfox As New FileInfo(dataFile)

                Dim fileExist As Boolean = CheckFileExist(fileInfox)
                If fileExist Then
                    Try
                        Response.Redirect("../Download.aspx?file=" & dataFile)
                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail(linkButton.Text))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfox.Name))
                End If

            Case "lnkDebitMemo"
                Dim linkButton As LinkButton = e.Item.FindControl("lnkDebitMemo")

                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("LogisticDestFileDirectory")
                Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
                Dim DestFullFilePath As String = fileInfo1.Directory.FullName & "\" & DestFile
                Dim dataFile As String = DestFullFilePath & "\" & e.Item.Cells(13).Text
                Dim fileInfox As New FileInfo(dataFile)

                Dim fileExist As Boolean = CheckFileExist(fileInfox)
                If fileExist Then
                    Try
                        Response.Redirect("../Download.aspx?file=" & dataFile)
                    Catch ex As Exception
                        MessageBox.Show(SR.DownloadFail(linkButton.Text))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfox.Name))
                End If

                'Case "History"

            Case "Delete"
                DeleteLogisticFee(CInt(e.CommandArgument))

        End Select
    End Sub


    Private Sub DeleteLogisticFee(ByVal nID As Integer)
        Try
            Dim nresult As Integer = 0
            Dim oPFFacade As New LogisticFeeFacade(User)
            Dim oPF As LogisticFee = oPFFacade.Retrieve(nID)
            oPF.RowStatus = CType(DBRowStatus.Deleted, Short)
            nresult = oPFFacade.Update(oPF)
            If nresult > 0 Then
                LogisticFeeBind(0)
            Else
                MessageBox.Show(SR.DeleteFail)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

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

    Private Sub dtgLogisticFee_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgLogisticFee.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dtgLogisticFee.SelectedIndex = -1
        dtgLogisticFee.CurrentPageIndex = 0
        LogisticFeeBind(dtgLogisticFee.CurrentPageIndex)
    End Sub

    Private Sub dtgLogisticFee_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgLogisticFee.PageIndexChanged
        dtgLogisticFee.CurrentPageIndex = e.NewPageIndex
        LogisticFeeBind(dtgLogisticFee.CurrentPageIndex)
        chkCheckAll.Value = "False"
    End Sub

#End Region

#Region "Custom"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.Logistic_cost_lihat_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Logistic Fee")
        End If
    End Sub
    Private Sub Initialize()
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")
        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnDownload.Enabled = False
        btnGenerate.Enabled = False
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            'txtKodeDealer.ReadOnly = True
            txtKodeDealer.Attributes.Add("readonly", "readonly")
            lblGenerate.Visible = SecurityProvider.Authorize(Context.User, SR.Logistic_cost_generate_bukti_potong_privilege) 'True '
            txtGenerate.Visible = SecurityProvider.Authorize(Context.User, SR.Logistic_cost_generate_bukti_potong_privilege) 'True '
            btnGenerate.Visible = SecurityProvider.Authorize(Context.User, SR.Logistic_cost_generate_bukti_potong_privilege) 'True '
        Else
            txtKodeDealer.ReadOnly = False
            lblGenerate.Visible = False
            txtGenerate.Visible = False
            btnGenerate.Visible = False
        End If

        BindStatus()
    End Sub

    Private Sub BindStatus()
        ddlStatus.Items.Clear()
        ddlStatus.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each item As ListItem In EnumLogisticFeeStatus.RetrieveLogisticFeeStatus()
            ddlStatus.Items.Add(item)
        Next
        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub LogisticFeeBind(ByVal idxPage As Integer)
        Dim objUserInfo As UserInfo = sHelper.GetSession("LOGINUSERINFO")
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If txtKodeDealer.Text.Trim() = String.Empty Then
                MessageBox.Show("Tentukan Dealer terlebih dahulu")
                Exit Sub
            End If
        End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNET.Domain.LogisticFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.LogisticFee), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        End If
        If ddlStatus.SelectedValue.ToString <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.LogisticFee), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If DateDiff(DateInterval.Day, CType(ICDari.Value, Date), CType(ICSampai.Value, Date)) >= 0 Then
            criterias.opAnd(New Criteria(GetType(LogisticFee), "LogisticDN.BillingDate", MatchType.GreaterOrEqual, ICDari.Value))
            criterias.opAnd(New Criteria(GetType(LogisticFee), "LogisticDN.BillingDate", MatchType.LesserOrEqual, ICSampai.Value))
        Else
            MessageBox.Show("Periode dari tidak boleh lebih besar daripada periode sampai")
            Exit Sub
        End If
        lblTotalBiaya.Text = "0"
        lblTotalPph.Text = "0"

        Dim totalRow As Integer = 0
        arlLogisticFee = New FinishUnit.LogisticFeeFacade(User).RetrieveActiveList(criterias, idxPage + 1, dtgLogisticFee.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))
        Dim arlPFDownload As ArrayList = New FinishUnit.LogisticFeeFacade(User).Retrieve(criterias)
        sHelper.SetSession("CritLogisticFee", criterias)
        If (arlLogisticFee.Count > 0) Then
            Dim varTotalAmount As Decimal = 0
            Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(DateTime.Now.Date, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_2").ValueId)
            Dim aggregates As New Aggregate(GetType(LogisticFee), "Amount", AggregateType.Sum)
 
            varTotalAmount += New LogisticFeeFacade(User).RetrieveScalar(aggregates, criterias)

            lblTotalBiaya.Text = FormatNumber(varTotalAmount, 0, , , TriState.UseDefault)
            'lblTotalPph.Text = FormatNumber((varTotalAmount * 0.02), 0, , , TriState.UseDefault)
            lblTotalPph.Text = FormatNumber((CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, PPh, varTotalAmount)), 0, , , TriState.UseDefault)

            dtgLogisticFee.DataSource = arlLogisticFee
            dtgLogisticFee.VirtualItemCount = totalRow
            dtgLogisticFee.DataBind()
            sHelper.SetSession("arlLogisticFee", arlPFDownload)
            btnDownload.Enabled = True
            btnGenerate.Enabled = True
        Else
            dtgLogisticFee.CurrentPageIndex = 0
            dtgLogisticFee.DataSource = Nothing
            dtgLogisticFee.VirtualItemCount = 0
            dtgLogisticFee.DataBind()
            btnDownload.Enabled = False
            btnGenerate.Enabled = False
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Function PopulateLogisticFee() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objPFFacade As New FinishUnit.LogisticFeeFacade(User)

        For Each oDataGridItem In dtgLogisticFee.Items
            If oDataGridItem.ItemType = ListItemType.Item Or oDataGridItem.ItemType = ListItemType.AlternatingItem Then
                chkExport = oDataGridItem.FindControl("cbCheck")
                Dim id As Integer = 0
                If chkExport.Checked Then
                    Dim _pf As New KTB.DNet.Domain.LogisticFee
                    Dim lblID As Label = CType(oDataGridItem.FindControl("lblID"), Label)
                    id = CType(lblID.Text, Integer)
                    _pf = objPFFacade.Retrieve(id)
                    If Not IsNothing(_pf) AndAlso _pf.ID > 0 Then
                        oExArgs.Add(_pf)
                    End If
                End If
            End If
        Next
        Return oExArgs
    End Function

    Private Sub DoDownload(ByVal arlPF As ArrayList)
        Dim sFileName As String
        sFileName = "Daftar Logistic Fee [" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "]"

        '-- Temp file must be a randomly named file!
        Dim LogisticFeeFile As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNET.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNET.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNET.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(LogisticFeeFile)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(LogisticFeeFile, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteDataToExcell(sw, arlPF)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub WriteDataToExcell(ByVal sw As StreamWriter, ByVal arlPF As ArrayList)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("DO STATUS - LOGISTIC COST")
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        If (arlPF.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("N0" & tab)
            itemLine.Append("STATUS" & tab)
            itemLine.Append("KODE DEALER" & tab)
            itemLine.Append("NAMA DEALER" & tab)
            itemLine.Append("GROUP DEALER" & tab)
            itemLine.Append("CREDIT ACCOUNT" & tab)
            itemLine.Append("TANGGAL INVOICE" & tab)
            itemLine.Append("DEBIT CHARGE" & tab)
            itemLine.Append("DEBIT MEMO" & tab)
            itemLine.Append("FAKTUR PAJAK" & tab)
            itemLine.Append("TOTAL BIAYA" & tab)
            itemLine.Append("PPH" & tab)

            sw.WriteLine(itemLine.ToString())
            Dim i As Integer = 1
            Try
                Dim pph As Decimal = CalcHelper.GetPPNMasterByTaxTypeId(DateTime.Now.Date, objStdCodeFacade.GetByCategoryValueCode("TaxType", "PPH23_2").ValueId)
                For Each item As LogisticFee In arlPF
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(CType(CInt(item.Status), EnumLogisticFeeStatus.LogisticFeeStatus).ToString & tab)
                    itemLine.Append(item.Dealer.DealerCode & tab)
                    itemLine.Append(item.Dealer.DealerName & tab)
                    itemLine.Append(item.Dealer.DealerGroup.GroupName & tab)
                    itemLine.Append(item.Dealer.CreditAccount & tab)

                    If Not IsNothing(item.LogisticDN) Then
                        itemLine.Append(item.LogisticDN.BillingDate & tab)
                        itemLine.Append(item.LogisticDN.LogisticDCHeader.DebitChargeNo & tab)
                        itemLine.Append(item.LogisticDN.DebitMemoNo & tab)
                    Else
                        itemLine.Append("" & tab)
                        itemLine.Append("" & tab)
                        itemLine.Append("" & tab)
                    End If
                    itemLine.Append(item.FakturPajakNo & tab)
                    itemLine.Append(Decimal.Round(item.Amount, 0) & tab)
                    'itemLine.Append(Decimal.Round(item.Amount * CDec(0.02), 0) & tab)
                    itemLine.Append(CalcHelper.PPHCalculation(CalcSourceTypeEnum.DPP, pph, item.Amount) & tab)
                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub


    Private Sub InitLockPeriod()

        Try
            Dim strVal As String = ""
            strVal = KTB.DNet.Lib.WebConfig.GetString("LockPeriodForLogisticCost")
            If strVal <> "" Then
                LockPeriodFrom = Convert.ToDateTime(strVal.Split(";")(0))
                LockPeriodTo = Convert.ToDateTime(strVal.Split(";")(1))
                IsLockPeriod = True

            Else
                IsLockPeriod = False
            End If
        Catch ex As Exception
            IsLockPeriod = False
        End Try
    End Sub

#End Region

End Class
