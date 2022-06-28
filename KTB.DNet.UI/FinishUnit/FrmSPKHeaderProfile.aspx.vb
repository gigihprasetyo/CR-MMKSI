#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
#End Region
Public Class FrmSPKHeaderProfile
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtKategori As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKondisiPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents lblLevelSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblJabatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblDibuatOleh As System.Web.UI.WebControls.Label
    Protected WithEvents lblCampaignName As System.Web.UI.WebControls.Label
    Protected WithEvents dtgPesananKendaraan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoSPK As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSPKOpenDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalUnit As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalHarga As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKategoriEdit As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hideCategory As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents txtUrlToBack As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblValidasiOleh As System.Web.UI.WebControls.Label
    Protected WithEvents lblValidateDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoSPKDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoSPKReference As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesmanCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblPengiriman As System.Web.UI.WebControls.Label
    Protected WithEvents lblPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategoriKendaraan As System.Web.UI.WebControls.Label
    Protected WithEvents BtnTutup As System.Web.UI.WebControls.Button
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
    Private objSPKHeader As KTB.DNet.Domain.SPKHeader
    Private Mode As enumMode.Mode
    Private objDealer As Dealer
    Private objUser As UserInfo
    Private sessionHelper As New SessionHelper
    Private _vstSPKHeader As String = "_vstSPKHeader"
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '    txtUrlToBack.Text = sessionHelper.GetSession("PrevPageDaftarSPK")
        UserPrivilege()
        If Not IsPostBack Then
            SearchSPKHeaderAndDetail()
            BindDataToPage()
        End If
    End Sub

    Sub dtgPesananKendaraan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPesananKendaraan.ItemDataBound

        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))

        If Not (objSPKHeader.SPKDetails.Count = 0 Or e.Item.ItemIndex = -1) Then 'Or e.Item.ItemIndex = -1
            Dim objSPKDetail As SPKDetail = objSPKHeader.SPKDetails(e.Item.ItemIndex)

            If (objSPKDetail.VechileColor.ColorCode = "ZZZZ") Then
                e.Item.Cells(2).Text = objSPKDetail.VechileColor.ColorIndName
            Else
                e.Item.Cells(2).Text = objSPKDetail.VechileColor.MaterialDescription.ToString
            End If

            If Integer.TryParse(objSPKDetail.RejectedReason, Nothing) Then
                Dim lblRej As Label = CType(e.Item.FindControl("lblRej"), Label)
                lblRej.Text = enumRejectedReason.GetStringValueStatus(objSPKDetail.RejectedReason)
            End If
            Dim lblCampaignName As Label = CType(e.Item.FindControl("lblCampaignName"), Label)
            If Not IsNothing(lblCampaignName) Then
                lblCampaignName.Text = objSPKDetail.CampaignName
            End If

            Dim lblTambahan As Label = CType(e.Item.FindControl("lblTambahan"), Label)
            Dim EnumTambahan As EnumSPKAdditional.SPKAdditionalParts = objSPKDetail.Additional
            lblTambahan.Text = EnumTambahan.ToString

            e.Item.Cells(11).Text = FormatNumber(CType(objSPKDetail.TotalAmount, Long), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)


            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim imgConsumentFaktur As HtmlImage = e.Item.FindControl("imgConsumentFaktur")
                If Not IsNothing(imgConsumentFaktur) Then
                    Mode = enumMode.Mode.EditMode
                    ViewState("Mode") = Mode

                    imgConsumentFaktur.Src = "../images/edit.gif"

                    If Not IsNothing(ViewState("Mode")) AndAlso CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.ViewMode Then
                        imgConsumentFaktur.Src = "../images/detail.gif"
                        imgConsumentFaktur.Alt = "Daftar Konsumen Faktur"
                    End If

                    If Not IsNothing(ViewState("Mode")) AndAlso CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.EditMode AndAlso objSPKDetail.SPKDetailCustomers.Count > 0 Then
                        imgConsumentFaktur.Src = "../images/edit.gif"
                        imgConsumentFaktur.Alt = "Daftar Konsumen Faktur"
                    End If


                    If Not IsNothing(ViewState("Mode")) AndAlso CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.EditMode AndAlso objSPKDetail.SPKDetailCustomers.Count = 0 Then
                        imgConsumentFaktur.Src = "../images/detail.gif"
                        imgConsumentFaktur.Alt = "Tambah Konsumen Faktur"
                    End If

                End If
            End If

        End If

    End Sub

    Sub dtgPesananKendaraan_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        Select Case (e.CommandName)
            Case "UpdateProfile"
                UpdateProfileCommand(e)

            Case "AddFaktur"
                AddFaktur(e)
        End Select
    End Sub

    Private Sub BtnTutup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnTutup.Click
        'Response.Redirect("FrmSPKHeader.aspx?Id=" & objSPKHeader.ID & "&Mode=2&isBack=1&sessionName=" & viewstate.Item(Me._vstSPKHeader))
        If Not IsNothing(Request.QueryString("FromPage")) Then
            Response.Redirect("FrmSPKDaftar.aspx")
        Else
            If Not IsNothing(Request.QueryString("Mode")) Then
                Dim spkHeaderID As Integer = CType(Request.QueryString("Id"), Integer)
                Response.Redirect("FrmSPKHeader.aspx?Id=" & spkHeaderID & "&Mode=2&isBack=1")
            Else
                Response.Redirect("FrmSPKDaftar.aspx")
            End If
        End If

    End Sub
