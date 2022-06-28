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
Imports KTB.DNet.BusinessFacade.FinishUnit
#End Region


Public Class FrmFactoring
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents calUpload As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents fliInput As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtCreditAccount As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPO As System.Web.UI.WebControls.Label
    Protected WithEvents hdnValidate As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents ddlFactoringStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnTransfer As System.Web.UI.WebControls.Button
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList

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
    Private objUser As UserInfo
#End Region

#Region "Custom Methods"

    Private Sub Initialization()
        Dim oD As Dealer = CType(Session.Item("DEALER"), Dealer)

        viewstate.Add(_mode, _modeView)
        viewstate.Add(Me._sortingField, "CreditAccount")
        viewstate.Add(Me._sortingMode, Sort.SortDirection.ASC)
        viewstate.Add("TotalPO", 0)
        Me._sessHelper.SetSession(Me._sessData, New ArrayList)
        Me._sessHelper.SetSession(Me._sessDataUpload, New ArrayList)
        Me.calUpload.Value = Now.Date

        Me.btnTransfer.Visible = (oD.Title = CType(EnumDealerTittle.DealerTittle.KTB, Short))
        Me.dtgMain.Columns(0).Visible = Me.btnTransfer.Visible
        With Me.ddlFactoringStatus.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", 0))
            .Add(New ListItem("Aktif", 1))
            .Add(New ListItem("Non-Aktif", 2))
        End With

        dtgMain.DataSource = New ArrayList
        dtgMain.DataBind()
        btnSave.Enabled = False

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True, companyCode)
        ddlProductCategory.Items.RemoveAt(0)

        Me.btnSave.Attributes.Add("OnClick", "return confirmSave();")
    End Sub

    Private Sub BindDTG()
        Dim arlFM As New ArrayList

        SetControls()
        If viewstate.Item(Me._mode) = Me._modeUpload Then
            If CType(Me._sessHelper.GetSession(Me._sessDataUpload), ArrayList).Count <= 0 Then
                arlFM = Me.GetDataFromFile()
            Else
                arlFM = Me._sessHelper.GetSession(Me._sessDataUpload)
            End If
        Else
            arlFM = Me.GetDataFromDB()
            Me._sessHelper.SetSession(Me._sessDataUpload, New ArrayList)
        End If
        Me._sessHelper.SetSession(Me._sessData, arlFM)

        viewstate.Item("TotalPO") = 0
        dtgMain.DataSource = arlFM
        dtgMain.DataBind()

        Me.lblTotalPO.Text = FormatNumber(CType(viewstate.Item("TotalPO"), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
    End Sub

    Private Function GetDataFromDB() As ArrayList
        Dim cFM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sFM As SortCollection = New SortCollection
        Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
        Dim Sql As String = String.Empty

        sFM.Add(New Sort(GetType(FactoringMaster), viewstate.Item(Me._sortingField), viewstate.Item(Me._sortingMode)))
        If Me.txtCreditAccount.Text.Trim <> "" Then
            Dim sCAs As String = Me.txtCreditAccount.Text.Trim.Replace(";", "','")

            'cFM.opAnd(New Criteria(GetType(FactoringMaster), "CreditAccount", MatchType.Exact, Me.txtCreditAccount.Text.Trim))
            cFM.opAnd(New Criteria(GetType(FactoringMaster), "CreditAccount", MatchType.InSet, "('" & sCAs & "')"))
        End If
        If CType(Me.ddlFactoringStatus.SelectedValue, Integer) = 1 Then 'Aktif
            Sql &= " ( select tc.Status from TransactionControl tc, Dealer d where tc.RowStatus=" & CType(DBRowStatus.Active, Short).ToString & " and tc.DealerID=d.ID and tc.Kind=" & CType(EnumDealerTransType.DealerTransKind.Factoring, Short).ToString & " and d.DealerCode=FactoringMaster.CreditAccount ) "
            cFM.opAnd(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.No, Sql))
        ElseIf CType(Me.ddlFactoringStatus.SelectedValue, Integer) = 2 Then 'Non-Aktif
            Sql &= " ( select tc.Status from TransactionControl tc, Dealer d where tc.RowStatus=" & CType(DBRowStatus.Active, Short).ToString & " and tc.DealerID=d.ID and tc.Kind=" & CType(EnumDealerTransType.DealerTransKind.Factoring, Short).ToString & " and d.DealerCode=FactoringMaster.CreditAccount ) "
            cFM.opAnd(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, Sql))
        End If
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID > 0 Then
            cFM.opAnd(New Criteria(GetType(FactoringMaster), "ProductCategory.ID", MatchType.Exact, PCID))
        End If
        Return oFMFac.Retrieve(cFM, sFM)
    End Function

    Private Function GetDataFromFile() As ArrayList

        If (Not fliInput.PostedFile Is Nothing) And (fliInput.PostedFile.ContentLength > 0) Then
            Dim SrcFile As String = Path.GetFileName(fliInput.PostedFile.FileName)   '-- Source file name
            Dim TempFile As String = Server.MapPath("") & "\..\DataTemp\FM_" & Now.ToString("yyyyMMddHHmmss") & "_" & SrcFile   '-- Temp file

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

                    If CekFileAuth(TempFile) Then
                        Dim parser As IParser = New FactoringParser

                        '-- Parse data file and store result into list
                        Dim arList As ArrayList = CType(parser.ParseNoTransaction(TempFile, "User"), ArrayList)

                        '-- Check errors if any
                        Dim bError As Boolean = False
                        For Each oFM As FactoringMaster In arList
                            If Not oFM.ErrorMessage = String.Empty Then
                                bError = True
                                Exit For
                            End If
                        Next
                        btnSave.Enabled = Not bError
                        Return arList
                    Else
                        MessageBox.Show("Terdapat struktur data yang salah")
                        btnSave.Enabled = False
                    End If

                End If
            Catch Exc As Exception
                MessageBox.Show(SR.UploadFail(SrcFile))
            End Try
        Else
            MessageBox.Show(SR.FileNotSelected)
        End If
    End Function

    Private Function CekFileAuth(ByVal tempFile As String) As Boolean
        Dim vReturn As Boolean = True
        objUser = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Dim oFile As System.IO.File
        Dim oRead As System.IO.StreamReader
        oRead = oFile.OpenText(tempFile)
        Dim LineIn As String
        Dim Fields() As String
        While oRead.Peek <> -1
            LineIn = oRead.ReadLine()
            If LineIn.Trim <> "" Then
                Fields = LineIn.Split(";")
                If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    If Fields.Length <> 4 Then
                        vReturn = False
                        Exit While
                    End If
                ElseIf objUser.Dealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                    If Fields.Length <> 5 Then
                        vReturn = False
                        Exit While
                    End If
                End If
            End If
        End While
        oRead.Close()
        Return vReturn
    End Function

    Private Sub SetControls()
        If Me.ViewState.Item(Me._mode) = Me._modeView Then
            btnFind.Enabled = True
            btnUpload.Enabled = True
            btnSave.Enabled = False
            dtgMain.Columns(12 + 1).Visible = True 'keterangan
            dtgMain.Columns(17 + 1).Visible = True ' editing controls
            dtgMain.EditItemIndex = -1
        ElseIf Me.ViewState.Item(Me._mode) = Me._modeEdit Then
            btnFind.Enabled = False
            btnUpload.Enabled = False
            btnSave.Enabled = False
            dtgMain.Columns(12 + 1).Visible = True 'keterangan
            dtgMain.Columns(17 + 1).Visible = True ' editing controls
        ElseIf Me.ViewState.Item(Me._mode) = Me._modeUpload Then
            btnFind.Enabled = True
            btnUpload.Enabled = True
            btnSave.Enabled = False
            dtgMain.Columns(12 + 1).Visible = True 'keterangan
            dtgMain.Columns(17 + 1).Visible = False ' False ' editing controls
            dtgMain.EditItemIndex = -1
        End If
    End Sub

    Private Function GetDetailName(ByVal strFullName As String) As String
        If strFullName.Length >= 6 AndAlso IsNumeric(strFullName.Substring(0, 6)) Then
            Return strFullName.Substring(5).Trim
        Else
            Return strFullName
        End If
    End Function

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.factoring_ceiling_lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Ceiling Master")
        End If
        Me.btnUpload.Enabled = SecurityProvider.Authorize(Context.User, SR.factoring_ceiling_upload_privilege)
        Me.btnSave.Enabled = Me.btnUpload.Enabled
        Me.dtgMain.Columns(17 + 1).Visible = SecurityProvider.Authorize(Context.User, SR.Factoring_ceiling_aktivasi_privilege) 'Action->Activate Or Deactivate
        Me.dtgMain.Columns(18 + 1).Visible = SecurityProvider.Authorize(Context.User, SR.Factoring_ceiling_edit_privilege) 'Action->Edit 
    End Sub

    Private Function GetOutstandingByCalc(ByVal PC As ProductCategory, ByVal CreditAccount As String) As Decimal
        Dim cDP As New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim oDPFac As New DailyPaymentFacade(User)
        Dim aggDP As New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)
        Dim Total As Decimal = 0
        cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.IsFactoring", MatchType.Exact, 1))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Status", MatchType.Exact, CType(enumStatusPO.Status.Selesai, Short)))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        'cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, CType(enumPaymentType.PaymentType.TOP, Short)))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.Exact, 0))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.Exact, 0))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        'cDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, Now.ToString("yyyy/MM/dd 00:00:00")))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.Exact, 7))

        cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Category.ProductCategory.ID", MatchType.Exact, PC.ID))
        Total = oDPFac.RetrieveAggregate(cDP, aggDP)

        Return Total
    End Function
