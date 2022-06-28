#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmSPPendingOrder
    Inherits System.Web.UI.Page


#Region "Custom Variable Declaration"
    Private nDealerID As Integer
    Private sessHelper As SessionHelper = New SessionHelper
    Private ArrList As ArrayList = New ArrayList
    Private totalRow As Integer = 0
    Private _isShowDetailAllowed As Boolean = False

    Private _sessData As String = "FrmSPPendingOrder.Data"
#End Region

#Region "Custom Method"

    

    Private Sub GetDealer()
        'Dim objDealer As Dealer = New DealerFacade(User).Retrieve(nDealerID)
        If Not IsNothing(Session("DEALER")) Then
            sessHelper.SetSession("sesDealer", Session("DEALER"))
        Else
            'Response.Redirect("../SessionExpired.htm")
        End If
    End Sub

    Private Sub GetOrderType()
        cmbOrderTye.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer 'LookUp.ArraySPOrderType
            cmbOrderTye.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        cmbOrderTye.DataBind()
    End Sub

    Private Sub GetDocumentType()
        cmbDocumentType.Items.Add(New ListItem("Semua", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPDocumentTypeKTBDealer 'LookUp.ArraySPOrderType
            cmbDocumentType.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
        cmbDocumentType.DataBind()
    End Sub
    Private Sub RetrieveHeader()
        GetDealer()
        Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblDealerCode.Text = objDealer.DealerCode
            lblDealerName.Text = objDealer.DealerName
            lblDealerTerm.Text = objDealer.SearchTerm2
        Else
            lblDealerCode.Text = ""
            lblDealerName.Text = ""
            lblDealerTerm.Text = ""
        End If
        GetOrderType()
        GetDocumentType()
        'cmdDownload.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpDownloadEstimate.aspx?term2=" & objDealer.SearchTerm2.ToUpper.Trim & "','',400,400,Estimate);")
    End Sub

    Private Sub RetrieveDetails(ByVal pageIndex As Integer)
        If ccPODateEnd.Value >= ccPODateStart.Value Then
            If txtDealerCode.Text.Trim <> "" Then
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim)
                lblDealerName.Text = objDealer.DealerName
                lblDealerTerm.Text = objDealer.SearchTerm2
                'Else
                '    lblDealerName.Text = String.Empty
                '    lblDealerTerm.Text = String.Empty
            End If
            FindData(pageIndex)
            If ArrList.Count > 0 Then
                dgPOEstimate.DataSource = ArrList
                dgPOEstimate.VirtualItemCount = totalRow
                dgPOEstimate.DataBind()
            Else
                dgPOEstimate.DataSource = New ArrayList
                dgPOEstimate.VirtualItemCount = 0
                dgPOEstimate.DataBind()
                If IsPostBack Then
                    MessageBox.Show(SR.DataNotFound("Data"))
                End If
            End If
        Else
            MessageBox.Show(SR.InvalidRangeDate)
        End If
    End Sub

    Private Sub FindData(ByVal pageIndex As Integer)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPendingOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim org As Dealer = CType(Session("DEALER"), Dealer)

        If org.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Dim objDealer As Dealer = CType(Session("sesDealer"), Dealer)
            criterias.opAnd(New Criteria(GetType(SparePartPendingOrder), "SparePartPO.Dealer.ID", MatchType.Exact, objDealer.ID))
        ElseIf (Not (String.IsNullOrEmpty(txtDealerCode.Text.Replace(";", "','")))) Then
            criterias.opAnd(New Criteria(GetType(SparePartPendingOrder), "SparePartPO.Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))

        End If
        If txtNomorPesanan.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SparePartPendingOrder), "SparePartPO.PONumber", MatchType.[Partial], txtNomorPesanan.Text.Trim))
        End If


        If cmbOrderTye.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartPendingOrder), "SparePartPO.OrderType", MatchType.Exact, cmbOrderTye.SelectedValue))
        If cmbDocumentType.SelectedValue <> "-1" Then criterias.opAnd(New Criteria(GetType(SparePartPendingOrder), "DocumentType", MatchType.Exact, cmbDocumentType.SelectedValue))
        criterias.opAnd(New Criteria(GetType(SparePartPendingOrder), "SparePartPO.PODate", MatchType.GreaterOrEqual, Format(ccPODateStart.Value, "yyyy/MM/dd")))
        criterias.opAnd(New Criteria(GetType(SparePartPendingOrder), "SparePartPO.PODate", MatchType.LesserOrEqual, Format(ccPODateEnd.Value, "yyyy/MM/dd")))

        ArrList = New SparePartPendingOrderFacade(User).RetrieveActiveList(pageIndex, dgPOEstimate.PageSize, totalRow, sessHelper.GetSession("SortCol"), sessHelper.GetSession("SortDirection"), criterias)
        Me.sessHelper.SetSession(Me._sessData, ArrList)



    End Sub

    Private Function CalculatePOEstimateAmount(ByVal arlPODetail As ArrayList) As Decimal
        Dim nPOAmount As Decimal = 0
        Return (nPOAmount)
    End Function



    Private Function GetNumericOnly(ByVal OriMoney As Decimal) As String
        Return FormatNumber(OriMoney, 0, TriState.False, TriState.UseDefault, TriState.False)
    End Function



