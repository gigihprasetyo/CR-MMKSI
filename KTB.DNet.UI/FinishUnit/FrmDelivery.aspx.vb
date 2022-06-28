#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.Drawing.Color
Imports KTB.DNet.Utility.CommonFunction

#End Region

#Region ".NET Base Class Namespace Imports"

Imports System.IO
Imports System.Text

#End Region

Public Class FrmDelivery
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnRefresh As System.Web.UI.WebControls.Button
    Protected WithEvents dgDeliveryOrder As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ICDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblTotalBiayaParkir As System.Web.UI.WebControls.Label
    Dim dt As DateTime = DateTime.Now
    Dim suffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
    Protected WithEvents ICDari2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ICSampai2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents chkTglCetak As System.Web.UI.WebControls.CheckBox
    Protected WithEvents chkTglKeluar As System.Web.UI.WebControls.CheckBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTotalChassis As System.Web.UI.WebControls.Label
    Protected WithEvents dgTemp As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is requir    ed by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


    Private objDealer As Dealer
#Region "Custom Method"

    Private Sub bindDgDeliveryOrder(ByVal indexPage As Integer)
        'Helper used for improving performance
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim criteriaDOHelper As New CriteriaComposite(New Criteria(GetType(V_RekapDOHelper), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim objAl, objAl3, objAlHelper As ArrayList
        If validateCriteria(criterias, criteriaDOHelper) Then
            objAl3 = New ChassisMasterFacade(User).Retrieve(criterias)
            Dim objSessionHelper As New SessionHelper
            Dim sortCol As SortCollection = New SortCollection
            sortCol.Add(New Sort(GetType(ChassisMaster), "Dealer.DealerCode", Sort.SortDirection.ASC))
            sortCol.Add(New Sort(GetType(ChassisMaster), "VechileColor.VechileType.VechileTypeCode", Sort.SortDirection.ASC))

            Dim sortColHelper As SortCollection = New SortCollection
            sortColHelper.Add(New Sort(GetType(V_RekapDOHelper), "DealerCode", Sort.SortDirection.ASC))
            sortColHelper.Add(New Sort(GetType(V_RekapDOHelper), "VechileTypeCode", Sort.SortDirection.ASC))

            objAlHelper = New V_RekapDOHelperFacade(User).Retrieve(criteriaDOHelper, sortColHelper)

            objAl = New ChassisMasterFacade(User).Retrieve(criterias, sortCol)
            objAl = validateDisplay(objAl)

            Dim newList As ArrayList = PrepareGridView(objAl, objAlHelper)

            objSessionHelper.SetSession("chassisMasterAL", objAl3)
            objSessionHelper.SetSession("NewchassisMasterAL", newList)

            dgDeliveryOrder.CurrentPageIndex = indexPage

            If Not newList Is Nothing Then
                dgDeliveryOrder.DataSource = newList
                lblTotalChassis.Text = objAl.Count
                dgDeliveryOrder.VirtualItemCount = newList.Count
                dgDeliveryOrder.DataBind()
                btnDownload.Enabled = True
            Else
                dgDeliveryOrder.DataSource = Nothing
                dgDeliveryOrder.DataBind()
                MessageBox.Show("Data Tidak Ditemukan")
                btnDownload.Enabled = False
                lblTotalBiayaParkir.Text = ""
                lblTotalChassis.Text = 0
            End If

        End If
    End Sub
    Private Function PrepareGridView(ByVal OldList As ArrayList, ByVal OldListHelper As ArrayList) As ArrayList

        Dim NewList As New ArrayList
        Dim LastValidObj As ChassisMaster
        Dim LastValidHelper As V_RekapDOHelper
        Dim objhelper As V_RekapDOHelper
        NewList.Clear()
        If OldList.Count > 0 Then

            Dim counter As Integer = 0
            For Each obj As ChassisMaster In OldList
                objhelper = OldListHelper(counter)
                If LastValidObj Is Nothing Then
                    LastValidObj = obj
                    LastValidObj.Unit = 1
                    LastValidHelper = objhelper
                Else
                    If Not (LastValidHelper.VechileTypeCode = objhelper.VechileTypeCode AndAlso LastValidHelper.DealerCode = objhelper.DealerCode) Then
                        NewList.Add(LastValidObj)
                        LastValidObj = obj
                        LastValidObj.Unit = 1
                        LastValidHelper = objhelper
                    Else
                        LastValidObj.Unit = LastValidObj.Unit + 1
                        LastValidObj.ParkingAmount = LastValidObj.ParkingAmount + obj.ParkingAmount
                    End If
                End If
                counter += 1
            Next
            NewList.Add(LastValidObj)
        End If
        Return NewList
    End Function

    Private Function validateCriteria(ByRef criterias As CriteriaComposite, ByRef criteriaDOHelper As CriteriaComposite) As Boolean
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            criteriaDOHelper.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_RekapDOHelper), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        If txtKodeDealer.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.DealerCode", MatchType.InSet, seperatePopUpReturn(txtKodeDealer.Text.Trim())))
            criteriaDOHelper.opAnd(New Criteria(GetType(V_RekapDOHelper), "DealerCode", MatchType.InSet, seperatePopUpReturn(txtKodeDealer.Text.Trim())))
        End If
        If chkTglCetak.Checked Then
            If DateDiff(DateInterval.Day, CType(ICDari.Value, Date), CType(ICSampai.Value, Date)) >= 0 Then
                If ICSampai.Value.Subtract(ICDari.Value).Days > 65 Then
                    MessageBox.Show("Periode tidak boleh melebihi 65 hari")
                    Return False
                Else
                    criterias.opAnd(New Criteria(GetType(ChassisMaster), "DODate", MatchType.GreaterOrEqual, ICDari.Value))
                    criterias.opAnd(New Criteria(GetType(ChassisMaster), "DODate", MatchType.LesserOrEqual, ICSampai.Value))
                    criteriaDOHelper.opAnd(New Criteria(GetType(V_RekapDOHelper), "DODate", MatchType.GreaterOrEqual, ICDari.Value))
                    criteriaDOHelper.opAnd(New Criteria(GetType(V_RekapDOHelper), "DODate", MatchType.LesserOrEqual, ICSampai.Value))
                End If
            Else
                MessageBox.Show("Periode dari tidak boleh lebih besar daripada periode sampai")
                Return False
            End If

        End If
        If chkTglKeluar.Checked Then
            If DateDiff(DateInterval.Day, CType(ICDari2.Value, Date), CType(ICSampai2.Value, Date)) >= 0 Then
                If ICSampai2.Value.Subtract(ICDari2.Value).Days > 65 Then
                    MessageBox.Show("Periode tidak boleh melebihi 65 hari")
                    Return False
                Else
                    criterias.opAnd(New Criteria(GetType(ChassisMaster), "GIDate", MatchType.GreaterOrEqual, ICDari2.Value))
                    criterias.opAnd(New Criteria(GetType(ChassisMaster), "GIDate", MatchType.LesserOrEqual, ICSampai2.Value))
                    criteriaDOHelper.opAnd(New Criteria(GetType(V_RekapDOHelper), "GIDate", MatchType.GreaterOrEqual, ICDari2.Value))
                    criteriaDOHelper.opAnd(New Criteria(GetType(V_RekapDOHelper), "GIDate", MatchType.LesserOrEqual, ICSampai2.Value))
                End If
            Else
                MessageBox.Show("Periode dari tidak boleh lebih besar daripada periode sampai")
                Return False
            End If
        End If

        If ddlStatus.SelectedValue <> "" Then
            If ddlStatus.SelectedValue = "Keluar" Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "GIDate", MatchType.No, "1/1/1900"))
                criteriaDOHelper.opAnd(New Criteria(GetType(V_RekapDOHelper), "GIDate", MatchType.No, "1/1/1900"))
            ElseIf ddlStatus.SelectedValue = "Belum Keluar" Then
                criterias.opAnd(New Criteria(GetType(ChassisMaster), "GIDate", MatchType.Exact, "1/1/1900"))
                criteriaDOHelper.opAnd(New Criteria(GetType(V_RekapDOHelper), "GIDate", MatchType.Exact, "1/1/1900"))
            End If
        End If
        Return True
    End Function
    Private Function validateDisplay(ByVal objAl As ArrayList) As ArrayList
        Dim objAl2 As New ArrayList
        Dim objChassisMaster As ChassisMaster
        Dim dblParkingFeetotal As Double = 0

        For Each objChassisMaster In objAl

            If objChassisMaster.GIDate < "1/1/1901" Then

                Dim temp As Integer = DateDiff(DateInterval.Day, objChassisMaster.DODate, Today) + 1
                'Free parking for 10 days
                temp -= 10
                If temp >= 0 Then
                    objChassisMaster.ParkingDays = temp
                    If temp <= 20 Then
                        objChassisMaster.ParkingAmount = temp * 10000
                    Else
                        objChassisMaster.ParkingAmount = ((temp - 20) * 20000) + 200000
                    End If
                Else
                    objChassisMaster.ParkingDays = 0
                    objChassisMaster.ParkingAmount = 0
                End If
            Else
                Dim temp As Integer = DateDiff(DateInterval.Day, objChassisMaster.DODate, objChassisMaster.GIDate) + 1
                'Free parking for 10 days
                temp -= 10
                If temp >= 0 Then
                    objChassisMaster.ParkingDays = temp
                    If temp <= 20 Then
                        objChassisMaster.ParkingAmount = temp * 10000
                    Else
                        objChassisMaster.ParkingAmount = ((temp - 20) * 20000) + 200000
                    End If
                Else
                    objChassisMaster.ParkingDays = 0
                    objChassisMaster.ParkingAmount = 0
                End If
            End If
            dblParkingFeetotal += objChassisMaster.ParkingAmount
            objAl2.Add(objChassisMaster)
        Next
        lblTotalBiayaParkir.Text = dblParkingFeetotal.ToString("#,###")
        Return objAl2
    End Function

    Private Sub dgDeliveryOrderBinding(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim RowValue As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)
            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim lblType As Label = CType(e.Item.FindControl("lblType"), Label)
            Dim lblUnit As Label = CType(e.Item.FindControl("lblUnit"), Label)

            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            lblID.Text = RowValue.ID

            Try
                lblDealer.Text = RowValue.Dealer.DealerCode
                lblDealerName.Text = RowValue.Dealer.DealerName
                lblType.Text = RowValue.VechileType
                lblUnit.Text = RowValue.Unit

            Catch ex As Exception
                lblDealer.Text = ""
            End Try
            Dim lblIndex As Label = CType(e.Item.FindControl("lblIndex"), Label)
            lblIndex.Text = e.Item.ItemIndex + 1 + (dgDeliveryOrder.CurrentPageIndex * dgDeliveryOrder.PageSize)
        End If
    End Sub

    Private Function download() As ArrayList
        Dim strText As StringBuilder
        Dim objAl As New ArrayList
        Dim delimiter As String = Chr(9)
        checkFileExistenceToDownload()

        objAl = CType(Session.Item("NewchassisMasterAL"), ArrayList)

        'Write Header
        strText = New StringBuilder
        strText.Append("Dealer Code")
        strText.Append(delimiter)
        strText.Append("Dealer Name")
        strText.Append(delimiter)
        strText.Append("Type")
        strText.Append(delimiter)
        strText.Append("Unit")
        strText.Append(delimiter)
        strText.Append("Biaya Parkir")
        strText.Append(delimiter)
        saveToTextFile(strText.ToString())

        For count As Integer = 0 To objAl.Count - 1
            Dim RowValue As ChassisMaster = CType(objAl.Item(count), ChassisMaster)

            strText = New StringBuilder

            Try
                strText.Append(RowValue.Dealer.DealerCode)
                strText.Append(delimiter)
                strText.Append(RowValue.Dealer.SearchTerm1)
                strText.Append(delimiter)
                strText.Append(RowValue.VechileColor.VechileType.VechileTypeCode)
                strText.Append(delimiter)
                strText.Append(RowValue.Unit)
                strText.Append(delimiter)
                strText.Append(RowValue.ParkingAmount.ToString("####"))
                strText.Append(delimiter)

                saveToTextFile(strText.ToString())
            Catch
            End Try
        Next
        Response.Redirect("../downloadlocal.aspx?file=DataTemp\delivery_" & suffix & ".txt")
        MessageBox.Show("Data Telah Disimpan")
    End Function
    Private Sub checkFileExistenceToDownload()
        Dim finfo As FileInfo = New FileInfo(Me.Server.MapPath("") & "\..\DataTemp\delivery_" & suffix & ".txt")

        If finfo.Exists Then
            finfo.Delete()
        End If

    End Sub
    Private Sub saveToTextFile(ByVal str As String)

        Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\delivery_" & suffix & ".txt", FileMode.Append, FileAccess.Write)
        Dim objStreamWriter As New StreamWriter(objFileStream)

        objStreamWriter.WriteLine(str)
        objStreamWriter.Close()

    End Sub
    Private Function isExistCode(ByVal sCode As String) As Boolean

        Dim objChassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
        Return objChassisMasterFacade.ValidateCode(sCode) > 0

    End Function
    Private Sub assignAttributeControl()
        lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
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

    Private Sub bindGridSorting(ByVal indexPage As Integer)
        If (indexPage >= 0) Then
            Dim objChassisMasterAl As ArrayList = CType(Session.Item("NewchassisMasterAL"), ArrayList)
            SortArraylist(objChassisMasterAl, GetType(ChassisMaster), CType(viewstate("currentSortColumn"), String), CType(viewstate("currentSortDirection"), Sort.SortDirection))
            dgDeliveryOrder.DataSource = objChassisMasterAl
            dgDeliveryOrder.DataBind()
            Dim objSessionHelper As New SessionHelper
            objSessionHelper.SetSession("NewchassisMasterAL", objChassisMasterAl)
        End If
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            ViewState("currentSortColumn") = "Dealer.DealerCode"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            CheckUserPrivelege()
            assignAttributeControl()
            bindDdlStatus()
        End If

    End Sub
    Private Sub CheckUserPrivelege()
        If Not SecurityProvider.Authorize(Context.User, SR.ListParkView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Parkir")
        End If
        btnDownload.Visible = SecurityProvider.Authorize(Context.User, SR.ListParkDowload_Privilege)
    End Sub
    Private Sub bindDdlStatus()
        ddlStatus.Items.Insert(0, New ListItem("Pilih Status", ""))
        ddlStatus.Items.Insert(1, New ListItem("Keluar", "Keluar"))
        ddlStatus.Items.Insert(2, New ListItem("Belum Keluar", "Belum Keluar"))
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If (Not chkTglCetak.Checked And Not chkTglKeluar.Checked) Then
            MessageBox.Show("Periode tanggal belum dipilih !")
        Else
            dgDeliveryOrder.CurrentPageIndex = 0
            bindDgDeliveryOrder(0)
        End If
    End Sub
    Private Sub dgDeliveryOrder_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDeliveryOrder.ItemDataBound
        dgDeliveryOrderBinding(e)
    End Sub
    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Try
            download()
            bindDgDeliveryOrder(0)
        Catch ex As Exception
            MessageBox.Show(ex.Message())
        End Try

    End Sub

    Private Sub dgDeliveryOrder_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDeliveryOrder.PageIndexChanged
        dgDeliveryOrder.SelectedIndex = -1
        dgDeliveryOrder.CurrentPageIndex = e.NewPageIndex
        bindDgDeliveryOrder(dgDeliveryOrder.CurrentPageIndex)
    End Sub

    Private Sub dgDeliveryOrder_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDeliveryOrder.SortCommand
        If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    viewstate("currentSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    viewstate("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            viewstate("currentSortColumn") = e.SortExpression
            viewstate("currentSortDirection") = Sort.SortDirection.ASC
        End If

        dgDeliveryOrder.SelectedIndex = -1
        dgDeliveryOrder.CurrentPageIndex = 0
        bindGridSorting(dgDeliveryOrder.CurrentPageIndex)
    End Sub
#End Region
End Class