Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search

Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports System.IO
Imports KTB.DNet.Security



Public Class PopUpPaymentStatus
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblNoReqPO As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoReqPOValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchTerm1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoP3B As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenis As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaPembayaran As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents lblP3BNumber As System.Web.UI.WebControls.Label
    Protected WithEvents txtlblNoReqPO As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblJenisValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPembayaranValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaPembayaranValue As System.Web.UI.WebControls.Label
    Protected WithEvents dtgEquipmentPayment As System.Web.UI.WebControls.DataGrid
    Protected WithEvents OpClient As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lblTotalPayPCT As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaPCT As System.Web.UI.WebControls.Label
    Protected WithEvents lblP3BDate As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents OpClient1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents OpClient2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents Hidden2 As System.Web.UI.HtmlControls.HtmlInputHidden

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"

    Private PENumber As String
    Private objEquipmentSalesHeader As EquipmentSalesHeader
    Private objEquipmentSalesPayment As EquipmentSalesPayment
    Private sessionHelper As New SessionHelper
    Private objDealer As Dealer
#End Region

#Region "Custom Method"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            ViewState("Count") = 0
            PENumber = Request.QueryString("PENumber")
            If PENumber <> String.Empty Then
                If Request.QueryString("type") <> "1" Then 'From PopUp Payment
                    txtlblNoReqPO.Visible = False
                    btnCari.Visible = False
                    dtgEquipmentPayment.ShowFooter = False
                    dtgEquipmentPayment.Columns(6).Visible = False
                    dtgEquipmentPayment.Columns(7).Visible = False
                    GetPEInformation(PENumber)
                    OpClient2.Visible = False
                Else                                        ' From Daftar P3B
                    txtlblNoReqPO.Text = PENumber
                    GetPEInformation(PENumber)
                    If Not OpClient1 Is Nothing Then
                        OpClient1.Visible = False
                    End If
                    lblNoReqPOValue.Visible = False
                End If
            Else                                            ' From Menu
                If Not OpClient2 Is Nothing AndAlso Not OpClient1 Is Nothing Then
                    OpClient2.Visible = False
                    OpClient1.Visible = False
                End If
                lblNoReqPOValue.Visible = False
            End If
        End If
        'Hidden2.Value = CInt(ViewState("Count")) + 1
        'ViewState("Count") = Hidden2.Value
    End Sub

    Private Sub GetPEInformation(ByVal PONumber As String)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EquipmentSalesHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentSalesHeader), "RegPONumber", MatchType.Exact, PONumber))
        Dim arrEquipment As ArrayList = New EquipmentSalesHeaderFacade(User).Retrieve(criterias)
        If arrEquipment.Count > 0 Then
            objEquipmentSalesHeader = arrEquipment(0)
            sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
            BindData()
        Else
            MessageBox.Show("P3B Tidak Terdaftar")
            ClearData()
        End If

    End Sub
    Private Sub ClearData()
        lblNoReqPOValue.Text = String.Empty
        lblJenisValue.Text = String.Empty
        lblDealerCode.Text = String.Empty
        lblSearchTerm1.Text = String.Empty
        lblP3BNumber.Text = String.Empty
        lblP3BDate.Text = String.Empty

        lblStatus.Text = String.Empty

        lblTotalValue.Text = String.Empty
        lblTotalPembayaranValue.Text = String.Empty
        lblSisaPembayaranValue.Text = String.Empty
        lblTotalPayPCT.Text = "(0%)"
        lblSisaPCT.Text = "(0%)"

        dtgEquipmentPayment.DataSource = Nothing
        dtgEquipmentPayment.DataBind()
    End Sub

    Private Sub BindData()
        lblNoReqPOValue.Text = objEquipmentSalesHeader.RegPONumber
        lblJenisValue.Text = CType(objEquipmentSalesHeader.Kind, EquipmentKind.EquipmentKindEnum).ToString
        lblDealerCode.Text = objEquipmentSalesHeader.Dealer.DealerCode
        lblSearchTerm1.Text = objEquipmentSalesHeader.Dealer.SearchTerm1
        lblP3BNumber.Text = objEquipmentSalesHeader.PONumber
        lblP3BDate.Text = Format(objEquipmentSalesHeader.CreatedTime, "dd MMMMMM yyyy")

        Dim EnumStatus As EquipmentStatus.EquipmentStatusEnum = objEquipmentSalesHeader.Status
        lblStatus.Text = EnumStatus.ToString

        lblTotalValue.Text = FormatNumber(objEquipmentSalesHeader.Total * 110 / 100, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblTotalPembayaranValue.Text = FormatNumber(objEquipmentSalesHeader.TotalPembayaran, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblSisaPembayaranValue.Text = FormatNumber(CType(lblTotalValue.Text, Double) - CType(lblTotalPembayaranValue.Text, Double), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        If objEquipmentSalesHeader.Total <> 0 Then
            lblTotalPayPCT.Text = "(" & FormatNumber(CType(objEquipmentSalesHeader.TotalPembayaran, Double) * 100 / (CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100)), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & "%)"
            lblSisaPCT.Text = "(" & FormatNumber((CType(lblTotalValue.Text, Double) - CType(objEquipmentSalesHeader.TotalPembayaran, Double)) * 100 / (CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100)), 2, TriState.UseDefault, TriState.UseDefault, TriState.True) & "%)"
        Else
            lblTotalPayPCT.Text = "(0%)"
            lblSisaPCT.Text = "(0%)"
        End If
        If CType(objEquipmentSalesHeader.TotalPembayaran, Double) * 100 / (CType(objEquipmentSalesHeader.Total, Double) + (10 * CType(objEquipmentSalesHeader.Total, Double) / 100)) >= 40 AndAlso (objEquipmentSalesHeader.IsKTBView <> 1) Then
            CreateTextFile()
        End If
        dtgEquipmentPayment.DataSource = objEquipmentSalesHeader.EquipmentSalesPayments
        dtgEquipmentPayment.DataBind()
    End Sub

    Sub dtgEquipmentPayment_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs)
        If (E.Item.ItemIndex <> -1) Then
            objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
            objEquipmentSalesPayment = objEquipmentSalesHeader.EquipmentSalesPayments(E.Item.ItemIndex)
            If objEquipmentSalesPayment.LastUpdateBy <> String.Empty Then
                E.Item.Cells(5).Text = UserInfo.Convert(objEquipmentSalesPayment.LastUpdateBy)
            Else
                E.Item.Cells(5).Text = UserInfo.Convert(objEquipmentSalesPayment.CreatedBy)
            End If

            If objEquipmentSalesPayment.Status = -1 Then
                E.Item.BackColor = System.Drawing.Color.Red
            End If
            Dim lbtnDelete As LinkButton = E.Item.FindControl("lbtnDelete")
            lbtnDelete.Visible = SecurityProvider.Authorize(Context.User, SR.P3BPaymentAdd_Privilege)
        End If
        If E.Item.ItemType = ListItemType.Footer Then
            Dim lbtnAdd As LinkButton = E.Item.FindControl("lbtnAdd")
            lbtnAdd.Visible = SecurityProvider.Authorize(Context.User, SR.P3BPaymentAdd_Privilege)
        End If
    End Sub

    Sub dtgEquipmentPayment_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs)
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        Select Case (e.CommandName)
            Case "Delete"
                'Dim lbl1 As Label = e.Item.Cells(0).FindControl("lblNo")
                Dim objEquipmentSalesPaymentFacade1 As New EquipmentSalesPaymentFacade(User)

                objEquipmentSalesPayment = objEquipmentSalesPaymentFacade1.Retrieve(CType(e.Item.Cells(0).Text, Integer))
                objEquipmentSalesPayment.Status = -1
                objEquipmentSalesPaymentFacade1.Update(objEquipmentSalesPayment)
                'objEquipmentSalesPaymentFacade1.Delete(objEquipmentSalesHeader.EquipmentSalesPayments.Item(CType(lbl1.Text, Integer) - 1))
                'objEquipmentSalesHeader.EquipmentSalesPayments.Remove(objEquipmentSalesHeader.EquipmentSalesPayments.Item(CType(lbl1.Text, Integer) - 1))
                BindData()
                GetPEInformation(txtlblNoReqPO.Text)
            Case "Add"
                Dim txt1 As TextBox = e.Item.FindControl("txtNomorKwitansi")
                Dim txt2 As TextBox = e.Item.FindControl("txtJumlah")
                If (ValidateItem(txt1.Text.ToUpper, txt2.Text) And ValidateDuplication(txt1.Text.ToUpper)) Then
                    If PaymentIsValid(CType(txt2.Text, Double)) Then
                        objEquipmentSalesPayment = New EquipmentSalesPayment
                        objEquipmentSalesPayment.Amount = txt2.Text
                        objEquipmentSalesPayment.EquipmentSalesHeader = objEquipmentSalesHeader
                        objEquipmentSalesPayment.KwitansiNumber = txt1.Text
                        Dim objEquipmentSalesPaymentFacade As New EquipmentSalesPaymentFacade(User)
                        objEquipmentSalesPaymentFacade.Insert(objEquipmentSalesPayment)
                    Else
                        MessageBox.Show("Nilai Kwitansi < 40% Sisa Pembayaran")
                    End If
                Else
                    Exit Sub
                End If
                GetPEInformation(objEquipmentSalesHeader.RegPONumber)
        End Select
    End Sub

    Private Function PaymentIsValid(ByVal amount As Double) As Boolean
        objEquipmentSalesHeader = sessionHelper.GetSession("Equipment")
        Dim IsFirstPayment As Boolean = True
        For Each item As EquipmentSalesPayment In objEquipmentSalesHeader.EquipmentSalesPayments
            If item.Status = 0 Then
                IsFirstPayment = False
                Exit For
            End If
        Next
        If IsFirstPayment Then
            If amount >= (CType(KTB.DNet.Lib.WebConfig.GetValue("BatasPembayaranPertamaP3B"), Double) * (objEquipmentSalesHeader.Total * 110 / 100)) / 100 Then
                Return True
            Else
                Return False
            End If

        Else
            Return True
        End If
    End Function

