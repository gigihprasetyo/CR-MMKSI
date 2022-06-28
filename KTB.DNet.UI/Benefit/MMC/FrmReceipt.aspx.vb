Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Benefit

Public Class FrmReceipt
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblCodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoClaim As System.Web.UI.WebControls.Label


    Protected WithEvents lblTanggalFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents lblKet As System.Web.UI.WebControls.Label

    Protected WithEvents lblJumlah As System.Web.UI.WebControls.Label
    Protected WithEvents icTglKuitansi As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglFaktur As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtAmount As System.Web.UI.WebControls.TextBox

    Protected WithEvents txtNoKuitansi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button

    Protected WithEvents hfIdHeader As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button

    Protected WithEvents txtNomorFaktur As System.Web.UI.WebControls.TextBox

    Protected WithEvents btnCetak As System.Web.UI.WebControls.Button
    Protected WithEvents lblNoKuitansi As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents lblAmount As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglKuitansi As System.Web.UI.WebControls.Label

    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button


    Protected WithEvents txtTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox

    Protected WithEvents panelInputan As System.Web.UI.WebControls.Panel
    Protected WithEvents panelCetak As System.Web.UI.WebControls.Panel

    Protected WithEvents lblTerbilang As System.Web.UI.WebControls.Label
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label

    Protected WithEvents lblCodeDealer1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer1 As System.Web.UI.WebControls.Label

    Protected WithEvents lblNamaDealer2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblKet1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnKembali1 As System.Web.UI.WebControls.Button

    Protected WithEvents hfPpn As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hfPph As System.Web.UI.WebControls.HiddenField


    Protected WithEvents lblNoClaimPrint As System.Web.UI.WebControls.Label
    Protected WithEvents hfNoClaimPrint As System.Web.UI.WebControls.HiddenField

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private sessHelper As New SessionHelper

    Private objBenefitClaimReceipt As New BenefitClaimReceipt

   
#Region "Private Property"
    Private Property SesType() As EnumAlertManagement.AlertManagementType
        Get
            Return CType(sessHelper.GetSession("ListAlertMasterType"), EnumAlertManagement.AlertManagementType)
        End Get
        Set(ByVal Value As EnumAlertManagement.AlertManagementType)
            sessHelper.SetSession("ListAlertMasterType", Value)
        End Set
    End Property
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.AlertManagementListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ALERT MANAGEMENT - Alert Managemen List")
        End If
    End Sub

    Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(context.User, SR.AlertManagementListView_Privilege)
