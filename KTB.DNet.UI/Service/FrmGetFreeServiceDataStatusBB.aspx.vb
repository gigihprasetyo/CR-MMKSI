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

#End Region

Public Class FrmGetFreeServiceDataStatusBB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnRefresh As System.Web.UI.WebControls.Button
    Protected WithEvents dgFreeServiceBB As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ICDari As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button

    Dim dt As DateTime = DateTime.Now
    Dim Suffix As String = CType(dt.Year, String) & CType(dt.Month, String) & CType(dt.Day, String) & CType(dt.Hour, String) & CType(dt.Minute, String) & CType(dt.Second, String) & CType(dt.Millisecond, String)
    'Dim suffix As String = New Random().Next(10000).ToString()
    Protected WithEvents ICSampai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerBranchCode As TextBox
    Protected WithEvents txtWONumber As TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSearchDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents ddlRejectStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlVisitType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlFSType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgFreeService As System.Web.UI.WebControls.DataGrid


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
        _sessHelper.RemoveSession("objFreeServiceBB")

        Dim criterias As New CriteriaComposite(New Criteria(GetType(FreeServiceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If

        If txtDealerBranchCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtDealerBranchCode.Text.Replace(";", "','") & "')"))
        End If

        If (Not String.IsNullOrEmpty(txtWONumber.Text)) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "WorkOrderNumber", MatchType.InSet, "('" & txtWONumber.Text.Replace(";", "','") & "')"))
        End If

        If ddlVisitType.SelectedValue <> "Semua" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "VisitType", MatchType.Exact, ddlVisitType.SelectedValue.ToString()))
        End If

        If ddlRejectStatus.SelectedValue <> "Semua" Then
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Reject", MatchType.Exact, ddlRejectStatus.SelectedValue.ToString()))
            'Start  : by:dna;for:Rina;Remark:on:20100608;Show all data when SAP isn't upload data to dnet yet.
            If ddlStatus.SelectedItem.Text.Trim.ToLower = "Selesai".ToLower Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Reject", MatchType.Exact, ddlRejectStatus.SelectedValue.ToString()))
            Else
                'and (--NotificationType
                '   fs.Status=3 and fs.NotificationType='Z1' or  --Z1 or Z3
                '   fs.Status<>3 and fs.NotificationType=''
                ')
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Status", MatchType.Exact, CType(EnumFSStatus.FSStatus.Selesai, String)), "(", True)
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Reject", MatchType.Exact, ddlRejectStatus.SelectedValue.ToString()))
                criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Status", MatchType.No, CType(EnumFSStatus.FSStatus.Selesai, String)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Reject", MatchType.Exact, ""), ")", False)
            End If
            'Start  : by:dna;for:Rina;Remark:on:20100608;Show all data when SAP isn't upload data to dnet yet.
        End If

        If ddlFSType.SelectedValue <> "0" Then
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "NotificationType", MatchType.Exact, ddlFSType.SelectedValue.ToString()))
            'Start  : by:dna;for:Rina;Remark:on:20100608;Show all data when SAP isn't upload data to dnet yet.
            If ddlStatus.SelectedItem.Text.Trim.ToLower = "Selesai".ToLower Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "NotificationType", MatchType.Exact, ddlFSType.SelectedValue.ToString()))
            Else
                'and (--NotificationType
                '   fs.Status=3 and fs.NotificationType='Z1' or  --Z1 or Z3
                '   fs.Status<>3 and fs.NotificationType=''
                ')
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Status", MatchType.Exact, CType(EnumFSStatus.FSStatus.Selesai, String)), "(", True)
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "NotificationType", MatchType.Exact, ddlFSType.SelectedValue.ToString()))
                criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Status", MatchType.No, CType(EnumFSStatus.FSStatus.Selesai, String)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "NotificationType", MatchType.Exact, ""), ")", False)
            End If
            'End    : by:dna;for:Rina;Remark:on:20100608;Show all data when SAP isn't upload data to dnet yet.
        End If

        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Dealer.ID", MatchType.Exact, objDealer.ID))
        'Else
        '    If txtKodeDealer.Visible And txtKodeDealer.Text <> String.Empty Then
        '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FreeServiceBB), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        '    End If
        'End If

        If Not validateCriteria(criterias) Then
            Return -1
        End If

        Dim objFreeServiceBBAl As ArrayList

        Dim sortColl As SortCollection = New SortCollection
        'sortColl.Add(New Sort(GetType(ChassisMasterBB), "ChassisNumber", Sort.SortDirection.ASC))  '-- Nomor chassis
        sortColl.Add(New Sort(GetType(FreeServiceBB), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))  '-- Nomor chassis

        objFreeServiceBBAl = New FreeServiceBBFacade(User).Retrieve(criterias, sortColl)
        If objFreeServiceBBAl.Count = 0 Then
            Return -2
        Else

        End If

        _sessHelper.SetSession("objFreeServiceBB", objFreeServiceBBAl)
        'ViewState("CurrentSortColumn") = "Status"
        'ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        btnDownload.Enabled = True
        Return 0
    End Function

    Private Sub DataBindGrid()
        Dim objFreeServiceBB As ArrayList = _sessHelper.GetSession("objFreeServiceBB")
        If objFreeServiceBB Is Nothing Or objFreeServiceBB.Count <= 0 Then
            dgFreeServiceBB.DataSource = Nothing
            dgFreeServiceBB.DataBind()
            Exit Sub
        End If
        'Sort
        'Dim isAsc As Boolean = ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        'Dim iCmp As IComparer = New ListComparer(isAsc, ViewState("CurrentSortColumn"))
        'objFreeServiceBB.Sort(iCmp)

        dgFreeServiceBB.DataSource = objFreeServiceBB
        dgFreeServiceBB.DataBind()
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
            criterias.opAnd(New Criteria(GetType(FreeServiceBB), "ReleaseDate", MatchType.GreaterOrEqual, startDate))
            criterias.opAnd(New Criteria(GetType(FreeServiceBB), "ReleaseDate", MatchType.LesserOrEqual, endDate))
        End If

        If ddlStatus.SelectedItem.Value.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(FreeServiceBB), "Status", MatchType.Exact, ddlStatus.SelectedValue))
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
        Dim RowValue As FreeServiceBB = CType(e.Item.DataItem, FreeServiceBB)

        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblAlasan As Label = CType(e.Item.FindControl("lblAlasan"), Label)
        Dim lblDeskripsi As Label = CType(e.Item.FindControl("lblDeskripsi"), Label)
        Dim lblTanggalJual As Label = CType(e.Item.FindControl("lblTanggalJual"), Label)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

        Dim lblLabourAmount As Label = CType(e.Item.FindControl("lblLabourAmount"), Label)
        Dim lblPartAmount As Label = CType(e.Item.FindControl("lblPartAmount"), Label)
        Dim lblPPNAmount As Label = CType(e.Item.FindControl("lblPPNAmount"), Label)
        Dim lblPPHAmount As Label = CType(e.Item.FindControl("lblPPHAmount"), Label)
        Dim lblNotifikasi As Label = CType(e.Item.FindControl("lblNotifikasi"), Label)

        lblNo.Text = (e.Item.ItemIndex + 1) + (dgFreeServiceBB.CurrentPageIndex * dgFreeServiceBB.PageSize)

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
        lblPartAmount.Text = Format(RowValue.PartAmount, "#,###")
        lblPPNAmount.Text = Format(RowValue.PPNAmount, "#,###")
        lblPPHAmount.Text = Format(RowValue.PPHAmount, "#,###")
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

        GetTotal(totalOpen, totalInProcess, totalA, totalD, totalLabourAmountA, _
            totalLabourAmountD, totalPartAmountA, totalPartAmountD, totalPPNAmountA, _
            totalPPNAmountD, totalPPHAmountA, totalPPHAmountD, totalAmountA, _
            totalAmountD)

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
    End Sub

    Private Sub bindDdlStatus()

        Dim listTitle As New EnumFSStatus
        Dim al2 As ArrayList = listTitle.RetrieveFSStatus
        For Each item As EnumFS In al2
            If item.NameFSStatus.ToUpper <> "RILIS" And item.NameFSStatus.ToUpper <> "BARU" Then
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
        ByRef totalAmountD As Double)

        Dim objAL As New ArrayList
        objAL = CType(_sessHelper.GetSession("objFreeServiceBB"), ArrayList)

        Dim intTotalOpen As Integer = 0
        Dim intTotalInProcess As Integer = 0
        Dim intTotalApprove As Integer = 0
        Dim intTotalDisapprove As Integer = 0

        Dim dblLabourAmountApp As Double = 0
        Dim dblPartAmountApp As Double = 0
        Dim dblPPNAmountApp As Double = 0
        Dim dblPPHAmountApp As Double = 0
        Dim dblLabourAmountDisapp As Double = 0
        Dim dblPartAmountDisapp As Double = 0
        Dim dblPPNAmountDisapp As Double = 0
        Dim dblPPHAmountDisapp As Double = 0

        For count As Integer = 0 To objAL.Count - 1

            Dim objFreeServiceBB As FreeServiceBB = CType(objAL.Item(count), FreeServiceBB)

            If Not IsNothing(objFreeServiceBB.Status) And Not objFreeServiceBB.Status = "" Then

                If objFreeServiceBB.Status = CType(EnumFSStatus.FSStatus.Baru, String) Then
                    intTotalOpen += 1
                End If

                If objFreeServiceBB.Status = CType(EnumFSStatus.FSStatus.Proses, String) Then
                    intTotalInProcess += 1
                End If
            End If

            If Not IsNothing(objFreeServiceBB.Reject) And Not objFreeServiceBB.Reject = "" Then

                If objFreeServiceBB.Reject.ToLower = "app" Then
                    intTotalApprove += 1
                    dblLabourAmountApp += objFreeServiceBB.LabourAmount
                    dblPartAmountApp += objFreeServiceBB.PartAmount
                    dblPPNAmountApp += objFreeServiceBB.PPNAmount
                    dblPPHAmountApp += objFreeServiceBB.PPHAmount
                End If

                If objFreeServiceBB.Reject.ToLower = "dapp" Then
                    intTotalDisapprove += 1
                    dblLabourAmountDisapp += objFreeServiceBB.LabourAmount
                    dblPartAmountDisapp += objFreeServiceBB.PartAmount
                    dblPPNAmountDisapp += objFreeServiceBB.PPNAmount
                    dblPPHAmountDisapp += objFreeServiceBB.PPHAmount
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

        totalAmountA = dblLabourAmountApp + dblPartAmountApp + dblPPNAmountApp + dblPPHAmountApp
        totalAmountD = dblLabourAmountDisapp + dblPartAmountDisapp + dblPPNAmountDisapp + dblPPHAmountDisapp
    End Sub

    Private Sub download()
        Dim strText As StringBuilder
        Dim objAl As New ArrayList
        Dim delimiter As String = Chr(9)
        checkFileExistenceToDownload()

        objAl = CType(_sessHelper.GetSession("objFreeServiceBB"), ArrayList)

        For count As Integer = 0 To objAl.Count - 1

            Dim objFreeServiceBB As FreeServiceBB = CType(objAl.Item(count), FreeServiceBB)
            strText = New StringBuilder

            'Dealer
            If IsNothing(objFreeServiceBB.Dealer) Then
                strText.Append(delimiter)
            Else
                strText.Append(objFreeServiceBB.Dealer.DealerCode.ToString)
                strText.Append(delimiter)
            End If

            'DealerBranch
            If IsNothing(objFreeServiceBB.DealerBranch) Then
                strText.Append(delimiter)
            Else
                strText.Append(objFreeServiceBB.DealerBranch.DealerBranchCode.ToString)
                strText.Append(delimiter)
            End If

            'Chassis
            strText.Append(objFreeServiceBB.ChassisMasterBB.ChassisNumber.ToString)
            strText.Append(delimiter)

            'Kind
            strText.Append(objFreeServiceBB.FSKind.KindCode.ToString)
            strText.Append(delimiter)

            'Service Date
            strText.Append(objFreeServiceBB.ServiceDate.ToString("dd/MM/yyyy"))
            strText.Append(delimiter)

            'Sold Date
            If objFreeServiceBB.SoldDate < New Date(1901, 1, 1) Then
                strText.Append("")
            Else
                strText.Append(objFreeServiceBB.SoldDate.ToString("dd/MM/yyyy"))
            End If
            strText.Append(delimiter)

            'MileAge
            strText.Append(objFreeServiceBB.MileAge.ToString)
            strText.Append(delimiter)

            'NotifType
            If IsNothing(objFreeServiceBB.NotificationType) Then
                strText.Append("")
            Else
                strText.Append(objFreeServiceBB.NotificationType.ToString)
            End If
            strText.Append(delimiter)

            'NotifNumber
            If IsNothing(objFreeServiceBB.NotificationNumber) Then
                strText.Append("")
            Else
                strText.Append(objFreeServiceBB.NotificationNumber.ToString)
            End If
            strText.Append(delimiter)

            'Status
            If IsNothing(objFreeServiceBB.Reject) Then
                strText.Append("")
            Else
                strText.Append(objFreeServiceBB.Reject)
            End If
            strText.Append(delimiter)

            'Alasan
            If objFreeServiceBB.Reject = "DAPP" Then
                If IsNothing(objFreeServiceBB.Reason) Then
                    strText.Append("")
                ElseIf objFreeServiceBB.Reason.ReasonCode <> "" Then
                    strText.Append(objFreeServiceBB.Reason.ReasonCode)
                End If
            End If
            strText.Append(delimiter)

            'Ongkos Kerja
            If IsNothing(objFreeServiceBB.LabourAmount) Then
                strText.Append("")
            Else
                strText.Append(objFreeServiceBB.LabourAmount.ToString("####"))
            End If
            strText.Append(delimiter)

            'Material
            If IsNothing(objFreeServiceBB.PartAmount) Then
                strText.Append("")
            Else
                strText.Append(objFreeServiceBB.PartAmount.ToString("####"))
            End If
            strText.Append(delimiter)

            'PPN
            If IsNothing(objFreeServiceBB.PPNAmount) Then
                strText.Append("")
            Else
                strText.Append(objFreeServiceBB.PPNAmount.ToString("####"))
            End If
            strText.Append(delimiter)

            'PPH
            If IsNothing(objFreeServiceBB.PPHAmount) Then
                strText.Append("")
            Else
                strText.Append(objFreeServiceBB.PPHAmount.ToString("####"))
            End If

            'Tipe Visit
            If IsNothing(objFreeServiceBB.VisitType) Then
                strText.Append(delimiter)
            Else
                strText.Append(objFreeServiceBB.VisitType.ToString)
                strText.Append(delimiter)
            End If

            'WO Number
            If IsNothing(objFreeServiceBB.WorkOrderNumber) Then
                strText.Append(delimiter)
            Else
                strText.Append(objFreeServiceBB.WorkOrderNumber.ToString)
                strText.Append(delimiter)
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
            bindDdlStatus()
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        lblSearchDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
    End Sub
    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.FreeServiceStatusView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FREE SERVICE - Daftar Status Free Service")
        End If

        'FreeServiceBBUploadSave_Privilege  
        btnDownload.Visible = SecurityProvider.Authorize(Context.User, SR.FreeServiceStatusDownload_Privilege)
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ChassisMasterBB.ChassisNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim nResult As Integer = RetriveDataAndSaveToCache()
        If nResult > -1 Then
            dgFreeServiceBB.CurrentPageIndex = 0
            DataBindGrid()
            btnDownload.Enabled = True
        Else
            If nResult = -2 Then
                dgFreeServiceBB.DataSource = Nothing
                dgFreeServiceBB.DataBind()
                MessageBox.Show(SR.DataNotFound("FreeServiceBB"))
            End If
            btnDownload.Enabled = False
        End If
    End Sub

    Private Sub dgFreeServiceBB_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgFreeServiceBB.ItemDataBound
        BindItemdgChassisNumber(e)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        download()
    End Sub

    Private Sub dgFreeServiceBB_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgFreeServiceBB.PageIndexChanged
        dgFreeServiceBB.CurrentPageIndex = e.NewPageIndex
        DataBindGrid()
    End Sub

    Private Sub dgFreeServiceBB_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgFreeServiceBB.SortCommand
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

        dgFreeServiceBB.SelectedIndex = -1
        RetriveDataAndSaveToCache()
        dgFreeServiceBB.CurrentPageIndex = 0
        DataBindGrid()

    End Sub

#End Region

End Class
