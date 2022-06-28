
#Region " .NET Base Class Namespace Imports "
Imports System.Linq
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security

#End Region

Public Class FrmInvoiceRevision
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "
    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Custom Variable Declaration "
    Private _sesshelper As New SessionHelper
    Dim _PCAccessAllowed As Boolean = False
    Dim _CVAccessAllowed As Boolean = False
    Dim _LCVAccessAllowed As Boolean = False
    Private objDealer As Dealer
#End Region

#Region " EventHandler "

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.RevisiFakturDaftarLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=REVISI FAKTUR - INPUT REVISI FAKTUR")
        End If

        _PCAccessAllowed = SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege)
        _CVAccessAllowed = SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege)
        _LCVAccessAllowed = SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege)

        If (Not _PCAccessAllowed) And (Not _CVAccessAllowed) And (Not _LCVAccessAllowed) Then
            Me.btnSearch.Visible = False
            Me.ddlCategory.Visible = False
        End If

        Dim objDealer As Dealer = CType(_sesshelper.GetSession("DEALER"), Dealer)

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            Me.dgInvoiceList.Columns(2).Visible = SecurityProvider.Authorize(Context.User, SR.RevisiFakturInput_Privilege)
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()

        objDealer = _sesshelper.GetSession("DEALER")

        If Not IsPostBack Then
            ViewState("currSortColumn") = "ChassisNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            DisplayDealer()  '-- Display dealer from login
            BindDropdownList()  '-- Init dropdownlist

            Dim crit As Hashtable = New Hashtable
            crit = CType(Session("CriteriaFormInvoiceList"), Hashtable)
            If Not crit Is Nothing Then
                lblDealerCode.Text = CStr(crit.Item("Dealer"))
                txtDealerBranchCode.Text = CStr(crit.Item("DealerBranchCode"))
                txtChassisNo.Text = CStr(crit.Item("ChassisNumber"))
                txtInvoiceNo.Text = CStr(crit.Item("InvoiceNumber"))
                txtCustomerCode.Text = CStr(crit.Item("CustomerCode"))
                ddlCategory.SelectedValue = CStr(crit.Item("Category"))
                icStartFaktur.Value = CType(crit("StartFaktur"), Date)
                icEndFaktur.Value = CType(crit("EndFaktur"), Date)
                icStartValid.Value = CType(crit("StartValid"), Date)
                icEndValid.Value = CType(crit("EndValid"), Date)
                icStartPrinted.Value = CType(crit("StartPrinted"), Date)
                icEndPrinted.Value = CType(crit("EndPrinted"), Date)
                chkFakturPeriod.Checked = CType(crit("chkFakturPeriod"), Boolean)
                chkValidPeriod.Checked = CType(crit("chkValidPeriod"), Boolean)
                chkPrintedPeriod.Checked = CType(crit("chkPrintedPeriod"), Boolean)
                chkIsTemporary.Checked = CType(crit("chkIsTemporary"), Boolean)
                dgInvoiceList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
                Try
                    CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
                    ddlSubCategory.SelectedValue = CStr(crit.Item("ddlSubCategory"))
                Catch ex As Exception

                End Try

                ReadData()   '-- Read all data matching criteria
                BindPage(dgInvoiceList.CurrentPageIndex)  '-- Bind page-1
            End If

            lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
            lblPopUp.Attributes("onClick") = "ShowPPTujuanSelection();"
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        storeCriteria()
        ReadData()   '-- Read all data matching criteria

        Dim InvoiceResList As ArrayList = CType(_sesshelper.GetSession("InvoiceResList"), ArrayList)

        If Not IsNothing(InvoiceResList) Then
            dgInvoiceList.CurrentPageIndex = 0
            BindPage(dgInvoiceList.CurrentPageIndex)  '-- Bind page-1
        End If
    End Sub

    Private Sub dgInvoiceList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgInvoiceList.PageIndexChanged
        '-- Change datagrid page

        dgInvoiceList.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)
    End Sub

    Private Sub dgInvoiceList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgInvoiceList.SortCommand
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
        dgInvoiceList.CurrentPageIndex = 0
        ReadData()
        BindPage(dgInvoiceList.CurrentPageIndex)

    End Sub

    Private Sub dgInvoiceList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgInvoiceList.ItemCommand

        If e.CommandName = "lnkDetail" Then
            '-- Retrieve Invoice and its related end customer if any
            Dim ChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

            '-- Store Invoice and its related end customer for display on form FrmEntryInvoiceRevision.aspx
            _sesshelper.SetSession("ChassisMaster", ChassisMaster)

            '-- Store the calling page
            _sesshelper.SetSession("FrmEntryInvoice_CalledBy", "FrmInvoiceRevision.aspx")

            storeCriteria()

            '-- Display Invoice and its related end customer on Entry Invoice page
            Server.Transfer("FrmEntryInvoice.aspx?ChassisMasterID=" & e.Item.Cells(0).Text.Trim)
        ElseIf e.CommandName = "lnkCreate" Then
            '-- Retrieve Invoice and its related end customer if any
            Dim ChassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))

            If ChassisMaster.PendingDesc.ToLower().Contains("block_faktur") OrElse ChassisMaster.PendingDesc.ToLower().Contains("block faktur") OrElse ChassisMaster.PendingDesc.ToLower().Contains("blok faktur") Then
                hdnMsg.Value = ChassisMaster.PendingDesc
                Exit Sub
            End If
            '-- Store Invoice and its related end customer for display on form FrmEntryInvoice.aspx
            _sesshelper.SetSession("ChassisMaster", ChassisMaster)

            storeCriteria()

            '-- Display Invoice and its related end customer on Entry Invoice page
            Response.Redirect("FrmEntryInvoiceRevisionType.aspx?ChassisMasterID=" & e.Item.Cells(0).Text.Trim)

        End If

    End Sub

    Private Sub dgInvoiceList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgInvoiceList.ItemDataBound
        '-- Handle data binding

        Dim RowValue As ChassisMaster = CType(e.Item.DataItem, ChassisMaster)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '-- Grid detail items

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dgInvoiceList.CurrentPageIndex * dgInvoiceList.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No
        End If

    End Sub

    Protected Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub
