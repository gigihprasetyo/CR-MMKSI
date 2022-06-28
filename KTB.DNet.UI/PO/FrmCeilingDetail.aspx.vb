#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region


Public Class FrmCeilingDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents pnlSearch As System.Web.UI.WebControls.Panel
    Protected WithEvents dtlDetails As System.Web.UI.WebControls.Repeater
    Protected WithEvents pnlDetails As System.Web.UI.WebControls.Panel
    Protected WithEvents BtnDownloadDtl As System.Web.UI.WebControls.Button
    Protected WithEvents dtlDetail As System.Web.UI.WebControls.DataList
    Protected WithEvents lblReqDeliveryDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblCeiling As System.Web.UI.WebControls.Label
    Protected WithEvents lblReportDate As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Private sHelper As New SessionHelper
#End Region

#Region "Custom Methods"

    Private Sub Initialization()

        If Request.Item("CreditAccount") Is Nothing Or Request.Item("PaymentType") Is Nothing Or Request.Item("ReportDate") Is Nothing Or Request.Item("ReqDeliveryDate") Is Nothing Then
            Response.Redirect("FrmCeiling.aspx")
            Exit Sub
        End If


        viewstate.Add("CreditAccount", Request.Item("CreditAccount"))
        viewstate.Add("PaymentType", Request.Item("PaymentType"))
        viewstate.Add("ReportDate", CDate(Request.Item("ReportDate")))
        viewstate.Add("ReqDeliveryDate", CDate(Request.Item("ReqDeliveryDate")))

        lblReqDeliveryDate.Text = Format(viewstate.Item("ReqDeliveryDate"), "dd/MM/yyyy")
        lblReportDate.Text = Format(viewstate.Item("ReportDate"), "dd/MM/yyyy")

    End Sub

    Private Function GetDealer(ByVal CreditAccount As String) As ArrayList
        Dim objDFac As DealerFacade = New DealerFacade(User)
        Dim arlDealer As ArrayList = New ArrayList
        Dim crtD As CriteriaComposite
        crtD = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtD.opAnd(New Criteria(GetType(Dealer), "CreditAccount", MatchType.Exact, Request.Item("CreditAccount")))

        arlDealer = objDFac.Retrieve(crtD)
        Return arlDealer
    End Function

    Private Sub BindDTL()
        Dim objSCMFac As sp_CreditMasterFacade = New sp_CreditMasterFacade(User)
        Dim arlSCM As ArrayList = New ArrayList
        Dim arlSCMToBind As ArrayList = New ArrayList
        Dim objSCM As New sp_CreditMaster
        Dim crtSCM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(sp_CreditMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "CreditAccount", MatchType.Exact, viewstate.Item("CreditAccount")))
        crtSCM.opAnd(New Criteria(GetType(sp_CreditMaster), "PaymentType", MatchType.Exact, viewstate.Item("PaymentType")))
        arlSCM = objSCMFac.RetrieveFromSP(viewstate.Item("ReportDate"), viewstate.Item("ReqDeliveryDate"), crtSCM)
        If arlSCM.Count > 0 Then
            objSCM = CType(arlSCM(0), sp_CreditMaster)
            lblCeiling.Text = FormatNumber(objSCM.Plafon, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        End If
        arlSCMToBind.Add(objSCM)
        arlSCMToBind.Add(objSCM)
        arlSCMToBind.Add(objSCM)
        arlSCMToBind.Add(objSCM)

        dtlDetail.DataSource = arlSCMToBind
        dtlDetail.DataBind()

        RegisterStartupScript("OpenWindow", "<script>FormatDTGProposed();</script>")

    End Sub

    Private Sub BindDtgOutstanding(ByRef Dtg As DataGrid)
        Dim arlDealer As ArrayList
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As ArrayList = New ArrayList
        Dim crtPOH As CriteriaComposite
        'Dim srtPOH As New SortCollection
        Dim arlOut As ArrayList = New ArrayList
        Dim objOut As clsOutstanding
        Dim tmpDate As Date
        Dim nTOPDays As Integer
        Dim TotalPOD As Decimal

        arlDealer = GetDealer(viewstate.Item("CreditAccount"))
        For Each objD As Dealer In arlDealer

            crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, objD.ID))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(viewstate.Item("PaymentType"), Short)))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Now.AddYears(-1)))
            
            arlPOH = objPOHFac.Retrieve(crtPOH)
            For Each objPOH As POHeader In arlPOH
                For Each objDP As DailyPayment In objPOH.DailyPayments
                    If (objDP.PaymentPurpose.ID = 3 Or objDP.PaymentPurpose.ID = 6) And objDP.IsReversed = 0 And objDP.IsCleared = 0 And objDP.EffectiveDate >= CType(lblReportDate.Text, Date).AddDays(1) AndAlso objDP.Status = CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) Then
                        objOut = New clsOutstanding

                        objOut.PONumber = objPOH.PONumber
                        objOut.DealerCode = objD.DealerCode
                        objOut.SONumber = objPOH.SONumber
                        objOut.GiroNumber = objDP.SlipNumber
                        objOut.DeliveryDate = objPOH.ReqAllocationDateTime
                        objOut.BaselineDate = objDP.BaselineDate
                        objOut.EfectiveDate = objDP.EffectiveDate
                        TotalPOD = 0
                        TotalPOD += objPOH.TotalPODetail()
                        objOut.Amount = FormatNumber(objDP.Amount, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)  'TotalPOD
                        arlOut.Add(objOut)
                    End If
                Next
            Next
        Next

        arlOut.Sort()
        Dtg.DataSource = arlOut
        Dtg.DataBind()
    End Sub

    Private Sub BindDtgProposedNew(ByRef Dtg As DataGrid)
        Dim arlProposed As New ArrayList
        Dim objProposed As clsProposed
        Dim EffDateStart As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date)
        Dim i As Integer
        Dim TotalProposed(5) As Decimal
        Dim TotalCair(5) As Decimal
        Dim Total As Decimal
        Dim avCeiling As Decimal = 0

        TotalProposed(0) = 0
        TotalProposed(1) = 0
        TotalProposed(2) = 0
        TotalProposed(3) = 0
        TotalProposed(4) = 0

        TotalCair(0) = 0
        TotalCair(1) = 0
        TotalCair(2) = 0
        TotalCair(3) = 0
        TotalCair(4) = 0

        For Each objPOH As POHeader In GetReqPOList()
            objProposed = New clsProposed
            objProposed.PONumber = objPOH.PONumber '& "ProposedPO"
            objProposed.SONumber = objPOH.SONumber
            objProposed.POStatus = GetPOStatusValue(CType(objPOH.Status, Integer))
            objProposed.AmountDate1 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date), "yyyy.MM.dd"), GetTotalPODetail(objPOH), 0)
            objProposed.AmountDate2 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(1), Date), "yyyy.MM.dd"), GetTotalPODetail(objPOH), 0)
            objProposed.AmountDate3 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date), "yyyy.MM.dd"), GetTotalPODetail(objPOH), 0)
            objProposed.AmountDate4 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(3), Date), "yyyy.MM.dd"), GetTotalPODetail(objPOH), 0)
            objProposed.AmountDate5 = IIf(Format(objPOH.ReqAllocationDateTime, "yyyy.MM.dd") = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date), "yyyy.MM.dd"), GetTotalPODetail(objPOH), 0)
            objProposed.IsRequestedPO = True
            arlProposed.Add(objProposed)

            TotalProposed(0) = TotalProposed(0) + objProposed.AmountDate1
            TotalProposed(1) = TotalProposed(1) + objProposed.AmountDate2
            TotalProposed(2) = TotalProposed(2) + objProposed.AmountDate3
            TotalProposed(3) = TotalProposed(3) + objProposed.AmountDate4
            TotalProposed(4) = TotalProposed(4) + objProposed.AmountDate5
        Next

        'EffDateStart = CommonFunction.AddNWorkingDay(EffDateStart, -2, True)
        'EffDateEnd = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date)  ' 4-2
        'For Each objPOH As POHeader In GetEffPOListA()
        '    objProposed = New clsProposed
        '    objProposed.PONumber = objPOH.PONumber '& "False+2"
        '    objProposed.SONumber = objPOH.SONumber
        '    objProposed.POStatus = GetPOStatusValue(CType(objPOH.Status, Integer))
        '    objProposed.AmountDate1 = GetTotalPODPA(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date))
        '    objProposed.AmountDate2 = GetTotalPODPA(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(1), Date))
        '    objProposed.AmountDate3 = GetTotalPODPA(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date))
        '    objProposed.AmountDate4 = GetTotalPODPA(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(3), Date))
        '    objProposed.AmountDate5 = GetTotalPODPA(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date))
        '    objProposed.IsRequestedPO = False
        '    arlProposed.Add(objProposed)

        '    TotalCair(0) = TotalCair(0) + objProposed.AmountDate1
        '    TotalCair(1) = TotalCair(1) + objProposed.AmountDate2
        '    TotalCair(2) = TotalCair(2) + objProposed.AmountDate3
        '    TotalCair(3) = TotalCair(3) + objProposed.AmountDate4
        '    TotalCair(4) = TotalCair(4) + objProposed.AmountDate5
        'Next

        'EffDateStart = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        'EffDateEnd = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date)
        'For Each objPOH As POHeader In GetEffPOListB()
        '    objProposed = New clsProposed
        '    objProposed.PONumber = objPOH.PONumber ' & "False+DP"
        '    objProposed.SONumber = objPOH.SONumber
        '    objProposed.POStatus = GetPOStatusValue(CType(objPOH.Status, Integer))
        '    objProposed.AmountDate1 = GetTotalPODPB(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date))
        '    objProposed.AmountDate2 = GetTotalPODPB(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(1), Date))
        '    objProposed.AmountDate3 = GetTotalPODPB(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date))
        '    objProposed.AmountDate4 = GetTotalPODPB(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(3), Date))
        '    objProposed.AmountDate5 = GetTotalPODPB(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date))
        '    objProposed.IsRequestedPO = False
        '    arlProposed.Add(objProposed)

        '    TotalCair(0) = TotalCair(0) + objProposed.AmountDate1
        '    TotalCair(1) = TotalCair(1) + objProposed.AmountDate2
        '    TotalCair(2) = TotalCair(2) + objProposed.AmountDate3
        '    TotalCair(3) = TotalCair(3) + objProposed.AmountDate4
        '    TotalCair(4) = TotalCair(4) + objProposed.AmountDate5
        'Next

        TotalCair = sHelper.GetSession("FrmCeilingDetail.TotalCair")
        'Virtual Footer Row I SubTotal
        objProposed = New clsProposed

        objProposed.PONumber = "JUMLAH"
        objProposed.SONumber = ""
        objProposed.POStatus = ""
        objProposed.AmountDate1 = FormatNumber(TotalProposed(0), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate2 = FormatNumber(TotalProposed(1), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate3 = FormatNumber(TotalProposed(2), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate4 = FormatNumber(TotalProposed(3), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate5 = FormatNumber(TotalProposed(4), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        arlProposed.Add(objProposed)
        'Virtual Footer Row II Total
        objProposed = New clsProposed

        objProposed.PONumber = "Balance of the Day " & (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType"))
        objProposed.SONumber = ""
        objProposed.POStatus = ""
        Dim lblJumlah As Label = Me.dtlDetail.Items(0).FindControl("lblJumlah")

        avCeiling = CDec(lblCeiling.Text) - CDec(lblJumlah.Text)
        Total = CDec(lblCeiling.Text) - CDec(lblJumlah.Text) - TotalProposed(0) '+ TotalCair(0) 'It's covered by SAP application
        'Total = avCeiling - (TotalProposed(0) + TotalProposed(1)) + (TotalCair(0) + TotalCair(1))
        objProposed.AmountDate1 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

        Total = Total - TotalProposed(1) + TotalCair(1)
        'Total = avCeiling - (TotalProposed(0) + TotalProposed(1) + TotalProposed(2)) + (TotalCair(0) + TotalCair(1) + TotalCair(2))
        objProposed.AmountDate2 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

        Total = Total - TotalProposed(2) + TotalCair(2)
        'Total = avCeiling - (TotalProposed(1) + TotalProposed(2) + TotalProposed(3)) + (TotalCair(1) + TotalCair(2) + TotalCair(3))
        objProposed.AmountDate3 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

        Total = Total - TotalProposed(3) + TotalCair(3)
        'Total = avCeiling - (TotalProposed(2) + TotalProposed(3) + TotalProposed(4)) + (TotalCair(2) + TotalCair(3) + TotalCair(4))
        objProposed.AmountDate4 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

        Total = Total - TotalProposed(4) + TotalCair(4)
        'Total = avCeiling - (TotalProposed(3) + TotalProposed(4)) + (TotalCair(3) + TotalCair(4))
        objProposed.AmountDate5 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

        arlProposed.Add(objProposed)

        Dtg.DataSource = arlProposed
        Dtg.DataBind()

        'Customize Datagrid Color
        'i = 0
        'For Each objProp As clsProposed In arlProposed
        '    If Not objProp.IsRequestedPO Then Dtg.Items(i).BackColor = Color.Yellow
        '    i += 1
        'Next
    End Sub


    Private Sub BindDtgProposedNewCair(ByRef Dtg As DataGrid)
        Dim arlProposed As New ArrayList
        Dim objProposed As clsProposed
        Dim EffDateStart As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date)
        Dim i As Integer
        Dim TotalProposed(5) As Decimal
        Dim TotalCair(5) As Decimal
        Dim Total As Decimal
        Dim avCeiling As Decimal = 0

        TotalProposed(0) = 0
        TotalProposed(1) = 0
        TotalProposed(2) = 0
        TotalProposed(3) = 0
        TotalProposed(4) = 0

        TotalCair(0) = 0
        TotalCair(1) = 0
        TotalCair(2) = 0
        TotalCair(3) = 0
        TotalCair(4) = 0

        EffDateStart = CommonFunction.AddNWorkingDay(EffDateStart, -2, True)
        EffDateEnd = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date)  ' 4-2
        For Each objPOH As POHeader In GetEffPOListA()
            objProposed = New clsProposed
            objProposed.PONumber = objPOH.PONumber '& "Cair+2"
            objProposed.SONumber = objPOH.SONumber
            objProposed.POStatus = GetPOStatusValue(CType(objPOH.Status, Integer))
            objProposed.AmountDate1 = GetTotalPODPA(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date))
            objProposed.AmountDate2 = GetTotalPODPA(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(1), Date))
            objProposed.AmountDate3 = GetTotalPODPA(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date))
            objProposed.AmountDate4 = GetTotalPODPA(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(3), Date))
            objProposed.AmountDate5 = GetTotalPODPA(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date))
            objProposed.IsRequestedPO = False
            arlProposed.Add(objProposed)

            TotalCair(0) = TotalCair(0) + objProposed.AmountDate1
            TotalCair(1) = TotalCair(1) + objProposed.AmountDate2
            TotalCair(2) = TotalCair(2) + objProposed.AmountDate3
            TotalCair(3) = TotalCair(3) + objProposed.AmountDate4
            TotalCair(4) = TotalCair(4) + objProposed.AmountDate5
        Next

        'EffDateStart = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        'EffDateEnd = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date)
        'For Each objPOH As POHeader In GetEffPOListB()
        '    objProposed = New clsProposed
        '    objProposed.PONumber = objPOH.PONumber '& "Cair+DP"
        '    objProposed.SONumber = objPOH.SONumber
        '    objProposed.POStatus = GetPOStatusValue(CType(objPOH.Status, Integer))
        '    objProposed.AmountDate1 = GetTotalPODPB(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date))
        '    objProposed.AmountDate2 = GetTotalPODPB(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(1), Date))
        '    objProposed.AmountDate3 = GetTotalPODPB(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date))
        '    objProposed.AmountDate4 = GetTotalPODPB(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(3), Date))
        '    objProposed.AmountDate5 = GetTotalPODPB(objpoh, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date))
        '    objProposed.IsRequestedPO = False
        '    arlProposed.Add(objProposed)

        '    TotalCair(0) = TotalCair(0) + objProposed.AmountDate1
        '    TotalCair(1) = TotalCair(1) + objProposed.AmountDate2
        '    TotalCair(2) = TotalCair(2) + objProposed.AmountDate3
        '    TotalCair(3) = TotalCair(3) + objProposed.AmountDate4
        '    TotalCair(4) = TotalCair(4) + objProposed.AmountDate5
        'Next

        'Virtual Footer Row I SubTotal
        objProposed = New clsProposed

        objProposed.PONumber = "JUMLAH"
        objProposed.SONumber = ""
        objProposed.POStatus = ""
        objProposed.AmountDate1 = FormatNumber(TotalCair(0), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate2 = FormatNumber(TotalCair(1), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate3 = FormatNumber(TotalCair(2), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate4 = FormatNumber(TotalCair(3), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate5 = FormatNumber(TotalCair(4), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        sHelper.SetSession("FrmCeilingDetail.TotalCair", TotalCair)

        arlProposed.Add(objProposed)
        'Virtual Footer Row II Total
        'objProposed = New clsProposed

        'objProposed.PONumber = "Balance For New PO " & (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType"))
        'objProposed.SONumber = ""
        'objProposed.POStatus = ""
        'Dim lblJumlah As Label = Me.dtlDetail.Items(0).FindControl("lblJumlah")

        'Total = CDec(lblCeiling.Text) - CDec(lblJumlah.Text) - TotalProposed(0) + TotalCair(0)
        'objProposed.AmountDate1 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        'Total = Total - TotalProposed(1) + TotalCair(1)
        'objProposed.AmountDate2 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        'Total = Total - TotalProposed(2) + TotalCair(2)
        'objProposed.AmountDate3 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        'Total = Total - TotalProposed(3) + TotalCair(3)
        'objProposed.AmountDate4 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        'Total = Total - TotalProposed(4) + TotalCair(4)
        'objProposed.AmountDate5 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

        'arlProposed.Add(objProposed)

        Dtg.DataSource = arlProposed
        Dtg.DataBind()

        'Customize Datagrid Color
        i = 0
        For Each objProp As clsProposed In arlProposed
            If Not objProp.IsRequestedPO Then Dtg.Items(i).BackColor = Color.Yellow
            i += 1
        Next
    End Sub


    Private Sub BindDtgProposedNewCairTOP(ByRef Dtg As DataGrid)
        Dim arlProposed As New ArrayList
        Dim objProposed As clsProposed
        Dim EffDateStart As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date)
        Dim i As Integer
        Dim TotalProposed(5) As Decimal
        Dim TotalCair(5) As Decimal
        Dim Total As Decimal
        Dim avCeiling As Decimal = 0

        TotalProposed(0) = 0
        TotalProposed(1) = 0
        TotalProposed(2) = 0
        TotalProposed(3) = 0
        TotalProposed(4) = 0

        TotalCair(0) = 0
        TotalCair(1) = 0
        TotalCair(2) = 0
        TotalCair(3) = 0
        TotalCair(4) = 0

        For Each objPOE As v_POEffectiveDate In GetEffPOListC()
            objProposed = New clsProposed
            objProposed.PONumber = objPOE.POHeader.PONumber '& "Cair+2"
            objProposed.SONumber = objPOE.POHeader.SONumber
            objProposed.POStatus = GetPOStatusValue(CType(objPOE.POHeader.Status, Integer))
            objProposed.AmountDate1 = GetTotalPODPC(objPOE, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date))
            objProposed.AmountDate2 = GetTotalPODPC(objPOE, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(1), Date))
            objProposed.AmountDate3 = GetTotalPODPC(objPOE, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date))
            objProposed.AmountDate4 = GetTotalPODPC(objPOE, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(3), Date))
            objProposed.AmountDate5 = GetTotalPODPC(objPOE, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date))
            objProposed.IsRequestedPO = False
            arlProposed.Add(objProposed)

            TotalCair(0) = TotalCair(0) + objProposed.AmountDate1
            TotalCair(1) = TotalCair(1) + objProposed.AmountDate2
            TotalCair(2) = TotalCair(2) + objProposed.AmountDate3
            TotalCair(3) = TotalCair(3) + objProposed.AmountDate4
            TotalCair(4) = TotalCair(4) + objProposed.AmountDate5
        Next

        EffDateStart = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        EffDateEnd = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date)
        For Each objPOH As POHeader In GetEffPOListB()
            objProposed = New clsProposed
            objProposed.PONumber = objPOH.PONumber '& "Cair = ReqAlloc+nTOP+1"
            objProposed.SONumber = objPOH.SONumber
            objProposed.POStatus = GetPOStatusValue(CType(objPOH.Status, Integer))
            objProposed.AmountDate1 = GetTotalPODPB(objPOH, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date))
            objProposed.AmountDate2 = GetTotalPODPB(objPOH, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(1), Date))
            objProposed.AmountDate3 = GetTotalPODPB(objPOH, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date))
            objProposed.AmountDate4 = GetTotalPODPB(objPOH, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(3), Date))
            objProposed.AmountDate5 = GetTotalPODPB(objPOH, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date))
            objProposed.IsRequestedPO = False
            arlProposed.Add(objProposed)

            TotalCair(0) = TotalCair(0) + objProposed.AmountDate1
            TotalCair(1) = TotalCair(1) + objProposed.AmountDate2
            TotalCair(2) = TotalCair(2) + objProposed.AmountDate3
            TotalCair(3) = TotalCair(3) + objProposed.AmountDate4
            TotalCair(4) = TotalCair(4) + objProposed.AmountDate5
        Next

        'Virtual Footer Row I SubTotal
        objProposed = New clsProposed

        objProposed.PONumber = "JUMLAH"
        objProposed.SONumber = ""
        objProposed.POStatus = ""
        objProposed.AmountDate1 = FormatNumber(TotalCair(0), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate2 = FormatNumber(TotalCair(1), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate3 = FormatNumber(TotalCair(2), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate4 = FormatNumber(TotalCair(3), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProposed.AmountDate5 = FormatNumber(TotalCair(4), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        sHelper.SetSession("FrmCeilingDetail.TotalCair", TotalCair)

        arlProposed.Add(objProposed)
        'Virtual Footer Row II Total
        'objProposed = New clsProposed

        'objProposed.PONumber = "Balance For New PO " & (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType"))
        'objProposed.SONumber = ""
        'objProposed.POStatus = ""
        'Dim lblJumlah As Label = Me.dtlDetail.Items(0).FindControl("lblJumlah")

        'Total = CDec(lblCeiling.Text) - CDec(lblJumlah.Text) - TotalProposed(0) + TotalCair(0)
        'objProposed.AmountDate1 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        'Total = Total - TotalProposed(1) + TotalCair(1)
        'objProposed.AmountDate2 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        'Total = Total - TotalProposed(2) + TotalCair(2)
        'objProposed.AmountDate3 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        'Total = Total - TotalProposed(3) + TotalCair(3)
        'objProposed.AmountDate4 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        'Total = Total - TotalProposed(4) + TotalCair(4)
        'objProposed.AmountDate5 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

        'arlProposed.Add(objProposed)

        Dtg.DataSource = arlProposed
        Dtg.DataBind()

        'Customize Datagrid Color
        i = 0
        For Each objProp As clsProposed In arlProposed
            If Not objProp.IsRequestedPO Then Dtg.Items(i).BackColor = Color.Yellow
            i += 1
        Next
    End Sub

    Private Function GetTotalPODPA(ByRef objPOH As POHeader, ByVal effDate As Date) As Decimal
        Dim Total As Decimal = 0

        If objPOH.Remark = "EffectiveDateFromDailyPayment" Then
            For Each objDP As DailyPayment In objPOH.DailyPayments
                If Format(objDP.EffectiveDate, "yyyyMMdd") = Format(effDate, "yyyyMMdd") _
                AndAlso objDP.IsCleared = 0 AndAlso objDP.IsReversed = 0 AndAlso objDP.RejectStatus = 0 AndAlso objDP.Status = CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) _
                AndAlso 1 = 1 Then ' Format(CommonFunction.AddNWorkingDay(objPOH.ReqAllocationDateTime, 2), "yyyy.MM.dd") = Format(effDate, "yyyy.MM.dd") Then
                    Total = GetTotalPODetail(objPOH)
                End If
            Next
        Else
            If Format(CommonFunction.AddNWorkingDay(objPOH.ReqAllocationDateTime, 2), "yyyy.MM.dd") = Format(effDate, "yyyy.MM.dd") Then
                Total = GetTotalPODetail(objPOH)
            End If

        End If

        Return Total
    End Function

    Private Function GetTotalPODPB(ByRef objPOH As POHeader, ByVal effDate As Date) As Decimal
        Dim Total As Decimal = 0

        For Each objDP As DailyPayment In objPOH.DailyPayments
            If objDP.RejectStatus = 0 AndAlso objDP.IsCleared = 0 AndAlso objDP.IsReversed = 0 AndAlso (objDP.PaymentPurpose.ID = 3 OrElse objDP.PaymentPurpose.ID = 6) _
            And (objDP.Status = CType(EnumPaymentStatus.PaymentStatus.Selesai, Short)) Then
                If Format(effDate, "yyyy.MM.dd") = Format(objDP.EffectiveDate, "yyyy.MM.dd") Then
                    Total += objDP.Amount
                End If
                'Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and RejectStatus=0 and IsCleared=0 and IsReversed=0 "
                'Sql &= " and dp.PaymentPurposeID in (3,6) and dp.EffectiveDate>='" & Format(EffDateStart, "yyyy.MM.dd") & "' and dp.EffectiveDate<='" & Format(EffDateEnd, "yyyy.MM.dd") & "') "
            End If
        Next
        Return Total
    End Function

    Private Function GetTotalPODPC(ByRef objPOE As v_POEffectiveDate, ByVal effDate As Date) As Decimal
        Dim Total As Decimal = 0

        If Format(objPOE.EffectiveDate, "yyyy.MM.dd") = Format(effDate, "yyyy.MM.dd") Then
            Total = GetTotalPODetail(objPOE.POHeader)
        End If

        Return Total
    End Function

    Private Function GetReqPOList() As ArrayList
        Dim EffDateStart As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date)

        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As New ArrayList
        Dim Sql As String = ""
        Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim Total As Decimal = 0

        'AvCeiling - POReqAllinH + POEffDateinH

        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, EffDateStart))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, EffDateEnd))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and dp.RejectStatus=0 and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & ")"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, viewstate.Item("CreditAccount")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(viewstate.Item("PaymentType"), Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))
        arlPOH = objPOHFac.Retrieve(crtPOH)

        Return arlPOH

    End Function