#End Region

#Region "Custom method"

    Private Sub UserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.Buat_spk_lihat_privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Surat Pesanan Kendaraan")
        End If
        'btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.buat_spk_simpan_privilege)

    End Sub

    Private Sub SearchSPKHeaderAndDetail()
        If Not Request.QueryString("Id") Is Nothing Then
            Dim spkHeaderID As Integer = CType(Request.QueryString("Id"), Integer)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKHeader), "ID", MatchType.Exact, CType(Request.QueryString("Id"), Integer)))
            Dim objSPKHeader1 As SPKHeader = New SPKHeaderFacade(User).Retrieve(criterias)(0)
            sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), objSPKHeader1)
        Else
            sessionHelper.SetSession(ViewState.Item(Me._vstSPKHeader), Nothing)
        End If
    End Sub
    Private Sub BindDataToPage()
        If Not IsNothing(sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))) Then
            objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
            BindHeaderToForm()
        End If
        BindDetailToGrid()
    End Sub

    Private Sub BindDetailToGrid()
        dtgPesananKendaraan.DataSource = objSPKHeader.SPKDetails
        dtgPesananKendaraan.DataBind()
        dtgPesananKendaraan.ShowFooter = False
        Dim strCalculation As String = CalculateUnitAndAmount(objSPKHeader)
        Dim arr() As String = strCalculation.Split(";")
        Dim unit As Integer = CType(arr(0).Trim, Integer)
        Dim amount As Double = CType(arr(1).Trim, Double)
        lblTotalUnit.Text = FormatNumber(unit, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " Unit"
        lblTotalHarga.Text = "Rp " & FormatNumber(amount, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Dim varIColl As Integer = dtgPesananKendaraan.Columns.Count - 1
        dtgPesananKendaraan.Columns(varIColl).Visible = True
    End Sub

    Private Function CalculateUnitAndAmount(ByVal obj As SPKHeader) As String
        Dim unit As Integer = 0
        Dim amount As Decimal = 0
        For Each item As SPKDetail In obj.SPKDetails
            If item.Status = 0 Then ' 0 -> status aktif
                unit += item.Quantity
                amount += item.TotalAmount
            End If
        Next
        Return unit.ToString + ";" + amount.ToString
    End Function

    Private Sub BindHeaderToForm()
        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        objUser = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not IsNothing(sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))) Then
            lblDealer.Text = objSPKHeader.Dealer.DealerCode & " / " & objSPKHeader.Dealer.SearchTerm1
            lblNoSPK.Text = objSPKHeader.SPKNumber
            If objSPKHeader.Status <> String.Empty Then
                lblStatus.Text = CType(objSPKHeader.Status, EnumStatusSPK.Status).ToString
            Else
                lblStatus.Text = EnumStatusSPK.Status.Awal.ToString
            End If

            lblPengajuan.Text = objSPKHeader.PlanInvoiceMonth.ToString & "-" & objSPKHeader.PlanInvoiceYear
            lblPengiriman.Text = objSPKHeader.PlanDeliveryMonth.ToString & "-" & objSPKHeader.PlanDeliveryYear.ToString
            lblNoSPKDealer.Text = objSPKHeader.DealerSPKNumber
            lblNoSPKReference.Text = objSPKHeader.SPKReferenceNumber
            lblSalesmanCode.Text = objSPKHeader.SalesmanHeader.SalesmanCode.ToString
            lblNamaSalesman.Text = objSPKHeader.SalesmanHeader.Name
            lblLevelSalesman.Text = objSPKHeader.SalesmanHeader.SalesmanLevel.Description
            lblJabatan.Text = objSPKHeader.SalesmanHeader.JobPosition.Description
            lblSPKOpenDate.Text = IIf(objSPKHeader.CreatedTime < New Date(1900, 1, 1), String.Empty, objSPKHeader.CreatedTime.ToString("dd/MM/yyyy"))
            lblDibuatOleh.Text = objUser.UserName
            If Not IsNothing(objSPKHeader.SPKCustomer) Then
                If Not IsNothing(objSPKHeader.SPKCustomer.SAPCustomer) Then
                    lblCampaignName.Text = objSPKHeader.SPKCustomer.SAPCustomer.CampaignName
                End If
            End If
        End If

    End Sub

    Private Sub UpdateProfileCommand(ByVal e As DataGridCommandEventArgs)

        objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        Dim objSPKDetail As SPKDetail = objSPKHeader.SPKDetails(e.Item.ItemIndex)

        Mode = ViewState("Mode")
        Dim strID As String = ""
        Dim cat As String = objSPKDetail.Category.CategoryCode

        'sessionHelper.SetSession("SPKDETAIL_PROFILE", objSPKDetail)

        'If Mode = enumMode.Mode.NewItemMode Then
        '    Response.Redirect("FrmSPKMasterProfile.aspx?Cat=" & cat & "Mode=" & CType(Mode, Integer) & "&spkHeader=" & viewstate.Item(Me._vstSPKHeader) & "&spkDetailIdx=" & e.Item.ItemIndex)
        'Else
        'Response.Redirect("FrmSPKMasterProfile.aspx?Cat=" & cat & "&Id=" & CType(Request.QueryString("Id"), Integer) & "&Mode=" & CType(Mode, Integer) & "&spkHeader=" & viewstate.Item(Me._vstSPKHeader) & "&spkDetailIdx=" & objSPKDetail.ID)
        Response.Redirect("FrmSPKMasterProfile.aspx?Cat=" & cat & "&Id=" & CType(Request.QueryString("Id"), Integer) & "&Mode=" & CType(Mode, Integer) & "&spkDetailIdx=" & objSPKDetail.ID)
        'End If

    End Sub



    Private Sub AddFaktur(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If


        Dim _SpkH As SPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
        If Not IsNothing(_SpkH) AndAlso Not IsNothing(_SpkH.SPKCustomer) Then
            objSPKHeader = sessionHelper.GetSession(ViewState.Item(Me._vstSPKHeader))
            'SalesmenInfo()
            Mode = ViewState("Mode")
            Dim varCustID As Integer = 0
            If Not IsNothing(objSPKHeader.SPKCustomer) AndAlso Not IsNothing(objSPKHeader.SPKCustomer.SAPCustomer) Then
                varCustID = objSPKHeader.SPKCustomer.SAPCustomer.ID
            End If
            If Not IsNothing(objSPKHeader) Then
                If objSPKHeader.SPKDetails.Count > 0 Then

                    If Mode = enumMode.Mode.NewItemMode Then
                        If varCustID > 0 Then
                            Response.Redirect("FrmSPKDetailCustomers.aspx?Mode=" & CType(Mode, Integer) & "&spkHeader=" & ViewState.Item(Me._vstSPKHeader) & "&CustId=" & varCustID.ToString() & "&spkDetailIdx=" & CType(objSPKHeader.SPKDetails(e.Item.ItemIndex), SPKDetail).ID.ToString() & "&SPKDetailID=" & CType(objSPKHeader.SPKDetails(e.Item.ItemIndex), SPKDetail).ID.ToString())
                        End If
                    Else
                        Response.Redirect("FrmSPKDetailCustomers.aspx?Id=" & CType(Request.QueryString("Id"), Integer) & "&Mode=" & CType(Mode, Integer) & "&spkHeader=" & ViewState.Item(Me._vstSPKHeader) & "&spkDetailIdx=" & CType(objSPKHeader.SPKDetails(e.Item.ItemIndex), SPKDetail).ID.ToString() & "&SPKDetailID=" & CType(objSPKHeader.SPKDetails(e.Item.ItemIndex), SPKDetail).ID.ToString() & "&Prof=1")
                    End If

                End If
            Else

            End If

        Else


        End If


    End Sub
#End Region



End Class
