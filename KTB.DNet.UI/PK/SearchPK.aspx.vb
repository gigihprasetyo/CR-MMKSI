#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Collections.Generic
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.SAP
Imports KTB.DNet.Parser.Domain
Imports Newtonsoft.Json
#End Region

Public Class SearchPK
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents BtnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgcari As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtPKNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPurpose As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtStatus As System.Web.UI.WebControls.Label
    Protected WithEvents txtKategori As System.Web.UI.WebControls.Label
    Protected WithEvents txtKondisiPesanan As System.Web.UI.WebControls.Label
    Protected WithEvents ddlRencanaPenebusan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
    Protected WithEvents btnTransferData As System.Web.UI.WebControls.Button
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lboxStatus As System.Web.UI.WebControls.ListBox
    Protected WithEvents btnTransferUlang As System.Web.UI.WebControls.Button
    Protected WithEvents lblQuantity As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblValidator As System.Web.UI.WebControls.Label
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlSubCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button

    Protected WithEvents txtDealerBranchCode As TextBox
    Protected WithEvents lblDealerBranch As Label
    Protected WithEvents lblPopUpDealerBranch As Label
    Protected WithEvents txtBranchName As TextBox
    Protected WithEvents hdnTitle As HiddenField

    Protected WithEvents txtPass As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUser As System.Web.UI.WebControls.TextBox

    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

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
    Private arlPKHeader As ArrayList
    Private arlPK As ArrayList
    Private objPKHeader As PKHeader
    Private sessionHelper As New SessionHelper
    Private objDealer As Dealer
#End Region

