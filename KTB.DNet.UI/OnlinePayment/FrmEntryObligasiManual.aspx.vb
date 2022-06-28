Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.OnlinePayment
Imports KTB.DNet.Utility
Imports KTB.DNet.Security



Public Class FrmEntryObligasiManual
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblAlamat As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents ddlTipeAssignment As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtJumlah As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKeterangan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkAgreement As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnReset As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents lblTglTransaksi As System.Web.UI.WebControls.Label
    Protected WithEvents icTransDate As KTB.DNet.WebCC.IntiCalendar

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
    Private objPaymentObligasiFacade As PaymentObligationFacade = New PaymentObligationFacade(User)

    Private bEntryPriv As Boolean

#Region "Check Privilage"
    Private Sub InitiateAuthorization()
        ' privilage not  defined yet
        'If Not SecurityProvider.Authorize(context.User, SR.Pembayaran_pembayaran_manual_lihat_privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=INFORMASI PEMBAYARAN - Pembayaran Manual")
        'End If

        bEntryPriv = SecurityProvider.Authorize(context.User, SR.Pembayaran_pembayaran_manual_buat_privilege)

        btnReset.Visible = bEntryPriv
        btnSimpan.Visible = bEntryPriv
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        InitiateAuthorization()

        If Not IsPostBack() Then
            Dim objDealer As Dealer = New SessionHelper().GetSession("DEALER")
            lblDealerCode.Text = objDealer.DealerCode + " / " + objDealer.SearchTerm1
            lblNamaDealer.Text = objDealer.DealerName
            lblAlamat.Text = objDealer.Address
            lblTglTransaksi.Text = Date.Today.ToString("dd/MM/yyyy")
            BindDDL()
            _sessHelper.SetSession("Mode", "Insert")
        End If
        'Me.chkAgreement.Attributes.Add("onclick", "ToggleDisclaimerAgreement();")
    End Sub
    Private Sub BindDDL()
        ddlTipeAssignment.Items.Clear()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PaymentAssignmentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PaymentAssignmentType), "Status", MatchType.Exact, CByte(EnumObligationType.ObligationTypeStatus.Aktif)))
        criterias.opAnd(New Criteria(GetType(PaymentAssignmentType), "SourceDocument", MatchType.Exact, CByte(EnumOnlinePayment.SourceDocument.MANUAL)))
        Dim _arrType As New ArrayList
        _arrType = New PaymentAssignmentTypeFacade(User).Retrieve(criterias)
        For Each liType As PaymentAssignmentType In _arrType
            ddlTipeAssignment.Items.Add(New ListItem(liType.Code, liType.ID))
        Next
        ddlTipeAssignment.Items.Insert(0, New ListItem("Pilih Tipe Assignment", -1))
        ddlTipeAssignment.DataBind()
    End Sub

    Private Sub DatatoObject(ByVal obj As PaymentObligation)
        Dim objPaymentAssignmentType As PaymentAssignmentType = New PaymentAssignmentTypeFacade(User).Retrieve(CInt(ddlTipeAssignment.SelectedValue))
        obj.Dealer = New SessionHelper().GetSession("DEALER")
        obj.PaymentAssignmentType = objPaymentAssignmentType
        obj.Assignment = objPaymentAssignmentType.Description
        If Not IsNothing(objPaymentAssignmentType.PaymentObligationType) Then
            obj.PaymentObligationType = objPaymentAssignmentType.PaymentObligationType
        End If
        obj.Sequence = 0
        obj.Amount = CDec(txtJumlah.Text)
        obj.SourceDocument = CByte(EnumOnlinePayment.SourceDocument.MANUAL)
        obj.Status = CByte(EnumObligationType.ObligationTypeStatus.Aktif)
        obj.Description = txtKeterangan.Text
        obj.DocDate = Date.Today
        obj.TransactionDueDate = icTransDate.Value
        obj.DueDate = icTransDate.Value
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If ValidateData() Then
            If _sessHelper.GetSession("Mode") = "Insert" Then
                Dim objPaymentObligasi As New PaymentObligation
                DatatoObject(objPaymentObligasi)
                Dim n As Integer = objPaymentObligasiFacade.InsertFromService(objPaymentObligasi)
                If n <> -1 Then
                    MessageBox.Show("Penyimpanan sukses")
                    _sessHelper.SetSession("objPayment", objPaymentObligasi)
                    _sessHelper.SetSession("Mode", "Update")
                Else
                    MessageBox.Show("Penyimpanan gagal")
                End If
            Else
                Dim objPaymentObligasi As New PaymentObligation
                objPaymentObligasi = CType(_sessHelper.GetSession("objPayment"), PaymentObligation)
                DatatoObject(objPaymentObligasi)
                If objPaymentObligasiFacade.UpdateFromService(objPaymentObligasi) <> -1 Then
                    MessageBox.Show("Penyimpanan sukses")
                    _sessHelper.SetSession("Mode", "Update")
                Else
                    MessageBox.Show("Penyimpanan gagal")
                End If
            End If
        End If

    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ddlTipeAssignment.SelectedIndex = 0
        txtJumlah.Text = String.Empty
        txtKeterangan.Text = String.Empty
        lblTglTransaksi.Text = Date.Today.ToString("dd/MM/yyyy")
        icTransDate.Value = Date.Today
        chkAgreement.Checked = False
        _sessHelper.SetSession("Mode", "Insert")
    End Sub
    Private Function ValidateData()
        Dim bcheck As Boolean = True
        If chkAgreement.Checked = False Then
            MessageBox.Show("Anda belum menyetujui bahwa data tersebut adalah benar")
            bcheck = False
        End If
        If ddlTipeAssignment.SelectedValue = -1 Then
            MessageBox.Show("Silahkan pilih tipe assignment")
            bcheck = False
        End If
        If txtJumlah.Text.Trim = String.Empty Then
            MessageBox.Show("Jumlah masih kosong")
            bcheck = False
        End If
        If txtKeterangan.Text.Trim = String.Empty Then
            MessageBox.Show("Keterangan masih kosong")
            bcheck = False
        End If
        Return bcheck
    End Function
End Class
