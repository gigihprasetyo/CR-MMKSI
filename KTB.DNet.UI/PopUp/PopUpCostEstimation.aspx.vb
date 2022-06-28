Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text
Imports KTB.DNet.WebApi.Models
Imports KTB.DNet.WebApi.Models.SalesForce
Imports System.Collections.Generic
Imports System.Linq

Public Class PopUpCostEstimation
    Inherits System.Web.UI.Page

    Private sessHelper As SessionHelper = New SessionHelper
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private standardCodeFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Private serviceBookingFacade As ServiceBookingFacade = New ServiceBookingFacade(User)
    'Private TSBAFacade As TempServiceBookingActivityFacade = New TempServiceBookingActivityFacade(User)
    Private appConfFacade As AppConfigFacade = New AppConfigFacade(User)
    Private SSAFacade As ServiceBookingActivityFacade = New ServiceBookingActivityFacade(User)
    Private GrandTotal As Decimal = 0
    Private GrandTotal2 As Decimal = 0
    Private vechileModelFacade As VechileModelFacade = New VechileModelFacade(User)
    Private fSKindFacade As FSKindFacade = New FSKindFacade(User)
    Private pMKindFacade As PMKindFacade = New PMKindFacade(User)
    Private gRKindFacade As GRKindFacade = New GRKindFacade(User)
    Private recallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)
    Private freeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
    Private recallServiceFacade As RecallServiceFacade = New RecallServiceFacade(User)
    Private pmHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
    Private vechileColorFacade As VechileColorFacade = New VechileColorFacade(User)
    Private chassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
    Private dealerFacade As DealerFacade = New DealerFacade(User)
    Private stallMasterFacade As StallMasterFacade = New StallMasterFacade(User)
    Private vechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
    Private srFUFacade As ServiceReminderFollowUpFacade = New ServiceReminderFollowUpFacade(User)
    Private ccResFacade As CustomerCaseResponseFacade = New CustomerCaseResponseFacade(User)
    Private ccFacade As CustomerCaseFacade = New CustomerCaseFacade(User)
    'Private boolEdit As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            dgCost.DataSource = New ArrayList()
            dgCost.VirtualItemCount = 0
            dgCost.DataBind()

            dgSparePart.DataSource = New ArrayList()
            dgSparePart.VirtualItemCount = 0
            dgSparePart.DataBind()

            If Not String.IsNullorEmpty(Request.QueryString("dealercode")) Then
                sessHelper.SetSession("dealercode", Request.QueryString("dealercode"))
                sessHelper.SetSession("vechiletypecode", Request.QueryString("vechiletypecode"))
                sessHelper.SetSession("chassisnumber", Request.QueryString("chassisnumber"))


                Dim objSBA = CType(sessHelper.GetSession("SBActivity"), ArrayList)
                'Dim objTSBA As New TempServiceBookingActivity
                'Dim VechileTypeCode As String = GetVechileTypeCodeByID(sessHelper.GetSession("vechiletypecode"))
                'crit = New CriteriaComposite(New Criteria(GetType(TempServiceBookingActivity), "ID", MatchType.Greater, 0))
                'crit.opAnd(New Criteria(GetType(TempServiceBookingActivity), "DealerCode", MatchType.Exact, sessHelper.GetSession("dealercode")))
                'crit.opAnd(New Criteria(GetType(TempServiceBookingActivity), "VechileTypeCode", MatchType.Exact, VechileTypeCode))
                'crit.opAnd(New Criteria(GetType(TempServiceBookingActivity), "ChassisNumber", MatchType.Exact, sessHelper.GetSession("chassisnumber")))

                'objSBA = TSBAFacade.Retrieve(crit)
                'If objSBA.Count > 0 Then
                '    objTSBA = CType(objSBA(0), TempServiceBookingActivity)
                'End If

                'If Not IsNothing(objTSBA) Then
                BindCost(objSBA)
                trCost.Visible = True
                trCost2.Visible = True
                'End If
                'TSBAFacade.Delete(objTSBA)
            End If
        End If
    End Sub

    Private Sub BindCost(ByVal objSBA As ArrayList)
        Dim CostEstimation As List(Of VWI_ServiceCostEstimation)
        Dim objCostList = New ArrayList
        Dim objCost = New ArrayList
        Dim tbFinalObject As DataTable
        Dim DTFinal As New DataTable

        If objSBA.Count > 0 Then
            For Each obj As ServiceBookingActivity In objSBA
                Dim KindCode As String = GetKindCodeByID(obj.ServiceTypeID, obj.KindCode)
                'Dim VechileTypeCode As String = GetVechileTypeCodeByID(ddlVehicleTypeCode.SelectedValue)
                Dim VechileTypeCode As String = GetVechileTypeCodeByID(sessHelper.GetSession("vechiletypecode"))
                crit = New CriteriaComposite(New Criteria(GetType(VWI_ServiceCostEstimation), "ID", MatchType.Greater, 0))
                crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "DealerCode", MatchType.Exact, objDealer.DealerCode))
                crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "VechileTypeCode", MatchType.Exact, VechileTypeCode))
                crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "ServiceType", MatchType.Exact, obj.ServiceTypeID))
                crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "KindCode", MatchType.Exact, KindCode))
                If (VechileTypeCode <> "") Then
                    crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "Varian", MatchType.Exact, VechileTypeCode.Substring(0, 2)))
                End If

                CostEstimation = New KTB.DNet.BusinessFacade.Service.VWI_ServiceCostEstimationFacade(User).RetrieveByCriteria(crit).Cast(Of VWI_ServiceCostEstimation).ToList
                If CostEstimation.Count > 0 Then
                    Dim objCosts As New VWI_ServiceCostEstimation
                    For Each objs As VWI_ServiceCostEstimation In CostEstimation
                        '.Add(New ListItem(obj.ID & " - " & obj.Name, obj.ID))
                        objCosts.JenisKegiatan = objs.JenisKegiatan
                        objCosts.JenisService = objs.JenisService
                        objCosts.JasaService = objs.JasaService
                        tbFinalObject = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataTable)(objs.Details)
                        If Not IsNothing(tbFinalObject) Then
                            'tbFinalObject = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataTable)(objs.Details)
                            DTFinal.Merge(tbFinalObject)
                        End If
                        'For Each dr As DataRow In tbFinalObject.Rows
                        '    objCostList.add(dr)
                        'Next
                        objCost.add(objCosts)

                    Next
                End If
            Next
            'For Each obj As ServiceBookingActivity In objSBA
            '    Dim KindCode As String = GetKindCodeByID(obj.ServiceTypeID, obj.KindCode)
            '    Dim VechileTypeCode As String = GetVechileTypeCodeByID(ddlVehicleTypeCode.SelectedValue)
            '    crit = New CriteriaComposite(New Criteria(GetType(VWI_ServiceCostEstimation), "DealerCode", MatchType.Exact, objDealer.DealerCode))
            '    crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "ServiceType", MatchType.Exact, obj.ServiceTypeID))
            '    'crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "KindCode", MatchType.Exact, obj.KindCode))
            '    'crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "VechileTypeCode", MatchType.Exact, ddlVehicleTypeCode.SelectedValue))
            '    crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "KindCode", MatchType.Exact, KindCode))
            '    'If obj.ServiceTypeID = 1 Then
            '    crit.opAnd(New Criteria(GetType(VWI_ServiceCostEstimation), "VechileTypeCode", MatchType.Exact, VechileTypeCode))
            '    'End If
            '    CostEstimation = New KTB.DNet.BusinessFacade.Service.VWI_ServiceCostEstimationFacade(User).RetrieveByCriteria(crit).Cast(Of VWI_ServiceCostEstimation).ToList
            '    If CostEstimation.Count > 0 Then
            '        Dim objCosts As New VWI_ServiceCostEstimation
            '        For Each objs As VWI_ServiceCostEstimation In CostEstimation
            '            '.Add(New ListItem(obj.ID & " - " & obj.Name, obj.ID))
            '            objCosts.JenisKegiatan = objs.JenisKegiatan
            '            objCosts.JenisService = objs.JenisService
            '            objCosts.JasaService = objs.JasaService
            '            tbFinalObject = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataTable)(objs.Details)
            '            DTFinal.Merge(tbFinalObject)
            '            'For Each dr As DataRow In tbFinalObject.Rows
            '            '    objCostList.add(dr)
            '            'Next
            '            objCost.add(objCosts)
            '        Next
            '    End If
            'Next
        End If
        GrandTotal = 0
        dgCost.DataSource = objCost
        dgCost.DataBind()
        GrandTotal2 = 0
        dgSparePart.DataSource = DTFinal
        dgSparePart.DataBind()
        ''dgCost.DataSource = CType(sessHelper.GetSession("SBActivity"), ArrayList)
        'dgCost.DataBind()
    End Sub

    Private Function GetKindCodeByID(ByVal JenisKegiatan As Integer, ByVal JenisService As String) As String
        Dim nResult As String = ""
        Select Case JenisKegiatan

            Case 1
                Dim objFSKind As New FSKind
                crit = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(FSKind), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = fSKindFacade.Retrieve(crit)
                If results.Count > 0 Then
                    objFSKind = CType(results(0), FSKind)
                    nResult = objFSKind.KindCode
                End If

            Case 2
                Dim objPMKind As New PMKind
                crit = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(PMKind), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = pMKindFacade.Retrieve(crit)

                If results.Count > 0 Then
                    objPMKind = CType(results(0), PMKind)
                    nResult = objPMKind.KindCode
                End If
            Case 3
                Dim objRC As New RecallCategory
                crit = New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(RecallCategory), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = recallCategoryFacade.Retrieve(crit)

                If results.Count > 0 Then
                    objRC = CType(results(0), RecallCategory)
                    nResult = objRC.RecallRegNo
                End If
            Case 4
                Dim objGRKind As New GRKind
                crit = New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(GRKind), "ID", MatchType.Exact, JenisService))
                Dim results As ArrayList = gRKindFacade.Retrieve(crit)

                If results.Count > 0 Then
                    objGRKind = CType(results(0), GRKind)
                    nResult = objGRKind.KindCode
                End If
        End Select
        Return nResult
    End Function

    Private Function GetVechileTypeCodeByID(ByVal ID As Integer) As String
        Dim nResult As String = ""
        Dim objVechileTypeCode As New VechileType
        crit = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(VechileType), "ID", MatchType.Exact, ID))
        Dim results As ArrayList = vechileTypeFacade.Retrieve(crit)
        If results.Count > 0 Then
            objVechileTypeCode = CType(results(0), VechileType)
            nResult = objVechileTypeCode.VechileTypeCode
        End If

        Return nResult
    End Function

    Protected Sub dgSparePart_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        Dim lblSubtotal As Label = CType(e.Item.FindControl("lblSubtotal"), Label)
        Dim lblGrandTotal As Label = CType(e.Item.FindControl("lblGrandTotal"), Label)
        'Dim lblTglPro As Label = CType(e.Item.FindControl("lblTglPro"), Label)
        'GrandTotal2 = 0
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgSparePart.PageSize * dgSparePart.CurrentPageIndex)
            GrandTotal2 = GrandTotal2 + CDec(lblSubtotal.Text)
            'If Not IsNothing(RowValue.JenisKegiatan) Then
            '    GrandTotal = GrandTotal + RowValue.JasaService
            '    lblGrandTotal.Text = GrandTotal
            '    '    If RowValue.ServiceDate <= "1/1/1900" Then
            '    '        lblTglPro.Text = ""
            '    '    Else
            '    '        lblTglPro.Text = Format(RowValue.CreatedTime, "dd/MM/yyyy")
            '    '    End If
            'End If
            'e.
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim rowTotal As Decimal =
            '    Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"))
            '    grdTotal = grdTotal + rowTotal
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim lbl As Label = DirectCast(e.Row.FindControl("lblTotal"), Label)
            '    lbl.Text = grdTotal.ToString("N2")
            'End If

        End If
        If e.Item.ItemType = ListItemType.Footer Then
            lblGrandTotal.Text = GrandTotal2
        End If
    End Sub

    Protected Sub dgCost_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        Dim RowValue As VWI_ServiceCostEstimation = CType(e.Item.DataItem, VWI_ServiceCostEstimation)
        Dim lblSubtotal As Label = CType(e.Item.FindControl("lblSubtotal"), Label)
        Dim lblGrandTotal As Label = CType(e.Item.FindControl("lblGrandTotal"), Label)
        'Dim lblTglPro As Label = CType(e.Item.FindControl("lblTglPro"), Label)
        'GrandTotal = 0
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dgCost.PageSize * dgCost.CurrentPageIndex)
            GrandTotal = GrandTotal + RowValue.JasaService
            'If Not IsNothing(RowValue.JenisKegiatan) Then
            '    GrandTotal = GrandTotal + RowValue.JasaService
            '    lblGrandTotal.Text = GrandTotal
            '    '    If RowValue.ServiceDate <= "1/1/1900" Then
            '    '        lblTglPro.Text = ""
            '    '    Else
            '    '        lblTglPro.Text = Format(RowValue.CreatedTime, "dd/MM/yyyy")
            '    '    End If
            'End If
            'e.
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    Dim rowTotal As Decimal =
            '    Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"))
            '    grdTotal = grdTotal + rowTotal
            'End If
            'If e.Row.RowType = DataControlRowType.Footer Then
            '    Dim lbl As Label = DirectCast(e.Row.FindControl("lblTotal"), Label)
            '    lbl.Text = grdTotal.ToString("N2")
            'End If

        End If
        If e.Item.ItemType = ListItemType.Footer Then
            lblGrandTotal.Text = GrandTotal
        End If
    End Sub
End Class