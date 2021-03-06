#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.VehicleData
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.General
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
#End Region

Public Class FrmListVDH
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSelectionData As System.Web.UI.WebControls.Label
    Protected WithEvents lblSerialNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblNikNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblEngineNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtItemNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSerialNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNikNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEngineNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgData As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblItemNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblChassisNo As System.Web.UI.WebControls.Label
    Protected WithEvents txtChassisNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblItemSearch As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"

    Private ssHelper As SessionHelper = New SessionHelper
    Private arlToDisplay As ArrayList = New ArrayList

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.VHListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=DATA KENDARAAN LAMA - Daftar Kendaraan")
        End If
    End Sub

    Private Function CheckBtnDownloadPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.VHListDownload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Event Handler"

    Private Function BindDDLYear()
        Dim startYear As Integer = 1975
        Dim endYear As Integer = 2003

        Dim li As ListItem = New ListItem("Silahkan Pilih", -1)
        ddlYear.Items.Add(li)
        For i As Integer = startYear To endYear
            li = New ListItem(i, i)
            ddlYear.Items.Add(li)
        Next
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then

            Dim objDealer As Dealer = CType(ssHelper.GetSession("DEALER"), Dealer)
            If IsEnabledVDH(objDealer) Then
                ViewState("currSortColumn") = "ID"
                ViewState("currSortDirection") = Sort.SortDirection.ASC
                lblItemSearch.Attributes("onclick") = "ShowItemNoSelection();"
                If Not IsNothing(ssHelper.GetSession("ListVDH")) Then
                    Dim criteriaTemp As ICriteria
                    criteriaTemp = CType(ssHelper.GetSession("ListVDH"), ICriteria)
                    arlToDisplay = New VDHFacade(User).Retrieve(criteriaTemp)
                    dtgData.DataSource = arlToDisplay
                    dtgData.DataBind()
                End If
                BindDDLYear()
            Else
                Server.Transfer("../FrmAccessDenied.aspx?modulName=VDH")
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        arlToDisplay = New ArrayList
        btnDownload.Enabled = False
        txtChassisNo.Text = String.Empty
        txtSerialNo.Text = String.Empty
        txtItemNo.Text = String.Empty
        txtNikNo.Text = String.Empty
        txtEngineNo.Text = String.Empty
        dtgData.DataSource = arlToDisplay
        dtgData.DataBind()
        btnDownload.Enabled = False

    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        '---Modif by Ronny--> download to File TXT.
        '---download data to excel
        'Response.Redirect("frmDownloadListVDH.aspx")
        DownloadData()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        bindToGrid(0)
    End Sub

    Private Sub dtgData_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgData.PageIndexChanged
        dtgData.CurrentPageIndex = e.NewPageIndex
        bindToGrid(e.NewPageIndex)
    End Sub

    Private Sub dtgData_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgData.SortCommand
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
        bindToGrid(dtgData.CurrentPageIndex)
    End Sub

    Private Sub dtgData_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgData.ItemDataBound
        If e.Item.ItemIndex >= 0 Then
            Dim listVDH As VDH = CType(e.Item.DataItem, VDH)
            CType(e.Item.FindControl("btnVHF"), Button).Attributes.Add("onclick", "showPopUp('../PopUp/PopUpVHF.aspx?ID=" & listVDH.ID & "', '', 600, 800);return false;")
            CType(e.Item.FindControl("btnFSC"), Button).Attributes.Add("onclick", "showPopUp('../PopUp/PopUpFSC.aspx?ID=" & listVDH.ID & "', '', 600, 800);return false;")
            e.Item.Cells(1).Text = (dtgData.CurrentPageIndex * dtgData.PageSize + e.Item.ItemIndex + 1).ToString()

            Dim _lblGridserialNo As Label = CType(e.Item.FindControl("lblGridserialNo"), Label)

            Select Case listVDH.Serial.Trim.Length
                Case 0
                    _lblGridserialNo.Text = "000000"

                Case 1
                    _lblGridserialNo.Text = "00000" & listVDH.Serial.ToString

                Case 2
                    _lblGridserialNo.Text = "0000" & listVDH.Serial.ToString

                Case 3
                    _lblGridserialNo.Text = "000" & listVDH.Serial.ToString

                Case 4
                    _lblGridserialNo.Text = "00" & listVDH.Serial.ToString

                Case 5
                    _lblGridserialNo.Text = "0" & listVDH.Serial.ToString

                Case 6
                    _lblGridserialNo.Text = listVDH.Serial.ToString

            End Select

            Dim _lblGridNikNo As Label = CType(e.Item.FindControl("lblgridNikNo"), Label)
            If listVDH.NIKNo.Trim.ToUpper = "NULL" Then
                _lblGridNikNo.Text = ""
            Else
                _lblGridNikNo.Text = listVDH.NIKNo.Trim
            End If

        End If
    End Sub
