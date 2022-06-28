Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.PO

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Linq
Imports System.Collections.Generic

Public Class FrmDealerPOTargetList
    Inherits System.Web.UI.Page

    Private sesHelper As New SessionHelper
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private deletePriv As Boolean
    Private oDealer As Dealer
    Private SessionGridDealerPOTarget = "FrmDealerPOTargetList.SessionGrid"

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Authorization()
        If Not IsPostBack Then
            ViewState("currSortColumn") = "ID"
            ViewState("currSortDirection") = Sort.SortDirection.DESC

            PageInit()
            BindDDLCategory()
            BindDdlModel()
            BindMonth()
            BindYear()

            If SesDealer().Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtDealerCode.Visible = False
                lnkBtnPopUpDealer.Visible = False
                lblDealerCode.Visible = True
                txtDealerCode.Text = SesDealer().DealerCode
                lblDealerCode.Text = SesDealer().DealerCode & " / " & SesDealer().DealerName
            Else
                txtDealerCode.Visible = True
                lnkBtnPopUpDealer.Visible = True
                lblDealerCode.Visible = False
                lblDealerCode.Text = ""
            End If

            ReadData()   '-- Read all data matching criteria
            BindGrid(dgDealerPOTarget.CurrentPageIndex)  '-- Bind page-1

        End If

        lnkBtnPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
    End Sub

    Private Sub BindDdlModel()
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlVehicleModel, ddlCategory.SelectedItem.Text)
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.Program_TOP_Khusus_Lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PURCHASE ORDER - PROGRAM TOP KHUSUS")
        End If
    End Sub

    Private Sub BindMonth()
        '
        Dim al As ArrayList = enumMonthGet.RetriveMonth()
        ddlMonth.DataSource = enumMonthGet.RetriveMonth()
        ddlMonth.DataValueField = "ValStatus"
        ddlMonth.DataTextField = "NameStatus"
        ddlMonth.DataBind()

        ddlMonth.SelectedValue = DateTime.Now.Month
    End Sub

    Private Sub BindYear()

        Dim a As Integer
        Dim now As DateTime = DateTime.Now
        For a = -1 To 1
            ddlYear.Items.Insert(0, New ListItem(now.AddYears(a).Year, now.AddYears(a).Year))
        Next
        ddlYear.SelectedValue = now.Year
    End Sub

    Private Sub PageInit()
        icTglBerlaku.Value = Date.Now
    End Sub

    Private Sub BindDDLCategory()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, "MMC"))
        arrDDL = New CategoryFacade(User).Retrieve(criterias)

        With ddlCategory
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "CategoryCode"
            .DataValueField = "ID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End With
        ddlCategory.SelectedIndex = 0
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrDealerPOTargetList As ArrayList = CType(sesHelper.GetSession(SessionGridDealerPOTarget), ArrayList)
        If arrDealerPOTargetList.Count <> 0 Then
            'CommonFunction.SortListControl(arrDealerPOTargetList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrDealerPOTargetList, pageIndex, dgDealerPOTarget.PageSize)
            dgDealerPOTarget.DataSource = PagedList
            dgDealerPOTarget.VirtualItemCount = arrDealerPOTargetList.Count()
            dgDealerPOTarget.DataBind()
        Else
            dgDealerPOTarget.DataSource = New ArrayList
            dgDealerPOTarget.VirtualItemCount = 0
            dgDealerPOTarget.CurrentPageIndex = 0
            dgDealerPOTarget.DataBind()
        End If
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerPOTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtDealerCode.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(DealerPOTarget), "Dealer.DealerCode", MatchType.InSet, "('" + txtDealerCode.Text.Trim.Replace(";", "','") + "')"))
        End If

        If ddlCategory.SelectedIndex <> 0 Then
            crit.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        If ddlCategory.SelectedIndex <> 0 And ddlVehicleModel.SelectedIndex <> 0 Then
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlVehicleModel.SelectedValue
            Dim strSql2 As String = "select a.ID from DealerPOTarget a join VechileModel b on a.VehicleModelID = b.ID and b.RowStatus = 0 "
            strSql2 += " where a.RowStatus = 0 and b.ID in (" & strSql & ")"
            crit.opAnd(New Criteria(GetType(DealerPOTarget), "ID", MatchType.InSet, "(" & strSql2 & ")"))
        End If
        'If ddlVehicleModel.SelectedIndex <> 0 Then
        '    crit.opAnd(New Criteria(GetType(DealerPOTarget), "VechileModel.ID", MatchType.Exact, ddlVehicleModel.SelectedValue))
        'End If

        If cbDate.Checked Then
            Dim dtCriteriaDate1 As DateTime = GenerateCriteriaDate(True)
            Dim tglBerlaku As New Date(dtCriteriaDate1.Year, dtCriteriaDate1.Month, dtCriteriaDate1.Day, 0, 0, 0)
            Dim strSQl As String = "select ID from DealerPOTarget "
            strSQl += "where convert(datetime, cast(year(ValidFrom) as varchar) + '/' + cast(month(ValidFrom) as varchar) + '/01')  <= '" + Format(tglBerlaku, "yyyy-MM-dd HH:mm:ss") + "' "
            strSQl += "and convert(datetime, cast(year(ValidTo) as varchar) + '/' + cast(month(ValidTo) as varchar) + '/01')  >= '" + Format(tglBerlaku, "yyyy-MM-dd HH:mm:ss") + "' "

            crit.opAnd(New Criteria(GetType(DealerPOTarget), "ID", MatchType.InSet, "(" & strSQl & ")"))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DealerPOTarget), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrDealerPOTargetList As ArrayList = New DealerPOTargetFacade(User).Retrieve(crit, sortColl)


        '================================================================================================
        Dim arrDealerPOTargetList2 = (From obj As DealerPOTarget In arrDealerPOTargetList
                               Order By obj.Dealer.ID, obj.VechileModel.ID, obj.IsDefault, Format(obj.ValidFrom, "yyyyMM"), obj.Sequence
                               Select obj).ToList()

        Dim intModelDPO As Integer = 0
        Dim blnIsCurrentPeriod As Boolean = False
        Dim sisaBefore As Integer = 0
        Dim arrDealerPOTargetList3 As ArrayList = New ArrayList
        Dim Sisa As Decimal = 0, Terpakai As Decimal = 0
        For Each objDPO As DealerPOTarget In arrDealerPOTargetList2
            Terpakai = 0
            If intModelDPO <> objDPO.VechileModel.ID Then
                intModelDPO = objDPO.VechileModel.ID
                sisaBefore = 0
                blnIsCurrentPeriod = False
            End If


            Dim PDetailFac As New PODetailFacade(User)
            Dim criteriaPD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
            criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Dealer.DealerCode", MatchType.Exact, objDPO.Dealer.DealerCode))
            criteriaPD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & objDPO.VechileModel.ID & ")"))
            If objDPO.IsDefault = 0 Then
                criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, objDPO.ValidFrom.Year & "-" & objDPO.ValidFrom.Month & "-01 00:00:00.000"))
                criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.LesserOrEqual, objDPO.ValidTo.Year & "-" & objDPO.ValidTo.Month & "-" & DateTime.DaysInMonth(objDPO.ValidTo.Year, objDPO.ValidTo.Month) & " 00:00:00.000"))
            Else
                If cbDate.Checked = True Then
                    criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, ddlYear.SelectedValue & "-" & ddlMonth.SelectedValue & "-01 00:00:00.000"))
                    criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.LesserOrEqual, ddlYear.SelectedValue & "-" & ddlMonth.SelectedValue & "-" & DateTime.DaysInMonth(ddlYear.SelectedValue, ddlMonth.SelectedValue) & " 00:00:00.000"))
                Else
                    criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.GreaterOrEqual, Date.Now.Year & "-" & Date.Now.Month & "-01 00:00:00.000"))
                    criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.ReqAllocationDateTime", MatchType.LesserOrEqual, Date.Now.Year & "-" & Date.Now.Month & "-" & DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month) & " 00:00:00.000"))
                End If
            End If

            criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.InSet, "(0, 2, 4, 6, 8)"))
            criteriaPD.opAnd(New Criteria(GetType(PODetail), "FreeDays", MatchType.Exact, objDPO.FreeDays))
            Dim arlPoDetail As ArrayList = PDetailFac.Retrieve(criteriaPD)
            If arlPoDetail.Count > 0 Then
                For Each pDetail As PODetail In arlPoDetail
                    Select Case pDetail.POHeader.Status
                        Case 0
                            Terpakai -= pDetail.ReqQty
                        Case 2
                            If pDetail.AllocQty = 0 Then
                                Terpakai -= pDetail.ReqQty
                            ElseIf pDetail.AllocQty > 0 Then
                                Terpakai -= pDetail.AllocQty
                            End If
                        Case 4, 6, 8
                            Terpakai -= pDetail.AllocQty
                    End Select
                Next
            End If

            objDPO.QtyUsed = Math.Abs(Terpakai)
            objDPO.QtySisa = (objDPO.MaxQuantity + sisaBefore) - Math.Abs(Terpakai)

            If objDPO.IsDefault = 0 Then
                If blnIsCurrentPeriod = False Then
                    If CType(Format(Date.Now, "yyyyMMdd"), Integer) >= CType(Format(objDPO.ValidFrom, "yyyyMMdd"), Integer) AndAlso CType(Format(Date.Now, "yyyyMMdd"), Integer) <= CType(Format(objDPO.ValidTo, "yyyyMMdd"), Integer) Then
                        sisaBefore = 0
                        blnIsCurrentPeriod = True
                    Else
                        sisaBefore = objDPO.QtySisa
                    End If
                Else
                    sisaBefore = 0
                End If
            Else
                'sisaBefore = 0
            End If
            'End If
            arrDealerPOTargetList3.Add(objDPO)
        Next

        sesHelper.SetSession(SessionGridDealerPOTarget, arrDealerPOTargetList3)
        If arrDealerPOTargetList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Private Function MaxDay(ByVal Month As Short, ByVal year As Integer) As Short
        Select Case Month
            Case 1, 3, 5, 7, 8, 10, 12
                Return 31
            Case 4, 6, 9, 11
                Return 30
            Case 2
                If year Mod 4 = 0 Then
                    Return 29
                Else
                    Return 28
                End If
        End Select

    End Function

    Private Function GenerateCriteriaDate(ByVal IsLowerBound As Boolean)
        Dim dt As DateTime

        If (ddlYear.SelectedValue <> "") Then
            If (ddlMonth.SelectedValue <> "") Then
                If IsLowerBound Then
                    dt = New DateTime(CInt(ddlYear.SelectedValue), CInt(ddlMonth.SelectedValue), 1, 0, 0, 0)
                Else
                    dt = New DateTime(CInt(ddlYear.SelectedValue), CInt(ddlMonth.SelectedValue), MaxDay(CInt(ddlMonth.SelectedValue), CInt(ddlYear.SelectedValue)), 23, 59, 59)
                End If
            Else
                If IsLowerBound Then
                    dt = New DateTime(CInt(ddlYear.SelectedValue), 1, 1, 0, 0, 0)
                Else
                    dt = New DateTime(CInt(ddlYear.SelectedValue), 12, 31, 23, 59, 59)
                End If
            End If
        Else
            dt = New DateTime(1900, 1, 1)
        End If

        Return dt
    End Function

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ReadData()
        dgDealerPOTarget.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dgDealerPOTarget.CurrentPageIndex)  '-- Bind page-1
    End Sub

    Private Sub dgDealerPOTarget_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDealerPOTarget.PageIndexChanged
        '-- Change datagrid page

        ReadData()
        dgDealerPOTarget.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
    End Sub

    Private Sub dgDealerPOTarget_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDealerPOTarget.SortCommand
        '-- Sort datagrid rows based on a column header clicked

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

        '-- Bind page-1
        dgDealerPOTarget.CurrentPageIndex = 0
        ReadData()
        BindGrid(dgDealerPOTarget.CurrentPageIndex)
    End Sub

    Protected Sub dgDealerPOTarget_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgDealerPOTarget.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As DealerPOTarget = CType(e.Item.DataItem, DealerPOTarget)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgDealerPOTarget.PageSize * dgDealerPOTarget.CurrentPageIndex)).ToString

            Dim cbIsDefault As CheckBox = CType(e.Item.FindControl("cbIsDefault"), CheckBox)

            If oData.IsDefault = 1 Then
                cbIsDefault.BackColor = Color.Yellow
                cbIsDefault.Checked = True
            Else
                cbIsDefault.BackColor = Color.White
                cbIsDefault.Checked = False
            End If

            Dim lblTerpakai As Label = CType(e.Item.FindControl("lblTerpakai"), Label)
            Dim lblSisa As Label = CType(e.Item.FindControl("lblSisa"), Label)

            lblTerpakai.Text = oData.QtyUsed
            lblSisa.Text = oData.QtySisa

            'TerpakaiDanSisa(oData.Dealer.DealerCode, oData.VechileModel.ID, oData.MaxQuantity, oData.FreeDays, lblSisa.Text, lblTerpakai.Text)

        End If
    End Sub

    Private Sub TerpakaiDanSisa(DealerCode As String, modelID As Integer, maxQty As Integer, freedays As Integer, ByRef Sisa As String, ByRef Terpakai As String)
        Dim PDetailFac As New PODetailFacade(User)
        Dim criteriaPD As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, (CType(DBRowStatus.Active, Short))))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Dealer.DealerCode", MatchType.Exact, DealerCode))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "ContractDetail.VechileColor.VechileType.VechileModel.ID", MatchType.InSet, "(" & modelID & ")"))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "POHeader.Status", MatchType.InSet, "(0, 2, 4, 6, 8)"))
        criteriaPD.opAnd(New Criteria(GetType(PODetail), "FreeDays", MatchType.Exact, freedays))
        Dim arlPoDetail As ArrayList = PDetailFac.Retrieve(criteriaPD)

        Sisa = maxQty

        For Each pDetail As PODetail In arlPoDetail
            Select Case pDetail.POHeader.Status
                Case 0
                    Sisa -= pDetail.ReqQty
                Case 2
                    If pDetail.AllocQty = 0 Then
                        Sisa -= pDetail.ReqQty
                    ElseIf pDetail.AllocQty > 0 Then
                        Sisa -= pDetail.AllocQty
                    End If
                Case 4, 6, 8
                    Sisa -= pDetail.AllocQty
            End Select
        Next

        Terpakai = maxQty - Sisa
    End Sub


    Protected Sub hdnDealer_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealer.ValueChanged
        txtDealerCode.Text = hdnDealer.Value.Trim
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        BindDdlModel()
    End Sub
End Class