#Region "Custom Method"
    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList
        objSSPO.Add(txtKodeDealer.Text.Trim)
        objSSPO.Add(txtPKNumber.Text)
        objSSPO.Add(ddlOrderType.SelectedIndex)
        objSSPO.Add(GetSelectedItem(lboxStatus))
        objSSPO.Add(ddlRencanaPenebusan.SelectedIndex)
        objSSPO.Add(ddlOrderType.SelectedIndex)
        objSSPO.Add(ddlPurpose.SelectedIndex)
        objSSPO.Add(dtgcari.CurrentPageIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        objSSPO.Add(ddlCategory.SelectedIndex)
        objSSPO.Add(ddlSubCategory.SelectedIndex)
        sessionHelper.SetSession("SESSIONSEARCHPK", objSSPO)
    End Sub

    Private Function GetSessionCriteria() As Boolean
        Dim objSSPO As ArrayList = sessionHelper.GetSession("SESSIONSEARCHPK")
        If Not objSSPO Is Nothing Then
            txtKodeDealer.Text = objSSPO.Item(0)
            txtPKNumber.Text = objSSPO.Item(1)
            ddlOrderType.SelectedIndex = objSSPO.Item(2)
            Dim str() As String = objSSPO.Item(3).ToString().Split(",")
            For Each item As ListItem In lboxStatus.Items
                For i As Integer = 0 To str.Length - 1
                    If item.Value.ToString = str(i).ToString Then
                        item.Selected = True
                        Exit For
                    End If
                Next
            Next
            ddlRencanaPenebusan.SelectedIndex = objSSPO.Item(4)
            ddlOrderType.SelectedIndex = objSSPO.Item(5)
            ddlPurpose.SelectedIndex = objSSPO.Item(6)
            dtgcari.CurrentPageIndex = objSSPO.Item(7)
            ViewState("CurrentSortColumn") = objSSPO.Item(8)
            ViewState("CurrentSortDirect") = objSSPO.Item(9)
            ddlCategory.SelectedIndex = objSSPO.Item(10)
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
            ddlSubCategory.SelectedIndex = objSSPO.Item(11)
            Return True
        End If
        Return False
    End Function
    Sub dtgcari_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dtgcari.CurrentPageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Private Sub BindGrid()
        BindDataToGrid(dtgcari.CurrentPageIndex)
        TotalAmount()
    End Sub

    Private Sub BindToDropDownList()
        Try
            ddlOrderType.Items.Clear()
            If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) Then
                Dim itemBlank As ListItem = New ListItem("Silahkan Pilih", -1)
                itemBlank.Selected = False
                ddlOrderType.Items.Add(itemBlank)
            End If
            For Each item As ListItem In LookUp.ArrayJenisPesanan
                If item.Text = "Bulanan" Then
                    If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKindBulanan_Privilege) Then
                        ddlOrderType.Items.Add(item)
                        item.Selected = True
                    End If
                ElseIf item.Text = "Tambahan" Then
                    If SecurityProvider.Authorize(Context.User, SR.PKKindAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKindTambahan_Privilege) Then
                        item.Selected = False
                        ddlOrderType.Items.Add(item)
                    End If
                End If
            Next
            ddlOrderType.SelectedIndex = 0
            'ddlOrderType.ClearSelection()
            ddlOrderType.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlOrderType, silahkan kirim error ini ke dnet admin")
        End Try

        Try
            '--DropDownList Rencana Penebusan
            ddlRencanaPenebusan.Items.Clear()
            ddlRencanaPenebusan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            For Each item As ListItem In LookUp.ArraylistMonth(True, 12, 6, DateTime.Now)
                item.Selected = False
                ddlRencanaPenebusan.Items.Add(item)
            Next
            ddlRencanaPenebusan.ClearSelection()
            ddlRencanaPenebusan.SelectedValue = Format(DateTime.Now, "MMM yyyy").ToString
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlRencanaPenebusan, silahkan kirim error ini ke dnet admin")
        End Try


        Try
            lboxStatus.Items.Clear()
            For Each item As ListItem In LookUp.ArrayStatusPK
                item.Selected = False
                lboxStatus.Items.Add(item)
            Next
            lboxStatus.ClearSelection()
            'lboxStatus.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show("Error Binding lboxStatus, silahkan kirim error ini ke dnet admin")
        End Try


        '--DropDownList Kondisi Pesanan
        Try
            ddlPurpose.Items.Clear()
            If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) Then
                Dim listitemBlank1 As ListItem = New ListItem("Silahkan Pilih", -1)
                listitemBlank1.Selected = False
                ddlPurpose.Items.Add(listitemBlank1)
            End If

            For Each item As ListItem In LookUp.ArrayPurpose
                item.Selected = False
                If item.Text = "Khusus" Then
                    If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKondisiKhusus_Privilege) Then
                        ddlPurpose.Items.Add(item)
                    End If
                ElseIf item.Text = "Biasa" Then
                    If SecurityProvider.Authorize(Context.User, SR.PKKondisiAll_Privilege) OrElse SecurityProvider.Authorize(Context.User, SR.PKKondisiBiasa_Privilege) Then
                        item.Selected = True
                        ddlPurpose.Items.Add(item)
                    End If
                End If
            Next
            ddlPurpose.SelectedIndex = 0
            'ddlPurpose.ClearSelection()
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlPurpose, silahkan kirim error ini ke dnet admin")
        End Try

    End Sub

    Private Sub BindToddlCategory()
        Try
            ddlCategory.Items.Clear()
            Dim arrayListCategory As ArrayList = New PKHeaderFacade(User).RetrieveListCategory

            If SecurityProvider.Authorize(Context.User, SR.PKCategoryAll_Privilege) Then
                Dim listitemBlank As New listItem("Silahkan Pilih", -1)
                listitemBlank.Selected = False
                ddlCategory.Items.Add(listitemBlank)
            End If
            For Each item As Category In arrayListCategory
                Dim listItem As New listItem(item.CategoryCode, item.ID)
                If item.CategoryCode = "PC" Then
                    If SecurityProvider.Authorize(Context.User, SR.PKCategoryPC_Privilege) Then
                        listItem.Selected = False
                        ddlCategory.Items.Add(listItem)
                    End If
                ElseIf item.CategoryCode = "LCV" Then
                    If SecurityProvider.Authorize(Context.User, SR.PKCategoryLCV_Privilege) Then
                        listItem.Selected = False
                        ddlCategory.Items.Add(listItem)
                    End If
                ElseIf item.CategoryCode = "CV" Then
                    If SecurityProvider.Authorize(Context.User, SR.PKCategoryCV_Privilege) Then
                        listItem.Selected = True
                        ddlCategory.Items.Add(listItem)
                    End If
                End If
            Next
            ddlCategory.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("Error Binding ddlCategory, silahkan kirim error ini ke dnet admin")
        End Try

    End Sub

    'Private Sub ddlRencanaPenebusan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRencanaPenebusan.SelectedIndexChanged
    '    If ddlRencanaPenebusan.SelectedIndex = 0 Then
    '        vldNomorPK.Enabled = True
    '    Else
    '        vldNomorPK.Enabled = False
    '    End If
    'End Sub

    Private Sub BindDataToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtPKNumber.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKNumber", MatchType.Exact, txtPKNumber.Text))
        End If

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
        End If
        If lboxStatus.SelectedIndex <> -1 Then
            Dim SelectedStatus As String = GetSelectedItem(lboxStatus)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKStatus", MatchType.InSet, "(" & SelectedStatus & ")"))
        End If
        If ddlCategory.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        If txtDealerBranchCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "DealerBranch.DealerBranchCode", MatchType.InSet, "('" & txtDealerBranchCode.Text.Replace(";", "','") & "')"))
        End If
        If ddlCategory.SelectedValue <> -1 And ddlSubCategory.SelectedValue <> "-1" Then
            'Dim Sql As String = ""
            'Dim oEVSC As EnumVehicleSubCategory = New EnumVehicleSubCategory
            'Dim sVals As String = oEVSC.GetSQLValue(ddlCategory.SelectedItem.Text, ddlSubCategory.SelectedValue)

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
        If ddlPurpose.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Purpose", MatchType.Exact, ddlPurpose.SelectedValue))
        End If
        If ddlOrderType.SelectedValue <> -1 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
        End If

        'If Not ddlRencanaPenebusan.SelectedIndex = 0 Then
        'if user select dropdownlist, filter direct get from dropdownlist, because item in 
        'dropdownlist already match with criteria (show item only for next month of this year)
        If ddlRencanaPenebusan.SelectedIndex <> 0 Then
            Dim tgl As DateTime = System.Convert.ToDateTime(ddlRencanaPenebusan.SelectedItem.ToString)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeMonth", MatchType.Exact, CType(tgl.Month, Integer)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RequestPeriodeYear", MatchType.Exact, CType(tgl.Year, Integer)))
        End If

        'Else
        '    'else set criteria for show item only for next month of this year
        '    Dim subcriterias As CriteriaComposite '= New CriteriaComposite(New Criteria(GetType(Ktb.DNet.Domain.PKHeader), "RequestPeriodeYear", MatchType.Exact, CInt(DateTime.Now.Year)))
        '    subcriterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PKHeader), "RequestPeriodeMonth", MatchType.GreaterOrEqual, CInt(DateTime.Now.Month)))

        '    criterias.opAnd(New Criteria(GetType(Ktb.DNet.Domain.PKHeader), "RequestPeriodeYear", MatchType.Greater, CInt(DateTime.Now.Year)))
        '    criterias.opOr(subcriterias)
        'End If
        'If currentPageIndex <= 0 Then
        'Dim arlPKheader As ArrayList = New PKHeaderFacade(User).RetrieveByCriteria(criterias, dtgcari.PageCount, dtgcari.PageSize, total)

        sessionHelper.SetSession("SearchPK.critsPK", criterias)

        arlPKHeader = New PKHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgcari.PageSize, _
                total, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dtgcari.DataSource = arlPKHeader
        dtgcari.VirtualItemCount = total
        If arlPKHeader.Count > 0 Then
            dtgcari.DataBind()
        Else
            dtgcari.DataBind()
            MessageBox.Show("Data Tidak Ditemukan")
        End If

        arlPK = New PKHeaderFacade(User).Retrieve(criterias)

    End Sub

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

    Private Sub TotalAmount()
        Dim tot As Double = 0
        Dim Qty As Double = 0
        For Each item As PKHeader In arlPK
            tot = tot + item.TotalHargaTebus
            Qty = Qty + item.TotalQuantity
        Next
        lblTotal.Text = FormatNumber(tot, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
        lblQuantity.Text = FormatNumber(Qty, 0, TriState.UseDefault, TriState.UseDefault, TriState.True) & " Unit"
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "PKNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        'ddlRencanaPenebusan.Attributes.Add("OnClick", "return ValidateData();")
        BtnCari.Attributes("onclick") = "return ValidateData();"

        Dim objDealer As Dealer = Session.Item("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            dtgcari.Columns(dtgcari.Columns.Count - 1).Visible = False
            hdnTitle.Value = "MKS"

        Else
            hdnTitle.Value = "DEALER"
            txtDealerBranchCode.Attributes.Add("ReadOnly", "ReadOnly")
        End If
    End Sub

    Private Sub BindHeaderGrid()
        arlPKHeader = New ArrayList
        dtgcari.DataSource = arlPKHeader
        dtgcari.DataBind()
    End Sub

    Private Function RetriveDownload() As ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtPKNumber.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "PKNumber", MatchType.Exact, txtPKNumber.Text))
        End If
        If txtKodeDealer.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PKHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
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

    Private Sub UserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.DaftarPKViewList_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar PK")
        End If

        btnTransferData.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPKTransfer_Privilege)
        btnTransferUlang.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPKReTransfer_Privilege)
        btnDelete.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarMODelete_Privilege)
        Dim isPriceVisible As Boolean = True
        isPriceVisible = Not (SecurityProvider.Authorize(Context.User, SR.ENHSalesHargaTidakTampil_Privilege))
        Label1.Visible = isPriceVisible
        Label2.Visible = isPriceVisible
        Label3.Visible = isPriceVisible
        lblTotal.Visible = isPriceVisible
        'dtgcari.Columns(13).Visible = isPriceVisible
    End Sub
    Private Sub Download()
        If sessionHelper.GetSession("SearchPK.critsPK") Is Nothing Then
            MessageBox.Show("Download data gagal ")
            Exit Sub
        End If

        Dim dFileName As String
        Dim NewFileName As String

        'NewFileName = "ListPK " & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".txt"
        NewFileName = "ListPK[" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & "].xls"
        
        Dim dirFile As String = Server.MapPath("") & "\..\DataTemp\"
        dFileName = dirFile & NewFileName
        Dim dir As DirectoryInfo = New DirectoryInfo(dirFile)
        If Not dir.Exists Then
            dir.Create()
        End If

        Dim _user As String
        _user = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String
        _password = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String
        _webServer = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then
                Dim dFInfo As FileInfo = New FileInfo(dFileName)
                If dFInfo.Exists Then
                    dFInfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(dFileName, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteListPKData(sw)
                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & NewFileName)
        Catch ex As Exception
            Dim dummy As Integer = 0
            MessageBox.Show("Download data gagal. " & ex.Message)
        End Try
    End Sub

    Private Sub WriteListPKData(ByRef sw As StreamWriter)
        Dim critsPK As CriteriaComposite
        Dim arlPK As ArrayList = New ArrayList
        Dim objPKFac As PKHeaderFacade = New PKHeaderFacade(User)
        Dim strBuffer As String
        Dim strBufferDet As String

        If sessionHelper.GetSession("SearchPK.critsPK") Is Nothing Then
            MessageBox.Show("Download data gagal ")
            Exit Sub
        End If

        critsPK = CType(sessionHelper.GetSession("SearchPK.critsPK"), CriteriaComposite)
        arlPK = objPKFac.Retrieve(critsPK)

        'No	Kategori	Nomor Reg PK	Kode Dealer	Jenis Pesanan	Tanggal PK	Tahun Perakitan	Tipe Kendaraan	Jumlah Unit Pesanan	Jumlah Unit Alokasi	Nama Pesanan Khusus	Periode Pesan	Status	Model/Tipe/Warna


        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append("No" & tab)
        itemLine.Append("Kategori" & tab)
        itemLine.Append("Nomor Reg PK" & tab)
        itemLine.Append("Kode Dealer" & tab)
        itemLine.Append("Jenis Pesanan" & tab)
        itemLine.Append("Tanggal PK" & tab)
        itemLine.Append("Tahun Perakitan" & tab)
        itemLine.Append("Material Kendaraan" & tab)
        itemLine.Append("Jumlah Unit Pesanan" & tab)
        itemLine.Append("Jumlah Unit Alokasi" & tab)
        itemLine.Append("Nama Pesanan Khusus" & tab)
        itemLine.Append("Periode Pesan" & tab)
        itemLine.Append("Status" & tab)
        itemLine.Append("Model/Tipe/Warna" & tab)
        sw.WriteLine(itemLine.ToString())
        Dim i As Integer = 1

        For Each objPK As PKHeader In arlPK
            If objPK.PKDetails.Count = 0 Then
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(objPK.Category.CategoryCode & tab)
                itemLine.Append(objPK.PKNumber & tab)
                itemLine.Append(objPK.Dealer.DealerCode & tab)
                Dim EnumOrderType As LookUp.EnumJenisPesanan = objPK.OrderType
                itemLine.Append(EnumOrderType.ToString & tab)
                itemLine.Append(objPK.PKDate.ToString("yyyyMMdd") & tab)
                itemLine.Append(objPK.ProductionYear & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                itemLine.Append("" & tab)
                i = i + 1
            Else
                For Each objPKDet As PKDetail In objPK.PKDetails
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(objPK.Category.CategoryCode & tab)
                    itemLine.Append(objPK.PKNumber & tab)
                    itemLine.Append(objPK.Dealer.DealerCode & tab)
                    Dim EnumOrderType As LookUp.EnumJenisPesanan = objPK.OrderType
                    itemLine.Append(EnumOrderType.ToString & tab)
                    itemLine.Append(objPK.PKDate.ToString("yyyyMMdd") & tab)
                    itemLine.Append(objPK.ProductionYear & tab)

                    itemLine.Append(objPKDet.VechileColor.MaterialNumber & tab)
                    itemLine.Append(objPKDet.TargetQty & tab)

                    Dim intResponQty As Integer = 0
                    If objPK.PKStatus = 0 Or objPK.PKStatus = 1 Or objPK.PKStatus = 2 Or objPK.PKStatus = 3 Then
                        intResponQty = IIf(IsNothing(objPKDet.ResponseQty), 0, objPKDet.ResponseQty)
                    Else
                        intResponQty = objPKDet.ResponseQty
                    End If

                    itemLine.Append(intResponQty.ToString() & tab)
                    itemLine.Append(objPK.ProjectName & tab)
                    itemLine.Append(objPK.PKDate.ToString("MMM yyyy") & tab)
                    itemLine.Append(CType(objPK.PKStatus, enumStatusPK.Status).ToString() & tab)
                    itemLine.Append(objPKDet.VechileColor.MaterialDescription & tab)

                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            End If
        Next
    End Sub

    Private Sub WriteListPKData_Old(ByRef sw As StreamWriter)
        Dim critsPK As CriteriaComposite
        Dim arlPK As ArrayList = New ArrayList
        Dim objPKFac As PKHeaderFacade = New PKHeaderFacade(User)
        Dim strBuffer As String
        Dim strBufferDet As String

        If sessionHelper.GetSession("SearchPK.critsPK") Is Nothing Then
            MessageBox.Show("Download data gagal ")
            Exit Sub
        End If

        critsPK = CType(sessionHelper.GetSession("SearchPK.critsPK"), CriteriaComposite)
        arlPK = objPKFac.Retrieve(critsPK)
        For Each objPK As PKHeader In arlPK
            strBuffer = ""
            strBuffer = strBuffer & objPK.Category.CategoryCode & ";"
            strBuffer = strBuffer & objPK.PKNumber & ";"
            strBuffer = strBuffer & objPK.Dealer.DealerCode & ";"
            strBuffer = strBuffer & IIf(objPK.OrderType = 0, "Bulanan", "Tambahan") & ";" 'in Lookup.ArrayJenisPesanan
            strBuffer = strBuffer & objPK.PKDate.ToString("yyyyMMdd") & ";" 'in Lookup.ArrayJenisPesanan

            strBuffer = strBuffer & objPK.ProductionYear & ";"
            For Each objPKDet As PKDetail In objPK.PKDetails
                strBufferDet = objPKDet.VehicleTypeCode & "" & objPKDet.VehicleColorCode & ";"
                strBufferDet = strBufferDet & objPKDet.TargetQty & ";"

                If objPK.PKStatus = 0 Or objPK.PKStatus = 1 Or objPK.PKStatus = 2 Or objPK.PKStatus = 3 Then
                    '   strBufferDet = strBufferDet & objPKDet.TargetQty & ";"
                    strBufferDet = strBufferDet & IIf(IsNothing(objPKDet.ResponseQty), 0, objPKDet.ResponseQty) & ";"
                Else
                    strBufferDet = strBufferDet & objPKDet.ResponseQty & ";"
                End If

                strBufferDet = strBufferDet & objPK.ProjectName & ";"
                Dim ObjDate As DateTime = New DateTime(objPK.RequestPeriodeYear, objPK.RequestPeriodeMonth, objPK.RequestPeriodeDay)
                strBufferDet = strBufferDet & ObjDate.ToString("MMM yyyy") & ";"
                strBufferDet = strBufferDet & CType(objPK.PKStatus, enumStatusPK.Status).ToString() & ";"


                strBufferDet = strBufferDet & objPKDet.VechileColor.MaterialDescription & ";"

                sw.WriteLine(strBuffer & strBufferDet)
            Next
        Next
    End Sub

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

    Private Function validatePopulatedPK(ByRef listPK As ArrayList, ByRef messages As String) As ArrayList
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        UserPrivilege()
        If Not IsPostBack Then
            btnDelete.Attributes.Add("Onclick", "return confirm('Anda yakin melakukan Hapus ??');")
            sessionHelper.SetSession("SearchPK.critsPK", Nothing)
            sessionHelper.SetSession("Un_freeze_status_baru_daftar_pk_privilege", SecurityProvider.Authorize(Context.User, SR.Un_freeze_status_baru_daftar_pk_privilege))
            sessionHelper.SetSession("IsInPeriodForFreezePK", CommonFunction.IsInPeriodForFreezePK(User))

            BindToDropDownList()
            BindToddlCategory()
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)

            InitiatePage()
            If ddlCategory.Items.Count = 0 OrElse ddlPurpose.Items.Count = 0 OrElse ddlOrderType.Items.Count = 0 Then
                BtnCari.Enabled = False
            End If
            If GetSessionCriteria() Then
                BindGrid()
            End If


            lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"

        End If
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        'lblSearchDealer.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection")
    End Sub

    Private Sub dtgCari_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgcari.SortCommand
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

        dtgcari.SelectedIndex = -1
        dtgcari.CurrentPageIndex = 0
        BindDataToGrid(dtgcari.CurrentPageIndex)

    End Sub

    Sub dtgCari_itemdataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not (arlPKHeader Is Nothing) Then
                objPKHeader = arlPKHeader(e.Item.ItemIndex)

                Dim Un_freeze_status_baru_daftar_pk_privilege As Boolean = sessionHelper.GetSession("Un_freeze_status_baru_daftar_pk_privilege")

                Dim lblUnFreeze As LinkButton = CType(e.Item.FindControl("lblUnFreeze"), LinkButton)
                Dim imgUnFreeze As HtmlImage = CType(e.Item.FindControl("imgUnFreeze"), HtmlImage)

                Dim oDealer As Dealer = CType(sessionHelper.GetSession("DEALER"), Dealer)

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

                e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgcari.PageSize * dtgcari.CurrentPageIndex)).ToString
                e.Item.Cells(3).Text = CType(objPKHeader.PKStatus, enumStatusPK.Status).ToString()
                'e.Item.Cells(3).Text = objPKHeader.Dealer.SearchTerm1
                e.Item.Cells(10).Text = objPKHeader.Category.CategoryCode
                e.Item.Cells(11).Text = CType(objPKHeader.OrderType, LookUp.EnumJenisPesanan).ToString
                e.Item.Cells(14).Text = FormatNumber((objPKHeader.TotalHargaTebus), 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
                e.Item.Cells(17).Text = objPKHeader.Dealer.DealerCode
                Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)

                Dim lblDealerBranchCode As Label = CType(e.Item.FindControl("lblDealerBranchCode"), Label)

                If Not IsNothing(objPKHeader.DealerBranch) Then
                    lblDealerBranchCode.Text = objPKHeader.DealerBranch.DealerBranchCode & " / " & objPKHeader.DealerBranch.Term1
                End If
                If Not IsNothing(lblDealer) Then
                    lblDealer.ToolTip = objPKHeader.Dealer.SearchTerm1
                End If
                Dim linkbtn As LinkButton = e.Item.FindControl("lbnEdit")
                Dim objDealer As Dealer = sessionHelper.GetSession("DEALER")
                'If objDealer Is Nothing Then
                '    Response.Redirect("../SessionExpired.htm")
                'End If
                If (objPKHeader.PKStatus <> enumStatusPK.Status.Baru) OrElse ((Not (objPKHeader.Dealer Is Nothing)) AndAlso (objPKHeader.Dealer.ID <> objDealer.ID)) Then
                    linkbtn.Text = "<img src=""../images/detail.gif"" border=""0"" alt=""Lihat"">"
                    'linkbtn.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPKIconView_Privilege)
                End If
                Dim lblHistoryStatus As Label = CType(e.Item.FindControl("lblHistoryStatus"), Label)
                lblHistoryStatus.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpChangeStatusHistoryFU.aspx?DocType=" & LookUp.DocumentType.Pesanan_Kendaraan & "&DocNumber=" & objPKHeader.PKNumber, "", 400, 400, "DealerSelection")

                Dim lblFlow As Label = CType(e.Item.FindControl("lblFlow"), Label)
                lblFlow.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDocumentFlow.aspx?flow=PK_" & objPKHeader.PKNumber, "", 500, 450, "ViewDailyPKFlow")

                If objPKHeader.PKStatus = enumStatusPK.Status.Rilis Then
                    e.Item.BackColor = System.Drawing.Color.Yellow
                End If
                Dim stockRatioProblem As String = ""
                If (New KTB.DNet.BusinessFacade.TransactionControlPKFacade(User).IsTransactionPKBlocked(objPKHeader, objPKHeader.Dealer, EnumTransactionControlPKKind.TransactionControlPKKind.KONFIRMASI_PK_TAMBAHAN, stockRatioProblem)) Then
                    If stockRatioProblem.Trim <> "" Then
                        e.Item.BackColor = System.Drawing.Color.Red
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
            End If
        End If
    End Sub

    Private Sub BtnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCari.Click
        If Not Page.IsValid Then
            Return
        End If
        dtgcari.CurrentPageIndex = 0
        BindGrid()
    End Sub

    Sub dtgCari_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        If e.CommandName = "Edit" Then
            Dim Key As String = KTB.DNet.UI.GlobalKey.GenerateKey()
            If e.Item.Cells(2).Text = 1 Then
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                sessionHelper.SetSession("CallerPage", "SearchPK")
                SetSessionCriteria()
                'Response.Redirect("../PK/PesananKendaraanBiasa.aspx?PKNumber=" & DNetEncryption.SymmetricEncrypt(e.Item.Cells(4).Text, Key) & "&DealerCode=" & DNetEncryption.SymmetricEncrypt(e.Item.Cells(13).Text, Key) & "&Src=search&key=" & Key)
                Response.Redirect("../PK/PesananKendaraanBiasa.aspx?PKNumber=" & e.Item.Cells(7).Text & "&DealerCode=" & e.Item.Cells(17).Text & "&Src=search&key=" & Key)
            Else
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                sessionHelper.SetSession("CallerPage", "SearchPK")
                SetSessionCriteria()
                'Response.Redirect("../PK/PesananKendaraanKhusus.aspx?PKNumber=" & DNetEncryption.SymmetricEncrypt(e.Item.Cells(4).Text, Key) & "&DealerCode=" & DNetEncryption.SymmetricEncrypt(e.Item.Cells(13).Text, Key) & "&Src=search&key=" & Key)
                Response.Redirect("../PK/PesananKendaraanKhusus.aspx?PKNumber=" & e.Item.Cells(7).Text & "&DealerCode=" & e.Item.Cells(17).Text & "&Src=search&key=" & Key)
            End If
        End If
    End Sub

    Private Sub btnTransferData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransferData.Click
        If Me.txtPass.Text = String.Empty Then
            'MessageBox.Show("Silahkan Masukkan Password SAP Anda!")
            RegisterStartupScript("OpenWindow", "<script>InputPasswordPlease();</script>")
            Exit Sub
        End If

        Dim invalidPK As String = String.Empty
        Dim listPK As ArrayList = validatePopulatedPK(PopulatePKTransferData(), invalidPK)
        Dim _fileHelper As New FileHelper
        Dim str As FileInfo
        Try
            If listPK.Count > 0 Then
                'str = _fileHelper.TransferPKtoSAP(listPK)
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
                'Dim objPKHeaderFacade As New PKHeaderFacade(User)
                'update tanggal pricing period based ontransferdate
                'For i As Integer = 0 To listPK.Count - 1
                '    Dim oPK As PKHeader = listPK(i)
                '    oPK.PricingPeriodeDay = Now.Day
                '    oPK.PricingPeriodeMonth = Now.Month
                '    oPK.PricingPeriodeYear = Now.Year
                '    listPK(i) = oPK
                'Next
                'objPKHeaderFacade.validatePK(listPK)
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
            MessageBox.Show("Tidak Ada PK yang bisa Didownload")
        End Try
    End Sub

    Private Sub btnTransferUlang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransferUlang.Click
        If Me.txtPass.Text = String.Empty Then
            'MessageBox.Show("Silahkan Masukkan Password SAP Anda!")
            RegisterStartupScript("OpenWindow", "<script>InputPasswordTransferUlang();</script>")
            Exit Sub
        End If

        Dim invalidPK As String = String.Empty
        Dim listPK As ArrayList = validatePopulatedPK(PopulatePKTransferData(), invalidPK)
        Dim _fileHelper As New FileHelper
        'Dim str As FileInfo
        Try
            If listPK.Count > 0 Then
                Dim arrNewPKHeader As New ArrayList()
                Dim transferStatus As Boolean = TransferPKs(listPK, arrNewPKHeader)
                If transferStatus Then
                    updateTransferedPKs(arrNewPKHeader)
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
            MessageBox.Show(SR.UploadFail(ex.Message))
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

    Private Sub dtgcari_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgcari.ItemCommand
        If e.CommandName = "lnkFreeze" Then
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim varID As Integer = CInt(lblID.Text)
            Dim IDPK As Integer = CStr(e.CommandArgument).Split(",").GetValue(varID)
            Dim IsUnlockFreeze As Integer = CStr(e.CommandArgument).Split(",").GetValue(1)

            Dim PKFacade As New PKHeaderFacade(User)

            Dim objPK As PKHeader = PKFacade.Retrieve(IDPK)
            objPK.IsUnlockFreeze = IsUnlockFreeze
            PKFacade.Update(objPK)
            BindGrid()
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Download()
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        CommonFunction.BindVehicleSubCategoryToDDL2(ddlSubCategory, ddlCategory.SelectedItem.Text)
    End Sub