#Region "OldScript"
    Private Function GetEffPOListAOld() As ArrayList
        Dim EffDateStart As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date) ' 4-2
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As ArrayList
        Dim arlPOH2 As ArrayList
        Dim crtPOH As CriteriaComposite
        Dim Sql As String = ""

        'doesn't have gyro
        EffDateStart = CommonFunction.AddNWorkingDay(EffDateStart, -2, True)
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        'Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and RejectStatus=0)"
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)

        'Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and RejectStatus=0)"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)), ")", False)

        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, viewstate.Item("CreditAccount")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(viewstate.Item("PaymentType"), Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, EffDateStart))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, EffDateEnd))
        arlPOH = objPOHFac.Retrieve(crtPOH)

        Return arlPOH
    End Function

    Private Function GetEffPOListBOld() As ArrayList
        Dim EffDateStart As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date) ' 4-2
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As ArrayList
        Dim arlPOH2 As ArrayList
        Dim crtPOH As CriteriaComposite
        Dim Sql As String = ""

        'has data in dailypayment = dailyPayment.effectivedate
        EffDateStart = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        EffDateStart = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date)
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)")) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and dp.RejectStatus=0 and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & " "
        Sql &= " and dp.EffectiveDate>='" & Format(EffDateStart, "yyyy.MM.dd") & "' and dp.EffectiveDate<='" & Format(EffDateEnd, "yyyy.MM.dd") & "') "
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.No, Sql))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, viewstate.Item("CreditAccount")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(viewstate.Item("PaymentType"), Short)))
        arlPOH2 = objPOHFac.Retrieve(crtPOH)

        Return arlPOH2
    End Function

