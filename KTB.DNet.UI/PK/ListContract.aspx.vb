#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports System.IO
#End Region

Public Class ListContract
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKondisi As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtContractNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgContract As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlContractPeriod As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPK As System.Web.UI.WebControls.Label
    Protected WithEvents txtNomorPK As System.Web.UI.WebControls.TextBox
    Protected WithEvents valPeriodeMO As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents lblspaDate As System.Web.UI.WebControls.Label
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents lblspaNumber As System.Web.UI.WebControls.Label
    Protected WithEvents btnLock As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatalKunci As System.Web.UI.WebControls.Button
    Protected WithEvents cboxDikunci As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents lblQuantity As System.Web.UI.WebControls.Label
    Protected WithEvents Label20 As System.Web.UI.WebControls.Label
    Protected WithEvents Label21 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList

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
    'Private total As Double = 0
    Private _ArlContract As ArrayList
    Private objDealer As Dealer
    Private sessionHelper As New sessionHelper
    Private MoDetailViewPrivilege As Boolean
#End Region

#Region "Custom Method"

    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList
        objSSPO.Add(txtDealerCode.Text.Trim)
        objSSPO.Add(ddlOrderType.SelectedIndex)
        objSSPO.Add(ddlContractPeriod.SelectedIndex)
        objSSPO.Add(ddlKondisi.SelectedIndex)
        objSSPO.Add(txtContractNumber.Text)
        objSSPO.Add(txtNomorPK.Text)
        objSSPO.Add(ddlCategory.SelectedIndex)
        objSSPO.Add(Me.ddlSubCategory.SelectedIndex)
        objSSPO.Add(cboxDikunci.Checked)
        objSSPO.Add(dgContract.CurrentPageIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessionHelper.SetSession("SESSIONLISTCONTRACT", objSSPO)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objSSPO As ArrayList = sessionHelper.GetSession("SESSIONLISTCONTRACT")
        If Not objSSPO Is Nothing Then
            txtDealerCode.Text = objSSPO.Item(0)
            ddlOrderType.SelectedIndex = objSSPO.Item(1)
            ddlContractPeriod.SelectedIndex = objSSPO.Item(2)
            ddlKondisi.SelectedIndex = objSSPO.Item(3)
            txtContractNumber.Text = objSSPO.Item(4)
            txtNomorPK.Text = objSSPO.Item(5)
            ddlCategory.SelectedIndex = objSSPO.Item(6)
            Me.ddlCategory_SelectedIndexChanged(Nothing, Nothing)
            Me.ddlSubCategory.SelectedIndex = objSSPO.Item(7)
            cboxDikunci.Checked = objSSPO.Item(8)
            dgContract.CurrentPageIndex = objSSPO.Item(9)
            ViewState("CurrentSortColumn") = objSSPO.Item(10)
            ViewState("CurrentSortDirect") = objSSPO.Item(11)
            Return True
        End If
        Return False
    End Function

    Private Sub BindToDropDownList()
        Dim listitemBlank As listItem

        '--DropDownList Periode MO 
        listitemBlank = New listItem("Silahkan Pilih", "")
        ddlContractPeriod.Items.Add(listitemBlank)
        For Each item As listItem In LookUp.ArraylistMonth(True, 12, 1, DateTime.Now)
            ddlContractPeriod.Items.Add(item)
        Next
        ddlContractPeriod.SelectedValue = Format(DateTime.Now, "MMM yyyy").ToString

        '--DropDownList Jenis Pesanan
        If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) Then
            Dim itemBlank As listItem = New listItem("Silahkan Pilih", -1)
            ddlOrderType.Items.Add(itemBlank)
        End If

        For Each item As listItem In LookUp.ArrayJenisPesanan
            If item.Text = "Bulanan" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKindBulanan_Privilege) Then
                    ddlOrderType.Items.Add(item)
                End If
            ElseIf item.Text = "Tambahan" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKindTambahan_Privilege) Then
                    ddlOrderType.Items.Add(item)
                End If
            End If
        Next

        '--DropDownList Kondisi Pesanan
        If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) Then
            listitemBlank = New listItem("Silahkan Pilih", -1)
            ddlKondisi.Items.Add(listitemBlank)
        End If

        For Each item As listItem In LookUp.ArrayPurpose
            If item.Text = "Khusus" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKondisiKhusus_Privilege) Then
                    ddlKondisi.Items.Add(item)
                End If
            ElseIf item.Text = "Biasa" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKondisiBiasa_Privilege) Then
                    ddlKondisi.Items.Add(item)
                End If
            End If
        Next

        '--DropDownList Kategori
        Dim arrayListCategory As ArrayList = New PKHeaderFacade(User).RetrieveListCategory

        If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
            Dim listitemBlank1 As New listItem("Silahkan Pilih", -1)
            ddlCategory.Items.Add(listitemBlank1)
        End If

        For Each item As Category In arrayListCategory
            Dim listItem As New listItem(item.CategoryCode, item.ID)
            If item.CategoryCode = "PC" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    ddlCategory.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "LCV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    ddlCategory.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "CV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                    ddlCategory.Items.Add(listItem)
                End If
            End If
        Next

    End Sub

    Private Sub BindDataToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        If txtDealerCode.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        If txtNomorPK.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "DealerPKNumber", MatchType.Exact, txtNomorPK.Text))
        If ddlOrderType.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractType", MatchType.Exact, ddlOrderType.SelectedValue))

        If cboxDikunci.Checked Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "Status", MatchType.Exact, "LOCK"))
        If ddlContractPeriod.SelectedItem.Text <> "Silahkan Pilih" Then
            Dim tgl As DateTime = System.Convert.ToDateTime(ddlContractPeriod.SelectedItem.ToString)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractPeriodMonth", MatchType.Exact, CType(tgl.Month, Integer)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractPeriodYear", MatchType.Exact, CType(tgl.Year, Integer)))
        End If

        If txtContractNumber.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "ContractNumber", MatchType.Exact, txtContractNumber.Text))
        If ddlCategory.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))

        If ddlCategory.SelectedValue <> -1 And ddlSubCategory.SelectedValue <> "-1" Then
            'Dim Sql As String = ""
            'Dim oEVSC As EnumVehicleSubCategory = New EnumVehicleSubCategory
            'Dim sVals As String = oEVSC.GetSQLValue(ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue)
            ''Dim sVals As String = EnumVehicleSubCategory.GetSQLValue(ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue)

            'Sql &= " select count(*) from ContractDetail cd, VechileColor vc, VechileType vt "
            'Sql &= " where cd.ContractHeaderID = ContractHeader.ID And cd.VehicleColorID=vc.ID and vc.VechileTypeID=vt.ID "
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
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "RowStatus", MatchType.No, "(" & Sql & ")"))

            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
            Dim strSql2 As String = "select distinct a.ContractHeaderID "
            strSql2 += " from ContractDetail a join VechileColor b on a.VehicleColorID = b.ID and b.RowStatus = 0 "
            strSql2 += " join VechileType c on b.VechileTypeID = c.ID and c.RowStatus = 0 "
            strSql2 += " join VechileModel d on c.ModelID = d.ID and d.RowStatus = 0 "
            strSql2 += " where a.RowStatus = 0 and d.ID in (" & strSql & ")"
            criterias.opAnd(New Criteria(GetType(ContractHeader), "ID", MatchType.InSet, "(" & strSql2 & ")"))

        End If
        If ddlKondisi.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "Purpose", MatchType.Exact, ddlKondisi.SelectedValue))

        'Dim _ListContract As ArrayList = New ContractHeaderFacade(User).Retrieve(criterias)
        Dim _ListContract As ArrayList = New ContractHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dgContract.PageSize, _
               total, CType(ViewState("CurrentSortColumn"), String), _
               CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        _ArlContract = New ContractHeaderFacade(User).Retrieve(criterias)
        'Dim _ListContractDetails As ArrayList = New ContractDetailFacade(User).RetrieveList
        'Dim gridUtility As New BinderGrid
        dgContract.DataSource = _ListContract
        dgContract.VirtualItemCount = total
        If _ArlContract.Count > 0 Then
            Try
                dgContract.DataBind()
            Catch ex As Exception
                Dim errors As String = ex.Message.ToString()
            End Try

            If _ListContract.Count > 0 Then
                'Dim dv As DataView = gridUtility.RetrieveHierarkiDataView(_ListContract, _ListContractDetails, "ContractHeader", "ContractDetail", "ID", "ContractHeaderID")
                'HGContract.DataSource = dv
                'HGContract.DataBind()
                'HGContract.RowExpanded(0) = False
                btnLock.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarMOLock_Privilege)
                btnBatalKunci.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarMOLock_Privilege)
                btnDelete.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarMODelete_Privilege)

            Else
                MessageBox.Show(SR.DataNotFound("Kontrak"))
            End If

        Else
            dgContract.DataBind()
            MessageBox.Show("Data Tidak Ditemukan")
        End If

    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, ByVal ContractID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "ContractHeader", MatchType.Exact, ContractID))
        Return criterias
    End Function


    Private Function CreateCriteriaForCheckRecordPO(ByVal DomainType As Type, ByVal ContractID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "ContractHeader.ID", MatchType.Exact, ContractID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub PopulateDeleteConfirm()
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objContractHeaderFacade As New ContractHeaderFacade(User)
        Dim succes As Boolean = False
        Dim Deleted As Boolean = False

        For Each oDataGridItem In dgContract.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _co As New KTB.Dnet.Domain.ContractHeader
                _co.ID = oDataGridItem.Cells(1).Text

                Dim sttausPO As String = ""

                sttausPO = CInt(enumStatusPO.Status.Batal).ToString()
                sttausPO = sttausPO & "," & CInt(enumStatusPO.Status.Ditolak).ToString()
                sttausPO = sttausPO & "," & CInt(enumStatusPO.Status.Tidak_Setuju).ToString()
                sttausPO = sttausPO & "," & CInt(enumStatusPO.Status.DiBlok).ToString()

                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "ContractHeader.ID", MatchType.Exact, _co.ID.ToString()))
                crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.POHeader), "Status", MatchType.NotInSet, sttausPO))
                Dim agg As Aggregate = New Aggregate(GetType(POHeader), "ID", AggregateType.Count)

                Dim IsPoExist As Boolean = False
                IsPoExist = (New HelperFacade(User, GetType(POHeader)).IsRecordExist(crit, agg))
                 
                If Not IsPoExist Then
                    Deleted = True
                    Try
                        Dim ocDetails As New ArrayList
                        _co = objContractHeaderFacade.Retrieve(_co.ID)
                        _co.RowStatus = DBRowStatus.Deleted

                        For Each objOCDetail As ContractDetail In _co.ContractDetails
                            objOCDetail.RowStatus = DBRowStatus.Deleted 
                            ocDetails.Add(objOCDetail)
                        Next

                        objContractHeaderFacade.UpdateTransaction(_co, ocDetails)
                        succes = True

                    Catch ex As Exception
                        succes = False
                    End Try

                    'oExArgs.Add(_co)
                Else
                    succes = False

                    BindGrid()
                    MessageBox.Show("Sudah ada pengajuan PO atas nomor O/C tersebut")
                    Return
                End If
            End If
        Next
        BindGrid()
        If succes Then 
            MessageBox.Show("OC berhasil dihapus, pastikan O/C di SAP juga sudah terhapus")
        End If

        If Deleted = False Then
            MessageBox.Show("Tidak ada OC yang dihapus")

        End If

    End Sub

    Private Sub PopulateLock()
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objContractHeaderFacade As New ContractHeaderFacade(User)

        For Each oDataGridItem In dgContract.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _co As New KTB.Dnet.Domain.ContractHeader
                _co.ID = oDataGridItem.Cells(1).Text

                'Dim criterias As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.POHeader), "ContractHeader.ID", MatchType.Exact, CType(tgl.Month, Integer)))
                '_PoHeader = New POHeaderFacade(User).RetrieveIDContract(_co.ID)
                '_co.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                'If Not (New HelperFacade(User, GetType(POHeader)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(POHeader), _co.ID), CreateAggreateForCheckRecord(GetType(POHeader)))) Then
                _co = objContractHeaderFacade.Retrieve(_co.ID)
                _co.Status = "LOCK"
                objContractHeaderFacade.Update(_co)
                'oExArgs.Add(_co)
                'End If
            End If
        Next

    End Sub

    Private Sub PopulateUnLock()
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objContractHeaderFacade As New ContractHeaderFacade(User)

        For Each oDataGridItem In dgContract.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _co As New KTB.Dnet.Domain.ContractHeader
                _co.ID = oDataGridItem.Cells(1).Text

                'Dim criterias As New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.POHeader), "ContractHeader.ID", MatchType.Exact, CType(tgl.Month, Integer)))
                '_PoHeader = New POHeaderFacade(User).RetrieveIDContract(_co.ID)
                '_co.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                'If Not (New HelperFacade(User, GetType(POHeader)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(POHeader), _co.ID), CreateAggreateForCheckRecord(GetType(POHeader)))) Then
                _co = objContractHeaderFacade.Retrieve(_co.ID)
                _co.Status = ""
                objContractHeaderFacade.Update(_co)
                'oExArgs.Add(_co)
                'End If
            End If
        Next

    End Sub

    Private Sub retriveDealer()
        objDealer = CType(sessionHelper.GetSession("DEALER"), Dealer)

        If Not objDealer Is Nothing Then
            lblspaNumber.Text = objDealer.SPANumber

            If objDealer.SPADate = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                lblspaDate.Text = Nothing
            Else
                lblspaDate.Text = Format(objDealer.SPADate, "dd MMMMMMMMMMMMMMM yyyy")
            End If
        End If

    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindToDropDownList()
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
            ' .BindVehicleSubCategoryToDDL(ddlSubCategory, ddlCategory.SelectedItem.Text)

            InitiatePage()
            retriveDealer()
            If ddlCategory.Items.Count = 0 OrElse ddlKondisi.Items.Count = 0 OrElse ddlOrderType.Items.Count = 0 Then
                btnCari.Enabled = False
            End If
            If GetSessionCriteria() Then
                BindGrid()
                TotalAmount()
            End If
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnDelete.Attributes.Add("Onclick", "return confirm('Anda yakin melakukan Hapus ??');")
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarMOView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar M/O")
        End If
        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        Label4.Visible = isPriceVisible
        Label20.Visible = isPriceVisible
        lblTotal.Visible = isPriceVisible
        Label21.Visible = isPriceVisible
        dgContract.Columns(12).Visible = isPriceVisible

    End Sub

    Sub dgContract_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dgContract.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Private Sub BindGrid()
        BindDataToGrid(dgContract.CurrentPageIndex)
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "ContractNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub dgContract_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgContract.SortCommand
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

        dgContract.SelectedIndex = -1
        dgContract.CurrentPageIndex = 0
        BindDataToGrid(dgContract.CurrentPageIndex)

    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dgContract.CurrentPageIndex = 0
        BindGrid()
        TotalAmount()
    End Sub

    Private Sub dgContract_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgContract.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

            If Not e.Item.DataItem Is Nothing Then
                'e.Item.DataItem.GetType().ToString()
                Dim RowValue As ContractHeader = CType(e.Item.DataItem, ContractHeader)
                If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                    e.Item.Cells(2).Text = (e.Item.ItemIndex + 1 + (dgContract.PageSize * dgContract.CurrentPageIndex)).ToString
                    If e.Item.ItemIndex = 0 Then
                        MoDetailViewPrivilege = SecurityProvider.Authorize(Context.User, SR.DaftarMOViewDetail_Privilege)
                    End If
                    Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
                    Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
                    Dim lblNoRegPK As Label = CType(e.Item.FindControl("lblNoRegPK"), Label)
                    Dim lblNomorPK As Label = CType(e.Item.FindControl("lblNomorPKs"), Label)
                    Dim lblOrderType As Label = CType(e.Item.FindControl("lblOrderType"), Label)
                    Dim lblProductionYear As Label = CType(e.Item.FindControl("lblProductionYear"), Label)
                    Dim lblProjectName As Label = CType(e.Item.FindControl("lblProjectName"), Label)

                    If Not IsNothing(RowValue.Dealer.DealerCode) Then
                        lblDealer.Text = RowValue.Dealer.DealerCode
                        lblDealer.ToolTip = RowValue.Dealer.SearchTerm1
                    Else
                        lblDealer.Text = Nothing
                    End If

                    If Not IsNothing(RowValue.Category.CategoryCode) Then
                        lblCategory.Text = RowValue.Category.CategoryCode
                    Else
                        lblCategory.Text = Nothing
                    End If

                    If RowValue.Status = "LOCK" Then
                        Dim lblStatus As Label = e.Item.FindControl("lblStatusContract")
                        lblStatus.Text = "<img src=""../images/unlock.gif"" border=""0"" alt=""Dikunci"">"
                        'e.Item.Cells(13).Text = "Dikunci"
                    End If

                    Dim _LblFileName As Label = e.Item.FindControl("lblString")
                    If RowValue.ContractNumber.ToString() <> String.Empty Then
                        _LblFileName.Text = "mofile_" & RowValue.ContractNumber.ToString() & ".pdf"
                    End If

                    lblProductionYear.Text = RowValue.ProductionYear
                    lblProjectName.Text = RowValue.ProjectName
                    lblNoRegPK.Text = RowValue.PKNumber
                    lblNomorPK.Text = RowValue.DealerPKNumber
                    Dim EnumOrderType As LookUp.EnumJenisPesanan = RowValue.ContractType
                    lblOrderType.Text = EnumOrderType.ToString
                    Dim lbtnDetail As LinkButton = e.Item.FindControl("lbtnDetail")
                    lbtnDetail.Visible = MoDetailViewPrivilege

                    Dim lblFlow As Label = CType(e.Item.FindControl("lblFlow"), Label)
                    lblFlow.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDocumentFlow.aspx?flow=MO_" & RowValue.ContractNumber, "", 500, 450, "ViewDailyPKFlow")

                End If

            End If
        End If
    End Sub

    Private Sub TotalAmount()
        Dim tot As Double = 0
        Dim Qty As Double = 0

        If ddlSubCategory.SelectedIndex = 0 Then
            For Each item As ContractHeader In _ArlContract
                tot = tot + item.TotalContract
                Qty = Qty + item.TotalQuantity
            Next
        Else
            'Dim oEVSC As EnumVehicleSubCategory = New EnumVehicleSubCategory
            'Dim sVals As String = oEVSC.GetSQLValue(ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue)

            For Each item As ContractHeader In _ArlContract
                For Each oCD As ContractDetail In item.ContractDetails
                    'For Each val As String In sVals.Split(";")
                    '    Dim ex As New System.Text.RegularExpressions.Regex("%")
                    '    Dim m As System.Text.RegularExpressions.MatchCollection
                    '    m = ex.Matches(val)
                    '    Dim Str As String
                    '    If m.Count = 1 Then
                    '        If val.EndsWith("%") Then
                    '            Str = val.Replace("%", "").ToLower()
                    '            If oCD.VechileColor.VechileType.Description.ToLower().IndexOf(Str) >= 0 AndAlso oCD.VechileColor.VechileType.Description.ToLower().StartsWith(Str) Then
                    '                Qty += oCD.TargetQty
                    '                tot += (oCD.Amount + (oCD.PPh22 * oCD.ContractHeader.FreePPh22Indicator)) * oCD.TargetQty

                    '            End If
                    '        ElseIf val.StartsWith("%") Then
                    '            Str = val.Replace("%", "").ToLower()
                    '            If oCD.VechileColor.VechileType.Description.ToLower().IndexOf(Str) >= 0 AndAlso oCD.VechileColor.VechileType.Description.ToLower().EndsWith(Str) Then
                    '                Qty += oCD.TargetQty
                    '                tot += (oCD.Amount + (oCD.PPh22 * oCD.ContractHeader.FreePPh22Indicator)) * oCD.TargetQty

                    '            End If
                    '        End If
                    '    Else
                    '        Str = val.Replace("%", "").ToLower()
                    '        If oCD.VechileColor.VechileType.Description.ToLower().IndexOf(Str) >= 0 Then
                    '            Qty += oCD.TargetQty
                    '            tot += (oCD.Amount + (oCD.PPh22 * oCD.ContractHeader.FreePPh22Indicator)) * oCD.TargetQty

                    '        End If
                    '    End If


                    'Next

                    Dim criterias As New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "SubCategoryVehicle.ID", MatchType.Exact, ddlSubCategory.SelectedValue))
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, oCD.VechileColor.VechileType.VechileModel.ID))
                    Dim _ArlSubCategoryVehicleToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
                    If Not IsNothing(_ArlSubCategoryVehicleToModel) AndAlso _ArlSubCategoryVehicleToModel.Count > 0 Then
                        Qty += oCD.TargetQty
                        tot += (oCD.Amount + (oCD.PPh22 * oCD.ContractHeader.FreePPh22Indicator)) * oCD.TargetQty
                    End If
                Next
            Next
        End If
        lblTotal.Text = FormatNumber(tot, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblQuantity.Text = FormatNumber(Qty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " Unit"
    End Sub

    Private Sub dgContract_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgContract.ItemCommand
        Select Case (e.CommandName)
            Case "detail"
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("ListContractDetail.aspx?id=" & e.Item.Cells(1).Text)
            Case "Download"
                Dim _date As String = Now.Day & Now.Month & Now.Year
                Dim _LblFileName As Label = e.Item.FindControl("lblString")
                Dim fileInfo As New fileInfo(KTB.DNet.Lib.WebConfig.GetValue("MOFileDirectory").ToString & "\" & _LblFileName.Text)
                'Dim fileInfo1 As New fileInfo(Server.MapPath(""))
                Dim fileInfo1 As New fileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
                Dim destFilePath As String = fileInfo1.Directory.FullName & "\" & KTB.DNet.Lib.WebConfig.GetValue("MODestFileDirectory").ToString & "\" & _date & "\" & _LblFileName.Text
                Dim _destFileInfo As fileInfo = New fileInfo(destFilePath)
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.202
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Try
                    success = imp.Start()
                    If success Then
                        If (fileInfo.Exists) Then
                            CreateFolder(_destFileInfo.Directory.ToString)
                            fileInfo.CopyTo(destFilePath, True)
                            Response.Redirect("../Download.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("MODestFileDirectory").ToString & "\" & _date & "\" & _LblFileName.Text)
                        Else
                            MessageBox.Show(SR.DownloadFail(_LblFileName.Text))
                        End If
                    Else
                        MessageBox.Show(SR.DownloadFail(_LblFileName.Text))
                    End If
                Catch ex As Exception
                    MessageBox.Show(SR.DownloadFail(_LblFileName.Text))
                Finally
                    imp.StopImpersonate()
                    imp = Nothing
                End Try
        End Select

    End Sub

    Private Sub CreateFolder(ByVal folder As String)
        Dim dirInfo As DirectoryInfo = New DirectoryInfo(folder)
        If Not dirInfo.Exists Then
            dirInfo.Create()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        PopulateDeleteConfirm()

    End Sub

    Private Sub ddlContractPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlContractPeriod.SelectedIndexChanged
        If ddlContractPeriod.SelectedIndex = 0 Then
            valPeriodeMO.Enabled = True
        Else
            valPeriodeMO.Enabled = False
        End If
    End Sub

    Private Sub btnLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLock.Click
        PopulateLock()
        BindGrid()
    End Sub

    Private Sub btnBatalKunci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatalKunci.Click
        PopulateUnLock()
        BindGrid()
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub

#End Region

    
End Class