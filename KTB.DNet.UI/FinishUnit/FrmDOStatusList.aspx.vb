#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.Drawing.Color
Imports KTB.DNet.Utility.CommonFunction
Imports OfficeOpenXml
Imports System.Linq

#End Region

#Region ".NET Base Class Namespace Imports"

Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade
Imports System.Collections.Generic
Imports OfficeOpenXml.Style

#End Region

Public Class FrmDOStatusList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnRefresh As System.Web.UI.WebControls.Button
    Protected WithEvents dgDeliveryOrder As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtNoDO As System.Web.UI.WebControls.TextBox
    Protected WithEvents ICDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtNoChassis As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnHapus As System.Web.UI.WebControls.Button
    Protected WithEvents txtNoPo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTotalBiayaParkir As System.Web.UI.WebControls.Label
    Dim dt As DateTime = DateTime.Now
    Dim suffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
    Protected WithEvents ICDari2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICSampai2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkTglCetak As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkTglKeluar As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTotalChassis As System.Web.UI.WebControls.Label
    Protected WithEvents txtColorGreen As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLocation As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private criteriadownload As String = "CriteriaDownload.FrmDOStatusList"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is requir    ed by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


    Private objDealer As Dealer
#Region "Custom Method"

    Private Sub bindDgDeliveryOrder(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, "0"))

        Dim objAl, objAl3 As ArrayList

        If validateCriteria(criterias) Then
            objAl3 = New ChassisMasterFacade(User).Retrieve(criterias)
            Dim objSessionHelper As New SessionHelper

            objSessionHelper.SetSession("chassisMasterAL", objAl3)

            Dim sortCol As SortCollection = New SortCollection
            sortCol.Add( _
                New Sort(GetType(ChassisMaster), _
                         CType(ViewState("currentSortColumn"), String), _
                         CType(ViewState("currentSortDirection"), Sort.SortDirection)))
            objAl = New ChassisMasterFacade(User).Retrieve(criterias, sortCol)
            'ActiveList(indexPage + 1, dgDeliveryOrder.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            'CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            'dtgGroup.VirtualItemCount = totalRow
            'dtgGroup.DataBind()

            objAl = validateDisplay(objAl)

            objSessionHelper.SetSession("chassisMasterAL", objAl)
            objSessionHelper.SetSession(criteriadownload, objAl)

            dgDeliveryOrder.CurrentPageIndex = indexPage

            If Not objAl.Count = 0 Then
                'Dim objSessionHelper As New SessionHelper

                'objSessionHelper.SetSession("chassisMasterAL", objAl3)

                dgDeliveryOrder.DataSource = objAl
                lblTotalChassis.Text = objAl.Count
                dgDeliveryOrder.VirtualItemCount = totalRow
                dgDeliveryOrder.DataBind()

                btnDownload.Enabled = True
                btnHapus.Enabled = True
            Else
                dgDeliveryOrder.DataSource = Nothing
                dgDeliveryOrder.DataBind()

                MessageBox.Show("Data Tidak Ditemukan")
                btnDownload.Enabled = False
                btnHapus.Enabled = False
                lblTotalBiayaParkir.Text = ""
            End If


        End If


    End Sub

    Private Function validateCriteria(ByRef criterias As CriteriaComposite) As Boolean

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.DealerCode", MatchType.InSet, seperatePopUpReturn(txtKodeDealer.Text.Trim())))
        End If

        If chkTglCetak.Checked Then

            If ICSampai.Value.Subtract(ICDari.Value).Days < 0 Then
                MessageBox.Show("Periode dari tidak boleh lebih besar daripada periode sampai")
                Return False
            End If

            If ICSampai.Value.Subtract(ICDari.Value).Days > 65 Then
                MessageBox.Show("Periode tidak boleh melebihi 65 hari")
                Return False
            End If

            If ICDari.Value.ToString <> "" Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "DODate", MatchType.GreaterOrEqual, ICDari.Value))
            End If

            If ICSampai.Value.ToString <> "" Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "DODate", MatchType.LesserOrEqual, ICSampai.Value))
            End If
        End If

        If chkTglKeluar.Checked Then

            If ICSampai2.Value.Subtract(ICDari2.Value).Days < 0 Then
                MessageBox.Show("Periode dari tidak boleh lebih besar daripada periode sampai")
                Return False
            End If

            If ICSampai2.Value.Subtract(ICDari2.Value).Days > 65 Then
                MessageBox.Show("Periode tidak boleh melebihi 65 hari")
                Return False
            End If

            If ICDari2.Value.ToString <> "" Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "GIDate", MatchType.GreaterOrEqual, ICDari2.Value))
            End If

            If ICSampai2.Value.ToString <> "" Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "GIDate", MatchType.LesserOrEqual, ICSampai2.Value))
            End If
        End If

        If txtNoDO.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "DONumber", MatchType.Exact, txtNoDO.Text))
        End If

        If txtNoChassis.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, txtNoChassis.Text))
        End If

        If txtNoPo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "PONumber", MatchType.Exact, txtNoPo.Text))
        End If

        If ddlStatus.SelectedValue <> "" Then
            If ddlStatus.SelectedValue = "Keluar" Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "GIDate", MatchType.No, "1/1/1900"))
            ElseIf ddlStatus.SelectedValue = "Belum Keluar" Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "GIDate", MatchType.Exact, "1/1/1900"))
                'Remaining Module
            ElseIf ddlStatus.SelectedValue = "Tahan DO" Then
                Dim Sql As String = ""
                Sql = "(select cm.ID from POHeader poh, ChassisMaster cm "
                Sql &= " where poh.SONumber=cm.SONumber and poh.RemarkStatus=" & enumPORemarkStatus.PORemarkStatus.TahanDO & ")"
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.InSet, Sql))
                'End Remaining Module
            End If
        End If

        If txtLocation.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Location.Location", MatchType.Partial, txtLocation.Text))
        End If

        Return True

    End Function

    Private Function validateDisplay(ByVal objAl As ArrayList) As ArrayList
        Dim objAl2 As New ArrayList
        Dim objChassisMaster As ChassisMaster
        Dim dblParkingFeetotal As Double = 0


        For Each objChassisMaster In objAl

            If objChassisMaster.GIDate < "1/1/1901" Then
                Dim parser As New ActualGIDateParser
                Dim temp As Integer = DateDiff(DateInterval.Day, objChassisMaster.DODate, Today) + 1

                objChassisMaster.ParkingDays = temp
                'temp -= 10
                'objChassisMaster.PenaltyParkingDays = temp

                If temp >= 0 Then
                    temp -= 10
                    objChassisMaster.PenaltyParkingDays = temp

                    Dim amount As Long = 0
                    If (objChassisMaster.DODate < New DateTime(2012, 9, 1, 0, 0, 0)) Then
                        'amount += PenaltyCalculation(temp, objChassisMaster)
                        Dim iDays1 As Integer = DateDiff(DateInterval.Day, objChassisMaster.DODate, New DateTime(2012, 9, 1, 0, 0, 0))
                        Dim iDays2 As Integer = DateDiff(DateInterval.Day, New DateTime(2012, 9, 1, 0, 0, 0), New DateTime(2012, 11, 1, 0, 0, 0))
                        Dim iDays3 As Integer = DateDiff(DateInterval.Day, New DateTime(2012, 11, 1, 0, 0, 0), New DateTime(2013, 1, 1, 0, 0, 0))

                        Dim temp1 As Integer = iDays1 - 10
                        If (temp1 > 0) Then
                            If (temp1 <= 20) Then
                                amount += temp1 * 10000
                                temp1 = 20 - temp1
                                objChassisMaster.PenaltyParkingDays = temp1
                                amount += parser.SepToOct12(objChassisMaster, True, objChassisMaster)
                            Else
                                amount += ((temp1 - 20) * 20000) + 200000
                            End If
                        End If
                        Dim temp2 As Integer = iDays2
                        If temp1 > 0 Then
                            If iDays2 > temp1 Then
                                If temp1 > 20 Then
                                    temp2 = iDays2
                                Else
                                    temp2 = iDays2 - temp1
                                End If
                            End If
                        Else
                            temp2 = iDays2 - Math.Abs(temp1)
                        End If
                        If temp2 > 0 Then
                            If temp1 < 0 Then
                                objChassisMaster.ParkingDays = temp2 + 5
                                amount += parser.SepToOct12(objChassisMaster, False, objChassisMaster)
                            Else
                                objChassisMaster.PenaltyParkingDays = temp2
                                amount += parser.SepToOct12(objChassisMaster, True, objChassisMaster)
                            End If
                        End If
                        Dim temp3 As Integer = 0
                        If temp1 < 0 Then
                            temp3 = temp - temp2
                        Else
                            temp3 = temp - temp2 - (iDays1 - 10)
                            If temp1 <= 20 Then
                                temp3 -= temp1
                            End If
                        End If
                        If temp3 > 0 Then
                            objChassisMaster.PenaltyParkingDays = temp3
                            amount += parser.NocToDec12(objChassisMaster, True, objChassisMaster)
                        End If
                        objChassisMaster.ParkingDays = temp + 10
                        objChassisMaster.ParkingAmount = amount

                    ElseIf (objChassisMaster.DODate >= New DateTime(2012, 9, 1, 0, 0, 0) And objChassisMaster.DODate <= New DateTime(2012, 10, 31, 23, 59, 59)) Or (objChassisMaster.DODate >= New DateTime(2013, 1, 1, 0, 0, 0)) Then
                        objChassisMaster.ParkingAmount = parser.SepToOct12(objChassisMaster, False, objChassisMaster)
                    ElseIf objChassisMaster.DODate >= New DateTime(2012, 11, 1, 0, 0, 0) And objChassisMaster.DODate <= New DateTime(2012, 12, 31, 23, 59, 59) Then
                        objChassisMaster.ParkingAmount = parser.NocToDec12(objChassisMaster, False, objChassisMaster)
                    End If
                Else
                    'objChassisMaster.ParkingDays = 0
                    objChassisMaster.PenaltyParkingDays = 0
                    objChassisMaster.ParkingAmount = 0
                End If
                If objChassisMaster.DONumber.Substring(0, 3) = "125" Then
                    'objChassisMaster.ParkingDays = 0
                    objChassisMaster.PenaltyParkingDays = 0
                    objChassisMaster.ParkingAmount = 0
                End If
                'objChassisMaster.PenaltyParkingDays = temp
                If objChassisMaster.PenaltyParkingDays < 0 Then
                    objChassisMaster.PenaltyParkingDays = 0
                    objChassisMaster.ParkingAmount = 0
                End If
            Else
                Dim temp As Integer = DateDiff(DateInterval.Day, objChassisMaster.DODate, objChassisMaster.GIDate) + 1
                objChassisMaster.ParkingDays = temp
                objChassisMaster.PenaltyParkingDays = temp - 10

                If (objChassisMaster.DODate < New DateTime(2012, 9, 1, 0, 0, 0)) Then
                    temp -= 10
                    If temp >= 0 Then
                        objChassisMaster.PenaltyParkingDays = temp
                    Else
                        objChassisMaster.PenaltyParkingDays = 0
                    End If
                ElseIf (objChassisMaster.DODate >= New DateTime(2012, 9, 1, 0, 0, 0) And objChassisMaster.DODate <= New DateTime(2012, 10, 31, 23, 59, 59)) Or (objChassisMaster.DODate >= New DateTime(2013, 1, 1, 0, 0, 0)) Then
                    If temp >= 6 Then
                        objChassisMaster.PenaltyParkingDays = temp - 5
                    Else
                        objChassisMaster.PenaltyParkingDays = 0
                    End If
                ElseIf objChassisMaster.DODate >= New DateTime(2012, 11, 1, 0, 0, 0) And objChassisMaster.DODate <= New DateTime(2012, 12, 31, 23, 59, 59) Then
                    If objChassisMaster.Category.CategoryCode = "PC" Then
                        If temp >= 21 Then
                            objChassisMaster.PenaltyParkingDays = temp - 20
                        Else
                            objChassisMaster.PenaltyParkingDays = 0
                        End If
                    ElseIf (objChassisMaster.Category.CategoryCode = "LCV") Or (objChassisMaster.Category.CategoryCode = "CV") Then
                        objChassisMaster.PenaltyParkingDays = temp - 5
                        If temp >= 6 Then
                            objChassisMaster.PenaltyParkingDays = temp - 5
                        Else
                            objChassisMaster.PenaltyParkingDays = 0
                        End If
                    Else
                        objChassisMaster.PenaltyParkingDays = 0
                    End If
                End If
                If objChassisMaster.PenaltyParkingDays < 0 Then
                    objChassisMaster.PenaltyParkingDays = 0
                End If
                If objChassisMaster.DONumber.Substring(0, 3) = "125" Then
                    objChassisMaster.ParkingDays = 0
                    objChassisMaster.PenaltyParkingDays = 0
                    objChassisMaster.ParkingAmount = 0
                End If

            End If
            dblParkingFeetotal += objChassisMaster.ParkingAmount
            objAl2.Add(objChassisMaster)
        Next
        lblTotalBiayaParkir.Text = dblParkingFeetotal.ToString("#,###")
        Return objAl2

    End Function

    Private Function PenaltyCalculation(ByVal temp As Integer, ByVal objChassisMaster As ChassisMaster) As Long
        Dim iDays1 As Integer = 0
        Dim iDays2 As Integer = 0
        Dim iDays3 As Integer = 0
        Dim iDays4 As Integer = 0
        Dim amount As Long = 0
        Dim temp2 As Integer = 0
        Dim temp21 As Integer = 0
        Dim temp22 As Integer = 0
        Dim temp3 As Integer = 0
        Dim temp4 As Integer = 0

        Dim dt1 As DateTime = New DateTime(2012, 9, 1, 0, 0, 0)
        Dim dt2 As DateTime = New DateTime(2012, 11, 1, 0, 0, 0)
        Dim dt3 As DateTime = New DateTime(2013, 1, 1, 0, 0, 0)

        iDays1 = DateDiff(DateInterval.Day, objChassisMaster.DODate, dt1)
        iDays2 = DateDiff(DateInterval.Day, dt1, dt2)
        iDays3 = DateDiff(DateInterval.Day, dt2, dt3)
        iDays4 = DateDiff(DateInterval.Day, dt3, Today)

        temp2 = temp - iDays1
        temp21 = 20 - iDays1
        temp22 = iDays2 - temp21

        If temp2 >= 0 Then
            If temp2 - iDays2 > 0 Then
                If iDays1 > 0 Then
                    temp2 = iDays2
                Else
                    temp2 = iDays2 + iDays1
                End If
            End If
            'temp21 (20 hari pertama)
            If temp21 > 0 Then
                If objChassisMaster.Category.CategoryCode = "PC" Then
                    If temp21 <= 20 Then
                        amount += temp21 * 10000
                    ElseIf temp21 > 20 And temp21 <= iDays2 Then
                        amount += temp2 * 20000
                    Else
                        amount = 0
                    End If
                ElseIf (objChassisMaster.Category.CategoryCode = "LCV") Or (objChassisMaster.Category.CategoryCode = "CV") Then
                    If temp21 <= 20 Then
                        amount += temp21 * 25000
                    ElseIf temp21 > 20 And temp21 <= iDays2 Then
                        temp21 += temp21 * 40000
                    Else
                        amount += 0
                    End If
                Else
                    amount += 0
                End If
            End If
            If temp22 > 0 Then
                If objChassisMaster.Category.CategoryCode = "PC" Then
                    If temp22 <= 20 Then
                        amount += temp22 * 10000
                    ElseIf temp22 > 20 And temp22 <= iDays2 Then
                        amount += temp22 * 20000
                    Else
                        amount = 0
                    End If
                ElseIf (objChassisMaster.Category.CategoryCode = "LCV") Or (objChassisMaster.Category.CategoryCode = "CV") Then
                    If temp22 <= 20 Then
                        amount += temp22 * 25000
                    ElseIf temp22 > 20 And temp22 <= iDays2 Then
                        amount += temp22 * 40000
                    Else
                        amount += 0
                    End If
                Else
                    amount += 0
                End If
            End If
        Else
            amount += 0
        End If
        If iDays1 < 0 Then
            temp3 = temp - temp2
        Else
            temp3 = temp - iDays1 - temp2
        End If
        If temp3 >= 0 Then
            If temp3 - iDays3 > 0 Then
                temp3 = iDays3
            End If
            If objChassisMaster.Category.CategoryCode = "PC" Then
                If temp3 <= 20 Then
                    amount += temp3 * 10000
                ElseIf temp3 > 20 And temp3 <= iDays3 Then
                    amount += temp3 * 20000
                Else
                    amount += 0
                End If
            ElseIf (objChassisMaster.Category.CategoryCode = "LCV") Or (objChassisMaster.Category.CategoryCode = "CV") Then
                If temp3 <= 20 Then
                    amount += temp3 * 25000
                ElseIf temp3 > 20 And temp3 <= iDays3 Then
                    amount += temp3 * 40000
                Else
                    amount += 0
                End If
            Else
                amount += 0
            End If
        Else
            amount += 0
        End If
        'temp4 = temp - iDays1 - iDays2 - temp2 - temp3
        If iDays1 < 0 Then
            temp4 = temp - temp2 - iDays3
        Else
            temp4 = temp - iDays2 - iDays3 - iDays1
        End If
        If temp4 >= 0 Then
            If temp4 - iDays4 > 0 Then
                temp4 = iDays4
            End If
            If objChassisMaster.Category.CategoryCode = "PC" Then
                If temp4 <= 20 Then
                    amount += temp4 * 10000
                ElseIf temp4 > 20 And temp4 <= iDays4 Then
                    amount += temp4 * 20000
                Else
                    amount = 0
                End If
            ElseIf (objChassisMaster.Category.CategoryCode = "LCV") Or (objChassisMaster.Category.CategoryCode = "CV") Then
                If temp4 <= 20 Then
                    amount += temp4 * 25000
                ElseIf temp4 > 20 And temp4 <= iDays4 Then
                    amount += temp4 * 40000
                Else
                    amount += 0
                End If
            Else
                amount += 0
            End If
        Else
            amount += 0
        End If

        Return amount
    End Function

    Private Sub dgDeliveryOrderBinding(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim RowValue As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)
            'If RowValue.Location.PODestination.ID = 1 Then
            '    Return
            'End If
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            lblID.Text = RowValue.ID
            Dim lblLamaParkir As Label = CType(e.Item.FindControl("lblLamaParkir"), Label)
            Dim lblPenaltyParkir As Label = CType(e.Item.FindControl("lblPenaltyParkir"), Label)
            Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
            Dim lblETD As Label = CType(e.Item.FindControl("lblETD"), Label)
            Dim lblETA As Label = CType(e.Item.FindControl("lblETA"), Label)
            Dim lblATA As Label = CType(e.Item.FindControl("lblATA"), Label)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lblGIDate As Label = CType(e.Item.FindControl("lblGIDate"), Label)
            Dim lblRemarkATA As Label = CType(e.Item.FindControl("lblRemarkATA"), Label)

            lblCategory.Text = RowValue.Category.CategoryCode
            If RowValue.GIDate = "1/1/1900" Then
                e.Item.BackColor = LightSalmon
                lblGIDate.Text = String.Empty
            Else
                lblGIDate.Text = RowValue.GIDate.ToString("dd/MM/yyyy")
            End If
            'Start Remaining Modul
            Dim txtSONumber As TextBox = e.Item.FindControl("txtSONumber")
            If txtSONumber.Text.Trim <> "" Then
                Dim objPOHFac As POHeaderFacade = New POHeaderFacade(User)
                Dim arlPOH As New ArrayList
                Dim crtPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crtPOH.opAnd(New Criteria(GetType(POHeader), "SONumber", MatchType.Exact, txtSONumber.Text))
                crtPOH.opAnd(New Criteria(GetType(POHeader), "RemarkStatus", MatchType.Exact, CType(enumPORemarkStatus.PORemarkStatus.TahanDO, Short)))
                'crtPOH.opAnd(New Criteria(GetType(POHeader), "Remark", MatchType.No, ""))
                arlPOH = objPOHFac.Retrieve(crtPOH)
                If arlPOH.Count > 0 Then
                    e.Item.BackColor = txtColorGreen.BackColor
                    CType(e.Item.FindControl("lblRemarkStatus"), Label).Text = "Tahan DO"
                End If
            End If
            'End Remaining Module

            'ETA ATA
            Dim poH As New POHeader
            Dim critPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critPOH.opAnd(New Criteria(GetType(POHeader), "SONumber", MatchType.Exact, RowValue.SONumber))
            Dim arlPO As New ArrayList
            arlPO = New POHeaderFacade(User).Retrieve(critPOH)
            If (Not IsNothing(RowValue.Location) AndAlso Not IsNothing(RowValue.Location.PODestination) AndAlso RowValue.Location.PODestination.ID <> 1) AndAlso arlPO.Count > 0 Then
                poH = CType(arlPO(0), POHeader)
                lblETD.Text = poH.ReqAllocationDateTime.ToString("dd/MM/yyyy")
                Try
                    If RowValue.GIDate.Year < 1990 Then
                        lblETA.Text = ""
                    Else
                        lblETA.Text = RowValue.GIDate.AddDays(poH.PODestination.LeadTime).ToString("dd/MM/yyyy")
                    End If
                Catch ex As Exception
                    lblETA.Text = ""
                End Try

                Dim cmATA As ChassisMasterATA = New ChassisMasterATAFacade(User).Retrieve(poH.PONumber, RowValue.ChassisNumber)
                If cmATA.ATA.Year > 2000 Then
                    lblATA.Text = cmATA.ATA.ToString("dd/MM/yyyy")
                    lblRemarkATA.Text = cmATA.RemarkATA
                    lnkbtnEdit.Visible = False
                Else
                    lblATA.Text = ""
                    lblRemarkATA.Text = ""
                    lnkbtnEdit.Visible = True
                End If

                If lblGIDate.Text = "" Then
                    lnkbtnEdit.Visible = False
                End If

                If dealerDMS() Then
                    lnkbtnEdit.Visible = False
                End If
                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    lnkbtnEdit.Visible = True
                End If
            Else
                lnkbtnEdit.Visible = False
            End If

            Try
                lblDealer.Text = RowValue.Dealer.DealerCode
                lblDealer.ToolTip = RowValue.Dealer.SearchTerm1

                If RowValue.ParkingDays > 0 Then
                    lblLamaParkir.Text = RowValue.ParkingDays.ToString("#,##0")
                    lblPenaltyParkir.Text = RowValue.PenaltyParkingDays.ToString("#,##0")
                Else
                    lblLamaParkir.Text = "0"
                    lblPenaltyParkir.Text = "0"
                End If
                'lblMaterial.Text = RowValue.VechileColor.MaterialNumber

            Catch ex As Exception
                lblDealer.Text = ""
                lblLamaParkir.Text = ""
                lblPenaltyParkir.Text = ""
            End Try

            Dim lblIndex As Label = CType(e.Item.FindControl("lblIndex"), Label)
            lblIndex.Text = e.Item.ItemIndex + 1 + (dgDeliveryOrder.CurrentPageIndex * dgDeliveryOrder.PageSize)

            'Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            If lnkbtnEdit.Visible Then
                lnkbtnEdit.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpInputATA.aspx?CMid=" & RowValue.ID & "&POid=" & poH.ID & "&ETA=" & RowValue.GIDate.AddDays(poH.PODestination.LeadTime).ToString("dd/MM/yyyy"), "", 250, 400, "PopUpHistory")
            End If

            'Debug gada CMATA
            'lnkbtnEdit.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpInputATA.aspx?CMid=1700154&POid=" & poH.ID & "&ETA=" & RowValue.GIDate.AddDays(poH.PODestination.LeadTime).ToString("dd/MM/yyyy"), "", 250, 400, "PopUpHistory")
            'Debug ada CMATA
            'lnkbtnEdit.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpInputATA.aspx?CMid=1700153&POid=" & poH.ID & "&ETA=" & RowValue.GIDate.AddDays(poH.PODestination.LeadTime).ToString("dd/MM/yyyy"), "", 250, 400, "PopUpHistory")

        End If

    End Sub

    Private Function download() As ArrayList

        Dim strText As StringBuilder
        Dim objAl As New ArrayList
        Dim delimiter As String = Chr(9)
        checkFileExistenceToDownload()

        objAl = CType(Session.Item("chassisMasterAL"), ArrayList)

        'Write Header
        strText = New StringBuilder
        strText.Append("Dealer")
        strText.Append(delimiter)
        strText.Append("No P/O")
        strText.Append(delimiter)
        strText.Append("No D/O")
        strText.Append(delimiter)
        strText.Append("No S/O")
        strText.Append(delimiter)
        strText.Append("Type/Color")
        strText.Append(delimiter)
        strText.Append("No Chassis")
        strText.Append(delimiter)
        strText.Append("No Mesin")
        strText.Append(delimiter)
        strText.Append("No Seri")
        strText.Append(delimiter)
        strText.Append("Tahun Pembuatan")
        strText.Append(delimiter)
        strText.Append("Tgl D/O")
        strText.Append(delimiter)
        strText.Append("Tgl Keluar")
        strText.Append(delimiter)
        strText.Append("Penalty Parkir (hari)")
        strText.Append(delimiter)
        strText.Append("Biaya Parkir")
        strText.Append(delimiter)
        strText.Append("Location")
        strText.Append(delimiter)
        strText.Append("DestinationName")
        strText.Append(delimiter)
        saveToTextFile(strText.ToString())

        For count As Integer = 0 To objAl.Count - 1
            Dim RowValue As ChassisMaster = CType(objAl.Item(count), ChassisMaster)

            strText = New StringBuilder

            Try
                strText.Append(RowValue.Dealer.DealerCode.ToString)
                strText.Append(delimiter)
                strText.Append(RowValue.PONumber.ToString)
                strText.Append(delimiter)
                strText.Append(RowValue.DONumber.ToString)
                strText.Append(delimiter)
                strText.Append(RowValue.SONumber.ToString)
                strText.Append(delimiter)
                strText.Append(RowValue.VechileColor.MaterialNumber)
                strText.Append(delimiter)
                strText.Append(RowValue.ChassisNumber.ToString)
                strText.Append(delimiter)
                strText.Append(RowValue.EngineNumber.ToString)
                strText.Append(delimiter)
                strText.Append(RowValue.SerialNumber.ToString)
                strText.Append(delimiter)
                If RowValue.ProductionYear <= 1900 Then
                    strText.Append("")
                Else
                    strText.Append(RowValue.ProductionYear.ToString())
                End If
                strText.Append(delimiter)
                strText.Append(RowValue.DODate.Date.ToString("dd/MM/yyyy"))
                strText.Append(delimiter)

                'GI date
                If RowValue.GIDate = "1/1/1900" Then
                    strText.Append("")
                Else
                    strText.Append(RowValue.GIDate.Date.ToString("dd/MM/yyyy"))
                End If
                strText.Append(delimiter)

                'penalty parking days
                'If RowValue.GIDate = "1/1/1900" Then
                '    strText.Append("")
                'Else
                '    If (RowValue.ParkingDays - 10) <= 0 Then
                '        strText.Append("0")
                '    Else
                '        strText.Append((RowValue.ParkingDays - 10).ToString("####"))
                '    End If
                'End If
                strText.Append(IIf(RowValue.PenaltyParkingDays = 0, "0", (RowValue.PenaltyParkingDays).ToString("####")))
                strText.Append(delimiter)

                'parking amount
                'If RowValue.GIDate = "1/1/1900" Then
                '    strText.Append("")
                'Else
                '    strText.Append(RowValue.ParkingAmount.ToString("####"))
                'End If
                strText.Append(IIf(RowValue.ParkingAmount = 0, "0", (RowValue.ParkingAmount).ToString("####")))
                strText.Append(delimiter)
                strText.Append(RowValue.Location.Location)
                strText.Append(delimiter)

                If Not IsNothing(RowValue.Location.PODestination) Then
                    strText.Append(RowValue.Location.PODestination.Nama)
                End If
                'strText.Append(RowValue.Category.ToString)
                'strText.Append("  ")
                'strText.Append(RowValue.VechileColor.MaterialNumber.ToString)
                'strText.Append("  ")
                'strText.Append(RowValue.SONumber.ToString)
                'strText.Append("  ")
                'strText.Append(RowValue.FakturStatus.ToString)
                'strText.Append("  ")

                Dim arlLocation As New ArrayList
                Dim strTmpID = RowValue.ID
                Dim objCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterLocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'objCriteria.opAnd(New Criteria(GetType(ChassisMasterLocation), "ChassisMasterID", MatchType.Exact, strTmpID))
                'arlLocation = New ChassisMasterLocationFacade(User).Retrieve(objCriteria)

                'If arlLocation.Count > 0 Then
                '    strText.Append(CType(arlLocation(0), ChassisMasterLocation).Location.ToString)
                'End If

                saveToTextFile(strText.ToString())
            Catch
            End Try
        Next

        Response.Redirect("../downloadlocal.aspx?file=DataTemp\do_status" & suffix & ".txt")

        MessageBox.Show("Data Telah Disimpan")

    End Function

    Private Sub checkFileExistenceToDownload()
        Dim finfo As FileInfo = New FileInfo(Me.Server.MapPath("") & "\..\DataTemp\do_status" & suffix & ".txt")

        If finfo.Exists Then
            finfo.Delete()
        End If

    End Sub

    Private Sub saveToTextFile(ByVal str As String)

        Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\do_status" & suffix & ".txt", FileMode.Append, FileAccess.Write)
        Dim objStreamWriter As New StreamWriter(objFileStream)

        objStreamWriter.WriteLine(str)
        objStreamWriter.Close()

    End Sub

    'Private Sub upload()
    '    Dim chassisMasterAL As New ArrayList

    '    chassisMasterAL = getDataByUploading()

    '    If IsNothing(chassisMasterAL) Then
    '        Throw New Exception("fomat isi file salah")
    '    Else
    '        processChassisMaster(chassisMasterAL)
    '    End If

    'End Sub

    'Private Sub uploadGIDate()
    '    Dim chassisMasterAL As New ArrayList

    '    chassisMasterAL = getDataByUploading()

    '    If IsNothing(chassisMasterAL) Then
    '        MessageBox.Show("Isi Text File Salah")
    '    Else
    '        processGIDate(chassisMasterAL)
    '    End If

    'End Sub

    'Private Function getDataByUploading() As ArrayList


    '    'If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
    '    '    Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)
    '    '    Dim DestFile As String = Path.GetTempPath & SrcFile

    '    '    Dim objUpload As New UploadToWebServer
    '    '    objUpload.Upload(DataFile.PostedFile.InputStream, DestFile)

    '    '    Dim objParser As IParser = New DOStatusListParser

    '    '    Dim objArrayList As ArrayList = CType(objParser.ParseNoTransaction(DestFile, "User"), ArrayList)

    '    '    Dim bError As Boolean = False
    '    '    For Each objChassisMaster As ChassisMaster In objArrayList
    '    '        If Not objChassisMaster.ErrorMessage = "".Trim() Then
    '    '            Return Nothing
    '    '        End If
    '    '    Next

    '    '    Return objArrayList

    '    'Else
    '    '    Throw New Exception("Pilih file yang akan di-upload.")
    '    'End If

    'End Function

    Private Function isExistCode(ByVal sCode As String) As Boolean

        Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
        Return objChassisMasterFacade.ValidateCode(sCode) > 0

    End Function

    Private Sub processChassisMaster(ByVal objAl As ArrayList)

        Try
            For Each objChassisMaster As ChassisMaster In objAl
                If Not isExistCode(objChassisMaster.ChassisNumber) Then
                    insertChassisMaster(objChassisMaster)
                Else
                    updateChassisMaster(objChassisMaster)
                End If
            Next
        Catch ex As Exception
            Throw New Exception("Process Upload Gagal")
        End Try

    End Sub

    Private Sub insertChassisMaster(ByVal objChassisMaster As ChassisMaster)

        Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
        objChassisMasterFacade.Insert(objChassisMaster)

    End Sub

    Private Sub updateChassisMaster(ByVal objChassisMaster As ChassisMaster)

        Dim criterias As CriteriaComposite
        Dim objChassisMaster2 As ChassisMaster
        Dim objAL2 As ArrayList

        Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
        criterias = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, objChassisMaster.ChassisNumber))

        objAL2 = New ChassisMasterFacade(User).Retrieve(criterias)

        If objAL2.Count > 0 Then
            objChassisMaster2 = objAL2(0)
        End If

        objChassisMaster.ID = objChassisMaster2.ID

        objChassisMasterFacade.Update(objChassisMaster)

    End Sub

    Private Sub updateGIDate(ByVal objChassisMaster As ChassisMaster)
        Dim criterias As CriteriaComposite
        Dim objChassisMaster2 As ChassisMaster
        Dim objAL2 As ArrayList

        Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
        criterias = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, objChassisMaster.ChassisNumber))

        objAL2 = New ChassisMasterFacade(User).Retrieve(criterias)

        If objAL2.Count > 0 Then
            objChassisMaster2 = CType(objAL2.Item(0), ChassisMaster)
        End If

        objChassisMaster2.GIDate = objChassisMaster.GIDate
        objChassisMaster2.ParkingDays = objChassisMaster.ParkingDays
        objChassisMaster2.ParkingAmount = objChassisMaster.ParkingAmount

        objChassisMasterFacade.Update(objChassisMaster2)

    End Sub

    Private Sub processGIDate(ByVal chassisMasterAL As ArrayList)

        For Each objChassisMaster As ChassisMaster In chassisMasterAL
            If Not isExistCode(objChassisMaster.ChassisNumber) Then
                MessageBox.Show("Proses Update Tanggal GI Gagal!")
            Else
                updateGIDate(objChassisMaster)
            End If
        Next

    End Sub

    Private Sub assignAttributeControl()
        lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub bindGridSorting(ByVal indexPage As Integer)

        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then

            Dim objChassisMasterAl As ArrayList

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If validateCriteria(criterias) Then

                'Old version cannot sum all data
                'objChassisMasterAl = New ChassisMasterFacade(User).RetrieveActiveList(indexPage + 1, dgDeliveryOrder.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), _
                '  CType(ViewState("currentSortDirection"), Sort.SortDirection), criterias)

                'objChassisMasterAl = validateDisplay(objChassisMasterAl)
                'dgDeliveryOrder.DataSource = objChassisMasterAl
                'dgDeliveryOrder.VirtualItemCount = totalRow

                Dim sortCol As SortCollection = New SortCollection
                sortCol.Add( _
                    New Sort(GetType(ChassisMaster), _
                             CType(ViewState("currentSortColumn"), String), _
                             CType(ViewState("currentSortDirection"), Sort.SortDirection)))
                objChassisMasterAl = New ChassisMasterFacade(User).Retrieve(criterias, sortCol)

                objChassisMasterAl = validateDisplay(objChassisMasterAl)
                dgDeliveryOrder.DataSource = objChassisMasterAl
                'dgDeliveryOrder.VirtualItemCount = totalRow
                dgDeliveryOrder.DataBind()

                Dim objSessionHelper As New SessionHelper
                objSessionHelper.SetSession("chassisMasterAL", objChassisMasterAl)

            End If

        End If

    End Sub

    Private Function seperatePopUpReturn(ByVal sDealerCodeCollumn As String)
        Dim sDealerCodeTemp() As String = sDealerCodeCollumn.Split(New Char() {";"})
        Dim sDealerCode As String = ""
        For i As Integer = 0 To sDealerCodeTemp.Length - 1
            sDealerCode = sDealerCode & "'" & sDealerCodeTemp(i).Trim() & "'"

            If Not (i = sDealerCodeTemp.Length - 1) Then
                sDealerCode = sDealerCode & ","
            End If
        Next
        sDealerCode = "(" & sDealerCode & ")"
        Return sDealerCode
    End Function

    Private Function validateUpdateRowStatus() As ArrayList
        Dim objChassisMaster As ChassisMaster
        Dim objChassisMasterAl As ArrayList
        Dim objCriteria As CriteriaComposite
        Dim objFreeServiceAl As ArrayList
        Dim objPDIAl As ArrayList
        Dim chassisMasterIdAl As New ArrayList
        Dim chkChooseItem As CheckBox
        Dim countCheckedItem As Integer = 0
        Dim lblID As Label
        Dim strMessage As String = String.Empty

        objChassisMasterAl = CType(Session.Item("chassisMasterAL"), ArrayList)

        For count As Integer = 0 To dgDeliveryOrder.Items.Count - 1
            chkChooseItem = CType(dgDeliveryOrder.Items(count).FindControl("chkChooseItem"), CheckBox)
            If chkChooseItem.Checked Then
                countCheckedItem += 1
                lblID = CType(dgDeliveryOrder.Items(count).FindControl("lblID"), Label)
                Dim newID As Integer
                Try
                    newID = CInt(lblID.Text)
                Catch ex As Exception
                    newID = 0
                End Try
                Try
                    objChassisMaster = New ChassisMasterFacade(User).Retrieve(newID)

                    'objChassisMaster = CType(objChassisMasterAl.Item(count), ChassisMaster)
                    'objCriteria = New CriteriaComposite((New Criteria(GetType(FreeService), "ChassisMaster.ID", MatchType.Exact, objChassisMaster.ID)))
                    If Not objChassisMaster Is Nothing Then
                        If objChassisMaster.ID > 0 Then
                            objCriteria = New CriteriaComposite((New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short))))
                            objCriteria.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ID", MatchType.Exact, lblID.Text))
                            objFreeServiceAl = New FreeServiceFacade(User).Retrieve(objCriteria)

                            objCriteria = New CriteriaComposite((New Criteria(GetType(PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short))))
                            objCriteria.opAnd(New Criteria(GetType(PDI), "ChassisMaster.ID", MatchType.Exact, objChassisMaster.ID))
                            objPDIAl = New PDIFacade(User).Retrieve(objCriteria)

                            Dim pass As Boolean = True


                            If objChassisMaster.FakturStatus >= 1 Then
                                pass = False
                                strMessage &= objChassisMaster.ChassisNumber.ToUpper & " - Data sudah dipakai oleh Faktur, hubungi bagian Faktur. \n"
                            End If
                            If objPDIAl.Count > 0 Then
                                'Dim objPDI As PDI = objPDIAl(0)
                                'If objPDI.ChassisMaster.ID > 0 Then
                                pass = False
                                strMessage &= objChassisMaster.ChassisNumber.ToUpper & " - Data sudah dipakai oleh PDI, hubungi bagian Service Adm. untuk proses cancel. \n"
                                'End If
                            End If
                            If objFreeServiceAl.Count > 0 Then
                                'Dim objFS As FreeService = objFreeServiceAl(0)
                                'If objFS.ID > 0 Then
                                pass = False
                                strMessage &= objChassisMaster.ChassisNumber.ToUpper & " - Data sudah dipakai oleh Free Service, hubungi bagian Service Adm. untuk proses cancel. \n"
                                'End If
                            End If
                            Dim objCritPM = New CriteriaComposite((New Criteria(GetType(PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short))))
                            objCritPM.opAnd(New Criteria(GetType(PMHeader), "ChassisMaster.ID", MatchType.Exact, objChassisMaster.ID))
                            Dim objPMAl As ArrayList = New PMHeaderFacade(User).Retrieve(objCritPM)
                            If objPMAl.Count > 0 Then
                                'Dim objPM As PMHeader = objPMAl(0)
                                'If objPM.ID > 0 Then
                                pass = False
                                strMessage &= objChassisMaster.ChassisNumber.ToUpper & " - Data sudah dipakai oleh Periodical Maintenance, hubungi bagian Service Adm. untuk proses cancel. \n"
                                'End If
                            End If

                            Dim objCritPQR = New CriteriaComposite((New Criteria(GetType(PQRHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short))))
                            objCritPQR.opAnd(New Criteria(GetType(PQRHeader), "ChassisMaster.ID", MatchType.Exact, objChassisMaster.ID))
                            Dim objPQRAl As ArrayList = New KTB.DNet.BusinessFacade.PQRHeaderFacade(User).Retrieve(objCritPQR)
                            If objPQRAl.Count > 0 Then
                                'Dim objPQR As PQRHeader = objPQRAl(0)
                                'If objPQR.ID > 0 Then
                                pass = False
                                strMessage &= objChassisMaster.ChassisNumber.ToUpper & " - Data sudah dipakai oleh PQR, hubungi bagian Field Service. \n"
                                'End If
                            End If

                            If pass = True And objFreeServiceAl.Count = 0 And objPDIAl.Count = 0 And objChassisMaster.FakturStatus = 0 Then
                                chassisMasterIdAl.Add(objChassisMaster)
                            End If
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

            End If
        Next
        If strMessage <> String.Empty Then
            MessageBox.Show(strMessage)
        End If

        If countCheckedItem = 0 Then
            Throw New Exception(SR.GridIsEmpty(""))
        Else
            If chassisMasterIdAl.Count <= 0 Then
                'Throw New Exception(SR.CannotDelete)
            Else
                Return chassisMasterIdAl
            End If
        End If

    End Function

    Private Sub updateRowStatus(ByVal objChassisMasterIdAl As ArrayList)

        Dim objChassisMaster As ChassisMaster

        For count As Integer = 0 To objChassisMasterIdAl.Count - 1
            objChassisMaster = CType(objChassisMasterIdAl.Item(count), ChassisMaster)
            Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "ChassisNumber", MatchType.Exact, objChassisMaster.ChassisNumber))

            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
            sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(ChassisMaster), "RowStatus", Sort.SortDirection.ASC))

            Dim ObjLastCM As ArrayList = objChassisMasterFacade.RetrieveByCriteria(criterias, sortColl)

            If Not IsNothing(ObjLastCM) AndAlso ObjLastCM.Count > 0 Then
                objChassisMaster.RowStatus = CType(ObjLastCM(0), ChassisMaster).RowStatus - 1
            End If
            objChassisMasterFacade.Update(objChassisMaster)

            If objChassisMaster.ChassisMasterLocations.Count > 0 Then
                Dim objCMLocation As ChassisMasterLocation
                Dim objCMLocationFacade As ChassisMasterLocationFacade = New ChassisMasterLocationFacade(User)
                objCMLocation = CType(objChassisMaster.ChassisMasterLocations.Item(0), ChassisMasterLocation)
                objCMLocation.RowStatus = DBRowStatus.Deleted
                objCMLocationFacade.Update(objCMLocation)
            End If
        Next

    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            ViewState("currentSortColumn") = "Dealer.DealerCode"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            CheckUserPrivelege()
            assignAttributeControl()
            bindDdlStatus()
        End If
        Dim control As Boolean = String.IsNullorEmpty(Page.Request.Params("__EVENTTARGET"))
        If Not control Then
            If Page.Request.Params("__EVENTTARGET").Contains("lnkbtnEdit") Then
                bindDgDeliveryOrder(0)
            End If
            'If Page.Request.Params("__EVENTTARGET").Contains("dgDeliveryOrder") Then
            '    Return
            'End If
        End If

    End Sub

    Private Sub CheckUserPrivelege()
        If Not SecurityProvider.Authorize(Context.User, SR.ListParkView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Parkir")
        End If
        btnHapus.Visible = SecurityProvider.Authorize(Context.User, SR.ListParkDelete_Privilege)
        btnDownload.Visible = SecurityProvider.Authorize(Context.User, SR.ListParkDowload_Privilege)
    End Sub

    Private Sub bindDdlStatus()
        ddlStatus.Items.Insert(0, New ListItem("Pilih Status", ""))
        ddlStatus.Items.Insert(1, New ListItem("Keluar", "Keluar"))
        ddlStatus.Items.Insert(2, New ListItem("Belum Keluar", "Belum Keluar"))
        'Start Remaining Module
        ddlStatus.Items.Insert(3, New ListItem("Tahan DO", "Tahan DO"))
        'End Remaining Module
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If (Not chkTglCetak.Checked And Not chkTglKeluar.Checked) Then
            MessageBox.Show("Periode tanggal belum dipilih !")
        Else
            bindDgDeliveryOrder(0)
        End If

    End Sub

    Private Sub dgDeliveryOrder_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDeliveryOrder.ItemDataBound
        dgDeliveryOrderBinding(e)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click

        Try
            SetDownload() 'Download Excel
            'download()
            bindDgDeliveryOrder(0)
        Catch ex As Exception
            MessageBox.Show(ex.Message())
        End Try

    End Sub

    Private Sub SetDownload()
        Dim arrData As New ArrayList
        Dim crits As CriteriaComposite
        If dgDeliveryOrder.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If
        Dim sessHelper As New SessionHelper
        'If Not IsNothing(sessHelper.GetSession(criteriadownload)) Then
        '    crits = CType(sessHelper.GetSession(criteriadownload), CriteriaComposite)
        'End If
        '' mengambil data yang dibutuhkan
        'arrData = New ChassisMasterFacade(User).Retrieve(crits)
        arrData = sessHelper.GetSession(criteriadownload)
        If arrData.Count > 0 Then
            CreateExcel("DO STATUS - Daftar Parkir Kendaraan", arrData)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)
            Dim lstH As New List(Of String)
            lstH.Add("Dealer")
            lstH.Add("No P/O")
            lstH.Add("No D/O")
            lstH.Add("No S/O")
            lstH.Add("Type/Color")
            lstH.Add("No Chassis")
            lstH.Add("No Mesin")
            lstH.Add("No Seri")
            lstH.Add("Tahun Pembuatan")
            lstH.Add("Tgl D/O")
            lstH.Add("Estimasi Tanggal Pengiriman")
            lstH.Add("Aktual Tanggal Pengiriman")
            lstH.Add("Estimasi Tanggal Kedatangan")
            lstH.Add("Aktual Tanggal Kedatangan")
            'lstH.Add("Tgl Keluar")
            lstH.Add("Penalty Parkir (hari)")
            lstH.Add("Biaya Parkir")
            lstH.Add("Location")
            lstH.Add("DestinationName")
            lstH.Add("Keterangan Penerimaan")

            'Header
            ws.Cells("A1").Value = FileName
            For ih As Integer = 0 To lstH.Count - 1
                ws.Cells(3, ih + 1).Value = lstH(ih).ToString()
                ws.Column(ih + 1).Width = 100
                ws.Cells(3, ih + 1).Style.Fill.PatternType = ExcelFillStyle.Solid
                ws.Cells(3, ih + 1).Style.Fill.BackgroundColor.SetColor(Color.LightGray)
            Next

            Dim idx As Integer = 3
            'Detail
            For i As Integer = 0 To Data.Count - 1
                idx = idx + 1

                Dim oChassisMaster As ChassisMaster = Data(i)
                ws.Cells(i + 4, 1).Value = oChassisMaster.Dealer.DealerCode
                ws.Cells(i + 4, 2).Value = oChassisMaster.PONumber
                ws.Cells(i + 4, 3).Value = oChassisMaster.DONumber
                ws.Cells(i + 4, 4).Value = oChassisMaster.SONumber
                ws.Cells(i + 4, 5).Value = oChassisMaster.VechileColor.MaterialNumber
                ws.Cells(i + 4, 6).Value = oChassisMaster.ChassisNumber
                ws.Cells(i + 4, 7).Value = oChassisMaster.EngineNumber
                ws.Cells(i + 4, 8).Value = oChassisMaster.SerialNumber
                ws.Cells(i + 4, 9).Value = oChassisMaster.ProductionYear
                ws.Cells(i + 4, 9).Style.Numberformat.Format = "0000"
                ws.Cells(i + 4, 10).Value = oChassisMaster.DODate
                ws.Cells(i + 4, 10).Style.Numberformat.Format = "dd/MM/yyyy"

                'ETA ATA
                Dim poH As New POHeader
                Dim cmATA As ChassisMasterATA
                Dim critPOH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                critPOH.opAnd(New Criteria(GetType(POHeader), "SONumber", MatchType.Exact, oChassisMaster.SONumber))
                Dim arlPO As ArrayList = New POHeaderFacade(User).Retrieve(critPOH)
                If arlPO.Count > 0 Then
                    poH = CType(arlPO(0), POHeader)
                    ws.Cells(i + 4, 11).Value = poH.ReqAllocationDateTime
                    ws.Cells(i + 4, 11).Style.Numberformat.Format = "dd/MM/yyyy"
                    If oChassisMaster.GIDate.Year > 1900 Then
                        ws.Cells(i + 4, 12).Value = oChassisMaster.GIDate
                        ws.Cells(i + 4, 12).Style.Numberformat.Format = "dd/MM/yyyy"
                        Try
                            ws.Cells(i + 4, 13).Value = oChassisMaster.GIDate.AddDays(poH.PODestination.LeadTime)
                        Catch ex As Exception
                            ws.Cells(i + 4, 13).Value = oChassisMaster.GIDate
                        End Try
                        ws.Cells(i + 4, 13).Style.Numberformat.Format = "dd/MM/yyyy"
                    Else
                        ws.Cells(i + 4, 12).Value = ""
                        ws.Cells(i + 4, 13).Value = ""

                    End If

                    cmATA = New ChassisMasterATAFacade(User).Retrieve(poH.PONumber, oChassisMaster.ChassisNumber)

                    Try
                        If cmATA.ID > 0 AndAlso cmATA.ATA.Year > 2000 Then
                            ws.Cells(i + 4, 14).Value = cmATA.ATA
                            ws.Cells(i + 4, 14).Style.Numberformat.Format = "dd/MM/yyyy"
                        Else
                            ws.Cells(i + 4, 14).Value = ""
                        End If
                    Catch ex As Exception
                        ws.Cells(i + 4, 14).Value = ""
                    End Try


                End If

                ws.Cells(i + 4, 15).Value = oChassisMaster.PenaltyParkingDays
                'ws.Cells(i + 4, 15).Style.Numberformat.Format = "0000"

                ws.Cells(i + 4, 16).Value = oChassisMaster.ParkingAmount
                ws.Cells(i + 4, 16).Style.Numberformat.Format = "#,##0"

                Try
                    ws.Cells(i + 4, 17).Value = oChassisMaster.Location.Location
                Catch ex As Exception

                End Try
                Try
                    ws.Cells(i + 4, 18).Value = oChassisMaster.Location.PODestination.Nama
                Catch ex As Exception

                End Try

                Try
                    If cmATA.ID > 0 AndAlso cmATA.ATA.Year > 2000 Then
                        ws.Cells(i + 4, 19).Value = cmATA.RemarkATA
                    Else
                        ws.Cells(i + 4, 19).Value = ""
                    End If
                Catch ex As Exception

                End Try


            Next



            ws.Cells(3, 1, idx, lstH.Count).AutoFitColumns()

            Dim modelTable = ws.Cells(3, 1, idx, lstH.Count)

            '/ Assign borders
            modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin
            modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin
            modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin
            modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin


            CreateExcelFile(pck, FileName.Replace(" ", "_") & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond)
        End Using

    End Sub

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)


        'Dim fileBytes = pck.GetAsByteArray()
        'Response.Clear()

        'Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        'Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=""{0}""; size={1}; creation-date={2}; modification-date={2}; read-date={2}", fileName, fileBytes.Length, DateTime.Now.ToString("R")))
        'Response.ContentType = "application/vnd.ms-excel" 'xls
        'Response.BinaryWrite(fileBytes)
        'Response.Flush()
        'Response.[End]()


        Dim Apendix As String = Guid.NewGuid().ToString()
        Dim fileDownloadName = fileName + Apendix.Substring(0, 4) + ".xlsx"
        Dim contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Dim fileStream As New MemoryStream()

        Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & fileDownloadName

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        If imp.Start() Then
            pck.SaveAs(New FileInfo(Server.MapPath("~/DataTemp/" & fileDownloadName)))
            imp.StopImpersonate()
            imp = Nothing
        Else
            imp = Nothing
            MessageBox.Show(SR.DownloadFail("Report"))
            Return
        End If
        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileDownloadName)

    End Sub

    'Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Try
    '        upload()
    '        MessageBox.Show("Data Berhasil Di Proses")
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    'End Sub

    'Private Sub btnUploadGI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        uploadGIDate()
    '        MessageBox.Show("Data Berhasil Di Proses")
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Private Sub dgDeliveryOrder_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDeliveryOrder.PageIndexChanged
        dgDeliveryOrder.SelectedIndex = -1
        dgDeliveryOrder.CurrentPageIndex = e.NewPageIndex
        bindDgDeliveryOrder(dgDeliveryOrder.CurrentPageIndex)

    End Sub

    Private Sub dgDeliveryOrder_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDeliveryOrder.SortCommand

        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
        End If

        dgDeliveryOrder.SelectedIndex = -1
        dgDeliveryOrder.CurrentPageIndex = 0
        bindGridSorting(dgDeliveryOrder.CurrentPageIndex)

    End Sub

    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        Dim objChassisMasterIdAL As ArrayList

        Try
            objChassisMasterIdAL = validateUpdateRowStatus()
            If Not IsNothing(objChassisMasterIdAL) AndAlso objChassisMasterIdAL.Count > 0 Then
                updateRowStatus(objChassisMasterIdAL)
                MessageBox.Show(SR.DataSuccesCanceled)
                bindDgDeliveryOrder(dgDeliveryOrder.CurrentPageIndex)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub

    Private Function dealerDMS() As Boolean
        Dim crite As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerSystems), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crite.opAnd(New Criteria(GetType(DealerSystems), "SystemID", MatchType.No, 1))
        crite.opAnd(New Criteria(GetType(DealerSystems), "Dealer.ID", MatchType.Exact, objDealer.ID))
        Dim arr As ArrayList = New DealerSystemsFacade(User).Retrieve(crite)
        If arr.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
End Class