#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.MDP

Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmEntryPOAllocation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgEntryPOAllocation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTipeWarna As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipeWarnaValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunPerakitan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunPerakitanValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipeValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategori As System.Web.UI.WebControls.Label
    Protected WithEvents lblKategoriValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnHitung As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotalUnit As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalUnitValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaUnit As System.Web.UI.WebControls.Label
    Protected WithEvents lblSisaUnitValue As System.Web.UI.WebControls.Label
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents lblPermintaanKirim As System.Web.UI.WebControls.Label
    Protected WithEvents lblPermintaanKirimAwal As System.Web.UI.WebControls.Label
    Protected WithEvents lblPermintaanKirimAkhir As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenisOrder As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenisOrderValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Protected WithEvents lblMaterialDescription As System.Web.UI.WebControls.Label
    Protected WithEvents lblMaterialDescriptionValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnAlokasi As System.Web.UI.WebControls.Button
    Protected WithEvents btnKonversi As System.Web.UI.WebControls.Button
    Protected WithEvents lblA As System.Web.UI.WebControls.Label
    Protected WithEvents lblB As System.Web.UI.WebControls.Label
    Protected WithEvents lblC As System.Web.UI.WebControls.Label
    Protected WithEvents lblD As System.Web.UI.WebControls.Label
    Protected WithEvents lblAvCeiling As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalPO As System.Web.UI.WebControls.Label
    Protected WithEvents hdnValConfirm As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtIsSaving As System.Web.UI.WebControls.TextBox

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
    Private objPOHeader As POHeader
    Private objPODetail As PODetail
    Private objVechileColor As VechileColor
    Private arlListPO As ArrayList
    Private PoId As String
    Private sessionHelper As New SessionHelper
    Private FlagATP As String

#End Region

#Region "Private Variables"
    Private sessHelp As SessionHelper = New SessionHelper
#End Region

#Region "Custom Method"
    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ContractDetail.VechileColor.MaterialNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub dtgEntryPOAllocation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEntryPOAllocation.SortCommand
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

        dtgEntryPOAllocation.SelectedIndex = -1
        dtgEntryPOAllocation.CurrentPageIndex = 0
        BindToGrid()
        BindDataToGrid()
    End Sub

    Private Sub GetPODetail()
        'PoId = Request.QueryString("id")
        lblPermintaanKirimAwal.Text = Request.QueryString("start")
        lblPermintaanKirimAkhir.Text = Request.QueryString("end")
        lblJenisOrderValue.Text = CType(Request.QueryString("orderType"), Lookup.EnumJenisOrder).ToString
        If Request.QueryString("orderType") = LookUp.EnumJenisOrder.Tambahan Then
            btnAlokasi.Visible = True
        Else
            btnAlokasi.Visible = False
        End If
        BindToGrid()
        'Session("arlListPO") = arlListPO
    End Sub

    Private Sub BindToGrid()
        'PoId = Request.QueryString("id")
        PoId = sessHelp.GetSession("FrmEntryPOAllocation.POID")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.Exact, CType(enumStatusPO.Status.Konfirmasi, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "ContractDetail.VechileColor.ID", MatchType.Exact, PoId))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.POType", CInt(ViewState("orderType"))))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ContractHeader.ProductionYear", CInt(ViewState("productionYear"))))
        Dim TanggalAwal As New DateTime(CInt(CType(lblPermintaanKirimAwal.Text, DateTime).Year), CInt(CType(lblPermintaanKirimAwal.Text, DateTime).Month), CInt(CType(lblPermintaanKirimAwal.Text, DateTime).Day), 0, 0, 0)
        Dim TanggalAkhir As New DateTime(CInt(CType(lblPermintaanKirimAkhir.Text, DateTime).Year), CInt(CType(lblPermintaanKirimAkhir.Text, DateTime).Month), CInt(CType(lblPermintaanKirimAkhir.Text, DateTime).Day), 0, 0, 0)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ReqAllocationDateTime", MatchType.Lesser, Format(TanggalAkhir.AddDays(1), "yyyy-MM-dd HH:mm:ss")))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(PODetail), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))
        arlListPO = New PODetailFacade(User).Retrieve(criterias, sortColl)
        sessionHelper.SetSession("arlListPO", arlListPO)
    End Sub
    Private Sub StoreInformation()
        ViewState("start") = Request.QueryString("start")
        ViewState("end") = Request.QueryString("end")
        ViewState("productionYear") = Request.QueryString("productionYear")
        ViewState("orderType") = Request.QueryString("orderType")
        ViewState("kategori") = Request.QueryString("kategori")
        ViewState("Tipe") = Request.QueryString("Tipe")
        ViewState("MaterialNumber") = Request.QueryString("MaterialNumber")
    End Sub

    Private Sub BindDataToGrid()
        dtgEntryPOAllocation.DataSource = arlListPO
        dtgEntryPOAllocation.DataBind()
        'Start Remaining Module
        If sessionHelper.GetSession("FrmEntryPOAllocation.IsOpenedByBack") = "1" Then
            UpdateDtgWithLastPosition()
            sessionHelper.SetSession("FrmEntryPOAllocation.IsOpenedByBack", "0")
        End If
        'End Remaining Module
        CountSisaAlokasi()
    End Sub

    Private Sub BindToHeaderToForm()
        If arlListPO.Count > 0 Then
            Dim objPODetail As PODetail = CType(arlListPO(0), PODetail)
            If Not (objPODetail Is Nothing) Then
                lblTahunPerakitanValue.Text = objPODetail.ContractDetail.ContractHeader.ProductionYear
                lblKategoriValue.Text = objPODetail.ContractDetail.ContractHeader.Category.CategoryCode
                lblTipeValue.Text = objPODetail.ContractDetail.VechileColor.VechileType.VechileTypeCode
                lblTipeWarnaValue.Text = objPODetail.ContractDetail.VechileColor.MaterialNumber
                lblMaterialDescriptionValue.Text = objPODetail.ContractDetail.VechileColor.MaterialDescription
                CountTotalUnit()
            End If

        Else
            btnKembali_Click(Nothing, Nothing)
        End If
    End Sub

    'Private Function GetPPQty() As PPQty
    '    Dim cP As New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    Dim aPs As ArrayList
    '    Dim oPFac As New PPQtyFacade(User)
    '    Dim oPD As PODetail

    '    arlListPO = sessionHelper.GetSession("arlListPO")
    '    If arlListPO.Count > 0 Then
    '        oPD = CType(arlListPO(0), PODetail)
    '        cP.opAnd(New Criteria(GetType(PPQty), "MaterialNumber", MatchType.Exact, oPD.ContractDetail.VechileColor.MaterialNumber))
    '        cP.opAnd(New Criteria(GetType(PPQty), "", MatchType.Exact, ""))
    '        cP.opAnd(New Criteria(GetType(PPQty), "", MatchType.Exact, ""))
    '        cP.opAnd(New Criteria(GetType(PPQty), "", MatchType.Exact, ""))
    '        cP.opAnd(New Criteria(GetType(PPQty), "", MatchType.Exact, ""))
    '        cP.opAnd(New Criteria(GetType(PPQty), "", MatchType.Exact, ""))
    '    Else
    '    End If
    'End Function

    Private Function TotalAlokasiAwal() As Integer
        Dim Total As Integer = 0
        For Each oPOD As PODetail In Me.arlListPO
            Total += oPOD.AllocQty
        Next
        Return Total
    End Function

    Private Sub CountTotalUnit()
        Dim arrListPPQty As ArrayList
        Dim total As Integer
        Dim TotalATP As Integer = 0

        arlListPO = sessionHelper.GetSession("arlListPO")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "MaterialNumber", MatchType.Exact, CType(arlListPO(0), PODetail).ContractDetail.VechileColor.MaterialNumber))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeMonth", MatchType.Exact, DateTime.Now.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeYear", MatchType.Exact, DateTime.Now.Year))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "ProductionYear", MatchType.Exact, CType(arlListPO(0), PODetail).POHeader.ContractHeader.ProductionYear))
        If (lblJenisOrderValue.Text = LookUp.EnumJenisOrder.Harian.ToString) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", MatchType.Exact, DateTime.Now.Day))
            arrListPPQty = New PPQtyFacade(User).Retrieve(criterias)
            If arrListPPQty.Count > 0 Then
                TotalATP = CType(arrListPPQty(0), PPQty).TotalSisa() + Me.TotalAlokasiAwal() ' .TotalATP()
            End If
            For Each item As PPQty In arrListPPQty
                total = total + item.AllocationQty
            Next
        Else
            Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", AggregateType.Max)
            Dim MaxTgl As Integer = New PPQtyFacade(User).RetrieveScalar(criterias, agg)
            If MaxTgl <> 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", MatchType.Exact, MaxTgl))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeMonth", MatchType.Exact, DateTime.Now.Month))
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeYear", MatchType.Exact, DateTime.Now.Year))
                arrListPPQty = New PPQtyFacade(User).Retrieve(criterias)
                If arrListPPQty.Count > 0 Then
                    TotalATP = CType(arrListPPQty(0), PPQty).TotalSisa() + Me.TotalAlokasiAwal()
                End If
                For Each item As PPQty In arrListPPQty
                    total = total + item.AllocationQty
                Next
                Dim criterias1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, CType(arlListPO(0), PODetail).ContractDetail.VechileColor.MaterialNumber))
                '   Modified by Ikhsan, 20081022
                '   Requested by Yurike/Andra/Doni KTB
                '   to anticipate the minus value of ATP
                'criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
                FlagATP = CType(sessionHelper.GetSession("ATPFlag"), String)
                'FlagATP = Request.QueryString("ATPFlag")
                If FlagATP.ToUpper = "TRUE" Then
                    criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.Exact, CType(enumStatusPK.Status.Rilis, Integer)))
                Else
                    criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
                End If
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReleaseDate", MatchType.Exact, MaxTgl))
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReleaseMonth", MatchType.Exact, DateTime.Now.Month))
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReleaseYear", MatchType.Exact, DateTime.Now.Year))
                criterias1.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ContractHeader.ProductionYear", MatchType.Exact, CType(arlListPO(0), PODetail).POHeader.ContractHeader.ProductionYear))
                Dim arrListPODetail = New PODetailFacade(User).Retrieve(criterias1)
                For Each item As PODetail In arrListPODetail
                    total = total - item.AllocQty
                Next
            Else
                btnSimpan.Enabled = False
                btnHitung.Enabled = False
                'MessageBox.Show("Propose Quantity tidak ditemukan")
                MessageBox.Show(SR.DataNotFound("Propose Quantity"))
            End If
        End If
        'If total < 0 Then
        '    lblTotalUnitValue.Text = "0"
        'Else
        lblTotalUnitValue.Text = TotalATP ' total
        'End If

    End Sub

    Private Sub CountSisaAlokasi()
        arlListPO = sessionHelper.GetSession("arlListPO")
        Dim total As Integer
        For Each item As DataGridItem In dtgEntryPOAllocation.Items
            Dim txtBox As TextBox = item.FindControl("txtAllocation")
            total = total + CInt(txtBox.Text)
        Next
        'MINUS ATP
        If (lblJenisOrderValue.Text = LookUp.EnumJenisOrder.Harian.ToString) And arlListPO.Count > 0 Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "ContractDetail.VechileColor.MaterialNumber", MatchType.Exact, CType(arlListPO(0), PODetail).ContractDetail.VechileColor.MaterialNumber))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.Status", MatchType.Exact, CType(enumStatusPO.Status.Rilis, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReleaseDate", MatchType.Exact, DateTime.Now.Day))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReleaseMonth", MatchType.Exact, DateTime.Now.Month))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODetail), "POHeader.ReleaseYear", MatchType.Exact, DateTime.Now.Year))
            Dim arrListPODetail = New PODetailFacade(User).Retrieve(criterias)
            For Each item As PODetail In arrListPODetail
                total = total + item.AllocQty
            Next
        End If


        'If (CInt(lblTotalUnitValue.Text) - total) < 0 Then
        '    lblSisaUnitValue.Text = "0"
        'Else
        lblSisaUnitValue.Text = CInt(lblTotalUnitValue.Text) - total
        'End If

    End Sub

    Private Function IsAvailabeForFactoring(ByVal objPOH As POHeader) As Boolean
        Dim TotInDB As Decimal = New v_POTotalDetailFacade(User).Retrieve(objPOH.ID).TotalDetail
        Dim TotInThisTrans As Decimal = 0 ' Me.GetTotalPOInThisTrans()
        Dim IsAfterSaving As Boolean = True 'always
        Dim AvCeiling As Decimal = 0
        Dim IsAvailable As Boolean = False

        IsAvailable = CommonFunction.IsEnoughForFactoring(objPOH.ContractHeader.Category.ProductCategory, objPOH.ID, objPOH.TotalPODetail(), objPOH.Dealer.CreditAccount, IsAfterSaving, AvCeiling)
        lblAvCeiling.Text = AvCeiling
        lblTotalPO.Text = objPOH.TotalPODetail()
        Return IsAvailable
    End Function
    Private Function IsQualifiedPO(ByVal objPOH As POHeader, ByVal StartDate As Date, ByVal EndDate As Date) As Boolean
        If objPOH.IsFactoring = 1 Then
            Return IsAvailabeForFactoring(objPOH)
            Exit Function
        End If
        Dim objCMFac As CreditMasterFacade = New CreditMasterFacade(User)
        Dim objCM As CreditMaster
        Dim AvCeiling As Decimal
        Dim TotalPO As Decimal = GetTotalPO(objPOH)
        Dim PaymentType As Short
        Dim objSCM As sp_CreditMaster
        Dim arlTemp As ArrayList = New ArrayList
        Dim TotalLiquefied As Decimal = 0
        Dim TotalAcceleratedGyro As Decimal = 0


        PaymentType = objPOH.TermOfPayment.PaymentType
        'Credit Ceiling
        objSCM = GetCeilingCredit(objPOH.ContractHeader.Category.ProductCategory, objPOH.ContractHeader.Dealer.CreditAccount, PaymentType)
        If objSCM Is Nothing Then
            Return False
        Else
            If objSCM.ID <= 0 Then
                Return False
            End If
        End If
        AvCeiling = (objSCM.Plafon - objSCM.OutStanding)
        lblA.Text = AvCeiling
        If PaymentType = enumPaymentType.PaymentType.TOP Then
            'Proposed PO
            AvCeiling = AvCeiling - objSCM.ProposedPO
            lblB.Text = objSCM.ProposedPO
            'Liquefied and Accelerated Gyro
            objCM = objCMFac.Retrieve(objPOH.ContractHeader.Category.ProductCategory, objPOH.ContractHeader.Dealer.CreditAccount, PaymentType)
            TotalLiquefied = 0
            TotalAcceleratedGyro = 0
            For Each objDealer As Dealer In objCM.Dealers
                arlTemp = GetDealerPO(objDealer, PaymentType)
                TotalLiquefied += arlTemp(0)
                TotalAcceleratedGyro += arlTemp(1)
            Next

            lblC.Text = TotalLiquefied
            lblD.Text = TotalAcceleratedGyro
            AvCeiling = AvCeiling + TotalLiquefied + TotalAcceleratedGyro
        ElseIf PaymentType = enumPaymentType.PaymentType.COD Then
            'AvCeiling = GetRemainCeiling(AvCeiling, objPOH.ContractHeader.Dealer.CreditAccount, PaymentType, DateSerial(Now.Year, Now.Month, Now.Day), CDate(lblPermintaanKirimAkhir.Text))
            AvCeiling = GetRemainCeiling(AvCeiling, objPOH.ContractHeader.Dealer.CreditAccount, PaymentType, StartDate, EndDate)
        End If
        lblAvCeiling.Text = AvCeiling
        lblTotalPO.Text = TotalPO
        If 0 > AvCeiling Then 'If TotalPO > AvCeiling Then
            'MessageBox.Show("Total PO melebihi Ceiling yang tersedia")
            Return False
        Else
            Return True
        End If

    End Function

