#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

Public Class ProposeAllocationList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents calRegAlocation As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlMaterialNumber As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlVechileTypeCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgAllocation As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblKategori As System.Web.UI.WebControls.Label
    Protected WithEvents ddlKategori As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlProductCategory As System.Web.UI.WebControls.DropDownList

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
    Private _arrPPQty As ArrayList
    Private _arrGroupPPQty As ArrayList
    Private sessionHelper As New SessionHelper
#End Region

#Region "Custom Method"


    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList
        objSSPO.Add(calRegAlocation.Value)
        objSSPO.Add(ddlCategory.SelectedIndex)
        objSSPO.Add(ddlVechileTypeCode.SelectedIndex)
        objSSPO.Add(ddlMaterialNumber.SelectedIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        objSSPO.Add(ddlYear.SelectedIndex)
        sessionHelper.SetSession("SESSIONPPQTY", objSSPO)
    End Sub

    Private Sub GetSessionCriteria()
        Dim objSSPO As ArrayList = sessionHelper.GetSession("SESSIONPPQTY")
        If Not objSSPO Is Nothing Then
            calRegAlocation.Value = objSSPO.Item(0)
            Me.BindCategory()
            ddlCategory.SelectedIndex = objSSPO.Item(1)
            Me.ddlCategory_SelectedIndexChanged(Nothing, Nothing)
            ddlVechileTypeCode.SelectedIndex = objSSPO.Item(2)
            Me.ddlVechileTypeCode_SelectedIndexChanged(Nothing, Nothing)
            ddlMaterialNumber.SelectedIndex = objSSPO.Item(3)
            ViewState("CurrentSortColumn") = objSSPO.Item(4)
            ViewState("CurrentSortDirect") = objSSPO.Item(5)
            Me.ddlYear.SelectedIndex = objSSPO.Item(6)
        End If
    End Sub


    'Private Sub BindToddlDealer()
    '    Dim arrayListDealer As ArrayList = New DealerFacade(User).RetrieveList
    '    Dim listitemBlank As New listItem("Silahkan Pilih", -1)

    '    ddlDealerCode.Items.Add(listitemBlank)
    '    For Each item As Dealer In arrayListDealer
    '        Dim listItem As New listItem(item.DealerCode, item.ID)
    '        ddlDealerCode.Items.Add(listItem)
    '    Next
    'End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "MaterialNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindCategory()
        Dim aCs As ArrayList = New CategoryFacade(User).RetrieveList()

        Dim PCID As Short = GetProductCategoryID()
        Me.ddlCategory.Items.Clear()
        Me.ddlCategory.Items.Add(New ListItem("Silahkan Pilih", -1))
        For Each oC As Category In aCs
            If PCID = 0 OrElse oC.ProductCategory.ID = PCID Then
                Me.ddlCategory.Items.Add(New ListItem(oC.CategoryCode, oC.ID))
            End If
        Next
    End Sub

    Private Function GetProductCategoryID() As Short
        Dim PCID As Short = CType(Me.ddlProductCategory.SelectedValue, Short)
        If PCID < 1 Then PCID = 0

        Return PCID
    End Function

    Private Sub BindToddlVechileType(ByVal CategoryID As Integer)
        Dim cVT As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim arrayListVechileType As ArrayList '= New VechileTypeFacade(User).RetrieveList
        Dim listitemBlank As New listItem("Silahkan Pilih", -1)

        cVT.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, CategoryID))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))

        arrayListVechileType = New VechileTypeFacade(User).Retrieve(cVT, sortColl)
        ddlVechileTypeCode.Items.Clear()
        ddlVechileTypeCode.Items.Add(listitemBlank)
        For Each item As VechileType In arrayListVechileType
            Dim listItem As New listItem(item.VechileTypeCode, item.ID)
            ddlVechileTypeCode.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindToddlVechileColor(ByVal id As Integer)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If id <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileColor), "VechileType.ID", MatchType.Exact, id))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(VechileColor), "MaterialNumber", Sort.SortDirection.ASC))

        Dim arrayListVechileColor As ArrayList = New VechileColorFacade(User).Retrieve(criterias, sortColl)
        Dim listitemBlank As New listItem("Silahkan Pilih", -1)

        ddlMaterialNumber.Items.Add(listitemBlank)
        For Each item As VechileColor In arrayListVechileColor
            Dim listItem As New listItem(item.MaterialNumber, item.ID)
            ddlMaterialNumber.Items.Add(listItem)
        Next
    End Sub

    Private Sub BindDataGrid()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PPQty), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Dim TanggalAwal As New DateTime(CInt(calRegAlocation.Value.Year), CInt(calRegAlocation.Value.Month), CInt(calRegAlocation.Value.Day), 0, 0, 0)

        'If txtKodeDealer.Text <> Nothing Then criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PPQty), "DealerCode", MatchType.Exact, txtKodeDealer.Text))
        'If txtKodeDealer.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PPQty), "DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))

        'If (ddlMaterialNumber.SelectedValue = -1) And (ddlVechileTypeCode.SelectedValue <> -1) Then
        '    'If ddlVechileTypeCode.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PPQty), "MaterialNumber", MatchType.StartsWith, ddlVechileTypeCode.SelectedItem.ToString))
        '    'If ddlMaterialNumber.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PPQty), "MaterialNumber", MatchType.Exact, ddlMaterialNumber.SelectedItem.ToString))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "MaterialNumber", MatchType.StartsWith, ddlVechileTypeCode.SelectedItem.ToString))
        'Else
        '    If ddlMaterialNumber.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "MaterialNumber", MatchType.Exact, ddlMaterialNumber.SelectedItem.ToString))
        'End If
        Dim Sql As String = " Select vc.MaterialNumber "
        Dim IsSelected As Boolean = False

        Dim PCID As Short = Me.GetProductCategoryID()
        Sql &= " from VechileColor vc inner join VechileType vt on vt.ID=vc.VechileTypeID inner join Category c on c.ID=vt.CategoryID "

        Sql &= " where vc.RowStatus=0 and vt.RowStatus=0 "
        If Me.ddlCategory.SelectedIndex > 0 Then
            IsSelected = True
            Sql &= " and vt.CategoryID=" & CType(Me.ddlCategory.SelectedValue, Integer).ToString & " "
        End If
        If Me.ddlVechileTypeCode.SelectedIndex > 0 Then
            IsSelected = True
            Sql &= " and vt.ID=" & CType(Me.ddlVechileTypeCode.SelectedValue, Integer).ToString & " "
        End If
        If Me.ddlMaterialNumber.SelectedIndex > 0 Then
            IsSelected = True
            Sql &= " and vc.MaterialNumber='" & Me.ddlMaterialNumber.SelectedItem.Text & "' "
        End If
        If PCID > 0 Then
            IsSelected = True
            Sql &= " and c.ProductCategoryID=" & PCID.ToString() & " "
        End If
        If IsSelected Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "MaterialNumber", MatchType.InSet, "(" & Sql & ")"))
        End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "ProductionYear", MatchType.Exact, CType(Me.ddlYear.SelectedValue, Integer)))

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeDate", CInt(calRegAlocation.Value.Day)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeMonth", CInt(calRegAlocation.Value.Month)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PPQty), "PeriodeYear", CInt(calRegAlocation.Value.Year)))

        '_arrPPQty = New PPQtyFacade(User).Retrieve(criterias)
        _arrPPQty = New PPQtyFacade(User).RetrieveList(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        If _arrPPQty.Count = 0 Then
            dgAllocation.DataBind()
            btnDownload.Visible = False
            MessageBox.Show("Data Tidak Ditemukan")
        Else
            dgAllocation.DataSource = GroupArrayList(_arrPPQty)
            dgAllocation.DataBind()

            btnDownload.Visible = True
        End If
    End Sub

    Private Function GroupArrayList(ByVal arrlist As ArrayList) As ArrayList
        _arrGroupPPQty = New ArrayList
        For Each item As PPQty In _arrPPQty
            If (_arrGroupPPQty.Count <> 0) Then
                Dim IsAlreadyExist As Boolean = False
                For Each item1 As PPQty In _arrGroupPPQty
                    If item1.MaterialNumber = item.MaterialNumber Then
                        IsAlreadyExist = True
                        Exit For
                    End If
                Next
                If Not IsAlreadyExist Then
                    _arrGroupPPQty.Add(item)
                End If
            Else
                _arrGroupPPQty.Add(item)
            End If
        Next
        Return _arrGroupPPQty
    End Function

    Private Function PopulatePPQty() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim oExArgs As New System.Collections.ArrayList
        Dim objPPQtyFacade As New PPQtyFacade(User)

        For Each oDataGridItem In dgAllocation.Items
            'If chkExport.Checked Then
            Dim _ppqty As New KTB.Dnet.Domain.PPQty
            _ppqty.ID = oDataGridItem.Cells.Item(0).Text
            'CType(oDataGridItem.FindControl("lblID"), Label).Text
            '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
            'If _ppqty.PKStatus = enumStatusPK.Status.Baru Then
            _ppqty = objPPQtyFacade.Retrieve(_ppqty.ID)
            '_pk.PKStatus = status.Validate
            oExArgs.Add(_ppqty)
            'End If
            'End If
        Next
        'status = Nothing
        Return oExArgs
    End Function

    Private Sub BindYear()
        Dim i As Integer

        Me.ddlYear.Items.Clear()
        For i = Now.Year - 2 To Now.Year
            Me.ddlYear.Items.Add(New ListItem(i.ToString, i))
        Next
        Me.ddlYear.SelectedValue = Now.Year
    End Sub
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
            InitiatePage()
            BindYear()
            Me.BindCategory()
            BindToddlVechileType(Me.ddlCategory.SelectedValue)
            BindToddlVechileColor(ddlVechileTypeCode.SelectedValue)
            btnDownload.Visible = False

            If Not SecurityProvider.Authorize(Context.User, SR.UnitUsulanSAPAlocationView_Privilege) Then
                Response.Redirect("../frmAccessDenied.aspx?modulName=Unit Usulan SAP")
            End If
            GetSessionCriteria()
            BindDataGrid()
        End If
        'lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        BindDataGrid()
    End Sub

    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim _fileHelper As New FileHelper
        Dim fileInfo1 As New FileInfo(Server.MapPath(""))
        Dim _fileNameInfo As FileInfo = _fileHelper.TransferPOProposetoSAP(PopulatePPQty, fileInfo1)
        Try
            Response.Redirect("../Downloadlocal.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("PPQtyDestFileDirectory").ToString & "\" & _fileNameInfo.Name)
        Catch ex As Exception
            MessageBox.Show(SR.DownloadFail(_fileNameInfo.Name))
        End Try
    End Sub

    Private Sub dgAllocation_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAllocation.ItemCommand
        If e.CommandName = "edit" Then
            Dim tanggal As Date = calRegAlocation.Value.ToString
            SetSessionCriteria()
            sessionHelper.SetSession("PrevPage", Request.Url.ToString())
            '  Dim RowValue As PPQty = CType(e.Item.DataItem, PPQty)

            Response.Redirect("ProposeAllocationListDetail.aspx?master=" & e.Item.Cells(4).Text & " &date=" & tanggal & " &code=" & e.Item.Cells(8).Text & "&modeltipewarna=" & e.Item.Cells(2).Text & " &tahunperakitan=" & e.Item.Cells(5).Text & " &stokATP=" & e.Item.Cells(6).Text)
        End If
    End Sub

    Private UnitATPViewPrivilege As Boolean
    Private Sub dgAllocation_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgAllocation.ItemDataBound
        Dim _vechilecolor As VechileColor
        If (e.Item.ItemIndex <> -1) Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

            If Not e.Item.DataItem Is Nothing Then
                If e.Item.ItemIndex = 0 Then
                    UnitATPViewPrivilege = SecurityProvider.Authorize(Context.User, SR.UnitUsulanSAPAlocationView_Privilege)
                End If
                'e.Item.DataItem.GetType().ToString()
                Dim RowValue As PPQty = CType(e.Item.DataItem, PPQty)
                e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dgAllocation.PageSize * dgAllocation.CurrentPageIndex)).ToString
                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                    e.Item.Cells(4).Text = RowValue.MaterialNumber
                    e.Item.Cells(8).Visible = UnitATPViewPrivilege
                    Dim lblProductCategory As Label = e.Item.FindControl("lblProductCategory")

                    lblProductCategory.Text = RowValue.VechileColor.VechileType.ProductCategory.Code
                    Try
                        _vechilecolor = New VechileColorFacade(User).RetrieveMaterial(RowValue.MaterialNumber)
                        e.Item.Cells(2).Text = _vechilecolor.MaterialDescription
                    Catch ex As Exception
                        e.Item.Cells(2).Text = ""
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub ddlVechileTypeCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlVechileTypeCode.SelectedIndexChanged
        ddlMaterialNumber.Items.Clear()
        BindToddlVechileColor(ddlVechileTypeCode.SelectedValue)
    End Sub

    Private Sub dgAllocation_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgAllocation.SortCommand
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

        dgAllocation.SelectedIndex = -1
        dgAllocation.CurrentPageIndex = 0
        BindDataGrid()
        'BindDataGrid(dtgPIUser.CurrentPageIndex)
        'ClearData()
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        Me.BindToddlVechileType(Me.ddlCategory.SelectedValue)
    End Sub

#End Region

    Private Sub ddlProductCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProductCategory.SelectedIndexChanged
        Me.BindCategory()
    End Sub
End Class