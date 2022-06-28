#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.SAP
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
#End Region

Public Class PopUpSPKMasterCountry
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtSalesmanCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoKTP As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgSalesmanHeader As System.Web.UI.WebControls.DataGrid

    Private _sessHelper As SessionHelper = New SessionHelper

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
    Private strDefDate As String = "1753/01/01"
    Dim arrSPKMasterList As New ArrayList
    Private SPKMasterFacade As New SPKMasterCountryCodePhoneFacade(User)
#End Region

    Private Sub ClearData()
        Me.txtSalesmanCode.Text = String.Empty

    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "CountryName"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPKMasterCountryCodePhone), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtNoKTP.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SPKMasterCountryCodePhone), "CountryCode", MatchType.[Partial], txtNoKTP.Text.Trim()))
        End If
        If txtSalesmanCode.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(SPKMasterCountryCodePhone), "CountryName", MatchType.[Partial], txtSalesmanCode.Text.Trim()))
        End If

        arrSPKMasterList = SPKMasterFacade.RetrieveByCriteria(criterias, indexPage + 1, dtgSalesmanHeader.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If arrSPKMasterList.Count > 0 Then
            dtgSalesmanHeader.CurrentPageIndex = indexPage
            dtgSalesmanHeader.DataSource = arrSPKMasterList
            dtgSalesmanHeader.VirtualItemCount = totalRow
            dtgSalesmanHeader.DataBind()
        Else
            MessageBox.Show(SR.DataNotFound("Kode Negara"))
        End If
        'Dim totalRow As Integer = 0
        'If (indexPage >= 0) And Not IsNothing(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite)) Then
        '    Dim arrSalesmanHeader As ArrayList = New SalesmanHeaderFacade(User).RetrieveByCriteria(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite), indexPage + 1, dtgSalesmanHeader.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        '    dtgSalesmanHeader.DataSource = arrSalesmanHeader
        '    dtgSalesmanHeader.VirtualItemCount = totalRow
        '    dtgSalesmanHeader.DataBind()
        'End If

        If dtgSalesmanHeader.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        'CreateCriteriaSearch()
        dtgSalesmanHeader.CurrentPageIndex = 0
        BindDataGrid(dtgSalesmanHeader.CurrentPageIndex)

        If dtgSalesmanHeader.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub CreateCriteriaSearch()

        'If Not IsNothing(Request.QueryString("Code")) Then
        '    Dim critTmp As CriteriaComposite
        '    ' mengambil data salesman yang sdh resign, keperluan reload data on SalesmanHeader form
        '    If Request.QueryString("Code") = "Resign" Then
        '        critTmp = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        critTmp.opAnd(New Criteria(GetType(SalesmanHeader), "ResignDate", MatchType.No, Date.Parse(strDefDate)))
        '        critTmp.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.Exact, CType(EnumSalesmanStatus.SalesmanStatus.Tidak_Aktif, String)))
        '        If Not IsNothing(Request.QueryString("Mode")) Then
        '            critTmp.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, Request.QueryString("Mode")))
        '        End If
        '        'If txtName.Text.Trim <> "" Then
        '        '    critTmp.opAnd(New Criteria(GetType(SalesmanHeader), "Name", MatchType.[Partial], txtName.Text.Trim))
        '        'End If
        '        If txtSalesmanCode.Text.Trim <> "" Then
        '            critTmp.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.[Partial], txtSalesmanCode.Text.Trim))
        '        End If
        '        If txtNoKTP.Text.Trim <> "" Then
        '            critTmp.opAnd(New Criteria(GetType(SalesmanHeader), "NoKTP.ProfileHeader.ID", MatchType.Exact, 29))
        '            critTmp.opAnd(New Criteria(GetType(SalesmanHeader), "NoKTP.ProfileGroup.ID", MatchType.Exact, 13))
        '            critTmp.opAnd(New Criteria(GetType(SalesmanHeader), "NoKTP.ProfileValue", MatchType.Partial, txtNoKTP.Text))
        '        End If

        '    End If
        '    _sessHelper.SetSession("Criteria", critTmp)
        '    Exit Sub
        'End If

        'Dim strSalesmanHeaderId As String
        'strSalesmanHeaderId = ""
        '' parameter dari ui salesman customer
        'If Not IsNothing(Request.QueryString("SAPNumber")) Then
        '    ' mengambil data salesman yg related dgn period ybs
        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(SAPRegister), "IsCancelled", MatchType.Exact, 0))
        '    criterias.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.SAPNumber", MatchType.Exact, Request.QueryString("SAPNumber")))
        '    Dim arrSAPRegister As ArrayList = New SAPRegisterFacade(User).Retrieve(criterias)

        '    If Not IsNothing(arrSAPRegister) Then
        '        If arrSAPRegister.Count > 0 Then
        '            For Each item As SAPRegister In arrSAPRegister
        '                If strSalesmanHeaderId = "" Then
        '                    strSalesmanHeaderId = item.SalesmanHeader.ID
        '                Else
        '                    strSalesmanHeaderId = strSalesmanHeaderId & ";" & item.SalesmanHeader.ID
        '                End If

        '            Next
        '        End If
        '    End If
        'End If

        '' menampilkan data salesman yang aktif & telah memiliki kode
        'Dim crits As New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'If strSalesmanHeaderId <> "" Then
        '    ' mengambil salesman yg register pd periode bersangkutan
        '    'Todo Inset
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "ID", MatchType.InSet, CommonFunction.GetStrValue(strSalesmanHeaderId, ";", ",")))
        'End If
        'If txtSalesmanCode.Text <> "" Then
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.[Partial], txtSalesmanCode.Text))
        'End If
        'If txtName.Text <> "" Then
        '    crits.opAnd(New Criteria(GetType(SalesmanHeader), "Name", MatchType.[Partial], txtName.Text))
        'End If

        'crits.opAnd(New Criteria(GetType(SalesmanHeader), "RegisterStatus", MatchType.Exact, CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)))

        '' parameter dari ui salesman header
        'If Not IsNothing(Request.QueryString("SSCode")) Then
        '    If Request.QueryString("SSCode") <> "" Then
        '        crits.opAnd(New Criteria(GetType(SalesmanHeader), "SalesmanCode", MatchType.No, Request.QueryString("SSCode")))
        '    End If
        'End If

        '' parameter dari ui EntrySalesVechilrDelivery
        'If Not IsNothing(Request.QueryString("FilterIndicator")) Then
        '    If Request.QueryString("FilterIndicator") <> String.Empty Then
        '        Select Case CType(Request.QueryString("FilterIndicator"), String)
        '            Case "Unit"
        '                crits.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.Unit, Byte)))
        '                'penambahan filter status = aktif untuk pengajuan spk
        '                'by anh 20130408 for angga
        '                crits.opAnd(New Criteria(GetType(SalesmanHeader), "Status", MatchType.Exact, CType(EnumSalesmanStatus.SalesmanStatus.Aktif, String)))
        '            Case "Sparepart"
        '                crits.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.Sparepart, Byte)))
        '            Case "Mekanik"
        '                crits.opAnd(New Criteria(GetType(SalesmanHeader), "SalesIndicator", MatchType.Exact, CType(EnumSalesmanUnit.SalesmanUnit.Mekanik, Byte)))
        '        End Select
        '    End If
        'End If

        'Dim objdealer As Dealer = New SessionHelper().GetSession("DEALER")
        'If Not objdealer.Title = EnumDealerTittle.DealerTittle.KTB Then
        '    If Val(Request.QueryString("IsGroupDealer")) = 0 Then
        '        crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.Exact, objdealer.ID))
        '    Else
        '        'For Faktur Get by dealer group
        '        Dim DealerGroupID As Integer = objdealer.DealerGroup.ID
        '        If DealerGroupID = 21 Then 'Single Dealer
        '            crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.Exact, objdealer.ID))
        '        Else
        '            crits.opAnd(New Criteria(GetType(SalesmanHeader), "Dealer.ID", MatchType.InSet, "(select ID from Dealer where DealerGroupID=" & DealerGroupID.ToString & ")"))
        '        End If
        '    End If
        'End If

        '_sessHelper.SetSession("Criteria", crits)

    End Sub

    Private Sub dtgSalesmanHeader_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSalesmanHeader.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanHeader As SPKMasterCountryCodePhone = e.Item.DataItem
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectSalesman"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
                '        Dim objJobPosition As JobPosition = objSalesmanHeader.JobPosition
                '        Dim lblJobPositionNew As Label = CType(e.Item.FindControl("lblJobPosition"), Label)
                '        Dim lblJobPositionDescNew As Label = CType(e.Item.FindControl("lblJobPositionDesc"), Label)
                '        Dim lblLevel As Label = CType(e.Item.FindControl("lblLevel"), Label)
                '        Dim lblDealerBranch As Label = CType(e.Item.FindControl("lblDealerBranch"), Label)


                '        If Not objSalesmanHeader.DealerBranch Is Nothing Then
                '            lblDealerBranch.Text = objSalesmanHeader.DealerBranch.DealerBranchCode & " / " & objSalesmanHeader.DealerBranch.Term1

                '        End If
                '        If Not objJobPosition Is Nothing Then
                '            lblJobPositionNew.Text = objSalesmanHeader.JobPosition.Code
                '            lblJobPositionDescNew.Text = objSalesmanHeader.JobPosition.Description
                '        Else
                '            lblJobPositionNew.Text = ""
                '            lblJobPositionDescNew.Text = ""
                '        End If
                '        Dim objSalesmanLevel As SalesmanLevel = objSalesmanHeader.SalesmanLevel
                '        If Not objSalesmanLevel Is Nothing Then
                '            lblLevel.Text = objSalesmanHeader.SalesmanLevel.Description
                '        Else
                '            lblLevel.Text = ""
                '        End If
            End If
        End If
    End Sub

    Private Sub dtgSalesmanHeader_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSalesmanHeader.PageIndexChanged
        dtgSalesmanHeader.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgSalesmanHeader.CurrentPageIndex)
    End Sub

    Private Sub dtgSalesmanHeader_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSalesmanHeader.SortCommand
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
        dtgSalesmanHeader.CurrentPageIndex = 0
        BindDataGrid(dtgSalesmanHeader.CurrentPageIndex)
    End Sub



End Class
