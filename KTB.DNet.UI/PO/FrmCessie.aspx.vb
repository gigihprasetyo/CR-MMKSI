#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Parser
Imports KTB.DNet.UI.Helper
Imports System.IO
Imports System.Text
#End Region

Public Class FrmCessie
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents dtgMain As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCreditAccount As System.Web.UI.WebControls.TextBox
    Protected WithEvents calStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents calEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblTotalPayment As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPurchase As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPiutang As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
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

    Private _sessHelper As New SessionHelper

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        If (calEnd.Value < calStart.Value) Then
            MessageBox.Show("Tanggal Mulai Harus Kurang Dari Sama Dengan Tanggal Akhir")
        Else
            If DateDiff(DateInterval.Day, calStart.Value, calEnd.Value) > 60 Then
                MessageBox.Show("Periode tanggal maksimal 60 hari")
                Exit Sub
            End If
            BindToDataGrid()
        End If
    End Sub

    Sub BindToDataGrid()
        Dim _lstObjCessie As New ArrayList
        Dim _CessieFacade As New KTB.DNet.BusinessFacade.PO.CessieFacade(User)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Cessie), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtCreditAccount.Text <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Cessie), "CessieNumber", MatchType.Exact, txtCreditAccount.Text))

        End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Cessie), "CessieDate", MatchType.LesserOrEqual, calEnd.Value))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Cessie), "CessieDate", MatchType.GreaterOrEqual, calStart.Value))

        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID > 0 Then
            Dim oC As Cessie
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Cessie), "ProductCategory.ID", MatchType.Exact, PCID))
        End If


        _lstObjCessie = _CessieFacade.Retrieve(criterias)

        Dim TotalPiutang As Decimal = 0
        Dim TotalPembelian As Decimal = 0
        Dim TotalPembayaran As Decimal = 0
        CountTotal(_lstObjCessie, TotalPiutang, TotalPembelian, TotalPembayaran)

        info.AddMergedColumns(New Integer() {7, 8, 9}, "Pembayaran Oleh DSF")

        Me.dtgMain.DataSource = _lstObjCessie
        Me.dtgMain.DataBind()
        Me._sessHelper.SetSession("FrmCessie.Data", _lstObjCessie)

        Me.lblTotalPayment.Text = FormatNumber(TotalPembayaran, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) ' String.Format("{0:C}", TotalPembayaran) 'TotalPembayaran.ToString()
        Me.lblTotalPiutang.Text = FormatNumber(TotalPiutang, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) ' String.Format("{0:C}", TotalPiutang) 'TotalPiutang.ToString()
        Me.lblTotalPurchase.Text = FormatNumber(TotalPembelian, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) 'String.Format("{0:C}", TotalPembelian) 'TotalPembelian.ToString()
        Me.btnDownload.Enabled = (_lstObjCessie.Count > 0)
    End Sub

    Private Sub dtgMain_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.EditCommand
        dtgMain.EditItemIndex = CInt(e.Item.ItemIndex)
        'Dim KeyId As Integer
        'KeyId = Integer.Parse(dtgMain.DataKeys(e.Item.ItemIndex))
        ''If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then

        'Dim objCessieDet As CessieDetail = GetCessieDetailByCessie(KeyId)
        'Dim lblBankNameEdit As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("LblBankNameEdit"), System.Web.UI.WebControls.Label)
        'Dim lblJumlahPembayaranEdit As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("lblJumlahPembayaranEdit"), System.Web.UI.WebControls.Label)
        'Dim lblTanggalTransferEdit As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("lblTanggalTransferEdit"), System.Web.UI.WebControls.Label)

        'If Not (lblBankNameEdit Is Nothing) Then
        '    lblBankNameEdit.Text = objCessieDet.BankName
        '    lblJumlahPembayaranEdit.Text = String.Format("{0:C}", objCessieDet.Amount)  'objCessieDet.Amount.ToString()
        '    lblTanggalTransferEdit.Text = objCessieDet.TransferDate
        'End If

        'e.Item.Cells(7).Text = objCessieDet.BankName
        'e.Item.Cells(8).Text = String.Format("{0:C}", objCessieDet.Amount) 'objCessieDet.Amount.ToString()
        'e.Item.Cells(9).Text = objCessieDet.TransferDate
        'End If

        BindToDataGrid()
    End Sub

    Private Sub dtgMain_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.CancelCommand
        dtgMain.EditItemIndex = -1
        BindToDataGrid()
    End Sub

    Private Sub dtgMain_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim img As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgIsExistFileIndicator"), System.Web.UI.WebControls.Image)
            Dim lblFile As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("LblFileText"), System.Web.UI.WebControls.Label)

            Dim objCessie As Cessie = GetCessie(dtgMain.DataKeys(e.Item.ItemIndex))
            If Not (img Is Nothing) Then
                If Not (lblFile Is Nothing) Then
                    If Not (objCessie Is Nothing) Then
                        If (objCessie.DownloadedBy.Length = 0) Then
                            img.ImageUrl = "../images/red.gif"
                        Else
                            img.ImageUrl = "../images/green.gif"
                        End If
                    End If

                End If

            End If
            Dim LblBankName As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("LblBankName"), System.Web.UI.WebControls.Label)
            Dim lblRefNo As Label = e.Item.FindControl("lblRefNo")
            Dim lblTanggalTransfer As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("lblTanggalTransfer"), System.Web.UI.WebControls.Label)
            Dim lblJumlahPembayaran As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("lblJumlahPembayaran"), System.Web.UI.WebControls.Label)
            Dim lblBankNameEdit As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("LblBankNameEdit"), System.Web.UI.WebControls.Label)
            Dim lblJumlahPembayaranEdit As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("lblJumlahPembayaranEdit"), System.Web.UI.WebControls.Label)
            Dim lblTanggalTransferEdit As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("lblTanggalTransferEdit"), System.Web.UI.WebControls.Label)
            Dim lblProductCategory As Label = e.Item.FindControl("lblProductCategory")
            Dim lblRegNumber As Label = e.Item.FindControl("lblRegNumber")

            lblProductCategory.Text = objcessie.ProductCategory.Code
            Dim objCessieDet As CessieDetail = GetCessieDetailByCessie(Integer.Parse(DataBinder.Eval(e.Item.DataItem, "ID")))
            If Not (LblBankName Is Nothing) Then
                If Not (objCessieDet Is Nothing) Then
                    LblBankName.Text = objCessieDet.BankName
                    lblRefNo.Text = objcessiedet.RefNumber
                    lblTanggalTransfer.Text = objCessie.PaymentDate.ToString("dd/MM/yyyy") ' objCessieDet.TransferDate.ToString("dd/MM/yyyy")
                    lblJumlahPembayaran.Text = FormatNumber(objCessie.PurchaseAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)  'String.Format("{0:#,###}", objCessieDet.Amount) 'objCessieDet.Amount.ToString()
                    lblRegNumber.Text = objcessiedet.RegNumber
                    If Not (lblBankNameEdit Is Nothing) Then
                        lblBankNameEdit.Text = objCessieDet.BankName
                        lblJumlahPembayaranEdit.Text = FormatNumber(objCessie.PurchaseAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) ' String.Format("{0:#,###}", objCessieDet.Amount)   'objCessieDet.Amount.ToString()
                        lblTanggalTransferEdit.Text = objCessie.PaymentDate.ToString("dd/MM/yyyy")  'objCessieDet.TransferDate.ToString("dd/MM/yyyy")

                    End If
                End If
            End If
            Dim oD As Dealer = CType(Session.Item("DEALER"), Dealer)
            If oD.Title = EnumDealerTittle.DealerTittle.KTB OrElse objCessie.NumOfTransfered > 0 OrElse IsConfirmed(objCessie) Then
                e.Item.Cells(14).Controls.Clear()  'Editing Column
            End If
        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            Dim objCessie As Cessie = GetCessie(dtgMain.DataKeys(e.Item.ItemIndex))
            Dim objCessieDet As CessieDetail = GetCessieDetailByCessie(Integer.Parse(DataBinder.Eval(e.Item.DataItem, "ID")))
            Dim txtBankName As TextBox = e.Item.FindControl("txtBankName")
            Dim txtRefNo As TextBox = e.Item.FindControl("txtRefNo")
            Dim txtJumlahPembayaran As TextBox = e.Item.FindControl("txtJumlahPembayaran")
            Dim calDatePayment As KTB.DNet.WebCC.IntiCalendar = e.Item.FindControl("calDatePayment")
            Dim lblProductCategoryEdit As Label = e.Item.FindControl("lblProductCategoryEdit")
            Dim lblRegNumberEdit As Label = e.Item.FindControl("lblRegNumberEdit")

            lblProductCategoryEdit.Text = objcessie.ProductCategory.Code
            txtBankName.Text = objCessieDet.BankName
            txtRefNo.Text = objCessieDet.RefNumber
            txtJumlahPembayaran.Text = FormatNumber(objCessie.PurchaseAmount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)  ' FormatNumber(objCessieDet.Amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            calDatePayment.Value = objcessie.PaymentDate.ToString("dd/MM/yyyy") ' objcessiedet.TransferDate
        End If

    End Sub

    

    Function GetCessieDetail(ByVal IdCessie As Integer) As ArrayList
        Dim _Facade As New KTB.DNet.BusinessFacade.PO.CessieDetailFacade(User)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CessieDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CessieDetail), "Cessie.ID", MatchType.Exact, IdCessie))

        Return _Facade.Retrieve(criterias)
    End Function

    Sub CountTotal(ByVal ListCessie As ArrayList, ByRef TotalPiutang As Double, ByRef TotalPembelian As Decimal, ByRef TotalPembayaran As Decimal)
        TotalPiutang = 0
        TotalPembelian = 0
        TotalPembayaran = 0
        For Each objCessie As Cessie In ListCessie
            TotalPiutang = TotalPiutang + objCessie.Amount
            TotalPembelian = TotalPembelian + objCessie.PurchaseAmount
            'Dim TotalTemp As Decimal = 0
            'For Each objCessieDetail As CessieDetail In GetCessieDetail(objCessie.ID)
            '    TotalTemp = TotalTemp + objCessieDetail.Amount
            'Next
            TotalPembayaran = TotalPembayaran + objCessie.DifferenceAmount  'objCessie.PurchaseAmount ' TotalPembayaran + TotalTemp
        Next
    End Sub

    Private Sub SetControls()
        Dim oD As Dealer = CType(Session.Item("DEALER"), Dealer)
        Me.btnUpload.Visible = False
        If oD.Title <> EnumDealerTittle.DealerTittle.LEASING Then
            Me.dtgMain.Columns(14).Visible = False 'Editing Column
            If oD.Title = EnumDealerTittle.DealerTittle.KTB Then
                Me.btnUpload.Visible = True
            End If
        Else
            Me.dtgMain.Columns(14).Visible = True 'Editing Column
        End If
    End Sub

    Private Function IsConfirmed(ByVal oC As Cessie) As Boolean
        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim cDP As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aDP As New ArrayList

        cDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, 0))
        cDP.opAnd(New Criteria(GetType(DailyPayment), "Reason", MatchType.Exact, oC.CessieNumber))
        aDP = oDPFac.Retrieve(cDP)
        Return IIf(aDP.Count > 0, True, False)
    End Function