#End Region


    Private Function GetEffPOListA() As ArrayList
        Dim EffDateStart As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date) ' 4-2
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As ArrayList
        Dim arlPOH2 As ArrayList
        Dim crtPOH As CriteriaComposite
        Dim Sql As String = ""
        Dim arlRsl As New ArrayList

        'doesn't have gyro

        EffDateStart = CommonFunction.AddNWorkingDay(EffDateStart, -2, True)
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and dp.RejectStatus=0 and dp.IsReversed=0 and dp.IsCleared=0 and dp.PaymentPurposeID in (3,6) and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & " )"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        'Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and RejectStatus=0)"
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, viewstate.Item("CreditAccount")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(viewstate.Item("PaymentType"), Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, EffDateStart))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, EffDateEnd))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            objpoh.PONumber = objpoh.PONumber '& " EffDate dari POH"
            objpoh.Remark = "EffectiveDateFromPOHeader"

            arlRsl.Add(objPOH)
        Next


        arlPOH = New ArrayList
        'having gyro
        EffDateStart = CommonFunction.AddNWorkingDay(EffDateStart, -2, True)
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and dp.RejectStatus=0 and dp.IsReversed=0 and dp.IsCleared=0 and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & ")"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.No, Sql), ")", False)
        'Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and RejectStatus=0)"
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, viewstate.Item("CreditAccount")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(viewstate.Item("PaymentType"), Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Now.AddYears(-1)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))

        'crtPOH.opAnd(New Criteria(GetType(POHeader), "DailyPayment.EffectiveDate", MatchType.GreaterOrEqual, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)))
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "DailyPayment.EffectiveDate", MatchType.LesserOrEqual, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date)))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        'Dim IsFirst As Boolean = True
        For Each objPOH As POHeader In arlPOH
            For Each objDP As DailyPayment In objpoh.DailyPayments
                'dp.RowStatus=0 and dp.POID=POHeader.ID 
                'and dp.RejectStatus=0 and dp.IsReversed=0 and dp.IsCleared=0
                If objDP.EffectiveDate >= CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date) _
                AndAlso objDP.EffectiveDate <= CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date) _
                AndAlso objDP.IsReversed = 0 AndAlso objDP.IsCleared = 0 AndAlso objDP.RejectStatus = 0 AndAlso objDP.Status = CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) _
                AndAlso (objDP.PaymentPurpose.ID = 3 OrElse objDP.PaymentPurpose.ID = 6) _
                AndAlso 1 = 1 Then
                    objpoh.PONumber = objpoh.PONumber '& " EffDate dari DP"
                    objpoh.Remark = "EffectiveDateFromDailyPayment"
                    arlRsl.Add(objPOH)
                    Exit For
                End If
            Next
        Next

        Return arlRsl
    End Function

    Private Function GetEffPOListB() As ArrayList
        Dim EffDateStart As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date) ' 4-2
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As ArrayList
        Dim arlPOH2 As ArrayList
        Dim crtPOH As CriteriaComposite
        Dim Sql As String = ""

        'has data in dailypayment = dailyPayment.effectivedate
        EffDateStart = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        EffDateEnd = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date)
        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)")) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID and dp.RejectStatus=0 and dp.IsCleared=0 and dp.IsReversed=0 and dp.Status=" & CType(EnumPaymentStatus.PaymentStatus.Selesai, Short) & " "
        Sql &= " and dp.PaymentPurposeID in (3,6) and dp.EffectiveDate>='" & Format(EffDateStart, "yyyy.MM.dd") & "' and dp.EffectiveDate<='" & Format(EffDateEnd, "yyyy.MM.dd") & "') "
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.No, Sql))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "IsFactoring", MatchType.No, 1))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.CreditAccount", MatchType.Exact, viewstate.Item("CreditAccount")))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(viewstate.Item("PaymentType"), Short)))
        arlPOH2 = objPOHFac.Retrieve(crtPOH)

        Return arlPOH2
    End Function

    Private Function GetEffPOListC() As ArrayList
        Dim EffDateStart As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date)
        Dim EffDateEnd As Date = CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date) ' 4-2
        Dim objPOEFac As v_POEffectiveDateFacade = New v_POEffectiveDateFacade(User)
        Dim arlPOE As New ArrayList
        Dim crtPOE As CriteriaComposite

        crtPOE = New CriteriaComposite(New Criteria(GetType(v_POEffectiveDate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOE.opAnd(New Criteria(GetType(v_POEffectiveDate), "CreditAccount", MatchType.Exact, viewstate.Item("CreditAccount")))
        crtPOE.opAnd(New Criteria(GetType(v_POEffectiveDate), "PaymentType", MatchType.Exact, CType(viewstate.Item("PaymentType"), Short)))
        crtPOE.opAnd(New Criteria(GetType(v_POEffectiveDate), "Status", MatchType.InSet, "(0,2,4,6,8)"))
        crtPOE.opAnd(New Criteria(GetType(v_POEffectiveDate), "EffectiveDate", MatchType.GreaterOrEqual, EffDateStart))
        crtPOE.opAnd(New Criteria(GetType(v_POEffectiveDate), "EffectiveDate", MatchType.LesserOrEqual, EffDateEnd))
        arlPOE = objPOEFac.Retrieve(crtPOE)

        Return arlPOE
    End Function


    Private Function GetReqPO(ByVal DealerID As Integer, ByVal curDate As Date) As Decimal
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As New ArrayList
        Dim Sql As String = ""
        Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim Total As Decimal = 0

        'AvCeiling - POReqAllinH + POEffDateinH

        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.Exact, curDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID)"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, DealerID))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(viewstate.Item("PaymentType"), Short)))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            Total += GetTotalPODetail(objPOH)
        Next

    End Function

    Private Function GetCairPO(ByVal DealerID As Integer, ByVal curDate As Date) As Decimal
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As New ArrayList
        Dim Sql As String = ""
        Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim Total As Decimal = 0
        'Jika punya Giro maka POCair = dari DailyPayment
        'Jika tidak punya Giro maka POCair = total dari PODetail

        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.Exact, curDate))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
        Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID)"
        crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, DealerID))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(viewstate.Item("PaymentType"), Short)))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        For Each objPOH As POHeader In arlPOH
            Total += GetTotalPODetail(objPOH)
        Next

    End Function

    Private Sub BindDtgProposed(ByRef Dtg As DataGrid)
        Dim arlDealer As ArrayList
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim arlPOH As ArrayList = New ArrayList
        Dim crtPOH As CriteriaComposite
        Dim arlProp As ArrayList = New ArrayList
        Dim objProp As clsProposed
        Dim tmpDate As Date
        Dim nTOPDays As Integer
        Dim TotalPOD As Decimal
        Dim Sql As String
        Dim ReportDate As Date = viewstate.Item("ReportDate")
        Dim ReqDevDate As Date = viewstate.Item("ReqDeliveryDate")
        Dim TotalProposed(5) As Decimal
        Dim Total As Decimal

        arlDealer = GetDealer(viewstate.Item("CreditAccount"))
        TotalProposed(0) = 0
        TotalProposed(1) = 0
        TotalProposed(2) = 0
        TotalProposed(3) = 0
        TotalProposed(4) = 0

        For Each objD As Dealer In arlDealer
            crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.ID", MatchType.Exact, objD.ID))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.PaymentType", MatchType.Exact, CType(viewstate.Item("PaymentType"), Short)))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(0,2,4,6,8)"), "(", True) 'LookUp.ArrayStatusPO
            Sql = "(select count(id) from DailyPayment dp where dp.RowStatus=0 and dp.POID=POHeader.ID)"
            crtPOH.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, Sql), ")", False)
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(ReportDate, "MM/dd/yyyy")))
            crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, Format(ReqDevDate, "MM/dd/yyyy")))

            arlPOH = objPOHFac.Retrieve(crtPOH)
            For Each objPOH As POHeader In arlPOH
                TotalPOD = 0
                'For Each objPOD As PODetail In objPOH.PODetails
                '    'find in Lookup.ArrayStatusPO
                '    If objPOH.Status = 0 Or objPOH.Status = 2 Then '0:Baru;2:Konfirmasi
                '        TotalPOD = TotalPOD + (objPOD.ReqQty * objPOD.Price)
                '    ElseIf objPOH.Status = 4 Or objPOH.Status = 6 Or objPOH.Status = 8 Then '4:Rilis;6:Setuju;8:Selesai
                '        TotalPOD = TotalPOD + (objPOD.AllocQty * objPOD.Price)
                '    End If
                'Next
                objProp = New clsProposed

                objProp.PONumber = objPOH.PONumber
                objProp.SONumber = objPOH.SONumber
                objProp.POStatus = GetPOStatusValue(objPOH.Status)
                'Total = GetProposed(objPOH.ID, ReportDate.AddDays(0))
                Total = GetProposed(objPOH.ID, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date))
                objProp.AmountDate1 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Total = GetProposed(objPOH.ID, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(1), Date))
                objProp.AmountDate2 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Total = GetProposed(objPOH.ID, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date))
                objProp.AmountDate3 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Total = GetProposed(objPOH.ID, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(3), Date))
                objProp.AmountDate4 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
                Total = GetProposed(objPOH.ID, CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date))
                objProp.AmountDate5 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

                TotalProposed(0) = TotalProposed(0) + objProp.AmountDate1
                TotalProposed(1) = TotalProposed(1) + objProp.AmountDate2
                TotalProposed(2) = TotalProposed(2) + objProp.AmountDate3
                TotalProposed(3) = TotalProposed(3) + objProp.AmountDate4
                TotalProposed(4) = TotalProposed(4) + objProp.AmountDate5
                arlProp.Add(objProp)
            Next
        Next
        'Virtual Footer Row I SubTotal
        objProp = New clsProposed

        objProp.PONumber = "JUMLAH"
        objProp.SONumber = ""
        objProp.POStatus = ""
        objProp.AmountDate1 = FormatNumber(TotalProposed(0), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProp.AmountDate2 = FormatNumber(TotalProposed(1), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProp.AmountDate3 = FormatNumber(TotalProposed(2), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProp.AmountDate4 = FormatNumber(TotalProposed(3), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        objProp.AmountDate5 = FormatNumber(TotalProposed(4), 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        arlProp.Add(objProp)
        'Virtual Footer Row II Total
        objProp = New clsProposed

        objProp.PONumber = "Balance For New PO " & (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType"))
        objProp.SONumber = ""
        objProp.POStatus = ""
        Dim lblJumlah As Label = Me.dtlDetail.Items(0).FindControl("lblJumlah")

        Total = CDec(lblCeiling.Text) - CDec(lblJumlah.Text) - TotalProposed(0)
        objProp.AmountDate1 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Total = Total - TotalProposed(1)
        objProp.AmountDate2 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Total = Total - TotalProposed(2)
        objProp.AmountDate3 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Total = Total - TotalProposed(3)
        objProp.AmountDate4 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
        Total = Total - TotalProposed(4)
        objProp.AmountDate5 = FormatNumber(Total, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

        arlProp.Add(objProp)


        Dtg.DataSource = arlProp
        Dtg.DataBind()
    End Sub

    Private Function GetProposed(ByVal POID As Integer, ByVal ReportDate As Date) As Decimal   ', ByVal ReqDevDate As Date) As Decimal
        Dim crtPOH As CriteriaComposite
        Dim Sql As String
        Dim arlPOH As ArrayList = New ArrayList
        Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
        Dim objPOH As POHeader
        Dim Total As Decimal = 0

        crtPOH = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ID", MatchType.Exact, POID))
        crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.Exact, Format(ReportDate, "MM/dd/yyyy")))
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.GreaterOrEqual, Format(ReportDate, "MM/dd/yyyy")))
        'crtPOH.opAnd(New Criteria(GetType(POHeader), "ReqAllocationDateTime", MatchType.LesserOrEqual, Format(ReqDevDate, "MM/dd/yyyy")))
        arlPOH = objPOHFac.Retrieve(crtPOH)
        If arlPOH.Count > 0 Then
            objPOH = arlPOH(0)
            Total += objPOH.TotalPODetail()
            'For Each objPOD As PODetail In objPOH.PODetails
            '    If objPOH.Status = 0 Or objPOH.Status = 2 Then
            '        Total = Total + (objPOD.ReqQty * objPOD.Price)
            '    ElseIf objPOH.Status = 4 Or objPOH.Status = 6 Or objPOH.Status = 8 Then
            '        Total = Total + (objPOD.AllocQty * objPOD.Price)
            '    End If
            'Next
        End If
        Return Total
    End Function

    Private Function GetTotalPODetail(ByRef objPOH As POHeader) As Decimal
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

    Private Function GetPOStatusValue(ByVal POStatus As Integer) As String
        Dim arlPOStatus As ArrayList = CType(LookUp.ArrayStatusPO, ArrayList)
        For Each li As ListItem In arlPOStatus
            If li.Value = POStatus Then Return li.Text
        Next

    End Function

    Private Class clsOutstanding
        Implements IComparable

        Private _pONumber As String
        Private _dealerCode As String
        Private _sONumber As String
        Private _giroNumber As String
        Private _deliveryDate As Date
        Private _baselineDate As Date
        Private _efectiveDate As Date
        Private _amount As Decimal

        Public Property PONumber() As String
            Get
                Return _pONumber
            End Get
            Set(ByVal Value As String)
                _pONumber = Value
            End Set
        End Property
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal Value As String)
                _dealerCode = Value
            End Set
        End Property
        Public Property SONumber() As String
            Get
                Return _sONumber
            End Get
            Set(ByVal Value As String)
                _sONumber = Value
            End Set
        End Property
        Public Property GiroNumber() As String
            Get
                Return _giroNumber
            End Get
            Set(ByVal Value As String)
                _giroNumber = Value
            End Set
        End Property
        Public Property DeliveryDate() As Date
            Get
                Return _deliveryDate
            End Get
            Set(ByVal Value As Date)
                _deliveryDate = Value
            End Set
        End Property
        Public Property BaselineDate() As Date
            Get
                Return _baselineDate
            End Get
            Set(ByVal Value As Date)
                _baselineDate = Value
            End Set
        End Property
        Public Property EfectiveDate() As Date
            Get
                Return _efectiveDate
            End Get
            Set(ByVal Value As Date)
                _efectiveDate = Value
            End Set
        End Property
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal Value As Decimal)
                _amount = Value
            End Set
        End Property

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            If Not TypeOf obj Is clsOutstanding Then
                Throw New Exception("Object is not clsOutstanding")
            End If
            Dim Compare As clsOutstanding = CType(obj, clsOutstanding)
            Dim result As Integer = Me.EfectiveDate.CompareTo(Compare.EfectiveDate)

            If result = 0 Then
                result = Me.EfectiveDate.CompareTo(Compare.EfectiveDate)
            End If
            Return result
        End Function

    End Class

    Private Class clsProposed
        Private _pONumber As String
        Private _sONumber As String
        Private _pOStatus As String
        Private _amountDate1 As Decimal
        Private _amountDate2 As Decimal
        Private _amountDate3 As Decimal
        Private _amountDate4 As Decimal
        Private _amountDate5 As Decimal
        Private _isRequestedPO As Boolean

        Public Property PONumber() As String
            Get
                Return _pONumber
            End Get
            Set(ByVal Value As String)
                _pONumber = Value
            End Set
        End Property

        Public Property SONumber() As String
            Get
                Return _sONumber
            End Get
            Set(ByVal Value As String)
                _sONumber = Value
            End Set
        End Property

        Public Property POStatus() As String
            Get
                Return _pOStatus
            End Get
            Set(ByVal Value As String)
                _pOStatus = Value
            End Set
        End Property

        Public Property AmountDate1() As Decimal
            Get
                Return _amountDate1
            End Get
            Set(ByVal Value As Decimal)
                _amountDate1 = Value
            End Set
        End Property

        Public Property AmountDate2() As Decimal
            Get
                Return _amountDate2
            End Get
            Set(ByVal Value As Decimal)
                _amountDate2 = Value
            End Set
        End Property
        Public Property AmountDate3() As Decimal
            Get
                Return _amountDate3
            End Get
            Set(ByVal Value As Decimal)
                _amountDate3 = Value
            End Set
        End Property
        Public Property AmountDate4() As Decimal
            Get
                Return _amountDate4
            End Get
            Set(ByVal Value As Decimal)
                _amountDate4 = Value
            End Set
        End Property
        Public Property AmountDate5() As Decimal
            Get
                Return _amountDate5
            End Get
            Set(ByVal Value As Decimal)
                _amountDate5 = Value
            End Set
        End Property

        Public Property IsRequestedPO() As Boolean
            Get
                Return _isRequestedPO
            End Get
            Set(ByVal Value As Boolean)
                _isRequestedPO = Value
            End Set
        End Property
    End Class


    Private Sub CreateFiveDays()
        Dim ReportDate As Date = CType(viewstate.Item("ReportDate"), Date)
        Dim i As Integer
        Dim arlDate As New ArrayList
        ReportDate = ReportDate.AddDays(-1)
        For i = 0 To 4
            ReportDate = CommonFunction.AddNWorkingDay(ReportDate, 1)
            arlDate.Add(ReportDate)
        Next
        sHelper.SetSession("arlReportDate", arlDate)
    End Sub

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Initialization()
            BindDTL()
        End If
    End Sub

    Private Sub dtlDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dtlDetail.ItemDataBound
        If e.Item.ItemIndex = 0 Then ' e.Item.ItemType = ListItemType.Item Then
            Dim objSCMFac As sp_CreditMasterFacade = New sp_CreditMasterFacade(User)
            Dim lblTitle As Label = e.Item.FindControl("lblTitle")
            Dim lblJumlah As Label = e.Item.FindControl("lblJumlah")
            Dim dtgDetail As DataGrid = e.Item.FindControl("dtgDetail")

            sHelper.SetSession("dtgDetail", dtgdetail)
            lblTitle.Text = (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType")) & " Outstanding"
            lblJumlah.Text = FormatNumber(lblJumlah.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            BindDtgOutstanding(dtgDetail)
        ElseIf e.Item.ItemIndex = 1 Then
            Dim dtgDetail2 As DataGrid = e.Item.FindControl("dtgDetail2")
            Dim lblTitle2 As Label = e.Item.FindControl("lblTitle2")
            Dim lblJumlah2 As Label = e.Item.FindControl("lblJumlah2")
            lbljumlah2.Text = sHelper.GetSession("FrmCeilingDetail.LiquefiedPO")
            lblTitle2.Text = (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType")) & " PO Cair"
            lblJumlah2.Text = FormatNumber(lblJumlah2.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)

            sHelper.SetSession("FrmCeilingDetail.dtgDetail2", dtgdetail2)
        ElseIf e.Item.ItemIndex = 2 Then
            Dim lblTitle As Label = e.Item.FindControl("lblTitle")
            Dim lblJumlah As Label = e.Item.FindControl("lblJumlah")
            Dim dtgDetail As DataGrid = e.Item.FindControl("dtgDetail")
            Dim dtgDetail2 As DataGrid = CType(sHelper.GetSession("FrmCeilingDetail.dtgDetail2"), DataGrid)  ' e.Item.FindControl("dtgDetail")

            lblTitle.Text = (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType")) & " PO Cair"
            lblJumlah.Text = FormatNumber(lblJumlah.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CreateFiveDays()
            dtgDetail2.Columns(3).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date), "dd-MMM") ' Format(CType(viewstate.Item("ReportDate"), Date), "dd-MMM")
            dtgDetail2.Columns(4).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(1), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(1), "dd-MMM")
            dtgDetail2.Columns(5).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(2), "dd-MMM")
            dtgDetail2.Columns(6).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(3), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(3), "dd-MMM")
            dtgDetail2.Columns(7).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(4), "dd-MMM")
            If viewstate.Item("PaymentType") = enumPaymentType.PaymentType.TOP Then
                BindDtgProposedNewCairTOP(dtgDetail2)
            Else
                BindDtgProposedNewCair(dtgDetail2)
            End If


            dtgdetail = dtgdetail2
            dtgdetail.DataBind()
            e.Item.Visible = False
            e.Item.Style.Add("visibility", "hidden")
        ElseIf e.Item.ItemIndex = 3 Then 'e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblTitle2 As Label = e.Item.FindControl("lblTitle2")
            Dim lblJumlah2 As Label = e.Item.FindControl("lblJumlah2")
            Dim dtgDetail2 As DataGrid = e.Item.FindControl("dtgDetail2")

            lblTitle2.Text = (New enumPaymentType).GetStringValue(viewstate.Item("PaymentType")) & " Telah Diajukan"
            lblJumlah2.Text = FormatNumber(lblJumlah2.Text, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            CreateFiveDays()
            dtgDetail2.Columns(3).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(0), Date), "dd-MMM") ' Format(CType(viewstate.Item("ReportDate"), Date), "dd-MMM")
            dtgDetail2.Columns(4).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(1), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(1), "dd-MMM")
            dtgDetail2.Columns(5).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(2), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(2), "dd-MMM")
            dtgDetail2.Columns(6).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(3), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(3), "dd-MMM")
            dtgDetail2.Columns(7).HeaderText = Format(CType(CType(sHelper.GetSession("arlReportDate"), ArrayList)(4), Date), "dd-MMM") '  Format(CType(viewstate.Item("ReportDate"), Date).AddDays(4), "dd-MMM")

            BindDtgProposedNew(dtgDetail2)
        End If

    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmCeiling.aspx")
    End Sub

#End Region
End Class
