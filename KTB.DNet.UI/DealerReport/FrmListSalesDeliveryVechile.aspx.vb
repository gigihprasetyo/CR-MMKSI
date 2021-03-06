Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.DealerReport
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.IO
Imports System.Text



Public Class FrmListSalesDeliveryVechile
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerSearch As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglDeliverySearch As System.Web.UI.WebControls.Label
    Protected WithEvents icTglDeliverDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglDeliverSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblReffDO As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgListDeliveryVechile As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTotalUnitVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalUnit As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus2 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents pnlChangeStatus As System.Web.UI.WebControls.Panel
    Protected WithEvents lblStatusTitle As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDOReffNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTujuan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDORegNo As System.Web.UI.WebControls.TextBox


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variable"
    Private oDeliveryCustomerHeaderFacade As New DeliveryCustomerHeaderFacade(User)
    Private oDeliveryCustomerHeader As New DeliveryCustomerHeader
    Private oDealer, objDealer As Dealer
    Private sessHelper As New SessionHelper
    Private _arrDeliveryCustomerHeader As ArrayList
    Private dt As DateTime = DateTime.Now
    Private TotalUnit As Integer
    Private Suffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
    Private bEditPriv As Boolean
    Private bViewPriv As Boolean

    Private bDownloadPriv As Boolean
#End Region



#Region "Custom Method"

#Region "Check Privilage"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.KirimKendaraanListView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Transaksi - Daftar Pengiriman")
        End If

        bViewPriv = SecurityProvider.Authorize(context.User, SR.KirimKendaraanListView_Privilege)
        bEditPriv = SecurityProvider.Authorize(context.User, SR.KirimKendaraanListEdit_Privilege)
        bDownloadPriv = SecurityProvider.Authorize(context.User, SR.KirimKendaraanListDownload_Privilege)
    End Sub

