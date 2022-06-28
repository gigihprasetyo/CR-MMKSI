Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security


#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Linq
#End Region

Public Class FrmDepC_InterestList
    Inherits System.Web.UI.Page

#Region "Private Variables"

    Private arlDepositC2InterestHeader As ArrayList = New ArrayList
    Private arlDepositC2InterestHeaderFilter As ArrayList = New ArrayList
    Private sHelper As New SessionHelper

    Dim TotIA As Long = 0
    Dim TotTA As Long = 0
    Dim TotNA As Long = 0

    Dim sessHelper As New SessionHelper
    Dim _lihat_daftar_interest_Privilege As Boolean = False

#End Region

#Region "Event Handlers"

    Private Sub InitiateAuthorization()
        _lihat_daftar_interest_Privilege = SecurityProvider.Authorize(Context.User, SR.lihat_daftar_interest_Privilege)

        If Not _lihat_daftar_interest_Privilege Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sparepart - Daftar Interest")
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            Initialize()
            ViewState("currentSortColumn") = "Dealer.DealerCode"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            GeneralScript.BindProductCategoryDdl(Me.ddlProductCategory, False, True)
        End If
    End Sub

    Private Sub dgDaftarInterest_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgDaftarInterest.ItemCommand
        Select Case e.CommandName
            Case "SimpanKwitansi", "SimpanLetter"
                Dim LblFullName As Label = Nothing
                If e.CommandName = "SimpanKwitansi" Then
                    LblFullName = e.Item.FindControl("lblFullNameKwitansi")
                ElseIf e.CommandName = "SimpanLetter" Then
                    LblFullName = e.Item.FindControl("lblFullNameLetter")
                End If

                If Not IsNothing(LblFullName) Then
                    Dim fileInfo As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
                    Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))

                    'Dim destFilePath As String = fileInfo1.Directory.FullName & "\DataFile\DepositInterest\" & LblFullName.Text
                    Dim destFilePath As String = fileInfo.Directory.FullName & "\" & LblFullName.Text
                    Dim newFileInfo As FileInfo = New FileInfo(destFilePath)

                    Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                    Dim success As Boolean = False
                    success = imp.Start()
                    If success Then
                        If (newFileInfo.Exists) Then
                            Try
                                'Try
                                '    Dim destinationFileInfo As New FileInfo(destFilePath)
                                '    If Not destinationFileInfo.Directory.Exists Then
                                '        destinationFileInfo.Directory.Create()
                                '    End If
                                '    fileInfo.CopyTo(destFilePath, True)
                                'Catch ex As Exception
                                'End Try

                                'Dim newFileInfo As FileInfo = New FileInfo(destFilePath)
                                'Dim exist As Boolean = newFileInfo.Exists
                                imp.StopImpersonate()
                                imp = Nothing
                                Response.Redirect("../Download.aspx?file=" & destFilePath)

                            Catch ex As Exception
                                MessageBox.Show(SR.DownloadFail(LblFullName.Text))
                            End Try
                        Else
                            MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                        End If
                    Else
                        MessageBox.Show("Gagal download file.")
                    End If
                End If
        End Select
    End Sub

    Private Sub dgDaftarInterest_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgDaftarInterest.ItemDataBound
        Dim container As Control = e.Item
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then

            Dim objDepositC2InterestHeader As DepositC2InterestHeader = CType(e.Item.DataItem, DepositC2InterestHeader)
            If Not IsNothing(objDepositC2InterestHeader) Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgDaftarInterest.CurrentPageIndex * dgDaftarInterest.PageSize)
                Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
                lblDealerCode.Text = objDepositC2InterestHeader.Dealer.DealerCode
                Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
                lblDealerName.Text = objDepositC2InterestHeader.Dealer.DealerName
                Dim lblProduk As Label = CType(e.Item.FindControl("lblProduk"), Label)
                lblProduk.Text = objDepositC2InterestHeader.ProductCategory.Code
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

                Dim btnExpandButton As HtmlImage = CType(container.FindControl("image_"), HtmlImage)
                If (Not (btnExpandButton) Is Nothing) Then
                    'btnExpandButton.Attributes.Add("OnClick", "Toggle('dgDepositAInterestH__ctl' & (e.Item.ItemIndex + 2) & '_divDepositAInterestD', 'dgDepositAInterestH__ctl' & (e.Item.ItemIndex + 2) & '_image_');")
                    Dim temp As String = "Toggle('dgDaftarInterest__ctl"
                    temp = temp & e.Item.ItemIndex + 3 & "_divDepositAInterestD'"
                    temp = temp & ", '" & "dgDaftarInterest__ctl"
                    temp = temp & e.Item.ItemIndex + 3
                    temp = temp & "_image_');"
                    btnExpandButton.Attributes.Add("OnClick", temp)
                End If

                Dim lblHeaderID As String = e.Item.Cells(0).Text

                Dim arlDetails As New ArrayList
                Dim dgDaftarInterestDetail As DataGrid = CType(container.FindControl("dgDaftarInterestDetail"), DataGrid)
                If (Not (dgDaftarInterestDetail) Is Nothing) Then
                    'Dim lblDepositInterestHID As Label = CType(e.Item.FindControl("lblDepositInterestHID"), Label)
                    'DepositAInterestHID = lblDepositInterestHID.Text.Trim
                    Dim btnDepositAInterestHID As Button = CType(container.FindControl("btnDepositInterestHID"), Button)
                    If (Not (btnDepositAInterestHID) Is Nothing) Then
                        btnDepositAInterestHID.CommandArgument = lblHeaderID
                        Me.ViewState("DepositAInterestHID") = lblHeaderID
                    End If

                    BindDataGridDetail(CInt(lblHeaderID), dgDaftarInterestDetail)

                End If
            End If

        End If
    End Sub

    Private Sub dgDaftarInterest_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgDaftarInterest.PageIndexChanged
        dgDaftarInterest.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgDaftarInterest.CurrentPageIndex)
    End Sub

    Private Sub dgDaftarInterest_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgDaftarInterest.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If

        dgDaftarInterest.SelectedIndex = -1
        dgDaftarInterest.CurrentPageIndex = 0
        BindDataGrid(dgDaftarInterest.CurrentPageIndex)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
    End Sub

