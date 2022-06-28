#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.MDP
Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade.FinishUnit
#End Region


Public Class FrmAdequacyReport
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCreditAccount As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents calRequestDelDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtReportDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents ddlFactoringStatus As System.Web.UI.WebControls.DropDownList
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
    Private _sortingField As String = "SortingField"
    Private _sortingMode As String = "SortingMode"
    Private _totalCeiling As String = "TotalCeiling"
    Private _sessHelper As New SessionHelper
    Private _sessData As String = "DataToDisplay"
#End Region

#Region "Custom Methods"

    Private Sub Initialization()
        viewstate.Add(Me._sortingField, "ID")
        viewstate.Add(Me._sortingMode, Sort.SortDirection.ASC)
        viewstate.Add(Me._totalCeiling, 0)
        Me._sessHelper.SetSession(Me._sessData, New ArrayList)
        Me.txtReportDate.Text = Now.ToString("dd/MM/yyyy")
        dtgMain.DataSource = New ArrayList
        dtgMain.DataBind()
        Me.btnDownload.Enabled = False
        Dim oD As Dealer = CType(Me._sessHelper.GetSession("DEALER"), Dealer)
        If oD.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Me.txtCreditAccount.Text = oD.CreditAccount
            Me.txtCreditAccount.Enabled = False
            Me.txtDealerName.Text = oD.DealerName
        Else
            Me.txtCreditAccount.Text = ""
            Me.txtCreditAccount.Enabled = True
            Me.txtDealerName.Text = ""
        End If

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True, companyCode)
        ddlProductCategory.Items.RemoveAt(0)

        With Me.ddlFactoringStatus.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", 0))
            .Add(New ListItem("Aktif", 1))
            .Add(New ListItem("Non-Aktif", 2))
        End With

    End Sub

    Private Function GetProductCategoryID() As Short
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
    End Function

    Private Function GetProductCategory() As ProductCategory
        Dim oPCFac As New ProductCategoryFacade(User)
        Dim oPC As ProductCategory
        oPC = oPCFac.Retrieve(Me.GetProductCategoryID())
        If IsNothing(oPC) Then oPC = New ProductCategory
        Return oPC
    End Function

    Private Sub BindDTG(ByVal iPage As Integer)
        'Clear Session first
        _sessHelper.SetSession("FrmAdequacyReport.IsAutoBind", False)
        _sessHelper.SetSession("FrmAdequacyReport.CreditAccount", "")
        _sessHelper.SetSession("FrmAdequacyReport.ReqDeliveryDate", Now)
        _sessHelper.SetSession("FrmAdequacyReport.PageIndex", iPage)

        Dim arlFM As New ArrayList
        viewstate.Item(Me._totalCeiling) = 0
        lblTotal.Text = viewstate.Item(Me._totalCeiling)
        arlFM = Me.GetDataFromDB()
        Me._sessHelper.SetSession(Me._sessData, arlFM)

        dtgMain.DataSource = arlFM
        dtgMain.DataBind()
        Me.btnDownload.Enabled = (arlFM.Count > 0)
    End Sub

    Private Function GetDataFromDB() As ArrayList
        Dim cFM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sFM As SortCollection = New SortCollection
        Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
        Dim Sql As String = String.Empty

        sFM.Add(New Sort(GetType(FactoringMaster), viewstate.Item(Me._sortingField), viewstate.Item(Me._sortingMode)))
        If Me.txtCreditAccount.Text.Trim <> "" Then
            Dim sCAs As String = Me.txtCreditAccount.Text.Trim.Replace(";", "','")

            cFM.opAnd(New Criteria(GetType(FactoringMaster), "CreditAccount", MatchType.InSet, "('" & sCAs & "')"))
        End If
        If CType(Me.ddlFactoringStatus.SelectedValue, Integer) = 1 Then 'Aktif
            Sql &= " ( select tc.Status from TransactionControl tc, Dealer d where tc.RowStatus=" & CType(DBRowStatus.Active, Short).ToString & " and tc.DealerID=d.ID and tc.Kind=" & CType(EnumDealerTransType.DealerTransKind.Factoring, Short).ToString & " and d.DealerCode=FactoringMaster.CreditAccount ) "
            cFM.opAnd(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.No, Sql))
        ElseIf CType(Me.ddlFactoringStatus.SelectedValue, Integer) = 2 Then 'Non-Aktif
            Sql &= " ( select tc.Status from TransactionControl tc, Dealer d where tc.RowStatus=" & CType(DBRowStatus.Active, Short).ToString & " and tc.DealerID=d.ID and tc.Kind=" & CType(EnumDealerTransType.DealerTransKind.Factoring, Short).ToString & " and d.DealerCode=FactoringMaster.CreditAccount ) "
            cFM.opAnd(New Criteria(GetType(FactoringMaster), "RowStatus", MatchType.Exact, Sql))
        End If
        Dim PCID As Short = Me.GetProductCategoryID()
        If PCID > 0 Then
            cFM.opAnd(New Criteria(GetType(FactoringMaster), "ProductCategory.ID", MatchType.Exact, PCID))
        End If
        Return oFMFac.Retrieve(cFM, sFM)
    End Function


    Private Sub DoDownload()
        Dim sFileName As String
        Dim fullFileName As String
        sFileName = "Ceiling Adequecy Report " & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

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
                WriteData(sw, CType(Me._sessHelper.GetSession(Me._sessData), ArrayList))

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

    Private Sub WriteData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Credit Account" & tab)
            itemLine.Append("Produk" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Nilai Ceiling" & tab)
            itemLine.Append("Outstanding" & tab)
            itemLine.Append("Ceiling Tersedia" & tab)
            itemLine.Append("PO Telah Diajukan" & tab)
            itemLine.Append("Sisa Ceiling" & tab)

            sw.WriteLine(itemLine.ToString())

            For Each di As DataGridItem In Me.dtgMain.Items
                Dim lblCreditAccount As Label = di.FindControl("lblCreditAccount")
                Dim lblAccountName As Label = di.FindControl("lblAccountName")
                Dim lblCeiling As Label = di.FindControl("lblCeiling")
                Dim lblOutstanding As Label = di.FindControl("lblOutstanding")
                Dim lblAvailableCeiling As Label = di.FindControl("lblAvailableCeiling")
                Dim lblPODiajukan As Label = di.FindControl("lblPODiajukan")
                Dim lblRemainCeiling As Label = di.FindControl("lblRemainCeiling")
                Dim lblProductCategory As Label = di.FindControl("lblProductCategory")

                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(di.ItemIndex + 1 & tab)

                itemLine.Append(lblCreditAccount.Text & tab)
                itemLine.Append(lblProductCategory.Text & tab)
                itemLine.Append(lblAccountName.Text & tab)
                itemLine.Append(lblCeiling.Text & tab)
                itemLine.Append(lblOutstanding.Text & tab)
                itemLine.Append(lblAvailableCeiling.Text & tab)
                itemLine.Append(lblPODiajukan.Text & tab)
                itemLine.Append(lblRemainCeiling.Text & tab)

                sw.WriteLine(itemLine.ToString())
            Next
        End If
    End Sub
    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Factoring_ceiling_lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Ceiling Master")
        End If
    End Sub

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckUserPrivilege()
        If Not IsPostBack Then
            Initialization()
            If Not _sessHelper.GetSession("FrmAdequacyReport.IsAutoBind") Is Nothing Then
                If CType(_sessHelper.GetSession("FrmAdequacyReport.IsAutoBind"), Boolean) = True Then
                    txtCreditAccount.Text = CType(_sessHelper.GetSession("FrmAdequacyReport.CreditAccount"), String)
                    Me.calRequestDelDate.Value = CType(_sessHelper.GetSession("FrmAdequacyReport.ReqDeliveryDate"), Date)
                    txtReportDate.Text = _sessHelper.GetSession("FrmAdequacyReport.ReportDate")
                    Me.ddlProductCategory.SelectedValue = CType(_sessHelper.GetSession("FrmAdequacyReport.ProductCategoryID"), Integer)
                    BindDTG(CType(_sessHelper.GetSession("FrmAdequcyReport.PageIndex"), Integer))

                End If
            End If
        End If
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        BindDTG(0)
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oFM As FactoringMaster = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex), FactoringMaster)
            Dim oFMFac As FactoringMasterFacade = New FactoringMasterFacade(User)
            Dim lblNo As Label = e.Item.FindControl("lblNo")
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
            Dim lblAccountName As Label = e.Item.FindControl("lblAccountName")
            Dim lblCeiling As Label = e.Item.FindControl("lblCeiling")
            Dim lblOutstanding As Label = e.Item.FindControl("lblOutstanding")
            Dim lblAvailableCeiling As Label = e.Item.FindControl("lblAvailableCeiling")
            Dim lblPODiajukan As Label = e.Item.FindControl("lblPODiajukan")
            Dim lblRemainCeiling As Label = e.Item.FindControl("lblRemainCeiling")
            Dim oPC As ProductCategory = oFM.ProductCategory
            Dim arlFactComponent As ArrayList = oFMFac.GetCeilingComponent(oPC, oFM.CreditAccount)
            Dim calMaxTOPDate As KTB.DNet.WebCC.IntiCalendar = e.Item.FindControl("calMaxTOPDate")
            Dim lblKeterangan As Label = e.Item.FindControl("lblKeterangan")
            Dim lblProductCategory As Label = e.Item.FindControl("lblProductCategory")

            lblNo.Text = e.Item.ItemIndex + 1
            lblCreditAccount.Text = oFM.CreditAccount
            lblProductCategory.Text = oPC.Code
            lblAccountName.Text = ""
            Dim oD As Dealer = New DealerFacade(User).Retrieve(oFM.CreditAccount)
            If Not IsNothing(oD) AndAlso oD.ID > 0 Then
                lblAccountName.Text = oD.DealerName
            End If
            lblCeiling.Text = FormatNumber(oFM.FactoringCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblOutstanding.Text = FormatNumber(oFM.Outstanding, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            Dim totalPODraft As Decimal = GetTotalPoDraft(oFM.CreditAccount, oFM.ProductCategory.ID)
            lblAvailableCeiling.Text = FormatNumber(oFM.FactoringCeiling - oFM.Outstanding, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            lblPODiajukan.Text = FormatNumber(CType(arlFactComponent(3), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            lblRemainCeiling.Text = FormatNumber(oFMFac.GetAvailableCeiling(oPC, oFM.CreditAccount, oFM.FactoringCeiling - oFM.GiroTolakan, CType(arlFactComponent(2), Decimal), CType(arlFactComponent(3), Decimal), CType(arlFactComponent(4), Decimal)) - totalPODraft, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

            calMaxTOPDate.Value = IIf(oFM.MaxTOPDate.Year < 1900, DateSerial(1900, 1, 1), oFM.MaxTOPDate)
            calMaxTOPDate.Enabled = False
            viewstate.Item(Me._totalCeiling) = CType(viewstate.Item(Me._totalCeiling), Decimal) + CType(lblRemainCeiling.Text, Decimal)

            'Anh 20110511 req by Riyadi
            'start Tambahan kolom keterangan
            If oFM.FactoringCeiling > 0 Then
                If oFM.MaxTOPDate < Date.Now Then
                    lblKeterangan.Text = "Tanggal validitas < tanggal hari ini"
                    e.Item.BackColor = System.Drawing.Color.LightPink
                ElseIf oFM.MaxTOPDate >= Date.Now And oFM.MaxTOPDate < Date.Now.AddDays(42) Then
                    lblKeterangan.Text = "Tanggal validitas kurang dari 6 minggu."
                    e.Item.BackColor = System.Drawing.Color.LightPink
                End If
            End If

            If oFM.StandardCeiling < oFM.FactoringCeiling Then
                lblKeterangan.Text = "Nilai Standard Ceiling < Factoring Ceiling"
                e.Item.BackColor = System.Drawing.Color.Yellow
            End If
            If oFM.FactoringCeiling < oFM.Outstanding Then
                lblKeterangan.Text = "Over Ceiling"
                e.Item.BackColor = System.Drawing.Color.Yellow
            End If
            'end Tambahan kolom keterangan
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            lblTotal.Text = FormatNumber(CType(viewstate.Item(Me._totalCeiling), Decimal), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
    End Sub

    Private Function GetTotalPoDraft(ByVal creditAccount As String, ByVal productCategoryId As Short) As Decimal
        Dim result As Decimal = 0
        Try
            CreateFiveDays()
            Dim arlProposed As New ArrayList
            Dim TotalProposed(5) As Decimal
            TotalProposed(0) = 0
            TotalProposed(1) = 0
            TotalProposed(2) = 0
            TotalProposed(3) = 0
            TotalProposed(4) = 0
            Dim objPOHFac As PODraftHeaderFacade = New PODraftHeaderFacade(User)
            Dim arlPOH As New ArrayList
            Dim objProposed As clsProposed

            Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtPOH.opAnd(New Criteria(GetType(PODraftHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, creditAccount))
            crtPOH.opAnd(New Criteria(GetType(PODraftHeader), "Status", MatchType.InSet, "(0)"))
            crtPOH.opAnd(New Criteria(GetType(PODraftHeader), "IsFactoring", MatchType.Exact, 1))
            crtPOH.opAnd(New Criteria(GetType(PODraftHeader), "ContractHeader.PKHeader.Category.ProductCategory.ID", MatchType.Exact, productCategoryId))

            arlPOH = objPOHFac.Retrieve(crtPOH)

            For Each objPOH As PODraftHeader In arlPOH
                'If objPOH.DailyPayments.Count < 1 Then

                objProposed = New clsProposed
                objProposed.PONumber = objPOH.DraftPONumber '& "ProposedPO"
                'objProposed.SONumber = objPOH.SONumber
                objProposed.POStatus = GetPOStatusValue(CType(objPOH.Status, Integer))
                objProposed.AmountDate1 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(0), Date), "yyyy.MM.dd"), GetTotalPODraftDetail(objPOH), 0)
                objProposed.AmountDate2 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(1), Date), "yyyy.MM.dd"), GetTotalPODraftDetail(objPOH), 0)
                objProposed.AmountDate3 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(2), Date), "yyyy.MM.dd"), GetTotalPODraftDetail(objPOH), 0)
                objProposed.AmountDate4 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(3), Date), "yyyy.MM.dd"), GetTotalPODraftDetail(objPOH), 0)
                objProposed.AmountDate5 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(_sessHelper.GetSession("arlReportDate"), ArrayList)(4), Date), "yyyy.MM.dd"), GetTotalPODraftDetail(objPOH), 0)
                objProposed.IsRequestedPO = True
                arlProposed.Add(objProposed)

                TotalProposed(0) = TotalProposed(0) + objProposed.AmountDate1
                TotalProposed(1) = TotalProposed(1) + objProposed.AmountDate2
                TotalProposed(2) = TotalProposed(2) + objProposed.AmountDate3
                TotalProposed(3) = TotalProposed(3) + objProposed.AmountDate4
                TotalProposed(4) = TotalProposed(4) + objProposed.AmountDate5
                'End If
            Next

            result = TotalProposed(0) + TotalProposed(1) + TotalProposed(2) + TotalProposed(3) + TotalProposed(4)

        Catch ex As Exception
            result = 0
        End Try
        Return result
    End Function

    Private Sub CreateFiveDays()
        Dim ReportDate As Date = CType(txtReportDate.Text, Date)
        Dim i As Integer
        Dim arlDate As New ArrayList
        ReportDate = ReportDate.AddDays(-1)
        For i = 0 To 4
            ReportDate = CommonFunction.AddNWorkingDay(ReportDate, 1)
            arlDate.Add(ReportDate)
        Next
        _sessHelper.SetSession("arlReportDate", arlDate)
    End Sub

    Private Function GetPOStatusValue(ByVal POStatus As Integer) As String
        Dim arlPOStatus As ArrayList = CType(LookUp.ArrayStatusPO, ArrayList)
        For Each li As ListItem In arlPOStatus
            If li.Value = POStatus Then Return li.Text
        Next

    End Function

    Private Function GetTotalPODraftDetail(ByRef objPOH As PODraftHeader) As Decimal
        Dim Total As Decimal = 0
        Total = objPOH.TotalPODetail()
        Return Total
    End Function

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
        BindDTG(0)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        DoDownload()
    End Sub