#Region "RemainCeilingOld"

    Private Function GetRemainCeilingOld(ByVal AvCeiling As Decimal, ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal
        Dim RemCeilH As Decimal = 0
        Dim RemCeilHPlus1 As Decimal = 0

        RemCeilH = AvCeiling - GetReqPO(CreditAccount, PaymentType, StartDate, EndDate) + GetPOCair(CreditAccount, PaymentType, StartDate, EndDate)
        RemCeilHPlus1 = AvCeiling - GetReqPO(CreditAccount, PaymentType, StartDate.AddDays(1), EndDate.AddDays(1)) + GetPOCair(CreditAccount, PaymentType, StartDate.AddDays(1), EndDate.AddDays(1))
        If RemCeilH < RemCeilHPlus1 Then
            Return RemCeilH
        Else
            Return RemCeilHPlus1
        End If

    End Function
    Private Function GetReqPOOld(ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal

        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As New ArrayList
        Dim Sql As String = ""
        Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim Total As Decimal = 0

        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, StartDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, EndDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and RejectStatus=0 and IsReversed=1 and IsCleared<>1 and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & ")"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            Total += GetTotalPODetail(objPOH)
        Next
        Return Total
    End Function
    Private Function GetTotalPODetailOld(ByRef objPOH As POHeader) As Decimal
        Dim Total As Decimal = 0
        Total = objPOH.TotalPODetail()
        'For Each objPOD As PODetail In objPOH.PODetails
        '    If objPOH.Status = 0 Or objPOH.Status = 2 Then
        '        Total = Total + (objPOD.ReqQty * objPOD.Price)
        '    ElseIf objPOH.Status = 4 Or objPOH.Status = 6 Or objPOH.Status = 8 Then
        '        Total = Total + (objPOD.AllocQty * objPOD.Price)
        '    End If
        'Next
        Return Total
    End Function
    Private Function GetPOCairOld(ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As ArrayList
        Dim crtPOH As CriteriaComposite
        Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim arlDP As ArrayList
        Dim crtDP As CriteriaComposite
        Dim Sql As String = ""
        Dim Total As Decimal = 0

        'have gyro
        'Accelerated
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, "0"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.GreaterOrEqual, StartDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.LesserOrEqual, EndDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        arlDP = objDPFac.Retrieve(crtDP)
        For Each objDP As DailyPayment In arlDP
            Total += objDP.Amount
        Next
        'Not Accelerated
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, "0"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, StartDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.LesserOrEqual, EndDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        arlDP = objDPFac.Retrieve(crtDP)
        For Each objDP As DailyPayment In arlDP
            Total += objDP.Amount
        Next

        'doesn't have gyro
        StartDate = AddWorkingDay(StartDate, -2, True)
        EndDate = AddWorkingDay(EndDate, -2, True)
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and RejectStatus=0 and IsReversed<>1 and IsCleared<>1 and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & ")"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, StartDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, EndDate))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            Total += GetTotalPODetail(objPOH)
        Next

        Return Total

    End Function

#End Region


#Region "RemainCeiling"

    Private Function GetRemainCeiling(ByVal AvCeiling As Decimal, ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal
        Dim RemCeilH As Decimal = 0
        Dim RemCeilHPlus1 As Decimal = 0
        Dim i As Integer
        Dim TotReq As Decimal = 0
        Dim TotCair As Decimal = 0
        Dim FocusedDate As Date

        'Start  :Get AvCeiling from Looping ' starting from ReportDate to ReqDelDate-1
        '3-5 = 2-1
        '0,1,2=3,4

        '3-3=0 -1 = 
        '
        For i = 0 To DateDiff(DateInterval.Day, StartDate, EndDate) - 1
            If i = 0 Then
                FocusedDate = StartDate
                If FocusedDate = EndDate Then Exit For
                'TotReq = GetReqPO(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                TotReq = GetReqPO(CreditAccount, PaymentType, FocusedDate, FocusedDate)
                TotCair = 0 'GetPOCair(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                AvCeiling = AvCeiling - TotReq + TotCair 'it's covered by SAP Application
            Else
                FocusedDate = AddWorkingDay(FocusedDate, 1)
                If FocusedDate = EndDate Then Exit For
                'TotReq = GetReqPO(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                'TotCair = GetPOCair(CreditAccount, PaymentType, StartDate.AddDays(i), StartDate.AddDays(i))
                TotReq = GetReqPO(CreditAccount, PaymentType, FocusedDate, FocusedDate)
                TotCair = GetPOCair(CreditAccount, PaymentType, FocusedDate, FocusedDate)

                AvCeiling = AvCeiling - TotReq + TotCair
            End If
        Next
        'End    :Get AvCeiling from Looping ' starting from ReportDate to ReqDelDate
        'Response.Write("AvCeilingFromDB=" & AvCeiling & "<br>")
        StartDate = EndDate
        Dim TotalA As Decimal = GetReqPO(CreditAccount, PaymentType, StartDate, EndDate)
        Dim TotalB As Decimal = 0 ' GetPOCair(CreditAccount, PaymentType, StartDate, EndDate)
        'lblAvCeilingFirst.Text = AvCeiling
        'lblA.Text = TotalA
        'lblC.Text = TotalB
        RemCeilH = AvCeiling - TotalA + TotalB
        'Response.Write("Date=" & StartDate & ":TotPO=" & TotalA & ":TotCair=" & TotalB & ":Ceiling=" & RemCeilH & "<br>")
        'TotalA = GetReqPO(CreditAccount, PaymentType, AddWorkingDay(startdate,1) StartDate.AddDays(1), EndDate.AddDays(1))
        TotalA = GetReqPO(CreditAccount, PaymentType, AddWorkingDay(StartDate, 1), AddWorkingDay(StartDate, 1))
        TotalB = GetPOCair(CreditAccount, PaymentType, AddWorkingDay(StartDate, 1), AddWorkingDay(StartDate, 1))
        'lblB.Text = TotalA
        'lblD.Text = TotalB
        'RemCeilHPlus1 = AvCeiling - TotalA + TotalB
        RemCeilHPlus1 = RemCeilH - TotalA + TotalB
        'Response.Write("Date=" & AddWorkingDay(StartDate, 1) & ":TotPO=" & TotalA & ":TotCair=" & TotalB & ":Ceiling=" & RemCeilHPlus1 & "<br>")
        If RemCeilH < RemCeilHPlus1 Then
            Return RemCeilH
        Else
            Return RemCeilHPlus1
        End If

    End Function
    Private Function GetReqPO(ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal

        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As New ArrayList
        Dim Sql As String = ""
        Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim Total As Decimal = 0

        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, StartDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, EndDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and RejectStatus=0 and IsReversed=1 and IsCleared<>1 and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & ")"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            Total += GetTotalPODetail(objPOH)
        Next
        Return Total
    End Function
    Private Function GetTotalPODetail(ByRef objPOH As POHeader) As Decimal
        Dim Total As Decimal = 0

        For Each objPOD As PODetail In objPOH.PODetails
            If objPOH.Status = 0 Or objPOH.Status = 2 Then
                Total = Total + (objPOD.ReqQty * objPOD.Price)
            ElseIf objPOH.Status = 4 Or objPOH.Status = 6 Or objPOH.Status = 8 Then
                Total = Total + (objPOD.AllocQty * objPOD.Price)
            End If
        Next
        Return Total
    End Function
    Private Function GetPOCair(ByVal CreditAccount As String, ByVal PaymentType As Integer, ByVal StartDate As Date, ByVal EndDate As Date) As Decimal
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As ArrayList
        Dim crtPOH As CriteriaComposite
        Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim arlDP As ArrayList
        Dim crtDP As CriteriaComposite
        Dim Sql As String = ""
        Dim Total As Decimal = 0

        'have gyro
        'Accelerated
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, "0"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.GreaterOrEqual, StartDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.LesserOrEqual, EndDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        arlDP = objDPFac.Retrieve(crtDP)
        For Each objDP As DailyPayment In arlDP
            Total += objDP.Amount
        Next
        'Not Accelerated
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, "0"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        'crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, StartDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.LesserOrEqual, EndDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        arlDP = objDPFac.Retrieve(crtDP)
        For Each objDP As DailyPayment In arlDP
            Total += objDP.Amount
        Next

        'doesn't have gyro
        StartDate = AddWorkingDay(StartDate, -2, True)
        EndDate = AddWorkingDay(EndDate, -2, True)
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and RejectStatus=0 and IsReversed<>1 and IsCleared<>1 and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & ")"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, CreditAccount))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, StartDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, EndDate))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            Total += GetTotalPODetail(objPOH)
        Next

        Return Total

    End Function

