#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Parser
Imports KTB.DNet.UI.Helper
Imports System.IO
Imports System.Text
#End Region


Public Class FrmPaymentList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents fliInput As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtCreditAccount As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchAccount As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents calStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlBank As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlCleared As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtRegNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnTransfer As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label

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
    Private _modeView As String = "ViewMode"
    Private _modeEdit As String = "EditMode"
    Private _modeUpload As String = "UploadMode"
    Private _mode As String = "Mode"
    Private _sortingField As String = "SortingField"
    Private _sortingMode As String = "SortingMode"
    Private _sessHelper As New SessionHelper
    Private _sessData As String = "DataToDisplay"
    Private _sessDataUpload As String = "UploadedData"
#End Region

#Region "Custom Methods"

    Private Sub Initialization()

        'dtgMain.Columns(8).Visible = False
        'dtgMain.Columns(9).Visible = False
        'dtgMain.Columns(11).Visible = False

        viewstate.Add(_mode, _modeView)
        viewstate.Add(Me._sortingField, "ID")
        viewstate.Add(Me._sortingMode, Sort.SortDirection.ASC)
        Me.BindRemarkStatus()
        Me.BindBank(Me.ddlBank)
        Me._sessHelper.SetSession(Me._sessData, New ArrayList)
        Me._sessHelper.SetSession(Me._sessDataUpload, New ArrayList)
        dtgMain.DataSource = New ArrayList
        dtgMain.DataBind()
    End Sub

    Private Sub BindRemarkStatus()
        With Me.ddlCleared.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each oLI As ListItem In EnumPaymentRemarkStatus.GetList
                .Add(oLI)
            Next
        End With
    End Sub

    Private Sub BindDTG()
        Dim arlFM As New ArrayList

        SetControls()
        If viewstate.Item(Me._mode) = Me._modeUpload Then
            'If CType(Me._sessHelper.GetSession(Me._sessDataUpload), ArrayList).Count <= 0 Then
            arlFM = Me.GetDataFromFile()
            Me._sessHelper.SetSession(Me._sessDataUpload, arlFM)
            'Else
            '    arlFM = Me._sessHelper.GetSession(Me._sessDataUpload)
            'End If
            btnSave.Enabled = True
            For Each oDP As DailyPayment In arlFM
                If Not IsNothing(oDP.ErrorMessage) AndAlso oDP.ErrorMessage <> "" Then
                    btnSave.Enabled = False
                    Exit For
                End If
            Next
            Me.btnTransfer.Enabled = False
            Me.btnDownload.Enabled = False
        Else
            arlFM = Me.GetDataFromDB()
            Me._sessHelper.SetSession(Me._sessDataUpload, New ArrayList)
            Me.btnTransfer.Enabled = True
            Me.btnDownload.Enabled = True
            If CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
                If arlFM.Count > 0 Then
                    Me.btnSave.Enabled = True
                End If
            End If
        End If
        Me._sessHelper.SetSession(Me._sessData, arlFM)

        dtgMain.DataSource = arlFM
        dtgMain.DataBind()
    End Sub

    Private Function GetDataFromDB() As ArrayList
        Dim cDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sDP As SortCollection = New SortCollection
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Category.ProductCategory.Code", MatchType.Exact, companyCode))

        sDP.Add(New Sort(GetType(DailyPayment), ViewState.Item(Me._sortingField), ViewState.Item(Me._sortingMode)))
        If Me.txtCreditAccount.Text.Trim <> "" Then
            Dim sCAs As String = Me.txtCreditAccount.Text.Trim.Replace(";", "','")
            cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Dealer.CreditAccount", MatchType.InSet, "('" & sCAs & "')"))
        End If
        If Me.txtKodeDealer.Text.Trim <> "" Then
            Dim sDealerCodes As String = Me.txtKodeDealer.Text.Trim.Replace(";", "','")
            cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Dealer.DealerCode", MatchType.InSet, "('" & sDealerCodes & "')"))
        End If

        If ddlBank.SelectedIndex > 0 Then
            cDP.opAnd(New Criteria(GetType(DailyPayment), "SlipNumber", MatchType.StartsWith, Me.ddlBank.SelectedValue))
        End If
        If Me.ddlCleared.SelectedValue <> "-1" Then
            'cDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.Exact, ddlCleared.SelectedValue))
            cDP.opAnd(New Criteria(GetType(DailyPayment), "RemarkStatus", MatchType.Exact, ddlCleared.SelectedValue))
        End If
        cDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, 1))
        'If Me.chkIsReversed.Checked Then
        '    cDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.Exact, 1))
        'End If
        If Me.txtSONumber.Text.Trim <> "" Then
            cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.SONumber", MatchType.Exact, Me.txtSONumber.Text))
        End If
        If Me.txtRegNumber.Text.Trim <> String.Empty Then cDP.opAnd(New Criteria(GetType(DailyPayment), "DailyPaymentHeader.RegNumber", MatchType.StartsWith, Me.txtRegNumber.Text.Trim))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "BaselineDate", MatchType.Greater, Me.calStart.Value.AddDays(-1)))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "BaselineDate", MatchType.Lesser, Me.calEnd.Value.AddDays(1)))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.IsFactoring", MatchType.Exact, 1))
        Return oDPFac.Retrieve(cDP, sDP)
    End Function

    Private Function GetDataFromFile() As ArrayList
        If (Not fliInput.PostedFile Is Nothing) And (fliInput.PostedFile.ContentLength > 0) Then
            Dim SrcFile As String = Path.GetFileName(fliInput.PostedFile.FileName)   '-- Source file name
            Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\" & SrcFile  '-- Temp file

            '-- Impersonation to manipulate file in server
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Try
                If imp.Start() Then

                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(fliInput.PostedFile.InputStream, TempFile)

                    imp.StopImpersonate()
                    imp = Nothing

                    Dim parser As IParser = New GyroParser

                    '-- Parse data file and store result into list
                    Dim arList As ArrayList = CType(parser.ParseNoTransaction(TempFile, "User"), ArrayList)

                    '-- Check errors if any
                    Dim bError As Boolean = False
                    For Each oDP As DailyPayment In arList
                        If oDP.POHeader.IsFactoring <> 1 Then
                            If IsNothing(oDP.ErrorMessage) Then oDP.ErrorMessage = String.Empty
                            oDP.ErrorMessage &= IIf(oDP.ErrorMessage = String.Empty, "", ";") & "Bukan PO Factoring"
                        End If
                        If Not oDP.ErrorMessage = String.Empty Then
                            bError = True
                            Exit For
                        End If
                    Next
                    btnSave.Enabled = Not bError
                    Return arList
                End If
            Catch Exc As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If
    End Function

    Private Sub SetControls()
        If Me.ViewState.Item(Me._mode) = Me._modeView Then
            btnFind.Enabled = True
            btnUpload.Enabled = SecurityProvider.Authorize(Context.User, SR.Factoring_gyro_upload_privilege) ' True
            btnSave.Enabled = False
            'dtgMain.Columns(10).Visible = False 'keterangan
            'dtgMain.Columns(11).Visible = True ' editing controls
            'dtgMain.EditItemIndex = -1
        ElseIf Me.ViewState.Item(Me._mode) = Me._modeEdit Then
            btnFind.Enabled = False
            btnUpload.Enabled = False
            btnSave.Enabled = False
            'dtgMain.Columns(10).Visible = False 'keterangan
            'dtgMain.Columns(11).Visible = True ' editing controls
        ElseIf Me.ViewState.Item(Me._mode) = Me._modeUpload Then
            btnFind.Enabled = True
            btnUpload.Enabled = SecurityProvider.Authorize(Context.User, SR.Factoring_gyro_upload_privilege) ' True
            btnSave.Enabled = False
            'dtgMain.Columns(10).Visible = True 'keterangan
            'dtgMain.Columns(11).Visible = True ' False ' editing controls
            'dtgMain.EditItemIndex = -1
        End If
    End Sub

    Private Function GetDetailName(ByVal strFullName As String) As String
        If strFullName.Length >= 6 AndAlso IsNumeric(strFullName.Substring(0, 6)) Then
            Return strFullName.Substring(5).Trim
        Else
            Return strFullName
        End If
    End Function

    Private Sub BindBank(ByRef pDdlBank As DropDownList)
        'Dim strBanks() As String = KTB.DNet.Lib.WebConfig.GetValue("BankName").Split(";")
        'Dim i As Integer

        'pDdlBank.Items.Clear()
        'pDdlBank.Items.Add(New ListItem("Silahkan Pilih", -1))
        'For i = 0 To strBanks.Length - 1
        '    pDdlBank.Items.Add(New ListItem(strBanks(i), strBanks(i)))
        'Next

        Dim oBFac As BankFacade = New BankFacade(User)
        Dim aB As ArrayList = oBFac.RetrieveActiveList()
        pDdlBank.Items.Clear()
        pDdlBank.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each oB As Bank In aB
            pDdlBank.Items.Add(New ListItem(oB.BankName, oB.BankCode))
        Next
    End Sub

    Private Function GetPOHeaderBySONumber(ByVal SONumber As String) As POHeader
        Dim oPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim cPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aPOH As New ArrayList

        cPOH.opAnd(New Criteria(GetType(POHeader), "SONumber", MatchType.Exact, SONumber))
        aPOH = oPOHFac.Retrieve(cPOH)
        If aPOH.Count > 0 Then
            Return aPOH(0)
        Else
            Return Nothing
        End If
    End Function
    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Pembayaran")
        End If
        '
        Me.btnUpload.Enabled = SecurityProvider.Authorize(Context.User, SR.factoring_gyro_upload_privilege)
        Me.btnSave.Enabled = Me.btnUpload.Enabled
    End Sub


    Private Sub DoDownload(ByRef arlData As ArrayList)
        Dim sFileName As String
        Dim fullFileName As String
        sFileName = "Daftar Pembayaran Factoring " & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        fullFileName = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(fullFileName)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(fullFileName, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteData(sw, arlData)

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

    Private Sub WriteData(ByVal sw As StreamWriter, ByVal arlData As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim DiscountAmount As Decimal, TotalCost As Decimal, Amount As Decimal
        Dim oDP As DailyPayment
        Dim i As Integer = 1

        If Not IsNothing(arlData) Then
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Status" & tab)
            itemLine.Append("No.Reg. Gyro" & tab)
            itemLine.Append("No.Reg. PO" & tab)
            itemLine.Append("Nomor SO" & tab)
            itemLine.Append("No Giro/Slip Transfer" & tab)
            itemLine.Append("Tanggal Jatuh Tempo" & tab)
            itemLine.Append("Tanggal Efektif" & tab)
            itemLine.Append("Nilai (Rp)" & tab)
            itemLine.Append("Status Pembayaran" & tab)
            itemLine.Append("Upload Terakhir Oleh" & tab)
            itemLine.Append("Upload Terakhir" & tab)
            itemLine.Append("Reversed" & tab)


            sw.WriteLine(itemLine.ToString())
            i = 1
            For Each di As DataGridItem In Me.dtgMain.Items
                oDP = CType(arlData(di.ItemIndex), DailyPayment)
                itemLine.Remove(0, itemLine.Length)

                itemLine.Append(i.ToString & tab)   'No
                itemLine.Append(EnumPaymentStatus.GetStringValue(oDP.Status) & tab)   'Status
                itemLine.Append(oDP.DailyPaymentHeader.RegNumber & tab)   'No.Reg. Gyro
                itemLine.Append(oDP.POHeader.DealerPONumber & tab)    'No.Reg. PO
                itemLine.Append(oDP.POHeader.SONumber & tab)   'Nomor SO
                itemLine.Append(oDP.SlipNumber & tab)   'No Giro/Slip Transfer
                itemLine.Append(oDP.BaselineDate.ToString("dd/MM/yyyy") & tab)   'Tanggal Jatuh Tempo
                itemLine.Append(oDP.EffectiveDate.ToString("dd/MM/yyyy") & tab)   'Tanggal Efektif
                itemLine.Append(Me.GetFormatNumber(oDP.Amount) & tab)   'Nilai (Rp)
                itemLine.Append(EnumPaymentRemarkStatus.GetStringValue(oDP.RemarkStatus) & tab)   'Status Pembayaran 
                itemLine.Append(oDP.LastUploadedBy & tab)   'Upload Terakhir Oleh'o2n
                itemLine.Append(IIf(oDP.LastUploadedTime.Year < 1990, "", oDP.LastUploadedTime.ToString("dd/MMM/yy hh:mm:ss")) & tab)   'Upload Terakhir 
                
                itemLine.Append(IIf(oDP.IsReversed = 1, "x", "") & tab)   'Reversed

                sw.WriteLine(itemLine.ToString())
                i += 1
            Next
        End If
    End Sub

    Private Function GetFormatNumber(ByRef Amount As Decimal) As String
        Return FormatNumber(Amount, 0, TriState.False, TriState.UseDefault, TriState.False)
    End Function

    Private Sub SaveUpload()
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim nError As Integer = 0
        Dim arlTemp As ArrayList = CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)
        Dim oDPDB As DailyPayment
        Dim aToUpdate As New ArrayList
        Dim cDP As CriteriaComposite
        Dim aDPTemp As ArrayList


        For Each di As DataGridItem In Me.dtgMain.Items
            Dim lblErrorMessage As Label = di.FindControl("lblErrorMessage")
            If lblErrorMessage.Text.Trim <> "" Then
                MessageBox.Show("Simpan Gagal. Data ada yang tidak valid")
                Exit Sub
            End If
        Next
        For Each oDP As DailyPayment In arlTemp
            If Me.ViewState.Item(Me._mode) = Me._modeUpload Then
                cDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "SlipNumber", MatchType.Exact, odp.SlipNumber))
                cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, odp.POHeader.ID))
                aDPTemp = oDPFac.Retrieve(cDP)
                If aDPTemp.Count > 0 Then
                    oDPDB = aDPTemp(0)
                Else
                    oDPDB = New DailyPayment
                End If
            Else
                oDPDB = oDPFac.Retrieve(oDP.ID)
            End If
            If Not IsNothing(oDPDB) AndAlso oDPDB.ID > 0 Then
                oDPDB.SlipNumber = oDP.SlipNumber
                oDPDB.POHeader = oDP.POHeader
                oDPDB.IsReversed = oDP.IsReversed
                oDPDB.IsCleared = oDP.IsCleared
                oDPDB.Amount = oDP.Amount
                oDPDB.RemarkStatus = odp.RemarkStatus
                oDPDB.ReUpload = 0
                oDPDB.LastUploadedBy = User.Identity.Name
                oDPDB.LastUploadedTime = Now
                If oDP.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.PT _
                    OrElse oDP.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.PTOffset _
                    OrElse oDP.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.RejectPaid _
                    OrElse oDP.RemarkStatus = EnumPaymentRemarkStatus.PaymentRemarkStatus.Cleared Then
                    oDPDB.EffectiveDate = oDP.EffectiveDate
                End If
                'oDPDB.EffectiveDate = odp.EffectiveDate
                If oDP.IsCleared = 1 Then oDPDB.Status = EnumPaymentStatus.PaymentStatus.Selesai
                aToUpdate.Add(oDPDB)
            Else
                If oDP.IsCleared = 1 Then oDP.Status = EnumPaymentStatus.PaymentStatus.Selesai
                'nError += IIf(oDPFac.insertDP(oDP) < 0, 1, 0)
                nError += 1
            End If
        Next
        If aToUpdate.Count > 0 Then
            oDPFac.Update(aToUpdate)
            TransferToSAP(aToUpdate)
        End If

        If nError > 0 Then
            If arlTemp.Count = nError Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(nError.ToString & " gagal disimpan")
            End If
            Exit Sub
        End If

        Dim sCAs As String = ""
        Dim sDCs As String = ""
        Dim minDate As Date = Now.AddYears(10)
        Dim maxDate As Date = Now.AddYears(-10)
        Dim sSONumbers As String = ""

        For Each oDP As DailyPayment In aToUpdate
            Dim GyroDate As Date = oDP.BaselineDate
            If GyroDate < minDate Then minDate = GyroDate
            If GyroDate > maxDate Then maxDate = GyroDate
            sCAs &= IIf(sCAs.Trim = "", "", ";") & oDP.POHeader.Dealer.CreditAccount
            sDCs &= IIf(sDCs.Trim = "", "", ";") & oDP.POHeader.Dealer.DealerCode
        Next
        If arlTemp.Count > 0 Then
            If minDate.Year < 1990 Then minDate = Me.calStart.Value
            If maxDate.Year < 1990 Then maxDate = Me.calEnd.Value
            If minDate.Year < 1990 Then minDate = Now
            If maxDate.Year < 1990 Then maxDate = Now
            If minDate = Now.AddYears(10) Then minDate = Me.calStart.Value
            If maxDate = Now.AddYears(-10) Then maxDate = Me.calEnd.Value
        End If
        Me.txtCreditAccount.Text = sCAs
        Me.txtKodeDealer.Text = sDCs
        Me.calStart.Value = minDate
        Me.calEnd.Value = maxDate
    End Sub

    Private Sub TransferToSAP(ByRef aDPs As ArrayList)
        Dim _fileHelper As New FileHelper
        Dim str As FileInfo
        Dim aToTransfers As New ArrayList
        Try
            For Each oDP As DailyPayment In aDPs
                If oDP.RemarkStatus = CType(EnumPaymentRemarkStatus.PaymentRemarkStatus.Reject, Short) _
                    OrElse oDP.RemarkStatus = CType(EnumPaymentRemarkStatus.PaymentRemarkStatus.NotCleared, Short) Then
                Else
                    aToTransfers.Add(oDP)
                End If
            Next
            If aToTransfers.Count = 0 Then Exit Sub
            str = _fileHelper.TransferFactoringPaymentToSAP(aToTransfers)
            MessageBox.Show(SR.UploadSucces(str.Name))
        Catch ex As Exception
            MessageBox.Show(SR.UploadFail(str.Name))
        End Try
    End Sub

    Private Sub SaveFindDB()
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)

        For Each di As DataGridItem In Me.dtgMain.Items
            Dim lblErrorMessage As Label = di.FindControl("lblErrorMessage")
            If lblErrorMessage.Text.Trim <> "" Then
                MessageBox.Show("Simpan Gagal. Data ada yang tidak valid")
                Exit Sub
            End If
        Next

        For Each di As DataGridItem In Me.dtgMain.Items
            Dim chkReUpload As CheckBox = di.FindControl("chkReUpload")
            If chkReUpload.Checked Then
                'Update DailyPayment set Uploadby to null
                Dim iD As Integer = CType(di.Cells(0).Text, Integer)
                Dim oDP As DailyPayment = oDPFac.Retrieve(iD)
                If Not oDP Is Nothing Then
                    oDP.ReUpload = 1
                    oDPFac.Update(oDP)
                End If
            End If
        Next

    End Sub

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            Initialization()
        End If
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Me.ViewState.Item(Me._mode) = Me._modeView
        BindDTG()
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        Dim IsUploading As Boolean = IIf(viewstate.Item(Me._mode) = Me._modeUpload, True, False)
        Dim Total As Decimal = 0
        Dim vstTotal As String = "dtgMain.Total"

        If CType(Session("Dealer"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
            If Me.ViewState.Item(Me._mode) = Me._modeUpload Then
                e.Item.Cells(14).Visible = False
            Else
                e.Item.Cells(14).Visible = True
            End If
        Else
            e.Item.Cells(14).Visible = False
        End If

        If e.Item.ItemType = ListItemType.Header Then
            Me.ViewState.Add(vstTotal, 0)
            Me.lblTotal.Text = FormatNumber(0, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oDP As DailyPayment = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex), DailyPayment)
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblStatus As Label = e.Item.FindControl("lblStatus")
            Dim lblRegNumber As Label = e.Item.FindControl("lblRegNumber")
            Dim lblPONumber As Label = e.Item.FindControl("lblPONumber")
            Dim lblSONumber As Label = e.Item.FindControl("lblSONumber")
            Dim lblSlipNumber As Label = e.Item.FindControl("lblSlipNumber")
            Dim lblEffectiveDate As Label = e.Item.FindControl("lblEffectiveDate")
            Dim lblBaselineDate As Label = e.Item.FindControl("lblBaselineDate")
            Dim lblAmount As Label = e.Item.FindControl("lblAmount")
            Dim lblIsCleared As Label = e.Item.FindControl("lblIsCleared")
            Dim lblIsReversed As Label = e.Item.FindControl("lblIsReversed")
            Dim lblLastUploadedBy As Label = e.Item.FindControl("lblLastUploadedBy")
            Dim lblLastUploadedTime As Label = e.Item.FindControl("lblLastUploadedTime")
            Dim lblErrorMessage As Label = e.Item.FindControl("lblErrorMessage")
            Dim chkReUpload As CheckBox = e.Item.FindControl("chkReUpload")
            Dim lblRegNumberDPH As Label = e.Item.FindControl("lblRegNumberDPH")

            lblNo.Text = e.Item.ItemIndex + 1
            lblStatus.Text = EnumPaymentStatus.GetStringValue(odp.Status)
            If IsNothing(odp.DailyPaymentHeader) OrElse odp.DailyPaymentHeader.ID < 1 Then
                lblRegNumber.Text = ""
            Else
                lblRegNumber.Text = odp.DailyPaymentHeader.RegNumber
            End If
            If IsNothing(odp.POHeader) Then 'OrElse odp.POHeader.ID < 1 Then
                lblPONumber.Text = ""
                lblSONumber.Text = ""
            Else
                lblPONumber.Text = odp.POHeader.DealerPONumber
                lblSONumber.Text = odp.POHeader.SONumber
            End If
            If IsNothing(oDP.DailyPaymentHeader) Then
                lblRegNumberDPH.Text = ""
            Else
                lblRegNumberDPH.Text = oDP.DailyPaymentHeader.RegNumber
            End If

            lblSlipNumber.Text = odp.SlipNumber
            lblEffectiveDate.Text = odp.EffectiveDate.ToString("dd/MM/yyyy")
            lblBaselineDate.Text = odp.BaselineDate.ToString("dd/MM/yyyy")
            lblAmount.Text = FormatNumber(odp.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'lblIsCleared.Text = IIf(odp.IsCleared = 1, "x", "")
            lblIsCleared.Text = IIf(odp.RemarkStatus > 0, EnumPaymentRemarkStatus.GetStringValue(odp.RemarkStatus), "")
            lblIsReversed.Text = IIf(odp.IsReversed = 1, "x", "")
            lblLastUploadedBy.Text = odp.LastUploadedBy
            lblLastUploadedTime.Text = IIf(odp.LastUploadedTime.Year < 1990, "", odp.LastUploadedTime.ToString("dd/MMM/yy hh:mm:ss"))
            lblErrorMessage.Text = IIf(IsUploading = True, odp.ErrorMessage, "")
            If odp.ReUpload = 1 Then
                chkReUpload.Visible = False
            End If
            Total = CType(Me.ViewState.Item(vstTotal), Decimal)
            Total += odp.Amount
            Me.ViewState.Item(vstTotal) = Total
        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            Dim oDP As DailyPayment = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex), DailyPayment)
            Dim lblNoE As Label = e.Item.FindControl("lblNoE")
            Dim lblStatusE As Label = e.Item.FindControl("lblStatusE")
            Dim lblRegNumberE As Label = e.Item.FindControl("lblRegNumberE")
            Dim lblPONumberE As Label = e.Item.FindControl("lblPONumberE")
            Dim txtSONumberE As TextBox = e.Item.FindControl("txtSONumberE")
            Dim lblEffectiveDate As Label = e.Item.FindControl("lblEffectiveDateE")
            Dim lblBaselineDateE As Label = e.Item.FindControl("lblBaselineDateE")
            Dim ddlBankE As DropDownList = e.Item.FindControl("ddlBankE")
            Dim txtGyroNumberE As TextBox = e.Item.FindControl("txtGyroNumberE")
            Dim txtAmountE As TextBox = e.Item.FindControl("txtAmountE")
            Dim ddlIsClearedE As DropDownList = e.Item.FindControl("ddlIsClearedE")
            Dim ddlIsReversedE As DropDownList = e.Item.FindControl("ddlIsReversedE")
            Dim lblLastUploadedByE As Label = e.Item.FindControl("lblLastUploadedByE")
            Dim lblLastUploadedTimeE As Label = e.Item.FindControl("lblLastUploadedTimeE")
            Dim lblErrorMessageE As Label = e.Item.FindControl("lblErrorMessageE")
            Dim chkReUpload As CheckBox = e.Item.FindControl("chkReUploadE")

            ddlIsClearedE.Items.Clear()
            'ddlIsClearedE.Items.Add(New ListItem("Ya", "1"))
            'ddlIsClearedE.Items.Add(New ListItem("Tidak", "0"))
            For Each oLI As ListItem In EnumPaymentRemarkStatus.GetList
                ddlIsClearedE.Items.Add(oLI)
            Next

            ddlIsReversedE.Items.Clear()
            ddlIsReversedE.Items.Add(New ListItem("Ya", "1"))
            ddlIsReversedE.Items.Add(New ListItem("Tidak", "0"))
            lblNoE.Text = e.Item.ItemIndex + 1
            lblStatusE.Text = EnumPaymentStatus.GetStringValue(odp.Status)
            If IsNothing(odp.DailyPaymentHeader) OrElse odp.DailyPaymentHeader.ID < 1 Then
                lblRegNumberE.Text = ""
            Else
                lblRegNumberE.Text = odp.DailyPaymentHeader.RegNumber
            End If
            If Not IsNothing(odp.POHeader) Then
                lblPONumberE.Text = odp.POHeader.DealerPONumber
                txtSONumberE.Text = odp.POHeader.SONumber
            Else
                lblPONumberE.Text = String.Empty
                txtSONumberE.Text = String.Empty
            End If
            BindBank(ddlBankE)
            Dim sTemps() As String = oDP.SlipNumber.Split(" ")
            Dim i As Integer = 0
            Dim sBankName As String = ""
            Dim sGyroNumber As String = ""

            For i = 0 To sTemps.Length - 1
                If i < sTemps.Length - 1 Then
                    sBankName &= sTemps(i) & " "
                ElseIf i = sTemps.Length - 1 Then
                    sGyroNumber = sTemps(i)
                End If
            Next

            sBankName = sBankName.Trim
            ddlBankE.SelectedValue = sBankName
            txtGyroNumberE.Text = sGyroNumber
            lblEffectiveDate.Text = odp.EffectiveDate.ToString("dd/MM/yyyy")
            lblBaselineDateE.Text = odp.BaselineDate.ToString("dd/MM/yyyy")
            txtAmountE.Text = FormatNumber(odp.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            'ddlIsClearedE.SelectedValue = odp.IsCleared
            ddlIsClearedE.SelectedValue = odp.RemarkStatus
            ddlIsReversedE.SelectedValue = odp.IsReversed
            lblLastUploadedByE.Text = odp.LastUploadedBy
            lblLastUploadedTimeE.Text = IIf(odp.LastUploadedTime.Year < 1990, "", odp.LastUploadedTime.ToString("dd/MMM/yy hh:mm:ss"))

            lblErrorMessageE.Text = IIf(IsUploading = True, odp.ErrorMessage, "")
            If odp.ReUpload = 1 Then
                chkReUpload.Visible = False
            End If
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            Total = CType(Me.ViewState.Item(vstTotal), Decimal)
            Me.lblTotal.Text = FormatNumber(Total, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Sub dtgMain_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgMain.SortCommand
        If e.SortExpression = Me.ViewState.Item(Me._sortingField) Then
            If Me.ViewState.Item(Me._sortingMode) = Sort.SortDirection.ASC Then
                Me.ViewState.Item(Me._sortingMode) = Sort.SortDirection.DESC
            Else
                Me.ViewState.Item(Me._sortingMode) = Sort.SortDirection.ASC
            End If
        Else
            Me.ViewState.Item(Me._sortingField) = e.SortExpression
            Me.ViewState.Item(Me._sortingMode) = Sort.SortDirection.ASC
        End If
        BindDTG()
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Try
            Me.ViewState.Item(Me._mode) = Me._modeUpload
            BindDTG()

        Catch ex As Exception
            MessageBox.Show("Upload gagal. Periksa kembali file yang anda upload")
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If viewstate.Item(Me._mode) = Me._modeUpload Then
            SaveUpload()
        Else
            SaveFindDB()
        End If

        Me.ViewState.Item(Me._mode) = Me._modeView
        BindDTG()
    End Sub

    Private Sub dtgMain_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.EditCommand
        'Me.ViewState.Item(Me._mode) = Me._modeEdit
        dtgMain.EditItemIndex = e.Item.ItemIndex
        BindDTG()
    End Sub

    Private Sub dtgMain_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.CancelCommand
        Me.dtgMain.EditItemIndex = -1
        BindDTG()
    End Sub

    Private Sub dtgMain_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.UpdateCommand
        Dim oDP As DailyPayment = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex), DailyPayment)
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim oDPOri As DailyPayment
        Dim txtSONumberE As TextBox = e.Item.FindControl("txtSONumberE")
        Dim ddlBankE As DropDownList = e.Item.FindControl("ddlBankE")
        Dim txtGyroNumberE As TextBox = e.Item.FindControl("txtGyroNumberE")
        Dim txtAmountE As TextBox = e.Item.FindControl("txtAmountE")
        Dim ddlIsClearedE As DropDownList = e.Item.FindControl("ddlIsClearedE")
        Dim ddlIsReversedE As DropDownList = e.Item.FindControl("ddlIsReversedE")
        Dim oPOH As POHeader
        Dim sVal As String = ""


        oDP.ErrorMessage = ""
        oDPOri = oDP

        sVal &= txtSONumberE.Text
        'sVal &= ";" & ddlIsClearedE.SelectedValue
        'sVal &= ";" & ddlIsReversedE.SelectedValue
        sVal &= ";" & ddlBankE.SelectedValue & " " & txtGyroNumberE.Text
        sVal &= ";" & txtAmountE.Text

        Dim oGP As GyroParser = New GyroParser
        Dim oDPTemp As DailyPayment = (New GyroParser).ParseFromString(sVal)

        oDP.POHeader = oDPTemp.POHeader
        oDP.IsCleared = oDPTemp.IsCleared
        oDP.RemarkStatus = ddlIsClearedE.SelectedValue
        oDP.IsReversed = oDPTemp.IsReversed
        oDP.SlipNumber = oDPTemp.SlipNumber
        oDP.Amount = oDPTemp.Amount
        oDP.ErrorMessage = oDPTemp.ErrorMessage

        If oDP.ErrorMessage <> "" Then
            CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex) = oDP
            MessageBox.Show(oDP.ErrorMessage)
            Exit Sub
        End If

        If viewstate.Item(Me._mode) = Me._modeUpload Then
            CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex) = oDP
            Me._sessHelper.SetSession(Me._sessDataUpload, Me._sessHelper.GetSession(Me._sessData))
        Else
            Dim aTemp As New ArrayList
            Dim dTemp As Date
            dTemp = New Date(oDP.BaselineDate.Year, oDP.BaselineDate.Month, oDP.BaselineDate.Day)
            oDP.BaselineDate = dTemp
            dTemp = New Date(oDP.DocDate.Year, oDP.DocDate.Month, oDP.DocDate.Day)
            oDP.DocDate = dTemp
            aTemp.Add(oDP)
            oDPFac.Update(aTemp)
            Me.ViewState.Item(Me._mode) = Me._modeView
        End If
        Me.dtgMain.EditItemIndex = -1
        BindDTG()
    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand

    End Sub

    Private Sub btnTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Dim aDP As ArrayList = Me._sessHelper.GetSession(Me._sessData)

    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim aDP As ArrayList = Me._sessHelper.GetSession(Me._sessData)

        If aDP.Count > 0 Then
            DoDownload(aDP)
        Else
            MessageBox.Show("Tidak ada data yang didownload")
        End If
    End Sub

#End Region
End Class
