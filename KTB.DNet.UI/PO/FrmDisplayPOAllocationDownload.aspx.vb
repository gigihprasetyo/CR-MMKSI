#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
#End Region

Public Class FrmDisplayPOAllocationDownload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgPOAllocation As System.Web.UI.WebControls.DataGrid

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
    Private arlPODetail As ArrayList
    Private objPODetail As PODetail
    Private arlGroupPODetail As ArrayList
    Private _ATPStok As Double = 0
    Private _TotalOrder As Double = 0
    Private _TotalAlokasi As Double = 0
    Private _OrderTidakTerpenuhi As Double = 0
#End Region

#Region "Custom Method"

    Private Sub BindToDataGrid()
        Dim ssHelper As SessionHelper = New SessionHelper
        arlPODetail = ssHelper.GetSession("POTODOWNLOWD")
        dtgPOAllocation.DataSource = arlPODetail
        If arlPODetail.Count > 0 Then
            dtgPOAllocation.DataBind()
        Else
            dtgPOAllocation.DataBind()
            MessageBox.Show("Data Tidak Ditemukan")
        End If
    End Sub

    Private Function CountTotal(ByVal objPODetail As PODetail, ByVal Type As String) As Integer
        Dim Total As Integer = 0
        For Each item As PODetail In arlPODetail
            If (item.ContractDetail.VechileColor.ID = objPODetail.ContractDetail.VechileColor.ID) Then
                If item.POHeader.ContractHeader.ProductionYear = objPODetail.POHeader.ContractHeader.ProductionYear Then
                    Select Case (Type)
                        Case "O/C Unit"
                            Total = Total + CInt(item.ContractDetail.SisaUnit)
                        Case "Propose"
                            Total = Total + CInt(item.ProposeQty)
                        Case "Alokasi"
                            Total = Total + CInt(item.AllocQty)
                        Case "Request"
                            Total = Total + CInt(item.ReqQty)

                    End Select
                End If
            End If
        Next
        Return Total
    End Function

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Page.EnableViewState = False
            SetDownload()
            BindToDataGrid()
        End If
    End Sub

    Private Sub SetDownload()
        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("Alokasi PO.xls").Append("""").ToString()
        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
    End Sub

    Sub dtgPOAllocation_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If Not (e.Item.ItemIndex = -1) Then
            objPODetail = CType(e.Item.DataItem, PODetail)
            e.Item.Cells(0).Text = objPODetail.ContractDetail.VechileColor.ID
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgPOAllocation.PageSize * dtgPOAllocation.CurrentPageIndex)).ToString
            e.Item.Cells(2).Text = objPODetail.ContractDetail.ContractHeader.Dealer.DealerCode
            e.Item.Cells(3).Text = objPODetail.ContractDetail.ContractHeader.Dealer.SearchTerm1

            e.Item.Cells(4).Text = objPODetail.POHeader.PONumber
            e.Item.Cells(5).Text = objPODetail.POHeader.DealerPONumber
            e.Item.Cells(6).Text = objPODetail.POHeader.CreatedTime.ToString("dd/MM/yyyy")
            e.Item.Cells(7).Text = objPODetail.POHeader.ReqAllocationDateTime
            e.Item.Cells(8).Text = objPODetail.POHeader.ContractHeader.Category.CategoryCode
            If objPODetail.POHeader.TermOfPayment.TermOfPaymentValue = 0 Then
                e.Item.Cells(9).Text = "COD"
            Else
                e.Item.Cells(9).Text = objPODetail.POHeader.TermOfPayment.TermOfPaymentCode
            End If
            e.Item.Cells(10).Text = objPODetail.ContractDetail.VechileColor.VechileType.VechileTypeCode
            e.Item.Cells(11).Text = objPODetail.ContractDetail.VechileColor.ColorCode
            e.Item.Cells(12).Text = objPODetail.ContractDetail.VechileColor.MaterialDescription
            e.Item.Cells(13).Text = objPODetail.ContractDetail.ContractHeader.ProductionYear
            e.Item.Cells(14).Text = CType(CInt(objPODetail.POHeader.POType), LookUp.EnumJenisOrder).ToString
            e.Item.Cells(15).Text = objPODetail.ContractDetail.ContractHeader.ProjectName
            e.Item.Cells(16).Text = objPODetail.ProposeQty
            e.Item.Cells(17).Text = objPODetail.ReqQty
            e.Item.Cells(18).Text = objPODetail.AllocQty
            Dim stockATP As Integer = objPODetail.StokATP
            e.Item.Cells(19).Text = stockATP
            e.Item.Cells(20).Text = stockATP - CountTotal(objPODetail, "Alokasi")
            e.Item.Cells(21).Text = objPODetail.ContractDetail.SisaUnit
        End If

    End Sub



    Private Function CountTotalUnit(ByVal pODetail As PODetail) As Integer
        'Dim arrListPPQty As ArrayList
        'Dim total As Integer
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "MaterialNumber", MatchType.Exact, pODetail.ContractDetail.VechileColor.MaterialNumber))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeMonth", MatchType.Exact, DateTime.Now.Month))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeYear", MatchType.Exact, DateTime.Now.Year))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "ProductionYear", MatchType.Exact, pODetail.POHeader.ContractHeader.ProductionYear))
        'If (pODetail.POHeader.POType = LookUp.EnumJenisOrder.Harian) Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", MatchType.Exact, DateTime.Now.Day))
        '    arrListPPQty = New PPQtyFacade(User).Retrieve(criterias)
        '    For Each item As PPQty In arrListPPQty
        '        total = total + item.AllocationQty
        '    Next
        'Else
        '    Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", AggregateType.Max)
        '    Dim MaxTgl As Integer = New PPQtyFacade(User).RetrieveScalar(criterias, agg)
        '    If MaxTgl <> 0 Then
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", MatchType.Exact, MaxTgl))
        '        arrListPPQty = New PPQtyFacade(User).Retrieve(criterias)
        '        For Each item As PPQty In arrListPPQty
        '            total = total + item.AllocationQty
        '        Next
        '        Dim criterias1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, pODetail.ContractDetail.VechileColor.MaterialNumber))
        '        criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
        '        criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReleaseDate", MatchType.Exact, MaxTgl))
        '        criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReleaseMonth", MatchType.Exact, DateTime.Now.Month))
        '        criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReleaseYear", MatchType.Exact, DateTime.Now.Year))
        '        Dim arrListPODetail = New PODetailFacade(User).Retrieve(criterias1)
        '        For Each item As pODetail In arrListPODetail
        '            total = total - item.AllocQty
        '        Next
        '        'ATP MINUS
        '        'if total < 0 then total = 0
        '    Else
        '        Return 0
        '    End If
        'End If
        'Return total
    End Function





#End Region

   
End Class