#End Region

    Private Function GetTotalPO(ByVal objPOH As POHeader) As Decimal
        Dim Total As Decimal = 0

        For Each objPOD As PODetail In objPOH.PODetails
            Total += objPOD.Price * GetPODetailNewAlloc(objPOD)
        Next
        Return Total

        Exit Function

        For Each objPOD As PODetail In objPOH.PODetails
            If objPOH.Status = 0 Or objPOH.Status = 2 Or objPOH.Status = 4 Then
                Total = Total + (objPOD.Price * objPOD.ReqQty)
            Else
                'Total = Total + (objPOD.Price * objPOD.AllocQty)
                Total = Total + (objPOD.Price * GetPODetailNewAlloc(objPOD))
            End If
        Next
        Return Total
    End Function

    Private Function GetPODetailNewAlloc(ByVal objPOD As PODetail) As Integer
        For Each di As DataGridItem In dtgEntryPOAllocation.Items
            Dim txtID As TextBox = di.FindControl("txtID")
            Dim chkItemChecked As CheckBox = di.FindControl("chkItemChecked")
            Dim txtAllocation As TextBox = di.FindControl("txtAllocation")

            If CType(txtID.Text, Integer) = objPOD.ID Then
                If chkItemChecked.Checked Then
                    If txtAllocation.Text.Trim = "" OrElse IsNumeric(txtAllocation.Text) = False Then
                        txtAllocation.Text = "0"
                    End If
                    Return CType(txtAllocation.Text, Integer)
                Else
                    Dim Total As Integer

                    If objPOD.POHeader.Status = enumStatusPO.Status.Konfirmasi Then
                        If objPOD.AllocQty > 0 Then
                            Total = objPOD.AllocQty
                        Else
                            Total = objPOD.ReqQty
                        End If
                    ElseIf objPOD.POHeader.Status = 0 Then
                        Total = objPOD.ReqQty
                    Else
                        Total = objPOD.AllocQty
                    End If
                    Return Total
                    'Return objPOD.AllocQty
                End If
            End If
        Next
    End Function

    Private Function GetCeilingCredit(ByVal PC As ProductCategory, ByVal CreditAccount As String, ByVal PaymentType As Short) As sp_CreditMaster
        Dim objSCMFac As sp_CreditMasterFacade = New sp_CreditMasterFacade(User)
        Dim arlSCM As ArrayList
        Dim objSCM As sp_CreditMaster
        Dim crtSCM As CriteriaComposite
        Dim ReportDate As Date = DateSerial(Now.Year, Now.Month, Now.Day)
        Dim ReqDelDate As Date = DateSerial(Now.Year, Now.Month, Now.Day)

        crtSCM = New CriteriaComposite(New Criteria(GetType(sp_CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "CreditAccount", MatchType.Exact, CreditAccount))
        crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "PaymentType", MatchType.Exact, PaymentType))
        arlSCM = objSCMFac.RetrieveFromSP(PC, ReportDate, ReqDelDate, crtSCM)
        If arlSCM.Count > 0 Then
            Return CType(arlSCM(0), sp_CreditMaster)
        Else
            Return Nothing
        End If
    End Function

    Private Function GetDealerPO(ByVal objDealer As Dealer, ByVal PaymentType As Short, Optional ByVal POIDWantToCheck As Integer = 0, Optional ByRef IsPOIncluded As Boolean = False) As ArrayList
        Dim TotalLiquefied As Decimal = 0
        Dim TotalAcceleratedGyro As Decimal = 0
        Dim arlResult As ArrayList = New ArrayList
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim crtPOH As CriteriaComposite
        Dim arlPOH As ArrayList = New ArrayList
        Dim EffectiveDate As Date
        Dim tmpDate As Date
        Dim nTOPDays As Integer
        Dim ReportDate As Date = DateSerial(Now.Year, Now.Month, Now.Day)
        Dim ReqDelDate As Date = DateSerial(Now.Year, Now.Month, Now.Day)

        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, objDealer.ID))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(ReportDate, "yyyy/MM/dd")))
        arlPOH = objPOHFac.Retrieve(crtPOH)

        For Each objPOH As POHeader In arlPOH
            'Total TotalLiquefied 
            'Start  :Optimize EffectiveDate calculation;By:DoniN;20100329
            'If IsHavingGyro(objPOH) Then
            '    EffectiveDate = CType(objPOH.DailyPayments(0), DailyPayment).EffectiveDate
            '    EffectiveDate = ReqDelDate ' assuming all dp is included in this procedure (it will be checked below)
            'Else
            '    If objPOH.TermOfPayment.PaymentType = enumPaymentType.PaymentType.TOP Then
            '        nTOPDays = CType(objPOH.TermOfPayment.TermOfPaymentCode.Substring(1), Integer)
            '        EffectiveDate = AddWorkingDay(objPOH.ReqAllocationDateTime, nTOPDays + 1)
            '    ElseIf objPOH.TermOfPayment.PaymentType = enumPaymentType.PaymentType.COD Then
            '        EffectiveDate = AddWorkingDay(objPOH.ReqAllocationDateTime, 0 + 2)
            '    ElseIf objPOH.TermOfPayment.PaymentType = enumPaymentType.PaymentType.RTGS Then
            '        EffectiveDate = AddWorkingDay(objPOH.ReqAllocationDateTime, 0)
            '    End If
            'End If
            EffectiveDate = IIf(objPOH.IsHavingGyro, ReqDelDate, objPOH.EffectiveDate)
            'End    :Optimize EffectiveDate calculation;By:DoniN;20100329

            If EffectiveDate >= ReportDate And EffectiveDate <= ReqDelDate Then
                If objPOH.Status = 8 Then
                    If EffectiveDate >= ReportDate.AddDays(1) Then
                        If objPOH.DailyPayments.Count = 0 Then
                            TotalLiquefied += objPOH.TotalPODetail()
                        Else
                            For Each objDP As DailyPayment In objPOH.DailyPayments
                                If (objDP.PaymentPurpose.ID = 3 Or objDP.PaymentPurpose.ID = 6) And objDP.IsReversed = 0 And objDP.IsCleared = 0 And objDP.EffectiveDate >= DateSerial(Now.Year, Now.Month, Now.Day + 1) And objDP.EffectiveDate <= ReqDelDate AndAlso objDP.Status = CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) Then
                                    TotalLiquefied = TotalLiquefied + objDP.Amount
                                    'sb.Append(objPOH.PONumber & vbTab & objPOH.Status & vbTab & objDP.Amount & vbTab & EffectiveDate.ToString("dd/MM/YYYY") & vbTab & "DPID=" & objDP.PaymentPurpose.ID & Chr(13))
                                End If
                            Next
                        End If
                    End If
                Else
                    TotalLiquefied += objPOH.TotalPODetail()
                End If
            End If

            Dim TotalPOInDB As Decimal = Me.GetTotalPO(objPOH)
            Dim TotalPOInPage As Decimal = 0

            'Start  : Use this allocation
            'If PODetail is listed in this datagrid and it's allocation is bigger than 0
            'then nilai podetail adalah Price * it's allocation (txtAllocation)
            'the way of it is:kurangi dengan yg didatabase lalu ditambah dengan yg di page (txtAllocation)
            TotalLiquefied -= TotalPOInDB
            TotalPOInPage = TotalPOInDB
            arlListPO = CType(sessionHelper.GetSession("arlListPO"), ArrayList)
            For Each di As DataGridItem In dtgEntryPOAllocation.Items
                Dim txtAllocation As TextBox = di.FindControl("txtAllocation")
                Dim objPODInPage As PODetail = arlListPO(di.ItemIndex)
                If objPODInPage.POHeader.ID = objPOH.ID Then
                    For Each objPODInDB As PODetail In objPOH.PODetails
                        If objPODInDB.ID = objPODInPage.ID Then
                            If CType(txtAllocation.Text, Decimal) > 0 Then
                                If objPOH.Status = 0 Or objPOH.Status = 2 Then
                                    TotalPOInPage -= (objPODInDB.Price * objPODInDB.ReqQty)
                                Else
                                    TotalPOInPage -= (objPODInDB.Price * objPODInDB.AllocQty)
                                End If
                                TotalPOInPage += (objPODInDB.Price * CType(txtAllocation.Text, Decimal))
                            End If
                        End If
                    Next
                End If
            Next
            TotalLiquefied += TotalPOInPage
            'End    : Use this allocation

        Next
        Dim objDPFac As DailyPaymentFacade = New DailyPaymentFacade(User)
        Dim crtDP As CriteriaComposite
        Dim arlDP As ArrayList
        Dim aggregates As New Aggregate(GetType(DailyPayment), "Amount", AggregateType.Sum)

        'Accelerated Gyro
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.Exact, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.GreaterOrEqual, ReportDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedDate", MatchType.LesserOrEqual, ReqDelDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Dealer.ID", MatchType.Exact, objDealer.ID))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        arlDP = objDPFac.Retrieve(crtDP)
        TotalAcceleratedGyro = 0
        For Each oDP As DailyPayment In arlDP
            TotalAcceleratedGyro += oDP.Amount
        Next
        'TotalAcceleratedGyro += objDPFac.GetAggregateResult(aggregates, crtDP)
        'Not Accelerated Gyro
        crtDP = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "AcceleratedGyro", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.GreaterOrEqual, ReportDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "EffectiveDate", MatchType.LesserOrEqual, ReqDelDate))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsReversed", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "IsCleared", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.No, "1"))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "Status", MatchType.Exact, CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.TermOfPayment.PaymentType", MatchType.Exact, PaymentType))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "POHeader.Dealer.ID", MatchType.Exact, objDealer.ID))
        crtDP.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.ID", MatchType.InSet, "(3,6)"))
        arlDP = objDPFac.Retrieve(crtDP)
        For Each oDP As DailyPayment In arlDP
            TotalAcceleratedGyro += oDP.Amount
        Next
        'TotalAcceleratedGyro += objDPFac.GetAggregateResult(aggregates, crtDP)

        arlResult.Add(TotalLiquefied)
        arlResult.Add(TotalAcceleratedGyro)
        Return arlResult
    End Function

    Private Function AddWorkingDay(ByVal StateDate As Date, ByVal nAdded As Integer, Optional ByVal IsBackWard As Boolean = False) As Date
        Dim objNHFac As NationalHolidayFacade = New NationalHolidayFacade(User)
        Dim crtNH As CriteriaComposite
        Dim rslDate As Date
        Dim IsHoliday As Boolean = True
        Dim arlNH As ArrayList = New ArrayList
        Dim i As Integer = 0

        rslDate = StateDate
        For i = 1 To Math.Abs(nAdded)
            rslDate = rslDate.AddDays(IIf(IsBackWard, -1, 1))
            IsHoliday = True
            While IsHoliday = True
                crtNH = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Exact, rslDate))
                arlNH = objNHFac.Retrieve(crtNH)
                If arlNH.Count < 1 Then
                    IsHoliday = False
                Else
                    rslDate = rslDate.AddDays(IIf(IsBackWard = False, 1, -1))
                End If
            End While
        Next
        'rslDate = StateDate.AddDays(nAdded)
        'While IsHoliday = True
        '    crtNH = New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    crtNH.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDateTime", MatchType.Exact, rslDate))
        '    arlNH = objNHFac.Retrieve(crtNH)
        '    If arlNH.Count < 1 Then
        '        IsHoliday = False
        '    Else
        '        rslDate = rslDate.AddDays(IIf(IsBackWard = False, 1, -1))
        '    End If
        'End While
        Return rslDate
    End Function

    Private Function IsHavingGyro(ByRef objPOH As POHeader) As Boolean
        Dim Rsl As Boolean = True

        If objPOH.DailyPayments.Count < 1 Then
            Rsl = False
        Else
            For Each objDP As DailyPayment In objPOH.DailyPayments
                If (objDP.PaymentPurpose.ID = 3 Or objDP.PaymentPurpose.ID = 6) And (objDP.RejectStatus = 0 And objDP.IsCleared = 0 And objDP.IsReversed = 0 And objDP.Status = CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)) Then
                    Return True
                Else
                    Rsl = False
                End If
            Next
        End If
        Return Rsl
    End Function

    Private Function IsValidPODate() As Boolean
        'Dim arlListPO As New ArrayList
        Dim i As Integer
        Dim IsConverted As Boolean
        Dim objPOD As PODetail
        Dim objPOH As POHeader
        Dim chkItemChecked As CheckBox
        Dim txtAllocation As TextBox
        Dim chkIsConverted As CheckBox
        Dim TmpDate As Date
        Dim strMessage As String = ""
        'sessionHelper.GetSession("arlListPO")
        arlListPO = sessionHelper.GetSession("arlListPO")
        If arlListPO.Count < 1 Then Exit Function
        For i = 0 To arlListPO.Count - 1
            IsConverted = False
            objPOD = arlListPO(i)
            objPOH = objPOD.POHeader
            'Remaining Modul
            txtAllocation = CType(dtgEntryPOAllocation.Items.Item(i).FindControl("txtAllocation"), TextBox)
            chkItemChecked = CType(dtgEntryPOAllocation.Items.Item(i).FindControl("chkItemChecked"), CheckBox)
            chkIsConverted = CType(dtgEntryPOAllocation.Items.Item(i).FindControl("chkIsConverted"), CheckBox)
            If chkItemChecked.Checked And chkIsConverted.Checked Then
                TmpDate = DateSerial(Now.Year, Now.Month, Now.Day)
                IsConverted = True
            Else
                TmpDate = objPOH.ReqAllocationDateTime
            End If
            If CType(txtAllocation.Text, Integer) > 0 And IsConverted = False And TmpDate < DateSerial(Now.Year, Now.Month, Now.Day) Then
                strMessage &= IIf(strMessage.Trim = "", "", "; ") & objPOH.PONumber
            End If
        Next
        If strMessage.Trim <> "" Then
            MessageBox.Confirm("Tgl permintaan kirim kurang dari hari ini, lanjut untuk menyimpan? ", "hdnValConfirm")
            Return False
        Else
            Return True
        End If
    End Function

    Private Function GetTotalPOBefore(ByVal POID As Integer) As Decimal
        Dim objPOH As POHeader = New POHeaderFacade(User).Retrieve(POID)
        Dim Total As Decimal = 0
        Total = objPOH.TotalPODetail()

        'For Each objPOD As PODetail In objPOH.PODetails
        '    If objPOH.Status = 0 Or objPOH.Status = 2 Or objPOH.Status = 4 Then
        '        Total += (objPOD.Price * objPOD.ReqQty)
        '    Else
        '        Total += (objPOD.Price * objPOD.AllocQty)
        '    End If
        'Next
        Return Total
    End Function

    Private Function IsLesserThanAvailableCeiling(ByVal objPOH As POHeader, Optional ByVal IsAfterSaving As Boolean = False) As Boolean
        Dim objD As Dealer = Session("DEALER")
        Dim TotalPO As Decimal = GetTotalPO(objPOH) ' CType(viewstate.Item("SubTotalHarga"), Decimal)
        Dim oTEOP As TermOfPayment = objPOH.TermOfPayment ' New TermOfPaymentFacade(User).Retrieve(CType(Me.ddlTermOfPayment.SelectedValue, Integer))
        Dim IsLesser As Boolean = False
        If objPOH.IsTransfer = 1 Then
            Return True
        End If
        If objPOH.IsFactoring = 1 Then
            Dim AvFactCeiling As Decimal = 0
            Dim oFM As FactoringMaster = New FactoringMasterFacade(User).Retrieve(objPOH.ContractHeader.Category.ProductCategory, objPOH.Dealer.CreditAccount)

            If oTEOP.PaymentType = enumPaymentType.PaymentType.TOP Then
                Dim dtJatuhTempo As Date = DateAdd(DateInterval.Day, oTEOP.TermOfPaymentValue, objPOH.ReqAllocationDateTime)
                If dtJatuhTempo > oFM.MaxTOPDate Then
                    MessageBox.Show("Jatuh Tempo PO Melebihi Tanggal Validitas Ceiling")
                    Return False
                End If
            End If
            IsLesser = CommonFunction.IsEnoughForFactoring(objPOH.ContractHeader.Category.ProductCategory, objPOH.ID, TotalPO, objPOH.Dealer.CreditAccount, IsAfterSaving, AvFactCeiling)        ' IsEnoughForFactoring()
            Me.lblA.Text = ""
            Me.lblB.Text = ""
            Me.lblC.Text = ""
            Me.lblD.Text = ""
            Me.lblAvCeiling.Text = FormatNumber(AvFactCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.lblTotalPO.Text = FormatNumber(TotalPO, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)

        Else
            Dim Ceiling As Decimal = 0
            Dim Proposed As Decimal = 0
            Dim Liquified As Decimal = 0
            Dim Outstanding As Decimal = 0
            Dim TodaysAvCeiling As Decimal = 0
            Dim TomorrowAvCeiling As Decimal = 0
            Dim AvCeiling As Decimal = 0

            IsLesser = CommonFunction.IsCeilingEnough(objPOH.ContractHeader.Category.ProductCategory, objPOH.ID, objPOH.ReqAllocationDateTime, TotalPO, objPOH.Dealer.CreditAccount, oTEOP.PaymentType, IsAfterSaving, Ceiling, Proposed, Liquified, Outstanding, TodaysAvCeiling, TomorrowAvCeiling, AvCeiling)
            Me.lblA.Text = FormatNumber(Ceiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.lblB.Text = FormatNumber(Proposed, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.lblC.Text = FormatNumber(Liquified, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.lblD.Text = ""

            Me.lblAvCeiling.Text = FormatNumber(AvCeiling, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Me.lblTotalPO.Text = FormatNumber(TotalPO, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
        Return IsLesser
    End Function

    Private Sub SaveDatagridPosition() 'Remaining Module - CR-Yurike by Doni MS
        Dim arlDTGPosition As New ArrayList
        Dim txtAllocation As TextBox '0
        Dim chkItemChecked As CheckBox '1
        Dim chkIsConverted As CheckBox '2
        Dim txtBlockedStatus As TextBox '3
        Dim lblReqAllocationDateTime As Label '4
        Dim lbtnNoRegPO As LinkButton '5

        For Each dtgItem As DataGridItem In dtgEntryPOAllocation.Items
            txtAllocation = CType(dtgItem.FindControl("txtAllocation"), TextBox)
            chkItemChecked = CType(dtgItem.FindControl("chkItemChecked"), CheckBox)
            chkIsConverted = CType(dtgItem.FindControl("chkIsConverted"), CheckBox)
            txtBlockedStatus = CType(dtgItem.FindControl("txtBlockedStatus"), TextBox)
            lblReqAllocationDateTime = CType(dtgItem.FindControl("lblReqAllocationDateTime"), Label)
            lbtnNoRegPO = CType(dtgItem.FindControl("lbtnNoRegPO"), LinkButton)

            arlDTGPosition.Add(txtAllocation.Text & ";" & chkItemChecked.Checked & ";" & chkIsConverted.Checked & ";" & txtBlockedStatus.Text & ";" & lblReqAllocationDateTime.Text & ";" & lbtnNoRegPO.Text)
        Next
        sessionHelper.SetSession("FrmEntryPOAllocation.DTGPosition", arlDTGPosition)
    End Sub

    Private Sub UpdateDtgWithLastPosition() 'Remaining Module - CR-Yurike by Doni MS
        Dim arlDTGPosition As New ArrayList
        Dim txtAllocation As TextBox '0
        Dim chkItemChecked As CheckBox '1
        Dim chkIsConverted As CheckBox '2
        Dim txtBlockedStatus As TextBox '3
        Dim lblReqAllocationDateTime As Label '4
        Dim lbtnNoRegPO As LinkButton '5
        Dim str() As String
        Dim i As Integer = 0

        arlDTGPosition = CType(sessionHelper.GetSession("FrmEntryPOAllocation.DTGPosition"), ArrayList)
        For i = 0 To arlDTGPosition.Count - 1
            str = CType(arlDTGPosition(i), String).Split(";")
            txtAllocation = CType(dtgEntryPOAllocation.Items(i).FindControl("txtAllocation"), TextBox)
            chkItemChecked = CType(dtgEntryPOAllocation.Items(i).FindControl("chkItemChecked"), CheckBox)
            chkIsConverted = CType(dtgEntryPOAllocation.Items(i).FindControl("chkIsConverted"), CheckBox)
            txtBlockedStatus = CType(dtgEntryPOAllocation.Items(i).FindControl("txtBlockedStatus"), TextBox)
            lblReqAllocationDateTime = CType(dtgEntryPOAllocation.Items(i).FindControl("lblReqAllocationDateTime"), Label)
            lbtnNoRegPO = CType(dtgEntryPOAllocation.Items(i).FindControl("lbtnNoRegPO"), LinkButton)

            If lbtnNoRegPO.Text <> str(5) Then
                BindDataToGrid()
                Exit Sub
            Else
                txtAllocation.Text = str(0)
                chkItemChecked.Checked = CType(str(1), Boolean)
                chkIsConverted.Checked = CType(str(2), Boolean)
                txtBlockedStatus.Text = str(3)
                lblReqAllocationDateTime.Text = str(4)
            End If
        Next
    End Sub

    Private Function IsEnableCeilingFilter(ByVal oPOH As POHeader) As Boolean
        If oPOH.IsFactoring = 1 Then Return True
        Dim oTC As TransactionControl = New DealerFacade(User).RetrieveTransactionControl(oPOH.Dealer.ID, EnumDealerTransType.DealerTransKind.FilterAlokasi)
        If Not IsNothing(oTC) AndAlso oTC.ID > 0 AndAlso oTC.Status = 1 Then
            Return True
        End If
        Return False
    End Function

#End Region

#Region "EventHandler"

    Sub dtgEntryPOAllocation_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs)
        arlListPO = sessionHelper.GetSession("arlListPO")
        If e.CommandName = "PODetail" Then
            sessionHelper.SetSession("PrevPage", Request.Url.ToString())
            '--Start Remaining Module
            sessionHelper.SetSession("FrmEntryPOAllocation.IsOpenedByBack", "1")
            SaveDatagridPosition()
            '--End Remaining Module
            Response.Redirect("../PO/PODetails.aspx?id=" & CType(arlListPO(e.Item.ItemIndex), PODetail).POHeader.ID)
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        If Not IsPostBack Then
            sessHelp.SetSession("FrmEntryPOAllocation.POID", Request.QueryString("id"))
            InitiatePage()
            StoreInformation()
            GetPODetail()
            BindToHeaderToForm()
            BindDataToGrid()
        Else
            If sessionHelper.GetSession("FrmEntryPOAllocation.IsCheckedDate") = "1" Then
                If Request.Form("hdnValConfirm") = "1" Then
                    hdnValConfirm.Value = "0"
                    btnSimpan_Click(sender, e)
                Else
                    sessionHelper.SetSession("FrmEntryPOAllocation.IsCheckedDate", "0")
                End If
            End If

            'if hdnValConfirm
            If txtIsSaving.Text = "1" Then
                btnSimpan_Click(sender, e)
            End If
        End If
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.AlokasiPODetail_Privielge) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Alokasi PO")
        End If
        btnSimpan.Visible = SecurityProvider.Authorize(Context.User, SR.AlokasiPOSaveDetail_Privilege)
    End Sub

    Sub dtgEntryPOAllocation_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If Not (arlListPO Is Nothing) Then
            If Not (arlListPO.Count = 0 Or e.Item.ItemIndex = -1) Then
                objPODetail = arlListPO(e.Item.ItemIndex)
                Dim lbtnPO As LinkButton = e.Item.FindControl("lbtnNoRegPO")
                lbtnPO.Text = objPODetail.POHeader.PONumber
                e.Item.Cells(0).Text = (e.Item.ItemIndex + 1 + (dtgEntryPOAllocation.PageSize * dtgEntryPOAllocation.CurrentPageIndex)).ToString
                e.Item.Cells(3).Text = objPODetail.ContractDetail.ContractHeader.Dealer.DealerName
                e.Item.Cells(4).Text = objPODetail.ContractDetail.ContractHeader.Dealer.DealerCode & " / " & objPODetail.ContractDetail.ContractHeader.Dealer.SearchTerm1
                e.Item.Cells(7).Text = objPODetail.ContractDetail.ContractHeader.ProjectName
                e.Item.Cells(8).Text = objPODetail.ProposeQty
                e.Item.Cells(9).Text = objPODetail.ReqQty
                e.Item.Cells(12).Text = objPODetail.ContractDetail.SisaUnit
                Dim rangeValidator As RangeValidator = e.Item.FindControl("RangeValidator1")
                rangeValidator.MaximumValue = CInt(objPODetail.ReqQty)
                Dim txt As TextBox = e.Item.FindControl("txtAllocation")
                txt.Text = objPODetail.AllocQty
                'e.Item.Cells(5).Text = Format(objPODetail.POHeader.ReqAllocationDateTime, "dd/MM/yyyy")
                e.Item.Cells(6).Text = Format(objPODetail.POHeader.CreatedTime, "dd/MM/yyyy")

                Dim chkIsMDP As CheckBox = e.Item.FindControl("chkIsMDP")
                chkIsMDP.Enabled = False
                Dim PODraftCriteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                PODraftCriteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.PODraftHeader), "POHeader.ID", MatchType.Exact, objPODetail.POHeader.ID))
                Dim arrPODraftHeader As ArrayList = New PODraftHeaderFacade(User).Retrieve(PODraftCriteria)

                Dim objPODraftHeader As PODraftHeader = New PODraftHeader
                If arrPODraftHeader.Count > 0 Then
                    chkIsMDP.Checked = True
                Else
                    chkIsMDP.Checked = False
                End If

                Dim lblFreeDays As Label = CType(e.Item.FindControl("lblFreeDays"), Label)
                lblFreeDays.Text = objPODetail.FreeDays
                Dim lblMaxTOPDay As Label = CType(e.Item.FindControl("lblMaxTOPDay"), Label)
                lblMaxTOPDay.Text = objPODetail.MaxTOPDay

            End If
        End If
    End Sub

    Private Sub btnHitung_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHitung.Click
        CountSisaAlokasi()
        If (CInt(lblSisaUnitValue.Text) < 0) Then
            MessageBox.Show(SR.OverAlocate("Produksi"))
        End If
        'Dim newArr As New ArrayList
        ''Dim dctTemp As New Dictionary(Of POHeader, Integer)
        'For Each item As DataGridItem In dtgEntryPOAllocation.Items
        '    Dim lblReqAllocationDateTime As Label = CType(item.FindControl("lblReqAllocationDateTime"), Label)
        '    'Dim lbtnNoRegPO As LinkButton = CType(item.FindControl("lbtnNoRegPO"), LinkButton)
        '    Dim txtAllocation As TextBox = CType(item.FindControl("txtAllocation"), TextBox)
        '    Dim txtID As TextBox = CType(item.FindControl("txtID"), TextBox)
        '    Dim lblFreeDays As Label = CType(item.FindControl("lblFreeDays"), Label)
        '    Dim lblMaxTOPDay As Label = CType(item.FindControl("lblMaxTOPDay"), Label)
        '    'ViewState("Tgl") = lblReqAllocationDateTime.Text

        '    'Dim PH = From pd As PODetail In arlListPO
        '    '         Where pd.POHeader.ReqAllocationDateTime = lblReqAllocationDateTime.Text
        '    '          Select pd.POHeader

        '    'For Each pHeader As POHeader In PH
        '    '    If Not dctTemp.ContainsKey(pHeader) Then
        '    '        dctTemp.Add(pHeader, 0)
        '    '    End If
        '    '    If txtAllocation.Text = "" OrElse txtAllocation.Text = String.Empty Then
        '    '        txtAllocation.Text = 0
        '    '    End If
        '    '    dctTemp(pHeader) += txtAllocation.Text
        '    'Next

        '    Dim pd As PODetail = New PODetailFacade(User).Retrieve(CInt(txtID.Text))
        '    Dim cri As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    cri.opAnd(New Criteria(GetType(PODetail), "POHeader.ID", MatchType.Exact, pd.POHeader.ID))
        '    cri.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.InSet, "(0,2,4,6,8)"))
        '    cri.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.VechileType.VechileModel.ID", MatchType.Exact, pd.ContractDetail.VechileColor.VechileType.VechileModel.ID))
        '    Dim arrPd As ArrayList = New PODetailFacade(User).Retrieve(cri)
        '    For Each a As PODetail In arrPd
        '        If a.ID = pd.ID Then
        '            a.AllocQty = txtAllocation.Text
        '        End If
        '        newArr.Add(a)
        '    Next
        '    'If pd.AllocQty <> CInt(txtAllocation.Text) Then
        '    If arrPd.Count > 0 Then
        '        Dim pdDealer As Dealer = CType(arrPd(0), PODetail).POHeader.Dealer
        '        Dim reqAllDate As Date = CDate(lblReqAllocationDateTime.Text)
        '        Dim dt As Date = DateSerial(reqAllDate.Year, reqAllDate.Month, reqAllDate.Day)
        '        Dim w As String = ""
        '        lblFreeDays.Text = SetFreeDays(pdDealer, arrPd, dt, dt, dt, lblMaxTOPDay.Text, w)
        '    End If
        '    'End If
        'Next
        'sessHelp.SetSession("UpdateNih", newArr)
        'For Each PH As KeyValuePair(Of POHeader, Integer) In dctTemp
        '    Dim _FD As Integer = 0
        '    Dim _MTD As Integer = 0
        '    sessHelp.SetSession("HeaderAlloc", PH.Value)
        '    HitungSetFreeDays(PH.Key, _MTD, _FD)
        '    For Each item As DataGridItem In dtgEntryPOAllocation.Items
        '        Dim lbtnNoRegPO As LinkButton = CType(item.FindControl("lbtnNoRegPO"), LinkButton)
        '        Dim lblFreeDays As Label = CType(item.FindControl("lblFreeDays"), Label)
        '        Dim lblMaxTOPDay As Label = CType(item.FindControl("lblMaxTOPDay"), Label)
        '        Dim txtID As TextBox = CType(item.FindControl("txtID"), TextBox)
        '        Dim TxtAllocation As TextBox = CType(item.FindControl("TxtAllocation"), TextBox)
        '        If CType(PH.Key, POHeader).PONumber = lbtnNoRegPO.Text Then
        '            lblFreeDays.Text = _FD
        '            lblMaxTOPDay.Text = _MTD
        '            Exit For
        '        End If
        '    Next
        '    sessHelp.RemoveSession("HeaderAlloc")
        'Next
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        Dim BlockedStatus As Short
        Dim sBlockedStatus As String
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim objPOH As POHeader
        Dim chkItemChecked As CheckBox
        Dim txtAllocation As TextBox
        Dim chkIsConverted As CheckBox
        Dim TmpDate As Date = Now
        Dim IsUpdated As Boolean = False
        Dim n As Integer
        Dim strMessage As String = ""
        Dim IsConverted As Boolean = False
        Dim TimeBeforeAlloc As DateTime = Now.AddSeconds(-5)

        If sessionHelper.GetSession("FrmEntryPOAllocation.IsCheckedDate") <> "1" Then
            If Not Page.IsValid Then
                Return
            End If
        End If

        If sessionHelper.GetSession("FrmEntryPOAllocation.IsCheckedDate") <> "1" Then
            If Not IsValidPODate() Then
                sessionHelper.SetSession("FrmEntryPOAllocation.IsCheckedDate", "1")
                Exit Sub
            End If
        Else
            sessionHelper.SetSession("FrmEntryPOAllocation.IsCheckedDate", "0")
        End If

        arlListPO = sessionHelper.GetSession("arlListPO")
        CountSisaAlokasi()
        If (CInt(lblSisaUnitValue.Text) >= 0) Then
            'Start  :DealerOrder
            'Dim sPODs As String = String.Empty
            'Dim oSAPFac As New sp_AllocPOFacade(User)
            'Dim oSAP As sp_AllocPO
            'Dim IsSuccess As Boolean = False

            'For i As Integer = 0 To arlListPO.Count - 1
            '    Dim objPODetail1 As PODetail = arlListPO(i)
            '    objPODetail1.AllocQty = CType(dtgEntryPOAllocation.Items.Item(i).FindControl("TxtAllocation"), TextBox).Text
            '    sPODs &= IIf(sPODs.Trim = "", "", ";") & objpodetail1.ID.ToString & ":" & objPODetail1.AllocQty.ToString
            'Next
            ''delegate allocation process to stored-procedure 
            'oSAP = oSAPFac.RetrieveFromSP(Now, sPODs)
            'If Not IsNothing(oSAP) AndAlso oSAP.ID > 0 Then
            '    IsSuccess = (oSAP.Status = 0)
            'End If
            'If IsSuccess = False Then
            '    MessageBox.Show("Simpan Alokasi Gagal..") 'double dots as indicator for dnet admin
            '    Exit Sub
            'End If
            'End    :DealerOrder

            'Reset To ZERO
            Dim oPODTemp As PODetail
            Dim objPODetailFacade As New PODetailFacade(User)
            For Each oPOD As PODetail In arlListPO
                oPODTemp = objPODetailFacade.Retrieve(oPOD.ID)
                oPODTemp.AllocQty = 0
                objPODetailFacade.Update(oPODTemp)
            Next
            Dim arlPOH As New ArrayList
            Dim oPODTrigger As PODetail = Nothing
            For i As Integer = 0 To arlListPO.Count - 1
                IsConverted = False
                Dim objPODetail1 As PODetail = arlListPO(i)
                objPODetail1.AllocQty = CType(dtgEntryPOAllocation.Items.Item(i).FindControl("TxtAllocation"), TextBox).Text
                objPODetail1.AllocationDateTime = Now.ToString("yyyy/MM/dd HH:mm:ss") 'will used in DealerOrder (PO\ProposeAllocationList.aspx.vb located in domain PPQty)
                oPODTrigger = objPODetail1
                objPODetailFacade.Update(objPODetail1)
                If i = 0 Then
                    Dim cPPQty As New CriteriaComposite(New Criteria(GetType(PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aPPQtys As ArrayList
                    Dim oPPQtyFac As New PPQtyFacade(User)

                    cPPQty.opAnd(New Criteria(GetType(PPQty), "MaterialNumber", MatchType.Exact, objPODetail1.ContractDetail.VechileColor.MaterialNumber))
                    cPPQty.opAnd(New Criteria(GetType(PPQty), "PeriodeDate", MatchType.Exact, Now.Day))
                    cPPQty.opAnd(New Criteria(GetType(PPQty), "PeriodeMonth", MatchType.Exact, Now.Month))
                    cPPQty.opAnd(New Criteria(GetType(PPQty), "PeriodeYear", MatchType.Exact, Now.Year))
                    aPPQtys = oPPQtyFac.Retrieve(cPPQty)
                    For Each oPPQty As PPQty In aPPQtys
                        oPPQty.ValidatedTime = TimeBeforeAlloc
                        oPPQtyFac.Update(oPPQty)
                    Next
                End If
                objPOH = New POHeaderFacade(User).Retrieve(objPODetail1.POHeader.ID)
                arlPOH.Add(objPOH)
                'Remaining Modul
                txtAllocation = CType(dtgEntryPOAllocation.Items.Item(i).FindControl("txtAllocation"), TextBox)
                chkItemChecked = CType(dtgEntryPOAllocation.Items.Item(i).FindControl("chkItemChecked"), CheckBox)
                chkIsConverted = CType(dtgEntryPOAllocation.Items.Item(i).FindControl("chkIsConverted"), CheckBox)
                If chkItemChecked.Checked Then
                    'Update Allocation DataTime with history
                    If txtAllocation.Text.Trim <> "" And chkIsConverted.Checked Then
                        If CType(txtAllocation.Text, Integer) > 0 Then
                            TmpDate = objPOH.ReqAllocationDateTime
                            objPOH.ReqAllocationDate = Now.Day
                            objPOH.ReqAllocationMonth = Now.Month
                            objPOH.ReqAllocationYear = Now.Year
                            objPOH.LastReqAllocationDateTime = TmpDate
                            objPOH.ReqAllocationDateTime = DateSerial(Now.Year, Now.Month, Now.Day)
                            objPOH.ChangedBy = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo).UserName
                            objPOH.ChangedTime = Format(Now, "yyy.MM.dd hh:mm:ss")

                            'If objPOH.POType = LookUp.EnumJenisOrder.Tambahan And Not IsLesserThanAvailableCeiling(objPOH.ID, objPOH.POType) Then
                            'objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.Blocked
                            'or
                            'objpoh.Status = lookup.ArrayStatusPO
                            'End If

                            'Start  :Optimize EffectiveDate calculation;By:DoniN;20100505
                            objPOH.EffectiveDate = New POHeaderFacade(User).GetPOEffectiveDate(objPOH.ReqAllocationDateTime, objPOH.TermOfPayment.PaymentType, objPOH.TermOfPayment.TermOfPaymentValue)
                            'End    :Optimize EffectiveDate calculation;By:DoniN;20100505
                            n = objPOHFac.Update(objPOH)
                            IsUpdated = True
                            IsConverted = True
                        End If
                    End If
                    'End Update Allocation DataTime with history
                    'Start Blocked
                    'Update the BlockedStatus of POHeader, this will cause the data shown in FrmCeilingBlockedList.aspx
                    'the value of txtBlockedStatus is updated when btnKonversi clicked
                    If objPOH.TermOfPayment.PaymentType <> enumPaymentType.PaymentType.RTGS Then
                        sBlockedStatus = CType(dtgEntryPOAllocation.Items.Item(i).FindControl("txtBlockedStatus"), TextBox).Text
                        If sBlockedStatus.Trim <> "" And IsNumeric(sBlockedStatus) Then
                            BlockedStatus = CType(sBlockedStatus, Short)
                            objPOH.BlockedStatus = BlockedStatus
                        End If
                    End If
                    'End Blocked
                    Dim IsEnableChecking As Boolean = IsEnableCeilingFilter(objPOH) '.ContractHeader.Dealer)
                    If IsEnableChecking Then
                        'Dim IsQualified As Boolean = IsQualifiedPO(objPOH, DateSerial(Now.Year, Now.Month, Now.Day), objPOH.ReqAllocationDateTime)

                        Dim IsQualified As Boolean = Me.IsLesserThanAvailableCeiling(objPOH, True) 'objPOH.ID, objPOH.TermOfPayment.PaymentType) ' IsQualifiedPO(objPOH, DateSerial(Now.Year, Now.Month, Now.Day), objPOH.ReqAllocationDateTime)
                        'Start  :Set Block Status eventhough user didn't hit the Conversion button
                        If objPOH.TermOfPayment.PaymentType <> enumPaymentType.PaymentType.RTGS AndAlso Not IsQualified Then
                            objPOH.BlockedStatus = enumPOBlockedStatus.POBlockedStatus.Blocked
                        End If
                    End If
                    'End    :Set Block Status eventhough user didn't hit the Conversion button
                    objPOHFac.Update(objPOH)
                End If
                'End Remaining Modul
                If IsConverted = False And CType(txtAllocation.Text, Integer) > 0 Then
                    If objPOH.ReqAllocationDateTime <= DateSerial(Now.Year, Now.Month, Now.Day) Then
                        strMessage &= IIf(strMessage.Trim = "", "", "; ") & objPOH.PONumber
                    End If
                End If

            Next
            If strMessage.Trim <> "" Then
                'MessageBox.Show("Tanggal permintaan kirim PO " & strMessage & " <= hari ini")
            End If
            BindToGrid()
            BindDataToGrid()
            'lblPermintaanKirimAkhir.Text
            If IsUpdated And CDate(lblPermintaanKirimAkhir.Text) < CDate(Now) Then
                lblPermintaanKirimAkhir.Text = Format(Now, "dd/MM/yyyy")
            End If
            If IsNothing(oPODTrigger) = False AndAlso oPODTrigger.ID > 0 Then
                oPODTrigger.RowStatus = 5
                objPODetailFacade.Update(oPODTrigger)
            End If

            'For Each poh As POHeader In arlPOH
            'SetFreeDays()
            'btnHitung_Click(Nothing, Nothing)
            'Dim arr As ArrayList = sessHelp.GetSession("UpdateNih")
            'For Each pd As PODetail In arr
            '    Dim _pdf As New PODetailFacade(User)
            '    _pdf.Update(pd)
            'Next
            'Next

            BindToGrid()
            BindDataToGrid()
            MessageBox.Show(SR.SaveSuccess())
        Else
            'MessageBox.Show("Total Alokasi Melebihi Total Produksi")
            MessageBox.Show(SR.OverAlocate("Produksi"))

        End If
    End Sub

    Private Sub SetFreeDays()
        Dim dctTemp As New Dictionary(Of POHeader, Integer)
        For Each item As DataGridItem In dtgEntryPOAllocation.Items
            Dim lblReqAllocationDateTime As Label = CType(item.FindControl("lblReqAllocationDateTime"), Label)
            Dim lbtnNoRegPO As LinkButton = CType(item.FindControl("lbtnNoRegPO"), LinkButton)
            Dim txtAllocation As TextBox = CType(item.FindControl("txtAllocation"), TextBox)
            Dim txtID As TextBox = CType(item.FindControl("txtID"), TextBox)

            Dim PH = From pd As PODetail In arlListPO
                     Where pd.POHeader.ReqAllocationDateTime = lblReqAllocationDateTime.Text
                      Select pd.POHeader

            For Each pHeader As POHeader In PH
                If Not dctTemp.ContainsKey(pHeader) Then
                    dctTemp.Add(pHeader, 0)
                End If
                If txtAllocation.Text = "" OrElse txtAllocation.Text = String.Empty Then
                    txtAllocation.Text = 0
                End If
                dctTemp(pHeader) += txtAllocation.Text
            Next
        Next

        For Each PH As KeyValuePair(Of POHeader, Integer) In dctTemp
            Dim _FD As Integer = 0
            Dim _MTD As Integer = 0
            sessHelp.SetSession("HeaderAlloc", PH.Value)
            HitungSetFreeDays(PH.Key, _MTD, _FD)
            For Each item As DataGridItem In dtgEntryPOAllocation.Items
                Dim lbtnNoRegPO As LinkButton = CType(item.FindControl("lbtnNoRegPO"), LinkButton)
                Dim lblFreeDays As Label = CType(item.FindControl("lblFreeDays"), Label)
                Dim lblMaxTOPDay As Label = CType(item.FindControl("lblMaxTOPDay"), Label)
                Dim txtID As TextBox = CType(item.FindControl("txtID"), TextBox)
                Dim TxtAllocation As TextBox = CType(item.FindControl("TxtAllocation"), TextBox)
                If CType(PH.Key, POHeader).PONumber = lbtnNoRegPO.Text Then
                    For Each d As PODetail In PH.Key.PODetails
                        d.FreeDays = _FD
                        d.MaxTOPDay = _MTD
                        Dim PDFacade As New PODetailFacade(User)
                        PDFacade.Update(d)
                    Next
                    Exit For
                End If
            Next
            sessHelp.RemoveSession("HeaderAlloc")
        Next

        'HACK soalnya kalo simpan 1x ga bener datanya
        If ViewState("Save") Then
            ViewState.Remove("Save")
            Exit Sub
        Else
            ViewState("Save") = True
            SetFreeDays()
        End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        Response.Redirect("../PO/FrmDisplayPOAllocation.aspx?start=" & ViewState("start") & "&end=" & ViewState("end") & "&orderType=" & ViewState("orderType") & "&productionYear=" & ViewState("productionYear") & "&Kategori=" & ViewState("kategori") & "&Tipe=" & ViewState("Tipe") & "&MaterialNumber=" & ViewState("MaterialNumber"))
    End Sub

    Private Sub btnAlokasi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlokasi.Click
        CountSisaAlokasi()
        Dim total As Integer = 0
        Dim totalOrder As Integer = 0
        For Each item As DataGridItem In dtgEntryPOAllocation.Items
            Dim txtBox As TextBox = item.FindControl("txtAllocation")
            total += CInt(txtBox.Text)
            totalOrder += CInt(item.Cells(9).Text)
        Next
        total += CInt(lblSisaUnitValue.Text)
        For Each item As DataGridItem In dtgEntryPOAllocation.Items
            Dim txtBox1 As TextBox = item.FindControl("txtAllocation")
            txtBox1.Text = System.Math.Floor((CInt(item.Cells(9).Text) / totalOrder) * total)
        Next
    End Sub

    Private Sub btnKonversi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKonversi.Click
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim objPODFac As PODetailFacade = New PODetailFacade(User)
        Dim objPOH As POHeader
        Dim str As String = String.Empty
        Dim strTOPFailed As String = String.Empty
        Dim TmpDate As Date = Now
        Dim BlockedStatus As Short

        For Each di As DataGridItem In dtgEntryPOAllocation.Items
            Dim chkItemChecked As CheckBox = di.FindControl("chkItemChecked")
            Dim txtID As TextBox = di.FindControl("txtID")
            Dim txtBlockedStatus As TextBox = di.FindControl("txtBlockedStatus")
            Dim txtAllocation As TextBox = di.FindControl("txtAllocation")
            Dim lblReqAllocationDateTime As Label = di.FindControl("lblReqAllocationDateTime")
            Dim chkIsConverted As CheckBox = di.FindControl("chkIsConverted")

            If chkItemChecked.Checked Then
                Dim sOrigReqDelDate As String
                Dim IsTempSaved As Boolean = False
                objPODetail = objPODFac.Retrieve(CType(txtID.Text, Integer))
                objPOH = objPODetail.POHeader
                sOrigReqDelDate = objPOH.ReqAllocationDateTime
                chkIsConverted.Checked = True
                If CType(txtAllocation.Text, Integer) > 0 Then
                    sOrigReqDelDate = lblReqAllocationDateTime.Text

                    objPOH.ReqAllocationDateTime = Format(Now, "yyyy.MM.dd")
                    objPOHFac.Update(objPOH)
                    IsTempSaved = True

                    lblReqAllocationDateTime.Text = Format(Now, "dd/MM/yyyy")
                End If
                Dim ReqDelDate As Date = CType(sOrigReqDelDate, Date)
                If IsTempSaved Then ReqDelDate = Now.Date
                'If IsEnableCeilingFilter(objPOH.ContractHeader.Dealer) AndAlso Not IsQualifiedPO(objPOH, Now.Date, Now.Date) Then
                ' If IsEnableCeilingFilter(objPOH) AndAlso Not IsQualifiedPO(objPOH, Now.Date, Now.Date) Then
                If IsEnableCeilingFilter(objPOH) AndAlso Not Me.IsLesserThanAvailableCeiling(objPOH, True) Then
                    str &= IIf(str.Trim = "", "", ", ") & objPOH.PONumber
                    BlockedStatus = CType(txtBlockedStatus.Text, Short)
                    If BlockedStatus <> enumPOBlockedStatus.POBlockedStatus.Blocked And BlockedStatus <> enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed And BlockedStatus <> enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed Then
                        txtBlockedStatus.Text = enumPOBlockedStatus.POBlockedStatus.Blocked
                    End If
                End If
                If txtBlockedStatus.Text <> CType(enumPOBlockedStatus.POBlockedStatus.Blocked, String) AndAlso objPOH.IsTransfer = 0 Then
                    Dim MaxTOPDate As Date = DateSerial(1900, 1, 1)
                    If objPOH.IsFactoring <> 1 Then
                        Dim oCM As CreditMaster = New CreditMasterFacade(User).Retrieve(objPOH.ContractHeader.Category.ProductCategory, objPOH.Dealer.CreditAccount, CType(enumPaymentType.PaymentType.TOP, Short))
                        If Not IsNothing(oCM) AndAlso oCM.ID > 0 Then
                            MaxTOPDate = oCM.MaxTOPDate
                        End If
                    Else
                        Dim oFM As FactoringMaster = New FactoringMasterFacade(User).Retrieve(objPOH.Dealer.CreditAccount)
                        If Not IsNothing(oFM) AndAlso oFM.ID > 0 Then
                            MaxTOPDate = oFM.MaxTOPDate
                        End If
                    End If
                    'reqdel+top tidak > maxtopdate 
                    'MessageBox.Show("1: " & Now.AddDays(objPOH.TermOfPayment.TermOfPaymentValue).ToString("yyyy.MM.dd") & "== 2." & MaxTOPDate.ToString("yyyy.MM.dd"))
                    If Now.AddDays(objPOH.TermOfPayment.TermOfPaymentValue) > MaxTOPDate AndAlso objPOH.IsTransfer <> 1 Then
                        strTOPFailed &= IIf(strTOPFailed.Trim = "", "", ", ") & objPOH.PONumber
                        BlockedStatus = CType(txtBlockedStatus.Text, Short)
                        If BlockedStatus <> enumPOBlockedStatus.POBlockedStatus.Blocked And BlockedStatus <> enumPOBlockedStatus.POBlockedStatus.PassedNotConfirmed And BlockedStatus <> enumPOBlockedStatus.POBlockedStatus.PassedAndConfirmed Then
                            txtBlockedStatus.Text = enumPOBlockedStatus.POBlockedStatus.Blocked
                        End If
                    End If
                End If

                If IsTempSaved Then
                    objPOH.ReqAllocationDateTime = sOrigReqDelDate
                    objPOHFac.Update(objPOH)
                End If
            Else
                chkIsConverted.Checked = False
            End If
        Next
        If str.Trim <> "" AndAlso strTOPFailed <> "" Then
            MessageBox.Show("PO " & str & " melebihi Ceiling yang tersedia.\nJatuh Tempo PO melebihi tanggal validitas Ceiling. PO = " & strTOPFailed)
        ElseIf str.Trim <> "" Then
            MessageBox.Show("PO " & str & " melebihi Ceiling yang tersedia.")
        ElseIf strTOPFailed.Trim <> "" Then
            MessageBox.Show("Jatuh Tempo PO melebihi tanggal validitas Ceiling. PO = " & strTOPFailed)
        End If

    End Sub

    Dim sums As Integer = 0
    Private Sub HitungSetFreeDays(poHeader As POHeader, ByRef _MaxTOP As Integer, ByRef _FreeDays As Integer)
        Dim objDealer As Dealer = poHeader.Dealer
        Dim dt As Date = Date.Now 'DateSerial(icPermintaanKirim.Value.Year, icPermintaanKirim.Value.Month, icPermintaanKirim.Value.Day)
        Dim warning As String = ""
        Dim newArr As New ArrayList
        For Each item As DataGridItem In dtgEntryPOAllocation.Items
            Try
                Dim txtAllocation As TextBox = CType(item.FindControl("txtAllocation"), TextBox)
                Dim lbtnNoRegPO As LinkButton = CType(item.FindControl("lbtnNoRegPO"), LinkButton)
                Dim txtID As TextBox = CType(item.FindControl("txtID"), TextBox)

                For Each _detail As PODetail In poHeader.PODetails
                    If lbtnNoRegPO.Text = _detail.POHeader.PONumber Then
                        If _detail.ID = txtID.Text Then
                            _detail.AllocQty = txtAllocation.Text
                        End If
                        _detail.FreeDays = 0
                        _detail.MaxTOPDay = 0
                    End If
                    If Not newArr.Contains(_detail) Then
                        newArr.Add(_detail)
                    End If
                Next
                'sum += CInt(txtAllocation.Text)
            Catch
            End Try
        Next
        _FreeDays = SetFreeDays(objDealer, newArr, poHeader.ReqAllocationDateTime, dt, dt, _MaxTOP, warning)
    End Sub

    'Public Function SetFreeDays(Dealer As Dealer, PoDetails As ArrayList, ValidFrom As Date, ValidTo As Date, ByRef VarMaxTOP As Integer, ByRef LastPeriodeRemain As String) As Integer
    '    Dim sessHelp As SessionHelper = New SessionHelper
    '    Dim recAllocDateTime As Date = Date.MinValue
    '    Dim POTargetFac As New DealerPOTargetFacade(User)
    '    Dim modelID As String = ""
    '    Dim detaiD As New ArrayList
    '    For Each podetail As PODetail In PoDetails
    '        If modelID.Length = 0 Then
    '            modelID = podetail.ContractDetail.VechileColor.VechileType.VechileModel.ID
    '        Else
    '            modelID = modelID & "," & podetail.ContractDetail.VechileColor.VechileType.VechileModel.ID
    '        End If
    '        recAllocDateTime = podetail.POHeader.ReqAllocationDateTime
    '        detaiD.Add(podetail.ID)
    '    Next
    '    Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerPOTarget), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
    '    criteria.opAnd(New Criteria(GetType(DealerPOTarget), "Dealer.ID", MatchType.Exact, Dealer.ID))
    '    criteria.opAnd(New Criteria(GetType(DealerPOTarget), "ValidFrom", MatchType.GreaterOrEqual, ValidFrom.Year & "-" & ValidFrom.Month & "-01 00:00:00.000"))
    '    criteria.opAnd(New Criteria(GetType(DealerPOTarget), "ValidTo", MatchType.LesserOrEqual, ValidTo.Year & "-" & ValidTo.Month & "-" & DateTime.DaysInMonth(ValidFrom.Year, ValidFrom.Month) & " 00:00:00.000"))
    '    criteria.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
    '    Dim sortColl As SortCollection = New SortCollection
    '    sortColl.Add(New Sort(GetType(DealerPOTarget), "ValidTo", Sort.SortDirection.ASC))  '-- Sort by Vechile type code
    '    Dim arlPOTarget As ArrayList = POTargetFac.Retrieve(criteria, sortColl)

    '    Dim PDetailFac As New PODetailFacade(User)
    '    Dim criteriaPD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
    '    criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Dealer.ID", MatchType.Exact, Dealer.ID))
    '    criteriaPD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
    '    criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, ValidFrom.Year & "-" & ValidFrom.Month & "-01 00:00:00.000"))
    '    criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.LesserOrEqual, ValidTo.Year & "-" & ValidTo.Month & "-" & DateTime.DaysInMonth(ValidFrom.Year, ValidFrom.Month) & " 00:00:00.000"))
    '    criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.InSet, ("(0, 2, 4, 6, 8)")))
    '    criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.IsFactoring", MatchType.Exact, 0))
    '    Dim arlPoDetail As ArrayList = PDetailFac.Retrieve(criteriaPD)

    '    Dim AllocRemain As Integer = 0
    '    Dim ExpiredPeriode As Boolean = False
    '    Dim OverQuantity As Boolean = False
    '    Dim CurrentQuantity As Integer = 0
    '    Dim arlPeriodeRemain As New ArrayList
    '    Dim dFDays As New Dictionary(Of Integer, Integer)
    '    Dim dFDaysTarget As New Dictionary(Of Integer, Integer)
    '    Dim _return As Integer = 0

    '    For Each pDetail As PODetail In arlPoDetail

    '        If Not IsNothing(sessHelp.GetSession("EditPO")) Then
    '            If detaiD.Contains(pDetail.ID) Then
    '                pDetail.FreeDays = 0
    '                recAllocDateTime = ValidFrom
    '            End If
    '        End If

    '        If Not dFDays.ContainsKey(pDetail.FreeDays) Then
    '            dFDays.Add(pDetail.FreeDays, 0)
    '        End If

    '        Select Case pDetail.POHeader.Status
    '            Case 0
    '                dFDays(pDetail.FreeDays) += pDetail.ReqQty
    '            Case 2
    '                If pDetail.AllocQty = 0 Then
    '                    dFDays(pDetail.FreeDays) += pDetail.ReqQty
    '                ElseIf pDetail.AllocQty > 0 Then
    '                    dFDays(pDetail.FreeDays) += pDetail.AllocQty
    '                End If
    '            Case 4, 6, 8
    '                dFDays(pDetail.FreeDays) += pDetail.AllocQty
    '        End Select
    '    Next

    '    If Not IsNothing(sessHelp.GetSession("EditPO")) Then
    '        dFDays(0) = CType(sessHelp.GetSession("EditPO"), Integer)
    '    End If

    '    Try
    '        Dim freeDays As ArrayList = POTargetFac.RetrieveDefaultFreeDays(Dealer, modelID) '3 Row
    '        If freeDays.Count > 0 Then
    '            For Each DPT As DealerPOTarget In freeDays
    '                '_return = CType(freeDays(0), DealerPOTarget).FreeDays
    '                'VarMaxTOP = CType(freeDays(0), DealerPOTarget).MaxTOPDay
    '                If recAllocDateTime <= DPT.ValidTo Then
    '                    _return = DPT.FreeDays
    '                    VarMaxTOP = DPT.MaxTOPDay
    '                    Exit For
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '    End Try

    '    Dim carryOver As Integer = 0
    '    For Each dPOT As DealerPOTarget In arlPOTarget
    '        If Not dFDaysTarget.ContainsKey(dPOT.FreeDays) Then
    '            dFDaysTarget.Add(dPOT.FreeDays, dPOT.MaxQuantity)
    '        End If

    '        If carryOver > 0 Then
    '            dFDaysTarget(dPOT.FreeDays) += carryOver
    '        End If

    '        If dFDays.ContainsKey(dPOT.FreeDays) Then
    '            dFDaysTarget(dPOT.FreeDays) -= dFDays(dPOT.FreeDays)
    '            dFDays.Remove(dPOT.FreeDays)
    '            AllocRemain += dFDaysTarget(dPOT.FreeDays)
    '            'If Date.Now.Date > dPOT.ValidTo Then
    '            '    dFDaysTarget(dPOT.FreeDays) = 0
    '            'End If
    '            If LastPeriodeRemain.Length = 0 Then
    '                LastPeriodeRemain = "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
    '            Else
    '                LastPeriodeRemain = LastPeriodeRemain & "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
    '            End If
    '        End If

    '        carryOver = 0
    '        If recAllocDateTime.Date <= dPOT.ValidTo Then
    '            ExpiredPeriode = False
    '        ElseIf recAllocDateTime.Date > dPOT.ValidTo Then
    '            ExpiredPeriode = True
    '            If Date.Now.Date > dPOT.ValidTo Then
    '                carryOver += dFDaysTarget(dPOT.FreeDays)
    '                dFDaysTarget.Remove(dPOT.FreeDays)
    '            End If
    '        End If

    '        If dFDays.ContainsKey(0) Then
    '            If ExpiredPeriode Then
    '                Continue For
    '            End If
    '            If AllocRemain >= 0 Then
    '                If OverQuantity AndAlso (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) >= 0 Then
    '                    _return = dPOT.FreeDays
    '                    VarMaxTOP = dPOT.MaxTOPDay
    '                    dFDaysTarget(dPOT.FreeDays) -= dFDays(0)
    '                    dFDays.Remove(0)
    '                    Continue For
    '                ElseIf (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) >= 0 Then
    '                    _return = dPOT.FreeDays
    '                    VarMaxTOP = dPOT.MaxTOPDay
    '                    dFDaysTarget(dPOT.FreeDays) -= dFDays(0)
    '                    dFDays.Remove(0)
    '                    OverQuantity = False
    '                ElseIf (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) < 0 Then
    '                    OverQuantity = True
    '                    Continue For
    '                Else
    '                    Continue For
    '                End If
    '            End If
    '            If LastPeriodeRemain.Length = 0 Then
    '                LastPeriodeRemain = "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
    '            Else
    '                LastPeriodeRemain = LastPeriodeRemain & "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
    '            End If
    '        End If
    '    Next


    '    Return _return
    'End Function

    Public Function SetFreeDays(Dealer As Dealer, PoDetails As ArrayList, recAllocDateTime As Date, ValidFrom As Date, ValidTo As Date, ByRef VarMaxTOP As Integer, ByRef LastPeriodeRemain As String) As Integer
        Dim sessHelp As SessionHelper = New SessionHelper
        Dim POTargetFac As New DealerPOTargetFacade(User)
        Dim modelID As String = ""
        Dim detaiD As New ArrayList
        For Each podetail As PODetail In PoDetails
            If modelID.Length = 0 Then
                modelID = podetail.ContractDetail.VechileColor.VechileType.VechileModel.ID
            Else
                modelID = modelID & "," & podetail.ContractDetail.VechileColor.VechileType.VechileModel.ID
            End If
            'recAllocDateTime = ValidFrom
            recAllocDateTime = podetail.POHeader.ReqAllocationDateTime
            detaiD.Add(podetail.ID)
        Next
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerPOTarget), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "Dealer.ID", MatchType.Exact, Dealer.ID))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "ValidFrom", MatchType.GreaterOrEqual, ValidFrom.Year & "-" & ValidFrom.Month & "-01 00:00:00.000"))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "ValidTo", MatchType.LesserOrEqual, ValidTo.Year & "-" & ValidTo.Month & "-" & DateTime.DaysInMonth(ValidFrom.Year, ValidFrom.Month) & " 00:00:00.000"))
        criteria.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DealerPOTarget), "ValidTo", Sort.SortDirection.ASC))  '-- Sort by Vechile type code
        Dim arlPOTarget As ArrayList = POTargetFac.Retrieve(criteria, sortColl)
        Dim arlPoDetail As New ArrayList

        Dim PDetailFac As New PODetailFacade(User)
        Dim criteriaPD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Dealer.ID", MatchType.Exact, Dealer.ID))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, ValidFrom.Year & "-" & ValidFrom.Month & "-01 00:00:00.000"))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.LesserOrEqual, ValidTo.Year & "-" & ValidTo.Month & "-" & DateTime.DaysInMonth(ValidFrom.Year, ValidFrom.Month) & " 00:00:00.000"))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.InSet, ("(0, 2, 4, 6, 8)")))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.IsFactoring", MatchType.Exact, 0))
        arlPoDetail = PDetailFac.Retrieve(criteriaPD)

        Dim AllocRemain As Integer = 0
        Dim ExpiredPeriode As Boolean = False
        Dim OverQuantity As Boolean = False
        Dim CurrentQuantity As Integer = 0
        Dim arlPeriodeRemain As New ArrayList
        Dim dFDays As New Dictionary(Of Integer, Integer)
        Dim dFDaysTarget As New Dictionary(Of Integer, Integer)
        Dim _return As Integer = 0

        For Each pDetail As PODetail In arlPoDetail
            If pDetail.POHeader.IsFactoring <> 0 Then
                Continue For
            End If

            If detaiD.Contains(pDetail.ID) Then
                pDetail.FreeDays = 0
                'recAllocDateTime = ValidFrom
                For Each _d As PODetail In PoDetails
                    If pDetail.AllocQty <> _d.AllocQty AndAlso pDetail.ID = _d.ID Then
                        pDetail.AllocQty = _d.AllocQty
                    End If
                Next
            End If

            If Not dFDays.ContainsKey(pDetail.FreeDays) Then
                dFDays.Add(pDetail.FreeDays, 0)
            End If

            Select Case pDetail.POHeader.Status
                Case 0
                    dFDays(pDetail.FreeDays) += pDetail.ReqQty
                Case 2
                    If pDetail.AllocQty = 0 Then
                        dFDays(pDetail.FreeDays) += pDetail.ReqQty
                    ElseIf pDetail.AllocQty > 0 Then
                        dFDays(pDetail.FreeDays) += pDetail.AllocQty
                    End If
                Case 4, 6, 8
                    dFDays(pDetail.FreeDays) += pDetail.AllocQty
            End Select
        Next

        Try
            Dim freeDays As ArrayList = POTargetFac.RetrieveDefaultFreeDays(Dealer, modelID)
            If freeDays.Count > 0 Then
                _return = CType(freeDays(0), DealerPOTarget).FreeDays
                VarMaxTOP = CType(freeDays(0), DealerPOTarget).MaxTOPDay
            End If
        Catch ex As Exception
        End Try

        Dim carryOver As Integer = 0
        For Each dPOT As DealerPOTarget In arlPOTarget
            If Not dFDaysTarget.ContainsKey(dPOT.FreeDays) Then
                dFDaysTarget.Add(dPOT.FreeDays, dPOT.MaxQuantity)
            End If

            If carryOver > 0 Then
                dFDaysTarget(dPOT.FreeDays) += carryOver
            End If

            If dFDays.ContainsKey(dPOT.FreeDays) Then
                dFDaysTarget(dPOT.FreeDays) -= dFDays(dPOT.FreeDays)
                dFDays.Remove(dPOT.FreeDays)
                AllocRemain += dFDaysTarget(dPOT.FreeDays)
                'If Date.Now.Date > dPOT.ValidTo Then
                '    dFDaysTarget(dPOT.FreeDays) = 0
                'End If
                If LastPeriodeRemain.Length = 0 Then
                    LastPeriodeRemain = "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
                Else
                    LastPeriodeRemain = LastPeriodeRemain & "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
                End If
            End If

            carryOver = 0
            If recAllocDateTime.Date <= dPOT.ValidTo Then
                ExpiredPeriode = False
            ElseIf recAllocDateTime.Date > dPOT.ValidTo Then
                ExpiredPeriode = True
                If Date.Now.Date > dPOT.ValidTo Then
                    carryOver += dFDaysTarget(dPOT.FreeDays)
                    dFDaysTarget.Remove(dPOT.FreeDays)
                End If
            End If

            If dFDays.ContainsKey(0) Then
                If ExpiredPeriode Then
                    Continue For
                End If

                If AllocRemain >= 0 Then
                    If OverQuantity AndAlso (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) >= 0 Then
                        _return = dPOT.FreeDays
                        VarMaxTOP = dPOT.MaxTOPDay
                        dFDaysTarget(dPOT.FreeDays) -= dFDays(0)
                        dFDays.Remove(0)
                        Continue For
                    ElseIf (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) >= 0 Then
                        _return = dPOT.FreeDays
                        VarMaxTOP = dPOT.MaxTOPDay
                        dFDaysTarget(dPOT.FreeDays) -= dFDays(0)
                        dFDays.Remove(0)
                        OverQuantity = False
                    ElseIf (dFDaysTarget(dPOT.FreeDays) - dFDays(0)) < 0 Then
                        OverQuantity = True
                        Continue For
                    Else
                        Continue For
                    End If
                Else
                    OverQuantity = True
                    Continue For
                End If

                If LastPeriodeRemain.Length = 0 Then
                    LastPeriodeRemain = "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
                Else
                    LastPeriodeRemain = LastPeriodeRemain & "Sisa freedays " & dPOT.FreeDays & " adalah " & dFDaysTarget.Item(dPOT.FreeDays) & "\n"
                End If
            End If
        Next

        Return _return
    End Function
#End Region

End Class