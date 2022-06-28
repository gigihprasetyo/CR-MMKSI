Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Imports System.IO
Imports System.Text
Imports System.Configuration

Public Class FrmDealerStock
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgDealerStock As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents pnlStockUpdate As System.Web.UI.WebControls.Panel
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents pnlRecap As System.Web.UI.WebControls.Panel
    Protected WithEvents txtPeriode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlArea As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variables Declaration"

    Dim TotalUnit As Integer = 0
    Dim sessHelper As New SessionHelper
    Private bDownloadPriv As Boolean
#End Region

#Region "Custom Methods"
#Region "Check Privilage"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.MontlyReportListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=LAPORAN - Rekap Bulanan")
        End If

        bDownloadPriv = SecurityProvider.Authorize(context.User, SR.MontlyReportListDownload_Privilege)
    End Sub

#End Region

    Sub BindCategory()
        Dim arl As New ArrayList
        arl = New FinishUnit.CategoryFacade(User).RetrieveActiveList()
        ddlCategory.DataSource = arl
        ddlCategory.DataTextField = "Description"
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Sub BindType()
        Dim arl As New ArrayList
        Dim crits As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))

        arl = New FinishUnit.VechileTypeFacade(User).Retrieve(crits)
        ddlType.DataSource = arl
        ddlType.DataTextField = "Description"
        ddlType.DataValueField = "ID"
        ddlType.DataBind()
        ddlType.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Sub SetPanelAndGridKTB(ByVal isKTB As Boolean)
        'pnlStockUpdate.Visible = Not isKTB
        '       pnlRecap.Visible = isKTB
        'lblDealer.Visible = Not isKTB
        'txtKodeDealer.Visible = isKTB
        'lblSearchDealer.Visible = isKTB
        dgDealerStock.Columns(1).Visible = isKTB
    End Sub

    Sub KTBData()
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub
    Private Function IsExistForKTB(ByVal VCMaterialNumber As String, ByVal arl As ArrayList, ByVal HeaderID As String) As Boolean
        Dim bResult As Boolean = False
        For Each item As DealerStockReportDetail In arl
            If (item.VechileColor.MaterialNumber.Trim() = VCMaterialNumber) And (item.DealerStockReportHeader.ID = HeaderID) Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function

    Private Function GetTotalUnit(ByVal ID As String, ByVal VCID As String) As Integer
        Dim arl As New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "DealerStockReportHeader.ID", MatchType.Exact, ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "VechileColor.ID", MatchType.Exact, VCID))
        arl = New DealerReport.DealerStockReportDetailFacade(User).Retrieve(criterias)
        Return arl.Count
    End Function
    Private Function IsExist(ByVal VCMaterialNumber As String, ByVal arl As ArrayList) As Boolean
        Dim bResult As Boolean = False
        For Each item As DealerStockReportDetail In arl
            If item.VechileColor.MaterialNumber.Trim() = VCMaterialNumber Then
                bResult = True
                Exit For
            End If
        Next
        Return bResult
    End Function
    Sub BindDataDealer(ByVal ID As String)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "DealerStockReportHeader.ID", MatchType.Exact, ID))

        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DealerStockReportDetail), "VechileColor.ID", Sort.SortDirection.ASC))

        Dim arlDetail As ArrayList = New ArrayList
        Dim arlDetailFilter As ArrayList = New ArrayList
        arlDetail = New DealerReport.DealerStockReportDetailFacade(User).Retrieve(criterias, sortColl)

        Dim VCID As Integer = 0
        For Each item As DealerStockReportDetail In arlDetail
            If (Not IsExist(item.VechileColor.MaterialNumber.Trim(), arlDetailFilter)) Then
                arlDetailFilter.Add(item)
            End If
        Next

        If (arlDetailFilter.Count > 0) Then
            sessHelper.SetSession("StockDealer", arlDetailFilter)
            'Session("StockDealer") = arlDetailFilter
            dgDealerStock.Visible = True
            dgDealerStock.DataSource = arlDetailFilter
            dgDealerStock.DataBind()
        Else
            dgDealerStock.Visible = False
            If bDownloadPriv Then
                btnDownload.Visible = False
            Else
                btnDownload.Visible = bDownloadPriv
            End If
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub
    Sub DealerData(ByVal oD As Dealer)
        lblDealer.Text = oD.DealerCode & " / " & oD.DealerName

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "Dealer.ID", MatchType.Exact, oD.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "PeriodMonth", MatchType.Exact, Date.Now.Month))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "PeriodYear", MatchType.Exact, Date.Now.Year))

        Dim arl As New ArrayList
        arl = New DealerReport.DealerStockReportHeaderFacade(User).Retrieve(criterias)

        If arl.Count > 0 Then
            'Dim oDS As DealerStockReportHeader = CType(arl(0), DealerStockReportHeader)

            'BindDataDealer(oDS.ID)
            sessHelper.SetSession("StockDealer", arl)
            'Session("StockDealer") = arl
            If bDownloadPriv Then
                btnDownload.Visible = True
            Else
                btnDownload.Visible = bDownloadPriv
            End If
            dgDealerStock.Visible = True
            dgDealerStock.DataSource = arl
            dgDealerStock.DataBind()

        Else

            'MessageBox.Show("Dealer Stok tidak ditemukan")
            MessageBox.Show("Data tidak ditemukan")
        End If

    End Sub

    Sub SearchForKTB(ByVal indexPage As Integer)
        Dim arlDealerTemp As New ArrayList
        Dim totalRow As Integer

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "VechileType.Status", MatchType.Exact, "A"))

        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            'kondisi
            'kode dealer kosong
            If txtKodeDealer.Text = String.Empty Then
                'area diisi
                Dim dealerArea As String
                If ddlArea.SelectedIndex <> 0 Then
                    Dim objDealerTemp As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
                    Dim dealerGroupID As Integer = objdealertemp.DealerGroup.ID
                    'get the dealer at that dealergroupid
                    Dim crits As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, Short)))
                    crits.opAnd(New Criteria(GetType(Dealer), "DealerGroup.ID", MatchType.Exact, dealerGroupID))
                    Dim arlList As ArrayList = New DealerFacade(User).Retrieve(crits)
                    For Each item As Dealer In arlList
                        arlDealerTemp.Add(item)
                    Next

                    'cek dealer berdasarkan area
                    Dim objArea As Area1 = New Area1Facade(User).Retrieve(CInt(ddlArea.SelectedValue))
                    If objarea.Dealers.Count > 0 Then
                        Dim i As Integer = 0
                        For Each item As Dealer In arlDealerTemp
                            If i = objarea.Dealers.Count Then
                                Exit For
                            End If
                            Dim objDealerCek As Dealer = objArea.Dealers(i)
                            If item.DealerCode.Trim = objDealerCek.DealerCode.Trim Then
                                dealerArea = dealerArea & item.DealerCode & ";"
                                i = i + 1
                            End If
                        Next
                    End If
                    If dealerArea <> "" Then
                        dealerArea = Left(dealerArea, dealerArea.Length - 1)
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "Dealer.DealerCode", MatchType.InSet, "('" & dealerArea.Replace(";", "','") & "')"))
                    End If
                End If
            Else
                'kode dealer tidak kosong
                Dim dealerArea As String
                If ddlArea.SelectedIndex <> 0 Then
                    'area dipilih
                    If txtKodeDealer.Text.IndexOf(";") = -1 Then
                        'hanya satu dealercode
                        Dim objDealerCek As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)

                        'add area1, refer bug 1132
                        Dim objArea As Area1 = New Area1Facade(User).Retrieve(CInt(ddlArea.SelectedValue))
                        If objArea.Dealers.Count > 0 Then
                            Dim i As Integer = 0
                            For Each item As Dealer In objarea.Dealers
                                If i = objarea.Dealers.Count Then
                                    Exit For
                                End If
                                If Not IsNothing(objdealercek.Area1) Then
                                    If item.DealerCode = objdealercek.DealerCode Then
                                        dealerarea = dealerarea & item.DealerCode & ";"
                                    End If
                                End If
                            Next
                        End If
                        If dealerArea <> "" Then
                            dealerArea = Left(dealerArea, dealerArea.Length - 1)
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "Dealer.DealerCode", MatchType.Exact, dealerArea.Trim()))
                        End If
                    Else
                        'get all dealer first
                        Dim arrDealer() As String = txtKodeDealer.Text.Split(";")
                        Dim arlDealerGet As New ArrayList
                        For Each item As String In arrDealer
                            Dim objDealerGet As Dealer = New DealerFacade(User).Retrieve(item)

                            arlDealerGet.Add(objDealerGet)
                        Next

                        'add area1, refer bug 1132
                        Dim objArea As Area1 = New Area1Facade(User).Retrieve(CInt(ddlArea.SelectedValue))
                        If objArea.Dealers.Count > 0 Then
                            Dim i As Integer = 0
                            For Each item As Dealer In objarea.Dealers
                                If i = objarea.Dealers.Count Then
                                    Exit For
                                End If
                                Dim objDealerCek As Dealer = arlDealerGet(i)
                                If Not IsNothing(item.Area1) Then
                                    If item.DealerCode = objdealercek.DealerCode Then
                                        dealerarea = dealerarea & item.DealerCode & ";"
                                        i = i + 1
                                    End If
                                End If
                            Next
                        End If
                        If dealerarea <> "" Then
                            dealerarea = Left(dealerarea, dealerarea.Length - 1)
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "Dealer.DealerCode", MatchType.InSet, "('" & dealerarea.Replace(";", "','") & "')"))
                        End If
                    End If
                Else
                    'area kosong
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
                End If
            End If
        Else    'login sebagai KTB
            If (txtKodeDealer.Text.Trim() <> String.Empty) Then
                'jika dipilih hanya single dealer
                If (txtKodeDealer.Text.IndexOf(";") = -1) Then
                    If ddlArea.SelectedIndex = 0 Then
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
                    Else
                        Dim objArea1 As Area1 = New Area1Facade(User).Retrieve(CInt(ddlArea.SelectedValue))
                        Dim objDealerSingle As Dealer = New DealerFacade(User).Retrieve(txtKodeDealer.Text.Trim)
                        If objArea1.Dealers.Count > 0 Then
                            If objArea1.ID = objDealerSingle.Area1.ID Then
                                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
                            End If
                        End If
                    End If
                Else
                    'jika dipilih multiple dealer
                    If ddlArea.SelectedIndex = 0 Then
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
                    Else
                        Dim arrDealer() As String = txtKodeDealer.Text.Split(";")
                        For Each item As String In arrDealer
                            Dim objDealerTemp As Dealer = New DealerFacade(User).Retrieve(item)

                            arlDealerTemp.Add(objDealerTemp)
                        Next

                        'add area1, refer bug 1132
                        Dim dealerArea As String
                        Dim objArea As Area1 = New Area1Facade(User).Retrieve(CInt(ddlArea.SelectedValue))
                        If objArea.Dealers.Count > 0 Then
                            Dim i As Integer = 0
                            For Each item As Dealer In arlDealerTemp
                                If i = objArea.Dealers.Count Then
                                    Exit For
                                End If
                                Dim objDealerCek As Dealer = objArea.Dealers(i)
                                If Not IsNothing(item.Area1) Then
                                    If item.Area1.ID = objDealerCek.Area1.ID Then
                                        dealerArea = dealerArea & item.DealerCode & ";"
                                        i = i + 1
                                    End If
                                End If
                            Next
                        End If
                        If dealerArea <> "" Then
                            dealerArea = Left(dealerArea, dealerArea.Length - 1)
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "Dealer.DealerCode", MatchType.InSet, "('" & dealerArea.Trim().Replace(";", "','") & "')"))
                        End If
                    End If
                End If
            Else
                'search dengan Kode Dealer Empty dan Area1 dipilih
                If ddlArea.SelectedIndex <> 0 Then
                    Dim objArea As Area1 = New Area1Facade(User).Retrieve(CInt(ddlArea.SelectedValue))
                    If objarea.Dealers.Count > 0 Then
                        Dim dealerarea As String
                        For Each item As Dealer In objarea.Dealers
                            dealerarea &= item.DealerCode & ";"
                        Next

                        dealerarea = Left(dealerarea, dealerarea.Length - 1)
                        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "Dealer.DealerCode", MatchType.InSet, "('" & dealerArea.Trim().Replace(";", "','") & "')"))
                    End If
                End If
            End If
        End If

        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'If (txtKodeDealer.Text.Trim() <> String.Empty) Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "DealerStockReportHeader.Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        'End If

        If (txtPeriode.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "PeriodMonth", MatchType.Exact, txtPeriode.Text.Substring(0, 2)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "PeriodYear", MatchType.Exact, txtPeriode.Text.Substring(2, 4)))
        End If

        'If (txtPeriode.Text.Trim() <> String.Empty) Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "DealerStockReportHeader.PeriodMonth", MatchType.Exact, txtPeriode.Text.Substring(0, 2)))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "DealerStockReportHeader.PeriodYear", MatchType.Exact, txtPeriode.Text.Substring(2, 4)))
        'End If


        If (ddlCategory.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "VechileType.Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        End If


        'If (ddlCategory.SelectedValue <> "-1") Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "ChassisMaster.Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        'End If

        If (ddlType.SelectedValue <> "-1") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportHeader), "VechileType.ID", MatchType.Exact, ddlType.SelectedValue))
        End If


        'If (ddlType.SelectedValue <> "-1") Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DealerStockReportDetail), "VechileColor.VechileType.ID", MatchType.Exact, ddlType.SelectedValue))
        'End If

        'Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        'sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DealerStockReportDetail), "VechileColor.ID", Sort.SortDirection.ASC))

        'Dim arlDetail As ArrayList = New ArrayList
        'Dim arlDetailFilter As ArrayList = New ArrayList
        'arlDetail = New DealerReport.DealerStockReportDetailFacade(User).Retrieve(criterias, sortColl)

        'Dim VCID As Integer = 0
        'For Each item As DealerStockReportDetail In arlDetail
        '    If (Not IsExistForKTB(item.VechileColor.MaterialNumber.Trim(), arlDetailFilter, item.DealerStockReportHeader.ID)) Then
        '        arlDetailFilter.Add(item)
        '    End If
        'Next

        Dim arl As New ArrayList
        arl = New DealerReport.DealerStockReportHeaderFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dgDealerStock.PageSize, totalRow)
        sessHelper.SetSession("Criterias4Download", criterias)

        dgDealerStock.VirtualItemCount = totalRow
        If arl.Count > 0 Then
            sessHelper.SetSession("StockDealer", arl)
            'Todo session
            'Session("StockDealer") = arl
            If bDownloadPriv Then
                btnDownload.Visible = True
            Else
                btnDownload.Visible = bDownloadPriv
            End If
            dgDealerStock.Visible = True
            dgDealerStock.DataSource = arl
            dgDealerStock.DataBind()

        Else
            dgDealerStock.Visible = False
            If bDownloadPriv Then
                btnDownload.Visible = False
            Else
                btnDownload.Visible = bDownloadPriv
            End If
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    'refer bug 1132
    Sub BindArea()
        Dim crits As New CriteriaComposite(New Criteria(GetType(Area1), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arlList As ArrayList = New Area1Facade(User).Retrieve(crits)
        ddlArea.DataSource = arlList
        ddlArea.DataTextField = "Description"
        ddlArea.DataValueField = "ID"
        ddlArea.DataBind()
        ddlArea.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
    End Sub


    Public Sub GetTotal(ByVal e As DataGridItemEventArgs)
        Dim crit As CriteriaComposite = CType(sessHelper.GetSession("Criterias4Download"), CriteriaComposite)
        Dim arlData As ArrayList = New DealerStockReportHeaderFacade(User).Retrieve(crit)
        If e.Item.ItemType = ListItemType.Footer Then
            Dim lblSalesVolume As Label = CType(e.Item.FindControl("lblTotalSalesVolume"), Label)
            Dim lblCarryOver As Label = CType(e.Item.FindControl("lblTotalCarryOver"), Label)
            Dim lblCarryOver_Min1 As Label = CType(e.Item.FindControl("lblTotalCarryOver_Min1"), Label)
            Dim lblNewOrder As Label = CType(e.Item.FindControl("lblTotalNewOrder"), Label)
            Dim lblBeginingStock As Label = CType(e.Item.FindControl("lblTotalBegStock"), Label)

            Dim sv As Integer = 0, co As Integer = 0, comin1 As Integer = 0
            Dim no As Integer = 0, bs As Integer = 0
            If arlData.Count > 0 Then
                For Each item As DealerStockReportHeader In arlData
                    sv = sv + item.SalesVolume
                    co = co + item.CarryOver
                    comin1 = comin1 + item.CarryOver_Min1
                    no = no + item.NewOrder
                    bs = bs + item.BeginingStock
                Next
            End If

            lblSalesVolume.Text = sv.ToString("#,##0")
            lblCarryOver.Text = co.ToString("#,##0")
            lblCarryOver_Min1.Text = comin1.ToString("#,##0")
            lblNewOrder.Text = no.ToString("#,##0")
            lblBeginingStock.Text = bs.ToString("#,##0")
        End If
    End Sub
#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()
        If Not IsPostBack Then
            Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            txtPeriode.Text = Date.Now.AddMonths(-1).ToString("MMyyyy")

            BindCategory()
            BindArea()
            BindType()
            'If (objDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            '    SetButtonKTB(False)
            '    SetPanelAndGridKTB(False)
            '    DealerData(objDealer)
            'ElseIf (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            'SetPanelAndGridKTB(True)
            txtKodeDealer.Visible = True
            lblSearchDealer.Visible = True
            KTBData()
            'End If
        End If
    End Sub

    Dim facade As New DealerStockReportHeaderFacade(User)
    Private Sub dgDealerStock_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDealerStock.ItemDataBound
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim oD As DealerStockReportHeader = CType(e.Item.DataItem, DealerStockReportHeader)

            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dgDealerStock.CurrentPageIndex * dgDealerStock.PageSize)
            CType(e.Item.FindControl("DgdsLblDealer"), Label).Text = oD.Dealer.DealerCode & " / " & oD.Dealer.SearchTerm1
            CType(e.Item.FindControl("lblKodeTipe"), Label).Text = oD.VechileType.VechileTypeCode
            CType(e.Item.FindControl("lblNamaTipe"), Label).Text = oD.VechileType.Description
            CType(e.Item.FindControl("lblSalesVolume"), Label).Text = oD.SalesVolume.ToString("#,##0")
            CType(e.Item.FindControl("lblCarryOver"), Label).Text = oD.CarryOver.ToString("#,##0")
            CType(e.Item.FindControl("lblCarryOver_Min1"), Label).Text = oD.CarryOver_Min1.ToString("#,##0")
            CType(e.Item.FindControl("lblNewOrder"), Label).Text = oD.NewOrder.ToString("#,##0")
            CType(e.Item.FindControl("lblBeginingStock"), Label).Text = oD.BeginingStock.ToString("#,##0")
            '    Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            '    Dim lblTotal As Label = CType(e.Item.FindControl("lblTotal"), Label)
            '    Dim lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            '    Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)

            '    lblDealer.Text = oD.DealerStockReportHeader.Dealer.DealerCode & " / " & oD.DealerStockReportHeader.Dealer.DealerName
            '    lbtnView.Attributes.Add("onclick", "showPopUp('../DealerReport/PopUpStockDealerDetail.aspx?ID=" & oD.DealerStockReportHeader.ID & "&VCID=" & oD.VechileColor.ID & "', '', 600, 800, null);return false;")
            '    Dim en As New EnumStockDealerStatus
            '    lblStatus.Text = en.GetStringValue(oD.DealerStockReportHeader.Status)
            '    Dim Tot As Integer = 0
            '    Tot = GetTotalUnit(oD.DealerStockReportHeader.ID, oD.VechileColor.ID)
            '    lblTotal.Text = Tot.ToString("#,##0")
            '    TotalUnit = TotalUnit + Tot

        End If
        If (e.Item.ItemType = ListItemType.Footer) Then
            GetTotal(e)
            'Dim lblSalesVolume As Label = CType(e.Item.FindControl("lblTotalSalesVolume"), Label)
            'Dim lblCarryOver As Label = CType(e.Item.FindControl("lblTotalCarryOver"), Label)
            'Dim lblCarryOver_Min1 As Label = CType(e.Item.FindControl("lblTotalCarryOver_Min1"), Label)
            'Dim lblNewOrder As Label = CType(e.Item.FindControl("lblTotalNewOrder"), Label)
            'Dim lblBeginingStock As Label = CType(e.Item.FindControl("lblTotalBegStock"), Label)

        End If
    End Sub



    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgDealerStock.CurrentPageIndex = 0
        If ValidateSearch() Then
            SearchForKTB(dgDealerStock.CurrentPageIndex)
        End If

    End Sub

    Private Function ValidateSearch() As Boolean
        If txtPeriode.Text.Trim <> "" Then
            Dim periode As String = txtPeriode.Text.Trim
            Try
                Dim valid_date As Date = New Date(periode.Substring(2, 4), periode.Substring(0, 2), 1)
            Catch ex As Exception
                MessageBox.Show("Invalid Periode")
                Return False
            End Try
        End If
        Return True
    End Function

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arrl As ArrayList = CType(sessHelper.GetSession("StockDealer"), ArrayList)
        If arrl Is Nothing Then
            MessageBox.Show("Tidak Ada Data Yang Diproses")
            Exit Sub
        End If
        If (New DealerReport.DealerStockReportDetailFacade(User).UpdateStatusToValidation(arrl) = 1) Then
            MessageBox.Show("Data berhasil diproses")
        Else
            MessageBox.Show("Data gagal diproses")
        End If
    End Sub

