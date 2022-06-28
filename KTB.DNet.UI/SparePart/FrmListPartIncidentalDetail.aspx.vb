#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmListPartIncidentalDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoPermintaan As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalinput As System.Web.UI.WebControls.Label
    Protected WithEvents lblnoPolisi As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblPIC As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents lblId As System.Web.UI.WebControls.Label
    Protected WithEvents lblPICValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPolisiValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPermintaanValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblEmail As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lblWOvalue As System.Web.UI.WebControls.Label
    Protected WithEvents lblEmailToValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnHapus As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPartDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSendEmail As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents lblNoSurat As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoSuratValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblTelp As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTelpValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunProduksi As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRangka As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRangkaValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipeValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunProduksiValue As System.Web.UI.WebControls.Label


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"

    Private objPartHeader As PartIncidentalHeader
    Private objPartDetail As PartIncidentalDetail
    'Private _Part As New KTB.Dnet.Domain.PartIncidentalDetail
    Private arlPartDetail As ArrayList
    Private SessionHelper As New SessionHelper
    Dim _id As String

#End Region

#Region "Custom Method"
    Private Sub GetHeaderID()
        _id = Request.QueryString("Id")
        If _id <> String.Empty Then
            objPartHeader = New PartIncidentalHeaderFacade(User).Retrieve(CInt(_id))
            lblNomorPermintaanValue.Text = objPartHeader.RequestNumber
            lblKodeDealerValue.Text = objPartHeader.Dealer.DealerCode & "/" & objPartHeader.Dealer.SearchTerm2
            lblTanggalValue.Text = Format(objPartHeader.CreatedTime, "dd/MM/yyyy")
            lblNomorPolisiValue.Text = objPartHeader.PoliceNumber
            lblNoSuratValue.Text = objPartHeader.DealerMailNumber
            lblStatusValue.Text = CType(objPartHeader.KTBStatus, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString

          

            If objPartHeader.EmailStatus = PartIncidentalStatus.PartIncidentalEmailStatusEnum.Dikirim Then
                If Not objPartHeader.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai Then
                    dtgPartDetail.Columns(1).Visible = False
                    btnSendEmail.Visible = False
                    btnHapus.Visible = False
                Else
                    dtgPartDetail.Columns(1).Visible = True
                    btnSendEmail.Visible = True
                    btnHapus.Visible = True
                End If
            Else
                If Not objPartHeader.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru Then
                    dtgPartDetail.Columns(1).Visible = False
                    btnSendEmail.Visible = False
                    btnHapus.Visible = False
                Else
                    dtgPartDetail.Columns(1).Visible = True
                    btnSendEmail.Visible = True
                    btnHapus.Visible = True
                End If
            End If

            lblPICValue.Text = objPartHeader.PIC
            lblWOvalue.Text = objPartHeader.WorkOrder
            lblTelpValue.Text = objPartHeader.Phone
            lblNoRangkaValue.Text = objPartHeader.ChassisNumber
            If Val(objPartHeader.AssemblyYear) = 1980 Then
                lblTahunProduksiValue.Text = "N/A"
            Else
                lblTahunProduksiValue.Text = objPartHeader.AssemblyYear
            End If

            lblTipeValue.Text = objPartHeader.VehicleType
        End If
    End Sub

    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.ViewPartIncidentalDetail_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Permintaan Khusus dari Dealer")
        End If

        btnHapus.Visible = SecurityProvider.Authorize(Context.User, SR.DeletePartIncidentalDetail_Privilege)
        btnSendEmail.Visible = SecurityProvider.Authorize(Context.User, SR.EmailPartIncidentalDetail_Privilege)
        'ddlAction.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranStatusTolakan_Privilege)
        'Label11.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranStatusTolakan_Privilege)

        'Label10.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranNomorAccounting_Privilege)
        'txtDocNumber.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranNomorAccounting_Privilege)
    End Sub

#End Region

#Region "EventHendlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        If Not IsPostBack Then

            'ViewState("Count") = 0
            GetHeaderID()
            BindToDataGrid()
        End If
        'HiddenField.Value = CInt(ViewState("Count")) + 1
        'ViewState("Count") = HiddenField.Value
        _id = Request.QueryString("Id")
        If btnSendEmail.Visible = True Then
            btnSendEmail.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Sparepart/FrmPartIncidentalSendEmail.aspx?Id=" & _id, "", 300, 300, "Back")
        End If
        btnHapus.Attributes.Add("OnClick", "return confirm('Yakin Data ini Akan Dihapus ??');")
    End Sub

    Private Sub BindToDataGrid()
        dtgPartDetail.DataSource = objPartHeader.PartIncidentalDetails
        If objPartHeader.PartIncidentalDetails.Count <= 1 Then
            btnHapus.Visible = False
            dtgPartDetail.Columns(1).Visible = False
        End If
        SessionHelper.SetSession("HeaderPart", objPartHeader)
        dtgPartDetail.DataBind()
    End Sub

    Private Sub dtgPartDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPartDetail.ItemDataBound
        If Not (objPartHeader.PartIncidentalDetails.Count = 0 Or e.Item.ItemIndex = -1) Then
            objPartDetail = objPartHeader.PartIncidentalDetails(e.Item.ItemIndex)
            e.Item.Cells(3).Text = objPartDetail.SparePartMaster.PartNumber
            e.Item.Cells(4).Text = objPartDetail.SparePartMaster.PartName
            e.Item.Cells(5).Text = objPartDetail.SparePartMaster.ModelCode
            If objPartDetail.Reject = -1 Then
                e.Item.Cells(7).Text = objPartDetail.Quantity
            ElseIf objPartDetail.StatusDetail = CType(PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal_Sebagian, Short) Then
                ' Jika Cek PO sudah ada ...
                If objPartDetail.PartIncidentalPOs.Count > 0 Then
                    Dim sumAllocatedInPO As Integer = 0
                    For Each objPartIncidentalPO As PartIncidentalPO In objPartDetail.PartIncidentalPOs
                        sumAllocatedInPO = sumAllocatedInPO + objPartIncidentalPO.Alocation
                    Next
                    e.Item.Cells(7).Text = (objPartDetail.Quantity - sumAllocatedInPO)
                Else
                    e.Item.Cells(7).Text = (objPartDetail.Quantity - objPartDetail.AlocatedQuantity)
                End If
            ElseIf objPartDetail.StatusDetail = CType(PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal, Short) Then
                e.Item.Cells(7).Text = objPartDetail.Quantity
            End If
            'e.Item.Cells(7).Text = objPartDetail.Reject
            e.Item.Cells(8).Text = objPartDetail.Remark
        End If
    End Sub

    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        objPartHeader = SessionHelper.GetSession("HeaderPart")
        If objPartHeader.PartIncidentalDetails.Count <= 1 Then
            MessageBox.Show("Hapus Data Harus dari Header")
        Else
            Dim totalDetailCount As Integer = objPartHeader.PartIncidentalDetails.Count
            Dim oDataGridItem As DataGridItem
            Dim chkExport As System.Web.UI.WebControls.CheckBox
            Dim oExArgs As New System.Collections.ArrayList
            Dim objPartFacade As New PartIncidentalDetailFacade(User)
            For Each oDataGridItem In dtgPartDetail.Items
                chkExport = oDataGridItem.FindControl("ChkExport")
                If chkExport.Checked Then
                    Dim _Part As New PartIncidentalDetail
                    _Part = objPartFacade.Retrieve(CInt(CType(oDataGridItem.FindControl("lblIDDetail"), Label).Text))
                    oExArgs.Add(_Part)
                End If
            Next
            If oExArgs.Count > 0 Then
                If oExArgs.Count < totalDetailCount Then
                    objPartFacade.Delete(oExArgs)
                Else
                    MessageBox.Show("Tidak Bisa Menghapus Semua Data")
                End If
            Else
                MessageBox.Show("Tidak Ada Data Yang Dipilih")
            End If
            End If
            RefreshGrid()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        SessionHelper.GetSession("CriteriaFromPartHeader")
        Response.Redirect("../SparePart/FrmListPartIncidental.aspx")
    End Sub

    Private Sub RefreshGrid()
        GetHeaderID()
        BindToDataGrid()
    End Sub

#End Region

   
End Class