#Region "HeaderGridhandler"

    Private Sub dtgMain_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgMain.ItemCreated

        Dim gvr As DataGridItem = e.Item
        If (gvr.ItemType = ListItemType.Header) Then
            e.Item.SetRenderMethodDelegate(AddressOf RenderHeader)
        End If
    End Sub


    Public ReadOnly Property info() As MergedColumnsInfo
        Get
            If (ViewState("info") Is Nothing) Then

                ViewState("info") = New MergedColumnsInfo
            End If
            Return CType(ViewState("info"), MergedColumnsInfo)
        End Get
    End Property


    <Serializable()> _
    Public Class MergedColumnsInfo
        Public MergedColumns As New ArrayList
        Public StartColumns As New Hashtable
        '            // key-value pairs: key = first column index, value = common title of merged columns 
        Public Titles As New Hashtable
        '            //parameters: merged columns's indexes, common title of merged columns 

        Public Sub AddMergedColumns(ByVal columnsIndexes As Integer(), ByVal title As String)

            If Not StartColumns.ContainsKey(columnsIndexes(0)) Then
                MergedColumns.AddRange(columnsIndexes)
                StartColumns.Add(columnsIndexes(0), columnsIndexes.Length)
                Titles.Add(columnsIndexes(0), title)
            End If


        End Sub
    End Class

    Public Sub RenderHeader(ByVal output As HtmlTextWriter, ByVal container As Control)
        For i As Integer = 0 To container.Controls.Count - 1
            Dim cell As TableCell = CType(container.Controls(i), TableCell)
            If (Not info.MergedColumns.Contains(i)) Then
                cell.Attributes("rowspan") = "2"
                cell.RenderControl(output)
            Else
                If (info.StartColumns.Contains(i)) Then
                    output.Write(String.Format("<th align='center' Class='titleTableSales' colspan='{0}'>{1}</th>", info.StartColumns(i), info.Titles(i)))
                End If
            End If
        Next
        output.RenderEndTag()

        dtgMain.HeaderStyle.AddAttributesToRender(output)
        output.RenderBeginTag("tr")

        For i As Integer = 0 To info.MergedColumns.Count - 1
            Dim cell As TableCell = CType(container.Controls(info.MergedColumns(i)), TableCell)
            cell.RenderControl(output)

        Next

    End Sub