#End Region

    Private Sub RetrieveDealer()
        Dim objSessionHelper As New SessionHelper
        Dim objDealer As Dealer = CType(objSessionHelper.GetSession("DEALER"), Dealer)
        If Not objDealer Is Nothing Then
            
            If Not objDealer.DealerGroup Is Nothing Then 'untuk dealer side
                lblCodeDealer.Text = objDealer.DealerCode
                lblNamaDealer.Text = objDealer.DealerName
            Else
                btnSimpan.Visible = False
            End If

        Else

        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'InitiateAuthorization()
        If Not IsPostBack Then
         
            BindDataGrid()

            RetrieveDealer()

            icTglKuitansi.Value = Now.Date
            If Not Request.QueryString("justview") Is Nothing Then
                btnSimpan.Visible = False
            End If
        End If
    End Sub

 

    Private Sub BindDataGrid()
        Dim id As String = Request.QueryString("id") 'id claimheader

        'Dim idchassis As String = Request.QueryString("idchassis")
        'Dim norecom As String = Request.QueryString("norecom")
        If Not id Is Nothing Then

            hfIdHeader.Value = id

            Dim objBenefitClaimHeader As BenefitClaimHeader = New BenefitClaimHeaderFacade(User).Retrieve(CInt(id))
            If Not objBenefitClaimHeader Is Nothing Then
                Dim idDealer As String = ""
                Dim namaDealer As String = ""
                'For Each el As BenefitMasterDealer In objBenefitClaimHeader.BenefitMasterDetail.BenefitMasterHeader.BenefitMasterDealers
                '    idDealer += el.Dealer.DealerCode
                '    namaDealer += el.Dealer.DealerName
                'Next
                idDealer = objBenefitClaimHeader.Dealer.DealerCode
                namaDealer = objBenefitClaimHeader.Dealer.DealerName

                lblCity.Text = objBenefitClaimHeader.Dealer.City.CityName


                For Each el As BenefitClaimReceipt In objBenefitClaimHeader.BenefitClaimReceipts
                    txtName.Text = el.Name
                    txtTitle.Text = el.Title
                    icTglFaktur.Value = el.FakturPajakDate
                Next

                lblCodeDealer.Text = idDealer

                lblNamaDealer.Text = namaDealer

                lblNoClaim.Text = objBenefitClaimHeader.ClaimRegNo
                Dim temp1 As String = ""
                Dim temp2 As String = ""
                Dim objFakturdate As DateTime
                Dim tempAmount As Decimal = 0, tempamount1 As Decimal = 0
                Dim CountUnit As Integer = 0
                For Each el As BenefitClaimDetails In objBenefitClaimHeader.BenefitClaimDetailss
                    If Not el.ChassisMaster Is Nothing And Not el.ChassisMaster.EndCustomer Is Nothing Then
                        '  temp1 += el.ChassisMaster.EndCustomer.FakturNumber
                        ' temp2 += el.ChassisMaster.EndCustomer.FakturDate.ToString("dd/MM/yyyy")
                        temp1 = el.ChassisMaster.EndCustomer.FakturNumber
                        temp2 = el.ChassisMaster.EndCustomer.FakturDate.ToString("dd/MM/yyyy")
                        objFakturdate = el.ChassisMaster.EndCustomer.FakturDate
                    End If
                    tempAmount = tempAmount + el.BenefitMasterDetail.Amount
                    CountUnit = CountUnit + 1
                Next
                tempamount1 = tempAmount
                Dim total As Decimal = tempamount1
                If Not tempAmount = 0 Then
                    'tempAmount = tempAmount + (0.14999999999999999 * tempamount1)
                    'If objBenefitClaimHeader.BenefitType.Name.ToLower.Contains("leasin") = True Then
                    '    tempAmount = tempAmount + (0.14999999999999999 * tempamount1)
                    'Else
                    '    tempAmount = tempAmount + (0.02 * tempamount1)
                    'End If


                    Dim nilaiPph As Decimal = 0
                    If objBenefitClaimHeader.BenefitType.LeasingBox = 1 Then
                        nilaiPph = Math.Round(((tempamount1 / (1 - 0.15)) - tempamount1))
                        hfPph.Value = nilaiPph.ToString
                        total = total + nilaiPph

                        ' ElseIf objDomain.BenefitType.Name.ToLower.Contains("cashbac") = True Then
                    Else
                        nilaiPph = Math.Round(((tempamount1 / (1 - 0.02)) - tempamount1))
                        hfPph.Value = nilaiPph.ToString()
                        total = total + nilaiPph

                    End If

                    hfPpn.Value = Math.Round((0.1 * (nilaiPph + tempamount1))).ToString("#,##0.00")
                    total = Math.Round(total + (0.1 * (nilaiPph + tempamount1)))

                End If




                txtAmount.Text = total.ToString("#,##0.00")

                hfNoClaimPrint.Value = objBenefitClaimHeader.ClaimRegNo


                lblKet.Text = "Untuk pembayaran sales campaign " & objBenefitClaimHeader.BenefitType.Name & _
                    " sebanyak " & CountUnit & " unit."

                'lblNomorFaktur.Text = temp1
                lblTanggalFaktur.Text = temp2
                'icTglFaktur.Value = objFakturdate
            End If
        End If

        Dim objBenefitClaimReceipt1 As BenefitClaimReceipt = New BenefitClaimReceiptFacade(User).RetrieveByClaimHeader(id)
        If Not objBenefitClaimReceipt1.ID = Nothing Then
            txtNoKuitansi.Text = objBenefitClaimReceipt1.ReceiptNo
            txtNomorFaktur.Text = objBenefitClaimReceipt1.FakturPajakNo
            ' txtAmount.Text = objBenefitClaimReceipt1.ReceiptAmount.ToString("#,##0.00")
            icTglKuitansi.Value = objBenefitClaimReceipt1.ReceiptDate
            txtName.Text = objBenefitClaimReceipt1.Name
            txtTitle.Text = objBenefitClaimReceipt1.Title
        End If

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not hfIdHeader.Value = "" Then
            If Not txtNomorFaktur.Text = "" And Not txtNoKuitansi.Text = "" Then
                Dim objHeader As BenefitClaimHeader = New BenefitClaimHeaderFacade(User).Retrieve(CInt(hfIdHeader.Value))
                Dim objBenefitClaimReceipt1 As BenefitClaimReceipt = New BenefitClaimReceiptFacade(User).RetrieveByClaimHeader(hfIdHeader.Value)

                objBenefitClaimReceipt1.BenefitClaimHeader = objHeader
                objBenefitClaimReceipt1.ReceiptNo = txtNoKuitansi.Text
                objBenefitClaimReceipt1.FakturPajakNo = txtNomorFaktur.Text
                objBenefitClaimReceipt1.ReceiptDate = icTglKuitansi.Value
                objBenefitClaimReceipt1.ReceiptAmount = CDec(txtAmount.Text.Replace(".", ""))
                objBenefitClaimReceipt1.PPHTotal = CDec(hfPph.Value)
                objBenefitClaimReceipt1.FakturPajakDate = icTglFaktur.Value
                'objBenefitClaimReceipt1.VATTotal = CDec(0.1 * (objBenefitClaimReceipt1.ReceiptAmount + objBenefitClaimReceipt1.PPHTotal))
                objBenefitClaimReceipt1.VATTotal = CDec(hfPpn.Value)

                objBenefitClaimReceipt1.Name = txtName.Text
                objBenefitClaimReceipt1.Title = txtTitle.Text

                Dim n As Integer = -1
                If Not objBenefitClaimReceipt1.ID = Nothing Then
                    n = New BenefitClaimReceiptFacade(User).UpdateReceipt(objBenefitClaimReceipt1)
                Else
                    n = New BenefitClaimReceiptFacade(User).InsertReceipt(objBenefitClaimReceipt1)
                End If

                If n = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show("Simpan Berhasil")
                    '   Response.Redirect("FrmBenefitClaimList.aspx")

                End If
            Else
                MessageBox.Show("Isi Faktur Pajak dan Nomor Kuitansi")
            End If


        End If

    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        If (btnSimpan.Visible = True) Or (Not Request.QueryString("justview") Is Nothing And btnSimpan.Visible = False) Then
            Response.Redirect("FrmBenefitClaimList.aspx")
        Else
            txtNoKuitansi.Visible = True
            txtNomorFaktur.Visible = txtNoKuitansi.Visible
            txtAmount.Visible = txtNoKuitansi.Visible
            icTglKuitansi.Visible = txtNoKuitansi.Visible
            btnSimpan.Visible = txtNoKuitansi.Visible
            btnCetak.Visible = txtNoKuitansi.Visible
            lblNoKuitansi.Visible = Not txtNoKuitansi.Visible
            lblNomorFaktur.Visible = lblNoKuitansi.Visible
            lblAmount.Visible = lblNoKuitansi.Visible
            lblTglKuitansi.Visible = lblNoKuitansi.Visible
            btnPrint.Visible = lblNoKuitansi.Visible

            lblNoClaimPrint.Visible = lblNoKuitansi.Visible
        End If



    End Sub

    Private Sub btnKembali1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali1.Click
        panelCetak.Visible = False
        panelInputan.Visible = Not panelCetak.Visible



    End Sub

    Private Sub btnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCetak.Click
        lblNoKuitansi.Text = txtNoKuitansi.Text


        lblNoClaimPrint.Text = hfNoClaimPrint.Value

        lblAmount.Text = txtAmount.Text.ToString()
        lblTglKuitansi.Text = Format(icTglKuitansi.Value, "dd-MM-yyyy")

        Dim cf As New CommonFunction

        ' lblTerbilang.Text = New BenefitClaimReceiptFacade(User).Terbilang(CInt(lblAmount.Text.Replace(".", "")))
        'lblTerbilang.Text = New BenefitClaimReceiptFacade(User).Terbilang(CInt(lblAmount.Text.Replace(".", "")))
        lblTerbilang.Text = cf.Terbilang(CInt(lblAmount.Text.Replace(".", "")))
        'lblTerbilang.Text = lblTerbilang.Text & " Rupiah"

        lblCodeDealer1.Text = lblCodeDealer.Text
        lblNamaDealer1.Text = lblNamaDealer.Text
        lblNamaDealer2.Text = lblNamaDealer1.Text

        lblName.Text = txtName.Text
        lblTitle.Text = txtTitle.Text

        lblKet1.Text = lblKet.Text

        panelCetak.Visible = True
        panelInputan.Visible = Not panelCetak.Visible
    End Sub
   
End Class
