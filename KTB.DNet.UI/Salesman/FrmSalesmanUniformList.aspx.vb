#Region "Custom Namespace Import"
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.UI.Helper
Imports System.io
Imports KTB.DNet.Security
#End Region

Public Class FrmSalesmanUniformList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents TextBox2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents icTglOrderFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglOrderUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDistCode As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtDistributionCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgSalesmanUniformPriceList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtOrderNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
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

#Region "Deklarasi"
    Dim criterias As CriteriaComposite
    Dim sHelper As New SessionHelper
    Private objDealer As Dealer
    Private _downloadPriv As Boolean = False
    Private _viewDetailPriv As Boolean = False

#End Region

#Region "Custom Method"
    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim blnBack As Boolean

        If indexPage >= 0 Then
            If Not IsNothing(sHelper.GetSession("ModeBack")) Then
                indexPage = ReadCriteria()
                dtgSalesmanUniformPriceList.CurrentPageIndex = indexPage
                sHelper.RemoveSession("ModeBack")
            End If

            Dim nResult As Integer = CreateCriteria()
            If nResult > -1 Then
                Dim arlUnifList As ArrayList = New SalesmanUniformOrderHeaderFacade(User).RetrieveActiveList(indexPage + 1, dtgSalesmanUniformPriceList.PageSize, totalRow, viewstate("SortColUL"), viewstate("SortDirectionUL"), criterias)

                If arlUnifList.Count > 0 Then
                    dtgSalesmanUniformPriceList.DataSource = arlUnifList
                    dtgSalesmanUniformPriceList.VirtualItemCount = totalRow
                    'btnDownload.Enabled = _downloadPriv
                Else
                    dtgSalesmanUniformPriceList.DataSource = New ArrayList
                    MessageBox.Show("Tidak ada data yang ditemukan")
                    'btnDownload.Enabled = False
                End If
            Else
                dtgSalesmanUniformPriceList.DataSource = Nothing
                dtgSalesmanUniformPriceList.DataBind()
                MessageBox.Show("Kode dealer tidak valid.")
                'btnDownload.Enabled = False
            End If

            If indexPage = 0 Then
                dtgSalesmanUniformPriceList.CurrentPageIndex = 0
            End If

            dtgSalesmanUniformPriceList.DataBind()
        End If
        SaveCriteria(indexPage)

    End Sub
    Private Sub SaveCriteria(ByVal IntCurrentPageIndex As Integer)
        Dim crits As Hashtable = New Hashtable
        crits.Add("DealerCode", txtDealerCode.Text)
        crits.Add("DistributionCode", txtDistributionCode.Text)
        crits.Add("OrderNo", txtOrderNo.Text)
        crits.Add("TglOrderFrom", icTglOrderFrom.Value)
        crits.Add("TglOrderUntil", icTglOrderUntil.Value)
        crits.Add("SortColumn", viewstate("SortColUL"))
        crits.Add("SortDirection", viewstate("SortDirectionUL"))
        crits.Add("CurrentPageIndex", dtgSalesmanUniformPriceList.CurrentPageIndex)
        sHelper.SetSession("UniformList", crits)

    End Sub
    Private Function ReadCriteria() As Integer
        Dim crits As Hashtable
        Dim intCurIndex As Integer
        intCurIndex = 0
        crits = CType(sHelper.GetSession("UniformList"), Hashtable)
        If Not IsNothing(crits) Then
            txtDealerCode.Text = CStr(crits.Item("DealerCode"))
            txtDistributionCode.Text = CStr(crits.Item("DistributionCode"))
            txtOrderNo.Text = CStr(crits.Item("OrderNo"))
            icTglOrderFrom.Value = CDate(crits.Item("TglOrderFrom"))
            icTglOrderUntil.Value = CDate(crits.Item("TglOrderUntil"))
            viewstate("SortColUL") = crits.Item("SortColumn")
            viewstate("SortDirectionUL") = crits.Item("SortDirection")
            intCurIndex = CInt(crits.Item("CurrentPageIndex"))
        End If
        Return intCurIndex
    End Function
    Private Function CreateCriteria() As Integer
        criterias = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'If txtDealerCode.Text <> String.Empty Then
        '    Dim strDealerCode As String = txtDealerCode.Text.Replace(";", "','")
        '    criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "Dealer.DealerCode", MatchType.InSet, "('" & strDealerCode & "')"))
        'End If

        objDealer = Session("DEALER")

        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
            End If
        Else
            If (txtDealerCode.Text.Trim <> String.Empty) Then
                If New DataOwner().IsdealerExistInGroup(txtDealerCode.Text.Trim, objDealer) Then
                    criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "Dealer.DealerCode", MatchType.InSet, "('" + Replace(txtDealerCode.Text, ";", "','") + "')"))
                Else
                    Return -1
                End If
            Else
                Dim strCrit As String = New DataOwner().GenerateDealerCodeSelection(objDealer, User)
                criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "Dealer.DealerCode", MatchType.InSet, strCrit))
            End If
        End If


        If txtDistributionCode.Text <> String.Empty Then
            Dim strDistrCode As String = txtDistributionCode.Text.Replace(";", "','")
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "SalesmanUnifDistribution.SalesmanUnifDistributionCode", MatchType.InSet, "('" & strDistrCode & "')"))
        End If

        If txtOrderNo.Text <> String.Empty Then
            'Dim strOrderNo As String = txtDistributionCode.Text.Replace(";", "','")
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "OrderNumber", MatchType.Exact, txtOrderNo.Text))
        End If

        If icTglOrderUntil.Value < icTglOrderFrom.Value Then
            MessageBox.Show("Tanggal Sampai tidak boleh kurang dari tanggal Dari")
        Else
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "OrderDate", MatchType.GreaterOrEqual, icTglOrderFrom.Value))
            criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "OrderDate", MatchType.LesserOrEqual, icTglOrderUntil.Value))
        End If

        '27-Sep-2007    Deddy H     hanya menampilkan unifDistribution yang aktif saja
        criterias.opAnd(New Criteria(GetType(SalesmanUniformOrderHeader), "SalesmanUnifDistribution.IsActive", MatchType.Exact, CType(enumStatusSalesmanUnifDistribution.StatusSalesmanUnifDistribution.Aktif, Integer)))

        sHelper.SetSession("UnifListCriterias", criterias)
        Return 0

    End Function
    Private Sub ClearData()
        Dim objUserInfo As UserInfo = New UserInfo
        objUserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If objUserInfo.Dealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
            txtDealerCode.Text = String.Empty
        End If
        txtDistributionCode.Text = String.Empty
        txtOrderNo.Text = String.Empty
        icTglOrderFrom.Value = Now
        icTglOrderUntil.Value = Now
    End Sub
    Private Sub downloaddata()
        'sHelper.SetSession("UnifListCriterias", criterias)

        If Not IsNothing(sHelper.GetSession("UnifListCriterias")) Then
            Dim criteriasDownload As CriteriaComposite
            criteriasDownload = CType(sHelper.GetSession("UnifListCriterias"), ICriteria)

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(Viewstate("SortDirectionUL"))) And (Not IsNothing(ViewState("SortColUL"))) Then
                sortColl.Add(New Sort(GetType(SalesmanUniformOrderHeader), ViewState("SortColUL"), Viewstate("SortDirectionUL")))
            Else
                sortColl = Nothing
            End If

            Dim arToProcess As ArrayList = New ArrayList
            Dim arToDownload As ArrayList = New ArrayList

            arToProcess = New SalesmanUniformOrderHeaderFacade(User).Retrieve(criteriasDownload, sortColl)

            If arToProcess.Count > 0 Then
                Dim i As Integer = 0
                Dim success As Boolean = False
                Dim Connect As Boolean = False
                Dim sw As StreamWriter
                Dim filename = String.Format("{0}{1}{2}", "SalesmanUniformOrder", Date.Now.ToString("ddMMyyyyHHmmss"), ".xls")
                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("DownloadSalesmanUniformOrder") & "\" & filename  '-- Destination file
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                Dim imp As SAPImpersonate

                If (Connect = False) Then
                    imp = New SAPImpersonate(_user, _password, _webServer)
                    Dim finfo As New FileInfo(DestFile)

                    Try
                        success = imp.Start()
                        If success Then
                            If Not finfo.Directory.Exists Then
                                Directory.CreateDirectory(finfo.DirectoryName)
                            End If
                            sw = New StreamWriter(DestFile)
                            Connect = True
                        End If
                    Catch ex As Exception
                        Throw ex
                        Exit Sub
                    End Try
                End If

                Dim strTab As String = Chr(9)
                ' Modified by Ikhsan, 25 Agustus 2008
                ' Requested by Rina, as Part Of CR
                ' Modify the Excel Structure.
                ' Start -------------------------------------------------------------
                ' sw.WriteLine("")
                sw.WriteLine("" + strTab + strTab + strTab + strTab + "Jumlah Ukuran Yang Dipesan")
                'sw.WriteLine("Kode Dealer" + strTab + "Nama Dealer" + strTab + "Kode Pesanan" + strTab + "No Order" + strTab + "Jumlah Ukuran Yang Dipesan")
                sw.WriteLine("Kode Dealer" + strTab + "Nama Dealer" + strTab + "Kode Pesanan" + strTab + "No Order" + strTab + "S" + strTab + "M" + strTab + "L" + strTab + "XL" + strTab + "XXL")


                ' End ---------------------------------------------------------------
                For Each item As SalesmanUniformOrderHeader In arToProcess
                    'cek salesmanUnifDistribution
                    If item.SalesmanUnifDistribution.ID > 0 Then
                        Dim objDistribution As SalesmanUnifDistribution = New SalesmanUnifDistributionFacade(User).Retrieve(CInt(item.SalesmanUnifDistribution.ID))
                        If objDistribution.ID > 0 Then
                            Dim strJumlahUkuran As String = "" 'String.Empty
                            Dim arlUniformOrder As ArrayList = New ArrayList
                            Dim CtrUnifOrderDet As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanUniformOrderDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            CtrUnifOrderDet.opAnd(New Criteria(GetType(SalesmanUniformOrderDetail), "SalesmanUniformOrderHeader.ID", MatchType.Exact, CInt(item.ID)))

                            arlUniformOrder = New SalesmanUniformOrderDetailFacade(User).Retrieve(CtrUnifOrderDet)

                            If arlUniformOrder.Count > 0 Then
                                Dim S As Integer = 0
                                Dim M As Integer = 0
                                Dim L As Integer = 0
                                Dim XL As Integer = 0
                                Dim XXL As Integer = 0
                                For Each itemDetail As SalesmanUniformOrderDetail In arlUniformOrder
                                    'Dim _unifsize As String = String.Empty
                                    '_unifsize = New EnumUniformSize().GetName(CInt(itemDetail.UniformSize))
                                    'If _unifsize <> String.Empty Then
                                    '    ' strJumlahUkuran = strJumlahUkuran & _unifsize & ":" & itemDetail.Qty.ToString & ", "
                                    '    strJumlahUkuran = strJumlahUkuran + itemDetail.Qty.ToString + strTab
                                    'Else
                                    '    ' strJumlahUkuran = strJumlahUkuran & "Not Valid Size:" & itemDetail.Qty.ToString & ", "
                                    '    strJumlahUkuran = strJumlahUkuran + "0" + strTab
                                    'End If
                                    Select Case CInt(itemDetail.UniformSize)
                                        Case 1
                                            S = S + itemDetail.Qty
                                        Case 2
                                            M = M + +itemDetail.Qty
                                        Case 3
                                            L = L + itemDetail.Qty
                                        Case 4
                                            XL = XL + itemDetail.Qty
                                        Case 5
                                            XXL = XXL + itemDetail.Qty
                                    End Select
                                Next
                                'If strJumlahUkuran <> String.Empty Then
                                '    strJumlahUkuran = Left(strJumlahUkuran, Len(strJumlahUkuran) - 2)
                                'End If
                                'sw.WriteLine(item.Dealer.DealerCode + strTab + item.Dealer.DealerName + strTab + item.SalesmanUnifDistribution.SalesmanUnifDistributionCode.ToString + strTab + IIf(item.OrderNumber <> String.Empty, item.OrderNumber, String.Empty) + strTab + strJumlahUkuran)
                                sw.WriteLine(item.Dealer.DealerCode + strTab + item.Dealer.DealerName + strTab + item.SalesmanUnifDistribution.SalesmanUnifDistributionCode.ToString + strTab + IIf(item.OrderNumber <> String.Empty, item.OrderNumber, String.Empty) + strTab + S.ToString + strTab + M.ToString + strTab + L.ToString + strTab + XL.ToString + strTab + XXL.ToString + strTab + String.Empty)
                            Else
                                'sw.WriteLine(item.Dealer.DealerCode + strTab + item.Dealer.DealerName + strTab + item.SalesmanUnifDistribution.SalesmanUnifDistributionCode.ToString + strTab + IIf(item.OrderNumber <> String.Empty, item.OrderNumber, String.Empty) + strTab + strJumlahUkuran)
                                sw.WriteLine(item.Dealer.DealerCode + strTab + item.Dealer.DealerName + strTab + item.SalesmanUnifDistribution.SalesmanUnifDistributionCode.ToString + strTab + IIf(item.OrderNumber <> String.Empty, item.OrderNumber, String.Empty) + strTab + String.Empty)
                            End If
                        End If
                    Else
                        sw.WriteLine(item.Dealer.DealerCode + strTab + String.Empty + strTab + IIf(item.OrderNumber <> String.Empty, item.OrderNumber, String.Empty) + strTab + String.Empty)

                    End If



                Next

                If (success = True) Then
                    sw.Close()
                    imp.StopImpersonate()
                    imp = Nothing
                    Dim PathFile As String = KTB.DNet.Lib.WebConfig.GetValue("DownloadSalesmanUniformOrder") & "\" & filename
                    Response.Redirect("../Download.aspx?file=" & PathFile)
                Else
                    MessageBox.Show("Download file Pemesanan Seragam Salesman gagal")
                End If
            Else
                MessageBox.Show("Tidak Ada Data Yang Didownload")
            End If
        End If
    End Sub