#End Region

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        PopulateDeleteConfirm()
    End Sub

    Private Sub BindVehicleSubCategoryToDDL()
        ddlSubCategory.Items.Clear()
        If ddlCategory.SelectedValue <> "" Then
            '-- SubCategoryVehicle criteria & sort
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.Exact, ""))  '-- Type still active
            criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(SubCategoryVehicle), "Name", Sort.SortDirection.ASC))  '-- Sort by Vechile type code

            '-- Bind ddlSubCategory dropdownlist
            ddlSubCategory.DataSource = New SubCategoryVehicleFacade(User).Retrieve(criterias, sortColl)
            ddlSubCategory.DataTextField = "Name"
            ddlSubCategory.DataValueField = "ID"
            ddlSubCategory.DataBind()
        End If
        ddlSubCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item
    End Sub

    Private Sub PopulateDeleteConfirm()
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objPKHeaderFacade As New PKHeaderFacade(User)
        Dim Succes As Boolean = False
        Dim Deleted As Boolean = False

        For Each oDataGridItem In dtgcari.Items
            chkExport = oDataGridItem.FindControl("ChkExport")
            If chkExport.Checked Then

                Dim lblID As Label = CType(oDataGridItem.FindControl("lblID"), Label)
                Dim varID As Integer = CInt(lblID.Text)
                Dim _co As New KTB.DNet.Domain.PKHeader

                _co.ID = varID
                _co = objPKHeaderFacade.Retrieve(_co.ID)

                If _co.PKStatus = CInt(enumStatusPK.Status.Konfirmasi).ToString() OrElse _
                    _co.PKStatus = CInt(enumStatusPK.Status.Rilis).ToString() OrElse _
                     _co.PKStatus = CInt(enumStatusPK.Status.Selesai).ToString() Then

                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                    crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.ContractHeader), "PKHeader.ID", MatchType.Exact, _co.ID.ToString()))
                    Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.ContractHeader), "ID", AggregateType.Count)

                    Dim IsPoExist As Boolean = False
                    IsPoExist = (New HelperFacade(User, GetType(KTB.DNet.Domain.ContractHeader)).IsRecordExist(crit, agg))

                    If Not IsPoExist Then
                        Deleted = True
                        Try
                            _co.RowStatus = DBRowStatus.Deleted

                            Dim pkDetails As New ArrayList

                            For Each objPkDetail As PKDetail In _co.PKDetails
                                objPkDetail.RowStatus = DBRowStatus.Deleted

                                pkDetails.Add(objPkDetail)
                            Next

                            objPKHeaderFacade.UpdatePKHeaderDetail(_co, pkDetails)
                            Succes = True
                        Catch ex As Exception
                            Succes = False
                        End Try

                    Else
                        Succes = False
                        BtnCari_Click(Me, Nothing)
                        MessageBox.Show("Sudah ada nomor O/C atas PK tersebut")
                        Return
                    End If

                End If


            End If
        Next


        If Succes Then
            BtnCari_Click(Me, Nothing)
            MessageBox.Show("PK sudah berhasil dihapus")

        End If

        If Deleted = False Then
            MessageBox.Show("Tidak ada PK yang dihapus")

        End If
    End Sub

End Class

Public Class Detail
    Public LineItem As String
    Public MaterialCode As String
    Public ContractQTY As String
    Public Model As String
End Class