#End Region

#Region " Custom Method "

    Private Sub DisplayDealer()
        '-- Display dealer info from login session "DEALER"

        If Not IsNothing(Session("DEALER")) Then
            Dim _Dealer As Dealer = CType(Session("DEALER"), Dealer)
            lblDealerCode.Text = _Dealer.DealerCode
            lblNamaDealer.Text = _Dealer.DealerName
        End If

    End Sub

    Private Sub BindDropdownList()

        '-- Category criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim cat As String = ""
        If _PCAccessAllowed Then
            cat = cat & "'PC',"
        End If
        If _CVAccessAllowed Then
            cat = cat & "'CV',"
        End If
        If _LCVAccessAllowed Then
            cat = cat & "'LCV',"
        End If
        If cat <> "" Then
            cat = "(" & cat.Substring(0, cat.Length - 1) & ")"
            criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.InSet, cat))
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code

        '-- Bind Category dropdownlist
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item

        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub

    Private Sub storeCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("Dealer", lblDealerCode.Text)
        crit.Add("DealerBranchCode", txtDealerBranchCode.Text)
        crit.Add("ChassisNumber", txtChassisNo.Text)
        crit.Add("InvoiceNumber", txtInvoiceNo.Text)
        crit.Add("Category", ddlCategory.SelectedValue)
        crit.Add("ddlSubCategory", ddlSubCategory.SelectedValue)
        crit.Add("StartFaktur", icStartFaktur.Value)
        crit.Add("EndFaktur", icEndFaktur.Value)
        crit.Add("StartValid", icStartValid.Value)
        crit.Add("EndValid", icEndValid.Value)
        crit.Add("StartPrinted", icStartPrinted.Value)
        crit.Add("EndPrinted", icEndPrinted.Value)
        crit.Add("chkFakturPeriod", chkFakturPeriod.Checked)
        crit.Add("chkValidPeriod", chkValidPeriod.Checked)
        crit.Add("chkPrintedPeriod", chkPrintedPeriod.Checked)
        crit.Add("chkIsTemporary", chkIsTemporary.Checked)
        crit.Add("PageIndex", dgInvoiceList.CurrentPageIndex)

        _sesshelper.SetSession("CriteriaFormInvoiceList", crit)
    End Sub

    Private Sub ReadData()
        '-- Read all data selected
        Dim InvoiceResultList As ArrayList = New ArrayList
        Dim isSearch As Boolean = False

        If chkFakturPeriod.Checked Then
            '-- Faktur date
            If icStartFaktur.Value > icEndFaktur.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal Faktur tidak valid")
                Return  '-- Directly exits
            Else
                If icEndFaktur.Value.Subtract(icStartFaktur.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal Faktur harus <= 65 hari")
                    Return  '-- Directly exits
                End If
            End If
        End If

        If chkValidPeriod.Checked Then
            '-- Validation date
            If icStartValid.Value > icEndValid.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal validasi tidak valid")
                Return  '-- Directly exits
            Else
                If icEndValid.Value.Subtract(icStartValid.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal validasi harus <= 65 hari")
                    Return  '-- Directly exits
                End If
            End If
        End If

        If chkPrintedPeriod.Checked Then
            '-- Printed date
            If icStartPrinted.Value > icEndPrinted.Value Then
                '-- It must be Start date <= End date
                MessageBox.Show("Interval tanggal Selesai tidak valid")
                Return  '-- Directly exits
            Else
                If icEndPrinted.Value.Subtract(icStartPrinted.Value).Days > 65 Then
                    '-- The difference must be lesser or equal to 65 days
                    MessageBox.Show("Selisih tanggal Selesai harus <= 65 hari")
                    Return  '-- Directly exits
                End If
            End If
        End If

        '-- Row status = active
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '-- Dealer Logedin
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "Dealer.DealerCode", MatchType.Exact, lblDealerCode.Text.Trim()))

        '-- Nomor chassis
        If txtChassisNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.[Partial], txtChassisNo.Text.Trim()))
            isSearch = True
        End If

        '-- Nomor faktur
        If txtInvoiceNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.FakturNumber", MatchType.[Partial], txtInvoiceNo.Text.Trim()))
            isSearch = True

        End If

        '-- Category
        If ddlCategory.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
            isSearch = True

        End If

        If txtCustomerCode.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.Customer.Code", MatchType.Exact, txtCustomerCode.Text.Trim()))
            isSearch = True
        End If

        '-- Status Faktur
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.Exact, Convert.ToInt32(EnumDNET.enumFakturKendaraan.Selesai)), "(", True)
        criterias.opOr(New Criteria(GetType(ChassisMaster), "FakturStatus", MatchType.Exact, Convert.ToInt32(EnumDNET.enumFakturKendaraan.Proses)), "(", True)
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.IsTemporary", MatchType.Exact, 1), "))", False)

        If txtDealerBranchCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ChassisMaster), "EndCustomer.SPKFaktur.SPKHeader.SalesmanHeader.DealerBranch.DealerBranchCode", MatchType.Exact, txtDealerBranchCode.Text))
            isSearch = True
        End If

        If chkFakturPeriod.Checked Then
            '-- Periode Konfirmasi
            Dim StartConfirm As New DateTime(CInt(icStartFaktur.Value.Year), CInt(icStartFaktur.Value.Month), CInt(icStartFaktur.Value.Day), 0, 0, 0)
            Dim EndConfirm As New DateTime(CInt(icEndFaktur.Value.Year), CInt(icEndFaktur.Value.Month), CInt(icEndFaktur.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.FakturDate", MatchType.GreaterOrEqual, Format(StartConfirm, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.FakturDate", MatchType.LesserOrEqual, Format(EndConfirm, "yyyy-MM-dd HH:mm:ss")))
            isSearch = True
        End If

        If chkValidPeriod.Checked Then
            '-- Periode Validasi
            Dim StartValid As New DateTime(CInt(icStartValid.Value.Year), CInt(icStartValid.Value.Month), CInt(icStartValid.Value.Day), 0, 0, 0)
            Dim EndValid As New DateTime(CInt(icEndValid.Value.Year), CInt(icEndValid.Value.Month), CInt(icEndValid.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.ValidateTime", MatchType.GreaterOrEqual, Format(StartValid, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.ValidateTime", MatchType.LesserOrEqual, Format(EndValid, "yyyy-MM-dd HH:mm:ss")))
            isSearch = True
        End If

        If chkPrintedPeriod.Checked Then
            '-- Periode Printed
            Dim HandoverDateStart As New DateTime(CInt(icStartPrinted.Value.Year), CInt(icStartPrinted.Value.Month), CInt(icStartPrinted.Value.Day), 0, 0, 0)
            Dim HandoverDateEnd As New DateTime(CInt(icEndPrinted.Value.Year), CInt(icEndPrinted.Value.Month), CInt(icEndPrinted.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.PrintedTime", MatchType.GreaterOrEqual, Format(HandoverDateStart, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.PrintedTime", MatchType.LesserOrEqual, Format(HandoverDateEnd, "yyyy-MM-dd HH:mm:ss")))
            isSearch = True
        End If

        ' -- Temposrary Faktur
        If chkIsTemporary.Checked Then
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.IsTemporary", MatchType.Exact, CType(CInt(Int(chkIsTemporary.Checked)), Short)))
            isSearch = True
        End If

        If ddlCategory.SelectedValue.ToString() <> "" And ddlSubCategory.SelectedValue <> "-1" Then
            'Dim Sql As String = ""
            'Dim oEVSC As EnumVehicleSubCategory = New EnumVehicleSubCategory
            'Dim sVals As String = oEVSC.GetSQLValue(ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue)

            'Sql &= "  SELECT  vc.[ID] FROM    dbo.VechileType vt INNER JOIN dbo.[VechileColor] vc ON [vc].[VechileTypeID] = [vt].[ID] WHERE   vt.[RowStatus] = 0 AND vc.[RowStatus] = 0 "
            'Dim i As Integer
            'For i = 0 To sVals.Split(";").Length - 1
            '    If i = 0 Then
            '        Sql &= " and (vt.Description like '" & sVals.Split(";")(i) & "' "
            '        If sVals.Split(";").Length = 1 Then Sql &= ")"
            '    ElseIf i = sVals.Split(";").Length - 1 Then
            '        Sql &= " or vt.Description like '" & sVals.Split(";")(i) & "') "
            '    Else
            '        Sql &= " or vt.Description like '" & sVals.Split(";")(i) & "'"
            '    End If
            'Next
            'criterias.opAnd(New Criteria(GetType(ChassisMaster), "VechileColor.ID", MatchType.InSet, "(" & Sql & ")"))

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            Dim strSql2 As String = "select distinct a.ID from VechileColor a join VechileType b on a.VechileTypeID = b.ID and b.RowStatus = 0 "
            strSql2 += " join VechileModel c on b.ModelID = c.ID and c.RowStatus = 0 where a.RowStatus = 0 and c.ID in (" & strSql & ")"
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "VechileColor.ID", MatchType.InSet, "(" & strSql2 & ")"))

            isSearch = True
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        'oCM.EndCustomer.ValidateBy 
        sortColl.Add(New Sort(GetType(ChassisMaster), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))  '-- Nomor chassis

        '-- Retrieve recordset
        InvoiceResultList = New ChassisMasterFacade(User).RetrieveByCriteria(criterias, sortColl)

        If Not isSearch Then
            InvoiceResultList = New System.Collections.ArrayList((From item As ChassisMaster In InvoiceResultList.OfType(Of ChassisMaster)()
                            Select item Order By item.EndCustomer.ValidateTime Descending).Take(100).ToList())
        End If

        If InvoiceResultList.Count < 1 Then
            _sesshelper.SetSession("InvoiceResList", InvoiceResultList)

            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        Else
            Dim result = New System.Collections.ArrayList((From item As ChassisMaster In InvoiceResultList.OfType(Of ChassisMaster)()
                    Where (item.EndCustomer.RevisionFaktur IsNot Nothing AndAlso item.EndCustomer.RevisionFaktur.RevisionStatus = EnumDNET.enumFakturKendaraanRev.Selesai) Or
                        item.EndCustomer.RevisionFaktur Is Nothing
                    Select item).ToList())

            '-- Store recordset into session for later use
            _sesshelper.SetSession("InvoiceResList", result)

        End If

        'store profileDetail into session
        'add by anh 2012-01-10
        'Dim critProfiles As New CriteriaComposite(New Criteria(GetType(ProfileDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'critProfiles.opAnd(New Criteria(GetType(ProfileDetail), "ProfileHeader.ID", MatchType.InSet, "(5,6)"))

        'Dim profileDetailList As ArrayList = New ProfileDetailFacade(User).Retrieve(critProfiles)
        '_sesshelper.SetSession("ProfileDetailList", profileDetailList)

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim InvoiceResList As ArrayList = CType(_sesshelper.GetSession("InvoiceResList"), ArrayList)
        If InvoiceResList.Count <> 0 Then
            Dim PagedList As ArrayList = ArrayListPager.DoPage(InvoiceResList, pageIndex, dgInvoiceList.PageSize)
            dgInvoiceList.DataSource = PagedList
            dgInvoiceList.VirtualItemCount = InvoiceResList.Count()
            dgInvoiceList.DataBind()
        Else
            dgInvoiceList.DataSource = New ArrayList
            dgInvoiceList.VirtualItemCount = 0
            dgInvoiceList.CurrentPageIndex = 0
            dgInvoiceList.DataBind()
        End If
        If dgInvoiceList.VirtualItemCount > 0 Then
            lblJumRecord.Text = "Jumlah record : " & dgInvoiceList.VirtualItemCount
        End If
    End Sub
#End Region

End Class