#End Region

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        If e.CommandName = "Detail" Then
            Dim lblCreditAccount As Label = e.Item.FindControl("lblCreditAccount")
            Dim lblOutstanding As Label = e.Item.FindControl("lblOutstanding")
            Dim lblPODiajukan As Label = e.Item.FindControl("lblPODiajukan")
            Dim lblCeiling As Label = e.Item.FindControl("lblCeiling")
            'Dim lblAvailableCeiling As Label = e.Item.FindControl("lblAvailableCeiling")
            'Dim lblRemainCeiling As Label = e.Item.FindControl("lblRemainCeiling")
            Dim oFM As FactoringMaster = CType(CType(Me._sessHelper.GetSession(Me._sessData), ArrayList)(e.Item.ItemIndex), FactoringMaster)

            'Make Session with current criteria/parameters
            _sessHelper.SetSession("FrmAdequacyReport.IsAutoBind", True)
            _sessHelper.SetSession("FrmAdequacyReport.CreditAccount", txtCreditAccount.Text)
            _sessHelper.SetSession("FrmAdequacyReport.ProductCategoryID", CType(Me.ddlProductCategory.SelectedValue, Integer))
            _sessHelper.SetSession("FrmAdequacyReport.ReqDeliveryDate", Me.calRequestDelDate.Value)
            _sessHelper.SetSession("FrmAdequacyReport.ReportDate", txtReportDate.Text)
            _sessHelper.SetSession("FrmAdequacyReport.PageIndex", dtgMain.CurrentPageIndex)


            Response.Redirect("FrmAdequacyReportDetail.aspx?CreditAccount=" & lblCreditAccount.Text & _
                "&ProductCategoryID=" & oFM.ProductCategory.ID.ToString() & _
                "&Ceiling=" & lblCeiling.Text & _
                "&ReportDate=" & txtReportDate.Text & _
                "&ReqDeliveryDate=" & calRequestDelDate.Value & "")
        End If
    End Sub

    Private Class clsProposed
        Private _pONumber As String
        Private _sONumber As String
        Private _pOStatus As String
        Private _amountDate1 As Decimal
        Private _amountDate2 As Decimal
        Private _amountDate3 As Decimal
        Private _amountDate4 As Decimal
        Private _amountDate5 As Decimal
        Private _isRequestedPO As Boolean

        Public Property PONumber() As String
            Get
                Return _pONumber
            End Get
            Set(ByVal Value As String)
                _pONumber = Value
            End Set
        End Property

        Public Property SONumber() As String
            Get
                Return _sONumber
            End Get
            Set(ByVal Value As String)
                _sONumber = Value
            End Set
        End Property

        Public Property POStatus() As String
            Get
                Return _pOStatus
            End Get
            Set(ByVal Value As String)
                _pOStatus = Value
            End Set
        End Property

        Public Property AmountDate1() As Decimal
            Get
                Return _amountDate1
            End Get
            Set(ByVal Value As Decimal)
                _amountDate1 = Value
            End Set
        End Property

        Public Property AmountDate2() As Decimal
            Get
                Return _amountDate2
            End Get
            Set(ByVal Value As Decimal)
                _amountDate2 = Value
            End Set
        End Property
        Public Property AmountDate3() As Decimal
            Get
                Return _amountDate3
            End Get
            Set(ByVal Value As Decimal)
                _amountDate3 = Value
            End Set
        End Property
        Public Property AmountDate4() As Decimal
            Get
                Return _amountDate4
            End Get
            Set(ByVal Value As Decimal)
                _amountDate4 = Value
            End Set
        End Property
        Public Property AmountDate5() As Decimal
            Get
                Return _amountDate5
            End Get
            Set(ByVal Value As Decimal)
                _amountDate5 = Value
            End Set
        End Property

        Public Property IsRequestedPO() As Boolean
            Get
                Return _isRequestedPO
            End Get
            Set(ByVal Value As Boolean)
                _isRequestedPO = Value
            End Set
        End Property
    End Class
End Class
