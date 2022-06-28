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

Public Class FrmPartIncidentalPO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents intPKDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtReqNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPartNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents intFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents intTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents dgPartList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents cbPKDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents cbPlanDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button

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
#End Region

#Region "Custom Method"

    Private Sub BindToDataGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim dealerList As String = "('" & txtKodeDealer.Text.Trim.Replace(";", "','") & "')"
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "PartIncidentalHeader.KTBStatus", MatchType.InSet, "(" & 0 & "," & 1 & ")"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "StatusDetail", MatchType.InSet, "(" & CType(PartIncidentalStatus.PartIncidentalDetailStatusEnum.Aktif, Short) & "," & CType(PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal_Sebagian, Short) & ")"))

        If txtKodeDealer.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "PartIncidentalHeader.Dealer.DealerCode", MatchType.InSet, dealerList))
        End If

        If cbPKDate.Checked = True Then
            Dim PKDateStart As DateTime = New DateTime(intPKDate.Value.Year, intPKDate.Value.Month, intPKDate.Value.Day, 0, 0, 0)
            Dim PKDateEnd As DateTime = New DateTime(intPKDate.Value.Year, intPKDate.Value.Month, intPKDate.Value.Day, 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "PartIncidentalHeader.IncidentalDate", MatchType.GreaterOrEqual, PKDateStart))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "PartIncidentalHeader.IncidentalDate", MatchType.LesserOrEqual, PKDateEnd))
        End If

        If cbPlanDate.Checked = True Then
            Dim PlanDateStart As DateTime = New DateTime(intFrom.Value.Year, intFrom.Value.Month, intFrom.Value.Day, 0, 0, 0)
            Dim PlanDateEnd As DateTime = New DateTime(intTo.Value.Year, intTo.Value.Month, intTo.Value.Day, 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "PlanDate", MatchType.GreaterOrEqual, PlanDateStart))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "PlanDate", MatchType.LesserOrEqual, PlanDateEnd))
        End If

        If txtReqNumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "PartIncidentalHeader.RequestNumber", MatchType.[Partial], txtReqNumber.Text))
        End If

        If txtPartNumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "SparePartMaster.PartNumber", MatchType.[Partial], txtPartNumber.Text))
        End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "Reject", MatchType.No, -1))
        Dim minDate As DateTime = New DateTime(1900, 1, 1, 0, 0, 0)

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "PlanDate", MatchType.Greater, minDate))

        Dim DetailList As ArrayList = New PartIncidentalDetailFacade(User).RetrieveFilteredActiveList(criterias, currentPageIndex + 1, dgPartList.PageSize, _
                   total, CType(ViewState("CurrentSortColumn"), String), _
                   CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        'Dim DetailList As ArrayList = New PartIncidentalDetailFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dgPartList.PageSize, _
        '           total, CType(ViewState("CurrentSortColumn"), String), _
        '           CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        Dim ArrlistAll As ArrayList = New PartIncidentalDetailFacade(User).RetrieveFilteredActiveList(criterias)
        sessionHelper.SetSession("arrDownloadAble", ArrlistAll)
        dgPartList.DataSource = DetailList
        dgPartList.VirtualItemCount = total
        dgPartList.DataBind()
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewListPengajuanPermintaanKhusus_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Permintaan Khusus dari Dealer")
        End If

    End Sub

#End Region

#Region "Event Handlers"

    Private Sub dgPartList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartList.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As PartIncidentalDetail = CType(e.Item.DataItem, PartIncidentalDetail)           
                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                    e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgPartList.CurrentPageIndex * dgPartList.PageSize)
                    Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                    lblDealerCode.Text = RowValue.PartIncidentalHeader.Dealer.DealerCode
                    Dim lblReqNo As Label = CType(e.Item.FindControl("lblReqNo"), Label)
                    lblReqNo.Text = RowValue.PartIncidentalHeader.RequestNumber

                    Dim lblDealerMailNumber As Label = CType(e.Item.FindControl("lblDealerMailNumber"), Label)
                    lblDealerMailNumber.Text = RowValue.PartIncidentalHeader.DealerMailNumber
                    Dim lblModelCode As Label = CType(e.Item.FindControl("lblModelCode"), Label)
                    lblModelCode.Text = RowValue.SparePartMaster.ModelCode
                    'Dim lblSupplierCode As Label = CType(e.Item.FindControl("lblSupplierCode"), Label)
                    'lblSupplierCode.Text = RowValue.SparePartMaster.SupplierCode
                    Dim lblStatusDetail As Label = CType(e.Item.FindControl("lblStatusDetail"), Label)

                    Dim lblPKDate As Label = CType(e.Item.FindControl("lblPKDate"), Label)
                    If RowValue.PartIncidentalHeader.IncidentalDate < New DateTime(1900, 1, 1, 0, 0, 0) Then
                        lblPKDate.Text = ""
                    Else
                        lblPKDate.Text = RowValue.PartIncidentalHeader.IncidentalDate

                    End If
                    Dim lblPlanDate As Label = CType(e.Item.FindControl("lblPlanDate"), Label)
                    If RowValue.PlanDate < New DateTime(1900, 1, 1, 0, 0, 0) Then
                        lblPlanDate.Text = ""
                    Else
                        lblPlanDate.Text = RowValue.PlanDate
                    End If


                    Dim lblPartSubNo As Label = CType(e.Item.FindControl("lblPartSubNo"), Label)
                    Dim lblPartSubName As Label = CType(e.Item.FindControl("lblPartSubName"), Label)
                    Dim lblPartNo As Label = CType(e.Item.FindControl("lblPartNo"), Label)

                    Dim lblPartName As Label = CType(e.Item.FindControl("lblPartName"), Label)
                    lblPartName.Text = RowValue.SparePartMaster.PartName
                    lblPartNo.Text = RowValue.SparePartMaster.PartNumber
                    If RowValue.SparePartMasterSubstitutionID > 0 Then
                        Dim spMasterSub As SparePartMaster = getSparePartMaster(RowValue.SparePartMasterSubstitutionID)
                        If spMasterSub.ID > 0 Then
                            'lblPartSubNo.Text = spMasterSub.PartNumber
                            'lblPartSubName.Text = spMasterSub.PartName
                            lblPartName.Text = String.Empty
                            lblPartNo.Text = String.Empty
                            lblPartName.Text = spMasterSub.PartName
                            lblPartNo.Text = spMasterSub.PartNumber
                            e.Item.BackColor = Color.Gainsboro


                        End If
                    End If

                    Dim lblOrgQty As Label = CType(e.Item.FindControl("lblOrgQty"), Label)
                    lblOrgQty.Text = RowValue.Quantity
                    Dim lblRemainQty As Label = CType(e.Item.FindControl("lblRemainQty"), Label)
                    lblRemainQty.Text = RowValue.RemainQuantity
                    Dim txtAlokasi As TextBox = CType(e.Item.FindControl("txtAlokasi"), TextBox)
                    txtAlokasi.Text = 0
                    Dim cb As CheckBox = CType(e.Item.FindControl("cbSelect"), CheckBox)
                    If RowValue.PartIncidentalHeader.KTBStatus <> PartIncidentalStatus.PartIncidentalKTBStatusEnum.Sedang_Proses Then
                    e.Item.BackColor = Color.Yellow
                    'CR SparePart 0908001
                    txtAlokasi.Enabled = True
                    cb.Visible = True
                    Else
                        cb.Visible = True
                        txtAlokasi.Enabled = True
                    End If

                    If RowValue.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal Then
                        lblStatusDetail.Text = "<img src=""../images/red.gif"" border=""0"">"
                        lblStatusDetail.ToolTip = "Batal"
                    ElseIf RowValue.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal_Sebagian Then
                        lblStatusDetail.Text = "<img src=""../images/yellow.gif"" border=""0"">"
                        lblStatusDetail.ToolTip = "Batal Sebagian"
                    Else
                        lblStatusDetail.Text = "<img src=""../images/green.gif"" border=""0"">"
                        lblStatusDetail.ToolTip = "Aktif"
                    End If             
            End If
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            If Not SecurityProvider.Authorize(Context.User, SR.ENHSparepartPKAlokasiView_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Alokasi Pesanan Khusus.")
            End If
            btnSave.Enabled = SecurityProvider.Authorize(Context.User, SR.ENHSparepartPKAlokasiEdit_Privilege)
            intFrom.Value = DateTime.Now.AddMonths(-1)
            BindToDataGrid(0)
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "SparePartMaster.PartNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        dgPartList.CurrentPageIndex = 0
        BindToDataGrid(dgPartList.CurrentPageIndex)
    End Sub

    Private Function getSparePartMaster(ByVal id As Integer) As SparePartMaster
        Dim spMasterFacade As SparePartMasterFacade = New SparePartMasterFacade(User)
        Return spMasterFacade.Retrieve(id)
    End Function

#End Region

    Private Sub dgPartList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgPartList.PageIndexChanged
        dgPartList.CurrentPageIndex = e.NewPageIndex
        BindToDataGrid(dgPartList.CurrentPageIndex)
    End Sub

    Private Sub dgPartList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgPartList.SortCommand
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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CreatePOAlocation()
    End Sub

    Private Sub CreatePOAlocation()
        Dim collPartIncidentalPO As ArrayList = New ArrayList
        Dim isValidOperation As Boolean = True
        Dim objPartIncidentalDetailFacade As PartIncidentalDetailFacade = New PartIncidentalDetailFacade(User)

        For Each item As DataGridItem In dgPartList.Items
            Dim cb As CheckBox = CType(item.FindControl("cbSelect"), CheckBox)
            If cb.Checked = True Then
                Dim objPartIncidentalPO As PartIncidentalPO = New PartIncidentalPO
                Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                objPartIncidentalPO.PONumber = ""
                objPartIncidentalPO.ProcessDate = Now
                Dim objPartPO As PartIncidentalDetail = New PartIncidentalDetail(lblID.Text)

                Dim objPartIncidentalDetail As PartIncidentalDetail = objPartIncidentalDetailFacade.Retrieve(CInt(lblID.Text))
                'If objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal Or objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal_Sebagian Then
                If objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal Then
                    MessageBox.Show("Pesanan yang sudah dibatalkan tidak bisa diproses")
                    Return
                End If

                objPartIncidentalPO.PartIncidentalDetail = objPartPO
                Dim lblRemainQty As Label = CType(item.FindControl("lblRemainQty"), Label)
                Dim txtAlokasi As TextBox = CType(item.FindControl("txtAlokasi"), TextBox)
                objPartIncidentalPO.Alocation = txtAlokasi.Text
                If CInt(txtAlokasi.Text) > CInt(lblRemainQty.Text) Then
                    isValidOperation = False
                Else
                    collPartIncidentalPO.Add(objPartIncidentalPO)
                End If
            End If
        Next
        If collPartIncidentalPO.Count > 0 Then
            If isValidOperation Then
                CreatePOPartIncidental(collPartIncidentalPO)
                dgPartList.CurrentPageIndex = 0
                BindToDataGrid(0)
            Else
                MessageBox.Show("Quantity Alokasi tidak valid.")
            End If
        Else
            If isValidOperation Then
                MessageBox.Show("Tidak ada item yang dipilih.")
            Else
                MessageBox.Show("Quantity Alokasi tidak valid.")
            End If

        End If

    End Sub

    Private Sub CreatePOPartIncidental(ByVal POList As ArrayList)
        If POList.Count > 0 Then
            For Each item As PartIncidentalPO In POList
                Dim poFacade As PartIncidentalPOFacade = New PartIncidentalPOFacade(User)
                poFacade.Insert(item)
            Next
        End If
        MessageBox.Show(SR.SaveSuccess)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Response.Redirect("frmDownloadPartIncidentalAllocation.aspx")
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CancelRequest()
    End Sub

    Private Sub CancelRequest()
        Dim collPartIncidentalDetail As ArrayList = New ArrayList
        Dim objPartIncidentalDetailFacade As PartIncidentalDetailFacade = New PartIncidentalDetailFacade(User)
        'Dim isValidOperation As Boolean = True


        For Each item As DataGridItem In dgPartList.Items
            Dim cb As CheckBox = CType(item.FindControl("cbSelect"), CheckBox)
            If cb.Checked = True Then
                Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                Dim lblOrgQty As Label = CType(item.FindControl("lblOrgQty"), Label)
                Dim lblRemainQty As Label = CType(item.FindControl("lblRemainQty"), Label)
                Dim txtAlokasi As TextBox = CType(item.FindControl("txtAlokasi"), TextBox)
                'Dim objPartIncidentalDetail As PartIncidentalDetail = New PartIncidentalDetail(lblID.Text)
                Dim objPartIncidentalDetail As PartIncidentalDetail = objPartIncidentalDetailFacade.Retrieve(CInt(lblID.Text))
                'Dim intPartIncidentalHeaderID As Integer = objPartIncidentalDetail.PartIncidentalHeader.ID
                'Dim intSparePartMasterID As Integer = objPartIncidentalDetail.SparePartMaster.ID

                If txtAlokasi.Text <> String.Empty And CInt(txtAlokasi.Text) > 0 Then
                    objPartIncidentalDetail.Alokasi = objPartIncidentalDetail.Alokasi + CInt(txtAlokasi.Text)
                End If

                If lblRemainQty.Text = 0 Then
                    MessageBox.Show("Pemesanan yang remain quantity sudah 0 tidak bisa dibatalkan")
                    Return
                ElseIf objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal Then
                    MessageBox.Show("Pemesanan yang sudah dibatalkan tidak bisa dibatalkan lagi")
                    Return
                Else
                    If txtAlokasi.Text = lblRemainQty.Text Then
                        objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal
                    Else
                        objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal_Sebagian
                    End If

                    collPartIncidentalDetail.Add(objPartIncidentalDetail)
                End If
            End If
        Next


        objPartIncidentalDetailFacade.Update(collPartIncidentalDetail)
        BindToDataGrid(dgPartList.CurrentPageIndex)
    End Sub
End Class