#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.UI.Helper
Imports System.Collections.Generic
Imports System.Linq
Imports System.IO
Imports System.Text
Imports Newtonsoft.Json
Imports KTB.DNet.SAP
Imports KTB.DNet.Parser.Domain
#End Region

Public Class ResponsePKOrder
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents Label20 As System.Web.UI.WebControls.Label
    Protected WithEvents Label21 As System.Web.UI.WebControls.Label
    Protected WithEvents txtPKNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPurpose As System.Web.UI.WebControls.DropDownList
    Protected WithEvents tblOperator As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents ddlRencanaPenebusan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents dgListPK As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lboxStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents btnProses As System.Web.UI.WebControls.Button
    Protected WithEvents ddlProses As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents lblQuantity As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnTransferData As System.Web.UI.WebControls.Button
    Protected WithEvents btnTransferUlang As System.Web.UI.WebControls.Button


    Protected WithEvents txtDealerBranchCode As TextBox
    Protected WithEvents lblDealerBranch As Label
    Protected WithEvents lblPopUpDealerBranch As Label
    Protected WithEvents txtBranchName As TextBox
    Protected WithEvents hdnTitle As HiddenField

    Protected WithEvents txtPass As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUser As System.Web.UI.WebControls.TextBox

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
    Private arlPK As ArrayList
    Private sessionHelper As New sessionHelper
    Private objDealer As Dealer
    Private setuju_rilis_respon_pk_privilege As Boolean = SecurityProvider.Authorize(Context.User, SR.Setuju_rilis_respon_pk_privilege)
#End Region