#End Region

#Region "Custom Method"



    Private Sub Initialize()
        txtKodeDealer.Attributes.Add("onkeypress", "return alphaNumericExcept(event,'<>?*%$')")

        Dim objUserInfo As UserInfo = sessHelper.GetSession("LOGINUSERINFO")
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            txtKodeDealer.Attributes.Remove("readonly")
        Else
            txtKodeDealer.Attributes.Add("readonly", "readonly")
        End If

        'TODO:   'Load Periode
        BindPeriode()

        'Load Tahun
        BindYear()

        'Get Status
    End Sub

    Private Sub BindPeriode()

        ddlPeriode.Items.Clear()
        ddlPeriode.Items.Insert(0, New ListItem("Please Select", "0"))
        ddlPeriode.Items.Insert(1, New ListItem("Jan - Mar", "1"))
        ddlPeriode.Items.Insert(2, New ListItem("Apr - Jun", "2"))
        ddlPeriode.Items.Insert(3, New ListItem("Jul - Sep", "3"))
        ddlPeriode.Items.Insert(4, New ListItem("Okt - Des", "4"))

    End Sub

    Private Sub BindYear()
        Dim curYear As Integer = Date.Now.Year
        Dim startYear As Integer = curYear - 5
        Dim EndYear As Integer = curYear + 5
        Dim intYear As Integer = 0
        ddlYear.Items.Add(New ListItem("Silahkan Pilih", "-1"))

        ddlYear.Items.Clear()
        For intYear = startYear To EndYear
            ddlYear.Items.Add(intYear.ToString)
        Next

        ddlYear.Items.FindByValue(Date.Now.Year.ToString).Selected = True
    End Sub

    Private Sub BindDataGrid(ByVal IndexPage As Integer)
        Dim oDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        'If objUserInfo.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    If txtKodeDealer.Text.Trim() = String.Empty Then
        '        MessageBox.Show("Tentukan Dealer terlebih dahulu")
        '        Exit Sub
        '    End If
        'End If

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositC2InterestHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If (txtKodeDealer.Text.Trim() <> String.Empty) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositC2InterestHeader), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Trim().Replace(";", "','") & "')"))
        Else
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                If oDealer.DealerGroup.ID = 21 Then 'single dealer
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositC2InterestHeader), "Dealer.ID", MatchType.Exact, oDealer.ID))
                Else
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositC2InterestHeader), "Dealer.DealerGroup.ID", MatchType.Exact, oDealer.DealerGroup.ID))
                End If
            End If
        End If

        If CInt(ddlProductCategory.SelectedValue) > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositC2InterestHeader), "ProductCategory.ID", MatchType.Exact, ddlProductCategory.SelectedValue))
        End If

        If ddlPeriode.SelectedValue.ToString <> "0" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositC2InterestHeader), "Periode", MatchType.Exact, ddlPeriode.SelectedValue.ToString))
        End If
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositC2InterestHeader), "Year", MatchType.Exact, ddlYear.SelectedValue.ToString))

        Dim totalRow As Integer = 0
        arlDepositC2InterestHeader = New DepositC2InterestHeaderFacade(User).RetrieveActiveList(criterias, IndexPage + 1, dgDaftarInterest.PageSize, totalRow, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))

        Dim arlDepositC2InterestHeaderAll As ArrayList = New DepositC2InterestHeaderFacade(User).RetrieveActiveList(criterias, CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection))

        sHelper.SetSession("DepC2DataToDownload", arlDepositC2InterestHeaderAll)

        If (arlDepositC2InterestHeader.Count > 0) Then
            dgDaftarInterest.Visible = True
            dgDaftarInterest.DataSource = arlDepositC2InterestHeader
            dgDaftarInterest.VirtualItemCount = totalRow
            dgDaftarInterest.DataBind()
        Else
            dgDaftarInterest.Visible = False

            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub BindDataGridDetail(ByVal HeaderID As Integer, ByVal dgDaftarInterestDetail As DataGrid)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositC2InterestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositC2InterestDetail), "DepositC2InterestHeader.ID", MatchType.Exact, HeaderID))

        Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
        sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DepositC2InterestDetail), "ID", Sort.SortDirection.ASC))

        Dim arlDepositC2InterestDetail As ArrayList = New ArrayList
        arlDepositC2InterestDetail = New DepositC2InterestDetailFacade(User).Retrieve(criterias, sortColl)
        If Not IsNothing(arlDepositC2InterestDetail) Then
            dgDaftarInterestDetail.DataSource = arlDepositC2InterestDetail
            dgDaftarInterestDetail.DataBind()
        End If

    End Sub