#End Region

#Region "Event Hendlers"

    Private Function ValidateItem(ByVal NomorKwitansi As String, ByVal Amount As String) As Boolean
        If (NomorKwitansi = String.Empty Or Amount = String.Empty) Then
            lblError.Text = "Error : Nomor Kwitansi dan Jumlah Tidak boleh Kosong"
            Return False
        End If
        Return True
    End Function

    Private Function ValidateDuplication(ByVal NomorKwitansi As String) As Boolean
        For Each item As EquipmentSalesPayment In objEquipmentSalesHeader.EquipmentSalesPayments
            If (item.KwitansiNumber = NomorKwitansi) Then
                lblError.Text = "Error : Duplikasi Nomor Kwitansi"
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        If txtlblNoReqPO.Text <> String.Empty Then
            GetPEInformation(txtlblNoReqPO.Text)
        End If
    End Sub

    Private Sub CreateTextFile()
        Dim _fileHelper As New FileHelper
        Dim fileInfo As FileInfo
        Dim arrListEquipmentSalesHeader As New ArrayList
        arrListEquipmentSalesHeader.Add(objEquipmentSalesHeader)
        Try
            fileInfo = _fileHelper.TransferPEtoSAP(arrListEquipmentSalesHeader)
            UpdateStatus()
            MessageBox.Show(SR.UploadSucces(fileInfo.Name))
        Catch ex As Exception
            MessageBox.Show(SR.UploadFail(fileInfo.Name))
        End Try
    End Sub

    Private Sub UpdateStatus()
        objEquipmentSalesHeader.IsKTBView = 1
        objEquipmentSalesHeader.Status = EquipmentStatus.EquipmentStatusEnum.Konfirmasi
        Dim objEquipmentSalesHeaderFacade As New EquipmentSalesHeaderFacade(User)
        objEquipmentSalesHeaderFacade.Update(objEquipmentSalesHeader)
        sessionHelper.SetSession("Equipment", objEquipmentSalesHeader)
    End Sub

#End Region

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessionHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If
    End Sub
End Class