#End Region

#Region "Cessie Detail"
    Sub CreateCessiedetail(ByVal objDetail As CessieDetail)
        Dim _facade As New CessieDetailFacade(User)
        _facade.Insert(objDetail)
        MessageBox.Show(SR.UpdateSucces)
    End Sub

    Sub UpdateCessieDetail(ByVal objDetail As CessieDetail)
        Dim _facade As New CessieDetailFacade(User)
        Dim i As Integer = _facade.Update(objDetail)

        MessageBox.Show(SR.UpdateSucces)
    End Sub

    Function GetCessie(ByVal _idCessie As Integer) As Cessie
        Dim _retVal As New Cessie
        Dim _facade As New CessieFacade(User)
        _retVal = _facade.Retrieve(_idCessie)
        Return _retVal
    End Function

    Function GetCessieDetailByCessie(ByVal _idCessie As Integer) As CessieDetail
        Dim _retVal As New CessieDetail
        Dim _facade As New CessieDetailFacade(User)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CessieDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CessieDetail), "Cessie.ID", MatchType.Exact, _idCessie))
        If (_facade.Retrieve(criterias).Count > 0) Then
            _retVal = _facade.Retrieve(criterias)(0)

        End If
        Return _retVal
    End Function

    Private Sub dtgMain_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.UpdateCommand
        If (e.CommandName = "Update") Then
            'If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim txtBankName As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtBankName"), System.Web.UI.WebControls.TextBox)
            Dim txtRefNo As TextBox = e.Item.FindControl("txtRefNo")
            Dim txtJumlahPembayaran As System.Web.UI.WebControls.TextBox = CType(e.Item.FindControl("txtJumlahPembayaran"), System.Web.UI.WebControls.TextBox)
            Dim calDatePayment As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("calDatePayment"), KTB.DNet.WebCC.IntiCalendar)

            Dim LblBankName As System.Web.UI.WebControls.Label = CType(dtgMain.Items(e.Item.ItemIndex).FindControl("LblBankName"), System.Web.UI.WebControls.Label)
            'Dim LblBankName As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("LblBankName"), System.Web.UI.WebControls.Label)
            Dim lblTanggalTransfer As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("lblTanggalTransfer"), System.Web.UI.WebControls.Label)
            Dim lblJumlahPembayaran As System.Web.UI.WebControls.Label = CType(e.Item.FindControl("lblJumlahPembayaran"), System.Web.UI.WebControls.Label)

            If txtBankName.Text.Trim = "" Then
                MessageBox.Show("Nama bank tidak boleh kosong")
                Exit Sub
            End If

            Dim _obj As New CessieDetail
            'get Cessie of Index
            Dim _errorMessage As String = String.Empty
            Dim _objCessie As New Cessie
            Dim KeyId As Integer
            KeyId = Integer.Parse(dtgMain.DataKeys(e.Item.ItemIndex))
            _objCessie = GetCessie(KeyId)
            _obj.Cessie = _objCessie
            _obj.BankName = txtBankName.Text
            _obj.RefNumber = txtRefNo.Text
            Try
                _obj.Amount = Decimal.Parse(txtJumlahPembayaran.Text)
            Catch ex As Exception
                _errorMessage = "Error value Amount"
                MessageBox.Show("TextAmount Should be decimal value")
            End Try

            _obj.TransferDate = calDatePayment.Value

            If (_errorMessage.Length = 0) Then
                'Check if exist
                Dim _CessieDetailFacade As New KTB.DNet.BusinessFacade.PO.CessieDetailFacade(User)
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CessieDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CessieDetail), "Cessie.ID", MatchType.Exact, KeyId))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CessieDetail), "BankName", MatchType.Exact, txtBankName.Text))

                If (_CessieDetailFacade.Retrieve(criterias).Count > 0) Then
                    'Do Update
                    Dim _objToUpdate As CessieDetail = _CessieDetailFacade.Retrieve(criterias)(0)
                    _objToUpdate.BankName = _obj.BankName
                    _objToUpdate.RefNumber = _obj.RefNumber
                    _objToUpdate.Amount = _obj.Amount
                    _objToUpdate.TransferDate = _obj.TransferDate
                    UpdateCessieDetail(_objToUpdate)
                Else
                    'Do Insert
                    CreateCessiedetail(_obj)
                End If
            End If
            'End If
        End If

        dtgMain.EditItemIndex = -1
        BindToDataGrid()
    End Sub