#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckUserPrivilege()
        If Not IsPostBack Then
            Initialization()
        Else
            If (Request.Form("hdnValidate") = "1") Then
                dtgMain_UpdateCommand(Nothing, Nothing)
            End If
            hdnValidate.Value = "-1"
        End If
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Me.ViewState.Item(Me._mode) = Me._modeView
        BindDTG()
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        Dim IsUploading As Boolean = IIf(viewstate.Item(Me._mode) = Me._modeUpload, True, False)
        objUser = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If e.Item.ItemType = ListItemType.Header Then
            If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                e.Item.Cells(5 + 1).Visible = False
            ElseIf objUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                e.Item.Cells(5 + 1).Visible = True
            End If
            If Me.ViewState.Item(Me._mode) = Me._modeUpload Then
                e.Item.Cells(18 + 1).Visible = False ' editing controls
            End If

        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oFM As FactoringMaster = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex), FactoringMaster)
            Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
            Dim lblAccountName As Label = e.Item.FindControl("lblAccountName")
            Dim lblTotalCeiling As Label = e.Item.FindControl("lblTotalCeiling")
            Dim lblStandardCeiling As Label = e.Item.FindControl("lblStandardCeiling")
            Dim lblFactoringCeiling As Label = e.Item.FindControl("lblFactoringCeiling")
            Dim lblOutstanding As Label = e.Item.FindControl("lblOutstanding")
            Dim lblAvCeiling As Label = e.Item.FindControl("lblAvCeiling")
            Dim lblPODiajukan As Label = e.Item.FindControl("lblPODiajukan")
            Dim lblAvailableCeiling As Label = e.Item.FindControl("lblAvailableCeiling")
            Dim lblErrorMessage As Label = e.Item.FindControl("lblErrorMessage")
            Dim lblStatus As Label = e.Item.FindControl("lblStatus")
            Dim lblCreatedBy As Label = e.Item.FindControl("lblCreatedBy")
            Dim lblLastUpdatedBy As Label = e.Item.FindControl("lblLastUpdatedBy")
            Dim lbtnActivate As LinkButton = e.Item.FindControl("lbtnActivate")
            Dim lbtnDeactivate As LinkButton = e.Item.FindControl("lbtnDeactivate")
            Dim lblMaxTOPDate As Label = e.Item.FindControl("lblMaxTOPDate")
            Dim arlFactComponent As ArrayList = oFMFac.GetCeilingComponent(oFM.ProductCategory, ofm.CreditAccount, IIf(IsUploading, ofm, Nothing))
            Dim lblOutstandingCalc As Label = e.Item.FindControl("lblOutstandingCalc")
            Dim lblProductCategory As Label = e.Item.FindControl("lblProductCategory")

            If IsNothing(oFM.ErrorMessage) Then ofm.ErrorMessage = ""
            lblNo.Text = e.Item.ItemIndex + 1
            lblCreditAccount.Text = ofm.CreditAccount
            lblAccountName.Text = ""
            Dim oD As Dealer = New DealerFacade(User).Retrieve(ofm.CreditAccount)
            If Not IsNothing(oD) AndAlso oD.ID > 0 Then
                lblAccountName.Text = oD.DealerName
            End If
            If Me.ViewState.Item(Me._mode) = Me._modeUpload Then
                Dim objFM As FactoringMaster = oFMFac.Retrieve(oFM.ProductCategory, oFM.CreditAccount)
                If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                    If Not IsNothing(objFM) Then
                        oFM.TotalCeiling = objFM.TotalCeiling
                        oFM.StandardCeiling = objFM.StandardCeiling
                    Else
                        oFM.TotalCeiling = 0
                        oFM.StandardCeiling = 0
                    End If
                ElseIf objUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    oFM.FactoringCeiling = objFM.FactoringCeiling
                    oFM.Outstanding = objFM.Outstanding
                    oFM.AvailableCeiling = objFM.AvailableCeiling
                End If
                lblTotalCeiling.Text = FormatNumber(oFM.TotalCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblStandardCeiling.Text = FormatNumber(oFM.StandardCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblFactoringCeiling.Text = FormatNumber(oFM.FactoringCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblOutstanding.Text = FormatNumber(ofm.Outstanding, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblAvCeiling.Text = FormatNumber(ofm.AvailableCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPODiajukan.Text = FormatNumber(CType(arlFactComponent(3), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblAvailableCeiling.Text = FormatNumber(oFMFac.GetAvailableCeiling(ofm.ProductCategory, ofm.CreditAccount, ofm.FactoringCeiling - ofm.GiroTolakan, CType(arlFactComponent(2), Decimal), CType(arlFactComponent(3), Decimal), CType(arlFactComponent(4), Decimal), IIf(IsUploading, ofm, Nothing)), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

                lblStatus.Text = enumFactoringStatus.GetStringValue(oFM.Status)
                lblMaxTOPDate.Text = IIf(ofm.MaxTOPDate.Year < 1990, DateSerial(1900, 1, 1).ToString("dd/MMM/yyyy"), ofm.MaxTOPDate.ToString("dd/MMM/yyyy"))
                lblErrorMessage.Text = ofm.ErrorMessage
                lblProductCategory.Text = ofm.ProductCategory.Code
            Else
                lblTotalCeiling.Text = FormatNumber(oFM.TotalCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblStandardCeiling.Text = FormatNumber(oFM.StandardCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblFactoringCeiling.Text = FormatNumber(oFM.FactoringCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblOutstanding.Text = FormatNumber(ofm.Outstanding, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblAvCeiling.Text = FormatNumber(ofm.AvailableCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblPODiajukan.Text = FormatNumber(CType(arlFactComponent(3), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                lblAvailableCeiling.Text = FormatNumber(oFMFac.GetAvailableCeiling(ofm.ProductCategory, ofm.CreditAccount, ofm.FactoringCeiling - ofm.GiroTolakan, CType(arlFactComponent(2), Decimal), CType(arlFactComponent(3), Decimal), CType(arlFactComponent(4), Decimal), IIf(IsUploading, ofm, Nothing)), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

                lblStatus.Text = enumFactoringStatus.GetStringValue(oFM.Status)
                lblMaxTOPDate.Text = IIf(ofm.MaxTOPDate.Year < 1990, DateSerial(1990, 1, 1).ToString("dd/MMM/yyyy"), ofm.MaxTOPDate.ToString("dd/MMM/yyyy"))

                If oFM.FactoringCeiling > 0 Then
                    If ofm.MaxTOPDate < Date.Now Then
                        lblErrorMessage.Text = "Tanggal validitas < tanggal hari ini"
                        e.Item.BackColor = System.Drawing.Color.LightPink
                    ElseIf ofm.MaxTOPDate >= Date.Now And ofm.MaxTOPDate < Date.Now.AddDays(42) Then
                        lblErrorMessage.Text = "Tanggal validitas kurang dari 6 minggu."
                        e.Item.BackColor = System.Drawing.Color.LightPink
                    End If
                End If
                lblProductCategory.Text = ofm.ProductCategory.Code

                If oFM.StandardCeiling < oFM.FactoringCeiling Then
                    lblErrorMessage.Text = "Nilai Standard Ceiling < Factoring Ceiling"
                    e.Item.BackColor = System.Drawing.Color.Yellow
                End If
                If oFM.FactoringCeiling < oFM.Outstanding Then
                    lblErrorMessage.Text = "Over Ceiling"
                    e.Item.BackColor = System.Drawing.Color.Yellow
                End If
            End If

            If ofm.Status = CType(enumFactoringStatus.FactoringStatus.Active, Short) Then
                lbtnActivate.Visible = False
                lbtnDeactivate.Visible = True
            Else
                lbtnActivate.Visible = True
                lbtnDeactivate.Visible = False
            End If
            lblCreatedBy.Text = GetDetailName(ofm.CreatedBy)
            lblLastUpdatedBy.Text = GetDetailName(ofm.LastUpdateBy)
            Dim OutStandCalc As Decimal = Me.GetOutstandingByCalc(ofm.ProductCategory, ofm.CreditAccount)
            lblOutstandingCalc.Text = FormatNumber(OutStandCalc, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            If ofm.Outstanding < OutStandCalc Then
                'ofm.ErrorMessage &= IIf(ofm.ErrorMessage.Trim = "", "", "; ") & "Nilai Outstanding DNet lebih besar"
                lblErrorMessage.Text &= IIf(lblErrorMessage.Text.Trim = "", "", "; ") & "Nilai Outstanding DNet lebih besar"
            End If
            'lblErrorMessage.Text = ofm.ErrorMessage
            If ofm.ErrorMessage <> "" Or lblErrorMessage.Text <> "" Then
                'e.Item.BackColor = System.Drawing.Color.Yellow
            Else
                e.Item.BackColor = dtgMain.ItemStyle.BackColor
            End If
            viewstate.Item("TotalPO") = CType(viewstate.Item("TotalPO"), Decimal) + CType(lblPODiajukan.Text, Decimal)

            If Me.ViewState.Item(Me._mode) = Me._modeUpload Then
                e.Item.Cells(18 + 1).Visible = False ' editing controls
            End If

            If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                e.Item.Cells(5 + 1).Visible = False
            ElseIf objUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                e.Item.Cells(5 + 1).Visible = True
            End If
        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            Dim oFM As FactoringMaster = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex), FactoringMaster)
            Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
            Dim lblNoE As Label = e.Item.FindControl("lblNoE")
            Dim lblCreditAccountE As Label = e.Item.FindControl("lblCreditAccountE")
            Dim lblAccountNameE As Label = e.Item.FindControl("lblAccountNameE")

            Dim txtTotalCeiling As TextBox = e.Item.FindControl("txtTotalCeiling")
            Dim txtStandardCeiling As TextBox = e.Item.FindControl("txtStandardCeiling")
            Dim txtFactoringCeiling As TextBox = e.Item.FindControl("txtFactoringCeiling")
            Dim txtOutstanding As TextBox = e.Item.FindControl("txtOutstanding")
            Dim txtAvCeiling As TextBox = e.Item.FindControl("txtAvCeiling")
            Dim lblTotalCeiling As Label = e.Item.FindControl("lblTotalCeilingE")
            Dim lblStandardCeiling As Label = e.Item.FindControl("lblStandardCeilingE")
            Dim lblFactoringCeiling As Label = e.Item.FindControl("lblFactoringCeilingE")
            Dim lblOutstanding As Label = e.Item.FindControl("lblOutstandingE")
            Dim lblAvCeiling As Label = e.Item.FindControl("lblAvCeilingE")

            Dim lblPODiajukanE As Label = e.Item.FindControl("lblPODiajukanE")
            Dim lblAvailableCeilingE As Label = e.Item.FindControl("lblAvailableCeilingE")
            Dim lblErrorMessageE As Label = e.Item.FindControl("lblErrorMessageE")
            Dim lblStatusE As Label = e.Item.FindControl("lblStatusE")
            Dim calMaxTOPDateE As KTB.DNet.WebCC.IntiCalendar = e.Item.FindControl("calMaxTOPDateE")
            Dim lblCreatedByE As Label = e.Item.FindControl("lblCreatedByE")
            Dim lblLastUpdatedByE As Label = e.Item.FindControl("lblLastUpdatedByE")
            Dim arlFactComponent As ArrayList = oFMFac.GetCeilingComponent(oFM.ProductCategory, oFM.CreditAccount, IIf(IsUploading, ofm, Nothing))
            Dim lblOutstandingCalcE As Label = e.Item.FindControl("lblOutstandingCalcE")
            Dim lblProductCategoryE As Label = e.Item.FindControl("lblProductCategoryE")

            lblNoE.Text = e.Item.ItemIndex + 1
            lblCreditAccountE.Text = ofm.CreditAccount
            lblAccountNameE.Text = ""
            Dim oD As Dealer = New DealerFacade(User).Retrieve(ofm.CreditAccount)
            If Not IsNothing(oD) AndAlso oD.ID > 0 Then
                lblAccountNameE.Text = oD.DealerName
            End If

            txtTotalCeiling.Text = FormatNumber(ofm.TotalCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            txtStandardCeiling.Text = FormatNumber(ofm.StandardCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            txtFactoringCeiling.Text = FormatNumber(ofm.FactoringCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            txtOutstanding.Text = FormatNumber(ofm.Outstanding, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            txtAvCeiling.Text = FormatNumber(ofm.AvailableCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            lblTotalCeiling.Text = FormatNumber(ofm.TotalCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblStandardCeiling.Text = FormatNumber(ofm.StandardCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblFactoringCeiling.Text = FormatNumber(ofm.FactoringCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblOutstanding.Text = FormatNumber(ofm.Outstanding, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblAvCeiling.Text = FormatNumber(ofm.AvailableCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            lblPODiajukanE.Text = FormatNumber(CType(arlFactComponent(3), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblAvailableCeilingE.Text = FormatNumber(ofmfac.GetAvailableCeiling(ofm.ProductCategory, ofm.CreditAccount, ofm.FactoringCeiling - ofm.GiroTolakan, CType(arlFactComponent(2), Decimal), CType(arlFactComponent(3), Decimal), CType(arlFactComponent(4), Decimal), IIf(IsUploading, ofm, Nothing)), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblStatusE.Text = enumFactoringStatus.GetStringValue(oFM.Status)
            calMaxTOPDateE.Value = IIf(ofm.MaxTOPDate.Year < 1990, DateSerial(1990, 1, 1), ofm.MaxTOPDate)
            lblCreatedByE.Text = GetDetailName(ofm.CreatedBy)
            lblLastUpdatedByE.Text = GetDetailName(ofm.LastUpdateBy)
            lblOutstandingCalcE.Text = FormatNumber(Me.GetOutstandingByCalc(ofm.ProductCategory, ofm.CreditAccount), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblProductCategoryE.Text = oFM.ProductCategory.Code

            If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                e.Item.Cells(5 + 1).Visible = False
                txtTotalCeiling.Visible = False
                lblTotalCeiling.Visible = False
                txtStandardCeiling.Visible = False
                lblStandardCeiling.Visible = True
                txtFactoringCeiling.Visible = True
                lblFactoringCeiling.Visible = False
                txtOutstanding.Visible = True
                lblOutstanding.Visible = False
                txtAvCeiling.Visible = True
                lblAvCeiling.Visible = False
                calMaxTOPDateE.Enabled = True
            ElseIf objUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                e.Item.Cells(5 + 1).Visible = True
                txtTotalCeiling.Visible = True
                lblTotalCeiling.Visible = False
                txtStandardCeiling.Visible = True
                lblStandardCeiling.Visible = False
                txtFactoringCeiling.Visible = False
                lblFactoringCeiling.Visible = True
                txtOutstanding.Visible = False
                lblOutstanding.Visible = True
                txtAvCeiling.Visible = False
                lblAvCeiling.Visible = True
                calMaxTOPDateE.Enabled = False
            End If
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
        Me.ViewState.Item(Me._mode) = Me._modeUpload
        BindDTG()
    End Sub
    Private Function GetProductCategory() As ProductCategory
        Dim oPC As ProductCategory = New ProductCategoryFacade(User).Retrieve(CType(Me.ddlProductCategory.SelectedValue, Integer))
        oPC.MarkLoaded()
        Return oPC
    End Function
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
        Dim nError As Integer = 0
        Dim arlTemp As ArrayList = CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)
        Dim oFMDB As FactoringMaster

        For Each oFM As FactoringMaster In arlTemp
            oFMDB = oFMFac.Retrieve(oFM.ProductCategory, ofm.CreditAccount)
            If Not IsNothing(oFMDB) AndAlso oFMDB.ID > 0 Then
                oFMDB.TotalCeiling = ofm.TotalCeiling
                oFMDB.StandardCeiling = ofm.StandardCeiling
                oFMDB.FactoringCeiling = ofm.FactoringCeiling
                oFMDB.GiroTolakan = ofm.GiroTolakan
                oFMDB.Outstanding = ofm.Outstanding
                oFMDB.AvailableCeiling = ofm.AvailableCeiling
                oFMDB.MaxTOPDate = ofm.MaxTOPDate
                oFMDB.ProductCategory = oFM.ProductCategory
                oFMDB.LastUploadedTime = Now
                oFMDB.LastUploadedBy = User.Identity.Name
                nError += IIf(oFMFac.Update(oFMDB) < 0, 1, 0)
            Else
                nError += IIf(oFMFac.Insert(oFM) < 0, 1, 0)
            End If
        Next
        If nError <= 0 Then
            MessageBox.Show(SR.SaveSuccess)
        ElseIf nError = arlTemp.Count Then
            MessageBox.Show(SR.SaveFail)
            Exit Sub
        Else
            MessageBox.Show(SR.SaveSuccess & ", data gagal disimpan sejumlah " & nError.ToString)
        End If

        Dim sCAs As String = ""
        For Each oFM As FactoringMaster In arlTemp
            sCAs &= IIf(sCAs.Trim = "", "", ";") & ofm.CreditAccount
        Next
        Me.txtCreditAccount.Text = sCAs
        Me.ViewState.Item(Me._mode) = Me._modeView
        BindDTG()
    End Sub

    Private Sub dtgMain_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.EditCommand
        Me.ViewState.Item(Me._mode) = Me._modeEdit
        dtgMain.EditItemIndex = e.Item.ItemIndex
        BindDTG()

    End Sub

    Private Sub dtgMain_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.CancelCommand
        Me.dtgMain.EditItemIndex = -1
        BindDTG()
    End Sub

    Private Sub dtgMain_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.UpdateCommand

        Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
        Dim oFMOri As FactoringMaster
        Dim oFM As FactoringMaster

        If (hdnValidate.Value = "-1") Then
            oFM = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex), FactoringMaster)
            oFMOri = oFM
            Dim lblPODiajukanE As Label = e.Item.FindControl("lblPODiajukanE")
            Dim lblAvailableCeilingE As Label = e.Item.FindControl("lblAvailableCeilingE")
            Dim lblErrorMessageE As Label = e.Item.FindControl("lblErrorMessageE")
            Dim lblStatusE As Label = e.Item.FindControl("lblStatusE")
            Dim calMaxTOPDateE As KTB.DNet.WebCC.IntiCalendar = e.Item.FindControl("calMaxTOPDateE")

            objUser = CType(_sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.LEASING Then
                Dim lblTotalCeiling As Label = e.Item.FindControl("lblTotalCeilingE")
                Dim lblStandardCeiling As Label = e.Item.FindControl("lblStandardCeilingE")
                Dim txtFactoringCeiling As TextBox = e.Item.FindControl("txtFactoringCeiling")
                Dim txtOutstanding As TextBox = e.Item.FindControl("txtOutstanding")
                Dim txtAvCeiling As TextBox = e.Item.FindControl("txtAvCeiling")
                oFM.MaxTOPDate = calMaxTOPDateE.Value
                If txtFactoringCeiling.Text.Trim <> "" Then
                    If CDec(txtFactoringCeiling.Text.Trim) > CDec(lblStandardCeiling.Text.Trim) Then
                        MessageBox.Show("Factoring Ceiling lebih besar Standard Ceiling")
                        Exit Sub
                    End If
                    oFM.FactoringCeiling = CType(txtFactoringCeiling.Text, Decimal)
                Else
                    oFM.FactoringCeiling = 0
                End If
                If txtOutstanding.Text.Trim <> "" Then
                    oFM.Outstanding = CType(txtOutstanding.Text, Decimal)
                Else
                    oFM.Outstanding = 0
                End If
                'If oFM.FactoringCeiling < oFM.Outstanding Then
                '    MessageBox.Show("Factoring Ceiling lebih kecil dari Outstanding")
                '    Exit Sub
                'End If
                oFM.AvailableCeiling = oFM.FactoringCeiling - oFM.Outstanding
                If oFM.TotalCeiling < oFM.FactoringCeiling Then
                    MessageBox.Show("Total Ceiling lebih kecil dari Factoring Ceiling")
                    Exit Sub
                End If
                If calMaxTOPDateE.Value < Date.Now Then
                    MessageBox.Show("Tanggal validitas < tanggal hari ini")
                    Exit Sub
                ElseIf calMaxTOPDateE.Value >= Date.Now And calMaxTOPDateE.Value < Date.Now.AddDays(42) Then
                    MessageBox.Show("Tanggal validitas kurang dari 6 minggu.")
                End If
            ElseIf objUser.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                oFM.MaxTOPDate = calMaxTOPDateE.Value
                Dim txtTotalCeiling As TextBox = e.Item.FindControl("txtTotalCeiling")
                Dim txtStandardCeiling As TextBox = e.Item.FindControl("txtStandardCeiling")
                If Not IsNothing(txtTotalCeiling) And Not IsNothing(txtStandardCeiling) Then
                    If txtTotalCeiling.Text.Trim <> "" Then
                        oFM.TotalCeiling = CType(txtTotalCeiling.Text.Trim, Decimal)
                    Else
                        oFM.TotalCeiling = 0
                    End If
                    If txtStandardCeiling.Text.Trim <> "" Then
                        oFM.StandardCeiling = CType(txtStandardCeiling.Text.Trim, Decimal)
                    Else
                        oFM.StandardCeiling = 0
                    End If
                    If oFM.TotalCeiling < oFM.FactoringCeiling Then
                        MessageBox.Show("Total Ceiling lebih kecil dari Factoring Ceiling")
                        Exit Sub
                    End If
                    If oFM.StandardCeiling < oFM.FactoringCeiling Then
                        MessageBox.Confirm("Nilai Standard Ceiling < Factoring Ceiling. Yakin ingin simpan?", "hdnValidate")
                        Me._sessHelper.SetSession("oFM", oFM)
                        Return
                    End If
                    If oFM.TotalCeiling < oFM.StandardCeiling Then
                        MessageBox.Show("Total Ceiling lebih kecil dari Standard Ceiling")
                        Exit Sub
                    End If
                End If
            End If

        Else
            oFM = CType(_sessHelper.GetSession("oFM"), FactoringMaster)
            UpdateFactoringMaster(oFM)
        End If
        If viewstate.Item(Me._mode) = Me._modeUpload Then
            CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex) = oFM
            Me._sessHelper.SetSession(Me._sessDataUpload, Me._sessHelper.GetSession(Me._sessData))
        Else
            If oFMFac.Update(oFM) = -1 Then
                Dim arlTemp As ArrayList = CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)
                arlTemp(e.Item.ItemIndex) = oFMOri
            End If
            Me.ViewState.Item(Me._mode) = Me._modeView
        End If


        Me.dtgMain.EditItemIndex = -1
        BindDTG()
        MessageBox.Show("Simpan Berhasil")

    End Sub

    Private Function UpdateFactoringMaster(ByVal oFM As FactoringMaster) As Integer
        Dim iReturn As Integer = 0
        Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
        iReturn = oFMFac.Update(oFM)
        Return iReturn
    End Function
    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        If e.CommandName.Trim.ToUpper = "Activate".ToUpper Then
            Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
            Dim oFM As FactoringMaster = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex), FactoringMaster)

            If oFM.Status <> enumFactoringStatus.FactoringStatus.Active Then
                oFM.Status = enumFactoringStatus.FactoringStatus.Active
                If oFMFac.Update(oFM) = -1 Then
                    oFM.Status = enumFactoringStatus.FactoringStatus.InActive
                    MessageBox.Show("Aktifasi gagal")
                Else
                    BindDTG()
                End If
            End If

        ElseIf e.CommandName.Trim.ToUpper = "Deactivate".ToUpper Then
            Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
            Dim oFM As FactoringMaster = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex), FactoringMaster)

            If oFM.Status <> enumFactoringStatus.FactoringStatus.InActive Then
                oFM.Status = enumFactoringStatus.FactoringStatus.InActive
                If oFMFac.Update(oFM) = -1 Then
                    oFM.Status = enumFactoringStatus.FactoringStatus.Active
                    MessageBox.Show("Non-Aktifasi gagal")
                Else
                    BindDTG()
                End If
            End If
        End If
    End Sub

    Private Sub btnTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Dim aFs As New ArrayList
        Dim oFM As FactoringMaster

        For Each di As DataGridItem In Me.dtgMain.Items
            If CType(di.FindControl("chkExport"), CheckBox).Checked Then
                oFM = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(di.ItemIndex), FactoringMaster)
                If oFM.ID > 0 Then
                    aFs.Add(oFM)
                End If
            End If
        Next
        If aFs.Count > 0 Then
            Dim _fileHelper As New FileHelper
            Dim str As FileInfo
            Try
                str = _fileHelper.TransferFactoringToSAP(aFs)
                MessageBox.Show(SR.UploadSucces(str.Name))
            Catch ex As Exception
                MessageBox.Show(SR.UploadFail(str.Name))
            End Try
        End If
    End Sub

#End Region
End Class