#Region "Custom Method"

    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList
        objSSPO.Add(txtDealerCode.Text)
        objSSPO.Add(txtPKNumber.Text.Trim)
        objSSPO.Add(ddlOrderType.SelectedIndex)
        objSSPO.Add(GetSelectedItem(lboxStatus))
        objSSPO.Add(ddlRencanaPenebusan.SelectedIndex)
        objSSPO.Add(ddlCategory.SelectedIndex)
        objSSPO.Add(ddlSubCategory.SelectedIndex)
        objSSPO.Add(ddlPurpose.SelectedIndex)
        objSSPO.Add(dgListPK.CurrentPageIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        Session("SESSIONRESPONSEPKORDER") = objSSPO
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Try
            Dim objSSPO As ArrayList = Session("SESSIONRESPONSEPKORDER")
            If Not objSSPO Is Nothing AndAlso btnCari.Enabled Then
                txtDealerCode.Text = objSSPO.Item(0)
                txtPKNumber.Text = objSSPO.Item(1)
                ddlOrderType.ClearSelection()
                'ddlOrderType.SelectedValue = Nothing

                ddlOrderType.SelectedIndex = objSSPO.Item(2)
                Dim str() As String = objSSPO.Item(3).ToString().Split(",")
                lboxStatus.ClearSelection()
                For Each item As ListItem In lboxStatus.Items
                    For i As Integer = 0 To str.Length - 1
                        If item.Value.ToString = str(i).ToString Then
                            item.Selected = True
                            Exit For
                        End If
                    Next
                Next
                ddlRencanaPenebusan.ClearSelection()
                ddlRencanaPenebusan.SelectedIndex = objSSPO.Item(4)
                ddlCategory.ClearSelection()
                ddlCategory.SelectedIndex = objSSPO.Item(5)
                CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
                ' Me.ddlCategory_SelectedIndexChanged(Nothing, Nothing)
                ddlSubCategory.ClearSelection()
                If ddlCategory.SelectedIndex > 0 Then
                    ddlSubCategory.SelectedIndex = objSSPO.Item(6) '
                End If
                ddlPurpose.ClearSelection()
                ddlPurpose.SelectedIndex = objSSPO.Item(7)
                dgListPK.CurrentPageIndex = objSSPO.Item(8)
                ViewState("CurrentSortColumn") = objSSPO.Item(9)
                ViewState("CurrentSortDirect") = objSSPO.Item(10)
                Return True
            End If
        Catch ex As Exception

        End Try

        Return False
    End Function

    Private Function GetSelectedItem(ByVal listboxStatus As ListBox) As String
        Dim _strStatus As String = String.Empty
        For Each item As ListItem In listboxStatus.Items
            If item.Selected Then
                If _strStatus = String.Empty Then
                    _strStatus = item.Value
                Else
                    _strStatus = _strStatus & "," & item.Value
                End If
            End If
        Next
        Return _strStatus
    End Function

    Private Sub RecordStatusChangeHistory(ByVal arrListPK As ArrayList, ByVal oldStatus As Integer)
        For Each item As PKHeader In arrListPK
            Dim objStatusChangeHistoryFacade As New StatusChangeHistoryFacade(User)
            objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Pesanan_Kendaraan), item.PKNumber, oldStatus, item.PKStatus)
        Next
    End Sub
    Private Sub BindGrid()
        BindDataGrid(dgListPK.CurrentPageIndex)
        TotalAmount()
    End Sub

    Private Sub BindToddlCategory()
        Dim arrayListCategory As ArrayList = New PKHeaderFacade(User).RetrieveListCategory

        ddlCategory.Items.Clear()
        If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
            Dim listitemBlank As New listItem("Pilih", -1)
            ddlCategory.Items.Add(listitemBlank)
        End If

        For Each item As Category In arrayListCategory
            Dim listItem As New listItem(item.CategoryCode, item.ID)
            If item.CategoryCode = "PC" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) Then
                    ddlCategory.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "LCV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) Then
                    ddlCategory.Items.Add(listItem)
                End If
            ElseIf item.CategoryCode = "CV" Then
                If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) Then
                    ddlCategory.Items.Add(listItem)
                End If
            End If
        Next
    End Sub
    'Private Sub BindToddlCategory()
    '    Dim arrayListCategory As ArrayList = New PKHeaderFacade(User).RetrieveListCategory
    '    Dim Item As ListItem
    '    Item = New ListItem("Silahkan Pilih", -1)
    '    ddlCategory.Items.Add(Item)
    '    For Each _item As Category In arrayListCategory
    '        Dim _listItem As New ListItem(_item.CategoryCode, _item.ID)
    '        ddlCategory.Items.Add(_listItem)
    '    Next
    'End Sub

    Private Sub BindToddl()
        Dim listitemBlank As ListItem
        listitemBlank = New ListItem("Silahkan Pilih", -1)
        Try
            lboxStatus.ClearSelection()
        Catch ex As Exception

        End Try

        ' lboxStatus.SelectedIndex = -1

        '--DropDownList Rencana Penebusan
        '--CR 11/03/2008: data yg ditampilkan menjadi n-6 sampai n+6
        ddlRencanaPenebusan.Items.Clear()
        For Each item As ListItem In LookUp.ArraylistMonth(True, 6, 6, DateTime.Now)
            ddlRencanaPenebusan.Items.Add(item)
        Next
        ddlRencanaPenebusan.ClearSelection()
        ddlRencanaPenebusan.SelectedValue = Format(DateTime.Now, "MMM yyyy").ToString


        '--DropDownList Kondisi Pesanan

        ddlPurpose.Items.Clear()
        If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) Then
            listitemBlank = New ListItem("Silahkan Pilih", -1)
            ddlPurpose.Items.Add(listitemBlank)
        End If
        For Each item As ListItem In LookUp.ArrayPurpose
            If item.Text = "Khusus" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKondisiKhusus_Privilege) Then
                    ddlPurpose.Items.Add(item)
                End If
            ElseIf item.Text = "Biasa" Then
                If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKondisiBiasa_Privilege) Then
                    ddlPurpose.Items.Add(item)
                End If
            End If
        Next
        ddlPurpose.ClearSelection()

        '--DropDownList Jenis Pesanan
        ddlOrderType.Items.Clear()
        If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) Then
            Dim itemBlank As ListItem = New ListItem("Silahkan Pilih", -1)
            ddlOrderType.Items.Add(itemBlank)
        End If

        For Each item As ListItem In LookUp.ArrayJenisPesanan
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

        '--DropDownList Proses
        If Not SecurityProvider.Authorize(Context.User, SR.ResponsePKStatusAll_Privilege) Then
            If Not SecurityProvider.Authorize(Context.User, SR.ResponsePKStatusBlock_Privilege) Then
                ddlProses.Items.RemoveAt(8)
                ddlProses.Items.RemoveAt(7)
            End If
            If Not SecurityProvider.Authorize(context.User, SR.ResponsePKStatusReject_Privilege) Then
                ddlProses.Items.RemoveAt(6)
                ddlProses.Items.RemoveAt(5)
            End If

            If Not SecurityProvider.Authorize(Context.User, SR.ResponsePKStatusRelease_Privilege) Then
                ddlProses.Items.RemoveAt(4)
                ddlProses.Items.RemoveAt(3)
            End If
            If Not SecurityProvider.Authorize(Context.User, SR.ResponsePKStatusConfirm_Privilege) Then
                ddlProses.Items.RemoveAt(2)
                ddlProses.Items.RemoveAt(1)
            End If
        End If
    End Sub

    Private Sub BindDataGrid(ByVal currentPageIndex As Integer)
        Try
            Dim total As Integer = 0
            Dim objPK As ArrayList

            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            objDealer = Session("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            End If
            If txtDealerCode.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))

            If ddlOrderType.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))

            If lboxStatus.SelectedIndex <> -1 Then
                Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKStatus", MatchType.InSet, "(" & SelectedStatus & ")"))
            Else
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Validasi, Integer) & "," & CType(enumStatusPK.Status.Konfirmasi, Integer) & "," & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPK.Status.Ditolak, Integer) & "," & CType(enumStatusPK.Status.Setuju, Short) & "," & CType(enumStatusPK.Status.DiBlok, Integer) & "," & (CType(enumStatusPK.Status.Baru, Integer)) & "," & (CType(enumStatusPK.Status.Tunggu_Diskon, Integer)) & ")"))
            End If
            'If txtPKNumber.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "DealerPKNumber", MatchType.Exact, txtPKNumber.Text))
            If txtPKNumber.Text.Trim <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKNumber", MatchType.Exact, txtPKNumber.Text))


            If txtDealerBranchCode.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtDealerBranchCode.Text.Replace(";", "','") & "')"))
            End If


            If ddlCategory.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
            'Start  :Not uploaded Yet
            If ddlCategory.SelectedValue <> -1 And ddlSubCategory.SelectedValue <> "-1" Then
                'Dim Sql As String = ""
                'Dim oEVSC As EnumVehicleSubCategory = New EnumVehicleSubCategory
                'Dim sVals As String = oEVSC.GetSQLValue(ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue)
                ''Dim sVals As String = EnumVehicleSubCategory.GetSQLValue(ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue)

                'Sql &= " select count(*) from PKDetail pkd,  VechileType vt "
                'Sql &= " where pkd.PKHeaderID = PKHeader.ID And pkd.VehicleTypeCode = vt.VechileTypeCode "
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
                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.No, "(" & Sql & ")"))

                Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where RowStatus = 0 and SubCategoryVehicleID = " & ddlSubCategory.SelectedValue
                Dim strSql2 As String = "select PKHeaderID from PKDetail a join VechileType b on a.VehicleTypeCode = b.VechileTypeCode and b.RowStatus = 0 "
                strSql2 += " join VechileModel c on b.ModelID = c.ID and c.RowStatus = 0 where a.RowStatus = 0 and c.ID in (" & strSql & ")"
                criterias.opAnd(New Criteria(GetType(PKHeader), "ID", MatchType.InSet, "(" & strSql2 & ")"))

            End If
            'End    :Not uploaded Yet
            If ddlPurpose.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Purpose", MatchType.Exact, ddlPurpose.SelectedValue))

            Dim tgl As DateTime = System.Convert.ToDateTime(ddlRencanaPenebusan.SelectedItem.ToString)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeMonth", MatchType.Exact, CType(tgl.Month, Integer)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeYear", MatchType.Exact, CType(tgl.Year, Integer)))

            sessionHelper.SetSession("SearchPK.critsPK", criterias)

            objPK = New PKHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dgListPK.PageSize, _
                          total, CType(ViewState("CurrentSortColumn"), String), _
                          CType(ViewState("CurrentSortDirect"), Sort.SortDirection))


            dgListPK.VirtualItemCount = total
            dgListPK.DataSource = objPK
            If objPK.Count = 0 Then
                tblOperator.Visible = False
                dgListPK.DataBind()
                MessageBox.Show("Data Tidak Ditemukan")
            Else
                dgListPK.DataBind()
                tblOperator.Visible = True
            End If

            arlPK = New PKHeaderFacade(User).Retrieve(criterias)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TotalAmount()
        'todo aggregate
        Dim tot As Double = 0
        Dim Qty As Double = 0
        For Each item As PKHeader In arlPK
            tot = tot + item.TotalHargaTebus
            Qty = Qty + item.TotalQuantity
        Next
        lblTotal.Text = FormatNumber(tot, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblQuantity.Text = FormatNumber(Qty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " Unit"
    End Sub

    Private Function PopulatePKConfirm() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As New KTB.Dnet.Domain.PKHeader
                _pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                _pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                If _pk.PKStatus = enumStatusPK.Status.Validasi Or _pk.PKStatus = enumStatusPK.Status.Tunggu_Diskon Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.Confirm
                    '_pk.IsAproveRilis = 1
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function CountPrice(ByVal objPKDetail As PKDetail, ByVal objPKHeader As PKHeader) As PKDetail
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, objPKHeader.Dealer.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.ID", MatchType.Exact, objPKDetail.VechileColor.ID))
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing("ValidFrom")) Then
            sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))
        Else
            sortColl = Nothing
        End If
        Dim objPriceArrayList As ArrayList = New PriceFacade(User).Retrieve(criterias, sortColl)
        Dim objPrice As Price
        Dim rencanaPenebusan As New DateTime(CInt(objPKDetail.PKHeader.RequestPeriodeYear), CInt(objPKDetail.PKHeader.RequestPeriodeMonth), 1, 0, 0, 0)
        For Each item As Price In objPriceArrayList
            If item.ValidFrom <= GetValidFromDocument(objPKHeader) Then '  rencanaPenebusan Then
                objPrice = item
                Exit For
            End If
        Next

        If objPrice Is Nothing Then
            Throw New Exception("Daftar Harga tidak ada untuk material : " & objPKDetail.MaterialNumber)
        End If

        objPKDetail.TargetAmount = Calculation.CountPKVehiclePrice(0, CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.BasePrice, Double), CType(objPrice.OptionPrice, Double), objPKDetail.ResponseSalesSurcharge)
        objPKDetail.ResponseAmount = Calculation.CountPKVehiclePrice(CType(objPKDetail.ResponseDiscount, Double), CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.BasePrice, Double), CType(objPrice.OptionPrice, Double), objPKDetail.ResponseSalesSurcharge)
        objPKDetail.TargetPPh22 = Calculation.CountPKPPh22(CType(objPrice.BasePrice, Double), 0, CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.PPh22, Double))
        objPKDetail.ResponsePPh22 = Calculation.CountPKPPh22(CType(objPrice.BasePrice, Double), CType(objPKDetail.ResponseDiscount, Double), CType(objPrice.PPN_BM, Double), CType(objPrice.PPN, Double), CType(objPrice.PPh22, Double))
        Dim objPKDetailFacade As New PKDetailFacade(User)
        objPKDetailFacade.Update(objPKDetail)
        Return objPKDetail
    End Function

    Private Function GetValidFromDocument(ByVal oPKH As PKHeader)
        Dim Tanggal As DateTime

        Tanggal = New Date(oPKH.RequestPeriodeYear, oPKH.RequestPeriodeMonth, oPKH.RequestPeriodeDay)
        'apakah mmenungkinkan, bulan validasi berbeda dengan bulan Rencana Penebusan
        'info : rencana penebusan ada 2 : 
        '1. utk bulanan, bulan>thismonth
        '2. utk tambahan, bulan yg sama dengan tanggal yg beda
        'jika tanggal validasi bisa berbeda dengan tanggal rencana penebusan (BEFORE OR AFTER), maka code menjadi
        'Tanggal = New Date(Now.Year, Now.Year.Month, 1)

        If CType(oPKH.OrderType, Integer) = CInt(LookUp.EnumJenisPesanan.Bulanan) Then
            Tanggal = DateSerial(Tanggal.Year, Tanggal.Month, 1)
        ElseIf CType(oPKH.OrderType, Integer) = CInt(LookUp.EnumJenisPesanan.Tambahan) Then
            Tanggal = DateSerial(Tanggal.Year, Tanggal.Month, Now.Day)
        End If
        Return Tanggal
    End Function

    Private Function PopulatePKRelease() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim i As Integer
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                '_pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                i = oDataGridItem.Cells(1).Text 'CType(oDataGridItem.FindControl("lblPKType"), Label).Text
                If _pk.PKStatus = enumStatusPK.Status.Konfirmasi Then
                    Dim RespQty As Integer = 0
                    If i = 1 Then
                        '----PK Biasa
                        _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                        If _pk.Dealer.FreePPh22Indicator = 0 Then
                            Dim requestDateTime As New DateTime(_pk.RequestPeriodeYear, _pk.RequestPeriodeMonth, 1)
                            If requestDateTime >= _pk.Dealer.FreePPh22From AndAlso requestDateTime < _pk.Dealer.FreePPh22To Then
                                _pk.FreePPh22Indicator = _pk.Dealer.FreePPh22Indicator
                            Else
                                _pk.FreePPh22Indicator = 1
                            End If
                        Else
                            _pk.FreePPh22Indicator = _pk.Dealer.FreePPh22Indicator
                        End If
                        _pk.PKStatus = status.Agree
                        For Each item As PKDetail In _pk.PKDetails
                            If item.VechileColor.ColorCode.ToUpper <> "ZZZZ" Then
                                item.PKHeader = _pk
                                item = CountPrice(item, _pk)
                            End If
                        Next
                        Dim totalAlokasi As Integer = 0
                        For Each item As PKDetail In _pk.PKDetails
                            totalAlokasi += item.ResponseQty
                        Next
                        If totalAlokasi <= 0 Then
                            _pk.PKStatus = status.Reject
                        End If
                        oExArgs.Add(_pk)
                    Else
                        _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                        If _pk.Dealer.FreePPh22Indicator = 0 Then
                            Dim requestDateTime As New DateTime(_pk.RequestPeriodeYear, _pk.RequestPeriodeMonth, 1)
                            If requestDateTime >= _pk.Dealer.FreePPh22From AndAlso requestDateTime < _pk.Dealer.FreePPh22To Then
                                _pk.FreePPh22Indicator = _pk.Dealer.FreePPh22Indicator
                            Else
                                _pk.FreePPh22Indicator = 1
                            End If
                        Else
                            _pk.FreePPh22Indicator = _pk.Dealer.FreePPh22Indicator
                        End If
                        Dim IsValidColor As Boolean = True
                        'For Each item As PKDetail In _pk.PKDetails
                        '    If item.VehicleColorCode.ToUpper = "ZZZZ" Then
                        '        IsValidColor = False
                        '    End If
                        'Next
                        'If IsValidColor Then

                        _pk.PKStatus = status.Release
                        If _pk.IsAproveRilis = 1 Then
                            If _pk.IsAutoApprovedDealer = 1 Then
                                _pk.PKStatus = status.Agree
                            End If
                        Else
                            If _pk.SPLNumber = "" Then
                                _pk.PKStatus = status.Agree
                            End If
                        End If

                        For Each item As PKDetail In _pk.PKDetails
                            If item.VechileColor.ColorCode.ToUpper <> "ZZZZ" Then
                                'item.PKHeader = _pk
                                item = CountPrice(item, _pk)
                                RespQty += item.ResponseQty
                            End If
                        Next
                        Dim totalAlokasi As Integer = 0
                        For Each item As PKDetail In _pk.PKDetails
                            totalAlokasi += item.ResponseQty
                        Next
                        oExArgs.Add(_pk)
                    End If
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulatePKUnRelease() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                '_pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                If _pk.PKStatus = enumStatusPK.Status.Rilis Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.UnRelease
                    For Each objPkDetail As PKDetail In _pk.PKDetails
                        objPkDetail.ResponseQty = 0
                    Next
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulatePKBatalSetuju() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                If _pk.PKStatus = enumStatusPK.Status.Setuju Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.UnAgree
                    For Each objPkDetail As PKDetail In _pk.PKDetails
                        objPkDetail.ResponseQty = 0
                    Next
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function


    Private Function PopulatePKBatal() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                '_pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                If _pk.PKStatus = enumStatusPK.Status.Konfirmasi Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.UnConfirm
                    For Each objPkDetail As PKDetail In _pk.PKDetails
                        objPkDetail.ResponseQty = 0
                    Next
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulatePKReject() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                '_pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                If _pk.PKStatus = enumStatusPK.Status.Konfirmasi Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.Reject
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulatePKUnReject() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                '_pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                If _pk.PKStatus = enumStatusPK.Status.Ditolak Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.Confirm
                    For Each objPkDetail As PKDetail In _pk.PKDetails
                        objPkDetail.ResponseQty = 0
                    Next
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "PKNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Function PopulatePKBtlBlok() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                '_pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                If _pk.PKStatus = enumStatusPK.Status.DiBlok Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.Confirm
                    For Each objPkDetail As PKDetail In _pk.PKDetails
                        objPkDetail.ResponseQty = 0
                    Next
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulatePKTungguDiskon() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                If _pk.PKStatus = enumStatusPK.Status.Konfirmasi Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.Tunggu_Diskon
                    'For Each objPkDetail As PKDetail In _pk.PKDetails
                    '    objPkDetail.ResponseQty = 0
                    'Next
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulatePKBlok() As ArrayList
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        Dim objPKHeaderFacade As New PKHeaderFacade(User)

        For Each oDataGridItem In dgListPK.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then
                Dim _pk As KTB.Dnet.Domain.PKHeader = New PKHeaderFacade(User).Retrieve(CInt(CType(oDataGridItem.FindControl("lblID"), Label).Text))
                '_pk.ID = CType(oDataGridItem.FindControl("lblID"), Label).Text
                '_pk.PKStatus = CType(oDataGridItem.FindControl("lblStatus"), Label).Text
                If _pk.PKStatus = enumStatusPK.Status.Rilis Then
                    _pk = objPKHeaderFacade.Retrieve(_pk.ID)
                    _pk.PKStatus = status.Block
                    For Each objPkDetail As PKDetail In _pk.PKDetails
                        objPkDetail.ResponseQty = 0
                    Next
                    oExArgs.Add(_pk)
                End If
            End If
        Next
        status = Nothing
        Return oExArgs
    End Function

    Private Function PopulatePKForDownloadPurpose() As String
        Dim helper As FileHelper = New FileHelper
        Dim finfo As FileInfo
        Dim filePath As String = String.Empty

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'If txtDealerCode.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        'If ddlOrderType.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
        'If lboxStatus.SelectedIndex <> -1 Then
        '    Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKStatus", MatchType.InSet, "(" & SelectedStatus & ")"))
        'Else
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Validasi, Integer) & "," & CType(enumStatusPK.Status.Konfirmasi, Integer) & "," & CType(enumStatusPK.Status.Rilis, Integer) & "," & CType(enumStatusPK.Status.Ditolak, Integer) & "," & CType(enumStatusPK.Status.DiBlok, Integer) & "," & CType(enumStatusPK.Status.Tunggu_Diskon, Integer) & ")"))
        'End If
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKStatus", MatchType.Exact, CType(enumStatusPK.Status.Konfirmasi, Integer)))
        ''If txtPKNumber.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "DealerPKNumber", MatchType.Exact, txtPKNumber.Text))
        'If txtPKNumber.Text.Trim <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKNumber", MatchType.Exact, txtPKNumber.Text))
        'If ddlCategory.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        'If ddlPurpose.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Purpose", MatchType.Exact, ddlPurpose.SelectedValue))
        'Dim tgl As DateTime = System.Convert.ToDateTime(ddlRencanaPenebusan.SelectedItem.ToString)
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeMonth", MatchType.Exact, CType(tgl.Month, Integer)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeYear", MatchType.Exact, CType(tgl.Year, Integer)))

        criterias = sessionHelper.GetSession("SearchPK.critsPK")
        arlPK = New PKHeaderFacade(User).Retrieve(criterias)
        'Dim arlPK As ArrayList = New PKHeaderFacade(User).Retrieve(criterias)

        If Not arlPK Is Nothing Then
            If arlPK.Count > 0 Then
                finfo = helper.DownloadPKRequest(arlPK)

                ' Dim dirFile As String = Server.MapPath("../") & KTB.DNet.Lib.WebConfig.GetValue("PKTemporary")
                Dim dirFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("PKTemporary")

                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.202
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Try
                    success = imp.Start
                    If success Then
                        Dim dir As DirectoryInfo = New DirectoryInfo(dirFile)
                        If Not dir.Exists Then
                            dir.Create()
                        End If
                        Dim _FileName As String = finfo.Name
                        Dim newFullName As String = dir.FullName & _FileName
                        finfo.CopyTo(newFullName, True)
                        finfo.Delete()
                        filePath = KTB.DNet.Lib.WebConfig.GetValue("PKTemporary") & _FileName
                    Else
                        MessageBox.Show(SR.DataNotFound("PK"))
                    End If
                Catch ex As Exception
                    MessageBox.Show(SR.DataNotFound("PK"))
                Finally
                    imp.StopImpersonate()
                    imp = Nothing
                End Try
            Else
                MessageBox.Show(SR.DataNotFound("PK"))
            End If
        End If
        Return filePath
    End Function

    'Private Function getDiscounts(ByVal PKHeaderDetails As ArrayList) As ArrayList
    '    Dim discount As ArrayList = New ArrayList()
    '    For Each detail As PKDetail In PKHeaderDetails
    '        Dim datas As DataSet = New PKHeaderFacade(User).RetrieveSp("exec sp_PKHeader_getDiscount @ID=" & detail.ID)
    '        If datas.Tables.Count > 0 Then
    '            Dim dataTable As DataTable = datas.Tables(0)
    '            If dataTable.Rows.Count > 0 Then
    '                Dim tempDiscount As Integer() = {0, 0, 0, 0, 0, 0, 0, 0, 0}
    '                For i As Integer = 1 To 9
    '                    Dim colStr = "discount" & i
    '                    If Not IsDBNull(dataTable.Rows(0)(colStr)) Then
    '                        tempDiscount(i - 1) = CInt(dataTable.Rows(0)(colStr))
    '                    End If
    '                Next

    '                discount.Add(tempDiscount)
    '            End If
    '        End If
    '    Next

    '    Return discount
    'End Function

    'Private Function getPKDetails(ByVal materialNo As String, ByVal pkNo As String) As ArrayList
    '    Dim arrPKDetail As ArrayList = New ArrayList()
    '    Dim rawData As DataSet = New PKHeaderFacade(User).RetrieveSp("exec sp_PKHeader_getPKDetail @MaterialNo='" & materialNo & "',@PKNo='" & pkNo & "'")
    '    If rawData.Tables.Count > 0 Then
    '        If rawData.Tables(0).Rows.Count > 0 Then
    '            For Each row As DataRow In rawData.Tables(0).Rows
    '                Dim id As Integer = CInt(row("ID"))
    '                Dim pkDetail As PKDetail = New PKDetailFacade(User).Retrieve(id)
    '                arrPKDetail.Add(pkDetail)
    '            Next
    '        End If
    '    End If

    '    Return arrPKDetail
    'End Function

    'Private Sub generateContract(ByVal pk As PKHeader, ByVal contractNo As String, ByVal lineItem As ArrayList, ByVal materialNo As ArrayList)
    '    Dim contractHeader As KTB.DNet.Parser.Domain.ContractJson = New KTB.DNet.Parser.Domain.ContractJson()
    '    contractHeader.ContractNo = contractNo
    '    contractHeader.DealerCode = pk.Dealer.DealerCode
    '    contractHeader.Description = pk.ProjectName
    '    contractHeader.PKNumber = pk.PKNumber
    '    contractHeader.ContractPeriod = pk.RequestPeriodeDay.ToString("D2") & pk.RequestPeriodeMonth.ToString("D2") & pk.RequestPeriodeYear.ToString("D4")
    '    contractHeader.ContractPricingPeriod = pk.PricingPeriodeDay.ToString("D2") & pk.PricingPeriodeMonth.ToString("D2") & pk.PricingPeriodeYear.ToString("D4")

    '    contractHeader.Detail = New List(Of ContractDetailJson)

    '    For i As Integer = 0 To materialNo.Count - 1

    '        Dim arrPKDetail As ArrayList = getPKDetails(materialNo(i), pk.PKNumber)
    '        Dim detail As KTB.DNet.Parser.Domain.ContractDetailJson = New KTB.DNet.Parser.Domain.ContractDetailJson()
    '        detail.LineItem = lineItem(i)
    '        detail.MaterialCode = materialNo(i)
    '        If arrPKDetail.Count > 0 Then
    '            detail.ContractQty = CType(arrPKDetail(0), PKDetail).ResponseQty
    '        End If

    '        contractHeader.Detail.Add(detail)
    '    Next

    '    Dim strJson As String = JsonConvert.SerializeObject(contractHeader)
    '    Dim JSONWorker As KTB.DNet.Parser.JSONWorker = New KTB.DNet.Parser.JSONWorker()
    '    JSONWorker.JSONProses(strJson, "OCCREATE")
    '    Exit Sub
    'End Sub

    'Private Function TransferPKs(al As ArrayList) As Boolean
    '    If al.Count > 0 Then
    '        Dim UserName As String
    '        Dim Password As String
    '        Dim sapConStr As String = KTB.DNet.Lib.WebConfig.GetValue("SAPConnectionStringEmpty") ' User "SAPConnectionString" and prompt user to enter password first
    '        Dim oSAPDnet As SAPDNet
    '        Dim SONumber As String = "", Msg As String = ""
    '        Dim aErrors As New ArrayList
    '        Dim oUI As UserInfo = CType(sessionHelper.GetSession("LOGINUSERINFO"), UserInfo)
    '        Dim oPKH As PKHeader

    '        Try
    '            UserName = Me.txtUser.Text
    '            Password = Me.txtPass.Text
    '            oSAPDnet = New SAPDNet(sapConStr, UserName, Password)
    '            'oSAPDnet = New SAPDNet(sapConStr)
    '            For i As Integer = 0 To al.Count - 1
    '                oPKH = CType(al(i), PKHeader)

    '                SONumber = ""
    '                Msg = ""

    '                Dim isDetailAllZZZZ As Boolean = True
    '                For Each item As PKDetail In oPKH.PKDetails
    '                    If item.VechileColor.ColorCode <> "ZZZZ" Then
    '                        isDetailAllZZZZ = False
    '                        Exit For
    '                    End If
    '                Next

    '                Dim objSPL As SPL

    '                If Not isDetailAllZZZZ Then
    '                    objSPL = New SPLFacade(System.Threading.Thread.CurrentPrincipal).Retrieve(oPKH.SPLNumber)
    '                End If

    '                Dim vechileColorID As Integer = New VechileColorFacade(System.Threading.Thread.CurrentPrincipal).Retrieve("ZZZZ").ID
    '                Dim result As ArrayList = New ArrayList() ''[inquiryNo, contractNo, materialNo, lineItem]
    '                Dim discounts As ArrayList = getDiscounts(oPKH.PKDetails)

    '                oSAPDnet.SendPKViaRFC(oPKH, Msg, isDetailAllZZZZ, objSPL, vechileColorID, discounts, result)
    '                If Msg <> String.Empty Then
    '                    aErrors.Add("Error PK : " & oPKH.PKNumber & ". " & Msg)
    '                Else
    '                    If result.Count > 0 Then
    '                        If result(1) <> "" OrElse Not IsNothing(result(1)) Then
    '                            generateContract(oPKH, result(1), result(3), result(2))
    '                        End If
    '                    End If

    '                    oPKH.PKStatus = 9
    '                    Dim updateResult As Integer = New PKHeaderFacade(User).Update(oPKH)
    '                End If
    '            Next
    '            If aErrors.Count > 0 Then
    '                Msg = ""
    '                For Each erm As String In aErrors
    '                    Msg = Msg & erm & ";"
    '                Next
    '                MessageBox.Show("Transfer Gagal. " & Msg)
    '                Return False
    '            Else
    '                MessageBox.Show("Transfer Berhasil.")
    '                Return True
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show("Transfer Gagal. " & ex.Message)
    '            Return False
    '        End Try
    '    Else
    '        MessageBox.Show("Tidak ada data PK untuk Proses Transfer Ulang")
    '        Return False
    '    End If
    'End Function

    Private Function TransferPKs(al As ArrayList, Optional ByRef arrNewPKHeader As ArrayList = Nothing) As Boolean
        If al.Count > 0 Then
            Dim rsltMessages As String = String.Empty
            Dim result As Boolean = New PKHelper(User).TransferPKs(al, Me.txtUser.Text, Me.txtPass.Text, rsltMessages, arrNewPKHeader)

            MessageBox.Show(rsltMessages)
            Return result
        Else
            MessageBox.Show("Tidak ada data PK untuk Proses Transfer Ulang")
            Return False
        End If
    End Function

    Private Function validatePopulatedPK(ByVal listPK As ArrayList, ByRef messages As String) As ArrayList
        messages = String.Empty
        Dim result As New ArrayList()
        Dim totalDiscount As Decimal = 0
        Dim validFlag As Boolean = True
        Dim validPK As ArrayList
        If listPK.Count > 0 Then
            For i As Integer = 0 To listPK.Count - 1
                Dim pKHeader As PKHeader = CType(listPK(i), PKHeader)
                If pKHeader.PKDetails.Count > 0 Then
                    For Each detail As PKDetail In pKHeader.PKDetails
                        Dim crit As New CriteriaComposite(New Criteria(GetType(PKDetailtoDiscount), "RowStatus", MatchType.Exact, 0))
                        crit.opAnd(New Criteria(GetType(PKDetailtoDiscount), "PKDetail.ID", MatchType.Exact, detail.ID))
                        Dim arrPkdetailToDiscount As ArrayList = New PKDetailtoDiscountFacade(User).Retrieve(crit)
                        totalDiscount = 0
                        If arrPkdetailToDiscount.Count > 0 Then

                            For Each pkDetailToDetail As PKDetailtoDiscount In arrPkdetailToDiscount
                                totalDiscount += pkDetailToDetail.Discount
                            Next
                        End If

                        If totalDiscount <> detail.ResponseDiscount Then
                            messages = messages & pKHeader.PKNumber & "\n"
                            validFlag = False
                            CType(listPK(i), PKHeader).ErrorMessage = "Not valid"
                            Exit For
                        Else
                            validFlag = True
                        End If
                    Next
                End If

                If validFlag Then
                    result.Add(pKHeader)
                End If
            Next
        End If

        Return result
    End Function