#End Region

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.daftar_cessie_lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Cessie")
        End If
        If CType(Session.Item("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.KTB Then
            Me.btnUpload.Enabled = SecurityProvider.Authorize(Context.User, SR.daftar_cessie_transfer_privilege)
        ElseIf CType(Session.Item("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.LEASING Then
            Me.dtgMain.Columns(14).Visible = SecurityProvider.Authorize(Context.User, SR.daftar_cessie_input_privilege)  'Edit Pembayaran
            Me.dtgMain.Columns(15).Visible = SecurityProvider.Authorize(Context.User, SR.daftar_cessie_download_privilege) 'Download PDF
            Me.dtgMain.Columns(16).Visible = Me.dtgMain.Columns(15).Visible 'Download Txt
        End If

    End Sub


    Private Sub DoDownload(ByRef arlData As ArrayList)
        Dim sFileName As String
        Dim fullFileName As String
        sFileName = "DaftarCessie" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

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
        Dim oC As Cessie
        Dim chkExport As CheckBox

        If Not IsNothing(arlData) Then
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No Cessie" & tab)
            itemLine.Append("Cessie Date" & tab)
            itemLine.Append("Amount Of AR (Rp)" & tab)
            itemLine.Append("VH (Rp)" & tab)
            itemLine.Append("PP (Rp)" & tab)
            itemLine.Append("IT (Rp)" & tab)
            itemLine.Append("Purchase Value by DSF (Rp)" & tab)
            itemLine.Append("Discount Amount" & tab)
            itemLine.Append("Admin Cost" & tab)
            itemLine.Append("Total Cost" & tab)
            itemLine.Append("Amount (Rp)" & tab)
            itemLine.Append("Scheduled Payment Date" & tab)

            sw.WriteLine(itemLine.ToString())

            For Each di As DataGridItem In Me.dtgMain.Items
                oc = CType(arlData(di.ItemIndex), Cessie)
                chkExport = di.FindControl("chkExport")
                If chkExport.Checked Then
                    DiscountAmount = oC.Amount - oC.PurchaseAmount
                    TotalCost = DiscountAmount + oC.AdminFee
                    Amount = oC.Amount - TotalCost

                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(oC.CessieNumber & tab)  'No Cessie
                    itemLine.Append(oC.CessieDate.ToString("dd/MMM/yyyy") & tab) 'Cessie Date
                    itemLine.Append(GetFormatNumber(oC.Amount) & tab) 'Amount Of AR
                    itemLine.Append(GetFormatNumber(oC.V_CessieDetail.VH) & tab)  'VH
                    itemLine.Append(GetFormatNumber(oC.V_CessieDetail.PP) & tab)   'PP
                    itemLine.Append(GetFormatNumber(oC.V_CessieDetail.IT) & tab)   'IT
                    itemLine.Append(GetFormatNumber(oC.PurchaseAmount) & tab)  'Purchase Value
                    itemLine.Append(GetFormatNumber(DiscountAmount) & tab)  'Discount Amount
                    itemLine.Append(GetFormatNumber(oC.AdminFee) & tab)  'Admin Cost
                    itemLine.Append(GetFormatNumber(TotalCost) & tab) 'Total Cost
                    itemLine.Append(GetFormatNumber(Amount) & tab)  'Amount (Rp)
                    itemLine.Append(oC.PaymentDate.ToString("dd/MMM/yyyy") & tab) 'Scheduled Payment Date

                    sw.WriteLine(itemLine.ToString())
                End If
            Next
        End If
    End Sub

    Private Function GetFormatNumber(ByRef Amount As Decimal) As String
        Return FormatNumber(Amount, 0, TriState.False, TriState.UseDefault, TriState.False)
    End Function

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckUserPrivilege()
        If (Not Page.IsPostBack) Then
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
            Me.btnDownload.Enabled = False
            BindToDataGrid()
        End If
        SetControls()
    End Sub

    Private Sub dtgMain_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgMain.ItemCommand
        Dim dirName As String = KTB.DNet.Lib.WebConfig.GetValue("CessieFileDirectory").ToString()
        'dirName = "../DataTemp"

        Dim _objCessie As New Cessie
        Dim _facade As New CessieFacade(User)
        _objCessie = GetCessie(dtgMain.DataKeys(e.Item.ItemIndex))
        If (e.CommandName = "DownloadFilePdf") Then
            If (_objCessie.PDFFile.Length > 0) Then
                If (_objCessie.DownloadedBy = String.Empty) AndAlso CType(Session("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.LEASING Then
                    _objCessie.DownloadedTime = DateTime.Now
                    _objCessie.DownloadedBy = User.Identity.Name
                    _facade.Update(_objCessie)
                End If
                'Response.Redirect("../DataTemp/" & _objCessie.PDFFile)
                AccessFile(_objCessie.PDFFile, dirName)
            Else
                MessageBox.Show("File Not Exist")
            End If
        ElseIf (e.CommandName = "DownloadFilePdf2") Then
            If (_objCessie.PDFFile2.Length > 0) Then
                If (_objCessie.DownloadedBy = String.Empty) AndAlso CType(Session("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.LEASING Then
                    _objCessie.DownloadedTime = DateTime.Now
                    _objCessie.DownloadedBy = User.Identity.Name
                    _facade.Update(_objCessie)
                End If
                'Response.Redirect("../DataTemp/" & _objCessie.PDFFile)
                AccessFile(_objCessie.PDFFile2, dirName)
            Else
                MessageBox.Show("File Not Exist")
            End If

        ElseIf (e.CommandName = "DownloadFileTxt") Then
            If (_objCessie.TextFile.Length > 0) Then
                If (_objCessie.DownloadedBy = String.Empty) AndAlso CType(Session("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.LEASING Then
                    _objCessie.DownloadedBy = User.Identity.Name
                    _facade.Update(_objCessie)
                End If
                'Response.Redirect("../DataTemp/" & _objCessie.TextFile)
                AccessFile(_objCessie.TextFile, dirName)
            Else
                MessageBox.Show("File Not Exist")
            End If

        End If
    End Sub

    Private Sub AccessFile(ByVal fileName As String, ByVal DirectoryName As String)

        Dim finfo As New fileInfo(fileName)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Dim fileInfo As New fileInfo(DirectoryName & "\" & fileName)
        Dim fileInfo1 As New fileInfo(DirectoryName)
        Dim destFilePath As String = fileInfo.FullName
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                If (fileInfo.Exists) Then
                    'Response.ContentType = "application/x-download"
                    'Response.AddHeader("Content-Disposition", "attachment;filename=" & fileInfo.Name)

                    'Response.WriteFile(destFilePath)
                    'Response.End()
                    Response.Redirect("../Download.aspx?file=" & destFilePath)
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                End If
                imp.StopImpersonate()
            End If

        Catch ex As Exception
            'MessageBox.Show(SR.DownloadFail(fileName))
            'Response.End()
        End Try
        'Return finfo
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim arlData As ArrayList = CType(_sessHelper.GetSession("FrmCessie.Data"), ArrayList)
        Dim arlToTransfer As ArrayList

        If arlData.Count > 0 Then
            Dim _fileHelper As New FileHelper
            Dim str As FileInfo

            arlToTransfer = New ArrayList
            For Each di As DataGridItem In Me.dtgMain.Items
                Dim chkExport As CheckBox = di.FindControl("chkExport")
                If chkExport.Checked Then
                    arlToTransfer.Add(arlData(di.ItemIndex))
                End If
            Next
            Try
                str = _fileHelper.TransferCessieToSAP(arlToTransfer)
                If Not IsNothing(str) Then
                    'Update NumOfTransfered
                    Dim oCFac As New CessieFacade(User)
                    For Each oC As Cessie In arlToTransfer
                        oC.NumOfTransfered += 1
                        oCFac.Update(oC)
                    Next
                    MessageBox.Show(SR.UploadSucces(str.Name))
                Else
                    MessageBox.Show(SR.UploadFail(str.Name))
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim aC As ArrayList = CType(Me._sessHelper.GetSession("FrmCessie.Data"), ArrayList)

        If Not IsNothing(aC) AndAlso aC.Count > 0 Then
            Dim chkExport As CheckBox
            Dim IsChecked As Boolean = False

            For Each di As DataGridItem In Me.dtgMain.Items
                chkExport = di.FindControl("chkExport")
                If chkExport.Checked Then
                    IsChecked = True
                    Exit For
                End If
            Next
            If IsChecked = False Then
                MessageBox.Show("Tidak ada data yang dipilih")
                Exit Sub
            End If
            DoDownload(aC)
        Else
            MessageBox.Show("Tidak ada yang didownload")
        End If

    End Sub
End Class