#End Region

    Private Sub ClearForm()
        txtKodeDealer.Text = ""
        txtDORegNo.Text = ""
        txtDOReffNo.Text = ""

        ddlStatus.SelectedIndex = 0
        ddlTujuan.SelectedIndex = 0

        icTglDeliverDari.Value = DateTime.Today
        icTglDeliverSampai.Value = DateTime.Today
    End Sub
    Private Sub SetSessionCriteria()
        Dim arrLastState As ArrayList = New ArrayList
        arrLastState.Add(txtKodeDealer.Text)
        arrLastState.Add(txtDORegNo.Text)
        arrLastState.Add(txtDOReffNo.Text)
        arrLastState.Add(icTglDeliverDari.Value)
        arrLastState.Add(icTglDeliverSampai.Value)

        arrLastState.Add(ddlStatus.SelectedIndex)
        arrLastState.Add(ddlTujuan.SelectedIndex)

        arrLastState.Add(dgListDeliveryVechile.CurrentPageIndex)
        arrLastState.Add(CType(ViewState("currSortColumn"), String))
        arrLastState.Add(CType(ViewState("currSortDirection"), Sort.SortDirection))
        sessHelper.SetSession("SDVSESSIONLASTSTATE", arrLastState)
    End Sub
    Private Sub GetSessionCriteria()
        Dim arrLastState As ArrayList = sessHelper.GetSession("SDVSESSIONLASTSTATE")
        If Not arrLastState Is Nothing Then
            txtKodeDealer.Text = arrLastState.Item(0)
            txtDORegNo.Text = arrLastState.Item(1)
            txtDOReffNo.Text = arrLastState.Item(2)
            icTglDeliverDari.Value = arrLastState.Item(3)
            icTglDeliverSampai.Value = arrLastState.Item(4)
            ddlStatus.SelectedIndex = arrLastState.Item(5)
            ddlTujuan.SelectedIndex = arrLastState.Item(6)
            dgListDeliveryVechile.CurrentPageIndex = arrLastState.Item(7)
            ViewState("currSortColumn") = arrLastState.Item(8)
            ViewState("currSortDirection") = arrLastState.Item(9)
        Else
            If CType(sessHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtKodeDealer.Text = CType(sessHelper.GetSession("DEALER"), Dealer).DealerCode
            End If
            ViewState("currSortColumn") = "SourceDealer.DealerCode"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            dgListDeliveryVechile.CurrentPageIndex = 0
        End If
    End Sub

    Private Function GetDealerCodeIDCollection(ByVal CollectionDealerCode As String) As String
        Dim result As String = String.Empty
        Dim arr As New ArrayList
        Dim _criterias As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        _criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.InSet, "('" & CollectionDealerCode.Replace(";", "','") & "')"))
        arr = New DealerFacade(User).RetrieveByCriteria(_criterias)

        If arr.Count > 0 Then
            For Each item As Dealer In arr
                result += item.ID.ToString() & ","
            Next

            result = result.Substring(0, result.Length - 1)
        End If
        Return result
    End Function

    Private Sub BindToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        TotalUnit = 0
        objDealer = Session("DEALER")
        Dim mode As Integer = 1
        Dim strDealer As String = String.Empty

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "Status", MatchType.No, CType(EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Batal, Short)))

        criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "PostingDate", MatchType.GreaterOrEqual, icTglDeliverDari.Value))
        criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "PostingDate", MatchType.LesserOrEqual, icTglDeliverSampai.Value.AddDays(1)))
        'If txtKodeDealer.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "SourceDealer.DealerCode", MatchType.Partial, txtKodeDealer.Text))

        If txtKodeDealer.Text.Trim <> String.Empty Then
            strDealer = GetDealerCodeIDCollection(txtKodeDealer.Text)
            If strDealer.Trim = String.Empty Then
                dgListDeliveryVechile.DataSource = New ArrayList
                dgListDeliveryVechile.DataBind()
                MessageBox.Show("Data Tidak Ditemukan")
                Return
            End If
        End If


        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (txtKodeDealer.Text.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "FromDealer", MatchType.InSet, "(" + GetDealerCodeIDCollection(txtKodeDealer.Text) + ")"))
            End If
        Else
            If (txtKodeDealer.Text.Trim <> String.Empty) Then
                If New DataOwner().IsdealerExistInGroup(txtKodeDealer.Text.Trim, objDealer) Then
                    criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "FromDealer", MatchType.InSet, "(" + GetDealerCodeIDCollection(txtKodeDealer.Text) + ")"))
                Else
                    mode = 0
                End If
            Else
                Dim strCrit As String = New DataOwner().GenerateDealerIDSelection(objDealer, User)
                criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "FromDealer", MatchType.InSet, strCrit))
            End If
        End If

        If txtDORegNo.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "RegDONumber", MatchType.[Partial], txtDORegNo.Text))
        If txtDOReffNo.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "ReffDONumber", MatchType.[Partial], txtDOReffNo.Text))
        If ddlStatus.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))

        If ddlTujuan.SelectedValue <> "Semua" Then
            If ddlTujuan.SelectedValue = "Dealer" Then
                criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "DestinationDealer", MatchType.No, 0))
            ElseIf ddlTujuan.SelectedValue = "Customer" Then
                criterias.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "Customer.ID", MatchType.No, 0))
            End If
        End If

        sessHelper.SetSession("CriteriaSesslstSlsDlvVeh", criterias)

        _arrDeliveryCustomerHeader = oDeliveryCustomerHeaderFacade.RetrieveByCriteria(criterias, currentPageIndex + 1, dgListDeliveryVechile.PageSize, total, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        sessHelper.SetSession("DeliveryVechileList", _arrDeliveryCustomerHeader)



        If mode = 0 Then
            dgListDeliveryVechile.DataSource = Nothing
            dgListDeliveryVechile.DataBind()
            MessageBox.Show("Kode dealer tidak valid.")
        Else
            If (_arrDeliveryCustomerHeader.Count > 0) Then
                pnlChangeStatus.Visible = True

                If bDownloadPriv Then
                    btnDownload.Visible = True
                Else
                    btnDownload.Visible = bDownloadPriv
                End If

                dgListDeliveryVechile.VirtualItemCount = total
                dgListDeliveryVechile.DataSource = _arrDeliveryCustomerHeader
                dgListDeliveryVechile.DataBind()
                SetStatusPanel()
            Else
                pnlChangeStatus.Visible = False

                If bDownloadPriv Then
                    btnDownload.Visible = False
                Else
                    btnDownload.Visible = bDownloadPriv
                End If

                dgListDeliveryVechile.DataSource = New ArrayList
                dgListDeliveryVechile.DataBind()
            End If
            lblTotalUnitVal.Text = TotalUnit.ToString("#,##0") & " Unit"
        End If
    End Sub

    Private Sub Download()
        Dim strText As StringBuilder
        Dim objAl As New ArrayList
        Dim delimiter As String = Chr(9)
        checkFileExistenceToDownload()
        Dim i As Integer = 0
        'objAl = CType(sessHelper.GetSession("DeliveryVechileList"), ArrayList)

        For Each item As DataGridItem In dgListDeliveryVechile.Items
            Dim chk As CheckBox = CType(item.FindControl("chkSelection"), CheckBox)
            If (chk.Checked) Then
                oDeliveryCustomerHeader = CType(CType(sessHelper.GetSession("DeliveryVechileList"), ArrayList)(i), DeliveryCustomerHeader) 'oPQRHeaderFacade.Retrieve(CInt(dgListPQR.DataKeys().Item(i)))
                objAl.Add(oDeliveryCustomerHeader)
            End If
            i = i + 1
        Next

        If objAl.Count < 1 Then
            MessageBox.Show("Tidak ada data yg bisa di download")
            Return
        End If


        strText = New StringBuilder

        'Dealer
        strText.Append("Dealer")
        strText.Append(delimiter)

        'Tujuan
        strText.Append("Tujuan")
        strText.Append(delimiter)
        strText.Append("Nama")
        strText.Append(delimiter)

        'No.Reg D/O
        strText.Append("No.Reg D/O")
        strText.Append(delimiter)

        'Tgl Kirim
        strText.Append("Tgl Kirim")
        strText.Append(delimiter)

        'No.Reff D/O
        strText.Append("No.Reff D/O")
        strText.Append(delimiter)

        'Jml Unit
        strText.Append("Jml Unit")

        Try
            saveToTextFile(strText.ToString())
        Catch
            MessageBox.Show("Persiapan Proses Download gagal")
            Return
        End Try

        For count As Integer = 0 To objAl.Count - 1

            Dim objDeliveryCustomerHeader As DeliveryCustomerHeader = CType(objAl.Item(count), DeliveryCustomerHeader)
            strText = New StringBuilder

            'Dealer
            If IsNothing(objDeliveryCustomerHeader.FromDealer) Then
                strText.Append(delimiter)
            Else
                'strText.Append(objDeliveryCustomerHeader.FromDealer.DealerCode & "/" & objDeliveryCustomerHeader.FromDealer.DealerName)
                Dim objFromDealer As Dealer = New DealerFacade(User).Retrieve(objDeliveryCustomerHeader.FromDealer)
                strText.Append(objFromDealer.DealerCode & "/" & objFromDealer.DealerName)
                strText.Append(delimiter)
            End If

            'Tujuan
            If objDeliveryCustomerHeader.Customer Is Nothing And objDeliveryCustomerHeader.Dealer Is Nothing Then
                strText.Append(delimiter)
            Else
                If Not objDeliveryCustomerHeader.Customer Is Nothing Then
                    strText.Append(objDeliveryCustomerHeader.Customer.Code)
                    strText.Append(delimiter)
                    strText.Append(objDeliveryCustomerHeader.Customer.Name1)
                    strText.Append(delimiter)
                ElseIf Not objDeliveryCustomerHeader.Dealer Is Nothing Then
                    strText.Append(objDeliveryCustomerHeader.Dealer.DealerCode)
                    strText.Append(delimiter)
                    strText.Append(objDeliveryCustomerHeader.Dealer.DealerName)
                    strText.Append(delimiter)
                End If

            End If

            'No.Reg D/O
            strText.Append(objDeliveryCustomerHeader.RegDONumber)
            strText.Append(delimiter)

            'Tgl Kirim
            strText.Append(objDeliveryCustomerHeader.PostingDate.ToString("dd/MM/yyyy"))
            strText.Append(delimiter)

            'No.Reff D/O
            strText.Append(objDeliveryCustomerHeader.ReffDONumber)
            strText.Append(delimiter)

            'Jml Unit
            strText.Append(objDeliveryCustomerHeader.DeliveryCustomerDetails.Count.ToString("#,##0"))

            'Detail
            'strText.Append(GetDetailString(objDeliveryCustomerHeader))

            Try
                saveToTextFile(strText.ToString())
            Catch
                MessageBox.Show("Persiapan Proses Download gagal")
                Return
            End Try
        Next

        Response.Redirect("../downloadlocal.aspx?file=DataTemp\DeliveryVechileList" & Suffix & ".xls")

        MessageBox.Show("Data Telah Disimpan")

    End Sub
    Private Sub checkFileExistenceToDownload()
        Dim finfo As FileInfo = New FileInfo(Me.Server.MapPath("") & "\..\DataTemp\DeliveryVechileList" & Suffix & ".xls")
        If finfo.Exists Then
            finfo.Delete()
        End If
    End Sub
    Private Sub saveToTextFile(ByVal str As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then
                Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\DeliveryVechileList" & Suffix & ".xls", FileMode.Append, FileAccess.Write)
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
    Private Function GetDetailString(ByRef obj As DeliveryCustomerHeader) As String
        Dim delim As String = "-"
        Dim result As String = String.Empty
        If Not obj.DeliveryCustomerDetails Is Nothing Then
            For Each objDetail As DeliveryCustomerDetail In obj.DeliveryCustomerDetails
                result = result + objDetail.ChassisMaster.ChassisNumber + delim
            Next
        End If

        If result <> String.Empty Then
            result = result.Substring(0, result.Length - 1)
        End If
        Return result
    End Function
    Private Sub BindStatus()
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = New EnumSalesDeliveryVechile().RetrieveSalesDeliveryVechileStatus()
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub

    Private Sub BindTujuan()
        ddlTujuan.Items.Clear()
        ddlTujuan.Items.Add(New ListItem("Silakan Pilih", "Semua"))
        ddlTujuan.Items.Add(New ListItem("Dealer", "Dealer"))
        ddlTujuan.Items.Add(New ListItem("Customer", "Customer"))
    End Sub

    Private Sub SetStatusPanel()
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then  ' Dealer
            pnlChangeStatus.Visible = False
        ElseIf oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then   ' KTB
            pnlChangeStatus.Visible = True
            ddlStatus2.Items.Clear()
            ddlStatus2.Items.Add(New ListItem("Silakan Pilih", -1))
            ddlStatus2.Items.Add(New ListItem("Selesai", EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Selesai))
            ddlStatus2.Items.Add(New ListItem("Batal Selesai", EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Baru))
        End If
    End Sub
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitiateAuthorization()

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If Not IsPostBack Then
            BindStatus()
            BindTujuan()
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            ClearForm()
            GetSessionCriteria()
            BindToGrid(dgListDeliveryVechile.CurrentPageIndex)
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgListDeliveryVechile.CurrentPageIndex = 0
        BindToGrid(dgListDeliveryVechile.CurrentPageIndex)
    End Sub
    Private Sub dgListDeliveryVechile_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgListDeliveryVechile.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        Dim arlData As ArrayList = New ArrayList
        arlData = CType(sessHelper.GetSession("DeliveryVechileList"), ArrayList)
        'dgListDeliveryVechile.DataSource = CommonFunction.PageAndSortArraylist(arlData, dgListDeliveryVechile.CurrentPageIndex, dgListDeliveryVechile.PageSize, GetType(DeliveryCustomerHeader), ViewState("currSortColumn"), ViewState("currSortDirection"))
        dgListDeliveryVechile.DataSource = CommonFunction.SortArraylist(arlData, GetType(DeliveryCustomerHeader), ViewState("currSortColumn"), ViewState("currSortDirection"))
        'arlData = CommonFunction.PageAndSortArraylist(arlData, dgListDeliveryVechile.CurrentPageIndex, dgListDeliveryVechile.PageSize, GetType(DeliveryCustomerHeader), ViewState("currSortColumn"), ViewState("currSortDirection"))
        arlData = CommonFunction.SortArraylist(arlData, GetType(DeliveryCustomerHeader), ViewState("currSortColumn"), ViewState("currSortDirection"))
        sessHelper.SetSession("DeliveryVechileList", arlData)
        dgListDeliveryVechile.DataBind()

        Dim total As Integer = 0
        _arrDeliveryCustomerHeader = oDeliveryCustomerHeaderFacade.RetrieveByCriteria(CType(sessHelper.GetSession("CriteriaSesslstSlsDlvVeh"), CriteriaComposite), dgListDeliveryVechile.CurrentPageIndex + 1, dgListDeliveryVechile.PageSize, total, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        dgListDeliveryVechile.DataSource = _arrDeliveryCustomerHeader
        dgListDeliveryVechile.DataBind()

        'BindToGrid(dgListDeliveryVechile.CurrentPageIndex)

    End Sub
    Private Sub dgListDeliveryVechile_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgListDeliveryVechile.PageIndexChanged
        dgListDeliveryVechile.CurrentPageIndex = e.NewPageIndex
        'BindToGrid(dgListDeliveryVechile.CurrentPageIndex)
        Dim total As Integer = 0
        _arrDeliveryCustomerHeader = oDeliveryCustomerHeaderFacade.RetrieveByCriteria(CType(sessHelper.GetSession("CriteriaSesslstSlsDlvVeh"), CriteriaComposite), dgListDeliveryVechile.CurrentPageIndex + 1, dgListDeliveryVechile.PageSize, total, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        dgListDeliveryVechile.DataSource = _arrDeliveryCustomerHeader
        dgListDeliveryVechile.DataBind()

    End Sub
    Private Sub dgListDeliveryVechile_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgListDeliveryVechile.ItemDataBound

        If e.Item.ItemIndex >= 0 Then
            objDealer = CType(Session("DEALER"), Dealer)
            Dim RowValue As DeliveryCustomerHeader = CType(e.Item.DataItem, DeliveryCustomerHeader)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)
            Select Case CType(RowValue.Status, EnumSalesDeliveryVechile.SalesDeliveryVechileStatus)
                Case EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Baru
                    If bEditPriv Then
                        If RowValue.FromDealer = objDealer.ID And RowValue.RegDONumber <> "Buat Faktur" Then
                            lnkbtnEdit.Visible = True
                        Else
                            lnkbtnEdit.Visible = False
                        End If
                    Else
                        lnkbtnEdit.Visible = bEditPriv
                    End If


                    lnkbtnView.Visible = False
                    lblStatus.Text = EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Baru.ToString
                Case EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Selesai

                    If bEditPriv Then
                        lnkbtnEdit.Visible = False
                    Else
                        lnkbtnEdit.Visible = bEditPriv
                    End If

                    lnkbtnView.Visible = True

                    lblStatus.Text = EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Selesai.ToString
                Case EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Batal
                    If bEditPriv Then
                        lnkbtnEdit.Visible = False
                    Else
                        lnkbtnEdit.Visible = bEditPriv
                    End If
                    lnkbtnView.Visible = False
                    lblStatus.Text = EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Batal.ToString
            End Select

            Dim lblTglKirim As Label = CType(e.Item.FindControl("lblTglKirim"), Label)
            If RowValue.PostingDate <= "01/01/1900" Then
                lblTglKirim.Text = ""
            Else
                lblTglKirim.Text = RowValue.PostingDate.ToString("dd/MM/yyyy")
            End If

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1) + (dgListDeliveryVechile.CurrentPageIndex * dgListDeliveryVechile.PageSize)

            Dim lblTujuan As Label = CType(e.Item.FindControl("lblTujuan"), Label)
            Dim lblNama As Label = CType(e.Item.FindControl("lblNama"), Label)
            If Not RowValue.Dealer Is Nothing Then
                lblTujuan.Text = RowValue.Dealer.DealerCode
                lblNama.Text = RowValue.Dealer.DealerName
            ElseIf Not RowValue.Customer Is Nothing Then
                lblTujuan.Text = RowValue.Customer.Code
                lblNama.Text = RowValue.Customer.Name1
            End If

            Dim lblJmlUnit As Label = CType(e.Item.FindControl("lblJmlUnit"), Label)
            lblJmlUnit.Text = RowValue.DeliveryCustomerDetails.Count.ToString("#,##0")

            TotalUnit += CInt(lblJmlUnit.Text)

            Dim lblDetail As Label = CType(e.Item.FindControl("lblDetail"), Label)
            lblDetail.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../DealerReport/FrmPopUpSalesDeliveryVechileDetail.aspx?id=" & RowValue.ID, "", 310, 500, "ShowPopUp")
            lnkbtnView.Visible = bViewPriv
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Dim objFromDealer As Dealer = New DealerFacade(User).Retrieve(RowValue.FromDealer)
            lblDealer.Text = objFromDealer.DealerCode
            lblDealer.ToolTip = objFromDealer.SearchTerm1

        End If
    End Sub
    Private Sub dgListDeliveryVechile_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgListDeliveryVechile.ItemCommand
        Select Case e.CommandName
            Case "Edit"
                SetSessionCriteria()
                Server.Transfer("~/DealerReport/FrmEntrySalesDeliveryVechile.aspx?Mode=Edit&ID=" & e.CommandArgument)
            Case "View"
                SetSessionCriteria()
                Server.Transfer("~/DealerReport/FrmEntrySalesDeliveryVechile.aspx?Mode=View&ID=" & e.CommandArgument)
        End Select
    End Sub
    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Download()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim i As Integer = 0
        Dim ErrMessage As String = String.Empty
        Dim arrList As ArrayList = New ArrayList

        For Each item As DataGridItem In dgListDeliveryVechile.Items
            Dim chk As CheckBox = CType(item.FindControl("chkSelection"), CheckBox)
            If (chk.Checked) Then
                oDeliveryCustomerHeader = CType(CType(sessHelper.GetSession("DeliveryVechileList"), ArrayList)(i), DeliveryCustomerHeader) 'oPQRHeaderFacade.Retrieve(CInt(dgListPQR.DataKeys().Item(i)))
                Select Case ddlStatus2.SelectedItem.Text
                    Case "Selesai"
                        ' valid status = Baru
                        If oDeliveryCustomerHeader.Status = EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Baru Then
                            arrList.Add(oDeliveryCustomerHeader)
                        End If
                    Case "Batal Selesai"
                        ' valid status = selesai
                        If oDeliveryCustomerHeader.Status = EnumSalesDeliveryVechile.SalesDeliveryVechileStatus.Selesai Then
                            arrList.Add(oDeliveryCustomerHeader)
                        End If
                End Select
            End If
            i = i + 1
        Next

        If (arrList.Count > 0) Then
            If ddlStatus2.SelectedValue = -1 Then
                MessageBox.Show("Silakan Pilih Status Baru")
                Return
            Else
                If (oDeliveryCustomerHeaderFacade.UbahStatusSalesDeliveryVechile(arrList, ddlStatus2.SelectedValue, ErrMessage) = -1) Then
                    If ErrMessage = String.Empty Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(ErrMessage)
                    End If
                Else
                    MessageBox.Show(SR.SaveSuccess)
                    BindToGrid(dgListDeliveryVechile.CurrentPageIndex)
                End If
            End If

        Else
            MessageBox.Show("Tidak ada data yg di pilih atau data yg valid.")
        End If

    End Sub

#End Region

End Class