#End Region


#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objDealer As Dealer = sHelper.GetSession("DEALER")

        If Not IsNothing(sHelper.GetSession("LOGINUSERINFO")) Then
            Dim objUserInfo As UserInfo = New UserInfo
            objUserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)
            If objUserInfo.Dealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                CheckPrivilege(True)
                _viewDetailPriv = CheckViewDetailPrivilege(True)
            Else
                CheckPrivilege(False)
                _viewDetailPriv = CheckViewDetailPrivilege(False)
            End If
        End If
        '1233
        ' Modified by Ikhsan, 21 Agustus 2008
        ' Requested by Rina, in order to allow KTB/Dealer to download file
        ' Reported as Bug, wrong logic implementation in bussines Process
        ' Start------------------------------------------------------------------
        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        _downloadPriv = CheckDownloadPrivilege()

        'btnDownload.Visible = _downloadPriv

        'End If
        ' End--------------------------------------------------------------------

        If Not IsPostBack Then
            viewstate.Add("SortColUL", "Dealer.DealerCode")
            viewstate.Add("SortDirectionUL", Sort.SortDirection.ASC)
            lblSearchDealer.Attributes.Add("onclick", "showPopUp('../SparePart/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);")
            lblSearchDistCode.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpUnifDistribution.aspx','',500,760,DistributionCodeSelection);")
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtDealerCode.ReadOnly = True
                txtDealerCode.Text = objDealer.DealerCode
                lblSearchDealer.Visible = False
                lblTitle.Text = "Daftar Seragam"
            Else
                lblSearchDealer.Visible = True
                txtDealerCode.ReadOnly = False
                lblTitle.Text = "Pengesahan Seragam"
            End If
            BindToGrid(0)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgSalesmanUniformPriceList.CurrentPageIndex = 0
        BindToGrid(dtgSalesmanUniformPriceList.CurrentPageIndex)
    End Sub

    Private Sub dtgSalesmanUniformPriceList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSalesmanUniformPriceList.PageIndexChanged
        dtgSalesmanUniformPriceList.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgSalesmanUniformPriceList.CurrentPageIndex)
    End Sub

    Private Sub dtgSalesmanUniformPriceList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSalesmanUniformPriceList.SortCommand
        If e.SortExpression = viewstate("SortColUL") Then
            If viewstate("SortDirectionUL") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirectionUL", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirectionUL", Sort.SortDirection.ASC)
            End If
        Else
            ViewState("SortDirectionUL") = Sort.SortDirection.ASC
        End If
        ViewState("SortColUL") = e.SortExpression

        dtgSalesmanUniformPriceList.SelectedIndex = -1
        BindToGrid(dtgSalesmanUniformPriceList.CurrentPageIndex)
    End Sub

    Private Sub dtgSalesmanUniformPriceList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSalesmanUniformPriceList.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            'Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            'lblNo.Text = e.Item.ItemIndex + 1

            Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            _lblNo.Text = e.Item.ItemIndex + 1 + (dtgSalesmanUniformPriceList.CurrentPageIndex * dtgSalesmanUniformPriceList.PageSize)

            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnEdit.Visible = _viewDetailPriv

        End If
    End Sub

    Private Sub dtgSalesmanUniformPriceList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSalesmanUniformPriceList.ItemCommand
        If e.CommandName = "view" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Response.Redirect("FrmSalesmanUniformOrder.aspx?id=" & CInt(lblID.Text.Trim) & "&mode=view")
        ElseIf e.CommandName = "edit" Then
            If Not IsActiveTransControl() Then
                MessageBox.Show("Anda tidak dapat melakukan fungsi edit")
                Return
            End If
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Response.Redirect("FrmSalesmanUniformOrder.aspx?id=" & CInt(lblID.Text.Trim) & "&mode=edit")
            End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        downloaddata()
    End Sub