#End Region


#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Page.Server.ScriptTimeout = 1800
        Page.Server.ClearError()
        If Not SecurityProvider.Authorize(Context.User, SR.ResponsePKViewList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pesanan Kendaraan")
        End If

        Dim isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        Label2.Visible = isPriceVisible
        Label1.Visible = isPriceVisible
        Label12.Visible = isPriceVisible
        lblTotal.Visible = isPriceVisible
        dgListPK.Columns(17).Visible = isPriceVisible

        If Not IsPostBack Then

            If Not IsNothing(Request.QueryString("New")) AndAlso Request.QueryString("New").ToString().ToLower() = "true" Then
                Session("SESSIONRESPONSEPKORDER") = Nothing
            End If
            sessionHelper.SetSession("Un_freeze_status_baru_daftar_pk_privilege", SecurityProvider.Authorize(Context.User, SR.Un_freeze_status_baru_daftar_pk_privilege))
            sessionHelper.SetSession("IsInPeriodForFreezePK", CommonFunction.IsInPeriodForFreezePK(User))
            BindToddl()
            BindToddlCategory()
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)

            InitiatePage()
            tblOperator.Visible = False
            ddlOrderType.ClearSelection()
            ddlOrderType.SelectedIndex = 0
            If ddlCategory.Items.Count = 0 OrElse ddlPurpose.Items.Count = 0 OrElse ddlOrderType.Items.Count = 0 Then
                btnCari.Enabled = False
            End If
            If GetSessionCriteria() Then
                BindGrid()
            End If
            Dim objDealer As Dealer = Session.Item("DEALER")
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                hdnTitle.Value = "MKS"

            Else
                hdnTitle.Value = "DEALER"
                txtDealerBranchCode.Attributes.Add("ReadOnly", "ReadOnly")
            End If

            lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnDownload.Visible = SecurityProvider.Authorize(Context.User, SR.ResponsePKDownload_Privilege)
    End Sub

    Private Sub dgListPK_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgListPK.SortCommand
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

        dgListPK.SelectedIndex = -1
        dgListPK.CurrentPageIndex = 0
        BindDataGrid(dgListPK.CurrentPageIndex)

    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dgListPK.CurrentPageIndex = 0
        BindGrid()
    End Sub

    Private ResponseViewPrivilege As Boolean
    Private Sub dgListPK_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgListPK.ItemDataBound
        Dim _category As Category
        Dim _dealer As Dealer

        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As PKHeader = CType(e.Item.DataItem, PKHeader)
            Dim objPKHeader As PKHeader = RowValue


            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then

                'Handle button approve
                Dim btnAprove As Button = CType(e.Item.FindControl("btnAprove"), Button)
                Dim lblAprove As Label = CType(e.Item.FindControl("lblAprove"), Label)

                'CR Move Unfreeze menu from Daftar PK to Responese PK
                Dim Un_freeze_status_baru_daftar_pk_privilege As Boolean = sessionHelper.GetSession("Un_freeze_status_baru_daftar_pk_privilege")

                Dim lblUnFreeze As LinkButton = CType(e.Item.FindControl("lblUnFreeze"), LinkButton)
                Dim imgUnFreeze As HtmlImage = CType(e.Item.FindControl("imgUnFreeze"), HtmlImage)

                Dim oDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)


                Dim lblDealerBranchCode As Label = CType(e.Item.FindControl("lblDealerBranchCode"), Label)

                If Not IsNothing(objPKHeader.DealerBranch) Then
                    lblDealerBranchCode.Text = objPKHeader.DealerBranch.DealerBranchCode & " / " & objPKHeader.DealerBranch.Term1
                End If


                If objPKHeader.FreezeStatus(Session("IsInPeriodForFreezePK")) = enumStatusPK.EnumFreezeStatus.NeverFreeze OrElse objPKHeader.FreezeStatus(Session("IsInPeriodForFreezePK")) = enumStatusPK.EnumFreezeStatus.NotFreeze Then
                    lblUnFreeze.Visible = False
                    imgUnFreeze.Visible = False
                ElseIf objPKHeader.FreezeStatus(Session("IsInPeriodForFreezePK")) = enumStatusPK.EnumFreezeStatus.Freeze Then
                    lblUnFreeze.Visible = True
                    imgUnFreeze.Visible = True
                    lblUnFreeze.CommandArgument = objPKHeader.ID.ToString & ",1"

                    If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                        lblUnFreeze.Attributes.Add("onclick", "return false;")
                    Else
                        If Un_freeze_status_baru_daftar_pk_privilege Then
                            lblUnFreeze.Attributes.Add("onclick", "return confirm('Apakah anda akan melakukan unfreeze PK ini (" & objPKHeader.PKNumber & ")?');")
                        Else
                            lblUnFreeze.Attributes.Add("onclick", "alert('Anda tidak memiliki privilege untuk melakukan unfreeze PK');return false;")
                        End If
                        imgUnFreeze.Alt = "click to un-freeze"
                    End If
                    imgUnFreeze.Src = "../images/unlock.gif"
                ElseIf objPKHeader.FreezeStatus(Session("IsInPeriodForFreezePK")) = enumStatusPK.EnumFreezeStatus.FreezeButUnlock Then
                    lblUnFreeze.Visible = True
                    imgUnFreeze.Visible = True

                    lblUnFreeze.CommandArgument = objPKHeader.ID.ToString & ",0"

                    If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
                        lblUnFreeze.Attributes.Add("onclick", "return false;")
                    Else
                        If Un_freeze_status_baru_daftar_pk_privilege Then
                            lblUnFreeze.Attributes.Add("onclick", "return confirm('Apakah anda akan melakukan freeze PK ini (" & objPKHeader.PKNumber & ")?');")
                        Else
                            lblUnFreeze.Attributes.Add("onclick", "alert('Anda tidak memiliki privilege untuk melakukan freeze PK');return false;")
                        End If
                        imgUnFreeze.Alt = "click to freeze"
                    End If

                    imgUnFreeze.Src = "../images/lock.gif"
                End If
                'ENd of CR Move Unfreeze menu from Daftar PK to Response PK

                btnAprove.Visible = False
                lblAprove.Visible = False

                Dim IsAproveRelease As Boolean = False
                If IsAproveReleaseRequired(RowValue, False) Then 'PK Khusus Konfirmasi
                    lblAprove.Visible = True
                    lblAprove.Text = CType(RowValue.IsAproveRilis, enumStatusPK.StatusSetujuRilis).ToString.Replace("_", " ")

                    If setuju_rilis_respon_pk_privilege Then
                        Select Case RowValue.IsAproveRilis
                            Case enumStatusPK.StatusSetujuRilis.Belum
                                btnAprove.Visible = True
                                btnAprove.Text = "Setujui"
                                btnAprove.CommandArgument = RowValue.ID.ToString & "," & CInt(enumStatusPK.StatusSetujuRilis.Disetujui).ToString
                                btnAprove.Attributes.Add("onclick", "return confirm('Apakah anda akan menyetujui rilis PK ini (" & RowValue.PKNumber & ")?');")
                            Case enumStatusPK.StatusSetujuRilis.Disetujui
                                IsAproveRelease = True
                                btnAprove.Visible = True
                                btnAprove.Text = "Batalkan"
                                btnAprove.CommandArgument = RowValue.ID.ToString & "," & CInt(enumStatusPK.StatusSetujuRilis.Belum).ToString
                                btnAprove.Attributes.Add("onclick", "return confirm('Apakah anda akan membatalkan persetujuan rilis PK ini (" & RowValue.PKNumber & ")?');")
                        End Select
                    End If

                End If

                Dim dblTotalUnit As Double = 0
                For Each item As PKDetail In objPKHeader.PKDetails
                    dblTotalUnit = dblTotalUnit + item.TargetQty
                Next
                Dim lblSumTargetQty As Label = CType(e.Item.FindControl("lblSumTargetQty"), Label)
                If Not IsNothing(lblSumTargetQty) Then
                    lblSumTargetQty.Text = dblTotalUnit.ToString("#,##0")
                End If


                e.Item.Cells(4).Text = (e.Item.ItemIndex + 1 + (dgListPK.PageSize * dgListPK.CurrentPageIndex)).ToString
                If e.Item.ItemIndex = 0 Then
                    ResponseViewPrivilege = SecurityProvider.Authorize(Context.User, SR.ResponsePKViewDetail_Privilege)
                End If
                Dim lblStatusString As Label = CType(e.Item.FindControl("lblStatusString"), Label)
                Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
                Dim lblOrderType As Label = CType(e.Item.FindControl("lblOrderType"), Label)
                Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
                Dim lblSubTotal As Label = CType(e.Item.FindControl("lblSubTotal"), Label)
                Dim EnumStatus As enumStatusPK.Status = RowValue.PKStatus
                Dim EnumOrderType As LookUp.EnumJenisPesanan = RowValue.OrderType
                lblOrderType.Text = EnumOrderType.ToString
                lblStatusString.Text = EnumStatus.ToString.Replace("_", " ")
                lblDealer.Text = RowValue.Dealer.DealerCode
                If Not IsNothing(lblDealer) Then
                    lblDealer.ToolTip = RowValue.Dealer.SearchTerm1
                End If
                'lblSubTotal.Text = FormatNumber(RowValue.TotalHargaTebus, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(21).Text = RowValue.Dealer.DealerCode
                'Try
                '    _category = New CategoryFacade(User).Retrieve(RowValue.Category.ID)
                '    lblCategory.Text = _category.CategoryCode
                'Catch ex As Exception
                '    lblCategory.Text = ""
                'End Try
                Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                If ((RowValue.PKStatus <> enumStatusPK.Status.Konfirmasi AndAlso RowValue.PKStatus <> enumStatusPK.Status.Tunggu_Diskon) Or IsAproveRelease) Then
                    lbtnEdit.Text = "<img src=""../images/detail.gif"" border=""0"" alt=""Lihat"">"
                    lbtnEdit.Visible = ResponseViewPrivilege
                Else
                    'lbtnEdit.Visible = SecurityProvider.Authorize(Context.User, SR.ResponsePKIconView_Privilege)
                End If
                If RowValue.PKStatus = enumStatusPK.Status.Rilis Then
                    e.Item.BackColor = System.Drawing.Color.Yellow
                End If

                For Each item As PKDetail In RowValue.PKDetails
                    If item.VehicleColorCode = "ZZZZ" Then
                        e.Item.BackColor = System.Drawing.Color.Aqua
                    End If
                Next

                Dim stockRatioProblem As String = ""
                If (New KTB.DNet.BusinessFacade.TransactionControlPKFacade(User).IsTransactionPKBlocked(objPKHeader, objPKHeader.Dealer, EnumTransactionControlPKKind.TransactionControlPKKind.KONFIRMASI_PK_TAMBAHAN, stockRatioProblem)) Then
                    If stockRatioProblem.Trim <> "" Then
                        e.Item.BackColor = System.Drawing.Color.Red
                    End If
                End If
            End If
        End If
    End Sub


    Private Function IsAproveReleaseRequired(ByVal objPKHeader As PKHeader, ByVal IsAlreadyChangeInClient As Boolean) As Boolean

        Dim result As Boolean = False

        If objPKHeader.Purpose = 0 And ((objPKHeader.PKStatus = enumStatusPK.Status.Konfirmasi And Not IsAlreadyChangeInClient) Or (objPKHeader.PKStatus = enumStatusPK.Status.Rilis And IsAlreadyChangeInClient)) Then
            If objPKHeader.SPLNumber.Trim <> "" OrElse objPKHeader.FreeIntIndicator = 0 OrElse objPKHeader.MaxTopIndicator <> -1 OrElse IsDetailNotZero(objPKHeader) Then
                result = True
            End If
        End If

        Return result
    End Function


    Private Function IsDetailNotZero(ByVal objPKHeader As PKHeader) As Boolean
        Dim result As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKDetail), "PKHeader.ID", MatchType.Exact, objPKHeader.ID))

        Dim arrListPKdetail As ArrayList = New PKDetailFacade(User).Retrieve(criterias)
        For Each item As PKDetail In arrListPKdetail
            If item.ResponseDiscount > 0 Or item.ResponseSalesSurcharge > 0 Then
                result = True
                Exit For
            End If
        Next

        Return result
    End Function

    Private Sub btnConfirm_Click()
        Dim listPK As ArrayList = PopulatePKConfirm()

        If listPK.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("PK", "Validasi"))
        Else
            Dim allstockRatioProblem As String = ""
            Dim validatedPKList As New ArrayList
            For i As Integer = 0 To listPK.Count - 1
                Dim objPKHeader As PKHeader = CType(listPK(i), PKHeader)
                Dim dealerPK As Dealer = objPKHeader.Dealer
                Dim stockRatioProblem As String = ""
                If (New KTB.DNet.BusinessFacade.TransactionControlPKFacade(User).IsTransactionPKBlocked(objPKHeader, dealerPK, EnumTransactionControlPKKind.TransactionControlPKKind.KONFIRMASI_PK_TAMBAHAN, stockRatioProblem)) Then
                    If stockRatioProblem.Trim <> "" Then
                        allstockRatioProblem &= stockRatioProblem
                    Else
                        validatedPKList.Add(objPKHeader)
                    End If
                Else
                    validatedPKList.Add(objPKHeader)
                End If
            Next
            listPK = validatedPKList
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.validatePK(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Validasi)
            BindGrid()
            If (allstockRatioProblem.Trim <> "") Then
                MessageBox.Show("PK tidak dapat dikonfirmasi dikarenakan stok ratio tidak terpenuhi.")
            End If
        End If
    End Sub

    Private Sub btnBatal_Click()
        Dim listPK As ArrayList = PopulatePKBatal()

        If listPK.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("PK", "Konfirmasi"))
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.UpdatePKStatusQty(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Konfirmasi)
            BindGrid()
        End If
    End Sub

    Private Sub dgListPK_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgListPK.ItemCommand
        Dim objDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)
        If e.CommandName = "edit" Then
            'If (e.Item.Cells(1).Text = 0) AndAlso (e.Item.Cells(15).Text = enumStatusPK.Status.Konfirmasi) Then
            'e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dgListPK.PageSize * dgListPK.CurrentPageIndex)).ToString

            Dim isAproveRilis As Boolean = False
            Dim lblAprove As Label = CType(e.Item.FindControl("lblAprove"), Label)
            If lblAprove.Visible = True And lblAprove.Text = enumStatusPK.StatusSetujuRilis.Disetujui.ToString Then
                isAproveRilis = True
            End If
            If ((e.Item.Cells(20).Text = enumStatusPK.Status.Konfirmasi OrElse e.Item.Cells(20).Text = enumStatusPK.Status.Tunggu_Diskon) And Not isAproveRilis) Then
                sessionHelper.SetSession("PrevPage", Request.Url.GetLeftPart(UriPartial.Path).ToString())
                SetSessionCriteria()
                Response.Redirect("ResponsePKOrderDetail.aspx?master=" & e.Item.Cells(0).Text)

            ElseIf (e.Item.Cells(1).Text = 1) Then   'PKType = 1
                sessionHelper.SetSession("PrevPage", Request.Url.GetLeftPart(UriPartial.Path).ToString())
                sessionHelper.SetSession("CallerPage", "ResponsePKOrder")
                SetSessionCriteria()
                Response.Redirect("../PK/PesananKendaraanBiasa.aspx?PKNumber=" & e.Item.Cells(9).Text & "&DealerCode=" & e.Item.Cells(21).Text & "&Src=" & "response")
            Else
                sessionHelper.SetSession("PrevPage", Request.Url.GetLeftPart(UriPartial.Path).ToString())
                sessionHelper.SetSession("CallerPage", "ResponsePKOrder")
                SetSessionCriteria()
                Response.Redirect("../PK/PesananKendaraanKhusus.aspx?PKNumber=" & e.Item.Cells(9).Text & "&DealerCode=" & e.Item.Cells(21).Text & "&Src=" & "key")
                'Response.Redirect("../PK/PesananKendaraanKhusus.aspx?PKNumber=" & e.Item.Cells(4).Text & "&DealerCode=" & e.Item.Cells(13).Text & "&Src=search&key=" & Key)

            End If

        ElseIf e.CommandName = "AproveRilis" Then
            Dim PKHeaderID, NewStatus As Integer
            PKHeaderID = CStr(e.CommandArgument).Split(",").GetValue(0)
            NewStatus = CStr(e.CommandArgument).Split(",").GetValue(1)

            Dim PKFacade As New PKHeaderFacade(User)
            Dim objPKHeader As PKHeader = PKFacade.Retrieve(PKHeaderID)
            objPKHeader.IsAproveRilis = NewStatus

            PKFacade.Update(objPKHeader)

            BindGrid()
        ElseIf e.CommandName = "lnkFreeze" Then
            Dim IDPK As Integer = CStr(e.CommandArgument).Split(",").GetValue(0)
            Dim IsUnlockFreeze As Integer = CStr(e.CommandArgument).Split(",").GetValue(1)

            Dim PKFacade As New PKHeaderFacade(User)

            Dim objPK As PKHeader = PKFacade.Retrieve(IDPK)
            objPK.IsUnlockFreeze = IsUnlockFreeze
            PKFacade.Update(objPK)
            BindGrid()
        End If
    End Sub

    Private Sub btnRelease_Click()
        Dim listPK As ArrayList = PopulatePKRelease()

        For Each item As PKHeader In listPK

            If IsAproveReleaseRequired(item, True) Then 'PK Khusus Konfirmasi
                If item.IsAproveRilis = enumStatusPK.StatusSetujuRilis.Belum Then
                    MessageBox.Show("Data PK khusus " & item.PKNumber & " belum disetujui untuk rilis, proses gagal")
                    Return
                End If
            End If

        Next

        If listPK.Count = 0 Then
            MessageBox.Show(SR.InvalidData("PK"))
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.validatePK(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Konfirmasi)
            BindGrid()
        End If
    End Sub

    Private Sub btnUnRelease_Click()
        Dim listPK As ArrayList = PopulatePKUnRelease()

        If listPK.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("PK", "Rilis"))
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.UpdatePKStatusQty(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Rilis)
            BindGrid()
        End If

    End Sub

    Public Sub dgListPK_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dgListPK.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Private Sub btnReject_Click()
        Dim listPK As ArrayList = PopulatePKReject()

        If listPK.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("PK", "Konfirmasi"))
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.UpdatePKStatusQty(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Konfirmasi)
            BindGrid()
        End If
    End Sub

    Private Sub btnUnReject_Click()
        Dim listPK As ArrayList = PopulatePKUnReject()

        If listPK.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("PK", "Ditolak"))
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.UpdatePKStatusQty(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Ditolak)
            BindGrid()
        End If
    End Sub

    Private Sub btnBtlBlok_Click()
        Dim listPK As ArrayList = PopulatePKBtlBlok()

        If listPK.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("PK", "Di Blok"))
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.UpdatePKStatusQty(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.DiBlok)
            BindGrid()
        End If
    End Sub

    Private Sub btnBtlSetuju_Click()
        Dim listPK As ArrayList = PopulatePKBatalSetuju()
        If listPK.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("PK", "Di Setuju"))
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.UpdatePKStatusQty(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Setuju)
            BindGrid()
        End If
    End Sub

    Private Sub btnBlok_Click()
        Dim listPK As ArrayList = PopulatePKBlok()

        If listPK.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("PK", "Rilis"))
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.validatePK(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Rilis)
            BindGrid()
        End If
    End Sub

    Private Sub btnTungguDiskon_Click()
        Dim listPK As ArrayList = PopulatePKTungguDiskon()

        If listPK.Count = 0 Then
            MessageBox.Show(SR.DataNotSelectedByStatus("PK", "Konfirmasi"))
        Else
            Dim objPKHeaderFacade As New PKHeaderFacade(User)
            objPKHeaderFacade.validatePK(listPK)
            RecordStatusChangeHistory(listPK, enumStatusPK.Status.Rilis)
            BindGrid()
        End If
    End Sub
    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        'Try
        '    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        '    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        '    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        '    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        '    Dim success As Boolean = False
        '    success = imp.Start()
        '    If success Then
        'Dim path As String = PopulatePKForDownloadPurpose()
        '        imp.StopImpersonate()
        '        imp = Nothing
        '        If Not path = String.Empty Then
        '            Response.Redirect("../Download.aspx?file=" & path)
        '        End If
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(SR.DownloadFail("PK"))
        'End Try

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        criterias = sessionHelper.GetSession("SearchPK.critsPK")
        If criterias Is Nothing Then
            MessageBox.Show("Tidak ada data")
            Exit Sub
        End If
        arlPK = New PKHeaderFacade(User).Retrieve(criterias)

        If Not arlPK Is Nothing Then
            If arlPK.Count > 0 Then
                DoDownload(arlPK)
            Else
                MessageBox.Show("Tidak ada data")
            End If
        Else
            MessageBox.Show("Tidak ada data")
        End If
    End Sub

    Private Sub DoDownload(ByVal dataArr As ArrayList)
        Dim sFileName As String
        sFileName = "Daftar_Pesanan_Respon_PK[" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "]"

        '-- Temp file must be a randomly named file!
        Dim PKData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        'Try
        If imp.Start() Then
            Dim finfo As FileInfo = New FileInfo(PKData)
            If finfo.Exists Then
                finfo.Delete()  '-- Delete temp file if exists
            End If

            '-- Create file stream
            Dim fs As FileStream = New FileStream(PKData, FileMode.CreateNew)
            '-- Create stream writer
            Dim sw As StreamWriter = New StreamWriter(fs)

            '-- Write data to temp file
            WritePKData(sw, dataArr)

            sw.Close()
            fs.Close()

            imp.StopImpersonate()
            imp = Nothing

        End If
        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        'Catch ex As Exception
        'MessageBox.Show("Download data gagal")
        'End Try
    End Sub

    Private Sub WritePKData(ByVal sw As StreamWriter, ByVal dataArr As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append("No" & tab)
        itemLine.Append("Kategori" & tab)
        itemLine.Append("Nomor Reg PK" & tab)
        itemLine.Append("Kode Dealer" & tab)
        itemLine.Append("Jenis Pesanan" & tab)
        itemLine.Append("Tahun Perakitan" & tab)
        itemLine.Append("Nama Pesanan Khusus" & tab)
        itemLine.Append("Tipe Kendaraan" & tab)
        itemLine.Append("Unit Pesanan" & tab)
        sw.WriteLine(itemLine.ToString())
        Dim i As Integer = 1
        For Each PK As PKHeader In dataArr
            For Each item As PKDetail In PK.PKDetails
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(PK.Category.CategoryCode & tab)
                itemLine.Append(PK.PKNumber & tab)
                itemLine.Append(PK.Dealer.DealerCode & tab)
                Dim EnumOrderType As Lookup.EnumJenisPesanan = PK.OrderType
                itemLine.Append(EnumOrderType.ToString & tab)
                itemLine.Append(PK.ProductionYear & tab)
                itemLine.Append(PK.ProjectName & tab)
                itemLine.Append(item.VechileColor.MaterialNumber & tab)
                itemLine.Append(item.TargetQty & tab)
                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next
        Next

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub

    Private Sub btnProses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProses.Click
        Select Case ddlProses.SelectedValue
            Case 0 'Konfirmasi
                btnConfirm_Click()
            Case 1 'Batal Konfirmasi
                btnBatal_Click()
            Case 2 'Rilis
                btnRelease_Click()
            Case 3 'Batal Rilis
                btnUnRelease_Click()
            Case 4 'Tolak
                btnReject_Click()
            Case 5 'Batal Tolak
                btnUnReject_Click()
            Case 6 'Blok
                btnBlok_Click()
            Case 7 'Batal Blok
                btnBtlBlok_Click()
            Case 8 'batal setuju
                btnBtlSetuju_Click()
            Case 9 'batal setuju
                btnTungguDiskon_Click()

        End Select
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub

    Private Sub btnTransferData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransferData.Click
        If Me.txtPass.Text = String.Empty Then
            RegisterStartupScript("OpenWindow", "<script>InputPasswordPlease();</script>")
            Exit Sub
        End If

        Dim invalidPK As String = String.Empty
        Dim listPK As ArrayList = validatePopulatedPK(PopulatePKTransferData(), invalidPK)
        Dim _fileHelper As New FileHelper
        'Dim str As FileInfo
        Try
            If listPK.Count > 0 Then
                Dim transferStatus As Boolean = TransferPKs(listPK)
                If transferStatus Then
                    'Dim objPKHeaderFacade As New PKHeaderFacade(User)
                    'objPKHeaderFacade.validatePK(listPK)

                    If invalidPK <> String.Empty Then
                        MessageBox.Show("Nomor PK berikut memiliki total discount dan response discount yang berbeda. Silahkan melakukan refresh discount.\n" & invalidPK)
                    End If
                Else
                    Exit Sub
                End If

                'MessageBox.Show(SR.UploadSucces(str.Name))
            Else
                If invalidPK <> String.Empty Then
                    MessageBox.Show("Nomor PK berikut memiliki total discount dan response discount yang berbeda. Silahkan melakukan refresh discount.\n" & invalidPK)
                Else
                    MessageBox.Show(SR.DataNotFoundByStatus("PK", "Setuju, Tidak setuju dan Blok"))
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Tidak Ada PK yang bisa Didownload")
        End Try
    End Sub

    Private Sub btnTransferUlang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransferUlang.Click
        If Me.txtPass.Text = String.Empty Then
            RegisterStartupScript("OpenWindow", "<script>InputPasswordTransferUlang();</script>")
            Exit Sub
        End If

        Dim invalidPK As String = String.Empty
        Dim listPK As ArrayList = validatePopulatedPK(PopulatePKTransferData(), invalidPK)
        Dim _fileHelper As New FileHelper
        'Dim str As FileInfo
        Try
            If listPK.Count > 0 Then
                Dim arrNwePKHeader As New ArrayList()
                Dim transferStatus As Boolean = TransferPKs(listPK, arrNwePKHeader)
                If transferStatus Then
                    updateTransferedPKs(arrNwePKHeader)
                    If invalidPK <> String.Empty Then
                        MessageBox.Show("Nomor PK berikut memiliki total discount dan response discount yang berbeda. Silahkan melakukan refresh discount.\n" & invalidPK)
                    End If
                Else
                    Exit Sub
                End If
                'MessageBox.Show(SR.UploadSucces(str.Name))
            Else
                If invalidPK <> String.Empty Then
                    MessageBox.Show("Nomor PK berikut memiliki total discount dan response discount yang berbeda. Silahkan melakukan refresh discount.\n" & invalidPK)
                Else
                    MessageBox.Show(SR.DataNotFoundByStatus("PK", "Setuju, Tidak setuju dan Blok"))
                End If
            End If
        Catch ex As Exception
            'MessageBox.Show(SR.UploadFail(str.Name))
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub updateTransferedPKs(ByVal aPKs As ArrayList)
        Dim oPKFac As New PKHeaderFacade(User)
        For Each oPK As PKHeader In aPKs
            oPK.RequestPeriodeDay = Now.Day
            oPK.PricingPeriodeDay = oPK.RequestPeriodeDay
            oPKFac.Update(oPK)
        Next
    End Sub

    Private Function PopulatePKTransferData() As ArrayList
        Dim PKHeaderArray As ArrayList = RetriveDownload()
        Dim pkCollection As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        If Not PKHeaderArray Is Nothing Then
            If PKHeaderArray.Count > 0 Then
                For Each _pk As PKHeader In PKHeaderArray
                    If (_pk.PKStatus = enumStatusPK.Status.Setuju Or _pk.PKStatus = enumStatusPK.Status.Tidak_Setuju Or _pk.PKStatus = enumStatusPK.Status.DiBlok) AndAlso (_pk.StatusDownload = 0) Then
                        If _pk.PKStatus = enumStatusPK.Status.Tidak_Setuju Or _pk.PKStatus = enumStatusPK.Status.DiBlok Then
                            For Each item As PKDetail In _pk.PKDetails
                                item.ResponseQty = 0
                            Next
                        End If
                        _pk.StatusDownload = 1
                        pkCollection.Add(_pk)
                    End If
                Next
            End If
        End If
        status = Nothing
        Return pkCollection
    End Function

    Private Function PopulatePKTransferUlangData() As ArrayList
        Dim PKHeaderArray As ArrayList = RetriveDownload()
        Dim pkCollection As New System.Collections.ArrayList
        Dim status As New enumStatusPK
        If Not PKHeaderArray Is Nothing Then
            If PKHeaderArray.Count > 0 Then
                For Each _pk As PKHeader In PKHeaderArray
                    If (_pk.PKStatus = enumStatusPK.Status.Setuju Or _pk.PKStatus = enumStatusPK.Status.Tidak_Setuju Or _pk.PKStatus = enumStatusPK.Status.DiBlok) AndAlso (_pk.StatusDownload = 1) Then
                        If _pk.PKStatus = enumStatusPK.Status.Tidak_Setuju Or _pk.PKStatus = enumStatusPK.Status.DiBlok Then
                            For Each item As PKDetail In _pk.PKDetails
                                item.ResponseQty = 0
                            Next
                        End If
                        '_pk.StatusDownload = 1
                        pkCollection.Add(_pk)
                    End If
                Next
            End If
        End If
        status = Nothing
        Return pkCollection
    End Function

    Private Function RetriveDownload() As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtPKNumber.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "DealerPKNumber", MatchType.Exact, txtPKNumber.Text))
        End If
        If txtDealerCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
        End If
        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKStatus", MatchType.InSet, "(" & SelectedStatus & ")"))
        End If
        If ddlCategory.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        End If
        If ddlPurpose.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Purpose", MatchType.Exact, ddlPurpose.SelectedValue))
        End If
        If ddlOrderType.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
        End If

        If ddlRencanaPenebusan.SelectedIndex <> 0 Then
            Dim tgl As DateTime = System.Convert.ToDateTime(ddlRencanaPenebusan.SelectedItem.ToString)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeMonth", MatchType.Exact, CType(tgl.Month, Integer)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeYear", MatchType.Exact, CType(tgl.Year, Integer)))
        End If
        'sessionHelper.SetSession("SearchPK.critsPK", criterias)
        criterias = sessionHelper.GetSession("SearchPK.critsPK")
        Dim PKHeaderArray As ArrayList = New PKHeaderFacade(User).Retrieve(criterias)
        Return PKHeaderArray
    End Function

#End Region

End Class