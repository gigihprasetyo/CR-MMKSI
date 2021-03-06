#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
#End Region

Public Class FrmPartIncidentalListPO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCodeValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusEmail As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTanggalInput As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents dgPartIncidentalDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents icProcDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icProcDateTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtPartNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents chkProcessDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkPKDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icPKDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private sessHelper As New SessionHelper
#End Region

#Region "PrivateCustomMethods"
    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList
        objSSPO.Add(lblDealerCodeValue.Text)
        objSSPO.Add(txtKodeDealer.Text)
        objSSPO.Add(chkProcessDate.Checked)
        objSSPO.Add(icProcDateFrom.Value)
        objSSPO.Add(icProcDateTo.Value)
        objSSPO.Add(chkPKDate.Checked)
        objSSPO.Add(icPKDate.Value)
        objSSPO.Add(txtPartNumber.Text)
        objSSPO.Add(txtPONumber.Text)
        objSSPO.Add(dgPartIncidentalDetail.CurrentPageIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessHelper.SetSession("SESSIONEQLIST", objSSPO)
    End Sub
    Private Sub GetSessionCriteria()
        Dim objSSPO As ArrayList = sessHelper.GetSession("SESSIONEQLIST")
        If Not objSSPO Is Nothing Then
            lblDealerCodeValue.Text = objSSPO.Item(0)
            txtKodeDealer.Text = objSSPO.Item(1)
            chkProcessDate.Checked = objSSPO.Item(2)
            icProcDateFrom.Value = objSSPO.Item(3)
            icProcDateTo.Value = objSSPO.Item(4)
            chkPKDate.Checked = objSSPO.Item(5)
            icPKDate.Value = objSSPO.Item(6)
            txtPartNumber.Text = objSSPO.Item(7)
            txtPONumber.Text = objSSPO.Item(8)
            dgPartIncidentalDetail.CurrentPageIndex = objSSPO.Item(9)
            ViewState("CurrentSortColumn") = objSSPO.Item(10)
            ViewState("CurrentSortDirect") = objSSPO.Item(11)
            BindDataGrid(0)
        End If
    End Sub
    Private Function IsPORelated(ByVal ObjPID As PartIncidentalDetail, ByVal PONumber As String) As Boolean
        Dim retValue As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PartIncidentalPO), "PartIncidentalDetail.ID", MatchType.Exact, ObjPID.ID))
        If PONumber.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(PartIncidentalPO), "PONumber", MatchType.[Partial], PONumber))
        End If
        Dim arrList As ArrayList = New PartIncidentalPOFacade(User).Retrieve(criterias)
        If arrList.Count > 0 Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function
    Private Function IsProcesDateRelated(ByVal ObjPID As PartIncidentalDetail, ByVal dateFrom As Date, ByVal dateTo As Date) As Boolean
        Dim retValue As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PartIncidentalPO), "PartIncidentalDetail.ID", MatchType.Exact, ObjPID.ID))
        criterias.opAnd(New Criteria(GetType(PartIncidentalPO), "ProcessDate", MatchType.GreaterOrEqual, Format(dateFrom, "yyyy-MM-dd HH:mm:ss")))
        criterias.opAnd(New Criteria(GetType(PartIncidentalPO), "ProcessDate", MatchType.LesserOrEqual, Format(dateTo, "yyyy-MM-dd HH:mm:ss")))
        Dim arrList As ArrayList = New PartIncidentalPOFacade(User).Retrieve(criterias)
        If arrList.Count > 0 Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function
    'Private Sub BindDataGrid(ByVal idx As Integer)
    '    'Dim obj As New PartIncidentalDetail
    '    'obj.PartIncidentalHeader.IncidentalDate()

    '    Dim totalRow As Integer = 0
    '    Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

    '    If lblDealerCodeValue.Visible Then
    '        criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), "PartIncidentalHeader.Dealer.DealerCode", _
    '        MatchType.Exact, lblDealerCode.Text))
    '    Else
    '        If txtKodeDealer.Text.Length > 0 Then
    '            criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), _
    '            "PartIncidentalHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
    '        End If
    '    End If

    '    If chkPKDate.Checked Then '--Create New Calendar
    '        Dim tglFrom As New Date(icProcDateFrom.Value.Year, icProcDateFrom.Value.Month, icProcDateFrom.Value.Day, 0, 0, 0)
    '        Dim tglTo As New Date(icProcDateTo.Value.Year, icProcDateTo.Value.Month, icProcDateTo.Value.Day, 23, 59, 59)
    '        criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), "PartIncidentalHeader.IncidentalDate", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")))
    '        criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), "PartIncidentalHeader.IncidentalDate", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")))
    '    End If

    '    If txtPartNumber.Text.Length > 0 Then
    '        criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), "PartIncidentalHeader.RequestNumber", MatchType.Partial, txtPartNumber.Text.Trim()))
    '    End If

    '    Dim arrList As New ArrayList
    '    Dim arrListDetail As New ArrayList
    '    Dim arrListFinal As New ArrayList
    '    arrListDetail = New PartIncidentalDetailFacade(User).RetrieveByCriteria(criterias, idx + 1, _
    '        dgPartIncidentalDetail.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
    '        CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

    '    'And CType(icProcDateFrom.Value, Date) <= CType(icProcDateTo.Value, Date) 
    '    If chkProcessDate.Checked Then '--Create New Calendar
    '        Dim tglProcFrom As New Date(icPKDate.Value.Year, icPKDate.Value.Month, icPKDate.Value.Day, 0, 0, 0)
    '        Dim tglProcTo As New Date(icPKDate.Value.Year, icPKDate.Value.Month, icPKDate.Value.Day, 23, 59, 59)
    '        For Each ObjPIDetail As PartIncidentalDetail In arrListDetail
    '            If IsProcesDateRelated(ObjPIDetail, tglProcFrom, tglProcTo) Then
    '                arrList.Add(ObjPIDetail)
    '            End If
    '        Next
    '    Else
    '        arrList = arrListDetail
    '    End If

    '    For Each ObjPIDetail As PartIncidentalDetail In arrList
    '        If IsPORelated(ObjPIDetail, txtPONumber.Text.Trim()) Then
    '            arrListFinal.Add(ObjPIDetail)
    '        End If
    '    Next

    '    dgPartIncidentalDetail.DataSource = arrListFinal
    '    dgPartIncidentalDetail.VirtualItemCount = arrListFinal.Count
    '    dgPartIncidentalDetail.DataBind()
    'End Sub
    Private Sub BindDataGrid(ByVal idx As Integer)
        'Dim obj As New PartIncidentalDetail
        'obj.PartIncidentalHeader.IncidentalDate()

        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If lblDealerCodeValue.Visible Then
            criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), "PartIncidentalHeader.Dealer.DealerCode", _
            MatchType.Exact, lblDealerCodeValue.Text))
        Else
            If txtKodeDealer.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), _
                "PartIncidentalHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
            End If
        End If

        If chkPKDate.Checked Then '--Create New Calendar
            Dim tglFrom As New Date(icPKDate.Value.Year, icPKDate.Value.Month, icPKDate.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icPKDate.Value.Year, icPKDate.Value.Month, icPKDate.Value.Day, 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), "PartIncidentalHeader.IncidentalDate", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), "PartIncidentalHeader.IncidentalDate", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")))
        End If

        If txtPartNumber.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), "SparePartMaster.PartNumber", MatchType.[Partial], txtPartNumber.Text.Trim()))
        End If

        Dim arrList As New ArrayList
        Dim arrListDetail As New ArrayList
        Dim arrListFinal As New ArrayList
        Dim arrListPaging As New ArrayList

        'arrListDetail = New PartIncidentalDetailFacade(User).RetrieveByCriteria(criterias, idx + 1, _
        '    dgPartIncidentalDetail.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
        '    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        ''And CType(icProcDateFrom.Value, Date) <= CType(icProcDateTo.Value, Date) 
        'If chkProcessDate.Checked Then '--Create New Calendar
        '    Dim tglProcFrom As New Date(icProcDateFrom.Value.Year, icProcDateFrom.Value.Month, icProcDateFrom.Value.Day, 0, 0, 0)
        '    Dim tglProcTo As New Date(icProcDateTo.Value.Year, icProcDateTo.Value.Month, icProcDateTo.Value.Day, 23, 59, 59)
        '    For Each ObjPIDetail As PartIncidentalDetail In arrListDetail
        '        If IsProcesDateRelated(ObjPIDetail, tglProcFrom, tglProcTo) Then
        '            arrList.Add(ObjPIDetail)
        '        End If
        '    Next
        'Else
        '    arrList = arrListDetail
        'End If


        'For Each ObjPIDetail As PartIncidentalDetail In arrList
        '    If IsPORelated(ObjPIDetail, txtPONumber.Text.Trim()) Then
        '        arrListFinal.Add(ObjPIDetail)
        '    End If
        'Next
        'And CType(icProcDateFrom.Value, Date) <= CType(icProcDateTo.Value, Date) 


        Dim strFilterDate As String = ""
        If chkProcessDate.Checked Then '--Create New Calendar
            Dim tglProcFrom As New Date(icProcDateFrom.Value.Year, icProcDateFrom.Value.Month, icProcDateFrom.Value.Day, 0, 0, 0)
            Dim tglProcTo As New Date(icProcDateTo.Value.Year, icProcDateTo.Value.Month, icProcDateTo.Value.Day, 23, 59, 59)
            strFilterDate = " where ProcessDate>= '" & icProcDateFrom.Value.ToString("yyyy-MM-dd") & "' and ProcessDate<'" & icProcDateTo.Value.AddDays(1).ToString("yyyy-MM-dd") & "'"

        End If

        criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), "ID", MatchType.InSet, "(Select PartIncidentalDetailID from PartIncidentalPO " & strFilterDate & " )"))

        If txtPONumber.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(PartIncidentalDetail), "ID", MatchType.InSet, "(Select PartIncidentalDetailID from PartIncidentalPO where PONumber like '%" & txtPONumber.Text.Trim & "%')"))

        End If

        arrListPaging = New PartIncidentalDetailFacade(User).RetrieveByCriteria(criterias, idx + 1, _
    dgPartIncidentalDetail.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        arrListFinal = New PartIncidentalDetailFacade(User).RetrieveByCriteria(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))


        'Dim intervalAwal As Integer = 0 + (idx * dgPartIncidentalDetail.PageSize)
        'Dim intervalAkhir As Integer = intervalAwal + dgPartIncidentalDetail.PageSize - 1

        'For i As Integer = intervalAwal To intervalAkhir
        '    If i <= arrListFinal.Count - 1 Then
        '        arrListPaging.Add(arrListFinal(i))
        '    Else
        '        Exit For
        '    End If
        'Next i
        Dim sessionhelper As New sessionhelper
        sessionhelper.SetSession("arlDownloadable", arrListFinal)
        dgPartIncidentalDetail.DataSource = arrListPaging
        dgPartIncidentalDetail.VirtualItemCount = totalRow
        dgPartIncidentalDetail.CurrentPageIndex = idx
        dgPartIncidentalDetail.DataBind()
    End Sub
    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = PartIncidentalStatus.RetrievePartIncidentalStatus
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataBind()
    End Sub
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ENHSPViewPOPesananKhusus_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar PO Permintaan Khusus")
        End If
    End Sub
    Private Sub Initialize()
        ViewState("CurrentSortColumn") = "SparePartMaster.AltPartNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        Dim ObjDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If ObjDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblDealerCodeValue.Visible = True
            txtKodeDealer.Visible = False
            lblSearchDealer.Visible = False
            lblDealerCodeValue.Text = ObjDealer.DealerCode
            txtKodeDealer.Text = ""
        Else
            lblDealerCodeValue.Visible = False
            txtKodeDealer.Visible = True
            lblSearchDealer.Visible = True
            lblDealerCodeValue.Text = ""
            txtKodeDealer.Text = ""
        End If
        Dim dt As Date = Now.AddDays(-7)
        icProcDateFrom.Value = dt
        icProcDateTo.Value = DateTime.Now
        icPKDate.Value = DateTime.Now
        txtPartNumber.Text = ""
        'bind status di sini
        txtPONumber.Text = ""
    End Sub
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
            GetSessionCriteria()
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
    End Sub
    Private Sub dgPartIncidentalDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartIncidentalDetail.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objPartIncidentalDetail As PartIncidentalDetail = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgPartIncidentalDetail.CurrentPageIndex * dgPartIncidentalDetail.PageSize)
            Dim dealerCode As String = objPartIncidentalDetail.PartIncidentalHeader.Dealer.DealerCode
            Dim reqNumber As String = objPartIncidentalDetail.PartIncidentalHeader.RequestNumber
            Dim tglPesanan As Date = objPartIncidentalDetail.PartIncidentalHeader.IncidentalDate
            Dim tglProses As String

            e.Item.Cells(2).Text = dealerCode
            e.Item.Cells(3).Text = reqNumber
            Dim strPO As String = String.Empty
            For Each item As PartIncidentalPO In objPartIncidentalDetail.PartIncidentalPOs
                If strPO = String.Empty Then
                    strPO = item.PONumber
                    tglProses = item.ProcessDate
                Else
                    strPO += "<br>" & item.PONumber
                    If tglProses = String.Empty Then
                        tglProses = item.ProcessDate.ToString("dd/MM/yyyy")
                    Else
                        tglProses += "<br>" & item.ProcessDate.ToString("dd/MM/yyyy")
                    End If
                End If
            Next

            e.Item.Cells(4).Text = strPO
            e.Item.Cells(5).Text = tglPesanan
            e.Item.Cells(6).Text = tglProses

            e.Item.Cells(8).Text = CType(e.Item.DataItem, PartIncidentalDetail).SparePartMaster.PartNumber
            e.Item.Cells(9).Text = CType(e.Item.DataItem, PartIncidentalDetail).SparePartMaster.PartName
            e.Item.Cells(10).Text = CType(e.Item.DataItem, PartIncidentalDetail).AlocatedQuantity
        End If
    End Sub
    Private Sub dgPartIncidentalDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPartIncidentalDetail.ItemCommand
        If e.CommandName = "Detail" Then
            sessHelper.SetSession("IDPartIncidentalDetail", CInt(e.Item.Cells(0).Text))
            SetSessionCriteria()
            Response.Redirect("FrmPartIncidentalDetailPO.aspx")
        End If
    End Sub
    Private Sub dgPartIncidentalDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPartIncidentalDetail.PageIndexChanged
        dgPartIncidentalDetail.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgPartIncidentalDetail.CurrentPageIndex)
    End Sub
    Private Sub dgPartIncidentalDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPartIncidentalDetail.SortCommand
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
        dgPartIncidentalDetail.SelectedIndex = -1
        dgPartIncidentalDetail.CurrentPageIndex = 0
        BindDataGrid(dgPartIncidentalDetail.CurrentPageIndex)
    End Sub
#End Region

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Response.Redirect("frmDownloadPartIncidentalPOHeader.aspx")
    End Sub
End Class
