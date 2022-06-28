#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Utility
#End Region

Public Class DetailRemainPKProcess
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblTotalUnitValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalUnit As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents lblRencanaPenebusan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalPesananValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunPerakitanAtauImport As System.Web.UI.WebControls.Label
    Protected WithEvents lblKotaValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenisPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategori As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPKValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPK As System.Web.UI.WebControls.Label
    Protected WithEvents dtgPesananKendaraan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblError As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealerValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPesananValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategoriValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenisPesananValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunPerakitanValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblRencanaPenebusanValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents lboxPKNumber As System.Web.UI.WebControls.ListBox
    Protected WithEvents btnKembali As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents HideField As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents Hidden1 As System.Web.UI.HtmlControls.HtmlInputHidden


    ' Protected WithEvents btnKembali As System.Web.UI.WebControls.Button

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
    Private objPKHeader As PKHeader
    Private objPKDetail As PKDetail
    Private sessionHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub BindDetailToGrid()
        dtgPesananKendaraan.DataSource = objPKHeader.PKDetails
        dtgPesananKendaraan.DataBind()
    End Sub

    Private Sub GetPKHeader()
        Dim PKid As String = Request.QueryString("id")
        objPKHeader = New PKHeaderFacade(User).Retrieve(CInt(PKid))
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "HeadPKNumber", MatchType.Exact, objPKHeader.ID))
        Dim arlPK As ArrayList = New PKHeaderFacade(User).Retrieve(criterias)
        If arlPK.Count > 0 Then
            btnSimpan.Enabled = False
        End If
    End Sub

    Private Sub BindHeaderToForm()
        lblKodeDealerValue.Text = objPKHeader.Dealer.DealerCode
        lblNomorPKValue.Text = objPKHeader.PKNumber
        lblStatusValue.Text = CType(objPKHeader.PKStatus, enumStatusPK.Status).ToString
        lblKategoriValue.Text = objPKHeader.Category.CategoryCode
        lblJenisPesananValue.Text = CType(objPKHeader.OrderType, enumOrderType.OrderType).ToString
        lblTahunPerakitanValue.Text = objPKHeader.ProductionYear
        Dim Month As enumMonth.Month
        Month = CInt(objPKHeader.RequestPeriodeMonth - 1)
        Dim str As String = Month.ToString & " " & objPKHeader.RequestPeriodeYear.ToString
        lblRencanaPenebusanValue.Text = str
        lblNomorPesananValue.Text = objPKHeader.DealerPKNumber
        'If (objPKHeader.HeadPKNumber <> 0) Then
        '    lblHeadPKNumberValue.Text = New PKHeaderFacade(User).Retrieve(objPKHeader.HeadPKNumber).PKNumber
        'Else
        '    lblHeadPKNumberValue.Text = String.Empty
        'End If
        lblTanggalPesananValue.Text = Format(objPKHeader.PKDate, "dd/MM/yyyy")
        lblNamaDealerValue.Text = objPKHeader.Dealer.DealerName
        lblKotaValue.Text = objPKHeader.Dealer.City.CityName
        Dim TotalUnit As Integer
        For Each item As PKDetail In objPKHeader.PKDetails
            TotalUnit = TotalUnit + item.TargetQty
        Next
        lblTotalUnitValue.Text = TotalUnit.ToString

    End Sub

    Private Sub GeneratePK()
        For i As Integer = 1 To 6
            Dim Total As Integer = 0
            For j As Integer = 0 To dtgPesananKendaraan.Items.Count - 1
                Dim txt As TextBox = dtgPesananKendaraan.Items(j).FindControl("TextBox" & i.ToString)
                If txt.Text = String.Empty Then
                    txt.Text = "0"
                End If
                Total = Total + CInt(txt.Text)
            Next
            If Not (Total = 0) Then
                Dim NewObjPKheader As PKHeader = GetNewObjPKHeader()
                NewObjPKheader.PKStatus = enumStatusPK.Status.Konfirmasi
                Dim int = CInt(objPKHeader.RequestPeriodeMonth) + i
                If int > 12 Then
                    NewObjPKheader.RequestPeriodeMonth = int - 12
                    NewObjPKheader.RequestPeriodeYear = CInt(objPKHeader.RequestPeriodeYear) + 1
                Else
                    NewObjPKheader.RequestPeriodeMonth = int
                End If
                For j As Integer = 0 To dtgPesananKendaraan.Items.Count - 1
                    Dim txt As TextBox = dtgPesananKendaraan.Items(j).FindControl("TextBox" & i.ToString)
                    If txt.Text <> String.Empty And txt.Text <> "0" Then
                        Dim newObjPKDetail As PKDetail = objPKHeader.PKDetails(j)
                        newObjPKDetail.TargetQty = CInt(txt.Text)
                        newObjPKDetail.ResponseQty = CInt(txt.Text)
                        'newObjPKDetail.ResponseSalesSurcharge = CType(objPKHeader.PKDetails(j), PKDetail).ResponseSalesSurcharge
                        NewObjPKheader.PKDetails.Add(newObjPKDetail)
                    End If
                Next
                Dim returnValue As Integer = New PKHeaderFacade(User).Insert(NewObjPKheader)
            End If
        Next
        MessageBox.Show("PK Berhasil Dibuat")
    End Sub

    Private Function GetNewObjPKHeader() As PKHeader
        Dim NewObjPKHeader As New PKHeader
        NewObjPKHeader.Category = objPKHeader.Category
        NewObjPKHeader.Dealer = objPKHeader.Dealer
        NewObjPKHeader.DealerPKNumber = objPKHeader.DealerPKNumber
        NewObjPKHeader.Description = objPKHeader.Description
        NewObjPKHeader.HeadPKNumber = objPKHeader.ID
        NewObjPKHeader.KTBResponse = objPKHeader.KTBResponse
        NewObjPKHeader.OrderType = objPKHeader.OrderType
        NewObjPKHeader.PKDate = objPKHeader.PKDate
        NewObjPKHeader.PKStatus = enumStatusPK.Status.Rilis
        NewObjPKHeader.PKType = objPKHeader.PKType
        NewObjPKHeader.PricingPeriodeMonth = objPKHeader.PricingPeriodeMonth
        NewObjPKHeader.PricingPeriodeYear = objPKHeader.PricingPeriodeYear
        NewObjPKHeader.ProductionYear = objPKHeader.ProductionYear
        NewObjPKHeader.ProjectDetail = objPKHeader.ProjectDetail
        NewObjPKHeader.ProjectName = objPKHeader.ProjectName
        NewObjPKHeader.Purpose = objPKHeader.Purpose
        NewObjPKHeader.RequestPeriodeMonth = objPKHeader.RequestPeriodeMonth
        NewObjPKHeader.RequestPeriodeYear = objPKHeader.RequestPeriodeYear
        NewObjPKHeader.FreePPh22Indicator = objPKHeader.FreePPh22Indicator
        NewObjPKHeader.SPLNumber = objPKHeader.SPLNumber
        NewObjPKHeader.RowStatus = objPKHeader.RowStatus
        Return NewObjPKHeader
    End Function

    Private Function ValidateData() As Boolean
        Dim ReturnValue As Boolean = True
        For i As Integer = 0 To dtgPesananKendaraan.Items.Count - 1
            Dim Total As Integer = 0
            For j As Integer = 1 To 6
                Dim txt As TextBox = dtgPesananKendaraan.Items(i).FindControl("TextBox" & j.ToString)
                If txt.Text = String.Empty Then
                    txt.Text = "0"
                End If
                Total = Total + CInt(txt.Text)
            Next
            If (Total > CInt(dtgPesananKendaraan.Items(i).Cells(7).Text)) Then
                dtgPesananKendaraan.Items(i).Cells(8).Text = "*"
                dtgPesananKendaraan.Items(i).Cells(8).ForeColor = System.Drawing.Color.Red
                lblError.Text = "Error: Jumlah Unit yang dialokasi tidak sama dengan Sisa Unit yang belum dialokasi"
                ReturnValue = False

            Else
                dtgPesananKendaraan.Items(i).Cells(8).Text = ""
            End If
        Next
        Return ReturnValue
    End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            GetPKHeader()
            BindHeaderToForm()
            BindDetailToGrid()
            sessionHelper.SetSession("PK", objPKHeader)
        End If
        btnSimpan.Attributes.Add("OnClick", "return confirm('Yakin PK ini Diproses?');")
    End Sub

    Sub dtgPesananKendaraan_ItemDataBound(ByVal Sender As Object, ByVal E As DataGridItemEventArgs)
        If Not (objPKHeader.PKDetails.Count = 0 Or E.Item.ItemIndex = -1) Then
            objPKDetail = objPKHeader.PKDetails(E.Item.ItemIndex)
            If (objPKDetail.VehicleColorCode = "ZZZZ") Then
                E.Item.Cells(2).Text = objPKDetail.VehicleColorName
            Else
                E.Item.Cells(2).Text = objPKDetail.VechileColor.MaterialDescription.ToString
            End If
            E.Item.Cells(7).Text = (CType(E.Item.Cells(5).Text, Long) - CType(E.Item.Cells(6).Text, Long)).ToString
            'If E.Item.Cells(7).Text = "0" Then
            'btnSimpan.Enabled = False
            'End If
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        objPKHeader = sessionHelper.GetSession("PK")
        Try
            If (ValidateData()) Then
                GeneratePK()
                btnSimpan.Enabled = False
                Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "HeadPKNumber", MatchType.Exact, objPKHeader.ID))
                Dim arlPK As ArrayList = New PKHeaderFacade(User).Retrieve(criterias)
                lboxPKNumber.DataSource = arlPK
                lboxPKNumber.DataTextField = "PKNumber"
                lboxPKNumber.DataValueField = "ID"
                lboxPKNumber.DataBind()
                Label5.Visible = True
                lboxPKNumber.Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)

        End Try
    End Sub

#End Region

    'Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
    '    objPKHeader = sessionHelper.GetSession("PK")
    '    Response.Redirect("../PK/PesananKendaraanKhusus.aspx?PKNumber=" & objPKHeader.PKNumber & "&DealerCode=" & objPKHeader.Dealer.DealerCode & "&Src=search")
    'End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not sessionHelper.GetSession("PrevPage") Is Nothing AndAlso Not sessionHelper.GetSession("PrevPage") = String.Empty Then
            Response.Redirect(sessionHelper.GetSession("PrevPage").ToString())
        Else
            Response.Redirect("../login.aspx")
        End If

    End Sub
End Class