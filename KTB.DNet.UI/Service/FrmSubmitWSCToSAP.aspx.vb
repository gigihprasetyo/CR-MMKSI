#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.Text
Imports System.IO
#End Region

Public Class FrmSubmitWSCToSAP
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgWSCInfo As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkdownload As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtDownload As System.Web.UI.WebControls.TextBox
    Protected WithEvents icCreatedDateEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icCreatedDateStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Custom Variable Declaration"
    Private _sessHelper As SessionHelper = New SessionHelper
    Private _ListDealerWSC As ArrayList
    Private _ListDealerWSCForDTG As ArrayList
    Private _CompleteListDealerWSCForDTG As ArrayList
    Private _nWSCCount As Integer = 0
    Private _bIsJustDownloadedToSAP As Boolean = False
    Dim dt As DateTime = DateTime.Now
    Private V_Suffix As String = "Static"
    'Dim sSuffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
    Private objDealer As Dealer
#End Region

#Region "Custom Method"

    Private Function sSuffix() As String
        If Not IsNothing(VIewState(V_Suffix)) Then
            Return CType(VIewState(V_Suffix), String)
        Else
            Return DateTime.Now.ToString("yyyyMMddHHmmss")
        End If

    End Function

    Public Function SaveWSCToServer(ByVal ListWSC As ArrayList) As String
        Dim objStringBuilder As StringBuilder
        Dim sMessage As String = ""
        Dim delimiter As String = ""","""

        Dim ChassisData As String = Server.MapPath("") & "\..\DataTemp\WSCData" & sSuffix() & ".txt"
        Dim objFileStream As New FileStream(ChassisData, FileMode.Append, FileAccess.Write)
        Dim objStreamWriter As New StreamWriter(objFileStream)
        Try
            For Each wsc As WSCHeader In ListWSC
                For Each wscDetail As wscDetail In wsc.WSCDetails
                    Try
                        objStringBuilder = New StringBuilder
                        objStringBuilder.Append("""")
                        objStringBuilder.Append(wsc.ClaimType)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.Dealer.DealerCode)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.ClaimNumber)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.RefClaimNumber)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.ChassisMaster.ChassisNumber)
                        objStringBuilder.Append(delimiter)
                        'objStringBuilder.Append(wsc.ServiceDate.Day.ToString.PadLeft(2, "0") & _
                        '                        wsc.ServiceDate.Month.ToString.PadLeft(2, "0") & _
                        '                        wsc.ServiceDate.Year.ToString.Substring(2, 2))
                        objStringBuilder.Append(wsc.ReleaseDate.Day.ToString.PadLeft(2, "0") & _
                                                wsc.ReleaseDate.Month.ToString.PadLeft(2, "0") & _
                                                wsc.ReleaseDate.Year.ToString.Substring(2, 2))
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.Miliage.ToString)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.PQR)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.PQRStatus)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.CodeA)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.CodeB)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.CodeC)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(Strings.Left(wsc.Description, 50))
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.EvidencePhoto)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.EvidenceInvoice)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wsc.EvidenceDmgPart)
                        objStringBuilder.Append(delimiter)

                        objStringBuilder.Append(wscDetail.WSCType)
                        objStringBuilder.Append(delimiter)
                        If wscDetail.WSCType = "L" Then
                            If Not IsNothing(wscDetail.LaborMaster) Then
                                objStringBuilder.Append(wscDetail.LaborMaster.LaborCode)
                                objStringBuilder.Append(delimiter)
                                objStringBuilder.Append(wscDetail.LaborMaster.WorkCode)
                                objStringBuilder.Append(delimiter)
                            Else
                                sMessage &= " Data Labor untuk No. Klaim " & wsc.ClaimNumber & " tidak ada."
                            End If


                            objStringBuilder.Append(wscDetail.Quantity.ToString.Replace( _
                                System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ","))
                            'masih nunggu sap
                            objStringBuilder.Append(delimiter)
                            objStringBuilder.Append(String.Format("{0:#}", wscDetail.PartPrice))
                            objStringBuilder.Append(delimiter)
                            objStringBuilder.Append("")

                        ElseIf wscDetail.WSCType = "P" Then
                            If Not IsNothing(wscDetail.SparePartMaster) Then
                                objStringBuilder.Append(wscDetail.SparePartMaster.PartNumber)
                                objStringBuilder.Append(delimiter)
                            Else
                                sMessage &= " Data Spare Part untuk No. Klaim " & wsc.ClaimNumber & " tidak ada."
                            End If

                            objStringBuilder.Append(wscDetail.Quantity.ToString.Replace( _
                                System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ","))
                            objStringBuilder.Append(delimiter)
                            objStringBuilder.Append(String.Format("{0:#}", wscDetail.PartPrice))
                            objStringBuilder.Append(delimiter)
                            objStringBuilder.Append("")
                            'masih nunggu sap
                            objStringBuilder.Append(delimiter)
                            If wscDetail.MainPart = 1 Then
                                objStringBuilder.Append("X")
                            Else
                                objStringBuilder.Append("")
                            End If

                        End If
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wscDetail.WSCHeader.EvidenceRepair)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wscDetail.WSCHeader.EvidenceWSCLetter)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wscDetail.WSCHeader.EvidenceWSCTechnical)
                        objStringBuilder.Append(delimiter)
                        objStringBuilder.Append(wscDetail.WSCHeader.FailureDate.ToString("ddMMyyyy"))
                        objStringBuilder.Append("""")
                        objStreamWriter.WriteLine(objStringBuilder.ToString)
                    Catch ex As Exception
                        sMessage = ex.Message
                    End Try
                Next
            Next
            objStreamWriter.Close()
            objFileStream.Close()
        Catch ex As Exception
            MessageBox.Show("Download gagal!")
        End Try




        Return sMessage

    End Function

    Private Sub saveToFile()
        'Response.Redirect("../DownloadContainer.aspx?file=DataTemp\WSCData" & sSuffix & ".txt" & "&from=Service/FrmSubmitWSCToSAP.aspx")
        Me.txtDownload.Text = "DataTemp\WSCData" & sSuffix() & ".txt"

        BindDealerWSC()

    End Sub


    Private Sub checkFileExistenceToDownload()
        Dim finfo As FileInfo = New FileInfo(Server.MapPath("") & "\..\DataTemp\WSCData" & sSuffix() & ".txt")
        If finfo.Exists Then
            finfo.Delete()
        End If

    End Sub

    Private Sub SortListControl(ByRef pCompletelistWSC As ArrayList, ByVal SortColumn As String, _
    ByVal SortDirection As Integer)

        Dim IsAsc As Boolean = True
        If SortDirection = Sort.SortDirection.ASC Then
            IsAsc = True
        ElseIf SortDirection = Sort.SortDirection.DESC Then
            IsAsc = False
        End If

        Dim objListComparer As IComparer = New ListComparer(IsAsc, SortColumn)
        pCompletelistWSC.Sort(objListComparer)

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

    Private Sub BindDealerWSC()
        Dim criterias As CriteriaComposite

        Dim sortColl As SortCollection = New SortCollection

        Dim StartKirim As New DateTime(CInt(icCreatedDateStart.Value.Year), CInt(icCreatedDateStart.Value.Month), CInt(icCreatedDateStart.Value.Day), 0, 0, 0)
        Dim EndKirim As New DateTime(CInt(icCreatedDateEnd.Value.Year), CInt(icCreatedDateEnd.Value.Month), CInt(icCreatedDateEnd.Value.Day), 23, 59, 59)

        If chkdownload.Checked Then
            sortColl.Add(New Sort(GetType(view_DealerWSCProccessed), "DealerCode", Sort.SortDirection.ASC))
            'criterias = New CriteriaComposite(New Criteria(GetType(view_DealerWSCProccessed), "CreatedDate", MatchType.Greater, Format(Me.icCreatedDateStart.Value.AddDays(-1), "yyyy/MM/dd")))
            'criterias.opAnd(New Criteria(GetType(view_DealerWSCProccessed), "CreatedDate", MatchType.Lesser, Format(Me.icCreatedDateEnd.Value.AddDays(1), "yyyy/MM/dd")))
            'criterias = New CriteriaComposite(New Criteria(GetType(view_DealerWSCProccessed), "CreatedDate", MatchType.GreaterOrEqual, Format(StartKirim, "yyyy-MM-dd HH:mm:ss")))
            criterias = New CriteriaComposite(New Criteria(GetType(view_DealerWSCProccessed), "ReleaseDate", MatchType.GreaterOrEqual, Format(StartKirim, "yyyy-MM-dd HH:mm:ss")))
            'criterias.opAnd(New Criteria(GetType(view_DealerWSCProccessed), "CreatedDate", MatchType.LesserOrEqual, Format(EndKirim, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(view_DealerWSCProccessed), "ReleaseDate", MatchType.LesserOrEqual, Format(EndKirim, "yyyy-MM-dd HH:mm:ss")))

            If Not (txtKodeDealer.Text.Trim() = "") Then
                criterias.opAnd(New Criteria(GetType(view_DealerWSCProccessed), "DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
            End If

            If ddlType.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(view_DealerWSCProccessed), "ClaimType", MatchType.Exact, ddlType.SelectedItem.Text))
            End If
            _ListDealerWSC = New DealerFacade(System.Threading.Thread.CurrentPrincipal).RetrieveDealerWSCProccessed(criterias, sortColl)

            If _ListDealerWSC.Count > 0 And (Not _ListDealerWSC Is Nothing) Then
                _CompleteListDealerWSCForDTG = New ArrayList
                Dim sDealerCode As String = ""
                Dim objDealerWSC As view_DealerWSCProccessed

                For Each dealerWSC As view_DealerWSCProccessed In _ListDealerWSC
                    If sDealerCode <> dealerWSC.DealerCode Then
                        objDealerWSC = New view_DealerWSCProccessed

                        objDealerWSC = dealerWSC
                        _CompleteListDealerWSCForDTG.Add(objDealerWSC)
                        sDealerCode = dealerWSC.DealerCode
                    Else
                        CType(objDealerWSC, view_DealerWSCProccessed).WSCCount = CType(objDealerWSC, view_DealerWSCProccessed).WSCCount + dealerWSC.WSCCount
                    End If
                Next
                _sessHelper.SetSession("WSCINFOS", _CompleteListDealerWSCForDTG)
                _ListDealerWSCForDTG = ArrayListPager.DoPage(_CompleteListDealerWSCForDTG, 0, Me.dtgWSCInfo.PageSize)
                Me.dtgWSCInfo.DataSource = _ListDealerWSCForDTG
                Me.dtgWSCInfo.VirtualItemCount = _CompleteListDealerWSCForDTG.Count
                Me.dtgWSCInfo.CurrentPageIndex = 0
                Me.btnDownload.Enabled = True
            Else
                Me.dtgWSCInfo.DataSource = New ArrayList
                Me.dtgWSCInfo.VirtualItemCount = 0
                _sessHelper.SetSession("WSCINFOS", Me.dtgWSCInfo.DataSource)
                If IsPostBack And Not _bIsJustDownloadedToSAP Then
                    MessageBox.Show("Tidak ada Warranty Service Claim")
                End If
                Me.btnDownload.Enabled = False
            End If
            Me.dtgWSCInfo.DataBind()
        Else
            sortColl.Add(New Sort(GetType(view_DealerWSC), "DealerCode", Sort.SortDirection.ASC))
            'criterias = New CriteriaComposite(New Criteria(GetType(view_DealerWSC), "CreatedDate", MatchType.Greater, Format(Me.icCreatedDateStart.Value.AddDays(-1), "yyyy/MM/dd")))
            'criterias.opAnd(New Criteria(GetType(view_DealerWSC), "CreatedDate", MatchType.Lesser, Format(Me.icCreatedDateEnd.Value.AddDays(1), "yyyy/MM/dd")))
            'criterias = New CriteriaComposite(New Criteria(GetType(view_DealerWSC), "CreatedDate", MatchType.GreaterOrEqual, Format(StartKirim, "yyyy-MM-dd HH:mm:ss")))
            criterias = New CriteriaComposite(New Criteria(GetType(view_DealerWSC), "ReleaseDate", MatchType.GreaterOrEqual, Format(StartKirim, "yyyy-MM-dd HH:mm:ss")))
            'criterias.opAnd(New Criteria(GetType(view_DealerWSC), "CreatedDate", MatchType.LesserOrEqual, Format(EndKirim, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(view_DealerWSC), "ReleaseDate", MatchType.LesserOrEqual, Format(EndKirim, "yyyy-MM-dd HH:mm:ss")))

            If Not (txtKodeDealer.Text.Trim() = "") Then
                criterias.opAnd(New Criteria(GetType(view_DealerWSC), "DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
            End If

            If ddlType.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(view_DealerWSC), "ClaimType", MatchType.Exact, ddlType.SelectedItem.Text))
            End If


            _ListDealerWSC = New DealerFacade(System.Threading.Thread.CurrentPrincipal).RetrieveDealerWSC(criterias, sortColl)

            If _ListDealerWSC.Count > 0 And (Not _ListDealerWSC Is Nothing) Then
                _CompleteListDealerWSCForDTG = New ArrayList
                Dim sDealerCode As String = ""
                Dim objDealerWSC As view_DealerWSC

                For Each dealerWSC As view_DealerWSC In _ListDealerWSC
                    If sDealerCode <> dealerWSC.DealerCode Then
                        objDealerWSC = New view_DealerWSC

                        objDealerWSC = dealerWSC
                        _CompleteListDealerWSCForDTG.Add(objDealerWSC)
                        sDealerCode = dealerWSC.DealerCode
                    Else
                        CType(objDealerWSC, view_DealerWSC).WSCCount = CType(objDealerWSC, view_DealerWSC).WSCCount + dealerWSC.WSCCount
                    End If
                Next
                _sessHelper.SetSession("WSCINFOS", _CompleteListDealerWSCForDTG)
                _ListDealerWSCForDTG = ArrayListPager.DoPage(_CompleteListDealerWSCForDTG, 0, Me.dtgWSCInfo.PageSize)
                Me.dtgWSCInfo.DataSource = _ListDealerWSCForDTG
                Me.dtgWSCInfo.VirtualItemCount = _CompleteListDealerWSCForDTG.Count
                Me.dtgWSCInfo.CurrentPageIndex = 0
                Me.btnDownload.Enabled = True
            Else
                Me.dtgWSCInfo.DataSource = New ArrayList
                Me.dtgWSCInfo.VirtualItemCount = 0
                _sessHelper.SetSession("WSCINFOS", Me.dtgWSCInfo.DataSource)
                If IsPostBack And Not _bIsJustDownloadedToSAP Then
                    MessageBox.Show("Tidak ada Warranty Service Claim")
                End If
                Me.btnDownload.Enabled = False

                dtgWSCInfo.CurrentPageIndex = 0

            End If
            Me.dtgWSCInfo.DataBind()
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub dtgWSCInfo_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgWSCInfo.PageIndexChanged
        Dim CompletelistWSCForDTG As ArrayList = _sessHelper.GetSession("WSCINFOS")
        If Not CompletelistWSCForDTG Is Nothing Then
            _ListDealerWSCForDTG = ArrayListPager.DoPage(CompletelistWSCForDTG, e.NewPageIndex, Me.dtgWSCInfo.PageSize)
            Me.dtgWSCInfo.DataSource = _ListDealerWSCForDTG
            Me.dtgWSCInfo.VirtualItemCount = CompletelistWSCForDTG.Count
            Me.dtgWSCInfo.CurrentPageIndex = e.NewPageIndex
            Me.dtgWSCInfo.DataBind()
        End If
    End Sub

    Private Sub dtgWSCInfo_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgWSCInfo.SortCommand
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

        Dim CompletelistWSCForDTG As ArrayList = _sessHelper.GetSession("WSCINFOS")
        If Not CompletelistWSCForDTG Is Nothing Then
            SortListControl(CompletelistWSCForDTG, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Integer))
            _ListDealerWSCForDTG = ArrayListPager.DoPage(CompletelistWSCForDTG, 0, dtgWSCInfo.PageSize)
            Me.dtgWSCInfo.DataSource = _ListDealerWSCForDTG
            Me.dtgWSCInfo.VirtualItemCount = CompletelistWSCForDTG.Count
            Me.dtgWSCInfo.CurrentPageIndex = 0
            Me.dtgWSCInfo.DataBind()
        End If
    End Sub

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.WSCTransferView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=WSC - Transfer WSC to SAP")
        End If
        Me.btnDownload.Visible = SecurityProvider.Authorize(context.User, SR.WSCTransferDownload_Privilege)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        Dim lblPopUp As Label = CType(Page.FindControl("lblPopUpDealer"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection")
        Me.btnDownload.Attributes("onclick") = "return confirm('" & "Anda yakin akan melakukan download ?" & "');"
        If Not IsPostBack Then
            BindClaimType()
            'GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, False)
            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

            Dim aCs As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
            For Each oC As Category In aCs
                Me.ddlCategory.Items.Add(New ListItem(oC.CategoryCode, oC.ID))
            Next

            Dim crit As Hashtable = CType(_sessHelper.GetSession("CriteriaFormSubmitWSCToSAP"), Hashtable)
            ViewState("CurrentSortColumn") = ""
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            If Not crit Is Nothing Then
                Me.txtKodeDealer.Text = CStr(crit.Item("dealer"))
                Me.icCreatedDateStart.Value = CDate(crit.Item("tanggal"))
                Me.chkdownload.Checked = CBool(crit.Item("redownload"))
                BindDealerWSC()
            Else
                Me.dtgWSCInfo.DataSource = New ArrayList
                Me.dtgWSCInfo.VirtualItemCount = 0
                Me.dtgWSCInfo.DataBind()
                Me.btnDownload.Enabled = False
            End If
        End If
        chkdownload.Checked = True
        chkdownload.Enabled = False
    End Sub

    Private Sub BindClaimType()
        Dim _enumClaimType As New EnumClaimType
        Dim _arrTmp As New ArrayList
        _arrTmp = _enumClaimType.RetrieveClaimType

        ddlType.DataSource = _arrTmp
        ddlType.DataTextField = "NameClaimType"
        ddlType.DataValueField = "ValClaimType"
        ddlType.DataBind()
        ddlType.Items.Insert(0, "Pilih")
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim objWSCHeaderFacade As WSCHeaderFacade
        objWSCHeaderFacade = New WSCHeaderFacade(User)

        Dim StartKirim As New DateTime(CInt(icCreatedDateStart.Value.Year), CInt(icCreatedDateStart.Value.Month), CInt(icCreatedDateStart.Value.Day), 0, 0, 0)
        Dim EndKirim As New DateTime(CInt(icCreatedDateEnd.Value.Year), CInt(icCreatedDateEnd.Value.Month), CInt(icCreatedDateEnd.Value.Day), 23, 59, 59)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(WSCHeader), "CreatedTime", MatchType.Greater, Format(Me.icCreatedDateStart.Value.AddDays(-1), "yyyy/MM/dd")))
        'criterias.opAnd(New Criteria(GetType(WSCHeader), "CreatedTime", MatchType.Lesser, Format(Me.icCreatedDateEnd.Value.AddDays(1), "yyyy/MM/dd")))
        'criterias.opAnd(New Criteria(GetType(WSCHeader), "CreatedTime", MatchType.GreaterOrEqual, Format(StartKirim, "yyyy-MM-dd HH:mm:ss")))
        'criterias.opAnd(New Criteria(GetType(WSCHeader), "CreatedTime", MatchType.LesserOrEqual, Format(EndKirim, "yyyy-MM-dd HH:mm:ss")))
        criterias.opAnd(New Criteria(GetType(WSCHeader), "ReleaseDate", MatchType.GreaterOrEqual, Format(StartKirim, "yyyy-MM-dd HH:mm:ss")))
        criterias.opAnd(New Criteria(GetType(WSCHeader), "ReleaseDate", MatchType.LesserOrEqual, Format(EndKirim, "yyyy-MM-dd HH:mm:ss")))

        If chkdownload.Checked Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "Status", MatchType.Exact, CStr(enumStatusWSC.Status.Proses)))
        Else
            criterias.opAnd(New Criteria(GetType(WSCHeader), "Status", MatchType.Exact, CStr(enumStatusWSC.Status.Baru)))
        End If
        'Category
        criterias.opAnd(New Criteria(GetType(WSCHeader), "ChassisMaster.Category.ID", MatchType.Exact, Me.ddlCategory.SelectedValue))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.WSCHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "Dealer.DealerCode", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If

        If ddlType.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimType", MatchType.Exact, ddlType.SelectedItem.Text))
        End If

        Dim sorts As SortCollection = New SortCollection
        sorts.Add(New Sort(GetType(WSCHeader), "Dealer.DealerCode"))

        Dim ListOpenWSC As ArrayList
        ListOpenWSC = objWSCHeaderFacade.RetrieveByCriteria(criterias, sorts)

        'MessageBox.Show(ListOpenWSC.Count.ToString())

        If (Not ListOpenWSC Is Nothing) AndAlso ListOpenWSC.Count > 0 Then
            VIewState(V_Suffix) = DateTime.Now.ToString("yyyyMMddHHmmss")
            Dim wsc As WSCHeader
            For Each wsc In ListOpenWSC
                wsc.Status = CStr(enumStatusWSC.Status.Proses)
            Next

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            Dim bSuccessWriteToServer As Boolean = False

            Try
                If imp.Start() Then
                    checkFileExistenceToDownload()
                    bSuccessWriteToServer = (SaveWSCToServer(ListOpenWSC) = "")
                    imp.StopImpersonate()
                    imp = Nothing
                End If

            Catch ex As Exception
                MessageBox.Show("Impersonate failed")
            End Try

            If bSuccessWriteToServer Then
                objWSCHeaderFacade.UpdateWSCUploadedToSAP(ListOpenWSC)
                _bIsJustDownloadedToSAP = True
                SaveControlValueToSession()
                saveToFile()
            End If
        Else
            Dim nDisplayed As Integer = Me.dtgWSCInfo.Items.Count
            If nDisplayed = 0 Then
                MessageBox.Show("Tidak ada data yang didownload")
            Else
                MessageBox.Show("Tidak ada data yang didownload. Data yang didownload bukan kategori yang dipilih.")
            End If
        End If
    End Sub


    Private Sub SaveControlValueToSession()
        Dim crit As Hashtable = New Hashtable
        crit.Add("dealer", Me.txtKodeDealer.Text)
        crit.Add("tanggal", Me.icCreatedDateStart.Value)
        crit.Add("redownload", Me.chkdownload.Checked)
        _sessHelper.SetSession("CriteriaFormSubmitWSCToSAP", crit)
    End Sub

    Private Sub dtgWSCInfo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgWSCInfo.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(0).Text = (e.Item.ItemIndex + 1 + (dtgWSCInfo.PageSize * dtgWSCInfo.CurrentPageIndex)).ToString

            If Me.chkdownload.Checked Then
                Dim DealerWSC As view_DealerWSCProccessed
                DealerWSC = CType(_ListDealerWSCForDTG.Item(e.Item.ItemIndex), view_DealerWSCProccessed)
                _nWSCCount = _nWSCCount + DealerWSC.WSCCount
            Else
                Dim DealerWSC As view_DealerWSC
                DealerWSC = CType(_ListDealerWSCForDTG.Item(e.Item.ItemIndex), view_DealerWSC)
                _nWSCCount = _nWSCCount + DealerWSC.WSCCount
            End If

        End If

        If e.Item.ItemType = ListItemType.Footer Then
            CType(e.Item.Cells(4).FindControl("lblSumOfTotalWSC"), Label).Text = _nWSCCount.ToString & " dari " & getTotalWSCAmount.ToString
        End If

    End Sub

    Private Function getTotalWSCAmount() As Integer
        Dim iReturnValue As Integer = 0
        Dim CompletelistWSCForDTG As ArrayList = _sessHelper.GetSession("WSCINFOS")
        If (Not CompletelistWSCForDTG Is Nothing) Then
            If (CompletelistWSCForDTG.Count > 0) Then
                If Me.chkdownload.Checked Then
                    For Each DealerWSC As view_DealerWSCProccessed In CompletelistWSCForDTG
                        iReturnValue = iReturnValue + DealerWSC.WSCCount
                    Next
                Else
                    For Each DealerWSC As view_DealerWSC In CompletelistWSCForDTG
                        iReturnValue = iReturnValue + DealerWSC.WSCCount
                    Next
                End If

            End If
        End If
        Return iReturnValue
    End Function

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If ddlType.SelectedIndex > 0 Then
            _nWSCCount = 0
            BindDealerWSC()
        Else
            MessageBox.Show("Type WSC belum dipilih")
        End If

    End Sub

#End Region

End Class