#End Region

#Region "Privilege & Transaction Control"


    Private Function IsActiveTransControl() As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TransactionControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TransactionControl), "Kind", MatchType.Exact, CInt(EnumDealerTransType.DealerTransKind.PesanSeragam)))
        criterias.opAnd(New Criteria(GetType(TransactionControl), "Dealer.ID", MatchType.Exact, CType(Session("Dealer"), Dealer).ID))

        Dim objTransControl As TransactionControl = New DealerFacade(User).RetrieveTransactionControlByCriteria(criterias)

        If IsNothing(objTransControl) Then
            Return True
        Else
            If objTransControl.Status = 1 Then
                Return True
            Else
                Return False
            End If
        End If

    End Function
    Private Sub CheckPrivilege(ByVal isKTB As Boolean)
        If isKTB Then
            If Not SecurityProvider.Authorize(context.User, SR.UniformApprovalView_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Seragam Tenaga Penjual - Daftar Seragam")
            End If
        Else
            If Not SecurityProvider.Authorize(context.User, SR.LihatDataSeragam_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Seragam Tenaga Penjual - Daftar Seragam")
            End If
        End If
    End Sub

    Private Function CheckViewDetailPrivilege(ByVal isKTB As Boolean) As Boolean
        If isKTB Then
            If Not SecurityProvider.Authorize(context.User, SR.UniformApprovalViewDetail_Privilege) Then
                Return False
            Else
                Return True
            End If
        Else
            If Not SecurityProvider.Authorize(context.User, SR.UniformListViewDetail_Privilege) Then
                Return False
            Else
                Return True
            End If
        End If
    End Function

    Private Function CheckDownloadPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.UniformListDownload_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region
End Class
