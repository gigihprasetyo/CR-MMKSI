Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Data.SqlClient
Imports System.Text

Public Class FrmPendingOrder
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "


    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents cmbDocumentType As DropDownList
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgPendingOrder As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ltrTable As System.Web.UI.WebControls.Literal
    Protected WithEvents dgPendingOrderDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label

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

    Private arlPendingOrder As ArrayList = New ArrayList
    Private arlPendingOrderFilter As ArrayList = New ArrayList
    Private ssHelper As SessionHelper = New SessionHelper
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PendingOrderView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PENDING - SP Pending Order")
        End If
    End Sub

    Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(context.User, SR.PendingOrderViewDetail_Privilege)

    Private Function CekPONOPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PendingOrderViewPONumber_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekSONOPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PendingOrderViewDetailSalesOrder_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Custome Methods"

    Sub BindDropDownList(ByVal ddl As DropDownList, ByVal arrayList As ArrayList)
        ddl.DataTextField = "NameStatus"
        ddl.DataValueField = "ValStatus"
        ddl.DataSource = arrayList
        ddl.DataBind()
    End Sub

    Sub BindOrderType()
        Dim arl As New ArrayList
        Dim oOT As SPPOOrderType = New SPPOOrderType
        arl = oOT.RetrieveOrderType()
        BindDropDownList(ddlOrderType, arl)
        ddlOrderType.Items.Insert(0, New ListItem("Silahkan Pilih", -1))

    End Sub

    Sub BindStatus()
        Dim arl As New ArrayList
        Dim oPS As EnumPendingOrderStatus = New EnumPendingOrderStatus
        arl = oPS.RetrievePendingOrderStatus()
        BindDropDownList(ddlStatus, arl)
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub

    Private Function IsExist(ByVal DealerCode As String, ByVal arl As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As PendingOrder In arl
            If item.Dealer.DealerCode.Trim().ToUpper() = DealerCode.Trim().ToUpper() Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Sub BindPendingOrder()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objDealer As Dealer = ssHelper.GetSession("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (txtKodeDealer.Text.Trim() <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
            End If
        Else
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "Dealer.DealerCode", MatchType.Exact, objDealer.DealerCode))
        End If

        'If Not String.IsNullOrEmpty(cmbDocumentType.SelectedValue) AndAlso cmbDocumentType.SelectedValue <> "-1" Then
        '    criterias.opAnd(New Criteria(GetType(PendingOrder), "SparePartPO.SparePartPOStatus.DocumentType", MatchType.Exact, cmbDocumentType.SelectedValue))
        'End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "IssueDate", MatchType.LesserOrEqual, icDate.Value))

        If (ddlOrderType.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "SparePartPO.OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
        End If

        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "BillingNumber", MatchType.IsNull, EnumPendingOrderStatus.PendingOrderStatus.Pending))
        'If (ddlStatus.SelectedValue <> "-1") Then
        '    If (ddlStatus.SelectedValue = EnumPendingOrderStatus.PendingOrderStatus.Pending) Then
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "BillingNumber", MatchType.IsNull, ddlOrderType.SelectedValue))
        '    ElseIf (ddlStatus.SelectedValue = EnumPendingOrderStatus.PendingOrderStatus.Complete) Then
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "BillingNumber", MatchType.IsNotNull, ddlOrderType.SelectedValue))
        '    End If
        'End If


        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
            sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(PendingOrder), ViewState("currSortColumn"), ViewState("currSortDirection")))
        Else
            sortColl = Nothing
        End If

        arlPendingOrder = New SparePart.PendingOrderFacade(User).Retrieve(criterias, sortColl)

        Dim DealerCode As String = String.Empty
        'For Each item As PendingOrder In arlPendingOrder 'oon

        '    If (Not IsExist(item.Dealer.DealerCode, arlPendingOrderFilter)) Then
        '        arlPendingOrderFilter.Add(item)
        '    End If
        'Next
        Dim documentType As String = cmbDocumentType.SelectedItem.Text
        Dim arlFiltered As New ArrayList
        For Each item As PendingOrder In arlPendingOrder
            If documentType <> "Semua" Then
                If GetDocumentTypeBySONumber(item) = documentType Then
                    If (Not IsExist(item.Dealer.DealerCode, arlPendingOrderFilter)) Then
                        arlPendingOrderFilter.Add(item)
                    End If
                End If
            Else
                If (Not IsExist(item.Dealer.DealerCode, arlPendingOrderFilter)) Then
                    arlPendingOrderFilter.Add(item)
                End If
            End If
        Next

        If (arlPendingOrderFilter.Count > 0) Then
            TablePendingOrder(arlPendingOrderFilter)
        Else
            ltrTable.Text = String.Empty
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub TablePendingOrder(ByVal arl As ArrayList)
        Dim TotalSalesAmount As Decimal = 0
        Dim arlList As ArrayList = New ArrayList
        Dim sb As StringBuilder = New StringBuilder
        Dim i As Integer = 1
        sb.Append("<TABLE style=background-color:#CDCDCD;border-color:Gainsboro;border-width:0px;width:100%; border=0>")
        sb.Append("<TR style=color:#F7F7F7;background-color:#4A3C8C;font-weight:bold;><TD class=titleTableParts>No</TD><TD class=titleTableParts>Kode Dealer</TD><TD class=titleTableParts>Nama Dealer</TD><TD class=titleTableParts>Available Deposit</TD><TD class=titleTableParts>Giro Receive</TD><TD class=titleTableParts>RO</TD><TD class=titleTableParts>Service</TD><TD class=titleTableParts>Total Pending</TD><TD class=titleTableParts>Last Update</TD></TR>")
        For Each item As PendingOrder In arl
            GetDataAndTotalSalesAmount(item.Dealer.DealerCode, TotalSalesAmount, arlList)
            If i Mod 2 <> 0 Then
                sb.Append(String.Format("<TR style=color:Black;background-color:#F1F6FB;><TD align=center><img id=ImgPendingOrder{0} onclick=Collapse('PendingOrder{0}','ImgPendingOrder{0}'); src='../images/plus.gif' border=0 style=height:12px;width:22px; /> {0}</TD><TD align=center>{1}</TD><TD>{2}</TD><TD align=right>{3}</TD><TD align=right>{4}</TD><TD align=right>{5}</TD><TD align=right>{6}</TD><TD align=right>{7}</TD><TD align=center>{8}</TD></TR>", i, item.Dealer.DealerCode, item.Dealer.DealerName & " - " & item.Dealer.City.CityName, item.AvailableDeposit.ToString("#,###"), item.GiroReceive.ToString("#,###"), item.RO.ToString("#,###"), item.Service.ToString("#,###"), TotalSalesAmount.ToString("#,###"), item.LastUpdateTime.ToString("dd/MM/yyyy - HH:mm:ss")))
                sb.Append(String.Format("<tr style=display:none id=PendingOrder{0}><td colspan=8>{1}</td></tr>", i, TablePendingOrderDetail(arlList).ToString()))
            Else
                sb.Append(String.Format("<TR style=color:Black;background-color:White;><TD align=center><img id=ImgPendingOrder{0} onclick=Collapse('PendingOrder{0}','ImgPendingOrder{0}'); src='../images/plus.gif' border=0 style=height:12px;width:22px; /> {0}</TD><TD align=center>{1}</TD><TD>{2}</TD><TD align=right>{3}</TD><TD align=right>{4}</TD><TD align=right>{5}</TD><TD align=right>{6}</TD><TD align=right>{7}</TD><TD align=center>{8}</TD></TR>", i, item.Dealer.DealerCode, item.Dealer.DealerName & " - " & item.Dealer.City.CityName, item.AvailableDeposit.ToString("#,###"), item.GiroReceive.ToString("#,###"), item.RO.ToString("#,###"), item.Service.ToString("#,###"), TotalSalesAmount.ToString("#,###"), item.LastUpdateTime.ToString("dd/MM/yyyy - HH:mm:ss")))
                sb.Append(String.Format("<tr style=display:none id=PendingOrder{0}><td colspan=8>{1}</td></tr>", i, TablePendingOrderDetail(arlList).ToString()))
            End If
            i = i + 1
        Next
        sb.Append("</TABLE>")
        ltrTable.Text = sb.ToString()
    End Sub

    Private Function TablePendingOrderDetail(ByVal arl As ArrayList) As String
        Dim sb As StringBuilder = New StringBuilder
        Dim j As Integer = 0
        Dim OrderType As String
        Dim strLink As String = "<a href=""javascript:ShowPODetail({8});"">{1}</a>"
        Dim strLinkNA As String = "{1}"
        Dim strLinkSO As String = "<a href=""javascript:ShowSODetail({9});"">{2}</a>"
        Dim strLinkSONA As String = "{2}"
        If bCekDetailPriv = False Then
            Return ""
        Else
            sb.Append("<TABLE style=background-color:#CDCDCD;border-color:Gainsboro;border-width:0px;width:100%; border=0>")
            sb.Append("<TR style=color:#F7F7F7;background-color:#4A3C8C;font-weight:bold;><TD class=titleTableParts>Tipe Order</TD><TD class=titleTableParts>Tipe Dokumen</TD><TD class=titleTableParts>PO Number</TD><TD class=titleTableParts>Sales Order</TD><TD class=titleTableParts>Issue Date</TD><TD class=titleTableParts>Sales Amount</TD><TD class=titleTableParts>PPN</TD><TD class=titleTableParts>Deposit C2</TD><TD class=titleTableParts>Sub Total Pending</TD></TR>")
            For Each item As PendingOrder In arl
                If (item.SparePartPO.OrderType = "E") Then
                    OrderType = "Emergency"
                ElseIf (item.SparePartPO.OrderType = "R") Then
                    OrderType = "Regular"
                ElseIf (item.SparePartPO.OrderType = "K") Then
                    OrderType = "Khusus"
                ElseIf (item.SparePartPO.OrderType = "I") Then
                    OrderType = "Indent"
                ElseIf (item.SparePartPO.OrderType = "Z") Then
                    OrderType = "Others Reguler"
                ElseIf (item.SparePartPO.OrderType = "Y") Then
                    OrderType = "Others Emergency"
                End If
                Dim doctype As String = "-"
                doctype = GetDocumentTypeBySONumber(item)

                'For Each liOrderType As ListItem In LookUp.ArraySPDocumentTypeKTBDealer 'tipe dokumen
                '    If Not IsNothing(item.SparePartPO.SparePartPOStatus) Then 'AndAlso item.SparePartPO.SparePartPOStatus.DocumentType.Equals(liOrderType.Value) 
                '        If item.SparePartPO.SparePartPOStatus.DocumentType.Trim = liOrderType.Value.Trim Then
                '            doctype = liOrderType.Text
                '            Exit For
                '        End If
                '    End If
                'Next

                If j Mod 2 <> 0 Then
                    sb.Append(String.Format("<TR style=color:Black;background-color:#F1F6FB;><TD align=center>{0}</TD><TD align=center>" & doctype & "</TD><TD align=center>" & IIf(CekPONOPriv, strLink, strLinkNA) & "</TD><TD align=center>" & IIf(CekSONOPriv, strLinkSO, strLinkSONA) & "</TD><TD align=center>{3}</TD><TD align=right>{4}</TD><TD align=right>{5}</TD><TD align=right>{6}</TD><TD align=right>{7}</TD></TR>", OrderType, item.SparePartPO.PONumber, item.SONumber, item.IssueDate.ToString("dd/MM/yyyy"), item.Retail.ToString("#,###"), item.PPN.ToString("#,###"), item.DepositC2.ToString("#,###"), item.TotalAmount.ToString("#,###"), item.SparePartPO.ID, item.SONumber))
                Else
                    sb.Append(String.Format("<TR style=color:Black;background-color:White;><TD align=center>{0}</TD><TD align=center>" & doctype & "</TD><TD align=center>" & IIf(CekPONOPriv, strLink, strLinkNA) & "</TD><TD align=center>" & IIf(CekSONOPriv, strLinkSO, strLinkSONA) & "</TD><TD align=center>{3}</TD><TD align=right>{4}</TD><TD align=right>{5}</TD><TD align=right>{6}</TD><TD align=right>{7}</TD></TR>", OrderType, item.SparePartPO.PONumber, item.SONumber, item.IssueDate.ToString("dd/MM/yyyy"), item.Retail.ToString("#,###"), item.PPN.ToString("#,###"), item.DepositC2.ToString("#,###"), item.TotalAmount.ToString("#,###"), item.SparePartPO.ID, item.SONumber))
                End If
                j += 1
            Next
            sb.Append("</TABLE>")
            Return sb.ToString()
        End If
    End Function

    Private Function GetDocumentTypeBySONumber(ByVal objPendingOrder As PendingOrder) As String
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOEstimate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOEstimate), "SparePartPO.ID", MatchType.Exact, objPendingOrder.SparePartPO.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOEstimate), "SONumber", MatchType.Exact, objPendingOrder.SONumber))
        Dim arlSPEstimate As ArrayList = New SparePart.SparePartPOEstimateFacade(User).Retrieve(criterias)

        Dim objSPEstimate As New SparePartPOEstimate
        If arlSPEstimate.Count > 0 Then
            objSPEstimate = arlSPEstimate(0)
        End If
        If objSPEstimate.ID > 0 Then
            Return LookUp.GetDocumentTypeDescription(objSPEstimate.DocumentType.Trim)
        Else
            Return "-"
        End If
    End Function


    Private Sub GetDataAndTotalSalesAmount(ByVal DealerCode As String, ByRef TotalSalesAmount As Decimal, ByRef arList As ArrayList)

        Dim arl As ArrayList = New ArrayList
        Dim Total As Decimal = 0
        arList = New ArrayList

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "Dealer.DealerCode", MatchType.Exact, DealerCode))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "IssueDate", MatchType.LesserOrEqual, icDate.Value))

        If (ddlOrderType.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "SparePartPO.OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
        End If
        'If Not String.IsNullOrEmpty(cmbDocumentType.SelectedValue) AndAlso cmbDocumentType.SelectedValue <> "-1" Then
        '    criterias.opAnd(New Criteria(GetType(PendingOrder), "SparePartPO.SparePartPOStatus.DocumentType", MatchType.Exact, cmbDocumentType.SelectedValue))
        'End If

        If (ddlStatus.SelectedValue = EnumPendingOrderStatus.PendingOrderStatus.Pending) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "BillingNumber", MatchType.IsNull, ddlOrderType.SelectedValue))
        ElseIf (ddlStatus.SelectedValue = EnumPendingOrderStatus.PendingOrderStatus.Complete) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PendingOrder), "BillingNumber", MatchType.IsNotNull, ddlOrderType.SelectedValue))
        End If

        arl = New SparePart.PendingOrderFacade(User).Retrieve(criterias, Nothing)

        Dim documentType As String = cmbDocumentType.SelectedItem.Text
        Dim arlFiltered As New ArrayList
        For Each item As PendingOrder In arl
            If documentType <> "Semua" Then
                If GetDocumentTypeBySONumber(item) = documentType Then
                    Total = Total + item.TotalAmount
                    arlFiltered.Add(item)
                End If
            Else
                Total = Total + item.TotalAmount
                arlFiltered.Add(item)
            End If

        Next
        TotalSalesAmount = Total
        arList = arlFiltered
    End Sub

    Private Sub GetDocumentType()
        cmbDocumentType.Items.Add(New ListItem("Semua", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPDocumentTypeKTBDealer
            cmbDocumentType.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        cmbDocumentType.DataBind()
    End Sub
#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        txtKodeDealer.Attributes.Add("readonly", "readonly")
        If Not IsPostBack Then
            ViewState("currSortColumn") = "Dealer.DealerCode"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            Dim objDealer As Dealer = ssHelper.GetSession("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                txtKodeDealer.Visible = True
                lblSearchDealer.Visible = True
                lblDealer.Visible = False
            Else
                txtKodeDealer.Visible = False
                lblSearchDealer.Visible = False
                lblDealer.Visible = True
                lblDealer.Text = objDealer.DealerCode & " - " & objDealer.DealerName
            End If
            BindOrderType()
            BindStatus()
            'BindPendingOrder()
            GetDocumentType()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindPendingOrder()
    End Sub
#End Region

End Class