#End Region

#Region "Custom Method"

    Public Function IsEnabledVDH(ByVal objDealer As Dealer) As Boolean
        'Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.).ToString)
        Dim objTransaction As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.DataKendaraanLama).ToString)
        If (objTransaction Is Nothing) Then
            Return True
        Else
            If objTransaction.Status = EnumDealerStatus.DealerStatus.Aktive Then
                Return True
            End If
        End If
        Return False
    End Function


    Private Sub bindToGrid(ByVal _currentPgIdx As Integer)

        Dim totalRow As Integer = 0
        Dim idx As Integer = _currentPgIdx

        Dim criteriasVDH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VDH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtItemNo.Text <> String.Empty Then
            criteriasVDH.opAnd(New Criteria(GetType(VDH), "ItemNo", MatchType.InSet, "('" & txtItemNo.Text.Replace(";", "','") & "')"))
        End If

        If txtChassisNo.Text <> String.Empty Then
            criteriasVDH.opAnd(New Criteria(GetType(VDH), "ChassisNo", MatchType.[Partial], txtChassisNo.Text))
        End If

        If txtSerialNo.Text <> String.Empty Then
            criteriasVDH.opAnd(New Criteria(GetType(VDH), "Serial", MatchType.[Partial], txtSerialNo.Text))
        End If

        If txtNikNo.Text <> String.Empty Then
            criteriasVDH.opAnd(New Criteria(GetType(VDH), "NIKNo", MatchType.[Partial], txtNikNo.Text))
        End If

        If txtEngineNo.Text <> String.Empty Then
            criteriasVDH.opAnd(New Criteria(GetType(VDH), "EngineNo", MatchType.[Partial], txtEngineNo.Text))
        End If

        If ddlYear.SelectedValue <> -1 Then
            criteriasVDH.opAnd(New Criteria(GetType(VDH), "ProductionYear", MatchType.Exact, ddlYear.SelectedValue))
        End If

        arlToDisplay = New VDHFacade(User).RetrieveActiveList(criteriasVDH, idx + 1, dtgData.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If arlToDisplay.Count > 0 Then
            dtgData.VirtualItemCount = totalRow
            dtgData.DataSource = arlToDisplay
            dtgData.DataBind()
            ssHelper.SetSession("ListVDH", criteriasVDH)
            ssHelper.SetSession("DownloadableVDH", arlToDisplay)
            If CheckBtnDownloadPriv() = False Then
                btnDownload.Enabled = False
            Else
                btnDownload.Enabled = True
            End If

            Label1.Text = "Total Records " & totalRow
        Else
            btnDownload.Enabled = False
            MessageBox.Show(SR.DataNotFound("Data"))
        End If


    End Sub

    Private Sub checkFileExistenceToDownload()
        Dim finfo As FileInfo = New FileInfo(Me.Server.MapPath("") & "\..\DataTemp\Histori Kendaraan-DaftarHistoriKendaraan.txt")
        If finfo.Exists Then
            finfo.Delete()
        End If
    End Sub

    Private Sub DownloadData()
        Dim strText As StringBuilder
        Dim arrToDownload As New ArrayList
        Dim delimiter As String = Chr(9)
        Dim _strData As String = String.Empty

        Dim _criterias As CriteriaComposite = CType(ssHelper.GetSession("ListVDH"), CriteriaComposite)

        '  arrToDownload = New VDHFacade(User).Retrieve(_criterias)
        Dim cuPage As Integer = dtgData.CurrentPageIndex
        Dim totalrow As Integer = 0
        arrToDownload = New VDHFacade(User).RetrieveActiveList(_criterias, cuPage + 1, dtgData.PageSize, totalrow, _
                CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        strText = New StringBuilder

        '---Header

        strText.Append("No.".PadRight(arrToDownload.Count.ToString.Length, " "))
        strText.Append(delimiter)
        strText.Append("Item No.".PadRight(15, " "))
        strText.Append(delimiter)
        strText.Append("Chassis No.".PadRight(11, " "))
        strText.Append(delimiter)
        strText.Append("Serial No.".PadRight(11, " "))
        strText.Append(delimiter)
        strText.Append("Engine No.".PadRight(11, " "))
        strText.Append(delimiter)
        strText.Append("NIK No.".PadRight(17, " "))
        strText.Append(delimiter)
        strText.Append("Production Year".PadRight(15, " "))
        strText.Append(delimiter)
        strText.Append("MMC Lot No.".PadRight(11, " "))
        strText.Append(delimiter)
        strText.Append("Invoice Buy".PadRight(15, " "))
        strText.Append(delimiter)
        strText.Append("Receipt CBU Date".PadRight(16, " "))
        strText.Append(delimiter)
        strText.Append("Carrossery Transfer Date".PadRight(24, " "))
        strText.Append(delimiter)
        strText.Append("Receipt Carrossery Date".PadRight(23, " "))
        strText.Append(delimiter)
        strText.Append("Customer".PadRight(8, " "))
        strText.Append(delimiter)
        strText.Append("End Customer Name".PadRight(40, " "))
        strText.Append(delimiter)
        strText.Append("End Customer Address".PadRight(100, " "))
        strText.Append(delimiter)
        strText.Append("Kelurahan".PadRight(25, " "))
        strText.Append(delimiter)
        strText.Append("Kecamatan".PadRight(25, " "))
        strText.Append(delimiter)
        strText.Append("Kabupaten".PadRight(25, " "))
        strText.Append(delimiter)
        strText.Append("Propinsi".PadRight(25, " "))
        strText.Append(delimiter)
        strText.Append("R".PadRight(5, " "))
        strText.Append(delimiter)
        strText.Append("Type".PadRight(4, " "))
        strText.Append(delimiter)
        strText.Append("Request Date".PadRight(12, " "))
        strText.Append(delimiter)

        strText.Append("DOPrint Date".PadRight(12, " "))
        strText.Append(delimiter)

        strText.Append("ScheduleShip Date".PadRight(17, " "))
        strText.Append(delimiter)

        strText.Append("SCV Date 1".PadRight(10, " "))
        strText.Append(delimiter)

        strText.Append("SVC Date 2".PadRight(10, " "))
        strText.Append(delimiter)

        strText.Append("SVC Cust 1".PadRight(10, " "))
        strText.Append(delimiter)

        strText.Append("SVC Cust 2".PadRight(10, " "))
        strText.Append(delimiter)

        strText.Append("Facture Open Date".PadRight(17, " "))
        strText.Append(delimiter)

        strText.Append("Facture Date".PadRight(12, " "))
        strText.Append(delimiter)

        strText.Append("Facture No".PadRight(10, " "))
        strText.Append(delimiter)

        strText.Append("Facture Comment".PadRight(30, " "))
        strText.Append(delimiter)

        strText.Append("VAT No.".PadRight(25, " "))
        strText.Append(delimiter)

        strText.Append("VAT Date".PadRight(10, " "))
        strText.Append(delimiter)

        strText.Append("StockOut Date".PadRight(13, " "))
        strText.Append(delimiter)

        strText.Append("Orders".PadRight(6, " "))
        strText.Append(delimiter)

        strText.Append("Invoice Sell".PadRight(12, " "))
        strText.Append(delimiter)

        strText.Append("PIUD No.".PadRight(15, " "))
        strText.Append(delimiter)

        strText.Append("PIUD Date".PadRight(10, " "))
        strText.Append(delimiter)


        strText.Append(vbNewLine)

        '--Detail
        For i As Integer = 0 To arrToDownload.Count - 1


            Dim obj As VDH = arrToDownload(i)

            '--No Urut 
            ' dtgData.CurrentPageIndex * dtgData.PageSize + e.Item.ItemIndex + 1
            'cuPage *  dtgdata.pagesize + i+1
            strText.Append((CStr(cuPage * dtgData.PageSize + i + 1) & ".").PadRight(arrToDownload.Count.ToString.Length, ""))
            strText.Append(delimiter)

            strText.Append(obj.ItemNo.Trim.PadRight(15, ""))
            strText.Append(delimiter)

            If obj.ChassisNo.Trim.Length <> 6 Then
                Dim txt As String = obj.ChassisNo.Trim
                setDisplay(txt)
                strText.Append(txt.PadRight(11, " "))
                strText.Append(delimiter)
            Else
                strText.Append(obj.ChassisNo.Trim.PadRight(11, " "))
                strText.Append(delimiter)
            End If

            If obj.Serial.Trim.Length <> 6 Then
                Dim txt As String = obj.Serial.Trim
                setDisplay(txt)
                strText.Append(txt.PadRight(11, " "))
                strText.Append(delimiter)
            Else
                strText.Append(obj.Serial.Trim.PadRight(11, " "))
                strText.Append(delimiter)
            End If

            strText.Append(obj.EngineNo.Trim.PadRight(11, " "))
            strText.Append(delimiter)

            _strData = obj.NIKNo.Trim.ToUpper
            convertData(_strData)
            strText.Append(_strData.PadRight(17, " "))
            strText.Append(delimiter)

            strText.Append(obj.ProductionYear.Trim.PadRight(15, " "))
            strText.Append(delimiter)

            If obj.MMCLotNo.Trim.Length <> 7 Then
                Dim txt As String = obj.MMCLotNo.Trim
                setDisplay(txt)
                strText.Append(("0" & txt).PadRight(11, " "))
                strText.Append(delimiter)
            Else
                strText.Append(obj.MMCLotNo.Trim.PadRight(11, " "))
                strText.Append(delimiter)
            End If

            _strData = obj.InvoiceBuy.Trim.ToUpper
            convertData(_strData)
            strText.Append(_strData.Trim.PadRight(15, " "))
            strText.Append(delimiter)

            strText.Append(IIf(Year(obj.ReceiptCBUDate) > 1970, obj.ReceiptCBUDate.ToString("dd/MM/yyyy").PadRight(16, " "), "".PadRight(16, " ")))
            strText.Append(delimiter)

            strText.Append(IIf(Year(obj.CarrosseryTransferDate) > 1970, obj.CarrosseryTransferDate.ToString("dd/MM/yyyy").PadRight(24, " "), "".PadRight(24, " ")))
            strText.Append(delimiter)

            strText.Append(IIf(Year(obj.ReceiptCarrosseryDate) > 1970, obj.ReceiptCarrosseryDate.ToString("dd/MM/yyyy").PadRight(23, " "), "".PadRight(23, " ")))
            strText.Append(delimiter)

            strText.Append(obj.Customer.Trim.PadRight(8, " "))
            strText.Append(delimiter)

            strText.Append(obj.EndCustomerName.Trim.PadRight(40, " "))
            strText.Append(delimiter)

            strText.Append(obj.EndCustomerAddress.Trim.PadRight(100, " "))
            strText.Append(delimiter)

            _strData = obj.Kelurahan.Trim.ToUpper
            convertData(_strData)
            strText.Append(_strData.Trim.PadRight(25, " "))
            strText.Append(delimiter)

            _strData = obj.Kecamatan.Trim.ToUpper
            convertData(_strData)
            strText.Append(_strData.Trim.PadRight(25, " "))
            strText.Append(delimiter)

            _strData = obj.Kabupaten.Trim.ToUpper
            convertData(_strData)
            strText.Append(_strData.Trim.PadRight(25, " "))
            strText.Append(delimiter)

            _strData = obj.Propinsi.Trim.ToUpper
            convertData(_strData)
            strText.Append(_strData.Trim.PadRight(25, " "))
            strText.Append(delimiter)

            _strData = obj.R.Trim.ToUpper
            convertData(_strData)
            strText.Append(_strData.PadRight(5, " "))
            strText.Append(delimiter)

            If obj.Type.Trim.ToUpper = "NU" Then
                _strData = String.Empty
            End If
            strText.Append(_strData.PadRight(4, " "))
            strText.Append(delimiter)

            strText.Append(obj.RequestDate.ToString("dd/MM/yyyy").PadRight(12, " "))
            strText.Append(delimiter)

            strText.Append(obj.DOPrintDate.ToString("dd/MM/yyyy").PadRight(12, " "))
            strText.Append(delimiter)

            strText.Append(IIf(Year(obj.ScheduleShipDate) > 1970, obj.ScheduleShipDate.ToString("dd/MM/yyyy").PadRight(17, " "), "".PadRight(17, " ")))
            strText.Append(delimiter)

            strText.Append(IIf(Year(obj.SCVDate1) > 1970, obj.SCVDate1.ToString("dd/MM/yyyy").PadRight(10, " "), "".PadRight(10, " ")))
            strText.Append(delimiter)

            strText.Append(IIf(Year(obj.SVCDate2) > 1970, obj.SVCDate2.ToString("dd/MM/yyyy").PadRight(10, " "), "".PadRight(10, " ")))
            strText.Append(delimiter)

            strText.Append(obj.SVCCust1.Trim.PadRight(10, " "))
            strText.Append(delimiter)

            strText.Append(obj.SVCCust2.Trim.PadRight(10, " "))
            strText.Append(delimiter)

            strText.Append(IIf(Year(obj.FactureOpenDate) > 1970, obj.FactureOpenDate.ToString("dd/MM/yyyy").PadRight(17, " "), "".PadRight(17, " ")))
            strText.Append(delimiter)

            strText.Append(IIf(Year(obj.FactureDate) > 1970, obj.FactureDate.ToString("dd/MM/yyyy").PadRight(12, " "), "".PadRight(12, " ")))
            strText.Append(delimiter)

            strText.Append(obj.FactureNo.Trim.PadRight(10, " "))
            strText.Append(delimiter)

            _strData = obj.FactureComment.Trim.ToUpper
            convertData(_strData)
            strText.Append(_strData.Trim.PadRight(30, " "))
            strText.Append(delimiter)

            _strData = obj.VATNo.Trim.ToUpper
            convertData(_strData)
            strText.Append(_strData.Trim.PadRight(25, " "))
            strText.Append(delimiter)

            strText.Append(obj.VATDate.ToString("dd/MM/yyyy").PadRight(10, " "))
            strText.Append(delimiter)

            strText.Append(obj.StockOutDate.ToString("dd/MM/yyyy").PadRight(13, " "))
            strText.Append(delimiter)

            strText.Append(obj.Orders.Trim.PadRight(6, " "))
            strText.Append(delimiter)

            strText.Append(obj.IncoiveSell.Trim.PadRight(12, " "))
            strText.Append(delimiter)

            strText.Append(obj.PIUDNo.Trim.PadRight(15, " "))
            strText.Append(delimiter)

            strText.Append(obj.PIUDDate.PadRight(10, " "))
            strText.Append(delimiter)


            strText.Append(vbNewLine)



        Next

        Try
            saveToTextFile(strText.ToString())
        Catch
            MessageBox.Show("Proses Download gagal")
            Return
        End Try

        Response.Redirect("../downloadlocal.aspx?file=DataTemp\Histori Kendaraan-DaftarHistoriKendaraan.txt")



    End Sub

    Private Sub saveToTextFile(ByVal str As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Try
            If imp.Start() Then

                Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\Histori Kendaraan-DaftarHistoriKendaraan.txt", FileMode.Create, FileAccess.Write)
                Dim objStreamWriter As New StreamWriter(objFileStream)

                objStreamWriter.WriteLine(str)
                objStreamWriter.Close()

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub setDisplay(ByRef txt As String)
        Select Case txt.Length
            Case 0
                txt = "000000"

            Case 1
                txt = "00000" & txt

            Case 2
                txt = "0000" & txt

            Case 3
                txt = "000" & txt
            Case 4
                txt = "00" & txt
            Case 5
                txt = "0" & txt

        End Select
    End Sub

    Private Sub convertData(ByRef _data As String)

        If _data = "NULL" Then
            _data = String.Empty
        End If

    End Sub

#End Region





End Class
