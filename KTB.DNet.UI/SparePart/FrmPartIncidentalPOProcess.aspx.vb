#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.SparePart
'Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
'Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports System.IO
Imports System.Text
#End Region

Public Class FrmPartIncidentalPOProcess
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
    Protected WithEvents txtPoNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents rbPO As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
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
    Private ArlPartDetail As ArrayList
#End Region

#Region "Custom Method"

    Private Sub BindToDataGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim dealerList As String = "('" & txtKodeDealer.Text.Trim.Replace(";", "','") & "')"
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PartIncidentalDetail.PartIncidentalHeader.KTBStatus", MatchType.InSet, "(" & 0 & "," & 1 & ")"))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PartIncidentalDetail.StatusDetail", MatchType.No, CType(PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PartIncidentalDetail.StatusDetail", MatchType.No, CType(PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal_Sebagian, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PartIncidentalDetail.StatusDetail", MatchType.NotInSet, "(" & CType(PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal, Short) & "," & CType(PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal_Sebagian, Short) & ")"))
        If txtKodeDealer.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PartIncidentalDetail.PartIncidentalHeader.Dealer.DealerCode", MatchType.InSet, dealerList))
        End If

        If cbPKDate.Checked = True Then
            Dim PKDateStart As DateTime = New DateTime(intPKDate.Value.Year, intPKDate.Value.Month, intPKDate.Value.Day, 0, 0, 0)
            Dim PKDateEnd As DateTime = New DateTime(intPKDate.Value.Year, intPKDate.Value.Month, intPKDate.Value.Day, 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PartIncidentalDetail.PartIncidentalHeader.IncidentalDate", MatchType.GreaterOrEqual, PKDateStart))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PartIncidentalDetail.PartIncidentalHeader.IncidentalDate", MatchType.LesserOrEqual, PKDateEnd))
        End If

        If cbPlanDate.Checked = True Then
            Dim PlanDateStart As DateTime = New DateTime(intFrom.Value.Year, intFrom.Value.Month, intFrom.Value.Day, 0, 0, 0)
            Dim PlanDateEnd As DateTime = New DateTime(intTo.Value.Year, intTo.Value.Month, intTo.Value.Day, 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PartIncidentalDetail.PlanDate", MatchType.GreaterOrEqual, PlanDateStart))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PartIncidentalDetail.PlanDate", MatchType.LesserOrEqual, PlanDateEnd))
        End If

        If txtReqNumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PartIncidentalDetail.PartIncidentalHeader.RequestNumber", MatchType.[Partial], txtReqNumber.Text))
        End If

        If txtPartNumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PartIncidentalDetail.SparePartMaster.PartNumber", MatchType.[Partial], txtPartNumber.Text))
        End If



        If txtPoNumber.Text.Trim <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PONumber", MatchType.[Partial], txtPoNumber.Text.Trim))
        End If

        'If cbPO.Checked = True Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PONumber", MatchType.No, String.Empty))
        'Else
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PONumber", MatchType.Greater.Exact, String.Empty))

        'End If

        If rbPO.Items(0).Selected Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PONumber", MatchType.No, String.Empty))
        End If
        If rbPO.Items(1).Selected Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "PONumber", MatchType.Greater.Exact, String.Empty))
        End If


        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalPO), "Alocation", MatchType.Greater, 0))

        Dim DetailList As ArrayList = New PartIncidentalPOFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dgPartList.PageSize, _
                   total, CType(ViewState("CurrentSortColumn"), String), _
                   CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        Dim ArlListAll As ArrayList = New PartIncidentalPOFacade(User).Retrieve(criterias)
        sessionHelper.SetSession("arlDownloadAble", ArlListAll)
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
    Private Sub dgPartList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPartList.ItemCommand
        Dim objPartIncidentalDetailFacade As PartIncidentalDetailFacade = New PartIncidentalDetailFacade(User)

        If e.CommandName.ToLower = "delete" Then
            Dim id As Integer = CType(e.Item.FindControl("lblID"), Label).Text
            Dim objPartIncidentalPOFacade As PartIncidentalPOFacade
            objPartIncidentalPOFacade = New PartIncidentalPOFacade(User)
            Dim objPartIncidentalPO As PartIncidentalPO = objPartIncidentalPOFacade.Retrieve(id)
            If id > 0 Then

                Dim objPartIncidentalDetail As PartIncidentalDetail = objPartIncidentalDetailFacade.Retrieve(objPartIncidentalPO.PartIncidentalDetail.ID)
                'If objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal Or objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal_Sebagian Then
                '    MessageBox.Show("Pesanan yang sudah dibatalkan tidak bisa dirubah")
                '    Return
                'End If
                If objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal Then
                    MessageBox.Show("Pesanan yang sudah dibatalkan tidak bisa dirubah")
                    Return
                End If

                objPartIncidentalPO.RowStatus = DBRowStatus.Deleted
                objPartIncidentalPOFacade = New PartIncidentalPOFacade(User)
                objPartIncidentalPOFacade.Update(objPartIncidentalPO)
                BindToDataGrid(0)
            End If
        Else
            If e.CommandName.ToLower = "save" Then
                Dim id As Integer = CType(e.Item.FindControl("lblID"), Label).Text
                Dim objPartIncidentalPOFacade As PartIncidentalPOFacade
                objPartIncidentalPOFacade = New PartIncidentalPOFacade(User)
                Dim objPartIncidentalPO As PartIncidentalPO = objPartIncidentalPOFacade.Retrieve(id)
                If id > 0 Then
                    Dim txtAlokasi As TextBox = CType(e.Item.FindControl("txtAlokasi"), TextBox)

                    If txtAlokasi.Text > objPartIncidentalPO.Alocation Then
                        txtAlokasi.Text = objPartIncidentalPO.Alocation
                        MessageBox.Show("Alokasi yang baru tidak boleh lebih dari sebelumnya")
                        Return
                    Else
                        If txtAlokasi.Text < 1 Then
                            objPartIncidentalPO.RowStatus = DBRowStatus.Deleted
                        End If

                        Dim objPartIncidentalDetail As PartIncidentalDetail = objPartIncidentalDetailFacade.Retrieve(objPartIncidentalPO.PartIncidentalDetail.ID)
                        'If objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal Or objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal_Sebagian Then
                        '    MessageBox.Show("Pesanan yang sudah dibatalkan tidak bisa dirubah")
                        '    Return
                        'End If
                        If objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal Then
                            MessageBox.Show("Pesanan yang sudah dibatalkan tidak bisa dirubah")
                            Return
                        End If

                        objPartIncidentalPO.Alocation = txtAlokasi.Text
                        objPartIncidentalPOFacade = New PartIncidentalPOFacade(User)
                        objPartIncidentalPOFacade.Update(objPartIncidentalPO)
                        BindToDataGrid(dgPartList.CurrentPageIndex)
                    End If

                End If

            End If
        End If
    End Sub

    Private Sub dgPartList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartList.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As PartIncidentalPO = CType(e.Item.DataItem, PartIncidentalPO)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgPartList.CurrentPageIndex * dgPartList.PageSize)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                lblDealerCode.Text = RowValue.PartIncidentalDetail.PartIncidentalHeader.Dealer.DealerCode
                Dim lblReqNo As Label = CType(e.Item.FindControl("lblReqNo"), Label)
                lblReqNo.Text = RowValue.PartIncidentalDetail.PartIncidentalHeader.RequestNumber
                Dim lblPKDate As Label = CType(e.Item.FindControl("lblPKDate"), Label)
                If RowValue.PartIncidentalDetail.PartIncidentalHeader.IncidentalDate < New DateTime(1900, 1, 1, 0, 0, 0) Then
                    lblPKDate.Text = ""
                Else
                    lblPKDate.Text = RowValue.PartIncidentalDetail.PartIncidentalHeader.IncidentalDate
                End If
                Dim lblPlanDate As Label = CType(e.Item.FindControl("lblPlanDate"), Label)
                Dim lblStatusDetail As Label = CType(e.Item.FindControl("lblStatusDetail"), Label)

                If RowValue.PartIncidentalDetail.PlanDate < New DateTime(1900, 1, 1, 0, 0, 0) Then
                    lblPlanDate.Text = ""
                Else
                    lblPlanDate.Text = RowValue.PartIncidentalDetail.PlanDate
                End If

                Dim lblPartSubNo As Label = CType(e.Item.FindControl("lblPartSubNo"), Label)

                Dim lblPartName As Label = CType(e.Item.FindControl("lblPartName"), Label)
                Dim lblPartNo As Label = CType(e.Item.FindControl("lblPartNo"), Label)

                lblPartNo.Text = RowValue.PartIncidentalDetail.SparePartMaster.PartNumber
                lblPartName.Text = RowValue.PartIncidentalDetail.SparePartMaster.PartName

                If RowValue.PartIncidentalDetail.SparePartMasterSubstitutionID > 0 Then
                    Dim spMasterSub As SparePartMaster = getSparePartMaster(RowValue.PartIncidentalDetail.SparePartMasterSubstitutionID)
                    If spMasterSub.ID > 0 Then
                        'lblPartSubNo.Text = spMasterSub.PartNumber
                        lblPartNo.Text = spMasterSub.PartNumber
                        lblPartName.Text = spMasterSub.PartName
                        e.Item.BackColor = Color.Yellow
                    End If
                End If

                Dim lblOrgQty As Label = CType(e.Item.FindControl("lblOrgQty"), Label)
                lblOrgQty.Text = RowValue.PartIncidentalDetail.Quantity
                Dim lblRemainQty As Label = CType(e.Item.FindControl("lblRemainQty"), Label)
                lblRemainQty.Text = RowValue.PartIncidentalDetail.RemainQuantity
                Dim txtAlokasi As TextBox = CType(e.Item.FindControl("txtAlokasi"), TextBox)
                txtAlokasi.Text = RowValue.Alocation
                Dim cbSelect As CheckBox = CType(e.Item.FindControl("cbSelect"), CheckBox)
                cbSelect.Enabled = True
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                Dim lbtnSave As LinkButton = CType(e.Item.FindControl("lbtnSave"), LinkButton)
                Dim lblAlokasi As Label = CType(e.Item.FindControl("lblAlokasi"), Label)
                lblAlokasi.Text = RowValue.Alocation
                lbtnDelete.Visible = SecurityProvider.Authorize(Context.User, SR.ENHSparepartPKUploadSAPEditivilege)
                lbtnSave.Visible = SecurityProvider.Authorize(Context.User, SR.ENHSparepartPKUploadSAPEditivilege)
                cbSelect.Visible = True
                txtAlokasi.Visible = True
                lblAlokasi.Visible = False
                If RowValue.PONumber.Trim <> String.Empty Then
                    cbSelect.Visible = False
                    lbtnDelete.Visible = False
                    lbtnSave.Visible = False
                    txtAlokasi.Visible = False
                    lblAlokasi.Visible = True
                End If

                If RowValue.PartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal Then
                    lblStatusDetail.Text = "<img src=""../images/red.gif"" border=""0"">"
                    lblStatusDetail.ToolTip = "Batal"
                    cbSelect.Enabled = False
                ElseIf RowValue.PartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal_Sebagian Then
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
            If Not SecurityProvider.Authorize(Context.User, SR.ENHSparepartPKUploadSAPView_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Upload Pesanan Khusus.")
            End If
            btnSave.Enabled = SecurityProvider.Authorize(Context.User, SR.ENHSparepartPKUploadSAPEditivilege)

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
        Dim objPartIncidentalDetailFacade As PartIncidentalDetailFacade = New PartIncidentalDetailFacade(User)
        Dim objPartIncidentalPOFacade As PartIncidentalPOFacade = New PartIncidentalPOFacade(User)

        For Each item As DataGridItem In dgPartList.Items
            Dim cb As CheckBox = CType(item.FindControl("cbSelect"), CheckBox)
            If cb.Checked = True Then
                Dim lblID As Label = CType(item.FindControl("lblID"), Label)

                Dim objPartIncidentalPO As PartIncidentalPO = objPartIncidentalPOFacade.Retrieve(CInt(lblID.Text))
                Dim objPartIncidentalDetail As PartIncidentalDetail = objPartIncidentalDetailFacade.Retrieve(objPartIncidentalPO.PartIncidentalDetail.ID)
                'If objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal Or objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal_Sebagian Then
                '    MessageBox.Show("Pesanan yang sudah dibatalkan tidak bisa diproses")
                '    Return
                'End If

                If objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal Then
                    MessageBox.Show("Pesanan yang sudah dibatalkan tidak bisa diproses")
                    Return
                End If


                Dim lblReqNo As Label = CType(item.FindControl("lblReqNo"), Label)
                Dim tempClass As PartTemporary = New PartTemporary(lblID.Text, lblReqNo.Text)
                collPartIncidentalPO.Add(tempClass)
            End If
        Next
        If collPartIncidentalPO.Count > 0 Then
            Dim blokList As ArrayList = PopulateBlokPart(collPartIncidentalPO)
            For Each item As ArrayList In blokList
                CreatePOPartIncidental(item)
            Next
            MessageBox.Show(SR.SaveSuccess)
            dgPartList.CurrentPageIndex = 0
            BindToDataGrid(0)
        Else
            MessageBox.Show("Tidak ada item yang terpilih.")
        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CancelRequest()
    End Sub

    Private Sub CancelRequest()
        Dim collPartIncidentalDetail As ArrayList = New ArrayList
        Dim objPartIncidentalDetailFacade As PartIncidentalDetailFacade = New PartIncidentalDetailFacade(User)
        Dim objPartIncidentalPOFacade As PartIncidentalPOFacade = New PartIncidentalPOFacade(User)
        'Dim isValidOperation As Boolean = True

        For Each item As DataGridItem In dgPartList.Items
            Dim cb As CheckBox = CType(item.FindControl("cbSelect"), CheckBox)
            If cb.Checked = True Then
                Dim lblID As Label = CType(item.FindControl("lblID"), Label)
                Dim lblOrgQty As Label = CType(item.FindControl("lblOrgQty"), Label)
                Dim lblRemainQty As Label = CType(item.FindControl("lblRemainQty"), Label)

                'Dim objPartIncidentalDetail As PartIncidentalDetail = New PartIncidentalDetail(lblID.Text)
                Dim objPartIncidentalPO As PartIncidentalPO = objPartIncidentalPOFacade.Retrieve(CInt(lblID.Text))
                Dim objPartIncidentalDetail As PartIncidentalDetail = objPartIncidentalDetailFacade.Retrieve(objPartIncidentalPO.PartIncidentalDetail.ID)
                Dim intPartIncidentalHeaderID As Integer = objPartIncidentalDetail.PartIncidentalHeader.ID
                Dim intSparePartMasterID As Integer = objPartIncidentalDetail.SparePartMaster.ID

                If lblRemainQty.Text = 0 Then
                    MessageBox.Show("Pemesanan yang remain quantity sudah 0 tidak bisa dibatalkan")
                    Return
                ElseIf objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal Or objPartIncidentalDetail.StatusDetail = PartIncidentalStatus.PartIncidentalDetailStatusEnum.Batal_Sebagian Then
                    MessageBox.Show("Pemesanan yang sudah dibatalkan tidak bisa dibatalkan lagi")
                    Return
                Else
                    If lblOrgQty.Text = lblRemainQty.Text Then
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

    Private Function PopulateBlokPart(ByVal objListPartTemp As ArrayList) As ArrayList
        Dim newList As ArrayList = New ArrayList
        Dim firstList As ArrayList = New ArrayList
        Dim temp As PartTemporary = CType(objListPartTemp.Item(0), PartTemporary)
        firstList.Add(temp)
        newList.Add(firstList)
        For i As Integer = 1 To objListPartTemp.Count - 1
            temp = CType(objListPartTemp.Item(i), PartTemporary)
            newList = ProcessNewPart(temp, newList)
        Next
        Return newList
    End Function

    Private Function ProcessNewPart(ByVal objPart As PartTemporary, ByVal newList As ArrayList) As ArrayList
        Dim isMatch As Boolean = False
        For i As Integer = 0 To newList.Count - 1
            Dim items As ArrayList = CType(newList.Item(i), ArrayList)
            For Each item As PartTemporary In items
                If objPart.RequestNumber = item.RequestNumber Then
                    isMatch = True
                    CType(newList.Item(i), ArrayList).Add(objPart)
                    Exit For
                End If
            Next
        Next
        If isMatch = False Then
            Dim tempList As ArrayList = New ArrayList
            tempList.Add(objPart)
            newList.Add(tempList)
        End If
        Return newList
    End Function

    Private Sub CreatePOPartIncidental(ByVal POList As ArrayList)
        If POList.Count > 0 Then
            Dim obj As PartIncidentalPO = New PartIncidentalPOFacade(User).Retrieve(CType(POList.Item(0), PartTemporary).ID)
            Dim ObjPO As SparePartPO = GetPOHeader(obj.PartIncidentalDetail.PartIncidentalHeader.Dealer)
            ArlPartHeader = New ArrayList
            For Each item As PartTemporary In POList
                Dim objPartPO As PartIncidentalPO = New PartIncidentalPOFacade(User).Retrieve(item.ID)
                ArlPartHeader.Add(GetSparePartPODetail(objPartPO))
            Next
            Dim _return As Integer = New SparePartPOFacade(User).InsertSparePartPO(ObjPO, ArlPartHeader)
            ObjPO = New SparePartPOFacade(User).Retrieve(_return)
            For Each item As PartTemporary In POList
                Dim _objPartPO As PartIncidentalPO = New PartIncidentalPOFacade(User).Retrieve(item.ID)
                _objPartPO.PONumber = ObjPO.PONumber
                Dim objPartPOFacade As PartIncidentalPOFacade = New PartIncidentalPOFacade(User)
                objPartPOFacade.Update(_objPartPO)
            Next
            CreateTextFileForKTB(obj, ObjPO.PONumber)
        End If
    End Sub


    Private Function GetSpMaster(ByVal id As Integer) As SparePartMaster
        Dim sp As SparePartMaster
        Dim facade As SparePartMasterFacade = New SparePartMasterFacade(User)
        sp = facade.Retrieve(id)
        If sp Is Nothing Then
            sp = New SparePartMaster
        End If
        Return sp
    End Function

    Private Function GetSparePartPODetail(ByVal obj As PartIncidentalPO) As SparePartPODetail
        Dim objPart As SparePartMaster
        If obj.PartIncidentalDetail.SparePartMasterSubstitutionID > 0 Then
            objPart = GetSpMaster(obj.PartIncidentalDetail.SparePartMasterSubstitutionID)
        Else
            objPart = obj.PartIncidentalDetail.SparePartMaster
        End If
        Dim objPODetail As SparePartPODetail = New SparePartPODetail
        objPODetail.Quantity = obj.Alocation
        objPODetail.SparePartMaster = objPart
        objPODetail.CheckListStatus = String.Empty
        objPODetail.RetailPrice = objPart.RetalPrice
        Return objPODetail
    End Function

    Private Function GetPOHeader(ByVal objDealer As Dealer) As SparePartPO
        Dim objPO As SparePartPO
        objPO = New SparePartPO
        objPO.PODate = Now
        objPO.Dealer = objDealer
        objPO.OrderType = "K"
        objPO.ProcessCode = "S"
        Return objPO
    End Function

    Private Sub CreateFolder(ByVal folderName As String)
        Dim dirInfo As DirectoryInfo = New DirectoryInfo(folderName)
        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If
    End Sub

    Private Sub CreateTextFileForKTB(ByVal obj As PartIncidentalPO, ByVal poNumber As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim FOLDER_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("DNetServerFolder") & poNumber.Substring(1, 4)
        Dim FILE_NAME As String = FOLDER_NAME + "\" + poNumber + ".PKD"
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _sapServer As String = KTB.DNet.Lib.WebConfig.GetValue("SAPServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _sapServer)
        Dim succes As Boolean = False
        Dim msg As String = String.Empty
        Dim fs As FileStream
        Dim w As StreamWriter
        Try
            succes = imp.Start()
            If succes Then
                CreateFolder(FOLDER_NAME)
                If File.Exists(FILE_NAME) Then
                    File.Delete(FILE_NAME)
                End If
                fs = New FileStream(FILE_NAME, FileMode.CreateNew)
                w = New StreamWriter(fs)
                WritePOHeaderToFile(w, poNumber, obj.PartIncidentalDetail.PartIncidentalHeader.Dealer.DealerName)
                WritePODetailToFile(w, poNumber)
            Else
                MessageBox.Show("Gagal Login ke SAP Server.")
            End If
        Catch ex As Exception
            MessageBox.Show(SR.DataSendFail)
        Finally
            w.Close()
            fs.Close()
            imp.StopImpersonate()
            imp = Nothing
        End Try
    End Sub

    Private Sub WritePOHeaderToFile(ByRef w As StreamWriter, ByVal poNumber As String, ByVal dealerCode As String)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        sbSetARecord.Append("T")
        sbSetARecord.Append(poNumber.PadRight(15, pad))
        sbSetARecord.Append(Left(dealerCode, 25).PadRight(25, pad))
        sbSetARecord.Append(String.Format("{0:yyyyMMdd}", Now))
        sbSetARecord.Append(ArlPartHeader.Count.ToString.PadLeft(4, "0"))
        w.WriteLine(sbSetARecord.ToString)
    End Sub

    Private Function WritePODetailToFile(ByRef w As StreamWriter, ByVal poNumber As String)
        Dim sbSetARecord As StringBuilder = New StringBuilder
        Dim pad As Char = " "
        Dim indek As Integer = 0
        For Each objPODetail As SparePartPODetail In ArlPartHeader
            indek = indek + 1
            sbSetARecord.Remove(0, sbSetARecord.Length)
            sbSetARecord.Append("D")
            sbSetARecord.Append(poNumber.PadRight(15, pad))
            sbSetARecord.Append(objPODetail.SparePartMaster.PartNumber.PadRight(20, pad))
            sbSetARecord.Append(objPODetail.Quantity.ToString.PadLeft(5, "0"))
            sbSetARecord.Append(String.Format("{0:yyyyMMdd}", objPODetail.SparePartPO.PODate))
            sbSetARecord.Append(indek.ToString.PadLeft(4, "0"))
            w.WriteLine(sbSetARecord.ToString)
        Next
    End Function

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Response.Redirect("frmDownloadPartIncidentalPOProcess.aspx")
    End Sub


End Class

Public Class PartTemporary
    Private _id As Integer
    Private _reqNo As String

    Public Sub New()

    End Sub

    Public Sub New(ByVal mid As Integer, ByVal mReqNo As String)
        _id = mid
        _reqNo = mReqNo
    End Sub

    Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal Value As Integer)
            _id = Value
        End Set
    End Property

    Property RequestNumber() As String
        Get
            Return _reqNo
        End Get
        Set(ByVal Value As String)
            _reqNo = Value
        End Set
    End Property
End Class