#End Region

#Region "EventHandler"

    Private Sub InitiateAuthorization()

        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If org.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewSPPO_Status_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Estimasi Pesanan")
            End If
            '--exclude  this privilege from Asra (BA)
            _isShowDetailAllowed = SecurityProvider.Authorize(Context.User, SR.ViewSPPO_StatusDetail_Privilege)
            
            txtDealerCode.Visible = False
            lblSearchDealer.Visible = False
            lblDealerCode.Visible = True
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.ENHStatusPemesananKTB_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Estimasi Pesanan")
            End If

            txtDealerCode.Visible = True
            lblSearchDealer.Visible = True
            lblDealerCode.Visible = False
        End If


    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        'Put user code to initialize the page here
        If Not IsPostBack Then
            sessHelper.SetSession("SortCol", "CreatedTime")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)

            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            btnGetDealer.Style("display") = "none"
            RetrieveHeader()
            RetrieveDetails(1)
            Dim org As Dealer = CType(Session("DEALER"), Dealer)
            
        End If

    End Sub

    Private Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Dim org As Dealer = CType(Session("DEALER"), Dealer)
        If (txtDealerCode.Text.Trim <> "") Then
            sessHelper.SetSession("SortCol", "CreatedTime")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
        Else

            sessHelper.SetSession("SortCol", "SparePartPO.Dealer.DealerCode")
            sessHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
        End If
        dgPOEstimate.CurrentPageIndex = 0
        RetrieveDetails(1)

    End Sub

    Private Sub dgPOEstimate_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPOEstimate.ItemDataBound
        Dim objPOPendingHeader As SparePartPendingOrder

        If e.Item.ItemIndex > -1 Then
            objPOPendingHeader = CType(ArrList(e.Item.ItemIndex), SparePartPendingOrder)

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgPOEstimate.PageSize * dgPOEstimate.CurrentPageIndex)
            e.Item.Cells(2).Text = objPOPendingHeader.SparePartPO.Dealer.DealerCode

            For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer 'LookUp.ArraySPOrderType
                If objPOPendingHeader.SparePartPO.OrderType.Equals(liOrderType.Value) Then
                    e.Item.Cells(3).Text = liOrderType.Text
                    Exit For
                End If
            Next
            For Each liOrderType As ListItem In LookUp.ArraySPDocumentTypeKTBDealer 'LookUp.ArraySPOrderType
                If objPOPendingHeader.DocumentType.Equals(liOrderType.Value) Then
                    e.Item.Cells(4).Text = liOrderType.Text
                    Exit For
                End If
            Next
            CType(e.Item.FindControl("lblPONo"), LinkButton).Attributes("href") = "javascript:{var x =" + GeneralScript.GetPopUpEventReference("../PopUp/PopUpSPPODetail.aspx?poid=" + objPOPendingHeader.SparePartPO.ID.ToString() + "", "", 510, 700, "SparePartPO") + "}"
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            SetDGEstimateDetailItem(e)
        End If
    End Sub

    Private Sub SetDGEstimateDetailItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        
    End Sub

    Private Sub dgPOEstimate_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPOEstimate.PageIndexChanged
        dgPOEstimate.CurrentPageIndex = e.NewPageIndex
        RetrieveDetails(e.NewPageIndex + 1)
    End Sub

    Private Sub dgPOEstimate_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPOEstimate.ItemCommand
        
    End Sub

    Private Sub btnGetDealer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetDealer.Click
        If txtDealerCode.Text.Length > 0 Then
            Dim ObjDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
            lblDealerName.Text = ObjDealer.DealerName
            lblDealerTerm.Text = ObjDealer.SearchTerm2
        End If
    End Sub

#End Region

    Private Sub dgPOEstimate_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPOEstimate.SortCommand
        If e.SortExpression = sessHelper.GetSession("SortCol") Then
            If sessHelper.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sessHelper.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sessHelper.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sessHelper.SetSession("SortCol", e.SortExpression)

        dgPOEstimate.SelectedIndex = -1
        dgPOEstimate.CurrentPageIndex = 0
        RetrieveDetails(0)
    End Sub


End Class