#End Region



    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim crits As CriteriaComposite = sessHelper.GetSession("Criterias4Download")
        Dim arl4Download As ArrayList = New DealerStockReportHeaderFacade(User).Retrieve(crits)
        sessHelper.SetSession("StockDealerDownload", arl4Download)
        DownloadData()
    End Sub

    Dim strFileName As String = "Rekap" & Date.Today.Day.ToString & Date.Today.Month.ToString & _
            Date.Today.Year.ToString & Date.Now.Hour.ToString & Date.Now.Minute.ToString & _
            Date.Now.Second.ToString & ".xls"
    Private Sub DownloadData()
        Dim strText As StringBuilder
        Dim _str As String = String.Empty
        Dim arrToDownload As New ArrayList
        Dim delimiter As String = Chr(9)
        'Dim cuPage As Integer = dtgCustomerRequest.CurrentPageIndex
        Dim i As Integer = 0

        Dim arltoDownload As ArrayList = New ArrayList
        If Not IsNothing(sessHelper.GetSession("StockDealerDownload")) Then
            'If Not IsNothing(sessHelper.GetSession("StockDealer")) Then
            'Dim crite As CriteriaComposite
            'crite = CType(sessHelper.GetSession("FilterCustomer"), CriteriaComposite)

            'arltoDownload = New CustomerFacade(User).Retrieve(crite)
            arltoDownload = CType(sessHelper.GetSession("StockDealerDownload"), ArrayList)
            If arltoDownload.Count > 0 Then
                strText = New StringBuilder
                '-- set Header
                HeaderDownload(strText, arltoDownload.Count.ToString.Length)
                '-- detail value
                Dim no As Integer = 1
                Dim TotalSalesVolume As Integer = 0
                For Each objCust As DealerStockReportHeader In arltoDownload
                    '-- No
                    strText.Append((no.ToString() & ".").Trim)
                    strText.Append(delimiter)

                    '---- Dealer
                    strText.Append(objCust.Dealer.DealerCode.Trim & " / " & objCust.Dealer.DealerName.Trim)
                    strText.Append(delimiter)

                    '---- Kode Tipe
                    '_str = objCust.Name1.Trim & objCust.Name2.Trim
                    strText.Append(objCust.VechileType.VechileTypeCode.Trim)
                    strText.Append(delimiter)

                    '---- Nama Tipe
                    strText.Append(objCust.VechileType.Description.Trim)
                    strText.Append(delimiter)

                    '---- Carry Over(n-1)
                    strText.Append(objCust.CarryOver_Min1)
                    strText.Append(delimiter)

                    '---- New Order 
                    strText.Append(objCust.NewOrder)
                    strText.Append(delimiter)

                    '---- Sales Volume 
                    strText.Append(objCust.SalesVolume)
                    TotalSalesVolume = TotalSalesVolume + objCust.SalesVolume
                    strText.Append(delimiter)

                    '---- Carry Over
                    strText.Append(objCust.CarryOver)
                    strText.Append(delimiter)

                    '---- Begining Stock
                    strText.Append(objCust.BeginingStock)
                    strText.Append(delimiter)

                    ''---- Kota 
                    '_str = objCust.PreArea.Trim & " " & objCust.City.CityName.Trim
                    'strText.Append(_str.Trim)
                    'strText.Append(delimiter)

                    ''---- Propinsi 
                    'strText.Append(objCust.City.Province.ProvinceName.Trim)
                    'strText.Append(delimiter)

                    '----new line
                    strText.Append(vbNewLine)
                    i = i + 1
                    no = no + 1
                Next
                strText.Append(vbNewLine & vbNewLine)
                strText.Append("Total Sales Volume = " & TotalSalesVolume)
                strText.Append(vbNewLine)
                Try
                    saveToTextFile(strText.ToString())
                Catch
                    MessageBox.Show("Proses Download gagal")
                    Return
                End Try

                Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & strFileName)

            End If
            'arltoDownload = CType(sessHelper.GetSession("ListCustomer"), ArrayList)


        End If
    End Sub

    Private Sub HeaderDownload(ByRef HeaderTxt As StringBuilder, ByVal RecCount As Integer)
        Dim delimiter As String = Chr(9)
        '--- No
        HeaderTxt.Append("No.")
        HeaderTxt.Append(delimiter)

        '---- Dealer
        HeaderTxt.Append("Dealer.")
        HeaderTxt.Append(delimiter)

        '---- Kode Tipe
        HeaderTxt.Append("Kode Tipe.")
        HeaderTxt.Append(delimiter)

        '---- Nama Tipe
        HeaderTxt.Append("Nama Tipe.")
        HeaderTxt.Append(delimiter)

        '-----Carry Over (n-1)
        HeaderTxt.Append("Carry Over(n-1)")
        HeaderTxt.Append(delimiter)

        '---- New Order 
        HeaderTxt.Append("New Order.")
        HeaderTxt.Append(delimiter)

        '---- Sales Volume 
        HeaderTxt.Append("Sales Volume.")
        HeaderTxt.Append(delimiter)

        '---- Carry Over 
        HeaderTxt.Append("Carry Over.")
        HeaderTxt.Append(delimiter)

        '---- Begining Stock 
        HeaderTxt.Append("Begining Stock.")
        HeaderTxt.Append(delimiter)

        ''---- Kota 
        'HeaderTxt.Append("Kota.")
        'HeaderTxt.Append(delimiter)

        ''---- Propinsi 
        'HeaderTxt.Append("Propinsi.")
        'HeaderTxt.Append(delimiter)

        '----new line
        HeaderTxt.Append(vbNewLine)

    End Sub

    Private Sub saveToTextFile(ByVal str As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Try
            If imp.Start() Then

                Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\" & strFileName, FileMode.Create, FileAccess.Write)
                Dim objStreamWriter As New StreamWriter(objFileStream)

                objStreamWriter.WriteLine(str)
                objStreamWriter.Close()

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub dgDealerStock_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDealerStock.PageIndexChanged
        dgDealerStock.CurrentPageIndex = e.NewPageIndex
        SearchForKTB(dgDealerStock.CurrentPageIndex)
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        If ddlCategory.SelectedIndex <> -1 Then
            Dim id As Integer = ddlCategory.SelectedValue
            If id = 0 Then
                BindType()
            Else
                Dim crits As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crits.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, id))
                crits.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))

                Dim arl As New ArrayList
                Dim sortColl As SortCollection = New SortCollection
                sortColl.Add(New Sort(GetType(VechileType), "Description", Sort.SortDirection.ASC))

                arl = New FinishUnit.VechileTypeFacade(User).Retrieve(crits, sortColl)
                ddlType.DataSource = arl
                ddlType.DataTextField = "Description"
                ddlType.DataValueField = "ID"
                ddlType.DataBind()
                ddlType.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
            End If
        End If
    End Sub
End Class
