#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.SparePart
'Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
'Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
#End Region

Public Class FrmListPartIncidentalKTB
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents dgPartList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents intFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents intTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private sessionHelper As New sessionHelper
    Private ArlPartHeader As ArrayList
    Private objDealer As Dealer
    Private objChassisFacade As New KTB.DNet.BusinessFacade.FinishUnit.ChassisMasterFacade(User)
    Private objPriorityDetailFacade As New PartIncidentalPriorityDetailFacade(User)
#End Region

#Region "Custom Method"

    Private Sub BindToddlStatus()

        ddlStatus.DataSource = PartIncidentalStatus.RetrievePartIncidentalKTBStatus
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataBind()
    End Sub

    Private Sub BindToDataGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.PartIncidentalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlStatus.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "KTBStatus", MatchType.Exact, ddlStatus.SelectedValue))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        If txtKodeDealer.Text.Trim <> String.Empty Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "EmailStatus", MatchType.Exact, CType(PartIncidentalStatus.PartIncidentalEmailStatusEnum.Dikirim, Integer)))

        If CType(intFrom.Value, Date) <= CType(intTo.Value, Date) Then '--Create New Calendar
            Dim tglFrom As New Date(intFrom.Value.Year, intFrom.Value.Month, intFrom.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(intTo.Value.Year, intTo.Value.Month, intTo.Value.Day, 23, 59, 59)
            '--Get Criterias From Selected Calendar
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "IncidentalDate", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "IncidentalDate", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")))
        Else
            dgPartList.DataBind()
            MessageBox.Show("Tanggal Awal Lebih Besar Dari Tanggal Akhir")
            Return
        End If

        'Dim sortColl As SortCollection = New SortCollection
        'If (Not IsNothing("ID")) Then
        '    sortColl.Add(New Sort(GetType(PartIncidentalHeader), "ID", Sort.SortDirection.DESC))
        'Else
        '    sortColl = Nothing
        'End If
        'alVehicleType = New VechileTypeFacade(User).Retrieve(criterias, sortColl)
        'dgPartList.DataSource = New PartIncidentalHeaderFacade(User).Retrieve(criterias, sortColl)
        ArlPartHeader = New PartIncidentalHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dgPartList.PageSize, _
                   total, CType(ViewState("CurrentSortColumn"), String), _
                   CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgPartList.DataSource = ArlPartHeader
        dgPartList.VirtualItemCount = total
        dgPartList.DataBind()
    End Sub

    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.ViewListPengajuanPermintaanKhusus_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Permintaan Khusus dari Dealer")
        End If
        '--060204 in active by request fo BA 
        'btnFind.Visible = SecurityProvider.Authorize(Context.User, SR.SearchPartIncidentalEntryList_Privilege)
        '--

        'btnDelete.Visible = SecurityProvider.Authorize(Context.User, SR.DeletePartIncidentalEntry_Privilege)
        'ddlAction.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranStatusTolakan_Privilege)
        'Label11.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranStatusTolakan_Privilege)

        'Label10.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranNomorAccounting_Privilege)
        'txtDocNumber.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranNomorAccounting_Privilege)
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ActivateUserPrivilege()
        If Not IsPostBack Then

            'Dim objPriorityfacade As New PartIncidentalPriorityFacade(User)
            'sessionHelper.SetSession("objPriorityNew", objPriorityfacade.Retrieve(CInt(KTB.DNet.Lib.WebConfig.GetValue("PartIncidentalPriorityIDNew"))))
            'Dim criterias As New CriteriaComposite(New Criteria(GetType(PartIncidentalPriority), "ID", MatchType.No, KTB.DNet.Lib.WebConfig.GetValue("PartIncidentalPriorityIDNew")))
            'sessionHelper.SetSession("objPriorityOthers", objPriorityfacade.Retrieve(criterias)(0))

            Dim id As Integer = Request.QueryString("ID")
            If id = 0 Then
                BindToddlStatus()
                intFrom.Value = DateTime.Now.Date.AddDays(-7)
            Else
                BindToddlStatus()
                ddlStatus.SelectedValue = sessionHelper.GetSession("StatusKTB")
                txtKodeDealer.Text = sessionHelper.GetSession("DealerKTB")
                intFrom.Value = sessionHelper.GetSession("CalenderFrom")
                intTo.Value = sessionHelper.GetSession("CalenderTo")

                InitiatePage()
                BindToDataGrid(dgPartList.CurrentPageIndex)
            End If

        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "IncidentalDate"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub dgPartList_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPartList.SortCommand
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

        dgPartList.SelectedIndex = -1
        dgPartList.CurrentPageIndex = 0
        BindToDataGrid(dgPartList.CurrentPageIndex)
    End Sub

    Private Sub dgPartList_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPartList.PageIndexChanged
        dgPartList.CurrentPageIndex = e.NewPageIndex
        BindToDataGrid(dgPartList.CurrentPageIndex)
    End Sub
    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        BindToDataGrid(dgPartList.CurrentPageIndex)
    End Sub

    Private Function UpdatePartStatus(ByVal header As PartIncidentalHeader) As Boolean
        Dim isComplete As Boolean = True
        Dim isAllPO As Boolean = True
        Dim _arrDataGrid As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "PartIncidentalHeader.ID", MatchType.Exact, header.ID))
        _arrDataGrid = New PartIncidentalDetailFacade(User).Retrieve(criterias)

        For Each item As PartIncidentalDetail In _arrDataGrid
            For Each po As PartIncidentalPO In item.PartIncidentalPOs
                If po.PONumber.Trim = String.Empty Then
                    isAllPO = False
                    Exit For
                End If
            Next
            If isAllPO = False Then
                Exit For
            End If
        Next

        For Each item As PartIncidentalDetail In _arrDataGrid
            If item.AlocatedQuantity < item.Quantity Then
                isComplete = False
                Exit For
            End If
        Next

        If isComplete And isAllPO Then
            header.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai
            Dim facade As PartIncidentalHeaderFacade = New PartIncidentalHeaderFacade(User)
            facade.Update(header)
            Return True
        End If

        Return False
    End Function

    Private Sub dgPartList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartList.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As PartIncidentalHeader = CType(e.Item.DataItem, PartIncidentalHeader)
            Dim isFinish As Boolean = FinishProsessPO(RowValue)
            If RowValue.KTBStatus <> PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai Then
                If isFinish Then
                    RowValue.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai
                End If
            End If

            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim EnumStatusKTB As PartIncidentalStatus.PartIncidentalKTBStatusEnum = RowValue.KTBStatus
                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgPartList.CurrentPageIndex * dgPartList.PageSize)
                e.Item.Cells(2).Text = RowValue.Dealer.DealerCode
                e.Item.Cells(3).Text = RowValue.Dealer.SearchTerm2
                If (RowValue.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru) Or (RowValue.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Sedang_Proses) Then
                    Dim isCompleted As Boolean = UpdatePartStatus(RowValue)
                    If isCompleted Then
                        EnumStatusKTB = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai
                    End If
                End If
                e.Item.Cells(10).Text = EnumStatusKTB.ToString
                If EnumStatusKTB <> PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai Then
                    Dim lbnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                    lbnDelete.Visible = False
                End If

                Dim lblPriority As Label = e.Item.FindControl("lblPriority")
                lblPriority.Text = GetStrPriority(e.Item.DataItem)

                If Val(RowValue.AssemblyYear) = 1980 Or Val(RowValue.AssemblyYear) = 0 Then
                    lblPriority.Text = "Others"
                Else
                    Dim objPriorityDetail As PartIncidentalPriorityDetail = objPriorityDetailFacade.Retrieve(Mid(RowValue.ChassisNumber, 4, 4))
                    If objPriorityDetail.ID = 0 Then
                        lblPriority.Text = "Others"
                    Else
                        If objPriorityDetail.StartProdYear > Val(RowValue.AssemblyYear) Then
                            lblPriority.Text = "Others"
                        Else
                            lblPriority.Text = objPriorityDetail.PartIncidentalPriority.Priority
                        End If
                    End If
                End If

            End If

            '--Get Confirm Message From Button Delete
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            End If

        End If
    End Sub

    Private Function GetStrPriority(ByVal objHeader As PartIncidentalHeader) As String
        Dim objChassis As ChassisMaster = objChassisFacade.Retrieve(objHeader.ChassisNumber)
    End Function

    Private Function FinishProsessPO(ByVal objHeader As PartIncidentalHeader) As Boolean
        Dim isFinished As Boolean = True
        If Not objHeader Is Nothing Then
            For Each item As PartIncidentalDetail In objHeader.PartIncidentalDetails
                If item.Reject <> -1 Then
                    isFinished = False
                    Exit For
                End If
            Next

            If isFinished Then
                objHeader.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai
                Dim objPOSPFacade As PartIncidentalHeaderFacade = New PartIncidentalHeaderFacade(User)
                objPOSPFacade.Update(objHeader)
            End If
        End If
        Return isFinished
    End Function

    Private Sub dgPartList_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPartList.ItemCommand
        Dim lblID As Label = e.Item.FindControl("lblID")
        If e.CommandName = "Detail" Then

            sessionHelper.SetSession("StatusKTB", ddlStatus.SelectedValue)
            sessionHelper.SetSession("DealerKTB", txtKodeDealer.Text)
            sessionHelper.SetSession("CalenderFrom", intFrom.Value)
            sessionHelper.SetSession("CalenderTo", intTo.Value)
            Response.Redirect("../SparePart/FrmListPartIncidentalKTBDetail.aspx?ID=" & lblID.Text)

        ElseIf e.CommandName = "Delete" Then
            Dim objIncidental As PartIncidentalHeader
            Dim objPartHeaderFacade As New PartIncidentalHeaderFacade(User)
            Dim objPartDetailFacade As New PartIncidentalDetailFacade(User)

            objIncidental = New PartIncidentalHeaderFacade(User).Retrieve(CInt(lblID.Text))
            If objIncidental.PartIncidentalDetails.Count > 0 Then
                For Each item As PartIncidentalDetail In objIncidental.PartIncidentalDetails
                    For Each poitem As PartIncidentalPO In item.PartIncidentalPOs
                        Dim poFacade As PartIncidentalPOFacade = New PartIncidentalPOFacade(User)
                        poFacade.Delete(poitem)
                    Next
                    objPartDetailFacade.Delete(item)
                Next

                objPartHeaderFacade.DeleteFromDB(objIncidental)
            Else
                objPartHeaderFacade.DeleteFromDB(objIncidental)
            End If

            BindToDataGrid(dgPartList.CurrentPageIndex)

        End If

    End Sub

#End Region


End Class