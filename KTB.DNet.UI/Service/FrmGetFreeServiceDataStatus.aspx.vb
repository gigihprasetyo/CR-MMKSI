#Region " Customer Name Space "

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports System.IO
Imports System.Text
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmGetFreeServiceDataStatus
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnRefresh As System.Web.UI.WebControls.Button
    Protected WithEvents dgFreeService As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ICDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents txtChassisNo As System.Web.UI.WebControls.TextBox
    Dim dt As DateTime = DateTime.Now
    Dim Suffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
    'Dim suffix As String = New Random().Next(10000).ToString()
    Protected WithEvents ICSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerBranchCode As TextBox
    Protected WithEvents txtBranchName As TextBox
    Protected WithEvents lblSearchDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents ddlRejectStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlFSType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCategori As System.Web.UI.WebControls.Label
    Protected WithEvents lblCategori2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlEvidence As System.Web.UI.WebControls.DropDownList

    Protected WithEvents txtNomorRangka As TextBox
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    'Added by Julius
    Private objDealer As Dealer
    Private _sessHelper As SessionHelper = New SessionHelper

#Region " subs & function "

    Private Function RetriveDataAndSaveToCache() As Integer
        _sessHelper.RemoveSession("objFreeService")

        Dim criterias As New CriteriaComposite(New Criteria(GetType(V_FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(V_FreeService), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(V_FreeService), "DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim.Replace(";", "','") & "')"))
        End If

        If txtDealerBranchCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(V_FreeService), "DealerBranchCode", MatchType.InSet, "('" & txtDealerBranchCode.Text.Trim.Replace(";", "','") & "')"))
        End If

        If ddlRejectStatus.SelectedValue <> "Semua" Then
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Reject", MatchType.Exact, ddlRejectStatus.SelectedValue.ToString()))
            'Start  : by:dna;for:Rina;Remark:on:20100608;Show all data when SAP isn't upload data to dnet yet.
            If ddlStatus.SelectedItem.Text.Trim.ToLower = "Selesai".ToLower Then
                criterias.opAnd(New Criteria(GetType(V_FreeService), "Reject", MatchType.Exact, ddlRejectStatus.SelectedValue.ToString()))
            Else
                'and (--NotificationType
                '   fs.Status=3 and fs.NotificationType='Z1' or  --Z1 or Z3
                '   fs.Status<>3 and fs.NotificationType=''
                ')
                criterias.opAnd(New Criteria(GetType(V_FreeService), "Status", MatchType.Exact, CType(EnumFSStatus.FSStatus.Selesai, String)), "(", True)
                criterias.opAnd(New Criteria(GetType(V_FreeService), "Reject", MatchType.Exact, ddlRejectStatus.SelectedValue.ToString()))
                criterias.opOr(New Criteria(GetType(V_FreeService), "Status", MatchType.No, CType(EnumFSStatus.FSStatus.Selesai, String)))
                criterias.opAnd(New Criteria(GetType(V_FreeService), "Reject", MatchType.Exact, ""), ")", False)
            End If
            'Start  : by:dna;for:Rina;Remark:on:20100608;Show all data when SAP isn't upload data to dnet yet.
        End If

        If ddlFSType.SelectedValue <> "0" Then
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "NotificationType", MatchType.Exact, ddlFSType.SelectedValue.ToString()))
            'Start  : by:dna;for:Rina;Remark:on:20100608;Show all data when SAP isn't upload data to dnet yet.
            'start cr doni enhancement service
            If ddlFSType.SelectedValue.ToString() = "Z3" OrElse ddlFSType.SelectedValue.ToString() = "Z1" Then
                Dim FStype As String = String.Empty
                If ddlFSType.SelectedValue.ToString() = "Z1" Then
                    FStype = "0"
                ElseIf ddlFSType.SelectedValue.ToString() = "Z3" Then
                    FStype = "1"
                End If
                If ddlStatus.SelectedItem.Text.Trim.ToLower = "Selesai".ToLower Then
                    criterias.opAnd(New Criteria(GetType(V_FreeService), "FSType", MatchType.Exact, FStype))
                    'criterias.opAnd(New Criteria(GetType(V_FreeService), "NotificationType", MatchType.Exact, ddlFSType.SelectedValue.ToString())) 'comment, request doni gc
                Else
                    'and (--NotificationType
                    '   fs.Status=3 and fs.NotificationType='Z1' or  --Z1 or Z3
                    '   fs.Status<>3 and fs.NotificationType=''
                    ')
                    criterias.opAnd(New Criteria(GetType(V_FreeService), "Status", MatchType.Exact, CType(EnumFSStatus.FSStatus.Selesai, String)), "(", True)
                    criterias.opAnd(New Criteria(GetType(V_FreeService), "FSType", MatchType.Exact, FStype))
                    criterias.opOr(New Criteria(GetType(V_FreeService), "Status", MatchType.No, CType(EnumFSStatus.FSStatus.Selesai, String)))
                    criterias.opAnd(New Criteria(GetType(V_FreeService), "FSType", MatchType.Exact, "''"), ")", False)
                    'End    : by:dna;for:Rina;Remark:on:20100608;Show all data when SAP isn't upload data to dnet yet.
                End If
            Else
                'start cr doni enhancement FStype
                criterias.opAnd(New Criteria(GetType(V_FreeService), "FSType", MatchType.Exact, ddlFSType.SelectedValue.ToString()))
                'end cr doni enhancement service
            End If

        End If

        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Dealer.ID", MatchType.Exact, objDealer.ID))
        'Else
        '    If txtKodeDealer.Visible And txtKodeDealer.Text <> String.Empty Then
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeService), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        '    End If
        'End If
        If Me.ddlCategory.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(V_FreeService), "CategoryID", MatchType.Exact, Me.ddlCategory.SelectedValue))
        End If

        If Not String.IsNullOrEmpty(txtNomorRangka.Text) Then
            criterias.opAnd(New Criteria(GetType(V_FreeService), "ChassisNumber", MatchType.Exact, txtNomorRangka.Text.Trim))
        End If
        If Not validateCriteria(criterias) Then
            Return -1
        End If

        If ddlEvidence.SelectedValue = 1 Then
            criterias.opAnd(New Criteria(GetType(V_FreeService), "FileName", MatchType.No, ""))
        ElseIf ddlEvidence.SelectedValue = 2 Then
            criterias.opAnd(New Criteria(GetType(V_FreeService), "FileName", MatchType.Exact, ""))
        End If

        Dim objFreeServiceAl As ArrayList

        Dim sortColl As SortCollection = New SortCollection
        'sortColl.Add(New Sort(GetType(ChassisMaster), "ChassisNumber", Sort.SortDirection.ASC))  '-- Nomor chassis
        sortColl.Add(New Sort(GetType(V_FreeService), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))  '-- Nomor chassis

        objFreeServiceAl = New V_FreeServiceFacade(User).Retrieve(criterias, sortColl)
        If objFreeServiceAl.Count = 0 Then
            Return -2
        Else

        End If

        _sessHelper.SetSession("objFreeService", objFreeServiceAl)
        'ViewState("CurrentSortColumn") = "Status"
        'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        btnDownload.Enabled = True
        Return 0
    End Function

    Private Sub DataBindGrid()
        Dim objFreeService As ArrayList = _sessHelper.GetSession("objFreeService")
        If objFreeService Is Nothing Or objFreeService.Count <= 0 Then
            dgFreeService.DataSource = Nothing
            dgFreeService.DataBind()
            Exit Sub
        End If
        'Sort
        'Dim isAsc As Boolean = ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        'Dim iCmp As IComparer = New ListComparer(isAsc, ViewState("CurrentSortColumn"))
        'objFreeService.Sort(iCmp)

        dgFreeService.DataSource = objFreeService
        dgFreeService.DataBind()
    End Sub

    Private Function validateCriteria(ByRef criterias As CriteriaComposite) As Boolean

        If ICDari.Value.ToString <> "" And ICSampai.Value.ToString <> "" Then

            If ICSampai.Value.Subtract(ICDari.Value).Days < 0 Then
                MessageBox.Show("Kriteria tanggal Rilis tidak valid.")
                Return False
            End If

            If ICSampai.Value.Subtract(ICDari.Value).Days > 65 Then
                MessageBox.Show("Periode Rilis tidak boleh melebihi 65 hari")
                Return False
            End If
            Dim startDate As DateTime = New DateTime(ICDari.Value.Year, ICDari.Value.Month, ICDari.Value.Day, 0, 0, 0)
            Dim endDate As DateTime = New DateTime(ICSampai.Value.Year, ICSampai.Value.Month, ICSampai.Value.Day, 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(V_FreeService), "ReleaseDate", MatchType.GreaterOrEqual, startDate))
            criterias.opAnd(New Criteria(GetType(V_FreeService), "ReleaseDate", MatchType.LesserOrEqual, endDate))
        End If

        If ddlStatus.SelectedItem.Value.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(V_FreeService), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        Return True
    End Function

    Private Sub BindItemdgChassisNumber(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            ItemTypeDataBound(e)
        ElseIf e.Item.ItemType = ListItemType.Footer Then
            FooterTypeDataBound(e)
        End If

    End Sub

    Private Sub ItemTypeDataBound(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim RowValue As V_FreeService = CType(e.Item.DataItem, V_FreeService)

        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblAlasan As Label = CType(e.Item.FindControl("lblAlasan"), Label)
        Dim lblDeskripsi As Label = CType(e.Item.FindControl("lblDeskripsi"), Label)
        Dim lblTanggalJual As Label = CType(e.Item.FindControl("lblTanggalJual"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

        Dim lblLabourAmount As Label = CType(e.Item.FindControl("lblLabourAmount"), Label)
        'Dim lblPartAmount As Label = CType(e.Item.FindControl("lblPartAmount"), Label)
        Dim lbtnPartAmount As LinkButton = CType(e.Item.FindControl("lbtnPartAmount"), LinkButton)
        Dim lblPPNAmount As Label = CType(e.Item.FindControl("lblPPNAmount"), Label)
        Dim lblPPHAmount As Label = CType(e.Item.FindControl("lblPPHAmount"), Label)
        Dim lblNotifikasi As Label = CType(e.Item.FindControl("lblNotifikasi"), Label)
        Dim lblVisitType As Label = CType(e.Item.FindControl("lblVisitType"), Label)
        Dim lblCashback As Label = CType(e.Item.FindControl("lblCashback"), Label)
        Dim lbtnDownload As LinkButton = CType(e.Item.FindControl("lbtnDownload"), LinkButton)
        Dim lblEvStatus As Label = CType(e.Item.FindControl("lblEvStatus"), Label)

        lblNo.Text = (e.Item.ItemIndex + 1) + (dgFreeService.CurrentPageIndex * dgFreeService.PageSize)

        If RowValue.VisitType.ToUpper() = "WI" Then
            lblVisitType.Text = "Walk In"
        ElseIf RowValue.VisitType.ToUpper() = "BO" Then
            lblVisitType.Text = "Booking"
        End If

        lblAlasan.Visible = RowValue.Reject = "DAPP"

        lblNotifikasi.Visible = RowValue.NotificationNumber <> "0"

        If RowValue.SoldDate = "01/01/1900" Then
            lblTanggalJual.Text = ""
        Else
            lblTanggalJual.Text = RowValue.SoldDate.ToString("dd/MM/yyyy")
        End If

        If RowValue.Status = EnumFSStatus.FSStatus.Baru Then
            lblStatus.Text = "Baru"
        ElseIf RowValue.Status = EnumFSStatus.FSStatus.Proses Then
            lblStatus.Text = "Proses"
            'ElseIf RowValue.Status = EnumFSStatus.FSStatus.Rilis Then
            '    lblStatus.Text = "Rilis"
        ElseIf RowValue.Status = EnumFSStatus.FSStatus.Selesai Then
            lblStatus.Text = "Selesai"
        End If

        lblLabourAmount.Text = Format(RowValue.LabourAmount, "#,###")
        'lblPartAmount.Text = Format(RowValue.PartAmount, "#,###")
        lbtnPartAmount.Text = Format(RowValue.PartAmount, "#,###")
        lbtnPartAmount.Attributes("onclick") = "ShowPartDetail(" + CType(RowValue.ID, String) + ")"

        lblPPNAmount.Text = Format(RowValue.PPNAmount, "#,###")
        lblPPHAmount.Text = Format(RowValue.PPHAmount, "#,###")
        lblCashback.Text = Format(RowValue.CashBack, "#,###")

        If RowValue.FilePath = String.Empty Then
            lbtnDownload.Visible = False
            lblEvStatus.Text = "Tidak terlampir"
        Else
            lblEvStatus.Text = "Terlampir"
        End If
    End Sub

    Private Sub FooterTypeDataBound(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblGrandTotalA As Label = CType(e.Item.FindControl("lblGrandTotalA"), Label)
        Dim lblGrandTotalD As Label = CType(e.Item.FindControl("lblGrandTotalD"), Label)

        Dim lblTotalNew As Label = CType(e.Item.FindControl("lblTotalNew"), Label)
        Dim lblTotalProcessed As Label = CType(e.Item.FindControl("lblTotalProcessed"), Label)

        Dim lblTotalApp As Label = CType(e.Item.FindControl("lblTotalApp"), Label)
        Dim lblTotalDisapp As Label = CType(e.Item.FindControl("lblTotalDisapp"), Label)

        Dim lblTotalLabourAmountA As Label = CType(e.Item.FindControl("lblTotalLabourAmountA"), Label)
        Dim lblTotalLabourAmountD As Label = CType(e.Item.FindControl("lblTotalLabourAmountD"), Label)

        Dim lblTotalPartAmountA As Label = CType(e.Item.FindControl("lblTotalPartAmountA"), Label)
        Dim lblTotalPartAmountD As Label = CType(e.Item.FindControl("lblTotalPartAmountD"), Label)

        Dim lblTotalPPNAmountA As Label = CType(e.Item.FindControl("lblTotalPPNAmountA"), Label)
        Dim lblTotalPPNAmountD As Label = CType(e.Item.FindControl("lblTotalPPNAmountD"), Label)

        Dim lblTotalPPHAmountA As Label = CType(e.Item.FindControl("lblTotalPPHAmountA"), Label)
        Dim lblTotalPPHAmountD As Label = CType(e.Item.FindControl("lblTotalPPHAmountD"), Label)

        Dim lblCashbackA As Label = CType(e.Item.FindControl("lblCashbackA"), Label)
        Dim lblCashbackD As Label = CType(e.Item.FindControl("lblCashbackD"), Label)

        Dim totalOpen As Integer = 0
        Dim totalInProcess As Integer = 0
        Dim totalA As Integer = 0
        Dim totalD As Integer = 0
        Dim totalLabourAmountA As Double = 0
        Dim totalLabourAmountD As Double = 0
        Dim totalPartAmountA As Double = 0
        Dim totalPartAmountD As Double = 0
        Dim totalPPNAmountA As Double = 0
        Dim totalPPNAmountD As Double = 0
        Dim totalPPHAmountA As Double = 0
        Dim totalPPHAmountD As Double = 0
        Dim totalAmountA As Double = 0
        Dim totalAmountD As Double = 0
        Dim totalCashBackA As Double = 0
        Dim totalCashBackD As Double = 0

        GetTotal(totalOpen, totalInProcess, totalA, totalD, totalLabourAmountA, _
            totalLabourAmountD, totalPartAmountA, totalPartAmountD, totalPPNAmountA, _
            totalPPNAmountD, totalPPHAmountA, totalPPHAmountD, totalAmountA, _
            totalAmountD, totalCashBackA, totalCashBackD)

        lblGrandTotalA.Text = Format(totalAmountA, "#,###")
        lblGrandTotalD.Text = Format(totalAmountD, "#,###")

        lblTotalNew.Text = CStr(totalOpen)
        lblTotalProcessed.Text = CStr(totalInProcess)

        lblTotalApp.Text = CStr(totalA)
        lblTotalDisapp.Text = CStr(totalD)

        lblTotalLabourAmountA.Text = Format(totalLabourAmountA, "#,###")
        lblTotalLabourAmountD.Text = Format(totalLabourAmountD, "#,###")

        lblTotalPartAmountA.Text = Format(totalPartAmountA, "#,###")
        lblTotalPartAmountD.Text = Format(totalPartAmountD, "#,###")

        lblTotalPPNAmountA.Text = Format(totalPPNAmountA, "#,###")
        lblTotalPPNAmountD.Text = Format(totalPPNAmountD, "#,###")

        lblTotalPPHAmountA.Text = Format(totalPPHAmountA, "#,###")
        lblTotalPPHAmountD.Text = Format(totalPPHAmountD, "#,###")

        lblCashbackA.Text = Format(totalCashBackA, "#,###")
        lblCashbackD.Text = Format(totalCashBackD, "#,###")
    End Sub

    Private Sub bindDdlStatus()

        Dim listTitle As New EnumFSStatus
        Dim al2 As ArrayList = listTitle.RetrieveFSStatus
        For Each item As EnumFS In al2
            If item.NameFSStatus <> EnumFSStatus.FSStatus.Rilis.ToString().ToUpper() And item.NameFSStatus.ToUpper <> EnumFSStatus.FSStatus.Baru.ToString().ToUpper() Then
                ddlStatus.Items.Add(New ListItem(item.NameFSStatus, item.ValFSStatus))
            End If
        Next
        ddlStatus.Items.Insert(0, New ListItem("Pilih Semua", ""))

    End Sub

    Private Sub GetTotal( _
        ByRef totalOpen As Integer, _
        ByRef totalInProcess As Integer, _
        ByRef totalA As Integer, _
        ByRef totalD As Integer, _
        ByRef totalLabourAmountA As Double, _
        ByRef totalLabourAmountD As Double, _
        ByRef totalPartAmountA As Double, _
        ByRef totalPartAmountD As Double, _
        ByRef totalPPNAmountA As Double, _
        ByRef totalPPNAmountD As Double, _
        ByRef totalPPHAmountA As Double, _
        ByRef totalPPHAmountD As Double, _
        ByRef totalAmountA As Double, _
        ByRef totalAmountD As Double, _
        ByRef totalCashbackA As Double, _
        ByRef totalCashbackD As Double
        )

        Dim objAL As New ArrayList
        objAL = CType(_sessHelper.GetSession("objFreeService"), ArrayList)

        Dim intTotalOpen As Integer = 0
        Dim intTotalInProcess As Integer = 0
        Dim intTotalApprove As Integer = 0
        Dim intTotalDisapprove As Integer = 0

        Dim dblLabourAmountApp As Double = 0
        Dim dblPartAmountApp As Double = 0
        Dim dblPPNAmountApp As Double = 0
        Dim dblPPHAmountApp As Double = 0
        Dim dblCashbackApp As Double = 0
        Dim dblLabourAmountDisapp As Double = 0
        Dim dblPartAmountDisapp As Double = 0
        Dim dblPPNAmountDisapp As Double = 0
        Dim dblPPHAmountDisapp As Double = 0
        Dim dblCashbackDisapp As Double = 0


        For count As Integer = 0 To objAL.Count - 1

            Dim objFreeService As V_FreeService = CType(objAL.Item(count), V_FreeService)

            If Not IsNothing(objFreeService.Status) And Not objFreeService.Status = "" Then

                If objFreeService.Status = CType(EnumFSStatus.FSStatus.Baru, String) Then
                    intTotalOpen += 1
                End If

                If objFreeService.Status = CType(EnumFSStatus.FSStatus.Proses, String) Then
                    intTotalInProcess += 1
                End If
            End If

            If Not IsNothing(objFreeService.Reject) And Not objFreeService.Reject = "" Then

                If objFreeService.Reject.ToLower = "app" Then
                    intTotalApprove += 1
                    dblLabourAmountApp += objFreeService.LabourAmount
                    dblPartAmountApp += objFreeService.PartAmount
                    dblPPNAmountApp += objFreeService.PPNAmount
                    dblPPHAmountApp += objFreeService.PPHAmount
                    dblCashbackApp += objFreeService.CashBack
                End If

                If objFreeService.Reject.ToLower = "dapp" Then
                    intTotalDisapprove += 1
                    dblLabourAmountDisapp += objFreeService.LabourAmount
                    dblPartAmountDisapp += objFreeService.PartAmount
                    dblPPNAmountDisapp += objFreeService.PPNAmount
                    dblPPHAmountDisapp += objFreeService.PPHAmount
                    dblCashbackDisapp += objFreeService.CashBack
                End If

            End If

        Next

        totalOpen = intTotalOpen
        totalInProcess = intTotalInProcess
        totalA = intTotalApprove
        totalD = intTotalDisapprove

        totalLabourAmountA = dblLabourAmountApp
        totalLabourAmountD = dblLabourAmountDisapp
        totalPartAmountA = dblPartAmountApp
        totalPartAmountD = dblPartAmountDisapp
        totalPPNAmountA = dblPPNAmountApp
        totalPPNAmountD = dblPPNAmountDisapp
        totalPPHAmountA = dblPPHAmountApp
        totalPPHAmountD = dblPPHAmountDisapp
        totalCashbackA = dblCashbackApp
        totalCashbackD = dblCashbackDisapp

        totalAmountA = dblLabourAmountApp + dblPartAmountApp + dblPPNAmountApp + dblPPHAmountApp + dblCashbackApp
        totalAmountD = dblLabourAmountDisapp + dblPartAmountDisapp + dblPPNAmountDisapp + dblPPHAmountDisapp + dblCashbackDisapp
    End Sub

    Private Sub download()
        Dim strText As StringBuilder
        Dim objAl As New ArrayList
        Dim delimiter As String = Chr(9)
        checkFileExistenceToDownload()

        objAl = CType(_sessHelper.GetSession("objFreeService"), ArrayList)

        For count As Integer = 0 To objAl.Count - 1

            Dim objFreeService As V_FreeService = CType(objAl.Item(count), V_FreeService)
            strText = New StringBuilder

            'Dealer
            'If IsNothing(objFreeService.Dealer) Then
            '    strText.Append(delimiter)
            'Else
            strText.Append(objFreeService.DealerCode.ToString)
            strText.Append(delimiter)
            'End If

            'DealerBranch
            strText.Append(objFreeService.DealerBranchCode.ToString)
            strText.Append(delimiter)

            'Chassis
            strText.Append(objFreeService.ChassisNumber.ToString)
            strText.Append(delimiter)

            'Kind
            strText.Append(objFreeService.KindCode.ToString)
            strText.Append(delimiter)

            'WorkOrderNumber
            strText.Append(objFreeService.WorkOrderNumber.ToString)
            strText.Append(delimiter)

            'Service Date
            strText.Append(objFreeService.ServiceDate.ToString("dd/MM/yyyy"))
            strText.Append(delimiter)

            'Sold Date
            If objFreeService.SoldDate < New Date(1901, 1, 1) Then
                strText.Append("")
            Else
                strText.Append(objFreeService.SoldDate.ToString("dd/MM/yyyy"))
            End If
            strText.Append(delimiter)

            'MileAge
            strText.Append(objFreeService.MileAge.ToString)
            strText.Append(delimiter)

            'NotifType
            If IsNothing(objFreeService.NotificationType) Then
                strText.Append("")
            Else
                strText.Append(objFreeService.NotificationType.ToString)
            End If
            strText.Append(delimiter)

            'NotifNumber
            If IsNothing(objFreeService.NotificationNumber) OrElse objFreeService.NotificationNumber = "" OrElse objFreeService.NotificationNumber = "0" Then
                strText.Append("")
            Else
                strText.Append(objFreeService.NotificationNumber.ToString)
            End If
            strText.Append(delimiter)

            'Status
            If IsNothing(objFreeService.Reject) Then
                strText.Append("")
            Else
                strText.Append(objFreeService.Reject)
            End If
            strText.Append(delimiter)

            'Alasan
            If objFreeService.Reject = "DAPP" Then
                'If IsNothing(objFreeService.Reason) Then
                '    strText.Append("")
                'Else
                If objFreeService.ReasonCode <> "" Then
                    strText.Append(objFreeService.ReasonCode)
                End If
            End If
            strText.Append(delimiter)

            'Ongkos Kerja
            If IsNothing(objFreeService.LabourAmount) Then
                strText.Append("")
            Else
                strText.Append(objFreeService.LabourAmount.ToString("####"))
            End If
            strText.Append(delimiter)

            'Material
            If IsNothing(objFreeService.PartAmount) Then
                strText.Append("")
            Else
                strText.Append(objFreeService.PartAmount.ToString("####"))
            End If
            strText.Append(delimiter)

            'PPN
            If IsNothing(objFreeService.PPNAmount) Then
                strText.Append("")
            Else
                strText.Append(objFreeService.PPNAmount.ToString("####"))
            End If
            strText.Append(delimiter)

            'PPH
            If IsNothing(objFreeService.PPHAmount) Then
                strText.Append("")
            Else
                strText.Append(objFreeService.PPHAmount.ToString("####"))
            End If
            strText.Append(delimiter)

            'CashBack
            If IsNothing(objFreeService.CashBack) Then
                strText.Append("")
            Else
                strText.Append(objFreeService.CashBack.ToString("####"))
            End If

            Try
                saveToTextFile(strText.ToString())
            Catch
                MessageBox.Show("Persiapan Proses Download gagal")
                Return
            End Try

        Next

        Response.Redirect("../downloadlocal.aspx?file=DataTemp\StatusFS" & Suffix & ".txt")

        MessageBox.Show("Data Telah Disimpan")
    End Sub

    Private Sub checkFileExistenceToDownload()

        Dim finfo As FileInfo = New FileInfo(Me.Server.MapPath("") & "\..\DataTemp\StatusFS" & Suffix & ".txt")

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

                Dim objFileStream As New FileStream(Me.Server.MapPath("") & "\..\DataTemp\StatusFS" & Suffix & ".txt", FileMode.Append, FileAccess.Write)
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

#End Region

#Region " control event handler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        objDealer = CType(_sessHelper.GetSession("DEALER"), Dealer)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            bindDdlCategory()
            bindDdlStatus()


            Dim isDealer As Boolean = IIf(CType(Session("LOGINUSERINFO"), UserInfo).Dealer.Title = EnumDealerTittle.DealerTittle.DEALER, False, True)

            lblCategori.Visible = isDealer
            lblCategori2.Visible = isDealer
            lblCategori2.Text = ":"

            ddlCategory.Visible = isDealer


            For IC As Integer = 0 To dgFreeService.Columns.Count - 1
                If dgFreeService.Columns(IC).HeaderText.ToLower() = "kategori" Then
                    dgFreeService.Columns(IC).Visible = isDealer
                End If


            Next
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblSearchDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
        'txtDealerBranchCode.Attributes.Add("ReadOnly", "ReadOnly")
        'txtBranchName.Attributes.Add("ReadOnly", "ReadOnly")
    End Sub

    Private Sub bindDdlCategory()
        Dim aCs As ArrayList = New CategoryFacade(User).RetrieveActiveList()
        Me.ddlCategory.Items.Clear()
        Me.ddlCategory.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each oC As Category In aCs
            Me.ddlCategory.Items.Add(New ListItem(oC.CategoryCode, oC.ID))
        Next
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.FreeServiceStatusView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FREE SERVICE - Daftar Status Free Service")
        End If

        'FreeServiceUploadSave_Privilege  
        btnDownload.Visible = SecurityProvider.Authorize(Context.User, SR.FreeServiceStatusDownload_Privilege)
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ChassisNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim nResult As Integer = RetriveDataAndSaveToCache()
        If nResult > -1 Then
            dgFreeService.CurrentPageIndex = 0
            DataBindGrid()
            btnDownload.Enabled = True
        Else
            If nResult = -2 Then
                dgFreeService.DataSource = Nothing
                dgFreeService.DataBind()
                MessageBox.Show(SR.DataNotFound("FreeService"))
            End If
            btnDownload.Enabled = False
        End If
    End Sub

    Private Sub dgFreeService_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgFreeService.ItemDataBound
        BindItemdgChassisNumber(e)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        download()
    End Sub

    Private Sub dgFreeService_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgFreeService.PageIndexChanged
        dgFreeService.CurrentPageIndex = e.NewPageIndex
        DataBindGrid()
    End Sub

    Private Sub dgFreeService_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgFreeService.SortCommand
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

        dgFreeService.SelectedIndex = -1
        RetriveDataAndSaveToCache()
        dgFreeService.CurrentPageIndex = 0
        DataBindGrid()

    End Sub

    Protected Sub dgFreeService_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgFreeService.ItemCommand
        Select Case (e.CommandName)
            Case "PartsDetail"
                Response.Redirect("../MDP/PopUpFreeServicePartDetail.aspx?id=" & e.Item.Cells(0).Text & "&mode=VIEW")
            Case "Download"
                Dim oFS As V_FreeService = CType(Session("objFreeService"), ArrayList)(e.Item.ItemIndex)
                Response.Redirect("../Download.aspx?file=" & oFS.FilePath & "&name=" & Path.GetFileNameWithoutExtension(oFS.FileName))
        End Select
    End Sub
#End Region

End Class