#End Region

    Protected Sub BtnDownload_Click(sender As Object, e As EventArgs) Handles BtnDownload.Click
        Dim data As ArrayList = CType(sHelper.GetSession("DepC2DataToDownload"), ArrayList)
        If IsNothing(data) Then
            MessageBox.Show("Tidak ada data yang di download")
        Else
            DoDownload(data)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList, Optional ByVal isDetail As Boolean = False, Optional ByVal isHeader As Boolean = False)
        Dim sFileName As String = "Interest"

        sFileName = sFileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond     '-- Set file name as "Status" + "PO number".xls

        Dim DepositAData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(DepositAData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(DepositAData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)

                WriteDepositC2Data(sw, data)

                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal " + ex.Message)
        End Try
    End Sub

    Private Sub WriteDepositC2Data(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Deposit C2 - Daftar Interest")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Nama Dealer" & tab)
            itemLine.Append("Periode" & tab)
            itemLine.Append("Produk" & tab)
            itemLine.Append("Interest" & tab)
            itemLine.Append("Tax (15%)" & tab)
            itemLine.Append("Netto" & tab)

            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            Dim DealerCode As String = ""
            Dim ProductCategoryCode As String = String.Empty

            ddlPeriode.Items.Insert(1, New ListItem("Jan - Mar", "1"))
            ddlPeriode.Items.Insert(2, New ListItem("Apr - Jun", "2"))
            ddlPeriode.Items.Insert(3, New ListItem("Jul - Sep", "3"))
            ddlPeriode.Items.Insert(4, New ListItem("Okt - Des", "4"))


            For Each item As DepositC2InterestHeader In data
                itemLine.Remove(0, itemLine.Length)
                Dim periodeStr As String = ""
                If item.Periode = 1 Then
                    periodeStr = "Jan - Mar " + ddlYear.Text
                ElseIf item.Periode = 2 Then
                    periodeStr = "Apr - Jun " + ddlYear.Text
                ElseIf item.Periode = 3 Then
                    periodeStr = "Jul - Sep " + ddlYear.Text
                ElseIf item.Periode = 4 Then
                    periodeStr = "Okt - Des " + ddlYear.Text
                End If

                If DealerCode <> item.Dealer.DealerCode.ToString OrElse ProductCategoryCode <> item.ProductCategory.Code Then
                    'If ProductCategoryCode <> item.PoductCatecoryCode Then
                    itemLine.Append(item.Dealer.DealerCode & tab)
                    itemLine.Append(item.Dealer.DealerName & tab)
                    itemLine.Append(periodeStr & tab)
                    itemLine.Append(item.ProductCategory.Code & tab)
                    itemLine.Append(Val(item.InterestAmount).ToString & tab)
                    itemLine.Append(Val(item.TaxAmount).ToString & tab)
                    itemLine.Append(Val(item.NettoAmount).ToString & tab)
                    'End If
                Else
                    itemLine.Append(tab)
                    itemLine.Append(tab)
                    itemLine.Append(tab)
                    itemLine.Append(periodeStr & tab)
                    itemLine.Append(Val(item.InterestAmount).ToString & tab)
                    itemLine.Append(Val(item.TaxAmount).ToString & tab)
                    itemLine.Append(Val(item.NettoAmount).ToString & tab)
                End If
                sw.WriteLine(itemLine.ToString())
                DealerCode = item.Dealer.DealerCode.ToString
                ProductCategoryCode = item.ProductCategory.Code
            Next
        End If
    End Sub


End Class